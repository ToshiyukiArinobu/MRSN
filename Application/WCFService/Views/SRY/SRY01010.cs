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
using KyoeiSystem.Framework.Common;

namespace KyoeiSystem.Application.WCFService
{

    /// <summary>
    /// SRY01010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class SRY01010_Member
    {
        public DateTime? 出庫日 { get; set; }
        public DateTime? 帰庫日 { get; set; }
        public decimal? 出庫H { get; set; }
        public decimal? 帰庫H { get; set; }
        public string 乗務員名 { get; set; }
        public string 出庫区分 { get; set; }
        public decimal? 水揚金額 { get; set; }
        public int? 経費1 { get; set; }
        public int? 経費2 { get; set; }
        public int? 経費3 { get; set; }
        public int? 経費4 { get; set; }
        public int? 経費5 { get; set; }
        public int? 経費6 { get; set; }
        public int? 経費7 { get; set; }
		public string 経費名1 { get; set; }
		public string 経費名2 { get; set; }
		public string 経費名3 { get; set; }
		public string 経費名4 { get; set; }
		public string 経費名5 { get; set; }
		public string 経費名6 { get; set; }
		public string 経費名7 { get; set; }
		public int? その他経費 { get; set; }
        public decimal? 拘束H { get; set; }
        public decimal? 運転H { get; set; }
        public decimal? 高速H { get; set; }
        public decimal? 作業H { get; set; }
        public decimal? 待機H { get; set; }
        public decimal? 休憩H { get; set; }
        public decimal? 残業H { get; set; }
        public decimal? 深夜H { get; set; }
        public decimal? 燃料L数 { get; set; }
        public int? 燃料代 { get; set; }
        public int 走行KM { get; set; }
        public int 実車KM { get; set; }
        public decimal? 輸送屯数 { get; set; }


        public int? コード { get; set; }
        public string 車輌番号 { get; set; }
        public DateTime 期間From { get; set; }
        public DateTime 期間To { get; set; }
        public string IDFrom { get; set; }
        public string IDTo { get; set; }
        public string IDList { get; set; }
        
    }


    /// <summary>
    /// SRY01010  CSV　メンバー
    /// </summary>
    [DataContract]
    public class SRY01010_Member_CSV
    {
        public int? コード { get; set; }
        public string 車輌番号 { get; set; }
        public DateTime? 出庫日 { get; set; }
        public DateTime? 帰庫日 { get; set; }
        public decimal? 出庫H { get; set; }
        public decimal? 帰庫H { get; set; }
        public string 乗務員名 { get; set; }
        public string 出庫区分 { get; set; }
        public decimal? 水揚金額 { get; set; }
        public int? 経費1 { get; set; }
        public int? 経費2 { get; set; }
        public int? 経費3 { get; set; }
        public int? 経費4 { get; set; }
        public int? 経費5 { get; set; }
        public int? 経費6 { get; set; }
        public int? 経費7 { get; set; }
        public int? その他経費 { get; set; }
        public decimal? 拘束H { get; set; }
        public decimal? 運転H { get; set; }
        public decimal? 高速H { get; set; }
        public decimal? 作業H { get; set; }
        public decimal? 待機H { get; set; }
        public decimal? 休憩H { get; set; }
        public decimal? 残業H { get; set; }
        public decimal? 深夜H { get; set; }
        public decimal? 燃料L数 { get; set; }
        public int? 燃料代 { get; set; }
        public int 走行KM { get; set; }
        public int 実車KM { get; set; }
        public decimal? 輸送屯数 { get; set; }
        
    }




