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
    public class M03
    {
        public class M03_BAIKA_Search
        {
            public int 仕入先コード { get; set; }
            public int 仕入先コード枝番 { get; set; }
            public int? 品番コード { get; set; }
            public string 品番名称 { get; set; }
            public decimal? 単価 { get; set; }
            // 以下フィルタ用項目
            public string 品群 { get; set; }
            public int? 商品区分 { get; set; }
            public string 仕入先名１ { get; set; }
            public string 仕入先名２ { get; set; }
            public bool 論理削除 { get; set; }
        }

        /// <summary>
        /// 仕入先コードで売価マスタを検索する
        /// </summary>
        /// <param name="CustomerCode">仕入先コード</param>
        /// <param name="CustomerEda">仕入先コード枝番</param>
        /// <returns></returns>
        public List<M03_BAIKA_Search> GetData_Supplier(string CustomerCode, string CustomerEda)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int code, eda;
                int.TryParse(CustomerCode, out code);
                int.TryParse(CustomerEda, out eda);

                var result = context.M03_BAIKA.Where(w => w.仕入先コード == code && w.枝番 == eda)
                              .GroupJoin(context.M01_TOK,
                                    x => new { コード = x.仕入先コード, 枝番 = x.枝番 },
                                    y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                                    (a, b) => new { a, b })
                              .SelectMany(x => x.b.DefaultIfEmpty(), (x, y) => new { x, 仕入先名１ = y.得意先名１, 仕入先名２ = y.得意先名２ })
                              .GroupJoin(context.M09_HIN, x => x.x.a.品番コード, y => y.品番コード, (c, d) => new { c, d })
                              .SelectMany(x => x.d.DefaultIfEmpty(), (x, y) =>
                                  new M03_BAIKA_Search
                                  {
                                      仕入先コード = x.c.x.a.仕入先コード,
                                      仕入先コード枝番 = x.c.x.a.枝番,
                                      品番コード = x.c.x.a.品番コード,
                                      品番名称 = y.自社品名,
                                      単価 = x.c.x.a.単価,
                                      // 以下フィルタ用項目
                                      品群 = y.品群,
                                      商品区分 = y.商品形態分類,
                                      仕入先名１ = x.c.仕入先名１,
                                      仕入先名２ = x.c.仕入先名２,
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
        public List<M03_BAIKA_Search> GetData_Product(string productCode, string colorCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                List<M03_BAIKA_Search> result;
                if (string.IsNullOrEmpty(colorCode))
                {
                    result = context.M03_BAIKA
                                    .Join(context.M09_HIN.Where(w => w.自社品番 == productCode), x => x.品番コード, y => y.品番コード, (a, b) => new { a, b })
                                    .Select(x => new { BAIKA = x.a, HIN = x.b })
                                    .GroupJoin(context.M01_TOK,
                                        x => new { コード = x.BAIKA.仕入先コード, 枝番 = x.BAIKA.枝番 },
                                        y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                                        (c, d) => new { c, d })
                                    .SelectMany(z => z.d.DefaultIfEmpty(), (p, q) =>
                                        new M03_BAIKA_Search
                                        {
                                            仕入先コード = p.c.BAIKA.仕入先コード,
                                            仕入先コード枝番 = p.c.BAIKA.枝番,
                                            品番コード = p.c.BAIKA.品番コード,
                                            品番名称 = p.c.HIN.自社品名,
                                            単価 = p.c.BAIKA.単価,
                                            // 以下フィルタ用項目
                                            品群 = p.c.HIN.品群,
                                            商品区分 = p.c.HIN.商品形態分類,
                                            仕入先名１ = q.得意先名１,
                                            仕入先名２ = q.得意先名２,
                                            論理削除 = false
                                        })
                                    .ToList();

                }
                else
                {
                    result = context.M03_BAIKA
                                    .Join(context.M09_HIN.Where(w => w.自社品番 == productCode && w.自社色 == colorCode), x => x.品番コード, y => y.品番コード, (a, b) => new { a, b })
                                    .Select(x => new { BAIKA = x.a, HIN = x.b })
                                    .GroupJoin(context.M01_TOK,
                                        x => new { コード = x.BAIKA.仕入先コード, 枝番 = x.BAIKA.枝番 },
                                        y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                                        (c, d) => new { c, d })
                                    .SelectMany(z => z.d.DefaultIfEmpty(), (p, q) =>
                                        new M03_BAIKA_Search
                                        {
                                            仕入先コード = p.c.BAIKA.仕入先コード,
                                            仕入先コード枝番 = p.c.BAIKA.枝番,
                                            品番コード = p.c.BAIKA.品番コード,
                                            品番名称 = p.c.HIN.自社品名,
                                            単価 = p.c.BAIKA.単価,
                                            // 以下フィルタ用項目
                                            品群 = p.c.HIN.品群,
                                            商品区分 = p.c.HIN.商品形態分類,
                                            仕入先名１ = q.得意先名１,
                                            仕入先名２ = q.得意先名２,
                                            論理削除 = false
                                        })
                                    .ToList();

                }

                return result;

            }

        }

        /// <summary>
        /// 仕入先売価設定データ登録処理
        /// </summary>
        /// <param name="ds">
        /// データセット
        /// 　[0:updTbl]登録・更新対象のデータテーブル
        /// 　[1:delTbl]削除対象のデータテーブル
        /// </param>
        public void Update(DataSet ds, int loginUserId)
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

                    M03_BAIKA_Search row = getRowData(rw);
                    // 対象データ取得
                    var data =
                        context.M03_BAIKA
                            .Where(w => w.仕入先コード == row.仕入先コード && w.枝番 == row.仕入先コード枝番 && w.品番コード == row.品番コード)
                            .FirstOrDefault();

                    if (data == null)
                    {
                        // 新規登録
                        M03_BAIKA bik = new M03_BAIKA();
                        bik.仕入先コード = row.仕入先コード;
                        bik.枝番 = row.仕入先コード枝番;
                        bik.品番コード = row.品番コード ?? -1;
                        bik.単価 = row.単価;
                        bik.登録者 = loginUserId;
                        bik.登録日時 = DateTime.Now;
                        bik.最終更新者 = loginUserId;
                        bik.最終更新日時 = DateTime.Now;

                        context.M03_BAIKA.ApplyChanges(bik);

                    }
                    else
                    {
                        // データ更新
                        data.単価 = row.単価;
                        data.最終更新者 = loginUserId;
                        data.最終更新日時 = DateTime.Now;

                        data.AcceptChanges();

                    }

                }

                // データ削除
                DataTable delTbl = ds.Tables["delTbl"];
                foreach (DataRow rw in delTbl.Rows)
                {
                    M03_BAIKA_Search row = getRowData(rw);

                    // 対象データ取得
                    var data =
                        context.M03_BAIKA
                            .Where(w => w.仕入先コード == row.仕入先コード && w.枝番 == row.仕入先コード枝番 && w.品番コード == row.品番コード)
                            .FirstOrDefault();

                    if (data == null)
                        continue;

                    context.M03_BAIKA.DeleteObject(data);
                    data.AcceptChanges();

                }

                context.SaveChanges();

            }

        }

        /// <summary>
        /// DataRow → M03_BAIKA_Searchへの変換をおこなう
        /// </summary>
        /// <param name="rw">DataRow</param>
        /// <returns></returns>
        private M03_BAIKA_Search getRowData(DataRow rw)
        {
            M03_BAIKA_Search data = new M03_BAIKA_Search();

            // REMARKS:必要分のみ設定
            data.仕入先コード = int.Parse(rw["仕入先コード"].ToString());
            data.仕入先コード枝番 = int.Parse(rw["仕入先コード枝番"].ToString());
            data.品番コード = int.Parse(rw["品番コード"].ToString());
            data.単価 = decimal.Parse(rw["単価"].ToString());

            return data;

        }

    }

}
