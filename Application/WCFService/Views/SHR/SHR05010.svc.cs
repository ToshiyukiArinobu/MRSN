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
    public class SHR05010 {


    /// <summary>
    /// SHR05010  印刷　メンバー
    /// </summary>
    [DataContract]
    public class SHR05010_Member
    {
         public int?[] 支払先指定 { get; set; }
         public int 支払先コード { get; set; }
         public string 支払先名 { get; set; }
         public decimal? 支払金額 { get; set; }
         public decimal? 支払通行料 { get; set; }
         public decimal? 課税支払額 { get; set; }
         public decimal? 支払消費税 { get; set; }
         public decimal? 当月支払額 { get; set; }
         public decimal? 請求金額 { get; set; }
         public decimal? 請求通行料 { get; set; }
         public decimal? 差益 { get; set; }
         public decimal? 差益率 { get; set; }
         public decimal? 件数 { get; set; }
         public string 未定 { get; set; }
         public string 対象年月 { get; set; }
         public int 集計年月 { get; set; }
         public string 集計区分 { get; set; }
         public int? 締日 { get; set; }
         public string 全締日 { get; set; }
         public string 表示締日 { get; set; }
         public string かな読み { get; set; }
         public int 取引区分 { get; set; }
         public decimal? 締日未定件数 { get; set; }
         public string 表示順序 { get; set; }
         public string 支払先指定コード { get; set; }
         public string 支払先Sコード { get; set; }
         public string 支払先Fコード { get; set; }
         public string 親子区分ID { get; set; }
         public string 作成区分 { get; set; }
    }

    /// <summary>
    /// SHR05010  CSV　メンバー
    /// </summary>
    [DataContract]
    public class SHR05010_Member_CSV
    {
         public int 支払先コード { get; set; }
         public string 支払先名 { get; set; }
         public decimal? 支払金額 { get; set; }
         public decimal? 支払通行料 { get; set; }
         public decimal? 課税支払額 { get; set; }
         public decimal? 支払消費税 { get; set; }
         public decimal? 当月支払額 { get; set; }
         public decimal? 請求金額 { get; set; }
         public decimal? 請求通行料 { get; set; }
         public decimal? 差益 { get; set; }
         public decimal? 差益率 { get; set; }
         public decimal? 件数 { get; set; }
         public int 集計年月 { get; set; }
         public int? 締日 { get; set; }
         public decimal? 締日未定件数 { get; set; }
         public int 取引区分 { get; set; }
         public string 未定 { get; set; }
         public string 対象年月 { get; set; }
         public string 集計区分 { get; set; }
         public string 全締日 { get; set; }
         public string 表示締日 { get; set; }
         public string かな読み { get; set; }
         public string 表示順序 { get; set; }
         public string 親子区分ID { get; set; }
         public string 作成区分 { get; set; }
    }
        #region 締日帳票印刷
        /// <summary>
        /// SHR05010 締日帳票印刷
        /// </summary>
        /// <param name="p商品ID">支払先コード</param>
        /// <returns>S02</returns>
    public List<SHR05010_Member> SEARCH_SHR05010_SimebiPreview(string p支払先From, string p支払先To, int?[] i支払先List, int 支払区分_CValue, int 集計区分_CValue, string p作成締日, bool b全締日集計, int? p作成年, int? p作成月, DateTime p集計期間From, DateTime p集計期間To, int p表示順序, int 作成区分_CValue)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                //差益率計算時に利用
                List<SHR05010_Member> retList = new List<SHR05010_Member>();
                //要素の削除時に利用
                List<SHR05010_Member> queryList = new List<SHR05010_Member>();
                context.Connection.Open();

                //支払先指定　表示用
                string 支払先指定表示 = string.Empty;
                int 集計 = 0;

                Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
                string p変換作成年 = p作成年.ToString();
                string p変換作成月 = p作成月.ToString();
                int num = sjisEnc.GetByteCount(p変換作成月);

                if (!string.IsNullOrEmpty(p作成締日))
                {
                    int? p変換作成締日 = AppCommon.IntParse(p作成締日);

                    //文字のバイト数が1の場合、月に0を足して表示
                    if (num == 1)
                    {
                        集計 = Convert.ToInt32(p作成年 + "0" + p作成月);
                    }
                    else
                    {
                        集計 = Convert.ToInt32(p変換作成年 + p変換作成月);
                    }

                }
                else
                {
                    //文字のバイト数が1の場合、月に0を足して表示
                    if (num == 1)
                    {
                        集計 = Convert.ToInt32(p作成年 + "0" + p作成月);
                    }
                    else
                    {
                        集計 = Convert.ToInt32(p変換作成年 + p変換作成月);
                    }
                }


                var query = (from m01 in context.M01_TOK
                             join s02 in context.S02_YOSS.Where(c => c.回数 == 1) on m01.得意先KEY equals s02.支払先KEY into v01Group
                             where m01.削除日付 == null
                             select new SHR05010_Member
                             {
                                 支払先コード = m01.得意先ID,
                                 支払先名 = m01.略称名,
                                 取引区分 = m01.取引区分,
                                 支払金額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額),
                                 支払通行料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日通行料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日通行料),
                                 課税支払額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日課税売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日課税売上),
                                 支払消費税 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日消費税),
                                 当月支払額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額 + v01.締日通行料 + v01.締日消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額 + v01.締日通行料 + v01.締日消費税),
                                 請求金額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車売上),
                                 請求通行料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車料),
                                 差益 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => ((v01.締日内傭車売上 + v01.締日内傭車料) - (v01.締日売上金額 + v01.締日通行料))) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => ((v01.締日内傭車売上 + v01.締日内傭車料) - (v01.締日売上金額 + v01.締日通行料))),
                                 差益率 = 0,
                                 //                                 差益率 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => ((v01.締日内傭車売上 - v01.締日売上金額) / (v01.締日内傭車売上 + v01.締日内傭車料)) * 100),

                                 全締日 = p作成締日 == string.Empty ? "全締日" : p作成締日,
                                 親子区分ID = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                 締日 = m01.Ｓ締日,
                                 集計年月 = 集計,
                                 件数 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日件数) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日件数),
                                 かな読み = m01.かな読み,
                                 未定 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日未定件数) >= 1 ? "未定" : "",
                                 対象年月 = p変換作成年 + "　年　" + p変換作成月 + "　月度",
                                 集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                 表示順序 = p表示順序 == 0 ? "ID順" : p表示順序 == 1 ? "かな順" : p表示順序 == 2 ? "売上順" : "ID順",
                                 支払先指定コード = 支払先指定表示 == "" ? "" : 支払先指定表示,
                                 支払先Sコード = p支払先From == "" ? "" : p支払先From + " ～ ",
                                 支払先Fコード = p支払先To == "" ? "" : p支払先To,
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

                    if (i支払先List.Length > 0)
                    {
                        var intCause = i支払先List;
                        query = query.Union(from m01 in context.M01_TOK
                                            join s02 in context.S02_YOSS.Where(c => c.回数 == 1) on m01.得意先KEY equals s02.支払先KEY into v01Group
                                            where intCause.Contains(m01.得意先ID) && m01.削除日付 == null
                                            select new SHR05010_Member
                                            {
                                                支払先コード = m01.得意先ID,
                                                支払先名 = m01.略称名,
                                                取引区分 = m01.取引区分,
                                                支払金額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額),
                                                支払通行料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日通行料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日通行料),
                                                課税支払額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日課税売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日課税売上),
                                                支払消費税 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日消費税),
                                                当月支払額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額 + v01.締日通行料 + v01.締日消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額 + v01.締日通行料 + v01.締日消費税),
                                                請求金額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車売上),
                                                請求通行料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車料),
                                                差益 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => ((v01.締日内傭車売上 + v01.締日内傭車料) - (v01.締日売上金額 + v01.締日通行料))) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => ((v01.締日内傭車売上 + v01.締日内傭車料) - (v01.締日売上金額 + v01.締日通行料))),
                                                差益率 = 0,
                                                全締日 = p作成締日 == string.Empty ? "全締日" : p作成締日,
                                                親子区分ID = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                                締日 = m01.Ｓ締日,
                                                集計年月 = 集計,
                                                件数 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日件数) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日件数),
                                                かな読み = m01.かな読み,
                                                未定 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日未定件数) >= 1 ? "未定" : "",
                                                対象年月 = p変換作成年 + "　年　" + p変換作成月 + "　月度",
                                                集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                                表示順序 = p表示順序 == 0 ? "ID順" : p表示順序 == 1 ? "かな順" : p表示順序 == 2 ? "売上順" : "ID順",
                                                支払先指定コード = 支払先指定表示 == "" ? "" : 支払先指定表示,
                                                支払先Sコード = p支払先From == "" ? "" : p支払先From + " ～ ",
                                                支払先Fコード = p支払先To == "" ? "" : p支払先To,
                                            });

                    }
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
						query = query.Where(c => c.当月支払額 != 0 || c.請求金額 != 0 || c.請求通行料 != 0);
                        break;

                    //支払先
                    case 1:
                        //query = query.Where(c => c.当月支払額 >= 0);
                        break;

                    default:
                        break;
                }


                //表示順序処理
                switch (p表示順序)
                {
                    //支払取引全体
                    case 0:
                        query = query.OrderBy(c => c.支払先コード);
                        break;

                    //支払先
                    case 1:
                        query = query.OrderBy(c => c.かな読み);
                        break;

                    //経費先
                    case 2:
                        query = query.OrderByDescending(c => c.当月支払額);
                        break;

                    default:
                        break;
                }

                //得意先指定の表示
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
                        queryList = query.ToList();


                        if (b全締日集計)
                        {
                            retList = queryList;
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].請求金額 == 0)
                                {
                                    retList[i].差益率 = null;
                                }
                                else
                                {
                                    retList[i].差益率 = (retList[i].差益 / (retList[i].請求金額 + retList[i].請求通行料)) * 100;
                                }
                            }
                        }
                        else
                        {
                            retList = queryList;
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].請求金額 == 0)
                                {
                                    retList[i].差益率 = null;
                                }
                                else
                                {
                                    retList[i].差益率 = (retList[i].差益 / (retList[i].請求金額 + retList[i].請求通行料)) * 100;
                                }
                            }
                        }


                        

                    
                
                return retList;
            }
        }
        #endregion

        #region 月次帳票印刷
        /// <summary>
        /// SHR05010 月次帳票印刷
        /// </summary>
        /// <param name="p商品ID">支払先コード</param>
        /// <returns>S12</returns>
        public List<SHR05010_Member> SEARCH_SHR05010_GetsujiPreview(string p支払先From, string p支払先To, int?[] i支払先List, int 支払区分_CValue, int 集計区分_CValue, string p作成締日, bool b全締日集計, int? p作成年, int? p作成月, DateTime p集計期間From, DateTime p集計期間To, int p表示順序, int 作成区分_CValue)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                //差益率計算時に利用
                List<SHR05010_Member> retList = new List<SHR05010_Member>();
                //要素の削除時に利用
                List<SHR05010_Member> queryList = new List<SHR05010_Member>();
                context.Connection.Open();

                //支払先指定　表示用
                string 支払先指定表示 = string.Empty;
                int 集計 = 0;

                Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
                string p変換作成年 = p作成年.ToString();
                string p変換作成月 = p作成月.ToString();
                int num = sjisEnc.GetByteCount(p変換作成月);

                if (!string.IsNullOrEmpty(p作成締日))
                {
                    int? p変換作成締日 = AppCommon.IntParse(p作成締日);

                    //文字のバイト数が1の場合、月に0を足して表示
                    if (num == 1)
                    {
                        集計 = Convert.ToInt32(p作成年 + "0" + p作成月);
                    }
                    else
                    {
                        集計 = Convert.ToInt32(p変換作成年 + p変換作成月);
                    }

                }
                else
                {
                    //文字のバイト数が1の場合、月に0を足して表示
                    if (num == 1)
                    {
                        集計 = Convert.ToInt32(p作成年 + "0" + p作成月);
                    }
                    else
                    {
                        集計 = Convert.ToInt32(p変換作成年 + p変換作成月);
                    }
                }

                //月次集計処理
                var query = (from m01 in context.M01_TOK
                             join s12 in context.S12_YOSG.Where(c => c.回数 == 1) on m01.得意先KEY equals s12.支払先KEY into v01Group
                             where m01.削除日付 == null
                             select new SHR05010_Member
                             {
                                 支払先コード = m01.得意先ID,
                                 支払先名 = m01.略称名,
                                 取引区分 = m01.取引区分,
                                 支払金額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額),
                                 支払通行料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次通行料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次通行料),
                                 課税支払額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次課税売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次課税売上),
                                 支払消費税 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次消費税),
                                 当月支払額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額 + v01.月次通行料 + v01.月次消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額 + v01.月次通行料 + v01.月次消費税),
                                 請求金額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車売上),
                                 請求通行料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車料),
                                 差益 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => (v01.月次内傭車売上 - v01.月次売上金額)) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => (v01.月次内傭車売上 - v01.月次売上金額)),
                                 差益率 = 0,
                                 全締日 = p作成締日 == string.Empty ? "全締日" : p作成締日,
                                 親子区分ID = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                 締日 = m01.Ｓ締日,
                                 集計年月 = 集計,
                                 件数 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次件数) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次件数),
                                 かな読み = m01.かな読み,
                                 未定 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次未定件数) >= 1 ? "未定" : "",
                                 対象年月 = p変換作成年 + "　年　" + p変換作成月 + "　月度",
                                 集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                 表示順序 = p表示順序 == 0 ? "ID順" : p表示順序 == 1 ? "かな順" : p表示順序 == 2 ? "売上順" : "ID順",
                                 支払先指定コード = 支払先指定表示 == "" ? "" : 支払先指定表示,
                                 支払先Sコード = p支払先From == "" ? "" : p支払先From + " ～ ",
                                 支払先Fコード = p支払先To == "" ? "" : p支払先To,
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

                    if (i支払先List.Length > 0)
                    {
                        var intCause = i支払先List;
                        query = query.Union(from m01 in context.M01_TOK
                                            join s12 in context.S12_YOSG.Where(c => c.回数 == 1) on m01.得意先KEY equals s12.支払先KEY into v01Group
                                            where intCause.Contains(m01.得意先ID) && m01.削除日付 == null
                                            select new SHR05010_Member
                                            {
                                                支払先コード = m01.得意先ID,
                                                支払先名 = m01.略称名,
                                                取引区分 = m01.取引区分,
                                                支払金額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額),
                                                支払通行料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次通行料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次通行料),
                                                課税支払額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次課税売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次課税売上),
                                                支払消費税 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次消費税),
                                                当月支払額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額 + v01.月次通行料 + v01.月次消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額 + v01.月次通行料 + v01.月次消費税),
                                                請求金額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車売上),
                                                請求通行料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車料),
                                                差益 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => (v01.月次内傭車売上 - v01.月次売上金額)) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => (v01.月次内傭車売上 - v01.月次売上金額)),
                                                差益率 = 0,
                                                全締日 = p作成締日 == string.Empty ? "全締日" : p作成締日,
                                                親子区分ID = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                                締日 = m01.Ｓ締日,
                                                集計年月 = 集計,
                                                件数 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次件数) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次件数),
                                                かな読み = m01.かな読み,
                                                未定 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次未定件数) >= 1 ? "未定" : "",
                                                対象年月 = p変換作成年 + "　年　" + p変換作成月 + "　月度",
                                                集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                                表示順序 = p表示順序 == 0 ? "ID順" : p表示順序 == 1 ? "かな順" : p表示順序 == 2 ? "売上順" : "ID順",
                                                支払先指定コード = 支払先指定表示 == "" ? "" : 支払先指定表示,
                                                支払先Sコード = p支払先From == "" ? "" : p支払先From + " ～ ",
                                                支払先Fコード = p支払先To == "" ? "" : p支払先To,
                                            });
                    }
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
						query = query.Where(c => c.当月支払額 != 0 || c.請求金額 != 0 || c.請求通行料 != 0);
                        break;

                    //支払先
                    case 1:
                        //query = query.Where(c => c.当月支払額 >= 0);
                        break;

                    default:
                        break;
                }


                //表示順序処理
                switch (p表示順序)
                {
                    //支払取引全体
                    case 0:
                        query = query.OrderBy(c => c.支払先コード);
                        break;

                    //支払先
                    case 1:
                        query = query.OrderBy(c => c.かな読み);
                        break;

                    //経費先
                    case 2:
                        query = query.OrderByDescending(c => c.当月支払額);
                        break;

                    default:
                        break;
                }

                //得意先指定の表示
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
                queryList = query.ToList();


                if (b全締日集計)
                {
                    retList = queryList;
                    for (int i = 0; i < retList.Count; i++)
                    {
                        if (retList[i].請求金額 == 0)
                        {
                            retList[i].差益率 = null;
                        }
                        else
                        {
                            retList[i].差益率 = (retList[i].差益 / (retList[i].請求金額 + retList[i].請求通行料)) * 100;
                        }
                    }
                }
                else
                {
                    retList = queryList;
                    for (int i = 0; i < retList.Count; i++)
                    {
                        if (retList[i].請求金額 == 0)
                        {
                            retList[i].差益率 = null;
                        }
                        else
                        {
                            retList[i].差益率 = (retList[i].差益 / (retList[i].請求金額 + retList[i].請求通行料)) * 100;
                        }
                    }
                }
                return retList;
            }
        }
        #endregion

        #region 締日CSV出力
        /// <summary>
        /// SHR05010 締日CSV出力
        /// </summary>
        /// <param name="p商品ID">支払先コード</param>
        /// <returns>S12</returns>
        public List<SHR05010_Member_CSV> SEARCH_SHR05010_SimebiCSV(string p支払先From, string p支払先To, int?[] i支払先List, int 支払区分_CValue, int 集計区分_CValue, string p作成締日, bool b全締日集計, int? p作成年, int? p作成月, DateTime p集計期間From, DateTime p集計期間To, int p表示順序, int 作成区分_CValue)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                //差益率計算時に利用
                List<SHR05010_Member_CSV> retList = new List<SHR05010_Member_CSV>();
                //要素の削除時に利用
                List<SHR05010_Member_CSV> queryList = new List<SHR05010_Member_CSV>();
                context.Connection.Open();


                //支払先指定　表示用
                string 支払先指定表示 = string.Empty;
                int 集計 = 0;

                Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
                string p変換作成年 = p作成年.ToString();
                string p変換作成月 = p作成月.ToString();
                int num = sjisEnc.GetByteCount(p変換作成月);

                if (!string.IsNullOrEmpty(p作成締日))
                {
                    int? p変換作成締日 = AppCommon.IntParse(p作成締日);

                    //文字のバイト数が1の場合、月に0を足して表示
                    if (num == 1)
                    {
                        集計 = Convert.ToInt32(p作成年 + "0" + p作成月);
                    }
                    else
                    {
                        集計 = Convert.ToInt32(p変換作成年 + p変換作成月);
                    }

                }
                else
                {
                    //文字のバイト数が1の場合、月に0を足して表示
                    if (num == 1)
                    {
                        集計 = Convert.ToInt32(p作成年 + "0" + p作成月);
                    }
                    else
                    {
                        集計 = Convert.ToInt32(p変換作成年 + p変換作成月);
                    }
                }

                var query = (from m01 in context.M01_TOK
                             join s02 in context.S02_YOSS.Where(c => c.回数 == 1) on m01.得意先KEY equals s02.支払先KEY into v01Group
                             where m01.削除日付 == null
                             select new SHR05010_Member_CSV
                             {
                                 支払先コード = m01.得意先ID,
                                 支払先名 = m01.略称名,
                                 取引区分 = m01.取引区分,
                                 支払金額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額),
                                 支払通行料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日通行料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日通行料),
                                 課税支払額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日課税売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日課税売上),
                                 支払消費税 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日消費税),
                                 当月支払額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額 + v01.締日通行料 + v01.締日消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額 + v01.締日通行料 + v01.締日消費税),
                                 請求金額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車売上),
                                 請求通行料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車料),
                                 差益 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => (v01.締日内傭車売上 - v01.締日売上金額)) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => (v01.締日内傭車売上 - v01.締日売上金額)),
                                 差益率 = 0,
                                 全締日 = p作成締日 == string.Empty ? "全締日" : p作成締日,
                                 親子区分ID = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                 締日 = m01.Ｓ締日,
                                 集計年月 = 集計,
                                 件数 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日件数) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日件数),
                                 かな読み = m01.かな読み,
                                 未定 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日未定件数) >= 1 ? "未定" : "",
                                 対象年月 = p変換作成年 + "　年　" + p変換作成月 + "　月度",
                                 集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                 表示順序 = p表示順序 == 0 ? "ID順" : p表示順序 == 1 ? "かな順" : p表示順序 == 2 ? "売上順" : "ID順",
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

                    if (i支払先List.Length > 0)
                    {
                        var intCause = i支払先List;
                        query = query.Union(from m01 in context.M01_TOK
                                            join s02 in context.S02_YOSS.Where(c => c.回数 == 1) on m01.得意先KEY equals s02.支払先KEY into v01Group
                                            where intCause.Contains(m01.得意先ID) && m01.削除日付 == null
                                            select new SHR05010_Member_CSV
                                            {
                                                支払先コード = m01.得意先ID,
                                                支払先名 = m01.略称名,
                                                取引区分 = m01.取引区分,
                                                支払金額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額),
                                                支払通行料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日通行料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日通行料),
                                                課税支払額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日課税売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日課税売上),
                                                支払消費税 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日消費税),
                                                当月支払額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額 + v01.締日通行料 + v01.締日消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日売上金額 + v01.締日通行料 + v01.締日消費税),
                                                請求金額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車売上),
                                                請求通行料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日内傭車料),
                                                差益 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => (v01.締日内傭車売上 - v01.締日売上金額)) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => (v01.締日内傭車売上 - v01.締日売上金額)),
                                                差益率 = 0,
                                                全締日 = p作成締日 == string.Empty ? "全締日" : p作成締日,
                                                親子区分ID = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                                締日 = m01.Ｓ締日,
                                                集計年月 = 集計,
                                                件数 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日件数) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日件数),
                                                かな読み = m01.かな読み,
                                                未定 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.締日未定件数) >= 1 ? "未定" : "",
                                                対象年月 = p変換作成年 + "　年　" + p変換作成月 + "　月度",
                                                集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                                表示順序 = p表示順序 == 0 ? "ID順" : p表示順序 == 1 ? "かな順" : p表示順序 == 2 ? "売上順" : "ID順",
                                            });
                    }
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
						query = query.Where(c => c.当月支払額 != 0 || c.請求金額 != 0 || c.請求通行料 != 0);
                        break;

                    //支払先
                    case 1:
                        //query = query.Where(c => c.当月支払額 >= 0);
                        break;

                    default:
                        break;
                }


                //表示順序処理
                switch (p表示順序)
                {
                    //支払取引全体
                    case 0:
                        query = query.OrderBy(c => c.支払先コード);
                        break;

                    //支払先
                    case 1:
                        query = query.OrderBy(c => c.かな読み);
                        break;

                    //経費先
                    case 2:
                        query = query.OrderByDescending(c => c.当月支払額);
                        break;

                    default:
                        break;
                }

                //得意先指定の表示
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
                        queryList = query.ToList();


                        if (b全締日集計)
                        {
                            retList = queryList;
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].請求金額 == 0)
                                {
                                    retList[i].差益率 = null;
                                }
                                else
                                {
                                    retList[i].差益率 = (retList[i].差益 / (retList[i].請求金額 + retList[i].請求通行料)) * 100;
                                }
                            }
                        }
                        else
                        {
                            retList = queryList;
                            for (int i = 0; i < retList.Count; i++)
                            {
                                if (retList[i].請求金額 == 0)
                                {
                                    retList[i].差益率 = null;
                                }
                                else
                                {
                                    retList[i].差益率 = (retList[i].差益 / (retList[i].請求金額 + retList[i].請求通行料)) * 100;
                                }
                            }
                        }
                return retList;
            }
        }
        #endregion

        #region 月次CSV出力
        /// <summary>
        /// SHR05010 月次CSV出力
        /// </summary>
        /// <param name="p商品ID">支払先コード</param>
        /// <returns>S12</returns>
    public List<SHR05010_Member_CSV> SEARCH_SHR05010_GetsujiCSV(string p支払先From, string p支払先To, int?[] i支払先List, int 支払区分_CValue, int 集計区分_CValue, string p作成締日, bool b全締日集計, int? p作成年, int? p作成月, DateTime p集計期間From, DateTime p集計期間To, int p表示順序, int 作成区分_CValue)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                //差益率計算時に利用
                List<SHR05010_Member_CSV> retList = new List<SHR05010_Member_CSV>();
                //要素の削除時に利用
                List<SHR05010_Member_CSV> queryList = new List<SHR05010_Member_CSV>();
                context.Connection.Open();

                //支払先指定　表示用
                string 支払先指定表示 = string.Empty;
                int 集計 = 0;

                Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
                string p変換作成年 = p作成年.ToString();
                string p変換作成月 = p作成月.ToString();
                int num = sjisEnc.GetByteCount(p変換作成月);

                if (!string.IsNullOrEmpty(p作成締日))
                {
                    int? p変換作成締日 = AppCommon.IntParse(p作成締日);

                    //文字のバイト数が1の場合、月に0を足して表示
                    if (num == 1)
                    {
                        集計 = Convert.ToInt32(p作成年 + "0" + p作成月);
                    }
                    else
                    {
                        集計 = Convert.ToInt32(p変換作成年 + p変換作成月);
                    }
                }
                else
                {
                    //文字のバイト数が1の場合、月に0を足して表示
                    if (num == 1)
                    {
                        集計 = Convert.ToInt32(p作成年 + "0" + p作成月);
                    }
                    else
                    {
                        集計 = Convert.ToInt32(p変換作成年 + p変換作成月);
                    }
                }

                //月次集計処理
                var query = (from m01 in context.M01_TOK
                             join s12 in context.S12_YOSG.Where(c => c.回数 == 1) on m01.得意先KEY equals s12.支払先KEY into v01Group
                             where m01.削除日付 == null
                             select new SHR05010_Member_CSV
                             {
                                 支払先コード = m01.得意先ID,
                                 支払先名 = m01.略称名,
                                 取引区分 = m01.取引区分,
                                 支払金額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額),
                                 支払通行料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次通行料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次通行料),
                                 課税支払額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次課税売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次課税売上),
                                 支払消費税 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次消費税),
                                 当月支払額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額 + v01.月次通行料 + v01.月次消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額 + v01.月次通行料 + v01.月次消費税),
                                 請求金額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車売上),
                                 請求通行料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車料),
                                 差益 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => (v01.月次内傭車売上 - v01.月次売上金額)) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => (v01.月次内傭車売上 - v01.月次売上金額)),
                                 差益率 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => ((v01.月次内傭車売上 - v01.月次売上金額) / (v01.月次内傭車売上 + v01.月次内傭車料)) * 100),
                                 全締日 = p作成締日 == string.Empty ? "全締日" : p作成締日,
                                 親子区分ID = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                 締日 = m01.Ｓ締日,
                                 集計年月 = 集計,
                                 件数 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次件数) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次件数),
                                 かな読み = m01.かな読み,
                                 未定 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次未定件数) >= 1 ? "未定" : "",
                                 対象年月 = p変換作成年 + "　年　" + p変換作成月 + "　月度",
                                 集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                 表示順序 = p表示順序 == 0 ? "ID順" : p表示順序 == 1 ? "かな順" : p表示順序 == 2 ? "売上順" : "ID順",
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

                    if (i支払先List.Length > 0)
                    {
                        var intCause = i支払先List;
                        //月次集計処理
                        query = query.Union(from m01 in context.M01_TOK
                                            join s12 in context.S12_YOSG.Where(c => c.回数 == 1) on m01.得意先KEY equals s12.支払先KEY into v01Group
                                            where intCause.Contains(m01.得意先ID) && m01.削除日付 == null
                                            select new SHR05010_Member_CSV
                                            {
                                                支払先コード = m01.得意先ID,
                                                支払先名 = m01.略称名,
                                                取引区分 = m01.取引区分,
                                                支払金額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額),
                                                支払通行料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次通行料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次通行料),
                                                課税支払額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次課税売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次課税売上),
                                                支払消費税 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次消費税),
                                                当月支払額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額 + v01.月次通行料 + v01.月次消費税) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次売上金額 + v01.月次通行料 + v01.月次消費税),
                                                請求金額 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車売上) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車売上),
                                                請求通行料 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車料) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次内傭車料),
                                                差益 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => (v01.月次内傭車売上 - v01.月次売上金額)) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => (v01.月次内傭車売上 - v01.月次売上金額)),
                                                差益率 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => ((v01.月次内傭車売上 - v01.月次売上金額) / (v01.月次内傭車売上 + v01.月次内傭車料)) * 100),
                                                全締日 = p作成締日 == string.Empty ? "全締日" : p作成締日,
                                                親子区分ID = m01.親子区分ID == 0 ? "" : m01.親子区分ID == 1 ? "親" : m01.親子区分ID == 2 ? "親" : "子",
                                                締日 = m01.Ｓ締日,
                                                集計年月 = 集計,
                                                件数 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次件数) == null ? 0 : v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次件数),
                                                かな読み = m01.かな読み,
                                                未定 = v01Group.Where(v01 => v01.集計年月 == 集計).Sum(v01 => v01.月次未定件数) >= 1 ? "未定" : "",
                                                対象年月 = p変換作成年 + "　年　" + p変換作成月 + "　月度",
                                                集計区分 = 集計区分_CValue == 0 ? "(締日データを集計)" : "(月次データを集計)",
                                                表示順序 = p表示順序 == 0 ? "ID順" : p表示順序 == 1 ? "かな順" : p表示順序 == 2 ? "売上順" : "ID順",
                                            });
                    }
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
						query = query.Where(c => c.当月支払額 != 0 || c.請求金額 != 0 || c.請求通行料 != 0);
                        break;

                    //支払先
                    case 1:
                        //query = query.Where(c => c.当月支払額 >= 0);
                        break;

                    default:
                        break;
                }


                //表示順序処理
                switch (p表示順序)
                {
                    //支払取引全体
                    case 0:
                        query = query.OrderBy(c => c.支払先コード);
                        break;

                    //支払先
                    case 1:
                        query = query.OrderBy(c => c.かな読み);
                        break;

                    //経費先
                    case 2:
                        query = query.OrderByDescending(c => c.当月支払額);
                        break;

                    default:
                        break;
                }

                //得意先指定の表示
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
                queryList = query.ToList();


                if (b全締日集計)
                {
                    retList = queryList;
                    for (int i = 0; i < retList.Count; i++)
                    {
                        if (retList[i].請求金額 == 0)
                        {
                            retList[i].差益率 = null;
                        }
                        else
                        {
                            retList[i].差益率 = (retList[i].差益 / (retList[i].請求金額 + retList[i].請求通行料)) * 100;
                        }
                    }
                }
                else
                {
                    retList = queryList;
                    for (int i = 0; i < retList.Count; i++)
                    {
                        if (retList[i].請求金額 == 0)
                        {
                            retList[i].差益率 = null;
                        }
                        else
                        {
                            retList[i].差益率 = (retList[i].差益 / (retList[i].請求金額 + retList[i].請求通行料)) * 100;
                        }
                    }
                }
                return retList;
            }
        }
        #endregion
    }
}
