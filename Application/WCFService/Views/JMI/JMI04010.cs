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
    /// JMI04010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class JMI04010_Member
    {
        public int コード { get; set; }
        public string 乗務員名 { get; set; }
        public decimal 社内金額 { get; set; }
        public int? 経費1 { get; set; }
        public int? 経費2 { get; set; }
        public int? 経費3 { get; set; }
        public int? 経費4 { get; set; }
        public int? 経費5 { get; set; }
        public int? 経費6 { get; set; }
        public int? 経費7 { get; set; }
        public int? 経費8 { get; set; }
        public decimal? 燃料L { get; set; }
        public int? 燃料代 { get; set; }
        public decimal? 歩合金額 { get; set; }
        public decimal? 走行KM { get; set; }
        public DateTime 期間From { get; set; }
        public DateTime 期間To { get; set; }
        public string コードFrom { get; set; }
        public string コードTo { get; set; }
        public string コードList { get; set; }
        public string 経費名1 { get; set; }
        public string 経費名2 { get; set; }
        public string 経費名3 { get; set; }
        public string 経費名4 { get; set; }
        public string 経費名5 { get; set; }
        public string 経費名6 { get; set; }
        public string 経費名7 { get; set; }


    }


    /// <summary>
    /// JMI04010  CSV　メンバー
    /// </summary>
    [DataContract]
    public class JMI04010_Member_CSV
    {
        public int コード { get; set; }
        public string 乗務員名 { get; set; }
        public decimal 社内金額 { get; set; }
        public int? 経費1 { get; set; }
        public int? 経費2 { get; set; }
        public int? 経費3 { get; set; }
        public int? 経費4 { get; set; }
        public int? 経費5 { get; set; }
        public int? 経費6 { get; set; }
        public int? 経費7 { get; set; }
        public int? 他経費計 { get; set; }
        public decimal? 燃料L { get; set; }
        public int? 燃料代 { get; set; }
        public decimal? 歩合金額 { get; set; }
        public decimal? 走行KM { get; set; }
        public string 経費名1 { get; set; }
        public string 経費名2 { get; set; }
        public string 経費名3 { get; set; }
        public string 経費名4 { get; set; }
        public string 経費名5 { get; set; }
        public string 経費名6 { get; set; }
        public string 経費名7 { get; set; }

    }


    [DataContract]
    public class JMI04010_M07
    {
        [DataMember]
        public int 経費ID { get; set; }
        [DataMember]
        public string 経費名 { get; set; }
    }


    public class JMI04010
    {
        #region 印刷
        /// <summary>
        /// JMI04010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<JMI04010_Member> GetDataList(string p乗務員From, string p乗務員To, int?[] i乗務員List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string s乗務員List)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<JMI04010_Member> retList = new List<JMI04010_Member>();
                context.Connection.Open();

                var query = (from m04 in context.M04_DRV
                             join t01 in context.T01_TRN.Where(t01 => (t01.支払日付 >= d集計期間From && t01.支払日付 <= d集計期間To) && ((t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1))) on m04.乗務員KEY equals t01.乗務員KEY into t01Group
                             join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on m04.乗務員KEY equals t02.乗務員KEY into t02Group
                             join t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) on m04.乗務員KEY equals t03.乗務員KEY into t03Group
                             where t01Group.Where(t01 => t01.乗務員KEY == m04.乗務員KEY).Any() == true || t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Any() == true ||
                                                t03Group.Where(t03 => t03.乗務員KEY == m04.乗務員KEY).Any() == true
                             //from m78Group in sm78.DefaultIfEmpty()

                             select new JMI04010_Member
                             {
                                 コード = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 社内金額 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額 + t01.支払割増１ + t01.支払割増２ + t01.支払通行料) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額 + t01.支払割増１ + t01.支払割増２ + t01.支払通行料),
                                 経費1 = t03Group.Where(t03 => t03.経費項目ID == 601).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 601).Sum(t03 => t03.金額),
                                 経費2 = t03Group.Where(t03 => t03.経費項目ID == 602).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 602).Sum(t03 => t03.金額),
                                 経費3 = t03Group.Where(t03 => t03.経費項目ID == 603).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 603).Sum(t03 => t03.金額),
                                 経費4 = t03Group.Where(t03 => t03.経費項目ID == 604).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 604).Sum(t03 => t03.金額),
                                 経費5 = t03Group.Where(t03 => t03.経費項目ID == 605).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 605).Sum(t03 => t03.金額),
                                 経費6 = t03Group.Where(t03 => t03.経費項目ID == 606).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 606).Sum(t03 => t03.金額),
                                 経費7 = t03Group.Where(t03 => t03.経費項目ID == 607).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 607).Sum(t03 => t03.金額),
                                 経費8 = t03Group.Where(t03 => t03.経費項目ID != 601 && t03.経費項目ID != 602 && t03.経費項目ID != 603 &&
                                                               t03.経費項目ID != 604 && t03.経費項目ID != 605 && t03.経費項目ID != 606 &&
                                                               t03.経費項目ID != 607 && t03.経費項目ID != 401).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID != 601 && t03.経費項目ID != 602 && t03.経費項目ID != 603 &&
                                                               t03.経費項目ID != 604 && t03.経費項目ID != 605 && t03.経費項目ID != 606 &&
                                                               t03.経費項目ID != 607 && t03.経費項目ID != 401).Sum(t03 => t03.金額),
                                 燃料L = t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.数量) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.数量),
                                 燃料代 = t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.金額),
                                 歩合金額 = Math.Round((t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * m04.歩合率 / 100), 0) == null ? 0 : Math.Round((t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * m04.歩合率 / 100), 0),
                                 走行KM = t02Group.Sum(t02 => t02.走行ＫＭ) == null ? 0 : t02Group.Sum(t02 => t02.走行ＫＭ),

                                 コードFrom = p乗務員From,
                                 コードTo = p乗務員To,
                                 コードList = s乗務員List,
                                 期間From = d集計期間From,
                                 期間To = d集計期間To,

                                 経費名1 = (from m07 in context.M07_KEI where m07.経費項目ID == 601 select m07.経費項目名).FirstOrDefault(),
                                 経費名2 = (from m07 in context.M07_KEI where m07.経費項目ID == 602 select m07.経費項目名).FirstOrDefault(),
                                 経費名3 = (from m07 in context.M07_KEI where m07.経費項目ID == 603 select m07.経費項目名).FirstOrDefault(),
                                 経費名4 = (from m07 in context.M07_KEI where m07.経費項目ID == 604 select m07.経費項目名).FirstOrDefault(),
                                 経費名5 = (from m07 in context.M07_KEI where m07.経費項目ID == 605 select m07.経費項目名).FirstOrDefault(),
                                 経費名6 = (from m07 in context.M07_KEI where m07.経費項目ID == 606 select m07.経費項目名).FirstOrDefault(),
                                 経費名7 = (from m07 in context.M07_KEI where m07.経費項目ID == 607 select m07.経費項目名).FirstOrDefault(),

                             }).AsQueryable();

                if (!(string.IsNullOrEmpty(p乗務員From + p乗務員To) && i乗務員List.Length == 0))
                {

                    //乗務員が検索対象に入っていない時全データ取得
                    if (string.IsNullOrEmpty(p乗務員From + p乗務員To))
                    {
                        query = query.Where(c => c.コード >= int.MaxValue);
                    }

                    //乗務員From処理　Min値
                    if (!string.IsNullOrEmpty(p乗務員From))
                    {
                        int i乗務員FROM = AppCommon.IntParse(p乗務員From);
                        query = query.Where(c => c.コード >= i乗務員FROM);
                    }

                    //乗務員To処理　Max値
                    if (!string.IsNullOrEmpty(p乗務員To))
                    {
                        int i乗務員TO = AppCommon.IntParse(p乗務員To);
                        query = query.Where(c => c.コード <= i乗務員TO);
                    }

                    if (i乗務員List.Length > 0)
                    {
                        var intCause = i乗務員List;
                        query = query.Union(from m04 in context.M04_DRV
                                            join t01 in context.T01_TRN.Where(t01 => (t01.支払日付 >= d集計期間From && t01.支払日付 <= d集計期間To) && ((t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1))) on m04.乗務員KEY equals t01.乗務員KEY into t01Group
                                            join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on m04.乗務員KEY equals t02.乗務員KEY into t02Group
                                            join t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) on m04.乗務員KEY equals t03.乗務員KEY into t03Group
                                            where t01Group.Where(t01 => t01.乗務員KEY == m04.乗務員KEY).Any() == true && intCause.Contains(m04.乗務員ID) || t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Any() == true && intCause.Contains(m04.乗務員ID) ||
                                                        t03Group.Where(t03 => t03.乗務員KEY == m04.乗務員KEY).Any() == true && intCause.Contains(m04.乗務員ID)
                                            //from m78Group in sm78.DefaultIfEmpty()

                                            select new JMI04010_Member
                                            {
                                                コード = m04.乗務員ID,
                                                乗務員名 = m04.乗務員名,
                                                社内金額 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額),
                                                経費1 = t03Group.Where(t03 => t03.経費項目ID == 601).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 601).Sum(t03 => t03.金額),
                                                経費2 = t03Group.Where(t03 => t03.経費項目ID == 602).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 602).Sum(t03 => t03.金額),
                                                経費3 = t03Group.Where(t03 => t03.経費項目ID == 603).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 603).Sum(t03 => t03.金額),
                                                経費4 = t03Group.Where(t03 => t03.経費項目ID == 604).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 604).Sum(t03 => t03.金額),
                                                経費5 = t03Group.Where(t03 => t03.経費項目ID == 605).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 605).Sum(t03 => t03.金額),
                                                経費6 = t03Group.Where(t03 => t03.経費項目ID == 606).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 606).Sum(t03 => t03.金額),
                                                経費7 = t03Group.Where(t03 => t03.経費項目ID == 607).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 607).Sum(t03 => t03.金額),
                                                経費8 = t03Group.Where(t03 => t03.経費項目ID != 601 && t03.経費項目ID != 602 && t03.経費項目ID != 603 &&
                                                                              t03.経費項目ID != 604 && t03.経費項目ID != 605 && t03.経費項目ID != 606 &&
                                                                              t03.経費項目ID != 607 && t03.経費項目ID != 401).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID != 601 && t03.経費項目ID != 602 && t03.経費項目ID != 603 &&
                                                                              t03.経費項目ID != 604 && t03.経費項目ID != 605 && t03.経費項目ID != 606 &&
                                                                              t03.経費項目ID != 607 && t03.経費項目ID != 401).Sum(t03 => t03.金額),
                                                燃料L = t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.数量) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.数量),
                                                燃料代 = t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.金額),
                                                歩合金額 = Math.Round((t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * m04.歩合率 / 100), 0) == null ? 0 : Math.Round((t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * m04.歩合率 / 100), 0),
                                                走行KM = t02Group.Sum(t02 => t02.走行ＫＭ) == null ? 0 : t02Group.Sum(t02 => t02.走行ＫＭ),

                                                コードFrom = p乗務員From,
                                                コードTo = p乗務員To,
                                                コードList = s乗務員List,
                                                期間From = d集計期間From,
                                                期間To = d集計期間To,

                                                経費名1 = (from m07 in context.M07_KEI where m07.経費項目ID == 601 select m07.経費項目名).FirstOrDefault(),
                                                経費名2 = (from m07 in context.M07_KEI where m07.経費項目ID == 602 select m07.経費項目名).FirstOrDefault(),
                                                経費名3 = (from m07 in context.M07_KEI where m07.経費項目ID == 603 select m07.経費項目名).FirstOrDefault(),
                                                経費名4 = (from m07 in context.M07_KEI where m07.経費項目ID == 604 select m07.経費項目名).FirstOrDefault(),
                                                経費名5 = (from m07 in context.M07_KEI where m07.経費項目ID == 605 select m07.経費項目名).FirstOrDefault(),
                                                経費名6 = (from m07 in context.M07_KEI where m07.経費項目ID == 606 select m07.経費項目名).FirstOrDefault(),
                                                経費名7 = (from m07 in context.M07_KEI where m07.経費項目ID == 607 select m07.経費項目名).FirstOrDefault(),


                                            });
                    }
                    else
                    {
                        query = query.Where(c => c.コード > int.MinValue && c.コード < int.MaxValue);

                    }

                }
                query = query.Distinct();
                //結果をリスト化
                retList = query.ToList();
                return retList;
            }

        }
        #endregion



        #region CSV出力

        /// <summary>
        /// JMI04010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<JMI04010_Member_CSV> GetDataList_CSV(string p乗務員From, string p乗務員To, int?[] i乗務員List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string s乗務員List)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {

                List<JMI04010_Member_CSV> retList = new List<JMI04010_Member_CSV>();
                context.Connection.Open();

                var query = (from m04 in context.M04_DRV
                             join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && ((t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1))) on m04.乗務員KEY equals t01.乗務員KEY into t01Group
                             join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on m04.乗務員KEY equals t02.乗務員KEY into t02Group
                             join t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) on m04.乗務員KEY equals t03.乗務員KEY into t03Group
                             where t01Group.Where(t01 => t01.乗務員KEY == m04.乗務員KEY).Any() == true || t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Any() == true ||
                                                t03Group.Where(t03 => t03.乗務員KEY == m04.乗務員KEY).Any() == true
                             //from m78Group in sm78.DefaultIfEmpty()

                             select new JMI04010_Member_CSV
                             {
                                 コード = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 社内金額 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額),
                                 経費1 = t03Group.Where(t03 => t03.経費項目ID == 601).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 601).Sum(t03 => t03.金額),
                                 経費2 = t03Group.Where(t03 => t03.経費項目ID == 602).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 602).Sum(t03 => t03.金額),
                                 経費3 = t03Group.Where(t03 => t03.経費項目ID == 603).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 603).Sum(t03 => t03.金額),
                                 経費4 = t03Group.Where(t03 => t03.経費項目ID == 604).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 604).Sum(t03 => t03.金額),
                                 経費5 = t03Group.Where(t03 => t03.経費項目ID == 605).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 605).Sum(t03 => t03.金額),
                                 経費6 = t03Group.Where(t03 => t03.経費項目ID == 606).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 606).Sum(t03 => t03.金額),
                                 経費7 = t03Group.Where(t03 => t03.経費項目ID == 607).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 607).Sum(t03 => t03.金額),
                                 他経費計 = t03Group.Where(t03 => t03.経費項目ID != 601 && t03.経費項目ID != 602 && t03.経費項目ID != 603 &&
                                                               t03.経費項目ID != 604 && t03.経費項目ID != 605 && t03.経費項目ID != 606 &&
                                                               t03.経費項目ID != 607 && t03.経費項目ID != 401).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID != 601 && t03.経費項目ID != 602 && t03.経費項目ID != 603 &&
                                                               t03.経費項目ID != 604 && t03.経費項目ID != 605 && t03.経費項目ID != 606 &&
                                                               t03.経費項目ID != 607 && t03.経費項目ID != 401).Sum(t03 => t03.金額),
                                 燃料L = t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.数量) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.数量),
                                 燃料代 = t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.金額),
                                 歩合金額 = Math.Round((t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * m04.歩合率 / 100), 0) == null ? 0 : Math.Round((t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * m04.歩合率 / 100), 0),
                                 走行KM = t02Group.Sum(t02 => t02.走行ＫＭ) == null ? 0 : t02Group.Sum(t02 => t02.走行ＫＭ),

                                 経費名1 = (from m07 in context.M07_KEI where m07.経費項目ID == 601 select m07.経費項目名).FirstOrDefault(),
                                 経費名2 = (from m07 in context.M07_KEI where m07.経費項目ID == 602 select m07.経費項目名).FirstOrDefault(),
                                 経費名3 = (from m07 in context.M07_KEI where m07.経費項目ID == 603 select m07.経費項目名).FirstOrDefault(),
                                 経費名4 = (from m07 in context.M07_KEI where m07.経費項目ID == 604 select m07.経費項目名).FirstOrDefault(),
                                 経費名5 = (from m07 in context.M07_KEI where m07.経費項目ID == 605 select m07.経費項目名).FirstOrDefault(),
                                 経費名6 = (from m07 in context.M07_KEI where m07.経費項目ID == 606 select m07.経費項目名).FirstOrDefault(),
                                 経費名7 = (from m07 in context.M07_KEI where m07.経費項目ID == 607 select m07.経費項目名).FirstOrDefault(),


                             }).AsQueryable();

                if (!(string.IsNullOrEmpty(p乗務員From + p乗務員To) && i乗務員List.Length == 0))
                {

                    //乗務員が検索対象に入っていない時全データ取得
                    if (string.IsNullOrEmpty(p乗務員From + p乗務員To))
                    {
                        query = query.Where(c => c.コード >= int.MaxValue);
                    }

                    //乗務員From処理　Min値
                    if (!string.IsNullOrEmpty(p乗務員From))
                    {
                        int i乗務員FROM = AppCommon.IntParse(p乗務員From);
                        query = query.Where(c => c.コード >= i乗務員FROM);
                    }

                    //乗務員To処理　Max値
                    if (!string.IsNullOrEmpty(p乗務員To))
                    {
                        int i乗務員TO = AppCommon.IntParse(p乗務員To);
                        query = query.Where(c => c.コード <= i乗務員TO);
                    }

                    if (i乗務員List.Length > 0)
                    {
                        var intCause = i乗務員List;
                        query = query.Union(from m04 in context.M04_DRV
                                            join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && ((t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1))) on m04.乗務員KEY equals t01.乗務員KEY into t01Group
                                            join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on m04.乗務員KEY equals t02.乗務員KEY into t02Group
                                            join t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) on m04.乗務員KEY equals t03.乗務員KEY into t03Group
                                            where t01Group.Where(t01 => t01.乗務員KEY == m04.乗務員KEY).Any() == true && intCause.Contains(m04.乗務員ID) || t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Any() == true && intCause.Contains(m04.乗務員ID) ||
                                                               t03Group.Where(t03 => t03.乗務員KEY == m04.乗務員KEY).Any() == true && intCause.Contains(m04.乗務員ID)
                                            //from m78Group in sm78.DefaultIfEmpty()

                                            select new JMI04010_Member_CSV
                                            {
                                                コード = m04.乗務員ID,
                                                乗務員名 = m04.乗務員名,
                                                社内金額 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額),
                                                経費1 = t03Group.Where(t03 => t03.経費項目ID == 601).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 601).Sum(t03 => t03.金額),
                                                経費2 = t03Group.Where(t03 => t03.経費項目ID == 602).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 602).Sum(t03 => t03.金額),
                                                経費3 = t03Group.Where(t03 => t03.経費項目ID == 603).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 603).Sum(t03 => t03.金額),
                                                経費4 = t03Group.Where(t03 => t03.経費項目ID == 604).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 604).Sum(t03 => t03.金額),
                                                経費5 = t03Group.Where(t03 => t03.経費項目ID == 605).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 605).Sum(t03 => t03.金額),
                                                経費6 = t03Group.Where(t03 => t03.経費項目ID == 606).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 606).Sum(t03 => t03.金額),
                                                経費7 = t03Group.Where(t03 => t03.経費項目ID == 607).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 607).Sum(t03 => t03.金額),
                                                他経費計 = t03Group.Where(t03 => t03.経費項目ID != 601 && t03.経費項目ID != 602 && t03.経費項目ID != 603 &&
                                                                              t03.経費項目ID != 604 && t03.経費項目ID != 605 && t03.経費項目ID != 606 &&
                                                                              t03.経費項目ID != 607 && t03.経費項目ID != 401).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID != 601 && t03.経費項目ID != 602 && t03.経費項目ID != 603 &&
                                                                              t03.経費項目ID != 604 && t03.経費項目ID != 605 && t03.経費項目ID != 606 &&
                                                                              t03.経費項目ID != 607 && t03.経費項目ID != 401).Sum(t03 => t03.金額),
                                                燃料L = t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.数量) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.数量),
                                                燃料代 = t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.金額),
                                                歩合金額 = Math.Round((t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * m04.歩合率 / 100), 0) == null ? 0 : Math.Round((t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * m04.歩合率 / 100), 0),
                                                走行KM = t02Group.Sum(t02 => t02.走行ＫＭ) == null ? 0 : t02Group.Sum(t02 => t02.走行ＫＭ),

                                                経費名1 = (from m07 in context.M07_KEI where m07.経費項目ID == 601 select m07.経費項目名).FirstOrDefault(),
                                                経費名2 = (from m07 in context.M07_KEI where m07.経費項目ID == 602 select m07.経費項目名).FirstOrDefault(),
                                                経費名3 = (from m07 in context.M07_KEI where m07.経費項目ID == 603 select m07.経費項目名).FirstOrDefault(),
                                                経費名4 = (from m07 in context.M07_KEI where m07.経費項目ID == 604 select m07.経費項目名).FirstOrDefault(),
                                                経費名5 = (from m07 in context.M07_KEI where m07.経費項目ID == 605 select m07.経費項目名).FirstOrDefault(),
                                                経費名6 = (from m07 in context.M07_KEI where m07.経費項目ID == 606 select m07.経費項目名).FirstOrDefault(),
                                                経費名7 = (from m07 in context.M07_KEI where m07.経費項目ID == 607 select m07.経費項目名).FirstOrDefault(),

                                            });
                    }
                    else
                    {
                        query = query.Where(c => c.コード > int.MinValue && c.コード < int.MaxValue);

                    }

                }
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
//    /// JMI04010  印刷　メンバー
//    /// </summary>
//    [DataContract]
//    public class JMI04010_Member
//    {
//        public int コード { get; set; }
//        public string 乗務員名 { get; set; }
//        public decimal 社内金額 { get; set; }
//        public int? 経費1 { get; set; }
//        public int? 経費2 { get; set; }
//        public int? 経費3 { get; set; }
//        public int? 経費4 { get; set; }
//        public int? 経費5 { get; set; }
//        public int? 経費6 { get; set; }
//        public int? 経費7 { get; set; }
//        public int? 経費8 { get; set; }
//        public decimal? 燃料L { get; set; }
//        public int? 燃料代 { get; set; }
//        public decimal? 歩合金額 { get; set; }
//        public decimal? 走行KM { get; set; }
//        public DateTime 期間From { get; set; }
//        public DateTime 期間To { get; set; }
//        public string コードFrom { get; set; }
//        public string コードTo { get; set; }
//        public string コードList { get; set; }
//        public string 経費名1 { get; set; }
//        public string 経費名2 { get; set; }
//        public string 経費名3 { get; set; }
//        public string 経費名4 { get; set; }
//        public string 経費名5 { get; set; }
//        public string 経費名6 { get; set; }
//        public string 経費名7 { get; set; }


