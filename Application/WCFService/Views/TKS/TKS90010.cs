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

        private const string PARAMS_NAME_COMPANY = "自社コード";
        private const string PARAMS_NAME_CREATE_DATE_START = "集計開始日";
        private const string PARAMS_NAME_CREATE_DATE_END = "集計終了日";

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
                int tokKbn = int.TryParse(取引区分, out iVal) ? iVal : -1;
                int? closingDays = int.TryParse(締日, out iVal) ? iVal : (int?)null;

                // 確定情報
                var fixData = context.S11_KAKUTEI.Where(w => w.自社コード == company && w.取引区分 == tokKbn);

                // 得意先情報
                var tokData = context.M01_TOK.Where(w => w.取引区分 == tokKbn && w.削除日時 == null);

                // 締日が指定されている場合、絞り込み
                if (!string.IsNullOrEmpty(締日))
                {
                    fixData = fixData.Where(w => w.締日 == closingDays);

                    switch (tokKbn)
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
                                .GroupJoin(tokData,
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
                                .GroupJoin(tokData,
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

        #region 請求締集計処理
        /// <summary>
        /// 都度請求締集計処理
        /// </summary>
        /// <param name="paramDic"></param>
        /// <param name="userId"></param>
        public void BillingAggregation(Dictionary<string, string> paramDic, int userId)
        {
            string companyCd, startDate, endDate;

            try
            {
                // 画面パラメータ展開
                companyCd = paramDic[PARAMS_NAME_COMPANY];
                startDate = paramDic[PARAMS_NAME_CREATE_DATE_START];
                endDate = paramDic[PARAMS_NAME_CREATE_DATE_END];

            }
            catch (Exception e)
            {
                throw e;
            }

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                using (DbTransaction tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    try
                    {
                        TKS01010 tok01010 = new TKS01010();

                        // パラメータ型変換
                        int iCompany = int.Parse(companyCd);
                        DateTime
                            tStartDate = DateTime.Parse(startDate),
                            tEndDate = DateTime.Parse(endDate);

                        // 得意先取得
                        List<M01_TOK> tokList = getTargetTOK(context, iCompany);

                        foreach (M01_TOK tok in tokList)
                        {
                            DateTime targetDate = tStartDate;
                            while (targetDate <= tEndDate)
                            {
                                TKS01010.TKS01010_SearchMember mem = new TKS01010.TKS01010_SearchMember();
                                mem.ID = string.Format("{0} - {1}", tok.取引先コード, tok.枝番);
                                mem.得意先コード = tok.取引先コード;
                                mem.得意先枝番 = tok.枝番;
                                mem.得意先名 = tok.得意先名１;
                                mem.締日 = targetDate.Day;
                                mem.区分 = string.Empty;
                                mem.開始日付1 = targetDate;
                                mem.終了日付1 = targetDate;
                                mem.開始日付2 = null;
                                mem.終了日付2 = null;
                                mem.開始日付3 = null;
                                mem.終了日付3 = null;
                                mem.クリア開始日付 = targetDate;
                                mem.クリア終了日付 = targetDate;
                                mem.入金日 = targetDate;

                                // 締集計の処理に投げる
                                tok01010.getAggregateData(context, iCompany, targetDate.Year * 100 + targetDate.Month, mem, 1, userId);

                                targetDate = targetDate.AddDays(1);

                            }// -- end loop date

                        }// -- end loop tok

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

        }
        #endregion


        #region 得意先情報取得
        /// <summary>
        /// 都度請求対象の得意先情報を取得する
        /// </summary>
        /// <param name="context"></param>
        /// <param name="iCompany"></param>
        /// <returns></returns>
        private List<M01_TOK> getTargetTOK(TRAC3Entities context, int iCompany)
        {
            var tokList =
                context.M01_TOK
                    .Where(w => w.削除日時 == null && w.担当会社コード == iCompany && w.Ｔ締日 == 0);

            return tokList.ToList();

        }
        #endregion

    }

}
