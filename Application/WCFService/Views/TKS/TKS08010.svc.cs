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
    public class TKS08010
    {
        #region メンバー定義
        /// <summary>
        /// TKS08010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class TKS08010_Member
        {
            public int?[] 得意先指定 { get; set; }
            public int? 得意先コード { get; set; }
            public string 得意先名 { get; set; }
            public decimal? 前月残高 { get; set; }
            public decimal? 入金金額 { get; set; }
            public decimal? 入金調整額 { get; set; }
            public decimal? 入金手形 { get; set; }
            public decimal? 入金その他 { get; set; }
            public decimal? 差引金額 { get; set; }
            public decimal? 売上金額 { get; set; }
            public decimal? 通行料 { get; set; }
            public decimal? 内課税額 { get; set; }
            public decimal? 消費税 { get; set; }
            public decimal? 当月売上 { get; set; }
            public decimal? 内傭車売上 { get; set; }
            public decimal? 内傭車料 { get; set; }
            public decimal? 当月合計額 { get; set; }
            public decimal? 繰越金額 { get; set; }
            public decimal? 件数 { get; set; }
            public string 未定 { get; set; }
            public DateTime 対象年月 { get; set; }
            public int? 集計年月 { get; set; }
            public string 親子区分 { get; set; }
            public string 締日集計区分 { get; set; }
            public string 作成区分 { get; set; }
            public int? Ｔ締日 { get; set; }
            public int? 締日 { get; set; }
            public string 全締日 { get; set; }
            //[DataMember]
            //public string 表示締日 { get; set; }
            public string かな読み { get; set; }
            public decimal? 未定件数 { get; set; }
            public string 表示順序 { get; set; }
            public string 得意先指定コード { get; set; }
            public string 得意先Sコード { get; set; }
            public string 得意先Fコード { get; set; }
            public int? 回数 { get; set; }
            public DateTime? 締集計開始日 { get; set; }
            public DateTime? 締集計終了日 { get; set; }
        }


        /// <summary>
        /// TKS08010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class TKS08010_Member1
        {
            public int?[] 得意先指定 { get; set; }
            public int? 得意先コード { get; set; }
            public string 得意先名 { get; set; }
            public decimal? 前月残高 { get; set; }
            public decimal? 入金金額 { get; set; }
            public decimal? 入金調整額 { get; set; }
            public decimal? 入金手形 { get; set; }
            public decimal? 入金その他 { get; set; }
            public decimal? 差引金額 { get; set; }
            public decimal? 売上金額 { get; set; }
            public decimal? 通行料 { get; set; }
            public decimal? 内課税額 { get; set; }
            public decimal? 消費税 { get; set; }
            public decimal? 当月売上 { get; set; }
            public decimal? 内傭車売上 { get; set; }
            public decimal? 内傭車料 { get; set; }
            public decimal? 当月合計額 { get; set; }
            public decimal? 繰越金額 { get; set; }
            public decimal? 件数 { get; set; }
            public string 未定 { get; set; }
            public DateTime 対象年月 { get; set; }
            public int? 集計年月 { get; set; }
            public string 親子区分 { get; set; }
            public string 締日集計区分 { get; set; }
            public string 作成区分 { get; set; }
            public int? Ｔ締日 { get; set; }
            public int? 締日 { get; set; }
            public string 全締日 { get; set; }
            //[DataMember]
            //public string 表示締日 { get; set; }
            public string かな読み { get; set; }
            public decimal? 未定件数 { get; set; }
            public string 表示順序 { get; set; }
            public string 得意先指定コード { get; set; }
            public string 得意先Sコード { get; set; }
            public string 得意先Fコード { get; set; }
            public int? 回数 { get; set; }
            public DateTime? 締集計開始日 { get; set; }
            public DateTime? 締集計終了日 { get; set; }
        }

        /// <summary>
        /// TKS08010  CSV　メンバー
        /// </summary>
        [DataContract]
        public class TKS08010_Member_CSV
        {
            public int? 得意先コード { get; set; }
            public string 得意先名 { get; set; }
            public string 会社名ｶﾅ { get; set; }
            public string 親子区分 { get; set; }
            public int? 締日 { get; set; }
            public decimal? 前月残高 { get; set; }
            public decimal? 入金金額 { get; set; }
            public decimal? 入金調整額 { get; set; }
            public decimal? 差引金額 { get; set; }
            public decimal? 売上金額 { get; set; }
            public decimal? 内課税額 { get; set; }
            public decimal? 消費税 { get; set; }
            public decimal? 通行料 { get; set; }
            //public decimal? 当月売上 { get; set; }
            public decimal? 当月合計額 { get; set; }
            public decimal? 繰越金額 { get; set; }
            public decimal? 件数 { get; set; }
            public string 未定 { get; set; }
            public string 作成年月 { get; set; }
            public string 集計区分 { get; set; }
            public string 作成区分 { get; set; }
            public string 表示締日 { get; set; }
            public string 表示順序 { get; set; }
            public string 得意先指定 { get; set; }
            public string 得意先ﾋﾟｯｸｱｯﾌﾟ { get; set; }
        }

        /// <summary>
        /// TKS08010  CSV　メンバー
        /// </summary>
        [DataContract]
        public class TKS08010_Member1_CSV
        {

            public int? 得意先コード { get; set; }
            public string 得意先名 { get; set; }
            public string 会社名ｶﾅ { get; set; }
            public string 親子区分 { get; set; }
            public int? 締日 { get; set; }
            public decimal? 前月残高 { get; set; }
            public decimal? 入金金額 { get; set; }
            public decimal? 入金調整額 { get; set; }
            public decimal? 差引金額 { get; set; }
            public decimal? 売上金額 { get; set; }
            public decimal? 内課税額 { get; set; }
            public decimal? 消費税 { get; set; }
            public decimal? 通行料 { get; set; }
            //public decimal? 当月売上 { get; set; }
            public decimal? 当月合計額 { get; set; }
            public decimal? 繰越金額 { get; set; }
            public decimal? 件数 { get; set; }
            public string 未定 { get; set; }
            public string 作成年月 { get; set; }
            public string 集計区分 { get; set; }
            public string 作成区分 { get; set; }
            public string 表示締日 { get; set; }
            public string 表示順序 { get; set; }
            public string 得意先指定 { get; set; }
            public string 得意先ﾋﾟｯｸｱｯﾌﾟ { get; set; }

        }
        #endregion

        #region 締日帳票印刷
        /// <summary>
        /// TKS08010 締日帳票印刷
        /// </summary>
        /// <param name="p商品ID">得意先コード</param>
        /// <returns>S02</returns>
        public List<TKS08010_Member> SEARCH_TKS08010_SimebiPreview(string p得意先From, string p得意先To, int?[] i得意先List, string p作成締日, string p作成年, string p作成月, DateTime d集計期間From, DateTime d集計期間To, int? 集計 , bool b全締日集計, int 集計区分_CValue, int 作成区分_CValue, int 表示順序_CValue)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<TKS08010_Member> retList = new List<TKS08010_Member>();
                context.Connection.Open();

                //支払先指定　表示用
                string 得意先指定表示 = string.Empty;

                //締日集計処理
                var query = (from m01 in context.M01_TOK
                             join s01 in context.S01_TOKS.Where(c => c.集計年月 == 集計 && c.回数 == 1) on m01.得意先KEY equals s01.得意先KEY into Group
                             where m01.削除日付 == null
                             select new TKS08010_Member
                             {
                                 得意先コード = m01.得意先ID,
                                 得意先名 = m01.略称名,
                                 前月残高 = Group.Sum(c => c.締日前月残高),
                                 入金金額 = Group.Sum(c => c.締日入金現金 + c.締日入金手形),
                                 入金調整額 = Group.Sum(c => c.締日入金その他),
                                 差引金額 = Group.Sum(c => (c.締日前月残高) - (c.締日入金現金 + c.締日入金手形 + c.締日入金その他)),
                                 売上金額 = Group.Sum(c => c.締日売上金額),
                                 通行料 = Group.Sum(c => c.締日通行料),
                                 内課税額 = Group.Sum(c => c.締日課税売上),
                                 消費税 = Group.Sum(c => c.締日消費税),
                                 当月売上 = Group.Sum(c => c.締日売上金額),
                                 内傭車売上 = Group.Sum(c => c.締日内傭車売上),
                                 内傭車料 = Group.Sum(c => c.締日内傭車料),
                                 当月合計額 = Group.Sum(c => c.締日売上金額 + c.締日通行料 + c.締日消費税),
                                 繰越金額 = Group.Sum(c => ((c.締日前月残高) - (c.締日入金現金 + c.締日入金手形 + c.締日入金その他)) + (c.締日売上金額 + c.締日通行料 + c.締日消費税)),
                                 全締日 = p作成締日 == "" ? "全締日" : p作成締日,
                                 親子区分 = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                 Ｔ締日 = m01.Ｔ締日,
                                 締日 = m01.Ｔ締日,
                                 締集計開始日 = d集計期間From,
                                 締集計終了日 = d集計期間To,
                                 集計年月 = 集計,
                                 件数 = Group.Sum(c => c.締日件数),
                                 かな読み = m01.かな読み,
                                 未定 = Group.Count(c => c.締日未定件数 > 0) != 0 ? "未定" : "",
                                 対象年月 = d集計期間To,
                                 締日集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                 作成区分 = 作成区分_CValue == 0 ? "（請求あり得意先のみ）" : "（請求無し得意先含む）",
                                 表示順序 = 表示順序_CValue == 0 ? "得意先ID" : 表示順序_CValue == 1 ? "かな読み" : 表示順序_CValue == 2 ? "前月残高" : 表示順序_CValue == 3 ? "当月売上" : 表示順序_CValue == 4 ? "繰越残高" : "繰越残高",
                                 未定件数 = Group.Sum(c => c.締日未定件数),
                                 得意先指定コード = 得意先指定表示 == "" ? "" : 得意先指定表示,
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
                        int i得意先FROM = AppCommon.IntParse(p得意先From);
                        query = query.Where(c => c.得意先コード >= i得意先FROM);
                    }

                    //得意先To処理　Max値
                    if (!string.IsNullOrEmpty(p得意先To))
                    {
                        int i得意先TO = AppCommon.IntParse(p得意先To);
                        query = query.Where(c => c.得意先コード <= i得意先TO);
                    }

                    //締日処理　
                    if (!string.IsNullOrEmpty(p作成締日))
                    {
                        int? 締日変換 = Convert.ToInt32(p作成締日);
                        query = query.Where(c => c.Ｔ締日 == 締日変換);
                    }

                    //全締日集計処理
                    if (b全締日集計 == true)
                    {
                        query = query.Where(c => c.Ｔ締日 >= 1 && c.Ｔ締日 <= 31);
                    }

                    if (i得意先List.Length > 0)
                    {
                        var intCause = i得意先List;
                        query = query.Union(from m01 in context.M01_TOK
                                            join s01 in context.S01_TOKS.Where(c => c.集計年月 == 集計 && c.回数 == 1) on m01.得意先KEY equals s01.得意先KEY into Group
                                            where m01.削除日付 == null && intCause.Contains(m01.得意先ID)
                                            select new TKS08010_Member
                                            {
                                                得意先コード = m01.得意先ID,
                                                得意先名 = m01.略称名,
                                                前月残高 = Group.Sum(c => c.締日前月残高),
                                                入金金額 = Group.Sum(c => c.締日入金現金 + c.締日入金手形),
                                                入金調整額 = Group.Sum(c => c.締日入金その他),
                                                差引金額 = Group.Sum(c => (c.締日前月残高) - (c.締日入金現金 + c.締日入金手形 + c.締日入金その他)),
                                                売上金額 = Group.Sum(c => c.締日売上金額),
                                                通行料 = Group.Sum(c => c.締日通行料),
                                                内課税額 = Group.Sum(c => c.締日課税売上),
                                                消費税 = Group.Sum(c => c.締日消費税),
                                                当月売上 = Group.Sum(c => c.締日売上金額),
                                                内傭車売上 = Group.Sum(c => c.締日内傭車売上),
                                                内傭車料 = Group.Sum(c => c.締日内傭車料),
                                                当月合計額 = Group.Sum(c => c.締日売上金額 + c.締日通行料 + c.締日消費税),
                                                繰越金額 = Group.Sum(c => ((c.締日前月残高) - (c.締日入金現金 + c.締日入金手形 + c.締日入金その他)) + (c.締日売上金額 + c.締日通行料 + c.締日消費税)),
                                                全締日 = p作成締日 == "" ? "全締日" : p作成締日,
                                                親子区分 = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                                Ｔ締日 = m01.Ｔ締日,
                                                締日 = m01.Ｔ締日,
                                                締集計開始日 = d集計期間From,
                                                締集計終了日 = d集計期間To,
                                                集計年月 = 集計,
                                                件数 = Group.Sum(c => c.締日件数),
                                                かな読み = m01.かな読み,
                                                未定 = Group.Count(c => c.締日未定件数 > 0) != 0 ? "未定" : "",
                                                対象年月 = d集計期間To,
                                                締日集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                                作成区分 = 作成区分_CValue == 0 ? "（請求あり得意先のみ）" : "（請求無し得意先含む）",
                                                表示順序 = 表示順序_CValue == 0 ? "得意先ID" : 表示順序_CValue == 1 ? "かな読み" : 表示順序_CValue == 2 ? "前月残高" : 表示順序_CValue == 3 ? "当月売上" : 表示順序_CValue == 4 ? "繰越残高" : "繰越残高",
                                                未定件数 = Group.Sum(c => c.締日未定件数),
                                                得意先指定コード = 得意先指定表示 == "" ? "" : 得意先指定表示,
                                                得意先Sコード = p得意先From == "" ? "" : p得意先From + " ～ ",
                                                得意先Fコード = p得意先To == "" ? "" : p得意先To,
                                            });

                        //締日処理　
                        if (!string.IsNullOrEmpty(p作成締日))
                        {
                            int? 締日変換 = Convert.ToInt32(p作成締日);
                            query = query.Where(c => c.Ｔ締日 == 締日変換);
                        }

                        //全締日集計処理
                        if (b全締日集計 == true)
                        {
                            query = query.Where(c => c.Ｔ締日 >= 1 && c.Ｔ締日 <= 31);
                        }
                    }
                }
                else
                {

                    //得意先From処理　Min値
                    if (!string.IsNullOrEmpty(p得意先From))
                    {
                        int i得意先FROM = AppCommon.IntParse(p得意先From);
                        query = query.Where(c => c.得意先コード >= i得意先FROM);
                    }

                    //得意先To処理　Max値
                    if (!string.IsNullOrEmpty(p得意先To))
                    {
                        int i得意先TO = AppCommon.IntParse(p得意先To);
                        query = query.Where(c => c.得意先コード <= i得意先TO);
                    }

                    //締日処理　
                    if (!string.IsNullOrEmpty(p作成締日))
                    {
                        int 締日変換 = Convert.ToInt32(p作成締日);
                        query = query.Where(c => c.Ｔ締日 == 締日変換);
                    }

                    //全締日集計処理
                    if (b全締日集計 == true)
                    {
                        query = query.Where(c => c.Ｔ締日 >= 1 && c.Ｔ締日 <= 31);
                    }
                }

                //内訳別表示処理
                //売上あり：0
                //売上なし：1
                if (作成区分_CValue == 0)
                {

                        query = query.Where(c => c.売上金額 != 0 || c.前月残高 != 0);
              
                }

                //表示順序処理
                switch (表示順序_CValue)
                {
                    case 0:
                        query = query.OrderBy(c => c.得意先コード);
                        break;

                    case 1:
                        query = query.OrderBy(c => c.かな読み);
                        break;

                    case 2:
                        query = query.OrderByDescending(c => c.前月残高);
                        break;

                    case 3:
                        query = query.OrderByDescending(c => c.当月売上);
                        break;

                    case 4:
                        query = query.OrderByDescending(c => c.前月残高);
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
                //結果をリスト化
                retList = query.Where(c => c.締集計開始日 != null).ToList();
                return retList;
            }
        }
        #endregion

        #region 月次帳票印刷
        /// <summary>
        /// TKS08010 月次帳票印刷
        /// </summary>
        /// <param name="p商品ID">得意先コード</param>
        /// <returns>S12</returns>
        public List<TKS08010_Member1> SEARCH_TKS08010_GetsujiPreview(string p得意先From, string p得意先To, int?[] i得意先List, string p作成締日, string p作成年, string p作成月, DateTime d集計期間From, DateTime d集計期間To, int? 集計, bool b全締日集計, int 集計区分_CValue, int 作成区分_CValue, int 表示順序_CValue)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<TKS08010_Member1> retList = new List<TKS08010_Member1>();
                context.Connection.Open();

                //支払先指定　表示用
                string 得意先指定表示 = string.Empty;

                //締日集計処理
                var query = (from m01 in context.M01_TOK
                             join s011 in context.S11_TOKG.Where(c => c.集計年月 == 集計 && c.回数 == 1) on m01.得意先KEY equals s011.得意先KEY into Group
                             where m01.削除日付 == null
                             select new TKS08010_Member1
                             {
                                 得意先コード = m01.得意先ID,
                                 得意先名 = m01.略称名,
                                 前月残高 = Group.Sum(c => c.月次前月残高),
                                 入金金額 = Group.Sum(c => c.月次入金現金 + c.月次入金手形),
                                 入金調整額 = Group.Sum(c => c.月次入金その他),
                                 差引金額 = Group.Sum(c => (c.月次前月残高) - (c.月次入金現金 + c.月次入金手形 + c.月次入金その他)),
                                 売上金額 = Group.Sum(c => c.月次売上金額),
                                 通行料 = Group.Sum(c => c.月次通行料),
                                 内課税額 = Group.Sum(c => c.月次課税売上),
                                 消費税 = Group.Sum(c => c.月次消費税),
                                 当月売上 = Group.Sum(c => c.月次売上金額),
                                 内傭車売上 = Group.Sum(c => c.月次内傭車売上),
                                 内傭車料 = Group.Sum(c => c.月次内傭車料),
                                 当月合計額 = Group.Sum(c => c.月次売上金額 + c.月次通行料 + c.月次消費税),
                                 繰越金額 = Group.Sum(c => ((c.月次前月残高) - (c.月次入金現金 + c.月次入金手形 + c.月次入金その他)) + (c.月次売上金額 + c.月次通行料 + c.月次消費税)),
                                 全締日 = p作成締日 == "" ? "全締日" : p作成締日,
                                 親子区分 = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                 Ｔ締日 = m01.Ｔ締日,
                                 締日 = m01.Ｔ締日,
                                 締集計開始日 = d集計期間From,
                                 締集計終了日 = d集計期間To,
                                 集計年月 = 集計,
                                 件数 = Group.Sum(c => c.月次件数),
                                 かな読み = m01.かな読み,
                                 未定 = Group.Count(c => c.月次未定件数 > 0) != 0 ? "未定" : "",
                                 対象年月 = d集計期間To,
                                 締日集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                 作成区分 = 作成区分_CValue == 0 ? "（請求あり得意先のみ）" : "（請求無し得意先含む）",
                                 表示順序 = 表示順序_CValue == 0 ? "得意先ID" : 表示順序_CValue == 1 ? "かな読み" : 表示順序_CValue == 2 ? "前月残高" : 表示順序_CValue == 3 ? "当月売上" : 表示順序_CValue == 4 ? "繰越残高" : "繰越残高",
                                 未定件数 = Group.Sum(c => c.月次未定件数),
                                 得意先指定コード = 得意先指定表示 == "" ? "" : 得意先指定表示,
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
                        int i支払先FROM = AppCommon.IntParse(p得意先From);
                        query = query.Where(c => c.得意先コード >= i支払先FROM);
                    }

                    //得意先To処理　Max値
                    if (!string.IsNullOrEmpty(p得意先To))
                    {
                        int i支払先TO = AppCommon.IntParse(p得意先To);
                        query = query.Where(c => c.得意先コード <= i支払先TO);
                    }

                    //締日処理　
                    if (!string.IsNullOrEmpty(p作成締日))
                    {
                        int 締日変換 = Convert.ToInt32(p作成締日);
                        query = query.Where(c => c.Ｔ締日 == 締日変換);
                    }

                    //全締日集計処理
                    if (b全締日集計 == true)
                    {
                        query = query.Where(c => c.Ｔ締日 >= 1 && c.Ｔ締日 <= 31);

                    }

                    if (i得意先List.Length > 0)
                    {
                        var intCause = i得意先List;
                        query = query.Union(from m01 in context.M01_TOK
                                            join s011 in context.S11_TOKG.Where(c => c.集計年月 == 集計 && c.回数 == 1) on m01.得意先KEY equals s011.得意先KEY into Group
                                            where intCause.Contains(m01.得意先ID) && m01.削除日付 == null
                                            select new TKS08010_Member1
                                            {
                                                得意先コード = m01.得意先ID,
                                                得意先名 = m01.略称名,
                                                前月残高 = Group.Sum(c => c.月次前月残高),
                                                入金金額 = Group.Sum(c => c.月次入金現金 + c.月次入金手形),
                                                入金調整額 = Group.Sum(c => c.月次入金その他),
                                                差引金額 = Group.Sum(c => (c.月次前月残高) - (c.月次入金現金 + c.月次入金手形 + c.月次入金その他)),
                                                売上金額 = Group.Sum(c => c.月次売上金額),
                                                通行料 = Group.Sum(c => c.月次通行料),
                                                内課税額 = Group.Sum(c => c.月次課税売上),
                                                消費税 = Group.Sum(c => c.月次消費税),
                                                当月売上 = Group.Sum(c => c.月次売上金額),
                                                内傭車売上 = Group.Sum(c => c.月次内傭車売上),
                                                内傭車料 = Group.Sum(c => c.月次内傭車料),
                                                当月合計額 = Group.Sum(c => c.月次売上金額 + c.月次通行料 + c.月次消費税),
                                                繰越金額 = Group.Sum(c => ((c.月次前月残高) - (c.月次入金現金 + c.月次入金手形 + c.月次入金その他)) + (c.月次売上金額 + c.月次通行料 + c.月次消費税)),
                                                全締日 = p作成締日 == "" ? "全締日" : p作成締日,
                                                親子区分 = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                                Ｔ締日 = m01.Ｔ締日,
                                                締日 = m01.Ｔ締日,
                                                締集計開始日 = d集計期間From,
                                                締集計終了日 = d集計期間To,
                                                集計年月 = 集計,
                                                件数 = Group.Sum(c => c.月次件数),
                                                かな読み = m01.かな読み,
                                                未定 = Group.Count(c => c.月次未定件数 > 0) != 0 ? "未定" : "",
                                                対象年月 = d集計期間To,
                                                締日集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                                作成区分 = 作成区分_CValue == 0 ? "（請求あり得意先のみ）" : "（請求無し得意先含む）",
                                                表示順序 = 表示順序_CValue == 0 ? "得意先ID" : 表示順序_CValue == 1 ? "かな読み" : 表示順序_CValue == 2 ? "前月残高" : 表示順序_CValue == 3 ? "当月売上" : 表示順序_CValue == 4 ? "繰越残高" : "繰越残高",
                                                未定件数 = Group.Sum(c => c.月次未定件数),
                                                得意先指定コード = 得意先指定表示 == "" ? "" : 得意先指定表示,
                                                得意先Sコード = p得意先From == "" ? "" : p得意先From + " ～ ",
                                                得意先Fコード = p得意先To == "" ? "" : p得意先To, 
                                            });

                        //締日処理　
                        if (!string.IsNullOrEmpty(p作成締日))
                        {
                            int 締日変換 = Convert.ToInt32(p作成締日);
                            query = query.Where(c => c.Ｔ締日 == 締日変換);
                        }

                        //全締日集計処理
                        if (b全締日集計 == true)
                        {
                            query = query.Where(c => c.Ｔ締日 >= 1 && c.Ｔ締日 <= 31);
                        }
                    }
                }
                else
                {
                    //締日処理　
                    if (!string.IsNullOrEmpty(p作成締日))
                    {
                        int 締日変換 = Convert.ToInt32(p作成締日);
                        query = query.Where(c => c.Ｔ締日 == 締日変換);
                    }
                    //全締日集計処理
                    if (b全締日集計 == true)
                    {
                        query = query.Where(c => c.Ｔ締日 >= 1 && c.Ｔ締日 <= 31);
                    }
                }

                //内訳別表示処理
                //売上あり：0
                //売上なし：1
                switch (作成区分_CValue)
                {
                    case 0:
                        query = query.Where(c => c.売上金額 != 0 || c.前月残高 != 0);
                        break;

                    case 1:
                        query = query.Where(c => c.売上金額 != 0);
                        break;

                    default:
                        break;
                }

                //表示順序処理
                switch (表示順序_CValue)
                {
                    case 0:
                        query = query.OrderBy(c => c.得意先コード);
                        break;

                    case 1:
                        query = query.OrderBy(c => c.かな読み);
                        break;

                    case 2:
                        query = query.OrderByDescending(c => c.前月残高);
                        break;

                    case 3:
                        query = query.OrderByDescending(c => c.当月合計額);
                        break;

                    case 4:
                        query = query.OrderByDescending(c => c.前月残高);
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
                //結果をリスト化
                retList = query.Where(c => c.締集計開始日 != null).ToList();
                return retList;
            }
        }
        #endregion

        #region 締日CSV出力
        /// <summary>
        /// TKS08010 締日CSV出力
        /// </summary>
        /// <param name="p商品ID">得意先コード</param>
        /// <returns>S12</returns>
        public List<TKS08010_Member_CSV> SEARCH_TKS08010_SimebiCSV(string p得意先From, string p得意先To, int?[] i得意先List, string p作成締日, string p作成年, string p作成月, DateTime d集計期間From, DateTime d集計期間To, int? 集計 , bool b全締日集計, int 集計区分_CValue, int 作成区分_CValue, int 表示順序_CValue)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<TKS08010_Member_CSV> retList = new List<TKS08010_Member_CSV>();
                context.Connection.Open();

                //支払先指定　　表示用
                string 得意先指定表示 = string.Empty;

                //締日集計処理
                var query = (from m01 in context.M01_TOK
                             join s01 in context.S01_TOKS.Where(c => c.集計年月 == 集計 && c.回数 == 1) on m01.得意先KEY equals s01.得意先KEY into Group
                             where m01.削除日付 == null
                             select new TKS08010_Member_CSV
                             {
                                 得意先コード = m01.得意先ID,
                                 得意先名 = m01.略称名,
                                 前月残高 = Group.Sum(c => c.締日前月残高),
                                 入金金額 = Group.Sum(c => c.締日入金現金 + c.締日入金手形),
                                 入金調整額 = Group.Sum(c => c.締日入金その他),
                                 差引金額 = Group.Sum(c => (c.締日前月残高) - (c.締日入金現金 + c.締日入金手形 + c.締日入金その他)),
                                 売上金額 = Group.Sum(c => c.締日売上金額),
                                 通行料 = Group.Sum(c => c.締日通行料),
                                 内課税額 = Group.Sum(c => c.締日課税売上),
                                 消費税 = Group.Sum(c => c.締日消費税),
                                 //当月売上 = Group.Sum(c => c.締日売上金額),
                                 当月合計額 = Group.Sum(c => c.締日売上金額 + c.締日通行料 + c.締日消費税),
                                 繰越金額 = Group.Sum(c => ((c.締日前月残高) - (c.締日入金現金 + c.締日入金手形 + c.締日入金その他)) + (c.締日売上金額 + c.締日通行料 + c.締日消費税)),
                                 表示締日 = p作成締日 == "" ? "全締日" : p作成締日,
                                 親子区分 = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                 締日 = m01.Ｔ締日,
                                 件数 = Group.Sum(c => c.締日件数),
                                 会社名ｶﾅ = m01.かな読み,
                                 得意先指定 = p得意先From + "～" + p得意先To,
                                 未定 = Group.Count(c => c.締日未定件数 > 0) != 0 ? "未定" : "",
                                 作成年月 = p作成年 + "年" + p作成月 + "月",
                                 集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                 作成区分 = 作成区分_CValue == 0 ? "（請求あり得意先のみ）" : "（請求無し得意先含む）",
                                 表示順序 = 表示順序_CValue == 0 ? "得意先ID" : 表示順序_CValue == 1 ? "かな読み" : 表示順序_CValue == 2 ? "前月残高" : 表示順序_CValue == 3 ? "当月売上" : 表示順序_CValue == 4 ? "繰越残高" : "繰越残高",
                                 得意先ﾋﾟｯｸｱｯﾌﾟ = 得意先指定表示 == "" ? "" : 得意先指定表示,
                                 
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
                    if (b全締日集計 == true)
                    {
                        query = query.Where(c => c.締日 >= 1 && c.締日 <= 31);
                    }

                    //締日処理　
                    if (!string.IsNullOrEmpty(p作成締日))
                    {
                        int 締日変換 = Convert.ToInt32(p作成締日);
                        query = query.Where(c => c.締日 == 締日変換);
                    }

                    if (i得意先List.Length > 0)
                    {
                        var intCause = i得意先List;
						query = query.Union(from m01 in context.M01_TOK
											join s01 in context.S01_TOKS.Where(c => c.集計年月 == 集計 && c.回数 == 1) on m01.得意先KEY equals s01.得意先KEY into Group
											where m01.削除日付 == null && intCause.Contains(m01.得意先ID)
											 select new TKS08010_Member_CSV
											 {
												 得意先コード = m01.得意先ID,
												 得意先名 = m01.略称名,
												 前月残高 = Group.Sum(c => c.締日前月残高),
                                                 入金金額 = Group.Sum(c => c.締日入金現金 + c.締日入金手形),
												 入金調整額 = Group.Sum(c => c.締日入金その他),
												 差引金額 = Group.Sum(c => (c.締日前月残高) - (c.締日入金現金 + c.締日入金手形 + c.締日入金その他)),
												 売上金額 = Group.Sum(c => c.締日売上金額),
												 通行料 = Group.Sum(c => c.締日通行料),
												 内課税額 = Group.Sum(c => c.締日課税売上),
												 消費税 = Group.Sum(c => c.締日消費税),
												 //当月売上 = Group.Sum(c => c.締日売上金額),
												 当月合計額 = Group.Sum(c => c.締日売上金額 + c.締日通行料 + c.締日消費税),
												 繰越金額 = Group.Sum(c => ((c.締日前月残高) - (c.締日入金現金 + c.締日入金手形 + c.締日入金その他)) + (c.締日売上金額 + c.締日通行料 + c.締日消費税)),
												 表示締日 = p作成締日 == "" ? "全締日" : p作成締日,
												 親子区分 = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
												 締日 = m01.Ｔ締日,
												 件数 = Group.Sum(c => c.締日件数),
												 会社名ｶﾅ = m01.かな読み,
												 得意先指定 = p得意先From + "～" + p得意先To,
												 未定 = Group.Count(c => c.締日未定件数 > 0) != 0 ? "未定" : "",
												 作成年月 = p作成年 + "年" + p作成月 + "月",
												 集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
												 作成区分 = 作成区分_CValue == 0 ? "（請求あり得意先のみ）" : "（請求無し得意先含む）",
												 表示順序 = 表示順序_CValue == 0 ? "得意先ID" : 表示順序_CValue == 1 ? "かな読み" : 表示順序_CValue == 2 ? "前月残高" : 表示順序_CValue == 3 ? "当月売上" : 表示順序_CValue == 4 ? "繰越残高" : "繰越残高",
												 得意先ﾋﾟｯｸｱｯﾌﾟ = 得意先指定表示 == "" ? "" : 得意先指定表示,

                                 
											 });

                        //締日処理　
                        if (!string.IsNullOrEmpty(p作成締日))
                        {
                            int 締日変換 = Convert.ToInt32(p作成締日);
                            query = query.Where(c => c.締日 == 締日変換);
                        }

                        //全締日集計処理
                        if (b全締日集計 == true)
                        {
                            query = query.Where(c => c.締日 >= 1 && c.締日 <= 31);
                        }
                    }
                }
                else
                {
                    //締日処理　
                    if (!string.IsNullOrEmpty(p作成締日))
                    {
                        int 締日変換 = Convert.ToInt32(p作成締日);
                        query = query.Where(c => c.締日 == 締日変換);
                    }

                    //全締日集計処理
                    if (b全締日集計 == true)
                    {
                        query = query.Where(c => c.締日 >= 1 && c.締日 <= 31);
                    }
                }

                //内訳別表示処理
                //売上あり：0
                //売上なし：1
                switch (作成区分_CValue)
                {
                    case 0:
                        query = query.Where(c => c.売上金額 != 0 || c.前月残高 != 0);
                        break;

                    case 1:
                        query = query.Where(c => c.売上金額 != 0);
                        break;

                    default:
                        break;
                }

                //表示順序処理
                switch (表示順序_CValue)
                {
                    case 0:
                        query = query.OrderBy(c => c.得意先コード);
                        break;

                    case 1:
                        query = query.OrderBy(c => c.会社名ｶﾅ);
                        break;

                    case 2:
                        query = query.OrderByDescending(c => c.前月残高);
                        break;

                    case 3:
                        query = query.OrderByDescending(c => c.当月合計額);
                        break;

                    case 4:
                        query = query.OrderByDescending(c => c.前月残高);
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
                //結果をリスト化
                retList = query.Where(c => c.得意先コード > 0).ToList();
                return retList;
            }
        }
        #endregion

        #region 月次CSV出力
        /// <summary>
        /// TKS08010 月次CSV出力
        /// </summary>
        /// <param name="p商品ID">得意先コード</param>
        /// <returns>S12</returns>
        public List<TKS08010_Member1_CSV> SEARCH_TKS08010_GetsujiCSV(string p得意先From, string p得意先To, int?[] i得意先List, string p作成締日, string p作成年, string p作成月, DateTime d集計期間From, DateTime d集計期間To, int? 集計 , bool b全締日集計, int 集計区分_CValue, int 作成区分_CValue, int 表示順序_CValue)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<TKS08010_Member1_CSV> retList = new List<TKS08010_Member1_CSV>();
                context.Connection.Open();

                //支払先指定　表示用
                string 得意先指定表示 = string.Empty;

                //締日集計処理
				var query = (from m01 in context.M01_TOK
							 join s011 in context.S11_TOKG.Where(c => c.集計年月 == 集計 && c.回数 == 1) on m01.得意先KEY equals s011.得意先KEY into Group
							 where m01.削除日付 == null
                             select new TKS08010_Member1_CSV
                             {
                                 得意先コード = m01.得意先ID,
                                 得意先名 = m01.略称名,
                                 前月残高 = Group.Sum(c => c.月次前月残高),
                                 入金金額 = Group.Sum(c => c.月次入金現金 + c.月次入金手形),
                                 入金調整額 = Group.Sum(c => c.月次入金その他),
                                 差引金額 = Group.Sum(c => (c.月次前月残高) - (c.月次入金現金 + c.月次入金手形 + c.月次入金その他)),
                                 売上金額 = Group.Sum(c => c.月次売上金額),
                                 通行料 = Group.Sum(c => c.月次通行料),
                                 内課税額 = Group.Sum(c => c.月次課税売上),
                                 消費税 = Group.Sum(c => c.月次消費税),
                                 //当月売上 = Group.Sum(c => c.月次売上金額),
                                 当月合計額 = Group.Sum(c => c.月次売上金額 + c.月次通行料 + c.月次消費税),
                                 繰越金額 = Group.Sum(c => ((c.月次前月残高) - (c.月次入金現金 + c.月次入金手形 + c.月次入金その他)) + (c.月次売上金額 + c.月次通行料 + c.月次消費税)),
                                 表示締日 = p作成締日 == "" ? "全締日" : p作成締日,
                                 親子区分 = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                 締日 = m01.Ｔ締日,
                                 件数 = Group.Sum(c => c.月次件数),
                                 会社名ｶﾅ = m01.かな読み,
                                 得意先指定 = p得意先From + "～" + p得意先To,
                                 未定 = Group.Count(c => c.月次未定件数 > 0) != 0 ? "未定" : "",
                                 作成年月 = p作成年 + "年" + p作成月 + "月",
                                 集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                 作成区分 = 作成区分_CValue == 0 ? "（請求あり得意先のみ）" : "（請求無し得意先含む）",
                                 表示順序 = 表示順序_CValue == 0 ? "得意先ID" : 表示順序_CValue == 1 ? "かな読み" : 表示順序_CValue == 2 ? "前月残高" : 表示順序_CValue == 3 ? "当月売上" : 表示順序_CValue == 4 ? "繰越残高" : "繰越残高",
                                 得意先ﾋﾟｯｸｱｯﾌﾟ = 得意先指定表示 == "" ? "" : 得意先指定表示,

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

                    //締日処理　
                    if (!string.IsNullOrEmpty(p作成締日))
                    {
                        int 締日変換 = Convert.ToInt32(p作成締日);
                        query = query.Where(c => c.締日 == 締日変換);
                    }

                    //全締日集計処理
                    if (b全締日集計 == true)
                    {
                        query = query.Where(c => c.締日 >= 1 && c.締日 <= 31);

                    }

                    if (i得意先List.Length > 0)
                    {
                        var intCause = i得意先List;
						query = query.Union(from m01 in context.M01_TOK
											join s011 in context.S11_TOKG.Where(c => c.集計年月 == 集計 && c.回数 == 1) on m01.得意先KEY equals s011.得意先KEY into Group
											where intCause.Contains(m01.得意先ID) && m01.削除日付 == null
                                            select new TKS08010_Member1_CSV
                                            {
                                                得意先コード = m01.得意先ID,
                                                得意先名 = m01.略称名,
                                                前月残高 = Group.Sum(c => c.月次前月残高),
                                                入金金額 = Group.Sum(c => c.月次入金現金 + c.月次入金手形),
                                                入金調整額 = Group.Sum(c => c.月次入金その他),
                                                差引金額 = Group.Sum(c => (c.月次前月残高) - (c.月次入金現金 + c.月次入金手形 + c.月次入金その他)),
                                                売上金額 = Group.Sum(c => c.月次売上金額),
                                                通行料 = Group.Sum(c => c.月次通行料),
                                                内課税額 = Group.Sum(c => c.月次課税売上),
                                                消費税 = Group.Sum(c => c.月次消費税),
                                                //当月売上 = Group.Sum(c => c.月次売上金額),
                                                当月合計額 = Group.Sum(c => c.月次売上金額 + c.月次通行料 + c.月次消費税),
                                                繰越金額 = Group.Sum(c => ((c.月次前月残高) - (c.月次入金現金 + c.月次入金手形 + c.月次入金その他)) + (c.月次売上金額 + c.月次通行料 + c.月次消費税)),
                                                表示締日 = p作成締日 == "" ? "全締日" : p作成締日,
                                                親子区分 = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                                締日 = m01.Ｔ締日,
                                                件数 = Group.Sum(c => c.月次件数),
                                                会社名ｶﾅ = m01.かな読み,
                                                得意先指定 = p得意先From + "～" + p得意先To,
                                                未定 = Group.Count(c => c.月次未定件数 > 0) != 0 ? "未定" : "",
                                                作成年月 = p作成年 + "年" + p作成月 + "月",
                                                集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                                作成区分 = 作成区分_CValue == 0 ? "（請求あり得意先のみ）" : "（請求無し得意先含む）",
                                                表示順序 = 表示順序_CValue == 0 ? "得意先ID" : 表示順序_CValue == 1 ? "かな読み" : 表示順序_CValue == 2 ? "前月残高" : 表示順序_CValue == 3 ? "当月売上" : 表示順序_CValue == 4 ? "繰越残高" : "繰越残高",
                                                得意先ﾋﾟｯｸｱｯﾌﾟ = 得意先指定表示 == "" ? "" : 得意先指定表示,

                                 
                                            });

                        //締日処理　
                        if (!string.IsNullOrEmpty(p作成締日))
                        {
                            int 締日変換 = Convert.ToInt32(p作成締日);
                            query = query.Where(c => c.締日 == 締日変換);
                        }

                        //全締日集計処理
                        if (b全締日集計 == true)
                        {
                            query = query.Where(c => c.締日 >= 1 && c.締日 <= 31);
                        }
                    }
                }
                else
                {
                    //締日処理　
                    if (!string.IsNullOrEmpty(p作成締日))
                    {
                        int 締日変換 = Convert.ToInt32(p作成締日);
                        query = query.Where(c => c.締日 == 締日変換);
                    }

                    //全締日集計処理
                    if (b全締日集計 == true)
                    {
                        query = query.Where(c => c.締日 >= 1 && c.締日 <= 31);
                    }
                }

                //内訳別表示処理
                //売上あり：0
                //売上なし：1
                switch (作成区分_CValue)
                {
                    //支払取引全体
                    case 0:
                        query = query.Where(c => c.売上金額 != 0 || c.前月残高 != 0);
                        break;

                    //支払先
                    case 1:
                        query = query.Where(c => c.売上金額 != 0);
                        break;

                    default:
                        break;
                }

                //表示順序処理
                switch (表示順序_CValue)
                {
                    case 0:
                        query = query.OrderBy(c => c.得意先コード);
                        break;

                    case 1:
                        query = query.OrderBy(c => c.会社名ｶﾅ);
                        break;

                    case 2:
                        query = query.OrderByDescending(c => c.前月残高);
                        break;

                    case 3:
                        query = query.OrderByDescending(c => c.当月合計額);
                        break;

                    case 4:
                        query = query.OrderByDescending(c => c.前月残高);
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
                //結果をリスト化
                retList = query.Where(c => c.得意先コード > 0).ToList();
                return retList;
            }
        }
        #endregion
    }
}