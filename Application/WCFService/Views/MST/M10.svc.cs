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
using System.Data.Objects.SqlClient;

namespace KyoeiSystem.Application.WCFService
{
    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コード、svc、および config ファイルで同時にクラス名 "DataDriveLogService" を変更できます。
    // 注意: このサービスをテストするために WCF テスト クライアントを起動するには、ソリューション エクスプローラーで DataDriveLogService.svc または DataDriveLogService.svc.cs を選択し、デバッグを開始してください。
    public class M10
    {

        #region メンバークラス定義

        public class M10_SHIN_Member
        {
            public string 自社品名 { get; set; }
            public int 品番コード { get; set; }
            public int 行 { get; set; }
            public string 材料品番 { get; set; }
            public string 材料品名 { get; set; }
            public string 材料色 { get; set; }
            public string 数量 { get; set; }
        }

        public class M10_TOKHIN_Search
        {
            public int 得意先コード { get; set; }
            public int 枝番 { get; set; }
            public int? 品番コード { get; set; }
            public string 品番名称 { get; set; }
            public string 得意先品番 { get; set; }
            // 以下フィルタ用項目
            public string 品群 { get; set; }
            public int? 商品区分 { get; set; }
            public string 得意先名１ { get; set; }
            public string 得意先名２ { get; set; }
            public bool 論理削除 { get; set; }
        }

        public class M10_TOKHIN_Named_Member
        {
            // REMARKS:各項目の追加分を定義
            public int 品番コード { get; set; }
            public string 得意先品番コード { get; set; }
            public string 自社品番 { get; set; }
            public string 自社色 { get; set; }
            public string 色名称 { get; set; }
            public string 自社品名 { get; set; }
            public int? 商品形態分類 { get; set; }
            public int? 商品分類 { get; set; }
            public int? 大分類 { get; set; }
            public string 大分類名 { get; set; }
            public int? 中分類 { get; set; }
            public string 中分類名 { get; set; }
            public string ブランド { get; set; }
            public string ブランド名 { get; set; }
            public string シリーズ { get; set; }
            public string シリーズ名 { get; set; }
            public string 品群 { get; set; }
            public string 品群名 { get; set; }
            public string 単位 { get; set; }
            public decimal? 原価 { get; set; }
            public decimal? 加工原価 { get; set; }
            public decimal? 卸値 { get; set; }
            public decimal? 売価 { get; set; }
            public decimal? 掛率 { get; set; }
            public int? 消費税区分 { get; set; }
            public int? 返却可能期限 { get; set; }
            public string ＪＡＮコード { get; set; }
            public string 備考１ { get; set; }
            public string 備考２ { get; set; }

        }

        #endregion


        #region M10_SHIN メソッド群

        /// <summary>
        /// M10_SHINのデータ取得
        /// </summary>
        /// <param name="p得意先ID">取引先ID</param>
        /// <returns>M10_UHK_Member</returns>
        public List<M10_SHIN_Member> GetData(string pセット品番コード,string p色コード)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                if(string.IsNullOrEmpty(p色コード))
                {
                    p色コード = null;
                }

                var main =
                    context.M10_SHIN
                    //.Join(context.M09_HIN.Where(w => w.削除日時 == null && w.自社品番 == pセット品番コード && w.自社色 == p色コード), nullの場合取得出来ない。
                    .Join(context.M09_HIN.Where(w => w.削除日時 == null && w.自社品番 == pセット品番コード && (p色コード == null ? w.自社色 == null : w.自社色 == p色コード)),
                                x => x.品番コード,
                                y => y.品番コード,
                                (x, y) => new { SHIN = x, HIN = y })
                            .Select(s => new { s.SHIN })
                            .AsQueryable();

