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
    /// DLY19010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class DLY19010_Member
    {

        public int 車輌コード { get; set; }
        public int 乗務員コード { get; set; }
        public DateTime? 日付 { get; set; }
        public string 車番 { get; set; }
        public string 乗務員名 { get; set; }

        public string 得意先名 { get; set; }
        public string 発地名 { get; set; }
        public string 着地名 { get; set; }
        public string 商品名 { get; set; }
        public decimal? 数量 { get; set; }
        public decimal? 重量 { get; set; }
        public int? 明細走行KM { get; set; }
        public string 備考 { get; set; }

        //public decimal 拘束時間 { get; set; }
        //public decimal 一般時間 { get; set; }
        //public decimal 高速時間 { get; set; }
        //public decimal 作業時間 { get; set; }
        //public decimal 待機時間 { get; set; }
        //public decimal 休憩時間 { get; set; }
        //public decimal 残業時間 { get; set; }
        //public decimal 深夜時間 { get; set; }

        //public int 走行KM { get; set; }
        //public int 実車KM { get; set; }
        //public decimal 輸送屯数 { get; set; }

        ////public decimal 出庫時間 { get; set; }
        ////public decimal 帰庫時間 { get; set; }
        ////public int 出庫メーター { get; set; }
        ////public int 帰庫メーター { get; set; }

    }




    public class DLY19010
    {
        #region 印刷
        /// <summary>
        /// DLY19010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<DLY19010_Member> GetDataList(string p乗務員From, string p乗務員To, int?[] i乗務員List, DateTime? d集計期間From, DateTime? d集計期間To, int i部門指定)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<DLY19010_Member> retList = new List<DLY19010_Member>();

                context.Connection.Open();

                var query = (from m04 in context.M04_DRV
                             from t01 in context.T01_TRN.Where(t01 => m04.乗務員KEY == t01.乗務員KEY && (t01.配送日付 >= d集計期間From && t01.配送日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1)))
                             join m01 in context.M01_TOK on t01.得意先KEY equals m01.得意先KEY into m01Group
                             from m01G in m01Group
                             join m05 in context.M05_CAR on t01.車輌KEY equals m05.車輌KEY into m05Group
                             from m05G in m05Group
                             where m04.削除日付 ==null
                             select new DLY19010_Member
                             {
                                 車輌コード = m05G.車輌ID,
                                 乗務員コード = m04.乗務員ID,
                                 日付 = t01.請求日付,
                                 車番 = t01.車輌番号,
                                 乗務員名 = m04.乗務員名,
                                 得意先名 = m01G.略称名,
                                 発地名 = t01.発地名,
                                 着地名 = t01.着地名,
                                 商品名 = t01.商品名,
                                 数量 = t01.数量 == null ? 0 : t01.数量,
                                 重量 = t01.重量 == null ? 0 : t01.重量,
                                 明細走行KM = t01.走行ＫＭ == null ? 0 : t01.走行ＫＭ,
                                 備考 = t01.請求摘要,
                                 

                             }).AsQueryable();

                if (!(string.IsNullOrEmpty(p乗務員From + p乗務員To) && i乗務員List.Length == 0))
                {
                    //乗務員が検索対象に入っていない時全データ取得
                    if (string.IsNullOrEmpty(p乗務員From + p乗務員To))
                    {
                        query = query.Where(c => c.乗務員コード >= int.MaxValue);
                    }

                    //乗務員From処理　Min値
                    if (!string.IsNullOrEmpty(p乗務員From))
                    {
                        int i乗務員FROM = AppCommon.IntParse(p乗務員From);
                        query = query.Where(c => c.乗務員コード >= i乗務員FROM);
                    }

                    //乗務員To処理　Max値
                    if (!string.IsNullOrEmpty(p乗務員To))
                    {
                        int i乗務員TO = AppCommon.IntParse(p乗務員To);
                        query = query.Where(c => c.乗務員コード <= i乗務員TO);
                    }

                    if (i乗務員List.Length > 0)
                    {
                        var intCause = i乗務員List;
                        query = query.Union(from m04 in context.M04_DRV
                                            from t01 in context.T01_TRN.Where(t01 => m04.乗務員KEY == t01.乗務員KEY && (t01.配送日付 >= d集計期間From && t01.配送日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 != 1)))
                                            join m01 in context.M01_TOK on t01.得意先KEY equals m01.得意先KEY into m01Group
                                            from m01G in m01Group
                                            join m05 in context.M05_CAR on t01.車輌KEY equals m05.車輌KEY into m05Group
                                            from m05G in m05Group
                                            where intCause.Contains(m04.乗務員ID) && m04.削除日付 == null
                                            select new DLY19010_Member
                                            {
                                                車輌コード = m05G.車輌ID,
                                                乗務員コード = m04.乗務員ID,
                                                日付 = t01.配送日付,
                                                車番 = t01.車輌番号,
                                                乗務員名 = m04.乗務員名,
                                                得意先名 = m01G.略称名,
                                                発地名 = t01.発地名,
                                                着地名 = t01.着地名,
                                                商品名 = t01.商品名,
                                                数量 = t01.数量,
                                                重量 = t01.重量,
                                                明細走行KM = t01.走行ＫＭ == null ? 0 : t01.走行ＫＭ,
                                                備考 = t01.請求摘要,
                                            }).AsQueryable();
                   
                    }

                    else
                    {
                        query = query.Where(c => c.乗務員コード > int.MinValue && c.乗務員コード < int.MaxValue);

                    }
                }
                query = query.Distinct();
                
                var ret = query.ToList();

                //レコード埋め作業
                var query_2 = (from q in ret
                                group q by new { q.日付, q.乗務員コード } into grp
                                select new DLY19010_Member
                             {
                                 日付 = grp.Key.日付,
                                 乗務員コード = grp.FirstOrDefault().乗務員コード,


                             }).ToList();

                foreach (var row in query_2)
                {
                    var query_3 = (from q in ret
                                    where q.乗務員コード == row.乗務員コード && q.日付 == row.日付
                                    select new DLY19010_Member
                                    {
                                        日付 = row.日付,
                                        乗務員コード = row.乗務員コード,

                                    }).ToList();
                    
					int cnt = 0;
                    if (query_3.Count > 0)
					{
                        cnt = 11 - (query_3.Count % 11);
					};
                    for (int i = 0; i < cnt; i++)
                    {
                        ret.Add(new DLY19010_Member
                        {
                            車輌コード = row.車輌コード,
                            乗務員コード = row.乗務員コード,
                            日付 = row.日付,
                            車番 = row.車番,
                            乗務員名 = string.Empty,
                            得意先名 = string.Empty,
                            発地名 = string.Empty,
                            着地名 = string.Empty,
                            商品名 = string.Empty,
                            数量 = null,
                            重量 = null,
                            明細走行KM = null,
                            備考 = string.Empty,

                        });
                    };

                };


                ret = ret.OrderBy(c => c.乗務員コード).ThenBy(c => c.日付).ToList();


                //結果
                return ret;
            }
        }
        #endregion


    }
}