using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 請求一覧表サービスクラス
    /// </summary>
    public class TKS03010
    {

        #region 項目クラス定義

        /// <summary>
        /// T11_NYKNHD + DTL 項目定義クラス
        /// </summary>
        public class T11_NYKN_Data
        {
            public int 自社コード { get; set; }
            public int 得意先コード { get; set; }
            public int 得意先枝番 { get; set; }
            public long 入金額 { get; set; }
        }

        /// <summary>
        /// TKS03010_請求一覧表帳票項目定義
        /// </summary>
        private class PrintMember
        {
            //20190906 add-s CB 軽減税率対応
            //public string 得意先コード { get; set; }
            //public string 得意先名称 { get; set; }
            //public string 請求締日 { get; set; }
            //public long 前月残高 { get; set; }
            //public long 入金金額 { get; set; }
            //public long 売上額 { get; set; }
            //public long 値引額 { get; set; }
            //public long 非課税売上額 { get; set; }
            //public long 消費税 { get; set; }
            //public long 当月請求額 { get; set; }
            //public long 件数 { get; set; }

            public string 得意先コード { get; set; }
            public string 得意先名称 { get; set; }
            public long 回数 { get; set; }
            public string 請求締日 { get; set; }
            public long 前月残高 { get; set; }
            public long 入金金額 { get; set; }
            public long 繰越残高 { get; set; }
            public long 売上額 { get; set; }
            //public long 値引額 { get; set; }
            public long 非課税売上額 { get; set; }
            public long 消費税 { get; set; }
            public long 当月請求額 { get; set; }
            public long 件数 { get; set; }

            public string 集計最終日 { get; set; }
            //20190906 add-e CB 軽減税率対応
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

        #region 請求一覧表の基本情報を取得
        /// <summary>
        /// 請求一覧表の基本情報を取得する
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

            // ベースとなる請求ヘッダ情報を取得
            List<S01_SEIHD> hdList = getHeaderData(condition);
            var hdData =
                //20190906 mod-s CB 軽減税率対応 集計最終日をキーに含める
                //hdList.GroupBy(g => new { g.自社コード, g.請求年月, g.請求締日, g.請求先コード, g.請求先枝番 })
                hdList.GroupBy(g => new { g.自社コード
                    , g.請求年月, g.請求締日
                    , g.請求先コード, g.請求先枝番
                    , g.入金日, g.集計最終日 })
                //20190906 mod-s CB 軽減税率対応
                    .Select(s => new
                    {
                        s.Key.自社コード,
                        s.Key.請求年月,
                        s.Key.請求締日,
                        s.Key.請求先コード,
                        s.Key.請求先枝番,
                        //20190906 add-s CB 軽減税率対応
                        s.Key.入金日,
                        s.Key.集計最終日,
                        回数 = s.Sum(m => m.回数),
                        //20190906 add-e CB 軽減税率対応

                        前月残高 = s.Sum(m => m.前月残高),

                        //20190906 del-s CB 軽減税率対応
                        //値引額 = s.Sum(m => m.値引額),
                        //20190906 del-e CB 軽減税率対応

                        //20190906 add-s CB 軽減税率対応
                        入金金額 = s.Sum(m => m.入金額),
                        繰越残高 = s.Sum(m => m.繰越残高),
                        //20190906 add-e CB 軽減税率対応

                        売上額 = s.Sum(m => m.売上額),
                        非税売上額 = s.Sum(m => m.非税売上額),
                        消費税 = s.Sum(m => m.消費税),
                        当月請求額 = s.Sum(m => m.当月請求額)

                    });

            // 詳細データ件数を取得
            List<S01_SEIDTL> dtlList = getDetailData(condition);

            //20190911 mod-s CB 軽減税率対応
            //var dtlData =
            //    dtlList.GroupBy(g => new { g.自社コード, g.請求年月, g.請求締日, g.請求先コード, g.請求先枝番 })
            //        .Select(s => new
            //        {
            //            s.Key.自社コード,
            //            s.Key.請求年月,
            //            s.Key.請求締日,
            //            s.Key.請求先コード,
            //            s.Key.請求先枝番,
            //            件数 = s.Count()
            //        });

            var dtlData =
                dtlList.GroupBy(g => new { g.自社コード
                    , g.請求年月
                    , g.請求締日
                    , g.請求先コード
                    , g.請求先枝番 
                    , g.入金日
                    , g.回数
                })
            .Select(s => new
            {
                s.Key.自社コード,
                s.Key.請求年月,
                s.Key.請求締日,
                s.Key.請求先コード,
                s.Key.請求先枝番,
                s.Key.入金日,
                s.Key.回数,
                件数 = s.Count()
            });
            //20190911 mod-e CB 軽減税率対応

            // 入金データを取得(金額集計済)
            List<T11_NYKN_Data> nykList = getNyukinData(createYearMonth / 100, createYearMonth % 100, hdList);

            // 取引先情報を取得
            List<M01_TOK> tokList = getTokData(hdList);

            // 取得データを統合して結果リストを作成
            var resultList =
                hdData.GroupJoin(dtlData,
                    //20190911 mod-s CB 軽減税率対応
                    //x => new { x.自社コード, x.請求先コード, x.請求先枝番 },
                    //y => new { y.自社コード, y.請求先コード, y.請求先枝番 },
                    x => new { x.自社コード, x.請求先コード, x.請求先枝番, x.請求年月, x.請求締日, x.入金日, x.回数 },
                    y => new { y.自社コード, y.請求先コード, y.請求先枝番, y.請求年月, y.請求締日, y.入金日, y.回数 },
                    //20190911 mod-e CB 軽減税率対応
                    (x, y) => new { x, y })
                .SelectMany(x => x.y.DefaultIfEmpty(),
                    (a, b) => new { NHD = a.x, NDTL = b })
                .GroupJoin(nykList,
                    x => new { x.NHD.自社コード, コード = x.NHD.請求先コード, 枝番 = x.NHD.請求先枝番 },
                    y => new { y.自社コード, コード = y.得意先コード, 枝番 = y.得意先枝番 },
                    (x, y) => new { x, y })
                .SelectMany(x => x.y.DefaultIfEmpty(),
                    (c, d) => new { c.x.NHD, c.x.NDTL, NYKN = d })
                .GroupJoin(tokList,
                    x => new { コード = x.NHD.請求先コード, 枝番 = x.NHD.請求先枝番 },
                    y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                    (x, y) => new { x, y })
                .SelectMany(x => x.y.DefaultIfEmpty(),
                    (e, f) => new { e.x.NHD, e.x.NDTL, e.x.NYKN, TOK = f })
                .OrderBy(o => o.NHD.請求先コード)
                .ThenBy(t => t.NHD.請求先枝番)
                .ToList()
                .Select(x => new PrintMember
                {
                    得意先コード = string.Format("{0:000} - {1:00}", x.NHD.請求先コード, x.NHD.請求先枝番),  // No.132-1 Mod
                    得意先名称 = x.TOK == null ? "" : x.TOK.得意先名１,

                    //20190906 add-s CB 軽減税率対応
                    回数 = x.NHD.回数,
                    //20190906 add-e CB 軽減税率対応

                    //20190906 mod-s CB 軽減税率対応
                    //請求締日 = x.NHD.請求締日.ToString(),
                    請求締日 = x.NHD.集計最終日.ToString(),
                    //20190906 mod-e CB 軽減税率対応

                    前月残高 = x.NHD.前月残高,

                    //20190906 mod-s CB 軽減税率対応
                    //入金金額 = x.NYKN == null ? 0 : x.NYKN.入金額,
                    入金金額 = x.NHD.入金金額,
                    //20190906 mod-e CB 軽減税率対応

                    //20190906 add-s CB 軽減税率対応
                    繰越残高 = x.NHD.繰越残高,
                    //20190906 add-e CB 軽減税率対応

                    売上額 = x.NHD.売上額,

                    //20190906 del-s CB 軽減税率対応
                    //値引額 = x.NHD.値引額,
                    //20190906 del-e CB 軽減税率対応

                    非課税売上額 = x.NHD.非税売上額,
                    消費税 = x.NHD.消費税,
                    当月請求額 = x.NHD.当月請求額,
                    件数 = x.NDTL == null ? 0 : x.NDTL.件数

                });

            //20190906 add-s CB 軽減税率対応 集計最終日　MM/DD 成型
            DataTable dt;
            dt = KESSVCEntry.ConvertListToDataTable<PrintMember>(resultList.ToList());

            foreach (DataRow dr in dt.Rows)
            {
                string sWk = dr["請求締日"].ToString();

                if (sWk.Trim().Length > 0)
                {
                    //MM/DD 成型
                    string sMMdd = sWk.Substring(5, 5);
                    dr["請求締日"] =  sMMdd;
                }
            }
            //20190906 add-e CB 軽減税率対応 集計最終日　MM/DD 成形

            //20190906 mod-s CB 軽減税率対応 
//            return KESSVCEntry.ConvertListToDataTable<PrintMember>(resultList.ToList());
            return dt;
            //20190906 mod-e CB 軽減税率対応 

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

        #region 請求ヘッダ情報取得
        /// <summary>
        /// 対象の請求ヘッダ情報を取得する
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private List<S01_SEIHD> getHeaderData(Dictionary<string, string> condition)
        {
            // 検索パラメータを展開
            int myCompany, createYearMonth, createType;
            int? closingDay, customerCode, customerEda;
            bool isAllDays;

            getFormParams(condition, out myCompany, out createYearMonth, out closingDay, out isAllDays, out customerCode, out customerEda, out createType);

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                var shd =
                    context.S01_SEIHD.Where(w => w.自社コード == myCompany && w.請求年月 == createYearMonth);

                if (!isAllDays)
                    shd = shd.Where(w => w.請求締日 == closingDay);

                if (customerCode != null && customerEda != null)
                    shd = shd.Where(w => w.請求先コード == customerCode && w.請求先枝番 == customerEda);

                if (createType == 1)
                    shd = shd.Where(w => w.売上額 != 0);          // No-144 Mod

                return shd.ToList();

            }

        }
        #endregion

        #region 請求明細情報取得
        /// <summary>
        /// 対象の請求明細情報を取得する
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private List<S01_SEIDTL> getDetailData(Dictionary<string, string> condition)
        {
            // 検索パラメータを展開
            int myCompany, createYearMonth, createType;
            int? closingDay, customerCode, customerEda;
            bool isAllDays;

            getFormParams(condition, out myCompany, out createYearMonth, out closingDay, out isAllDays, out customerCode, out customerEda, out createType);

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                var sdtl =
                    context.S01_SEIDTL.Where(w => w.自社コード == myCompany && w.請求年月 == createYearMonth);

                if (!isAllDays)
                    sdtl = sdtl.Where(w => w.請求締日 == closingDay);

                if (customerCode != null && customerEda != null)
                    sdtl = sdtl.Where(w => w.請求先コード == customerCode && w.請求先枝番 == customerEda);

                return sdtl.ToList();

            }

        }
        #endregion

        #region 入金情報取得
        /// <summary>
        /// 対象の入金(ヘッダ)情報を取得する
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private List<T11_NYKN_Data> getNyukinData(int year, int month, List<S01_SEIHD> hdList)
        {
            List<T11_NYKN_Data> nykList = new List<T11_NYKN_Data>();

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                foreach (var hdRow in hdList)
                {
                    // -- 締日の範囲日付を算出
                    DateTime startDate = AppCommon.GetClosingDate(year, month, hdRow.請求締日, -1);
                    DateTime endDate = AppCommon.GetClosingDate(year, month, hdRow.請求締日, 0).AddDays(-1);

                    var nykhd =
                        context.T11_NYKNHD
                            .Where(w => w.削除日時 == null && w.入金先自社コード == hdRow.自社コード &&
                                w.得意先コード == hdRow.請求先コード && w.得意先枝番 == hdRow.請求先枝番 &&
                                w.入金日 >= startDate && w.入金日 <= endDate)
                            .Join(context.T11_NYKNDTL,
                                x => x.伝票番号,
                                y => y.伝票番号,
                                (x, y) => new { NYKNHD = x, NYKNDTL = y })
                            .GroupBy(g => new { g.NYKNHD.入金先自社コード, g.NYKNHD.得意先コード, g.NYKNHD.得意先枝番 })
                            .Select(s => new T11_NYKN_Data
                            {
                              自社コード = s.Key.入金先自社コード,
                              得意先コード = (int)s.Key.得意先コード,
                              得意先枝番 = (int)s.Key.得意先枝番,
                              入金額 = s.Sum(m => m.NYKNDTL.金額)
                            });

                    nykList.AddRange(nykhd.ToList());

                }

                return nykList;

            }

        }
        #endregion

        #region 取引先情報取得
        /// <summary>
        /// 対象の取引先情報を取得する
        /// </summary>
        /// <param name="hdList"></param>
        /// <returns></returns>
        private List<M01_TOK> getTokData(List<S01_SEIHD> hdList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                var tokList =
                    context.M01_TOK.Where(w => w.削除日時 == null)
                        .Join(hdList,
                            x => new { コード = x.取引先コード, 枝番 = x.枝番 },
                            y => new { コード = y.請求先コード, 枝番 = y.請求先枝番 },
                            (x, y) => new { TOK = x, SHD = y })
                        .Select(x => x.TOK);


                var test = 
                    context.M01_TOK.Where(w => w.削除日時 == null)
                        .ToList()
                        .Where(w => hdList.Exists(v => v.請求先コード == w.取引先コード && v.請求先枝番 == w.枝番));

                //return tokList.ToList();
                return test.ToList();

            }

        }
        #endregion

    }

}