                //var query = main
                //            .Join(context.M09_HIN.Where(w => w.削除日時 == null),
                //                x => x.SHIN.構成品番コード,
                //                y => y.品番コード,
                //                (x, y) => new { x, HIN = y })
                //            .Select(s => new { SHIN = s.x, s.HIN })
                //            .GroupJoin(context.M06_IRO.Where(o => o.削除日時 == null),
                //                p => p.HIN.自社色,
                //                q => q.色コード,
                //                (p, q) => new { p, q })
                //            .SelectMany(x => x.q.DefaultIfEmpty(),
                //                (x, y) => new M10_SHIN_Member
                //                {
                //                    品番コード = x.p.HIN.品番コード,
                //                    行 = x.p.SHIN.SHIN.部品行,
                //                    材料品番 = x.p.HIN.自社品番,
                //                    材料品名 = x.p.HIN.自社品名,
                //                    材料色 = y.色名称,
                //                    数量 = SqlFunctions.StringConvert((double?)x.p.SHIN.SHIN.使用数量)
                //                }).AsQueryable();

                var query = (from m in main
                            from mhin in context.M09_HIN.Where(c => c.品番コード == m.SHIN.品番コード)
                            from mkousei in context.M09_HIN.Where( k => k.品番コード == m.SHIN.構成品番コード)
                            from iro in context.M06_IRO.Where( j => j.色コード == mkousei.自社色).DefaultIfEmpty()
                            select new M10_SHIN_Member
                            {
                                自社品名 = mhin.自社品名,
                                品番コード = m.SHIN.構成品番コード,
                                行 = m.SHIN.部品行,
                                材料品番 = mkousei.自社品番,
                                材料品名 = mkousei.自社品名,
                                材料色 = iro.色名称,
                                数量 = SqlFunctions.StringConvert((double?)m.SHIN.使用数量)
                            }).AsQueryable();
                            

