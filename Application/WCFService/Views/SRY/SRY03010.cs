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
    /// SRY03010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class SRY03010_Member
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
        public string 車輌番号 { get; set; }
        public string 車種名 { get; set; }
        public DateTime 期間From { get; set; }
        public DateTime 期間To { get; set; }
        public decimal? 歩合率 { get; set; }

        public string 経費名1 { get; set; }
        public string 経費名2 { get; set; }
        public string 経費名3 { get; set; }
        public string 経費名4 { get; set; }
        public string 経費名5 { get; set; }
        public string 経費名6 { get; set; }
        public string 経費名7 { get; set; }


    }

    /// <summary>
    /// SRY03010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class SRY03010_Date
    {
        public DateTime? 日付 { get; set; }
    }

    /// <summary>
    /// SRY03010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class SRY03010_KTRN
    {
        public DateTime? 日付 { get; set; }
        public int? 経費項目ID { get; set; }
        public decimal? 数量 { get; set; }
        public int? 金額 { get; set; }
        public int? 車輌KEY { get; set; }
        public string 経費名 { get; set; }
    }

    /// <summary>
    /// SRY03010  CSV　メンバー
    /// </summary>
    [DataContract]
    public class SRY03010_Member_CSV
    {
        public int? コード { get; set; }
        public string 車輌番号 { get; set; }
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
    public class SRY03010_M07
    {
        [DataMember]
        public int 経費ID { get; set; }
        [DataMember]
        public string 経費名 { get; set; }
    }


    public class SRY03010
    {
        #region 印刷
        /// <summary>
        /// SRY03010 印刷
        /// </summary>
        /// <param name="p商品ID">車輌コード</param>
        /// <returns>T01</returns>
        public List<SRY03010_Member> GetDataList(string p車輌From, string p車輌To, int?[] i車輌List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string s車輌List)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				int syafrom = AppCommon.IntParse(p車輌From) == 0 ? int.MinValue : AppCommon.IntParse(p車輌From);
				int syato = AppCommon.IntParse(p車輌To) == 0 ? int.MaxValue : AppCommon.IntParse(p車輌To);
				if ((string.IsNullOrEmpty(p車輌From + p車輌To) && i車輌List.Length != 0))
				{
					syafrom = int.MaxValue;
					syato = int.MaxValue;
				}
                List<SRY03010_Member> retList = new List<SRY03010_Member>();
                List<SRY03010_Date> retList2 = new List<SRY03010_Date>();

                context.Connection.Open();

                for (DateTime dDate = d集計期間From; dDate <= d集計期間To; dDate = dDate.AddDays(1))
                {
                    retList2.Add(new SRY03010_Date() { 日付 = dDate });
                }

                int[] lst;
                lst = (from m05 in context.M05_CAR
                       let t01l = from t01 in context.T01_TRN.Where(t01 => (t01.支払日付 >= d集計期間From && t01.支払日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) select t01.車輌KEY
                       let t02l = from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) select t02.車輌KEY
                       let t03l = from t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) select t03.車輌ID
                       where t01l.Contains(m05.車輌KEY) || t02l.Contains(m05.車輌KEY) || t03l.Contains(m05.車輌KEY)
                       select m05.車輌KEY).ToArray();

                var query2 = (from t in context.T03_KTRN
                              join m07 in context.M07_KEI on t.経費項目ID equals m07.経費項目ID
                              where m07.固定変動区分 == 1
                              select new SRY03010_KTRN
                              {
                                  日付 = t.経費発生日,
                                  経費項目ID = t.経費項目ID,
                                  数量 = t.数量,
                                  金額 = t.金額,
                                  車輌KEY = t.車輌ID,
                                  経費名 = m07.経費項目名,
                              }).AsQueryable();

                var query = (from retdate in retList2
							 from m05 in context.M05_CAR.Where(c => (c.車輌ID >= syafrom && c.車輌ID <= syato || (i車輌List.Contains(c.車輌ID))))
                             join m06 in context.M06_SYA on m05.車種ID equals m06.車種ID into m06Group
                             from m06g in m06Group
                             join t01 in context.T01_TRN.Where(t01 => (t01.支払日付 >= d集計期間From && t01.支払日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) on retdate.日付 equals t01.支払日付 into t01Group
                             join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on retdate.日付 equals t02.労務日 into t02Group
                             join t03 in query2.Where(t03 => t03.日付 >= d集計期間From && t03.日付 <= d集計期間To) on retdate.日付 equals t03.日付 into t03Group
                             let t01l = from t01 in context.T01_TRN.Where(t01 => t01.支払日付 >= d集計期間From && t01.支払日付 <= d集計期間To) select t01.車輌KEY
                             let t02l = from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) select t02.車輌KEY
                             let t03l = from t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) select t03.車輌ID
                             where lst.Contains(m05.車輌KEY)
                             //where t01l.Contains(m04.車輌KEY) || t02l.Contains(m04.車輌KEY) || t03l.Contains(m04.車輌KEY)
                             orderby m05.車輌ID, retdate.日付
                             select new SRY03010_Member
                             {
                                 日付 = retdate.日付,
                                 社内金額 = t01Group.Where(t01gr => t01gr.車輌KEY == m05.車輌KEY && t01gr.支払日付 == retdate.日付 && t01gr.支払先KEY == null).Sum(t01gr => t01gr.支払金額 + t01gr.支払割増１ + t01gr.支払割増２ + t01gr.支払通行料) == null ? 0 : t01Group.Where(t01gr => t01gr.車輌KEY == m05.車輌KEY && t01gr.支払日付 == retdate.日付 && t01gr.支払先KEY == null).Sum(t01gr => t01gr.支払金額 + t01gr.支払割増１ + t01gr.支払割増２ + t01gr.支払通行料),
                                 経費1 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 601).Sum(t03gr => t03gr.金額),
                                 経費2 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 602).Sum(t03gr => t03gr.金額),
                                 経費3 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 603).Sum(t03gr => t03gr.金額),
                                 経費4 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 604).Sum(t03gr => t03gr.金額),
                                 経費5 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 605).Sum(t03gr => t03gr.金額),
                                 経費6 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 606).Sum(t03gr => t03gr.金額),
                                 経費7 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 607).Sum(t03gr => t03gr.金額),
                                 その他経費 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 &&
                                                            (t03gr.経費項目ID != 601 && t03gr.経費項目ID != 602 && t03gr.経費項目ID != 604 && t03gr.経費項目ID != 605 &&
                                                             t03gr.経費項目ID != 608 && t03gr.経費項目ID != 607 && t03gr.経費項目ID != 401)).Sum(t03gr => t03gr.金額),
                                 燃料Ｌ数 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.数量),
                                 燃料代 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.金額),
                                 輸送屯数 = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数),
                                 走行KM = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ),
                                 実車KM = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ),
                                 コード = m05.車輌ID,
                                 車輌番号 = m05.車輌番号,
                                 車種名 = m06g.車種名,
                                 期間From = d集計期間From,
                                 期間To = d集計期間To,

                                 経費名1 = context.M07_KEI.Where(q => q.経費項目ID == 601).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名2 = context.M07_KEI.Where(q => q.経費項目ID == 602).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名3 = context.M07_KEI.Where(q => q.経費項目ID == 603).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名4 = context.M07_KEI.Where(q => q.経費項目ID == 604).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名5 = context.M07_KEI.Where(q => q.経費項目ID == 605).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名6 = context.M07_KEI.Where(q => q.経費項目ID == 606).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名7 = context.M07_KEI.Where(q => q.経費項目ID == 607).Select(c => c.経費項目名).FirstOrDefault(),

                             }).AsQueryable();

				//if (!(string.IsNullOrEmpty(p車輌From + p車輌To) && i車輌List.Length == 0))
				//{

				//	//車輌が検索対象に入っていない時全データ取得
				//	if (string.IsNullOrEmpty(p車輌From + p車輌To))
				//	{
				//		query = query.Where(c => c.コード >= int.MaxValue);
				//	}

				//	//車輌From処理　Min値
				//	if (!string.IsNullOrEmpty(p車輌From))
				//	{
				//		int i車輌FROM = AppCommon.IntParse(p車輌From);
				//		query = query.Where(c => c.コード >= i車輌FROM);
				//	}

				//	//車輌To処理　Max値
				//	if (!string.IsNullOrEmpty(p車輌To))
				//	{
				//		int i車輌TO = AppCommon.IntParse(p車輌To);
				//		query = query.Where(c => c.コード <= i車輌TO);
				//	}


				//	if (string.IsNullOrEmpty(p車輌From + p車輌To) && i車輌List.Length > 0)
				//	{
				//		var intCause = i車輌List;
				//		query = query.Union(from retdate in retList2
				//							from m05 in context.M05_CAR
				//							join m06 in context.M06_SYA on m05.車種ID equals m06.車種ID into m06Group
				//							from m06g in m06Group
				//							join t01 in context.T01_TRN.Where(t01 => (t01.支払日付 >= d集計期間From && t01.支払日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) on retdate.日付 equals t01.支払日付 into t01Group
				//							join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on retdate.日付 equals t02.労務日 into t02Group
				//							join t03 in query2.Where(t03 => t03.日付 >= d集計期間From && t03.日付 <= d集計期間To) on retdate.日付 equals t03.日付 into t03Group
				//							let t01l = from t01 in context.T01_TRN.Where(t01 => t01.支払日付 >= d集計期間From && t01.支払日付 <= d集計期間To) select t01.車輌KEY
				//							let t02l = from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) select t02.車輌KEY
				//							let t03l = from t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) select t03.車輌ID
				//							where lst.Contains(m05.車輌KEY) && intCause.Contains(m05.車輌ID)
				//							//where t01l.Contains(m04.車輌KEY) || t02l.Contains(m04.車輌KEY) || t03l.Contains(m04.車輌KEY)
				//							orderby m05.車輌ID, retdate.日付
				//							select new SRY03010_Member
				//							{
				//								日付 = retdate.日付,
				//								社内金額 = t01Group.Where(t01gr => t01gr.車輌KEY == m05.車輌KEY && t01gr.支払日付 == retdate.日付 && t01gr.支払先KEY == null).Sum(t01gr => t01gr.支払金額 + t01gr.支払割増１ + t01gr.支払割増２ + t01gr.支払通行料) == null ? 0 : t01Group.Where(t01gr => t01gr.車輌KEY == m05.車輌KEY && t01gr.支払日付 == retdate.日付 && t01gr.支払先KEY == null).Sum(t01gr => t01gr.支払金額 + t01gr.支払割増１ + t01gr.支払割増２ + t01gr.支払通行料),
				//								経費1 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 601).Sum(t03gr => t03gr.金額),
				//								経費2 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 602).Sum(t03gr => t03gr.金額),
				//								経費3 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 603).Sum(t03gr => t03gr.金額),
				//								経費4 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 604).Sum(t03gr => t03gr.金額),
				//								経費5 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 605).Sum(t03gr => t03gr.金額),
				//								経費6 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 606).Sum(t03gr => t03gr.金額),
				//								経費7 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 607).Sum(t03gr => t03gr.金額),
				//								その他経費 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 &&
				//														   (t03gr.経費項目ID != 601 && t03gr.経費項目ID != 602 && t03gr.経費項目ID != 604 && t03gr.経費項目ID != 605 &&
				//															t03gr.経費項目ID != 608 && t03gr.経費項目ID != 607 && t03gr.経費項目ID != 401)).Sum(t03gr => t03gr.金額),
				//								燃料Ｌ数 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.数量),
				//								燃料代 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.金額),
				//								輸送屯数 = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数),
				//								走行KM = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ),
				//								実車KM = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ),
				//								コード = m05.車輌ID,
				//								車輌番号 = m05.車輌番号,
				//								車種名 = m06g.車種名,
				//								期間From = d集計期間From,
				//								期間To = d集計期間To,

				//								経費名1 = context.M07_KEI.Where(q => q.経費項目ID == 601).Select(c => c.経費項目名).FirstOrDefault(),
				//								経費名2 = context.M07_KEI.Where(q => q.経費項目ID == 602).Select(c => c.経費項目名).FirstOrDefault(),
				//								経費名3 = context.M07_KEI.Where(q => q.経費項目ID == 603).Select(c => c.経費項目名).FirstOrDefault(),
				//								経費名4 = context.M07_KEI.Where(q => q.経費項目ID == 604).Select(c => c.経費項目名).FirstOrDefault(),
				//								経費名5 = context.M07_KEI.Where(q => q.経費項目ID == 605).Select(c => c.経費項目名).FirstOrDefault(),
				//								経費名6 = context.M07_KEI.Where(q => q.経費項目ID == 606).Select(c => c.経費項目名).FirstOrDefault(),
				//								経費名7 = context.M07_KEI.Where(q => q.経費項目ID == 607).Select(c => c.経費項目名).FirstOrDefault(),


				//							});
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
        /// SRY03010 印刷
        /// </summary>
        /// <param name="p商品ID">車輌コード</param>
        /// <returns>T01</returns>
        public List<SRY03010_Member_CSV> GetDataList_CSV(string p車輌From, string p車輌To, int?[] i車輌List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string s車輌List)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				int syafrom = AppCommon.IntParse(p車輌From) == 0 ? int.MinValue : AppCommon.IntParse(p車輌From);
				int syato = AppCommon.IntParse(p車輌To) == 0 ? int.MaxValue : AppCommon.IntParse(p車輌To);
				if ((string.IsNullOrEmpty(p車輌From + p車輌To) && i車輌List.Length != 0))
				{
					syafrom = int.MaxValue;
					syato = int.MaxValue;
				}

                List<SRY03010_Member_CSV> retList = new List<SRY03010_Member_CSV>();
                List<SRY03010_Date> retList2 = new List<SRY03010_Date>();

                context.Connection.Open();

                for (DateTime dDate = d集計期間From; dDate <= d集計期間To; dDate = dDate.AddDays(1))
                {
                    retList2.Add(new SRY03010_Date() { 日付 = dDate });
                }

                int[] lst;
                lst = (from m05 in context.M05_CAR
                       let t01l = from t01 in context.T01_TRN.Where(t01 => (t01.支払日付 >= d集計期間From && t01.支払日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) select t01.車輌KEY
                       let t02l = from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) select t02.車輌KEY
                       let t03l = from t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) select t03.車輌ID
                       where t01l.Contains(m05.車輌KEY) || t02l.Contains(m05.車輌KEY) || t03l.Contains(m05.車輌KEY)
                       select m05.車輌KEY).ToArray();

                var query2 = (from t in context.T03_KTRN
                              join m07 in context.M07_KEI on t.経費項目ID equals m07.経費項目ID
                              where m07.固定変動区分 == 1
                              select new SRY03010_KTRN
                              {
                                  日付 = t.経費発生日,
                                  経費項目ID = t.経費項目ID,
                                  数量 = t.数量,
                                  金額 = t.金額,
                                  車輌KEY = t.車輌ID,
                                  経費名 = m07.経費項目名,
                              }).AsQueryable();

                var query = (from retdate in retList2
							 from m05 in context.M05_CAR.Where(c => (c.車輌ID >= syafrom && c.車輌ID <= syato || (i車輌List.Contains(c.車輌ID))))
							 join m06 in context.M06_SYA on m05.車種ID equals m06.車種ID into m06Group
                             from m06g in m06Group
                             join t01 in context.T01_TRN.Where(t01 => (t01.支払日付 >= d集計期間From && t01.支払日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) on retdate.日付 equals t01.支払日付 into t01Group
                             join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on retdate.日付 equals t02.労務日 into t02Group
                             join t03 in query2.Where(t03 => t03.日付 >= d集計期間From && t03.日付 <= d集計期間To) on retdate.日付 equals t03.日付 into t03Group
                             let t01l = from t01 in context.T01_TRN.Where(t01 => t01.支払日付 >= d集計期間From && t01.支払日付 <= d集計期間To) select t01.車輌KEY
                             let t02l = from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) select t02.車輌KEY
                             let t03l = from t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) select t03.車輌ID
                             where lst.Contains(m05.車輌KEY)
                             //where t01l.Contains(m04.車輌KEY) || t02l.Contains(m04.車輌KEY) || t03l.Contains(m04.車輌KEY)
                             orderby m05.車輌ID, retdate.日付
                             select new SRY03010_Member_CSV
                             {
                                 日付 = retdate.日付,
                                 社内金額 = t01Group.Where(t01gr => t01gr.車輌KEY == m05.車輌KEY && t01gr.支払日付 == retdate.日付 && t01gr.支払先KEY == null).Sum(t01gr => t01gr.支払金額 + t01gr.支払割増１ + t01gr.支払割増２ + t01gr.支払通行料) == null ? 0 : t01Group.Where(t01gr => t01gr.車輌KEY == m05.車輌KEY && t01gr.支払日付 == retdate.日付 && t01gr.支払先KEY == null).Sum(t01gr => t01gr.支払金額 + t01gr.支払割増１ + t01gr.支払割増２ + t01gr.支払通行料),
                                 経費1 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 601).Sum(t03gr => t03gr.金額),
                                 経費2 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 602).Sum(t03gr => t03gr.金額),
                                 経費3 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 603).Sum(t03gr => t03gr.金額),
                                 経費4 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 604).Sum(t03gr => t03gr.金額),
                                 経費5 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 605).Sum(t03gr => t03gr.金額),
                                 経費6 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 606).Sum(t03gr => t03gr.金額),
                                 経費7 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 607).Sum(t03gr => t03gr.金額),
                                 その他経費 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 &&
                                                            (t03gr.経費項目ID != 601 && t03gr.経費項目ID != 602 && t03gr.経費項目ID != 604 && t03gr.経費項目ID != 605 &&
                                                             t03gr.経費項目ID != 608 && t03gr.経費項目ID != 607 && t03gr.経費項目ID != 401)).Sum(t03gr => t03gr.金額),
                                 燃料Ｌ数 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.数量),
                                 燃料代 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.金額),
                                 輸送屯数 = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数),
                                 走行KM = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ),
                                 実車KM = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ),
                                 コード = m05.車輌ID,
                                 車輌番号 = m05.車輌番号,
                                 車種名 = m06g.車種名,

                                 経費名1 = context.M07_KEI.Where(q => q.経費項目ID == 601).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名2 = context.M07_KEI.Where(q => q.経費項目ID == 602).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名3 = context.M07_KEI.Where(q => q.経費項目ID == 603).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名4 = context.M07_KEI.Where(q => q.経費項目ID == 604).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名5 = context.M07_KEI.Where(q => q.経費項目ID == 605).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名6 = context.M07_KEI.Where(q => q.経費項目ID == 606).Select(c => c.経費項目名).FirstOrDefault(),
                                 経費名7 = context.M07_KEI.Where(q => q.経費項目ID == 607).Select(c => c.経費項目名).FirstOrDefault(),
                             }).AsQueryable();

				//if (!(string.IsNullOrEmpty(p車輌From + p車輌To) && i車輌List.Length == 0))
				//{

				//	//車輌が検索対象に入っていない時全データ取得
				//	if (string.IsNullOrEmpty(p車輌From + p車輌To))
				//	{
				//		query = query.Where(c => c.コード >= int.MaxValue);
				//	}

				//	//車輌From処理　Min値
				//	if (!string.IsNullOrEmpty(p車輌From))
				//	{
				//		int i車輌FROM = AppCommon.IntParse(p車輌From);
				//		query = query.Where(c => c.コード >= i車輌FROM);
				//	}

				//	//車輌To処理　Max値
				//	if (!string.IsNullOrEmpty(p車輌To))
				//	{
				//		int i車輌TO = AppCommon.IntParse(p車輌To);
				//		query = query.Where(c => c.コード <= i車輌TO);
				//	}


				//	if (string.IsNullOrEmpty(p車輌From + p車輌To) && i車輌List.Length > 0)
				//	{
				//		var intCause = i車輌List;
				//		query = query.Union(from retdate in retList2
				//							from m05 in context.M05_CAR
				//							join m06 in context.M06_SYA on m05.車種ID equals m06.車種ID into m06Group
				//							from m06g in m06Group
				//							join t01 in context.T01_TRN.Where(t01 => (t01.支払日付 >= d集計期間From && t01.支払日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) on retdate.日付 equals t01.請求日付 into t01Group
				//							join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on retdate.日付 equals t02.労務日 into t02Group
				//							join t03 in query2.Where(t03 => t03.日付 >= d集計期間From && t03.日付 <= d集計期間To) on retdate.日付 equals t03.日付 into t03Group
				//							let t01l = from t01 in context.T01_TRN.Where(t01 => t01.支払日付 >= d集計期間From && t01.支払日付 <= d集計期間To) select t01.車輌KEY
				//							let t02l = from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) select t02.車輌KEY
				//							let t03l = from t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) select t03.車輌ID
				//							where lst.Contains(m05.車輌KEY) && intCause.Contains(m05.車輌ID)
				//							//where t01l.Contains(m04.車輌KEY) || t02l.Contains(m04.車輌KEY) || t03l.Contains(m04.車輌KEY)
				//							orderby m05.車輌ID, retdate.日付
				//							select new SRY03010_Member_CSV
				//							{
				//								日付 = retdate.日付,
				//								社内金額 = t01Group.Where(t01gr => t01gr.車輌KEY == m05.車輌KEY && t01gr.支払日付 == retdate.日付 && t01gr.支払先KEY == null).Sum(t01gr => t01gr.支払金額 + t01gr.支払割増１ + t01gr.支払割増２ + t01gr.支払通行料) == null ? 0 : t01Group.Where(t01gr => t01gr.車輌KEY == m05.車輌KEY && t01gr.支払日付 == retdate.日付 && t01gr.支払先KEY == null).Sum(t01gr => t01gr.支払金額 + t01gr.支払割増１ + t01gr.支払割増２ + t01gr.支払通行料),
				//								経費1 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 601).Sum(t03gr => t03gr.金額),
				//								経費2 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 602).Sum(t03gr => t03gr.金額),
				//								経費3 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 603).Sum(t03gr => t03gr.金額),
				//								経費4 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 604).Sum(t03gr => t03gr.金額),
				//								経費5 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 605).Sum(t03gr => t03gr.金額),
				//								経費6 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 606).Sum(t03gr => t03gr.金額),
				//								経費7 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 607).Sum(t03gr => t03gr.金額),
				//								その他経費 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 &&
				//														   (t03gr.経費項目ID != 601 && t03gr.経費項目ID != 602 && t03gr.経費項目ID != 604 && t03gr.経費項目ID != 605 &&
				//															t03gr.経費項目ID != 608 && t03gr.経費項目ID != 607 && t03gr.経費項目ID != 401)).Sum(t03gr => t03gr.金額),
				//								燃料Ｌ数 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.数量),
				//								燃料代 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.金額),
				//								輸送屯数 = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数),
				//								走行KM = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ),
				//								実車KM = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ),
				//								コード = m05.車輌ID,
				//								車輌番号 = m05.車輌番号,
				//								車種名 = m06g.車種名,

				//								経費名1 = context.M07_KEI.Where(q => q.経費項目ID == 601).Select(c => c.経費項目名).FirstOrDefault(),
				//								経費名2 = context.M07_KEI.Where(q => q.経費項目ID == 602).Select(c => c.経費項目名).FirstOrDefault(),
				//								経費名3 = context.M07_KEI.Where(q => q.経費項目ID == 603).Select(c => c.経費項目名).FirstOrDefault(),
				//								経費名4 = context.M07_KEI.Where(q => q.経費項目ID == 604).Select(c => c.経費項目名).FirstOrDefault(),
				//								経費名5 = context.M07_KEI.Where(q => q.経費項目ID == 605).Select(c => c.経費項目名).FirstOrDefault(),
				//								経費名6 = context.M07_KEI.Where(q => q.経費項目ID == 606).Select(c => c.経費項目名).FirstOrDefault(),
				//								経費名7 = context.M07_KEI.Where(q => q.経費項目ID == 607).Select(c => c.経費項目名).FirstOrDefault(),
				//							});
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

