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
using System.Web.UI.WebControls;


namespace KyoeiSystem.Application.WCFService
{
    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コード、svc、および config ファイルで同時にクラス名 "DataDriveLogService" を変更できます。
    // 注意: このサービスをテストするために WCF テスト クライアントを起動するには、ソリューション エクスプローラーで DataDriveLogService.svc または DataDriveLogService.svc.cs を選択し、デバッグを開始してください。
    public class TKS04010
    {
        /// <summary>
        /// TKS06010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class TKS04010_Member_day
        {
            public DateTime? 日付 { get; set; }
        }
        /// <summary>
        /// TKS06010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class TKS04010_Member_Youbi
        {
            public string 曜日 { get; set; }
            public DateTime? 日付 { get; set; }
        }
        /// <summary>
        /// TKS06010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class TKS04010_Member
        {
            public DateTime? 日付 { get; set; }
            public string 曜日 { get; set; }
            public int?[] 得意先指定 { get; set; }
            public int? 得意先コード { get; set; }
            public string 得意先名 { get; set; }
            public decimal? 売上金額 { get; set; }
            public decimal? 通行料 { get; set; }
            public decimal? 距離割増１ { get; set; }
            public decimal? 距離割増２ { get; set; } 
            public decimal? 時間割増 { get; set; }
            public decimal? 当月売上額 { get; set; }
            public decimal? 傭車使用売上 { get; set; }
            public decimal? 傭車料 { get; set; }
            public decimal? 差益 { get; set; }
            public decimal? 差益率 { get; set; }
            public int? 件数 { get; set; }
            public string 未定 { get; set; }
            public DateTime 対象年月 { get; set; }
            public string 親子区分ID { get; set; }
            public string 得意先指定コード { get; set; }
            public string 得意先Sコード { get; set; }
            public string 得意先Fコード { get; set; }
            public DateTime? 締集計開始日 { get; set; }
            public DateTime? 締集計終了日 { get; set; }
            public string rpt集計開始日 { get; set; }
            public string rpt集計終了日 { get; set; }
        }
        /// <summary>
        /// TKS06010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class TKS04010_Member_CSV
		{
			public DateTime? 日付 { get; set; }
			public string 曜日 { get; set; }
			public int?[] 得意先指定 { get; set; }
			public int? 得意先コード { get; set; }
			public string 得意先名 { get; set; }
			public decimal? 売上金額 { get; set; }
			public decimal? 通行料 { get; set; }
			public decimal? 距離割増１ { get; set; }
			public decimal? 距離割増２ { get; set; }
			public decimal? 時間割増 { get; set; }
			public decimal? 当月売上額 { get; set; }
			public decimal? 傭車使用売上 { get; set; }
			public decimal? 傭車料 { get; set; }
			public decimal? 差益 { get; set; }
			public decimal? 差益率 { get; set; }
			public int? 件数 { get; set; }
			public string 未定 { get; set; }
        }

        #region 得意先売上日計表プレビュー
        /// <summary>
        /// TKS04010 得意先売上日計表プレビュー
        /// </summary>
        /// <param name="p商品ID">得意先コード</param>
        /// <returns>S02</returns>
        public List<TKS04010_Member> SEARCH_TKS04010_Preview(string p得意先From, string p得意先To, int?[] i得意先List, string p作成締日, string p作成年, string p作成月, DateTime p集計期間From, DateTime p集計期間To)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				int tokfrom = AppCommon.IntParse(p得意先From) == 0 ? int.MinValue : AppCommon.IntParse(p得意先From);
				int tokto = AppCommon.IntParse(p得意先To) == 0 ? int.MaxValue : AppCommon.IntParse(p得意先To);
				if ((string.IsNullOrEmpty(p得意先From + p得意先To) && i得意先List.Length != 0))
				{
					tokfrom = int.MaxValue;
					tokto = int.MaxValue;
				}

                //支払先指定　表示用
                string 得意先指定表示 = string.Empty;
                //query格納LIST
                List<TKS04010_Member> retList = new List<TKS04010_Member>();
                //日付格納LIST
                List<TKS04010_Member_day> retList_day = new List<TKS04010_Member_day>();
                //日付、曜日格納LIST
                List<TKS04010_Member_Youbi> retList_Youbi = new List<TKS04010_Member_Youbi>();