    public class SRY01010
    {
        #region 印刷
        /// <summary>
        /// SRY01010 印刷
        /// </summary>
        /// <param name="p商品ID">車輌コード</param>
        /// <returns>T01</returns>
        public List<SRY01010_Member> GetDataList(string p車輌From, string p車輌To, int?[] i車輌List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string sIDList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<SRY01010_Member> retList = new List<SRY01010_Member>();

                context.Connection.Open();

                var query = (from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To)
                             join t03 in context.T03_KTRN on t02.明細番号 equals t03.明細番号 into t03Group
                             join t01 in context.T01_TRN.Where(t01 => (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1 )) on t02.明細番号 equals t01.明細番号 into t01Group
                             join m04 in context.M04_DRV on t02.乗務員KEY equals m04.乗務員KEY into m04Group
                             from m04g in m04Group.DefaultIfEmpty()
                             join m78 in context.M78_SYK on t02.出勤区分ID equals m78.出勤区分ID into m78Group
                             from m78g in m78Group.DefaultIfEmpty()
                             join m05 in context.M05_CAR on t02.車輌KEY equals m05.車輌KEY into m05Group
                             from m05g in m05Group.DefaultIfEmpty()
                             where m05g.車輌KEY != null
                             select new SRY01010_Member
                             {
                                 出庫日 = t02.実運行日開始,
                                 帰庫日 = t02.実運行日終了,
                                 出庫H = t02.出庫時間,
                                 帰庫H = t02.帰庫時間,
                                 乗務員名 = m04g.乗務員名,
                                 出庫区分 = m78g.出勤区分名,
                                 水揚金額 = t01Group.Sum(s => s.水揚金額),
                                 経費1 = t03Group.Where(w => w.経費項目ID == 601).Sum(s => s.金額),
                                 経費2 = t03Group.Where(w => w.経費項目ID == 602).Sum(s => s.金額),
                                 経費3 = t03Group.Where(w => w.経費項目ID == 603).Sum(s => s.金額),
                                 経費4 = t03Group.Where(w => w.経費項目ID == 604).Sum(s => s.金額),
                                 経費5 = t03Group.Where(w => w.経費項目ID == 605).Sum(s => s.金額),
                                 経費6 = t03Group.Where(w => w.経費項目ID == 606).Sum(s => s.金額),
                                 経費7 = t03Group.Where(w => w.経費項目ID == 607).Sum(s => s.金額),
                                 その他経費 = t03Group.Where(w => w.経費項目ID != 601 && w.経費項目ID != 602 && w.経費項目ID != 603 && w.経費項目ID != 604
                                                            && w.経費項目ID != 605 && w.経費項目ID != 606 && w.経費項目ID != 607
                                                            && w.経費項目ID != 401).Sum(s => s.金額),
								 経費名1 = (from m07 in context.M07_KEI.Where(c => c.経費項目ID == 601) select m07.経費項目名).FirstOrDefault(),
								 経費名2 = (from m07 in context.M07_KEI.Where(c => c.経費項目ID == 602) select m07.経費項目名).FirstOrDefault(),
								 経費名3 = (from m07 in context.M07_KEI.Where(c => c.経費項目ID == 603) select m07.経費項目名).FirstOrDefault(),
								 経費名4 = (from m07 in context.M07_KEI.Where(c => c.経費項目ID == 604) select m07.経費項目名).FirstOrDefault(),
								 経費名5 = (from m07 in context.M07_KEI.Where(c => c.経費項目ID == 605) select m07.経費項目名).FirstOrDefault(),
								 経費名6 = (from m07 in context.M07_KEI.Where(c => c.経費項目ID == 606) select m07.経費項目名).FirstOrDefault(),
								 経費名7 = (from m07 in context.M07_KEI.Where(c => c.経費項目ID == 607) select m07.経費項目名).FirstOrDefault(),
								 拘束H = t02.拘束時間,
                                 運転H = t02.運転時間,
                                 高速H = t02.高速時間,
                                 作業H = t02.作業時間,
                                 待機H = t02.待機時間,
                                 休憩H = t02.休憩時間,
                                 残業H = t02.残業時間,
                                 深夜H = t02.深夜時間,
                                 燃料L数 = t03Group.Where(w => w.経費項目ID == 401).Sum(s => s.数量),
                                 燃料代 = t03Group.Where(w => w.経費項目ID == 401).Sum(s => s.金額),
                                 走行KM = t02.走行ＫＭ,
                                 実車KM = t02.実車ＫＭ,
                                 輸送屯数 = t02.輸送屯数,

                                 コード = m05g.車輌ID,
                                 車輌番号 = m05g.車輌番号,
                                 期間From = d集計期間From,
                                 期間To = d集計期間To,
                                 IDFrom = p車輌From,
                                 IDTo = p車輌To,
                                 IDList = sIDList,
                                 
                             }).AsQueryable();