#region 出力データがおかしい


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.ServiceModel;
//using System.Text;
//using System.Data.Objects;
//using System.Data;
//using System.Data.Common;
//using System.Transactions;
//using System.Collections;
//using System.Data.Entity;

//namespace KyoeiSystem.Application.WCFService
//{

//    /// <summary>
//    /// SRY03010  印刷　メンバー
//    /// </summary>
//    [DataContract]
//    public class SRY03010_Member
//    {
//        public DateTime? 日付 { get; set; }
//        public decimal? 社内金額 { get; set; }
//        public int? 経費1 { get; set; }
//        public int? 経費2 { get; set; }
//        public int? 経費3 { get; set; }
//        public int? 経費4 { get; set; }
//        public int? 経費5 { get; set; }
//        public int? 経費6 { get; set; }
//        public int? 経費7 { get; set; }
//        public int? その他経費 { get; set; }
//        public decimal? 燃料Ｌ数 { get; set; }
//        public decimal? 燃料代 { get; set; }
//        public decimal? 輸送屯数 { get; set; }
//        public int? 走行KM { get; set; }
//        public int? 実車KM { get; set; }

//        public int? コード { get; set; }
//        public string 車輌番号 { get; set; }
//        public string 車種名 { get; set; }
//        public DateTime 期間From { get; set; }
//        public DateTime 期間To { get; set; }
//        public decimal? 歩合率 { get; set; }