                //変数
                string[] array = new string[1];      　          //日付元配列
                string[,] DayOfWeek;                 　          //日付用
                int j = 0;                           　          //配列要素数用
                int nYear = 0, nMonth = 0, nDay = 0, sYear = 0, sMonth = 0, sDay = 0; 　//日付分割用
                DateTime f集計期間From, t集計期間To; 　          //集計開始・終了期間
                int 日数 = 0;

                //日付、曜日計算処理
                for (int i = 0; p集計期間From <= p集計期間To; i++)
                {
                    f集計期間From = p集計期間From;
                    t集計期間To = p集計期間To;
                    TimeSpan ts = t集計期間To - f集計期間From;   //日付の減算
                    日数 = ts.Days + 1;                          //日付は1～なので、+1。

                    Array.Resize(ref array, i + 1);   //配列要素の追加
                    array[i] = f集計期間From.AddDays(i).ToShortDateString();

                    nYear = Convert.ToInt32(array[i].Substring(0, 4));   //年分割
                    nMonth = Convert.ToInt32(array[i].Substring(5, 2));  //月分割
                    nDay = Convert.ToInt32(array[i].Substring(8, 2));    //日分割
                    sYear = Convert.ToInt32(f集計期間From.ToString().Substring(0, 4));   //表示用
                    sMonth = Convert.ToInt32(f集計期間From.ToString().Substring(5, 2));  //表示用
                    sDay = Convert.ToInt32(f集計期間From.ToString().Substring(8, 2));　　//表示用

                    DayOfWeek = new string[日数, 2];  //DayOfWeekの二次元配列要素数の設定

                    if (nMonth == 1 || nMonth == 2)   //曜日判定配列へ各々挿入（ツェラーの公式）
                    {
                        nYear--;
                        nMonth += 12;
                    }
                    //日付、曜日データを作成したリストに追加
                    retList_Youbi.Add(new TKS04010_Member_Youbi()
                    {
                        日付 = Convert.ToDateTime(DayOfWeek[i, j] = array[i]),
                        曜日 = DayOfWeek[i, j] = ((nYear + nYear / 4 - nYear / 100 + nYear / 400 + (13 * nMonth + 8) / 5 + nDay) % 7).ToString()
                    });
                    //条件の日付まで来たら終了
                    if (array[i] == t集計期間To.ToShortDateString())
                    {
                        break;
                    }
                }
				int 締日 = AppCommon.IntParse(p作成締日.ToString());
                context.Connection.Open();

                int[] lst;