//    }


//    /// <summary>
//    /// JMI04010  CSV　メンバー
//    /// </summary>
//    [DataContract]
//    public class JMI04010_Member_CSV
//    {
//        public int コード { get; set; }
//        public string 乗務員名 { get; set; }
//        public decimal 社内金額 { get; set; }
//        public int? 経費1 { get; set; }
//        public int? 経費2 { get; set; }
//        public int? 経費3 { get; set; }
//        public int? 経費4 { get; set; }
//        public int? 経費5 { get; set; }
//        public int? 経費6 { get; set; }
//        public int? 経費7 { get; set; }
//        public int? 他経費計 { get; set; }
//        public decimal? 燃料L { get; set; }
//        public int? 燃料代 { get; set; }
//        public decimal? 歩合金額 { get; set; }
//        public decimal? 走行KM { get; set; }
//        public string 経費名1 { get; set; }
//        public string 経費名2 { get; set; }
//        public string 経費名3 { get; set; }
//        public string 経費名4 { get; set; }
//        public string 経費名5 { get; set; }
//        public string 経費名6 { get; set; }
//        public string 経費名7 { get; set; }

//    }


//    [DataContract]
//    public class JMI04010_M07
//    {
//        [DataMember]
//        public int 経費ID { get; set; }
//        [DataMember]
//        public string 経費名 { get; set; }
//    }
    