//        public string 経費名1 { get; set; }
//        public string 経費名2 { get; set; }
//        public string 経費名3 { get; set; }
//        public string 経費名4 { get; set; }
//        public string 経費名5 { get; set; }
//        public string 経費名6 { get; set; }
//        public string 経費名7 { get; set; }


//    }

//    /// <summary>
//    /// SRY03010  印刷　メンバー
//    /// </summary>
//    [DataContract]
//    public class SRY03010_Date
//    {
//        public DateTime? 日付 { get; set; }
//    }

//    /// <summary>
//    /// SRY03010  印刷　メンバー
//    /// </summary>
//    [DataContract]
//    public class SRY03010_KTRN
//    {
//        public DateTime? 日付 { get; set; }
//        public int? 経費項目ID { get; set; }
//        public decimal? 数量 { get; set; }
//        public int? 金額 { get; set; }
//        public int? 車輌KEY { get; set; }
//        public string 経費名 { get; set; }
//    }

//    /// <summary>
//    /// SRY03010  CSV　メンバー
//    /// </summary>
//    [DataContract]
//    public class SRY03010_Member_CSV
//    {
//        public int? コード { get; set; }
//        public string 車輌番号 { get; set; }
//        public string 車種名 { get; set; }
//        public DateTime? 日付 { get; set; }
//        public decimal? 社内金額 { get; set; }
//        public int? 経費1 { get; set; }
//        public int? 経費2 { get; set; }
//        public int? 経費3 { get; set; }
//        public int? 経費4 { get; set; }
//        public int? 経費5 { get; set; }
//        public int? 経費6 { get; set; }
//        public int? 経費7 { get; set; }
//        public int? その他経費 { get; set; }
//        public decimal? 燃料Ｌ数 { get; set; }
//        public decimal? 燃料代 { get; set; }
//        public decimal? 輸送屯数 { get; set; }
//        public int? 走行KM { get; set; }
//        public int? 実車KM { get; set; }

