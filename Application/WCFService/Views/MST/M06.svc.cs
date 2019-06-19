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
    public class M06 {

        /// <summary>
        /// 色マスタ 表示項目定義クラス
        /// </summary>
        public class SEARCH_M06
        {
            [DataMember]
            public string 色コード { get; set; }
            [DataMember]
            public string 色名称 { get; set; }
        }

        /// <summary>
        /// 色マスタリスト 表示項目定義クラス
        /// </summary>
        public class SEARCH_LIST_M06
        {
            [DataMember]
            public string コードFROM { get; set; }
            [DataMember]
            public string コードTO { get; set; }
            [DataMember]
            public string[] 色名 { get; set; }
        }

        /// <summary>
        /// 色マスタ検索データを取得する
        /// </summary>
        /// <param name="p色コード"></param>
        /// <param name="pオプション">
        ///   -2:先頭データ取得
        ///   -1:前データ取得
        ///    0:指定コード取得
        ///    1:次データ取得
        ///    2:最終データ取得
        /// </param>
        /// <returns></returns>
		public List<M06_IRO> GetData(string p色コード, int? pオプション)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

                var query = context.M06_IRO
                                .OrderBy(o => o.色コード)
                                .AsQueryable();

                if (pオプション != null)
                {
                    // データが1件もない状態で<< < > >>を押された時の処理
                    if (string.IsNullOrEmpty(p色コード) && query.Count() == 0)
                    {
                        return null;
                    }

                    if (pオプション == 0)
                    {
                        // コード指定
                        query = query.Where(c => c.色コード == p色コード);
                    }
                    else
                    {
                        #region オプション別データ抽出
                        switch (pオプション)
                        {
                            case -1:
                                // 前データ取得
                                query = query.Where(c =>
                                    c.色コード == (context.M06_IRO
                                                        .Where(w => w.削除日時 == null && w.色コード.CompareTo(p色コード) < 0)
                                                        .Max(s => s.色コード)
                                                  ));
                                break;

                            case 1:
                                // 次データ取得
                                query = query.Where(c =>
                                    c.色コード == (context.M06_IRO
                                                        .Where(w => w.削除日時 == null && w.色コード.CompareTo(p色コード) > 0)
                                                        .Min(s => s.色コード)
                                                  ));
                                break;

                            case -2:
                                // 先頭データ取得
                                query = query.Where(c =>
                                    c.色コード == (context.M06_IRO
                                                        .Where(w => w.削除日時 == null)
                                                        .Min(s => s.色コード)
                                                  ));
                                break;

                            case 2:
                                // 最終データ取得
                                query = query.Where(c =>
                                    c.色コード == (context.M06_IRO
                                                        .Where(w => w.削除日時 == null)
                                                        .Max(s => s.色コード)
                                                  ));
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
                    ret = context.M06_IRO
                                .Where(c => c.削除日時 == null && c.色コード == p色コード)
                                .FirstOrDefault();

                }

                return getSingleList(ret);

            }

        }

        /// <summary>
        /// 取得データ１行をリスト形式にして返す
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private List<M06_IRO> getSingleList(M06_IRO item)
        {
            List<M06_IRO> result = new List<M06_IRO>();
            if (item != null)
            {
                result.Add(item);
            }

            return result;

        }

        /// <summary>
        /// 色マスタのデータ更新をおこなう
        /// </summary>
        /// <param name="pUpdateData"></param>
        /// <param name="pLoginUserCode"></param>
        /// <returns></returns>
        public int Update(SEARCH_M06 pUpdateData, int pLoginUserCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 更新行を特定
                // REMARKS:削除済みコードが指定された場合に更新させる為、削除日時は参照しない
                var data = context.M06_IRO
                                .Where(w => w.色コード == pUpdateData.色コード)
                                .FirstOrDefault();

                if (data == null)
                {   // 登録
                    M06_IRO m06 = new M06_IRO();
                    m06.色コード = pUpdateData.色コード;
                    m06.色名称 = pUpdateData.色名称;
                    m06.登録者 = pLoginUserCode;
                    m06.登録日時 = DateTime.Now;
                    m06.最終更新者 = pLoginUserCode;
                    m06.最終更新日時 = DateTime.Now;

                    // 登録実行
                    context.M06_IRO.ApplyChanges(m06);

                }
                else
                {   // 更新または削除済データの登録時
                    data.色名称 = pUpdateData.色名称;
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
        /// 色マスタのデータ削除(論理)をおこなう
        /// </summary>
        /// <param name="p色コード"></param>
        /// <param name="pLoginUserCode"></param>
        public void Delete(string p色コード, int pLoginUserCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 削除行を特定
                var m06 = context.M06_IRO
                                .Where(w => w.色コード == p色コード)
                                .FirstOrDefault();

                if(m06 != null)
                {
                    m06.削除者 = pLoginUserCode;
                    m06.削除日時 = DateTime.Now;

                    // 削除更新実行
                    m06.AcceptChanges();

                }

                // データベースをコミット
                context.SaveChanges();

            }

        }

        /// <summary>
        /// 色マスタのCSV出力データ取得をおこなう
        /// </summary>
        /// <param name="codeFrom"></param>
        /// <param name="codeTo"></param>
        /// <param name="nameAry"></param>
        /// <returns></returns>
        public List<M06_IRO> GetCsv(string codeFrom, string codeTo, string[] nameAry)
        {
            // 条件指定データを生成
            SEARCH_LIST_M06 cond = getConditon(codeFrom, codeTo, nameAry);

            return getColorList(cond);

        }

        /// <summary>
        /// 色マスタの印刷データ取得をおこなう
        /// </summary>
        /// <param name="codeFrom"></param>
        /// <param name="codeTo"></param>
        /// <param name="nameAry"></param>
        /// <returns></returns>
        public List<M06_IRO> GetRpt(string codeFrom, string codeTo, string[] nameAry)
        {
            // 条件指定データを生成
            SEARCH_LIST_M06 cond = getConditon(codeFrom, codeTo, nameAry);

            return getColorList(cond);

        }

        /// <summary>
        /// 色マスタリスト出力条件データを作成する
        /// </summary>
        /// <param name="codeFrom"></param>
        /// <param name="codeTo"></param>
        /// <param name="nameAry"></param>
        /// <returns></returns>
        private SEARCH_LIST_M06 getConditon(string codeFrom, string codeTo, string[] nameAry)
        {
            SEARCH_LIST_M06 cond = new SEARCH_LIST_M06();
            cond.コードFROM = codeFrom;
            cond.コードTO = codeTo;
            cond.色名 = nameAry;

            return cond;

        }

        /// <summary>
        /// データ取得実処理部
        /// </summary>
        /// <returns></returns>
        private List<M06_IRO> getColorList(SEARCH_LIST_M06 condition)
        {
            List<M06_IRO> list = new List<M06_IRO>();

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var m16 = context.M06_IRO
                    .Where(w => w.削除日時 == null)
                    .Select(c => c).AsQueryable();

                if (!string.IsNullOrEmpty(condition.コードFROM))
                {
                    m16 = m16.Where(x => x.色コード.CompareTo(condition.コードFROM) >= 0);
                }

                if (!string.IsNullOrEmpty(condition.コードTO))
                {
                    m16 = m16.Where(x => x.色コード.CompareTo(condition.コードTO) <= 0);
                }

                if (condition.色名.Length > 0)
                {
                    m16 = m16.Where(w => condition.色名.Any(names => w.色名称.Contains(names)));
                }

                // リスト取得
                list = m16.ToList();

            }

            return list;

        }

    }

}
