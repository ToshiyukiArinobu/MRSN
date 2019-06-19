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
    /// JMI05010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class JMI05010_Member
    {

        public DateTime? 日付 { get; set; }
        public string 車番 { get; set; }
        public string 発地名 { get; set; }
        public string 着地名 { get; set; }
        public string 商品名 { get; set; }
        public decimal? 数量 { get; set; }
        public decimal? 重量 { get; set; }
        public int? 社内金額 { get; set; }
		public int? 社内立替 { get; set; }
		public string 得意先名 { get; set; }
        public string 備考 { get; set; }
		public int? 売上金額 { get; set; }
		public int? 通行料 { get; set; }
		public int? 明細番号 { get; set; }
		public int? 行 { get; set; }

        public int? コード { get; set; }
        public string 乗務員名 { get; set; }
        public DateTime 期間From { get; set; }
        public DateTime 期間To { get; set; }
		public string IDList { get; set; }
		

    }


    /// <summary>
    /// JMI05010  CSV　メンバー
    /// </summary>
    [DataContract]
    public class JMI05010_Member_CSV
    {
        public int? コード { get; set; }
        public string 乗務員名 { get; set; }
        public DateTime? 日付 { get; set; }
        public string 車番 { get; set; }
        public string 発地名 { get; set; }
        public string 着地名 { get; set; }
        public string 商品名 { get; set; }
        public decimal? 数量 { get; set; }
        public decimal? 重量 { get; set; }
		public int? 社内金額 { get; set; }
		public int? 社内立替 { get; set; }
		public string 得意先名 { get; set; }
        public string 備考 { get; set; }
        public int? 売上金額 { get; set; }
        public int? 通行料 { get; set; }
		public int? 明細番号 { get; set; }
		public int? 行 { get; set; }


    }


    [DataContract]
    public class JMI05010_M07
    {
        [DataMember]
        public int 経費ID { get; set; }
        [DataMember]
        public string 経費名 { get; set; }
    }


    public class JMI05010
    {
        #region 印刷
        /// <summary>
        /// JMI05010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
		public List<JMI05010_Member> GetDataList(string p乗務員From, string p乗務員To, int?[] i乗務員List, int? p作成締日, DateTime? d集計期間From, DateTime? d集計期間To, string p作成年度)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<JMI05010_Member> retList = new List<JMI05010_Member>();

                context.Connection.Open();

                var query = (from t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1)))
                             join m04 in context.M04_DRV on t01.乗務員KEY equals m04.乗務員KEY into m04Group
                             from m04G in m04Group.DefaultIfEmpty()
                             join m01 in context.M01_TOK on t01.得意先KEY equals m01.得意先KEY into m01Group
                             from m01G in m01Group.DefaultIfEmpty()
                             where m04G.乗務員KEY != null
                             select new JMI05010_Member
                             {
                                 日付 = t01.請求日付,
                                 車番 = t01.車輌番号,
                                 発地名 = t01.発地名,
                                 着地名 = t01.着地名,
                                 商品名 = t01.商品名,
                                 数量 = t01.数量,
                                 重量 = t01.重量,
                                 社内金額 = t01.支払金額,
								 社内立替 = t01.支払通行料,
                                 得意先名 = m01G.略称名,
                                 備考 = t01.請求摘要,
								 売上金額 = t01.売上金額 + t01.請求割増１ + t01.請求割増２,
                                 通行料 = t01.通行料,
                                 コード = m04G.乗務員ID,
                                 乗務員名 = m04G.乗務員名,
                                 期間From = (DateTime)d集計期間From,
								 期間To = (DateTime)d集計期間To,
								 明細番号 = t01.明細番号,
								 行 = t01.明細行,

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
                        query = query.Union(from t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1)))
                                     join m04 in context.M04_DRV on t01.乗務員KEY equals m04.乗務員KEY into m04Group
                                     from m04G in m04Group.DefaultIfEmpty()
                                     join m01 in context.M01_TOK on t01.得意先KEY equals m01.得意先KEY into m01Group
                                     from m01G in m01Group.DefaultIfEmpty()
                                     where intCause.Contains(m04G.乗務員ID) && m04G.乗務員KEY != null
                                     select new JMI05010_Member
                                     {
                                         日付 = t01.請求日付,
                                         車番 = t01.車輌番号,
                                         発地名 = t01.発地名,
                                         着地名 = t01.着地名,
                                         商品名 = t01.商品名,
                                         数量 = t01.数量,
                                         重量 = t01.重量,
                                         社内金額 = t01.支払金額,
										 社内立替 = t01.支払通行料,
										 得意先名 = m01G.略称名,
                                         備考 = t01.請求摘要,
										 売上金額 = t01.売上金額 + t01.請求割増１ + t01.請求割増２,
                                         通行料 = t01.通行料,
                                         コード = m04G.乗務員ID,
                                         乗務員名 = m04G.乗務員名,
										 期間From = (DateTime)d集計期間From,
										 期間To = (DateTime)d集計期間To,
										 明細番号 = t01.明細番号,
										 行 = t01.明細行,

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


        #region 印刷
        /// <summary>
        /// JMI05010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<JMI05010_Member> GetDataList_Syagai(string p乗務員From, string p乗務員To, int?[] i乗務員List, int? p作成締日, DateTime? d集計期間From, DateTime? d集計期間To, string p作成年度)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<JMI05010_Member> retList = new List<JMI05010_Member>();

                context.Connection.Open();

                var query = (from t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1)))
                             join m04 in context.M04_DRV on t01.乗務員KEY equals m04.乗務員KEY into m04Group
                             from m04G in m04Group.DefaultIfEmpty()
                             join m01 in context.M01_TOK on t01.得意先KEY equals m01.得意先KEY into m01Group
                             from m01G in m01Group.DefaultIfEmpty()
                             where m04G.乗務員KEY != null
                             select new JMI05010_Member
                             {
                                 日付 = t01.請求日付,
                                 車番 = t01.車輌番号,
                                 発地名 = t01.発地名,
                                 着地名 = t01.着地名,
                                 商品名 = t01.商品名,
                                 数量 = t01.数量,
                                 重量 = t01.重量,
                                 社内金額 = t01.支払金額,
								 社内立替 = t01.支払通行料,
								 得意先名 = m01G.略称名,
                                 備考 = t01.請求摘要,
								 売上金額 = t01.売上金額 + t01.請求割増１ + t01.請求割増２,
                                 通行料 = t01.通行料,
                                 コード = m04G.乗務員ID,
                                 乗務員名 = m04G.乗務員名,
								 期間From = (DateTime)d集計期間From,
								 期間To = (DateTime)d集計期間To,
								 明細番号 = t01.明細番号,
								 行 = t01.明細行,

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
                        query = query.Union(from t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1)))
                                     join m04 in context.M04_DRV on t01.乗務員KEY equals m04.乗務員KEY into m04Group
                                     from m04G in m04Group.DefaultIfEmpty()
                                     join m01 in context.M01_TOK on t01.得意先KEY equals m01.得意先KEY into m01Group
                                     from m01G in m01Group.DefaultIfEmpty()
                                     where intCause.Contains(m04G.乗務員ID) && m04G.乗務員KEY != null
                                     select new JMI05010_Member
                                     {
                                         日付 = t01.請求日付,
                                         車番 = t01.車輌番号,
                                         発地名 = t01.発地名,
                                         着地名 = t01.着地名,
                                         商品名 = t01.商品名,
                                         数量 = t01.数量,
                                         重量 = t01.重量,
                                         社内金額 = t01.支払金額,
										 社内立替 = t01.支払通行料,
										 得意先名 = m01G.略称名,
                                         備考 = t01.請求摘要,
										 売上金額 = t01.売上金額 + t01.請求割増１ + t01.請求割増２,
                                         通行料 = t01.通行料,
                                         コード = m04G.乗務員ID,
                                         乗務員名 = m04G.乗務員名,
										 期間From = (DateTime)d集計期間From,
										 期間To = (DateTime)d集計期間To,
										 明細番号 = t01.明細番号,
										 行 = t01.明細行,

                                     });
                    }
                    else
                    {
                        query = query.Where(c => c.コード > int.MinValue && c.コード < int.MaxValue);
                    }

                }

                query = query.Distinct();
				query = query.OrderBy(c => c.日付);
                //結果をリスト化
                retList = query.ToList();
                return retList;
            }

        }
        #endregion



        #region CSV出力

        /// <summary>
        /// JMI05010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<JMI05010_Member_CSV> GetDataList_CSV(string p乗務員From, string p乗務員To, int?[] i乗務員List, int? p作成締日, DateTime? d集計期間From, DateTime? d集計期間To, string p作成年度)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<JMI05010_Member_CSV> retList = new List<JMI05010_Member_CSV>();

                context.Connection.Open();

                var query = (from t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1)))
                             join m04 in context.M04_DRV on t01.乗務員KEY equals m04.乗務員KEY into m04Group
                             from m04G in m04Group.DefaultIfEmpty()
                             join m01 in context.M01_TOK on t01.得意先KEY equals m01.得意先KEY into m01Group
                             from m01G in m01Group.DefaultIfEmpty()
                             where m04G.乗務員KEY != null
                             select new JMI05010_Member_CSV
                             {
                                 日付 = t01.請求日付,
                                 車番 = t01.車輌番号,
                                 発地名 = t01.発地名,
                                 着地名 = t01.着地名,
                                 商品名 = t01.商品名,
                                 数量 = t01.数量,
                                 重量 = t01.重量,
                                 社内金額 = t01.支払金額,
								 社内立替 = t01.支払通行料,
								 得意先名 = m01G.略称名,
                                 備考 = t01.請求摘要,
								 売上金額 = t01.売上金額 + t01.請求割増１ + t01.請求割増２,
                                 通行料 = t01.通行料,
                                 コード = m04G.乗務員ID,
                                 乗務員名 = m04G.乗務員名,
								 明細番号 = t01.明細番号,
								 行 = t01.明細行,

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
                        query = query.Union(from t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1)))
                                            join m04 in context.M04_DRV on t01.乗務員KEY equals m04.乗務員KEY into m04Group
                                            from m04G in m04Group.DefaultIfEmpty()
                                            join m01 in context.M01_TOK on t01.得意先KEY equals m01.得意先KEY into m01Group
                                            from m01G in m01Group.DefaultIfEmpty()
                                            where intCause.Contains(m04G.乗務員ID) && m04G.乗務員KEY != null
                                            select new JMI05010_Member_CSV
                                            {
                                                日付 = t01.請求日付,
                                                車番 = t01.車輌番号,
                                                発地名 = t01.発地名,
                                                着地名 = t01.着地名,
                                                商品名 = t01.商品名,
                                                数量 = t01.数量,
                                                重量 = t01.重量,
                                                社内金額 = t01.支払金額,
												社内立替 = t01.支払通行料,
												得意先名 = m01G.略称名,
                                                備考 = t01.請求摘要,
												売上金額 = t01.売上金額 + t01.請求割増１ + t01.請求割増２,
                                                通行料 = t01.通行料,
                                                コード = m04G.乗務員ID,
                                                乗務員名 = m04G.乗務員名,
												明細番号 = t01.明細番号,
												行 = t01.明細行,

                                            });
                    }
                    else
                    {
                        query = query.Where(c => c.コード > int.MinValue && c.コード < int.MaxValue);
                    }
                }
				query = query.Distinct();
				query = query.OrderBy(c => c.日付);
                //結果をリスト化
                retList = query.ToList();
                return retList;
            }
        }
        #endregion
    }
}