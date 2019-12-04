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
    public class M70 {

        /// <summary>
        /// 自社マスタ検索データを取得する
        /// </summary>
        /// <param name="p自社ID"></param>
        /// <param name="pオプション">
        ///   -2:先頭データ取得
        ///   -1:前データ取得
        ///    0:指定コード取得
        ///    1:次データ取得
        ///    2:最終データ取得
        /// </param>
        /// <returns></returns>
		public List<M70_JIS> GetData(string p自社ID, int? pオプション)
		{
            // パラメータの型変換
            int iCompany = convertCompanyCode(p自社ID);

			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

                var query = context.M70_JIS
                                .OrderBy(o => o.自社コード)
                                .AsQueryable();

                if (pオプション != null)
                {
                    // データが1件もない状態で<< < > >>を押された時の処理
                    if (string.IsNullOrEmpty(p自社ID) && query.Count() == 0)
                        return null;

                    if (pオプション == 0)
                    {
                        // コード指定
                        query = query.Where(c => c.自社コード == iCompany);

                    }
                    else
                    {
                        #region オプション別データ抽出
                        switch (pオプション)
                        {
                            case -1:
                                // 前データ取得
                                query = query.Where(c =>
                                    c.自社コード == context.M70_JIS
                                                        .Where(w => w.削除日時 == null && w.自社コード.CompareTo(iCompany) < 0)
                                                        .Max(s => s.自社コード)
                                );
                                break;

                            case 1:
                                // 次データ取得
                                query = query.Where(c =>
                                    c.自社コード == context.M70_JIS
                                                        .Where(w => w.削除日時 == null && w.自社コード.CompareTo(iCompany) > 0)
                                                        .Min(s => s.自社コード)

                                );
                                break;

                            case -2:
                                // 先頭データ取得
                                query = query.Where(c =>
                                    c.自社コード == context.M70_JIS
                                                        .Where(w => w.削除日時 == null)
                                                        .Min(s => s.自社コード)
                                );
                                break;

                            case 2:
                                // 最終データ取得
                                query = query.Where(c =>
                                    c.自社コード == context.M70_JIS
                                                        .Where(w => w.削除日時 == null)
                                                        .Max(s => s.自社コード)
                                );
                                break;

                        }
                        #endregion

                    }

                }
                else
                {
                    // オプションがnullの場合はList取得とする。
                    return query.Where(w => w.削除日時 == null).ToList();

                }

                var ret = query.FirstOrDefault();

                if (ret == null)
                {
                    /*
                     * ページング動作で先頭・最終データが取得できなかった場合
                     * または削除済みコードを指定した場合なので対象コードでデータを取得する
                    */
                    ret = context.M70_JIS
                                .Where(c => c.削除日時 == null && c.自社コード == iCompany)
                                .FirstOrDefault();

                }

                return getSingleList(ret);

            }

        }

        /// <summary>
        /// 自社マスタより販社データをリストで取得する
        /// </summary>
        /// <returns></returns>
        public static List<M70_JIS> GetHanList()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var result =
                    context.M70_JIS
                        .Where(w => w.削除日時 == null && w.自社区分 == (int)CommonConstants.自社区分.販社)
                        .OrderBy(o => o.自社コード);

                return result.ToList();

            }

        }

        /// <summary>
        /// 取得データ１行をリスト形式にして返す
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private List<M70_JIS> getSingleList(M70_JIS item)
        {
            List<M70_JIS> result = new List<M70_JIS>();
            if (item != null)
                result.Add(item);

            return result;

        }

        /// <summary>
        /// 自社マスタのデータ更新をおこなう
        /// </summary>
        /// <param name="pUpdateData"></param>
        /// <param name="pLoginUserCode"></param>
        /// <returns></returns>
        public int Update(M70_JIS pUpdateData, int pLoginUserCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 更新行を特定
                // REMARKS:削除済みコードが指定された場合に更新させる為、削除日時は参照しない
                var data = context.M70_JIS
                                .Where(w => w.自社コード == pUpdateData.自社コード)
                                .FirstOrDefault();

                if (data == null)
                {   // 登録
                    M70_JIS m70 = new M70_JIS();

                    int? code = pUpdateData.自社コード;
                    m70.自社コード = code.Value;
                    m70.自社名 = pUpdateData.自社名;
                    m70.代表者名 = pUpdateData.代表者名;
                    m70.郵便番号 = pUpdateData.郵便番号;
                    m70.住所１ = pUpdateData.住所１;
                    m70.住所２ = pUpdateData.住所２;
                    m70.電話番号 = pUpdateData.電話番号;
                    m70.ＦＡＸ = pUpdateData.ＦＡＸ;
                    m70.振込銀行１ = pUpdateData.振込銀行１;
                    m70.振込銀行２ = pUpdateData.振込銀行２;
                    m70.振込銀行３ = pUpdateData.振込銀行３;
                    m70.法人ナンバー = pUpdateData.法人ナンバー;
                    m70.自社区分 = pUpdateData.自社区分;
                    m70.取引先コード = pUpdateData.取引先コード;
                    m70.枝番 = pUpdateData.枝番;
                    m70.ロゴ画像 = pUpdateData.ロゴ画像;
                    m70.決算月 = pUpdateData.決算月;
                    m70.登録者 = pLoginUserCode;
                    m70.登録日時 = DateTime.Now;
                    m70.最終更新者 = pLoginUserCode;
                    m70.最終更新日時 = DateTime.Now;

                    // 登録実行
                    context.M70_JIS.ApplyChanges(m70);

                }
                else
                {   // 更新または削除済データの登録時
                    data.自社名 = pUpdateData.自社名;
                    data.代表者名 = pUpdateData.代表者名;
                    data.郵便番号 = pUpdateData.郵便番号;
                    data.住所１ = pUpdateData.住所１;
                    data.住所２ = pUpdateData.住所２;
                    data.電話番号 = pUpdateData.電話番号;
                    data.ＦＡＸ = pUpdateData.ＦＡＸ;
                    data.振込銀行１ = pUpdateData.振込銀行１;
                    data.振込銀行２ = pUpdateData.振込銀行２;
                    data.振込銀行３ = pUpdateData.振込銀行３;
                    data.法人ナンバー = pUpdateData.法人ナンバー;
                    data.自社区分 = pUpdateData.自社区分;
                    data.取引先コード = pUpdateData.取引先コード;
                    data.枝番 = pUpdateData.枝番;
                    data.ロゴ画像 = pUpdateData.ロゴ画像;
                    data.決算月 = pUpdateData.決算月;
                    data.最終更新者 = pLoginUserCode;
                    data.最終更新日時 = DateTime.Now;
                    data.削除者 = null;
                    data.削除日時 = null;

                    // 更新実行
                    data.AcceptChanges();

                }

                // データベースのコミット
                context.SaveChanges();

            }

            return 1;

        }

        /// <summary>
        /// 自社マスタのデータ削除(論理)をおこなう
        /// </summary>
        /// <param name="p自社ID"></param>
        /// <param name="pLoginUserCode"></param>
        public void Delete(string p自社ID, int pLoginUserCode)
        {
            // パラメータの型変換
            int? ret = convertCompanyCode(p自社ID);
            if (ret == null)
                return;

            int iCompany = ret.Value;
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 削除行を特定
                var m70 = context.M70_JIS
                                .Where(w => w.自社コード == iCompany)
                                .FirstOrDefault();

                if(m70 != null)
                {
                    m70.削除者 = pLoginUserCode;
                    m70.削除日時 = DateTime.Now;

                    // 削除更新実行
                    m70.AcceptChanges();

                }

                // データベースをコミット
                context.SaveChanges();

            }

        }

        /// <summary>
        /// 自社IDを文字列型から数値型に変換する
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        private int convertCompanyCode(string companyCode)
        {
            // パラメータの型変換
            int iCompany = 0;

            // REMARKS:データリスト取得用にnullはチェック対象外とする
            if (companyCode != null && !int.TryParse(companyCode, out iCompany))
            {
                // 変換に失敗する場合は最小値を返す
                return int.MinValue;
            }

            return iCompany;

        }

        /// <summary>
        /// M70_JISのデータ取得
        /// </summary>
        /// <param name="p自社ID">自社ID</param>
        /// <returns>M70_JIS_Member</returns>
        public List<M70_JIS> GetImageData()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = context.M70_JIS.Where(x => x.自社コード == 1);

                //var query = (from m70 in context.M70_JIS
                //             where m70.自社ID == 1
                //             select new M70_JIS_Member
                //             {
                //                 自社ID = m70.自社ID,
                //                 登録日時 = m70.登録日時,
                //                 更新日時 = m70.更新日時,
                //                 自社名 = m70.自社名,
                //                 代表者名 = m70.代表者名,
                //                 郵便番号 = m70.郵便番号,
                //                 住所１ = m70.住所１,
                //                 住所２ = m70.住所２,
                //                 電話番号 = m70.電話番号,
                //                 ＦＡＸ = m70.ＦＡＸ,
                //                 振込銀行１ = m70.振込銀行１,
                //                 振込銀行２ = m70.振込銀行２,
                //                 振込銀行３ = m70.振込銀行３,
                //                 法人ナンバー = m70.法人ナンバー,
                //                 削除日付 = m70.削除日付,
                //                 画像 = m70.ロゴ画像,
                //             }).AsQueryable();

                return query.ToList();

            }

        }

    }

}
