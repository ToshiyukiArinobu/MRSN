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

    public class TKS17010
    {

        /// <summary>
        /// SHR07010 印刷　メンバー
        /// </summary>
        [DataContract]
        public class TKS17010_Member
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


        public class TKS17010_Member1
        {
            [DataMember]
            public string 項目 { get; set; }
            [DataMember]
            public string 月1 { get; set; }
            [DataMember]
            public string 月2 { get; set; }
            [DataMember]
            public string 月3 { get; set; }
            [DataMember]
            public string 月4 { get; set; }
            [DataMember]
            public string 月5 { get; set; }
            [DataMember]
            public string 月6 { get; set; }
            [DataMember]
            public string 月7 { get; set; }
            [DataMember]
            public string 月8 { get; set; }
            [DataMember]
            public string 月9 { get; set; }
            [DataMember]
            public string 月10 { get; set; }
            [DataMember]
            public string 月11 { get; set; }
            [DataMember]
            public string 月12 { get; set; }
            [DataMember]
            public decimal? 売上1 { get; set; }
            [DataMember]
            public decimal? 売上2 { get; set; }
            [DataMember]
            public decimal? 売上3 { get; set; }
            [DataMember]
            public decimal? 売上4 { get; set; }
            [DataMember]
            public decimal? 売上5 { get; set; }
            [DataMember]
            public decimal? 売上6 { get; set; }
            [DataMember]
            public decimal? 売上7 { get; set; }
            [DataMember]
            public decimal? 売上8 { get; set; }
            [DataMember]
            public decimal? 売上9 { get; set; }
            [DataMember]
            public decimal? 売上10 { get; set; }
            [DataMember]
            public decimal? 売上11 { get; set; }
            [DataMember]
            public decimal? 売上12 { get; set; }
        }




        #region TKS17010

        public List<TKS17010_Member1> TKS17010_GetData(DateTime p開始年月)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                //DateTime[] 年月 = new DateTime[12];
                //int[] i年月 = new int[12];

                //DateTime 開始年月 = DateTime.Parse( p開始年月.ToString() + "01");
                //DateTime 終了年月 = DateTime.Parse( p終了年月.ToString() + "01");

                //for (int i = 0; i < 12; i++)
                //{
                //    年月[i] = p開始年月.AddMonths(i);
                //}
                //for (int i = 0; i < 12; i++)
                //{
                //    if (年月[i].Month.ToString().Length == 1)
                //    {
                //        i年月[i] = AppCommon.IntParse(年月[i].Year.ToString() + "0" + 年月[i].Month.ToString());
                //    }
                //    else
                //    {
                //        i年月[i] = AppCommon.IntParse(年月[i].Year.ToString() + 年月[i].Month.ToString());
                //    }

                //}


                DateTime 年月1 = p開始年月;
                DateTime 年月2 = p開始年月.AddMonths(1);
                DateTime 年月3 = p開始年月.AddMonths(2);
                DateTime 年月4 = p開始年月.AddMonths(3);
                DateTime 年月5 = p開始年月.AddMonths(4);
                DateTime 年月6 = p開始年月.AddMonths(5);
                DateTime 年月7 = p開始年月.AddMonths(6);
                DateTime 年月8 = p開始年月.AddMonths(7);
                DateTime 年月9 = p開始年月.AddMonths(8);
                DateTime 年月10 = p開始年月.AddMonths(9);
                DateTime 年月11 = p開始年月.AddMonths(10);
                DateTime 年月12 = p開始年月.AddMonths(11);
                int i年月1;
                int i年月2;
                int i年月3;
                int i年月4;
                int i年月5;
                int i年月6;
                int i年月7;
                int i年月8;
                int i年月9;
                int i年月10;
                int i年月11;
                int i年月12;
                if (年月1.Month.ToString().Length == 1)
                {
                    i年月1 = AppCommon.IntParse(年月1.Year.ToString() + "0" + 年月1.Month.ToString());
                }
                else
                {
                    i年月1 = AppCommon.IntParse(年月1.Year.ToString() + 年月1.Month.ToString());
                }

                if (年月2.Month.ToString().Length == 1)
                {
                    i年月2 = AppCommon.IntParse(年月2.Year.ToString() + "0" + 年月2.Month.ToString());
                }
                else
                {
                    i年月2 = AppCommon.IntParse(年月2.Year.ToString() + 年月2.Month.ToString());
                }

                if (年月3.Month.ToString().Length == 1)
                {
                    i年月3 = AppCommon.IntParse(年月3.Year.ToString() + "0" + 年月3.Month.ToString());
                }
                else
                {
                    i年月3 = AppCommon.IntParse(年月3.Year.ToString() + 年月3.Month.ToString());
                }

                if (年月4.Month.ToString().Length == 1)
                {
                    i年月4 = AppCommon.IntParse(年月4.Year.ToString() + "0" + 年月4.Month.ToString());
                }
                else
                {
                    i年月4 = AppCommon.IntParse(年月4.Year.ToString() + 年月4.Month.ToString());
                }

                if (年月5.Month.ToString().Length == 1)
                {
                    i年月5 = AppCommon.IntParse(年月5.Year.ToString() + "0" + 年月5.Month.ToString());
                }
                else
                {
                    i年月5 = AppCommon.IntParse(年月5.Year.ToString() + 年月5.Month.ToString());
                }

                if (年月6.Month.ToString().Length == 1)
                {
                    i年月6 = AppCommon.IntParse(年月6.Year.ToString() + "0" + 年月6.Month.ToString());
                }
                else
                {
                    i年月6 = AppCommon.IntParse(年月6.Year.ToString() + 年月6.Month.ToString());
                }

                if (年月7.Month.ToString().Length == 1)
                {
                    i年月7 = AppCommon.IntParse(年月7.Year.ToString() + "0" + 年月7.Month.ToString());
                }
                else
                {
                    i年月7 = AppCommon.IntParse(年月7.Year.ToString() + 年月7.Month.ToString());
                }

                if (年月8.Month.ToString().Length == 1)
                {
                    i年月8 = AppCommon.IntParse(年月8.Year.ToString() + "0" + 年月8.Month.ToString());
                }
                else
                {
                    i年月8 = AppCommon.IntParse(年月8.Year.ToString() + 年月8.Month.ToString());
                }

                if (年月9.Month.ToString().Length == 1)
                {
                    i年月9 = AppCommon.IntParse(年月9.Year.ToString() + "0" + 年月9.Month.ToString());
                }
                else
                {
                    i年月9 = AppCommon.IntParse(年月9.Year.ToString() + 年月9.Month.ToString());
                }

                if (年月10.Month.ToString().Length == 1)
                {
                    i年月10 = AppCommon.IntParse(年月10.Year.ToString() + "0" + 年月10.Month.ToString());
                }
                else
                {
                    i年月10 = AppCommon.IntParse(年月10.Year.ToString() + 年月10.Month.ToString());
                }

                if (年月11.Month.ToString().Length == 1)
                {
                    i年月11 = AppCommon.IntParse(年月11.Year.ToString() + "0" + 年月11.Month.ToString());
                }
                else
                {
                    i年月11 = AppCommon.IntParse(年月11.Year.ToString() + 年月11.Month.ToString());
                }

                if (年月12.Month.ToString().Length == 1)
                {
                    i年月12 = AppCommon.IntParse(年月12.Year.ToString() + "0" + 年月12.Month.ToString());
                }
                else
                {
                    i年月12 = AppCommon.IntParse(年月12.Year.ToString() + 年月12.Month.ToString());
                }



                
                List<TKS17010_Member1> query = new List<TKS17010_Member1>();

                context.Connection.Open();

                try
                {

                    #region 集計
                    #region 売上集計
                    query.Add(new TKS17010_Member1
                    {
                        月1 = 年月1.Month.ToString() + "月",
                        月2 = 年月2.Month.ToString() + "月",
                        月3 = 年月3.Month.ToString() + "月",
                        月4 = 年月4.Month.ToString() + "月",
                        月5 = 年月5.Month.ToString() + "月",
                        月6 = 年月6.Month.ToString() + "月",
                        月7 = 年月7.Month.ToString() + "月",
                        月8 = 年月8.Month.ToString() + "月",
                        月9 = 年月9.Month.ToString() + "月",
                        月10 = 年月10.Month.ToString() + "月",
                        月11 = 年月11.Month.ToString() + "月",
                        月12 = 年月12.Month.ToString() + "月",
                        売上1 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月1 select vv01.月次売上金額).Sum(),
                        売上2 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月2 select vv01.月次売上金額).Sum(),
                        売上3 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月3 select vv01.月次売上金額).Sum(),
                        売上4 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月4 select vv01.月次売上金額).Sum(),
                        売上5 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月5 select vv01.月次売上金額).Sum(),
                        売上6 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月6 select vv01.月次売上金額).Sum(),
                        売上7 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月7 select vv01.月次売上金額).Sum(),
                        売上8 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月8 select vv01.月次売上金額).Sum(),
                        売上9 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月9 select vv01.月次売上金額).Sum(),
                        売上10 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月10 select vv01.月次売上金額).Sum(),
                        売上11 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月11 select vv01.月次売上金額).Sum(),
                        売上12 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月12 select vv01.月次売上金額).Sum(),
                        項目 = "売上",
                    });
                    #endregion

                    #region 売上集計
                    query.Add(new TKS17010_Member1
                    {
                        月1 = 年月1.Month.ToString() + "月",
                        月2 = 年月2.Month.ToString() + "月",
                        月3 = 年月3.Month.ToString() + "月",
                        月4 = 年月4.Month.ToString() + "月",
                        月5 = 年月5.Month.ToString() + "月",
                        月6 = 年月6.Month.ToString() + "月",
                        月7 = 年月7.Month.ToString() + "月",
                        月8 = 年月8.Month.ToString() + "月",
                        月9 = 年月9.Month.ToString() + "月",
                        月10 = 年月10.Month.ToString() + "月",
                        月11 = 年月11.Month.ToString() + "月",
                        月12 = 年月12.Month.ToString() + "月",



                        売上1 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月1 select vv01.月次売上金額).Sum()
                                - (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月1 select vv01.月次内傭車料 ).Sum()
                                - (decimal)(from s14 in context.S14_CARSB join m07 in context.M07_KEI on s14.経費項目ID equals m07.経費項目ID
                                            where (s14.集計年月 >= i年月1 && s14.固定変動区分 == 1 && m07.編集区分 == 0)
                                            select s14.金額).DefaultIfEmpty().Sum(),
                        売上2 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月2 select vv01.月次売上金額).Sum()
                                - (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月2 select vv01.月次内傭車料).Sum()
                                - (decimal)(from s14 in context.S14_CARSB
                                            join m07 in context.M07_KEI on s14.経費項目ID equals m07.経費項目ID
                                     where (s14.集計年月 >= i年月2 && s14.固定変動区分 == 1 && m07.編集区分 == 0)
                                            select s14.金額).DefaultIfEmpty().Sum(),

                        売上3 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月3 select vv01.月次売上金額).Sum()
                                - (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月3 select vv01.月次内傭車料).Sum()
                                - (decimal)(from s14 in context.S14_CARSB
                                            join m07 in context.M07_KEI on s14.経費項目ID equals m07.経費項目ID
                                            where (s14.集計年月 >= i年月3 && s14.固定変動区分 == 1 && m07.編集区分 == 0)
                                            select s14.金額).DefaultIfEmpty().Sum(),
                        売上4 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月4 select vv01.月次売上金額).Sum()
                                - (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月4 select vv01.月次内傭車料).Sum()
                                - (decimal)(from s14 in context.S14_CARSB
                                   join m07 in context.M07_KEI on s14.経費項目ID equals m07.経費項目ID
                                   where (s14.集計年月 >= i年月4 && s14.固定変動区分 == 1 && m07.編集区分 == 0)
                                            select s14.金額).DefaultIfEmpty().Sum(),
                        売上5 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月5 select vv01.月次売上金額).Sum()
                                - (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月5 select vv01.月次内傭車料).Sum()
                                - (decimal)(from s14 in context.S14_CARSB
                                            join m07 in context.M07_KEI on s14.経費項目ID equals m07.経費項目ID
                                            where (s14.集計年月 >= i年月5 && s14.固定変動区分 == 1 && m07.編集区分 == 0)
                                            select s14.金額).DefaultIfEmpty().Sum(),
                        売上6 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月6 select vv01.月次売上金額).Sum()
                                - (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月6 select vv01.月次内傭車料).Sum()
                                - (decimal)(from s14 in context.S14_CARSB
                                           join m07 in context.M07_KEI on s14.経費項目ID equals m07.経費項目ID
                                           where (s14.集計年月 >= i年月6 && s14.固定変動区分 == 1 && m07.編集区分 == 0)
                                            select s14.金額).DefaultIfEmpty().Sum(),
                        売上7 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月7 select vv01.月次売上金額).Sum()
                                - (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月7 select vv01.月次内傭車料).Sum()
                                - (decimal)(from s14 in context.S14_CARSB
                                           join m07 in context.M07_KEI on s14.経費項目ID equals m07.経費項目ID
                                           where (s14.集計年月 >= i年月7 && s14.固定変動区分 == 1 && m07.編集区分 == 0)
                                            select s14.金額).DefaultIfEmpty().Sum(),
                        売上8 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月8 select vv01.月次売上金額).Sum()
                                - (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月8 select vv01.月次内傭車料).Sum()
                                - (decimal)(from s14 in context.S14_CARSB
                                           join m07 in context.M07_KEI on s14.経費項目ID equals m07.経費項目ID
                                           where (s14.集計年月 >= i年月8 && s14.固定変動区分 == 1 && m07.編集区分 == 0)
                                            select s14.金額).DefaultIfEmpty().Sum(),
                        売上9 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月9 select vv01.月次売上金額).Sum()
                                - (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月9 select vv01.月次内傭車料).Sum()
                                - (decimal)(from s14 in context.S14_CARSB
                                           join m07 in context.M07_KEI on s14.経費項目ID equals m07.経費項目ID
                                           where (s14.集計年月 >= i年月9 && s14.固定変動区分 == 1 && m07.編集区分 == 0)
                                            select s14.金額).DefaultIfEmpty().Sum(),
                        売上10 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月10 select vv01.月次売上金額).Sum()
                                - (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月10 select vv01.月次内傭車料).Sum()
                                - (decimal)(from s14 in context.S14_CARSB
                                        join m07 in context.M07_KEI on s14.経費項目ID equals m07.経費項目ID
                                        where (s14.集計年月 >= i年月10 && s14.固定変動区分 == 1 && m07.編集区分 == 0)
                                            select s14.金額).DefaultIfEmpty().Sum(),
                        売上11 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月11 select vv01.月次売上金額).Sum()
                                - (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月11 select vv01.月次内傭車料).Sum()
                                - (decimal)(from s14 in context.S14_CARSB
                                           join m07 in context.M07_KEI on s14.経費項目ID equals m07.経費項目ID
                                           where (s14.集計年月 >= i年月11 && s14.固定変動区分 == 1 && m07.編集区分 == 0)
                                            select s14.金額).DefaultIfEmpty().Sum(),
                        売上12 = (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月12 select vv01.月次売上金額).Sum()
                                - (from vv01 in context.V_得意先月次 where vv01.集計年月 == i年月12 select vv01.月次内傭車料).Sum()
                                - (decimal)(from s14 in context.S14_CARSB
                                            join m07 in context.M07_KEI on s14.経費項目ID equals m07.経費項目ID
                                            where (s14.集計年月 >= i年月12 && s14.固定変動区分 == 1 && m07.編集区分 == 0)
                                            select s14.金額).DefaultIfEmpty().Sum(),
                        項目 = "粗利",
                    });
                    #endregion


                    #endregion

                    //TKS17010_DATASET dset = new TKS17010_DATASET()
                    //{
                    //    売上構成グラフ = query1,
                    //    得意先上位グラフ = query2,
                    //    傭車先上位グラフ = query3,
                    //    損益分岐点グラフ = query4,
                    //};


                    foreach (TKS17010_Member1 list in query)
                    {
                        if (list.売上1 == null) { list.売上1 = 0; }
                        if (list.売上2 == null) { list.売上2 = 0; }
                        if (list.売上3 == null) { list.売上3 = 0; }
                        if (list.売上4 == null) { list.売上4 = 0; }
                        if (list.売上5 == null) { list.売上5 = 0; }
                        if (list.売上6 == null) { list.売上6 = 0; }
                        if (list.売上7 == null) { list.売上7 = 0; }
                        if (list.売上8 == null) { list.売上8 = 0; }
                        if (list.売上9 == null) { list.売上9 = 0; }
                        if (list.売上10 == null) { list.売上10 = 0; }
                        if (list.売上11 == null) { list.売上11 = 0; }
                        if (list.売上12 == null) { list.売上12 = 0; }
                    }
                    
                    
                    
                    return query;

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