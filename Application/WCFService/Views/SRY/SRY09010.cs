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
using KyoeiSystem.Framework.Common;

namespace KyoeiSystem.Application.WCFService
{
    public class SRY09010g_Member
    {
        [DataMember]
        public int 車輌Key { get; set; }
        [DataMember]
        public decimal 合計 { get; set; }
    }


    #region SRY09010_Member

    public class SRY09010_Member
    {
        [DataMember]
        public int? 車輌ID { get; set; }
        [DataMember]
        public string 車輌番号 { get; set; }
        [DataMember]
        public int 日数 { get; set; }
        [DataMember]
        public decimal 拘束H { get; set; }
        [DataMember]
        public decimal 運転H { get; set; }
        [DataMember]
        public decimal 高速H { get; set; }
        [DataMember]
        public decimal 作業H { get; set; }
        [DataMember]
        public decimal 休憩H { get; set; }
        [DataMember]
        public decimal 残業H { get; set; }
        [DataMember]
        public decimal 深夜H { get; set; }
        [DataMember]
        public int 走行KM { get; set; }
        [DataMember]
        public int 実車KM { get; set; }
        [DataMember]
        public decimal 輸送屯数 { get; set; }
        [DataMember]
        public decimal 運送収入 { get; set; }
        [DataMember]
        public decimal 燃料L { get; set; }
        [DataMember]
        public decimal 経費合計 { get; set; }
        [DataMember]
        public string 集計年月From { get; set; }
        [DataMember]
        public string 集計年月To { get; set; }
        [DataMember]
        public string 表示順序 { get; set; }
        [DataMember]
        public string 車輌指定 { get; set; }
        [DataMember]
        public string 車輌ﾋﾟｯｸｱｯﾌﾟ { get; set; }
    }

    #endregion

    public class SRY09010
    {
        #region SRY09010 印刷

        public List<SRY09010_Member> SRY09010_GetDataHinList(string s車輌From , string s車輌To , int?[] i車輌List , string p集計期間From , string p集計期間To , int i表示順序)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                string s集計期間From, s集計期間To;
                s集計期間From = p集計期間From.ToString().Substring(0, 4) + "年" + p集計期間From.ToString().Substring(4, 2) + "月度";
                s集計期間To = p集計期間To.ToString().Substring(0, 4) + "年" + p集計期間To.ToString().Substring(4, 2) + "月度";
                int i集計期間From, i集計期間To;
                i集計期間From = Convert.ToInt32(p集計期間From);
                i集計期間To = Convert.ToInt32(p集計期間To);

