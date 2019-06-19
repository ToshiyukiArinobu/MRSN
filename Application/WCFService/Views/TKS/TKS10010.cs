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
using System.Data.Objects.SqlClient;

namespace KyoeiSystem.Application.WCFService
{

    /// <summary>
    /// TKS10010_tMember 入金滞留額
    /// </summary>
    [DataContract]
    public class TKS10010_tMember
    {
        [DataMember]
        public int 得意先コード { get; set; }
        [DataMember]
        public string 得意先名 { get; set; }
        [DataMember]
        public string 集計年月 { get; set; }
        [DataMember]
        public string 表示区分 { get; set; }
        [DataMember]
        public int 年月 { get; set; }
        [DataMember]
        public decimal? 入金金額 { get; set; }
        [DataMember]
        public decimal? 調整他 { get; set; }
        [DataMember]
        public decimal? 差引金額 { get; set; }
        [DataMember]
        public decimal? 売上金額 { get; set; }
        [DataMember]
        public decimal? 消費税 { get; set; }
        [DataMember]
        public decimal? 通行料 { get; set; }
        [DataMember]
        public decimal? 当月合計 { get; set; }
        [DataMember]
        public decimal? 繰越残高 { get; set; }
        [DataMember]
        public decimal 前月残高 { get; set; }
    }


    /// <summary>
    /// TKS10010_Members
    /// </summary>
    [DataContract]
    public class TKS10010_Member
    {
        [DataMember]
        public int コード { get; set; }
        [DataMember]
        public string 得意先名 { get; set; }
        [DataMember]
        public string 作成年月度 { get; set; }
        [DataMember]
        public string 親子区分 { get; set; }
        [DataMember]
        public string 集金区分 { get; set; }
        [DataMember]
        public string 集金日区分 { get; set; }
        [DataMember]
        public int 集金日 { get; set; }
        [DataMember]
        public int サイト { get; set; }
        [DataMember]
        public decimal? 入金滞留額 { get; set; }
        [DataMember]
        public decimal? 一ヶ月前残 { get; set; }
        [DataMember]
        public decimal? 二ヶ月前残 { get; set; }
        [DataMember]
        public decimal? 三ヶ月前残 { get; set; }
        [DataMember]
        public decimal? 四ヶ月前残 { get; set; }
        [DataMember]
        public decimal? 五ヶ月前残 { get; set; }
        [DataMember]
        public string 得意先指定 { get; set; }
        [DataMember]
        public string 得意先ﾋﾟｯｸｱｯﾌﾟ { get; set; }

    }

    [DataContract]
    public class TKS10010N_Member
    {
        [DataMember]
        public int 得意先ID { get; set; }
        [DataMember]
        public string 入出金年月 { get; set; }
        [DataMember]
        public int 入金金額 { get; set; }
    }

    public class TKS10010
    {

