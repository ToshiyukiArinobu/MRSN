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
    public class MST02011
    {
      
       
        /// <summary>
        /// MST02011データメンバー
        /// </summary>
        /// [DataContract]
        public class MST02011_spread
        {
            public int? 品番コード { get; set; }
            public string 自社品番 { get; set; }
            public string 色 { get; set; }
            public string 自社品名 { get; set; }
            public decimal? 原価 { get; set; }
            public decimal? 加工原価 { get; set; }
            public decimal? 卸値 { get; set; }
            public decimal? 売価 { get; set; }
            public decimal? 掛率 { get; set; }
        }

        /// <summary>
        /// 品番データを取得する
        /// </summary>
        /// <returns></returns>
        public List<M09_HIN> GetData(string p自社品名, int p商品分類, int p商品形態)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

               
                var result = (
                    from m09 in context.M09_HIN
                    from m06 in context.M06_IRO.Where(w => (m09.自社色.Equals(w.色コード))).DefaultIfEmpty()
                    where (m09.商品分類 == p商品分類 || p商品分類 == 0)
                    && (m09.商品形態分類 == p商品形態 || p商品形態 == 0)
                    //select new MST02011_spread
                    //{
                    //    品番コード = m09.品番コード,
                    //    自社品番 = m09.自社品番,
                    //    色 = m06.色名称,
                    //    自社品名 = m09.自社品名,
                    //    原価 = m09.原価,
                    //    加工原価 = m09.加工原価,
                    //    卸値 = m09.卸値,
                    //    売価 = m09.売価,
                    //    掛率 = m09.掛率,

                    //}
                    select m09
                    ).AsQueryable();

                if (!string.IsNullOrEmpty(p自社品名))
                {
                    result = result.Where(c => c.自社品名.Contains(p自社品名));
                }
                return result.ToList();
            }
        }

        /// <summary>
        /// 商品群データの登録・更新をおこなう
        /// </summary>
        /// <param name="updData"></param>
        /// <param name="pLoginUserCode"></param>
        /// <returns></returns>
        public int Update(DataSet ds, int pLoginUserCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try
                {
                    context.Connection.Open();

                    DataTable dt = ds.Tables["MST02011_GetData"];

                    foreach (DataRow row in dt.Rows)
                    {
                        // 変更なしデータは処理対象外とする
                        if (row.RowState == DataRowState.Unchanged)
                            continue;



                        // 対象データ取得
                        var data =
                            context.M09_HIN
                                .Where(w => w.品番コード == (int)row["品番コード"])
                                .FirstOrDefault();

                        if (data != null)
                        {
                            data.自社品番 = (string)row["自社品番"];
                            data.自社色 = (string)row["自社色"];
                            data.商品形態分類 = (int?)row["商品形態分類"];
                            data.商品分類 = (int?)row["商品分類"];
                            data.大分類 = (int?)row["大分類"];
                            data.中分類 = (int?)row["中分類"];
                            data.ブランド = (string)row["ブランド"];
                            data.シリーズ = (string)row["シリーズ"];
                            data.品群 = (string)row["品群"];
                            data.自社品名 = (string)row["自社品名"];
                            data.単位 = (string)row["単位"];
                            data.原価 = decimal.Parse(row["原価"].ToString());
                            data.加工原価 = decimal.Parse(row["加工原価"].ToString());
                            data.卸値 = decimal.Parse(row["卸値"].ToString());
                            data.売価 = decimal.Parse(row["売価"].ToString());
                            data.掛率 = decimal.Parse(row["掛率"].ToString());
                            data.消費税区分 = (int?)row["消費税区分"];
                            data.備考１ = (string)row["備考１"];
                            data.備考２ = (string)row["備考２"];
                            data.返却可能期限 = (int?)row["返却可能期限"];
                            data.ＪＡＮコード = (string)row["ＪＡＮコード"];



                            data.最終更新者 = pLoginUserCode;
                            data.最終更新日時 = DateTime.Now;

                            data.AcceptChanges();
                        }

                    }

                    context.SaveChanges();

                }
                catch (Exception ex)
                {
                    return 0;
                }

            }

            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rw">DataRow</param>
        /// <returns></returns>
        private M09_HIN getRowData(DataRow rw)
        {
            M09_HIN data = new M09_HIN();

            
            data.品番コード = int.Parse(rw["品番コード"].ToString());
            data.原価 = decimal.Parse(rw["原価"].ToString());
            data.加工原価 = decimal.Parse(rw["加工原価"].ToString());
            data.卸値 = decimal.Parse(rw["卸値"].ToString());
            data.売価 = decimal.Parse(rw["売価"].ToString());
            data.掛率 = decimal.Parse(rw["掛率"].ToString());
            return data;

        }

    }

}
