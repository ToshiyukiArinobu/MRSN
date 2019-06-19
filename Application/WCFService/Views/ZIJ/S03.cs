using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 在庫サービスクラス
    /// </summary>
    public class S03 : BaseService
    {
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId">ログインユーザID</param>
        public S03(TRAC3Entities context, int userId)
        {
            _context = context;
            _loginUserId = userId;

        }
        #endregion

        #region 在庫情報の登録・更新
        /// <summary>
        /// 在庫情報の登録・更新をおこなう
        /// </summary>
        /// <param name="stokData"></param>
        public void S03_STOK_Update(S03_STOK stokData)
        {
            var record =
                _context.S03_STOK
                    .Where(x =>
                        x.倉庫コード == stokData.倉庫コード &&
                        x.品番コード == stokData.品番コード &&
                        x.賞味期限 == stokData.賞味期限)
                    .FirstOrDefault();

            if (record == null)
            {
                // データなしの為追加
                S03_STOK stok = new S03_STOK();

                stok.倉庫コード = stokData.倉庫コード;
                stok.品番コード = stokData.品番コード;
                stok.賞味期限 = stokData.賞味期限;
                stok.在庫数 = stokData.在庫数;
                stok.登録者 = _loginUserId;
                stok.登録日時 = com.GetDbDateTime();
                stok.最終更新者 = _loginUserId;
                stok.最終更新日時 = com.GetDbDateTime();

                _context.S03_STOK.ApplyChanges(stok);

            }
            else
            {
                // データを更新
                record.在庫数 = record.在庫数 + stokData.在庫数;
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