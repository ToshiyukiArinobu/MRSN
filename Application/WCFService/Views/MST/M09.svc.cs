using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Objects;
using System.Data;
using System.Data.Common;
using System.Transactions;


namespace KyoeiSystem.Application.WCFService
{
    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コード、svc、および config ファイルで同時にクラス名 "DataDriveLogService" を変更できます。
    // 注意: このサービスをテストするために WCF テスト クライアントを起動するには、ソリューション エクスプローラーで DataDriveLogService.svc または DataDriveLogService.svc.cs を選択し、デバッグを開始してください。

    /// <summary>
    /// 品番マスタサービスクラス
    /// </summary>
    public class M09
    {
        private const int 論理削除 = 9;

        /// <summary>
        /// M09_HINの各種名称付与クラス
        /// </summary>
        public class M09_HIN_NAMED : M09_HIN
        {
            public string 自社色名 { get; set; }
            public string 商品形態分類名 { get; set; }
            public string 商品分類名 { get; set; }
            public string 大分類名 { get; set; }
            public string 中分類名 { get; set; }
            public string ブランド名 { get; set; }
            public string シリーズ名 { get; set; }
            public string 品群名 { get; set; }
            public Decimal? マスタ原価 { get; set; }

        }

        /// <summary>
        /// 品番データを取得する
        /// </summary>
        /// <returns></returns>
        public List<M09_HIN> GetData(string productCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int iProductCode = -1;
                if(!int.TryParse(productCode, out iProductCode))
                    return new List<M09_HIN>();

                var result =
                    context.M09_HIN.Where(w => (w.削除日時 == null || w.論理削除 == 論理削除) && w.品番コード == iProductCode);

                return result.ToList();

            }

        }

        /// <summary>
        /// 品番の全データを取得する
        /// </summary>
        /// <returns></returns>
        public List<M09_HIN> GetDataList()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var result =
                    context.M09_HIN.Where(w => w.削除日時 == null || w.論理削除 == 論理削除);