//    public class JMI04010
//    {
//        #region 印刷
//        /// <summary>
//        /// JMI04010 印刷
//        /// </summary>
//        /// <param name="p商品ID">乗務員コード</param>
//        /// <returns>T01</returns>
//        public List<JMI04010_Member> GetDataList(string p乗務員From, string p乗務員To, int?[] i乗務員List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string s乗務員List )
//        {
//            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
//            {
//                List<JMI04010_Member> retList = new List<JMI04010_Member>();
//                context.Connection.Open();

//                var query = (from m04 in context.M04_DRV
//                             join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && ((t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1))) on m04.乗務員KEY equals t01.乗務員KEY into t01Group
//                             join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on m04.乗務員KEY equals t02.乗務員KEY into t02Group
//                             join t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) on m04.乗務員KEY equals t03.乗務員KEY into t03Group
//                             where t01Group.Where(t01 => t01.乗務員KEY == m04.乗務員KEY).Any() == true || t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Any() == true ||
//                                                t03Group.Where(t03 => t03.乗務員KEY == m04.乗務員KEY).Any() == true
//                                //from m78Group in sm78.DefaultIfEmpty()

//                             select new JMI04010_Member
//                             {
//                                 コード = m04.乗務員ID,
//                                 乗務員名 = m04.乗務員名,
//                                 社内金額 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額),
//                                 経費1 = t03Group.Where(t03 => t03.経費項目ID == 601).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 601).Sum(t03 => t03.金額),
//                                 経費2 = t03Group.Where(t03 => t03.経費項目ID == 602).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 602).Sum(t03 => t03.金額),
//                                 経費3 = t03Group.Where(t03 => t03.経費項目ID == 603).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 603).Sum(t03 => t03.金額),
//                                 経費4 = t03Group.Where(t03 => t03.経費項目ID == 604).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 604).Sum(t03 => t03.金額),
//                                 経費5 = t03Group.Where(t03 => t03.経費項目ID == 605).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 605).Sum(t03 => t03.金額),
//                                 経費6 = t03Group.Where(t03 => t03.経費項目ID == 606).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 606).Sum(t03 => t03.金額),
//                                 経費7 = t03Group.Where(t03 => t03.経費項目ID == 607).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 607).Sum(t03 => t03.金額),
//                                 経費8 = t03Group.Where(t03 => t03.経費項目ID != 601 && t03.経費項目ID != 602  && t03.経費項目ID != 603 &&
//                                                               t03.経費項目ID != 604 && t03.経費項目ID != 605 && t03.経費項目ID != 606 &&
//                                                               t03.経費項目ID != 607 && t03.経費項目ID != 401).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID != 601 && t03.経費項目ID != 602  && t03.経費項目ID != 603 &&
//                                                               t03.経費項目ID != 604 && t03.経費項目ID != 605 && t03.経費項目ID != 606 &&
//                                                               t03.経費項目ID != 607 && t03.経費項目ID != 401).Sum(t03 => t03.金額),
//                                燃料L = t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.数量) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.数量),
//                                燃料代 = t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.金額),
//                                歩合金額 = Math.Round((t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * m04.歩合率 / 100), 0) == null ? 0 : Math.Round((t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * m04.歩合率 / 100), 0),
//                                走行KM = t02Group.Sum(t02 => t02.走行ＫＭ) == null ? 0 : t02Group.Sum(t02 => t02.走行ＫＭ),

