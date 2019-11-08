using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 出金問合せサービスクラス
    /// </summary>
    public class ZIJ04010
    {
        #region 項目クラス定義

        /// <summary>
        /// ZIJ04010 出金問合せ 検索メンバー
        /// </summary>
        public class SearchDataMember
        {
            public string 伝票番号 { get; set; }
            public string 出金元自社コード { get; set; }
            public string 出金元自社名 { get; set; }
            public string 出金日 { get; set; }
            public string 出金先販社コード { get; set; }
            public string 出金先販社名 { get; set; }
            public string 得意先コード { get; set; }
            public string 得意先枝番 { get; set; }
            public string 得意先名 { get; set; }
            public string 金種コード { get; set; }
            public string 金種名 { get; set; }
            public int 金額 { get; set; }
            public string 期日 { get; set; }
            public string 摘要 { get; set; }
        }

        #endregion

        #region 出金問合せ検索処理
        /// <summary>
        /// 出金問合せ検索をおこなう
        /// </summary>
        /// <param name="p自社コード"></param>
        /// <param name="p自社販社区分"></param>
        /// <param name="cond">
        /// === 検索条件辞書 ===
        /// 入金日From
        /// 入金日To
        /// 入金元販社コード
        /// 得意先コード
        /// 得意先枝番
        /// 金種コード
        /// </param>
        /// <returns></returns>
        public List<SearchDataMember> GetDataList(int p自社コード, Dictionary<string, string> cond)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try
                {
                    // 金種(名称)データ取得
                    var goldType =
                        context.M99_COMBOLIST
                            .Where(w => w.分類 == "随時" && w.機能 == "出金問合せ" && w.カテゴリ == "金種");

                    #region 出金基本情報を構成
                    var payDataList =
                        context.T12_PAYHD.Where(w => w.削除日時 == null && w.出金元自社コード == p自社コード)
                            .Join(context.T12_PAYDTL.Where(w => w.削除日時 == null),
                                x => x.伝票番号,
                                y => y.伝票番号,
                                (x, y) => new { PHD = x, PDTL = y })
                            .Join(context.M70_JIS.Where(w => w.削除日時 == null),
                                x => x.PHD.出金元自社コード,
                                y => y.自社コード,
                                (x, y) => new { x.PHD, x.PDTL, JIS1 = y })
                            .GroupJoin(goldType,
                                x => x.PDTL.金種コード,
                                y => y.コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (a, b) => new { a.x.PHD, a.x.PDTL, a.x.JIS1, NM = b })
                            .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                                x => x.PHD.出金先販社コード,
                                y => y.自社コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (c, d) => new { c.x.PHD, c.x.PDTL, c.x.JIS1, c.x.NM, JIS2 = d })
                            .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                                x => new { code = x.PHD.得意先コード ?? -1, eda = x.PHD.得意先枝番 ?? -1 },
                                y => new { code = y.取引先コード, eda = y.枝番 },
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (e, f) => new { e.x.PHD, e.x.PDTL, e.x.JIS1, e.x.JIS2, e.x.NM, TOK = f })
                            .OrderBy(o => o.PHD.出金日)
                            .ThenBy(t => t.PDTL.伝票番号)
                            .ThenBy(t => t.PDTL.行番号)
                            .ToList();

                    #endregion

                    #region 検索条件絞込

                    // 入金日From
                    DateTime wkFromDate;
                    string fromDate = cond["出金日From"];
                    if (!string.IsNullOrEmpty(fromDate) && DateTime.TryParse(fromDate, out wkFromDate))
                        payDataList = payDataList.Where(w => w.PHD.出金日 >= wkFromDate).ToList();

                    // 入金日To
                    DateTime wkToDate;
                    string toDate = cond["出金日To"];
                    if (!string.IsNullOrEmpty(toDate) && DateTime.TryParse(toDate, out wkToDate))
                        payDataList = payDataList.Where(w => w.PHD.出金日 <= wkToDate).ToList();

                    // 入金元販社
                    int wkJisCode;
                    string jisCode = cond["出金先販社コード"];
                    if (!string.IsNullOrEmpty(jisCode) && int.TryParse(jisCode, out wkJisCode))
                        payDataList = payDataList.Where(w => w.PHD.出金先販社コード >= wkJisCode).ToList();

                    // 得意先
                    int wkCode, wkEda;
                    string tCode = cond["得意先コード"], tEda = cond["得意先枝番"];
                    if (!string.IsNullOrEmpty(tCode) && !string.IsNullOrEmpty(tEda))
                    {
                        if (int.TryParse(tCode, out wkCode) && int.TryParse(tEda, out wkEda))
                            payDataList = payDataList.Where(w => w.PHD.得意先コード == wkCode && w.PHD.得意先枝番 == wkEda).ToList();

                    }

                    // 金種コード
                    int wkGoldType;
                    string sGoldType = cond["金種コード"];
                    if (!string.IsNullOrEmpty(sGoldType) && int.TryParse(sGoldType, out wkGoldType))
                        if (wkGoldType > 0)
                            payDataList = payDataList.Where(w => w.PDTL.金種コード == wkGoldType).ToList();

                    #endregion

                    var resultList =
                        payDataList
                            .ToList()
                            .Select(x => new SearchDataMember
                            {
                                伝票番号 = x.PDTL.伝票番号.ToString(),
                                出金元自社コード = x.PHD.出金元自社コード.ToString(),
                                出金元自社名 = x.JIS1.自社名 ?? "",
                                出金日 = x.PHD.出金日.ToString("yyyy/MM/dd"),
                                出金先販社コード = x.PHD.出金先販社コード.ToString(),
                                出金先販社名 = x.JIS2 == null ? "" : x.JIS2.自社名 ?? "",
                                得意先コード = string.Format("{0:D4}", x.PHD.得意先コード),     // No.223 Mod
                                得意先枝番 = string.Format("{0:D2}", x.PHD.得意先枝番),         // No.223 Mod
                                得意先名 = x.TOK == null ? "" : x.TOK.略称名 ?? "",
                                金種コード = x.PDTL.金種コード.ToString(),
                                金種名 = x.NM == null ? "" : x.NM.表示名 ?? "",
                                金額 = x.PDTL.金額,
                                期日 = x.PDTL.期日 == null ? "" : ((DateTime)x.PDTL.期日).ToString("yyyy/MM/dd"),
                                摘要 = x.PDTL.摘要
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