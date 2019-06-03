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
using System.Data.SqlClient;

namespace KyoeiSystem.Application.WCFService
{

    /// <summary>
    /// JMI10010  運転適正診断印刷　メンバー
    /// </summary>
    [DataContract]
    public class JMI10010_Member
    {
        public int コード { get; set; }
        public int KEY { get; set; }
        public int 集計年月 { get; set; }
        public DateTime? 登録年月日 { get; set; }
        public int? 自社部門ID { get; set; }
        public int? 車種ID { get; set; }
        public int? 車輌KEY { get; set; }
        public int? 営業日数 { get; set; }
        public int? 稼働日数 { get; set; }
        public int? 走行KM { get; set; }
        public int? 実車KM { get; set; }
        public decimal? 輸送屯数 { get; set; }
        public int? 運送収入 { get; set; }
        public decimal? 燃料L { get; set; }
        public decimal? 一般管理費 { get; set; }
        public decimal? 拘束時間 { get; set; }
        public decimal? 運転時間 { get; set; }
        public decimal? 高速時間 { get; set; }
        public decimal? 作業時間 { get; set; }
        public decimal? 待機時間 { get; set; }
        public decimal? 休憩時間 { get; set; }
        public decimal? 残業時間 { get; set; }
        public decimal? 深夜時間 { get; set; }
    }
    [DataContract]
    public class JMI10010_sbMember
    {
        public decimal 金額  { get; set; }
        public int 経費項目ID  { get; set; }
        public string 経費項目名  { get; set; }
        public int?  固定変動区分  { get; set; }
        public DateTime? 更新日時 { get; set; }
        public int 集計年月 { get; set; }
		public int 乗務員KEY { get; set; }
		public int? 車輌KEY { get; set; }
		public DateTime? 登録日時 { get; set; }
        }
    /// <summary>
    /// JMI10010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class JMI10010_Date
    {
        public DateTime? 日付 { get; set; }
    }

    /// <summary>
    /// JMI10010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class JMI10010_KADOU
    {
        public int 乗務員KEY { get; set; }
        public DateTime? 日付 { get; set; }
        public int? 回数 { get; set; }
    }

    /// <summary>
    /// JMI10010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class JMI10010_KADOU2
    {
        public int 乗務員KEY { get; set; }
        public int? 回数 { get; set; }
    }

    [DataContract]
    public class JMI10010_2
    {
        [DataMember]
        public int 経費ID { get; set; }
        [DataMember]
        public string 経費名 { get; set; }
    }
    

    public class JMI10010
    {
        #region 適正診断一覧印刷
        /// <summary>
        /// JMI10010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<JMI10010_Member> SYUKEI(string p乗務員From, string p乗務員To, int?[] i乗務員List, int? p作成締日, DateTime d集計期間From, DateTime d集計期間To, int i集計年月, string s乗務員List, int i営業日数, int i一般管理費, int i固定再計算, DateTime d集計年月 )
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<JMI10010_Member> retList = new List<JMI10010_Member>();
                List<JMI10010_Date> retList2 = new List<JMI10010_Date>();
                context.Connection.Open();



                for (DateTime dDate = d集計期間From; dDate <= d集計期間To; dDate = dDate.AddDays(1))
                {
                    retList2.Add(new JMI10010_Date() { 日付 = dDate});
                }

                DataTable dtTest = new DataTable();
                dtTest.Columns.Add("日付", typeof(DateTime));


                var query2 = (from d in retList2
                              from m04 in context.M04_DRV.Where(m04 => m04.削除日付 == null)
                              join t02 in context.T02_UTRN.Where(t02 => t02.出勤区分ID <= 4) on m04.乗務員KEY equals t02.乗務員KEY into t02Group
                              select new JMI10010_KADOU
                              {
                                  乗務員KEY = m04.乗務員ID,
                                  日付 = d.日付,
                                  回数 = t02Group.Where(t02g => t02g.勤務開始日 <= d.日付 && t02g.勤務終了日 >= d.日付).Any() == true ? 1 : 0,
                              }).ToList();


                //var query3 = (from m04 in context.M04_DRV
                //              join q in query2 on m04.乗務員KEY equals q.乗務員KEY into qGroup
                //              select new JMI10010_KADOU2
                //              {
                //                  乗務員KEY = m04.乗務員KEY,
                //                  回数 = qGroup.Where(qg => qg.乗務員KEY == m04.乗務員KEY).Sum(qg => qg.回数),
                //              }).AsQueryable();

