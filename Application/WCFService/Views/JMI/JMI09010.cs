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
    /// JMI09010  運転適正診断印刷　メンバー
    /// </summary>
    [DataContract]
    public class JMI09010_Member
    {
        public DateTime? 免許有効日 { get; set; }
        public int 乗務員ID { get; set; }
        public string 乗務員名 { get; set; }
        public string カナ読み { get; set; }
        public int 自社部門ID { get; set; }
        public string 自社部門名 { get; set; }
        public int? 事業者ID { get; set; }
        public string 事業者名 { get; set; }
        public int? 就労区分 { get; set; }
        public string 就労区分条件 { get; set; }
        public string 就労区分表示 { get; set; }
        public string 終了区分 { get; set; }
        public DateTime? 期間From { get; set; }
        public DateTime? 期間To { get; set; }
        public string コードFrom { get; set; }
        public string コードTo { get; set; }
        public string コードList { get; set; }
        public int? 表示順序 { get; set; }
        public string 表示順序表示 { get; set; }
        public string 表示区分 { get; set; }
    }


    /// <summary>
    /// JMI09010  CSV　メンバー
    /// </summary>
    [DataContract]
    public class JMI09010_Member_CSV
    {
        public DateTime? 免許有効日 { get; set; }
        public int 乗務員ID { get; set; }
        public string 乗務員名 { get; set; }
        public string カナ読み { get; set; }
        public int 自社部門ID { get; set; }
        public string 自社部門名 { get; set; }
        public int? 事業者ID { get; set; }
        public string 事業者名 { get; set; }
        public int? 就労区分 { get; set; }
        public string 終了区分 { get; set; }
        public DateTime? 期間From { get; set; }
        public DateTime? 期間To { get; set; }
        public int? 表示順序 { get; set; }
    }

    public class JMI09010
    {
        #region 印刷
        /// <summary>
        /// JMI09010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<JMI09010_Member> GetDataList(string p乗務員From, string p乗務員To, int?[] i乗務員List, int? i集計期間From, int? i集計期間To, string s乗務員List, int i部門区分, int i就労区分 ,int i表示順序 , int i表示区分)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<JMI09010_Member> retList = new List<JMI09010_Member>();
                context.Connection.Open();
                DateTime? d集計期間From;
                DateTime? d集計期間To;
                DateTime result;
                //d集計期間From = DateTime.Parse(Convert.ToInt32(i集計期間From).ToString() + "/01/01");
                if (DateTime.TryParse(Convert.ToInt32(i集計期間From).ToString() + "/01/01", out result))
                {
                    d集計期間From = result;
                }
                else
                {
                    d集計期間From = null;
                }
                //d集計期間To = DateTime.Parse(Convert.ToInt32(i集計期間To).ToString() + "/12/31");
                if (DateTime.TryParse(Convert.ToInt32(i集計期間To).ToString() + "/12/31", out result))
                {
                    d集計期間To = result;
                }
                else
                {
                    d集計期間To = null;
                }

                var query = (from m04 in context.M04_DRV
                             from m70 in context.M70_JIS.Where(m70 => m70.自社ID == m04.自社ID).DefaultIfEmpty()
                             from m71 in context.M71_BUM.Where(m71 => m71.自社部門ID == m04.自社部門ID).DefaultIfEmpty()
                             where (i部門区分 == 0 || m04.自社部門ID == i部門区分) && m04.削除日付 == null
                             select new JMI09010_Member
                             {
                                 免許有効日 = m04.免許有効年月日1,
                                 乗務員ID = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 カナ読み = m04.かな読み,
                                 自社部門ID = m04.自社部門ID,
                                 自社部門名 = m71.自社部門名,
                                 事業者ID = m04.自社ID,
                                 事業者名 = m70.自社名,
                                 就労区分 = m04.就労区分,
                                 就労区分表示 = m04.就労区分 == 0 ? "就労者" : m04.就労区分 == 1 ? "休職者" : m04.就労区分 == 2 ? "退職者" : "",
                                 就労区分条件 = i就労区分 == 0 ? "全件表示" : i就労区分 == 1 ? "就労者のみ" : i就労区分 == 2 ? "休職者のみ" : i就労区分 == 3 ? "退職者のみ" : "",
                                 コードFrom = p乗務員From,
                                 コードTo = p乗務員To,
                                 コードList = s乗務員List,
                                 期間From = d集計期間From,
                                 期間To = d集計期間To,
                                 表示順序 = i表示順序,
                                 表示順序表示 = i表示順序 == 0 ? "ID順" : i表示順序 == 1 ? "カナ順" : i表示順序 == 2 ? "日付順" : "",
                                 表示区分 = i表示区分 == 0 ? "有効日未入力は非表示" : i表示区分 == 1 ? "有効日未入力を表示" : "",
                             }).AsQueryable();

                query = query.Distinct();

                if (!(string.IsNullOrEmpty(p乗務員From + p乗務員To) && i乗務員List.Length == 0))
                {
                    //乗務員が検索対象に入っていない時全データ取得
                    if (string.IsNullOrEmpty(p乗務員From + p乗務員To))
                    {
                        query = query.Where(c => c.乗務員ID >= int.MaxValue);
                    }

                    //乗務員From処理　Min値
                    if (!string.IsNullOrEmpty(p乗務員From))
                    {
                        int i乗務員FROM = AppCommon.IntParse(p乗務員From);
                        query = query.Where(c => c.乗務員ID >= i乗務員FROM);
                    }

                    //乗務員To処理　Max値
                    if (!string.IsNullOrEmpty(p乗務員To))
                    {
                        int i乗務員TO = AppCommon.IntParse(p乗務員To);
                        query = query.Where(c => c.乗務員ID <= i乗務員TO);
                    }

                    //日付条件
                    if (i集計期間From != null && i集計期間To != null)
                    {
                        query = query.Where(c => c.免許有効日 >= d集計期間From && c.免許有効日 <= d集計期間To);
                    }
                    else if (i集計期間From != null)
                    {
                        query = query.Where(c => c.免許有効日 >= d集計期間From);
                    }
                    else if (i集計期間To != null)
                    {
                        query = query.Where(c => c.免許有効日 <= d集計期間To);
                    }

                    if (i乗務員List.Length > 0)
                    {
                        var intCause = i乗務員List;
                        query = query.Union(from m04 in context.M04_DRV
                                            from m70 in context.M70_JIS.Where(m70 => m70.自社ID == m04.自社ID).DefaultIfEmpty()
                                            from m71 in context.M71_BUM.Where(m71 => m71.自社部門ID == m04.自社部門ID).DefaultIfEmpty()
                                            where intCause.Contains(m04.乗務員ID) && m04.削除日付 == null
                                            select new JMI09010_Member
                                            {
                                                免許有効日 = m04.免許有効年月日1,
                                                乗務員ID = m04.乗務員ID,
                                                乗務員名 = m04.乗務員名,
                                                カナ読み = m04.かな読み,
                                                自社部門ID = m04.自社部門ID,
                                                自社部門名 = m71.自社部門名,
                                                事業者ID = m04.自社ID,
                                                事業者名 = m70.自社名,
                                                就労区分 = m04.就労区分,
                                                就労区分表示 = m04.就労区分 == 0 ? "就労者" : m04.就労区分 == 1 ? "休職者" : m04.就労区分 == 2 ? "退職者" : "",
                                                就労区分条件 = i就労区分 == 0 ? "全件表示" : i就労区分 == 1 ? "就労者のみ" : i就労区分 == 2 ? "休職者のみ" : i就労区分 == 3 ? "退職者のみ" : "",
                                                コードFrom = p乗務員From,
                                                コードTo = p乗務員To,
                                                コードList = s乗務員List,
                                                期間From = d集計期間From,
                                                期間To = d集計期間To,
                                                表示順序 = i表示順序,
                                                表示順序表示 = i表示順序 == 0 ? "ID順" : i表示順序 == 1 ? "カナ順" : i表示順序 == 2 ? "日付順" : "",
                                                表示区分 = i表示区分 == 0 ? "有効日未入力は非表示" : i表示区分 == 1 ? "有効日未入力を表示" : "",
                                            });
                    }
                }
                else
                {
                    //入力が無い場合
                    //乗務員IDの範囲全体を取得
                    query = query.Where(c => c.乗務員ID > int.MinValue && c.乗務員ID < int.MaxValue);

                    //日付条件
                    if (i集計期間From != null && i集計期間To != null)
                    {
                        query = query.Where(c => c.免許有効日 >= d集計期間From && c.免許有効日 <= d集計期間To);
                    }
                    else if (i集計期間From != null)
                    {
                        query = query.Where(c => c.免許有効日 >= d集計期間From);
                    }
                    else if (i集計期間To != null)
                    {
                        query = query.Where(c => c.免許有効日 <= d集計期間To);
                    }
                }

                //就労区分
                switch (i就労区分)
                {
                    //全件
                    case 0:
                        query = query.Where(c => c.就労区分 >= 0);
                        break;
                    //就労者
                    case 1:
                        query = query.Where(c => c.就労区分 == 0);
                        break;
                    //休職者
                    case 2:
                        query = query.Where(c => c.就労区分 == 1);
                        break;
                    //退職者
                    case 3:
                        query = query.Where(c => c.就労区分 == 2);
                        break;
                }
                //表示順序
                switch (i表示順序)
                {
                    case 0:
                        query = query.OrderBy(c => c.乗務員ID);
                        break;
                    case 1:
                        query = query.OrderBy(c => c.カナ読み);
                        break;
                    case 2:
                        query = query.OrderBy(c => c.免許有効日);
                        break;
                }
                //表示区分
                switch (i表示区分)
                {
                    case 0:
                        break;
                    case 1:
                        query = query.Where(c => c.免許有効日 != null);
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
        /// JMI09010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<JMI09010_Member_CSV> GetDataList_CSV(string p乗務員From, string p乗務員To, int?[] i乗務員List, int? i集計期間From, int? i集計期間To , string s乗務員List, int i部門区分, int i就労区分, int i表示順序, int i表示区分)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<JMI09010_Member_CSV> retList = new List<JMI09010_Member_CSV>();
                context.Connection.Open();
                DateTime? d集計期間From;
                DateTime? d集計期間To;
                DateTime result;
                //d集計期間From = DateTime.Parse(Convert.ToInt32(i集計期間From).ToString() + "/01/01");
                if (DateTime.TryParse(Convert.ToInt32(i集計期間From).ToString() + "/01/01", out result))
                {
                    d集計期間From = result;
                }
                else
                {
                    d集計期間From = null;
                }
                //d集計期間To = DateTime.Parse(Convert.ToInt32(i集計期間To).ToString() + "/12/31");
                if (DateTime.TryParse(Convert.ToInt32(i集計期間To).ToString() + "/12/31", out result))
                {
                    d集計期間To = result;
                }
                else
                {
                    d集計期間To = null;
                }

                var query = (from m04 in context.M04_DRV
                             from m70 in context.M70_JIS.Where(m70 => m70.自社ID == m04.自社ID).DefaultIfEmpty()
                             from m71 in context.M71_BUM.Where(m71 => m71.自社部門ID == m04.自社部門ID).DefaultIfEmpty()
                             where m04.削除日付 == null
                             select new JMI09010_Member_CSV
                             {
                                 免許有効日 = m04.免許有効年月日1,
                                 乗務員ID = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 カナ読み = m04.かな読み,
                                 自社部門ID = m04.自社部門ID,
                                 自社部門名 = m71.自社部門名,
                                 事業者ID = m70.自社ID,
                                 事業者名 = m70.自社名,
                                 就労区分 = m04.就労区分,
                                 期間From = d集計期間From,
                                 期間To = d集計期間To,
                                 表示順序 = i表示順序,
                             }).AsQueryable();

                query = query.Distinct();

                if (!(string.IsNullOrEmpty(p乗務員From + p乗務員To) && i乗務員List.Length == 0))
                {
                    //乗務員が検索対象に入っていない時全データ取得
                    if (string.IsNullOrEmpty(p乗務員From + p乗務員To))
                    {
                        query = query.Where(c => c.乗務員ID >= int.MaxValue);
                    }

                    //乗務員From処理　Min値
                    if (!string.IsNullOrEmpty(p乗務員From))
                    {
                        int i乗務員FROM = AppCommon.IntParse(p乗務員From);
                        query = query.Where(c => c.乗務員ID >= i乗務員FROM);
                    }

                    //乗務員To処理　Max値
                    if (!string.IsNullOrEmpty(p乗務員To))
                    {
                        int i乗務員TO = AppCommon.IntParse(p乗務員To);
                        query = query.Where(c => c.乗務員ID <= i乗務員TO);
                    }

                    //日付条件
                    if (i集計期間From != null && i集計期間To != null)
                    {
                        query = query.Where(c => c.免許有効日 >= d集計期間From && c.免許有効日 <= d集計期間To);
                    }
                    else if (i集計期間From != null)
                    {
                        query = query.Where(c => c.免許有効日 >= d集計期間From);
                    }
                    else if (i集計期間To != null)
                    {
                        query = query.Where(c => c.免許有効日 <= d集計期間To);
                    }

                    if (i乗務員List.Length > 0)
                    {
                        var intCause = i乗務員List;
                        query = query.Union(from m04 in context.M04_DRV
                                            from m70 in context.M70_JIS.Where(m70 => m70.自社ID == m04.自社ID).DefaultIfEmpty()
                                            from m71 in context.M71_BUM.Where(m71 => m71.自社部門ID == m04.自社部門ID).DefaultIfEmpty()
                                            where intCause.Contains(m04.乗務員ID) && m04.削除日付 == null
                                            select new JMI09010_Member_CSV
                                            {
                                                免許有効日 = m04.免許有効年月日1,
                                                乗務員ID = m04.乗務員ID,
                                                乗務員名 = m04.乗務員名,
                                                カナ読み = m04.かな読み,
                                                自社部門ID = m04.自社部門ID,
                                                自社部門名 = m71.自社部門名,
                                                事業者ID = m70.自社ID,
                                                事業者名 = m70.自社名,
                                                就労区分 = m04.就労区分,
                                                期間From = d集計期間From,
                                                期間To = d集計期間To,
                                                表示順序 = i表示順序,
                                            });
                    }
                }
                else
                {
                    query = query.Where(c => c.乗務員ID > int.MinValue && c.乗務員ID < int.MaxValue);


                    //日付条件
                    if (i集計期間From != null && i集計期間To != null)
                    {
                        query = query.Where(c => c.免許有効日 >= d集計期間From && c.免許有効日 <= d集計期間To);
                    }
                    else if (i集計期間From != null)
                    {
                        query = query.Where(c => c.免許有効日 >= d集計期間From);
                    }
                    else if (i集計期間To != null)
                    {
                        query = query.Where(c => c.免許有効日 <= d集計期間To);
                    }
                }

                //就労区分
                switch (i就労区分)
                {
                    //全件
                    case 0:
                        query = query.Where(c => c.就労区分 >= 0);
                        break;
                    //就労者
                    case 1:
                        query = query.Where(c => c.就労区分 == 0);
                        break;
                    //休職者
                    case 2:
                        query = query.Where(c => c.就労区分 == 1);
                        break;
                    //退職者
                    case 3:
                        query = query.Where(c => c.就労区分 == 2);
                        break;
                }
                //表示順序
                switch (i表示順序)
                {
                    case 0:
                        query = query.OrderBy(c => c.乗務員ID);
                        break;
                    case 1:
                        query = query.OrderBy(c => c.カナ読み);
                        break;
                    case 2:
                        query = query.OrderBy(c => c.免許有効日);
                        break;
                }
                //表示区分
                switch (i表示区分)
                {
                    case 0:
                        break;
                    case 1:
                        query = query.Where(c => c.免許有効日 != null);
                        break;
                }

                //結果をリスト化
                retList = query.ToList();
                return retList;
            }
        }
        #endregion
    }
}