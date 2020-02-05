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
            public string 自社名 { get; set; }
            public int 自社コード { get; set; }
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

        /// <summary>
        /// 確定情報
        /// </summary>
        public class TKS90010_FIX_INFO
        {
            public int? 自社コード { get; set; }
            public int? 確定区分 { get; set; }
            public string 区分 { get; set; }                // 請求/支払
            public int? 取引区分 { get; set; }
            public string 取引先コード { get; set; }
            public string 枝番 { get; set; }
            public string 取引先名 { get; set; }
            public int? 締日 { get; set; }
            public DateTime? 確定日 { get; set; }
        }


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

                // 得意先情報
                IQueryable<M01_TOK> seiTokData, shiTokData;
                var tokData = context.M01_TOK.Where(w => w.取引区分 == triKbn && w.削除日時 == null);
                
                // 自社コードが指定されている場合、絞り込み
                if (!string.IsNullOrEmpty(自社コード))
                {
                    tokData = tokData.Where(w => w.担当会社コード == company);
                }

                seiTokData = tokData;
                shiTokData = tokData;

                // 締日が指定されている場合、絞り込み
                if (!string.IsNullOrEmpty(締日))
                {
                    switch (triKbn)
                    {
                        case (int)CommonConstants.取引区分.得意先:
                            seiTokData = tokData.Where(w => w.Ｔ締日 == closingDays && w.Ｔ締日 > 0);
                            seiTokData = tokData.Where(w => w.Ｔ締日 == closingDays);
                            break;
                        case (int)CommonConstants.取引区分.仕入先:
                        case (int)CommonConstants.取引区分.加工先:
                            shiTokData = tokData.Where(w => w.Ｓ締日 == closingDays && w.Ｓ締日 > 0);
                            break;
                        case (int)CommonConstants.取引区分.相殺:
                            seiTokData = tokData.Where(w => (w.Ｔ締日 == closingDays && w.Ｔ締日 > 0));
                            shiTokData = tokData.Where(w => (w.Ｓ締日 == closingDays && w.Ｓ締日 > 0));
                            break;
                        default:
                            break;
                    }
                }

                // 検索情報取得
                // 請求確定データ
                var seiData = seiTokData.Where(w => w.Ｔ締日 != null && w.Ｔ締日 > 0)
                                .GroupJoin(context.S11_KAKUTEI.Where(w => w.確定区分 == (int)CommonConstants.確定区分.請求),
                                    x => new { code = x.担当会社コード, tKbn = x.取引区分, day = (int)x.Ｔ締日 },
                                    y => new { code = y.自社コード, tKbn = y.取引区分, day = y.締日 },
                                    (x, y) => new { x, y })
                                .SelectMany(z => z.y.DefaultIfEmpty(),
                                    (a, b) => new { TOK_T = a.x, FIX = b })
                                .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                                    x => new { jis = x.TOK_T.担当会社コード },
                                    y => new { jis = y.自社コード },
                                    (x, y) => new { x, y })
                                .SelectMany(z => z.y.DefaultIfEmpty(),
                                    (c, d) => new { TOK_T = c.x.TOK_T, FIX = c.x.FIX, JIS = d })
                                .GroupBy(g => new
                                {
                                    g.TOK_T.担当会社コード,
                                    g.JIS.自社名,
                                    g.TOK_T.取引区分,
                                    g.TOK_T.Ｔ締日,
                                    g.FIX.確定日,
                                    g.TOK_T.取引先コード,
                                    g.TOK_T.枝番,
                                    g.TOK_T.略称名
                                })
                                .ToList()
                                .Select(s => new TKS90010_SearchMember
                                {
                                    自社名 = s.Key.自社名,
                                    自社コード = s.Key.担当会社コード,
                                    確定区分 = (int)CommonConstants.確定区分.請求,
                                    区分 = CommonConstants.確定区分_請求,
                                    取引区分ID = s.Key.取引区分,
                                    取引区分 = s.Key.取引区分 == (int)CommonConstants.取引区分.得意先 ? CommonConstants.取引区分_得意先 :
                                               s.Key.取引区分 == (int)CommonConstants.取引区分.仕入先 ? CommonConstants.取引区分_仕入先 :
                                               s.Key.取引区分 == (int)CommonConstants.取引区分.加工先 ? CommonConstants.取引区分_加工先 :
                                               s.Key.取引区分 == (int)CommonConstants.取引区分.相殺 ? CommonConstants.取引区分_相殺 :
                                               string.Empty,
                                    ID = string.Format("{0:D4} - {1:D2}", s.Key.取引先コード, s.Key.枝番),
                                    得意先名 = s.Key.略称名,
                                    締日 = (int)s.Key.Ｔ締日,
                                    確定日 = s.Key.確定日
                                });

                // 支払確定データ
                var shrData = shiTokData.Where(w => w.Ｓ締日 != null)
                                .GroupJoin(context.S11_KAKUTEI.Where(w => w.確定区分 == (int)CommonConstants.確定区分.支払),
                                    x => new { code = x.担当会社コード, tKbn = x.取引区分, day = (int)x.Ｓ締日 },
                                    y => new { code = y.自社コード, tKbn = y.取引区分, day = y.締日 },
                                    (x, y) => new { x, y })
                                .SelectMany(z => z.y.DefaultIfEmpty(),
                                    (a, b) => new { TOK_S = a.x, FIX = b })
                                .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                                    x => new { jis = x.TOK_S.担当会社コード },
                                    y => new { jis = y.自社コード },
                                    (x, y) => new { x, y })
                                .SelectMany(z => z.y.DefaultIfEmpty(),
                                    (c, d) => new { FIX = c.x.FIX, TOK_S = c.x.TOK_S, JIS = d })
                                .GroupBy(g => new
                                {
                                    g.TOK_S.担当会社コード,
                                    g.JIS.自社名,
                                    g.TOK_S.取引区分,
                                    g.TOK_S.Ｓ締日,
                                    g.FIX.確定日,
                                    g.TOK_S.取引先コード,
                                    g.TOK_S.枝番,
                                    g.TOK_S.略称名
                                })
                                .ToList()
                                .Select(s => new TKS90010_SearchMember
                                {
                                    自社名 = s.Key.自社名,
                                    自社コード = s.Key.担当会社コード,
                                    確定区分 = (int)CommonConstants.確定区分.支払,
                                    区分 = CommonConstants.確定区分_支払,
                                    取引区分ID = s.Key.取引区分,
                                    取引区分 = s.Key.取引区分 == (int)CommonConstants.取引区分.得意先 ? CommonConstants.取引区分_得意先 :
                                               s.Key.取引区分 == (int)CommonConstants.取引区分.仕入先 ? CommonConstants.取引区分_仕入先 :
                                               s.Key.取引区分 == (int)CommonConstants.取引区分.加工先 ? CommonConstants.取引区分_加工先 :
                                               s.Key.取引区分 == (int)CommonConstants.取引区分.相殺 ? CommonConstants.取引区分_相殺 :
                                               string.Empty,
                                    ID = string.Format("{0:D4} - {1:D2}", s.Key.取引先コード, s.Key.枝番),

                                    得意先名 = s.Key.略称名,
                                    締日 = (int)s.Key.Ｓ締日,
                                    確定日 = s.Key.確定日
                                });

                // 請求データと支払データを結合
                var result = seiData.Concat(shrData);

                return result.OrderBy(o => o.自社コード)
                             .ThenBy(o => o.確定区分)
                             .ThenBy(o => o.締日)
                             .ThenBy(o => o.ID)
                             .ToList();
            }
        }

        #endregion

        #region 確定テーブル更新
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

                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    try
                    {
                        // パラメータ型変換
                        int iVal = 0;
                        DateTime dt;
                        DateTime minDt = DateTime.MinValue;
                        int company = int.TryParse(自社コード, out iVal) ? iVal : -1;
                        int fixKbn = int.TryParse(確定区分, out iVal) ? iVal : -1;
                        int triKbn = int.TryParse(取引区分, out iVal) ? iVal : -1;
                        int closingDays = int.TryParse(締日, out iVal) ? iVal : -1;
                        DateTime? fixDay = DateTime.TryParse(確定日, out dt) ? dt : (DateTime?)null;
                        if (fixDay == minDt)
                        {
                            fixDay = null;
                        }

                        // 更新行を特定
                        var s11Data =
                                context.S11_KAKUTEI
                                    .Where(x => x.自社コード == company &&
                                            x.取引区分 == triKbn &&
                                            x.確定区分 == fixKbn &&
                                            x.締日 == closingDays)
                                .Select(c => c)
                                .FirstOrDefault();

                        if (s11Data != null)
                        {
                            s11Data.確定日 = fixDay;
                            s11Data.登録日時 = DateTime.Now;
                            s11Data.登録者 = pLoginUserCode;
                            s11Data.AcceptChanges();
                        }
                        else
                        {
                            S11_KAKUTEI s11 = new S11_KAKUTEI();
                            s11.自社コード = company;
                            s11.取引区分 = triKbn;
                            s11.確定区分 = fixKbn;
                            s11.締日 = closingDays;
                            s11.確定日 = fixDay;
                            s11.登録者 = pLoginUserCode;
                            s11.登録日時 = DateTime.Now;

                            context.S11_KAKUTEI.ApplyChanges(s11);
                        }
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }
            }
            return 1;
        }
        #endregion

        #region 確定済チェック
        /// <summary>
        /// 確定済チェック
        /// </summary>
        /// <param name="自社コード"></param>
        /// <param name="得意先コード"></param>
        /// <param name="得意先枝番"></param>
        /// <param name="仕入先コード"></param>
        /// <param name="仕入先枝番"></param>
        /// <returns></returns>
        public DataTable CheckFix(string 自社コード, string 得意先コード, string 得意先枝番, string 仕入先コード, string 仕入先枝番)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // パラメータ型変換
                int iVal = 0;
                int company = int.TryParse(自社コード, out iVal) ? iVal : -1;
                int tCode = int.TryParse(得意先コード, out iVal) ? iVal : -1;
                int tEda = int.TryParse(得意先枝番, out iVal) ? iVal : -1;
                int sCode = int.TryParse(仕入先コード, out iVal) ? iVal : -1;
                int sEda = int.TryParse(仕入先枝番, out iVal) ? iVal : -1;

                IEnumerable<TKS90010_FIX_INFO> tFixData = null;
                IEnumerable<TKS90010_FIX_INFO> sFixData = null;

                // 得意先情報
                if (!string.IsNullOrEmpty(得意先コード))
                {
                    var tokData = context.M01_TOK.Where(w => w.取引先コード == tCode && w.枝番 == tEda &&
                                                             w.削除日時 == null);

                    tFixData = tokData
                                    .GroupJoin(context.S11_KAKUTEI.Where(w => w.確定区分 == (int)CommonConstants.確定区分.請求),
                                        x => new { jis = x.担当会社コード, tKbn = x.取引区分, day = (int)x.Ｔ締日 },
                                        y => new { jis = y.自社コード, tKbn = y.取引区分, day = y.締日 },
                                        (x, y) => new { x, y })
                                    .SelectMany(z => z.y.DefaultIfEmpty(),
                                        (a, b) => new { TOK = a.x, FIX = b })
                                    .ToList()
                                    .Select(s => new TKS90010_FIX_INFO
                                    {
                                        自社コード = s.FIX != null ? s.FIX.自社コード : (int?)null,
                                        区分 = CommonConstants.確定区分_請求,
                                        確定区分 = s.FIX != null ? s.FIX.確定区分 : (int?)null,
                                        取引区分 = s.TOK.取引区分,
                                        取引先コード = s.TOK.取引先コード.ToString(),
                                        枝番 = s.TOK.枝番.ToString(),
                                        取引先名 = s.TOK.略称名,
                                        締日 = s.FIX != null ? s.FIX.締日 : (int?)null,
                                        確定日 = s.FIX != null ? s.FIX.確定日 : (DateTime?)null
                                    })
                                    .Union
                                    (tokData
                                    .GroupJoin(context.S11_KAKUTEI.Where(w => w.確定区分 == (int)CommonConstants.確定区分.支払),
                                        x => new { jis = x.担当会社コード, tKbn = x.取引区分, day = (int)x.Ｓ締日 },
                                        y => new { jis = y.自社コード, tKbn = y.取引区分, day = y.締日 },
                                        (x, y) => new { x, y })
                                    .SelectMany(z => z.y.DefaultIfEmpty(),
                                        (a, b) => new { TOK = a.x, FIX = b })
                                    .ToList()
                                    .Select(s => new TKS90010_FIX_INFO
                                    {
                                        自社コード = s.FIX != null ? s.FIX.自社コード : (int?)null,
                                        区分 = CommonConstants.確定区分_支払,
                                        確定区分 = s.FIX != null ? s.FIX.確定区分 : (int?)null,
                                        取引区分 = s.TOK.取引区分,
                                        取引先コード = s.TOK.取引先コード.ToString(),
                                        枝番 = s.TOK.枝番.ToString(),
                                        取引先名 = s.TOK.略称名,
                                        締日 = s.FIX != null ? s.FIX.締日 : (int?)null,
                                        確定日 = s.FIX != null ? s.FIX.確定日 : (DateTime?)null
                                    }));
                }
                
                // 仕入先情報
                if (!string.IsNullOrEmpty(仕入先コード))
                {
                    var sTokData = context.M01_TOK.Where(w => w.取引先コード == sCode && w.枝番 == sEda &&
                                                         w.削除日時 == null);
                    
                    sFixData = sTokData
                                    .GroupJoin(context.S11_KAKUTEI.Where(w => w.確定区分 == (int)CommonConstants.確定区分.支払),
                                        x => new { jis = x.担当会社コード, tKbn = x.取引区分, day = (int)x.Ｓ締日 },
                                        y => new { jis = y.自社コード, tKbn = y.取引区分, day = y.締日 },
                                        (x, y) => new { x, y })
                                    .SelectMany(z => z.y.DefaultIfEmpty(),
                                        (a, b) => new { TOK = a.x, FIX = b })
                                    .ToList()
                                    .Select(s => new TKS90010_FIX_INFO
                                    {
                                        自社コード = s.FIX != null ? s.FIX.自社コード : (int?)null,
                                        区分 = CommonConstants.確定区分_支払,
                                        確定区分 = s.FIX != null ? s.FIX.確定区分 : (int?)null,
                                        取引区分 = s.TOK.取引区分,
                                        取引先コード = s.TOK.取引先コード.ToString(),
                                        枝番 = s.TOK.枝番.ToString(),
                                        取引先名 = s.TOK.略称名,
                                        締日 = s.FIX != null ? s.FIX.締日 : (int?)null,
                                        確定日 = s.FIX != null ? s.FIX.確定日 : (DateTime?)null 
                                    })
                                    .Union
                                    (sTokData
                                    .GroupJoin(context.S11_KAKUTEI.Where(w => w.確定区分 == (int)CommonConstants.確定区分.請求),
                                        x => new { jis = x.担当会社コード, tKbn = x.取引区分, day = (int)x.Ｔ締日 },
                                        y => new { jis = y.自社コード, tKbn = y.取引区分, day = y.締日 },
                                        (x, y) => new { x, y })
                                    .SelectMany(z => z.y.DefaultIfEmpty(),
                                        (a, b) => new { TOK = a.x, FIX = b })
                                    .ToList()
                                    .Select(s => new TKS90010_FIX_INFO
                                    {
                                        自社コード = s.FIX != null ? s.FIX.自社コード : (int?)null,
                                        区分 = CommonConstants.確定区分_請求,
                                        確定区分 = s.FIX != null ? s.FIX.確定区分 : (int?)null,
                                        取引区分 = s.TOK.取引区分,
                                        取引先コード = s.TOK.取引先コード.ToString(),
                                        枝番 = s.TOK.枝番.ToString(),
                                        取引先名 = s.TOK.略称名,
                                        締日 = s.FIX != null ? s.FIX.締日 : (int?)null,
                                        確定日 = s.FIX != null ? s.FIX.確定日 : (DateTime?)null 
                                    }));
                }

                // 確定データを連結
                tFixData = tFixData != null ? 
                            (sFixData != null ? tFixData.Concat(sFixData) : tFixData) : sFixData;

                // Datatable変換
                DataTable fixDt = KESSVCEntry.ConvertListToDataTable(tFixData.ToList());
                return fixDt;
            }
        }

        #endregion
    }

}
