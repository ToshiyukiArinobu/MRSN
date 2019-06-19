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
    /// 都度請求締集計サービス
    /// </summary>
    public class TKS09010
    {
        #region << 定数定義 >>

        private const string PARAMS_NAME_COMPANY = "自社コード";
        private const string PARAMS_NAME_CREATE_DATE_START = "集計開始日";
        private const string PARAMS_NAME_CREATE_DATE_END = "集計終了日";

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