//                                コードFrom = p乗務員From,
//                                コードTo = p乗務員To,
//                                コードList = s乗務員List,
//                                期間From = d集計期間From,
//                                期間To = d集計期間To,

//                                経費名1 = (from m07 in context.M07_KEI where m07.経費項目ID == 601 select m07.経費項目名).FirstOrDefault(),
//                                経費名2 = (from m07 in context.M07_KEI where m07.経費項目ID == 602 select m07.経費項目名).FirstOrDefault(),
//                                経費名3 = (from m07 in context.M07_KEI where m07.経費項目ID == 603 select m07.経費項目名).FirstOrDefault(),
//                                経費名4 = (from m07 in context.M07_KEI where m07.経費項目ID == 604 select m07.経費項目名).FirstOrDefault(),
//                                経費名5 = (from m07 in context.M07_KEI where m07.経費項目ID == 605 select m07.経費項目名).FirstOrDefault(),
//                                経費名6 = (from m07 in context.M07_KEI where m07.経費項目ID == 606 select m07.経費項目名).FirstOrDefault(),
//                                経費名7 = (from m07 in context.M07_KEI where m07.経費項目ID == 607 select m07.経費項目名).FirstOrDefault(),

//                             }).AsQueryable();

//                if (!(string.IsNullOrEmpty(p乗務員From + p乗務員To) && i乗務員List.Length == 0))
//                {