//        public string 経費名1 { get; set; }
//        public string 経費名2 { get; set; }
//        public string 経費名3 { get; set; }
//        public string 経費名4 { get; set; }
//        public string 経費名5 { get; set; }
//        public string 経費名6 { get; set; }
//        public string 経費名7 { get; set; }


//    }


//    [DataContract]
//    public class SRY03010_M07
//    {
//        [DataMember]
//        public int 経費ID { get; set; }
//        [DataMember]
//        public string 経費名 { get; set; }
//    }


//    public class SRY03010
//    {
//        #region 印刷
//        /// <summary>
//        /// SRY03010 印刷
//        /// </summary>
//        /// <param name="p商品ID">車輌コード</param>
//        /// <returns>T01</returns>
//        public List<SRY03010_Member> GetDataList(string p車輌From, string p車輌To, int?[] i車輌List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string s車輌List)
//        {
//            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
//            {
//                List<SRY03010_Member> retList = new List<SRY03010_Member>();
//                List<SRY03010_Date> retList2 = new List<SRY03010_Date>();

//                context.Connection.Open();

//                for (DateTime dDate = d集計期間From; dDate <= d集計期間To; dDate = dDate.AddDays(1))
//                {
//                    retList2.Add(new SRY03010_Date() { 日付 = dDate });
//                }

