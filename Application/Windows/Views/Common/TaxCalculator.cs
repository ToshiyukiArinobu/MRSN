using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyoeiSystem.Application.Windows.Views.Common
{
    using DebugLog = System.Diagnostics.Debug;

    /// <summary>
    /// 消費税計算クラス
    /// </summary>
    public class TaxCalculator
    {
        #region << 定数定義 >>

        /// <summary>
        /// テーブル列名 運用開始日付
        /// </summary>
        private const string COLUMNS_NAME_START_DATE = "適用開始日付";
        /// <summary>
        /// テーブル列名 消費税率
        /// </summary>
        private const string COLUMNS_NAME_SALES_TAX_RATE = "消費税率";
        /// <summary>
        /// テーブル列名 軽減税率
        /// </summary>
        private const string COLUMNS_NAME_REDUCED_TAX_RATE = "軽減税率";

        #endregion

        #region << クラス変数定義 >>

        /// <summary>
        /// 消費税マスタデータテーブル
        /// </summary>
        /// <remarks>
        /// M73_ZEIテーブルそのままの内容を想定
        /// </remarks>
        private DataTable _taxTbl = new DataTable();

        #endregion



        #region コンストラクタ
        /// <summary>
        /// 消費税計算 コンストラクタ
        /// </summary>
        /// <param name="tbl"></param>
        public TaxCalculator(DataTable tbl)
        {
            _taxTbl = tbl;
        }
        #endregion

        #region 消費税率取得
        /// <summary>
        /// 指定日時点の消費税を取得する
        /// </summary>
        /// <param name="targetDate">対象となる日付</param>
        /// <param name="option">消費税区分(0:通常税率、1:軽減税率、2:非課税)</param>
        /// <returns></returns>
        public int getTargetTaxRate(DateTime targetDate, int option = 0)
        {
            int rate = 0;

            // 対象となる開始日を取得
            var maxDate =
                _taxTbl.AsEnumerable()
                    .Where(x => x.Field<DateTime>(COLUMNS_NAME_START_DATE) <= targetDate)
                    .Max(m => m.Field<DateTime>(COLUMNS_NAME_START_DATE));

            // 対象日付が取得できない場合は以下を処理しない
            if (maxDate == null)
                return rate;

            // 対象開始日と一致する消費税情報を取得
            var targetRow =
                _taxTbl.AsEnumerable()
                    .Where(x => x.Field<DateTime>(COLUMNS_NAME_START_DATE) == maxDate)
                    .FirstOrDefault();

            switch (option)
            {
                case 0:
                    rate = AppCommon.IntParse(targetRow[COLUMNS_NAME_SALES_TAX_RATE].ToString());
                    DebugLog.WriteLine(String.Format("[{0}]消費税率:{1}%", targetRow[COLUMNS_NAME_START_DATE], rate));
                    break;

                case 1:
                    rate = AppCommon.IntParse(targetRow[COLUMNS_NAME_REDUCED_TAX_RATE].ToString());
                    DebugLog.WriteLine(String.Format("[{0}]軽減税率:{1}%", targetRow[COLUMNS_NAME_START_DATE], rate));
                    break;

                case 2:
                    rate = 0;
                    break;

                default:
                    break;

            }

            return rate;

        }
        #endregion

        #region 消費税計算
        /// <summary>
        /// 消費税計算をおこない、対象金額の消費税額を返す
        /// </summary>
        /// <param name="baseDate">計算基準日</param>
        /// <param name="price">対象金額</param>
        /// <param name="taxRateKbn">消費税区分[M09_HIN.消費税区分]0:通常税率、1:軽減税率、2:非課税</param>
        /// <param name="taxKbnId">税区分ID[M01_TOK.税区分ID]1:切捨て、2:四捨五入、3:切上げ</param>
        /// <returns>消費税額</returns>
        public decimal CalculateTax(DateTime baseDate, decimal price, int taxRateKbn, int taxKbnId)
        {
            decimal conTax = 0;

            // 税率取得
            decimal taxRate = getTargetTaxRate(baseDate, taxRateKbn);
            // 税額計算
            decimal calcValue = decimal.Multiply(price, decimal.Divide(taxRate, 100m));

            switch (taxKbnId)
            {
                case 1:
                    // 切捨て
                    conTax += Math.Floor(calcValue);
                    break;

                case 2:
                    // 四捨五入
                    conTax += Math.Round(calcValue, 0);
                    break;

                case 3:
                    // 切上げ
                    conTax += Math.Ceiling(calcValue);
                    break;

                default:
                    // 上記以外は税計算なしとする
                    break;

            }

            return conTax;

        }

        /// <summary>
        /// 消費税計算をおこない、対象金額の消費税額を返す
        /// </summary>
        /// <param name="baseDate">計算基準日</param>
        /// <param name="cost">対象の単価</param>
        /// <param name="qty">対象の数量</param>
        /// <param name="taxRateKbn">消費税区分[M09_HIN.消費税区分]0:通常税率、1:軽減税率、2:非課税</param>
        /// <param name="taxKbnId">税区分ID[M01_TOK.税区分ID]1:切捨て、2:四捨五入、3:切上げ</param>
        /// <returns>消費税額</returns>
        public decimal CalculateTax(DateTime baseDate, int cost, int qty, int taxRateKbn, int taxKbnId)
        {
            // 金額算出
            long price = ((long)cost * qty);

            return CalculateTax(baseDate, price, taxRateKbn, taxKbnId);

        }
        #endregion

    }

}
