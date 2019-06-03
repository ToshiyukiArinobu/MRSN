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
using System.Data.SqlClient;


namespace KyoeiSystem.Application.WCFService
{
    public class MENU01
	{
		public class Oshirase_Member
		{
			public string 名称 { get; set; }
			public string 内容 { get; set; }
			public DateTime 期限 { get; set; }
		}

		/// <summary>
		/// M04_DRVのリスト取得
		/// </summary>
		/// <param name="p乗務員ID">乗務員ID</param>
		/// <returns>M04_DRV_MemberのList</returns>
		public List<Oshirase_Member> Oshirase( )
		{
            //using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            //{
            //    context.Connection.Open();

            //    DateTime Kigen = DateTime.Now.AddMonths(2);

            //    var ret = (from x in context.M04_DRV
            //               where x.免許有効年月日1 != null && x.免許有効年月日1 <= Kigen && x.削除日付 == null && x.就労区分 == 0
            //               orderby x.免許有効年月日1
            //               select new Oshirase_Member()
            //               {
            //                   名称 = x.乗務員名,
            //                   内容 = "免許更新",
            //                   期限 = (DateTime)x.免許有効年月日1,
            //               }
            //               ).ToList();

            //    Kigen = DateTime.Now.AddMonths(1);
            //    ret = ret.Union (from x in context.M05_CAR
            //                     where x.次回車検日 != null && x.次回車検日 <= Kigen && x.廃車区分 == 0 && x.削除日付 == null
            //                     orderby x.次回車検日
            //               select new Oshirase_Member()
            //               {
            //                   名称 = x.車輌登録番号,
            //                   内容 = "車　検",
            //                   期限 = (DateTime)x.次回車検日,
            //               }
            //                ).ToList();
            //    return ret;
            //}
            return null;
		}


    }
}
