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
    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コード、svc、および config ファイルで同時にクラス名 "DataDriveLogService" を変更できます。
    // 注意: このサービスをテストするために WCF テスト クライアントを起動するには、ソリューション エクスプローラーで DataDriveLogService.svc または DataDriveLogService.svc.cs を選択し、デバッグを開始してください。
    public class NNG08010
    {
        /// <summary>
        /// NNG08010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class NNG08010_Member1
        {
            public int? 部門ID { get; set; }
            public int? 部門KEY { get; set; }
            public string 部門名 { get; set; }
            public int 集計年月 { get; set; }
            public decimal? 締日売上金額 { get; set; }
        }

        #region メンバー定義
        /// <summary>
        /// NNG08010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class NNG08010_Member
        {
            public int? コード { get; set; }
            public string 部門名 { get; set; }
            public string カナ読み { get; set; }
            public string s年度 { get; set; }
            public int i年度 { get; set; }
            public decimal? 月1 { get; set; }
            public decimal? 月2 { get; set; }
            public decimal? 月3 { get; set; }
            public decimal? 月4 { get; set; }
            public decimal? 月5 { get; set; }
            public decimal? 月6 { get; set; }
            public decimal? 月7 { get; set; }
            public decimal? 月8 { get; set; }
            public decimal? 月9 { get; set; }
            public decimal? 月10 { get; set; }
            public decimal? 月11 { get; set; }
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
			public decimal? 年合計 { get; set; }
            public decimal? 平均 { get; set; }
            public decimal? 構成比 { get; set; }
            public decimal? 総合計 { get; set; }
            public decimal? 当年除数 { get; set; }
            public decimal? 前年除数 { get; set; }
            public decimal? 前々年除数 { get; set; }
            public DateTime 開始年月 { get; set; }
            public DateTime 終了年月 { get; set; }
            public string 表示区分 { get; set; }
            public string 全締日 { get; set; }
            public string 表示順序 { get; set; }
            public string 指定コード { get; set; }
            public string コードFrom { get; set; }
            public string コードTo { get; set; }


        }

        /// <summary>
        /// NNG08010  CSV　メンバー
        /// </summary>
        [DataContract]
        public class NNG08010_Member_CSV
        {
            public int? コード { get; set; }
            public string 部門名 { get; set; }
            public string カナ読み { get; set; }
            public string s年度 { get; set; }
            public int i年度 { get; set; }
            public decimal? 月1 { get; set; }
            public decimal? 月2 { get; set; }
            public decimal? 月3 { get; set; }
            public decimal? 月4 { get; set; }
            public decimal? 月5 { get; set; }
            public decimal? 月6 { get; set; }
            public decimal? 月7 { get; set; }
            public decimal? 月8 { get; set; }
            public decimal? 月9 { get; set; }
            public decimal? 月10 { get; set; }
            public decimal? 月11 { get; set; }
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
			public decimal? 年合計 { get; set; }
            public decimal? 平均 { get; set; }
            //public decimal? 構成比 { get; set; }
            //public decimal? 総合計 { get; set; }
            public DateTime 開始年月 { get; set; }
            public DateTime 終了年月 { get; set; }
            public string 表示区分 { get; set; }
            public string 全締日 { get; set; }
            public string 表示順序 { get; set; }
            public string 指定コード { get; set; }
            public string コードFrom { get; set; }
            public string コードTo { get; set; }

        }

        #endregion

        #region 締日帳票印刷
        /// <summary>
        /// NNG08010 締日帳票印刷
        /// </summary>
        /// <param name="p商品ID">部門コード</param>
        /// <returns>S02</returns>
        public List<NNG08010_Member> SEARCH_NNG08010(string p部門From, string p部門To, int?[] i部門List, bool 前年前々年, int 表示区分_CValue, int p表示順序, DateTime[] d開始年月日, DateTime[] d終了年月日, DateTime[] d前年開始年月日, DateTime[] d前年終了年月日, DateTime[] d前々年開始年月日, DateTime[] d前々年終了年月日, string s表示区分, string s表示順序, string s部門List)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<NNG08010_Member> retList = new List<NNG08010_Member>();
                context.Connection.Open();

				string s月1 = d開始年月日[0].Month.ToString() + "月";
				string s月2 = d開始年月日[1].Month.ToString() + "月";
				string s月3 = d開始年月日[2].Month.ToString() + "月";
				string s月4 = d開始年月日[3].Month.ToString() + "月";
				string s月5 = d開始年月日[4].Month.ToString() + "月";
				string s月6 = d開始年月日[5].Month.ToString() + "月";
				string s月7 = d開始年月日[6].Month.ToString() + "月";
				string s月8 = d開始年月日[7].Month.ToString() + "月";
				string s月9 = d開始年月日[8].Month.ToString() + "月";
				string s月10 = d開始年月日[9].Month.ToString() + "月";
				string s月11 = d開始年月日[10].Month.ToString() + "月";
				string s月12 = d開始年月日[11].Month.ToString() + "月";

                DateTime  開始年月日1 = d開始年月日[0];
                DateTime  開始年月日2 = d開始年月日[1];
                DateTime  開始年月日3 = d開始年月日[2];
                DateTime  開始年月日4 = d開始年月日[3];
                DateTime  開始年月日5 = d開始年月日[4];
                DateTime  開始年月日6 = d開始年月日[5];
                DateTime  開始年月日7 = d開始年月日[6];
                DateTime  開始年月日8 = d開始年月日[7];
                DateTime  開始年月日9 = d開始年月日[8];
                DateTime  開始年月日10 = d開始年月日[9];
                DateTime  開始年月日11 = d開始年月日[10];
                DateTime  開始年月日12 = d開始年月日[11];

                DateTime  終了年月日1 = d終了年月日[0];
                DateTime  終了年月日2 = d終了年月日[1];
                DateTime  終了年月日3 = d終了年月日[2];
                DateTime  終了年月日4 = d終了年月日[3];
                DateTime  終了年月日5 = d終了年月日[4];
                DateTime  終了年月日6 = d終了年月日[5];
                DateTime  終了年月日7 = d終了年月日[6];
                DateTime  終了年月日8 = d終了年月日[7];
                DateTime  終了年月日9 = d終了年月日[8];
                DateTime  終了年月日10 = d終了年月日[9];
                DateTime  終了年月日11 = d終了年月日[10];
                DateTime  終了年月日12 = d終了年月日[11];

                //前年
                DateTime  前年開始年月日1 = d前年開始年月日[0];
                DateTime  前年開始年月日2 = d前年開始年月日[1];
                DateTime  前年開始年月日3 = d前年開始年月日[2];
                DateTime  前年開始年月日4 = d前年開始年月日[3];
                DateTime  前年開始年月日5 = d前年開始年月日[4];
                DateTime  前年開始年月日6 = d前年開始年月日[5];
                DateTime  前年開始年月日7 = d前年開始年月日[6];
                DateTime  前年開始年月日8 = d前年開始年月日[7];
                DateTime  前年開始年月日9 = d前年開始年月日[8];
                DateTime  前年開始年月日10 = d前年開始年月日[9];
                DateTime  前年開始年月日11 = d前年開始年月日[10];
                DateTime  前年開始年月日12 = d前年開始年月日[11];

                DateTime  前年終了年月日1 = d前年終了年月日[0];
                DateTime  前年終了年月日2 = d前年終了年月日[1];
                DateTime  前年終了年月日3 = d前年終了年月日[2];
                DateTime  前年終了年月日4 = d前年終了年月日[3];
                DateTime  前年終了年月日5 = d前年終了年月日[4];
                DateTime  前年終了年月日6 = d前年終了年月日[5];
                DateTime  前年終了年月日7 = d前年終了年月日[6];
                DateTime  前年終了年月日8 = d前年終了年月日[7];
                DateTime  前年終了年月日9 = d前年終了年月日[8];
                DateTime  前年終了年月日10 = d前年終了年月日[9];
                DateTime  前年終了年月日11 = d前年終了年月日[10];
                DateTime  前年終了年月日12 = d前年終了年月日[11];

                //前々年
                DateTime  前々年開始年月日1 = d前々年開始年月日[0];
                DateTime  前々年開始年月日2 = d前々年開始年月日[1];
                DateTime  前々年開始年月日3 = d前々年開始年月日[2];
                DateTime  前々年開始年月日4 = d前々年開始年月日[3];
                DateTime  前々年開始年月日5 = d前々年開始年月日[4];
                DateTime  前々年開始年月日6 = d前々年開始年月日[5];
                DateTime  前々年開始年月日7 = d前々年開始年月日[6];
                DateTime  前々年開始年月日8 = d前々年開始年月日[7];
                DateTime  前々年開始年月日9 = d前々年開始年月日[8];
                DateTime  前々年開始年月日10 = d前々年開始年月日[9];
                DateTime  前々年開始年月日11 = d前々年開始年月日[10];
                DateTime  前々年開始年月日12 = d前々年開始年月日[11];

                DateTime  前々年終了年月日1 = d前々年終了年月日[0];
                DateTime  前々年終了年月日2 = d前々年終了年月日[1];
                DateTime  前々年終了年月日3 = d前々年終了年月日[2];
                DateTime  前々年終了年月日4 = d前々年終了年月日[3];
                DateTime  前々年終了年月日5 = d前々年終了年月日[4];
                DateTime  前々年終了年月日6 = d前々年終了年月日[5];
                DateTime  前々年終了年月日7 = d前々年終了年月日[6];
                DateTime  前々年終了年月日8 = d前々年終了年月日[7];
                DateTime  前々年終了年月日9 = d前々年終了年月日[8];
                DateTime  前々年終了年月日10 = d前々年終了年月日[9];
                DateTime  前々年終了年月日11 = d前々年終了年月日[10];
                DateTime  前々年終了年月日12 = d前々年終了年月日[11];



                var query = (from m71 in context.M71_BUM
                             join t01 in context.T01_TRN.Where(t01 => (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1))) on m71.自社部門ID equals t01.自社部門ID into t01Group
                             select new NNG08010_Member
                             {
                                 コード = m71.自社部門ID,
                                 部門名 = m71.自社部門名,
                                 カナ読み = m71.かな読み,
                                 s年度 = "当年",
                                 i年度 = 1,
                                 月1 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日1 && t01.請求日付 <= 終了年月日1).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日1 && t01.請求日付 <= 終了年月日1).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月2 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日2 && t01.請求日付 <= 終了年月日2).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日2 && t01.請求日付 <= 終了年月日2).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月3 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日3 && t01.請求日付 <= 終了年月日3).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日3 && t01.請求日付 <= 終了年月日3).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月4 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日4 && t01.請求日付 <= 終了年月日4).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日4 && t01.請求日付 <= 終了年月日4).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月5 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日5 && t01.請求日付 <= 終了年月日5).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日5 && t01.請求日付 <= 終了年月日5).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月6 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日6 && t01.請求日付 <= 終了年月日6).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日6 && t01.請求日付 <= 終了年月日6).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月7 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日7 && t01.請求日付 <= 終了年月日7).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日7 && t01.請求日付 <= 終了年月日7).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月8 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日8 && t01.請求日付 <= 終了年月日8).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日8 && t01.請求日付 <= 終了年月日8).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月9 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日9 && t01.請求日付 <= 終了年月日9).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日9 && t01.請求日付 <= 終了年月日9).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月10 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日10 && t01.請求日付 <= 終了年月日10).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日10 && t01.請求日付 <= 終了年月日10).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月11 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日11 && t01.請求日付 <= 終了年月日11).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日11 && t01.請求日付 <= 終了年月日11).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月12 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日12 && t01.請求日付 <= 終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日12 && t01.請求日付 <= 終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
								 月名1 = s月1,
								 月名2 = s月2,
								 月名3 = s月3,
								 月名4 = s月4,
								 月名5 = s月5,
								 月名6 = s月6,
								 月名7 = s月7,
								 月名8 = s月8,
								 月名9 = s月9,
								 月名10 = s月10,
								 月名11 = s月11,
								 月名12 = s月12,
                                 年合計 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日1 && t01.請求日付 <= 終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日1 && t01.請求日付 <= 終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 平均 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日1 && t01.請求日付 <= 終了年月日12).Count(t01 => t01.売上金額 != 0 && t01.請求割増１ != 0 && t01.請求割増２ != 0 && t01.通行料 != 0) == 0 ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日1 && t01.請求日付 <= 終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) / t01Group.Where(t01 => t01.請求日付 >= 開始年月日1 && t01.請求日付 <= 終了年月日12).Count(t01 => t01.売上金額 != 0 && t01.請求割増１ != 0 && t01.請求割増２ != 0 && t01.通行料 != 0),
                                 構成比 = Math.Round((decimal)(context.T01_TRN.Where(t01 => (t01.請求日付 >= 開始年月日1 && t01.請求日付 <= 終了年月日12) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1))).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) / (t01Group.Where(t01 => (t01.請求日付 >= 開始年月日1 && t01.請求日付 <= 終了年月日12) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1))).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料))), 2),
                                 総合計 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 終了年月日1).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 終了年月日1).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 開始年月 = 終了年月日1,
                                 終了年月 = 終了年月日12,
                                 表示区分 = s表示区分,
                                 表示順序 = s表示順序,
                                 指定コード = s部門List,
                                 コードFrom = p部門From,
                                 コードTo = p部門To,

                             }).AsQueryable();


                //前年処理
                if (前年前々年 == true)
                {

                    query = query.Union(from m71 in context.M71_BUM
                                        join t01 in context.T01_TRN.Where(t01 => t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1)) on m71.自社部門ID equals t01.自社部門ID into t01Group
                                        select new NNG08010_Member
                                       {
                                           コード = m71.自社部門ID,
                                           部門名 = m71.自社部門名,
                                           カナ読み = m71.かな読み,
                                           s年度 = "前年",
                                           i年度 = 2,
                                           月1 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日1 && t01.請求日付 <= 前年終了年月日1).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日1 && t01.請求日付 <= 前年終了年月日1).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                           月2 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日2 && t01.請求日付 <= 前年終了年月日2).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日2 && t01.請求日付 <= 前年終了年月日2).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                           月3 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日3 && t01.請求日付 <= 前年終了年月日3).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日3 && t01.請求日付 <= 前年終了年月日3).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                           月4 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日4 && t01.請求日付 <= 前年終了年月日4).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日4 && t01.請求日付 <= 前年終了年月日4).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                           月5 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日5 && t01.請求日付 <= 前年終了年月日5).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日5 && t01.請求日付 <= 前年終了年月日5).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                           月6 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日6 && t01.請求日付 <= 前年終了年月日6).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日6 && t01.請求日付 <= 前年終了年月日6).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                           月7 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日7 && t01.請求日付 <= 前年終了年月日7).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日7 && t01.請求日付 <= 前年終了年月日7).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                           月8 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日8 && t01.請求日付 <= 前年終了年月日8).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日8 && t01.請求日付 <= 前年終了年月日8).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                           月9 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日9 && t01.請求日付 <= 前年終了年月日9).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日9 && t01.請求日付 <= 前年終了年月日9).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                           月10 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日10 && t01.請求日付 <= 前年終了年月日10).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日10 && t01.請求日付 <= 前年終了年月日10).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                           月11 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日11 && t01.請求日付 <= 前年終了年月日11).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日11 && t01.請求日付 <= 前年終了年月日11).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                           月12 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日12 && t01.請求日付 <= 前年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日12 && t01.請求日付 <= 前年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
										   月名1 = s月1,
										   月名2 = s月2,
										   月名3 = s月3,
										   月名4 = s月4,
										   月名5 = s月5,
										   月名6 = s月6,
										   月名7 = s月7,
										   月名8 = s月8,
										   月名9 = s月9,
										   月名10 = s月10,
										   月名11 = s月11,
										   月名12 = s月12,
										   年合計 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日1 && t01.請求日付 <= 前年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日1 && t01.請求日付 <= 前年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                           平均 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日1 && t01.請求日付 <= 前年終了年月日12).Count(t01 => t01.売上金額 != 0 && t01.請求割増１ != 0 && t01.請求割増２ != 0 && t01.通行料 != 0) == 0 ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日1 && t01.請求日付 <= 前年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) / t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日1 && t01.請求日付 <= 前年終了年月日12).Count(t01 => t01.売上金額 != 0 && t01.請求割増１ != 0 && t01.請求割増２ != 0 && t01.通行料 != 0),
                                           
                                           構成比 = Math.Round((decimal)(context.T01_TRN.Where(t01 => (t01.請求日付 >= 前年開始年月日1 && t01.請求日付 <= 前年終了年月日12) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1))).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == 0 ? 0 : (t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日1 && t01.請求日付 <= 前年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料))), 2) == 0 ? 0 :
                                           Math.Round((decimal)(context.T01_TRN.Where(t01 => (t01.請求日付 >= 前年開始年月日1 && t01.請求日付 <= 前年終了年月日12) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1))).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) / (t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日1 && t01.請求日付 <= 前年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料))), 2),
                                          
                                           総合計 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 終了年月日1).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 終了年月日1).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                           開始年月 = 終了年月日1,
                                           終了年月 = 終了年月日12,
                                           表示区分 = s表示区分,
                                           表示順序 = s表示順序,
                                           指定コード = s部門List,
                                           コードFrom = p部門From,
                                           コードTo = p部門To,

                                       }).AsQueryable();


                    query = query.Union(from m71 in context.M71_BUM
                                        join t01 in context.T01_TRN.Where(t01 => t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1)) on m71.自社部門ID equals t01.自社部門ID into t01Group
                                        select new NNG08010_Member
                                        {
                                            コード = m71.自社部門ID,
                                            部門名 = m71.自社部門名,
                                            カナ読み = m71.かな読み,
                                            s年度 = "前々年",
                                            i年度 = 3,
                                            月1 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 前々年終了年月日1).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 前々年終了年月日1).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月2 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日2 && t01.請求日付 <= 前々年終了年月日2).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日2 && t01.請求日付 <= 前々年終了年月日2).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月3 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日3 && t01.請求日付 <= 前々年終了年月日3).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日3 && t01.請求日付 <= 前々年終了年月日3).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月4 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日4 && t01.請求日付 <= 前々年終了年月日4).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日4 && t01.請求日付 <= 前々年終了年月日4).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月5 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日5 && t01.請求日付 <= 前々年終了年月日5).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日5 && t01.請求日付 <= 前々年終了年月日5).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月6 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日6 && t01.請求日付 <= 前々年終了年月日6).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日6 && t01.請求日付 <= 前々年終了年月日6).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月7 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日7 && t01.請求日付 <= 前々年終了年月日7).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日7 && t01.請求日付 <= 前々年終了年月日7).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月8 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日8 && t01.請求日付 <= 前々年終了年月日8).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日8 && t01.請求日付 <= 前々年終了年月日8).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月9 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日9 && t01.請求日付 <= 前々年終了年月日9).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日9 && t01.請求日付 <= 前々年終了年月日9).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月10 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日10 && t01.請求日付 <= 前々年終了年月日10).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日10 && t01.請求日付 <= 前々年終了年月日10).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月11 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日11 && t01.請求日付 <= 前々年終了年月日11).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日11 && t01.請求日付 <= 前々年終了年月日11).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月12 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日12 && t01.請求日付 <= 前々年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日12 && t01.請求日付 <= 前々年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
											月名1 = s月1,
											月名2 = s月2,
											月名3 = s月3,
											月名4 = s月4,
											月名5 = s月5,
											月名6 = s月6,
											月名7 = s月7,
											月名8 = s月8,
											月名9 = s月9,
											月名10 = s月10,
											月名11 = s月11,
											月名12 = s月12,
											年合計 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 前々年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 前々年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            平均 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 前々年終了年月日12).Count(t01 => t01.売上金額 != 0 && t01.請求割増１ != 0 && t01.請求割増２ != 0 && t01.通行料 != 0) == 0 ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 前々年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) / t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 前々年終了年月日12).Count(t01 => t01.売上金額 != 0 && t01.請求割増１ != 0 && t01.請求割増２ != 0 && t01.通行料 != 0),
                                            構成比 = Math.Round((decimal)(context.T01_TRN.Where(t01 => (t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 前々年終了年月日12) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1))).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == 0 ? 0 : (t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 前々年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料))), 2) == 0 ? 0 :
                                            Math.Round((decimal)(context.T01_TRN.Where(t01 => (t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 前々年終了年月日12) && (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1))).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) / (t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 前々年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料))), 2),
                                            総合計 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 終了年月日1).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 終了年月日1).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            開始年月 = 終了年月日1,
                                            終了年月 = 終了年月日12,
                                            表示区分 = s表示区分,
                                            表示順序 = s表示順序,
                                            指定コード = s部門List,
                                            コードFrom = p部門From,
                                            コードTo = p部門To,

                                        }).AsQueryable();

                    int i部門FROM;
                    int i部門TO;
                    //部門From処理　Min値
                    if (!string.IsNullOrEmpty(p部門From))
                    {
                        i部門FROM = AppCommon.IntParse(p部門From);
                    }
                    else
                    {
                        i部門FROM = int.MinValue;
                    }

                    //部門To処理　Max値
                    if (!string.IsNullOrEmpty(p部門To))
                    {
                        i部門TO = AppCommon.IntParse(p部門To);
                    }
                    else
                    {
                        i部門TO = int.MaxValue;
                    }

                    var intCause = i部門List;
                    if (string.IsNullOrEmpty(p部門From + p部門To))
                    {
                        if (i部門List.Length > 0)
                        {
                            query = query.Where(q => intCause.Contains(q.コード));
                        }
                    }
                    else
                    {
                        if (i部門List.Length > 0)
                        {
                            query = query.Where(q => intCause.Contains(q.コード) || (q.コード >= i部門FROM && q.コード <= i部門TO));
                        }
                        else
                        {
                            query = query.Where(q => (q.コード >= i部門FROM && q.コード <= i部門TO));
                        }
                    }

                    //表示処理
                    //売上あり：0
                    //売上なし：1
                    switch (表示区分_CValue)
                    {
                        //売上あり
                        case 0:
                            query = query.Where(c => c.総合計 != 0);
                            break;

                        default:
                            break;
                    }

                    //表示順序処理
                    switch (p表示順序)
                    {
                        //コード
                        case 0:
                            //query = query.OrderBy(c => new { c.コード, c.i年度 });
                            query = query.OrderBy(c =>  c.コード).ThenBy(c => c.i年度);
                            break;

                        //カナ読み
                        case 1:
                            query = query.OrderBy(c => c.カナ読み).ThenBy(c => c.コード).ThenBy(c => c.i年度);
                            break;

                        //売上
                        case 2:
                            query = query.OrderByDescending(c => c.総合計 ).ThenBy(c => c.コード).ThenBy(c => c.i年度 );
                            break;

                        default:
                            break;
                    }

                }
                else
                {

                    int i部門FROM;
                    int i部門TO;
                    //部門From処理　Min値
                    if (!string.IsNullOrEmpty(p部門From))
                    {
                        i部門FROM = AppCommon.IntParse(p部門From);
                    }
                    else
                    {
                        i部門FROM = int.MinValue;
                    }

                    //部門To処理　Max値
                    if (!string.IsNullOrEmpty(p部門To))
                    {
                        i部門TO = AppCommon.IntParse(p部門To);
                    }
                    else
                    {
                        i部門TO = int.MaxValue;
                    }

                    var intCause = i部門List;
                    if (string.IsNullOrEmpty(p部門From + p部門To))
                    {
                        if (i部門List.Length > 0)
                        {
                            query = query.Where(q => intCause.Contains(q.コード));
                        }
                    }
                    else
                    {
                        if (i部門List.Length > 0)
                        {
                            query = query.Where(q => intCause.Contains(q.コード) || (q.コード >= i部門FROM && q.コード <= i部門TO));
                        }
                        else
                        {
                            query = query.Where(q => (q.コード >= i部門FROM && q.コード <= i部門TO));
                        }
                    }

                    query = query.Distinct();

                    //表示処理
                    //売上あり：0
                    //売上なし：1
                    switch (表示区分_CValue)
                    {
                        //売上あり
                        case 0:
                            query = query.Where(c => c.年合計 != 0);
                            break;

                        default:
                            break;
                    }

                    //表示順序処理
                    switch (p表示順序)
                    {
                        //コード
                        case 0:
                            //query = query.OrderBy(c => new { c.コード, c.i年度 });
                            query = query.OrderBy(c => c.コード).ThenBy(c => c.i年度);
                            break;

                        //カナ読み
                        case 1:
                            query = query.OrderBy(c => c.カナ読み).ThenBy(c => c.コード).ThenBy(c => c.i年度);
                            break;

                        //売上
                        case 2:
                            query = query.OrderByDescending(c => c.年合計).ThenBy(c => c.コード).ThenBy(c => c.i年度);
                            break;

                        default:
                            break;
                    }

                }

                //結果をリスト化
                retList = query.ToList();
                int cnt;
                
                for (int i = 0; i < retList.Count; i++)
                {
                    cnt = 0;
                    if (retList[i].月1 != 0 && retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月2 != 0 && retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月3 != 0 && retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月4 != 0 && retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月5 != 0 && retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月6 != 0 && retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月7 != 0 && retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月8 != 0 && retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月9 != 0 && retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月10 != 0 && retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月11 != 0 && retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月12 != 0 && retList[i].月1 != null)
                    {
                        cnt += 1;
                    }

                    if (cnt == 0)
                    {
                        retList[i].平均 = 0;
                    }
                    else
                    {
                        retList[i].平均 = Math.Round((decimal)(retList[i].年合計 / cnt), 0);
                    }
                }


                return retList;

            }

        }

        #endregion

        #region 締日CSV出力
        /// <summary>
        /// NNG08010 締日CSV出力
        /// </summary>
        /// <param name="p商品ID">部門コード</param>
        /// <returns>S12</returns>
        public List<NNG08010_Member_CSV> SEARCH_NNG08010_CSV(string p部門From, string p部門To, int?[] i部門List, bool 前年前々年, int 表示区分_CValue, int p表示順序, DateTime[] d開始年月日, DateTime[] d終了年月日, DateTime[] d前年開始年月日, DateTime[] d前年終了年月日, DateTime[] d前々年開始年月日, DateTime[] d前々年終了年月日, string s表示区分, string s表示順序, string s部門List)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<NNG08010_Member_CSV> retList = new List<NNG08010_Member_CSV>();
                context.Connection.Open();

				string s月1 = d開始年月日[0].Month.ToString() + "月";
				string s月2 = d開始年月日[1].Month.ToString() + "月";
				string s月3 = d開始年月日[2].Month.ToString() + "月";
				string s月4 = d開始年月日[3].Month.ToString() + "月";
				string s月5 = d開始年月日[4].Month.ToString() + "月";
				string s月6 = d開始年月日[5].Month.ToString() + "月";
				string s月7 = d開始年月日[6].Month.ToString() + "月";
				string s月8 = d開始年月日[7].Month.ToString() + "月";
				string s月9 = d開始年月日[8].Month.ToString() + "月";
				string s月10 = d開始年月日[9].Month.ToString() + "月";
				string s月11 = d開始年月日[10].Month.ToString() + "月";
				string s月12 = d開始年月日[11].Month.ToString() + "月";

                DateTime 開始年月日1 = d開始年月日[0];
                DateTime 開始年月日2 = d開始年月日[1];
                DateTime 開始年月日3 = d開始年月日[2];
                DateTime 開始年月日4 = d開始年月日[3];
                DateTime 開始年月日5 = d開始年月日[4];
                DateTime 開始年月日6 = d開始年月日[5];
                DateTime 開始年月日7 = d開始年月日[6];
                DateTime 開始年月日8 = d開始年月日[7];
                DateTime 開始年月日9 = d開始年月日[8];
                DateTime 開始年月日10 = d開始年月日[9];
                DateTime 開始年月日11 = d開始年月日[10];
                DateTime 開始年月日12 = d開始年月日[11];

                DateTime 終了年月日1 = d終了年月日[0];
                DateTime 終了年月日2 = d終了年月日[1];
                DateTime 終了年月日3 = d終了年月日[2];
                DateTime 終了年月日4 = d終了年月日[3];
                DateTime 終了年月日5 = d終了年月日[4];
                DateTime 終了年月日6 = d終了年月日[5];
                DateTime 終了年月日7 = d終了年月日[6];
                DateTime 終了年月日8 = d終了年月日[7];
                DateTime 終了年月日9 = d終了年月日[8];
                DateTime 終了年月日10 = d終了年月日[9];
                DateTime 終了年月日11 = d終了年月日[10];
                DateTime 終了年月日12 = d終了年月日[11];

                //前年
                DateTime 前年開始年月日1 = d前年開始年月日[0];
                DateTime 前年開始年月日2 = d前年開始年月日[1];
                DateTime 前年開始年月日3 = d前年開始年月日[2];
                DateTime 前年開始年月日4 = d前年開始年月日[3];
                DateTime 前年開始年月日5 = d前年開始年月日[4];
                DateTime 前年開始年月日6 = d前年開始年月日[5];
                DateTime 前年開始年月日7 = d前年開始年月日[6];
                DateTime 前年開始年月日8 = d前年開始年月日[7];
                DateTime 前年開始年月日9 = d前年開始年月日[8];
                DateTime 前年開始年月日10 = d前年開始年月日[9];
                DateTime 前年開始年月日11 = d前年開始年月日[10];
                DateTime 前年開始年月日12 = d前年開始年月日[11];

                DateTime 前年終了年月日1 = d前年終了年月日[0];
                DateTime 前年終了年月日2 = d前年終了年月日[1];
                DateTime 前年終了年月日3 = d前年終了年月日[2];
                DateTime 前年終了年月日4 = d前年終了年月日[3];
                DateTime 前年終了年月日5 = d前年終了年月日[4];
                DateTime 前年終了年月日6 = d前年終了年月日[5];
                DateTime 前年終了年月日7 = d前年終了年月日[6];
                DateTime 前年終了年月日8 = d前年終了年月日[7];
                DateTime 前年終了年月日9 = d前年終了年月日[8];
                DateTime 前年終了年月日10 = d前年終了年月日[9];
                DateTime 前年終了年月日11 = d前年終了年月日[10];
                DateTime 前年終了年月日12 = d前年終了年月日[11];

                //前々年
                DateTime 前々年開始年月日1 = d前々年開始年月日[0];
                DateTime 前々年開始年月日2 = d前々年開始年月日[1];
                DateTime 前々年開始年月日3 = d前々年開始年月日[2];
                DateTime 前々年開始年月日4 = d前々年開始年月日[3];
                DateTime 前々年開始年月日5 = d前々年開始年月日[4];
                DateTime 前々年開始年月日6 = d前々年開始年月日[5];
                DateTime 前々年開始年月日7 = d前々年開始年月日[6];
                DateTime 前々年開始年月日8 = d前々年開始年月日[7];
                DateTime 前々年開始年月日9 = d前々年開始年月日[8];
                DateTime 前々年開始年月日10 = d前々年開始年月日[9];
                DateTime 前々年開始年月日11 = d前々年開始年月日[10];
                DateTime 前々年開始年月日12 = d前々年開始年月日[11];

                DateTime 前々年終了年月日1 = d前々年終了年月日[0];
                DateTime 前々年終了年月日2 = d前々年終了年月日[1];
                DateTime 前々年終了年月日3 = d前々年終了年月日[2];
                DateTime 前々年終了年月日4 = d前々年終了年月日[3];
                DateTime 前々年終了年月日5 = d前々年終了年月日[4];
                DateTime 前々年終了年月日6 = d前々年終了年月日[5];
                DateTime 前々年終了年月日7 = d前々年終了年月日[6];
                DateTime 前々年終了年月日8 = d前々年終了年月日[7];
                DateTime 前々年終了年月日9 = d前々年終了年月日[8];
                DateTime 前々年終了年月日10 = d前々年終了年月日[9];
                DateTime 前々年終了年月日11 = d前々年終了年月日[10];
                DateTime 前々年終了年月日12 = d前々年終了年月日[11];



                var query = (from m71 in context.M71_BUM
                             join t01 in context.T01_TRN.Where(t01 => (t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1))) on m71.自社部門ID equals t01.自社部門ID into t01Group
                             select new NNG08010_Member_CSV
                             {
                                 コード = m71.自社部門ID,
                                 部門名 = m71.自社部門名,
                                 カナ読み = m71.かな読み,
                                 s年度 = "当年",
                                 i年度 = 1,
                                 月1 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日1 && t01.請求日付 <= 終了年月日1).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日1 && t01.請求日付 <= 終了年月日1).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月2 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日2 && t01.請求日付 <= 終了年月日2).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日2 && t01.請求日付 <= 終了年月日2).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月3 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日3 && t01.請求日付 <= 終了年月日3).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日3 && t01.請求日付 <= 終了年月日3).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月4 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日4 && t01.請求日付 <= 終了年月日4).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日4 && t01.請求日付 <= 終了年月日4).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月5 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日5 && t01.請求日付 <= 終了年月日5).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日5 && t01.請求日付 <= 終了年月日5).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月6 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日6 && t01.請求日付 <= 終了年月日6).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日6 && t01.請求日付 <= 終了年月日6).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月7 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日7 && t01.請求日付 <= 終了年月日7).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日7 && t01.請求日付 <= 終了年月日7).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月8 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日8 && t01.請求日付 <= 終了年月日8).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日8 && t01.請求日付 <= 終了年月日8).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月9 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日9 && t01.請求日付 <= 終了年月日9).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日9 && t01.請求日付 <= 終了年月日9).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月10 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日10 && t01.請求日付 <= 終了年月日10).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日10 && t01.請求日付 <= 終了年月日10).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月11 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日11 && t01.請求日付 <= 終了年月日11).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日11 && t01.請求日付 <= 終了年月日11).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 月12 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日12 && t01.請求日付 <= 終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日12 && t01.請求日付 <= 終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
								 月名1 = s月1,
								 月名2 = s月2,
								 月名3 = s月3,
								 月名4 = s月4,
								 月名5 = s月5,
								 月名6 = s月6,
								 月名7 = s月7,
								 月名8 = s月8,
								 月名9 = s月9,
								 月名10 = s月10,
								 月名11 = s月11,
								 月名12 = s月12,
								 年合計 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日1 && t01.請求日付 <= 終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日1 && t01.請求日付 <= 終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                 平均 = t01Group.Where(t01 => t01.請求日付 >= 開始年月日1 && t01.請求日付 <= 終了年月日12).Count(t01 => t01.売上金額 != 0 && t01.請求割増１ != 0 && t01.請求割増２ != 0 && t01.通行料 != 0) == 0 ? 0 : t01Group.Where(t01 => t01.請求日付 >= 開始年月日1 && t01.請求日付 <= 終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) / t01Group.Where(t01 => t01.請求日付 >= 開始年月日1 && t01.請求日付 <= 終了年月日12).Count(t01 => t01.売上金額 != 0 && t01.請求割増１ != 0 && t01.請求割増２ != 0 && t01.通行料 != 0),
                                 開始年月 = 終了年月日1,
                                 終了年月 = 終了年月日12,
                                 表示区分 = s表示区分,
                                 表示順序 = s表示順序,
                                 指定コード = s部門List,
                                 コードFrom = p部門From,
                                 コードTo = p部門To,

                             }).AsQueryable();


                //前年処理
                if (前年前々年 == true)
                {

                    query = query.Union(from m71 in context.M71_BUM
                                        join t01 in context.T01_TRN.Where(t01 => t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1)) on m71.自社部門ID equals t01.自社部門ID into t01Group
                                        select new NNG08010_Member_CSV
                                        {
                                            コード = m71.自社部門ID,
                                            部門名 = m71.自社部門名,
                                            カナ読み = m71.かな読み,
                                            s年度 = "前年",
                                            i年度 = 2,
                                            月1 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日1 && t01.請求日付 <= 前年終了年月日1).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日1 && t01.請求日付 <= 前年終了年月日1).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月2 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日2 && t01.請求日付 <= 前年終了年月日2).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日2 && t01.請求日付 <= 前年終了年月日2).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月3 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日3 && t01.請求日付 <= 前年終了年月日3).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日3 && t01.請求日付 <= 前年終了年月日3).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月4 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日4 && t01.請求日付 <= 前年終了年月日4).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日4 && t01.請求日付 <= 前年終了年月日4).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月5 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日5 && t01.請求日付 <= 前年終了年月日5).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日5 && t01.請求日付 <= 前年終了年月日5).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月6 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日6 && t01.請求日付 <= 前年終了年月日6).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日6 && t01.請求日付 <= 前年終了年月日6).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月7 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日7 && t01.請求日付 <= 前年終了年月日7).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日7 && t01.請求日付 <= 前年終了年月日7).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月8 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日8 && t01.請求日付 <= 前年終了年月日8).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日8 && t01.請求日付 <= 前年終了年月日8).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月9 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日9 && t01.請求日付 <= 前年終了年月日9).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日9 && t01.請求日付 <= 前年終了年月日9).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月10 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日10 && t01.請求日付 <= 前年終了年月日10).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日10 && t01.請求日付 <= 前年終了年月日10).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月11 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日11 && t01.請求日付 <= 前年終了年月日11).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日11 && t01.請求日付 <= 前年終了年月日11).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月12 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日12 && t01.請求日付 <= 前年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日12 && t01.請求日付 <= 前年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
											月名1 = s月1,
											月名2 = s月2,
											月名3 = s月3,
											月名4 = s月4,
											月名5 = s月5,
											月名6 = s月6,
											月名7 = s月7,
											月名8 = s月8,
											月名9 = s月9,
											月名10 = s月10,
											月名11 = s月11,
											月名12 = s月12,
											年合計 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日1 && t01.請求日付 <= 前年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日1 && t01.請求日付 <= 前年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            平均 = t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日1 && t01.請求日付 <= 前年終了年月日12).Count(t01 => t01.売上金額 != 0 && t01.請求割増１ != 0 && t01.請求割増２ != 0 && t01.通行料 != 0) == 0 ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日1 && t01.請求日付 <= 前年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) / t01Group.Where(t01 => t01.請求日付 >= 前年開始年月日1 && t01.請求日付 <= 前年終了年月日12).Count(t01 => t01.売上金額 != 0 && t01.請求割増１ != 0 && t01.請求割増２ != 0 && t01.通行料 != 0),
                                            開始年月 = 終了年月日1,
                                            終了年月 = 終了年月日12,
                                            表示区分 = s表示区分,
                                            表示順序 = s表示順序,
                                            指定コード = s部門List,
                                            コードFrom = p部門From,
                                            コードTo = p部門To,

                                        }).AsQueryable();


                    query = query.Union(from m71 in context.M71_BUM
                                        join t01 in context.T01_TRN.Where(t01 => t01.入力区分 != 3 || (t01.入力区分 == 3 && t01.明細行 == 1)) on m71.自社部門ID equals t01.自社部門ID into t01Group
                                        select new NNG08010_Member_CSV
                                        {
                                            コード = m71.自社部門ID,
                                            部門名 = m71.自社部門名,
                                            カナ読み = m71.かな読み,
                                            s年度 = "前々年",
                                            i年度 = 3,
                                            月1 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 前々年終了年月日1).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 前々年終了年月日1).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月2 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日2 && t01.請求日付 <= 前々年終了年月日2).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日2 && t01.請求日付 <= 前々年終了年月日2).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月3 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日3 && t01.請求日付 <= 前々年終了年月日3).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日3 && t01.請求日付 <= 前々年終了年月日3).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月4 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日4 && t01.請求日付 <= 前々年終了年月日4).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日4 && t01.請求日付 <= 前々年終了年月日4).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月5 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日5 && t01.請求日付 <= 前々年終了年月日5).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日5 && t01.請求日付 <= 前々年終了年月日5).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月6 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日6 && t01.請求日付 <= 前々年終了年月日6).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日6 && t01.請求日付 <= 前々年終了年月日6).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月7 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日7 && t01.請求日付 <= 前々年終了年月日7).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日7 && t01.請求日付 <= 前々年終了年月日7).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月8 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日8 && t01.請求日付 <= 前々年終了年月日8).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日8 && t01.請求日付 <= 前々年終了年月日8).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月9 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日9 && t01.請求日付 <= 前々年終了年月日9).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日9 && t01.請求日付 <= 前々年終了年月日9).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月10 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日10 && t01.請求日付 <= 前々年終了年月日10).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日10 && t01.請求日付 <= 前々年終了年月日10).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月11 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日11 && t01.請求日付 <= 前々年終了年月日11).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日11 && t01.請求日付 <= 前々年終了年月日11).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            月12 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日12 && t01.請求日付 <= 前々年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日12 && t01.請求日付 <= 前々年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
											月名1 = s月1,
											月名2 = s月2,
											月名3 = s月3,
											月名4 = s月4,
											月名5 = s月5,
											月名6 = s月6,
											月名7 = s月7,
											月名8 = s月8,
											月名9 = s月9,
											月名10 = s月10,
											月名11 = s月11,
											月名12 = s月12,
											年合計 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 前々年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) == null ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 前々年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料),
                                            平均 = t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 前々年終了年月日12).Count(t01 => t01.売上金額 != 0 && t01.請求割増１ != 0 && t01.請求割増２ != 0 && t01.通行料 != 0) == 0 ? 0 : t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 前々年終了年月日12).Sum(t01 => t01.売上金額 + t01.請求割増１ + t01.請求割増２ + t01.通行料) / t01Group.Where(t01 => t01.請求日付 >= 前々年開始年月日1 && t01.請求日付 <= 前々年終了年月日12).Count(t01 => t01.売上金額 != 0 && t01.請求割増１ != 0 && t01.請求割増２ != 0 && t01.通行料 != 0),
                                            開始年月 = 終了年月日1,
                                            終了年月 = 終了年月日12,
                                            表示区分 = s表示区分,
                                            表示順序 = s表示順序,
                                            指定コード = s部門List,
                                            コードFrom = p部門From,
                                            コードTo = p部門To,

                                        }).AsQueryable();

                    int i部門FROM;
                    int i部門TO;
                    //部門From処理　Min値
                    if (!string.IsNullOrEmpty(p部門From))
                    {
                        i部門FROM = AppCommon.IntParse(p部門From);
                    }
                    else
                    {
                        i部門FROM = int.MinValue;
                    }

                    //部門To処理　Max値
                    if (!string.IsNullOrEmpty(p部門To))
                    {
                        i部門TO = AppCommon.IntParse(p部門To);
                    }
                    else
                    {
                        i部門TO = int.MaxValue;
                    }

                    var intCause = i部門List;
                    if (string.IsNullOrEmpty(p部門From + p部門To))
                    {
                        if (i部門List.Length > 0)
                        {
                            query = query.Where(q => intCause.Contains(q.コード));
                        }
                    }
                    else
                    {
                        if (i部門List.Length > 0)
                        {
                            query = query.Where(q => intCause.Contains(q.コード) || (q.コード >= i部門FROM && q.コード <= i部門TO));
                        }
                        else
                        {
                            query = query.Where(q => (q.コード >= i部門FROM && q.コード <= i部門TO));
                        }
                    }

                    //表示処理
                    //売上あり：0
                    //売上なし：1
                    switch (表示区分_CValue)
                    {
                        //売上あり
                        case 0:
                            query = query.Where(c => c.年合計 != 0);
                            break;

                        default:
                            break;
                    }

                    //表示順序処理
                    switch (p表示順序)
                    {
                        //コード
                        case 0:
                            //query = query.OrderBy(c => new { c.コード, c.i年度 });
                            query = query.OrderBy(c => c.コード).ThenBy(c => c.i年度);
                            break;

                        //カナ読み
                        case 1:
                            query = query.OrderBy(c => c.カナ読み).ThenBy(c => c.コード).ThenBy(c => c.i年度);
                            break;

                        //売上
                        case 2:
                            query = query.OrderByDescending(c => c.年合計).ThenBy(c => c.コード).ThenBy(c => c.i年度);
                            break;

                        default:
                            break;
                    }

                }
                else
                {

                    int i部門FROM;
                    int i部門TO;
                    //部門From処理　Min値
                    if (!string.IsNullOrEmpty(p部門From))
                    {
                        i部門FROM = AppCommon.IntParse(p部門From);
                    }
                    else
                    {
                        i部門FROM = int.MinValue;
                    }

                    //部門To処理　Max値
                    if (!string.IsNullOrEmpty(p部門To))
                    {
                        i部門TO = AppCommon.IntParse(p部門To);
                    }
                    else
                    {
                        i部門TO = int.MaxValue;
                    }

                    var intCause = i部門List;
                    if (string.IsNullOrEmpty(p部門From + p部門To))
                    {
                        if (i部門List.Length > 0)
                        {
                            query = query.Where(q => intCause.Contains(q.コード));
                        }
                    }
                    else
                    {
                        if (i部門List.Length > 0)
                        {
                            query = query.Where(q => intCause.Contains(q.コード) || (q.コード >= i部門FROM && q.コード <= i部門TO));
                        }
                        else
                        {
                            query = query.Where(q => (q.コード >= i部門FROM && q.コード <= i部門TO));
                        }
                    }

                    query = query.Distinct();

                    //表示処理
                    //売上あり：0
                    //売上なし：1
                    switch (表示区分_CValue)
                    {
                        //売上あり
                        case 0:
                            query = query.Where(c => c.年合計 != 0);
                            break;

                        default:
                            break;
                    }

                    //表示順序処理
                    switch (p表示順序)
                    {
                        //コード
                        case 0:
                            //query = query.OrderBy(c => new { c.コード, c.i年度 });
                            query = query.OrderBy(c => c.コード).ThenBy(c => c.i年度);
                            break;

                        //カナ読み
                        case 1:
                            query = query.OrderBy(c => c.カナ読み).ThenBy(c => c.コード).ThenBy(c => c.i年度);
                            break;

                        //売上
                        case 2:
                            query = query.OrderByDescending(c => c.年合計).ThenBy(c => c.コード).ThenBy(c => c.i年度);
                            break;

                        default:
                            break;
                    }

                }

                //結果をリスト化
                retList = query.ToList();
                int cnt;

                for (int i = 0; i < retList.Count; i++)
                {
                    cnt = 0;
                    if (retList[i].月1 != 0 || retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月2 != 0 || retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月3 != 0 || retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月4 != 0 || retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月5 != 0 || retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月6 != 0 || retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月7 != 0 || retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月8 != 0 || retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月9 != 0 || retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月10 != 0 || retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月11 != 0 || retList[i].月1 != null)
                    {
                        cnt += 1;
                    }
                    if (retList[i].月12 != 0 || retList[i].月1 != null)
                    {
                        cnt += 1;
                    }

                    if (cnt == 0)
                    {
                        retList[i].平均 = 0;
                    }
                    else
                    {
                        retList[i].平均 = Math.Round((decimal)(retList[i].年合計 / cnt), 0);
                    }
                }
                return retList;

            }

        }
        #endregion

    }
}
