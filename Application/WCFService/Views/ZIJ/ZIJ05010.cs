using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 売上明細問合せサービスクラス
    /// </summary>
    public class ZIJ05010
    {
        #region 項目クラス定義

        /// <summary>
        /// ZIJ05010 請求明細問合せ 検索メンバー
        /// </summary>
        public class SearchDataMember
        {
            public int 会社名コード { get; set; }  // No.227,228 Add
            public string 自社名 { get; set; }     // No.227,228 Add
            public string 売上日 { get; set; }     // No.130-1 Mod
            public string 請求日 { get; set; }     // No.130-1 Mod
            public string 売上区分 { get; set; }
            public string 伝票番号 { get; set; }
            public string 元伝票番号 { get; set; }
            public int 行番号 { get; set; }
            public string 得意先コード { get; set; }  // No.227,228 Add
            public string 得意先 { get; set; }
            public int 品番コード { get; set; }
            public string 自社品番 { get; set; }
            public string 自社品名 { get; set; }
            public string 自社色 { get; set; }
            public string 賞味期限 { get; set; }    // No.130-1 Mod
            public decimal 単価 { get; set; }
            public decimal 数量 { get; set; }
            public string 単位 { get; set; }
            public int 金額 { get; set; }
            public string 摘要 { get; set; }
            public string 受注番号 { get; set; }
            public string 納品伝票番号 { get; set; }
        }

        #endregion

        #region 請求明細問合せ検索処理
        /// <summary>
        /// 売上明細問合せ検索をおこなう
        /// </summary>
        /// <param name="p自社コード"></param>
        /// <param name="p自社販社区分"></param>
        /// <param name="cond">
        /// === 検索条件辞書 ===
        /// 売上日From
        /// 売上日To
        /// 売上区分
        /// 得意先コード
        /// 得意先枝番
        /// </param>
        /// <returns></returns>
        public List<SearchDataMember> GetDataList(int p自社コード, Dictionary<string, string> cond)
        {
            int jisKbn = int.Parse(cond["売上先"]);

            if (jisKbn == 0)
            {
                // 得意先の売上を取得
                return GetData(p自社コード, cond);
            }
            else
            {
                // 販社の売上を取得
                return GetData_Hansha(p自社コード, cond);
            }
        }
        #endregion

        #region　売上明細問合せ検索をおこなう（得意先）
        /// <summary>
        /// 売上明細問合せ検索をおこなう（得意先）
        /// </summary>
        /// <param name="p自社コード"></param>
        /// <param name="p自社販社区分"></param>
        /// <param name="cond">
        /// === 検索条件辞書 ===
        /// 売上日From
        /// 売上日To
        /// 売上区分
        /// 得意先コード
        /// 得意先枝番
        /// </param>
        /// <returns></returns>
        public List<SearchDataMember> GetData(int p自社コード, Dictionary<string, string> cond)
        {
            // 画面パラメータを展開
            DateTime wkDt;
            int wkVal;
            DateTime? salesDateFrom = DateTime.TryParse(cond["売上日From"], out wkDt) ? wkDt : (DateTime?)null,
                salesDateTo = DateTime.TryParse(cond["売上日To"], out wkDt) ? wkDt : (DateTime?)null;
            int salesKbn = int.Parse(cond["売上区分"]);
            int? customerCode = int.TryParse(cond["得意先コード"], out wkVal) ? wkVal : (int?)null,
                customerEda = int.TryParse(cond["得意先枝番"], out wkVal) ? wkVal : (int?)null;
            string myProductCode = cond["自社品番"];     // No.205 Add

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try
                {
                    // 売上区分(名称)データ取得
                    var salesKbnData =
                        context.M99_COMBOLIST
                            .Where(w => w.分類 == "随時" && w.機能 == "請求明細問合せ" && w.カテゴリ == "売上区分");

                    var urData =
                        context.T02_URHD.Where(w => w.削除日時 == null && w.会社名コード == p自社コード)
                            .Join(context.T02_URDTL.Where(w => w.削除日時 == null),
                                x => x.伝票番号,
                                y => y.伝票番号,
                                (x, y) => new { UHD = x, UDTL = y })
                            .Join(context.M09_HIN.Where(w => w.削除日時 == null),
                                x => x.UDTL.品番コード,
                                y => y.品番コード,
                                (x, y) => new { UHD = x.UHD, UDTL = x.UDTL, HIN = y});

                    #region 検索条件による絞込み

                    if (salesDateFrom != null)
                        urData = urData.Where(x => x.UHD.売上日 >= salesDateFrom);

                    if (salesDateTo != null)
                        urData = urData.Where(x => x.UHD.売上日 <= salesDateTo);

                    if (salesKbn > 0)
                        urData = urData.Where(w => w.UHD.売上区分 == salesKbn);

                    // No.205 Add Start
                    if (!string.IsNullOrEmpty(myProductCode))
                        urData = urData.Where(w => w.HIN.自社品番 == myProductCode);
                    // No.205 Add End

                    if (customerCode != null && customerEda != null)
                        urData = urData.Where(w => w.UHD.得意先コード == customerCode && w.UHD.得意先枝番 == customerEda);

                    #endregion


                    var resultList =
                        urData
                            .Join(salesKbnData,
                                x => x.UHD.売上区分,
                                y => y.コード,
                                (x, y) => new { x.UHD, x.UDTL, x.HIN, KBN = y })
                            .GroupJoin(context.M01_TOK,
                                x => new { code = x.UHD.得意先コード, eda = x.UHD.得意先枝番 },
                                y => new { code = y.取引先コード, eda = y.枝番 },
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (a, b) => new { a.x.UHD, a.x.UDTL, a.x.HIN, a.x.KBN, TOK = b })
                            .GroupJoin(context.M06_IRO,
                                x => x.HIN.自社色,
                                y => y.色コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (e, f) => new { e.x.UHD, e.x.UDTL, e.x.KBN, e.x.TOK, e.x.HIN, IRO = f })
                            .GroupJoin(context.M70_JIS.Where(x => x.削除日時 == null),
                                x => x.UHD.会社名コード,
                                y => y.自社コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (g, h) => new { g.x.UHD, g.x.UDTL, g.x.KBN, g.x.TOK, g.x.HIN, g.x.IRO, JIS = h })
                            .OrderBy(o => o.UHD.得意先コード)
                            .ThenBy(t => t.UHD.得意先枝番)
                            .ThenBy(t => t.UHD.売上日)
                            .ThenBy(t => t.UHD.伝票番号)                    // No-120 Add
                            .ThenBy(t => t.UDTL.行番号)
                            .ToList()
                            .Select(x => new SearchDataMember
                            {
                                会社名コード = x.UHD.会社名コード,                // No.227,228 Add
                                自社名 = x.JIS.自社名 ?? "",                      // No.227,228 Add
                                売上日 = x.UHD.売上日.ToShortDateString(),        // No.130-1 Mod
                                請求日 = x.UHD.売上日.Day >= x.TOK.Ｔ締日 ?
                                    AppCommon.GetClosingDate(x.UHD.売上日.Year, x.UHD.売上日.Month, x.TOK.Ｔ締日 ?? 31, 1).ToShortDateString() :     // No.130-1 Mod
                                    AppCommon.GetClosingDate(x.UHD.売上日.Year, x.UHD.売上日.Month, x.TOK.Ｔ締日 ?? 31, 0).ToShortDateString(),      // No.130-1 Mod
                                売上区分 = x.KBN.表示名,
                                伝票番号 = x.UHD.伝票番号.ToString(),
                                元伝票番号 = x.UHD.元伝票番号 != null ? x.UHD.元伝票番号.ToString() : string.Empty,
                                行番号 = x.UDTL.行番号,
                                得意先コード = string.Format("{0:D4} - {1:D2}", x.UHD.得意先コード, x.UHD.得意先枝番),           // No.227,228 Add
                                得意先 = x.TOK != null ? x.TOK.略称名 : string.Empty,
                                品番コード = x.UDTL.品番コード,
                                自社品番 = x.HIN != null ? x.HIN.自社品番 : string.Empty,
                                自社品名 = !string.IsNullOrEmpty(x.UDTL.自社品名) ? x.UDTL.自社品名 :
                                                x.HIN != null ? x.HIN.自社品名 : string.Empty,                                  // No.390 Mod
                                自社色 = x.IRO != null ? x.IRO.色名称 : string.Empty,
                                賞味期限 = x.UDTL.賞味期限 == null ? null : x.UDTL.賞味期限.Value.ToShortDateString(),          // No.130-1 Mod
                                単価 = x.UDTL.単価,
                                数量 = x.UDTL.数量,
                                単位 = x.UDTL.単位,
                                金額 = x.UDTL.金額 == null ? 0 : x.UHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ? (int)x.UDTL.金額 : (int)x.UDTL.金額 * -1,     // No.115 Mod
                                摘要 = x.UDTL.摘要,
                                受注番号 = x.UHD.受注番号.ToString(),
                                納品伝票番号 = x.UHD.納品伝票番号.ToString(),
                            })
                            .ToList();

                    return resultList;

                }
                catch (System.ArgumentException agex)
                {
                    throw agex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }
        #endregion

        #region　売上明細問合せ検索をおこなう（販社）// No.199 Add
        /// <summary>
        /// 売上明細問合せ検索をおこなう（販社）
        /// </summary>
        /// <param name="p自社コード"></param>
        /// <param name="p自社販社区分"></param>
        /// <param name="cond">
        /// === 検索条件辞書 ===
        /// 売上日From
        /// 売上日To
        /// 売上区分
        /// 得意先コード
        /// 得意先枝番
        /// </param>
        /// <returns></returns>
        public List<SearchDataMember> GetData_Hansha(int p自社コード, Dictionary<string, string> cond)
        {
            // 画面パラメータを展開
            DateTime wkDt;
            int wkVal;
            DateTime? salesDateFrom = DateTime.TryParse(cond["売上日From"], out wkDt) ? wkDt : (DateTime?)null,
                salesDateTo = DateTime.TryParse(cond["売上日To"], out wkDt) ? wkDt : (DateTime?)null;
            int salesKbn = int.Parse(cond["売上区分"]);
            int? customerCode = int.TryParse(cond["得意先コード"], out wkVal) ? wkVal : (int?)null,
                customerEda = int.TryParse(cond["得意先枝番"], out wkVal) ? wkVal : (int?)null;
            string myProductCode = cond["自社品番"];

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try
                {
                    // 売上区分(名称)データ取得
                    var salesKbnData =
                        context.M99_COMBOLIST
                            .Where(w => w.分類 == "随時" && w.機能 == "請求明細問合せ" && w.カテゴリ == "売上区分");

                    // (販社)売上データ
                    var urHanData =
                        context.M70_JIS
                            .Where(w => w.削除日時 == null &&
                                w.取引先コード != null &&
                                w.枝番 != null &&
                                w.自社区分 == (int)CommonConstants.自社区分.販社)
                            .Join(context.T02_URHD_HAN.Where(w => w.削除日時 == null),
                                x => x.自社コード,
                                y => y.販社コード,
                                (x, y) => new { JIS = x, UHD_HAN = y })
                            .Join(context.T02_URDTL_HAN.Where(w => w.削除日時 == null),
                                x => x.UHD_HAN.伝票番号,
                                y => y.伝票番号,
                                (x, y) => new { JIS = x.JIS, UHD = x.UHD_HAN, UDTL = y })
                            .Join(context.M09_HIN.Where(w => w.削除日時 == null),
                                x => x.UDTL.品番コード,
                                y => y.品番コード,
                                (x, y) => new { JIS = x.JIS, UHD = x.UHD, UDTL = x.UDTL, HIN = y });

                    #region 検索条件による絞込み

                    if (salesDateFrom != null)
                        urHanData = urHanData.Where(x => x.UHD.売上日 >= salesDateFrom);

                    if (salesDateTo != null)
                        urHanData = urHanData.Where(x => x.UHD.売上日 <= salesDateTo);

                    if (salesKbn > 0)
                        urHanData = urHanData.Where(w => w.UHD.売上区分 == salesKbn);

                    if (!string.IsNullOrEmpty(myProductCode))
                        urHanData = urHanData.Where(w => w.HIN.自社品番 == myProductCode);
                    
                    if (customerCode != null && customerEda != null)
                        urHanData = urHanData.Where(w => w.JIS.取引先コード == customerCode && w.JIS.枝番 == customerEda);

                    #endregion

                    var resultList =
                        urHanData
                            .Join(salesKbnData,
                                x => x.UHD.売上区分,
                                y => y.コード,
                                (x, y) => new { x.UHD, x.UDTL, x.JIS, x.HIN, KBN = y })
                            .GroupJoin(context.M01_TOK,
                                x => new { code = (int)x.JIS.取引先コード, eda = (int)x.JIS.枝番 },
                                y => new { code = y.取引先コード, eda = y.枝番 },
                                (x, y) => new { x,  y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (a, b) => new { a.x.UHD, a.x.UDTL, a.x.JIS, a.x.HIN, a.x.KBN, TOK = b })
                            .GroupJoin(context.M06_IRO,
                                x => x.HIN.自社色,
                                y => y.色コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (e, f) => new { e.x.UHD, e.x.UDTL, e.x.JIS, e.x.HIN, e.x.KBN, e.x.TOK, IRO = f })
                            .OrderBy(o => o.UHD.販社コード)
                            .ThenBy(t => t.JIS.枝番)
                            .ThenBy(t => t.UHD.売上日)
                            .ThenBy(t => t.UHD.伝票番号)
                            .ThenBy(t => t.UDTL.行番号)
                            .ToList()
                            .Select(x => new SearchDataMember
                            {
                                会社名コード = x.UHD.会社名コード,              // No.227,228 Add
                                自社名 = x.JIS.自社名 ?? "",                    // No.227,228 Add
                                売上日 = x.UHD.売上日.ToShortDateString(),
                                請求日 = x.UHD.売上日.Day >= x.TOK.Ｔ締日 ?
                                    AppCommon.GetClosingDate(x.UHD.売上日.Year, x.UHD.売上日.Month, x.TOK.Ｔ締日 ?? 31, 1).ToShortDateString() : 
                                    AppCommon.GetClosingDate(x.UHD.売上日.Year, x.UHD.売上日.Month, x.TOK.Ｔ締日 ?? 31, 0).ToShortDateString(),
                                売上区分 = x.KBN.表示名,
                                伝票番号 = x.UHD.伝票番号.ToString(),
                                元伝票番号 = string.Empty,
                                行番号 = x.UDTL.行番号,
                                得意先コード = string.Format("{0:D4} - {1:D2}", x.JIS.取引先コード, x.JIS.枝番),  // No.227,228 Add
                                得意先 = x.TOK != null ? x.TOK.略称名 : string.Empty,
                                品番コード = x.UDTL.品番コード,
                                自社品番 = x.HIN != null ? x.HIN.自社品番 : string.Empty,
                                自社品名 = !string.IsNullOrEmpty(x.UDTL.自社品名) ? x.UDTL.自社品名 :
                                                x.HIN != null ? x.HIN.自社品名 : string.Empty,                                  // No.390 Mod
                                自社色 = x.IRO != null ? x.IRO.色名称 : string.Empty,
                                賞味期限 = x.UDTL.賞味期限 == null ? null : x.UDTL.賞味期限.Value.ToShortDateString(),
                                単価 = x.UDTL.単価,
                                数量 = x.UDTL.数量,
                                単位 = x.UDTL.単位,
                                金額 = x.UDTL.金額 == null ? 0 : x.UHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ? (int)x.UDTL.金額 : (int)x.UDTL.金額 * -1,
                                摘要 = x.UDTL.摘要,
                                受注番号 = x.UHD.受注番号.ToString(),
                                納品伝票番号 = x.UHD.納品伝票番号.ToString(),
                            })
                            .ToList();

                    return resultList;

                }
                catch (System.ArgumentException agex)
                {
                    throw agex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }
        #endregion
    }

}