//                    //乗務員が検索対象に入っていない時全データ取得
//                    if (string.IsNullOrEmpty(p乗務員From + p乗務員To))
//                    {
//                        query = query.Where(c => c.コード >= int.MaxValue);
//                    }

//                    //乗務員From処理　Min値
//                    if (!string.IsNullOrEmpty(p乗務員From))
//                    {
//                        int i乗務員FROM = AppCommon.IntParse(p乗務員From);
//                        query = query.Where(c => c.コード >= i乗務員FROM);
//                    }

//                    //乗務員To処理　Max値
//                    if (!string.IsNullOrEmpty(p乗務員To))
//                    {
//                        int i乗務員TO = AppCommon.IntParse(p乗務員To);
//                        query = query.Where(c => c.コード <= i乗務員TO);
//                    }

//                    if (i乗務員List.Length > 0)
//                    {
//                        var intCause = i乗務員List;
//                        query = query.Union(from m04 in context.M04_DRV
//                                            join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && ((t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1))) on m04.乗務員KEY equals t01.乗務員KEY into t01Group
//                                     join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on m04.乗務員KEY equals t02.乗務員KEY into t02Group
//                                     join t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) on m04.乗務員KEY equals t03.乗務員KEY into t03Group
//                                            where t01Group.Where(t01 => t01.乗務員KEY == m04.乗務員KEY).Any() == true && intCause.Contains(m04.乗務員ID) || t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Any() == true && intCause.Contains(m04.乗務員ID) ||
//                                                        t03Group.Where(t03 => t03.乗務員KEY == m04.乗務員KEY).Any() == true && intCause.Contains(m04.乗務員ID)
//                                     //from m78Group in sm78.DefaultIfEmpty()

