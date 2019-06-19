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
using System.Collections;
using System.Data.Entity;

namespace KyoeiSystem.Application.WCFService
{

    /// <summary>
    /// NNG07010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class NNG07010_Member
    {
        public DateTime? 日付 { get; set; }
        public decimal? 売上金額 { get; set; }
        public decimal? 割増1 { get; set; }
        public decimal? 割増2 { get; set; }
        public decimal? 通行料 { get; set; }
        public decimal? 売上合計 { get; set; }
        public decimal? 傭車使用売上 { get; set; }
        public decimal? 支払金額 { get; set; }
        public decimal? 支払通行料 { get; set; }
        public decimal? 差益 { get; set; }
        public decimal? 差益率 { get; set; }
        public int? 件数 { get; set; }
        public int 未定件数 { get; set; }
        public int? コード { get; set; }
        public string 部門名 { get; set; }
        public DateTime 期間From { get; set; }
        public DateTime 期間To { get; set; }

    }

    /// <summary>
    /// NNG07010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class NNG07010_Date
    {
        public DateTime? 日付 { get; set; }
    }


    /// <summary>
    /// NNG07010  CSV　メンバー
    /// </summary>
    [DataContract]
    public class NNG07010_Member_CSV
    {
        public int? コード { get; set; }
        public string 部門名 { get; set; }
        public DateTime? 日付 { get; set; }
        public decimal? 売上金額 { get; set; }
        public decimal? 割増1 { get; set; }
        public decimal? 割増2 { get; set; }
        public decimal? 通行料 { get; set; }
        public decimal? 売上合計 { get; set; }
        public decimal? 傭車使用売上 { get; set; }
        public decimal? 支払金額 { get; set; }
        public decimal? 支払通行料 { get; set; }
        public decimal? 差益 { get; set; }
        public decimal? 差益率 { get; set; }
        public int? 件数 { get; set; }
        public int 未定件数 { get; set; }

    }


    public class NNG07010
    {
        #region 印刷
        /// <summary>
        /// NNG07010 印刷
        /// </summary>
        /// <param name="p商品ID">部門コード</param>
        /// <returns>T01</returns>
        public List<NNG07010_Member> GetDataList(string p部門From, string p部門To, int?[] i部門List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string s部門List )
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				int bumfrom = AppCommon.IntParse(p部門From) == 0 ? int.MinValue : AppCommon.IntParse(p部門From);
				int bumto = AppCommon.IntParse(p部門To) == 0 ? int.MaxValue : AppCommon.IntParse(p部門To);
				if ((string.IsNullOrEmpty(p部門From + p部門To) && i部門List.Length != 0))
				{
					bumfrom = int.MaxValue;
					bumto = int.MaxValue;
				}

                List<NNG07010_Member> retList = new List<NNG07010_Member>();
                List<NNG07010_Date> retList2 = new List<NNG07010_Date>();

                context.Connection.Open();

                for (DateTime dDate = d集計期間From; dDate <= d集計期間To; dDate = dDate.AddDays(1))
                {
                    retList2.Add(new NNG07010_Date() { 日付 = dDate});
                }