                if (!(string.IsNullOrEmpty(p車輌From + p車輌To) && i車輌List.Length == 0))
                {

                    //車輌が検索対象に入っていない時全データ取得
                    if (string.IsNullOrEmpty(p車輌From + p車輌To))
                    {
                        query = query.Where(c => c.コード >= int.MaxValue);
                    }

                    //車輌From処理　Min値
                    if (!string.IsNullOrEmpty(p車輌From))
                    {
                        int i車輌FROM = AppCommon.IntParse(p車輌From);
                        query = query.Where(c => c.コード >= i車輌FROM);
                    }

                    //車輌To処理　Max値
                    if (!string.IsNullOrEmpty(p車輌To))
                    {
                        int i車輌TO = AppCommon.IntParse(p車輌To);
                        query = query.Where(c => c.コード <= i車輌TO);
                    }


                    if (i車輌List.Length > 0)
                    {
                        var intCause = i車輌List;


                        query = query.Union(from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To && t02.車輌KEY != null)
                                     join t03 in context.T03_KTRN on t02.明細番号 equals t03.明細番号 into t03Group
                                     join t01 in context.T01_TRN.Where(t01 => (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1)) on t02.明細番号 equals t01.明細番号 into t01Group
                                     join m04 in context.M04_DRV on t02.乗務員KEY equals m04.乗務員KEY into m04Group
                                     from m04g in m04Group.DefaultIfEmpty()
                                     join m78 in context.M78_SYK on t02.出勤区分ID equals m78.出勤区分ID into m78Group
                                     from m78g in m78Group.DefaultIfEmpty()
                                     join m05 in context.M05_CAR on t02.車輌KEY equals m05.車輌KEY into m05Group
                                     from m05g in m05Group.DefaultIfEmpty()
                                     where intCause.Contains(m05g.車輌ID) && m05g.車輌KEY != null
                                     select new SRY01010_Member
                                     {
                                         出庫日 = t02.実運行日開始,
                                         帰庫日 = t02.実運行日終了,
                                         出庫H = t02.出庫時間,
                                         帰庫H = t02.帰庫時間,
                                         乗務員名 = m04g.乗務員名,
                                         出庫区分 = m78g.出勤区分名,
                                         水揚金額 = t01Group.Sum(s => s.水揚金額),
                                         経費1 = t03Group.Where(w => w.経費項目ID == 601).Sum(s => s.金額),
                                         経費2 = t03Group.Where(w => w.経費項目ID == 602).Sum(s => s.金額),
                                         経費3 = t03Group.Where(w => w.経費項目ID == 603).Sum(s => s.金額),
                                         経費4 = t03Group.Where(w => w.経費項目ID == 604).Sum(s => s.金額),
                                         経費5 = t03Group.Where(w => w.経費項目ID == 605).Sum(s => s.金額),
                                         経費6 = t03Group.Where(w => w.経費項目ID == 606).Sum(s => s.金額),
                                         経費7 = t03Group.Where(w => w.経費項目ID == 607).Sum(s => s.金額),
                                         その他経費 = t03Group.Where(w => w.経費項目ID != 601 && w.経費項目ID != 602 && w.経費項目ID != 603 && w.経費項目ID != 604
                                                                    && w.経費項目ID != 605 && w.経費項目ID != 606 && w.経費項目ID != 607
																	&& w.経費項目ID != 401).Sum(s => s.金額),
										 経費名1 = (from m07 in context.M07_KEI.Where(c => c.経費項目ID == 601) select m07.経費項目名).FirstOrDefault(),
										 経費名2 = (from m07 in context.M07_KEI.Where(c => c.経費項目ID == 602) select m07.経費項目名).FirstOrDefault(),
										 経費名3 = (from m07 in context.M07_KEI.Where(c => c.経費項目ID == 603) select m07.経費項目名).FirstOrDefault(),
										 経費名4 = (from m07 in context.M07_KEI.Where(c => c.経費項目ID == 604) select m07.経費項目名).FirstOrDefault(),
										 経費名5 = (from m07 in context.M07_KEI.Where(c => c.経費項目ID == 605) select m07.経費項目名).FirstOrDefault(),
										 経費名6 = (from m07 in context.M07_KEI.Where(c => c.経費項目ID == 606) select m07.経費項目名).FirstOrDefault(),
										 経費名7 = (from m07 in context.M07_KEI.Where(c => c.経費項目ID == 607) select m07.経費項目名).FirstOrDefault(),
                                         拘束H = t02.拘束時間,
                                         運転H = t02.運転時間,
                                         高速H = t02.高速時間,
                                         作業H = t02.作業時間,
                                         待機H = t02.待機時間,
                                         休憩H = t02.休憩時間,
                                         残業H = t02.残業時間,
                                         深夜H = t02.深夜時間,
                                         燃料L数 = t03Group.Where(w => w.経費項目ID == 401).Sum(s => s.数量),
                                         燃料代 = t03Group.Where(w => w.経費項目ID == 401).Sum(s => s.金額),
                                         走行KM = t02.走行ＫＭ,
                                         実車KM = t02.実車ＫＭ,
                                         輸送屯数 = t02.輸送屯数,

                                         コード = m05g.車輌ID,
                                         車輌番号 = m05g.車輌番号,
                                         期間From = d集計期間From,
                                         期間To = d集計期間To,
                                         IDFrom = p車輌From,
                                         IDTo = p車輌To,
                                         IDList = sIDList,

                                     });
                    }
                }
                query = query.Distinct();
                //結果をリスト化
                query = query.OrderBy(c => c.コード).ThenBy(c => c.出庫日);

