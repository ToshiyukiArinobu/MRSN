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
    public class SHR05010
    {

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
        /// SHR05010_支払一覧表帳票項目定義
        /// </summary>
        private class PrintMember
        {
            public string 仕入先コード { get; set; }
            public string 仕入先名称 { get; set; }
            public string 支払締日 { get; set; }
            public long 前月残高 { get; set; }
            public long 出金金額 { get; set; }
            public long 支払額 { get; set; }
            public long 値引額 { get; set; }
            public long 非課税支払額 { get; set; }
            public long 消費税 { get; set; }
            public long 当月支払額 { get; set; }
            public long 件数 { get; set; }
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
            int myCompany, createYearMonth, createType;
            int? closingDay, customerCode, customerEda;
            bool isAllDays;

            getFormParams(condition, out myCompany, out createYearMonth, out closingDay, out isAllDays, out customerCode, out customerEda, out createType);

            // ベースとなる支払ヘッダ情報を取得
            List<S02_SHRHD> hdList = getHeaderData(condition);
            var hdData =
                hdList.GroupBy(g => new { g.自社コード, g.支払年月, g.支払締日, g.支払先コード, g.支払先枝番 })
                    .Select(s => new
                    {
                        s.Key.自社コード,
                        s.Key.支払年月,
                        s.Key.支払締日,
                        s.Key.支払先コード,
                        s.Key.支払先枝番,
                        前月残高 = s.Sum(m => m.前月残高),
                        売上額 = s.Sum(m => m.支払額),
                        値引額 = s.Sum(m => m.値引額),
                        非税売上額 = s.Sum(m => m.非課税支払額),
                        消費税 = s.Sum(m => m.消費税),
                        当月支払額 = s.Sum(m => m.当月支払額)
                    });

            // 詳細データ件数を取得
            List<S02_SHRDTL> dtlList = getDetailData(condition);
            var dtlData =
                dtlList.GroupBy(g => new { g.自社コード, g.支払年月, g.支払締日, g.支払先コード, g.支払先枝番 })
                    .Select(s => new
                    {
                        s.Key.自社コード,
                        s.Key.支払年月,
                        s.Key.支払締日,
                        s.Key.支払先コード,
                        s.Key.支払先枝番,
                        件数 = s.Count()
                    });

            // 入金データを取得(金額集計済)
            List<T12_PAY_Data> payList = getPayData(createYearMonth / 100, createYearMonth % 100, hdList);

            // 取引先情報を取得
            List<M01_TOK> tokList = getTokData(hdList);

            // 取得データを統合して結果リストを作成
            var resultList =
                hdData.GroupJoin(dtlData,
                    x => new { x.自社コード, x.支払先コード, x.支払先枝番 },
                    y => new { y.自社コード, y.支払先コード, y.支払先枝番 },
                    (x, y) => new { x, y })
                .SelectMany(x => x.y.DefaultIfEmpty(),
                    (a, b) => new { NHD = a.x, NDTL = b })
                .GroupJoin(payList,
                    x => new { x.NHD.自社コード, コード = x.NHD.支払先コード, 枝番 = x.NHD.支払先枝番 },
                    y => new { y.自社コード, コード = y.得意先コード, 枝番 = y.得意先枝番 },
                    (x, y) => new { x, y })
                .SelectMany(x => x.y.DefaultIfEmpty(),
                    (c, d) => new { c.x.NHD, c.x.NDTL, PAY = d })
                .GroupJoin(tokList,
                    x => new { コード = x.NHD.支払先コード, 枝番 = x.NHD.支払先枝番 },
                    y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                    (x, y) => new { x, y })
                .SelectMany(x => x.y.DefaultIfEmpty(),
                    (e, f) => new { e.x.NHD, e.x.NDTL, e.x.PAY, TOK = f })
                .OrderBy(o => o.NHD.支払先コード)
                .ThenBy(t => t.NHD.支払先枝番)
                .ToList()
                .Select(x => new PrintMember
                {
                    仕入先コード = string.Format("{0:000} - {1:00}", x.NHD.支払先コード, x.NHD.支払先枝番),      // No-149 Mod
                    仕入先名称 = x.TOK == null ? "" : x.TOK.得意先名１,
                    支払締日 = x.NHD.支払締日.ToString(),
                    前月残高 = x.NHD.前月残高,
                    出金金額 = x.PAY == null ? 0 : x.PAY.出金額,
                    支払額 = x.NHD.売上額,
                    値引額 = x.NHD.値引額,
                    非課税支払額 = x.NHD.非税売上額,
                    消費税 = x.NHD.消費税,
                    当月支払額 = x.NHD.当月支払額,
                    件数 = x.NDTL == null ? 0 : x.NDTL.件数
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
            out int? customerEda,
            out int createType )
        {
            int ival = -1;

            myCompany = int.Parse(condition["自社コード"]);
            createYearMonth = int.Parse(condition["作成年月"].Replace("/", ""));
            closingDay = int.TryParse(condition["作成締日"], out ival) ? ival : (int?)null;
            isAllDays = bool.Parse(condition["全締日"]);
            customerCode = int.TryParse(condition["得意先コード"], out ival) ? ival : (int?)null;
            customerEda = int.TryParse(condition["得意先枝番"], out ival) ? ival : (int?)null;
            createType = int.Parse(condition["作成区分"]);

        }
        #endregion

        #region 支払ヘッダ情報取得
        /// <summary>
        /// 対象の支払ヘッダ情報を取得する
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private List<S02_SHRHD> getHeaderData(Dictionary<string, string> condition)
        {
            // 検索パラメータを展開
            int myCompany, createYearMonth, createType;
            int? closingDay, customerCode, customerEda;
            bool isAllDays;

            getFormParams(condition, out myCompany, out createYearMonth, out closingDay, out isAllDays, out customerCode, out customerEda, out createType);

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                var shd =
                    context.S02_SHRHD.Where(w => w.自社コード == myCompany && w.支払年月 == createYearMonth);

                if (!isAllDays)
                    shd = shd.Where(w => w.支払締日 == closingDay);

                if (customerCode != null && customerEda != null)
                    shd = shd.Where(w => w.支払先コード == customerCode && w.支払先枝番 == customerEda);

                if (createType == 1)
                    shd = shd.Where(w => w.支払額 != 0);

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
            int myCompany, createYearMonth, createType;
            int? closingDay, customerCode, customerEda;
            bool isAllDays;

            getFormParams(condition, out myCompany, out createYearMonth, out closingDay, out isAllDays, out customerCode, out customerEda, out createType);

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

        #region 出金情報取得
        /// <summary>
        /// 対象の出金情報を取得する
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private List<T12_PAY_Data> getPayData(int year, int month, List<S02_SHRHD> hdList)
        {
            List<T12_PAY_Data> payList = new List<T12_PAY_Data>();

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                foreach (var hdRow in hdList)
                {
                    // -- 締日の範囲日付を算出
                    DateTime startDate = AppCommon.GetClosingDate(year, month, hdRow.支払締日, -1);
                    DateTime endDate = AppCommon.GetClosingDate(year, month, hdRow.支払締日, 0).AddDays(-1);

                    var nykhd =
                        context.T12_PAYHD
                            .Where(w => w.削除日時 == null && w.出金元自社コード == hdRow.自社コード &&
                                w.得意先コード == hdRow.支払先コード && w.得意先枝番 == hdRow.支払先枝番 &&
                                w.出金日 >= startDate && w.出金日 <= endDate)
                            .Join(context.T12_PAYDTL,
                                x => x.伝票番号,
                                y => y.伝票番号,
                                (x, y) => new { PAYHD = x, PAYDTL = y })
                            .GroupBy(g => new { g.PAYHD.出金元自社コード, g.PAYHD.得意先コード, g.PAYHD.得意先枝番 })
                            .Select(s => new T12_PAY_Data
                            {
                                自社コード = s.Key.出金元自社コード,
                                得意先コード = (int)s.Key.得意先コード,
                                得意先枝番 = (int)s.Key.得意先枝番,
                                出金額 = s.Sum(m => m.PAYDTL.金額)
                            });

                    payList.AddRange(nykhd.ToList());

                }

                return payList;

            }

        }
        #endregion

        #region 取引先情報取得
        /// <summary>
        /// 対象の取引先情報を取得する
        /// </summary>
        /// <param name="hdList"></param>
        /// <returns></returns>
        private List<M01_TOK> getTokData(List<S02_SHRHD> hdList)
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

    }

}
