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
    /// DLY23010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class DLY23010_Member
    {
        public DateTime? 日付 { get; set; }
        public decimal? 売上金額 { get; set; }
        public decimal? 請求通行料 { get; set; }
        public decimal? 支払金額 { get; set; }
        public decimal? 支払通行料 { get; set; }
        public decimal? 社内合計 { get; set; }
        public decimal? 数量 { get; set; }
        public decimal? 重量 { get; set; }
        public int? 走行KM { get; set; }
        public int? 実車KM { get; set; }
        public decimal? 燃料L { get; set; }
        public decimal? 燃料代 { get; set; }
        public decimal? 高速代 { get; set; }
        public decimal? その他経費 { get; set; }
        public decimal? 経費合計 { get; set; }
        public DateTime 期間From { get; set; }
        public DateTime 期間To { get; set; }

    }

    /// <summary>
    /// DLY23010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class DLY23010_Date
    {
        public DateTime? 日付 { get; set; }
    }

    /// <summary>
    /// DLY23010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class DLY23010_KTRN
    {
        public DateTime? 日付 { get; set; }
        public int? 経費項目ID { get; set; }
        public int? 金額 { get; set; }
        public decimal? 数量 { get; set; }
    }

    /// <summary>
    /// DLY23010  CSV　メンバー
    /// </summary>
    [DataContract]
    public class DLY23010_Member_CSV
    {
        public DateTime? 日付 { get; set; }
        public decimal? 売上金額 { get; set; }
        public decimal? 請求通行料 { get; set; }
        public decimal? 支払金額 { get; set; }
        public decimal? 支払通行料 { get; set; }
        public decimal? 社内合計 { get; set; }
        public decimal? 数量 { get; set; }
        public decimal? 重量 { get; set; }
        public int? 走行KM { get; set; }
        public int? 実車KM { get; set; }
        public decimal? 燃料L { get; set; }
        public decimal? 燃料代 { get; set; }
        public decimal? 高速代 { get; set; }
        public decimal? その他経費 { get; set; }
        public decimal? 経費合計 { get; set; }

    }


    [DataContract]
    public class DLY23010_M07
    {
        [DataMember]
        public int 経費ID { get; set; }
        [DataMember]
        public string 経費名 { get; set; }
    }
    

    public class DLY23010
    {
        #region 印刷
        /// <summary>
        /// DLY23010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<DLY23010_Member> GetDataList(string p乗務員From, string p乗務員To, int?[] i乗務員List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string s乗務員List )
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<DLY23010_Member> retList = new List<DLY23010_Member>();
                List<DLY23010_Date> retList2 = new List<DLY23010_Date>();

                context.Connection.Open();

                for (DateTime dDate = d集計期間From; dDate <= d集計期間To; dDate = dDate.AddDays(1))
                {
                    retList2.Add(new DLY23010_Date() { 日付 = dDate});
                }

                var query2 = (from t in context.T03_KTRN
                              join m07 in context.M07_KEI on t.経費項目ID equals m07.経費項目ID
							  where m07.固定変動区分 == 1 && t.経費発生日 >= d集計期間From && t.経費発生日 <= d集計期間To
                              select new DLY23010_KTRN
                             {
                              日付 = t.経費発生日,
                              経費項目ID = t.経費項目ID,
                              金額 = t.金額,
                              数量 = t.数量,
                             }).AsQueryable();


                var query = (from retdate in retList2
							 join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 == 3 && t01.明細行 == 1) || (t01.入力区分 != 3)) on retdate.日付 equals t01.請求日付 into t01Group
							 join t01_2 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 == 3 && t01.明細行 != 1) || (t01.入力区分 != 3)) on retdate.日付 equals t01_2.請求日付 into t01_2Group
							 join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on retdate.日付 equals t02.労務日 into t02Group
                             join t03 in query2.Where(t03 => t03.日付  >= d集計期間From && t03.日付 <= d集計期間To ) on retdate.日付 equals t03.日付 into t03Group
							 //let t01l = from t01 in context.T01_TRN.Where(t01 => t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) select t01.乗務員KEY
							 //let t02l = from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) select t02.乗務員KEY
							 //let t03l = from t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) select t03.乗務員KEY
                             orderby retdate.日付
                             select new DLY23010_Member
                             {
                                 日付 = retdate.日付,
                                 売上金額 = t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２) == null ? 0 : t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２),
                                 請求通行料 = t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.通行料) == null ? 0 : t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.通行料),
                                 支払金額 = t01_2Group.Where(t01gr => t01gr.支払日付 == retdate.日付 && (t01gr.支払先KEY != null && t01gr.支払先KEY != 0)).Sum(t01gr => t01gr.支払金額) == null ? 0 : t01_2Group.Where(t01gr => t01gr.支払日付 == retdate.日付 && (t01gr.支払先KEY != null && t01gr.支払先KEY != 0)).Sum(t01gr => t01gr.支払金額),
                                 支払通行料 = t01_2Group.Where(t01gr => t01gr.支払日付 == retdate.日付 && (t01gr.支払先KEY != null && t01gr.支払先KEY != 0)).Sum(t01gr => t01gr.支払通行料) == null ? 0 : t01_2Group.Where(t01gr => t01gr.支払日付 == retdate.日付 && (t01gr.支払先KEY != null && t01gr.支払先KEY != 0)).Sum(t01gr => t01gr.支払通行料),
								 //支払金額 = t01Group.Where(t01gr => t01gr.支払日付 == retdate.日付 && (t01gr.支払先KEY != null && t01gr.支払先KEY != 0)).Sum(t01gr => t01gr.支払金額) == null ? 0 : t01Group.Where(t01gr => t01gr.支払日付 == retdate.日付 && (t01gr.支払先KEY != null && t01gr.支払先KEY != 0)).Sum(t01gr => t01gr.支払金額),
								 //支払通行料 = t01Group.Where(t01gr => t01gr.支払日付 == retdate.日付 && (t01gr.支払先KEY != null && t01gr.支払先KEY != 0)).Sum(t01gr => t01gr.支払通行料) == null ? 0 : t01Group.Where(t01gr => t01gr.支払日付 == retdate.日付 && (t01gr.支払先KEY != null && t01gr.支払先KEY != 0)).Sum(t01gr => t01gr.支払通行料),
								 社内合計 = t01_2Group.Where(t01gr => t01gr.請求日付 == retdate.日付 && (t01gr.乗務員KEY != null && t01gr.乗務員KEY != 0)).Sum(t01gr => t01gr.支払金額 + t01gr.支払通行料) == null ? 0 : t01_2Group.Where(t01gr => t01gr.請求日付 == retdate.日付 && (t01gr.乗務員KEY != null && t01gr.乗務員KEY != 0)).Sum(t01gr => t01gr.支払金額 + t01gr.支払通行料),
                                 数量 = t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.数量) == null ? 0 : t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.数量),
                                 重量 = t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.重量) == null ? 0 : t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.重量),
                                 走行KM = t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.走行ＫＭ) == null ? 0 : t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.走行ＫＭ),
                                 実車KM = t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.実車ＫＭ) == null ? 0 : t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.実車ＫＭ),
                                 燃料L = t03Group.Where(t03gr => t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.数量) == null ? 0 : t03Group.Where(t03gr => t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.数量),
                                 燃料代 = t03Group.Where(t03gr => t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.金額) == null ? 0 : t03Group.Where(t03gr => t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.金額),
                                 高速代 = t03Group.Where(t03gr => t03gr.日付 == retdate.日付 && (t03gr.経費項目ID == 601 || t03gr.経費項目ID == 602)).Sum(t03gr => t03gr.金額) == null ? 0 : t03Group.Where(t03gr => t03gr.日付 == retdate.日付 && (t03gr.経費項目ID == 601 || t03gr.経費項目ID == 602)).Sum(t03gr => t03gr.金額),
                                 その他経費 = t03Group.Where(t03gr => t03gr.日付 == retdate.日付 && (t03gr.経費項目ID != 401 && t03gr.経費項目ID != 601 && t03gr.経費項目ID != 602)).Sum(t03gr => t03gr.金額) == null ? 0 : t03Group.Where(t03gr => t03gr.日付 == retdate.日付 && (t03gr.経費項目ID != 401 && t03gr.経費項目ID != 601 && t03gr.経費項目ID != 602)).Sum(t03gr => t03gr.金額),
                                 経費合計 = t03Group.Where(t03gr => t03gr.日付 == retdate.日付).Sum(t03gr => t03gr.金額) == null ? 0 : t03Group.Where(t03gr => t03gr.日付 == retdate.日付).Sum(t03gr => t03gr.金額),
                                期間From = d集計期間From,
                                期間To = d集計期間To,
                             }).AsQueryable();

                query = query.Distinct();
                //結果をリスト化
                retList = query.ToList();
                return retList;
            }

        }
        #endregion

        #region CSV出力
        /// <summary>
        /// DLY23010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<DLY23010_Member_CSV> GetDataList_CSV(string p乗務員From, string p乗務員To, int?[] i乗務員List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string s乗務員List)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<DLY23010_Member_CSV> retList = new List<DLY23010_Member_CSV>();
                List<DLY23010_Date> retList2 = new List<DLY23010_Date>();

                context.Connection.Open();

                for (DateTime dDate = d集計期間From; dDate <= d集計期間To; dDate = dDate.AddDays(1))
                {
                    retList2.Add(new DLY23010_Date() { 日付 = dDate });
                }

                var query2 = (from t in context.T03_KTRN
							  join m07 in context.M07_KEI on t.経費項目ID equals m07.経費項目ID
							  where m07.固定変動区分 == 1 && t.経費発生日 >= d集計期間From && t.経費発生日 <= d集計期間To
                              select new DLY23010_KTRN
                              {
                                  日付 = t.経費発生日,
                                  経費項目ID = t.経費項目ID,
                                  金額 = t.金額,
                                  数量 = t.数量,
                              }).AsQueryable();

				var query = (from retdate in retList2
							 join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 == 3 && t01.明細行 == 1) || (t01.入力区分 != 3)) on retdate.日付 equals t01.請求日付 into t01Group
							 join t01_2 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 == 3 && t01.明細行 != 1) || (t01.入力区分 != 3)) on retdate.日付 equals t01_2.請求日付 into t01_2Group
							 join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on retdate.日付 equals t02.労務日 into t02Group
							 join t03 in query2.Where(t03 => t03.日付 >= d集計期間From && t03.日付 <= d集計期間To) on retdate.日付 equals t03.日付 into t03Group
							 //let t01l = from t01 in context.T01_TRN.Where(t01 => t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) select t01.乗務員KEY
							 //let t02l = from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) select t02.乗務員KEY
							 //let t03l = from t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) select t03.乗務員KEY
							 orderby retdate.日付
							 select new DLY23010_Member_CSV
							 {
								 日付 = retdate.日付,
								 売上金額 = t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２) == null ? 0 : t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２),
								 請求通行料 = t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.通行料) == null ? 0 : t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.通行料),
								 支払金額 = t01_2Group.Where(t01gr => t01gr.支払日付 == retdate.日付 && (t01gr.支払先KEY != null && t01gr.支払先KEY != 0)).Sum(t01gr => t01gr.支払金額) == null ? 0 : t01_2Group.Where(t01gr => t01gr.支払日付 == retdate.日付 && (t01gr.支払先KEY != null && t01gr.支払先KEY != 0)).Sum(t01gr => t01gr.支払金額),
								 支払通行料 = t01_2Group.Where(t01gr => t01gr.支払日付 == retdate.日付 && (t01gr.支払先KEY != null && t01gr.支払先KEY != 0)).Sum(t01gr => t01gr.支払通行料) == null ? 0 : t01_2Group.Where(t01gr => t01gr.支払日付 == retdate.日付 && (t01gr.支払先KEY != null && t01gr.支払先KEY != 0)).Sum(t01gr => t01gr.支払通行料),
								 //支払金額 = t01Group.Where(t01gr => t01gr.支払日付 == retdate.日付 && (t01gr.支払先KEY != null && t01gr.支払先KEY != 0)).Sum(t01gr => t01gr.支払金額) == null ? 0 : t01Group.Where(t01gr => t01gr.支払日付 == retdate.日付 && (t01gr.支払先KEY != null && t01gr.支払先KEY != 0)).Sum(t01gr => t01gr.支払金額),
								 //支払通行料 = t01Group.Where(t01gr => t01gr.支払日付 == retdate.日付 && (t01gr.支払先KEY != null && t01gr.支払先KEY != 0)).Sum(t01gr => t01gr.支払通行料) == null ? 0 : t01Group.Where(t01gr => t01gr.支払日付 == retdate.日付 && (t01gr.支払先KEY != null && t01gr.支払先KEY != 0)).Sum(t01gr => t01gr.支払通行料),
								 社内合計 = t01_2Group.Where(t01gr => t01gr.請求日付 == retdate.日付 && (t01gr.乗務員KEY != null && t01gr.乗務員KEY != 0)).Sum(t01gr => t01gr.支払金額 + t01gr.支払通行料) == null ? 0 : t01_2Group.Where(t01gr => t01gr.請求日付 == retdate.日付 && (t01gr.乗務員KEY != null && t01gr.乗務員KEY != 0)).Sum(t01gr => t01gr.支払金額 + t01gr.支払通行料),
								 数量 = t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.数量) == null ? 0 : t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.数量),
								 重量 = t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.重量) == null ? 0 : t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.重量),
								 走行KM = t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.走行ＫＭ) == null ? 0 : t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.走行ＫＭ),
								 実車KM = t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.実車ＫＭ) == null ? 0 : t01Group.Where(t01gr => t01gr.請求日付 == retdate.日付).Sum(t01gr => t01gr.実車ＫＭ),
								 燃料L = t03Group.Where(t03gr => t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.数量) == null ? 0 : t03Group.Where(t03gr => t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.数量),
								 燃料代 = t03Group.Where(t03gr => t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.金額) == null ? 0 : t03Group.Where(t03gr => t03gr.日付 == retdate.日付 && t03gr.経費項目ID == 401).Sum(t03gr => t03gr.金額),
								 高速代 = t03Group.Where(t03gr => t03gr.日付 == retdate.日付 && (t03gr.経費項目ID == 601 || t03gr.経費項目ID == 602)).Sum(t03gr => t03gr.金額) == null ? 0 : t03Group.Where(t03gr => t03gr.日付 == retdate.日付 && (t03gr.経費項目ID == 601 || t03gr.経費項目ID == 602)).Sum(t03gr => t03gr.金額),
								 その他経費 = t03Group.Where(t03gr => t03gr.日付 == retdate.日付 && (t03gr.経費項目ID != 401 && t03gr.経費項目ID != 601 && t03gr.経費項目ID != 602)).Sum(t03gr => t03gr.金額) == null ? 0 : t03Group.Where(t03gr => t03gr.日付 == retdate.日付 && (t03gr.経費項目ID != 401 && t03gr.経費項目ID != 601 && t03gr.経費項目ID != 602)).Sum(t03gr => t03gr.金額),
								 経費合計 = t03Group.Where(t03gr => t03gr.日付 == retdate.日付).Sum(t03gr => t03gr.金額) == null ? 0 : t03Group.Where(t03gr => t03gr.日付 == retdate.日付).Sum(t03gr => t03gr.金額),
							 }).AsQueryable();


                query = query.Distinct();
                //結果をリスト化
                retList = query.ToList();
                return retList;
            }

        }
        #endregion
    }
}