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
    public class SHR09010
    {

        #region メンバー定義
        /// <summary>
        /// SHR09010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class SHR09010_Member2
        {
            public int 支払先ID { get; set; }
            public string 支払先名 { get; set; }
            public int 支払先KEY { get; set; }
            public int サイト { get; set; }
            public int 年月 { get; set; }
            public int 締日 { get; set; }
            public int 集金日 { get; set; }
			public DateTime? 削除日付 { get; set; }
			public int 作成年月 { get; set; }
        }

        /// <summary>
        /// SHR09010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class SHR09010_Member
        {
            public int? 支払先コード { get; set; }
            public string 支払先名 { get; set; }
            public string 親子区分ID { get; set; }
            public int? 集金日 { get; set; }
            public int? サイト { get; set; }
            public string 表示サイト { get; set; }
            public decimal? 支払金額 { get; set; }
            public decimal? 内課税額 { get; set; }
            public decimal? 消費税 { get; set; }
            public decimal? 支払通行料 { get; set; }
            public decimal? 支払予定額 { get; set; }
            public DateTime? 支払年月 { get; set; }
            public int? 締日 { get; set; }
            public decimal? 当月出金額 { get; set; }
            public decimal? 出金調整額 { get; set; }
            public decimal? 出金合計額 { get; set; }
            public DateTime? 対象年月 { get; set; }
            public int 集計年月 { get; set; }
            public string 全集金日 { get; set; }
            public string 支払先指定コード { get; set; }
            public string 支払先Sコード { get; set; }
            public string 支払先Fコード { get; set; }
            public DateTime? 締集計開始日 { get; set; }
            public DateTime? 締集計終了日 { get; set; }
            public string 表示区分 { get; set; }
        }

        /// <summary>
        /// SHR09010  CSV　メンバー
        /// </summary>
        [DataContract]
        public class SHR09010_Member_CSV
        {
            public int? 支払先コード { get; set; }
            public string 支払先名 { get; set; }
            public int? 集金日 { get; set; }
            public int? サイト { get; set; }
            public decimal? 支払金額 { get; set; }
            public decimal? 内課税額 { get; set; }
            public decimal? 消費税 { get; set; }
            public decimal? 支払通行料 { get; set; }
            public decimal? 支払予定額 { get; set; }
            public DateTime? 支払年月 { get; set; }
            public int? 締日 { get; set; }
            public decimal? 当月出金額 { get; set; }
            public decimal? 出金調整額 { get; set; }
            public decimal? 出金合計額 { get; set; }
            public DateTime? 対象年月 { get; set; }
            public int 集計年月 { get; set; }
            public string 全集金日 { get; set; }
            public int 表示区分 { get; set; }
        }
        #endregion

        #region 帳票印刷
        /// <summary>
        /// SHR09010 帳票印刷
        /// </summary>
        /// <param name="p商品ID">支払先コード</param>
        /// <returns>SHR09010</returns>
        public List<SHR09010_Member> SHR09010_GetDataList(string p支払先From, string p支払先To, int?[] i支払先List, string p集金日, bool b全集金日, string p作成年, string p作成月, int 作成区分_CValue, DateTime? d集計期間From, DateTime? d集計期間To, int? 集計)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<SHR09010_Member> retList = new List<SHR09010_Member>();
                List<SHR09010_Member> queryList = new List<SHR09010_Member>();
                context.Connection.Open();

                //支払先指定　表示用
                string 支払先指定表示 = string.Empty;

                //サイト計算
                var query2 = (from m01 in context.M01_TOK.Where(m01 => m01.削除日付 == null)
                              select new SHR09010_Member2
                              {
                                  支払先ID = m01.得意先ID,
                                  支払先名 = m01.略称名,
                                  支払先KEY = m01.得意先KEY,
                                  サイト = m01.Ｓサイト日,
								  締日 = m01.Ｓ締日,
								  集金日 = m01.Ｓ集金日,
								  作成年月 = 集計 ?? 0,
                              }).ToList();

                DateTime d集計年月 = DateTime.Now;
                for (int i = 0; i < query2.Count; i++)
                {
                    if (DateTime.TryParse(((p作成年).ToString() + "/" + (p作成月).ToString() + "/" + "01"), out d集計年月))
                    {
                        query2[i].年月 = ((d集計年月.AddMonths(-query2[i].サイト).Year) * 100) + (d集計年月.AddMonths(-query2[i].サイト).Month);
                    };
                };

				var query = (from q01 in query2
							 join s02 in context.S02_YOSS on new { q01.支払先KEY, nen = q01.年月 } equals new { s02.支払先KEY, nen = s02.集計年月 } into v01Group
							 join nyukin in context.S02_YOSS on new { q01.支払先KEY, nen = q01.作成年月 } equals new { nyukin.支払先KEY, nen = nyukin.集計年月 } into nyukinGroup
                             select new SHR09010_Member
                             {
                                 支払先コード = q01.支払先ID,
                                 支払先名 = q01.支払先名,
                                 集金日 = q01.集金日,
                                 表示サイト = q01.サイト == 0 ? "当月" : q01.サイト == 1 ? "翌月" : q01.サイト == 2 ? "翌々月" : q01.サイト == 3 ? "３ヶ月" : "",
                                 サイト = q01.サイト,
                                 支払金額 = v01Group.Sum(v01 => v01.締日売上金額),
                                 内課税額 = v01Group.Sum(v01 => v01.締日課税売上),
                                 消費税 = v01Group.Sum(v01 => v01.締日消費税),
                                 支払通行料 = v01Group.Sum(v01 => v01.締日通行料),
                                 支払予定額 = v01Group.Sum(v01 => v01.締日売上金額 + v01.締日通行料 + v01.締日消費税),
                                 支払年月 = null,
                                 締日 = q01.締日,
								 当月出金額 = nyukinGroup.Sum(v01 => v01.締日入金現金 + v01.締日入金手形),
								 出金調整額 = nyukinGroup.Sum(v01 => v01.締日入金その他),
								 出金合計額 = nyukinGroup.Sum(v01 => v01.締日入金現金 + v01.締日入金手形 + v01.締日入金その他),
                                 集計年月 = q01.年月,
                                 全集金日 = b全集金日 == true ? "全集金日" : p集金日 == null ? "なし" : p集金日,
                                 対象年月 = d集計期間From,
                                 支払先指定コード = 支払先指定表示 == "" ? "" : 支払先指定表示,
                                 支払先Sコード = p支払先From == "" ? "" : p支払先From + " ～ ",
                                 支払先Fコード = p支払先To == "" ? "" : p支払先To,
                                 表示区分 = 作成区分_CValue == 0 ? "（支払予定有り支払先のみ）" : "（支払予定無し支払先含む）",
                             }).AsQueryable();

                query = query.Distinct();

                if (!(string.IsNullOrEmpty(p支払先From + p支払先To) && i支払先List.Length == 0))
                {
                    //支払先が検索対象に入っていない時全データ取得
                    if (string.IsNullOrEmpty(p支払先From + p支払先To))
                    {
                        query = query.Where(c => c.支払先コード >= int.MaxValue);
                    }

                    //支払先From処理　Min値
                    if (!string.IsNullOrEmpty(p支払先From))
                    {
                        int i支払先FROM = AppCommon.IntParse(p支払先From);
                        query = query.Where(c => c.支払先コード >= i支払先FROM);
                    }

                    //支払先To処理　Max値
                    if (!string.IsNullOrEmpty(p支払先To))
                    {
                        int i支払先TO = AppCommon.IntParse(p支払先To);
                        query = query.Where(c => c.支払先コード <= i支払先TO);
                    }

                    //全締日集計処理
                    if (b全集金日 == true)
                    {
                        query = query.Where(c => c.集金日 >= 1 && c.集金日 <= 31);
                    }

                    //締日処理　
                    if (!string.IsNullOrEmpty(p集金日))
                    {
                        int? p変換作成締日 = AppCommon.IntParse(p集金日);
                        query = query.Where(c => c.集金日 == p変換作成締日);
                    }

                    if (i支払先List.Length > 0)
                    {
                        var intCause = i支払先List;
						query = query.Union(from q01 in query2
											join s02 in context.S02_YOSS on new { q01.支払先KEY, nen = q01.年月 } equals new { s02.支払先KEY, nen = s02.集計年月 } into v01Group
											join nyukin in context.S02_YOSS on new { q01.支払先KEY, nen = q01.作成年月 } equals new { nyukin.支払先KEY, nen = nyukin.集計年月 } into nyukinGroup
                                            where intCause.Contains(q01.支払先ID)
                                            select new SHR09010_Member
                                            {
                                                支払先コード = q01.支払先ID,
                                                支払先名 = q01.支払先名,
                                                集金日 = q01.集金日,
                                                表示サイト = q01.サイト == 0 ? "当月" : q01.サイト == 1 ? "翌月" : q01.サイト == 2 ? "翌々月" : q01.サイト == 3 ? "３ヶ月" : "",
												サイト = q01.サイト,
												支払金額 = v01Group.Sum(v01 => v01.締日売上金額),
												内課税額 = v01Group.Sum(v01 => v01.締日課税売上),
												消費税 = v01Group.Sum(v01 => v01.締日消費税),
												支払通行料 = v01Group.Sum(v01 => v01.締日通行料),
												支払予定額 = v01Group.Sum(v01 => v01.締日売上金額 + v01.締日通行料 + v01.締日消費税),
                                                支払年月 = null,
												締日 = q01.締日,
												当月出金額 = nyukinGroup.Sum(v01 => v01.締日入金現金 + v01.締日入金手形),
												出金調整額 = nyukinGroup.Sum(v01 => v01.締日入金その他),
												出金合計額 = nyukinGroup.Sum(v01 => v01.締日入金現金 + v01.締日入金手形 + v01.締日入金その他),
                                                集計年月 = q01.年月,
                                                全集金日 = b全集金日 == true ? "全集金日" : p集金日 == null ? "なし" : p集金日,
                                                対象年月 = d集計期間From,
                                                支払先指定コード = 支払先指定表示 == "" ? "" : 支払先指定表示,
                                                支払先Sコード = p支払先From == "" ? "" : p支払先From + " ～ ",
                                                支払先Fコード = p支払先To == "" ? "" : p支払先To,
                                                表示区分 = 作成区分_CValue == 0 ? "（支払予定有り支払先のみ）" : "（支払予定無し支払先含む）",
                                            }).OrderBy(c => c.支払先コード);
                        //全締日集計処理
                        if (b全集金日 == true)
                        {
                            query = query.Where(c => c.集金日 >= 1 && c.集金日 <= 31);
                        }

                        //締日処理　
                        if (!string.IsNullOrEmpty(p集金日))
                        {
                            int? p変換作成締日 = AppCommon.IntParse(p集金日);
                            query = query.Where(c => c.集金日 == p変換作成締日);
                        }
                    }
                }
                else
                {
                    //支払先From処理　Min値
                    if (string.IsNullOrEmpty(p支払先From))
                    {
                        query = query.Where(c => c.支払先コード >= int.MinValue);
                    }

                    //支払先To処理　Max値
                    if (string.IsNullOrEmpty(p支払先To))
                    {
                        query = query.Where(c => c.支払先コード <= int.MaxValue);
                    }

                    //全締日集計処理
                    if (b全集金日 == true)
                    {
                        query = query.Where(c => c.集金日 >= 1 && c.集金日 <= 31);
                    }

                    //締日処理
                    if (!string.IsNullOrEmpty(p集金日))
                    {
                        int? p変換作成締日 = AppCommon.IntParse(p集金日);
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
                        query = query.Where(c => c.支払予定額 != 0);
                        break;

                    //支払先
                    case 1:
                        //query = query.Where(c => c.支払予定額 >= 0);
                        break;

                    default:
                        break;
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

                //仮のリスト型を作成しデータを入れる
                List<SHR09010_Member> LIST = query.ToList();
                //** 変数 **//
                int? i支払先コード = 0;

                //リストを一件一件回す
                foreach (var data in LIST)
                {
                    //** 初回のみ  **//  i支払先コードが0の為追加
                    //** 2件目以降 **//  前回のi支払先コードと一致すればデータを取り除く
                    if (i支払先コード != data.支払先コード)
                    {
                        //重複してなければ追加
                        queryList.Add(data);
                    }
                    //今回の支払先コードを保管する
                    i支払先コード = data.支払先コード;
                }

                //必要のないデータの削除
                //queryList = query.ToList();
                //int cnt = 0;
                //for (int i = 0; i < queryList.Count; i++)
                //{
                //    if (queryList[i].出金合計額 == 0)
                //    {
                //        cnt++;
                //    }
                //    else
                //    {
                //        cnt--;
                //    }

                //    if (Convert.ToInt32(queryList.Count()) == cnt)
                //    {
                //        queryList.RemoveAll(c => c.当月出金額 == 0);
                //    }
                //    else
                //    {
                //        continue;
                //    }
                //}

                List<SHR09010_Member> queryLIST = queryList;

                for (int i = 0; i < queryLIST.Count; i++)
                {
                    if (queryLIST[i].支払年月 == null)
                    {
                        if (queryLIST[i].サイト != null)
                        {
                            DateTime Wk;
                            if (DateTime.TryParse(p作成年 + "/" + p作成月 + "/" + "01", out Wk))
                            {
                                DateTime dtBirth = Wk;
                                dtBirth = dtBirth.AddMonths(Convert.ToInt32(-queryLIST[i].サイト));
                                queryLIST[i].支払年月 = dtBirth;
                            }
                        }
                    }
                }
                return queryLIST.ToList();
            }

        }
        #endregion

        #region 帳票CSV
        /// <summary>
        /// SHR09010 帳票CSV
        /// </summary>
        /// <param name="p支払先FromID">支払先コード</param>
        /// <returns>S02</returns>
        public List<SHR09010_Member_CSV> SHR09010_CSV_GetDataList(string p支払先From, string p支払先To, int?[] i支払先List, string p集金日, bool b全集金日, string p作成年, string p作成月, int 作成区分_CValue, DateTime? d集計期間From, DateTime? d集計期間To, int? 集計)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<SHR09010_Member_CSV> retList = new List<SHR09010_Member_CSV>();
                List<SHR09010_Member_CSV> queryList = new List<SHR09010_Member_CSV>();
                context.Connection.Open();

                //支払先指定　表示用
                string 支払先指定表示 = string.Empty;

                //サイト計算
                var query2 = (from m01 in context.M01_TOK.Where(m01 => m01.削除日付 == null)
                              select new SHR09010_Member2
                              {
                                  支払先ID = m01.得意先ID,
                                  支払先名 = m01.略称名,
                                  支払先KEY = m01.得意先KEY,
                                  サイト = m01.Ｓサイト日,
								  締日 = m01.Ｓ締日,
								  集金日 = m01.Ｓ集金日,
								  作成年月 = 集計 ?? 0,
                              }).ToList();

                DateTime d集計年月 = DateTime.Now;
                for (int i = 0; i < query2.Count; i++)
                {
                    if (DateTime.TryParse(((p作成年).ToString() + "/" + (p作成月).ToString() + "/" + "01"), out d集計年月))
                    {
                        query2[i].年月 = ((d集計年月.AddMonths(-query2[i].サイト).Year) * 100) + (d集計年月.AddMonths(-query2[i].サイト).Month);
                    };
                };

				var query = (from q01 in query2
							 join s02 in context.S02_YOSS on new { q01.支払先KEY, nen = q01.年月 } equals new { s02.支払先KEY, nen = s02.集計年月 } into v01Group
							 join nyukin in context.S02_YOSS on new { q01.支払先KEY, nen = q01.作成年月 } equals new { nyukin.支払先KEY, nen = nyukin.集計年月 } into nyukinGroup
                             select new SHR09010_Member_CSV
                             {
                                 支払先コード = q01.支払先ID,
                                 支払先名 = q01.支払先名,
                                 集金日 = q01.集金日,
								 サイト = q01.サイト,
								 支払金額 = v01Group.Sum(v01 => v01.締日売上金額),
								 内課税額 = v01Group.Sum(v01 => v01.締日課税売上),
								 消費税 = v01Group.Sum(v01 => v01.締日消費税),
								 支払通行料 = v01Group.Sum(v01 => v01.締日通行料),
								 支払予定額 = v01Group.Sum(v01 => v01.締日売上金額 + v01.締日通行料 + v01.締日消費税),
                                 支払年月 = null,
								 締日 = q01.締日,
								 当月出金額 = nyukinGroup.Sum(v01 => v01.締日入金現金 + v01.締日入金手形),
								 出金調整額 = nyukinGroup.Sum(v01 => v01.締日入金その他),
								 出金合計額 = nyukinGroup.Sum(v01 => v01.締日入金現金 + v01.締日入金手形 + v01.締日入金その他),
                                 集計年月 = q01.年月,
                                 全集金日 = b全集金日 == true ? "全集金日" : p集金日 == null ? "なし" : p集金日,
                                 対象年月 = d集計期間From,
                             }).AsQueryable();


                if (!(string.IsNullOrEmpty(p支払先From + p支払先To) && i支払先List.Length == 0))
                {

                    //支払先が検索対象に入っていない時全データ取得
                    if (string.IsNullOrEmpty(p支払先From + p支払先To))
                    {
                        query = query.Where(c => c.支払先コード >= int.MaxValue);
                    }

                    //支払先From処理　Min値
                    if (!string.IsNullOrEmpty(p支払先From))
                    {
                        int i支払先FROM = AppCommon.IntParse(p支払先From);
                        query = query.Where(c => c.支払先コード >= i支払先FROM);
                    }

                    //支払先To処理　Max値
                    if (!string.IsNullOrEmpty(p支払先To))
                    {
                        int i支払先TO = AppCommon.IntParse(p支払先To);
                        query = query.Where(c => c.支払先コード <= i支払先TO);
                    }

                    //全締日集計処理
                    if (b全集金日 == true)
                    {
                        query = query.Where(c => c.集金日 >= 1 && c.集金日 <= 31);
                    }

                    //締日処理　
                    if (!string.IsNullOrEmpty(p集金日))
                    {
                        int? p変換作成締日 = AppCommon.IntParse(p集金日);
                        query = query.Where(c => c.集金日 == p変換作成締日);
                    }

                    if (i支払先List.Length > 0)
                    {
                        var intCause = i支払先List;
						query = query.Union(from q01 in query2
											join s02 in context.S02_YOSS on new { q01.支払先KEY, nen = q01.年月 } equals new { s02.支払先KEY, nen = s02.集計年月 } into v01Group
											join nyukin in context.S02_YOSS on new { q01.支払先KEY, nen = q01.作成年月 } equals new { nyukin.支払先KEY, nen = nyukin.集計年月 } into nyukinGroup
                                            where intCause.Contains(q01.支払先ID)
                                            select new SHR09010_Member_CSV
                                            {
                                                支払先コード = q01.支払先ID,
                                                支払先名 = q01.支払先名,
                                                集金日 = q01.集金日,
												サイト = q01.サイト,
												支払金額 = v01Group.Sum(v01 => v01.締日売上金額),
												内課税額 = v01Group.Sum(v01 => v01.締日課税売上),
												消費税 = v01Group.Sum(v01 => v01.締日消費税),
												支払通行料 = v01Group.Sum(v01 => v01.締日通行料),
												支払予定額 = v01Group.Sum(v01 => v01.締日売上金額 + v01.締日通行料 + v01.締日消費税),
                                                支払年月 = null,
												締日 = q01.締日,
												当月出金額 = nyukinGroup.Sum(v01 => v01.締日入金現金 + v01.締日入金手形),
												出金調整額 = nyukinGroup.Sum(v01 => v01.締日入金その他),
												出金合計額 = nyukinGroup.Sum(v01 => v01.締日入金現金 + v01.締日入金手形 + v01.締日入金その他),
                                                集計年月 = q01.年月,
                                                全集金日 = b全集金日 == true ? "全集金日" : p集金日 == null ? "なし" : p集金日,
                                                対象年月 = d集計期間From,
                                            }).AsQueryable();

                        //全締日集計処理
                        if (b全集金日 == true)
                        {
                            query = query.Where(c => c.集金日 >= 1 && c.集金日 <= 31);

                        }

                        //締日処理　
                        if (!string.IsNullOrEmpty(p集金日))
                        {
                            int? p変換作成締日 = AppCommon.IntParse(p集金日);
                            query = query.Where(c => c.集金日 == p変換作成締日);
                        }
                    }

                }
                else
                {

                    //支払先From処理　Min値
                    if (string.IsNullOrEmpty(p支払先From))
                    {
                        query = query.Where(c => c.支払先コード >= int.MinValue);
                    }

                    //支払先To処理　Max値
                    if (string.IsNullOrEmpty(p支払先To))
                    {
                        query = query.Where(c => c.支払先コード <= int.MaxValue);
                    }

                    //全締日集計処理
                    if (b全集金日 == true)
                    {
                        query = query.Where(c => c.集金日 >= 1 && c.集金日 <= 31);
                    }

                    //締日処理
                    if (!string.IsNullOrEmpty(p集金日))
                    {
                        int? p変換作成締日 = AppCommon.IntParse(p集金日);
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
                        query = query.Where(c => c.支払予定額 != 0);
                        break;

                    //支払先
                    case 1:
                        //query = query.Where(c => c.支払予定額 >= 0);
                        break;

                    default:
                        break;
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

                //必要のないデータの削除
                //queryList = query.ToList();
                //int cnt = 0;
                //for (int i = 0; i < queryList.Count; i++)
                //{
                //    if (queryList[i].出金合計額 == 0)
                //    {
                //        cnt++;
                //    }
                //    else
                //    {
                //        cnt--;
                //    }

                //    if (Convert.ToInt32(queryList.Count()) == cnt)
                //    {
                //        queryList.RemoveAll(c => c.当月出金額 == 0);
                //    }
                //    else
                //    {
                //        continue;
                //    }
                //}

                List<SHR09010_Member_CSV> queryLIST = queryList;

                for (int i = 0; i < queryLIST.Count(); i++)
                {
                    if (queryLIST[i].支払年月 == null)
                    {
                        if (queryLIST[i].サイト != null)
                        {
                            DateTime Wk;
                            if (DateTime.TryParse(p作成年 + "/" + p作成月 + "/" + "01", out Wk))
                            {
                                DateTime dtBirth = Wk;
                                dtBirth = dtBirth.AddMonths(Convert.ToInt32(-queryLIST[i].サイト));
                                queryLIST[i].支払年月 = dtBirth;
                            }
                        }
                    }
                }
                //出力
                return queryLIST.ToList();
            }

        }
        #endregion
    }
}