//                int[] lst;
//                lst = (from m05 in context.M05_CAR
//                       let t01l = from t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) select t01.車輌KEY
//                       let t02l = from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) select t02.車輌KEY
//                       let t03l = from t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) select t03.車輌ID
//                       where t01l.Contains(m05.車輌KEY) || t02l.Contains(m05.車輌KEY) || t03l.Contains(m05.車輌KEY)
//                       select m05.車輌KEY).ToArray();

//                var query2 = (from t in context.T03_KTRN
//                              join m07 in context.M07_KEI on t.経費項目ID equals m07.経費項目ID
//                              where m07.固定変動区分 == 1
//                              select new SRY03010_KTRN
//                             {
//                                 日付 = t.経費発生日,
//                                 経費項目ID = t.経費項目ID,
//                                 数量 = t.数量,
//                                 金額 = t.金額,
//                                 車輌KEY = t.車輌ID,
//                                 経費名 = m07.経費項目名,
//                             }).AsQueryable();

//                var query = (from retdate in retList2
//                             from m05 in context.M05_CAR
//                             join m06 in context.M06_SYA on m05.車種ID equals m06.車種ID into m06Group
//                             from m06g in m06Group
//                             join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) on retdate.日付 equals t01.請求日付 into t01Group
//                             join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on retdate.日付 equals t02.労務日 into t02Group
//                             join t03 in query2.Where(t03 => t03.日付 >= d集計期間From && t03.日付 <= d集計期間To) on retdate.日付 equals t03.日付 into t03Group
//                             let t01l = from t01 in context.T01_TRN.Where(t01 => t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) select t01.車輌KEY
//                             let t02l = from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) select t02.車輌KEY
//                             let t03l = from t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) select t03.車輌ID
//                             where lst.Contains(m05.車輌KEY)
//                             //where t01l.Contains(m04.車輌KEY) || t02l.Contains(m04.車輌KEY) || t03l.Contains(m04.車輌KEY)
//                             orderby m05.車輌ID, retdate.日付
//                             select new SRY03010_Member
//                             {
//                                 日付 = retdate.日付,
//                                 社内金額 = t01Group.Where(t01gr => t01gr.車輌KEY == m05.車輌KEY && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２) == null ? 0 : t01Group.Where(t01gr => t01gr.車輌KEY == m05.車輌KEY && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２),
//                                 経費1 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 601).Sum(t03gr => t03gr.金額),
//                                 経費2 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 602).Sum(t03gr => t03gr.金額),
//                                 経費3 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 603).Sum(t03gr => t03gr.金額),
//                                 経費4 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 604).Sum(t03gr => t03gr.金額),
//                                 経費5 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 605).Sum(t03gr => t03gr.金額),
//                                 経費6 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 606).Sum(t03gr => t03gr.金額),
//                                 経費7 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 607).Sum(t03gr => t03gr.金額),
//                                 その他経費 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 &&
//                                                            (t03gr.経費項目ID != 601 && t03gr.経費項目ID != 602 && t03gr.経費項目ID != 604 && t03gr.経費項目ID != 605 &&
//                                                             t03gr.経費項目ID != 608 && t03gr.経費項目ID != 607 && t03gr.経費項目ID != 401)).Sum(t03gr => t03gr.金額),
//                                 燃料Ｌ数 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.数量),
//                                 燃料代 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.金額),
//                                 輸送屯数 = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数),
//                                 走行KM = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ),
//                                 実車KM = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ),
//                                 コード = m05.車輌ID,
//                                 車輌番号 = m05.車輌番号,
//                                 車種名 = m06g.車種名,
//                                 期間From = d集計期間From,
//                                 期間To = d集計期間To,

