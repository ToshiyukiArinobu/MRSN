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
            public DateTime 売上日 { get; set; }
            public DateTime 請求日 { get; set; }
            public string 売上区分 { get; set; }
            public string 伝票番号 { get; set; }
            public string 元伝票番号 { get; set; }
            public int 行番号 { get; set; }
            public string 得意先 { get; set; }
            public int 品番コード { get; set; }
            public string 自社品番 { get; set; }
            public string 自社品名 { get; set; }
            public string 自社色 { get; set; }
            public DateTime? 賞味期限 { get; set; }
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
            // 画面パラメータを展開
            DateTime wkDt;
            int wkVal;
            DateTime? salesDateFrom = DateTime.TryParse(cond["売上日From"], out wkDt) ? wkDt : (DateTime?)null,
                salesDateTo = DateTime.TryParse(cond["売上日To"], out wkDt) ? wkDt : (DateTime?)null;
            int salesKbn = int.Parse(cond["売上区分"]);
            int? customerCode = int.TryParse(cond["得意先コード"], out wkVal) ? wkVal : (int?)null,
                customerEda = int.TryParse(cond["得意先枝番"], out wkVal) ? wkVal : (int?)null;

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
                                (x, y) => new { UHD = x, UDTL = y });

                    #region 検索条件による絞込み

                    if (salesDateFrom != null)
                        urData = urData.Where(x => x.UHD.売上日 >= salesDateFrom);

                    if (salesDateTo != null)
                        urData = urData.Where(x => x.UHD.売上日 <= salesDateTo);

                    if (salesKbn > 0)
                        urData = urData.Where(w => w.UHD.売上区分 == salesKbn);

                    if (customerCode != null && customerEda != null)
                        urData = urData.Where(w => w.UHD.得意先コード == customerCode && w.UHD.得意先枝番 == customerEda);

                    #endregion


                    var resultList =
                        urData
                            .Join(salesKbnData,
                                x => x.UHD.売上区分,
                                y => y.コード,
                                (x, y) => new { x.UHD, x.UDTL, KBN = y })
                            .GroupJoin(context.M01_TOK,
                                x => new { code = x.UHD.得意先コード, eda = x.UHD.得意先枝番 },
                                y => new { code = y.取引先コード, eda = y.枝番 },
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (a, b) => new { a.x.UHD, a.x.UDTL, a.x.KBN, TOK = b })
                            .GroupJoin(context.M09_HIN,
                                x => x.UDTL.品番コード,
                                y => y.品番コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (c, d) => new { c.x.UHD, c.x.UDTL, c.x.KBN, c.x.TOK, HIN = d })
                            .GroupJoin(context.M06_IRO,
                                x => x.HIN.自社色,
                                y => y.色コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (e, f) => new { e.x.UHD, e.x.UDTL, e.x.KBN, e.x.TOK, e.x.HIN, IRO = f })
                            .OrderBy(o => o.UHD.得意先コード)
                            .ThenBy(t => t.UHD.得意先枝番)
                            .ThenBy(t => t.UHD.売上日)
                            .ThenBy(t => t.UHD.伝票番号)                    // No-120 Add
                            .ThenBy(t => t.UDTL.行番号)
                            .ToList()
                            .Select(x => new SearchDataMember
                            {
                                売上日 = x.UHD.売上日,
                                請求日 = x.UHD.売上日.Day >= x.TOK.Ｔ締日 ?
                                    AppCommon.GetClosingDate(x.UHD.売上日.Year, x.UHD.売上日.Month, x.TOK.Ｔ締日 ?? 31, 1) :
                                    AppCommon.GetClosingDate(x.UHD.売上日.Year, x.UHD.売上日.Month, x.TOK.Ｔ締日 ?? 31, 0),
                                売上区分 = x.KBN.表示名,
                                伝票番号 = x.UHD.伝票番号.ToString(),
                                元伝票番号 = x.UHD.元伝票番号 != null ? x.UHD.元伝票番号.ToString() : string.Empty,
                                行番号 = x.UDTL.行番号,
                                得意先 = x.TOK != null ? x.TOK.得意先名１ : string.Empty,
                                品番コード = x.UDTL.品番コード,
                                自社品番 = x.HIN != null ? x.HIN.自社品番 : string.Empty,
                                自社品名 = x.HIN != null ? x.HIN.自社品名 : string.Empty,
                                自社色 = x.IRO != null ? x.IRO.色名称 : string.Empty,
                                賞味期限 = x.UDTL.賞味期限,
                                単価 = x.UDTL.単価,
                                数量 = x.UDTL.数量,
                                単位 = x.UDTL.単位,
                                金額 = x.UDTL.金額 ?? 0,
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