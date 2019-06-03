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
    public class TKS06010
    {
        #region メンバー定義
        /// <summary>
        /// TKS06010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class TKS06010_Member
        {
            public int?[] 得意先指定 { get; set; }
            public int? 得意先コード { get; set; }
            public string 得意先名 { get; set; }
            public decimal? 売上金額 { get; set; }
            public decimal? 通行料 { get; set; }
            public decimal? 課税売上 { get; set; }
            public decimal? 消費税 { get; set; }
            public decimal? 当月売上額 { get; set; }
            public decimal? 傭車使用売上 { get; set; }
            public decimal? 傭車料 { get; set; }
            public decimal? 差益 { get; set; }
            public decimal? 差益率 { get; set; }
            public decimal? 件数 { get; set; }
            public string 未定 { get; set; }
            public string 対象年月 { get; set; }
            public int? 集計年月 { get; set; }
            public string 集計区分 { get; set; }
            public string 親子区分ID { get; set; }
            public int? 締日 { get; set; }
            public string 全締日 { get; set; }
            public string かな読み { get; set; }
            public int 取引区分 { get; set; }
            public decimal? 締日未定件数 { get; set; }
            public string 表示順序 { get; set; }
            public string 得意先指定コード { get; set; }
            public string 得意先Sコード { get; set; }
            public string 得意先Fコード { get; set; }
            public int? 明細番号 { get; set; }
            public int? 明細行 { get; set; }
            public int? 回数 { get; set; }
            public int? 請求内訳ID { get; set; }
            public DateTime? 締集計開始日 { get; set; }
            public DateTime? 締集計終了日 { get; set; }
            public int? 親コード { get; set; }
        }

        /// <summary>
        /// TKS06010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class TKS06010_Member1
        {
            public int?[] 得意先指定 { get; set; }
            public int? 得意先コード { get; set; }
            public string 得意先名 { get; set; }
            public decimal? 売上金額 { get; set; }
            public decimal? 通行料 { get; set; }
            public decimal? 課税売上 { get; set; }
            public decimal? 消費税 { get; set; }
            public decimal? 当月売上額 { get; set; }
            public decimal? 傭車使用売上 { get; set; }
            public decimal? 傭車料 { get; set; }
            public decimal? 差益 { get; set; }
            public decimal? 差益率 { get; set; }
            public decimal? 件数 { get; set; }
            public string 未定 { get; set; }
            public string 対象年月 { get; set; }
            public int? 集計年月 { get; set; }
            public string 集計区分 { get; set; }
            public string 親子区分ID { get; set; }
            public int? 締日 { get; set; }
            public string 全締日 { get; set; }
            public string かな読み { get; set; }
            public int 取引区分 { get; set; }
            public decimal? 締日未定件数 { get; set; }
            public string 表示順序 { get; set; }
            public string 得意先指定コード { get; set; }
            public string 得意先Sコード { get; set; }
            public string 得意先Fコード { get; set; }
            public int? 明細番号 { get; set; }
            public int? 明細行 { get; set; }
            public int? 回数 { get; set; }
            public int? 請求内訳ID { get; set; }
            public DateTime? 月集計開始日 { get; set; }
            public DateTime? 月集計終了日 { get; set; }
            public int? 親コード { get; set; }
        }


        /// <summary>
        /// TKS06010  CSV　メンバー
        /// </summary>
        [DataContract]
        public class TKS06010_Member_CSV
        {
            public int 得意先コード { get; set; }
            public string 得意先名 { get; set; }
            public int? 請求内訳ID { get; set; }
            public string 請求内訳名 { get; set; }
            public decimal? 売上金額 { get; set; }
            public decimal? 通行料 { get; set; }
            public decimal? 課税売上 { get; set; }
            public decimal? 消費税 { get; set; }
            public decimal? 当月売上額 { get; set; }
            public decimal? 傭車使用売上 { get; set; }
            public decimal? 傭車料 { get; set; }
            public decimal? 差益 { get; set; }
            public decimal? 差益率 { get; set; }
            public decimal? 件数 { get; set; }
            public string 未定 { get; set; }
            public string 親子区分ID { get; set; }
            public int? 締日 { get; set; }
            public decimal? 締日未定件数 { get; set; }
			//public string 対象年月 { get; set; }
			//public int? 集計年月 { get; set; }
			public string かな読み { get; set; }
			//public int? 回数 { get; set; }
			//public string 全締日 { get; set; }
			//public string 集計区分 { get; set; }
			//public string 表示順序 { get; set; }
			//public int? 親コード { get; set; }
        }

        /// <summary>
        /// TKS06010  CSV　メンバー
        /// </summary>
        [DataContract]
        public class TKS06010_Member1_CSV
        {
            public int 得意先コード { get; set; }
            public string 得意先名 { get; set; }
            public int? 請求内訳ID { get; set; }
            public string 請求内訳名 { get; set; }
            public decimal? 売上金額 { get; set; }
            public decimal? 通行料 { get; set; }
            public decimal? 課税売上 { get; set; }
            public decimal? 消費税 { get; set; }
            public decimal? 当月売上額 { get; set; }
            public decimal? 傭車使用売上 { get; set; }
            public decimal? 傭車料 { get; set; }
            public decimal? 差益 { get; set; }
            public decimal? 差益率 { get; set; }
            public decimal? 件数 { get; set; }
            public string 未定 { get; set; }
            public string 親子区分ID { get; set; }
            public int? 締日 { get; set; }
            public decimal? 締日未定件数 { get; set; }
            public string 対象年月 { get; set; }
            public int? 集計年月 { get; set; }
            public string かな読み { get; set; }
            public int? 回数 { get; set; }
            public string 全締日 { get; set; }
            public string 集計区分 { get; set; }
            public string 表示順序 { get; set; }
            public int? 親コード { get; set; }
        }

        [DataContract]
        public class TKS
        {
            public int 得意先コード { get; set; }
            public string 得意先名 { get; set; }
            public int 締日 { get; set; }
            public string かな読み { get; set; }
        }
        #endregion

        #region 締日帳票印刷
        /// <summary>
        /// TKS06010 締日帳票印刷
        /// </summary>
        /// <param name="p商品ID">得意先コード</param>
        /// <returns>S02</returns>
        public List<TKS06010_Member> SEARCH_TKS06010_SimebiPreview(string p得意先From, string p得意先To, int?[] i得意先List, int 集計区分_CValue, string p作成締日, bool b全締日集計, string p作成年, string p作成月, int p表示順序, int 作成区分_CValue, bool b内訳別合計, DateTime p集計期間From, DateTime p集計期間To, int? 集計)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                //差益率計算時に利用
                List<TKS06010_Member> retList = new List<TKS06010_Member>();
                //要素の削除時に利用
                List<TKS06010_Member> queryList = new List<TKS06010_Member>();
                context.Connection.Open();

                //支払先指定　表示用
                string 得意先指定表示 = string.Empty;
                //集計一覧
                var query = (from t01 in context.M01_TOK
                             join s01 in context.S01_TOKS.Where(c => c.回数 == 1) on t01.得意先KEY equals s01.得意先KEY into v01Group
                             where t01.削除日付 == null
                             select new TKS06010_Member
                             {
                                 得意先コード = t01.得意先ID,
                                 請求内訳ID = null,
                                 得意先名 = t01.略称名,
                                 売上金額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額),
                                 通行料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日通行料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日通行料),
                                 課税売上 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日課税売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日課税売上),
                                 消費税 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日消費税),
                                 当月売上額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額 + v01.締日通行料 + v01.締日消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額 + v01.締日通行料 + v01.締日消費税),
                                 傭車使用売上 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車売上),
                                 傭車料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車料),
                                 差益 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車売上 - v01.締日内傭車料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車売上 - v01.締日内傭車料),
                                 差益率 = 0,//v01Group.Where(v01 => v01.集計年月 == 集計).Max(v01 => ((v01.締日内傭車売上 - v01.締日内傭車料) / (v01.締日内傭車売上 == 0 ? 1 : v01.締日内傭車売上)) * 100) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Max(v01 => ((v01.締日内傭車売上 - v01.締日内傭車料) / (v01.締日内傭車売上 == 0 ? 1 : v01.締日内傭車売上)) * 100),
                                 全締日 = p作成締日 == string.Empty ? "全締日" : p作成締日,
                                 親子区分ID = t01.親子区分ID == 0 ? "" : t01.親子区分ID == 1 ? "親" : t01.親子区分ID == 2 ? "親" : "子",
                                 締日 = t01.Ｔ締日,
                                 集計年月 = 集計,
                                 件数 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日件数) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日件数),
                                 かな読み = t01.かな読み,
                                 未定 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日未定件数) >= 1 ? "未定" : "",
                                 対象年月 = p作成年 + "　年　" + p作成月 + "　月度",
                                 集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                 表示順序 = p表示順序 == 0 ? "ID順" : p表示順序 == 1 ? "かな順" : p表示順序 == 2 ? "売上順" : "ID順",
                                 得意先指定コード = 得意先指定表示 == "" ? "" : 得意先指定表示,
                                 得意先Sコード = p得意先From == "" ? "" : p得意先From + " ～ ",
                                 得意先Fコード = p得意先To == "" ? "" : p得意先To,
                                 親コード = t01.親ID,
                             }).AsQueryable();



                //内訳表示処理
                if (b内訳別合計 == true)
                {
                    query = query.Union(from m01 in context.M01_TOK
                                        join t01 in context.T01_TRN.Where(t01 => (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 ==1)) on m01.得意先KEY equals t01.得意先KEY into mt01Group
                                        join s01 in context.S01_TOKS.Where(s01 => s01.集計年月 == 集計 && s01.回数 == 1) on m01.得意先KEY equals s01.得意先KEY
                                        join m10 in context.M10_UHK on m01.得意先KEY equals m10.得意先KEY
                                        where m01.削除日付 == null
                                        select new TKS06010_Member
                                        {
                                            得意先コード = m01.得意先ID,
                                            請求内訳ID = m10.請求内訳ID,
                                            得意先名 = m10.請求内訳名,
                                            売上金額 = mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額) == null ? 0 : mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額),
                                            通行料 = mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.通行料) == null ? 0 : mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.通行料),
                                            課税売上 = 0,
                                            消費税 = 0,
                                            当月売上額 = mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額 + mt01.通行料) == null ? 0 : mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額 + mt01.通行料),
                                            傭車使用売上 = mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額) == null ? 0 : mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額),
                                            傭車料 = mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.支払金額) == null ? 0 : mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.支払金額),
                                            差益 = mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額 - mt01.支払金額) == null ? 0 : mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額 - mt01.支払金額),
                                            差益率 = 0,
                                            全締日 = p作成締日 == string.Empty ? "全締日" : p作成締日,
                                            親子区分ID = m01.親子区分ID != null ? "内訳" : "内訳",
                                            締日 = m01.Ｔ締日,
                                            集計年月 = s01.集計年月,
                                            件数 = mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Count() == null ? 0 : mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Count(),
                                            かな読み = m01.かな読み,
                                            未定 = s01.締日未定件数 >= 1 ? "未定" : "",
                                            対象年月 = p作成年 + "　年　" + p作成月 + "　月度",
                                            集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                            表示順序 = p表示順序 == 0 ? "ID順" : p表示順序 == 1 ? "かな順" : p表示順序 == 2 ? "売上順" : "ID順",
                                            得意先指定コード = 得意先指定表示 == "" ? "" : 得意先指定表示,
                                            得意先Sコード = p得意先From == "" ? "" : p得意先From + " ～ ",
                                            得意先Fコード = p得意先To == "" ? "" : p得意先To,
                                            親コード = m01.親ID,
                                        }).AsQueryable();
                }

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

                //内訳別表示処理
                //売上あり：0
                //売上なし：1
                switch (作成区分_CValue)
                {
                    //支払取引全体
                    case 0:
						query = query.Where(c => c.当月売上額 != 0 || c.傭車料 != 0);
                        break;

                    //支払先
                    case 1:
                        //query = query.Where(c => c.当月売上額 >= 0);
                        break;

                    default:
                        break;
                }


                    //表示順序処理
                    switch (p表示順序)
                    {
                        //支払取引全体
                        case 0:
                            query = query.OrderBy(c => c.得意先コード);
                            break;

                        //支払先
                        case 1:
                            query = query.OrderBy(c => c.かな読み);
                            break;

                        //経費先
                        case 2:
                            query = query.OrderByDescending(c => c.当月売上額);
                            break;

                        default:
                            break;
                    }

                //得意先指定の表示
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

                //必要のないデータの削除
                queryList = query.ToList();


                //差益率計算
                var intCause = i得意先List;
                decimal? p合計 = 0;
                decimal? 各差益率 = 0;
                //From、Toが空のときのピックアップ処理
                if (string.IsNullOrEmpty(p得意先From + p得意先To))
                {
                    if (i得意先List.Length > 0)
                    {
                        queryList = (queryList.Where(c => intCause.Contains(c.得意先コード)).ToList());
                    }

                    if (b全締日集計)
                    {
                        retList = queryList;
                        for (int i = 0; i < retList.Count; i++)
                        {
                            if (retList[i].傭車使用売上 == 0)
                            {
                                retList[i].差益率 = (retList[i].差益 / 1) * 100;
                            }
                            else
                            {
                                retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                            }
                        }
                    }
                    else
                    {
                        retList = queryList;
                        for (int i = 0; i < retList.Count; i++)
                        {
                            if (retList[i].傭車使用売上 == 0)
                            {
                                retList[i].差益率 = (retList[i].差益 / 1) * 100;
                            }
                            else
                            {
                                retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                            }
                        }
                    }
                }
                else
                //From、Toが入力された場合のピックアップ処理
                {
                    if (i得意先List.Length > 0)
                    {
                        //*** From、Toの入力があり、かつピックアップ指定入力時 ***//
                        int iFrom = 0;
                        int iTo = 0;

                        if (!string.IsNullOrEmpty(p得意先From) && !string.IsNullOrEmpty(p得意先To))
                        {
                            iFrom = AppCommon.IntParse(p得意先From);
                            iTo = AppCommon.IntParse(p得意先To);
                            queryList = (queryList.Where(c => intCause.Contains(c.得意先コード) || (c.得意先コード >= iFrom && c.得意先コード <= iTo))).ToList();
                        }
                        else if (!string.IsNullOrEmpty(p得意先From))
                        {
                            iFrom = AppCommon.IntParse(p得意先From);
                            queryList = (queryList.Where(c => intCause.Contains(c.得意先コード) || (c.得意先コード >= iFrom))).ToList();
                        }
                        else if (!string.IsNullOrEmpty(p得意先To))
                        {
                            iTo = AppCommon.IntParse(p得意先To);
                            queryList = (queryList.Where(c => intCause.Contains(c.得意先コード) || (c.得意先コード <= iTo))).ToList();
                        }

                        if (b全締日集計)
                        {
                            retList = queryList;
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].傭車使用売上 == 0)
                                {
                                    retList[i].差益率 = (retList[i].差益 / 1) * 100;
                                }
                                else
                                {
                                    retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                                }
                            }
                        }
                        else
                        {
                            retList = queryList;
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].傭車使用売上 == 0)
                                {
                                    retList[i].差益率 = (retList[i].差益 / 1) * 100;
                                }
                                else
                                {
                                    retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                                }
                            }
                        }
                    }
                    else
                    //*** From、Toの入力があり、かつピックアップ指定が未入力の時 ***//
                    {
                        if (!string.IsNullOrEmpty(p得意先From) && !string.IsNullOrEmpty(p得意先To))
                        {
                            int? ipForm = AppCommon.IntParse(p得意先From);
                            int? ipTo = AppCommon.IntParse(p得意先To);
                            queryList = (queryList.Where(c => (c.得意先コード >= ipForm && c.得意先コード <= ipTo))).ToList();
                        }
                        else if (!string.IsNullOrEmpty(p得意先From))
                        {
                            int? ipFrom = AppCommon.IntParse(p得意先From);
                            queryList = (queryList.Where(c => (c.得意先コード >= ipFrom))).ToList();
                        }
                        else if (!string.IsNullOrEmpty(p得意先To))
                        {
                            int? ipTo = AppCommon.IntParse(p得意先To);
                            queryList = (queryList.Where(c => (c.得意先コード <= ipTo))).ToList();
                        }

                        if (b全締日集計)
                        {
                            retList = queryList;
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].傭車使用売上 == 0)
                                {
                                    retList[i].差益率 = (retList[i].差益 / 1) * 100;
                                }
                                else
                                {
                                    retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                                }
                            }
                        }
                        else
                        {
                            retList = queryList;
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].傭車使用売上 == 0)
                                {
                                    retList[i].差益率 = (retList[i].差益 / 1) * 100;
                                }
                                else
                                {
                                    retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                                }
                            }
                        }
                    }
                }
                //出力
                return retList;
            }
        }
        #endregion

        #region 月次帳票印刷
        /// <summary>
        /// TKS06010 月次帳票印刷
        /// </summary>
        /// <param name="p商品ID">得意先コード</param>
        /// <returns>S12</returns>
        public List<TKS06010_Member1> SEARCH_TKS06010_GetsujiPreview(string p得意先From, string p得意先To, int?[] i得意先List, int 集計区分_CValue, string p作成締日, bool b全締日集計, string p作成年, string p作成月, int p表示順序, int 作成区分_CValue, bool b内訳別合計, DateTime p集計期間From, DateTime p集計期間To, int? 集計)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                //差益率計算時に利用
                List<TKS06010_Member1> retList = new List<TKS06010_Member1>();
                //要素の削除時に利用
                List<TKS06010_Member1> queryList = new List<TKS06010_Member1>();
                context.Connection.Open();

                //支払先指定　表示用
                string 得意先指定表示 = string.Empty;
                //集計一覧
                var query = (from t01 in context.M01_TOK
                             join s11 in context.S11_TOKG.Where(c => c.回数 == 1) on t01.得意先KEY equals s11.得意先KEY into v01Group
                             where t01.削除日付 == null
                             select new TKS06010_Member1
                             {
                                 得意先コード = t01.得意先ID,
                                 請求内訳ID = null,
                                 得意先名 = t01.略称名,
                                 売上金額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額),
                                 通行料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次通行料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次通行料),
                                 課税売上 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次課税売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次課税売上),
                                 消費税 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次消費税),
                                 当月売上額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額 + v01.月次通行料 + v01.月次消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額 + v01.月次通行料 + v01.月次消費税),
                                 傭車使用売上 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車売上),
                                 傭車料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車料),
                                 差益 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車売上 - v01.月次内傭車料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車売上 - v01.月次内傭車料),
                                 差益率 = 0,//v01Group.Where(v01 => v01.集計年月 == 集計).Max(v01 => ((v01.締日内傭車売上 - v01.締日内傭車料) / (v01.締日内傭車売上 == 0 ? 1 : v01.締日内傭車売上)) * 100) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Max(v01 => ((v01.締日内傭車売上 - v01.締日内傭車料) / (v01.締日内傭車売上 == 0 ? 1 : v01.締日内傭車売上)) * 100),
                                 全締日 = p作成締日 == string.Empty ? "全締日" : p作成締日,
                                 親子区分ID = t01.親子区分ID == 0 ? "" : t01.親子区分ID == 1 ? "親" : t01.親子区分ID == 2 ? "親" : "子",
                                 締日 = t01.Ｔ締日,
                                 集計年月 = 集計,
                                 件数 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次件数) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次件数),
                                 かな読み = t01.かな読み,
                                 未定 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次未定件数) >= 1 ? "未定" : "",
                                 対象年月 = p作成年 + "　年　" + p作成月 + "　月度",
                                 集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                 表示順序 = p表示順序 == 0 ? "ID順" : p表示順序 == 1 ? "かな順" : p表示順序 == 2 ? "売上順" : "ID順",
                                 得意先指定コード = 得意先指定表示 == "" ? "" : 得意先指定表示,
                                 得意先Sコード = p得意先From == "" ? "" : p得意先From + " ～ ",
                                 得意先Fコード = p得意先To == "" ? "" : p得意先To,
                                 親コード = t01.親ID,
                             }).AsQueryable();

                //内訳表示処理
                if (b内訳別合計 == true)
                {
                    query = query.Union(from m01 in context.M01_TOK
                                        join t01 in context.T01_TRN.Where(t01 => (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 == 1)) on m01.得意先KEY equals t01.得意先KEY into mt01Group
                                        join s01 in context.S11_TOKG.Where(s01 => s01.集計年月 == 集計 && s01.回数 == 1) on m01.得意先KEY equals s01.得意先KEY
                                        join m10 in context.M10_UHK on m01.得意先KEY equals m10.得意先KEY
                                        where m01.削除日付 == null
                                        select new TKS06010_Member1
                                        {
                                            得意先コード = m01.得意先ID,
                                            請求内訳ID = m10.請求内訳ID,
                                            得意先名 = m10.請求内訳名,
                                            売上金額 = mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額) == null ? 0 : mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額),
                                            通行料 = mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.通行料) == null ? 0 : mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.通行料),
                                            課税売上 = 0,
                                            消費税 = 0,
                                            当月売上額 = mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額 + mt01.通行料) == null ? 0 : mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額 + mt01.通行料),
                                            傭車使用売上 = mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額) == null ? 0 : mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額),
                                            傭車料 = mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.支払金額) == null ? 0 : mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.支払金額),
                                            差益 = mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額 - mt01.支払金額) == null ? 0 : mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額 - mt01.支払金額),
                                            差益率 = 0,
                                            全締日 = p作成締日 == string.Empty ? "全締日" : p作成締日,
                                            親子区分ID = m01.親子区分ID != null ? "内訳" : "内訳",
                                            締日 = m01.Ｔ締日,
                                            集計年月 = s01.集計年月,
                                            件数 = mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Count() == null ? 0 : mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Count(),
                                            かな読み = m01.かな読み,
                                            未定 = s01.月次未定件数 >= 1 ? "未定" : "",
                                            対象年月 = p作成年 + "　年　" + p作成月 + "　月度",
                                            集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                            表示順序 = p表示順序 == 0 ? "ID順" : p表示順序 == 1 ? "かな順" : p表示順序 == 2 ? "売上順" : "ID順",
                                            得意先指定コード = 得意先指定表示 == "" ? "" : 得意先指定表示,
                                            得意先Sコード = p得意先From == "" ? "" : p得意先From + " ～ ",
                                            得意先Fコード = p得意先To == "" ? "" : p得意先To,
                                            親コード = m01.親ID,
                                        }).AsQueryable();
                }

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

                //内訳別表示処理
                //売上あり：0
                //売上なし：1
                switch (作成区分_CValue)
                {
                    //支払取引全体
                    case 0:
						query = query.Where(c => c.当月売上額 != 0 || c.傭車料 != 0);
                        break;

                    //支払先
                    case 1:
                        //query = query.Where(c => c.当月売上額 >= 0);
                        break;

                    default:
                        break;
                }


                var ret = (from q01 in query
                           from oya in context.M01_TOK.Where(c => c.得意先ID == q01.親コード)
                           where oya.削除日付 == null
                           select new TKS
                           {
                               得意先コード = oya.得意先ID,
                               締日 = oya.Ｔ締日,
                               得意先名 = oya.略称名,
                               かな読み = oya.かな読み,

                           }).Distinct().AsQueryable();

                List<TKS> retLIST = ret.ToList();
                List<TKS06010_Member1> qList = query.ToList();

                for (int i = 0; i < retLIST.Count(); i++)
                {
                    int i得意先コード = retLIST[i].得意先コード;
                    if (qList.Where(c => c.得意先コード == i得意先コード).Count() == 0)
                    {
                        qList.Add(new TKS06010_Member1()
                        {
                            得意先コード = i得意先コード,
                            請求内訳ID = null,
                            得意先名 = retLIST[i].得意先名,
                            売上金額 = null,
                            通行料 = null,
                            課税売上 = null,
                            消費税 = null,
                            当月売上額 = null,
                            傭車使用売上 = null,
                            傭車料 = null,
                            差益 = null,
                            差益率 = null,
                            全締日 = null,
                            親子区分ID = "親",
                            締日 = retLIST[i].締日,
                            集計年月 = null,
                            件数 = null,
                            かな読み = retLIST[i].かな読み,
                            未定 = null,
                            対象年月 = null,
                            集計区分 = null,
                            表示順序 = null,
                            得意先指定コード = 得意先指定表示 == "" ? "" : 得意先指定表示,
                            得意先Sコード = p得意先From == "" ? "" : p得意先From + " ～ ",
                            得意先Fコード = p得意先To == "" ? "" : p得意先To,
                        });
                    }
                }

                foreach (var row in qList)
                {
                    if (row.親コード != null)
                    {

                        foreach (var rows in qList)
                        {
                            if (rows.得意先コード == row.親コード)
                            {
                                if (rows.請求内訳ID == null)
                                {
                                    rows.得意先コード = rows.得意先コード;
                                    rows.請求内訳ID = null;
                                    rows.得意先名 = rows.得意先名;
                                    rows.売上金額 = rows.売上金額 == null ? 0 + row.売上金額 : rows.売上金額 + row.売上金額;
                                    rows.通行料 = rows.通行料 == null ? 0 + row.通行料 : rows.通行料 + row.通行料;
                                    rows.課税売上 = rows.課税売上 == null ? 0 * row.課税売上 : rows.課税売上 + row.課税売上;
                                    rows.消費税 = rows.消費税 == null ? 0 + row.消費税 : rows.消費税 + row.消費税;
                                    rows.当月売上額 = rows.当月売上額 == null ? 0 + row.当月売上額 : rows.当月売上額 + row.当月売上額;
                                    rows.傭車使用売上 = rows.傭車使用売上 == null ? 0 + row.傭車使用売上 : rows.傭車使用売上 + row.傭車使用売上;
                                    rows.傭車料 = rows.傭車料 == null ? 0 * row.傭車料 : rows.傭車料 + row.傭車料;
                                    rows.差益 = rows.傭車使用売上 - rows.傭車料;
                                    rows.差益率 = 0;
                                    rows.全締日 = row.全締日;
                                    rows.親子区分ID = "親";
                                    rows.締日 = row.締日;
                                    rows.集計年月 = row.集計年月;
                                    rows.件数 = rows.件数 == null ? 0 + row.件数 : rows.件数 + row.件数;
                                    rows.かな読み = row.かな読み;
                                    rows.未定 = rows.未定 == "未定" ? "未定" : row.未定 == "未定" ? "未定" : "";
                                    rows.対象年月 = row.対象年月;
                                    rows.集計区分 = row.集計区分;
                                    rows.表示順序 = row.表示順序;
                                    rows.得意先指定コード = row.得意先指定コード;
                                    rows.得意先Sコード = row.得意先Sコード;
                                    rows.得意先Fコード = row.得意先Fコード;
                                }
                            }
                        }

                    }
                }


                //表示順序処理
                switch (p表示順序)
                {
                    //支払取引全体
                    case 0:
                        qList = qList.OrderBy(c => c.得意先コード).ToList();
                        break;

                    //支払先
                    case 1:
                        qList = qList.OrderBy(c => c.かな読み).ToList();
                        break;

                    //経費先
                    case 2:
                        qList = qList.OrderByDescending(c => c.当月売上額).ToList();
                        break;

                    default:
                        break;
                }

                //得意先指定の表示
                if (i得意先List.Length > 0)
                {
                    for (int i = 0; i < qList.Count(); i++)
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

                //必要のないデータの削除
                queryList = qList.ToList();
                int cnt = 0;
                for (int i = 0; i < queryList.Count; i++)
                {
                    if (queryList[i].集計年月 == 集計)
                    {
                        if (queryList[i].件数 < 1)
                        {
                            cnt++;
                        }
                        else
                        {
                            cnt--;
                        }

                        if (Convert.ToInt32(queryList.Count()) == cnt)
                        {
                            queryList.RemoveAll(c => c.集計年月 == 集計);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                //差益率計算
                var intCause = i得意先List;
                decimal? p合計 = 0;
                decimal? 各差益率 = 0;
                //From、Toが空のときのピックアップ処理
                if (string.IsNullOrEmpty(p得意先From + p得意先To))
                {
                    if (i得意先List.Length > 0)
                    {
                        queryList = (queryList.Where(c => intCause.Contains(c.得意先コード)).ToList());
                    }

                    if (b全締日集計)
                    {
                        retList = queryList;
                        for (int i = 0; i < retList.Count; i++)
                        {
                            if (retList[i].傭車使用売上 == 0)
                            {
                                retList[i].差益率 = (retList[i].差益 / 1) * 100;
                            }
                            else
                            {
                                retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                            }
                        }
                    }
                    else
                    {
                        retList = queryList;
                        for (int i = 0; i < retList.Count; i++)
                        {
                            if (retList[i].傭車使用売上 == 0)
                            {
                                retList[i].差益率 = (retList[i].差益 / 1) * 100;
                            }
                            else
                            {
                                retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                            }
                        }
                    }
                }
                else
                //From、Toが入力された場合のピックアップ処理
                {
                    if (i得意先List.Length > 0)
                    {
                        //*** From、Toの入力があり、かつピックアップ指定入力時 ***//
                        int iFrom = 0;
                        int iTo = 0;

                        if (!string.IsNullOrEmpty(p得意先From) && !string.IsNullOrEmpty(p得意先To))
                        {
                            iFrom = AppCommon.IntParse(p得意先From);
                            iTo = AppCommon.IntParse(p得意先To);
                            queryList = (queryList.Where(c => intCause.Contains(c.得意先コード) || (c.得意先コード >= iFrom && c.得意先コード <= iTo))).ToList();
                        }
                        else if (!string.IsNullOrEmpty(p得意先From))
                        {
                            iFrom = AppCommon.IntParse(p得意先From);
                            queryList = (queryList.Where(c => intCause.Contains(c.得意先コード) || (c.得意先コード >= iFrom))).ToList();
                        }
                        else if (!string.IsNullOrEmpty(p得意先To))
                        {
                            iTo = AppCommon.IntParse(p得意先To);
                            queryList = (queryList.Where(c => intCause.Contains(c.得意先コード) || (c.得意先コード <= iTo))).ToList();
                        }

                        if (b全締日集計)
                        {
                            retList = queryList;
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].傭車使用売上 == 0)
                                {
                                    retList[i].差益率 = (retList[i].差益 / 1) * 100;
                                }
                                else
                                {
                                    retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                                }
                            }
                        }
                        else
                        {
                            retList = queryList;
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].傭車使用売上 == 0)
                                {
                                    retList[i].差益率 = (retList[i].差益 / 1) * 100;
                                }
                                else
                                {
                                    retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                                }
                            }
                        }
                    }
                    else
                    //*** From、Toの入力があり、かつピックアップ指定が未入力の時 ***//
                    {
                        if (!string.IsNullOrEmpty(p得意先From) && !string.IsNullOrEmpty(p得意先To))
                        {
                            int? ipForm = AppCommon.IntParse(p得意先From);
                            int? ipTo = AppCommon.IntParse(p得意先To);
                            queryList = (queryList.Where(c => (c.得意先コード >= ipForm && c.得意先コード <= ipTo))).ToList();
                        }
                        else if (!string.IsNullOrEmpty(p得意先From))
                        {
                            int? ipFrom = AppCommon.IntParse(p得意先From);
                            queryList = (queryList.Where(c => (c.得意先コード >= ipFrom))).ToList();
                        }
                        else if (!string.IsNullOrEmpty(p得意先To))
                        {
                            int? ipTo = AppCommon.IntParse(p得意先To);
                            queryList = (queryList.Where(c => (c.得意先コード <= ipTo))).ToList();
                        }

                        if (b全締日集計)
                        {
                            retList = queryList;
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].傭車使用売上 == 0)
                                {
                                    retList[i].差益率 = (retList[i].差益 / 1) * 100;
                                }
                                else
                                {
                                    retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                                }
                            }
                        }
                        else
                        {
                            retList = queryList;
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].傭車使用売上 == 0)
                                {
                                    retList[i].差益率 = (retList[i].差益 / 1) * 100;
                                }
                                else
                                {
                                    retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                                }
                            }
                        }
                    }
                }
                //出力
                return retList;
            }
        }
        #endregion

        #region 締日CSV出力
        /// <summary>
        /// TKS06010 締日CSV出力
        /// </summary>
        /// <param name="p商品ID">得意先コード</param>
        /// <returns>S12</returns>
        public List<TKS06010_Member_CSV> SEARCH_TKS06010_SimebiCSV(string p得意先From, string p得意先To, int?[] i得意先List, int 集計区分_CValue, string p作成締日, bool b全締日集計, string p作成年, string p作成月, int p表示順序, int 作成区分_CValue, bool b内訳別合計, DateTime p集計期間From, DateTime p集計期間To, int? 集計)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<TKS06010_Member_CSV> retList = new List<TKS06010_Member_CSV>();
                List<TKS06010_Member_CSV> queryList = new List<TKS06010_Member_CSV>();
                context.Connection.Open();

                //集計一覧
                var query = (from t01 in context.M01_TOK
                             join s01 in context.S01_TOKS.Where(c => c.回数 == 1) on t01.得意先KEY equals s01.得意先KEY into v01Group
                             where t01.削除日付 == null
                             select new TKS06010_Member_CSV
                             {
                                 得意先コード = t01.得意先ID,
                                 請求内訳ID = null,
                                 得意先名 = t01.略称名,
                                 売上金額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額),
                                 通行料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日通行料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日通行料),
                                 課税売上 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日課税売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日課税売上),
                                 消費税 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日消費税),
                                 当月売上額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額 + v01.締日通行料 + v01.締日消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額 + v01.締日通行料 + v01.締日消費税),
                                 傭車使用売上 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車売上),
                                 傭車料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車料),
                                 差益 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車売上 - v01.締日内傭車料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車売上 - v01.締日内傭車料),
                                 差益率 = 0,//v01Group.Where(v01 => v01.集計年月 == 集計).Max(v01 => ((v01.締日内傭車売上 - v01.締日内傭車料) / (v01.締日内傭車売上 == 0 ? 1 : v01.締日内傭車売上)) * 100) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Max(v01 => ((v01.締日内傭車売上 - v01.締日内傭車料) / (v01.締日内傭車売上 == 0 ? 1 : v01.締日内傭車売上)) * 100),
								 //全締日 = p作成締日 == string.Empty ? "全締日" : p作成締日,
                                 親子区分ID = t01.親子区分ID == 0 ? "" : t01.親子区分ID == 1 ? "親" : t01.親子区分ID == 2 ? "親" : "子",
                                 締日 = t01.Ｔ締日,
                                 件数 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日件数) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日件数),
								 かな読み = t01.かな読み,
                                 未定 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日未定件数) >= 1 ? "未定" : "",
								 //対象年月 = p作成年 + "　年　" + p作成月 + "　月度",
								 //集計年月 = 集計,
								 //集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
								 //表示順序 = p表示順序 == 0 ? "ID順" : p表示順序 == 1 ? "かな順" : p表示順序 == 2 ? "売上順" : "ID順",
								 //親コード = t01.親ID
                             }).AsQueryable();

                //内訳表示処理
                if (b内訳別合計 == true)
                {
                    query = query.Union(from m01 in context.M01_TOK
                                        join t01 in context.T01_TRN.Where(t01 => (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 == 1)) on m01.得意先KEY equals t01.得意先KEY into mt01Group
                                        join s01 in context.S01_TOKS.Where(s01 => s01.集計年月 == 集計 && s01.回数 == 1) on m01.得意先KEY equals s01.得意先KEY
                                        join m10 in context.M10_UHK on m01.得意先KEY equals m10.得意先KEY
                                        where m01.削除日付 == null
                                        select new TKS06010_Member_CSV
                                        {
                                            得意先コード = m01.得意先ID,
                                            請求内訳ID = m10.請求内訳ID,
                                            得意先名 = m10.請求内訳名,
                                            売上金額 = mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額) == null ? 0 : mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額),
                                            通行料 = mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.通行料) == null ? 0 : mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.通行料),
                                            課税売上 = 0,
                                            消費税 = 0,
                                            当月売上額 = mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額 + mt01.通行料) == null ? 0 : mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額 + mt01.通行料),
                                            傭車使用売上 = mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額) == null ? 0 : mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額),
                                            傭車料 = mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.支払金額) == null ? 0 : mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.支払金額),
                                            差益 = mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額 - mt01.支払金額) == null ? 0 : mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額 - mt01.支払金額),
                                            差益率 = 0,  //***下にて算出***//
											//全締日 = p作成締日 == string.Empty ? "全締日" : p作成締日,
                                            親子区分ID = m01.親子区分ID != null ? "内訳" : "内訳",
                                            締日 = m01.Ｔ締日,
                                            件数 = mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Count() == null ? 0 : mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Count(),
											かな読み = m01.かな読み,
                                            未定 = s01.締日未定件数 >= 1 ? "未定" : "",
											//対象年月 = p作成年 + "　年　" + p作成月 + "　月度",
											//集計年月 = 集計,
											//集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
											//表示順序 = p表示順序 == 0 ? "ID順" : p表示順序 == 1 ? "かな順" : p表示順序 == 2 ? "売上順" : "ID順",
											//親コード = m01.親ID
                                        }).AsQueryable();
                }

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

                //内訳別表示処理
                //売上あり：0
                //売上なし：1
                switch (作成区分_CValue)
                {
                    //支払取引全体
                    case 0:
						query = query.Where(c => c.当月売上額 != 0 || c.傭車料 != 0);
                        break;

                    //支払先
                    case 1:
                        //query = query.Where(c => c.当月売上額 >= 0);
                        break;

                    default:
                        break;
                }


				//var ret = (from q01 in query
				//		   from oya in context.M01_TOK.Where(c => c.得意先KEY == q01.親コード)
				//		   where oya.削除日付 == null
				//		   select new TKS
				//		   {
				//			   得意先コード = oya.得意先ID,
				//			   締日 = oya.Ｔ締日,
				//			   得意先名 = oya.略称名,
				//			   かな読み = oya.かな読み,

				//		   }).Distinct().AsQueryable();

				//List<TKS> retLIST = ret.ToList();



				//List<TKS06010_Member_CSV> qList = query.ToList();
				//for (int i = 0; i < retLIST.Count(); i++)
				//{
				//	int i得意先コード = retLIST[i].得意先コード;
				//	if (qList.Where(c => c.得意先コード == i得意先コード).Count() == 0)
				//	{
				//		qList.Add(new TKS06010_Member_CSV()
				//		{
				//			得意先コード = i得意先コード,
				//			請求内訳ID = null,
				//			得意先名 = retLIST[i].得意先名,
				//			売上金額 = null,
				//			通行料 = null,
				//			課税売上 = null,
				//			消費税 = null,
				//			当月売上額 = null,
				//			傭車使用売上 = null,
				//			傭車料 = null,
				//			差益 = null,
				//			差益率 = null,
				//			全締日 = null,
				//			親子区分ID = "親",
				//			締日 = retLIST[i].締日,
				//			集計年月 = null,
				//			件数 = null,
				//			かな読み = retLIST[i].かな読み,
				//			未定 = null,
				//			対象年月 = null,
				//			集計区分 = null,
				//			表示順序 = null,
                            
				//		});
				//	}
				//}

				//foreach (var row in qList)
				//{
				//	if (row.親コード != null)
				//	{

				//		foreach (var rows in qList)
				//		{
				//			if (rows.得意先コード == row.親コード)
				//			{
				//				if (rows.請求内訳ID == null)
				//				{
				//					rows.得意先コード = rows.得意先コード;
				//					rows.請求内訳ID = null;
				//					rows.得意先名 = rows.得意先名;
				//					rows.売上金額 = rows.売上金額 == null ? 0 + row.売上金額 : rows.売上金額 + row.売上金額;
				//					rows.通行料 = rows.通行料 == null ? 0 + row.通行料 : rows.通行料 + row.通行料;
				//					rows.課税売上 = rows.課税売上 == null ? 0 * row.課税売上 : rows.課税売上 + row.課税売上;
				//					rows.消費税 = rows.消費税 == null ? 0 + row.消費税 : rows.消費税 + row.消費税;
				//					rows.当月売上額 = rows.当月売上額 == null ? 0 + row.当月売上額 : rows.当月売上額 + row.当月売上額;
				//					rows.傭車使用売上 = rows.傭車使用売上 == null ? 0 + row.傭車使用売上 : rows.傭車使用売上 + row.傭車使用売上;
				//					rows.傭車料 = rows.傭車料 == null ? 0 * row.傭車料 : rows.傭車料 + row.傭車料;
				//					rows.差益 = rows.傭車使用売上 - rows.傭車料;
				//					rows.差益率 = 0;
				//					rows.全締日 = row.全締日;
				//					rows.親子区分ID = "親";
				//					rows.締日 = row.締日;
				//					rows.集計年月 = row.集計年月;
				//					rows.件数 = rows.件数 == null ? 0 + row.件数 : rows.件数 + row.件数;
				//					rows.かな読み = row.かな読み;
				//					rows.未定 = rows.未定 == "未定" ? "未定" : row.未定 == "未定" ? "未定" : "";
				//					rows.対象年月 = row.対象年月;
				//					rows.集計区分 = row.集計区分;
				//					rows.表示順序 = row.表示順序;
                                 
				//				}
				//			}
				//		}

				//	}
				//}

                //表示順序処理
                switch (p表示順序)
                {
                    //支払取引全体
                    case 0:
					query = query.OrderBy(c => c.得意先コード);
                        break;

                    //支払先
                    case 1:
						query = query.OrderBy(c => c.かな読み);
                        break;

                    //経費先
                    case 2:
						query = query.OrderByDescending(c => c.当月売上額);
                        break;

                    default:
                        break;
                }

				////得意先指定の表示
				//if (i得意先List.Length > 0)
				//{
				//	for (int i = 0; i < query.Count(); i++)
				//	{
				//		得意先指定表示 = 得意先指定表示 + i得意先List[i].ToString();

				//		if (i < i得意先List.Length)
				//		{
				//			if (i == i得意先List.Length - 1)
				//			{
				//				break;
				//			}
				//			得意先指定表示 = 得意先指定表示 + ",";
				//		}
				//		if (i得意先List.Length == 1)
				//		{
				//			break;
				//		}
				//	}
				//}

                //必要のないデータの削除
				queryList = query.ToList();

                //差益率計算
                var intCause = i得意先List;
                decimal? p合計 = 0;
                decimal? 各差益率 = 0;
                //From、Toが空のときのピックアップ処理
                if (string.IsNullOrEmpty(p得意先From + p得意先To))
                {
                    if (i得意先List.Length > 0)
                    {
                        queryList = (queryList.Where(c => intCause.Contains(c.得意先コード)).ToList());
                    }

                    if (b全締日集計)
                    {
                        retList = queryList;
                        for (int i = 0; i < retList.Count; i++)
                        {
                            if (retList[i].傭車使用売上 == 0)
                            {
                                retList[i].差益率 = (retList[i].差益 / 1) * 100;
                            }
                            else
                            {
                                retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                            }
                        }
                    }
                    else
                    {
                        retList = queryList;
                        for (int i = 0; i < retList.Count; i++)
                        {
                            if (retList[i].傭車使用売上 == 0)
                            {
                                retList[i].差益率 = (retList[i].差益 / 1) * 100;
                            }
                            else
                            {
                                retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                            }
                        }
                    }
                }
                else
                //From、Toが入力された場合のピックアップ処理
                {
                    if (i得意先List.Length > 0)
                    {
                        //*** From、Toの入力があり、かつピックアップ指定入力時 ***//
                        int iFrom = 0;
                        int iTo = 0;

                        if (!string.IsNullOrEmpty(p得意先From) && !string.IsNullOrEmpty(p得意先To))
                        {
                            iFrom = AppCommon.IntParse(p得意先From);
                            iTo = AppCommon.IntParse(p得意先To);
                            queryList = (queryList.Where(c => intCause.Contains(c.得意先コード) || (c.得意先コード >= iFrom && c.得意先コード <= iTo))).ToList();
                        }
                        else if (!string.IsNullOrEmpty(p得意先From))
                        {
                            iFrom = AppCommon.IntParse(p得意先From);
                            queryList = (queryList.Where(c => intCause.Contains(c.得意先コード) || (c.得意先コード >= iFrom))).ToList();
                        }
                        else if (!string.IsNullOrEmpty(p得意先To))
                        {
                            iTo = AppCommon.IntParse(p得意先To);
                            queryList = (queryList.Where(c => intCause.Contains(c.得意先コード) || (c.得意先コード <= iTo))).ToList();
                        }

                        if (b全締日集計)
                        {
                            retList = queryList;
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].傭車使用売上 == 0)
                                {
                                    retList[i].差益率 = (retList[i].差益 / 1) * 100;
                                }
                                else
                                {
                                    retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                                }
                            }
                        }
                        else
                        {
                            retList = queryList;
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].傭車使用売上 == 0)
                                {
                                    retList[i].差益率 = (retList[i].差益 / 1) * 100;
                                }
                                else
                                {
                                    retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                                }
                            }
                        }
                    }
                    else
                    //*** From、Toの入力があり、かつピックアップ指定が未入力の時 ***//
                    {
                        if (!string.IsNullOrEmpty(p得意先From) && !string.IsNullOrEmpty(p得意先To))
                        {
                            int? ipForm = AppCommon.IntParse(p得意先From);
                            int? ipTo = AppCommon.IntParse(p得意先To);
                            queryList = (queryList.Where(c => (c.得意先コード >= ipForm && c.得意先コード <= ipTo))).ToList();
                        }
                        else if (!string.IsNullOrEmpty(p得意先From))
                        {
                            int? ipFrom = AppCommon.IntParse(p得意先From);
                            queryList = (queryList.Where(c => (c.得意先コード >= ipFrom))).ToList();
                        }
                        else if (!string.IsNullOrEmpty(p得意先To))
                        {
                            int? ipTo = AppCommon.IntParse(p得意先To);
                            queryList = (queryList.Where(c => (c.得意先コード <= ipTo))).ToList();
                        }

                        if (b全締日集計)
                        {
                            retList = queryList;
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].傭車使用売上 == 0)
                                {
                                    retList[i].差益率 = (retList[i].差益 / 1) * 100;
                                }
                                else
                                {
                                    retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                                }
                            }
                        }
                        else
                        {
                            retList = queryList;
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].傭車使用売上 == 0)
                                {
                                    retList[i].差益率 = (retList[i].差益 / 1) * 100;
                                }
                                else
                                {
                                    retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                                }
                            }
                        }
                    }
                }
                //出力
                return retList;
            }
        }
        #endregion

        #region 月次CSV出力
        /// <summary>
        /// TKS06010 月次CSV出力
        /// </summary>
        /// <param name="p商品ID">得意先コード</param>
        /// <returns>S12</returns>
        public List<TKS06010_Member1_CSV> SEARCH_TKS06010_GetsujiCSV(string p得意先From, string p得意先To, int?[] i得意先List, int 集計区分_CValue, string p作成締日, bool b全締日集計, string p作成年, string p作成月, int p表示順序, int 作成区分_CValue, bool b内訳別合計, DateTime p集計期間From, DateTime p集計期間To, int? 集計)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<TKS06010_Member1_CSV> retList = new List<TKS06010_Member1_CSV>();
                List<TKS06010_Member1_CSV> queryList = new List<TKS06010_Member1_CSV>();
                context.Connection.Open();

                //集計一覧
                var query = (from t01 in context.M01_TOK.Where(c => c.削除日付 == null)
                             join s11 in context.S11_TOKG.Where(c => c.回数 == 1) on t01.得意先KEY equals s11.得意先KEY into v01Group
                             where t01.削除日付 == null
                             select new TKS06010_Member1_CSV
                             {
                                 得意先コード = t01.得意先ID,
                                 請求内訳ID = null,
                                 得意先名 = t01.略称名,
                                 売上金額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額),
                                 通行料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次通行料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次通行料),
                                 課税売上 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次課税売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次課税売上),
                                 消費税 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次消費税),
                                 当月売上額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額 + v01.月次通行料 + v01.月次消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額 + v01.月次通行料 + v01.月次消費税),
                                 傭車使用売上 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車売上),
                                 傭車料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車料),
                                 差益 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車売上 - v01.月次内傭車料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車売上 - v01.月次内傭車料),
                                 差益率 = 0,//v01Group.Where(v01 => v01.集計年月 == 集計).Max(v01 => ((v01.締日内傭車売上 - v01.締日内傭車料) / (v01.締日内傭車売上 == 0 ? 1 : v01.締日内傭車売上)) * 100) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Max(v01 => ((v01.締日内傭車売上 - v01.締日内傭車料) / (v01.締日内傭車売上 == 0 ? 1 : v01.締日内傭車売上)) * 100),
                                 全締日 = p作成締日 == null ? "全締日" : p作成締日,
                                 親子区分ID = t01.親子区分ID == 0 ? "" : t01.親子区分ID == 1 ? "親" : t01.親子区分ID == 2 ? "親" : "子",
                                 締日 = t01.Ｔ締日,
                                 件数 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次件数) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次件数),
                                 かな読み = t01.かな読み,
                                 未定 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次未定件数) >= 1 ? "未定" : "",
								 //対象年月 = p作成年 + "　年　" + p作成月 + "　月度",
								 //集計年月 = 集計,
								 //集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
								 //表示順序 = p表示順序 == 0 ? "ID順" : p表示順序 == 1 ? "かな順" : p表示順序 == 2 ? "売上順" : "ID順",
								 //親コード = t01.親ID,
                             }).AsQueryable();

                //内訳表示処理
                if (b内訳別合計 == true)
                {
                    query = query.Union(from m01 in context.M01_TOK
                                        join t01 in context.T01_TRN.Where(t01 => (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 == 1)) on m01.得意先KEY equals t01.得意先KEY into mt01Group
                                        join s01 in context.S11_TOKG.Where(s01 => s01.集計年月 == 集計 && s01.回数 == 1) on m01.得意先KEY equals s01.得意先KEY
                                        join m10 in context.M10_UHK on m01.得意先KEY equals m10.得意先KEY
                                        where m01.削除日付 == null
                                        select new TKS06010_Member1_CSV
                                        {
                                            得意先コード = m01.得意先ID,
                                            請求内訳ID = m10.請求内訳ID,
                                            得意先名 = m10.請求内訳名,
                                            売上金額 = mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額) == null ? 0 : mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額),
                                            通行料 = mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.通行料) == null ? 0 : mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.通行料),
                                            課税売上 = 0,
                                            消費税 = 0,
                                            当月売上額 = mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額 + mt01.通行料) == null ? 0 : mt01Group.Where(mt01 => mt01.請求日付 >= p集計期間From && mt01.請求日付 <= p集計期間To && mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額 + mt01.通行料),
                                            傭車使用売上 = mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額) == null ? 0 : mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額),
                                            傭車料 = mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.支払金額) == null ? 0 : mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.支払金額),
                                            差益 = mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額 - mt01.支払金額) == null ? 0 : mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Sum(mt01 => mt01.売上金額 - mt01.支払金額),
                                            差益率 = 0,
                                            全締日 = p作成締日 == null ? "全締日" : p作成締日,
                                            親子区分ID = m01.親子区分ID != null ? "内訳" : "内訳",
                                            締日 = m01.Ｔ締日,
                                            件数 = mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Count() == null ? 0 : mt01Group.Where(mt01 => mt01.支払先KEY != 0 && mt01.請求内訳ID == m10.請求内訳ID).Count(),
                                            かな読み = m01.かな読み,
                                            未定 = s01.月次未定件数 >= 1 ? "未定" : "",
											//対象年月 = p作成年 + "　年　" + p作成月 + "　月度",
											//集計年月 = 集計,
											//集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
											//表示順序 = p表示順序 == 0 ? "ID順" : p表示順序 == 1 ? "かな順" : p表示順序 == 2 ? "売上順" : "ID順",
											//親コード = m01.親ID,
                                        }).AsQueryable();
                }

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

                //内訳別表示処理
                //売上あり：0
                //売上なし：1
                switch (作成区分_CValue)
                {
                    //支払取引全体
                    case 0:
						query = query.Where(c => c.当月売上額 != 0 || c.傭車料 != 0);
                        break;

                    //支払先
                    case 1:
                        //query = query.Where(c => c.当月売上額 >= 0);
                        break;

                    default:
                        break;
                }

				//var ret = (from q01 in query
				//		   from oya in context.M01_TOK.Where(c => c.得意先ID == q01.親コード)
				//		   where oya.削除日付 == null
				//		   select new TKS
				//		   {
				//			   得意先コード = oya.得意先ID,
				//			   締日 = oya.Ｔ締日,
				//			   得意先名 = oya.略称名,
				//			   かな読み = oya.かな読み,

				//		   }).Distinct().AsQueryable();

				//List<TKS> retLIST = ret.ToList();



				//List<TKS06010_Member1_CSV> qList = query.ToList();
				//for (int i = 0; i < retLIST.Count(); i++)
				//{
				//	int i得意先コード = retLIST[i].得意先コード;
				//	if (qList.Where(c => c.得意先コード == i得意先コード).Count() == 0)
				//	{
				//		qList.Add(new TKS06010_Member1_CSV()
				//		{
				//			得意先コード = i得意先コード,
				//			請求内訳ID = null,
				//			得意先名 = retLIST[i].得意先名,
				//			売上金額 = null,
				//			通行料 = null,
				//			課税売上 = null,
				//			消費税 = null,
				//			当月売上額 = null,
				//			傭車使用売上 = null,
				//			傭車料 = null,
				//			差益 = null,
				//			差益率 = null,
				//			全締日 = null,
				//			親子区分ID = "親",
				//			締日 = retLIST[i].締日,
				//			集計年月 = null,
				//			件数 = null,
				//			かな読み = retLIST[i].かな読み,
				//			未定 = null,
				//			対象年月 = null,
				//			集計区分 = null,
				//			表示順序 = null,

				//		});
				//	}
				//}

				//foreach (var row in qList)
				//{
				//	if (row.親コード != null)
				//	{

				//		foreach (var rows in qList)
				//		{
				//			if (rows.得意先コード == row.親コード)
				//			{
				//				if (rows.請求内訳ID == null)
				//				{
				//					rows.得意先コード = rows.得意先コード;
				//					rows.請求内訳ID = null;
				//					rows.得意先名 = rows.得意先名;
				//					rows.売上金額 = rows.売上金額 == null ? 0 + row.売上金額 : rows.売上金額 + row.売上金額;
				//					rows.通行料 = rows.通行料 == null ? 0 + row.通行料 : rows.通行料 + row.通行料;
				//					rows.課税売上 = rows.課税売上 == null ? 0 * row.課税売上 : rows.課税売上 + row.課税売上;
				//					rows.消費税 = rows.消費税 == null ? 0 + row.消費税 : rows.消費税 + row.消費税;
				//					rows.当月売上額 = rows.当月売上額 == null ? 0 + row.当月売上額 : rows.当月売上額 + row.当月売上額;
				//					rows.傭車使用売上 = rows.傭車使用売上 == null ? 0 + row.傭車使用売上 : rows.傭車使用売上 + row.傭車使用売上;
				//					rows.傭車料 = rows.傭車料 == null ? 0 * row.傭車料 : rows.傭車料 + row.傭車料;
				//					rows.差益 = rows.傭車使用売上 - rows.傭車料;
				//					rows.差益率 = 0;
				//					rows.全締日 = row.全締日;
				//					rows.親子区分ID = "親";
				//					rows.締日 = row.締日;
				//					rows.集計年月 = row.集計年月;
				//					rows.件数 = rows.件数 == null ? 0 + row.件数 : rows.件数 + row.件数;
				//					rows.かな読み = row.かな読み;
				//					rows.未定 = rows.未定 == "未定" ? "未定" : row.未定 == "未定" ? "未定" : "";
				//					rows.対象年月 = row.対象年月;
				//					rows.集計区分 = row.集計区分;
				//					rows.表示順序 = row.表示順序;

				//				}
				//			}
				//		}

				//	}
				//}

                //表示順序処理
                switch (p表示順序)
                {
                    //支払取引全体
                    case 0:
					query = query.OrderBy(c => c.得意先コード);
                        break;

                    //支払先
                    case 1:
						query = query.OrderBy(c => c.かな読み);
                        break;

                    //経費先
                    case 2:
						query = query.OrderByDescending(c => c.当月売上額);
                        break;

                    default:
                        break;
                }

                //件数のないデータの削除
				queryList = query.ToList();

                //差益率計算
                var intCause = i得意先List;
                decimal? p合計 = 0;
                decimal? 各差益率 = 0;
                //From、Toが空のときのピックアップ処理
                if (string.IsNullOrEmpty(p得意先From + p得意先To))
                {
                    if (i得意先List.Length > 0)
                    {
                        queryList = (queryList.Where(c => intCause.Contains(c.得意先コード)).ToList());
                    }

                    if (b全締日集計)
                    {
                        retList = queryList;
                        for (int i = 0; i < retList.Count; i++)
                        {
                            if (retList[i].傭車使用売上 == 0)
                            {
                                retList[i].差益率 = (retList[i].差益 / 1) * 100;
                            }
                            else
                            {
                                retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                            }
                        }
                    }
                    else
                    {
                        retList = queryList;
                        for (int i = 0; i < retList.Count; i++)
                        {
                            if (retList[i].傭車使用売上 == 0)
                            {
                                retList[i].差益率 = (retList[i].差益 / 1) * 100;
                            }
                            else
                            {
                                retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                            }
                        }
                    }
                }
                else
                //From、Toが入力された場合のピックアップ処理
                {
                    if (i得意先List.Length > 0)
                    {
                        //*** From、Toの入力があり、かつピックアップ指定入力時 ***//
                        int iFrom = 0;
                        int iTo = 0;

                        if (!string.IsNullOrEmpty(p得意先From) && !string.IsNullOrEmpty(p得意先To))
                        {
                            iFrom = AppCommon.IntParse(p得意先From);
                            iTo = AppCommon.IntParse(p得意先To);
                            queryList = (queryList.Where(c => intCause.Contains(c.得意先コード) || (c.得意先コード >= iFrom && c.得意先コード <= iTo))).ToList();
                        }
                        else if (!string.IsNullOrEmpty(p得意先From))
                        {
                            iFrom = AppCommon.IntParse(p得意先From);
                            queryList = (queryList.Where(c => intCause.Contains(c.得意先コード) || (c.得意先コード >= iFrom))).ToList();
                        }
                        else if (!string.IsNullOrEmpty(p得意先To))
                        {
                            iTo = AppCommon.IntParse(p得意先To);
                            queryList = (queryList.Where(c => intCause.Contains(c.得意先コード) || (c.得意先コード <= iTo))).ToList();
                        }

                        if (b全締日集計)
                        {
                            retList = queryList;
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].傭車使用売上 == 0)
                                {
                                    retList[i].差益率 = (retList[i].差益 / 1) * 100;
                                }
                                else
                                {
                                    retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                                }
                            }
                        }
                        else
                        {
                            retList = queryList;
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].傭車使用売上 == 0)
                                {
                                    retList[i].差益率 = (retList[i].差益 / 1) * 100;
                                }
                                else
                                {
                                    retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                                }
                            }
                        }
                    }
                    else
                    //*** From、Toの入力があり、かつピックアップ指定が未入力の時 ***//
                    {
                        if (!string.IsNullOrEmpty(p得意先From) && !string.IsNullOrEmpty(p得意先To))
                        {
                            int? ipForm = AppCommon.IntParse(p得意先From);
                            int? ipTo = AppCommon.IntParse(p得意先To);
                            queryList = (queryList.Where(c => (c.得意先コード >= ipForm && c.得意先コード <= ipTo))).ToList();
                        }
                        else if (!string.IsNullOrEmpty(p得意先From))
                        {
                            int? ipFrom = AppCommon.IntParse(p得意先From);
                            queryList = (queryList.Where(c => (c.得意先コード >= ipFrom))).ToList();
                        }
                        else if (!string.IsNullOrEmpty(p得意先To))
                        {
                            int? ipTo = AppCommon.IntParse(p得意先To);
                            queryList = (queryList.Where(c => (c.得意先コード <= ipTo))).ToList();
                        }

                        if (b全締日集計)
                        {
                            retList = queryList;
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].傭車使用売上 == 0)
                                {
                                    retList[i].差益率 = (retList[i].差益 / 1) * 100;
                                }
                                else
                                {
                                    retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                                }
                            }
                        }
                        else
                        {
                            retList = queryList;
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].傭車使用売上 == 0)
                                {
                                    retList[i].差益率 = (retList[i].差益 / 1) * 100;
                                }
                                else
                                {
                                    retList[i].差益率 = (retList[i].差益 / retList[i].傭車使用売上) * 100;
                                }
                            }
                        }
                    }
                }
                //出力
                return retList;
            }
        }
        #endregion
    }
}