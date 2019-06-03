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
    #region 請求用メンバー【SHR13010_Company_Sales】

    /// <summary>
    /// 請求日メンバー
    /// </summary>
    [DataContract]
    public class SHR13010_Company_Sales
    {
        [DataMember]
        public int? コード { get; set; }
        [DataMember]
        public string 取引先名 { get; set; }
        [DataMember]
        public int サイト { get; set; }
        [DataMember]
        public int 集計日付 { get; set; }
        [DataMember]
        public DateTime 請求月 { get; set; }
        [DataMember]
        public int 集金日 { get; set; }
        [DataMember]
        public decimal 通行料 { get; set; }
        [DataMember]
        public decimal 消費税 { get; set; }
        [DataMember]
        public decimal 売上金額 { get; set; }
        [DataMember]
        public decimal 売上金額計 { get; set; }
    }

    #endregion

    #region 支払日メンバー【SHR13010_Company_Payment】

    /// <summary>
    /// 支払日メンバー
    /// </summary>
    [DataContract]
    public class SHR13010_Company_Payment
    {

        [DataMember]
        public int? コードs { get; set; }
        [DataMember]
        public string 取引先名s { get; set; }
        [DataMember]
        public int サイトs { get; set; }
        [DataMember]
        public int 集計日付s { get; set; }
        [DataMember]
        public DateTime 支払月s { get; set; }
        [DataMember]
        public int 集金日s { get; set; }
        [DataMember]
        public decimal 支払通行料 { get; set; }
        [DataMember]
        public decimal 支払消費税 { get; set; }
        [DataMember]
        public decimal 傭車金額 { get; set; }
        [DataMember]
        public decimal 傭車金額計 { get; set; }
    }

    #endregion

    #region 印刷用メンバー【SHR13010_Member】

    /// <summary>
    /// 印刷用メンバー
    /// </summary>
    public class SHR13010_Member
    {
        [DataMember]
        public int? コード { get; set; }
        [DataMember]
        public string 取引先名 { get; set; }
        [DataMember]
        public string 親子区分 { get; set; }
        [DataMember]
        public decimal 売上 { get; set; }
        [DataMember]
        public decimal 支払 { get; set; }
        [DataMember]
        public int? 請求月 { get; set; }
        [DataMember]
        public decimal 売上金額 { get; set; }
        [DataMember]
        public decimal 消費税 { get; set; }
        [DataMember]
        public decimal 通行料 { get; set; }
        [DataMember]
        public decimal 売上金額計 { get; set; }
        [DataMember]
        public int? 支払月 { get; set; }
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
        public int 取引区分 { get; set; }
        [DataMember]
        public int 集金日 { get; set; }
        [DataMember]
        public int サイト { get; set; }
        [DataMember]
        public int S集金日 { get; set; }
        [DataMember]
        public int サイトs { get; set; }
        [DataMember]
        public string 取引先指定 { get; set; }
        [DataMember]
        public string 取引先ﾋﾟｯｸｱｯﾌﾟ { get; set; }
       
    }

    #endregion


    public class SHR13010
    {

        public List<SHR13010_Member> SHR13010_GetDataHinList(string s取引先From, string s取引先To, int?[] i取引先List, int i作成年, int i作成月, DateTime d集計月 , string s作成年月度 , int i作成区分, int i取引区分)
        {
            string 取引先指定ﾋﾟｯｸｱｯﾌﾟ = string.Empty;

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<SHR13010_Member> retList = new List<SHR13010_Member>();
                context.Connection.Open();

                //売上データ【全件抽出】
                var query = (from m01 in context.M01_TOK.Where(c => c.削除日付 == null && c.親子区分ID != 3)
                             join s01 in context.S01_TOKS.Where(c => c.回数 == 1) on m01.得意先KEY equals s01.得意先KEY into Group
                             from s01Group in Group
                             group new { s01Group, m01 } by new { m01.得意先ID, m01.略称名, m01.Ｔサイト日, m01.Ｔ集金日, s01Group.集計年月 } into grGroup
                             select new SHR13010_Company_Sales
                             {
                                 コード = grGroup.Key.得意先ID,
                                 取引先名 = grGroup.Key.略称名,
                                 サイト = grGroup.Key.Ｔサイト日,
                                 集計日付 = grGroup.Key.集計年月,
                                 集金日 = grGroup.Key.Ｔ集金日,
                                 通行料 = grGroup.Sum(c => c.s01Group.締日通行料),
                                 消費税 = grGroup.Sum(c => c.s01Group.締日消費税),
                                 売上金額 = grGroup.Sum(c => c.s01Group.締日売上金額),
                                 売上金額計 = grGroup.Sum(c => c.s01Group.締日売上金額 + c.s01Group.締日消費税 + c.s01Group.締日通行料),
                             }).ToList();

                //支払データ【全件抽出】
                var query2 = (from m01 in context.M01_TOK.Where(c => c.削除日付 == null && c.親子区分ID != 3)
                              join s02 in context.S02_YOSS.Where(c => c.回数 == 1) on m01.得意先KEY equals s02.支払先KEY into Group
                              from s02Group in Group
                              group new { m01, s02Group } by new { m01.得意先ID, m01.略称名, m01.Ｓサイト日, m01.Ｓ集金日, s02Group.集計年月 } into grGroup
                              select new SHR13010_Company_Payment
                              {
                                  コードs = grGroup.Key.得意先ID,
                                  取引先名s = grGroup.Key.略称名,
                                  サイトs = grGroup.Key.Ｓサイト日,
                                  集計日付s = grGroup.Key.集計年月,
                                  集金日s = grGroup.Key.Ｓ集金日,
                                  支払通行料 = grGroup.Sum(c => c.s02Group.締日通行料),
                                  支払消費税 = grGroup.Sum(c => c.s02Group.締日消費税),
                                  傭車金額 = grGroup.Sum(c => c.s02Group.締日売上金額),
                                  傭車金額計 = grGroup.Sum(c => c.s02Group.締日売上金額 + c.s02Group.締日消費税 + c.s02Group.締日通行料),
                              }).ToList();

                //集計日付に会社ごとのサイト数を計算し【請求月】を算出
                for (int i = 0; i < query.Count; i++)
                {
                    if (Convert.ToDateTime((query[i].集計日付).ToString().Substring(0, 4) + "/" + query[i].集計日付.ToString().Substring(4, 2) + "/01").AddMonths(query[i].サイト) == d集計月)
                    {
                        //サイト数を計算したデータと入力されたデータが同じなら挿入
                        query[i].請求月 = Convert.ToDateTime((query[i].集計日付).ToString().Substring(0, 4) + "/" + query[i].集計日付.ToString().Substring(4, 2) + "/01").AddMonths(query[i].サイト);
                    }
                    else
                    {
                        //集計の必要のないデータには【1111年11月11日】を挿入
                        query[i].請求月 = Convert.ToDateTime("1111/11/11");
                    }
                };


                //集計日付に会社ごとのサイト数を計算し【支払月】を算出
                for (int i = 0; i < query2.Count; i++)
                {
                    if (Convert.ToDateTime((query2[i].集計日付s).ToString().Substring(0, 4) + "/" + query2[i].集計日付s.ToString().Substring(4, 2) + "/01").AddMonths(query2[i].サイトs) == d集計月)
                    {
                        //サイト数を計算したデータと入力されたデータが同じなら挿入
                        query2[i].支払月s = Convert.ToDateTime((query2[i].集計日付s).ToString().Substring(0, 4) + "/" + query2[i].集計日付s.ToString().Substring(4, 2) + "/01").AddMonths(query2[i].サイトs);
                    }
                    else
                    {
                        //集計の必要のないデータには【1111年11月11日】を挿入
                        query2[i].支払月s = Convert.ToDateTime("1111/11/11");
                    }
                };

                //必要のないデータを変数化し【RemoveAll】で取り除く
                DateTime Value = Convert.ToDateTime("1111/11/11");
                query.RemoveAll(c => c.請求月 == Value);
                query2.RemoveAll(c => c.支払月s == Value);

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                #region 売上と支払　両方ともデータがあるとき
                if (query.Count() != 0 && query2.Count() != 0)
                {
                    //2つのクエリの【和集合】を求める
                    var retquery = (from s01 in query select s01.コード).Union(
                                    from s02 in query2 select s02.コードs).AsQueryable();

                    //【売上】と【支払】データを1つにまとめる
                    var join = (from ret in retquery
                                from m01 in context.M01_TOK.Where(c => c.削除日付 == null && c.親子区分ID != 3)
                                from q01 in query.Where(c => c.コード == m01.得意先ID).DefaultIfEmpty()
                                from q02 in query2.Where(c => c.コードs == m01.得意先ID).DefaultIfEmpty()
                                group new { m01, q01, q02 } by
                                new
                                {
                                    m01.得意先ID,
                                    m01.略称名,
                                    m01.Ｔサイト日,
                                    m01.Ｓサイト日,
                                    m01.Ｔ集金日,
                                    m01.Ｓ集金日,
                                    m01.親子区分ID,
                                    m01.取引区分,
                                } into grGroup
                                select new SHR13010_Member
                                {
                                    コード = grGroup.Key.得意先ID == null ? 0 : grGroup.Key.得意先ID,
                                    取引先名 = grGroup.Key.略称名 == null ? "" : grGroup.Key.略称名,
                                    サイト = grGroup.Key.Ｔサイト日 == null ? 0 : grGroup.Key.Ｔサイト日,
                                    サイトs = grGroup.Key.Ｓサイト日 == null ? 0 : grGroup.Key.Ｓサイト日,
                                    集金日 = grGroup.Key.Ｔ集金日 == null ? 0 : grGroup.Key.Ｔ集金日,
                                    S集金日 = grGroup.Key.Ｓ集金日 == null ? 0 : grGroup.Key.Ｓ集金日,
                                    親子区分 = grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ? "親" : "子" == null ? "" : grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ? "親" : "子",
                                    取引区分 = grGroup.Key.取引区分 == null ? 10 : grGroup.Key.取引区分,
                                    作成年月度 = s作成年月度 == null ? "" : s作成年月度,
                                    取引先指定 = s取引先From + "～" + s取引先To,
                                    取引先ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "" : 取引先指定ﾋﾟｯｸｱｯﾌﾟ,
                                }).AsQueryable();




                    //＜＜＜データの絞込み＞＞＞
                    if (!(string.IsNullOrEmpty(s取引先From + s取引先To) && i取引先List.Length == 0))
                    {
                        if (string.IsNullOrEmpty(s取引先From + s取引先To))
                        {
                            join = join.Where(c => c.コード >= int.MaxValue);
                        }

                        if (!string.IsNullOrEmpty(s取引先From))
                        {
                            int i取引先From = AppCommon.IntParse(s取引先From);
                            join = join.Where(c => c.コード >= i取引先From);
                        }

                        if (!string.IsNullOrEmpty(s取引先To))
                        {
                            int i取引先To = AppCommon.IntParse(s取引先To);
                            join = join.Where(c => c.コード <= i取引先To);
                        }

                        if (i取引先List.Length > 0)
                        {
                            var intCouse = i取引先List;
                            join = join.Union(from ret in retquery
                                              from m01 in context.M01_TOK.Where(c => c.削除日付 == null && c.親子区分ID != 3)
                                              from q01 in query.Where(c => c.コード == m01.得意先ID).DefaultIfEmpty()
                                              from q02 in query2.Where(c => c.コードs == m01.得意先ID).DefaultIfEmpty()
                                              group new { m01, q01, q02 } by
                                              new
                                              {
                                                  m01.得意先ID,
                                                  m01.略称名,
                                                  m01.Ｔサイト日,
                                                  m01.Ｓサイト日,
                                                  m01.Ｔ集金日,
                                                  m01.Ｓ集金日,
                                                  m01.親子区分ID,
                                                  m01.取引区分,
                                              } into grGroup
                                              where intCouse.Contains(grGroup.Key.得意先ID)
                                              select new SHR13010_Member
                                              {
                                                  コード = grGroup.Key.得意先ID == null ? 0 : grGroup.Key.得意先ID,
                                                  取引先名 = grGroup.Key.略称名 == null ? "" : grGroup.Key.略称名,
                                                  サイト = grGroup.Key.Ｔサイト日 == null ? 0 : grGroup.Key.Ｔサイト日,
                                                  サイトs = grGroup.Key.Ｓサイト日 == null ? 0 : grGroup.Key.Ｓサイト日,
                                                  集金日 = grGroup.Key.Ｔ集金日 == null ? 0 : grGroup.Key.Ｔ集金日,
                                                  S集金日 = grGroup.Key.Ｓ集金日 == null ? 0 : grGroup.Key.Ｓ集金日,
                                                  親子区分 = grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ? "親" : "子" == null ? "" : grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ? "親" : "子",
                                                  取引区分 = grGroup.Key.取引区分 == null ? 10 : grGroup.Key.取引区分,
                                                  作成年月度 = s作成年月度 == null ? "" : s作成年月度,
                                                  取引先指定 = s取引先From + "～" + s取引先To,
                                                  取引先ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "" : 取引先指定ﾋﾟｯｸｱｯﾌﾟ,
                                              }).OrderBy(c => c.コード).AsQueryable();

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
                        if (string.IsNullOrEmpty(s取引先From))
                        {
                            join = join.Where(c => c.コード >= int.MinValue);
                        }

                        if (string.IsNullOrEmpty(s取引先To))
                        {
                            join = join.Where(c => c.コード <= int.MaxValue);
                        }
                    }

                    //【売上】・【支払】・【売上/支払】データをList化
                    List<SHR13010_Member> LIST = join.ToList();
                    List<SHR13010_Company_Sales> SalesList = query.ToList();
                    List<SHR13010_Company_Payment> PaymentList = query2.ToList();
                    List<SHR13010_Member> queryLIST = new List<SHR13010_Member>();

                    int? name = 0;
                    foreach (var data in LIST)
                    {
                        if (name != data.コード)
                        {
                            queryLIST.Add(data);
                        }
                        name = data.コード;
                    }

                    //売上と支払を1つにしたデータをLoop
                    for (int i = 0; i < queryLIST.Count(); i++)
                    {
                        //QueryListに当てはまる【売上】データがある場合、データ挿入
                        for (int x = 0; x < SalesList.Count(); x++)
                        {
                            if (queryLIST[i].コード == SalesList[x].コード)
                            {
                                queryLIST[i].請求月 = SalesList[x].集計日付;
                                queryLIST[i].通行料 = SalesList[x].通行料;
                                queryLIST[i].消費税 = SalesList[x].消費税;
                                queryLIST[i].売上金額 = SalesList[x].売上金額;
                                queryLIST[i].売上金額計 = SalesList[x].売上金額計;
                            }
                            else
                            {
                                continue;
                            }
                        }

                        //QueryListに当てはまる【支払】データがある場合、データ挿入
                        for (int y = 0; y < PaymentList.Count(); y++)
                        {
                            if (queryLIST[i].コード == PaymentList[y].コードs)
                            {
                                queryLIST[i].支払月 = PaymentList[y].集計日付s;
                                queryLIST[i].支払通行料 = PaymentList[y].支払通行料;
                                queryLIST[i].支払消費税 = PaymentList[y].支払消費税;
                                queryLIST[i].傭車金額 = PaymentList[y].傭車金額;
                                queryLIST[i].傭車金額計 = PaymentList[y].傭車金額計;
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
                            queryLIST[i].売上 = queryLIST[i].売上金額計 - queryLIST[i].傭車金額計;
                        }
                        else if (queryLIST[i].売上金額計 < queryLIST[i].傭車金額計)
                        {
                            queryLIST[i].支払 = queryLIST[i].傭車金額計 - queryLIST[i].売上金額計;
                        }
                        else
                        {
                            continue;
                        }
                    }



                    //取引ありのみ表示
                    if (i作成区分 == 0)
                    {
                        queryLIST = queryLIST.Where(c => c.売上金額計 != 0 || c.傭車金額計 != 0).ToList();
                    }
                    //【全取引】の企業のみ
                    if (i取引区分 == 1)
                    {
                        queryLIST = queryLIST.Where(c => c.取引区分 == 0).ToList();
                    }

                    //親子区分【子】を非表示に
                    queryLIST = queryLIST.Where(c => c.親子区分 != "子").ToList();

                    //表示順序変更
                    queryLIST = queryLIST.OrderBy(c => c.コード).ToList();

                    return queryLIST.ToList();

                }
                #endregion

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                #region 売上データのみ
                else if (query.Count() != 0 && query2.Count() == 0)
                {
                    var retquery = (from s01 in query select s01.コード).Union(
                from s02 in query2 select s02.コードs).Distinct().AsQueryable();

                    var join = (from ret in retquery
                                from m01 in context.M01_TOK.Where(c => c.削除日付 == null && c.親子区分ID != 3)
                                from q01 in query.Where(c => c.コード == m01.得意先ID).DefaultIfEmpty()
                                group new { m01, q01 } by
                                new
                                {
                                    m01.得意先ID,
                                    m01.略称名,
                                    m01.Ｔサイト日,
                                    m01.Ｓサイト日,
                                    m01.Ｔ集金日,
                                    m01.Ｓ集金日,
                                    m01.親子区分ID,
                                    m01.取引区分,
                                } into grGroup
                                select new SHR13010_Member
                                {
                                    コード = grGroup.Key.得意先ID,
                                    取引先名 = grGroup.Key.略称名,
                                    サイト = grGroup.Key.Ｔサイト日,
                                    サイトs = grGroup.Key.Ｓサイト日,
                                    集金日 = grGroup.Key.Ｔ集金日,
                                    S集金日 = grGroup.Key.Ｔ集金日,
                                    請求月 = query.Where(c => c.コード == grGroup.Key.得意先ID).Sum(c => c.集計日付) == 0 ? 0 : query.Where(c => c.コード == grGroup.Key.得意先ID).Distinct().Sum(c => c.集計日付),
                                    支払月 = 0,
                                    通行料 = query.Where(c => c.コード == grGroup.Key.得意先ID).Sum(c => c.通行料) == 0 ? 0 : query.Where(c => c.コード == grGroup.Key.得意先ID).Distinct().Sum(c => c.通行料),
                                    支払通行料 = 0,
                                    消費税 = query.Where(c => c.コード == grGroup.Key.得意先ID).Sum(c => c.消費税) == 0 ? 0 : query.Where(c => c.コード == grGroup.Key.得意先ID).Distinct().Sum(c => c.消費税),
                                    支払消費税 = 0,
                                    売上金額 = query.Where(c => c.コード == grGroup.Key.得意先ID).Sum(c => c.売上金額) == 0 ? 0 : query.Where(c => c.コード == grGroup.Key.得意先ID).Distinct().Sum(c => c.売上金額),
                                    傭車金額 = 0,
                                    売上金額計 = query.Where(c => c.コード == grGroup.Key.得意先ID).Sum(c => c.売上金額計) == 0 ? 0 : query.Where(c => c.コード == grGroup.Key.得意先ID).Distinct().Sum(c => c.売上金額計),
                                    傭車金額計 = 0,
                                    売上 = query.Where(c => c.コード == grGroup.Key.得意先ID).Sum(c => c.売上金額計) == 0 ? 0 : query.Where(c => c.コード == grGroup.Key.得意先ID).Distinct().Sum(c => c.売上金額計),
                                    支払 = 0,
                                    親子区分 = grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ? "親" : "子",
                                    取引区分 = grGroup.Key.取引区分,
                                    作成年月度 = s作成年月度,
                                    取引先指定 = s取引先From + "～" + s取引先To,
                                    取引先ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "" : 取引先指定ﾋﾟｯｸｱｯﾌﾟ,
                                }).AsQueryable();

                    //＜＜＜データの絞込み＞＞＞
                    if (!(string.IsNullOrEmpty(s取引先From + s取引先To) && i取引先List.Length == 0))
                    {
                        if (string.IsNullOrEmpty(s取引先From + s取引先To))
                        {
                            join = join.Where(c => c.コード >= int.MaxValue);
                        }

                        if (!string.IsNullOrEmpty(s取引先From))
                        {
                            int i取引先From = AppCommon.IntParse(s取引先From);
                            join = join.Where(c => c.コード >= i取引先From);
                        }

                        if (!string.IsNullOrEmpty(s取引先To))
                        {
                            int i取引先To = AppCommon.IntParse(s取引先To);
                            join = join.Where(c => c.コード <= i取引先To);
                        }

                        if (i取引先List.Length > 0)
                        {
                            var intCouse = i取引先List;
                            join = join.Union(from ret in retquery
                                              from m01 in context.M01_TOK.Where(c => c.削除日付 == null && c.親子区分ID != 3)
                                              from q01 in query.Where(c => c.コード == m01.得意先ID).DefaultIfEmpty()
                                              group new { m01, q01 } by
                                              new
                                              {
                                                  m01.得意先ID,
                                                  m01.略称名,
                                                  m01.Ｔサイト日,
                                                  m01.Ｓサイト日,
                                                  m01.Ｔ集金日,
                                                  m01.Ｓ集金日,
                                                  m01.親子区分ID,
                                                  m01.取引区分,
                                              } into grGroup
                                              where intCouse.Contains(grGroup.Key.得意先ID)
                                              select new SHR13010_Member
                                              {
                                                  コード = grGroup.Key.得意先ID,
                                                  取引先名 = grGroup.Key.略称名,
                                                  サイト = grGroup.Key.Ｔサイト日,
                                                  サイトs = grGroup.Key.Ｓサイト日,
                                                  集金日 = grGroup.Key.Ｔ集金日,
                                                  S集金日 = grGroup.Key.Ｔ集金日,
                                                  請求月 = query.Where(c => c.コード == grGroup.Key.得意先ID).Sum(c => c.集計日付) == 0 ? 0 : query.Where(c => c.コード == grGroup.Key.得意先ID).Distinct().Sum(c => c.集計日付),
                                                  支払月 = 0,
                                                  通行料 = query.Where(c => c.コード == grGroup.Key.得意先ID).Sum(c => c.通行料) == 0 ? 0 : query.Where(c => c.コード == grGroup.Key.得意先ID).Distinct().Sum(c => c.通行料),
                                                  支払通行料 = 0,
                                                  消費税 = query.Where(c => c.コード == grGroup.Key.得意先ID).Sum(c => c.消費税) == 0 ? 0 : query.Where(c => c.コード == grGroup.Key.得意先ID).Distinct().Sum(c => c.消費税),
                                                  支払消費税 = 0,
                                                  売上金額 = query.Where(c => c.コード == grGroup.Key.得意先ID).Sum(c => c.売上金額) == 0 ? 0 : query.Where(c => c.コード == grGroup.Key.得意先ID).Distinct().Sum(c => c.売上金額),
                                                  傭車金額 = 0,
                                                  売上金額計 = query.Where(c => c.コード == grGroup.Key.得意先ID).Sum(c => c.売上金額計) == 0 ? 0 : query.Where(c => c.コード == grGroup.Key.得意先ID).Distinct().Sum(c => c.売上金額計),
                                                  傭車金額計 = 0,
                                                  売上 = query.Where(c => c.コード == grGroup.Key.得意先ID).Sum(c => c.売上金額計) == 0 ? 0 : query.Where(c => c.コード == grGroup.Key.得意先ID).Distinct().Sum(c => c.売上金額計),
                                                  支払 = 0,
                                                  親子区分 = grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ? "親" : "子",
                                                  取引区分 = grGroup.Key.取引区分,
                                                  作成年月度 = s作成年月度,
                                                  取引先指定 = s取引先From + "～" + s取引先To,
                                                  取引先ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "" : 取引先指定ﾋﾟｯｸｱｯﾌﾟ,
                                              });

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
                        if (string.IsNullOrEmpty(s取引先From))
                        {
                            join = join.Where(c => c.コード >= int.MinValue);
                        }

                        if (string.IsNullOrEmpty(s取引先To))
                        {
                            join = join.Where(c => c.コード <= int.MaxValue);
                        }
                    }


                    //取引ありのみ表示
                    if (i作成区分 == 0)
                    {
                        join = join.Where(c => c.売上金額計 != 0 || c.傭車金額計 != 0).Distinct();
                    }

                    //【全取引】の企業のみ
                    if (i取引区分 == 1)
                    {
                        join = join.Where(c => c.取引区分 == 0).Distinct();
                    }

                    //親子区分【子】を非表示に
                    join = join.Where(c => c.親子区分 != "子");

                    //表示順序変更
                    join = join.OrderBy(c => c.コード);

                    return join.ToList();
                }
                #endregion

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                #region 支払データのみ

                else if (query.Count() == 0 && query2.Count() != 0)
                {
                    var retquery = (from s01 in query select s01.コード).Union(
                from s02 in query2 select s02.コードs).AsQueryable();


                    var join = (from ret in retquery
                                from m01 in context.M01_TOK.Where(c => c.削除日付 == null && c.親子区分ID != 3)
                                from q02 in query2.Where(c => c.コードs == m01.得意先ID).DefaultIfEmpty()
                                group new { m01, q02 } by
                                new
                                {
                                    m01.得意先ID,
                                    m01.略称名,
                                    m01.Ｔサイト日,
                                    m01.Ｓサイト日,
                                    m01.Ｔ集金日,
                                    m01.Ｓ集金日,
                                    m01.親子区分ID,
                                    m01.取引区分,
                                } into grGroup
                                select new SHR13010_Member
                                {
                                    コード = grGroup.Key.得意先ID,
                                    取引先名 = grGroup.Key.略称名,
                                    サイト = grGroup.Key.Ｔサイト日,
                                    サイトs = grGroup.Key.Ｓサイト日,
                                    集金日 = grGroup.Key.Ｔ集金日,
                                    S集金日 = grGroup.Key.Ｔ集金日,
                                    請求月 = 0,
                                    支払月 = query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.集計日付s) == 0 ? 0 : query2.Where(c => c.コードs == grGroup.Key.得意先ID).Distinct().Sum(c => c.集計日付s),
                                    通行料 = 0,
                                    支払通行料 = query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.支払通行料) == 0 ? 0 : query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.支払通行料),
                                    消費税 = 0,
                                    支払消費税 = query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.支払消費税) == 0 ? 0 : query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.支払消費税),
                                    売上金額 = 0,
                                    傭車金額 = query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.傭車金額) == 0 ? 0 : query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.傭車金額),
                                    売上金額計 = 0,
                                    傭車金額計 = query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.傭車金額計) == 0 ? 0 : query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.傭車金額計),
                                    売上 = 0,
                                    支払 = query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.傭車金額計) == 0 ? 0 : query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.傭車金額計),
                                    親子区分 = grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ? "親" : "子" == null ? "" : grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ? "親" : "子",
                                    取引区分 = grGroup.Key.取引区分 == null ? 10 : grGroup.Key.取引区分,
                                    作成年月度 = s作成年月度,
                                    取引先指定 = s取引先From + "～" + s取引先To,
                                    取引先ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "" : 取引先指定ﾋﾟｯｸｱｯﾌﾟ,
                                }).AsQueryable().Distinct();

                    //＜＜＜データの絞込み＞＞＞
                    if (!(string.IsNullOrEmpty(s取引先From + s取引先To) && i取引先List.Length == 0))
                    {
                        if (string.IsNullOrEmpty(s取引先From + s取引先To))
                        {
                            join = join.Where(c => c.コード >= int.MaxValue);
                        }

                        if (!string.IsNullOrEmpty(s取引先From))
                        {
                            int i取引先From = AppCommon.IntParse(s取引先From);
                            join = join.Where(c => c.コード >= i取引先From);
                        }

                        if (!string.IsNullOrEmpty(s取引先To))
                        {
                            int i取引先To = AppCommon.IntParse(s取引先To);
                            join = join.Where(c => c.コード <= i取引先To);
                        }

                        if (i取引先List.Length > 0)
                        {
                            var intCouse = i取引先List;
                            join = join.Union(from ret in retquery
                                              from m01 in context.M01_TOK.Where(c => c.削除日付 == null && c.親子区分ID != 3)
                                              from q02 in query2.Where(c => c.コードs == m01.得意先ID).DefaultIfEmpty()
                                              group new { m01, q02 } by
                                              new
                                              {
                                                  m01.得意先ID,
                                                  m01.略称名,
                                                  m01.Ｔサイト日,
                                                  m01.Ｓサイト日,
                                                  m01.Ｔ集金日,
                                                  m01.Ｓ集金日,
                                                  m01.親子区分ID,
                                                  m01.取引区分,
                                              } into grGroup
                                              where intCouse.Contains(grGroup.Key.得意先ID)
                                              select new SHR13010_Member
                                              {
                                                  コード = grGroup.Key.得意先ID,
                                                  取引先名 = grGroup.Key.略称名,
                                                  サイト = grGroup.Key.Ｔサイト日,
                                                  サイトs = grGroup.Key.Ｓサイト日,
                                                  集金日 = grGroup.Key.Ｔ集金日,
                                                  S集金日 = grGroup.Key.Ｔ集金日,
                                                  請求月 = 0,
                                                  支払月 = query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.集計日付s) == 0 ? 0 : query2.Where(c => c.コードs == grGroup.Key.得意先ID).Distinct().Sum(c => c.集計日付s),
                                                  通行料 = 0,
                                                  支払通行料 = query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.支払通行料) == 0 ? 0 : query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.支払通行料),
                                                  消費税 = 0,
                                                  支払消費税 = query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.支払消費税) == 0 ? 0 : query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.支払消費税),
                                                  売上金額 = 0,
                                                  傭車金額 = query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.傭車金額) == 0 ? 0 : query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.傭車金額),
                                                  売上金額計 = 0,
                                                  傭車金額計 = query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.傭車金額計) == 0 ? 0 : query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.傭車金額計),
                                                  売上 = 0,
                                                  支払 = query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.傭車金額計) == 0 ? 0 : query2.Where(c => c.コードs == grGroup.Key.得意先ID).Sum(c => c.傭車金額計),
                                                  親子区分 = grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ? "親" : "子" == null ? "" : grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ? "親" : "子",
                                                  取引区分 = grGroup.Key.取引区分 == null ? 10 : grGroup.Key.取引区分,
                                                  作成年月度 = s作成年月度,
                                                  取引先指定 = s取引先From + "～" + s取引先To,
                                                  取引先ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "" : 取引先指定ﾋﾟｯｸｱｯﾌﾟ,
                                              });

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
                        if (string.IsNullOrEmpty(s取引先From))
                        {
                            join = join.Where(c => c.コード >= int.MinValue);
                        }

                        if (string.IsNullOrEmpty(s取引先To))
                        {
                            join = join.Where(c => c.コード <= int.MaxValue);
                        }
                    }


                    //取引ありのみ表示
                    if (i作成区分 == 0)
                    {
                        join = join.Where(c => c.売上金額計 != 0 || c.傭車金額計 != 0);
                    }

                    //【全取引】の企業のみ
                    if (i取引区分 == 1)
                    {
                        join = join.Where(c => c.取引区分 == 0);
                    }

                    //親子区分【子】を非表示に
                    join = join.Where(c => c.親子区分 != "子");

                    //表示順序変更
                    join = join.OrderBy(c => c.コード);

                    return join.ToList();
                }
                #endregion

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                #region データ無し

                else
                {
                    var join = (from m01 in context.M01_TOK.Where(c => c.削除日付 == null && c.親子区分ID != 3)
                                from s01 in context.S01_TOKS.Where(c => c.得意先KEY == m01.得意先KEY && c.回数 == 1).DefaultIfEmpty()
                                from s02 in context.S02_YOSS.Where(c => c.支払先KEY == m01.得意先KEY && c.回数 == 1).DefaultIfEmpty()
                                group new { m01, s01, s02 } by new
                                {
                                    m01.得意先ID,
                                    m01.略称名,
                                    m01.Ｔサイト日,
                                    m01.Ｓサイト日,
                                    m01.Ｔ集金日,
                                    m01.Ｓ集金日,
                                    m01.親子区分ID,
                                    m01.取引区分,
                                } into grGroup
                                select new SHR13010_Member
                                {
                                    コード = grGroup.Key.得意先ID,
                                    取引先名 = grGroup.Key.略称名,
                                    サイト = grGroup.Key.Ｔサイト日,
                                    サイトs = grGroup.Key.Ｓサイト日,
                                    集金日 = grGroup.Key.Ｔ集金日,
                                    S集金日 = grGroup.Key.Ｔ集金日,
                                    請求月 = 0,
                                    支払月 = 0,
                                    通行料 = 0,
                                    支払通行料 = 0,
                                    消費税 = 0,
                                    支払消費税 = 0,
                                    売上金額 = 0,
                                    傭車金額 = 0,
                                    売上金額計 = 0,
                                    傭車金額計 = 0,
                                    売上 = 0,
                                    支払 = 0,
                                    親子区分 = grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ? "親" : "子" == null ? "" : grGroup.Key.親子区分ID == 0 ? "" : grGroup.Key.親子区分ID == 1 ? "親" : grGroup.Key.親子区分ID == 2 ? "親" : "子",
                                    取引区分 = grGroup.Key.取引区分 == null ? 10 : grGroup.Key.取引区分,
                                    作成年月度 = s作成年月度,
                                    取引先指定 = s取引先From + "～" + s取引先To,
                                    取引先ﾋﾟｯｸｱｯﾌﾟ = 取引先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "" : 取引先指定ﾋﾟｯｸｱｯﾌﾟ,

                                }).Distinct().AsQueryable();

                #endregion


                    //作成区分が0だった場合
                    if (i作成区分 == 0)
                    {
                        //売上・支払が発生していないデータを除く
                        join = join.Where(c => c.売上金額計 != 0 || c.傭車金額計 != 0);
                    }
                    return join.ToList();

                }

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            }
        }


    }
}




