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
    /// <summary>
    /// 運転日報関連機能
    /// </summary>
    public class DLY16010
    {

        public class DLY16010_Member
        {
            [DataMember]
            public int 明細番号 { get; set; }
            [DataMember]
            public int 明細行 { get; set; }
            [DataMember]
            public DateTime? 支払日付 { get; set; }
            [DataMember]
            public DateTime? 手形決済日 { get; set; }
            [DataMember]
            public int 支払先ID { get; set; }
            [DataMember]
            public string 支払先名 { get; set; }
            [DataMember]
            public string 支払区分 { get; set; }
            [DataMember]
            public decimal? 入出金金額 { get; set; }
            [DataMember]
            public string 摘要名 { get; set; }
            [DataMember]
            public DateTime? 検索日付From { get; set; }
            [DataMember]
            public DateTime? 検索日付To { get; set; }
            [DataMember]
            public string 検索日付選択 { get; set; }

        }

        public class DLY16010_TotalValue
        {
            [DataMember]
            public decimal 合計金額 { get; set; }
        }

        public class DLY16010_DATASET
        {
            public List<DLY16010_Member> DataList = new List<DLY16010_Member>();
            public List<DLY16010_TotalValue> TotalList = new List<DLY16010_TotalValue>();
        }


        /// <summary>
        /// Spread情報取得 【SELECT】
        /// </summary>
        /// <param name="i得意先ID"></param>
        /// <param name="d検索日付From"></param>
        /// <param name="d検索日付To"></param>
        /// <param name="i日付区分"></param>
        /// <returns></returns>
        public DLY16010_DATASET GetListDLY16010(int? p担当者ID, int i得意先ID, DateTime d検索日付From, DateTime d検索日付To, int i日付区分, int p入金区分, string p摘要指定)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                DLY16010_DATASET result = new DLY16010_DATASET();

                var query = (from t04 in context.T04_NYUK
                             from m01 in context.M01_TOK.Where(c => c.得意先KEY == t04.取引先KEY)
                             where t04.明細区分 == 3 &&
							 (p担当者ID == null || t04.入力者ID == p担当者ID) && (p入金区分 == 0 || t04.入出金区分 == p入金区分) && (p摘要指定 == "" || t04.摘要名.Contains(p摘要指定))
                             select new DLY16010_Member
                             {
                                 明細番号 = t04.明細番号,
                                 明細行 = t04.明細行,
                                 支払日付 = t04.入出金日付,
                                 手形決済日 = t04.手形日付,
                                 支払先ID = m01.得意先ID,
                                 支払先名 = m01.得意先名１,
                                 支払区分 = t04.入出金区分 == 1 ? "現金" : t04.入出金区分 == 2 ? "振込" : t04.入出金区分 == 3 ? "小切手" : t04.入出金区分 == 4 ? "手形" : t04.入出金区分 == 5 ? "相殺" : t04.入出金区分 == 6 ? "調整" : t04.入出金区分 == 7 ? "その他" : t04.入出金区分 == 8 ? "値引" : "手数料",
                                 入出金金額 = t04.入出金金額,
                                 摘要名 = t04.摘要名,
                             }).AsQueryable();
                DateTime TestTime = Convert.ToDateTime("0001/01/01");
                //日付区分【1 : 手形日付】
                if (i日付区分 == 0)
                {
                    if (d検索日付From != TestTime && d検索日付To != TestTime)
                    {
                        query = query.Where(c => c.支払日付 >= d検索日付From && c.支払日付 <= d検索日付To);
                    }
                    else if (d検索日付From != TestTime && d検索日付To == TestTime)
                    {
                        query = query.Where(c => c.支払日付 >= d検索日付From && c.支払日付 <= DateTime.MaxValue);
                    }
                    else if (d検索日付From == TestTime && d検索日付To != TestTime)
                    {
                        query = query.Where(c => c.支払日付 >= DateTime.MinValue && c.支払日付 <= d検索日付To);
                    }
                    else
                    {
                        query = query.Where(c => c.支払日付 >= DateTime.MinValue && c.支払日付 <= DateTime.MaxValue);
                    }
                }
                //日付区分【1 : 手形区分】
                else
                {
                    if (d検索日付From != TestTime && d検索日付To != TestTime)
                    {
                        query = query.Where(c => c.手形決済日 >= d検索日付From && c.手形決済日 <= d検索日付To);
                    }
                    else if (d検索日付From != TestTime && d検索日付To == TestTime)
                    {
                        query = query.Where(c => c.手形決済日 >= d検索日付From && c.手形決済日 <= DateTime.MaxValue);
                    }
                    else if (d検索日付From == TestTime && d検索日付To != TestTime)
                    {
                        query = query.Where(c => c.手形決済日 >= DateTime.MinValue && c.手形決済日 <= d検索日付To);
                    }
                    else
                    {
                        query = query.Where(c => c.手形決済日 >= DateTime.MinValue && c.手形決済日 <= DateTime.MaxValue);
                    }
                }



                //支払先IDで絞込み
                if (i得意先ID != 0)
                {
                    query = query.Where(c => c.支払先ID == i得意先ID);
                }

                List<DLY16010_Member> ret;

                ret = query.ToList();

                result.DataList = ret;
                result.TotalList.Add(new DLY16010_TotalValue());
                foreach (var rec in result.DataList)
                {
                    result.TotalList[0].合計金額 += (decimal)rec.入出金金額;
                }

                return result;
            }
        }


        /// <summary>
        /// Spread情報変更 【UPDATE】
        /// </summary>
        /// <param name="p明細番号"></param>
        /// <param name="p明細行"></param>
        /// <param name="colname"></param>
        /// <param name="val"></param>
        public void Update(int p明細番号, int p明細行, string colname, object val)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // トランザクションのインスタンス化(開始)
                using (var tran = new TransactionScope())
                {
                    DateTime updtime = DateTime.Now;
                    string sql = string.Empty;

                    sql = string.Format("UPDATE T04_NYUK SET {0} = '{1}' WHERE 明細番号 = {2} AND 明細行 = {3}"
                                        , colname, val.ToString(), p明細番号, p明細行);
                    context.Connection.Open();
                    int count = context.ExecuteStoreCommand(sql);
                    // トリガが定義されていると、更新結果は複数行になる
                    if (count > 0)
                    {
                        tran.Complete();
                    }
                    else
                    {
                        // 更新行なし
                        throw new Framework.Common.DBDataNotFoundException();
                    }

                }
            }
        }



        /// <summary>
        /// 印刷 【OutPut】
        /// </summary>
        /// <param name="i得意先ID"></param>
        /// <param name="d検索日付From"></param>
        /// <param name="d検索日付To"></param>
        /// <param name="i日付区分"></param>
        /// <returns></returns>
        public List<DLY16010_Member> GetListDLY16010_Pri(int i得意先ID, int i日付区分 , DateTime? p検索日付From , DateTime? p検索日付To)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                DLY16010_Member result = new DLY16010_Member();

                var query = (from t04 in context.T04_NYUK
                             from m01 in context.M01_TOK.Where(c => c.得意先KEY == t04.取引先KEY)
                             where t04.明細区分 == 3
                             select new DLY16010_Member
                             {
                                 明細番号 = t04.明細番号,
                                 明細行 = t04.明細行,
                                 支払日付 = t04.入出金日付,
                                 手形決済日 = t04.手形日付,
                                 支払先ID = m01.得意先ID,
                                 支払先名 = m01.得意先名１,
                                 支払区分 = t04.入出金区分 == 1 ? "現金" : t04.入出金区分 == 2 ? "振込" : t04.入出金区分 == 3 ? "小切手" : t04.入出金区分 == 4 ? "手形" : t04.入出金区分 == 5 ? "相殺" : t04.入出金区分 == 6 ? "調整" : t04.入出金区分 == 7 ? "その他" : t04.入出金区分 == 8 ? "値引" : "手数料",
                                 入出金金額 = t04.入出金金額,
                                 摘要名 = t04.摘要名,
                                 検索日付From = p検索日付From,
                                 検索日付To = p検索日付To,
                                 検索日付選択　= i日付区分 == 0 ? "支払日付" : "手形日付"
                             }).AsQueryable();

                //日付区分【1 : 手形日付】
                if (i日付区分 == 0)
                {
                    if (p検索日付From != null && p検索日付To != null)
                    {
                        query = query.Where(c => c.支払日付 >= p検索日付From && c.支払日付 <= p検索日付To);
                    }
                    else if (p検索日付From != null && p検索日付To == null)
                    {
                        query = query.Where(c => c.支払日付 >= p検索日付From && c.支払日付 <= DateTime.MaxValue);
                    }
                    else if (p検索日付From == null && p検索日付To != null)
                    {
                        query = query.Where(c => c.支払日付 >= DateTime.MinValue && c.支払日付 <= p検索日付To);
                    }
                    else
                    {
                        query = query.Where(c => c.支払日付 >= DateTime.MinValue && c.支払日付 <= DateTime.MaxValue);
                    }
                }
                //日付区分【1 : 手形区分】
                else
                {
                    if (p検索日付From != null && p検索日付To != null)
                    {
                        query = query.Where(c => c.手形決済日 >= p検索日付From && c.手形決済日 <= p検索日付To);
                    }
                    else if (p検索日付From != null && p検索日付To == null)
                    {
                        query = query.Where(c => c.手形決済日 >= p検索日付From && c.手形決済日 <= DateTime.MaxValue);
                    }
                    else if (p検索日付From == null && p検索日付To != null)
                    {
                        query = query.Where(c => c.手形決済日 >= DateTime.MinValue && c.手形決済日 <= p検索日付To);
                    }
                    else
                    {
                        query = query.Where(c => c.手形決済日 >= DateTime.MinValue && c.手形決済日 <= DateTime.MaxValue);
                    }
                }
                //支払先IDで絞込み
                if (i得意先ID != 0)
                {
                    query = query.Where(c => c.支払先ID == i得意先ID);
                }

             

                return query.ToList();
            }
        }
    }
}
