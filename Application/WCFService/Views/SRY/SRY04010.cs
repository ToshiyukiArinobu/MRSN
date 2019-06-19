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
    /// SRY04010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class SRY04010_Member
    {
        public DateTime? 日付 { get; set; }
        public decimal? 社内金額 { get; set; }
        public int? 経費1 { get; set; }
        public int? 経費2 { get; set; }
        public int? 経費3 { get; set; }
        public int? 経費4 { get; set; }
        public int? 経費5 { get; set; }
        public int? 経費6 { get; set; }
        public int? 経費7 { get; set; }
        public int? その他経費 { get; set; }
        public decimal? 燃料Ｌ数 { get; set; }
        public decimal? 燃料代 { get; set; }
        public decimal? 輸送屯数 { get; set; }
        public int? 走行KM { get; set; }
        public int? 実車KM { get; set; }

        public int? コード { get; set; }
        public string 車種名 { get; set; }
        public DateTime 期間From { get; set; }
        public DateTime 期間To { get; set; }

        public string 経費名1 { get; set; }
        public string 経費名2 { get; set; }
        public string 経費名3 { get; set; }
        public string 経費名4 { get; set; }
        public string 経費名5 { get; set; }
        public string 経費名6 { get; set; }
        public string 経費名7 { get; set; }

    }

    /// <summary>
    /// SRY04010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class SRY04010_Date
    {
        public DateTime? 日付 { get; set; }
    }

    /// <summary>
    /// SRY04010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class SRY04010_KTRN
    {
        public DateTime? 日付 { get; set; }
        public int? 経費項目ID { get; set; }
        public decimal? 数量 { get; set; }
        public int? 金額 { get; set; }
        public int? 車種KEY { get; set; }
    }

    /// <summary>
    /// SRY04010  CSV　メンバー
    /// </summary>
    [DataContract]
    public class SRY04010_Member_CSV
    {
        public int? コード { get; set; }
        public string 車種名 { get; set; }
        public DateTime? 日付 { get; set; }
        public decimal? 社内金額 { get; set; }
        public int? 経費1 { get; set; }
        public int? 経費2 { get; set; }
        public int? 経費3 { get; set; }
        public int? 経費4 { get; set; }
        public int? 経費5 { get; set; }
        public int? 経費6 { get; set; }
        public int? 経費7 { get; set; }
        public int? その他経費 { get; set; }
        public decimal? 燃料Ｌ数 { get; set; }
        public decimal? 燃料代 { get; set; }
        public decimal? 輸送屯数 { get; set; }
        public int? 走行KM { get; set; }
        public int? 実車KM { get; set; }

        public string 経費名1 { get; set; }
        public string 経費名2 { get; set; }
        public string 経費名3 { get; set; }
        public string 経費名4 { get; set; }
        public string 経費名5 { get; set; }
        public string 経費名6 { get; set; }
        public string 経費名7 { get; set; }

    }

    [DataContract]
    public class SRY04010_CAR
    {
        [DataMember]
        public int 車種ID { get; set; }
    }


    [DataContract]
    public class SRY04010_M07
    {
        [DataMember]
        public int 経費ID { get; set; }
        [DataMember]
        public string 経費名 { get; set; }
    }


    public class SRY04010
    {
        #region 印刷
        /// <summary>
        /// SRY04010 印刷
        /// </summary>
        /// <param name="p商品ID">車種コード</param>
        /// <returns>T01</returns>
        public List<SRY04010_Member> GetDataList(string p車種From, string p車種To, int?[] i車種List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string s車種List)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				int syafrom = AppCommon.IntParse(p車種From) == 0 ? int.MinValue : AppCommon.IntParse(p車種From);
				int syato = AppCommon.IntParse(p車種To) == 0 ? int.MaxValue : AppCommon.IntParse(p車種To);
				if ((string.IsNullOrEmpty(p車種From + p車種To) && i車種List.Length != 0))
				{
					syafrom = int.MaxValue;
					syato = int.MaxValue;
				}

                List<SRY04010_Member> retList = new List<SRY04010_Member>();
                List<SRY04010_Date> retList2 = new List<SRY04010_Date>();

                context.Connection.Open();

                for (DateTime dDate = d集計期間From; dDate <= d集計期間To; dDate = dDate.AddDays(1))
                {
                    retList2.Add(new SRY04010_Date() { 日付 = dDate });
                }

                //var query3 = (from m05 in context.M05_CAR
                //              let t03l = from t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) select t03.車輌ID
                //              where t03l.Contains(m05.)
                //              select new SRY04010_CAR
                //              {
                //                  車種ID = m05.車種ID,
                //              }).AsQueryable();

                int[] lst;
                lst = (from m06 in context.M06_SYA.Where(m06 => m06.削除日付 == null)
                       let t01l = from t01 in context.T01_TRN.Where(t01 => t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) select t01.車種ID
                       let t02l = from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) select t02.車種ID
                       where t01l.Contains(m06.車種ID) || t02l.Contains(m06.車種ID)
                       select m06.車種ID).ToArray();

                var query2 = (from t in context.T03_KTRN
                              join m07 in context.M07_KEI on t.経費項目ID equals m07.経費項目ID
                              join m05 in context.M05_CAR on t.車輌ID equals m05.車輌KEY into m05Group
                              from m05g in m05Group
                              join m06 in context.M06_SYA.Where(m06 => m06.削除日付 == null) on m05g.車種ID equals m06.車種ID into m06Group
                              from m06g in m06Group
                              where m07.固定変動区分 == 1 && t.経費発生日 >= d集計期間From && t.経費発生日 <= d集計期間To
                              select new SRY04010_KTRN
                             {
                                 日付 = t.経費発生日,
                                 経費項目ID = t.経費項目ID,
                                 数量 = t.数量,
                                 金額 = t.金額,
                                 車種KEY = m06g.車種ID,
                             }).AsQueryable();

                int?[] lst2;
                lst2 = query2.Select(c => c.車種KEY).ToArray();

                query2 = query2.Where(c => c.金額 != 0);

                var query = (from retdate in retList2
                             join t01 in context.T01_TRN.Where(t01 => (t01.支払日付 >= d集計期間From && t01.支払日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) on retdate.日付 equals t01.支払日付 into t01Group
                             join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on retdate.日付 equals t02.労務日 into t02Group
                             join t03 in query2.Where(t03 => t03.日付 >= d集計期間From && t03.日付 <= d集計期間To) on retdate.日付 equals t03.日付 into t03Group
							 from m06 in context.M06_SYA.Where(m06 => (m06.削除日付 == null) && (m06.車種ID >= syafrom && m06.車種ID <= syato || (i車種List.Contains(m06.車種ID))))
                             where lst.Contains(m06.車種ID) || lst2.Contains(m06.車種ID) 
                             orderby m06.車種ID, retdate.日付
                             select new SRY04010_Member
                             {
                                 日付 = retdate.日付,
                                 社内金額 = t01Group.Where(t01gr => t01gr.車種ID == m06.車種ID && t01gr.支払日付 == retdate.日付 && t01gr.支払先KEY == null).Sum(t01gr => t01gr.支払金額 + t01gr.支払割増１ + t01gr.支払割増２ + t01gr.支払通行料) == null ? 0 : t01Group.Where(t01gr => t01gr.車種ID == m06.車種ID && t01gr.支払日付 == retdate.日付 && t01gr.支払先KEY == null).Sum(t01gr => t01gr.支払金額 + t01gr.支払割増１ + t01gr.支払割増２ + t01gr.支払通行料),
                                 経費1 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 601).Sum(t03gr => t03gr.金額),
                                 経費2 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 602).Sum(t03gr => t03gr.金額),
                                 経費3 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 603).Sum(t03gr => t03gr.金額),
                                 経費4 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 604).Sum(t03gr => t03gr.金額),
                                 経費5 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 605).Sum(t03gr => t03gr.金額),
                                 経費6 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 606).Sum(t03gr => t03gr.金額),
                                 経費7 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 607).Sum(t03gr => t03gr.金額),
                                 その他経費 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 &&
                                                            (t03gr.経費項目ID != 601 && t03gr.経費項目ID != 602 && t03gr.経費項目ID != 604 && t03gr.経費項目ID != 605 &&
                                                             t03gr.経費項目ID != 608 && t03gr.経費項目ID != 607 && t03gr.経費項目ID != 401)).Sum(t03gr => t03gr.金額),
                                 燃料Ｌ数 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.数量),
                                 燃料代 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.金額),
                                 輸送屯数 = t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数) == null ? 0 : t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数),
                                 走行KM = t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ),
                                 実車KM = t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ),
                                 コード = m06.車種ID,
                                 車種名 = m06.車種名,
                                 期間From = d集計期間From,
                                 期間To = d集計期間To,

                                 経費名1 = context.M07_KEI.Where(q => q.経費項目ID == 601).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名2 = context.M07_KEI.Where(q => q.経費項目ID == 602).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名3 = context.M07_KEI.Where(q => q.経費項目ID == 603).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名4 = context.M07_KEI.Where(q => q.経費項目ID == 604).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名5 = context.M07_KEI.Where(q => q.経費項目ID == 605).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名6 = context.M07_KEI.Where(q => q.経費項目ID == 606).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名7 = context.M07_KEI.Where(q => q.経費項目ID == 607).Select(c => c.経費項目名).FirstOrDefault(),

                             }).AsQueryable().Distinct();

				//if (!(string.IsNullOrEmpty(p車種From + p車種To) && i車種List.Length == 0))
				//{

				//	//車種が検索対象に入っていない時全データ取得
				//	if (string.IsNullOrEmpty(p車種From + p車種To))
				//	{
				//		query = query.Where(c => c.コード >= int.MaxValue);
				//	}

				//	//車種From処理　Min値
				//	if (!string.IsNullOrEmpty(p車種From))
				//	{
				//		int i車種FROM = AppCommon.IntParse(p車種From);
				//		query = query.Where(c => c.コード >= i車種FROM);
				//	}

				//	//車種To処理　Max値
				//	if (!string.IsNullOrEmpty(p車種To))
				//	{
				//		int i車種TO = AppCommon.IntParse(p車種To);
				//		query = query.Where(c => c.コード <= i車種TO);
				//	}


				//	if (i車種List.Length > 0)
				//	{
				//		var intCause = i車種List;
				//		query = query.Union(from retdate in retList2
				//					 from m06 in context.M06_SYA.Where(m06 => m06.削除日付 == null)
				//							join t01 in context.T01_TRN.Where(t01 => (t01.支払日付 >= d集計期間From && t01.支払日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) on retdate.日付 equals t01.支払日付 into t01Group
				//					 join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on retdate.日付 equals t02.労務日 into t02Group
				//					 join t03 in query2.Where(t03 => t03.日付 >= d集計期間From && t03.日付 <= d集計期間To) on retdate.日付 equals t03.日付 into t03Group
				//							where lst.Contains(m06.車種ID) || lst2.Contains(m06.車種ID)
				//					 orderby m06.車種ID, retdate.日付
				//							where intCause.Contains(m06.車種ID)
				//							orderby m06.車種ID, retdate.日付
				//					 select new SRY04010_Member
				//					 {
				//						 日付 = retdate.日付,
				//						 社内金額 = t01Group.Where(t01gr => t01gr.車種ID == m06.車種ID && t01gr.支払日付 == retdate.日付 && t01gr.支払先KEY == null).Sum(t01gr => t01gr.支払金額 + t01gr.支払割増１ + t01gr.支払割増２ + t01gr.支払通行料) == null ? 0 : t01Group.Where(t01gr => t01gr.車種ID == m06.車種ID && t01gr.支払日付 == retdate.日付 && t01gr.支払先KEY == null).Sum(t01gr => t01gr.支払金額 + t01gr.支払割増１ + t01gr.支払割増２ + t01gr.支払通行料),
				//						 経費1 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 601).Sum(t03gr => t03gr.金額),
				//						 経費2 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 602).Sum(t03gr => t03gr.金額),
				//						 経費3 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 603).Sum(t03gr => t03gr.金額),
				//						 経費4 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 604).Sum(t03gr => t03gr.金額),
				//						 経費5 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 605).Sum(t03gr => t03gr.金額),
				//						 経費6 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 606).Sum(t03gr => t03gr.金額),
				//						 経費7 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 607).Sum(t03gr => t03gr.金額),
				//						 その他経費 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 &&
				//													(t03gr.経費項目ID != 601 && t03gr.経費項目ID != 602 && t03gr.経費項目ID != 604 && t03gr.経費項目ID != 605 &&
				//													 t03gr.経費項目ID != 608 && t03gr.経費項目ID != 607 && t03gr.経費項目ID != 401)).Sum(t03gr => t03gr.金額),
				//						 燃料Ｌ数 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.数量),
				//						 燃料代 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.金額),
				//						 輸送屯数 = t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数) == null ? 0 : t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数),
				//						 走行KM = t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ),
				//						 実車KM = t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ),
				//						 コード = m06.車種ID,
				//						 車種名 = m06.車種名,
				//						 期間From = d集計期間From,
				//						 期間To = d集計期間To,

				//						 経費名1 = context.M07_KEI.Where(q => q.経費項目ID == 601).Select(c => c.経費項目名).FirstOrDefault(),
				//						 経費名2 = context.M07_KEI.Where(q => q.経費項目ID == 602).Select(c => c.経費項目名).FirstOrDefault(),
				//						 経費名3 = context.M07_KEI.Where(q => q.経費項目ID == 603).Select(c => c.経費項目名).FirstOrDefault(),
				//						 経費名4 = context.M07_KEI.Where(q => q.経費項目ID == 604).Select(c => c.経費項目名).FirstOrDefault(),
				//						 経費名5 = context.M07_KEI.Where(q => q.経費項目ID == 605).Select(c => c.経費項目名).FirstOrDefault(),
				//						 経費名6 = context.M07_KEI.Where(q => q.経費項目ID == 606).Select(c => c.経費項目名).FirstOrDefault(),
				//						 経費名7 = context.M07_KEI.Where(q => q.経費項目ID == 607).Select(c => c.経費項目名).FirstOrDefault(),

				//					 });

				//		////車種From処理　Min値
				//		//if (!string.IsNullOrEmpty(p車種From))
				//		//{
				//		//    int i車種FROM = AppCommon.IntParse(p車種From);
				//		//    query = query.Where(c => c.コード >= i車種FROM);
				//		//}

				//		////車種To処理　Max値
				//		//if (!string.IsNullOrEmpty(p車種To))
				//		//{
				//		//    int i車種TO = AppCommon.IntParse(p車種To);
				//		//    query = query.Where(c => c.コード <= i車種TO);
				//		//}


				//	}

				//	else
				//	{
				//		query = query.Where(c => c.コード > int.MinValue && c.コード < int.MaxValue);

				//	}
				//}
                query = query.Distinct();
                //結果をリスト化
                retList = query.ToList();
                return retList;
            }

        }
        #endregion

        #region CSV出力
        /// <summary>
        /// SRY04010 印刷
        /// </summary>
        /// <param name="p商品ID">車種コード</param>
        /// <returns>T01</returns>
        public List<SRY04010_Member_CSV> GetDataList_CSV(string p車種From, string p車種To, int?[] i車種List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string s車種List)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				int syafrom = AppCommon.IntParse(p車種From) == 0 ? int.MinValue : AppCommon.IntParse(p車種From);
				int syato = AppCommon.IntParse(p車種To) == 0 ? int.MaxValue : AppCommon.IntParse(p車種To);
				if ((string.IsNullOrEmpty(p車種From + p車種To) && i車種List.Length != 0))
				{
					syafrom = int.MaxValue;
					syato = int.MaxValue;
				}

                List<SRY04010_Member_CSV> retList = new List<SRY04010_Member_CSV>();
                List<SRY04010_Date> retList2 = new List<SRY04010_Date>();

                context.Connection.Open();

                for (DateTime dDate = d集計期間From; dDate <= d集計期間To; dDate = dDate.AddDays(1))
                {
                    retList2.Add(new SRY04010_Date() { 日付 = dDate });
                }

                //var query3 = (from m05 in context.M05_CAR
                //              let t03l = from t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) select t03.車輌ID
                //              where t03l.Contains(m05.)
                //              select new SRY04010_CAR
                //              {
                //                  車種ID = m05.車種ID,
                //              }).AsQueryable();

                int[] lst;
                lst = (from m06 in context.M06_SYA.Where(m06 => m06.削除日付 == null)
                       let t01l = from t01 in context.T01_TRN.Where(t01 => t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) select t01.車種ID
                       let t02l = from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) select t02.車種ID
                       where t01l.Contains(m06.車種ID) || t02l.Contains(m06.車種ID)
                       select m06.車種ID).ToArray();

                var query2 = (from t in context.T03_KTRN
                              join m07 in context.M07_KEI on t.経費項目ID equals m07.経費項目ID
                              join m05 in context.M05_CAR on t.車輌ID equals m05.車輌KEY into m05Group
                              from m05g in m05Group
                              join m06 in context.M06_SYA.Where(m06 => m06.削除日付 == null) on m05g.車種ID equals m06.車種ID into m06Group
                              from m06g in m06Group
                              where m07.固定変動区分 == 1 && t.経費発生日 >= d集計期間From && t.経費発生日 <= d集計期間To
                              select new SRY04010_KTRN
                              {
                                  日付 = t.経費発生日,
                                  経費項目ID = t.経費項目ID,
                                  数量 = t.数量,
                                  金額 = t.金額,
                                  車種KEY = m06g.車種ID,
                              }).AsQueryable();

                int?[] lst2;
                lst2 = query2.Select(c => c.車種KEY).ToArray();

                var query = (from retdate in retList2
							 from m06 in context.M06_SYA.Where(m06 => (m06.削除日付 == null) && (m06.車種ID >= syafrom && m06.車種ID <= syato || (i車種List.Contains(m06.車種ID))))
							 join t01 in context.T01_TRN.Where(t01 => (t01.支払日付 >= d集計期間From && t01.支払日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) on retdate.日付 equals t01.支払日付 into t01Group
                             join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on retdate.日付 equals t02.労務日 into t02Group
                             join t03 in query2.Where(t03 => t03.日付 >= d集計期間From && t03.日付 <= d集計期間To) on retdate.日付 equals t03.日付 into t03Group
                             where lst.Contains(m06.車種ID) || lst2.Contains(m06.車種ID) 
                             orderby m06.車種ID, retdate.日付
                             select new SRY04010_Member_CSV
                             {
                                 日付 = retdate.日付,
                                 社内金額 = t01Group.Where(t01gr => t01gr.車種ID == m06.車種ID && t01gr.支払日付 == retdate.日付 && t01gr.支払先KEY == null).Sum(t01gr => t01gr.支払金額 + t01gr.支払割増１ + t01gr.支払割増２ + t01gr.支払通行料) == null ? 0 : t01Group.Where(t01gr => t01gr.車種ID == m06.車種ID && t01gr.支払日付 == retdate.日付 && t01gr.支払先KEY == null).Sum(t01gr => t01gr.支払金額 + t01gr.支払割増１ + t01gr.支払割増２ + t01gr.支払通行料),
                                 経費1 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 601).Sum(t03gr => t03gr.金額),
                                 経費2 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 602).Sum(t03gr => t03gr.金額),
                                 経費3 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 603).Sum(t03gr => t03gr.金額),
                                 経費4 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 604).Sum(t03gr => t03gr.金額),
                                 経費5 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 605).Sum(t03gr => t03gr.金額),
                                 経費6 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 606).Sum(t03gr => t03gr.金額),
                                 経費7 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 607).Sum(t03gr => t03gr.金額),
                                 その他経費 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 &&
                                                            (t03gr.経費項目ID != 601 && t03gr.経費項目ID != 602 && t03gr.経費項目ID != 604 && t03gr.経費項目ID != 605 &&
                                                             t03gr.経費項目ID != 608 && t03gr.経費項目ID != 607 && t03gr.経費項目ID != 401)).Sum(t03gr => t03gr.金額),
                                 燃料Ｌ数 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.数量),
                                 燃料代 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.金額),
                                 輸送屯数 = t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数) == null ? 0 : t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数),
                                 走行KM = t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ),
                                 実車KM = t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ),
                                 コード = m06.車種ID,
                                 車種名 = m06.車種名,

                                 経費名1 = context.M07_KEI.Where(q => q.経費項目ID == 601).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名2 = context.M07_KEI.Where(q => q.経費項目ID == 602).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名3 = context.M07_KEI.Where(q => q.経費項目ID == 603).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名4 = context.M07_KEI.Where(q => q.経費項目ID == 604).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名5 = context.M07_KEI.Where(q => q.経費項目ID == 605).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名6 = context.M07_KEI.Where(q => q.経費項目ID == 606).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名7 = context.M07_KEI.Where(q => q.経費項目ID == 607).Select(c => c.経費項目名).FirstOrDefault(),

                             }).AsQueryable().Distinct();

				//if (!(string.IsNullOrEmpty(p車種From + p車種To) && i車種List.Length == 0))
				//{

				//	//車種が検索対象に入っていない時全データ取得
				//	if (string.IsNullOrEmpty(p車種From + p車種To))
				//	{
				//		query = query.Where(c => c.コード >= int.MaxValue);
				//	}

				//	//車種From処理　Min値
				//	if (!string.IsNullOrEmpty(p車種From))
				//	{
				//		int i車種FROM = AppCommon.IntParse(p車種From);
				//		query = query.Where(c => c.コード >= i車種FROM);
				//	}

				//	//車種To処理　Max値
				//	if (!string.IsNullOrEmpty(p車種To))
				//	{
				//		int i車種TO = AppCommon.IntParse(p車種To);
				//		query = query.Where(c => c.コード <= i車種TO);
				//	}


				//	if (i車種List.Length > 0)
				//	{
				//		var intCause = i車種List;
				//		query = query.Union(from retdate in retList2
				//							from m06 in context.M06_SYA.Where(m06 => m06.削除日付 == null)
				//							join t01 in context.T01_TRN.Where(t01 => (t01.支払日付 >= d集計期間From && t01.支払日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) on retdate.日付 equals t01.支払日付 into t01Group
				//							join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on retdate.日付 equals t02.労務日 into t02Group
				//							join t03 in query2.Where(t03 => t03.日付 >= d集計期間From && t03.日付 <= d集計期間To) on retdate.日付 equals t03.日付 into t03Group
				//							where lst.Contains(m06.車種ID) || lst2.Contains(m06.車種ID) 
				//							orderby m06.車種ID, retdate.日付
				//							where intCause.Contains(m06.車種ID)
				//							orderby m06.車種ID, retdate.日付
				//							select new SRY04010_Member_CSV
				//							{
				//								日付 = retdate.日付,
				//								社内金額 = t01Group.Where(t01gr => t01gr.車種ID == m06.車種ID && t01gr.支払日付 == retdate.日付 && t01gr.支払先KEY == null).Sum(t01gr => t01gr.支払金額 + t01gr.支払割増１ + t01gr.支払割増２ + t01gr.支払通行料) == null ? 0 : t01Group.Where(t01gr => t01gr.車種ID == m06.車種ID && t01gr.支払日付 == retdate.日付 && t01gr.支払先KEY == null).Sum(t01gr => t01gr.支払金額 + t01gr.支払割増１ + t01gr.支払割増２ + t01gr.支払通行料),
				//								経費1 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 601).Sum(t03gr => t03gr.金額),
				//								経費2 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 602).Sum(t03gr => t03gr.金額),
				//								経費3 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 603).Sum(t03gr => t03gr.金額),
				//								経費4 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 604).Sum(t03gr => t03gr.金額),
				//								経費5 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 605).Sum(t03gr => t03gr.金額),
				//								経費6 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 606).Sum(t03gr => t03gr.金額),
				//								経費7 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 607).Sum(t03gr => t03gr.金額),
				//								その他経費 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 &&
				//														   (t03gr.経費項目ID != 601 && t03gr.経費項目ID != 602 && t03gr.経費項目ID != 604 && t03gr.経費項目ID != 605 &&
				//															t03gr.経費項目ID != 608 && t03gr.経費項目ID != 607 && t03gr.経費項目ID != 401)).Sum(t03gr => t03gr.金額),
				//								燃料Ｌ数 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.数量),
				//								燃料代 = t03Group.Where(t03gr => t03gr.車種KEY == m06.車種ID && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.金額),
				//								輸送屯数 = t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数) == null ? 0 : t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数),
				//								走行KM = t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ),
				//								実車KM = t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車種ID == m06.車種ID && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ),
				//								コード = m06.車種ID,
				//								車種名 = m06.車種名,

				//								経費名1 = context.M07_KEI.Where(q => q.経費項目ID == 601).Select(c => c.経費項目名).FirstOrDefault(),
				//								経費名2 = context.M07_KEI.Where(q => q.経費項目ID == 602).Select(c => c.経費項目名).FirstOrDefault(),
				//								経費名3 = context.M07_KEI.Where(q => q.経費項目ID == 603).Select(c => c.経費項目名).FirstOrDefault(),
				//								経費名4 = context.M07_KEI.Where(q => q.経費項目ID == 604).Select(c => c.経費項目名).FirstOrDefault(),
				//								経費名5 = context.M07_KEI.Where(q => q.経費項目ID == 605).Select(c => c.経費項目名).FirstOrDefault(),
				//								経費名6 = context.M07_KEI.Where(q => q.経費項目ID == 606).Select(c => c.経費項目名).FirstOrDefault(),
				//								経費名7 = context.M07_KEI.Where(q => q.経費項目ID == 607).Select(c => c.経費項目名).FirstOrDefault(),

				//							});

				//		//車種From処理　Min値
				//		if (!string.IsNullOrEmpty(p車種From))
				//		{
				//			int i車種FROM = AppCommon.IntParse(p車種From);
				//			query = query.Where(c => c.コード >= i車種FROM);
				//		}

				//		//車種To処理　Max値
				//		if (!string.IsNullOrEmpty(p車種To))
				//		{
				//			int i車種TO = AppCommon.IntParse(p車種To);
				//			query = query.Where(c => c.コード <= i車種TO);
				//		}


				//	}

				//	else
				//	{
				//		query = query.Where(c => c.コード > int.MinValue && c.コード < int.MaxValue);

				//	}
				//}
                query = query.Distinct();
                //結果をリスト化
                retList = query.ToList();
                return retList;
            }

        }

        #endregion
    }
}