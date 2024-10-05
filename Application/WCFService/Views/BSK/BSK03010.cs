using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// ブランド･商品別売上統計表 サービスクラス
    /// </summary>
    public class BSK03010
    {

        #region << 定数定義 >>

        /// <summary>
        /// 「その他」の自社品番
        /// </summary>
        private const string SONOTA_HINBAN = "99-99";         // No.389 Add

        #endregion

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
            public string ブランドコード { get; set; }
            public string ブランド名 { get; set; }
            public int 品番コード { get; set; }
            public string 自社品番 { get; set; }     // No.322 Add
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
            // No.402 Add Start
            public long 集計数量０１ { get; set; }
            public long 集計数量０２ { get; set; }
            public long 集計数量０３ { get; set; }
            public long 集計数量０４ { get; set; }
            public long 集計数量０５ { get; set; }
            public long 集計数量０６ { get; set; }
            public long 集計数量０７ { get; set; }
            public long 集計数量０８ { get; set; }
            public long 集計数量０９ { get; set; }
            public long 集計数量１０ { get; set; }
            public long 集計数量１１ { get; set; }
            public long 集計数量１２ { get; set; }
            public long 集計合計数量 { get; set; }
            public decimal 数量構成比率 { get; set; }
            // No.402 Add End
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
            public string ブランド { get; set; }     // No.402 Add
            public string ブランド名 { get; set; }   // No.402 Add
            public string 自社品番 { get; set; }
            public string 自社品名 { get; set; }
            public string 色名称 { get; set; }
            public long 金額 { get; set; }
            public int 数量 { get; set; }            // No.402 Add
        }

        #endregion

        #endregion
        public class BSK03010_DATASET
        {
            public List<BSK03010_PrintMember> PRINT_DATA = null;
        }
        #region 集計データ取得
        /// <summary>
        /// 集計情報を取得する
        /// </summary>
        /// <param name="paramDic"></param>
        /// <returns></returns>
        public BSK03010_DATASET GetPrintList(Dictionary<string, string> paramDic)
        {
            BSK03010_DATASET result = new BSK03010_DATASET();

            // パラメータ展開
            int? company;
            string brandFrom, brandTo, productFrom, productTo;
            int itemTypeFrom, itemTypeTo;
            DateTime? startYm, endYm;

            getFormParams(paramDic, out company, out brandFrom, out brandTo, out productFrom, out productTo, out itemTypeFrom, out itemTypeTo, out startYm, out endYm); // No.402 Add

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 対象として取引区分：得意先、相殺を対象とする
                List<int> kbnList = new List<int>() { (int)CommonConstants.取引区分.得意先, (int)CommonConstants.取引区分.相殺, (int)CommonConstants.取引区分.販社 };  // No.402 Mod

                var tok =
                    context.M01_TOK.Where(w => kbnList.Contains(w.取引区分));

                // No.402 Add Start
                var hin =
                    context.M09_HIN.AsQueryable();

                #region 条件絞り込み
                // 自社が指定されていれば条件追加
                if (company != null)
                {
                    var jis = context.M70_JIS.Where(w => w.自社コード == company).FirstOrDefault();
                    tok = tok.Where(w => w.担当会社コード == jis.自社コード);
                }

                // ブランドが指定されている場合
                string compBrandFrom = brandFrom;
                string compBrandTo = brandTo;

                // ブランドFromToを再設定 0123…ABC順で絞り込む
                if (!string.IsNullOrEmpty(brandFrom) && !string.IsNullOrEmpty(brandTo) &&
                    brandFrom.CompareTo(brandTo) > 0)
                {
                    compBrandFrom = brandTo;
                    compBrandTo = brandFrom;
                }

                if (!string.IsNullOrEmpty(brandFrom))
                {
                    hin = hin.Where(w => w.ブランド.CompareTo(compBrandFrom) >= 0);
                }

                if (!string.IsNullOrEmpty(brandTo))
                {
                    hin = hin.Where(w => w.ブランド.CompareTo(compBrandTo) <= 0);
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

                // 商品形態分類が指定されている場合
                int compTypeFrom = itemTypeFrom;
                int compTypeTo = itemTypeTo;

                // 商品形態分類FromToを再設定
                if (itemTypeFrom != 0 && itemTypeTo != 0 &&
                    itemTypeFrom > itemTypeTo)
                {
                    compTypeFrom = itemTypeTo;
                    compTypeTo = itemTypeFrom;
                }
                if (itemTypeFrom != 0)
                {
                    hin = hin.Where(w => w.商品形態分類 >= compTypeFrom);
                }

                if (itemTypeTo != 0)
                {
                    hin = hin.Where(w => w.商品形態分類 <= compTypeTo);
                }

                #endregion
                // No.402 Add End

                // ソート順を指定
                tok = tok.OrderBy(o => o.取引先コード).ThenBy(t => t.枝番);

                // 得意先毎に集計を実施
                List<BSK03010_PrintMember> resultList = new List<BSK03010_PrintMember>();
                foreach (M01_TOK tokRow in tok)
                {
                    DateTime lastMonth = (DateTime)endYm;               // No.402 Mod
                    DateTime targetMonth = (DateTime)startYm;           // No.402 Mod

                    #region 年月単位で集計データを取得
                    // 年月毎のデータDic
                    Dictionary<int, List<TallyMember>> tokDic = new Dictionary<int, List<TallyMember>>();
                    while (targetMonth <= lastMonth)
                    {
                        int yearMonth = targetMonth.Year * 100 + targetMonth.Month;
                        DateTime dtStartDate = targetMonth;
                        DateTime dtEndDate = dtStartDate.AddMonths(1).AddDays(-1);
                        
                        #region 通常売上
                        // No.227,228 Mod Start
                        var dtlList =
                            context.T02_URHD
                                .Where(w =>
                                    w.会社名コード == tokRow.担当会社コード &&        // No.400 Mod
                                    w.売上日 >= dtStartDate &&
                                    w.売上日 <= dtEndDate &&
                                    w.得意先コード == tokRow.取引先コード &&
                                    w.得意先枝番 == tokRow.枝番 && 
                                    w.削除日時 == null)
                                .Join(context.T02_URDTL,
                                    x => x.伝票番号,
                                    y => y.伝票番号,
                                    (x, y) => new { x, y })
                                .Join(hin,                                          // No.402 Mod
                                    x => x.y.品番コード,
                                    y => y.品番コード,
                                    (x, y) => new { SD = x, HN = y })
                                .GroupJoin(context.M06_IRO,
                                    x => x.HN.自社色,
                                    y => y.色コード,
                                    (x, y) => new { x, y })
                                .SelectMany(z => z.y.DefaultIfEmpty(),
                                    (c, d) => new { c.x.SD, c.x.HN, IR = d })
                                .GroupJoin(context.M14_BRAND,
                                    x => x.HN.ブランド,
                                    y => y.ブランドコード,
                                    (x, y) => new { x, y })
                                .SelectMany(z => z.y.DefaultIfEmpty(),
                                    (e, f) => new { e.x.SD, e.x.HN, e.x.IR, BR = f })
                                .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                                    x => x.SD.x.会社名コード,
                                    y => y.自社コード,
                                    (x, y) => new { x, y })
                                .SelectMany(z => z.y.DefaultIfEmpty(),
                                (g, h) => new { g.x.SD, g.x.HN, g.x.IR, g.x.BR, JIS = h, GroupHinName = g.x.HN.自社品番 == SONOTA_HINBAN ? g.x.SD.y.自社品名 : null })  // No.389 Mod
                                .GroupBy(g => new
                                {
                                    g.SD.x.会社名コード,
                                    g.JIS.自社名,
                                    yearMonth,
                                    g.SD.x.得意先コード,
                                    g.SD.x.得意先枝番,
                                    g.SD.y.品番コード,
                                    g.HN.自社品番,
                                    g.HN.自社色,
                                    g.HN.ブランド,                                  // No.402 Mod
                                    g.BR.ブランド名,                                // No.402 Mod
                                    g.GroupHinName,                                 // No.389 Mod
                                    g.IR.色名称
                                })
                                .Select(s => new TallyMember
                                {
                                    自社コード = s.Key.会社名コード,        // No.227,228 Add
                                    自社名 = s.Key.自社名,                // No.227,228 Add
                                    品番コード = s.Key.品番コード,
                                    自社色 = s.Key.自社色,
                                    ブランド = s.Key.ブランド,
                                    ブランド名 = s.Key.ブランド名,
                                    自社品番 = s.Key.自社品番,
                                    自社品名 = s.Key.GroupHinName,          // No.389 Mod
                                    色名称 = s.Key.色名称,
                                    金額 = (long)s.Sum(m => m.SD.y.金額),           // No.402 Mod
                                    数量 = (int)s.Sum(m => m.SD.y.数量)       // No.402 Mod
                                });
                        // No.227,228 Mod End
                        #endregion

                        #region 通常売上
                        // No.227,228 Mod Start
                        var dtlListHAN =
                            context.T02_URHD_HAN
                                .Join(context.M70_JIS,
                                    x => x.販社コード,
                                    y => y.自社コード,
                                    (x, y) => new { x, y })
                                .Where(w =>
                                    w.x.会社名コード == tokRow.担当会社コード &&        // No.400 Mod
                                    w.x.売上日 >= dtStartDate &&
                                    w.x.売上日 <= dtEndDate &&
                                    w.y.取引先コード == tokRow.取引先コード &&
                                    w.y.枝番 == tokRow.枝番 &&
                                    w.x.削除日時 == null)
                                .Join(context.T02_URDTL_HAN,
                                    x => x.x.伝票番号,
                                    y => y.伝票番号,
                                    (x, y) => new { x, y })
                                .Join(hin,                                          // No.402 Mod
                                    x => x.y.品番コード,
                                    y => y.品番コード,
                                    (x, y) => new { SD = x, HN = y })
                                .GroupJoin(context.M06_IRO,
                                    x => x.HN.自社色,
                                    y => y.色コード,
                                    (x, y) => new { x, y })
                                .SelectMany(z => z.y.DefaultIfEmpty(),
                                    (c, d) => new { c.x.SD, c.x.HN, IR = d })
                                .GroupJoin(context.M14_BRAND,
                                    x => x.HN.ブランド,
                                    y => y.ブランドコード,
                                    (x, y) => new { x, y })
                                .SelectMany(z => z.y.DefaultIfEmpty(),
                                    (e, f) => new { e.x.SD, e.x.HN, e.x.IR, BR = f })
                                .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                                    x => x.SD.x.x.会社名コード,
                                    y => y.自社コード,
                                    (x, y) => new { x, y })
                                .SelectMany(z => z.y.DefaultIfEmpty(),
                                (g, h) => new { g.x.SD, g.x.HN, g.x.IR, g.x.BR, JIS = h, GroupHinName = g.x.HN.自社品番 == SONOTA_HINBAN ? g.x.SD.y.自社品名 : null })  // No.389 Mod
                                .GroupBy(g => new
                                {
                                    g.SD.x.x.会社名コード,
                                    g.JIS.自社名,
                                    yearMonth,
                                    g.SD.x.y.取引先コード,
                                    g.SD.x.y.枝番,
                                    g.SD.y.品番コード,
                                    g.HN.自社品番,
                                    g.HN.自社色,
                                    g.HN.ブランド,                                  // No.402 Mod
                                    g.BR.ブランド名,                                // No.402 Mod
                                    g.GroupHinName,                                 // No.389 Mod
                                    g.IR.色名称
                                })
                                .Select(s => new TallyMember
                                {
                                    自社コード = s.Key.会社名コード,        // No.227,228 Add
                                    自社名 = s.Key.自社名,                // No.227,228 Add
                                    品番コード = s.Key.品番コード,
                                    自社色 = s.Key.自社色,
                                    ブランド = s.Key.ブランド,
                                    ブランド名 = s.Key.ブランド名,
                                    自社品番 = s.Key.自社品番,
                                    自社品名 = s.Key.GroupHinName,          // No.389 Mod
                                    色名称 = s.Key.色名称,
                                    金額 = (long)s.Sum(m => m.SD.y.金額),           // No.402 Mod
                                    数量 = (int)s.Sum(m => m.SD.y.数量)       // No.402 Mod
                                });
                        // No.227,228 Mod End
                        #endregion

                        dtlList = dtlList.Concat(dtlListHAN);
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
                            int i品番コード = tallyList[i].品番コード;        // No.389 Add

                            BSK03010_PrintMember print = new BSK03010_PrintMember();
                            print.自社コード = tallyList[i].自社コード;       // No.227,228 Add
                            print.自社名 = tallyList[i].自社名;               // No.227,228 Add
                            print.ブランドコード = tallyList[i].ブランド;     // No.402 Mod
                            print.ブランド名 = tallyList[i].ブランド名;       // No.402 Mod
                            print.品番コード = i品番コード;                   // No.389 Mod
                            print.自社品番 = tallyList[i].自社品番;           // No.322 Add
                            print.品番名称 = tallyList[i].自社品名 == null ?
                                 hin.Where(c => c.品番コード == i品番コード).Select(c => c.自社品名).FirstOrDefault() : tallyList[i].自社品名;          // No.389 Mod
                            print.色名称 = tallyList[i].色名称;
                            print.集計合計額 += tallyList[i].金額;
                            print.集計合計数量 += tallyList[i].数量;          // No.402 Add

                            #region monthCountにより設定列分け
                            switch (monthCount)
                            {
                                case 1:
                                    print.集計売上額０１ = tallyList[i].金額;
                                    print.集計数量０１ = tallyList[i].数量;             // No.402 Add
                                    break;

                                case 2:
                                    print.集計売上額０２ = tallyList[i].金額;
                                    print.集計数量０２ = tallyList[i].数量;             // No.402 Add
                                    break;

                                case 3:
                                    print.集計売上額０３ = tallyList[i].金額;
                                    print.集計数量０３ = tallyList[i].数量;             // No.402 Add
                                    break;

                                case 4:
                                    print.集計売上額０４ = tallyList[i].金額;
                                    print.集計数量０４ = tallyList[i].数量;             // No.402 Add
                                    break;

                                case 5:
                                    print.集計売上額０５ = tallyList[i].金額;
                                    print.集計数量０５ = tallyList[i].数量;             // No.402 Add
                                    break;

                                case 6:
                                    print.集計売上額０６ = tallyList[i].金額;
                                    print.集計数量０６ = tallyList[i].数量;             // No.402 Add
                                    break;

                                case 7:
                                    print.集計売上額０７ = tallyList[i].金額;
                                    print.集計数量０７ = tallyList[i].数量;             // No.402 Add
                                    break;

                                case 8:
                                    print.集計売上額０８ = tallyList[i].金額;
                                    print.集計数量０８ = tallyList[i].数量;             // No.402 Add
                                    break;

                                case 9:
                                    print.集計売上額０９ = tallyList[i].金額;
                                    print.集計数量０９ = tallyList[i].数量;             // No.402 Add
                                    break;

                                case 10:
                                    print.集計売上額１０ = tallyList[i].金額;
                                    print.集計数量１０ = tallyList[i].数量;             // No.402 Add
                                    break;

                                case 11:
                                    print.集計売上額１１ = tallyList[i].金額;
                                    print.集計数量１１ = tallyList[i].数量;             // No.402 Add
                                    break;

                                case 12:
                                    print.集計売上額１２ = tallyList[i].金額;
                                    print.集計数量１２ = tallyList[i].数量;             // No.402 Add
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
                            .GroupBy(g => new { 自社コード = g.自社コード, 自社名 = g.自社名, ブランドコード = g.ブランドコード, ブランド名 = g.ブランド名, g.品番コード, g.自社品番, g.品番名称, g.色名称 })   // No.227,228 Mod No.322
                            .Select(s => new BSK03010_PrintMember
                            {
                                自社コード = s.Key.自社コード,    // No.227,228 Add
                                自社名 = s.Key.自社名,            // No.227,228 Add
                                ブランドコード = s.Key.ブランドコード,    // No.402 Mod
                                ブランド名 = s.Key.ブランド名,            // No.402 Mod
                                品番コード = s.Key.品番コード,
                                自社品番 = s.Key.自社品番,        // No.322 Add
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
                                // No.402 Add Start
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
                                // No.402 Add End
                            })
                            .OrderBy(o => o.ブランドコード)
                            .ThenBy(t => t.品番コード)
                            .ToList();

                    #endregion

                    resultList.AddRange(printList);

                }

                // 最終集計を実施(ここまでで得意先毎の集計になっている為)
                resultList =
                    resultList
                        .GroupBy(g => new
                        {
                            自社コード = g.自社コード,        // No.227,228 Add
                            自社名 = g.自社名,                // No.227,228 Add
                            ブランドコード = g.ブランドコード,    // No.402 Mod
                            ブランド名 = g.ブランド名,            // No.402 Mod
                            品番コード = g.品番コード,
                            自社品番 = g.自社品番,            // No.322 Add
                            品番名称 = g.品番名称,
                            色名称 = g.色名称
                        })
                        .Select(s => new BSK03010_PrintMember
                        {
                            自社コード = s.Key.自社コード,            // No.227,228 Add
                            自社名 = s.Key.自社名,                    // No.227,228 Add
                            ブランドコード = s.Key.ブランドコード,    // No.402 Mod
                            ブランド名 = s.Key.ブランド名,            // No.402 Mod
                            品番コード = s.Key.品番コード,
                            自社品番 = s.Key.自社品番,                // No.322 Add
                            品番名称 = string.Format("{0} {1}", s.Key.自社品番, s.Key.品番名称),   // No.322 Mod
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
                                resultList.Where(w => w.自社コード == s.Key.自社コード && w.ブランドコード == s.Key.ブランドコード).Sum(m => m.集計合計額) == 0 ?         // No.420 Mod
                                    0 :
                                    Math.Round(
                                        Decimal.Divide(
                                            s.Sum(m => m.集計合計額),
                                            resultList
                                                .Where(w => w.自社コード == s.Key.自社コード && w.ブランドコード == s.Key.ブランドコード).Sum(m => m.集計合計額)          // No.420 Mod
                                            ) * 100, 2),
                            // No.402 Add Start
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
                            数量構成比率 =
                                resultList.Where(w => w.自社コード == s.Key.自社コード && w.ブランドコード == s.Key.ブランドコード).Sum(m => m.集計合計数量) == 0 ?       // No.420 Mod
                                    0 :
                                    Math.Round(
                                        Decimal.Divide(
                                            s.Sum(m => m.集計合計数量),
                                            resultList
                                                .Where(w => w.自社コード == s.Key.自社コード && w.ブランドコード == s.Key.ブランドコード).Sum(m => m.集計合計数量)        // No.420 Mod
                                            ) * 100, 2)
                            // No.402 Add End
                        })
                        .ToList();

                result.PRINT_DATA = resultList
                                        .OrderBy(c => c.自社コード)
                                        .ThenBy(t => t.ブランドコード)
                                        .ThenBy(t => t.自社品番).ToList();
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
        /// <param name="brandFrom"></param>
        /// <param name="brandTo"></param>
        /// <param name="productFrom"></param>
        /// <param name="productTo"></param>
        /// <param name="itemTypeFrom"></param>
        /// <param name="itemTypeTo"></param>
        /// <param name="startYm"></param>
        /// <param name="endYm"></param>
        private void getFormParams(Dictionary<string, string> paramDic, out int? company, out string brandFrom, out string brandTo,
                                    out string productFrom, out string productTo, out int itemTypeFrom, out int itemTypeTo,
                                    out DateTime? startYm, out DateTime? endYm)
        {
            int ival;
            DateTime dWk;

            company = Int32.TryParse(paramDic["自社コード"], out ival) ? ival : (int?)null;
            brandFrom = paramDic["ブランドコードFrom"];
            brandTo = paramDic["ブランドコードTo"];
            productFrom = paramDic["自社品番From"];
            productTo = paramDic["自社品番To"];
            itemTypeFrom = Int32.TryParse(paramDic["商品形態分類From"], out ival) ? ival : 0;
            itemTypeTo = Int32.TryParse(paramDic["商品形態分類To"], out ival) ? ival : 0;
            startYm = DateTime.TryParse(paramDic["処理開始"], out dWk) ? dWk : (DateTime?)null;
            endYm = DateTime.TryParse(paramDic["処理終了"], out dWk) ? dWk : (DateTime?)null;
        }
        #endregion
    }

}