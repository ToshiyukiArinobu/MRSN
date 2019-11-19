using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 揚り明細問合せサービスクラス
    /// </summary>
    public class ZIJ10010
    {
        #region 項目クラス定義

        /// <summary>
        /// ZIJ05010 請求明細問合せ 検索メンバー
        /// </summary>
        public class SearchDataMember
        {
            public int 会社名コード { get; set; }      // No.227,228 Add
            public string 自社名 { get; set; }         // No.227,228 Add
            public string 仕上日 { get; set; }         // No.130-2 Mod
            public string 加工区分 { get; set; }
            public string 伝票番号 { get; set; }
            public int 行番号 { get; set; }
            public string 外注先コード { get; set; }   // No.227,228 Add
            public string 外注先 { get; set; }
            public int 品番コード { get; set; }
            public string 自社品番 { get; set; }
            public string 自社品名 { get; set; }
            public string 自社色 { get; set; }
            public string 賞味期限 { get; set; }        // No.130-2 Mod
            public decimal 単価 { get; set; }
            public decimal 数量 { get; set; }
            public string 単位 { get; set; }
            public int 金額 { get; set; }
            public string 摘要 { get; set; }
        }

        #endregion

        #region 揚り明細問合せ検索処理
        /// <summary>
        /// 揚り明細問合せ検索をおこなう
        /// </summary>
        /// <param name="p自社コード"></param>
        /// <param name="p自社販社区分"></param>
        /// <param name="cond">
        /// === 検索条件辞書 ===
        /// 揚り日From
        /// 揚り日To
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
            DateTime? salesDateFrom = DateTime.TryParse(cond["仕上日From"], out wkDt) ? wkDt : (DateTime?)null,
                salesDateTo = DateTime.TryParse(cond["仕上日To"], out wkDt) ? wkDt : (DateTime?)null;
            int salesKbn = int.Parse(cond["加工区分"]);
            int? customerCode = int.TryParse(cond["加工先コード"], out wkVal) ? wkVal : (int?)null,
                customerEda = int.TryParse(cond["加工先枝番"], out wkVal) ? wkVal : (int?)null;

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try
                {
                    // 加工区分(名称)データ取得
                    var salesKbnData =
                        context.M99_COMBOLIST
                            .Where(w => w.分類 == "日次" && w.機能 == "揚り入力" && w.カテゴリ == "加工区分");

                    var agrData =
                        context.T04_AGRHD.Where(w => w.削除日時 == null && w.会社名コード == p自社コード)
                            .Join(context.T04_AGRDTL.Where(w => w.削除日時 == null),
                                x => x.伝票番号,
                                y => y.伝票番号,
                                (x, y) => new { AGRHD = x, AGRDTL = y });

                    #region 検索条件による絞込み

                    if (salesDateFrom != null)
                        agrData = agrData.Where(x => x.AGRHD.仕上り日 >= salesDateFrom);

                    if (salesDateTo != null)
                        agrData = agrData.Where(x => x.AGRHD.仕上り日 <= salesDateTo);

                    if (salesKbn > 0)
                        agrData = agrData.Where(w => w.AGRHD.加工区分 == salesKbn);

                    if (customerCode != null && customerEda != null)
                        agrData = agrData.Where(w => w.AGRHD.外注先コード == customerCode && w.AGRHD.外注先枝番 == customerEda);

                    #endregion


                    var resultList =
                        agrData
                            .Join(salesKbnData,
                                x => x.AGRHD.加工区分,
                                y => y.コード,
                                (x, y) => new { AGRHD = x.AGRHD, AGRDTL = x.AGRDTL, KBN = y })
                            .GroupJoin(context.M01_TOK,
                                x => new { code = x.AGRHD.外注先コード, eda = x.AGRHD.外注先枝番 },
                                y => new { code = y.取引先コード, eda = y.枝番 },
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (a, b) => new { AGRHD = a.x.AGRHD, AGRDTL = a.x.AGRDTL, a.x.KBN, TOK = b })
                            .GroupJoin(context.M09_HIN,
                                x => x.AGRDTL.品番コード,
                                y => y.品番コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (c, d) => new { AGRHD = c.x.AGRHD, AGRDTL = c.x.AGRDTL, c.x.KBN, c.x.TOK, HIN = d })
                            .GroupJoin(context.M06_IRO,
                                x => x.HIN.自社色,
                                y => y.色コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (e, f) => new { AGRHD = e.x.AGRHD, AGRDTL = e.x.AGRDTL, e.x.KBN, e.x.TOK, e.x.HIN, IRO = f })
                            .GroupJoin(context.M70_JIS.Where(x => x.削除日時 == null),
                                x => x.AGRHD.会社名コード,
                                y => y.自社コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (g, h) => new { AGRHD = g.x.AGRHD, AGRDTL = g.x.AGRDTL, g.x.KBN, g.x.TOK, g.x.HIN, g.x.IRO, JIS = h })
                            .OrderBy(o => o.AGRHD.外注先コード)
                            .ThenBy(t => t.AGRHD.外注先枝番)
                            .ThenBy(t => t.AGRHD.仕上り日)
                            .ThenBy(t => t.AGRDTL.伝票番号)
                            .ThenBy(t => t.AGRDTL.行番号)
                            .ToList()
                            .Select(x => new SearchDataMember
                            {
                                会社名コード = x.AGRHD.会社名コード,               // No.227,228 Add
                                自社名 = x.JIS.自社名 ?? "",                        // No.227,228 Add
                                仕上日 = x.AGRHD.仕上り日.ToShortDateString(),     // No.130-2 Mod
                                加工区分 = x.KBN.表示名,
                                伝票番号 = x.AGRHD.伝票番号.ToString(),
                                行番号 = x.AGRDTL.行番号,
                                外注先コード = string.Format("{0:D4} - {1:D2}", x.AGRHD.外注先コード, x.AGRHD.外注先枝番),   // No.227,228 Add
                                外注先 = x.TOK != null ? x.TOK.略称名 : (x.KBN.コード == 3 ? "自社" : string.Empty),
                                品番コード = x.AGRDTL.品番コード,
                                自社品番 = x.HIN != null ? x.HIN.自社品番 : string.Empty,
                                自社品名 = x.HIN != null ? x.HIN.自社品名 : string.Empty,
                                自社色 = x.IRO != null ? x.IRO.色名称 : string.Empty,
                                賞味期限 = x.AGRDTL.賞味期限 == null ? null : x.AGRDTL.賞味期限.Value.ToShortDateString(),   // No.130-2 Mod
                                単価 = x.AGRDTL.単価 == null ? 0 : (decimal)x.AGRDTL.単価,
                                数量 = x.AGRDTL.数量 == null ? 0 : (decimal)x.AGRDTL.数量,
                                単位 = x.AGRDTL.単位,
                                金額 = x.AGRDTL.金額 ?? 0,
                                摘要 = x.AGRDTL.摘要,
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