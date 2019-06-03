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
    /// JMI03010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class JMI03010_Member
    {
        [DataMember]
        public DateTime 出庫 { get; set; }
        [DataMember]
        public DateTime 帰庫 { get; set; }
        [DataMember]
        public string 車番 { get; set; }
        [DataMember]
        public string 出区 { get; set; }
        [DataMember]
        public decimal? 出社H { get; set; }
        [DataMember]
        public decimal? 退社H { get; set; }
        [DataMember]
        public decimal? 拘束H { get; set; }
        [DataMember]
        public decimal? 運転H { get; set; }
        [DataMember]
        public decimal? 高速H { get; set; }
        [DataMember]
        public decimal? 作業H { get; set; }
        [DataMember]
        public decimal? 待機H { get; set; }
        [DataMember]
        public decimal? 休憩H { get; set; }
        [DataMember]
        public decimal? 残業H { get; set; }
        [DataMember]
        public decimal? 深夜H { get; set; }
        [DataMember]
        public int 走行KM { get; set; }
        [DataMember]
        public int 実車KM { get; set; }
        [DataMember]
        public decimal? 輸送屯数 { get; set; }
        [DataMember]
        public decimal? 経費合計 { get; set; }
        [DataMember]
        public int? 明細番号 { get; set; }


        [DataMember]
        public int コード { get; set; }
        [DataMember]
        public string 乗務員名 { get; set; }
        [DataMember]
        public DateTime 期間From { get; set; }
        [DataMember]
        public DateTime 期間To { get; set; }

    }


    /// <summary>
    /// JMI03010  CSV　メンバー
    /// </summary>
    [DataContract]
    public class JMI03010_Member_CSV
    {
        [DataMember]
        public int コード { get; set; }
        [DataMember]
        public string 乗務員名 { get; set; }
        [DataMember]
        public DateTime 出庫 { get; set; }
        [DataMember]
        public DateTime 帰庫 { get; set; }
        [DataMember]
        public string 車番 { get; set; }
        [DataMember]
        public string 出区 { get; set; }
        [DataMember]
        public decimal? 出社H { get; set; }
        [DataMember]
        public decimal? 退社H { get; set; }
        [DataMember]
        public decimal? 拘束H { get; set; }
        [DataMember]
        public decimal? 運転H { get; set; }
        [DataMember]
        public decimal? 高速H { get; set; }
        [DataMember]
        public decimal? 作業H { get; set; }
        [DataMember]
        public decimal? 待機H { get; set; }
        [DataMember]
        public decimal? 休憩H { get; set; }
        [DataMember]
        public decimal? 残業H { get; set; }
        [DataMember]
        public decimal? 深夜H { get; set; }
        [DataMember]
        public int 走行KM { get; set; }
        [DataMember]
        public int 実車KM { get; set; }
        [DataMember]
        public decimal? 輸送屯数 { get; set; }
        [DataMember]
        public decimal? 経費合計 { get; set; }
        [DataMember]
        public int? 明細番号 { get; set; }



    }


    [DataContract]
    public class JMI03010_M07
    {
        [DataMember]
        public int 経費ID { get; set; }
        [DataMember]
        public string 経費名 { get; set; }
    }


    public class JMI03010
    {
        #region 印刷
        /// <summary>
        /// JMI03010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<JMI03010_Member> GetDataList(string p乗務員From, string p乗務員To, int?[] i乗務員List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<JMI03010_Member> retList = new List<JMI03010_Member>();
                context.Connection.Open();

                var query = (from t02 in context.T02_UTRN.Where(x => x.勤務開始日 >= d集計期間From && x.勤務開始日 <= d集計期間To)
                             join m04 in context.M04_DRV on t02.乗務員KEY equals m04.乗務員KEY
                             join m78 in context.M78_SYK on t02.出勤区分ID equals m78.出勤区分ID into sm78
                                from m78Group in sm78.DefaultIfEmpty()
                             join t01 in context.T01_TRN.Where(t01 => (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1)) on t02.明細番号 equals t01.明細番号  into t01Group
                             join t03 in context.T03_KTRN on t02.明細番号 equals t03.明細番号 into t03Group

                             select new JMI03010_Member
                             {
                                 出庫 = t02.勤務開始日,
                                 帰庫 = t02.勤務終了日,
                                 車番 = t02.車輌番号,
                                 出区 = m78Group.出勤区分名,
                                 出社H = t02.出庫時間,
                                 退社H = t02.帰庫時間,
                                 拘束H = t02.拘束時間 == null ? 0 : t02.拘束時間,
                                 運転H = t02.運転時間 == null ? 0 : t02.運転時間,
                                 高速H = t02.高速時間 == null ? 0 : t02.高速時間,
                                 作業H = t02.作業時間 == null ? 0 : t02.作業時間,
                                 待機H = t02.待機時間 == null ? 0 : t02.待機時間,
                                 休憩H = t02.休憩時間 == null ? 0 : t02.休憩時間,
                                 残業H = t02.残業時間 == null ? 0 : t02.残業時間,
                                 深夜H = t02.深夜時間 == null ? 0 : t02.深夜時間,
                                 走行KM = t02.走行ＫＭ,
                                 実車KM = t02.実車ＫＭ,
                                 輸送屯数 = t02.輸送屯数,
                                 経費合計 = t03Group.Sum(t03 => t03.金額) == null ? 0 : t03Group.Sum(t03 => t03.金額),
                                 明細番号 = t02.明細番号,

                                 コード = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 期間From = d集計期間From,
                                 期間To = d集計期間To,

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
                        query = query.Union(from t02 in context.T02_UTRN.Where(x => x.勤務開始日 >= d集計期間From && x.勤務開始日 <= d集計期間To)
                                     join m04 in context.M04_DRV on t02.乗務員KEY equals m04.乗務員KEY
                                     join m78 in context.M78_SYK on t02.出勤区分ID equals m78.出勤区分ID into sm78
                                     from m78Group in sm78.DefaultIfEmpty()
                                     join t01 in context.T01_TRN.Where(t01 => (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1)) on t02.明細番号 equals t01.明細番号 into t01Group
                                     join t03 in context.T03_KTRN on t02.明細番号 equals t03.明細番号 into t03Group
                                     where intCause.Contains(m04.乗務員ID)

                                     select new JMI03010_Member
                                     {
                                         出庫 = t02.勤務開始日,
                                         帰庫 = t02.勤務終了日,
                                         車番 = t02.車輌番号,
                                         出区 = m78Group.出勤区分名,
                                         出社H = t02.出庫時間,
                                         退社H = t02.帰庫時間,
                                         拘束H = t02.拘束時間 == null ? 0 : t02.拘束時間,
                                         運転H = t02.運転時間 == null ? 0 : t02.運転時間,
                                         高速H = t02.高速時間 == null ? 0 : t02.高速時間,
                                         作業H = t02.作業時間 == null ? 0 : t02.作業時間,
                                         待機H = t02.待機時間 == null ? 0 : t02.待機時間,
                                         休憩H = t02.休憩時間 == null ? 0 : t02.休憩時間,
                                         残業H = t02.残業時間 == null ? 0 : t02.残業時間,
                                         深夜H = t02.深夜時間 == null ? 0 : t02.深夜時間,
                                         走行KM = t02.走行ＫＭ,
                                         実車KM = t02.実車ＫＭ,
                                         輸送屯数 = t02.輸送屯数,
                                         経費合計 = t03Group.Sum(t03 => t03.金額) == null ? 0 : t03Group.Sum(t03 => t03.金額),
                                         明細番号 = t02.明細番号,

                                         コード = m04.乗務員ID,
                                         乗務員名 = m04.乗務員名,
                                         期間From = d集計期間From,
                                         期間To = d集計期間To,
                                     });
                    }

                    else
                    {
                        query = query.Where(c => c.コード > int.MinValue && c.コード < int.MaxValue);

                    }

                }

                query = query.Distinct();
                //結果をリスト化
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

				retList = (retList.OrderBy(c => c.コード).ThenBy(c => c.出庫)).ToList();

                return retList;
            }

        }
        #endregion



        #region CSV出力

        /// <summary>
        /// JMI03010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<JMI03010_Member_CSV> GetDataList_CSV(string p乗務員From, string p乗務員To, int?[] i乗務員List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, int p作成年度)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<JMI03010_Member_CSV> retList = new List<JMI03010_Member_CSV>();
                context.Connection.Open();

                var query = (from t02 in context.T02_UTRN.Where(x => x.勤務開始日 >= d集計期間From && x.勤務開始日 <= d集計期間To)
                             join m04 in context.M04_DRV on t02.乗務員KEY equals m04.乗務員KEY
                             join m78 in context.M78_SYK on t02.出勤区分ID equals m78.出勤区分ID into sm78
                             from m78Group in sm78.DefaultIfEmpty()
                             join t01 in context.T01_TRN.Where(t01 => (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1)) on t02.明細番号 equals t01.明細番号 into t01Group
                             join t03 in context.T03_KTRN on t02.明細番号 equals t03.明細番号 into t03Group

                             select new JMI03010_Member_CSV
                             {
                                 出庫 = t02.勤務開始日,
                                 帰庫 = t02.勤務終了日,
                                 車番 = t02.車輌番号,
                                 出区 = m78Group.出勤区分名,
                                 出社H = t02.出庫時間,
                                 退社H = t02.帰庫時間,
                                 拘束H = t02.拘束時間 == null ? 0 : t02.拘束時間,
                                 運転H = t02.運転時間 == null ? 0 : t02.運転時間,
                                 高速H = t02.高速時間 == null ? 0 : t02.高速時間,
                                 作業H = t02.作業時間 == null ? 0 : t02.作業時間,
                                 待機H = t02.待機時間 == null ? 0 : t02.待機時間,
                                 休憩H = t02.休憩時間 == null ? 0 : t02.休憩時間,
                                 残業H = t02.残業時間 == null ? 0 : t02.残業時間,
                                 深夜H = t02.深夜時間 == null ? 0 : t02.深夜時間,
                                 走行KM = t02.走行ＫＭ,
                                 実車KM = t02.実車ＫＭ,
                                 輸送屯数 = t02.輸送屯数,
                                 経費合計 = t03Group.Sum(t03 => t03.金額) == null ? 0 : t03Group.Sum(t03 => t03.金額),
                                 明細番号 = t02.明細番号,

                                 コード = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,

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
                        query = query.Union(from t02 in context.T02_UTRN.Where(x => x.勤務開始日 >= d集計期間From && x.勤務開始日 <= d集計期間To)
                                            join m04 in context.M04_DRV on t02.乗務員KEY equals m04.乗務員KEY
                                            join m78 in context.M78_SYK on t02.出勤区分ID equals m78.出勤区分ID into sm78
                                            from m78Group in sm78.DefaultIfEmpty()
                                            join t01 in context.T01_TRN.Where(t01 => (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1)) on t02.明細番号 equals t01.明細番号 into t01Group
                                            join t03 in context.T03_KTRN on t02.明細番号 equals t03.明細番号 into t03Group
                                            where intCause.Contains(m04.乗務員ID)

                                            select new JMI03010_Member_CSV
                                            {
                                                出庫 = t02.勤務開始日,
                                                帰庫 = t02.勤務終了日,
                                                車番 = t02.車輌番号,
                                                出区 = m78Group.出勤区分名,
                                                出社H = t02.出庫時間,
                                                退社H = t02.帰庫時間,
                                                拘束H = t02.拘束時間 == null ? 0 : t02.拘束時間,
                                                運転H = t02.運転時間 == null ? 0 : t02.運転時間,
                                                高速H = t02.高速時間 == null ? 0 : t02.高速時間,
                                                作業H = t02.作業時間 == null ? 0 : t02.作業時間,
                                                待機H = t02.待機時間 == null ? 0 : t02.待機時間,
                                                休憩H = t02.休憩時間 == null ? 0 : t02.休憩時間,
                                                残業H = t02.残業時間 == null ? 0 : t02.残業時間,
                                                深夜H = t02.深夜時間 == null ? 0 : t02.深夜時間,
                                                走行KM = t02.走行ＫＭ,
                                                実車KM = t02.実車ＫＭ,
                                                輸送屯数 = t02.輸送屯数,
                                                経費合計 = t03Group.Sum(t03 => t03.金額) == null ? 0 : t03Group.Sum(t03 => t03.金額),
                                                明細番号 = t02.明細番号,

                                                コード = m04.乗務員ID,
                                                乗務員名 = m04.乗務員名,
                                            });

                    }

                    else
                    {
                        query = query.Where(c => c.コード > int.MinValue && c.コード < int.MaxValue);

                    }

                }

                query = query.Distinct();
                //結果をリスト化
				retList = query.OrderBy(c => c.コード).ThenBy(c => c.出庫).ToList();
                return retList;
            }

        }
        #endregion
    }
}