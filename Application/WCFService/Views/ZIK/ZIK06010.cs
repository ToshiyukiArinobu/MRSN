using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 在庫評価額一覧表サービスクラス
    /// </summary>
    public class ZIK06010
    {
        #region 定数定義
        /// <summary>
        /// 賞味期限初期値
        /// </summary>
        DateTime DEFAULT_YYYYMMDD = new DateTime(9999, 12, 31);

        public enum 対象在庫 :int
        {
            月次在庫 = 0,
            調整在庫 = 1
        }

        public enum 商品形態分類 : int
        {
            SET品 = 1
        }

        #endregion

        #region 項目クラス定義

        #region 月次在庫Excel出力メンバー
        /// <summary>
        /// ZIK06010 月次在庫Excel出力メンバー
        /// </summary>
        public class SearchDataMember
        {
            public int 締年月 { get; set; }
            public int 品番コード { get; set; }
            public string 自社品番 { get; set; }
            public string 自社色 { get; set; }
            public string 自社品名 { get; set; }
            public string 色名称 { get; set; }
            public DateTime? 賞味期限 { get; set; }
            public int 倉庫コード { get; set; }
            public string 倉庫名 { get; set; }
            public int 在庫数量 { get; set; }
            public int? 調整数量 { get; set; }
        }
        #endregion

        #region Excel取込データチェックメンバー
        /// <summary>
        /// ZIK06010 Excel取込データチェックメンバー
        /// </summary>
        public class CheckErrMember
        {
            public int? 行番号 { get; set; }
            public int 品番コード { get; set; }
            public string 自社品番 { get; set; }
            public string 自社色 { get; set; }
            public string 色名称 { get; set; }
            public int 倉庫コード { get; set; }
            public string 倉庫名 { get; set; }
            public string MST自社品番 { get; set; }
            public string MST自社色 { get; set; }
            public string MST色名称 { get; set; }
            public int? MST倉庫コード { get; set; }
            public string MST倉庫名 { get; set; }
        }
        #endregion

        #region ZIK06010 Excel取込データ
        /// <summary>
        /// ZIK06010 Excel取込データ
        /// </summary>
        public class InputDataMember
        {
            public int 場所会社コード { get; set; }
            public int 締年月 { get; set; }
            public int 品番コード { get; set; }
            public DateTime 賞味期限 { get; set; }
            public int 倉庫コード { get; set; }
            public int 在庫数量 { get; set; }
        }
        #endregion

        #region 在庫評価額一覧表　帳票出力メンバー
        /// <summary>
        /// 在庫評価額一覧表　帳票出力メンバー
        /// </summary>
        public class PrintMenber
        {
            public int 自社コード { get; set; }
            public string 自社名 { get; set; }
            public int 倉庫コード { get; set; }
            public string 倉庫名 { get; set; }
            public int 品番コード { get; set; }
            public string 自社品番 { get; set; }
            public string 自社品名 { get; set; }
            public string 自社色コード { get; set; }
            public string 色名称 { get; set; }
            public string 賞味期限 { get; set; }
            public int 数量 { get; set; }
            public string 単位 { get; set; }
            public decimal 単価 { get; set; }
            public decimal 評価額 { get; set; }
        }
        #endregion

        #endregion

        #region 月次在庫データ取得
        /// <summary>
        /// 月次在庫データ取得
        /// </summary>
        /// <param name="pDateYM">処理年月</param>
        /// <param name="pCompany">会社コード</param>
        /// <param name="pSokoCd">倉庫コード</param>
        /// <returns></returns>
        public List<SearchDataMember> GetData(string pDateYM, string pCompany, string pSokoCd)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try
                {
                    int yearMonth = int.Parse(pDateYM.Replace("/", ""));
                    int val = -1;
                    int companyCd = Int32.TryParse(pCompany, out val) ? val : -1;
                    int sokoCd = Int32.TryParse(pSokoCd, out val)? val : -1;

                    // 月次在庫データ
                    var stockMon = context.S05_STOK_MONTH.Where(w => w.締年月 == yearMonth)
                                    .Join(context.M22_SOUK.Where(w => w.削除日時 == null),
                                        x => x.倉庫コード,
                                        y => y.倉庫コード,
                                        (x, y) => new { STOK = x, SOKO = y });

                    // 条件絞り込み
                    if (companyCd != -1)
                    {
                        stockMon = stockMon.Where(w => w.SOKO.場所会社コード == companyCd);
                    }

                    if (sokoCd != -1)
                    {
                        stockMon = stockMon.Where(w => w.STOK.倉庫コード == sokoCd);
                    }

                    var result = stockMon
                                    .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                                        x => x.STOK.品番コード,
                                        y => y.品番コード,
                                        (x, y) => new {x, y})
                                    .SelectMany(z => z.y.DefaultIfEmpty(),
                                        (a, b) => new {STOK = a.x.STOK, SOKO = a.x.SOKO, HIN = b})
                                    .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null),
                                        x => x.HIN.自社色,
                                        y => y.色コード,
                                        (x, y) => new {x, y})
                                    .SelectMany(z => z.y.DefaultIfEmpty(),
                                        (c, d) => new {STOK = c.x.STOK, SOKO = c.x.SOKO, HIN = c.x.HIN, IRO = d})
                                    .Select(s => new SearchDataMember
                                    {
                                        締年月 = s.STOK.締年月,
                                        品番コード = s.STOK.品番コード,
                                        自社品番 = s.HIN.自社品番,
                                        自社色 = s.HIN.自社色,
                                        色名称 = s.IRO.色名称,
                                        自社品名 = s.HIN.自社品名,
                                        賞味期限 = s.STOK.賞味期限 == DEFAULT_YYYYMMDD ? (DateTime?)null : s.STOK.賞味期限,
                                        倉庫コード = s.STOK.倉庫コード,
                                        倉庫名 = s.SOKO.倉庫名,
                                        在庫数量 = s.STOK.在庫数量,
                                        調整数量 = null
                                    });

                    result = result.OrderBy(o => o.品番コード)
                                   .ThenBy(o => o.倉庫コード)
                                   .ThenBy(o => o.賞味期限);

                    return result.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region データ存在チェック
        /// <summary>
        /// データ存在チェック
        /// </summary>
        /// <param name="pDt"></param>
        /// <returns>エラーデータ</returns>
        public List<CheckErrMember> CheckExist(DataSet pDs)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try
                {
                    DataTable dt = pDs.Tables[0];
                    var baseData = dt.AsEnumerable()
                                    .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                                        x => x.Field<int>("品番コード"),
                                        y => y.品番コード,
                                        (x, y) => new { x, y })
                                    .SelectMany(z => z.y.DefaultIfEmpty(),
                                        (a, b) => new { ORG = a.x, HIN = b })
                                    .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null),
                                        x => x.HIN.自社色,
                                        y => y.色コード,
                                        (x, y) => new { x, y })
                                    .SelectMany(z => z.y.DefaultIfEmpty(),
                                        (c, d) => new { ORG = c.x.ORG, HIN = c.x.HIN, IRO = d })
                                    .GroupJoin(context.M22_SOUK.Where(w => w.削除日時 == null),
                                        x => x.ORG.Field<int>("倉庫コード"),
                                        y => y.倉庫コード,
                                        (x, y) => new { x, y })
                                    .SelectMany(z => z.y.DefaultIfEmpty(),
                                        (e, f) => new { ORG = e.x.ORG, HIN = e.x.HIN, IRO = e.x.IRO, SOKO = f })
                                    .Select(s => new CheckErrMember
                                    {
                                        行番号 = s.ORG.Field<int>("行番号"),
                                        品番コード = s.ORG.Field<int>("品番コード"),
                                        自社品番 = s.ORG.Field<string>("自社品番"),
                                        自社色 = s.ORG.Field<string>("自社色"),
                                        色名称 = s.ORG.Field<string>("色名称"),
                                        倉庫コード = s.ORG.Field<int>("倉庫コード"),
                                        倉庫名 = s.ORG.Field<string>("倉庫名"),
                                        MST自社品番 = s.HIN.自社品番,
                                        MST自社色 = s.HIN.自社色,
                                        MST色名称 = s.IRO == null ? null : s.IRO.色名称,
                                        MST倉庫名 = s.SOKO.倉庫名,
                                        MST倉庫コード = s.SOKO == null? (int?)null : s.SOKO.倉庫コード
                                    });

                    // 存在エラーデータを抽出
                    var result = baseData.Where(w => w.MST自社品番 == null ||
                                                     w.MST倉庫名 == null ||
                                                     w.MST倉庫コード == (int?)null ||
                                                     w.自社品番 != w.MST自社品番 ||
                                                     w.自社色 != w.MST自社色 ||
                                                     w.倉庫名 != w.MST倉庫名 ||
                                                     w.倉庫コード != w.MST倉庫コード ||
                                                     (w.MST自社色 != null && w.色名称 == null) ||
                                                     (w.MST自社色 != null && w.色名称 != w.MST色名称))
                                          .OrderBy(o => o.行番号).ToList();

                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region 調整在庫更新
        /// <summary>
        /// 調整在庫更新
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="pDs"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int Update(Dictionary<string, string> dic, DataSet pDs, int userID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    try
                    {
                        DataTable dt = pDs.Tables[0];
                        int val = -1;
                        int yearMonth = Int32.TryParse(dic["処理年月"].Replace("/", ""), out val) ? val : -1;
                        int companyCd = Int32.TryParse(dic["会社コード"], out val) ? val : -1;
                        int sokoCd = Int32.TryParse(dic["倉庫コード"], out val) ? val : -1;

                        // 入力データ
                        var inputData = dt.AsEnumerable().Where(w => w.Field<int>("締年月") == yearMonth)
                                            .Join(context.M22_SOUK.Where(w => w.削除日時 == null),
                                                x => x.Field<int>("倉庫コード"),
                                                y => y.倉庫コード,
                                                (x, y) => new { ORG = x, SOKO = y })
                                            .Select(s => new InputDataMember
                                            {
                                                場所会社コード = s.SOKO.場所会社コード,
                                                締年月 = s.ORG.Field<int>("締年月"),
                                                品番コード = s.ORG.Field<int>("品番コード"),
                                                賞味期限 = s.ORG.Field<DateTime>("賞味期限"),
                                                倉庫コード = s.ORG.Field<int>("倉庫コード"),
                                                在庫数量 = s.ORG.Field<string>("調整数量") == null ? 
                                                                Int32.TryParse(s.ORG.Field<string>("在庫数量"), out val)? val: 0:
                                                          Int32.TryParse(s.ORG.Field<string>("調整数量"), out val)? val : 0
                                            });

                        // 条件絞り込み
                        if (companyCd != -1)
                        {
                            inputData = inputData.Where(w => w.場所会社コード == companyCd);
                        }
                        if (sokoCd != -1)
                        {
                            inputData = inputData.Where(w => w.倉庫コード == sokoCd);
                        }

                        // 既存データの物理削除
                        foreach (InputDataMember inp in inputData)
                        {
                            // 既存データの物理削除
                            var delData = context.S05_STOK_ADJUST
                                            .Where(w => w.締年月 == inp.締年月 &&
                                                        w.倉庫コード == inp.倉庫コード &&
                                                        w.品番コード == inp.品番コード &&
                                                        w.賞味期限 == inp.賞味期限).FirstOrDefault();
                            if (delData != null)
                            {
                                context.S05_STOK_ADJUST.DeleteObject(delData);
                            }

                            // 新規データの登録
                            S05_STOK_ADJUST s05in = new S05_STOK_ADJUST();
                            s05in.締年月 = inp.締年月;
                            s05in.倉庫コード = inp.倉庫コード;
                            s05in.品番コード = inp.品番コード;
                            s05in.賞味期限 = inp.賞味期限;
                            s05in.在庫数量 = inp.在庫数量;
                            s05in.登録者 = userID;
                            s05in.登録日時 = DateTime.Now;

                            context.S05_STOK_ADJUST.ApplyChanges(s05in);
                        }
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
        
        #region 在庫評価額一覧印刷データ取得
        /// <summary>
        /// 在庫評価額一覧印刷データ取得
        /// </summary>
        /// <param name="dic">検索条件Dictionary</param>
        /// <returns></returns>
        public List<PrintMenber> GetPrintData(Dictionary<string, string> dic)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try
                {
                    int val = -1;
                    int yearMonth = Int32.TryParse(dic["処理年月"].Replace("/", ""), out val) ? val : -1;
                    int companyCd = Int32.TryParse(dic["会社コード"], out val) ? val : -1;
                    int sokoCd = Int32.TryParse(dic["倉庫コード"], out val) ? val : -1;
                    int tagetStock = Int32.TryParse(dic["対象在庫"], out val) ?
                                        val == (int)対象在庫.月次在庫 ? (int)対象在庫.月次在庫 : (int)対象在庫.調整在庫 :
                                     -1;

                    #region 月次在庫データを取得
                    // 月次在庫データを取得
                    if (tagetStock == (int)対象在庫.月次在庫)
                    {
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
                            .GroupJoin(context.M22_SOUK.Where(w => w.削除日時 == null),
                                x => x.STOK_MONTH.倉庫コード,
                                y => y.倉庫コード,
                                (x, y) => new { x, y })
                            .SelectMany(z => z.y.DefaultIfEmpty(),
                                (a, b) => new {STOK_MONTH = a.x.STOK_MONTH, HIN = a.x.HIN, SOUK = b})
                                .ToList();

                        // 検索条件絞り込み
                        if (companyCd != -1)
                        {
                            stocktakingList = stocktakingList.Where(w => w.SOUK.場所会社コード == companyCd).ToList();
                        }
                        if (sokoCd != -1)
                        {
                            stocktakingList = stocktakingList.Where(w => w.STOK_MONTH.倉庫コード == sokoCd).ToList();
                        }

                        // ===========================
                        // 直近の仕入単価(最少額)を取得
                        // ===========================
                        // 年月末日の取得
                        ZIK05010 zik05010 = new ZIK05010();
                        DateTime dteEndofMonth = zik05010.getDateEndofMonth(yearMonth);

                        // 品番毎の直近日付を取得する
                        var LatestList =
                             context.T03_SRHD.Where(w => w.削除日時 == null && w.仕入日 < dteEndofMonth)
                                .Join(context.T03_SRDTL.Where(w => w.削除日時 == null),
                                    x => x.伝票番号,
                                    y => y.伝票番号,
                                    (x, y) => new { SHD = x, SDTL = y })
                            .GroupBy(g => new { g.SDTL.品番コード, g.SDTL.賞味期限 })
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
                                    x => new { dno = x.SRHD.伝票番号, hin = x.Latest.品番コード, date = x.Latest.賞味期限 },
                                    y => new { dno = y.伝票番号, hin = y.品番コード, date = y.賞味期限 },
                                    (x, y) => new { x.Latest, x.SRHD, SDTL = y })
                            .GroupBy(g => new { g.SDTL.品番コード, g.SDTL.賞味期限 })
                            .Select(s => new ZIK05010.SearchDataUnitPrice
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
                                    x => new { dno = x.STOK_MONTH.品番コード, date = x.STOK_MONTH.賞味期限 },
                                    y => new { dno = y.品番コード, date = y.賞味期限 },
                                    (x, y) => new { x, y })
                                .SelectMany(x => x.y.DefaultIfEmpty(),
                                    (e, f) => new { e.x.STOK_MONTH, e.x.HIN, e.x.SOUK, e.x.IRO, e.x.JIS, Purchase = f })
                                .Select(x => new PrintMenber
                                {
                                    自社コード = x.JIS.自社コード,
                                    自社名 = x.JIS.自社名 ?? "",
                                    倉庫名 = x.SOUK != null ? x.SOUK.倉庫略称名 : "",
                                    倉庫コード = x.STOK_MONTH.倉庫コード,
                                    品番コード = x.STOK_MONTH.品番コード,
                                    自社品番 = x.HIN.自社品番,
                                    自社品名 = x.HIN.自社品名,
                                    自社色コード = x.HIN.自社色,
                                    色名称 = x.IRO != null ? x.IRO.色名称 : "",
                                    賞味期限 = AppCommon.GetMaxDate() == x.STOK_MONTH.賞味期限 ? "" : x.STOK_MONTH.賞味期限.ToString("yyyy/MM/dd"),
                                    数量 = x.STOK_MONTH.在庫数量,
                                    単位 = x.HIN.単位,
                                    単価 = x.HIN.商品形態分類 == (int)商品形態分類.SET品 ?
                                                x.HIN.原価 ?? 0 :
                                           x.Purchase != null ?
                                                x.Purchase.仕入単価 : x.HIN.原価 ?? 0,
                                    評価額 = x.STOK_MONTH.在庫数量 * (x.Purchase != null ? x.Purchase.仕入単価 : x.HIN.原価 ?? 0)
                                })
                                .OrderBy(o => o.自社コード)
                                .ThenBy(t => t.倉庫コード)
                                .ThenBy(t => t.品番コード)
                                .ThenBy(t => t.自社色コード)
                                .ThenBy(t => t.賞味期限)
                                ;

                        return dataList.ToList();
                    }
                    #endregion

                    #region 調整在庫データを取得
                    // 調整在庫データを取得
                    else
                    {
                        // ===========================
                        // 在庫基本情報取得
                        // ===========================
                        // 調整在庫情報取得
                        var stockAdjTakingList =
                            context.S05_STOK_ADJUST.Where(w => w.締年月 == yearMonth)
                            .Join(context.M09_HIN.Where(w => w.削除日時 == null),
                                x => x.品番コード,
                                y => y.品番コード,
                                (x, y) => new { STOK_MONTH = x, HIN = y })
                            .GroupJoin(context.M22_SOUK.Where(w => w.削除日時 == null),
                                x => x.STOK_MONTH.倉庫コード,
                                y => y.倉庫コード,
                                (x, y) => new { x, y })
                            .SelectMany(z => z.y.DefaultIfEmpty(),
                                (a, b) => new { STOK_MONTH = a.x.STOK_MONTH, HIN = a.x.HIN, SOUK = b })
                                .ToList();

                        // 検索条件絞り込み
                        if (companyCd != -1)
                        {
                            stockAdjTakingList = stockAdjTakingList.Where(w => w.SOUK.場所会社コード == companyCd).ToList();
                        }
                        if (sokoCd != -1)
                        {
                            stockAdjTakingList = stockAdjTakingList.Where(w => w.STOK_MONTH.倉庫コード == sokoCd).ToList();
                        }

                        // ===========================
                        // 直近の仕入単価(最少額)を取得
                        // ===========================
                        // 年月末日の取得
                        ZIK05010 zik05010 = new ZIK05010();
                        DateTime dteEndofMonth = zik05010.getDateEndofMonth(yearMonth);

                        // 品番毎の直近日付を取得する
                        var LatestList =
                             context.T03_SRHD.Where(w => w.削除日時 == null && w.仕入日 < dteEndofMonth)
                                .Join(context.T03_SRDTL.Where(w => w.削除日時 == null),
                                    x => x.伝票番号,
                                    y => y.伝票番号,
                                    (x, y) => new { SHD = x, SDTL = y })
                            .GroupBy(g => new { g.SDTL.品番コード, g.SDTL.賞味期限 })
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
                                    x => new { dno = x.SRHD.伝票番号, hin = x.Latest.品番コード, date = x.Latest.賞味期限 },
                                    y => new { dno = y.伝票番号, hin = y.品番コード, date = y.賞味期限 },
                                    (x, y) => new { x.Latest, x.SRHD, SDTL = y })
                            .GroupBy(g => new { g.SDTL.品番コード, g.SDTL.賞味期限 })
                            .Select(s => new ZIK05010.SearchDataUnitPrice
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
                            stockAdjTakingList
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
                                    x => new { dno = x.STOK_MONTH.品番コード, date = x.STOK_MONTH.賞味期限 },
                                    y => new { dno = y.品番コード, date = y.賞味期限 },
                                    (x, y) => new { x, y })
                                .SelectMany(x => x.y.DefaultIfEmpty(),
                                    (e, f) => new { e.x.STOK_MONTH, e.x.HIN, e.x.SOUK, e.x.IRO, e.x.JIS, Purchase = f })
                                .Select(x => new PrintMenber
                                {
                                    自社コード = x.JIS.自社コード,
                                    自社名 = x.JIS.自社名 ?? "",
                                    倉庫名 = x.SOUK != null ? x.SOUK.倉庫略称名 : "",
                                    倉庫コード = x.STOK_MONTH.倉庫コード,
                                    品番コード = x.STOK_MONTH.品番コード,
                                    自社品番 = x.HIN.自社品番,
                                    自社品名 = x.HIN.自社品名,
                                    自社色コード = x.HIN.自社色,
                                    色名称 = x.IRO != null ? x.IRO.色名称 : "",
                                    賞味期限 = AppCommon.GetMaxDate() == x.STOK_MONTH.賞味期限 ? "" : x.STOK_MONTH.賞味期限.ToString("yyyy/MM/dd"),
                                    数量 = x.STOK_MONTH.在庫数量,
                                    単位 = x.HIN.単位,
                                    単価 = x.HIN.商品形態分類 == (int)商品形態分類.SET品 ?
                                                x.HIN.原価 ?? 0 :
                                           x.Purchase != null ?
                                                x.Purchase.仕入単価 : x.HIN.原価 ?? 0,
                                    評価額 = x.STOK_MONTH.在庫数量 * (x.Purchase != null ? x.Purchase.仕入単価 : x.HIN.原価 ?? 0)
                                })
                                .OrderBy(o => o.自社コード)
                                .ThenBy(t => t.倉庫コード)
                                .ThenBy(t => t.品番コード)
                                .ThenBy(t => t.自社色コード)
                                .ThenBy(t => t.賞味期限)
                                ;

                        return dataList.ToList();
                     
                    }
                    
                    #endregion
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