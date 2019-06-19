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
    /// SRY02010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class SRY02010_Member
    {
        public DateTime? 日付 { get; set; }
        public string 発地名 { get; set; }
        public string 着地名 { get; set; }
        public string 商品名 { get; set; }
        public decimal? 数量 { get; set; }
        public decimal? 重量 { get; set; }
        public int? 売上金額 { get; set; }
        public int? 割増１ { get; set; }
        public int? 割増２ { get; set; }
        public int? 通行料 { get; set; }
        public string 得意先名 { get; set; }
        public string 備考 { get; set; }
        public string 乗務員名 { get; set; }
        public int? コード { get; set; }
        public string 車輌番号 { get; set; }
        public DateTime 期間From { get; set; }
		public DateTime 期間To { get; set; }
		public int 明細番号 { get; set; }
		public int 明細行 { get; set; }

    }


    /// <summary>
    /// SRY02010  CSV　メンバー
    /// </summary>
    [DataContract]
    public class SRY02010_Member_CSV
    {
        public int? コード { get; set; }
        public string 車輌番号 { get; set; }
        public string 乗務員名 { get; set; }
        public DateTime? 日付 { get; set; }
        public string 発地名 { get; set; }
        public string 着地名 { get; set; }
        public string 商品名 { get; set; }
        public decimal? 数量 { get; set; }
        public decimal? 重量 { get; set; }
        public int? 売上金額 { get; set; }
        public int? 割増１ { get; set; }
        public int? 割増２ { get; set; }
        public int? 通行料 { get; set; }
        public string 得意先名 { get; set; }
        public string 備考 { get; set; }
		public int 明細番号 { get; set; }
		public int 明細行 { get; set; }
	}




    public class SRY02010
    {
        #region 印刷
        /// <summary>
        /// SRY02010 印刷
        /// </summary>
        /// <param name="p商品ID">車輌コード</param>
        /// <returns>T01</returns>
        public List<SRY02010_Member> GetDataList(string p車輌From, string p車輌To, int?[] i車輌List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<SRY02010_Member> retList = new List<SRY02010_Member>();

                context.Connection.Open();

                var query = (from m05 in context.M05_CAR
                             join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) on m05.車輌KEY equals t01.車輌KEY
                             join m01 in context.M01_TOK on t01.得意先KEY equals m01.得意先KEY into m01group
                             from m01G in m01group.DefaultIfEmpty()
                             join m04 in context.M04_DRV on t01.乗務員KEY equals m04.乗務員KEY into m04group
                             from m04G in m04group.DefaultIfEmpty()
                             select new SRY02010_Member
                             {
                                 日付 = t01.請求日付,
                                 発地名 = t01.発地名,
                                 着地名 = t01.着地名,
                                 商品名 = t01.商品名,
                                 数量 = t01.数量,
                                 重量 = t01.重量,
                                 売上金額 = t01.売上金額,
                                 割増１ = t01.請求割増１,
                                 割増２ = t01.請求割増２,
                                 通行料 = t01.通行料,
                                 得意先名 = m01G.略称名,
                                 備考 = t01.請求摘要,
                                 乗務員名 = m04G.乗務員名,
                                 コード = m05.車輌ID,
                                 車輌番号 = m05.車輌番号,
                                 期間From = d集計期間From,
                                 期間To = d集計期間To,
								 明細番号 = t01.明細番号,
								 明細行 = t01.明細行,

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
                        query = query.Union(from m05 in context.M05_CAR
                                            join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) on m05.車輌KEY equals t01.車輌KEY
                                            join m01 in context.M01_TOK on t01.得意先KEY equals m01.得意先KEY into m01group
                                            from m01G in m01group.DefaultIfEmpty()
                                            join m04 in context.M04_DRV on t01.乗務員KEY equals m04.乗務員KEY into m04group
                                            from m04G in m04group.DefaultIfEmpty()
                                            where intCause.Contains(m05.車輌ID)
                                            select new SRY02010_Member
                                            {
                                                日付 = t01.請求日付,
                                                発地名 = t01.発地名,
                                                着地名 = t01.着地名,
                                                商品名 = t01.商品名,
                                                数量 = t01.数量,
                                                重量 = t01.重量,
                                                売上金額 = t01.売上金額,
                                                割増１ = t01.請求割増１,
                                                割増２ = t01.請求割増２,
                                                通行料 = t01.通行料,
                                                得意先名 = m01G.略称名,
                                                備考 = t01.請求摘要,
                                                乗務員名 = m04G.乗務員名,
                                                コード = m05.車輌ID,
                                                車輌番号 = m05.車輌番号,
                                                期間From = d集計期間From,
                                                期間To = d集計期間To,
												明細番号 = t01.明細番号,
												明細行 = t01.明細行,

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
                //結果をリスト化
                query = query.OrderBy(c => c.コード).ThenBy(c => c.日付);
                retList = query.ToList();
                return retList;

            }
        }
        #endregion


        #region CSV出力
        /// <summary>
        /// SRY02010 印刷
        /// </summary>
        /// <param name="p商品ID">車輌コード</param>
        /// <returns>T01</returns>
        public List<SRY02010_Member_CSV> GetDataList_CSV(string p車輌From, string p車輌To, int?[] i車輌List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<SRY02010_Member_CSV> retList = new List<SRY02010_Member_CSV>();

                context.Connection.Open();

                var query = (from m05 in context.M05_CAR
                             join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) on m05.車輌KEY equals t01.車輌KEY
                             join m01 in context.M01_TOK on t01.得意先KEY equals m01.得意先KEY into m01group
                             from m01G in m01group.DefaultIfEmpty()
                             join m04 in context.M04_DRV on t01.乗務員KEY equals m04.乗務員KEY into m04group
                             from m04G in m04group.DefaultIfEmpty()
                             select new SRY02010_Member_CSV
                             {
                                 日付 = t01.請求日付,
                                 発地名 = t01.発地名,
                                 着地名 = t01.着地名,
                                 商品名 = t01.商品名,
                                 数量 = t01.数量,
                                 重量 = t01.重量,
                                 売上金額 = t01.売上金額,
                                 割増１ = t01.請求割増１,
                                 割増２ = t01.請求割増２,
                                 通行料 = t01.通行料,
                                 得意先名 = m01G.略称名,
                                 備考 = t01.請求摘要,
                                 乗務員名 = m04G.乗務員名,
                                 コード = m05.車輌ID,
                                 車輌番号 = m05.車輌番号,
								 明細番号 = t01.明細番号,
								 明細行 = t01.明細行,

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
                        query = query.Union(from m05 in context.M05_CAR
                                            join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1))) on m05.車輌KEY equals t01.車輌KEY
                                            join m01 in context.M01_TOK on t01.得意先KEY equals m01.得意先KEY into m01group
                                            from m01G in m01group.DefaultIfEmpty()
                                            join m04 in context.M04_DRV on t01.乗務員KEY equals m04.乗務員KEY into m04group
                                            from m04G in m04group.DefaultIfEmpty()
                                            where intCause.Contains(m05.車輌ID)
                                            select new SRY02010_Member_CSV
                                            {
                                                日付 = t01.請求日付,
                                                発地名 = t01.発地名,
                                                着地名 = t01.着地名,
                                                商品名 = t01.商品名,
                                                数量 = t01.数量,
                                                重量 = t01.重量,
                                                売上金額 = t01.売上金額,
                                                割増１ = t01.請求割増１,
                                                割増２ = t01.請求割増２,
                                                通行料 = t01.通行料,
                                                得意先名 = m01G.略称名,
                                                備考 = t01.請求摘要,
                                                乗務員名 = m04G.乗務員名,
                                                コード = m05.車輌ID,
                                                車輌番号 = m05.車輌番号,
												明細番号 = t01.明細番号,
												明細行 = t01.明細行,

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
                query = query.OrderBy(c => c.コード).ThenBy(c => c.日付);
                //結果をリスト化
                retList = query.ToList();
                return retList;

            }
        }


            #endregion
    }
}