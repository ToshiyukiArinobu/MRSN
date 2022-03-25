using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 商品在庫問い合わせサービスクラス
    /// </summary>
    public class ZIJ09010
    {
        #region 項目クラス定義

        /// <summary>
        /// ZIJ09010 商品在庫問い合わせ 検索メンバー
        /// </summary>
        public class SearchDataMember
        {
            public int 自社コード { get; set; }        // No.227,228 Add
            public string 自社名 { get; set; }         // No.227,228 Add
            public string 倉庫コード { get; set; }
            public string 倉庫名称 { get; set; }
            public string 品番コード { get; set; }
            public string 自社品番コード { get; set; }
            public string 自社色コード { get; set; }
            public string 品番名称 { get; set; }
            public string 色名称 { get; set; }
            public string 賞味期限 { get; set; }
            public decimal 在庫数量 { get; set; }
            public string 単位 { get; set; }
        }

        #endregion

        #region 商品在庫問い合わせ検索情報取得
        /// <summary>
        /// 商品在庫問い合わせ検索情報取得
        /// </summary>
        /// <param name="p自社コード">画面指定の自社コード</param>
        /// <param name="p自社販社区分">(ログインユーザの)自社販社区分</param>
        /// <param name="cond">
        ///  == 検索条件 ==
        ///  自社品番(品番指定検索)
        ///  検索品名(品名Like検索)
        ///  商品分類
        ///  シリーズ
        ///  ブランド
        /// </param>
        /// <returns></returns>
        public List<SearchDataMember> GetDataList(int p自社コード, int p自社販社区分, Dictionary<string, string> cond)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try
                {
                    // 在庫基本情報
                    var stockList =
                        context.S03_STOK.Where(w => w.削除日時 == null && w.在庫数 != 0)
                            .Join(context.M09_HIN.Where(w => w.削除日時 == null),
                                x => x.品番コード,
                                y => y.品番コード,
                                (x, y) => new { STOK = x, HIN = y })
                            .ToList();

                    #region 入力項目による絞込

                    // 自社品番の条件チェック
                    string myProduct = cond["自社品番"];
                    if (!string.IsNullOrEmpty(myProduct))
                        stockList = stockList.Where(w => w.HIN.自社品番 == myProduct).ToList();

                    // 品名の条件チェック
                    string productName = cond["検索品名"];
                    if (!string.IsNullOrEmpty(productName))
                        stockList = stockList.Where(w => w.HIN.自社品名 != null && w.HIN.自社品名.Contains(productName)).ToList();

                    // 商品分類の条件チェック
                    int itemType;
                    if (int.TryParse(cond["商品分類"], out itemType))
                    {
                        if (itemType >= CommonConstants.商品分類.食品.GetHashCode())
                            stockList = stockList.Where(w => w.HIN.商品分類 == itemType).ToList();
                    }

                    // ブランドの条件チェック
                    string brand = cond["ブランド"];
                    if (!string.IsNullOrEmpty(brand))
                        stockList = stockList.Where(w => w.HIN.ブランド == brand).ToList();

                    // シリーズの条件チェック
                    string series = cond["シリーズ"];
                    if (!string.IsNullOrEmpty(series))
                        stockList = stockList.Where(w => w.HIN.シリーズ == series).ToList();

                    #endregion

                    var jis =
                        context.M70_JIS
                            .Where(w => w.削除日時 == null && w.自社コード == p自社コード)
                            .FirstOrDefault();

                    // 販社の場合は、販社倉庫、マルセン倉庫(xxx確保)の在庫を抽出
                    // 自社の場合は、すべての倉庫の在庫を抽出
                    if (jis != null && jis.自社区分 == CommonConstants.自社区分.販社.GetHashCode())
                    {
                        // No.270 Mod Start
                        var soukList = context.M22_SOUK.Where(v =>
                                            v.削除日時 == null &&
                                            (v.寄託会社コード == p自社コード))
                                        .Select(s => s.倉庫コード).ToList();
                        stockList =
                            stockList.Where(w => soukList.Contains(w.STOK.倉庫コード)).ToList();
                    }

                    // 検索データ取得
                    var dataList =
                        stockList
                            .GroupJoin(context.M22_SOUK.Where(w => w.削除日時 == null),
                                x => x.STOK.倉庫コード,
                                y => y.倉庫コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (a, b) => new { a.x.STOK, a.x.HIN, SOUK = b })
                            .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null),
                                x => x.HIN.自社色,
                                y => y.色コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (c, d) => new { c.x.STOK, c.x.HIN, c.x.SOUK, IRO = d })
                            .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                                x => x.SOUK.寄託会社コード,
                                y => y.自社コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (e, f) => new {e.x.STOK, e.x.HIN, e.x.SOUK, e.x.IRO, JIS = f})
                            .Select(x => new SearchDataMember
                            {
                                自社コード = x.JIS.自社コード,                  // No.227,228 Add
                                自社名 = x.JIS.自社名 ?? "",                    // No.227,228 Add
                                倉庫コード = x.STOK.倉庫コード.ToString(),
                                倉庫名称 = x.SOUK != null ? x.SOUK.倉庫名 : "",
                                品番コード = x.STOK.品番コード.ToString(),
                                自社品番コード = x.HIN.自社品番,
                                自社色コード = x.HIN.自社色,
                                品番名称 = x.HIN.自社品名,
                                色名称 = x.IRO != null ? x.IRO.色名称 : "",
                                賞味期限 = AppCommon.GetMaxDate() == x.STOK.賞味期限 ? "" : x.STOK.賞味期限.ToString("yyyy/MM/dd"),
                                在庫数量 = x.STOK.在庫数,
                                単位 = x.HIN.単位
                            });

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

    }

}