				//retList = query.ToList();
				foreach (var rec in query)
				{
					// 各時間項目の時分を、分に変換する。
					rec.拘束H = LinqSub.時間TO分(rec.拘束H);
					rec.運転H = LinqSub.時間TO分(rec.運転H);
					rec.高速H = LinqSub.時間TO分(rec.高速H);
					rec.作業H = LinqSub.時間TO分(rec.作業H);
					rec.待機H = LinqSub.時間TO分(rec.待機H);
					rec.休憩H = LinqSub.時間TO分(rec.休憩H);
					rec.残業H = LinqSub.時間TO分(rec.残業H);
					rec.深夜H = LinqSub.時間TO分(rec.深夜H);

					retList.Add(rec);
				}


                return retList;

            }
        }
        #endregion


        #region CSV出力

        /// <summary>
        /// SRY01010 印刷
        /// </summary>
        /// <param name="p商品ID">車輌コード</param>
        /// <returns>T01</returns>
        public List<SRY01010_Member_CSV> GetDataList_CSV(string p車輌From, string p車輌To, int?[] i車輌List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string sIDList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<SRY01010_Member_CSV> retList = new List<SRY01010_Member_CSV>();

                context.Connection.Open();

                var query = (from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To)
                             join t03 in context.T03_KTRN on t02.明細番号 equals t03.明細番号 into t03Group
                             join t01 in context.T01_TRN.Where(t01 => (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1)) on t02.明細番号 equals t01.明細番号 into t01Group
                             join m04 in context.M04_DRV on t02.乗務員KEY equals m04.乗務員KEY into m04Group
                             from m04g in m04Group.DefaultIfEmpty()
                             join m78 in context.M78_SYK on t02.出勤区分ID equals m78.出勤区分ID into m78Group
                             from m78g in m78Group.DefaultIfEmpty()
                             join m05 in context.M05_CAR on t02.車輌KEY equals m05.車輌KEY into m05Group
                             from m05g in m05Group.DefaultIfEmpty()
                             where m05g.車輌KEY != null
                             select new SRY01010_Member_CSV
                             {
                                 出庫日 = t02.実運行日開始,
                                 帰庫日 = t02.実運行日終了,
                                 出庫H = t02.出庫時間,
                                 帰庫H = t02.帰庫時間,
                                 乗務員名 = m04g.乗務員名,
                                 出庫区分 = m78g.出勤区分名,
                                 水揚金額 = t01Group.Sum(s => s.水揚金額),
                                 経費1 = t03Group.Where(w => w.経費項目ID == 601).Sum(s => s.金額),
                                 経費2 = t03Group.Where(w => w.経費項目ID == 602).Sum(s => s.金額),
                                 経費3 = t03Group.Where(w => w.経費項目ID == 603).Sum(s => s.金額),
                                 経費4 = t03Group.Where(w => w.経費項目ID == 604).Sum(s => s.金額),
                                 経費5 = t03Group.Where(w => w.経費項目ID == 605).Sum(s => s.金額),
                                 経費6 = t03Group.Where(w => w.経費項目ID == 606).Sum(s => s.金額),
                                 経費7 = t03Group.Where(w => w.経費項目ID == 607).Sum(s => s.金額),
                                 その他経費 = t03Group.Where(w => w.経費項目ID != 601 && w.経費項目ID != 602 && w.経費項目ID != 603 && w.経費項目ID != 604
                                                            && w.経費項目ID != 605 && w.経費項目ID != 606 && w.経費項目ID != 607
                                                            && w.経費項目ID != 401).Sum(s => s.金額),
                                 拘束H = t02.拘束時間,
                                 運転H = t02.運転時間,
                                 高速H = t02.高速時間,
                                 作業H = t02.作業時間,
                                 待機H = t02.待機時間,
                                 休憩H = t02.休憩時間,
                                 残業H = t02.残業時間,
                                 深夜H = t02.深夜時間,
                                 燃料L数 = t03Group.Where(w => w.経費項目ID == 401).Sum(s => s.数量),
                                 燃料代 = t03Group.Where(w => w.経費項目ID == 401).Sum(s => s.金額),
                                 走行KM = t02.走行ＫＭ,
                                 実車KM = t02.実車ＫＭ,
                                 輸送屯数 = t02.輸送屯数,

                                 コード = m05g.車輌ID,
                                 車輌番号 = m05g.車輌番号,

                             }).AsQueryable();

                if (!(string.IsNullOrEmpty(p車輌From + p車輌To) && i車輌List.Length == 0))
                {

                    //車輌が検索対象に入っていない時全データ取得
                    if (string.IsNullOrEmpty(p車輌From + p車輌To))
                    {
                        query = query.Where(c => c.コード >= int.MaxValue);
                    }

                    //車輌From処理　Min値
                    if (!string.IsNullOrEmpty(p車輌From))
                    {
                        int i車輌FROM = AppCommon.IntParse(p車輌From);
                        query = query.Where(c => c.コード >= i車輌FROM);
                    }

                    //車輌To処理　Max値
                    if (!string.IsNullOrEmpty(p車輌To))
                    {
                        int i車輌TO = AppCommon.IntParse(p車輌To);
                        query = query.Where(c => c.コード <= i車輌TO);
                    }


                    if (i車輌List.Length > 0)
                    {
                        var intCause = i車輌List;


                        query = query.Union(from t02 in context.T02_UTRN.Where(t02 => t02.労務日 >= d集計期間From && t02.労務日 <= d集計期間To && t02.車輌KEY != null)
                                            join t03 in context.T03_KTRN on t02.明細番号 equals t03.明細番号 into t03Group
                                            join t01 in context.T01_TRN.Where(t01 => (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1)) on t02.明細番号 equals t01.明細番号 into t01Group
                                            join m04 in context.M04_DRV on t02.乗務員KEY equals m04.乗務員KEY into m04Group
                                            from m04g in m04Group.DefaultIfEmpty()
                                            join m78 in context.M78_SYK on t02.出勤区分ID equals m78.出勤区分ID into m78Group
                                            from m78g in m78Group.DefaultIfEmpty()
                                            join m05 in context.M05_CAR on t02.車輌KEY equals m05.車輌KEY into m05Group
                                            from m05g in m05Group.DefaultIfEmpty()
                                            where intCause.Contains(m05g.車輌ID) && m05g.車輌KEY != null
                                            select new SRY01010_Member_CSV
                                            {
                                                出庫日 = t02.実運行日開始,
                                                帰庫日 = t02.実運行日終了,
                                                出庫H = t02.出庫時間,
                                                帰庫H = t02.帰庫時間,
                                                乗務員名 = m04g.乗務員名,
                                                出庫区分 = m78g.出勤区分名,
                                                水揚金額 = t01Group.Sum(s => s.水揚金額),
                                                経費1 = t03Group.Where(w => w.経費項目ID == 601).Sum(s => s.金額),
                                                経費2 = t03Group.Where(w => w.経費項目ID == 602).Sum(s => s.金額),
                                                経費3 = t03Group.Where(w => w.経費項目ID == 603).Sum(s => s.金額),
                                                経費4 = t03Group.Where(w => w.経費項目ID == 604).Sum(s => s.金額),
                                                経費5 = t03Group.Where(w => w.経費項目ID == 605).Sum(s => s.金額),
                                                経費6 = t03Group.Where(w => w.経費項目ID == 606).Sum(s => s.金額),
                                                経費7 = t03Group.Where(w => w.経費項目ID == 607).Sum(s => s.金額),
                                                その他経費 = t03Group.Where(w => w.経費項目ID != 601 && w.経費項目ID != 602 && w.経費項目ID != 603 && w.経費項目ID != 604
                                                                           && w.経費項目ID != 605 && w.経費項目ID != 606 && w.経費項目ID != 607
                                                                           && w.経費項目ID != 401).Sum(s => s.金額),
                                                拘束H = t02.拘束時間,
                                                運転H = t02.運転時間,
                                                高速H = t02.高速時間,
                                                作業H = t02.作業時間,
                                                待機H = t02.待機時間,
                                                休憩H = t02.休憩時間,
                                                残業H = t02.残業時間,
                                                深夜H = t02.深夜時間,
                                                燃料L数 = t03Group.Where(w => w.経費項目ID == 401).Sum(s => s.数量),
                                                燃料代 = t03Group.Where(w => w.経費項目ID == 401).Sum(s => s.金額),
                                                走行KM = t02.走行ＫＭ,
                                                実車KM = t02.実車ＫＭ,
                                                輸送屯数 = t02.輸送屯数,

                                                コード = m05g.車輌ID,
                                                車輌番号 = m05g.車輌番号,

                                            });
                        ////車輌From処理　Min値
                        //if (!string.IsNullOrEmpty(p車輌From))
                        //{
                        //    int i車輌FROM = AppCommon.IntParse(p車輌From);
                        //    query = query.Where(c => c.コード >= i車輌FROM);
                        //}

                        ////車輌To処理　Max値
                        //if (!string.IsNullOrEmpty(p車輌To))
                        //{
                        //    int i車輌TO = AppCommon.IntParse(p車輌To);
                        //    query = query.Where(c => c.コード <= i車輌TO);
                        //}


                        //else
                        //{
                        //    query = query.Where(c => c.コード > int.MinValue && c.コード < int.MaxValue);
                        //}

                    }
                }
                query = query.Distinct();
                query = query.OrderBy(c => c.コード).ThenBy(c => c.出庫日);
                //結果をリスト化
                retList = query.ToList();
                return retList;

            }
        }


            #endregion
    }
}