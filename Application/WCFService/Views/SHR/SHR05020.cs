using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 支払一覧表サービスクラス
    /// </summary>
    public class SHR05020
    {
        #region 定数定義

        //変更する場合は、DataSetのテーブル名と同じ名称にする必要があります。
        /// <summary>請求書印刷 ヘッダーテーブル名</summary>
        private const string PRINT_HEADER_TABLE_NAME = "SHR05021_H支払明細書";
        /// <summary>請求書印刷 明細テーブル名</summary>
        private const string PRINT_DETAIL_TABLE_NAME = "SHR05021_D支払明細書";

        #endregion

        #region 項目クラス定義

        /// <summary>
        /// T12_PAYHD + DTL 項目定義クラス
        /// </summary>
        public class T12_PAY_Data
        {
            public int 自社コード { get; set; }
            public int 得意先コード { get; set; }
            public int 得意先枝番 { get; set; }
            public long 出金額 { get; set; }
        }
        /// <summary>
        /// S01_SHRHD_Data + 自社名
        /// </summary>
        public class S02_SHRHD_Data
        {
            public int 自社コード { get; set; }
            public string 自社名 { get; set; }
            public int 支払年月 { get; set; }
            public int 支払締日 { get; set; }
            public int 支払先コード { get; set; }
            public int 支払先枝番 { get; set; }
            public int 支払日 { get; set; }
            public int 回数 { get; set; }
            public DateTime? 集計開始日 { get; set; }
            public DateTime? 集計最終日 { get; set; }
            public long 前月残高 { get; set; }
            public long 前回支払額 { get; set; }
            public long 今回出金額 { get; set; }
            public long 出金額 { get; set; }
            public long 繰越残高 { get; set; }
            public long 値引額 { get; set; }
            public long 非課税支払額 { get; set; }
            public long 支払額 { get; set; }
            public long 通常消費税 { get; set; }
            public long 軽減消費税 { get; set; }
            public long 消費税 { get; set; }
            public long 当月支払額 { get; set; }

            public decimal 通常税率対象金額 { get; set; }
            public decimal 軽減税率対象金額 { get; set; }
            public decimal 通常税率消費税 { get; set; }
            public decimal 軽減税率消費税 { get; set; }
        }
        /// <summary>
        /// SHR05010_支払一覧表帳票項目定義（簡易）
        /// </summary>
        private class PrintMember
        {
            public int 自社コード { get; set; }      // No.277,288 Add
            public string 自社名 { get; set; }       // No.277,288 Add
            public string 仕入先コード { get; set; }
            public string 仕入先名称 { get; set; }
            public string 支払締日 { get; set; }
            public DateTime 仕入日 { get; set; }
            public int 品番コード { get; set; }
            public string 自社品番 { get; set; }
            public string 色コード { get; set; }
            public string 品名 { get; set; }
            public decimal 単価 { get; set; }
            public decimal 数量 { get; set; }
            public int 金額 { get; set; }
            public int 通常消費税 { get; set; }
            public int 軽減消費税 { get; set; }
            public string 摘要 { get; set; }         // No.336 Add
        }

        #endregion

        #region 支払明細書帳票（詳細）

        private class PrintHeaderMember
        {
            /// <summary>
            /// 請求先で改ページさせる為のキー文字列
            /// </summary>
            public string PagingKey { get; set; }
            public string 自社コード { get; set; }
            public string 支払年月 { get; set; }
            public string 支払先コード { get; set; }
            public string 支払先枝番 { get; set; }
            public string 得意先コード { get; set; }
            public string 得意先枝番 { get; set; }
            public int 回数 { get; set; }

            public int 支払年 { get; set; }
            public int 支払月 { get; set; }
            public string 支払先郵便番号 { get; set; }
            public string 支払先住所１ { get; set; }
            public string 支払先住所２ { get; set; }
            public string 得意先名称 { get; set; }
            public string 得意先名称２ { get; set; }
            public string 得意先部課名称 { get; set; }
            public string 自社名称 { get; set; }
            public string 自社郵便番号 { get; set; }
            public string 自社住所 { get; set; }
            public string 自社TEL { get; set; }
            public string 自社FAX { get; set; }
            public string 締日 { get; set; }
            public string 発行日付 { get; set; }
            public decimal 支払額 { get; set; }
            public decimal 消費税S { get; set; }

            public decimal 前回支払額 { get; set; }
            public decimal 今回出金額 { get; set; }
            public decimal 繰越残高 { get; set; }
            public decimal 今回支払額 { get; set; }

            public decimal 通常税率対象金額 { get; set; }
            public decimal 軽減税率対象金額 { get; set; }
            public decimal 通常税率消費税 { get; set; }
            public decimal 軽減税率消費税 { get; set; }
            public decimal 非税売上額 { get; set; }
            public DateTime? 集計最終日 { get; set; }
            public int 消費税率 { get; set; }
            public int 軽減税率 { get; set; }
        }

        /// <summary>
        /// TKS01020_D請求書
        /// </summary>
        private class PrintDetailMember
        {
            public string PagingKey { get; set; }
            public string 自社コード { get; set; }
            public string 支払年月 { get; set; }
            public string 支払先コード { get; set; }
            public string 支払先枝番 { get; set; }
            public string 得意先コード { get; set; }
            public string 得意先枝番 { get; set; }
            public int 回数 { get; set; }

            public int 伝票番号 { get; set; }
            public string 仕入日 { get; set; }
            public string 自社品番 { get; set; }
            public string 相手品番 { get; set; }
            public string 品番名称 { get; set; }
            public decimal 数量 { get; set; }
            public decimal 単価 { get; set; }
            public int 金額 { get; set; }
            /// <summary>
            /// 軽減税率適用の場合に「*」を設定
            /// </summary>
            public string 軽減税率適用 { get; set; }
            public string 摘要 { get; set; }

        }
        #endregion

        #region CSV出力データ取得
        /// <summary>
        /// ＣＳＶ出力データを取得する
        /// </summary>
        /// <param name="condition">
        ///  == 検索条件 ==
        /// 自社コード
        /// 作成年月
        /// 作成締日
        /// 全締日
        /// 得意先コード
        /// 得意先枝番
        /// 作成区分
        /// </param>
        /// <returns></returns>
        public DataTable GetCsvData(Dictionary<string, string> condition)
        {
            DataTable dt = getCommonData(condition);

            return dt;

        }
        #endregion

        #region 帳票出力データ取得
        /// <summary>
        /// 帳票出力データを取得する
        /// </summary>
        /// <param name="condition">
        ///  == 検索条件 ==
        /// 自社コード
        /// 作成年月
        /// 作成締日
        /// 全締日
        /// 得意先コード
        /// 得意先枝番
        /// 作成区分
        /// </param>
        /// <returns></returns>
        public DataTable GetPrintData(Dictionary<string, string> condition)
        {
            DataTable dt = getCommonData(condition);

            return dt;

        }
        #endregion

        #region 支払一覧表の基本情報を取得
        /// <summary>
        /// 支払一覧表の基本情報を取得する
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private DataTable getCommonData(Dictionary<string, string> condition)
        {
            // 検索パラメータを展開
            int myCompany, createYearMonth;
            int? closingDay, customerCode, customerEda;
            bool isAllDays;

            getFormParams(condition, out myCompany, out createYearMonth, out closingDay, out isAllDays, out customerCode, out customerEda);

            // ベースとなる支払ヘッダ情報を取得
            List<S02_SHRHD_Data> hdList = getHeaderData(condition);
            var hdData =
                hdList.GroupBy(g => new { g.自社コード, g.自社名, g.支払年月, g.支払締日, g.支払先コード, g.支払先枝番, g.通常消費税, g.軽減消費税 })
                    .Select(s => new
                    {
                        s.Key.自社コード,
                        s.Key.自社名,          // No.227,228 Add
                        s.Key.支払年月,
                        s.Key.支払締日,
                        s.Key.支払先コード,
                        s.Key.支払先枝番,
                        s.Key.通常消費税,
                        s.Key.軽減消費税
                    });

            // 詳細データ件数を取得
            List<S02_SHRDTL> dtlList = getDetailData(condition);
            var dtlData =
                dtlList.Select(s => new
                    {
                        s.自社コード,
                        s.支払年月,
                        s.支払締日,
                        s.支払先コード,
                        s.支払先枝番,
                        s.仕入日,              // No.326 Add
                        s.品番コード,
                        s.自社品名,            // No.390 Add
                        s.単価,
                        s.数量,
                        s.金額,
                        s.摘要,                // No.336 Add
                    });


            // 取引先情報を取得
            List<M01_TOK> tokList = getTokData(hdList);

            // 品番情報を取得
            List<M09_HIN> hinList = getHinData(dtlList);

            // 取得データを統合して結果リストを作成
            var resultList =
                hdData.GroupJoin(dtlData,
                    x => new { x.自社コード, x.支払年月, x.支払締日, x.支払先コード, x.支払先枝番 },
                    y => new { y.自社コード, y.支払年月, y.支払締日, y.支払先コード, y.支払先枝番 },
                    (x, y) => new { x, y })
                .SelectMany(x => x.y,
                    (a, b) => new { NHD = a.x, NDTL = b })
                .GroupJoin(hinList,
                    x => new { x.NDTL.品番コード },
                    y => new { y.品番コード },
                    (x, y) => new { x, y })
                .SelectMany(x => x.y,
                    (c, d) => new { c.x.NHD, c.x.NDTL, HIN = d })
                .GroupJoin(tokList,
                    x => new { コード = x.NHD.支払先コード, 枝番 = x.NHD.支払先枝番 },
                    y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                    (x, y) => new { x, y })
                .SelectMany(x => x.y,
                    (e, f) => new { e.x.NHD, e.x.NDTL, e.x.HIN, TOK = f })
                .OrderBy(o => o.NHD.支払先コード)
                .ThenBy(t => t.NHD.支払先枝番)
                .ToList()
                .Select(x => new PrintMember
                {
                    自社コード = x.NHD.自社コード,        // No.277,288 Add
                    自社名 = x.NHD.自社名,                // No.277,288 Add
                    仕入先コード = string.Format("{0:D4} - {1:D2}", x.NHD.支払先コード, x.NHD.支払先枝番),      // No-150, No.223 Mod
                    仕入先名称 = x.TOK == null ? "" : x.TOK.略称名,   // No.326 Mod
                    支払締日 = x.NHD.支払締日.ToString(),
                    仕入日 = x.NDTL.仕入日,                // No.326 Mod
                    品番コード = x.NDTL.品番コード,
                    自社品番 = x.HIN.自社品番,
                    色コード = x.HIN.自社色,
                    //品名 = x.HIN.自社品名,
                    品名 = !string.IsNullOrEmpty(x.NDTL.自社品名) ? x.NDTL.自社品名 : x.HIN.自社品名,           // No.390 Add
                    単価 = x.NDTL.単価,
                    数量 = x.NDTL.数量,
                    金額 = x.NDTL.金額,
                    通常消費税 = (int)x.NHD.通常消費税,
                    軽減消費税 = (int)x.NHD.軽減消費税,
                    摘要 = x.NDTL.摘要,                    // No.336 Add
                });

            return KESSVCEntry.ConvertListToDataTable<PrintMember>(resultList.ToList());

        }
        #endregion

        #region パラメータ展開
        /// <summary>
        /// フォームパラメータを展開する
        /// </summary>
        /// <param name="condition">検索条件辞書</param>
        /// <param name="myCompany">自社コード</param>
        /// <param name="createYearMonth">作成年月(yyyymm)</param>
        /// <param name="closingDay">締日</param>
        /// <param name="isAllDays">全締日を対象とするか</param>
        /// <param name="customerCode">得意先コード</param>
        /// <param name="customerEda">得意先枝番</param>
        /// <param name="createType">作成区分</param>
        private void getFormParams(
            Dictionary<string, string> condition,
            out int myCompany,
            out int createYearMonth,
            out int? closingDay,
            out bool isAllDays,
            out int? customerCode,
            out int? customerEda)
        {
            int ival = -1;

            myCompany = int.Parse(condition["自社コード"]);
            createYearMonth = int.Parse(condition["作成年月"].Replace("/", ""));
            closingDay = int.TryParse(condition["作成締日"], out ival) ? ival : (int?)null;
            isAllDays = bool.Parse(condition["全締日"]);
            customerCode = int.TryParse(condition["得意先コード"], out ival) ? ival : (int?)null;
            customerEda = int.TryParse(condition["得意先枝番"], out ival) ? ival : (int?)null;
        }
        #endregion

        #region 支払ヘッダ情報取得
        /// <summary>
        /// 対象の支払ヘッダ情報を取得する
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private List<S02_SHRHD_Data> getHeaderData(Dictionary<string, string> condition)
        {
            // 検索パラメータを展開
            int myCompany, createYearMonth;
            int? closingDay, customerCode, customerEda;
            bool isAllDays;

            getFormParams(condition, out myCompany, out createYearMonth, out closingDay, out isAllDays, out customerCode, out customerEda);

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // No.227,228 Mod Start
                var shd =
                    context.S02_SHRHD.Where(w => w.自社コード == myCompany && w.支払年月 == createYearMonth)
                    .GroupJoin(context.M70_JIS.Where(x => x.削除日時 == null),
                        x => x.自社コード,
                        y => y.自社コード,
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(),
                        (a, b) => new { SHRHD = a.x, JIS = b })
                    .Select(x => new S02_SHRHD_Data
                    {
                        自社コード = x.SHRHD.自社コード,
                        自社名 = x.JIS.自社名,
                        支払年月 = x.SHRHD.支払年月,
                        支払締日 = x.SHRHD.支払締日,
                        支払先コード = x.SHRHD.支払先コード,
                        支払先枝番 = x.SHRHD.支払先枝番,
                        支払日 = x.SHRHD.支払日,
                        前回支払額 = x.SHRHD.前月残高,
                        今回出金額 = x.SHRHD.出金額,
                        回数 = x.SHRHD.回数,
                        集計開始日 = x.SHRHD.集計開始日,
                        集計最終日 = x.SHRHD.集計最終日,
                        前月残高 = x.SHRHD.前月残高,
                        出金額 = x.SHRHD.出金額,
                        繰越残高 = x.SHRHD.繰越残高,
                        値引額 = x.SHRHD.値引額,
                        非課税支払額 = x.SHRHD.非課税支払額,
                        支払額 = x.SHRHD.支払額,
                        通常消費税 = x.SHRHD.通常税率消費税,
                        軽減消費税 = x.SHRHD.軽減税率消費税,
                        当月支払額 = x.SHRHD.当月支払額,
                        通常税率対象金額 = x.SHRHD.通常税率対象金額,
                        軽減税率対象金額 = x.SHRHD.軽減税率対象金額,
                        通常税率消費税 = x.SHRHD.通常税率消費税,
                        軽減税率消費税 = x.SHRHD.軽減税率消費税,
                        消費税 = x.SHRHD.消費税,

                    });
                // No.227,228 Mod End

                if (!isAllDays)
                    shd = shd.Where(w => w.支払締日 == closingDay);

                if (customerCode != null && customerEda != null)
                    shd = shd.Where(w => w.支払先コード == customerCode && w.支払先枝番 == customerEda);


                return shd.ToList();

            }

        }
        #endregion

        #region 支払明細情報取得
        /// <summary>
        /// 対象の支払明細情報を取得する
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private List<S02_SHRDTL> getDetailData(Dictionary<string, string> condition)
        {
            // 検索パラメータを展開
            int myCompany, createYearMonth;
            int? closingDay, customerCode, customerEda;
            bool isAllDays;

            getFormParams(condition, out myCompany, out createYearMonth, out closingDay, out isAllDays, out customerCode, out customerEda);

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                var sdtl =
                    context.S02_SHRDTL.Where(w => w.自社コード == myCompany && w.支払年月 == createYearMonth);

                if (!isAllDays)
                    sdtl = sdtl.Where(w => w.支払締日 == closingDay);

                if (customerCode != null && customerEda != null)
                    sdtl = sdtl.Where(w => w.支払先コード == customerCode && w.支払先枝番 == customerEda);

                return sdtl.ToList();

            }

        }
        #endregion

        #region 取引先情報取得
        /// <summary>
        /// 対象の取引先情報を取得する
        /// </summary>
        /// <param name="hdList"></param>
        /// <returns></returns>
        private List<M01_TOK> getTokData(List<S02_SHRHD_Data> hdList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                var tokList =
                    context.M01_TOK.Where(w => w.削除日時 == null)
                        .Join(hdList,
                            x => new { コード = x.取引先コード, 枝番 = x.枝番 },
                            y => new { コード = y.支払先コード, 枝番 = y.支払先枝番 },
                            (x, y) => new { TOK = x, SHD = y })
                        .Select(x => x.TOK);


                var wkTOK =
                    context.M01_TOK.Where(w => w.削除日時 == null)
                        .ToList()
                        .Where(w => hdList.Exists(v => v.支払先コード == w.取引先コード && v.支払先枝番 == w.枝番));

                return wkTOK.ToList();

            }

        }
        #endregion

        #region 品番情報取得
        /// <summary>
        /// 対象の取引先情報を取得する
        /// </summary>
        /// <param name="hdList"></param>
        /// <returns></returns>
        private List<M09_HIN> getHinData(List<S02_SHRDTL> hdList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                //var tokList =
                //    context.M09_HIN.Where(w => w.削除日時 == null)
                //        .Join(hdList,
                //            x => new { コード = x.取引先コード, 枝番 = x.枝番 },
                //            y => new { コード = y.支払先コード, 枝番 = y.支払先枝番 },
                //            (x, y) => new { TOK = x, SHD = y })
                //        .Select(x => x.TOK);


                var wkHIN =
                    context.M09_HIN.Where(w => w.削除日時 == null)
                        .ToList()
                        .Where(w => hdList.Exists(v => v.品番コード == w.品番コード));

                return wkHIN.ToList();

            }

        }
        #endregion

        #region 請求書印字データ取得
        /// <summary>
        /// 請求書印字データを取得する
        /// </summary>
        /// <param name="condition">
        ///  == 検索条件 ==
        ///  自社コード
        ///  作成年月日
        ///  作成年月
        ///  作成締日
        ///  得意先コード
        ///  得意先枝番
        /// </param>
        /// <param name="ds">
        /// [0]請求一覧データ
        /// </param>
        /// <returns></returns>
        public DataSet GetPrintDetail(Dictionary<string, string> condition)
        {
            DataSet dsResult = new DataSet();

            // パラメータの型変換
            DateTime printDate = DateTime.Parse(condition["作成年月日"]);

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {

                // 対象の請求ヘッダを取得
                List<S02_SHRHD_Data> hdList = getHeaderData(condition);


                #region 帳票ヘッダ情報取得
                var hdResult = hdList
                        .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                            x => new { コード = x.支払先コード, 枝番 = x.支払先枝番 },
                            y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(),
                            (a, b) => new { SHRHD = a.x, TOK = b })
                        .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                            x => x.SHRHD.自社コード,
                            y => y.自社コード,
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(),
                            (c, d) => new { c.x.SHRHD, c.x.TOK, JIS = d })
                        .OrderBy(o => o.SHRHD.支払先コード)
                        .ThenBy(t => t.SHRHD.支払先枝番)
                        .ToList()
                    .Select(x => new PrintHeaderMember
                    {
                        PagingKey = string.Concat(x.SHRHD.支払先コード, "-", x.SHRHD.支払先枝番, "-", x.SHRHD.支払日, ">", x.SHRHD.回数),
                        自社コード = x.SHRHD.自社コード.ToString(),
                        支払年月 = x.SHRHD.支払年月.ToString(),
                        支払先コード = x.SHRHD.支払先コード.ToString(),
                        支払先枝番 = x.SHRHD.支払先枝番.ToString(),
                        得意先コード = string.Format("{0:D4}", x.SHRHD.支払先コード),   // No.223 Mod
                        得意先枝番 = string.Format("{0:D2}", x.SHRHD.支払先枝番),       // No.233 Mod
                        回数 = x.SHRHD.回数,
                        支払年 = x.SHRHD.支払年月 / 100,
                        支払月 = x.SHRHD.支払年月 % 100,
                        支払先郵便番号 = x.TOK.郵便番号,
                        支払先住所１ = x.TOK.住所１,
                        支払先住所２ = x.TOK.住所２,
                        得意先名称 = x.TOK.得意先名１,
                        得意先名称２ = x.TOK.得意先名２,
                        得意先部課名称 = x.TOK.部課名称,
                        自社名称 = x.JIS.自社名,
                        自社郵便番号 = x.JIS.郵便番号,
                        自社住所 = x.JIS.住所１.Trim() + x.JIS.住所２.Trim(),
                        自社TEL = x.JIS.電話番号,
                        自社FAX = x.JIS.ＦＡＸ,
                        締日 = (x.TOK.Ｓ締日 >= 31) ? "末" : x.TOK.Ｓ締日.ToString(),
                        発行日付 = printDate.ToString("yyyy/MM/dd"),

                        支払額 = x.SHRHD.支払額,
                        消費税S = x.SHRHD.消費税,
                        今回支払額 = x.SHRHD.支払額 + x.SHRHD.消費税,
                        前回支払額 = x.SHRHD.前回支払額,
                        今回出金額 = x.SHRHD.今回出金額,
                        繰越残高 = x.SHRHD.繰越残高,
                        通常税率対象金額 = x.SHRHD.通常税率対象金額,
                        軽減税率対象金額 = x.SHRHD.軽減税率対象金額,
                        通常税率消費税 = x.SHRHD.通常税率消費税,
                        軽減税率消費税 = x.SHRHD.軽減税率消費税,
                        非税売上額 = x.SHRHD.非課税支払額,
                        集計最終日 = x.SHRHD.集計最終日
                    });
                #endregion

                #region 帳票明細情報取得


                // 対象の請求ヘッダを取得
                List<S02_SHRDTL> dtlList = getDetailData(condition);

                var dtlResult =
                    dtlList.GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                            x => x.品番コード,
                            y => y.品番コード,
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(),
                            (a, b) => new { SDTL = a.x, HIN = b })
                        .GroupJoin(context.M10_TOKHIN.Where(w => w.削除日時 == null),
                            x => new { コード = x.SDTL.支払先コード, 枝番 = x.SDTL.支払先枝番 },
                            y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(),
                            (c, d) => new { c.x.SDTL, c.x.HIN, TOKHIN = d })
                        .OrderBy(o => o.SDTL.支払先コード)
                        .ThenBy(o => o.SDTL.支払先枝番)
                        .ToList()
                        .Select(x => new PrintDetailMember
                        {
                            PagingKey = string.Concat(x.SDTL.支払先コード, "-", x.SDTL.支払先枝番, "-", x.SDTL.支払日, ">", x.SDTL.回数),
                            自社コード = x.SDTL.自社コード.ToString(),
                            支払年月 = x.SDTL.支払年月.ToString(),
                            支払先コード = x.SDTL.支払先コード.ToString(),
                            支払先枝番 = x.SDTL.支払先枝番.ToString(),
                            得意先コード = string.Format("{0:D4}", x.SDTL.支払先コード),
                            得意先枝番 = string.Format("{0:D2}", x.SDTL.支払先枝番),
                            回数 = x.SDTL.回数,
                            
                            伝票番号 = x.SDTL.伝票番号,
                            仕入日 = x.SDTL.仕入日.ToString("yyyy/MM/dd"),
                            自社品番 = x.HIN.自社品番,
                            相手品番 = x.TOKHIN == null ? "" : x.TOKHIN.得意先品番コード,
                            品番名称 = !string.IsNullOrEmpty(x.SDTL.自社品名) ? x.SDTL.自社品名 : x.HIN.自社品名,     // No.389 Mod
                            数量 = x.SDTL.数量,
                            単価 = x.SDTL.単価,
                            金額 = x.SDTL.金額,

                            //20190902 CB mod - s 軽減税率対応
                            //軽減税率適用 = x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? "*" : ""
                            軽減税率適用 = x.SDTL.伝票区分 != (int)CommonConstants.支払伝票区分.仕入伝票 ? "" : (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? "軽"
                                            : x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.非課税 ? "非" : ""),
                            //20190902 CB mod - e
                            摘要 = x.SDTL.摘要
                        });

                #endregion

                #region 期間内の出金明細
                foreach (var mem in hdList.ToList())
                {
                    var shukinDtl =
                        context.T12_PAYHD.Where(c =>
                                    c.出金元自社コード == mem.自社コード &&
                                        c.得意先コード == mem.支払先コード &&
                                        c.得意先枝番 == mem.支払先枝番 &&
                                    c.出金日 >= mem.集計開始日 &&
                                    c.出金日 <= mem.集計最終日 &&
                                    c.削除日時 == null
                                    )
                                    .GroupJoin(context.T12_PAYDTL.Where(c => c.削除日時 == null),
                                        x => x.伝票番号,
                                        y => y.伝票番号,
                                        (x, y) => new { x, y })
                                    .SelectMany(x => x.y.DefaultIfEmpty(),
                                        (a, b) => new { HD = a.x, DTL = b })
                                    .GroupJoin(context.M99_COMBOLIST.Where(c =>
                                        c.分類 == "随時" &&
                                        c.機能 == "入金問合せ" &&
                                        c.カテゴリ == "金種"),
                                        x => x.DTL.金種コード,
                                        y => y.コード,
                                        (x, y) => new { x, y })
                                        .SelectMany(x => x.y.DefaultIfEmpty(),
                                        (a, b) => new { SHU = a.x, CMB = b })
                                    .ToList()
                                    .Select(x => new PrintDetailMember
                                    {
                                        PagingKey = string.Concat(mem.支払先コード, "-", mem.支払先枝番, "-", mem.支払日, ">", mem.回数),
                                        自社コード = mem.自社コード.ToString(),
                                        支払年月 = mem.支払年月.ToString(),
                                        支払先コード = mem.支払先コード.ToString(),
                                        支払先枝番 = mem.支払先枝番.ToString(),
                                        得意先コード = string.Format("{0:D4}", mem.支払先コード),   // No.223 Mod
                                        得意先枝番 = string.Format("{0:D2}", mem.支払先枝番),       // No.223 Mod
                                        回数 = mem.回数,

                                        伝票番号 = x.SHU.HD.伝票番号,              // No-181 Mod
                                        仕入日 = x.SHU.HD.出金日.ToString("yyyy/MM/dd"),
                                        自社品番 = string.Empty,
                                        相手品番 = string.Empty,
                                        品番名称 = x.CMB.表示名 == null ? string.Empty : x.CMB.表示名,
                                        数量 = 0,
                                        単価 = 0,
                                        金額 = x.SHU.DTL.金額,

                                        軽減税率適用 = "",
                                        摘要 = x.SHU.DTL.摘要,
                                    });


                    //売上日→伝票番号の順でソート
                    dtlResult = dtlResult.Concat(shukinDtl).OrderBy(o => o.仕入日).ThenBy(o => o.伝票番号);
                }


                #endregion
                //S01_SHRHDの集計最終日を基準としてM73_ZEIから税率を取得
                DataTable dt;
                dt = KESSVCEntry.ConvertListToDataTable<PrintHeaderMember>(hdResult.AsQueryable().ToList());

                M73 M73Service;
                M73Service = new M73();

                foreach (DataRow dr in dt.Rows)
                {
                    // drを使った処理(カラムにアクセスする場合は dr["カラム名"]と表記)
                    DateTime? DateTimeWk = (DateTime)dr["集計最終日"];

                    if (DateTimeWk != null)
                    {
                        //共通関数仕様　+1日
                        DateTime answer = (DateTime)DateTimeWk;
                        answer = answer.AddDays(1);

                        List<M73.M73_ZEI_Member> lstM73 = M73Service.GetData(answer, -1);

                        dr["軽減税率"] = lstM73[0].軽減税率;
                        dr["消費税率"] = lstM73[0].消費税率;
                    }
                }
                DataTable hdDt = dt;

                hdDt.TableName = PRINT_HEADER_TABLE_NAME;
                DataTable dtlDt = KESSVCEntry.ConvertListToDataTable<PrintDetailMember>(dtlResult.AsQueryable().ToList());
                dtlDt.TableName = PRINT_DETAIL_TABLE_NAME;

                if (dsResult.Tables.Contains(hdDt.TableName))
                {
                    // ２件目以降
                    dsResult.Tables[PRINT_HEADER_TABLE_NAME].Merge(hdDt);
                    dsResult.Tables[PRINT_DETAIL_TABLE_NAME].Merge(dtlDt);

                }
                else
                {
                    // １件目
                    dsResult.Tables.Add(hdDt);
                    dsResult.Tables.Add(dtlDt);

                }
            }

            return dsResult;

        }
        #endregion
    }

}
