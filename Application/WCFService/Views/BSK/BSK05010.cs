using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    using debugLog = System.Diagnostics.Debug;

    /// <summary>
    /// 年次販社売上調整サービスクラス
    /// </summary>
    public class BSK05010
    {
        #region << メンバクラス定義 >>

        /// <summary>
        /// 年次販社売上調整項目メンバクラス
        /// </summary>
        public class BSK05010_SearchMember
        {
            public string 決算対象年月 { get; set; }
            public long 決算調整前金額 { get; set; }
            public long 決算調整見込金額 { get; set; }
            public long? 決算調整後金額 { get; set; }
        }

        #endregion

        #region << 定数定義 >>

        /// <summary>送信パラメータ 対象販社</summary>
        private string PARAM_NAME_COMPANY = "対象販社";
        /// <summary>送信パラメータ 対象年度</summary>
        private string PARAM_NAME_YEAR = "処理年度";
        /// <summary>送信パラメータ 調整比率</summary>
        private string PARAM_NAME_RATE = "調整比率";

        #endregion

        Common com = new Common();


        #region 調整見込計算
        /// <summary>
        /// 調整見込計算をおこなった結果を取得する
        /// </summary>
        /// <param name="paramDic">パラメータDic</param>
        /// <returns></returns>
        public List<BSK05010_SearchMember> GetDataList(Dictionary<string, string> paramDic)
        {
            int compCd = int.Parse(paramDic[PARAM_NAME_COMPANY]),
                year = int.Parse(paramDic[PARAM_NAME_YEAR].Replace("/", ""));
            decimal rate = decimal.Parse(paramDic[PARAM_NAME_RATE]);

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var jis =
                    context.M70_JIS
                        .Where(w => w.削除日時 == null && w.自社コード == compCd)
                        .FirstOrDefault();

                // 決算月から請求年月の範囲を取得
                int month = jis.決算月 ?? CommonConstants.DEFAULT_SETTLEMENT_MONTH;
                DateTime priodEndDate = new DateTime(month < 4 ? year + 1 : year, month, 1);
                DateTime priodStrDate = priodEndDate.AddMonths(-11);
                int sYearMonth = priodStrDate.Year * 100 + priodStrDate.Month,
                    eYearMonth = priodEndDate.Year * 100 + priodEndDate.Month;

                DateTime targetDate = priodStrDate;
                List<string> targetList = new List<string>();
                while (targetDate <= priodEndDate)
                {
                    targetList.Add(targetDate.ToString("yyyy/MM"));
                    targetDate = targetDate.AddMonths(1);
                }

                var urData = getSalesAjustData(context, year, compCd);

                var seiData =
                    context.S01_SEIHD
                        .Where(w => w.請求先コード == jis.取引先コード &&
                            w.請求先枝番 == jis.枝番 &&
                            w.請求年月 >= sYearMonth && w.請求年月 <= eYearMonth)
                        .GroupBy(g => new { g.自社コード, g.請求年月 })
                        .OrderBy(o => o.Key.請求年月)
                        .ToList()
                        .Select(s => new
                        {
                            決算対象年月 = string.Format("{0}/{1:D2}", s.Key.請求年月 / 100, s.Key.請求年月 % 100),
                            決算調整前金額 = s.Sum(m => m.売上額) - s.Sum(m => m.値引額),
                        })
                        .Select(s => new BSK05010_SearchMember
                        {
                            決算対象年月 = s.決算対象年月,
                            決算調整前金額 = s.決算調整前金額,
                            決算調整見込金額 = (long)Math.Round(s.決算調整前金額 * (rate / 100 + 1), 0),
                        });

                var result =
                    targetList.AsQueryable()
                        .GroupJoin(seiData,
                            x => x,
                            y => y.決算対象年月,
                            (x, y) => new { x, y })
                        .SelectMany(z => z.y.DefaultIfEmpty(),
                            (a, b) => new { 対象年月 = a.x, SEI = b })
                        .ToList()
                        .Select(s => new BSK05010_SearchMember
                            {
                                決算対象年月 = s.対象年月,
                                決算調整前金額 = s.SEI != null ? s.SEI.決算調整前金額 : 0,
                                決算調整見込金額 = s.SEI != null ? s.SEI.決算調整見込金額 : 0,
                                決算調整後金額 = urData.ContainsKey(s.対象年月) ? urData[s.対象年月] : (long?)null
                            });

                return result.ToList();
            
            }

        }
        #endregion

        #region 調整後売上調整情報取得
        /// <summary>
        /// 調整後の売上集計情報を取得する
        /// </summary>
        /// <param name="context"></param>
        /// <param name="year"></param>
        /// <param name="compCd"></param>
        /// <returns></returns>
        private Dictionary<string, long?> getSalesAjustData(TRAC3Entities context, int year, int compCd)
        {
            Dictionary<string, long?> resultDic = new Dictionary<string, long?>();

            // 取引先情報取得
            var hanData =
                context.M70_JIS
                    .Where(w => w.削除日時 == null && w.自社コード == compCd)
                    .Join(context.M01_TOK,
                        x => new { code = x.取引先コード ?? 0, eda = x.枝番 ?? 0 },
                        y => new { code = y.取引先コード, eda = y.枝番 },
                        (x, y) => new { JIS = x, TOK = y })
                    .FirstOrDefault();

            // 決算月・請求締日から売上集計期間を算出する
            int pMonth = hanData.JIS.決算月 ?? CommonConstants.DEFAULT_SETTLEMENT_MONTH,
                pYear = pMonth < 4 ? year + 1 : year;

            DateTime lastMonth = new DateTime(pYear, pMonth, 1);
            DateTime targetMonth = lastMonth.AddMonths(-11);

            while (targetMonth <= lastMonth)
            {
                // 開始日は前月締日を設定
                // No.101-3 Mod Start
                DateTime calcStartDate =
                    AppCommon.GetClosingDate(targetMonth.AddMonths(-1).Year, targetMonth.AddMonths(-1).Month, hanData.TOK.Ｔ締日 ?? CommonConstants.DEFAULT_CLOSING_DAY);
                // 終了日は当月締日の前日を設定
                DateTime calcEndDate =
                    AppCommon.GetClosingDate(targetMonth.Year, targetMonth.Month, hanData.TOK.Ｔ締日 ?? CommonConstants.DEFAULT_CLOSING_DAY).AddDays(-1);
                // No.101-3 Mod End

                var ajustData =
                    context.T02_URHD_HAN
                        .Where(w => w.削除日時 == null &&
                            w.販社コード == compCd &&
                            w.売上日 >= calcStartDate && w.売上日 <= calcEndDate)
                        .Join(context.T02_URDTL_HAN.Where(w => w.削除日時 == null),
                            x => x.伝票番号,
                            y => y.伝票番号,
                            (a, b) => new { UHD = a, UDTL = b });

                long? ajustPrice =
                    ajustData.Sum(m =>
                        m.UHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ? m.UDTL.調整金額 : m.UDTL.調整金額 * -1) ?? 0;

                // 計算結果を格納
                resultDic.Add(targetMonth.ToString("yyyy/MM"), ajustPrice);

                // 次データの為に各値をカウントアップ
                targetMonth = targetMonth.AddMonths(1);

            }

            return resultDic;

        }
        #endregion


        #region 調整計算
        /// <summary>
        /// 調整計算をおこなう
        /// </summary>
        /// <param name="paramDic">パラメータDic</param>
        /// <param name="userId">ログインユーザID</param>
        public List<BSK05010_SearchMember> SetCalculate(Dictionary<string, string> paramDic, int userId)
        {
            int compCd = int.Parse(paramDic[PARAM_NAME_COMPANY]),
                year = int.Parse(paramDic[PARAM_NAME_YEAR].Replace("/", ""));
            decimal rate = decimal.Parse(paramDic[PARAM_NAME_RATE]);
            M73 zeiService = new M73();

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    try
                    {
                        var hanData =
                            context.M70_JIS
                                .Where(w => w.削除日時 == null && w.自社コード == compCd)
                                .Join(context.M01_TOK,
                                    x => new { code = x.取引先コード ?? 0, eda = x.枝番 ?? 0 },
                                    y => new { code = y.取引先コード, eda = y.枝番 },
                                    (x, y) => new { JIS = x, TOK = y })
                                .FirstOrDefault();

                        // 決算月・請求締日から売上集計期間を算出する
                        int pMonth = hanData.JIS.決算月 ?? CommonConstants.DEFAULT_SETTLEMENT_MONTH,
                            pYear = pMonth < 4 ? year + 1 : year;

                        // 締日前日が集計最終日
                        // No.101-3 Mod Start
                        DateTime priodEndDate =
                            AppCommon.GetClosingDate(pYear, pMonth, hanData.TOK.Ｔ締日 ?? CommonConstants.DEFAULT_CLOSING_DAY).AddDays(-1);
                        // No.101-3 Mod End

                        // 最終日から１２ヶ月遡って翌日を集計開始日とする
                        DateTime priodStrDate = priodEndDate.AddMonths(-12).AddDays(1);

                        //debugLog.WriteLine(string.Format("集計期間：{0}～{1}", priodStrDate.ToString("yyyy/MM/dd"), priodEndDate.ToString("yyyy/MM/dd")));

                        #region 販社明細の調整計算(単価・金額)
                        // 対象期間内の販社売上明細を取得
                        var dtlList =
                            context.T02_URDTL_HAN
                                .Where(w =>
                                    w.削除日時 == null &&
                                    context.T02_URHD_HAN
                                        .Where(v => v.削除日時 == null &&
                                            v.販社コード == compCd &&
                                            v.売上日 >= priodStrDate && v.売上日 <= priodEndDate)
                                        .Select(s => s.伝票番号)
                                    .Contains(w.伝票番号));

                        foreach (var data in dtlList)
                        {
                            decimal calcRate = rate / 100m + 1;
                            // 調整比率を調整額として反映
                            data.調整単価 = Math.Round(data.単価 * calcRate, 0);
                            data.調整金額 = decimal.ToInt32(Math.Round((data.金額 ?? 0) * calcRate, 0));

                            //debugLog.WriteLine(string.Format("Rate:{0:#,0.##}", calcRate));
                            //debugLog.WriteLine(string.Format("単価:{0:#,0}　⇒　{1:#,0.##}", data.単価, data.調整単価));
                            //debugLog.WriteLine(string.Format("金額:{0:#,0}　⇒　{1:#,0.##}", data.金額, data.調整金額));
                            //debugLog.WriteLine("--------------------");

                            data.AcceptChanges();

                        }
                        #endregion

                        // 変更状態を確定
                        context.SaveChanges();

                        #region 販社ヘッダの調整計算(消費税)
                        var hdList =
                            context.T02_URHD_HAN
                                .Where(w => w.削除日時 == null &&
                                    w.販社コード == compCd &&
                                    w.売上日 >= priodStrDate && w.売上日 <= priodEndDate);

                        foreach (var data in hdList)
                        {
                            int sumTax = 0;
                            foreach (var row in context.T02_URDTL_HAN.Where(w => w.削除日時 == null && w.伝票番号 == data.伝票番号))
                            {
                                // No.101-3 Mod Start
                                sumTax += decimal.ToInt32(
                                    zeiService.getCalculatTax(hanData.TOK.Ｔ消費税区分, data.売上日, row.品番コード, row.調整金額 ?? 0, row.数量));
                                // No.101-3 Mod End
                            }

                            data.調整消費税 = sumTax;
                            data.調整比率 = rate;
                            data.AcceptChanges();

                        }

                        #endregion

                        // 変更状態を確定
                        context.SaveChanges();

                        // トランザクションコミット
                        tran.Commit();

                    }
                    catch (Exception ex)
                    {
                        // トランザクションロールバック
                        tran.Rollback();
                        throw ex;
                    }

                }// end transaction

            }

            // データを再取得して返却
            return GetDataList(paramDic);

        }
        #endregion


        #region 調整確定
        /// <summary>
        /// 調整計算確定処理をおこなう
        /// </summary>
        /// <param name="paramDic">パラメータDic</param>
        /// <param name="userId">ログインユーザID</param>
        public void SetConfirm(Dictionary<string, string> paramDic, int userId)
        {
            int compCd = int.Parse(paramDic[PARAM_NAME_COMPANY]),
                year = int.Parse(paramDic[PARAM_NAME_YEAR].Replace("/", ""));
            decimal rate = decimal.Parse(paramDic[PARAM_NAME_RATE]);

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    try
                    {
                        // 取引先情報取得
                        var hanData =
                            context.M70_JIS
                                .Where(w => w.削除日時 == null && w.自社コード == compCd)
                                .Join(context.M01_TOK,
                                    x => new { code = x.取引先コード ?? 0, eda = x.枝番 ?? 0 },
                                    y => new { code = y.取引先コード, eda = y.枝番 },
                                    (x, y) => new { JIS = x, TOK = y })
                                .FirstOrDefault();

                        // 決算月・請求締日から売上集計期間を算出する
                        int pMonth = hanData.JIS.決算月 ?? CommonConstants.DEFAULT_SETTLEMENT_MONTH,
                            pYear = pMonth < 4 ? year + 1 : year;

                        DateTime lastMonth = new DateTime(pYear, pMonth, 1);
                        DateTime targetMonth = lastMonth.AddMonths(-11);

                        while (targetMonth <= lastMonth)
                        {
                            // 開始日は前月締日を設定
                            // No.101-3 Mod Start
                            DateTime calcStartDate =
                                AppCommon.GetClosingDate(targetMonth.AddMonths(-1).Year, targetMonth.AddMonths(-1).Month, hanData.TOK.Ｔ締日 ?? CommonConstants.DEFAULT_CLOSING_DAY);
                            // 終了日は当月締日の前日を設定
                            DateTime calcEndDate =
                                AppCommon.GetClosingDate(targetMonth.Year, targetMonth.Month, hanData.TOK.Ｔ締日 ?? CommonConstants.DEFAULT_CLOSING_DAY).AddDays(-1);
                            // No.101-3 Mod End

                            var hdList =
                                context.T02_URHD_HAN
                                    .Where(v => v.削除日時 == null &&
                                        v.販社コード == compCd &&
                                        v.売上日 >= calcStartDate && v.売上日 <= calcEndDate);

                            foreach (var hdRow in hdList)
                            {
                                // 売上ヘッダ情報更新
                                hdRow.消費税 = hdRow.調整消費税 ?? 0;
                                hdRow.最終更新者 = userId;
                                hdRow.最終更新日時 = com.GetDbDateTime();

                                hdRow.AcceptChanges();

                                // 仕入情報が存在するか
                                var srhd =
                                    context.T03_SRHD_HAN.Where(w => w.削除日時 == null && w.伝票番号 == hdRow.伝票番号)
                                        .FirstOrDefault();

                                if (srhd != null)
                                {
                                    // 仕入がある場合は値を更新する
                                    srhd.消費税 = hdRow.調整消費税;
                                    srhd.最終更新者 = userId;
                                    srhd.最終更新日時 = com.GetDbDateTime();

                                    srhd.AcceptChanges();

                                }


                                // 売上明細情報更新
                                foreach (var dtlRow in context.T02_URDTL_HAN.Where(w => w.削除日時 == null && w.伝票番号 == hdRow.伝票番号))
                                {
                                    dtlRow.単価 = dtlRow.調整単価 ?? 0;
                                    dtlRow.金額 = dtlRow.調整金額;
                                    dtlRow.最終更新者 = userId;
                                    dtlRow.最終更新日時 = com.GetDbDateTime();

                                    dtlRow.AcceptChanges();

                                    if (srhd != null)
                                    {
                                        // 仕入があれば対象の仕入明細の値を更新する
                                        var srdtl =
                                            context.T03_SRDTL_HAN.Where(w => w.伝票番号 == dtlRow.伝票番号 && w.行番号 == dtlRow.行番号)
                                                .FirstOrDefault();

                                        if (srdtl == null)
                                            continue;

                                        srdtl.単価 = dtlRow.調整単価 ?? 0;
                                        srdtl.金額 = dtlRow.調整金額 ?? 0;
                                        srdtl.最終更新者 = userId;
                                        srdtl.最終更新日時 = com.GetDbDateTime();

                                        srdtl.AcceptChanges();

                                    }

                                }


                            }

                            // データ更新後に締集計をおこなう
                            TKS01010 clampService = new TKS01010();
                            // 集計処理に必要なデータを作成
                            var jis =
                                context.M70_JIS.Where(w =>
                                    w.削除日時 == null && w.自社区分 == (int)CommonConstants.自社区分.自社)
                                    .First();

                            TKS01010.TKS01010_SearchMember srcMem = new TKS01010.TKS01010_SearchMember();
                            srcMem.ID = string.Format("{0:D3} - {1:D2}", hanData.TOK.取引先コード, hanData.TOK.枝番);
                            srcMem.得意先コード = hanData.TOK.取引先コード;
                            srcMem.得意先枝番 = hanData.TOK.枝番;
                            srcMem.得意先名 = hanData.TOK.得意先名１;
                            // No.101-3 Mod Start
                            srcMem.締日 = hanData.TOK.Ｔ締日 ?? CommonConstants.DEFAULT_CLOSING_DAY;
                            // No.101-3 Mod End
                            srcMem.開始日付1 = calcStartDate;
                            srcMem.終了日付1 = calcEndDate;
                            // No.101-3 Mod Start
                            srcMem.入金日 =
                                AppCommon.GetClosingDate(targetMonth.Year, targetMonth.Month, hanData.TOK.Ｔ入金日１ ?? CommonConstants.DEFAULT_CLOSING_DAY, hanData.TOK.Ｔサイト１ ?? 0);    // No-169 Mod
                            // No.101-3 Mod End

                            List<TKS01010.TKS01010_SearchMember> list = new List<TKS01010.TKS01010_SearchMember>();
                            list.Add(srcMem);
                            DataTable dt = KESSVCEntry.ConvertListToDataTable(list);
                            DataSet ds = new DataSet();
                            ds.Tables.Add(dt);

                            // 集計実行
                            clampService.BillingAggregation(ds, jis.自社コード, targetMonth.Year * 100 + targetMonth.Month, userId);


                            // 次データの為に各値をカウントアップ
                            targetMonth = targetMonth.AddMonths(1);

                        }


                        // 変更状態を確定
                        context.SaveChanges();

                        // トランザクションコミット
                        tran.Commit();

                    }
                    catch (Exception ex)
                    {
                        // トランザクションロールバック
                        tran.Rollback();
                        throw ex;
                    }

                }// end transaction

            }

        }
        #endregion

    }

}