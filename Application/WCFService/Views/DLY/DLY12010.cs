using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 販社売上修正 サービスクラス
    /// </summary>
    public class DLY12010 : BaseService
    {
        #region << 定数定義 >>

        /// <summary>売上ヘッダ テーブル名</summary>
        private const string T02_HEADER_TABLE_NAME = "T02_URHD";
        /// <summary>売上明細 テーブル名</summary>
        private const string T02_DETAIL_TABLE_NAME = "T02_URDTL";
        /// <summary>消費税 テーブル名</summary>
        private const string M73_ZEI_TABLE_NAME = "M73_ZEI";
        /// <summary>自社 テーブル名</summary>
        private const string M70_JIS_TABLE_NAME = "M70_JIS";

        #endregion

        #region << サービス定義 >>

        /// <summary>売上情報サービス</summary>
        protected T02 T02Service;
        /// <summary>仕入情報サービス</summary>
        protected T03 T03Service;
        /// <summary>移動情報サービス</summary>
        protected T05 T05Service;
        /// <summary>在庫情報サービス</summary>
        protected S03 S03Service;
        /// <summary>入出庫履歴サービス</summary>
        protected S04 S04Service;

        #endregion

        #region 拡張クラス定義

        /// <summary>
        /// 売上ヘッダ検索項目クラス定義
        /// </summary>
        public class T02_URHD_Search_Extension
        {
            public string 伝票番号 { get; set; }
            public string 会社名コード { get; set; }
            public int 伝票要否 { get; set; }
            public DateTime? 売上日 { get; set; }
            public int 売上区分 { get; set; }
            public string 得意先コード { get; set; }
            public string 得意先枝番 { get; set; }
            public string 在庫倉庫コード { get; set; }
            public string 納品伝票番号 { get; set; }
            public DateTime? 出荷日 { get; set; }
            public string 受注番号 { get; set; }
            public string 出荷元コード { get; set; }
            public string 出荷元枝番 { get; set; }
            public string 出荷元名 { get; set; }
            public string 出荷先コード { get; set; }
            public string 出荷先枝番 { get; set; }
            public string 出荷先名 { get; set; }
            public string 仕入先コード { get; set; }
            public string 仕入先枝番 { get; set; }
            public string 備考 { get; set; }
            // No-94 Add Start
            public int 通常税率対象金額 { get; set; }
            public int 軽減税率対象金額 { get; set; }
            public int 通常税率消費税 { get; set; }
            public int 軽減税率消費税 { get; set; }
            // No-94 Add End
            // No-95 Add Start
            public int 小計 { get; set; }
            public int 総合計 { get; set; }
            // No-95 Add End
            public int 消費税 { get; set; }
            public int 販社コード { get; set; }
            public bool 入力元画面DLY12010 { get; set; }
        }

        /// <summary>
        /// 売上明細検索項目クラス定義
        /// </summary>
        public class T02_URDTL_Search_Extension
        {
            public string 伝票番号 { get; set; }
            public int 行番号 { get; set; }
            public string 品番コード { get; set; }
            public DateTime? 賞味期限 { get; set; }
            public decimal? 数量 { get; set; }
            public string 単位 { get; set; }
            public decimal? 単価 { get; set; }
            public decimal? 金額 { get; set; }
            public string 税区分 { get; set; }             // No-94 Add
            public string 摘要 { get; set; }

            public bool マルセン仕入 { get; set; }
            public string 自社品番 { get; set; }
            public string 得意先品番コード { get; set; }
            public string 自社品名 { get; set; }
            public string 自社色 { get; set; }
            public string 自社色名 { get; set; }
            /// <summary>0:通常、1:軽減税率、2:非課税</summary>
            public int 消費税区分 { get; set; }
            /// <summary>1:食品、2:繊維、3:その他</summary>
            public int 商品分類 { get; set; }

        }

        #endregion


        #region 売上入力検索情報取得
        /// <summary>
        /// 売上検索情報を取得する
        /// </summary>
        /// <param name="companyCode">自社コード</param>
        /// <param name="slipNumber">伝票番号</param>
        /// <param name="pageParam">ページング番号(-1:前のデータ、0:指定明細番号、1:次のデータ</param>
        /// <param name="userId">ログインユーザID</param>
        /// <returns></returns>
        public DataSet GetData(string companyCode, string slipNumber, int pageParam, int userId)
        {
            DataSet t02ds = new DataSet();

            string targetSlipNum = getSlipNumberForPaging(companyCode, slipNumber, pageParam);
            List<T02_URHD_Search_Extension> hdList = getT02_URHD_Extension(companyCode, targetSlipNum);

            List<T02_URDTL_Search_Extension> dtlList = getT02_URDTL_Extension(companyCode, targetSlipNum);

            M73 taxService = new M73();
            List<M73_ZEI> taxList = taxService.GetDataList();
            // 項目制御用に自社情報(自社区分)を取得する
            M70 jisService = new M70();
            List<M70_JIS> jisList = jisService.GetData(companyCode, CommonConstants.PagingOption.Paging_Code.GetHashCode());

            bool isRegist = (hdList.Count == 0);
            if (isRegist)
            {
                if (string.IsNullOrEmpty(targetSlipNum))
                {
                    // 入力伝票番号が空値の場合は新規伝票を取得
                    M88 svc = new M88();

                    T02_URHD_Search_Extension hd = new T02_URHD_Search_Extension();
                    hd.伝票番号 = svc.getNextNumber(CommonConstants.明細番号ID.ID01_売上_仕入_移動, userId).ToString();
                    hd.伝票要否 = (int)CommonConstants.伝票要否.未発行;
                    hd.会社名コード = companyCode;
                    hd.売上日 = DateTime.Now;
                    hd.売上区分 = (int)CommonConstants.売上区分.通常売上;
                    hd.出荷日 = DateTime.Now;

                    hdList.Add(hd);

                }
                else
                {
                    // 入力番号なしの場合はデータなしとして返す
                    return null;
                }

            }

            int rowCnt = 1;
            foreach (T02_URDTL_Search_Extension row in dtlList)
                row.行番号 = rowCnt++;

            // Datatable変換
            DataTable dthd = KESSVCEntry.ConvertListToDataTable(hdList);
            DataTable dtdtl = KESSVCEntry.ConvertListToDataTable(dtlList);
            DataTable dttax = KESSVCEntry.ConvertListToDataTable(taxList);
            DataTable dtjis = KESSVCEntry.ConvertListToDataTable(jisList);

            dthd.TableName = T02_HEADER_TABLE_NAME;
            t02ds.Tables.Add(dthd);

            dtdtl.TableName = T02_DETAIL_TABLE_NAME;
            t02ds.Tables.Add(dtdtl);

            dttax.TableName = M73_ZEI_TABLE_NAME;
            t02ds.Tables.Add(dttax);

            dtjis.TableName = M70_JIS_TABLE_NAME;
            t02ds.Tables.Add(dtjis);

            return t02ds;

        }
        #endregion


        public Dictionary<int, string> CheckStockQty(string storeHouseCode, DataSet ds)
        {
            Dictionary<int, string> resultDic = new Dictionary<int, string>();
            DataTable dt = ds.Tables[T02_DETAIL_TABLE_NAME];

            List<T02_URDTL_Search_Extension> dtlList = getDetailDataList(dt);

            Common com = new Common();
            int code = AppCommon.IntParse(storeHouseCode);

            foreach (T02_URDTL_Search_Extension row in dtlList)
            {
                if (string.IsNullOrEmpty(row.品番コード))
                    continue;

                int product = AppCommon.IntParse(row.品番コード);
                decimal nowStockQty = 0;
                if (!com.CheckStokItemQty(code, product, row.賞味期限, out nowStockQty, row.数量 ?? 0))
                {
                    // キー：行番号、値：エラーメッセージ
                    resultDic.Add(row.行番号, string.Format("在庫数が不足しています。(現在庫数：{0:#,0.##})", nowStockQty));
                }

            }

            return resultDic;

        }

        private List<T02_URDTL_Search_Extension> getDetailDataList(DataTable dt)
        {
            var resultList =
                dt.Select("", "", DataViewRowState.CurrentRows).AsEnumerable()
                .Where(c => !string.IsNullOrEmpty(c.Field<string>("自社品番")))
                    .Select(s => new T02_URDTL_Search_Extension
                    {
                        伝票番号 = s.Field<string>("伝票番号"),
                        行番号 = s.Field<int>("行番号"),
                        品番コード = s.Field<string>("品番コード"),
                        賞味期限 = s.Field<DateTime?>("賞味期限"),
                        数量 = s.Field<decimal?>("数量"),
                        単位 = s.Field<string>("単位"),
                        単価 = s.Field<decimal?>("単価"),
                        金額 = s.Field<decimal?>("金額"),
                        摘要 = s.Field<string>("摘要"),
                        マルセン仕入 = s.Field<bool?>("マルセン仕入") ?? false,

                        自社品番 = s.Field<string>("自社品番"),
                        得意先品番コード = s.Field<string>("得意先品番コード"),
                        自社品名 = s.Field<string>("自社品名"),
                        自社色 = s.Field<string>("自社色"),
                        自社色名 = s.Field<string>("自社色名"),
                        消費税区分 = s.Field<int>("消費税区分"),
                        商品分類 = s.Field<int>("商品分類"),
                        税区分 = s.Field<string>("税区分")            // No-94 Add
                    })
                    .ToList();

            return resultList;

        }

        #region << 売上入力情報取得 >>

        #region 対象伝票番号取得
        /// <summary>
        /// 検索対象となる伝票番号を取得する
        /// </summary>
        /// <param name="compCd"></param>
        /// <param name="slipNum"></param>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        private string getSlipNumberForPaging(string compCd, string slipNum, int pageParam)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int iCompCd = int.Parse(compCd),
                    iSlipNumber;
                bool isConv = int.TryParse(slipNum, out iSlipNumber);

                int 通常売上返品区分 = CommonConstants.売上区分.通常売上返品.GetHashCode();
                var hdhanList =
                      context.T02_URHD_HAN.Where(w => w.削除日時 == null && w.会社名コード == iCompCd && w.売上区分 < 通常売上返品区分)
                        .Join(context.T02_URDTL_HAN.Where(w => w.削除日時 == null),
                            x => x.伝票番号, y => y.伝票番号, (a, b) => new { URHD_HAN = a, URDTL_HAN = b });

                switch (pageParam)
                {
                    case (int)CommonConstants.PagingOption.Paging_Before:
                        #region 前データ取得
                        if (isConv)
                        {
                            // 明細番号指定あり
                            hdhanList =
                                hdhanList.Where(w => w.URHD_HAN.伝票番号 ==
                                    context.T02_URHD_HAN.Where(v => v.会社名コード == iCompCd && v.伝票番号 < iSlipNumber && v.削除日時 == null && v.売上区分 < 通常売上返品区分)
                                        .Join(context.T02_URDTL_HAN.Where(w1 => w1.削除日時 == null),
                                            x1 => x1.伝票番号, y1 => y1.伝票番号, (a, b) => new { URHD_HAN = a, URDTL_HAN = b })
                                    .Max(m => m.URHD_HAN.伝票番号));

                            if (hdhanList.Count() == 0)
                            {
                                // この場合現在指定番号が最小明細番号なので自分を再取得する
                                return getSlipNumberForPaging(compCd, slipNum, (int)CommonConstants.PagingOption.Paging_Code);
                            }

                        }
                        else
                        {
                            // 明細番号指定なし
                            hdhanList =
                                hdhanList.Where(w => w.URHD_HAN.伝票番号 ==
                                    context.T02_URHD_HAN.Where(v => v.会社名コード == iCompCd && v.削除日時 == null && v.売上区分 < 通常売上返品区分)
                                        .Join(context.T02_URDTL_HAN.Where(w1 => w1.削除日時 == null),
                                            x => x.伝票番号, y => y.伝票番号, (a, b) => new { URHD_HAN = a, URDTL_HAN = b })
                                    .Min(m => m.URHD_HAN.伝票番号));
                        }
                        #endregion
                        break;

                    case (int)CommonConstants.PagingOption.Paging_Code:
                        // 一致データ取得
                        if (isConv)
                        {
                            hdhanList = hdhanList.Where(w => w.URHD_HAN.伝票番号 == iSlipNumber);

                            if (hdhanList.Count() == 0)
                                // この場合は存在しない明細番号が指定されている
                                return slipNum;

                        }
                        else
                            return string.Empty;

                        break;

                    case (int)CommonConstants.PagingOption.Paging_After:
                        #region 次データ取得
                        if (isConv)
                        {
                            // 明細番号指定あり
                            hdhanList =
                                hdhanList.Where(w => w.URHD_HAN.伝票番号 ==
                                    context.T02_URHD_HAN.Where(v => v.会社名コード == iCompCd && v.伝票番号 > iSlipNumber && v.削除日時 == null && v.売上区分 < 通常売上返品区分)
                                        .Join(context.T02_URDTL_HAN.Where(w1 => w1.削除日時 == null),
                                            x => x.伝票番号, y => y.伝票番号, (a, b) => new { URHD_HAN = a, URDTL_HAN = b })
                                        .Min(m => m.URHD_HAN.伝票番号));

                            if (hdhanList.Count() == 0)
                            {
                                // この場合現在指定番号が最大明細番号なので自分を再取得する
                                return getSlipNumberForPaging(compCd, slipNum, (int)CommonConstants.PagingOption.Paging_Code);
                            }

                        }
                        else
                        {
                            // 明細番号指定なし
                            hdhanList =
                                hdhanList.Where(w => w.URHD_HAN.伝票番号 ==
                                    context.T02_URHD_HAN.Where(v => v.会社名コード == iCompCd && v.削除日時 == null && v.売上区分 < 通常売上返品区分)
                                        .Join(context.T02_URDTL_HAN.Where(w1 => w1.削除日時 == null),
                                            x => x.伝票番号, y => y.伝票番号, (a, b) => new { URHD_HAN = a, URDTL_HAN = b })
                                    .Max(m => m.URHD_HAN.伝票番号));
                        }
                        #endregion
                        break;

                    default:
                        break;

                }

                var targetData = hdhanList.FirstOrDefault();

                if (targetData != null)
                    return hdhanList.FirstOrDefault().URHD_HAN.伝票番号.ToString();

                else
                    return string.Empty;

            }

        }

        #endregion

        #region 売上ヘッダ情報取得
        /// <summary>
        /// 売上ヘッダ情報を取得する
        /// </summary>
        /// <param name="companyCode">会社名コード</param>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        private List<T02_URHD_Search_Extension> getT02_URHD_Extension(string companyCode, string slipNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int code, num;
                int 通常売上返品区分 = CommonConstants.売上区分.通常売上返品.GetHashCode();

                if (int.TryParse(companyCode, out code) && int.TryParse(slipNumber, out num))
                {
                    var result =
                        context.T02_URHD_HAN
                            .Where(w => w.削除日時 == null && w.伝票番号 == num && w.会社名コード == code && w.売上区分 < 通常売上返品区分)
                        .Join(context.T02_URDTL_HAN.Where(w => w.削除日時 == null),
                            x => x.伝票番号, y => y.伝票番号, (a, b) => new { URHD_HAN = a, URDTL_HAN = b })
                        .Join(context.T03_SRHD_HAN.Where(w => w.削除日時 == null),
                            x => x.URHD_HAN.伝票番号, y => y.伝票番号, (c, d) => new { c.URHD_HAN, c.URDTL_HAN, SRHD_HAN = d })
                        .Join(context.T03_SRDTL_HAN.Where(w => w.削除日時 == null),
                            x => x.URHD_HAN.伝票番号, y => y.伝票番号, (e, f) => new { e.URHD_HAN, e.URDTL_HAN, e.SRHD_HAN, SRDTL_HAN = f })
                        .GroupJoin(context.T02_URHD.Where(w => w.削除日時 == null),
                            x => x.URHD_HAN.伝票番号, y => y.伝票番号, (x, y) => new { x, y })
                        .SelectMany(z => z.y.DefaultIfEmpty(), (g, h) => new { g.x.URHD_HAN, g.x.URDTL_HAN, g.x.SRHD_HAN, g.x.SRDTL_HAN, URHD = h })
                        .ToList()
                        .Select(x => new T02_URHD_Search_Extension
                            {
                                伝票番号 = x.URHD_HAN.伝票番号.ToString(),
                                会社名コード = x.URHD_HAN.会社名コード.ToString(),
                                伝票要否 = x.URHD_HAN.伝票要否,
                                売上日 = x.URHD_HAN.売上日,
                                売上区分 = x.URHD_HAN.売上区分,
                                得意先コード = x.URHD == null ? string.Empty : x.URHD.得意先コード.ToString(),
                                得意先枝番 = x.URHD == null ? string.Empty : x.URHD.得意先枝番.ToString(),
                                在庫倉庫コード = x.URHD_HAN.在庫倉庫コード.ToString(),
                                納品伝票番号 = x.URHD_HAN.納品伝票番号.ToString(),
                                出荷日 = x.URHD_HAN.出荷日,
                                受注番号 = x.URHD_HAN.受注番号.ToString(),
                                出荷元コード = x.URHD == null ? string.Empty : x.URHD.出荷元コード.ToString(),
                                出荷元枝番 = x.URHD == null ? string.Empty : x.URHD.出荷元枝番.ToString(),
                                出荷元名 = x.URHD == null ? string.Empty : x.URHD.出荷元名,
                                出荷先コード = x.URHD == null ? string.Empty : x.URHD.出荷先コード.ToString(),
                                出荷先枝番 = x.URHD == null ? string.Empty : x.URHD.出荷先枝番.ToString(),
                                出荷先名 = x.URHD == null ? string.Empty : x.URHD.出荷先名,
                                仕入先コード = x.URHD_HAN.仕入先コード.ToString(),
                                仕入先枝番 = x.URHD_HAN.仕入先枝番.ToString(),
                                備考 = x.URHD_HAN.備考,
                                // No-94 Add Start
                                通常税率対象金額 = x.URHD_HAN.通常税率対象金額 ?? 0,
                                軽減税率対象金額 = x.URHD_HAN.軽減税率対象金額 ?? 0,
                                通常税率消費税 = x.URHD_HAN.通常税率消費税 ?? 0,
                                軽減税率消費税 = x.URHD_HAN.軽減税率消費税 ?? 0,
                                // No-94 Add End
                                // No-95 Add Start
                                小計 = x.URHD_HAN.小計 ?? 0,
                                総合計 = x.URHD_HAN.総合計 ?? 0,
                                // No-95 Add End
                                消費税 = x.URHD_HAN.消費税,
                                販社コード = x.URHD_HAN.販社コード,
                                入力元画面DLY12010 = x.URHD == null ? true : false
                            });
                    return result.ToList();
                }
                else
                {
                    return new List<T02_URHD_Search_Extension>();

                }

            }

        }
        #endregion

        #region 売上明細情報取得
        /// <summary>
        /// 売上明細情報(品番情報含む)を取得する
        /// </summary>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        private List<T02_URDTL_Search_Extension> getT02_URDTL_Extension(string myCompany, string slipNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int code, num;
                if (int.TryParse(myCompany, out code) && int.TryParse(slipNumber, out num))
                {
                    int 通常売上返品区分 = CommonConstants.売上区分.通常売上返品.GetHashCode();

                    // 伝票番号から売上ヘッダ情報を取得
                    var urhd_han =
                        context.T02_URHD_HAN
                            .Where(w => w.削除日時 == null && w.伝票番号 == num && w.売上区分 < 通常売上返品区分)
                            .FirstOrDefault();

                    if (urhd_han == null)
                        return new List<T02_URDTL_Search_Extension>();

                    var result =
                        context.T02_URDTL_HAN
                            .Where(w => w.削除日時 == null && w.伝票番号 == num)
                        .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                            x => x.品番コード, y => y.品番コード, (x, y) => new { x, y })
                        .SelectMany(z => z.y.DefaultIfEmpty(), (a, b) => new { URDTL_HAN = a.x, HIN = b })
                        .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null),
                            x => x.HIN.自社色, y => y.色コード, (x, y) => new { x, y })
                        .SelectMany(z => z.y.DefaultIfEmpty(), (c, d) => new { c.x.URDTL_HAN, c.x.HIN, IRO = d })
                        .ToList()
                        .Select(x => new T02_URDTL_Search_Extension
                            {
                                伝票番号 = x.URDTL_HAN.伝票番号.ToString(),
                                行番号 = x.URDTL_HAN.行番号,
                                品番コード = x.URDTL_HAN.品番コード.ToString(),
                                自社品番 = x.HIN.自社品番,
                                得意先品番コード = string.Empty,
                                自社品名 = x.HIN.自社品名,
                                自社色 = x.HIN.自社色,
                                自社色名 = x.IRO != null ? x.IRO.色名称 : string.Empty,
                                賞味期限 = x.URDTL_HAN.賞味期限,
                                数量 = x.URDTL_HAN.数量,
                                単位 = x.URDTL_HAN.単位,
                                単価 = x.URDTL_HAN.単価,
                                金額 = x.URDTL_HAN.金額 ?? 0,
                                税区分 =           // No-94 Add
                                    x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? CommonConstants.消費税区分略称_軽減税率 :
                                    x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.非課税 ? CommonConstants.消費税区分略称_非課税 : string.Empty,
                                摘要 = x.URDTL_HAN.摘要,
                                マルセン仕入 = x.URDTL_HAN != null,
                                消費税区分 = x.HIN.消費税区分 ?? 0,
                                商品分類 = x.HIN.商品分類 ?? 0
                            })
                            .OrderBy(o => o.伝票番号)
                            .ThenBy(t => t.行番号);

                    return result.ToList();

                }
                else
                {
                    return new List<T02_URDTL_Search_Extension>();
                }

            }

        }
        #endregion

        #endregion

        #region 売上入力情報登録・更新
        /// <summary>
        /// 売上入力情報を登録・更新する
        /// </summary>
        /// <param name="ds">
        /// 売上データセット
        /// [0:T032_URHD、1:T02_URDTL]
        /// </param>
        /// <param name="userId">ユーザID</param>
        /// <returns></returns>
        public int Update(DataSet ds, int userId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    T02Service = new T02(context, userId);
                    T03Service = new T03(context, userId);
                    T05Service = new T05(context, userId);
                    S03Service = new S03(context, userId);
                    S04Service = new S04(context, userId, S04.機能ID.売上入力);

                    try
                    {
                        DataRow hdRow = ds.Tables[T02_HEADER_TABLE_NAME].Rows[0];
                        T02_URHD urhd = convertDataRowToT02_URHD_Entity(hdRow);
                        DataTable dtUrDtl = ds.Tables[T02_DETAIL_TABLE_NAME];

                        // 登録対象データの状態(新規or編集)を取得する
                        bool isRegistData =
                            context.T02_URHD
                                .Where(w => w.削除日時 == null && w.伝票番号 == urhd.伝票番号)
                                .Count() == 0;

                        switch (urhd.売上区分)
                        {
                            case (int)CommonConstants.売上区分.販社売上:
                            case (int)CommonConstants.売上区分.メーカー販社商流直送:
                                setSalesCompanyProc(urhd, dtUrDtl, hdRow);
                                break;

                            default:
                                break;

                        }

                        // 変更状態を確定
                        context.SaveChanges();

                        tran.Commit();

                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }

                }

            }

            return 1;

        }
        #endregion

        #region 売上入力情報削除
        /// <summary>
        /// 売上入力情報の削除をおこなう
        /// </summary>
        /// <param name="slipNumber">伝票番号</param>
        /// <param name="userId">ユーザID</param>
        /// <returns></returns>
        public void Delete(string slipNumber, int userId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    T02Service = new T02(context, userId);
                    T03Service = new T03(context, userId);
                    T05Service = new T05(context, userId);
                    S03Service = new S03(context, userId);
                    S04Service = new S04(context, userId, S04.機能ID.売上入力);

                    int number = 0;
                    if (int.TryParse(slipNumber, out number))
                    {
                        try
                        {

                            var urhd_han =
                               context.T02_URHD_HAN
                                   .Where(x => x.削除日時 == null &&
                                       x.伝票番号 == number)
                                        .FirstOrDefault();

                            var urdtl_han =
                               context.T02_URDTL_HAN
                                   .Where(x => x.削除日時 == null &&
                                       x.伝票番号 == number)
                                        .ToList();

                            // 伝票データが存在しない場合は処理しない。
                            if (urdtl_han == null || urdtl_han.Count == 0) return;

                            var mrsnsr =
                                context.T03_SRDTL_HAN.Where(c => c.削除日時 == null).ToList();

                            #region 販社移動削除

                            // ①仕入ヘッダ論理削除
                            T03_SRHD_HAN hanSrHdData = T03Service.T03_SRHD_HAN_Delete(number);

                            // ②仕入明細論理削除
                            List<T03_SRDTL_HAN> hanSrDtlList = T03Service.T03_SRDTL_HAN_Delete(number);

                            // ③売上ヘッダ論理削除
                            T02_URHD_HAN hanUrHdData = T02Service.T02_URHD_HAN_Delete(number);

                            // ④売上明細論理削除
                            List<T02_URDTL_HAN> hanUrDtlList = T02Service.T02_URDTL_HAN_Delete(number);

                            #endregion

                            // 変更状態を確定
                            context.SaveChanges();

                            tran.Commit();
                        }
                        catch
                        {
                            tran.Rollback();
                            throw new Exception("削除処理実行中にエラーが発生しました。");

                        }

                    }
                    else
                    {
                        throw new KeyNotFoundException("伝票番号が正しくありません");
                    }
                }

            }

        }
        #endregion


        #region << 売上区分別更新処理群 >>

        #region 販社売上更新処理/メーカー販社商流直送更新処理
        /// <summary>
        /// 販社売上時の更新処理
        /// </summary>
        /// <param name="urhd"></param>
        /// <param name="dtlTbl"></param>
        /// <param name="hdRow"></param>
        private void setSalesCompanyProc(T02_URHD urhd, DataTable dtlTbl, DataRow hdRow)
        {

            // マルセン仕入チェックON（以降の処理用）
            foreach (DataRow row in dtlTbl.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;

                row["マルセン仕入"] = true;
            }

            // 1.販社仕入ヘッダの更新
            setT03_SRHD_HAN_Update(urhd, dtlTbl, CommonConstants.仕入区分.通常, hdRow);

            // 2.販社仕入明細の更新
            setT03_SRDTL_HAN_Update(urhd, dtlTbl);

            // 3.販社売上ヘッダの更新
            setT02_URHD_HAN_Update(urhd, dtlTbl, hdRow);

            // 4.販社売上明細の更新
            setT02_URDTL_HAN_Update(urhd, dtlTbl);

        }
        #endregion

        #endregion

        #region << 販社仕入ヘッダ登録更新処理 >>
        /// <summary>
        /// 販社仕入ヘッダの登録・更新をおこなう
        /// </summary>
        /// <param name="t02Data"></param>
        /// <param name="dtlTbl"></param>
        /// <param name="supKbn"></param>
        /// <param name="hdRow"></param>
        public void setT03_SRHD_HAN_Update(T02_URHD t02Data, DataTable dtlTbl, CommonConstants.仕入区分 supKbn, DataRow hdRow)
        {
            // 売上対象の自社情報・取引先情報を取得する
            var m70自社 = getFromJis();

            T03_SRHD_HAN srhdhan = new T03_SRHD_HAN();

            srhdhan.伝票番号 = t02Data.伝票番号;
            srhdhan.会社名コード = ParseInt(hdRow["販社コード"]) ?? 0;
            srhdhan.仕入日 = t02Data.売上日;
            srhdhan.仕入区分 = supKbn.GetHashCode();
            srhdhan.仕入先コード = m70自社.自社コード;
            srhdhan.入荷先コード = ParseInt(hdRow["販社コード"]) ?? 0;
            srhdhan.発注番号 = t02Data.受注番号;
            srhdhan.備考 = t02Data.備考;
            // No-94 Add Start
            srhdhan.通常税率対象金額 = t02Data.通常税率対象金額;
            srhdhan.軽減税率対象金額 = t02Data.軽減税率対象金額;
            srhdhan.通常税率消費税 = t02Data.通常税率消費税;
            srhdhan.軽減税率消費税 = t02Data.軽減税率消費税;
            // No-94 Add End
            // No-95 Add Start
            srhdhan.小計 = t02Data.小計;
            srhdhan.総合計 = t02Data.総合計;
            // No-95 Add End
            srhdhan.消費税 = t02Data.消費税;

            T03Service.T03_SRHD_HAN_Update(srhdhan);

        }
        #endregion

        #region << 販社仕入明細登録更新処理 >>
        /// <summary>
        /// 仕入明細情報の更新処理をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dt">明細テーブル</param>
        /// <param name="userId">ログインユーザID</param>
        protected int setT03_SRDTL_HAN_Update(T02_URHD urhd, DataTable dt)
        {
            T03Service.T03_SRDTL_HAN_DeleteRecords(urhd.伝票番号);

            // 明細追加
            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;

                T02_URDTL dtlData = convertDataRowToT02_URDTL_Entity(row);
                bool bval,
                    仕入チェック = bool.TryParse(row["マルセン仕入"].ToString(), out bval) ? bval : false;

                if (dtlData.品番コード <= 0)
                    continue;

                // チェック判定アリかつチェックボックス=オフの場合は登録処理をスキップする
                if (仕入チェック == false)
                    continue;

                T03_SRDTL_HAN srdtlhan = new T03_SRDTL_HAN();
                srdtlhan.伝票番号 = urhd.伝票番号;
                srdtlhan.行番号 = dtlData.行番号;
                srdtlhan.品番コード = dtlData.品番コード;
                srdtlhan.賞味期限 = dtlData.賞味期限;
                srdtlhan.数量 = dtlData.数量;
                srdtlhan.単位 = dtlData.単位;
                srdtlhan.単価 = dtlData.単価;
                srdtlhan.金額 = dtlData.金額 ?? 0;
                srdtlhan.摘要 = dtlData.摘要;

                T03Service.T03_SRDTL_HAN_Update(srdtlhan);

            }

            return 1;

        }
        #endregion

        #region << 販社売上ヘッダ更新処理 >>

        /// <summary>
        /// 販社売上ヘッダ情報の更新をおこなう
        /// </summary>
        /// <param name="urhdData">売上ヘッダデータ</param>
        /// <param name="dt">売上明細データテーブル</param>
        /// <param name="hdRow"></param>
        protected void setT02_URHD_HAN_Update(T02_URHD urhdData, DataTable dt, DataRow hdRow)
        {

            using (_context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                #region 売上区分により仕入先を切り替える
                ////// ※会社名コードは必ずマルセン(自社)になる
                int wk自社コード;

                switch (urhdData.売上区分)
                {
                    case (int)CommonConstants.売上区分.販社売上:
                    case (int)CommonConstants.売上区分.販社売上返品:
                        // >>仕入先にはマルセン(自社)
                        // >>在庫倉庫には販社倉庫
                        //   が設定されている想定
                        var m70自社 = _context.M70_JIS.Where(c => c.自社区分 == (int)CommonConstants.自社区分.自社).FirstOrDefault();
                        // ⇒会社名コード = 仕入先より取得
                        wk自社コード = m70自社.自社コード;

                        break;

                    case (int)CommonConstants.売上区分.メーカー販社商流直送:
                    case (int)CommonConstants.売上区分.メーカー販社商流直送返品:
                        // >> 仕入先にはメーカー等
                        // >> 在庫倉庫にはマルセン(自社)
                        //   が設定されている想定
                        // ⇒会社名コード = 在庫倉庫より取得
                        wk自社コード = getM70_JISFromM22_SOUK(urhdData.在庫倉庫コード).自社コード;
                        break;

                    default:
                        // その他の場合は入力内容をそのまま設定
                        wk自社コード = urhdData.会社名コード;
                        break;
                }
                #endregion

                T02_URHD_HAN urhd = new T02_URHD_HAN();

                urhd.伝票番号 = urhdData.伝票番号;
                urhd.会社名コード = wk自社コード;
                urhd.伝票要否 = urhdData.伝票要否;
                urhd.売上日 = urhdData.売上日;
                urhd.売上区分 = urhdData.売上区分;
                urhd.販社コード = ParseInt(hdRow["販社コード"]) ?? 0;
                urhd.在庫倉庫コード = urhdData.在庫倉庫コード;
                urhd.納品伝票番号 = urhdData.納品伝票番号;
                urhd.出荷日 = urhdData.出荷日;
                urhd.受注番号 = urhdData.受注番号;
                urhd.仕入先コード = urhdData.仕入先コード;
                urhd.仕入先枝番 = urhdData.仕入先枝番;
                urhd.備考 = urhdData.備考;
                // No-94 Add Start
                urhd.通常税率対象金額 = urhdData.通常税率対象金額;
                urhd.軽減税率対象金額 = urhdData.軽減税率対象金額;
                urhd.通常税率消費税 = urhdData.通常税率消費税;
                urhd.軽減税率消費税 = urhdData.軽減税率消費税;
                // No-94 Add End
                // No-95 Add Start
                urhd.小計 = urhdData.小計;
                urhd.総合計 = urhdData.総合計;
                // No-95 Add End
                urhd.消費税 = urhdData.消費税;

                // 販社売上ヘッダの登録実行
                T02Service.T02_URHD_HAN_Update(urhd);
            }
        }

        #endregion

        #region << 販社売上明細更新処理 >>

        /// <summary>
        /// 販社売上明細情報の更新をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dt">明細テーブル</param>
        /// <param name="userId">ログインユーザID</param>
        /// <returns></returns>
        protected int setT02_URDTL_HAN_Update(T02_URHD urhd, DataTable dt)
        {
            using (_context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // Del-Insの為対象伝票を物理削除
                T02Service.T02_URDTL_HAN_DeleteRecords(urhd.伝票番号);

                #region 売上区分によりS税区分IDを切り替える

                switch (urhd.売上区分)
                {
                    case (int)CommonConstants.売上区分.販社売上:
                    case (int)CommonConstants.売上区分.販社売上返品:
                        // >>仕入先にはマルセン(自社)
                        var m70自社 = _context.M70_JIS.Where(c => c.自社区分 == (int)CommonConstants.自社区分.自社).FirstOrDefault();
                        break;
                }
                #endregion

                // 明細追加
                foreach (DataRow row in dt.Rows)
                {
                    if (row.RowState == DataRowState.Deleted)
                        continue;

                    T02_URDTL dtlData = convertDataRowToT02_URDTL_Entity(row);
                    bool bval,
                        仕入チェック = bool.TryParse(row["マルセン仕入"].ToString(), out bval) ? bval : false;

                    if (dtlData.品番コード <= 0)
                        continue;

                    // チェック判定アリかつチェックボックス=オフの場合は登録処理をスキップする
                    if (仕入チェック == false)
                        continue;

                    T02_URDTL_HAN urdtl = new T02_URDTL_HAN();
                    urdtl.伝票番号 = urhd.伝票番号;
                    urdtl.行番号 = dtlData.行番号;
                    urdtl.品番コード = dtlData.品番コード;
                    urdtl.賞味期限 = dtlData.賞味期限;
                    urdtl.数量 = dtlData.数量;
                    urdtl.単位 = dtlData.単位;
                    urdtl.単価 = dtlData.単価;
                    urdtl.金額 = dtlData.金額;
                    urdtl.摘要 = dtlData.摘要;

                    T02Service.T02_URDTL_HAN_Update(urdtl);

                }
            }
            return 1;

        }

        #endregion

        #region << 処理関連 >>

        #region DataRow to EntityClass
        /// <summary>
        /// DataRow型をT02_URHDに変換する
        /// </summary>
        /// <param name="drow"></param>
        /// <returns></returns>
        protected T02_URHD convertDataRowToT02_URHD_Entity(DataRow drow)
        {
            T02_URHD urhd = new T02_URHD();
            int ival = 0;

            urhd.伝票番号 = ParseNumeric<int>(drow["伝票番号"]);
            urhd.会社名コード = ParseNumeric<int>(drow["会社名コード"]);
            urhd.伝票要否 = ParseNumeric<int>(drow["伝票要否"]);
            urhd.売上日 = (DateTime)DateParse(drow["売上日"]);
            urhd.売上区分 = ParseNumeric<int>(drow["売上区分"]);
            urhd.得意先コード = ParseNumeric<int>(drow["得意先コード"]);
            urhd.得意先枝番 = ParseNumeric<int>(drow["得意先枝番"]);
            urhd.在庫倉庫コード = ParseNumeric<int>(drow["在庫倉庫コード"]);
            urhd.納品伝票番号 = ParseInt(drow["納品伝票番号"]);
            urhd.出荷日 = (DateTime)DateParse(drow["出荷日"]);
            urhd.受注番号 = ParseInt(drow["受注番号"]);
            urhd.出荷元コード = ParseInt(drow["出荷元コード"]);
            urhd.出荷元枝番 = ParseInt(drow["出荷元枝番"]);
            urhd.出荷元名 = drow["出荷元名"].ToString();
            urhd.出荷先コード = ParseInt(drow["出荷先コード"]);
            urhd.出荷先枝番 = ParseInt(drow["出荷先枝番"]);
            urhd.出荷先名 = drow["出荷先名"].ToString();
            urhd.仕入先コード = ParseInt(drow["仕入先コード"]);
            urhd.仕入先枝番 = ParseInt(drow["仕入先枝番"]);
            urhd.備考 = drow["備考"].ToString();
            // REMARKS:カラムが無い場合は参照しない
            urhd.元伝票番号 = drow.Table.Columns.Contains("元伝票番号") ?
                (int.TryParse(drow["元伝票番号"].ToString(), out ival) ? ival : (int?)null) : (int?)null;
            // No-94 Add Start
            urhd.通常税率対象金額 = ParseNumeric<int>(drow["通常税率対象金額"]);
            urhd.軽減税率対象金額 = ParseNumeric<int>(drow["軽減税率対象金額"]);
            urhd.通常税率消費税 = ParseNumeric<int>(drow["通常税率消費税"]);
            urhd.軽減税率消費税 = ParseNumeric<int>(drow["軽減税率消費税"]);
            // No-94 Add End
            // No-95 Add Start
            urhd.小計 = ParseNumeric<int>(drow["小計"]);
            urhd.総合計 = ParseNumeric<int>(drow["総合計"]);
            // No-95 Add End
            urhd.消費税 = ParseNumeric<int>(drow["消費税"]);

            return urhd;

        }

        /// <summary>
        /// DataRow型をT02_SRDTLに変換する
        /// </summary>
        /// <param name="drow"></param>
        /// <returns></returns>
        protected T02_URDTL convertDataRowToT02_URDTL_Entity(DataRow drow)
        {
            T02_URDTL urdtl = new T02_URDTL();
            DataRow wkRow = drow.Table.Clone().NewRow();

            if (drow.RowState == DataRowState.Deleted)
            {   // 対象が削除行の場合
                // 対象データの参照ができるようにする
                DataTable wkTbl = drow.Table.Copy();
                wkTbl.RejectChanges();

                var orgCode = drow["品番コード", DataRowVersion.Original];

                foreach (DataRow dr in wkTbl.Select(string.Format("品番コード = {0}", orgCode)))
                {
                    wkRow.ItemArray = dr.ItemArray;
                    break;  // 複数取る事はないと思うが念の為
                }

            }
            else
            {
                wkRow.ItemArray = drow.ItemArray;
            }

            urdtl.伝票番号 = ParseNumeric<int>(wkRow["伝票番号"]);
            urdtl.行番号 = ParseNumeric<int>(wkRow["行番号"]);
            urdtl.品番コード = ParseNumeric<int>(wkRow["品番コード"]);
            urdtl.賞味期限 = DateParse(wkRow["賞味期限"]);
            urdtl.数量 = ParseNumeric<decimal>(wkRow["数量"]);
            urdtl.単位 = wkRow["単位"].ToString();
            urdtl.単価 = ParseNumeric<decimal>(wkRow["単価"]);
            urdtl.金額 = Decimal.ToInt32(ParseNumeric<decimal>(wkRow["金額"]));
            urdtl.摘要 = wkRow["摘要"].ToString();

            return urdtl;

        }
        #endregion

        #region 指定倉庫の自社マスタ取得
        /// <summary>
        /// 倉庫コードから対象の自社マスタを取得する
        /// </summary>
        /// <param name="stokpile">倉庫コード</param>
        /// <returns></returns>
        protected M70_JIS getM70_JISFromM22_SOUK(int stokpile)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                var jis =
                    context.M70_JIS
                        .Where(w =>
                            w.削除日時 == null &&
                            w.自社コード ==
                                context.M22_SOUK
                                    .Where(v => v.倉庫コード == stokpile)
                                    .Select(s => s.寄託会社コード)
                                    .FirstOrDefault())
                        .FirstOrDefault();

                return jis;

            }

        }

        #endregion

        #region 会社名から該当倉庫を取得
        /// <summary>
        /// 自社(マルセン)の倉庫コードを取得する
        /// </summary>
        /// <returns></returns>
        private int getStockpileFromJis()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                var jis =
                    context.M70_JIS
                        .Where(w =>
                            w.削除日時 == null &&
                            w.自社区分 == (int)CommonConstants.自社区分.自社)
                        .FirstOrDefault();

                return getStockpileFromJis(jis.自社コード);

            }

        }

        /// <summary>
        /// 自社(マルセン)マスタデータを取得する
        /// </summary>
        /// <returns></returns>
        private M70_JIS getFromJis()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                var jis =
                    context.M70_JIS
                        .Where(w =>
                            w.削除日時 == null &&
                            w.自社区分 == (int)CommonConstants.自社区分.自社)
                        .FirstOrDefault();

                return jis;
            }
        }

        /// <summary>
        /// 会社名コードから該当の倉庫コードを取得する
        /// </summary>
        /// <param name="context"></param>
        /// <param name="会社名コード">M70_JIS</param>
        /// <returns></returns>
        private int getStockpileFromJis(int 会社名コード)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                var souk =
                    context.M22_SOUK
                        .Where(w =>
                            w.削除日時 == null &&
                            w.場所会社コード == 会社名コード &&
                            w.寄託会社コード == 会社名コード)
                        .Select(s => s.倉庫コード)
                        .FirstOrDefault();

                return souk;

            }

        }

        #region T02の在庫倉庫コードを取得
        /// <summary>
        ///T02の倉庫コードを取得する
        /// </summary>
        /// <returns></returns>
        private int getT02ZaikoSouk(int p会社名コード, int p伝票番号)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                var t02zaiSouk =
                    context.T02_URHD
                        .Where(w =>
                            w.削除日時 == null &&
                            w.会社名コード == p会社名コード &&
                            w.伝票番号 == p伝票番号)
                            .Select(c => c.在庫倉庫コード)
                        .FirstOrDefault();

                return t02zaiSouk;
            }
        }
        #endregion

        #endregion

        #region 卸値取得
        /// <summary>
        /// 対象の卸値を取得する
        /// </summary>
        /// <param name="company"></param>
        /// <param name="eda"></param>
        /// <param name="productCode"></param>
        /// <returns></returns>
        private decimal getWholesalePrice(int productCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {

                // 仕入先売価が見つからない場合は品番マスタの仕入単価を返す
                var m09 =
                    context.M09_HIN
                        .Where(w =>
                            w.削除日時 == null &&
                            w.品番コード == productCode)
                        .FirstOrDefault();

                if (m09 == null)
                    return 0;

                return m09.卸値 ?? 0;

            }

        }
        #endregion

        #endregion

    }

}