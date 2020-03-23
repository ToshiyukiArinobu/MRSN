﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 得意先別売上統計表 サービスクラス
    /// </summary>
    public class BSK02010
    {
        #region << メンバクラス定義 >>

        #region 帳票印刷メンバ
        /// <summary>
        /// 帳票印刷メンバクラス
        /// </summary>
        public class BSK02010_PrintMember
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
            public long 金額 { get; set; }
        }

        #endregion

        #endregion

        public class BSK02010_DATASET
        {
            public List<BSK02010_PrintMember> PRINT_DATA = null;
            //public List<M70_JIS> M70 = null;
        }

        #region 集計データ取得
        /// <summary>
        /// 集計情報を取得する
        /// </summary>
        /// <param name="paramDic"></param>
        /// <returns></returns>
        public BSK02010_DATASET GetPrintList(Dictionary<string, string> paramDic)
        {
            BSK02010_DATASET result = new BSK02010_DATASET();

            // パラメータ展開
            int? company, fromCode, fromEda, toCode, toEda;
            DateTime? startYm, endYm;

            getFormParams(paramDic, out company, out fromCode, out fromEda, out toCode, out toEda, out startYm, out endYm);     // No.398.Add

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 対象として取引区分：得意先、相殺、販社を対象とする(全販社対象とするNo.398)
                List<int> kbnList = new List<int>() { (int)CommonConstants.取引区分.得意先, (int)CommonConstants.取引区分.相殺, (int)CommonConstants.取引区分.販社 };

                var tok =
                    context.M01_TOK.Where(w => w.削除日時 == null &&
                                            kbnList.Contains(w.取引区分)); 
                
                // No.398 Add Start
                #region 条件絞り込み
                // 自社が指定されていれば条件追加
                if (company != null)
                {
                    var jis = context.M70_JIS.Where(w => w.自社コード == company).FirstOrDefault();
                    tok = tok.Where(w => w.担当会社コード == jis.自社コード);
                }

                // 得意先が指定されている場合
                if (fromCode != null)
                {
                    tok = tok.Where(w => w.取引先コード >= fromCode && w.枝番 >= fromEda);
                }
                if (toCode != null)
                {
                    tok = tok.Where(w => w.取引先コード <= toCode && w.枝番 <= toEda);
                }

                tok = tok.OrderBy(o => o.担当会社コード).ThenBy(t => t.取引先コード).ThenBy(t => t.枝番);

                #endregion
                // No.398 Add End

                // 得意先毎に集計を実施
                List<BSK02010_PrintMember> resultList = new List<BSK02010_PrintMember>();
                foreach (M01_TOK tokRow in tok)
                {
                    DateTime targetMonth = (DateTime)startYm;   // No.398 Mod
                    DateTime lastMonth = (DateTime)endYm;       // No.398 Mod

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
                                    w.自社コード == tokRow.担当会社コード &&        // No.398 Mod
                                    w.請求年月 == yearMonth &&
                                    w.請求先コード == tokRow.取引先コード &&
                                    w.請求先枝番 == tokRow.枝番)
                                .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                                    x => x.自社コード,
                                    y => y.自社コード,
                                    (x, y) => new { x, y })
                                .SelectMany(x => x.y.DefaultIfEmpty(),
                                    (a, b) => new { SEIDH = a.x, JIS = b})
                                .GroupBy(g => new { g.SEIDH.自社コード, g.JIS.自社名, g.SEIDH.請求年月, g.SEIDH.請求先コード, g.SEIDH.請求先枝番 })
                                .Select(s => new TallyMember
                                {
                                    自社コード = s.Key.自社コード,
                                    自社名 = s.Key.自社名,
                                    金額 = s.Sum(m => m.SEIDH.金額)
                                });
                        // No.227,228 Mod End
                        #endregion

                        // 対象月の集計データを格納
                        tokDic.Add(yearMonth, dtlList.ToList());

                        // カウントアップ
                        targetMonth = targetMonth.AddMonths(1);

                    }
                    #endregion

                    #region 印字用データリスト作成
                    // 年度の集計が終わったらプリントクラスに設定
                    List<BSK02010_PrintMember> printList = new List<BSK02010_PrintMember>();
                    int monthCount = 1;
                    foreach (int dicKey in tokDic.Keys)
                    {
                        List<TallyMember> tallyList = tokDic[dicKey];

                        for (int i = 0; i < tallyList.Count; i++)
                        {
                            BSK02010_PrintMember print = new BSK02010_PrintMember();
                            print.自社コード = tallyList[i].自社コード;       // No.227,228 Add
                            print.自社名 = tallyList[i].自社名;               // No.227,228 Add
                            print.得意先コード = string.Format("{0:D4} - {1:D2}", tokRow.取引先コード, tokRow.枝番);     // No.132-3 Mod
                            print.得意先名 = tokRow.略称名;     // No.229 Mod
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

                    resultList.AddRange(printList);

                }

                #region データリストを集計して最終データを作成
                decimal total = Convert.ToDecimal(resultList.Sum(t => t.集計合計額));
                // 合計がゼロとなるデータは出力対象外とする
                if (total == 0)
                {
                    result.PRINT_DATA = new List<BSK02010_PrintMember>();
                    return result;
                }

                resultList =
                    resultList.AsEnumerable()
                        .GroupBy(g => new { g.自社コード, g.自社名, g.得意先コード, g.得意先名 })
                        .Select(s => new BSK02010_PrintMember
                        {
                            自社コード = s.Key.自社コード,        // No.227,228 Add
                            自社名 = s.Key.自社名,                // No.227,228 Add
                            得意先コード = s.Key.得意先コード,
                            得意先名 = s.Key.得意先名,
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
                        .OrderBy(o => o.自社コード).ThenBy(t => t.得意先コード)        // No.398 Mod
                        .ToList();

                #endregion

                result.PRINT_DATA = resultList;

                return result;

            }

        }

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
        private void getFormParams(Dictionary<string, string> paramDic, out int? company, out int? fromCode, out int? fromEda, 
                                    out int? toCode, out int? toEda, out DateTime? startYm, out DateTime? endYm)
        {
            int ival;
            DateTime dWk;

            company = Int32.TryParse(paramDic["自社コード"], out ival) ? ival : (int?)null;
            fromCode = Int32.TryParse(paramDic["得意先コードFROM"], out ival) ? ival : (int?)null;
            fromEda = Int32.TryParse(paramDic["得意先枝番FROM"], out ival) ? ival : (int?)null;
            toCode = Int32.TryParse(paramDic["得意先コードTO"], out ival) ? ival : (int?)null;
            toEda = Int32.TryParse(paramDic["得意枝番先TO"], out ival) ? ival : (int?)null;
            startYm = DateTime.TryParse(paramDic["処理開始"], out dWk) ? dWk : (DateTime?)null;
            endYm = DateTime.TryParse(paramDic["処理終了"], out dWk) ? dWk : (DateTime?)null;
        }
        #endregion
        #endregion

    }

}