//                                 経費名1 = context.M07_KEI.Where(q => q.経費項目ID == 601).Select(c => c.経費項目名).FirstOrDefault(),
//                                 経費名2 = context.M07_KEI.Where(q => q.経費項目ID == 602).Select(c => c.経費項目名).FirstOrDefault(),
//                                 経費名3 = context.M07_KEI.Where(q => q.経費項目ID == 603).Select(c => c.経費項目名).FirstOrDefault(),
//                                 経費名4 = context.M07_KEI.Where(q => q.経費項目ID == 604).Select(c => c.経費項目名).FirstOrDefault(),
//                                 経費名5 = context.M07_KEI.Where(q => q.経費項目ID == 605).Select(c => c.経費項目名).FirstOrDefault(),
//                                 経費名6 = context.M07_KEI.Where(q => q.経費項目ID == 606).Select(c => c.経費項目名).FirstOrDefault(),
//                                 経費名7 = context.M07_KEI.Where(q => q.経費項目ID == 607).Select(c => c.経費項目名).FirstOrDefault(),

//                             }).AsQueryable();

//                if (!(string.IsNullOrEmpty(p車輌From + p車輌To) && i車輌List.Length == 0))
//                {

//                    //車輌が検索対象に入っていない時全データ取得
//                    if (string.IsNullOrEmpty(p車輌From + p車輌To))
//                    {
//                        query = query.Where(c => c.コード >= int.MaxValue);
//                    }

//                    //車輌From処理　Min値
//                    if (!string.IsNullOrEmpty(p車輌From))
//                    {
//                        int i車輌FROM = AppCommon.IntParse(p車輌From);
//                        query = query.Where(c => c.コード >= i車輌FROM);
//                    }

//                    //車輌To処理　Max値
//                    if (!string.IsNullOrEmpty(p車輌To))
//                    {
//                        int i車輌TO = AppCommon.IntParse(p車輌To);
//                        query = query.Where(c => c.コード <= i車輌TO);
//                    }


//                    if (string.IsNullOrEmpty(p車輌From + p車輌To) && i車輌List.Length > 0)
//                    {
//                        var intCause = i車輌List;
//                        query = query.Union(from retdate in retList2
//                                            from m05 in context.M05_CAR
//                                            join m06 in context.M06_SYA on m05.車種ID equals m06.車種ID into m06Group
//                                            from m06g in m06Group
//                                            join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) on retdate.日付 equals t01.請求日付 into t01Group
//                                            join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on retdate.日付 equals t02.労務日 into t02Group
//                                            join t03 in query2.Where(t03 => t03.日付 >= d集計期間From && t03.日付 <= d集計期間To) on retdate.日付 equals t03.日付 into t03Group
//                                            let t01l = from t01 in context.T01_TRN.Where(t01 => t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) select t01.車輌KEY
//                                            let t02l = from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) select t02.車輌KEY
//                                            let t03l = from t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) select t03.車輌ID
//                                            where lst.Contains(m05.車輌KEY) && intCause.Contains(m05.車輌ID)
//                                            //where t01l.Contains(m04.車輌KEY) || t02l.Contains(m04.車輌KEY) || t03l.Contains(m04.車輌KEY)
//                                            orderby m05.車輌ID, retdate.日付
//                                            select new SRY03010_Member
//                                            {
//                                                日付 = retdate.日付,
//                                                社内金額 = t01Group.Where(t01gr => t01gr.車輌KEY == m05.車輌KEY && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２) == null ? 0 : t01Group.Where(t01gr => t01gr.車輌KEY == m05.車輌KEY && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２),
//                                                経費1 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 601).Sum(t03gr => t03gr.金額),
//                                                経費2 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 602).Sum(t03gr => t03gr.金額),
//                                                経費3 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 603).Sum(t03gr => t03gr.金額),
//                                                経費4 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 604).Sum(t03gr => t03gr.金額),
//                                                経費5 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 605).Sum(t03gr => t03gr.金額),
//                                                経費6 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 606).Sum(t03gr => t03gr.金額),
//                                                経費7 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 607).Sum(t03gr => t03gr.金額),
//                                                その他経費 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 &&
//                                                                           (t03gr.経費項目ID != 601 && t03gr.経費項目ID != 602 && t03gr.経費項目ID != 604 && t03gr.経費項目ID != 605 &&
//                                                                            t03gr.経費項目ID != 608 && t03gr.経費項目ID != 607 && t03gr.経費項目ID != 401)).Sum(t03gr => t03gr.金額),
//                                                燃料Ｌ数 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.数量),
//                                                燃料代 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.金額),
//                                                輸送屯数 = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数),
//                                                走行KM = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ),
//                                                実車KM = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ),
//                                                コード = m05.車輌ID,
//                                                車輌番号 = m05.車輌番号,
//                                                車種名 = m06g.車種名,
//                                                期間From = d集計期間From,
//                                                期間To = d集計期間To,

