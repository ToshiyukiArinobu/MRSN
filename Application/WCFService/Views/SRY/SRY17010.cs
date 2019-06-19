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
    #region SRY17010_Member

    public class SRY17010_Member
    {
        [DataMember]
        public int? 車輌コード { get; set; }
        [DataMember]
        public int? 自社部門ID { get; set; }
        [DataMember]
        public string 車輌番号 { get; set; }
        [DataMember]
        public string 車輌名 { get; set; }
        [DataMember]
        public string 車種名 { get; set; }
        [DataMember]
        public DateTime? 前回日付 { get; set; }
        [DataMember]
        public string 項目 { get; set; }
        [DataMember]
        public int? 交換期間 { get; set; }
        [DataMember]
        public int? 交換距離 { get; set; }
        [DataMember]
        public decimal? 数量 { get; set; }
        [DataMember]
        public string 摘要 { get; set; }
        [DataMember]
        public int? 前回交換時距離 { get; set; }
        [DataMember]
        public int? 交換時距離 { get; set; }
        [DataMember]
        public int? 使用時間 { get; set; }
        [DataMember]
        public int? 使用距離 { get; set; }
        [DataMember]
        public DateTime 次回予定日 { get; set; }
        [DataMember]
        public int? 次回予定距離 { get; set; }
        [DataMember]
        public int? 明細No { get; set; }
        [DataMember]
        public int? 行 { get; set; }
        [DataMember]
        public DateTime 集計From { get; set; }
        [DataMember]
        public DateTime 集計To { get; set; }
        [DataMember]
        public string 作成年度 { get; set; }
    }

    public class SRY17010_Member1
    {
        [DataMember]
        public int? 車輌コード { get; set; }
        [DataMember]
        public int? 車輌KEY { get; set; }
        [DataMember]
        public string 車輌番号 { get; set; }
        [DataMember]
        public string 車輌登録番号 { get; set; }
        [DataMember]
        public string 車種名 { get; set; }
        [DataMember]
        public int? 自社部門ID { get; set; }
        [DataMember]
        public DateTime? 前回日付 { get; set; }
        [DataMember]
        public int? 前回交換時距離 { get; set; }
        [DataMember]
        public int? 経費項目ID { get; set; }
        [DataMember]
        public string 経費項目名 { get; set; }
        [DataMember]
        public DateTime? 経費発生日 { get; set; }
        [DataMember]
        public int? 交換期間 { get; set; }
        [DataMember]
        public int? 交換距離 { get; set; }
        [DataMember]
        public decimal? 数量 { get; set; }
        [DataMember]
        public string 摘要 { get; set; }
        [DataMember]
        public int? 交換時距離 { get; set; }
        [DataMember]
        public int? 使用時間 { get; set; }
        [DataMember]
        public int? 使用距離 { get; set; }
        [DataMember]
        public DateTime? 次回予定日 { get; set; }
        [DataMember]
        public int? 次回予定距離 { get; set; }
        [DataMember]
        public int 明細No { get; set; }
        [DataMember]
        public int 行 { get; set; }
        [DataMember]
        public DateTime 開始日付 { get; set; }
        [DataMember]
        public DateTime 終了日付 { get; set; }
        [DataMember]
        public string sDate { get; set; }

    }
    #endregion

    #region 計算方法
    /* 計算方法 */
    //[使用期間] 前回修理した日付と今回修理した日付の差
    //[使用距離] 前回のメータから今回のメーターの差
    //[次回予定日] 現在から使用期間を積算した値
    //[次回予定キロ] 交換距離と交換時距離の積算した値
    #endregion


    public class SRY17010
    {
        public List<SRY17010_Member1> SRY17010_GetDataHinList(string s車輌番号From, string s車輌番号To, int?[] i車輌ﾋﾟｯｸｱｯﾌﾟ, DateTime d集計期間From, DateTime d集計期間To, int? i自社部門ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                string sDate;
                sDate = d集計期間To.ToString();
                sDate = sDate.Substring(0, 4) + "年" + sDate.Substring(5, 2) + "月度";

                List<SRY17010_Member> retList = new List<SRY17010_Member>();
                context.Connection.Open();
                try
                {
                    var query = (from m05 in context.M05_CAR
                                 join t03 in context.T03_KTRN.Where(t03 => t03.経費項目ID >= 501 && t03.経費項目ID <= 506 // && t03.メーター != null
                                     && t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) on m05.車輌ID equals t03.車輌ID into KTRNGroup
                                 from t03Group in KTRNGroup.DefaultIfEmpty()
                                 join m06 in context.M06_SYA on m05.車種ID equals m06.車種ID into SYAGroup
                                 from m06Group in SYAGroup.DefaultIfEmpty()
                                 join m07kei in context.M07_KEI on t03Group.経費項目ID equals m07kei.経費項目ID into keiGroup
                                 from m07keiGroup in keiGroup.DefaultIfEmpty()
                                 join m07kou in context.M07_KOU on m07keiGroup.経費項目ID equals m07kou.経費項目ID into kouGroup
                                 from m07kouGroup in kouGroup.DefaultIfEmpty()
                                 where m05.削除日付 == null
                                 select new SRY17010_Member1
                                 {
                                     車輌コード = m05.車輌ID,
                                     車輌番号 = t03Group.車輌番号 == null ? t03Group.車輌番号 : t03Group.車輌番号,
                                     車輌登録番号 = m05.車輌登録番号,
                                     車種名 = m06Group.車種名,
                                     自社部門ID = t03Group.自社部門ID,
                                     前回日付 = null,
                                     経費発生日 = t03Group.経費発生日,
                                     経費項目名 = m07keiGroup.経費項目名,
                                     交換期間 = m07kouGroup.交換期間 == null ? 0 : m07kouGroup.交換期間,
                                     交換距離 = m07kouGroup.交換距離 == null ? 0 : m07kouGroup.交換距離,
                                     使用時間 = null,
                                     次回予定日 = null,
                                     数量 = t03Group.数量 == null ? 0 : t03Group.数量,
                                     摘要 = t03Group.摘要名,
                                     交換時距離 = t03Group.メーター == null ? 0 : t03Group.メーター,
                                     次回予定距離 = m07kouGroup.交換距離 + t03Group.メーター == null ? 0 : m07kouGroup.交換距離 + t03Group.メーター,
                                     明細No = t03Group.明細番号,
                                     行 = t03Group.明細行,
                                     前回交換時距離 = KTRNGroup.Where(t03 => t03.メーター != null).Max(t03 => t03.メーター) == null ? 0 : KTRNGroup.Where(t03 => t03.メーター != null).Max(t03 => t03.メーター),
                                     経費項目ID = t03Group.経費項目ID,
                                     開始日付 = d集計期間From,
                                     終了日付 = d集計期間To,
                                     sDate = sDate,
                                 }).Distinct().AsQueryable();

                    //***検索条件***//
                    if (!(string.IsNullOrEmpty(s車輌番号From + s車輌番号To) && i車輌ﾋﾟｯｸｱｯﾌﾟ.Length == 0))
                    {
                        if (string.IsNullOrEmpty(s車輌番号From + s車輌番号To))
                        {
                            query = query.Where(c => c.車輌コード >= int.MaxValue);
                        }

                        //車輌番号From処理　Min値
                        if (!string.IsNullOrEmpty(s車輌番号From))
                        {
                            int i車輌番号From = AppCommon.IntParse(s車輌番号From);
                            query = query.Where(c => c.車輌コード >= i車輌番号From);
                        }

                        //車輌番号To処理　Max値
                        if (!string.IsNullOrEmpty(s車輌番号To))
                        {
                            int i車輌番号To = AppCommon.IntParse(s車輌番号To);
                            query = query.Where(c => c.車輌コード <= i車輌番号To);
                        }

                        //自社部門
                        if (i自社部門ID != null)
                        {
                            //自社部門で検索
                            query = query.Where(c => c.自社部門ID == i自社部門ID);
                        }
                        else
                        {
                            //すべての自社部門で検索
                            query = query.Where(c => c.自社部門ID >= int.MinValue && c.自社部門ID <= int.MaxValue);
                        }

                        //支払先ﾋﾟｯｸｱｯﾌﾟ
                        if (i車輌ﾋﾟｯｸｱｯﾌﾟ.Length > 0)
                        {
                            var intCause = i車輌ﾋﾟｯｸｱｯﾌﾟ;
                            query = query.Union(from m05 in context.M05_CAR
                                                join t03 in context.T03_KTRN.Where(t03 => t03.経費項目ID >= 501 && t03.経費項目ID <= 506 // && t03.メーター != null
                                                    && t03.経費発生日 >= d集計期間From && t03.経費発生日 <= d集計期間To) on m05.車輌ID equals t03.車輌ID into KTRNGroup
                                                from t03Group in KTRNGroup.DefaultIfEmpty()
                                                join m06 in context.M06_SYA on m05.車種ID equals m06.車種ID into SYAGroup
                                                from m06Group in SYAGroup.DefaultIfEmpty()
                                                join m07kei in context.M07_KEI on t03Group.経費項目ID equals m07kei.経費項目ID into keiGroup
                                                from m07keiGroup in keiGroup.DefaultIfEmpty()
                                                join m07kou in context.M07_KOU on m07keiGroup.経費項目ID equals m07kou.経費項目ID into kouGroup
                                                from m07kouGroup in kouGroup.DefaultIfEmpty()
                                                where m05.削除日付 == null && intCause.Contains(m05.車輌ID)
                                                select new SRY17010_Member1
                                                {
                                                    車輌コード = m05.車輌ID,
                                                    車輌番号 = t03Group.車輌番号 == null ? t03Group.車輌番号 : t03Group.車輌番号,
                                                    車輌登録番号 = m05.車輌登録番号,
                                                    車種名 = m06Group.車種名,
                                                    自社部門ID = t03Group.自社部門ID,
                                                    前回日付 = null,
                                                    経費発生日 = t03Group.経費発生日,
                                                    経費項目名 = m07keiGroup.経費項目名,
                                                    交換期間 = m07kouGroup.交換期間 == null ? 0 : m07kouGroup.交換期間,
                                                    交換距離 = m07kouGroup.交換距離 == null ? 0 : m07kouGroup.交換距離,
                                                    使用時間 = null,
                                                    次回予定日 = null,
                                                    数量 = t03Group.数量 == null ? 0 : t03Group.数量,
                                                    摘要 = t03Group.摘要名,
                                                    交換時距離 = t03Group.メーター == null ? 0 : t03Group.メーター,
                                                    次回予定距離 = m07kouGroup.交換距離 + t03Group.メーター == null ? 0 : m07kouGroup.交換距離 + t03Group.メーター,
                                                    明細No = t03Group.明細番号,
                                                    行 = t03Group.明細行,
                                                    前回交換時距離 = KTRNGroup.Where(t03 => t03.メーター != null).Max(t03 => t03.メーター) == null ? 0 : KTRNGroup.Where(t03 => t03.メーター != null).Max(t03 => t03.メーター),
                                                    経費項目ID = t03Group.経費項目ID,
                                                    開始日付 = d集計期間From,
                                                    終了日付 = d集計期間To,
                                                    sDate = sDate,
                                                });

                            if (i自社部門ID != null)
                            {
                                //自社部門で検索
                                query = query.Where(c => c.自社部門ID == i自社部門ID);
                            }
                            else
                            {
                                //すべての自社部門で検索
                                query = query.Where(c => c.自社部門ID >= int.MinValue && c.自社部門ID <= int.MaxValue);
                            }
                        }
                    }
                    else
                    {
                        //車輌番号がFrom&ToがNullだった場合　全件取得
                        if (string.IsNullOrEmpty(s車輌番号From + s車輌番号To))
                        {
                            query = query.Where(c => c.車輌コード >= int.MinValue && c.車輌コード <= int.MaxValue);
                        }
                        //車輌番号FromがNullだった場合 Min ～ 車輌番号To
                        else if (string.IsNullOrEmpty(s車輌番号From))
                        {
                            int i車輌番号To = AppCommon.IntParse(s車輌番号To);
                            query = query.Where(c => c.車輌コード <= i車輌番号To);
                        }
                        //車輌番号ToがNullだった場合 s車輌番号From ～ Max
                        else if (string.IsNullOrEmpty(s車輌番号To))
                        {
                            int i車輌番号From = AppCommon.IntParse(s車輌番号From);
                            query = query.Where(c => c.車輌コード >= i車輌番号From);
                        }

                        //自社部門
                        if (i自社部門ID != null)
                        {
                            //自社部門で検索
                            query = query.Where(c => c.自社部門ID == i自社部門ID);
                        }
                        else
                        {
                            //すべての自社部門で検索
                            query = query.Where(c => c.自社部門ID >= int.MinValue && c.自社部門ID <= int.MaxValue);
                        }
                    }

                    //List<SRY17010_Member> queryLIST = new List<SRY17010_Member>();
                    List<SRY17010_Member1> queryLIST = query.ToList();
                    queryLIST = query.ToList();
                    int 使用時間TS = 0;
                    for (int i = 0; i < queryLIST.Count(); i++)
                    {
                        if (queryLIST[i].前回日付 == null)
                        {
                            DateTime? 前回日付 = queryLIST[i].経費発生日 - TimeSpan.Parse(queryLIST[i].交換期間.ToString());
                            queryLIST[i].前回日付 = 前回日付;
                        }

                        if (queryLIST[i].使用時間 == null)
                        {
                            TimeSpan TS使用時間 = Convert.ToDateTime(queryLIST[i].経費発生日) - Convert.ToDateTime(queryLIST[i].前回日付);
                            使用時間TS = AppCommon.IntParse(TS使用時間.Days.ToString()) * 24;
                            queryLIST[i].使用時間 = 使用時間TS;
                        }

                        if (queryLIST[i].次回予定日 == null)
                        {
                            DateTime? 次回予定日 = queryLIST[i].経費発生日 + TimeSpan.Parse(queryLIST[i].交換期間.ToString());
                            queryLIST[i].次回予定日 = 次回予定日;
                        }
                    }
                    return queryLIST;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}