                int[] lst;
                lst = (from m71 in context.M71_BUM
                       let t01l = from t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1))) select t01.自社部門ID
                       where t01l.Contains(m71.自社部門ID) 
                       select m71.自社部門ID).ToArray();


                
                var query = (from retdate in retList2
							 from m71 in context.M71_BUM.Where(c => (c.自社部門ID >= bumfrom && c.自社部門ID <= bumto || (i部門List.Contains(c.自社部門ID))))
                             join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1)) on retdate.日付 equals t01.請求日付 into t01Group
                             let t01l = from t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1))) select t01.自社部門ID
                             where lst.Contains(m71.自社部門ID)
                             orderby m71.自社部門ID, retdate.日付
                              select new NNG07010_Member
                             {
                                 日付 = retdate.日付,
                                 売上金額 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 ),
                                 割増1 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.請求割増１),
                                 割増2 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.請求割増２),
                                 通行料 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.通行料),
                                 売上合計 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料),
                                 傭車使用売上 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付 && t01gr.支払先KEY > 0).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料),
                                 支払金額 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付 && t01gr.支払先KEY > 0).Sum(t01gr => t01gr.支払金額),
                                 支払通行料 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付 && t01gr.支払先KEY > 0).Sum(t01gr => t01gr.支払通行料),
                                 差益 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料 - t01gr.支払金額 - t01gr.支払通行料),
                                 差益率 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料) == 0 ? 0 :
                                            Math.Round((decimal)t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料 - t01gr.支払金額 - t01gr.支払通行料) / t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料),2),
                                 件数 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Count(),
                                 未定件数 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Count(t01gr => t01gr.売上未定区分 == 1),
                                 
                                 コード = m71.自社部門ID,
                                 部門名 = m71.自社部門名,
                                期間From = d集計期間From,
                                期間To = d集計期間To,

                             }).AsQueryable();


				//int i部門FROM;
				//int i部門TO;
				////部門From処理　Min値
				//if (!string.IsNullOrEmpty(p部門From))
				//{
				//	i部門FROM = AppCommon.IntParse(p部門From);
				//}
				//else
				//{
				//	i部門FROM = int.MinValue;
				//}

				////部門To処理　Max値
				//if (!string.IsNullOrEmpty(p部門To))
				//{
				//	i部門TO = AppCommon.IntParse(p部門To);
				//}
				//else
				//{
				//	i部門TO = int.MaxValue;
				//}

				//var intCause = i部門List;
				//if (string.IsNullOrEmpty(p部門From + p部門To))
				//{
				//	if (i部門List.Length > 0)
				//	{
				//		query = query.Where(q => intCause.Contains(q.コード));
				//	}
				//}
				//else
				//{
				//	if (i部門List.Length > 0)
				//	{
				//		query = query.Where(q => intCause.Contains(q.コード) || (q.コード >= i部門FROM && q.コード <= i部門TO));
				//	}
				//	else
				//	{
				//		query = query.Where(q => (q.コード >= i部門FROM && q.コード <= i部門TO));
				//	}
				//}

                query = query.Distinct();
                query = query.OrderBy(q => q.コード).ThenBy(q => q.日付);
                //結果をリスト化
                retList = query.ToList();
                return retList;
            }

        }
        #endregion



        #region CSV出力
        /// <summary>
        /// NNG07010 印刷
        /// </summary>
        /// <param name="p商品ID">部門コード</param>
        /// <returns>T01</returns>
        public List<NNG07010_Member_CSV> GetDataList_CSV(string p部門From, string p部門To, int?[] i部門List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string s部門List)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				int bumfrom = AppCommon.IntParse(p部門From) == 0 ? int.MinValue : AppCommon.IntParse(p部門From);
				int bumto = AppCommon.IntParse(p部門To) == 0 ? int.MaxValue : AppCommon.IntParse(p部門To);
				if ((string.IsNullOrEmpty(p部門From + p部門To) && i部門List.Length != 0))
				{
					bumfrom = int.MaxValue;
					bumto = int.MaxValue;
				}

                List<NNG07010_Member_CSV> retList = new List<NNG07010_Member_CSV>();
                List<NNG07010_Date> retList2 = new List<NNG07010_Date>();

                context.Connection.Open();

                for (DateTime dDate = d集計期間From; dDate <= d集計期間To; dDate = dDate.AddDays(1))
                {
                    retList2.Add(new NNG07010_Date() { 日付 = dDate });
                }

                int[] lst;
                lst = (from m71 in context.M71_BUM
                       let t01l = from t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1))) select t01.自社部門ID
                       where t01l.Contains(m71.自社部門ID)
                       select m71.自社部門ID).ToArray();



                var query = (from retdate in retList2
							 from m71 in context.M71_BUM.Where(c => (c.自社部門ID >= bumfrom && c.自社部門ID <= bumto || (i部門List.Contains(c.自社部門ID))))
							 join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1)) on retdate.日付 equals t01.請求日付 into t01Group
                             let t01l = from t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1))) select t01.自社部門ID
                             where lst.Contains(m71.自社部門ID)
                             orderby m71.自社部門ID, retdate.日付
                             select new NNG07010_Member_CSV
                             {
                                 日付 = retdate.日付,
                                 売上金額 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額),
                                 割増1 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.請求割増１),
                                 割増2 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.請求割増２),
                                 通行料 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.通行料),
                                 売上合計 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料),
                                 傭車使用売上 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付 && t01gr.支払先KEY > 0).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料),
                                 支払金額 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付 && t01gr.支払先KEY > 0).Sum(t01gr => t01gr.支払金額),
                                 支払通行料 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付 && t01gr.支払先KEY > 0).Sum(t01gr => t01gr.支払通行料),
                                 差益 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料 - t01gr.支払金額 - t01gr.支払通行料),
                                 差益率 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料) == 0 ? 0 :
                                            Math.Round((decimal)t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料 - t01gr.支払金額 - t01gr.支払通行料) / t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料), 2),
                                 件数 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Count(),
                                 未定件数 = t01Group.Where(t01gr => t01gr.自社部門ID == m71.自社部門ID && t01gr.請求日付 == retdate.日付).Count(t01gr => t01gr.売上未定区分 == 1),

                                 コード = m71.自社部門ID,
                                 部門名 = m71.自社部門名,

                             }).AsQueryable();


				//int i部門FROM;
				//int i部門TO;
				////部門From処理　Min値
				//if (!string.IsNullOrEmpty(p部門From))
				//{
				//	i部門FROM = AppCommon.IntParse(p部門From);
				//}
				//else
				//{
				//	i部門FROM = int.MinValue;
				//}

				////部門To処理　Max値
				//if (!string.IsNullOrEmpty(p部門To))
				//{
				//	i部門TO = AppCommon.IntParse(p部門To);
				//}
				//else
				//{
				//	i部門TO = int.MaxValue;
				//}

				//var intCause = i部門List;
				//if (string.IsNullOrEmpty(p部門From + p部門To))
				//{
				//	if (i部門List.Length > 0)
				//	{
				//		query = query.Where(q => intCause.Contains(q.コード));
				//	}
				//}
				//else
				//{
				//	if (i部門List.Length > 0)
				//	{
				//		query = query.Where(q => intCause.Contains(q.コード) || (q.コード >= i部門FROM && q.コード <= i部門TO));
				//	}
				//	else
				//	{
				//		query = query.Where(q => (q.コード >= i部門FROM && q.コード <= i部門TO));
				//	}
				//}

                query = query.Distinct();
                query = query.OrderBy(q => q.コード).ThenBy(q => q.日付);
                //結果をリスト化
                retList = query.ToList();
                return retList;
            }

        }

        #endregion
    }
}