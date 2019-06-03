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
    public class SRY11010g_Member
    {
        [DataMember]
        public int 車輌KEY { get; set; }
        [DataMember]
        public int 車輌ID { get; set; }
        [DataMember]
        public int? 車種ID { get; set; }
        [DataMember]
        public string 車輌番号 { get; set; }
        [DataMember]
        public string 主乗務員 { get; set; }
        [DataMember]
        public int 台数 { get; set; }
        [DataMember]
        public string 車種 { get; set; }
        [DataMember]
        public decimal? 運送収入 { get; set; }
        [DataMember]
        public decimal? 限界利益 { get; set; }
        [DataMember]
        public decimal? 小計1 { get; set; }
        [DataMember]
        public decimal? 小計2 { get; set; }
        [DataMember]
        public decimal? 小計3 { get; set; }


        [DataMember]
        public decimal? 車輌直接費 { get; set; }
        [DataMember]
        public decimal? 車輌直接益 { get; set; }
        [DataMember]
        public decimal? 一般管理費 { get; set; }
        [DataMember]
        public decimal? 当月利益 { get; set; }
        [DataMember]
        public int? 稼働日数 { get; set; }
        [DataMember]
        public int? 実車KM { get; set; }
        [DataMember]
        public int? 空車KM { get; set; }
        [DataMember]
        public int? 走行KM { get; set; }
        [DataMember]
        public decimal 燃料L { get; set; }
        [DataMember]
        public decimal 燃費 { get; set; }
        [DataMember]
        public decimal 収入1KM { get; set; }
        [DataMember]
        public decimal 原価1KM { get; set; }
    }

    public class SRY11010_KEI_Member
    {
        [DataMember]
        public int 印刷グループID { get; set; }
        [DataMember]
        public int 経費ID { get; set; }
        [DataMember]
        public string 経費項目名 { get; set; }
        [DataMember]
        public int 台数 { get; set; }
        [DataMember]
        public decimal 金額 { get; set; }
    }

    public class SRY11010_PRELIST_Member
    {
        [DataMember]
        public int 印刷グループID { get; set; }
        [DataMember]
        public int 経費ID { get; set; }
        [DataMember]
        public string 経費項目名 { get; set; }
        [DataMember]
        public int 車輌KEY { get; set; }
        [DataMember]
        public int 車輌ID { get; set; }
        [DataMember]
        public int? 車種ID { get; set; }
        [DataMember]
        public int 台数 { get; set; }
        [DataMember]
        public decimal 金額 { get; set; }
    }

    public class SRY11010_Syoukei_Member
    {
        [DataMember]
        public int 車種ID { get; set; }
        [DataMember]
        public int 経費ID { get; set; }
        [DataMember]
        public int 印刷グループID { get; set; }
        [DataMember]
        public string 経費項目名 { get; set; }
        [DataMember]
        public int 台数 { get; set; }
        [DataMember]
        public decimal 金額 { get; set; }
    }

    #region SRY11010_Member

    public class SRY11010_Member
    {
        [DataMember]
        public int? 車種ID1 { get; set; }
        [DataMember]
        public string 車種1 { get; set; }
        [DataMember]
        public string 車種名1 { get; set; }
        [DataMember]
        public int 台数1 { get; set; }
        [DataMember]
        public decimal? 運送収入1 { get; set; }

        [DataMember]
        public int? 車種ID2 { get; set; }
        [DataMember]
        public string 車種2 { get; set; }
        [DataMember]
        public string 車種名2 { get; set; }
        [DataMember]
        public int 台数2 { get; set; }
        [DataMember]
        public decimal? 運送収入2 { get; set; }

        [DataMember]
        public int? 車種ID3 { get; set; }
        [DataMember]
        public string 車種3 { get; set; }
        [DataMember]
        public string 車種名3 { get; set; }
        [DataMember]
        public int 台数3 { get; set; }
        [DataMember]
        public decimal? 運送収入3 { get; set; }

        [DataMember]
        public int? 車種ID4 { get; set; }
        [DataMember]
        public string 車種4 { get; set; }
        [DataMember]
        public string 車種名4 { get; set; }
        [DataMember]
        public int 台数4 { get; set; }
        [DataMember]
        public decimal? 運送収入4 { get; set; }

        [DataMember]
        public int? 車種ID5 { get; set; }
        [DataMember]
        public string 車種5 { get; set; }
        [DataMember]
        public string 車種名5 { get; set; }
        [DataMember]
        public int 台数5 { get; set; }
        [DataMember]
        public decimal? 運送収入5 { get; set; }

        [DataMember]
        public int? 車種ID6 { get; set; }
        [DataMember]
        public string 車種6 { get; set; }
        [DataMember]
        public string 車種名6 { get; set; }
        [DataMember]
        public int 台数6 { get; set; }
        [DataMember]
        public decimal? 運送収入6 { get; set; }

        [DataMember]
        public decimal? 車輌直接費1 { get; set; }
        [DataMember]
        public decimal? 車輌直接益1 { get; set; }
        [DataMember]
        public decimal? 一般管理費1 { get; set; }
        [DataMember]
        public decimal? 当月利益1 { get; set; }
        [DataMember]
        public int? 稼働日数1 { get; set; }
        [DataMember]
        public int? 実車KM1 { get; set; }
        [DataMember]
        public int? 空車KM1 { get; set; }
        [DataMember]
        public int? 走行KM1 { get; set; }
        [DataMember]
        public decimal? 燃料L1 { get; set; }
        [DataMember]
        public decimal? 燃費1 { get; set; }
        [DataMember]
        public decimal? 収入1KM1 { get; set; }
        [DataMember]
        public decimal? 原価1KM1 { get; set; }

        [DataMember]
        public decimal? 車輌直接費2 { get; set; }
        [DataMember]
        public decimal? 車輌直接益2 { get; set; }
        [DataMember]
        public decimal? 一般管理費2 { get; set; }
        [DataMember]
        public decimal? 当月利益2 { get; set; }
        [DataMember]
        public int? 稼働日数2 { get; set; }
        [DataMember]
        public int? 実車KM2 { get; set; }
        [DataMember]
        public int? 空車KM2 { get; set; }
        [DataMember]
        public int? 走行KM2 { get; set; }
        [DataMember]
        public decimal? 燃料L2 { get; set; }
        [DataMember]
        public decimal? 燃費2 { get; set; }
        [DataMember]
        public decimal? 収入1KM2 { get; set; }
        [DataMember]
        public decimal? 原価1KM2 { get; set; }

        [DataMember]
        public decimal? 車輌直接費3 { get; set; }
        [DataMember]
        public decimal? 車輌直接益3 { get; set; }
        [DataMember]
        public decimal? 一般管理費3 { get; set; }
        [DataMember]
        public decimal? 当月利益3 { get; set; }
        [DataMember]
        public int? 稼働日数3 { get; set; }
        [DataMember]
        public int? 実車KM3 { get; set; }
        [DataMember]
        public int? 空車KM3 { get; set; }
        [DataMember]
        public int? 走行KM3 { get; set; }
        [DataMember]
        public decimal? 燃料L3 { get; set; }
        [DataMember]
        public decimal? 燃費3 { get; set; }
        [DataMember]
        public decimal? 収入1KM3 { get; set; }
        [DataMember]
        public decimal? 原価1KM3 { get; set; }

        [DataMember]
        public decimal? 車輌直接費4 { get; set; }
        [DataMember]
        public decimal? 車輌直接益4 { get; set; }
        [DataMember]
        public decimal? 一般管理費4 { get; set; }
        [DataMember]
        public decimal? 当月利益4 { get; set; }
        [DataMember]
        public int? 稼働日数4 { get; set; }
        [DataMember]
        public int? 実車KM4 { get; set; }
        [DataMember]
        public int? 空車KM4 { get; set; }
        [DataMember]
        public int? 走行KM4 { get; set; }
        [DataMember]
        public decimal? 燃料L4 { get; set; }
        [DataMember]
        public decimal? 燃費4 { get; set; }
        [DataMember]
        public decimal? 収入1KM4 { get; set; }
        [DataMember]
        public decimal? 原価1KM4 { get; set; }

        [DataMember]
        public decimal? 車輌直接費5 { get; set; }
        [DataMember]
        public decimal? 車輌直接益5 { get; set; }
        [DataMember]
        public decimal? 一般管理費5 { get; set; }
        [DataMember]
        public decimal? 当月利益5 { get; set; }
        [DataMember]
        public int? 稼働日数5 { get; set; }
        [DataMember]
        public int? 実車KM5 { get; set; }
        [DataMember]
        public int? 空車KM5 { get; set; }
        [DataMember]
        public int? 走行KM5 { get; set; }
        [DataMember]
        public decimal? 燃料L5 { get; set; }
        [DataMember]
        public decimal? 燃費5 { get; set; }
        [DataMember]
        public decimal? 収入1KM5 { get; set; }
        [DataMember]
        public decimal? 原価1KM5 { get; set; }

        [DataMember]
        public decimal? 車輌直接費6 { get; set; }
        [DataMember]
        public decimal? 車輌直接益6 { get; set; }
        [DataMember]
        public decimal? 一般管理費6 { get; set; }
        [DataMember]
        public decimal? 当月利益6 { get; set; }
        [DataMember]
        public int? 稼働日数6 { get; set; }
        [DataMember]
        public int? 実車KM6 { get; set; }
        [DataMember]
        public int? 空車KM6 { get; set; }
        [DataMember]
        public int? 走行KM6 { get; set; }
        [DataMember]
        public decimal? 燃料L6 { get; set; }
        [DataMember]
        public decimal? 燃費6 { get; set; }
        [DataMember]
        public decimal? 収入1KM6 { get; set; }
        [DataMember]
        public decimal? 原価1KM6 { get; set; }


        [DataMember]
        public int 印刷グループID { get; set; }

        [DataMember]
        public int 経費ID { get; set; }
        [DataMember]
        public string 経費項目名 { get; set; }

        [DataMember]
        public decimal? 金額1 { get; set; }
        [DataMember]
        public decimal? 金額2 { get; set; }
        [DataMember]
        public decimal? 金額3 { get; set; }
        [DataMember]
        public decimal? 金額4 { get; set; }
        [DataMember]
        public decimal? 金額5 { get; set; }
        [DataMember]
        public decimal? 金額6 { get; set; }

        [DataMember]
        public string 年 { get; set; }
        [DataMember]
        public string 月 { get; set; }
        [DataMember]
        public string 表示順序 { get; set; }
        [DataMember]
        public string コードFrom { get; set; }
        [DataMember]
        public string コードTo { get; set; }
        [DataMember]
        public string 車種ﾋﾟｯｸｱｯﾌﾟ { get; set; }
    }

    #endregion

    public class SRY11010
    {
        #region SRY11010 印刷
        public List<SRY11010_Member> SRY11010_GetDataList(string s車種From, string s車種To, int?[] i車種List, string s車種List, int i年月, int 年, int 月, DateTime? 終了年月日)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {

                List<SRY11010_Member> retList = new List<SRY11010_Member>();

                context.Connection.Open();
                try
                {
                    string 車輌ﾋﾟｯｸｱｯﾌﾟ指定 = string.Empty;

                    var carlistz = (from m05 in context.M05_CAR.Where(c => c.削除日付 == null)
                                    join s14 in context.S14_CAR.Where(c => c.集計年月 == i年月) on m05.車輌KEY equals s14.車輌KEY
                                    join m04 in context.M04_DRV.Where(c => c.削除日付 == null) on m05.乗務員KEY equals m04.乗務員KEY into m04Group
                                    from m04g in m04Group.DefaultIfEmpty()
                                    join m06 in context.M06_SYA.Where(c => c.削除日付 == null) on m05.車種ID equals m06.車種ID into m06Group
                                    from m06g in m06Group
                                    orderby m05.車種ID
                                    where m05.車種ID != 0
                                    select new SRY11010g_Member
                                    {
                                        車輌KEY = m05.車輌KEY,
                                        車輌ID = m05.車輌ID,
                                        車種ID = m05.車種ID,
                                        車輌番号 = m05.車輌番号,
                                        主乗務員 = m04g.乗務員名,
                                        車種 = m06g.車種名,
                                        運送収入 = s14.運送収入,

                                        一般管理費 = s14.一般管理費,

                                        稼働日数 = s14.稼動日数,
                                        実車KM = s14.実車ＫＭ,
                                        空車KM = s14.走行ＫＭ - s14.実車ＫＭ,
                                        走行KM = s14.走行ＫＭ,
                                        燃料L = s14.燃料Ｌ == null ? 0 : s14.燃料Ｌ,
                                        燃費 = s14.燃料Ｌ == 0 ? 0 : s14.燃料Ｌ == null ? 0 : s14.走行ＫＭ / s14.燃料Ｌ,
                                        収入1KM = s14.走行ＫＭ == 0 ? 0 : s14.走行ＫＭ == null ? 0 : s14.運送収入 / s14.走行ＫＭ,
                                    }).AsQueryable();

                    if (!(string.IsNullOrEmpty(s車種From + s車種To) && i車種List.Length == 0))
                    {
                        if (string.IsNullOrEmpty(s車種From + s車種To))
                        {
                            carlistz = carlistz.Where(c => c.車種ID >= int.MaxValue);
                        }

                        //車種From処理　Min値
                        if (!string.IsNullOrEmpty(s車種From))
                        {
                            int i車種From = AppCommon.IntParse(s車種From);
                            carlistz = carlistz.Where(c => c.車種ID >= i車種From);
                        }

                        //車種To処理　Max値
                        if (!string.IsNullOrEmpty(s車種To))
                        {
                            int i車種TO = AppCommon.IntParse(s車種To);
                            carlistz = carlistz.Where(c => c.車種ID <= i車種TO);
                        }

                        if (i車種List.Length > 0)
                        {
                            var intCause = i車種List;
                            carlistz = carlistz.Union(from m05 in context.M05_CAR.Where(c => c.削除日付 == null)
                                                      join s14 in context.S14_CAR.Where(c => c.集計年月 == i年月) on m05.車輌KEY equals s14.車輌KEY
                                                      join m04 in context.M04_DRV.Where(c => c.削除日付 == null) on m05.乗務員KEY equals m04.乗務員KEY into m04Group
                                                      from m04g in m04Group.DefaultIfEmpty()
                                                      join m06 in context.M06_SYA.Where(c => c.削除日付 == null) on m05.車種ID equals m06.車種ID into m06Group
                                                      from m06g in m06Group
                                                      orderby m05.車種ID
                                                      where intCause.Contains(m05.車種ID)
                                                      select new SRY11010g_Member
                                                      {
                                                          車輌KEY = m05.車輌KEY,
                                                          車輌ID = m05.車輌ID,
                                                          車種ID = m05.車種ID,
                                                          車輌番号 = m05.車輌番号,
                                                          主乗務員 = m04g.乗務員名,
                                                          車種 = m06g.車種名,
                                                          運送収入 = s14.運送収入,

                                                          一般管理費 = s14.一般管理費,

                                                          稼働日数 = s14.稼動日数,
                                                          実車KM = s14.実車ＫＭ,
                                                          空車KM = s14.走行ＫＭ - s14.実車ＫＭ,
                                                          走行KM = s14.走行ＫＭ,
                                                          燃料L = s14.燃料Ｌ,
                                                          燃費 = s14.燃料Ｌ == 0 ? 0 : s14.燃料Ｌ == null ? 0 : s14.走行ＫＭ / s14.燃料Ｌ,
                                                          収入1KM = s14.走行ＫＭ == 0 ? 0 : s14.走行ＫＭ == null ? 0 : s14.運送収入 / s14.走行ＫＭ,
                                                      });
                        };
                    }
                    else
                    {
                        //車種From処理　Min値
                        if (!string.IsNullOrEmpty(s車種From))
                        {
                            int i車種From = AppCommon.IntParse(s車種From);
                            carlistz = carlistz.Where(c => c.車種ID >= i車種From);
                        }

                        //車種To処理　Max値
                        if (!string.IsNullOrEmpty(s車種To))
                        {
                            int i車種TO = AppCommon.IntParse(s車種To);
                            carlistz = carlistz.Where(c => c.車種ID <= i車種TO);
                        }
                    }

                    carlistz.Distinct();
                    var carlistzz = carlistz.ToList();




                    //運行費項目取得
                    var keilist = (from m07 in context.M07_KEI
                                   where m07.編集区分 == 0 && m07.固定変動区分 == 1 && m07.経費区分 != 3
                                   orderby m07.経費項目ID
                                   select new JMI12010_KEI_Member
                                   {
                                       印刷グループID = 1,
                                       経費ID = m07.経費項目ID,
                                       経費項目名 = m07.経費項目名,
                                   }).AsQueryable();

                    //運行費小計
                    bool bl = (from m07 in context.M07_KEI select m07).Any(c => c.編集区分 == 0 && c.固定変動区分 == 1 && c.経費区分 != 3);

                    if (bl)
                    {
                        //var a = (from m07 in context.M07_KEI
                        //         select new JMI12010_KEI_Member
                        //         {
                        //             印刷グループID = 1,
                        //             経費ID = 9999999,
                        //             経費項目名 = "【 小 計 】",
                        //         }).AsQueryable();
                        //a = a.Take(1);
                        //keilist = keilist.Union(a.AsQueryable());

                        //限界利益
                        var a = (from m07 in context.M07_KEI
                             select new JMI12010_KEI_Member
                             {
                                 印刷グループID = 2,
                                 経費ID = 9999999,
                                 経費項目名 = "限界利益",
                             }).AsQueryable();
                        a = a.Take(1);
                        keilist = keilist.Union(a.AsQueryable());

                    }

                    //乗務員経費
                    keilist = keilist.Union(from m07 in context.M07_KEI
                                            where m07.編集区分 == 0 && m07.経費区分 == 3
                                            orderby m07.経費項目ID
                                            select new JMI12010_KEI_Member
                                            {
                                                印刷グループID = 3,
                                                経費ID = m07.経費項目ID,
                                                経費項目名 = m07.経費項目名,
                                            }).AsQueryable();

                    //乗務員経費小計
                    //bl = (from m07 in context.M07_KEI select m07).Any(c => c.編集区分 == 0 && c.経費区分 == 3);

                    //if (bl)
                    //{
                    //    var a = (from m07 in context.M07_KEI
                    //             select new JMI12010_KEI_Member
                    //             {
                    //                 印刷グループID = 3,
                    //                 経費ID = 9999999,
                    //                 経費項目名 = "【 小 計 】",
                    //             }).AsQueryable();
                    //    a = a.Take(1);
                    //    keilist = keilist.Union(a.AsQueryable());

                    //}

                    //その他経費
                    keilist = keilist.Union(from m07 in context.M07_KEI
                                            where m07.編集区分 == 0 && m07.固定変動区分 == 0 && m07.経費区分 != 3
                                            orderby m07.経費項目ID
                                            select new JMI12010_KEI_Member
                                            {
                                                印刷グループID = 4,
                                                経費ID = m07.経費項目ID,
                                                経費項目名 = m07.経費項目名,
                                            }).AsQueryable();

                    keilist = keilist.Distinct();

                    //その他経費小計
                    //bl = (from m07 in context.M07_KEI select m07).Any(c => c.編集区分 == 0 && c.固定変動区分 == 0 && c.経費区分 != 3);

                    //if (bl)
                    //{
                    //    var a = (from m07 in context.M07_KEI
                    //             select new JMI12010_KEI_Member
                    //             {
                    //                 印刷グループID = 4,
                    //                 経費ID = 9999999,
                    //                 経費項目名 = "【 小 計 】",
                    //             }).AsQueryable();
                    //    a = a.Take(1);
                    //    keilist = keilist.Union(a.AsQueryable());

                    //}

                    keilist = keilist.OrderBy(c => c.印刷グループID).ThenBy(c => c.経費ID);





                    //経費取得
                    var prelistz = (from car in carlistz
                                    from kei in keilist
                                    orderby car.車輌ID, kei.印刷グループID, kei.経費ID
                                    select new SRY11010_PRELIST_Member
                                    {
                                        印刷グループID = kei.印刷グループID,
                                        経費ID = kei.経費ID,
                                        経費項目名 = kei.経費項目名,
                                        車輌ID = car.車輌ID,
                                        車輌KEY = car.車輌KEY,
                                        車種ID = car.車種ID,
                                        //金額 = s14sgroup.Where(c => c.経費項目ID == kei.経費ID).Select(c => c.金額).FirstOrDefault() == null ? 0 : s14sgroup.Where(c => c.経費項目ID == kei.経費ID).Select(c => c.金額).FirstOrDefault(),
                                    }).ToList();

                    var prelist = (from pre in prelistz
                                   join s14 in context.S14_CARSB.Where(c => c.集計年月 == i年月) on new { a = pre.車輌KEY, b = pre.経費ID } equals new { a = s14.車輌KEY, b = s14.経費項目ID } into s14Group
                                   //from s14g in s14Group.DefaultIfEmpty()
                                   select new SRY11010_PRELIST_Member
                                   {
                                       印刷グループID = pre.印刷グループID,
                                       経費ID = pre.経費ID,
                                       経費項目名 = pre.経費項目名,
                                       車輌ID = pre.車輌ID,
                                       車輌KEY = pre.車輌KEY,
                                       車種ID = pre.車種ID,
                                       金額 = s14Group.Select(c => c.金額).Sum(),

                                   }).AsQueryable();

                    prelist = prelist.Union(from pre in prelist.Where(c => c.印刷グループID != 2)
                                            group pre by new { pre.車輌KEY, pre.車輌ID, pre.車種ID, pre.印刷グループID } into preGroup
                                            select new SRY11010_PRELIST_Member
                                            {
                                                印刷グループID = preGroup.Key.印刷グループID,
                                                経費ID = 9999999,
                                                経費項目名 = "【 小 計 】",
                                                車輌ID = preGroup.Key.車輌ID,
                                                車輌KEY = preGroup.Key.車輌KEY,
                                                車種ID = preGroup.Key.車種ID,
                                                金額 = preGroup.Select(c => c.金額).Sum(),

                                            }).AsQueryable();

                    //合計追加
                    if (prelist.Any())
                    {
                        prelist = prelist.Union(from pre in prelist
                                                group pre by new { pre.印刷グループID, pre.経費ID, pre.経費項目名 } into preGroup
                                                select new SRY11010_PRELIST_Member
                                                {
                                                    印刷グループID = preGroup.Key.印刷グループID,
                                                    経費ID = preGroup.Key.経費ID,
                                                    経費項目名 = preGroup.Key.経費項目名,
                                                    車輌ID = 999999,
                                                    車輌KEY = 999999,
                                                    車種ID = 9999,
                                                    金額 = preGroup.Select(c => c.金額).Sum(),

                                                }).AsQueryable();
                    }

                    prelist = prelist.Distinct();
                    prelist = prelist.OrderBy(c => c.車輌ID).ThenBy(c => c.印刷グループID);

                    var carlist = (from car in carlistzz
                                   join pre in prelist.Where(c => c.印刷グループID != 2) on car.車輌KEY equals pre.車輌KEY into preGroup
                                   select new SRY11010g_Member
                                   {
                                       車輌KEY = car.車輌KEY,
                                       車輌ID = car.車輌ID,
                                       車種ID = car.車種ID,
                                       車輌番号 = car.車輌番号,
                                       主乗務員 = car.主乗務員,
                                       車種 = car.車種,
                                       運送収入 = car.運送収入,

                                       一般管理費 = car.一般管理費,

                                       稼働日数 = car.稼働日数,
                                       実車KM = car.実車KM,
                                       空車KM = car.走行KM - car.実車KM,
                                       走行KM = car.走行KM,
                                       燃料L = car.燃料L,
                                       燃費 = car.燃料L == 0 ? 0 : (decimal)car.走行KM / car.燃料L,
                                       収入1KM = car.収入1KM,
                                       限界利益 = car.運送収入 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額).Sum(),
                                       小計1 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額).Sum() == null ? 0 : preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額).Sum(),
                                       小計2 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額).Sum() == null ? 0 : preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額).Sum(),
                                       小計3 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額).Sum() == null ? 0 : preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額).Sum(),

                                   }).AsQueryable();

                    carlist = (from car in carlist
                               group car by new {car.車種ID, car.車種} into carGroup
                               select new SRY11010g_Member
                               {
                                       車種ID = carGroup.Key.車種ID,
                                       台数 = carGroup.Count(),
                                       車種 = carGroup.Key.車種,
                                       運送収入 = carGroup.Select(c => c.運送収入).Sum(),

                                       一般管理費 = carGroup.Select(c => c.一般管理費).Sum(),

                                       稼働日数 = carGroup.Select(c => c.稼働日数).Sum(),
                                       実車KM = carGroup.Select(c => c.実車KM).Sum(),
                                       空車KM = carGroup.Select(c => c.走行KM).Sum() - carGroup.Select(c => c.実車KM).Sum(),
                                       走行KM = carGroup.Select(c => c.走行KM).Sum(),
                                       燃料L = carGroup.Select(c => c.燃料L).Sum(),
                                       燃費 = carGroup.Select(c => c.燃料L).Sum() == 0 ? 0 : (decimal)(carGroup.Select(c => c.走行KM).Sum() / carGroup.Select(c => c.燃料L).Sum()),
                                       収入1KM = carGroup.Select(c => c.走行KM).Sum() == null ? 0 : carGroup.Select(c => c.走行KM).Sum() == 0 ? 0 : (decimal)(carGroup.Select(c => c.運送収入).Sum() / carGroup.Select(c => c.走行KM).Sum()),
                                       限界利益 = carGroup.Select(c => c.限界利益).Sum(),
                                       小計1 = carGroup.Select(c => c.小計1).Sum(),
                                       小計2 = carGroup.Select(c => c.小計2).Sum(),
                                       小計3 = carGroup.Select(c => c.小計3).Sum(),

                               }).AsQueryable();

                    var carlistzzz = carlist.ToList();
                    carlistzzz.Add(new SRY11010g_Member
                    {
                        車種ID = 9999,
                        台数 = carlist.Sum(c => c.台数),
                        車種 = "【 合 計 】",
                        運送収入 = carlist.Sum(c => c.運送収入),

                        一般管理費 = carlist.Sum(c => c.一般管理費),

                        稼働日数 = carlist.Sum(c => c.稼働日数),
                        実車KM = carlist.Sum(c => c.実車KM),
                        空車KM = carlist.Sum(c => c.走行KM) - carlist.Sum(c => c.実車KM),
                        走行KM = carlist.Sum(c => c.走行KM),
                        燃料L = carlist.Sum(c => c.燃料L),
                        燃費 = carlist.Sum(c => c.燃料L) == 0 ? 0 : (decimal)carlist.Sum(c => c.走行KM) / carlist.Sum(c => c.燃料L),
                        収入1KM = carlist.Sum(c => c.収入1KM),
                        限界利益 = carlist.Sum(c => c.限界利益),
                        小計1 = carlist.Sum(c => c.小計1),
                        小計2 = carlist.Sum(c => c.小計2),
                        小計3 = carlist.Sum(c => c.小計3),
                    });

                    ////車種From処理　Min値
                    //if (!string.IsNullOrEmpty(s車種From))
                    //{
                    //    int i車種From = AppCommon.IntParse(s車種From);
                    //    carlistz = carlistz.Where(c => c.車種ID >= i車種From);
                    //}

                    ////車種To処理　Max値
                    //if (!string.IsNullOrEmpty(s車種To))
                    //{
                    //    int i車種TO = AppCommon.IntParse(s車種To);
                    //    carlistz = carlistz.Where(c => c.車種ID <= i車種TO);
                    //}

                    //if (i車種List.Length > 0)
                    //{
                    //    var intCause = i車種List;



                    prelist = (from pre in prelist
                               group pre by new { pre.車種ID, pre.印刷グループID, pre.経費ID, pre.経費項目名 } into preGroup
                               select new SRY11010_PRELIST_Member
                               {
                                   印刷グループID = preGroup.Key.印刷グループID,
                                   経費ID = preGroup.Key.経費ID,
                                   経費項目名 = preGroup.Key.経費項目名,
                                   車種ID = preGroup.Key.車種ID,
                                   台数 = preGroup.Count(),
                                   金額 = preGroup.Select(c => c.金額).Sum(),

                               }).AsQueryable();
                    prelist = prelist.Distinct();
                    prelist = prelist.OrderBy(c => c.車種ID).ThenBy(c => c.印刷グループID).ThenBy(c => c.経費ID);

                    //var prelistz = (from p in prelist
                    //               join s14s in context.S14_CARSB.Where(c => c.集計年月 == i年月) on new { a = p.経費ID, b = p.車輌KEY } equals new { a = s14s.経費項目ID, b = s14s.車輌KEY } into s14sgroup
                    //               from s14sg in s14sgroup.DefaultIfEmpty()
                    //               orderby p.車輌ID, p.印刷グループID, p.経費ID
                    //               select new SRY11010_PRELIST_Member
                    //               {
                    //                   印刷グループID = p.印刷グループID,
                    //                   経費ID = p.経費ID,
                    //                   経費項目名 = p.経費項目名,
                    //                   車輌ID = p.車輌ID,
                    //                   車輌KEY = p.車輌KEY,
                    //                   金額 = s14sg.金額 == null ? 0 : s14sg.金額,
                    //               }).AsQueryable();


                    var listprelist = prelist.ToList();


                    var prt_GROPID1 = listprelist.Where(c => c.印刷グループID == 1).Select(c => c.経費ID);
                    var prt_GROPID3 = listprelist.Where(c => c.印刷グループID == 3).Select(c => c.経費ID);
                    var prt_GROPID4 = listprelist.Where(c => c.印刷グループID == 4).Select(c => c.経費ID);
                    prt_GROPID1 = prt_GROPID1.Distinct();
                    prt_GROPID3 = prt_GROPID3.Distinct();
                    prt_GROPID4 = prt_GROPID4.Distinct();

                    var prt_GROPID = listprelist.Select(c => c.経費ID);
                    prt_GROPID = prt_GROPID.Distinct();

                    //印刷用データ作成
                    int cnt = 0;
                    int[] carid = new int[6];
                    int? carid1 = 0, carid2 = 0, carid3 = 0, carid4 = 0, carid5 = 0, carid6 = 0;
                    foreach (SRY11010g_Member a in carlistzzz)
                    {
                        switch (cnt)
                        {
                            case 0:
                                carid1 = a.車種ID;
                                break;
                            case 1:
                                carid2 = a.車種ID;
                                break;
                            case 2:
                                carid3 = a.車種ID;
                                break;
                            case 3:
                                carid4 = a.車種ID;
                                break;
                            case 4:
                                carid5 = a.車種ID;
                                break;
                            case 5:
                                carid6 = a.車種ID;
                                break;
                        };


                        cnt += 1;

                        if (cnt >= 6)
                        {
                            List<SRY11010_PRELIST_Member> plist = (from p in prelist
                                                                   where p.車種ID == carid1
                                                                   select p).ToList();

                            SRY11010g_Member a1 = (from p in carlistzzz where p.車種ID == carid1 select p).FirstOrDefault();
                            SRY11010g_Member a2 = (from p in carlistzzz where p.車種ID == carid2 select p).FirstOrDefault();
                            SRY11010g_Member a3 = (from p in carlistzzz where p.車種ID == carid3 select p).FirstOrDefault();
                            SRY11010g_Member a4 = (from p in carlistzzz where p.車種ID == carid4 select p).FirstOrDefault();
                            SRY11010g_Member a5 = (from p in carlistzzz where p.車種ID == carid5 select p).FirstOrDefault();
                            SRY11010g_Member a6 = (from p in carlistzzz where p.車種ID == carid6 select p).FirstOrDefault();


                            decimal syaryoutyokusetuhi1 = 0;
                            decimal syaryoutyokusetuhi2 = 0;
                            decimal syaryoutyokusetuhi3 = 0;
                            decimal syaryoutyokusetuhi4 = 0;
                            decimal syaryoutyokusetuhi5 = 0;
                            decimal syaryoutyokusetuhi6 = 0;

                            syaryoutyokusetuhi1 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.車種ID == carid1).Sum(c => c.金額);
                            syaryoutyokusetuhi2 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.車種ID == carid2).Sum(c => c.金額);
                            syaryoutyokusetuhi3 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.車種ID == carid3).Sum(c => c.金額);
                            syaryoutyokusetuhi4 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.車種ID == carid4).Sum(c => c.金額);
                            syaryoutyokusetuhi5 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.車種ID == carid5).Sum(c => c.金額);
                            syaryoutyokusetuhi6 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.車種ID == carid6).Sum(c => c.金額);

                            //if ((from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                            //     join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                            //     from m05g in m05GROUP.DefaultIfEmpty()
                            //     join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                            //     from s14sg in s14sGroup.DefaultIfEmpty()
                            //     where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid1
                            //     select s14sg.金額).Any())
                            //{
                            //    syaryoutyokusetuhi1 = (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                            //                           join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                            //                           from m05g in m05GROUP.DefaultIfEmpty()
                            //                           join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                            //                           from s14sg in s14sGroup.DefaultIfEmpty()
                            //                           where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid1
                            //                           select s14sg.金額).Sum();
                            //}

                            //if ((from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                            //     join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                            //     from m05g in m05GROUP.DefaultIfEmpty()
                            //     join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                            //     from s14sg in s14sGroup.DefaultIfEmpty()
                            //     where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid2
                            //     select s14sg.金額).Any())
                            //{
                            //    syaryoutyokusetuhi2 = (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                            //                           join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                            //                           from m05g in m05GROUP.DefaultIfEmpty()
                            //                           join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                            //                           from s14sg in s14sGroup.DefaultIfEmpty()
                            //                           where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid2
                            //                           select s14sg.金額).Sum();
                            //}

                            //if ((from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                            //     join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                            //     from m05g in m05GROUP.DefaultIfEmpty()
                            //     join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                            //     from s14sg in s14sGroup.DefaultIfEmpty()
                            //     where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid3
                            //     select s14sg.金額).Any())
                            //{
                            //    syaryoutyokusetuhi3 = (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                            //                           join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                            //                           from m05g in m05GROUP.DefaultIfEmpty()
                            //                           join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                            //                           from s14sg in s14sGroup.DefaultIfEmpty()
                            //                           where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid3
                            //                           select s14sg.金額).Sum();
                            //}

                            //if ((from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                            //     join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                            //     from m05g in m05GROUP.DefaultIfEmpty()
                            //     join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                            //     from s14sg in s14sGroup.DefaultIfEmpty()
                            //     where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid4
                            //     select s14sg.金額).Any())
                            //{
                            //    syaryoutyokusetuhi4 = (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                            //                           join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                            //                           from m05g in m05GROUP.DefaultIfEmpty()
                            //                           join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                            //                           from s14sg in s14sGroup.DefaultIfEmpty()
                            //                           where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid4
                            //                           select s14sg.金額).Sum();
                            //}

                            //if ((from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                            //     join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                            //     from m05g in m05GROUP.DefaultIfEmpty()
                            //     join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                            //     from s14sg in s14sGroup.DefaultIfEmpty()
                            //     where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid4
                            //     select s14sg.金額).Any())
                            //{
                            //    syaryoutyokusetuhi5 = (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                            //                           join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                            //                           from m05g in m05GROUP.DefaultIfEmpty()
                            //                           join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                            //                           from s14sg in s14sGroup.DefaultIfEmpty()
                            //                           where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid5
                            //                           select s14sg.金額).Sum();
                            //}

                            //if ((from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                            //     join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                            //     from m05g in m05GROUP.DefaultIfEmpty()
                            //     join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                            //     from s14sg in s14sGroup.DefaultIfEmpty()
                            //     where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid6
                            //     select s14sg.金額).Any())
                            //{
                            //    syaryoutyokusetuhi6 = (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                            //                           join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                            //                           from m05g in m05GROUP.DefaultIfEmpty()
                            //                           join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                            //                           from s14sg in s14sGroup.DefaultIfEmpty()
                            //                           where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid6
                            //                           select s14sg.金額).Sum();
                            //}


                            foreach (var row in plist)
                            {

                                SRY11010_Member list = new SRY11010_Member()
                                {
                                    一般管理費1 = a1 == null ? null : a1.一般管理費,
                                    一般管理費2 = a2 == null ? null : a2.一般管理費,
                                    一般管理費3 = a3 == null ? null : a3.一般管理費,
                                    一般管理費4 = a4 == null ? null : a4.一般管理費,
                                    一般管理費5 = a5 == null ? null : a5.一般管理費,
                                    一般管理費6 = a6 == null ? null : a6.一般管理費,

                                    印刷グループID = row.印刷グループID,

                                    運送収入1 = a1 == null ? null : a1.運送収入,
                                    運送収入2 = a2 == null ? null : a2.運送収入,
                                    運送収入3 = a3 == null ? null : a3.運送収入,
                                    運送収入4 = a4 == null ? null : a4.運送収入,
                                    運送収入5 = a5 == null ? null : a5.運送収入,
                                    運送収入6 = a6 == null ? null : a6.運送収入,

                                    稼働日数1 = a1 == null ? null : a1.稼働日数,
                                    稼働日数2 = a2 == null ? null : a2.稼働日数,
                                    稼働日数3 = a3 == null ? null : a3.稼働日数,
                                    稼働日数4 = a4 == null ? null : a4.稼働日数,
                                    稼働日数5 = a5 == null ? null : a5.稼働日数,
                                    稼働日数6 = a6 == null ? null : a6.稼働日数,


                                    金額1 = row.経費ID == 9999999 ?
                                                row.印刷グループID == 2 ?
                                                a1 == null ? null : a1.限界利益 :
                                                prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid1).Select(c => c.金額).Sum() : 
                                                carid1 == 9999 ?
                                                prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid1).Select(c => c.金額).Sum() :
                                                (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                                 join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                                 from m05g in m05GROUP.DefaultIfEmpty()
                                                 join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                                 from s14sg in s14sGroup.DefaultIfEmpty()
                                                 where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid1
                                                 select s14sg.金額).Any() == true
                                                 ?
                                                (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                                 join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                                 from m05g in m05GROUP.DefaultIfEmpty()
                                                 join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                                 from s14sg in s14sGroup.DefaultIfEmpty()
                                                 where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid1
                                                 select s14sg.金額).Sum()
                                                 : 0,
                                    金額2 = row.経費ID == 9999999 ?
                                                row.印刷グループID == 2 ?
                                                a2 == null ? null : a2.限界利益 :
                                                prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid2).Select(c => c.金額).Sum() :
                                                carid2 == 9999 ?
                                                prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid2).Select(c => c.金額).Sum() :
                                                (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                                 join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                                 from m05g in m05GROUP.DefaultIfEmpty()
                                                 join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                                 from s14sg in s14sGroup.DefaultIfEmpty()
                                                 where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid2
                                                 select s14sg.金額).Any() == true
                                                 ?
                                                (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                                 join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                                 from m05g in m05GROUP.DefaultIfEmpty()
                                                 join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                                 from s14sg in s14sGroup.DefaultIfEmpty()
                                                 where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid2
                                                 select s14sg.金額).Sum()
                                                 : 0,
                                    金額3 = row.経費ID == 9999999 ?
                                                row.印刷グループID == 2 ?
                                                a3 == null ? null : a3.限界利益 :
                                                prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid3).Select(c => c.金額).Sum() :
                                                carid3 == 9999 ?
                                                prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid3).Select(c => c.金額).Sum() :
                                                (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                                 join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                                 from m05g in m05GROUP.DefaultIfEmpty()
                                                 join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                                 from s14sg in s14sGroup.DefaultIfEmpty()
                                                 where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid3
                                                 select s14sg.金額).Any() == true
                                                 ?
                                                (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                                 join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                                 from m05g in m05GROUP.DefaultIfEmpty()
                                                 join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                                 from s14sg in s14sGroup.DefaultIfEmpty()
                                                 where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid3
                                                 select s14sg.金額).Sum()
                                                 : 0,
                                    金額4 = row.経費ID == 9999999 ?
                                                row.印刷グループID == 2 ?
                                                a4 == null ? null : a4.限界利益 :
                                                prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid4).Select(c => c.金額).Sum() :
                                                carid4 == 9999 ?
                                                prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid4).Select(c => c.金額).Sum() :
                                                (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                                 join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                                 from m05g in m05GROUP.DefaultIfEmpty()
                                                 join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                                 from s14sg in s14sGroup.DefaultIfEmpty()
                                                 where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid4
                                                 select s14sg.金額).Any() == true
                                                 ?
                                                (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                                 join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                                 from m05g in m05GROUP.DefaultIfEmpty()
                                                 join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                                 from s14sg in s14sGroup.DefaultIfEmpty()
                                                 where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid4
                                                 select s14sg.金額).Sum()
                                                 : 0,
                                    金額5 = row.経費ID == 9999999 ?
                                                row.印刷グループID == 2 ?
                                                a5 == null ? null : a5.限界利益 :
                                                prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid5).Select(c => c.金額).Sum() :
                                                carid5 == 9999 ?
                                                prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid5).Select(c => c.金額).Sum() :
                                                (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                                 join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                                 from m05g in m05GROUP.DefaultIfEmpty()
                                                 join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                                 from s14sg in s14sGroup.DefaultIfEmpty()
                                                 where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid5
                                                 select s14sg.金額).Any() == true
                                                 ?
                                                (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                                 join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                                 from m05g in m05GROUP.DefaultIfEmpty()
                                                 join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                                 from s14sg in s14sGroup.DefaultIfEmpty()
                                                 where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid5
                                                 select s14sg.金額).Sum()
                                                 : 0,
                                    金額6 = row.経費ID == 9999999 ?
                                                row.印刷グループID == 2 ?
                                                a6 == null ? null : a6.限界利益 :
                                                prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid6).Select(c => c.金額).Sum() :
                                                carid6 == 9999 ?
                                                prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid6).Select(c => c.金額).Sum() :
                                                (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                                 join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                                 from m05g in m05GROUP.DefaultIfEmpty()
                                                 join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                                 from s14sg in s14sGroup.DefaultIfEmpty()
                                                 where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid6
                                                 select s14sg.金額).Any() == true
                                                 ?
                                                (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                                 join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                                 from m05g in m05GROUP.DefaultIfEmpty()
                                                 join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                                 from s14sg in s14sGroup.DefaultIfEmpty()
                                                 where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid6
                                                 select s14sg.金額).Sum()
                                                 : 0,

                                    //金額1 = (from p in prelist where p.車輌KEY == carid1 && p.経費ID == row.経費ID select p.金額).FirstOrDefault(),
                                    //金額2 = (from p in prelist where p.車輌KEY == carid2 && p.経費ID == row.経費ID select p.金額).FirstOrDefault(),
                                    //金額3 = (from p in prelist where p.車輌KEY == carid3 && p.経費ID == row.経費ID select p.金額).FirstOrDefault(),
                                    //金額4 = (from p in prelist where p.車輌KEY == carid4 && p.経費ID == row.経費ID select p.金額).FirstOrDefault(),
                                    //金額5 = (from p in prelist where p.車輌KEY == carid5 && p.経費ID == row.経費ID select p.金額).FirstOrDefault(),
                                    //金額6 = (from p in prelist where p.車輌KEY == carid6 && p.経費ID == row.経費ID select p.金額).FirstOrDefault(),

                                    空車KM1 = a1 == null ? null : a1.空車KM,
                                    空車KM2 = a2 == null ? null : a2.空車KM,
                                    空車KM3 = a3 == null ? null : a3.空車KM,
                                    空車KM4 = a4 == null ? null : a4.空車KM,
                                    空車KM5 = a5 == null ? null : a5.空車KM,
                                    空車KM6 = a6 == null ? null : a6.空車KM,

                                    経費ID = row.経費ID,
                                    経費項目名 = row.経費項目名,

                                    原価1KM1 = a1 == null ? null : a1.走行KM == null ? null : a1.走行KM == 0 ? null : (a1.一般管理費 + a1.小計1 + a1.小計2 + a1.小計3) / a1.走行KM,
                                    原価1KM2 = a2 == null ? null : a2.走行KM == null ? null : a2.走行KM == 0 ? null : (a2.一般管理費 + a2.小計1 + a2.小計2 + a2.小計3) / a2.走行KM,
                                    原価1KM3 = a3 == null ? null : a3.走行KM == null ? null : a3.走行KM == 0 ? null : (a3.一般管理費 + a3.小計1 + a3.小計2 + a3.小計3) / a3.走行KM,
                                    原価1KM4 = a4 == null ? null : a4.走行KM == null ? null : a4.走行KM == 0 ? null : (a4.一般管理費 + a4.小計1 + a4.小計2 + a4.小計3) / a4.走行KM,
                                    原価1KM5 = a5 == null ? null : a5.走行KM == null ? null : a5.走行KM == 0 ? null : (a5.一般管理費 + a5.小計1 + a5.小計2 + a5.小計3) / a5.走行KM,
                                    原価1KM6 = a6 == null ? null : a6.走行KM == null ? null : a6.走行KM == 0 ? null : (a6.一般管理費 + a6.小計1 + a6.小計2 + a6.小計3) / a6.走行KM,

                                    実車KM1 = a1 == null ? null : a1.実車KM,
                                    実車KM2 = a2 == null ? null : a2.実車KM,
                                    実車KM3 = a3 == null ? null : a3.実車KM,
                                    実車KM4 = a4 == null ? null : a4.実車KM,
                                    実車KM5 = a5 == null ? null : a5.実車KM,
                                    実車KM6 = a6 == null ? null : a6.実車KM,

                                    車種1 = a1 == null ? null : a1.車種,
                                    車種2 = a2 == null ? null : a2.車種,
                                    車種3 = a3 == null ? null : a3.車種,
                                    車種4 = a4 == null ? null : a4.車種,
                                    車種5 = a5 == null ? null : a5.車種,
                                    車種6 = a6 == null ? null : a6.車種,

                                    台数1 = a1 == null ? 0 : a1.台数,
                                    台数2 = a2 == null ? 0 : a2.台数,
                                    台数3 = a3 == null ? 0 : a3.台数,
                                    台数4 = a4 == null ? 0 : a4.台数,
                                    台数5 = a5 == null ? 0 : a5.台数,
                                    台数6 = a6 == null ? 0 : a6.台数,

                                    車種ID1 = a1 == null ? null : (int?)a1.車種ID,
                                    車種ID2 = a2 == null ? null : (int?)a2.車種ID,
                                    車種ID3 = a3 == null ? null : (int?)a3.車種ID,
                                    車種ID4 = a4 == null ? null : (int?)a4.車種ID,
                                    車種ID5 = a5 == null ? null : (int?)a5.車種ID,
                                    車種ID6 = a6 == null ? null : (int?)a6.車種ID,

                                    車輌直接益1 = a1 == null ? null : a1.運送収入 - syaryoutyokusetuhi1,
                                    車輌直接益2 = a2 == null ? null : a2.運送収入 - syaryoutyokusetuhi2,
                                    車輌直接益3 = a3 == null ? null : a3.運送収入 - syaryoutyokusetuhi3,
                                    車輌直接益4 = a4 == null ? null : a4.運送収入 - syaryoutyokusetuhi4,
                                    車輌直接益5 = a5 == null ? null : a5.運送収入 - syaryoutyokusetuhi5,
                                    車輌直接益6 = a6 == null ? null : a6.運送収入 - syaryoutyokusetuhi6,

                                    車輌直接費1 = syaryoutyokusetuhi1,
                                    車輌直接費2 = syaryoutyokusetuhi2,
                                    車輌直接費3 = syaryoutyokusetuhi3,
                                    車輌直接費4 = syaryoutyokusetuhi4,
                                    車輌直接費5 = syaryoutyokusetuhi5,
                                    車輌直接費6 = syaryoutyokusetuhi6,


                                    収入1KM1 = a1 == null ? null : (decimal?)a1.収入1KM,
                                    収入1KM2 = a2 == null ? null : (decimal?)a2.収入1KM,
                                    収入1KM3 = a3 == null ? null : (decimal?)a3.収入1KM,
                                    収入1KM4 = a4 == null ? null : (decimal?)a4.収入1KM,
                                    収入1KM5 = a5 == null ? null : (decimal?)a5.収入1KM,
                                    収入1KM6 = a6 == null ? null : (decimal?)a6.収入1KM,

                                    走行KM1 = a1 == null ? null : a1.走行KM,
                                    走行KM2 = a2 == null ? null : a2.走行KM,
                                    走行KM3 = a3 == null ? null : a3.走行KM,
                                    走行KM4 = a4 == null ? null : a4.走行KM,
                                    走行KM5 = a5 == null ? null : a5.走行KM,
                                    走行KM6 = a6 == null ? null : a6.走行KM,

                                    当月利益1 = a1 == null ? 0 - syaryoutyokusetuhi1 : a1.運送収入 == null ? 0 - syaryoutyokusetuhi1 - a1.一般管理費 : a1.一般管理費 == null ? a1.運送収入 - syaryoutyokusetuhi1 : a1.運送収入 - syaryoutyokusetuhi1 - a1.一般管理費,
                                    当月利益2 = a2 == null ? 0 - syaryoutyokusetuhi2 : a2.運送収入 == null ? 0 - syaryoutyokusetuhi2 - a2.一般管理費 : a2.一般管理費 == null ? a2.運送収入 - syaryoutyokusetuhi2 : a2.運送収入 - syaryoutyokusetuhi2 - a2.一般管理費,
                                    当月利益3 = a3 == null ? 0 - syaryoutyokusetuhi3 : a3.運送収入 == null ? 0 - syaryoutyokusetuhi3 - a3.一般管理費 : a3.一般管理費 == null ? a3.運送収入 - syaryoutyokusetuhi3 : a3.運送収入 - syaryoutyokusetuhi3 - a3.一般管理費,
                                    当月利益4 = a4 == null ? 0 - syaryoutyokusetuhi4 : a4.運送収入 == null ? 0 - syaryoutyokusetuhi4 - a4.一般管理費 : a4.一般管理費 == null ? a4.運送収入 - syaryoutyokusetuhi4 : a4.運送収入 - syaryoutyokusetuhi4 - a4.一般管理費,
                                    当月利益5 = a5 == null ? 0 - syaryoutyokusetuhi5 : a5.運送収入 == null ? 0 - syaryoutyokusetuhi5 - a5.一般管理費 : a5.一般管理費 == null ? a5.運送収入 - syaryoutyokusetuhi5 : a5.運送収入 - syaryoutyokusetuhi5 - a5.一般管理費,
                                    当月利益6 = a6 == null ? 0 - syaryoutyokusetuhi6 : a6.運送収入 == null ? 0 - syaryoutyokusetuhi6 - a6.一般管理費 : a6.一般管理費 == null ? a6.運送収入 - syaryoutyokusetuhi6 : a6.運送収入 - syaryoutyokusetuhi6 - a6.一般管理費,

                                    燃費1 = a1 == null ? null : (decimal?)a1.燃費,
                                    燃費2 = a2 == null ? null : (decimal?)a2.燃費,
                                    燃費3 = a3 == null ? null : (decimal?)a3.燃費,
                                    燃費4 = a4 == null ? null : (decimal?)a4.燃費,
                                    燃費5 = a5 == null ? null : (decimal?)a5.燃費,
                                    燃費6 = a6 == null ? null : (decimal?)a6.燃費,

                                    燃料L1 = a1 == null ? null : (decimal?)a1.燃料L,
                                    燃料L2 = a2 == null ? null : (decimal?)a2.燃料L,
                                    燃料L3 = a3 == null ? null : (decimal?)a3.燃料L,
                                    燃料L4 = a4 == null ? null : (decimal?)a4.燃料L,
                                    燃料L5 = a5 == null ? null : (decimal?)a5.燃料L,
                                    燃料L6 = a6 == null ? null : (decimal?)a6.燃料L,


                                    年 = 年.ToString(),
                                    コードFrom = s車種From,
                                    コードTo = s車種To,
                                    車種ﾋﾟｯｸｱｯﾌﾟ = s車種List,
                                    月 = 月.ToString(),

                                };
                                retList.Add(list);
                            };

                            cnt = 0;
                            carid1 = 0; carid2 = 0; carid3 = 0; carid4 = 0; carid5 = 0; carid6 = 0;
                        };

                    };

                    //余り分があれば
                    if (cnt > 0)
                    {
                        List<SRY11010_PRELIST_Member> plist = (from p in prelist
                                                               where p.車種ID == carid1
                                                               select p).ToList();

                        SRY11010g_Member a1 = (from p in carlistzzz where p.車種ID == carid1 select p).FirstOrDefault();
                        SRY11010g_Member a2 = (from p in carlistzzz where p.車種ID == carid2 select p).FirstOrDefault();
                        SRY11010g_Member a3 = (from p in carlistzzz where p.車種ID == carid3 select p).FirstOrDefault();
                        SRY11010g_Member a4 = (from p in carlistzzz where p.車種ID == carid4 select p).FirstOrDefault();
                        SRY11010g_Member a5 = (from p in carlistzzz where p.車種ID == carid5 select p).FirstOrDefault();
                        SRY11010g_Member a6 = (from p in carlistzzz where p.車種ID == carid6 select p).FirstOrDefault();


                        decimal syaryoutyokusetuhi1 = 0;
                        decimal syaryoutyokusetuhi2 = 0;
                        decimal syaryoutyokusetuhi3 = 0;
                        decimal syaryoutyokusetuhi4 = 0;
                        decimal syaryoutyokusetuhi5 = 0;
                        decimal syaryoutyokusetuhi6 = 0;

                        syaryoutyokusetuhi1 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.車種ID == carid1).Sum(c => c.金額);
                        syaryoutyokusetuhi2 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.車種ID == carid2).Sum(c => c.金額);
                        syaryoutyokusetuhi3 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.車種ID == carid3).Sum(c => c.金額);
                        syaryoutyokusetuhi4 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.車種ID == carid4).Sum(c => c.金額);
                        syaryoutyokusetuhi5 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.車種ID == carid5).Sum(c => c.金額);
                        syaryoutyokusetuhi6 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.車種ID == carid6).Sum(c => c.金額);

                        //if ((from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                        //        join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                        //        from m05g in m05GROUP.DefaultIfEmpty()
                        //        join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                        //        from s14sg in s14sGroup.DefaultIfEmpty()
                        //        where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid1
                        //        select s14sg.金額).Any())
                        //{
                        //    syaryoutyokusetuhi1 = (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                        //                            join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                        //                            from m05g in m05GROUP.DefaultIfEmpty()
                        //                            join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                        //                            from s14sg in s14sGroup.DefaultIfEmpty()
                        //                            where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid1
                        //                            select s14sg.金額).Sum();
                        //}

                        //if ((from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                        //        join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                        //        from m05g in m05GROUP.DefaultIfEmpty()
                        //        join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                        //        from s14sg in s14sGroup.DefaultIfEmpty()
                        //        where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid2
                        //        select s14sg.金額).Any())
                        //{
                        //    syaryoutyokusetuhi2 = (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                        //                            join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                        //                            from m05g in m05GROUP.DefaultIfEmpty()
                        //                            join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                        //                            from s14sg in s14sGroup.DefaultIfEmpty()
                        //                            where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid2
                        //                            select s14sg.金額).Sum();
                        //}

                        //if ((from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                        //     join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                        //     from m05g in m05GROUP.DefaultIfEmpty()
                        //     join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                        //     from s14sg in s14sGroup.DefaultIfEmpty()
                        //     where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid3
                        //     select s14sg.金額).Any())
                        //{
                        //    syaryoutyokusetuhi3 = (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                        //                           join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                        //                           from m05g in m05GROUP.DefaultIfEmpty()
                        //                           join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                        //                           from s14sg in s14sGroup.DefaultIfEmpty()
                        //                           where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid3
                        //                           select s14sg.金額).Sum();
                        //}

                        //if ((from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                        //     join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                        //     from m05g in m05GROUP.DefaultIfEmpty()
                        //     join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                        //     from s14sg in s14sGroup.DefaultIfEmpty()
                        //     where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid4
                        //     select s14sg.金額).Any())
                        //{
                        //    syaryoutyokusetuhi4 = (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                        //                           join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                        //                           from m05g in m05GROUP.DefaultIfEmpty()
                        //                           join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                        //                           from s14sg in s14sGroup.DefaultIfEmpty()
                        //                           where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid4
                        //                           select s14sg.金額).Sum();
                        //}

                        //if ((from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                        //     join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                        //     from m05g in m05GROUP.DefaultIfEmpty()
                        //     join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                        //     from s14sg in s14sGroup.DefaultIfEmpty()
                        //     where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid5
                        //     select s14sg.金額).Any())
                        //{
                        //    syaryoutyokusetuhi5 = (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                        //                           join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                        //                           from m05g in m05GROUP.DefaultIfEmpty()
                        //                           join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                        //                           from s14sg in s14sGroup.DefaultIfEmpty()
                        //                           where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid5
                        //                           select s14sg.金額).Sum();
                        //}

                        //if ((from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                        //     join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                        //     from m05g in m05GROUP.DefaultIfEmpty()
                        //     join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                        //     from s14sg in s14sGroup.DefaultIfEmpty()
                        //     where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid6
                        //     select s14sg.金額).Any())
                        //{
                        //    syaryoutyokusetuhi6 = (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                        //                           join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                        //                           from m05g in m05GROUP.DefaultIfEmpty()
                        //                           join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                        //                           from s14sg in s14sGroup.DefaultIfEmpty()
                        //                           where prt_GROPID.Contains(s14sg.経費項目ID) && s14sg.集計年月 == i年月 && m06.車種ID == carid6
                        //                           select s14sg.金額).Sum();
                        //}


                        foreach (var row in plist)
                        {

                            SRY11010_Member list = new SRY11010_Member()
                            {
                                一般管理費1 = a1 == null ? null : a1.一般管理費,
                                一般管理費2 = a2 == null ? null : a2.一般管理費,
                                一般管理費3 = a3 == null ? null : a3.一般管理費,
                                一般管理費4 = a4 == null ? null : a4.一般管理費,
                                一般管理費5 = a5 == null ? null : a5.一般管理費,
                                一般管理費6 = a6 == null ? null : a6.一般管理費,

                                印刷グループID = row.印刷グループID,

                                運送収入1 = a1 == null ? null : a1.運送収入,
                                運送収入2 = a2 == null ? null : a2.運送収入,
                                運送収入3 = a3 == null ? null : a3.運送収入,
                                運送収入4 = a4 == null ? null : a4.運送収入,
                                運送収入5 = a5 == null ? null : a5.運送収入,
                                運送収入6 = a6 == null ? null : a6.運送収入,

                                稼働日数1 = a1 == null ? null : a1.稼働日数,
                                稼働日数2 = a2 == null ? null : a2.稼働日数,
                                稼働日数3 = a3 == null ? null : a3.稼働日数,
                                稼働日数4 = a4 == null ? null : a4.稼働日数,
                                稼働日数5 = a5 == null ? null : a5.稼働日数,
                                稼働日数6 = a6 == null ? null : a6.稼働日数,


                                金額1 = row.経費ID == 9999999 ?
                                            row.印刷グループID == 2 ?
                                            a1 == null ? null : a1.限界利益 :
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid1).Select(c => c.金額).Sum() :
                                            carid1 == 9999 ?
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid1).Select(c => c.金額).Sum() :
                                            (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                             join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                             from m05g in m05GROUP.DefaultIfEmpty()
                                             join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                             from s14sg in s14sGroup.DefaultIfEmpty()
                                             where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid1
                                             select s14sg.金額).Any() == true
                                             ?
                                            (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                             join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                             from m05g in m05GROUP.DefaultIfEmpty()
                                             join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                             from s14sg in s14sGroup.DefaultIfEmpty()
                                             where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid1
                                             select s14sg.金額).Sum()
                                             : 0,
                                金額2 = row.経費ID == 9999999 ?
                                            row.印刷グループID == 2 ?
                                            a2 == null ? null : a2.限界利益 :
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid2).Select(c => c.金額).Sum() :
                                            carid2 == 9999 ?
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid2).Select(c => c.金額).Sum() :
                                            (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                             join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                             from m05g in m05GROUP.DefaultIfEmpty()
                                             join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                             from s14sg in s14sGroup.DefaultIfEmpty()
                                             where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid2
                                             select s14sg.金額).Any() == true
                                             ?
                                            (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                             join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                             from m05g in m05GROUP.DefaultIfEmpty()
                                             join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                             from s14sg in s14sGroup.DefaultIfEmpty()
                                             where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid2
                                             select s14sg.金額).Sum()
                                             : 0,
                                金額3 = row.経費ID == 9999999 ?
                                            row.印刷グループID == 2 ?
                                            a3 == null ? null : a3.限界利益 :
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid3).Select(c => c.金額).Sum() :
                                            carid3 == 9999 ?
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid3).Select(c => c.金額).Sum() :
                                            (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                             join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                             from m05g in m05GROUP.DefaultIfEmpty()
                                             join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                             from s14sg in s14sGroup.DefaultIfEmpty()
                                             where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid3
                                             select s14sg.金額).Any() == true
                                             ?
                                            (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                             join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                             from m05g in m05GROUP.DefaultIfEmpty()
                                             join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                             from s14sg in s14sGroup.DefaultIfEmpty()
                                             where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid3
                                             select s14sg.金額).Sum()
                                             : 0,
                                金額4 = row.経費ID == 9999999 ?
                                            row.印刷グループID == 2 ?
                                            a4 == null ? null : a4.限界利益 :
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid4).Select(c => c.金額).Sum() :
                                            carid4 == 9999 ?
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid4).Select(c => c.金額).Sum() :
                                            (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                             join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                             from m05g in m05GROUP.DefaultIfEmpty()
                                             join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                             from s14sg in s14sGroup.DefaultIfEmpty()
                                             where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid4
                                             select s14sg.金額).Any() == true
                                             ?
                                            (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                             join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                             from m05g in m05GROUP.DefaultIfEmpty()
                                             join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                             from s14sg in s14sGroup.DefaultIfEmpty()
                                             where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid4
                                             select s14sg.金額).Sum()
                                             : 0,
                                金額5 = row.経費ID == 9999999 ?
                                            row.印刷グループID == 2 ?
                                            a5 == null ? null : a5.限界利益 :
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid5).Select(c => c.金額).Sum() :
                                            carid5 == 9999 ?
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid5).Select(c => c.金額).Sum() :
                                            (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                             join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                             from m05g in m05GROUP.DefaultIfEmpty()
                                             join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                             from s14sg in s14sGroup.DefaultIfEmpty()
                                             where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid5
                                             select s14sg.金額).Any() == true
                                             ?
                                            (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                             join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                             from m05g in m05GROUP.DefaultIfEmpty()
                                             join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                             from s14sg in s14sGroup.DefaultIfEmpty()
                                             where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid5
                                             select s14sg.金額).Sum()
                                             : 0,
                                金額6 = row.経費ID == 9999999 ?
                                            row.印刷グループID == 2 ?
                                            a6 == null ? null : a6.限界利益 :
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid6).Select(c => c.金額).Sum() :
                                            carid6 == 9999 ?
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車種ID == carid6).Select(c => c.金額).Sum() :
                                            (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                             join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                             from m05g in m05GROUP.DefaultIfEmpty()
                                             join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                             from s14sg in s14sGroup.DefaultIfEmpty()
                                             where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid6
                                             select s14sg.金額).Any() == true
                                             ?
                                            (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                             join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車区分 == 0 || c.廃車区分 == 1 && c.廃車日 > 終了年月日)) on m06.車種ID equals m05.車種ID into m05GROUP
                                             from m05g in m05GROUP.DefaultIfEmpty()
                                             join s14s in context.S14_CARSB on m05g.車輌KEY equals s14s.車輌KEY into s14sGroup
                                             from s14sg in s14sGroup.DefaultIfEmpty()
                                             where s14sg.経費項目ID == row.経費ID && s14sg.集計年月 == i年月 && m06.車種ID == carid6
                                             select s14sg.金額).Sum()
                                             : 0,

                                //金額1 = (from p in prelist where p.車輌KEY == carid1 && p.経費ID == row.経費ID select p.金額).FirstOrDefault(),
                                //金額2 = (from p in prelist where p.車輌KEY == carid2 && p.経費ID == row.経費ID select p.金額).FirstOrDefault(),
                                //金額3 = (from p in prelist where p.車輌KEY == carid3 && p.経費ID == row.経費ID select p.金額).FirstOrDefault(),
                                //金額4 = (from p in prelist where p.車輌KEY == carid4 && p.経費ID == row.経費ID select p.金額).FirstOrDefault(),
                                //金額5 = (from p in prelist where p.車輌KEY == carid5 && p.経費ID == row.経費ID select p.金額).FirstOrDefault(),
                                //金額6 = (from p in prelist where p.車輌KEY == carid6 && p.経費ID == row.経費ID select p.金額).FirstOrDefault(),

                                空車KM1 = a1 == null ? null : a1.空車KM,
                                空車KM2 = a2 == null ? null : a2.空車KM,
                                空車KM3 = a3 == null ? null : a3.空車KM,
                                空車KM4 = a4 == null ? null : a4.空車KM,
                                空車KM5 = a5 == null ? null : a5.空車KM,
                                空車KM6 = a6 == null ? null : a6.空車KM,

                                経費ID = row.経費ID,
                                経費項目名 = row.経費項目名,

                                原価1KM1 = a1 == null ? null : a1.走行KM == null ? null : a1.走行KM == 0 ? null : (a1.一般管理費 + a1.小計1 + a1.小計2 + a1.小計3) / a1.走行KM,
                                原価1KM2 = a2 == null ? null : a2.走行KM == null ? null : a2.走行KM == 0 ? null : (a2.一般管理費 + a2.小計1 + a2.小計2 + a2.小計3) / a2.走行KM,
                                原価1KM3 = a3 == null ? null : a3.走行KM == null ? null : a3.走行KM == 0 ? null : (a3.一般管理費 + a3.小計1 + a3.小計2 + a3.小計3) / a3.走行KM,
                                原価1KM4 = a4 == null ? null : a4.走行KM == null ? null : a4.走行KM == 0 ? null : (a4.一般管理費 + a4.小計1 + a4.小計2 + a4.小計3) / a4.走行KM,
                                原価1KM5 = a5 == null ? null : a5.走行KM == null ? null : a5.走行KM == 0 ? null : (a5.一般管理費 + a5.小計1 + a5.小計2 + a5.小計3) / a5.走行KM,
                                原価1KM6 = a6 == null ? null : a6.走行KM == null ? null : a6.走行KM == 0 ? null : (a6.一般管理費 + a6.小計1 + a6.小計2 + a6.小計3) / a6.走行KM,

                                実車KM1 = a1 == null ? null : a1.実車KM,
                                実車KM2 = a2 == null ? null : a2.実車KM,
                                実車KM3 = a3 == null ? null : a3.実車KM,
                                実車KM4 = a4 == null ? null : a4.実車KM,
                                実車KM5 = a5 == null ? null : a5.実車KM,
                                実車KM6 = a6 == null ? null : a6.実車KM,

                                車種1 = a1 == null ? null : a1.車種,
                                車種2 = a2 == null ? null : a2.車種,
                                車種3 = a3 == null ? null : a3.車種,
                                車種4 = a4 == null ? null : a4.車種,
                                車種5 = a5 == null ? null : a5.車種,
                                車種6 = a6 == null ? null : a6.車種,

                                台数1 = a1 == null ? 0 : a1.台数,
                                台数2 = a2 == null ? 0 : a2.台数,
                                台数3 = a3 == null ? 0 : a3.台数,
                                台数4 = a4 == null ? 0 : a4.台数,
                                台数5 = a5 == null ? 0 : a5.台数,
                                台数6 = a6 == null ? 0 : a6.台数,

                                車種ID1 = a1 == null ? null : (int?)a1.車種ID,
                                車種ID2 = a2 == null ? null : (int?)a2.車種ID,
                                車種ID3 = a3 == null ? null : (int?)a3.車種ID,
                                車種ID4 = a4 == null ? null : (int?)a4.車種ID,
                                車種ID5 = a5 == null ? null : (int?)a5.車種ID,
                                車種ID6 = a6 == null ? null : (int?)a6.車種ID,

                                車輌直接益1 = a1 == null ? null : a1.運送収入 - syaryoutyokusetuhi1,
                                車輌直接益2 = a2 == null ? null : a2.運送収入 - syaryoutyokusetuhi2,
                                車輌直接益3 = a3 == null ? null : a3.運送収入 - syaryoutyokusetuhi3,
                                車輌直接益4 = a4 == null ? null : a4.運送収入 - syaryoutyokusetuhi4,
                                車輌直接益5 = a5 == null ? null : a5.運送収入 - syaryoutyokusetuhi5,
                                車輌直接益6 = a6 == null ? null : a6.運送収入 - syaryoutyokusetuhi6,

                                車輌直接費1 = syaryoutyokusetuhi1,
                                車輌直接費2 = syaryoutyokusetuhi2,
                                車輌直接費3 = syaryoutyokusetuhi3,
                                車輌直接費4 = syaryoutyokusetuhi4,
                                車輌直接費5 = syaryoutyokusetuhi5,
                                車輌直接費6 = syaryoutyokusetuhi6,


                                収入1KM1 = a1 == null ? null : (decimal?)a1.収入1KM,
                                収入1KM2 = a2 == null ? null : (decimal?)a2.収入1KM,
                                収入1KM3 = a3 == null ? null : (decimal?)a3.収入1KM,
                                収入1KM4 = a4 == null ? null : (decimal?)a4.収入1KM,
                                収入1KM5 = a5 == null ? null : (decimal?)a5.収入1KM,
                                収入1KM6 = a6 == null ? null : (decimal?)a6.収入1KM,

                                走行KM1 = a1 == null ? null : a1.走行KM,
                                走行KM2 = a2 == null ? null : a2.走行KM,
                                走行KM3 = a3 == null ? null : a3.走行KM,
                                走行KM4 = a4 == null ? null : a4.走行KM,
                                走行KM5 = a5 == null ? null : a5.走行KM,
                                走行KM6 = a6 == null ? null : a6.走行KM,

                                当月利益1 = a1 == null ? 0 - syaryoutyokusetuhi1 : a1.運送収入 == null ? 0 - syaryoutyokusetuhi1 - a1.一般管理費 : a1.一般管理費 == null ? a1.運送収入 - syaryoutyokusetuhi1 : a1.運送収入 - syaryoutyokusetuhi1 - a1.一般管理費,
                                当月利益2 = a2 == null ? 0 - syaryoutyokusetuhi2 : a2.運送収入 == null ? 0 - syaryoutyokusetuhi2 - a2.一般管理費 : a2.一般管理費 == null ? a2.運送収入 - syaryoutyokusetuhi2 : a2.運送収入 - syaryoutyokusetuhi2 - a2.一般管理費,
                                当月利益3 = a3 == null ? 0 - syaryoutyokusetuhi3 : a3.運送収入 == null ? 0 - syaryoutyokusetuhi3 - a3.一般管理費 : a3.一般管理費 == null ? a3.運送収入 - syaryoutyokusetuhi3 : a3.運送収入 - syaryoutyokusetuhi3 - a3.一般管理費,
                                当月利益4 = a4 == null ? 0 - syaryoutyokusetuhi4 : a4.運送収入 == null ? 0 - syaryoutyokusetuhi4 - a4.一般管理費 : a4.一般管理費 == null ? a4.運送収入 - syaryoutyokusetuhi4 : a4.運送収入 - syaryoutyokusetuhi4 - a4.一般管理費,
                                当月利益5 = a5 == null ? 0 - syaryoutyokusetuhi5 : a5.運送収入 == null ? 0 - syaryoutyokusetuhi5 - a5.一般管理費 : a5.一般管理費 == null ? a5.運送収入 - syaryoutyokusetuhi5 : a5.運送収入 - syaryoutyokusetuhi5 - a5.一般管理費,
                                当月利益6 = a6 == null ? 0 - syaryoutyokusetuhi6 : a6.運送収入 == null ? 0 - syaryoutyokusetuhi6 - a6.一般管理費 : a6.一般管理費 == null ? a6.運送収入 - syaryoutyokusetuhi6 : a6.運送収入 - syaryoutyokusetuhi6 - a6.一般管理費,

                                燃費1 = a1 == null ? null : (decimal?)a1.燃費,
                                燃費2 = a2 == null ? null : (decimal?)a2.燃費,
                                燃費3 = a3 == null ? null : (decimal?)a3.燃費,
                                燃費4 = a4 == null ? null : (decimal?)a4.燃費,
                                燃費5 = a5 == null ? null : (decimal?)a5.燃費,
                                燃費6 = a6 == null ? null : (decimal?)a6.燃費,

                                燃料L1 = a1 == null ? null : (decimal?)a1.燃料L,
                                燃料L2 = a2 == null ? null : (decimal?)a2.燃料L,
                                燃料L3 = a3 == null ? null : (decimal?)a3.燃料L,
                                燃料L4 = a4 == null ? null : (decimal?)a4.燃料L,
                                燃料L5 = a5 == null ? null : (decimal?)a5.燃料L,
                                燃料L6 = a6 == null ? null : (decimal?)a6.燃料L,


                                年 = 年.ToString(),
                                コードFrom = s車種From,
                                コードTo = s車種To,
                                車種ﾋﾟｯｸｱｯﾌﾟ = s車種List,
                                月 = 月.ToString(),

                            };
                            retList.Add(list);
                        };

                        cnt = 0;
                        carid1 = 0; carid2 = 0; carid3 = 0; carid4 = 0; carid5 = 0; carid6 = 0;
                    };


                    //var header = (from s14 in context.S14_CAR
                    //              where s14.集計年月 >= i集計期間From && s14.集計年月 <= i集計期間To
                    //              group s14 by s14.車輌KEY into Group
                    //              select new SRY11010_Member
                    //              {
                    //                  車輌Key = Group.Key,
                    //                  合計 = Group.Sum(c => c.金額),
                    //              }).AsQueryable();



                    //var goukei = (from s14c in context.S14_CARSB
                    //              where s14c.集計年月 >= i集計期間From && s14c.集計年月 <= i集計期間To
                    //              group s14c by s14c.車輌KEY into Group
                    //              select new SRY11010g_Member
                    //              {
                    //                  車輌Key = Group.Key,
                    //                  合計 = Group.Sum(c => c.金額),
                    //              }).AsQueryable();

                    //var query = (from m05 in context.M05_CAR.Where(c => c.廃車区分 == 0 && c.削除日付 == null)
                    //             join s14 in context.S14_CAR.Where(c => c.集計年月 >= i集計期間From && c.集計年月 <= i集計期間To) on m05.車輌KEY equals s14.車輌KEY into Group
                    //             from s14Group in Group
                    //             join s14g in goukei on s14Group.車輌KEY equals s14g.車輌Key into sg14Group
                    //             from sgg14Group in sg14Group
                    //             group new { m05, s14Group ,sgg14Group } by new { m05.車輌ID, m05.車輌番号 ,sgg14Group.合計 } into grGroup
                    //            select new SRY11010_Member
                    //            {
                    //                車輌ID = grGroup.Key.車輌ID,
                    //                車輌番号 = grGroup.Key.車輌番号,
                    //                日数 = grGroup.Sum(c => c.s14Group.稼動日数) == null ? 0 : grGroup.Sum(c => c.s14Group.稼動日数),
                    //                拘束H = grGroup.Sum(c => c.s14Group.拘束時間) == null ? 0 : grGroup.Sum(c => c.s14Group.拘束時間),
                    //                運転H = grGroup.Sum(c => c.s14Group.運転時間) == null ? 0 : grGroup.Sum(c => c.s14Group.運転時間),
                    //                高速H = grGroup.Sum(c => c.s14Group.高速時間) == null ? 0 : grGroup.Sum(c => c.s14Group.高速時間),
                    //                作業H = grGroup.Sum(c => c.s14Group.作業時間) == null ? 0 : grGroup.Sum(c => c.s14Group.作業時間),
                    //                休憩H = grGroup.Sum(c => c.s14Group.休憩時間) == null ? 0 : grGroup.Sum(c => c.s14Group.休憩時間),
                    //                残業H = grGroup.Sum(c => c.s14Group.残業時間) == null ? 0 : grGroup.Sum(c => c.s14Group.残業時間),
                    //                深夜H = grGroup.Sum(c => c.s14Group.深夜時間) == null ? 0 : grGroup.Sum(c => c.s14Group.深夜時間),
                    //                走行KM = grGroup.Sum(c => c.s14Group.走行ＫＭ) == null ? 0 : grGroup.Sum(c => c.s14Group.走行ＫＭ),
                    //                実車KM = grGroup.Sum(c => c.s14Group.実車ＫＭ) == null ? 0 : grGroup.Sum(c => c.s14Group.実車ＫＭ),
                    //                輸送屯数 = grGroup.Sum(c => c.s14Group.輸送屯数) == null ? 0 : grGroup.Sum(c => c.s14Group.輸送屯数),
                    //                運送収入 = grGroup.Sum(c => c.s14Group.運送収入) == null ? 0 : grGroup.Sum(c => c.s14Group.運送収入),
                    //                燃料L = grGroup.Sum(c => c.s14Group.燃料Ｌ) == null ? 0 : grGroup.Sum(c => c.s14Group.燃料Ｌ),
                    //                経費合計 = grGroup.Key.合計,
                    //                集計年月From = s集計期間From,
                    //                集計年月To = s集計期間To,
                    //                表示順序 = i表示順序 == 0 ? "コード順" : i表示順序 == 1 ? "車種順" : "運送収入順",
                    //                車輌指定 = s車輌From + "～" + s車輌To,
                    //                車輌ﾋﾟｯｸｱｯﾌﾟ = 車輌ﾋﾟｯｸｱｯﾌﾟ指定 == "" ? "無" : 車輌ﾋﾟｯｸｱｯﾌﾟ指定,
                    //            }).AsQueryable();

                    ////***検索条件***//
                    //if (!(string.IsNullOrEmpty(s車輌From + s車輌To) && i車輌List.Length == 0))
                    //{

                    //    //車輌From処理　Min値
                    //    if (!string.IsNullOrEmpty(s車輌From))
                    //    {
                    //        int i車輌From = AppCommon.IntParse(s車輌From);
                    //        query = query.Where(c => c.車輌ID >= i車輌From);
                    //    }

                    //    //車輌To処理　Max値
                    //    if (!string.IsNullOrEmpty(s車輌To))
                    //    {
                    //        int i車輌TO = AppCommon.IntParse(s車輌To);
                    //        query = query.Where(c => c.車輌ID <= i車輌TO);
                    //    }

                    //    if (i車輌List.Length > 0)
                    //    {
                    //        var intCause = i車輌List;
                    //        query = query.Union(from m05 in context.M05_CAR.Where(c => c.廃車区分 == 0 && c.削除日付 == null)
                    //                            join s14 in context.S14_CAR.Where(c => c.集計年月 >= i集計期間From && c.集計年月 <= i集計期間To) on m05.車輌KEY equals s14.車輌KEY into Group
                    //                            from s14Group in Group
                    //                            join s14g in goukei on s14Group.車輌KEY equals s14g.車輌Key into sg14Group
                    //                            from sgg14Group in sg14Group
                    //                            group new { m05, s14Group, sgg14Group } by new { m05.車輌ID, m05.車輌番号, sgg14Group.合計 } into grGroup
                    //                            where intCause.Contains(grGroup.Key.車輌ID)
                    //                            select new SRY11010_Member
                    //                            {
                    //                                車輌ID = grGroup.Key.車輌ID,
                    //                                車輌番号 = grGroup.Key.車輌番号,
                    //                                日数 = grGroup.Sum(c => c.s14Group.稼動日数) == null ? 0 : grGroup.Sum(c => c.s14Group.稼動日数),
                    //                                拘束H = grGroup.Sum(c => c.s14Group.拘束時間) == null ? 0 : grGroup.Sum(c => c.s14Group.拘束時間),
                    //                                運転H = grGroup.Sum(c => c.s14Group.運転時間) == null ? 0 : grGroup.Sum(c => c.s14Group.運転時間),
                    //                                高速H = grGroup.Sum(c => c.s14Group.高速時間) == null ? 0 : grGroup.Sum(c => c.s14Group.高速時間),
                    //                                作業H = grGroup.Sum(c => c.s14Group.作業時間) == null ? 0 : grGroup.Sum(c => c.s14Group.作業時間),
                    //                                休憩H = grGroup.Sum(c => c.s14Group.休憩時間) == null ? 0 : grGroup.Sum(c => c.s14Group.休憩時間),
                    //                                残業H = grGroup.Sum(c => c.s14Group.残業時間) == null ? 0 : grGroup.Sum(c => c.s14Group.残業時間),
                    //                                深夜H = grGroup.Sum(c => c.s14Group.深夜時間) == null ? 0 : grGroup.Sum(c => c.s14Group.深夜時間),
                    //                                走行KM = grGroup.Sum(c => c.s14Group.走行ＫＭ) == null ? 0 : grGroup.Sum(c => c.s14Group.走行ＫＭ),
                    //                                実車KM = grGroup.Sum(c => c.s14Group.実車ＫＭ) == null ? 0 : grGroup.Sum(c => c.s14Group.実車ＫＭ),
                    //                                輸送屯数 = grGroup.Sum(c => c.s14Group.輸送屯数) == null ? 0 : grGroup.Sum(c => c.s14Group.輸送屯数),
                    //                                運送収入 = grGroup.Sum(c => c.s14Group.運送収入) == null ? 0 : grGroup.Sum(c => c.s14Group.運送収入),
                    //                                燃料L = grGroup.Sum(c => c.s14Group.燃料Ｌ) == null ? 0 : grGroup.Sum(c => c.s14Group.燃料Ｌ),
                    //                                経費合計 = grGroup.Key.合計,
                    //                                集計年月From = s集計期間From,
                    //                                集計年月To = s集計期間To,
                    //                                表示順序 = i表示順序 == 0 ? "コード順" : i表示順序 == 1 ? "車種順" : "運送収入順",
                    //                                車輌指定 = s車輌From + "～" + s車輌To,
                    //                                車輌ﾋﾟｯｸｱｯﾌﾟ = 車輌ﾋﾟｯｸｱｯﾌﾟ指定 == "" ? "無" : 車輌ﾋﾟｯｸｱｯﾌﾟ指定,
                    //                            }).AsQueryable();
                    //    }
                    //}
                    //else
                    //{
                    //    //車輌FromがNullだった場合
                    //    if (string.IsNullOrEmpty(s車輌From))
                    //    {
                    //        query = query.Where(c => c.車輌ID >= int.MinValue);
                    //    }
                    //    //車輌ToがNullだった場合
                    //    if (string.IsNullOrEmpty(s車輌To))
                    //    {
                    //        query = query.Where(c => c.車輌ID <= int.MaxValue);
                    //    }

                    //}

                    ////乗務員指定の表示
                    //if (i車輌List.Length > 0)
                    //{
                    //    for (int i = 0; i < query.Count(); i++)
                    //    {
                    //        車輌ﾋﾟｯｸｱｯﾌﾟ指定 = 車輌ﾋﾟｯｸｱｯﾌﾟ指定 + i車輌List[i].ToString();

                    //        if (i < i車輌List.Length)
                    //        {

                    //            if (i == i車輌List.Length - 1)
                    //            {
                    //                break;
                    //            }

                    //            車輌ﾋﾟｯｸｱｯﾌﾟ指定 = 車輌ﾋﾟｯｸｱｯﾌﾟ指定 + ",";

                    //        }

                    //        if (i車輌List.Length == 1)
                    //        {
                    //            break;
                    //        }

                    //    }
                    //}

                    ////表示順序変更
                    //switch (i表示順序)
                    //{
                    //    case 0:
                    //        //車輌番号昇順
                    //        query = query.OrderBy(c => c.車輌ID);
                    //        break;
                    //    case 1:
                    //        query = query.OrderBy(c => c.車輌番号);
                    //        break;
                    //    case 2:
                    //        //運送収入降順
                    //        query = query.OrderByDescending(c => c.運送収入);
                    //        break;
                    //}

                    //query = query.Where(c => c.経費合計 != 0);

                    //  retList = query.ToList();
                    return retList;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion


		#region SRY11010 CSV
		public DataTable SRY11010_GetData_CSV(string s車種From, string s車種To, int?[] i車種List, string s車種List, int i年月, int 年, int 月, DateTime? 終了年月日)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{

				List<SRY11010_Member> retList = new List<SRY11010_Member>();

				context.Connection.Open();
				try
				{
					string 車輌ﾋﾟｯｸｱｯﾌﾟ指定 = string.Empty;

					var carlistz = (from m05 in context.M05_CAR.Where(c => c.削除日付 == null)
									join s14 in context.S14_CAR.Where(c => c.集計年月 == i年月) on m05.車輌KEY equals s14.車輌KEY
									join m04 in context.M04_DRV.Where(c => c.削除日付 == null) on m05.乗務員KEY equals m04.乗務員KEY into m04Group
									from m04g in m04Group.DefaultIfEmpty()
									join m06 in context.M06_SYA.Where(c => c.削除日付 == null) on m05.車種ID equals m06.車種ID into m06Group
									from m06g in m06Group
									orderby m05.車種ID
									where m05.車種ID != 0
									select new SRY11010g_Member
									{
										車輌KEY = m05.車輌KEY,
										車輌ID = m05.車輌ID,
										車種ID = m05.車種ID,
										車輌番号 = m05.車輌番号,
										主乗務員 = m04g.乗務員名,
										車種 = m06g.車種名,
										運送収入 = s14.運送収入,

										一般管理費 = s14.一般管理費,

										稼働日数 = s14.稼動日数,
										実車KM = s14.実車ＫＭ,
										空車KM = s14.走行ＫＭ - s14.実車ＫＭ,
										走行KM = s14.走行ＫＭ,
										燃料L = s14.燃料Ｌ == null ? 0 : s14.燃料Ｌ,
										燃費 = s14.燃料Ｌ == 0 ? 0 : s14.燃料Ｌ == null ? 0 : s14.走行ＫＭ / s14.燃料Ｌ,
										収入1KM = s14.走行ＫＭ == 0 ? 0 : s14.走行ＫＭ == null ? 0 : s14.運送収入 / s14.走行ＫＭ,
									}).AsQueryable();

					if (!(string.IsNullOrEmpty(s車種From + s車種To) && i車種List.Length == 0))
					{
						if (string.IsNullOrEmpty(s車種From + s車種To))
						{
							carlistz = carlistz.Where(c => c.車種ID >= int.MaxValue);
						}

						//車種From処理　Min値
						if (!string.IsNullOrEmpty(s車種From))
						{
							int i車種From = AppCommon.IntParse(s車種From);
							carlistz = carlistz.Where(c => c.車種ID >= i車種From);
						}

						//車種To処理　Max値
						if (!string.IsNullOrEmpty(s車種To))
						{
							int i車種TO = AppCommon.IntParse(s車種To);
							carlistz = carlistz.Where(c => c.車種ID <= i車種TO);
						}

						if (i車種List.Length > 0)
						{
							var intCause = i車種List;
							carlistz = carlistz.Union(from m05 in context.M05_CAR.Where(c => c.削除日付 == null)
													  join s14 in context.S14_CAR.Where(c => c.集計年月 == i年月) on m05.車輌KEY equals s14.車輌KEY
													  join m04 in context.M04_DRV.Where(c => c.削除日付 == null) on m05.乗務員KEY equals m04.乗務員KEY into m04Group
													  from m04g in m04Group.DefaultIfEmpty()
													  join m06 in context.M06_SYA.Where(c => c.削除日付 == null) on m05.車種ID equals m06.車種ID into m06Group
													  from m06g in m06Group
													  orderby m05.車種ID
													  where intCause.Contains(m05.車種ID)
													  select new SRY11010g_Member
													  {
														  車輌KEY = m05.車輌KEY,
														  車輌ID = m05.車輌ID,
														  車種ID = m05.車種ID,
														  車輌番号 = m05.車輌番号,
														  主乗務員 = m04g.乗務員名,
														  車種 = m06g.車種名,
														  運送収入 = s14.運送収入,

														  一般管理費 = s14.一般管理費,

														  稼働日数 = s14.稼動日数,
														  実車KM = s14.実車ＫＭ,
														  空車KM = s14.走行ＫＭ - s14.実車ＫＭ,
														  走行KM = s14.走行ＫＭ,
														  燃料L = s14.燃料Ｌ,
														  燃費 = s14.燃料Ｌ == 0 ? 0 : s14.燃料Ｌ == null ? 0 : s14.走行ＫＭ / s14.燃料Ｌ,
														  収入1KM = s14.走行ＫＭ == 0 ? 0 : s14.走行ＫＭ == null ? 0 : s14.運送収入 / s14.走行ＫＭ,
													  });
						};
					}
					else
					{
						//車種From処理　Min値
						if (!string.IsNullOrEmpty(s車種From))
						{
							int i車種From = AppCommon.IntParse(s車種From);
							carlistz = carlistz.Where(c => c.車種ID >= i車種From);
						}

						//車種To処理　Max値
						if (!string.IsNullOrEmpty(s車種To))
						{
							int i車種TO = AppCommon.IntParse(s車種To);
							carlistz = carlistz.Where(c => c.車種ID <= i車種TO);
						}
					}

					carlistz.Distinct();
					var carlistzz = carlistz.ToList();




					//運行費項目取得
					var keilist = (from m07 in context.M07_KEI
								   where m07.編集区分 == 0 && m07.固定変動区分 == 1 && m07.経費区分 != 3
								   orderby m07.経費項目ID
								   select new JMI12010_KEI_Member
								   {
									   印刷グループID = 1,
									   経費ID = m07.経費項目ID,
									   経費項目名 = m07.経費項目名,
								   }).AsQueryable();

					//運行費小計
					bool bl = (from m07 in context.M07_KEI select m07).Any(c => c.編集区分 == 0 && c.固定変動区分 == 1 && c.経費区分 != 3);

					if (bl)
					{
						//var a = (from m07 in context.M07_KEI
						//         select new JMI12010_KEI_Member
						//         {
						//             印刷グループID = 1,
						//             経費ID = 9999999,
						//             経費項目名 = "【 小 計 】",
						//         }).AsQueryable();
						//a = a.Take(1);
						//keilist = keilist.Union(a.AsQueryable());

						//限界利益
						var a = (from m07 in context.M07_KEI
								 select new JMI12010_KEI_Member
								 {
									 印刷グループID = 2,
									 経費ID = 9999999,
									 経費項目名 = "限界利益",
								 }).AsQueryable();
						a = a.Take(1);
						keilist = keilist.Union(a.AsQueryable());

					}

					//乗務員経費
					keilist = keilist.Union(from m07 in context.M07_KEI
											where m07.編集区分 == 0 && m07.経費区分 == 3
											orderby m07.経費項目ID
											select new JMI12010_KEI_Member
											{
												印刷グループID = 3,
												経費ID = m07.経費項目ID,
												経費項目名 = m07.経費項目名,
											}).AsQueryable();

					//乗務員経費小計
					//bl = (from m07 in context.M07_KEI select m07).Any(c => c.編集区分 == 0 && c.経費区分 == 3);

					//if (bl)
					//{
					//    var a = (from m07 in context.M07_KEI
					//             select new JMI12010_KEI_Member
					//             {
					//                 印刷グループID = 3,
					//                 経費ID = 9999999,
					//                 経費項目名 = "【 小 計 】",
					//             }).AsQueryable();
					//    a = a.Take(1);
					//    keilist = keilist.Union(a.AsQueryable());

					//}

					//その他経費
					keilist = keilist.Union(from m07 in context.M07_KEI
											where m07.編集区分 == 0 && m07.固定変動区分 == 0 && m07.経費区分 != 3
											orderby m07.経費項目ID
											select new JMI12010_KEI_Member
											{
												印刷グループID = 4,
												経費ID = m07.経費項目ID,
												経費項目名 = m07.経費項目名,
											}).AsQueryable();

					keilist = keilist.Distinct();

					//その他経費小計
					//bl = (from m07 in context.M07_KEI select m07).Any(c => c.編集区分 == 0 && c.固定変動区分 == 0 && c.経費区分 != 3);

					//if (bl)
					//{
					//    var a = (from m07 in context.M07_KEI
					//             select new JMI12010_KEI_Member
					//             {
					//                 印刷グループID = 4,
					//                 経費ID = 9999999,
					//                 経費項目名 = "【 小 計 】",
					//             }).AsQueryable();
					//    a = a.Take(1);
					//    keilist = keilist.Union(a.AsQueryable());

					//}

					keilist = keilist.OrderBy(c => c.印刷グループID).ThenBy(c => c.経費ID);





					//経費取得
					var prelistz = (from car in carlistz
									from kei in keilist
									orderby car.車輌ID, kei.印刷グループID, kei.経費ID
									select new SRY11010_PRELIST_Member
									{
										印刷グループID = kei.印刷グループID,
										経費ID = kei.経費ID,
										経費項目名 = kei.経費項目名,
										車輌ID = car.車輌ID,
										車輌KEY = car.車輌KEY,
										車種ID = car.車種ID,
										//金額 = s14sgroup.Where(c => c.経費項目ID == kei.経費ID).Select(c => c.金額).FirstOrDefault() == null ? 0 : s14sgroup.Where(c => c.経費項目ID == kei.経費ID).Select(c => c.金額).FirstOrDefault(),
									}).ToList();

					var prelist = (from pre in prelistz
								   join s14 in context.S14_CARSB.Where(c => c.集計年月 == i年月) on new { a = pre.車輌KEY, b = pre.経費ID } equals new { a = s14.車輌KEY, b = s14.経費項目ID } into s14Group
								   //from s14g in s14Group.DefaultIfEmpty()
								   select new SRY11010_PRELIST_Member
								   {
									   印刷グループID = pre.印刷グループID,
									   経費ID = pre.経費ID,
									   経費項目名 = pre.経費項目名,
									   車輌ID = pre.車輌ID,
									   車輌KEY = pre.車輌KEY,
									   車種ID = pre.車種ID,
									   金額 = s14Group.Select(c => c.金額).Sum(),

								   }).AsQueryable();

					prelist = prelist.Union(from pre in prelist.Where(c => c.印刷グループID != 2)
											group pre by new { pre.車輌KEY, pre.車輌ID, pre.車種ID, pre.印刷グループID } into preGroup
											select new SRY11010_PRELIST_Member
											{
												印刷グループID = preGroup.Key.印刷グループID,
												経費ID = 9999999,
												経費項目名 = "【 小 計 】",
												車輌ID = preGroup.Key.車輌ID,
												車輌KEY = preGroup.Key.車輌KEY,
												車種ID = preGroup.Key.車種ID,
												金額 = preGroup.Select(c => c.金額).Sum(),

											}).AsQueryable();

					//合計追加
					if (prelist.Any())
					{
						prelist = prelist.Union(from pre in prelist
												group pre by new { pre.印刷グループID, pre.経費ID, pre.経費項目名 } into preGroup
												select new SRY11010_PRELIST_Member
												{
													印刷グループID = preGroup.Key.印刷グループID,
													経費ID = preGroup.Key.経費ID,
													経費項目名 = preGroup.Key.経費項目名,
													車輌ID = 999999,
													車輌KEY = 999999,
													車種ID = 9999,
													金額 = preGroup.Select(c => c.金額).Sum(),

												}).AsQueryable();
					}

					prelist = prelist.Distinct();
					prelist = prelist.OrderBy(c => c.車輌ID).ThenBy(c => c.印刷グループID);

					var carlist = (from car in carlistzz
								   join pre in prelist.Where(c => c.印刷グループID != 2) on car.車輌KEY equals pre.車輌KEY into preGroup
								   select new SRY11010g_Member
								   {
									   車輌KEY = car.車輌KEY,
									   車輌ID = car.車輌ID,
									   車種ID = car.車種ID,
									   車輌番号 = car.車輌番号,
									   主乗務員 = car.主乗務員,
									   車種 = car.車種,
									   運送収入 = car.運送収入,

									   一般管理費 = car.一般管理費,

									   稼働日数 = car.稼働日数,
									   実車KM = car.実車KM,
									   空車KM = car.走行KM - car.実車KM,
									   走行KM = car.走行KM,
									   燃料L = car.燃料L,
									   燃費 = car.燃料L == 0 ? 0 : (decimal)car.走行KM / car.燃料L,
									   収入1KM = car.収入1KM,
									   限界利益 = car.運送収入 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額).Sum(),
									   小計1 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額).Sum() == null ? 0 : preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額).Sum(),
									   小計2 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額).Sum() == null ? 0 : preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額).Sum(),
									   小計3 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額).Sum() == null ? 0 : preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額).Sum(),

								   }).AsQueryable();

					carlist = (from car in carlist
							   group car by new { car.車種ID, car.車種 } into carGroup
							   select new SRY11010g_Member
							   {
								   車種ID = carGroup.Key.車種ID,
								   台数 = carGroup.Count(),
								   車種 = carGroup.Key.車種,
								   運送収入 = carGroup.Select(c => c.運送収入).Sum(),

								   一般管理費 = carGroup.Select(c => c.一般管理費).Sum(),

								   稼働日数 = carGroup.Select(c => c.稼働日数).Sum(),
								   実車KM = carGroup.Select(c => c.実車KM).Sum(),
								   空車KM = carGroup.Select(c => c.走行KM).Sum() - carGroup.Select(c => c.実車KM).Sum(),
								   走行KM = carGroup.Select(c => c.走行KM).Sum(),
								   燃料L = carGroup.Select(c => c.燃料L).Sum(),
								   燃費 = carGroup.Select(c => c.燃料L).Sum() == 0 ? 0 : (decimal)(carGroup.Select(c => c.走行KM).Sum() / carGroup.Select(c => c.燃料L).Sum()),
								   収入1KM = carGroup.Select(c => c.走行KM).Sum() == null ? 0 : carGroup.Select(c => c.走行KM).Sum() == 0 ? 0 : (decimal)(carGroup.Select(c => c.運送収入).Sum() / carGroup.Select(c => c.走行KM).Sum()),
								   限界利益 = carGroup.Select(c => c.限界利益).Sum(),
								   小計1 = carGroup.Select(c => c.小計1).Sum(),
								   小計2 = carGroup.Select(c => c.小計2).Sum(),
								   小計3 = carGroup.Select(c => c.小計3).Sum(),

							   }).AsQueryable();

					var carlistzzz = carlist.ToList();
					carlistzzz.Add(new SRY11010g_Member
					{
						車種ID = 9999,
						台数 = carlist.Sum(c => c.台数),
						車種 = "【 合 計 】",
						運送収入 = carlist.Sum(c => c.運送収入),

						一般管理費 = carlist.Sum(c => c.一般管理費),

						稼働日数 = carlist.Sum(c => c.稼働日数),
						実車KM = carlist.Sum(c => c.実車KM),
						空車KM = carlist.Sum(c => c.走行KM) - carlist.Sum(c => c.実車KM),
						走行KM = carlist.Sum(c => c.走行KM),
						燃料L = carlist.Sum(c => c.燃料L),
						燃費 = carlist.Sum(c => c.燃料L) == 0 ? 0 : (decimal)carlist.Sum(c => c.走行KM) / carlist.Sum(c => c.燃料L),
						収入1KM = carlist.Sum(c => c.収入1KM),
						限界利益 = carlist.Sum(c => c.限界利益),
						小計1 = carlist.Sum(c => c.小計1),
						小計2 = carlist.Sum(c => c.小計2),
						小計3 = carlist.Sum(c => c.小計3),
					});

					////車種From処理　Min値
					//if (!string.IsNullOrEmpty(s車種From))
					//{
					//    int i車種From = AppCommon.IntParse(s車種From);
					//    carlistz = carlistz.Where(c => c.車種ID >= i車種From);
					//}

					////車種To処理　Max値
					//if (!string.IsNullOrEmpty(s車種To))
					//{
					//    int i車種TO = AppCommon.IntParse(s車種To);
					//    carlistz = carlistz.Where(c => c.車種ID <= i車種TO);
					//}

					//if (i車種List.Length > 0)
					//{
					//    var intCause = i車種List;



					prelist = (from pre in prelist
							   group pre by new { pre.車種ID, pre.印刷グループID, pre.経費ID, pre.経費項目名 } into preGroup
							   select new SRY11010_PRELIST_Member
							   {
								   印刷グループID = preGroup.Key.印刷グループID,
								   経費ID = preGroup.Key.経費ID,
								   経費項目名 = preGroup.Key.経費項目名,
								   車種ID = preGroup.Key.車種ID,
								   台数 = preGroup.Count(),
								   金額 = preGroup.Select(c => c.金額).Sum(),

							   }).AsQueryable();
					prelist = prelist.Distinct();
					prelist = prelist.OrderBy(c => c.車種ID).ThenBy(c => c.印刷グループID).ThenBy(c => c.経費ID);

					//var prelistz = (from p in prelist
					//               join s14s in context.S14_CARSB.Where(c => c.集計年月 == i年月) on new { a = p.経費ID, b = p.車輌KEY } equals new { a = s14s.経費項目ID, b = s14s.車輌KEY } into s14sgroup
					//               from s14sg in s14sgroup.DefaultIfEmpty()
					//               orderby p.車輌ID, p.印刷グループID, p.経費ID
					//               select new SRY11010_PRELIST_Member
					//               {
					//                   印刷グループID = p.印刷グループID,
					//                   経費ID = p.経費ID,
					//                   経費項目名 = p.経費項目名,
					//                   車輌ID = p.車輌ID,
					//                   車輌KEY = p.車輌KEY,
					//                   金額 = s14sg.金額 == null ? 0 : s14sg.金額,
					//               }).AsQueryable();


					var listprelist = prelist.ToList();


					var prt_GROPID1 = listprelist.Where(c => c.印刷グループID == 1).Select(c => c.経費ID);
					var prt_GROPID3 = listprelist.Where(c => c.印刷グループID == 3).Select(c => c.経費ID);
					var prt_GROPID4 = listprelist.Where(c => c.印刷グループID == 4).Select(c => c.経費ID);
					prt_GROPID1 = prt_GROPID1.Distinct();
					prt_GROPID3 = prt_GROPID3.Distinct();
					prt_GROPID4 = prt_GROPID4.Distinct();

					var prt_GROPID = listprelist.Select(c => c.経費ID);
					prt_GROPID = prt_GROPID.Distinct();


					//CSVデータ作成
					int cnt = 0;

					DataTable printdata = new DataTable();
					printdata.Columns.Add("車種ID", typeof(Int32));
					printdata.Columns.Add("車種番号".ToString(), typeof(String));
					printdata.Columns.Add("台数".ToString(), typeof(decimal));
					printdata.Columns.Add("運送収入".ToString(), typeof(decimal));

					if (carlistzzz == null)
					{
						return null;
					}

					//列作成
					int id = 1;
					foreach (SRY11010g_Member a in carlistzzz)
					{
						id = (int)a.車種ID;
						break;
					}
					List<SRY11010_PRELIST_Member> plist = (from p in prelist
														   where p.車種ID == id
														   select p).ToList();
					int syoukei_cnt = 0;
					foreach (SRY11010_PRELIST_Member row in plist)
					{
						if (row.経費項目名.ToString() == "【 小 計 】")
						{
							printdata.Columns.Add(row.経費項目名.ToString() + syoukei_cnt.ToString(), typeof(decimal));
							syoukei_cnt += 1;
						}
						else
						{
							var coltyp = new DataColumn(row.経費項目名.ToString(), typeof(decimal));
							coltyp.AllowDBNull = true;
							//printdata.Columns[1].AllowDBNull = true;
							printdata.Columns.Add(coltyp);
						}
					}
					var coltyp2 = new DataColumn("車輌直接費", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("車輌直接益", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("一般管理費", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("営業利益", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("稼働日数", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("実車KM", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("空車KM", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("走行KM", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("燃料L", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("燃費", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("収入1KM", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("原価1KM", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);


					//データ挿入
					int rowcnt = 0;
					foreach (SRY11010g_Member row in carlistzzz)
					{
						printdata.Rows.Add();
						var list = prelist.Where(c => c.車種ID == row.車種ID);
						printdata.Rows[rowcnt][0] = row.車種ID;
						printdata.Rows[rowcnt][1] = row.車輌番号;
						printdata.Rows[rowcnt][2] = row.台数;
						printdata.Rows[rowcnt][3] = row.運送収入;
						//syaryoutyokusetuhi = (decimal)(row.小計1 + row.小計2 + row.小計3);

						decimal syaryoutyokusetuhi = 0;
						int colcnt = 4;
						foreach (SRY11010_PRELIST_Member row2 in list)
						{
							printdata.Rows[rowcnt][colcnt] = row2.金額;
							if (row2.経費項目名 == "限界利益")
							{
								printdata.Rows[rowcnt][colcnt] = row.限界利益;
							}
							if (row2.経費項目名 == "【 小 計 】")
							{
								syaryoutyokusetuhi += row2.金額;
							}

							colcnt += 1;
						}
						printdata.Rows[rowcnt][colcnt] = syaryoutyokusetuhi;
						colcnt += 1;
						printdata.Rows[rowcnt][colcnt] = row.運送収入 == null ? 0 - syaryoutyokusetuhi : row.運送収入 - syaryoutyokusetuhi;
						colcnt += 1;
						if (row.一般管理費 == null)
						{
							printdata.Rows[rowcnt][colcnt] = DBNull.Value;
						}
						else
						{
							printdata.Rows[rowcnt][colcnt] = row.一般管理費;
						}
						colcnt += 1;
						if ((row.運送収入 - syaryoutyokusetuhi - row.一般管理費) == null)
						{
							printdata.Rows[rowcnt][colcnt] = DBNull.Value;
						}
						else
						{
							printdata.Rows[rowcnt][colcnt] = row.運送収入 - syaryoutyokusetuhi - row.一般管理費;
						}
						colcnt += 1;
						if (row.稼働日数 == null)
						{
							printdata.Rows[rowcnt][colcnt] = DBNull.Value;
						}
						else
						{
							printdata.Rows[rowcnt][colcnt] = row.稼働日数;
						}
						colcnt += 1;
						if (row.実車KM == null)
						{
							printdata.Rows[rowcnt][colcnt] = DBNull.Value;
						}
						else
						{
							printdata.Rows[rowcnt][colcnt] = row.実車KM;
						}
						colcnt += 1;
						if (row.空車KM == null)
						{
							printdata.Rows[rowcnt][colcnt] = DBNull.Value;
						}
						else
						{
							printdata.Rows[rowcnt][colcnt] = row.空車KM;
						}
						colcnt += 1;
						if (row.走行KM == null)
						{
							printdata.Rows[rowcnt][colcnt] = DBNull.Value;
						}
						else
						{
							printdata.Rows[rowcnt][colcnt] = row.走行KM;
						}
						colcnt += 1;
						printdata.Rows[rowcnt][colcnt] = row.燃料L;
						colcnt += 1;
						printdata.Rows[rowcnt][colcnt] = Math.Round(row.燃費, 1, MidpointRounding.AwayFromZero);
						colcnt += 1;
						printdata.Rows[rowcnt][colcnt] = Math.Round(row.収入1KM, 0, MidpointRounding.AwayFromZero);
						colcnt += 1;
						printdata.Rows[rowcnt][colcnt] = (row.走行KM == null || row.走行KM == 0) ? 0 : Math.Round((decimal)((row.一般管理費 + syaryoutyokusetuhi) / row.走行KM), 2, MidpointRounding.AwayFromZero);
						colcnt += 1;

						rowcnt += 1;
					}


					return printdata;

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