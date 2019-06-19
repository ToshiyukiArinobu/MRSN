using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 月次在庫締集計サービスクラス
    /// </summary>
    public class ZIK01010
    {
        #region << 項目定義メンバクラス >>

        /// <summary>
        /// 基本倉庫・品番項目メンバークラス
        /// </summary>
        public class BaseItemMember
        {
            public int 倉庫コード { get; set; }
            public int 品番コード { get; set; }
            public DateTime 賞味期限 { get; set; }
        }

        /// <summary>
        /// 入出庫項目メンバクラス
        /// </summary>
        public class HistorySummaryMember : BaseItemMember
        {
            public int 数量 { get; set; }
        }

        /// <summary>
        /// 帳票印刷データ
        /// </summary>
        public class PrintDataMember
        {
            public int 品番コード { get; set; }
            public int 倉庫コード { get; set; }
            public DateTime? 賞味期限 { get; set; }
            public string 自社品番 { get; set; }
            public string 自社品名 { get; set; }
            public string 倉庫名 { get; set; }
            public int 在庫数量 { get; set; }
        }
        #endregion


        #region 締集計実行済確認
        /// <summary>
        /// 対象年月の締集計が実行されているかを確認する
        /// </summary>
        /// <param name="YearMonth">集計年月</param>
        /// <returns>true:作成済、false:未作成</returns>
        public bool IsCheckSummary(string yearMonth)
        {
            int iYearMonth = int.Parse(yearMonth.Replace("/", ""));

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                int cnt = context.S05_STOK_MONTH.Where(w => w.締年月 == iYearMonth).Count();

                return cnt > 0;

            }

        }
        #endregion

        #region 在庫集計処理
        /// <summary>
        /// 在庫集計処理
        /// </summary>
        /// <param name="paramDic">パラメータ辞書</param>
        /// <param name="userId">ログインユーザID</param>
        public void InventorySummary(Dictionary<string, string> paramDic, int userId)
        {
            string yearMonth = paramDic["集計年月"];
            int iYearMonth = int.Parse(yearMonth.Replace("/", ""));
            DateTime startDate = DateTime.Parse(string.Format("{0}/01", yearMonth));
            // 集計月前月
            int oneOldYearMonth = int.Parse(startDate.AddMonths(-1).ToString("yyyyMM"));
            // 集計月末日
            DateTime endDate = new DateTime(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    try
                    {

                        #region 集計のベースとなる倉庫・商品データを作成

                        // -- A.倉庫・品番の全組合せリスト
                        var itemList =
                            context.M22_SOUK.Where(w => w.削除日時 == null)
                                .SelectMany(x => context.M09_HIN.Where(w => w.削除日時 == null),
                                    (x, y) => new { SOUK = x, HIN = y });

                        // -- B.前月の在庫締集計結果データ
                        var stokMonthList =
                            context.S05_STOK_MONTH
                                .Where(w => w.締年月 == oneOldYearMonth);

                        // -- C.対象月の入出庫データ
                        var historyList =
                            context.S04_HISTORY
                                .Where(w => w.入出庫日 >= startDate && w.入出庫日 <= endDate);

                        #endregion

                        // 基本倉庫・商品データ
                        var baseList = (
                            itemList.ToList()
                                .Select(s => new BaseItemMember
                                    {
                                        倉庫コード = s.SOUK.倉庫コード,
                                        品番コード = s.HIN.品番コード,
                                        賞味期限 = AppCommon.GetMaxDate()
                                    })
                                .Union(stokMonthList.ToList()
                                    .Select(s => new BaseItemMember
                                        {
                                            倉庫コード = s.倉庫コード,
                                            品番コード = s.品番コード,
                                            賞味期限 = s.賞味期限
                                        }))
                                .Union(historyList.ToList()
                                    .Select(s => new BaseItemMember
                                        {
                                            倉庫コード = s.倉庫コード,
                                            品番コード = s.品番コード,
                                            賞味期限 = s.賞味期限 ?? AppCommon.GetMaxDate()
                                        }))
                                )
                                //.Distinct() // Remarks:Distinctが効かないのでGroupByで対応
                                .GroupBy(g => new { g.倉庫コード, g.品番コード, g.賞味期限 })
                                .Select(s => s.Key)
                                .OrderBy(o => o.倉庫コード)
                                .ThenBy(t => t.品番コード)
                                .ThenBy(t => t.賞味期限);

                        #region 入出庫データのデータを集計

                        // -- D.入庫データ集計
                        var receipt =
                            historyList
                                .Where(w => w.削除日時 == null && w.入出庫区分 == (int)CommonConstants.入出庫区分.ID01_入庫)
                                .GroupBy(g => new { g.倉庫コード, g.品番コード, g.賞味期限 })
                                .ToList()
                                .Select(s => new HistorySummaryMember
                                {
                                    倉庫コード = s.Key.倉庫コード,
                                    品番コード = s.Key.品番コード,
                                    賞味期限 = s.Key.賞味期限 ?? AppCommon.GetMaxDate(),
                                    数量 = s.Sum(m => m.数量 ?? 0)
                                });

                        // -- E.出庫データ集計
                        var issue =
                            historyList
                                .Where(w => w.削除日時 == null && w.入出庫区分 == (int)CommonConstants.入出庫区分.ID02_出庫)
                                .GroupBy(g => new { g.倉庫コード, g.品番コード, g.賞味期限 })
                                .ToList()
                                .Select(s => new HistorySummaryMember
                                {
                                    倉庫コード = s.Key.倉庫コード,
                                    品番コード = s.Key.品番コード,
                                    賞味期限 = s.Key.賞味期限 ?? AppCommon.GetMaxDate(),
                                    数量 = s.Sum(m => m.数量 ?? 0)
                                });

                        #endregion
                        // 上記までのデータを使用して在庫数を計算取得
                        var result =
                            baseList
                                .GroupJoin(stokMonthList,
                                    x => new { x.倉庫コード, x.品番コード, x.賞味期限 },
                                    y => new { y.倉庫コード, y.品番コード, y.賞味期限 },
                                    (x, y) => new { x, y })
                                .SelectMany(z => z.y.DefaultIfEmpty(),
                                    (a, b) => new { BASE = a.x, STK = b })
                                .GroupJoin(receipt,
                                    x => new { x.BASE.倉庫コード, x.BASE.品番コード, x.BASE.賞味期限 },
                                    y => new { y.倉庫コード, y.品番コード, y.賞味期限 },
                                    (x, y) => new { x, y })
                                .SelectMany(z => z.y.DefaultIfEmpty(),
                                    (c, d) => new { c.x.BASE, c.x.STK, NYK = d })
                                .GroupJoin(issue,
                                    x => new { x.BASE.倉庫コード, x.BASE.品番コード, x.BASE.賞味期限 },
                                    y => new { y.倉庫コード, y.品番コード, y.賞味期限 },
                                    (x, y) => new { x, y })
                                .SelectMany(z => z.y.DefaultIfEmpty(),
                                    (e, f) => new { e.x.BASE, e.x.STK, e.x.NYK, SYK = f })
                                .Select(s => new S05_STOK_MONTH
                                {
                                    締年月 = iYearMonth,
                                    倉庫コード = s.BASE.倉庫コード,
                                    品番コード = s.BASE.品番コード,
                                    賞味期限 = s.BASE.賞味期限,
                                    在庫数量 =
                                        (s.STK != null ? s.STK.在庫数量 : 0) +
                                        (s.NYK != null ? s.NYK.数量 : 0) -
                                        (s.SYK != null ? s.SYK.数量 : 0),
                                    登録者 = userId,
                                    登録日時 = DateTime.Now
                                });

                        //▼Add Start Arinobu 2019/03/19　主キー違反がおこるため
                        result = result
                                  .GroupBy(g => new { g.締年月, g.倉庫コード, g.品番コード, g.賞味期限 })
                                  .Select(s => new S05_STOK_MONTH
                                  {
                                      締年月 = s.Key.締年月,
                                      倉庫コード = s.Key.倉庫コード,
                                      品番コード = s.Key.品番コード,
                                      賞味期限 = s.Key.賞味期限,
                                      在庫数量 = s.Max(m => m.在庫数量),
                                      登録者 = userId,
                                      登録日時 = DateTime.Now
                                  });
                        //▲Add End Arinobu 2019/03/19

                        // 集計年月の作成済締データを一括削除
                        var delObject = context.ExecuteStoreQuery<object>(string.Format("DELETE FROM S05_STOK_MONTH WHERE 締年月 = {0}", iYearMonth));

                        // 抽出データを登録
                        result.Where(w => w.在庫数量 != 0).ToList().ForEach(S05 => context.S05_STOK_MONTH.ApplyChanges(S05));

                        // 変更内容を確定
                        context.SaveChanges();

                        // トランザクションのコミット
                        tran.Commit();

                    }
                    catch (Exception ex)
                    {
                        // トランザクションのロールバック
                        tran.Rollback();
                        throw ex;
                    }

                }

            }

        }
        #endregion

        #region 帳票出力データ取得
        /// <summary>
        /// 帳票出力データを取得する
        /// </summary>
        /// <param name="yearMonth"></param>
        /// <returns></returns>
        public List<PrintDataMember> GetPrintData(string yearMonth)
        {
            int iYearMonth = int.Parse(yearMonth.Replace("/", ""));

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                var result =
                    context.S05_STOK_MONTH.Where(w => w.締年月 == iYearMonth)
                        .GroupJoin(context.M22_SOUK,
                            x => x.倉庫コード,
                            y => y.倉庫コード,
                            (x, y) => new { x, y })
                        .SelectMany(z => z.y.DefaultIfEmpty(),
                            (a, b) => new { STOK = a.x, SOUK = b })
                        .GroupJoin(context.M09_HIN,
                            x => x.STOK.品番コード,
                            y => y.品番コード,
                            (x, y) => new { x, y })
                        .SelectMany(z => z.y.DefaultIfEmpty(),
                            (c, d) => new { c.x.STOK, c.x.SOUK, HIN = d })
                        .ToList()
                        .Select(s => new PrintDataMember
                        {
                            品番コード = s.STOK.品番コード,
                            倉庫コード = s.STOK.倉庫コード,
                            賞味期限 = (s.STOK.賞味期限 != AppCommon.GetMaxDate() ? s.STOK.賞味期限 : (DateTime?)null),
                            自社品番 = s.HIN.自社品番,
                            自社品名 = s.HIN.自社品名,
                            倉庫名 = s.SOUK.倉庫名,
                            在庫数量 = s.STOK.在庫数量
                        })
                        .OrderBy(o => o.品番コード)
                        .ThenBy(t => t.倉庫コード)
                        .ThenBy(t => t.賞味期限);

                return result.ToList();

            }

        }
        #endregion

    }

}