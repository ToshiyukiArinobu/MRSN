using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 売上入力 サービスクラス
    /// </summary>
    public class DLY03010 : BaseService
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
            public int 消費税 { get; set; }

            public int Ｓ支払消費税区分 { get; set; }
            public int Ｓ税区分ID { get; set; }

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
                            商品分類 = s.Field<int>("商品分類")
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
                var hdList =
                    context.T02_URHD.Where(w => w.削除日時 == null && w.会社名コード == iCompCd && w.売上区分 < 通常売上返品区分);

                switch (pageParam)
                {
                    case (int)CommonConstants.PagingOption.Paging_Before:
                        #region 前データ取得
                        if (isConv)
                        {
                            // 明細番号指定あり
                            hdList =
                                hdList.Where(w => w.伝票番号 ==
                                    context.T02_URHD.Where(v => v.会社名コード == iCompCd && v.伝票番号 < iSlipNumber && v.削除日時 == null && v.売上区分 < 通常売上返品区分)
                                        .Max(m => m.伝票番号));

                            if (hdList.Count() == 0)
                            {
                                // この場合現在指定番号が最小明細番号なので自分を再取得する
                                return getSlipNumberForPaging(compCd, slipNum, (int)CommonConstants.PagingOption.Paging_Code);
                            }

                        }
                        else
                        {
                            // 明細番号指定なし
                            hdList =
                                hdList.Where(w => w.伝票番号 ==
                                    context.T02_URHD.Where(v => v.会社名コード == iCompCd && v.削除日時 == null && v.売上区分 < 通常売上返品区分).Min(m => m.伝票番号));
                        }
                        #endregion
                        break;

                    case (int)CommonConstants.PagingOption.Paging_Code:
                        // 一致データ取得
                        if (isConv)
                        {
                            hdList = hdList.Where(w => w.伝票番号 == iSlipNumber);

                            if (hdList.Count() == 0)
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
                            hdList =
                                hdList.Where(w => w.伝票番号 ==
                                    context.T02_URHD.Where(v => v.会社名コード == iCompCd && v.伝票番号 > iSlipNumber && v.削除日時 == null && v.売上区分 < 通常売上返品区分)
                                        .Min(m => m.伝票番号));

                            if (hdList.Count() == 0)
                            {
                                // この場合現在指定番号が最大明細番号なので自分を再取得する
                                return getSlipNumberForPaging(compCd, slipNum, (int)CommonConstants.PagingOption.Paging_Code);
                            }

                        }
                        else
                        {
                            // 明細番号指定なし
                            hdList =
                                hdList.Where(w => w.伝票番号 ==
                                    context.T02_URHD.Where(v => v.会社名コード == iCompCd && v.削除日時 == null && v.売上区分 < 通常売上返品区分).Max(m => m.伝票番号));
                        }
                        #endregion
                        break;

                    default:
                        break;

                }

                var targetData = hdList.FirstOrDefault();

                if (targetData != null)
                    return hdList.FirstOrDefault().伝票番号.ToString();

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
                if (int.TryParse(companyCode, out code) && int.TryParse(slipNumber, out num))
                {
                    var result =
                        context.T02_URHD.Where(w => w.削除日時 == null && w.会社名コード == code && w.伝票番号 == num)
                            .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                                x => new { コード = x.得意先コード, 枝番 = x.得意先枝番 },
                                y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                                (p, q) => new { p, q })
                            .SelectMany(g => g.q.DefaultIfEmpty(), (a, b) => new { a, b })
                            .Select(z => new
                            {
                                伝票番号 = z.a.p.伝票番号,
                                会社名コード = z.a.p.会社名コード,
                                伝票要否 = z.a.p.伝票要否,
                                売上日 = z.a.p.売上日,
                                売上区分 = z.a.p.売上区分,
                                得意先コード = z.a.p.得意先コード,
                                得意先枝番 = z.a.p.得意先枝番,
                                在庫倉庫コード = z.a.p.在庫倉庫コード,
                                納品伝票番号 = z.a.p.納品伝票番号,
                                出荷日 = z.a.p.出荷日,
                                受注番号 = z.a.p.受注番号,
                                出荷元コード = z.a.p.出荷元コード,
                                出荷元枝番 = z.a.p.出荷元枝番,
                                出荷元名 = z.a.p.出荷元名,
                                出荷先コード = z.a.p.出荷先コード,
                                出荷先枝番 = z.a.p.出荷先枝番,
                                出荷先名 = z.a.p.出荷先名,
                                仕入先コード = z.a.p.仕入先コード,
                                仕入先枝番 = z.a.p.仕入先枝番,
                                備考 = z.a.p.備考,
                                消費税 = z.a.p.消費税,
                                Ｓ支払消費税区分 = z.b.Ｓ支払消費税区分,
                                Ｓ税区分ID = z.b.Ｓ支払区分
                            })
                            .ToList()
                            .Select(x => new T02_URHD_Search_Extension
                            {
                                伝票番号 = x.伝票番号.ToString(),
                                会社名コード = x.会社名コード.ToString(),
                                伝票要否 = x.伝票要否,
                                売上日 = x.売上日,
                                売上区分 = x.売上区分,
                                得意先コード = x.得意先コード.ToString(),
                                得意先枝番 = x.得意先枝番.ToString(),
                                在庫倉庫コード = x.在庫倉庫コード.ToString(),
                                納品伝票番号 = x.納品伝票番号.ToString(),
                                出荷日 = x.出荷日,
                                受注番号 = x.受注番号.ToString(),
                                出荷元コード = x.出荷元コード.ToString(),
                                出荷元枝番 = x.出荷元枝番.ToString(),
                                出荷元名 = (x.出荷元名 ?? string.Empty).ToString(),
                                出荷先コード = x.出荷先コード.ToString(),
                                出荷先枝番 = x.出荷先枝番.ToString(),
                                出荷先名 = (x.出荷先名 ?? string.Empty).ToString(),
                                仕入先コード = x.仕入先コード.ToString(),
                                仕入先枝番 = x.仕入先枝番.ToString(),
                                備考 = x.備考,
                                消費税 = x.消費税,
                                Ｓ支払消費税区分 = x.Ｓ支払消費税区分,
                                Ｓ税区分ID = x.Ｓ税区分ID
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
                    // 伝票番号から売上ヘッダ情報を取得
                    var urhd =
                        context.T02_URHD
                            .Where(w => w.削除日時 == null && w.会社名コード == code && w.伝票番号 == num)
                            .FirstOrDefault();

                    if (urhd == null)
                        return new List<T02_URDTL_Search_Extension>();

                    var result =
                        context.T02_URDTL.Where(w => w.削除日時 == null && w.伝票番号 == num)
                            .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null), x => x.品番コード, y => y.品番コード, (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(), (a, b) => new { URDTL = a.x, HIN = b })
                            .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null), x => x.HIN.自社色, y => y.色コード, (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(), (c, d) => new { c.x.URDTL, c.x.HIN, IRO = d })
                            .GroupJoin(context.T03_SRDTL_HAN.Where(w => w.削除日時 == null),
                                x => new { x.URDTL.伝票番号, x.URDTL.行番号 },
                                y => new { y.伝票番号, y.行番号 },
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(), (e, f) => new { e.x.URDTL, e.x.HIN, e.x.IRO, SRDTL_HAN = f })
                            .GroupJoin(context.M10_TOKHIN.Where(w => w.削除日時 == null),
                                x => new { 品番 = x.URDTL.品番コード, 得意先 = urhd.得意先コード, 枝番 = urhd.得意先枝番 },
                                y => new { 品番 = y.品番コード, 得意先 = y.取引先コード, 枝番 = y.枝番 },
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(), (g, h) => new { g.x.URDTL, g.x.HIN, g.x.IRO, g.x.SRDTL_HAN, TOKHIN = h })
                            .ToList()
                            .Select(x => new T02_URDTL_Search_Extension
                            {
                                伝票番号 = x.URDTL.伝票番号.ToString(),
                                行番号 = x.URDTL.行番号,
                                品番コード = x.URDTL.品番コード.ToString(),
                                自社品番 = x.HIN.自社品番,
                                得意先品番コード = x.TOKHIN == null ? "" : x.TOKHIN.得意先品番コード,
                                自社品名 = x.HIN.自社品名,
                                自社色 = x.HIN.自社色,
                                自社色名 = x.IRO != null ? x.IRO.色名称 : string.Empty,
                                賞味期限 = x.URDTL.賞味期限,
                                数量 = x.URDTL.数量,
                                単位 = x.URDTL.単位,
                                単価 = x.URDTL.単価,
                                金額 = x.URDTL.金額 ?? 0,
                                摘要 = x.URDTL.摘要,
                                マルセン仕入 = x.SRDTL_HAN != null,
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
                            case (int)CommonConstants.売上区分.通常売上:
                            case (int)CommonConstants.売上区分.委託売上:
                            case (int)CommonConstants.売上区分.預け売上:
                                // 通常更新(売上～在庫引去り)
                                setUsualSalesProc(ds);
                                break;

                            case (int)CommonConstants.売上区分.販社売上:
                                setSalesCompanyProc(urhd, dtUrDtl);
                                break;

                            case (int)CommonConstants.売上区分.メーカー直送:
                                setMakerDirectProc(context, ds, userId);
                                break;

                            case (int)CommonConstants.売上区分.メーカー販社商流直送:
                                setCommerceDirectDeliveryProc(context, ds, userId);
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

                            var urhd =
                               context.T02_URHD
                                   .Where(x => x.削除日時 == null &&
                                       x.伝票番号 == number)
                                        .FirstOrDefault();

                            var urdtl =
                               context.T02_URDTL
                                   .Where(x => x.削除日時 == null &&
                                       x.伝票番号 == number)
                                        .ToList();

                            // 伝票データが存在しない場合は処理しない。
                            if (urdtl == null || urdtl.Count == 0) return;

                            var mrsnsr =
                                context.T03_SRDTL_HAN.Where(c => c.削除日時 == null).ToList();

                            #region メーカー仕入削除

                            // ①仕入ヘッダ論理削除
                            T03Service.T03_SRHD_Delete(number);

                            // ②仕入明細論理削除
                            List<T03_SRDTL> SrDtlData = T03Service.T03_SRDTL_Delete(number);

                            if (SrDtlData.Count > 0)
                            {

                                foreach (T02_URDTL row in urdtl)
                                {
                                    // ③在庫更新 ※倉庫コードはメーカー仕入れなので自社固定
                                    S03_STOK stok = new S03_STOK();
                                    stok.倉庫コード = getStockpileFromJis();
                                    stok.品番コード = row.品番コード;
                                    stok.賞味期限 = AppCommon.DateTimeToDate(row.賞味期限, DateTime.MaxValue);
                                    stok.在庫数 = ((decimal)row.数量) * -1;

                                    S03Service.S03_STOK_Update(stok);

                                    // ④入出庫履歴作成

                                    Dictionary<string, string> hstDic = new Dictionary<string, string>()
                                    {
                                        { S04.COLUMNS_NAME_入出庫日, urhd.売上日.ToString("yyyy/MM/dd") },
                                        { S04.COLUMNS_NAME_倉庫コード, stok.倉庫コード.ToString() },
                                        { S04.COLUMNS_NAME_伝票番号, urhd.伝票番号.ToString() },
                                        { S04.COLUMNS_NAME_品番コード, stok.品番コード.ToString() }
                                    };

                                    // 履歴削除を実行
                                    S04Service.DeleteProductHistory(hstDic);
                                }
                            }


                            #endregion

                            #region 販社移動削除

                            // ①仕入ヘッダ論理削除
                            T03_SRHD_HAN hanSrHdData = T03Service.T03_SRHD_HAN_Delete(number);

                            // ②仕入明細論理削除
                            List<T03_SRDTL_HAN> hanSrDtlList = T03Service.T03_SRDTL_HAN_Delete(number);

                            // ③売上ヘッダ論理削除
                            T02_URHD_HAN hanUrHdData = T02Service.T02_URHD_HAN_Delete(number);

                            // ④売上明細論理削除
                            List<T02_URDTL_HAN> hanUrDtlList = T02Service.T02_URDTL_HAN_Delete(number);

                            // ⑤移動ヘッダ論理削除
                            T05_IDOHD idoHdData = T05Service.T05_IDOHD_Delete(number);

                            // ⑥移動明細論理削除
                            List<T05_IDODTL> idoDtlList = T05Service.T05_IDODTL_Delete(number);

                            if (idoDtlList.Count > 0)
                            {
                                // 移動時の在庫調整
                                // マルセン(自社)からの出庫処理
                                foreach (T02_URDTL row in urdtl)
                                {

                                    //マルセン仕入存在チェック
                                    bool マルセン仕入flg = mrsnsr.Where(c => c.伝票番号 == row.伝票番号 && c.行番号 == row.行番号).Any();
                                    if (マルセン仕入flg)
                                    {
                                        // ⇒在庫倉庫から出庫処理
                                        // ⑦在庫更新
                                        S03_STOK srstok = new S03_STOK();
                                        srstok.倉庫コード = urhd.在庫倉庫コード;
                                        srstok.品番コード = row.品番コード;
                                        srstok.賞味期限 = AppCommon.DateTimeToDate(row.賞味期限, DateTime.MaxValue);
                                        srstok.在庫数 = (decimal)row.数量;

                                        S03Service.S03_STOK_Update(srstok);

                                        // ⑧履歴削除
                                        Dictionary<string, string> hstDic = new Dictionary<string, string>()
                                        {
                                            { S04.COLUMNS_NAME_入出庫日, urhd.売上日.ToString("yyyy/MM/dd") },
                                            { S04.COLUMNS_NAME_倉庫コード,  srstok.倉庫コード.ToString() },
                                            { S04.COLUMNS_NAME_伝票番号, urhd.伝票番号.ToString() },
                                            { S04.COLUMNS_NAME_品番コード, srstok.品番コード.ToString() }
                                        };

                                        // 履歴削除を実行
                                        S04Service.DeleteProductHistory(hstDic);

                                        // ⇒販社への入庫処理
                                        // ⑨在庫更新
                                        S03_STOK hanstok = new S03_STOK();
                                        hanstok.倉庫コード = getStockpileFromJis(urhd.会社名コード);
                                        hanstok.品番コード = row.品番コード;
                                        hanstok.賞味期限 = AppCommon.DateTimeToDate(row.賞味期限, DateTime.MaxValue);
                                        hanstok.在庫数 = (decimal)row.数量 * -1;

                                        S03Service.S03_STOK_Update(hanstok);

                                        // ⑩履歴削除
                                        Dictionary<string, string> hanHstDic = new Dictionary<string, string>()
                                        {
                                            { S04.COLUMNS_NAME_入出庫日, urhd.売上日.ToString("yyyy/MM/dd") },
                                            { S04.COLUMNS_NAME_倉庫コード, hanstok.倉庫コード.ToString() },
                                            { S04.COLUMNS_NAME_伝票番号, urhd.伝票番号.ToString() },
                                            { S04.COLUMNS_NAME_品番コード,  hanstok.品番コード.ToString() }
                                        };

                                        // 履歴削除を実行
                                        S04Service.DeleteProductHistory(hanHstDic);
                                    }
                                }
                            }

                            #endregion

                            #region 売上削除
                            // ①売上ヘッダ論理削除
                            T02_URHD urHdData = T02Service.T02_URHD_Delete(number);

                            // ②売上明細論理削除
                            List<T02_URDTL> urDtlList = T02Service.T02_URDTL_Delete(number);

                            // 区分ごとで倉庫を変更する
                            int wk倉庫コード;
                            switch (urhd.売上区分)
                            {
                                case (int)CommonConstants.売上区分.メーカー直送:
                                case (int)CommonConstants.売上区分.メーカー直送返品:
                                    wk倉庫コード = getStockpileFromJis();
                                    break;
                                case (int)CommonConstants.売上区分.販社売上:
                                case (int)CommonConstants.売上区分.販社売上返品:
                                case (int)CommonConstants.売上区分.メーカー販社商流直送:
                                case (int)CommonConstants.売上区分.メーカー販社商流直送返品:
                                    wk倉庫コード = getStockpileFromJis(urhd.会社名コード);
                                    break;
                                default:
                                    wk倉庫コード = urhd.在庫倉庫コード;
                                    break;
                            }

                            foreach (T02_URDTL row in urdtl)
                            {
                                // ③在庫更新
                                S03_STOK stok = new S03_STOK();
                                stok.倉庫コード = wk倉庫コード;
                                stok.品番コード = row.品番コード;
                                stok.賞味期限 = AppCommon.DateTimeToDate(row.賞味期限, DateTime.MaxValue);
                                stok.在庫数 = (decimal)row.数量;

                                S03Service.S03_STOK_Update(stok);

                                // ④入出庫履歴作成
                                Dictionary<string, string> hstDic = new Dictionary<string, string>()
                                {
                                    { S04.COLUMNS_NAME_入出庫日, urhd.売上日.ToString("yyyy/MM/dd") },
                                    { S04.COLUMNS_NAME_倉庫コード, stok.倉庫コード.ToString() },
                                    { S04.COLUMNS_NAME_伝票番号, urhd.伝票番号.ToString() },
                                    { S04.COLUMNS_NAME_品番コード,  stok.品番コード.ToString() }
                                };

                                // 履歴削除を実行
                                S04Service.DeleteProductHistory(hstDic);
                            }
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

        #region 通常、委託、預け売上更新処理
        /// <summary>
        /// 通常、委託、預け売上時の更新処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ds"></param>
        /// <param name="userId"></param>
        private void setUsualSalesProc(DataSet ds)
        {
            DataRow hdRow = ds.Tables[T02_HEADER_TABLE_NAME].Rows[0];
            T02_URHD hdData = convertDataRowToT02_URHD_Entity(hdRow);
            DataTable dtlTbl = ds.Tables[T02_DETAIL_TABLE_NAME];

            // 1.売上ヘッダの更新
            T02Service.T02_URHD_Update(hdData);

            // 2.売上詳細の更新
            setT02_URDTL_Update(hdData, dtlTbl, false);

            // 3.在庫の更新
            setS03_STOK_Update(hdData, dtlTbl);

        }
        #endregion

        #region 販社売上更新処理
        /// <summary>
        /// 販社売上時の更新処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ds"></param>
        /// <param name="userId"></param>
        private void setSalesCompanyProc(T02_URHD urhd, DataTable dtlTbl)
        {

            // 自社倉庫と在庫倉庫が異なる場合場合のみ移動処理を行う
            bool difSouk = urhd.在庫倉庫コード != T05Service.getShippingDestination(urhd);

            // 前回在庫倉庫を取得
            int BefZaiSouk = getT02ZaikoSouk(urhd.会社名コード, urhd.伝票番号);


            // 1.販社仕入ヘッダの更新
            setT03_SRHD_HAN_Update(urhd, dtlTbl, CommonConstants.仕入区分.通常);

            // 2.販社仕入明細の更新
            setT03_SRDTL_HAN_Update(urhd, dtlTbl);

            // 3.販社売上ヘッダの更新
            setT02_URHD_HAN_Update(urhd, dtlTbl);

            // 4.販社売上明細の更新
            setT02_URDTL_HAN_Update(urhd, dtlTbl);

            #region 倉庫移動処理

            // 5.移動ヘッダの更新
            setT05_IDOHD_Update(urhd, difSouk);

            // 6.移動明細の更新
            setT05_IDODTL_Update(urhd, dtlTbl, difSouk);

            // 7.販社在庫(販社入庫)の更新(入出庫履歴も同時に作成)
            setS03_HAN_STOK_Update(urhd, dtlTbl, difSouk, BefZaiSouk);

            #endregion

            // 8.売上ヘッダの更新 
            T02Service.T02_URHD_Update(urhd);

            // 9.売上詳細の更新
            setT02_URDTL_Update(urhd, dtlTbl, false);

            // 10.在庫(引去)の更新
            setS03_STOK_Update(urhd, dtlTbl);

        }
        #endregion

        #region メーカー直送更新処理
        /// <summary>
        /// メーカー直送時の更新処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ds"></param>
        /// <param name="userId"></param>
        private void setMakerDirectProc(TRAC3Entities context, DataSet ds, int userId)
        {
            /*
            ⇒①仕入データ作成(マルセン→メーカーの場合)
            ⇒②売上データ作成(マルセン→得意先の場合)
            */
            DataRow hdRow = ds.Tables[T02_HEADER_TABLE_NAME].Rows[0];
            T02_URHD hdData = convertDataRowToT02_URHD_Entity(hdRow);
            DataTable dtlTbl = ds.Tables[T02_DETAIL_TABLE_NAME];

            // メーカーは在庫倉庫をマルセンに固定
            hdData.在庫倉庫コード = getStockpileFromJis();

            // 1.仕入ヘッダの更新
            setT03_SRHD_Update(hdData, CommonConstants.仕入区分.通常);

            // 2.仕入詳細の更新
            setT03_SRDTL_Update(hdData, dtlTbl, false);

            // 3.メーカー仕入在庫の更新
            setS03_MA_STOK_Update(hdData, dtlTbl);

            // 4.売上ヘッダの更新
            T02Service.T02_URHD_Update(hdData);

            // 5.売上明細の更新
            setT02_URDTL_Update(hdData, dtlTbl, false);

            // 6.在庫(引去)の更新
            setS03_STOK_Update(hdData, dtlTbl);

        }
        #endregion

        #region メーカー販社商流直送更新処理
        /// <summary>
        /// メーカー販社商流直送時の更新処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ds"></param>
        /// <param name="userId"></param>
        private void setCommerceDirectDeliveryProc(TRAC3Entities context, DataSet ds, int userId)
        {
            DataRow hdRow = ds.Tables[T02_HEADER_TABLE_NAME].Rows[0];
            T02_URHD urhd = convertDataRowToT02_URHD_Entity(hdRow);
            DataTable dtlDt = ds.Tables[T02_DETAIL_TABLE_NAME];

            // メーカー固定値設定
            urhd.在庫倉庫コード = getStockpileFromJis();

            foreach (DataRow row in dtlDt.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;

                row["マルセン仕入"] = true;
            }

            #region 1.仕入ヘッダの更新(メーカー⇒マルセン)
            T03_SRHD srhd = new T03_SRHD();
            srhd.伝票番号 = urhd.伝票番号;
            srhd.会社名コード = getStockpileFromJis();
            srhd.仕入日 = urhd.売上日;
            srhd.入力区分 = (int)CommonConstants.入力区分.売上入力;
            srhd.仕入区分 = (int)CommonConstants.仕入区分.通常;
            srhd.仕入先コード = urhd.仕入先コード ?? -1;
            srhd.仕入先枝番 = urhd.仕入先枝番 ?? -1;
            srhd.入荷先コード = getM70_JISFromM22_SOUK(urhd.在庫倉庫コード).自社コード;
            srhd.発注番号 = urhd.受注番号;
            srhd.備考 = urhd.備考;
            srhd.元伝票番号 = urhd.元伝票番号;
            srhd.消費税 = urhd.消費税;

            T03Service.T03_SRHD_Update(srhd);

            #endregion

            // 2.仕入明細の更新
            setT03_SRDTL_Update(urhd, dtlDt, false);

            // 3.メーカー仕入の在庫更新
            setS03_MA_STOK_Update(urhd, dtlDt);

            // 自社倉庫と在庫倉庫が異なる場合場合のみ移動処理を行う(メーカ販社は必ず異なる)
            bool difShip = urhd.在庫倉庫コード != T05Service.getShippingDestination(urhd);

            // 4.販社仕入ヘッダの更新(マルセン⇒販社)
            setT03_SRHD_HAN_Update(urhd, dtlDt, CommonConstants.仕入区分.通常);

            // 5.販社仕入明細の更新
            setT03_SRDTL_HAN_Update(urhd, dtlDt);

            // 6.販社売上ヘッダの更新
            setT02_URHD_HAN_Update(urhd, dtlDt);

            // 7.販社売上明細の更新
            setT02_URDTL_HAN_Update(urhd, dtlDt);


            // 8.移動ヘッダの更新(マルセン⇒販社)
            setT05_IDOHD_Update(urhd, difShip);

            // 9.移動明細の更新
            setT05_IDODTL_Update(urhd, dtlDt, difShip);

            #region 自社⇒販社への入出庫処理(在庫・入出庫履歴)
            foreach (DataRow row in dtlDt.Rows)
            {
                T02_URDTL urdtl = convertDataRowToT02_URDTL_Entity(row);

                if (urdtl.品番コード <= 0)
                    continue;

                #region 在庫調整数計算
                decimal stockQty = 0;
                if (row.RowState == DataRowState.Deleted)
                {
                    // 数量分在庫数を加算
                    if (row.HasVersion(DataRowVersion.Original))
                    {
                        stockQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]);

                    }
                    else
                        stockQty = urdtl.数量;

                }
                else if (row.RowState == DataRowState.Added)
                {
                    // 数量分在庫数を減算
                    stockQty = urdtl.数量 * -1;

                }
                else if (row.RowState == DataRowState.Modified)
                {
                    // オリジナル(変更前数量)と比較して差分数量を加減算
                    if (row.HasVersion(DataRowVersion.Original))
                    {
                        decimal orgQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]);
                        stockQty = (orgQty - urdtl.数量);

                    }

                }
                else
                {
                    // 対象なし(DataRowState.Unchanged)
                    continue;
                }
                #endregion

                // マルセン仕入 = ON
                // ⇒マルセン(自社)からの出庫処理
                S03_STOK outStok = new S03_STOK();

                int outSouk = getStockpileFromJis(); // マルセン(自社)を強制で設定
                outStok.倉庫コード = outSouk;
                outStok.品番コード = urdtl.品番コード;
                outStok.賞味期限 = AppCommon.DateTimeToDate(urdtl.賞味期限, DateTime.MaxValue);
                outStok.在庫数 = stockQty;

                S03Service.S03_STOK_Update(outStok);

                #region 出庫履歴の作成
                S04_HISTORY outHistory = new S04_HISTORY();

                outHistory.入出庫日 = urhd.売上日;
                outHistory.入出庫時刻 = com.GetDbDateTime().TimeOfDay;
                outHistory.倉庫コード = outSouk;
                outHistory.入出庫区分 = (int)CommonConstants.入出庫区分.ID02_出庫;
                outHistory.品番コード = urdtl.品番コード;
                outHistory.賞味期限 = urdtl.賞味期限;
                outHistory.数量 = decimal.ToInt32(urdtl.数量);
                outHistory.伝票番号 = urhd.伝票番号;

                Dictionary<string, string> hstDic = new Dictionary<string, string>()
                    {
                        { S04.COLUMNS_NAME_入出庫日, outHistory.入出庫日.ToString("yyyy/MM/dd") },
                        { S04.COLUMNS_NAME_倉庫コード, outHistory.倉庫コード.ToString() },
                        { S04.COLUMNS_NAME_伝票番号, outHistory.伝票番号.ToString() },
                        { S04.COLUMNS_NAME_品番コード, outHistory.品番コード.ToString() },
                        { S04.COLUMNS_NAME_入出庫区分, outHistory.入出庫区分.ToString() }
                    };

                if (row.RowState == DataRowState.Added)
                {
                    // 売上作成の為、履歴作成
                    S04Service.CreateProductHistory(outHistory);
                }
                else if (row.RowState == DataRowState.Deleted)
                {
                    S04Service.DeleteProductHistory(hstDic);
                }
                else if (row.RowState == DataRowState.Modified)
                {
                    // 売上更新の為、履歴更新
                    S04Service.UpdateProductHistory(outHistory, hstDic);
                }
                else
                {
                    // 対象なし(DataRowState.Unchanged)
                    continue;
                }
                #endregion

                // ⇒販社への入庫処理
                S03_STOK inStok = new S03_STOK();

                int inSouk = getStockpileFromJis(urhd.会社名コード);
                inStok.倉庫コード = inSouk;
                inStok.品番コード = urdtl.品番コード;
                inStok.賞味期限 = AppCommon.DateTimeToDate(urdtl.賞味期限, DateTime.MaxValue);
                inStok.在庫数 = stockQty * -1;

                S03Service.S03_STOK_Update(inStok);

                // 入庫履歴の作成
                #region 入庫履歴の作成
                S04_HISTORY inHistory = new S04_HISTORY();

                inHistory.入出庫日 = urhd.売上日;
                inHistory.入出庫時刻 = com.GetDbDateTime().TimeOfDay;
                inHistory.倉庫コード = inSouk;
                inHistory.入出庫区分 = (int)CommonConstants.入出庫区分.ID01_入庫;
                inHistory.品番コード = urdtl.品番コード;
                inHistory.賞味期限 = urdtl.賞味期限;
                inHistory.数量 = decimal.ToInt32(urdtl.数量);
                inHistory.伝票番号 = urhd.伝票番号;

                hstDic = new Dictionary<string, string>()
                    {
                        { S04.COLUMNS_NAME_入出庫日, inHistory.入出庫日.ToString("yyyy/MM/dd") },
                        { S04.COLUMNS_NAME_倉庫コード, inHistory.倉庫コード.ToString() },
                        { S04.COLUMNS_NAME_伝票番号, inHistory.伝票番号.ToString() },
                        { S04.COLUMNS_NAME_品番コード, inHistory.品番コード.ToString() },
                        { S04.COLUMNS_NAME_入出庫区分, inHistory.入出庫区分.ToString() }
                    };

                if (row.RowState == DataRowState.Added)
                {
                    // 売上作成の為、履歴作成
                    S04Service.CreateProductHistory(inHistory);
                }
                else if (row.RowState == DataRowState.Deleted)
                {
                    S04Service.DeleteProductHistory(hstDic);
                }
                else if (row.RowState == DataRowState.Modified)
                {
                    // 売上更新の為、履歴更新
                    S04Service.UpdateProductHistory(inHistory, hstDic);
                }
                else
                {
                    // 対象なし(DataRowState.Unchanged)
                    continue;
                }
                #endregion

            }
            #endregion

            // 10.売上ヘッダの更新
            T02Service.T02_URHD_Update(urhd);

            // 11.売上明細の更新
            setT02_URDTL_Update(urhd, dtlDt, false);

            // 12.在庫(引去)の更新
            setS03_STOK_Update(urhd, dtlDt);

        }
        #endregion

        #endregion


        #region<< 売上明細登録更新処理 >>
        /// <summary>
        /// 売上明細情報の更新をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dt">明細テーブル</param>
        /// <param name="userId">ログインユーザID</param>
        /// <param name="isNonCheckItemWithout">マルセン仕入チェックが無いものを除外するか</param>
        /// <returns></returns>
        protected void setT02_URDTL_Update(T02_URHD urhd, DataTable dt, bool isNonCheckItemWithout)
        {
            // 対象伝票の明細を物理削除(Del-Ins)
            T02Service.T02_URDTL_DeleteRecords(urhd.伝票番号);

            // 明細追加
            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;

                T02_URDTL dtlData = convertDataRowToT02_URDTL_Entity(row);

                if (dtlData.品番コード <= 0)
                    continue;

                bool bval,
                    仕入チェック = bool.TryParse(row["マルセン仕入"].ToString(), out bval) ? bval : false;

                // チェック判定アリかつチェックボックス=オフの場合は登録処理をスキップする
                if (isNonCheckItemWithout && 仕入チェック == false)
                    continue;

                // 売上明細データを作成して登録更新
                T02_URDTL urdtl = new T02_URDTL();
                urdtl.伝票番号 = urhd.伝票番号;
                urdtl.行番号 = dtlData.行番号;
                urdtl.品番コード = dtlData.品番コード;
                urdtl.賞味期限 = dtlData.賞味期限;
                urdtl.数量 = dtlData.数量;
                urdtl.単位 = dtlData.単位;
                urdtl.単価 = dtlData.単価;
                urdtl.金額 = dtlData.金額;
                urdtl.摘要 = dtlData.摘要;

                T02Service.T02_URDTL_Update(urdtl);

            }

        }
        #endregion

        #region << 在庫情報更新処理 >>
        /// <summary>
        /// 在庫情報の更新をおこなう
        /// </summary>
        /// <param name="urhd">売上ヘッダデータ</param>
        /// <param name="dtlTbl">売上明細データテーブル</param>
        /// </param>
        private void setS03_STOK_Update(T02_URHD urhd, DataTable dtlTbl)
        {
            foreach (DataRow row in dtlTbl.Rows)
            {
                T02_URDTL urdtl = convertDataRowToT02_URDTL_Entity(row);

                if (urdtl.品番コード <= 0)
                    continue;

                decimal stockQty = 0;
                #region 通常売上
                if (row.RowState == DataRowState.Deleted)
                {
                    // 数量分在庫数を加算
                    if (row.HasVersion(DataRowVersion.Original))
                    {
                        stockQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]);

                    }
                    else
                        stockQty = urdtl.数量;

                }
                else if (row.RowState == DataRowState.Added)
                {
                    // 数量分在庫数を減算
                    stockQty = urdtl.数量 * -1;

                }
                else if (row.RowState == DataRowState.Modified)
                {
                    // オリジナル(変更前数量)と比較して差分数量を加減算
                    if (row.HasVersion(DataRowVersion.Original))
                    {
                        decimal orgQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]);
                        stockQty = (orgQty - urdtl.数量);

                    }

                }
                else
                {
                    // 対象なし(DataRowState.Unchanged)
                    continue;
                }
                #endregion

                // 出荷元の会社から(得意先への)出庫処理
                S03_STOK stok = new S03_STOK();

                //stok.倉庫コード = getStockpileFromJis(urhd.会社名コード);
                switch (urhd.売上区分)
                {
                    case (int)CommonConstants.売上区分.メーカー直送:
                    case (int)CommonConstants.売上区分.メーカー直送返品:
                        stok.倉庫コード = getStockpileFromJis();
                        break;
                    case (int)CommonConstants.売上区分.販社売上:
                    case (int)CommonConstants.売上区分.販社売上返品:
                    case (int)CommonConstants.売上区分.メーカー販社商流直送:
                    case (int)CommonConstants.売上区分.メーカー販社商流直送返品:
                        stok.倉庫コード = getStockpileFromJis(urhd.会社名コード);
                        break;
                    default:
                        stok.倉庫コード = urhd.在庫倉庫コード;
                        break;
                }

                stok.品番コード = urdtl.品番コード;
                stok.賞味期限 = AppCommon.DateTimeToDate(urdtl.賞味期限, DateTime.MaxValue);
                stok.在庫数 = stockQty;

                S03Service.S03_STOK_Update(stok);

                #region 出庫履歴の作成
                S04_HISTORY history = new S04_HISTORY();

                history.入出庫日 = urhd.売上日;
                history.入出庫時刻 = com.GetDbDateTime().TimeOfDay;
                history.倉庫コード = stok.倉庫コード;
                history.入出庫区分 = (int)CommonConstants.入出庫区分.ID02_出庫;
                history.品番コード = urdtl.品番コード;
                history.賞味期限 = urdtl.賞味期限;
                history.数量 = decimal.ToInt32(urdtl.数量);
                history.伝票番号 = urhd.伝票番号;

                Dictionary<string, string> hstDic = new Dictionary<string, string>()
                    {
                        { S04.COLUMNS_NAME_入出庫日, history.入出庫日.ToString("yyyy/MM/dd") },
                        { S04.COLUMNS_NAME_倉庫コード, history.倉庫コード.ToString() },
                        { S04.COLUMNS_NAME_伝票番号, history.伝票番号.ToString() },
                        { S04.COLUMNS_NAME_品番コード, history.品番コード.ToString() },
                        { S04.COLUMNS_NAME_入出庫区分, history.入出庫区分.ToString() }
                    };

                if (row.RowState == DataRowState.Added)
                {
                    // 売上作成の為、履歴作成
                    S04Service.CreateProductHistory(history);
                }
                else if (row.RowState == DataRowState.Deleted)
                {
                    S04Service.DeleteProductHistory(hstDic);
                }
                else if (row.RowState == DataRowState.Modified)
                {
                    // 売上更新の為、履歴更新
                    S04Service.UpdateProductHistory(history, hstDic);
                }
                else
                {
                    // 対象なし(DataRowState.Unchanged)
                    continue;
                }

                #endregion

            }

        }
        #endregion

        #region << 販社在庫情報更新処理 >>
        /// <summary>
        /// 販社の移動在庫情報の更新をおこなう
        /// </summary>
        /// <param name="urhd">売上ヘッダデータ</param>
        /// <param name="dtlTbl">売上明細データテーブル</param>
        /// <param name="difSouk">自社倉庫と在庫倉庫が異なる場合</param>
        /// <param name="befZaiSouk">前回在庫倉庫</param>
        private void setS03_HAN_STOK_Update(T02_URHD urhd, DataTable dtlTbl, bool difSouk, int befZaiSouk)
        {
            // マルセン仕入のチェック状態で生成データが変わる
            // ⇒チェックオンは仕入先→販社への入出庫
            // ⇒チェックオフは販社からの出庫

            foreach (DataRow row in dtlTbl.Rows)
            {
                T02_URDTL urdtl = convertDataRowToT02_URDTL_Entity(row);

                if (urdtl.品番コード <= 0)
                    continue;

                bool bval, isCheck;
                decimal stockQty = 0;
                DataRowState 行状態 = row.RowState;

                bool befDifSouk = befZaiSouk != T05Service.getShippingDestination(urhd);
                bool 前回マルセン仕入Chk = (row.HasVersion(DataRowVersion.Original) && bool.TryParse(row["マルセン仕入", DataRowVersion.Original].ToString(), out bval)) ? bval : false;
                bool 前回移動済Flg = befDifSouk && 前回マルセン仕入Chk;

                #region 数量取得

                if (行状態 == DataRowState.Deleted)
                {
                    if (!前回移動済Flg) continue;

                    // 数量分在庫数を加算
                    if (row.HasVersion(DataRowVersion.Original))
                    {
                        stockQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]);
                    }
                    else
                        stockQty = urdtl.数量;

                }
                else if (行状態 == DataRowState.Added)
                {
                    isCheck = bool.TryParse(row["マルセン仕入"].ToString(), out bval) ? bval : false;

                    // 新規行時マルセン仕入 = OFFまたは移動倉庫が同じ場合、移動伝票作成しない
                    if (!isCheck || !difSouk) continue;

                    // 数量分在庫数を減算
                    stockQty = urdtl.数量 * -1;

                }
                else if (行状態 == DataRowState.Modified || row.RowState == DataRowState.Unchanged)
                {

                    isCheck = bool.TryParse(row["マルセン仕入"].ToString(), out bval) ? bval : false;

                    // マルセン仕入が選択されているかチェック
                    if (isCheck)
                    {
                        // 在庫倉庫が変更されたかチェック
                        if (befZaiSouk == urhd.在庫倉庫コード)
                        {
                            if (!difSouk) continue;

                            if (前回マルセン仕入Chk)
                            {
                                // オリジナル(変更前数量)と比較して差分数量を加減算
                                行状態 = DataRowState.Modified;
                                decimal orgQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]);
                                stockQty = (orgQty - urdtl.数量);
                            }
                            else
                            {
                                // 新規登録
                                stockQty = urdtl.数量 * -1;
                                行状態 = DataRowState.Added;
                            }
                        }
                        else
                        {
                            // 移動元倉庫と移動先倉庫が異なるかチェック
                            if (difSouk)
                            {
                                if (前回移動済Flg)
                                {
                                    // 前回の移動在庫を削除
                                    decimal orgQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]);
                                    setS04_HAN_HISTORY_ZAI_Create(urhd, urdtl, DataRowState.Deleted, orgQty, befZaiSouk);
                                }

                                // 新規登録
                                stockQty = urdtl.数量 * -1;
                                行状態 = DataRowState.Added;

                            }
                            else
                            {
                                if (!前回移動済Flg) continue;

                                // 在庫倉庫と自社倉庫が同じ場合、元に戻す
                                行状態 = DataRowState.Deleted;
                                stockQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]);

                            }
                        }
                    }
                    else
                    {
                        if (前回移動済Flg)
                        {
                            // 在庫倉庫と自社倉庫が同じ場合、元に戻す
                            行状態 = DataRowState.Deleted;
                            stockQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]);
                        }
                        else
                            continue;
                    }
                }

                #endregion

                int outSouk;
                if (行状態 == DataRowState.Deleted)
                {
                    // ⇒削除処理は前回登録時の在庫倉庫
                    outSouk = befZaiSouk;
                }
                else
                {
                    // ⇒在庫倉庫からの出庫処理
                    outSouk = urhd.在庫倉庫コード;
                }

                // 今回の移動在庫を適用
                setS04_HAN_HISTORY_ZAI_Create(urhd, urdtl, 行状態, stockQty, outSouk);
            }

        }
        #endregion

        #region << メーカー仕入在庫情報更新 >>
        /// <summary>
        /// メーカー仕入れの在庫情報の更新をおこなう自社への入庫処理(在庫・入出庫履歴)
        /// </summary>
        /// <param name="urhd"></param>
        /// <param name="dtlDt"></param>
        private void setS03_MA_STOK_Update(T02_URHD urhd, DataTable dtlDt)
        {

            foreach (DataRow row in dtlDt.Rows)
            {
                T02_URDTL urdtl = convertDataRowToT02_URDTL_Entity(row);

                if (urdtl.品番コード <= 0)
                    continue;

                decimal stockQty = 0;
                #region 通常売上
                if (row.RowState == DataRowState.Deleted)
                {
                    // 数量分在庫数を加算
                    if (row.HasVersion(DataRowVersion.Original))
                    {
                        stockQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]) * -1;

                    }
                    else
                        stockQty = urdtl.数量 * -1;

                }
                else if (row.RowState == DataRowState.Added)
                {
                    // 数量分在庫数を減算
                    stockQty = urdtl.数量;

                }
                else if (row.RowState == DataRowState.Modified)
                {
                    // オリジナル(変更前数量)と比較して差分数量を加減算
                    if (row.HasVersion(DataRowVersion.Original))
                    {
                        decimal orgQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]);
                        stockQty = (urdtl.数量 - orgQty);

                    }

                }
                else
                {
                    // 対象なし(DataRowState.Unchanged)
                    continue;
                }
                #endregion

                // ⇒マルセン(自社)への入庫処理
                S03_STOK outStok = new S03_STOK();

                int inSouk = getStockpileFromJis(); // マルセン(自社)を強制で設定
                outStok.倉庫コード = inSouk;
                outStok.品番コード = urdtl.品番コード;
                outStok.賞味期限 = AppCommon.DateTimeToDate(urdtl.賞味期限, DateTime.MaxValue);
                outStok.在庫数 = stockQty;

                S03Service.S03_STOK_Update(outStok);

                #region 出庫履歴の作成
                S04_HISTORY outHistory = new S04_HISTORY();

                outHistory.入出庫日 = urhd.売上日;
                outHistory.入出庫時刻 = com.GetDbDateTime().TimeOfDay;
                outHistory.倉庫コード = inSouk;
                outHistory.入出庫区分 = (int)CommonConstants.入出庫区分.ID01_入庫;
                outHistory.品番コード = urdtl.品番コード;
                outHistory.賞味期限 = urdtl.賞味期限;
                outHistory.数量 = decimal.ToInt32(urdtl.数量);
                outHistory.伝票番号 = urhd.伝票番号;

                Dictionary<string, string> hstDic = new Dictionary<string, string>()
                    {
                        { S04.COLUMNS_NAME_入出庫日, outHistory.入出庫日.ToString("yyyy/MM/dd") },
                        { S04.COLUMNS_NAME_倉庫コード, outHistory.倉庫コード.ToString() },
                        { S04.COLUMNS_NAME_伝票番号, outHistory.伝票番号.ToString() },
                        { S04.COLUMNS_NAME_品番コード, outHistory.品番コード.ToString() },
                        { S04.COLUMNS_NAME_入出庫区分, outHistory.入出庫区分.ToString() }
                    };

                if (row.RowState == DataRowState.Added)
                {
                    // 売上作成の為、履歴作成
                    S04Service.CreateProductHistory(outHistory);
                }
                else if (row.RowState == DataRowState.Deleted)
                {
                    S04Service.DeleteProductHistory(hstDic);
                }
                else if (row.RowState == DataRowState.Modified)
                {
                    // 売上更新の為、履歴更新
                    S04Service.UpdateProductHistory(outHistory, hstDic);
                }
                else
                {
                    // 対象なし(DataRowState.Unchanged)
                    continue;
                }
                #endregion

            }

        }
        #endregion

        #region << 入出庫履歴更新処理 >>
        /// <summary>
        /// 販社の入出庫在庫、履歴の登録・更新をおこなう
        /// </summary>
        /// <param name="urhd">仕入ヘッダデータ</param>
        /// <param name="urdtl">仕入明細データテーブル</param>
        /// <param name="行状態">登録・削除・編集</param>
        /// <param name="stockQty">移動数</param>
        /// <param name="outSouk">移動元倉庫</param>
        private void setS04_HAN_HISTORY_ZAI_Create(T02_URHD urhd, T02_URDTL urdtl, DataRowState 行状態, decimal stockQty, int outSouk)
        {
            S03_STOK outStok = new S03_STOK();
            outStok.倉庫コード = outSouk;
            outStok.品番コード = urdtl.品番コード;
            outStok.賞味期限 = AppCommon.DateTimeToDate(urdtl.賞味期限, DateTime.MaxValue);
            outStok.在庫数 = stockQty;

            S03Service.S03_STOK_Update(outStok);

            #region 出庫履歴の作成
            S04_HISTORY outHistory = new S04_HISTORY();

            outHistory.入出庫日 = urhd.売上日;
            outHistory.入出庫時刻 = com.GetDbDateTime().TimeOfDay;
            outHistory.倉庫コード = outSouk;
            outHistory.入出庫区分 = (int)CommonConstants.入出庫区分.ID02_出庫;
            outHistory.品番コード = urdtl.品番コード;
            outHistory.賞味期限 = urdtl.賞味期限;
            outHistory.数量 = decimal.ToInt32(urdtl.数量);
            outHistory.伝票番号 = urhd.伝票番号;

            Dictionary<string, string> hstDic = new Dictionary<string, string>()
                    {
                        { S04.COLUMNS_NAME_入出庫日, outHistory.入出庫日.ToString("yyyy/MM/dd") },
                        { S04.COLUMNS_NAME_倉庫コード, outHistory.倉庫コード.ToString() },
                        { S04.COLUMNS_NAME_伝票番号, outHistory.伝票番号.ToString() },
                        { S04.COLUMNS_NAME_品番コード, outHistory.品番コード.ToString() },
                        { S04.COLUMNS_NAME_入出庫区分, outHistory.入出庫区分.ToString() }
                    };

            if (行状態 == DataRowState.Added)
            {
                // 売上作成の為、履歴作成
                S04Service.CreateProductHistory(outHistory);
            }
            else if (行状態 == DataRowState.Deleted)
            {
                S04Service.DeleteProductHistory(hstDic);
            }
            else if (行状態 == DataRowState.Modified)
            {
                // 売上更新の為、履歴更新
                S04Service.UpdateProductHistory(outHistory, hstDic);
            }
            else
            {
                // 対象なし(DataRowState.Unchanged)
                return;
            }
            #endregion

            // ⇒販社への入庫処理
            S03_STOK inStok = new S03_STOK();

            int inSouk = getStockpileFromJis(urhd.会社名コード);
            //int inSouk = urhd.在庫倉庫コード;
            inStok.倉庫コード = inSouk;
            inStok.品番コード = urdtl.品番コード;
            inStok.賞味期限 = AppCommon.DateTimeToDate(urdtl.賞味期限, DateTime.MaxValue);
            inStok.在庫数 = stockQty * -1;

            S03Service.S03_STOK_Update(inStok);

            // 入庫履歴の作成
            #region 入庫履歴の作成
            S04_HISTORY inHistory = new S04_HISTORY();

            inHistory.入出庫日 = urhd.売上日;
            inHistory.入出庫時刻 = com.GetDbDateTime().TimeOfDay;
            inHistory.倉庫コード = inSouk;
            inHistory.入出庫区分 = (int)CommonConstants.入出庫区分.ID01_入庫;
            inHistory.品番コード = urdtl.品番コード;
            inHistory.賞味期限 = urdtl.賞味期限;
            inHistory.数量 = decimal.ToInt32(urdtl.数量);
            inHistory.伝票番号 = urhd.伝票番号;

            hstDic = new Dictionary<string, string>()
                    {
                        { S04.COLUMNS_NAME_入出庫日, inHistory.入出庫日.ToString("yyyy/MM/dd") },
                        { S04.COLUMNS_NAME_倉庫コード, inHistory.倉庫コード.ToString() },
                        { S04.COLUMNS_NAME_伝票番号, inHistory.伝票番号.ToString() },
                        { S04.COLUMNS_NAME_品番コード, inHistory.品番コード.ToString() },
                        { S04.COLUMNS_NAME_入出庫区分, inHistory.入出庫区分.ToString() }
                    };

            if (行状態 == DataRowState.Added)
            {
                // 売上作成の為、履歴作成
                S04Service.CreateProductHistory(inHistory);
            }
            else if (行状態 == DataRowState.Deleted)
            {
                S04Service.DeleteProductHistory(hstDic);
            }
            else if (行状態 == DataRowState.Modified)
            {
                // 売上更新の為、履歴更新
                S04Service.UpdateProductHistory(inHistory, hstDic);
            }
            else
            {
                // 対象なし(DataRowState.Unchanged)
                return;
            }
            #endregion

        }
        #endregion

        #region << 移動ヘッダ登録更新処理 >>
        /// <summary>
        /// 移動ヘッダ情報の更新処理をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="hdRow">売上ヘッダ行</param>
        /// <param name="userId">自社倉庫と在庫倉庫が異なる場合</param>
        private void setT05_IDOHD_Update(T02_URHD t02Data, bool difSouk)
        {
            if (difSouk)
            {
                T05_IDOHD idohd = new T05_IDOHD();

                idohd.伝票番号 = t02Data.伝票番号;
                idohd.会社名コード = t02Data.会社名コード;
                idohd.日付 = t02Data.売上日;
                idohd.移動区分 = CommonConstants.移動区分.売上移動.GetHashCode();
                idohd.出荷元倉庫コード = t02Data.在庫倉庫コード;
                idohd.出荷先倉庫コード = T05Service.getShippingDestination(t02Data);

                // ヘッダデータの登録を実行
                T05Service.T05_IDOHD_Update(idohd);

            }
        }
        #endregion

        #region << 移動明細登録更新処理 >>
        /// <summary>
        /// 移動明細情報の更新をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dt">明細テーブル</param>
        /// <param name="userId">ログインユーザID</param>
        /// <param name="isNonCheckItemWithout">自社倉庫と在庫倉庫が異なるか</param>
        /// <returns></returns>
        protected int setT05_IDODTL_Update(T02_URHD urhd, DataTable dtlTbl, bool difSouk)
        {
            T05Service.T05_IDODTL_DeleteRecords(urhd.伝票番号);

            // 明細追加
            foreach (DataRow row in dtlTbl.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;

                T02_URDTL dtlData = convertDataRowToT02_URDTL_Entity(row);
                bool bval,
                    仕入チェック = bool.TryParse(row["マルセン仕入"].ToString(), out bval) ? bval : false;

                if (dtlData.品番コード <= 0)
                    continue;

                // 出荷元と出荷先が異なるかつチェックボックス=オフの場合は登録処理をスキップする
                if (!difSouk || 仕入チェック == false)
                    continue;

                T05_IDODTL idodtl = new T05_IDODTL();

                idodtl.伝票番号 = urhd.伝票番号;
                idodtl.行番号 = dtlData.行番号;
                idodtl.品番コード = dtlData.品番コード;
                idodtl.賞味期限 = dtlData.賞味期限;
                idodtl.数量 = dtlData.数量;
                idodtl.摘要 = dtlData.摘要;

                // 売上明細登録処理実行
                T05Service.T05_IDODTL_Update(idodtl);

            }

            return 1;

        }
        #endregion

        #region << 販社仕入ヘッダ登録更新処理 >>
        /// <summary>
        /// 販社仕入ヘッダの登録・更新をおこなう
        /// </summary>
        /// <param name="t02Data"></param>
        /// <param name="dtlTbl"></param>
        /// <param name="supKbn"></param>
        public void setT03_SRHD_HAN_Update(T02_URHD t02Data, DataTable dtlTbl, CommonConstants.仕入区分 supKbn)
        {
            // 売上対象の自社情報・取引先情報を取得する
            var m70自社 = getFromJis();
            M70_JIS srJis = getM70_JISFromM22_SOUK(t02Data.在庫倉庫コード);
            M01_TOK tok = M01.M01_TOK_Single_GetData(srJis.取引先コード ?? 0, srJis.枝番 ?? 0);

            #region 消費税再計算集計
            int setTax = 0;
            // 全商品でない可能性があるのでチェックされている商品の消費税を再計算する
            foreach (DataRow dtlRow in dtlTbl.Rows)
            {
                T02_URDTL dtl = convertDataRowToT02_URDTL_Entity(dtlRow);

                // 商品未設定レコードは処理対象外とする
                if (dtl.品番コード <= 0)
                    continue;

                // 削除されたレコードは処理対象外とする
                if (DataRowState.Deleted == dtlRow.RowState)
                    continue;

                bool bval,
                   仕入チェック = bool.TryParse(dtlRow["マルセン仕入"].ToString(), out bval) ? bval : false;
                if (!仕入チェック)
                    continue;   // チェックされていない商品は読み飛ばし

                decimal tax = T05Service.getCalcSalesTax(t02Data.売上日, tok.Ｓ税区分ID, dtl.品番コード, dtl.数量);
                setTax += Decimal.ToInt32(tax);

            }
            #endregion

            T03_SRHD_HAN srhdhan = new T03_SRHD_HAN();

            srhdhan.伝票番号 = t02Data.伝票番号;
            srhdhan.会社名コード = t02Data.会社名コード;
            srhdhan.仕入日 = t02Data.売上日;
            srhdhan.仕入区分 = supKbn.GetHashCode();
            srhdhan.仕入先コード = m70自社.自社コード;
            srhdhan.入荷先コード = t02Data.会社名コード;
            srhdhan.発注番号 = t02Data.受注番号;
            srhdhan.備考 = t02Data.備考;
            srhdhan.消費税 = setTax;

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

                // (卸値)金額算出
                decimal cost = getWholesalePrice( dtlData.品番コード);
                int amount = Decimal.ToInt32(decimal.Parse(((decimal)(cost * dtlData.数量)).ToString()));

                T03_SRDTL_HAN srdtlhan = new T03_SRDTL_HAN();
                srdtlhan.伝票番号 = urhd.伝票番号;
                srdtlhan.行番号 = dtlData.行番号;
                srdtlhan.品番コード = dtlData.品番コード;
                srdtlhan.賞味期限 = dtlData.賞味期限;
                srdtlhan.数量 = dtlData.数量;
                srdtlhan.単位 = dtlData.単位;
                srdtlhan.単価 = cost;
                srdtlhan.金額 = amount;
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
        protected void setT02_URHD_HAN_Update(T02_URHD urhdData, DataTable dt)
        {

            using (_context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // 税計算の為仕入先データを取得する
                //M01_TOK tok = M01.M01_TOK_Single_GetData(urhdData.仕入先コード ?? 0, urhdData.仕入先枝番 ?? 0);

                #region 売上区分により仕入先を切り替える
                // ※会社名コードは必ずマルセン(自社)になる
                int wk自社コード;

                // TODO:切り捨て固定で設定しているが変更する可能性あり
                int wkＳ税区分ID = 1;
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

                        //wkＳ税区分ID = M01.M01_TOK_Single_GetData(m70自社.取引先コード ?? 0, m70自社.枝番 ?? 1).Ｓ税区分ID;
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
                        //wkＳ税区分ID = tok.Ｓ税区分ID;
                        break;
                }
                #endregion

                #region 卸値による消費税集計をおこなう
                int setTax = 0;
                foreach (DataRow dtlRow in dt.Rows)
                {
                    T02_URDTL dtl = convertDataRowToT02_URDTL_Entity(dtlRow);

                    // 商品未設定レコードは処理対象外とする
                    if (dtl.品番コード <= 0)
                        continue;

                    // 削除されたレコードは処理対象外とする
                    if (DataRowState.Deleted == dtlRow.RowState)
                        continue;

                    // チェック判定アリかつチェックボックス=オフの場合は登録処理をスキップする
                    bool bval,
                        仕入チェック = bool.TryParse(dtlRow["マルセン仕入"].ToString(), out bval) ? bval : false;
                    if (仕入チェック == false)
                        continue;
                    
                    decimal tax = T05Service.getCalcSalesTax(urhdData.売上日, wkＳ税区分ID, dtl.品番コード, dtl.数量);
                     setTax += Decimal.ToInt32(tax);

                }
                #endregion

                T02_URHD_HAN urhd = new T02_URHD_HAN();

                urhd.伝票番号 = urhdData.伝票番号;
                urhd.会社名コード = wk自社コード;
                urhd.伝票要否 = urhdData.伝票要否;
                urhd.売上日 = urhdData.売上日;
                urhd.売上区分 = urhdData.売上区分;
                urhd.販社コード = urhdData.会社名コード;
                urhd.在庫倉庫コード = urhdData.在庫倉庫コード;
                urhd.納品伝票番号 = urhdData.納品伝票番号;
                urhd.出荷日 = urhdData.出荷日;
                urhd.受注番号 = urhdData.受注番号;
                urhd.仕入先コード = urhdData.仕入先コード;
                urhd.仕入先枝番 = urhdData.仕入先枝番;
                urhd.備考 = urhdData.備考;
                urhd.消費税 = setTax;

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

                int p税区分ID = 1;

                // 税計算の為得意先データを取得
                //var tok = M01.M01_TOK_Single_GetData(urhd.仕入先コード ?? 0, urhd.仕入先枝番 ?? 0);
                //Ｔ税区分ID = tok.Ｔ税区分ID;

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

                    // (卸値)金額算出
                    decimal cost = getWholesalePrice(dtlData.品番コード);
                    int amount = Decimal.ToInt32(cost * dtlData.数量);

                    T02_URDTL_HAN urdtl = new T02_URDTL_HAN();
                    urdtl.伝票番号 = urhd.伝票番号;
                    urdtl.行番号 = dtlData.行番号;
                    urdtl.品番コード = dtlData.品番コード;
                    urdtl.賞味期限 = dtlData.賞味期限;
                    urdtl.数量 = dtlData.数量;
                    urdtl.単位 = dtlData.単位;
                    urdtl.単価 = cost;
                    // No-72,No-73 Start
                    //urdtl.金額 = decimal.ToInt32(amount * dtlData.数量);
                    urdtl.金額 = amount;
                    // No-72,No-73 Start
                    urdtl.摘要 = dtlData.摘要;

                    T02Service.T02_URDTL_HAN_Update(urdtl);

                }
            }
            return 1;

        }

        #endregion

        #region << 仕入ヘッダ登録更新処理 >>
        /// <summary>
        /// 仕入ヘッダ情報の更新処理をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="row">売上ヘッダ行</param>
        /// <param name="userId">ログインユーザID</param>
        /// <param name="supKbn">仕入区分</param>
        public void setT03_SRHD_Update(T02_URHD urhd, CommonConstants.仕入区分 supKbn)
        {
            T03_SRHD srhd = new T03_SRHD();

            srhd.伝票番号 = urhd.伝票番号;
            srhd.会社名コード = urhd.会社名コード;
            srhd.仕入日 = urhd.売上日;
            srhd.入力区分 = (int)CommonConstants.入力区分.売上入力;
            srhd.仕入区分 = supKbn.GetHashCode();
            srhd.仕入先コード = urhd.仕入先コード ?? -1;
            srhd.仕入先枝番 = urhd.仕入先枝番 ?? -1;
            srhd.入荷先コード = getM70_JISFromM22_SOUK(urhd.在庫倉庫コード).自社コード;
            srhd.発注番号 = urhd.受注番号;
            srhd.備考 = urhd.備考;
            srhd.元伝票番号 = urhd.元伝票番号;
            srhd.消費税 = urhd.消費税;

            T03Service.T03_SRHD_Update(srhd);

        }
        #endregion

        #region << 仕入明細登録更新処理 >>
        /// <summary>
        /// 仕入明細情報の更新処理をおこなう
        /// </summary>
        /// <param name="urhd">売上ヘッダデータ</param>
        /// <param name="dt">売上明細データテーブル</param>
        /// <param name="isNonCheckItemWithout">
        /// マルセン仕入チェックが無いものを除外するか
        /// </param>
        protected void setT03_SRDTL_Update(T02_URHD urhd, DataTable dt, bool isNonCheckItemWithout)
        {
            // 登録済みデータを物理削除
            T03Service.T03_SRDTL_DeleteRecords(urhd.伝票番号);

            // 明細追加
            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;

                T02_URDTL dtlData = convertDataRowToT02_URDTL_Entity(row);
                bool bval,
                    isCheck = bool.TryParse(row["マルセン仕入"].ToString(), out bval) ? bval : false;

                if (dtlData.品番コード <= 0)
                    continue;

                // チェック判定アリかつチェックボックス=オフの場合は登録処理をスキップする
                if (isNonCheckItemWithout && isCheck == false)
                    continue;

                T03_SRDTL srdtl = new T03_SRDTL();
                srdtl.伝票番号 = urhd.伝票番号;
                srdtl.行番号 = dtlData.行番号;
                srdtl.品番コード = dtlData.品番コード;
                srdtl.賞味期限 = dtlData.賞味期限;
                srdtl.数量 = dtlData.数量;
                srdtl.単位 = dtlData.単位;
                srdtl.単価 = dtlData.単価;
                srdtl.金額 = dtlData.金額 ?? 0;
                srdtl.摘要 = dtlData.摘要;

                T03Service.T03_SRDTL_Update(srdtl);

            }

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
            //No-54 Start
            urdtl.金額 = Decimal.ToInt32(ParseNumeric<decimal>(wkRow["金額"]));
            //No-54 End
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