                List<SRY09010_Member> retList = new List<SRY09010_Member>();
                context.Connection.Open();
                try
                {
                    string 車輌ﾋﾟｯｸｱｯﾌﾟ指定 = string.Empty;

                    var goukei = (from s14c in context.S14_CARSB
                                  where s14c.集計年月 >= i集計期間From && s14c.集計年月 <= i集計期間To
                                  group s14c by s14c.車輌KEY into Group
                                  select new SRY09010g_Member
                                  {
                                      車輌Key = Group.Key,
                                      合計 = Group.Sum(c => c.金額),
                                  }).AsQueryable();

                    var query = (from m05 in context.M05_CAR.Where(c => c.削除日付 == null)
                                 join s14 in context.S14_CAR.Where(c => c.集計年月 >= i集計期間From && c.集計年月 <= i集計期間To) on m05.車輌KEY equals s14.車輌KEY into Group
                                 from s14Group in Group
                                 join s14g in goukei on s14Group.車輌KEY equals s14g.車輌Key into sg14Group
                                 from sgg14Group in sg14Group
                                 group new { m05, s14Group ,sgg14Group } by new { m05.車輌ID, m05.車輌番号 ,sgg14Group.合計 } into grGroup
                                select new SRY09010_Member
                                {
                                    車輌ID = grGroup.Key.車輌ID,
                                    車輌番号 = grGroup.Key.車輌番号,
                                    日数 = grGroup.Sum(c => c.s14Group.稼動日数) == null ? 0 : grGroup.Sum(c => c.s14Group.稼動日数),
                                    拘束H = grGroup.Sum(c => c.s14Group.拘束時間) == null ? 0 : grGroup.Sum(c => c.s14Group.拘束時間),
                                    運転H = grGroup.Sum(c => c.s14Group.運転時間) == null ? 0 : grGroup.Sum(c => c.s14Group.運転時間),
                                    高速H = grGroup.Sum(c => c.s14Group.高速時間) == null ? 0 : grGroup.Sum(c => c.s14Group.高速時間),
                                    作業H = grGroup.Sum(c => c.s14Group.作業時間) == null ? 0 : grGroup.Sum(c => c.s14Group.作業時間),
                                    休憩H = grGroup.Sum(c => c.s14Group.休憩時間) == null ? 0 : grGroup.Sum(c => c.s14Group.休憩時間),
                                    残業H = grGroup.Sum(c => c.s14Group.残業時間) == null ? 0 : grGroup.Sum(c => c.s14Group.残業時間),
                                    深夜H = grGroup.Sum(c => c.s14Group.深夜時間) == null ? 0 : grGroup.Sum(c => c.s14Group.深夜時間),
                                    走行KM = grGroup.Sum(c => c.s14Group.走行ＫＭ) == null ? 0 : grGroup.Sum(c => c.s14Group.走行ＫＭ),
                                    実車KM = grGroup.Sum(c => c.s14Group.実車ＫＭ) == null ? 0 : grGroup.Sum(c => c.s14Group.実車ＫＭ),
                                    輸送屯数 = grGroup.Sum(c => c.s14Group.輸送屯数) == null ? 0 : grGroup.Sum(c => c.s14Group.輸送屯数),
                                    運送収入 = grGroup.Sum(c => c.s14Group.運送収入) == null ? 0 : grGroup.Sum(c => c.s14Group.運送収入),
                                    燃料L = grGroup.Sum(c => c.s14Group.燃料Ｌ) == null ? 0 : grGroup.Sum(c => c.s14Group.燃料Ｌ),
                                    経費合計 = grGroup.Key.合計,
                                    集計年月From = s集計期間From,
                                    集計年月To = s集計期間To,
                                    表示順序 = i表示順序 == 0 ? "ID順" : i表示順序 == 1 ? "車種順" : "運送収入順",
                                    車輌指定 = s車輌From + "～" + s車輌To,
                                    車輌ﾋﾟｯｸｱｯﾌﾟ = 車輌ﾋﾟｯｸｱｯﾌﾟ指定 == "" ? "無" : 車輌ﾋﾟｯｸｱｯﾌﾟ指定,
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
                            query = query.Union(from m05 in context.M05_CAR.Where(c => c.削除日付 == null)
                                                join s14 in context.S14_CAR.Where(c => c.集計年月 >= i集計期間From && c.集計年月 <= i集計期間To) on m05.車輌KEY equals s14.車輌KEY into Group
                                                from s14Group in Group
                                                join s14g in goukei on s14Group.車輌KEY equals s14g.車輌Key into sg14Group
                                                from sgg14Group in sg14Group
                                                group new { m05, s14Group, sgg14Group } by new { m05.車輌ID, m05.車輌番号, sgg14Group.合計 } into grGroup
                                                where intCause.Contains(grGroup.Key.車輌ID)
                                                select new SRY09010_Member
                                                {
                                                    車輌ID = grGroup.Key.車輌ID,
                                                    車輌番号 = grGroup.Key.車輌番号,
                                                    日数 = grGroup.Sum(c => c.s14Group.稼動日数) == null ? 0 : grGroup.Sum(c => c.s14Group.稼動日数),
                                                    拘束H = grGroup.Sum(c => c.s14Group.拘束時間) == null ? 0 : grGroup.Sum(c => c.s14Group.拘束時間),
                                                    運転H = grGroup.Sum(c => c.s14Group.運転時間) == null ? 0 : grGroup.Sum(c => c.s14Group.運転時間),
                                                    高速H = grGroup.Sum(c => c.s14Group.高速時間) == null ? 0 : grGroup.Sum(c => c.s14Group.高速時間),
                                                    作業H = grGroup.Sum(c => c.s14Group.作業時間) == null ? 0 : grGroup.Sum(c => c.s14Group.作業時間),
                                                    休憩H = grGroup.Sum(c => c.s14Group.休憩時間) == null ? 0 : grGroup.Sum(c => c.s14Group.休憩時間),
                                                    残業H = grGroup.Sum(c => c.s14Group.残業時間) == null ? 0 : grGroup.Sum(c => c.s14Group.残業時間),
                                                    深夜H = grGroup.Sum(c => c.s14Group.深夜時間) == null ? 0 : grGroup.Sum(c => c.s14Group.深夜時間),
                                                    走行KM = grGroup.Sum(c => c.s14Group.走行ＫＭ) == null ? 0 : grGroup.Sum(c => c.s14Group.走行ＫＭ),
                                                    実車KM = grGroup.Sum(c => c.s14Group.実車ＫＭ) == null ? 0 : grGroup.Sum(c => c.s14Group.実車ＫＭ),
                                                    輸送屯数 = grGroup.Sum(c => c.s14Group.輸送屯数) == null ? 0 : grGroup.Sum(c => c.s14Group.輸送屯数),
                                                    運送収入 = grGroup.Sum(c => c.s14Group.運送収入) == null ? 0 : grGroup.Sum(c => c.s14Group.運送収入),
                                                    燃料L = grGroup.Sum(c => c.s14Group.燃料Ｌ) == null ? 0 : grGroup.Sum(c => c.s14Group.燃料Ｌ),
                                                    経費合計 = grGroup.Key.合計,
                                                    集計年月From = s集計期間From,
                                                    集計年月To = s集計期間To,
                                                    表示順序 = i表示順序 == 0 ? "ID順" : i表示順序 == 1 ? "車種順" : "運送収入順",
                                                    車輌指定 = s車輌From + "～" + s車輌To,
                                                    車輌ﾋﾟｯｸｱｯﾌﾟ = 車輌ﾋﾟｯｸｱｯﾌﾟ指定 == "" ? "無" : 車輌ﾋﾟｯｸｱｯﾌﾟ指定,
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
                            query = query.OrderBy(c => c.車輌番号);
                            break;
                        case 2:
                            //運送収入降順
                            query = query.OrderByDescending(c => c.運送収入);
                            break;
                    }

                    query = query.Where(c => c.経費合計 != 0 || c.日数 != 0 || c.運送収入 != 0);

					//retList = query.ToList();
					foreach (var rec in query)
					{
						// 各時間項目の時分を、分に変換する。
						rec.拘束H = (decimal)LinqSub.時間TO分(rec.拘束H);
						rec.運転H = (decimal)LinqSub.時間TO分(rec.運転H);
						rec.高速H = (decimal)LinqSub.時間TO分(rec.高速H);
						rec.作業H = (decimal)LinqSub.時間TO分(rec.作業H);
						//rec.待機H = (decimal)LinqSub.時間TO分(rec.待機H);
						rec.休憩H = (decimal)LinqSub.時間TO分(rec.休憩H);
						rec.残業H = (decimal)LinqSub.時間TO分(rec.残業H);
						rec.深夜H = (decimal)LinqSub.時間TO分(rec.深夜H);

						retList.Add(rec);
					}


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