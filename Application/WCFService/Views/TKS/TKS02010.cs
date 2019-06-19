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
    #region TKS02010_Member

    /// <summary>
    /// TKS02010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class TKS02010_Member
    {
        [DataMember] public int? 得意先コード { get; set; }
        [DataMember] public string T郵便番号 { get; set; }
        [DataMember] public string T住所1 { get; set; }
        [DataMember] public string T住所2 { get; set; }
        [DataMember] public string 得意先名 { get; set; }
        [DataMember] public string 請求内訳名 { get; set; }
        [DataMember] public string 自社名 { get; set; }
        [DataMember] public string J郵便番号 { get; set; }
        [DataMember] public string J住所1 { get; set; }
        [DataMember] public string J住所2 { get; set; }
        [DataMember] public string TEL { get; set; }
        [DataMember] public string FAX { get; set; }
        [DataMember] public string 振込銀行1 { get; set; }
        [DataMember] public string 振込銀行2 { get; set; }
        [DataMember] public string 振込銀行3 { get; set; }
        [DataMember] public DateTime 日付 { get; set; }
        [DataMember] public string 発地名 { get; set; }
        [DataMember] public string 着地名 { get; set; }
        [DataMember] public string 品名 { get; set; }
        [DataMember] public decimal 数量 { get; set; }
        [DataMember] public decimal 売上単価 { get; set; }
        [DataMember] public int 売上金額 { get; set; }
        [DataMember] public int 通行料 { get; set; }
        [DataMember] public string 摘要 { get; set; }
        [DataMember] public int 締日 { get; set; }
        [DataMember] public int 内課税金額 { get; set; }
        [DataMember] public int 売上金額合計 { get; set; }
        [DataMember] public int 当月合計額 { get; set; }
  }
    #endregion

    #region TKS02010_Goukei_Member
    /// <summary>
    /// TKS02010 当月合計金額
    /// </summary>
    public class TKS02010_Goukei_Member
    {
        [DataMember]
        public int 得意先コード { get; set; }
        [DataMember]
        public string T郵便番号 { get; set; }
        [DataMember]
        public string T住所1 { get; set; }
        [DataMember]
        public string T住所2 { get; set; }
        [DataMember]
        public string 得意先名 { get; set; }
        [DataMember]
        public string 請求内訳名 { get; set; }
        [DataMember]
        public string 自社名 { get; set; }
        [DataMember]
        public string J郵便番号 { get; set; }
        [DataMember]
        public string J住所1 { get; set; }
        [DataMember]
        public string J住所2 { get; set; }
        [DataMember]
        public string TEL { get; set; }
        [DataMember]
        public string FAX { get; set; }
        [DataMember]
        public string 振込銀行1 { get; set; }
        [DataMember]
        public string 振込銀行2 { get; set; }
        [DataMember]
        public string 振込銀行3 { get; set; }
        [DataMember]
        public DateTime 日付 { get; set; }
        [DataMember]
        public string 発地名 { get; set; }
        [DataMember]
        public string 着地名 { get; set; }
        [DataMember]
        public string 品名 { get; set; }
        [DataMember]
        public decimal 数量 { get; set; }
        [DataMember]
        public decimal 売上単価 { get; set; }
        [DataMember]
        public int 売上金額 { get; set; }
        [DataMember]
        public int 通行料 { get; set; }
        [DataMember]
        public string 摘要 { get; set; }
        [DataMember]
        public int 締日 { get; set; }
        [DataMember]
        public int 内課税金額 { get; set; }
        [DataMember]
        public int 売上金額合計 { get; set; }
    }
    [DataContract]

    #endregion

    #region TKS02010_SPREAD_Member
    public class TKS02010_SPREAD_Member
    {
        [DataMember]
        public int 取引先ID { get; set; }
        [DataMember]
        public string 取引先名 { get; set; }
        [DataMember]
        public string 内訳名 { get; set; }
        [DataMember]
        public string 郵便番号 { get; set; }
        [DataMember]
        public string 住所1 { get; set; }
        [DataMember]
        public string 住所2 { get; set; }
        [DataMember]
        public string 電話番号 { get; set; }
        [DataMember]
        public decimal 請求額 { get; set; }
    }

    #endregion

    public class TKS02010
    {
        /// <summary>
        /// 売上明細書
        /// </summary>
        /// <returns></returns>
        public List<TKS02010_Member> GetListTKS02010(int? i得意先コード, int?[] i得意先List, int? i自社コード, int? i作成締日, DateTime? d集計期間From, DateTime? d集計期間To)
        {
            List<TKS02010_Member> retList = new List<TKS02010_Member>();
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from t01 in context.T01_TRN.Where(x => x.入力区分 != 3 || (x.入力区分 == 3 && x.明細行 == 1))
                             from m01 in context.M01_TOK.Where(x => x.得意先KEY == t01.得意先KEY)
                             from m10 in context.M10_UHK.Where(x => x.得意先KEY == t01.得意先KEY && x.請求内訳ID == t01.請求内訳ID)
                             from m70 in context.M70_JIS.Where(x => x.自社ID == i自社コード)
                             where m01.削除日付 == null
                             select new TKS02010_Member
                             {
                                 得意先コード = m01.得意先ID,
                                 T郵便番号 = m01.郵便番号,
                                 T住所1 = m01.住所１,
                                 T住所2 = m01.住所２,
                                 得意先名 = m01.得意先名１,
                                 請求内訳名 = m10.請求内訳名 == null ? "なし" : m10.請求内訳名,
                                 自社名 = m70.自社名,
                                 J郵便番号 = m70.郵便番号,
                                 J住所1 = m70.住所１,
                                 J住所2 = m70.住所２,
                                 TEL = m70.電話番号,
                                 FAX = m70.ＦＡＸ,
                                 振込銀行1 = m70.振込銀行１,
                                 振込銀行2 = m70.振込銀行２,
                                 振込銀行3 = m70.振込銀行３,
                                 日付 = t01.請求日付,
                                 発地名 = t01.発地名,
                                 着地名 = t01.着地名,
                                 品名 = t01.商品名,
                                 数量 = t01.数量,
                                 売上単価 = t01.売上単価,
                                 売上金額 = t01.売上金額,
                                 通行料 = t01.通行料,
                                 摘要 = t01.請求摘要 == null ? "なし": t01.請求摘要,
                                 締日 = m01.Ｔ締日,
                                 内課税金額 = t01.請求税区分 == 0 ? t01.売上金額 + t01.請求割増１ + t01.請求割増２ : 0 ,
                                 売上金額合計 = t01.売上金額 + t01.請求割増１ + t01.請求割増２,
                             }).AsQueryable();


                if (i得意先コード != 0 && i得意先List.Length != 0 || i得意先コード == 0 && i得意先List.Length != 0 || i得意先コード != 0 && i得意先List.Length == 0)
                {

                    //得意先コードがあるときのみ
                    if (i得意先コード != 0)
                    {
                        query = query.Where(x => x.得意先コード == i得意先コード);
                    }
                    else if(i得意先コード == 0)
                    {
                        query = query.Where(c => c.得意先コード >= int.MaxValue);
                    }

                    //作成締日
                    query = query.Where(x => x.締日 == i作成締日);

                    //集計期間
                    query = query.Where(x => x.日付 >= d集計期間From && x.日付 <= d集計期間To);

                    //ﾋﾟｯｸｱｯﾌﾟがtrueの時
                    if (i得意先List.Length > 0)
                    {
                        var intCause = i得意先List;
                        query = query.Union(from t01 in context.T01_TRN.Where(x => x.入力区分 != 3 || (x.入力区分 == 3 && x.明細行 == 1))
                                            from m01 in context.M01_TOK.Where(x => x.得意先KEY == t01.得意先KEY)
                                            from m10 in context.M10_UHK.Where(x => x.得意先KEY == t01.得意先KEY && x.請求内訳ID == t01.請求内訳ID)
                                            from m70 in context.M70_JIS.Where(x => x.自社ID == i自社コード)
                                            where intCause.Contains(m01.得意先ID) && m01.削除日付 == null
                                            select new TKS02010_Member
                                            {
                                                得意先コード = m01.得意先ID,
                                                T郵便番号 = m01.郵便番号,
                                                T住所1 = m01.住所１,
                                                T住所2 = m01.住所２,
                                                得意先名 = m01.得意先名１,
                                                請求内訳名 = m10.請求内訳名,
                                                自社名 = m70.自社名,
                                                J郵便番号 = m70.郵便番号,
                                                J住所1 = m70.住所１,
                                                J住所2 = m70.住所２,
                                                TEL = m70.電話番号,
                                                FAX = m70.ＦＡＸ,
                                                振込銀行1 = m70.振込銀行１,
                                                振込銀行2 = m70.振込銀行２,
                                                振込銀行3 = m70.振込銀行３,
                                                日付 = t01.請求日付,
                                                発地名 = t01.発地名,
                                                着地名 = t01.着地名,
                                                品名 = t01.商品名,
                                                数量 = t01.数量,
                                                売上単価 = t01.売上単価,
                                                売上金額 = t01.売上金額,
                                                通行料 = t01.通行料,
                                                摘要 = t01.請求摘要,
                                                締日 = m01.Ｔ締日,
                                                内課税金額 = t01.請求税区分 == 0 ? t01.売上金額 + t01.請求割増１ + t01.請求割増２ : 0,
                                                売上金額合計 = t01.売上金額 + t01.請求割増１ + t01.請求割増２,
                                            }).AsQueryable();
                    }

                    //作成締日
                    query = query.Where(x => x.締日 == i作成締日);

                    //集計期間
                    query = query.Where(x => x.日付 >= d集計期間From && x.日付 <= d集計期間To);

                    query = query.OrderBy(c => c.得意先コード);

                }
                else
                {
                    //* * * * * 得意先もﾋﾟｯｸｱｯﾌﾟ先がNullの場合全件取得 * * * * *//

                    //集計期間(全件取得)
                    if (d集計期間From != null && d集計期間To != null)
                    {
                        query = query.Where(x => x.日付 >= d集計期間From && x.日付 <= d集計期間To);
                    }
                    
                    //締日(ALL締日)
                    if(i作成締日 != null)
                    {
                        query = query.Where(x => x.締日 == i作成締日);
                    }

                    query = query.OrderBy(c => c.得意先コード);

                }


                //結果をリスト変換
                retList = query.ToList();
                return retList;
            }
        }
        //得意先コード, 自社ID, 作成締日, 集計期間From, 集計期間To
        public List<TKS02010_SPREAD_Member> GetListTKS02010_SPREAD(int i得意先コード, int i自社コード, int i作成締日, DateTime d集計期間From, DateTime d集計期間To)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from t01 in context.T01_TRN.Where(x => x.入力区分 != 3 || (x.入力区分 == 3 && x.明細行 == 1))
                             from m01 in context.M01_TOK.Where(c => c.得意先KEY == t01.得意先KEY && c.得意先ID == i得意先コード && c.削除日付 == null)
                             from m10 in context.M10_UHK.Where(c => c.請求内訳ID == t01.請求内訳ID).DefaultIfEmpty()
                             where m01.Ｔ締日 == i作成締日 && t01.請求日付 >= d集計期間From && t01.請求日付 <= d集計期間To
                             group new { t01 , m01 , m10 } by new { m01.得意先ID , m01.得意先名１ , m01.住所１ , m01.住所２ , m01.郵便番号 , m01.電話番号 , m10.請求内訳名 } into Group 
                             select new TKS02010_SPREAD_Member
                           {
                               取引先ID = Group.Key.得意先ID,
                               取引先名 = Group.Key.得意先名１,
                               内訳名 = Group.Key.請求内訳名 == null ? "なし" : Group.Key.請求内訳名,
                               郵便番号 = Group.Key.郵便番号,
                               住所1 = Group.Key.住所１,
                               住所2 = Group.Key.住所２,
                               電話番号 = Group.Key.電話番号,
                               請求額 = Group.Sum(c => c.t01.売上金額),
                           }).AsQueryable();

                return query.ToList();
            }
        }

    }
}