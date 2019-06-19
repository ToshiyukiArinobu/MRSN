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
    #region Jisya

    public class SRY14010_Jisya
    {
        [DataMember]
        public int 事業者番号 { get; set; }
        [DataMember]
        public string 自社住所 { get; set; }
        [DataMember]
        public string 事業社名 { get; set; }
        [DataMember]
        public string 代表社名 { get; set; }
        [DataMember]
        public string 電話番号 { get; set; }

    }
    #endregion

    #region Height

    public class SRY14010_Height
    {
        [DataMember]
        public decimal 利用運送 { get; set; }
    }

    #endregion

    #region Item

    public class SRY14010_Item
    {
        [DataMember]
        public decimal 営業収入 { get; set; }
    }

    #endregion

    #region Member2

    public class SRY14010_Member2
    {

        [DataMember]
        public int 運輸局 { get; set; }
        [DataMember]
        public int 車輌KEY { get; set; }
		[DataMember]
		public int 車輌ID { get; set; }
		[DataMember]
		public DateTime? 廃車日 { get; set; }
		[DataMember]
		public int 延実在日 { get; set; }
		[DataMember]
        public int 該当外_延実在庫 { get; set; }
        [DataMember]
        public int 該当外_延実働車 { get; set; }
        [DataMember]
        public int 該当外_走行 { get; set; }
        [DataMember]
        public int 該当外_実車 { get; set; }
        [DataMember]
        public decimal 該当外_実運送 { get; set; }
        [DataMember]
        public int 該当外_利用運送 { get; set; }
        [DataMember]
        public int 該当外_営業収入 { get; set; }
    }

    #endregion

    #region Member3

    public class SRY14010_Member3
    {

        [DataMember]
        public int 事業者番号 { get; set; }
        [DataMember]
        public string 自社住所 { get; set; }
        [DataMember]
        public string 事業社名 { get; set; }
        [DataMember]
        public string 代表社名 { get; set; }
        [DataMember]
        public string 電話番号 { get; set; }
        [DataMember]
        public DateTime 請求From { get; set; }
        [DataMember]
        public DateTime 請求To { get; set; }

        [DataMember]
        public string 事業用自動車 { get; set; }
        [DataMember]
        public string 従業員数 { get; set; }
        [DataMember]
        public string 運転者数 { get; set; }

        [DataMember]
        public int 運輸局 { get; set; }
        [DataMember]
        public int 車輌KEY { get; set; }
        [DataMember]
        public int 車輌ID { get; set; }
        [DataMember]
        public int 該当外_延実在庫 { get; set; }
        [DataMember]
        public int 該当外_延実働車 { get; set; }
        [DataMember]
        public int 該当外_走行 { get; set; }
        [DataMember]
        public int 該当外_実車 { get; set; }
        [DataMember]
        public decimal 該当外_実運送 { get; set; }
        [DataMember]
        public decimal 該当外_利用運送 { get; set; }
        [DataMember]
        public decimal 該当外_営業収入 { get; set; }

           //北海道
        
        [DataMember]
        public int 北海道_延実在庫 { get; set; }
        [DataMember]
        public int 北海道_延実働車 { get; set; }
        [DataMember]
        public int 北海道_走行 { get; set; }
        [DataMember]
        public int 北海道_実車 { get; set; }
        [DataMember]
        public decimal 北海道_実運送 { get; set; }
        [DataMember]
        public decimal 北海道_利用運送 { get; set; }
        [DataMember]
        public decimal 北海道_営業収入 { get; set; }
        
        //東北
        
        [DataMember]
        public int 東北_延実在庫 { get; set; }
        [DataMember]
        public int 東北_延実働車 { get; set; }
        [DataMember]
        public int 東北_走行 { get; set; }
        [DataMember]
        public int 東北_実車 { get; set; }
        [DataMember]
        public decimal 東北_実運送 { get; set; }
        [DataMember]
        public decimal 東北_利用運送 { get; set; }
        [DataMember]
        public decimal 東北_営業収入 { get; set; }
        
        //北陸信越
        
        [DataMember]
        public int　北陸信越_延実在庫 { get; set; }
        [DataMember]
        public int 北陸信越_延実働車 { get; set; }
        [DataMember]
        public int 北陸信越_走行 { get; set; }
        [DataMember]
        public int 北陸信越_実車 { get; set; }
        [DataMember]
        public decimal 北陸信越_実運送 { get; set; }
        [DataMember]
        public decimal 北陸信越_利用運送 { get; set; }
        [DataMember]
        public decimal 北陸信越_営業収入 { get; set; }
        
        //関東
        
        [DataMember]
        public int 関東_延実在庫 { get; set; }
        [DataMember]
        public int 関東_延実働車 { get; set; }
        [DataMember]
        public int 関東_走行 { get; set; }
        [DataMember]
        public int 関東_実車 { get; set; }
        [DataMember]
        public decimal 関東_実運送 { get; set; }
        [DataMember]
        public decimal 関東_利用運送 { get; set; }
        [DataMember]
        public decimal 関東_営業収入 { get; set; }

        //中部

        [DataMember]
        public int 中部_延実在庫 { get; set; }
        [DataMember]
        public int 中部_延実働車 { get; set; }
        [DataMember]
        public int 中部_走行 { get; set; }
        [DataMember]
        public int 中部_実車 { get; set; }
        [DataMember]
        public decimal 中部_実運送 { get; set; }
        [DataMember]
        public decimal 中部_利用運送 { get; set; }
        [DataMember]
        public decimal 中部_営業収入 { get; set; }

        //近畿

        [DataMember]
        public int 近畿_延実在庫 { get; set; }
        [DataMember]
        public int 近畿_延実働車 { get; set; }
        [DataMember]
        public int 近畿_走行 { get; set; }
        [DataMember]
        public int 近畿_実車 { get; set; }
        [DataMember]
        public decimal 近畿_実運送 { get; set; }
        [DataMember]
        public decimal 近畿_利用運送 { get; set; }
        [DataMember]
        public decimal 近畿_営業収入 { get; set; }

        //中国

        [DataMember]
        public int 中国_延実在庫 { get; set; }
        [DataMember]
        public int 中国_延実働車 { get; set; }
        [DataMember]
        public int 中国_走行 { get; set; }
        [DataMember]
        public int 中国_実車 { get; set; }
        [DataMember]
        public decimal 中国_実運送 { get; set; }
        [DataMember]
        public decimal 中国_利用運送 { get; set; }
        [DataMember]
        public decimal 中国_営業収入 { get; set; }

        //四国

        [DataMember]
        public int 四国_延実在庫 { get; set; }
        [DataMember]
        public int 四国_延実働車 { get; set; }
        [DataMember]
        public int 四国_走行 { get; set; }
        [DataMember]
        public int 四国_実車 { get; set; }
        [DataMember]
        public decimal 四国_実運送 { get; set; }
        [DataMember]
        public decimal 四国_利用運送 { get; set; }
        [DataMember]
        public decimal 四国_営業収入 { get; set; }        

        //九州

        [DataMember]
        public int 九州_延実在庫 { get; set; }
        [DataMember]
        public int 九州_延実働車 { get; set; }
        [DataMember]
        public int 九州_走行 { get; set; }
        [DataMember]
        public int 九州_実車 { get; set; }
        [DataMember]
        public decimal 九州_実運送 { get; set; }
        [DataMember]
        public decimal 九州_利用運送 { get; set; }
        [DataMember]
        public decimal 九州_営業収入 { get; set; }

        //沖縄

        [DataMember]
        public int 沖縄_延実在庫 { get; set; }
        [DataMember]
        public int 沖縄_延実働車 { get; set; }
        [DataMember]
        public int 沖縄_走行 { get; set; }
        [DataMember]
        public int 沖縄_実車 { get; set; }
        [DataMember]
        public decimal 沖縄_実運送 { get; set; }
        [DataMember]
        public decimal 沖縄_利用運送 { get; set; }
        [DataMember]
        public decimal 沖縄_営業収入 { get; set; }
    }

    #endregion

    #region SRY14010_Member_CSV

    public class SRY14010_Member_CSV
    {

        [DataMember]
        public int 事業者番号 { get; set; }
        [DataMember]
        public string 自社住所 { get; set; }
        [DataMember]
        public string 事業社名 { get; set; }
        [DataMember]
        public string 代表社名 { get; set; }
        [DataMember]
        public string 電話番号 { get; set; }

        [DataMember]
        public string 事業用自動車 { get; set; }
        [DataMember]
        public string 従業員数 { get; set; }
        [DataMember]
        public string 運転者数 { get; set; }

        [DataMember]
        public int 運輸局 { get; set; }
        [DataMember]
        public string 運輸局名 { get; set; }
        [DataMember]
        public int 延実在庫 { get; set; }
        [DataMember]
        public int 延実働車 { get; set; }
        [DataMember]
        public int 走行 { get; set; }
        [DataMember]
        public int 実車 { get; set; }
        [DataMember]
        public decimal 実運送 { get; set; }
        [DataMember]
        public decimal 利用運送 { get; set; }
        [DataMember]
        public decimal 営業収入 { get; set; }

    }

    #endregion

    #region SRY14010_Member2_CSV

    public class SRY14010_Member2_CSV
    {
        [DataMember]
        public int 運輸局 { get; set; }
        [DataMember]
        public int 該当外_延実在庫 { get; set; }
        [DataMember]
        public int 該当外_延実働車 { get; set; }
        [DataMember]
        public int 該当外_走行 { get; set; }
        [DataMember]
        public int 該当外_実車 { get; set; }
        [DataMember]
        public decimal 該当外_実運送 { get; set; }
        [DataMember]
        public int 該当外_利用運送 { get; set; }
        [DataMember]
        public int 該当外_営業収入 { get; set; }

    }

    #endregion
      


    public class SRY14010
    {
        #region SRY14010　印刷

        public List<SRY14010_Member3> SRY14010_GetDataHinList(string s車輌From, string s車輌To, int?[] i車輌List, int d作成年月From, int d作成年月To,
                                                             int iCmb_Value, int s自社コード, string s自動車数, string s従業員数, string s運転者数 , DateTime d請求From , DateTime d請求To)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {

                try
                {
                    List<SRY14010_Member3> retList = new List<SRY14010_Member3>();
                    context.Connection.Open();

                    TimeSpan t日数 = d請求To - d請求From;
                    int i日数 = Convert.ToInt32(t日数.Days + 1);
                    var query2 = (from m05 in context.M05_CAR.Where(c => c.削除日付 == null)
                                  join s14 in context.S14_CAR.Where(s14 => d作成年月From <= s14.集計年月 && d作成年月To >= s14.集計年月) on m05.車輌KEY equals s14.車輌KEY into s14Group
                                  select new SRY14010_Member2
                                  {
                                      運輸局 = m05.運輸局ID,
                                      車輌KEY = m05.車輌KEY,
                                      車輌ID = m05.車輌ID,
									  廃車日 = m05.廃車日,
                                      該当外_延実働車 = s14Group.Sum(c => c.稼動日数) == null ? 0 : s14Group.Sum(c => c.稼動日数),
                                      該当外_走行 = s14Group.Sum(c => c.走行ＫＭ) == null ? 0 : s14Group.Sum(c => c.走行ＫＭ),
                                      該当外_実車 = s14Group.Sum(c => c.実車ＫＭ) == null ? 0 : s14Group.Sum(c => c.実車ＫＭ),
                                      該当外_実運送 = s14Group.Sum(c => c.輸送屯数) == null ? 0 : s14Group.Sum(c => c.輸送屯数),

                                  }).AsQueryable();

                    if (!(string.IsNullOrEmpty(s車輌From + s車輌To) && i車輌List.Length == 0))
                    {
                        if (!string.IsNullOrEmpty(s車輌From))
                        {
                            int i車輌From = AppCommon.IntParse(s車輌From);
                            query2 = query2.Where(c => c.車輌ID >= i車輌From);
                        }

                        if (!string.IsNullOrEmpty(s車輌To))
                        {
                            int i車輌To = AppCommon.IntParse(s車輌To);
                            query2 = query2.Where(c => c.車輌ID <= i車輌To);
                        }
                    }
                    else
                    {
                        query2 = query2.Where(c => c.車輌ID > int.MinValue && c.車輌ID <= int.MaxValue);
                    }

					var query21 = query2.ToList();
					foreach (SRY14010_Member2 row in query21)
					{
						if (row.廃車日 == null || row.廃車日 > d請求To)
						{
							row.延実在日 = i日数;
						}
						else
						{
							if (row.廃車日 > d請求From )
							{
								TimeSpan 日数 = (DateTime)row.廃車日 - d請求From;
								row.延実在日 = 日数.Days + 1;
							}
						}
					}




                    //営業収入
                    var item = (from m01 in context.M01_TOK
                                join s01 in context.S01_TOKS on m01.得意先KEY equals s01.得意先KEY into Group
                                where m01.親子区分ID != 3 && m01.取引区分 != 3
                                select new SRY14010_Item
                                {
                                    営業収入 = Group.Where(c => c.集計年月 >= d作成年月From && c.集計年月 <= d作成年月To).Sum(c => c.締日売上金額 + c.締日通行料) == null ? 0 : Group.Where(c => c.集計年月 >= d作成年月From && c.集計年月 <= d作成年月To).Sum(c => c.締日売上金額 + c.締日通行料),
                                }).AsQueryable();
                    var item2 = (from itm in item
                                 select new SRY14010_Item
                                 {
                                     営業収入 = item.Sum(c => c.営業収入)
                                 }).Distinct().AsQueryable();

                    //利用運送
                    var heit = (from m01 in context.M01_TOK
                                join t01 in context.T01_TRN.Where(t01 => (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細番号 != 1)) on m01.得意先KEY equals t01.支払先KEY into Group
                                from t01Group in Group
                                group t01Group by new { t01Group.明細区分, t01Group.支払先KEY } into grGroup
                                where grGroup.Key.明細区分 == 1 && grGroup.Key.支払先KEY != 0
                                select new SRY14010_Height
                                {
                                    利用運送 = grGroup.Where(c => c.請求日付 >= d請求From && c.請求日付 <= d請求To).Sum(c => c.重量) == null ? 0 : grGroup.Where(c => c.請求日付 >= d請求From && c.請求日付 <= d請求To).Sum(c => c.重量),
                                }).AsQueryable();

                    if (heit.ToList().Count == 0)
                    {
                        heit = (from m01 in context.M01_TOK
                                select new SRY14010_Height
                                {
                                    利用運送 = 0
                                }).AsQueryable();
                    }

                    var heit2 = (from hei in heit
                                 select new SRY14010_Height
                                 {
                                     利用運送 = heit.Sum(c => c.利用運送) == null ? 0 : heit.Sum(c => c.利用運送),
                                 }).Distinct().AsQueryable();
                    //自社
                    var jisya = (from m70 in context.M70_JIS
                                 where m70.自社ID == s自社コード
                                 select new SRY14010_Jisya
                                 {
                                     事業者番号 = m70.自社ID,
                                     自社住所 = m70.住所１ + m70.住所２,
                                     事業社名 = m70.自社名,
                                     代表社名 = m70.代表者名,
                                     電話番号 = m70.電話番号,
                                 }).AsQueryable();
                    //**在庫の数は日付の日数**//
                    var query3 = (from m70 in jisya
                                  from m99 in context.M99_COMBOLIST.Where(m99 => m99.分類 == "マスタ" && m99.機能 == "車輌マスタ" && m99.カテゴリ == "所属運輸局")
                                  join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車日 == null || c.廃車日 <= d請求To)) on m99.コード equals m05.運輸局ID into m05Group
								  join q in query2 on m99.コード equals q.運輸局 into qGroup
                                  select new SRY14010_Member3
                                  {
                                      事業者番号 = m70.事業者番号,
                                      自社住所 = m70.自社住所,
                                      事業社名 = m70.事業社名,
                                      代表社名 = m70.代表社名,
                                      電話番号 = m70.電話番号,
                                      請求From = d請求From,
                                      請求To = d請求To,
                                      運輸局 = m99.コード,
                                      事業用自動車 = s自動車数,
                                      従業員数 = s従業員数,
                                      運転者数 = s運転者数,
									  //該当外_延実在庫 = m05Group.Where(m05 => m05.運輸局ID == m99.コード).Count() * i日数,
									  //該当外_延実働車 = qGroup.Where(q => q.運輸局 == m99.コード).Sum(c => c.該当外_延実働車) == null ? 0 : qGroup.Where(q => q.運輸局 == m99.コード).Sum(c => c.該当外_延実働車),
									  //該当外_走行 = qGroup.Where(q => q.運輸局 == m99.コード).Sum(c => c.該当外_走行) == null ? 0 : qGroup.Where(q => q.運輸局 == m99.コード).Sum(c => c.該当外_走行),
									  //該当外_実車 = qGroup.Where(q => q.運輸局 == m99.コード).Sum(c => c.該当外_実車) == null ? 0 : qGroup.Where(q => q.運輸局 == m99.コード).Sum(c => c.該当外_実車),
									  //該当外_実運送 = qGroup.Where(q => q.運輸局 == m99.コード).Sum(c => c.該当外_実運送) == null ? 0 : qGroup.Where(q => q.運輸局 == m99.コード).Sum(c => c.該当外_実運送),
                                  }).AsQueryable();



					List<SRY14010_Member3> queryLIST = query3.ToList();
                    List<SRY14010_Height> heightLIST = heit2.ToList();
                    List<SRY14010_Item> itemList = item2.ToList();
                    queryLIST = query3.ToList();

					foreach (var row in queryLIST)
					{
						row.該当外_延実在庫 = query21.Where(c => c.運輸局 == row.運輸局).Sum(c => c.延実在日);
						row.該当外_延実働車 = query21.Where(c => c.運輸局 == row.運輸局).Sum(c => c.該当外_延実働車);
						row.該当外_走行 = query21.Where(c => c.運輸局 == row.運輸局).Sum(c => c.該当外_走行);
						row.該当外_実車 = query21.Where(c => c.運輸局 == row.運輸局).Sum(c => c.該当外_実車);
						row.該当外_実運送 = query21.Where(c => c.運輸局 == row.運輸局).Sum(c => c.該当外_実運送);
					}

                    for (int i = 0; i < queryLIST.Count(); i++)
                    {
                        switch (i)
                        {
                            case 0:
                                queryLIST[0].事業者番号 = queryLIST[i].事業者番号;
                                queryLIST[0].自社住所 = queryLIST[i].自社住所;
                                queryLIST[0].事業社名 = queryLIST[i].事業社名;
                                queryLIST[0].代表社名 = queryLIST[i].代表社名;
                                queryLIST[0].電話番号 = queryLIST[i].電話番号;
                                queryLIST[0].事業用自動車 = queryLIST[i].事業用自動車;
                                queryLIST[0].従業員数 = queryLIST[i].従業員数;
                                queryLIST[0].運転者数 = queryLIST[i].運転者数;
                                queryLIST[0].該当外_延実在庫 = queryLIST[i].該当外_延実在庫;
                                queryLIST[0].該当外_延実働車 = queryLIST[i].該当外_延実働車;
                                queryLIST[0].該当外_走行 = queryLIST[i].該当外_走行;
                                queryLIST[0].該当外_実車 = queryLIST[i].該当外_実車;
                                queryLIST[0].該当外_実運送 = queryLIST[i].該当外_実運送;
                                if (iCmb_Value == 0)
                                {
                                    queryLIST[0].該当外_利用運送 = heightLIST[0].利用運送;
                                    queryLIST[0].該当外_営業収入 = itemList[0].営業収入;
                                }
                                break;

                            case 1:
                                queryLIST[0].北海道_延実在庫 = queryLIST[i].該当外_延実在庫;
                                queryLIST[0].北海道_延実働車 = queryLIST[i].該当外_延実働車;
                                queryLIST[0].北海道_走行 = queryLIST[i].該当外_走行;
                                queryLIST[0].北海道_実車 = queryLIST[i].該当外_実車;
                                queryLIST[0].北海道_実運送 = queryLIST[i].該当外_実運送;
                                if (iCmb_Value == 1)
                                {
                                    queryLIST[0].北海道_利用運送 = heightLIST[0].利用運送;
                                    queryLIST[0].北海道_営業収入 = itemList[0].営業収入;
                                }
                                break;

                            case 2:
                                queryLIST[0].東北_延実在庫 = queryLIST[i].該当外_延実在庫;
                                queryLIST[0].東北_延実働車 = queryLIST[i].該当外_延実働車;
                                queryLIST[0].東北_走行 = queryLIST[i].該当外_走行;
                                queryLIST[0].東北_実車 = queryLIST[i].該当外_実車;
                                queryLIST[0].東北_実運送 = queryLIST[i].該当外_実運送;
                                if (iCmb_Value == 2)
                                {
                                    queryLIST[0].東北_利用運送 = heightLIST[0].利用運送;
                                    queryLIST[0].東北_営業収入 = itemList[0].営業収入;
                                }
                                break;

                            case 3:
                                queryLIST[0].北陸信越_延実在庫 = queryLIST[i].該当外_延実在庫;
                                queryLIST[0].北陸信越_延実働車 = queryLIST[i].該当外_延実働車;
                                queryLIST[0].北陸信越_走行 = queryLIST[i].該当外_走行;
                                queryLIST[0].北陸信越_実車 = queryLIST[i].該当外_実車;
                                queryLIST[0].北陸信越_実運送 = queryLIST[i].該当外_実運送;
                                if (iCmb_Value == 3)
                                {
                                    queryLIST[0].北陸信越_利用運送 = heightLIST[0].利用運送;
                                    queryLIST[0].北陸信越_営業収入 = itemList[0].営業収入;
                                }
                                break;

                            case 4:
                                queryLIST[0].関東_延実在庫 = queryLIST[i].該当外_延実在庫;
                                queryLIST[0].関東_延実働車 = queryLIST[i].該当外_延実働車;
                                queryLIST[0].関東_走行 = queryLIST[i].該当外_走行;
                                queryLIST[0].関東_実車 = queryLIST[i].該当外_実車;
                                queryLIST[0].関東_実運送 = queryLIST[i].該当外_実運送;
                                if (iCmb_Value == 4)
                                {
                                    queryLIST[0].関東_利用運送 = heightLIST[0].利用運送;
                                    queryLIST[0].関東_営業収入 = itemList[0].営業収入;
                                }
                                break;

                            case 5:
                                queryLIST[0].中部_延実在庫 = queryLIST[i].該当外_延実在庫;
                                queryLIST[0].中部_延実働車 = queryLIST[i].該当外_延実働車;
                                queryLIST[0].中部_走行 = queryLIST[i].該当外_走行;
                                queryLIST[0].中部_実車 = queryLIST[i].該当外_実車;
                                queryLIST[0].中部_実運送 = queryLIST[i].該当外_実運送;
                                if (iCmb_Value == 5)
                                {
                                    queryLIST[0].中部_利用運送 = heightLIST[0].利用運送;
                                    queryLIST[0].中部_営業収入 = itemList[0].営業収入;
                                }
                                break;

                            case 6:
                                queryLIST[0].近畿_延実在庫 = queryLIST[i].該当外_延実在庫;
                                queryLIST[0].近畿_延実働車 = queryLIST[i].該当外_延実働車;
                                queryLIST[0].近畿_走行 = queryLIST[i].該当外_走行;
                                queryLIST[0].近畿_実車 = queryLIST[i].該当外_実車;
                                queryLIST[0].近畿_実運送 = queryLIST[i].該当外_実運送;
                                if (iCmb_Value == 6)
                                {
                                    queryLIST[0].近畿_利用運送 = heightLIST[0].利用運送;
                                    queryLIST[0].近畿_営業収入 = itemList[0].営業収入;
                                }
                                break;

                            case 7:
                                queryLIST[0].中国_延実在庫 = queryLIST[i].該当外_延実在庫;
                                queryLIST[0].中国_延実働車 = queryLIST[i].該当外_延実働車;
                                queryLIST[0].中国_走行 = queryLIST[i].該当外_走行;
                                queryLIST[0].中国_実車 = queryLIST[i].該当外_実車;
                                queryLIST[0].中国_実運送 = queryLIST[i].該当外_実運送;
                                if (iCmb_Value == 7)
                                {
                                    queryLIST[0].中国_利用運送 = heightLIST[0].利用運送;
                                    queryLIST[0].中国_営業収入 = itemList[0].営業収入;
                                }
                                break;

                            case 8:
                                queryLIST[0].四国_延実在庫 = queryLIST[i].該当外_延実在庫;
                                queryLIST[0].四国_延実働車 = queryLIST[i].該当外_延実働車;
                                queryLIST[0].四国_走行 = queryLIST[i].該当外_走行;
                                queryLIST[0].四国_実車 = queryLIST[i].該当外_実車;
                                queryLIST[0].四国_実運送 = queryLIST[i].該当外_実運送;
                                if (iCmb_Value == 8)
                                {
                                    queryLIST[0].四国_利用運送 = heightLIST[0].利用運送;
                                    queryLIST[0].四国_営業収入 = itemList[0].営業収入;
                                }
                                break;

                            case 9:
                                queryLIST[0].九州_延実在庫 = queryLIST[i].該当外_延実在庫;
                                queryLIST[0].九州_延実働車 = queryLIST[i].該当外_延実働車;
                                queryLIST[0].九州_走行 = queryLIST[i].該当外_走行;
                                queryLIST[0].九州_実車 = queryLIST[i].該当外_実車;
                                queryLIST[0].九州_実運送 = queryLIST[i].該当外_実運送;
                                if (iCmb_Value == 9)
                                {
                                    queryLIST[0].九州_利用運送 = heightLIST[0].利用運送;
                                    queryLIST[0].九州_営業収入 = itemList[0].営業収入;
                                }
                                break;

                            case 10:
                                queryLIST[0].沖縄_延実在庫 = queryLIST[i].該当外_延実在庫;
                                queryLIST[0].沖縄_延実働車 = queryLIST[i].該当外_延実働車;
                                queryLIST[0].沖縄_走行 = queryLIST[i].該当外_走行;
                                queryLIST[0].沖縄_実車 = queryLIST[i].該当外_実車;
                                queryLIST[0].沖縄_実運送 = queryLIST[i].該当外_実運送;
                                if (iCmb_Value == 10)
                                {
                                    queryLIST[0].沖縄_利用運送 = heightLIST[0].利用運送;
                                    queryLIST[0].沖縄_営業収入 = itemList[0].営業収入;
                                }
                                break;

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
        #endregion

        #region SRY14010_CSV　印刷

        public List<SRY14010_Member_CSV> SRY14010_GetDataHinList_CSV(string s車輌From, string s車輌To, int?[] i車輌List, int d作成年月From, int d作成年月To,
                                                             int iCmb_Value, int s自社コード, string s自動車数, string s従業員数, string s運転者数, DateTime d請求From, DateTime d請求To)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {

                List<SRY14010_Member_CSV> retList = new List<SRY14010_Member_CSV>();
                context.Connection.Open();
                try
				{
					TimeSpan t日数 = d請求To - d請求From;
					int i日数 = Convert.ToInt32(t日数.Days + 1);
					var query2 = (from m05 in context.M05_CAR.Where(c => c.削除日付 == null)
								  join s14 in context.S14_CAR.Where(s14 => d作成年月From <= s14.集計年月 && d作成年月To >= s14.集計年月) on m05.車輌KEY equals s14.車輌KEY into s14Group
								  select new SRY14010_Member2
								  {
									  運輸局 = m05.運輸局ID,
									  車輌KEY = m05.車輌KEY,
									  車輌ID = m05.車輌ID,
									  廃車日 = m05.廃車日,
									  該当外_延実働車 = s14Group.Sum(c => c.稼動日数) == null ? 0 : s14Group.Sum(c => c.稼動日数),
									  該当外_走行 = s14Group.Sum(c => c.走行ＫＭ) == null ? 0 : s14Group.Sum(c => c.走行ＫＭ),
									  該当外_実車 = s14Group.Sum(c => c.実車ＫＭ) == null ? 0 : s14Group.Sum(c => c.実車ＫＭ),
									  該当外_実運送 = s14Group.Sum(c => c.輸送屯数) == null ? 0 : s14Group.Sum(c => c.輸送屯数),

								  }).AsQueryable();

					if (!(string.IsNullOrEmpty(s車輌From + s車輌To) && i車輌List.Length == 0))
					{
						if (!string.IsNullOrEmpty(s車輌From))
						{
							int i車輌From = AppCommon.IntParse(s車輌From);
							query2 = query2.Where(c => c.車輌ID >= i車輌From);
						}

						if (!string.IsNullOrEmpty(s車輌To))
						{
							int i車輌To = AppCommon.IntParse(s車輌To);
							query2 = query2.Where(c => c.車輌ID <= i車輌To);
						}
					}
					else
					{
						query2 = query2.Where(c => c.車輌ID > int.MinValue && c.車輌ID <= int.MaxValue);
					}

					var query21 = query2.ToList();
					foreach (SRY14010_Member2 row in query21)
					{
						if (row.廃車日 == null || row.廃車日 > d請求To)
						{
							row.延実在日 = i日数;
						}
						else
						{
							if (row.廃車日 > d請求From)
							{
								TimeSpan 日数 = (DateTime)row.廃車日 - d請求From;
								row.延実在日 = 日数.Days + 1;
							}
						}
					}

                    //営業収入
                    var item = (from m01 in context.M01_TOK
                                join s01 in context.S01_TOKS on m01.得意先KEY equals s01.得意先KEY into Group
                                where m01.親子区分ID != 3 && m01.取引区分 != 3
                                select new SRY14010_Item
                                {
                                    営業収入 = Group.Where(c => c.集計年月 >= d作成年月From && c.集計年月 <= d作成年月To).Sum(c => c.締日売上金額 + c.締日通行料) == null ? 0 : Group.Where(c => c.集計年月 >= d作成年月From && c.集計年月 <= d作成年月To).Sum(c => c.締日売上金額 + c.締日通行料),
                                }).AsQueryable();
                    var item2 = (from itm in item
                                 select new SRY14010_Item
                                 {
                                     営業収入 = item.Sum(c => c.営業収入)
                                 }).Distinct().AsQueryable();

                    //利用運送
                    var heit = (from m01 in context.M01_TOK
                                join t01 in context.T01_TRN.Where(t01 => (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1)) on m01.得意先KEY equals t01.支払先KEY into Group
                                from t01Group in Group
                                group t01Group by new { t01Group.明細区分, t01Group.支払先KEY } into grGroup
                                where grGroup.Key.明細区分 == 1 && grGroup.Key.支払先KEY != 0
                                select new SRY14010_Height
                                {
									利用運送 = grGroup.Where(c => c.請求日付 >= d請求From && c.請求日付 <= d請求To).Sum(c => c.重量) == null ? 0 : grGroup.Where(c => c.請求日付 >= d請求From && c.請求日付 <= d請求To).Sum(c => c.重量),
                                }).AsQueryable();

					if (heit.ToList().Count == 0)
					{
						heit = (from m01 in context.M01_TOK
								select new SRY14010_Height
								{
									利用運送 = 0
								}).AsQueryable();
					}

                    var heit2 = (from hei in heit
                                 select new SRY14010_Height
								 {
									 利用運送 = heit.Sum(c => c.利用運送) == null ? 0 : heit.Sum(c => c.利用運送),
                                 }).Distinct().AsQueryable();
                    //自社
                    var jisya = (from m70 in context.M70_JIS
                                 where m70.自社ID == s自社コード
                                 select new SRY14010_Jisya
                                 {
                                     事業者番号 = m70.自社ID,
                                     自社住所 = m70.住所１ + m70.住所２,
                                     事業社名 = m70.自社名,
                                     代表社名 = m70.代表者名,
                                     電話番号 = m70.電話番号,
                                 }).AsQueryable();

                    var query3 = (from m70 in jisya
								  from m99 in context.M99_COMBOLIST.Where(m99 => m99.分類 == "マスタ" && m99.機能 == "車輌マスタ" && m99.カテゴリ == "所属運輸局")
								  join m05 in context.M05_CAR.Where(c => c.削除日付 == null && (c.廃車日 == null || c.廃車日 <= d請求To)) on m99.コード equals m05.運輸局ID into m05Group
                                  join q in query2 on m99.コード equals q.運輸局 into qGroup
                                  select new SRY14010_Member_CSV
                                  {
                                      事業者番号 = m70.事業者番号,
                                      自社住所 = m70.自社住所,
                                      事業社名 = m70.事業社名,
                                      代表社名 = m70.代表社名,
                                      電話番号 = m70.電話番号,
                                      運輸局 = m99.コード,
                                      運輸局名 = m99.コード == 0 ? "該当外" : m99.コード == 1 ? "北海道" : m99.コード == 2 ? "東北" : m99.コード == 3 ? "北陸信越" : m99.コード == 4 ? "関東" : m99.コード == 5 ? "中部" : m99.コード == 6 ? "近畿" : m99.コード == 7 ? "中国" : m99.コード == 8 ? "四国" : m99.コード == 9 ? "九州" : "沖縄",
                                      事業用自動車 = s自動車数,
                                      従業員数 = s従業員数,
                                      運転者数 = s運転者数,
									  //延実在庫 = m05Group.Where(m05 => m05.運輸局ID == m99.コード).Count() * 31,
									  //延実働車 = qGroup.Where(q => q.運輸局 == m99.コード).Sum(c => c.該当外_延実働車) == null ? 0 : qGroup.Where(q => q.運輸局 == m99.コード).Sum(c => c.該当外_延実働車),
									  //走行 = qGroup.Where(q => q.運輸局 == m99.コード).Sum(c => c.該当外_走行) == null ? 0 : qGroup.Where(q => q.運輸局 == m99.コード).Sum(c => c.該当外_走行),
									  //実車 = qGroup.Where(q => q.運輸局 == m99.コード).Sum(c => c.該当外_実車) == null ? 0 : qGroup.Where(q => q.運輸局 == m99.コード).Sum(c => c.該当外_実車),
									  //実運送 = qGroup.Where(q => q.運輸局 == m99.コード).Sum(c => c.該当外_実運送) == null ? 0 : qGroup.Where(q => q.運輸局 == m99.コード).Sum(c => c.該当外_実運送),

                                  }).AsQueryable();

                    List<SRY14010_Member_CSV> queryLIST = query3.ToList();
                    List<SRY14010_Height> heightLIST = heit2.ToList();
                    List<SRY14010_Item> itemList = item2.ToList();

					foreach (var row in queryLIST)
					{
						row.延実在庫 = query21.Where(c => c.運輸局 == row.運輸局).Sum(c => c.延実在日);
						row.延実働車 = query21.Where(c => c.運輸局 == row.運輸局).Sum(c => c.該当外_延実働車);
						row.走行 = query21.Where(c => c.運輸局 == row.運輸局).Sum(c => c.該当外_走行);
						row.実車 = query21.Where(c => c.運輸局 == row.運輸局).Sum(c => c.該当外_実車);
						row.実運送 = query21.Where(c => c.運輸局 == row.運輸局).Sum(c => c.該当外_実運送);
					}

					for (int i = 0; i < queryLIST.Count(); i++)
                      {
                          switch (i)
                          {     
                              case 0:
                                  queryLIST[0].事業者番号 = queryLIST[i].事業者番号;
                                  queryLIST[0].自社住所 = queryLIST[i].自社住所;
                                  queryLIST[0].事業社名 = queryLIST[i].事業社名;
                                  queryLIST[0].代表社名 = queryLIST[i].代表社名;
                                  queryLIST[0].電話番号 = queryLIST[i].電話番号;
                                  queryLIST[0].事業用自動車 = queryLIST[i].事業用自動車;
                                  queryLIST[0].従業員数 = queryLIST[i].従業員数;
                                  queryLIST[0].運転者数 = queryLIST[i].運転者数;
                                  queryLIST[i].延実在庫 = queryLIST[i].延実在庫;
                                  queryLIST[i].延実働車 = queryLIST[i].延実働車;
                                  queryLIST[i].走行 = queryLIST[i].走行;
                                  queryLIST[i].実車 = queryLIST[i].実車;
                                  queryLIST[i].実運送 = queryLIST[i].実運送;
                                  if (iCmb_Value == 0)
                                  {
                                      queryLIST[i].利用運送 = heightLIST[0].利用運送;
                                      queryLIST[i].営業収入 = itemList[0].営業収入;
                                  }
                                  break;

                              case 1:
                                  queryLIST[i].延実在庫 = queryLIST[i].延実在庫;
                                  queryLIST[i].延実働車 = queryLIST[i].延実働車;
                                  queryLIST[i].走行 = queryLIST[i].走行;
                                  queryLIST[i].実車 = queryLIST[i].実車;
                                  queryLIST[i].実運送 = queryLIST[i].実運送;
                                  if (iCmb_Value == 1)
                                  {
                                      queryLIST[i].利用運送 = heightLIST[0].利用運送;
                                      queryLIST[i].営業収入 = itemList[0].営業収入;
                                  }
                                  break;

                              case 2:
                                  queryLIST[i].延実在庫 = queryLIST[i].延実在庫;
                                  queryLIST[i].延実働車 = queryLIST[i].延実働車;
                                  queryLIST[i].走行 = queryLIST[i].走行;
                                  queryLIST[i].実車 = queryLIST[i].実車;
                                  queryLIST[i].実運送 = queryLIST[i].実運送;
                                  if(iCmb_Value == 2)
                                  {
                                      queryLIST[i].利用運送 = heightLIST[0].利用運送;
                                      queryLIST[i].営業収入 = itemList[0].営業収入;
                                  }
                                  break;

                              case 3:
                                  queryLIST[i].延実在庫 = queryLIST[i].延実在庫;
                                  queryLIST[i].延実働車 = queryLIST[i].延実働車;
                                  queryLIST[i].走行 = queryLIST[i].走行;
                                  queryLIST[i].実車 = queryLIST[i].実車;
                                  queryLIST[i].実運送 = queryLIST[i].実運送;
                                  if (iCmb_Value == 3)
                                  {
                                      queryLIST[i].利用運送 = heightLIST[0].利用運送;
                                      queryLIST[i].営業収入 = itemList[0].営業収入;
                                  }
                                  break;

                              case 4:
                                  queryLIST[i].延実在庫 = queryLIST[i].延実在庫;
                                  queryLIST[i].延実働車 = queryLIST[i].延実働車;
                                  queryLIST[i].走行 = queryLIST[i].走行;
                                  queryLIST[i].実車 = queryLIST[i].実車;
                                  queryLIST[i].実運送 = queryLIST[i].実運送;
                                  if (iCmb_Value == 4)
                                  {
                                      queryLIST[i].利用運送 = heightLIST[0].利用運送;
                                      queryLIST[i].営業収入 = itemList[0].営業収入;
                                  }
                                  break;

                              case 5:
                                  queryLIST[i].延実在庫 = queryLIST[i].延実在庫;
                                  queryLIST[i].延実働車 = queryLIST[i].延実働車;
                                  queryLIST[i].走行 = queryLIST[i].走行;
                                  queryLIST[i].実車 = queryLIST[i].実車;
                                  queryLIST[i].実運送 = queryLIST[i].実運送;
                                  if (iCmb_Value == 5)
                                  {
                                      queryLIST[i].利用運送 = heightLIST[0].利用運送;
                                      queryLIST[i].営業収入 = itemList[0].営業収入;
                                  }
                                  break;

                              case 6:
                                  queryLIST[i].延実在庫 = queryLIST[i].延実在庫;
                                  queryLIST[i].延実働車 = queryLIST[i].延実働車;
                                  queryLIST[i].走行 = queryLIST[i].走行;
                                  queryLIST[i].実車 = queryLIST[i].実車;
                                  queryLIST[i].実運送 = queryLIST[i].実運送;
                                  if (iCmb_Value == 6)
                                  {
                                      queryLIST[i].利用運送 = heightLIST[0].利用運送;
                                      queryLIST[i].営業収入 = itemList[0].営業収入;
                                  }
                                  break;

                              case 7:
                                  queryLIST[i].延実在庫 = queryLIST[i].延実在庫;
                                  queryLIST[i].延実働車 = queryLIST[i].延実働車;
                                  queryLIST[i].走行 = queryLIST[i].走行;
                                  queryLIST[i].実車 = queryLIST[i].実車;
                                  queryLIST[i].実運送 = queryLIST[i].実運送;
                                  if (iCmb_Value == 7)
                                  {
                                      queryLIST[i].利用運送 = heightLIST[0].利用運送;
                                      queryLIST[i].営業収入 = itemList[0].営業収入;
                                  }
                                  break;

                              case 8:
                                  queryLIST[i].延実在庫 = queryLIST[i].延実在庫;
                                  queryLIST[i].延実働車 = queryLIST[i].延実働車;
                                  queryLIST[i].走行 = queryLIST[i].走行;
                                  queryLIST[i].実車 = queryLIST[i].実車;
                                  queryLIST[i].実運送 = queryLIST[i].実運送;
                                  if (iCmb_Value == 8)
                                  {
                                      queryLIST[i].利用運送 = heightLIST[0].利用運送;
                                      queryLIST[i].営業収入 = itemList[0].営業収入;
                                  }
                                  break;

                              case 9:
                                  queryLIST[i].延実在庫 = queryLIST[i].延実在庫;
                                  queryLIST[i].延実働車 = queryLIST[i].延実働車;
                                  queryLIST[i].走行 = queryLIST[i].走行;
                                  queryLIST[i].実車 = queryLIST[i].実車;
                                  queryLIST[i].実運送 = queryLIST[i].実運送;
                                  if (iCmb_Value == 9)
                                  {
                                      queryLIST[i].利用運送 = heightLIST[0].利用運送;
                                      queryLIST[i].営業収入 = itemList[0].営業収入;
                                  }
                                  break;

                              case 10:
                                  queryLIST[i].延実在庫 = queryLIST[i].延実在庫;
                                  queryLIST[i].延実働車 = queryLIST[i].延実働車;
                                  queryLIST[i].走行 = queryLIST[i].走行;
                                  queryLIST[i].実車 = queryLIST[i].実車;
                                  queryLIST[i].実運送 = queryLIST[i].実運送;
                                  if (iCmb_Value == 10)
                                  {
                                      queryLIST[i].利用運送 = heightLIST[0].利用運送;
                                      queryLIST[i].営業収入 = itemList[0].営業収入;
                                  }
                                  break;

                          }
                      }
                                          return queryLIST;

                    //return query3.ToList();

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