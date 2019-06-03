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
    public class M16
    {
        /// <summary>
        /// 商品群マスタ 表示項目定義クラス
        /// </summary>
        public class SEARCH_M16
        {
            [DataMember]
            public string 商品群コード { get; set; }
            [DataMember]
            public string 商品群名 { get; set; }
            [DataMember]
            public string かな読み { get; set; }
            [DataMember]
            public DateTime? 削除日時 { get; set; }
        }

        /// <summary>
        /// 商品群リスト 表示項目定義クラス
        /// </summary>
        public class SEARCH_LIST_M16
        {
            [DataMember]
            public string コードFROM { get; set; }
            [DataMember]
            public string コードTO { get; set; }
            [DataMember]
            public string[] 商品群名 { get; set; }
            [DataMember]
            public string 並び順 { get; set; }
        }

        /// <summary>
        /// 商品群データ取得
        /// </summary>
        /// <param name="p商品群コード"></param>
        /// <param name="pオプション">
        ///  null:全データリスト取得
        ///    -2:先頭データ取得
        ///    -1:前データ取得
        ///     0:コード指定
        ///     1:次データ取得
        ///     2:最終データ取得
        /// </param>
        /// <returns></returns>
        public List<SEARCH_M16> GetData(string p商品群コード, int? pオプション)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query =
                    context.M16_HINGUN
                        .Select(m16 => new SEARCH_M16
                        {
                            商品群コード = m16.品群コード,
                            商品群名 = m16.品群名,
                            かな読み = m16.かな読み,
                            削除日時 = m16.削除日時
                        })
                        .OrderBy(c => c.商品群コード)
                        .AsQueryable();

                if (pオプション != null)
                {
                    // データが1件もない状態で<< < > >>を押された時の処理
                    if (string.IsNullOrEmpty(p商品群コード) && query.Count() == 0)
                    {
                        return null;
                    }

                    if (pオプション == 0)
                    {
                        // コード指定
                        query = query.Where(c => c.商品群コード == p商品群コード);
                    }
                    else
                    {
                        #region オプション別データ抽出
                        switch (pオプション)
                        {
                            case -1:
                                // 前データ取得
                                query = query.Where(c => c.商品群コード == (context.M16_HINGUN
                                    .Where(w => w.削除日時 == null && w.品群コード.CompareTo(p商品群コード) < 0)
                                    .Max(s => s.品群コード)));
                                break;

                            case 1:
                                // 次データ取得
                                query = query.Where(c => c.商品群コード == (context.M16_HINGUN
                                    .Where(w => w.削除日時 == null && w.品群コード.CompareTo(p商品群コード) > 0)
                                    .Min(s => s.品群コード)));
                                break;

                            case -2:
                                // 先頭データ取得
                                query = query.Where(c => c.商品群コード == (context.M16_HINGUN
                                    .Where(w => w.削除日時 == null)
                                    .Min(s => s.品群コード)));
                                break;

                            case 2:
                                // 最終データ取得
                                query = query.Where(c => c.商品群コード == (context.M16_HINGUN
                                    .Where(w => w.削除日時 == null)
                                    .Max(s => s.品群コード)));
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
                List<SEARCH_M16> result = new List<SEARCH_M16>();
                if (ret != null)
                {
                    result.Add(ret);
                }

                return result;

            }

        }

        /// <summary>
        /// 商品群データの登録・更新をおこなう
        /// </summary>
        /// <param name="pUpdateData"></param>
        /// <param name="pLoginUserCode"></param>
        /// <returns></returns>
        public int Update(SEARCH_M16 pUpdateData, int pLoginUserCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 更新行を特定
                var m16 = context.M16_HINGUN
                    .Where(x => x.品群コード == pUpdateData.商品群コード)
                    .Select(c => c).FirstOrDefault();

                if (m16 == null)
                {
                    // 未登録時(追加)
                    M16_HINGUN m16hingun = new M16_HINGUN();

                    m16hingun.品群コード = pUpdateData.商品群コード;
                    m16hingun.品群名 = pUpdateData.商品群名;
                    m16hingun.かな読み = pUpdateData.かな読み;
                    m16hingun.登録日時 = DateTime.Now;
                    m16hingun.登録者 = pLoginUserCode;
                    m16hingun.最終更新日時 = DateTime.Now;
                    m16hingun.最終更新者 = pLoginUserCode;

                    context.M16_HINGUN.ApplyChanges(m16hingun);

                }
                else
                {
                    // 登録済(更新)
                    m16.品群名 = pUpdateData.商品群名;
                    m16.かな読み = pUpdateData.かな読み;
                    m16.最終更新日時 = DateTime.Now;
                    m16.最終更新者 = pLoginUserCode;
                    m16.削除日時 = null;
                    m16.削除者 = null;

                    m16.AcceptChanges();

                }

                context.SaveChanges();

            }
            
            return 1;

        }

        /// <summary>
        /// 商品群データの削除をおこなう
        /// </summary>
        /// <param name="hingunCode">対象の商品群コード</param>
        /// <param name="pLoginUserCode">操作担当者ID</param>
        public void Delete(string hingunCode, int pLoginUserCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 削除行を特定
                var m16 = context.M16_HINGUN
                    .Where(x => x.品群コード == hingunCode)
                    .Select(c => c).FirstOrDefault();

                if (m16 != null)
                {
                    m16.削除日時 = DateTime.Now;
                    m16.削除者 = pLoginUserCode;

                    m16.AcceptChanges();
                }

                context.SaveChanges();
            }

        }

        /// <summary>
        /// CSV出力データの取得をおこなう
        /// </summary>
        public List<M16_HINGUN> GetCsv(string codeFrom, string codeTo, string[] nameAry, string sortType)
        {
            SEARCH_LIST_M16 cond = new SEARCH_LIST_M16();
            cond.コードFROM = codeFrom;
            cond.コードTO = codeTo;
            cond.商品群名 = nameAry;
            cond.並び順 = sortType;

            return getHingunList(cond);

        }

        /// <summary>
        /// 帳票出力データの取得をおこなう
        /// </summary>
        /// <param name="codeFrom">コードFrom</param>
        /// <param name="codeTo">コードTo</param>
        /// <param name="nameAry">商品群名(配列)</param>
        /// <param name="sortType">表示方法(0:コード順、1:カナ読み順)</param>
        /// <returns></returns>
        public List<SEARCH_M16> GetRpt(string codeFrom, string codeTo, string[] nameAry, string sortType)
        {
            SEARCH_LIST_M16 cond = new SEARCH_LIST_M16();
            cond.コードFROM = codeFrom;
            cond.コードTO = codeTo;
            cond.商品群名 = nameAry;
            cond.並び順 = sortType;

            List<M16_HINGUN> list = getHingunList(cond);

            // 帳票出力用に項目を絞り込む
            var resultList = 
                list.Select(x => new SEARCH_M16
                    {
                        商品群コード = x.品群コード,
                        商品群名 = x.品群名,
                        かな読み = x.かな読み
                    });

            return resultList.ToList();

        }

        /// <summary>
        /// データ取得実処理部
        /// </summary>
        /// <returns></returns>
        private List<M16_HINGUN> getHingunList(SEARCH_LIST_M16 condition)
        {
            List<M16_HINGUN> list = new List<M16_HINGUN>();

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var m16 = 
                    context.M16_HINGUN
                        .Where(w => w.削除日時 == null)
                        .AsQueryable();

                if (!string.IsNullOrEmpty(condition.コードFROM))
                {
                    m16 = m16.Where(x => x.品群コード.CompareTo(condition.コードFROM) >= 0);
                }

                if (!string.IsNullOrEmpty(condition.コードTO))
                {
                    m16 = m16.Where(x => x.品群コード.CompareTo(condition.コードTO) <= 0);
                }

                if (condition.商品群名.Length > 0)
                {
                    m16 = m16.Where(w => condition.商品群名.Any(names => w.品群名.Contains(names)));
                }

                switch (condition.並び順)
                {
                    case "0":   // コード順
                        m16 = m16.OrderBy(o => o.品群コード);
                        break;

                    case "1":   // カナ読み順
                        m16 = m16.OrderBy(o => o.かな読み).ThenBy(o => o.品群コード);
                        break;

                    default:    // 選択肢なし
                        break;

                }

                // リスト取得
                list = m16.ToList();

            }

            return list;

        }

    }

}
