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
    /// JMI02010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class JMI
    {
        public DateTime 日付 { get; set; }
        public int day { get; set; }
    }
    /// <summary>
    /// JMI02010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class JMI02010_Member
    {

        [DataMember]
        public DateTime 出庫 { get; set; }
        [DataMember]
        public DateTime 帰庫 { get; set; }
        [DataMember]
        public string 車番 { get; set; }
        [DataMember]
        public decimal? 社内金額 { get; set; }
        [DataMember]
        public int? 経費1 { get; set; }
        [DataMember]
        public int? 経費2 { get; set; }
        [DataMember]
        public int? 経費3 { get; set; }
        [DataMember]
        public int? 経費4 { get; set; }
        [DataMember]
        public int? 経費5 { get; set; }
        [DataMember]
        public int? 経費6 { get; set; }
        [DataMember]
        public int? 経費7 { get; set; }
        [DataMember]
        public int? その他経費 { get; set; }
        [DataMember]
        public decimal? 燃料Ｌ数 { get; set; }
        [DataMember]
        public decimal? 燃料代 { get; set; }
        [DataMember]
        public decimal? 走行KM { get; set; }
        [DataMember]
        public decimal? 歩合率 { get; set; }
        [DataMember]
        public decimal? 歩合金額 { get; set; }

        [DataMember]
        public int コード { get; set; }
        [DataMember]
        public string 乗務員名 { get; set; }
        [DataMember]
        public DateTime 期間From { get; set; }
        [DataMember]
        public DateTime 期間To { get; set; }

        [DataMember]
        public string 経費項目1 { get; set; }
        [DataMember]
        public string 経費項目2 { get; set; }
        [DataMember]
        public string 経費項目3 { get; set; }
        [DataMember]
        public string 経費項目4 { get; set; }
        [DataMember]
        public string 経費項目5 { get; set; }
        [DataMember]
        public string 経費項目6 { get; set; }
        [DataMember]
        public string 経費項目7 { get; set; }
    }


    /// <summary>
    /// JMI02010  CSV　メンバー
    /// </summary>
    [DataContract]
    public class JMI02010_Member_CSV
    {
        [DataMember]
        public DateTime 出庫 { get; set; }
        [DataMember]
        public DateTime 帰庫 { get; set; }
        [DataMember]
        public string 車番 { get; set; }
        [DataMember]
        public decimal? 社内金額 { get; set; }
        [DataMember]
        public int? 経費1 { get; set; }
        [DataMember]
        public int? 経費2 { get; set; }
        [DataMember]
        public int? 経費3 { get; set; }
        [DataMember]
        public int? 経費4 { get; set; }
        [DataMember]
        public int? 経費5 { get; set; }
        [DataMember]
        public int? 経費6 { get; set; }
        [DataMember]
        public int? 経費7 { get; set; }
        [DataMember]
        public int? その他経費 { get; set; }
        [DataMember]
        public decimal? 燃料Ｌ数 { get; set; }
        [DataMember]
        public decimal? 燃料代 { get; set; }
        [DataMember]
        public decimal? 走行KM { get; set; }
        [DataMember]
        public decimal? 歩合率 { get; set; }
        [DataMember]
        public decimal? 歩合金額 { get; set; }
        [DataMember]
        public int コード { get; set; }
        [DataMember]
        public string 乗務員名 { get; set; }
        [DataMember]
        public DateTime 期間From { get; set; }
        [DataMember]
        public DateTime 期間To { get; set; }
    }


    [DataContract]
    public class JMI02010_M07
    {
        [DataMember]
        public int 経費ID { get; set; }
        [DataMember]
        public string 経費名 { get; set; }
    }


    public class JMI02010
    {
        #region 印刷
        /// <summary>
        /// JMI02010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<JMI02010_Member> GetDataList(string p乗務員From, string p乗務員To, int?[] i乗務員List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                DataTable tbl = new DataTable();
                List<JMI02010_Member> retList = new List<JMI02010_Member>();

                ////テストリスト型LINQ組込テスト
                //List<JMI> retList2 = new List<JMI>();
                //retList2.Add(new JMI() { 日付 = DateTime.Now, day=1 });


                context.Connection.Open();

                ////テストリスト型LINQ組込テスト
                //var querymtest = (from m07 in context.M07_KEI
                //                from rr2 in retList2
                //                select new JMI02010_M07
                //                {
                //                    経費ID = m07.経費項目ID,
                //                    経費名 = m07.経費項目名,
                //                }).ToList();


                //全件表示
                var querym07 = (from m07 in context.M07_KEI
                                select new JMI02010_M07
                                {
                                    経費ID = m07.経費項目ID,
                                    経費名 = m07.経費項目名,
                                }).ToList();

                var querywhere = querym07.Where(x => x.経費ID == 601).ToList();
                string KEI1 = querywhere[0].経費名.ToString();
                querywhere = querym07.Where(x => x.経費ID == 602).ToList();
                string KEI2 = querywhere[0].経費名.ToString();
                querywhere = querym07.Where(x => x.経費ID == 603).ToList();
                string KEI3 = querywhere[0].経費名.ToString();
                querywhere = querym07.Where(x => x.経費ID == 604).ToList();
                string KEI4 = querywhere[0].経費名.ToString();
                querywhere = querym07.Where(x => x.経費ID == 605).ToList();
                string KEI5 = querywhere[0].経費名.ToString();
                querywhere = querym07.Where(x => x.経費ID == 606).ToList();
                string KEI6 = querywhere[0].経費名.ToString();
                querywhere = querym07.Where(x => x.経費ID == 607).ToList();
                string KEI7 = querywhere[0].経費名.ToString();



                var query = (from t02 in context.T02_UTRN.Where(x => x.勤務開始日 >= d集計期間From && x.勤務開始日 <= d集計期間To)
                                 join m04 in context.M04_DRV on t02.乗務員KEY equals m04.乗務員KEY
                                 join t01 in context.T01_TRN.Where(t01 => t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1)) on t02.明細番号 equals t01.明細番号  into t01Group
								 join t03 in context.T03_KTRN on t02.明細番号 equals t03.明細番号 into t03Group
                                 select new JMI02010_Member
                                 {
                                     出庫 = t02.勤務開始日,
                                     帰庫 = t02.勤務終了日,
                                     車番 = t02.車輌番号,
                                     社内金額 = t01Group.Where(order => order.支払先KEY == null).Sum(order => order.支払金額) == null ? 0 : t01Group.Where(order => order.支払先KEY == null).Sum(order => order.支払金額),
									 経費1 = t03Group.Where(order2 => order2.経費項目ID == 601 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 601 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
									 経費2 = t03Group.Where(order2 => order2.経費項目ID == 602 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 602 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
									 経費3 = t03Group.Where(order2 => order2.経費項目ID == 603 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 603 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
									 経費4 = t03Group.Where(order2 => order2.経費項目ID == 604 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 604 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
									 経費5 = t03Group.Where(order2 => order2.経費項目ID == 605 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 605 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
									 経費6 = t03Group.Where(order2 => order2.経費項目ID == 606 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 606 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
									 経費7 = t03Group.Where(order2 => order2.経費項目ID == 607 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 607 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
                                     その他経費 = t03Group.Where(order2 => order2.経費項目ID != 601 && order2.経費項目ID != 602 &&
                                                                           order2.経費項目ID != 603 && order2.経費項目ID != 604 &&
                                                                           order2.経費項目ID != 605 && order2.経費項目ID != 606 &&
                                                                           order2.経費項目ID != 607 &&
																		   order2.経費項目ID != 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額)
                                                                            == null ? 0 : t03Group.Where(order2 => order2.経費項目ID != 601 && order2.経費項目ID != 602 &&
                                                                                           order2.経費項目ID != 603 && order2.経費項目ID != 604 &&
                                                                                           order2.経費項目ID != 605 && order2.経費項目ID != 606 &&
                                                                                           order2.経費項目ID != 607 &&
																						   order2.経費項目ID != 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),

									 燃料Ｌ数 = t03Group.Where(order2 => order2.経費項目ID == 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.数量) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.数量),
									 燃料代 = t03Group.Where(order2 => order2.経費項目ID == 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
                                 走行KM = t02.走行ＫＭ == null ? 0 : t02.走行ＫＭ,
                                 歩合率 = m04.歩合率 == null ? 0 : m04.歩合率,
                                 歩合金額 = 0,

                                 コード = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 期間From = d集計期間From,
                                 期間To = d集計期間To,

                                 経費項目1 = KEI1,
                                 経費項目2 = KEI2,
                                 経費項目3 = KEI3,
                                 経費項目4 = KEI4,
                                 経費項目5 = KEI5,
                                 経費項目6 = KEI6,
                                 経費項目7 = KEI7,
                                 

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


                        query = query.Union(from t02 in context.T02_UTRN.Where(x => x.勤務開始日 >= d集計期間From && x.勤務開始日 <= d集計期間To)
                                 join m04 in context.M04_DRV on t02.乗務員KEY equals m04.乗務員KEY
                                 join t01 in context.T01_TRN.Where( t01 => t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1)) on t02.明細番号 equals t01.明細番号  into t01Group
								 join t03 in context.T03_KTRN on t02.明細番号 equals t03.明細番号 into t03Group
                                 where intCause.Contains(m04.乗務員ID)
                                 select new JMI02010_Member
                                 {
                                     出庫 = t02.勤務開始日,
                                     帰庫 = t02.勤務終了日,
                                     車番 = t02.車輌番号,
                                     社内金額 = t01Group.Where(order => order.支払先KEY == null).Sum(order => order.支払金額) == null ? 0 : t01Group.Where(order => order.支払先KEY == null).Sum(order => order.支払金額),
									 経費1 = t03Group.Where(order2 => order2.経費項目ID == 601 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 601 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
									 経費2 = t03Group.Where(order2 => order2.経費項目ID == 602 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 602 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
									 経費3 = t03Group.Where(order2 => order2.経費項目ID == 603 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 603 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
									 経費4 = t03Group.Where(order2 => order2.経費項目ID == 604 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 604 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
									 経費5 = t03Group.Where(order2 => order2.経費項目ID == 605 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 605 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
									 経費6 = t03Group.Where(order2 => order2.経費項目ID == 606 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 606 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
									 経費7 = t03Group.Where(order2 => order2.経費項目ID == 607 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 607 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
                                     その他経費 = t03Group.Where(order2 => order2.経費項目ID != 601 && order2.経費項目ID != 602 &&
                                                                           order2.経費項目ID != 603 && order2.経費項目ID != 604 &&
                                                                           order2.経費項目ID != 605 && order2.経費項目ID != 606 &&
                                                                           order2.経費項目ID != 607 &&
																		   order2.経費項目ID != 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額)
                                                                            == null ? 0 : t03Group.Where(order2 => order2.経費項目ID != 601 && order2.経費項目ID != 602 &&
                                                                                           order2.経費項目ID != 603 && order2.経費項目ID != 604 &&
                                                                                           order2.経費項目ID != 605 && order2.経費項目ID != 606 &&
                                                                                           order2.経費項目ID != 607 &&
																						   order2.経費項目ID != 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),

									 燃料Ｌ数 = t03Group.Where(order2 => order2.経費項目ID == 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.数量) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.数量),
									 燃料代 = t03Group.Where(order2 => order2.経費項目ID == 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
                                 走行KM = t02.走行ＫＭ == null ? 0 : t02.走行ＫＭ,
                                 歩合率 = m04.歩合率 == null ? 0 : m04.歩合率,
                                 歩合金額 = 0,

                                 コード = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 期間From = d集計期間From,
                                 期間To = d集計期間To,

                                 経費項目1 = KEI1,
                                 経費項目2 = KEI2,
                                 経費項目3 = KEI3,
                                 経費項目4 = KEI4,
                                 経費項目5 = KEI5,
                                 経費項目6 = KEI6,
                                 経費項目7 = KEI7,
                                 

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
        /// JMI02010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<JMI02010_Member_CSV> GetDataList_CSV(string p乗務員From, string p乗務員To, int?[] i乗務員List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, int p作成年度)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                DataTable tbl = new DataTable();
                List<JMI02010_Member_CSV> retList = new List<JMI02010_Member_CSV>();

                ////テストリスト型LINQ組込テスト
                //List<JMI> retList2 = new List<JMI>();
                //retList2.Add(new JMI() { 日付 = DateTime.Now, day=1 });


                context.Connection.Open();

                ////テストリスト型LINQ組込テスト
                //var querymtest = (from m07 in context.M07_KEI
                //                from rr2 in retList2
                //                select new JMI02010_M07
                //                {
                //                    経費ID = m07.経費項目ID,
                //                    経費名 = m07.経費項目名,
                //                }).ToList();


                //全件表示
                var querym07 = (from m07 in context.M07_KEI
                                select new JMI02010_M07
                                {
                                    経費ID = m07.経費項目ID,
                                    経費名 = m07.経費項目名,
                                }).ToList();

                var querywhere = querym07.Where(x => x.経費ID == 601).ToList();
                string KEI1 = querywhere[0].経費名.ToString();
                querywhere = querym07.Where(x => x.経費ID == 602).ToList();
                string KEI2 = querywhere[0].経費名.ToString();
                querywhere = querym07.Where(x => x.経費ID == 603).ToList();
                string KEI3 = querywhere[0].経費名.ToString();
                querywhere = querym07.Where(x => x.経費ID == 604).ToList();
                string KEI4 = querywhere[0].経費名.ToString();
                querywhere = querym07.Where(x => x.経費ID == 605).ToList();
                string KEI5 = querywhere[0].経費名.ToString();
                querywhere = querym07.Where(x => x.経費ID == 606).ToList();
                string KEI6 = querywhere[0].経費名.ToString();
                querywhere = querym07.Where(x => x.経費ID == 607).ToList();
                string KEI7 = querywhere[0].経費名.ToString();



                var query = (from t02 in context.T02_UTRN.Where(x => x.勤務開始日 >= d集計期間From && x.勤務開始日 <= d集計期間To)
                             join m04 in context.M04_DRV on t02.乗務員KEY equals m04.乗務員KEY
                             join t01 in context.T01_TRN.Where(t01 => t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1)) on t02.明細番号 equals t01.明細番号 into t01Group
							 join t03 in context.T03_KTRN on t02.明細番号 equals t03.明細番号 into t03Group
                             select new JMI02010_Member_CSV
                             {
                                 出庫 = t02.勤務開始日,
                                 帰庫 = t02.勤務終了日,
                                 車番 = t02.車輌番号,
                                 社内金額 = t01Group.Where(order => order.支払先KEY == null).Sum(order => order.支払金額) == null ? 0 : t01Group.Where(order => order.支払先KEY == null).Sum(order => order.支払金額),
								 経費1 = t03Group.Where(order2 => order2.経費項目ID == 601 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 601 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
								 経費2 = t03Group.Where(order2 => order2.経費項目ID == 602 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 602 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
								 経費3 = t03Group.Where(order2 => order2.経費項目ID == 603 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 603 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
								 経費4 = t03Group.Where(order2 => order2.経費項目ID == 604 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 604 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
								 経費5 = t03Group.Where(order2 => order2.経費項目ID == 605 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 605 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
								 経費6 = t03Group.Where(order2 => order2.経費項目ID == 606 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 606 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
								 経費7 = t03Group.Where(order2 => order2.経費項目ID == 607 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 607 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
                                 その他経費 = t03Group.Where(order2 => order2.経費項目ID != 601 && order2.経費項目ID != 602 &&
                                                                       order2.経費項目ID != 603 && order2.経費項目ID != 604 &&
                                                                       order2.経費項目ID != 605 && order2.経費項目ID != 606 &&
                                                                       order2.経費項目ID != 607 &&
																	   order2.経費項目ID != 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額)
                                                                        == null ? 0 : t03Group.Where(order2 => order2.経費項目ID != 601 && order2.経費項目ID != 602 &&
                                                                                       order2.経費項目ID != 603 && order2.経費項目ID != 604 &&
                                                                                       order2.経費項目ID != 605 && order2.経費項目ID != 606 &&
                                                                                       order2.経費項目ID != 607 &&
																					   order2.経費項目ID != 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),

								 燃料Ｌ数 = t03Group.Where(order2 => order2.経費項目ID == 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.数量) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.数量),
								 燃料代 = t03Group.Where(order2 => order2.経費項目ID == 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
                                 走行KM = t02.走行ＫＭ == null ? 0 : t02.走行ＫＭ,
                                 歩合率 = m04.歩合率 == null ? 0 : m04.歩合率,
                                 歩合金額 = 0,

                                 コード = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 期間From = d集計期間From,
                                 期間To = d集計期間To,

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


                        query = query.Union(from t02 in context.T02_UTRN.Where(x => x.勤務開始日 >= d集計期間From && x.勤務開始日 <= d集計期間To)
                                            join m04 in context.M04_DRV on t02.乗務員KEY equals m04.乗務員KEY
                                            join t01 in context.T01_TRN.Where(t01 => t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1)) on t02.明細番号 equals t01.明細番号 into t01Group
											join t03 in context.T03_KTRN on t02.明細番号 equals t03.明細番号 into t03Group
                                            where intCause.Contains(m04.乗務員ID)
                                            select new JMI02010_Member_CSV
                                            {
                                                出庫 = t02.勤務開始日,
                                                帰庫 = t02.勤務終了日,
                                                車番 = t02.車輌番号,
                                                社内金額 = t01Group.Where(order => order.支払先KEY == null).Sum(order => order.支払金額) == null ? 0 : t01Group.Where(order => order.支払先KEY == null).Sum(order => order.支払金額),
												経費1 = t03Group.Where(order2 => order2.経費項目ID == 601 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 601 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
												経費2 = t03Group.Where(order2 => order2.経費項目ID == 602 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 602 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
												経費3 = t03Group.Where(order2 => order2.経費項目ID == 603 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 603 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
												経費4 = t03Group.Where(order2 => order2.経費項目ID == 604 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 604 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
												経費5 = t03Group.Where(order2 => order2.経費項目ID == 605 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 605 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
												経費6 = t03Group.Where(order2 => order2.経費項目ID == 606 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 606 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
												経費7 = t03Group.Where(order2 => order2.経費項目ID == 607 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 607 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
                                                その他経費 = t03Group.Where(order2 => order2.経費項目ID != 601 && order2.経費項目ID != 602 &&
                                                                                      order2.経費項目ID != 603 && order2.経費項目ID != 604 &&
                                                                                      order2.経費項目ID != 605 && order2.経費項目ID != 606 &&
                                                                                      order2.経費項目ID != 607 &&
																					  order2.経費項目ID != 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額)
                                                                                       == null ? 0 : t03Group.Where(order2 => order2.経費項目ID != 601 && order2.経費項目ID != 602 &&
                                                                                                      order2.経費項目ID != 603 && order2.経費項目ID != 604 &&
                                                                                                      order2.経費項目ID != 605 && order2.経費項目ID != 606 &&
                                                                                                      order2.経費項目ID != 607 &&
																									  order2.経費項目ID != 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),

												燃料Ｌ数 = t03Group.Where(order2 => order2.経費項目ID == 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.数量) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.数量),
												燃料代 = t03Group.Where(order2 => order2.経費項目ID == 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額) == null ? 0 : t03Group.Where(order2 => order2.経費項目ID == 401 && order2.乗務員KEY == t02.乗務員KEY).Sum(order2 => order2.金額),
                                                走行KM = t02.走行ＫＭ == null ? 0 : t02.走行ＫＭ,
                                                歩合率 = m04.歩合率 == null ? 0 : m04.歩合率,
                                                歩合金額 = 0,

                                                コード = m04.乗務員ID,
                                                乗務員名 = m04.乗務員名,
                                                期間From = d集計期間From,
                                                期間To = d集計期間To,

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
				retList = (retList.OrderBy(a => a.コード).ThenBy(a => a.出庫)).ToList();
				//retList = (from q in retList orderby q.コード, q.出庫 select q);
				return retList;
            }

        }

        #endregion
    }
}