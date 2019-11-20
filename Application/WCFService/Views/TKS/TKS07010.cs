using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 入金予定表サービスクラス
    /// </summary>
    public class TKS07010
    {

        #region CSV出力データ取得
        /// <summary>
        /// ＣＳＶ出力データを取得する
        /// </summary>
        /// <param name="condition">
        ///  == 検索条件 ==
        /// 自社コード
        /// 入金年月
        /// 入金日
        /// 全入金日
        /// 得意先コード
        /// 得意先枝番
        /// 作成区分
        /// </param>
        /// <returns></returns>
        public DataTable GetCsvData(Dictionary<string, string> condition)
        {
            DataTable dt = getCommonData(condition);

            // 出力に不要な列を削除する
            dt.Columns.Remove("K入金年月");
            dt.Columns.Remove("K入金日");
            dt.Columns.Remove("K得意先コード");
            dt.Columns.Remove("K得意先枝番");
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
        /// 入金年月
        /// 入金日
        /// 全入金日
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


        #region 入金予定表の基本情報を取得
        /// <summary>
        /// 入金予定表の基本情報を取得する
        /// </summary>
        /// <param name="condition">
        ///  == 検索条件 ==
        /// 自社コード
        /// 入金年月
        /// 入金日
        /// 全入金日
        /// 得意先コード
        /// 得意先枝番
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
                    context.V_TKS08010
                        .Where(w => w.自社コード == myCompany && w.K入金年月 == paymentYearMonth);

                #region 任意入力条件の適用
                if (customerCode != null && customerEda != null)
                    baseList = baseList
                        .Where(w => w.K得意先コード == customerCode && w.K得意先枝番 == customerEda);

                if (paymentDay != null)
                    baseList = baseList
                        .Where(w => w.K入金日 == paymentDay);

                if (createType == 1)
                    baseList = baseList
                        .Where(w => w.回収予定額 != 0);              // No-146 Mod

                #endregion

                // データを入金年月日順でソート
                baseList =
                    baseList
                        .OrderBy(o => o.自社コード)
                        .ThenBy(t => t.K入金年月)
                        .ThenBy(t => t.K入金日)
                        .ThenBy(t => t.K得意先コード)
                        .ThenBy(t => t.K得意先枝番);

                return KESSVCEntry.ConvertListToDataTable<V_TKS08010>(baseList.ToList());

            }

        }
        #endregion

        #region パラメータ展開
        /// <summary>
        /// フォームパラメータを展開する
        /// </summary>
        /// <param name="condition">検索条件辞書</param>
        /// <param name="myCompany">自社コード</param>
        /// <param name="paymentYearMonth">入金年月(yyyymm)</param>
        /// <param name="paymentDay">入金日</param>
        /// <param name="isAllDays">全入金日を対象とするか</param>
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
            paymentYearMonth = int.Parse(condition["入金年月"].Replace("/", ""));
            paymentDay = int.TryParse(condition["入金日"], out ival) ? ival : (int?)null;
            isAllDays = bool.Parse(condition["全入金日"]);
            customerCode = int.TryParse(condition["得意先コード"], out ival) ? ival : (int?)null;
            customerEda = int.TryParse(condition["得意先枝番"], out ival) ? ival : (int?)null;
            createType = int.Parse(condition["作成区分"]);

        }
        #endregion

    }

}
