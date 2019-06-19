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
    public class NNG04010
    {

        #region メンバー定義
        /// <summary>
        /// NNG04010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class NNG04010_Member
        {
            [DataMember]
            public string 作成年度1 { get; set; }
            [DataMember]
            public string 作成年度2 { get; set; }
            [DataMember]
            public int 車輌コード { get; set; }
            [DataMember]
            public string 車番 { get; set; }
            [DataMember]
            public string 車種名 { get; set; }
            [DataMember]
            public string 区分 { get; set; }
            [DataMember]
            public decimal? 月1 { get; set; }
            [DataMember]
            public decimal? 月2 { get; set; }
            [DataMember]
            public decimal? 月3 { get; set; }
            [DataMember]
            public decimal? 月4 { get; set; }
            [DataMember]
            public decimal? 月5 { get; set; }
            [DataMember]
            public decimal? 月6 { get; set; }
            [DataMember]
            public decimal? 月7 { get; set; }
            [DataMember]
            public decimal? 月8 { get; set; }
            [DataMember]
            public decimal? 月9 { get; set; }
            [DataMember]
            public decimal? 月10 { get; set; }
            [DataMember]
            public decimal? 月11 { get; set; }
            [DataMember]
			public decimal? 月12 { get; set; }
			public string 月名1 { get; set; }
			public string 月名2 { get; set; }
			public string 月名3 { get; set; }
			public string 月名4 { get; set; }
			public string 月名5 { get; set; }
			public string 月名6 { get; set; }
			public string 月名7 { get; set; }
			public string 月名8 { get; set; }
			public string 月名9 { get; set; }
			public string 月名10 { get; set; }
			public string 月名11 { get; set; }
			public string 月名12 { get; set; }
            [DataMember]
            public decimal? 年間合計 { get; set; }
            [DataMember]
            public decimal? 平均 { get; set; }
            [DataMember]
            public decimal? 構成比 { get; set; }
            [DataMember]
            public string 表示区分 { get; set; }
            [DataMember]
            public string 表示順序 { get; set; }
            [DataMember]
            public decimal? 当年売上 { get; set; }
            [DataMember]
            public decimal? 売上順位データ { get; set; }
            [DataMember]
            public string 車輌指定 { get; set; }
            [DataMember]
            public string 車輌ﾋﾟｯｸｱｯﾌﾟ { get; set; }
            [DataMember]
            public decimal? 総年合計 { get; set; }
        }

        public class NNG03010g
        {
            [DataMember]
            public int 車輌ID { get; set; }
            [DataMember]
            public decimal? 当年合計 { get; set; }
            [DataMember]
            public decimal? 前年合計 { get; set; }
            [DataMember]
            public decimal? 前々年合計 { get; set; }
        }

        public class Total
        {
            [DataMember]
            public decimal? 当年合計 { get; set; }
            [DataMember]
            public decimal? 前年合計 { get; set; }
            [DataMember]
            public decimal? 前々年合計 { get; set; }
        }

        #endregion

        #region メンバー定義_CSV
        /// <summary>
        /// NNG04010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class NNG03010_Member_CSV
        {
            [DataMember]
            public string 作成年度1 { get; set; }
            [DataMember]
            public string 作成年度2 { get; set; }
            [DataMember]
            public int 乗務員コード { get; set; }
            [DataMember]
            public string 運転者名 { get; set; }
            [DataMember]
            public string 区分 { get; set; }
            [DataMember]
            public decimal? 月1 { get; set; }
            [DataMember]
            public decimal? 月2 { get; set; }
            [DataMember]
            public decimal? 月3 { get; set; }
            [DataMember]
            public decimal? 月4 { get; set; }
            [DataMember]
            public decimal? 月5 { get; set; }
            [DataMember]
            public decimal? 月6 { get; set; }
            [DataMember]
            public decimal? 月7 { get; set; }
            [DataMember]
            public decimal? 月8 { get; set; }
            [DataMember]
            public decimal? 月9 { get; set; }
            [DataMember]
            public decimal? 月10 { get; set; }
            [DataMember]
            public decimal? 月11 { get; set; }
            [DataMember]
			public decimal? 月12 { get; set; }
			public string 月名1 { get; set; }
			public string 月名2 { get; set; }
			public string 月名3 { get; set; }
			public string 月名4 { get; set; }
			public string 月名5 { get; set; }
			public string 月名6 { get; set; }
			public string 月名7 { get; set; }
			public string 月名8 { get; set; }
			public string 月名9 { get; set; }
			public string 月名10 { get; set; }
			public string 月名11 { get; set; }
			public string 月名12 { get; set; }
            [DataMember]
            public decimal? 年合計 { get; set; }
            [DataMember]
            public decimal? 平均 { get; set; }
            [DataMember]
            public decimal? 構成比 { get; set; }
            [DataMember]
            public string 表示区分 { get; set; }
            [DataMember]
            public string 表示順序 { get; set; }
            [DataMember]
            public int? 当年売上 { get; set; }
            [DataMember]
            public decimal? 売上順位データ { get; set; }
            [DataMember]
            public string 乗務員指定 { get; set; }
        }

        public class NNG03010g_CSV
        {
            [DataMember]
            public int 乗務員コード { get; set; }
            [DataMember]
            public decimal? 当年合計 { get; set; }
            [DataMember]
            public decimal? 前年合計 { get; set; }
            [DataMember]
            public decimal? 前々年合計 { get; set; }
        }

        public class Total_CSV
        {
            [DataMember]
            public decimal? 当年合計 { get; set; }
            [DataMember]
            public decimal? 前年合計 { get; set; }
            [DataMember]
            public decimal? 前々年合計 { get; set; }
        }

        #endregion


        #region 車輌別月別売上合計表
        /// <summary>
        /// NNG04010 車輌月別売上合計表
        /// </summary>
        /// <param name="車輌ID">車輌コード</param>
        /// <returns>S02</returns>
        public List<NNG04010_Member> NNG04010_GetDataHinList(string s車輌From, string s車輌To, int?[] i車輌List, int i表示区分, int i表示順序, bool 前年前々年,
                                                     string s作成年月度1, string s作成年月度2, int[] d開始年月日, int[] d前年開始年月日, int[] d前々年開始年月日)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<NNG04010_Member> retList = new List<NNG04010_Member>();
                context.Connection.Open();

                string 車輌ﾋﾟｯｸｱｯﾌﾟ指定 = string.Empty;

                #region 時間定義

                int 開始年月日1 = d開始年月日[0];
                int 開始年月日2 = d開始年月日[1];
                int 開始年月日3 = d開始年月日[2];
                int 開始年月日4 = d開始年月日[3];
                int 開始年月日5 = d開始年月日[4];
                int 開始年月日6 = d開始年月日[5];
                int 開始年月日7 = d開始年月日[6];
                int 開始年月日8 = d開始年月日[7];
                int 開始年月日9 = d開始年月日[8];
                int 開始年月日10 = d開始年月日[9];
                int 開始年月日11 = d開始年月日[10];
                int 開始年月日12 = d開始年月日[11];

                //前年
                int 前年開始年月日1 = d前年開始年月日[0];
                int 前年開始年月日2 = d前年開始年月日[1];
                int 前年開始年月日3 = d前年開始年月日[2];
                int 前年開始年月日4 = d前年開始年月日[3];
                int 前年開始年月日5 = d前年開始年月日[4];
                int 前年開始年月日6 = d前年開始年月日[5];
                int 前年開始年月日7 = d前年開始年月日[6];
                int 前年開始年月日8 = d前年開始年月日[7];
                int 前年開始年月日9 = d前年開始年月日[8];
                int 前年開始年月日10 = d前年開始年月日[9];
                int 前年開始年月日11 = d前年開始年月日[10];
                int 前年開始年月日12 = d前年開始年月日[11];

                //前々年
                int 前々年開始年月日1 = d前々年開始年月日[0];
                int 前々年開始年月日2 = d前々年開始年月日[1];
                int 前々年開始年月日3 = d前々年開始年月日[2];
                int 前々年開始年月日4 = d前々年開始年月日[3];
                int 前々年開始年月日5 = d前々年開始年月日[4];
                int 前々年開始年月日6 = d前々年開始年月日[5];
                int 前々年開始年月日7 = d前々年開始年月日[6];
                int 前々年開始年月日8 = d前々年開始年月日[7];
                int 前々年開始年月日9 = d前々年開始年月日[8];
                int 前々年開始年月日10 = d前々年開始年月日[9];
                int 前々年開始年月日11 = d前々年開始年月日[10];
                int 前々年開始年月日12 = d前々年開始年月日[11];

				//月名
                //DateTime d年月日 = DateTime.Parse(開始年月日1.ToString().Substring(0, 4) + "/" + 開始年月日1.ToString().Substring(4, 2) + "/" + "01");
                DateTime d年月日;
                DateTime.TryParse(開始年月日1.ToString().Substring(0, 4) + "/" + 開始年月日1.ToString().Substring(4, 2) + "/" + "01", out d年月日);
				
                string s月名1 = d年月日.Month + "月";
				string s月名2 = d年月日.AddMonths(1).Month + "月";
				string s月名3 = d年月日.AddMonths(2).Month + "月";
				string s月名4 = d年月日.AddMonths(3).Month + "月";
				string s月名5 = d年月日.AddMonths(4).Month + "月";
				string s月名6 = d年月日.AddMonths(5).Month + "月";
				string s月名7 = d年月日.AddMonths(6).Month + "月";
				string s月名8 = d年月日.AddMonths(7).Month + "月";
				string s月名9 = d年月日.AddMonths(8).Month + "月";
				string s月名10 = d年月日.AddMonths(9).Month + "月";
				string s月名11 = d年月日.AddMonths(10).Month + "月";
				string s月名12 = d年月日.AddMonths(11).Month + "月";


                #endregion

                #region 当年データ


                var query = (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                             from m05 in context.M05_CAR.Where(c => c.車種ID == m06.車種ID && c.廃車区分 == 0 && c.削除日付 == null)
                             join V01 in context.V_車輌月別売上合計表 on m05.車輌ID equals V01.車輌ID into V01Group
                             select new NNG04010_Member
                             {
                                 作成年度1 = s作成年月度1,
                                 作成年度2 = s作成年月度2,
                                 車輌コード = m05.車輌ID,
                                 車番 = m05.車輌番号,
                                 車種名 = m06.車種名,
                                 区分 = 前年前々年 == false ? "" : "当年",
                                 月1 = V01Group.Where(c => c.集計年月 == 開始年月日1).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 開始年月日1).Sum(c => c.運送収入),
                                 月2 = V01Group.Where(c => c.集計年月 == 開始年月日2).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 開始年月日2).Sum(c => c.運送収入),
                                 月3 = V01Group.Where(c => c.集計年月 == 開始年月日3).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 開始年月日3).Sum(c => c.運送収入),
                                 月4 = V01Group.Where(c => c.集計年月 == 開始年月日4).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 開始年月日4).Sum(c => c.運送収入),
                                 月5 = V01Group.Where(c => c.集計年月 == 開始年月日5).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 開始年月日5).Sum(c => c.運送収入),
                                 月6 = V01Group.Where(c => c.集計年月 == 開始年月日6).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 開始年月日6).Sum(c => c.運送収入),
                                 月7 = V01Group.Where(c => c.集計年月 == 開始年月日7).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 開始年月日7).Sum(c => c.運送収入),
                                 月8 = V01Group.Where(c => c.集計年月 == 開始年月日8).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 開始年月日8).Sum(c => c.運送収入),
                                 月9 = V01Group.Where(c => c.集計年月 == 開始年月日9).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 開始年月日9).Sum(c => c.運送収入),
                                 月10 = V01Group.Where(c => c.集計年月 == 開始年月日10).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 開始年月日10).Sum(c => c.運送収入),
                                 月11 = V01Group.Where(c => c.集計年月 == 開始年月日11).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 開始年月日11).Sum(c => c.運送収入),
								 月12 = V01Group.Where(c => c.集計年月 == 開始年月日12).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 開始年月日12).Sum(c => c.運送収入),
								 月名1 = s月名1,
								 月名2 = s月名2,
								 月名3 = s月名3,
								 月名4 = s月名4,
								 月名5 = s月名5,
								 月名6 = s月名6,
								 月名7 = s月名7,
								 月名8 = s月名8,
								 月名9 = s月名9,
								 月名10 = s月名10,
								 月名11 = s月名11,
								 月名12 = s月名12,
                                 表示区分 = i表示区分 == 0 ? "(売上あり乗務員のみ)" : "(売上無し乗務員含む)",
                                 表示順序 = i表示順序 == 0 ? "車輌ID" : i表示順序 == 1 ? "合計金額" : "",
                                 年間合計 = V01Group.Where(c => c.集計年月 >= 開始年月日1 && c.集計年月 <= 開始年月日12).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 >= 開始年月日1 && c.集計年月 <= 開始年月日12).Sum(c => c.運送収入),
                                 平均 = V01Group.Where(c => c.集計年月 >= 開始年月日1 && c.集計年月 <= 開始年月日12).Count(c => c.運送収入 != 0) == 0 ? 0 : V01Group.Where(c => c.集計年月 >= 開始年月日1 && c.集計年月 <= 開始年月日12).Sum(c => c.運送収入)/ V01Group.Where(c => c.集計年月 >= 開始年月日1 && c.集計年月 <= 開始年月日12).Count(c => c.運送収入 != 0),                                                                                                                                                                                                                                                             
                                 構成比 = V01Group.Where(v01 => v01.集計年月 >= 開始年月日1 && v01.集計年月 <= 開始年月日12).Sum(c => c.運送収入) / context.V_車輌月別売上合計表.Where(c => c.集計年月 >= 開始年月日1 && c.集計年月 <= 開始年月日12).Sum(c => c.運送収入) * 100 == null ? 0 : V01Group.Where(v01 => v01.集計年月 >= 開始年月日1 && v01.集計年月 <= 開始年月日12).Sum(c => c.運送収入) / context.V_車輌月別売上合計表.Where(c => c.集計年月 >= 開始年月日1 && c.集計年月 <= 開始年月日12).Sum(c => c.運送収入) * 100,
                                 当年売上 = V01Group.Where(c => c.集計年月 >= 開始年月日1 && c.集計年月 <= 開始年月日12).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 >= 開始年月日1 && c.集計年月 <= 開始年月日12).Sum(c => c.運送収入),
                                 売上順位データ = 前年前々年 == false ? V01Group.Where(c => c.集計年月 >= 開始年月日1 && c.集計年月 <= 開始年月日12).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 >= 前々年開始年月日12 && c.集計年月 <= 開始年月日12).Sum(c => c.運送収入) : V01Group.Where(c => c.集計年月 >= 前々年開始年月日12 && c.集計年月 <= 開始年月日12).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 >= 前々年開始年月日12 && c.集計年月 <= 開始年月日12).Sum(c => c.運送収入),
                                 車輌指定 = s車輌From + "～" + s車輌To,
                                 車輌ﾋﾟｯｸｱｯﾌﾟ = 車輌ﾋﾟｯｸｱｯﾌﾟ指定 == "" ? "無" : 車輌ﾋﾟｯｸｱｯﾌﾟ指定,
                                 総年合計 = 0,
                             }).AsQueryable();
                //車輌指定の表示
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
                #endregion


                #region 前年データ

                //＜＜＜前年データ集計＞＞＞
                if (前年前々年 == true)
                {
                    query = query.Union(from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                        from m05 in context.M05_CAR.Where(c => c.車種ID == m06.車種ID && c.廃車区分 == 0 && c.削除日付 == null)
                                        join V01 in context.V_車輌月別売上合計表 on m05.車輌ID equals V01.車輌ID into V01Group
                                        select new NNG04010_Member
                                        {
                                            作成年度1 = s作成年月度1,
                                            作成年度2 = s作成年月度2,
                                            車輌コード = m05.車輌ID,
                                            車番 = m05.車輌番号,
                                            車種名 = m06.車種名,
                                            区分 = 前年前々年 == false ? "" : "前年",
                                            月1 = V01Group.Where(c => c.集計年月 == 前年開始年月日1).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前年開始年月日1).Sum(c => c.運送収入),
                                            月2 = V01Group.Where(c => c.集計年月 == 前年開始年月日2).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前年開始年月日2).Sum(c => c.運送収入),
                                            月3 = V01Group.Where(c => c.集計年月 == 前年開始年月日3).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前年開始年月日3).Sum(c => c.運送収入),
                                            月4 = V01Group.Where(c => c.集計年月 == 前年開始年月日4).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前年開始年月日4).Sum(c => c.運送収入),
                                            月5 = V01Group.Where(c => c.集計年月 == 前年開始年月日5).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前年開始年月日5).Sum(c => c.運送収入),
                                            月6 = V01Group.Where(c => c.集計年月 == 前年開始年月日6).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前年開始年月日6).Sum(c => c.運送収入),
                                            月7 = V01Group.Where(c => c.集計年月 == 前年開始年月日7).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前年開始年月日7).Sum(c => c.運送収入),
                                            月8 = V01Group.Where(c => c.集計年月 == 前年開始年月日8).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前年開始年月日8).Sum(c => c.運送収入),
                                            月9 = V01Group.Where(c => c.集計年月 == 前年開始年月日9).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前年開始年月日9).Sum(c => c.運送収入),
                                            月10 = V01Group.Where(c => c.集計年月 == 前年開始年月日10).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前年開始年月日10).Sum(c => c.運送収入),
                                            月11 = V01Group.Where(c => c.集計年月 == 前年開始年月日11).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前年開始年月日11).Sum(c => c.運送収入),
                                            月12 = V01Group.Where(c => c.集計年月 == 前年開始年月日12).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前年開始年月日12).Sum(c => c.運送収入),
											月名1 = s月名1,
											月名2 = s月名2,
											月名3 = s月名3,
											月名4 = s月名4,
											月名5 = s月名5,
											月名6 = s月名6,
											月名7 = s月名7,
											月名8 = s月名8,
											月名9 = s月名9,
											月名10 = s月名10,
											月名11 = s月名11,
											月名12 = s月名12,
											表示区分 = i表示区分 == 0 ? "(売上あり乗務員のみ)" : "(売上無し乗務員含む)",
                                            表示順序 = i表示順序 == 0 ? "車輌ID" : i表示順序 == 1 ? "合計金額" : "",
                                            年間合計 = V01Group.Where(c => c.集計年月 >= 前年開始年月日1 && c.集計年月 <= 前年開始年月日12).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 >= 前年開始年月日1 && c.集計年月 <= 前年開始年月日12).Sum(c => c.運送収入),
                                            平均 = V01Group.Where(c => c.集計年月 >= 前年開始年月日1 && c.集計年月 <= 前年開始年月日12).Count(c => c.運送収入 != 0) == 0 ? 0 : V01Group.Where(c => c.集計年月 >= 前年開始年月日1 && c.集計年月 <= 前年開始年月日12).Sum(c => c.運送収入) / V01Group.Where(c => c.集計年月 >= 前年開始年月日1 && c.集計年月 <= 前年開始年月日12).Count(c => c.運送収入 != 0),
                                            構成比 = V01Group.Where(v01 => v01.集計年月 >= 前年開始年月日1 && v01.集計年月 <= 前年開始年月日12).Sum(c => c.運送収入) / context.V_車輌月別売上合計表.Where(c => c.集計年月 >= 前年開始年月日1 && c.集計年月 <= 前年開始年月日12).Sum(c => c.運送収入) * 100 == null ? 0 : V01Group.Where(v01 => v01.集計年月 >= 前年開始年月日1 && v01.集計年月 <= 前年開始年月日12).Sum(c => c.運送収入) / context.V_車輌月別売上合計表.Where(c => c.集計年月 >= 前年開始年月日1 && c.集計年月 <= 前年開始年月日12).Sum(c => c.運送収入) * 100,
                                            当年売上 = V01Group.Where(c => c.集計年月 >= 開始年月日1 && c.集計年月 <= 開始年月日12).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 >= 開始年月日1 && c.集計年月 <= 開始年月日12).Sum(c => c.運送収入),
                                            売上順位データ = V01Group.Where(v01 => v01.集計年月 >= 前々年開始年月日12 && v01.集計年月 <= 開始年月日12).Sum(v01 => v01.運送収入) == null ? 0 : V01Group.Where(v01 => v01.集計年月 >= 前々年開始年月日12 && v01.集計年月 <= 開始年月日12).Sum(v01 => v01.運送収入),
                                            車輌指定 = s車輌From + "～" + s車輌To,
                                            車輌ﾋﾟｯｸｱｯﾌﾟ = 車輌ﾋﾟｯｸｱｯﾌﾟ指定 == "" ? "無" : 車輌ﾋﾟｯｸｱｯﾌﾟ指定,
                                            総年合計 = 0,
                                        }).AsQueryable();

                #endregion


                #region 前々年データ
                    //＜＜＜前々年のデータ＞＞＞
                    query = query.Union(from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                        from m05 in context.M05_CAR.Where(c => c.車種ID == m06.車種ID && c.廃車区分 == 0 && c.削除日付 == null)
                                        join V01 in context.V_車輌月別売上合計表 on m05.車輌ID equals V01.車輌ID into V01Group
                                        select new NNG04010_Member
                                        {
                                            作成年度1 = s作成年月度1,
                                            作成年度2 = s作成年月度2,
                                            車輌コード = m05.車輌ID,
                                            車番 = m05.車輌番号,
                                            車種名 = m06.車種名,
                                            区分 = 前年前々年 == false ? "" : "前々年",
                                            月1 = V01Group.Where(c => c.集計年月 == 前々年開始年月日1).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前々年開始年月日1).Sum(c => c.運送収入),
                                            月2 = V01Group.Where(c => c.集計年月 == 前々年開始年月日2).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前々年開始年月日2).Sum(c => c.運送収入),
                                            月3 = V01Group.Where(c => c.集計年月 == 前々年開始年月日3).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前々年開始年月日3).Sum(c => c.運送収入),
                                            月4 = V01Group.Where(c => c.集計年月 == 前々年開始年月日4).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前々年開始年月日4).Sum(c => c.運送収入),
                                            月5 = V01Group.Where(c => c.集計年月 == 前々年開始年月日5).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前々年開始年月日5).Sum(c => c.運送収入),
                                            月6 = V01Group.Where(c => c.集計年月 == 前々年開始年月日6).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前々年開始年月日6).Sum(c => c.運送収入),
                                            月7 = V01Group.Where(c => c.集計年月 == 前々年開始年月日7).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前々年開始年月日7).Sum(c => c.運送収入),
                                            月8 = V01Group.Where(c => c.集計年月 == 前々年開始年月日8).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前々年開始年月日8).Sum(c => c.運送収入),
                                            月9 = V01Group.Where(c => c.集計年月 == 前々年開始年月日9).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前々年開始年月日9).Sum(c => c.運送収入),
                                            月10 = V01Group.Where(c => c.集計年月 == 前々年開始年月日10).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前々年開始年月日10).Sum(c => c.運送収入),
                                            月11 = V01Group.Where(c => c.集計年月 == 前々年開始年月日11).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前々年開始年月日11).Sum(c => c.運送収入),
                                            月12 = V01Group.Where(c => c.集計年月 == 前々年開始年月日12).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 == 前々年開始年月日12).Sum(c => c.運送収入),
											月名1 = s月名1,
											月名2 = s月名2,
											月名3 = s月名3,
											月名4 = s月名4,
											月名5 = s月名5,
											月名6 = s月名6,
											月名7 = s月名7,
											月名8 = s月名8,
											月名9 = s月名9,
											月名10 = s月名10,
											月名11 = s月名11,
											月名12 = s月名12,
											表示区分 = i表示区分 == 0 ? "(売上あり乗務員のみ)" : "(売上無し乗務員含む)",
                                            表示順序 = i表示順序 == 0 ? "車輌ID" : i表示順序 == 1 ? "合計金額" : "",
                                            年間合計 = V01Group.Where(c => c.集計年月 >= 前々年開始年月日1 && c.集計年月 <= 前々年開始年月日12).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 >= 前々年開始年月日1 && c.集計年月 <= 前々年開始年月日12).Sum(c => c.運送収入),
                                            平均 = V01Group.Where(c => c.集計年月 >= 前々年開始年月日1 && c.集計年月 <= 前々年開始年月日12).Count(c => c.運送収入 != 0) == 0 ? 0 : V01Group.Where(c => c.集計年月 >= 前々年開始年月日1 && c.集計年月 <= 前々年開始年月日12).Sum(c => c.運送収入) / V01Group.Where(c => c.集計年月 >= 前々年開始年月日1 && c.集計年月 <= 前々年開始年月日12).Count(c => c.運送収入 != 0),
                                            構成比 = V01Group.Where(v01 => v01.集計年月 >= 前々年開始年月日1 && v01.集計年月 <= 前々年開始年月日12).Sum(c => c.運送収入) / context.V_車輌月別売上合計表.Where(c => c.集計年月 >= 前々年開始年月日1 && c.集計年月 <= 前々年開始年月日12).Sum(c => c.運送収入) * 100 == null ? 0 : V01Group.Where(v01 => v01.集計年月 >= 前々年開始年月日1 && v01.集計年月 <= 前々年開始年月日12).Sum(c => c.運送収入) / context.V_車輌月別売上合計表.Where(c => c.集計年月 >= 前々年開始年月日1 && c.集計年月 <= 前々年開始年月日12).Sum(c => c.運送収入) * 100,
                                            当年売上 = V01Group.Where(c => c.集計年月 >= 開始年月日1 && c.集計年月 <= 開始年月日12).Sum(c => c.運送収入) == null ? 0 : V01Group.Where(c => c.集計年月 >= 開始年月日1 && c.集計年月 <= 開始年月日12).Sum(c => c.運送収入),
                                            売上順位データ = 前年前々年 == false ? V01Group.Where(v01 => v01.集計年月 >= 開始年月日1 && v01.集計年月 <= 開始年月日12).Sum(v01 => v01.運送収入) == null ? 0 : V01Group.Where(v01 => v01.集計年月 >= 前々年開始年月日12 && v01.集計年月 <= 前々年開始年月日12).Sum(v01 => v01.運送収入) : V01Group.Where(v01 => v01.集計年月 >= 前々年開始年月日12 && v01.集計年月 <= 開始年月日12).Sum(v01 => v01.運送収入) == null ? 0 : V01Group.Where(v01 => v01.集計年月 >= 前々年開始年月日12 && v01.集計年月 <= 開始年月日12).Sum(v01 => v01.運送収入),
                                            車輌指定 = s車輌From + "～" + s車輌To,
                                            車輌ﾋﾟｯｸｱｯﾌﾟ = 車輌ﾋﾟｯｸｱｯﾌﾟ指定 == "" ? "無" : 車輌ﾋﾟｯｸｱｯﾌﾟ指定,
                                            総年合計 = 0,
                                        }).AsQueryable();
                }

                #endregion


                #region データ条件項目



                //表示順序
                switch (i表示順序)
                {
                    case 0:
                        query = query.OrderBy(c => c.車輌コード).ThenByDescending(c => c.区分);
                        break;
                    case 1:
                        query = query.OrderByDescending(c => c.売上順位データ).ThenBy(c => c.車輌コード).ThenByDescending(c => c.区分);
                        break;
                }

                //表示区分
                if (i表示区分 == 0)
                {
                    query = query.Where(c => c.当年売上 > 0);
                }
                else
                {
                    query = query.Where(c => c.当年売上 >= 0);
                }



                var intCause = i車輌List;
                decimal? p合計 = 0;
                decimal? T当年合計 = 0;
                decimal? Z前年合計 = 0;
                decimal? Z前々年合計 = 0;
                //ピックアップ処理
                if (string.IsNullOrEmpty(s車輌From + s車輌To))
                {
                    if (i車輌List.Length > 0)
                    {
                        query = query.Where(c => intCause.Contains(c.車輌コード));
                    }

                    //前年全前年
                    if (前年前々年 == true)
                    {
                        //総年合計計算
                        retList = query.ToList();
                        for (int i = 0; i < retList.Count; i++)
                        {
                            if (retList[i].区分 == "当年")
                            {
                                p合計 = retList[i].年間合計;
                                T当年合計 = p合計 + T当年合計;
                            }

                            if (retList[i].区分 == "前年")
                            {
                                p合計 = retList[i].年間合計;
                                Z前年合計 = p合計 + Z前年合計;
                            }

                            if (retList[i].区分 == "前々年")
                            {
                                p合計 = retList[i].年間合計;
                                Z前々年合計 = p合計 + Z前々年合計;
                            }
                        }

                        //構成比計算
                        for (int j = 0; j < retList.Count; j++)
                        {
                            if (retList[j].区分 == "当年")
                            {
                                retList[j].総年合計 = T当年合計;
                            }

                            if (retList[j].区分 == "前年")
                            {
                                retList[j].総年合計 = Z前年合計;
                            }

                            if (retList[j].区分 == "前々年")
                            {
                                retList[j].総年合計 = Z前々年合計;
                            }

                            //0除算処理
                            //構成比計算
                            if (retList[j].総年合計 == 0)
                            {
                                retList[j].構成比 = (retList[j].年間合計 / 1) * 100;
                                Math.Round((decimal)retList[j].構成比, 0);
                            }
                            else
                            {
                                retList[j].構成比 = (retList[j].年間合計 / retList[j].総年合計) * 100;
                                Math.Round((decimal)retList[j].構成比, 0);
                            }
                        }
                    }
                    else
                    {
                        //当年処理
                        //総年合計計算
                        retList = query.ToList();
                        for (int i = 0; i < retList.Count; i++)
                        {
                            p合計 = retList[i].年間合計;
                            T当年合計 = p合計 + T当年合計;
                        }

                        //構成比計算
                        for (int j = 0; j < retList.Count; j++)
                        {
                            retList[j].総年合計 = T当年合計;
                            //0除算処理
                            //構成比計算
                            if (retList[j].総年合計 == 0)
                            {
                                retList[j].構成比 = (retList[j].年間合計 / 1) * 100;
                                Math.Round((decimal)retList[j].構成比, 0);
                            }
                            else
                            {
                                retList[j].構成比 = (retList[j].年間合計 / retList[j].総年合計) * 100;
                                Math.Round((decimal)retList[j].構成比, 0);
                            }
                        }
                    }
                }
                else
                {
                    //From、To、ピックアップ指定を入力された場合
                    int iFrom = 1;
                    int iTo = 999999999;
                    iFrom = AppCommon.IntParse(s車輌From);
                    iTo = AppCommon.IntParse(s車輌To);

                    if (i車輌List.Length > 0)
                    {
                        query = query.Where(c => intCause.Contains(c.車輌コード) || (c.車輌コード >= iFrom && c.車輌コード <= iTo));

                        //前年全前年
                        if (前年前々年 == true)
                        {
                            //総年合計計算
                            retList = query.ToList();
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].区分 == "当年")
                                {
                                    p合計 = retList[i].年間合計;
                                    T当年合計 = p合計 + T当年合計;
                                }

                                if (retList[i].区分 == "前年")
                                {
                                    p合計 = retList[i].年間合計;
                                    Z前年合計 = p合計 + Z前年合計;
                                }

                                if (retList[i].区分 == "前々年")
                                {
                                    p合計 = retList[i].年間合計;
                                    Z前々年合計 = p合計 + Z前々年合計;
                                }
                            }

                            //構成比計算
                            for (int j = 0; j < retList.Count; j++)
                            {
                                if (retList[j].区分 == "当年")
                                {
                                    retList[j].総年合計 = T当年合計;
                                }

                                if (retList[j].区分 == "前年")
                                {
                                    retList[j].総年合計 = Z前年合計;
                                }

                                if (retList[j].区分 == "前々年")
                                {
                                    retList[j].総年合計 = Z前々年合計;
                                }

                                //0除算処理
                                //構成比計算
                                if (retList[j].総年合計 == 0)
                                {
                                    retList[j].構成比 = (retList[j].年間合計 / 1) * 100;
                                    Math.Round((decimal)retList[j].構成比, 0);
                                }
                                else
                                {
                                    retList[j].構成比 = (retList[j].年間合計 / retList[j].総年合計) * 100;
                                    Math.Round((decimal)retList[j].構成比, 0);
                                }
                            }
                        }
                        else
                        {
                            //前年全前年
                            if (前年前々年 == true)
                            {
                                //総年合計計算
                                retList = query.ToList();
                                for (int i = 0; i < retList.Count; i++)
                                {
                                    if (retList[i].区分 == "当年")
                                    {
                                        p合計 = retList[i].年間合計;
                                        T当年合計 = p合計 + T当年合計;
                                    }

                                    if (retList[i].区分 == "前年")
                                    {
                                        p合計 = retList[i].年間合計;
                                        Z前年合計 = p合計 + Z前年合計;
                                    }

                                    if (retList[i].区分 == "前々年")
                                    {
                                        p合計 = retList[i].年間合計;
                                        Z前々年合計 = p合計 + Z前々年合計;
                                    }
                                }

                                //構成比計算
                                for (int j = 0; j < retList.Count; j++)
                                {
                                    if (retList[j].区分 == "当年")
                                    {
                                        retList[j].総年合計 = T当年合計;
                                    }

                                    if (retList[j].区分 == "前年")
                                    {
                                        retList[j].総年合計 = Z前年合計;
                                    }

                                    if (retList[j].区分 == "前々年")
                                    {
                                        retList[j].総年合計 = Z前々年合計;
                                    }

                                    //0除算処理
                                    //構成比計算
                                    if (retList[j].総年合計 == 0)
                                    {
                                        retList[j].構成比 = (retList[j].年間合計 / 1) * 100;
                                        Math.Round((decimal)retList[j].構成比, 0);
                                    }
                                    else
                                    {
                                        retList[j].構成比 = (retList[j].年間合計 / retList[j].総年合計) * 100;
                                        Math.Round((decimal)retList[j].構成比, 0);
                                    }
                                }
                            }
                            else
                            {
                                //当年処理
                                //総年合計計算
                                retList = query.ToList();
                                for (int i = 0; i < retList.Count; i++)
                                {
                                    p合計 = retList[i].年間合計;
                                    T当年合計 = p合計 + T当年合計;
                                }

                                //構成比計算
                                for (int j = 0; j < retList.Count; j++)
                                {

                                    retList[j].総年合計 = T当年合計;
                                    //0除算処理
                                    //構成比計算
                                    if (retList[j].総年合計 == 0)
                                    {
                                        retList[j].構成比 = (retList[j].年間合計 / 1) * 100;
                                        Math.Round((decimal)retList[j].構成比, 0);
                                    }
                                    else
                                    {
                                        retList[j].構成比 = (retList[j].年間合計 / retList[j].総年合計) * 100;
                                        Math.Round((decimal)retList[j].構成比, 0);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        query = query.Where(c => (c.車輌コード >= iFrom && c.車輌コード <= iTo));

                        //前年全前年
                        if (前年前々年 == true)
                        {
                            //総年合計計算
                            retList = query.ToList();
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].区分 == "当年")
                                {
                                    p合計 = retList[i].年間合計;
                                    T当年合計 = p合計 + T当年合計;
                                }

                                if (retList[i].区分 == "前年")
                                {
                                    p合計 = retList[i].年間合計;
                                    Z前年合計 = p合計 + Z前年合計;
                                }

                                if (retList[i].区分 == "前々年")
                                {
                                    p合計 = retList[i].年間合計;
                                    Z前々年合計 = p合計 + Z前々年合計;
                                }
                            }

                            //構成比計算
                            for (int j = 0; j < retList.Count; j++)
                            {
                                if (retList[j].区分 == "当年")
                                {
                                    retList[j].総年合計 = T当年合計;
                                }

                                if (retList[j].区分 == "前年")
                                {
                                    retList[j].総年合計 = Z前年合計;
                                }

                                if (retList[j].区分 == "前々年")
                                {
                                    retList[j].総年合計 = Z前々年合計;
                                }

                                //0除算処理
                                //構成比計算
                                if (retList[j].総年合計 == 0)
                                {
                                    retList[j].構成比 = (retList[j].年間合計 / 1) * 100;
                                    Math.Round((decimal)retList[j].構成比, 0);
                                }
                                else
                                {
                                    retList[j].構成比 = (retList[j].年間合計 / retList[j].総年合計) * 100;
                                    Math.Round((decimal)retList[j].構成比, 0);
                                }
                            }
                        }
                        else
                        {
                            //前年全前年
                            if (前年前々年 == true)
                            {
                                //総年合計計算
                                retList = query.ToList();
                                for (int i = 0; i < retList.Count; i++)
                                {
                                    if (retList[i].区分 == "当年")
                                    {
                                        p合計 = retList[i].年間合計;
                                        T当年合計 = p合計 + T当年合計;
                                    }

                                    if (retList[i].区分 == "前年")
                                    {
                                        p合計 = retList[i].年間合計;
                                        Z前年合計 = p合計 + Z前年合計;
                                    }

                                    if (retList[i].区分 == "前々年")
                                    {
                                        p合計 = retList[i].年間合計;
                                        Z前々年合計 = p合計 + Z前々年合計;
                                    }
                                }

                                //構成比計算
                                for (int j = 0; j < retList.Count; j++)
                                {
                                    if (retList[j].区分 == "当年")
                                    {
                                        retList[j].総年合計 = T当年合計;
                                    }

                                    if (retList[j].区分 == "前年")
                                    {
                                        retList[j].総年合計 = Z前年合計;
                                    }

                                    if (retList[j].区分 == "前々年")
                                    {
                                        retList[j].総年合計 = Z前々年合計;
                                    }

                                    //0除算処理
                                    //構成比計算
                                    if (retList[j].総年合計 == 0)
                                    {
                                        retList[j].構成比 = (retList[j].年間合計 / 1) * 100;
                                        Math.Round((decimal)retList[j].構成比, 0);
                                    }
                                    else
                                    {
                                        retList[j].構成比 = (retList[j].年間合計 / retList[j].総年合計) * 100;
                                        Math.Round((decimal)retList[j].構成比, 0);
                                    }
                                }
                            }
                            else
                            {
                                //当年処理
                                //総年合計計算
                                retList = query.ToList();
                                for (int i = 0; i < retList.Count; i++)
                                {
                                    p合計 = retList[i].年間合計;
                                    T当年合計 = p合計 + T当年合計;
                                }

                                //構成比計算
                                for (int j = 0; j < retList.Count; j++)
                                {

                                    retList[j].総年合計 = T当年合計;
                                    //0除算処理
                                    //構成比計算
                                    if (retList[j].総年合計 == 0)
                                    {
                                        retList[j].構成比 = (retList[j].年間合計 / 1) * 100;
                                        Math.Round((decimal)retList[j].構成比, 0);
                                    }
                                    else
                                    {
                                        retList[j].構成比 = (retList[j].年間合計 / retList[j].総年合計) * 100;
                                        Math.Round((decimal)retList[j].構成比, 0);
                                    }
                                }
                            }
                        }
                    }
                }


                return retList;

                #endregion


            }

        }
        #endregion

    }
}
