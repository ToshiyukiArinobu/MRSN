using System;
using System.Collections.Generic;
using System.Linq;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 移動情報サービスクラス
    /// </summary>
    public class T05 : BaseService
    {
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId"></param>
        public T05(TRAC3Entities context, int userId)
        {
            _context = context;
            _loginUserId = userId;
        }
        #endregion

        #region << 移動ヘッダ登録更新 >>
        /// <summary>
        /// 移動ヘッダの登録・更新をおこなう
        /// </summary>
        /// <param name="idohd"></param>
        public void T05_IDOHD_Update(T05_IDOHD idohd)
        {
            // 登録データ取得
            var hdData =
                _context.T05_IDOHD
                    .Where(w => w.伝票番号 == idohd.伝票番号)
                    .FirstOrDefault();

            if (hdData == null)
            {
                // データなしの為追加
                T05_IDOHD ido = new T05_IDOHD();

                ido.伝票番号 = idohd.伝票番号;
                ido.会社名コード = idohd.会社名コード;
                ido.日付 = idohd.日付;
                ido.移動区分 = idohd.移動区分;
                ido.出荷元倉庫コード = idohd.出荷元倉庫コード;
                ido.出荷先倉庫コード = idohd.出荷先倉庫コード;
                ido.登録者 = _loginUserId;
                ido.登録日時 = com.GetDbDateTime();
                ido.最終更新者 = _loginUserId;
                ido.最終更新日時 = com.GetDbDateTime();

                _context.T05_IDOHD.ApplyChanges(ido);

            }
            else
            {
                // データを更新
                hdData.会社名コード = idohd.会社名コード;
                hdData.日付 = idohd.日付;
                hdData.移動区分 = idohd.移動区分;
                hdData.出荷元倉庫コード = idohd.出荷元倉庫コード;
                hdData.出荷先倉庫コード = idohd.出荷先倉庫コード;
                hdData.最終更新者 = _loginUserId;
                hdData.最終更新日時 = com.GetDbDateTime();
                hdData.削除者 = null;
                hdData.削除日時 = null;

                hdData.AcceptChanges();

            }

        }
        #endregion

        #region << 移動明細登録更新 >>
        /// <summary>
        /// 移動明細の登録・更新をおこなう
        /// </summary>
        /// <param name="idoDtlData"></param>
        public void T05_IDODTL_Update(T05_IDODTL idoDtlData)
        {
            // 登録済データが存在するか判定
            var dtlData =
                _context.T05_IDODTL
                    .Where(w => w.伝票番号 == idoDtlData.伝票番号 && w.行番号 == idoDtlData.行番号)
                    .FirstOrDefault();

            if (dtlData == null)
            {
                // データなしの為追加
                T05_IDODTL srdtl = new T05_IDODTL();
                srdtl.伝票番号 = idoDtlData.伝票番号;
                srdtl.行番号 = idoDtlData.行番号;
                srdtl.品番コード = idoDtlData.品番コード;
                srdtl.賞味期限 = idoDtlData.賞味期限;
                srdtl.数量 = idoDtlData.数量;
                srdtl.摘要 = idoDtlData.摘要;
                srdtl.登録者 = _loginUserId;
                srdtl.登録日時 = com.GetDbDateTime();
                srdtl.最終更新者 = _loginUserId;
                srdtl.最終更新日時 = com.GetDbDateTime();

                _context.T05_IDODTL.ApplyChanges(srdtl);

            }
            else
            {
                // データを更新
                dtlData.品番コード = idoDtlData.品番コード;
                dtlData.賞味期限 = idoDtlData.賞味期限;
                dtlData.数量 = idoDtlData.数量;
                dtlData.摘要 = idoDtlData.摘要;
                dtlData.最終更新者 = _loginUserId;
                dtlData.最終更新日時 = com.GetDbDateTime();

                dtlData.AcceptChanges();

            }

        }
        #endregion

        #region 移動ヘッダ論理削除
        /// <summary>
        /// 移動ヘッダの論理削除をおこなう
        /// </summary>
        /// <param name="number">対象伝票番号</param>
        public T05_IDOHD T05_IDOHD_Delete(int number)
        {
            // 対象データ取得
            var hdResult =
                _context.T05_IDOHD
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

        #region 移動明細論理削除
        /// <summary>
        /// 移動明細の論理削除をおこなう
        /// </summary>
        /// <param name="number">対象伝票番号</param>
        public List<T05_IDODTL> T05_IDODTL_Delete(int number)
        {
            var dtlResult =
                _context.T05_IDODTL
                    .Where(w => w.伝票番号 == number)
                    .ToList();

            if (dtlResult != null)
            {
                foreach (T05_IDODTL idodtl in dtlResult)
                {
                    idodtl.削除者 = _loginUserId;
                    idodtl.削除日時 = com.GetDbDateTime();

                    idodtl.AcceptChanges();

                }

            }

            return dtlResult;

        }
        #endregion

        #region 指定伝票の仕入明細削除
        /// <summary>
        /// 仕入明細から指定伝票番号の明細データを物理削除する
        /// </summary>
        /// <param name="denNumber"></param>
        public void T05_IDODTL_DeleteRecords(int denNumber)
        {
            var delData = _context.T05_IDODTL.Where(w => w.伝票番号 == denNumber).ToList();
            if (delData != null)
            {
                foreach (T05_IDODTL dtl in delData)
                    _context.T05_IDODTL.DeleteObject(dtl);

                _context.SaveChanges();

            }

        }
        #endregion



        #region 出荷先倉庫取得
        /// <summary>
        /// 出荷先倉庫を取得する
        /// </summary>
        /// <param name="t02Data"></param>
        /// <returns></returns>
        public int getShippingDestination(T02_URHD t02Data)
        {
            if (t02Data.会社名コード == null)
                return -1;

            var code =
                _context.M70_JIS.Where(w => w.自社コード == t02Data.会社名コード)
                    .Join(_context.M22_SOUK
                            .Where(w => w.削除日時 == null && w.場所会社コード == w.寄託会社コード),
                        x => x.自社コード,
                        y => y.場所会社コード,
                        (x, y) => new { JIS = x, SOUK = y })
                    .Select(s => s.SOUK.倉庫コード)
                    .FirstOrDefault();

            return code;

        }
        #endregion

        #region 消費税計算
        /// <summary>
        /// 品番の卸値から消費税計算をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="salesDate">売上日</param>
        /// <param name="taxKbn">税区分ID</param>
        /// <param name="productCode">品番コード</param>
        /// <param name="qty">数量</param>
        /// <param name="taxKbn">消費税区分[M01_TOK.T消費税区分(S支払消費税区分)] 1:一括、2:個別、3:請求無し</param>
        /// <returns></returns>
        public decimal getCalcSalesTax(DateTime salesDate, int taxKbnId, int productCode, decimal qty, int taxKbn)
        {
            var hin =
                _context.M09_HIN
                    .Where(w => w.削除日時 == null && w.品番コード == productCode)
                    .FirstOrDefault();

            if (hin == null)
                return 0;

            decimal conTax = 0;
            decimal taxRate = getTargetTaxRate(salesDate, hin.消費税区分 ?? 0);
            decimal calcValue = decimal.Multiply((hin.卸値 ?? 0) * qty, decimal.Divide(taxRate, 100m));

            // No.272 Add Start
            if (taxKbn == (int)CommonConstants.消費税区分.ID03_請求無)
            {
                return conTax;
            }
            // No.272 Add End

            switch (taxKbnId)
            {
                case 1:
                    // 切捨て
                    // No.133 Mod Start
                    if (calcValue > 0)
                    {
                        conTax += Math.Floor(calcValue);
                    }
                    else
                    {
                        conTax += Math.Ceiling(calcValue);
                    }
                    // No.133 Mod End
                    break;

                case 2:
                    // 四捨五入
                    conTax += Math.Round(calcValue, 0);
                    break;

                case 3:
                    // 切上げ
                    // No.133 Mod Start
                    if (calcValue > 0)
                    {
                        conTax += Math.Ceiling(calcValue);
                    }
                    else
                    {
                        conTax += Math.Floor(calcValue);
                    }
                    // No.133 Mod End
                    break;

                default:
                    // 上記以外は税計算なしとする
                    break;

            }

            return conTax;

        }
        #endregion

        #region 指定日時点の消費税率取得
        /// <summary>
        /// 指定日時点の消費税を取得する
        /// </summary>
        /// <param name="targetDate">対象となる日付</param>
        /// <param name="option">消費税区分(0:対象外、1:対象)</param>
        /// <returns></returns>
        private int getTargetTaxRate(DateTime? targetDate, int option = 0)
        {
            int rate = 0;

            // 対象となる開始日を取得
            var maxDate =
                _context.M73_ZEI
                    .Where(w => w.削除日時 == null && w.適用開始日付 <= targetDate)
                    .Max(m => m.適用開始日付);

            // 対象日付が取得できない場合は以下を処理しない
            if (maxDate == null)
                return rate;

            // 対象開始日と一致する消費税情報を取得
            var targetRow =
                _context.M73_ZEI
                    .Where(x => x.適用開始日付 == maxDate)
                    .FirstOrDefault();

            switch (option)
            {
                case 0:
                    // 通常税率
                    rate = targetRow.消費税率 ?? 0;
                    break;

                case 1:
                    // 軽減税率
                    rate = targetRow.軽減税率 ?? 0;
                    break;

                case 2:
                    // 非課税
                    rate = 0;
                    break;

                default:
                    break;

            }

            return rate;

        }
        #endregion

    }

}