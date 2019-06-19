using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 基底サービスクラス
    /// </summary>
    public class BaseService
    {
        /// <summary></summary>
        protected TRAC3Entities _context;

        /// <summary>ログインユーザID</summary>
        /// <remarks>登録・更新時に登録者、更新者、削除者の設定に使用</remarks>
        protected int _loginUserId;

        /// <summary></summary>
        protected Common com = new Common();



        #region << 関数系 >>

        #region ParseNumeric
        protected T ParseNumeric<T>(object obj) where T : struct
        {
            if (obj == null)
                return default(T);

            if (typeof(T) == typeof(int))
            {
                int val = -1;
                if (!int.TryParse(obj.ToString(), out val))
                    return default(T);

                return (T)((object)val);

            }
            else if (typeof(T) == typeof(long))
            {
                long val = -1;
                if (!long.TryParse(obj.ToString(), out val))
                    return default(T);

                return (T)((object)val);

            }
            else if (typeof(T) == typeof(double))
            {
                double val = -1;
                if (!double.TryParse(obj.ToString(), out val))
                    return default(T);

                return (T)((object)val);

            }
            else if (typeof(T) == typeof(decimal))
            {
                decimal val = -1;
                if (!decimal.TryParse(obj.ToString(), out val))
                    return default(T);

                return (T)((object)val);

            }

            return default(T);

        }
        #endregion

        #region DateParse
        /// <summary>
        /// オブジェクト型日付をDateTime?型に変換する
        /// </summary>
        /// <param name="objDt"></param>
        /// <returns></returns>
        protected DateTime? DateParse(object objDt)
        {
            DateTime wkdt = new DateTime();

            return DateTime.TryParse(string.Format("{0:yyyy/MM/dd}", objDt), out wkdt) ? wkdt : (DateTime?)null;

        }
        #endregion

        #region ParseInt
        /// <summary>
        /// オブジェクト型をInt型に変換する
        /// ただし、返却値はnullを許可するものとする
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected int? ParseInt(object obj)
        {
            int ival = -1;

            if (obj == null)
                return null;

            return int.TryParse(obj.ToString(), out ival) ? (int?)ival : null;

        }
        #endregion



        #endregion

    }

}