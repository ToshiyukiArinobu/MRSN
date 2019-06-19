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
    /// NNG06010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class NNG06010_Member
    {
        public decimal? 売上金額 { get; set; }
        public decimal? 割増1 { get; set; }
        public decimal? 割増2 { get; set; }
        public decimal? 通行料 { get; set; }
        public decimal? 売上合計 { get; set; }
        public decimal? 傭車使用売上 { get; set; }
        public decimal? 支払金額 { get; set; }
        public decimal? 支払通行料 { get; set; }
        public decimal? 差益 { get; set; }
        public decimal? 差益率 { get; set; }
        public int? 件数 { get; set; }
        public int 未定件数 { get; set; }
        public int? コード { get; set; }
        public string 部門名 { get; set; }
        public DateTime 期間From { get; set; }
        public DateTime 期間To { get; set; }
        public string コードFrom { get; set; }
        public string コードTo { get; set; }
        public string コードList { get; set; }
        public string 表示順 { get; set; }
        public string かな読み { get; set; }

    }


    /// <summary>
    /// NNG06010  CSV　メンバー
    /// </summary>
    [DataContract]
    public class NNG06010_Member_CSV
    {
        public int? コード { get; set; }
        public string 部門名 { get; set; }
        public string かな読み { get; set; }
        public decimal? 売上金額 { get; set; }
        public decimal? 割増1 { get; set; }
        public decimal? 割増2 { get; set; }
        public decimal? 通行料 { get; set; }
        public decimal? 売上合計 { get; set; }
        public decimal? 傭車使用売上 { get; set; }
        public decimal? 支払金額 { get; set; }
        public decimal? 支払通行料 { get; set; }
        public decimal? 差益 { get; set; }
        public decimal? 差益率 { get; set; }
        public int? 件数 { get; set; }
        public int 未定件数 { get; set; }
    }


    [DataContract]
    public class NNG06010_M07
    {
        [DataMember]
        public int 経費ID { get; set; }
        [DataMember]
        public string 経費名 { get; set; }
    }
    

    public class NNG06010
    {
        #region 印刷
        /// <summary>
        /// NNG06010 印刷
        /// </summary>
        /// <param name="p商品ID">部門コード</param>
        /// <returns>T01</returns>
        public List<NNG06010_Member> GetDataList(string p部門From, string p部門To, int?[] i部門List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string s部門List, int i表示順 )
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<NNG06010_Member> retList = new List<NNG06010_Member>();
                context.Connection.Open();

                int[] lst;
                lst = (from m71 in context.M71_BUM
                       let t01l = from t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1))) select t01.自社部門ID
                       where t01l.Contains(m71.自社部門ID)
                       select m71.自社部門ID).ToArray();

                var query = (from m71 in context.M71_BUM
                             join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1))) on m71.自社部門ID equals t01.自社部門ID into t01Group
                             where t01Group.Where(t01 => t01.自社部門ID == m71.自社部門ID).Any() == true
                             select new NNG06010_Member
                             {
                                 
                                 コード = m71.自社部門ID,
                                 部門名 = m71.自社部門名,
                                 売上金額 = t01Group.Sum(t01 => t01.売上金額) == null ? 0 : t01Group.Sum(t01 => t01.売上金額),
                                 割増1 = t01Group.Sum(t01 => t01.請求割増１) == null ? 0 : t01Group.Sum(t01 => t01.請求割増１),
                                 割増2 = t01Group.Sum(t01 => t01.請求割増２) == null ? 0 : t01Group.Sum(t01 => t01.請求割増２),
                                 通行料 = t01Group.Sum(t01 => t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.通行料),
                                 売上合計 = t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 傭車使用売上 = t01Group.Where(t01gr => t01gr.支払先KEY > 0).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料),
                                 支払金額 = t01Group.Where(t01gr => t01gr.支払先KEY > 0).Sum(t01gr => t01gr.支払金額),
                                 支払通行料 = t01Group.Where(t01gr => t01gr.支払先KEY > 0).Sum(t01gr => t01gr.支払通行料),
                                 差益 = t01Group.Where(t01gr => t01gr.支払先KEY > 0).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料 - t01gr.支払金額 - t01gr.支払通行料),
                                 差益率 = t01Group.Where(t01gr => t01gr.支払先KEY > 0).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料) == 0 ? 0 :
                                            Math.Round((decimal)t01Group.Where(t01gr => t01gr.支払先KEY > 0).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料 - t01gr.支払金額 - t01gr.支払通行料) / t01Group.Where(t01gr => t01gr.支払先KEY > 0).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料), 2),
                                 件数 = t01Group.Count(),
                                 未定件数 = t01Group.Count(t01gr => t01gr.売上未定区分 == 1),

                                 コードFrom = p部門From,
                                 コードTo = p部門To,
                                 コードList = s部門List,
                                 期間From = d集計期間From,
                                 期間To = d集計期間To,
                                 かな読み = m71.かな読み,
                                 表示順 = i表示順 == 0 ? "ID順" : i表示順 == 1 ? "かな読み" : "売上順",


                             }).AsQueryable();


                int i部門FROM;
                int i部門TO;
                //部門From処理　Min値
                if (!string.IsNullOrEmpty(p部門From))
                {
                    i部門FROM = AppCommon.IntParse(p部門From);
                }
                else
                {
                    i部門FROM = int.MinValue;
                }

                //部門To処理　Max値
                if (!string.IsNullOrEmpty(p部門To))
                {
                    i部門TO = AppCommon.IntParse(p部門To);
                }
                else
                {
                    i部門TO = int.MaxValue;
                }

                var intCause = i部門List;
                if (string.IsNullOrEmpty(p部門From + p部門To))
                {
                    if (i部門List.Length > 0)
                    {
                        query = query.Where(q => intCause.Contains(q.コード));
                    }
                }
                else
                {
                    if (i部門List.Length > 0)
                    {
                        query = query.Where(q => intCause.Contains(q.コード) || (q.コード >= i部門FROM && q.コード <= i部門TO));
                    }
                    else
                    {
                        query = query.Where(q => (q.コード >= i部門FROM && q.コード <= i部門TO));
                    }
                }

                //表示順序処理
                switch (i表示順)
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
                query = query.Distinct();
                retList = query.ToList();
                return retList;

            }

        }
        #endregion

        #region CSV出力
        /// <summary>
        /// NNG06010 印刷
        /// </summary>
        /// <param name="p商品ID">部門コード</param>
        /// <returns>T01</returns>
        public List<NNG06010_Member_CSV> GetDataList_CSV(string p部門From, string p部門To, int?[] i部門List, int p作成締日, DateTime d集計期間From, DateTime d集計期間To, string p作成年度, string s部門List, int i表示順)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<NNG06010_Member_CSV> retList = new List<NNG06010_Member_CSV>();
                context.Connection.Open();

                int[] lst;
                lst = (from m71 in context.M71_BUM
                       let t01l = from t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1))) select t01.自社部門ID
                       where t01l.Contains(m71.自社部門ID)
                       select m71.自社部門ID).ToArray();

                var query = (from m71 in context.M71_BUM
                             join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1))) on m71.自社部門ID equals t01.自社部門ID into t01Group
                             where t01Group.Where(t01 => t01.自社部門ID == m71.自社部門ID).Any() == true
                             select new NNG06010_Member_CSV
                             {

                                 コード = m71.自社部門ID,
                                 部門名 = m71.自社部門名,
                                 売上金額 = t01Group.Sum(t01 => t01.売上金額) == null ? 0 : t01Group.Sum(t01 => t01.売上金額),
                                 割増1 = t01Group.Sum(t01 => t01.請求割増１) == null ? 0 : t01Group.Sum(t01 => t01.請求割増１),
                                 割増2 = t01Group.Sum(t01 => t01.請求割増２) == null ? 0 : t01Group.Sum(t01 => t01.請求割増２),
                                 通行料 = t01Group.Sum(t01 => t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.通行料),
                                 売上合計 = t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 傭車使用売上 = t01Group.Where(t01gr => t01gr.支払先KEY > 0).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料),
                                 支払金額 = t01Group.Where(t01gr => t01gr.支払先KEY > 0).Sum(t01gr => t01gr.支払金額),
                                 支払通行料 = t01Group.Where(t01gr => t01gr.支払先KEY > 0).Sum(t01gr => t01gr.支払通行料),
                                 差益 = t01Group.Where(t01gr => t01gr.支払先KEY > 0).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料 - t01gr.支払金額 - t01gr.支払通行料),
                                 差益率 = t01Group.Where(t01gr => t01gr.支払先KEY > 0).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料) == 0 ? 0 :
                                            Math.Round((decimal)t01Group.Where(t01gr => t01gr.支払先KEY > 0).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料 - t01gr.支払金額 - t01gr.支払通行料) / t01Group.Where(t01gr => t01gr.支払先KEY > 0).Sum(t01gr => t01gr.売上金額 + t01gr.請求割増１ + t01gr.請求割増２ + t01gr.通行料), 2),
                                 件数 = t01Group.Count(),
                                 未定件数 = t01Group.Count(t01gr => t01gr.売上未定区分 == 1),

                                 かな読み = m71.かな読み,


                             }).AsQueryable();


                int i部門FROM;
                int i部門TO;
                //部門From処理　Min値
                if (!string.IsNullOrEmpty(p部門From))
                {
                    i部門FROM = AppCommon.IntParse(p部門From);
                }
                else
                {
                    i部門FROM = int.MinValue;
                }

                //部門To処理　Max値
                if (!string.IsNullOrEmpty(p部門To))
                {
                    i部門TO = AppCommon.IntParse(p部門To);
                }
                else
                {
                    i部門TO = int.MaxValue;
                }

                var intCause = i部門List;
                if (string.IsNullOrEmpty(p部門From + p部門To))
                {
                    if (i部門List.Length > 0)
                    {
                        query = query.Where(q => intCause.Contains(q.コード));
                    }
                }
                else
                {
                    if (i部門List.Length > 0)
                    {
                        query = query.Where(q => intCause.Contains(q.コード) || (q.コード >= i部門FROM && q.コード <= i部門TO));
                    }
                    else
                    {
                        query = query.Where(q => (q.コード >= i部門FROM && q.コード <= i部門TO));
                    }
                }

                //表示順序処理
                switch (i表示順)
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
                query = query.Distinct();
                retList = query.ToList();
                return retList;

            }

        }
        #endregion
    }
}