//                                                経費名1 = context.M07_KEI.Where(q => q.経費項目ID == 601).Select(c => c.経費項目名).FirstOrDefault(),
//                                                経費名2 = context.M07_KEI.Where(q => q.経費項目ID == 602).Select(c => c.経費項目名).FirstOrDefault(),
//                                                経費名3 = context.M07_KEI.Where(q => q.経費項目ID == 603).Select(c => c.経費項目名).FirstOrDefault(),
//                                                経費名4 = context.M07_KEI.Where(q => q.経費項目ID == 604).Select(c => c.経費項目名).FirstOrDefault(),
//                                                経費名5 = context.M07_KEI.Where(q => q.経費項目ID == 605).Select(c => c.経費項目名).FirstOrDefault(),
//                                                経費名6 = context.M07_KEI.Where(q => q.経費項目ID == 606).Select(c => c.経費項目名).FirstOrDefault(),
//                                                経費名7 = context.M07_KEI.Where(q => q.経費項目ID == 607).Select(c => c.経費項目名).FirstOrDefault(),


//                                            });
//                    }

//                    else
//                    {
//                        query = query.Where(c => c.コード > int.MinValue && c.コード < int.MaxValue);

//                    }
//                }
//                query = query.Distinct();
//                //結果をリスト化
//                retList = query.ToList();
//                return retList;
//            }

//        }
//        #endregion

//        #region CSV出力

//        /// <summary>
//        /// SRY03010 印刷
//        /// </summary>
//        /// <param name="p商品ID">車輌コード</param>
//        /// <returns>T01</returns>
//        public List<SRY03010_Member_CSV> GetDataList_CSV(string p車輌From, string p車輌To, int?[] i車輌List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string s車輌List)
//        {
//            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
//            {
//                List<SRY03010_Member_CSV> retList = new List<SRY03010_Member_CSV>();
//                List<SRY03010_Date> retList2 = new List<SRY03010_Date>();

//                context.Connection.Open();

//                for (DateTime dDate = d集計期間From; dDate <= d集計期間To; dDate = dDate.AddDays(1))
//                {
//                    retList2.Add(new SRY03010_Date() { 日付 = dDate });
//                }

//                int[] lst;
//                lst = (from m05 in context.M05_CAR
//                       let t01l = from t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) select t01.車輌KEY
//                       let t02l = from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) select t02.車輌KEY
//                       let t03l = from t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) select t03.車輌ID
//                       where t01l.Contains(m05.車輌KEY) || t02l.Contains(m05.車輌KEY) || t03l.Contains(m05.車輌KEY)
//                       select m05.車輌KEY).ToArray();

//                var query2 = (from t in context.T03_KTRN
//                              join m07 in context.M07_KEI on t.経費項目ID equals m07.経費項目ID
//                              where m07.固定変動区分 == 1
//                              select new SRY03010_KTRN
//                              {
//                                  日付 = t.経費発生日,
//                                  経費項目ID = t.経費項目ID,
//                                  数量 = t.数量,
//                                  金額 = t.金額,
//                                  車輌KEY = t.車輌ID,
//                                  経費名 = m07.経費項目名,
//                              }).AsQueryable();

//                var query = (from retdate in retList2
//                             from m05 in context.M05_CAR
//                             join m06 in context.M06_SYA on m05.車種ID equals m06.車種ID into m06Group
//                             from m06g in m06Group
//                             join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) on retdate.日付 equals t01.請求日付 into t01Group
//                             join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on retdate.日付 equals t02.労務日 into t02Group
//                             join t03 in query2.Where(t03 => t03.日付 >= d集計期間From && t03.日付 <= d集計期間To) on retdate.日付 equals t03.日付 into t03Group
//                             let t01l = from t01 in context.T01_TRN.Where(t01 => t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) select t01.車輌KEY
//                             let t02l = from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) select t02.車輌KEY
//                             let t03l = from t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) select t03.車輌ID
//                             where lst.Contains(m05.車輌KEY)
//                             //where t01l.Contains(m04.車輌KEY) || t02l.Contains(m04.車輌KEY) || t03l.Contains(m04.車輌KEY)
//                             orderby m05.車輌ID, retdate.日付
//                             select new SRY03010_Member_CSV
//                             {
//                                 日付 = retdate.日付,
//                                 社内金額 = t01Group.Where(t01gr => t01gr.車輌KEY == m05.車輌KEY && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２) == null ? 0 : t01Group.Where(t01gr => t01gr.車輌KEY == m05.車輌KEY && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２),
//                                 経費1 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 601).Sum(t03gr => t03gr.金額),
//                                 経費2 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 602).Sum(t03gr => t03gr.金額),
//                                 経費3 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 603).Sum(t03gr => t03gr.金額),
//                                 経費4 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 604).Sum(t03gr => t03gr.金額),
//                                 経費5 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 605).Sum(t03gr => t03gr.金額),
//                                 経費6 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 606).Sum(t03gr => t03gr.金額),
//                                 経費7 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 607).Sum(t03gr => t03gr.金額),
//                                 その他経費 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 &&
//                                                            (t03gr.経費項目ID != 601 && t03gr.経費項目ID != 602 && t03gr.経費項目ID != 604 && t03gr.経費項目ID != 605 &&
//                                                             t03gr.経費項目ID != 608 && t03gr.経費項目ID != 607 && t03gr.経費項目ID != 401)).Sum(t03gr => t03gr.金額),
//                                 燃料Ｌ数 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.数量),
//                                 燃料代 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.金額),
//                                 輸送屯数 = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数),
//                                 走行KM = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ),
//                                 実車KM = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ),
//                                 コード = m05.車輌ID,
//                                 車輌番号 = m05.車輌番号,
//                                 車種名 = m06g.車種名,

