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
    public class SRY19010g_Member
    {
        [DataMember]
        public int 車輌Key { get; set; }
        [DataMember]
        public decimal 燃費 { get; set; }
        [DataMember]
        public decimal 対比 { get; set; }
        [DataMember]
        public decimal 燃費差益 { get; set; }
        [DataMember]
        public decimal 燃料L { get; set; }
        [DataMember]
        public int 走行KM { get; set; }
    }


    #region SRY19010_Member

    public class SRY19010_Member
    {
        [DataMember]
        public int? 車輌ID { get; set; }
        [DataMember]
        public string 車輌番号 { get; set; }
        [DataMember]
        public string 乗務員名 { get; set; }
        [DataMember]
        public int 日数 { get; set; }
        [DataMember]
        public decimal 運賃 { get; set; }
        [DataMember]
        public decimal 壱日当り { get; set; }
        [DataMember]
        public decimal 輸送屯数 { get; set; }
        [DataMember]
        public int 走行KM { get; set; }
        [DataMember]
        public int 実車KM { get; set; }
        [DataMember]
        public decimal 燃料L { get; set; }
        [DataMember]
        public decimal 燃費 { get; set; }
        [DataMember]
        public decimal 燃料代 { get; set; }
        [DataMember]
        public decimal 前月燃費 { get; set; }
        [DataMember]
        public decimal 前月対比 { get; set; }
        [DataMember]
        public decimal 前月燃費差益 { get; set; }
        [DataMember]
        public decimal 前月燃料L { get; set; }
        [DataMember]
        public int 前月走行KM { get; set; }
        [DataMember]
        public decimal 前年燃費 { get; set; }
        [DataMember]
        public decimal 前年対比 { get; set; }
        [DataMember]
        public decimal 前年燃費差益 { get; set; }
        [DataMember]
        public decimal 前年燃料L { get; set; }
        [DataMember]
        public int 前年走行KM { get; set; }
        [DataMember]
        public string 作成年月 { get; set; }
        [DataMember]
        public string コードFrom { get; set; }
        [DataMember]
        public string コードTo { get; set; }
        [DataMember]
        public string 表示順序 { get; set; }
        [DataMember]
        public string 車輌指定 { get; set; }
        [DataMember]
        public string 車輌ﾋﾟｯｸｱｯﾌﾟ { get; set; }
    }

    #endregion

    public class SRY19010
    {
        #region SRY19010 印刷

        public List<SRY19010_Member> SRY19010_GetDataHinList(string s車輌From, string s車輌To, int?[] i車輌List, int p年月, int p前月, int p前年, int i表示順序, string s車輌List)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                string s作成年月;
                s作成年月 = p年月.ToString().Substring(0, 4) + "年" + p年月.ToString().Substring(4, 2) + "月度";


                List<SRY19010_Member> retList = new List<SRY19010_Member>();
                context.Connection.Open();
                try
                {
                    string 車輌ﾋﾟｯｸｱｯﾌﾟ指定 = string.Empty;

                    var ZENGETU = (from s14c in context.S14_CAR
                                  where s14c.集計年月 == p前月
                                  select new SRY19010g_Member
                                  {
                                      車輌Key = s14c.車輌KEY,
                                      燃費 = s14c.燃料Ｌ == 0 ? 0 : Math.Round((decimal)((s14c.走行ＫＭ / s14c.燃料Ｌ)), 2),
                                      走行KM = s14c.走行ＫＭ,
                                      燃料L = s14c.燃料Ｌ,
                                  }).AsQueryable();

                    var ZENNEN = (from s14c in context.S14_CAR
                                  where s14c.集計年月 == p前年
                                  select new SRY19010g_Member
                                  {
                                      車輌Key = s14c.車輌KEY,
                                      燃費 = s14c.燃料Ｌ == 0 ? 0 : Math.Round((decimal)((s14c.走行ＫＭ / s14c.燃料Ｌ)), 2),
                                      走行KM = s14c.走行ＫＭ,
                                      燃料L = s14c.燃料Ｌ,
                                  }).AsQueryable();

                    var query = (from m05 in context.M05_CAR.Where(c => c.廃車区分 == 0 && c.削除日付 == null)
                                 from m04 in context.M04_DRV.Where(c => c.削除日付 == null && c.乗務員KEY == m05.乗務員KEY).DefaultIfEmpty()
                                 from s14 in context.S14_CAR.Where(c => c.集計年月 == p年月 && c.車輌KEY == m05.車輌KEY)
                                 from s14sb in context.S14_CARSB.Where(c => c.集計年月 == p年月 && c.車輌KEY == m05.車輌KEY && c.経費項目ID == 401).DefaultIfEmpty()
                                 from zengetu in ZENGETU.Where(c => c.車輌Key == m05.車輌KEY).DefaultIfEmpty()
                                 from zennen in ZENNEN.Where(c => c.車輌Key == m05.車輌KEY).DefaultIfEmpty()
                                 select new SRY19010_Member
                                 {
                                     車輌ID = m05.車輌KEY,
                                     車輌番号 = m05.車輌番号,
                                     乗務員名 = m04.乗務員名,
                                     日数 = s14.稼動日数 == null ? 0 : s14.稼動日数,
                                     運賃 = s14.運送収入 == null ? 0 : s14.運送収入,
                                     壱日当り = s14.稼動日数 == null ? 0 : s14.稼動日数 == 0 ? 0 : Math.Round((decimal)((s14.運送収入 / s14.稼動日数)), 0),
                                     輸送屯数 = s14.輸送屯数 == null ? 0 : s14.輸送屯数,
                                     走行KM = s14.走行ＫＭ == null ? 0 : s14.走行ＫＭ,
                                     実車KM = s14.実車ＫＭ == null ? 0 : s14.実車ＫＭ,
                                     燃料L = s14.燃料Ｌ == null ? 0 : s14.燃料Ｌ,
                                     燃費 = s14.燃料Ｌ ==  null ? 0 : s14.燃料Ｌ == 0 ? 0 : Math.Round((decimal)((s14.走行ＫＭ / s14.燃料Ｌ)), 2),
                                     燃料代 = s14sb.金額 == null ? 0 : s14sb.金額,

                                     前月燃費 = zengetu.燃費 == null ? 0 : zengetu.燃費,
                                     前月対比 = zengetu.燃費 == null ? 0 : zengetu.燃費 == 0 ? 0 : s14.燃料Ｌ == 0 ? 0 : Math.Round((decimal)((s14.走行ＫＭ / s14.燃料Ｌ) / zengetu.燃費), 0),
                                     前月燃費差益 = zengetu.燃費 == null ? 0 : zengetu.燃費 == 0 ? 0 : s14.燃料Ｌ == 0 ? 0 : Math.Round((decimal)((s14sb.金額 / s14.燃料Ｌ) * (s14.走行ＫＭ / zengetu.燃費)), 0),
                                     前月走行KM = zengetu.走行KM == null ? 0 : zengetu.走行KM,
                                     前月燃料L = zengetu.燃料L == null ? 0 : zengetu.燃料L,

                                     前年燃費 = zennen.燃費 == null ? 0 : zennen.燃費,
                                     前年対比 = zennen.燃費 == null ? 0 : zennen.燃費 == 0 ? 0 : s14.燃料Ｌ == 0 ? 0 : Math.Round((decimal)((s14.走行ＫＭ / s14.燃料Ｌ) / zennen.燃費), 0),
                                     前年燃費差益 = zennen.燃費 == null ? 0 : zennen.燃費 == 0 ? 0 : s14.燃料Ｌ == 0 ? 0 : Math.Round((decimal)((s14sb.金額 / s14.燃料Ｌ) * (s14.走行ＫＭ / zennen.燃費)), 0),
                                     前年走行KM = zennen.走行KM == null ? 0 : zennen.走行KM,
                                     前年燃料L = zennen.燃料L == null ? 0 : zennen.燃料L,

                                     コードFrom = s車輌From,
                                     コードTo = s車輌To,
                                     車輌ﾋﾟｯｸｱｯﾌﾟ = s車輌List,
                                     作成年月 = s作成年月,

                                 }).AsQueryable();



                    //***検索条件***//
                    if (!(string.IsNullOrEmpty(s車輌From + s車輌To) && i車輌List.Length == 0))
                    {

                        //From & ToがNULLだった場合
                        if (string.IsNullOrEmpty(s車輌From + s車輌To))
                        {
                            query = query.Where(c => c.車輌ID >= int.MaxValue);
                        }

                        //車輌From処理　Min値
                        if (!string.IsNullOrEmpty(s車輌From))
                        {
                            int i車輌From = AppCommon.IntParse(s車輌From);
                            query = query.Where(c => c.車輌ID >= i車輌From);
                        }

                        //車輌To処理　Max値
                        if (!string.IsNullOrEmpty(s車輌To))
                        {
                            int i車輌TO = AppCommon.IntParse(s車輌To);
                            query = query.Where(c => c.車輌ID <= i車輌TO);
                        }

                        if (i車輌List.Length > 0)
                        {
                            var intCause = i車輌List;
                            query = query.Union(from m05 in context.M05_CAR.Where(c => c.廃車区分 == 0 && c.削除日付 == null)
                                                from m04 in context.M04_DRV.Where(c => c.削除日付 == null && c.乗務員KEY == m05.乗務員KEY).DefaultIfEmpty()
                                                from s14 in context.S14_CAR.Where(c => c.集計年月 == p年月 && c.車輌KEY == m05.車輌KEY)
                                                from s14sb in context.S14_CARSB.Where(c => c.集計年月 == p年月 && c.車輌KEY == m05.車輌KEY && c.経費項目ID == 401).DefaultIfEmpty()
                                                from zengetu in ZENGETU.Where(c => c.車輌Key == m05.車輌KEY).DefaultIfEmpty()
                                                from zennen in ZENNEN.Where(c => c.車輌Key == m05.車輌KEY).DefaultIfEmpty()
                                                where intCause.Contains(m05.車輌ID)
                                                select new SRY19010_Member
                                                {
                                                    車輌ID = m05.車輌KEY,
                                                    車輌番号 = m05.車輌番号,
                                                    乗務員名 = m04.乗務員名,
                                                    日数 = s14.稼動日数 == null ? 0 : s14.稼動日数,
                                                    運賃 = s14.運送収入 == null ? 0 : s14.運送収入,
                                                    壱日当り = s14.稼動日数 == null ? 0 : s14.稼動日数 == 0 ? 0 : Math.Round((decimal)((s14.運送収入 / s14.稼動日数)), 0),
                                                    輸送屯数 = s14.輸送屯数 == null ? 0 : s14.輸送屯数,
                                                    走行KM = s14.走行ＫＭ == null ? 0 : s14.走行ＫＭ,
                                                    実車KM = s14.実車ＫＭ == null ? 0 : s14.実車ＫＭ,
                                                    燃料L = s14.燃料Ｌ == null ? 0 : s14.燃料Ｌ,
                                                    燃費 = s14.燃料Ｌ == null ? 0 : s14.燃料Ｌ == 0 ? 0 : Math.Round((decimal)((s14.走行ＫＭ / s14.燃料Ｌ)), 2),
                                                    燃料代 = s14sb.金額 == null ? 0 : s14sb.金額,

                                                    前月燃費 = zengetu.燃費 == null ? 0 : zengetu.燃費,
                                                    前月対比 = zengetu.燃費 == null ? 0 : zengetu.燃費 == 0 ? 0 : s14.燃料Ｌ == 0 ? 0 : Math.Round((decimal)((s14.走行ＫＭ / s14.燃料Ｌ) / zengetu.燃費), 0),
                                                    前月燃費差益 = zengetu.燃費 == null ? 0 : zengetu.燃費 == 0 ? 0 : s14.燃料Ｌ == 0 ? 0 : Math.Round((decimal)((s14sb.金額 / s14.燃料Ｌ) * (s14.走行ＫＭ / zengetu.燃費)), 0),
                                                    前月走行KM = zengetu.走行KM == null ? 0 : zengetu.走行KM,
                                                    前月燃料L = zengetu.燃料L == null ? 0 : zengetu.燃料L,

                                                    前年燃費 = zennen.燃費 == null ? 0 : zennen.燃費,
                                                    前年対比 = zennen.燃費 == null ? 0 : zennen.燃費 == 0 ? 0 : s14.燃料Ｌ == 0 ? 0 : Math.Round((decimal)((s14.走行ＫＭ / s14.燃料Ｌ) / zennen.燃費), 0),
                                                    前年燃費差益 = zennen.燃費 == null ? 0 : zennen.燃費 == 0 ? 0 : s14.燃料Ｌ == 0 ? 0 : Math.Round((decimal)((s14sb.金額 / s14.燃料Ｌ) * (s14.走行ＫＭ / zennen.燃費)), 0),
                                                    前年走行KM = zennen.走行KM == null ? 0 : zennen.走行KM,
                                                    前年燃料L = zennen.燃料L == null ? 0 : zennen.燃料L,

                                                    コードFrom = s車輌From,
                                                    コードTo = s車輌To,
                                                    車輌ﾋﾟｯｸｱｯﾌﾟ = s車輌List,
                                                    作成年月 = s作成年月,
                                                    

                                                });
                        }
                    }
                    else
                    {
                        //車輌FromがNullだった場合
                        if (string.IsNullOrEmpty(s車輌From))
                        {
                            query = query.Where(c => c.車輌ID >= int.MinValue);
                        }
                        //車輌ToがNullだった場合
                        if (string.IsNullOrEmpty(s車輌To))
                        {
                            query = query.Where(c => c.車輌ID <= int.MaxValue);
                        }

                    }

                    //乗務員指定の表示
                    if (i車輌List.Length > 0)
                    {
                        for (int i = 0; i < query.Count(); i++)
                        {
                            車輌ﾋﾟｯｸｱｯﾌﾟ指定 = 車輌ﾋﾟｯｸｱｯﾌﾟ指定 + i車輌List[i].ToString();

                            if (i < i車輌List.Length)
                            {

                                if (i == i車輌List.Length - 1)
                                {
                                    break;
                                }

                                車輌ﾋﾟｯｸｱｯﾌﾟ指定 = 車輌ﾋﾟｯｸｱｯﾌﾟ指定 + ",";

                            }

                            if (i車輌List.Length == 1)
                            {
                                break;
                            }

                        }
                    }

                    //表示順序変更
                    switch (i表示順序)
                    {
                        case 0:
                            //車輌番号昇順
                            query = query.OrderBy(c => c.車輌ID);
                            break;
                        case 1:
                            query = query.OrderByDescending(c => c.燃料L);
                            break;
                        case 2:
                            //運送収入降順
                            query = query.OrderByDescending(c => c.燃費);
                            break;
                    }

                    query = query.Where(c => c.日数 != 0 || c.運賃 != 0 || c.壱日当り != 0 || c.輸送屯数 != 0 || c.走行KM != 0 || c.実車KM != 0 || c.燃料L != 0 || c.燃費 != 0 || c.燃料代 != 0);

                      retList = query.ToList();
                      return retList;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

    }
}