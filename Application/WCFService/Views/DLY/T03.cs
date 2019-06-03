using System;
using System.Collections.Generic;
using System.Linq;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 仕入ヘッダ/明細 サービスクラス
    /// </summary>
    public class T03 : BaseService
    {
        #region << 定数定義 >>

        /// <summary>仕入ヘッダ テーブル名</summary>
        private const string T03_HEADER_TABLE_NAME = "T03_SRHD";
        /// <summary>仕入明細 テーブル名</summary>
        private const string T03_DETAIL_TABLE_NAME = "T03_SRDTL";
        /// <summary>消費税 テーブル名</summary>
        private const string M73_TABLE_NAME = "M73_ZEI";

        #endregion

        #region << 拡張クラス定義 >>

        public class T03_SRHD_Extension
        {
            public int 伝票番号 { get; set; }
            public string 会社名コード { get; set; }
            public DateTime 仕入日 { get; set; }
            public int 入力区分 { get; set; }
            public int 仕入区分 { get; set; }
            public string 仕入先コード { get; set; }
            public string 仕入先枝番 { get; set; }
            public string 入荷先コード { get; set; }
            public string 発注番号 { get; set; }
            public string 備考 { get; set; }
            public string 元伝票番号 { get; set; }
            public int 消費税 { get; set; }
            // REMARKS:Entityを基本に不足情報を補完する
            /// <summary>1:一括、2:個別</summary>
            public int Ｓ支払消費税区分 { get; set; }
            /// <summary>1:切捨て、2:四捨五入、3:切上げ、9:税なし</summary>
            public int Ｓ税区分ID { get; set; }
        }

        public class T03_SRDTL_Extension : T03_SRDTL
        {
            // REMARKS:Entityを基本に不足情報を補完する
            public string 自社品番 { get; set; }
            public string 自社品名 { get; set; }
            /// <summary>0:通常、1:軽減税率、2:非課税</summary>
            public int 消費税区分 { get; set; }
            /// <summary>1:食品、2:繊維、3:その他</summary>
            public int 商品分類 { get; set; }
        }

        /// <summary>
        /// 返品用仕入ヘッダ拡張クラス
        /// </summary>
        public class T03_SRHD_RT_Extension
        {
            public string 伝票番号 { get; set; }
            public string 会社名コード { get; set; }
            public DateTime 仕入日 { get; set; }
            public int 入力区分 { get; set; }
            public int 仕入区分 { get; set; }
            public string 仕入先コード { get; set; }
            public string 仕入先枝番 { get; set; }
            public string 入荷先コード { get; set; }
            public string 発注番号 { get; set; }
            public string 備考 { get; set; }
            public int? 消費税 { get; set; }
            public string 元伝票番号 { get; set; }
            public DateTime? 元仕入日 { get; set; }
            /// <summary>新規作成データかどうか</summary>
            public bool データ状態 { get; set; }
        }

        /// <summary>
        /// 返品用仕入明細拡張クラス
        /// </summary>
        public class T03_SRDTL_RT_Extension : T03_SRDTL
        {
            // REMARKS:Entityを基本に不足情報を補完する
            public string 自社品番 { get; set; }
            public string 自社品名 { get; set; }
            public decimal? 仕入数量 { get; set; }
            /// <summary>0:通常、1:軽減税率、2:非課税</summary>
            public int 消費税区分 { get; set; }
            /// <summary>1:食品、2:繊維、3:その他</summary>
            public int 商品分類 { get; set; }
        }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId"></param>
        public T03(TRAC3Entities context, int userId)
        {
            _context = context;
            _loginUserId = userId;

        }
        #endregion

        #region << 仕入ヘッダ関連 >>

        #region 仕入ヘッダ登録更新
        /// <summary>
        /// 仕入ヘッダの登録・更新をおこなう
        /// </summary>
        /// <param name="shdData"></param>
        public void T03_SRHD_Update(T03_SRHD shdData)
        {
            // 登録済データが存在するか判定
            var hdData = _context.T03_SRHD
                            .Where(w => w.伝票番号 == shdData.伝票番号)
                            .FirstOrDefault();

            if (hdData == null)
            {
                // データなしの為追加
                T03_SRHD srhd = new T03_SRHD();

                srhd.伝票番号 = shdData.伝票番号;
                srhd.会社名コード = shdData.会社名コード;
                srhd.仕入日 = shdData.仕入日;
                srhd.入力区分 = shdData.入力区分;
                srhd.仕入区分 = shdData.仕入区分;
                srhd.仕入先コード = shdData.仕入先コード;
                srhd.仕入先枝番 = shdData.仕入先枝番;
                srhd.入荷先コード = shdData.入荷先コード;
                srhd.発注番号 = shdData.発注番号;
                srhd.備考 = shdData.備考;
                srhd.元伝票番号 = shdData.元伝票番号;
                srhd.消費税 = shdData.消費税;
                srhd.登録者 = _loginUserId;
                srhd.登録日時 = com.GetDbDateTime();
                srhd.最終更新者 = _loginUserId;
                srhd.最終更新日時 = com.GetDbDateTime();

                _context.T03_SRHD.ApplyChanges(srhd);

            }
            else
            {
                // データを更新
                hdData.会社名コード = shdData.会社名コード;
                hdData.仕入日 = shdData.仕入日;
                hdData.入力区分 = shdData.入力区分;
                hdData.仕入区分 = shdData.仕入区分;
                hdData.仕入先コード = shdData.仕入先コード;
                hdData.仕入先枝番 = shdData.仕入先枝番;
                hdData.入荷先コード = shdData.入荷先コード;
                hdData.発注番号 = shdData.発注番号;
                hdData.備考 = shdData.備考;
                hdData.元伝票番号 = shdData.元伝票番号;
                hdData.消費税 = shdData.消費税;
                hdData.最終更新者 = _loginUserId;
                hdData.最終更新日時 = com.GetDbDateTime();

                hdData.AcceptChanges();

            }

        }
        #endregion

        #region 仕入ヘッダ論理削除
        /// <summary>
        /// 仕入ヘッダの論理削除をおこなう
        /// </summary>
        /// <param name="number">対象伝票番号</param>
        public T03_SRHD T03_SRHD_Delete(int number)
        {
            // 対象データ取得
            var hdResult =
                _context.T03_SRHD
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


        #region << 仕入明細関連 >>

        #region 指定伝票の仕入明細削除
        /// <summary>
        /// 仕入明細から指定伝票番号の明細データを物理削除する
        /// </summary>
        /// <param name="denNumber"></param>
        public void T03_SRDTL_DeleteRecords(int denNumber)
        {
            var delData = _context.T03_SRDTL.Where(w => w.伝票番号 == denNumber).ToList();
            if (delData != null)
            {
                foreach (T03_SRDTL dtl in delData)
                    _context.T03_SRDTL.DeleteObject(dtl);

                _context.SaveChanges();

            }

        }
        #endregion

        #region 仕入明細登録更新
        /// <summary>
        /// 仕入明細の登録・更新をおこなう
        /// </summary>
        /// <param name="sdtlData"></param>
        public void T03_SRDTL_Update(T03_SRDTL sdtlData)
        {
            // 登録済データが存在するか判定
            var dtlData =
                _context.T03_SRDTL
                    .Where(w => w.伝票番号 == sdtlData.伝票番号 && w.行番号 == sdtlData.行番号)
                    .FirstOrDefault();

            if (dtlData == null)
            {
                // データなしの為追加
                T03_SRDTL srdtl = new T03_SRDTL();
                srdtl.伝票番号 = sdtlData.伝票番号;
                srdtl.行番号 = sdtlData.行番号;
                srdtl.品番コード = sdtlData.品番コード;
                srdtl.賞味期限 = sdtlData.賞味期限;
                srdtl.数量 = sdtlData.数量;
                srdtl.単位 = sdtlData.単位;
                srdtl.単価 = sdtlData.単価;
                srdtl.金額 = sdtlData.金額;
                srdtl.摘要 = sdtlData.摘要;
                srdtl.登録者 = _loginUserId;
                srdtl.登録日時 = com.GetDbDateTime();
                srdtl.最終更新者 = _loginUserId;
                srdtl.最終更新日時 = com.GetDbDateTime();

                _context.T03_SRDTL.ApplyChanges(srdtl);

            }
            else
            {
                // データを更新
                dtlData.品番コード = sdtlData.品番コード;
                dtlData.賞味期限 = sdtlData.賞味期限;
                dtlData.数量 = sdtlData.数量;
                dtlData.単位 = sdtlData.単位;
                dtlData.単価 = sdtlData.単価;
                dtlData.金額 = sdtlData.金額;
                dtlData.摘要 = sdtlData.摘要;
                dtlData.最終更新者 = _loginUserId;
                dtlData.最終更新日時 = com.GetDbDateTime();

                dtlData.AcceptChanges();

            }

        }
        #endregion

        #region 仕入明細論理削除
        /// <summary>
        /// 仕入明細情報の論理削除をおこなう
        /// </summary>
        /// <param name="number">対象伝票番号</param>
        /// <returns>対象の仕入明細リスト</returns>
        public List<T03_SRDTL> T03_SRDTL_Delete(int number)
        {
            var dtlResult =
                _context.T03_SRDTL
                    .Where(w => w.伝票番号 == number)
                    .ToList();

            if (dtlResult != null)
            {
                foreach (T03_SRDTL srdtl in dtlResult)
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


        #region << 仕入販社ヘッダ関連 >>

        #region 仕入ヘッダ登録更新
        /// <summary>
        /// 仕入ヘッダの登録・更新をおこなう
        /// </summary>
        /// <param name="shdData"></param>
        public void T03_SRHD_HAN_Update(T03_SRHD_HAN shdData)
        {
            // 登録済データが存在するか判定
            var hdData = _context.T03_SRHD_HAN
                            .Where(w => w.伝票番号 == shdData.伝票番号)
                            .FirstOrDefault();

            if (hdData == null)
            {
                // データなしの為追加
                T03_SRHD_HAN srhd = new T03_SRHD_HAN();

                srhd.伝票番号 = shdData.伝票番号;
                srhd.会社名コード = shdData.会社名コード;
                srhd.仕入日 = shdData.仕入日;
                srhd.仕入区分 = shdData.仕入区分;
                srhd.仕入先コード = shdData.仕入先コード;
                srhd.入荷先コード = shdData.入荷先コード;
                srhd.発注番号 = shdData.発注番号;
                srhd.備考 = shdData.備考;
                srhd.消費税 = shdData.消費税;
                srhd.登録者 = _loginUserId;
                srhd.登録日時 = com.GetDbDateTime();
                srhd.最終更新者 = _loginUserId;
                srhd.最終更新日時 = com.GetDbDateTime();

                _context.T03_SRHD_HAN.ApplyChanges(srhd);

            }
            else
            {
                // データを更新
                hdData.会社名コード = shdData.会社名コード;
                hdData.仕入日 = shdData.仕入日;
                hdData.仕入区分 = shdData.仕入区分;
                hdData.仕入先コード = shdData.仕入先コード;
                hdData.入荷先コード = shdData.入荷先コード;
                hdData.発注番号 = shdData.発注番号;
                hdData.備考 = shdData.備考;
                hdData.消費税 = shdData.消費税;
                hdData.最終更新者 = _loginUserId;
                hdData.最終更新日時 = com.GetDbDateTime();

                hdData.AcceptChanges();

            }

        }
        #endregion



        #endregion


        #region << 仕入販社明細関連 >>

        #region 指定伝票の販社仕入明細削除
        /// <summary>
        /// 販社仕入明細から指定伝票番号の明細データを物理削除する
        /// </summary>
        /// <param name="denNumber"></param>
        public void T03_SRDTL_HAN_DeleteRecords(int denNumber)
        {
            var delData = _context.T03_SRDTL_HAN.Where(w => w.伝票番号 == denNumber).ToList();
            if (delData != null)
            {
                foreach (T03_SRDTL_HAN dtl in delData)
                    _context.T03_SRDTL_HAN.DeleteObject(dtl);

                _context.SaveChanges();

            }

        }
        #endregion

        #region 仕入明細登録更新
        /// <summary>
        /// 仕入明細の登録・更新をおこなう
        /// </summary>
        /// <param name="sdtlData"></param>
        public void T03_SRDTL_HAN_Update(T03_SRDTL_HAN sdtlData)
        {
            // 登録済データが存在するか判定
            var dtlData =
                _context.T03_SRDTL_HAN
                    .Where(w => w.伝票番号 == sdtlData.伝票番号 && w.行番号 == sdtlData.行番号)
                    .FirstOrDefault();

            if (dtlData == null)
            {
                // データなしの為追加
                T03_SRDTL_HAN srdtl = new T03_SRDTL_HAN();
                srdtl.伝票番号 = sdtlData.伝票番号;
                srdtl.行番号 = sdtlData.行番号;
                srdtl.品番コード = sdtlData.品番コード;
                srdtl.賞味期限 = sdtlData.賞味期限;
                srdtl.数量 = sdtlData.数量;
                srdtl.単位 = sdtlData.単位;
                srdtl.単価 = sdtlData.単価;
                srdtl.金額 = sdtlData.金額;
                srdtl.摘要 = sdtlData.摘要;
                srdtl.登録者 = _loginUserId;
                srdtl.登録日時 = com.GetDbDateTime();
                srdtl.最終更新者 = _loginUserId;
                srdtl.最終更新日時 = com.GetDbDateTime();

                _context.T03_SRDTL_HAN.ApplyChanges(srdtl);

            }
            else
            {
                // データを更新
                dtlData.品番コード = sdtlData.品番コード;
                dtlData.賞味期限 = sdtlData.賞味期限;
                dtlData.数量 = sdtlData.数量;
                dtlData.単位 = sdtlData.単位;
                dtlData.単価 = sdtlData.単価;
                dtlData.金額 = sdtlData.金額;
                dtlData.摘要 = sdtlData.摘要;
                dtlData.最終更新者 = _loginUserId;
                dtlData.最終更新日時 = com.GetDbDateTime();

                dtlData.AcceptChanges();

            }

        }
        #endregion

        #endregion



        #region << 処理関連 >>

        /// <summary>
        /// 会社名コードから該当の倉庫コードを取得する
        /// </summary>
        /// <param name="context"></param>
        /// <param name="会社名コード">M70_JIS</param>
        /// <returns></returns>
        public int get倉庫コード(int 会社名コード)
        {
            var souk =
                _context.M22_SOUK
                    .Where(w => w.削除日時 == null &&
                        w.場所会社コード == 会社名コード &&
                        w.場所会社コード == w.寄託会社コード)
                    .Select(s => s.倉庫コード)
                    .FirstOrDefault();

            return souk;

        }

        #endregion

    }

}