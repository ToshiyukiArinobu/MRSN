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
	public class TKS31010
	{
		/// <summary>
		/// 締日集計処理
		/// </summary>
		/// <returns></returns>
		public int ExecuteSummaryOnTerm(string pickup得意先, int? p得意先FROM, int? p得意先TO, int? p締日, string p集計年月, int p再計算)
		{
			int cnt = -1;
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

				using (var tran = new TransactionScope())
				{
					DateTime updtime = DateTime.Now;

					context.SaveChanges();

					tran.Complete();
				}
			}

			return cnt;
		}

	}
}