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

    public class TKS15010
    {

        /// <summary>
        /// SHR07010 印刷　メンバー
        /// </summary>
        [DataContract]
        public class TKS15010_Member
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


        public class TKS15010_Member1
        {
            [DataMember]
            public string 項目名 { get; set; }
            [DataMember]
            public decimal? 売上金額 { get; set; }
        }

        public class TKS15010_Member2
        {
            [DataMember]
            public string 得意先名 { get; set; }
            [DataMember]
            public decimal? 売上金額 { get; set; }

        }

        public class TKS15010_Member3
        {
            [DataMember]
            public string 傭車先名 { get; set; }
            [DataMember]
            public decimal? 売上金額 { get; set; }

        }

        public class TKS15010_Member4
        {
            [DataMember]
            public string 項目名 { get; set; }
            [DataMember]
            public decimal? 売上金額 { get; set; }
            [DataMember]
            public decimal? 経費金額 { get; set; }

        }

        public class TKS15010_DATASET
        {
            public List<TKS15010_Member1> 売上構成グラフ = null;
            public List<TKS15010_Member2> 得意先上位グラフ = null;
            public List<TKS15010_Member3> 傭車先上位グラフ = null;
            public List<TKS15010_Member4> 損益分岐点グラフ = null;
        }


        #region TKS15010

        public TKS15010_DATASET TKS15010_GetData(int p作成年月)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                //支払先指定　表示用
                string 得意先指定表示 = string.Empty;
                //累計日付、年度の表示
                string 月度 = string.Empty;
                string 累計開始From = string.Empty;
                string 累計開始To = string.Empty;
                string 累計開始年 = string.Empty;
                string 累計開始月 = string.Empty;
                string 累計表示期間 = string.Empty;

                List<TKS15010_Member1> retList1 = new List<TKS15010_Member1>();
                context.Connection.Open();

                try
                {
                    var query1 = (from v01 in context.V_得意先締日.Where(c => c.集計年月 == p作成年月)
                                  group v01 by v01.集計年月 into g
                                  select new TKS15010_Member1
                                  {
                                      項目名 = "自社売上",
                                      売上金額 = g.Sum(c => c.締日売上金額) - g.Sum(c => c.締日内傭車売上),
                                  }).ToList();

                    query1 = query1.Union(from v01 in context.V_得意先締日.Where(c => c.集計年月 == p作成年月)
                                          group v01 by v01.集計年月 into g
                                          select new TKS15010_Member1
                                          {
                                              項目名 = "傭車売上",
                                              売上金額 = g.Sum(c => c.締日内傭車売上),
                                          }).ToList();

                    //    return query.ToList();



                    var query2 = (from v01 in context.V_得意先締日
                                  join m01 in context.M01_TOK on v01.得意先KEY equals m01.得意先KEY into m01Group
                                  from m01g in m01Group.DefaultIfEmpty()
                                  orderby v01.締日売上金額 descending
                                  where v01.集計年月 == p作成年月
                                  select new TKS15010_Member2
                                  {
                                      得意先名 = m01g.略称名,
                                      売上金額 = v01.締日売上金額,
                                  }).ToList();

                    var query3 = (from v01 in context.V_支払締日
                                  join m01 in context.M01_TOK on v01.支払先KEY equals m01.得意先KEY into m01Group
                                  from m01g in m01Group.DefaultIfEmpty()
                                  orderby v01.締日売上金額 descending
                                  where v01.集計年月 == p作成年月
                                  select new TKS15010_Member3
                                  {
                                      傭車先名 = m01g.略称名,
                                      売上金額 = v01.締日売上金額,
                                  }).ToList();

                    #region 損益分岐点計算
                    decimal uriage = (decimal)(from v01 in context.V_得意先締日
                                               where v01.集計年月 == p作成年月
                                               select v01.締日売上金額).Sum();

                    decimal koteihi = (decimal)(from s14 in context.S14_CARSB
                                                join m07 in context.M07_KEI on s14.経費項目ID equals m07.経費項目ID
                                                where s14.集計年月 == p作成年月 && s14.固定変動区分 == 0 && m07.編集区分 == 0
                                                select s14.金額).Sum();

                    decimal hendouhi = (decimal)(from s14 in context.S14_CARSB
                                                join m07 in context.M07_KEI on s14.経費項目ID equals m07.経費項目ID
                                                where s14.集計年月 == p作成年月 && s14.固定変動区分 == 1 && m07.編集区分 == 0
                                                select s14.金額).Sum();

                    uriage   = 100000000;
                    koteihi  =  30000000;
                    hendouhi =  50000000;

                    decimal genkairieki = uriage - koteihi;
                    decimal keijyourieki = uriage - koteihi - koteihi;
                    decimal sonekibunkiten;
                    try
                    {
                        sonekibunkiten = koteihi / ((uriage - hendouhi) / uriage);
                    }
                    catch
                    {
                        sonekibunkiten = 0;
                    }

                    decimal FMhiritu;
                    try
                    {
                        FMhiritu = koteihi / genkairieki;
                    }
                    catch
                    {
                        FMhiritu = 0;
                    }

                    List<TKS15010_Member4> query4 = new List<TKS15010_Member4>();
                    query4.Add(new TKS15010_Member4 { 項目名 = "売上金額", 売上金額 = 0, 経費金額 = 0 });
                    query4.Add(new TKS15010_Member4 { 項目名 = "売上金額", 売上金額 = sonekibunkiten, 経費金額 = sonekibunkiten });
                    query4.Add(new TKS15010_Member4 { 項目名 = "固定費", 売上金額 = 0, 経費金額 = koteihi });
                    query4.Add(new TKS15010_Member4 { 項目名 = "固定費", 売上金額 = sonekibunkiten, 経費金額 = koteihi });
                    query4.Add(new TKS15010_Member4 { 項目名 = "総費用", 売上金額 = 0, 経費金額 = koteihi });
                    query4.Add(new TKS15010_Member4 { 項目名 = "総費用", 売上金額 = sonekibunkiten, 経費金額 = sonekibunkiten });

                    //query4 new TKS15010_Member4 { 項目名 = "売上金額", 売上金額 = 0, 経費金額 = 0, };
                    //query4 = new TKS15010_Member4 { 項目名 = "売上金額", 売上金額 = 0, 経費金額 = 0, };


                    #endregion

                    TKS15010_DATASET dset = new TKS15010_DATASET()
                    {
                        売上構成グラフ = query1,
                        得意先上位グラフ = query2,
                        傭車先上位グラフ = query3,
                        損益分岐点グラフ = query4,
                    };

                    return dset;

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