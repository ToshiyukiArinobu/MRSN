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
    public class MST20011
    {
        #region メンバークラス定義

        public class MST20011_TOKHIN_Search
        {
            public string 自社品番 { get; set; }
            public string 色 { get; set; }
            public int 得意先コード { get; set; }
            public int 枝番 { get; set; }
            public string 得意先名 { get; set; }
            public string 得意先品番 { get; set; }
        }


        #endregion


        #region MST20011_TOKHIN_Searchメソッド群

        /// <summary>
        /// 得意先コードで得意先品番マスタを検索する
        /// </summary>
        /// <param name="CustomerCode">得意先コード</param>
        /// <param name="CustomerEda">得意先コード枝番</param>
        /// <returns></returns>
        public List<MST20011_TOKHIN_Search> GetData(string productCode, string CustomerCode, string CustomerEda)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int code, eda;
                int.TryParse(CustomerCode, out code);
                int.TryParse(CustomerEda, out eda);

                var result = (from tok in context.M10_TOKHIN
                              from m01 in context.M01_TOK.Where(m => m.取引先コード == tok.取引先コード && m.枝番 == tok.枝番)
                              from m09 in context.M09_HIN.Where(c => c.品番コード == tok.品番コード)
                              where
                              (tok.取引先コード == code || CustomerCode == "")
                              &&( tok.枝番 == eda || CustomerEda == "" )
                              &&( m09.自社品番 == productCode || productCode == "" )
                              select new MST20011_TOKHIN_Search
                                   {
                                       得意先コード = tok.取引先コード,
                                       枝番 = tok.枝番,
                                       自社品番 = m09.自社品番,
                                       色 = m09.自社色,
                                       得意先名 = m01.得意先名１,
                                       得意先品番 = tok.得意先品番コード,
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
        public void Update(DataSet ds, int loginUserId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // データ登録・更新
                DataTable updTbl = ds.Tables["MST20011_GetData"];
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
                        context.M10_TOKHIN
                            .Where(w => w.取引先コード == i得意先コード &&
                                w.枝番 == i枝番 &&
                                w.品番コード == hinban.品番コード)
                            .FirstOrDefault();

                    if (data == null)
                    {
                        // 新規登録
                        M10_TOKHIN tokhin = new M10_TOKHIN();
                        tokhin.取引先コード = i得意先コード;
                        tokhin.枝番 = i枝番;
                        tokhin.品番コード = hinban.品番コード;
                        tokhin.得意先品番コード = rw["得意先品番"].ToString();
                        tokhin.登録者 = loginUserId;
                        tokhin.登録日時 = DateTime.Now;
                        tokhin.最終更新者 = loginUserId;
                        tokhin.最終更新日時 = DateTime.Now;

                        context.M10_TOKHIN.ApplyChanges(tokhin);

                    }
                    else
                    {
                        // データ更新
                        data.得意先品番コード = rw["得意先品番"].ToString();
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
