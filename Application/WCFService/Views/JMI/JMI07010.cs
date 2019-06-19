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
    /// JMI07010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class JMI07010_Member
    {
        public int コード { get; set; }
        public string 乗務員名 { get; set; }
        public int 売上金額 { get; set; }
        public int? 通行料 { get; set; }
        public int? 売上合計 { get; set; }
		public int? 社内金額 { get; set; }
		public int? 社内立替 { get; set; }
		public decimal 歩合金額 { get; set; }
        public int? 経費合計 { get; set; }
        public decimal? 拘束H { get; set; }
        public decimal? 運転H { get; set; }
        public decimal? 高速H { get; set; }
        public decimal? 作業H { get; set; }
        public decimal? 待機H { get; set; }
        public decimal? 休憩H { get; set; }
        public decimal? 残業H { get; set; }
        public decimal? 深夜H { get; set; }
        public int? 走行KM { get; set; }
        public int? 実車KM { get; set; }
        public DateTime 期間From { get; set; }
        public DateTime 期間To { get; set; }
        public string コードFrom { get; set; }
        public string コードTo { get; set; }
        public string コードList { get; set; }
        public string かな読み { get; set; }
        public string 表示順 { get; set; }

    }


    /// <summary>
    /// JMI07010  CSV　メンバー
    /// </summary>
    [DataContract]
    public class JMI07010_Member_CSV
    {
        public int コード { get; set; }
        public string 乗務員名 { get; set; }
        public string かな読み { get; set; }
        public int 売上金額 { get; set; }
        public int? 通行料 { get; set; }
        public int? 売上合計 { get; set; }
		public int? 社内金額 { get; set; }
		public int? 社内立替 { get; set; }
		public decimal 歩合金額 { get; set; }
        public int? 経費合計 { get; set; }
        public decimal? 拘束H { get; set; }
        public decimal? 運転H { get; set; }
        public decimal? 高速H { get; set; }
        public decimal? 作業H { get; set; }
        public decimal? 待機H { get; set; }
        public decimal? 休憩H { get; set; }
        public decimal? 残業H { get; set; }
        public decimal? 深夜H { get; set; }
        public int? 走行KM { get; set; }
        public int? 実車KM { get; set; }
    }


    [DataContract]
    public class JMI07010_M07
    {
        [DataMember]
        public int 経費ID { get; set; }
        [DataMember]
        public string 経費名 { get; set; }
    }
    

    public class JMI07010
    {
        #region 印刷
        /// <summary>
        /// JMI07010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<JMI07010_Member> GetDataList(string p乗務員From, string p乗務員To, int?[] i乗務員List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string s乗務員List, int i表示順 )
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<JMI07010_Member> retList = new List<JMI07010_Member>();
                context.Connection.Open();

                int[] lst;
                lst = (from m04 in context.M04_DRV
                       let t01l = from t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) select t01.乗務員KEY
                       let t02l = from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) select t02.乗務員KEY
                       let t03l = from t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) select t03.乗務員KEY
                       where t01l.Contains(m04.乗務員KEY) || t02l.Contains(m04.乗務員KEY) || t03l.Contains(m04.乗務員KEY)
                       select m04.乗務員KEY).ToArray();

                var query2 = (from t in context.T03_KTRN
                              join m07 in context.M07_KEI on t.経費項目ID equals m07.経費項目ID
                              where m07.固定変動区分 == 1 && t.経費発生日 >= d集計期間From && t.経費発生日 <= d集計期間To
                              select new JMI06010_KTRN
                              {
                                  日付 = t.経費発生日,
                                  経費項目ID = t.経費項目ID,
                                  金額 = t.金額,
                                  乗務員KEY = t.乗務員KEY,
                              }).AsQueryable();

                var query = (from m04 in context.M04_DRV
                             join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) on m04.乗務員KEY equals t01.乗務員KEY into t01Group
                             join t02 in context.V_T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on m04.乗務員KEY equals t02.乗務員KEY into t02Group
                             join t03 in query2.Where(t03 => t03.日付 >= d集計期間From && t03.日付 <= d集計期間To) on m04.乗務員KEY equals t03.乗務員KEY into t03Group
                             where t01Group.Where(t01 => t01.乗務員KEY == m04.乗務員KEY).Any() == true || t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Any() == true ||
                                                t03Group.Where(t03 => t03.乗務員KEY == m04.乗務員KEY).Any() == true
                             //from m78Group in sm78.DefaultIfEmpty()

                             select new JMI07010_Member
                             {

                                 コード = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 売上金額 = t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２) == null ? 0 : t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２),
                                 通行料 = t01Group.Sum(t01 => t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.通行料),
                                 売上合計 = t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
								 社内金額 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額),
								 社内立替 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払通行料) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払通行料),
								 歩合金額 = Math.Round(t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * (m04.歩合率 / 100)) == null ? 0 : Math.Round(t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * (m04.歩合率 / 100)),
                                 経費合計 = t03Group.Sum(t03 => t03.金額) == null ? 0 : t03Group.Sum(t03 => t03.金額),
                                 拘束H = t02Group.Sum(t02 => t02.拘束時間) == null ? 0 : t02Group.Sum(t02 => t02.拘束時間),
                                 運転H = t02Group.Sum(t02 => t02.運転時間) == null ? 0 : t02Group.Sum(t02 => t02.運転時間),
                                 高速H = t02Group.Sum(t02 => t02.高速時間) == null ? 0 : t02Group.Sum(t02 => t02.高速時間),
                                 作業H = t02Group.Sum(t02 => t02.作業時間) == null ? 0 : t02Group.Sum(t02 => t02.作業時間),
                                 待機H = t02Group.Sum(t02 => t02.待機時間) == null ? 0 : t02Group.Sum(t02 => t02.待機時間),
                                 休憩H = t02Group.Sum(t02 => t02.休憩時間) == null ? 0 : t02Group.Sum(t02 => t02.休憩時間),
                                 残業H = t02Group.Sum(t02 => t02.残業時間) == null ? 0 : t02Group.Sum(t02 => t02.残業時間),
                                 深夜H = t02Group.Sum(t02 => t02.深夜時間) == null ? 0 : t02Group.Sum(t02 => t02.深夜時間),
                                 走行KM = t02Group.Sum(t02 => t02.走行ＫＭ) == null ? 0 : t02Group.Sum(t02 => t02.走行ＫＭ),
                                 実車KM = t02Group.Sum(t02 => t02.実車ＫＭ) == null ? 0 : t02Group.Sum(t02 => t02.実車ＫＭ),

                                 コードFrom = p乗務員From,
                                 コードTo = p乗務員To,
                                 コードList = s乗務員List,
                                 期間From = d集計期間From,
                                 期間To = d集計期間To,
                                 かな読み = m04.かな読み,
                                 //表示順 = i表示順 == 0 ? "ID順" : i表示順 == 1 ? "かな読み" : "売上順",
                                 表示順 = i表示順 == 0 ? "ID順" : i表示順 == 1 ? "かな読み" : i表示順 == 2 ? "売上順" : "拘束時間順",


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
                                            join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) on m04.乗務員KEY equals t01.乗務員KEY into t01Group
                                            join t02 in context.V_T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on m04.乗務員KEY equals t02.乗務員KEY into t02Group
                                            join t03 in query2.Where(t03 => t03.日付 >= d集計期間From && t03.日付 <= d集計期間To) on m04.乗務員KEY equals t03.乗務員KEY into t03Group
                                            where t01Group.Where(t01 => t01.乗務員KEY == m04.乗務員KEY).Any() == true && intCause.Contains(m04.乗務員ID) || t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Any() == true && intCause.Contains(m04.乗務員ID) ||
                                                        t03Group.Where(t03 => t03.乗務員KEY == m04.乗務員KEY).Any() == true && intCause.Contains(m04.乗務員ID)
                                            //from m78Group in sm78.DefaultIfEmpty()

                                            select new JMI07010_Member
                                            {

                                                コード = m04.乗務員ID,
                                                乗務員名 = m04.乗務員名,
                                                売上金額 = t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２) == null ? 0 : t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２),
                                                通行料 = t01Group.Sum(t01 => t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.通行料),
                                                売上合計 = t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                                社内金額 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額),
												社内立替 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払通行料) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払通行料),
												歩合金額 = Math.Round(t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * (m04.歩合率 / 100)) == null ? 0 : Math.Round(t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * (m04.歩合率 / 100)),
                                                経費合計 = t03Group.Sum(t03 => t03.金額) == null ? 0 : t03Group.Sum(t03 => t03.金額),
                                                拘束H = t02Group.Sum(t02 => t02.拘束時間) == null ? 0 : t02Group.Sum(t02 => t02.拘束時間),
                                                運転H = t02Group.Sum(t02 => t02.運転時間) == null ? 0 : t02Group.Sum(t02 => t02.運転時間),
                                                高速H = t02Group.Sum(t02 => t02.高速時間) == null ? 0 : t02Group.Sum(t02 => t02.高速時間),
                                                作業H = t02Group.Sum(t02 => t02.作業時間) == null ? 0 : t02Group.Sum(t02 => t02.作業時間),
                                                待機H = t02Group.Sum(t02 => t02.待機時間) == null ? 0 : t02Group.Sum(t02 => t02.待機時間),
                                                休憩H = t02Group.Sum(t02 => t02.休憩時間) == null ? 0 : t02Group.Sum(t02 => t02.休憩時間),
                                                残業H = t02Group.Sum(t02 => t02.残業時間) == null ? 0 : t02Group.Sum(t02 => t02.残業時間),
                                                深夜H = t02Group.Sum(t02 => t02.深夜時間) == null ? 0 : t02Group.Sum(t02 => t02.深夜時間),
                                                走行KM = t02Group.Sum(t02 => t02.走行ＫＭ) == null ? 0 : t02Group.Sum(t02 => t02.走行ＫＭ),
                                                実車KM = t02Group.Sum(t02 => t02.実車ＫＭ) == null ? 0 : t02Group.Sum(t02 => t02.実車ＫＭ),

                                                コードFrom = p乗務員From,
                                                コードTo = p乗務員To,
                                                コードList = s乗務員List,
                                                期間From = d集計期間From,
                                                期間To = d集計期間To,
                                                かな読み = m04.かな読み,
                                                //表示順 = i表示順 == 0 ? "ID順" : i表示順 == 1 ? "かな読み" : "売上順",
                                                表示順 = i表示順 == 0 ? "ID順" : i表示順 == 1 ? "かな読み" : i表示順 == 2 ? "売上順" : "拘束時間順",

                                            });
                    }
                    else
                    {
                        query = query.Where(c => c.コード > int.MinValue && c.コード < int.MaxValue);

                    }

                }

                query = query.Distinct();

                switch(i表示順)
                {
                    case 0:
                        query = query.OrderBy(c => c.コード);
                        break;
                    case 1:
                        query = query.OrderBy(c => c.かな読み);
                        break;
                    case 2:
                        query = query.OrderByDescending(c => c.売上金額);
                        break;
                    case 3:
                        query = query.OrderByDescending(c => c.拘束H);
                        break;
                }
                
                //結果をリスト化
                retList = query.ToList();

                return retList;
            }

        }
        #endregion



        #region CSV出力
        /// <summary>
        /// JMI07010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<JMI07010_Member_CSV> GetDataList_CSV(string p乗務員From, string p乗務員To, int?[] i乗務員List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string s乗務員List, int i表示順)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<JMI07010_Member_CSV> retList = new List<JMI07010_Member_CSV>();
                context.Connection.Open();

                int[] lst;
                lst = (from m04 in context.M04_DRV
                       let t01l = from t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) select t01.乗務員KEY
                       let t02l = from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) select t02.乗務員KEY
                       let t03l = from t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) select t03.乗務員KEY
                       where t01l.Contains(m04.乗務員KEY) || t02l.Contains(m04.乗務員KEY) || t03l.Contains(m04.乗務員KEY)
                       select m04.乗務員KEY).ToArray();

                var query2 = (from t in context.T03_KTRN
                              join m07 in context.M07_KEI on t.経費項目ID equals m07.経費項目ID
                              where m07.固定変動区分 == 1 && t.経費発生日 >= d集計期間From && t.経費発生日 <= d集計期間To
                              select new JMI06010_KTRN
                              {
                                  日付 = t.経費発生日,
                                  経費項目ID = t.経費項目ID,
                                  金額 = t.金額,
                                  乗務員KEY = t.乗務員KEY,
                              }).AsQueryable();

                var query = (from m04 in context.M04_DRV
                             join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) on m04.乗務員KEY equals t01.乗務員KEY into t01Group
                             join t02 in context.V_T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on m04.乗務員KEY equals t02.乗務員KEY into t02Group
                             join t03 in query2.Where(t03 => t03.日付 >= d集計期間From && t03.日付 <= d集計期間To) on m04.乗務員KEY equals t03.乗務員KEY into t03Group
                             where t01Group.Where(t01 => t01.乗務員KEY == m04.乗務員KEY).Any() == true || t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Any() == true ||
                                                t03Group.Where(t03 => t03.乗務員KEY == m04.乗務員KEY).Any() == true
                             //from m78Group in sm78.DefaultIfEmpty()

                             select new JMI07010_Member_CSV
                             {

                                 コード = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 売上金額 = t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２) == null ? 0 : t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２),
                                 通行料 = t01Group.Sum(t01 => t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.通行料),
                                 売上合計 = t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 社内金額 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額),
								 社内立替 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払通行料) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払通行料),
								 歩合金額 = Math.Round(t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * (m04.歩合率 / 100)) == null ? 0 : Math.Round(t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * (m04.歩合率 / 100)),
                                 経費合計 = t03Group.Sum(t03 => t03.金額) == null ? 0 : t03Group.Sum(t03 => t03.金額),
                                 拘束H = t02Group.Sum(t02 => t02.拘束時間) == null ? 0 : t02Group.Sum(t02 => t02.拘束時間),
                                 運転H = t02Group.Sum(t02 => t02.運転時間) == null ? 0 : t02Group.Sum(t02 => t02.運転時間),
                                 高速H = t02Group.Sum(t02 => t02.高速時間) == null ? 0 : t02Group.Sum(t02 => t02.高速時間),
                                 作業H = t02Group.Sum(t02 => t02.作業時間) == null ? 0 : t02Group.Sum(t02 => t02.作業時間),
                                 待機H = t02Group.Sum(t02 => t02.待機時間) == null ? 0 : t02Group.Sum(t02 => t02.待機時間),
                                 休憩H = t02Group.Sum(t02 => t02.休憩時間) == null ? 0 : t02Group.Sum(t02 => t02.休憩時間),
                                 残業H = t02Group.Sum(t02 => t02.残業時間) == null ? 0 : t02Group.Sum(t02 => t02.残業時間),
                                 深夜H = t02Group.Sum(t02 => t02.深夜時間) == null ? 0 : t02Group.Sum(t02 => t02.深夜時間),
                                 走行KM = t02Group.Sum(t02 => t02.走行ＫＭ) == null ? 0 : t02Group.Sum(t02 => t02.走行ＫＭ),
                                 実車KM = t02Group.Sum(t02 => t02.実車ＫＭ) == null ? 0 : t02Group.Sum(t02 => t02.実車ＫＭ),

                                 かな読み = m04.かな読み,


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
                                            join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) on m04.乗務員KEY equals t01.乗務員KEY into t01Group
                                            join t02 in context.V_T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on m04.乗務員KEY equals t02.乗務員KEY into t02Group
                                            join t03 in query2.Where(t03 => t03.日付 >= d集計期間From && t03.日付 <= d集計期間To) on m04.乗務員KEY equals t03.乗務員KEY into t03Group
                                            where t01Group.Where(t01 => t01.乗務員KEY == m04.乗務員KEY).Any() == true && intCause.Contains(m04.乗務員ID) || t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Any() == true && intCause.Contains(m04.乗務員ID) ||
                                                               t03Group.Where(t03 => t03.乗務員KEY == m04.乗務員KEY).Any() == true && intCause.Contains(m04.乗務員ID)
                                            //from m78Group in sm78.DefaultIfEmpty()

                                            select new JMI07010_Member_CSV
                                            {

                                                コード = m04.乗務員ID,
                                                乗務員名 = m04.乗務員名,
                                                売上金額 = t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２) == null ? 0 : t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２),
                                                通行料 = t01Group.Sum(t01 => t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.通行料),
                                                売上合計 = t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                                社内金額 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額),
												社内立替 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払通行料) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払通行料),
												歩合金額 = Math.Round(t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * (m04.歩合率 / 100)) == null ? 0 : Math.Round(t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * (m04.歩合率 / 100)),
                                                経費合計 = t03Group.Sum(t03 => t03.金額) == null ? 0 : t03Group.Sum(t03 => t03.金額),
                                                拘束H = t02Group.Sum(t02 => t02.拘束時間) == null ? 0 : t02Group.Sum(t02 => t02.拘束時間),
                                                運転H = t02Group.Sum(t02 => t02.運転時間) == null ? 0 : t02Group.Sum(t02 => t02.運転時間),
                                                高速H = t02Group.Sum(t02 => t02.高速時間) == null ? 0 : t02Group.Sum(t02 => t02.高速時間),
                                                作業H = t02Group.Sum(t02 => t02.作業時間) == null ? 0 : t02Group.Sum(t02 => t02.作業時間),
                                                待機H = t02Group.Sum(t02 => t02.待機時間) == null ? 0 : t02Group.Sum(t02 => t02.待機時間),
                                                休憩H = t02Group.Sum(t02 => t02.休憩時間) == null ? 0 : t02Group.Sum(t02 => t02.休憩時間),
                                                残業H = t02Group.Sum(t02 => t02.残業時間) == null ? 0 : t02Group.Sum(t02 => t02.残業時間),
                                                深夜H = t02Group.Sum(t02 => t02.深夜時間) == null ? 0 : t02Group.Sum(t02 => t02.深夜時間),
                                                走行KM = t02Group.Sum(t02 => t02.走行ＫＭ) == null ? 0 : t02Group.Sum(t02 => t02.走行ＫＭ),
                                                実車KM = t02Group.Sum(t02 => t02.実車ＫＭ) == null ? 0 : t02Group.Sum(t02 => t02.実車ＫＭ),

                                                かな読み = m04.かな読み,


                                            });
                    }
                    else
                    {
                        query = query.Where(c => c.コード > int.MinValue && c.コード < int.MaxValue);

                    }

                }

                query = query.Distinct();

                switch (i表示順)
                {
                    case 0:
                        query = query.OrderBy(c => c.コード);
                        break;
                    case 1:
                        query = query.OrderBy(c => c.かな読み);
                        break;
                    case 2:
                        query = query.OrderByDescending(c => c.売上金額);
                        break;
                    case 3:
                        query = query.OrderByDescending(c => c.拘束H);
                        break;
                }
                
                //結果をリスト化
                retList = query.ToList();

                //これを使用するとエラーになる
                //retList = (from q in query
                //           select new JMI07010_Member_CSV
                //           {
                //               コード = q.コード,
                //               乗務員名 = q.乗務員名,
                //               売上金額 = q.売上金額,
                //               通行料 = q.通行料,
                //               売上合計 = q.売上合計,
                //               社内金額 = q.社内金額,
                //               歩合金額 = q.歩合金額,
                //               経費合計 = q.経費合計,
                //               拘束H = KyoeiSystem.Framework.Common.LinqSub.分TO時間(q.拘束H),
                //               運転H = KyoeiSystem.Framework.Common.LinqSub.分TO時間(q.運転H),
                //               高速H = KyoeiSystem.Framework.Common.LinqSub.分TO時間(q.高速H),
                //               作業H = KyoeiSystem.Framework.Common.LinqSub.分TO時間(q.作業H),
                //               待機H = KyoeiSystem.Framework.Common.LinqSub.分TO時間(q.待機H),
                //               休憩H = KyoeiSystem.Framework.Common.LinqSub.分TO時間(q.休憩H),
                //               残業H = KyoeiSystem.Framework.Common.LinqSub.分TO時間(q.残業H),
                //               深夜H = KyoeiSystem.Framework.Common.LinqSub.分TO時間(q.深夜H),
                //               走行KM = q.走行KM,
                //               実車KM = q.実車KM,
							   
                //           }).ToList();

                return retList;
            }

        }
        #endregion
    }
}