                lst = (from m01 in context.M01_TOK
                       join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= p集計期間From && t01.請求日付 <= p集計期間To) && ((t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 == 1))) on m01.得意先KEY equals t01.得意先KEY
					   where m01.削除日付 == null && m01.Ｔ締日 == 締日
                       select m01.得意先KEY).ToArray();

                //締日集計処理
                var query = (from y01 in retList_Youbi
							 from m01 in context.M01_TOK.Where(x => (x.得意先ID >= tokfrom && x.得意先ID <= tokto || (i得意先List.Contains(x.得意先ID))))
                             join t01 in context.T01_TRN.Where(t01 => t01.請求日付 >= p集計期間From && t01.請求日付 <= p集計期間To && (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 == 1)) on y01.日付 equals t01.請求日付 into s01Group
                             join s01 in context.T01_TRN.Where(t01 => t01.請求日付 >= p集計期間From && t01.請求日付 <= p集計期間To && (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1)) on y01.日付 equals s01.請求日付 into s02Group
                             where lst.Contains(m01.得意先KEY) && m01.削除日付 == null
                             orderby m01.得意先KEY

                             select new TKS04010_Member
                             {
                                 日付 = y01.日付,
                                 曜日 = y01.曜日 == "0" ? "日" : y01.曜日 == "1" ? "月" : y01.曜日 == "2" ? "火" : y01.曜日 == "3" ? "水" : y01.曜日 == "4" ? "木" : y01.曜日 == "5" ? "金" : y01.曜日 == "6" ? "土" : "",
                                 得意先コード = m01.得意先ID,
                                 得意先名 = m01.略称名,
                                 売上金額 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上金額),
                                 通行料 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.通行料),
                                 距離割増１ = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.請求割増１),
                                 距離割増２ = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.請求割増２),
                                 当月売上額 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２),
                                 傭車使用売上 = s02Group.Where(grp => grp.得意先KEY == m01.得意先KEY && grp.支払先KEY > 0).Sum(grp => grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２),
                                 傭車料 = s02Group.Where(grp => grp.得意先KEY == m01.得意先KEY && grp.支払先KEY > 0).Sum(grp => grp.支払金額 + grp.支払通行料 + grp.支払割増１ + grp.支払割増２),
                                 差益 = s02Group.Where(grp => grp.得意先KEY == m01.得意先KEY && grp.支払先KEY > 0).Sum(grp => (grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２) - (grp.支払金額 + grp.支払通行料 + grp.支払割増１ + grp.支払割増２)),
                                 差益率 = 0,
                                 締集計開始日 = p集計期間From,
                                 締集計終了日 = p集計期間To,
                                 件数 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Count(),
                                 未定 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上未定区分) >= 1 ? "未定" : "",
                                 対象年月 = p集計期間From,
                                 親子区分ID = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 1 ? "親" : "子",
                                 得意先指定コード = 得意先指定表示 == "" ? "無" : 得意先指定表示,
                                 得意先Sコード = p得意先From == "" ? "" : p得意先From + " ～ ",
                                 得意先Fコード = p得意先To == "" ? "" : p得意先To,
                             }).AsQueryable();

				//if (!(string.IsNullOrEmpty(p得意先From + p得意先To) && i得意先List.Length == 0))
				//{
				//	//得意先が検索対象に入っていない時全データ取得
				//	if (string.IsNullOrEmpty(p得意先From + p得意先To))
				//	{
				//		query = query.Where(c => c.得意先コード >= int.MaxValue);
				//	}

				//	//得意先From処理　Min値
				//	if (!string.IsNullOrEmpty(p得意先From))
				//	{
				//		int i支払先FROM = AppCommon.IntParse(p得意先From);
				//		query = query.Where(c => c.得意先コード >= i支払先FROM);
				//	}

				//	//得意先To処理　Max値
				//	if (!string.IsNullOrEmpty(p得意先To))
				//	{
				//		int i支払先TO = AppCommon.IntParse(p得意先To);
				//		query = query.Where(c => c.得意先コード <= i支払先TO);
				//	}

				//	if (i得意先List.Length > 0)
				//	{
				//		var intCause = i得意先List;
				//		query = query.Union(from y01 in retList_Youbi
				//							from m01 in context.M01_TOK
				//							join t01 in context.T01_TRN.Where(t01 => t01.請求日付 >= p集計期間From && t01.請求日付 <= p集計期間To && (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 == 1)) on y01.日付 equals t01.請求日付 into s01Group
				//							join s01 in context.T01_TRN.Where(t01 => t01.請求日付 >= p集計期間From && t01.請求日付 <= p集計期間To && (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1)) on y01.日付 equals s01.請求日付 into s02Group
				//							where lst.Contains(m01.得意先KEY)
				//							where intCause.Contains(m01.得意先ID) && m01.削除日付 == null
				//							orderby m01.得意先ID
				//							select new TKS04010_Member
				//							{
				//								日付 = y01.日付,
				//								曜日 = y01.曜日 == "0" ? "日" : y01.曜日 == "1" ? "月" : y01.曜日 == "2" ? "火" : y01.曜日 == "3" ? "水" : y01.曜日 == "4" ? "木" : y01.曜日 == "5" ? "金" : y01.曜日 == "6" ? "土" : "",
				//								得意先コード = m01.得意先ID,
				//								得意先名 = m01.略称名,
				//								売上金額 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上金額),
				//								通行料 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.通行料),
				//								距離割増１ = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.請求割増１),
				//								距離割増２ = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.請求割増２),
				//								当月売上額 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２),
				//								傭車使用売上 = s02Group.Where(grp => grp.得意先KEY == m01.得意先KEY && grp.支払先KEY > 0).Sum(grp => grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２),
				//								傭車料 = s02Group.Where(grp => grp.得意先KEY == m01.得意先KEY && grp.支払先KEY > 0).Sum(grp => grp.支払金額 + grp.支払通行料 + grp.支払割増１ + grp.支払割増２),
				//								差益 = s02Group.Where(grp => grp.得意先KEY == m01.得意先KEY && grp.支払先KEY > 0).Sum(grp => (grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２) - (grp.支払金額 + grp.支払通行料 + grp.支払割増１ + grp.支払割増２)),
				//								差益率 = 0,
				//								締集計開始日 = p集計期間From,
				//								締集計終了日 = p集計期間To,
				//								件数 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Count(),
				//								未定 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上未定区分) >= 1 ? "未定" : "",
				//								対象年月 = p集計期間From,
				//								親子区分ID = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 1 ? "親" : "子",
				//								得意先指定コード = 得意先指定表示 == "" ? "無" : 得意先指定表示,
				//								得意先Sコード = p得意先From == "" ? "" : p得意先From + " ～ ",
				//								得意先Fコード = p得意先To == "" ? "" : p得意先To,
				//							});
				//	}
				//}

                //支払先指定の表示
                if (i得意先List.Length > 0)
                {
                    for (int it = 0; it < query.Count(); it++)
                    {
                        得意先指定表示 = 得意先指定表示 + i得意先List[it].ToString();

                        if (it < i得意先List.Length)
                        {
                            if (it == i得意先List.Length - 1)
                            {
                                break;
                            }
                            得意先指定表示 = 得意先指定表示 + ",";
                        }
                        if (i得意先List.Length == 1)
                        {
                            break;
                        }
                    }
                }

                List<TKS04010_Member> queryList = query.ToList();
                foreach (var Rows in queryList)
                {
                    if (Rows.傭車使用売上 != 0)
                    {
                        if (Rows.差益 != 0)
                        {
                            Rows.差益率 = (Rows.差益 / Rows.傭車使用売上) * 100;
                        }
                        else
                        {
                            Rows.差益率 = 0;
                        }
                    }
                    else
                    {
                        Rows.差益率 = 0;
                    }
                }


                //結果をリスト化

                return queryList;
            }
        }
        #endregion



		#region 得意先売上日計表プレビュー
		/// <summary>
		/// TKS04010 得意先売上日計表プレビュー
		/// </summary>
		/// <param name="p商品ID">得意先コード</param>
		/// <returns>S02</returns>
		public List<TKS04010_Member_CSV> SEARCH_TKS04010_Preview_CSV(string p得意先From, string p得意先To, int?[] i得意先List, string p作成締日, string p作成年, string p作成月, DateTime p集計期間From, DateTime p集計期間To)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				int tokfrom = AppCommon.IntParse(p得意先From) == 0 ? int.MinValue : AppCommon.IntParse(p得意先From);
				int tokto = AppCommon.IntParse(p得意先To) == 0 ? int.MaxValue : AppCommon.IntParse(p得意先To);
				if ((string.IsNullOrEmpty(p得意先From + p得意先To) && i得意先List.Length != 0))
				{
					tokfrom = int.MaxValue;
					tokto = int.MaxValue;
				}

				//支払先指定　表示用
				string 得意先指定表示 = string.Empty;
				//query格納LIST
				List<TKS04010_Member_CSV> retList = new List<TKS04010_Member_CSV>();
				//日付格納LIST
				List<TKS04010_Member_day> retList_day = new List<TKS04010_Member_day>();
				//日付、曜日格納LIST
				List<TKS04010_Member_Youbi> retList_Youbi = new List<TKS04010_Member_Youbi>();

				//変数
				string[] array = new string[1];      　          //日付元配列
				string[,] DayOfWeek;                 　          //日付用
				int j = 0;                           　          //配列要素数用
				int nYear = 0, nMonth = 0, nDay = 0, sYear = 0, sMonth = 0, sDay = 0; 　//日付分割用
				DateTime f集計期間From, t集計期間To; 　          //集計開始・終了期間
				int 日数 = 0;

				//日付、曜日計算処理
				for (int i = 0; p集計期間From <= p集計期間To; i++)
				{
					f集計期間From = p集計期間From;
					t集計期間To = p集計期間To;
					TimeSpan ts = t集計期間To - f集計期間From;   //日付の減算
					日数 = ts.Days + 1;                          //日付は1～なので、+1。

					Array.Resize(ref array, i + 1);   //配列要素の追加
					array[i] = f集計期間From.AddDays(i).ToShortDateString();

					nYear = Convert.ToInt32(array[i].Substring(0, 4));   //年分割
					nMonth = Convert.ToInt32(array[i].Substring(5, 2));  //月分割
					nDay = Convert.ToInt32(array[i].Substring(8, 2));    //日分割
					sYear = Convert.ToInt32(f集計期間From.ToString().Substring(0, 4));   //表示用
					sMonth = Convert.ToInt32(f集計期間From.ToString().Substring(5, 2));  //表示用
					sDay = Convert.ToInt32(f集計期間From.ToString().Substring(8, 2));　　//表示用

					DayOfWeek = new string[日数, 2];  //DayOfWeekの二次元配列要素数の設定

					if (nMonth == 1 || nMonth == 2)   //曜日判定配列へ各々挿入（ツェラーの公式）
					{
						nYear--;
						nMonth += 12;
					}
					//日付、曜日データを作成したリストに追加
					retList_Youbi.Add(new TKS04010_Member_Youbi()
					{
						日付 = Convert.ToDateTime(DayOfWeek[i, j] = array[i]),
						曜日 = DayOfWeek[i, j] = ((nYear + nYear / 4 - nYear / 100 + nYear / 400 + (13 * nMonth + 8) / 5 + nDay) % 7).ToString()
					});
					//条件の日付まで来たら終了
					if (array[i] == t集計期間To.ToShortDateString())
					{
						break;
					}
				}
				int 締日 = AppCommon.IntParse(p作成締日.ToString());
				context.Connection.Open();

				int[] lst;

				lst = (from m01 in context.M01_TOK
					   join t01 in context.T01_TRN.Where(t01 => (t01.請求日付 >= p集計期間From && t01.請求日付 <= p集計期間To) && ((t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 == 1))) on m01.得意先KEY equals t01.得意先KEY
					   where m01.削除日付 == null && m01.Ｔ締日 == 締日
					   select m01.得意先KEY).ToArray();

				//締日集計処理
				var query = (from y01 in retList_Youbi
							 from m01 in context.M01_TOK.Where(x => (x.得意先ID >= tokfrom && x.得意先ID <= tokto || (i得意先List.Contains(x.得意先ID))))
							 join t01 in context.T01_TRN.Where(t01 => t01.請求日付 >= p集計期間From && t01.請求日付 <= p集計期間To && (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 == 1)) on y01.日付 equals t01.請求日付 into s01Group
							 join s01 in context.T01_TRN.Where(t01 => t01.請求日付 >= p集計期間From && t01.請求日付 <= p集計期間To && (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1)) on y01.日付 equals s01.請求日付 into s02Group
							 where lst.Contains(m01.得意先KEY) && m01.削除日付 == null
							 orderby m01.得意先KEY

							 select new TKS04010_Member_CSV
							 {
								 日付 = y01.日付,
								 曜日 = y01.曜日 == "0" ? "日" : y01.曜日 == "1" ? "月" : y01.曜日 == "2" ? "火" : y01.曜日 == "3" ? "水" : y01.曜日 == "4" ? "木" : y01.曜日 == "5" ? "金" : y01.曜日 == "6" ? "土" : "",
								 得意先コード = m01.得意先ID,
								 得意先名 = m01.略称名,
								 売上金額 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上金額),
								 通行料 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.通行料),
								 距離割増１ = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.請求割増１),
								 距離割増２ = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.請求割増２),
								 当月売上額 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２),
								 傭車使用売上 = s02Group.Where(grp => grp.得意先KEY == m01.得意先KEY && grp.支払先KEY > 0).Sum(grp => grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２),
								 傭車料 = s02Group.Where(grp => grp.得意先KEY == m01.得意先KEY && grp.支払先KEY > 0).Sum(grp => grp.支払金額 + grp.支払通行料 + grp.支払割増１ + grp.支払割増２),
								 差益 = s02Group.Where(grp => grp.得意先KEY == m01.得意先KEY && grp.支払先KEY > 0).Sum(grp => (grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２) - (grp.支払金額 + grp.支払通行料 + grp.支払割増１ + grp.支払割増２)),
								 差益率 = 0,
								 //締集計開始日 = p集計期間From,
								 //締集計終了日 = p集計期間To,
								 件数 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Count(),
								 未定 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上未定区分) >= 1 ? "未定" : "",
							 }).AsQueryable();

				//if (!(string.IsNullOrEmpty(p得意先From + p得意先To) && i得意先List.Length == 0))
				//{
				//	//得意先が検索対象に入っていない時全データ取得
				//	if (string.IsNullOrEmpty(p得意先From + p得意先To))
				//	{
				//		query = query.Where(c => c.得意先コード >= int.MaxValue);
				//	}

				//	//得意先From処理　Min値
				//	if (!string.IsNullOrEmpty(p得意先From))
				//	{
				//		int i支払先FROM = AppCommon.IntParse(p得意先From);
				//		query = query.Where(c => c.得意先コード >= i支払先FROM);
				//	}

				//	//得意先To処理　Max値
				//	if (!string.IsNullOrEmpty(p得意先To))
				//	{
				//		int i支払先TO = AppCommon.IntParse(p得意先To);
				//		query = query.Where(c => c.得意先コード <= i支払先TO);
				//	}

				//	if (i得意先List.Length > 0)
				//	{
				//		var intCause = i得意先List;
				//		query = query.Union(from y01 in retList_Youbi
				//							from m01 in context.M01_TOK
				//							join t01 in context.T01_TRN.Where(t01 => t01.請求日付 >= p集計期間From && t01.請求日付 <= p集計期間To && (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 == 1)) on y01.日付 equals t01.請求日付 into s01Group
				//							join s01 in context.T01_TRN.Where(t01 => t01.請求日付 >= p集計期間From && t01.請求日付 <= p集計期間To && (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1)) on y01.日付 equals s01.請求日付 into s02Group
				//							where lst.Contains(m01.得意先KEY)
				//							where intCause.Contains(m01.得意先ID) && m01.削除日付 == null
				//							orderby m01.得意先ID
				//							select new TKS04010_Member_CSV
				//							{
				//								日付 = y01.日付,
				//								曜日 = y01.曜日 == "0" ? "日" : y01.曜日 == "1" ? "月" : y01.曜日 == "2" ? "火" : y01.曜日 == "3" ? "水" : y01.曜日 == "4" ? "木" : y01.曜日 == "5" ? "金" : y01.曜日 == "6" ? "土" : "",
				//								得意先コード = m01.得意先ID,
				//								得意先名 = m01.略称名,
				//								売上金額 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上金額),
				//								通行料 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.通行料),
				//								距離割増１ = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.請求割増１),
				//								距離割増２ = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.請求割増２),
				//								当月売上額 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２),
				//								傭車使用売上 = s02Group.Where(grp => grp.得意先KEY == m01.得意先KEY && grp.支払先KEY > 0).Sum(grp => grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２),
				//								傭車料 = s02Group.Where(grp => grp.得意先KEY == m01.得意先KEY && grp.支払先KEY > 0).Sum(grp => grp.支払金額 + grp.支払通行料 + grp.支払割増１ + grp.支払割増２),
				//								差益 = s02Group.Where(grp => grp.得意先KEY == m01.得意先KEY && grp.支払先KEY > 0).Sum(grp => (grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２) - (grp.支払金額 + grp.支払通行料 + grp.支払割増１ + grp.支払割増２)),
				//								差益率 = 0,
				//								//締集計開始日 = p集計期間From,
				//								//締集計終了日 = p集計期間To,
				//								件数 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Count(),
				//								未定 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上未定区分) >= 1 ? "未定" : "",
				//							});
				//	}
				//}

				//支払先指定の表示
				if (i得意先List.Length > 0)
				{
					for (int it = 0; it < query.Count(); it++)
					{
						得意先指定表示 = 得意先指定表示 + i得意先List[it].ToString();

						if (it < i得意先List.Length)
						{
							if (it == i得意先List.Length - 1)
							{
								break;
							}
							得意先指定表示 = 得意先指定表示 + ",";
						}
						if (i得意先List.Length == 1)
						{
							break;
						}
					}
				}

				List<TKS04010_Member_CSV> queryList = query.ToList();
				foreach (var Rows in queryList)
				{
					if (Rows.傭車使用売上 != 0)
					{
						if (Rows.差益 != 0)
						{
							Rows.差益率 = (Rows.差益 / Rows.傭車使用売上) * 100;
						}
						else
						{
							Rows.差益率 = 0;
						}
					}
					else
					{
						Rows.差益率 = 0;
					}
				}


				//結果をリスト化

				return queryList;
			}
		}
		#endregion

    }
}
