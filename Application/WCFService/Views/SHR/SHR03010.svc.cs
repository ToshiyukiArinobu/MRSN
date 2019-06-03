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
    public class SHR03010
    {
    #region メンバー定義
    // データメンバー定義
    /// <summary>
    /// T03_KTRN_Member
    /// </summary>
    [DataContract]
    public class T03_KTRN_Member_Pre
    {
        public DateTime? 経費発生日 { get; set; }
        public int? 支払先ID { get; set; }
        public string 支払先名 { get; set; }
        public int? 車輌ID { get; set; }
        public string 車輌番号 { get; set; }
        public string 乗務員名 { get; set; }
        public int? 自社部門ID { get; set; }
        public string 経費項目名 { get; set; }
        public string 経費補助名称 { get; set; }
        public decimal? 単価 { get; set; }
        public decimal? 内軽油税分 { get; set; }
        public decimal? 数量 { get; set; }
        public int? 金額 { get; set; }
        public int? 摘要ID { get; set; }
        public string 摘要名 { get; set; }
        public int 明細番号 { get; set; }
        public int 明細行 { get; set; }
        public int S締日 { get; set; }
        public string 収支対象 { get; set; }
        public string 対象年 { get; set; }
        public string 対象月 { get; set; }
        public int 社内区分 { get; set; }
        public DateTime? 集計F { get; set; }
        public DateTime? 集計T { get; set; }
    }
    #endregion

    #region 印刷画面
        /// <summary>
        /// SHR03010画面のプレビュー　T03_KTRNよりリスト取得
        /// </summary>
        /// <param name="p明細番号">明細番号</param>
        /// <param name="p明細行">明細行</param>
        /// <returns>T03_KTRN_Member_PreのList</returns>
        public List<T03_KTRN_Member_Pre> SearchPreviewListData(string p支払先From, string p支払先To, int?[] i支払先List, string p作成締日, string p作成年, string p作成月, DateTime d集計期間From, DateTime d集計期間To)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<T03_KTRN_Member_Pre> retList = new List<T03_KTRN_Member_Pre>();
                context.Connection.Open();

                var query = (
                             from t03 in context.T03_KTRN
                             from m01 in context.M01_TOK.Where(m01 => m01.得意先KEY == t03.支払先KEY).DefaultIfEmpty()
                             from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == t03.乗務員KEY).DefaultIfEmpty()
                             from m07 in context.M07_KEI.Where(m07 => m07.経費項目ID == t03.経費項目ID).DefaultIfEmpty()
                             orderby t03.経費発生日
                             where m01.削除日付 == null
                             select new T03_KTRN_Member_Pre
                           {
                               支払先ID = m01.得意先ID,
                               支払先名 = m01.略称名,
                               経費発生日 = t03.経費発生日,
                               車輌ID = t03.車輌ID,
                               車輌番号 = t03.車輌番号,
                               乗務員名 = m04.乗務員名,
                               自社部門ID = t03.自社部門ID,
                               経費項目名 = m07.経費項目名,
                               経費補助名称 = t03.経費補助名称,
                               単価 = t03.単価 == null ? 0 : t03.単価,
                               内軽油税分 = t03.内軽油税分 == null ? 0 : t03.内軽油税分,
                               数量 = t03.数量 == null ? 0 : t03.数量,
                               金額 = t03.金額 == null ? 0 : t03.金額,
                               摘要ID = t03.摘要ID,
                               摘要名 = t03.摘要名,
                               明細番号 = t03.明細番号,
                               明細行 = t03.明細行,
                               S締日 = m01.Ｓ締日,
                               収支対象 = t03.収支区分 == 0 ? "対 象" : t03.収支区分 == 1 ? "非対象" : "",
                               集計F = d集計期間From,
                               集計T = d集計期間To,
                               対象年 = p作成年,
                               対象月 = p作成月,
                           }).Distinct().AsQueryable();

                if (!(string.IsNullOrEmpty(p支払先From + p支払先To) && i支払先List.Length == 0))
                {
                    //支払先が検索対象に入っていない時全データ取得
                    if (string.IsNullOrEmpty(p支払先From + p支払先To))
                    {
                        query = query.Where(c => c.支払先ID >= int.MaxValue);
                    }

                    //支払先From処理　Min値
                    if (!string.IsNullOrEmpty(p支払先From))
                    {
                        int i支払先FROM = AppCommon.IntParse(p支払先From);
                        query = query.Where(c => c.支払先ID >= i支払先FROM);
                    }

                    //支払先To処理　Max値
                    if (!string.IsNullOrEmpty(p支払先To))
                    {
                        int i支払先TO = AppCommon.IntParse(p支払先To);
                        query = query.Where(c => c.支払先ID <= i支払先TO);
                    }

                    //締日処理
                    if (!string.IsNullOrEmpty(p作成締日))
                    {
                        int? 変換締日 = Convert.ToInt32(p作成締日);
                        query = query.Where(c => c.S締日 == 変換締日);
                    }

                    //日付処理
                    if (string.IsNullOrEmpty(p作成年) && string.IsNullOrEmpty(p作成月) || string.IsNullOrEmpty(p作成年) || string.IsNullOrEmpty(p作成月))
                    {
                        query = query.Where(c => c.経費発生日 >= DateTime.MinValue && c.経費発生日 <= DateTime.MaxValue);
                    }
                    else
                    {
                        query = query.Where(c => c.経費発生日 >= d集計期間From && c.経費発生日 <= d集計期間To);
                    }

                    if (i支払先List.Length > 0)
                    {
                        var intCause = i支払先List;

                        query = query.Union(from t03 in context.T03_KTRN
                                            from m01 in context.M01_TOK.Where(m01 => m01.得意先KEY == t03.支払先KEY).DefaultIfEmpty()
                                            from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == t03.乗務員KEY).DefaultIfEmpty()
                                            from m07 in context.M07_KEI.Where(m07 => m07.経費項目ID == t03.経費項目ID).DefaultIfEmpty()
                                            where m01.削除日付 == null && intCause.Contains(m01.得意先ID)
                                            orderby t03.経費発生日
                                            select new T03_KTRN_Member_Pre
                                            {
                                                支払先ID = m01.得意先ID,
                                                支払先名 = m01.略称名,
                                                経費発生日 = t03.経費発生日,
                                                車輌ID = t03.車輌ID,
                                                車輌番号 = t03.車輌番号,
                                                乗務員名 = m04.乗務員名,
                                                自社部門ID = t03.自社部門ID,
                                                経費項目名 = m07.経費項目名,
                                                経費補助名称 = t03.経費補助名称,
                                                単価 = t03.単価 == null ? 0 : t03.単価,
                                                内軽油税分 = t03.内軽油税分 == null ? 0 : t03.内軽油税分,
                                                数量 = t03.数量 == null ? 0 : t03.数量,
                                                金額 = t03.金額 == null ? 0 : t03.金額,
                                                摘要ID = t03.摘要ID,
                                                摘要名 = t03.摘要名,
                                                明細番号 = t03.明細番号,
                                                明細行 = t03.明細行,
                                                S締日 = m01.Ｓ締日,
                                                収支対象 = t03.収支区分 == 0 ? "対 象" : t03.収支区分 == 1 ? "非対象" : "",
                                                集計F = d集計期間From,
                                                集計T = d集計期間To,
                                                対象年 = p作成年,
                                                対象月 = p作成月,
                                            });

                        //締日処理
                        if (!string.IsNullOrEmpty(p作成締日))
                        {
                            int? 変換締日 = Convert.ToInt32(p作成締日);
                            query = query.Where(c => c.S締日 == 変換締日);
                        }

                        //日付処理
                        if (string.IsNullOrEmpty(p作成年) && string.IsNullOrEmpty(p作成月) || string.IsNullOrEmpty(p作成年) || string.IsNullOrEmpty(p作成月))
                        {
                            query = query.Where(c => c.経費発生日 >= DateTime.MinValue && c.経費発生日 <= DateTime.MaxValue);
                        }
                        else
                        {
                            query = query.Where(c => c.経費発生日 >= d集計期間From && c.経費発生日 <= d集計期間To);
                        }
                    }
                }
                else
                {
                    //締日処理
                    if (!string.IsNullOrEmpty(p作成締日))
                    {
                        int? 変換締日 = Convert.ToInt32(p作成締日);
                        query = query.Where(c => c.S締日 == 変換締日);
                    }

                    if (string.IsNullOrEmpty(p作成年) && string.IsNullOrEmpty(p作成月) || string.IsNullOrEmpty(p作成年) || string.IsNullOrEmpty(p作成月))
                    {
                        query = query.Where(c => c.経費発生日 >= DateTime.MinValue && c.経費発生日 <= DateTime.MaxValue);
                    }
                    else
                    {
                        query = query.Where(c => c.経費発生日 >= d集計期間From && c.経費発生日 <= d集計期間To);
                    }
                }
                //結果をリスト化
				query = query.OrderBy(c => c.経費発生日);
                retList = query.ToList();
                return retList;
            }
        }
    #   endregion

    #region CSV出力
        /// <summary>
        /// SHR03010画面のプレビュー　T03_KTRNよりリスト取得
        /// </summary>
        /// <param name="p明細番号">明細番号</param>
        /// <param name="p明細行">明細行</param>
        /// <returns>T03_KTRN_Member_PreのList</returns>
        public List<T03_KTRN_Member_Pre> SearchCsvListData(string p支払先From, string p支払先To, int?[] i支払先List, string p作成締日, string p作成年, string p作成月, DateTime d集計期間From, DateTime d集計期間To)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<T03_KTRN_Member_Pre> retList = new List<T03_KTRN_Member_Pre>();
                context.Connection.Open();

                var query = (
                             from t03 in context.T03_KTRN
                             from m01 in context.M01_TOK.Where(m01 => m01.得意先KEY == t03.支払先KEY).DefaultIfEmpty()
                             from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == t03.乗務員KEY).DefaultIfEmpty()
                             from m07 in context.M07_KEI.Where(m07 => m07.経費項目ID == t03.経費項目ID).DefaultIfEmpty()
                             orderby t03.経費発生日
                             where m01.削除日付 == null
                             select new T03_KTRN_Member_Pre
                             {
                                 支払先ID = m01.得意先ID,
                                 支払先名 = m01.略称名,
                                 経費発生日 = t03.経費発生日,
                                 車輌ID = t03.車輌ID,
                                 車輌番号 = t03.車輌番号,
                                 乗務員名 = m04.乗務員名,
                                 自社部門ID = t03.自社部門ID,
                                 経費項目名 = m07.経費項目名,
                                 経費補助名称 = t03.経費補助名称,
                                 単価 = t03.単価 == null ? 0 : t03.単価,
                                 内軽油税分 = t03.内軽油税分 == null ? 0 : t03.内軽油税分,
                                 数量 = t03.数量 == null ? 0 : t03.数量,
                                 金額 = t03.金額 == null ? 0 : t03.金額,
                                 摘要ID = t03.摘要ID,
                                 摘要名 = t03.摘要名,
                                 明細番号 = t03.明細番号,
                                 明細行 = t03.明細行,
                                 S締日 = m01.Ｓ締日,
                                 収支対象 = t03.収支区分 == 0 ? "対 象" : t03.収支区分 == 1 ? "非対象" : "",
                                 集計F = d集計期間From,
                                 集計T = d集計期間To,
                                 対象年 = p作成年,
                                 対象月 = p作成月,
                             }).Distinct().AsQueryable();

                if (!(string.IsNullOrEmpty(p支払先From + p支払先To) && i支払先List.Length == 0))
                {
                    //支払先が検索対象に入っていない時全データ取得
                    if (string.IsNullOrEmpty(p支払先From + p支払先To))
                    {
                        query = query.Where(c => c.支払先ID >= int.MaxValue);
                    }

                    //支払先From処理　Min値
                    if (!string.IsNullOrEmpty(p支払先From))
                    {
                        int i支払先FROM = AppCommon.IntParse(p支払先From);
                        query = query.Where(c => c.支払先ID >= i支払先FROM);
                    }

                    //支払先To処理　Max値
                    if (!string.IsNullOrEmpty(p支払先To))
                    {
                        int i支払先TO = AppCommon.IntParse(p支払先To);
                        query = query.Where(c => c.支払先ID <= i支払先TO);
                    }

                    //締日処理
                    if (!string.IsNullOrEmpty(p作成締日))
                    {
                        int? 変換締日 = Convert.ToInt32(p作成締日);
                        query = query.Where(c => c.S締日 == 変換締日);
                    }

                    //日付処理
                    if (string.IsNullOrEmpty(p作成年) && string.IsNullOrEmpty(p作成月) || string.IsNullOrEmpty(p作成年) || string.IsNullOrEmpty(p作成月))
                    {
                        query = query.Where(c => c.経費発生日 >= DateTime.MinValue && c.経費発生日 <= DateTime.MaxValue);
                    }
                    else
                    {
                        query = query.Where(c => c.経費発生日 >= d集計期間From && c.経費発生日 <= d集計期間To);
                    }

                    if (i支払先List.Length > 0)
                    {
                        var intCause = i支払先List;

                        query = query.Union(from t03 in context.T03_KTRN
                                            from m01 in context.M01_TOK.Where(m01 => m01.得意先KEY == t03.支払先KEY).DefaultIfEmpty()
                                            from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == t03.乗務員KEY).DefaultIfEmpty()
                                            from m07 in context.M07_KEI.Where(m07 => m07.経費項目ID == t03.経費項目ID).DefaultIfEmpty()
                                            where m01.削除日付 == null && intCause.Contains(m01.得意先ID)
                                            orderby t03.経費発生日
                                            select new T03_KTRN_Member_Pre
                                            {
                                                支払先ID = m01.得意先ID,
                                                支払先名 = m01.略称名,
                                                経費発生日 = t03.経費発生日,
                                                車輌ID = t03.車輌ID,
                                                車輌番号 = t03.車輌番号,
                                                乗務員名 = m04.乗務員名,
                                                自社部門ID = t03.自社部門ID,
                                                経費項目名 = m07.経費項目名,
                                                経費補助名称 = t03.経費補助名称,
                                                単価 = t03.単価 == null ? 0 : t03.単価,
                                                内軽油税分 = t03.内軽油税分 == null ? 0 : t03.内軽油税分,
                                                数量 = t03.数量 == null ? 0 : t03.数量,
                                                金額 = t03.金額 == null ? 0 : t03.金額,
                                                摘要ID = t03.摘要ID,
                                                摘要名 = t03.摘要名,
                                                明細番号 = t03.明細番号,
                                                明細行 = t03.明細行,
                                                S締日 = m01.Ｓ締日,
                                                収支対象 = t03.収支区分 == 0 ? "対 象" : t03.収支区分 == 1 ? "非対象" : "",
                                                集計F = d集計期間From,
                                                集計T = d集計期間To,
                                                対象年 = p作成年,
                                                対象月 = p作成月,
                                            });

                        //締日処理　
                        if (!string.IsNullOrEmpty(p作成締日))
                        {
                            int? 変換締日 = Convert.ToInt32(p作成締日);
                            query = query.Where(c => c.S締日 == 変換締日);
                        }
                    }
                }
                else
                {
                    //締日処理
                    if (!string.IsNullOrEmpty(p作成締日))
                    {
                        int? 変換締日 = Convert.ToInt32(p作成締日);
                        query = query.Where(c => c.S締日 == 変換締日);
                    }

                    if (string.IsNullOrEmpty(p作成年) && string.IsNullOrEmpty(p作成月) || string.IsNullOrEmpty(p作成年) || string.IsNullOrEmpty(p作成月))
                    {
                        query = query.Where(c => c.経費発生日 >= DateTime.MinValue && c.経費発生日 <= DateTime.MaxValue);
                    }
                    else
                    {
                        query = query.Where(c => c.経費発生日 >= d集計期間From && c.経費発生日 <= d集計期間To);
                    }
                }
				//結果をリスト化
				query = query.OrderBy(c => c.経費発生日);
                retList = query.ToList();
                return retList;
            }
        }
        #endregion
    }
}
