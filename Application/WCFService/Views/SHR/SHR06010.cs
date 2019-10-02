using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 出金予定表サービスクラス
    /// </summary>
    public class SHR06010
    {

        #region CSV出力データ取得
        /// <summary>
        /// ＣＳＶ出力データを取得する
        /// </summary>
        /// <param name="condition">
        ///  == 検索条件 ==
        /// 自社コード
        /// 出金年月
        /// 出金日
        /// 全出金日
        /// 得意先コード
        /// 得意先枝番
        /// 作成区分
        /// </param>
        /// <returns></returns>
        public DataTable GetCsvData(Dictionary<string, string> condition)
        {
            DataTable dt = getCommonData(condition);

            // 出力に不要な列を削除する
            dt.Columns.Remove("K自社コード");
            dt.Columns.Remove("K支払年月");
            dt.Columns.Remove("K入金日");
            dt.Columns.Remove("K支払先コード");
            dt.Columns.Remove("K支払先枝番");
            if(dt.Columns.Contains("ChangeTracker"))
                dt.Columns.Remove("ChangeTracker");

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
        /// 出金年月
        /// 出金日
        /// 全出金日
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


        #region 出金予定表の基本情報を取得
        /// <summary>
        /// 出金予定表の基本情報を取得する
        /// </summary>
        /// <param name="condition">
        ///  == 検索条件 ==
        /// 自社コード
        /// 出金年月
        /// 出金日
        /// 全出金日
        /// 仕入先コード
        /// 仕入先枝番
        /// 作成区分
        /// </param>
        /// <returns></returns>
        private DataTable getCommonData(Dictionary<string, string> condition)
        {
            // 検索パラメータを展開
            int myCompany, paymentYearMonth, createType;
            int? paymentDay, customerCode, customerEda;
            bool isAllDays;

            getFormParams(condition, out myCompany, out paymentYearMonth, out paymentDay, out isAllDays, out customerCode, out customerEda, out createType);

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                var baseList =
                    context.V_SHR06010
                        .Where(w => w.K自社コード == myCompany && w.K支払年月 == paymentYearMonth);

                #region 任意入力条件の適用
                if (customerCode != null && customerEda != null)
                    baseList = baseList
                        .Where(w => w.K支払先コード == customerCode && w.K支払先枝番 == customerEda);

                if (paymentDay != null)
                    baseList = baseList
                        .Where(w => w.K入金日 == paymentDay);

                if (createType == 1)
                    baseList = baseList
                        .Where(w => w.支払予定額 != 0);

                #endregion

                // データを出金年月日順でソート
                baseList =
                    baseList
                        .OrderBy(o => o.K自社コード)
                        .ThenBy(t => t.K支払年月)
                        .ThenBy(t => t.K入金日)
                        .ThenBy(t => t.K支払先コード)
                        .ThenBy(t => t.K支払先枝番);

                return KESSVCEntry.ConvertListToDataTable<V_SHR06010>(baseList.ToList());

            }

        }
        #endregion

        #region パラメータ展開
        /// <summary>
        /// フォームパラメータを展開する
        /// </summary>
        /// <param name="condition">検索条件辞書</param>
        /// <param name="myCompany">自社コード</param>
        /// <param name="paymentYearMonth">出金年月(yyyymm)</param>
        /// <param name="paymentDay">出金日</param>
        /// <param name="isAllDays">全出金日を対象とするか</param>
        /// <param name="customerCode">得意先コード</param>
        /// <param name="customerEda">得意先枝番</param>
        /// <param name="createType">作成区分</param>
        private void getFormParams(
            Dictionary<string, string> condition,
            out int myCompany,
            out int paymentYearMonth,
            out int? paymentDay,
            out bool isAllDays,
            out int? customerCode,
            out int? customerEda,
            out int createType )
        {
            int ival = -1;

            myCompany = int.Parse(condition["自社コード"]);
            paymentYearMonth = int.Parse(condition["支払年月"].Replace("/", ""));
            paymentDay = int.TryParse(condition["支払日"], out ival) ? ival : (int?)null;
            isAllDays = bool.Parse(condition["全締日"]);
            customerCode = int.TryParse(condition["支払先コード"], out ival) ? ival : (int?)null;
            customerEda = int.TryParse(condition["支払先枝番"], out ival) ? ival : (int?)null;
            createType = int.Parse(condition["作成区分"]);

        }
        #endregion

    }

}
