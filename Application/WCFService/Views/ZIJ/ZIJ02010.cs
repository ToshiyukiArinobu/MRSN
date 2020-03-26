using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 仕入明細問合せサービスクラス
    /// </summary>
    public class ZIJ02010
    {
        #region 項目クラス定義

        /// <summary>
        /// ZIJ02010 仕入明細問合せ 検索メンバー
        /// </summary>
        public class SearchDataMember
        {
            public int 会社名コード { get; set; }
            public string 自社名 { get; set; }         // No.227,228 Add
            public string 仕入日 { get; set; }         // No.130-3 Mod
            public string 支払日 { get; set; }         // No.130-3 Mod
            public int? 仕入区分コード { get; set; }   //No.396 Add
            public string 仕入区分 { get; set; }
            public string 入力区分 { get; set; }
            public int 伝票番号 { get; set; }        // No.200 Mod
            public string 元伝票番号 { get; set; }      // No.200 Mod
            public int 行番号 { get; set; }
            public string 仕入先コード { get; set; }    // No.227,228 Add
            public string 仕入先名 { get; set; }
            public string 入荷先名 { get; set; }        //No.396 Add
            public int 品番コード { get; set; }
            public string 自社品番 { get; set; }
            public string 自社品名 { get; set; }
            public string 自社色 { get; set; }
            public string 賞味期限 { get; set; }        // No.130-3 Mod
            public decimal 単価 { get; set; }
            public decimal 数量 { get; set; }
            public string 単位 { get; set; }
            public int 金額 { get; set; }
            public string 摘要 { get; set; }
            public int? 発注番号 { get; set; }
            public int 消費税 { get; set; }             // No.396 Add
        }

        #endregion

        #region 仕入明細問合せ検索処理
        /// <summary>
        /// 仕入明細問合せ検索をおこなう
        /// </summary>
        /// <param name="p自社コード"></param>
        /// <param name="cond">
        /// === 検索条件辞書 ===
        /// 仕入日From - 仕入日To
        /// 入金日From - 入金日To
        /// 入力区分
        /// 仕入先コード - 仕入先枝番
        /// 入荷先コード
        /// </param>
        /// <returns></returns>
        public List<SearchDataMember> GetDataList(int p自社コード, Dictionary<string, string> cond)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try
                {
                    #region パラメータの型変換

                    DateTime?
                        sDateFrom = stringToDate(cond, "仕入日From"),
                        sDateTo = stringToDate(cond, "仕入日To"),
                        nDateFrom = stringToDate(cond, "入金日From"),
                        nDateTo = stringToDate(cond, "入金日To");

                    int ival;
                    int? inputType = int.TryParse(cond["入力区分"], out ival) ? (ival >= 0 ? ival : (int?)null) : (int?)null;
                    int?
                        supCode = int.TryParse(cond["仕入先コード"], out ival) ? ival : (int?)null,
                        supEda = int.TryParse(cond["仕入先枝番"], out ival) ? ival : (int?)null,
                        arrivalCode = int.TryParse(cond["入荷先コード"], out ival) ? ival : (int?)null;

                    string hinban = cond["自社品番"];　             // No.396 Add

                    #endregion

                    // 基本情報取得
                    var srDataList =
                        context.T03_SRHD.Where(w => w.削除日時 == null && w.会社名コード == p自社コード)
                            .Join(context.T03_SRDTL.Where(w => w.削除日時 == null),
                                x => x.伝票番号,
                                y => y.伝票番号,
                                (x, y) => new { SHD = x, SDTL = y })
                            .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                                x => x.SDTL.品番コード,
                                y => y.品番コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new { x.x.SHD, x.x.SDTL, HIN = y });

                    #region 条件絞込

                    // 仕入日From - To
                    if (sDateFrom != null)
                        srDataList = srDataList.Where(w => w.SHD.仕入日 >= sDateFrom);

                    if (sDateTo != null)
                        srDataList = srDataList.Where(w => w.SHD.仕入日 <= sDateTo);

                    // 入金日From - To
                    // TODO:保留

                    // 入力区分
                    if (inputType != null)
                        srDataList = srDataList.Where(w => w.SHD.入力区分 == inputType);

                    // 仕入先
                    if (supCode != null && supEda != null)
                        srDataList = srDataList.Where(w => w.SHD.仕入先コード == supCode && w.SHD.仕入先枝番 == supEda);

                    // 入荷先
                    if (arrivalCode != null)
                        srDataList = srDataList.Where(w => w.SHD.入荷先コード == arrivalCode);

                    // 自社品番
                    if (!string.IsNullOrEmpty(hinban))
                        srDataList = srDataList.Where(w => w.HIN.自社品番 == hinban); 　    // No.396 Add

                    #endregion

                    // 返品分のデータを取得する
                    //var returnList = srDataList.Where(w => w.SRHD.仕入区分 == (int)CommonConstants.仕入区分.返品).ToList();

                    #region 各名称を取得して検索メンバークラスに整形
                    var resultList =
                        srDataList
                            .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                                x => new { code = x.SHD.仕入先コード, eda = x.SHD.仕入先枝番 },
                                y => new { code = y.取引先コード, eda = y.枝番 },
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (a, b) => new { a.x.SHD, a.x.SDTL, a.x.HIN, TOK = b })
                            .GroupJoin(context.M06_IRO,
                                x => x.HIN.自社色,
                                y => y.色コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (e, f) => new { e.x.SHD, e.x.SDTL, e.x.TOK, e.x.HIN, IRO = f })
                            .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                                x => x.SHD.会社名コード,
                                y => y.自社コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (g, h) => new { g.x.SHD, g.x.SDTL, g.x.TOK, g.x.HIN, g.x.IRO, JIS = h })
                        // 入荷先
                            .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                                x => x.SHD.入荷先コード,
                                y => y.自社コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (e, f) => new { e.x.SHD, e.x.SDTL, e.x.TOK, e.x.HIN, e.x.IRO, e.x.JIS, JIS2 = f })
                            .ToList()
                            .Select(x => new SearchDataMember
                            {
                                会社名コード = x.SHD.会社名コード,
                                自社名 = x.JIS.自社名 ?? "",                  // No.227,228 Add
                                仕入日 = x.SHD.仕入日.ToShortDateString(),    // No.130-3 Mod
                                // No-128 Mod Start
                                支払日 = x.TOK.Ｓ入金日１ == 0 ? string.Empty : x.SHD.仕入日.Day >= (x.TOK.Ｓ締日 ?? 31) ?
                                    // No.101-5 Mod Start
                                    new DateTime(x.SHD.仕入日.Year, x.SHD.仕入日.Month, ((x.TOK.Ｓ入金日１ ?? 31) >= 28 ? DateTime.DaysInMonth(x.SHD.仕入日.Year, x.SHD.仕入日.Month) : x.TOK.Ｓ入金日１ ?? 31)).AddMonths((x.TOK.Ｓサイト１ ?? 0) + 1).ToShortDateString() :  // No.130-3 Mod
                                    new DateTime(x.SHD.仕入日.Year, x.SHD.仕入日.Month, ((x.TOK.Ｓ入金日１ ?? 31) >= 28 ? DateTime.DaysInMonth(x.SHD.仕入日.Year, x.SHD.仕入日.Month) : x.TOK.Ｓ入金日１ ?? 31)).AddMonths(x.TOK.Ｓサイト１ ?? 0).ToShortDateString(),         // No.130-3 Mod
                                // No.101-5 Mod End
                                // No-128 Mod End
                                仕入区分コード = x.SHD.仕入区分, 　    // No.396 Add
                                仕入区分 = x.SHD.仕入区分 == (int)CommonConstants.仕入区分.通常 ? CommonConstants.仕入区分_通常 :
                                           x.SHD.仕入区分 == (int)CommonConstants.仕入区分.返品 ? CommonConstants.仕入区分_返品 :
                                           string.Empty,
                                入力区分 = x.SHD.入力区分 == (int)CommonConstants.入力区分.仕入入力 ? CommonConstants.入力区分_仕入入力 :
                                           x.SHD.入力区分 == (int)CommonConstants.入力区分.売上入力 ? CommonConstants.入力区分_売上入力 :
                                           string.Empty,
                                伝票番号 = x.SHD.伝票番号,
                                元伝票番号 = x.SHD.元伝票番号 != null ? x.SHD.元伝票番号.ToString() : string.Empty,
                                行番号 = x.SDTL.行番号,
                                仕入先コード = string.Format("{0:D4} - {1:D2}", x.SHD.仕入先コード, x.SHD.仕入先枝番),   // No.227,228
                                仕入先名 = x.TOK.略称名,
                                入荷先名 = x.JIS2 != null ? x.JIS2.自社名 : "", 　    // No.396 Add
                                品番コード = x.SDTL.品番コード,
                                自社品番 = x.HIN.自社品番,
                                自社品名 = x.HIN.自社品名,
                                自社色 = x.IRO != null ? x.IRO.色名称 : string.Empty,
                                賞味期限 = x.SDTL.賞味期限 == null ? null : x.SDTL.賞味期限.Value.ToShortDateString(),    // No.130-3 Mod
                                数量 = x.SHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ? x.SDTL.数量 : x.SDTL.数量 * -1,
                                単価 = x.SDTL.単価,
                                単位 = x.SDTL.単位,
                                金額 = x.SHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ? x.SDTL.金額 : x.SDTL.金額 * -1, 　    // No.396 Mod
                                消費税 = x.SHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ? (x.SHD.消費税 ?? 0) : (x.SHD.消費税 ?? 0) * -1, 　    // No.396 Add
                                摘要 = x.SDTL.摘要,
                                発注番号 = x.SHD.発注番号
                            })
                            .OrderBy(o => o.仕入日)
                            .ThenBy(t => t.伝票番号)
                            .ThenBy(t => t.行番号)
                            .ThenBy(t => t.会社名コード)
                            .ToList();
                    #endregion

                    if (inputType == null || inputType != CommonConstants.入力区分.仕入入力.GetHashCode())
                    {
                        // 販社データ取得
                        var hanList = GetHanDataList(context, p自社コード, cond);
                        // 既存リストに追加
                        resultList.AddRange(hanList);
                        // リスト追加後にソート実施
                        resultList = resultList.OrderBy(o => o.仕入日).ThenBy(t => t.伝票番号).ThenBy(t => t.行番号).ThenBy(t => t.会社名コード).ToList();

                    }

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

        /// <summary>
        /// 販社仕入情報を取得する
        /// </summary>
        /// <param name="context"></param>
        /// <param name="p自社コード"></param>
        /// <param name="cond"></param>
        /// <returns></returns>
        private List<SearchDataMember> GetHanDataList(TRAC3Entities context, int p自社コード, Dictionary<string, string> cond)
        {
            try
            {
                #region パラメータの型変換

                DateTime?
                    sDateFrom = stringToDate(cond, "仕入日From"),
                    sDateTo = stringToDate(cond, "仕入日To"),
                    nDateFrom = stringToDate(cond, "入金日From"),
                    nDateTo = stringToDate(cond, "入金日To");

                int ival;
                int? inputType = int.TryParse(cond["入力区分"], out ival) ? (ival >= 0 ? ival : (int?)null) : (int?)null;
                int?
                    supCode = int.TryParse(cond["仕入先コード"], out ival) ? ival : (int?)null,
                    supEda = int.TryParse(cond["仕入先枝番"], out ival) ? ival : (int?)null,
                    arrivalCode = int.TryParse(cond["入荷先コード"], out ival) ? ival : (int?)null;
                string hinban = cond["自社品番"];
                #endregion

                // 基本情報の取得
                var srDataList =
                    context.T03_SRHD_HAN.Where(w => w.削除日時 == null && w.会社名コード == p自社コード)
                        .Join(context.T03_SRDTL_HAN.Where(w => w.削除日時 == null),
                            x => x.伝票番号,
                            y => y.伝票番号,
                            (x, y) => new { SRHD = x, SRDTL = y })
                        .GroupJoin(context.T02_URHD.Where(w => w.削除日時 == null),
                            x => x.SRHD.伝票番号,
                            y => y.伝票番号,
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(),
                            (e, f) => new { e.x.SRHD, e.x.SRDTL, URHD = f })
                        .Where(w =>
                            w.URHD.売上区分 != (int)CommonConstants.売上区分.メーカー販社商流直送 &&
                            w.URHD.売上区分 != (int)CommonConstants.売上区分.メーカー販社商流直送返品)
                    // 商品マスタ
                        .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                            x => x.SRDTL.品番コード,
                            y => y.品番コード,
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(), (a, b) => new { a.x.SRHD, a.x.SRDTL, a.x.URHD, HIN = b })
                    // 色名称
                        .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null),
                            x => x.HIN.自社色,
                            y => y.色コード,
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(),
                           (c, d) => new { c.x.SRHD, c.x.SRDTL, c.x.URHD, c.x.HIN, IRO = d });

                #region 条件絞込

                // 仕入日From - To
                if (sDateFrom != null)
                    srDataList = srDataList.Where(w => w.SRHD.仕入日 >= sDateFrom);

                if (sDateTo != null)
                    srDataList = srDataList.Where(w => w.SRHD.仕入日 <= sDateTo);

                // 入金日From - To
                // TODO:保留

                // 入力区分
                // REMARKS:販社ヘッダには入力区分が無いので未指定(売上入力固定)

                // 仕入先
                // REMARKS:画面からの仕入先指定は不可

                // 入荷先
                if (arrivalCode != null)
                    srDataList = srDataList.Where(w => w.SRHD.入荷先コード == arrivalCode);

                // 自社品番
                if (!string.IsNullOrEmpty(hinban))
                    srDataList = srDataList.Where(w => w.HIN.自社品番 == hinban);

                #endregion

                #region 各名称を取得して検索メンバークラスに整形
                var resultList =
                    srDataList
                    // 会社名
                        .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                            x => x.SRHD.会社名コード,
                            y => y.自社コード,
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(),
                            (a, b) => new { a.x.SRHD, a.x.SRDTL, a.x.URHD, a.x.HIN, a.x.IRO, JIS = b })
                    // 仕入先
                        .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                            x => x.SRHD.仕入先コード,
                            y => y.自社コード,
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(),
                            (c, d) => new { c.x.SRHD, c.x.SRDTL, c.x.URHD, c.x.HIN, c.x.IRO, c.x.JIS, SJIS = d })
                    // 入荷先
                        .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                            x => x.SRHD.入荷先コード,
                            y => y.自社コード,
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(),
                            (e, f) => new { e.x.SRHD, e.x.SRDTL, e.x.URHD, e.x.HIN, e.x.IRO, e.x.JIS, e.x.SJIS, NJIS = f })
                        .ToList()
                        .Select(x => new SearchDataMember
                        {

                            会社名コード = x.SRHD.会社名コード,
                            自社名 = x.JIS != null ? x.JIS.自社名 : "",
                            仕入日 = x.SRHD.仕入日.ToString("yyyy/MM/dd"),
                            支払日 = "",// TODO:一応足しておく
                            仕入区分コード = x.SRHD.仕入区分,
                            仕入区分 = x.SRHD.仕入区分 == (int)CommonConstants.仕入区分.通常 ? CommonConstants.仕入区分_通常 :
                                       x.SRHD.仕入区分 == (int)CommonConstants.仕入区分.返品 ? CommonConstants.仕入区分_返品 :
                                       string.Empty,
                            //入力区分 = CommonConstants.入力区分.売上入力.GetHashCode().ToString(),
                            入力区分 = CommonConstants.Get入力区分Dic()[CommonConstants.入力区分.売上入力.GetHashCode()],
                            伝票番号 = x.SRHD.伝票番号,
                            元伝票番号 = x.URHD != null ? x.URHD.元伝票番号.ToString() : string.Empty,
                            行番号 = x.SRDTL.行番号,
                            仕入先コード = x.SRHD.仕入先コード.ToString(),
                            仕入先名 = x.SJIS != null ? x.SJIS.自社名 : "",
                            入荷先名 = x.NJIS != null ? x.NJIS.自社名 : "",
                            品番コード = x.SRDTL.品番コード,
                            自社品番 = x.HIN != null ? x.HIN.自社品番 : string.Empty,
                            自社品名 = x.HIN != null ? x.HIN.自社品名 : string.Empty,
                            自社色 = x.IRO != null ? x.IRO.色名称 : string.Empty,
                            賞味期限 = x.SRDTL.賞味期限 == null ? null : x.SRDTL.賞味期限.Value.ToShortDateString(),
                            数量 = x.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ? x.SRDTL.数量 : x.SRDTL.数量 * -1,
                            単価 = x.SRDTL.単価,
                            単位 = x.SRDTL.単位,
                            金額 = x.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ? x.SRDTL.金額 : x.SRDTL.金額 * -1,
                            消費税 = x.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ? (x.SRHD.消費税 ?? 0) : (x.SRHD.消費税 ?? 0) * -1,
                            摘要 = x.SRDTL.摘要,
                            発注番号 = x.SRHD.発注番号,
                        }).ToList();

                #endregion

                return resultList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cond"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        private DateTime? stringToDate(Dictionary<string, string> cond, string keyName)
        {
            string wkDateString = cond[keyName];
            DateTime dt;
            DateTime? wkDate = DateTime.TryParse(wkDateString, out dt) ? dt : (DateTime?)null;

            return wkDate;

        }

    }

}