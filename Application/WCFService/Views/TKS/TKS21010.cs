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

    public class TKS21010
    {
        #region TKS21010 メンバー
        public class TKS21010_Member1
        {
            [DataMember]
            public int 部門ID { get; set; }
            [DataMember]
            public string 部門名 { get; set; }
            [DataMember]
            public string 項目 { get; set; }
            [DataMember]
            public string 月1 { get; set; }
            [DataMember]
            public string 月2 { get; set; }
            [DataMember]
            public string 月3 { get; set; }
            [DataMember]
            public string 月4 { get; set; }
            [DataMember]
            public string 月5 { get; set; }
            [DataMember]
            public string 月6 { get; set; }
            [DataMember]
            public string 月7 { get; set; }
            [DataMember]
            public string 月8 { get; set; }
            [DataMember]
            public string 月9 { get; set; }
            [DataMember]
            public string 月10 { get; set; }
            [DataMember]
            public string 月11 { get; set; }
            [DataMember]
            public string 月12 { get; set; }
            [DataMember]
            public decimal? 総売上1 { get; set; }
            [DataMember]
            public decimal? 総売上2 { get; set; }
            [DataMember]
            public decimal? 総売上3 { get; set; }
            [DataMember]
            public decimal? 総売上4 { get; set; }
            [DataMember]
            public decimal? 総売上5 { get; set; }
            [DataMember]
            public decimal? 総売上6 { get; set; }
            [DataMember]
            public decimal? 総売上7 { get; set; }
            [DataMember]
            public decimal? 総売上8 { get; set; }
            [DataMember]
            public decimal? 総売上9 { get; set; }
            [DataMember]
            public decimal? 総売上10 { get; set; }
            [DataMember]
            public decimal? 総売上11 { get; set; }
            [DataMember]
            public decimal? 総売上12 { get; set; }
            [DataMember]
            public decimal? 総売上合計 { get; set; }
            [DataMember]
            public decimal? 自社売上1 { get; set; }
            [DataMember]
            public decimal? 自社売上2 { get; set; }
            [DataMember]
            public decimal? 自社売上3 { get; set; }
            [DataMember]
            public decimal? 自社売上4 { get; set; }
            [DataMember]
            public decimal? 自社売上5 { get; set; }
            [DataMember]
            public decimal? 自社売上6 { get; set; }
            [DataMember]
            public decimal? 自社売上7 { get; set; }
            [DataMember]
            public decimal? 自社売上8 { get; set; }
            [DataMember]
            public decimal? 自社売上9 { get; set; }
            [DataMember]
            public decimal? 自社売上10 { get; set; }
            [DataMember]
            public decimal? 自社売上11 { get; set; }
            [DataMember]
            public decimal? 自社売上12 { get; set; }
            [DataMember]
            public decimal? 自社売上合計 { get; set; }
            [DataMember]
            public decimal? 傭車売上1 { get; set; }
            [DataMember]
            public decimal? 傭車売上2 { get; set; }
            [DataMember]
            public decimal? 傭車売上3 { get; set; }
            [DataMember]
            public decimal? 傭車売上4 { get; set; }
            [DataMember]
            public decimal? 傭車売上5 { get; set; }
            [DataMember]
            public decimal? 傭車売上6 { get; set; }
            [DataMember]
            public decimal? 傭車売上7 { get; set; }
            [DataMember]
            public decimal? 傭車売上8 { get; set; }
            [DataMember]
            public decimal? 傭車売上9 { get; set; }
            [DataMember]
            public decimal? 傭車売上10 { get; set; }
            [DataMember]
            public decimal? 傭車売上11 { get; set; }
            [DataMember]
            public decimal? 傭車売上12 { get; set; }
            [DataMember]
            public decimal? 傭車売上合計 { get; set; }
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

        public class TKS21010_Member2
        {
            [DataMember]
            public int 部門ID { get; set; }
            [DataMember]
            public decimal? 総売上 { get; set; }
        }

        #endregion 


        #region TKS21011 前年対比　メンバー

        public class TKS21011_Member1
        {
            [DataMember]
            public int 順序ID { get; set; }
            [DataMember]
            public int 部門ID { get; set; }
            [DataMember]
            public string 部門名 { get; set; }
            [DataMember]
            public string 項目 { get; set; }
            [DataMember]
            public string 月1 { get; set; }
            [DataMember]
            public string 月2 { get; set; }
            [DataMember]
            public string 月3 { get; set; }
            [DataMember]
            public string 月4 { get; set; }
            [DataMember]
            public string 月5 { get; set; }
            [DataMember]
            public string 月6 { get; set; }
            [DataMember]
            public string 月7 { get; set; }
            [DataMember]
            public string 月8 { get; set; }
            [DataMember]
            public string 月9 { get; set; }
            [DataMember]
            public string 月10 { get; set; }
            [DataMember]
            public string 月11 { get; set; }
            [DataMember]
            public string 月12 { get; set; }
            [DataMember]
            public decimal? 当年1 { get; set; }
            [DataMember]
            public decimal? 当年2 { get; set; }
            [DataMember]
            public decimal? 当年3 { get; set; }
            [DataMember]
            public decimal? 当年4 { get; set; }
            [DataMember]
            public decimal? 当年5 { get; set; }
            [DataMember]
            public decimal? 当年6 { get; set; }
            [DataMember]
            public decimal? 当年7 { get; set; }
            [DataMember]
            public decimal? 当年8 { get; set; }
            [DataMember]
            public decimal? 当年9 { get; set; }
            [DataMember]
            public decimal? 当年10 { get; set; }
            [DataMember]
            public decimal? 当年11 { get; set; }
            [DataMember]
            public decimal? 当年12 { get; set; }
            [DataMember]
            public decimal? 当年合計 { get; set; }
            [DataMember]
            public decimal? 前年1 { get; set; }
            [DataMember]
            public decimal? 前年2 { get; set; }
            [DataMember]
            public decimal? 前年3 { get; set; }
            [DataMember]
            public decimal? 前年4 { get; set; }
            [DataMember]
            public decimal? 前年5 { get; set; }
            [DataMember]
            public decimal? 前年6 { get; set; }
            [DataMember]
            public decimal? 前年7 { get; set; }
            [DataMember]
            public decimal? 前年8 { get; set; }
            [DataMember]
            public decimal? 前年9 { get; set; }
            [DataMember]
            public decimal? 前年10 { get; set; }
            [DataMember]
            public decimal? 前年11 { get; set; }
            [DataMember]
            public decimal? 前年12 { get; set; }
            [DataMember]
            public decimal? 前年合計 { get; set; }
            [DataMember]
            public decimal? 対比1 { get; set; }
            [DataMember]
            public decimal? 対比2 { get; set; }
            [DataMember]
            public decimal? 対比3 { get; set; }
            [DataMember]
            public decimal? 対比4 { get; set; }
            [DataMember]
            public decimal? 対比5 { get; set; }
            [DataMember]
            public decimal? 対比6 { get; set; }
            [DataMember]
            public decimal? 対比7 { get; set; }
            [DataMember]
            public decimal? 対比8 { get; set; }
            [DataMember]
            public decimal? 対比9 { get; set; }
            [DataMember]
            public decimal? 対比10 { get; set; }
            [DataMember]
            public decimal? 対比11 { get; set; }
            [DataMember]
            public decimal? 対比12 { get; set; }
            [DataMember]
            public decimal? 対比合計 { get; set; }
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

        public class TKS21011_Member2
        {
            [DataMember]
            public int 部門ID { get; set; }
            [DataMember]
            public decimal? 総売上 { get; set; }
        }

        #endregion


        #region TKS21010 CSV
        public class TKS21010_CSV
        {
            [DataMember]
            public int 部門ID { get; set; }
            [DataMember]
            public string 部門名 { get; set; }
            [DataMember]
            public string 項目 { get; set; }
            [DataMember]
            public string 項目2 { get; set; }
            [DataMember]
            public decimal 月1 { get; set; }
            [DataMember]
            public decimal 月2 { get; set; }
            [DataMember]
            public decimal 月3 { get; set; }
            [DataMember]
            public decimal 月4 { get; set; }
            [DataMember]
            public decimal 月5 { get; set; }
            [DataMember]
            public decimal 月6 { get; set; }
            [DataMember]
            public decimal 月7 { get; set; }
            [DataMember]
            public decimal 月8 { get; set; }
            [DataMember]
            public decimal 月9 { get; set; }
            [DataMember]
            public decimal 月10 { get; set; }
            [DataMember]
            public decimal 月11 { get; set; }
            [DataMember]
            public decimal 月12 { get; set; }
            [DataMember]
            public decimal 合計 { get; set; }
            [DataMember]
            public string 月1項目 { get; set; }
            [DataMember]
            public string 月2項目 { get; set; }
            [DataMember]
            public string 月3項目 { get; set; }
            [DataMember]
            public string 月4項目 { get; set; }
            [DataMember]
            public string 月5項目 { get; set; }
            [DataMember]
            public string 月6項目 { get; set; }
            [DataMember]
            public string 月7項目 { get; set; }
            [DataMember]
            public string 月8項目 { get; set; }
            [DataMember]
            public string 月9項目 { get; set; }
            [DataMember]
            public string 月10項目 { get; set; }
            [DataMember]
            public string 月11項目 { get; set; }
            [DataMember]
            public string 月12項目 { get; set; }
        }
        #endregion

        #region TKS21011 CSV
        public class TKS21011_CSV
        {
            [DataMember]
            public int 部門ID { get; set; }
            [DataMember]
            public string 部門名 { get; set; }
            [DataMember]
            public string 項目 { get; set; }
            [DataMember]
            public string 項目2 { get; set; }
            [DataMember]
            public decimal 月1 { get; set; }
            [DataMember]
            public decimal 月2 { get; set; }
            [DataMember]
            public decimal 月3 { get; set; }
            [DataMember]
            public decimal 月4 { get; set; }
            [DataMember]
            public decimal 月5 { get; set; }
            [DataMember]
            public decimal 月6 { get; set; }
            [DataMember]
            public decimal 月7 { get; set; }
            [DataMember]
            public decimal 月8 { get; set; }
            [DataMember]
            public decimal 月9 { get; set; }
            [DataMember]
            public decimal 月10 { get; set; }
            [DataMember]
            public decimal 月11 { get; set; }
            [DataMember]
            public decimal 月12 { get; set; }
            [DataMember]
            public decimal 合計 { get; set; }
            [DataMember]
            public string 月1項目 { get; set; }
            [DataMember]
            public string 月2項目 { get; set; }
            [DataMember]
            public string 月3項目 { get; set; }
            [DataMember]
            public string 月4項目 { get; set; }
            [DataMember]
            public string 月5項目 { get; set; }
            [DataMember]
            public string 月6項目 { get; set; }
            [DataMember]
            public string 月7項目 { get; set; }
            [DataMember]
            public string 月8項目 { get; set; }
            [DataMember]
            public string 月9項目 { get; set; }
            [DataMember]
            public string 月10項目 { get; set; }
            [DataMember]
            public string 月11項目 { get; set; }
            [DataMember]
            public string 月12項目 { get; set; }
        }
        #endregion


        #region TKS21010

        public List<TKS21010_Member1> TKS21010_GetData(string p部門From, string p部門To, int?[] p部門List, string s部門List, string p作成締日, DateTime[] p開始日付, DateTime[] p終了日付)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                DateTime 開始年月1 = p開始日付[0];
                DateTime 開始年月2 = p開始日付[1];
                DateTime 開始年月3 = p開始日付[2];
                DateTime 開始年月4 = p開始日付[3];
                DateTime 開始年月5 = p開始日付[4];
                DateTime 開始年月6 = p開始日付[5];
                DateTime 開始年月7 = p開始日付[6];
                DateTime 開始年月8 = p開始日付[7];
                DateTime 開始年月9 = p開始日付[8];
                DateTime 開始年月10 = p開始日付[9];
                DateTime 開始年月11 = p開始日付[10];
                DateTime 開始年月12 = p開始日付[11];

                DateTime 終了年月1 = p終了日付[0];
                DateTime 終了年月2 = p終了日付[1];
                DateTime 終了年月3 = p終了日付[2];
                DateTime 終了年月4 = p終了日付[3];
                DateTime 終了年月5 = p終了日付[4];
                DateTime 終了年月6 = p終了日付[5];
                DateTime 終了年月7 = p終了日付[6];
                DateTime 終了年月8 = p終了日付[7];
                DateTime 終了年月9 = p終了日付[8];
                DateTime 終了年月10 = p終了日付[9];
                DateTime 終了年月11 = p終了日付[10];
                DateTime 終了年月12 = p終了日付[11];

                string s年月1 = p終了日付[0].Month.ToString() + "月";
                string s年月2 = p終了日付[1].Month.ToString() + "月";
                string s年月3 = p終了日付[2].Month.ToString() + "月";
                string s年月4 = p終了日付[3].Month.ToString() + "月";
                string s年月5 = p終了日付[4].Month.ToString() + "月";
                string s年月6 = p終了日付[5].Month.ToString() + "月";
                string s年月7 = p終了日付[6].Month.ToString() + "月";
                string s年月8 = p終了日付[7].Month.ToString() + "月";
                string s年月9 = p終了日付[8].Month.ToString() + "月";
                string s年月10 = p終了日付[9].Month.ToString() + "月";
                string s年月11 = p終了日付[10].Month.ToString() + "月";
                string s年月12 = p終了日付[11].Month.ToString() + "月";

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

                if (p終了日付[0].Month.ToString().Length == 1)
                {
                    i年月1 = AppCommon.IntParse(p終了日付[0].Year.ToString() + "0" + p終了日付[0].Month.ToString());
                }
                else
                {
                    i年月1 = AppCommon.IntParse(p終了日付[0].Year.ToString() + p終了日付[0].Month.ToString());
                }

                if (p終了日付[1].Month.ToString().Length == 1)
                {
                    i年月2 = AppCommon.IntParse(p終了日付[1].Year.ToString() + "0" + p終了日付[1].Month.ToString());
                }
                else
                {
                    i年月2 = AppCommon.IntParse(p終了日付[1].Year.ToString() + p終了日付[1].Month.ToString());
                }

                if (p終了日付[2].Month.ToString().Length == 1)
                {
                    i年月3 = AppCommon.IntParse(p終了日付[2].Year.ToString() + "0" + p終了日付[2].Month.ToString());
                }
                else
                {
                    i年月3 = AppCommon.IntParse(p終了日付[2].Year.ToString() + p終了日付[2].Month.ToString());
                }

                if (p終了日付[3].Month.ToString().Length == 1)
                {
                    i年月4 = AppCommon.IntParse(p終了日付[3].Year.ToString() + "0" + p終了日付[3].Month.ToString());
                }
                else
                {
                    i年月4 = AppCommon.IntParse(p終了日付[3].Year.ToString() + p終了日付[3].Month.ToString());
                }

                if (p終了日付[4].Month.ToString().Length == 1)
                {
                    i年月5 = AppCommon.IntParse(p終了日付[4].Year.ToString() + "0" + p終了日付[4].Month.ToString());
                }
                else
                {
                    i年月5 = AppCommon.IntParse(p終了日付[4].Year.ToString() + p終了日付[4].Month.ToString());
                }

                if (p終了日付[5].Month.ToString().Length == 1)
                {
                    i年月6 = AppCommon.IntParse(p終了日付[5].Year.ToString() + "0" + p終了日付[5].Month.ToString());
                }
                else
                {
                    i年月6 = AppCommon.IntParse(p終了日付[5].Year.ToString() + p終了日付[5].Month.ToString());
                }

                if (p終了日付[6].Month.ToString().Length == 1)
                {
                    i年月7 = AppCommon.IntParse(p終了日付[6].Year.ToString() + "0" + p終了日付[6].Month.ToString());
                }
                else
                {
                    i年月7 = AppCommon.IntParse(p終了日付[6].Year.ToString() + p終了日付[6].Month.ToString());
                }

                if (p終了日付[7].Month.ToString().Length == 1)
                {
                    i年月8 = AppCommon.IntParse(p終了日付[7].Year.ToString() + "0" + p終了日付[7].Month.ToString());
                }
                else
                {
                    i年月8 = AppCommon.IntParse(p終了日付[7].Year.ToString() + p終了日付[7].Month.ToString());
                }

                if (p終了日付[8].Month.ToString().Length == 1)
                {
                    i年月9 = AppCommon.IntParse(p終了日付[8].Year.ToString() + "0" + p終了日付[8].Month.ToString());
                }
                else
                {
                    i年月9 = AppCommon.IntParse(p終了日付[8].Year.ToString() + p終了日付[8].Month.ToString());
                }

                if (p終了日付[9].Month.ToString().Length == 1)
                {
                    i年月10 = AppCommon.IntParse(p終了日付[9].Year.ToString() + "0" + p終了日付[9].Month.ToString());
                }
                else
                {
                    i年月10 = AppCommon.IntParse(p終了日付[9].Year.ToString() + p終了日付[9].Month.ToString());
                }

                if (p終了日付[10].Month.ToString().Length == 1)
                {
                    i年月11 = AppCommon.IntParse(p終了日付[10].Year.ToString() + "0" + p終了日付[10].Month.ToString());
                }
                else
                {
                    i年月11 = AppCommon.IntParse(p終了日付[10].Year.ToString() + p終了日付[10].Month.ToString());
                }

                if (p終了日付[11].Month.ToString().Length == 1)
                {
                    i年月12 = AppCommon.IntParse(p終了日付[11].Year.ToString() + "0" + p終了日付[11].Month.ToString());
                }
                else
                {
                    i年月12 = AppCommon.IntParse(p終了日付[11].Year.ToString() + p終了日付[11].Month.ToString());
                }


                //List<TKS21010_Member1> query = new List<TKS21010_Member1>();

                context.Connection.Open();

                try
                {

                    #region 集計

                    var query = (from m71 in context.M71_BUM.Where(q => q.削除日付 == null)
                                 select new TKS21010_Member1
                                 {
                                     部門ID = m71.自社部門ID,
                                     部門名 = m71.自社部門名,

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
                                     総売上1 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月1 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上2 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月2 && t01.請求日付 <= 終了年月2 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上3 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月3 && t01.請求日付 <= 終了年月3 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上4 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月4 && t01.請求日付 <= 終了年月4 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上5 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月5 && t01.請求日付 <= 終了年月5 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上6 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月6 && t01.請求日付 <= 終了年月6 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上7 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月7 && t01.請求日付 <= 終了年月7 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上8 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月8 && t01.請求日付 <= 終了年月8 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上9 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月9 && t01.請求日付 <= 終了年月9 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上10 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月10 && t01.請求日付 <= 終了年月10 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上11 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月11 && t01.請求日付 <= 終了年月11 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上12 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月12 && t01.請求日付 <= 終了年月12 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上合計 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月12 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),

                                     自社売上1 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月1) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上2 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月2 && t01.請求日付 <= 終了年月2) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上3 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月3 && t01.請求日付 <= 終了年月3) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上4 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月4 && t01.請求日付 <= 終了年月4) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上5 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月5 && t01.請求日付 <= 終了年月5) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上6 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月6 && t01.請求日付 <= 終了年月6) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上7 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月7 && t01.請求日付 <= 終了年月7) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上8 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月8 && t01.請求日付 <= 終了年月8) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上9 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月9 && t01.請求日付 <= 終了年月9) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上10 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月10 && t01.請求日付 <= 終了年月10) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上11 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月11 && t01.請求日付 <= 終了年月11) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上12 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月12 && t01.請求日付 <= 終了年月12) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上合計 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月12) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),

                                     傭車売上1 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月1) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上2 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月2 && t01.請求日付 <= 終了年月2) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上3 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月3 && t01.請求日付 <= 終了年月3) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上4 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月4 && t01.請求日付 <= 終了年月4) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上5 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月5 && t01.請求日付 <= 終了年月5) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上6 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月6 && t01.請求日付 <= 終了年月6) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上7 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月7 && t01.請求日付 <= 終了年月7) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上8 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月8 && t01.請求日付 <= 終了年月8) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上9 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月9 && t01.請求日付 <= 終了年月9) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上10 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月10 && t01.請求日付 <= 終了年月10) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上11 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月11 && t01.請求日付 <= 終了年月11) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上12 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月12 && t01.請求日付 <= 終了年月12) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上合計 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月12) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),

                                     コードFrom = p部門From,
                                     コードTo = p部門To,
                                     ピックアップ指定 = s部門List,
                                     開始日付 = 開始年月1,
                                     終了日付 = 終了年月12,

                                     項目 = "売上",
                                 }).AsQueryable();
                    //query.AsEnumerable().Where(q => q.月1 = )
                    #endregion

                    //TKS21010_DATASET dset = new TKS21010_DATASET()
                    //{
                    //    売上構成グラフ = query1,
                    //    得意先上位グラフ = query2,
                    //    傭車先上位グラフ = query3,
                    //    損益分岐点グラフ = query4,
                    //};


                    var query2 = (from q in query
                                  group q by q.部門ID into qGroup
                                  select new TKS21010_Member2
                                  {
                                      部門ID = qGroup.Key,
                                      総売上 = qGroup.Select(q => (q.総売上合計 == null ? 0 : q.総売上合計)).Sum(),

                                  }).AsQueryable();
                    query2 = query2.Where(q => q.総売上 != 0 && q.総売上 != null);

                    query = (from q in query
                             let qqlet = from qq in query2 select qq.部門ID
                             where qqlet.Contains(q.部門ID)
                             select q).AsQueryable();

                    #region 部門指定

                    var query3 = query;

                    //部門From処理　Min値
                    if (!string.IsNullOrEmpty(p部門From))
                    {
                        int i部門From = AppCommon.IntParse(p部門From);
                        query = query.Where(c => c.部門ID >= i部門From || c.部門ID == 999999999);
                    }

                    //部門To処理　Max値
                    if (!string.IsNullOrEmpty(p部門To))
                    {
                        int i部門TO = AppCommon.IntParse(p部門To);
                        query = query.Where(c => c.部門ID <= i部門TO || c.部門ID == 999999999);
                    }

                    if (p部門List.Length > 0)
                    {
                        if ((string.IsNullOrEmpty(p部門From)) && (string.IsNullOrEmpty(p部門To)))
                        {
                            query = query3.Where(q => p部門List.Contains(q.部門ID) || q.部門ID == 999999999);
                        }
                        else
                        {
                            query = query.Union(query3.Where(q => p部門List.Contains(q.部門ID) || q.部門ID == 999999999));
                        }
                    }
                    query = query.Distinct();

                    #endregion

                    query = query.OrderBy(q => q.部門ID);

                    var result = query.ToList();

                    if (result != null)
                    {
                        result.Add(new TKS21010_Member1
                        {
                            コードFrom = result.Select(c => c.コードFrom).First(),
                            コードTo = result.Select(c => c.コードTo).First(),
                            ピックアップ指定 = result.Select(c => c.ピックアップ指定).First(),
                            開始日付 = result.Select(c => c.開始日付).First(),
                            終了日付 = result.Select(c => c.終了日付).First(),
                            部門ID = 99999,
                            部門名 = "【 合 計 】",
                            月1 = result.Select(c => c.月1).First(),
                            月2 = result.Select(c => c.月2).First(),
                            月3 = result.Select(c => c.月3).First(),
                            月4 = result.Select(c => c.月4).First(),
                            月5 = result.Select(c => c.月5).First(),
                            月6 = result.Select(c => c.月6).First(),
                            月7 = result.Select(c => c.月7).First(),
                            月8 = result.Select(c => c.月8).First(),
                            月9 = result.Select(c => c.月9).First(),
                            月10 = result.Select(c => c.月10).First(),
                            月11 = result.Select(c => c.月11).First(),
                            月12 = result.Select(c => c.月12).First(),
                            自社売上1 = result.Sum(c => c.自社売上1),
                            自社売上2 = result.Sum(c => c.自社売上2),
                            自社売上3 = result.Sum(c => c.自社売上3),
                            自社売上4 = result.Sum(c => c.自社売上4),
                            自社売上5 = result.Sum(c => c.自社売上5),
                            自社売上6 = result.Sum(c => c.自社売上6),
                            自社売上7 = result.Sum(c => c.自社売上7),
                            自社売上8 = result.Sum(c => c.自社売上8),
                            自社売上9 = result.Sum(c => c.自社売上9),
                            自社売上10 = result.Sum(c => c.自社売上10),
                            自社売上11 = result.Sum(c => c.自社売上11),
                            自社売上12 = result.Sum(c => c.自社売上12),
                            自社売上合計 = result.Sum(c => c.自社売上合計),
                            総売上1 = result.Sum(c => c.総売上1),
                            総売上2 = result.Sum(c => c.総売上2),
                            総売上3 = result.Sum(c => c.総売上3),
                            総売上4 = result.Sum(c => c.総売上4),
                            総売上5 = result.Sum(c => c.総売上5),
                            総売上6 = result.Sum(c => c.総売上6),
                            総売上7 = result.Sum(c => c.総売上7),
                            総売上8 = result.Sum(c => c.総売上8),
                            総売上9 = result.Sum(c => c.総売上9),
                            総売上10 = result.Sum(c => c.総売上10),
                            総売上11 = result.Sum(c => c.総売上11),
                            総売上12 = result.Sum(c => c.総売上12),
                            総売上合計 = result.Sum(c => c.総売上合計),
                            傭車売上1 = result.Sum(c => c.傭車売上1),
                            傭車売上2 = result.Sum(c => c.傭車売上2),
                            傭車売上3 = result.Sum(c => c.傭車売上3),
                            傭車売上4 = result.Sum(c => c.傭車売上4),
                            傭車売上5 = result.Sum(c => c.傭車売上5),
                            傭車売上6 = result.Sum(c => c.傭車売上6),
                            傭車売上7 = result.Sum(c => c.傭車売上7),
                            傭車売上8 = result.Sum(c => c.傭車売上8),
                            傭車売上9 = result.Sum(c => c.傭車売上9),
                            傭車売上10 = result.Sum(c => c.傭車売上10),
                            傭車売上11 = result.Sum(c => c.傭車売上11),
                            傭車売上12 = result.Sum(c => c.傭車売上12),
                            傭車売上合計 = result.Sum(c => c.傭車売上合計),

                        });
                    }


                    return result;

                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }
        }
        #endregion

        #region TKS21010 CSV

        public List<TKS21010_CSV> TKS21010_GetData_CSV(string p部門From, string p部門To, int?[] p部門List, string s部門List, string p作成締日, DateTime[] p開始日付, DateTime[] p終了日付)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                DateTime 開始年月1 = p開始日付[0];
                DateTime 開始年月2 = p開始日付[1];
                DateTime 開始年月3 = p開始日付[2];
                DateTime 開始年月4 = p開始日付[3];
                DateTime 開始年月5 = p開始日付[4];
                DateTime 開始年月6 = p開始日付[5];
                DateTime 開始年月7 = p開始日付[6];
                DateTime 開始年月8 = p開始日付[7];
                DateTime 開始年月9 = p開始日付[8];
                DateTime 開始年月10 = p開始日付[9];
                DateTime 開始年月11 = p開始日付[10];
                DateTime 開始年月12 = p開始日付[11];

                DateTime 終了年月1 = p終了日付[0];
                DateTime 終了年月2 = p終了日付[1];
                DateTime 終了年月3 = p終了日付[2];
                DateTime 終了年月4 = p終了日付[3];
                DateTime 終了年月5 = p終了日付[4];
                DateTime 終了年月6 = p終了日付[5];
                DateTime 終了年月7 = p終了日付[6];
                DateTime 終了年月8 = p終了日付[7];
                DateTime 終了年月9 = p終了日付[8];
                DateTime 終了年月10 = p終了日付[9];
                DateTime 終了年月11 = p終了日付[10];
                DateTime 終了年月12 = p終了日付[11];

                string s年月1 = p終了日付[0].Month.ToString() + "月";
                string s年月2 = p終了日付[1].Month.ToString() + "月";
                string s年月3 = p終了日付[2].Month.ToString() + "月";
                string s年月4 = p終了日付[3].Month.ToString() + "月";
                string s年月5 = p終了日付[4].Month.ToString() + "月";
                string s年月6 = p終了日付[5].Month.ToString() + "月";
                string s年月7 = p終了日付[6].Month.ToString() + "月";
                string s年月8 = p終了日付[7].Month.ToString() + "月";
                string s年月9 = p終了日付[8].Month.ToString() + "月";
                string s年月10 = p終了日付[9].Month.ToString() + "月";
                string s年月11 = p終了日付[10].Month.ToString() + "月";
                string s年月12 = p終了日付[11].Month.ToString() + "月";

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

                if (p終了日付[0].Month.ToString().Length == 1)
                {
                    i年月1 = AppCommon.IntParse(p終了日付[0].Year.ToString() + "0" + p終了日付[0].Month.ToString());
                }
                else
                {
                    i年月1 = AppCommon.IntParse(p終了日付[0].Year.ToString() + p終了日付[0].Month.ToString());
                }

                if (p終了日付[1].Month.ToString().Length == 1)
                {
                    i年月2 = AppCommon.IntParse(p終了日付[1].Year.ToString() + "0" + p終了日付[1].Month.ToString());
                }
                else
                {
                    i年月2 = AppCommon.IntParse(p終了日付[1].Year.ToString() + p終了日付[1].Month.ToString());
                }

                if (p終了日付[2].Month.ToString().Length == 1)
                {
                    i年月3 = AppCommon.IntParse(p終了日付[2].Year.ToString() + "0" + p終了日付[2].Month.ToString());
                }
                else
                {
                    i年月3 = AppCommon.IntParse(p終了日付[2].Year.ToString() + p終了日付[2].Month.ToString());
                }

                if (p終了日付[3].Month.ToString().Length == 1)
                {
                    i年月4 = AppCommon.IntParse(p終了日付[3].Year.ToString() + "0" + p終了日付[3].Month.ToString());
                }
                else
                {
                    i年月4 = AppCommon.IntParse(p終了日付[3].Year.ToString() + p終了日付[3].Month.ToString());
                }

                if (p終了日付[4].Month.ToString().Length == 1)
                {
                    i年月5 = AppCommon.IntParse(p終了日付[4].Year.ToString() + "0" + p終了日付[4].Month.ToString());
                }
                else
                {
                    i年月5 = AppCommon.IntParse(p終了日付[4].Year.ToString() + p終了日付[4].Month.ToString());
                }

                if (p終了日付[5].Month.ToString().Length == 1)
                {
                    i年月6 = AppCommon.IntParse(p終了日付[5].Year.ToString() + "0" + p終了日付[5].Month.ToString());
                }
                else
                {
                    i年月6 = AppCommon.IntParse(p終了日付[5].Year.ToString() + p終了日付[5].Month.ToString());
                }

                if (p終了日付[6].Month.ToString().Length == 1)
                {
                    i年月7 = AppCommon.IntParse(p終了日付[6].Year.ToString() + "0" + p終了日付[6].Month.ToString());
                }
                else
                {
                    i年月7 = AppCommon.IntParse(p終了日付[6].Year.ToString() + p終了日付[6].Month.ToString());
                }

                if (p終了日付[7].Month.ToString().Length == 1)
                {
                    i年月8 = AppCommon.IntParse(p終了日付[7].Year.ToString() + "0" + p終了日付[7].Month.ToString());
                }
                else
                {
                    i年月8 = AppCommon.IntParse(p終了日付[7].Year.ToString() + p終了日付[7].Month.ToString());
                }

                if (p終了日付[8].Month.ToString().Length == 1)
                {
                    i年月9 = AppCommon.IntParse(p終了日付[8].Year.ToString() + "0" + p終了日付[8].Month.ToString());
                }
                else
                {
                    i年月9 = AppCommon.IntParse(p終了日付[8].Year.ToString() + p終了日付[8].Month.ToString());
                }

                if (p終了日付[9].Month.ToString().Length == 1)
                {
                    i年月10 = AppCommon.IntParse(p終了日付[9].Year.ToString() + "0" + p終了日付[9].Month.ToString());
                }
                else
                {
                    i年月10 = AppCommon.IntParse(p終了日付[9].Year.ToString() + p終了日付[9].Month.ToString());
                }

                if (p終了日付[10].Month.ToString().Length == 1)
                {
                    i年月11 = AppCommon.IntParse(p終了日付[10].Year.ToString() + "0" + p終了日付[10].Month.ToString());
                }
                else
                {
                    i年月11 = AppCommon.IntParse(p終了日付[10].Year.ToString() + p終了日付[10].Month.ToString());
                }

                if (p終了日付[11].Month.ToString().Length == 1)
                {
                    i年月12 = AppCommon.IntParse(p終了日付[11].Year.ToString() + "0" + p終了日付[11].Month.ToString());
                }
                else
                {
                    i年月12 = AppCommon.IntParse(p終了日付[11].Year.ToString() + p終了日付[11].Month.ToString());
                }


                //List<TKS21010_Member1> query = new List<TKS21010_Member1>();

                context.Connection.Open();

                try
                {

                    #region 集計

                    var query = (from m71 in context.M71_BUM.Where(q => q.削除日付 == null)
                                 select new TKS21010_Member1
                                 {
                                     部門ID = m71.自社部門ID,
                                     部門名 = m71.自社部門名,

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
                                     総売上1 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月1 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上2 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月2 && t01.請求日付 <= 終了年月2 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上3 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月3 && t01.請求日付 <= 終了年月3 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上4 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月4 && t01.請求日付 <= 終了年月4 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上5 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月5 && t01.請求日付 <= 終了年月5 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上6 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月6 && t01.請求日付 <= 終了年月6 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上7 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月7 && t01.請求日付 <= 終了年月7 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上8 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月8 && t01.請求日付 <= 終了年月8 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上9 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月9 && t01.請求日付 <= 終了年月9 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上10 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月10 && t01.請求日付 <= 終了年月10 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上11 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月11 && t01.請求日付 <= 終了年月11 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上12 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月12 && t01.請求日付 <= 終了年月12 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     総売上合計 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月12 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),

                                     自社売上1 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月1) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上2 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月2 && t01.請求日付 <= 終了年月2) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上3 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月3 && t01.請求日付 <= 終了年月3) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上4 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月4 && t01.請求日付 <= 終了年月4) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上5 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月5 && t01.請求日付 <= 終了年月5) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上6 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月6 && t01.請求日付 <= 終了年月6) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上7 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月7 && t01.請求日付 <= 終了年月7) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上8 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月8 && t01.請求日付 <= 終了年月8) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上9 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月9 && t01.請求日付 <= 終了年月9) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上10 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月10 && t01.請求日付 <= 終了年月10) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上11 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月11 && t01.請求日付 <= 終了年月11) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上12 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月12 && t01.請求日付 <= 終了年月12) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     自社売上合計 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月12) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),

                                     傭車売上1 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月1) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上2 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月2 && t01.請求日付 <= 終了年月2) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上3 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月3 && t01.請求日付 <= 終了年月3) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上4 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月4 && t01.請求日付 <= 終了年月4) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上5 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月5 && t01.請求日付 <= 終了年月5) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上6 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月6 && t01.請求日付 <= 終了年月6) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上7 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月7 && t01.請求日付 <= 終了年月7) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上8 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月8 && t01.請求日付 <= 終了年月8) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上9 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月9 && t01.請求日付 <= 終了年月9) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上10 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月10 && t01.請求日付 <= 終了年月10) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上11 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月11 && t01.請求日付 <= 終了年月11) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上12 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月12 && t01.請求日付 <= 終了年月12) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     傭車売上合計 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月12) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),

                                     コードFrom = p部門From,
                                     コードTo = p部門To,
                                     ピックアップ指定 = s部門List,
                                     開始日付 = 開始年月1,
                                     終了日付 = 終了年月12,

                                     項目 = "売上",
                                 }).AsQueryable();
                    //query.AsEnumerable().Where(q => q.月1 = )
                    #endregion

                    //TKS21010_DATASET dset = new TKS21010_DATASET()
                    //{
                    //    売上構成グラフ = query1,
                    //    得意先上位グラフ = query2,
                    //    傭車先上位グラフ = query3,
                    //    損益分岐点グラフ = query4,
                    //};


                    var query2 = (from q in query
                                  group q by q.部門ID into qGroup
                                  select new TKS21010_Member2
                                  {
                                      部門ID = qGroup.Key,
                                      総売上 = qGroup.Select(q => (q.総売上合計 == null ? 0 : q.総売上合計)).Sum(),

                                  }).AsQueryable();
                    query2 = query2.Where(q => q.総売上 != 0 && q.総売上 != null);

                    query = (from q in query
                             let qqlet = from qq in query2 select qq.部門ID
                             where qqlet.Contains(q.部門ID)
                             select q).AsQueryable();

                    #region 部門指定

                    var query3 = query;

                    //部門From処理　Min値
                    if (!string.IsNullOrEmpty(p部門From))
                    {
                        int i部門From = AppCommon.IntParse(p部門From);
                        query = query.Where(c => c.部門ID >= i部門From || c.部門ID == 999999999);
                    }

                    //部門To処理　Max値
                    if (!string.IsNullOrEmpty(p部門To))
                    {
                        int i部門TO = AppCommon.IntParse(p部門To);
                        query = query.Where(c => c.部門ID <= i部門TO || c.部門ID == 999999999);
                    }

                    if (p部門List.Length > 0)
                    {
                        if ((string.IsNullOrEmpty(p部門From)) && (string.IsNullOrEmpty(p部門To)))
                        {
                            query = query3.Where(q => p部門List.Contains(q.部門ID) || q.部門ID == 999999999);
                        }
                        else
                        {
                            query = query.Union(query3.Where(q => p部門List.Contains(q.部門ID) || q.部門ID == 999999999));
                        }
                    }
                    query = query.Distinct();

                    #endregion

                    query = query.OrderBy(q => q.部門ID);

                    var result = query.ToList();

                    if (result != null)
                    {
                        result.Add(new TKS21010_Member1
                        {
                            コードFrom = result.Select(c => c.コードFrom).First(),
                            コードTo = result.Select(c => c.コードTo).First(),
                            ピックアップ指定 = result.Select(c => c.ピックアップ指定).First(),
                            開始日付 = result.Select(c => c.開始日付).First(),
                            終了日付 = result.Select(c => c.終了日付).First(),
                            部門ID = 99999,
                            部門名 = "【 合 計 】",
                            月1 = result.Select(c => c.月1).First(),
                            月2 = result.Select(c => c.月2).First(),
                            月3 = result.Select(c => c.月3).First(),
                            月4 = result.Select(c => c.月4).First(),
                            月5 = result.Select(c => c.月5).First(),
                            月6 = result.Select(c => c.月6).First(),
                            月7 = result.Select(c => c.月7).First(),
                            月8 = result.Select(c => c.月8).First(),
                            月9 = result.Select(c => c.月9).First(),
                            月10 = result.Select(c => c.月10).First(),
                            月11 = result.Select(c => c.月11).First(),
                            月12 = result.Select(c => c.月12).First(),
                            自社売上1 = result.Sum(c => c.自社売上1),
                            自社売上2 = result.Sum(c => c.自社売上2),
                            自社売上3 = result.Sum(c => c.自社売上3),
                            自社売上4 = result.Sum(c => c.自社売上4),
                            自社売上5 = result.Sum(c => c.自社売上5),
                            自社売上6 = result.Sum(c => c.自社売上6),
                            自社売上7 = result.Sum(c => c.自社売上7),
                            自社売上8 = result.Sum(c => c.自社売上8),
                            自社売上9 = result.Sum(c => c.自社売上9),
                            自社売上10 = result.Sum(c => c.自社売上10),
                            自社売上11 = result.Sum(c => c.自社売上11),
                            自社売上12 = result.Sum(c => c.自社売上12),
                            自社売上合計 = result.Sum(c => c.自社売上合計),
                            総売上1 = result.Sum(c => c.総売上1),
                            総売上2 = result.Sum(c => c.総売上2),
                            総売上3 = result.Sum(c => c.総売上3),
                            総売上4 = result.Sum(c => c.総売上4),
                            総売上5 = result.Sum(c => c.総売上5),
                            総売上6 = result.Sum(c => c.総売上6),
                            総売上7 = result.Sum(c => c.総売上7),
                            総売上8 = result.Sum(c => c.総売上8),
                            総売上9 = result.Sum(c => c.総売上9),
                            総売上10 = result.Sum(c => c.総売上10),
                            総売上11 = result.Sum(c => c.総売上11),
                            総売上12 = result.Sum(c => c.総売上12),
                            総売上合計 = result.Sum(c => c.総売上合計),
                            傭車売上1 = result.Sum(c => c.傭車売上1),
                            傭車売上2 = result.Sum(c => c.傭車売上2),
                            傭車売上3 = result.Sum(c => c.傭車売上3),
                            傭車売上4 = result.Sum(c => c.傭車売上4),
                            傭車売上5 = result.Sum(c => c.傭車売上5),
                            傭車売上6 = result.Sum(c => c.傭車売上6),
                            傭車売上7 = result.Sum(c => c.傭車売上7),
                            傭車売上8 = result.Sum(c => c.傭車売上8),
                            傭車売上9 = result.Sum(c => c.傭車売上9),
                            傭車売上10 = result.Sum(c => c.傭車売上10),
                            傭車売上11 = result.Sum(c => c.傭車売上11),
                            傭車売上12 = result.Sum(c => c.傭車売上12),
                            傭車売上合計 = result.Sum(c => c.傭車売上合計),

                        });
                    }

                    List<TKS21010_CSV> result_csv = new List<TKS21010_CSV>();

                    foreach (TKS21010_Member1 row in result)
                    { 
                        result_csv.Add(new TKS21010_CSV
                                            {
                                                部門ID = row.部門ID,
                                                部門名 = row.部門名,
                                                項目 = "総売上",
                                                月1 = row.総売上1 == null ? 0 : (decimal)row.総売上1,
                                                月2 = row.総売上2 == null ? 0 : (decimal)row.総売上2,
                                                月3 = row.総売上3 == null ? 0 : (decimal)row.総売上3,
                                                月4 = row.総売上4 == null ? 0 : (decimal)row.総売上4,
                                                月5 = row.総売上5 == null ? 0 : (decimal)row.総売上5,
                                                月6 = row.総売上6 == null ? 0 : (decimal)row.総売上6,
                                                月7 = row.総売上7 == null ? 0 : (decimal)row.総売上7,
                                                月8 = row.総売上8 == null ? 0 : (decimal)row.総売上8,
                                                月9 = row.総売上9 == null ? 0 : (decimal)row.総売上9,
                                                月10 = row.総売上10 == null ? 0 : (decimal)row.総売上10,
                                                月11 = row.総売上11 == null ? 0 : (decimal)row.総売上11,
                                                月12 = row.総売上12 == null ? 0 : (decimal)row.総売上12,
                                                合計 = row.総売上合計 == null ? 0 : (decimal)row.総売上合計,
                                                月1項目 = row.月1,
                                                月2項目 = row.月2,
                                                月3項目 = row.月3,
                                                月4項目 = row.月4,
                                                月5項目 = row.月5,
                                                月6項目 = row.月6,
                                                月7項目 = row.月7,
                                                月8項目 = row.月8,
                                                月9項目 = row.月9,
                                                月10項目 = row.月10,
                                                月11項目 = row.月11,
                                                月12項目 = row.月12,
                                            });
                        result_csv.Add(new TKS21010_CSV
                                            {
                                                部門ID = row.部門ID,
                                                部門名 = row.部門名,
                                                項目 = "自社売上",
                                                月1 = row.自社売上1 == null ? 0 : (decimal)row.自社売上1,
                                                月2 = row.自社売上2 == null ? 0 : (decimal)row.自社売上2,
                                                月3 = row.自社売上3 == null ? 0 : (decimal)row.自社売上3,
                                                月4 = row.自社売上4 == null ? 0 : (decimal)row.自社売上4,
                                                月5 = row.自社売上5 == null ? 0 : (decimal)row.自社売上5,
                                                月6 = row.自社売上6 == null ? 0 : (decimal)row.自社売上6,
                                                月7 = row.自社売上7 == null ? 0 : (decimal)row.自社売上7,
                                                月8 = row.自社売上8 == null ? 0 : (decimal)row.自社売上8,
                                                月9 = row.自社売上9 == null ? 0 : (decimal)row.自社売上9,
                                                月10 = row.自社売上10 == null ? 0 : (decimal)row.自社売上10,
                                                月11 = row.自社売上11 == null ? 0 : (decimal)row.自社売上11,
                                                月12 = row.自社売上12 == null ? 0 : (decimal)row.自社売上12,
                                                合計 = row.自社売上合計 == null ? 0 : (decimal)row.自社売上合計,
                                                月1項目 = row.月1,
                                                月2項目 = row.月2,
                                                月3項目 = row.月3,
                                                月4項目 = row.月4,
                                                月5項目 = row.月5,
                                                月6項目 = row.月6,
                                                月7項目 = row.月7,
                                                月8項目 = row.月8,
                                                月9項目 = row.月9,
                                                月10項目 = row.月10,
                                                月11項目 = row.月11,
                                                月12項目 = row.月12,
                                            });
                        result_csv.Add(new TKS21010_CSV
                                            {
                                                部門ID = row.部門ID,
                                                部門名 = row.部門名,
                                                項目 = "自社売上",
                                                月1 = row.傭車売上1 == null ? 0 : (decimal)row.傭車売上1,
                                                月2 = row.傭車売上2 == null ? 0 : (decimal)row.傭車売上2,
                                                月3 = row.傭車売上3 == null ? 0 : (decimal)row.傭車売上3,
                                                月4 = row.傭車売上4 == null ? 0 : (decimal)row.傭車売上4,
                                                月5 = row.傭車売上5 == null ? 0 : (decimal)row.傭車売上5,
                                                月6 = row.傭車売上6 == null ? 0 : (decimal)row.傭車売上6,
                                                月7 = row.傭車売上7 == null ? 0 : (decimal)row.傭車売上7,
                                                月8 = row.傭車売上8 == null ? 0 : (decimal)row.傭車売上8,
                                                月9 = row.傭車売上9 == null ? 0 : (decimal)row.傭車売上9,
                                                月10 = row.傭車売上10 == null ? 0 : (decimal)row.傭車売上10,
                                                月11 = row.傭車売上11 == null ? 0 : (decimal)row.傭車売上11,
                                                月12 = row.傭車売上12 == null ? 0 : (decimal)row.傭車売上12,
                                                合計 = row.傭車売上合計 == null ? 0 : (decimal)row.傭車売上合計,
                                                月1項目 = row.月1,
                                                月2項目 = row.月2,
                                                月3項目 = row.月3,
                                                月4項目 = row.月4,
                                                月5項目 = row.月5,
                                                月6項目 = row.月6,
                                                月7項目 = row.月7,
                                                月8項目 = row.月8,
                                                月9項目 = row.月9,
                                                月10項目 = row.月10,
                                                月11項目 = row.月11,
                                                月12項目 = row.月12,
                                            });
                    };

                    return result_csv;

                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }
        }
        #endregion



        #region TKS21011  前年対比

        public List<TKS21011_Member1> TKS21011_GetData(string p部門From, string p部門To, int?[] p部門List, string s部門List, string p作成締日, DateTime[] p開始日付, DateTime[] p終了日付, int p前年対比)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                DateTime 開始年月1 = p開始日付[0];
                DateTime 開始年月2 = p開始日付[1];
                DateTime 開始年月3 = p開始日付[2];
                DateTime 開始年月4 = p開始日付[3];
                DateTime 開始年月5 = p開始日付[4];
                DateTime 開始年月6 = p開始日付[5];
                DateTime 開始年月7 = p開始日付[6];
                DateTime 開始年月8 = p開始日付[7];
                DateTime 開始年月9 = p開始日付[8];
                DateTime 開始年月10 = p開始日付[9];
                DateTime 開始年月11 = p開始日付[10];
                DateTime 開始年月12 = p開始日付[11];

                DateTime 終了年月1 = p終了日付[0];
                DateTime 終了年月2 = p終了日付[1];
                DateTime 終了年月3 = p終了日付[2];
                DateTime 終了年月4 = p終了日付[3];
                DateTime 終了年月5 = p終了日付[4];
                DateTime 終了年月6 = p終了日付[5];
                DateTime 終了年月7 = p終了日付[6];
                DateTime 終了年月8 = p終了日付[7];
                DateTime 終了年月9 = p終了日付[8];
                DateTime 終了年月10 = p終了日付[9];
                DateTime 終了年月11 = p終了日付[10];
                DateTime 終了年月12 = p終了日付[11];

                DateTime 前年開始年月1 = p開始日付[0].AddYears(-1);
                DateTime 前年開始年月2 = p開始日付[1].AddYears(-1);
                DateTime 前年開始年月3 = p開始日付[2].AddYears(-1);
                DateTime 前年開始年月4 = p開始日付[3].AddYears(-1);
                DateTime 前年開始年月5 = p開始日付[4].AddYears(-1);
                DateTime 前年開始年月6 = p開始日付[5].AddYears(-1);
                DateTime 前年開始年月7 = p開始日付[6].AddYears(-1);
                DateTime 前年開始年月8 = p開始日付[7].AddYears(-1);
                DateTime 前年開始年月9 = p開始日付[8].AddYears(-1);
                DateTime 前年開始年月10 = p開始日付[9].AddYears(-1);
                DateTime 前年開始年月11 = p開始日付[10].AddYears(-1);
                DateTime 前年開始年月12 = p開始日付[11].AddYears(-1);

                DateTime 前年終了年月1 = p終了日付[0].AddYears(-1);
                DateTime 前年終了年月2 = p終了日付[1].AddYears(-1);
                DateTime 前年終了年月3 = p終了日付[2].AddYears(-1);
                DateTime 前年終了年月4 = p終了日付[3].AddYears(-1);
                DateTime 前年終了年月5 = p終了日付[4].AddYears(-1);
                DateTime 前年終了年月6 = p終了日付[5].AddYears(-1);
                DateTime 前年終了年月7 = p終了日付[6].AddYears(-1);
                DateTime 前年終了年月8 = p終了日付[7].AddYears(-1);
                DateTime 前年終了年月9 = p終了日付[8].AddYears(-1);
                DateTime 前年終了年月10 = p終了日付[9].AddYears(-1);
                DateTime 前年終了年月11 = p終了日付[10].AddYears(-1);
                DateTime 前年終了年月12 = p終了日付[11].AddYears(-1);

                string s年月1 = p終了日付[0].Month.ToString() + "月";
                string s年月2 = p終了日付[1].Month.ToString() + "月";
                string s年月3 = p終了日付[2].Month.ToString() + "月";
                string s年月4 = p終了日付[3].Month.ToString() + "月";
                string s年月5 = p終了日付[4].Month.ToString() + "月";
                string s年月6 = p終了日付[5].Month.ToString() + "月";
                string s年月7 = p終了日付[6].Month.ToString() + "月";
                string s年月8 = p終了日付[7].Month.ToString() + "月";
                string s年月9 = p終了日付[8].Month.ToString() + "月";
                string s年月10 = p終了日付[9].Month.ToString() + "月";
                string s年月11 = p終了日付[10].Month.ToString() + "月";
                string s年月12 = p終了日付[11].Month.ToString() + "月";

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

                if (p終了日付[0].Month.ToString().Length == 1)
                {
                    i年月1 = AppCommon.IntParse(p終了日付[0].Year.ToString() + "0" + p終了日付[0].Month.ToString());
                }
                else
                {
                    i年月1 = AppCommon.IntParse(p終了日付[0].Year.ToString() + p終了日付[0].Month.ToString());
                }

                if (p終了日付[1].Month.ToString().Length == 1)
                {
                    i年月2 = AppCommon.IntParse(p終了日付[1].Year.ToString() + "0" + p終了日付[1].Month.ToString());
                }
                else
                {
                    i年月2 = AppCommon.IntParse(p終了日付[1].Year.ToString() + p終了日付[1].Month.ToString());
                }

                if (p終了日付[2].Month.ToString().Length == 1)
                {
                    i年月3 = AppCommon.IntParse(p終了日付[2].Year.ToString() + "0" + p終了日付[2].Month.ToString());
                }
                else
                {
                    i年月3 = AppCommon.IntParse(p終了日付[2].Year.ToString() + p終了日付[2].Month.ToString());
                }

                if (p終了日付[3].Month.ToString().Length == 1)
                {
                    i年月4 = AppCommon.IntParse(p終了日付[3].Year.ToString() + "0" + p終了日付[3].Month.ToString());
                }
                else
                {
                    i年月4 = AppCommon.IntParse(p終了日付[3].Year.ToString() + p終了日付[3].Month.ToString());
                }

                if (p終了日付[4].Month.ToString().Length == 1)
                {
                    i年月5 = AppCommon.IntParse(p終了日付[4].Year.ToString() + "0" + p終了日付[4].Month.ToString());
                }
                else
                {
                    i年月5 = AppCommon.IntParse(p終了日付[4].Year.ToString() + p終了日付[4].Month.ToString());
                }

                if (p終了日付[5].Month.ToString().Length == 1)
                {
                    i年月6 = AppCommon.IntParse(p終了日付[5].Year.ToString() + "0" + p終了日付[5].Month.ToString());
                }
                else
                {
                    i年月6 = AppCommon.IntParse(p終了日付[5].Year.ToString() + p終了日付[5].Month.ToString());
                }

                if (p終了日付[6].Month.ToString().Length == 1)
                {
                    i年月7 = AppCommon.IntParse(p終了日付[6].Year.ToString() + "0" + p終了日付[6].Month.ToString());
                }
                else
                {
                    i年月7 = AppCommon.IntParse(p終了日付[6].Year.ToString() + p終了日付[6].Month.ToString());
                }

                if (p終了日付[7].Month.ToString().Length == 1)
                {
                    i年月8 = AppCommon.IntParse(p終了日付[7].Year.ToString() + "0" + p終了日付[7].Month.ToString());
                }
                else
                {
                    i年月8 = AppCommon.IntParse(p終了日付[7].Year.ToString() + p終了日付[7].Month.ToString());
                }

                if (p終了日付[8].Month.ToString().Length == 1)
                {
                    i年月9 = AppCommon.IntParse(p終了日付[8].Year.ToString() + "0" + p終了日付[8].Month.ToString());
                }
                else
                {
                    i年月9 = AppCommon.IntParse(p終了日付[8].Year.ToString() + p終了日付[8].Month.ToString());
                }

                if (p終了日付[9].Month.ToString().Length == 1)
                {
                    i年月10 = AppCommon.IntParse(p終了日付[9].Year.ToString() + "0" + p終了日付[9].Month.ToString());
                }
                else
                {
                    i年月10 = AppCommon.IntParse(p終了日付[9].Year.ToString() + p終了日付[9].Month.ToString());
                }

                if (p終了日付[10].Month.ToString().Length == 1)
                {
                    i年月11 = AppCommon.IntParse(p終了日付[10].Year.ToString() + "0" + p終了日付[10].Month.ToString());
                }
                else
                {
                    i年月11 = AppCommon.IntParse(p終了日付[10].Year.ToString() + p終了日付[10].Month.ToString());
                }

                if (p終了日付[11].Month.ToString().Length == 1)
                {
                    i年月12 = AppCommon.IntParse(p終了日付[11].Year.ToString() + "0" + p終了日付[11].Month.ToString());
                }
                else
                {
                    i年月12 = AppCommon.IntParse(p終了日付[11].Year.ToString() + p終了日付[11].Month.ToString());
                }


                //List<TKS21011_Member1> query = new List<TKS21011_Member1>();

                context.Connection.Open();

                try
                {

                    #region 集計
                    ///総売上
                    var query = (from m71 in context.M71_BUM.Where(q => q.削除日付 == null)
                                 select new TKS21011_Member1
                                 {
                                     順序ID = 1,
                                     項目 = "総売上",
                                     部門ID = m71.自社部門ID,
                                     部門名 = m71.自社部門名,

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
                                     当年1 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月1 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年2 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月2 && t01.請求日付 <= 終了年月2 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年3 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月3 && t01.請求日付 <= 終了年月3 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年4 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月4 && t01.請求日付 <= 終了年月4 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年5 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月5 && t01.請求日付 <= 終了年月5 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年6 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月6 && t01.請求日付 <= 終了年月6 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年7 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月7 && t01.請求日付 <= 終了年月7 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年8 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月8 && t01.請求日付 <= 終了年月8 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年9 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月9 && t01.請求日付 <= 終了年月9 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年10 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月10 && t01.請求日付 <= 終了年月10 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年11 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月11 && t01.請求日付 <= 終了年月11 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年12 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月12 && t01.請求日付 <= 終了年月12 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年合計 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月12 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),

                                     前年1 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月1 && t01.請求日付 <= 前年終了年月1) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年2 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月2 && t01.請求日付 <= 前年終了年月2) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年3 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月3 && t01.請求日付 <= 前年終了年月3) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年4 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月4 && t01.請求日付 <= 前年終了年月4) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年5 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月5 && t01.請求日付 <= 前年終了年月5) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年6 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月6 && t01.請求日付 <= 前年終了年月6) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年7 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月7 && t01.請求日付 <= 前年終了年月7) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年8 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月8 && t01.請求日付 <= 前年終了年月8) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年9 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月9 && t01.請求日付 <= 前年終了年月9) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年10 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月10 && t01.請求日付 <= 前年終了年月10) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年11 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月11 && t01.請求日付 <= 前年終了年月11) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年12 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月12 && t01.請求日付 <= 前年終了年月12) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年合計 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月1 && t01.請求日付 <= 前年終了年月12) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),

                                     コードFrom = p部門From,
                                     コードTo = p部門To,
                                     ピックアップ指定 = s部門List,
                                     開始日付 = 開始年月1,
                                     終了日付 = 終了年月12,

                                 }).AsQueryable();

                    ///自社売上
                    query = query.Union(from m71 in context.M71_BUM.Where(q => q.削除日付 == null)
                                 select new TKS21011_Member1
                                 {
                                     順序ID = 2,
                                     項目 = "自社売上",
                                     部門ID = m71.自社部門ID,
                                     部門名 = m71.自社部門名,

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
                                     当年1 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月1 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年2 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月2 && t01.請求日付 <= 終了年月2 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年3 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月3 && t01.請求日付 <= 終了年月3 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年4 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月4 && t01.請求日付 <= 終了年月4 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年5 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月5 && t01.請求日付 <= 終了年月5 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年6 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月6 && t01.請求日付 <= 終了年月6 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年7 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月7 && t01.請求日付 <= 終了年月7 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年8 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月8 && t01.請求日付 <= 終了年月8 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年9 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月9 && t01.請求日付 <= 終了年月9 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年10 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月10 && t01.請求日付 <= 終了年月10 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年11 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月11 && t01.請求日付 <= 終了年月11 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年12 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月12 && t01.請求日付 <= 終了年月12 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年合計 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月12 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),

                                     前年1 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月1 && t01.請求日付 <= 前年終了年月1) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年2 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月2 && t01.請求日付 <= 前年終了年月2) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年3 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月3 && t01.請求日付 <= 前年終了年月3) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年4 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月4 && t01.請求日付 <= 前年終了年月4) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年5 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月5 && t01.請求日付 <= 前年終了年月5) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年6 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月6 && t01.請求日付 <= 前年終了年月6) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年7 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月7 && t01.請求日付 <= 前年終了年月7) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年8 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月8 && t01.請求日付 <= 前年終了年月8) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年9 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月9 && t01.請求日付 <= 前年終了年月9) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年10 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月10 && t01.請求日付 <= 前年終了年月10) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年11 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月11 && t01.請求日付 <= 前年終了年月11) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年12 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月12 && t01.請求日付 <= 前年終了年月12) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年合計 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月1 && t01.請求日付 <= 前年終了年月12) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),

                                     コードFrom = p部門From,
                                     コードTo = p部門To,
                                     ピックアップ指定 = s部門List,
                                     開始日付 = 開始年月1,
                                     終了日付 = 終了年月12,

                                 }).AsQueryable();

                    ///傭車売上
                    query = query.Union(from m71 in context.M71_BUM.Where(q => q.削除日付 == null)
                                        select new TKS21011_Member1
                                        {
                                            順序ID = 3,
                                            項目 = "傭車売上",
                                            部門ID = m71.自社部門ID,
                                            部門名 = m71.自社部門名,

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
                                            当年1 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月1 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年2 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月2 && t01.請求日付 <= 終了年月2 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年3 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月3 && t01.請求日付 <= 終了年月3 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年4 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月4 && t01.請求日付 <= 終了年月4 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年5 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月5 && t01.請求日付 <= 終了年月5 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年6 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月6 && t01.請求日付 <= 終了年月6 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年7 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月7 && t01.請求日付 <= 終了年月7 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年8 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月8 && t01.請求日付 <= 終了年月8 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年9 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月9 && t01.請求日付 <= 終了年月9 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年10 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月10 && t01.請求日付 <= 終了年月10 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年11 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月11 && t01.請求日付 <= 終了年月11 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年12 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月12 && t01.請求日付 <= 終了年月12 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年合計 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月12 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),

                                            前年1 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月1 && t01.請求日付 <= 前年終了年月1) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年2 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月2 && t01.請求日付 <= 前年終了年月2) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年3 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月3 && t01.請求日付 <= 前年終了年月3) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年4 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月4 && t01.請求日付 <= 前年終了年月4) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年5 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月5 && t01.請求日付 <= 前年終了年月5) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年6 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月6 && t01.請求日付 <= 前年終了年月6) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年7 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月7 && t01.請求日付 <= 前年終了年月7) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年8 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月8 && t01.請求日付 <= 前年終了年月8) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年9 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月9 && t01.請求日付 <= 前年終了年月9) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年10 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月10 && t01.請求日付 <= 前年終了年月10) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年11 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月11 && t01.請求日付 <= 前年終了年月11) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年12 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月12 && t01.請求日付 <= 前年終了年月12) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年合計 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月1 && t01.請求日付 <= 前年終了年月12) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),

                                            コードFrom = p部門From,
                                            コードTo = p部門To,
                                            ピックアップ指定 = s部門List,
                                            開始日付 = 開始年月1,
                                            終了日付 = 終了年月12,

                                        }).AsQueryable();



                    //query.AsEnumerable().Where(q => q.月1 = )
                    #endregion

                    //TKS21011_DATASET dset = new TKS21011_DATASET()
                    //{
                    //    売上構成グラフ = query1,
                    //    得意先上位グラフ = query2,
                    //    傭車先上位グラフ = query3,
                    //    損益分岐点グラフ = query4,
                    //};


                    var query2 = (from q in query
                                  group q by q.部門ID into qGroup
                                  select new TKS21011_Member2
                                  {
                                      部門ID = qGroup.Key,
                                      総売上 = qGroup.Select(q => (q.当年合計 == null ? 0 : q.当年合計)).Sum(),

                                  }).AsQueryable();
                    query2 = query2.Where(q => q.総売上 != 0 && q.総売上 != null);

                    query = (from q in query
                             let qqlet = from qq in query2 select qq.部門ID
                             where qqlet.Contains(q.部門ID)
                             select q).AsQueryable();

                    #region 部門指定

                    var query3 = query;

                    //部門From処理　Min値
                    if (!string.IsNullOrEmpty(p部門From))
                    {
                        int i部門From = AppCommon.IntParse(p部門From);
                        query = query.Where(c => c.部門ID >= i部門From || c.部門ID == 999999999);
                    }

                    //部門To処理　Max値
                    if (!string.IsNullOrEmpty(p部門To))
                    {
                        int i部門TO = AppCommon.IntParse(p部門To);
                        query = query.Where(c => c.部門ID <= i部門TO || c.部門ID == 999999999);
                    }

                    if (p部門List.Length > 0)
                    {
                        if ((string.IsNullOrEmpty(p部門From)) && (string.IsNullOrEmpty(p部門To)))
                        {
                            query = query3.Where(q => p部門List.Contains(q.部門ID) || q.部門ID == 999999999);
                        }
                        else
                        {
                            query = query.Union(query3.Where(q => p部門List.Contains(q.部門ID) || q.部門ID == 999999999));
                        }
                    }
                    query = query.Distinct();

                    #endregion

                    query = query.OrderBy(q => q.部門ID);

                    var result = query.ToList();

                    if (result != null)
                    {
                        result.Add(new TKS21011_Member1
                        {
                            順序ID = 1,
                            項目 = "総売上",
                            コードFrom = result.Where(c => c.順序ID == 1).Select(c => c.コードFrom).First(),
                            コードTo = result.Where(c => c.順序ID == 1).Select(c => c.コードTo).First(),
                            ピックアップ指定 = result.Where(c => c.順序ID == 1).Select(c => c.ピックアップ指定).First(),
                            開始日付 = result.Where(c => c.順序ID == 1).Select(c => c.開始日付).First(),
                            終了日付 = result.Where(c => c.順序ID == 1).Select(c => c.終了日付).First(),
                            部門ID = 99999,
                            部門名 = "【 合 計 】",
                            月1 = result.Where(c => c.順序ID == 1).Select(c => c.月1).First(),
                            月2 = result.Where(c => c.順序ID == 1).Select(c => c.月2).First(),
                            月3 = result.Where(c => c.順序ID == 1).Select(c => c.月3).First(),
                            月4 = result.Where(c => c.順序ID == 1).Select(c => c.月4).First(),
                            月5 = result.Where(c => c.順序ID == 1).Select(c => c.月5).First(),
                            月6 = result.Where(c => c.順序ID == 1).Select(c => c.月6).First(),
                            月7 = result.Where(c => c.順序ID == 1).Select(c => c.月7).First(),
                            月8 = result.Where(c => c.順序ID == 1).Select(c => c.月8).First(),
                            月9 = result.Where(c => c.順序ID == 1).Select(c => c.月9).First(),
                            月10 = result.Where(c => c.順序ID == 1).Select(c => c.月10).First(),
                            月11 = result.Where(c => c.順序ID == 1).Select(c => c.月11).First(),
                            月12 = result.Where(c => c.順序ID == 1).Select(c => c.月12).First(),
                            前年1 = result.Where(c => c.順序ID == 1).Sum(c => c.前年1),
                            前年2 = result.Where(c => c.順序ID == 1).Sum(c => c.前年2),
                            前年3 = result.Where(c => c.順序ID == 1).Sum(c => c.前年3),
                            前年4 = result.Where(c => c.順序ID == 1).Sum(c => c.前年4),
                            前年5 = result.Where(c => c.順序ID == 1).Sum(c => c.前年5),
                            前年6 = result.Where(c => c.順序ID == 1).Sum(c => c.前年6),
                            前年7 = result.Where(c => c.順序ID == 1).Sum(c => c.前年7),
                            前年8 = result.Where(c => c.順序ID == 1).Sum(c => c.前年8),
                            前年9 = result.Where(c => c.順序ID == 1).Sum(c => c.前年9),
                            前年10 = result.Where(c => c.順序ID == 1).Sum(c => c.前年10),
                            前年11 = result.Where(c => c.順序ID == 1).Sum(c => c.前年11),
                            前年12 = result.Where(c => c.順序ID == 1).Sum(c => c.前年12),
                            前年合計 = result.Where(c => c.順序ID == 1).Sum(c => c.前年合計),
                            当年1 = result.Where(c => c.順序ID == 1).Sum(c => c.当年1),
                            当年2 = result.Where(c => c.順序ID == 1).Sum(c => c.当年2),
                            当年3 = result.Where(c => c.順序ID == 1).Sum(c => c.当年3),
                            当年4 = result.Where(c => c.順序ID == 1).Sum(c => c.当年4),
                            当年5 = result.Where(c => c.順序ID == 1).Sum(c => c.当年5),
                            当年6 = result.Where(c => c.順序ID == 1).Sum(c => c.当年6),
                            当年7 = result.Where(c => c.順序ID == 1).Sum(c => c.当年7),
                            当年8 = result.Where(c => c.順序ID == 1).Sum(c => c.当年8),
                            当年9 = result.Where(c => c.順序ID == 1).Sum(c => c.当年9),
                            当年10 = result.Where(c => c.順序ID == 1).Sum(c => c.当年10),
                            当年11 = result.Where(c => c.順序ID == 1).Sum(c => c.当年11),
                            当年12 = result.Where(c => c.順序ID == 1).Sum(c => c.当年12),
                            当年合計 = result.Where(c => c.順序ID == 1).Sum(c => c.当年合計),
                            //対比1 = result.Sum(c => c.対比1),
                            //対比2 = result.Sum(c => c.対比2),
                            //対比3 = result.Sum(c => c.対比3),
                            //対比4 = result.Sum(c => c.対比4),
                            //対比5 = result.Sum(c => c.対比5),
                            //対比6 = result.Sum(c => c.対比6),
                            //対比7 = result.Sum(c => c.対比7),
                            //対比8 = result.Sum(c => c.対比8),
                            //対比9 = result.Sum(c => c.対比9),
                            //対比10 = result.Sum(c => c.対比10),
                            //対比11 = result.Sum(c => c.対比11),
                            //対比12 = result.Sum(c => c.対比12),
                            //対比合計 = result.Sum(c => c.対比合計),
                        });
                        result.Add(new TKS21011_Member1
                        {
                            順序ID = 2,
                            項目 = "自社売上",
                            コードFrom = result.Where(c => c.順序ID == 2).Select(c => c.コードFrom).First(),
                            コードTo = result.Where(c => c.順序ID == 2).Select(c => c.コードTo).First(),
                            ピックアップ指定 = result.Where(c => c.順序ID == 2).Select(c => c.ピックアップ指定).First(),
                            開始日付 = result.Where(c => c.順序ID == 2).Select(c => c.開始日付).First(),
                            終了日付 = result.Where(c => c.順序ID == 2).Select(c => c.終了日付).First(),
                            部門ID = 99999,
                            部門名 = "【 合 計 】",
                            月1 = result.Where(c => c.順序ID == 2).Select(c => c.月1).First(),
                            月2 = result.Where(c => c.順序ID == 2).Select(c => c.月2).First(),
                            月3 = result.Where(c => c.順序ID == 2).Select(c => c.月3).First(),
                            月4 = result.Where(c => c.順序ID == 2).Select(c => c.月4).First(),
                            月5 = result.Where(c => c.順序ID == 2).Select(c => c.月5).First(),
                            月6 = result.Where(c => c.順序ID == 2).Select(c => c.月6).First(),
                            月7 = result.Where(c => c.順序ID == 2).Select(c => c.月7).First(),
                            月8 = result.Where(c => c.順序ID == 2).Select(c => c.月8).First(),
                            月9 = result.Where(c => c.順序ID == 2).Select(c => c.月9).First(),
                            月10 = result.Where(c => c.順序ID == 2).Select(c => c.月10).First(),
                            月11 = result.Where(c => c.順序ID == 2).Select(c => c.月11).First(),
                            月12 = result.Where(c => c.順序ID == 2).Select(c => c.月12).First(),
                            前年1 = result.Where(c => c.順序ID == 2).Sum(c => c.前年1),
                            前年2 = result.Where(c => c.順序ID == 2).Sum(c => c.前年2),
                            前年3 = result.Where(c => c.順序ID == 2).Sum(c => c.前年3),
                            前年4 = result.Where(c => c.順序ID == 2).Sum(c => c.前年4),
                            前年5 = result.Where(c => c.順序ID == 2).Sum(c => c.前年5),
                            前年6 = result.Where(c => c.順序ID == 2).Sum(c => c.前年6),
                            前年7 = result.Where(c => c.順序ID == 2).Sum(c => c.前年7),
                            前年8 = result.Where(c => c.順序ID == 2).Sum(c => c.前年8),
                            前年9 = result.Where(c => c.順序ID == 2).Sum(c => c.前年9),
                            前年10 = result.Where(c => c.順序ID == 2).Sum(c => c.前年10),
                            前年11 = result.Where(c => c.順序ID == 2).Sum(c => c.前年11),
                            前年12 = result.Where(c => c.順序ID == 2).Sum(c => c.前年12),
                            前年合計 = result.Where(c => c.順序ID == 2).Sum(c => c.前年合計),
                            当年1 = result.Where(c => c.順序ID == 2).Sum(c => c.当年1),
                            当年2 = result.Where(c => c.順序ID == 2).Sum(c => c.当年2),
                            当年3 = result.Where(c => c.順序ID == 2).Sum(c => c.当年3),
                            当年4 = result.Where(c => c.順序ID == 2).Sum(c => c.当年4),
                            当年5 = result.Where(c => c.順序ID == 2).Sum(c => c.当年5),
                            当年6 = result.Where(c => c.順序ID == 2).Sum(c => c.当年6),
                            当年7 = result.Where(c => c.順序ID == 2).Sum(c => c.当年7),
                            当年8 = result.Where(c => c.順序ID == 2).Sum(c => c.当年8),
                            当年9 = result.Where(c => c.順序ID == 2).Sum(c => c.当年9),
                            当年10 = result.Where(c => c.順序ID == 2).Sum(c => c.当年10),
                            当年11 = result.Where(c => c.順序ID == 2).Sum(c => c.当年11),
                            当年12 = result.Where(c => c.順序ID == 2).Sum(c => c.当年12),
                            当年合計 = result.Where(c => c.順序ID == 2).Sum(c => c.当年合計),
                            //対比1 = result.Sum(c => c.対比1),
                            //対比2 = result.Sum(c => c.対比2),
                            //対比3 = result.Sum(c => c.対比3),
                            //対比4 = result.Sum(c => c.対比4),
                            //対比5 = result.Sum(c => c.対比5),
                            //対比6 = result.Sum(c => c.対比6),
                            //対比7 = result.Sum(c => c.対比7),
                            //対比8 = result.Sum(c => c.対比8),
                            //対比9 = result.Sum(c => c.対比9),
                            //対比10 = result.Sum(c => c.対比10),
                            //対比11 = result.Sum(c => c.対比11),
                            //対比12 = result.Sum(c => c.対比12),
                            //対比合計 = result.Sum(c => c.対比合計),
                        });
                        result.Add(new TKS21011_Member1
                        {
                            順序ID = 3,
                            項目 = "傭車売上",
                            コードFrom = result.Where(c => c.順序ID == 3).Select(c => c.コードFrom).First(),
                            コードTo = result.Where(c => c.順序ID == 3).Select(c => c.コードTo).First(),
                            ピックアップ指定 = result.Where(c => c.順序ID == 3).Select(c => c.ピックアップ指定).First(),
                            開始日付 = result.Where(c => c.順序ID == 3).Select(c => c.開始日付).First(),
                            終了日付 = result.Where(c => c.順序ID == 3).Select(c => c.終了日付).First(),
                            部門ID = 99999,
                            部門名 = "【 合 計 】",
                            月1 = result.Where(c => c.順序ID == 3).Select(c => c.月1).First(),
                            月2 = result.Where(c => c.順序ID == 3).Select(c => c.月2).First(),
                            月3 = result.Where(c => c.順序ID == 3).Select(c => c.月3).First(),
                            月4 = result.Where(c => c.順序ID == 3).Select(c => c.月4).First(),
                            月5 = result.Where(c => c.順序ID == 3).Select(c => c.月5).First(),
                            月6 = result.Where(c => c.順序ID == 3).Select(c => c.月6).First(),
                            月7 = result.Where(c => c.順序ID == 3).Select(c => c.月7).First(),
                            月8 = result.Where(c => c.順序ID == 3).Select(c => c.月8).First(),
                            月9 = result.Where(c => c.順序ID == 3).Select(c => c.月9).First(),
                            月10 = result.Where(c => c.順序ID == 3).Select(c => c.月10).First(),
                            月11 = result.Where(c => c.順序ID == 3).Select(c => c.月11).First(),
                            月12 = result.Where(c => c.順序ID == 3).Select(c => c.月12).First(),
                            前年1 = result.Where(c => c.順序ID == 3).Sum(c => c.前年1),
                            前年2 = result.Where(c => c.順序ID == 3).Sum(c => c.前年2),
                            前年3 = result.Where(c => c.順序ID == 3).Sum(c => c.前年3),
                            前年4 = result.Where(c => c.順序ID == 3).Sum(c => c.前年4),
                            前年5 = result.Where(c => c.順序ID == 3).Sum(c => c.前年5),
                            前年6 = result.Where(c => c.順序ID == 3).Sum(c => c.前年6),
                            前年7 = result.Where(c => c.順序ID == 3).Sum(c => c.前年7),
                            前年8 = result.Where(c => c.順序ID == 3).Sum(c => c.前年8),
                            前年9 = result.Where(c => c.順序ID == 3).Sum(c => c.前年9),
                            前年10 = result.Where(c => c.順序ID == 3).Sum(c => c.前年10),
                            前年11 = result.Where(c => c.順序ID == 3).Sum(c => c.前年11),
                            前年12 = result.Where(c => c.順序ID == 3).Sum(c => c.前年12),
                            前年合計 = result.Where(c => c.順序ID == 3).Sum(c => c.前年合計),
                            当年1 = result.Where(c => c.順序ID == 3).Sum(c => c.当年1),
                            当年2 = result.Where(c => c.順序ID == 3).Sum(c => c.当年2),
                            当年3 = result.Where(c => c.順序ID == 3).Sum(c => c.当年3),
                            当年4 = result.Where(c => c.順序ID == 3).Sum(c => c.当年4),
                            当年5 = result.Where(c => c.順序ID == 3).Sum(c => c.当年5),
                            当年6 = result.Where(c => c.順序ID == 3).Sum(c => c.当年6),
                            当年7 = result.Where(c => c.順序ID == 3).Sum(c => c.当年7),
                            当年8 = result.Where(c => c.順序ID == 3).Sum(c => c.当年8),
                            当年9 = result.Where(c => c.順序ID == 3).Sum(c => c.当年9),
                            当年10 = result.Where(c => c.順序ID == 3).Sum(c => c.当年10),
                            当年11 = result.Where(c => c.順序ID == 3).Sum(c => c.当年11),
                            当年12 = result.Where(c => c.順序ID == 3).Sum(c => c.当年12),
                            当年合計 = result.Where(c => c.順序ID == 3).Sum(c => c.当年合計),
                            //対比1 = result.Sum(c => c.対比1),
                            //対比2 = result.Sum(c => c.対比2),
                            //対比3 = result.Sum(c => c.対比3),
                            //対比4 = result.Sum(c => c.対比4),
                            //対比5 = result.Sum(c => c.対比5),
                            //対比6 = result.Sum(c => c.対比6),
                            //対比7 = result.Sum(c => c.対比7),
                            //対比8 = result.Sum(c => c.対比8),
                            //対比9 = result.Sum(c => c.対比9),
                            //対比10 = result.Sum(c => c.対比10),
                            //対比11 = result.Sum(c => c.対比11),
                            //対比12 = result.Sum(c => c.対比12),
                            //対比合計 = result.Sum(c => c.対比合計),
                        });
                    }

                    foreach (TKS21011_Member1 row in result)
                    {
                        row.対比1 = row.前年1 == 0 ? 0 : row.前年1 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年1 == null ? 0 : row.当年1) / (row.前年1 == null ? 0 : row.前年1) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比2 = row.前年2 == 0 ? 0 : row.前年2 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年2 == null ? 0 : row.当年2) / (row.前年2 == null ? 0 : row.前年2) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比3 = row.前年3 == 0 ? 0 : row.前年3 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年3 == null ? 0 : row.当年3) / (row.前年3 == null ? 0 : row.前年3) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比4 = row.前年4 == 0 ? 0 : row.前年4 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年4 == null ? 0 : row.当年4) / (row.前年4 == null ? 0 : row.前年4) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比5 = row.前年5 == 0 ? 0 : row.前年5 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年5 == null ? 0 : row.当年5) / (row.前年5 == null ? 0 : row.前年5) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比6 = row.前年6 == 0 ? 0 : row.前年6 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年6 == null ? 0 : row.当年6) / (row.前年6 == null ? 0 : row.前年6) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比7 = row.前年7 == 0 ? 0 : row.前年7 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年7 == null ? 0 : row.当年7) / (row.前年7 == null ? 0 : row.前年7) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比8 = row.前年8 == 0 ? 0 : row.前年8 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年8 == null ? 0 : row.当年8) / (row.前年8 == null ? 0 : row.前年8) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比9 = row.前年9 == 0 ? 0 : row.前年9 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年9 == null ? 0 : row.当年9) / (row.前年9 == null ? 0 : row.前年9) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比10 = row.前年10 == 0 ? 0 : row.前年10 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年10 == null ? 0 : row.当年10) / (row.前年10 == null ? 0 : row.前年10) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比11 = row.前年11 == 0 ? 0 : row.前年11 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年11 == null ? 0 : row.当年11) / (row.前年11 == null ? 0 : row.前年11) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比12 = row.前年12 == 0 ? 0 : row.前年12 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年12 == null ? 0 : row.当年12) / (row.前年12 == null ? 0 : row.前年12) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比合計 = row.前年合計 == 0 ? 0 : row.前年合計 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年合計 == null ? 0 : row.当年合計) / (row.前年合計 == null ? 0 : row.前年合計) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
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



        #region TKS21011 CSV 前年対比

        public List<TKS21011_CSV> TKS21011_GetData_CSV(string p部門From, string p部門To, int?[] p部門List, string s部門List, string p作成締日, DateTime[] p開始日付, DateTime[] p終了日付, int p前年対比)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                DateTime 開始年月1 = p開始日付[0];
                DateTime 開始年月2 = p開始日付[1];
                DateTime 開始年月3 = p開始日付[2];
                DateTime 開始年月4 = p開始日付[3];
                DateTime 開始年月5 = p開始日付[4];
                DateTime 開始年月6 = p開始日付[5];
                DateTime 開始年月7 = p開始日付[6];
                DateTime 開始年月8 = p開始日付[7];
                DateTime 開始年月9 = p開始日付[8];
                DateTime 開始年月10 = p開始日付[9];
                DateTime 開始年月11 = p開始日付[10];
                DateTime 開始年月12 = p開始日付[11];

                DateTime 終了年月1 = p終了日付[0];
                DateTime 終了年月2 = p終了日付[1];
                DateTime 終了年月3 = p終了日付[2];
                DateTime 終了年月4 = p終了日付[3];
                DateTime 終了年月5 = p終了日付[4];
                DateTime 終了年月6 = p終了日付[5];
                DateTime 終了年月7 = p終了日付[6];
                DateTime 終了年月8 = p終了日付[7];
                DateTime 終了年月9 = p終了日付[8];
                DateTime 終了年月10 = p終了日付[9];
                DateTime 終了年月11 = p終了日付[10];
                DateTime 終了年月12 = p終了日付[11];

                DateTime 前年開始年月1 = p開始日付[0].AddYears(-1);
                DateTime 前年開始年月2 = p開始日付[1].AddYears(-1);
                DateTime 前年開始年月3 = p開始日付[2].AddYears(-1);
                DateTime 前年開始年月4 = p開始日付[3].AddYears(-1);
                DateTime 前年開始年月5 = p開始日付[4].AddYears(-1);
                DateTime 前年開始年月6 = p開始日付[5].AddYears(-1);
                DateTime 前年開始年月7 = p開始日付[6].AddYears(-1);
                DateTime 前年開始年月8 = p開始日付[7].AddYears(-1);
                DateTime 前年開始年月9 = p開始日付[8].AddYears(-1);
                DateTime 前年開始年月10 = p開始日付[9].AddYears(-1);
                DateTime 前年開始年月11 = p開始日付[10].AddYears(-1);
                DateTime 前年開始年月12 = p開始日付[11].AddYears(-1);

                DateTime 前年終了年月1 = p終了日付[0].AddYears(-1);
                DateTime 前年終了年月2 = p終了日付[1].AddYears(-1);
                DateTime 前年終了年月3 = p終了日付[2].AddYears(-1);
                DateTime 前年終了年月4 = p終了日付[3].AddYears(-1);
                DateTime 前年終了年月5 = p終了日付[4].AddYears(-1);
                DateTime 前年終了年月6 = p終了日付[5].AddYears(-1);
                DateTime 前年終了年月7 = p終了日付[6].AddYears(-1);
                DateTime 前年終了年月8 = p終了日付[7].AddYears(-1);
                DateTime 前年終了年月9 = p終了日付[8].AddYears(-1);
                DateTime 前年終了年月10 = p終了日付[9].AddYears(-1);
                DateTime 前年終了年月11 = p終了日付[10].AddYears(-1);
                DateTime 前年終了年月12 = p終了日付[11].AddYears(-1);

                string s年月1 = p終了日付[0].Month.ToString() + "月";
                string s年月2 = p終了日付[1].Month.ToString() + "月";
                string s年月3 = p終了日付[2].Month.ToString() + "月";
                string s年月4 = p終了日付[3].Month.ToString() + "月";
                string s年月5 = p終了日付[4].Month.ToString() + "月";
                string s年月6 = p終了日付[5].Month.ToString() + "月";
                string s年月7 = p終了日付[6].Month.ToString() + "月";
                string s年月8 = p終了日付[7].Month.ToString() + "月";
                string s年月9 = p終了日付[8].Month.ToString() + "月";
                string s年月10 = p終了日付[9].Month.ToString() + "月";
                string s年月11 = p終了日付[10].Month.ToString() + "月";
                string s年月12 = p終了日付[11].Month.ToString() + "月";

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

                if (p終了日付[0].Month.ToString().Length == 1)
                {
                    i年月1 = AppCommon.IntParse(p終了日付[0].Year.ToString() + "0" + p終了日付[0].Month.ToString());
                }
                else
                {
                    i年月1 = AppCommon.IntParse(p終了日付[0].Year.ToString() + p終了日付[0].Month.ToString());
                }

                if (p終了日付[1].Month.ToString().Length == 1)
                {
                    i年月2 = AppCommon.IntParse(p終了日付[1].Year.ToString() + "0" + p終了日付[1].Month.ToString());
                }
                else
                {
                    i年月2 = AppCommon.IntParse(p終了日付[1].Year.ToString() + p終了日付[1].Month.ToString());
                }

                if (p終了日付[2].Month.ToString().Length == 1)
                {
                    i年月3 = AppCommon.IntParse(p終了日付[2].Year.ToString() + "0" + p終了日付[2].Month.ToString());
                }
                else
                {
                    i年月3 = AppCommon.IntParse(p終了日付[2].Year.ToString() + p終了日付[2].Month.ToString());
                }

                if (p終了日付[3].Month.ToString().Length == 1)
                {
                    i年月4 = AppCommon.IntParse(p終了日付[3].Year.ToString() + "0" + p終了日付[3].Month.ToString());
                }
                else
                {
                    i年月4 = AppCommon.IntParse(p終了日付[3].Year.ToString() + p終了日付[3].Month.ToString());
                }

                if (p終了日付[4].Month.ToString().Length == 1)
                {
                    i年月5 = AppCommon.IntParse(p終了日付[4].Year.ToString() + "0" + p終了日付[4].Month.ToString());
                }
                else
                {
                    i年月5 = AppCommon.IntParse(p終了日付[4].Year.ToString() + p終了日付[4].Month.ToString());
                }

                if (p終了日付[5].Month.ToString().Length == 1)
                {
                    i年月6 = AppCommon.IntParse(p終了日付[5].Year.ToString() + "0" + p終了日付[5].Month.ToString());
                }
                else
                {
                    i年月6 = AppCommon.IntParse(p終了日付[5].Year.ToString() + p終了日付[5].Month.ToString());
                }

                if (p終了日付[6].Month.ToString().Length == 1)
                {
                    i年月7 = AppCommon.IntParse(p終了日付[6].Year.ToString() + "0" + p終了日付[6].Month.ToString());
                }
                else
                {
                    i年月7 = AppCommon.IntParse(p終了日付[6].Year.ToString() + p終了日付[6].Month.ToString());
                }

                if (p終了日付[7].Month.ToString().Length == 1)
                {
                    i年月8 = AppCommon.IntParse(p終了日付[7].Year.ToString() + "0" + p終了日付[7].Month.ToString());
                }
                else
                {
                    i年月8 = AppCommon.IntParse(p終了日付[7].Year.ToString() + p終了日付[7].Month.ToString());
                }

                if (p終了日付[8].Month.ToString().Length == 1)
                {
                    i年月9 = AppCommon.IntParse(p終了日付[8].Year.ToString() + "0" + p終了日付[8].Month.ToString());
                }
                else
                {
                    i年月9 = AppCommon.IntParse(p終了日付[8].Year.ToString() + p終了日付[8].Month.ToString());
                }

                if (p終了日付[9].Month.ToString().Length == 1)
                {
                    i年月10 = AppCommon.IntParse(p終了日付[9].Year.ToString() + "0" + p終了日付[9].Month.ToString());
                }
                else
                {
                    i年月10 = AppCommon.IntParse(p終了日付[9].Year.ToString() + p終了日付[9].Month.ToString());
                }

                if (p終了日付[10].Month.ToString().Length == 1)
                {
                    i年月11 = AppCommon.IntParse(p終了日付[10].Year.ToString() + "0" + p終了日付[10].Month.ToString());
                }
                else
                {
                    i年月11 = AppCommon.IntParse(p終了日付[10].Year.ToString() + p終了日付[10].Month.ToString());
                }

                if (p終了日付[11].Month.ToString().Length == 1)
                {
                    i年月12 = AppCommon.IntParse(p終了日付[11].Year.ToString() + "0" + p終了日付[11].Month.ToString());
                }
                else
                {
                    i年月12 = AppCommon.IntParse(p終了日付[11].Year.ToString() + p終了日付[11].Month.ToString());
                }


                //List<TKS21011_Member1> query = new List<TKS21011_Member1>();

                context.Connection.Open();

                try
                {

                    #region 集計
                    ///総売上
                    var query = (from m71 in context.M71_BUM.Where(q => q.削除日付 == null)
                                 select new TKS21011_Member1
                                 {
                                     順序ID = 1,
                                     項目 = "総売上",
                                     部門ID = m71.自社部門ID,
                                     部門名 = m71.自社部門名,

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
                                     当年1 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月1 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年2 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月2 && t01.請求日付 <= 終了年月2 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年3 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月3 && t01.請求日付 <= 終了年月3 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年4 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月4 && t01.請求日付 <= 終了年月4 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年5 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月5 && t01.請求日付 <= 終了年月5 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年6 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月6 && t01.請求日付 <= 終了年月6 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年7 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月7 && t01.請求日付 <= 終了年月7 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年8 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月8 && t01.請求日付 <= 終了年月8 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年9 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月9 && t01.請求日付 <= 終了年月9 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年10 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月10 && t01.請求日付 <= 終了年月10 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年11 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月11 && t01.請求日付 <= 終了年月11 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年12 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月12 && t01.請求日付 <= 終了年月12 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     当年合計 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月12 select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),

                                     前年1 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月1 && t01.請求日付 <= 前年終了年月1) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年2 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月2 && t01.請求日付 <= 前年終了年月2) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年3 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月3 && t01.請求日付 <= 前年終了年月3) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年4 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月4 && t01.請求日付 <= 前年終了年月4) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年5 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月5 && t01.請求日付 <= 前年終了年月5) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年6 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月6 && t01.請求日付 <= 前年終了年月6) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年7 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月7 && t01.請求日付 <= 前年終了年月7) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年8 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月8 && t01.請求日付 <= 前年終了年月8) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年9 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月9 && t01.請求日付 <= 前年終了年月9) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年10 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月10 && t01.請求日付 <= 前年終了年月10) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年11 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月11 && t01.請求日付 <= 前年終了年月11) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年12 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月12 && t01.請求日付 <= 前年終了年月12) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                     前年合計 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月1 && t01.請求日付 <= 前年終了年月12) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),

                                     コードFrom = p部門From,
                                     コードTo = p部門To,
                                     ピックアップ指定 = s部門List,
                                     開始日付 = 開始年月1,
                                     終了日付 = 終了年月12,

                                 }).AsQueryable();

                    ///自社売上
                    query = query.Union(from m71 in context.M71_BUM.Where(q => q.削除日付 == null)
                                        select new TKS21011_Member1
                                        {
                                            順序ID = 2,
                                            項目 = "自社売上",
                                            部門ID = m71.自社部門ID,
                                            部門名 = m71.自社部門名,

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
                                            当年1 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月1 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年2 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月2 && t01.請求日付 <= 終了年月2 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年3 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月3 && t01.請求日付 <= 終了年月3 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年4 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月4 && t01.請求日付 <= 終了年月4 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年5 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月5 && t01.請求日付 <= 終了年月5 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年6 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月6 && t01.請求日付 <= 終了年月6 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年7 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月7 && t01.請求日付 <= 終了年月7 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年8 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月8 && t01.請求日付 <= 終了年月8 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年9 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月9 && t01.請求日付 <= 終了年月9 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年10 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月10 && t01.請求日付 <= 終了年月10 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年11 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月11 && t01.請求日付 <= 終了年月11 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年12 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月12 && t01.請求日付 <= 終了年月12 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年合計 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月12 && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),

                                            前年1 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月1 && t01.請求日付 <= 前年終了年月1) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年2 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月2 && t01.請求日付 <= 前年終了年月2) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年3 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月3 && t01.請求日付 <= 前年終了年月3) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年4 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月4 && t01.請求日付 <= 前年終了年月4) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年5 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月5 && t01.請求日付 <= 前年終了年月5) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年6 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月6 && t01.請求日付 <= 前年終了年月6) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年7 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月7 && t01.請求日付 <= 前年終了年月7) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年8 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月8 && t01.請求日付 <= 前年終了年月8) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年9 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月9 && t01.請求日付 <= 前年終了年月9) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年10 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月10 && t01.請求日付 <= 前年終了年月10) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年11 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月11 && t01.請求日付 <= 前年終了年月11) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年12 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月12 && t01.請求日付 <= 前年終了年月12) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年合計 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月1 && t01.請求日付 <= 前年終了年月12) && (t01.支払先KEY == null || t01.支払先KEY == 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),

                                            コードFrom = p部門From,
                                            コードTo = p部門To,
                                            ピックアップ指定 = s部門List,
                                            開始日付 = 開始年月1,
                                            終了日付 = 終了年月12,

                                        }).AsQueryable();

                    ///傭車売上
                    query = query.Union(from m71 in context.M71_BUM.Where(q => q.削除日付 == null)
                                        select new TKS21011_Member1
                                        {
                                            順序ID = 3,
                                            項目 = "傭車売上",
                                            部門ID = m71.自社部門ID,
                                            部門名 = m71.自社部門名,

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
                                            当年1 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月1 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年2 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月2 && t01.請求日付 <= 終了年月2 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年3 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月3 && t01.請求日付 <= 終了年月3 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年4 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月4 && t01.請求日付 <= 終了年月4 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年5 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月5 && t01.請求日付 <= 終了年月5 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年6 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月6 && t01.請求日付 <= 終了年月6 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年7 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月7 && t01.請求日付 <= 終了年月7 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年8 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月8 && t01.請求日付 <= 終了年月8 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年9 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月9 && t01.請求日付 <= 終了年月9 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年10 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月10 && t01.請求日付 <= 終了年月10 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年11 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月11 && t01.請求日付 <= 終了年月11 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年12 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月12 && t01.請求日付 <= 終了年月12 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            当年合計 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && t01.請求日付 >= 開始年月1 && t01.請求日付 <= 終了年月12 && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),

                                            前年1 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月1 && t01.請求日付 <= 前年終了年月1) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年2 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月2 && t01.請求日付 <= 前年終了年月2) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年3 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月3 && t01.請求日付 <= 前年終了年月3) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年4 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月4 && t01.請求日付 <= 前年終了年月4) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年5 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月5 && t01.請求日付 <= 前年終了年月5) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年6 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月6 && t01.請求日付 <= 前年終了年月6) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年7 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月7 && t01.請求日付 <= 前年終了年月7) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年8 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月8 && t01.請求日付 <= 前年終了年月8) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年9 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月9 && t01.請求日付 <= 前年終了年月9) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年10 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月10 && t01.請求日付 <= 前年終了年月10) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年11 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月11 && t01.請求日付 <= 前年終了年月11) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年12 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月12 && t01.請求日付 <= 前年終了年月12) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),
                                            前年合計 = (from t01 in context.T01_TRN where (t01.自社部門ID == m71.自社部門ID) && (t01.請求日付 >= 前年開始年月1 && t01.請求日付 <= 前年終了年月12) && (t01.支払先KEY != null && t01.支払先KEY != 0) select t01.売上金額 + t01.請求割増１ + t01.請求割増２).Sum(),

                                            コードFrom = p部門From,
                                            コードTo = p部門To,
                                            ピックアップ指定 = s部門List,
                                            開始日付 = 開始年月1,
                                            終了日付 = 終了年月12,

                                        }).AsQueryable();



                    //query.AsEnumerable().Where(q => q.月1 = )
                    #endregion

                    //TKS21011_DATASET dset = new TKS21011_DATASET()
                    //{
                    //    売上構成グラフ = query1,
                    //    得意先上位グラフ = query2,
                    //    傭車先上位グラフ = query3,
                    //    損益分岐点グラフ = query4,
                    //};


                    var query2 = (from q in query
                                  group q by q.部門ID into qGroup
                                  select new TKS21011_Member2
                                  {
                                      部門ID = qGroup.Key,
                                      総売上 = qGroup.Select(q => (q.当年合計 == null ? 0 : q.当年合計)).Sum(),

                                  }).AsQueryable();
                    query2 = query2.Where(q => q.総売上 != 0 && q.総売上 != null);

                    query = (from q in query
                             let qqlet = from qq in query2 select qq.部門ID
                             where qqlet.Contains(q.部門ID)
                             select q).AsQueryable();

                    #region 部門指定

                    var query3 = query;

                    //部門From処理　Min値
                    if (!string.IsNullOrEmpty(p部門From))
                    {
                        int i部門From = AppCommon.IntParse(p部門From);
                        query = query.Where(c => c.部門ID >= i部門From || c.部門ID == 999999999);
                    }

                    //部門To処理　Max値
                    if (!string.IsNullOrEmpty(p部門To))
                    {
                        int i部門TO = AppCommon.IntParse(p部門To);
                        query = query.Where(c => c.部門ID <= i部門TO || c.部門ID == 999999999);
                    }

                    if (p部門List.Length > 0)
                    {
                        if ((string.IsNullOrEmpty(p部門From)) && (string.IsNullOrEmpty(p部門To)))
                        {
                            query = query3.Where(q => p部門List.Contains(q.部門ID) || q.部門ID == 999999999);
                        }
                        else
                        {
                            query = query.Union(query3.Where(q => p部門List.Contains(q.部門ID) || q.部門ID == 999999999));
                        }
                    }
                    query = query.Distinct();

                    #endregion

                    query = query.OrderBy(q => q.部門ID).ThenBy(q => q.順序ID);

                    var result = query.ToList();

                    if (result != null)
                    {
                        result.Add(new TKS21011_Member1
                        {
                            順序ID = 1,
                            項目 = "総売上",
                            コードFrom = result.Where(c => c.順序ID == 1).Select(c => c.コードFrom).First(),
                            コードTo = result.Where(c => c.順序ID == 1).Select(c => c.コードTo).First(),
                            ピックアップ指定 = result.Where(c => c.順序ID == 1).Select(c => c.ピックアップ指定).First(),
                            開始日付 = result.Where(c => c.順序ID == 1).Select(c => c.開始日付).First(),
                            終了日付 = result.Where(c => c.順序ID == 1).Select(c => c.終了日付).First(),
                            部門ID = 99999,
                            部門名 = "【 合 計 】",
                            月1 = result.Where(c => c.順序ID == 1).Select(c => c.月1).First(),
                            月2 = result.Where(c => c.順序ID == 1).Select(c => c.月2).First(),
                            月3 = result.Where(c => c.順序ID == 1).Select(c => c.月3).First(),
                            月4 = result.Where(c => c.順序ID == 1).Select(c => c.月4).First(),
                            月5 = result.Where(c => c.順序ID == 1).Select(c => c.月5).First(),
                            月6 = result.Where(c => c.順序ID == 1).Select(c => c.月6).First(),
                            月7 = result.Where(c => c.順序ID == 1).Select(c => c.月7).First(),
                            月8 = result.Where(c => c.順序ID == 1).Select(c => c.月8).First(),
                            月9 = result.Where(c => c.順序ID == 1).Select(c => c.月9).First(),
                            月10 = result.Where(c => c.順序ID == 1).Select(c => c.月10).First(),
                            月11 = result.Where(c => c.順序ID == 1).Select(c => c.月11).First(),
                            月12 = result.Where(c => c.順序ID == 1).Select(c => c.月12).First(),
                            前年1 = result.Where(c => c.順序ID == 1).Sum(c => c.前年1),
                            前年2 = result.Where(c => c.順序ID == 1).Sum(c => c.前年2),
                            前年3 = result.Where(c => c.順序ID == 1).Sum(c => c.前年3),
                            前年4 = result.Where(c => c.順序ID == 1).Sum(c => c.前年4),
                            前年5 = result.Where(c => c.順序ID == 1).Sum(c => c.前年5),
                            前年6 = result.Where(c => c.順序ID == 1).Sum(c => c.前年6),
                            前年7 = result.Where(c => c.順序ID == 1).Sum(c => c.前年7),
                            前年8 = result.Where(c => c.順序ID == 1).Sum(c => c.前年8),
                            前年9 = result.Where(c => c.順序ID == 1).Sum(c => c.前年9),
                            前年10 = result.Where(c => c.順序ID == 1).Sum(c => c.前年10),
                            前年11 = result.Where(c => c.順序ID == 1).Sum(c => c.前年11),
                            前年12 = result.Where(c => c.順序ID == 1).Sum(c => c.前年12),
                            前年合計 = result.Where(c => c.順序ID == 1).Sum(c => c.前年合計),
                            当年1 = result.Where(c => c.順序ID == 1).Sum(c => c.当年1),
                            当年2 = result.Where(c => c.順序ID == 1).Sum(c => c.当年2),
                            当年3 = result.Where(c => c.順序ID == 1).Sum(c => c.当年3),
                            当年4 = result.Where(c => c.順序ID == 1).Sum(c => c.当年4),
                            当年5 = result.Where(c => c.順序ID == 1).Sum(c => c.当年5),
                            当年6 = result.Where(c => c.順序ID == 1).Sum(c => c.当年6),
                            当年7 = result.Where(c => c.順序ID == 1).Sum(c => c.当年7),
                            当年8 = result.Where(c => c.順序ID == 1).Sum(c => c.当年8),
                            当年9 = result.Where(c => c.順序ID == 1).Sum(c => c.当年9),
                            当年10 = result.Where(c => c.順序ID == 1).Sum(c => c.当年10),
                            当年11 = result.Where(c => c.順序ID == 1).Sum(c => c.当年11),
                            当年12 = result.Where(c => c.順序ID == 1).Sum(c => c.当年12),
                            当年合計 = result.Where(c => c.順序ID == 1).Sum(c => c.当年合計),
                            //対比1 = result.Sum(c => c.対比1),
                            //対比2 = result.Sum(c => c.対比2),
                            //対比3 = result.Sum(c => c.対比3),
                            //対比4 = result.Sum(c => c.対比4),
                            //対比5 = result.Sum(c => c.対比5),
                            //対比6 = result.Sum(c => c.対比6),
                            //対比7 = result.Sum(c => c.対比7),
                            //対比8 = result.Sum(c => c.対比8),
                            //対比9 = result.Sum(c => c.対比9),
                            //対比10 = result.Sum(c => c.対比10),
                            //対比11 = result.Sum(c => c.対比11),
                            //対比12 = result.Sum(c => c.対比12),
                            //対比合計 = result.Sum(c => c.対比合計),
                        });
                        result.Add(new TKS21011_Member1
                        {
                            順序ID = 2,
                            項目 = "自社売上",
                            コードFrom = result.Where(c => c.順序ID == 2).Select(c => c.コードFrom).First(),
                            コードTo = result.Where(c => c.順序ID == 2).Select(c => c.コードTo).First(),
                            ピックアップ指定 = result.Where(c => c.順序ID == 2).Select(c => c.ピックアップ指定).First(),
                            開始日付 = result.Where(c => c.順序ID == 2).Select(c => c.開始日付).First(),
                            終了日付 = result.Where(c => c.順序ID == 2).Select(c => c.終了日付).First(),
                            部門ID = 99999,
                            部門名 = "【 合 計 】",
                            月1 = result.Where(c => c.順序ID == 2).Select(c => c.月1).First(),
                            月2 = result.Where(c => c.順序ID == 2).Select(c => c.月2).First(),
                            月3 = result.Where(c => c.順序ID == 2).Select(c => c.月3).First(),
                            月4 = result.Where(c => c.順序ID == 2).Select(c => c.月4).First(),
                            月5 = result.Where(c => c.順序ID == 2).Select(c => c.月5).First(),
                            月6 = result.Where(c => c.順序ID == 2).Select(c => c.月6).First(),
                            月7 = result.Where(c => c.順序ID == 2).Select(c => c.月7).First(),
                            月8 = result.Where(c => c.順序ID == 2).Select(c => c.月8).First(),
                            月9 = result.Where(c => c.順序ID == 2).Select(c => c.月9).First(),
                            月10 = result.Where(c => c.順序ID == 2).Select(c => c.月10).First(),
                            月11 = result.Where(c => c.順序ID == 2).Select(c => c.月11).First(),
                            月12 = result.Where(c => c.順序ID == 2).Select(c => c.月12).First(),
                            前年1 = result.Where(c => c.順序ID == 2).Sum(c => c.前年1),
                            前年2 = result.Where(c => c.順序ID == 2).Sum(c => c.前年2),
                            前年3 = result.Where(c => c.順序ID == 2).Sum(c => c.前年3),
                            前年4 = result.Where(c => c.順序ID == 2).Sum(c => c.前年4),
                            前年5 = result.Where(c => c.順序ID == 2).Sum(c => c.前年5),
                            前年6 = result.Where(c => c.順序ID == 2).Sum(c => c.前年6),
                            前年7 = result.Where(c => c.順序ID == 2).Sum(c => c.前年7),
                            前年8 = result.Where(c => c.順序ID == 2).Sum(c => c.前年8),
                            前年9 = result.Where(c => c.順序ID == 2).Sum(c => c.前年9),
                            前年10 = result.Where(c => c.順序ID == 2).Sum(c => c.前年10),
                            前年11 = result.Where(c => c.順序ID == 2).Sum(c => c.前年11),
                            前年12 = result.Where(c => c.順序ID == 2).Sum(c => c.前年12),
                            前年合計 = result.Where(c => c.順序ID == 2).Sum(c => c.前年合計),
                            当年1 = result.Where(c => c.順序ID == 2).Sum(c => c.当年1),
                            当年2 = result.Where(c => c.順序ID == 2).Sum(c => c.当年2),
                            当年3 = result.Where(c => c.順序ID == 2).Sum(c => c.当年3),
                            当年4 = result.Where(c => c.順序ID == 2).Sum(c => c.当年4),
                            当年5 = result.Where(c => c.順序ID == 2).Sum(c => c.当年5),
                            当年6 = result.Where(c => c.順序ID == 2).Sum(c => c.当年6),
                            当年7 = result.Where(c => c.順序ID == 2).Sum(c => c.当年7),
                            当年8 = result.Where(c => c.順序ID == 2).Sum(c => c.当年8),
                            当年9 = result.Where(c => c.順序ID == 2).Sum(c => c.当年9),
                            当年10 = result.Where(c => c.順序ID == 2).Sum(c => c.当年10),
                            当年11 = result.Where(c => c.順序ID == 2).Sum(c => c.当年11),
                            当年12 = result.Where(c => c.順序ID == 2).Sum(c => c.当年12),
                            当年合計 = result.Where(c => c.順序ID == 2).Sum(c => c.当年合計),
                            //対比1 = result.Sum(c => c.対比1),
                            //対比2 = result.Sum(c => c.対比2),
                            //対比3 = result.Sum(c => c.対比3),
                            //対比4 = result.Sum(c => c.対比4),
                            //対比5 = result.Sum(c => c.対比5),
                            //対比6 = result.Sum(c => c.対比6),
                            //対比7 = result.Sum(c => c.対比7),
                            //対比8 = result.Sum(c => c.対比8),
                            //対比9 = result.Sum(c => c.対比9),
                            //対比10 = result.Sum(c => c.対比10),
                            //対比11 = result.Sum(c => c.対比11),
                            //対比12 = result.Sum(c => c.対比12),
                            //対比合計 = result.Sum(c => c.対比合計),
                        });
                        result.Add(new TKS21011_Member1
                        {
                            順序ID = 3,
                            項目 = "傭車売上",
                            コードFrom = result.Where(c => c.順序ID == 3).Select(c => c.コードFrom).First(),
                            コードTo = result.Where(c => c.順序ID == 3).Select(c => c.コードTo).First(),
                            ピックアップ指定 = result.Where(c => c.順序ID == 3).Select(c => c.ピックアップ指定).First(),
                            開始日付 = result.Where(c => c.順序ID == 3).Select(c => c.開始日付).First(),
                            終了日付 = result.Where(c => c.順序ID == 3).Select(c => c.終了日付).First(),
                            部門ID = 99999,
                            部門名 = "【 合 計 】",
                            月1 = result.Where(c => c.順序ID == 3).Select(c => c.月1).First(),
                            月2 = result.Where(c => c.順序ID == 3).Select(c => c.月2).First(),
                            月3 = result.Where(c => c.順序ID == 3).Select(c => c.月3).First(),
                            月4 = result.Where(c => c.順序ID == 3).Select(c => c.月4).First(),
                            月5 = result.Where(c => c.順序ID == 3).Select(c => c.月5).First(),
                            月6 = result.Where(c => c.順序ID == 3).Select(c => c.月6).First(),
                            月7 = result.Where(c => c.順序ID == 3).Select(c => c.月7).First(),
                            月8 = result.Where(c => c.順序ID == 3).Select(c => c.月8).First(),
                            月9 = result.Where(c => c.順序ID == 3).Select(c => c.月9).First(),
                            月10 = result.Where(c => c.順序ID == 3).Select(c => c.月10).First(),
                            月11 = result.Where(c => c.順序ID == 3).Select(c => c.月11).First(),
                            月12 = result.Where(c => c.順序ID == 3).Select(c => c.月12).First(),
                            前年1 = result.Where(c => c.順序ID == 3).Sum(c => c.前年1),
                            前年2 = result.Where(c => c.順序ID == 3).Sum(c => c.前年2),
                            前年3 = result.Where(c => c.順序ID == 3).Sum(c => c.前年3),
                            前年4 = result.Where(c => c.順序ID == 3).Sum(c => c.前年4),
                            前年5 = result.Where(c => c.順序ID == 3).Sum(c => c.前年5),
                            前年6 = result.Where(c => c.順序ID == 3).Sum(c => c.前年6),
                            前年7 = result.Where(c => c.順序ID == 3).Sum(c => c.前年7),
                            前年8 = result.Where(c => c.順序ID == 3).Sum(c => c.前年8),
                            前年9 = result.Where(c => c.順序ID == 3).Sum(c => c.前年9),
                            前年10 = result.Where(c => c.順序ID == 3).Sum(c => c.前年10),
                            前年11 = result.Where(c => c.順序ID == 3).Sum(c => c.前年11),
                            前年12 = result.Where(c => c.順序ID == 3).Sum(c => c.前年12),
                            前年合計 = result.Where(c => c.順序ID == 3).Sum(c => c.前年合計),
                            当年1 = result.Where(c => c.順序ID == 3).Sum(c => c.当年1),
                            当年2 = result.Where(c => c.順序ID == 3).Sum(c => c.当年2),
                            当年3 = result.Where(c => c.順序ID == 3).Sum(c => c.当年3),
                            当年4 = result.Where(c => c.順序ID == 3).Sum(c => c.当年4),
                            当年5 = result.Where(c => c.順序ID == 3).Sum(c => c.当年5),
                            当年6 = result.Where(c => c.順序ID == 3).Sum(c => c.当年6),
                            当年7 = result.Where(c => c.順序ID == 3).Sum(c => c.当年7),
                            当年8 = result.Where(c => c.順序ID == 3).Sum(c => c.当年8),
                            当年9 = result.Where(c => c.順序ID == 3).Sum(c => c.当年9),
                            当年10 = result.Where(c => c.順序ID == 3).Sum(c => c.当年10),
                            当年11 = result.Where(c => c.順序ID == 3).Sum(c => c.当年11),
                            当年12 = result.Where(c => c.順序ID == 3).Sum(c => c.当年12),
                            当年合計 = result.Where(c => c.順序ID == 3).Sum(c => c.当年合計),
                            //対比1 = result.Sum(c => c.対比1),
                            //対比2 = result.Sum(c => c.対比2),
                            //対比3 = result.Sum(c => c.対比3),
                            //対比4 = result.Sum(c => c.対比4),
                            //対比5 = result.Sum(c => c.対比5),
                            //対比6 = result.Sum(c => c.対比6),
                            //対比7 = result.Sum(c => c.対比7),
                            //対比8 = result.Sum(c => c.対比8),
                            //対比9 = result.Sum(c => c.対比9),
                            //対比10 = result.Sum(c => c.対比10),
                            //対比11 = result.Sum(c => c.対比11),
                            //対比12 = result.Sum(c => c.対比12),
                            //対比合計 = result.Sum(c => c.対比合計),
                        });
                    }

                    foreach (TKS21011_Member1 row in result)
                    {
                        row.対比1 = row.前年1 == 0 ? 0 : row.前年1 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年1 == null ? 0 : row.当年1) / (row.前年1 == null ? 0 : row.前年1) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比2 = row.前年2 == 0 ? 0 : row.前年2 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年2 == null ? 0 : row.当年2) / (row.前年2 == null ? 0 : row.前年2) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比3 = row.前年3 == 0 ? 0 : row.前年3 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年3 == null ? 0 : row.当年3) / (row.前年3 == null ? 0 : row.前年3) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比4 = row.前年4 == 0 ? 0 : row.前年4 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年4 == null ? 0 : row.当年4) / (row.前年4 == null ? 0 : row.前年4) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比5 = row.前年5 == 0 ? 0 : row.前年5 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年5 == null ? 0 : row.当年5) / (row.前年5 == null ? 0 : row.前年5) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比6 = row.前年6 == 0 ? 0 : row.前年6 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年6 == null ? 0 : row.当年6) / (row.前年6 == null ? 0 : row.前年6) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比7 = row.前年7 == 0 ? 0 : row.前年7 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年7 == null ? 0 : row.当年7) / (row.前年7 == null ? 0 : row.前年7) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比8 = row.前年8 == 0 ? 0 : row.前年8 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年8 == null ? 0 : row.当年8) / (row.前年8 == null ? 0 : row.前年8) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比9 = row.前年9 == 0 ? 0 : row.前年9 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年9 == null ? 0 : row.当年9) / (row.前年9 == null ? 0 : row.前年9) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比10 = row.前年10 == 0 ? 0 : row.前年10 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年10 == null ? 0 : row.当年10) / (row.前年10 == null ? 0 : row.前年10) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比11 = row.前年11 == 0 ? 0 : row.前年11 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年11 == null ? 0 : row.当年11) / (row.前年11 == null ? 0 : row.前年11) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比12 = row.前年12 == 0 ? 0 : row.前年12 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年12 == null ? 0 : row.当年12) / (row.前年12 == null ? 0 : row.前年12) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                        row.対比合計 = row.前年合計 == 0 ? 0 : row.前年合計 == null ? 0 : Math.Round(AppCommon.DecimalParse(((row.当年合計 == null ? 0 : row.当年合計) / (row.前年合計 == null ? 0 : row.前年合計) * 100).ToString()), 1, MidpointRounding.AwayFromZero);
                    };


                    List<TKS21011_CSV> result_csv = new List<TKS21011_CSV>();

                    foreach (TKS21011_Member1 row in result)
                    {
                        result_csv.Add(new TKS21011_CSV
                        {
                            部門ID = row.部門ID,
                            部門名 = row.部門名,
                            項目 = row.順序ID == 1 ? "総売上" : row.順序ID == 2 ? "自社売上" : row.順序ID == 3 ? "傭車売上" : "",
                            項目2 = "当年",
                            月1 = row.当年1 == null ? 0 : (decimal)row.当年1,
                            月2 = row.当年2 == null ? 0 : (decimal)row.当年2,
                            月3 = row.当年3 == null ? 0 : (decimal)row.当年3,
                            月4 = row.当年4 == null ? 0 : (decimal)row.当年4,
                            月5 = row.当年5 == null ? 0 : (decimal)row.当年5,
                            月6 = row.当年6 == null ? 0 : (decimal)row.当年6,
                            月7 = row.当年7 == null ? 0 : (decimal)row.当年7,
                            月8 = row.当年8 == null ? 0 : (decimal)row.当年8,
                            月9 = row.当年9 == null ? 0 : (decimal)row.当年9,
                            月10 = row.当年10 == null ? 0 : (decimal)row.当年10,
                            月11 = row.当年11 == null ? 0 : (decimal)row.当年11,
                            月12 = row.当年12 == null ? 0 : (decimal)row.当年12,
                            合計 = row.当年合計 == null ? 0 : (decimal)row.当年合計,
                            月1項目 = row.月1,
                            月2項目 = row.月2,
                            月3項目 = row.月3,
                            月4項目 = row.月4,
                            月5項目 = row.月5,
                            月6項目 = row.月6,
                            月7項目 = row.月7,
                            月8項目 = row.月8,
                            月9項目 = row.月9,
                            月10項目 = row.月10,
                            月11項目 = row.月11,
                            月12項目 = row.月12,
                        });
                        result_csv.Add(new TKS21011_CSV
                        {
                            部門ID = row.部門ID,
                            部門名 = row.部門名,
                            項目 = row.順序ID == 1 ? "総売上" : row.順序ID == 2 ? "自社売上" : row.順序ID == 3 ? "傭車売上" : "",
                            項目2 = "前年",
                            月1 = row.前年1 == null ? 0 : (decimal)row.前年1,
                            月2 = row.前年2 == null ? 0 : (decimal)row.前年2,
                            月3 = row.前年3 == null ? 0 : (decimal)row.前年3,
                            月4 = row.前年4 == null ? 0 : (decimal)row.前年4,
                            月5 = row.前年5 == null ? 0 : (decimal)row.前年5,
                            月6 = row.前年6 == null ? 0 : (decimal)row.前年6,
                            月7 = row.前年7 == null ? 0 : (decimal)row.前年7,
                            月8 = row.前年8 == null ? 0 : (decimal)row.前年8,
                            月9 = row.前年9 == null ? 0 : (decimal)row.前年9,
                            月10 = row.前年10 == null ? 0 : (decimal)row.前年10,
                            月11 = row.前年11 == null ? 0 : (decimal)row.前年11,
                            月12 = row.前年12 == null ? 0 : (decimal)row.前年12,
                            合計 = row.前年合計 == null ? 0 : (decimal)row.前年合計,
                            月1項目 = row.月1,
                            月2項目 = row.月2,
                            月3項目 = row.月3,
                            月4項目 = row.月4,
                            月5項目 = row.月5,
                            月6項目 = row.月6,
                            月7項目 = row.月7,
                            月8項目 = row.月8,
                            月9項目 = row.月9,
                            月10項目 = row.月10,
                            月11項目 = row.月11,
                            月12項目 = row.月12,
                        });
                        result_csv.Add(new TKS21011_CSV
                        {
                            部門ID = row.部門ID,
                            部門名 = row.部門名,
                            項目 = row.順序ID == 1 ? "総売上" : row.順序ID == 2 ? "自社売上" : row.順序ID == 3 ? "傭車売上" : "",
                            項目2 = "前年対比",
                            月1 = row.対比1 == null ? 0 : (decimal)row.対比1,
                            月2 = row.対比2 == null ? 0 : (decimal)row.対比2,
                            月3 = row.対比3 == null ? 0 : (decimal)row.対比3,
                            月4 = row.対比4 == null ? 0 : (decimal)row.対比4,
                            月5 = row.対比5 == null ? 0 : (decimal)row.対比5,
                            月6 = row.対比6 == null ? 0 : (decimal)row.対比6,
                            月7 = row.対比7 == null ? 0 : (decimal)row.対比7,
                            月8 = row.対比8 == null ? 0 : (decimal)row.対比8,
                            月9 = row.対比9 == null ? 0 : (decimal)row.対比9,
                            月10 = row.対比10 == null ? 0 : (decimal)row.対比10,
                            月11 = row.対比11 == null ? 0 : (decimal)row.対比11,
                            月12 = row.対比12 == null ? 0 : (decimal)row.対比12,
                            合計 = row.対比合計 == null ? 0 : (decimal)row.対比合計,
                            月1項目 = row.月1,
                            月2項目 = row.月2,
                            月3項目 = row.月3,
                            月4項目 = row.月4,
                            月5項目 = row.月5,
                            月6項目 = row.月6,
                            月7項目 = row.月7,
                            月8項目 = row.月8,
                            月9項目 = row.月9,
                            月10項目 = row.月10,
                            月11項目 = row.月11,
                            月12項目 = row.月12,
                        });
                    };





                    return result_csv;

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