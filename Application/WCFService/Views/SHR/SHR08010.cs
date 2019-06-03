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
    #region Member

    /// <summary>
    /// SHR08010  印刷　メンバー
    /// </summary>
    public class SHR08010_Member
    {

        [DataMember]
        public int 支払先コード { get; set; }
        [DataMember]
        public string 支払先名 { get; set; }
        [DataMember]
        public string 会社名ｶﾅ { get; set; }
        [DataMember]
        public string 親子区分名 { get; set; }
        [DataMember]
        public int 締日 { get; set; }
        [DataMember]
        public decimal? 前月残高 { get; set; }
        [DataMember]
        public decimal? 出金金額 { get; set; }
        [DataMember]
        public decimal? 出金調整額 { get; set; }
        [DataMember]
        public decimal? 差引金額 { get; set; }
        [DataMember]
        public decimal? 当月支払 { get; set; }
        [DataMember]
        public decimal? 内課税額 { get; set; }
        [DataMember]
        public decimal? 消費税 { get; set; }
        [DataMember]
        public decimal? 通行料 { get; set; }
        [DataMember]
        public decimal? 当月合計額 { get; set; }
        [DataMember]
        public decimal? 繰越金額 { get; set; }
        [DataMember]
        public decimal? 件数 { get; set; }
        [DataMember]
        public string 未定 { get; set; }
        [DataMember]
        public string 作成年月 { get; set; }
        [DataMember]
        public string 集計区分 { get; set; }
        [DataMember]
        public string 表示区分 { get; set; }
        [DataMember]
        public string 表示締日 { get; set; }
        [DataMember]
        public string 表示順序 { get; set; }
        [DataMember]
        public string 支払先指定 { get; set; }
        [DataMember]
        public string 支払先ﾋﾟｯｸｱｯﾌﾟ { get; set; }

    }

    #endregion

    public class SHR08010
    {

        #region 印刷 SHR08010 支払一覧表(***締日データ***)

        ////<summary>
        ////SHR08010 印刷 支払一覧表
        ////</summary>
        ////<param name="p商品ID">支払先コード</param>
        ////<returns>T01</returns>
        public List<SHR08010_Member> SHR08010_GetDataHinList(string p支払先From, string p支払先To, int?[] i支払先List, string p作成締日, bool 全締日集計, int i集計年月, int i集計区分, int i表示区分, string i作成年, string i作成月, int i表示順序)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                string 支払先指定ﾋﾟｯｸｱｯﾌﾟ = string.Empty;
                List<SHR08010_Member> retList = new List<SHR08010_Member>();
                context.Connection.Open();

                var query = (from m01 in context.M01_TOK
                             join s02 in context.S02_YOSS.Where(c => c.集計年月 == i集計年月 && c.回数 == 1) on m01.得意先KEY equals s02.支払先KEY into Group
                             where m01.削除日付 == null
                             select new SHR08010_Member
                             {
                                 支払先コード = m01.得意先ID,
                                 支払先名 = m01.略称名,
                                 会社名ｶﾅ = m01.かな読み,
                                 親子区分名 = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                 締日 = m01.Ｓ締日,
                                 前月残高 = Group.Sum(c => c.締日前月残高),
                                 出金金額 = Group.Sum(c => c.締日入金現金 + c.締日入金手形),
                                 出金調整額 = Group.Sum(c => c.締日入金その他),
                                 差引金額 = Group.Sum(c => (c.締日前月残高) - (c.締日入金現金 + c.締日入金手形 + c.締日入金その他)),
                                 当月支払 = Group.Sum(c => c.締日売上金額),
                                 内課税額 = Group.Sum(c => c.締日課税売上),
                                 消費税 = Group.Sum(c => c.締日消費税),
                                 通行料 = Group.Sum(c => c.締日通行料),
                                 当月合計額 = Group.Sum(c => c.締日売上金額 + c.締日通行料 + c.締日消費税),
                                 繰越金額 = Group.Sum(c => ((c.締日前月残高) - (c.締日入金現金 + c.締日入金手形 + c.締日入金その他)) + (c.締日売上金額 + c.締日通行料 + c.締日消費税)),
                                 件数 = Group.Sum(c => c.締日件数),
                                 作成年月 = i作成年 + "年" + i作成月 + "年度",
                                 支払先指定 = p支払先From + "～" + p支払先To,
                                 未定 = Group.Count(c => c.締日未定件数 > 0) != 0 ? "未定" : "",
                                 集計区分 = i集計区分 == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                 表示区分 = i表示区分 == 0 ? "(支払あり支払先のみ)" : "(支払無し支払先含む)",
                                 表示締日 = 全締日集計 == true ? "全締日" : p作成締日,
                                 表示順序 = i表示順序 == 0 ? "支払先ID" : i表示順序 == 1 ? "ｶﾅ読み" : i表示順序 == 2 ? "前月残高" : i表示順序 == 3 ? "当月支払" : "繰越金額",
                                 支払先ﾋﾟｯｸｱｯﾌﾟ = 支払先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "" : 支払先指定ﾋﾟｯｸｱｯﾌﾟ,
                             }).AsQueryable();
             
                //***検索条件***//
                if (!(string.IsNullOrEmpty(p支払先From + p支払先To) && i支払先List.Length == 0))
                {
                    //From & ToがNULLだった場合
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

                    //締日処理
                    if (全締日集計 == true)
                    {
                        query = query.Where(c => c.締日 >= 1 && c.締日 <= 31);
                    }
                    else
                    {
                        int i作成締日 = AppCommon.IntParse(p作成締日);
                        query = query.Where(c => c.締日 == i作成締日);
                    }

                    if (i支払先List.Length > 0)
                    {
                        var intCause = i支払先List;
                        query = query.Union(from m01 in context.M01_TOK
                                            join s02 in context.S02_YOSS.Where(c => c.集計年月 == i集計年月 && c.回数 == 1) on m01.得意先KEY equals s02.支払先KEY into Group
                                     where m01.削除日付 == null && intCause.Contains(m01.得意先ID)
                                     select new SHR08010_Member
                                     {
                                         支払先コード = m01.得意先ID,
                                         支払先名 = m01.略称名,
                                         会社名ｶﾅ = m01.かな読み,
                                         親子区分名 = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                         締日 = m01.Ｓ締日,
                                         前月残高 = Group.Sum(c => c.締日前月残高),
                                         出金金額 = Group.Sum(c => c.締日入金現金 + c.締日入金手形),
                                         出金調整額 = Group.Sum(c => c.締日入金その他),
                                         差引金額 = Group.Sum(c => (c.締日前月残高) - (c.締日入金現金 + c.締日入金手形 + c.締日入金その他)),
                                         当月支払 = Group.Sum(c => c.締日売上金額),
                                         内課税額 = Group.Sum(c => c.締日課税売上),
                                         消費税 = Group.Sum(c => c.締日消費税),
                                         通行料 = Group.Sum(c => c.締日通行料),
                                         当月合計額 = Group.Sum(c => c.締日売上金額 + c.締日通行料 + c.締日消費税),
                                         繰越金額 = Group.Sum(c => ((c.締日前月残高) - (c.締日入金現金 + c.締日入金手形 + c.締日入金その他)) + (c.締日売上金額 + c.締日通行料 + c.締日消費税)),
                                         件数 = Group.Sum(c => c.締日件数),
                                         作成年月 = i作成年 + "年" + i作成月 + "年度",
                                         支払先指定 = p支払先From + "～" + p支払先To,
                                         未定 = Group.Count(c => c.締日未定件数 > 0) != 0 ? "未定" : "",
                                         集計区分 = i集計区分 == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                         表示区分 = i表示区分 == 0 ? "(支払あり支払先のみ)" : "(支払無し支払先含む)",
                                         表示締日 = 全締日集計 == true ? "全締日" : p作成締日,
                                         表示順序 = i表示順序 == 0 ? "支払先ID" : i表示順序 == 1 ? "ｶﾅ読み" : i表示順序 == 2 ? "前月残高" : i表示順序 == 3 ? "当月支払" : "繰越金額",
                                         支払先ﾋﾟｯｸｱｯﾌﾟ = 支払先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "" : 支払先指定ﾋﾟｯｸｱｯﾌﾟ,
                                     }).AsQueryable();

                    }
                }
                else
                {
                    //支払先From処理　Min値
                    if (!string.IsNullOrEmpty(p支払先From))
                    {
                        int i支払先FROM = AppCommon.IntParse(p支払先From);
                        query = query.Where(c => c.支払先コード >= int.MinValue);
                    }

                    //支払先To処理　Max値
                    if (!string.IsNullOrEmpty(p支払先To))
                    {
                        int i支払先TO = AppCommon.IntParse(p支払先To);
                        query = query.Where(c => c.支払先コード <= int.MaxValue);
                    }

                    //締日処理
                    if (全締日集計 == true)
                    {
                        query = query.Where(c => c.締日 >= 1 && c.締日 <= 31);
                    }
                    else
                    {
                        int i作成締日 = AppCommon.IntParse(p作成締日);
                        query = query.Where(c => c.締日 == i作成締日);
                    }
                }

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

                //支払予定無し支払先のみ
                if (i表示区分 == 0)
                {
                    query = query.Where(c => c.当月支払 != 0 || c.前月残高 != 0);
                }

                //表示順序1
                switch (i表示順序)
                {
                    case 0://支払先コード
                        query = query.OrderBy(order => order.支払先コード);
                        break;

                    case 1://会社名ｶﾅ
                        query = query.OrderBy(order => order.会社名ｶﾅ);
                        break;

                    case 2://前月残高
                        query = query.OrderByDescending(order => order.前月残高);
                        break;

                    case 3://当月支払
                        query = query.OrderByDescending(order => order.当月支払);
                        break;

                    case 4://繰越金額
                        query = query.OrderByDescending(order => order.繰越金額);
                        break;
                }

                retList = query.Where(c => c.支払先コード > 0).ToList();
                return retList;
            }
        }
        #endregion

        #region 印刷 SHR08010 支払一覧表(***月次データ***)

        ////<summary>
        ////SHR08010 印刷 支払一覧表
        ////</summary>
        ////<param name="p商品ID">支払先コード</param>
        ////<returns>T01</returns>
        public List<SHR08010_Member> SHR08010_GetDataHinList2(string p支払先From, string p支払先To, int?[] i支払先List, string p作成締日, bool 全締日集計, int i集計年月, int i集計区分, int i表示区分, string i作成年, string i作成月, int i表示順序)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                string 支払先指定ﾋﾟｯｸｱｯﾌﾟ = string.Empty;
                List<SHR08010_Member> retList = new List<SHR08010_Member>();
                context.Connection.Open();

                var query = (from m01 in context.M01_TOK
                             join s12 in context.S12_YOSG.Where(c => c.集計年月 == i集計年月 && c.回数 == 1) on m01.得意先KEY equals s12.支払先KEY into Group
                             where m01.削除日付 == null
                             select new SHR08010_Member
                             {
                                 支払先コード = m01.得意先ID,
                                 支払先名 = m01.略称名,
                                 会社名ｶﾅ = m01.かな読み,
                                 親子区分名 = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                 締日 = m01.Ｓ締日,
                                 前月残高 = Group.Sum(c => c.月次前月残高),
                                 出金金額 = Group.Sum(c => c.月次入金現金 + c.月次入金手形),
                                 出金調整額 = Group.Sum(c => c.月次入金その他),
                                 差引金額 = Group.Sum(c => (c.月次前月残高) - (c.月次入金現金 + c.月次入金手形 + c.月次入金その他)),
                                 当月支払 = Group.Sum(c => c.月次売上金額),
                                 内課税額 = Group.Sum(c => c.月次課税売上),
                                 消費税 = Group.Sum(c => c.月次消費税),
                                 通行料 = Group.Sum(c => c.月次通行料),
                                 当月合計額 = Group.Sum(c => c.月次売上金額 + c.月次通行料 + c.月次消費税),
                                 繰越金額 = Group.Sum(c => ((c.月次前月残高) - (c.月次入金現金 + c.月次入金手形 + c.月次入金その他)) + (c.月次売上金額 + c.月次通行料 + c.月次消費税)),
                                 件数 = Group.Sum(c => c.月次件数),
                                 作成年月 = i作成年 + "年" + i作成月 + "年度",
                                 支払先指定 = p支払先From + "～" + p支払先To,
                                 未定 = Group.Count(c => c.月次未定件数 > 0) != 0 ? "未定" : "",
                                 集計区分 = i集計区分 == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                 表示区分 = i表示区分 == 0 ? "(支払あり支払先のみ)" : "(支払無し支払先含む)",
                                 表示締日 = 全締日集計 == true ? "全締日" : p作成締日,
                                 表示順序 = i表示順序 == 0 ? "支払先ID" : i表示順序 == 1 ? "ｶﾅ読み" : i表示順序 == 2 ? "前月残高" : i表示順序 == 3 ? "当月支払" : "繰越金額",
                                 支払先ﾋﾟｯｸｱｯﾌﾟ = 支払先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "" : 支払先指定ﾋﾟｯｸｱｯﾌﾟ,
                             }).AsQueryable();
           

                //***検索条件***//
                if (!(string.IsNullOrEmpty(p支払先From + p支払先To) && i支払先List.Length == 0))
                {
                    //From & ToがNULLだった場合
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

                    //締日処理
                    if (全締日集計 == true)
                    {
                        query = query.Where(c => c.締日 >= 1 && c.締日 <= 31);
                    }
                    else
                    {
                        int i作成締日 = AppCommon.IntParse(p作成締日);
                        query = query.Where(c => c.締日 == i作成締日);
                    }

                    if (i支払先List.Length > 0)
                    {
                        var intCause = i支払先List;
                        query = query.Union(from m01 in context.M01_TOK
                                            join s12 in context.S12_YOSG.Where(c => c.集計年月 == i集計年月 && c.回数 == 1) on m01.得意先KEY equals s12.支払先KEY into Group
                                            where m01.削除日付 == null && intCause.Contains(m01.得意先ID)
                                            select new SHR08010_Member
                                            {
                                                支払先コード = m01.得意先ID,
                                                支払先名 = m01.略称名,
                                                会社名ｶﾅ = m01.かな読み,
                                                親子区分名 = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                                締日 = m01.Ｓ締日,
                                                前月残高 = Group.Sum(c => c.月次前月残高),
                                                出金金額 = Group.Sum(c => c.月次入金現金 + c.月次入金手形),
                                                出金調整額 = Group.Sum(c => c.月次入金その他),
                                                差引金額 = Group.Sum(c => (c.月次前月残高) - (c.月次入金現金 + c.月次入金手形 + c.月次入金その他)),
                                                当月支払 = Group.Sum(c => c.月次売上金額),
                                                内課税額 = Group.Sum(c => c.月次課税売上),
                                                消費税 = Group.Sum(c => c.月次消費税),
                                                通行料 = Group.Sum(c => c.月次通行料),
                                                当月合計額 = Group.Sum(c => c.月次売上金額 + c.月次通行料 + c.月次消費税),
                                                繰越金額 = Group.Sum(c => ((c.月次前月残高) - (c.月次入金現金 + c.月次入金手形 + c.月次入金その他)) + (c.月次売上金額 + c.月次通行料 + c.月次消費税)),
                                                件数 = Group.Sum(c => c.月次件数),
                                                作成年月 = i作成年 + "年" + i作成月 + "年度",
                                                支払先指定 = p支払先From + "～" + p支払先To,
                                                未定 = Group.Count(c => c.月次未定件数 > 0) != 0 ? "未定" : "",
                                                集計区分 = i集計区分 == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                                表示区分 = i表示区分 == 0 ? "(支払あり支払先のみ)" : "(支払無し支払先含む)",
                                                表示締日 = 全締日集計 == true ? "全締日" : p作成締日,
                                                表示順序 = i表示順序 == 0 ? "支払先ID" : i表示順序 == 1 ? "ｶﾅ読み" : i表示順序 == 2 ? "前月残高" : i表示順序 == 3 ? "当月支払" : "繰越金額",
                                                支払先ﾋﾟｯｸｱｯﾌﾟ = 支払先指定ﾋﾟｯｸｱｯﾌﾟ == "" ? "" : 支払先指定ﾋﾟｯｸｱｯﾌﾟ,
                                            }).AsQueryable();
                    }
                }
                else
                {
                    //支払先From処理　Min値
                    if (!string.IsNullOrEmpty(p支払先From))
                    {
                        int i支払先FROM = AppCommon.IntParse(p支払先From);
                        query = query.Where(c => c.支払先コード >= int.MinValue);
                    }

                    //支払先To処理　Max値
                    if (!string.IsNullOrEmpty(p支払先To))
                    {
                        int i支払先TO = AppCommon.IntParse(p支払先To);
                        query = query.Where(c => c.支払先コード <= int.MaxValue);
                    }

                    //締日処理
                    if (全締日集計 == true)
                    {
                        query = query.Where(c => c.締日 >= 1 && c.締日 <= 31);
                    }
                    else
                    {
                        int i作成締日 = AppCommon.IntParse(p作成締日);
                        query = query.Where(c => c.締日 == i作成締日);
                    }

                }

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

                switch (i表示区分)
                {
                    //支払取引全体
                    case 0:
                        query = query.Where(c => c.当月支払 != 0 || c.前月残高 != 0);
                        break;

                    //支払先
                    case 1:
                        query = query.Where(c => c.当月支払 != 0);
                        break;

                    default:
                        break;
                }

                //表示順序1
                switch (i表示順序)
                {
                    case 0://支払先コード
                        query = query.OrderBy(order => order.支払先コード);
                        break;

                    case 1://会社名ｶﾅ
                        query = query.OrderBy(order => order.会社名ｶﾅ);
                        break;

                    case 2://前月残高
                        query = query.OrderByDescending(order => order.前月残高);
                        break;

                    case 3://当月支払
                        query = query.OrderByDescending(order => order.当月支払);
                        break;

                    case 4://繰越金額
                        query = query.OrderByDescending(order => order.繰越金額);
                        break;
                }

                retList = query.Where(c => c.支払先コード > 0).ToList();
                return retList;
            }
        }
        #endregion

    }

}