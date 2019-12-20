using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 棚卸更新サービスクラス
    /// </summary>
    public class ZIK04010 : BaseService
    {
        #region << 項目定義メンバクラス >>

        /// <summary>
        /// 棚卸更新データ
        /// </summary>
        public class StocktakingDataMember　: S10_STOCKTAKING 
        {
            public decimal 在庫数 { get; set; }
        }
        #endregion

        #region << サービス定義 >>

        /// <summary>在庫情報サービス</summary>
        S03 S03Service;
        /// <summary>入出庫履歴サービス</summary>
        S04 S04Service;
        /// <summary>棚卸在庫サービス</summary>
        S10 S10Service;

        #endregion

        #region 棚卸更新実行済確認
        /// <summary>
        /// 対象年月日の棚卸更新の実行済み確認、棚卸入力確認を行う
        /// </summary>
        /// <param name="pMyCompany">会社コード</param>
        /// <param name="pStocktakingDate">棚卸日</param>
        /// <param name="pParamDic">パラメータ辞書</param>
        /// <returns>0:対象あり、-1:棚卸未入力、1:棚卸更新済み</returns>
        public int IsCheckStocktaking(int pMyCompany, string pStocktakingDate, Dictionary<string, string> pParamDic)
        {
            DateTime dteStocktaking = DateTime.Parse(pStocktakingDate);
            int intResult = 0;                      // 0:対象あり

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // ---------------------------
                // 情報取得
                // ---------------------------
                // 棚卸在庫情報(品番マスタ,倉庫マスタ)
                var stocktakingList =
                    context.S10_STOCKTAKING.Where(w => w.削除日時 == null
                                                && w.棚卸日 == dteStocktaking)
                        .Join(context.M09_HIN.Where(w => w.削除日時 == null),
                            x => x.品番コード,
                            y => y.品番コード,
                            (x, y) => new { STOCKTAKING = x, HIN = y })
                        .Join(context.M22_SOUK.Where(w => w.削除日時 == null
                                                    && w.場所会社コード == pMyCompany),
                            x => x.STOCKTAKING.倉庫コード,
                            y => y.倉庫コード,
                            (x, y) => new { x.STOCKTAKING, x.HIN, SOUK = y })
                        .ToList();

                #region 入力項目による絞込

                // 倉庫の条件チェック
                string Warehouse = pParamDic["倉庫"];
                if (string.IsNullOrEmpty(Warehouse) == false)
                {
                    stocktakingList = stocktakingList.Where(w => w.STOCKTAKING.倉庫コード == int.Parse(Warehouse)).ToList();
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

                // ---------------------------
                // 件数取得・チェック処理
                // ---------------------------
                int intCnt = stocktakingList.Count();                                                       // 全件
                int intCompleteCnt = stocktakingList.Where(w => w.STOCKTAKING.更新済みFLG == 1).Count();    // 棚卸済み件数

                // 戻り値設定
                if (intCompleteCnt > 0 && (intCnt - intCompleteCnt) == 0)
                {
                    // 全件が棚卸更新済みの場合
                    intResult = 1;                  // 1:棚卸更新済み
                }
                else if (intCompleteCnt == 0 && intCnt == 0)
                {
                    // 棚卸入力済みが存在しない場合
                    intResult = -1;                 // -1:棚卸未入力
                }

                return intResult;
            }

        }
        #endregion

        #region 棚卸更新処理
        /// <summary>
        /// 棚卸更新処理
        /// </summary>
        /// <param name="pMyCompany">会社コード</param>
        /// <param name="pStocktakingDate">棚卸日</param>
        /// <param name="pParamDic">パラメータ辞書</param>
        /// <param name="pUserId">ログインユーザID</param>
        public void InventoryStocktaking(int pMyCompany, string pStocktakingDate, Dictionary<string, string> pParamDic, int pUserId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // ---------------------------
                // 初期処理
                // ---------------------------
                context.Connection.Open();

                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    S03Service = new S03(context, pUserId);
                    S04Service = new S04(context, pUserId, S04.機能ID.棚卸更新);
                    S10Service = new S10(context, pUserId);

                    try
                    {
                        // ---------------------------
                        // 主処理
                        // ---------------------------
                        // 棚卸更新　対象情報取得
                        List<StocktakingDataMember> lstResult = getData(context, pMyCompany, pStocktakingDate, pParamDic);

                        foreach (StocktakingDataMember row in lstResult)
                        {

                            // 処理対象チェック
                            bool bolRetTarget = CheckTargetRecord(context, row);
                            if (bolRetTarget == false)
                            {
                                continue;
                            }

                            // 入出庫履歴テーブル更新チェック
                            bool bolRetUpd = CheckS04_HISTORYUpdate(context, row);

                            if (bolRetUpd == true)
                            {
                                // 入出庫履歴テーブル　更新
                                Update_S04_HISTORY(context, row);
                            }

                            // 在庫テーブル　更新
                            Update_S03_STOK(context, row);

                            // 棚卸在庫テーブル　更新
                            Update_S10_STOCKTAKING(context, row);

                        }

                        // ---------------------------
                        // 終了処理
                        // ---------------------------
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

        /// <summary>
        /// 棚卸更新　対象情報取得
        /// </summary>
        /// <param name="context">TRAC3Entities</param>
        /// <param name="pMyCompany">自社コード</param>
        /// <param name="pStocktakingDate">棚卸日</param>
        /// <param name="pParamDic">パラメータ辞書</param>
        private List<StocktakingDataMember> getData(TRAC3Entities context, int pMyCompany, string pStocktakingDate, Dictionary<string, string> pParamDic)
        {
            DateTime dteStocktaking = DateTime.Parse(pStocktakingDate);

            // ---------------------------
            // 情報取得
            // ---------------------------
            // 棚卸在庫情報(品番マスタ,倉庫マスタ,在庫テーブル)
            var stocktakingList =
                    context.S10_STOCKTAKING.Where(w => w.削除日時 == null
                                                && w.棚卸日 == dteStocktaking
                                                && w.更新済みFLG == 0)
                    .Join(context.M09_HIN.Where(w => w.削除日時 == null),
                        x => x.品番コード,
                        y => y.品番コード,
                        (x, y) => new { STOCKTAKING = x, HIN = y })
                    .Join(context.M22_SOUK.Where(w => w.削除日時 == null
                                                && w.場所会社コード == pMyCompany),
                            x => x.STOCKTAKING.倉庫コード,
                            y => y.倉庫コード,
                        (x, y) => new { x.STOCKTAKING, x.HIN, SOUK = y })
                    .GroupJoin(context.S03_STOK.Where(w => w.削除日時 == null),
                            x => new { 倉庫コード = x.STOCKTAKING.倉庫コード, 品番コード = x.STOCKTAKING.品番コード, 賞味期限 = x.STOCKTAKING.賞味期限 },
                            y => new { 倉庫コード = y.倉庫コード, 品番コード = y.品番コード,  賞味期限 = y.賞味期限},
                            (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(),
                            (a, b) => new { STOCKTAKING = a.x.STOCKTAKING, HIN = a.x.HIN, SOUK = a.x.SOUK, STOK = b })
                    .ToList();

            #region 入力項目による絞込

            // 倉庫の条件チェック
            string Warehouse = pParamDic["倉庫"];
            if (string.IsNullOrEmpty(Warehouse) == false)
            {
                stocktakingList = stocktakingList.Where(w => w.STOCKTAKING.倉庫コード == int.Parse(Warehouse)).ToList();
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

            // ---------------------------
            // 出力形式に成型
            // ---------------------------
            var varResult = stocktakingList
                               .Select(s => new StocktakingDataMember
                               {
                                   棚卸日 = s.STOCKTAKING.棚卸日,
                                   倉庫コード = s.STOCKTAKING.倉庫コード,
                                   品番コード = s.STOCKTAKING.品番コード,
                                   賞味期限 = s.STOCKTAKING.賞味期限,
                                   実在庫数 = s.STOCKTAKING.実在庫数,
                                   品番追加FLG = s.STOCKTAKING.品番追加FLG,
                                   更新済みFLG = s.STOCKTAKING.更新済みFLG,
                                   在庫数 = s.STOK == null ? 0 : s.STOK.在庫数
                               })
                                .ToList();

            return varResult;
        }

        /// <summary>
        /// 処理対象チェック
        /// </summary>
        /// <param name="context">TRAC3Entities</param>
        /// <param name="pRow">StocktakingDataMember</param>
        private bool CheckTargetRecord(TRAC3Entities context, StocktakingDataMember pRow)
        {
            bool bolResult = true;

            // 処理対象チェック
            if ((pRow.在庫数 == null || pRow.在庫数 == 0) && pRow.実在庫数 == 0)
            {
                bolResult = false;
            }

            return bolResult;
        }

        /// <summary>
        /// 入出庫履歴テーブル更新チェック
        /// </summary>
        /// <param name="context">TRAC3Entities</param>
        /// <param name="pRow">StocktakingDataMember</param>
        private bool CheckS04_HISTORYUpdate(TRAC3Entities context, StocktakingDataMember pRow)
        {
            bool bolResult = true;

            // 在庫数と実在庫数のチェック
            if (pRow.在庫数 == pRow.実在庫数)
            {
                bolResult = false;
            }

            return bolResult;
        }

        /// <summary>
        /// 入出庫履歴テーブル　更新
        /// </summary>
        /// <param name="context">TRAC3Entities</param>
        /// <param name="pRow">StocktakingDataMember</param>
        private void Update_S04_HISTORY(TRAC3Entities context, StocktakingDataMember pRow)
        {

            // 入出庫履歴テーブル　編集
            decimal dcmStockQtyhist = 0;
            dcmStockQtyhist = pRow.実在庫数 - pRow.在庫数;
            int intInOutKbn = 0;

            if (dcmStockQtyhist > 0)
            {
                intInOutKbn = (int)CommonConstants.入出庫区分.ID01_入庫;
            }
            else
            {
                intInOutKbn = (int)CommonConstants.入出庫区分.ID02_出庫;
            }

            S04_HISTORY history = new S04_HISTORY();

            history.入出庫日 = pRow.棚卸日;
            history.入出庫時刻 = com.GetDbDateTime().TimeOfDay;
            history.倉庫コード = pRow.倉庫コード;
            history.入出庫区分 = intInOutKbn;
            history.品番コード = pRow.品番コード;
            history.賞味期限 = pRow.賞味期限;
            history.数量 = decimal.ToInt32(dcmStockQtyhist);
            history.伝票番号 = null;

            Dictionary<string, string> hstDic = new Dictionary<string, string>()
                {
                    { S04.COLUMNS_NAME_入出庫日, history.入出庫日.ToString("yyyy/MM/dd") },
                    { S04.COLUMNS_NAME_倉庫コード, history.倉庫コード.ToString() },
                    { S04.COLUMNS_NAME_伝票番号, history.伝票番号.ToString() },
                    { S04.COLUMNS_NAME_品番コード, history.品番コード.ToString() },
                };

            // ---------------------------
            // 入出庫履歴テーブル　登録
            // ---------------------------
            S04Service.CreateProductHistory(history);
        }

        /// <summary>
        /// 在庫テーブル　更新
        /// </summary>
        /// <param name="context">TRAC3Entities</param>
        /// <param name="pRow">StocktakingDataMember</param>
        private void Update_S03_STOK(TRAC3Entities context, StocktakingDataMember pRow)
        {
            // 在庫テーブル　編集
            S03_STOK stok = new S03_STOK();
            stok.倉庫コード = pRow.倉庫コード;
            stok.品番コード = pRow.品番コード;
            stok.賞味期限 = AppCommon.DateTimeToDate(pRow.賞味期限, DateTime.MaxValue);
            stok.在庫数 = pRow.実在庫数;

            // ---------------------------
            // 在庫テーブル　更新
            // ---------------------------
            S03Service.S03_STOK_Update(stok);
        
        }

        /// <summary>
        /// 棚卸在庫テーブル　更新
        /// </summary>
        /// <param name="context">TRAC3Entities</param>
        /// <param name="pRow">StocktakingDataMember</param>
        private void Update_S10_STOCKTAKING(TRAC3Entities context, StocktakingDataMember pRow)
        {
            // 棚卸在庫テーブル　編集
            S10_STOCKTAKING stocktaking = new S10_STOCKTAKING();
            stocktaking.棚卸日 = pRow.棚卸日;
            stocktaking.倉庫コード = pRow.倉庫コード;
            stocktaking.品番コード = pRow.品番コード;
            stocktaking.賞味期限 = pRow.賞味期限;
            stocktaking.実在庫数 = pRow.実在庫数;
            stocktaking.品番追加FLG = pRow.品番追加FLG;
            stocktaking.更新済みFLG = 1;

            // ---------------------------
            // 棚卸在庫テーブル　更新
            // ---------------------------
            S10Service.S10_STOCKTAKING_Update(stocktaking);
        }

        #endregion
    }

}