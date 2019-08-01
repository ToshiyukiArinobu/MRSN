﻿using System;
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
            public int 品番コード { get; set; }
            public string 自社品番 { get; set; }
            public string 色コード { get; set; }
            public string 品名 { get; set; }
            public decimal 単価 { get; set; }
            public decimal 数量 { get; set; }
            public int 金額 { get; set; }
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
                        s.品番コード,
                        s.単価,
                        s.数量,
                        s.金額,
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
                    y => new { y.品番コード},
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
                    仕入先コード = x.NHD.支払先コード + " - " + x.NHD.支払先枝番,
                    仕入先名称 = x.TOK == null ? "" : x.TOK.得意先名１,
                    支払締日 = x.NHD.支払締日.ToString(),
                    品番コード = x.NDTL.品番コード,
                    自社品番 = x.HIN.自社品番,
                    色コード = x.HIN.自社色,
                    品名 = x.HIN.自社品名,
                    単価 = x.NDTL.単価,
                    数量 = x.NDTL.数量,
                    金額 = x.NDTL.金額,
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
            out int? customerEda )
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
        private List<S02_SHRHD> getHeaderData(Dictionary<string, string> condition)
        {
            // 検索パラメータを展開
            int myCompany, createYearMonth;
            int? closingDay, customerCode, customerEda;
            bool isAllDays;

            getFormParams(condition, out myCompany, out createYearMonth, out closingDay, out isAllDays, out customerCode, out customerEda);

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                var shd =
                    context.S02_SHRHD.Where(w => w.自社コード == myCompany && w.支払年月 == createYearMonth);

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
        
    }

}