//                                     select new JMI04010_Member
//                                     {
//                                         コード = m04.乗務員ID,
//                                         乗務員名 = m04.乗務員名,
//                                         社内金額 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額),
//                                         経費1 = t03Group.Where(t03 => t03.経費項目ID == 601).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 601).Sum(t03 => t03.金額),
//                                         経費2 = t03Group.Where(t03 => t03.経費項目ID == 602).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 602).Sum(t03 => t03.金額),
//                                         経費3 = t03Group.Where(t03 => t03.経費項目ID == 603).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 603).Sum(t03 => t03.金額),
//                                         経費4 = t03Group.Where(t03 => t03.経費項目ID == 604).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 604).Sum(t03 => t03.金額),
//                                         経費5 = t03Group.Where(t03 => t03.経費項目ID == 605).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 605).Sum(t03 => t03.金額),
//                                         経費6 = t03Group.Where(t03 => t03.経費項目ID == 606).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 606).Sum(t03 => t03.金額),
//                                         経費7 = t03Group.Where(t03 => t03.経費項目ID == 607).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 607).Sum(t03 => t03.金額),
//                                         経費8 = t03Group.Where(t03 => t03.経費項目ID != 601 && t03.経費項目ID != 602 && t03.経費項目ID != 603 &&
//                                                                       t03.経費項目ID != 604 && t03.経費項目ID != 605 && t03.経費項目ID != 606 &&
//                                                                       t03.経費項目ID != 607 && t03.経費項目ID != 401).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID != 601 && t03.経費項目ID != 602 && t03.経費項目ID != 603 &&
//                                                                       t03.経費項目ID != 604 && t03.経費項目ID != 605 && t03.経費項目ID != 606 &&
//                                                                       t03.経費項目ID != 607 && t03.経費項目ID != 401).Sum(t03 => t03.金額),
//                                         燃料L = t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.数量) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.数量),
//                                         燃料代 = t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.金額),
//                                         歩合金額 = Math.Round((t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * m04.歩合率 / 100), 0) == null ? 0 : Math.Round((t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * m04.歩合率 / 100), 0),
//                                         走行KM = t02Group.Sum(t02 => t02.走行ＫＭ) == null ? 0 : t02Group.Sum(t02 => t02.走行ＫＭ),