                return query.ToList();

            }

        }

        /// <summary>
        /// M10_SHINのデータ更新
        /// </summary>
        /// <param name="myProductCode">自社品番</param>
        /// <param name="updList">材料品情報リスト</param>
        /// <param name="pLoginUserCode">担当者ID</param>
        /// <returns></returns>
        public int Update(string myProductCode, string myColorCode, DataSet updDs, int pLoginUserCode)
        {
            DataTable updTbl = updDs.Tables["result"];
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                if (string.IsNullOrEmpty(myColorCode))
                {
                    myColorCode = null;
                }

                // -- 対象品番情報を削除 --
                // ①対象品番の取得
                var target = context.M09_HIN.Where(w => w.自社品番 == myProductCode && (myColorCode == null ? w.自社色 == null : w.自社色 == myColorCode)).FirstOrDefault();
                // 対象データが取得できなかった場合は処理終了
                if (target == null)
                    return 0;

                // ②対象品番のセット品番情報を削除
                var del = context.M10_SHIN.Where(w => w.品番コード == target.品番コード).ToList();
                if (del == null)
                    return 0;

                foreach (var tar in del)
                    context.M10_SHIN.DeleteObject(tar);

                // -- 対象品番情報を登録 --
                int rowCount = 1;
                foreach (DataRow row in updTbl.Rows)
                {
                    M10_SHIN_Member mem = getM10SHIN_ConvertMemberRow(row);

                    // 材料品番が未入力は登録なしとする
                    if (string.IsNullOrEmpty(mem.材料品番))
                        continue;

                    int qty = int.Parse(mem.数量);

                    M10_SHIN m10 = new M10_SHIN();
                    m10.品番コード = target.品番コード;
                    m10.部品行 = rowCount;
                    m10.構成品番コード = mem.品番コード;
                    m10.使用数量 = qty;
                    m10.登録者 = pLoginUserCode;
                    m10.登録日時 = DateTime.Now;
                    m10.最終更新者 = pLoginUserCode;
                    m10.最終更新日時 = DateTime.Now;

                    context.M10_SHIN.ApplyChanges(m10);
                    rowCount++;

                }

                context.SaveChanges();

            }

            return 1;

        }

        #endregion

        #region M10_TOKHINメソッド群

        /// <summary>
        /// 得意先コードで得意先品番マスタを検索する
        /// </summary>
        /// <param name="CustomerCode">得意先コード</param>
        /// <param name="CustomerEda">得意先コード枝番</param>
        /// <returns></returns>
        public List<M10_TOKHIN_Search> GetData_TOKHIN(string CustomerCode, string CustomerEda)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int code, eda;
                int.TryParse(CustomerCode, out code);
                int.TryParse(CustomerEda, out eda);

                var result = context.M10_TOKHIN.Where(w => w.取引先コード == code && w.枝番 == eda)
                              .GroupJoin(context.M01_TOK,
                                    x => new { コード = x.取引先コード, 枝番 = x.枝番 },
                                    y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                                    (a, b) => new { a, b })
                              .SelectMany(x => x.b.DefaultIfEmpty(), (x, y) => new { x, 得意先名１ = y.得意先名１, 得意先名２ = y.得意先名２ })
                              .GroupJoin(context.M09_HIN, x => x.x.a.品番コード, y => y.品番コード, (c, d) => new { c, d })
                              .SelectMany(x => x.d.DefaultIfEmpty(), (x, y) =>
                                  new M10_TOKHIN_Search
                                  {
                                      得意先コード = x.c.x.a.取引先コード,
                                      枝番 = x.c.x.a.枝番,
                                      品番コード = x.c.x.a.品番コード,
                                      品番名称 = y.自社品名,
                                      得意先品番 = x.c.x.a.得意先品番コード,
                                      // 以下フィルタ用項目
                                      品群 = y.品群,
                                      商品区分 = y.商品形態分類,
                                      得意先名１ = x.c.得意先名１,
                                      得意先名２ = x.c.得意先名２,
                                      論理削除 = false
                                  });

                return result.ToList();

            }

        }

        /// <summary>
        /// 品番コードで売価マスタを検索する
        /// </summary>
        /// <param name="productCode">自社品番</param>
        /// <param name="colorCode">色</param>
        /// <returns></returns>
        public List<M10_TOKHIN_Search> GetData_TOKHIN_Product(string productCode, string colorCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                List<M10_TOKHIN_Search> result;
                result = context.M10_TOKHIN
                            .Join(context.M09_HIN.Where(w =>
                                        w.削除日時 == null && w.自社品番 == productCode),
                                x => x.品番コード,
                                y => y.品番コード,
                                (a, b) => new { a, b })
                            .Select(x => new { TOKHIN = x.a, HIN = x.b })
                            .GroupJoin(context.M01_TOK,
                                x => new { コード = x.TOKHIN.取引先コード, 枝番 = x.TOKHIN.枝番 },
                                y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                                (c, d) => new { c, d })
                            .SelectMany(z => z.d.DefaultIfEmpty(),
                                (p, q) => new M10_TOKHIN_Search
                                {
                                    得意先コード = p.c.TOKHIN.取引先コード,
                                    枝番 = p.c.TOKHIN.枝番,
                                    品番コード = p.c.TOKHIN.品番コード,
                                    品番名称 = p.c.HIN.自社品名,
                                    得意先品番 = p.c.TOKHIN.得意先品番コード,
                                    // 以下フィルタ用項目
                                    品群 = p.c.HIN.品群,
                                    商品区分 = p.c.HIN.商品形態分類,
                                    得意先名１ = q.得意先名１,
                                    得意先名２ = q.得意先名２,
                                    論理削除 = false
                                })
                            .ToList();

                return result;

            }

        }

        /// <summary>
        /// 得意先品番を含む名前付き品番データリストを取得する
        /// </summary>
        public List<M10_TOKHIN_Named_Member> GetTOKHIN_NamedDataList(int 得意先コード, int 得意先枝番)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var result =
                    context.M09_HIN.Where(w => w.削除日時 == null)
                        .GroupJoin(context.M10_TOKHIN.Where(w => w.削除日時 == null),
                            x => new { 品番コード = x.品番コード, 取引先コード = 得意先コード, 枝番 = 得意先枝番  },
                            y => new { 品番コード = y.品番コード, 取引先コード = y.取引先コード, 枝番 = y.枝番 },
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(), (a, b) => new { HIN = a.x, TOKHIN = b })
                        .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null), x => x.HIN.自社色, y => y.色コード, (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(), (c, d) => new { c.x.HIN, c.x.TOKHIN, IRO = d })
                        .GroupJoin(context.M12_DAIBUNRUI.Where(w => w.削除日時 == null), x => x.HIN.大分類, y => y.大分類コード, (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(), (e, f) => new { e.x.HIN, e.x.TOKHIN, e.x.IRO, DAI = f })
                        .GroupJoin(context.M13_TYUBUNRUI.Where(w => w.削除日時 == null), x => x.HIN.中分類, y => y.中分類コード, (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(), (g, h) => new { g.x.HIN, g.x.TOKHIN, g.x.IRO, g.x.DAI, TYU = h })
                        .GroupJoin(context.M14_BRAND.Where(w => w.削除日時 == null), x => x.HIN.ブランド, y => y.ブランドコード, (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(), (i, j) => new { i.x.HIN, i.x.TOKHIN, i.x.IRO, i.x.DAI, i.x.TYU, BRAND = j })
                        .GroupJoin(context.M15_SERIES.Where(w => w.削除日時 == null), x => x.HIN.シリーズ, y => y.シリーズコード, (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(), (k, l) => new { k.x.HIN, k.x.TOKHIN, k.x.IRO, k.x.DAI, k.x.TYU, k.x.BRAND, SERIES = l })
                        .GroupJoin(context.M16_HINGUN.Where(w => w.削除日時 == null), x => x.HIN.品群, y => y.品群コード, (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(), (m, n) => new { m.x.HIN, m.x.TOKHIN, m.x.IRO, m.x.DAI, m.x.TYU, m.x.BRAND, m.x.SERIES, HINGUN = n })

                        .GroupJoin(context.M02_BAIKA.Where(w => w.削除日時 == null && w.得意先コード == 得意先コード && w.枝番 == 得意先枝番), x => x.HIN.品番コード, y => y.品番コード, (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(), (o, p) => new { o.x.HIN, o.x.TOKHIN, o.x.IRO, o.x.DAI, o.x.TYU, o.x.BRAND, o.x.SERIES, o.x.HINGUN, TBAI = p })
                        .GroupJoin(context.M03_BAIKA.Where(w => w.削除日時 == null && w.仕入先コード == 得意先コード && w.枝番 == 得意先枝番), x => x.HIN.品番コード, y => y.品番コード, (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(), (q, r) => new { q.x.HIN, q.x.TOKHIN, q.x.IRO, q.x.DAI, q.x.TYU, q.x.BRAND, q.x.SERIES, q.x.HINGUN, q.x.TBAI, SBAI = r })
                        .GroupJoin(context.M04_BAIKA.Where(w => w.削除日時 == null && w.外注先コード == 得意先コード && w.枝番 == 得意先枝番), x => x.HIN.品番コード, y => y.品番コード, (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(), (s, t) => new { s.x.HIN, s.x.TOKHIN, s.x.IRO, s.x.DAI, s.x.TYU, s.x.BRAND, s.x.SERIES, s.x.HINGUN, s.x.TBAI, s.x.SBAI, GBAI = t })
                        
                        .Select(x => new M10_TOKHIN_Named_Member
                            {
                                品番コード = x.HIN.品番コード,
                                自社品番 = x.HIN.自社品番,
                                得意先品番コード = x.TOKHIN.得意先品番コード,
                                自社色 = x.HIN.自社色,
                                色名称 = x.IRO.色名称,
                                自社品名 = x.HIN.自社品名,
                                商品形態分類 = x.HIN.商品形態分類,
                                商品分類 = x.HIN.商品分類,
                                大分類 = x.HIN.大分類,
                                大分類名 = x.DAI.大分類名,
                                中分類 = x.HIN.中分類,
                                中分類名 = x.TYU.中分類名,
                                ブランド = x.HIN.ブランド,
                                ブランド名 = x.BRAND.ブランド名,
                                シリーズ = x.HIN.シリーズ,
                                シリーズ名 = x.SERIES.シリーズ名,
                                品群 = x.HIN.品群,
                                品群名 = x.HINGUN.品群名,
                                単位 = x.HIN.単位,
                                原価 = x.SBAI != null ? x.SBAI.単価 : x.HIN.原価,
                                加工原価 = x.GBAI != null ? x.GBAI.単価 : x.HIN.加工原価,
                                卸値 = x.HIN.卸値,
                                売価 = x.TBAI != null ? x.TBAI.単価 : x.HIN.売価,
                                掛率 = x.HIN.掛率,
                                消費税区分 = x.HIN.消費税区分,
                                返却可能期限 = x.HIN.返却可能期限,
                                ＪＡＮコード = x.HIN.ＪＡＮコード,
                                備考１ = x.HIN.備考１,
                                備考２ = x.HIN.備考２
                            });

                return result.ToList();

            }

        }

        /// <summary>
        /// 得意先品番登録データ登録処理
        /// </summary>
        /// <param name="ds">
        /// データセット
        /// 　[0:updTbl]登録・更新対象のデータテーブル
        /// 　[1:delTbl]削除対象のデータテーブル
        /// </param>
        public void Update_TOKHIN(DataSet ds, int loginUserId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // データ登録・更新
                DataTable updTbl = ds.Tables["updTbl"];
                foreach (DataRow rw in updTbl.Rows)
                {
                    // 変更なしデータは処理対象外とする
                    if (rw.RowState == DataRowState.Unchanged)
                        continue;

                    M10_TOKHIN_Search row = getM10TOKHINHIN_ConvertMemberRow(rw);
                    // 対象データ取得
                    var data =
                        context.M10_TOKHIN
                            .Where(w => w.取引先コード == row.得意先コード &&
                                w.枝番 == row.枝番 &&
                                w.品番コード == row.品番コード)
                            .FirstOrDefault();

                    if (data == null)
                    {
                        // 新規登録
                        M10_TOKHIN tokhin = new M10_TOKHIN();
                        tokhin.取引先コード = row.得意先コード;
                        tokhin.枝番 = row.枝番;
                        tokhin.品番コード = row.品番コード ?? -1;
                        tokhin.得意先品番コード = row.得意先品番;
                        tokhin.登録者 = loginUserId;
                        tokhin.登録日時 = DateTime.Now;
                        tokhin.最終更新者 = loginUserId;
                        tokhin.最終更新日時 = DateTime.Now;

                        context.M10_TOKHIN.ApplyChanges(tokhin);

                    }
                    else
                    {
                        // データ更新
                        data.得意先品番コード = row.得意先品番;
                        data.最終更新者 = loginUserId;
                        data.最終更新日時 = DateTime.Now;
                        data.削除者 = null;
                        data.削除日時 = null;

                        data.AcceptChanges();

                    }

                }

                // データ削除
                DataTable delTbl = ds.Tables["delTbl"];
                foreach (DataRow rw in delTbl.Rows)
                {
                    M10_TOKHIN_Search row = getM10TOKHINHIN_ConvertMemberRow(rw);

                    // 対象データ取得
                    var data =
                        context.M10_TOKHIN
                            .Where(w => w.取引先コード == row.得意先コード
                                && w.枝番 == row.枝番 &&
                                w.品番コード == row.品番コード)
                            .FirstOrDefault();

                    if (data == null)
                        continue;

                    context.M10_TOKHIN.DeleteObject(data);
                    data.AcceptChanges();

                }

                context.SaveChanges();

            }

        }



        #endregion


        #region << 処理関連 >>

        /// <summary>
        /// DataRowをM10_SHIN_Memberクラスにコンバートする
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private M10_SHIN_Member getM10SHIN_ConvertMemberRow(DataRow row)
        {
            M10_SHIN_Member mem = new M10_SHIN_Member();

            mem.品番コード = int.Parse(row["品番コード"].ToString());
            mem.行 = int.Parse(row["行"].ToString());
            mem.材料品番 = row["材料品番"].ToString();
            mem.数量 = row["数量"].ToString().Trim();

            return mem;

        }

        /// <summary>
        /// DataRowをM10_SHIN_Memberクラスにコンバートする
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private M10_TOKHIN_Search getM10TOKHINHIN_ConvertMemberRow(DataRow row)
        {
            M10_TOKHIN_Search mem = new M10_TOKHIN_Search();

            mem.得意先コード = int.Parse(row["得意先コード"].ToString());
            mem.枝番 = int.Parse(row["枝番"].ToString());
            mem.品番コード = int.Parse(row["品番コード"].ToString());
            mem.得意先品番 = row["得意先品番"].ToString();

            return mem;

        }

        #endregion

    }

}
