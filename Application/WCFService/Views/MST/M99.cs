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

namespace KyoeiSystem.Application.WCFService
{
	/// <summary>
	/// 請求書関連機能
	/// </summary>
	public class M99
	{
		/// <summary>
		/// クリスタルレポート定義ファイルデータ取得
        /// </summary>
        /// <returns>W_TKS01010_01_Memberのリスト</returns>
		public List<M99_RPT> GetReportDefine()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
				var ret = (from rpt in context.M99_RPT select rpt).ToList();

				return ret;
            }
        }

		/// <summary>
		/// クリスタルレポート定義ファイルデータ更新
		/// </summary>
		/// <returns>W_TKS01010_01_Memberのリスト</returns>
		public void PutReportDefine(string id, string name, byte[] data)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
                // トランザクションのインスタンス化(開始)
                using (var tran = new TransactionScope())
                {
					context.Connection.Open();
					var rpt = (from x in context.M99_RPT
							   where x.帳票ID == id
							   select x).FirstOrDefault();
					if (data == null)
					{
						if (rpt != null)
						{
							context.M99_RPT.DeleteObject(rpt);
						}
					}
					else
					{
						if (rpt == null)
						{
							rpt = new M99_RPT();
						}
						rpt.帳票ID = id;
						rpt.帳票名 = name;
						rpt.レポート定義データ = data;
						context.M99_RPT.ApplyChanges(rpt);
					}

					context.SaveChanges();

					tran.Complete();
				}

				return ;
			}

		}


	}
}