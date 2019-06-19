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
    /// JMI14010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class JMI14010_Member
    {
        public int コード { get; set; }
        public string 乗務員名 { get; set; }
        public int 売上金額 { get; set; }
        public int? 通行料 { get; set; }
        public int? 売上合計 { get; set; }
        public int? 社内金額 { get; set; }
        public int? 歩合金額 { get; set; }
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
    /// JMI14010  CSV　メンバー
    /// </summary>
    [DataContract]
    public class JMI14010_Member_CSV
	{
		public int ID { get; set; }
		public int? 連携ID { get; set; }
        public string 乗務員名 { get; set; }
        public string かな読み { get; set; }
        public int 売上金額 { get; set; }
        public int? 通行料 { get; set; }
        public int? 売上合計 { get; set; }
        public int? 社内金額 { get; set; }
        public int? 歩合金額 { get; set; }
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
    public class JMI14010_M07
    {
        [DataMember]
        public int 経費ID { get; set; }
        [DataMember]
        public string 経費名 { get; set; }
    }


	/// <summary>
	/// JMI04010  CSV　メンバー
	/// </summary>
	[DataContract]
	public class JMI14010_JMI04010_Member_CSV
	{
		public int コード { get; set; }
		public int? 連携コード { get; set; }
		public string 乗務員名 { get; set; }
		public decimal 社内金額 { get; set; }
		public int? 経費1 { get; set; }
		public int? 経費2 { get; set; }
		public int? 経費3 { get; set; }
		public int? 経費4 { get; set; }
		public int? 経費5 { get; set; }
		public int? 経費6 { get; set; }
		public int? 経費7 { get; set; }
		public int? 他経費計 { get; set; }
		public decimal? 燃料L { get; set; }
		public int? 燃料代 { get; set; }
		public int? 歩合金額 { get; set; }
		public decimal? 走行KM { get; set; }
		public string 経費名1 { get; set; }
		public string 経費名2 { get; set; }
		public string 経費名3 { get; set; }
		public string 経費名4 { get; set; }
		public string 経費名5 { get; set; }
		public string 経費名6 { get; set; }
		public string 経費名7 { get; set; }

	}



    public class JMI14010
    {
        #region 印刷
        /// <summary>
        /// JMI14010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<JMI14010_Member> GetDataList(string p乗務員From, string p乗務員To, int?[] i乗務員List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string s乗務員List, int i表示順 )
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<JMI14010_Member> retList = new List<JMI14010_Member>();
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

                             select new JMI14010_Member
                             {

                                 コード = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 売上金額 = t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２) == null ? 0 : t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２),
                                 通行料 = t01Group.Sum(t01 => t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.通行料),
                                 売上合計 = t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 社内金額 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額),
                                 歩合金額 = Math.Round(t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * (m04.歩合率 / 100)) == null ? 0 : (int)Math.Round(t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * (m04.歩合率 / 100)),
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
                                 表示順 = i表示順 == 0 ? "ID順" : i表示順 == 1 ? "かな読み" : "売上順",


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

                                            select new JMI14010_Member
                                            {

                                                コード = m04.乗務員ID,
                                                乗務員名 = m04.乗務員名,
                                                売上金額 = t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２) == null ? 0 : t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２),
                                                通行料 = t01Group.Sum(t01 => t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.通行料),
                                                売上合計 = t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                                社内金額 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額),
                                                歩合金額 = Math.Round(t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * (m04.歩合率 / 100)) == null ? 0 : (int)Math.Round(t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * (m04.歩合率 / 100)),
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
                                                表示順 = i表示順 == 0 ? "ID順" : i表示順 == 1 ? "かな読み" : "売上順",


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
                }
                
                //結果をリスト化
                retList = query.ToList();

                return retList;
            }

        }
        #endregion



        #region CSV出力
        /// <summary>
        /// JMI14010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<JMI14010_Member_CSV> GetDataList_CSV(string p乗務員From, string p乗務員To, int?[] i乗務員List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string s乗務員List, int i表示順)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<JMI14010_Member_CSV> retList = new List<JMI14010_Member_CSV>();
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

                             select new JMI14010_Member_CSV
                             {

                                 ID = m04.乗務員ID,
								 連携ID = m04.デジタコCD,
                                 乗務員名 = m04.乗務員名,
                                 売上金額 = t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２) == null ? 0 : t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２),
                                 通行料 = t01Group.Sum(t01 => t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.通行料),
                                 売上合計 = t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 社内金額 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額),
                                 歩合金額 = Math.Round(t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * (m04.歩合率 / 100)) == null ? 0 : (int)Math.Round(t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * (m04.歩合率 / 100)),
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
                        query = query.Where(c => c.ID >= int.MaxValue);
                    }

                    //乗務員From処理　Min値
                    if (!string.IsNullOrEmpty(p乗務員From))
                    {
                        int i乗務員FROM = AppCommon.IntParse(p乗務員From);
                        query = query.Where(c => c.ID >= i乗務員FROM);
                    }

                    //乗務員To処理　Max値
                    if (!string.IsNullOrEmpty(p乗務員To))
                    {
                        int i乗務員TO = AppCommon.IntParse(p乗務員To);
                        query = query.Where(c => c.ID <= i乗務員TO);
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

                                            select new JMI14010_Member_CSV
                                            {

                                                ID = m04.乗務員ID,
												連携ID = m04.デジタコCD,
                                                乗務員名 = m04.乗務員名,
                                                売上金額 = t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２) == null ? 0 : t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２),
                                                通行料 = t01Group.Sum(t01 => t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.通行料),
                                                売上合計 = t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                                社内金額 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額),
                                                歩合金額 = Math.Round(t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * (m04.歩合率 / 100)) == null ? 0 : (int)Math.Round(t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * (m04.歩合率 / 100)),
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
                        query = query.Where(c => c.ID > int.MinValue && c.ID < int.MaxValue);

                    }

                }

                query = query.Distinct();

                switch (i表示順)
                {
                    case 0:
                        query = query.OrderBy(c => c.ID);
                        break;
                    case 1:
                        query = query.OrderBy(c => c.かな読み);
                        break;
                    case 2:
                        query = query.OrderByDescending(c => c.売上金額);
                        break;
                }
                
                //結果をリスト化
                retList = query.ToList();

				int syo;
				int amari;
				foreach (JMI14010_Member_CSV row in retList)
				{
					syo = Math.DivRem((int)row.運転H, 60, out amari);
					row.運転H = syo + ((decimal)amari / 100);
					syo = Math.DivRem((int)row.休憩H, 60, out amari);
					row.休憩H = syo + ((decimal)amari / 100);
					syo = Math.DivRem((int)row.拘束H, 60, out amari);
					row.拘束H = syo + ((decimal)amari / 100);
					syo = Math.DivRem((int)row.高速H, 60, out amari);
					row.高速H = syo + ((decimal)amari / 100);
					syo = Math.DivRem((int)row.作業H, 60, out amari);
					row.作業H = syo + ((decimal)amari / 100);
					syo = Math.DivRem((int)row.残業H, 60, out amari);
					row.残業H = syo + ((decimal)amari / 100);
					syo = Math.DivRem((int)row.深夜H, 60, out amari);
					row.深夜H = syo + ((decimal)amari / 100);
					syo = Math.DivRem((int)row.待機H, 60, out amari);
					row.待機H = syo + ((decimal)amari / 100);
				}

                //これを使用するとエラーになる
                //retList = (from q in query
                //           select new JMI14010_Member_CSV
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


		#region CSV出力
		/// <summary>
		/// JMI01010 CSV出力
		/// </summary>
		/// <param name="p商品ID">乗務員コード</param>
		/// <returns>T01</returns>
		public DataTable GetDataList_SYUKKIN_CSV(string p乗務員From, string p乗務員To, int?[] i乗務員List, DateTime d集計期間From, DateTime d集計期間To, string s乗務員ピックアップ)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				DataTable dtTest = new DataTable();
				int cnt = 0;
				ArrayList strData = new ArrayList();

				dtTest.Columns.Add("乗務員ID", typeof(Int32));
				dtTest.Columns.Add("連携ID", typeof(Int32));
				dtTest.Columns["連携ID"].AllowDBNull = true;
				dtTest.Columns.Add("乗務員名", typeof(string));
				//
				for (DateTime x = d集計期間From; x <= d集計期間To; cnt += 1)
				{
					strData.Add(x.Date.ToString());
					//strData[cnt] = x.Date.ToString();

					dtTest.Columns.Add((d集計期間From.AddDays(cnt).Day).ToString(), typeof(string));
					x = x.AddDays(1);
				}
				for (int i = dtTest.Columns.Count; i < 34; i++)
				{
					dtTest.Columns.Add("列" + (cnt + 1).ToString(), typeof(string));
					cnt++;
				}
				// 主キーの設定
				dtTest.PrimaryKey = new DataColumn[] { dtTest.Columns["乗務員ID"] };


				List<JMI01010_date> retDateList = new List<JMI01010_date>();

				List<JMI01010_Member> retDrvList = new List<JMI01010_Member>();
				context.Connection.Open();

				//データがある乗務員のみ列挙
				//int[] DRV_IDList;
				//全件表示
				var query1 = (from drv in context.M04_DRV.Where(drv => drv.削除日付 == null).DefaultIfEmpty()

							  orderby drv.乗務員ID
							  select new JMI01010_JMI
							  {
								  乗務員コード = drv.乗務員ID,
								  連携コード = drv.デジタコCD,
								  乗務員名 = drv.乗務員名,
							  }
							  ).AsQueryable();


				if (!(string.IsNullOrEmpty(p乗務員From + p乗務員To) && i乗務員List.Length == 0))
				{

					//乗務員が検索対象に入っていない時全データ取得
					if (string.IsNullOrEmpty(p乗務員From + p乗務員To))
					{
						query1 = query1.Where(c => c.乗務員コード >= int.MaxValue);
					}

					//乗務員From処理　Min値
					if (!string.IsNullOrEmpty(p乗務員From))
					{
						int i乗務員FROM;
						int.TryParse(p乗務員From, out i乗務員FROM);
						query1 = query1.Where(c => c.乗務員コード >= i乗務員FROM);
					}

					//乗務員To処理　Max値
					if (!string.IsNullOrEmpty(p乗務員To))
					{
						int i乗務員TO;
						int.TryParse(p乗務員To, out i乗務員TO);
						query1 = query1.Where(c => c.乗務員コード <= i乗務員TO);
					}

					if (i乗務員List.Length > 0)
					{
						var intCause = i乗務員List;
						query1 = query1.Union(from m04 in context.M04_DRV
											  where intCause.Contains(m04.乗務員ID)
											  select new JMI01010_JMI
											  {
												  乗務員コード = m04.乗務員ID,
												  連携コード = m04.デジタコCD,
												  乗務員名 = m04.乗務員名,

											  });
						//乗務員From処理　Min値
						if (!string.IsNullOrEmpty(p乗務員From))
						{
							int i乗務員FROM;
							int.TryParse(p乗務員From, out i乗務員FROM);
							query1 = query1.Where(c => c.乗務員コード >= i乗務員FROM);
						}

						//乗務員To処理　Max値
						if (!string.IsNullOrEmpty(p乗務員To))
						{
							int i乗務員TO;
							int.TryParse(p乗務員To, out i乗務員TO);
							query1 = query1.Where(c => c.乗務員コード <= i乗務員TO);
						}
					}
				}
				else
				{
					query1 = query1.Where(c => c.乗務員コード > int.MinValue && c.乗務員コード < int.MaxValue);
				}

				if (i乗務員List.Length != 0)
				{
					int?[] intCause = i乗務員List;
					query1 = query1.Union(from drv in context.M04_DRV.Where(drv => drv.削除日付 == null).DefaultIfEmpty()
										  where intCause.Contains(drv.乗務員ID)
										  orderby drv.乗務員ID
										  select new JMI01010_JMI
										  {
											  乗務員コード = drv.乗務員ID,
											  連携コード = drv.デジタコCD,
											  乗務員名 = drv.乗務員名,
										  });
				}

				query1 = query1.Distinct();

				List<JMI01010_JMI> query1LIST;

				query1LIST = query1.ToList();

				//乗務員レコード追加
				int i乗務員 = 0;
				//DataTable dtTest = new DataTable();
				for (int i = 0; i < query1LIST.Count; i++)
				{
					i乗務員 = query1LIST[i].乗務員コード;
					DataRow dr = dtTest.NewRow();

					dr["乗務員ID"] = query1LIST[i].乗務員コード;
					dr["連携ID"] = query1LIST[i].連携コード == null ? 0 : query1LIST[i].連携コード;
					dr["乗務員名"] = query1LIST[i].乗務員名;

					dtTest.Rows.Add(dr);
				}

				//UTRNデータ抽出
				var query = (from t02 in context.T02_UTRN
							 from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == t02.乗務員KEY).DefaultIfEmpty()
							 from q in query1.Where(c => c.乗務員コード == m04.乗務員ID)
							 where t02.勤務開始日 <= d集計期間To && t02.勤務終了日 >= d集計期間From
							 select new JMI01010_Member
							 {
								 開始日 = (DateTime)d集計期間From,
								 終了日 = (DateTime)d集計期間To,
								 IDFROM = p乗務員From,
								 IDTO = p乗務員To,
								 乗務員コード = m04.乗務員ID,
								 乗務員名 = m04.乗務員名,
								 出勤区分 = t02.出勤区分ID,
								 基点日 = t02.勤務開始日 < d集計期間From ? d集計期間From : t02.勤務開始日,
								 期間 = EntityFunctions.DiffDays((t02.勤務開始日 < d集計期間From ? d集計期間From : t02.勤務開始日), (t02.勤務終了日 > d集計期間To ? d集計期間To : t02.勤務終了日)) + 1,
							 }).ToList();


				try
				{
					//出勤区分更新
					//i乗務員 = 0;
					//DataSet dstest = new DataSet
					//DataTable dtTest = new DataTable();
					for (int i = 0; i < query.Count; i++)
					{
						DateTime Kitenbi = (DateTime)query[i].基点日;
						for (int ii = 0; ii < query[i].期間; ii++)
						{
							if (query[i].乗務員コード != null)
							{
								DataRow targetRow = dtTest.Rows.Find(query[i].乗務員コード);
								//if (query[i].出勤区分 >= 1 && query[i].出勤区分 <= 7)
								//{
								//    targetRow[Convert.ToString(Kitenbi.Day + ii)] = query2.Where(x => x.コード == query[i].出勤区分);
								//}else
								//{
								//    targetRow[Convert.ToString(Kitenbi.Day + ii)] = "他";
								//}
								if (Kitenbi.AddDays(ii) <= d集計期間To)
								{
									//d集計期間From.Day
									TimeSpan ts = Kitenbi.AddDays(ii) - d集計期間From.AddDays(-1);
									targetRow[Convert.ToString(ts.Days)] = query[i].出勤区分;
								}
							}
						}
					}
				}
				catch (Exception ex)
				{ }

				//出勤区分合計算出
				int MAXCOL = dtTest.Columns.Count;

				//出勤区分取得

				var query2 = (from syk in context.M78_SYK.Where(syk => syk.削除日付 == null).DefaultIfEmpty()
							  orderby syk.出勤区分ID
							  select new JMI01010_syukin
							  {
								  コード = syk.出勤区分ID,
								  出勤区分名 = syk.出勤区分名,
							  }
							  ).AsQueryable();

				//テーブルに合計列追加
				var querywhere = query2.Where(x => x.コード == 1).ToList();
				//1～7個目
				for (int i = 0; i < 8; i++)
				{

					querywhere = query2.Where(x => x.コード == i).ToList();
					if (querywhere != null)
					{
						dtTest.Columns.Add((querywhere[0].出勤区分名.ToString()).ToString(), typeof(String));
					}
					else
					{
						dtTest.Columns.Add(("合計項目" + i).ToString(), typeof(String));
					}

					//dtTest.Columns.Add(("合計" + i).ToString(), typeof(Int32));

					//querywhere = query2.Where(x => x.コード == i).ToList();
					//if(querywhere != null)
					//{
					//    dtTest.Columns.Add(querywhere[0].出勤区分名 , typeof(Int32));
					//}else
					//{
					//    dtTest.Columns.Add( "" , typeof(Int32));
					//}
				}
				dtTest.Columns.Add("他計", typeof(Int32));
				dtTest.Columns.Add("合計", typeof(Int32));
				//dtTest.Columns.Add("他計", typeof(Int32));
				//dtTest.Columns.Add("合計", typeof(Int32));

				//合計を追加
				int GK0 = 0, GK1 = 0, GK2 = 0, GK3 = 0, GK4 = 0, GK5 = 0, GK6 = 0, GK7 = 0, GK8 = 0, GK20 = 0;
				for (int i = 0; i < dtTest.Rows.Count; i++)
				{
					for (int ii = 3; ii < MAXCOL; ii++)
					{
						DataRow targetRow = dtTest.Rows[i];
						switch (targetRow[ii].ToString())
						{
						case "0":
							GK0++;
							break;
						case "1":
							GK1++;
							break;
						case "2":
							GK2++;
							break;
						case "3":
							GK3++;
							break;
						case "4":
							GK4++;
							break;
						case "5":
							GK5++;
							break;
						case "6":
							GK6++;
							break;
						case "7":
							GK7++;
							break;
						case "":
							break;
						case null:
							break;
						default:
							GK8++;
							break;
						}
					}

					int iii = MAXCOL;
					DataRow targetRow2;
					targetRow2 = dtTest.Rows[i];
					targetRow2[iii] = GK0;
					iii++;
					targetRow2[iii] = GK1;
					iii++;
					targetRow2[iii] = GK2;
					iii++;
					targetRow2[iii] = GK3;
					iii++;
					targetRow2[iii] = GK4;
					iii++;
					targetRow2[iii] = GK5;
					iii++;
					targetRow2[iii] = GK6;
					iii++;
					targetRow2[iii] = GK7;
					iii++;
					targetRow2[iii] = GK8;
					GK20 = GK0 + GK1 + GK2 + GK3 + GK4 + GK5 + GK6 + GK7 + GK8;
					iii++;
					targetRow2[iii] = GK20;
					GK0 = 0;
					GK1 = 0;
					GK2 = 0;
					GK3 = 0;
					GK4 = 0;
					GK5 = 0;
					GK6 = 0;
					GK7 = 0;
					GK8 = 0;
					GK20 = 0;
				}

				//出勤データをＩＤから文字へ置き換え
				//DataSet dstest = new DataSet
				//DataTable dtTest = new DataTable();
				for (int i = 0; i < query.Count; i++)
				{
					DateTime Kitenbi = (DateTime)query[i].基点日;
					for (int ii = 0; ii < query[i].期間; ii++)
					{
						TimeSpan ts = Kitenbi.AddDays(ii) - d集計期間From.AddDays(-1);
						if (Kitenbi.AddDays(ii) <= d集計期間To)
						{
							if (query[i].乗務員コード != null)
							{
								DataRow targetRow = dtTest.Rows.Find(query[i].乗務員コード);
								if (query[i].出勤区分 >= 0 && query[i].出勤区分 <= 7)
								{
									int id;
									int.TryParse(query[i].出勤区分.ToString(), out id);
									querywhere = query2.Where(x => x.コード == id).ToList();

									char c1 = querywhere[0].出勤区分名.ToString()[0];
									targetRow[Convert.ToString(ts.Days)] = c1;
								}
								else
								{
									targetRow[Convert.ToString(ts.Days)] = "他";
								}
								//targetRow[Convert.ToString(Kitenbi.Day + ii)] = query[i].出勤区分;
							}
						}
					}
				}

				//デバック用後でコメント削除
				////データがない行を削除
				//for (int i = dtTest.Rows.Count-1; i >= 0; i--)
				//{
				//    DataRow targetRow = dtTest.Rows[i];
				//    if ((targetRow[dtTest.Columns.Count - 1]).ToString() == "0")
				//    {
				//        targetRow.Delete();
				//    }
				//}


				//dtTest.Columns.Add("開始日付", typeof(DateTime));
				//dtTest.Columns.Add("終了日付", typeof(DateTime));
				//dtTest.Columns.Add("IDFrom", typeof(String));
				//dtTest.Columns.Add("IDTo", typeof(String));
				//dtTest.Columns.Add("IDList", typeof(String));

				//for (int x = 1; x <= 31; x += 1)
				//{
				//	dtTest.Columns.Add(x + "日", typeof(String));
				//}

				////日付の項目を追加
				//for (DateTime x = d集計期間From; x <= d集計期間To; cnt += 1)
				//{
				//    strData.Add(x.Date.ToString());
				//    //strData[cnt] = x.Date.ToString();

				//    dtTest.Columns.Add(Convert.ToString(x.Day), typeof(string));
				//    x = x.AddDays(1);
				//}
				//for (int i = dtTest.Columns.Count; i < 33; i++)
				//{
				//    dtTest.Columns.Add("", typeof(string));
				//}

				//合計項目用の列作成
				//1～7個目
				//for (int i = 0; i < 8; i++)
				//{
				//	querywhere = query2.Where(x => x.コード == i).ToList();
				//	if (querywhere != null)
				//	{
				//		dtTest.Columns.Add((querywhere[0].出勤区分名.ToString()).ToString(), typeof(String));
				//	}
				//	else
				//	{
				//		dtTest.Columns.Add(("合計項目" + i).ToString(), typeof(String));
				//	}
				//}

				//for (int i = 0; i < dtTest.Rows.Count; i++)
				//{
				//DataRow targetRow = dtTest.Rows[i];
				//targetRow["開始日付"] = d集計期間From;
				//targetRow["終了日付"] = d集計期間To;
				//targetRow["IDFrom"] = p乗務員From;
				//targetRow["IDTo"] = p乗務員To;
				//targetRow["IDList"] = s乗務員ピックアップ;
				//日付の項目を追加
				//cnt = 0;
				//for (DateTime x = d集計期間From; x <= d集計期間To; x = x.AddDays(1))
				//{
				//	cnt++;
				//	targetRow[cnt.ToString() + "日"] = x.Day.ToString();

				//}
				//for (cnt = 0; cnt <= 7; cnt++)
				//{
				//	querywhere = query2.Where(x => x.コード == cnt).ToList();

				//	if (querywhere != null)
				//	{
				//		targetRow["合計項目" + cnt.ToString()] = querywhere[0].出勤区分名;
				//	}
				//	else
				//	{
				//		targetRow["合計項目" + cnt.ToString()] = "";
				//	}
				//}
				//for (int i = cnt; i < dtTest.Columns.Count; i++)
				//{
				//    targetRow["IDList"] = "";
				//}

				//}


				////datatableをリスト化
				//List<DataRow> retList = new List<DataRow>();
				//for (int i = 0; i < dtTest.Rows.Count; i++)
				//{
				//    DataRow targetRow = dtTest.Rows[i];
				//    retList.Add(targetRow);
				//}


				//データテーブル型で返す
				return dtTest;

			}

		}
		#endregion


		#region CSV出力

		/// <summary>
		/// JMI04010 印刷
		/// </summary>
		/// <param name="p商品ID">乗務員コード</param>
		/// <returns>T01</returns>
		public List<JMI14010_JMI04010_Member_CSV> GetDataList_KEIHI_CSV(string p乗務員From, string p乗務員To, int?[] i乗務員List, DateTime d集計期間From, DateTime d集計期間To, string s乗務員List)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{

				List<JMI14010_JMI04010_Member_CSV> retList = new List<JMI14010_JMI04010_Member_CSV>();
				context.Connection.Open();

				var query = (from m04 in context.M04_DRV
							 join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && ((t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1))) on m04.乗務員KEY equals t01.乗務員KEY into t01Group
							 join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on m04.乗務員KEY equals t02.乗務員KEY into t02Group
							 join t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) on m04.乗務員KEY equals t03.乗務員KEY into t03Group
							 where t01Group.Where(t01 => t01.乗務員KEY == m04.乗務員KEY).Any() == true || t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Any() == true ||
												t03Group.Where(t03 => t03.乗務員KEY == m04.乗務員KEY).Any() == true
							 //from m78Group in sm78.DefaultIfEmpty()

							 select new JMI14010_JMI04010_Member_CSV
							 {
								 コード = m04.乗務員ID,
								 連携コード = m04.デジタコCD,
								 乗務員名 = m04.乗務員名,
								 社内金額 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額),
								 経費1 = t03Group.Where(t03 => t03.経費項目ID == 601).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 601).Sum(t03 => t03.金額),
								 経費2 = t03Group.Where(t03 => t03.経費項目ID == 602).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 602).Sum(t03 => t03.金額),
								 経費3 = t03Group.Where(t03 => t03.経費項目ID == 603).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 603).Sum(t03 => t03.金額),
								 経費4 = t03Group.Where(t03 => t03.経費項目ID == 604).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 604).Sum(t03 => t03.金額),
								 経費5 = t03Group.Where(t03 => t03.経費項目ID == 605).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 605).Sum(t03 => t03.金額),
								 経費6 = t03Group.Where(t03 => t03.経費項目ID == 606).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 606).Sum(t03 => t03.金額),
								 経費7 = t03Group.Where(t03 => t03.経費項目ID == 607).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 607).Sum(t03 => t03.金額),
								 他経費計 = t03Group.Where(t03 => t03.経費項目ID != 601 && t03.経費項目ID != 602 && t03.経費項目ID != 603 &&
															   t03.経費項目ID != 604 && t03.経費項目ID != 605 && t03.経費項目ID != 606 &&
															   t03.経費項目ID != 607 && t03.経費項目ID != 401).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID != 601 && t03.経費項目ID != 602 && t03.経費項目ID != 603 &&
															   t03.経費項目ID != 604 && t03.経費項目ID != 605 && t03.経費項目ID != 606 &&
															   t03.経費項目ID != 607 && t03.経費項目ID != 401).Sum(t03 => t03.金額),
								 燃料L = t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.数量) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.数量),
								 燃料代 = t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.金額),
								 歩合金額 = Math.Round((t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * m04.歩合率 / 100), 0) == null ? 0 : (int)Math.Round((t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * m04.歩合率 / 100), 0),
								 走行KM = t02Group.Sum(t02 => t02.走行ＫＭ) == null ? 0 : t02Group.Sum(t02 => t02.走行ＫＭ),

								 経費名1 = (from m07 in context.M07_KEI where m07.経費項目ID == 601 select m07.経費項目名).FirstOrDefault(),
								 経費名2 = (from m07 in context.M07_KEI where m07.経費項目ID == 602 select m07.経費項目名).FirstOrDefault(),
								 経費名3 = (from m07 in context.M07_KEI where m07.経費項目ID == 603 select m07.経費項目名).FirstOrDefault(),
								 経費名4 = (from m07 in context.M07_KEI where m07.経費項目ID == 604 select m07.経費項目名).FirstOrDefault(),
								 経費名5 = (from m07 in context.M07_KEI where m07.経費項目ID == 605 select m07.経費項目名).FirstOrDefault(),
								 経費名6 = (from m07 in context.M07_KEI where m07.経費項目ID == 606 select m07.経費項目名).FirstOrDefault(),
								 経費名7 = (from m07 in context.M07_KEI where m07.経費項目ID == 607 select m07.経費項目名).FirstOrDefault(),


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
											join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && ((t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1))) on m04.乗務員KEY equals t01.乗務員KEY into t01Group
											join t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on m04.乗務員KEY equals t02.乗務員KEY into t02Group
											join t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) on m04.乗務員KEY equals t03.乗務員KEY into t03Group
											where t01Group.Where(t01 => t01.乗務員KEY == m04.乗務員KEY).Any() == true && intCause.Contains(m04.乗務員ID) || t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Any() == true && intCause.Contains(m04.乗務員ID) ||
															   t03Group.Where(t03 => t03.乗務員KEY == m04.乗務員KEY).Any() == true && intCause.Contains(m04.乗務員ID)
											//from m78Group in sm78.DefaultIfEmpty()

											select new JMI14010_JMI04010_Member_CSV
											{
												コード = m04.乗務員ID,
												連携コード = m04.デジタコCD,
												乗務員名 = m04.乗務員名,
												社内金額 = t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) == null ? 0 : t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額),
												経費1 = t03Group.Where(t03 => t03.経費項目ID == 601).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 601).Sum(t03 => t03.金額),
												経費2 = t03Group.Where(t03 => t03.経費項目ID == 602).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 602).Sum(t03 => t03.金額),
												経費3 = t03Group.Where(t03 => t03.経費項目ID == 603).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 603).Sum(t03 => t03.金額),
												経費4 = t03Group.Where(t03 => t03.経費項目ID == 604).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 604).Sum(t03 => t03.金額),
												経費5 = t03Group.Where(t03 => t03.経費項目ID == 605).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 605).Sum(t03 => t03.金額),
												経費6 = t03Group.Where(t03 => t03.経費項目ID == 606).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 606).Sum(t03 => t03.金額),
												経費7 = t03Group.Where(t03 => t03.経費項目ID == 607).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 607).Sum(t03 => t03.金額),
												他経費計 = t03Group.Where(t03 => t03.経費項目ID != 601 && t03.経費項目ID != 602 && t03.経費項目ID != 603 &&
																			  t03.経費項目ID != 604 && t03.経費項目ID != 605 && t03.経費項目ID != 606 &&
																			  t03.経費項目ID != 607 && t03.経費項目ID != 401).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID != 601 && t03.経費項目ID != 602 && t03.経費項目ID != 603 &&
																			  t03.経費項目ID != 604 && t03.経費項目ID != 605 && t03.経費項目ID != 606 &&
																			  t03.経費項目ID != 607 && t03.経費項目ID != 401).Sum(t03 => t03.金額),
												燃料L = t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.数量) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.数量),
												燃料代 = t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.金額) == null ? 0 : t03Group.Where(t03 => t03.経費項目ID == 401).Sum(t03 => t03.金額),
												歩合金額 = Math.Round((t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * m04.歩合率 / 100), 0) == null ? 0 : (int)Math.Round((t01Group.Where(c => c.支払先KEY == null).Sum(t01 => t01.支払金額) * m04.歩合率 / 100), 0),
												走行KM = t02Group.Sum(t02 => t02.走行ＫＭ) == null ? 0 : t02Group.Sum(t02 => t02.走行ＫＭ),

												経費名1 = (from m07 in context.M07_KEI where m07.経費項目ID == 601 select m07.経費項目名).FirstOrDefault(),
												経費名2 = (from m07 in context.M07_KEI where m07.経費項目ID == 602 select m07.経費項目名).FirstOrDefault(),
												経費名3 = (from m07 in context.M07_KEI where m07.経費項目ID == 603 select m07.経費項目名).FirstOrDefault(),
												経費名4 = (from m07 in context.M07_KEI where m07.経費項目ID == 604 select m07.経費項目名).FirstOrDefault(),
												経費名5 = (from m07 in context.M07_KEI where m07.経費項目ID == 605 select m07.経費項目名).FirstOrDefault(),
												経費名6 = (from m07 in context.M07_KEI where m07.経費項目ID == 606 select m07.経費項目名).FirstOrDefault(),
												経費名7 = (from m07 in context.M07_KEI where m07.経費項目ID == 607 select m07.経費項目名).FirstOrDefault(),

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
    }
}