﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 売上返品入力サービスクラス
    /// </summary>
    public class DLY03020 : DLY03010
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

        #region 拡張クラス定義

        /// <summary>
        /// 売上ヘッダ(返品)検索項目クラス定義
        /// </summary>
        public class T02_URHD_RT_Extension
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
            public string 出荷先名 { get; set; }            // No-70
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
            public string 元伝票番号 { get; set; }
            public DateTime? 元売上日 { get; set; }

            /// <summary>新規作成データかどうか</summary>
            public bool データ状態 { get; set; }

            public int Ｔ消費税区分 { get; set; }             // No-101 Mod
            public int Ｔ税区分ID { get; set; }               // No-101 Mod

        }

        /// <summary>
        /// 売上明細(返品)検索項目クラス定義
        /// </summary>
        public class T02_URDTL_RT_Extension
        {
            public string 伝票番号 { get; set; }
            public int 行番号 { get; set; }
            public string 品番コード { get; set; }
            public DateTime? 賞味期限 { get; set; }
            public decimal? 数量 { get; set; }
            public string 単位 { get; set; }
            public decimal? 単価 { get; set; }
            public int 金額 { get; set; }
            public string 税区分 { get; set; }             // No-94 Add
            public string 摘要 { get; set; }

            public bool マルセン仕入 { get; set; }
            public string 自社品番 { get; set; }
            public string 得意先品番コード { get; set; }
            public string 自社品名 { get; set; }
            public string 自社色 { get; set; }
            public string 自社色名 { get; set; }
            public decimal? 売上数量 { get; set; }
            /// <summary>0:通常、1:軽減税率、2:非課税</summary>
            public int 消費税区分 { get; set; }
            /// <summary>1:食品、2:繊維、3:その他</summary>
            public int 商品分類 { get; set; }

        }

        #endregion


        #region 売上返品情報取得
        /// <summary>
        /// 売上検索情報を取得する
        /// </summary>
        /// <param name="companyCode">自社コード</param>
        /// <param name="slipNumber">伝票番号</param>
        /// <param name="userId">ログインユーザID</param>
        /// <returns></returns>
        public DataSet GetRtData(string companyCode, string slipNumber, int userId)
        {
            DataSet t02ds = new DataSet();

            List<T02_URHD_RT_Extension> hdList = getT02_URHD_RT_Extension(companyCode, slipNumber, userId);
            List<T02_URDTL_RT_Extension> dtlList = getT02_URDTL_RT_Extension(companyCode, slipNumber);
            M73 taxService = new M73();
            List<M73_ZEI> taxList = taxService.GetDataList();
            // 項目制御用に自社情報(自社区分)を取得する
            M70 jisService = new M70();
            List<M70_JIS> jisList = jisService.GetData(companyCode, CommonConstants.PagingOption.Paging_Code.GetHashCode());

            if (hdList.Count == 0)
                return t02ds;

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

        #region << 売上返品入力情報取得 >>

        #region 売上ヘッダ情報取得
        /// <summary>
        /// 売上ヘッダ情報を取得する
        /// </summary>
        /// <param name="companyCode">会社名コード</param>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        private List<T02_URHD_RT_Extension> getT02_URHD_RT_Extension(string companyCode, string slipNumber, int userId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int code, num;
                if (int.TryParse(companyCode, out code) && int.TryParse(slipNumber, out num))
                {
                    var result =
                        context.T02_URHD.Where(w => w.削除日時 == null && w.会社名コード == code && w.伝票番号 == num)
                            .GroupJoin(context.T02_URHD.Where(w => w.削除日時 == null),
                                x => x.元伝票番号,
                                y => y.伝票番号,
                                (x, y) => new { x, y })
                            .SelectMany(g => g.y.DefaultIfEmpty(),
                                (a, b) => new { URTHD = a.x, URHD = b })
                            .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                                x => new { コード = x.URTHD.得意先コード, 枝番 = x.URTHD.得意先枝番 },
                                y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                                (x, y) => new { x, y })
                            .SelectMany(g => g.y.DefaultIfEmpty(),
                                (c, d) => new { c.x.URTHD, c.x.URHD, TOK = d })
                            .ToList();

                    if (result.Select(s => s.URTHD.売上区分).FirstOrDefault() >= CommonConstants.売上区分.通常売上返品.GetHashCode())
                    {
                        // 返品データ(編集)の場合
                        var resultRt =
                            result.Select(s => new T02_URHD_RT_Extension
                            {
                                伝票番号 = s.URTHD.伝票番号.ToString(),
                                会社名コード = s.URTHD.会社名コード.ToString(),
                                伝票要否 = s.URTHD.伝票要否,
                                売上日 = s.URTHD.売上日,
                                売上区分 = s.URTHD.売上区分,
                                得意先コード = s.URTHD.得意先コード.ToString(),
                                得意先枝番 = s.URTHD.得意先枝番.ToString(),
                                在庫倉庫コード = s.URTHD.在庫倉庫コード.ToString(),
                                納品伝票番号 = s.URTHD.納品伝票番号.ToString(),
                                出荷日 = s.URTHD.出荷日,
                                受注番号 = s.URTHD.受注番号.ToString(),
                                出荷元コード = s.URTHD.出荷元コード.ToString(),
                                出荷元枝番 = s.URTHD.出荷元枝番.ToString(),
                                出荷元名 = s.URTHD.出荷元名.ToString(),
                                出荷先コード = s.URTHD.出荷先コード.ToString(),
                                出荷先枝番 = s.URTHD.出荷先枝番.ToString(),
                                出荷先名 = s.URTHD.出荷先名.ToString(),                 // No-70
                                仕入先コード = s.URTHD.仕入先コード.ToString(),
                                仕入先枝番 = s.URTHD.仕入先枝番.ToString(),
                                備考 = s.URTHD.備考,
                                元伝票番号 = s.URTHD.元伝票番号.ToString(),
                                元売上日 = s.URHD == null ? (DateTime?)null : s.URHD.売上日,
                                消費税 = s.URTHD.消費税,
                                // No-94 Add Start
                                通常税率対象金額 = s.URTHD.通常税率対象金額 ?? 0,
                                軽減税率対象金額 = s.URTHD.軽減税率対象金額 ?? 0,
                                通常税率消費税 = s.URTHD.通常税率消費税 ?? 0,
                                軽減税率消費税 = s.URTHD.軽減税率消費税 ?? 0,
                                // No-94 Add End
                                // No-95 Add Start
                                小計 = s.URTHD.小計 ?? 0,
                                総合計 = s.URTHD.総合計 ?? 0,
                                // No-95 Add End
                                データ状態 = false,
                                Ｔ消費税区分 = s.TOK.Ｔ消費税区分,                  // No-101 Mod
                                Ｔ税区分ID = s.TOK.Ｔ税区分ID                       // No-101 Mod
                            })
                            .ToList();

                        return resultRt;

                    }
                    else
                    {
                        // 売上データ(新規)の場合

                        // ①新規伝票番号を取得
                        M88 svc = new M88();
                        int newDenNum = svc.getNextNumber(CommonConstants.明細番号ID.ID01_売上_仕入_移動, userId);
                        DateTime retDate = (DateTime)AppCommon.DateTimeToDate(DateTime.Now);

                        // ②売上区分は元伝票の売上区分に対する返品を取得する
                        int salesKbn = getReturnSalesKbn(result.Select(s => s.URTHD.売上区分).FirstOrDefault());

                        // ②ヘッダデータを作成
                        var resultHd =
                            result.Select(s => new T02_URHD_RT_Extension
                            {
                                伝票番号 = newDenNum.ToString(),// 新規伝票番号を設定
                                会社名コード = s.URTHD.会社名コード.ToString(),
                                伝票要否 = CommonConstants.伝票要否.未発行.GetHashCode(),
                                売上日 = retDate,// ⇒返品日(初期値としてシステム日付を設定)
                                売上区分 = salesKbn,// 元伝票の売上区分の返品を設定
                                得意先コード = s.URTHD.得意先コード.ToString(),
                                得意先枝番 = s.URTHD.得意先枝番.ToString(),
                                在庫倉庫コード = s.URTHD.在庫倉庫コード.ToString(),
                                納品伝票番号 = s.URTHD.納品伝票番号.ToString(),
                                出荷日 = s.URTHD.出荷日,
                                受注番号 = s.URTHD.受注番号.ToString(),
                                出荷元コード = s.URTHD.出荷元コード.ToString(),
                                出荷元枝番 = s.URTHD.出荷元枝番.ToString(),
                                出荷元名 = s.URTHD.出荷元名.ToString(),
                                出荷先コード = s.URTHD.出荷先コード.ToString(),
                                出荷先枝番 = s.URTHD.出荷先枝番.ToString(),
                                出荷先名 = s.URTHD.出荷先名.ToString(),                 // No-70
                                仕入先コード = s.URTHD.仕入先コード.ToString(),
                                仕入先枝番 = s.URTHD.仕入先枝番.ToString(),
                                備考 = s.URTHD.備考,
                                元伝票番号 = s.URTHD.伝票番号.ToString(),
                                元売上日 = s.URTHD.売上日,
                                消費税 = s.URTHD.消費税,
                                // No-94 Add Start
                                通常税率対象金額 = s.URTHD.通常税率対象金額 ?? 0,
                                軽減税率対象金額 = s.URTHD.軽減税率対象金額 ?? 0,
                                通常税率消費税 = s.URTHD.通常税率消費税 ?? 0,
                                軽減税率消費税 = s.URTHD.軽減税率消費税 ?? 0,
                                // No-94 Add End
                                // No-95 Add Start
                                小計 = s.URTHD.小計 ?? 0,
                                総合計 = s.URTHD.総合計 ?? 0,
                                // No-95 Add End
                                データ状態 = true,
                                Ｔ消費税区分 = s.TOK.Ｔ消費税区分,                  // No-101 Mod
                                Ｔ税区分ID = s.TOK.Ｔ税区分ID                       // No-101 Mod
                            })
                            .ToList();

                        return resultHd;

                    }

                }
                else
                {
                    return new List<T02_URHD_RT_Extension>();

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
        private List<T02_URDTL_RT_Extension> getT02_URDTL_RT_Extension(string myCompany, string slipNumber)
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

                    // データ無し終了
                    if (urhd == null)
                        return null;

                    // 基本となるデータを取得
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
                            .ToList();

                    if (urhd.売上区分 >= CommonConstants.売上区分.通常売上返品.GetHashCode())
                    {
                        // データが返品データの場合

                        // 元伝票から売上数量を取得
                        int 元伝票番号 = urhd.元伝票番号 ?? 0;

                        var resultRt =
                            result
                                .GroupJoin(context.T02_URDTL.Where(w => w.削除日時 == null && w.伝票番号 == 元伝票番号),
                                    x => x.URDTL.行番号,
                                    y => y.行番号,
                                    (x, y) => new { x, y })
                                .SelectMany(x => x.y.DefaultIfEmpty(),
                                    (c, d) => new { URTDTL = c.x.URDTL, c.x.HIN, c.x.IRO, c.x.SRDTL_HAN, c.x.TOKHIN, URDTL = d })
                                .Select(x => new T02_URDTL_RT_Extension
                                {
                                    伝票番号 = x.URTDTL.伝票番号.ToString(),
                                    行番号 = x.URTDTL.行番号,
                                    品番コード = x.URTDTL.品番コード.ToString(),
                                    自社品番 = x.HIN.自社品番,
                                    得意先品番コード = x.TOKHIN == null ? "" : x.TOKHIN.得意先品番コード,
                                    自社品名 = x.HIN.自社品名,
                                    自社色 = x.HIN.自社色,
                                    自社色名 = x.IRO != null ? x.IRO.色名称 : "",
                                    賞味期限 = x.URTDTL.賞味期限,
                                    売上数量 = x.URDTL != null ? x.URDTL.数量 : (decimal?)null,
                                    数量 = x.URTDTL.数量,
                                    単位 = x.URTDTL.単位,
                                    単価 = x.URTDTL.単価,
                                    金額 = x.URTDTL.金額 ?? 0,
                                    税区分 =           // No-94 Add
                                        x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? CommonConstants.消費税区分略称_軽減税率 :
                                        x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.非課税 ? CommonConstants.消費税区分略称_非課税 : string.Empty,
                                    摘要 = x.URTDTL.摘要,
                                    マルセン仕入 = x.SRDTL_HAN != null,
                                    消費税区分 = x.HIN.消費税区分 ?? 0,
                                    商品分類 = x.HIN.商品分類 ?? 0
                                })
                                .ToList();

                        return resultRt;

                    }
                    else
                    {
                        // データが売上データの場合
                        var resultDtl =
                            result.Select(x =>
                                new T02_URDTL_RT_Extension
                                {
                                    伝票番号 = x.URDTL.伝票番号.ToString(),
                                    行番号 = x.URDTL.行番号,
                                    品番コード = x.URDTL.品番コード.ToString(),
                                    自社品番 = x.HIN.自社品番,
                                    得意先品番コード = x.TOKHIN == null ? "" : x.TOKHIN.得意先品番コード,
                                    自社品名 = x.HIN.自社品名,
                                    自社色 = x.HIN.自社色,
                                    自社色名 = x.IRO == null ? "" : x.IRO.色名称,
                                    賞味期限 = x.URDTL.賞味期限,
                                    売上数量 = x.URDTL.数量,
                                    数量 = x.URDTL.数量,
                                    単位 = x.URDTL.単位,
                                    単価 = x.URDTL.単価,
                                    金額 = x.URDTL.金額 ?? 0,
                                    税区分 =           // No-94 Add
                                        x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? CommonConstants.消費税区分略称_軽減税率 :
                                        x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.非課税 ? CommonConstants.消費税区分略称_非課税 : string.Empty,
                                    摘要 = x.URDTL.摘要,
                                    マルセン仕入 = x.SRDTL_HAN != null,
                                    消費税区分 = x.HIN.消費税区分 ?? 0,
                                    商品分類 = x.HIN.商品分類 ?? 0
                                })
                                .ToList();

                        return resultDtl;

                    }

                }
                else
                {
                    return new List<T02_URDTL_RT_Extension>();
                }

            }

        }
        #endregion

        #region 返品売上区分取得
        /// <summary>
        /// 売上区分に対応する返品売上区分を取得する
        /// </summary>
        /// <param name="sales"></param>
        /// <returns></returns>
        private int getReturnSalesKbn(int sales)
        {
            int salesKbn = 0;
            switch (sales)
            {
                case (int)CommonConstants.売上区分.通常売上:
                    salesKbn = CommonConstants.売上区分.通常売上返品.GetHashCode();
                    break;

                case (int)CommonConstants.売上区分.販社売上:
                    salesKbn = CommonConstants.売上区分.販社売上返品.GetHashCode();
                    break;

                case (int)CommonConstants.売上区分.メーカー直送:
                    salesKbn = CommonConstants.売上区分.メーカー直送返品.GetHashCode();
                    break;

                case (int)CommonConstants.売上区分.メーカー販社商流直送:
                    salesKbn = CommonConstants.売上区分.メーカー販社商流直送返品.GetHashCode();
                    break;

                case (int)CommonConstants.売上区分.委託売上:
                    salesKbn = CommonConstants.売上区分.委託売上返品.GetHashCode();
                    break;

                case (int)CommonConstants.売上区分.預け売上:
                    salesKbn = CommonConstants.売上区分.預け売上返品.GetHashCode();
                    break;

                default:
                    break;

            }

            return salesKbn;

        }
        #endregion

        #endregion

        #region 売上返品入力情報登録・更新
        /// <summary>
        /// 売上入力情報を登録・更新する
        /// </summary>
        /// <param name="ds">
        /// 売上データセット
        /// [0:T032_URHD、1:T02_URDTL]
        /// </param>
        /// <param name="userId">ユーザID</param>
        /// <returns></returns>
        public void Update(DataSet ds, int userId)
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
                    S04Service = new S04(context, userId, S04.機能ID.売上返品);

                    try
                    {
                        DataRow hdRow = ds.Tables[T02_HEADER_TABLE_NAME].Rows[0];
                        T02_URHD urhd = convertDataRowToT02_URHD_Entity(hdRow);
                        DataTable dtlTbl = ds.Tables[T02_DETAIL_TABLE_NAME];

                        // 登録対象データの状態(新規or編集)を取得する
                        bool isRegistData =
                            context.T02_URHD
                                .Where(w => w.削除日時 == null && w.伝票番号 == urhd.伝票番号)
                                .Count() == 0;

                        switch (urhd.売上区分)
                        {
                            case (int)CommonConstants.売上区分.通常売上返品:
                            case (int)CommonConstants.売上区分.委託売上返品:
                            case (int)CommonConstants.売上区分.預け売上返品:
                                setUsualSaleReturnsProc(urhd, dtlTbl, isRegistData, hdRow);                 // No.156-4 Mod
                                break;

                            case (int)CommonConstants.売上区分.販社売上返品:
                                setSalesCompanyReturnsProc(urhd, dtlTbl, hdRow,isRegistData);               // No-97 Mod
                                break;

                            case (int)CommonConstants.売上区分.メーカー直送返品:
                                setMakerDirectReturnsProc(urhd, dtlTbl, hdRow, isRegistData);               // No.156-4 Mod
                                break;

                            case (int)CommonConstants.売上区分.メーカー販社商流直送返品:
                                setCommerceDirectDeliveryReturnsProc(urhd, dtlTbl, hdRow, isRegistData);    // No-97 Mod
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

        }
        #endregion


        #region << 売上区分別更新処理群 >>

        #region 通常、委託、預け売上返品更新
        /// <summary>
        /// 通常、委託、預け売上時の更新処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ds">登録対象データセット</param>
        /// <param name="userId">ログインユーザID</param>
        /// <param name="isRegist">データ状態(登録or編集)：登録時）真、編集時）偽</param>
        /// <param name="orghd">編集前ヘッダーデータ</param>
        private void setUsualSaleReturnsProc(T02_URHD urhd, DataTable dtlTbl, bool isRegist, DataRow orghd)
        {
            // 1.売上ヘッダの更新
            T02Service.T02_URHD_Update(urhd);

            // 2.売上詳細の更新
            setT02_URDTL_Update(urhd, dtlTbl, false);

            // 3.在庫の更新
            setS03_STOK_Returns_Update(urhd, dtlTbl, false, isRegist);

            // 4.入出庫履歴の生成
            setS04_HISTORY_Create(urhd, dtlTbl, orghd);         // No.156-4

        }
        #endregion

        #region 販社売上返品更新
        /// <summary>
        /// 販社売上時の更新処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="urhd">売上ヘッダーデータ</param>
        /// <param name="dtlTbl">売上明細データ</param>
        /// <param name="hdRow">編集前ヘッダーデータ</param>
        /// <param name="isRegist">データ状態(登録or編集)：登録時）真、編集時）偽</param>
        private void setSalesCompanyReturnsProc(T02_URHD urhd, DataTable dtlTbl, DataRow hdRow, bool isRegist)
        {

            // 出荷元と出荷先が異なる場合場合のみ移動処理を行う
            bool difShip = urhd.在庫倉庫コード != T05Service.getShippingDestination(urhd);

            // 1.売上ヘッダの更新
            T02Service.T02_URHD_Update(urhd);

            // 2.売上詳細の更新
            setT02_URDTL_Update(urhd, dtlTbl, false);

            // 3.在庫(返品)の更新(得意先⇒販社)
            setS03_STOK_Returns_Update(urhd, dtlTbl, false, isRegist);

            // 4.入出庫履歴の生成
            setS04_HISTORY_Create(urhd, dtlTbl, hdRow);        // No.156-4 Mod

            // 5.販社仕入ヘッダの更新
            setT03_SRHD_HAN_Update(urhd, dtlTbl, hdRow, CommonConstants.仕入区分.返品);       // No-97 Mod

            // 6.販社仕入明細の更新
            setT03_SRDTL_HAN_Update(urhd, dtlTbl);

            // 7.販社売上ヘッダの更新
            setT02_URHD_HAN_Update(urhd, dtlTbl, hdRow);       // No-97 Mod

            // 8.販社売上明細の更新
            setT02_URDTL_HAN_Update(urhd, dtlTbl);

            // 在庫倉庫が自社と異なる場合のみ処理をおこなう
            if (difShip)
            {
                // 9.移動ヘッダの更新
                setT05_IDOHD_Returns_Update(urhd);

                // 10.移動明細の更新
                setT05_IDODTL_Update(urhd, dtlTbl, true);

                // 11.在庫返品処理(販社⇒マルセン(自社))
                setS03_STOK_Returns_Update(urhd, dtlTbl, true, isRegist);

                // 12.販社の入出庫履歴の生成
                setS04_HISTORY_HANReturns_Update(urhd, dtlTbl, hdRow, true);        // No.156-4
            }
        }
        #endregion

        #region メーカー直送返品更新
        /// <summary>
        /// メーカー直送時の更新処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="urhd">売上ヘッダーデータ</param>
        /// <param name="dtlTbl">売上明細データ</param>
        /// <param name="orghd">編集前売上ヘッダーデータ</param>
        /// <param name="isRegist">データ状態(登録or編集)：登録時）真、編集時）偽</param>
        private void setMakerDirectReturnsProc(T02_URHD urhd, DataTable dtlTbl, DataRow orghd, bool isRegist)
        {
            // 1.売上ヘッダの更新
            T02Service.T02_URHD_Update(urhd);

            // 2.売上明細の更新
            setT02_URDTL_Update(urhd, dtlTbl, false);

            // 3.返品在庫の更新
            setS03_STOK_Returns_Update(urhd, dtlTbl, false, isRegist);

            // 4.入出庫履歴の生成(入庫)
            setS04_HISTORY_Create(urhd, dtlTbl, orghd);         // No.156-4

            // 5-1.仕入ヘッダ金額の再計算
            T02_URHD recalcData = reCalcT03_SRHD(urhd, dtlTbl);           // No.272 Add

            // 5-2.仕入ヘッダの更新
            setT03_SRHD_Update(recalcData, CommonConstants.仕入区分.返品);

            // 6.仕入詳細の更新
            setT03_SRDTL_Update(urhd, dtlTbl, false);

            #region 7.仕入在庫の更新(自社⇒メーカー)
            foreach (DataRow row in dtlTbl.Rows)
            {
                T02_URDTL urdtl = convertDataRowToT02_URDTL_Entity(row);

                if (urdtl.品番コード <= 0)
                    continue;

                decimal stockQty = 0;

                #region 在庫調整数計算
                if (isRegist)
                {
                    // 登録時：設定された数量をそのまま処理
                    // 設定数量分を在庫を加算(返品)
                    stockQty = urdtl.数量;

                }
                else
                {
                    // 編集時
                    if (row.RowState == DataRowState.Deleted)
                    {
                        // 削除データは処理対象外
                        continue;
                    }
                    else if (row.RowState == DataRowState.Added)
                    {
                        // 追加データは機能上あり得ない
                        continue;
                    }
                    else if (row.RowState == DataRowState.Modified)
                    {
                        // オリジナル(変更前数量)と比較して差分数量を加減算
                        if (row.HasVersion(DataRowVersion.Original))
                        {
                            decimal orgQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]);
                            stockQty = (urdtl.数量 - orgQty);             // No-98 Mod
                        }

                    }
                    else
                    {
                        // 対象なし(DataRowState.Unchanged)
                        continue;
                    }

                }
                #endregion

                S03_STOK stok = new S03_STOK();
                stok.倉庫コード = urhd.在庫倉庫コード;
                stok.品番コード = urdtl.品番コード;
                stok.賞味期限 = AppCommon.DateTimeToDate(urdtl.賞味期限, DateTime.MaxValue);
                stok.在庫数 = stockQty * -1;

                S03Service.S03_STOK_Update(stok);

            }
            #endregion

            #region 8.仕入入出庫履歴の生成(メーカへの出庫)
            foreach (DataRow row in dtlTbl.Rows)
            {
                // 売上明細データ取得
                T02_URDTL urdtl = convertDataRowToT02_URDTL_Entity(row);

                // 商品未設定レコードは処理しない
                if (urdtl.品番コード <= 0)
                    continue;

                S04_HISTORY history = new S04_HISTORY();

                history.入出庫日 = urhd.売上日;    // 返品日
                history.入出庫時刻 = com.GetDbDateTime().TimeOfDay;
                history.倉庫コード = urhd.在庫倉庫コード;
                history.入出庫区分 = (int)CommonConstants.入出庫区分.ID02_出庫;
                history.品番コード = urdtl.品番コード;
                history.賞味期限 = urdtl.賞味期限;
                history.数量 = Math.Abs(decimal.ToInt32(urdtl.数量));
                history.伝票番号 = urhd.伝票番号;

                Dictionary<string, string> hstDic = new Dictionary<string, string>()
                    {
                        // No.156-4 Mod STart
                        { S04.COLUMNS_NAME_入出庫日, orghd == null ? 
                                                        history.入出庫日.ToString("yyyy/MM/dd") : string.Format("{0:yyyy/MM/dd}", orghd["売上日", DataRowVersion.Original]) },
                        // No.156-4 Mod End
                        { S04.COLUMNS_NAME_倉庫コード, history.倉庫コード.ToString() },
                        { S04.COLUMNS_NAME_伝票番号, history.伝票番号.ToString() },
                        { S04.COLUMNS_NAME_品番コード, history.品番コード.ToString() }
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
                // No.156-4 Mod STart
                else
                {
                    // 売上更新の為、履歴更新(DataRowState.Unchanged、DataRowState.Modified)
                    S04Service.UpdateProductHistory(history, hstDic);
                }
                // No.156-4 Mod End
            }
            #endregion

        }
        #endregion

        #region メーカー販社商流直送返品更新
        /// <summary>
        /// メーカー販社商流直送時の更新処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ds">登録対象データセット</param>
        /// <param name="userId">ログインユーザID</param>
        /// <param name="isRegist">データ状態(登録or編集)：登録時）真、編集時）偽</param>
        private void setCommerceDirectDeliveryReturnsProc(T02_URHD urhd, DataTable dtlTbl, DataRow hdRow, bool isRegist)
        {
            // 1.売上ヘッダの更新
            T02Service.T02_URHD_Update(urhd);

            // 2.売上明細の更新
            setT02_URDTL_Update(urhd, dtlTbl, false);

            // 3.在庫(売上返品)の更新
            setS03_STOK_Returns_Update(urhd, dtlTbl, false, isRegist);

            // 4.入出庫履歴の生成
            setS04_HISTORY_Create(urhd, dtlTbl, hdRow);         // No.156-4 Mod

            // 5.移動ヘッダの更新(販社⇒マルセン)
            setT05_IDOHD_Returns_Update(urhd);

            // 6.移動明細の更新
            // No-74 Mod Start
            // 自社倉庫と在庫倉庫が異なる場合場合のみ移動処理を行う(メーカ販社は必ず異なる)
            bool difShip = urhd.在庫倉庫コード != T05Service.getShippingDestination(urhd);
            setT05_IDODTL_Update(urhd, dtlTbl, difShip);
            // No-74 Mod Start

            // 7.販社仕入ヘッダの更新(販社⇒マルセン)
            setT03_SRHD_HAN_Update(urhd, dtlTbl, hdRow, CommonConstants.仕入区分.返品);       // No-97 Mod

            // 8.販社仕入明細の更新
            setT03_SRDTL_HAN_Update(urhd, dtlTbl);

            // 9.販社売上ヘッダの更新
            setT02_URHD_HAN_Update(urhd, dtlTbl, hdRow);       // No-97 Mod

            // 10.販社売上明細の更新
            setT02_URDTL_HAN_Update(urhd, dtlTbl);

            // 11.在庫返品処理(販社⇒マルセン(自社))
            setS03_STOK_Returns_Update(urhd, dtlTbl, true, isRegist);

            // 12.販社の入出庫履歴の生成
            setS04_HISTORY_HANReturns_Update(urhd, dtlTbl, hdRow, true);    // No.156-4 MOd

            #region 13.仕入ヘッダの更新(メーカー⇒マルセン)
            // 仕入ヘッダ金額の再計算
            T02_URHD recalcData = reCalcT03_SRHD(urhd, dtlTbl);           // No.272 Add

            T03_SRHD srhd = new T03_SRHD();

            srhd.伝票番号 = urhd.伝票番号;
            srhd.会社名コード = urhd.会社名コード;
            srhd.仕入日 = urhd.売上日;
            srhd.入力区分 = (int)CommonConstants.入力区分.売上入力;
            srhd.仕入区分 = (int)CommonConstants.仕入区分.返品;
            srhd.仕入先コード = urhd.仕入先コード ?? -1;
            srhd.仕入先枝番 = urhd.仕入先枝番 ?? -1;
            srhd.入荷先コード = getM70_JISFromM22_SOUK(urhd.在庫倉庫コード).自社コード;
            srhd.発注番号 = urhd.受注番号;
            srhd.備考 = urhd.備考;
            srhd.元伝票番号 = urhd.元伝票番号;
            // No-94 Add Start
            srhd.通常税率対象金額 = recalcData.通常税率対象金額;
            srhd.軽減税率対象金額 = recalcData.軽減税率対象金額;
            srhd.通常税率消費税 = recalcData.通常税率消費税;
            srhd.軽減税率消費税 = recalcData.軽減税率消費税;
            // No-94 Add End
            // No-95 Add Start
            srhd.小計 = recalcData.小計;
            srhd.総合計 = recalcData.総合計;
            // No-95 Add End
            srhd.消費税 = recalcData.消費税;

            T03Service.T03_SRHD_Update(srhd);
            #endregion

            // 14.仕入明細の更新
            setT03_SRDTL_Update(urhd, dtlTbl, false);

            #region 15.在庫更新(メーカーへの出庫)
            foreach (DataRow row in dtlTbl.Rows)
            {
                T02_URDTL urdtl = convertDataRowToT02_URDTL_Entity(row);

                if (urdtl.品番コード <= 0)
                    continue;

                decimal stockQty = 0;

                #region 在庫調整数計算
                if (isRegist)
                {
                    // 登録時：設定された数量をそのまま処理
                    // 設定数量分を在庫を加算(返品)
                    stockQty = urdtl.数量;

                }
                else
                {
                    // 編集時
                    if (row.RowState == DataRowState.Deleted)
                    {
                        // 削除データは処理対象外
                        continue;
                    }
                    else if (row.RowState == DataRowState.Added)
                    {
                        // 追加データは機能上あり得ない
                        continue;
                    }
                    else if (row.RowState == DataRowState.Modified)
                    {
                        // オリジナル(変更前数量)と比較して差分数量を加減算
                        if (row.HasVersion(DataRowVersion.Original))
                        {
                            decimal orgQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]);
                            stockQty = (urdtl.数量 - orgQty);             // No-98 Mod
                        }

                    }
                    else
                    {
                        // 対象なし(DataRowState.Unchanged)
                        continue;
                    }

                }
                #endregion

                S03_STOK stok = new S03_STOK();
                stok.倉庫コード = urhd.在庫倉庫コード;
                stok.品番コード = urdtl.品番コード;
                stok.賞味期限 = AppCommon.DateTimeToDate(urdtl.賞味期限, DateTime.MaxValue);
                stok.在庫数 = stockQty * -1;

                S03Service.S03_STOK_Update(stok);

            }
            #endregion

            #region 16.入出庫履歴の生成(自社⇒メーカーの出庫)
            foreach (DataRow row in dtlTbl.Rows)
            {
                // 売上明細データ取得
                T02_URDTL urdtl = convertDataRowToT02_URDTL_Entity(row);

                // 商品未設定レコードは処理しない
                if (urdtl.品番コード <= 0)
                    continue;

                S04_HISTORY history = new S04_HISTORY();

                history.入出庫日 = urhd.売上日;    // 返品日
                history.入出庫時刻 = com.GetDbDateTime().TimeOfDay;
                history.倉庫コード = urhd.在庫倉庫コード;
                history.入出庫区分 = (int)CommonConstants.入出庫区分.ID02_出庫;
                history.品番コード = urdtl.品番コード;
                history.賞味期限 = urdtl.賞味期限;
                history.数量 = Math.Abs(decimal.ToInt32(urdtl.数量));
                history.伝票番号 = urhd.伝票番号;

                Dictionary<string, string> hstDic = new Dictionary<string, string>()
                    {
                        // No.156-4 Mod STart
                        { S04.COLUMNS_NAME_入出庫日, hdRow == null ? 
                                                        history.入出庫日.ToString("yyyy/MM/dd") : string.Format("{0:yyyy/MM/dd}", hdRow["売上日", DataRowVersion.Original]) },
                        // No.156-4 Mod End
                        { S04.COLUMNS_NAME_倉庫コード, history.倉庫コード.ToString() },
                        { S04.COLUMNS_NAME_伝票番号, history.伝票番号.ToString() },
                        { S04.COLUMNS_NAME_品番コード, history.品番コード.ToString() }
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
                // No.156-4 Mod STart
                else
                {
                    // 売上更新の為、履歴更新(DataRowState.Unchanged、DataRowState.Modified)
                    S04Service.UpdateProductHistory(history, hstDic);
                }
                // No.156-4 Mod End

            }
            #endregion

        }
        #endregion

        #endregion

        #region << 販社仕入ヘッダ登録更新処理 >>
        // No-97 Add Start
        /// <summary>
        /// 販社仕入ヘッダの登録・更新をおこなう
        /// </summary>
        /// <param name="t02Data"></param>
        /// <param name="dtlTbl"></param>
        /// <param name="hdRow"></param>
        /// <param name="supKbn"></param>
        private void setT03_SRHD_HAN_Update(T02_URHD t02Data, DataTable dtlTbl, DataRow hdRow, CommonConstants.仕入区分 supKbn)
        {
            // 売上対象の自社情報・取引先情報を取得する
            var m70自社 = getFromJis();
            M70_JIS srJis = getM70_JISFromM22_SOUK(t02Data.在庫倉庫コード);
            M01_TOK tok = M01.M01_TOK_Single_GetData(srJis.取引先コード ?? 0, srJis.枝番 ?? 0);

            // 消費税再計算の基準日は元伝票の売上日とする
            DateTime strUriageDate = DateTime.Parse(hdRow["元売上日"].ToString());

            #region 消費税再計算集計
            int setTax = 0;
            int intTsujyo = 0;
            int intKeigen = 0;
            int intHikazei = 0;
            int intTaxTsujyo = 0;
            int intTaxKeigen = 0;


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

                int intZeikbn = int.Parse(dtlRow["消費税区分"].ToString());

                decimal dcmCost = getWholesalePrice(dtl.品番コード);
                int intKingakuWk = Decimal.ToInt32(decimal.Parse(((decimal)(dcmCost * dtl.数量)).ToString()));
                int intTaxWk = Decimal.ToInt32(T05Service.getCalcSalesTax(strUriageDate, tok.Ｔ税区分ID, dtl.品番コード, dtl.数量, tok.Ｓ支払消費税区分));       // No-101, 272 Mod

                switch (intZeikbn)
                {
                    case (int)CommonConstants.商品消費税区分.通常税率:
                        intTsujyo += intKingakuWk;
                        intTaxTsujyo += intTaxWk;
                        break;
                    case (int)CommonConstants.商品消費税区分.軽減税率:
                        intKeigen += intKingakuWk;
                        intTaxKeigen += intTaxWk;
                        break;
                    case (int)CommonConstants.商品消費税区分.非課税:
                        intHikazei += intKingakuWk;
                        break;
                    default:
                        break;
                }
                setTax += intTaxWk;

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
            srhdhan.通常税率対象金額 = intTsujyo;
            srhdhan.軽減税率対象金額 = intKeigen;
            srhdhan.通常税率消費税 = intTaxTsujyo;
            srhdhan.軽減税率消費税 = intTaxKeigen;
            srhdhan.小計 = intTsujyo + intKeigen + intHikazei;
            srhdhan.総合計 = srhdhan.小計 + setTax;
            srhdhan.消費税 = setTax;

            T03Service.T03_SRHD_HAN_Update(srhdhan);

        }
        // No-97 Add End
        #endregion

        #region << 販社売上ヘッダ更新処理 >>
        // No-97 Add Start
        /// <summary>
        /// 販社売上ヘッダ情報の更新をおこなう
        /// </summary>
        /// <param name="urhdData">売上ヘッダデータ</param>
        /// <param name="dt">売上明細データテーブル</param>
        /// <param name="hdRow"></param>
        private void setT02_URHD_HAN_Update(T02_URHD urhdData, DataTable dt, DataRow hdRow)
        {

            using (_context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                #region 売上区分により仕入先を切り替える
                // ※会社名コードは必ずマルセン(自社)になる
                int wk自社コード;

                // TODO:切り捨て固定で設定しているが変更する可能性あり
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

                // 消費税再計算の基準日は元伝票の売上日とする
                DateTime strUriageDate = DateTime.Parse(hdRow["元売上日"].ToString());

                #region 卸値による消費税集計をおこなう
                int setTax = 0;
                int intTsujyo = 0;
                int intKeigen = 0;
                int intHikazei = 0;
                int intTaxTsujyo = 0;
                int intTaxKeigen = 0;

                // No.272 Add Start
                // 販社の税区分を取得する
                List<M70_JIS> jisList = M70.GetHanList();
                var jis = jisList.Where(w => w.自社コード == urhdData.会社名コード).FirstOrDefault();
                int code = Int32.Parse(jis.取引先コード.ToString());
                int eda = Int32.Parse(jis.枝番.ToString());
                M01_TOK tok = M01.M01_TOK_Single_GetData(code, eda);
                int Ｔ消費税区分 = tok != null ? tok.Ｔ消費税区分 : 1;
                int Ｔ税区分ID = tok != null ? tok.Ｔ税区分ID : 1;

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


                    int intZeikbn = int.Parse(dtlRow["消費税区分"].ToString());

                    decimal dcmCost = getWholesalePrice(dtl.品番コード);
                    int intKingakuWk = Decimal.ToInt32(decimal.Parse(((decimal)(dcmCost * dtl.数量)).ToString()));
                    int intTaxWk = Decimal.ToInt32(T05Service.getCalcSalesTax(strUriageDate, Ｔ税区分ID, dtl.品番コード, dtl.数量, Ｔ消費税区分));             // No-101,272 Mod

                    switch (intZeikbn)
                    {
                        case (int)CommonConstants.商品消費税区分.通常税率:
                            intTsujyo += intKingakuWk;
                            intTaxTsujyo += intTaxWk;
                            break;
                        case (int)CommonConstants.商品消費税区分.軽減税率:
                            intKeigen += intKingakuWk;
                            intTaxKeigen += intTaxWk;
                            break;
                        case (int)CommonConstants.商品消費税区分.非課税:
                            intHikazei += intKingakuWk;
                            break;
                        default:
                            break;
                    }
                    setTax += intTaxWk;

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
                urhd.通常税率対象金額 = intTsujyo;
                urhd.軽減税率対象金額 = intKeigen;
                urhd.通常税率消費税 = intTaxTsujyo;
                urhd.軽減税率消費税 = intTaxKeigen;
                urhd.小計 = intTsujyo + intKeigen + intHikazei;
                urhd.総合計 = urhd.小計 + setTax;
                urhd.消費税 = setTax;

                // 販社売上ヘッダの登録実行
                T02Service.T02_URHD_HAN_Update(urhd);
            }
        }
        // No-97 Add End
        #endregion

        #region 在庫返品更新
        /// <summary>
        /// 在庫情報(返品時)の更新をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ds">売上データセット[0:T03_SRHD、1:T03_SRDTL]</param>
        /// <param name="userId">ユーザID</param>
        /// <param name="isNonCheckItemWithout">マルセン仕入チェックが無いものを除外するか</param>
        /// <param name="isRegist">データ状態(登録or編集)：登録時）真、編集時）偽</param>
        /// <returns></returns>
        private void setS03_STOK_Returns_Update(T02_URHD urhd, DataTable dtlTbl, bool isNonCheckItemWithout, bool isRegist)
        {
            foreach (DataRow row in dtlTbl.Rows)
            {
                T02_URDTL urdtl = convertDataRowToT02_URDTL_Entity(row);

                if (urdtl.品番コード <= 0)
                    continue;

                bool bval,
                    仕入チェック = bool.TryParse(row["マルセン仕入"].ToString(), out bval) ? bval : false;

                // チェック判定アリかつチェックボックス=オフの場合は登録処理をスキップする
                if (isNonCheckItemWithout && 仕入チェック == false)
                    continue;

                decimal stockQty = 0;

                #region 在庫調整数計算
                if (isRegist)
                {
                    // 登録時：設定された数量をそのまま処理
                    // 設定数量分を在庫を加算(返品)
                    stockQty = urdtl.数量;

                }
                else
                {
                    // 編集時
                    if (row.RowState == DataRowState.Deleted)
                    {
                        // 削除データは処理対象外
                        continue;
                    }
                    else if (row.RowState == DataRowState.Added)
                    {
                        // 追加データは機能上あり得ない
                        continue;
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

                }
                #endregion

                if (isNonCheckItemWithout)
                {

                    // ⇒販社から出庫処理
                    S03_STOK outStok = new S03_STOK();

                    int outSouk = getStockpileFromJis(urhd.会社名コード);
                    outStok.倉庫コード = outSouk;
                    outStok.品番コード = urdtl.品番コード;
                    outStok.賞味期限 = AppCommon.DateTimeToDate(urdtl.賞味期限, DateTime.MaxValue);
                    outStok.在庫数 = stockQty * -1;

                    S03Service.S03_STOK_Update(outStok);

                   
                    // ⇒在庫倉庫への入庫処理
                    S03_STOK inStok = new S03_STOK();

                    int inSouk = urhd.在庫倉庫コード; 
                    inStok.倉庫コード = inSouk;
                    inStok.品番コード = urdtl.品番コード;
                    inStok.賞味期限 = AppCommon.DateTimeToDate(urdtl.賞味期限, DateTime.MaxValue);
                    inStok.在庫数 = stockQty;
                    S03Service.S03_STOK_Update(inStok);
                }
                else
                {
                    S03_STOK stok = new S03_STOK();

                    switch (urhd.売上区分)
                    {
                        case (int)CommonConstants.売上区分.メーカー直送返品:
                            // マルセン
                            stok.倉庫コード = getStockpileFromJis();
                            break;
                        case (int)CommonConstants.売上区分.販社売上返品:
                        case (int)CommonConstants.売上区分.メーカー販社商流直送返品:
                            // 自社名の倉庫
                            stok.倉庫コード = getStockpileFromJis(urhd.会社名コード);
                            break;
                        default:
                            // 在庫倉庫
                            stok.倉庫コード = urhd.在庫倉庫コード;
                            break;
                    }

                    stok.品番コード = urdtl.品番コード;
                    stok.賞味期限 = AppCommon.DateTimeToDate(urdtl.賞味期限, DateTime.MaxValue);
                    stok.在庫数 = stockQty;

                    S03Service.S03_STOK_Update(stok);
                }
            }

        }

        #endregion

        #region << 入庫履歴更新処理 >>
        /// <summary>
        /// 入庫履歴の登録・更新をおこなう
        /// </summary>
        /// <param name="hdData">売上ヘッダデータ</param>
        /// <param name="dtlTable">売上明細データテーブル</param>
        /// <param name="orghd">編集前ヘッダーデータ</param>
        private void setS04_HISTORY_Create(T02_URHD hdData, DataTable dtlTable, DataRow orghd)
        {
            foreach (DataRow row in dtlTable.Rows)
            {
                // 売上明細データ取得
                T02_URDTL urdtl = convertDataRowToT02_URDTL_Entity(row);

                // 商品未設定レコードは処理しない
                if (urdtl.品番コード <= 0)
                    continue;

                S04_HISTORY history = new S04_HISTORY();

                switch (hdData.売上区分)
                {
                    case (int)CommonConstants.売上区分.メーカー直送返品:
                        // マルセン
                        history.倉庫コード = getStockpileFromJis();
                        break;
                    case (int)CommonConstants.売上区分.販社売上返品:
                    case (int)CommonConstants.売上区分.メーカー販社商流直送返品:
                        // 自社名の倉庫
                        history.倉庫コード = getStockpileFromJis(hdData.会社名コード);
                        break;
                    default:
                        // 在庫倉庫
                        history.倉庫コード = hdData.在庫倉庫コード;
                        break;
                }

                history.入出庫日 = hdData.売上日;  // 返品日
                history.入出庫時刻 = com.GetDbDateTime().TimeOfDay;
                //history.入出庫区分 = (int)S04Service.getInboundType(row, "数量", urdtl.数量);
                history.入出庫区分 = (int)CommonConstants.入出庫区分.ID01_入庫;
                history.品番コード = urdtl.品番コード;
                history.賞味期限 = urdtl.賞味期限;
                history.数量 = decimal.ToInt32(urdtl.数量);
                history.伝票番号 = hdData.伝票番号;

                Dictionary<string, string> hstDic = new Dictionary<string, string>()
                    {
                        // No.156-3 Mod Start
                        { S04.COLUMNS_NAME_入出庫日, orghd == null ?
                                                        history.入出庫日.ToString("yyyy/MM/dd") : string.Format("{0:yyyy/MM/dd}", orghd["売上日", DataRowVersion.Original]) },
                        // No.156-3 Mod End
                        { S04.COLUMNS_NAME_倉庫コード, history.倉庫コード.ToString() },
                        { S04.COLUMNS_NAME_伝票番号, history.伝票番号.ToString() },
                        { S04.COLUMNS_NAME_品番コード, history.品番コード.ToString() }
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
                // No.156-4 Mod Start
                else
                {
                    // 売上更新の為、履歴更新(DataRowState.Unchanged、DataRowState.Modified)
                    S04Service.UpdateProductHistory(history, hstDic);
                }
                // No.156-4 Mod End

            }

        }
        #endregion

        #region 移動ヘッダ情報の返品更新
        /// <summary>
        /// 移動ヘッダ情報(返品)の更新処理をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="hdRow">売上ヘッダ行</param>
        /// <param name="userId">ログインユーザID</param>
        private void setT05_IDOHD_Returns_Update(T02_URHD urhd)
        {
            /**
             * 通常時とは出荷元・出荷先が反転するので別途メソッドを作成
             **/
            T05_IDOHD idohd = new T05_IDOHD();

            idohd.伝票番号 = urhd.伝票番号;
            idohd.会社名コード = urhd.会社名コード;
            idohd.日付 = urhd.売上日;
            idohd.移動区分 = (int)CommonConstants.移動区分.売上移動;
            idohd.出荷元倉庫コード = getStockpileFromJis(urhd.会社名コード);
            idohd.出荷先倉庫コード = urhd.在庫倉庫コード;

            T05Service.T05_IDOHD_Update(idohd);

        }
        #endregion


        #region 販社の入出庫履歴の生成

        /// <summary>
        /// 販社の入出庫履歴の登録・更新をおこなう
        /// </summary>
        /// <param name="urhd"></param>
        /// <param name="dtlTbl"></param>
        /// <param name="orghd">編集前ヘッダーデータ</param>
        /// <param name="isNonCheckItemWithout">マルセン仕入チェックが無いものを除外するか</param>
        private void setS04_HISTORY_HANReturns_Update(T02_URHD urhd, DataTable dtlTbl, DataRow orghd, bool isNonCheckItemWithout)
        {

            foreach (DataRow row in dtlTbl.Rows)
            {
                // 売上明細データ取得
                T02_URDTL urdtl = convertDataRowToT02_URDTL_Entity(row);

                // 商品未設定レコードは処理しない
                if (urdtl.品番コード <= 0)
                    continue;

                bool bval,
                  仕入チェック = bool.TryParse(row["マルセン仕入"].ToString(), out bval) ? bval : false;

                // チェック判定アリかつチェックボックス=オフの場合は登録処理をスキップする
                if (isNonCheckItemWithout && 仕入チェック == false)
                    continue;

                #region 販社からの出庫
                S04_HISTORY history = new S04_HISTORY();
                history.入出庫日 = urhd.売上日;    // 返品日
                history.入出庫時刻 = com.GetDbDateTime().TimeOfDay;
                history.倉庫コード = getStockpileFromJis(urhd.会社名コード);
                history.入出庫区分 = (int)CommonConstants.入出庫区分.ID02_出庫;
                history.品番コード = urdtl.品番コード;
                history.賞味期限 = urdtl.賞味期限;
                history.数量 = Math.Abs(decimal.ToInt32(urdtl.数量));
                history.伝票番号 = urhd.伝票番号;

                Dictionary<string, string> hstDic = new Dictionary<string, string>()
                    {
                        // No.156-3 Mod Start
                        { S04.COLUMNS_NAME_入出庫日, orghd == null ?
                                                        history.入出庫日.ToString("yyyy/MM/dd") : string.Format("{0:yyyy/MM/dd}", orghd["売上日", DataRowVersion.Original]) },
                        // No.156-3 Mod End
                        { S04.COLUMNS_NAME_倉庫コード, history.倉庫コード.ToString() },
                        { S04.COLUMNS_NAME_伝票番号, history.伝票番号.ToString() },
                        { S04.COLUMNS_NAME_品番コード, history.品番コード.ToString() }
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
                // No.156-4 Mod STart
                else
                {
                    // 売上更新の為、履歴更新(DataRowState.Unchanged、DataRowState.Modified))
                    S04Service.UpdateProductHistory(history, hstDic);
                }
                // No.156-4 Mod End
                #endregion

                #region 自社の入庫
                history = new S04_HISTORY();
                history.入出庫日 = urhd.売上日;    // 返品日
                history.入出庫時刻 = com.GetDbDateTime().TimeOfDay;
                history.倉庫コード = urhd.在庫倉庫コード;
                history.入出庫区分 = (int)CommonConstants.入出庫区分.ID01_入庫;
                history.品番コード = urdtl.品番コード;
                history.賞味期限 = urdtl.賞味期限;
                history.数量 = Math.Abs(decimal.ToInt32(urdtl.数量));
                history.伝票番号 = urhd.伝票番号;

                hstDic = new Dictionary<string, string>()
                    {
                        //{ S04.COLUMNS_NAME_入出庫日, history.入出庫日.ToString("yyyy/MM/dd") },
                        // No.156-3 Mod Start
                        { S04.COLUMNS_NAME_入出庫日, orghd == null ?
                                                        history.入出庫日.ToString("yyyy/MM/dd") : string.Format("{0:yyyy/MM/dd}", orghd["売上日", DataRowVersion.Original]) },
                        // No.156-3 Mod End
                        { S04.COLUMNS_NAME_倉庫コード, history.倉庫コード.ToString()},
                        { S04.COLUMNS_NAME_伝票番号, history.伝票番号.ToString() },
                        { S04.COLUMNS_NAME_品番コード, history.品番コード.ToString() }
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
                // No.156-4 Mod Start
                else
                {
                    // 売上更新の為、履歴更新(DataRowState.Unchanged、DataRowState.Modified)
                    S04Service.UpdateProductHistory(history, hstDic);
                }
                // No.156-4 Mod End
                #endregion

            }
        }
        #endregion

        #region << 処理関連 >>

        #region 出荷元コードから倉庫取得
        /// <summary>
        /// 出荷元コードから対象の倉庫コードを取得する
        /// </summary>
        /// <param name="出荷元コード"></param>
        /// <returns></returns>
        private int get出荷先倉庫コード(int 出荷元コード)
        {
            if (出荷元コード == null)
                return -1;

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                var code =
                    context.M70_JIS.Where(w => w.自社コード == 出荷元コード)
                        .Join(context.M22_SOUK.Where(w => w.削除日時 == null && w.場所会社コード == w.寄託会社コード),
                            x => x.自社コード,
                            y => y.場所会社コード,
                            (x, y) => new { JIS = x, SOUK = y })
                        .Select(s => s.SOUK.倉庫コード)
                        .FirstOrDefault();

                return code;

            }

        }

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

        // No-97 Add Start
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
        // No-97 Add End

        #endregion

        #endregion

    }

}
