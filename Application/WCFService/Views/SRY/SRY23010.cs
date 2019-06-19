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

    #region SRY23010_Member
    [DataContract]
    public class SRY23010_Member
    {
        //表示用データ
        [DataMember]
        public string 種別 { get; set; }
        [DataMember]
        public int 保有台数 { get; set; }
        [DataMember]
        public decimal 走行距離 { get; set; }
        [DataMember]
        public decimal? 燃料使用量 { get; set; }
        [DataMember]
        public decimal? 燃費 { get; set; }
        [DataMember]
        public decimal CO2排出係数1 { get; set; }
        [DataMember]
        public decimal CO2排出係数2 { get; set; }
        [DataMember]
        public decimal 二酸化炭素排出量 { get; set; }

        //算術用データ(非表示)
        [DataMember]
        public int? G車種ID { get; set; }
        [DataMember]
        public decimal 排出走行1 { get; set; }
        [DataMember]
        public decimal 排出走行2 { get; set; }
        [DataMember]
        public int? 事業用区分 { get; set; }
        [DataMember]
        public int? ディーゼル区分 { get; set; }
        [DataMember]
        public int? 小型普通区分 { get; set; }
        [DataMember]
        public int? 低公害車区分 { get; set; }
        [DataMember]
        public int? CO2区分 { get; set; }
        [DataMember]
        public int? 部門ID { get; set; }
    }

    public class SRY23010_TotalMember
    {
        [DataMember]
        public string 種別 { get; set; }
        [DataMember]
        public int 保有台数 { get; set; }
        [DataMember]
        public decimal 走行距離 { get; set; }
        [DataMember]
        public decimal? 燃料使用量 { get; set; }
        [DataMember]
        public decimal? 燃費 { get; set; }
        [DataMember]
        public decimal CO2排出係数1 { get; set; }
        [DataMember]
        public decimal CO2排出係数2 { get; set; }
        [DataMember]
        public decimal 二酸化炭素排出量 { get; set; }
        [DataMember]
        public int? 事業用区分 { get; set; }
        [DataMember]
        public int? ディーゼル区分 { get; set; }
    }

    public class SRY23010_SyaCntMember
    {
        [DataMember]
        public int 車輌ID { get; set; }
        [DataMember]
        public int 車輌KEY { get; set; }
        [DataMember]
        public int? G車種ID { get; set; }
        [DataMember]
        public int? CO2区分 { get; set; }
        [DataMember]
        public int? 規制区分ID { get; set; }
        [DataMember]
        public int? 部門ID { get; set; }
    }

    public class SRY23020_Member
    {
        [DataMember]
        public int? 規制区分ID { get; set; }
        [DataMember]
        public string 種別 { get; set; }
        [DataMember]
        public int 保有台数 { get; set; }
    }

    public class SRY23010_GDATE_Member
    {
        [DataMember] public int? G期首月日 { get; set; }
        [DataMember] public int? G期末月日 { get; set; }
    }

    #endregion

    public class SRY23010
    {
        #region SRY23010

        public List<SRY23010_TotalMember> SRY23010_GetData(DateTime? p集計期間From, DateTime? p集計期間To, int p部門コード)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<SRY23010_TotalMember> retList = new List<SRY23010_TotalMember>();
                context.Connection.Open();
                try
                {
                    var query = (from m14 in context.M14_GSYA.Where(m14 => m14.削除日付 == null)
                                 select new SRY23010_Member
                                 {
                                     G車種ID = m14.G車種ID,
                                     種別 = m14.G車種名,
                                     CO2排出係数1 = (decimal)m14.CO2排出係数１,
                                     CO2排出係数2 = (decimal)m14.CO2排出係数２,
                                     事業用区分 = m14.事業用区分,
                                     ディーゼル区分 = m14.ディーゼル区分,
                                     小型普通区分 = m14.小型普通区分,
                                     低公害車区分 = m14.低公害区分,
                                 }).ToList();

                    var syacnt = (from m05 in context.M05_CAR
                                  where m05.削除日付 == null && m05.廃車区分 == 0 || (m05.廃車区分 == 1 && m05.廃車日 >= p集計期間To)
                                  select new SRY23010_SyaCntMember
                                  {
                                      車輌ID = m05.車輌ID,
                                      車輌KEY = m05.車輌KEY,
                                      G車種ID = m05.G車種ID,
                                      CO2区分 = m05.CO2区分,
                                      部門ID = m05.自社部門ID,
                                  }).ToList();

                    if (p部門コード != 0)
                    {
                        syacnt = syacnt.Where(m05 => m05.部門ID == p部門コード).OrderByDescending(m05 => m05.規制区分ID).ToList(); ;
                    }

                    var kmcnt = (from m05 in syacnt
                                 join t02 in context.T02_UTRN.Where(t02 => t02.実運行日開始 >= p集計期間From && t02.実運行日終了 <= p集計期間To && t02.明細行 == 1) on m05.車輌KEY equals t02.車輌KEY into Group
                                 from kmGroup in Group
                                 group new { kmGroup, m05 } by new { m05.G車種ID, m05.CO2区分 } into grGroup
                                 select new SRY23010_Member
                                 {
                                     G車種ID = grGroup.Key.G車種ID,
                                     CO2区分 = grGroup.Key.CO2区分,
                                     排出走行1 = p部門コード == 0 ? grGroup.Sum(t02 => t02.kmGroup.走行ＫＭ) : grGroup.Where(t02 => t02.kmGroup.自社部門ID == p部門コード).Sum(t02 => t02.kmGroup.走行ＫＭ),
                                     排出走行2 = p部門コード == 0 ? grGroup.Sum(t02 => t02.kmGroup.走行ＫＭ) : grGroup.Where(t02 => t02.kmGroup.自社部門ID == p部門コード).Sum(t02 => t02.kmGroup.走行ＫＭ),

                                 }).ToList();
                    kmcnt.RemoveAll(c => c.G車種ID == 0);

                    var gascnt = (from m05 in syacnt
                                  join t03 in context.T03_KTRN.Where(t03 => t03.経費発生日 >= p集計期間From && t03.経費発生日 <= p集計期間To && t03.経費項目ID == 401) on m05.車輌KEY equals t03.車輌ID into Group
                                  from gasGroup in Group
                                  group new { gasGroup, m05 } by new { m05.G車種ID } into grGroup
                                  select new SRY23010_Member
                                  {
                                      G車種ID = grGroup.Key.G車種ID,
                                      燃料使用量 = p部門コード == 0 ? grGroup.Sum(t03 => t03.gasGroup.数量) : grGroup.Where(t03 => t03.gasGroup.自社部門ID == p部門コード).Sum(t03 => t03.gasGroup.数量),
                                  }).ToList();

                    int Count = query.Count() + 6;
                    List<SRY23010_Member> queryLIST = query.ToList();
                    List<SRY23010_TotalMember> totalList = new List<SRY23010_TotalMember>();
                    List<SRY23010_SyaCntMember> syacntList = syacnt.ToList();
                    List<SRY23010_Member> kmcntLIST = kmcnt.ToList();
                    List<SRY23010_Member> gascntLIST = gascnt.ToList();

                    if (queryLIST.Count == 0)
                    {
                        return totalList;
                    }

                    //初期値設定
                    int x = 0; int y = 0;

                    //*****   保有台数計算   ******//
                    foreach (var Row in queryLIST)
                    {
                        foreach (var Sya in syacntList)
                        {
                            if (queryLIST[x].G車種ID == syacntList[y].G車種ID)
                            {
                                queryLIST[x].保有台数 += 1;
                            }
                            y++;
                        }
                        y = 0;
                        x++;
                    }


                    //*****   距離   *****//
                    x = 0; y = 0;
                    foreach (var Row in queryLIST)
                    {
                        foreach (var Km in kmcntLIST)
                        {
                            if (queryLIST[x].G車種ID == kmcntLIST[y].G車種ID)
                            {
                                if (kmcntLIST[y].CO2区分 == 1)
                                {
                                    queryLIST[x].走行距離 += kmcntLIST[y].排出走行1;
                                }
                                else
                                {
                                    queryLIST[x].走行距離 += kmcntLIST[y].排出走行2;
                                }

                                if (kmcntLIST[y].CO2区分 == 1)
                                {
                                    queryLIST[x].排出走行1 += kmcntLIST[y].排出走行1;
                                }
                                else
                                {
                                    queryLIST[x].排出走行2 += kmcntLIST[y].排出走行2;
                                }
                            }
                            y++;
                        }
                        y = 0;
                        x++;
                    }

                    //*****   燃料使用量   *****//
                    x = 0; y = 0;
                    foreach (var Row in queryLIST)
                    {
                        foreach (var Km in gascntLIST)
                        {
                            if (queryLIST[x].G車種ID == gascntLIST[y].G車種ID)
                            {
                                queryLIST[x].燃料使用量 = gascntLIST[y].燃料使用量;
                            }
                            y++;
                        }
                        y = 0;
                        x++;
                    }

                    //*****   燃費   *****//
                    x = 0; y = 0;
                    foreach (var Row in queryLIST)
                    {
                        if (queryLIST[x].走行距離 != 0 && queryLIST[x].燃料使用量 != 0)
                        {
                            queryLIST[x].燃費 = queryLIST[x].走行距離 / queryLIST[x].燃料使用量;
                        }
                        x++;
                    }

                    //*****   二酸化炭素排出量   *****//
                    x = 0; y = 0;
                    foreach (var Row in queryLIST)
                    {
                        foreach (var Km in kmcntLIST)
                        {
                            if (queryLIST[x].G車種ID == kmcntLIST[y].G車種ID)
                            {
                                if (kmcntLIST[y].CO2区分 == 1)
                                {
                                    queryLIST[x].二酸化炭素排出量 += kmcntLIST[y].排出走行1 * queryLIST[x].CO2排出係数1;
                                }
                                else
                                {
                                    queryLIST[x].二酸化炭素排出量 += kmcntLIST[y].排出走行2 * queryLIST[x].CO2排出係数2;
                                }
                            }
                            y++;
                        }
                        y = 0;
                        x++;
                    }

                    //*****   順序変更   *****//
                    queryLIST = queryLIST.OrderBy(c => c.事業用区分).ThenBy(c => c.ディーゼル区分).ThenBy(c => c.G車種ID).ToList();

                    //*****合計 * ****//
                    x = 0; y = 0;
                    int z = 0;
                    foreach (var Row in queryLIST)
                    {
                        if (queryLIST[x].事業用区分 == 0 && queryLIST[x].ディーゼル区分 == 0)
                        {
                            totalList.Add(new SRY23010_TotalMember() { 種別 = "", 保有台数 = 0, 走行距離 = 0, 燃料使用量 = 0, 燃費 = 0, CO2排出係数1 = 0, CO2排出係数2 = 0, 二酸化炭素排出量 = 0 });
                            totalList[z].種別 = queryLIST[x].種別;
                            totalList[z].保有台数 = queryLIST[x].保有台数;
                            totalList[z].走行距離 = queryLIST[x].走行距離;
                            totalList[z].燃料使用量 = queryLIST[x].燃料使用量;
                            totalList[z].燃費 = queryLIST[x].燃費;
                            totalList[z].CO2排出係数1 = queryLIST[x].CO2排出係数1;
                            totalList[z].CO2排出係数2 = queryLIST[x].CO2排出係数2;
                            totalList[z].二酸化炭素排出量 = queryLIST[x].二酸化炭素排出量;
                            totalList[z].事業用区分 = queryLIST[x].事業用区分;
                            totalList[z].ディーゼル区分 = queryLIST[x].ディーゼル区分;
                        }
                        x++;
                        z++;
                    }
                    x = 0;
                    int tcnt = totalList.Count;
                    z = tcnt;
                    if (totalList.Count != 0)
                    {
                        totalList.Add(new SRY23010_TotalMember() { 種別 = "", 保有台数 = 0, 走行距離 = 0, 燃料使用量 = 0, 燃費 = 0, CO2排出係数1 = 0, CO2排出係数2 = 0, 二酸化炭素排出量 = 0 });
                        totalList[tcnt].種別 = "小計";
                        for (int i = 0; queryLIST.Count > i; i++)
                        {
                            if (queryLIST[i].事業用区分 == 0 && queryLIST[i].ディーゼル区分 == 0)
                            {
                                totalList[z].保有台数 += queryLIST[i].保有台数;
                                totalList[z].走行距離 += queryLIST[i].走行距離;
                                totalList[z].燃料使用量 += queryLIST[i].燃料使用量;
                                totalList[z].二酸化炭素排出量 += queryLIST[i].二酸化炭素排出量;
                                totalList[z].事業用区分 = 9;
                                totalList[z].ディーゼル区分 = 9;
                            }
                        }
                    }
                        x = 0;
                        z++;
                    
                    foreach (var Row in queryLIST)
                    {
                        if (queryLIST[x].事業用区分 == 0 && queryLIST[x].ディーゼル区分 == 1)
                        {
                            totalList.Add(new SRY23010_TotalMember() { 種別 = "", 保有台数 = 0, 走行距離 = 0, 燃料使用量 = 0, 燃費 = 0, CO2排出係数1 = 0, CO2排出係数2 = 0, 二酸化炭素排出量 = 0 });
                            totalList[z].種別 = queryLIST[x].種別;
                            totalList[z].保有台数 = queryLIST[x].保有台数;
                            totalList[z].走行距離 = queryLIST[x].走行距離;
                            totalList[z].燃料使用量 = queryLIST[x].燃料使用量;
                            totalList[z].燃費 = queryLIST[x].燃費;
                            totalList[z].CO2排出係数1 = queryLIST[x].CO2排出係数1;
                            totalList[z].CO2排出係数2 = queryLIST[x].CO2排出係数2;
                            totalList[z].二酸化炭素排出量 = queryLIST[x].二酸化炭素排出量;
                            totalList[z].事業用区分 = queryLIST[x].事業用区分;
                            totalList[z].ディーゼル区分 = queryLIST[x].ディーゼル区分;
                            z++;
                        }
                        x++;
                    }
                    tcnt = totalList.Count;
                    z = tcnt;
                    if (totalList.Count != 0)
                    {
                        totalList.Add(new SRY23010_TotalMember() { 種別 = "", 保有台数 = 0, 走行距離 = 0, 燃料使用量 = 0, 燃費 = 0, CO2排出係数1 = 0, CO2排出係数2 = 0, 二酸化炭素排出量 = 0 });
                        totalList[tcnt].種別 = "小計";
                        for (int i = 0; queryLIST.Count > i; i++)
                        {
                            if (queryLIST[i].事業用区分 == 0 && queryLIST[i].ディーゼル区分 == 1)
                            {
                                totalList[z].保有台数 += queryLIST[i].保有台数;
                                totalList[z].走行距離 += queryLIST[i].走行距離;
                                totalList[z].燃料使用量 += queryLIST[i].燃料使用量;
                                totalList[z].二酸化炭素排出量 += queryLIST[i].二酸化炭素排出量;
                                totalList[z].事業用区分 = 9;
                                totalList[z].ディーゼル区分 = 9;
                            }
                        }
                    }
                        tcnt = totalList.Count;
                        z = tcnt;
                        totalList.Add(new SRY23010_TotalMember() { 種別 = "", 保有台数 = 0, 走行距離 = 0, 燃料使用量 = 0, 燃費 = 0, CO2排出係数1 = 0, CO2排出係数2 = 0, 二酸化炭素排出量 = 0 });
                        totalList[tcnt].種別 = "事業用計";
                        for (int i = 0; totalList.Count > i; i++)
                        {
                            if (totalList[i].種別 == "小計")
                            {
                                totalList[z].保有台数 += totalList[i].保有台数;
                                totalList[z].走行距離 += totalList[i].走行距離;
                                totalList[z].燃料使用量 += totalList[i].燃料使用量;
                                totalList[z].二酸化炭素排出量 += totalList[i].二酸化炭素排出量;
                                totalList[z].事業用区分 = 9;
                                totalList[z].ディーゼル区分 = 9;
                            }
                        }
                        
                        z++;
                    
                    x = 0;
                    

                    foreach (var Row in queryLIST)
                    {
                        if (queryLIST[x].事業用区分 == 1)
                        {
                            totalList.Add(new SRY23010_TotalMember() { 種別 = "", 保有台数 = 0, 走行距離 = 0, 燃料使用量 = 0, 燃費 = 0, CO2排出係数1 = 0, CO2排出係数2 = 0, 二酸化炭素排出量 = 0 });
                            totalList[z].種別 = queryLIST[x].種別;
                            totalList[z].保有台数 = queryLIST[x].保有台数;
                            totalList[z].走行距離 = queryLIST[x].走行距離;
                            totalList[z].燃料使用量 = queryLIST[x].燃料使用量;
                            totalList[z].燃費 = queryLIST[x].燃費;
                            totalList[z].CO2排出係数1 = queryLIST[x].CO2排出係数1;
                            totalList[z].CO2排出係数2 = queryLIST[x].CO2排出係数2;
                            totalList[z].二酸化炭素排出量 = queryLIST[x].二酸化炭素排出量;
                            totalList[z].事業用区分 = queryLIST[x].事業用区分;
                            totalList[z].ディーゼル区分 = queryLIST[x].ディーゼル区分;
                            z++;
                        }
                        x++;
                    }
                    tcnt = totalList.Count;
                    z = tcnt;
                    totalList.Add(new SRY23010_TotalMember() { 種別 = "", 保有台数 = 0, 走行距離 = 0, 燃料使用量 = 0, 燃費 = 0, CO2排出係数1 = 0, CO2排出係数2 = 0, 二酸化炭素排出量 = 0 });
                    totalList[tcnt].種別 = "自家用計";
                    for (int i = 0; queryLIST.Count > i; i++)
                    {
                        if (queryLIST[i].事業用区分 == 1)
                        {
                            totalList[z].保有台数 += queryLIST[i].保有台数;
                            totalList[z].走行距離 += queryLIST[i].走行距離;
                            totalList[z].燃料使用量 += queryLIST[i].燃料使用量;
                            totalList[z].二酸化炭素排出量 += queryLIST[i].二酸化炭素排出量;
                            totalList[z].事業用区分 = 9;
                            totalList[z].ディーゼル区分 = 9;
                        }
                    }
                    tcnt = totalList.Count;
                    z = tcnt;
                    totalList.Add(new SRY23010_TotalMember() { 種別 = "", 保有台数 = 0, 走行距離 = 0, 燃料使用量 = 0, 燃費 = 0, CO2排出係数1 = 0, CO2排出係数2 = 0, 二酸化炭素排出量 = 0 });
                    totalList[tcnt].種別 = "合計";
                    for (int i = 0; queryLIST.Count > i; i++)
                    {
                            totalList[z].保有台数 += queryLIST[i].保有台数;
                            totalList[z].走行距離 += queryLIST[i].走行距離;
                            totalList[z].燃料使用量 += queryLIST[i].燃料使用量;
                            totalList[z].二酸化炭素排出量 += queryLIST[i].二酸化炭素排出量;
                            totalList[z].事業用区分 = 9;
                            totalList[z].ディーゼル区分 = 9;
                    }


                    return totalList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        #endregion

        public List<SRY23020_Member> SRY23020_GetData(DateTime? p集計期間From, DateTime? p集計期間To, int p部門コード)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<SRY23020_Member> retList = new List<SRY23020_Member>();
                context.Connection.Open();
                try
                {
                    var query = (from m12 in context.M12_KIS.Where(m12 => m12.削除日付 == null)
                                 select new SRY23020_Member
                                 {
                                     規制区分ID = m12.規制区分ID,
                                     種別 = m12.規制名,
                                 }).OrderByDescending(m12 => m12.規制区分ID).ToList();

                    var syacnt = (from m05 in context.M05_CAR
                                  where m05.削除日付 == null && m05.廃車区分 == 0 || (m05.廃車区分 == 1 && m05.廃車日 >= p集計期間To)
                                  select new SRY23010_SyaCntMember
                                  {
                                      規制区分ID = m05.規制区分ID,
                                      部門ID = m05.自社部門ID
                                  }).OrderByDescending(m05 => m05.規制区分ID).ToList();

                    if (p部門コード != 0)
                    {
                        syacnt = syacnt.Where(m05 => m05.部門ID == p部門コード).OrderByDescending(m05 => m05.規制区分ID).ToList(); ;
                    }

                    List<SRY23020_Member> queryLIST = query.ToList();
                    List<SRY23010_SyaCntMember> syacntList = syacnt.ToList();
                    //初期値設定
                    int x = 0; int y = 0;

                    //*****   保有台数計算   ******//
                    foreach (var Row in queryLIST)
                    {
                        foreach (var Sya in syacntList)
                        {
                            if (queryLIST[x].規制区分ID == syacntList[y].規制区分ID)
                            {
                                queryLIST[x].保有台数 += 1;
                            }
                            y++;
                        }
                        y = 0;
                        x++;
                    }

                    return queryLIST;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// G期首・期末月日取得
        /// </summary>
        /// <returns></returns>
        public List<SRY23010_GDATE_Member> SRY23010_GDATE_GetData()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<SRY23010_GDATE_Member> retList = new List<SRY23010_GDATE_Member>();
                context.Connection.Open();
                try
                {
                    var query = (from m87 in context.M87_CNTL
                                 select new SRY23010_GDATE_Member
                                 {
                                     G期首月日 = m87.Ｇ期首月日,
                                     G期末月日 = m87.Ｇ期末月日,
                                 }).AsQueryable();
                    query.FirstOrDefault();
                    retList = query.ToList();
                    return retList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}