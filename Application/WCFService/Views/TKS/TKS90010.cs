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

    /// <summary>
    /// 確定処理サービス
    /// </summary>
    public class TKS90010
    {
        #region << 定数定義 >>
        #endregion

        #region << 項目クラス定義 >>
        /// <summary>
        /// 確定情報 検索メンバークラス
        /// </summary>
        public class TKS90010_SearchMember
        {
            public int 確定区分 { get; set; }
            public string 区分 { get; set; }                // 請求/支払
            public int 取引区分ID { get; set; }
            public string 取引区分 { get; set; }
            public string ID { get; set; }                  // 得意先コード xxxx-xx
            public string 得意先名 { get; set; }
            public int 締日 { get; set; }
            public DateTime? 確定日 { get; set; }
        }
        #endregion

        #region 確定情報取得
        /// <summary>
        /// 確定情報取得
        /// </summary>
        /// <param name="自社コード"></param>
        /// <param name="締日"></param>
        /// <param name="取引先区分"></param>
        /// <returns></returns>
        public List<TKS90010_SearchMember> GetListData(string 自社コード, string 締日, string 取引区分)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // パラメータ型変換
                int iVal = 0;
                int company = int.TryParse(自社コード, out iVal) ? iVal : -1;
                int triKbn = int.TryParse(取引区分, out iVal) ? iVal : -1;
                int? closingDays = int.TryParse(締日, out iVal) ? iVal : (int?)null;

                // 確定情報
                var fixData = context.S11_KAKUTEI.Where(w => w.自社コード == company && w.取引区分 == triKbn);

                // 得意先情報
                var tokData = context.M01_TOK.Where(w => w.取引区分 == triKbn && w.削除日時 == null);

                // 締日が指定されている場合、絞り込み
                if (!string.IsNullOrEmpty(締日))
                {
                    fixData = fixData.Where(w => w.締日 == closingDays);

                    switch (triKbn)
                    {
                        case (int)CommonConstants.取引区分.得意先:
                            tokData = tokData.Where(w => w.Ｔ締日 == closingDays && w.Ｔ締日 > 0);
                            break;
                        case (int)CommonConstants.取引区分.仕入先:
                        case (int)CommonConstants.取引区分.加工先:
                            tokData = tokData.Where(w => w.Ｓ締日 == closingDays && w.Ｓ締日 > 0);
                            break;
                        case (int)CommonConstants.取引区分.相殺:
                            tokData = tokData.Where(w => w.Ｔ締日 == closingDays || w.Ｓ締日 == closingDays);
                            break;
                        default:
                            break;
                    }
                }

                // 検索情報取得
                // 請求確定データ
                var seiData = fixData.Where(w => w.確定区分 == (int)CommonConstants.確定区分.請求)
                                .GroupJoin(tokData.Where(w => w.Ｔ締日 != null),
                                    x => new { code = x.自社コード, day = x.締日 },
                                    y => new { code = y.担当会社コード, day = (int)y.Ｔ締日 },
                                    (x, y) => new { x, y })
                                .SelectMany(z => z.y.DefaultIfEmpty(),
                                    (a, b) => new { FIX = a.x, TOK_T = b })
                                .ToList()
                                .Select(s => new TKS90010_SearchMember
                                {
                                    確定区分 = s.FIX.確定区分,
                                    区分 = CommonConstants.確定区分_請求,
                                    取引区分ID = s.FIX.取引区分,
                                    取引区分 = s.FIX.取引区分 == (int)CommonConstants.取引区分.得意先 ? CommonConstants.取引区分_得意先 :
                                               s.FIX.取引区分 == (int)CommonConstants.取引区分.仕入先 ? CommonConstants.取引区分_仕入先 :
                                               s.FIX.取引区分 == (int)CommonConstants.取引区分.加工先 ? CommonConstants.取引区分_加工先 :
                                               s.FIX.取引区分 == (int)CommonConstants.取引区分.相殺 ? CommonConstants.取引区分_相殺 :
                                               string.Empty,
                                    ID = string.Format("{0:D4} - {1:D2}", s.TOK_T.取引先コード, s.TOK_T.枝番),
                                    得意先名 = s.TOK_T.略称名,
                                    締日 = (int)s.TOK_T.Ｔ締日,
                                    確定日 = s.FIX.確定日
                                });

                // 支払確定データ
                var shrData = fixData.Where(w => w.確定区分 == (int)CommonConstants.確定区分.支払)
                                .GroupJoin(tokData.Where(w => w.Ｓ締日 != null),
                                    x => new { code = x.自社コード, day = x.締日 },
                                    y => new { code = y.担当会社コード, day = (int)y.Ｓ締日 },
                                    (x, y) => new { x, y })
                                .SelectMany(z => z.y.DefaultIfEmpty(),
                                    (a, b) => new { FIX = a.x, TOK_S = b })
                                .ToList()
                                .Select(s => new TKS90010_SearchMember
                                {
                                    確定区分 = s.FIX.確定区分,
                                    区分 = CommonConstants.確定区分_支払,
                                    取引区分ID = s.FIX.取引区分,
                                    取引区分 = s.FIX.取引区分 == (int)CommonConstants.取引区分.得意先 ? CommonConstants.取引区分_得意先 :
                                               s.FIX.取引区分 == (int)CommonConstants.取引区分.仕入先 ? CommonConstants.取引区分_仕入先 :
                                               s.FIX.取引区分 == (int)CommonConstants.取引区分.加工先 ? CommonConstants.取引区分_加工先 :
                                               s.FIX.取引区分 == (int)CommonConstants.取引区分.相殺 ? CommonConstants.取引区分_相殺 :
                                               string.Empty,
                                    ID = string.Format("{0:D4} - {1:D2}", s.TOK_S.取引先コード, s.TOK_S.枝番),
                                    
                                    得意先名 = s.TOK_S.略称名,
                                    締日 = (int)s.TOK_S.Ｓ締日,
                                    確定日 = s.FIX.確定日
                                });

                // 請求データと支払データを結合
                var result = seiData.Concat(shrData);

                return result.OrderBy(o => o.確定区分)
                             .ThenBy(o => o.締日)
                             .ToList();
            }
        }

        #endregion

        /// <summary>
        /// 確定テーブル更新
        /// </summary>
        /// <param name="自社コード"></param>
        /// <param name="締日"></param>
        /// <param name="確定日"></param>
        /// <param name="取引区分"></param>
        /// <returns></returns>
        public int Update(string 自社コード, string 取引区分, string 確定区分, string 締日, string 確定日, int pLoginUserCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // パラメータ型変換
                int iVal = 0;
                DateTime dt;
                int company = int.TryParse(自社コード, out iVal) ? iVal : -1;
                int fixKbn = int.TryParse(確定区分, out iVal) ? iVal : -1;
                int triKbn = int.TryParse(取引区分, out iVal) ? iVal : -1;
                int? closingDays = int.TryParse(締日, out iVal) ? iVal : (int?)null;
                DateTime? fixDay = DateTime.TryParse(確定日, out dt) ? dt : (DateTime?) null;

                // 更新行を特定
                var s11 =
                    context.S11_KAKUTEI
                        .Where(x => x.自社コード == company &&
                                    x.取引区分 == triKbn &&
                                    x.確定区分 == fixKbn &&
                                    x.締日 == closingDays)
                        .Select(c => c)
                        .FirstOrDefault();

                if (s11 != null)
                {
                    s11.確定日 = fixDay;
                    s11.登録日時 = DateTime.Now;
                    s11.登録者 = pLoginUserCode;
                    s11.AcceptChanges();
                }

                context.SaveChanges();

                return 1;
            }
        }
    }

}