        public List<TKS10010_Member> TKS10010_GetDataHinList(string s得意先From, string s得意先To, int?[] i得意先List, string s作成集金日, bool b全集金日集計,
                                                                string s作成年, string s作成月, int i作成区分, string s作成年月度)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {

                string 取引先指定ﾋﾟｯｸｱｯﾌﾟ = string.Empty;


                //1～5ヶ月前の日付を計算
                string s集計年月;
                s集計年月 = s作成年 + "/" + s作成月 + "/01";

                DateTime dnowTime, doneTime, dtwoTime, dThreeTime, dfourTime, dfiveTime;
                dnowTime = Convert.ToDateTime(s集計年月);
                doneTime = Convert.ToDateTime(s集計年月).AddMonths(-1);
                dtwoTime = Convert.ToDateTime(s集計年月).AddMonths(-2);
                dThreeTime = Convert.ToDateTime(s集計年月).AddMonths(-3);
                dfourTime = Convert.ToDateTime(s集計年月).AddMonths(-4);
                dfiveTime = Convert.ToDateTime(s集計年月).AddMonths(-5);
                int nowTime, oneTime, twoTime, ThreeTime, fourTime, fiveTime;
                //Int版
                nowTime = Convert.ToInt32(dnowTime.ToString().Substring(0, 4) + dnowTime.ToString().Substring(5, 2));
                oneTime = Convert.ToInt32(doneTime.ToString().Substring(0, 4) + doneTime.ToString().Substring(5, 2));
                twoTime = Convert.ToInt32(dtwoTime.ToString().Substring(0, 4) + dtwoTime.ToString().Substring(5, 2));
                ThreeTime = Convert.ToInt32(dThreeTime.ToString().Substring(0, 4) + dThreeTime.ToString().Substring(5, 2));
                fourTime = Convert.ToInt32(dfourTime.ToString().Substring(0, 4) + dfourTime.ToString().Substring(5, 2));
                fiveTime = Convert.ToInt32(dfiveTime.ToString().Substring(0, 4) + dfiveTime.ToString().Substring(5, 2));


                List<TKS10010_Member> retList = new List<TKS10010_Member>();
                context.Connection.Open();

                var query = (from m01 in context.M01_TOK.Where(c => c.削除日付 == null)
                             join s01 in context.S01_TOKS.Where(c => c.回数 == 1) on m01.得意先KEY equals s01.得意先KEY into Group
                             from s01Group in Group
                             group new { m01, s01Group } by new { m01.得意先ID, m01.略称名, s01Group.集計年月, m01.Ｔ締日期首残 } into grGroup
                             select new TKS10010_tMember
                             {
                                 得意先コード = grGroup.Key.得意先ID,
                                 得意先名 = grGroup.Key.略称名,
                                 集計年月 = s集計年月,
                                 年月 = grGroup.Key.集計年月,
                                 入金金額 = grGroup.Sum(c => c.s01Group.締日入金現金 + c.s01Group.締日入金手形) == null ? 0 : grGroup.Sum(c => c.s01Group.締日入金現金 + c.s01Group.締日入金手形),
                                 調整他 = grGroup.Sum(c => c.s01Group.締日入金その他) == null ? 0 : grGroup.Sum(c => c.s01Group.締日入金その他),
                                 売上金額 = grGroup.Sum(c => c.s01Group.締日売上金額) == null ? 0 : grGroup.Sum(c => c.s01Group.締日売上金額),
                                 消費税 = grGroup.Sum(c => c.s01Group.締日消費税) == null ? 0 : grGroup.Sum(c => c.s01Group.締日消費税),
                                 通行料 = grGroup.Sum(c => c.s01Group.締日通行料) == null ? 0 : grGroup.Sum(c => c.s01Group.締日通行料),
                                 当月合計 = grGroup.Sum(c => c.s01Group.締日売上金額 + c.s01Group.締日消費税 + c.s01Group.締日通行料) == null ? 0 : grGroup.Sum(c => c.s01Group.締日売上金額 + c.s01Group.締日消費税 + c.s01Group.締日通行料),
                                 前月残高 = grGroup.Where(c => c.s01Group.回数 == 1).Sum(c => c.s01Group.締日前月残高) == null ? 0 : grGroup.Where(c => c.s01Group.回数 == 1).Sum(c => c.s01Group.締日前月残高),

                             }).AsQueryable();
                int cnt = 0;
                List<TKS10010_tMember> queryList = query.ToList();
                if (queryList.Count > 0)
                {
                    queryList[0].繰越残高 = (queryList[0].売上金額 + queryList[0].消費税 + queryList[0].通行料) - (queryList[0].入金金額 + queryList[0].調整他);
                    for (int i = 1; i < queryList.Count(); i++)
                    {
                        if (queryList[i].得意先コード == queryList[i - 1].得意先コード)
                        {


                            queryList[i].差引金額 = queryList[i].前月残高 - queryList[i].入金金額 - queryList[i].調整他;
                            queryList[i].繰越残高 = queryList[i].差引金額 + queryList[i].売上金額 + queryList[i].消費税 + queryList[i].通行料;
                            if (queryList[i].繰越残高 == 0 && cnt == 0)
                            {
                                queryList[i].繰越残高 = queryList[i + 1].前月残高;
                                cnt++;
                            }

                        }
                        else
                        {
                            queryList[i].差引金額 = 0;
                            queryList[i].繰越残高 = 0;
                            queryList[i].繰越残高 = queryList[i].繰越残高 = (queryList[i].売上金額 + queryList[i].消費税 + queryList[i].通行料) - (queryList[i].入金金額 + queryList[i].調整他);
                        }
                    }
                }

                queryList = queryList.OrderBy(c => c.得意先コード).ToList();


                var query2 = (from q01 in queryList
                              join m01 in context.M01_TOK.Where(c => c.親子区分ID != 3) on q01.得意先コード equals m01.得意先ID into m01Group
                              from Group in m01Group
                              group new { q01, Group } by new { q01.得意先コード, q01.得意先名, Group.親子区分ID, Group.Ｔ集金日, Group.Ｔサイト日, Group.Ｔ締日期首残 } into grGroup
                              select new TKS10010_Member
                              {
                                  コード = grGroup.Key.得意先コード,
                                  得意先名 = grGroup.Key.得意先名,
                                  親子区分 = grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ? "親" : "子",
                                  集金日 = grGroup.Key.Ｔ集金日,
                                  サイト = grGroup.Key.Ｔサイト日,
                                  入金滞留額 = grGroup.Where(c => c.q01.年月 == nowTime).Sum(c => c.q01.繰越残高) == 0 ? 0 : grGroup.Where(c => c.q01.年月 == nowTime).Sum(c => c.q01.繰越残高),
                                  一ヶ月前残 = grGroup.Where(c => c.q01.年月 == oneTime).Sum(c => c.q01.繰越残高) == 0 ? grGroup.Where(c => c.q01.年月 == nowTime).Sum(c => c.q01.繰越残高) : grGroup.Where(c => c.q01.年月 == oneTime).Sum(c => c.q01.繰越残高),
                                  二ヶ月前残 = grGroup.Where(c => c.q01.年月 == twoTime).Sum(c => c.q01.繰越残高) == 0 ? grGroup.Where(c => c.q01.年月 == oneTime).Sum(c => c.q01.繰越残高) : grGroup.Where(c => c.q01.年月 == twoTime).Sum(c => c.q01.繰越残高),
                                  三ヶ月前残 = grGroup.Where(c => c.q01.年月 == ThreeTime).Sum(c => c.q01.繰越残高) == 0 ? grGroup.Where(c => c.q01.年月 == twoTime).Sum(c => c.q01.繰越残高) : grGroup.Where(c => c.q01.年月 == ThreeTime).Sum(c => c.q01.繰越残高),
                                  四ヶ月前残 = grGroup.Where(c => c.q01.年月 == fourTime).Sum(c => c.q01.繰越残高) == 0 ? grGroup.Where(c => c.q01.年月 == ThreeTime).Sum(c => c.q01.繰越残高) : grGroup.Where(c => c.q01.年月 == fourTime).Sum(c => c.q01.繰越残高),
                                  五ヶ月前残 = grGroup.Where(c => c.q01.年月 == fiveTime).Sum(c => c.q01.繰越残高) == 0 ? 0 : grGroup.Where(c => c.q01.年月 == fiveTime).Sum(c => c.q01.繰越残高),
                                  作成年月度 = s作成年月度,
                                  集金区分 = i作成区分 == 0 ? "滞留有りのみ" : "滞留無しを含み",
                                  集金日区分 = b全集金日集計 == true ? "全集金日" : s作成集金日 + "日",
                                  得意先指定 = s得意先From + "～" + s得意先To,
                                  得意先ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "" : 取引先指定ﾋﾟｯｸｱｯﾌﾟ,
                              }).AsQueryable();

                ///＜＜＜データの絞込み＞＞＞
                if (!(string.IsNullOrEmpty(s得意先From + s得意先To) && i得意先List.Length == 0))
                {
                    if (string.IsNullOrEmpty(s得意先From + s得意先To))
                    {
                        query = query.Where(c => c.得意先コード >= int.MaxValue);
                        query2 = query2.Where(c => c.コード >= int.MaxValue);
                    }

                    if (!string.IsNullOrEmpty(s得意先From))
                    {
                        int i得意先From = AppCommon.IntParse(s得意先From);
                        query = query.Where(c => c.得意先コード >= i得意先From);
                        query2 = query2.Where(c => c.コード >= i得意先From);
                    }

                    if (!string.IsNullOrEmpty(s得意先To))
                    {
                        int i得意先To = AppCommon.IntParse(s得意先To);
                        query = query.Where(c => c.得意先コード <= i得意先To);
                        query2 = query2.Where(c => c.コード <= i得意先To);
                    }

                    if (i得意先List.Length > 0)
                    {
                        var intCouse = i得意先List;
                        query2 = query2.Union(from q01 in queryList
                                              join m01 in context.M01_TOK.Where(c => c.親子区分ID != 3) on q01.得意先コード equals m01.得意先ID into m01Group
                                              from Group in m01Group
                                              group new { q01, Group } by new { q01.得意先コード, q01.得意先名, Group.親子区分ID, Group.Ｔ集金日, Group.Ｔサイト日, Group.Ｔ締日期首残 } into grGroup
                                              where intCouse.Contains(grGroup.Key.得意先コード)

                                              select new TKS10010_Member
                                              {
                                                  コード = grGroup.Key.得意先コード,
                                                  得意先名 = grGroup.Key.得意先名,
                                                  親子区分 = grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ? "親" : "子",
                                                  集金日 = grGroup.Key.Ｔ集金日,
                                                  サイト = grGroup.Key.Ｔサイト日,
                                                  入金滞留額 = grGroup.Where(c => c.q01.年月 == nowTime).Sum(c => c.q01.繰越残高) == 0 ? 0 : grGroup.Where(c => c.q01.年月 == nowTime).Sum(c => c.q01.繰越残高),
                                                  一ヶ月前残 = grGroup.Where(c => c.q01.年月 == oneTime).Sum(c => c.q01.繰越残高) == 0 ? grGroup.Where(c => c.q01.年月 == oneTime).Sum(c => c.q01.繰越残高) : grGroup.Where(c => c.q01.年月 == oneTime).Sum(c => c.q01.繰越残高),
                                                  二ヶ月前残 = grGroup.Where(c => c.q01.年月 == twoTime).Sum(c => c.q01.繰越残高) == 0 ? grGroup.Where(c => c.q01.年月 == oneTime).Sum(c => c.q01.繰越残高) : grGroup.Where(c => c.q01.年月 == twoTime).Sum(c => c.q01.繰越残高),
                                                  三ヶ月前残 = grGroup.Where(c => c.q01.年月 == ThreeTime).Sum(c => c.q01.繰越残高) == 0 ? grGroup.Where(c => c.q01.年月 == twoTime).Sum(c => c.q01.繰越残高) : grGroup.Where(c => c.q01.年月 == ThreeTime).Sum(c => c.q01.繰越残高),
                                                  四ヶ月前残 = grGroup.Where(c => c.q01.年月 == fourTime).Sum(c => c.q01.繰越残高) == 0 ? grGroup.Where(c => c.q01.年月 == ThreeTime).Sum(c => c.q01.繰越残高) : grGroup.Where(c => c.q01.年月 == fourTime).Sum(c => c.q01.繰越残高),
                                                  五ヶ月前残 = grGroup.Where(c => c.q01.年月 == fiveTime).Sum(c => c.q01.繰越残高) == 0 ? 0 : grGroup.Where(c => c.q01.年月 == fiveTime).Sum(c => c.q01.繰越残高) + grGroup.Key.Ｔ締日期首残,
                                                  作成年月度 = s作成年月度,
                                                  集金区分 = i作成区分 == 0 ? "滞留有りのみ" : "滞留無しを含み",
                                                  集金日区分 = b全集金日集計 == true ? "全集金日" : s作成集金日 + "日",
                                                  得意先指定 = s得意先From + "～" + s得意先To,
                                                  得意先ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "" : 取引先指定ﾋﾟｯｸｱｯﾌﾟ,
                                              });

                        //取引先指定の表示
                        if (i得意先List.Length > 0)
                        {
                            for (int i = 0; i < query2.Count(); i++)
                            {
                                取引先指定ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ + i得意先List[i].ToString();

                                if (i < i得意先List.Length)
                                {

                                    if (i == i得意先List.Length - 1)
                                    {
                                        break;
                                    }

                                    取引先指定ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ + ",";

                                }

                                if (i得意先List.Length == 1)
                                {
                                    break;
                                }

                            }
                        }
                    }
                }
                else
                {
                    //取引先FromがNullだった場合
                    query2 = query2.Where(c => c.コード >= int.MinValue);

                    //得意先ToがNullだった場合
                    query2 = query2.Where(c => c.コード <= int.MaxValue);
                }

                List<TKS10010_Member> queryList2 = query2.ToList();
                for (int i = 0; i < queryList2.Count(); i++)
                {
                    int site = queryList2[i].サイト;
                    switch (site)
                    {
                        case 0:
                            queryList2[i].一ヶ月前残 = 0;
                            queryList2[i].二ヶ月前残 = 0;
                            queryList2[i].三ヶ月前残 = 0;
                            queryList2[i].四ヶ月前残 = 0;
                            queryList2[i].五ヶ月前残 = 0;
                            break;
                        case 1:
                            queryList2[i].二ヶ月前残 = 0;
                            queryList2[i].三ヶ月前残 = 0;
                            queryList2[i].四ヶ月前残 = 0;
                            queryList2[i].五ヶ月前残 = 0;
                            break;
                        case 2:
                            queryList2[i].三ヶ月前残 = 0;
                            queryList2[i].四ヶ月前残 = 0;
                            queryList2[i].五ヶ月前残 = 0;
                            break;
                        case 3:
                            queryList2[i].四ヶ月前残 = 0;
                            queryList2[i].五ヶ月前残 = 0;
                            break;
                        case 4:
                            queryList2[i].五ヶ月前残 = 0;
                            break;
                    }
                }


                if (i作成区分 == 0)
                {
                    queryList2 = queryList2.Where(c => c.入金滞留額 != 0).ToList();
                }
                if (b全集金日集計 == false)
                {
                    if (s作成集金日 != "")
                    {
                        int i作成集金日 = AppCommon.IntParse(s作成集金日);
                        queryList2 = queryList2.Where(c => c.集金日 == i作成集金日).ToList();
                    }
                }
                return queryList2.ToList();

            }
        }
    }
}