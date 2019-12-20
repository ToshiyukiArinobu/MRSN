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
        /// <returns></returns>
        public List<SearchDataMember> GetPrintList(int pMyCompany, string pDate, Dictionary<string, string> pParamDic)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try
                {
                    int yearMonth = int.Parse(pDate.Replace("/", ""));

                    // 在庫基本情報
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
                    string Warehouse = pParamDic["倉庫"];
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
                    if (int.TryParse(pParamDic["商品分類"], out itemType) == true)
                    {
                        if (itemType >= CommonConstants.商品分類.食品.GetHashCode())
                        {
                            stocktakingList = stocktakingList.Where(w => w.HIN.商品分類 == itemType).ToList();
                        }
                    }

                    // ブランドの条件チェック
                    string brand = pParamDic["ブランド"];
                    if (string.IsNullOrEmpty(brand) == false)
                    {
                        stocktakingList = stocktakingList.Where(w => w.HIN.ブランド == brand).ToList();
                    }

                    // シリーズの条件チェック
                    string series = pParamDic["シリーズ"];
                    if (string.IsNullOrEmpty(series) == false)
                    {
                        stocktakingList = stocktakingList.Where(w => w.HIN.シリーズ == series).ToList();
                    }

                    #endregion

                    // TODO:直近の仕入単価(最少額)を取得
                    // 直近の仕入単価(最少額)を取得



 
                    // 検索データ取得
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
                                (e, f) => new { e.x.STOK_MONTH, e.x.HIN, e.x.SOUK, e.x.IRO, JIS = f })
                            .Select(x => new SearchDataMember
                            {
                                自社コード = x.JIS.自社コード,
                                自社名 = x.JIS.自社名 ?? "",
                                倉庫コード = x.STOK_MONTH.倉庫コード.ToString(),
                                倉庫名称 = x.SOUK != null ? x.SOUK.倉庫名 : "",
                                品番コード = x.STOK_MONTH.品番コード.ToString(),
                                自社品番コード = x.HIN.自社品番,
                                自社色コード = x.HIN.自社色,
                                品番名称 = x.HIN.自社品名,
                                色名称 = x.IRO != null ? x.IRO.色名称 : "",
                                賞味期限 = AppCommon.GetMaxDate() == x.STOK_MONTH.賞味期限 ? "" : x.STOK_MONTH.賞味期限.ToString("yyyy/MM/dd"),
                                数量 = x.STOK_MONTH.在庫数量,
                                単位 = x.HIN.単位,
                                単価 = 1,
                                金額　= 100

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