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
    public class TKS11010
    {
        #region メンバー定義
        /// <summary>
        /// TKS11010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class TKS11010_Member2
        {
            public int 得意先ID { get; set; }
            public string 得意先名 { get; set; }
            public int 得意先KEY { get; set; }
            public int? 親子区分ID { get; set; }
            public int? サイト { get; set; }
            public int 年月 { get; set; }
            public int? 締日 { get; set; }
            public int? 集金日 { get; set; }
			public DateTime 削除日付 { get; set; }
			public int 作成年月 { get; set; }
        }

        /// <summary>
        /// TKS11010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class TKS11010_Member
        {
            public int? 得意先コード { get; set; }
            public string 得意先名 { get; set; }
            public string 親子区分ID { get; set; }
            public int? 集金日 { get; set; }
            public int? サイト { get; set; }
            public string 表示サイト { get; set; }
            public decimal 売上金額 { get; set; }
            public decimal 内課税額 { get; set; }
            public decimal 消費税 { get; set; }
            public decimal 通行料 { get; set; }
            public decimal 回収予定額 { get; set; }
            public DateTime? 請求年月 { get; set; }
            public int? 締日 { get; set; }
            public decimal 当月入金額 { get; set; }
            public decimal 入金調整額 { get; set; }
            public decimal 入金合計額 { get; set; }
            public DateTime? 対象年月 { get; set; }
            public int 集計年月 { get; set; }
            public string 全集金日 { get; set; }
            public string 得意先指定コード { get; set; }
            public string 得意先Sコード { get; set; }
            public string 得意先Fコード { get; set; }
            public DateTime? 締集計開始日 { get; set; }
            public DateTime? 締集計終了日 { get; set; }
            public string 表示区分 { get; set; }
        }

        /// <summary>
        /// TKS11010  CSV　メンバー
        /// </summary>
        [DataContract]
        public class TKS11010_Member_CSV
        {
            public int? 得意先コード { get; set; }
            public string 得意先名 { get; set; }
            public string 親子区分ID { get; set; }
            public int? 集金日 { get; set; }
            public int? サイト { get; set; }
            public decimal 売上金額 { get; set; }
            public decimal 内課税額 { get; set; }
            public decimal 消費税 { get; set; }
            public decimal 通行料 { get; set; }
            public decimal 回収予定額 { get; set; }
            public DateTime? 請求年月 { get; set; }
            public int? 締日 { get; set; }
            public decimal 当月入金額 { get; set; }
            public decimal 入金調整額 { get; set; }
            public decimal 入金合計額 { get; set; }
            public DateTime? 対象年月 { get; set; }
            public int 集計年月 { get; set; }
            public string 全集金日 { get; set; }
            public string 表示区分 { get; set; }
        }
        #endregion

        #region 帳票印刷
        /// <summary>
        /// TKS11010 帳票印刷
        /// </summary>
        /// <param name="p商品ID">得意先コード</param>
        /// <returns>TKS11010</returns>
        public List<TKS11010_Member> SEARCH_TKS11010_GetDataList(string p得意先From, string p得意先To, int?[] i得意先List, string p作成集金日, bool b全集金日, string p作成年, string p作成月, int 作成区分_CValue, int? 集計, DateTime? d集計期間From, DateTime? d集計期間To)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<TKS11010_Member> retList = new List<TKS11010_Member>();
                List<TKS11010_Member> queryList = new List<TKS11010_Member>();
                context.Connection.Open();

                //支払先指定　表示用
                string 得意先指定表示 = string.Empty;
                //サイト計算
                var query2 = (from m01 in context.M01_TOK.Where(m01 => m01.削除日付 == null && m01.親子区分ID != 3)
                              select new TKS11010_Member2
                              {
                                  得意先ID = m01.得意先ID,
                                  得意先名 = m01.略称名,
                                  得意先KEY = m01.得意先KEY,
                                  サイト = m01.Ｔサイト日,
                                  締日 = m01.Ｔ締日,
                                  集金日 = m01.Ｔ集金日,
								  親子区分ID = m01.親子区分ID,
								  作成年月 = 集計 ?? 0,
                              }).ToList();

                DateTime d集計年月 = DateTime.Now;
                for (int i = 0; i < query2.Count; i++)
                {
                    if (DateTime.TryParse(((p作成年).ToString() + "/" + (p作成月).ToString() + "/" + "01"), out d集計年月))
                    {
                        query2[i].年月 = ((d集計年月.AddMonths(Convert.ToInt32(-query2[i].サイト)).Year) * 100) + (d集計年月.AddMonths(Convert.ToInt32(-query2[i].サイト)).Month);
                    };
                };

				var query = (from q01 in query2
							 join v01 in context.S01_TOKS on new { q01.得意先KEY, nen = q01.年月 } equals new { v01.得意先KEY, nen = v01.集計年月 } into v01Group
							 join nyukin in context.S01_TOKS on new { q01.得意先KEY, nen = q01.作成年月 } equals new { nyukin.得意先KEY, nen = nyukin.集計年月 } into nyukinGroup
                             select new TKS11010_Member
                             {
                                 得意先コード = q01.得意先ID,
                                 得意先名 = q01.得意先名,
                                 親子区分ID = q01.親子区分ID == 0 ? "" : q01.親子区分ID == 1 ? "親" : q01.親子区分ID == 2 ? "親" : "子",
                                 集金日 = q01.集金日 == null ? 0 : q01.集金日,
                                 表示サイト = q01.サイト == 0 ? "当月" : q01.サイト == 1 ? "翌月" : q01.サイト == 2 ? "翌々月" : q01.サイト == 3 ? "３ヶ月" : "",
								 サイト = q01.サイト == null ? 0 : q01.サイト,
								 売上金額 = v01Group.Sum(v01g => v01g.締日売上金額),
								 内課税額 = v01Group.Sum(v01g => v01g.締日課税売上),
								 消費税 = v01Group.Sum(v01g => v01g.締日消費税),
								 通行料 = v01Group.Sum(v01g => v01g.締日通行料),
								 回収予定額 = v01Group.Sum(v01g => v01g.締日売上金額 + v01g.締日通行料 + v01g.締日消費税),
                                 請求年月 = null,
								 締日 = q01.締日 == null ? 0 : q01.締日,
								 当月入金額 = nyukinGroup.Sum(c => c.締日入金現金 + c.締日入金手形),
								 入金調整額 = nyukinGroup.Sum(c => c.締日入金その他),
								 入金合計額 = nyukinGroup.Sum(c => c.締日入金現金 + c.締日入金手形 + c.締日入金その他),
                                 集計年月 = q01.年月,
                                 全集金日 = p作成集金日 == string.Empty ? "全集金日" : p作成集金日,
                                 対象年月 = d集計期間From,
                                 得意先指定コード = 得意先指定表示 == "" ? "" : 得意先指定表示,
                                 得意先Sコード = p得意先From == "" ? "" : p得意先From + " ～ ",
                                 得意先Fコード = p得意先To == "" ? "" : p得意先To,
                                 表示区分 = 作成区分_CValue == 0 ? "（回収予定あり得意先のみ）" : 作成区分_CValue == 1 ? "（回収予定無し得意先含む）" : "",
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
                        int i支払先FROM = AppCommon.IntParse(p得意先From);
                        query = query.Where(c => c.得意先コード >= i支払先FROM);
                    }

                    //得意先To処理　Max値
                    if (!string.IsNullOrEmpty(p得意先To))
                    {
                        int i支払先TO = AppCommon.IntParse(p得意先To);
                        query = query.Where(c => c.得意先コード <= i支払先TO);
                    }

                    //全締日集計処理
                    if (b全集金日 == true)
                    {
                        query = query.Where(c => c.集金日 >= 1 && c.集金日 <= 31);
                    }

                    //締日処理　
                    if (!string.IsNullOrEmpty(p作成集金日))
                    {
                        int? p変換作成締日 = AppCommon.IntParse(p作成集金日);
                        query = query.Where(c => c.集金日 == p変換作成締日);
                    }

                    if (i得意先List.Length > 0)
                    {
                        var intCause = i得意先List;
						query = query.Union(from q01 in query2
											join v01 in context.S01_TOKS on new { q01.得意先KEY, nen = q01.年月 } equals new { v01.得意先KEY, nen = v01.集計年月 } into v01Group
											join nyukin in context.S01_TOKS on new { q01.得意先KEY, nen = q01.作成年月 } equals new { nyukin.得意先KEY, nen = nyukin.集計年月 } into nyukinGroup
                                            where intCause.Contains(q01.得意先ID)
                                            select new TKS11010_Member
                                            {
                                                得意先コード = q01.得意先ID,
                                                得意先名 = q01.得意先名,
                                                親子区分ID = q01.親子区分ID == 0 ? "" : q01.親子区分ID == 1 ? "親" : q01.親子区分ID == 2 ? "親" : "子",
                                                集金日 = q01.集金日 == null ? 0 : q01.集金日,
                                                表示サイト = q01.サイト == 0 ? "当月" : q01.サイト == 1 ? "翌月" : q01.サイト == 2 ? "翌々月" : q01.サイト == 3 ? "３ヶ月" : "",
												サイト = q01.サイト == null ? 0 : q01.サイト,
												売上金額 = v01Group.Sum(v01g => v01g.締日売上金額),
												内課税額 = v01Group.Sum(v01g => v01g.締日課税売上),
												消費税 = v01Group.Sum(v01g => v01g.締日消費税),
												通行料 = v01Group.Sum(v01g => v01g.締日通行料),
												回収予定額 = v01Group.Sum(v01g => v01g.締日売上金額 + v01g.締日通行料 + v01g.締日消費税),
                                                請求年月 = null,
												締日 = q01.締日 == null ? 0 : q01.締日,
												当月入金額 = nyukinGroup.Sum(c => c.締日入金現金 + c.締日入金手形),
												入金調整額 = nyukinGroup.Sum(c => c.締日入金その他),
												入金合計額 = nyukinGroup.Sum(c => c.締日入金現金 + c.締日入金手形 + c.締日入金その他),
                                                集計年月 = q01.年月,
                                                全集金日 = p作成集金日 == string.Empty ? "全集金日" : p作成集金日,
                                                対象年月 = d集計期間From,
                                                得意先指定コード = 得意先指定表示 == "" ? "" : 得意先指定表示,
                                                得意先Sコード = p得意先From == "" ? "" : p得意先From + " ～ ",
                                                得意先Fコード = p得意先To == "" ? "" : p得意先To,
                                                表示区分 = 作成区分_CValue == 0 ? "（回収予定あり得意先のみ）" : 作成区分_CValue == 1 ? "（回収予定無し得意先含む）" : "",
                                            }).AsQueryable();

                        //全締日集計処理
                        if (b全集金日 == true)
                        {
                            query = query.Where(c => c.集金日 >= 1 && c.集金日 <= 31);

                        }

                        //締日処理　
                        if (!string.IsNullOrEmpty(p作成集金日))
                        {
                            int? p変換作成締日 = AppCommon.IntParse(p作成集金日);
                            query = query.Where(c => c.集金日 == p変換作成締日);
                        }
                    }
                }
                else
                {
                    //全締日集計処理
                    if (b全集金日 == true)
                    {
                        query = query.Where(c => c.集金日 >= 1 && c.集金日 <= 31);
                    }

                    //締日処理
                    if (!string.IsNullOrEmpty(p作成集金日))
                    {
                        int? p変換作成締日 = AppCommon.IntParse(p作成集金日);
                        query = query.Where(c => c.集金日 == p変換作成締日);
                    }
                }

                //内訳別表示処理
                //売上あり：0
                //売上なし：1
                switch (作成区分_CValue)
                {
                    //支払取引全体
                    case 0:
                        query = query.Where(c => c.回収予定額 != 0);
                        break;

                    //支払先
                    case 1:
                        //query = query.Where(c => c.回収予定額 >= 0);
                        break;

                    default:
                        break;
                }

                //支払先指定の表示
                if (i得意先List.Length > 0)
                {
                    for (int i = 0; i < query.Count(); i++)
                    {
                        得意先指定表示 = 得意先指定表示 + i得意先List[i].ToString();

                        if (i < i得意先List.Length)
                        {
                            if (i == i得意先List.Length - 1)
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

                List<TKS11010_Member> queryLIST = new List<TKS11010_Member>();
                queryLIST = query.ToList();

                for (int i = 0; i < queryLIST.Count(); i++)
                {
                    if (queryLIST[i].請求年月 == null)
                    {
                        if (queryLIST[i].サイト != null)
                        {
                            DateTime Wk;
                            Wk = DateTime.TryParse(p作成年 + "/" + p作成月 + "/" + "01", out Wk) ? Wk : DateTime.Today;
                            DateTime dtBirth = Wk;
                            dtBirth = dtBirth.AddMonths(Convert.ToInt32(-queryLIST[i].サイト));
                            queryLIST[i].請求年月 = dtBirth;
                        }
                    }
                }
                return queryLIST.ToList();
            }
        }
        #endregion

        #region 締日帳票CSV
        /// <summary>
        /// TKS11010 締日帳票CSV
        /// </summary>
        /// <param name="p商品ID">得意先コード</param>
        /// <returns>S02</returns>
        public List<TKS11010_Member_CSV> SEARCH_TKS11010_GetDataList_CSV(string p得意先From, string p得意先To, int?[] i得意先List, string p作成集金日, bool b全集金日, string p作成年, string p作成月, int 作成区分_CValue, int? 集計, DateTime? d集計期間From, DateTime? d集計期間To)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<TKS11010_Member_CSV> retList = new List<TKS11010_Member_CSV>();
                List<TKS11010_Member_CSV> queryList = new List<TKS11010_Member_CSV>();
                context.Connection.Open();

                //支払先指定　表示用
                string 得意先指定表示 = string.Empty;
                //サイト計算
                var query2 = (from m01 in context.M01_TOK.Where(m01 => m01.削除日付 == null && m01.親子区分ID != 3)
                              select new TKS11010_Member2
                              {
                                  得意先ID = m01.得意先ID,
                                  得意先名 = m01.略称名,
                                  得意先KEY = m01.得意先KEY,
                                  サイト = m01.Ｔサイト日,
                                  締日 = m01.Ｔ締日,
                                  集金日 = m01.Ｔ集金日,
								  親子区分ID = m01.親子区分ID,
								  作成年月 = 集計 ?? 0,
                              }).ToList();

                DateTime d集計年月 = DateTime.Now;
                for (int i = 0; i < query2.Count; i++)
                {
                    if (DateTime.TryParse(((p作成年).ToString() + "/" + (p作成月).ToString() + "/" + "01"), out d集計年月))
                    {
                        query2[i].年月 = ((d集計年月.AddMonths(Convert.ToInt32(-query2[i].サイト)).Year) * 100) + (d集計年月.AddMonths(Convert.ToInt32(-query2[i].サイト)).Month);
                    };
                };

				var query = (from q01 in query2
							 join v01 in context.S01_TOKS on new { q01.得意先KEY, nen = q01.年月 } equals new { v01.得意先KEY, nen = v01.集計年月 } into v01Group
							 join nyukin in context.S01_TOKS on new { q01.得意先KEY, nen = q01.作成年月 } equals new { nyukin.得意先KEY, nen = nyukin.集計年月 } into nyukinGroup
                             select new TKS11010_Member_CSV
                             {
                                 得意先コード = q01.得意先ID,
                                 得意先名 = q01.得意先名,
                                 親子区分ID = q01.親子区分ID == 0 ? "" : q01.親子区分ID == 1 ? "親" : q01.親子区分ID == 2 ? "親" : "子",
                                 集金日 = q01.集金日 == null ? 0 : q01.集金日,
								 サイト = q01.サイト == null ? 0 : q01.サイト,
								 売上金額 = v01Group.Sum(v01g => v01g.締日売上金額),
								 内課税額 = v01Group.Sum(v01g => v01g.締日課税売上),
								 消費税 = v01Group.Sum(v01g => v01g.締日消費税),
								 通行料 = v01Group.Sum(v01g => v01g.締日通行料),
								 回収予定額 = v01Group.Sum(v01g => v01g.締日売上金額 + v01g.締日通行料 + v01g.締日消費税),
                                 請求年月 = null,
								 締日 = q01.締日 == null ? 0 : q01.締日,
								 当月入金額 = nyukinGroup.Sum(c => c.締日入金現金 + c.締日入金手形),
								 入金調整額 = nyukinGroup.Sum(c => c.締日入金その他),
								 入金合計額 = nyukinGroup.Sum(c => c.締日入金現金 + c.締日入金手形 + c.締日入金その他),
                                 集計年月 = q01.年月,
                                 全集金日 = p作成集金日 == string.Empty ? "全集金日" : p作成集金日,
                                 対象年月 = d集計期間From,
                                 表示区分 = 作成区分_CValue == 0 ? "（回収予定あり得意先のみ）" : 作成区分_CValue == 1 ? "（回収予定無し得意先含む）" : "",
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
                        int i支払先FROM = AppCommon.IntParse(p得意先From);
                        query = query.Where(c => c.得意先コード >= i支払先FROM);
                    }

                    //得意先To処理　Max値
                    if (!string.IsNullOrEmpty(p得意先To))
                    {
                        int i支払先TO = AppCommon.IntParse(p得意先To);
                        query = query.Where(c => c.得意先コード <= i支払先TO);
                    }

                    //全締日集計処理
                    if (b全集金日 == true)
                    {
                        query = query.Where(c => c.集金日 >= 1 && c.集金日 <= 31);
                    }

                    //締日処理　
                    if (!string.IsNullOrEmpty(p作成集金日))
                    {
                        int? p変換作成締日 = AppCommon.IntParse(p作成集金日);
                        query = query.Where(c => c.集金日 == p変換作成締日);
                    }

                    if (i得意先List.Length > 0)
                    {
                        var intCause = i得意先List;
						query = query.Union(from q01 in query2
											join v01 in context.S01_TOKS on new { q01.得意先KEY, nen = q01.年月 } equals new { v01.得意先KEY, nen = v01.集計年月 } into v01Group
											join nyukin in context.S01_TOKS on new { q01.得意先KEY, nen = q01.作成年月 } equals new { nyukin.得意先KEY, nen = nyukin.集計年月 } into nyukinGroup
                                            where intCause.Contains(q01.得意先ID)
                                            select new TKS11010_Member_CSV
                                            {
                                                得意先コード = q01.得意先ID,
                                                得意先名 = q01.得意先名,
                                                親子区分ID = q01.親子区分ID == 0 ? "" : q01.親子区分ID == 1 ? "親" : q01.親子区分ID == 2 ? "親" : "子",
                                                集金日 = q01.集金日 == null ? 0 : q01.集金日,
												サイト = q01.サイト == null ? 0 : q01.サイト,
												売上金額 = v01Group.Sum(v01g => v01g.締日売上金額),
												内課税額 = v01Group.Sum(v01g => v01g.締日課税売上),
												消費税 = v01Group.Sum(v01g => v01g.締日消費税),
												通行料 = v01Group.Sum(v01g => v01g.締日通行料),
												回収予定額 = v01Group.Sum(v01g => v01g.締日売上金額 + v01g.締日通行料 + v01g.締日消費税),
                                                請求年月 = null,
												締日 = q01.締日 == null ? 0 : q01.締日,
												当月入金額 = nyukinGroup.Sum(c => c.締日入金現金 + c.締日入金手形),
												入金調整額 = nyukinGroup.Sum(c => c.締日入金その他),
												入金合計額 = nyukinGroup.Sum(c => c.締日入金現金 + c.締日入金手形 + c.締日入金その他),
                                                集計年月 = q01.年月,
                                                全集金日 = p作成集金日 == string.Empty ? "全集金日" : p作成集金日,
                                                対象年月 = d集計期間From,
                                                表示区分 = 作成区分_CValue == 0 ? "（回収予定あり得意先のみ）" : 作成区分_CValue == 1 ? "（回収予定無し得意先含む）" : "",
                                            }).AsQueryable();

                        //全締日集計処理
                        if (b全集金日 == true)
                        {
                            query = query.Where(c => c.集金日 >= 1 && c.集金日 <= 31);

                        }

                        //締日処理　
                        if (!string.IsNullOrEmpty(p作成集金日))
                        {
                            int? p変換作成締日 = AppCommon.IntParse(p作成集金日);
                            query = query.Where(c => c.集金日 == p変換作成締日);
                        }
                    }
                }
                else
                {
                    //得意先From処理　Min値
                    if (string.IsNullOrEmpty(p得意先From))
                    {
                        query = query.Where(c => c.得意先コード >= int.MinValue);
                    }

                    //得意先To処理　Max値
                    if (string.IsNullOrEmpty(p得意先To))
                    {
                        query = query.Where(c => c.得意先コード <= int.MaxValue);
                    }

                    //全締日集計処理
                    if (b全集金日 == true)
                    {
                        query = query.Where(c => c.集金日 >= 1 && c.集金日 <= 31);
                    }

                    //締日処理
                    if (!string.IsNullOrEmpty(p作成集金日))
                    {
                        int? p変換作成締日 = AppCommon.IntParse(p作成集金日);
                        query = query.Where(c => c.集金日 == p変換作成締日);
                    }
                }

                //内訳別表示処理
                //売上あり：0
                //売上なし：1
                switch (作成区分_CValue)
                {
                    //支払取引全体
                    case 0:
                        query = query.Where(c => c.回収予定額 != 0);
                        break;

                    //支払先
                    case 1:
                        //query = query.Where(c => c.回収予定額 >= 0);
                        break;

                    default:
                        break;
                }

                //支払先指定の表示
                if (i得意先List.Length > 0)
                {
                    for (int i = 0; i < query.Count(); i++)
                    {
                        得意先指定表示 = 得意先指定表示 + i得意先List[i].ToString();

                        if (i < i得意先List.Length)
                        {
                            if (i == i得意先List.Length - 1)
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

                List<TKS11010_Member_CSV> queryLIST = new List<TKS11010_Member_CSV>();
                queryLIST = query.ToList();

                for (int i = 0; i < queryLIST.Count(); i++)
                {
                    if (queryLIST[i].請求年月 == null)
                    {
                        if (queryLIST[i].サイト != null)
                        {
                            DateTime Wk;
                            Wk = DateTime.TryParse(p作成年 + "/" + p作成月 + "/" + "01", out Wk) ? Wk : DateTime.Today;
                            DateTime dtBirth = Wk;
                            dtBirth = dtBirth.AddMonths(Convert.ToInt32(-queryLIST[i].サイト));
                            queryLIST[i].請求年月 = dtBirth;
                        }
                    }
                }
                return queryLIST.ToList();
            }
        }
        #endregion
    }
}
