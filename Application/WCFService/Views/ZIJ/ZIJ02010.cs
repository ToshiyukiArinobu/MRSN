using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 支払明細問合せサービスクラス
    /// </summary>
    public class ZIJ02010
    {
        #region 項目クラス定義

        /// <summary>
        /// ZIJ02010 支払明細問合せ 検索メンバー
        /// </summary>
        public class SearchDataMember
        {
            public int 会社名コード { get; set; }
            public DateTime 仕入日 { get; set; }
            public DateTime 支払日 { get; set; }
            public string 仕入区分 { get; set; }
            public string 入力区分 { get; set; }
            public int 伝票番号 { get; set; }
            public int? 元伝票番号 { get; set; }
            public int 行番号 { get; set; }
            public string 仕入先名 { get; set; }
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
            public int? 発注番号 { get; set; }
        }

        #endregion

        #region 支払明細問合せ検索処理
        /// <summary>
        /// 支払明細問合せ検索をおこなう
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

                    #endregion

                    // 基本情報取得
                    var srDataList =
                        context.T03_SRHD.Where(w => w.削除日時 == null && w.会社名コード == p自社コード)
                            .Join(context.T03_SRDTL.Where(w => w.削除日時 == null),
                                x => x.伝票番号,
                                y => y.伝票番号,
                                (x, y) => new { SHD = x, SDTL = y });

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
                                (a, b) => new { a.x.SHD, a.x.SDTL, TOK = b })
                            .GroupJoin(context.M09_HIN,
                                x => x.SDTL.品番コード,
                                y => y.品番コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (c, d) => new { c.x.SHD, c.x.SDTL, c.x.TOK, HIN = d })
                            .GroupJoin(context.M06_IRO,
                                x => x.HIN.自社色,
                                y => y.色コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (e, f) => new { e.x.SHD, e.x.SDTL, e.x.TOK, e.x.HIN, IRO = f })
                            .ToList()
                            .Select(x => new SearchDataMember
                            {
                                会社名コード = x.SHD.会社名コード,
                                仕入日 = x.SHD.仕入日,
                                // No-128 Mod Start
                                支払日 = x.SHD.仕入日.Day >= (x.TOK.Ｓ締日 ?? 31) ?
                                    // No.101-5 Mod Start
                                    new DateTime(x.SHD.仕入日.Year, x.SHD.仕入日.Month, ((x.TOK.Ｓ入金日１ ?? 31) >= 28 ? DateTime.DaysInMonth(x.SHD.仕入日.Year, x.SHD.仕入日.Month) : x.TOK.Ｓ入金日１ ?? 31)).AddMonths((x.TOK.Ｓサイト１ ?? 0) + 1) :
                                    new DateTime(x.SHD.仕入日.Year, x.SHD.仕入日.Month, ((x.TOK.Ｓ入金日１ ?? 31) >= 28 ? DateTime.DaysInMonth(x.SHD.仕入日.Year, x.SHD.仕入日.Month) : x.TOK.Ｓ入金日１ ?? 31)).AddMonths(x.TOK.Ｓサイト１ ?? 0),
                                    // No.101-5 Mod End
                                // No-128 Mod End
                                仕入区分 = x.SHD.仕入区分 == (int)CommonConstants.仕入区分.通常 ? CommonConstants.仕入区分_通常 :
                                           x.SHD.仕入区分 == (int)CommonConstants.仕入区分.返品 ? CommonConstants.仕入区分_返品 :
                                           string.Empty,
                                入力区分 = x.SHD.入力区分 == (int)CommonConstants.入力区分.仕入入力 ? CommonConstants.入力区分_仕入入力 :
                                           x.SHD.入力区分 == (int)CommonConstants.入力区分.売上入力 ? CommonConstants.入力区分_売上入力 :
                                           string.Empty,
                                伝票番号 = x.SHD.伝票番号,
                                元伝票番号 = x.SHD.元伝票番号,
                                行番号 = x.SDTL.行番号,
                                仕入先名 = x.TOK.得意先名１,
                                品番コード = x.SDTL.品番コード,
                                自社品番 = x.HIN.自社品番,
                                自社品名 = x.HIN.自社品名,
                                自社色 = x.IRO != null ? x.IRO.色名称 : string.Empty,
                                賞味期限 = x.SDTL.賞味期限,
                                数量 = x.SHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ? x.SDTL.数量 : x.SDTL.数量 * -1,
                                単価 = x.SHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ? x.SDTL.単価 : x.SDTL.単価 * -1,
                                単位 = x.SDTL.単位,
                                金額 = x.SDTL.金額,
                                摘要 = x.SDTL.摘要,
                                発注番号 = x.SHD.発注番号
                            })
                            .OrderBy(o => o.仕入日)
                            .ThenBy(t => t.伝票番号)
                            .ThenBy(t => t.行番号)
                            .ThenBy(t => t.会社名コード)
                            .ToList();
                    #endregion

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
            /*
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

                #endregion

                // 基本情報の取得
                var srDataList =
                    context.T03_SRHD_HAN.Where(w => w.削除日時 == null && w.会社名コード == p自社コード)
                        .Join(context.T03_SRDTL_HAN
                            .Where(w => w.削除日時 == null)
                            .GroupBy(g => new { g.伝票番号 })
                            .Select(x => new { x.Key.伝票番号, 合計金額 = x.Sum(s => s.金額) }),
                        x => x.伝票番号,
                        y => y.伝票番号,
                        (x, y) => new { SRHD = x, SRDTL = y })
                        .GroupJoin(context.T02_URHD.Where(w => w.削除日時 == null),
                            x => x.SRHD.伝票番号,
                            y => y.伝票番号,
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(),
                            (a, b) => new { a.x.SRHD, a.x.SRDTL, URHD = b })
                        .Where(w =>
                            w.URHD.売上区分 != (int)CommonConstants.売上区分.メーカー販社商流直送 &&
                            w.URHD.売上区分 != (int)CommonConstants.売上区分.メーカー販社商流直送返品);

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

                #endregion

                // 返品分のデータを取得する
                var returnList = srDataList.Where(w => w.SRHD.仕入区分 == (int)CommonConstants.仕入区分.返品).ToList();

                #region 各名称を取得して検索メンバークラスに整形
                var resultList =
                    srDataList.Where(w => w.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品).ToList()
                        // 返品分の仕入情報
                        .GroupJoin(returnList,
                            x => x.SRHD.伝票番号,
                            y => y.URHD.元伝票番号,
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(),
                            (p, q) => new { p.x.SRHD, p.x.SRDTL, RTSR = q })
                        // 会社名
                        .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                            x => x.SRHD.会社名コード,
                            y => y.自社コード,
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(),
                            (a, b) => new { a.x.SRHD, a.x.SRDTL, a.x.RTSR, JIS = b })
                        // 仕入先
                        .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                            x => x.SRHD.仕入先コード,
                            y => y.自社コード,
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(),
                            (c, d) => new { c.x.SRHD, c.x.SRDTL, c.x.RTSR, c.x.JIS, SJIS = d })
                        // 入荷先
                        .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                            x => x.SRHD.入荷先コード,
                            y => y.自社コード,
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(),
                            (e, f) => new { e.x.SRHD, e.x.SRDTL, e.x.RTSR, e.x.JIS, e.x.SJIS, NJIS = f })
                        .ToList()
                        .Select(x => new SearchDataMember
                        {
                            伝票番号 = x.SRHD.伝票番号.ToString(),
                            返品伝票番号 = x.RTSR != null ? x.RTSR.SRHD.伝票番号.ToString() : "",
                            会社名コード = x.SRHD.会社名コード.ToString(),
                            自社名 = x.JIS != null ? x.JIS.自社名 : "",
                            仕入日 = x.SRHD.仕入日.ToString("yyyy/MM/dd"),
                            支払日 = "",// TODO:一応足しておく
                            入力区分 = CommonConstants.入力区分.売上入力.GetHashCode().ToString(),
                            入力区分名 = CommonConstants.Get入力区分Dic()[CommonConstants.入力区分.売上入力.GetHashCode()],
                            仕入先コード = x.SRHD.仕入先コード.ToString(),
                            仕入先枝番 = "",
                            仕入先名 = x.SJIS != null ? x.SJIS.自社名 : "",
                            入荷先コード = x.SRHD.入荷先コード.ToString(),
                            入荷先名 = x.NJIS != null ? x.NJIS.自社名 : "",
                            発注番号 = x.SRHD.発注番号.ToString(),
                            備考 = x.SRHD.備考,
                            合計金額 = x.SRDTL.合計金額,
                            消費税 = x.SRHD.消費税 ?? 0,
                            返品合計金額 = x.RTSR != null ? (x.RTSR.SRDTL.合計金額 * -1) : 0,
                            返品消費税 = x.RTSR != null ? (x.RTSR.SRHD.消費税 * -1) ?? 0 : 0
                        })
                        .ToList();
                #endregion

                return resultList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            */
            return null;
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