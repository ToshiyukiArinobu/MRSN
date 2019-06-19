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
    public class NNG02010
    {

        #region メンバー定義

        /// <summary>
        /// NNG02010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class NNG02010_Member
        {
            public int? 支払先コード { get; set; }
            public string 支払先名 { get; set; }
            public int 支払区分 { get; set; }
            public string 区分 { get; set; }
            public decimal? 売上金額 { get; set; }
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
            public decimal? 構成比1 { get; set; }
            public decimal? 売上順位データ { get; set; }
            public decimal? 総年合計 { get; set; }
            public DateTime 対象年月1 { get; set; }
            public DateTime 対象年月2 { get; set; }
            public int? 集計年月 { get; set; }
            public string 表示区分 { get; set; }
            public string 親子区分ID { get; set; }
            public int? 締日 { get; set; }
            public string 全締日 { get; set; }
            public string かな読み { get; set; }
            public string 表示順序 { get; set; }
            public string 支払先指定コード { get; set; }

        }

        /// <summary>
        /// NNG02010  CSV　メンバー
        /// </summary>
        [DataContract]
        public class NNG02010_Member_CSV
        {
            public int? 支払先コード { get; set; }
            public string 支払先名 { get; set; }
            public int 支払区分 { get; set; }
            public string 区分 { get; set; }
            public decimal? 売上金額 { get; set; }
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
            public decimal? 総年合計 { get; set; }
            public decimal? 構成比1 { get; set; }
            public decimal? 売上順位データ { get; set; }
            public DateTime 対象年月1 { get; set; }
            public DateTime 対象年月2 { get; set; }
            public int? 集計年月 { get; set; }
            public string 表示区分 { get; set; }
            public string 親子区分ID { get; set; }
            public int? 締日 { get; set; }
            public string 全締日 { get; set; }
            public string かな読み { get; set; }
            public string 表示順序 { get; set; }
        }

        #endregion

        #region 帳票印刷
        /// <summary>
        /// NNG02010 帳票印刷
        /// </summary>
        /// <param name="p商品ID">支払先コード</param>
        /// <returns>S02</returns>
        public List<NNG02010_Member> SEARCH_NNG02010_GetDataList(string p支払先From, string p支払先To, int?[] i支払先List, string p作成年月, string p作成締日, string p作成年, string p作成月, int 支払区分, bool b全締日集計, bool 前年前々年, int 表示区分_CValue, int p表示順序)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<NNG02010_Member> retList = new List<NNG02010_Member>();
                context.Connection.Open();

                //ピックアップ変数
                string 支払先指定表示 = string.Empty;
                //当月開始日付
                //DateTime d当月 = DateTime.Parse(p作成年 + "/" + p作成月 + "/" + "01").AddMonths(0);
                DateTime d当月;
                DateTime.TryParse(p作成年 + "/" + p作成月 + "/" + "01", out d当月);

				string s月1 = d当月.AddMonths(0).Month.ToString() + "月";
				string s月2 = d当月.AddMonths(1).Month.ToString() + "月";
				string s月3 = d当月.AddMonths(2).Month.ToString() + "月";
				string s月4 = d当月.AddMonths(3).Month.ToString() + "月";
				string s月5 = d当月.AddMonths(4).Month.ToString() + "月";
				string s月6 = d当月.AddMonths(5).Month.ToString() + "月";
				string s月7 = d当月.AddMonths(6).Month.ToString() + "月";
				string s月8 = d当月.AddMonths(7).Month.ToString() + "月";
				string s月9 = d当月.AddMonths(8).Month.ToString() + "月";
				string s月10 = d当月.AddMonths(9).Month.ToString() + "月";
				string s月11 = d当月.AddMonths(10).Month.ToString() + "月";
				string s月12 = d当月.AddMonths(11).Month.ToString() + "月";

                //範囲データを設定
                int 月1 = d当月.AddMonths(0).Year * 100 + d当月.AddMonths(0).Month;
                int 月2 = d当月.AddMonths(1).Year * 100 + d当月.AddMonths(1).Month;
                int 月3 = d当月.AddMonths(2).Year * 100 + d当月.AddMonths(2).Month;
                int 月4 = d当月.AddMonths(3).Year * 100 + d当月.AddMonths(3).Month;
                int 月5 = d当月.AddMonths(4).Year * 100 + d当月.AddMonths(4).Month;
                int 月6 = d当月.AddMonths(5).Year * 100 + d当月.AddMonths(5).Month;
                int 月7 = d当月.AddMonths(6).Year * 100 + d当月.AddMonths(6).Month;
                int 月8 = d当月.AddMonths(7).Year * 100 + d当月.AddMonths(7).Month;
                int 月9 = d当月.AddMonths(8).Year * 100 + d当月.AddMonths(8).Month;
                int 月10 = d当月.AddMonths(9).Year * 100 + d当月.AddMonths(9).Month;
                int 月11 = d当月.AddMonths(10).Year * 100 + d当月.AddMonths(10).Month;
                int 月12 = d当月.AddMonths(11).Year * 100 + d当月.AddMonths(11).Month;
                //前年開始日付
                int 前年月1 = d当月.AddMonths(-12).Year * 100 + d当月.AddMonths(-12).Month;
                int 前年月2 = d当月.AddMonths(-11).Year * 100 + d当月.AddMonths(-11).Month;
                int 前年月3 = d当月.AddMonths(-10).Year * 100 + d当月.AddMonths(-10).Month;
                int 前年月4 = d当月.AddMonths(-9).Year * 100 + d当月.AddMonths(-9).Month;
                int 前年月5 = d当月.AddMonths(-8).Year * 100 + d当月.AddMonths(-8).Month;
                int 前年月6 = d当月.AddMonths(-7).Year * 100 + d当月.AddMonths(-7).Month;
                int 前年月7 = d当月.AddMonths(-6).Year * 100 + d当月.AddMonths(-6).Month;
                int 前年月8 = d当月.AddMonths(-5).Year * 100 + d当月.AddMonths(-5).Month;
                int 前年月9 = d当月.AddMonths(-4).Year * 100 + d当月.AddMonths(-4).Month;
                int 前年月10 = d当月.AddMonths(-3).Year * 100 + d当月.AddMonths(-3).Month;
                int 前年月11 = d当月.AddMonths(-2).Year * 100 + d当月.AddMonths(-2).Month;
                int 前年月12 = d当月.AddMonths(-1).Year * 100 + d当月.AddMonths(-1).Month;
                //前々年開始日付
                int 前々年月1 = d当月.AddMonths(-24).Year * 100 + d当月.AddMonths(-24).Month;
                int 前々年月2 = d当月.AddMonths(-23).Year * 100 + d当月.AddMonths(-23).Month;
                int 前々年月3 = d当月.AddMonths(-22).Year * 100 + d当月.AddMonths(-22).Month;
                int 前々年月4 = d当月.AddMonths(-21).Year * 100 + d当月.AddMonths(-21).Month;
                int 前々年月5 = d当月.AddMonths(-20).Year * 100 + d当月.AddMonths(-20).Month;
                int 前々年月6 = d当月.AddMonths(-19).Year * 100 + d当月.AddMonths(-19).Month;
                int 前々年月7 = d当月.AddMonths(-18).Year * 100 + d当月.AddMonths(-18).Month;
                int 前々年月8 = d当月.AddMonths(-17).Year * 100 + d当月.AddMonths(-17).Month;
                int 前々年月9 = d当月.AddMonths(-16).Year * 100 + d当月.AddMonths(-16).Month;
                int 前々年月10 = d当月.AddMonths(-15).Year * 100 + d当月.AddMonths(-15).Month;
                int 前々年月11 = d当月.AddMonths(-14).Year * 100 + d当月.AddMonths(-14).Month;
                int 前々年月12 = d当月.AddMonths(-14).Year * 100 + d当月.AddMonths(-13).Month;

                //当月終了日付 ***LINQ内表示のみに使用***
                DateTime d当年_F = d当月.AddMonths(11);
                //*** 当年処理 ***
                var query = (from m01 in context.M01_TOK
                             join v01 in context.V_支払締日 on m01.得意先KEY equals v01.支払先KEY into v01Group
                             where m01.削除日付 == null
                             select new NNG02010_Member
                             {
                                 支払先コード = m01.得意先ID,
                                 支払先名 = m01.得意先名１,
                                 支払区分 = m01.取引区分,
                                 区分 = 前年前々年 == false ? "" : "当年",
                                 売上金額 = v01Group.Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Sum(v01 => v01.締日売上金額),
                                 月1 = v01Group.Where(v01 => v01.集計年月 == 月1).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月1).Sum(v01 => v01.締日売上金額),
                                 月2 = v01Group.Where(v01 => v01.集計年月 == 月2).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月2).Sum(v01 => v01.締日売上金額),
                                 月3 = v01Group.Where(v01 => v01.集計年月 == 月3).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月3).Sum(v01 => v01.締日売上金額),
                                 月4 = v01Group.Where(v01 => v01.集計年月 == 月4).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月4).Sum(v01 => v01.締日売上金額),
                                 月5 = v01Group.Where(v01 => v01.集計年月 == 月5).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月5).Sum(v01 => v01.締日売上金額),
                                 月6 = v01Group.Where(v01 => v01.集計年月 == 月6).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月6).Sum(v01 => v01.締日売上金額),
                                 月7 = v01Group.Where(v01 => v01.集計年月 == 月7).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月7).Sum(v01 => v01.締日売上金額),
                                 月8 = v01Group.Where(v01 => v01.集計年月 == 月8).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月8).Sum(v01 => v01.締日売上金額),
                                 月9 = v01Group.Where(v01 => v01.集計年月 == 月9).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月9).Sum(v01 => v01.締日売上金額),
                                 月10 = v01Group.Where(v01 => v01.集計年月 == 月10).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月10).Sum(v01 => v01.締日売上金額),
                                 月11 = v01Group.Where(v01 => v01.集計年月 == 月11).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月11).Sum(v01 => v01.締日売上金額),
                                 月12 = v01Group.Where(v01 => v01.集計年月 == 月12).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月12).Sum(v01 => v01.締日売上金額),
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
								 年合計 = v01Group.Where(v01 => v01.集計年月 >= 月1 && v01.集計年月 <= 月12).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 >= 月1 && v01.集計年月 <= 月12).Sum(v01 => v01.締日売上金額),
                                 平均 = v01Group.Where(c => c.集計年月 >= 月1 && c.集計年月 <= 月12).Count(c => c.締日売上金額 != 0) == 0 ? 0 : v01Group.Where(c => c.集計年月 >= 月1 && c.集計年月 <= 月12).Sum(c => c.締日売上金額) / v01Group.Where(c => c.集計年月 >= 月1 && c.集計年月 <= 月12).Count(c => c.締日売上金額 != 0),
                                 売上順位データ = 前年前々年 == false ? v01Group.Where(v01 => v01.集計年月 >= 月1 && v01.集計年月 <= 月12).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 >= 前々年月12 && v01.集計年月 <= 月12).Sum(v01 => v01.締日売上金額) : v01Group.Where(v01 => v01.集計年月 >= 前々年月12 && v01.集計年月 <= 月12).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 >= 前々年月12 && v01.集計年月 <= 月12).Sum(v01 => v01.締日売上金額),
                                 構成比1 = 0,
                                 総年合計 = 0,
                                 対象年月1 = d当月,
                                 対象年月2 = d当年_F,
                                 全締日 = p作成締日 == "" ? "全締日集計" : p作成締日,
                                 親子区分ID = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                 締日 = m01.Ｔ締日,
                                 かな読み = m01.かな読み,
                                 表示区分 = 表示区分_CValue == 0 ? "（売上あり支払先のみ）" : "（売上無し支払先含む）",
                                 表示順序 = p表示順序 == 0 ? "支払先ID" : p表示順序 == 1 ? "かな読み" : p表示順序 == 2 ? "売上金額" : "支払先ID",
                                 支払先指定コード = 支払先指定表示 == "" ? "" : 支払先指定表示,
                             }).AsQueryable();

                //*** 前年処理 ***
                if (前年前々年 == true)
                {
                    query = query.Union(from m01 in context.M01_TOK
                                        join v01 in context.V_支払締日 on m01.得意先KEY equals v01.支払先KEY into v01Group
                                        where m01.削除日付 == null
                                        select new NNG02010_Member
                                        {
                                            支払先コード = m01.得意先ID,
                                            支払先名 = m01.得意先名１,
                                            支払区分 = m01.取引区分,
                                            区分 = "前年",
                                            売上金額 = v01Group.Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Sum(v01 => v01.締日売上金額),
                                            月1 = v01Group.Where(v01 => v01.集計年月 == 前年月1).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月1).Sum(v01 => v01.締日売上金額),
                                            月2 = v01Group.Where(v01 => v01.集計年月 == 前年月2).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月2).Sum(v01 => v01.締日売上金額),
                                            月3 = v01Group.Where(v01 => v01.集計年月 == 前年月3).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月3).Sum(v01 => v01.締日売上金額),
                                            月4 = v01Group.Where(v01 => v01.集計年月 == 前年月4).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月4).Sum(v01 => v01.締日売上金額),
                                            月5 = v01Group.Where(v01 => v01.集計年月 == 前年月5).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月5).Sum(v01 => v01.締日売上金額),
                                            月6 = v01Group.Where(v01 => v01.集計年月 == 前年月6).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月6).Sum(v01 => v01.締日売上金額),
                                            月7 = v01Group.Where(v01 => v01.集計年月 == 前年月7).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月7).Sum(v01 => v01.締日売上金額),
                                            月8 = v01Group.Where(v01 => v01.集計年月 == 前年月8).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月8).Sum(v01 => v01.締日売上金額),
                                            月9 = v01Group.Where(v01 => v01.集計年月 == 前年月9).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月9).Sum(v01 => v01.締日売上金額),
                                            月10 = v01Group.Where(v01 => v01.集計年月 == 前年月10).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月10).Sum(v01 => v01.締日売上金額),
                                            月11 = v01Group.Where(v01 => v01.集計年月 == 前年月11).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月11).Sum(v01 => v01.締日売上金額),
                                            月12 = v01Group.Where(v01 => v01.集計年月 == 前年月12).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月12).Sum(v01 => v01.締日売上金額),
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
											年合計 = v01Group.Where(v01 => v01.集計年月 >= 前年月1 && v01.集計年月 <= 前年月12).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 >= 前年月1 && v01.集計年月 <= 前年月12).Sum(v01 => v01.締日売上金額),
                                            平均 = v01Group.Where(c => c.集計年月 >= 前年月1 && c.集計年月 <= 前年月12).Count(c => c.締日売上金額 != 0) == 0 ? 0 : v01Group.Where(c => c.集計年月 >= 前年月1 && c.集計年月 <= 前年月12).Sum(c => c.締日売上金額) / v01Group.Where(c => c.集計年月 >= 前年月1 && c.集計年月 <= 前年月12).Count(c => c.締日売上金額 != 0),
                                            売上順位データ = v01Group.Where(v01 => v01.集計年月 >= 前々年月12 && v01.集計年月 <= 月12).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 >= 前々年月12 && v01.集計年月 <= 月12).Sum(v01 => v01.締日売上金額),
                                            構成比1 = 0,
                                            総年合計 = 0,
                                            対象年月1 = d当月,
                                            対象年月2 = d当年_F,
                                            全締日 = p作成締日 == "" ? "全締日集計" : p作成締日,
                                            親子区分ID = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                            締日 = m01.Ｔ締日,
                                            かな読み = m01.かな読み,
                                            表示区分 = 表示区分_CValue == 0 ? "（売上あり支払先のみ）" : "（売上無し支払先含む）",
                                            表示順序 = p表示順序 == 0 ? "支払先ID" : p表示順序 == 1 ? "かな読み" : p表示順序 == 2 ? "売上金額" : "支払先ID",
                                            支払先指定コード = 支払先指定表示 == "" ? "" : 支払先指定表示,
                                        }).AsQueryable();

                    //*** 前々年処理 ***
                    query = query.Union(from m01 in context.M01_TOK
                                        join v01 in context.V_支払締日 on m01.得意先KEY equals v01.支払先KEY into v01Group
                                        where m01.削除日付 == null
                                        select new NNG02010_Member
                                        {
                                            支払先コード = m01.得意先ID,
                                            支払先名 = m01.得意先名１,
                                            支払区分 = m01.取引区分,
                                            区分 = "前々年",
                                            売上金額 = v01Group.Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Sum(v01 => v01.締日売上金額),
                                            月1 = v01Group.Where(v01 => v01.集計年月 == 前々年月1).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月1).Sum(v01 => v01.締日売上金額),
                                            月2 = v01Group.Where(v01 => v01.集計年月 == 前々年月2).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月2).Sum(v01 => v01.締日売上金額),
                                            月3 = v01Group.Where(v01 => v01.集計年月 == 前々年月3).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月3).Sum(v01 => v01.締日売上金額),
                                            月4 = v01Group.Where(v01 => v01.集計年月 == 前々年月4).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月4).Sum(v01 => v01.締日売上金額),
                                            月5 = v01Group.Where(v01 => v01.集計年月 == 前々年月5).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月5).Sum(v01 => v01.締日売上金額),
                                            月6 = v01Group.Where(v01 => v01.集計年月 == 前々年月6).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月6).Sum(v01 => v01.締日売上金額),
                                            月7 = v01Group.Where(v01 => v01.集計年月 == 前々年月7).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月7).Sum(v01 => v01.締日売上金額),
                                            月8 = v01Group.Where(v01 => v01.集計年月 == 前々年月8).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月8).Sum(v01 => v01.締日売上金額),
                                            月9 = v01Group.Where(v01 => v01.集計年月 == 前々年月9).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月9).Sum(v01 => v01.締日売上金額),
                                            月10 = v01Group.Where(v01 => v01.集計年月 == 前々年月10).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月10).Sum(v01 => v01.締日売上金額),
                                            月11 = v01Group.Where(v01 => v01.集計年月 == 前々年月11).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月11).Sum(v01 => v01.締日売上金額),
                                            月12 = v01Group.Where(v01 => v01.集計年月 == 前々年月12).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月12).Sum(v01 => v01.締日売上金額),
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
											年合計 = v01Group.Where(v01 => v01.集計年月 >= 前々年月1 && v01.集計年月 <= 前々年月12).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 >= 前々年月1 && v01.集計年月 <= 前々年月12).Sum(v01 => v01.締日売上金額),
                                            平均 = v01Group.Where(c => c.集計年月 >= 前々年月1 && c.集計年月 <= 前々年月12).Count(c => c.締日売上金額 != 0) == 0 ? 0 : v01Group.Where(c => c.集計年月 >= 前々年月1 && c.集計年月 <= 前々年月12).Sum(c => c.締日売上金額) / v01Group.Where(c => c.集計年月 >= 前々年月1 && c.集計年月 <= 前々年月12).Count(c => c.締日売上金額 != 0),
                                            売上順位データ = v01Group.Where(v01 => v01.集計年月 >= 前々年月12 && v01.集計年月 <= 月12).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 >= 前々年月12 && v01.集計年月 <= 月12).Sum(v01 => v01.締日売上金額),
                                            構成比1 = 0,
                                            総年合計 = 0,
                                            対象年月1 = d当月,
                                            対象年月2 = d当年_F,
                                            全締日 = p作成締日 == "" ? "全締日集計" : p作成締日,
                                            親子区分ID = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                            締日 = m01.Ｔ締日,
                                            かな読み = m01.かな読み,
                                            表示区分 = 表示区分_CValue == 0 ? "（売上あり支払先のみ）" : "（売上無し支払先含む）",
                                            表示順序 = p表示順序 == 0 ? "支払先ID" : p表示順序 == 1 ? "かな読み" : p表示順序 == 2 ? "売上金額" : "支払先ID",
                                            支払先指定コード = 支払先指定表示 == "" ? "" : 支払先指定表示,
                                        }).AsQueryable();
                }
                //重複処理
                query = query.Distinct();

                //全締日集計処理
                if (b全締日集計 == true)
                {
                    query = query.Where(c => c.締日 >= 1 && c.締日 <= 31);
                }

                //締日処理　
                if (!string.IsNullOrEmpty(p作成締日))
                {
                    int? p変換作成締日 = AppCommon.IntParse(p作成締日);
                    query = query.Where(c => c.締日 == p変換作成締日);
                }

                //支払区分処理
                switch (支払区分)
                {
                    //支払取引全体
                    case 0:
                        query = query.Where(c => c.支払区分 == 0);
                        break;

                    //支払先
                    case 1:
                        query = query.Where(c => c.支払区分 == 2);
                        break;

                    //経費先
                    case 2:
                        query = query.Where(c => c.支払区分 == 3);
                        break;

                    default:
                        break;
                }

                //表示順序処理
                if (前年前々年 == true)
                {
                    switch (表示区分_CValue)
                    {
                        //０を含まない
                        case 0:
                            query = query.Where(c => c.売上順位データ > 0);
                            break;

                        //０を含む
                        case 1:
                            query = query.Where(c => c.売上順位データ >= 0);
                            break;

                        default:
                            break;
                    }

                    //表示順序処理
                    switch (p表示順序)
                    {
                        case 0:
                            query = query.OrderBy(c => c.支払先コード).ThenByDescending(c => c.区分);
                            break;

                        case 1:
                            query = query.OrderBy(c => c.かな読み).ThenBy(c => c.支払先コード).ThenByDescending(c => c.区分);
                            break;

                        case 2:
                            query = query.OrderByDescending(c => c.売上順位データ).ThenBy(c => c.支払先コード).ThenByDescending(c => c.区分);
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    switch (表示区分_CValue)
                    {
                        case 0:
                            query = query.Where(c => c.年合計 > 0);
                            break;

                        case 1:
                            query = query.Where(c => c.年合計 >= 0);
                            break;

                        default:
                            break;
                    }

                    //表示順序処理
                    switch (p表示順序)
                    {
                        case 0:
                            query = query.OrderBy(c => c.支払先コード);
                            break;

                        case 1:
                            query = query.OrderBy(c => c.かな読み);
                            break;

                        case 2:
                            query = query.OrderByDescending(c => c.年合計);
                            break;

                        default:
                            break;
                    }
                }

                //支払先指定の表示
                if (i支払先List.Length > 0)
                {
                    for (int i = 0; i < query.Count(); i++)
                    {
                        支払先指定表示 = 支払先指定表示 + i支払先List[i].ToString();

                        if (i < i支払先List.Length)
                        {
                            if (i == i支払先List.Length - 1)
                            {
                                break;
                            }
                            支払先指定表示 = 支払先指定表示 + ",";
                        }
                        if (i支払先List.Length == 1)
                        {
                            break;
                        }
                    }
                }

                var intCause = i支払先List;
                decimal? p合計 = 0;
                decimal? T当年合計 = 0;
                decimal? Z前年合計 = 0;
                decimal? Z前々年合計 = 0;
                //ピックアップ処理
                if (string.IsNullOrEmpty(p支払先From + p支払先To))
                {
                    if (i支払先List.Length > 0)
                    {
                        query = query.Where(c => intCause.Contains(c.支払先コード));
                    }

                    //前年全前年
                    if (前年前々年 == true)
                    {
                        //総年合計計算
                        retList = query.ToList();
                        for (int i = 0; i < retList.Count; i++)
                        {
                            if (retList[i].親子区分ID != "子")
                            {

                                if (retList[i].区分 == "当年")
                                {
                                    p合計 = retList[i].年合計;
                                    T当年合計 = p合計 + T当年合計;
                                }

                                if (retList[i].区分 == "前年")
                                {
                                    p合計 = retList[i].年合計;
                                    Z前年合計 = p合計 + Z前年合計;
                                }

                                if (retList[i].区分 == "前々年")
                                {
                                    p合計 = retList[i].年合計;
                                    Z前々年合計 = p合計 + Z前々年合計;
                                }
                            }
                            else
                            {
                                continue;
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
                                retList[j].構成比1 = (retList[j].年合計 / 1) * 100;
                                Math.Round((decimal)retList[j].構成比1, 0);
                            }
                            else
                            {
                                retList[j].構成比1 = (retList[j].年合計 / retList[j].総年合計) * 100;
                                Math.Round((decimal)retList[j].構成比1, 0);
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
                            if (retList[i].親子区分ID != "子")
                            {
                                p合計 = retList[i].年合計;
                                T当年合計 = p合計 + T当年合計;
                            }
                            else
                            {
                                continue;
                            }
                        }

                        //構成比計算
                        for (int j = 0; j < retList.Count; j++)
                        {
                            retList[j].総年合計 = T当年合計;
                            //0除算処理
                            //構成比計算
                            if (retList[j].総年合計 == 0)
                            {
                                retList[j].構成比1 = (retList[j].年合計 / 1) * 100;
                                Math.Round((decimal)retList[j].構成比1, 0);
                            }
                            else
                            {
                                retList[j].構成比1 = (retList[j].年合計 / retList[j].総年合計) * 100;
                                Math.Round((decimal)retList[j].構成比1, 0);
                            }
                        }
                    }
                }
                else
                {
                    //From、To、ピックアップ指定を入力された場合
                    int iFrom = 0;
                    int iTo = 0;
                    
                    if (i支払先List.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(p支払先From) && !string.IsNullOrEmpty(p支払先To))
                        {
                            iFrom = AppCommon.IntParse(p支払先From);
                            iTo = AppCommon.IntParse(p支払先To);
                            query = query.Where(c => intCause.Contains(c.支払先コード) || (c.支払先コード >= iFrom && c.支払先コード <= iTo));
                        }
                        else if (!string.IsNullOrEmpty(p支払先From))
                        {
                            iFrom = AppCommon.IntParse(p支払先From);
                            query = query.Where(c => intCause.Contains(c.支払先コード) || (c.支払先コード >= iFrom));
                        }
                        else if (!string.IsNullOrEmpty(p支払先To))
                        {
                            iTo = AppCommon.IntParse(p支払先To);
                            query = query.Where(c => intCause.Contains(c.支払先コード) || (c.支払先コード <= iTo));
                        }

                        //前年全前年
                        if (前年前々年 == true)
                        {
                            //総年合計計算
                            retList = query.ToList();
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].親子区分ID != "子")
                                {
                                    if (retList[i].区分 == "当年")
                                    {
                                        p合計 = retList[i].年合計;
                                        T当年合計 = p合計 + T当年合計;
                                    }

                                    if (retList[i].区分 == "前年")
                                    {
                                        p合計 = retList[i].年合計;
                                        Z前年合計 = p合計 + Z前年合計;
                                    }

                                    if (retList[i].区分 == "前々年")
                                    {
                                        p合計 = retList[i].年合計;
                                        Z前々年合計 = p合計 + Z前々年合計;
                                    }
                                }
                                else
                                {
                                    continue;
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
                                    retList[j].構成比1 = (retList[j].年合計 / 1) * 100;
                                    Math.Round((decimal)retList[j].構成比1, 0);
                                }
                                else
                                {
                                    retList[j].構成比1 = (retList[j].年合計 / retList[j].総年合計) * 100;
                                    Math.Round((decimal)retList[j].構成比1, 0);
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
                                    if (retList[i].親子区分ID != "子")
                                    {
                                        if (retList[i].区分 == "当年")
                                        {
                                            p合計 = retList[i].年合計;
                                            T当年合計 = p合計 + T当年合計;
                                        }

                                        if (retList[i].区分 == "前年")
                                        {
                                            p合計 = retList[i].年合計;
                                            Z前年合計 = p合計 + Z前年合計;
                                        }

                                        if (retList[i].区分 == "前々年")
                                        {
                                            p合計 = retList[i].年合計;
                                            Z前々年合計 = p合計 + Z前々年合計;
                                        }
                                    }
                                    else
                                    {
                                        continue;
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
                                        retList[j].構成比1 = (retList[j].年合計 / 1) * 100;
                                        Math.Round((decimal)retList[j].構成比1, 0);
                                    }
                                    else
                                    {
                                        retList[j].構成比1 = (retList[j].年合計 / retList[j].総年合計) * 100;
                                        Math.Round((decimal)retList[j].構成比1, 0);
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
                                    if (retList[i].親子区分ID != "子")
                                    {
                                        p合計 = retList[i].年合計;
                                        T当年合計 = p合計 + T当年合計;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                //構成比計算
                                for (int j = 0; j < retList.Count; j++)
                                {

                                    retList[j].総年合計 = T当年合計;
                                    //0除算処理
                                    //構成比計算
                                    if (retList[j].総年合計 == 0)
                                    {
                                        retList[j].構成比1 = (retList[j].年合計 / 1) * 100;
                                        Math.Round((decimal)retList[j].構成比1, 0);
                                    }
                                    else
                                    {
                                        retList[j].構成比1 = (retList[j].年合計 / retList[j].総年合計) * 100;
                                        Math.Round((decimal)retList[j].構成比1, 0);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(p支払先From) && !string.IsNullOrEmpty(p支払先To))
                        {
                            int? ipForm = AppCommon.IntParse(p支払先From);
                            int? ipTo = AppCommon.IntParse(p支払先To);
                            query = query.Where(c => (c.支払先コード >= ipForm && c.支払先コード <= ipTo));
                        }
                        else if (!string.IsNullOrEmpty(p支払先From))
                        {
                            int? ipFrom = AppCommon.IntParse(p支払先From);
                            query = query.Where(c => (c.支払先コード >= ipFrom));
                        }
                        else if (!string.IsNullOrEmpty(p支払先To))
                        {
                            int? ipTo = AppCommon.IntParse(p支払先To);
                            query = query.Where(c => (c.支払先コード <= ipTo));
                        }

                        //前年全前年
                        if (前年前々年 == true)
                        {
                            //総年合計計算
                            retList = query.ToList();
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].親子区分ID != "子")
                                {
                                    if (retList[i].区分 == "当年")
                                    {
                                        p合計 = retList[i].年合計;
                                        T当年合計 = p合計 + T当年合計;
                                    }

                                    if (retList[i].区分 == "前年")
                                    {
                                        p合計 = retList[i].年合計;
                                        Z前年合計 = p合計 + Z前年合計;
                                    }

                                    if (retList[i].区分 == "前々年")
                                    {
                                        p合計 = retList[i].年合計;
                                        Z前々年合計 = p合計 + Z前々年合計;
                                    }
                                }
                                else
                                {
                                    continue;
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
                                    retList[j].構成比1 = (retList[j].年合計 / 1) * 100;
                                    Math.Round((decimal)retList[j].構成比1, 0);
                                }
                                else
                                {
                                    retList[j].構成比1 = (retList[j].年合計 / retList[j].総年合計) * 100;
                                    Math.Round((decimal)retList[j].構成比1, 0);
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
                                    if (retList[i].親子区分ID != "子")
                                    {
                                        if (retList[i].区分 == "当年")
                                        {
                                            p合計 = retList[i].年合計;
                                            T当年合計 = p合計 + T当年合計;
                                        }

                                        if (retList[i].区分 == "前年")
                                        {
                                            p合計 = retList[i].年合計;
                                            Z前年合計 = p合計 + Z前年合計;
                                        }

                                        if (retList[i].区分 == "前々年")
                                        {
                                            p合計 = retList[i].年合計;
                                            Z前々年合計 = p合計 + Z前々年合計;
                                        }
                                    }
                                    else
                                    {
                                        continue;
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
                                        retList[j].構成比1 = (retList[j].年合計 / 1) * 100;
                                        Math.Round((decimal)retList[j].構成比1, 0);
                                    }
                                    else
                                    {
                                        retList[j].構成比1 = (retList[j].年合計 / retList[j].総年合計) * 100;
                                        Math.Round((decimal)retList[j].構成比1, 0);
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
                                    if (retList[i].親子区分ID != "子")
                                    {
                                        p合計 = retList[i].年合計;
                                        T当年合計 = p合計 + T当年合計;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                //構成比計算
                                for (int j = 0; j < retList.Count; j++)
                                {
                                    retList[j].総年合計 = T当年合計;
                                    //0除算処理
                                    //構成比計算
                                    if (retList[j].総年合計 == 0)
                                    {
                                        retList[j].構成比1 = (retList[j].年合計 / 1) * 100;
                                        Math.Round((decimal)retList[j].構成比1, 0);
                                    }
                                    else
                                    {
                                        retList[j].構成比1 = (retList[j].年合計 / retList[j].総年合計) * 100;
                                        Math.Round((decimal)retList[j].構成比1, 0);
                                    }
                                }
                            }
                        }
                    }
                }

                //ピックアップ
                if (i支払先List.Length > 0)
                {
                    for (int i = 0; i < retList.Count; i++)
                    {
                        支払先指定表示 = 支払先指定表示 + i支払先List[i].ToString();

                        if (i < i支払先List.Length)
                        {

                            if (i == i支払先List.Length - 1)
                            {
                                break;
                            }

                            支払先指定表示 = 支払先指定表示 + ",";

                        }

                        if (i支払先List.Length == 1)
                        {
                            break;
                        }

                    }
                }


                foreach (var Row in retList)
                {
                    if (Row.親子区分ID != "子")
                    {
                        if (Row.総年合計 == 0)
                        {
                            Row.構成比1 = (Row.年合計 / 1) * 100;
                            Math.Round((decimal)Row.構成比1, 0);
                        }
                        else
                        {
                            Row.構成比1 = (Row.年合計 / Row.総年合計) * 100;
                            Math.Round((decimal)Row.構成比1, 0);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                return retList;
            }
        }
        #endregion

        #region CSV出力

        public List<NNG02010_Member_CSV> SEARCH_NNG02010_CSV_GetDataList(string p支払先From, string p支払先To, int?[] i支払先List, string p作成年月, string p作成締日, string p作成年, string p作成月, int 支払区分, bool b全締日集計, bool 前年前々年, int 表示区分_CValue, int p表示順序)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<NNG02010_Member_CSV> retList = new List<NNG02010_Member_CSV>();
                context.Connection.Open();

                //支払先指定　表示用
                string 支払先指定表示 = string.Empty;
                //当月開始日付
                // DateTime d当月 = DateTime.Parse(p作成年 + "/" + p作成月 + "/" + "01").AddMonths(0);
                DateTime d当月;
                DateTime.TryParse(p作成年 + "/" + p作成月 + "/" + "01", out d当月);

				string s月1 = d当月.AddMonths(0).Month.ToString() + "月";
				string s月2 = d当月.AddMonths(1).Month.ToString() + "月";
				string s月3 = d当月.AddMonths(2).Month.ToString() + "月";
				string s月4 = d当月.AddMonths(3).Month.ToString() + "月";
				string s月5 = d当月.AddMonths(4).Month.ToString() + "月";
				string s月6 = d当月.AddMonths(5).Month.ToString() + "月";
				string s月7 = d当月.AddMonths(6).Month.ToString() + "月";
				string s月8 = d当月.AddMonths(7).Month.ToString() + "月";
				string s月9 = d当月.AddMonths(8).Month.ToString() + "月";
				string s月10 = d当月.AddMonths(9).Month.ToString() + "月";
				string s月11 = d当月.AddMonths(10).Month.ToString() + "月";
				string s月12 = d当月.AddMonths(11).Month.ToString() + "月";


                //範囲データを設定
                int 月1 = d当月.AddMonths(0).Year * 100 + d当月.AddMonths(0).Month;
                int 月2 = d当月.AddMonths(1).Year * 100 + d当月.AddMonths(1).Month;
                int 月3 = d当月.AddMonths(2).Year * 100 + d当月.AddMonths(2).Month;
                int 月4 = d当月.AddMonths(3).Year * 100 + d当月.AddMonths(3).Month;
                int 月5 = d当月.AddMonths(4).Year * 100 + d当月.AddMonths(4).Month;
                int 月6 = d当月.AddMonths(5).Year * 100 + d当月.AddMonths(5).Month;
                int 月7 = d当月.AddMonths(6).Year * 100 + d当月.AddMonths(6).Month;
                int 月8 = d当月.AddMonths(7).Year * 100 + d当月.AddMonths(7).Month;
                int 月9 = d当月.AddMonths(8).Year * 100 + d当月.AddMonths(8).Month;
                int 月10 = d当月.AddMonths(9).Year * 100 + d当月.AddMonths(9).Month;
                int 月11 = d当月.AddMonths(10).Year * 100 + d当月.AddMonths(10).Month;
                int 月12 = d当月.AddMonths(11).Year * 100 + d当月.AddMonths(11).Month;
                //前年開始日付
                int 前年月1 = d当月.AddMonths(-12).Year * 100 + d当月.AddMonths(-12).Month;
                int 前年月2 = d当月.AddMonths(-11).Year * 100 + d当月.AddMonths(-11).Month;
                int 前年月3 = d当月.AddMonths(-10).Year * 100 + d当月.AddMonths(-10).Month;
                int 前年月4 = d当月.AddMonths(-9).Year * 100 + d当月.AddMonths(-9).Month;
                int 前年月5 = d当月.AddMonths(-8).Year * 100 + d当月.AddMonths(-8).Month;
                int 前年月6 = d当月.AddMonths(-7).Year * 100 + d当月.AddMonths(-7).Month;
                int 前年月7 = d当月.AddMonths(-6).Year * 100 + d当月.AddMonths(-6).Month;
                int 前年月8 = d当月.AddMonths(-5).Year * 100 + d当月.AddMonths(-5).Month;
                int 前年月9 = d当月.AddMonths(-4).Year * 100 + d当月.AddMonths(-4).Month;
                int 前年月10 = d当月.AddMonths(-3).Year * 100 + d当月.AddMonths(-3).Month;
                int 前年月11 = d当月.AddMonths(-2).Year * 100 + d当月.AddMonths(-2).Month;
                int 前年月12 = d当月.AddMonths(-1).Year * 100 + d当月.AddMonths(-1).Month;
                //前々年開始日付
                int 前々年月1 = d当月.AddMonths(-24).Year * 100 + d当月.AddMonths(-24).Month;
                int 前々年月2 = d当月.AddMonths(-23).Year * 100 + d当月.AddMonths(-23).Month;
                int 前々年月3 = d当月.AddMonths(-22).Year * 100 + d当月.AddMonths(-22).Month;
                int 前々年月4 = d当月.AddMonths(-21).Year * 100 + d当月.AddMonths(-21).Month;
                int 前々年月5 = d当月.AddMonths(-20).Year * 100 + d当月.AddMonths(-20).Month;
                int 前々年月6 = d当月.AddMonths(-19).Year * 100 + d当月.AddMonths(-19).Month;
                int 前々年月7 = d当月.AddMonths(-18).Year * 100 + d当月.AddMonths(-18).Month;
                int 前々年月8 = d当月.AddMonths(-17).Year * 100 + d当月.AddMonths(-17).Month;
                int 前々年月9 = d当月.AddMonths(-16).Year * 100 + d当月.AddMonths(-16).Month;
                int 前々年月10 = d当月.AddMonths(-15).Year * 100 + d当月.AddMonths(-15).Month;
                int 前々年月11 = d当月.AddMonths(-14).Year * 100 + d当月.AddMonths(-14).Month;
                int 前々年月12 = d当月.AddMonths(-14).Year * 100 + d当月.AddMonths(-13).Month;

                //当月終了日付 ***LINQ内表示のみに使用***
                DateTime d当年_F = d当月.AddMonths(11);

                var query = (from m01 in context.M01_TOK
                             join v01 in context.V_支払締日 on m01.得意先KEY equals v01.支払先KEY into v01Group
                             where m01.削除日付 == null
                             select new NNG02010_Member_CSV
                             {
                                 支払先コード = m01.得意先ID,
                                 支払先名 = m01.得意先名１,
                                 支払区分 = m01.取引区分,
                                 区分 = 前年前々年 == false ? "" : "当年",
                                 売上金額 = v01Group.Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Sum(v01 => v01.締日売上金額),
                                 月1 = v01Group.Where(v01 => v01.集計年月 == 月1).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月1).Sum(v01 => v01.締日売上金額),
                                 月2 = v01Group.Where(v01 => v01.集計年月 == 月2).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月2).Sum(v01 => v01.締日売上金額),
                                 月3 = v01Group.Where(v01 => v01.集計年月 == 月3).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月3).Sum(v01 => v01.締日売上金額),
                                 月4 = v01Group.Where(v01 => v01.集計年月 == 月4).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月4).Sum(v01 => v01.締日売上金額),
                                 月5 = v01Group.Where(v01 => v01.集計年月 == 月5).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月5).Sum(v01 => v01.締日売上金額),
                                 月6 = v01Group.Where(v01 => v01.集計年月 == 月6).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月6).Sum(v01 => v01.締日売上金額),
                                 月7 = v01Group.Where(v01 => v01.集計年月 == 月7).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月7).Sum(v01 => v01.締日売上金額),
                                 月8 = v01Group.Where(v01 => v01.集計年月 == 月8).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月8).Sum(v01 => v01.締日売上金額),
                                 月9 = v01Group.Where(v01 => v01.集計年月 == 月9).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月9).Sum(v01 => v01.締日売上金額),
                                 月10 = v01Group.Where(v01 => v01.集計年月 == 月10).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月10).Sum(v01 => v01.締日売上金額),
                                 月11 = v01Group.Where(v01 => v01.集計年月 == 月11).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月11).Sum(v01 => v01.締日売上金額),
                                 月12 = v01Group.Where(v01 => v01.集計年月 == 月12).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 月12).Sum(v01 => v01.締日売上金額),
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
								 年合計 = v01Group.Where(v01 => v01.集計年月 >= 月1 && v01.集計年月 <= 月12).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 >= 月1 && v01.集計年月 <= 月12).Sum(v01 => v01.締日売上金額),
                                 平均 = v01Group.Where(c => c.集計年月 >= 月1 && c.集計年月 <= 月12).Count(c => c.締日売上金額 != 0) == 0 ? 0 : v01Group.Where(c => c.集計年月 >= 月1 && c.集計年月 <= 月12).Sum(c => c.締日売上金額) / v01Group.Where(c => c.集計年月 >= 月1 && c.集計年月 <= 月12).Count(c => c.締日売上金額 != 0),
                                 総年合計 = 0,
                                 売上順位データ = 前年前々年 == false ? v01Group.Where(v01 => v01.集計年月 >= 月1 && v01.集計年月 <= 月12).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 >= 前々年月12 && v01.集計年月 <= 月12).Sum(v01 => v01.締日売上金額) : v01Group.Where(v01 => v01.集計年月 >= 前々年月12 && v01.集計年月 <= 月12).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 >= 前々年月12 && v01.集計年月 <= 月12).Sum(v01 => v01.締日売上金額),
                                 構成比1 = 0,
                                 全締日 = p作成締日 == "" ? "全締日集計" : p作成締日,
                                 親子区分ID = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                 締日 = m01.Ｔ締日,
                                 かな読み = m01.かな読み,
                                 表示区分 = 表示区分_CValue == 0 ? "（売上あり支払先のみ）" : "（売上無し支払先含む）",
                                 表示順序 = p表示順序 == 0 ? "支払先ID" : p表示順序 == 1 ? "かな読み" : p表示順序 == 2 ? "売上金額" : "支払先ID",
                             }).AsQueryable();

                //*** 前年前々年処理 ***
                if (前年前々年 == true)
                {
                    query = query.Union(from m01 in context.M01_TOK
                                        join v01 in context.V_支払締日 on m01.得意先KEY equals v01.支払先KEY into v01Group
                                        where m01.削除日付 == null
                                        select new NNG02010_Member_CSV
                                        {
                                            支払先コード = m01.得意先ID,
                                            支払先名 = m01.得意先名１,
                                            支払区分 = m01.取引区分,
                                            区分 = "前年",
                                            売上金額 = v01Group.Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Sum(v01 => v01.締日売上金額),
                                            月1 = v01Group.Where(v01 => v01.集計年月 == 前年月1).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月1).Sum(v01 => v01.締日売上金額),
                                            月2 = v01Group.Where(v01 => v01.集計年月 == 前年月2).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月2).Sum(v01 => v01.締日売上金額),
                                            月3 = v01Group.Where(v01 => v01.集計年月 == 前年月3).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月3).Sum(v01 => v01.締日売上金額),
                                            月4 = v01Group.Where(v01 => v01.集計年月 == 前年月4).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月4).Sum(v01 => v01.締日売上金額),
                                            月5 = v01Group.Where(v01 => v01.集計年月 == 前年月5).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月5).Sum(v01 => v01.締日売上金額),
                                            月6 = v01Group.Where(v01 => v01.集計年月 == 前年月6).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月6).Sum(v01 => v01.締日売上金額),
                                            月7 = v01Group.Where(v01 => v01.集計年月 == 前年月7).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月7).Sum(v01 => v01.締日売上金額),
                                            月8 = v01Group.Where(v01 => v01.集計年月 == 前年月8).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月8).Sum(v01 => v01.締日売上金額),
                                            月9 = v01Group.Where(v01 => v01.集計年月 == 前年月9).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月9).Sum(v01 => v01.締日売上金額),
                                            月10 = v01Group.Where(v01 => v01.集計年月 == 前年月10).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月10).Sum(v01 => v01.締日売上金額),
                                            月11 = v01Group.Where(v01 => v01.集計年月 == 前年月11).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月11).Sum(v01 => v01.締日売上金額),
                                            月12 = v01Group.Where(v01 => v01.集計年月 == 前年月12).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前年月12).Sum(v01 => v01.締日売上金額),
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
											年合計 = v01Group.Where(v01 => v01.集計年月 >= 前年月1 && v01.集計年月 <= 前年月12).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 >= 前年月1 && v01.集計年月 <= 前年月12).Sum(v01 => v01.締日売上金額),
                                            平均 = v01Group.Where(c => c.集計年月 >= 前年月1 && c.集計年月 <= 前年月12).Count(c => c.締日売上金額 != 0) == 0 ? 0 : v01Group.Where(c => c.集計年月 >= 前年月1 && c.集計年月 <= 前年月12).Sum(c => c.締日売上金額) / v01Group.Where(c => c.集計年月 >= 前年月1 && c.集計年月 <= 前年月12).Count(c => c.締日売上金額 != 0),
                                            総年合計 = 0,
                                            売上順位データ = v01Group.Where(v01 => v01.集計年月 >= 前々年月12 && v01.集計年月 <= 月12).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 >= 前々年月12 && v01.集計年月 <= 月12).Sum(v01 => v01.締日売上金額),
                                            構成比1 = 0,
                                            全締日 = p作成締日 == "" ? "全締日集計" : p作成締日,
                                            親子区分ID = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                            締日 = m01.Ｔ締日,
                                            かな読み = m01.かな読み,
                                            表示区分 = 表示区分_CValue == 0 ? "（売上あり支払先のみ）" : "（売上無し支払先含む）",
                                            表示順序 = p表示順序 == 0 ? "支払先ID" : p表示順序 == 1 ? "かな読み" : p表示順序 == 2 ? "売上金額" : "支払先ID",
                                        }).AsQueryable();
                    //*** 前々年処理 ***
                    query = query.Union(from m01 in context.M01_TOK
                                        join v01 in context.V_支払締日 on m01.得意先KEY equals v01.支払先KEY into v01Group
                                        where m01.削除日付 == null
                                        select new NNG02010_Member_CSV
                                        {
                                            支払先コード = m01.得意先ID,
                                            支払先名 = m01.得意先名１,
                                            支払区分 = m01.取引区分,
                                            区分 = "前々年",
                                            売上金額 = v01Group.Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Sum(v01 => v01.締日売上金額),
                                            月1 = v01Group.Where(v01 => v01.集計年月 == 前々年月1).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月1).Sum(v01 => v01.締日売上金額),
                                            月2 = v01Group.Where(v01 => v01.集計年月 == 前々年月2).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月2).Sum(v01 => v01.締日売上金額),
                                            月3 = v01Group.Where(v01 => v01.集計年月 == 前々年月3).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月3).Sum(v01 => v01.締日売上金額),
                                            月4 = v01Group.Where(v01 => v01.集計年月 == 前々年月4).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月4).Sum(v01 => v01.締日売上金額),
                                            月5 = v01Group.Where(v01 => v01.集計年月 == 前々年月5).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月5).Sum(v01 => v01.締日売上金額),
                                            月6 = v01Group.Where(v01 => v01.集計年月 == 前々年月6).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月6).Sum(v01 => v01.締日売上金額),
                                            月7 = v01Group.Where(v01 => v01.集計年月 == 前々年月7).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月7).Sum(v01 => v01.締日売上金額),
                                            月8 = v01Group.Where(v01 => v01.集計年月 == 前々年月8).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月8).Sum(v01 => v01.締日売上金額),
                                            月9 = v01Group.Where(v01 => v01.集計年月 == 前々年月9).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月9).Sum(v01 => v01.締日売上金額),
                                            月10 = v01Group.Where(v01 => v01.集計年月 == 前々年月10).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月10).Sum(v01 => v01.締日売上金額),
                                            月11 = v01Group.Where(v01 => v01.集計年月 == 前々年月11).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月11).Sum(v01 => v01.締日売上金額),
                                            月12 = v01Group.Where(v01 => v01.集計年月 == 前々年月12).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 前々年月12).Sum(v01 => v01.締日売上金額),
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
											年合計 = v01Group.Where(v01 => v01.集計年月 >= 前々年月1 && v01.集計年月 <= 前々年月12).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 >= 前々年月1 && v01.集計年月 <= 前々年月12).Sum(v01 => v01.締日売上金額),
                                            平均 = v01Group.Where(c => c.集計年月 >= 前々年月1 && c.集計年月 <= 前々年月12).Count(c => c.締日売上金額 != 0) == 0 ? 0 : v01Group.Where(c => c.集計年月 >= 前々年月1 && c.集計年月 <= 前々年月12).Sum(c => c.締日売上金額) / v01Group.Where(c => c.集計年月 >= 前々年月1 && c.集計年月 <= 前々年月12).Count(c => c.締日売上金額 != 0),
                                            総年合計 = 0,
                                            売上順位データ = v01Group.Where(v01 => v01.集計年月 >= 前々年月12 && v01.集計年月 <= 月12).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 >= 前々年月12 && v01.集計年月 <= 月12).Sum(v01 => v01.締日売上金額),
                                            構成比1 = 0,
                                            全締日 = p作成締日 == "" ? "全締日集計" : p作成締日,
                                            親子区分ID = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                            締日 = m01.Ｔ締日,
                                            かな読み = m01.かな読み,
                                            表示区分 = 表示区分_CValue == 0 ? "（売上あり支払先のみ）" : "（売上無し支払先含む）",
                                            表示順序 = p表示順序 == 0 ? "支払先ID" : p表示順序 == 1 ? "かな読み" : p表示順序 == 2 ? "売上金額" : "支払先ID",
                                        }).AsQueryable();
                }
                //重複処理
                query = query.Distinct();

                //全締日集計処理
                if (b全締日集計 == true)
                {
                    query = query.Where(c => c.締日 >= 1 && c.締日 <= 31);
                }

                //締日処理　
                if (!string.IsNullOrEmpty(p作成締日))
                {
                    int? p変換作成締日 = AppCommon.IntParse(p作成締日);
                    query = query.Where(c => c.締日 == p変換作成締日);
                }

                //支払区分処理
                switch (支払区分)
                {
                    //支払取引全体
                    case 0:
                        query = query.Where(c => c.支払区分 == 0);
                        break;

                    //支払先
                    case 1:
                        query = query.Where(c => c.支払区分 == 2);
                        break;

                    //経費先
                    case 2:
                        query = query.Where(c => c.支払区分 == 3);
                        break;

                    default:
                        break;
                }

                //表示順序処理
                if (前年前々年 == true)
                {
                    switch (表示区分_CValue)
                    {
                        //０を含まない
                        case 0:
                            query = query.Where(c => c.売上順位データ > 0);
                            break;

                        //０を含む
                        case 1:
                            query = query.Where(c => c.売上順位データ >= 0);
                            break;

                        default:
                            break;
                    }

                    //表示順序処理
                    switch (p表示順序)
                    {
                        case 0:
                            query = query.OrderBy(c => c.支払先コード).ThenByDescending(c => c.区分);
                            break;

                        case 1:
                            query = query.OrderBy(c => c.かな読み).ThenBy(c => c.支払先コード).ThenByDescending(c => c.区分);
                            break;

                        case 2:
                            query = query.OrderByDescending(c => c.売上順位データ).ThenBy(c => c.支払先コード).ThenByDescending(c => c.区分);
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    switch (表示区分_CValue)
                    {
                        case 0:
                            query = query.Where(c => c.年合計 > 0);
                            break;

                        case 1:
                            query = query.Where(c => c.年合計 >= 0);
                            break;

                        default:
                            break;
                    }

                    //表示順序処理
                    switch (p表示順序)
                    {
                        case 0:
                            query = query.OrderBy(c => c.支払先コード);
                            break;

                        case 1:
                            query = query.OrderBy(c => c.かな読み);
                            break;

                        case 2:
                            query = query.OrderByDescending(c => c.年合計);
                            break;

                        default:
                            break;
                    }
                }

                //支払先指定の表示
                if (i支払先List.Length > 0)
                {
                    for (int i = 0; i < query.Count(); i++)
                    {
                        支払先指定表示 = 支払先指定表示 + i支払先List[i].ToString();

                        if (i < i支払先List.Length)
                        {
                            if (i == i支払先List.Length - 1)
                            {
                                break;
                            }
                            支払先指定表示 = 支払先指定表示 + ",";
                        }
                        if (i支払先List.Length == 1)
                        {
                            break;
                        }
                    }
                }

                var intCause = i支払先List;
                decimal? p合計 = 0;
                decimal? T当年合計 = 0;
                decimal? Z前年合計 = 0;
                decimal? Z前々年合計 = 0;
                //ピックアップ処理
                if (string.IsNullOrEmpty(p支払先From + p支払先To))
                {
                    if (i支払先List.Length > 0)
                    {
                        query = query.Where(c => intCause.Contains(c.支払先コード));
                    }

                    //前年全前年
                    if (前年前々年 == true)
                    {
                        //総年合計計算
                        retList = query.ToList();
                        for (int i = 0; i < retList.Count; i++)
                        {
                            if (retList[i].親子区分ID != "子")
                            {

                                if (retList[i].区分 == "当年")
                                {
                                    p合計 = retList[i].年合計;
                                    T当年合計 = p合計 + T当年合計;
                                }

                                if (retList[i].区分 == "前年")
                                {
                                    p合計 = retList[i].年合計;
                                    Z前年合計 = p合計 + Z前年合計;
                                }

                                if (retList[i].区分 == "前々年")
                                {
                                    p合計 = retList[i].年合計;
                                    Z前々年合計 = p合計 + Z前々年合計;
                                }
                            }
                            else
                            {
                                continue;
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
                                retList[j].構成比1 = (retList[j].年合計 / 1) * 100;
                                Math.Round((decimal)retList[j].構成比1, 0);
                            }
                            else
                            {
                                retList[j].構成比1 = (retList[j].年合計 / retList[j].総年合計) * 100;
                                Math.Round((decimal)retList[j].構成比1, 0);
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
                            if (retList[i].親子区分ID != "子")
                            {
                                p合計 = retList[i].年合計;
                                T当年合計 = p合計 + T当年合計;
                            }
                            else
                            {
                                continue;
                            }
                        }

                        //構成比計算
                        for (int j = 0; j < retList.Count; j++)
                        {
                            retList[j].総年合計 = T当年合計;
                            //0除算処理
                            //構成比計算
                            if (retList[j].総年合計 == 0)
                            {
                                retList[j].構成比1 = (retList[j].年合計 / 1) * 100;
                                Math.Round((decimal)retList[j].構成比1, 0);
                            }
                            else
                            {
                                retList[j].構成比1 = (retList[j].年合計 / retList[j].総年合計) * 100;
                                Math.Round((decimal)retList[j].構成比1, 0);
                            }
                        }
                    }
                }
                else
                {
                    //From、To、ピックアップ指定を入力された場合
                    int iFrom = 0;
                    int iTo = 0;

                    if (i支払先List.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(p支払先From) && !string.IsNullOrEmpty(p支払先To))
                        {
                            iFrom = AppCommon.IntParse(p支払先From);
                            iTo = AppCommon.IntParse(p支払先To);
                            query = query.Where(c => intCause.Contains(c.支払先コード) || (c.支払先コード >= iFrom && c.支払先コード <= iTo));
                        }
                        else if (!string.IsNullOrEmpty(p支払先From))
                        {
                            iFrom = AppCommon.IntParse(p支払先From);
                            query = query.Where(c => intCause.Contains(c.支払先コード) || (c.支払先コード >= iFrom));
                        }
                        else if (!string.IsNullOrEmpty(p支払先To))
                        {
                            iTo = AppCommon.IntParse(p支払先To);
                            query = query.Where(c => intCause.Contains(c.支払先コード) || (c.支払先コード <= iTo));
                        }

                        //前年全前年
                        if (前年前々年 == true)
                        {
                            //総年合計計算
                            retList = query.ToList();
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].親子区分ID != "子")
                                {
                                    if (retList[i].区分 == "当年")
                                    {
                                        p合計 = retList[i].年合計;
                                        T当年合計 = p合計 + T当年合計;
                                    }

                                    if (retList[i].区分 == "前年")
                                    {
                                        p合計 = retList[i].年合計;
                                        Z前年合計 = p合計 + Z前年合計;
                                    }

                                    if (retList[i].区分 == "前々年")
                                    {
                                        p合計 = retList[i].年合計;
                                        Z前々年合計 = p合計 + Z前々年合計;
                                    }
                                }
                                else
                                {
                                    continue;
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
                                    retList[j].構成比1 = (retList[j].年合計 / 1) * 100;
                                    Math.Round((decimal)retList[j].構成比1, 0);
                                }
                                else
                                {
                                    retList[j].構成比1 = (retList[j].年合計 / retList[j].総年合計) * 100;
                                    Math.Round((decimal)retList[j].構成比1, 0);
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
                                    if (retList[i].親子区分ID != "子")
                                    {
                                        if (retList[i].区分 == "当年")
                                        {
                                            p合計 = retList[i].年合計;
                                            T当年合計 = p合計 + T当年合計;
                                        }

                                        if (retList[i].区分 == "前年")
                                        {
                                            p合計 = retList[i].年合計;
                                            Z前年合計 = p合計 + Z前年合計;
                                        }

                                        if (retList[i].区分 == "前々年")
                                        {
                                            p合計 = retList[i].年合計;
                                            Z前々年合計 = p合計 + Z前々年合計;
                                        }
                                    }
                                    else
                                    {
                                        continue;
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
                                        retList[j].構成比1 = (retList[j].年合計 / 1) * 100;
                                        Math.Round((decimal)retList[j].構成比1, 0);
                                    }
                                    else
                                    {
                                        retList[j].構成比1 = (retList[j].年合計 / retList[j].総年合計) * 100;
                                        Math.Round((decimal)retList[j].構成比1, 0);
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
                                    if (retList[i].親子区分ID != "子")
                                    {
                                        p合計 = retList[i].年合計;
                                        T当年合計 = p合計 + T当年合計;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                //構成比計算
                                for (int j = 0; j < retList.Count; j++)
                                {

                                    retList[j].総年合計 = T当年合計;
                                    //0除算処理
                                    //構成比計算
                                    if (retList[j].総年合計 == 0)
                                    {
                                        retList[j].構成比1 = (retList[j].年合計 / 1) * 100;
                                        Math.Round((decimal)retList[j].構成比1, 0);
                                    }
                                    else
                                    {
                                        retList[j].構成比1 = (retList[j].年合計 / retList[j].総年合計) * 100;
                                        Math.Round((decimal)retList[j].構成比1, 0);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(p支払先From) && !string.IsNullOrEmpty(p支払先To))
                        {
                            int? ipForm = AppCommon.IntParse(p支払先From);
                            int? ipTo = AppCommon.IntParse(p支払先To);
                            query = query.Where(c => (c.支払先コード >= ipForm && c.支払先コード <= ipTo));
                        }
                        else if (!string.IsNullOrEmpty(p支払先From))
                        {
                            int? ipFrom = AppCommon.IntParse(p支払先From);
                            query = query.Where(c => (c.支払先コード >= ipFrom));
                        }
                        else if (!string.IsNullOrEmpty(p支払先To))
                        {
                            int? ipTo = AppCommon.IntParse(p支払先To);
                            query = query.Where(c => (c.支払先コード <= ipTo));
                        }

                        //前年全前年
                        if (前年前々年 == true)
                        {
                            //総年合計計算
                            retList = query.ToList();
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].親子区分ID != "子")
                                {
                                    if (retList[i].区分 == "当年")
                                    {
                                        p合計 = retList[i].年合計;
                                        T当年合計 = p合計 + T当年合計;
                                    }

                                    if (retList[i].区分 == "前年")
                                    {
                                        p合計 = retList[i].年合計;
                                        Z前年合計 = p合計 + Z前年合計;
                                    }

                                    if (retList[i].区分 == "前々年")
                                    {
                                        p合計 = retList[i].年合計;
                                        Z前々年合計 = p合計 + Z前々年合計;
                                    }
                                }
                                else
                                {
                                    continue;
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
                                    retList[j].構成比1 = (retList[j].年合計 / 1) * 100;
                                    Math.Round((decimal)retList[j].構成比1, 0);
                                }
                                else
                                {
                                    retList[j].構成比1 = (retList[j].年合計 / retList[j].総年合計) * 100;
                                    Math.Round((decimal)retList[j].構成比1, 0);
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
                                    if (retList[i].親子区分ID != "子")
                                    {
                                        if (retList[i].区分 == "当年")
                                        {
                                            p合計 = retList[i].年合計;
                                            T当年合計 = p合計 + T当年合計;
                                        }

                                        if (retList[i].区分 == "前年")
                                        {
                                            p合計 = retList[i].年合計;
                                            Z前年合計 = p合計 + Z前年合計;
                                        }

                                        if (retList[i].区分 == "前々年")
                                        {
                                            p合計 = retList[i].年合計;
                                            Z前々年合計 = p合計 + Z前々年合計;
                                        }
                                    }
                                    else
                                    {
                                        continue;
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
                                        retList[j].構成比1 = (retList[j].年合計 / 1) * 100;
                                        Math.Round((decimal)retList[j].構成比1, 0);
                                    }
                                    else
                                    {
                                        retList[j].構成比1 = (retList[j].年合計 / retList[j].総年合計) * 100;
                                        Math.Round((decimal)retList[j].構成比1, 0);
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
                                    if (retList[i].親子区分ID != "子")
                                    {
                                        p合計 = retList[i].年合計;
                                        T当年合計 = p合計 + T当年合計;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                //構成比計算
                                for (int j = 0; j < retList.Count; j++)
                                {
                                    retList[j].総年合計 = T当年合計;
                                    //0除算処理
                                    //構成比計算
                                    if (retList[j].総年合計 == 0)
                                    {
                                        retList[j].構成比1 = (retList[j].年合計 / 1) * 100;
                                        Math.Round((decimal)retList[j].構成比1, 0);
                                    }
                                    else
                                    {
                                        retList[j].構成比1 = (retList[j].年合計 / retList[j].総年合計) * 100;
                                        Math.Round((decimal)retList[j].構成比1, 0);
                                    }
                                }
                            }
                        }
                    }
                }

                //ピックアップ
                if (i支払先List.Length > 0)
                {
                    for (int i = 0; i < retList.Count; i++)
                    {
                        支払先指定表示 = 支払先指定表示 + i支払先List[i].ToString();

                        if (i < i支払先List.Length)
                        {

                            if (i == i支払先List.Length - 1)
                            {
                                break;
                            }

                            支払先指定表示 = 支払先指定表示 + ",";

                        }

                        if (i支払先List.Length == 1)
                        {
                            break;
                        }

                    }
                }


                foreach (var Row in retList)
                {
                    if (Row.親子区分ID != "子")
                    {
                        if (Row.総年合計 == 0)
                        {
                            Row.構成比1 = (Row.年合計 / 1) * 100;
                            Math.Round((decimal)Row.構成比1, 0);
                        }
                        else
                        {
                            Row.構成比1 = (Row.年合計 / Row.総年合計) * 100;
                            Math.Round((decimal)Row.構成比1, 0);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                return retList;
            }
        }
        #endregion
    }
}
