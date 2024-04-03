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
    /// 売上データ一覧サービス
    /// </summary>
    public class TKS02010
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

        #region 拡張クラス定義
        /// <summary>
        /// TKS02010_売上データ一覧表帳票項目定義
        /// </summary>
        private class PrintMember
        {
            public int 自社コード { get; set; }          // No.227,228 Add
            public string 自社名 { get; set; }           // No.227,228 Add
            public string 得意先コード { get; set; }
            public string 得意先名称 { get; set; }
            public long 前月繰越 { get; set; }
            public long 入金額 { get; set; }
            public long 通常税率対象売上額 { get; set; }
            public long 通常税消費税 { get; set; }
            public long 税込売上額 { get; set; }
            public long 軽減税率対象売上額 { get; set; }
            public long 軽減税消費税 { get; set; }
            public long 軽減税込売上額 { get; set; }
            public long 非課税売上額 { get; set; }
            public long 当月残高 { get; set; }
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
        /// 得意先コード
        /// 得意先枝番
        /// 作成区分
        /// ユーザーID
        /// </param>
        /// <returns></returns>
        public DataTable GetPrintData(Dictionary<string, string> condition)
        {
            DataTable dt = SalesAggregation(condition);

            return dt;

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
        /// 得意先コード
        /// 得意先枝番
        /// 作成区分
        /// ユーザーID
        /// </param>
        /// <returns></returns>
        public DataTable GetCsvData(Dictionary<string, string> condition)
        {
            DataTable dt = SalesAggregation(condition);

            return dt;

        }
        #endregion

        #region 売上集計処理
        /// <summary>
        /// 売上集計処理
        /// </summary>
        /// <param name="自社コード"></param>
        /// <param name="作成年月"></param>
        /// <param name="得意先コード"></param>
        /// <param name="枝番"></param>
        /// <param name="印刷区分"></param>
        /// <param name="userId"></param>
        public DataTable SalesAggregation(Dictionary<string, string> condition)
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

                    // 対象として取引区分：得意先、相殺、販社を対象とする
                    List<int> kbnList = new List<int>() { (int)CommonConstants.取引区分.得意先, (int)CommonConstants.取引区分.相殺, (int)CommonConstants.取引区分.販社 };

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

                        // 売上一覧データ取得
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
        /// <param name="userId">ログインユーザID</param>
        public void getAggregateData(TRAC3Entities context, int company, int yearMonth, int code, int eda, int userId)
        {
            DateTime targetStDate = new DateTime(yearMonth / 100, yearMonth % 100, 1);
            DateTime targetEdDate = new DateTime(yearMonth / 100, yearMonth % 100, 1).AddMonths(1).AddDays(-1);

            // 対象の取引先が「販社」の場合は販社売上を参照する為、ロジックを分岐する
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
                    AppCommon.GetClosingDate(targetStDate.Year, targetStDate.Month, tokdata.TOK.Ｔ入金日１ ?? CommonConstants.DEFAULT_CLOSING_DAY, tokdata.TOK.Ｔサイト１ ?? 0);
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

        #region 売上一覧ヘッダ登録処理
        /// <summary>
        /// 売上一覧ヘッダ登録処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">自社コード</param>
        /// <param name="yearMonth">作成年月</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        /// <param name="paymentDate">入金日</param>
        /// <param name="userId">ログインユーザID</param>
        private void setHeaderInfo(TRAC3Entities context, int company, int yearMonth, int code, int eda,  
                                        DateTime? targetStDate, DateTime? targetEdDate, DateTime paymentDate, int userId)
        {
            int cnt = 1;
            TKS01010 tks01010 = new TKS01010();

            // ヘッダ情報取得
            S01_SEIHD urdata = tks01010.getHeaderInfo(context, company, yearMonth, code, eda, cnt, targetStDate, targetEdDate, paymentDate, userId, true);  // No.305 Mod

            // 都度請求の場合はヘッダデータを作成しない
            if (urdata == null)
            {
                return;
            }

            // 前月残高の再設定
            S06_URIHD befData = getLastChargeInfo(context, company, yearMonth, code, eda, cnt);
            urdata.前月残高 = befData == null? 0 : befData.当月請求額;

            // 繰越金額、当月残高の再計算
            urdata.繰越残高 = urdata.前月残高 - urdata.入金額;
            urdata.当月請求額 = urdata.繰越残高 + urdata.売上額 + urdata.消費税;

            // ヘッダ情報の整形
            S06_URIHD s06data = ConvertToS06_URIHD_Entity(urdata);

            // ヘッダ情報登録
            S06_URIHD_Update(context, s06data);
        }

        /// <summary>
        /// 売上一覧ヘッダ登録処理(販社)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="myCompanyCode">自社コード</param>
        /// <param name="yearMonth">請求年月(yyyymm)</param>
        /// <param name="salesCompanyCode">販社コード(M70_JIS)</param>
        /// <param name="code">取引先コード</param>
        /// <param name="eda">枝番</param>
        /// <param name="paymentDate">入金日</param>
        /// <param name="userId">ログインユーザID</param>
        private void setHeaderInfoHan(TRAC3Entities context, int myCompanyCode, int yearMonth, int salesCompanyCode, 
                                        DateTime? targetStDate, DateTime? targetEdDate, int? code, int? eda, DateTime paymentDate, int userId)
        {
            int cnt = 1;
            TKS01010 tks01010 = new TKS01010();
            
            // ヘッダ情報取得(販社)
            S01_SEIHD urdata = tks01010.getHeaderInfoHan(context, myCompanyCode, yearMonth, salesCompanyCode, cnt, targetStDate, targetEdDate, paymentDate, userId, true);  // No.305 Mod

            // 都度請求の場合はヘッダデータを作成しない
            if (urdata == null)
            {
                return;
            }

            // 前月残高の再設定
            S06_URIHD befData = getLastChargeInfo(context, myCompanyCode, yearMonth, code, eda, cnt);
            urdata.前月残高 = befData == null ? 0 : befData.当月請求額;

            // 繰越金額、当月残高の再計算
            urdata.繰越残高 = urdata.前月残高 - urdata.入金額;
            urdata.当月請求額 = urdata.繰越残高 + urdata.売上額 + urdata.消費税;

            // ヘッダ情報の整形
            S06_URIHD s06data = ConvertToS06_URIHD_Entity(urdata);
            
            // ヘッダ情報登録
            S06_URIHD_Update(context, s06data);

        }

        #endregion

        #region 前月情報取得

        /// <summary>
        /// 前月情報取得
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">会社名コード</param>
        /// <param name="yearMonth">作成年月</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="cnt">回数</param>
        public S06_URIHD getLastChargeInfo(TRAC3Entities context, int company, int yearMonth, int? code, int? eda, int cnt)
        {
            // 前回請求情報取得
            DateTime befCntMonth = new DateTime(yearMonth / 100, yearMonth % 100, 1);
            if (cnt == 1)
            {
                befCntMonth = new DateTime(yearMonth / 100, yearMonth % 100, 1).AddMonths(-1);
            }

            var befSeiCnt =
                context.S06_URIHD
                    .Where(w => w.自社コード == company &&
                        w.請求年月 == (befCntMonth.Year * 100 + befCntMonth.Month) &&
                        w.請求先コード == code &&
                        w.請求先枝番 == eda)
                    .OrderByDescending(o => o.回数)
                    .FirstOrDefault();

            return befSeiCnt;
        }

        #endregion

        #region 売上一覧ヘッダ更新処理
        /// <summary>
        /// 売上一覧ヘッダ更新処理(Del/Ins)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="hdData"></param>
        private void S06_URIHD_Update(TRAC3Entities context, S06_URIHD hdData)
        {
            // No.298 Mod Start del/Insに変更
            var urihd =
                context.S06_URIHD.Where(w =>
                    w.自社コード == hdData.自社コード &&
                    w.請求年月 == hdData.請求年月 &&
                    w.請求先コード == hdData.請求先コード &&
                    w.請求先枝番 == hdData.請求先枝番)
                    .ToList();

            if (urihd.Any())
            {
                // 登録済データを削除
                foreach (S06_URIHD dtl in urihd)
                {
                    context.S06_URIHD.DeleteObject(dtl);
                }
                context.SaveChanges();
            }

            S06_URIHD data = new S06_URIHD();

            data.自社コード = hdData.自社コード;
            data.請求年月 = hdData.請求年月;
            data.請求締日 = hdData.請求締日;
            data.請求先コード = hdData.請求先コード;
            data.請求先枝番 = hdData.請求先枝番;
            data.入金日 = hdData.入金日;
            data.回数 = hdData.回数;
            data.請求年月日 = hdData.請求年月日;
            data.集計開始日 = hdData.集計開始日;
            data.集計最終日 = hdData.集計最終日;
            data.前月残高 = hdData.前月残高;
            data.入金額 = hdData.入金額;
            data.繰越残高 = hdData.繰越残高;
            data.売上額 = hdData.売上額;
            data.値引額 = hdData.値引額;
            data.非税売上額 = hdData.非税売上額;
            data.通常税率対象金額 = hdData.通常税率対象金額;
            data.軽減税率対象金額 = hdData.軽減税率対象金額;
            data.通常税率消費税 = hdData.通常税率消費税;
            data.軽減税率消費税 = hdData.軽減税率消費税;
            data.消費税 = hdData.消費税;
            data.当月請求額 = hdData.当月請求額;
            data.登録者 = hdData.登録者;
            data.登録日時 = DateTime.Now;

            context.S06_URIHD.ApplyChanges(data);
            // No.298 Mod End
        }
        #endregion

        #region 売上一覧データ取得
        /// <summary>
        /// 売上一覧データ取得
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="company">会社コード</param>
        /// <param name="yearMonth">作成年月</param>
        /// <param name="tokList">得意先リスト</param>
        /// <param name="printKbn">印刷区分 0:集約する 1:集約しない</param>
        /// <returns></returns>
        private DataTable getData(TRAC3Entities context, int company, int yearMonth, List<M01_TOK> tokList, int printKbn)
        {
            DataTable dt = new DataTable();

            // 売上一覧データ取得
            List<S06_URIHD> uriList = getHeaderData(context, company, yearMonth, tokList);

            if (uriList == null)
            {
                return null;
            }

            // 取得データを統合して結果リストを作成
            if (printKbn == 印刷区分.集約しない.GetHashCode())
            {
                var resultList =
                    uriList.GroupJoin(tokList,
                        x => new { コード = x.請求先コード, 枝番 = x.請求先枝番 },
                        y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y,
                        (a, b) => new { UHD = a.x, TOK = b })
                    .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                        x => x.UHD.自社コード,
                        y => y.自社コード,
                        (x, y) => new { x, y})
                    .SelectMany(x => x.y.DefaultIfEmpty(),
                        (c, d) => new {c.x.UHD, c.x.TOK, JIS = d})
                    .GroupBy(g => new
                    {
                        g.UHD.自社コード,
                        g.JIS.自社名,
                        g.UHD.請求年月,
                        g.UHD.請求先コード,
                        g.UHD.請求先枝番,
                        g.TOK.略称名,
                        g.TOK.得意先名１
                    })
                    .Select(x => new PrintMember
                    {
                        自社コード = x.Key.自社コード,        // No.227,228 Add
                        自社名 = x.Key.自社名,                // No.227,228 Add
                        得意先コード = string.Format("{0:D4} - {1:D2}", x.Key.請求先コード, x.Key.請求先枝番),   // No.223 Mod
                        得意先名称 = x.Key.略称名 == null ? x.Key.得意先名１ : x.Key.略称名,
                        前月繰越 = (long)x.Sum(s => s.UHD.前月残高),
                        入金額 = (long)x.Sum(s => s.UHD.入金額),
                        通常税率対象売上額 = (long)x.Sum(s => s.UHD.通常税率対象金額),
                        軽減税率対象売上額 = (long)x.Sum(s => s.UHD.軽減税率対象金額),
                        通常税消費税 = (long)x.Sum(s => s.UHD.通常税率消費税),
                        軽減税消費税 = (long)x.Sum(s => s.UHD.軽減税率消費税),
                        税込売上額 = (long)x.Sum(s => s.UHD.通常税率対象金額) + (long)x.Sum(s => s.UHD.通常税率消費税),
                        軽減税込売上額 = (long)x.Sum(s => s.UHD.軽減税率対象金額) + (long)x.Sum(s => s.UHD.軽減税率消費税),
                        非課税売上額 = (long)x.Sum(s => s.UHD.非税売上額),
                        当月残高 = (long)x.Sum(s => s.UHD.当月請求額),
                    }).ToList();

                resultList = resultList.OrderBy(o => o.得意先コード).ToList();
                dt = KESSVCEntry.ConvertListToDataTable<PrintMember>(resultList);
            }
            else
            {
                List<PrintMember> hanshaList = new List<PrintMember>();
                List<PrintMember> wkList = new List<PrintMember>();

                // 販社リスト取得
                var hanList =
                    tokList.Where(c => c.取引先コード == (int)CommonConstants.取引先コード.販社)
                    .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null && w.自社区分 == (int)CommonConstants.自社区分.販社 &&　w.取引先コード != null && w.枝番 != null),
                        x => new { code = x.取引先コード, eda = x.枝番 },
                        y => new { code = (int)y.取引先コード, eda = (int)y.枝番 },
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(),
                        (a, b) => new { HAN = a.x, JIS = b }).ToList();
                
                if (hanList.Count() > 0)
                {
                    // 販社は集約しせず抽出
                    hanshaList =
                        uriList.GroupJoin(hanList,
                            x => new { コード = x.請求先コード, 枝番 = x.請求先枝番 },
                            y => new { コード = y.HAN.取引先コード, 枝番 = y.HAN.枝番 },
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y,
                            (a, b) => new { UHD = a.x, TOK = b })
                        .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                            x => x.UHD.自社コード,
                            y => y.自社コード,
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(),
                            (c, d) => new { c.x.UHD, c.x.TOK, JIS = d})
                        .GroupBy(g => new
                        {
                            g.UHD.自社コード,
                            g.JIS.自社名,
                            g.UHD.請求年月,
                            g.UHD.請求先コード,
                            g.UHD.請求先枝番,
                            g.TOK.HAN.略称名,
                            g.TOK.HAN.得意先名１
                        })
                        .Select(x => new PrintMember
                        {
                            自社コード = x.Key.自社コード,        // No.227,228 Add
                            自社名 = x.Key.自社名,                // No.227,228 Add
                            得意先コード = string.Format("{0:D4} - {1:D2}", x.Key.請求先コード, x.Key.請求先枝番),
                            得意先名称 = x.Key.略称名 == null ? x.Key.得意先名１ : x.Key.略称名,
                            前月繰越 = (long)x.Sum(s => s.UHD.前月残高),
                            入金額 = (long)x.Sum(s => s.UHD.入金額),
                            通常税率対象売上額 = (long)x.Sum(s => s.UHD.通常税率対象金額),
                            軽減税率対象売上額 = (long)x.Sum(s => s.UHD.軽減税率対象金額),
                            通常税消費税 = (long)x.Sum(s => s.UHD.通常税率消費税),
                            軽減税消費税 = (long)x.Sum(s => s.UHD.軽減税率消費税),
                            税込売上額 = (long)x.Sum(s => s.UHD.通常税率対象金額) + (long)x.Sum(s => s.UHD.通常税率消費税),
                            軽減税込売上額 = (long)x.Sum(s => s.UHD.軽減税率対象金額) + (long)x.Sum(s => s.UHD.軽減税率消費税),
                            非課税売上額 = (long)x.Sum(s => s.UHD.非税売上額),
                            当月残高 = (long)x.Sum(s => s.UHD.当月請求額),
                        }).ToList();
                }

                // 販社以外の得意先は集約して抽出
                tokList = tokList.Where(x => hanList.Select(s => s.HAN.取引先コード).Contains(x.取引先コード) == false).ToList();

                if (tokList.Count > 0)
                {
                    wkList = 
                    uriList.GroupJoin(tokList,
                            x => new { コード = x.請求先コード, 枝番 = x.請求先枝番 },
                            y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y,
                            (a, b) => new { UHD = a.x, TOK = b })
                        .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                            x => x.UHD.自社コード,
                            y => y.自社コード,
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(),
                            (c, d) => new { c.x.UHD, c.x.TOK, JIS = d })
                        .GroupBy(g => new
                        {
                            g.UHD.自社コード,
                            g.JIS.自社名,
                            g.UHD.請求年月,
                            g.UHD.請求先コード,
                            //g.TOK.略称名,
                            //g.TOK.得意先名１
                        })
                        .Select(x => new PrintMember
                        {
                            自社コード = x.Key.自社コード,        // No.227,228 Add
                            自社名 = x.Key.自社名,                // No.227,228 Add
                            得意先コード = string.Format("{0:0000} - 00", x.Key.請求先コード),
                            得意先名称 = (from m01 in context.M01_TOK.Where(c => c.取引先コード == x.Key.請求先コード) select m01.略称名 ).First(),
                            前月繰越 = (long)x.Sum(s => s.UHD.前月残高),
                            入金額 = (long)x.Sum(s => s.UHD.入金額),
                            通常税率対象売上額 = (long)x.Sum(s => s.UHD.通常税率対象金額),
                            軽減税率対象売上額 = (long)x.Sum(s => s.UHD.軽減税率対象金額),
                            通常税消費税 = (long)x.Sum(s => s.UHD.通常税率消費税),
                            軽減税消費税 = (long)x.Sum(s => s.UHD.軽減税率消費税),
                            税込売上額 = (long)x.Sum(s => s.UHD.通常税率対象金額) + (long)x.Sum(s => s.UHD.通常税率消費税),
                            軽減税込売上額 = (long)x.Sum(s => s.UHD.軽減税率対象金額) + (long)x.Sum(s => s.UHD.軽減税率消費税),
                            非課税売上額 = (long)x.Sum(s => s.UHD.非税売上額),
                            当月残高 = (long)x.Sum(s => s.UHD.当月請求額),
                        }).ToList();
                }

                // 販社リストと得意先リストを結合
                var resultList = hanshaList.ToList().Concat(wkList);

                resultList = resultList.OrderBy(o => o.得意先コード).ToList();
                dt = KESSVCEntry.ConvertListToDataTable<PrintMember>(resultList.ToList());
            }
            
            return dt;
        }
        #endregion

        #region 売上一覧ヘッダデータ取得
        /// <summary>
        /// 売上一覧ヘッダデータ取得
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company"></param>
        /// <param name="yearMonth"></param>
        /// <param name="tokList"></param>
        /// <returns></returns>
        private List<S06_URIHD> getHeaderData(TRAC3Entities context, int company, int yearMonth, List<M01_TOK> tokList)
        {
            List<S06_URIHD> uriList = new List<S06_URIHD>();
            foreach (M01_TOK tok in tokList)
            {
                List<S06_URIHD> wk = context.S06_URIHD.Where(w => w.自社コード == company && w.請求年月 == yearMonth &&
                                                w.請求先コード == tok.取引先コード && w.請求先枝番 == tok.枝番
                                                && (w.当月請求額 != 0 || w.売上額 != 0 || w.前月残高 != 0 || w.入金額 != 0 )
                                                ).ToList();

                uriList = uriList.Concat(wk).ToList();
            }

            return uriList;
        }

        #endregion

        #region パラメータ展開
        /// <summary>
        /// フォームパラメータを展開する
        /// </summary>
        /// <param name="condition">検索条件辞書</param>
        /// <param name="myCompany">自社コード</param>
        /// <param name="createYearMonth">作成年月(yyyymm)</param>
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
            customerCode = int.TryParse(condition["得意先コード"], out ival) ? ival : (int?)null;
            customerEda = int.TryParse(condition["得意先枝番"], out ival) ? ival : (int?)null;
            createType = int.Parse(condition["作成区分"]);
            userId = int.Parse(condition["userId"]);
        }
        #endregion

        #region << サービス処理関連 >>

        #region S06_URIHD_Entityへ変換
        /// <summary>
        /// S06_URIHD_Entityへ変換
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private S06_URIHD ConvertToS06_URIHD_Entity(S01_SEIHD row)
        {
            S06_URIHD s06hd = new S06_URIHD();

            s06hd.自社コード = row.自社コード;
            s06hd.請求年月 = row.請求年月;
            s06hd.請求締日 = row.請求締日;
            s06hd.請求先コード = row.請求先コード;
            s06hd.請求先枝番 = row.請求先枝番;
            s06hd.入金日 = row.入金日;
            s06hd.回数 = row.回数;
            s06hd.請求年月日 = row.請求年月日;
            s06hd.集計開始日 = row.集計開始日;
            s06hd.集計最終日 = row.集計最終日;
            s06hd.前月残高 = row.前月残高;
            s06hd.入金額 = row.入金額;
            s06hd.繰越残高 = row.繰越残高;
            s06hd.通常税率対象金額 = row.通常税率対象金額;
            s06hd.軽減税率対象金額 = row.軽減税率対象金額;
            s06hd.値引額 = row.値引額;
            s06hd.非税売上額 = row.非税売上額;
            s06hd.売上額 = row.売上額;
            s06hd.通常税率消費税 = row.通常税率消費税;
            s06hd.軽減税率消費税 = row.軽減税率消費税;
            s06hd.消費税 = row.消費税;
            s06hd.当月請求額 = row.当月請求額;
            s06hd.登録者 = row.登録者;

            return s06hd;
        }
        #endregion

        #endregion

    }

}
