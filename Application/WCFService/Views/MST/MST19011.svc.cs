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
    public class MST19011
    {
        #region メンバークラス定義

        public class MST19011_M02BAIKA_Search
        {
            public string 自社品番 { get; set; }
            public string 色 { get; set; }
            public int 得意先コード { get; set; }
            public int 枝番 { get; set; }
            public string 得意先名 { get; set; }
            public decimal 単価 { get; set; }
        }


        #endregion


        #region MST19011_M02BAIKA_Searchメソッド群

        /// <summary>
        /// 得意先コードで得意先品番マスタを検索する
        /// </summary>
        /// <param name="CustomerCode">得意先コード</param>
        /// <param name="CustomerEda">得意先コード枝番</param>
        /// <returns></returns>
        public List<MST19011_M02BAIKA_Search> GetData(string productCode, string CustomerCode, string CustomerEda)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int code, eda;
                int.TryParse(CustomerCode, out code);
                int.TryParse(CustomerEda, out eda);

                var result = (from tok in context.M02_BAIKA
                              from m01 in context.M01_TOK.Where(m => m.取引先コード == tok.得意先コード && m.枝番 == tok.枝番)
                              from m09 in context.M09_HIN.Where(c => c.品番コード == tok.品番コード)
                              where
                              (tok.得意先コード == code || CustomerCode == "")
                              &&( tok.枝番 == eda || CustomerEda == "" )
                              &&( m09.自社品番 == productCode || productCode == "" )
                              select new MST19011_M02BAIKA_Search
                                   {
                                       得意先コード = tok.得意先コード,
                                       枝番 = tok.枝番,
                                       自社品番 = m09.自社品番,
                                       色 = m09.自社色,
                                       得意先名 = m01.得意先名１,
                                       単価 = tok.単価 == null ? 0 : (decimal)tok.単価,
                                   }).OrderBy(c => c.得意先コード).ThenBy(c=>c.枝番).ThenBy(c=>c.自社品番);

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
        public void Update(DataSet ds, int loginUserId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // データ登録・更新
                DataTable updTbl = ds.Tables["MST19011_GetData"];
                foreach (DataRow rw in updTbl.Rows)
                {
                    string strJishaHinban = rw["自社品番"].ToString();
                    string strIro = string.IsNullOrEmpty(rw["色"].ToString()) ? null : rw["色"].ToString();
                    M09_HIN hinban;

                    if (strIro == null)
                    {
                        hinban = context.M09_HIN.Where(c => c.自社品番 == strJishaHinban
                                                            && c.自社色 == null).FirstOrDefault();
                    }
                    else
                    {
                        hinban = context.M09_HIN.Where(c => c.自社品番 == strJishaHinban
                                                    && c.自社色 == strIro).FirstOrDefault();
                    }

                    if (hinban == null)
                    {
                        continue;
                    }

                    int i得意先コード = int.Parse(rw["得意先コード"].ToString());
                    int i枝番 = int.Parse(rw["枝番"].ToString());
 
                    // 対象データ取得
                    var data =
                        context.M02_BAIKA
                            .Where(w => w.得意先コード == i得意先コード &&
                                w.枝番 == i枝番 &&
                                w.品番コード == hinban.品番コード)
                            .FirstOrDefault();

                    if (data == null)
                    {
                        // 新規登録
                        M02_BAIKA m02baika = new M02_BAIKA();
                        m02baika.得意先コード = (int)rw["得意先コード"];
                        m02baika.枝番 = (int)rw["枝番"];
                        m02baika.品番コード = hinban.品番コード;
                        m02baika.単価 = decimal.Parse(rw["単価"].ToString());
                        m02baika.登録者 = loginUserId;
                        m02baika.登録日時 = DateTime.Now;
                        m02baika.最終更新者 = loginUserId;
                        m02baika.最終更新日時 = DateTime.Now;

                        context.M02_BAIKA.ApplyChanges(m02baika);

                    }
                    else
                    {
                        // データ更新
                        data.単価 = decimal.Parse(rw["単価"].ToString());
                        data.最終更新者 = loginUserId;
                        data.最終更新日時 = DateTime.Now;
                        data.削除者 = null;
                        data.削除日時 = null;

                        data.AcceptChanges();

                    }
                }
                context.SaveChanges();

            }

        }

        #endregion

    }

}
