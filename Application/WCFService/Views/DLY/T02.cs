using System.Collections.Generic;
using System.Linq;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 売上ヘッダ/明細 サービスクラス
    /// </summary>
    public class T02 : BaseService
    {
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId"></param>
        public T02(TRAC3Entities context, int userId)
        {
            _context = context;
            _loginUserId = userId;
        }
        #endregion

        #region << 売上ヘッダ関連 >>

        #region 売上ヘッダ登録・更新
        /// <summary>
        /// 売上ヘッダの登録・更新をおこなう
        /// </summary>
        /// <param name="t02Data"></param>
        public void T02_URHD_Update(T02_URHD t02Data)
        {
            // 登録データ取得
            var hdData =
                _context.T02_URHD
                    .Where(w => w.伝票番号 == t02Data.伝票番号)
                    .FirstOrDefault();

            if (hdData == null)
            {
                // データなしの為追加
                T02_URHD urhd = new T02_URHD();

                urhd.伝票番号 = t02Data.伝票番号;
                urhd.会社名コード = t02Data.会社名コード;
                urhd.伝票要否 = t02Data.伝票要否;
                urhd.売上日 = t02Data.売上日;
                urhd.売上区分 = t02Data.売上区分;
                urhd.得意先コード = t02Data.得意先コード;
                urhd.得意先枝番 = t02Data.得意先枝番;
                urhd.在庫倉庫コード = t02Data.在庫倉庫コード;
                urhd.納品伝票番号 = t02Data.納品伝票番号;
                urhd.出荷日 = t02Data.出荷日;
                urhd.受注番号 = t02Data.受注番号;
                urhd.出荷元コード = t02Data.出荷元コード;
                urhd.出荷元枝番 = t02Data.出荷元枝番;
                urhd.出荷元名 = t02Data.出荷元名;
                urhd.出荷先コード = t02Data.出荷先コード;
                urhd.出荷先枝番 = t02Data.出荷先枝番;
                urhd.出荷先名 = t02Data.出荷先名;
                urhd.仕入先コード = t02Data.仕入先コード;
                urhd.仕入先枝番 = t02Data.仕入先枝番;
                urhd.備考 = t02Data.備考;
                urhd.元伝票番号 = t02Data.元伝票番号;
                // No-94 Add Start
                urhd.通常税率対象金額 = t02Data.通常税率対象金額;
                urhd.軽減税率対象金額 = t02Data.軽減税率対象金額;
                urhd.通常税率消費税 = t02Data.通常税率消費税;
                urhd.軽減税率消費税 = t02Data.軽減税率消費税;
                // No-94 Add End
                // No-95 Add Start
                urhd.小計 = t02Data.小計;
                urhd.総合計 = t02Data.総合計;
                // No-95 Add End
                urhd.消費税 = t02Data.消費税;
                urhd.登録者 = _loginUserId;
                urhd.登録日時 = com.GetDbDateTime();
                urhd.最終更新者 = _loginUserId;
                urhd.最終更新日時 = com.GetDbDateTime();

                _context.T02_URHD.ApplyChanges(urhd);

            }
            else
            {
                // データを更新
                hdData.会社名コード = t02Data.会社名コード;
                hdData.伝票要否 = t02Data.伝票要否;
                hdData.売上日 = t02Data.売上日;
                hdData.売上区分 = t02Data.売上区分;
                hdData.得意先コード = t02Data.得意先コード;
                hdData.得意先枝番 = t02Data.得意先枝番;
                hdData.在庫倉庫コード = t02Data.在庫倉庫コード;
                hdData.納品伝票番号 = t02Data.納品伝票番号;
                hdData.出荷日 = t02Data.出荷日;
                hdData.受注番号 = t02Data.受注番号;
                hdData.出荷元コード = t02Data.出荷元コード;
                hdData.出荷元枝番 = t02Data.出荷元枝番;
                hdData.出荷元名 = t02Data.出荷元名;
                hdData.出荷先コード = t02Data.出荷先コード;
                hdData.出荷先枝番 = t02Data.出荷先枝番;
                hdData.出荷先名 = t02Data.出荷先名;
                hdData.仕入先コード = t02Data.仕入先コード;
                hdData.仕入先枝番 = t02Data.仕入先枝番;
                hdData.備考 = t02Data.備考;
                hdData.元伝票番号 = t02Data.元伝票番号;
                // No-94 Add Start
                hdData.通常税率対象金額 = t02Data.通常税率対象金額;
                hdData.軽減税率対象金額 = t02Data.軽減税率対象金額;
                hdData.通常税率消費税 = t02Data.通常税率消費税;
                hdData.軽減税率消費税 = t02Data.軽減税率消費税;
                // No-94 Add End
                // No-95 Add Start
                hdData.小計 = t02Data.小計;
                hdData.総合計 = t02Data.総合計;
                // No-95 Add End
                hdData.消費税 = t02Data.消費税;
                hdData.最終更新者 = _loginUserId;
                hdData.最終更新日時 = com.GetDbDateTime();
                hdData.削除者 = null;
                hdData.削除日時 = null;

                hdData.AcceptChanges();

            }

        }
        #endregion

        #region 売上ヘッダ論理削除
        /// <summary>
        /// 売上ヘッダの論理削除をおこなう
        /// </summary>
        /// <param name="number">対象伝票番号</param>
        public T02_URHD T02_URHD_Delete(int number)
        {
            // 対象データ取得
            var hdResult =
                _context.T02_URHD
                    .Where(w => w.伝票番号 == number)
                    .FirstOrDefault();

            if (hdResult != null)
            {
                hdResult.削除者 = _loginUserId;
                hdResult.削除日時 = com.GetDbDateTime();

                hdResult.AcceptChanges();

            }

            return hdResult;

        }
        #endregion

        #endregion


        #region << 売上明細関連 >>

        #region 指定伝票の売上明細削除
        /// <summary>
        /// 売上明細から指定伝票番号の明細データを物理削除する
        /// </summary>
        /// <param name="denNumber"></param>
        public void T02_URDTL_DeleteRecords(int denNumber)
        {
            var delData = _context.T02_URDTL.Where(w => w.伝票番号 == denNumber).ToList();
            if (delData != null)
            {
                foreach (T02_URDTL dtl in delData)
                    _context.T02_URDTL.DeleteObject(dtl);

                _context.SaveChanges();

            }

        }
        #endregion

        #region 売上明細登録更新
        /// <summary>
        /// 売上明細の登録・更新をおこなう
        /// </summary>
        /// <param name="udtlData"></param>
        public void T02_URDTL_Update(T02_URDTL udtlData)
        {
            var dtlData =
                _context.T02_URDTL
                    .Where(w => w.伝票番号 == udtlData.伝票番号 && w.行番号 == udtlData.行番号)
                    .FirstOrDefault();

            if (dtlData == null)
            {
                // データなしの為追加
                T02_URDTL urdtl = new T02_URDTL();

                urdtl.伝票番号 = udtlData.伝票番号;
                urdtl.行番号 = udtlData.行番号;
                urdtl.品番コード = udtlData.品番コード;
                urdtl.賞味期限 = udtlData.賞味期限;
                urdtl.数量 = udtlData.数量;
                urdtl.単位 = udtlData.単位;
                urdtl.単価 = udtlData.単価;
                urdtl.金額 = udtlData.金額;
                urdtl.摘要 = udtlData.摘要;
                urdtl.登録者 = _loginUserId;
                urdtl.登録日時 = com.GetDbDateTime();
                urdtl.最終更新者 = _loginUserId;
                urdtl.最終更新日時 = com.GetDbDateTime();

                _context.T02_URDTL.ApplyChanges(urdtl);

            }
            else
            {
                // データを更新
                dtlData.品番コード = udtlData.品番コード;
                dtlData.賞味期限 = udtlData.賞味期限;
                dtlData.数量 = udtlData.数量;
                dtlData.単位 = udtlData.単位;
                dtlData.単価 = udtlData.単価;
                dtlData.金額 = udtlData.金額;
                dtlData.摘要 = udtlData.摘要;
                dtlData.最終更新者 = _loginUserId;
                dtlData.最終更新日時 = com.GetDbDateTime();
                dtlData.削除者 = null;
                dtlData.削除日時 = null;

                dtlData.AcceptChanges();

            }


        }
        #endregion

        #region 売上明細論理削除
        /// <summary>
        /// 売上明細情報の論理削除をおこなう
        /// </summary>
        /// <param name="number">対象伝票番号</param>
        /// <returns>対象の仕入明細リスト</returns>
        public List<T02_URDTL> T02_URDTL_Delete(int number)
        {
            var dtlResult =
                _context.T02_URDTL
                    .Where(w => w.伝票番号 == number)
                    .ToList();

            if (dtlResult != null)
            {
                foreach (T02_URDTL srdtl in dtlResult)
                {
                    srdtl.削除者 = _loginUserId;
                    srdtl.削除日時 = com.GetDbDateTime();

                    srdtl.AcceptChanges();

                }

            }

            return dtlResult;

        }
        #endregion

        #endregion


        #region << 販社売上ヘッダ関連 >>

        #region 売上ヘッダ登録・更新
        /// <summary>
        /// 売上ヘッダの登録・更新をおこなう
        /// </summary>
        /// <param name="t02Data"></param>
        public void T02_URHD_HAN_Update(T02_URHD_HAN t02Data)
        {
            // 登録データ取得
            var hdData =
                _context.T02_URHD_HAN
                    .Where(w => w.伝票番号 == t02Data.伝票番号)
                    .FirstOrDefault();

            if (hdData == null)
            {
                // データなしの為追加
                T02_URHD_HAN urhd = new T02_URHD_HAN();

                urhd.伝票番号 = t02Data.伝票番号;
                urhd.会社名コード = t02Data.会社名コード;
                urhd.伝票要否 = t02Data.伝票要否;
                urhd.売上日 = t02Data.売上日;
                urhd.売上区分 = t02Data.売上区分;
                urhd.販社コード = t02Data.販社コード;
                urhd.在庫倉庫コード = t02Data.在庫倉庫コード;
                urhd.納品伝票番号 = t02Data.納品伝票番号;
                urhd.出荷日 = t02Data.出荷日;
                urhd.受注番号 = t02Data.受注番号;
                urhd.出荷元コード = t02Data.出荷元コード;
                urhd.出荷先コード = t02Data.出荷先コード;
                urhd.仕入先コード = t02Data.仕入先コード;
                urhd.仕入先枝番 = t02Data.仕入先枝番;
                urhd.備考 = t02Data.備考;
                // No-94 Add Start
                urhd.通常税率対象金額 = t02Data.通常税率対象金額;
                urhd.軽減税率対象金額 = t02Data.軽減税率対象金額;
                urhd.通常税率消費税 = t02Data.通常税率消費税;
                urhd.軽減税率消費税 = t02Data.軽減税率消費税;
                // No-94 Add End
                // No-95 Add Start
                urhd.小計 = t02Data.小計;
                urhd.総合計 = t02Data.総合計;
                // No-95 Add End
                urhd.消費税 = t02Data.消費税;
                urhd.登録者 = _loginUserId;
                urhd.登録日時 = com.GetDbDateTime();
                urhd.最終更新者 = _loginUserId;
                urhd.最終更新日時 = com.GetDbDateTime();

                _context.T02_URHD_HAN.ApplyChanges(urhd);

            }
            else
            {
                // データを更新
                hdData.会社名コード = t02Data.会社名コード;
                hdData.伝票要否 = t02Data.伝票要否;
                hdData.売上日 = t02Data.売上日;
                hdData.売上区分 = t02Data.売上区分;
                hdData.販社コード = t02Data.販社コード;
                hdData.在庫倉庫コード = t02Data.在庫倉庫コード;
                hdData.納品伝票番号 = t02Data.納品伝票番号;
                hdData.出荷日 = t02Data.出荷日;
                hdData.受注番号 = t02Data.受注番号;
                hdData.出荷元コード = t02Data.出荷元コード;
                hdData.出荷先コード = t02Data.出荷先コード;
                hdData.仕入先コード = t02Data.仕入先コード;
                hdData.仕入先枝番 = t02Data.仕入先枝番;
                hdData.備考 = t02Data.備考;
                // No-94 Add Start
                hdData.通常税率対象金額 = t02Data.通常税率対象金額;
                hdData.軽減税率対象金額 = t02Data.軽減税率対象金額;
                hdData.通常税率消費税 = t02Data.通常税率消費税;
                hdData.軽減税率消費税 = t02Data.軽減税率消費税;
                // No-94 Add End
                // No-95 Add Start
                hdData.小計 = t02Data.小計;
                hdData.総合計 = t02Data.総合計;
                // No-95 Add End
                hdData.消費税 = t02Data.消費税;
                hdData.最終更新者 = _loginUserId;
                hdData.最終更新日時 = com.GetDbDateTime();
                hdData.削除者 = null;
                hdData.削除日時 = null;

                hdData.AcceptChanges();

            }

        }
        #endregion

        #region 売上ヘッダ論理削除
        /// <summary>
        /// 売上ヘッダの論理削除をおこなう
        /// </summary>
        /// <param name="number">対象伝票番号</param>
        public T02_URHD_HAN T02_URHD_HAN_Delete(int number)
        {
            // 対象データ取得
            var hdResult =
                _context.T02_URHD_HAN
                    .Where(w => w.伝票番号 == number)
                    .FirstOrDefault();

            if (hdResult != null)
            {
                hdResult.削除者 = _loginUserId;
                hdResult.削除日時 = com.GetDbDateTime();

                hdResult.AcceptChanges();

            }

            return hdResult;

        }
        #endregion

        #endregion


        #region << 販社売上明細関連 >>

        #region 指定伝票の販社売上明細削除
        /// <summary>
        /// 販社売上明細から指定伝票番号の明細データを物理削除する
        /// </summary>
        /// <param name="denNumber"></param>
        public void T02_URDTL_HAN_DeleteRecords(int denNumber)
        {
            var delData = _context.T02_URDTL_HAN.Where(w => w.伝票番号 == denNumber).ToList();
            if (delData != null)
            {
                foreach (T02_URDTL_HAN dtl in delData)
                    _context.T02_URDTL_HAN.DeleteObject(dtl);

                _context.SaveChanges();

            }

        }
        #endregion

        #region 売上明細登録更新
        /// <summary>
        /// 売上明細の登録・更新をおこなう
        /// </summary>
        /// <param name="udtlData"></param>
        public void T02_URDTL_HAN_Update(T02_URDTL_HAN udtlData)
        {
            var dtlData =
                _context.T02_URDTL_HAN
                    .Where(w => w.伝票番号 == udtlData.伝票番号 && w.行番号 == udtlData.行番号)
                    .FirstOrDefault();

            if (dtlData == null)
            {
                // データなしの為追加
                T02_URDTL_HAN urdtl = new T02_URDTL_HAN();

                urdtl.伝票番号 = udtlData.伝票番号;
                urdtl.行番号 = udtlData.行番号;
                urdtl.品番コード = udtlData.品番コード;
                urdtl.賞味期限 = udtlData.賞味期限;
                urdtl.数量 = udtlData.数量;
                urdtl.単位 = udtlData.単位;
                urdtl.単価 = udtlData.単価;
                urdtl.金額 = udtlData.金額;
                urdtl.摘要 = udtlData.摘要;
                urdtl.登録者 = _loginUserId;
                urdtl.登録日時 = com.GetDbDateTime();
                urdtl.最終更新者 = _loginUserId;
                urdtl.最終更新日時 = com.GetDbDateTime();

                _context.T02_URDTL_HAN.ApplyChanges(urdtl);

            }
            else
            {
                // データを更新
                dtlData.品番コード = udtlData.品番コード;
                dtlData.賞味期限 = udtlData.賞味期限;
                dtlData.数量 = udtlData.数量;
                dtlData.単位 = udtlData.単位;
                dtlData.単価 = udtlData.単価;
                dtlData.金額 = udtlData.金額;
                dtlData.摘要 = udtlData.摘要;
                dtlData.最終更新者 = _loginUserId;
                dtlData.最終更新日時 = com.GetDbDateTime();
                dtlData.削除者 = null;
                dtlData.削除日時 = null;

                dtlData.AcceptChanges();

            }


        }
        #endregion

        #region 売上明細論理削除
        /// <summary>
        /// 売上明細情報の論理削除をおこなう
        /// </summary>
        /// <param name="number">対象伝票番号</param>
        /// <returns>対象の仕入明細リスト</returns>
        public List<T02_URDTL_HAN> T02_URDTL_HAN_Delete(int number)
        {
            var dtlResult =
                _context.T02_URDTL_HAN
                    .Where(w => w.伝票番号 == number)
                    .ToList();

            if (dtlResult != null)
            {
                foreach (T02_URDTL_HAN srdtl in dtlResult)
                {
                    srdtl.削除者 = _loginUserId;
                    srdtl.削除日時 = com.GetDbDateTime();

                    srdtl.AcceptChanges();

                }

            }

            return dtlResult;

        }
        #endregion

        #endregion

    }

}