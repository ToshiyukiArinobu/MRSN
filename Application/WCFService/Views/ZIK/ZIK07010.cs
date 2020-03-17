using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 適正在庫入力サービスクラス
    /// </summary>
    public class ZIK07010
    {
        #region 項目クラス定義

        /// <summary>
        /// ZIK07010 適正在庫入力 検索メンバー
        /// </summary>
        public class SearchDataMember
        {
            public int 自社コード { get; set; }
            public string 自社名 { get; set; }
            public int 倉庫コード { get; set; }
            public string 倉庫名称 { get; set; }
            public int 品番コード { get; set; }
            public string 自社品番コード { get; set; }
            public string 自社色コード { get; set; }
            public string 自社品名 { get; set; }
            public string 色名称 { get; set; }
            public decimal 適正数量 { get; set; }
            public decimal 最低数量 { get; set; }
            public string 単位1 { get; set; }
            public string 単位2 { get; set; }
        }

        /// <summary>
        /// 在庫TBL 基本情報(賞味期限集約)
        /// </summary>
        public class BaseStockMember
        {
            public int 倉庫コード { get; set; }
            public int 品番コード { get; set; }
            public decimal 在庫数量 { get; set; }
        }

        #endregion

        #region 適正在庫入力検索情報取得
        /// <summary>
        /// 適正在庫入力検索情報取得
        /// </summary>
        /// <param name="p自社コード">画面指定の自社コード</param>
        /// <param name="p倉庫コード">画面指定の倉庫コード</param>
        /// <param name="cond">
        ///  == 検索条件 ==
        ///  自社品番(品番指定検索)
        ///  検索品名(品名Like検索)
        ///  商品分類
        ///  シリーズ
        ///  ブランド
        /// </param>
        /// <returns></returns>
        public List<SearchDataMember> GetDataList(string p自社コード, string p倉庫コード, Dictionary<string, string> cond)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try
                {
                    int val = -1;

                    // 在庫基本情報
                    var stockList =
                        context.S03_STOK.Where(w => w.削除日時 == null)
                            .GroupBy(g => new { g.倉庫コード, g.品番コード })
                            .Select(s => new BaseStockMember
                            {
                                倉庫コード = s.Key.倉庫コード,
                                品番コード = s.Key.品番コード,
                                在庫数量 = s.Sum(m => m.在庫数)
                            })
                            .Join(context.M09_HIN.Where(w => w.削除日時 == null),
                                x => x.品番コード,
                                y => y.品番コード,
                                (x, y) => new { STOK = x, HIN = y })
                            .ToList();

                    // 雑コードを除く商品
                    stockList = stockList.Where(w => w.HIN.商品形態分類 != (int)CommonConstants.商品形態分類.雑コード).ToList();

                    // DEB
                    //stockList = stockList.Where(w => w.STOK.品番コード == 1).ToList();

                    #region 入力項目による絞込
                    // 自社コードの条件チェック
                    if (!string.IsNullOrEmpty(p自社コード))
                    {
                        int n自社コード = Int32.TryParse(p自社コード, out val) ? val : -1;
                        var jis =
                            context.M70_JIS
                                .Where(w => w.削除日時 == null && w.自社コード == n自社コード)
                                .FirstOrDefault();

                        // 自社倉庫、マルセン倉庫(xxx確保)の在庫を抽出
                        if (jis != null)
                        {
                            var soukList = context.M22_SOUK.Where(v =>
                                                v.削除日時 == null &&
                                                (v.寄託会社コード == n自社コード) ||
                                                (v.場所会社コード == n自社コード))
                                            .Select(s => s.倉庫コード).ToList();
                            stockList =
                                stockList.Where(w => soukList.Contains(w.STOK.倉庫コード)).ToList();
                        }
                    }

                    // 倉庫コードの条件チェック
                    if (!string.IsNullOrEmpty(p倉庫コード))
                    {
                        int n倉庫コード = Int32.TryParse(p倉庫コード, out val) ? val : -1;
                        stockList = stockList.Where(w => w.STOK.倉庫コード == n倉庫コード).ToList();
                    }

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

                    // 検索データ取得
                    var dataList =
                        stockList
                            .GroupJoin(context.S03_STOK_JUST,
                                x => new { soukCd = x.STOK.倉庫コード, hinCd = x.STOK.品番コード },
                                y => new { soukCd = y.倉庫コード, hinCd = y.品番コード },
                                (x, y) => new { x, y })
                            .SelectMany(z => z.y.DefaultIfEmpty(),
                                (a, b) => new { STOK = a.x.STOK, HIN = a.x.HIN, JUST = b })
                            .GroupJoin(context.M22_SOUK.Where(w => w.削除日時 == null),
                                x => x.STOK.倉庫コード,
                                y => y.倉庫コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (c, d) => new { c.x.STOK, c.x.HIN, c.x.JUST, SOUK = d })
                            .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null),
                                x => x.HIN.自社色,
                                y => y.色コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (e, f) => new { e.x.STOK, e.x.HIN, e.x.JUST, e.x.SOUK, IRO = f })
                            .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                                x => x.SOUK.寄託会社コード,
                                y => y.自社コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (g, h) => new { g.x.STOK, g.x.HIN, g.x.JUST, g.x.SOUK, g.x.IRO, JIS = h })
                            .Select(s => new SearchDataMember
                            {
                                //自社コード = s.JIS.自社コード,
                                //自社名 = s.JIS.自社名 ?? "",
                                倉庫コード = s.STOK.倉庫コード,
                                倉庫名称 = s.SOUK != null ? s.SOUK.倉庫名 : "",
                                品番コード = s.STOK.品番コード,
                                自社品番コード = s.HIN.自社品番,
                                自社色コード = s.HIN.自社色,
                                自社品名 = s.HIN.自社品名,
                                色名称 = s.IRO != null ? s.IRO.色名称 : "",
                                適正数量 = s.JUST != null ? s.JUST.適正在庫数量 : 0,
                                最低数量 = s.JUST != null ? s.JUST.最低在庫数量 : 0,
                                単位1 = s.HIN.単位,
                                単位2 = s.HIN.単位
                            });

                    return dataList.OrderBy(o => o.品番コード).ThenBy(o => o.倉庫コード).ToList();

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

        #region 適正在庫更新
        /// <summary>
        /// 適正在庫更新
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int Update(DataSet ds, int userId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    try
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            if (row.RowState == DataRowState.Modified)
                            {
                                S03_STOK_JUST s03 = new S03_STOK_JUST();
                                s03.倉庫コード = row.Field<int>("倉庫コード");
                                s03.品番コード = row.Field<int>("品番コード");
                                s03.適正在庫数量 = row.Field<decimal>("適正数量");
                                s03.最低在庫数量 = row.Field<decimal>("最低数量");
                                s03.登録者 = userId;
                                s03.登録日時 = DateTime.Now;

                                S03_STOK_JUST_Update(context, s03);
                            }
                        }

                        // 変更状態を確定
                        context.SaveChanges();

                        tran.Commit();

                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }

            }
            return 1;
        }
        #endregion

        #region 適正在庫TBL(S03_STOK_JUST)更新
        /// <summary>
        /// 適正在庫TBL(S03_STOK_JUST)更新
        /// </summary>
        /// <param name="context"></param>
        /// <param name="s03data"></param>
        public void S03_STOK_JUST_Update(TRAC3Entities context, S03_STOK_JUST s03data)
        {
            // 登録済データが存在するか判定
            var sjData =
                context.S03_STOK_JUST
                    .Where(w => w.倉庫コード == s03data.倉庫コード && w.品番コード == s03data.品番コード)
                    .FirstOrDefault();

            // 新規登録
            if (sjData == null)
            {
                context.S03_STOK_JUST.ApplyChanges(s03data);
            }
            // 更新
            else
            {
                context.S03_STOK_JUST.DeleteObject(sjData);
                context.S03_STOK_JUST.ApplyChanges(s03data);
            }
        }
        #endregion

    }

}