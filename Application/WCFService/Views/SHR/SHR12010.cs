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

    #region SHR12010_Member

    [DataContract]
    public class SHR12010_Member
    {
        [DataMember]
        public int 取引先ID { get; set; }
        [DataMember]
        public string 取引先名 { get; set; }
        [DataMember]
        public int 取引区分 { get; set; }
        [DataMember]
        public string 親子区分 { get; set; }
        [DataMember]
        public int 締日 { get; set; }
        [DataMember]
        public decimal 売上金額 { get; set; }
        [DataMember]
        public decimal 売上消費税 { get; set; }
        [DataMember]
        public decimal 通行料 { get; set; }
        [DataMember]
        public decimal 売上金額計 { get; set; }
        [DataMember]
        public decimal 売上合計 { get; set; }
        [DataMember]
        public decimal 支払合計 { get; set; }
        [DataMember]
        public decimal 傭車金額 { get; set; }
        [DataMember]
        public decimal 支払消費税 { get; set; }
        [DataMember]
        public decimal 支払通行料 { get; set; }
        [DataMember]
        public decimal 傭車金額計 { get; set; }
        [DataMember]
        public string 作成年月度 { get; set; }
        [DataMember]
        public string 取引先指定 { get; set; }
        [DataMember]
        public string 取引先ﾋﾟｯｸｱｯﾌﾟ { get; set; }
    }

    #endregion

    #region SHR12010_Value1 売上Value1

    //売上データ取得
    public class SHR12010_Value1
    {
        [DataMember]
        public int 取引先ID { get; set; }
        [DataMember]
        public string 取引先名 { get; set; }
        [DataMember]
        public string 親子区分 { get; set; }
        [DataMember]
        public int 締日 { get; set; }
        [DataMember]
        public decimal 売上金額 { get; set; }
        [DataMember]
        public decimal 売上消費税 { get; set; }
        [DataMember]
        public decimal 通行料 { get; set; }
        [DataMember]
        public decimal 売上金額計 { get; set; }
        [DataMember]
        public decimal 売上合計 { get; set; }
        [DataMember]
        public int 取引区分 { get; set; }

    }

    #endregion

    #region SHR12010_Value2 支払Value2

    //支払データ取得
    public class SHR12010_Value2
    {
        [DataMember]
        public int 取引先ID { get; set; }
        [DataMember]
        public string 取引先名 { get; set; }
        [DataMember]
        public string 親子区分 { get; set; }
        [DataMember]
        public int 締日 { get; set; }
        [DataMember]
        public decimal 支払合計 { get; set; }
        [DataMember]
        public decimal 傭車金額 { get; set; }
        [DataMember]
        public decimal 支払消費税 { get; set; }
        [DataMember]
        public decimal 支払通行料 { get; set; }
        [DataMember]
        public decimal 傭車金額計 { get; set; }
        [DataMember]
        public int 取引区分 { get; set; }
    }

    #endregion

    #region SHR12010_Sales 売上データ取得

    //売上データ取得
    public class SHR12010_Sales
    {
        [DataMember]
        public int 取引先ID { get; set; }
        [DataMember]
        public string 取引先名 { get; set; }
        [DataMember]
        public string 親子区分 { get; set; }
        [DataMember]
        public int 締日 { get; set; }
        [DataMember]
        public decimal 売上金額 { get; set; }
        [DataMember]
        public decimal 売上消費税 { get; set; }
        [DataMember]
        public decimal 通行料 { get; set; }
        [DataMember]
        public decimal 売上金額計 { get; set; }
        [DataMember]
        public decimal 売上合計 { get; set; }
        [DataMember]
        public int 取引区分 { get; set; }

    }

    #endregion

    #region SHR12010_Payment 支払データ取得

    //支払データ取得
    public class SHR12010_Payment
    {
        [DataMember]
        public int 取引先ID { get; set; }
        [DataMember]
        public string 取引先名 { get; set; }
        [DataMember]
        public string 親子区分 { get; set; }
        [DataMember]
        public int 締日 { get; set; }
        [DataMember]
        public decimal 支払合計 { get; set; }
        [DataMember]
        public decimal 傭車金額 { get; set; }
        [DataMember]
        public decimal 支払消費税 { get; set; }
        [DataMember]
        public decimal 支払通行料 { get; set; }
        [DataMember]
        public decimal 傭車金額計 { get; set; }
        [DataMember]
        public int 取引区分 { get; set; }
    }

    #endregion

    #region データ結合メンバー

    public class SHR12010_Cord
    {
        [DataMember]
        public int 取引先ID { get; set; }
        [DataMember]
        public string 取引先名 { get; set; }
        [DataMember]
        public int 取引区分 { get; set; }
        [DataMember]
        public string 親子区分 { get; set; }
        [DataMember]
        public int 締日 { get; set; }
    }

    #endregion


    public class SHR12010
    {

        public List<SHR12010_Member> SHR12010_GetDataHinList(string s取引先From, string s取引先To, int?[] i取引先List, int i作成年, int i作成月, int i作成年月, int i作成区分, int i取引区分, string s作成年月度)
        {
            string 取引先指定ﾋﾟｯｸｱｯﾌﾟ = string.Empty;


            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<SHR12010_Member> retList = new List<SHR12010_Member>();
                context.Connection.Open();


                //売上データ取得
                var Value1 = (from m01 in context.M01_TOK.Where(c => c.削除日付 == null && c.親子区分ID != 3).DefaultIfEmpty()
                             join s01 in context.S01_TOKS.Where(c => c.回数 == 1).DefaultIfEmpty() on m01.得意先KEY equals s01.得意先KEY into Group
                             from s01Group in Group.Where(c => c.集計年月 == i作成年月)
                             group new {m01,s01Group} by new {m01.得意先ID , m01.略称名 , m01.親子区分ID , m01.Ｔ締日 , m01.取引区分} into grGroup
                             select new SHR12010_Value1
                             {
                                 取引先ID = grGroup.Key.得意先ID,
                                 取引先名 = grGroup.Key.略称名,
                                 取引区分 = grGroup.Key.取引区分,
                                 親子区分 = grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ?"親" : "子",
                                 締日 = grGroup.Key.Ｔ締日,
                                 売上金額 = grGroup.Sum(c => c.s01Group.締日売上金額) == null ? 0 : grGroup.Sum(c => c.s01Group.締日売上金額),
                                 売上消費税 = grGroup.Sum(c => c.s01Group.締日消費税) == null ? 0 : grGroup.Sum(c => c.s01Group.締日消費税),
                                 通行料 = grGroup.Sum(c => c.s01Group.締日通行料) == null ? 0 : grGroup.Sum(c => c.s01Group.締日通行料),
                                 売上金額計 = grGroup.Sum(c => c.s01Group.締日売上金額 + c.s01Group.締日消費税 + c.s01Group.締日通行料) == null ? 0 : grGroup.Sum(c => c.s01Group.締日売上金額 + c.s01Group.締日消費税 + c.s01Group.締日通行料), 
                             }).AsQueryable();

                var Sales = (from m01 in context.M01_TOK.Where(c => c.削除日付 == null && c.親子区分ID != 3)
                             from sal in Value1.Where(c => c.取引先ID == m01.得意先ID).DefaultIfEmpty()
                             select new SHR12010_Sales
                         {
                             取引先ID = m01.得意先ID,
                             取引先名 = m01.略称名,
                             取引区分 = m01.取引区分,
                             親子区分 = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                             締日 = m01.Ｔ締日,
                             売上金額 = Value1.Where(c => c.取引先ID == m01.得意先ID).Sum(c => c.売上金額) == null ? 0 : sal.売上金額,
                             売上消費税 = Value1.Where(c => c.取引先ID == m01.得意先ID).Sum(c => c.売上消費税) == null ? 0 : sal.売上消費税,
                             通行料 = Value1.Where(c => c.取引先ID == m01.得意先ID).Sum(c => c.通行料) == null ? 0 : sal.通行料,
                             売上金額計 = Value1.Where(c => c.取引先ID == m01.得意先ID).Sum(c => c.売上金額計) == null ? 0 : sal.売上金額計,
                         }).AsQueryable();

                //支払データ取得
                var Value2 = (from m01 in context.M01_TOK.Where(c => c.削除日付 == null && c.親子区分ID != 3).DefaultIfEmpty()
                               join s02 in context.S02_YOSS.Where(c => c.回数 == 1).DefaultIfEmpty() on m01.得意先KEY equals s02.支払先KEY into Group
                               from s02Group in Group.Where(c => c.集計年月 == i作成年月)
                               group new { m01, s02Group } by new { m01.得意先ID, m01.略称名, m01.親子区分ID, s02Group.締日, m01.取引区分 } into grGroup
                               select new SHR12010_Value2
                               {
                                   取引先ID = grGroup.Key.得意先ID,
                                   取引先名 = grGroup.Key.略称名,
                                   取引区分 = grGroup.Key.取引区分,
                                   親子区分 = grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ?"親" : "子",
                                 　締日 = grGroup.Key.締日,
                                   支払合計 = grGroup.Sum(c => c.s02Group.締日売上金額) == null ? 0 : grGroup.Sum(c => c.s02Group.締日売上金額),
                                   傭車金額 = grGroup.Sum(c => c.s02Group.締日売上金額) == null ? 0 : grGroup.Sum(c => c.s02Group.締日売上金額),
                                   支払消費税 = grGroup.Sum(c => c.s02Group.締日消費税) == null ? 0 : grGroup.Sum(c => c.s02Group.締日消費税),
                                   支払通行料 = grGroup.Sum(c => c.s02Group.締日通行料) == null ? 0 : grGroup.Sum(c => c.s02Group.締日通行料),
                                   傭車金額計 = grGroup.Sum(c => c.s02Group.締日売上金額 + c.s02Group.締日消費税 + c.s02Group.締日通行料) == null ? 0 :grGroup.Sum(c => c.s02Group.締日売上金額 + c.s02Group.締日消費税 + c.s02Group.締日通行料),
                               }).AsQueryable();

                var Payment = (from m01 in context.M01_TOK.Where(c => c.削除日付 == null && c.親子区分ID != 3)
                               from pay in Value2.Where(c => c.取引先ID == m01.得意先ID).DefaultIfEmpty()
                               select new SHR12010_Payment
                             {
                                 取引先ID = m01.得意先ID,
                                 取引先名 = m01.略称名,
                                 取引区分 = m01.取引区分,
                                 親子区分 = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                 締日 = m01.Ｔ締日,
                                 支払合計 = Value2.Where(c => c.取引先ID == m01.得意先ID).Sum(c => c.支払合計) == null ? 0 : pay.支払合計,
                                 傭車金額 = Value2.Where(c => c.取引先ID == m01.得意先ID).Sum(c => c.傭車金額) == null ? 0 : pay.傭車金額,
                                 支払消費税 = Value2.Where(c => c.取引先ID == m01.得意先ID).Sum(c => c.支払消費税) == null ? 0 : pay.支払消費税,
                                 支払通行料 = Value2.Where(c => c.取引先ID == m01.得意先ID).Sum(c => c.支払通行料) == null ? 0 : pay.支払通行料,
                                 傭車金額計 = Value2.Where(c => c.取引先ID == m01.得意先ID).Sum(c => c.傭車金額計) == null ? 0 : pay.傭車金額計,
                             }).AsQueryable();


                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                #region 売上と支払データ

                if (Sales.Count() != 0 && Payment.Count() != 0)
                {
                    //2つのクエリの【和集合】を求める
                    var retquery = (from s01 in Sales select s01.取引先ID).Union(
                                    from s02 in Payment select s02.取引先ID).AsQueryable();

                    var query = (from ret in retquery
                                 from m01 in context.M01_TOK.Where(c => c.削除日付 == null && c.親子区分ID != 3).DefaultIfEmpty()
                                from s01 in Sales.Where(c => c.取引先ID == ret).DefaultIfEmpty()
                                from s02 in Payment.Where(c => c.取引先ID == ret).DefaultIfEmpty()
                                where ret == m01.得意先ID
                                group new { m01 , s01 , s02 } by new
                                {  
                                    m01.得意先ID,
                                    m01.略称名,
                                    m01.取引区分,
                                    m01.親子区分ID,
                                    m01.Ｔ締日
                                }into grGroup
                                 select new SHR12010_Member
                                {
                                    取引先ID = grGroup.Key.得意先ID == null ? 0 : grGroup.Key.得意先ID,
                                    取引先名 = grGroup.Key.略称名 == null ? "" : grGroup.Key.略称名,
                                    取引区分 = grGroup.Key.取引区分 == null ? 0 : grGroup.Key.取引区分,
                                    親子区分 = grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ? "親" : "子" == null ? "" : grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ? "親" : "子",
                                    締日 = grGroup.Key.Ｔ締日 == null ? 0 : grGroup.Key.Ｔ締日,
                                    作成年月度 = s作成年月度,
                                    取引先指定 = s取引先From + "～" + s取引先To,
                                    取引先ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "" : 取引先指定ﾋﾟｯｸｱｯﾌﾟ,
                                }).Distinct().AsQueryable();

                    if (!(string.IsNullOrEmpty(s取引先From + s取引先To) && i取引先List.Length == 0))
                    {
                        if (string.IsNullOrEmpty(s取引先From + s取引先To))
                        {
                            query = query.Where(c => c.取引先ID >= int.MaxValue);
                        }

                        //取引先From
                        if (!string.IsNullOrEmpty(s取引先From))
                        {
                            int i取引先From = AppCommon.IntParse(s取引先From);
                            query = query.Where(c => c.取引先ID >= i取引先From);
                        }

                        //取引先To
                        if (!string.IsNullOrEmpty(s取引先To))
                        {
                            int i取引先To = AppCommon.IntParse(s取引先To);
                            query = query.Where(c => c.取引先ID <= i取引先To);
                        }

                        //取引先ピックアップ
                        if (i取引先List.Length > 0)
                        {
                            var intCause = i取引先List;
                            //全件表示
                            query = query.Union(from ret in retquery
                                                from m01 in context.M01_TOK.Where(c => c.削除日付 == null && c.親子区分ID != 3)
                                                from s01 in Sales.Where(c => c.取引先ID == ret).DefaultIfEmpty()
                                                from s02 in Payment.Where(c => c.取引先ID == ret).DefaultIfEmpty()
                                                where ret == m01.得意先ID && intCause.Contains(m01.得意先ID)
                                                group new { m01, s01, s02 } by new
                                                {
                                                    m01.得意先ID,
                                                    m01.略称名,
                                                    m01.取引区分,
                                                    m01.親子区分ID,
                                                    m01.Ｔ締日
                                                } into grGroup
                                                select new SHR12010_Member
                                                    {
                                                        取引先ID = grGroup.Key.得意先ID == null ? 0 : grGroup.Key.得意先ID,
                                                        取引先名 = grGroup.Key.略称名 == null ? "" : grGroup.Key.略称名,
                                                        取引区分 = grGroup.Key.取引区分 == null ? 0 : grGroup.Key.取引区分,
                                                        親子区分 = grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ? "親" : "子" == null ? "" : grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ? "親" : "子",
                                                        締日 = grGroup.Key.Ｔ締日 == null ? 0 : grGroup.Key.Ｔ締日,
                                                        作成年月度 = s作成年月度,
                                                        取引先指定 = s取引先From + "～" + s取引先To,
                                                        取引先ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "" : 取引先指定ﾋﾟｯｸｱｯﾌﾟ,
                                                    }).Distinct().AsQueryable();

                            //取引先指定の表示
                            if (i取引先List.Length > 0)
                            {
                                for (int i = 0; i < query.Count(); i++)
                                {
                                    取引先指定ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ + i取引先List[i].ToString();

                                    if (i < i取引先List.Length)
                                    {

                                        if (i == i取引先List.Length - 1)
                                        {
                                            break;
                                        }

                                        取引先指定ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ + ",";

                                    }

                                    if (i取引先List.Length == 1)
                                    {
                                        break;
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        //s取引先FromがNULLだった場合
                        if (string.IsNullOrEmpty(s取引先From))
                        {
                            query = query.Where(c => c.取引先ID >= int.MinValue);
                        }

                        //s取引先ToがNullだった場合
                        if (string.IsNullOrEmpty(s取引先To))
                        {
                            query = query.Where(c => c.取引先ID <= int.MaxValue);
                        }

                    }


                    //【売上】・【支払】・【売上/支払】データをList化
                    List<SHR12010_Member> queryLIST = query.ToList();
                    List<SHR12010_Sales> SalesList = Sales.ToList();
                    List<SHR12010_Payment> PaymentList = Payment.ToList();

                    //売上と支払を1つにしたデータをLoop
                    for (int i = 0; i < queryLIST.Count(); i++)
                    {
                        //QueryListに当てはまる【売上】データがある場合、データ挿入
                        for (int x = 0; x < SalesList.Count(); x++)
                        {
                            if (queryLIST[i].取引先ID == SalesList[x].取引先ID)
                            {
                                queryLIST[i].売上金額 = SalesList[x].売上金額;
                                queryLIST[i].売上金額計 = SalesList[x].売上金額計;
                                queryLIST[i].売上消費税 = SalesList[x].売上消費税;
                                queryLIST[i].通行料 = SalesList[x].通行料;
                            }
                            else
                            {
                                continue;
                            }
                        }

                        //QueryListに当てはまる【支払】データがある場合、データ挿入
                        for (int y = 0; y < PaymentList.Count(); y++)
                        {
                            if (queryLIST[i].取引先ID == PaymentList[y].取引先ID)
                            {
                                queryLIST[i].傭車金額 = PaymentList[y].傭車金額;
                                queryLIST[i].傭車金額計 = PaymentList[y].傭車金額計;
                                queryLIST[i].支払消費税 = PaymentList[y].支払消費税;
                                queryLIST[i].支払通行料 = PaymentList[y].支払通行料;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }

                    //帳票の【売上額】と【支払額】を比較して額が多いほうの差分データを挿入
                    for (int i = 0; i < queryLIST.Count(); i++)
                    {
                        if (queryLIST[i].売上金額計 > queryLIST[i].傭車金額計)
                        {
                            queryLIST[i].売上合計 = queryLIST[i].売上金額計 - queryLIST[i].傭車金額計;
                        }
                        else if (queryLIST[i].売上金額計 < queryLIST[i].傭車金額計)
                        {
                            queryLIST[i].支払合計 = queryLIST[i].傭車金額計 - queryLIST[i].売上金額計;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    


                    //作成区分が0だった場合
                    if (i作成区分 == 0)
                    {
                        //売上・支払が発生していないデータを除く
                        queryLIST = queryLIST.Where(c => c.売上金額計 != 0 || c.傭車金額計 != 0).ToList();
                    }


                    //取引区分が1だった場合【全取引】のみのデータを取得
                    if (i取引区分 == 1)
                    {
                        queryLIST = queryLIST.Where(c => c.取引区分 == 0).ToList();
                    }

                    //親子区分が【子】のデータを除外
                    queryLIST = queryLIST.Where(c => c.親子区分 != "子").ToList();

                    return queryLIST.ToList();
                }

                #endregion

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                #region 売上のみのデータがある場合

                else if (Sales.Count() != 0 && Payment.Count() == 0)
                {
                    var Cord = (from s01 in Sales
                                select new SHR12010_Cord
                                {
                                    取引先ID = s01.取引先ID == null ? 0 : s01.取引先ID,
                                    取引先名 = s01.取引先名 == null ? "なし" : s01.取引先名,
                                    取引区分 = s01.取引区分 == null ? 0 : s01.取引区分,
                                    親子区分 = s01.親子区分 == null ? "なし" : s01.親子区分,
                                    締日 = s01.締日 == null ? 0 : s01.締日,
                                }).Distinct().AsQueryable();


                    //全件表示
                    var query = (from Cor in Cord
                                 join s01 in Sales on Cor.取引先ID equals s01.取引先ID into s01Group
                                 from S01Group in s01Group.DefaultIfEmpty()
                                 group S01Group  by new
                                 {
                                     Cor.取引先ID,
                                     Cor.取引先名,
                                     Cor.取引区分,
                                     Cor.親子区分,
                                     Cor.締日,
                                     S01Group.売上金額,
                                     S01Group.売上消費税,
                                     S01Group.通行料,
                                     S01Group.売上金額計,
                                 } into grGroup

                                 select new SHR12010_Member
                                 {
                                     取引先ID = grGroup.Key.取引先ID,
                                     取引先名 = grGroup.Key.取引先名,
                                     親子区分 = grGroup.Key.親子区分,
                                     締日 = grGroup.Key.締日,
                                     売上金額 = grGroup.Key.売上金額 == null ? 0 : grGroup.Key.売上金額,
                                     売上消費税 = grGroup.Key.売上消費税 == null ? 0 : grGroup.Key.売上消費税,
                                     通行料 = grGroup.Key.通行料 == null ? 0 : grGroup.Key.通行料,
                                     売上金額計 = grGroup.Key.売上金額計 == null ? 0 : grGroup.Key.売上金額計,
                                     傭車金額 = 0,
                                     支払消費税 = 0,
                                     支払通行料 = 0,
                                     傭車金額計 = 0,
                                     売上合計 = grGroup.Key.売上金額計,
                                     支払合計 = 0,
                                     作成年月度 = s作成年月度,
                                     取引先指定 = s取引先From + "～" + s取引先To,
                                     取引先ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "" : 取引先指定ﾋﾟｯｸｱｯﾌﾟ,
                                 }).AsQueryable();

                    if (!(string.IsNullOrEmpty(s取引先From + s取引先To) && i取引先List.Length == 0))
                    {
                        if (string.IsNullOrEmpty(s取引先From + s取引先To))
                        {
                            query = query.Where(c => c.取引先ID >= int.MaxValue);
                        }

                        //取引先From
                        if (!string.IsNullOrEmpty(s取引先From))
                        {
                            int i取引先From = AppCommon.IntParse(s取引先From);
                            query = query.Where(c => c.取引先ID >= i取引先From);
                        }

                        //取引先To
                        if (!string.IsNullOrEmpty(s取引先To))
                        {
                            int i取引先To = AppCommon.IntParse(s取引先To);
                            query = query.Where(c => c.取引先ID <= i取引先To);
                        }

                        //取引先ピックアップ
                        if (i取引先List.Length > 0)
                        {
                            var intCause = i取引先List;
                            //全件表示
                            query = query.Union(from Cor in Cord
                                                join s01 in Sales on Cor.取引先ID equals s01.取引先ID into s01Group
                                                from S01Group in s01Group.DefaultIfEmpty()
                                                group S01Group by new
                                                {
                                                    Cor.取引先ID,
                                                    Cor.取引先名,
                                                    Cor.取引区分,
                                                    Cor.親子区分,
                                                    Cor.締日,
                                                    S01Group.売上金額,
                                                    S01Group.売上消費税,
                                                    S01Group.通行料,
                                                    S01Group.売上金額計,
                                                } into grGroup
                                                where intCause.Contains(grGroup.Key.取引先ID)
                                                select new SHR12010_Member
                                                {
                                                    取引先ID = grGroup.Key.取引先ID,
                                                    取引先名 = grGroup.Key.取引先名,
                                                    親子区分 = grGroup.Key.親子区分,
                                                    締日 = grGroup.Key.締日,
                                                    売上金額 = grGroup.Key.売上金額 == null ? 0 : grGroup.Key.売上金額,
                                                    売上消費税 = grGroup.Key.売上消費税 == null ? 0 : grGroup.Key.売上消費税,
                                                    通行料 = grGroup.Key.通行料 == null ? 0 : grGroup.Key.通行料,
                                                    売上金額計 = grGroup.Key.売上金額計 == null ? 0 : grGroup.Key.売上金額計,
                                                    傭車金額 = 0,
                                                    支払消費税 = 0,
                                                    支払通行料 = 0,
                                                    傭車金額計 = 0,
                                                    売上合計 = grGroup.Key.売上金額計,
                                                    支払合計 = 0,
                                                    作成年月度 = s作成年月度,
                                                    取引先指定 = s取引先From + "～" + s取引先To,
                                                    取引先ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "" : 取引先指定ﾋﾟｯｸｱｯﾌﾟ,
                                                });
                        }
                    }
                    else
                    {
                        //s取引先FromがNULLだった場合
                        if (string.IsNullOrEmpty(s取引先From))
                        {
                            query = query.Where(c => c.取引先ID >= int.MinValue);
                        }

                        //s取引先ToがNullだった場合
                        if (string.IsNullOrEmpty(s取引先To))
                        {
                            query = query.Where(c => c.取引先ID <= int.MaxValue);
                        }

                    }

                    //取引先指定の表示
                    if (i取引先List.Length > 0)
                    {
                        for (int i = 0; i < query.Count(); i++)
                        {
                            取引先指定ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ + i取引先List[i].ToString();

                            if (i < i取引先List.Length)
                            {

                                if (i == i取引先List.Length - 1)
                                {
                                    break;
                                }

                                取引先指定ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ + ",";

                            }

                            if (i取引先List.Length == 1)
                            {
                                break;
                            }

                        }
                    }

                    //作成区分が0だった場合
                    if (i作成区分 == 0)
                    {
                        //売上・支払が発生していないデータを除く
                        query = query.Where(c => c.売上金額計 != 0 || c.傭車金額計 != 0);
                    }


                    //取引区分が1だった場合【全取引】のみのデータを取得
                    if (i取引区分 == 1)
                    {
                        query = query.Where(c => c.取引区分 == 0);
                    }

                    //親子区分が【子】のデータを除外
                    query = query.Where(c => c.親子区分 != "子");

                    return query.ToList();
                }

                #endregion

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                #region 支払のみのデータがある場合

                else if (Sales.Count() == 0 && Payment.Count() != 0)
                {

                    var Cord = (from s02 in Payment
                                select new SHR12010_Cord
                                {
                                    取引先ID = s02.取引先ID == null ? 0 : s02.取引先ID,
                                    取引先名 = s02.取引先名 == null ? "なし" : s02.取引先名,
                                    取引区分 = s02.取引区分 == null ? 0 : s02.取引区分,
                                    親子区分 = s02.親子区分 == null ? "なし" : s02.親子区分,
                                    締日 = s02.締日 == null ? 0 : s02.締日,
                                }).Distinct().AsQueryable();


                    //全件表示
                    var query = (from Cor in Cord
                                 join s02 in Payment on Cor.取引先ID equals s02.取引先ID into s02Group
                                 from S02Group in s02Group.DefaultIfEmpty()
                                 group S02Group by new
                                 {
                                     Cor.取引先ID,
                                     Cor.取引先名,
                                     Cor.取引区分,
                                     Cor.親子区分,
                                     Cor.締日,
                                     S02Group.支払合計,
                                     S02Group.傭車金額,
                                     S02Group.支払消費税,
                                     S02Group.支払通行料,
                                     S02Group.傭車金額計,
                                 } into grGroup

                                 select new SHR12010_Member
                                 {
                                     取引先ID = grGroup.Key.取引先ID,
                                     取引先名 = grGroup.Key.取引先名,
                                     親子区分 = grGroup.Key.親子区分,
                                     締日 = grGroup.Key.締日,
                                     売上金額 = 0,
                                     売上消費税 = 0,
                                     通行料 = 0,
                                     売上金額計 = 0,
                                     傭車金額 = grGroup.Key.傭車金額 == null ? 0 : grGroup.Key.傭車金額,
                                     支払消費税 = grGroup.Key.支払消費税 == null ? 0 : grGroup.Key.支払消費税,
                                     支払通行料 = grGroup.Key.支払通行料 == null ? 0 : grGroup.Key.支払通行料,
                                     傭車金額計 = grGroup.Key.傭車金額計 == null ? 0 : grGroup.Key.傭車金額計,
                                     売上合計 = 0,
                                     支払合計 = grGroup.Key.傭車金額計 == null ? 0 : grGroup.Key.傭車金額計,
                                     作成年月度 = s作成年月度,
                                     取引先指定 = s取引先From + "～" + s取引先To,
                                     取引先ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "" : 取引先指定ﾋﾟｯｸｱｯﾌﾟ,
                                 }).AsQueryable();

                    if (!(string.IsNullOrEmpty(s取引先From + s取引先To) && i取引先List.Length == 0))
                    {
                        if (string.IsNullOrEmpty(s取引先From + s取引先To))
                        {
                            query = query.Where(c => c.取引先ID >= int.MaxValue);
                        }

                        //取引先From
                        if (!string.IsNullOrEmpty(s取引先From))
                        {
                            int i取引先From = AppCommon.IntParse(s取引先From);
                            query = query.Where(c => c.取引先ID >= i取引先From);
                        }

                        //取引先To
                        if (!string.IsNullOrEmpty(s取引先To))
                        {
                            int i取引先To = AppCommon.IntParse(s取引先To);
                            query = query.Where(c => c.取引先ID <= i取引先To);
                        }

                        //取引先ピックアップ
                        if (i取引先List.Length > 0)
                        {
                            var intCause = i取引先List;

                            //全件表示
                            query = query.Union(from Cor in Cord
                                                join s02 in Payment on Cor.取引先ID equals s02.取引先ID into s02Group
                                                from S02Group in s02Group.DefaultIfEmpty()
                                                group S02Group by new
                                                {
                                                    Cor.取引先ID,
                                                    Cor.取引先名,
                                                    Cor.取引区分,
                                                    Cor.親子区分,
                                                    Cor.締日,
                                                    S02Group.支払合計,
                                                    S02Group.傭車金額,
                                                    S02Group.支払消費税,
                                                    S02Group.支払通行料,
                                                    S02Group.傭車金額計,
                                                } into grGroup
                                                where intCause.Contains(grGroup.Key.取引先ID)
                                                select new SHR12010_Member
                                                {
                                                    取引先ID = grGroup.Key.取引先ID,
                                                    取引先名 = grGroup.Key.取引先名,
                                                    親子区分 = grGroup.Key.親子区分,
                                                    締日 = grGroup.Key.締日,
                                                    売上金額 = 0,
                                                    売上消費税 = 0,
                                                    通行料 = 0,
                                                    売上金額計 = 0,
                                                    傭車金額 = grGroup.Key.傭車金額 == null ? 0 : grGroup.Key.傭車金額,
                                                    支払消費税 = grGroup.Key.支払消費税 == null ? 0 : grGroup.Key.支払消費税,
                                                    支払通行料 = grGroup.Key.支払通行料 == null ? 0 : grGroup.Key.支払通行料,
                                                    傭車金額計 = grGroup.Key.傭車金額計 == null ? 0 : grGroup.Key.傭車金額計,
                                                    売上合計 = 0,
                                                    支払合計 = grGroup.Key.傭車金額計 == null ? 0 : grGroup.Key.傭車金額計,
                                                    作成年月度 = s作成年月度,
                                                    取引先指定 = s取引先From + "～" + s取引先To,
                                                    取引先ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "" : 取引先指定ﾋﾟｯｸｱｯﾌﾟ,
                                                });
                        }
                    }
                    else
                    {
                        //s取引先FromがNULLだった場合
                        if (string.IsNullOrEmpty(s取引先From))
                        {
                            query = query.Where(c => c.取引先ID >= int.MinValue);
                        }

                        //s取引先ToがNullだった場合
                        if (string.IsNullOrEmpty(s取引先To))
                        {
                            query = query.Where(c => c.取引先ID <= int.MaxValue);
                        }

                    }

                    //取引先指定の表示
                    if (i取引先List.Length > 0)
                    {
                        for (int i = 0; i < query.Count(); i++)
                        {
                            取引先指定ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ + i取引先List[i].ToString();

                            if (i < i取引先List.Length)
                            {

                                if (i == i取引先List.Length - 1)
                                {
                                    break;
                                }

                                取引先指定ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ + ",";

                            }

                            if (i取引先List.Length == 1)
                            {
                                break;
                            }

                        }
                    }

                    //作成区分が0だった場合
                    if (i作成区分 == 0)
                    {
                        //売上・支払が発生していないデータを除く
                        query = query.Where(c => c.売上金額計 != 0 || c.傭車金額計 != 0);
                    }


                    //取引区分が1だった場合【全取引】のみのデータを取得
                    if (i取引区分 == 1)
                    {
                        query = query.Where(c => c.取引区分 == 0);
                    }

                    //親子区分が【子】のデータを除外
                    query = query.Where(c => c.親子区分 != "子");

                    return query.ToList();
                }

                #endregion

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                else
                {
                    var query = (
                                        from m01 in context.M01_TOK.Where(c => c.削除日付 == null && c.親子区分ID != 3)
                                        from s01 in context.S01_TOKS.Where(c => c.得意先KEY == m01.得意先KEY && c.回数 == 1).DefaultIfEmpty()
                                        from s02 in context.S02_YOSS.Where(c => c.支払先KEY == m01.得意先KEY && c.回数 == 1).DefaultIfEmpty()
                                        group new { m01, s01, s02 } by new
                                        {
                                            m01.得意先ID,
                                            m01.略称名,
                                            m01.取引区分,
                                            m01.親子区分ID,
                                            m01.Ｔ締日
                                        } into grGroup
                                        select new SHR12010_Member
                                        {
                                            取引先ID = grGroup.Key.得意先ID == null ? 0 : grGroup.Key.得意先ID,
                                            取引先名 = grGroup.Key.略称名 == null ? "" : grGroup.Key.略称名,
                                            取引区分 = grGroup.Key.取引区分 == null ? 0 : grGroup.Key.取引区分,
                                            親子区分 = grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ? "親" : "子" == null ? "" : grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ? "親" : "子",
                                            締日 = grGroup.Key.Ｔ締日 == null ? 0 : grGroup.Key.Ｔ締日,
                                            作成年月度 = s作成年月度,
                                            取引先指定 = s取引先From + "～" + s取引先To,
                                            取引先ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "" : 取引先指定ﾋﾟｯｸｱｯﾌﾟ,
                                            売上金額 = 0,
                                            売上消費税 = 0,
                                            通行料 = 0,
                                            売上金額計 = 0,
                                            傭車金額 = 0,
                                            支払消費税 = 0,
                                            支払通行料 = 0,
                                            傭車金額計 = 0,
                                            売上合計 = 0,
                                            支払合計 = 0,

                                        }).Distinct().AsQueryable();
                    //作成区分が0だった場合
                    if (i作成区分 == 0)
                    {
                        //売上・支払が発生していないデータを除く
                        query = query.Where(c => c.売上金額計 != 0 || c.傭車金額計 != 0);
                    }
                    return query.ToList();

                }
            }
        }


    }
}



