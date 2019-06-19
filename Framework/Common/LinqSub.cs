using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyoeiSystem.Framework.Common
{
	/// <summary>
	/// Linq関連機能
	/// </summary>
	public static class LinqSub
	{
		/// <summary>
		/// 時間のコレクションを全て合計した値を返す
		/// </summary>
		/// <param name="source">時間の値の配列</param>
		/// <returns>合計時間</returns>
		public static decimal? 時間集計(IEnumerable<decimal?> source)
		{
			decimal? result = null;

			foreach (var rec in source)
			{
				if (rec == null)
				{
					continue;
				}
				if (result == null)
				{
					result = rec;
					continue;
				}
				long l1 = (long)Math.Truncate((decimal)rec) * 60 + (long)((rec - decimal.Truncate((decimal)rec)) * 100);
				long l2 = (long)Math.Truncate((decimal)result) * 60 + (long)((result - decimal.Truncate((decimal)result)) * 100);
				long sum = l1 + l2;
				result = (decimal)Math.Truncate((decimal)sum / 60m) + ((decimal)sum % 60m) / 100;
			}

			return 時間TO分(result);
		}

		/// <summary>
		/// ２つの時間を加算した結果を返す
		/// </summary>
		/// <param name="d1">時間値１</param>
		/// <param name="d2">時間値２</param>
		/// <returns>合計時間値</returns>
		public static decimal? 時間加算(decimal? d1, decimal? d2)
		{
			decimal? result = null;

			if (d1 == null)
			{
				return d2;
			}
			if (d2 == null)
			{
				return d1;
			}

			long l1 = (long)Math.Truncate((decimal)d1) * 60 + (long)((d1 - decimal.Truncate((decimal)d1)) * 100);
			long l2 = (long)Math.Truncate((decimal)d2) * 60 + (long)((d2 - decimal.Truncate((decimal)d2)) * 100);
			long sum = l1 + l2;
			result = (decimal)Math.Truncate((decimal)sum / 60m) + ((decimal)sum % 60m) / 100;

			return 時間TO分(result);
		}

		/// <summary>
		/// 時間単位を分単位(hh.mm)に変換する。
		/// <remarks>
		/// 例）1時間15分 の場合 引数：1.25(時間) => 戻り値：1.15
		/// </remarks>
		/// </summary>
		/// <param name="hhmm">時間単位の値</param>
		/// <returns>分単位の値</returns>
		public static decimal? 時間TO分(decimal? hhmm)
		{
			decimal? result = null;
			if (hhmm == null)
			{
				return null;
			}

			result = (Math.Truncate((decimal)hhmm) * 60m) + ((hhmm - decimal.Truncate((decimal)hhmm)) * 100m);

			return result;
		}

		/// <summary>
		/// 分単位(hh.mm)の時間を時間単位に変換する。
		/// <remarks>
		/// 例）1時間15分 の場合 引数：1.15(時間) => 戻り値：1.25
		/// </remarks>
		/// </summary>
		/// <param name="mm">分単位の値</param>
		/// <returns>時間単位の値</returns>
		public static decimal? 分TO時間(decimal? mm)
		{
			decimal? result = null;
			if (mm == null)
			{
				return null;
			}
			decimal hh = Math.Truncate((decimal)mm / 60m);
			result = hh + ((mm % 60) / 100m);

			return result;
		}

		//// 使い方例用データクラス
		//class TEST
		//{
		//	public string key1 { get; set; }
		//	public decimal? 時間 { get; set; }
		//}

		//// 使い方例
		//void 例()
		//{
		//	List<TEST> list = new List<TEST>()
		//		{
		//			new TEST{ key1 = "AAA", 時間 =  null, },
		//			new TEST{ key1 = "ccc", 時間 =  1.2m, },
		//			new TEST{ key1 = "AAA", 時間 = 10.5m, },
		//			new TEST{ key1 = "AAA", 時間 =  1.7m, },
		//			new TEST{ key1 = "BBB", 時間 =  1.3m, },
		//		};

		//	// 配列の中の時間をLOOPで合計する方法
		//	decimal? d = null;
		//	var t = (from r in list select r).ToArray();
		//	foreach (var rec in t)
		//	{
		//		d = LinqSub.時間加算(d, rec.時間);
		//	}

		//	// Linq のGROUP化した結果セットの中で、一発でグループ毎の時間集計を行う方法
		//	var tt = (from r in list
		//			  group r by new { key = r.key1 } into grp
		//			  select new TEST
		//			  {
		//				  key1 = grp.Key.key,
		//				  時間 = LinqSub.時間集計((from g in grp select g.時間)),
		//			  }).ToArray();
		//}
	}
}
