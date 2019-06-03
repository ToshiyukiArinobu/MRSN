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
            [DataMember]
            public DateTime? 日付 { get; set; }
        }

        /// <summary>
        /// TKS06010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class TKS04010_Member_Youbi
        {
            [DataMember]
            public string 曜日 { get; set; }
            [DataMember]
            public DateTime? 日付 { get; set; }
        }


        /// <summary>
        /// SRY01010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class SRY01010_Member
        {
            [DataMember]
            public DateTime? 日付 { get; set; }
            [DataMember]
            public string 曜日 { get; set; }
            [DataMember]
            public int?[] 得意先指定 { get; set; }
            [DataMember]
            public int? 得意先コード { get; set; }
            [DataMember]
            public string 得意先名 { get; set; }
            [DataMember]
            public decimal? 売上金額 { get; set; }
            [DataMember]
            public decimal? 通行料 { get; set; }
            [DataMember]
            public decimal? 距離割増１ { get; set; }
            [DataMember]
            public decimal? 距離割増２ { get; set; } 
            [DataMember]
            public decimal? 時間割増 { get; set; }
            [DataMember]
            public decimal? 当月売上額 { get; set; }
            [DataMember]
            public decimal? 傭車使用売上 { get; set; }
            [DataMember]
            public decimal? 傭車料 { get; set; }
            [DataMember]
            public decimal? 差益 { get; set; }
            [DataMember]
            public decimal? 差益率 { get; set; }
            [DataMember]
            public int? 件数 { get; set; }
            [DataMember]
            public string 未定 { get; set; }//ここまで一緒
            [DataMember]
            public string 対象年月 { get; set; }
            [DataMember]
            public string 親子区分ID { get; set; }
            [DataMember]
            public string 得意先指定コード { get; set; }
            [DataMember]
            public string 得意先Sコード { get; set; }
            [DataMember]
            public string 得意先Fコード { get; set; }
            [DataMember]
            public DateTime? 締集計開始日 { get; set; }
            [DataMember]
            public DateTime? 締集計終了日 { get; set; }
            [DataMember]
            public string rpt集計開始日 { get; set; }
            [DataMember]
            public string rpt集計終了日 { get; set; }

        }


        /// <summary>
        /// SRY01010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class SRY01010_Member_CSV
        {
            [DataMember]
            public DateTime? 日付 { get; set; }
            [DataMember]
            public string 曜日 { get; set; }
            [DataMember]
            public int? 得意先コード { get; set; }
            [DataMember]
            public string 得意先名 { get; set; }
            [DataMember]
            public decimal? 売上金額 { get; set; }
            [DataMember]
            public decimal? 通行料 { get; set; }
            [DataMember]
            public decimal? 距離割増１ { get; set; }
            [DataMember]
            public decimal? 距離割増２ { get; set; }
            [DataMember]
            public decimal? 時間割増 { get; set; }
            [DataMember]
            public decimal? 当月売上額 { get; set; }
            [DataMember]
            public decimal? 傭車使用売上 { get; set; }
            [DataMember]
            public decimal? 傭車料 { get; set; }
            [DataMember]
            public decimal? 差益 { get; set; }
            [DataMember]
            public decimal? 差益率 { get; set; }
            [DataMember]
            public int? 件数 { get; set; }
            [DataMember]
            public string 未定 { get; set; }
            [DataMember]
            public string 対象年月 { get; set; }
            [DataMember]
            public string 親子区分ID { get; set; }
            [DataMember]
            public int 締日未定件数 { get; set; }
            [DataMember]
            public DateTime? 締集計開始日 { get; set; }
            [DataMember]
            public DateTime? 締集計終了日 { get; set; }
        }




        #region 得意先売上日計表プレビュー
        /// <summary>
        /// TKS04010 得意先売上日計表プレビュー
        /// </summary>
        /// <param name="p商品ID">得意先コード</param>
        /// <returns>S02</returns>
        public List<SRY01010_Member> SEARCH_TKS04010_Preview(string p得意先From, string p得意先To, int?[] i得意先List, int? p作成締日, string p作成年, string p作成月, DateTime p集計期間From, DateTime p集計期間To)
        {
            using (TRAC3Entities context = new TRAC3Entities())
            {
                //支払先指定　表示用
                string 支払先指定表示 = string.Empty;

                //query格納LIST
                List<SRY01010_Member> retList = new List<SRY01010_Member>();
                //日付格納LIST
                List<TKS04010_Member_day> retList_day = new List<TKS04010_Member_day>();
                //日付、曜日格納LIST
                List<TKS04010_Member_Youbi> retList_Youbi = new List<TKS04010_Member_Youbi>();

                //変数
                string[] array = new string[1];      　          //日付元配列
                string[,] DayOfWeek;                 　          //日付用
                int j = 0;                           　          //配列要素数用
                int nYear = 0, nMonth = 0, nDay = 0, sYear = 0, sMonth = 0, sDay = 0; 　//日付分割用
                DateTime f集計期間From , t集計期間To; 　          //集計開始・終了期間
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
                    retList_Youbi.Add(new TKS04010_Member_Youbi() { 日付 = Convert.ToDateTime(DayOfWeek[i, j] = array[i]),
                                                                    曜日 = DayOfWeek[i, j] = ((nYear + nYear / 4 - nYear / 100 + nYear / 400 + (13 * nMonth + 8) / 5 + nDay) % 7).ToString() });
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
                lst = (from m01 in context.M01_TOK
                       join t01 in context.T01_TRN.Where(t01 => t01.請求日付 >= p集計期間From && t01.請求日付 <= p集計期間To) on m01.得意先KEY equals t01.得意先KEY
                       select m01.得意先KEY).ToArray();

                //締日集計処理
                var query = (from y01 in retList_Youbi
                             from m01 in context.M01_TOK
                             join t01 in context.T01_TRN.Where(t01 => t01.請求日付 >= p集計期間From && t01.請求日付 <= p集計期間To) on y01.日付 equals t01.請求日付 into s01Group
                             where lst.Contains(m01.得意先KEY)
                             orderby m01.得意先KEY

                             select new SRY01010_Member
                             {
                                 日付 = y01.日付,
                                 曜日 = y01.曜日 == "0" ? "日" : y01.曜日 == "1" ? "月" : y01.曜日 == "2" ? "火" : y01.曜日 == "3" ? "水" : y01.曜日 == "4" ? "木" : y01.曜日 == "5" ? "金" : y01.曜日 == "6" ? "土" : "",
                                 得意先コード = m01.得意先ID,
                                 得意先名 = m01.得意先名１,
                                 売上金額 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上金額),
                                 通行料 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.通行料),
                                 距離割増１ = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.請求割増１),
                                 距離割増２ = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.請求割増２),
                                 当月売上額 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２),
                                 傭車使用売上 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY && grp.支払先KEY != 0).Sum(grp => grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２),
                                 傭車料 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY && grp.支払先KEY != 0).Sum(grp => grp.支払金額 + grp.支払通行料 + grp.支払割増１ + grp.支払割増２),
                                 差益 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY && grp.支払先KEY != 0).Sum(grp => (grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２) - (grp.支払金額 + grp.支払通行料 + grp.支払割増１ + grp.支払割増２)),
                                 差益率 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY &&  grp.支払先KEY != 0).Sum(grp => (((grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２) - (grp.支払金額 + grp.支払通行料 + grp.支払割増１ + grp.支払割増２)) * 100 / (grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２))),
                                 締集計開始日 = p集計期間From,
                                 締集計終了日 = p集計期間To,
                                 rpt集計開始日 = sYear + " 年 " + sMonth + " 月 " + sDay + " 日 ～ ",
                                 rpt集計終了日 = nYear + " 年 " + nMonth + " 月 " + nDay + " 日 迄 ",
                                 件数 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Count(),
                                 未定 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上未定区分) >= 1 ? "未定" : "",
                                 対象年月 = p作成年 + "　年　" + p作成月 + "　月度",
                                 親子区分ID = m01.親子区分ID == 1 ? "親子" : m01.親子区分ID == 2 ? "子" : "",
                                 得意先指定コード = 支払先指定表示 == "" ? "無" : 支払先指定表示,
                                 得意先Sコード = p得意先From == "" ? "" : p得意先From + " ～ ",
                                 得意先Fコード = p得意先To == "" ? "" : p得意先To,

                             }).AsQueryable();

                

                if (!(string.IsNullOrEmpty(p得意先From + p得意先To) && i得意先List.Length == 0))
                {

                    //得意先が検索対象に入っていない時全データ取得
                    if (string.IsNullOrEmpty(p得意先From + p得意先To))
                    {
                        query = query.Where(c => c.得意先コード >= int.MaxValue);
                    }

                    //得意先From処理　Min値
                    if (!string.IsNullOrEmpty(p得意先From))
                    {
                        int i支払先FROM = int.Parse(p得意先From);
                        query = query.Where(c => c.得意先コード >= i支払先FROM);
                    }

                    //得意先To処理　Max値
                    if (!string.IsNullOrEmpty(p得意先To))
                    {
                        int i支払先TO = int.Parse(p得意先To);
                        query = query.Where(c => c.得意先コード <= i支払先TO);
                    }


                    if (string.IsNullOrEmpty(p得意先From + p得意先To) && i得意先List.Length > 0)
                    {
                        var intCause = i得意先List;
                        query = query.Union(from y01 in retList_Youbi
                                            from m01 in context.M01_TOK
                                            join t01 in context.T01_TRN.Where(t01 => t01.請求日付 >= p集計期間From && t01.請求日付 <= p集計期間To) on y01.日付 equals t01.請求日付 into s01Group
                                            orderby m01.得意先KEY
                                            where lst.Contains(m01.得意先KEY)
                                            where intCause.Contains(m01.得意先ID)
                                            select new SRY01010_Member
                                            {
                                                日付 = y01.日付,
                                                曜日 = y01.曜日 == "0" ? "日" : y01.曜日 == "1" ? "月" : y01.曜日 == "2" ? "火" : y01.曜日 == "3" ? "水" : y01.曜日 == "4" ? "木" : y01.曜日 == "5" ? "金" : y01.曜日 == "6" ? "土" : "",
                                                得意先コード = m01.得意先ID,
                                                得意先名 = m01.得意先名１,
                                                売上金額 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上金額),
                                                通行料 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.通行料),
                                                距離割増１ = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.請求割増１),
                                                距離割増２ = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.請求割増２),
                                                当月売上額 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２),
                                                傭車使用売上 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.支払金額 + grp.支払通行料),
                                                傭車料 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.支払割増１ + grp.支払割増２),
                                                差益 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => (grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２) - (grp.支払金額 + grp.支払通行料 + grp.支払割増１ + grp.支払割増２)),
                                                差益率 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => (grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２) - (grp.支払金額 + grp.支払通行料 + grp.支払割増１ + grp.支払割増２) / (grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２)) * 100,
                                                締集計開始日 = p集計期間From,
                                                締集計終了日 = p集計期間To,
                                                rpt集計開始日 = sYear + " 年 " + sMonth + " 月 " + sDay + " 日 ～ ",
                                                rpt集計終了日 = nYear + " 年 " + nMonth + " 月 " + nDay + " 日 迄 ",
                                                件数 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Count(),
                                                未定 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上未定区分) >= 1 ? "未定" : "",
                                                対象年月 = p作成年 + "　年　" + p作成月 + "　月度",
                                                得意先指定コード = 支払先指定表示 == "" ? "無" : 支払先指定表示,
                                                得意先Sコード = p得意先From == "" ? "" : p得意先From + " ～ ",
                                                得意先Fコード = p得意先To == "" ? "" : p得意先To,

                                            });
                    }
                }


                //支払先指定の表示
                if (i得意先List.Length > 0)
                {
                    for (int it = 0; it < query.Count(); it++)
                    {
                        支払先指定表示 = 支払先指定表示 + i得意先List[it].ToString();

                        if (it < i得意先List.Length)
                        {

                            if (it == i得意先List.Length - 1)
                            {
                                break;
                            }

                            支払先指定表示 = 支払先指定表示 + ",";

                        }

                        if (i得意先List.Length == 1)
                        {
                            break;
                        }

                    }
                }

                //結果をリスト化
                retList = query.ToList();

                return retList;

            }

        }
        #endregion




        #region 得意先売上日計表CSV
        /// <summary>
        /// TKS04010 得意先売上日計表CSV
        /// </summary>
        /// <param name="p商品ID">得意先コード</param>
        /// <returns>S12</returns>
        public List<SRY01010_Member_CSV> SEARCH_TKS04010_CSV(string p得意先From, string p得意先To, int?[] i得意先List, int? p作成締日, string p作成年, string p作成月, DateTime p集計期間From, DateTime p集計期間To)
        {
            using (TRAC3Entities context = new TRAC3Entities())
            {
                //日付格納LIST宣言
                List<SRY01010_Member_CSV> retList = new List<SRY01010_Member_CSV>();
                List<TKS04010_Member_day> retList_day = new List<TKS04010_Member_day>();
                List<TKS04010_Member_Youbi> retList_Youbi = new List<TKS04010_Member_Youbi>();

                //支払先指定　表示用
                string 支払先指定表示 = string.Empty;
                //変数
                string[] array = new string[1];
                string[,] DayOfWeek;
                var s = DateTime.Now;
                int j = 0;

                //日付計算処理
                for (int i = 0; p集計期間From < p集計期間To; i++)
                {
                    DateTime fff = p集計期間From;
                    DateTime ttt = p集計期間To;
                    TimeSpan ts = ttt - fff;
                    int 日数 = ts.Days + 1;
                    //配列要素の追加
                    Array.Resize(ref array, i + 1);
                    array[i] = fff.AddDays(i).ToShortDateString();
                    //年月日取得と変換
                    int nYear = Convert.ToInt32(array[i].Substring(0, 4));
                    int nMonth = Convert.ToInt32(array[i].Substring(5, 2));
                    int nDay = Convert.ToInt32(array[i].Substring(8, 2));

                    DayOfWeek = new string[日数, 2];
                    //曜日判定
                    if (nMonth == 1 || nMonth == 2)
                    {
                        nYear--;
                        nMonth += 12;
                    }
                    //日付、曜日データ追加
                    retList_Youbi.Add(new TKS04010_Member_Youbi()
                    {
                        日付 = Convert.ToDateTime(DayOfWeek[i, j] = array[i]),
                        曜日 = DayOfWeek[i, j] = ((nYear + nYear / 4 - nYear / 100 + nYear / 400 + (13 * nMonth + 8) / 5 + nDay) % 7).ToString()
                    });
                    //条件の日付まで来たら終了
                    if (array[i] == ttt.ToShortDateString())
                    {
                        break;
                    }
                }

                context.Connection.Open();

                int[] lst;

                //内部結合
                lst = (from m01 in context.M01_TOK
                       join t01 in context.T01_TRN.Where(t01 => t01.請求日付 >= p集計期間From && t01.請求日付 <= p集計期間To) on m01.得意先KEY equals t01.得意先KEY
                       select m01.得意先KEY).ToArray();

                //月次集計処理
                var query = (from y01 in retList_Youbi
                             from m01 in context.M01_TOK
                             join t01 in context.T01_TRN.Where(t01 => t01.請求日付 >= p集計期間From && t01.請求日付 <= p集計期間To) on y01.日付 equals t01.請求日付 into s01Group
                             where lst.Contains(m01.得意先KEY)
                             orderby m01.得意先KEY
                             select new SRY01010_Member_CSV
                             {
                                 日付 = y01.日付,
                                 曜日 = y01.曜日 == "0" ? "日" : y01.曜日 == "1" ? "月" : y01.曜日 == "2" ? "火" : y01.曜日 == "3" ? "水" : y01.曜日 == "4" ? "木" : y01.曜日 == "5" ? "金" : y01.曜日 == "6" ? "土" : "",
                                 得意先コード = m01.得意先ID,
                                 得意先名 = m01.得意先名１,
                                 売上金額 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上金額),
                                 通行料 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.通行料),
                                 距離割増１ = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.請求割増１),
                                 距離割増２ = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.請求割増２),
                                 当月売上額 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２),
                                 傭車使用売上 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.支払金額 + grp.支払通行料),
                                 傭車料 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.支払割増１ + grp.支払割増２),
                                 差益 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => (grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２) - (grp.支払金額 + grp.支払通行料 + grp.支払割増１ + grp.支払割増２)),
                                 差益率 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => (grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２) - (grp.支払金額 + grp.支払通行料 + grp.支払割増１ + grp.支払割増２) / (grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２)) * 100,
                                 締集計開始日 = p集計期間From,
                                 締集計終了日 = p集計期間To,
                                 件数 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Count(),
                                 未定 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上未定区分) >= 1 ? "未定" : "",
                                 対象年月 = p作成年 + "年" + p作成月 + "月度",
                                 //得意先指定コード = 支払先指定表示 == "" ? "無" : 支払先指定表示,
                                 //得意先Sコード = p得意先From == "" ? "" : p得意先From + " ～ ",
                                 //得意先Fコード = p得意先To == "" ? "" : p得意先To,

                             }).AsQueryable();


                if (!(string.IsNullOrEmpty(p得意先From + p得意先To) && i得意先List.Length == 0))
                {

                    //得意先が検索対象に入っていない時全データ取得
                    if (string.IsNullOrEmpty(p得意先From + p得意先To))
                    {
                        query = query.Where(c => c.得意先コード >= int.MaxValue);
                    }

                    //得意先From処理　Min値
                    if (!string.IsNullOrEmpty(p得意先From))
                    {
                        int i支払先FROM = int.Parse(p得意先From);
                        query = query.Where(c => c.得意先コード >= i支払先FROM);
                    }

                    //得意先To処理　Max値
                    if (!string.IsNullOrEmpty(p得意先To))
                    {
                        int i支払先TO = int.Parse(p得意先To);
                        query = query.Where(c => c.得意先コード <= i支払先TO);
                    }


                    if (i得意先List.Length > 0)
                    {
                        var intCause = i得意先List;
                        query = query.Union(from y01 in retList_Youbi
                                            from m01 in context.M01_TOK
                                            join t01 in context.T01_TRN.Where(t01 => t01.請求日付 >= p集計期間From && t01.請求日付 <= p集計期間To) on y01.日付 equals t01.請求日付 into s01Group
                                            orderby m01.得意先KEY
                                            where lst.Contains(m01.得意先KEY)
                                            where intCause.Contains(m01.得意先KEY)
                                            select new SRY01010_Member_CSV
                                            {
                                                日付 = y01.日付,
                                                曜日 = y01.曜日 == "0" ? "日" : y01.曜日 == "1" ? "月" : y01.曜日 == "2" ? "火" : y01.曜日 == "3" ? "水" : y01.曜日 == "4" ? "木" : y01.曜日 == "5" ? "金" : y01.曜日 == "6" ? "土" : "",
                                                得意先コード = m01.得意先ID,
                                                得意先名 = m01.得意先名１,
                                                売上金額 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上金額),
                                                通行料 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.通行料),
                                                距離割増１ = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.請求割増１),
                                                距離割増２ = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.請求割増２),
                                                当月売上額 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２),
                                                傭車使用売上 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.支払金額 + grp.支払通行料),
                                                傭車料 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.支払割増１ + grp.支払割増２),
                                                差益 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => (grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２) - (grp.支払金額 + grp.支払通行料 + grp.支払割増１ + grp.支払割増２)),
                                                差益率 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => (grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２) - (grp.支払金額 + grp.支払通行料 + grp.支払割増１ + grp.支払割増２) / (grp.売上金額 + grp.通行料 + grp.請求割増１ + grp.請求割増２)) * 100,
                                                締集計開始日 = p集計期間From,
                                                締集計終了日 = p集計期間To,
                                                件数 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Count(),
                                                未定 = s01Group.Where(grp => grp.得意先KEY == m01.得意先KEY).Sum(grp => grp.売上未定区分) >= 1 ? "未定" : "",
                                                対象年月 = p作成年 + "年" + p作成月 + "月度",
                                                //得意先指定コード = 支払先指定表示 == "" ? "無" : 支払先指定表示,
                                                //得意先Sコード = p得意先From == "" ? "" : p得意先From + " ～ ",
                                                //得意先Fコード = p得意先To == "" ? "" : p得意先To,
                                            });

                    }
                }


                //支払先指定の表示
                if (i得意先List.Length > 0)
                {
                    for (int i = 0; i < query.Count(); i++)
                    {
                        支払先指定表示 = 支払先指定表示 + i得意先List[i].ToString();

                        if (i < i得意先List.Length)
                        {

                            if (i == i得意先List.Length - 1)
                            {
                                break;
                            }

                            支払先指定表示 = 支払先指定表示 + ",";

                        }

                        if (i得意先List.Length == 1)
                        {
                            break;
                        }

                    }
                }

                //結果をリスト化
                retList = query.ToList();
                return retList;

            }

        }
        #endregion

    }
}
