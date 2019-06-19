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
    /// <summary>
    /// SHR10010 印刷　メンバー
    /// </summary>
    [DataContract]
    public class SHR10010_Member
    {
        [DataMember]
        public DateTime? 日付 { get; set; }
        [DataMember]
        public string 曜日 { get; set; }
        [DataMember]
        public int 得意先ID { get; set; }
        [DataMember]
        public string 支払先名 { get; set; }
        [DataMember]
        public decimal 経費傭車料 { get; set; }
        [DataMember]
        public decimal 傭車立替 { get; set; }
        [DataMember]
        public int 当月傭車計 { get; set; }
        [DataMember]
        public decimal 売上金額 { get; set; }
        [DataMember]
        public int 通行料 { get; set; }
        [DataMember]
        public decimal 差益 { get; set; }
        [DataMember]
        public decimal 差益率 { get; set; }
        [DataMember]
        public int 件数 { get; set; }
        [DataMember]
        public string 未定 { get; set; }
        [DataMember]
        public string 親子区分 { get; set; }
        [DataMember]
        public DateTime 集計開始日 { get; set; }
        [DataMember]
        public DateTime 集計終了日 { get; set; }
        [DataMember]
        public string 支払先指定 { get; set; }
        [DataMember]
        public string 支払先ﾋﾟｯｸｱｯﾌﾟ { get; set; }
    }

    /// <summary>
    /// SHR10010  日付List　メンバー
    /// </summary>
    [DataContract]
    public class SHR10010_Member_Days
    {
        [DataMember]
        public DateTime? 日付 { get; set; }
    }

    /// <summary>
    /// SHR10010  曜日List　メンバー
    /// </summary>
    [DataContract]
    public class SHR10010_Member_WeekDay
    {
        [DataMember]
        public string 曜日 { get; set; }
        [DataMember]
        public DateTime? 日付 { get; set; }
    }



    public class SHR10010
    {
        /// <summary>
        /// SHR02010 印刷
        /// </summary>
        /// <param name="p商品ID">支払先コード</param>
        /// <returns>T01</returns>
        public List<SHR10010_Member> SHR10010_GetDataHinList(string i支払先From, string i支払先To, int?[] i支払先List, DateTime d集計期間From, DateTime d集計期間To)
        {

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				int tokfrom = AppCommon.IntParse(i支払先From) == 0 ? int.MinValue : AppCommon.IntParse(i支払先From);
				int tokto = AppCommon.IntParse(i支払先To) == 0 ? int.MaxValue : AppCommon.IntParse(i支払先To);
				if ((string.IsNullOrEmpty(i支払先From + i支払先To) && i支払先List.Length != 0))
				{
					tokfrom = int.MaxValue;
					tokto = int.MaxValue;
				}

                //支払先指定　表示用
                string 支払先指定ﾋﾟｯｸｱｯﾌﾟ = string.Empty;

                //query格納LIST
                List<SHR10010_Member> retList = new List<SHR10010_Member>();
                //日付格納LIST
                List<SHR10010_Member_Days> retList_Days = new List<SHR10010_Member_Days>();
                //日付、曜日格納LIST
                List<SHR10010_Member_WeekDay> retList_WeekDay = new List<SHR10010_Member_WeekDay>();

                //変数
                string[] array = new string[1];      　          //日付元配列
                string[,] DayOfWeek;                 　          //日付用
                int j = 0;                           　          //配列要素数用
                int nYear = 0, nMonth = 0, nDay = 0, sYear = 0, sMonth = 0, sDay = 0; 　//日付分割用
                DateTime f集計期間From, t集計期間To; 　          //集計開始・終了期間
                int 日数 = 0;

                //日付、曜日計算処理
                for (int i = 0; d集計期間From <= d集計期間To; i++)
                {
                    f集計期間From = d集計期間From;
                    t集計期間To = d集計期間To;
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
                    retList_WeekDay.Add(new SHR10010_Member_WeekDay()
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

                context.Connection.Open();

                int[] lst;

                //売上データの無い顧客データを内部結合で整理
                //日付期間内にある得意先の売上データを抽出
                lst = (from m01 in context.M01_TOK.Where(c => c.削除日付 == null)
                       join t01 in context.T01_TRN.Where(t01 => (t01.支払日付 >= d集計期間From && t01.支払日付 <= d集計期間To) && ((t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1))) on m01.得意先KEY equals t01.支払先KEY
                       select m01.得意先KEY).ToArray();

                //全件表示
                var query = (from w01 in retList_WeekDay
							 from m01 in context.M01_TOK.Where(c => (c.削除日付 == null) && (c.得意先ID >= tokfrom && c.得意先ID <= tokto || (i支払先List.Contains(c.得意先ID))))
                             join t01 in context.T01_TRN.Where(t01 => t01.支払日付 >= d集計期間From && t01.支払日付 <= d集計期間To && (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1)) on w01.日付 equals t01.支払日付 into s01Group
                             where lst.Contains(m01.得意先KEY)
                             orderby m01.得意先KEY

                             select new SHR10010_Member
                             {

                                 得意先ID = m01.得意先ID,
                                 日付 = w01.日付,
                                 曜日 = w01.曜日 == "0" ? "日" : w01.曜日 == "1" ? "月" : w01.曜日 == "2" ? "火" : w01.曜日 == "3" ? "水" : w01.曜日 == "4" ? "木" : w01.曜日 == "5" ? "金" : w01.曜日 == "6" ? "土" : "",
                                 経費傭車料 = s01Group.Where(gr => gr.支払先KEY == m01.得意先KEY).Sum(gr => gr.支払金額 + gr.支払割増１ + gr.支払割増２),
                                 傭車立替 = s01Group.Where(gr => gr.支払先KEY == m01.得意先KEY).Sum(gr => gr.支払通行料),
                                 当月傭車計 = s01Group.Where(gr => gr.支払先KEY == m01.得意先KEY && gr.支払先KEY != 0).Sum(gr => gr.支払金額 + gr.支払割増１ + gr.支払割増２ + gr.支払通行料),
                                 売上金額 = s01Group.Where(gr => gr.支払先KEY == m01.得意先KEY).Sum(gr => gr.売上金額 + gr.請求割増１ + gr.請求割増２),
                                 通行料 = s01Group.Where(gr => gr.支払先KEY == m01.得意先KEY).Sum(gr => gr.通行料),
                                 差益 = s01Group.Where(gr => gr.支払先KEY == m01.得意先KEY && gr.支払先KEY != 0).Sum(gr => (gr.売上金額 + gr.通行料 + gr.請求割増１ + gr.請求割増２) - (gr.支払金額 + gr.支払通行料 + gr.支払割増１ + gr.支払割増２)),
                                 差益率 = 0,
                                 件数 = s01Group.Where(gr => gr.支払先KEY == m01.得意先KEY).Count(),
                                 未定 = s01Group.Where(gr => gr.支払先KEY == m01.得意先KEY).Sum(gr => gr.売上未定区分) >= 1 ? "未定" : "",
                                 支払先名 = m01.略称名,
                                 親子区分 = m01.親子区分ID == 0 ? "一般" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                 集計開始日 = d集計期間From,
                                 集計終了日 = d集計期間To,
                                 支払先指定 = i支払先From + "～" + i支払先To,
                                 支払先ﾋﾟｯｸｱｯﾌﾟ = 支払先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "無" : 支払先指定ﾋﾟｯｸｱｯﾌﾟ,

                             }).AsQueryable();

				//if (!(string.IsNullOrEmpty(i支払先From + i支払先To) && i支払先List.Length == 0))
				//{
                   
				//	//得意先が検索対象に入っていない時全データ取得
				//	if (string.IsNullOrEmpty(i支払先From + i支払先To))
				//	{
				//		query = query.Where(c => c.得意先ID >= int.MaxValue);
				//	}
				//	if (!string.IsNullOrEmpty(i支払先From))
				//	{
				//		int 支払先From = AppCommon.IntParse(i支払先From);
				//		query = query.Where(c => c.得意先ID >= 支払先From);
				//	}
				//	if (!string.IsNullOrEmpty(i支払先To))
				//	{
				//		int 支払先To = AppCommon.IntParse(i支払先To);
				//		query = query.Where(c => c.得意先ID <= 支払先To);
				//	}


				//	//支払先ﾋﾟｯｸｱｯﾌﾟ処理
				//	if (i支払先List.Length > 0 )
				//	{
				//		var intCause = i支払先List;
				//		query = query.Union(from w01 in retList_WeekDay
				//							from m01 in context.M01_TOK.Where(c => c.削除日付 == null)
				//							join t01 in context.T01_TRN.Where(t01 => t01.支払日付 >= d集計期間From && t01.支払日付 <= d集計期間To && (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1)) on w01.日付 equals t01.支払日付 into s01Group
                                            
				//							where lst.Contains(m01.得意先KEY)
				//							where intCause.Contains(m01.得意先ID)
				//							orderby m01.得意先KEY
				//							select new SHR10010_Member
				//							{

				//								得意先ID = m01.得意先ID,
				//								日付 = w01.日付,
				//								曜日 = w01.曜日 == "0" ? "日" : w01.曜日 == "1" ? "月" : w01.曜日 == "2" ? "火" : w01.曜日 == "3" ? "水" : w01.曜日 == "4" ? "木" : w01.曜日 == "5" ? "金" : w01.曜日 == "6" ? "土" : "",
				//								経費傭車料 = s01Group.Where(gr => gr.支払先KEY == m01.得意先KEY).Sum(gr => gr.支払金額 + gr.支払割増１ + gr.支払割増２),
				//								傭車立替 = s01Group.Where(gr => gr.支払先KEY == m01.得意先KEY).Sum(gr => gr.支払通行料),
				//								当月傭車計 = s01Group.Where(gr => gr.支払先KEY == m01.得意先KEY && gr.支払先KEY != 0).Sum(gr => gr.支払金額  + gr.支払割増１ + gr.支払割増２ + gr.支払通行料),
				//								売上金額 = s01Group.Where(gr => gr.支払先KEY == m01.得意先KEY).Sum(gr => gr.売上金額 + gr.請求割増１ + gr.請求割増２),
				//								通行料 = s01Group.Where(gr => gr.支払先KEY == m01.得意先KEY).Sum(gr => gr.通行料),
				//								差益 = s01Group.Where(gr => gr.支払先KEY == m01.得意先KEY && gr.支払先KEY != 0).Sum(gr => (gr.売上金額 + gr.通行料 + gr.請求割増１ + gr.請求割増２) - (gr.支払金額 + gr.支払通行料 + gr.支払割増１ + gr.支払割増２)),
				//								差益率 = 0,
				//								件数 = s01Group.Where(gr => gr.支払先KEY == m01.得意先KEY).Count(),
				//								未定 = s01Group.Where(gr => gr.支払先KEY == m01.得意先KEY).Sum(gr => gr.売上未定区分) >= 1 ? "未定" : "",
				//								支払先名 = m01.略称名,
				//								親子区分 = m01.親子区分ID == 0 ? "一般" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
				//								集計開始日 = d集計期間From,
				//								集計終了日 = d集計期間To,
				//								支払先指定 = i支払先From + "～" + i支払先To,
				//								支払先ﾋﾟｯｸｱｯﾌﾟ = 支払先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "無" : 支払先指定ﾋﾟｯｸｱｯﾌﾟ,
				//							});

				//	}

				//}

                //支払先指定の表示
                if (i支払先List.Length > 0)
                {
                    for (int Count = 0; Count < query.Count(); Count++)
                    {
                        支払先指定ﾋﾟｯｸｱｯﾌﾟ = 支払先指定ﾋﾟｯｸｱｯﾌﾟ + i支払先List[Count].ToString();

                        if (Count < i支払先List.Length)
                        {

                            if (Count == i支払先List.Length - 1)
                            {
                                break;
                            }

                            支払先指定ﾋﾟｯｸｱｯﾌﾟ = 支払先指定ﾋﾟｯｸｱｯﾌﾟ + ",";

                        }

                        if (i支払先List.Length == 1)
                        {
                            break;
                        }

                    }
                }

                List<SHR10010_Member> queryList = query.ToList();
                foreach (var Rows in queryList)
                {
                    if (Rows.売上金額 != 0)
                    {
                        if (Rows.差益 != 0)
                        {
                            Rows.差益率 = (Rows.差益 / (Rows.売上金額 + Rows.通行料)) * 100;
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
                retList = queryList;

                return retList;

            }
        }
    }
}



