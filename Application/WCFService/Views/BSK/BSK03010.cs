using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// シリーズ･商品別売上統計表 サービスクラス
    /// </summary>
    public class BSK03010
    {
        #region << メンバクラス定義 >>

        #region 帳票印刷メンバ
        /// <summary>
        /// 帳票印刷メンバクラス
        /// </summary>
        public class BSK03010_PrintMember
        {
            /// <summary>得意先コード</summary>
            /// <remarks>
            /// 帳票で改ページさせる為、
            /// 「コード」「枝番」を結合して設定
            /// </remarks>
            public int 自社コード { get; set; }      // No.227,228 Add
            public string 自社名 { get; set; }       // No.227,228 Add
            public string シリーズコード { get; set; }
            public string シリーズ名 { get; set; }
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
            public string シリーズ名 { get; set; }
            public string 自社品番 { get; set; }
            public string 自社品名 { get; set; }
            public string 色名称 { get; set; }
            public long 金額 { get; set; }
        }

        #endregion

        #endregion

        #region 集計データ取得
        /// <summary>
        /// 集計情報を取得する
        /// </summary>
        /// <param name="paramDic"></param>
        /// <returns></returns>
        public List<BSK03010_PrintMember> GetPrintList(Dictionary<string, string> paramDic)
        {
            // パラメータ展開
            int company = int.Parse(paramDic["自社コード"]),
                year = int.Parse(paramDic["処理年度"].Replace("/", ""));
            string seriesCode = paramDic["シリーズコード"];

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 自社情報を取得
                var jis =
                    context.M70_JIS
                        .Where(w => w.自社コード == company)
                        .FirstOrDefault();

                // 対象として取引区分：得意先、相殺を対象とする
                List<int> kbnList = new List<int>() { (int)CommonConstants.取引区分.得意先, (int)CommonConstants.取引区分.相殺 };
                if (jis.自社区分 == (int)CommonConstants.自社区分.自社)
                    kbnList.Add((int)CommonConstants.取引区分.販社);

                var tok =
                    context.M01_TOK.Where(w => w.削除日時 == null && kbnList.Contains(w.取引区分));

                // ソート順を指定
                tok = tok.OrderBy(o => o.取引先コード).ThenBy(t => t.枝番);

                // 得意先毎に集計を実施
                List<BSK03010_PrintMember> resultList = new List<BSK03010_PrintMember>();
                foreach (M01_TOK tokRow in tok)
                {
                    // 決算月・請求締日から売上集計期間を算出する
                    int pMonth = jis.決算月 ?? CommonConstants.DEFAULT_SETTLEMENT_MONTH,
                        pYear = pMonth < 4 ? year + 1 : year;

                    DateTime lastMonth = new DateTime(pYear, pMonth, 1);
                    DateTime targetMonth = lastMonth.AddMonths(-11);

                    #region 年月単位で集計データを取得
                    // 年月毎のデータDic
                    Dictionary<int, List<TallyMember>> tokDic = new Dictionary<int, List<TallyMember>>();
                    while (targetMonth <= lastMonth)
                    {
                        int yearMonth = targetMonth.Year * 100 + targetMonth.Month;

                        #region linq
                        // No.227,228 Mod Start
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
                                    (c, d) => new { c.x.SD, c.x.HN, IR = d })
                                .GroupJoin(context.M15_SERIES,
                                    x => x.HN.シリーズ,
                                    y => y.シリーズコード,
                                    (x, y) => new { x, y })
                                .SelectMany(z => z.y.DefaultIfEmpty(),
                                    (e, f) => new { e.x.SD, e.x.HN, e.x.IR, SR = f })
                                .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                                    x => x.SD.自社コード,
                                    y => y.自社コード,
                                    (x, y) => new { x, y })
                                .SelectMany(z => z.y.DefaultIfEmpty(),
                                    (g, h) => new { g.x.SD, g.x.HN, g.x.IR, g.x.SR, JIS = h})
                                .GroupBy(g => new
                                {
                                    g.SD.自社コード,
                                    g.JIS.自社名,
                                    g.SD.請求年月,
                                    g.SD.請求先コード,
                                    g.SD.請求先枝番,
                                    g.SD.品番コード,
                                    g.HN.自社品番,
                                    g.HN.自社色,
                                    g.HN.シリーズ,
                                    g.SR.シリーズ名,
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
                                    シリーズ名 = s.Key.シリーズ名,
                                    自社品番 = s.Key.自社品番,
                                    自社品名 = s.Key.自社品名,
                                    色名称 = s.Key.色名称,
                                    金額 = s.Sum(m => m.SD.金額)
                                });
                        // No.227,228 Mod End
                        #endregion

                        // シリーズ指定がある場合は絞り込み
                        if (!string.IsNullOrEmpty(seriesCode))
                            dtlList =
                                dtlList.Where(w => w.シリーズ == seriesCode);

                        // 対象月の集計データを格納
                        tokDic.Add(yearMonth, dtlList.ToList());

                        // カウントアップ
                        targetMonth = targetMonth.AddMonths(1);

                    }
                    #endregion

                    #region 印字用データリスト作成
                    // 年度の集計が終わったらプリントクラスに設定
                    List<BSK03010_PrintMember> printList = new List<BSK03010_PrintMember>();
                    int monthCount = 1;
                    foreach (int dicKey in tokDic.Keys)
                    {
                        List<TallyMember> tallyList = tokDic[dicKey];

                        for (int i = 0; i < tallyList.Count; i++)
                        {
                            BSK03010_PrintMember print = new BSK03010_PrintMember();
                            print.自社コード = tallyList[i].自社コード;       // No.227,228 Add
                            print.自社名 = tallyList[i].自社名;               // No.227,228 Add
                            print.シリーズコード = tallyList[i].シリーズ;
                            print.シリーズ名 = tallyList[i].シリーズ名;
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
                    printList =
                        printList.AsEnumerable()
                            .GroupBy(g => new { 自社コード = g.自社コード, 自社名 = g.自社名, シリーズコード = g.シリーズコード, シリーズ名 = g.シリーズ名, g.品番コード, g.品番名称, g.色名称 })   // No.227,228 Mod
                            .Select(s => new BSK03010_PrintMember
                            {
                                自社コード = s.Key.自社コード,    // No.227,228 Add
                                自社名 = s.Key.自社名,            // No.227,228 Add
                                シリーズコード = s.Key.シリーズコード,
                                シリーズ名 = s.Key.シリーズ名,
                                品番コード = s.Key.品番コード,
                                品番名称 = s.Key.品番名称,
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
                            })
                            .OrderBy(o => o.シリーズコード)
                            .ThenBy(t => t.品番コード)
                            .ToList();

                    #endregion

                    resultList.AddRange(printList);

                }

                // 最終集計を実施(ここまでで得意先毎の集計になっている為)
                resultList =
                    resultList
                        .GroupBy(g => new {
                            自社コード = g.自社コード,        // No.227,228 Add
                            自社名 = g.自社名,                // No.227,228 Add
                            シリーズコード = g.シリーズコード,
                            シリーズ名 = g.シリーズ名,
                            品番コード = g.品番コード,
                            品番名称 = g.品番名称,
                            色名称 = g.色名称
                        })
                        .Select(s => new BSK03010_PrintMember
                        {
                            自社コード = s.Key.自社コード,            // No.227,228 Add
                            自社名 = s.Key.自社名,                    // No.227,228 Add
                            シリーズコード = s.Key.シリーズコード,
                            シリーズ名 = s.Key.シリーズ名,
                            品番コード = s.Key.品番コード,
                            品番名称 = s.Key.品番名称,
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
                                resultList.Where(w => w.シリーズコード == s.Key.シリーズコード).Sum(m => m.集計合計額) == 0 ?
                                    0 :
                                    Math.Round(
                                        Decimal.Divide(
                                            s.Sum(m => m.集計合計額),
                                            resultList
                                                .Where(w => w.シリーズコード == s.Key.シリーズコード).Sum(m => m.集計合計額)
                                            ) * 100, 2)
                        })
                        .ToList();

                return resultList;

            }

        }
        #endregion

    }

}