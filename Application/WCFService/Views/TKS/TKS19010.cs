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

    public class TKS19010
    {

        /// <summary>
        /// TKS19010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class TKS19010_Member_day
        {
            public DateTime 日付 { get; set; }
            public string 曜日 { get; set; }
        }

        /// <summary>
        /// SHR07010 印刷　メンバー
        /// </summary>
        [DataContract]
        public class TKS19010_Member
        {
            [DataMember]
            public int? 得意先コード { get; set; }
            [DataMember]
            public string 得意先指定 { get; set; }
            [DataMember]
            public string 得意先指定コード { get; set; }
            [DataMember]
            public string 得意先名 { get; set; }
            [DataMember]
            public string 締日 { get; set; }
            [DataMember]
            public int? 締日明細 { get; set; }
            [DataMember]
            public string 集計年月 { get; set; }
            [DataMember]
            public decimal? 当月売上額 { get; set; }
            [DataMember]
            public decimal 当月売上合計額 { get; set; }
            [DataMember]
            public decimal? 当月構成 { get; set; }
            [DataMember]
            public decimal 累計売上額 { get; set; }
            [DataMember]
            public decimal? 累計構成 { get; set; }
            [DataMember]
            public decimal 前月売上額 { get; set; }
            [DataMember]
            public decimal 前年同月 { get; set; }
            [DataMember]
            public Decimal? 前年構成 { get; set; }
            [DataMember]
            public decimal 前々年同月 { get; set; }
            [DataMember]
            public decimal? 前々年構成 { get; set; }
            [DataMember]
            public DateTime 累計開始月 { get; set; }
            [DataMember]
            public DateTime 累計終了月 { get; set; }
            [DataMember]
            public string 親子区分 { get; set; }
            [DataMember]
            public string 作成区分 { get; set; }
            [DataMember]
            public string 表示順序1 { get; set; }
            [DataMember]
            public string 表示順序2 { get; set; }
            [DataMember]
            public string 表示順序3 { get; set; }
            [DataMember]
            public string 累計集計日 { get; set; }

        }


        public class TKS19010_Member1
        {
            [DataMember]
            public int 車輌ID { get; set; }
            [DataMember]
            public string 車輌名 { get; set; }
            [DataMember]
            public DateTime? 日付 { get; set; }
            [DataMember]
            public decimal 日計 { get; set; }
            [DataMember]
            public decimal 累計 { get; set; }
            [DataMember]
            public decimal 予算残 { get; set; }
            [DataMember]
            public decimal? 達成率 { get; set; }
            [DataMember]
            public decimal 予算 { get; set; }
            //[DataMember]
            //public DateTime 開始日付 { get; set; }
            //[DataMember]
            //public DateTime 終了日付 { get; set; }
            //[DataMember]
            //public DateTime 年月 { get; set; }
            //[DataMember]
            //public string コードFrom { get; set; }
            //[DataMember]
            //public string コードTo { get; set; }
            //[DataMember]
            //public string ピックアップ指定 { get; set; }
        }

        public class TKS19010_Member2
        {
            [DataMember]
            public DateTime? 日付 { get; set; }
            [DataMember]
            public int 車輌ID1 { get; set; }
            [DataMember]
            public string 車輌名1 { get; set; }
            [DataMember]
            public decimal 日計1 { get; set; }
            [DataMember]
            public decimal 累計1 { get; set; }
            [DataMember]
            public decimal 予算残1 { get; set; }
            [DataMember]
            public decimal? 達成率1 { get; set; }
            [DataMember]
            public decimal 予算1 { get; set; }
            [DataMember]
            public int 車輌ID2 { get; set; }
            [DataMember]
            public string 車輌名2 { get; set; }
            [DataMember]
            public decimal 日計2 { get; set; }
            [DataMember]
            public decimal 累計2 { get; set; }
            [DataMember]
            public decimal 予算残2 { get; set; }
            [DataMember]
            public decimal? 達成率2 { get; set; }
            [DataMember]
            public decimal 予算2 { get; set; }
            [DataMember]
            public int 車輌ID3 { get; set; }
            [DataMember]
            public string 車輌名3 { get; set; }
            [DataMember]
            public decimal 日計3 { get; set; }
            [DataMember]
            public decimal 累計3 { get; set; }
            [DataMember]
            public decimal 予算残3 { get; set; }
            [DataMember]
            public decimal? 達成率3 { get; set; }
            [DataMember]
            public decimal 予算3 { get; set; }
            [DataMember]
            public DateTime 開始年月日 { get; set; }
            [DataMember]
            public DateTime 終了年月日 { get; set; }
            [DataMember]
            public DateTime 年月 { get; set; }
            [DataMember]
            public string コードFrom { get; set; }
            [DataMember]
            public string コードTo { get; set; }
            [DataMember]
            public string ピックアップ指定 { get; set; }
        }

        public class TKS19010_Let
        {
            [DataMember]
            public int 車輌ID { get; set; }
            [DataMember]
            public decimal 合計 { get; set; }
        }


        #region TKS19010

        public List<TKS19010_Member2> TKS19010_GetData(string p車輌From, string p車輌To, int?[] p車輌List, string s車輌List, string p作成締日, string p作成年, string p作成月, DateTime p集計期間From, DateTime p集計期間To)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try
                {
                    DateTime d作成年月;
                    DateTime Wk;
                    d作成年月 = DateTime.TryParse(p作成年 + "/" + p作成月 + "/" + 01, out Wk) ? Wk : DateTime.Today;

                    int p作成年月;

                    if (p作成月.Length == 1)
                    {
                        p作成年月 = AppCommon.IntParse(p作成年 + "0" + p作成月);
                    }
                    else
                    {
                        p作成年月 = AppCommon.IntParse(p作成年 + p作成月);
                    }

                    //日付格納LIST
                    List<TKS19010_Member_day> retList_day = new List<TKS19010_Member_day>();

                    DateTime d日付 = DateTime.TryParse(p集計期間From.ToString(), out Wk) ? Wk :DateTime.Today;
                    //日付、曜日計算処理
                    for (int i = 0; d日付 <= p集計期間To; d日付 = d日付.AddDays(1))
                    {
                        retList_day.Add(new TKS19010_Member_day()
                            {
                                日付 = d日付,
                                曜日 = ("日月火水木金土").Substring(AppCommon.IntParse(d日付.DayOfWeek.ToString("d")), 1) + "曜",
                            });
                    }

                    //                List<TKS19010_Member1> retList = new List<TKS19010_Member1>();

                    context.Connection.Open();

                    //try
                    //{

                    #region 集計


                    var query = (from rday in retList_day
                                 from m05 in context.M05_CAR.Where(q => q.削除日付 == null)
                                 join m17 in context.M17_CYSN.Where(q => q.年月 == p作成年月) on m05.車輌KEY equals m17.車輌KEY into m17Group
								 join t01 in context.T01_TRN.Where(t01 => ((t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 == 1))) on new { a = rday.日付, b = m05.車輌KEY } equals new { a = t01.請求日付, b = (int)(t01.車輌KEY == null ? 0 : t01.車輌KEY) } into t01Group
                                 //from t01g in t01Group.DefaultIfEmpty()
                                 orderby m05.車輌ID, rday.日付
                                 select new TKS19010_Member1
                                 {
                                     日付 = rday.日付,
                                     車輌ID = m05.車輌ID,
                                     車輌名 = m05.車輌番号,
                                     日計 = (t01Group.Sum(q => q.売上金額) == null ? 0 : t01Group.Sum(q => q.売上金額))
                                        + (t01Group.Sum(q => q.請求割増１) == null ? 0 : t01Group.Sum(q => q.請求割増１))
                                        + (t01Group.Sum(q => q.請求割増２) == null ? 0 : t01Group.Sum(q => q.請求割増２)),
                                     予算 = m17Group.Select(q => q.売上予算).FirstOrDefault(),

                                     //コードFrom = p車輌From,
                                     //コードTo = p車輌To,
                                     //ピックアップ指定 = s車輌List,
                                     //開始日付 = p集計期間From,
                                     //終了日付 = p集計期間To,
                                     //年月 = d作成年月,
                                 }).AsQueryable();


                    var carlet = (from q in query
                                  group q by q.車輌ID into qGroup
                                  select new TKS19010_Let
                                  {
                                      車輌ID = qGroup.Key,
                                      合計 = qGroup.Sum(q => q.日計),
                                  }).AsQueryable();

                    query = (from q in query
                             let bb = from b in carlet where b.合計 != 0 select b.車輌ID
                             where bb.Contains(q.車輌ID)
                             select new TKS19010_Member1
                             {
                                 日付 = q.日付,
                                 車輌ID = q.車輌ID,
                                 車輌名 = q.車輌名,
                                 日計 = q.日計,
                                 予算 = q.予算,
                             }).AsQueryable();


                    var query2 = (from rday in retList_day
								  join t01 in context.T01_TRN.Where(t01 => ((t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 == 1))) on new { a = rday.日付 } equals new { a = t01.請求日付 } into t01Group
                                  //from t01g in t01Group.DefaultIfEmpty()
                                  orderby rday.日付
                                  select new TKS19010_Member1
                                  {
                                      日付 = rday.日付,
                                      車輌ID = 0,
                                      車輌名 = "全車輌",
                                      日計 = (t01Group.Sum(q => q.売上金額) == null ? 0 : t01Group.Sum(q => q.売上金額))
                                         + (t01Group.Sum(q => q.請求割増１) == null ? 0 : t01Group.Sum(q => q.請求割増１))
                                         + (t01Group.Sum(q => q.請求割増２) == null ? 0 : t01Group.Sum(q => q.請求割増２)),
                                      予算 = context.M17_CYSN.Where(q => q.年月 == p作成年月).Any() == false ? 0 : context.M17_CYSN.Where(q => q.年月 == p作成年月).Sum(q => q.売上予算),
                                      //コードFrom = p車輌From,
                                      //コードTo = p車輌To,
                                      //ピックアップ指定 = s車輌List,
                                      //開始日付 = p集計期間From,
                                      //終了日付 = p集計期間To,
                                      //年月 = d作成年月,

                                  }).AsQueryable();

                    var query3 = query;

                    //車輌From処理　Min値
                    if (!string.IsNullOrEmpty(p車輌From))
                    {
                        int i車輌From = AppCommon.IntParse(p車輌From);
                        query = query.Where(c => c.車輌ID >= i車輌From);
                    }

                    //車輌To処理　Max値
                    if (!string.IsNullOrEmpty(p車輌To))
                    {
                        int i車輌TO = AppCommon.IntParse(p車輌To);
                        query = query.Where(c => c.車輌ID <= i車輌TO);
                    }

                    if (p車輌List.Length > 0)
                    {
                        if ((string.IsNullOrEmpty(p車輌From)) && (string.IsNullOrEmpty(p車輌To)))
                        {
                            query = query3.Where(q => p車輌List.Contains(q.車輌ID));
                        }
                        else
                        {
                            query = query.Union(query3.Where(q => p車輌List.Contains(q.車輌ID)));
                        }
                    }
                    query = query.Distinct();


                    query3 = query.Union(query2).OrderBy(q => q.車輌ID).ThenBy(q => q.日付);

                    //var query3 = query.OrderBy(q => q.車輌ID).ThenBy(q => q.日付);

                    var query4 = query3.ToList();

                    int m17code = 0;
                    decimal Ruikei = 0;
                    foreach (var Lst in query4)
                    {
                        if (m17code != Lst.車輌ID)
                        {
                            Ruikei = 0;
                            m17code = Lst.車輌ID;
                        }
                        Ruikei += Lst.日計;
                        Lst.累計 = Ruikei;
                        Lst.予算残 = Lst.予算 - Ruikei;
                        Lst.達成率 = Lst.予算 == 0 ? 0 : Math.Round((Ruikei / Lst.予算 * 100), 1, MidpointRounding.AwayFromZero);
                    };




                    #endregion

                    //TKS19010_DATASET dset = new TKS19010_DATASET()
                    //{
                    //    売上構成グラフ = query1,
                    //    得意先上位グラフ = query2,
                    //    傭車先上位グラフ = query3,
                    //    損益分岐点グラフ = query4,
                    //};


                    var bumid = (from que in query4
                                 group que by new { que.車輌ID } into queGroup
                                 select new TKS19010_Member1()
                                 {
                                     車輌ID = queGroup.Key.車輌ID,
                                 }).AsQueryable();

                    List<TKS19010_Member2> retList = new List<TKS19010_Member2>();
                    int cnt = 0;
                    int? bumid1 = null, bumid2 = null, bumid3 = null, bumid4 = null;
                    foreach (var id in bumid)
                    {
                        switch (cnt)
                        {
                            case 0:
                                bumid1 = id.車輌ID;
                                break;
                            case 1:
                                bumid2 = id.車輌ID;
                                break;
                            case 2:
                                bumid3 = id.車輌ID;
                                break;
                            //case 3:
                            //    bumid4 = id.車輌ID;
                            //    break;
                        }
                        cnt += 1;

                        if (cnt >= 3)
                        {
                            var q1 = query4.Where(q => q.車輌ID == bumid1);
                            var q2 = query4.Where(q => q.車輌ID == bumid2);
                            var q3 = query4.Where(q => q.車輌ID == bumid3);
                            var q4 = query4.Where(q => q.車輌ID == bumid4);

                            foreach (var row in q1)
                            {

                                TKS19010_Member2 list = new TKS19010_Member2()
                                {

                                    日付 = row.日付,
                                    車輌ID1 = row.車輌ID,
                                    車輌名1 = row.車輌名,
                                    日計1 = row.日計,
                                    累計1 = row.累計,
                                    予算残1 = row.予算残,
                                    達成率1 = row.達成率,
                                    予算1 = row.予算,

                                    車輌ID2 = q2.Where(q => q.日付 == row.日付).Select(q => q.車輌ID).FirstOrDefault(),
                                    車輌名2 = q2.Where(q => q.日付 == row.日付).Select(q => q.車輌名).FirstOrDefault(),
                                    日計2 = q2.Where(q => q.日付 == row.日付).Select(q => q.日計).FirstOrDefault(),
                                    累計2 = q2.Where(q => q.日付 == row.日付).Select(q => q.累計).FirstOrDefault(),
                                    予算残2 = q2.Where(q => q.日付 == row.日付).Select(q => q.予算残).FirstOrDefault(),
                                    達成率2 = q2.Where(q => q.日付 == row.日付).Select(q => q.達成率).FirstOrDefault(),
                                    予算2 = q2.Where(q => q.日付 == row.日付).Select(q => q.予算).FirstOrDefault(),

                                    車輌ID3 = q3.Where(q => q.日付 == row.日付).Select(q => q.車輌ID).FirstOrDefault(),
                                    車輌名3 = q3.Where(q => q.日付 == row.日付).Select(q => q.車輌名).FirstOrDefault(),
                                    日計3 = q3.Where(q => q.日付 == row.日付).Select(q => q.日計).FirstOrDefault(),
                                    累計3 = q3.Where(q => q.日付 == row.日付).Select(q => q.累計).FirstOrDefault(),
                                    予算残3 = q3.Where(q => q.日付 == row.日付).Select(q => q.予算残).FirstOrDefault(),
                                    達成率3 = q3.Where(q => q.日付 == row.日付).Select(q => q.達成率).FirstOrDefault(),
                                    予算3 = q3.Where(q => q.日付 == row.日付).Select(q => q.予算).FirstOrDefault(),

                                    コードFrom = p車輌From,
                                    コードTo = p車輌To,
                                    ピックアップ指定 = s車輌List,
                                    開始年月日 = p集計期間From,
                                    終了年月日 = p集計期間To,
                                    年月 = d作成年月,

                                };
                                retList.Add(list);

                            }



                            cnt = 0;
                            bumid1 = null; bumid2 = null; bumid3 = null; bumid4 = null;
                        }
                    }
                    //余り分があれば
                    if (cnt > 0)
                    {
                        var q1 = query4.Where(q => q.車輌ID == bumid1);
                        var q2 = query4.Where(q => q.車輌ID == bumid2);
                        var q3 = query4.Where(q => q.車輌ID == bumid3);
                        var q4 = query4.Where(q => q.車輌ID == bumid4);

                        foreach (var row in q1)
                        {

                            TKS19010_Member2 list = new TKS19010_Member2()
                            {

                                日付 = row.日付,
                                車輌ID1 = row.車輌ID,
                                車輌名1 = row.車輌名,
                                日計1 = row.日計,
                                累計1 = row.累計,
                                予算残1 = row.予算残,
                                達成率1 = row.達成率,
                                予算1 = row.予算,

                                車輌ID2 = q2.Where(q => q.日付 == row.日付).Select(q => q.車輌ID).FirstOrDefault(),
                                車輌名2 = q2.Where(q => q.日付 == row.日付).Select(q => q.車輌名).FirstOrDefault(),
                                日計2 = q2.Where(q => q.日付 == row.日付).Select(q => q.日計).FirstOrDefault(),
                                累計2 = q2.Where(q => q.日付 == row.日付).Select(q => q.累計).FirstOrDefault(),
                                予算残2 = q2.Where(q => q.日付 == row.日付).Select(q => q.予算残).FirstOrDefault(),
                                達成率2 = q2.Where(q => q.日付 == row.日付).Select(q => q.達成率).FirstOrDefault(),
                                予算2 = q2.Where(q => q.日付 == row.日付).Select(q => q.予算).FirstOrDefault(),

                                車輌ID3 = q3.Where(q => q.日付 == row.日付).Select(q => q.車輌ID).FirstOrDefault(),
                                車輌名3 = q3.Where(q => q.日付 == row.日付).Select(q => q.車輌名).FirstOrDefault(),
                                日計3 = q3.Where(q => q.日付 == row.日付).Select(q => q.日計).FirstOrDefault(),
                                累計3 = q3.Where(q => q.日付 == row.日付).Select(q => q.累計).FirstOrDefault(),
                                予算残3 = q3.Where(q => q.日付 == row.日付).Select(q => q.予算残).FirstOrDefault(),
                                達成率3 = q3.Where(q => q.日付 == row.日付).Select(q => q.達成率).FirstOrDefault(),
                                予算3 = q3.Where(q => q.日付 == row.日付).Select(q => q.予算).FirstOrDefault(),

                                コードFrom = p車輌From,
                                コードTo = p車輌To,
                                ピックアップ指定 = s車輌List,
                                開始年月日 = p集計期間From,
                                終了年月日 = p集計期間To,
                                年月 = d作成年月,
                            };
                            retList.Add(list);
                        }
                    }





                    return retList;

                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }
        }
        #endregion



        #region TKS19010 CSV

        public List<TKS19010_Member1> TKS19010_GetData_CSV(string p車輌From, string p車輌To, int?[] p車輌List, string s車輌List, string p作成締日, string p作成年, string p作成月, DateTime p集計期間From, DateTime p集計期間To)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try
                {
                    DateTime d作成年月;
                    DateTime Wk;
                    d作成年月 = DateTime.TryParse(p作成年 + "/" + p作成月 + "/" + 01 , out Wk) ? Wk : DateTime.Today;

                    int p作成年月;

                    if (p作成月.Length == 1)
                    {
                        p作成年月 = AppCommon.IntParse(p作成年 + "0" + p作成月);
                    }
                    else
                    {
                        p作成年月 = AppCommon.IntParse(p作成年 + p作成月);
                    }

                    //日付格納LIST
                    List<TKS19010_Member_day> retList_day = new List<TKS19010_Member_day>();

                    DateTime d日付 = DateTime.TryParse(p集計期間From.ToString(),out Wk) ? Wk : DateTime.Today;
                    //日付、曜日計算処理
                    for (int i = 0; d日付 <= p集計期間To; d日付 = d日付.AddDays(1))
                    {
                        retList_day.Add(new TKS19010_Member_day()
                        {
                            日付 = d日付,
                            曜日 = ("日月火水木金土").Substring(AppCommon.IntParse(d日付.DayOfWeek.ToString("d")), 1) + "曜",
                        });
                    }

                    //                List<TKS19010_Member1> retList = new List<TKS19010_Member1>();

                    context.Connection.Open();

                    //try
                    //{

                    #region 集計


                    var query = (from rday in retList_day
                                 from m05 in context.M05_CAR.Where(q => q.削除日付 == null)
                                 join m17 in context.M17_CYSN.Where(q => q.年月 == p作成年月) on m05.車輌KEY equals m17.車輌KEY into m17Group
								 join t01 in context.T01_TRN.Where(t01 => ((t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 == 1))) on new { a = rday.日付, b = m05.車輌KEY } equals new { a = t01.請求日付, b = (int)(t01.車輌KEY == null ? 0 : t01.車輌KEY) } into t01Group
                                 //from t01g in t01Group.DefaultIfEmpty()
                                 orderby m05.車輌ID, rday.日付
                                 select new TKS19010_Member1
                                 {
                                     日付 = rday.日付,
                                     車輌ID = m05.車輌ID,
                                     車輌名 = m05.車輌番号,
                                     日計 = (t01Group.Sum(q => q.売上金額) == null ? 0 : t01Group.Sum(q => q.売上金額))
                                        + (t01Group.Sum(q => q.請求割増１) == null ? 0 : t01Group.Sum(q => q.請求割増１))
                                        + (t01Group.Sum(q => q.請求割増２) == null ? 0 : t01Group.Sum(q => q.請求割増２)),
                                     予算 = m17Group.Select(q => q.売上予算).FirstOrDefault(),

                                     //コードFrom = p車輌From,
                                     //コードTo = p車輌To,
                                     //ピックアップ指定 = s車輌List,
                                     //開始日付 = p集計期間From,
                                     //終了日付 = p集計期間To,
                                     //年月 = d作成年月,
                                 }).AsQueryable();


                    var carlet = (from q in query
                                  group q by q.車輌ID into qGroup
                                  select new TKS19010_Let
                                  {
                                      車輌ID = qGroup.Key,
                                      合計 = qGroup.Sum(q => q.日計),
                                  }).AsQueryable();

                    query = (from q in query
                             let bb = from b in carlet where b.合計 != 0 select b.車輌ID
                             where bb.Contains(q.車輌ID)
                             select new TKS19010_Member1
                             {
                                 日付 = q.日付,
                                 車輌ID = q.車輌ID,
                                 車輌名 = q.車輌名,
                                 日計 = q.日計,
                                 予算 = q.予算,
                             }).AsQueryable();


                    var query2 = (from rday in retList_day
								  join t01 in context.T01_TRN.Where(t01 => ((t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 == 1))) on new { a = rday.日付 } equals new { a = t01.請求日付 } into t01Group
                                  //from t01g in t01Group.DefaultIfEmpty()
                                  orderby rday.日付
                                  select new TKS19010_Member1
                                  {
                                      日付 = rday.日付,
                                      車輌ID = 0,
                                      車輌名 = "全車輌",
                                      日計 = (t01Group.Sum(q => q.売上金額) == null ? 0 : t01Group.Sum(q => q.売上金額))
                                         + (t01Group.Sum(q => q.請求割増１) == null ? 0 : t01Group.Sum(q => q.請求割増１))
                                         + (t01Group.Sum(q => q.請求割増２) == null ? 0 : t01Group.Sum(q => q.請求割増２)),
                                      予算 = context.M17_CYSN.Where(q => q.年月 == p作成年月).Any() == false ? 0 : context.M17_CYSN.Where(q => q.年月 == p作成年月).Sum(q => q.売上予算),
                                      //コードFrom = p車輌From,
                                      //コードTo = p車輌To,
                                      //ピックアップ指定 = s車輌List,
                                      //開始日付 = p集計期間From,
                                      //終了日付 = p集計期間To,
                                      //年月 = d作成年月,

                                  }).AsQueryable();

                    var query3 = query;

                    //車輌From処理　Min値
                    if (!string.IsNullOrEmpty(p車輌From))
                    {
                        int i車輌From = AppCommon.IntParse(p車輌From);
                        query = query.Where(c => c.車輌ID >= i車輌From);
                    }

                    //車輌To処理　Max値
                    if (!string.IsNullOrEmpty(p車輌To))
                    {
                        int i車輌TO = AppCommon.IntParse(p車輌To);
                        query = query.Where(c => c.車輌ID <= i車輌TO);
                    }

                    if (p車輌List.Length > 0)
                    {
                        if ((string.IsNullOrEmpty(p車輌From)) && (string.IsNullOrEmpty(p車輌To)))
                        {
                            query = query3.Where(q => p車輌List.Contains(q.車輌ID));
                        }
                        else
                        {
                            query = query.Union(query3.Where(q => p車輌List.Contains(q.車輌ID)));
                        }
                    }
                    query = query.Distinct();


                    query3 = query.Union(query2).OrderBy(q => q.車輌ID).ThenBy(q => q.日付);

                    //var query3 = query.OrderBy(q => q.車輌ID).ThenBy(q => q.日付);

                    var query4 = query3.ToList();

                    int m17code = 0;
                    decimal Ruikei = 0;
                    foreach (var Lst in query4)
                    {
                        if (m17code != Lst.車輌ID)
                        {
                            Ruikei = 0;
                            m17code = Lst.車輌ID;
                        }
                        Ruikei += Lst.日計;
                        Lst.累計 = Ruikei;
                        Lst.予算残 = Lst.予算 - Ruikei;
                        Lst.達成率 = Lst.予算 == 0 ? 0 : Math.Round((Ruikei / Lst.予算 * 100), 1, MidpointRounding.AwayFromZero);
                    };




                    #endregion

                    return query4;

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