//                                 経費名1 = context.M07_KEI.Where(q => q.経費項目ID == 601).Select(c => c.経費項目名).FirstOrDefault(),
//                                 経費名2 = context.M07_KEI.Where(q => q.経費項目ID == 602).Select(c => c.経費項目名).FirstOrDefault(),
//                                 経費名3 = context.M07_KEI.Where(q => q.経費項目ID == 603).Select(c => c.経費項目名).FirstOrDefault(),
//                                 経費名4 = context.M07_KEI.Where(q => q.経費項目ID == 604).Select(c => c.経費項目名).FirstOrDefault(),
//                                 経費名5 = context.M07_KEI.Where(q => q.経費項目ID == 605).Select(c => c.経費項目名).FirstOrDefault(),
//                                 経費名6 = context.M07_KEI.Where(q => q.経費項目ID == 606).Select(c => c.経費項目名).FirstOrDefault(),
//                                 経費名7 = context.M07_KEI.Where(q => q.経費項目ID == 607).Select(c => c.経費項目名).FirstOrDefault(),
//                             }).AsQueryable();

//                if (!(string.IsNullOrEmpty(p車輌From + p車輌To) && i車輌List.Length == 0))
//                {

//                    //車輌が検索対象に入っていない時全データ取得
//                    if (string.IsNullOrEmpty(p車輌From + p車輌To))
//                    {
//                        query = query.Where(c => c.コード >= int.MaxValue);
//                    }

//                    //車輌From処理　Min値
//                    if (!string.IsNullOrEmpty(p車輌From))
//                    {
//                        int i車輌FROM = AppCommon.IntParse(p車輌From);
//                        query = query.Where(c => c.コード >= i車輌FROM);
//                    }

//                    //車輌To処理　Max値
//                    if (!string.IsNullOrEmpty(p車輌To))
//                    {
//                        int i車輌TO = AppCommon.IntParse(p車輌To);
//                        query = query.Where(c => c.コード <= i車輌TO);
//                    }


//                    if (string.IsNullOrEmpty(p車輌From + p車輌To) && i車輌List.Length > 0)
//                    {
//                        var intCause = i車輌List;
//                        query = query.Union(from retdate in retList2
//                                            from m05 in context.M05_CAR
//                                            join m06 in context.M06_SYA on m05.車種ID equals m06.車種ID into m06Group
//                                            from m06g in m06Group
//                                            join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) on retdate.日付 equals t01.請求日付 into t01Group
//                                            join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on retdate.日付 equals t02.労務日 into t02Group
//                                            join t03 in query2.Where(t03 => t03.日付 >= d集計期間From && t03.日付 <= d集計期間To) on retdate.日付 equals t03.日付 into t03Group
//                                            let t01l = from t01 in context.T01_TRN.Where(t01 => t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) select t01.車輌KEY
//                                            let t02l = from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) select t02.車輌KEY
//                                            let t03l = from t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) select t03.車輌ID
//                                            where lst.Contains(m05.車輌KEY) && intCause.Contains(m05.車輌ID)
//                                            //where t01l.Contains(m04.車輌KEY) || t02l.Contains(m04.車輌KEY) || t03l.Contains(m04.車輌KEY)
//                                            orderby m05.車輌ID, retdate.日付
//                                            select new SRY03010_Member_CSV
//                                            {
//                                                日付 = retdate.日付,
//                                                社内金額 = t01Group.Where(t01gr => t01gr.車輌KEY == m05.車輌KEY && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２) == null ? 0 : t01Group.Where(t01gr => t01gr.車輌KEY == m05.車輌KEY && t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２),
//                                                経費1 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 601).Sum(t03gr => t03gr.金額),
//                                                経費2 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 602).Sum(t03gr => t03gr.金額),
//                                                経費3 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 603).Sum(t03gr => t03gr.金額),
//                                                経費4 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 604).Sum(t03gr => t03gr.金額),
//                                                経費5 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 605).Sum(t03gr => t03gr.金額),
//                                                経費6 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 606).Sum(t03gr => t03gr.金額),
//                                                経費7 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 607).Sum(t03gr => t03gr.金額),
//                                                その他経費 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 &&
//                                                                           (t03gr.経費項目ID != 601 && t03gr.経費項目ID != 602 && t03gr.経費項目ID != 604 && t03gr.経費項目ID != 605 &&
//                                                                            t03gr.経費項目ID != 608 && t03gr.経費項目ID != 607 && t03gr.経費項目ID != 401)).Sum(t03gr => t03gr.金額),
//                                                燃料Ｌ数 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.数量),
//                                                燃料代 = t03Group.Where(t03gr => t03gr.車輌KEY == m05.車輌KEY && t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.金額),
//                                                輸送屯数 = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.輸送屯数),
//                                                走行KM = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.走行ＫＭ),
//                                                実車KM = t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ) == null ? 0 : t02Group.Where(t02gr => t02gr.車輌KEY == m05.車輌KEY && t02gr.労務日 == retdate.日付).Sum(t02gr => t02gr.実車ＫＭ),
//                                                コード = m05.車輌ID,
//                                                車輌番号 = m05.車輌番号,
//                                                車種名 = m06g.車種名,

//                                                経費名1 = context.M07_KEI.Where(q => q.経費項目ID == 601).Select(c => c.経費項目名).FirstOrDefault(),
//                                                経費名2 = context.M07_KEI.Where(q => q.経費項目ID == 602).Select(c => c.経費項目名).FirstOrDefault(),
//                                                経費名3 = context.M07_KEI.Where(q => q.経費項目ID == 603).Select(c => c.経費項目名).FirstOrDefault(),
//                                                経費名4 = context.M07_KEI.Where(q => q.経費項目ID == 604).Select(c => c.経費項目名).FirstOrDefault(),
//                                                経費名5 = context.M07_KEI.Where(q => q.経費項目ID == 605).Select(c => c.経費項目名).FirstOrDefault(),
//                                                経費名6 = context.M07_KEI.Where(q => q.経費項目ID == 606).Select(c => c.経費項目名).FirstOrDefault(),
//                                                経費名7 = context.M07_KEI.Where(q => q.経費項目ID == 607).Select(c => c.経費項目名).FirstOrDefault(),
//                                            });
//                    }

//                    else
//                    {
//                        query = query.Where(c => c.コード > int.MinValue && c.コード < int.MaxValue);

//                    }
//                }
//                query = query.Distinct();
//                //結果をリスト化
//                retList = query.ToList();
//                return retList;
//            }

//        }

//        #endregion
//    }
//}


#endregion