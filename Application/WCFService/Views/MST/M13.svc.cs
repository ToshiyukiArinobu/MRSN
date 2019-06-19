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
    /// 商品中分類マスタサービスクラス
    /// </summary>
    public class M13
    {
        Common com = new Common();

        public class SEARCH_M13
        {
            [DataMember]
            public int 大分類コード { get; set; }
            [DataMember]
            public string 大分類名 { get; set; }
            [DataMember]
            public int 中分類コード { get; set; }
            [DataMember]
            public string 中分類名 { get; set; }
        }

        #region 検索画面LOAD時

        /// <summary>
        /// LOAD時にセットするデータ
        /// </summary>
        /// <param name="majorClassCode">大分類コード</param>
        /// <returns></returns>
        public List<SEARCH_M13> SEARCH_GetData(int majorClassCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query =
                    context.M13_TYUBUNRUI
                        .Where(w => w.削除日時 == null && w.大分類コード == majorClassCode)
                        .Join(context.M12_DAIBUNRUI,
                            x => x.大分類コード,
                            y => y.大分類コード,
                            (x, y) => new { x, 大分類名称 = y.大分類名 })
                        .Select(z => new SEARCH_M13
                        {
                            大分類コード = z.x.大分類コード,
                            大分類名 = z.大分類名称,
                            中分類コード = z.x.中分類コード,
                            中分類名 = z.x.中分類名
                        })
                        .OrderBy(o => o.大分類コード)
                        .ThenBy(t => t.中分類コード)
                        .AsQueryable();

                return query.ToList();

            }

        }

        #endregion

        #region << 商品中分類マスタ保守 >>

        #region 中分類検索データ取得
        /// <summary>
        /// 商品中分類情報検索
        /// </summary>
        /// <param name="majCode">大分類コード</param>
        /// <param name="tyuCode">中分類コード</param>
        /// <param name="pagingNum">ページングオプション</param>
        /// <returns></returns>
        public List<M13_TYUBUNRUI> MST04010_SearchData(string majCode, string tyuCode, int pagingNum)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                CommonConstants.PagingOption option = (CommonConstants.PagingOption)pagingNum;
                int ival,
                    iMajCode = int.Parse(majCode);
                int? iTyuCode = int.TryParse(tyuCode, out ival) ? ival : (int?)null;

                #region 検索対象コード取得
                int searchCode = 0;
                var baseSearch = context.M13_TYUBUNRUI.Where(w => w.大分類コード == iMajCode);
                switch (option)
                {
                    case CommonConstants.PagingOption.Paging_Top:
                        searchCode = baseSearch.Where(w => w.削除日時 == null).Min(m => m.中分類コード);
                        break;

                    case CommonConstants.PagingOption.Paging_Before:
                        if (iTyuCode == null)
                            searchCode = baseSearch.Where(w => w.削除日時 == null).Min(m => m.中分類コード);
                        else
                        {
                            var wkResult = baseSearch.Where(w => w.削除日時 == null && w.中分類コード < iTyuCode);
                            if (wkResult.Count() == 0)
                                return MST04010_SearchData(majCode, tyuCode, (int)CommonConstants.PagingOption.Paging_Top);
                            else
                                searchCode = wkResult.Max(m => m.中分類コード);
                        }
                        break;

                    case CommonConstants.PagingOption.Paging_Code:
                        if (iTyuCode == null)
                            searchCode = -1;
                        else
                            searchCode = (int)iTyuCode;
                        break;

                    case CommonConstants.PagingOption.Paging_After:
                        if (iTyuCode == null)
                            searchCode = baseSearch.Where(w => w.削除日時 == null).Min(m => m.中分類コード);
                        else
                        {
                            var wkResult = baseSearch.Where(w => w.削除日時 == null && w.中分類コード > iTyuCode);
                            if (wkResult.Count() == 0)
                                return MST04010_SearchData(majCode, tyuCode, (int)CommonConstants.PagingOption.Paging_End);
                            else
                                searchCode = wkResult.Min(m => m.中分類コード);
                        }
                        break;

                    case CommonConstants.PagingOption.Paging_End:
                        searchCode = baseSearch.Where(w => w.削除日時 == null).Max(m => m.中分類コード);
                        break;

                }
                #endregion

                var result =
                    context.M13_TYUBUNRUI
                        .Where(w => w.大分類コード == iMajCode && w.中分類コード == searchCode);

                return result.ToList();

            }

        }
        #endregion

        #region 中分類情報更新
        /// <summary>
        /// 商品中分類情報の更新をおこなう
        /// </summary>
        /// <param name="majCode">大分類コード</param>
        /// <param name="chuCode">中分類コード</param>
        /// <param name="chuName">中分類名称</param>
        /// <param name="userId">ログインユーザID</param>
        /// <returns></returns>
        public void MST04010_UpdateData(int majCode, int chuCode, string chuName, int userId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 登録状態確認
                var tyu =
                    context.M13_TYUBUNRUI
                        .Where(w =>
                            w.大分類コード == majCode &&
                            w.中分類コード == chuCode)
                        .FirstOrDefault();

                if (tyu == null)
                {
                    M13_TYUBUNRUI ent = new M13_TYUBUNRUI();

                    ent.大分類コード = majCode;
                    ent.中分類コード = chuCode;
                    ent.中分類名 = chuName;
                    ent.登録者 = userId;
                    ent.登録日時 = com.GetDbDateTime();
                    ent.最終更新者 = userId;
                    ent.最終更新日時 = com.GetDbDateTime();

                    context.M13_TYUBUNRUI.ApplyChanges(ent);

                }
                else
                {
                    tyu.中分類名 = chuName;
                    tyu.最終更新者 = userId;
                    tyu.最終更新日時 = com.GetDbDateTime();

                    tyu.AcceptChanges();

                }

                context.SaveChanges();

            }

        }
        #endregion

        #region 中分類情報(論理)削除
        /// <summary>
        /// 中分類情報の論理削除をおこなう
        /// </summary>
        /// <returns></returns>
        public int MST04010_DeleteData(int majCode, int chuCode, int userId)
        {
            int returnCode = -1;

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 登録状態確認
                var tyu =
                    context.M13_TYUBUNRUI
                        .Where(w =>
                            w.大分類コード == majCode &&
                            w.中分類コード == chuCode)
                        .FirstOrDefault();

                if (tyu == null)
                {
                    // 更新対象データなし
                }
                else
                {
                    tyu.削除者 = userId;
                    tyu.削除日時 = com.GetDbDateTime();

                    tyu.AcceptChanges();

                }

                returnCode = 0;
                context.SaveChanges();

            }

            return returnCode;

        }
        #endregion



        #endregion

    }

}