//                                         コードFrom = p乗務員From,
//                                         コードTo = p乗務員To,
//                                         コードList = s乗務員List,
//                                         期間From = d集計期間From,
//                                         期間To = d集計期間To,

//                                         経費名1 = (from m07 in context.M07_KEI where m07.経費項目ID == 601 select m07.経費項目名).FirstOrDefault(),
//                                         経費名2 = (from m07 in context.M07_KEI where m07.経費項目ID == 602 select m07.経費項目名).FirstOrDefault(),
//                                         経費名3 = (from m07 in context.M07_KEI where m07.経費項目ID == 603 select m07.経費項目名).FirstOrDefault(),
//                                         経費名4 = (from m07 in context.M07_KEI where m07.経費項目ID == 604 select m07.経費項目名).FirstOrDefault(),
//                                         経費名5 = (from m07 in context.M07_KEI where m07.経費項目ID == 605 select m07.経費項目名).FirstOrDefault(),
//                                         経費名6 = (from m07 in context.M07_KEI where m07.経費項目ID == 606 select m07.経費項目名).FirstOrDefault(),
//                                         経費名7 = (from m07 in context.M07_KEI where m07.経費項目ID == 607 select m07.経費項目名).FirstOrDefault(),


//                                     });
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
//        /// JMI04010 印刷
//        /// </summary>
//        /// <param name="p商品ID">乗務員コード</param>
//        /// <returns>T01</returns>
//        public List<JMI04010_Member_CSV> GetDataList_CSV(string p乗務員From, string p乗務員To, int?[] i乗務員List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string s乗務員List)
//        {
//            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
//            {

//                List<JMI04010_Member_CSV> retList = new List<JMI04010_Member_CSV>();
//                context.Connection.Open();

//                var query = (from m04 in context.M04_DRV
//                             join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && ((t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1))) on m04.乗務員KEY equals t01.乗務員KEY into t01Group
//                             join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on m04.乗務員KEY equals t02.乗務員KEY into t02Group
//                             join t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) on m04.乗務員KEY equals t03.乗務員KEY into t03Group
//                             where t01Group.Where(t01 => t01.乗務員KEY == m04.乗務員KEY).Any() == true || t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Any() == true ||
//                                                t03Group.Where(t03 => t03.乗務員KEY == m04.乗務員KEY).Any() == true
//                             //from m78Group in sm78.DefaultIfEmpty()

//                             select new JMI04010_Member_CSV
//                             {
//                                 コード = m04.乗務員ID,
//                                 乗務員名 = m04.乗務員名,
//                                 社内金額 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額),
//                                 経費1 = t03Group.Where(t03 => t03.経費項目ID == 601).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 601).Sum(t03 => t03.金額),
//                                 経費2 = t03Group.Where(t03 => t03.経費項目ID == 602).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 602).Sum(t03 => t03.金額),
//                                 経費3 = t03Group.Where(t03 => t03.経費項目ID == 603).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 603).Sum(t03 => t03.金額),
//                                 経費4 = t03Group.Where(t03 => t03.経費項目ID == 604).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 604).Sum(t03 => t03.金額),
//                                 経費5 = t03Group.Where(t03 => t03.経費項目ID == 605).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 605).Sum(t03 => t03.金額),
//                                 経費6 = t03Group.Where(t03 => t03.経費項目ID == 606).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 606).Sum(t03 => t03.金額),
//                                 経費7 = t03Group.Where(t03 => t03.経費項目ID == 607).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 607).Sum(t03 => t03.金額),
//                                 他経費計 = t03Group.Where(t03 => t03.経費項目ID != 601 && t03.経費項目ID != 602 && t03.経費項目ID != 603 &&
//                                                               t03.経費項目ID != 604 && t03.経費項目ID != 605 && t03.経費項目ID != 606 &&
//                                                               t03.経費項目ID != 607 && t03.経費項目ID != 401).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID != 601 && t03.経費項目ID != 602 && t03.経費項目ID != 603 &&
//                                                               t03.経費項目ID != 604 && t03.経費項目ID != 605 && t03.経費項目ID != 606 &&
//                                                               t03.経費項目ID != 607 && t03.経費項目ID != 401).Sum(t03 => t03.金額),
//                                 燃料L = t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.数量) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.数量),
//                                 燃料代 = t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.金額),
//                                 歩合金額 = Math.Round((t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * m04.歩合率 / 100), 0) == null ? 0 : Math.Round((t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * m04.歩合率 / 100), 0),
//                                 走行KM = t02Group.Sum(t02 => t02.走行ＫＭ) == null ? 0 : t02Group.Sum(t02 => t02.走行ＫＭ),

//                                経費名1 = (from m07 in context.M07_KEI where m07.経費項目ID == 601 select m07.経費項目名).FirstOrDefault(),
//                                経費名2 = (from m07 in context.M07_KEI where m07.経費項目ID == 602 select m07.経費項目名).FirstOrDefault(),
//                                経費名3 = (from m07 in context.M07_KEI where m07.経費項目ID == 603 select m07.経費項目名).FirstOrDefault(),
//                                経費名4 = (from m07 in context.M07_KEI where m07.経費項目ID == 604 select m07.経費項目名).FirstOrDefault(),
//                                経費名5 = (from m07 in context.M07_KEI where m07.経費項目ID == 605 select m07.経費項目名).FirstOrDefault(),
//                                経費名6 = (from m07 in context.M07_KEI where m07.経費項目ID == 606 select m07.経費項目名).FirstOrDefault(),
//                                経費名7 = (from m07 in context.M07_KEI where m07.経費項目ID == 607 select m07.経費項目名).FirstOrDefault(),


