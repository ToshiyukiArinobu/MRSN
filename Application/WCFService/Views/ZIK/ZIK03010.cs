using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 棚卸更新サービスクラス
    /// </summary>
    public class ZIK03010 : BaseService
    {
        #region << 項目定義メンバクラス >>

        /// <summary>
        /// 棚卸更新データ
        /// </summary>
        public class StocktakingDataMember : S10_STOCKTAKING
        {
            [DataMember]
            public string 倉庫名 { get; set; }
            [DataMember]
            public string 自社品番 { get; set; }
            [DataMember]
            public string 自社品名 { get; set; }
            [DataMember]
            public string 自社色 { get; set; }
            [DataMember]
            public string 表示用賞味期限 { get; set; }
            [DataMember]
            public decimal? 数量 { get; set; }
            [DataMember]
            public string 単位 { get; set; }
            [DataMember]
            public decimal? 差異数量 { get; set; }

        }

        #endregion

        #region << サービス定義 >>
        #endregion

        #region 場所別棚卸在庫取得処理
        /// <summary>
        /// 場所別棚卸在庫取得処理
        /// </summary>
        /// <param name="pMyCompany">会社コード</param>
        /// <param name="pStocktakingDate">棚卸日</param>
        /// <param name="pParamDic">パラメータ辞書</param>
        /// <param name="option">0：新規、１：編集</param>
        /// <returns></returns>
        public List<StocktakingDataMember> GetDifStocktaking(int pMyCompany, string pStocktakingDate, Dictionary<string, string> pParamDic)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {

                // データを取得
                List<StocktakingDataMember> retList = new List<StocktakingDataMember>();

                retList = GetStockTakingList(context, pMyCompany, pStocktakingDate, pParamDic);

                return retList;
            }
        }
        #endregion

        #region 棚卸在庫データ取得

        /// <summary>
        /// 棚卸　対象情報取得
        /// </summary>
        /// <param name="context">TRAC3Entities</param>
        /// <param name="pMyCompany">自社コード</param>
        /// <param name="pStocktakingDate">棚卸日</param>
        /// <param name="pParamDic">パラメータ辞書</param>
        private List<StocktakingDataMember> GetStockTakingList(TRAC3Entities context, int pMyCompany, string pStocktakingDate, Dictionary<string, string> pParamDic)
        {
            DateTime dtStocktaking = DateTime.Parse(pStocktakingDate);

            // ---------------------------
            // 情報取得
            // ---------------------------
            // 棚卸在庫情報(品番マスタ,倉庫マスタ,在庫テーブル)

            List<StocktakingDataMember> retResult = new List<StocktakingDataMember>();

            var stocktakingList =
                 context.S10_STOCKTAKING.Where(w => w.削除日時 == null && w.棚卸日 == dtStocktaking && w.更新済みFLG == 0)
                    .Join(context.M09_HIN.Where(w => w.削除日時 == null),
                        x => x.品番コード,
                        y => y.品番コード,
                        (x, y) => new { STOCKTAKING = x, HIN = y })
                    .Join(context.M22_SOUK.Where(w => w.削除日時 == null && w.場所会社コード == pMyCompany),
                        x => x.STOCKTAKING.倉庫コード,
                        y => y.倉庫コード,
                        (x, y) => new { x.STOCKTAKING, x.HIN, SOUK = y })
                    .GroupJoin(context.S03_STOK.Where(w => w.削除日時 == null),
                        x => new { 倉庫コード = x.STOCKTAKING.倉庫コード, 品番コード = x.STOCKTAKING.品番コード, 賞味期限 = x.STOCKTAKING.賞味期限 },
                        y => new { 倉庫コード = y.倉庫コード, 品番コード = y.品番コード, 賞味期限 = y.賞味期限 },
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(), (a, b) => new { STOCKTAKING = a.x.STOCKTAKING, HIN = a.x.HIN, SOUK = a.x.SOUK, STOK = b })
                .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null), x => x.HIN.自社色, y => y.色コード, (x, y) => new { x, y })
                .SelectMany(x => x.y.DefaultIfEmpty(), (g, h) => new { g.x.STOCKTAKING, g.x.HIN, g.x.SOUK, g.x.STOK, IRO = h })
                .GroupJoin(context.M14_BRAND.Where(w => w.削除日時 == null), x => x.HIN.ブランド, y => y.ブランドコード, (x, y) => new { x, y })
                .SelectMany(x => x.y.DefaultIfEmpty(), (i, j) => new { i.x.STOCKTAKING, i.x.HIN, i.x.SOUK, i.x.STOK, i.x.IRO, BRAND = j })
                .GroupJoin(context.M15_SERIES.Where(w => w.削除日時 == null), x => x.HIN.シリーズ, y => y.シリーズコード, (x, y) => new { x, y })
                .SelectMany(x => x.y.DefaultIfEmpty(), (k, l) => new { k.x.STOCKTAKING, k.x.HIN, k.x.SOUK, k.x.STOK, k.x.IRO, k.x.BRAND, SERIES = l })
                .AsQueryable();


            #region 入力項目による絞込


            // 倉庫の条件チェック
            string Warehouse = pParamDic["倉庫コード"];

            if (string.IsNullOrEmpty(Warehouse) == false)
            {
                int iWarehouse = int.Parse(Warehouse);
                stocktakingList = stocktakingList.Where(w => w.STOCKTAKING.倉庫コード == iWarehouse);
            }

            // 自社品番の条件チェック
            string myProduct = pParamDic["自社品番"];
            if (string.IsNullOrEmpty(myProduct) == false)
            {
                stocktakingList = stocktakingList.Where(w => w.HIN.自社品番 == myProduct);
            }

            // 品名の条件チェック
            string productName = pParamDic["自社品名"];
            if (string.IsNullOrEmpty(productName) == false)
            {
                stocktakingList = stocktakingList.Where(w => w.HIN.自社品名 != null && w.HIN.自社品名.Contains(productName));
            }

            // 商品分類の条件チェック
            int itemType;
            if (int.TryParse(pParamDic["商品分類コード"], out itemType) == true)
            {
                if (itemType >= CommonConstants.商品分類.食品.GetHashCode())
                {
                    stocktakingList = stocktakingList.Where(w => w.HIN.商品分類 == itemType);
                }
            }

            // ブランドの条件チェック
            string brand = pParamDic["ブランドコード"];
            if (string.IsNullOrEmpty(brand) == false)
            {
                stocktakingList = stocktakingList.Where(w => w.HIN.ブランド == brand);
            }

            // シリーズの条件チェック
            string series = pParamDic["シリーズコード"];
            if (string.IsNullOrEmpty(series) == false)
            {
                stocktakingList = stocktakingList.Where(w => w.HIN.シリーズ == series);
            }

            #endregion


            // ---------------------------
            // 出力形式に成型
            // ---------------------------
            retResult = stocktakingList
                               .Select(s => new StocktakingDataMember
                               {
                                   棚卸日 = s.STOCKTAKING.棚卸日,
                                   倉庫コード = s.STOCKTAKING.倉庫コード,
                                   倉庫名 = s.SOUK.倉庫略称名,
                                   品番コード = s.STOCKTAKING.品番コード,
                                   自社品番 = s.HIN.自社品番,
                                   自社品名 = s.HIN.自社品名,
                                   自社色 = s.IRO == null ? null : s.IRO.色名称,
                                   賞味期限 = s.STOCKTAKING.賞味期限,
                                   数量 = s.STOK == null ? 0 : s.STOK.在庫数,
                                   単位 = s.HIN.単位,
                                   実在庫数 = s.STOCKTAKING.実在庫数,
                                   品番追加FLG = s.STOCKTAKING.品番追加FLG,
                                   更新済みFLG = s.STOCKTAKING.更新済みFLG,
                               })
                                .ToList();

            DateTime maxDate = AppCommon.GetMaxDate();

            foreach (var row in retResult)
            {
                // 表示用賞味期限を設定
                row.表示用賞味期限 = maxDate == row.賞味期限 ? "" : row.賞味期限.ToString("yyyy/MM/dd");

                // 差異数量を設定
                row.差異数量 = row.実在庫数 - row.数量;
            }

            return retResult;

        }

        #endregion

    }
}