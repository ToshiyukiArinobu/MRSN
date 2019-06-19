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

    public class SRY24010
    {

        #region SRY24010 前年対比　メンバー

        public class SRY24010_Member1
        {
            [DataMember]
            public int 順序ID { get; set; }
            [DataMember]
            public int 車輌ID { get; set; }
            [DataMember]
            public string 車輌名 { get; set; }
            [DataMember]
            public string 項目 { get; set; }
            [DataMember]
            public DateTime 月1 { get; set; }
            [DataMember]
            public DateTime 月2 { get; set; }
            [DataMember]
            public DateTime 月3 { get; set; }
            [DataMember]
            public DateTime 月4 { get; set; }
            [DataMember]
            public DateTime 月5 { get; set; }
            [DataMember]
            public DateTime 月6 { get; set; }
            [DataMember]
            public DateTime 月7 { get; set; }
            [DataMember]
            public DateTime 月8 { get; set; }
            [DataMember]
            public DateTime 月9 { get; set; }
            [DataMember]
            public DateTime 月10 { get; set; }
            [DataMember]
            public DateTime 月11 { get; set; }
            [DataMember]
            public DateTime 月12 { get; set; }
            [DataMember]
            public int 営業日数1 { get; set; }
            [DataMember]
            public int 営業日数2 { get; set; }
            [DataMember]
            public int 営業日数3 { get; set; }
            [DataMember]
            public int 営業日数4 { get; set; }
            [DataMember]
            public int 営業日数5 { get; set; }
            [DataMember]
            public int 営業日数6 { get; set; }
            [DataMember]
            public int 営業日数7 { get; set; }
            [DataMember]
            public int 営業日数8 { get; set; }
            [DataMember]
            public int 営業日数9 { get; set; }
            [DataMember]
            public int 営業日数10 { get; set; }
            [DataMember]
            public int 営業日数11 { get; set; }
            [DataMember]
            public int 営業日数12 { get; set; }
            [DataMember]
            public int 営業日数合計 { get; set; }
            [DataMember]
            public int 稼働日数1 { get; set; }
            [DataMember]
            public int 稼働日数2 { get; set; }
            [DataMember]
            public int 稼働日数3 { get; set; }
            [DataMember]
            public int 稼働日数4 { get; set; }
            [DataMember]
            public int 稼働日数5 { get; set; }
            [DataMember]
            public int 稼働日数6 { get; set; }
            [DataMember]
            public int 稼働日数7 { get; set; }
            [DataMember]
            public int 稼働日数8 { get; set; }
            [DataMember]
            public int 稼働日数9 { get; set; }
            [DataMember]
            public int 稼働日数10 { get; set; }
            [DataMember]
            public int 稼働日数11 { get; set; }
            [DataMember]
            public int 稼働日数12 { get; set; }
            [DataMember]
            public int 稼働日数合計 { get; set; }
            [DataMember]
            public decimal 稼働率1 { get; set; }
            [DataMember]
            public decimal 稼働率2 { get; set; }
            [DataMember]
            public decimal 稼働率3 { get; set; }
            [DataMember]
            public decimal 稼働率4 { get; set; }
            [DataMember]
            public decimal 稼働率5 { get; set; }
            [DataMember]
            public decimal 稼働率6 { get; set; }
            [DataMember]
            public decimal 稼働率7 { get; set; }
            [DataMember]
            public decimal 稼働率8 { get; set; }
            [DataMember]
            public decimal 稼働率9 { get; set; }
            [DataMember]
            public decimal 稼働率10 { get; set; }
            [DataMember]
            public decimal 稼働率11 { get; set; }
            [DataMember]
            public decimal 稼働率12 { get; set; }
            [DataMember]
            public decimal 稼働率合計 { get; set; }
            [DataMember]
            public decimal 前年対比1 { get; set; }
            [DataMember]
            public decimal 前年対比2 { get; set; }
            [DataMember]
            public decimal 前年対比3 { get; set; }
            [DataMember]
            public decimal 前年対比4 { get; set; }
            [DataMember]
            public decimal 前年対比5 { get; set; }
            [DataMember]
            public decimal 前年対比6 { get; set; }
            [DataMember]
            public decimal 前年対比7 { get; set; }
            [DataMember]
            public decimal 前年対比8 { get; set; }
            [DataMember]
            public decimal 前年対比9 { get; set; }
            [DataMember]
            public decimal 前年対比10 { get; set; }
            [DataMember]
            public decimal 前年対比11 { get; set; }
            [DataMember]
            public decimal 前年対比12 { get; set; }
            [DataMember]
            public decimal 前年対比合計 { get; set; }
            [DataMember]
            public decimal 前月対比1 { get; set; }
            [DataMember]
            public decimal 前月対比2 { get; set; }
            [DataMember]
            public decimal 前月対比3 { get; set; }
            [DataMember]
            public decimal 前月対比4 { get; set; }
            [DataMember]
            public decimal 前月対比5 { get; set; }
            [DataMember]
            public decimal 前月対比6 { get; set; }
            [DataMember]
            public decimal 前月対比7 { get; set; }
            [DataMember]
            public decimal 前月対比8 { get; set; }
            [DataMember]
            public decimal 前月対比9 { get; set; }
            [DataMember]
            public decimal 前月対比10 { get; set; }
            [DataMember]
            public decimal 前月対比11 { get; set; }
            [DataMember]
            public decimal 前月対比12 { get; set; }
            [DataMember]
            public decimal 前月対比合計 { get; set; }
            [DataMember]
            public DateTime 開始日付 { get; set; }
            [DataMember]
            public DateTime 終了日付 { get; set; }
            [DataMember]
            public string コードFrom { get; set; }
            [DataMember]
            public string コードTo { get; set; }
            [DataMember]
            public string ピックアップ指定 { get; set; }
        }

        public class SRY24010_Member2
        {
            [DataMember]
            public int 車輌ID { get; set; }
            [DataMember]
            public decimal? 総売上 { get; set; }
        }

        #endregion


        #region SRY24010  前年対比

        public List<SRY24010_Member1> SRY24010_GetData(string p車輌From, string p車輌To, int?[] p車輌List, string s車輌List, string p作成締日, DateTime[] p年月date, int[] p年月, int[] p前年年月, int[] p前月年月)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                int 年月1 = p年月[0];
                int 年月2 = p年月[1];
                int 年月3 = p年月[2];
                int 年月4 = p年月[3];
                int 年月5 = p年月[4];
                int 年月6 = p年月[5];
                int 年月7 = p年月[6];
                int 年月8 = p年月[7];
                int 年月9 = p年月[8];
                int 年月10 = p年月[9];
                int 年月11 = p年月[10];
                int 年月12 = p年月[11];

                int 前年年月1 = p前年年月[0];
                int 前年年月2 = p前年年月[1];
                int 前年年月3 = p前年年月[2];
                int 前年年月4 = p前年年月[3];
                int 前年年月5 = p前年年月[4];
                int 前年年月6 = p前年年月[5];
                int 前年年月7 = p前年年月[6];
                int 前年年月8 = p前年年月[7];
                int 前年年月9 = p前年年月[8];
                int 前年年月10 = p前年年月[9];
                int 前年年月11 = p前年年月[10];
                int 前年年月12 = p前年年月[11];

                int 前月年月1 = p前月年月[0];
                int 前月年月2 = p前月年月[1];
                int 前月年月3 = p前月年月[2];
                int 前月年月4 = p前月年月[3];
                int 前月年月5 = p前月年月[4];
                int 前月年月6 = p前月年月[5];
                int 前月年月7 = p前月年月[6];
                int 前月年月8 = p前月年月[7];
                int 前月年月9 = p前月年月[8];
                int 前月年月10 = p前月年月[9];
                int 前月年月11 = p前月年月[10];
                int 前月年月12 = p前月年月[11];

                DateTime s年月1 = p年月date[0];
                DateTime s年月2 = p年月date[1];
                DateTime s年月3 = p年月date[2];
                DateTime s年月4 = p年月date[3];
                DateTime s年月5 = p年月date[4];
                DateTime s年月6 = p年月date[5];
                DateTime s年月7 = p年月date[6];
                DateTime s年月8 = p年月date[7];
                DateTime s年月9 = p年月date[8];
                DateTime s年月10 = p年月date[9];
                DateTime s年月11 = p年月date[10];
                DateTime s年月12 = p年月date[11];

                int i年月1;
                int i年月2;
                int i年月3;
                int i年月4;
                int i年月5;
                int i年月6;
                int i年月7;
                int i年月8;
                int i年月9;
                int i年月10;
                int i年月11;
                int i年月12;



                //List<SRY24010_Member1> query = new List<SRY24010_Member1>();

                context.Connection.Open();

                try
                {

                    #region 集計
                    ///総売上
                    var query_1 = (from m05 in context.M05_CAR.Where(q => q.削除日付 == null)
                                 join s14_1 in context.S14_CAR.Where(q => q.集計年月 == 年月1) on m05.車輌KEY equals s14_1.車輌KEY into s14Group1
                                 from s14_1G in s14Group1.DefaultIfEmpty()
                                 join s14_2 in context.S14_CAR.Where(q => q.集計年月 == 年月2) on m05.車輌KEY equals s14_2.車輌KEY into s14Group2
                                 from s14_2G in s14Group2.DefaultIfEmpty()
                                 join s14_3 in context.S14_CAR.Where(q => q.集計年月 == 年月3) on m05.車輌KEY equals s14_3.車輌KEY into s14Group3
                                 from s14_3G in s14Group3.DefaultIfEmpty()
                                 join s14_4 in context.S14_CAR.Where(q => q.集計年月 == 年月4) on m05.車輌KEY equals s14_4.車輌KEY into s14Group4
                                 from s14_4G in s14Group4.DefaultIfEmpty()
                                 join s14_5 in context.S14_CAR.Where(q => q.集計年月 == 年月5) on m05.車輌KEY equals s14_5.車輌KEY into s14Group5
                                 from s14_5G in s14Group5.DefaultIfEmpty()
                                 join s14_6 in context.S14_CAR.Where(q => q.集計年月 == 年月6) on m05.車輌KEY equals s14_6.車輌KEY into s14Group6
                                 from s14_6G in s14Group6.DefaultIfEmpty()
                                 join s14_7 in context.S14_CAR.Where(q => q.集計年月 == 年月7) on m05.車輌KEY equals s14_7.車輌KEY into s14Group7
                                 from s14_7G in s14Group7.DefaultIfEmpty()
                                 join s14_8 in context.S14_CAR.Where(q => q.集計年月 == 年月8) on m05.車輌KEY equals s14_8.車輌KEY into s14Group8
                                 from s14_8G in s14Group8.DefaultIfEmpty()
                                 join s14_9 in context.S14_CAR.Where(q => q.集計年月 == 年月9) on m05.車輌KEY equals s14_9.車輌KEY into s14Group9
                                 from s14_9G in s14Group9.DefaultIfEmpty()
                                 join s14_10 in context.S14_CAR.Where(q => q.集計年月 == 年月10) on m05.車輌KEY equals s14_10.車輌KEY into s14Group10
                                 from s14_10G in s14Group10.DefaultIfEmpty()
                                 join s14_11 in context.S14_CAR.Where(q => q.集計年月 == 年月11) on m05.車輌KEY equals s14_11.車輌KEY into s14Group11
                                 from s14_11G in s14Group11.DefaultIfEmpty()
                                 join s14_12 in context.S14_CAR.Where(q => q.集計年月 == 年月12) on m05.車輌KEY equals s14_12.車輌KEY into s14Group12
                                 from s14_12G in s14Group12.DefaultIfEmpty()
                                 //join s14_13 in context.S14_CAR.Where(q => q.集計年月 >= 年月1 && q.集計年月 <= 年月12) on m05.車輌KEY equals s14_13.車輌KEY into s14Group13
                                 //from s14_13G in s14Group13.DefaultIfEmpty()
                                 


                                 select new SRY24010_Member1
                                 {
                                     順序ID = 1,
                                     車輌ID = m05.車輌ID,
                                     車輌名 = m05.車輌番号,

                                     月1 = s年月1,
                                     月2 = s年月2,
                                     月3 = s年月3,
                                     月4 = s年月4,
                                     月5 = s年月5,
                                     月6 = s年月6,
                                     月7 = s年月7,
                                     月8 = s年月8,
                                     月9 = s年月9,
                                     月10 = s年月10,
                                     月11 = s年月11,
                                     月12 = s年月12,
                                     営業日数1 = s14_1G == null ? 0 : s14_1G.営業日数,
                                     営業日数2 = s14_2G == null ? 0 : s14_2G.営業日数,
                                     営業日数3 = s14_3G == null ? 0 : s14_3G.営業日数,
                                     営業日数4 = s14_4G == null ? 0 : s14_4G.営業日数,
                                     営業日数5 = s14_5G == null ? 0 : s14_5G.営業日数,
                                     営業日数6 = s14_6G == null ? 0 : s14_6G.営業日数,
                                     営業日数7 = s14_7G == null ? 0 : s14_7G.営業日数,
                                     営業日数8 = s14_8G == null ? 0 : s14_8G.営業日数,
                                     営業日数9 = s14_9G == null ? 0 : s14_9G.営業日数,
                                     営業日数10 = s14_10G == null ? 0 : s14_10G.営業日数,
                                     営業日数11 = s14_11G == null ? 0 : s14_11G.営業日数,
                                     営業日数12 = s14_12G == null ? 0 : s14_12G.営業日数,
                                     //営業日数合計 = s14Group13 == null ? 0 : s14Group13.Select(c => c.営業日数).Sum(),

                                     稼働日数1 = s14_1G.稼動日数 == null ? 0 : s14_1G.稼動日数,
                                     稼働日数2 = s14_2G.稼動日数 == null ? 0 : s14_2G.稼動日数,
                                     稼働日数3 = s14_3G.稼動日数 == null ? 0 : s14_3G.稼動日数,
                                     稼働日数4 = s14_4G.稼動日数 == null ? 0 : s14_4G.稼動日数,
                                     稼働日数5 = s14_5G.稼動日数 == null ? 0 : s14_5G.稼動日数,
                                     稼働日数6 = s14_6G.稼動日数 == null ? 0 : s14_6G.稼動日数,
                                     稼働日数7 = s14_7G.稼動日数 == null ? 0 : s14_7G.稼動日数,
                                     稼働日数8 = s14_8G.稼動日数 == null ? 0 : s14_8G.稼動日数,
                                     稼働日数9 = s14_9G.稼動日数 == null ? 0 : s14_9G.稼動日数,
                                     稼働日数10 = s14_10G.稼動日数 == null ? 0 : s14_10G.稼動日数,
                                     稼働日数11 = s14_11G.稼動日数 == null ? 0 : s14_11G.稼動日数,
                                     稼働日数12 = s14_12G.稼動日数 == null ? 0 : s14_12G.稼動日数,
                                     //稼働日数合計 = s14Group13 == null ? 0 : s14Group13.Select(c => c.稼動日数).Sum(),


                                     ////一時的に日数挿入
                                     //前年対比1 = s14_1G.稼動日数 == null ? 0 : s14_1G.稼動日数,
                                     //前年対比2 = s14Group.Where(c => c.集計年月 == 前年年月2).Select(c => c.稼動日数).FirstOrDefault(),
                                     //前年対比3 = s14Group.Where(c => c.集計年月 == 前年年月3).Select(c => c.稼動日数).FirstOrDefault(),
                                     //前年対比4 = s14Group.Where(c => c.集計年月 == 前年年月4).Select(c => c.稼動日数).FirstOrDefault(),
                                     //前年対比5 = s14Group.Where(c => c.集計年月 == 前年年月5).Select(c => c.稼動日数).FirstOrDefault(),
                                     //前年対比6 = s14Group.Where(c => c.集計年月 == 前年年月6).Select(c => c.稼動日数).FirstOrDefault(),
                                     //前年対比7 = s14Group.Where(c => c.集計年月 == 前年年月7).Select(c => c.稼動日数).FirstOrDefault(),
                                     //前年対比8 = s14Group.Where(c => c.集計年月 == 前年年月8).Select(c => c.稼動日数).FirstOrDefault(),
                                     //前年対比9 = s14Group.Where(c => c.集計年月 == 前年年月9).Select(c => c.稼動日数).FirstOrDefault(),
                                     //前年対比10 = s14Group.Where(c => c.集計年月 == 前年年月10).Select(c => c.稼動日数).FirstOrDefault(),
                                     //前年対比11 = s14Group.Where(c => c.集計年月 == 前年年月11).Select(c => c.稼動日数).FirstOrDefault(),
                                     //前年対比12 = s14Group.Where(c => c.集計年月 == 前年年月12).Select(c => c.稼動日数).FirstOrDefault(),
                                     ////一時的に日数挿入
                                     //前月対比1 = s14Group.Where(c => c.集計年月 == 前月年月1).Select(c => c.稼動日数).FirstOrDefault(),
                                     //前月対比2 = s14Group.Where(c => c.集計年月 == 前月年月2).Select(c => c.稼動日数).FirstOrDefault(),
                                     //前月対比3 = s14Group.Where(c => c.集計年月 == 前月年月3).Select(c => c.稼動日数).FirstOrDefault(),
                                     //前月対比4 = s14Group.Where(c => c.集計年月 == 前月年月4).Select(c => c.稼動日数).FirstOrDefault(),
                                     //前月対比5 = s14Group.Where(c => c.集計年月 == 前月年月5).Select(c => c.稼動日数).FirstOrDefault(),
                                     //前月対比6 = s14Group.Where(c => c.集計年月 == 前月年月6).Select(c => c.稼動日数).FirstOrDefault(),
                                     //前月対比7 = s14Group.Where(c => c.集計年月 == 前月年月7).Select(c => c.稼動日数).FirstOrDefault(),
                                     //前月対比8 = s14Group.Where(c => c.集計年月 == 前月年月8).Select(c => c.稼動日数).FirstOrDefault(),
                                     //前月対比9 = s14Group.Where(c => c.集計年月 == 前月年月9).Select(c => c.稼動日数).FirstOrDefault(),
                                     //前月対比10 = s14Group.Where(c => c.集計年月 == 前月年月10).Select(c => c.稼動日数).FirstOrDefault(),
                                     //前月対比11 = s14Group.Where(c => c.集計年月 == 前月年月11).Select(c => c.稼動日数).FirstOrDefault(),
                                     //前月対比12 = s14Group.Where(c => c.集計年月 == 前月年月12).Select(c => c.稼動日数).FirstOrDefault(),

                                     コードFrom = p車輌From,
                                     コードTo = p車輌To,
                                     ピックアップ指定 = s車輌List,
                                     //開始日付 = 開始年月1,
                                     //終了日付 = 終了年月12,

                                 }).ToList();

                    var query_2 = (from m05 in context.M05_CAR.Where(q => q.削除日付 == null)
                                 join s14_1 in context.S14_CAR.Where(q => q.集計年月 == 前年年月1) on m05.車輌KEY equals s14_1.車輌KEY into s14Group1
                                 from s14_1G in s14Group1.DefaultIfEmpty()
                                 join s14_2 in context.S14_CAR.Where(q => q.集計年月 == 前年年月2) on m05.車輌KEY equals s14_2.車輌KEY into s14Group2
                                 from s14_2G in s14Group2.DefaultIfEmpty()
                                 join s14_3 in context.S14_CAR.Where(q => q.集計年月 == 前年年月3) on m05.車輌KEY equals s14_3.車輌KEY into s14Group3
                                 from s14_3G in s14Group3.DefaultIfEmpty()
                                 join s14_4 in context.S14_CAR.Where(q => q.集計年月 == 前年年月4) on m05.車輌KEY equals s14_4.車輌KEY into s14Group4
                                 from s14_4G in s14Group4.DefaultIfEmpty()
                                 join s14_5 in context.S14_CAR.Where(q => q.集計年月 == 前年年月5) on m05.車輌KEY equals s14_5.車輌KEY into s14Group5
                                 from s14_5G in s14Group5.DefaultIfEmpty()
                                 join s14_6 in context.S14_CAR.Where(q => q.集計年月 == 前年年月6) on m05.車輌KEY equals s14_6.車輌KEY into s14Group6
                                 from s14_6G in s14Group6.DefaultIfEmpty()
                                 join s14_7 in context.S14_CAR.Where(q => q.集計年月 == 前年年月7) on m05.車輌KEY equals s14_7.車輌KEY into s14Group7
                                 from s14_7G in s14Group7.DefaultIfEmpty()
                                 join s14_8 in context.S14_CAR.Where(q => q.集計年月 == 前年年月8) on m05.車輌KEY equals s14_8.車輌KEY into s14Group8
                                 from s14_8G in s14Group8.DefaultIfEmpty()
                                 join s14_9 in context.S14_CAR.Where(q => q.集計年月 == 前年年月9) on m05.車輌KEY equals s14_9.車輌KEY into s14Group9
                                 from s14_9G in s14Group9.DefaultIfEmpty()
                                 join s14_10 in context.S14_CAR.Where(q => q.集計年月 == 前年年月10) on m05.車輌KEY equals s14_10.車輌KEY into s14Group10
                                 from s14_10G in s14Group10.DefaultIfEmpty()
                                 join s14_11 in context.S14_CAR.Where(q => q.集計年月 == 前年年月11) on m05.車輌KEY equals s14_11.車輌KEY into s14Group11
                                 from s14_11G in s14Group11.DefaultIfEmpty()
                                 join s14_12 in context.S14_CAR.Where(q => q.集計年月 == 前年年月12) on m05.車輌KEY equals s14_12.車輌KEY into s14Group12
                                 from s14_12G in s14Group12.DefaultIfEmpty()
//                                 join s14_13 in context.S14_CAR.Where(q => q.集計年月 >= 前年年月1 && q.集計年月 <= 前年年月12) on m05.車輌KEY equals s14_13.車輌KEY into s14Group13

                                 select new SRY24010_Member1
                                 {
                                     順序ID = 1,
                                     車輌ID = m05.車輌ID,
                                     車輌名 = m05.車輌番号,

                                     月1 = s年月1,
                                     月2 = s年月2,
                                     月3 = s年月3,
                                     月4 = s年月4,
                                     月5 = s年月5,
                                     月6 = s年月6,
                                     月7 = s年月7,
                                     月8 = s年月8,
                                     月9 = s年月9,
                                     月10 = s年月10,
                                     月11 = s年月11,
                                     月12 = s年月12,

                                     //一時的に日数挿入
                                     前年対比1 = s14_1G.稼動日数 == null ? 0 : s14_1G.稼動日数,
                                     前年対比2 = s14_2G.稼動日数 == null ? 0 : s14_2G.稼動日数,
                                     前年対比3 = s14_3G.稼動日数 == null ? 0 : s14_3G.稼動日数,
                                     前年対比4 = s14_4G.稼動日数 == null ? 0 : s14_4G.稼動日数,
                                     前年対比5 = s14_5G.稼動日数 == null ? 0 : s14_5G.稼動日数,
                                     前年対比6 = s14_6G.稼動日数 == null ? 0 : s14_6G.稼動日数,
                                     前年対比7 = s14_7G.稼動日数 == null ? 0 : s14_7G.稼動日数,
                                     前年対比8 = s14_8G.稼動日数 == null ? 0 : s14_8G.稼動日数,
                                     前年対比9 = s14_9G.稼動日数 == null ? 0 : s14_9G.稼動日数,
                                     前年対比10 = s14_10G.稼動日数 == null ? 0 : s14_10G.稼動日数,
                                     前年対比11 = s14_11G.稼動日数 == null ? 0 : s14_11G.稼動日数,
                                     前年対比12 = s14_12G.稼動日数 == null ? 0 : s14_12G.稼動日数,
                                     //前年対比合計 = s14Group13 == null ? 0 : s14Group13.Select(c => c.稼動日数).Sum(),


                                     コードFrom = p車輌From,
                                     コードTo = p車輌To,
                                     ピックアップ指定 = s車輌List,
                                     //開始日付 = 開始年月1,
                                     //終了日付 = 終了年月12,

                                 }).ToList();

                    var query_3 = (from m05 in context.M05_CAR.Where(q => q.削除日付 == null)
                                   join s14_1 in context.S14_CAR.Where(q => q.集計年月 == 前月年月1) on m05.車輌KEY equals s14_1.車輌KEY into s14Group1
                                   from s14_1G in s14Group1.DefaultIfEmpty()
                                   join s14_2 in context.S14_CAR.Where(q => q.集計年月 == 前月年月2) on m05.車輌KEY equals s14_2.車輌KEY into s14Group2
                                   from s14_2G in s14Group2.DefaultIfEmpty()
                                   join s14_3 in context.S14_CAR.Where(q => q.集計年月 == 前月年月3) on m05.車輌KEY equals s14_3.車輌KEY into s14Group3
                                   from s14_3G in s14Group3.DefaultIfEmpty()
                                   join s14_4 in context.S14_CAR.Where(q => q.集計年月 == 前月年月4) on m05.車輌KEY equals s14_4.車輌KEY into s14Group4
                                   from s14_4G in s14Group4.DefaultIfEmpty()
                                   join s14_5 in context.S14_CAR.Where(q => q.集計年月 == 前月年月5) on m05.車輌KEY equals s14_5.車輌KEY into s14Group5
                                   from s14_5G in s14Group5.DefaultIfEmpty()
                                   join s14_6 in context.S14_CAR.Where(q => q.集計年月 == 前月年月6) on m05.車輌KEY equals s14_6.車輌KEY into s14Group6
                                   from s14_6G in s14Group6.DefaultIfEmpty()
                                   join s14_7 in context.S14_CAR.Where(q => q.集計年月 == 前月年月7) on m05.車輌KEY equals s14_7.車輌KEY into s14Group7
                                   from s14_7G in s14Group7.DefaultIfEmpty()
                                   join s14_8 in context.S14_CAR.Where(q => q.集計年月 == 前月年月8) on m05.車輌KEY equals s14_8.車輌KEY into s14Group8
                                   from s14_8G in s14Group8.DefaultIfEmpty()
                                   join s14_9 in context.S14_CAR.Where(q => q.集計年月 == 前月年月9) on m05.車輌KEY equals s14_9.車輌KEY into s14Group9
                                   from s14_9G in s14Group9.DefaultIfEmpty()
                                   join s14_10 in context.S14_CAR.Where(q => q.集計年月 == 前月年月10) on m05.車輌KEY equals s14_10.車輌KEY into s14Group10
                                   from s14_10G in s14Group10.DefaultIfEmpty()
                                   join s14_11 in context.S14_CAR.Where(q => q.集計年月 == 前月年月11) on m05.車輌KEY equals s14_11.車輌KEY into s14Group11
                                   from s14_11G in s14Group11.DefaultIfEmpty()
                                   join s14_12 in context.S14_CAR.Where(q => q.集計年月 == 前月年月12) on m05.車輌KEY equals s14_12.車輌KEY into s14Group12
                                   from s14_12G in s14Group12.DefaultIfEmpty()
//                                   join s14_13 in context.S14_CAR.Where(q => q.集計年月 >= 前月年月1 && q.集計年月 <= 前月年月12) on m05.車輌KEY equals s14_13.車輌KEY into s14Group13


                                   select new SRY24010_Member1
                                   {
                                       順序ID = 1,
                                       車輌ID = m05.車輌ID,
                                       車輌名 = m05.車輌番号,

                                       月1 = s年月1,
                                       月2 = s年月2,
                                       月3 = s年月3,
                                       月4 = s年月4,
                                       月5 = s年月5,
                                       月6 = s年月6,
                                       月7 = s年月7,
                                       月8 = s年月8,
                                       月9 = s年月9,
                                       月10 = s年月10,
                                       月11 = s年月11,
                                       月12 = s年月12,

                                       //一時的に日数挿入
                                       前月対比1 = s14_1G.稼動日数 == null ? 0 : s14_1G.稼動日数,
                                       前月対比2 = s14_2G.稼動日数 == null ? 0 : s14_2G.稼動日数,
                                       前月対比3 = s14_3G.稼動日数 == null ? 0 : s14_3G.稼動日数,
                                       前月対比4 = s14_4G.稼動日数 == null ? 0 : s14_4G.稼動日数,
                                       前月対比5 = s14_5G.稼動日数 == null ? 0 : s14_5G.稼動日数,
                                       前月対比6 = s14_6G.稼動日数 == null ? 0 : s14_6G.稼動日数,
                                       前月対比7 = s14_7G.稼動日数 == null ? 0 : s14_7G.稼動日数,
                                       前月対比8 = s14_8G.稼動日数 == null ? 0 : s14_8G.稼動日数,
                                       前月対比9 = s14_9G.稼動日数 == null ? 0 : s14_9G.稼動日数,
                                       前月対比10 = s14_10G.稼動日数 == null ? 0 : s14_10G.稼動日数,
                                       前月対比11 = s14_11G.稼動日数 == null ? 0 : s14_11G.稼動日数,
                                       前月対比12 = s14_12G.稼動日数 == null ? 0 : s14_12G.稼動日数,
//                                       前月対比合計 = s14Group13 == null ? 0 : s14Group13.Select(c => c.稼動日数).Sum(),

                                       コードFrom = p車輌From,
                                       コードTo = p車輌To,
                                       ピックアップ指定 = s車輌List,
                                       //開始日付 = 開始年月1,
                                       //終了日付 = 終了年月12,

                                   }).ToList();

                    var query = (from q1 in query_1
                                 join q2 in query_2 on q1.車輌ID equals q2.車輌ID into q2Group
                                 from q2g in q2Group.DefaultIfEmpty()
                                 join q3 in query_3 on q1.車輌ID equals q3.車輌ID into q3Group
                                 from q3g in q3Group.DefaultIfEmpty()
                                 select new SRY24010_Member1
                                 {
                                     順序ID = 1,
                                     車輌ID = q1.車輌ID,
                                     車輌名 = q1.車輌名,

                                     月1 = q1.月1,
                                     月2 = q1.月2,
                                     月3 = q1.月3,
                                     月4 = q1.月4,
                                     月5 = q1.月5,
                                     月6 = q1.月6,
                                     月7 = q1.月7,
                                     月8 = q1.月8,
                                     月9 = q1.月9,
                                     月10 = q1.月10,
                                     月11 = q1.月11,
                                     月12 = q1.月12,
                                     営業日数1 = q1.営業日数1,
                                     営業日数2 = q1.営業日数2,
                                     営業日数3 = q1.営業日数3,
                                     営業日数4 = q1.営業日数4,
                                     営業日数5 = q1.営業日数5,
                                     営業日数6 = q1.営業日数6,
                                     営業日数7 = q1.営業日数7,
                                     営業日数8 = q1.営業日数8,
                                     営業日数9 = q1.営業日数9,
                                     営業日数10 = q1.営業日数10,
                                     営業日数11 = q1.営業日数11,
                                     営業日数12 = q1.営業日数12,
                                     営業日数合計 = q1.営業日数1 + q1.営業日数2 + q1.営業日数3 + q1.営業日数4 + q1.営業日数5 + q1.営業日数6 + q1.営業日数7 + q1.営業日数8 + q1.営業日数9 + q1.営業日数10 + q1.営業日数11 + q1.営業日数12,

                                     稼働日数1 = q1.稼働日数1,
                                     稼働日数2 = q1.稼働日数2,
                                     稼働日数3 = q1.稼働日数3,
                                     稼働日数4 = q1.稼働日数4,
                                     稼働日数5 = q1.稼働日数5,
                                     稼働日数6 = q1.稼働日数6,
                                     稼働日数7 = q1.稼働日数7,
                                     稼働日数8 = q1.稼働日数8,
                                     稼働日数9 = q1.稼働日数9,
                                     稼働日数10 = q1.稼働日数10,
                                     稼働日数11 = q1.稼働日数11,
                                     稼働日数12 = q1.稼働日数12,
                                     稼働日数合計 = q1.稼働日数1 + q1.稼働日数2 + q1.稼働日数3 + q1.稼働日数4 + q1.稼働日数5 + q1.稼働日数6 + q1.稼働日数7 + q1.稼働日数8 + q1.稼働日数9 + q1.稼働日数10 + q1.稼働日数11 + q1.稼働日数12,

                                     前年対比1 = q2g.前年対比1,
                                     前年対比2 = q2g.前年対比2,
                                     前年対比3 = q2g.前年対比3,
                                     前年対比4 = q2g.前年対比4,
                                     前年対比5 = q2g.前年対比5,
                                     前年対比6 = q2g.前年対比6,
                                     前年対比7 = q2g.前年対比7,
                                     前年対比8 = q2g.前年対比8,
                                     前年対比9 = q2g.前年対比9,
                                     前年対比10 = q2g.前年対比10,
                                     前年対比11 = q2g.前年対比11,
                                     前年対比12 = q2g.前年対比12,
                                     前年対比合計 = q2g.前年対比1 + q2g.前年対比2 + q2g.前年対比3 + q2g.前年対比4 + q2g.前年対比5 + q2g.前年対比6 + q2g.前年対比7 + q2g.前年対比8 + q2g.前年対比9 + q2g.前年対比10 + q2g.前年対比11 + q2g.前年対比12,

                                     前月対比1 = q3g.前月対比1,
                                     前月対比2 = q3g.前月対比2,
                                     前月対比3 = q3g.前月対比3,
                                     前月対比4 = q3g.前月対比4,
                                     前月対比5 = q3g.前月対比5,
                                     前月対比6 = q3g.前月対比6,
                                     前月対比7 = q3g.前月対比7,
                                     前月対比8 = q3g.前月対比8,
                                     前月対比9 = q3g.前月対比9,
                                     前月対比10 = q3g.前月対比10,
                                     前月対比11 = q3g.前月対比11,
                                     前月対比12 = q3g.前月対比12,
                                     前月対比合計 = q3g.前月対比1 + q3g.前月対比2 + q3g.前月対比3 + q3g.前月対比4 + q3g.前月対比5 + q3g.前月対比6 + q3g.前月対比7 + q3g.前月対比8 + q3g.前月対比9 + q3g.前月対比10 + q3g.前月対比11 + q3g.前月対比12,

                                     コードFrom = p車輌From,
                                     コードTo = p車輌To,
                                     ピックアップ指定 = s車輌List,
                                     //開始日付 = 開始年月1,
                                     //終了日付 = 終了年月12,

                                 }).AsQueryable();


                    //query.AsEnumerable().Where(q => q.月1 = )
                    #endregion

                    //SRY24010_DATASET dset = new SRY24010_DATASET()
                    //{
                    //    売上構成グラフ = query1,
                    //    得意先上位グラフ = query2,
                    //    傭車先上位グラフ = query3,
                    //    損益分岐点グラフ = query4,
                    //};


                    var query2 = (from q in query
                                  group q by q.車輌ID into qGroup
                                  select new SRY24010_Member2
                                  {
                                      車輌ID = qGroup.Key,
                                      総売上 = qGroup.Select(q => (q.稼働日数1 + q.稼働日数2 + q.稼働日数3 + q.稼働日数4 + q.稼働日数5 + q.稼働日数6 + q.稼働日数7 + q.稼働日数8 + q.稼働日数9 + q.稼働日数10 + q.稼働日数11 + q.稼働日数12)).Sum(),

                                  }).AsQueryable();
                    query2 = query2.Where(q => q.総売上 != 0 && q.総売上 != null);

                    query = (from q in query
                             let qqlet = from qq in query2 select qq.車輌ID
                             where qqlet.Contains(q.車輌ID)
                             select q).AsQueryable();

                    #region 車輌指定

                    var query3 = query;

                    //車輌From処理　Min値
                    if (!string.IsNullOrEmpty(p車輌From))
                    {
                        int i車輌From = AppCommon.IntParse(p車輌From);
                        query = query.Where(c => c.車輌ID >= i車輌From || c.車輌ID == 999999999);
                    }

                    //車輌To処理　Max値
                    if (!string.IsNullOrEmpty(p車輌To))
                    {
                        int i車輌TO = AppCommon.IntParse(p車輌To);
                        query = query.Where(c => c.車輌ID <= i車輌TO || c.車輌ID == 999999999);
                    }

                    if (p車輌List.Length > 0)
                    {
                        if ((string.IsNullOrEmpty(p車輌From)) && (string.IsNullOrEmpty(p車輌To)))
                        {
                            query = query3.Where(q => p車輌List.Contains(q.車輌ID) || q.車輌ID == 999999999);
                        }
                        else
                        {
                            query = query.Union(query3.Where(q => p車輌List.Contains(q.車輌ID) || q.車輌ID == 999999999));
                        }
                    }
                    query = query.Distinct();

                    #endregion

                    query = query.OrderBy(q => q.車輌ID);

                    var result = query.ToList();


                    foreach (SRY24010_Member1 row in result)
                    {
                        row.稼働率1 = row.営業日数1 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数1.ToString()) / (row.営業日数1) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.稼働率2 = row.営業日数2 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数2.ToString()) / (row.営業日数2) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.稼働率3 = row.営業日数3 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数3.ToString()) / (row.営業日数3) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.稼働率4 = row.営業日数4 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数4.ToString()) / (row.営業日数4) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.稼働率5 = row.営業日数5 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数5.ToString()) / (row.営業日数5) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.稼働率6 = row.営業日数6 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数6.ToString()) / (row.営業日数6) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.稼働率7 = row.営業日数7 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数7.ToString()) / (row.営業日数7) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.稼働率8 = row.営業日数8 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数8.ToString()) / (row.営業日数8) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.稼働率9 = row.営業日数9 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数9.ToString()) / (row.営業日数9) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.稼働率10 = row.営業日数10 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数10.ToString()) / (row.営業日数10) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.稼働率11 = row.営業日数11 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数11.ToString()) / (row.営業日数11) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.稼働率12 = row.営業日数12 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数12.ToString()) / (row.営業日数12) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.稼働率合計 = row.営業日数合計 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数合計.ToString()) / (row.営業日数合計) * 100).ToString()), 1, MidpointRounding.AwayFromZero);

                        row.前年対比1 = row.前年対比1 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数1.ToString()) / (row.前年対比1) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前年対比2 = row.前年対比2 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数2.ToString()) / (row.前年対比2) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前年対比3 = row.前年対比3 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数3.ToString()) / (row.前年対比3) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前年対比4 = row.前年対比4 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数4.ToString()) / (row.前年対比4) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前年対比5 = row.前年対比5 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数5.ToString()) / (row.前年対比5) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前年対比6 = row.前年対比6 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数6.ToString()) / (row.前年対比6) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前年対比7 = row.前年対比7 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数7.ToString()) / (row.前年対比7) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前年対比8 = row.前年対比8 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数8.ToString()) / (row.前年対比8) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前年対比9 = row.前年対比9 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数9.ToString()) / (row.前年対比9) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前年対比10 = row.前年対比10 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数10.ToString()) / (row.前年対比10) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前年対比11 = row.前年対比11 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数11.ToString()) / (row.前年対比11) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前年対比12 = row.前年対比12 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数12.ToString()) / (row.前年対比12) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前年対比合計 = row.前年対比合計 == 0 ? 0 : Math.Round(AppCommon.DecimalParse((AppCommon.DecimalParse(row.稼働日数合計.ToString()) / (row.前年対比合計) * 100).ToString()), 1, MidpointRounding.AwayFromZero);

                        row.前月対比1 = row.前月対比1 == 0 ? 0 : Math.Round(AppCommon.DecimalParse(((row.稼働日数1) / (row.前月対比1) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前月対比2 = row.前月対比2 == 0 ? 0 : Math.Round(AppCommon.DecimalParse(((row.稼働日数2) / (row.前月対比2) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前月対比3 = row.前月対比3 == 0 ? 0 : Math.Round(AppCommon.DecimalParse(((row.稼働日数3) / (row.前月対比3) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前月対比4 = row.前月対比4 == 0 ? 0 : Math.Round(AppCommon.DecimalParse(((row.稼働日数4) / (row.前月対比4) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前月対比5 = row.前月対比5 == 0 ? 0 : Math.Round(AppCommon.DecimalParse(((row.稼働日数5) / (row.前月対比5) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前月対比6 = row.前月対比6 == 0 ? 0 : Math.Round(AppCommon.DecimalParse(((row.稼働日数6) / (row.前月対比6) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前月対比7 = row.前月対比7 == 0 ? 0 : Math.Round(AppCommon.DecimalParse(((row.稼働日数7) / (row.前月対比7) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前月対比8 = row.前月対比8 == 0 ? 0 : Math.Round(AppCommon.DecimalParse(((row.稼働日数8) / (row.前月対比8) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前月対比9 = row.前月対比9 == 0 ? 0 : Math.Round(AppCommon.DecimalParse(((row.稼働日数9) / (row.前月対比9) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前月対比10 = row.前月対比10 == 0 ? 0 : Math.Round(AppCommon.DecimalParse(((row.稼働日数10) / (row.前月対比10) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前月対比11 = row.前月対比11 == 0 ? 0 : Math.Round(AppCommon.DecimalParse(((row.稼働日数11) / (row.前月対比11) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前月対比12 = row.前月対比12 == 0 ? 0 : Math.Round(AppCommon.DecimalParse(((row.稼働日数12) / (row.前月対比12) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.前月対比合計 = row.前月対比合計 == 0 ? 0 : Math.Round(AppCommon.DecimalParse(((row.稼働日数合計) / (row.前月対比合計) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                    };

                    return result;

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