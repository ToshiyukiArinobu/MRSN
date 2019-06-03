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
    public class SRY10010g_Member
    {
        [DataMember]
        public int 車輌Key { get; set; }
        [DataMember]
        public decimal 合計 { get; set; }
    }


    #region SRY10010_Member

	public class SRY10010_Member
	{
		[DataMember]
		public int? コード { get; set; }
		[DataMember]
		public string 車輌番号 { get; set; }
		[DataMember]
		public DateTime? 廃車日 { get; set; }
		[DataMember]
		public string 乗務員名 { get; set; }
		[DataMember]
		public decimal? 収入日数 { get; set; }
		[DataMember]
		public decimal? 収入時間 { get; set; }
		[DataMember]
		public decimal? 収入KM { get; set; }
		[DataMember]
		public decimal? 収入屯数 { get; set; }
		[DataMember]
		public decimal? 固定費日数 { get; set; }
		[DataMember]
		public decimal? 変動費KM { get; set; }
		[DataMember]
		public decimal? 修理日数 { get; set; }
		[DataMember]
		public decimal? 修理KM { get; set; }
		[DataMember]
		public decimal? 損益日数 { get; set; }
		[DataMember]
		public decimal? 損益KM { get; set; }
		[DataMember]
		public decimal? 稼働率 { get; set; }
		[DataMember]
		public DateTime 集計年月From { get; set; }
		[DataMember]
		public DateTime 集計年月To { get; set; }
		[DataMember]
		public string 車輌List { get; set; }
		[DataMember]
		public string 表示順序 { get; set; }
		[DataMember]
		public string コードFrom { get; set; }
		[DataMember]
		public string コードTo { get; set; }
	}

	public class SRY10010_jikan
	{
		[DataMember]
		public int? コード { get; set; }
		[DataMember]
		public int? KEY { get; set; }
		[DataMember]
		public decimal? 拘束時間 { get; set; }
	}

    #endregion

    #region SRY10010_Member_CSV

    public class SRY10010_Member_CSV
    {
        [DataMember]
		public int? コード { get; set; }
		[DataMember]
		public string 車輌番号 { get; set; }
		[DataMember]
		public DateTime? 廃車日 { get; set; }
        [DataMember]
        public string 乗務員名 { get; set; }
        [DataMember]
        public decimal? 収入日数 { get; set; }
        [DataMember]
        public decimal? 収入時間 { get; set; }
        [DataMember]
        public decimal? 収入KM { get; set; }
        [DataMember]
        public decimal? 収入屯数 { get; set; }
        [DataMember]
        public decimal? 固定費日数 { get; set; }
        [DataMember]
        public decimal? 変動費KM { get; set; }
        [DataMember]
        public decimal? 修理日数 { get; set; }
        [DataMember]
        public decimal? 修理KM { get; set; }
        [DataMember]
        public decimal? 損益日数 { get; set; }
        [DataMember]
        public decimal? 損益KM { get; set; }
        [DataMember]
        public decimal? 稼働率 { get; set; }
    }
    #endregion


    public class SRY10010
    {
        #region SRY10010 印刷

        public List<SRY10010_Member> SRY10010_GetDataList(string s車輌From, string s車輌To, int?[] i車輌List, string s車輌List, DateTime d集計期間From, DateTime d集計期間To)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                string 車輌指定ﾋﾟｯｸｱｯﾌﾟ = string.Empty;
                int i集計期間From = AppCommon.IntParse(d集計期間From.ToString("yyyyMM"));
                int i集計期間To = AppCommon.IntParse(d集計期間To.ToString("yyyyMM"));
                List<SRY10010_Member> retList = new List<SRY10010_Member>();
                context.Connection.Open();

               　try
                {
                    //var 車輌自社締日 = (from m87 in context.M87_CNTL.Where(m87 => m87.管理ID == 1)
                    //             select new {m87.車輌自社締日}).ToList();
                    ////int iSime = AppCommon.IntParse(車輌自社締日[0].車輌自社締日.ToString());
                    //iSime += 1;
                    //if ((iSime) >= 32)
                    //{
                    //    iSime = 1;
                    //}
                    //if (iSime.ToString().Length == 1){
                    //    if (!DateTime.TryParse(d集計期間From.ToString("yyyy/MM/0") + iSime.ToString(), out d集計期間From))
                    //    {
                    //        return retList;
                    //    }
                    //}
                    //else
                    //{
                    //    if (!DateTime.TryParse(d集計期間From.ToString("yyyy/MM/") + iSime.ToString(), out d集計期間From))
                    //    {
                    //        return retList;
                    //    }
                    //}
					
					//var time = (from s14 in context.V_S14_CAR.Where(c => c.集計年月 >= i集計期間From && c.集計年月 <= i集計期間To)
					//			select new SRY10010_jikan
					//			{
					//				KEY = s14.車輌KEY,
					//				コード = (from m05 in context.M05_CAR.Where(c => c.車輌KEY == s14.車輌KEY) select m05.車輌ID).FirstOrDefault(),
					//				拘束時間 = s14.拘束時間,
					//			}).ToList();
					//List<SRY10010_jikan> time2 = new List<SRY10010_jikan>();

					//foreach (var rec in time)
					//{
					//	// 各時間項目の時分を、分に変換する。
					//	rec.拘束時間 = LinqSub.時間TO分(rec.拘束時間);
					//	time2.Add(rec);
					//}

                    var query2 = (from s14sb in context.S14_CARSB
                                  join m07 in context.M07_KEI on s14sb.経費項目ID equals m07.経費項目ID into m07Group
                                  select new { s14sb.金額, s14sb.経費項目ID, s14sb.経費項目名,s14sb.固定変動区分,s14sb.更新日時,s14sb.車輌KEY,s14sb.集計年月,s14sb.登録日時,m07Group.FirstOrDefault().経費区分 }
                        );

                    var query = (from m05 in context.M05_CAR.Where(m05 => m05.削除日付 == null)
                                 join m04 in context.M04_DRV on m05.乗務員KEY equals m04.乗務員KEY into m04Group
                                 join s14 in context.V_S14_CAR.Where(c => c.集計年月 >= i集計期間From && c.集計年月 <= i集計期間To) on m05.車輌KEY equals s14.車輌KEY into s14Group
                                 join s14sb in query2.Where(c => c.集計年月 >= i集計期間From && c.集計年月 <= i集計期間To) on m05.車輌KEY equals s14sb.車輌KEY into s14sbGroup
                                select new SRY10010_Member
                                {
                                    コード = m05.車輌ID,
                                    車輌番号 = m05.車輌番号,
									廃車日 = m05.廃車日,
                                    乗務員名 = m04Group.FirstOrDefault().乗務員名,
                                    収入日数 = s14Group.Sum(c => c.稼動日数) == 0 ? 0 : Math.Round(s14Group.Sum(c => c.運送収入) / s14Group.Sum(c => c.稼動日数), 0),
									収入時間 = (s14Group.Sum(c => c.拘束時間)) == 0 ? 0 : Math.Round((s14Group.Sum(c => c.運送収入)) / (decimal)((s14Group.Sum(c => c.拘束時間)) / 60), 0),
                                    収入KM = s14Group.Sum(c => c.走行ＫＭ) == 0 ? 0 : Math.Round(s14Group.Sum(c => c.運送収入) / s14Group.Sum(c => c.走行ＫＭ), 0),
                                    収入屯数 = s14Group.Sum(c => c.輸送屯数) == 0 ? 0 : Math.Round(s14Group.Sum(c => c.運送収入) / s14Group.Sum(c => c.輸送屯数), 0),
                                    固定費日数 = s14Group.Sum(c => c.稼動日数) == 0 ? 0 : Math.Round(s14sbGroup.Where(c => c.固定変動区分 == 0).Sum(c => c.金額) / s14Group.Sum(c => c.稼動日数), 0),
                                    変動費KM = s14Group.Sum(c => c.走行ＫＭ) == 0 ? 0 : Math.Round(s14sbGroup.Where(c => c.固定変動区分 == 1).Sum(c => c.金額) / s14Group.Sum(c => c.走行ＫＭ), 0),
                                    修理日数 = s14Group.Sum(c => c.稼動日数) == 0 ? 0 : Math.Round(s14sbGroup.Where(c => c.経費項目ID == s14sbGroup.Where(d => d.経費区分 == 5).Min(d => d.経費項目ID)).Sum(c => c.金額) / s14Group.Sum(c => c.稼動日数), 0),
                                    修理KM = s14Group.Sum(c => c.走行ＫＭ) == 0 ? 0 : Math.Round(s14sbGroup.Where(c => c.経費項目ID == s14sbGroup.Where(d => d.経費区分 == 5).Min(d => d.経費項目ID)).Sum(c => c.金額) / s14Group.Sum(c => c.走行ＫＭ), 0),
                                    損益日数 = s14Group.Sum(c => c.稼動日数) == 0 ? 0 : Math.Round((s14Group.Sum(c => c.運送収入) - s14sbGroup.Sum(c => c.金額)) / s14Group.Sum(c => c.稼動日数), 0),
									損益KM = s14Group.Sum(c => c.走行ＫＭ) == 0 ? 0 : Math.Round((s14Group.Sum(c => c.運送収入) - s14sbGroup.Sum(c => c.金額)) / s14Group.Sum(c => c.走行ＫＭ), 0),
									稼働率 = s14Group.Sum(c => c.稼動日数) == 0 ? 0 : Math.Round((decimal)(s14Group.Sum(c => c.拘束時間) / (s14Group.Sum(c => c.稼動日数) * 1440)), 2),
									//稼働率 = s14Group.Sum(c => c.稼動日数) == 0 ? 0 : (s14Group.Sum(c => c.稼動日数) * 1440),
                                    集計年月From = d集計期間From,
                                    集計年月To = d集計期間To,
                                    コードFrom = s車輌From,
                                    コードTo = s車輌To,
                                    車輌List = 車輌指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "無" : 車輌指定ﾋﾟｯｸｱｯﾌﾟ
                                }).AsQueryable();

					//foreach (SRY10010_Member row in query)
					//{
					//	foreach (SRY10010_jikan row2 in time2)
					//	{
					//		if (row.コード == row2.コード)
					//		{
					//			row.稼働率 = row.稼働率 == null ? 0 : row.稼働率 == 0 ? 0 : Math.Round((decimal)row2.拘束時間 / (decimal)row.稼働率, MidpointRounding.AwayFromZero);
					//		}
					//	}
					//}

                    query = query.Distinct();
                    int i車輌FROM;
                    int i車輌TO;
                    //部門From処理　Min値
                    if (!string.IsNullOrEmpty(s車輌From))
                    {
                        i車輌FROM = AppCommon.IntParse(s車輌From);
                    }
                    else
                    {
                        i車輌FROM = int.MinValue;
                    }

                    //部門To処理　Max値
                    if (!string.IsNullOrEmpty(s車輌To))
                    {
                        i車輌TO = AppCommon.IntParse(s車輌To);
                    }
                    else
                    {
                        i車輌TO = int.MaxValue;
                    }

                    var intCause = i車輌List;
                    if (string.IsNullOrEmpty(s車輌From + s車輌To))
                    {
                        if (i車輌List.Length > 0)
                        {
                            query = query.Where(q => intCause.Contains(q.コード));
                        }
                    }
                    else
                    {
                        if (i車輌List.Length > 0)
                        {
                            query = query.Where(q => intCause.Contains(q.コード) || (q.コード >= i車輌FROM && q.コード <= i車輌TO));
                        }
                        else
                        {
                            query = query.Where(q => (q.コード >= i車輌FROM && q.コード <= i車輌TO));
                        }
                    }

                    if (i車輌List.Length > 0)
                    {
                        for (int Count = 0; Count < query.Count(); Count++)
                        {
                            車輌指定ﾋﾟｯｸｱｯﾌﾟ = 車輌指定ﾋﾟｯｸｱｯﾌﾟ + i車輌List[Count].ToString();

                            if (Count < i車輌List.Length)
                            {

                                if (Count == i車輌List.Length - 1)
                                {
                                    break;
                                }

                                車輌指定ﾋﾟｯｸｱｯﾌﾟ = 車輌指定ﾋﾟｯｸｱｯﾌﾟ + ",";

                            }

                            if (i車輌List.Length == 1)
                            {
                                break;
                            }

                        }
                    }

                    query = query.Distinct();
					retList = query.Where(c => c.廃車日 == null || ((((DateTime)c.廃車日).Year * 100 + ((DateTime)c.廃車日).Month) >= i集計期間From)).ToList();
					//retList = query.ToList();
                    return retList;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion



        #region SRY10010 CSV
        public List<SRY10010_Member_CSV> SRY10010_GetData_CSV(string s車輌From, string s車輌To, int?[] i車輌List, string s車輌List, DateTime d集計期間From, DateTime d集計期間To)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {

                int i集計期間From = AppCommon.IntParse(d集計期間From.ToString("yyyyMM"));
                int i集計期間To = AppCommon.IntParse(d集計期間To.ToString("yyyyMM"));
                List<SRY10010_Member_CSV> retList = new List<SRY10010_Member_CSV>();
                context.Connection.Open();

                try
                {
                    //var 車輌自社締日 = (from m87 in context.M87_CNTL.Where(m87 => m87.管理ID == 1)
                    //             select new {m87.車輌自社締日}).ToList();
                    ////int iSime = AppCommon.IntParse(車輌自社締日[0].車輌自社締日.ToString());
                    //iSime += 1;
                    //if ((iSime) >= 32)
                    //{
                    //    iSime = 1;
                    //}
                    //if (iSime.ToString().Length == 1){
                    //    if (!DateTime.TryParse(d集計期間From.ToString("yyyy/MM/0") + iSime.ToString(), out d集計期間From))
                    //    {
                    //        return retList;
                    //    }
                    //}
                    //else
                    //{
                    //    if (!DateTime.TryParse(d集計期間From.ToString("yyyy/MM/") + iSime.ToString(), out d集計期間From))
                    //    {
                    //        return retList;
                    //    }
                    //}

                    var query2 = (from s14sb in context.S14_CARSB
                                  join m07 in context.M07_KEI on s14sb.経費項目ID equals m07.経費項目ID into m07Group
                                  select new { s14sb.金額, s14sb.経費項目ID, s14sb.経費項目名, s14sb.固定変動区分, s14sb.更新日時, s14sb.車輌KEY, s14sb.集計年月, s14sb.登録日時, m07Group.FirstOrDefault().経費区分 }
                        );

                    var query = (from m05 in context.M05_CAR.Where(m05 => m05.削除日付 == null)
                                 join m04 in context.M04_DRV on m05.乗務員KEY equals m04.乗務員KEY into m04Group
                                 join s14 in context.V_S14_CAR.Where(c => c.集計年月 >= i集計期間From && c.集計年月 <= i集計期間To) on m05.車輌KEY equals s14.車輌KEY into s14Group
                                 join s14sb in query2.Where(c => c.集計年月 >= i集計期間From && c.集計年月 <= i集計期間To) on m05.車輌KEY equals s14sb.車輌KEY into s14sbGroup
                                 select new SRY10010_Member_CSV
                                 {
                                     コード = m05.車輌ID,
                                     車輌番号 = m05.車輌番号,
									 廃車日 = m05.廃車日,
                                     乗務員名 = m04Group.FirstOrDefault().乗務員名,
                                     収入日数 = s14Group.Sum(c => c.稼動日数) == 0 ? 0 : Math.Round(s14Group.Sum(c => c.運送収入) / s14Group.Sum(c => c.稼動日数), 0),
									 収入時間 = (s14Group.Sum(c => c.拘束時間) * 60) == 0 ? 0 : Math.Round((s14Group.Sum(c => c.運送収入)) / (decimal)((s14Group.Sum(c => c.拘束時間)) / 60), 0),
                                     収入KM = s14Group.Sum(c => c.走行ＫＭ) == 0 ? 0 : Math.Round(s14Group.Sum(c => c.運送収入) / s14Group.Sum(c => c.走行ＫＭ), 0),
                                     収入屯数 = s14Group.Sum(c => c.輸送屯数) == 0 ? 0 : Math.Round(s14Group.Sum(c => c.運送収入) / s14Group.Sum(c => c.輸送屯数), 0),
                                     固定費日数 = s14Group.Sum(c => c.稼動日数) == 0 ? 0 : Math.Round(s14sbGroup.Where(c => c.固定変動区分 == 0).Sum(c => c.金額) / s14Group.Sum(c => c.稼動日数), 0),
                                     変動費KM = s14Group.Sum(c => c.走行ＫＭ) == 0 ? 0 : Math.Round(s14sbGroup.Where(c => c.固定変動区分 == 1).Sum(c => c.金額) / s14Group.Sum(c => c.走行ＫＭ), 0),
                                     修理日数 = s14Group.Sum(c => c.稼動日数) == 0 ? 0 : Math.Round(s14sbGroup.Where(c => c.経費項目ID == s14sbGroup.Where(d => d.経費区分 == 5).Min(d => d.経費項目ID)).Sum(c => c.金額) / s14Group.Sum(c => c.稼動日数), 0),
                                     修理KM = s14Group.Sum(c => c.走行ＫＭ) == 0 ? 0 : Math.Round(s14sbGroup.Where(c => c.経費項目ID == s14sbGroup.Where(d => d.経費区分 == 5).Min(d => d.経費項目ID)).Sum(c => c.金額) / s14Group.Sum(c => c.走行ＫＭ), 0),
                                     損益日数 = s14Group.Sum(c => c.稼動日数) == 0 ? 0 : Math.Round((s14Group.Sum(c => c.運送収入) - s14sbGroup.Sum(c => c.金額)) / s14Group.Sum(c => c.稼動日数), 0),
                                     損益KM = s14Group.Sum(c => c.走行ＫＭ) == 0 ? 0 : Math.Round((s14Group.Sum(c => c.運送収入) - s14sbGroup.Sum(c => c.金額)) / s14Group.Sum(c => c.走行ＫＭ), 0),
									 稼働率 = s14Group.Sum(c => c.稼動日数) == 0 ? 0 : Math.Round((decimal)(s14Group.Sum(c => c.拘束時間) / (s14Group.Sum(c => c.稼動日数) * 1440)), 2),

                                 }).AsQueryable();

                    query = query.Distinct();
                    int i車輌FROM;
                    int i車輌TO;
                    //部門From処理　Min値
                    if (!string.IsNullOrEmpty(s車輌From))
                    {
                        i車輌FROM = AppCommon.IntParse(s車輌From);
                    }
                    else
                    {
                        i車輌FROM = int.MinValue;
                    }

                    //部門To処理　Max値
                    if (!string.IsNullOrEmpty(s車輌To))
                    {
                        i車輌TO = AppCommon.IntParse(s車輌To);
                    }
                    else
                    {
                        i車輌TO = int.MaxValue;
                    }

                    var intCause = i車輌List;
                    if (string.IsNullOrEmpty(s車輌From + s車輌To))
                    {
                        if (i車輌List.Length > 0)
                        {
                            query = query.Where(q => intCause.Contains(q.コード));
                        }
                    }
                    else
                    {
                        if (i車輌List.Length > 0)
                        {
                            query = query.Where(q => intCause.Contains(q.コード) || (q.コード >= i車輌FROM && q.コード <= i車輌TO));
                        }
                        else
                        {
                            query = query.Where(q => (q.コード >= i車輌FROM && q.コード <= i車輌TO));
                        }
                    }

					query = query.Distinct();
					retList = query.Where(c => c.廃車日 == null || ((((DateTime)c.廃車日).Year * 100 + ((DateTime)c.廃車日).Month) >= i集計期間From)).ToList();
                    return retList;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion


    }
}