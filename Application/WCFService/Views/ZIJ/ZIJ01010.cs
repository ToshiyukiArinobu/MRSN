using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 仕入データ問合せサービスクラス
    /// </summary>
    public class ZIJ01010
    {
        #region 項目クラス定義

        /// <summary>
        /// ZIJ01010 支払明細問合せ 検索メンバー
        /// </summary>
        public class SearchDataMember
        {
            public int 伝票番号 { get; set; }          //No.406 Mod
            public string 返品伝票番号 { get; set; }
            public string 会社名コード { get; set; }
            public string 自社名 { get; set; }
            public string 仕入日 { get; set; }
            public string 支払日 { get; set; }
            public string 入力区分 { get; set; }
            public string 入力区分名 { get; set; }
            //public string 仕入区分 { get; set; }
            //public string 仕入区分名 { get; set; }
            public string 仕入先コード { get; set; }
            public string 仕入先名 { get; set; }
            public string 入荷先コード { get; set; }
            public string 入荷先名 { get; set; }
            public string 発注番号 { get; set; }
            public string 備考 { get; set; }
            //public string 元伝票番号 { get; set; }
            public long 合計金額 { get; set; }
            public int 消費税 { get; set; }
            public long 返品合計金額 { get; set; }
            public int 返品消費税 { get; set; }
        }

        #endregion

        #region 仕入データ問合せ検索処理
        /// <summary>
        /// 仕入データ問合せ検索をおこなう
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
                            .Join(context.T03_SRDTL
                                    .Where(w => w.削除日時 == null)
                                    .GroupBy(g => new { g.伝票番号 })
                                    .Select(x => new { x.Key.伝票番号, 合計金額 = x.Sum(s => s.金額) }),
                                x => x.伝票番号,
                                y => y.伝票番号,
                                (x, y) => new { SRHD = x, SRDTL = y });

                    #region 条件絞込

                    // 仕入日From - To
                    if (sDateFrom != null)
                        srDataList = srDataList.Where(w => w.SRHD.仕入日 >= sDateFrom);

                    if (sDateTo != null)
                        srDataList = srDataList.Where(w => w.SRHD.仕入日 <= sDateTo);

                    // 入金日From - To
                    // TODO:保留

                    // 入力区分
                    if (inputType != null)
                        srDataList = srDataList.Where(w => w.SRHD.入力区分 == inputType);

                    // 仕入先
                    if (supCode != null && supEda != null)
                        srDataList = srDataList.Where(w => w.SRHD.仕入先コード == supCode && w.SRHD.仕入先枝番 == supEda);

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
                                y => y.SRHD.元伝票番号,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (p, q) => new { p.x.SRHD, p.x.SRDTL, RTSR = q })
                            // 会社名
                            .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                                x => x.SRHD.会社名コード,
                                y => y.自社コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (a, b) => new { a.x.SRHD, a.x.SRDTL, a.x.RTSR, JIS1 = b })
                            // 仕入先
                            .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                                x => new { code = x.SRHD.仕入先コード, eda = x.SRHD.仕入先枝番 },
                                y => new { code = y.取引先コード, eda = y.枝番 },
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (c, d) => new { c.x.SRHD, c.x.SRDTL, c.x.RTSR, c.x.JIS1, TOK = d })
                            // 入荷先
                            .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                                x => x.SRHD.入荷先コード,
                                y => y.自社コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (e, f) => new { e.x.SRHD, e.x.SRDTL, e.x.RTSR, e.x.JIS1, e.x.TOK, JIS2 = f })
                            .ToList()
                            .Select(x => new SearchDataMember
                            {
                                伝票番号 = x.SRHD.伝票番号,          //No.406 Mod
                                返品伝票番号 = x.RTSR != null ? x.RTSR.SRHD.伝票番号.ToString() : "",
                                会社名コード = x.SRHD.会社名コード.ToString(),
                                自社名 = x.JIS1 != null ? x.JIS1.自社名 : "",
                                仕入日 = x.SRHD.仕入日.ToString("yyyy/MM/dd"),
                                支払日 = "",// TODO:一応足しておく
                                入力区分 = x.SRHD.入力区分.ToString(),
                                入力区分名 = CommonConstants.Get入力区分Dic()[x.SRHD.入力区分],
                                仕入先コード = string.Format("{0:D4} - {1:D2}", x.SRHD.仕入先コード, x.SRHD.仕入先枝番), // No.227,228 Mod
                                仕入先名 = x.TOK != null ? x.TOK.略称名 : "",
                                入荷先コード = x.SRHD.入荷先コード.ToString(),
                                入荷先名 = x.JIS2 != null ? x.JIS2.自社名 : "",
                                発注番号 = x.SRHD.発注番号.ToString(),
                                備考 = x.SRHD.備考,
                                合計金額 = x.SRDTL.合計金額,
                                消費税 = x.SRHD.消費税 ?? 0,
                                返品合計金額 = x.RTSR != null ? (x.RTSR.SRDTL.合計金額 * -1) : 0,
                                返品消費税 = x.RTSR != null ? (x.RTSR.SRHD.消費税 * -1) ?? 0 : 0
                            })
                            .ToList();
                    #endregion

                    if (inputType == null || inputType != CommonConstants.入力区分.仕入入力.GetHashCode())
                    {
                        var hanList = GetHanDataList(context, p自社コード, cond);
                        // 既存リストに追加
                        resultList.AddRange(hanList);

                    }

                    // リスト追加後にソート実施
                    resultList = resultList.OrderBy(o => o.仕入日).ThenBy(t => t.伝票番号).ToList();          //No.406 Mod

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
                            伝票番号 = x.SRHD.伝票番号,          //No.406 Mod
                            返品伝票番号 = x.RTSR != null ? x.RTSR.SRHD.伝票番号.ToString() : "",
                            会社名コード = x.SRHD.会社名コード.ToString(),
                            自社名 = x.JIS != null ? x.JIS.自社名 : "",
                            仕入日 = x.SRHD.仕入日.ToString("yyyy/MM/dd"),
                            支払日 = "",// TODO:一応足しておく
                            入力区分 = CommonConstants.入力区分.売上入力.GetHashCode().ToString(),
                            入力区分名 = CommonConstants.Get入力区分Dic()[CommonConstants.入力区分.売上入力.GetHashCode()],
                            仕入先コード = string.Format("{0:D4} - {1:D2}",x.SJIS.取引先コード, x.SJIS.枝番),     // No.227,228 Mod
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

        }

        #endregion


        /// <summary>
        /// 指定の文字列を日付型に変換して返す
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