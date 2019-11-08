using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 仕入データ一覧表サービスクラス
    /// </summary>
    public class SHR01010
    {
        #region << 列挙型定義 >>
        /// <summary>
        /// 印刷区分
        /// </summary>
        private enum 印刷区分 : int
        {
            集約する = 1,
            集約しない = 2
        }
        #endregion

        #region 項目クラス定義
        /// <summary>
        /// SHR01010_仕入データ一覧表帳票項目定義
        /// </summary>
        private class PrintMember
        {
            public string 仕入先コード { get; set; }
            public string 仕入先名称 { get; set; }
            public long 前月繰越 { get; set; }
            public long 出金金額 { get; set; }
            public long 値引額 { get; set; }
            public long 通常税率対象支払額 { get; set; }
            public long 通常税消費税 { get; set; }
            public long 税込支払額 { get; set; }
            public long 軽減税率対象支払額 { get; set; }
            public long 軽減税消費税 { get; set; }
            public long 軽減税込支払額 { get; set; }
            public long 非課税支払額 { get; set; }
            public long 当月支払額 { get; set; }
        }
        #endregion

        #region CSV出力データ取得
        /// <summary>
        /// ＣＳＶ出力データを取得する
        /// </summary>
        /// <param name="condition">
        ///  == 検索条件 ==
        /// 自社コード
        /// 作成年月
        /// 全締日
        /// 得意先コード
        /// 得意先枝番
        /// 作成区分
        /// </param>
        /// <returns></returns>
        public DataTable GetCsvData(Dictionary<string, string> condition)
        {
            DataTable dt = PaymentAggregation(condition);
            return dt;

        }
        #endregion

        #region 帳票出力データ取得
        /// <summary>
        /// 帳票出力データを取得する
        /// </summary>
        /// <param name="condition">
        ///  == 検索条件 ==
        /// 自社コード
        /// 作成年月
        /// 全締日
        /// 得意先コード
        /// 得意先枝番
        /// 作成区分
        /// </param>
        /// <returns></returns>
        public DataTable GetPrintData(Dictionary<string, string> condition)
        {
            DataTable dt = PaymentAggregation(condition);
            return dt;

        }
        #endregion


        #region 仕入集計処理
        /// <summary>
        /// 仕入集計処理
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable PaymentAggregation(Dictionary<string, string> condition)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // 検索パラメータを展開
                int myCompany, createYearMonth, createType, userId;
                int? customerCode, customerEda;

                getFormParams(condition, out myCompany, out createYearMonth, out customerCode, out customerEda, out createType, out userId);

                try
                {
                    context.Connection.Open();

                    // 対象として取引区分：仕入先、相殺、販社を対象とする
                    List<int> kbnList = new List<int>() { (int)CommonConstants.取引区分.仕入先, (int)CommonConstants.取引区分.相殺, (int)CommonConstants.取引区分.販社 };

                    // 集計得意先を取得
                    List<M01_TOK> tokList =
                                context.M01_TOK.Where(w => w.削除日時 == null && kbnList.Contains(w.取引区分)).ToList();

                    // 取引先が指定されていれば条件追加
                    if (customerCode != null && customerEda != null)
                    {
                        tokList = tokList.Where(w => w.取引先コード == customerCode && w.枝番 == customerEda).ToList();
                    }

                    if (customerCode != null && customerEda == null)
                    {
                        tokList = tokList.Where(w => w.取引先コード == customerCode).ToList();
                    }

                    tokList = tokList.OrderBy(o => o.取引先コード).ThenBy(t => t.枝番).ToList();

                    try
                    {
                        foreach (var tok in tokList)
                        {
                            // 集計処理
                            getAggregateData(context, myCompany, createYearMonth, tok.取引先コード, tok.枝番, userId);
                        }

                        context.SaveChanges();

                        // 仕入一覧データ取得
                        DataTable dt = getData(context, myCompany, createYearMonth, tokList, createType);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region 集計処理
        /// <summary>
        /// 集計処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">自社コード</param>
        /// <param name="yearMonth">請求年月</param>
        /// <param name="code">取引先コード</param>
        /// <param name="eda">枝番</param>
        /// <param name="userId">userId</param>
        public void getAggregateData(TRAC3Entities context, int company, int yearMonth, int code, int eda, int userId)
        {
            DateTime targetStDate = new DateTime(yearMonth / 100, yearMonth % 100, 1);
            DateTime targetEdDate = new DateTime(yearMonth / 100, yearMonth % 100, 1).AddMonths(1).AddDays(-1);

            // 対象の取引先が「販社」の場合は販社仕入を参照する為、ロジックを分岐する
            var tokdata =
                context.M01_TOK.Where(w => w.削除日時 == null && w.取引先コード == code && w.枝番 == eda)
                    .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null && w.取引先コード != null && w.枝番 != null),
                        x => new { code = x.取引先コード, eda = x.枝番 },
                        y => new { code = (int)y.取引先コード, eda = (int)y.枝番 },
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(),
                        (a, b) => new { TOK = a.x, JIS = b })
                    .FirstOrDefault();

            DateTime paymentDate;
            // 入金日の算出
            try
            {
                paymentDate =
                    AppCommon.GetClosingDate(targetStDate.Year, targetStDate.Month, tokdata.TOK.Ｓ入金日１ ?? CommonConstants.DEFAULT_CLOSING_DAY, tokdata.TOK.Ｓサイト１ ?? 0);
            }
            catch
            {
                // 基本的にあり得ないがこの場合は当月末日を指定
                paymentDate = new DateTime(targetStDate.Year, targetStDate.Month, DateTime.DaysInMonth(targetStDate.Year, targetStDate.Month));
            }

            if (tokdata.JIS != null && tokdata.JIS.自社区分 == CommonConstants.自社区分.販社.GetHashCode())
            {
                // ヘッダ情報の登録
                setHeaderInfoHan(context, company, yearMonth, tokdata.JIS.自社コード, targetStDate, targetEdDate, code, eda, paymentDate, userId);
            }
            else
            {
                // ヘッダ情報の登録
                setHeaderInfo(context, company, yearMonth, code, eda, targetStDate, targetEdDate, paymentDate, userId);
            }
        }
        #endregion

        #region 仕入一覧ヘッダ登録処理
        /// <summary>
        /// 仕入一覧ヘッダ登録処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">自社コード</param>
        /// <param name="yearMonth">作成年月</param>
        /// <param name="code">支払先コード</param>
        /// <param name="eda">支払先枝番</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        /// <param name="paymentDate">入金日</param>
        /// <param name="userId">ログインユーザID</param>
        private void setHeaderInfo(TRAC3Entities context, int company, int yearMonth, int code, int eda,
                                        DateTime? targetStDate, DateTime? targetEdDate, DateTime paymentDate, int userId)
        {
            int cnt = 1;
            SHR03010 shr03010 = new SHR03010();

            // ヘッダ情報取得
            S02_SHRHD shrHd = shr03010.getHeaderInfo(context, company, yearMonth, code, eda, cnt, targetStDate, targetEdDate, paymentDate, userId);

            // 都度請求の場合はヘッダデータを作成しない
            if (shrHd == null)
            {
                return;
            }

            // 前月残高の再設定
            S07_SRIHD befData = getLastChargeInfo(context, company, yearMonth, code, eda, cnt);
            shrHd.前月残高 = befData == null ? 0 : befData.当月支払額;

            // 繰越金額、当月残高の再計算
            shrHd.繰越残高 = shrHd.前月残高 - shrHd.出金額;
            shrHd.当月支払額 = shrHd.繰越残高 + shrHd.支払額 + shrHd.消費税;

            // ヘッダ情報の整形
            S07_SRIHD s07data = ConvertToS07_SRIHD_Entity(shrHd);

            // ヘッダ情報登録
            S07_SRIHD_Update(context, s07data);
        }
        #endregion

        #region 仕入一覧ヘッダ登録処理(販社)
        /// <summary>
        /// 仕入一覧ヘッダ登録処理(販社)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company"></param>
        /// <param name="yearMonth"></param>
        /// <param name="salesCompanyCode"></param>
        /// <param name="targetStDate"></param>
        /// <param name="targetEdDate"></param>
        /// <param name="code"></param>
        /// <param name="eda"></param>
        /// <param name="paymentDate"></param>
        /// <param name="userId"></param>
        private void setHeaderInfoHan(TRAC3Entities context, int myCompanyCode, int yearMonth, int salesCompanyCode, DateTime targetStDate, DateTime targetEdDate, 
                                        int code, int eda, DateTime paymentDate, int userId)
        {
            int cnt = 1;
            SHR03010 shr03010 = new SHR03010();

            // ヘッダ情報取得(販社)
            S02_SHRHD shrHd = shr03010.getHeaderInfoHan(context, myCompanyCode, yearMonth, salesCompanyCode, cnt, targetStDate, targetEdDate, paymentDate, userId);

            // 都度請求の場合はヘッダデータを作成しない
            if (shrHd == null)
            {
                return;
            }

            // 前月残高の再設定
            S07_SRIHD befData = getLastChargeInfo(context, myCompanyCode, yearMonth, code, eda, cnt);
            shrHd.前月残高 = befData == null ? 0 : befData.当月支払額;

            // 繰越金額、当月残高の再計算
            shrHd.繰越残高 = shrHd.前月残高 - shrHd.出金額;
            shrHd.当月支払額 = shrHd.繰越残高 + shrHd.支払額 + shrHd.消費税;

            // ヘッダ情報の整形
            S07_SRIHD s07data = ConvertToS07_SRIHD_Entity(shrHd);

            // ヘッダ情報登録
            S07_SRIHD_Update(context, s07data);
        }
        #endregion

        #region 前月情報取得
        /// <summary>
        /// 前月情報取得
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">自社コード</param>
        /// <param name="yearMonth">支払年月</param>
        /// <param name="code">支払先コード</param>
        /// <param name="eda">支払先枝番</param>
        /// <param name="cnt">回数</param>
        /// <returns></returns>
        private S07_SRIHD getLastChargeInfo(TRAC3Entities context, int company, int yearMonth, int code, int eda, int cnt)
        {
            // 前回請求情報取得
            DateTime befCntMonth = new DateTime(yearMonth / 100, yearMonth % 100, 1);
            if (cnt == 1)
            {
                befCntMonth = new DateTime(yearMonth / 100, yearMonth % 100, 1).AddMonths(-1);
            }

            var befSeiCnt =
                context.S07_SRIHD
                    .Where(w => w.自社コード == company &&
                        w.支払年月 == (befCntMonth.Year * 100 + befCntMonth.Month) &&
                        w.支払先コード == code &&
                        w.支払先枝番 == eda)
                    .OrderByDescending(o => o.回数)
                    .FirstOrDefault();

            return befSeiCnt;
        }
        #endregion

        #region 仕入一覧ヘッダ更新処理
        /// <summary>
        /// 仕入一覧ヘッダ更新処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="hdData"></param>
        private void S07_SRIHD_Update(TRAC3Entities context, S07_SRIHD hdData)
        {
            var srihd =
                context.S07_SRIHD.Where(w =>
                    w.自社コード == hdData.自社コード &&
                    w.支払年月 == hdData.支払年月 &&
                    w.支払先コード == hdData.支払先コード &&
                    w.支払先枝番 == hdData.支払先枝番 &&
                    w.支払日 == hdData.支払日 &&
                    w.回数 == hdData.回数)
                    .FirstOrDefault();

            if (srihd == null)
            {
                // 登録なしなので登録をおこなう
                S07_SRIHD data = new S07_SRIHD();

                data.自社コード = hdData.自社コード;
                data.支払年月 = hdData.支払年月;
                data.支払締日 = hdData.支払締日;
                data.支払先コード = hdData.支払先コード;
                data.支払先枝番 = hdData.支払先枝番;
                data.支払日 = hdData.支払日;
                data.回数 = hdData.回数;
                data.支払年月日 = hdData.支払年月日;
                data.集計開始日 = hdData.集計開始日;
                data.集計最終日 = hdData.集計最終日;
                data.前月残高 = hdData.前月残高;
                data.出金額 = hdData.出金額;
                data.繰越残高 = hdData.繰越残高;
                data.支払額 = hdData.支払額;
                data.値引額 = hdData.値引額;
                data.非課税支払額 = hdData.非課税支払額;
                data.通常税率対象金額 = hdData.通常税率対象金額;
                data.軽減税率対象金額 = hdData.軽減税率対象金額;
                data.通常税率消費税 = hdData.通常税率消費税;
                data.軽減税率消費税 = hdData.軽減税率消費税;
                data.消費税 = hdData.消費税;
                data.当月支払額 = hdData.当月支払額;
                data.登録者 = hdData.登録者;
                data.登録日時 = DateTime.Now;

                context.S07_SRIHD.ApplyChanges(data);

            }
            else
            {
                // 登録済みなので更新をおこなう
                srihd.支払年月日 = hdData.支払年月日;
                srihd.集計開始日 = hdData.集計開始日;
                srihd.集計最終日 = hdData.集計最終日;
                srihd.前月残高 = hdData.前月残高;
                srihd.出金額 = hdData.出金額;
                srihd.繰越残高 = hdData.繰越残高;
                srihd.支払額 = hdData.支払額;
                srihd.値引額 = hdData.値引額;
                srihd.非課税支払額 = hdData.非課税支払額;
                srihd.通常税率対象金額 = hdData.通常税率対象金額;
                srihd.軽減税率対象金額 = hdData.軽減税率対象金額;
                srihd.通常税率消費税 = hdData.通常税率消費税;
                srihd.軽減税率消費税 = hdData.軽減税率消費税;
                srihd.消費税 = hdData.消費税;
                srihd.当月支払額 = hdData.当月支払額;
                srihd.登録者 = hdData.登録者;
                srihd.登録日時 = DateTime.Now;

                srihd.AcceptChanges();

            }

        }
        #endregion

        #region 仕入一覧データ取得
        /// <summary>
        /// 仕入一覧データ取得
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="company">会社コード</param>
        /// <param name="yearMonth">作成年月</param>
        /// <param name="tokList">支払先リスト</param>
        /// <param name="printKbn">印刷区分 0:集約する 1:集約しない</param>
        /// <returns></returns>
        private DataTable getData(TRAC3Entities context, int company, int yearMonth, List<M01_TOK> tokList, int printKbn)
        {
            DataTable dt = new DataTable();

            // 仕入一覧データ取得
            List<S07_SRIHD> sriList = getHeaderData(context, company, yearMonth, tokList);

            if (sriList == null)
            {
                return null;
            }

            // 取得データを統合して結果リストを作成
            if (printKbn == 印刷区分.集約しない.GetHashCode())
            {
                var resultList =
                    sriList.GroupJoin(tokList,
                        x => new { コード = x.支払先コード, 枝番 = x.支払先枝番 },
                        y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y,
                        (a, b) => new { SHD = a.x, TOK = b })
                    .GroupBy(g => new
                    {
                        g.SHD.自社コード,
                        g.SHD.支払年月,
                        g.SHD.支払先コード,
                        g.SHD.支払先枝番,
                        g.TOK.略称名,
                        g.TOK.得意先名１
                    })
                    .Select(x => new PrintMember
                    {
                        仕入先コード = string.Format("{0:0000} - {1:00}", x.Key.支払先コード, x.Key.支払先枝番),
                        仕入先名称 = x.Key.略称名 == null ? x.Key.得意先名１ : x.Key.略称名,
                        前月繰越 = (long)x.Sum(s => s.SHD.前月残高),
                        出金金額 = (long)x.Sum(s => s.SHD.出金額),
                        通常税率対象支払額 = (long)x.Sum(s => s.SHD.通常税率対象金額),
                        軽減税率対象支払額 = (long)x.Sum(s => s.SHD.軽減税率対象金額),
                        通常税消費税 = (long)x.Sum(s => s.SHD.通常税率消費税),
                        軽減税消費税 = (long)x.Sum(s => s.SHD.軽減税率消費税),
                        税込支払額 = (long)x.Sum(s => s.SHD.通常税率対象金額) + (long)x.Sum(s => s.SHD.通常税率消費税),
                        軽減税込支払額 = (long)x.Sum(s => s.SHD.軽減税率対象金額) + (long)x.Sum(s => s.SHD.軽減税率消費税),
                        非課税支払額 = (long)x.Sum(s => s.SHD.非課税支払額),
                        当月支払額 = (long)x.Sum(s => s.SHD.当月支払額),
                    }).ToList();

                resultList = resultList.OrderBy(o => o.仕入先コード).ToList();
                dt = KESSVCEntry.ConvertListToDataTable<PrintMember>(resultList);
            }
            else
            {
                List<PrintMember> hanshaList = new List<PrintMember>();
                List<PrintMember> wkList = new List<PrintMember>();

                // 販社リスト取得
                var hanList =
                    tokList
                    .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null && w.自社区分 == (int)CommonConstants.自社区分.販社 && w.取引先コード != null && w.枝番 != null),
                        x => new { code = x.取引先コード, eda = x.枝番 },
                        y => new { code = (int)y.取引先コード, eda = (int)y.枝番 },
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y,
                        (a, b) => new { HAN = a.x, JIS = b }).ToList();

                if (hanList.Count() > 0)
                {
                    // 販社は集約しせず抽出
                    hanshaList =
                        sriList.GroupJoin(hanList,
                            x => new { コード = x.支払先コード, 枝番 = x.支払先枝番 },
                            y => new { コード = y.HAN.取引先コード, 枝番 = y.HAN.枝番 },
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y,
                            (a, b) => new { SHD = a.x, TOK = b })
                        .GroupBy(g => new
                        {
                            g.SHD.自社コード,
                            g.SHD.支払年月,
                            g.SHD.支払先コード,
                            g.SHD.支払先枝番,
                            g.TOK.HAN.略称名,
                            g.TOK.HAN.得意先名１
                        })
                        .Select(x => new PrintMember
                        {
                            仕入先コード = string.Format("{0:0000} - {1:00}", x.Key.支払先コード, x.Key.支払先枝番),
                            仕入先名称 = x.Key.略称名 == null ? x.Key.得意先名１ : x.Key.略称名,
                            前月繰越 = (long)x.Sum(s => s.SHD.前月残高),
                            出金金額 = (long)x.Sum(s => s.SHD.出金額),
                            通常税率対象支払額 = (long)x.Sum(s => s.SHD.通常税率対象金額),
                            軽減税率対象支払額 = (long)x.Sum(s => s.SHD.軽減税率対象金額),
                            通常税消費税 = (long)x.Sum(s => s.SHD.通常税率消費税),
                            軽減税消費税 = (long)x.Sum(s => s.SHD.軽減税率消費税),
                            税込支払額 = (long)x.Sum(s => s.SHD.通常税率対象金額) + (long)x.Sum(s => s.SHD.通常税率消費税),
                            軽減税込支払額 = (long)x.Sum(s => s.SHD.軽減税率対象金額) + (long)x.Sum(s => s.SHD.軽減税率消費税),
                            非課税支払額 = (long)x.Sum(s => s.SHD.非課税支払額),
                            当月支払額 = (long)x.Sum(s => s.SHD.当月支払額),
                        }).ToList();
                }

                // 販社以外の得意先は集約して抽出
                tokList = tokList.Where(x => hanList.Select(s => s.HAN.取引先コード).Contains(x.取引先コード) == false).ToList();

                if (tokList.Count > 0)
                {
                    wkList =
                    sriList.GroupJoin(tokList,
                            x => new { コード = x.支払先コード, 枝番 = x.支払先枝番 },
                            y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y,
                            (a, b) => new { SHD = a.x, TOK = b })
                        .GroupBy(g => new
                        {
                            g.SHD.自社コード,
                            g.SHD.支払年月,
                            g.SHD.支払先コード,
                            g.TOK.得意先名１
                            //g.TOK.略称名
                        })
                        .Select(x => new PrintMember
                        {
                            仕入先コード = string.Format("{0:0000} - 00", x.Key.支払先コード),
                            仕入先名称 = x.Key.得意先名１ == null ? "" : x.Key.得意先名１,
                            前月繰越 = (long)x.Sum(s => s.SHD.前月残高),
                            出金金額 = (long)x.Sum(s => s.SHD.出金額),
                            通常税率対象支払額 = (long)x.Sum(s => s.SHD.通常税率対象金額),
                            軽減税率対象支払額 = (long)x.Sum(s => s.SHD.軽減税率対象金額),
                            通常税消費税 = (long)x.Sum(s => s.SHD.通常税率消費税),
                            軽減税消費税 = (long)x.Sum(s => s.SHD.軽減税率消費税),
                            税込支払額 = (long)x.Sum(s => s.SHD.通常税率対象金額) + (long)x.Sum(s => s.SHD.通常税率消費税),
                            軽減税込支払額 = (long)x.Sum(s => s.SHD.軽減税率対象金額) + (long)x.Sum(s => s.SHD.軽減税率消費税),
                            非課税支払額 = (long)x.Sum(s => s.SHD.非課税支払額),
                            当月支払額 = (long)x.Sum(s => s.SHD.当月支払額),
                        }).ToList();
                }

                // 販社リストと得意先リストを結合
                var resultList = hanshaList.ToList().Concat(wkList);

                resultList = resultList.OrderBy(o => o.仕入先コード).ToList();
                dt = KESSVCEntry.ConvertListToDataTable<PrintMember>(resultList.ToList());
            }

            return dt;
        }
        #endregion

        #region 仕入一覧ヘッダデータ取得
        /// <summary>
        /// 仕入一覧ヘッダデータ取得
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company"></param>
        /// <param name="yearMonth"></param>
        /// <param name="tokList"></param>
        /// <returns></returns>
        private List<S07_SRIHD> getHeaderData(TRAC3Entities context, int company, int yearMonth, List<M01_TOK> tokList)
        {
            List<S07_SRIHD> shdList = new List<S07_SRIHD>();
            foreach (M01_TOK tok in tokList)
            {
                List<S07_SRIHD> wk = context.S07_SRIHD.Where(w => w.自社コード == company && w.支払年月 == yearMonth &&
                                                    w.支払先コード == tok.取引先コード && w.支払先枝番 == tok.枝番 &&
                                                    w.当月支払額 != 0).ToList();

                shdList = shdList.Concat(wk).ToList(); 
            }

            return shdList;
        }

        #endregion

        #region パラメータ展開
        /// <summary>
        /// フォームパラメータを展開する
        /// </summary>
        /// <param name="condition">検索条件辞書</param>
        /// <param name="myCompany">自社コード</param>
        /// <param name="createYearMonth">作成年月(yyyymm)</param>
        /// <param name="closingDay">締日</param>
        /// <param name="isAllDays">全締日を対象とするか</param>
        /// <param name="customerCode">得意先コード</param>
        /// <param name="customerEda">得意先枝番</param>
        /// <param name="createType">作成区分</param>
        private void getFormParams(
            Dictionary<string, string> condition,
            out int myCompany,
            out int createYearMonth,
            out int? customerCode,
            out int? customerEda,
            out int createType,
            out int userId)
        {
            int ival = -1;

            myCompany = int.Parse(condition["自社コード"]);
            createYearMonth = int.Parse(condition["作成年月"].Replace("/", ""));
            customerCode = int.TryParse(condition["仕入先コード"], out ival) ? ival : (int?)null;
            customerEda = int.TryParse(condition["仕入先枝番"], out ival) ? ival : (int?)null;
            createType = int.Parse(condition["作成区分"]);            
            userId = int.Parse(condition["userId"]);

        }
        #endregion

        #region << サービス処理関連 >>

        #region S07_SRIHD_Entityへ変換
        /// <summary>
        /// S06_URIHD_Entityへ変換
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private S07_SRIHD ConvertToS07_SRIHD_Entity(S02_SHRHD row)
        {
            S07_SRIHD s07hd = new S07_SRIHD();

            s07hd.自社コード = row.自社コード;
            s07hd.支払年月 = row.支払年月;
            s07hd.支払締日 = row.支払締日;
            s07hd.支払先コード = row.支払先コード;
            s07hd.支払先枝番 = row.支払先枝番;
            s07hd.支払日 = row.支払日;
            s07hd.回数 = row.回数;
            s07hd.支払年月日 = row.支払年月日;
            s07hd.集計開始日 = row.集計開始日;
            s07hd.集計最終日 = row.集計最終日;
            s07hd.前月残高 = row.前月残高;
            s07hd.出金額 = row.出金額;
            s07hd.繰越残高 = row.繰越残高;
            s07hd.通常税率対象金額 = row.通常税率対象金額;
            s07hd.軽減税率対象金額 = row.軽減税率対象金額;
            s07hd.値引額 = row.値引額;
            s07hd.非課税支払額 = row.非課税支払額;
            s07hd.支払額 = row.支払額;
            s07hd.通常税率消費税 = row.通常税率消費税;
            s07hd.軽減税率消費税 = row.軽減税率消費税;
            s07hd.消費税 = row.消費税;
            s07hd.当月支払額 = row.当月支払額;
            s07hd.登録者 = row.登録者;

            return s07hd;
        }
        #endregion

        #endregion

    }

}
