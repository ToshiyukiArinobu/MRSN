using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 入金予定表サービスクラス
    /// </summary>
    public class TKS07010
    {
        #region << メンバクラス定義 >>

        /// <summary>
        /// 入金データメンバクラス
        /// </summary>
        public class NyknMember
        {
            public int 入金先自社コード { get; set; }
            public string 入金年月 { get; set; }
            public int 得意先コード { get; set; }
            public int 得意先枝番 { get; set; }
            public int 金額 { get; set; }
        }

        /// <summary>
        /// 帳票印刷メンバクラス
        /// </summary>
        public class PrintMenber
        {
            public int 自社コード { get; set; }
            public string 自社名 { get; set; }
            public string 得意先コード { get; set; }
            public string 得意先名 { get; set; }
            public long 売上額 { get; set; }
            public long 消費税 { get; set; }
            public long 回収予定額 { get; set; }
            public string 請求年月 { get; set; }             // yyyy/MM
            public int 締日 { get; set; }
            public long 現金・振込・手形 { get; set; }
            public long 手形 { get; set; }
            public string 入金予定日 { get; set; }           // yyyy/MM/dd
            public string 期日 { get; set; }                 // yyyy/MM/dd
        }
        #endregion

        #region CSV出力データ取得
        /// <summary>
        /// ＣＳＶ出力データを取得する
        /// </summary>
        /// <param name="condition">
        ///  == 検索条件 ==
        /// 自社コード
        /// 入金年月
        /// 入金日
        /// 全入金日
        /// 得意先コード
        /// 得意先枝番
        /// 作成区分
        /// </param>
        /// <returns></returns>
        public List<PrintMenber> GetCsvData(Dictionary<string, string> condition)
        {
            List<PrintMenber> result = getCommonData(condition);

            return result;

        }
        #endregion

        #region 帳票出力データ取得
        /// <summary>
        /// 帳票出力データを取得する
        /// </summary>
        /// <param name="condition">
        ///  == 検索条件 ==
        /// 自社コード
        /// 入金年月
        /// 入金日
        /// 全入金日
        /// 得意先コード
        /// 得意先枝番
        /// 作成区分
        /// </param>
        /// <returns></returns>
        public List<PrintMenber> GetPrintData(Dictionary<string, string> condition)
        {
            List<PrintMenber> result = getCommonData(condition);

            return result;

        }
        #endregion

        #region 入金予定表の基本情報を取得  No.395 Mod
        /// <summary>
        /// 入金予定表の基本情報を取得する
        /// </summary>
        /// <param name="condition">
        ///  == 検索条件 ==
        /// 自社コード
        /// 入金年月
        /// 入金日
        /// 全入金日
        /// 得意先コード
        /// 得意先枝番
        /// 作成区分
        /// </param>
        /// <returns></returns>
        private List<PrintMenber> getCommonData(Dictionary<string, string> condition)
        {
            // 検索パラメータを展開
            int myCompany, paymentYearMonth, createType;
            int? paymentDay, customerCode, customerEda;
            bool isAllDays;

            getFormParams(condition, out myCompany, out paymentYearMonth, out paymentDay, out isAllDays, out customerCode, out customerEda, out createType);

            try
            {
                using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
                {
                    // 対象として取引区分：得意先、相殺、販社を対象とする
                    List<int> kbnList = new List<int>() { (int)CommonConstants.取引区分.得意先, (int)CommonConstants.取引区分.相殺, (int)CommonConstants.取引区分.販社 };

                    // 集計得意先を取得
                    List<M01_TOK> tokList =
                                context.M01_TOK.Where(w => w.削除日時 == null &&
                                                            kbnList.Contains(w.取引区分) &&
                                                            w.担当会社コード == myCompany).ToList();
                    // 請求情報取得
                    var seiData = context.S01_SEIHD
                                    .Where(w => w.自社コード == myCompany && w.請求年月 == paymentYearMonth).ToList();

                    #region 条件絞り込み
                    // 締日が指定されている場合
                    if (paymentDay != null)
                    {
                        tokList = tokList.Where(w => w.Ｔ入金日１ == paymentDay).ToList();
                    }

                    // 取引先が指定されている場合
                    if (customerCode != null && customerEda != null)
                    {
                        tokList = tokList.Where(w => w.取引先コード == customerCode && w.枝番 == customerEda).ToList();
                    }

                    if (customerCode != null && customerEda == null)
                    {
                        tokList = tokList.Where(w => w.取引先コード == customerCode).ToList();
                    }
                    #endregion

                    tokList = tokList.OrderBy(o => o.取引先コード).ThenBy(t => t.枝番).ToList();

                    #region 集計処理
                    // 得意先毎に集計を実施
                    List<PrintMenber> resultList = new List<PrintMenber>();
                    foreach (M01_TOK tok in tokList)
                    {
                        var resultData = seiData.Where(w => w.請求先コード == tok.取引先コード &&
                                                            w.請求先枝番 == tok.枝番)
                                        .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                                            x => x.自社コード,
                                            y => y.自社コード,
                                            (x, y) => new { x, y })
                                        .SelectMany(z => z.y.DefaultIfEmpty(),
                                            (a, b) => new { SHD = a.x, JIS = b })
                                        .ToList()
                                        .Select(s => new PrintMenber
                                         {
                                             自社コード = s.SHD.自社コード,
                                             自社名 = s.JIS.自社名,
                                             得意先コード = string.Format("{0:D4} - {1:D2}", tok.取引先コード, tok.枝番),
                                             得意先名 = tok.略称名,
                                             売上額 = s.SHD.売上額,
                                             消費税 = s.SHD.消費税,
                                             回収予定額 = s.SHD.売上額 + s.SHD.消費税,
                                             請求年月 = s.SHD.請求年月.ToString().Insert(4, "/"),
                                             締日 = s.SHD.請求締日,

                                             現金・振込・手形 = tok.Ｔサイト２ == null ? s.SHD.売上額 + s.SHD.消費税 :
                                                                    tok.Ｔ入金日２ == null ? s.SHD.売上額 + s.SHD.消費税 :

                                                                    // Ｔ請求区分:1(以上)の場合
                                                                    // 請求条件金額の手形をxx枚発行し残金を現金とする
                                                                    tok.Ｔ請求区分 == (int)CommonConstants.請求・支払区分.ID01_以上 ?
                                                                        tok.Ｔ請求条件 > 0 ?
                                                                            (s.SHD.売上額 + s.SHD.消費税) % tok.Ｔ請求条件 :
                                                                            0 :

                                                                    // Ｔ請求区分:(以下)の場合
                                                                    // 請求条件金額の手形を発行し、残金を現金とする
                                                                    tok.Ｔ請求条件 > 0 ?
                                                                        (s.SHD.売上額 + s.SHD.消費税) - tok.Ｔ請求条件 > 0 ?
                                                                            (s.SHD.売上額 + s.SHD.消費税) - tok.Ｔ請求条件 :
                                                                            s.SHD.売上額 + s.SHD.消費税 :
                                                                        // 手形請求条件金額に満たない場合、全額を現金に振り分ける
                                                                        s.SHD.売上額 + s.SHD.消費税,

                                             手形 = tok.Ｔサイト２ == null ? 0 :
                                                        tok.Ｔ入金日２ == null ? 0 :

                                                            // Ｔ請求区分:1(以上)の場合
                                                            // 請求条件金額の手形をxx枚発行する
                                                            tok.Ｔ請求区分 == (int)CommonConstants.請求・支払区分.ID01_以上 ?
                                                                tok.Ｔ請求条件 > 0 ?
                                                                    ((s.SHD.売上額 + s.SHD.消費税) / tok.Ｔ請求条件) * tok.Ｔ請求条件 :
                                                                    s.SHD.売上額 + s.SHD.消費税 :

                                                            // Ｔ請求区分:2(以下)の場合
                                                            // 請求条件金額の手形を発行する
                                                            s.SHD.売上額 + s.SHD.消費税 - tok.Ｔ請求条件 >= 0 ?
                                                                tok.Ｔ請求条件 :
                                                                0,

                                             入金予定日 = s.SHD.入金日.ToString().Insert(4, "/").Insert(7, "/"),
                                             期日 = tok.Ｔサイト２ == null ? null :
                                                        tok.Ｔ入金日２ == null ? null :
                                                            AppCommon.GetClosingDate(s.SHD.請求年月 / 100,
                                                                                        s.SHD.請求年月 % 100, 
                                                                                        (int)tok.Ｔ入金日２,
                                                                                        (int)tok.Ｔサイト２).ToShortDateString()

                                         });


                        resultList.AddRange(resultData);

                    }
                    #endregion

                    // 条件絞り込み
                    // 入金予定ありのみ
                    if (createType == 1)
                    {
                        resultList = resultList.Where(w => w.回収予定額 != 0).ToList();
                    }

                    return resultList
                        .OrderBy(o => o.自社コード)
                        .ThenBy(t => t.入金予定日)
                        .ThenBy(t => t.締日)
                        .ThenBy(t => t.得意先コード)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region パラメータ展開
        /// <summary>
        /// フォームパラメータを展開する
        /// </summary>
        /// <param name="condition">検索条件辞書</param>
        /// <param name="myCompany">自社コード</param>
        /// <param name="paymentYearMonth">入金年月(yyyymm)</param>
        /// <param name="paymentDay">入金日</param>
        /// <param name="isAllDays">全入金日を対象とするか</param>
        /// <param name="customerCode">得意先コード</param>
        /// <param name="customerEda">得意先枝番</param>
        /// <param name="createType">作成区分</param>
        private void getFormParams(
            Dictionary<string, string> condition,
            out int myCompany,
            out int paymentYearMonth,
            out int? paymentDay,
            out bool isAllDays,
            out int? customerCode,
            out int? customerEda,
            out int createType )
        {
            int ival = -1;

            myCompany = int.Parse(condition["自社コード"]);
            paymentYearMonth = int.Parse(condition["入金年月"].Replace("/", ""));
            paymentDay = int.TryParse(condition["入金日"], out ival) ? ival : (int?)null;
            isAllDays = bool.Parse(condition["全入金日"]);
            customerCode = int.TryParse(condition["得意先コード"], out ival) ? ival : (int?)null;
            customerEda = int.TryParse(condition["得意先枝番"], out ival) ? ival : (int?)null;
            createType = int.Parse(condition["作成区分"]);

        }
        #endregion
    }

}
