using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 棚卸在庫サービスクラス
    /// </summary>
    public class S10 : BaseService
    {
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId">ログインユーザID</param>
        public S10(TRAC3Entities context, int userId)
        {
            _context = context;
            _loginUserId = userId;

        }
        #endregion

        #region 棚卸在庫情報の登録・更新
        /// <summary>
        /// 棚卸在庫情報の登録・更新をおこなう
        /// </summary>
        /// <param name="stokData"></param>
        public void S10_STOCKTAKING_Update(S10_STOCKTAKING StocktakingData)
        {
            var record =
                _context.S10_STOCKTAKING
                    .Where(x =>
                        x.棚卸日 == StocktakingData.棚卸日 &&
                        x.倉庫コード == StocktakingData.倉庫コード &&
                        x.品番コード == StocktakingData.品番コード &&
                        x.賞味期限 == StocktakingData.賞味期限)
                    .FirstOrDefault();

            if (record == null)
            {
                // データなしの為追加
                S10_STOCKTAKING Stocktaking = new S10_STOCKTAKING();

                Stocktaking.棚卸日 = StocktakingData.棚卸日;
                Stocktaking.倉庫コード = StocktakingData.倉庫コード;
                Stocktaking.品番コード = StocktakingData.品番コード;
                Stocktaking.賞味期限 = StocktakingData.賞味期限;
                Stocktaking.実在庫数 = StocktakingData.実在庫数;
                Stocktaking.品番追加FLG = StocktakingData.品番追加FLG;
                Stocktaking.更新済みFLG = StocktakingData.更新済みFLG;
                Stocktaking.登録者 = _loginUserId;
                Stocktaking.登録日時 = com.GetDbDateTime();
                Stocktaking.最終更新者 = _loginUserId;
                Stocktaking.最終更新日時 = com.GetDbDateTime();

                _context.S10_STOCKTAKING.ApplyChanges(Stocktaking);

            }
            else
            {
                // データを更新
                record.実在庫数 = StocktakingData.実在庫数;
                record.品番追加FLG = StocktakingData.品番追加FLG;
                record.更新済みFLG = StocktakingData.更新済みFLG;
                record.最終更新者 = _loginUserId;
                record.最終更新日時 = com.GetDbDateTime();
                record.削除者 = null;
                record.削除日時 = null;

                record.AcceptChanges();

            }

        }
        #endregion

    }

}