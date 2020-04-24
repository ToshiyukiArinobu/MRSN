using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 得意先・商品別売上統計表 サービスクラス
    /// </summary>
    public class BSK01010
    {

        #region << 定数定義 >>

        /// <summary>品名「その他」の自社品番</summary>
        private const string HINBAN_SONOTA = "99-99";        // No.389  Add

        #endregion

        #region << メンバクラス定義 >>

        #region 帳票印刷メンバ
        /// <summary>
        /// 帳票印刷メンバクラス
        /// </summary>
        public class BSK01010_PrintMember
        {
            /// <summary>得意先コード</summary>
            /// <remarks>
            /// 帳票で改ページさせる為、
            /// 「コード」「枝番」を結合して設定
            /// </remarks>
            public int 自社コード { get; set; }      // No.227,228 Add
            public string 自社名 { get; set; }       // No.227,228 Add
            public string 得意先コード { get; set; }
            public string 得意先名 { get; set; }
            public int 品番コード { get; set; }
            public string 自社品番 { get; set; }        // No.321 Add
            public string 品番名称 { get; set; }
            public string 色名称 { get; set; }
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

            // No.400 Add Start
            public int 集計数量０１ { get; set; }
            public int 集計数量０２ { get; set; }
            public int 集計数量０３ { get; set; }
            public int 集計数量０４ { get; set; }
            public int 集計数量０５ { get; set; }
            public int 集計数量０６ { get; set; }
            public int 集計数量０７ { get; set; }
            public int 集計数量０８ { get; set; }
            public int 集計数量０９ { get; set; }
            public int 集計数量１０ { get; set; }
            public int 集計数量１１ { get; set; }
            public int 集計数量１２ { get; set; }
            public int 集計合計数量 { get; set; }
            public decimal 数量構成比率 { get; set; }
            // No.400 Add End


        }
        #endregion

        #region 集計メンバ
        /// <summary>
        /// 商品集計メンバクラス
        /// </summary>
        public class TallyMember
        {
            public int 自社コード { get; set; }      // No.227,228 Add
            public string 自社名 { get; set; }       // No.227,228 Add
            public int 品番コード { get; set; }
            public string 自社色 { get; set; }
            public string シリーズ { get; set; }
            public string 自社品番 { get; set; }
            public string 自社品名 { get; set; }
            public string 色名称 { get; set; }
            public long 金額 { get; set; }
            public int 数量 { get; set; }   // No.400 Add
        }

        #endregion

        public class BSK01010_DATASET
        {
            public List<BSK01010_PrintMember> PRINT_DATA = null;
        }

        #endregion

        #region 集計データ取得
        /// <summary>
        /// 集計情報を取得する
        /// </summary>
        /// <param name="paramDic"></param>
        /// <returns></returns>
        public BSK01010_DATASET GetPrintList(Dictionary<string, string> paramDic)
        {
            BSK01010_DATASET result = new BSK01010_DATASET();

            // パラメータ展開
            DateTime? startYm, endYm;
            int? company, fromCode, fromEda, toCode, toEda;
            string productFrom, productTo;

            getFormParams(paramDic, out startYm, out endYm, out company, out fromCode, out fromEda, out toCode, out toEda, out productFrom, out productTo);

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 対象として取引区分：得意先、相殺、販社を対象とする(全販社対象とするNo.400)
                List<int> kbnList = new List<int>() { (int)CommonConstants.取引区分.得意先, (int)CommonConstants.取引区分.相殺, (int)CommonConstants.取引区分.販社 };

                // 自社情報を取得   
                var tok =
                    context.M01_TOK.Where(w => w.削除日時 == null && kbnList.Contains(w.取引区分));

                // No.400 Add Start
                var hin =
                    context.M09_HIN.Where(w => w.削除日時 == null);

                #region 条件絞り込み

                // 対象自社が指定されていれば条件追加
                if (company != null)
                {
                    var jis = context.M70_JIS.Where(w => w.自社コード == company).FirstOrDefault();
                    tok = tok.Where(w => w.担当会社コード == jis.自社コード);
                }

                // 得意先FromToを再設定 FromがToを超える場合調整
                if (fromCode != null && toCode != null)
                {
                    // 得意先FromToが指定されている場合
                    int? wTokFrom = fromCode * 1000 + fromEda;
                    int? wTokTo = toCode * 1000 + toEda;

                    if (wTokFrom > wTokTo)
                    {
                        // FromとToを入れ替える
                        wTokFrom = toCode * 1000 + toEda;
                        wTokTo = fromCode * 1000 + fromEda;
                    }

                    tok = tok.Where(w => w.取引先コード * 1000 + w.枝番 >= wTokFrom && w.取引先コード * 1000 + w.枝番 <= wTokTo);

                }
                else
                {
                    // 得意先FromかToのどちらかが指定されている場合
                    if (fromCode != null)
                    {
                        tok = tok.Where(w => w.取引先コード * 1000 + w.枝番 >= fromCode * 1000 + fromEda);
                    }

                    if (toCode != null)
                    {
                        tok = tok.Where(w => w.取引先コード * 1000 + w.枝番 <= toCode * 1000 + toEda);
                    }
                }

                // 自社品番指定されている場合
                string compFrom = productFrom;
                string compTo = productTo;

                // 自社品番FromToを再設定 0123…ABC順で絞り込む
                if (!string.IsNullOrEmpty(productFrom) && !string.IsNullOrEmpty(productTo) &&
                    productFrom.CompareTo(productTo) > 0)
                {
                    compFrom = productTo;
                    compTo = productFrom;
                }

                if (!string.IsNullOrEmpty(productFrom))
                {
                    hin = hin.Where(w => w.自社品番.CompareTo(compFrom) >= 0);
                }

                if (!string.IsNullOrEmpty(productTo))
                {
                    hin = hin.Where(w => w.自社品番.CompareTo(compTo) <= 0);
                }


                // ソート順を指定
                tok = tok.OrderBy(o => o.担当会社コード).ThenBy(t => t.取引先コード).ThenBy(t => t.枝番);

                #endregion
                // No.400 Add End

                // 得意先毎に集計を実施
                List<BSK01010_PrintMember> resultList = new List<BSK01010_PrintMember>();
                foreach (M01_TOK tokRow in tok)
                {
                    // 決算月・請求締日から売上集計期間を算出する
                    DateTime targetMonth = (DateTime)startYm;   // No.400 Mod
                    DateTime lastMonth = (DateTime)endYm;       // No.400 Mod

                    #region 年月単位で集計データを取得
                    // 年月毎のデータDic
                    Dictionary<int, List<TallyMember>> tokDic = new Dictionary<int, List<TallyMember>>();
                    while (targetMonth <= lastMonth)
                    {
                        int yearMonth = targetMonth.Year * 100 + targetMonth.Month;

                        #region linq
                        var dtlList =
                            context.S01_SEIDTL
                                .Where(w =>
                                    w.自社コード == tokRow.担当会社コード &&        // No.400 Mod
                                    w.請求年月 == yearMonth &&
                                    w.請求先コード == tokRow.取引先コード &&
                                    w.請求先枝番 == tokRow.枝番)
                                .Join(hin,
                                    x => x.品番コード,
                                    y => y.品番コード,
                                    (x, y) => new { SD = x, HN = y })
                                .GroupJoin(context.M06_IRO,
                                    x => x.HN.自社色,
                                    y => y.色コード,
                                    (x, y) => new { x, y })
                                .SelectMany(z => z.y.DefaultIfEmpty(),
                                    (a, b) => new { a.x.SD, a.x.HN, IR = b })
                                .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                                    x => x.SD.自社コード,
                                    y => y.自社コード,
                                    (x, y) => new { x, y })
                                .SelectMany(z => z.y.DefaultIfEmpty(),
                                (c, d) => new { c.x.SD, c.x.HN, c.x.IR, JIS = d, GroupHinName = (c.x.HN.自社品番 == HINBAN_SONOTA ? c.x.SD.自社品名 : null) })      // No.389 Mod
                                .GroupBy(g => new
                                {
                                    g.SD.自社コード,
                                    g.JIS.自社名,          // No.227,228 Add
                                    g.SD.請求年月,
                                    g.SD.請求先コード,
                                    g.SD.請求先枝番,
                                    g.SD.品番コード,
                                    g.HN.自社品番,
                                    g.HN.自社色,
                                    g.HN.シリーズ,
                                    g.GroupHinName,        // No.389 Mod
                                    g.IR.色名称
                                })
                                .Select(s => new TallyMember
                                {
                                    自社コード = s.Key.自社コード,        // No.227,228 Add
                                    自社名 = s.Key.自社名,                // No.227,228 Add
                                    品番コード = s.Key.品番コード,
                                    自社色 = s.Key.自社色,
                                    シリーズ = s.Key.シリーズ,
                                    自社品番 = s.Key.自社品番,
                                    自社品名 = s.Key.GroupHinName,        // No.389 Mod
                                    色名称 = s.Key.色名称,
                                    金額 = s.Sum(m => m.SD.金額),
                                    数量 = (int)s.Sum(m => m.SD.数量)
                                });
                        #endregion

                        // 対象月の集計データを格納
                        tokDic.Add(yearMonth, dtlList.ToList());

                        // カウントアップ
                        targetMonth = targetMonth.AddMonths(1);

                    }
                    #endregion

                    #region 印字用データリスト作成
                    // 年度の集計が終わったらプリントクラスに設定
                    List<BSK01010_PrintMember> printList = new List<BSK01010_PrintMember>();
                    int monthCount = 1;
                    foreach (int dicKey in tokDic.Keys)
                    {
                        List<TallyMember> tallyList = tokDic[dicKey];

                        for (int i = 0; i < tallyList.Count; i++)
                        {
                            int i品番コード = tallyList[i].品番コード;          // No.389 Add

                            BSK01010_PrintMember print = new BSK01010_PrintMember();
                            print.自社コード = tallyList[i].自社コード;
                            print.自社名 = tallyList[i].自社名;
                            print.得意先コード = string.Format("{0:D4} - {1:D2}", tokRow.取引先コード, tokRow.枝番);     // No.132-2 Mod
                            print.得意先名 = tokRow.略称名;  // No.229 Mod
                            print.品番コード = i品番コード;                     // No.389 Mod
                            print.自社品番 = tallyList[i].自社品番;     // No.321 Add
                            print.品番名称 = tallyList[i].自社品名 == null ?
                                hin.Where(c => c.品番コード == i品番コード).Select(c => c.自社品名).FirstOrDefault() : tallyList[i].自社品名;          // No.389 Mod
                            print.色名称 = tallyList[i].色名称;
                            print.集計合計額 += tallyList[i].金額;
                            print.集計合計数量 += tallyList[i].数量;        // No.400 Add

                            #region monthCountにより設定列分け
                            switch (monthCount)
                            {
                                case 1:
                                    print.集計売上額０１ = tallyList[i].金額;
                                    print.集計数量０１ = tallyList[i].数量;       // No.400 Add
                                    break;

                                case 2:
                                    print.集計売上額０２ = tallyList[i].金額;
                                    print.集計数量０２ = tallyList[i].数量;       // No.400 Add
                                    break;

                                case 3:
                                    print.集計売上額０３ = tallyList[i].金額;
                                    print.集計数量０３ = tallyList[i].数量;       // No.400 Add
                                    break;

                                case 4:
                                    print.集計売上額０４ = tallyList[i].金額;
                                    print.集計数量０４ = tallyList[i].数量;       // No.400 Add
                                    break;

                                case 5:
                                    print.集計売上額０５ = tallyList[i].金額;
                                    print.集計数量０５ = tallyList[i].数量;       // No.400 Add
                                    break;

                                case 6:
                                    print.集計売上額０６ = tallyList[i].金額;
                                    print.集計数量０６ = tallyList[i].数量;       // No.400 Add
                                    break;

                                case 7:
                                    print.集計売上額０７ = tallyList[i].金額;
                                    print.集計数量０７ = tallyList[i].数量;       // No.400 Add
                                    break;

                                case 8:
                                    print.集計売上額０８ = tallyList[i].金額;
                                    print.集計数量０８ = tallyList[i].数量;       // No.400 Add
                                    break;

                                case 9:
                                    print.集計売上額０９ = tallyList[i].金額;
                                    print.集計数量０９ = tallyList[i].数量;       // No.400 Add
                                    break;

                                case 10:
                                    print.集計売上額１０ = tallyList[i].金額;
                                    print.集計数量１０ = tallyList[i].数量;       // No.400 Add
                                    break;

                                case 11:
                                    print.集計売上額１１ = tallyList[i].金額;
                                    print.集計数量１１ = tallyList[i].数量;       // No.400 Add
                                    break;

                                case 12:
                                    print.集計売上額１２ = tallyList[i].金額;
                                    print.集計数量１２ = tallyList[i].数量;       // No.400 Add
                                    break;

                                default:
                                    break;
                            }
                            #endregion

                            printList.Add(print);

                        }

                        monthCount++;

                    }
                    #endregion

                    #region データリストを集計して最終データを作成
                    decimal total = Convert.ToDecimal(printList.AsEnumerable().Sum(m => m.集計合計額));
                    int NumTotal = Convert.ToInt32(printList.Sum(t => t.集計合計数量));      // No.400 Add
                    // 合計がゼロとなるデータは出力対象外とする
                    if (total == 0)
                        continue;

                    printList =
                        printList.AsEnumerable()
                            .GroupBy(g => new { g.自社コード, g.自社名, g.得意先コード, g.得意先名, g.品番コード, g.品番名称, g.自社品番, g.色名称 }) // No.227,228 Mod No.321
                            .Select(s => new BSK01010_PrintMember
                            {
                                自社コード = s.Key.自社コード,        // No.227,228 Mod
                                自社名 = s.Key.自社名,                // No.227,228 Mod
                                得意先コード = s.Key.得意先コード,
                                得意先名 = s.Key.得意先名,
                                品番コード = s.Key.品番コード,
                                自社品番 = s.Key.自社品番,            // No.321 Add   
                                品番名称 = string.Format("{0} {1}", s.Key.自社品番, s.Key.品番名称),   // No.321 Mod
                                色名称 = s.Key.色名称,
                                集計売上額０１ = s.Sum(m => m.集計売上額０１),
                                集計売上額０２ = s.Sum(m => m.集計売上額０２),
                                集計売上額０３ = s.Sum(m => m.集計売上額０３),
                                集計売上額０４ = s.Sum(m => m.集計売上額０４),
                                集計売上額０５ = s.Sum(m => m.集計売上額０５),
                                集計売上額０６ = s.Sum(m => m.集計売上額０６),
                                集計売上額０７ = s.Sum(m => m.集計売上額０７),
                                集計売上額０８ = s.Sum(m => m.集計売上額０８),
                                集計売上額０９ = s.Sum(m => m.集計売上額０９),
                                集計売上額１０ = s.Sum(m => m.集計売上額１０),
                                集計売上額１１ = s.Sum(m => m.集計売上額１１),
                                集計売上額１２ = s.Sum(m => m.集計売上額１２),
                                集計合計額 = s.Sum(m => m.集計合計額),
                                構成比率 =
                                    Math.Round(
                                        Decimal.Divide(s.Sum(m => m.集計合計額), total) * 100, 2),

                                // No.400 Add Start
                                集計数量０１ = s.Sum(m => m.集計数量０１),
                                集計数量０２ = s.Sum(m => m.集計数量０２),
                                集計数量０３ = s.Sum(m => m.集計数量０３),
                                集計数量０４ = s.Sum(m => m.集計数量０４),
                                集計数量０５ = s.Sum(m => m.集計数量０５),
                                集計数量０６ = s.Sum(m => m.集計数量０６),
                                集計数量０７ = s.Sum(m => m.集計数量０７),
                                集計数量０８ = s.Sum(m => m.集計数量０８),
                                集計数量０９ = s.Sum(m => m.集計数量０９),
                                集計数量１０ = s.Sum(m => m.集計数量１０),
                                集計数量１１ = s.Sum(m => m.集計数量１１),
                                集計数量１２ = s.Sum(m => m.集計数量１２),
                                集計合計数量 = s.Sum(m => m.集計合計数量),
                                数量構成比率 = NumTotal == 0 ? 0 :
                                Math.Round(
                                    Decimal.Divide(s.Sum(m => m.集計合計数量), NumTotal) * 100, 2)
                                // No.400 Add End

                            })
                            .OrderBy(o => o.得意先コード)
                            .ThenBy(t => t.品番コード)
                            .ToList();

                    #endregion

                    resultList.AddRange(printList);

                }

                result.PRINT_DATA = resultList;
                return result;

            }

        }
        #endregion

        #region 検索パラメータ展開
        /// <summary>
        /// 検索パラメータ展開
        /// </summary>
        /// <param name="paramDic"></param>
        /// <param name="company"></param>
        /// <param name="fromCode"></param>
        /// <param name="fromEda"></param>
        /// <param name="toCode"></param>
        /// <param name="toEda"></param>
        /// <param name="startYm"></param>
        /// <param name="endYm"></param>
        private void getFormParams(Dictionary<string, string> paramDic, out DateTime? startYm, out DateTime? endYm, out int? company, out int? fromCode, out int? fromEda,
                                    out int? toCode, out int? toEda, out string productFrom, out string productTo)
        {
            int ival;
            DateTime dWk;

            // パラメータ展開
            company = Int32.TryParse(paramDic["自社コード"], out ival) ? ival : (int?)null;
            fromCode = Int32.TryParse(paramDic["得意先コードFrom"], out ival) ? ival : (int?)null;
            fromEda = Int32.TryParse(paramDic["得意先枝番From"], out ival) ? ival : (int?)null;
            toCode = Int32.TryParse(paramDic["得意先コードTo"], out ival) ? ival : (int?)null;
            toEda = Int32.TryParse(paramDic["得意先枝番To"], out ival) ? ival : (int?)null;
            startYm = DateTime.TryParse(paramDic["処理開始"], out dWk) ? dWk : (DateTime?)null;
            endYm = DateTime.TryParse(paramDic["処理終了"], out dWk) ? dWk : (DateTime?)null;
            productFrom = paramDic["自社品番From"];
            productTo = paramDic["自社品番To"];
        }
        #endregion
    }

}