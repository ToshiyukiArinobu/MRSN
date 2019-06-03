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
    /// JMI08010  運転適正診断印刷　メンバー
    /// </summary>
    [DataContract]
    public class JMI08010A_Member
    {
        public int コード { get; set; }
        public string 乗務員名 { get; set; }
        public string かな読み { get; set; }
        public string 種別 { get; set; }
        public DateTime 実施年月日 { get; set; }
        public string 実施機関名 { get; set; }
        public string 所見摘要等 { get; set; }
        public int 期間From { get; set; }
        public int 期間To { get; set; }
        public string コードFrom { get; set; }
        public string コードTo { get; set; }
        public string コードList { get; set; }
        public string 表示順序 { get; set; }
        public int 年 { get; set; }

    }
    /// <summary>
    /// JMI08010  運転適正診断印刷　メンバー
    /// </summary>
    [DataContract]
    public class JMI08010A_CSV_Member
    {
        public int コード { get; set; }
        public string 乗務員名 { get; set; }
        public string かな読み { get; set; }
        public string 種別 { get; set; }
        public DateTime 実施年月日 { get; set; }
        public string 実施機関名 { get; set; }
        public string 所見摘要等 { get; set; }
        public int 期間From { get; set; }
        public int 期間To { get; set; }
        public int 年 { get; set; }

    }

    /// <summary>
    /// JMI08010  事故履歴　メンバー
    /// </summary>
    [DataContract]
    public class JMI08010B_Member
    {
        public int コード { get; set; }
        public string 乗務員名 { get; set; }
        public string かな読み { get; set; }
        public DateTime 発生年月日 { get; set; }
        public string 概要処置等 { get; set; }
        public int 期間From { get; set; }
        public int 期間To { get; set; }
        public string コードFrom { get; set; }
        public string コードTo { get; set; }
        public string コードList { get; set; }
        public string 表示順序 { get; set; }
        public int 年 { get; set; }
    }
    /// <summary>
    /// JMI08010  事故履歴　メンバー
    /// </summary>
    [DataContract]
    public class JMI08010B_CSV_Member
    {
        public int コード { get; set; }
        public string 乗務員名 { get; set; }
        public string かな読み { get; set; }
        public DateTime 発生年月日 { get; set; }
        public string 概要処置等 { get; set; }
        public int 期間From { get; set; }
        public int 期間To { get; set; }
        public int 年 { get; set; }
    }

    /// <summary>
    /// JMI08010  違反履歴　メンバー
    /// </summary>
    [DataContract]
    public class JMI08010C_Member
    {
        public int コード { get; set; }
        public string 乗務員名 { get; set; }
        public string かな読み { get; set; }
        public DateTime 実施年月日 { get; set; }
        public string 教育内容 { get; set; }
        public string 区分 { get; set; }
        public int 期間From { get; set; }
        public int 期間To { get; set; }
        public string コードFrom { get; set; }
        public string コードTo { get; set; }
        public string コードList { get; set; }
        public string 表示順序 { get; set; }
        public int 年 { get; set; }
    }
    /// <summary>
    /// JMI08010  違反履歴　メンバー
    /// </summary>
    [DataContract]
    public class JMI08010C_CSV_Member
    {
        public int コード { get; set; }
        public string 乗務員名 { get; set; }
        public string かな読み { get; set; }
        public DateTime 実施年月日 { get; set; }
        public string 教育内容 { get; set; }
        public string 区分 { get; set; }
        public int 期間From { get; set; }
        public int 期間To { get; set; }
        public int 年 { get; set; }
    }


    [DataContract]
    public class JMI08010_M07
    {
        [DataMember]
        public int 経費ID { get; set; }
        [DataMember]
        public string 経費名 { get; set; }
    }
    

    public class JMI08010
    {
        #region 適正診断一覧印刷
        /// <summary>
        /// JMI08010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<JMI08010A_Member> GetDataListA(string p乗務員From, string p乗務員To, int?[] i乗務員List, int p作成締日, int i集計期間From, int i集計期間To, string p作成年度, string s乗務員List, int i表示順序)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<JMI08010A_Member> retList = new List<JMI08010A_Member>();
                context.Connection.Open();

                var query = (from m04 in context.M04_DRV
                             join m04d in context.M04_DDT1.Where(m04d => ((DateTime)m04d.実施年月日).Year >= i集計期間From
                                                            && ((DateTime)m04d.実施年月日).Year <= i集計期間To)
                                                                on m04.乗務員KEY equals m04d.乗務員KEY
                             join m99 in context.M99_COMBOLIST.Where(m99 => m99.分類 == "乗務員" && m99.機能 == "乗務員状況履歴" && m99.カテゴリ == "運転適正診断")
                                      on m04d.対象種類 equals m99.コード

                             select new JMI08010A_Member
                             {
                                 コード = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 かな読み = m04.かな読み,
                                 種別 = m99.表示名,
                                 実施年月日 = ((DateTime)m04d.実施年月日),
                                 実施機関名 = m04d.実施機関名,
                                 所見摘要等 = m04d.所見摘要,
                                 コードFrom = p乗務員From,
                                 コードTo = p乗務員To,
                                 コードList = s乗務員List,
                                 期間From = i集計期間From,
                                 期間To = i集計期間To,
                                 表示順序 = i表示順序 == 0 ? "ID順" : i表示順序 == 1 ? "カナ順" : i表示順序 == 2 ? "日付順" : "",
                                 年 = (m04d.実施年月日 == null) ? 0 : (int)((DateTime)m04d.実施年月日).Year,

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
                                            join m04d in context.M04_DDT1.Where(m04d => ((DateTime)m04d.実施年月日).Year >= i集計期間From
                                                                           && ((DateTime)m04d.実施年月日).Year <= i集計期間To)
                                                                               on m04.乗務員KEY equals m04d.乗務員KEY
                                            join m99 in context.M99_COMBOLIST.Where(m99 => m99.分類 == "乗務員" && m99.機能 == "乗務員状況履歴" && m99.カテゴリ == "運転適正診断")
                                                     on m04d.対象種類 equals m99.コード
                                            where intCause.Contains(m04.乗務員ID)
                                            select new JMI08010A_Member
                                            {
                                                コード = m04.乗務員ID,
                                                乗務員名 = m04.乗務員名,
                                                かな読み = m04.かな読み,
                                                種別 = m99.表示名,
                                                実施年月日 = ((DateTime)m04d.実施年月日),
                                                実施機関名 = m04d.実施機関名,
                                                所見摘要等 = m04d.所見摘要,
                                                コードFrom = p乗務員From,
                                                コードTo = p乗務員To,
                                                コードList = s乗務員List,
                                                期間From = i集計期間From,
                                                期間To = i集計期間To,
                                                表示順序 = i表示順序 == 0 ? "ID順" : i表示順序 == 1 ? "カナ順" : i表示順序 == 2 ? "日付順" : "",
                                                年 = (m04d.実施年月日 == null) ? 0 : (int)((DateTime)m04d.実施年月日).Year,

                                            });
                    }
                    else
                    {
                        query = query.Where(c => c.コード > int.MinValue && c.コード < int.MaxValue);
                    }

                }

                query = query.Distinct();

                switch (i表示順序)
                {
                    case 0:
                        query = query.OrderBy(c => c.コード);
                        break;
                    case 1:
                        query = query.OrderBy(c => c.かな読み);
                        break;
                    case 2:
                        //query = query.OrderByDescending(c => c.売上金額);
                        break;
                }
                //結果をリスト化
                retList = query.ToList();
                return retList;
            }
        }
        #endregion

        #region 事故違反履歴一覧表印刷
        /// <summary>
        /// JMI08010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<JMI08010B_Member> GetDataListB(string p乗務員From, string p乗務員To, int?[] i乗務員List, int p作成締日, int i集計期間From, int i集計期間To, string p作成年度, string s乗務員List, int i表示順序)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<JMI08010B_Member> retList = new List<JMI08010B_Member>();
                context.Connection.Open();

                var query = (from m04 in context.M04_DRV
                             join m04d in context.M04_DDT2.Where(m04d => ((DateTime)m04d.発生年月日).Year >= i集計期間From
                                                            && ((DateTime)m04d.発生年月日).Year <= i集計期間To)
                                                                on m04.乗務員KEY equals m04d.乗務員KEY
                             select new JMI08010B_Member
                             {
                                 コード = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 かな読み = m04.かな読み,
                                 発生年月日 = ((DateTime)m04d.発生年月日),
                                 概要処置等 = m04d.概要処置,
                                 コードFrom = p乗務員From,
                                 コードTo = p乗務員To,
                                 コードList = s乗務員List,
                                 期間From = i集計期間From,
                                 期間To = i集計期間To,
                                 表示順序 = i表示順序 == 0 ? "ID順" : i表示順序 == 1 ? "カナ順" : i表示順序 == 2 ? "日付順" : "",
                                 年 = ((DateTime)m04d.発生年月日).Year == null ? 0 : ((DateTime)m04d.発生年月日).Year,

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
                                     join m04d in context.M04_DDT2.Where(m04d => ((DateTime)m04d.発生年月日).Year >= i集計期間From
                                                                    && ((DateTime)m04d.発生年月日).Year <= i集計期間To)
                                                                        on m04.乗務員KEY equals m04d.乗務員KEY
                                     where intCause.Contains(m04.乗務員ID)
                                     select new JMI08010B_Member
                                     {
                                         コード = m04.乗務員ID,
                                         乗務員名 = m04.乗務員名,
                                         かな読み = m04.かな読み,
                                         発生年月日 = ((DateTime)m04d.発生年月日),
                                         概要処置等 = m04d.概要処置,
                                         コードFrom = p乗務員From,
                                         コードTo = p乗務員To,
                                         コードList = s乗務員List,
                                         期間From = i集計期間From,
                                         期間To = i集計期間To,
                                         表示順序 = i表示順序 == 0 ? "ID順" : i表示順序 == 1 ? "カナ順" : i表示順序 == 2 ? "日付順" : "",
                                         年 = ((DateTime)m04d.発生年月日).Year == null ? 0 : ((DateTime)m04d.発生年月日).Year,

                                     });
                    }
                    else
                    {
                        query = query.Where(c => c.コード > int.MinValue && c.コード < int.MaxValue);
                    }

                }

                query = query.Distinct();

                switch (i表示順序)
                {
                    case 0:
                        query = query.OrderBy(c => c.コード);
                        break;
                    case 1:
                        query = query.OrderBy(c => c.かな読み);
                        break;
                    case 2:
                        //query = query.OrderByDescending(c => c.売上金額);
                        break;
                }
                //結果をリスト化
                retList = query.ToList();
                return retList;
            }
        }
        #endregion

        #region 特別教育実施状況印刷
        /// <summary>
        /// JMI08010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<JMI08010C_Member> GetDataListC(string p乗務員From, string p乗務員To, int?[] i乗務員List, int p作成締日, int i集計期間From, int i集計期間To, string p作成年度, string s乗務員List, int i表示順序)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<JMI08010C_Member> retList = new List<JMI08010C_Member>();
                context.Connection.Open();

                var query = (from m04 in context.M04_DRV
                             join m04d in context.M04_DDT3.Where(m04d => ((DateTime)m04d.実施年月日).Year >= i集計期間From
                                                            && ((DateTime)m04d.実施年月日).Year <= i集計期間To)
                                                                on m04.乗務員KEY equals m04d.乗務員KEY
                             select new JMI08010C_Member
                             {
                                 コード = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 かな読み = m04.かな読み,
                                 実施年月日 = (DateTime)m04d.実施年月日,
                                 教育内容 = m04d.教育内容,
                                 区分 = m04d.教育種類 == 0 ? "初任" : m04d.教育種類 == 1 ? "高齢" : m04d.教育種類 == 2 ? "事故惹起" : "",
                                 コードFrom = p乗務員From,
                                 コードTo = p乗務員To,
                                 コードList = s乗務員List,
                                 期間From = i集計期間From,
                                 期間To = i集計期間To,
                                 表示順序 = i表示順序 == 0 ? "ID順" : i表示順序 == 1 ? "カナ順" : i表示順序 == 2 ? "日付順" : "",
                                 年 = ((DateTime)m04d.実施年月日).Year == null ? 0 : ((DateTime)m04d.実施年月日).Year,

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
                                 join m04d in context.M04_DDT3.Where(m04d => ((DateTime)m04d.実施年月日).Year >= i集計期間From
                                                                && ((DateTime)m04d.実施年月日).Year <= i集計期間To)
                                                                    on m04.乗務員KEY equals m04d.乗務員KEY
                                 where intCause.Contains(m04.乗務員ID)
                                 select new JMI08010C_Member
                                 {
                                     コード = m04.乗務員ID,
                                     乗務員名 = m04.乗務員名,
                                     かな読み = m04.かな読み,
                                     実施年月日 = (DateTime)m04d.実施年月日,
                                     教育内容 = m04d.教育内容,
                                     区分 = m04d.教育種類 == 0 ? "初任" : m04d.教育種類 == 1 ? "高齢" : m04d.教育種類 == 2 ? "事故惹起" : "",
                                     コードFrom = p乗務員From,
                                     コードTo = p乗務員To,
                                     コードList = s乗務員List,
                                     期間From = i集計期間From,
                                     期間To = i集計期間To,
                                     表示順序 = i表示順序 == 0 ? "ID順" : i表示順序 == 1 ? "カナ順" : i表示順序 == 2 ? "日付順" : "",
                                     年 = ((DateTime)m04d.実施年月日).Year == null ? 0 : ((DateTime)m04d.実施年月日).Year,

                                 });
                    }
                    else
                    {
                        query = query.Where(c => c.コード > int.MinValue && c.コード < int.MaxValue);
                    }

                }

                query = query.Distinct();

                switch (i表示順序)
                {
                    case 0:
                        query = query.OrderBy(c => c.コード);
                        break;
                    case 1:
                        query = query.OrderBy(c => c.かな読み);
                        break;
                    case 2:
                        //query = query.OrderByDescending(c => c.売上金額);
                        break;
                }

                //結果をリスト化
                retList = query.ToList();
                return retList;
            }
        }
        #endregion



        #region CSV出力
        #endregion

        #region 適正診断一覧印刷
        /// <summary>
        /// JMI08010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<JMI08010A_CSV_Member> GetDataListA_CSV(string p乗務員From, string p乗務員To, int?[] i乗務員List, int p作成締日, int i集計期間From, int i集計期間To, string p作成年度, string s乗務員List, int i表示順序)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<JMI08010A_CSV_Member> retList = new List<JMI08010A_CSV_Member>();
                context.Connection.Open();

                var query = (from m04 in context.M04_DRV
                             join m04d in context.M04_DDT1.Where(m04d => ((DateTime)m04d.実施年月日).Year >= i集計期間From
                                                            && ((DateTime)m04d.実施年月日).Year <= i集計期間To)
                                                                on m04.乗務員KEY equals m04d.乗務員KEY
                             join m99 in context.M99_COMBOLIST.Where(m99 => m99.分類 == "乗務員" && m99.機能 == "乗務員状況履歴" && m99.カテゴリ == "運転適正診断")
                                      on m04d.対象種類 equals m99.コード

                             select new JMI08010A_CSV_Member
                             {
                                 コード = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 かな読み = m04.かな読み,
                                 種別 = m99.表示名,
                                 実施年月日 = ((DateTime)m04d.実施年月日),
                                 実施機関名 = m04d.実施機関名,
                                 所見摘要等 = m04d.所見摘要,
                                 期間From = i集計期間From,
                                 期間To = i集計期間To,
                                 年 = (m04d.実施年月日 == null) ? 0 : (int)((DateTime)m04d.実施年月日).Year,

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
                                            join m04d in context.M04_DDT1.Where(m04d => ((DateTime)m04d.実施年月日).Year >= i集計期間From
                                                                           && ((DateTime)m04d.実施年月日).Year <= i集計期間To)
                                                                               on m04.乗務員KEY equals m04d.乗務員KEY
                                            join m99 in context.M99_COMBOLIST.Where(m99 => m99.分類 == "乗務員" && m99.機能 == "乗務員状況履歴" && m99.カテゴリ == "運転適正診断")
                                                     on m04d.対象種類 equals m99.コード
                                            where intCause.Contains(m04.乗務員ID)
                                            select new JMI08010A_CSV_Member
                                            {
                                                コード = m04.乗務員ID,
                                                乗務員名 = m04.乗務員名,
                                                かな読み = m04.かな読み,
                                                種別 = m99.表示名,
                                                実施年月日 = ((DateTime)m04d.実施年月日),
                                                実施機関名 = m04d.実施機関名,
                                                所見摘要等 = m04d.所見摘要,
                                                期間From = i集計期間From,
                                                期間To = i集計期間To,
                                                年 = (m04d.実施年月日 == null) ? 0 : (int)((DateTime)m04d.実施年月日).Year,

                                            });
                    }
                    else
                    {
                        query = query.Where(c => c.コード > int.MinValue && c.コード < int.MaxValue);
                    }

                }

                query = query.Distinct();

                switch (i表示順序)
                {
                    case 0:
                        query = query.OrderBy(c => c.コード);
                        break;
                    case 1:
                        query = query.OrderBy(c => c.かな読み);
                        break;
                    case 2:
                        //query = query.OrderByDescending(c => c.売上金額);
                        break;
                }

                //結果をリスト化
                retList = query.ToList();
                return retList;
            }
        }
        #endregion

        #region 事故違反履歴一覧表印刷
        /// <summary>
        /// JMI08010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<JMI08010B_CSV_Member> GetDataListB_CSV(string p乗務員From, string p乗務員To, int?[] i乗務員List, int p作成締日, int i集計期間From, int i集計期間To, string p作成年度, string s乗務員List, int i表示順序)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<JMI08010B_CSV_Member> retList = new List<JMI08010B_CSV_Member>();
                context.Connection.Open();

                var query = (from m04 in context.M04_DRV
                             join m04d in context.M04_DDT2.Where(m04d => ((DateTime)m04d.発生年月日).Year >= i集計期間From
                                                            && ((DateTime)m04d.発生年月日).Year <= i集計期間To)
                                                                on m04.乗務員KEY equals m04d.乗務員KEY
                             select new JMI08010B_CSV_Member
                             {
                                 コード = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 かな読み = m04.かな読み,
                                 発生年月日 = ((DateTime)m04d.発生年月日),
                                 概要処置等 = m04d.概要処置,
                                 期間From = i集計期間From,
                                 期間To = i集計期間To,
                                 年 = ((DateTime)m04d.発生年月日).Year == null ? 0 : ((DateTime)m04d.発生年月日).Year,

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
                                            join m04d in context.M04_DDT2.Where(m04d => ((DateTime)m04d.発生年月日).Year >= i集計期間From
                                                                           && ((DateTime)m04d.発生年月日).Year <= i集計期間To)
                                                                               on m04.乗務員KEY equals m04d.乗務員KEY
                                            where intCause.Contains(m04.乗務員ID)
                                            select new JMI08010B_CSV_Member
                                            {
                                                コード = m04.乗務員ID,
                                                乗務員名 = m04.乗務員名,
                                                かな読み = m04.かな読み,
                                                発生年月日 = ((DateTime)m04d.発生年月日),
                                                概要処置等 = m04d.概要処置,
                                                期間From = i集計期間From,
                                                期間To = i集計期間To,
                                                年 = ((DateTime)m04d.発生年月日).Year == null ? 0 : ((DateTime)m04d.発生年月日).Year,

                                            });
                    }
                    else
                    {
                        query = query.Where(c => c.コード > int.MinValue && c.コード < int.MaxValue);
                    }

                }

                query = query.Distinct();

                switch (i表示順序)
                {
                    case 0:
                        query = query.OrderBy(c => c.コード);
                        break;
                    case 1:
                        query = query.OrderBy(c => c.かな読み);
                        break;
                    case 2:
                        //query = query.OrderByDescending(c => c.売上金額);
                        break;
                }

                //結果をリスト化
                retList = query.ToList();
                return retList;
            }
        }
        #endregion


        #region 特別教育実施状況印刷
        /// <summary>
        /// JMI08010 印刷
        /// </summary>
        /// <param name="p商品ID">乗務員コード</param>
        /// <returns>T01</returns>
        public List<JMI08010C_CSV_Member> GetDataListC_CSV(string p乗務員From, string p乗務員To, int?[] i乗務員List, int p作成締日, int i集計期間From, int i集計期間To, string p作成年度, string s乗務員List, int i表示順序)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<JMI08010C_CSV_Member> retList = new List<JMI08010C_CSV_Member>();
                context.Connection.Open();

                var query = (from m04 in context.M04_DRV
                             join m04d in context.M04_DDT3.Where(m04d => ((DateTime)m04d.実施年月日).Year >= i集計期間From
                                                            && ((DateTime)m04d.実施年月日).Year <= i集計期間To)
                                                                on m04.乗務員KEY equals m04d.乗務員KEY
                             select new JMI08010C_CSV_Member
                             {
                                 コード = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 かな読み = m04.かな読み,
                                 実施年月日 = (DateTime)m04d.実施年月日,
                                 教育内容 = m04d.教育内容,
                                 区分 = m04d.教育種類 == 0 ? "初任" : m04d.教育種類 == 1 ? "高齢" : m04d.教育種類 == 2 ? "事故惹起" : "",
                                 期間From = i集計期間From,
                                 期間To = i集計期間To,
                                 年 = ((DateTime)m04d.実施年月日).Year == null ? 0 : ((DateTime)m04d.実施年月日).Year,

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
                                            join m04d in context.M04_DDT3.Where(m04d => ((DateTime)m04d.実施年月日).Year >= i集計期間From
                                                                           && ((DateTime)m04d.実施年月日).Year <= i集計期間To)
                                                                               on m04.乗務員KEY equals m04d.乗務員KEY
                                            where intCause.Contains(m04.乗務員ID)
                                            select new JMI08010C_CSV_Member
                                            {
                                                コード = m04.乗務員ID,
                                                乗務員名 = m04.乗務員名,
                                                かな読み = m04.かな読み,
                                                実施年月日 = (DateTime)m04d.実施年月日,
                                                教育内容 = m04d.教育内容,
                                                区分 = m04d.教育種類 == 0 ? "初任" : m04d.教育種類 == 1 ? "高齢" : m04d.教育種類 == 2 ? "事故惹起" : "",
                                                期間From = i集計期間From,
                                                期間To = i集計期間To,
                                                年 = ((DateTime)m04d.実施年月日).Year == null ? 0 : ((DateTime)m04d.実施年月日).Year,

                                            });
                    }
                    else
                    {
                        query = query.Where(c => c.コード > int.MinValue && c.コード < int.MaxValue);
                    }

                }

                query = query.Distinct();

                switch (i表示順序)
                {
                    case 0:
                        query = query.OrderBy(c => c.コード);
                        break;
                    case 1:
                        query = query.OrderBy(c => c.かな読み);
                        break;
                    case 2:
                        //query = query.OrderByDescending(c => c.売上金額);
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