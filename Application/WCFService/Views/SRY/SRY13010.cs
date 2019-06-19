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
    public class SRY13010g_Member
    {
        [DataMember]
        public int 車種Key { get; set; }
        [DataMember]
        public decimal 合計 { get; set; }
    }


    #region SRY13010_Member
    public class SRY13010_Member
    {
        [DataMember]
        public int? コード { get; set; }
        [DataMember]
        public string 車種名 { get; set; }
        [DataMember]
        public int? 台数 { get; set; }
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
        public DateTime 集計年月From { get; set; }
        [DataMember]
        public DateTime 集計年月To { get; set; }
        [DataMember]
        public string 車種List { get; set; }
        [DataMember]
        public string 表示順序 { get; set; }
        [DataMember]
        public string コードFrom { get; set; }
        [DataMember]
        public string コードTo { get; set; }
    }
    #endregion


    #region SRY13010_Member_CSV
    public class SRY13010_Member_CSV
    {
        [DataMember]
        public int? コード { get; set; }
        [DataMember]
        public string 車種名 { get; set; }
        [DataMember]
        public int? 台数 { get; set; }
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
    }
    #endregion


    public class SRY13010
    {
        #region SRY13010 印刷

        public List<SRY13010_Member> SRY13010_GetDataList(string s車種From, string s車種To, int?[] i車種List, string s車種List, DateTime d集計期間From, DateTime d集計期間To)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                string 車種指定ﾋﾟｯｸｱｯﾌﾟ = string.Empty;
                int i集計期間From = AppCommon.IntParse(d集計期間From.ToString("yyyyMM"));
                int i集計期間To = AppCommon.IntParse(d集計期間To.ToString("yyyyMM"));
                List<SRY13010_Member> retList = new List<SRY13010_Member>();
                context.Connection.Open();

               　try
                {
                    var 車種自社締日 = (from m87 in context.M87_CNTL.Where(m87 => m87.管理ID == 1)
                                 select new {m87.車輌自社締日}).ToList();
                    int iSime = AppCommon.IntParse(車種自社締日[0].車輌自社締日.ToString());
                    iSime += 1;
                    if ((iSime) >= 32)
                    {
                        iSime = 1;
                    }
                    if (iSime.ToString().Length == 1){
                        if (!DateTime.TryParse(d集計期間From.ToString("yyyy/MM/0") + iSime.ToString(), out d集計期間From))
                        {
                            return retList;
                        }
                    }
                    else
                    {
                        if (!DateTime.TryParse(d集計期間From.ToString("yyyy/MM/") + iSime.ToString(), out d集計期間From))
                        {
                            return retList;
                        }
                    }

                    var query3 = (from m06 in context.M06_SYA.Where(m06 => m06.削除日付 == null)
                                  join m05 in context.M05_CAR.Where(m05 => m05.削除日付 == null) on m06.車種ID equals m05.車種ID into m05Group
                                  select new { m06.車種ID, m06.登録日時, m06.更新日時, m06.車種名, m06.積載重量, m06.削除日付, 台数 = m05Group.Count(m05g => m05g.廃車区分 == 0) }
                        );

                    var query2 = (from s14sb in context.V_車種月次サブ
                                  join m07 in context.M07_KEI on s14sb.経費項目ID equals m07.経費項目ID into m07Group
                                  select new { s14sb.金額, s14sb.経費項目ID, s14sb.経費項目名, s14sb.固定変動区分, s14sb.車種ID, s14sb.集計年月, m07Group.FirstOrDefault().経費区分 }
                        );

                    var query = (from m06 in query3
                                 join s14 in context.V_車種月別売上合計表.Where(c => c.集計年月 >= i集計期間From && c.集計年月 <= i集計期間To) on m06.車種ID equals s14.車種ID into s14Group
                                 join s14sb in query2.Where(c => c.集計年月 >= i集計期間From && c.集計年月 <= i集計期間To) on m06.車種ID equals s14sb.車種ID into s14sbGroup

                                select new SRY13010_Member
                                {
                                    コード = m06.車種ID,
                                    車種名 = m06.車種名,
                                    台数 = m06.台数,
                                    収入日数 = s14Group.Sum(c => c.稼動日数) == 0 ? 0 : Math.Round((decimal)(s14Group.Sum(c => c.運送収入) / s14Group.Sum(c => c.稼動日数)),0),
                                    収入時間 = (s14Group.Sum(c => c.拘束時間) * 60) == 0 ? 0 : Math.Round((decimal)((s14Group.Sum(c => c.運送収入) * 60) / s14Group.Sum(c => c.拘束時間)), 0),
                                    収入KM = s14Group.Sum(c => c.走行ＫＭ) == 0 ? 0 : Math.Round((decimal)(s14Group.Sum(c => c.運送収入) / s14Group.Sum(c => c.走行ＫＭ)), 0),
                                    収入屯数 = s14Group.Sum(c => c.輸送屯数) == 0 ? 0 : Math.Round((decimal)(s14Group.Sum(c => c.運送収入) / s14Group.Sum(c => c.輸送屯数)), 0),
                                    固定費日数 = s14Group.Sum(c => c.稼動日数) == 0 ? 0 : Math.Round((decimal)(s14sbGroup.Where(c => c.固定変動区分 == 0).Sum(c => c.金額) / s14Group.Sum(c => c.稼動日数)), 0),
                                    変動費KM = s14Group.Sum(c => c.走行ＫＭ) == 0 ? 0 : Math.Round((decimal)(s14sbGroup.Where(c => c.固定変動区分 == 1).Sum(c => c.金額) / s14Group.Sum(c => c.走行ＫＭ)), 0),
                                    修理日数 = s14Group.Sum(c => c.稼動日数) == 0 ? 0 : Math.Round((decimal)(s14sbGroup.Where(c => c.経費項目ID == s14sbGroup.Where(d => d.経費区分 == 5).Min(d => d.経費項目ID)).Sum(c => c.金額) / s14Group.Sum(c => c.稼動日数)), 0),
                                    修理KM = s14Group.Sum(c => c.走行ＫＭ) == 0 ? 0 : Math.Round((decimal)(s14sbGroup.Where(c => c.経費項目ID == s14sbGroup.Where(d => d.経費区分 == 5).Min(d => d.経費項目ID)).Sum(c => c.金額) / s14Group.Sum(c => c.走行ＫＭ)), 0),
                                    損益日数 = s14Group.Sum(c => c.稼動日数) == 0 ? 0 : Math.Round((decimal)((s14Group.Sum(c => c.運送収入) - s14sbGroup.Sum(c => c.金額)) / s14Group.Sum(c => c.稼動日数)), 0),
                                    損益KM = s14Group.Sum(c => c.走行ＫＭ) == 0 ? 0 : Math.Round((decimal)((s14Group.Sum(c => c.運送収入) - s14sbGroup.Sum(c => c.金額)) / s14Group.Sum(c => c.走行ＫＭ)), 0),
                                    集計年月From = d集計期間From,
                                    集計年月To = d集計期間To,
                                    コードFrom = s車種From,
                                    コードTo = s車種To,
                                    車種List = 車種指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "無" : 車種指定ﾋﾟｯｸｱｯﾌﾟ
                                }).AsQueryable();

                    query = query.Distinct();
					query = query.Where(c => c.台数 != 0);
                    int i車種FROM;
                    int i車種TO;
                    //部門From処理　Min値
                    if (!string.IsNullOrEmpty(s車種From))
                    {
                        i車種FROM = AppCommon.IntParse(s車種From);
                    }
                    else
                    {
                        i車種FROM = int.MinValue;
                    }

                    //部門To処理　Max値
                    if (!string.IsNullOrEmpty(s車種To))
                    {
                        i車種TO = AppCommon.IntParse(s車種To);
                    }
                    else
                    {
                        i車種TO = int.MaxValue;
                    }

                    var intCause = i車種List;
                    if (string.IsNullOrEmpty(s車種From + s車種To))
                    {
                        if (i車種List.Length > 0)
                        {
                            query = query.Where(q => intCause.Contains(q.コード));
                        }
                    }
                    else
                    {
                        if (i車種List.Length > 0)
                        {
                            query = query.Where(q => intCause.Contains(q.コード) || (q.コード >= i車種FROM && q.コード <= i車種TO));
                        }
                        else
                        {
                            query = query.Where(q => (q.コード >= i車種FROM && q.コード <= i車種TO));
                        }
                    }

                    if (i車種List.Length > 0)
                    {
                        for (int Count = 0; Count < query.Count(); Count++)
                        {
                            車種指定ﾋﾟｯｸｱｯﾌﾟ = 車種指定ﾋﾟｯｸｱｯﾌﾟ + i車種List[Count].ToString();

                            if (Count < i車種List.Length)
                            {

                                if (Count == i車種List.Length - 1)
                                {
                                    break;
                                }

                                車種指定ﾋﾟｯｸｱｯﾌﾟ = 車種指定ﾋﾟｯｸｱｯﾌﾟ + ",";

                            }

                            if (i車種List.Length == 1)
                            {
                                break;
                            }

                        }
                    }

                    query = query.Distinct();  
                    retList = query.ToList();
                    return retList;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion


        #region SRY13010 CSV
        public List<SRY13010_Member_CSV> SRY13010_GetData_CSV(string s車種From, string s車種To, int?[] i車種List, string s車種List, DateTime d集計期間From, DateTime d集計期間To)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {

                int i集計期間From = AppCommon.IntParse(d集計期間From.ToString("yyyyMM"));
                int i集計期間To = AppCommon.IntParse(d集計期間To.ToString("yyyyMM"));
                List<SRY13010_Member_CSV> retList = new List<SRY13010_Member_CSV>();
                context.Connection.Open();

               　try
                {
                    var 車種自社締日 = (from m87 in context.M87_CNTL.Where(m87 => m87.管理ID == 1)
                                 select new {m87.車輌自社締日}).ToList();
                    int iSime = AppCommon.IntParse(車種自社締日[0].車輌自社締日.ToString());
                    iSime += 1;
                    if ((iSime) >= 32)
                    {
                        iSime = 1;
                    }
                    if (iSime.ToString().Length == 1){
                        if (!DateTime.TryParse(d集計期間From.ToString("yyyy/MM/0") + iSime.ToString(), out d集計期間From))
                        {
                            return retList;
                        }
                    }
                    else
                    {
                        if (!DateTime.TryParse(d集計期間From.ToString("yyyy/MM/") + iSime.ToString(), out d集計期間From))
                        {
                            return retList;
                        }
                    }

                    var query3 = (from m06 in context.M06_SYA.Where(m06 => m06.削除日付 == null)
                                  join m05 in context.M05_CAR.Where(m05 => m05.削除日付 == null) on m06.車種ID equals m05.車種ID into m05Group
                                  select new { m06.車種ID, m06.登録日時, m06.更新日時, m06.車種名, m06.積載重量, m06.削除日付, 台数 = m05Group.Count(m05g => m05g.廃車区分 == 0) }
                        );

                    var query2 = (from s14sb in context.V_車種月次サブ
                                  join m07 in context.M07_KEI on s14sb.経費項目ID equals m07.経費項目ID into m07Group
                                  select new { s14sb.金額, s14sb.経費項目ID, s14sb.経費項目名, s14sb.固定変動区分, s14sb.車種ID, s14sb.集計年月, m07Group.FirstOrDefault().経費区分 }
                        );

                    var query = (from m06 in query3
                                 join s14 in context.V_車種月別売上合計表.Where(c => c.集計年月 >= i集計期間From && c.集計年月 <= i集計期間To) on m06.車種ID equals s14.車種ID into s14Group
                                 join s14sb in query2.Where(c => c.集計年月 >= i集計期間From && c.集計年月 <= i集計期間To) on m06.車種ID equals s14sb.車種ID into s14sbGroup

                                 select new SRY13010_Member_CSV
                                {
                                    コード = m06.車種ID,
                                    車種名 = m06.車種名,
                                    台数 = m06.台数,
                                    収入日数 = s14Group.Sum(c => c.稼動日数) == 0 ? 0 : Math.Round((decimal)(s14Group.Sum(c => c.運送収入) / s14Group.Sum(c => c.稼動日数)),0),
                                    収入時間 = (s14Group.Sum(c => c.拘束時間) * 60) == 0 ? 0 : Math.Round((decimal)((s14Group.Sum(c => c.運送収入) * 60) / s14Group.Sum(c => c.拘束時間)), 0),
                                    収入KM = s14Group.Sum(c => c.走行ＫＭ) == 0 ? 0 : Math.Round((decimal)(s14Group.Sum(c => c.運送収入) / s14Group.Sum(c => c.走行ＫＭ)), 0),
                                    収入屯数 = s14Group.Sum(c => c.輸送屯数) == 0 ? 0 : Math.Round((decimal)(s14Group.Sum(c => c.運送収入) / s14Group.Sum(c => c.輸送屯数)), 0),
                                    固定費日数 = s14Group.Sum(c => c.稼動日数) == 0 ? 0 : Math.Round((decimal)(s14sbGroup.Where(c => c.固定変動区分 == 0).Sum(c => c.金額) / s14Group.Sum(c => c.稼動日数)), 0),
                                    変動費KM = s14Group.Sum(c => c.走行ＫＭ) == 0 ? 0 : Math.Round((decimal)(s14sbGroup.Where(c => c.固定変動区分 == 1).Sum(c => c.金額) / s14Group.Sum(c => c.走行ＫＭ)), 0),
                                    修理日数 = s14Group.Sum(c => c.稼動日数) == 0 ? 0 : Math.Round((decimal)(s14sbGroup.Where(c => c.経費項目ID == s14sbGroup.Where(d => d.経費区分 == 5).Min(d => d.経費項目ID)).Sum(c => c.金額) / s14Group.Sum(c => c.稼動日数)), 0),
                                    修理KM = s14Group.Sum(c => c.走行ＫＭ) == 0 ? 0 : Math.Round((decimal)(s14sbGroup.Where(c => c.経費項目ID == s14sbGroup.Where(d => d.経費区分 == 5).Min(d => d.経費項目ID)).Sum(c => c.金額) / s14Group.Sum(c => c.走行ＫＭ)), 0),
                                    損益日数 = s14Group.Sum(c => c.稼動日数) == 0 ? 0 : Math.Round((decimal)((s14Group.Sum(c => c.運送収入) - s14sbGroup.Sum(c => c.金額)) / s14Group.Sum(c => c.稼動日数)), 0),
                                    損益KM = s14Group.Sum(c => c.走行ＫＭ) == 0 ? 0 : Math.Round((decimal)((s14Group.Sum(c => c.運送収入) - s14sbGroup.Sum(c => c.金額)) / s14Group.Sum(c => c.走行ＫＭ)), 0),

                                }).AsQueryable();

					query = query.Distinct();
					query = query.Where(c => c.台数 != 0);
                    int i車種FROM;
                    int i車種TO;
                    //部門From処理　Min値
                    if (!string.IsNullOrEmpty(s車種From))
                    {
                        i車種FROM = AppCommon.IntParse(s車種From);
                    }
                    else
                    {
                        i車種FROM = int.MinValue;
                    }

                    //部門To処理　Max値
                    if (!string.IsNullOrEmpty(s車種To))
                    {
                        i車種TO = AppCommon.IntParse(s車種To);
                    }
                    else
                    {
                        i車種TO = int.MaxValue;
                    }

                    var intCause = i車種List;
                    if (string.IsNullOrEmpty(s車種From + s車種To))
                    {
                        if (i車種List.Length > 0)
                        {
                            query = query.Where(q => intCause.Contains(q.コード));
                        }
                    }
                    else
                    {
                        if (i車種List.Length > 0)
                        {
                            query = query.Where(q => intCause.Contains(q.コード) || (q.コード >= i車種FROM && q.コード <= i車種TO));
                        }
                        else
                        {
                            query = query.Where(q => (q.コード >= i車種FROM && q.コード <= i車種TO));
                        }
                    }

                    query = query.Distinct();  
                    retList = query.ToList();
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