using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Objects;
using System.Data;
using System.Data.Common;
using System.Transactions;
using KyoeiSystem.Framework.Common;

namespace KyoeiSystem.Application.WCFService
{

    /// <summary>
    /// 共通処理
    /// </summary>
	public class Common
	{
        #region DBの現在日時取得
        /// <summary>
        /// 現在のDB日付を取得する
        /// </summary>
        /// <returns>Db現在日時</returns>
        public DateTime GetDbDateTime()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try { context.Connection.Open(); }
                catch (Exception ex) { throw new DBOpenException(ex); }

                var date = context.ExecuteStoreQuery<DateTime>("SELECT GETDATE()", null).FirstOrDefault();

                return date;

            }

        }
        #endregion

        #region 住所情報取得
        public List<M99_ZIP> GetZipList()
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				try { context.Connection.Open(); }
				catch (Exception ex) { throw new DBOpenException(ex); }

                var zipList =
                    context.M99_ZIP
                        .OrderBy(o => o.郵便番号)
                        .ToList();

                return zipList;

            }

        }
        #endregion

        #region メッセージ情報取得
        public List<M99_MSG> GetMessageList()
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				try { context.Connection.Open(); }
				catch (Exception ex) { throw new DBOpenException(ex); }

                var msgList =
                    context.M99_MSG
                        .Where(w => w.削除日付 == null)
                        .ToList();

                return msgList;

            }

		}
        #endregion

        #region コンボ情報取得
        public List<M99_COMBOLIST> GetCodeList()
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				try { context.Connection.Open(); }
				catch (Exception ex) { throw new DBOpenException(ex); }

                var cmbList =
                    context.M99_COMBOLIST
                        .ToList();

                return cmbList;

            }

		}
        #endregion

        #region 在庫数量チェック

        /// <summary>
        /// 在庫情報を照会して要求数量が在庫数量を下回るかチェックする
        /// </summary>
        /// <param name="storeHouseCode">倉庫コード</param>
        /// <param name="productCode">品番コード</param>
        /// <param name="expirationDate">賞味期限</param>
        /// <param name="nowStockQty">現在庫数(個数返却用)</param>
        /// <param name="requestQty">要求数量</param>
        /// <returns>下回る場合真</returns>
        public bool CheckStokItemQty(int storeHouseCode, int productCode, DateTime? expirationDate, out decimal nowStockQty, decimal requestQty = 0)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try { context.Connection.Open(); }
                catch (Exception ex) { throw new DBOpenException(ex); }

                // REMARKS:DateTime.MaxValue だと時刻を含む為
                DateTime maxDate = AppCommon.GetMaxDate();

                var stokInfo =
                    context.S03_STOK
                        .Where(w =>
                            w.削除日時 == null &&
                            w.倉庫コード == storeHouseCode &&
                            w.品番コード == productCode &&
                            w.賞味期限 == (expirationDate ?? maxDate))
                        .FirstOrDefault();

                if (stokInfo == null)
                {
                    // 対象在庫なし
                    nowStockQty = 0;
                    return false;
                }

                // 在庫数と要求数を比較した結果を返す
                nowStockQty = stokInfo.在庫数;
                return (stokInfo.在庫数 >= requestQty);

            }

        }

        /// <summary>
        /// 在庫情報を照会して要求数量が在庫数量を下回るかチェックする
        /// </summary>
        /// <param name="storeHouseCode">倉庫コード</param>
        /// <param name="productCode">品番コード</param>
        /// <param name="expirationDate">賞味期限</param>
        /// <param name="requestQty">要求数量</param>
        /// <returns>下回る場合真</returns>
        public bool CheckStokItemQty(int storeHouseCode, int productCode, DateTime? expirationDate, decimal requestQty = 0)
        {
            decimal qty = 0;
            return CheckStokItemQty(storeHouseCode, productCode, expirationDate, out qty, requestQty);

        }
        #endregion

        #region 消費税関連

        #region 消費税計算
        /// <summary>
        /// 品番から消費税計算をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="salesDate">売上日</param>
        /// <param name="productCode">品番コード</param>
        /// <param name="price">単価</param>
        /// <param name="qty">数量</param>
        /// /// <param name="taxKbn">税区分ID</param>
        /// <param name="taxKbn">消費税区分[M01_TOK.T消費税区分(S支払消費税区分)] 1:一括、2:個別、3:請求無し</param>
        /// <returns></returns>
        public decimal CalculateTax(DateTime salesDate, int productCode, decimal price, decimal qty, int taxKbnId, int taxKbn )
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                var hin =
                    context.M09_HIN
                        .Where(w => w.削除日時 == null && w.品番コード == productCode)
                        .FirstOrDefault();

                if (hin == null)
                    return 0;

                decimal conTax = 0;
                decimal taxRate = getTargetTaxRate(salesDate, hin.消費税区分 ?? 0);
                decimal calcValue = decimal.Multiply(price * qty, decimal.Divide(taxRate, 100m));

                if (taxKbn == (int)CommonConstants.消費税区分.ID03_請求無)
                {
                    return conTax;
                }

                switch (taxKbnId)
                {
                    case 1:
                        // 切捨て
                        if (calcValue > 0)
                        {
                            conTax += Math.Floor(calcValue);
                        }
                        else
                        {
                            conTax += Math.Ceiling(calcValue);
                        }
                        break;

                    case 2:
                        // 四捨五入
                        conTax += Math.Round(calcValue, 0);
                        break;

                    case 3:
                        // 切上げ
                        if (calcValue > 0)
                        {
                            conTax += Math.Ceiling(calcValue);
                        }
                        else
                        {
                            conTax += Math.Floor(calcValue);
                        }
                        break;

                    default:
                        // 上記以外は税計算なしとする
                        break;
                }

                return conTax;
            }
        }
        #endregion

        #region 指定日時点の消費税率取得
        /// <summary>
        /// 指定日時点の消費税を取得する
        /// </summary>
        /// <param name="targetDate">対象となる日付</param>
        /// <param name="option">消費税区分(0:対象外、1:対象)</param>
        /// <returns></returns>
        private int getTargetTaxRate(DateTime? targetDate, int option = 0)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                int rate = 0;

                // 対象となる開始日を取得
                var maxDate =
                    context.M73_ZEI
                        .Where(w => w.削除日時 == null && w.適用開始日付 <= targetDate)
                        .Max(m => m.適用開始日付);

                // 対象日付が取得できない場合は以下を処理しない
                if (maxDate == null)
                    return rate;

                // 対象開始日と一致する消費税情報を取得
                var targetRow =
                    context.M73_ZEI
                        .Where(x => x.適用開始日付 == maxDate)
                        .FirstOrDefault();

                switch (option)
                {
                    case 0:
                        // 通常税率
                        rate = targetRow.消費税率 ?? 0;
                        break;

                    case 1:
                        // 軽減税率
                        rate = targetRow.軽減税率 ?? 0;
                        break;

                    case 2:
                        // 非課税
                        rate = 0;
                        break;

                    default:
                        break;

                }

                return rate;
            }
        }
        #endregion


        #endregion
    }

    public static class AppCommon
    {
        public static int IntParse(string str, int defval = 0)
        {
            int work = defval;
            int.TryParse(str, out work);
            return work;
        }

        public static int IntParse(string str, System.Globalization.NumberStyles numstyle, int defval = 0)
        {
            int work = defval;
            int.TryParse(str, numstyle, System.Globalization.NumberFormatInfo.CurrentInfo, out work);
            return work;
        }

        public static decimal DecimalParse(string str, decimal defval = 0m)
        {
            decimal work = defval;
            decimal.TryParse(str, out work);
            return work;
        }

        public static long LongParse(string str, long defVal = 0)
        {
            long work = defVal;
            long.TryParse(str, out work);

            return work;

        }

        #region 日付型関連
        /// <summary>
        /// 日付型(時刻含)から時刻を除いた結果を返す
        /// </summary>
        /// <param name="date"></param>
        /// <param name="defDate"></param>
        /// <returns></returns>
        public static DateTime DateTimeToDate(DateTime? date, DateTime defDate)
        {
            if (date == null)
                return DateTime.Parse(defDate.ToString("yyyy/MM/dd"));

            DateTime dt;
            return DateTime.TryParse(((DateTime)date).ToString("yyyy/MM/dd"), out dt) ?
                        dt : DateTime.Parse(defDate.ToString("yyyy/MM/dd"));

        }

        /// <summary>
        /// 日付型(時刻含)から時刻を除いた結果を返す
        /// </summary>
        /// <param name="date"></param>
        /// <param name="defDate"></param>
        /// <returns></returns>
        public static DateTime? DateTimeToDate(DateTime? date, DateTime? defDate = null)
        {
            DateTime? defDt = null;
            if (defDate != null)
                defDt = DateTime.Parse(((DateTime)defDate).ToString("yyyy/MM/dd"));

            if (date == null && defDate != null)
                return defDt;

            DateTime dt;
            return DateTime.TryParse(((DateTime)date).ToString("yyyy/MM/dd"), out dt) ?
                        dt : defDt;

        }

        /// <summary>
        /// 日付オブジェクトから日付型(時刻除く)に変換して返す
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime? ObjectToDate(object date)
        {
            if (date == null)
                return (DateTime?)null;

            DateTime dt;
            return DateTime.TryParse(date.ToString(), out dt) ?
                        dt : (DateTime?)null;

        }

        /// <summary>
        /// 最大日付(時刻除く)を取得する
        /// </summary>
        /// <returns></returns>
        public static DateTime GetMaxDate()
        {
            return DateTime.Parse(DateTime.MaxValue.ToString("yyyy/MM/dd"));

        }
        #endregion

        #region 締日算出
        /// <summary>
        /// 指定された締日を算出して返す
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="closingDay">締日</param>
        /// <param name="addingMonths">加算月数</param>
        /// <returns></returns>
        public static DateTime GetClosingDate(int year, int month, int closingDay, int addingMonths = 0)
        {
            DateTime baseDate = new DateTime(year, month, 1);
            baseDate = baseDate.AddMonths(addingMonths);

            // 締日が月末指定の場合
            int lastDay = closingDay;
            if (closingDay == 31)
                lastDay = DateTime.DaysInMonth(baseDate.Year, baseDate.Month);

            try
            {
                return new DateTime(baseDate.Year, baseDate.Month, lastDay);

            }
            catch
            {
                // == 月末ではないが日付にならなかった場合用 ==
                // 例)締日30で締年月が２月だった場合など
                // ⇒30なので月末扱いにならずそのまま変換するが失敗してしまうパターン
                // 　⇒末日を取得して再生成する
                return new DateTime(baseDate.Year, baseDate.Month, DateTime.DaysInMonth(baseDate.Year, baseDate.Month));

            }

        }
        #endregion

    }

}