//                             }).AsQueryable();

//                if (!(string.IsNullOrEmpty(p乗務員From + p乗務員To) && i乗務員List.Length == 0))
//                {

//                    //乗務員が検索対象に入っていない時全データ取得
//                    if (string.IsNullOrEmpty(p乗務員From + p乗務員To))
//                    {
//                        query = query.Where(c => c.コード >= int.MaxValue);
//                    }

//                    //乗務員From処理　Min値
//                    if (!string.IsNullOrEmpty(p乗務員From))
//                    {
//                        int i乗務員FROM = AppCommon.IntParse(p乗務員From);
//                        query = query.Where(c => c.コード >= i乗務員FROM);
//                    }

//                    //乗務員To処理　Max値
//                    if (!string.IsNullOrEmpty(p乗務員To))
//                    {
//                        int i乗務員TO = AppCommon.IntParse(p乗務員To);
//                        query = query.Where(c => c.コード <= i乗務員TO);
//                    }

//                    if (i乗務員List.Length > 0)
//                    {
//                        var intCause = i乗務員List;
//                        query = query.Union(from m04 in context.M04_DRV
//                                            join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && ((t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1))) on m04.乗務員KEY equals t01.乗務員KEY into t01Group
//                                            join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on m04.乗務員KEY equals t02.乗務員KEY into t02Group
//                                            join t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) on m04.乗務員KEY equals t03.乗務員KEY into t03Group
//                                            where t01Group.Where(t01 => t01.乗務員KEY == m04.乗務員KEY).Any() == true && intCause.Contains(m04.乗務員ID) || t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Any() == true && intCause.Contains(m04.乗務員ID) ||
//                                                               t03Group.Where(t03 => t03.乗務員KEY == m04.乗務員KEY).Any() == true && intCause.Contains(m04.乗務員ID)
//                                            //from m78Group in sm78.DefaultIfEmpty()

//                                            select new JMI04010_Member_CSV
//                                            {
//                                                コード = m04.乗務員ID,
//                                                乗務員名 = m04.乗務員名,
//                                                社内金額 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額),
//                                                経費1 = t03Group.Where(t03 => t03.経費項目ID == 601).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 601).Sum(t03 => t03.金額),
//                                                経費2 = t03Group.Where(t03 => t03.経費項目ID == 602).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 602).Sum(t03 => t03.金額),
//                                                経費3 = t03Group.Where(t03 => t03.経費項目ID == 603).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 603).Sum(t03 => t03.金額),
//                                                経費4 = t03Group.Where(t03 => t03.経費項目ID == 604).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 604).Sum(t03 => t03.金額),
//                                                経費5 = t03Group.Where(t03 => t03.経費項目ID == 605).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 605).Sum(t03 => t03.金額),
//                                                経費6 = t03Group.Where(t03 => t03.経費項目ID == 606).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 606).Sum(t03 => t03.金額),
//                                                経費7 = t03Group.Where(t03 => t03.経費項目ID == 607).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 607).Sum(t03 => t03.金額),
//                                                他経費計 = t03Group.Where(t03 => t03.経費項目ID != 601 && t03.経費項目ID != 602 && t03.経費項目ID != 603 &&
//                                                                              t03.経費項目ID != 604 && t03.経費項目ID != 605 && t03.経費項目ID != 606 &&
//                                                                              t03.経費項目ID != 607 && t03.経費項目ID != 401).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID != 601 && t03.経費項目ID != 602 && t03.経費項目ID != 603 &&
//                                                                              t03.経費項目ID != 604 && t03.経費項目ID != 605 && t03.経費項目ID != 606 &&
//                                                                              t03.経費項目ID != 607 && t03.経費項目ID != 401).Sum(t03 => t03.金額),
//                                                燃料L = t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.数量) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.数量),
//                                                燃料代 = t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.金額),
//                                                歩合金額 = Math.Round((t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * m04.歩合率 / 100), 0) == null ? 0 : Math.Round((t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * m04.歩合率 / 100), 0),
//                                                走行KM = t02Group.Sum(t02 => t02.走行ＫＭ) == null ? 0 : t02Group.Sum(t02 => t02.走行ＫＭ),

//                                                経費名1 = (from m07 in context.M07_KEI where m07.経費項目ID == 601 select m07.経費項目名).FirstOrDefault(),
//                                                経費名2 = (from m07 in context.M07_KEI where m07.経費項目ID == 602 select m07.経費項目名).FirstOrDefault(),
//                                                経費名3 = (from m07 in context.M07_KEI where m07.経費項目ID == 603 select m07.経費項目名).FirstOrDefault(),
//                                                経費名4 = (from m07 in context.M07_KEI where m07.経費項目ID == 604 select m07.経費項目名).FirstOrDefault(),
//                                                経費名5 = (from m07 in context.M07_KEI where m07.経費項目ID == 605 select m07.経費項目名).FirstOrDefault(),
//                                                経費名6 = (from m07 in context.M07_KEI where m07.経費項目ID == 606 select m07.経費項目名).FirstOrDefault(),
//                                                経費名7 = (from m07 in context.M07_KEI where m07.経費項目ID == 607 select m07.経費項目名).FirstOrDefault(),

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