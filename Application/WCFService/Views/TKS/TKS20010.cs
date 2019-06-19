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

    public class TKS20010
    {

        /// <summary>
        /// TKS20010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class TKS20010_Member_day
        {
            public DateTime 日付 { get; set; }
            public string 曜日 { get; set; }
        }

        /// <summary>
        /// SHR07010 印刷　メンバー
        /// </summary>
        [DataContract]
        public class TKS20010_Member
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


        public class TKS20010_Member1
        {
            [DataMember]
            public int 得意先ID { get; set; }
            [DataMember]
            public string 得意先名 { get; set; }
            [DataMember]
            public int 部門ID { get; set; }
            [DataMember]
            public string 部門名 { get; set; }
            [DataMember]
            public decimal 総売上 { get; set; }
            [DataMember]
            public decimal 自社売上 { get; set; }
            [DataMember]
            public decimal 傭車売上 { get; set; }
            [DataMember]
            public DateTime 開始日付 { get; set; }
            [DataMember]
            public DateTime 終了日付 { get; set; }
            [DataMember]
            public DateTime 年月 { get; set; }
            [DataMember]
            public string コードFrom { get; set; }
            [DataMember]
            public string コードTo { get; set; }
            [DataMember]
            public string ピックアップ指定 { get; set; }
        }


        public class TKS20010_Let
        {
            [DataMember]
            public int 得意先ID { get; set; }
            [DataMember]
            public int 部門ID { get; set; }
            [DataMember]
            public decimal 合計 { get; set; }
        }


        #region TKS20010

        public List<TKS20010_Member1> TKS20010_GetData(string p得意先From, string p得意先To, int?[] p得意先List, string s得意先List, string p作成締日, string p作成年, string p作成月, DateTime p集計期間From, DateTime p集計期間To)
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

                    context.Connection.Open();

                    //try
                    //{

                    #region 集計
                    var query = (from m01 in context.M01_TOK.Where(q => q.削除日付 == null)
                                 from m71 in context.M71_BUM.Where(q => q.削除日付 == null)
                                 join t01 in context.T01_TRN.Where(q => (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))) on new { a = m01.得意先KEY, b = m71.自社部門ID } equals new { a = (int)(t01.得意先KEY == null ? 0 : t01.得意先KEY), b = t01.自社部門ID } into t01Group

                                 //from t01g in t01Group.DefaultIfEmpty()
                                 orderby m01.得意先ID, m71.自社部門ID
                                 select new TKS20010_Member1
                                 {
                                     得意先ID = m01.得意先ID,
                                     得意先名 = m01.略称名,
                                     部門ID = m71.自社部門ID,
                                     部門名 = m71.自社部門名,
                                     総売上 = t01Group.Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
                                     自社売上 = t01Group.Where(q => q.支払先KEY == 0 || q.支払先KEY == null).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Where(q => q.支払先KEY == 0 || q.支払先KEY == null).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
                                     傭車売上 = t01Group.Where(q => q.支払先KEY != 0).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Where(q => q.支払先KEY != 0).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),

                                     コードFrom = p得意先From,
                                     コードTo = p得意先To,
                                     ピックアップ指定 = s得意先List,
                                     開始日付 = p集計期間From,
                                     終了日付 = p集計期間To,
                                     年月 = d作成年月,


                                 }).AsQueryable();

                    query = query.Union(from m01 in context.M01_TOK.Where(q => q.削除日付 == null)
										join t01 in context.T01_TRN.Where(q => (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))) on m01.得意先KEY equals t01.得意先KEY into t01Group
                                        select new TKS20010_Member1
                                        {
                                            得意先ID = m01.得意先ID,
                                            得意先名 = m01.略称名,
                                            部門ID = 999999,
                                            部門名 = "＜ 合 計 ＞",
                                            総売上 = t01Group.Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
                                            自社売上 = t01Group.Where(q => q.支払先KEY == 0 || q.支払先KEY == null).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Where(q => q.支払先KEY == 0 || q.支払先KEY == null).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
                                            傭車売上 = t01Group.Where(q => q.支払先KEY != 0).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Where(q => q.支払先KEY != 0).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),

                                            コードFrom = p得意先From,
                                            コードTo = p得意先To,
                                            ピックアップ指定 = s得意先List,
                                            開始日付 = p集計期間From,
                                            終了日付 = p集計期間To,
                                            年月 = d作成年月,
                                        }).AsQueryable();

                    query = query.Union(from m01 in context.M01_TOK.Where(q => q.削除日付 == null)
										join t01 in context.T01_TRN.Where(q => ((q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To) && q.自社部門ID == 0) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))) on m01.得意先KEY equals t01.得意先KEY into t01Group
                                        select new TKS20010_Member1
                                        {
                                            得意先ID = m01.得意先ID,
                                            得意先名 = m01.略称名,
                                            部門ID = 0,
                                            部門名 = "－部門無し－",
                                            総売上 = t01Group.Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
                                            自社売上 = t01Group.Where(q => q.支払先KEY == 0 || q.支払先KEY == null).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Where(q => q.支払先KEY == 0 || q.支払先KEY == null).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
                                            傭車売上 = t01Group.Where(q => q.支払先KEY != 0).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Where(q => q.支払先KEY != 0).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),

                                            コードFrom = p得意先From,
                                            コードTo = p得意先To,
                                            ピックアップ指定 = s得意先List,
                                            開始日付 = p集計期間From,
                                            終了日付 = p集計期間To,
                                            年月 = d作成年月,
                                        }).AsQueryable();

                    query = query.Union(from m71 in context.M71_BUM.Where(q => q.削除日付 == null)
										join t01 in context.T01_TRN.Where(q => (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))) on m71.自社部門ID equals t01.自社部門ID into t01Group
                                        select new TKS20010_Member1
                                        {
                                            得意先ID = 999999999,
                                            得意先名 = "【 合 計 】",
                                            部門ID = m71.自社部門ID,
                                            部門名 = m71.自社部門名,
                                            総売上 = t01Group.Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
                                            自社売上 = t01Group.Where(q => q.支払先KEY == 0 || q.支払先KEY == null).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Where(q => q.支払先KEY == 0 || q.支払先KEY == null).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
                                            傭車売上 = t01Group.Where(q => q.支払先KEY != 0).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Where(q => q.支払先KEY != 0).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),

                                            コードFrom = p得意先From,
                                            コードTo = p得意先To,
                                            ピックアップ指定 = s得意先List,
                                            開始日付 = p集計期間From,
                                            終了日付 = p集計期間To,
                                            年月 = d作成年月,
                                        }).AsQueryable();

                    # region 部門なし合計追加
                    var query_a = (from t01 in context.M87_CNTL
                                   select new TKS20010_Member1()
                                   {
                                       得意先ID = 999999999,
                                       得意先名 = "【 合 計 】",
                                       部門ID = 0,
                                       部門名 = "－部門無し－",
									   総売上 = context.T01_TRN.Where(q => ((q.自社部門ID == 0) && (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To)) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : context.T01_TRN.Where(q => ((q.自社部門ID == 0) && (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To)) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
									   自社売上 = context.T01_TRN.Where(q => ((q.自社部門ID == 0) && ((q.支払先KEY == 0 || q.支払先KEY == null) && (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To))) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : context.T01_TRN.Where(q => ((q.自社部門ID == 0) && ((q.支払先KEY == 0 || q.支払先KEY == null) && (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To))) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
									   傭車売上 = context.T01_TRN.Where(q => ((q.自社部門ID == 0) && ((q.支払先KEY != 0) && (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To))) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : context.T01_TRN.Where(q => ((q.自社部門ID == 0) && ((q.支払先KEY != 0) && (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To))) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),

                                       コードFrom = p得意先From,
                                       コードTo = p得意先To,
                                       ピックアップ指定 = s得意先List,
                                       開始日付 = p集計期間From,
                                       終了日付 = p集計期間To,
                                       年月 = d作成年月,
                                   }).AsQueryable();

                    query_a = query_a.Take(1);

                    query = query.Union(query_a);

                    #endregion


                    #region 得意先部門合計追加
                    query_a = (from t01 in context.M87_CNTL
                               select new TKS20010_Member1()
                               {
                                   得意先ID = 999999999,
                                   得意先名 = "【 合 計 】",
                                   部門ID = 999999,
                                   部門名 = "＜ 合 計 ＞",
								   総売上 = context.T01_TRN.Where(q => (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : context.T01_TRN.Where(q => (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
								   自社売上 = context.T01_TRN.Where(q => ((q.支払先KEY == 0 || q.支払先KEY == null) && (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To)) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : context.T01_TRN.Where(q => ((q.支払先KEY == 0 || q.支払先KEY == null) && (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To)) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
								   傭車売上 = context.T01_TRN.Where(q => ((q.支払先KEY != 0) && (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To)) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : context.T01_TRN.Where(q => ((q.支払先KEY != 0) && (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To)) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),

                                   コードFrom = p得意先From,
                                   コードTo = p得意先To,
                                   ピックアップ指定 = s得意先List,
                                   開始日付 = p集計期間From,
                                   終了日付 = p集計期間To,
                                   年月 = d作成年月,
                               }).AsQueryable();

                    query_a = query_a.Take(1);

                    query = query.Union(query_a);
                    #endregion

                    var query2 = query.ToList();

                    query = (from q in query2
                             where q.総売上 != null && q.総売上 != 0
                             select new TKS20010_Member1
                             {
                                 得意先ID = q.得意先ID,
                                 得意先名 = q.得意先名,
                                 部門ID = q.部門ID,
                                 部門名 = q.部門名,
                                 総売上 = q.総売上,
                                 自社売上 = q.自社売上,
                                 傭車売上 = q.傭車売上,

                                 コードFrom = p得意先From,
                                 コードTo = p得意先To,
                                 ピックアップ指定 = s得意先List,
                                 開始日付 = p集計期間From,
                                 終了日付 = p集計期間To,
                                 年月 = d作成年月,

                             }).AsQueryable();

                    //var bumlet = (from q in query2
                    //              group q by new {q.部門ID} into qGroup
                    //              select new TKS20010_Let
                    //              {
                    //                  部門ID = qGroup.Key.部門ID,
                    //                  合計 = qGroup.DefaultIfEmpty().Sum(q => q.総売上),
                    //                  得意先ID = 0,
                    //              }).ToList();

                    //var a = toklet.Where(aa => aa.合計 != 0).Select(aa => aa.得意先ID);
                    //var b = bumlet.Where(bb => bb.合計 != 0).Select(bb => bb.部門ID);

                    //query = (from qq in query2
                    //         join q in query on new {a = qq.得意先ID, b = qq.部門ID } equals new { a = q.得意先ID, b = q.部門ID } into qGroup
                    //         select new TKS20010_Member1
                    //         {
                    //             得意先ID = qGroup.得意先ID,
                    //             得意先名 = q.得意先名,
                    //             部門ID = q.部門ID,
                    //             部門名 = q.部門名,
                    //             総売上 = q.総売上,
                    //             自社売上 = q.自社売上,
                    //             傭車売上 = q.傭車売上,
                    //         }).AsQueryable();



                    var query3 = query;

                    //得意先From処理　Min値
                    if (!string.IsNullOrEmpty(p得意先From))
                    {
                        int i得意先From = AppCommon.IntParse(p得意先From);
                        query = query.Where(c => c.得意先ID >= i得意先From || c.得意先ID == 999999999);
                    }

                    //得意先To処理　Max値
                    if (!string.IsNullOrEmpty(p得意先To))
                    {
                        int i得意先TO = AppCommon.IntParse(p得意先To);
                        query = query.Where(c => c.得意先ID <= i得意先TO || c.得意先ID == 999999999);
                    }

                    if (p得意先List.Length > 0)
                    {
                        if ((string.IsNullOrEmpty(p得意先From)) && (string.IsNullOrEmpty(p得意先To)))
                        {
                            query = query3.Where(q => p得意先List.Contains(q.得意先ID) || q.得意先ID == 999999999);
                        }
                        else
                        {
                            query = query.Union(query3.Where(q => p得意先List.Contains(q.得意先ID) || q.得意先ID == 999999999));
                        }
                    }
                    query = query.Distinct();


                    query3 = query.OrderBy(q => q.得意先ID).ThenBy(q => q.部門ID);

                    //var query3 = query.OrderBy(q => q.得意先ID).ThenBy(q => q.日付);

                    var retList = query3.ToList();



                    #endregion

                    //TKS20010_DATASET dset = new TKS20010_DATASET()
                    //{
                    //    売上構成グラフ = query1,
                    //    得意先上位グラフ = query2,
                    //    傭車先上位グラフ = query3,
                    //    損益分岐点グラフ = query4,
                    //};







                    return retList;

                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }
        }
        #endregion


        #region TKS20010

        public List<TKS20010_Member1> TKS20010_GetData_CSV(string p得意先From, string p得意先To, int?[] p得意先List, string s得意先List, string p作成締日, string p作成年, string p作成月, DateTime p集計期間From, DateTime p集計期間To)
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

                    context.Connection.Open();

                    //try
                    //{

                    #region 集計

                    var query = (from m01 in context.M01_TOK.Where(q => q.削除日付 == null)
                                 from m71 in context.M71_BUM.Where(q => q.削除日付 == null)
								 join t01 in context.T01_TRN.Where(q => (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))) on new { a = m01.得意先KEY, b = m71.自社部門ID } equals new { a = (int)(t01.得意先KEY == null ? 0 : t01.得意先KEY), b = t01.自社部門ID } into t01Group

                                 //from t01g in t01Group.DefaultIfEmpty()
                                 orderby m01.得意先ID, m71.自社部門ID
                                 select new TKS20010_Member1
                                 {
                                     得意先ID = m01.得意先ID,
                                     得意先名 = m01.略称名,
                                     部門ID = m71.自社部門ID,
                                     部門名 = m71.自社部門名,
                                     総売上 = t01Group.Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
                                     自社売上 = t01Group.Where(q => q.支払先KEY == 0 || q.支払先KEY == null).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Where(q => q.支払先KEY == 0 || q.支払先KEY == null).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
                                     傭車売上 = t01Group.Where(q => q.支払先KEY != 0).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Where(q => q.支払先KEY != 0).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),

                                     コードFrom = p得意先From,
                                     コードTo = p得意先To,
                                     ピックアップ指定 = s得意先List,
                                     開始日付 = p集計期間From,
                                     終了日付 = p集計期間To,
                                     年月 = d作成年月,


                                 }).AsQueryable();

                    query = query.Union(from m01 in context.M01_TOK.Where(q => q.削除日付 == null)
										join t01 in context.T01_TRN.Where(q => (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))) on m01.得意先KEY equals t01.得意先KEY into t01Group
                                        select new TKS20010_Member1
                                        {
                                            得意先ID = m01.得意先ID,
                                            得意先名 = m01.略称名,
                                            部門ID = 999999,
                                            部門名 = "＜ 合 計 ＞",
                                            総売上 = t01Group.Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
                                            自社売上 = t01Group.Where(q => q.支払先KEY == 0 || q.支払先KEY == null).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Where(q => q.支払先KEY == 0 || q.支払先KEY == null).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
                                            傭車売上 = t01Group.Where(q => q.支払先KEY != 0).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Where(q => q.支払先KEY != 0).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),

                                            コードFrom = p得意先From,
                                            コードTo = p得意先To,
                                            ピックアップ指定 = s得意先List,
                                            開始日付 = p集計期間From,
                                            終了日付 = p集計期間To,
                                            年月 = d作成年月,
                                        }).AsQueryable();

                    query = query.Union(from m01 in context.M01_TOK.Where(q => q.削除日付 == null)
										join t01 in context.T01_TRN.Where(q => ((q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))) && q.自社部門ID == 0) on m01.得意先KEY equals t01.得意先KEY into t01Group
                                        select new TKS20010_Member1
                                        {
                                            得意先ID = m01.得意先ID,
                                            得意先名 = m01.略称名,
                                            部門ID = 0,
                                            部門名 = "－部門無し－",
                                            総売上 = t01Group.Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
                                            自社売上 = t01Group.Where(q => q.支払先KEY == 0 || q.支払先KEY == null).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Where(q => q.支払先KEY == 0 || q.支払先KEY == null).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
                                            傭車売上 = t01Group.Where(q => q.支払先KEY != 0).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Where(q => q.支払先KEY != 0).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),

                                            コードFrom = p得意先From,
                                            コードTo = p得意先To,
                                            ピックアップ指定 = s得意先List,
                                            開始日付 = p集計期間From,
                                            終了日付 = p集計期間To,
                                            年月 = d作成年月,
                                        }).AsQueryable();

                    query = query.Union(from m71 in context.M71_BUM.Where(q => q.削除日付 == null)
										join t01 in context.T01_TRN.Where(q => (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))) on m71.自社部門ID equals t01.自社部門ID into t01Group
                                        select new TKS20010_Member1
                                        {
                                            得意先ID = 999999999,
                                            得意先名 = "【 合 計 】",
                                            部門ID = m71.自社部門ID,
                                            部門名 = m71.自社部門名,
                                            総売上 = t01Group.Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
                                            自社売上 = t01Group.Where(q => q.支払先KEY == 0 || q.支払先KEY == null).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Where(q => q.支払先KEY == 0 || q.支払先KEY == null).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
                                            傭車売上 = t01Group.Where(q => q.支払先KEY != 0).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : t01Group.Where(q => q.支払先KEY != 0).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),

                                            コードFrom = p得意先From,
                                            コードTo = p得意先To,
                                            ピックアップ指定 = s得意先List,
                                            開始日付 = p集計期間From,
                                            終了日付 = p集計期間To,
                                            年月 = d作成年月,
                                        }).AsQueryable();

                    # region 部門なし合計追加
                    var query_a = (from t01 in context.M87_CNTL
                                   select new TKS20010_Member1()
                                   {
                                       得意先ID = 999999999,
                                       得意先名 = "【 合 計 】",
                                       部門ID = 0,
                                       部門名 = "－部門無し－",
									   総売上 = context.T01_TRN.Where(q => ((q.自社部門ID == 0) && (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To)) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : context.T01_TRN.Where(q => ((q.自社部門ID == 0) && (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To)) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
									   自社売上 = context.T01_TRN.Where(q => ((q.自社部門ID == 0) && ((q.支払先KEY == 0 || q.支払先KEY == null) && (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To))) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : context.T01_TRN.Where(q => ((q.自社部門ID == 0) && ((q.支払先KEY == 0 || q.支払先KEY == null) && (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To))) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
									   傭車売上 = context.T01_TRN.Where(q => ((q.自社部門ID == 0) && ((q.支払先KEY != 0) && (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To))) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : context.T01_TRN.Where(q => ((q.自社部門ID == 0) && ((q.支払先KEY != 0) && (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To))) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),

                                       コードFrom = p得意先From,
                                       コードTo = p得意先To,
                                       ピックアップ指定 = s得意先List,
                                       開始日付 = p集計期間From,
                                       終了日付 = p集計期間To,
                                       年月 = d作成年月,
                                   }).AsQueryable();

                    query_a = query_a.Take(1);

                    query = query.Union(query_a);

                    #endregion


                    #region 得意先部門合計追加
                    query_a = (from t01 in context.M87_CNTL
                               select new TKS20010_Member1()
                               {
                                   得意先ID = 999999999,
                                   得意先名 = "【 合 計 】",
                                   部門ID = 999999,
                                   部門名 = "＜ 合 計 ＞",
								   総売上 = context.T01_TRN.Where(q => (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : context.T01_TRN.Where(q => (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
								   自社売上 = context.T01_TRN.Where(q => ((q.支払先KEY == 0 || q.支払先KEY == null) && (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To)) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : context.T01_TRN.Where(q => ((q.支払先KEY == 0 || q.支払先KEY == null) && (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To)) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),
								   傭車売上 = context.T01_TRN.Where(q => ((q.支払先KEY != 0) && (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To)) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２) == null ? 0 : context.T01_TRN.Where(q => ((q.支払先KEY != 0) && (q.請求日付 >= p集計期間From && q.請求日付 <= p集計期間To)) && (((q.入力区分 != 3) || (q.入力区分 == 3 && q.明細行 == 1)) && (q.社内区分 == 0))).Sum(q => q.売上金額 + q.請求割増１ + q.請求割増２),

                                   コードFrom = p得意先From,
                                   コードTo = p得意先To,
                                   ピックアップ指定 = s得意先List,
                                   開始日付 = p集計期間From,
                                   終了日付 = p集計期間To,
                                   年月 = d作成年月,
                               }).AsQueryable();

                    query_a = query_a.Take(1);

                    query = query.Union(query_a);
                    #endregion

                    var query2 = query.ToList();

                    query = (from q in query2
                             where q.総売上 != null && q.総売上 != 0
                             select new TKS20010_Member1
                             {
                                 得意先ID = q.得意先ID,
                                 得意先名 = q.得意先名,
                                 部門ID = q.部門ID,
                                 部門名 = q.部門名,
                                 総売上 = q.総売上,
                                 自社売上 = q.自社売上,
                                 傭車売上 = q.傭車売上,

                                 コードFrom = p得意先From,
                                 コードTo = p得意先To,
                                 ピックアップ指定 = s得意先List,
                                 開始日付 = p集計期間From,
                                 終了日付 = p集計期間To,
                                 年月 = d作成年月,

                             }).AsQueryable();

                    //var bumlet = (from q in query2
                    //              group q by new {q.部門ID} into qGroup
                    //              select new TKS20010_Let
                    //              {
                    //                  部門ID = qGroup.Key.部門ID,
                    //                  合計 = qGroup.DefaultIfEmpty().Sum(q => q.総売上),
                    //                  得意先ID = 0,
                    //              }).ToList();

                    //var a = toklet.Where(aa => aa.合計 != 0).Select(aa => aa.得意先ID);
                    //var b = bumlet.Where(bb => bb.合計 != 0).Select(bb => bb.部門ID);

                    //query = (from qq in query2
                    //         join q in query on new {a = qq.得意先ID, b = qq.部門ID } equals new { a = q.得意先ID, b = q.部門ID } into qGroup
                    //         select new TKS20010_Member1
                    //         {
                    //             得意先ID = qGroup.得意先ID,
                    //             得意先名 = q.得意先名,
                    //             部門ID = q.部門ID,
                    //             部門名 = q.部門名,
                    //             総売上 = q.総売上,
                    //             自社売上 = q.自社売上,
                    //             傭車売上 = q.傭車売上,
                    //         }).AsQueryable();



                    var query3 = query;

                    //得意先From処理　Min値
                    if (!string.IsNullOrEmpty(p得意先From))
                    {
                        int i得意先From = AppCommon.IntParse(p得意先From);
                        query = query.Where(c => c.得意先ID >= i得意先From || c.得意先ID == 999999999);
                    }

                    //得意先To処理　Max値
                    if (!string.IsNullOrEmpty(p得意先To))
                    {
                        int i得意先TO = AppCommon.IntParse(p得意先To);
                        query = query.Where(c => c.得意先ID <= i得意先TO || c.得意先ID == 999999999);
                    }

                    if (p得意先List.Length > 0)
                    {
                        if ((string.IsNullOrEmpty(p得意先From)) && (string.IsNullOrEmpty(p得意先To)))
                        {
                            query = query3.Where(q => p得意先List.Contains(q.得意先ID) || q.得意先ID == 999999999);
                        }
                        else
                        {
                            query = query.Union(query3.Where(q => p得意先List.Contains(q.得意先ID) || q.得意先ID == 999999999));
                        }
                    }
                    query = query.Distinct();


                    query3 = query.OrderBy(q => q.得意先ID).ThenBy(q => q.部門ID);

                    //var query3 = query.OrderBy(q => q.得意先ID).ThenBy(q => q.日付);

                    var retList = query3.ToList();



                    #endregion

                    //TKS20010_DATASET dset = new TKS20010_DATASET()
                    //{
                    //    売上構成グラフ = query1,
                    //    得意先上位グラフ = query2,
                    //    傭車先上位グラフ = query3,
                    //    損益分岐点グラフ = query4,
                    //};







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