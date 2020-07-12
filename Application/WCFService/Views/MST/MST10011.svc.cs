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
    public class MST10011
    {
        #region メンバークラス定義

        public class MST10011_SHIN_Search
        {
            public string セット品番 { get; set; }
            public string 色 { get; set; }
            public string セット品名 { get; set; }
            public string 構成品番 { get; set; }
            public string 構成品色 { get; set; }
            public string 構成品名 { get; set; }
            public int 使用数量 { get; set; }
        }


        #endregion


        #region MST10011_SHIN_Searchメソッド群

        /// <summary>
        /// セット品番マスタを検索する
        /// </summary>
        /// <param name="CustomerCode">得意先コード</param>
        /// <param name="CustomerEda">得意先コード枝番</param>
        /// <returns></returns>
        public List<MST10011_SHIN_Search> GetData(string productCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var result = (from m09 in context.M09_HIN.Where(c => c.商品形態分類 == 1)
                              from shin in context.M10_SHIN.Where(c => c.品番コード == m09.品番コード)
                              from sm09 in context.M09_HIN.Where(c => c.品番コード == shin.構成品番コード)
                              where
                              ( m09.自社品番 == productCode || productCode == "" )
                              select new MST10011_SHIN_Search
                                   {
                                       セット品番 = m09.自社品番,
                                       色 = m09.自社色,
                                       セット品名 = m09.自社品名,
                                       構成品番 = sm09.自社品番,
                                       構成品色 = sm09.自社色,
                                       構成品名 = sm09.自社品名,
                                       使用数量 = shin.使用数量
                                   }).OrderBy(c => c.セット品番).ThenBy(c => c.色).ThenBy(c => c.構成品番);

                return result.ToList();

            }

        }

        /// <summary>
        /// セット品番マスタ登録処理
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
                DataTable updTbl = ds.Tables["MST10011_GetData"];


                Dictionary<string, List<DataRow>> dicSetProducts = new Dictionary<string, List<DataRow>>();
                string strKey = string.Empty;
                List<DataRow> hinList = new List<DataRow>();
                foreach (DataRow rw in updTbl.Rows)
                {
                    strKey = rw["セット品番"].ToString() + "_" + rw["色"].ToString();

                   if(dicSetProducts.Keys.Contains(strKey))
                    {
                        hinList.Add(rw);
                        dicSetProducts[strKey] = hinList;
                    }
                    else
                    {
                        hinList = new List<DataRow>();
                        hinList.Add(rw);
                        dicSetProducts.Add(strKey, hinList);
                    }
                }

                foreach (string key in dicSetProducts.Keys)
                {
                    List<DataRow> kouseihinList = dicSetProducts[key];
                    M09_HIN hinban = null;
                    for (int i = 0; i < kouseihinList.Count; i++)
                    {
                        DataRow rw = kouseihinList[i];
                        string strJishaHinban = rw["セット品番"].ToString();
                        string strIro = string.IsNullOrEmpty(rw["色"].ToString()) ? null : rw["色"].ToString();

                        if (i == 0)
                        {
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

                            var delData = context.M10_SHIN.Where(w => w.品番コード == hinban.品番コード).ToList();
                            if (delData != null)
                            {
                                foreach (M10_SHIN dtl in delData)
                                {
                                    context.M10_SHIN.DeleteObject(dtl);
                                }

                                context.SaveChanges();

                            }
                        }

                        string strKoseiHinban = rw["構成品番"].ToString();
                        string strKoseiIro = string.IsNullOrEmpty(rw["構成品色"].ToString()) ? null : rw["構成品色"].ToString();

                        M09_HIN koseihinban = null;
                        if (strKoseiIro == null)
                        {
                            koseihinban = context.M09_HIN.Where(c => c.自社品番 == strKoseiHinban
                                                                && c.自社色 == null).FirstOrDefault();
                        }
                        else
                        {
                            koseihinban = context.M09_HIN.Where(c => c.自社品番 == strKoseiHinban
                                                        && c.自社色 == strKoseiIro).FirstOrDefault();
                        }

                        if (koseihinban == null)
                        {
                            continue;
                        }

                        M10_SHIN addHin = new M10_SHIN();

                        addHin.品番コード = hinban.品番コード;
                        addHin.部品行 = i + 1;
                        addHin.構成品番コード = koseihinban.品番コード;
                        addHin.使用数量 = int.Parse(rw["使用数量"].ToString());
                        addHin.登録者 = loginUserId;
                        addHin.登録日時 = DateTime.Now;
                        addHin.最終更新者 = loginUserId;
                        addHin.最終更新日時 = DateTime.Now;

                        context.M10_SHIN.AddObject(addHin);
                    }
                }

                context.SaveChanges();

            }

        }

        #endregion

    }

}
