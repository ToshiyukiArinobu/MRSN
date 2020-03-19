using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 担当者・得意先別売上統計表 サービスクラス
    /// </summary>
    public class BSK04010
    {
        #region 定数
        /// <summary>
        /// 売上先
        /// </summary>
        public enum 売上先
        {
            得意先 = 0,
            販社 = 1,
            両方 = 2
        }

        /// <summary>
        /// 作成区分
        /// </summary>
        public enum 作成区分
        {
            売上ありのみ = 0,
            売上なし含む = 1
        }
        #endregion

        #region << メンバクラス定義 >>

        #region 帳票印刷メンバ

        #region (月別)帳票印刷メンバクラス
        /// <summary>
        /// (月別)帳票印刷メンバクラス
        /// </summary>
        public class BSK04010_PrintMember_Month
        {
            /// <summary>得意先コード</summary>
            /// <remarks>
            /// 帳票で改ページさせる為、
            /// 「コード」「枝番」を結合して設定
            /// </remarks>
            public int 自社コード { get; set; }
            public string 自社名 { get; set; }
            public int? 担当者ID { get; set; }
            public string 担当者名 { get; set; }
            public string 得意先コード { get; set; }
            public string 得意先名 { get; set; }
            public long 集計売上額０１ { get; set; }
            public long 集計売上額０２ { get; set; }
            public long 集計売上額０３ { get; set; }
            public long 集計売上額０４ { get; set; }
            public long 集計売上額０５ { get; set; }
            public long 集計売上額０６ { get; set; }
            public long 集計売上額０７ { get; set; }
            public long 集計売上額０８ { get; set; }
            public long 集計売上額０９ { get; set; }
            public long 集計売上額１０ { get; set; }
            public long 集計売上額１１ { get; set; }
            public long 集計売上額１２ { get; set; }
            public long 集計合計額 { get; set; }
            public decimal 構成比率 { get; set; }
        }
        #endregion

        #region (日別)帳票印刷メンバクラス
        /// <summary>
        /// (日別)帳票印刷メンバクラス
        /// </summary>
        public class BSK04010_PrintMember_Day
        {
            /// <summary>得意先コード</summary>
            /// <remarks>
            /// 帳票で改ページさせる為、
            /// 「コード」「枝番」を結合して設定
            /// </remarks>
            public int 自社コード { get; set; }
            public string 自社名 { get; set; }
            public int? 担当者ID { get; set; }
            public string 担当者名 { get; set; }
            public string 得意先コード { get; set; }
            public string 得意先名 { get; set; }
            public long 集計売上額０１ { get; set; }
            public long 集計売上額０２ { get; set; }
            public long 集計売上額０３ { get; set; }
            public long 集計売上額０４ { get; set; }
            public long 集計売上額０５ { get; set; }
            public long 集計売上額０６ { get; set; }
            public long 集計売上額０７ { get; set; }
            public long 集計売上額０８ { get; set; }
            public long 集計売上額０９ { get; set; }
            public long 集計売上額１０ { get; set; }
            public long 集計売上額１１ { get; set; }
            public long 集計売上額１２ { get; set; }
            public long 集計売上額１３ { get; set; }
            public long 集計売上額１４ { get; set; }
            public long 集計売上額１５ { get; set; }
            public long 集計売上額１６ { get; set; }
            public long 集計売上額１７ { get; set; }
            public long 集計売上額１８ { get; set; }
            public long 集計売上額１９ { get; set; }
            public long 集計売上額２０ { get; set; }
            public long 集計売上額２１ { get; set; }
            public long 集計売上額２２ { get; set; }
            public long 集計売上額２３ { get; set; }
            public long 集計売上額２４ { get; set; }
            public long 集計売上額２５ { get; set; }
            public long 集計売上額２６ { get; set; }
            public long 集計売上額２７ { get; set; }
            public long 集計売上額２８ { get; set; }
            public long 集計売上額２９ { get; set; }
            public long 集計売上額３０ { get; set; }
            public long 集計売上額３１ { get; set; }
            public long 集計合計額 { get; set; }
            public decimal 構成比率 { get; set; }
        }
        #endregion

        #endregion

        #region 集計メンバ
        /// <summary>
        /// 集計メンバクラス
        /// </summary>
        public class TallyMember
        {
            public int 年月 { get; set; }
            public int 日付 { get; set; }
            public int 自社コード { get; set; }
            public string 自社名 { get; set; }
            public int? 担当者コード { get; set; }
            public string 担当者名 { get; set; }
            public int 得意先コード { get; set; }
            public int 得意先枝番 { get; set; }
            public string 得意先名 { get; set; }
            public long 金額 { get; set; }
        }

        #endregion

        #region 集計対象得意先情報クラス
        /// <summary>
        /// 集計対象得意先情報クラス
        /// </summary>
        public class TOK_INFO
        {
            public int 自社コード { get; set; }
            public string 自社名 { get; set; }
            public int? 担当者コード { get; set; }
            public string 担当者名 { get; set; }
            public int 取引先コード { get; set; }
            public int 枝番 { get; set; }
            public string 略称名 { get; set; }
            public int 取引区分 { get; set; }
        }
        #endregion

        #region 担当者別集計合計クラス
        /// <summary>
        /// 担当者別集計合計クラス
        /// </summary>
        public class 担当者別集計合計
        {
            public int 自社コード { get; set; }
            public int? 担当者コード { get; set; }
            public long 担当者別集計合計額 { get; set; }
        }
        #endregion

        #endregion

        #region 自社情報の取得
        /// <summary>
        /// 自社情報の取得
        /// </summary>
        /// <param name="p自社コード"></param>
        /// <returns></returns>
        public List<M70_JIS> GetJisInfo(string p自社コード)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                int val = 1;
                int n自社コード = Int32.TryParse(p自社コード, out val) ? val : 1;
                var query = context.M70_JIS
                            .Where(w => w.自社コード == n自社コード && w.削除日時 == null);

                return query.ToList();
            }
        }
        #endregion

        #region （月別）集計データ取得
        /// <summary>
        /// （月別）集計情報を取得する
        /// </summary>
        /// <param name="paramDic"></param>
        /// <returns></returns>
        public List<BSK04010_PrintMember_Month> GetPrintList_Month(Dictionary<string, string> paramDic)
        {
            // 検索パラメータ展開
            int year, uriageKind, createType;
            int? company, staffCd, customerCd, customerEd;
            DateTime? startYm, endYm, createYm;

            getFormParams(paramDic, out year, out company, out staffCd, out customerCd, out customerEd, out startYm, out endYm, out createYm, out uriageKind, out createType);

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 対象とする取引区分を設定
                List<int> kbnList = new List<int>();
                if (uriageKind == (int)売上先.得意先)
                {
                    kbnList.Add((int)CommonConstants.取引区分.得意先);
                    kbnList.Add((int)CommonConstants.取引区分.相殺);
                }
                else if (uriageKind == (int)売上先.販社)
                {
                    kbnList.Add((int)CommonConstants.取引区分.販社);
                }
                else
                {
                    kbnList.Add((int)CommonConstants.取引区分.得意先);
                    kbnList.Add((int)CommonConstants.取引区分.相殺);
                    kbnList.Add((int)CommonConstants.取引区分.販社);
                }

                var tokList =
                    context.M01_TOK.Where(w => w.削除日時 == null && kbnList.Contains(w.取引区分));

                #region 条件絞り込み
                // 自社が指定されていれば条件追加
                if (company != null)
                {
                    var jis = context.M70_JIS.Where(w => w.自社コード == company).FirstOrDefault();
                    tokList = tokList.Where(w => w.担当会社コード == jis.自社コード);
                }

                // 担当者が指定されていれば条件追加
                if (staffCd != null)
                {
                    tokList = tokList.Where(w => w.Ｔ担当者コード == staffCd);
                }

                // 取引先が指定されていれば条件追加
                if (customerCd != null && customerEd != null)
                {
                    tokList = tokList.Where(w => w.取引先コード == customerCd && w.枝番 == customerEd);
                }
                if (customerCd != null && customerEd == null)
                {
                    tokList = tokList.Where(w => w.取引先コード == customerCd);
                }
                #endregion

                // 集計用得意先情報を作成(速度改善のため)
                List<TOK_INFO> tokInfoList = getTokInfo(context, tokList);

                // 得意先毎に集計を実施
                List<BSK04010_PrintMember_Month> resultList = new List<BSK04010_PrintMember_Month>();

                foreach (TOK_INFO tok in tokInfoList)
                {
                    // 年月単位で集計データを取得
                    List <TallyMember> tallyList;

                    // 売上先：得意先
                    if (tok.取引区分 == (int)CommonConstants.取引区分.得意先 || tok.取引区分 == (int)CommonConstants.取引区分.相殺)
                    {
                        tallyList = getYearMonthAggregateData(context, tok, startYm, endYm);
                    }
                    // 売上先：販社
                    else
                    {
                        tallyList = getYearMonthAggregateData_HAN(context, tok, startYm, endYm);
                    }

                    // 年度の集計が終わったらプリントクラスに設定
                    // 印字用データリスト作成
                    BSK04010_PrintMember_Month printData;
                    printData = setPrintListMonth(tallyList);

                    // 作成区分(売上ありのみ)が指定されている場合
                    if (createType == (int)作成区分.売上ありのみ)
                    {
                        if (printData.集計合計額 > 0)
                        {
                            resultList.Add(printData);
                        }
                    }
                    else
                    {
                        resultList.Add(printData);
                    }
                }

                // データリストを集計して構成比を作成
                decimal total = Convert.ToDecimal(resultList.Sum(t => t.集計合計額));
                // 合計がゼロとなるデータは出力対象外とする
                if (total == 0)
                    return new List<BSK04010_PrintMember_Month>();

                // 自社コード・担当者ID毎の合計を算出
                var grpTotal = resultList.AsEnumerable()
                                .GroupBy(g => new { g.自社コード, g.担当者ID })
                                .Select(s => new 担当者別集計合計
                                {
                                    自社コード = s.Key.自社コード,
                                    担当者コード = s.Key.担当者ID,
                                    担当者別集計合計額 = s.Sum(m => m.集計合計額)
                                });

                // 構成比を計算
                resultList =
                    resultList.AsEnumerable()
                        .GroupJoin(grpTotal,
                            x => new { jis = x.自社コード, tnt = x.担当者ID },
                            y => new { jis = y.自社コード, tnt = y.担当者コード },
                            (x, y) => new { x, y })
                            .SelectMany(z => z.y.DefaultIfEmpty(),
                                (a, b) => new { RET = a.x, CALC = b })
                        .GroupBy(g => new { g.RET.自社コード, g.RET.自社名, g.RET.担当者ID, g.RET.担当者名, g.RET.得意先コード, g.RET.得意先名, g.CALC.担当者別集計合計額 })
                        .Select(s => new BSK04010_PrintMember_Month
                        {
                            自社コード = s.Key.自社コード,
                            自社名 = s.Key.自社名,
                            担当者ID = s.Key.担当者ID,
                            担当者名 = s.Key.担当者名,
                            得意先コード = s.Key.得意先コード,
                            得意先名 = s.Key.得意先名,
                            集計売上額０１ = s.Sum(m => m.RET.集計売上額０１),
                            集計売上額０２ = s.Sum(m => m.RET.集計売上額０２),
                            集計売上額０３ = s.Sum(m => m.RET.集計売上額０３),
                            集計売上額０４ = s.Sum(m => m.RET.集計売上額０４),
                            集計売上額０５ = s.Sum(m => m.RET.集計売上額０５),
                            集計売上額０６ = s.Sum(m => m.RET.集計売上額０６),
                            集計売上額０７ = s.Sum(m => m.RET.集計売上額０７),
                            集計売上額０８ = s.Sum(m => m.RET.集計売上額０８),
                            集計売上額０９ = s.Sum(m => m.RET.集計売上額０９),
                            集計売上額１０ = s.Sum(m => m.RET.集計売上額１０),
                            集計売上額１１ = s.Sum(m => m.RET.集計売上額１１),
                            集計売上額１２ = s.Sum(m => m.RET.集計売上額１２),
                            集計合計額 = s.Sum(m => m.RET.集計合計額),
                            構成比率 = s.Key.担当者別集計合計額 != 0 ?
                                Math.Round(
                                    Decimal.Divide(s.Sum(m => m.RET.集計合計額), s.Key.担当者別集計合計額) * 100, 2) : 0
                        })
                        .OrderBy(o => o.自社コード).ThenBy(o => o.担当者ID).ThenBy(o => o.得意先コード)
                        .ToList();

                return resultList;

            }

        }
        #endregion

        #region（日別）集計データ取得
        /// <summary>
        /// （日別）集計情報を取得する
        /// </summary>
        /// <param name="paramDic"></param>
        /// <returns></returns>
        public List<BSK04010_PrintMember_Day> GetPrintList_Day(Dictionary<string, string> paramDic)
        {
            // 検索パラメータ展開
            int year, uriageKind, createType;
            int? company, staffCd, customerCd, customerEd;
            DateTime? startYm, endYm, createYm;

            getFormParams(paramDic, out year, out company, out staffCd, out customerCd, out customerEd, out startYm, out endYm, out createYm, out uriageKind, out createType);

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 対象とする取引区分を設定
                List<int> kbnList = new List<int>();
                if (uriageKind == (int)売上先.得意先)
                {
                    kbnList.Add((int)CommonConstants.取引区分.得意先);
                    kbnList.Add((int)CommonConstants.取引区分.相殺);
                }
                else if (uriageKind == (int)売上先.販社)
                {
                    kbnList.Add((int)CommonConstants.取引区分.販社);
                }
                else
                {
                    kbnList.Add((int)CommonConstants.取引区分.得意先);
                    kbnList.Add((int)CommonConstants.取引区分.相殺);
                    kbnList.Add((int)CommonConstants.取引区分.販社);
                }

                var tokList =
                    context.M01_TOK.Where(w => w.削除日時 == null && kbnList.Contains(w.取引区分));

                #region 条件絞り込み
                // 自社が指定されていれば条件追加
                if (company != null)
                {
                    var jis = context.M70_JIS.Where(w => w.自社コード == company).FirstOrDefault();
                    tokList = tokList.Where(w => w.担当会社コード == jis.自社コード);
                }

                // 担当者が指定されていれば条件追加
                if (staffCd != null)
                {
                    tokList = tokList.Where(w => w.Ｔ担当者コード == staffCd);
                }

                // 取引先が指定されていれば条件追加
                if (customerCd != null && customerEd != null)
                {
                    tokList = tokList.Where(w => w.取引先コード == customerCd && w.枝番 == customerEd);
                }
                if (customerCd != null && customerEd == null)
                {
                    tokList = tokList.Where(w => w.取引先コード == customerCd);
                }
                #endregion

                // 集計用得意先情報を作成(速度改善のため)
                List<TOK_INFO> tokInfoList = getTokInfo(context, tokList);

                // 得意先毎に集計を実施
                List<BSK04010_PrintMember_Day> resultList = new List<BSK04010_PrintMember_Day>();

                foreach (TOK_INFO tok in tokInfoList)
                {
                    // 日単位で集計データを取得
                    List < TallyMember > TallyList;

                    // 売上先：得意先
                    if (tok.取引区分 == (int)CommonConstants.取引区分.得意先 || tok.取引区分 == (int)CommonConstants.取引区分.相殺)
                    {
                        TallyList = getDayAggregateData(context, tok, createYm);
                    }
                    // 売上先：販社
                    else
                    {
                        TallyList = getDayAggregateData_HAN(context, tok, createYm);
                    }

                    // 日単位の集計が終わったらプリントクラスに設定
                    // 印字用データ作成
                    BSK04010_PrintMember_Day printData;
                    printData = setPrintListDay(TallyList);

                    // 作成区分(売上ありのみ)が指定されている場合
                    if (createType == (int)作成区分.売上ありのみ)
                    {
                        if (printData.集計合計額 != 0)
                        {
                            resultList.Add(printData);
                        }
                    }
                    else
                    {
                        resultList.Add(printData);
                    }
                }

                // データリストを集計して構成比を作成
                decimal total = Convert.ToDecimal(resultList.Sum(t => t.集計合計額));
                // 合計がゼロとなるデータは出力対象外とする
                if (total == 0)
                return new List<BSK04010_PrintMember_Day>();

                // 自社コード・担当者ID毎の合計を算出
                var grpTotal = resultList.AsEnumerable()
                                .GroupBy(g => new { g.自社コード, g.担当者ID })
                                .Select(s => new 担当者別集計合計
                                {
                                    自社コード = s.Key.自社コード,
                                    担当者コード = s.Key.担当者ID,
                                    担当者別集計合計額 = s.Sum(m => m.集計合計額)
                                });

                // 構成比を計算
                resultList =
                    resultList.AsEnumerable()
                        .GroupJoin(grpTotal,
                            x => new { jis = x.自社コード, tnt = x.担当者ID },
                            y => new { jis = y.自社コード, tnt = y.担当者コード },
                            (x, y) => new { x, y })
                            .SelectMany(z => z.y.DefaultIfEmpty(),
                                (a, b) => new { RET = a.x, CALC = b})
                        .GroupBy(g => new { g.RET.自社コード, g.RET.自社名, g.RET.担当者ID, g.RET.担当者名, g.RET.得意先コード, g.RET.得意先名, g.CALC.担当者別集計合計額 })
                        .Select(s => new BSK04010_PrintMember_Day
                        {
                            自社コード = s.Key.自社コード,
                            自社名 = s.Key.自社名,
                            担当者ID = s.Key.担当者ID,
                            担当者名 = s.Key.担当者名,
                            得意先コード = s.Key.得意先コード,
                            得意先名 = s.Key.得意先名,
                            集計売上額０１ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額０１) / 1000),
                            集計売上額０２ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額０２) / 1000),
                            集計売上額０３ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額０３) / 1000),
                            集計売上額０４ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額０４) / 1000),
                            集計売上額０５ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額０５) / 1000),
                            集計売上額０６ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額０６) / 1000),
                            集計売上額０７ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額０７) / 1000),
                            集計売上額０８ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額０８) / 1000),
                            集計売上額０９ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額０９) / 1000),
                            集計売上額１０ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額１０) / 1000),
                            集計売上額１１ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額１１) / 1000),
                            集計売上額１２ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額１２) / 1000),
                            集計売上額１３ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額１３) / 1000),
                            集計売上額１４ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額１４) / 1000),
                            集計売上額１５ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額１５) / 1000),
                            集計売上額１６ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額１６) / 1000),
                            集計売上額１７ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額１７) / 1000),
                            集計売上額１８ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額１８) / 1000),
                            集計売上額１９ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額１９) / 1000),
                            集計売上額２０ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額２０) / 1000),
                            集計売上額２１ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額２１) / 1000),
                            集計売上額２２ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額２２) / 1000),
                            集計売上額２３ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額２３) / 1000),
                            集計売上額２４ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額２４) / 1000),
                            集計売上額２５ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額２５) / 1000),
                            集計売上額２６ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額２６) / 1000),
                            集計売上額２７ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額２７) / 1000),
                            集計売上額２８ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額２８) / 1000),
                            集計売上額２９ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額２９) / 1000),
                            集計売上額３０ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額３０) / 1000),
                            集計売上額３１ = (long)Math.Floor((double)s.Sum(m => m.RET.集計売上額３１) / 1000),
                            集計合計額 = (long)Math.Floor((double)s.Sum(m => m.RET.集計合計額) / 1000),
                            構成比率 = s.Key.担当者別集計合計額 != 0 ?
                                Math.Round(
                                    Decimal.Divide(s.Sum(m => m.RET.集計合計額), s.Key.担当者別集計合計額) * 100, 2) : 0
                        })
                        .OrderBy(o => o.自社コード).ThenBy(o => o.担当者ID).ThenBy(o => o.得意先コード)
                        .ToList();

                return resultList;

            }

        }
        #endregion

        #region 年月毎の得意先の売上集計処理を行う
        /// <summary>
        /// 年月毎の得意先の売上集計処理を行う
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="startYm">作成開始年月(yyyy/mm/01)</param>
        /// <param name="endYm">作成終了年月(yyyy/mm/末日)</param>
        /// <param name="createYm">作成月(yyyy/mm)</param>
        /// <param name="uriageKind">売上先</param>
        private List<TallyMember> getYearMonthAggregateData(TRAC3Entities context, TOK_INFO tokData,
                                                                        DateTime? startYm, DateTime? endYm)
        {
            // 年月毎のデータDic
            List<TallyMember> tallyList = new List<TallyMember>();
            // 対象月
            DateTime targetMonth = new DateTime(startYm.Value.Year, startYm.Value.Month, 1);

            // 年月毎のデータを取得
            while (targetMonth <= endYm)
            {
                int yearMonth = targetMonth.Year * 100 + targetMonth.Month;
                DateTime endYmd = targetMonth.AddMonths(1).AddDays(-1);

                // 本日以降の売上は取得しない
                endYmd = DateTime.Now <= endYmd ? DateTime.Now : endYmd;

                var uriData = context.T02_URHD
                                .Where(w => w.会社名コード == tokData.自社コード &&
                                    w.得意先コード == tokData.取引先コード &&
                                    w.得意先枝番 == tokData.枝番 &&
                                    w.売上日 >= targetMonth && w.売上日 <= endYmd)
                                .GroupBy(g => new
                                {
                                    g.会社名コード,
                                    g.得意先コード,
                                    g.得意先枝番
                                })
                                .Select(s => new TallyMember
                                {
                                    年月 = yearMonth,
                                    自社コード = s.Key.会社名コード,
                                    自社名 = tokData.自社名,
                                    担当者コード = tokData.担当者コード,
                                    担当者名 = tokData.担当者名,
                                    得意先コード = s.Key.得意先コード,
                                    得意先枝番 = s.Key.得意先枝番,
                                    得意先名 = tokData.略称名,
                                    金額 = (int)s.Sum(m => m.小計)

                                });


                if (!uriData.Any())
                {
                    // 空のデータを作成
                    var wkData = new TallyMember
                    {
                        年月 = yearMonth,
                        自社コード = tokData.自社コード,
                        自社名 = tokData.自社名,
                        担当者コード = tokData.担当者コード,
                        担当者名 = tokData.担当者名,
                        得意先コード = tokData.取引先コード,
                        得意先枝番 = tokData.枝番,
                        得意先名 = tokData.略称名,
                        金額 = 0
                    };

                    // 対象月の集計データを格納
                    tallyList.Add(wkData);
                }
                else
                {
                    // 対象月の集計データを格納
                    tallyList.AddRange(uriData.ToList());
                }

                // カウントアップ
                targetMonth = targetMonth.AddMonths(1);
            }

            return tallyList;
        }
        #endregion

        #region 年月毎の得意先の売上集計処理を行う(販社)
        /// <summary>
        /// 年月毎の得意先の売上集計処理を行う(販社)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="tokData"></param>
        /// <param name="startYm"></param>
        /// <param name="endYm"></param>
        /// <param name="createYm"></param>
        /// <returns></returns>
        private List<TallyMember> getYearMonthAggregateData_HAN(TRAC3Entities context, TOK_INFO tokData, 
                                                                                    DateTime? startYm, DateTime? endYm)
        {
            // 年月毎のデータList
            List<TallyMember> tallyList = new List<TallyMember>();
            // 対象月
            DateTime targetMonth = new DateTime(startYm.Value.Year, startYm.Value.Month, 1);
            // 対象自社
            var jisData = context.M70_JIS.Where(w => w.削除日時 == null && w.自社コード == tokData.自社コード).FirstOrDefault();
            // 対象得意先
            var targetHAN = context.M70_JIS.Where(w => w.取引先コード == tokData.取引先コード && w.枝番 == tokData.枝番).FirstOrDefault();
            // 担当者情報
            var staffData = context.M72_TNT.Where(w => w.削除日時 == null && w.担当者ID == tokData.担当者コード).FirstOrDefault();

            // 年月毎のデータを取得
            while (targetMonth <= endYm)
            {
                int yearMonth = targetMonth.Year * 100 + targetMonth.Month;
                DateTime endYmd = targetMonth.AddMonths(1).AddDays(-1);

                // 本日以降の売上は取得しない
                endYmd = DateTime.Now <= endYmd ? DateTime.Now : endYmd;

                var uriData = context.T02_URHD_HAN
                                .Where(w => w.削除日時 == null &&
                                    w.会社名コード == jisData.自社コード &&
                                    w.販社コード == targetHAN.自社コード &&
                                    w.売上日 >= targetMonth && w.売上日 <= endYmd)
                                .GroupBy(g => new
                                {
                                    g.会社名コード,
                                    g.販社コード
                                })
                                .Select(s => new TallyMember
                                {
                                    年月 = yearMonth,
                                    自社コード = jisData.自社コード,
                                    自社名 = jisData.自社名,
                                    担当者コード = tokData.担当者コード,
                                    担当者名 = staffData.担当者名,
                                    得意先コード = tokData.取引先コード,
                                    得意先枝番 = tokData.枝番,
                                    得意先名 = tokData.略称名,
                                    金額 = (int)s.Sum(m => m.小計)

                                });

                if (!uriData.Any())
                {
                    // 空のデータを作成
                    var wkData = new TallyMember
                                {
                                    年月 = yearMonth,
                                    自社コード = jisData.自社コード,
                                    自社名 = jisData.自社名,
                                    担当者コード = tokData.担当者コード,
                                    担当者名 = staffData.担当者名,
                                    得意先コード = tokData.取引先コード,
                                    得意先枝番 = tokData.枝番,
                                    得意先名 = tokData.略称名,
                                    金額 = 0
                                };

                    tallyList.Add(wkData);
                }
                else
                {
                    // 対象月の集計データを格納
                    tallyList.AddRange(uriData.ToList());
                }

                // カウントアップ
                targetMonth = targetMonth.AddMonths(1);
            }

            return tallyList;
        }
        #endregion

        #region 日毎の得意先の売上集計処理を行う
        /// <summary>
        /// 日毎の得意先の売上集計処理を行う
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="startYm">作成開始年月(yyyy/mm/01)</param>
        /// <param name="endYm">作成終了年月(yyyy/mm/末日)</param>
        /// <param name="createYm">作成月(yyyy/mm)</param>
        /// <param name="uriageKind">売上先</param>
        private List<TallyMember> getDayAggregateData(TRAC3Entities context, TOK_INFO tokData, DateTime? createYm)
        {
            // 日毎のデータList
            List<TallyMember> tallyList = new List<TallyMember>();
            // 対象日
            DateTime targetDay = new DateTime(createYm.Value.Year, createYm.Value.Month, 1);
            // 対象月末
            DateTime monthEndYmd = targetDay.AddMonths(1).AddDays(-1);
            // 対象終了日
            DateTime endYmd = DateTime.Now <= monthEndYmd ? DateTime.Now : monthEndYmd;

            int dayNo = targetDay.Day;
            IQueryable<TallyMember> uriData;

            // 日毎のデータを取得
            while (targetDay <= endYmd)
            {
                dayNo = targetDay.Day;

                uriData = context.T02_URHD
                            .Where(w => w.会社名コード == tokData.自社コード &&
                                w.得意先コード == tokData.取引先コード &&
                                w.得意先枝番 == tokData.枝番 &&
                                w.売上日 == targetDay)
                            .GroupBy(g => new
                            {
                                g.会社名コード,
                                g.得意先コード,
                                g.得意先枝番,
                            })
                            .Select(s => new TallyMember
                            {
                                日付 = dayNo,
                                自社コード = s.Key.会社名コード,
                                自社名 = tokData.自社名,
                                担当者コード = tokData.担当者コード,
                                担当者名 = tokData.担当者名,
                                得意先コード = s.Key.得意先コード,
                                得意先枝番 = s.Key.得意先枝番,
                                得意先名 = tokData.略称名,
                                金額 = (int)s.Sum(m => m.小計)

                            }).Distinct();

                if (!uriData.Any())
                {
                    // 空のデータを作成
                    var wkData = new TallyMember
                    {
                        日付 = dayNo,
                        自社コード = tokData.自社コード,
                        自社名 = tokData.自社名,
                        担当者コード = tokData.担当者コード,
                        担当者名 = tokData.担当者名,
                        得意先コード = tokData.取引先コード,
                        得意先枝番 = tokData.枝番,
                        得意先名 = tokData.略称名,
                        金額 = 0,
                    };
                    // 対象日の集計データを格納
                    tallyList.Add(wkData);
                }
                else
                {
                    // 対象日の集計データを格納
                    tallyList.AddRange(uriData.ToList());
                }

                // カウントアップ
                targetDay = targetDay.AddDays(1);
            }

            // 本日以降の売上データを作成
            if (endYmd < monthEndYmd)
            {
                while (targetDay <= monthEndYmd)
                {
                    dayNo = targetDay.Day;

                    // 空のデータを作成
                    var wkData = new TallyMember
                    {
                        日付 = dayNo,
                        自社コード = tokData.自社コード,
                        自社名 = tokData.自社名,
                        担当者コード = tokData.担当者コード,
                        担当者名 = tokData.担当者名,
                        得意先コード = tokData.取引先コード,
                        得意先枝番 = tokData.枝番,
                        得意先名 = tokData.略称名,
                        金額 = 0,
                    };
                    // 対象日の集計データを格納
                    tallyList.Add(wkData);

                    // カウントアップ
                    targetDay = targetDay.AddDays(1);
                }
            }

            return tallyList;
        }
        #endregion

        #region 日毎の得意先の売上集計処理を行う(販社)
        /// <summary>
        /// 日毎の得意先の売上集計処理を行う(販社)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="tokData"></param>
        /// <param name="startYm"></param>
        /// <param name="endYm"></param>
        /// <param name="createYm"></param>
        /// <returns></returns>
        private List<TallyMember> getDayAggregateData_HAN(TRAC3Entities context, TOK_INFO tokData, DateTime? createYm)
        {
            // 日毎のデータList
            List<TallyMember> tallyList = new  List<TallyMember>();
            // 対象日
            DateTime targetDay = new DateTime(createYm.Value.Year, createYm.Value.Month, 1);
            // 対象月末
            DateTime monthEndYmd = targetDay.AddMonths(1).AddDays(-1);
            // 対象終了日
            DateTime endYmd = DateTime.Now <= monthEndYmd ? DateTime.Now : monthEndYmd;
            // 対象得意先
            var targetHAN = context.M70_JIS.Where(w => w.取引先コード == tokData.取引先コード && w.枝番 == tokData.枝番).FirstOrDefault();
            

            int dayNo = targetDay.Day;
            IQueryable<TallyMember> uriData;

            // 日毎のデータを取得
            while (targetDay <= endYmd)
            {
                dayNo = targetDay.Day;

                uriData = context.T02_URHD_HAN
                            .Where(w => w.削除日時 == null &&
                                w.会社名コード == tokData.自社コード &&
                                w.販社コード == targetHAN.自社コード &&
                                w.売上日 == targetDay)
                            .GroupBy(g => new
                            {
                                g.会社名コード,
                                g.販社コード
                            })
                            .Select(s => new TallyMember
                            {
                                日付 = dayNo,
                                自社コード = tokData.自社コード,
                                自社名 = tokData.自社名,
                                担当者コード = tokData.担当者コード,
                                担当者名 = tokData.担当者名,
                                得意先コード = tokData.取引先コード,
                                得意先枝番 = tokData.枝番,
                                得意先名 = tokData.略称名,
                                金額 = (int)s.Sum(m => m.小計)

                            });

                if (!uriData.Any())
                {
                    // 空のデータを作成
                    var wkData = new TallyMember
                    {
                        日付 = dayNo,
                        自社コード = tokData.自社コード,
                        自社名 = tokData.自社名,
                        担当者コード = tokData.担当者コード,
                        担当者名 = tokData.担当者名,
                        得意先コード = tokData.取引先コード,
                        得意先枝番 = tokData.枝番,
                        得意先名 = tokData.略称名,
                        金額 = 0,
                    };
                    // 対象日の集計データを格納
                    tallyList.Add(wkData);

                }
                else
                {
                    // 対象月の集計データを格納
                    tallyList.AddRange(uriData.ToList());
                }

                // カウントアップ
                targetDay = targetDay.AddDays(1);
            }

            // 本日以降の売上データを作成
            if (endYmd < monthEndYmd)
            {
                while (targetDay <= monthEndYmd)
                {
                    dayNo = targetDay.Day;

                    // 空のデータを作成
                    var wkData = new TallyMember
                    {
                        日付 = dayNo,
                        自社コード = tokData.自社コード,
                        自社名 = tokData.自社名,
                        担当者コード = tokData.担当者コード,
                        担当者名 = tokData.担当者名,
                        得意先コード = tokData.取引先コード,
                        得意先枝番 = tokData.枝番,
                        得意先名 = tokData.略称名,
                        金額 = 0,
                    };
                    // 対象日の集計データを格納
                    tallyList.Add(wkData);

                    // カウントアップ
                    targetDay = targetDay.AddDays(1);
                }
            }

            return tallyList;
        }
        #endregion

        #region （月別）印字用データリスト作成
        /// <summary>
        /// 印字用データリスト作成（月別）
        /// </summary>
        /// <param name="tokDic"></param>
        /// <returns></returns>
        private BSK04010_PrintMember_Month setPrintListMonth(List<TallyMember> tallyList)
        {
            int monthCount = 1;
            tallyList.OrderBy(o => o.年月);
            BSK04010_PrintMember_Month print = new BSK04010_PrintMember_Month();

            foreach (TallyMember tally in tallyList)
            {
                print.自社コード = tally.自社コード;
                print.自社名 = tally.自社名;
                print.担当者ID = tally.担当者コード;
                print.担当者名 = tally.担当者名;
                print.得意先コード = string.Format("{0:D4} - {1:D2}", tally.得意先コード, tally.得意先枝番);
                print.得意先名 = tally.得意先名;
                print.集計合計額 += tally.金額;

                #region monthCountにより設定列分け
                switch (monthCount)
                {
                    case 1:
                        print.集計売上額０１ = tally.金額;
                        break;

                    case 2:
                        print.集計売上額０２ = tally.金額;
                        break;

                    case 3:
                        print.集計売上額０３ = tally.金額;
                        break;

                    case 4:
                        print.集計売上額０４ = tally.金額;
                        break;

                    case 5:
                        print.集計売上額０５ = tally.金額;
                        break;

                    case 6:
                        print.集計売上額０６ = tally.金額;
                        break;

                    case 7:
                        print.集計売上額０７ = tally.金額;
                        break;

                    case 8:
                        print.集計売上額０８ = tally.金額;
                        break;

                    case 9:
                        print.集計売上額０９ = tally.金額;
                        break;

                    case 10:
                        print.集計売上額１０ = tally.金額;
                        break;

                    case 11:
                        print.集計売上額１１ = tally.金額;
                        break;

                    case 12:
                        print.集計売上額１２ = tally.金額;
                        break;

                    default:
                        break;
                }
                monthCount++;
                #endregion
            }

            return print;
        }
        #endregion

        #region （日別）印字用データリスト作成
        /// <summary>
        /// 印字用データリスト作成（日別）
        /// </summary>
        /// <param name="tokDic"></param>
        /// <returns></returns>
        private BSK04010_PrintMember_Day setPrintListDay(List<TallyMember> tallyList)
        {
            tallyList.OrderBy(o => o.日付);
            BSK04010_PrintMember_Day print = new BSK04010_PrintMember_Day();

            foreach (TallyMember tally in tallyList)
            {
                print.自社コード = tally.自社コード;
                print.自社名 = tally.自社名;
                print.担当者ID = tally.担当者コード;
                print.担当者名 = tally.担当者名;
                print.得意先コード = string.Format("{0:D4} - {1:D2}", tally.得意先コード, tally.得意先枝番);
                print.得意先名 = tally.得意先名;
                print.集計合計額 += tally.金額;

                #region 日付により設定列分け
                switch (tally.日付)
                {
                    case 1:
                        print.集計売上額０１ = tally.金額;
                        break;

                    case 2:
                        print.集計売上額０２ = tally.金額;
                        break;

                    case 3:
                        print.集計売上額０３ = tally.金額;
                        break;

                    case 4:
                        print.集計売上額０４ = tally.金額;
                        break;

                    case 5:
                        print.集計売上額０５ = tally.金額;
                        break;

                    case 6:
                        print.集計売上額０６ = tally.金額;
                        break;

                    case 7:
                        print.集計売上額０７ = tally.金額;
                        break;

                    case 8:
                        print.集計売上額０８ = tally.金額;
                        break;

                    case 9:
                        print.集計売上額０９ = tally.金額;
                        break;

                    case 10:
                        print.集計売上額１０ = tally.金額;
                        break;

                    case 11:
                        print.集計売上額１１ = tally.金額;
                        break;

                    case 12:
                        print.集計売上額１２ = tally.金額;
                        break;

                    case 13:
                        print.集計売上額１３ = tally.金額;
                        break;

                    case 14:
                        print.集計売上額１４ = tally.金額;
                        break;

                    case 15:
                        print.集計売上額１５ = tally.金額;
                        break;

                    case 16:
                        print.集計売上額１６ = tally.金額;
                        break;

                    case 17:
                        print.集計売上額１７ = tally.金額;
                        break;

                    case 18:
                        print.集計売上額１８ = tally.金額;
                        break;

                    case 19:
                        print.集計売上額１９ = tally.金額;
                        break;

                    case 20:
                        print.集計売上額２０ = tally.金額;
                        break;

                    case 21:
                        print.集計売上額２１ = tally.金額;
                        break;

                    case 22:
                        print.集計売上額２２ = tally.金額;
                        break;

                    case 23:
                        print.集計売上額２３ = tally.金額;
                        break;

                    case 24:
                        print.集計売上額２４ = tally.金額;
                        break;

                    case 25:
                        print.集計売上額２５ = tally.金額;
                        break;

                    case 26:
                        print.集計売上額２６ = tally.金額;
                        break;

                    case 27:
                        print.集計売上額２７ = tally.金額;
                        break;

                    case 28:
                        print.集計売上額２８ = tally.金額;
                        break;

                    case 29:
                        print.集計売上額２９ = tally.金額;
                        break;

                    case 30:
                        print.集計売上額３０ = tally.金額;
                        break;

                    case 31:
                        print.集計売上額３１ = tally.金額;
                        break;

                    default:
                        break;
                }
                #endregion
            }
            return print;
        }
        #endregion

        #region 集計用得意先情報を作成
        /// <summary>
        /// 集計用得意先情報を作成
        /// </summary>
        /// <param name="context"></param>
        /// <param name="tokList"></param>
        /// <returns></returns>
        List<TOK_INFO> getTokInfo(TRAC3Entities context, IQueryable<M01_TOK> tokList)
        {
            IQueryable<TOK_INFO> tokInfo = tokList
                                                .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                                                    x => x.担当会社コード,
                                                    y => y.自社コード,
                                                    (x, y) => new { x, y })
                                                 .SelectMany(z => z.y.DefaultIfEmpty(),
                                                    (a, b) => new { TOK = a.x, JIS = b })
                                                 .GroupJoin(context.M72_TNT.Where(w => w.削除日時 == null),
                                                    x => x.TOK.Ｔ担当者コード,
                                                    y => y.担当者ID,
                                                    (x, y) => new { x, y })
                                                 .SelectMany(z => z.y.DefaultIfEmpty(),
                                                    (c, d) => new { TOK = c.x.TOK, JIS = c.x.JIS, TNT = d })
                                                 .Select(s => new TOK_INFO
                                                 {
                                                     自社コード = s.TOK.担当会社コード,
                                                     自社名 = s.JIS.自社名,
                                                     担当者コード = s.TOK.Ｔ担当者コード,
                                                     担当者名 = s.TNT.担当者名,
                                                     取引先コード = s.TOK.取引先コード,
                                                     枝番 = s.TOK.枝番,
                                                     略称名 = s.TOK.略称名,
                                                     取引区分 = s.TOK.取引区分,
                                                 });

            return tokInfo.OrderBy(o => o.自社コード).ThenBy(o => o.担当者コード).ThenBy(o => o.取引先コード).ThenBy(o => o.枝番).ToList();
        }
        #endregion
        
        #region 検索パラメータ展開
        /// <summary>
        /// 検索パラメータ展開
        /// </summary>
        /// <param name="paramDic">検索条件Dic</param>
        /// <param name="year">処理年度</param>
        /// <param name="company">自社コード</param>
        /// <param name="customerCd">得意先コード</param>
        /// <param name="customerEd">得意先枝番</param>
        /// <param name="startYm">作成開始年月</param>
        /// <param name="endYm">作成終了年月</param>
        /// <param name="createYm">作成月</param>
        /// <param name="uriageKind">売上先</param>
        private void getFormParams(Dictionary<string, string> paramDic, out int year, out int? company, out int? staffCd, out int? customerCd, out int? customerEd,
                                    out DateTime? startYm, out DateTime? endYm, out DateTime? createYm, out int uriageKind, out int createType)
        {

            int ival;
            DateTime dWk;
            StringBuilder sbStartYm = new StringBuilder();
            StringBuilder sbEndYm = new StringBuilder();
            StringBuilder sbCreateYm = new StringBuilder();
            sbStartYm.Append(paramDic["作成開始年月"]).Append("/01");
            sbEndYm.Append(paramDic["作成終了年月"]).Append("/01");
            sbCreateYm.Append(paramDic["作成月"]).Append("/01");   // No.361 Mod

            startYm = DateTime.TryParse(sbStartYm.ToString(), out dWk) ? dWk : (DateTime?)null;
            endYm = DateTime.TryParse(sbEndYm.ToString(), out dWk) ? dWk : (DateTime?)null;
            createYm = DateTime.TryParse(sbCreateYm.ToString(), out dWk) ? dWk : (DateTime?)null;
            if (endYm != null)
            {
                endYm = endYm.Value.AddMonths(1).AddDays(-1);
            }

            year = int.Parse(paramDic["処理年度"].Replace("/", ""));
            company = int.TryParse(paramDic["自社コード"], out ival) ? ival : (int?)null;
            staffCd = int.TryParse(paramDic["担当者コード"], out ival) ? ival : (int?)null;
            customerCd = int.TryParse(paramDic["得意先コード"], out ival) ? ival : (int?)null;
            customerEd = int.TryParse(paramDic["得意先枝番"], out ival) ? ival : (int?)null;
            uriageKind = int.TryParse(paramDic["売上先"], out ival) ? ival : 0;
            createType = int.TryParse(paramDic["作成区分"], out ival) ? ival : 0;
        }
        #endregion
    }

}