                return result.ToList();

            }

        }

        /// <summary>
        /// 品番の全データ(名称付き)を取得する
        /// </summary>
        /// <param name="paramDic">
        /// [0]コード
        /// [1]枝番
        /// </param>
        /// <returns></returns>
        public List<M09_HIN_NAMED> GetNamedDataList(Dictionary<string, int?> paramDic)
        {
            int? code = paramDic["コード"];
            int? eda = paramDic["枝番"];

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var result =
                    context.M09_HIN.Where(w => w.削除日時 == null || w.論理削除 == 論理削除)
                    .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null),
                        x => x.自社色, y => y.色コード, (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (a, b) => new { HIN = a.x, IRO = b })
                    .GroupJoin(context.M12_DAIBUNRUI.Where(w => w.削除日時 == null),
                        x => x.HIN.大分類, y => y.大分類コード, (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (c, d) => new { c.x.HIN, c.x.IRO, DAI = d })
                    .GroupJoin(context.M13_TYUBUNRUI.Where(w => w.削除日時 == null),
                        x => x.HIN.中分類, y => y.中分類コード, (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (e, f) => new { e.x.HIN, e.x.IRO, e.x.DAI, TYU = f })
                    .GroupJoin(context.M14_BRAND.Where(w => w.削除日時 == null),
                        x => x.HIN.ブランド, y => y.ブランドコード, (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (g, h) => new { g.x.HIN, g.x.IRO, g.x.DAI, g.x.TYU, BRA = h })
                    .GroupJoin(context.M15_SERIES.Where(w => w.削除日時 == null),
                        x => x.HIN.シリーズ, y => y.シリーズコード, (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (i, j) => new { i.x.HIN, i.x.IRO, i.x.DAI, i.x.TYU, i.x.BRA, SER = j })
                    .GroupJoin(context.M16_HINGUN.Where(w => w.削除日時 == null),
                        x => x.HIN.品群, y => y.品群コード, (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (k, l) => new { k.x.HIN, k.x.IRO, k.x.DAI, k.x.TYU, k.x.BRA, k.x.SER, GUN = l })

                    .GroupJoin(context.M02_BAIKA.Where(w => w.削除日時 == null && w.得意先コード == code && w.枝番 == eda), x => x.HIN.品番コード, y => y.品番コード, (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(), (m, n) => new { m.x.HIN, m.x.IRO, m.x.DAI, m.x.TYU, m.x.BRA, m.x.SER, m.x.GUN, TBAI = n })
                    .GroupJoin(context.M03_BAIKA.Where(w => w.削除日時 == null && w.仕入先コード == code && w.枝番 == eda), x => x.HIN.品番コード, y => y.品番コード, (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(), (o, p) => new { o.x.HIN, o.x.IRO, o.x.DAI, o.x.TYU, o.x.BRA, o.x.SER, o.x.GUN, o.x.TBAI, SBAI = p })
                    .GroupJoin(context.M04_BAIKA.Where(w => w.削除日時 == null && w.外注先コード == code && w.枝番 == eda), x => x.HIN.品番コード, y => y.品番コード, (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(), (q, r) => new { q.x.HIN, q.x.IRO, q.x.DAI, q.x.TYU, q.x.BRA, q.x.SER, q.x.GUN, q.x.TBAI, q.x.SBAI, GBAI = r })

                    .Select(t => new M09_HIN_NAMED
                    {
                        品番コード = t.HIN.品番コード,
                        自社品番 = t.HIN.自社品番,
                        自社色 = t.HIN.自社色,
                        自社色名 = t.IRO.色名称,
                        商品形態分類 = t.HIN.商品形態分類,
                        商品形態分類名 = "",
                        商品分類 = t.HIN.商品分類,
                        商品分類名 = "",
                        大分類 = t.HIN.大分類,
                        大分類名 = t.DAI.大分類名,
                        中分類 = t.HIN.中分類,
                        中分類名 = t.TYU.中分類名,
                        ブランド = t.HIN.ブランド,
                        ブランド名 = t.BRA.ブランド名,
                        シリーズ = t.HIN.シリーズ,
                        シリーズ名 = t.SER.シリーズ名,
                        品群 = t.HIN.品群,
                        品群名 = t.GUN.品群名,
                        自社品名 = t.HIN.自社品名,
                        単位 = t.HIN.単位,
                        マスタ原価 = t.HIN.原価,
                        原価 = t.SBAI.単価,// != null ? t.SBAI.単価 : t.HIN.原価,
                        加工原価 = t.GBAI != null ? t.GBAI.単価 : t.HIN.加工原価,
                        卸値 = t.HIN.卸値,
                        売価 = t.TBAI != null ? t.TBAI.単価 : t.HIN.売価,
                        掛率 = t.HIN.掛率,
                        消費税区分 = t.HIN.消費税区分,
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

        /// <summary>
        /// 最大番号＋１のレコードを生成して返す
        /// </summary>
        /// <returns></returns>
        public List<M09_HIN> GetNextNumber()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int productCode = context.M09_HIN.Max(x => x.品番コード);

                M09_HIN m09 = new M09_HIN();
                m09.品番コード = productCode + 1;

                return new List<M09_HIN>() { m09 };

            }


        }

        /// <summary>
        /// 商品群データの登録・更新をおこなう
        /// </summary>
        /// <param name="updData"></param>
        /// <param name="pLoginUserCode"></param>
        /// <returns></returns>
        public int Update(M09_HIN updData, int pLoginUserCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 更新行を特定
                var m09 =
                    context.M09_HIN
                        .Where(x => x.品番コード == updData.品番コード)
                        .Select(c => c)
                        .FirstOrDefault();

                if (m09 == null)
                {
                    // 未登録時(追加)
                    M09_HIN hin = new M09_HIN();

                    hin.品番コード = updData.品番コード;
                    hin.自社品番 = updData.自社品番;
                    hin.自社色 = updData.自社色;
                    hin.商品形態分類 = updData.商品形態分類;
                    hin.商品分類 = updData.商品分類;
                    hin.大分類 = updData.大分類;
                    hin.中分類 = updData.中分類;
                    hin.ブランド = updData.ブランド;
                    hin.シリーズ = updData.シリーズ;
                    hin.品群 = updData.品群;
                    hin.自社品名 = updData.自社品名;
                    hin.単位 = updData.単位;
                    hin.原価 = updData.原価;
                    hin.加工原価 = updData.加工原価;
                    hin.卸値 = updData.卸値;
                    hin.売価 = updData.売価;
                    hin.掛率 = updData.掛率;
                    hin.消費税区分 = updData.消費税区分;
                    hin.備考１ = updData.備考１;
                    hin.備考２ = updData.備考２;
                    hin.返却可能期限 = updData.返却可能期限;
                    hin.ＪＡＮコード = updData.ＪＡＮコード;
                    hin.論理削除 = updData.論理削除;
                    hin.登録日時 = DateTime.Now;
                    hin.登録者 = pLoginUserCode;
                    hin.最終更新日時 = DateTime.Now;
                    hin.最終更新者 = pLoginUserCode;
                    if (updData.論理削除.Equals(9))
                    {
                        m09.削除日時 = DateTime.Now;
                        m09.削除者 = pLoginUserCode;

                    }

                    context.M09_HIN.ApplyChanges(hin);

                }
                else
                {
                    // 登録済(更新)
                    m09.自社品番 = updData.自社品番;
                    m09.自社色 = updData.自社色;
                    m09.商品形態分類 = updData.商品形態分類;
                    m09.商品分類 = updData.商品分類;
                    m09.大分類 = updData.大分類;
                    m09.中分類 = updData.中分類;
                    m09.ブランド = updData.ブランド;
                    m09.シリーズ = updData.シリーズ;
                    m09.品群 = updData.品群;
                    m09.自社品名 = updData.自社品名;
                    m09.単位 = updData.単位;
                    m09.原価 = updData.原価;
                    m09.加工原価 = updData.加工原価;
                    m09.卸値 = updData.卸値;
                    m09.売価 = updData.売価;
                    m09.掛率 = updData.掛率;
                    m09.消費税区分 = updData.消費税区分;
                    m09.備考１ = updData.備考１;
                    m09.備考２ = updData.備考２;
                    m09.返却可能期限 = updData.返却可能期限;
                    m09.ＪＡＮコード = updData.ＪＡＮコード;
                    m09.論理削除 = updData.論理削除;
                    if (updData.論理削除.Equals(9))
                    {
                        m09.削除日時 = DateTime.Now;
                        m09.削除者 = pLoginUserCode;

                    }
                    else
                    {
                        m09.削除日時 = null;
                        m09.削除者 = null;
                        m09.最終更新日時 = DateTime.Now;
                        m09.最終更新者 = pLoginUserCode;

                    }

                    m09.AcceptChanges();

                }

                context.SaveChanges();

            }

            return 1;

        }

        /// <summary>
        /// 名前付き品番情報を取得する
        /// </summary>
        /// <param name="myProductCode">自社品番</param>
        /// <param name="itemType">商品形態分類の配列</param>
        /// <returns></returns>
        public List<M09_HIN_NAMED> GetNamedData(string myProductCode, int[] itemType)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                List<int> itemTypeList = new List<int>(itemType);
                if (itemType == null || itemTypeList.Count == 0)
                    itemTypeList = new List<int>() { 1, 2, 3, 4 };  // デフォルト値として全指定状態を設定

                var result =
                    context.M09_HIN.Where(w =>
                            (w.削除日時 == null || w.論理削除 == 論理削除) && w.自社品番 == myProductCode && itemTypeList.Contains(w.商品形態分類 ?? 0))
                    .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null),
                        x => x.自社色, y => y.色コード, (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (p, q) => new { HIN = p.x, 色名称 = q.色名称 })
                    .GroupJoin(context.M12_DAIBUNRUI.Where(w => w.削除日時 == null),
                        x => x.HIN.大分類, y => y.大分類コード, (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (p, q) => new { p.x.HIN, p.x.色名称, 大分類名 = q.大分類名 })
                    .GroupJoin(context.M13_TYUBUNRUI.Where(w => w.削除日時 == null),
                        x => x.HIN.中分類, y => y.中分類コード, (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (p, q) => new { p.x.HIN, p.x.色名称, p.x.大分類名, 中分類名 = q.中分類名 })
                    .GroupJoin(context.M14_BRAND.Where(w => w.削除日時 == null),
                        x => x.HIN.ブランド, y => y.ブランドコード, (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (p, q) => new { p.x.HIN, p.x.色名称, p.x.大分類名, p.x.中分類名, ブランド名 = q.ブランド名 })
                    .GroupJoin(context.M15_SERIES.Where(w => w.削除日時 == null),
                        x => x.HIN.シリーズ, y => y.シリーズコード, (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (p, q) => new { p.x.HIN, p.x.色名称, p.x.大分類名, p.x.中分類名, p.x.ブランド名, シリーズ名 = q.シリーズ名 })
                    .GroupJoin(context.M16_HINGUN.Where(w => w.削除日時 == null),
                        x => x.HIN.品群, y => y.品群コード, (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (p, q) => new { p.x.HIN, p.x.色名称, p.x.大分類名, p.x.中分類名, p.x.ブランド名, p.x.シリーズ名, 品群名 = q.品群名 })
                    .Select(t => new M09_HIN_NAMED
                    {
                        品番コード = t.HIN.品番コード,
                        自社品番 = t.HIN.自社品番,
                        自社色 = t.HIN.自社色,
                        自社色名 = t.色名称,
                        商品形態分類 = t.HIN.商品形態分類,
                        商品形態分類名 = "",
                        商品分類 = t.HIN.商品分類,
                        商品分類名 = "",
                        大分類 = t.HIN.大分類,
                        大分類名 = t.大分類名,
                        中分類 = t.HIN.中分類,
                        中分類名 = t.中分類名,
                        ブランド = t.HIN.ブランド,
                        ブランド名 = t.ブランド名,
                        シリーズ = t.HIN.シリーズ,
                        シリーズ名 = t.シリーズ名,
                        品群 = t.HIN.品群,
                        品群名 = t.品群名,
                        自社品名 = t.HIN.自社品名,
                        単位 = t.HIN.単位,
                        原価 = t.HIN.原価,
                        加工原価 = t.HIN.加工原価,
                        卸値 = t.HIN.卸値,
                        売価 = t.HIN.売価,
                        掛率 = t.HIN.掛率,
                        消費税区分 = t.HIN.消費税区分,
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

    }

}
