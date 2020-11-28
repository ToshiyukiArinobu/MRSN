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
    public class ZIK02010 : BaseService
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
            public string ブランド { get; set; }
            [DataMember]
            public string シリーズ { get; set; }
            [DataMember]
            public string 表示用賞味期限 { get; set; }
            [DataMember]
            public decimal? 数量 { get; set; }
            [DataMember]
            public string 単位 { get; set; }
            [DataMember]
            public int? 商品分類 { get; set; }
            [DataMember]
            public bool 新規データFLG { get; set; }
            [DataMember]
            public bool 行追加FLG { get; set; }
        }

        /// <summary>
        /// 倉庫マスタ 表示項目定義クラス
        /// </summary>
        public class M22_SOUK_Member
        {
            [DataMember]
            public int 倉庫コード { get; set; }
            [DataMember]
            public string 倉庫名 { get; set; }
            [DataMember]
            public string 略称名 { get; set; }
            [DataMember]
            public string かな読み { get; set; }
            [DataMember]
            public string 場所会社コード { get; set; }
            [DataMember]
            public string 寄託会社コード { get; set; }
            [DataMember]
            public int? 登録担当者 { get; set; }
            [DataMember]
            public int? 更新担当者 { get; set; }
            [DataMember]
            public int? 削除担当者 { get; set; }
            [DataMember]
            public DateTime? 登録日時 { get; set; }
            [DataMember]
            public DateTime? 更新日時 { get; set; }
            [DataMember]
            public DateTime? 削除日時 { get; set; }
            [DataMember]
            public int 場所会社自社区分 { get; set; }
        }

        #endregion

        #region << 列挙型定義 >>

        private enum AddEditFlg : int
        {
            新規 = 0,
            編集 = 1,
        }

        #endregion

        #region << サービス定義 >>
        #endregion

        #region 起動時削除処理

        /// <summary>
        /// 棚卸しデータ削除処理
        /// </summary>
        /// <param name="context">TRAC3Entities</param>
        /// <param name="pRow">StocktakingDataMember</param>
        public void IsInitialDelProcess()
        {

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // ---------------------------
                // 起動時削除情報取得
                // ---------------------------
                // 棚卸在庫情報(品番マスタ,倉庫マスタ,在庫テーブル)

                DateTime sysDate = DateTime.Now.Date;
                DateTime tagetMonth = sysDate.AddMonths(-6);

                List<StocktakingDataMember> retResult = new List<StocktakingDataMember>();

                var delList =
                     context.S10_STOCKTAKING.Where(w => w.削除日時 == null && w.棚卸日 < tagetMonth && w.更新済みFLG == 0)
                    .AsQueryable();

                foreach (var row in delList)
                {
                    context.S10_STOCKTAKING.DeleteObject(row);
                }

                context.SaveChanges();
            }
        }

        #endregion

        #region 棚卸更新実行済確認
        /// <summary>
        /// 対象年月日の棚卸更新の実行済み確認、棚卸入力確認を行う
        /// </summary>
        /// <param name="pMyCompany">会社コード</param>
        /// <param name="pStocktakingDate">棚卸日</param>
        /// <param name="pParamDic">パラメータ辞書</param>
        /// <returns></returns>
        public bool IsCheckRegistered(int pMyCompany, string pStocktakingDate, Dictionary<string, string> pParamDic)
        {
            DateTime dteStocktaking = DateTime.Parse(pStocktakingDate);

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                //  在庫テーブルのデータが棚卸更新済みかチェック

                // ---------------------------
                // 情報取得
                // ---------------------------
                // 更新済棚卸在庫情報(品番マスタ,倉庫マスタ)

                var 更新済棚卸在庫List = GetStockTakingList(context, pMyCompany, pStocktakingDate, pParamDic, 1);

                var 在庫List = GetStockList(context, pMyCompany, pStocktakingDate, pParamDic);
                var stocktakingList = 更新済棚卸在庫List.Union(在庫List, new ParameterComparer());

                #region 入力項目による絞込

                // 自社品番の条件チェック
                string myProduct = pParamDic["自社品番"];
                if (string.IsNullOrEmpty(myProduct) == false)
                {
                    stocktakingList = stocktakingList.Where(w => w.自社品番 == myProduct);
                }

                // 品名の条件チェック
                string productName = pParamDic["自社品名"];
                if (string.IsNullOrEmpty(productName) == false)
                {
                    stocktakingList = stocktakingList.Where(w => w.自社品名 != null && w.自社品名.Contains(productName));
                }

                // 商品分類の条件チェック
                int itemType;
                if (int.TryParse(pParamDic["商品分類"], out itemType) == true)
                {
                    if (itemType >= CommonConstants.商品分類.食品.GetHashCode())
                    {
                        stocktakingList = stocktakingList.Where(w => w.商品分類 == itemType);
                    }
                }

                // ブランドの条件チェック
                string brand = pParamDic["ブランドコード"];
                if (string.IsNullOrEmpty(brand) == false)
                {
                    stocktakingList = stocktakingList.Where(w => w.ブランド == brand);
                }

                // シリーズの条件チェック
                string series = pParamDic["シリーズコード"];
                if (string.IsNullOrEmpty(series) == false)
                {
                    stocktakingList = stocktakingList.Where(w => w.シリーズ == series);
                }

                #endregion

                // ---------------------------
                // チェック処理
                // ---------------------------

                bool retResult = stocktakingList.Count() > 0 && stocktakingList.All(c => c.更新済みFLG == 1);

                return retResult;
            }

        }
        #endregion

        #region 棚卸未更新データの前回棚卸日を取得
        /// <summary>
        /// 棚卸未更新データの前回棚卸日を取得
        /// </summary>
        /// <param name="pMyCompany">会社コード</param>
        /// <param name="pStocktakingDate">棚卸日</param>
        /// <param name="pParamDic">パラメータ辞書</param>
        /// <returns></returns>
        public DateTime? GetStocktakingDate(int pMyCompany, string pStocktakingDate, Dictionary<string, string> pParamDic)
        {
            //DateTime dteStocktaking = DateTime.Parse(pStocktakingDate);       // No352 del

            DateTime? retDt = null;

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // ---------------------------
                // 情報取得
                // ---------------------------
                // 棚卸在庫情報(品番マスタ,倉庫マスタ)


                var stocktakingList =
                    context.S10_STOCKTAKING.Where(w => w.削除日時 == null && w.更新済みFLG == 0)
                        .Join(context.M09_HIN.Where(w => w.削除日時 == null),
                            x => x.品番コード,
                            y => y.品番コード,
                            (x, y) => new { STOCKTAKING = x, HIN = y })
                        .Join(context.M22_SOUK.Where(w => w.削除日時 == null
                                                    && w.場所会社コード == pMyCompany),
                            x => x.STOCKTAKING.倉庫コード,
                            y => y.倉庫コード,
                            (x, y) => new { x.STOCKTAKING, x.HIN, SOUK = y })
                        .AsQueryable();

                #region 入力項目による絞込

                // 倉庫の条件チェック
                string Warehouse = pParamDic["倉庫コード"];
                if (string.IsNullOrEmpty(Warehouse) == false)
                {
                    int iWarehouse = int.Parse(Warehouse);
                    stocktakingList = stocktakingList.Where(w => w.STOCKTAKING.倉庫コード == iWarehouse);
                }

                #endregion

                var 直近在庫 = stocktakingList.OrderByDescending(w => w.STOCKTAKING.棚卸日).FirstOrDefault();

                // ---------------------------
                // 件数取得・チェック処理
                // ---------------------------

                // 戻り値設定
                if (直近在庫 != null)
                {
                    retDt = 直近在庫.STOCKTAKING.棚卸日;
                }

                return retDt;
            }

        }
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
        public List<StocktakingDataMember> GetStockTaking(int pMyCompany, string pStocktakingDate, Dictionary<string, string> pParamDic)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {

                // 倉庫毎のデータを取得

                List<StocktakingDataMember> retList = new List<StocktakingDataMember>();


                DateTime dteStocktaking = DateTime.Parse(pStocktakingDate);

                // 在庫データ取得
                var 在庫List = GetStockList(context, pMyCompany, pStocktakingDate, pParamDic);

                var 棚卸在庫List = GetStockTakingList(context, pMyCompany, pStocktakingDate, pParamDic, 0);

                // 編集中のデータが存在するかチェック
                if (棚卸在庫List.Count == 0)
                {
                    // 新規
                    retList = 在庫List.OrderBy(c => c.品番追加FLG).ThenBy(c => c.倉庫コード).ThenBy(c => c.品番コード).ThenBy(c => c.賞味期限).ToList();
                }
                else
                {
                    // 編集   
                    // 棚卸在庫テーブルと不足分の在庫を結合
                    retList = 棚卸在庫List.Union(在庫List, new ParameterComparer())
                        .OrderBy(c => c.品番追加FLG).ThenBy(c => c.倉庫コード).ThenBy(c => c.品番コード).ThenBy(c => c.賞味期限).ToList();
                }

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
        /// <param name="p更新済みFLG">更新済みFLG（0:未更新、1:更新済み）</param>
        private List<StocktakingDataMember> GetStockTakingList(TRAC3Entities context, int pMyCompany, string pStocktakingDate, Dictionary<string, string> pParamDic, int p更新済みFLG)
        {
            DateTime dtStocktaking = DateTime.Parse(pStocktakingDate);

            // ---------------------------
            // 情報取得
            // ---------------------------
            // 棚卸在庫情報(品番マスタ,倉庫マスタ,在庫テーブル)

            List<StocktakingDataMember> retResult = new List<StocktakingDataMember>();

            var stocktakingList =
                 context.S10_STOCKTAKING.Where(w => w.削除日時 == null && w.棚卸日 == dtStocktaking && w.更新済みFLG == p更新済みFLG)
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
                                   シリーズ = s.SERIES == null ? null : s.SERIES.シリーズコード,
                                   ブランド = s.BRAND == null ? null : s.BRAND.ブランドコード,
                                   自社色 = s.IRO == null ? null : s.IRO.色名称,
                                   賞味期限 = s.STOCKTAKING.賞味期限,
                                   数量 = s.STOK == null ? 0 : s.STOK.在庫数,
                                   単位 = s.HIN.単位,
                                   実在庫数 = s.STOCKTAKING.実在庫数,
                                   品番追加FLG = s.STOCKTAKING.品番追加FLG,
                                   更新済みFLG = s.STOCKTAKING.更新済みFLG,
                                   商品分類 = s.HIN.商品分類,
                                   新規データFLG = false,
                                   行追加FLG = false,
                               })
                                .ToList();

            DateTime maxDate = AppCommon.GetMaxDate();

            foreach (var row in retResult)
            {
                row.表示用賞味期限 = maxDate == row.賞味期限 ? "" : row.賞味期限.ToString("yyyy/MM/dd");
            }

            return retResult;

        }

        #endregion

        #region 在庫データ取得

        /// <summary>
        /// 在庫データ取得
        /// </summary>
        /// <param name="context">TRAC3Entities</param>
        /// <param name="pMyCompany">自社コード</param>
        /// <param name="pStocktakingDate">棚卸日</param>
        /// <param name="pParamDic">パラメータ辞書</param>
        public List<StocktakingDataMember> GetStockList(TRAC3Entities context, int pMyCompany, string pStocktakingDate, Dictionary<string, string> pParamDic)
        {
            DateTime dteStocktaking = DateTime.Parse(pStocktakingDate);

            // ---------------------------
            // 情報取得
            // ---------------------------
            // 棚卸在庫情報(品番マスタ,倉庫マスタ,在庫テーブル)

            List<StocktakingDataMember> retResult = new List<StocktakingDataMember>();

            var stockList =
                context.S03_STOK.Where(w => w.削除日時 == null)
                .Join(context.M22_SOUK.Where(w => w.削除日時 == null && w.場所会社コード == pMyCompany), x => x.倉庫コード, y => y.倉庫コード, (x, y) => new { STOK = x, SOUK = y })
                .Join(context.M09_HIN.Where(w => w.削除日時 == null), x => x.STOK.品番コード, y => y.品番コード, (x, y) => new { x.STOK, x.SOUK, HIN = y })
                .GroupJoin(context.S10_STOCKTAKING.Where(w => w.棚卸日 == dteStocktaking && w.更新済みFLG == 1 && w.削除日時 == null),
                        x => new { 倉庫コード = x.STOK.倉庫コード, 品番コード = x.STOK.品番コード, 賞味期限 = x.STOK.賞味期限 },
                        y => new { 倉庫コード = y.倉庫コード, 品番コード = y.品番コード, 賞味期限 = y.賞味期限 },
                        (x, y) => new { x, y })
                .SelectMany(x => x.y.DefaultIfEmpty(), (a, b) => new { a.x.STOK, a.x.SOUK, a.x.HIN, STOCKTAKING = b })
                .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null), x => x.HIN.自社色, y => y.色コード, (x, y) => new { x, y })
                .SelectMany(x => x.y.DefaultIfEmpty(), (e, f) => new { e.x.STOK, e.x.SOUK, e.x.HIN, e.x.STOCKTAKING, IRO = f })
                .GroupJoin(context.M14_BRAND.Where(w => w.削除日時 == null), x => x.HIN.ブランド, y => y.ブランドコード, (x, y) => new { x, y })
                .SelectMany(x => x.y.DefaultIfEmpty(), (g, h) => new { g.x.STOK, g.x.SOUK, g.x.HIN, g.x.STOCKTAKING, g.x.IRO, BRAND = h })
                .GroupJoin(context.M15_SERIES.Where(w => w.削除日時 == null), x => x.HIN.シリーズ, y => y.シリーズコード, (x, y) => new { x, y })
                .SelectMany(x => x.y.DefaultIfEmpty(), (i, j) => new { i.x.STOK, i.x.SOUK, i.x.HIN, i.x.STOCKTAKING, i.x.IRO, i.x.BRAND, SERIES = j })
                .Where(c => c.STOCKTAKING == null)
                .AsQueryable();


            #region 入力項目による絞込

            // 倉庫の条件チェック
            string Warehouse = pParamDic["倉庫コード"];
            if (string.IsNullOrEmpty(Warehouse) == false)
            {
                int iWarehouse = int.Parse(Warehouse);
                stockList = stockList.Where(w => w.STOK.倉庫コード == iWarehouse);
            }

            #endregion

            // ---------------------------
            // 出力形式に成型
            // ---------------------------
            retResult = stockList
                               .Select(s => new StocktakingDataMember
                               {
                                   棚卸日 = dteStocktaking,
                                   倉庫コード = s.STOK.倉庫コード,
                                   倉庫名 = s.SOUK.倉庫略称名,
                                   品番コード = s.STOK.品番コード,
                                   自社品番 = s.HIN.自社品番,
                                   自社品名 = s.HIN.自社品名,
                                   シリーズ = s.SERIES == null ? null : s.SERIES.シリーズコード,
                                   ブランド = s.BRAND == null ? null : s.BRAND.ブランドコード,
                                   自社色 = s.IRO == null ? null : s.IRO.色名称,
                                   賞味期限 = s.STOK.賞味期限,

                                   数量 = s.STOK == null ? 0 : s.STOK.在庫数,
                                   単位 = s.HIN.単位,
                                   実在庫数 = 0,
                                   品番追加FLG = 0,
                                   更新済みFLG = 0,
                                   商品分類 = s.HIN.商品分類,
                                   新規データFLG = true,
                                   行追加FLG = false,
                               })
                                .ToList();

            DateTime maxDate = AppCommon.GetMaxDate();

            foreach (var row in retResult)
            {
                row.表示用賞味期限 = maxDate == row.賞味期限 ? "" : row.賞味期限.ToString("yyyy/MM/dd");
            }

            return retResult;

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
        public void UpdateStocktaking(DataSet ds, int pMyCompany, string pStocktakingDate, Dictionary<string, string> pParamDic, int pUserId, bool 新規FLG)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // ---------------------------
                // 初期処理
                // ---------------------------
                context.Connection.Open();

                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    try
                    {
                        DataTable tbl = ds.Tables[0];

                        // 登録用リスト
                        List<S10_STOCKTAKING> 倉庫別棚卸在庫List = new List<S10_STOCKTAKING>();

                        // 倉庫単位で更新処理をする
                        Dictionary<string, string> unitDic = new Dictionary<string, string>();
                        unitDic.Add("倉庫コード", pParamDic["倉庫コード"]);
                        unitDic.Add("自社品番", string.Empty);
                        unitDic.Add("自社品名", string.Empty);
                        unitDic.Add("商品分類", string.Empty);
                        unitDic.Add("ブランドコード", string.Empty);
                        unitDic.Add("シリーズコード", string.Empty);


                        // ---------------------------
                        // データ設定
                        // ---------------------------                    

                        foreach (DataRow row in tbl.Rows)
                        {
                            if (row.RowState != DataRowState.Deleted)
                            {
                                倉庫別棚卸在庫List.Add(ConvertS10_STOCKTAKING_Entity(row, pUserId, com.GetDbDateTime()));
                            }
                        }

                        // ---------------------------
                        // 既存データの削除削除処理
                        // ---------------------------

                        DateTime dt = DateTime.Parse(pStocktakingDate);
                        Delete_S10_STOCKTAKING(context, pMyCompany, dt, unitDic);

                        // ---------------------------
                        // 更新処理
                        // ---------------------------

                        foreach (var row in 倉庫別棚卸在庫List)
                        {
                            context.S10_STOCKTAKING.ApplyChanges(row);
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

        #region 削除処理

        /// <summary>
        /// 棚卸しデータ削除処理
        /// </summary>
        /// <param name="context">TRAC3Entities</param>
        /// <param name="pRow">StocktakingDataMember</param>
        private void Delete_S10_STOCKTAKING(TRAC3Entities context, int pMyCompany, DateTime pStocktakingDate, Dictionary<string, string> pParamDic)
        {

            // ---------------------------
            // 削除情報取得
            // ---------------------------
            // 棚卸在庫情報(品番マスタ,倉庫マスタ,在庫テーブル)

            List<StocktakingDataMember> retResult = new List<StocktakingDataMember>();

            var delList =
                context.S10_STOCKTAKING.Where(w => w.削除日時 == null && w.更新済みFLG == 0)
                .Join(context.M22_SOUK.Where(w => w.削除日時 == null && w.場所会社コード == pMyCompany),
                        x => x.倉庫コード,
                        y => y.倉庫コード,
                        (a, b) => new { STOCKTAKING = a, SOUK = b })
                .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null), x => x.STOCKTAKING.品番コード, y => y.品番コード, (x, y) => new { x, y })
                .SelectMany(x => x.y.DefaultIfEmpty(), (c, d) => new { c.x.STOCKTAKING, c.x.SOUK, HIN = d })
                .AsQueryable();


            #region 入力項目による絞込

            // 倉庫の条件チェック
            string Warehouse = pParamDic["倉庫コード"];
            if (string.IsNullOrEmpty(Warehouse) == false)
            {
                int iWarehouse = int.Parse(Warehouse);
                delList = delList.Where(w => w.STOCKTAKING.倉庫コード == iWarehouse);
            }

            #endregion


            foreach (var row in delList)
            {
                context.S10_STOCKTAKING.DeleteObject(row.STOCKTAKING);
            }


            context.SaveChanges();
        }

        #endregion

        #endregion

        #region 追加行情報取得
        /// <summary>
        /// 追加行情報取得
        /// </summary>
        /// <param name="pMyCompany">会社コード</param>
        /// <param name="pStocktakingDate">棚卸日</param>
        /// <param name="pParamDic">パラメータ辞書</param>
        /// <returns></returns>
        public string CheckAddRowData(string pStocktakingDate, int? SoukCd, int? HinCd, string s賞味期限, int? 会社コード, int? 指定倉庫)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // 棚卸在庫テーブルにデータが存在するかチェック

                DateTime dteStocktaking = DateTime.Parse(pStocktakingDate);

                DateTime dt賞味期限 = string.IsNullOrEmpty(s賞味期限) ? AppCommon.DateTimeToDate(null, DateTime.MaxValue) : DateTime.Parse(s賞味期限);

                string retMsg = string.Empty;

                // 棚卸在庫データ取得
                var stocktakingExist =
                context.S10_STOCKTAKING.Where(w => w.削除日時 == null && w.棚卸日 == dteStocktaking
                    && w.倉庫コード == SoukCd && w.品番コード == HinCd && w.賞味期限 == dt賞味期限);

                // 更新対象倉庫
                var ret =
                  context.M22_SOUK
                      .Where(x =>
                          x.倉庫コード == (指定倉庫 == null ? x.倉庫コード : 指定倉庫) && x.場所会社コード == 会社コード &&
                           x.削除日時 == null
                          ).Select(c => c.倉庫コード).ToArray();

                if (stocktakingExist.Any(c => c.更新済みFLG == 1))
                {
                    retMsg = "棚卸更新済みです。追加できません。";
                }
                else if (stocktakingExist.Any(c => c.更新済みFLG == 0 && !ret.Contains(c.倉庫コード)))
                {
                    retMsg = "入力済みです。追加できません。";
                }

                return retMsg;
            }
        }
        #endregion

        #region マスタ参照

        /// <summary>
        /// 倉庫情報取得
        /// </summary>
        /// <param name="stokData"></param>
        public List<M22_SOUK_Member> GetSOUK(int? 倉庫コード, int 会社コード)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret =
                    context.M22_SOUK
                        .Where(x =>
                            x.倉庫コード == 倉庫コード && x.場所会社コード == 会社コード &&
                             x.削除日時 == null
                            )
                        .Select(row => new M22_SOUK_Member
                        {
                            倉庫コード = row.倉庫コード,
                            倉庫名 = row.倉庫名,
                            略称名 = row.倉庫略称名,
                            かな読み = row.かな読み,
                        }).ToList();

                return ret;
            }

        }

        /// <summary>
        /// 品番コードで品番情報を取得する
        /// </summary>
        /// <returns></returns>
        public List<UcMST.M10_TOKHIN_Extension> GetHIN(string productCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int iProductCode = -1;
                if (!int.TryParse(productCode, out iProductCode))
                    return new List<UcMST.M10_TOKHIN_Extension>();

                var result =
                    context.M09_HIN.Where(w => w.削除日時 == null && w.品番コード == iProductCode)
                    .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null),
                        x => x.自社色, y => y.色コード, (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (a, b) => new { HIN = a.x, IRO = b })
                    .Select(t => new UcMST.M10_TOKHIN_Extension
                    {
                        品番コード = t.HIN.品番コード,
                        自社品番 = t.HIN.自社品番,
                        自社色 = t.HIN.自社色,
                        色名称 = t.IRO.色名称,
                        商品形態分類 = t.HIN.商品形態分類,
                        商品分類 = t.HIN.商品分類,
                        自社品名 = t.HIN.自社品名,
                        単位 = t.HIN.単位,
                        論理削除 = t.HIN.論理削除,
                        削除日時 = t.HIN.削除日時,
                        削除者 = t.HIN.削除者,
                        登録日時 = t.HIN.登録日時,
                        登録者 = t.HIN.登録者,
                        最終更新日時 = t.HIN.最終更新日時,
                        最終更新者 = t.HIN.最終更新者,
                        備考１ = t.HIN.備考１,
                        備考２ = t.HIN.備考２,
                        返却可能期限 = t.HIN.返却可能期限,
                        ＪＡＮコード = t.HIN.ＪＡＮコード
                    });

                return result.ToList();

            }

        }

        #endregion

        #region << 処理関連 >>

        #region 登録データ作成
        /// <summary>
        /// S10_STOCKTAKINGに変換する
        /// </summary>
        /// <param name="drow"></param>
        /// <returns></returns>
        protected S10_STOCKTAKING ConvertS10_STOCKTAKING_Entity(DataRow drow, int pUserId, DateTime pUpdateDate)
        {
            S10_STOCKTAKING retRow = new S10_STOCKTAKING();

            retRow.棚卸日 = (DateTime)DateParse(drow["棚卸日"]);
            retRow.倉庫コード = ParseNumeric<int>(drow["倉庫コード"]);
            retRow.品番コード = ParseNumeric<int>(drow["品番コード"]);
            retRow.賞味期限 = AppCommon.DateTimeToDate(DateParse(drow["賞味期限"]), DateTime.MaxValue);
            retRow.実在庫数 = ParseNumeric<decimal>(drow["実在庫数"]);
            retRow.品番追加FLG = ParseNumeric<int>(drow["品番追加FLG"]);
            retRow.更新済みFLG = 0;
            retRow.登録者 = pUserId;
            retRow.登録日時 = pUpdateDate;
            retRow.最終更新者 = pUserId;
            retRow.最終更新日時 = pUpdateDate;

            return retRow;
        }

        #endregion

        #endregion

        #region << UNIONの結合条件設定 >>

        private class ParameterComparer : IEqualityComparer<StocktakingDataMember>
        {
            public bool Equals(StocktakingDataMember i_lhs, StocktakingDataMember i_rhs)
            {
                if (i_lhs.倉庫コード == i_rhs.倉庫コード &&
                    i_lhs.品番コード == i_rhs.品番コード &&
                    i_lhs.賞味期限 == i_rhs.賞味期限)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(StocktakingDataMember i_obj)
            {
                return i_obj.倉庫コード ^ i_obj.品番コード ^ i_obj.賞味期限.GetHashCode();
            }
        }

        #endregion
    }
}