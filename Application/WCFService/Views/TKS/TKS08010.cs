using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 入金予定実績表サービスクラス
    /// </summary>
    public class TKS08010
    {
        #region 入金予定実績表メンバクラス
        public class TKS08010_Member
        {
            public string 得意先コード { get; set; }
            public string 得意先枝番 { get; set; }
            public string 得意先名 { get; set; }
            public int 入金日 { get; set; }
            public int サイト { get; set; }
            public string 対象年月１ { get; set; }
            public long? 入金予定額１ { get; set; }
            public long? 入金額１ { get; set; }
            public string 対象年月２ { get; set; }
            public long? 入金予定額２ { get; set; }
            public long? 入金額２ { get; set; }
            public string 対象年月３ { get; set; }
            public long? 入金予定額３ { get; set; }
            public long? 入金額３ { get; set; }
            public string 対象年月４ { get; set; }
            public long? 入金予定額４ { get; set; }
            public long? 入金額４ { get; set; }
            public string 対象年月５ { get; set; }
            public long? 入金予定額５ { get; set; }
            public long? 入金額５ { get; set; }
            public string 対象年月６ { get; set; }
            public long? 入金予定額６ { get; set; }
            public long? 入金額６ { get; set; }
            public long 入金滞留額 { get; set; }
        }
        #endregion

        #region << 定数定義 >>

        /// <summary>出力月数</summary>
        private const int REF_MONTHS = 6;

        #endregion


        #region CSV出力データ取得
        /// <summary>
        /// ＣＳＶ出力データを取得する
        /// </summary>
        /// <param name="condition">
        ///  == 検索条件 ==
        /// 自社コード
        /// 基準年月
        /// 入金締日
        /// 全入金日
        /// 得意先コード
        /// 得意先枝番
        /// 作成区分
        /// </param>
        /// <returns></returns>
        public List<TKS08010_Member> GetCsvData(Dictionary<string, string> condition)
        {
            List<TKS08010_Member> dt = getCommonData(condition);

            // 出力に不要な列を削除する
            //if(dt.Columns.Contains("ChangeTracker"))
            //    dt.Columns.Remove("ChangeTracker");

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
        /// 基準年月
        /// 入金締日
        /// 全入金日
        /// 得意先コード
        /// 得意先枝番
        /// 作成区分
        /// </param>
        /// <returns></returns>
        public List<TKS08010_Member> GetPrintData(Dictionary<string, string> condition)
        {
            List<TKS08010_Member> dt = getCommonData(condition);

            return dt;

        }
        #endregion


        #region 入金予定実績表の基本情報を取得
        /// <summary>
        /// 入金予定実績表の基本情報を取得する
        /// </summary>
        /// <param name="condition">
        ///  == 検索条件 ==
        /// 自社コード
        /// 基準年月
        /// 入金締日
        /// 全入金日
        /// 得意先コード
        /// 得意先枝番
        /// 作成区分
        /// </param>
        /// <returns></returns>
        private List<TKS08010_Member> getCommonData(Dictionary<string, string> condition)
        {
            // 検索パラメータを展開
            int myCompany, ReferenceYearMonth, createType;
            int? closingDay, customerCode, customerEda;
            bool isAllDays;

            // 入力パラメータを展開
            getFormParams(condition, out myCompany, out ReferenceYearMonth, out closingDay, out isAllDays, out customerCode, out customerEda, out createType);

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // Remarks:自社or販社の判定に必要
                var jis =
                    context.M70_JIS
                        .Where(w => w.削除日時 == null && w.自社コード == myCompany)
                        .FirstOrDefault();

                var baseTok =
                    context.M01_TOK
                        .Where(w => w.削除日時 == null && w.担当会社コード == myCompany);

                // -- 検索条件適用：得意先
                if (customerCode != null && customerEda != null)
                    baseTok = baseTok.Where(w => w.取引先コード == customerCode && w.枝番 == customerEda);

                List<int> kbnList = new List<int>();
                kbnList.Add((int)CommonConstants.取引区分.得意先);
                // 自社の場合は《取引区分=販社》を含めて検索する
                if (jis.自社区分 == (int)CommonConstants.自社区分.自社)
                    kbnList.Add((int)CommonConstants.取引区分.販社);

                #region 入金情報を取得
                var nyknList =
                    context.T11_NYKNHD.Where(w =>
                            w.削除日時 == null &&
                            w.入金先自社コード == myCompany &&
                            w.得意先コード != null &&
                            w.得意先枝番 != null)
                        .Join(context.T11_NYKNDTL.Where(w => w.削除日時 == null),
                            x => x.伝票番号,
                            y => y.伝票番号,
                            (x, y) => new {
                                x.入金先自社コード,
                                x.得意先コード,
                                x.得意先枝番,
                                x.入金日,
                                y.金額
                            })
                        .GroupBy(g => new { g.入金先自社コード, g.得意先コード, g.得意先枝番, g.入金日 })
                        .Select(s => new {
                            自社コード = s.Key.入金先自社コード,
                            得意先コード = s.Key.得意先コード,
                            得意先枝番 = s.Key.得意先枝番,
                            入金日 = s.Key.入金日,
                            金額 = s.Sum(m => m.金額)
                        });
                #endregion

                int monthAgo1 = REF_MONTHS - 1, monthAgo2 = REF_MONTHS - 2,
                    monthAgo3 = REF_MONTHS - 3, monthAgo4 = REF_MONTHS - 4,
                    monthAgo5 = REF_MONTHS - 5;

                List<TKS08010_Member> stackList = new List<TKS08010_Member>();
                for (int i = REF_MONTHS; i > 0; i--)
                {
                    DateTime refYearMonth =  new DateTime(ReferenceYearMonth / 100, ReferenceYearMonth % 100, 1);
                    DateTime targetYearMonth = refYearMonth.AddMonths(i * -1);
                    int yearMonth = (targetYearMonth.Year * 100) + targetYearMonth.Month;

                    // 入金情報の請求年月を取得
                    var nykn =
                        nyknList
                            .Join(context.S01_SEIHD.Where(w => w.請求年月 == yearMonth),
                                x => new { comp = x.自社コード, code = x.得意先コード ?? 0, eda = x.得意先枝番 ?? 0 },
                                y => new { comp = y.自社コード, code = y.請求先コード, eda = y.請求先枝番 },
                                (a, b) => new { NYKN = a, SHD = b })
                            .Where(w => w.NYKN.入金日 >= w.SHD.集計開始日 && w.NYKN.入金日 <= w.SHD.集計最終日)
                            .GroupBy(g => new { g.NYKN.自社コード, g.NYKN.得意先コード, g.NYKN.得意先枝番, g.SHD.請求年月 })
                            .Select(s => new
                            {
                                s.Key.自社コード,
                                s.Key.得意先コード,
                                s.Key.得意先枝番,
                                s.Key.請求年月,
                                金額 = s.Sum(m => m.NYKN.金額)
                            });

                    var data =
                        baseTok
                            .GroupJoin(context.S01_SEIHD.Where(w => w.自社コード == myCompany && w.請求年月 == yearMonth),
                                x => new { code = x.取引先コード, eda = x.枝番 },
                                y => new { code = y.請求先コード, eda = y.請求先枝番 },
                                (x, y) => new { x, y })
                            .SelectMany(s => s.y.DefaultIfEmpty(),
                                (a, b) => new { TOK = a.x, SHD = b })
                            .GroupJoin(nykn,
                                x => new { comp = x.SHD.自社コード, code = x.SHD.請求先コード, eda = x.SHD.請求先枝番, ym = x.SHD.請求年月 },
                                y => new { comp = y.自社コード, code = y.得意先コード ?? 0, eda = y.得意先枝番 ?? 0, ym = y.請求年月 },
                                (x, y) => new { x, y })
                            .SelectMany(s => s.y.DefaultIfEmpty(),
                                (c, d) => new { c.x.TOK, c.x.SHD, NYKN = d })
                            .ToList()
                            .Select(s => new TKS08010_Member
                            {
                                得意先コード = s.TOK.取引先コード.ToString(),
                                得意先枝番 = s.TOK.枝番.ToString(),
                                得意先名 = s.TOK.得意先名１,
                                // No.101-4 Mod Start
                                入金日 = s.TOK.Ｔ入金日１ ?? 31,
                                サイト = s.TOK.Ｔサイト１ ?? 0,
                                // No.101-4 Mod End
                                対象年月１ = refYearMonth.AddMonths(REF_MONTHS * -1).ToString("yyyy/MM"),
                                入金予定額１ = (i == REF_MONTHS && s.SHD != null) ? s.SHD.当月請求額 : (long?)null,
                                入金額１ = (i == REF_MONTHS && s.NYKN != null) ? s.NYKN.金額 : (long?)null,
                                対象年月２ = refYearMonth.AddMonths(monthAgo1 * -1).ToString("yyyy/MM"),
                                入金予定額２ = (i == monthAgo1 && s.SHD != null) ? s.SHD.当月請求額 : (long?)null,
                                入金額２ = (i == monthAgo1 && s.NYKN != null) ? s.NYKN.金額 : (long?)null,
                                対象年月３ = refYearMonth.AddMonths(monthAgo2 * -1).ToString("yyyy/MM"),
                                入金予定額３ = (i == monthAgo2 && s.SHD != null) ? s.SHD.当月請求額 : (long?)null,
                                入金額３ = (i == monthAgo2 && s.NYKN != null) ? s.NYKN.金額 : (long?)null,
                                対象年月４ = refYearMonth.AddMonths(monthAgo3 * -1).ToString("yyyy/MM"),
                                入金予定額４ = (i == monthAgo3 && s.SHD != null) ? s.SHD.当月請求額 : (long?)null,
                                入金額４ = (i == monthAgo3 && s.NYKN != null) ? s.NYKN.金額 : (long?)null,
                                対象年月５ = refYearMonth.AddMonths(monthAgo4 * -1).ToString("yyyy/MM"),
                                入金予定額５ = (i == monthAgo4 && s.SHD != null) ? s.SHD.当月請求額 : (long?)null,
                                入金額５ = (i == monthAgo4 && s.NYKN != null) ? s.NYKN.金額 : (long?)null,
                                対象年月６ = refYearMonth.AddMonths(monthAgo5 * -1).ToString("yyyy/MM"),
                                入金予定額６ = (i == monthAgo5 && s.SHD != null) ? s.SHD.当月請求額 : (long?)null,
                                入金額６ = (i == monthAgo5 && s.NYKN != null) ? s.NYKN.金額 : (long?)null
                            });

                    // 取得結果をリストに格納
                    stackList.AddRange(data.ToList());

                }

                // 取得結果を集計してデータを作成する
                var result =
                    stackList.AsEnumerable()
                        .GroupBy(g => new
                        {
                            g.得意先コード,
                            g.得意先枝番,
                            g.得意先名,
                            g.入金日,
                            g.サイト,
                            g.対象年月１,
                            g.対象年月２,
                            g.対象年月３,
                            g.対象年月４,
                            g.対象年月５,
                            g.対象年月６
                        })
                        .Select(s => new TKS08010_Member
                        {
                            得意先コード = s.Key.得意先コード,
                            得意先枝番 = s.Key.得意先枝番,
                            得意先名 = s.Key.得意先名,
                            入金日 = s.Key.入金日,
                            サイト = s.Key.サイト,
                            対象年月１ = s.Key.対象年月１,
                            入金予定額１ = s.Sum(m => m.入金予定額１),
                            入金額１ = s.Sum(m => m.入金額１),
                            対象年月２ = s.Key.対象年月２,
                            入金予定額２ = s.Sum(m => m.入金予定額２),
                            入金額２ = s.Sum(m => m.入金額２),
                            対象年月３ = s.Key.対象年月３,
                            入金予定額３ = s.Sum(m => m.入金予定額３),
                            入金額３ = s.Sum(m => m.入金額３),
                            対象年月４ = s.Key.対象年月４,
                            入金予定額４ = s.Sum(m => m.入金予定額４),
                            入金額４ = s.Sum(m => m.入金額４),
                            対象年月５ = s.Key.対象年月５,
                            入金予定額５ = s.Sum(m => m.入金予定額５),
                            入金額５ = s.Sum(m => m.入金額５),
                            対象年月６ = s.Key.対象年月６,
                            入金予定額６ = s.Sum(m => m.入金予定額６),
                            入金額６ = s.Sum(m => m.入金額６),
                            入金滞留額 =
                                s.Sum(m => m.入金予定額１ ?? 0 + m.入金予定額２ ?? 0 + m.入金予定額３ ?? 0 + m.入金予定額４ ?? 0 + m.入金予定額５ ?? 0 + m.入金予定額６ ?? 0) -
                                s.Sum(m => m.入金額１ ?? 0 + m.入金額２ ?? 0 + m.入金額３ ?? 0 + m.入金額４ ?? 0 + m.入金額５ ?? 0 + m.入金額６ ?? 0)
                        });

                // -- 検索条件適用：入金締日
                if (!isAllDays && closingDay != null)
                    result = result.Where(w => w.入金日 == closingDay);

                // -- 検索条件適用：作成区分(1:滞留ありのみ、2:滞留なし含む)
                if (createType == 1)
                    result = result.Where(w => w.入金滞留額 != 0);           // No152 Mod

                return result.ToList();

            }

        }
        #endregion

        #region パラメータ展開
        /// <summary>
        /// フォームパラメータを展開する
        /// </summary>
        /// <param name="condition">検索条件辞書</param>
        /// <param name="myCompany">自社コード</param>
        /// <param name="ReferenceYearMonth">基準年月(yyyymm)</param>
        /// <param name="closingDay">入金締日</param>
        /// <param name="isAllDays">全入金日を対象とするか</param>
        /// <param name="customerCode">得意先コード</param>
        /// <param name="customerEda">得意先枝番</param>
        /// <param name="createType">作成区分</param>
        private void getFormParams(
            Dictionary<string, string> condition,
            out int myCompany,
            out int ReferenceYearMonth,
            out int? closingDay,
            out bool isAllDays,
            out int? customerCode,
            out int? customerEda,
            out int createType )
        {
            int ival = -1;

            myCompany = int.Parse(condition["自社コード"]);
            ReferenceYearMonth = int.Parse(condition["基準年月"].Replace("/", ""));
            closingDay = int.TryParse(condition["入金締日"], out ival) ? ival : (int?)null;
            isAllDays = bool.Parse(condition["全入金日"]);
            customerCode = int.TryParse(condition["得意先コード"], out ival) ? ival : (int?)null;
            customerEda = int.TryParse(condition["得意先枝番"], out ival) ? ival : (int?)null;
            createType = int.Parse(condition["作成区分"]);

        }
        #endregion

    }

}