                var query = (from m04 in context.M04_DRV.Where(p => p.削除日付 == null)
                             join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) on m04.乗務員KEY equals t01.乗務員KEY into t01Group
                             from t01g in t01Group.DefaultIfEmpty()
                             join t02 in context.V_T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on m04.乗務員KEY equals t02.乗務員KEY into t02Group
                             from t02g in t02Group.DefaultIfEmpty()
                             join t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) on m04.乗務員KEY equals t03.乗務員KEY into t03Group
                             from t03g in t03Group.DefaultIfEmpty()
                             join s13 in context.S13_DRV.Where(s13 => s13.集計年月 == i集計年月) on m04.乗務員KEY equals s13.乗務員KEY into s13Group
                             from s13g in s13Group.DefaultIfEmpty()
                             join m05 in context.M05_CAR.Where(m05 => m05.削除日付 == null) on m04.乗務員KEY equals m05.乗務員KEY into m05Group
                             //join q in query2 on m04.乗務員KEY equals q.乗務員KEY into qGroup
                             select new JMI10010_Member
                             {
                                 KEY = m04.乗務員KEY,
                                 コード = m04.乗務員ID,
                                 集計年月 = i集計年月,
                                 登録年月日 = s13g.登録日時,
                                 自社部門ID = m04.自社部門ID,
                                 車種ID = m05Group.Min(m05 => m05.車種ID),
                                 車輌KEY = m05Group.Min(m05 => m05.車輌KEY),
                                 営業日数 = i営業日数,
                                 稼働日数 = 0,
                                 //稼働日数 = qGroup.Where(q => q.乗務員KEY == m04.乗務員KEY).Sum(q => q.回数),
                                 走行KM = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.走行ＫＭ),
                                 実車KM = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.実車ＫＭ),
                                 輸送屯数 = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.輸送屯数),
                                 運送収入 = t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 燃料L = t03Group.Where(t03 => t03.乗務員KEY == m04.乗務員KEY && t03.経費項目ID == 401).Sum(t03 => t03.数量),
                                 一般管理費 = (t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null || t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == 0) ? 0 : Math.Round((decimal)(t01Group.Sum(t01 => (decimal)t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) / 100 * i一般管理費), 0),
                                 拘束時間 = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.拘束時間),
                                 運転時間 = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.運転時間),
                                 高速時間 = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.高速時間),
                                 作業時間 = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.作業時間),
                                 待機時間 = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.待機時間),
                                 休憩時間 = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.休憩時間),
                                 残業時間 = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.残業時間),
                                 深夜時間 = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.深夜時間),

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


                        query = query.Union(from m04 in context.M04_DRV.Where(p => p.削除日付 == null)
                                     join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || t01.入力区分 == 3 && t01.明細行 != 1)) on m04.乗務員KEY equals t01.乗務員KEY into t01Group
                                     from t01g in t01Group.DefaultIfEmpty()
                                     join t02 in context.V_T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To) on m04.乗務員KEY equals t02.乗務員KEY into t02Group
                                     from t02g in t02Group.DefaultIfEmpty()
                                     join t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) on m04.乗務員KEY equals t03.乗務員KEY into t03Group
                                     from t03g in t03Group.DefaultIfEmpty()
                                     join s13 in context.S13_DRV.Where(s13 => s13.集計年月 == i集計年月) on m04.乗務員KEY equals s13.乗務員KEY into s13Group
                                     from s13g in s13Group.DefaultIfEmpty()
                                     join m05 in context.M05_CAR.Where(m05 => m05.削除日付 == null) on m04.乗務員KEY equals m05.乗務員KEY into m05Group
                                     //from m05g in m05Group.DefaultIfEmpty()
                                     //join q in query2 on m04.乗務員KEY equals q.乗務員KEY into qGroup
                                     where intCause.Contains(m04.乗務員ID)
                                     select new JMI10010_Member
                                     {
                                         KEY = m04.乗務員KEY,
                                         コード = m04.乗務員ID,
                                         集計年月 = i集計年月,
                                         登録年月日 = s13g.登録日時,
                                         自社部門ID = m04.自社部門ID,
                                         車種ID = m05Group.Min(m05 => m05.車種ID),
                                         車輌KEY = m05Group.Min(m05 => m05.車輌KEY),
                                         営業日数 = i営業日数,
                                         稼働日数 = 0,
                                         //稼働日数 = qGroup.Where(q => q.乗務員KEY == m04.乗務員KEY).Sum(q => q.回数),
                                         走行KM = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.走行ＫＭ),
                                         実車KM = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.実車ＫＭ),
                                         輸送屯数 = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.輸送屯数),
                                         運送収入 = t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                         燃料L = t03Group.Where(t03 => t03.乗務員KEY == m04.乗務員KEY && t03.経費項目ID == 401).Sum(t03 => t03.数量),
                                         一般管理費 = (t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null || t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == 0) ? 0 : Math.Round((decimal)(t01Group.Sum(t01 => (decimal)t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) / 100 * i一般管理費), 0),
                                         拘束時間 = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.拘束時間),
                                         運転時間 = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.運転時間),
                                         高速時間 = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.高速時間),
                                         作業時間 = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.作業時間),
                                         待機時間 = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.待機時間),
                                         休憩時間 = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.休憩時間),
                                         残業時間 = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.残業時間),
                                         深夜時間 = t02Group.Where(t02 => t02.乗務員KEY == m04.乗務員KEY).Sum(t02 => t02.深夜時間),

                                     }).AsQueryable();


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

                        //集計期間処理
                        //query = query.Where(c => c.集計 > d集計期間From && d集計期間To > c.集計);

                    }
                    else
                    {
                        query = query.Where(c => c.コード > int.MinValue && c.コード < int.MaxValue);

                        ////締日処理　
                        //if (p作成締日 != 0)
                        //{
                        //    query = query.Where(c => c.締日 == p作成締日);
                        //}

                        ////集計期間処理
                        //query = query.Where(c => c.集計 > d集計期間From && d集計期間To > c.集計);

                    }

                }
                query = query.Distinct();



				//List<JMI10010_Member> queryLIST = query.ToList();
                List<JMI10010_KADOU> queryLIST2 = query2.ToList();

				var queryLIST = (from q in query.ToArray()
								 select new JMI10010_Member
								 {
									 KEY = q.KEY,
									 コード = q.コード,
									 集計年月 = q.集計年月,
									 登録年月日 = q.登録年月日,
									 自社部門ID = q.自社部門ID,
									 車種ID = q.車種ID,
									 車輌KEY = q.車輌KEY,
									 営業日数 = q.営業日数,
									 稼働日数 = q.稼働日数,
									 走行KM = q.走行KM,
									 実車KM = q.実車KM,
									 輸送屯数 = q.輸送屯数,
									 運送収入 = q.運送収入,
									 燃料L = q.燃料L,
									 一般管理費 = q.一般管理費,
									 拘束時間 = KyoeiSystem.Framework.Common.LinqSub.分TO時間(q.拘束時間),
									 運転時間 = KyoeiSystem.Framework.Common.LinqSub.分TO時間(q.運転時間),
									 高速時間 = KyoeiSystem.Framework.Common.LinqSub.分TO時間(q.高速時間),
									 作業時間 = KyoeiSystem.Framework.Common.LinqSub.分TO時間(q.作業時間),
									 待機時間 = KyoeiSystem.Framework.Common.LinqSub.分TO時間(q.待機時間),
									 休憩時間 = KyoeiSystem.Framework.Common.LinqSub.分TO時間(q.休憩時間),
									 残業時間 = KyoeiSystem.Framework.Common.LinqSub.分TO時間(q.残業時間),
									 深夜時間 = KyoeiSystem.Framework.Common.LinqSub.分TO時間(q.深夜時間),

								 }).ToList();

                for (int i = 0; i < queryLIST.Count; i++)
                {
                    queryLIST2 = query2.ToList();
                    queryLIST2 = queryLIST2.Where(q => q.乗務員KEY == queryLIST[i].コード).ToList();
                    for (int ii = 0; ii < queryLIST2.Count; ii++)
                    {
                        if(queryLIST[i].コード == queryLIST2[ii].乗務員KEY)
                        {
                            if (queryLIST2[ii].回数 != 0)
                            {
                                queryLIST[i].稼働日数 += queryLIST2[ii].回数;
                            }
                        }
                    }
                }

                int[] lst;
                lst = (from q in query  select q.KEY).ToArray();

                //削除行を特定
                var ret = from x in context.S13_DRV
                           where x.集計年月 == i集計年月 && lst.Contains(x.乗務員KEY)
                           select x;

                foreach (var r in ret)
                {
                    context.DeleteObject(r);
                }
                context.SaveChanges();


				do { }
				while ((from x in context.S13_DRV
						where x.集計年月 == i集計年月 && lst.Contains(x.乗務員KEY)
						select x).Count() != 0);


				//sqlbulukcopy準備
				DataTable dt = new DataTable("S13_DRV");
				dt.Columns.Add("乗務員KEY", Type.GetType("System.Int32"));
				dt.Columns.Add("集計年月", Type.GetType("System.Int32"));
				dt.Columns.Add("登録日時", Type.GetType("System.DateTime"));
				dt.Columns.Add("更新日時", Type.GetType("System.DateTime"));
				dt.Columns.Add("自社部門ID", Type.GetType("System.Int32"));
				dt.Columns.Add("車種ID", Type.GetType("System.Int32"));
				dt.Columns.Add("車輌KEY", Type.GetType("System.Int32"));
				dt.Columns.Add("営業日数", Type.GetType("System.Int32"));
				dt.Columns.Add("稼動日数", Type.GetType("System.Int32"));
				dt.Columns.Add("走行ＫＭ", Type.GetType("System.Int32"));
				dt.Columns.Add("実車ＫＭ", Type.GetType("System.Int32"));
				dt.Columns.Add("輸送屯数", Type.GetType("System.Decimal"));
				dt.Columns.Add("運送収入", Type.GetType("System.Decimal"));
				dt.Columns.Add("燃料Ｌ", Type.GetType("System.Decimal"));
				dt.Columns.Add("一般管理費", Type.GetType("System.Decimal"));
				dt.Columns.Add("拘束時間", Type.GetType("System.Decimal"));
				dt.Columns.Add("運転時間", Type.GetType("System.Decimal"));
				dt.Columns.Add("高速時間", Type.GetType("System.Decimal"));
				dt.Columns.Add("作業時間", Type.GetType("System.Decimal"));
				dt.Columns.Add("待機時間", Type.GetType("System.Decimal"));
				dt.Columns.Add("休憩時間", Type.GetType("System.Decimal"));
				dt.Columns.Add("残業時間", Type.GetType("System.Decimal"));
				dt.Columns.Add("深夜時間", Type.GetType("System.Decimal"));

				foreach (var r in queryLIST)
				{
					DataRow dr = dt.NewRow();
					dr[0] = r.KEY;
					dr[1] = r.集計年月;
					dr[2] = (object)r.登録年月日 ?? DBNull.Value;
					dr[3] = DateTime.Now;
					dr[4] = (object)r.自社部門ID ?? DBNull.Value;
					dr[5] = (object)r.車種ID ?? DBNull.Value;
					dr[6] = (object)r.車輌KEY ?? DBNull.Value;
					dr[7] = i営業日数 == null ? 0 : i営業日数;
					dr[8] = (int)(queryLIST.Where(c => c.コード == r.コード).Select(c => c.稼働日数)).FirstOrDefault();
					dr[9] = r.走行KM == null ? 0 : AppCommon.IntParse(r.走行KM.ToString());
					dr[10] = r.実車KM == null ? 0 : AppCommon.IntParse(r.実車KM.ToString());
					dr[11] = r.輸送屯数 == null ? 0 : AppCommon.DecimalParse(r.輸送屯数.ToString());
					dr[12] = r.運送収入 == null ? 0 : AppCommon.DecimalParse(r.運送収入.ToString());
					dr[13] = r.燃料L == null ? 0 : AppCommon.DecimalParse(r.燃料L.ToString());
					dr[14] = r.一般管理費 == null ? 0 : Math.Round(AppCommon.DecimalParse(r.一般管理費.ToString()), 0, MidpointRounding.AwayFromZero);
					dr[15] = r.拘束時間 == null ? 0 : AppCommon.DecimalParse(r.拘束時間.ToString());
					dr[16] = r.運転時間 == null ? 0 : AppCommon.DecimalParse(r.運転時間.ToString());
					dr[17] = r.高速時間 == null ? 0 : AppCommon.DecimalParse(r.高速時間.ToString());
					dr[18] = r.作業時間 == null ? 0 : AppCommon.DecimalParse(r.作業時間.ToString());
					dr[19] = r.待機時間 == null ? 0 : AppCommon.DecimalParse(r.待機時間.ToString());
					dr[20] = r.休憩時間 == null ? 0 : AppCommon.DecimalParse(r.休憩時間.ToString());
					dr[21] = r.残業時間 == null ? 0 : AppCommon.DecimalParse(r.残業時間.ToString());
					dr[22] = r.深夜時間 == null ? 0 : AppCommon.DecimalParse(r.深夜時間.ToString());
					dt.Rows.Add(dr);
				}

				try //SQL_BULK_COPY書込み
				{
					var connect = CommonData.TRAC3_SQL_GetConnectionString();
					using (var bulkCopy = new SqlBulkCopy(connect))
					{
						bulkCopy.DestinationTableName = dt.TableName; // テーブル名をSqlBulkCopyに教える
						bulkCopy.WriteToServer(dt); // bulkCopy実行
					}
				}
				catch (Exception e)
				{
				}


				////データ書き込み
				//foreach (var r in queryLIST.Distinct())
				//{
				//	S13_DRV s13 = new S13_DRV();
				//	s13.乗務員KEY = r.KEY;
				//	s13.集計年月 = r.集計年月;
				//	s13.登録日時 = r.登録年月日;
				//	s13.更新日時 = DateTime.Now;
				//	s13.自社部門ID = r.自社部門ID;
				//	s13.車種ID = r.車種ID;
				//	s13.車輌KEY = r.車輌KEY;
				//	s13.営業日数 = i営業日数 == null ? 0 : i営業日数;
				//	s13.稼動日数 = (int)(queryLIST.Where(c => c.コード == r.コード).Select(c => c.稼働日数)).FirstOrDefault();
				//	s13.走行ＫＭ = r.走行KM == null ? 0 : AppCommon.IntParse(r.走行KM.ToString());
				//	s13.実車ＫＭ = r.実車KM == null ? 0 : AppCommon.IntParse(r.実車KM.ToString());
				//	s13.輸送屯数 = r.輸送屯数 == null ? 0 : AppCommon.DecimalParse(r.輸送屯数.ToString());
				//	s13.運送収入 = r.運送収入 == null ? 0 : Math.Truncate((decimal)r.運送収入);
				//	s13.燃料Ｌ = r.燃料L == null ? 0 : AppCommon.DecimalParse(r.燃料L.ToString());
				//	s13.一般管理費 = r.一般管理費 == null ? 0 : Math.Truncate((decimal)r.一般管理費);
				//	s13.拘束時間 = r.拘束時間 == null ? 0 : AppCommon.DecimalParse(r.拘束時間.ToString());
				//	s13.運転時間 = r.運転時間 == null ? 0 : AppCommon.DecimalParse(r.運転時間.ToString());
				//	s13.高速時間 = r.高速時間 == null ? 0 : AppCommon.DecimalParse(r.高速時間.ToString());
				//	s13.作業時間 = r.作業時間 == null ? 0 : AppCommon.DecimalParse(r.作業時間.ToString());
				//	s13.待機時間 = r.待機時間 == null ? 0 : AppCommon.DecimalParse(r.待機時間.ToString());
				//	s13.休憩時間 = r.休憩時間 == null ? 0 : AppCommon.DecimalParse(r.休憩時間.ToString());
				//	s13.残業時間 = r.残業時間 == null ? 0 : AppCommon.DecimalParse(r.残業時間.ToString());
				//	s13.深夜時間 = r.深夜時間 == null ? 0 : AppCommon.DecimalParse(r.深夜時間.ToString());
				//	context.S13_DRV.ApplyChanges(s13);

				//}
				//context.SaveChanges();



                //経費項目集計↓↓↓↓↓↓↓↓↓↓↓↓↓↓
                lst = (from q in query select q.KEY).ToArray();

				DateTime d前月年月 = d集計年月;
				d前月年月 = d前月年月.AddMonths(-1);
                int i前月年月 = d前月年月.Year * 100 + d前月年月.Month;
                //前月データ取得
                var query_zen = from s13sb in context.S13_DRVSB
                                where s13sb.集計年月 == i前月年月
                                 select s13sb ;

                //削除行を特定
                var ret_tougetu_del = from x in context.S13_DRVSB
                            where x.集計年月 == i集計年月 && lst.Contains(x.乗務員KEY)
                            select x;

				var ret_tougetu = ret_tougetu_del.ToList();
                //当月データを削除
                foreach (var r in ret_tougetu)
                {
                    context.DeleteObject(r);
                }
                context.SaveChanges();


				do { }
				while ((from x in context.S13_DRVSB
                            where x.集計年月 == i集計年月 && lst.Contains(x.乗務員KEY)
                            select x).Count() != 0);

                //当月データ集計
                var ret_k = (from m04 in context.M04_DRV.Where(m04 => m04.削除日付 == null)
                             from m07 in context.M07_KEI
							 join m05 in context.M05_CAR.Where(c => c.削除日付 == null) on m04.乗務員KEY equals m05.乗務員KEY into m05Group
							 //from m05g in m05Group.DefaultIfEmpty()
                             join t02 in context.T03_KTRN.Where(t02 => t02.経費発生日 >= d集計期間From && t02.経費発生日 <= d集計期間To) on new { a = m04.乗務員KEY, b = m07.経費項目ID } equals new { a = (int)t02.乗務員KEY, b = (int)t02.経費項目ID } into t02Group
                             //join s13 in ret_tougetu.Where(s13 => s13.集計年月 == i集計年月) on m04.乗務員KEY equals s13.乗務員KEY into s13Group
                             where lst.Contains(m04.乗務員KEY)
                             select new JMI10010_sbMember
                            {
								金額 = m07.固定変動区分 == 1 ? (t02Group.Sum(t02 => t02.金額) == null ? 0 : (decimal)t02Group.Sum(t02 => t02.金額)) : 0,
								//金額 = m07.固定変動区分 == 1 ? (t02Group.Sum(t02 => t02.金額) == null ? 0 : (decimal)t02Group.Sum(t02 => t02.金額)) : (s13Group.Where(s13 => s13.乗務員KEY == m04.乗務員KEY && s13.経費項目ID == m07.経費項目ID).Sum(s13 => s13.金額) == null ? 0 : s13Group.Where(s13 => s13.乗務員KEY == m04.乗務員KEY && s13.経費項目ID == m07.経費項目ID).Sum(s13 => s13.金額)),
								経費項目ID = m07.経費項目ID,
                                経費項目名 = m07.経費項目名,
                                固定変動区分 = m07.固定変動区分,
                                更新日時 = DateTime.Now,
                                集計年月 = i集計年月,
                                乗務員KEY = m04.乗務員KEY,
								登録日時 = DateTime.Now,
								//登録日時 = s13Group.Min(s13 => s13.更新日時),
								車輌KEY = m05Group.Min(m05 => m05.車輌KEY),
							}).ToList();

				try
				{

					foreach (var row in ret_k)
					{
						if (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Count() == 0)
						{
							row.登録日時 = DateTime.Now;
						}
						else
						{
							row.登録日時 = ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Select(c => c.登録日時).FirstOrDefault();
						}

						if (row.経費項目ID == 102)
						{
							if (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Count() == 0 || i固定再計算 == 1)
							{
								row.金額 = ((context.M05_CAR.Where(m05 => m05.車輌KEY == row.車輌KEY).Select(c => c.固定自動車税)).FirstOrDefault()) == null ? 0 : (decimal)((context.M05_CAR.Where(m05 => m05.車輌KEY == row.車輌KEY).Select(c => c.固定自動車税)).FirstOrDefault());
								row.金額 = Math.Round((row.金額 / 12), 0, MidpointRounding.AwayFromZero);
							}
							else
							{
								row.金額 = (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Select(s13 => s13.金額)).FirstOrDefault();
							}
							continue;
						}
						if (row.経費項目ID == 103)
						{
							if (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Count() == 0 || i固定再計算 == 1)
							{
								row.金額 = ((context.M05_CAR.Where(m05 => m05.車輌KEY == row.車輌KEY).Select(c => c.固定重量税)).FirstOrDefault()) == null ? 0 : (decimal)((context.M05_CAR.Where(m05 => m05.車輌KEY == row.車輌KEY).Select(c => c.固定重量税)).FirstOrDefault());
								row.金額 = Math.Round((row.金額 / 12), 0, MidpointRounding.AwayFromZero);
							}
							else
							{
								row.金額 = (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Select(s13 => s13.金額)).FirstOrDefault();
							}
							continue;
						}
						if (row.経費項目ID == 104)
						{
							if (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Count() == 0 || i固定再計算 == 1)
							{
								row.金額 = ((context.M05_CAR.Where(m05 => m05.車輌KEY == row.車輌KEY).Select(c => c.固定取得税)).FirstOrDefault()) == null ? 0 : (decimal)((context.M05_CAR.Where(m05 => m05.車輌KEY == row.車輌KEY).Select(c => c.固定取得税)).FirstOrDefault());
								row.金額 = Math.Round((row.金額 / 12), 0, MidpointRounding.AwayFromZero);
							}
							else
							{
								row.金額 = (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Select(s13 => s13.金額)).FirstOrDefault();
							}
							continue;
						}
						if (row.経費項目ID == 201)
						{
							if (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Count() == 0 || i固定再計算 == 1)
							{
								row.金額 = ((context.M05_CAR.Where(m05 => m05.車輌KEY == row.車輌KEY).Select(c => c.固定自賠責保険)).FirstOrDefault()) == null ? 0 : (decimal)((context.M05_CAR.Where(m05 => m05.車輌KEY == row.車輌KEY).Select(c => c.固定自賠責保険)).FirstOrDefault());
								row.金額 = Math.Round((row.金額 / 12), 0, MidpointRounding.AwayFromZero);
							}
							else
							{
								row.金額 = (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Select(s13 => s13.金額)).FirstOrDefault();
							}
							continue;
						}
						if (row.経費項目ID == 202)
						{
							if (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Count() == 0 || i固定再計算 == 1)
							{
								row.金額 = ((context.M05_CAR.Where(m05 => m05.車輌KEY == row.車輌KEY).Select(c => c.固定車輌保険)).FirstOrDefault()) == null ? 0 : (decimal)((context.M05_CAR.Where(m05 => m05.車輌KEY == row.車輌KEY).Select(c => c.固定車輌保険)).FirstOrDefault());
								row.金額 = Math.Round((row.金額 / 12), 0, MidpointRounding.AwayFromZero);
							}
							else
							{
								row.金額 = (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Select(s13 => s13.金額)).FirstOrDefault();
							}
							continue;
						}
						if (row.経費項目ID == 203)
						{
							if (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Count() == 0 || i固定再計算 == 1)
							{
								row.金額 = ((context.M05_CAR.Where(m05 => m05.車輌KEY == row.車輌KEY).Select(c => c.固定対人保険)).FirstOrDefault()) == null ? 0 : (decimal)((context.M05_CAR.Where(m05 => m05.車輌KEY == row.車輌KEY).Select(c => c.固定対人保険)).FirstOrDefault());
								row.金額 = Math.Round((row.金額 / 12), 0, MidpointRounding.AwayFromZero);
							}
							else
							{
								row.金額 = (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Select(s13 => s13.金額)).FirstOrDefault();
							}
							continue;
						}
						if (row.経費項目ID == 204)
						{
							if (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Count() == 0 || i固定再計算 == 1)
							{
								row.金額 = ((context.M05_CAR.Where(m05 => m05.車輌KEY == row.車輌KEY).Select(c => c.固定対物保険)).FirstOrDefault()) == null ? 0 : (decimal)((context.M05_CAR.Where(m05 => m05.車輌KEY == row.車輌KEY).Select(c => c.固定対物保険)).FirstOrDefault());
								row.金額 = Math.Round((row.金額 / 12), 0, MidpointRounding.AwayFromZero);
							}
							else
							{
								row.金額 = (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Select(s13 => s13.金額)).FirstOrDefault();
							}
							continue;
						}
						if (row.経費項目ID == 205)
						{
							if (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Count() == 0 || i固定再計算 == 1)
							{
								row.金額 = ((context.M05_CAR.Where(m05 => m05.車輌KEY == row.車輌KEY).Select(c => c.固定貨物保険)).FirstOrDefault()) == null ? 0 : (decimal)((context.M05_CAR.Where(m05 => m05.車輌KEY == row.車輌KEY).Select(c => c.固定貨物保険)).FirstOrDefault());
								row.金額 = Math.Round((row.金額 / 12), 0, MidpointRounding.AwayFromZero);
							}
							else
							{
								row.金額 = (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Select(s13 => s13.金額)).FirstOrDefault();
							}
							continue;
						}
						if (row.経費項目ID == 301)
						{
							if (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Count() == 0 || i固定再計算 == 1)
							{
								row.金額 = ((context.M04_DRV.Where(m04 => m04.乗務員KEY == row.乗務員KEY).Select(c => c.固定給与)).FirstOrDefault()) == null ? 0 : (decimal)((context.M04_DRV.Where(m04 => m04.乗務員KEY == row.乗務員KEY).Select(c => c.固定給与)).FirstOrDefault());
							}
							else
							{
								row.金額 = (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Select(s13 => s13.金額)).FirstOrDefault();
							}
							continue;
						}
						if (row.経費項目ID == 303)
						{
							if (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Count() == 0 || i固定再計算 == 1)
							{
								row.金額 = ((context.M04_DRV.Where(m04 => m04.乗務員KEY == row.乗務員KEY).Select(c => c.固定賞与積立金)).FirstOrDefault()) == null ? 0 : (decimal)((context.M04_DRV.Where(m04 => m04.乗務員KEY == row.乗務員KEY).Select(c => c.固定賞与積立金)).FirstOrDefault());
							}
							else
							{
								row.金額 = (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Select(s13 => s13.金額)).FirstOrDefault();
							}
							continue;
						}
						if (row.経費項目ID == 304)
						{
							if (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Count() == 0 || i固定再計算 == 1)
							{
								row.金額 = ((context.M04_DRV.Where(m04 => m04.乗務員KEY == row.乗務員KEY).Select(c => c.固定福利厚生費)).FirstOrDefault()) == null ? 0 : (decimal)((context.M04_DRV.Where(m04 => m04.乗務員KEY == row.乗務員KEY).Select(c => c.固定福利厚生費)).FirstOrDefault());
							}
							else
							{
								row.金額 = (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Select(s13 => s13.金額)).FirstOrDefault();
							}
							continue;
						}
						if (row.経費項目ID == 305)
						{
							if (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Count() == 0 || i固定再計算 == 1)
							{
								row.金額 = ((context.M04_DRV.Where(m04 => m04.乗務員KEY == row.乗務員KEY).Select(c => c.固定法定福利費)).FirstOrDefault()) == null ? 0 : (decimal)((context.M04_DRV.Where(m04 => m04.乗務員KEY == row.乗務員KEY).Select(c => c.固定法定福利費)).FirstOrDefault());
							}
							else
							{
								row.金額 = (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Select(s13 => s13.金額)).FirstOrDefault();
							}
							continue;
						}
						if (row.経費項目ID == 306)
						{
							if (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Count() == 0 || i固定再計算 == 1)
							{
								row.金額 = ((context.M04_DRV.Where(m04 => m04.乗務員KEY == row.乗務員KEY).Select(c => c.固定退職引当金)).FirstOrDefault()) == null ? 0 : (decimal)((context.M04_DRV.Where(m04 => m04.乗務員KEY == row.乗務員KEY).Select(c => c.固定退職引当金)).FirstOrDefault());
							}
							else
							{
								row.金額 = (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Select(s13 => s13.金額)).FirstOrDefault();
							}
							continue;
						}
						if (row.経費項目ID == 307)
						{
							if (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Count() == 0 || i固定再計算 == 1)
							{
								row.金額 = ((context.M04_DRV.Where(m04 => m04.乗務員KEY == row.乗務員KEY).Select(c => c.固定労働保険)).FirstOrDefault()) == null ? 0 : (decimal)((context.M04_DRV.Where(m04 => m04.乗務員KEY == row.乗務員KEY).Select(c => c.固定労働保険)).FirstOrDefault());
							}
							else
							{
								row.金額 = (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Select(s13 => s13.金額)).FirstOrDefault();
							}
							continue;
						}



						//
						if (row.固定変動区分 == 0)
						{
							if (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Count() == 0 || i固定再計算 == 1)
							{
								row.金額 = query_zen == null ? 0 : query_zen.Where(c => c.乗務員KEY == row.乗務員KEY && c.経費項目ID == row.経費項目ID).Select(c => c.金額).FirstOrDefault();
							}
							else
							{
								row.金額 = ret_tougetu == null ? 0 : (ret_tougetu.Where(s13 => s13.集計年月 == i集計年月 && s13.乗務員KEY == row.乗務員KEY && s13.経費項目ID == row.経費項目ID).Select(s13 => s13.金額)).FirstOrDefault();
							}
						}


					}
				}
				catch (Exception ex)
				{

				}



				//if (i固定再計算 == 1)
				//{
				//	foreach (var r in ret_k)
				//	{
				//		if (r.固定変動区分 == 0)
				//		{
				//			foreach (var r2 in query_zen)
				//			{
				//				if (r.経費項目ID == r2.経費項目ID)
				//				{
				//					r.金額 = r2.金額;
				//				}
				//			}
				//		}
				//	}
				//}

				//ret_k = ret_k.Distinct();


				//sqlbulukcopy準備
				DataTable DRVSB_dt = new DataTable("S13_DRVSB");
				DRVSB_dt.Columns.Add("乗務員KEY", Type.GetType("System.Int32"));
				DRVSB_dt.Columns.Add("集計年月", Type.GetType("System.Int32"));
				DRVSB_dt.Columns.Add("経費項目ID", Type.GetType("System.Int32"));
				DRVSB_dt.Columns.Add("登録日時", Type.GetType("System.DateTime"));
				DRVSB_dt.Columns.Add("更新日時", Type.GetType("System.DateTime"));
				DRVSB_dt.Columns.Add("経費項目名", Type.GetType("System.String"));
				DRVSB_dt.Columns.Add("固定変動区分", Type.GetType("System.Int32"));
				DRVSB_dt.Columns.Add("金額", Type.GetType("System.Decimal"));

				foreach (var r in ret_k)
				{
					DataRow dr = DRVSB_dt.NewRow();
					dr[0] = r.乗務員KEY;
					dr[1] = r.集計年月;
					dr[2] = r.経費項目ID == null ? 0 : AppCommon.DecimalParse(r.経費項目ID.ToString());
					dr[3] = (object)r.登録日時 ?? DBNull.Value;
					dr[4] = (object)r.更新日時 ?? DBNull.Value;
					dr[5] = (object)r.経費項目名 ?? DBNull.Value;
					dr[6] = r.固定変動区分 == null ? 0 : AppCommon.IntParse(r.固定変動区分.ToString());
					dr[7] = r.金額 == null ? 0 : AppCommon.DecimalParse(r.金額.ToString());

					DRVSB_dt.Rows.Add(dr);
				}

				try //SQL_BULK_COPY書込み
				{
					var connect = CommonData.TRAC3_SQL_GetConnectionString();
					using (var bulkCopy = new SqlBulkCopy(connect))
					{
						bulkCopy.DestinationTableName = DRVSB_dt.TableName; // テーブル名をSqlBulkCopyに教える
						bulkCopy.WriteToServer(DRVSB_dt); // bulkCopy実行
					}
				}
				catch (Exception e)
				{
				}


				////データ書き込み
				//foreach (var r in ret_k)
				//{
				//	S13_DRVSB s13sb = new S13_DRVSB();
				//	s13sb.金額 = r.金額;
				//	s13sb.経費項目ID = r.経費項目ID;
				//	s13sb.経費項目名 = r.経費項目名;
				//	s13sb.固定変動区分 = r.固定変動区分;
				//	s13sb.更新日時 = r.更新日時;
				//	s13sb.集計年月 = r.集計年月;
				//	s13sb.乗務員KEY = r.乗務員KEY;
				//	s13sb.登録日時 = r.登録日時;
				//	context.S13_DRVSB.ApplyChanges(s13sb);

				//}
				//context.SaveChanges();

                


                //結果をリスト化
                retList = queryLIST.ToList();
                return retList;

            }
        }
        #endregion

        #region CSV出力
        #endregion
    }
}