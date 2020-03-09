using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 商品在庫残高一覧表サービスクラス
    /// </summary>
    public class ZIK05010
    {
        #region 項目クラス定義

        /// <summary>
        /// ZIK05010 商品在庫残高一覧表 検索メンバー
        /// </summary>
        public class SearchDataMember
        {
            public int 自社コード { get; set; }
            public string 自社名 { get; set; }
            public string 倉庫コード { get; set; }
            public string 倉庫名称 { get; set; }
            public string 品番コード { get; set; }
            public string 自社品番コード { get; set; }
            public string 自社色コード { get; set; }
            public string 品番名称 { get; set; }
            public string 色名称 { get; set; }
            public string 賞味期限 { get; set; }
            public decimal 数量 { get; set; }
            public string 単位 { get; set; }
            public decimal 単価 { get; set; }
            public decimal 金額 { get; set; }
        }

        /// <summary>
        /// ZIK05010 直近仕入単価取得 検索メンバー
        /// </summary>
        public class SearchDataUnitPrice
        {
            public int 品番コード { get; set; }
            public DateTime 賞味期限 { get; set; }
            public decimal 仕入単価 { get; set; }
        }

        #endregion

        #region 商品在庫残高一覧表情報取得
        /// <summary>
        /// 商品在庫残高一覧表情報取得
        /// </summary>
        /// <param name="pMyCompany">画面指定の自社コード</param>
        /// <param name="pDate">締年月</param>
        /// <param name="pParamDic">
        ///  == 検索条件 ==
        ///  自社品番(品番指定検索)
        ///  検索品名(品名Like検索)
        ///  商品分類
        ///  シリーズ
        ///  ブランド
        /// </param>
        /// <param name="pCoefficient">係数</param>
        /// <returns></returns>
        public List<SearchDataMember> GetPrintList(int pMyCompany, string pDate, Dictionary<string, string> pParamDic, decimal pCoefficient)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try
                {
                    int yearMonth = int.Parse(pDate.Replace("/", ""));

                    // ===========================
                    // 在庫基本情報取得
                    // ===========================
                    // 月次在庫情報取得
                    var stocktakingList =
                        context.S05_STOK_MONTH.Where(w => w.締年月 == yearMonth)
                        .Join(context.M09_HIN.Where(w => w.削除日時 == null),
                            x => x.品番コード,
                            y => y.品番コード,
                            (x, y) => new { STOK_MONTH = x, HIN = y })
                        .Join(context.M22_SOUK.Where(w => w.削除日時 == null
                                                    && w.場所会社コード == pMyCompany),
                            x => x.STOK_MONTH.倉庫コード,
                            y => y.倉庫コード,
                            (x, y) => new { x.STOK_MONTH, x.HIN, SOUK = y })
                            .ToList();

                    #region 入力項目による絞込

                    // 倉庫の条件チェック
                    string Warehouse = pParamDic["倉庫コード"];
                    if (string.IsNullOrEmpty(Warehouse) == false)
                    {
                        stocktakingList = stocktakingList.Where(w => w.STOK_MONTH.倉庫コード == int.Parse(Warehouse)).ToList();
                    }

                    // 自社品番の条件チェック
                    string myProduct = pParamDic["自社品番"];
                    if (string.IsNullOrEmpty(myProduct) == false)
                    {
                        stocktakingList = stocktakingList.Where(w => w.HIN.自社品番 == myProduct).ToList();
                    }

                    // 品名の条件チェック
                    string productName = pParamDic["自社品名"];
                    if (string.IsNullOrEmpty(productName) == false)
                    {
                        stocktakingList = stocktakingList.Where(w => w.HIN.自社品名 != null && w.HIN.自社品名.Contains(productName)).ToList();
                    }

                    // 商品分類の条件チェック
                    int itemType;
                    if (int.TryParse(pParamDic["商品分類コード"], out itemType) == true)
                    {
                        if (itemType >= CommonConstants.商品分類.食品.GetHashCode())
                        {
                            stocktakingList = stocktakingList.Where(w => w.HIN.商品分類 == itemType).ToList();
                        }
                    }

                    // ブランドの条件チェック
                    string brand = pParamDic["ブランドコード"];
                    if (string.IsNullOrEmpty(brand) == false)
                    {
                        stocktakingList = stocktakingList.Where(w => w.HIN.ブランド == brand).ToList();
                    }

                    // シリーズの条件チェック
                    string series = pParamDic["シリーズコード"];
                    if (string.IsNullOrEmpty(series) == false)
                    {
                        stocktakingList = stocktakingList.Where(w => w.HIN.シリーズ == series).ToList();
                    }

                    #endregion

                    // ===========================
                    // 直近の仕入単価(最少額)を取得
                    // ===========================
                    // 年月末日の取得
                    DateTime dteEndofMonth = getDateEndofMonth(yearMonth);

                    // 品番毎の直近日付を取得する
                    var LatestList =
                         context.T03_SRHD.Where(w => w.削除日時 == null && w.仕入日 < dteEndofMonth)
                            .Join(context.T03_SRDTL.Where(w => w.削除日時 == null),
                                x => x.伝票番号,
                                y => y.伝票番号,
                                (x, y) => new { SHD = x, SDTL = y })
                        .GroupBy(g => new { g.SDTL.品番コード, g.SDTL.賞味期限})
                        .Select(s => new 
                        {
                            品番コード = s.Key.品番コード,
                            賞味期限 = s.Key.賞味期限,
                            仕入日 = s.Max(m => m.SHD.仕入日),
                        })
                        .OrderBy(o => o.品番コード)
                        .ToList();


                    // 直近の仕入単価(最少額)を取得
                    DateTime dteMaxDate = AppCommon.GetMaxDate();

                    var PurchaseList =
                        LatestList
                            .Join(context.T03_SRHD.Where(w => w.削除日時 == null),
                                x => x.仕入日,
                                y => y.仕入日,
                                (x, y) => new { Latest = x, SRHD = y })
                            .Join(context.T03_SRDTL.Where(w => w.削除日時 == null),
                                x => new { dno = x.SRHD.伝票番号, hin = x.Latest.品番コード, date = x.Latest.賞味期限},
                                y => new { dno = y.伝票番号, hin = y.品番コード, date = y.賞味期限},
                                (x, y) => new { x.Latest, x.SRHD, SDTL = y })
                        .GroupBy(g => new { g.SDTL.品番コード, g.SDTL.賞味期限})
                        .Select(s => new SearchDataUnitPrice
                        {
                            品番コード = s.Key.品番コード,
                            賞味期限 = s.Key.賞味期限 ?? dteMaxDate,
                            仕入単価 = s.Min(m => m.SDTL.単価),
                        })
                        .OrderBy(o => o.品番コード)
                        .ToList();

                    // ===========================
                    // 帳票データ取得
                    // ===========================
                    var dataList =
                        stocktakingList
                            .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null),
                                x => x.HIN.自社色,
                                y => y.色コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (a, b) => new { a.x.STOK_MONTH, a.x.HIN, a.x.SOUK, IRO = b })
                            .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                                x => x.SOUK.寄託会社コード,
                                y => y.自社コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (c, d) => new { c.x.STOK_MONTH, c.x.HIN, c.x.SOUK, c.x.IRO, JIS = d })
                            .GroupJoin(PurchaseList.Where(w => w.品番コード > 0),
                                x => new { dno = x.STOK_MONTH.品番コード, date = x.STOK_MONTH.賞味期限},
                                y => new { dno = y.品番コード, date = y.賞味期限},
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (e, f) => new { e.x.STOK_MONTH, e.x.HIN, e.x.SOUK, e.x.IRO, e.x.JIS , Purchase = f })
                            .Select(x => new SearchDataMember
                            {
                                自社コード = x.JIS.自社コード,
                                自社名 = x.JIS.自社名 ?? "",
                                倉庫コード = x.STOK_MONTH.倉庫コード.ToString(),
                                倉庫名称 = x.SOUK != null ? x.SOUK.倉庫略称名 : "",
                                品番コード = x.STOK_MONTH.品番コード.ToString(),
                                自社品番コード = x.HIN.自社品番,
                                自社色コード = x.HIN.自社色,
                                品番名称 = x.HIN.自社品名,
                                色名称 = x.IRO != null ? x.IRO.色名称 : "",
                                賞味期限 = AppCommon.GetMaxDate() == x.STOK_MONTH.賞味期限 ? "" : x.STOK_MONTH.賞味期限.ToString("yyyy/MM/dd"),
                                数量 = x.STOK_MONTH.在庫数量,
                                単位 = x.HIN.単位,
                                単価 = (x.Purchase != null ? x.Purchase.仕入単価 : x.HIN.原価 ?? 0) * pCoefficient,
                                金額 = x.STOK_MONTH.在庫数量 * (x.Purchase != null ? x.Purchase.仕入単価 : x.HIN.原価 ?? 0) * pCoefficient
                            })
                            .OrderBy(o => o.自社コード)
                            .ThenBy(t => t.倉庫コード)
                            .ThenBy(t => t.品番コード)
                            .ThenBy(t => t.自社色コード)
                            .ThenBy(t => t.賞味期限)
                            ;

                    return dataList.ToList();

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
        #endregion

        #region 関数
        /// <summary>
        /// 年月の末日を取得する
        /// </summary>
        /// <param name="作成年月"></param>
        /// <param name="targetRow"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public DateTime getDateEndofMonth(int pYearMonth)
        {
            int intYear = pYearMonth / 100;
            int intMonth = pYearMonth % 100;

            var theDay = new DateTime(intYear, intMonth, 01);

            DateTime lastDayOfMonth = (new DateTime(theDay.Year, theDay.Month, theDay.Day)) 
                                  .AddMonths(1) 
                                  .AddDays(-1.0);
            return lastDayOfMonth;

        }
        #endregion

    }

}