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
        }

        #endregion

        public class BSK01010_DATASET
        {
            public List<BSK01010_PrintMember> PRINT_DATA = null;
            public List<M70_JIS> M70 = null;
        }

        #endregion

        #region 集計データ取得
        /// <summary>
        /// 集計情報を取得する
        /// </summary>
        /// <param name="paramDic"></param>
        /// <returns></returns>
        //public List<BSK01010_PrintMember> GetPrintList(Dictionary<string, string> paramDic)
        public BSK01010_DATASET GetPrintList(Dictionary<string, string> paramDic)
        {
            BSK01010_DATASET result = new BSK01010_DATASET();
            // パラメータ展開
            int ival,
                company = int.Parse(paramDic["自社コード"]),
                year = int.Parse(paramDic["処理年度"].Replace("/", ""));
            int? customerCd = int.TryParse(paramDic["得意先コード"], out ival) ? ival : (int?)null,
                customerEd = int.TryParse(paramDic["得意先枝番"], out ival) ? ival : (int?)null;
            string output = paramDic["出力項目"];

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 自社情報を取得
                result.M70 =
                    context.M70_JIS
                        .Where(w => w.自社コード == company).ToList();
                 
               var jis = result.M70.FirstOrDefault();

                // 対象として取引区分：得意先、相殺を対象とする
                List<int> kbnList = new List<int>() { (int)CommonConstants.取引区分.得意先, (int)CommonConstants.取引区分.相殺 };
                if (jis.自社区分 == (int)CommonConstants.自社区分.自社)
                    kbnList.Add((int)CommonConstants.取引区分.販社);

                var tok =
                    context.M01_TOK.Where(w => w.削除日時 == null && kbnList.Contains(w.取引区分));

                // 取引先が指定されていれば条件追加
                if (customerCd != null && customerEd != null)
                    tok = tok.Where(w => w.取引先コード == customerCd && w.枝番 == customerEd);

                // ソート順を指定
                tok = tok.OrderBy(o => o.取引先コード).ThenBy(t => t.枝番);

                // 得意先毎に集計を実施
                List<BSK01010_PrintMember> resultList = new List<BSK01010_PrintMember>();
                foreach (M01_TOK tokRow in tok)
                {
                    // 決算月・請求締日から売上集計期間を算出する
                    int pMonth = jis.決算月 ?? CommonConstants.DEFAULT_SETTLEMENT_MONTH,
                        pYear = year + 1;

                    DateTime lastMonth = new DateTime(pYear, pMonth, 1);
                    DateTime targetMonth = lastMonth.AddMonths(-11);

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
                                    w.自社コード == company &&
                                    w.請求年月 == yearMonth &&
                                    w.請求先コード == tokRow.取引先コード &&
                                    w.請求先枝番 == tokRow.枝番)
                                .GroupJoin(context.M09_HIN,
                                    x => x.品番コード,
                                    y => y.品番コード,
                                    (x, y) => new { x, y })
                                .SelectMany(z => z.y.DefaultIfEmpty(),
                                    (a, b) => new { SD = a.x, HN = b })
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
                                    (c, d) => new { c.x.SD, c.x.HN, c.x.IR, JIS = d})
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
                                    g.HN.自社品名,
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
                                    自社品名 = s.Key.自社品名,
                                    色名称 = s.Key.色名称,
                                    金額 = output == "1" ? s.Sum(m => m.SD.金額) : (int)s.Sum(m => m.SD.数量),
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
                            BSK01010_PrintMember print = new BSK01010_PrintMember();
                            print.自社コード = tallyList[i].自社コード;
                            print.自社名 = tallyList[i].自社名;
                            print.得意先コード = string.Format("{0:D4} - {1:D2}", tokRow.取引先コード, tokRow.枝番);     // No.132-2 Mod
                            print.得意先名 = tokRow.略称名;  // No.229 Mod
                            print.品番コード = tallyList[i].品番コード;
                            print.品番名称 = tallyList[i].自社品名;
                            print.色名称 = tallyList[i].色名称;
                            print.集計合計額 += tallyList[i].金額;

                            #region monthCountにより設定列分け
                            switch (monthCount)
                            {
                                case 1:
                                    print.集計売上額０１ = tallyList[i].金額;
                                    break;

                                case 2:
                                    print.集計売上額０２ = tallyList[i].金額;
                                    break;

                                case 3:
                                    print.集計売上額０３ = tallyList[i].金額;
                                    break;

                                case 4:
                                    print.集計売上額０４ = tallyList[i].金額;
                                    break;

                                case 5:
                                    print.集計売上額０５ = tallyList[i].金額;
                                    break;

                                case 6:
                                    print.集計売上額０６ = tallyList[i].金額;
                                    break;

                                case 7:
                                    print.集計売上額０７ = tallyList[i].金額;
                                    break;

                                case 8:
                                    print.集計売上額０８ = tallyList[i].金額;
                                    break;

                                case 9:
                                    print.集計売上額０９ = tallyList[i].金額;
                                    break;

                                case 10:
                                    print.集計売上額１０ = tallyList[i].金額;
                                    break;

                                case 11:
                                    print.集計売上額１１ = tallyList[i].金額;
                                    break;

                                case 12:
                                    print.集計売上額１２ = tallyList[i].金額;
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
                    // 合計がゼロとなるデータは出力対象外とする
                    if (total == 0)
                        continue;

                    printList =
                        printList.AsEnumerable()
                            .GroupBy(g => new { g.自社コード, g.自社名, g.得意先コード, g.得意先名, g.品番コード, g.品番名称, g.色名称 }) // No.227,228 Mod
                            .Select(s => new BSK01010_PrintMember
                            {
                                自社コード = s.Key.自社コード,        // No.227,228 Mod
                                自社名 = s.Key.自社名,                // No.227,228 Mod
                                得意先コード = s.Key.得意先コード,
                                得意先名 = s.Key.得意先名,
                                品番コード = s.Key.品番コード,
                                品番名称 = string.Format("{0} {1}", s.Key.品番コード, s.Key.品番名称),   // No.321 Mod
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
                                        Decimal.Divide(s.Sum(m => m.集計合計額), total) * 100, 2)
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

    }

}