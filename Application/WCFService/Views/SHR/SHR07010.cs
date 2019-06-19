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

    /// <summary>
    /// SHR07010 支払残高問い合わせ
    /// </summary>
    [DataContract]
    public class SHR07010_Member
    {
        [DataMember]
        public int 支払先コード { get; set; }
        [DataMember]
        public string 支払先名 { get; set; }
        [DataMember]
        public string 集計年月 { get; set; }
        [DataMember]
        public string 表示区分 { get; set; }
        [DataMember]
        public int 年月 { get; set; }
        [DataMember]
        public decimal 出金金額 { get; set; }
        [DataMember]
        public decimal 調整他 { get; set; }
        [DataMember]
        public decimal 差引繰越 { get; set; }
        [DataMember]
        public decimal 支払金額 { get; set; }
        [DataMember]
        public decimal 消費税 { get; set; }
        [DataMember]
        public decimal 傭車立替 { get; set; }
        [DataMember]
        public decimal 当月合計 { get; set; }
        [DataMember]
        public decimal 繰越残高 { get; set; }
        [DataMember]
        public string 表示年月 { get; set; }
        [DataMember]
        public decimal 前月残高 { get; set; }
    }

    public class SHR07010
    {
        public List<SHR07010_Member> SHR07010_GetDataHinList(int i得意先ID, string s作成年 , string s作成月 , int i集計年月 , int i表示区分, string s集計年月)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {


                List<SHR07010_Member> retList = new List<SHR07010_Member>();
                context.Connection.Open();
                //【締日データ編】
                if (i表示区分 == 0)
                {
                    var qList = (from m01 in context.M01_TOK
                                 join s02 in context.S02_YOSS.Where(c => c.回数 == 1) on m01.得意先KEY equals s02.支払先KEY into Group
                                 from s02Group in Group
                                 group new { m01, s02Group } by new { m01.得意先ID, m01.略称名, s02Group.集計年月 } into grGroup
                                 where grGroup.Key.得意先ID == i得意先ID
                                 select new SHR07010_Member
                                 {
                                     支払先コード = grGroup.Key.得意先ID,
                                     支払先名 = grGroup.Key.略称名,
                                     集計年月 = s集計年月,
                                     表示区分 = i表示区分 == 0 ? "締日" : "月次",
                                     年月 = 0,
                                     出金金額 = 0,
                                     調整他 = 0,
                                     支払金額 = 0,
                                     消費税 = 0,
                                     傭車立替 = 0,
                                     当月合計 = 0,
                                     前月残高 = 0,
                                 }).Distinct().AsQueryable();


                    var query = (from m01 in context.M01_TOK
                                 join s02 in context.S02_YOSS.Where(c => c.回数 == 1) on m01.得意先KEY equals s02.支払先KEY into Group
                                 from s02Group in Group
                                 group new { m01, s02Group } by new { m01.得意先ID, m01.略称名, s02Group.集計年月, m01.Ｓ締日期首残 } into grGroup
                                 where grGroup.Key.得意先ID == i得意先ID
                                 select new SHR07010_Member
                                 {
                                     支払先コード = grGroup.Key.得意先ID,
                                     支払先名 = grGroup.Key.略称名,
                                     集計年月 = s集計年月,
                                     表示区分 = i表示区分 == 0 ? "締日" : "月次",
                                     年月 = grGroup.Key.集計年月,
                                     出金金額 = grGroup.Sum(c => c.s02Group.締日入金現金 + c.s02Group.締日入金手形) == null ? 0 : grGroup.Sum(c => c.s02Group.締日入金現金 + c.s02Group.締日入金手形),
                                     調整他 = grGroup.Sum(c => c.s02Group.締日入金その他) == null ? 0 : grGroup.Sum(c => c.s02Group.締日入金その他),
                                     支払金額 = grGroup.Sum(c => c.s02Group.締日売上金額) == null ? 0 : grGroup.Sum(c => c.s02Group.締日売上金額),
                                     消費税 = grGroup.Sum(c => c.s02Group.締日消費税) == null ? 0 : grGroup.Sum(c => c.s02Group.締日消費税),
                                     傭車立替 = grGroup.Sum(c => c.s02Group.締日通行料) == null ? 0 : grGroup.Sum(c => c.s02Group.締日通行料),
                                     当月合計 = grGroup.Sum(c => c.s02Group.締日売上金額 + c.s02Group.締日消費税 + c.s02Group.締日通行料) == null ? 0 : grGroup.Sum(c => c.s02Group.締日売上金額 + c.s02Group.締日消費税 + c.s02Group.締日通行料),
                                     前月残高 = grGroup.Where(c => c.s02Group.回数 == 1).Sum(c => c.s02Group.締日前月残高) == null ? 0 : grGroup.Where(c => c.s02Group.回数 == 1).Sum(c => c.s02Group.締日前月残高),
                                 }).AsQueryable();
                    int cnt = 0;
                    List<SHR07010_Member> queryList = query.ToList();
                    //前回繰越を元に差引金額と現在の繰越残高を算出
                    if (queryList.Count > 0)
                    {
                        queryList[0].繰越残高 = (queryList[0].支払金額 + queryList[0].消費税 + queryList[0].傭車立替) - (queryList[0].出金金額 + queryList[0].調整他) + queryList[0].前月残高;
                        for (int i = 1; i < queryList.Count(); i++)
                        {
                            queryList[i].差引繰越 = queryList[i].前月残高 - queryList[i].出金金額 - queryList[i].調整他;
                            queryList[i].繰越残高 = queryList[i].差引繰越 + queryList[i].支払金額 + queryList[i].消費税 + queryList[i].傭車立替;
                            if (queryList[i].繰越残高 == 0 && cnt == 0 && queryList.Count() < 2)
                            {
                                queryList[i].繰越残高 = queryList[i + 1].前月残高;
                                cnt++;
                            }
                        }
                        }
                    else
                    {
                        return queryList;
                    }

                    queryList = queryList.Where(c => c.年月 >= i集計年月).ToList();
                    
                    if (queryList.Count() == 0)
                    {
                        return queryList.ToList();
                    }

                    List<SHR07010_Member> queryList2 = qList.ToList();
                    queryList2[0].繰越残高 = queryList[0].差引繰越;
                    if (queryList2[0].繰越残高 == decimal.Zero)
                    {
                        queryList2[0].繰越残高 = queryList[0].前月残高;
                        queryList[0].差引繰越 = queryList2[0].繰越残高;
                    }
                    queryList.AddRange(queryList2);
                    queryList = queryList.OrderBy(c => c.年月).ToList();

                    int queryCount = queryList.Count();
                    for (int i = 1; i < queryCount; i++)
                    {
                        int Days = queryList[i].年月;
                        queryList[i].表示年月 = Days.ToString().Substring(0, 4) + "/" + Days.ToString().Substring(4, 2);
                    }
                    return queryList.ToList();
                }
                //【月次データ編】
                else
                {


                    var qList = (from m01 in context.M01_TOK
                                 join s12 in context.S12_YOSG.Where(c => c.回数 == 1) on m01.得意先KEY equals s12.支払先KEY into Group
                                 from s12Group in Group
                                 group new { m01, s12Group } by new { m01.得意先ID, m01.略称名, s12Group.集計年月 } into grGroup
                                 where grGroup.Key.得意先ID == i得意先ID
                                 select new SHR07010_Member
                                 {
                                     支払先コード = grGroup.Key.得意先ID,
                                     支払先名 = grGroup.Key.略称名,
                                     集計年月 = s集計年月,
                                     表示区分 = i表示区分 == 0 ? "締日" : "月次",
                                     年月 = 0,
                                     出金金額 = 0,
                                     調整他 = 0,
                                     支払金額 = 0,
                                     消費税 = 0,
                                     傭車立替 = 0,
                                     当月合計 = 0,
                                     前月残高 = 0,
                                 }).Distinct().AsQueryable();



                    var query = (from m01 in context.M01_TOK
                                 join s12 in context.S12_YOSG.Where(c => c.回数 == 1) on m01.得意先KEY equals s12.支払先KEY into Group
                                 from s12Group in Group
                                 group new { m01, s12Group } by new { m01.得意先ID, m01.略称名, s12Group.集計年月 , m01.Ｓ月次期首残 } into grGroup
                                 where grGroup.Key.得意先ID == i得意先ID
                                 select new SHR07010_Member
                                 {
                                     支払先コード = grGroup.Key.得意先ID,
                                     支払先名 = grGroup.Key.略称名,
                                     集計年月 = s集計年月,
                                     表示区分 = i表示区分 == 0 ? "締日" : "月次",
                                     年月 = grGroup.Key.集計年月,
                                     出金金額 = grGroup.Sum(c => c.s12Group.月次入金現金 + c.s12Group.月次入金手形) == null ? 0 : grGroup.Sum(c => c.s12Group.月次入金現金 + c.s12Group.月次入金手形),
                                     調整他 = grGroup.Sum(c => c.s12Group.月次入金その他) == null ? 0 : grGroup.Sum(c => c.s12Group.月次入金その他),
                                     支払金額 = grGroup.Sum(c => c.s12Group.月次売上金額) == null ? 0 : grGroup.Sum(c => c.s12Group.月次売上金額),
                                     消費税 = grGroup.Sum(c => c.s12Group.月次消費税) == null ? 0 : grGroup.Sum(c => c.s12Group.月次消費税),
                                     傭車立替 = grGroup.Sum(c => c.s12Group.月次通行料) == null ? 0 : grGroup.Sum(c => c.s12Group.月次通行料),
                                     当月合計 = grGroup.Sum(c => c.s12Group.月次売上金額 + c.s12Group.月次消費税 + c.s12Group.月次通行料) == null ? 0 : grGroup.Sum(c => c.s12Group.月次売上金額 + c.s12Group.月次消費税 + c.s12Group.月次通行料),
                                     前月残高 = grGroup.Where(c => c.s12Group.回数 == 1).Sum(c => c.s12Group.月次前月残高) == null ? 0 : grGroup.Where(c => c.s12Group.回数 == 1).Sum(c => c.s12Group.月次前月残高),

                                 }).AsQueryable();
                    int cnt = 0;
                    List<SHR07010_Member> queryList = query.ToList();
                    //前回繰越を元に差引金額と現在の繰越残高を算出
                    if (queryList.Count > 0)
                    {
                        queryList[0].繰越残高 = (queryList[0].支払金額 + queryList[0].消費税 + queryList[0].傭車立替) - (queryList[0].出金金額 + queryList[0].調整他) + queryList[0].前月残高;
                        for (int i = 1; i < queryList.Count(); i++)
                        {
                            queryList[i].差引繰越 = queryList[i].前月残高 - queryList[i].出金金額 - queryList[i].調整他;
                            queryList[i].繰越残高 = queryList[i].差引繰越 + queryList[i].支払金額 + queryList[i].消費税 + queryList[i].傭車立替;
                            if (queryList[i].繰越残高 == 0 && cnt == 0 && queryList.Count() < 2)
                            {
                                queryList[i].繰越残高 = queryList[i + 1].前月残高;
                                cnt++;
                            }
                        }
                    }
                    else
                    {
                        return queryList;
                    }

                    queryList = queryList.Where(c => c.年月 >= i集計年月).ToList();

                    if (queryList.Count() == 0)
                    {
                        return queryList.ToList();
                    }

                    List<SHR07010_Member> queryList2 = qList.ToList();
                    queryList2[0].繰越残高 = queryList[0].差引繰越;
                    if (queryList2[0].繰越残高 == decimal.Zero)
                    {
                        queryList2[0].繰越残高 = queryList[0].前月残高;
                        queryList[0].差引繰越 = queryList2[0].繰越残高;
                    }
                    queryList.AddRange(queryList2);
                    queryList = queryList.OrderBy(c => c.年月).ToList();

                    int queryCount = queryList.Count();
                    for (int i = 1; i < queryCount; i++)
                    {
                        int Days = queryList[i].年月;
                        queryList[i].表示年月 = Days.ToString().Substring(0, 4) + "/" + Days.ToString().Substring(4, 2);
                    }
                    return queryList.ToList();

                }
            }
        }
    }
}