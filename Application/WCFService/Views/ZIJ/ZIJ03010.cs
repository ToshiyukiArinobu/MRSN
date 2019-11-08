using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 入金問合せサービスクラス
    /// </summary>
    public class ZIJ03010
    {
        #region 項目クラス定義

        /// <summary>
        /// ZIJ03010 入金問合せ 検索メンバー
        /// </summary>
        public class SearchDataMember
        {
            public string 伝票番号 { get; set; }
            public string 入金先自社コード { get; set; }
            public string 入金先自社名 { get; set; }
            public string 入金日 { get; set; }
            public string 入金元販社コード { get; set; }
            public string 入金元販社名 { get; set; }
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

        #region 入金問合せ検索処理
        /// <summary>
        /// 入金問合せ検索をおこなう
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
                            .Where(w => w.分類 == "随時" && w.機能 == "入金問合せ" && w.カテゴリ == "金種");

                    #region 入金基本情報を構成
                    var nkDataList =
                        context.T11_NYKNHD.Where(w => w.削除日時 == null && w.入金先自社コード == p自社コード)
                            .Join(context.T11_NYKNDTL.Where(w => w.削除日時 == null),
                                x => x.伝票番号,
                                y => y.伝票番号,
                                (x, y) => new { NHD = x, NDTL = y })
                            .Join(context.M70_JIS.Where(w => w.削除日時 == null),
                                x => x.NHD.入金先自社コード,
                                y => y.自社コード,
                                (x, y) => new { x.NHD, x.NDTL, JIS1 = y })
                            .GroupJoin(goldType,
                                x => x.NDTL.金種コード,
                                y => y.コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (a, b) => new { a.x.NHD, a.x.NDTL, a.x.JIS1, NM = b })
                            .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                                x => x.NHD.入金元販社コード,
                                y => y.自社コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (c, d) => new { c.x.NHD, c.x.NDTL, c.x.JIS1, c.x.NM, JIS2 = d })
                            .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                                x => new { code = x.NHD.得意先コード ?? -1, eda = x.NHD.得意先枝番 ?? -1 },
                                y => new { code = y.取引先コード, eda = y.枝番 },
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (e, f) => new { e.x.NHD, e.x.NDTL, e.x.JIS1, e.x.JIS2, e.x.NM, TOK = f })
                            .OrderBy(o => o.NHD.入金日)
                            .ThenBy(t => t.NDTL.伝票番号)
                            .ThenBy(t => t.NDTL.行番号)
                            .ToList();

                    #endregion

                    #region 検索条件絞込

                    // 入金日From
                    DateTime wkFromDate;
                    string fromDate = cond["入金日From"];
                    if (!string.IsNullOrEmpty(fromDate) && DateTime.TryParse(fromDate, out wkFromDate))
                        nkDataList = nkDataList.Where(w => w.NHD.入金日 >= wkFromDate).ToList();

                    // 入金日To
                    DateTime wkToDate;
                    string toDate = cond["入金日To"];
                    if (!string.IsNullOrEmpty(toDate) && DateTime.TryParse(toDate, out wkToDate))
                        nkDataList = nkDataList.Where(w => w.NHD.入金日 <= wkToDate).ToList();

                    // 入金元販社
                    int wkJisCode;
                    string jisCode = cond["入金元販社コード"];
                    if (!string.IsNullOrEmpty(jisCode) && int.TryParse(jisCode, out wkJisCode))
                        nkDataList = nkDataList.Where(w => w.NHD.入金元販社コード >= wkJisCode).ToList();

                    // 得意先
                    int wkCode, wkEda;
                    string tCode = cond["得意先コード"], tEda = cond["得意先枝番"];
                    if (!string.IsNullOrEmpty(tCode) && !string.IsNullOrEmpty(tEda))
                    {
                        if(int.TryParse(tCode, out wkCode) && int.TryParse(tEda, out wkEda))
                            nkDataList = nkDataList.Where(w => w.NHD.得意先コード == wkCode && w.NHD.得意先枝番 == wkEda).ToList();

                    }

                    // 金種コード
                    int wkGoldType;
                    string sGoldType = cond["金種コード"];
                    if(!string.IsNullOrEmpty(sGoldType) && int.TryParse(sGoldType, out wkGoldType))
                        if(wkGoldType > 0)
                            nkDataList = nkDataList.Where(w => w.NDTL.金種コード == wkGoldType).ToList();

                    #endregion

                    var resultList =
                        nkDataList
                            .ToList()
                            .Select(x => new SearchDataMember
                            {
                                伝票番号 = x.NDTL.伝票番号.ToString(),
                                入金先自社コード = x.NHD.入金先自社コード.ToString(),
                                入金先自社名 = x.JIS1.自社名 ?? "",
                                入金日 = x.NHD.入金日.ToString("yyyy/MM/dd"),
                                入金元販社コード = x.NHD.入金元販社コード.ToString(),
                                入金元販社名 = x.JIS2 == null ? "" : x.JIS2.自社名 ?? "",
                                得意先コード = string.Format("{0:D4}", x.NHD.得意先コード),     // No.223 Mod
                                得意先枝番 = string.Format("{0:D2}", x.NHD.得意先枝番),         // No.223 Mod
                                得意先名 = x.TOK == null ? "" : x.TOK.略称名 ?? "",  // No.229 Mod
                                金種コード = x.NDTL.金種コード.ToString(),
                                金種名 = x.NM == null ? "" : x.NM.表示名 ?? "",
                                金額 = x.NDTL.金額,
                                期日 = x.NDTL.期日 == null ? "" : ((DateTime)x.NDTL.期日).ToString("yyyy/MM/dd"),
                                摘要 = x.NDTL.摘要
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