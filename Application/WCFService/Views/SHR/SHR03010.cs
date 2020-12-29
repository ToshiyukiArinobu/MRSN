using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 支払締集計サービスクラス
    /// </summary>
    public class SHR03010
    {
        #region << 項目クラス定義 >>

        /// <summary>
        /// 支払対象検索メンバークラス
        /// </summary>
        public class SHR03010_SearchMember
        {
            public string ID { get; set; }
            public int 支払先コード { get; set; }
            public int 支払先枝番 { get; set; }
            public string 支払先名 { get; set; }
            public int 締日 { get; set; }
            public string 区分 { get; set; }
            public DateTime? 開始日付1 { get; set; }
            public DateTime? 終了日付1 { get; set; }
            public DateTime? 開始日付2 { get; set; }
            public DateTime? 終了日付2 { get; set; }
            public DateTime? 開始日付3 { get; set; }
            public DateTime? 終了日付3 { get; set; }
            public DateTime クリア開始日付 { get; set; }
            public DateTime クリア終了日付 { get; set; }
            public DateTime 入金日 { get; set; }
        }

        // No-84 Start
        public class SHR03010_SRDataMember
        {
            public int 伝票番号 { get; set; }
            public int 会社名コード { get; set; }
            public System.DateTime 仕入日 { get; set; }
            public int 入力区分 { get; set; }
            public int 仕入区分 { get; set; }
            public int 仕入先コード { get; set; }
            public int 仕入先枝番 { get; set; }
            public int 入荷先コード { get; set; }
            public Nullable<int> 発注番号 { get; set; }
            public string 備考 { get; set; }
            public Nullable<int> 元伝票番号 { get; set; }
            public Nullable<int> 通常税率対象金額 { get; set; }
            public Nullable<int> 軽減税率対象金額 { get; set; }
            public Nullable<int> 非課税金額 { get; set; }
            public Nullable<int> 通常税率消費税 { get; set; }
            public Nullable<int> 軽減税率消費税 { get; set; }
            public Nullable<int> 消費税 { get; set; }
            public Nullable<int> 当月支払額 { get; set; }
            public Nullable<int> Ｓ締日 { get; set; }
            public Nullable<int> Ｓ入金日１ { get; set; }
            public int Ｓ支払消費税区分 { get; set; }
            public int Ｓ税区分ID { get; set; }
            public Nullable<int> 消費税区分 { get; set; }
        }
        // No-84 End

        #endregion
        #region 拡張クラス定義
        /// <summary>
        /// 出金検索クラス定義
        /// </summary>
        public class T12_PAY_Search_Extension
        {
            public int 得意先コード { get; set; }
            public int 得意先枝番 { get; set; }
            public int 合計金額 { get; set; }
        }
        #endregion
        #region 支払集計対象の得意先リストを取得
        /// <summary>
        /// 支払集計対象の得意先リストを取得する
        /// </summary>
        /// <param name="自社コード"></param>
        /// <param name="作成年月"></param>
        /// <param name="作成締日"></param>
        /// <param name="支払先コード"></param>
        /// <param name="支払先枝番"></param>
        /// <returns></returns>
        public List<SHR03010_SearchMember> GetListData(string 自社コード, string 作成年月, string 作成締日, string 支払先コード, string 支払先枝番)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                #region パラメータ型変換

                int iVal;
                int company = int.TryParse(自社コード, out iVal) ? iVal : -1;
                int yearMonth = int.TryParse(作成年月.Replace("/", ""), out iVal) ? iVal : -1;
                int? closingDays = int.TryParse(作成締日, out iVal) ? iVal : (int?)null;
                int? code = int.TryParse(支払先コード, out iVal) ? iVal : (int?)null;
                int? eda = int.TryParse(支払先枝番, out iVal) ? iVal : (int?)null;

                #endregion

                #region 検索対象の支払情報取得

                var SHRHD =
                    context.S02_SHRHD.Where(w => w.自社コード == company && w.支払年月 == yearMonth);

                // 作成締日絞込
                if (closingDays != null)
                    SHRHD = SHRHD.Where(w => w.支払締日 == closingDays);

                // 支払先(仕入先)絞込
                if (code != null && eda != null)
                    SHRHD = SHRHD.Where(w => w.支払先コード == code && w.支払先枝番 == eda);

                #endregion

                #region 検索対象の取引先情報取得

                // 検索対象の取引区分リスト
                List<int> kbnList = new List<int>() {
                    (int)CommonConstants.取引区分.仕入先,
                    (int)CommonConstants.取引区分.相殺,
                    (int)CommonConstants.取引区分.加工先,               // No-84
                    (int)CommonConstants.取引区分.販社
                };

                // REMARKS:自社と同一の取引先は含まない
                var TOK =
                    context.M01_TOK
                        .Where(w =>
                            w.削除日時 == null &&
                            w.担当会社コード == company &&
                            kbnList.Contains(w.取引区分) &&
                            !(context.M70_JIS.Where(v => v.削除日時 == null &&
                                v.自社区分 == (int)CommonConstants.自社区分.自社)
                            .Any(v => v.取引先コード == w.取引先コード && v.枝番 == w.枝番)));

                // -- 自社(本社)情報取得
                var jis =
                    context.M70_JIS
                        .Where(w => w.削除日時 == null && w.自社コード == company)
                        .FirstOrDefault();

                if (jis != null && jis.自社区分 == (int)CommonConstants.自社区分.販社)
                {
                    // 販社の場合は自社(マルセン)を仕入先として追加する
                    var wkJis =
                        context.M70_JIS
                            .Where(w => w.削除日時 == null && w.自社区分 == (int)CommonConstants.自社区分.自社)
                            .FirstOrDefault();

                    TOK =
                        TOK.Union(
                            context.M01_TOK.Where(w =>
                                w.削除日時 == null && 
                                w.取引先コード == wkJis.取引先コード &&
                                w.枝番 == wkJis.枝番
                            )
                        );

                }

                // 作成締日絞込
                if (closingDays != null)
                    TOK = TOK.Where(w => w.Ｓ締日 == closingDays);

                // 得意先(支払先)絞込
                if (code != null && eda != null)
                {
                    TOK = TOK.Where(w => w.取引先コード == code && w.枝番 == eda);
                }
                else if (code != null)
                {
                    TOK = TOK.Where(w => w.取引先コード == code);
                }
                #endregion

                #region 検索情報の取得
                var result = TOK
                    .GroupJoin(SHRHD.Where(w => w.回数 == 1),
                        x => new { コード = x.取引先コード, 枝番 = x.枝番 },
                        y => new { コード = y.支払先コード, 枝番 = y.支払先枝番 },
                        (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (a, b) => new { TOK = a.x, SH1 = b })
                    .GroupJoin(SHRHD.Where(w => w.回数 == 2),
                        x => new { コード = x.TOK.取引先コード, 枝番 = x.TOK.枝番 },
                        y => new { コード = y.支払先コード, 枝番 = y.支払先枝番 },
                        (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (c, d) => new { c.x.TOK, c.x.SH1, SH2 = d })
                    .GroupJoin(SHRHD.Where(w => w.回数 == 3),
                        x => new { コード = x.TOK.取引先コード, 枝番 = x.TOK.枝番 },
                        y => new { コード = y.支払先コード, 枝番 = y.支払先枝番 },
                        (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (e, f) => new { e.x.TOK, e.x.SH1, e.x.SH2, SH3 = f })
                    .ToList()
                    .Select(x => new SHR03010_SearchMember
                    {
                        ID = string.Format("{0:D4} - {1:D2}", x.TOK.取引先コード, x.TOK.枝番),      // No.223 Mod
                        支払先コード = x.TOK.取引先コード,
                        支払先枝番 = x.TOK.枝番,
                        支払先名 = x.TOK.略称名,  // No.229 Mod
                        締日 = x.TOK.Ｓ締日 ?? 31,
                        区分 = x.SH1 == null ? "新規" : "",
                        開始日付1 = x.SH1 == null ? (DateTime?)null : x.SH1.集計開始日,
                        終了日付1 = x.SH1 == null ? (DateTime?)null : x.SH1.集計最終日,
                        開始日付2 = x.SH2 == null ? (DateTime?)null : x.SH2.集計開始日,
                        終了日付2 = x.SH2 == null ? (DateTime?)null : x.SH2.集計最終日,
                        開始日付3 = x.SH3 == null ? (DateTime?)null : x.SH3.集計開始日,
                        終了日付3 = x.SH3 == null ? (DateTime?)null : x.SH3.集計最終日
                    })
                    .OrderBy(o => o.支払先コード)
                    .ThenBy(t => t.支払先枝番)
                    .ToList();

                #endregion

                #region 締日の掲載及び設定

                foreach (var row in result)
                {
                    // 締日期間を取得
                    DateTime startDate, endDate, paymentDate;
                    getClosingDatePriod(context, yearMonth, row, out startDate, out endDate, out paymentDate);

                    if (!string.IsNullOrWhiteSpace(row.区分))
                    {
                        // 新規の場合は取得締日を初期値として設定する
                        row.開始日付1 = startDate;
                        row.終了日付1 = endDate;
                    }

                    row.クリア開始日付 = startDate;
                    row.クリア終了日付 = endDate;
                    row.入金日 = paymentDate;

                }

                #endregion

                return result;

            }

        }
        #endregion

        #region 支払締集計処理
        /// <summary>
        /// 支払締集計処理
        /// </summary>
        /// <param name="tbl">集計対象データテーブル</param>
        /// <param name="作成年月"></param>
        public void PaymentAggregation(DataSet ds, int 会社コード, int 作成年月, int userId)
        {
            DataTable tbl = ds.Tables[0];
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                foreach (DataRow row in tbl.Rows)
                {
                    SHR03010_SearchMember mem = getConvertSearchMember(row);
                    int cnt = 1;

                    // １回目の集計処理
                    getAggregateData(context, 会社コード, 作成年月, mem, cnt++, userId);

                    // ２回目の集計処理
                    getAggregateData(context, 会社コード, 作成年月, mem, cnt++, userId);

                    // ３回目の集計処理
                    getAggregateData(context, 会社コード, 作成年月, mem, cnt++, userId);

                }

                context.SaveChanges();

            }

        }
        #endregion


        #region 回数あたり集計処理
        /// <summary>
        /// 回数あたりの集計処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">自社コード</param>
        /// <param name="yearMonth">支払年月</param>
        /// <param name="mem">支払リスト行</param>
        /// <param name="cnt">支払回数</param>
        /// <param name="userId">ログインユーザID</param>
        private void getAggregateData(TRAC3Entities context, int company, int yearMonth, SHR03010_SearchMember mem, int cnt, int userId)
        {
            // 得意先コード、枝番を取得
            string[] tokAry = mem.ID.Split('-');
            int code = AppCommon.IntParse(tokAry[0], -1);
            int eda = AppCommon.IntParse(tokAry[1], -1);

            #region 集計期間を取得
            DateTime? targetStDate = null, targetEdDate = null;
            switch (cnt)
            {
                case 1:
                    targetStDate = mem.開始日付1;
                    targetEdDate = mem.終了日付1;
                    break;

                case 2:
                    targetStDate = mem.開始日付2;
                    targetEdDate = mem.終了日付2;
                    break;

                case 3:
                    targetStDate = mem.開始日付3;
                    targetEdDate = mem.終了日付3;
                    break;

                default:
                    break;

            }
            #endregion

            // 集計期間が取れない場合は処理しない
            if (targetStDate == null || targetStDate == null)
                return;

            // 対象の取引先が「自社」の場合は販社仕入を参照する為、ロジックを分岐する
            var tokdata =
                context.M01_TOK.Where(w => w.削除日時 == null && w.取引先コード == code && w.枝番 == eda)
                    .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null && w.取引先コード != null && w.枝番 != null),
                        x => new { code = x.取引先コード, eda = x.枝番 },
                        y => new { code = (int)y.取引先コード, eda = (int)y.枝番 },
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(),
                        (a, b) => new { TOK = a.x, JIS = b })
                    .FirstOrDefault();

            if (tokdata.JIS != null && tokdata.JIS.自社区分 == CommonConstants.自社区分.自社.GetHashCode())
            {
                // ヘッダ情報の登録
                setHeaderInfoHan(context, company, yearMonth, tokdata.JIS.自社コード, cnt, targetStDate, targetEdDate, mem.入金日, userId);

                // 明細情報の登録
                setDetailInfoHan(context, company, yearMonth, tokdata.JIS.自社コード, cnt, targetStDate, targetEdDate, mem.入金日, userId);

            }
            else
            {
                // ヘッダ情報の登録
                setHeaderInfo(context, company, yearMonth, code, eda, cnt, targetStDate, targetEdDate, mem.入金日, userId);

                // 明細情報の登録
                setDetailInfo(context, company, yearMonth, code, eda, cnt, targetStDate, targetEdDate, mem.入金日, userId);

            }

        }
        #endregion

        #region 支払ヘッダ登録処理
        /// <summary>
        /// 支払ヘッダ登録処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">自社コード</param>
        /// <param name="yearMonth">作成年月</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="cnt">回数</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        /// <param name="paymentDate">入金日</param>
        /// <param name="userId">ログインユーザID</param>
        private void setHeaderInfo(TRAC3Entities context, int company, int yearMonth, int code, int eda, int cnt, DateTime? targetStDate, DateTime? targetEdDate, DateTime paymentDate, int userId)
        {
            // ヘッダ情報取得
            S02_SHRHD srdata = getHeaderInfo(context, company, yearMonth, code, eda, cnt, targetStDate, targetEdDate, paymentDate, userId);

            // ヘッダ情報登録
            S02_SHRHD_Update(context, srdata);

        }

        /// <summary>
        /// 支払ヘッダ登録処理(販社)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="myCompanyCode">自社コード</param>
        /// <param name="yearMonth">支払年月(yyyymm)</param>
        /// <param name="paymentCompanyCode">支払先コード(M70_JIS)</param>
        /// <param name="cnt">回数</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        /// <param name="paymentDate">入金日</param>
        /// <param name="userId">ログインユーザID</param>
        private void setHeaderInfoHan(TRAC3Entities context, int myCompanyCode, int yearMonth, int paymentCompanyCode, int cnt, DateTime? targetStDate, DateTime? targetEdDate, DateTime paymentDate, int userId)
        {
            // ヘッダ情報取得
            S02_SHRHD srdata = getHeaderInfoHan(context, myCompanyCode, yearMonth, paymentCompanyCode, cnt, targetStDate, targetEdDate, paymentDate, userId);

            // ヘッダ情報登録
            S02_SHRHD_Update(context, srdata);

        }

        #endregion

        #region 支払明細登録処理
        /// <summary>
        /// 支払明細登録処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">自社コード</param>
        /// <param name="yearMonth">作成年月</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="cnt">回数</param>
        /// <param name="targetStDate">集計期間開始</param>
        /// <param name="targetEdDate">集計期間終了</param>
        /// <param name="paymentDate">入金日</param>
        /// <param name="userId">ログインユーザID</param>
        private void setDetailInfo(TRAC3Entities context, int company, int yearMonth, int code, int eda, int cnt, DateTime? targetStDate, DateTime? targetEdDate, DateTime paymentDate, int userId)
        {
            // 明細情報取得
            List<S02_SHRDTL> dtlList = getDetailInfo(context, company, yearMonth, code, eda, cnt, targetStDate, targetEdDate, paymentDate, userId);

            // 明細情報の登録
            S02_SHRDTL_Update(context, dtlList.ToList());

        }

        /// <summary>
        /// 支払明細登録処理(販社)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="myCompanyCode">自社コード</param>
        /// <param name="yearMonth">支払年月</param>
        /// <param name="salesCompanyCode">販社コード(M70_JIS)</param>
        /// <param name="cnt">回数</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        /// <param name="paymentDate">入金日</param>
        /// <param name="userId">ログインユーザID</param>
        private void setDetailInfoHan(TRAC3Entities context, int myCompanyCode, int yearMonth, int salesCompanyCode, int cnt, DateTime? targetStDate, DateTime? targetEdDate, DateTime paymentDate, int userId)
        {
            // 明細情報取得
            List<S02_SHRDTL> dtlList = getDetailInfoHan(context, myCompanyCode, yearMonth, salesCompanyCode, cnt, targetStDate, targetEdDate, paymentDate, userId);

            // 明細情報の登録
            S02_SHRDTL_Update(context, dtlList.ToList());

        }
        #endregion

        #region 請求ヘッダ情報取得
        /// <summary>
        /// 支払ヘッダ情報取得
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">自社コード</param>
        /// <param name="yearMonth">作成年月</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="cnt">回数</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        /// <param name="paymentDate">入金日</param>
        /// <param name="userId">ログインユーザID</param>
        public S02_SHRHD getHeaderInfo(TRAC3Entities context, int company, int yearMonth, int code, int eda, int cnt, DateTime? targetStDate, DateTime? targetEdDate, DateTime paymentDate, int userId)
        {
            int 仕入先入金日 = int.Parse(paymentDate.ToString("yyyyMMdd"));

            // 前回支払情報取得
            var befShiCnt = getLastPaymentInfo(context, company, yearMonth, code, eda, cnt);

            // 出金情報取得
            var syukin = getPaymentInfo(context, company, code, eda, targetStDate, targetEdDate);

            // No-84,86 Start
            var srList =
                context.T03_SRHD
                    .Where(w => w.削除日時 == null &&
                        w.会社名コード == company &&
                        w.仕入先コード == code &&
                        w.仕入先枝番 == eda &&
                        w.仕入日 >= targetStDate && w.仕入日 <= targetEdDate)
                    .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                        x => new { コード = x.仕入先コード, 枝番 = x.仕入先枝番 },
                        y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                        (x, y) => new { SRHD = x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (a, b) => new { a.SRHD, TOK = b });

            var wkDataListSr =
                srList.ToList()
                    .Select(x => new SHR03010_SRDataMember
                    {
                        伝票番号 = x.SRHD.伝票番号,
                        会社名コード = x.SRHD.会社名コード,
                        仕入日 = x.SRHD.仕入日,
                        入力区分 = x.SRHD.入力区分,
                        仕入区分 = x.SRHD.仕入区分,
                        仕入先コード = x.SRHD.仕入先コード,
                        仕入先枝番 = x.SRHD.仕入先枝番,
                        入荷先コード = x.SRHD.入荷先コード,
                        発注番号 = x.SRHD.発注番号,
                        備考 = x.SRHD.備考,
                        元伝票番号 = x.SRHD.元伝票番号,
                        通常税率対象金額 = x.SRHD.通常税率対象金額,
                        軽減税率対象金額 = x.SRHD.軽減税率対象金額,
                        非課税金額 = x.SRHD.小計 - x.SRHD.通常税率対象金額 - x.SRHD.軽減税率対象金額,
                        通常税率消費税 = x.SRHD.通常税率消費税,
                        軽減税率消費税 = x.SRHD.軽減税率消費税,
                        消費税 = x.SRHD.通常税率消費税 + x.SRHD.軽減税率消費税,
                        当月支払額 = x.SRHD.小計 + x.SRHD.通常税率消費税 + x.SRHD.軽減税率消費税,
                        Ｓ締日 = x.TOK.Ｓ締日,
                        Ｓ入金日１ = x.TOK.Ｓ入金日１,
                        Ｓ支払消費税区分 = x.TOK.Ｓ支払消費税区分,
                        Ｓ税区分ID = x.TOK.Ｓ税区分ID
                    });

            // 揚り情報取得(支払ヘッダ)
            List<SHR03010_SRDataMember> wkDataListAgr = setHeadAgrInfo(context, company, yearMonth, code, eda, cnt, targetStDate, targetEdDate, paymentDate, userId);

            // 仕入情報と揚り情報を結合する
            var dtlList = (
                wkDataListSr.ToList()
                    .Concat(wkDataListAgr));

            var wkData =
                dtlList
                    .GroupJoin(context.M73_ZEI.Where(w => w.削除日時 == null),
                        x => context.M73_ZEI.Where(w => w.削除日時 == null && w.適用開始日付 <= x.仕入日).Max(m => m.適用開始日付),
                        y => y.適用開始日付,
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(),
                        (e, f) => new { e.x, ZEI = f })
                    .ToList()
                    .Select(x => new
                    {
                        自社コード = x.x.会社名コード,
                        支払年月 = yearMonth,
                        支払締日 = x.x.Ｓ締日 ?? 31,
                        支払先コード = x.x.仕入先コード,
                        支払先枝番 = x.x.仕入先枝番,
                        回数 = cnt,
                        支払年月日 = AppCommon.GetClosingDate(yearMonth / 100, yearMonth % 100, x.x.Ｓ入金日１ ?? 31, 0),
                        集計開始日 = targetStDate,
                        集計最終日 = targetEdDate,
                        支払消費税区分 = x.x.Ｓ支払消費税区分,
                        消費税区分 = x.x.消費税区分,
                        金額 =
                            x.x.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                                x.x.当月支払額 : x.x.当月支払額 * -1,
                        消費税丸め区分 = x.x.Ｓ税区分ID,
                        通常税率対象金額 =
                            x.x.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                                x.x.通常税率対象金額 : x.x.通常税率対象金額 * -1,
                        軽減税率対象金額 =
                            x.x.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                                x.x.軽減税率対象金額 : x.x.軽減税率対象金額 * -1,
                        通常税率消費税 =
                            x.x.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                                x.x.通常税率消費税 : x.x.通常税率消費税 * -1,
                        軽減税率消費税 =
                            x.x.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                                x.x.軽減税率消費税 : x.x.軽減税率消費税 * -1,
                        伝票非課税金額 =
                            x.x.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                                (x.x.非課税金額) :
                                (x.x.非課税金額) * -1,
                        伝票金額 =
                            x.x.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                                (x.x.通常税率対象金額 + x.x.軽減税率対象金額 + x.x.非課税金額) : (x.x.通常税率対象金額 + x.x.軽減税率対象金額 + x.x.非課税金額) * -1,
                    });
            // No-84,86 End

            // ヘッダ情報整形
            var srdata =
                wkData
                    .GroupJoin(context.M73_ZEI.Where(w => w.削除日時 == null),
                        x => context.M73_ZEI.Where(w => w.削除日時 == null && w.適用開始日付 <= x.支払年月日).Max(m => m.適用開始日付),
                        y => y.適用開始日付,
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(), (a, b) => new { Data = a.x, ZEI = b })
                    .GroupBy(g => new
                    {
                        g.Data.自社コード,
                        g.Data.支払年月,
                        g.Data.支払締日,
                        g.Data.支払先コード,
                        g.Data.支払先枝番,
                        g.Data.回数,
                        支払年月日 = g.Data.支払年月日,
                        g.Data.集計開始日,
                        g.Data.集計最終日,
                        g.Data.支払消費税区分,
                        g.Data.消費税丸め区分,
                        g.ZEI.消費税率,
                        g.ZEI.軽減税率
                    })
                    .Select(x => new S02_SHRHD
                    {
                        自社コード = x.Key.自社コード,
                        支払年月 = x.Key.支払年月,
                        支払締日 = x.Key.支払締日,
                        支払先コード = x.Key.支払先コード,
                        支払先枝番 = x.Key.支払先枝番,
                        支払日 = 仕入先入金日,
                        回数 = x.Key.回数,
                        支払年月日 = x.Key.支払年月日,
                        集計開始日 = x.Key.集計開始日,
                        集計最終日 = x.Key.集計最終日,
                        前月残高 = befShiCnt == null ? 0 : befShiCnt.当月支払額,
                        出金額 = syukin == null ? 0 : syukin.合計金額,
                        繰越残高 = 0,
                        通常税率対象金額 = (long)x.Sum(s => s.Data.通常税率対象金額),
                        軽減税率対象金額 = (long)x.Sum(s => s.Data.軽減税率対象金額),
                        値引額 = 0,
                        非課税支払額 = (long)x.Sum(s => s.Data.伝票非課税金額),
                        支払額 = (long)x.Sum(s => s.Data.伝票金額),
                        通常税率消費税 = x.Key.支払消費税区分 == (int)CommonConstants.消費税区分.ID01_一括 ?
                                x.Key.消費税丸め区分 == (int)CommonConstants.税区分.ID01_切捨て ?
                                    x.Sum(s => s.Data.通常税率対象金額) > 0 ?
                                        (long)Math.Floor((double)(x.Sum(s => s.Data.通常税率対象金額) * x.Key.消費税率 / (double)100)) :
                                        (long)Math.Ceiling((double)(x.Sum(s => s.Data.通常税率対象金額) * x.Key.消費税率 / (double)100)) :
                                x.Key.消費税丸め区分 == (int)CommonConstants.税区分.ID02_四捨五入 ?
                                    (long)Math.Round((double)(x.Sum(s => s.Data.通常税率対象金額) * x.Key.消費税率 / (double)100)) :
                                x.Key.消費税丸め区分 == (int)CommonConstants.税区分.ID03_切上げ ?
                                    x.Sum(s => s.Data.通常税率対象金額) > 0 ?
                                        (long)Math.Ceiling((double)(x.Sum(s => s.Data.通常税率対象金額) * x.Key.消費税率 / (double)100)) :
                                        (long)Math.Floor((double)(x.Sum(s => s.Data.通常税率対象金額) * x.Key.消費税率 / (double)100)) :
                                0 :
                                (long)x.Sum(s => s.Data.通常税率消費税),
                        軽減税率消費税 = x.Key.支払消費税区分 == (int)CommonConstants.消費税区分.ID01_一括 ?
                                x.Key.消費税丸め区分 == (int)CommonConstants.税区分.ID01_切捨て ?
                                    x.Sum(s => s.Data.軽減税率対象金額) > 0 ?
                                        (long)Math.Floor((double)(x.Sum(s => s.Data.軽減税率対象金額) * x.Key.軽減税率 / (double)100)) :
                                        (long)Math.Ceiling((double)(x.Sum(s => s.Data.軽減税率対象金額) * x.Key.軽減税率 / (double)100)) :
                                x.Key.消費税丸め区分 == (int)CommonConstants.税区分.ID02_四捨五入 ?
                                    (long)Math.Round((double)(x.Sum(s => s.Data.軽減税率対象金額) * x.Key.軽減税率 / (double)100)) :
                                x.Key.消費税丸め区分 == (int)CommonConstants.税区分.ID03_切上げ ?
                                    x.Sum(s => s.Data.軽減税率対象金額) > 0 ?
                                        (long)Math.Ceiling((double)(x.Sum(s => s.Data.軽減税率対象金額) * x.Key.軽減税率 / (double)100)) :
                                        (long)Math.Floor((double)(x.Sum(s => s.Data.軽減税率対象金額) * x.Key.軽減税率 / (double)100)) :
                                0 :
                                (long)x.Sum(s => s.Data.軽減税率消費税),
                        消費税 = 0,
                        当月支払額 = 0,
                        登録者 = userId,
                        登録日時 = DateTime.Now
                    })
                    .FirstOrDefault();

            if (srdata == null)
            {
                // 空のヘッダ情報を作成
                srdata =
                    context.M01_TOK
                        .Where(w => w.削除日時 == null && w.取引先コード == code && w.枝番 == eda)
                        .ToList()
                        .Select(x => new S02_SHRHD
                        {
                            自社コード = company,
                            支払年月 = yearMonth,
                            支払締日 = x.Ｓ締日 ?? 31,
                            支払先コード = x.取引先コード,
                            支払先枝番 = x.枝番,
                            支払日 = 仕入先入金日,
                            回数 = cnt,
                            支払年月日 = AppCommon.GetClosingDate(yearMonth / 100, yearMonth % 100, x.Ｓ入金日１ ?? 31, 0),
                            集計開始日 = targetStDate,
                            集計最終日 = targetEdDate,
                            前月残高 = x.Ｓ締日 == 0 ? 0 : befShiCnt == null ? 0 : befShiCnt.当月支払額,
                            出金額 = syukin == null ? 0 : syukin.合計金額,
                            登録者 = userId,
                            登録日時 = DateTime.Now
                        })
                        .FirstOrDefault();

            }

            // 繰越残高を設定
            srdata.繰越残高 = srdata.前月残高 - srdata.出金額;
            // 消費税を設定
            srdata.消費税 = srdata.通常税率消費税 + srdata.軽減税率消費税;
            // 支払額を設定
            srdata.当月支払額 = srdata.繰越残高 + srdata.支払額 + srdata.消費税;

            return srdata;

        }

        /// <summary>
        /// 支払ヘッダ情報取得(販社)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="myCompanyCode">自社コード</param>
        /// <param name="yearMonth">支払年月(yyyymm)</param>
        /// <param name="paymentCompanyCode">支払先コード(M70_JIS)</param>
        /// <param name="cnt">回数</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        /// <param name="paymentDate">入金日</param>
        /// <param name="userId">ログインユーザID</param>
        public S02_SHRHD getHeaderInfoHan(TRAC3Entities context, int myCompanyCode, int yearMonth, int paymentCompanyCode, int cnt, DateTime? targetStDate, DateTime? targetEdDate, DateTime paymentDate, int userId)
        {
            int 支払日 = int.Parse(paymentDate.ToString("yyyyMMdd"));

            // 自社マスタ
            var targetJis =
                context.M70_JIS
                    .Where(w => w.削除日時 == null && w.自社コード == paymentCompanyCode)
                    .First();

            // 前回支払情報取得
            var befShiCnt = getLastPaymentInfo(context, myCompanyCode, yearMonth, targetJis.取引先コード, targetJis.枝番, cnt);

            // 出金情報取得
            var syukin = getPaymentInfo(context, myCompanyCode, targetJis.取引先コード, targetJis.枝番, targetStDate, targetEdDate);

            // 基本情報
            var srList =
                context.T03_SRHD_HAN
                    .Where(w => w.削除日時 == null &&
                        w.会社名コード == myCompanyCode &&
                        w.仕入先コード == paymentCompanyCode &&
                        w.仕入日 >= targetStDate && w.仕入日 <= targetEdDate)
                    .Join(context.M70_JIS.Where(w => w.削除日時 == null),
                        x => x.仕入先コード,
                        y => y.自社コード,
                        (x, y) => new { SRHD = x, JIS = y })
                    .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                        x => new { コード = (int)x.JIS.取引先コード, 枝番 = (int)x.JIS.枝番 },
                        y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                        (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (a, b) => new { a.x.SRHD, a.x.JIS, TOK = b });

            var wkData =
                srList
                    .GroupJoin(context.M73_ZEI.Where(w => w.削除日時 == null),
                        x => context.M73_ZEI.Where(w => w.削除日時 == null && w.適用開始日付 <= x.SRHD.仕入日).Max(m => m.適用開始日付),
                        y => y.適用開始日付,
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(),
                        (e, f) => new { e.x.SRHD, e.x.TOK, ZEI = f })
                    .ToList()
                    .Select(x => new
                    {
                        自社コード = x.SRHD.会社名コード,
                        支払年月 = yearMonth,
                        支払締日 = x.TOK.Ｓ締日 ?? 31,
                        支払先コード = x.TOK.取引先コード,
                        支払先枝番 = x.TOK.枝番,
                        回数 = cnt,
                        支払年月日 = AppCommon.GetClosingDate(yearMonth / 100, yearMonth % 100, x.TOK.Ｓ入金日１ ?? 31, 0),
                        集計開始日 = targetStDate,
                        集計最終日 = targetEdDate,
                        支払消費税区分 = x.TOK.Ｓ支払消費税区分,
                        消費税丸め区分 = x.TOK.Ｓ税区分ID,
                        通常税率対象金額 =
                            x.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                                x.SRHD.通常税率対象金額 : x.SRHD.通常税率対象金額 * -1,
                        軽減税率対象金額 =
                            x.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                                x.SRHD.軽減税率対象金額 : x.SRHD.軽減税率対象金額 * -1,
                        通常税率消費税 =
                            x.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                                x.SRHD.通常税率消費税 : x.SRHD.通常税率消費税 * -1,
                        軽減税率消費税 =
                            x.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                                x.SRHD.軽減税率消費税 : x.SRHD.軽減税率消費税 * -1,
                        伝票非課税金額 =
                            x.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                                (x.SRHD.小計 - (x.SRHD.通常税率対象金額 + x.SRHD.軽減税率対象金額)) :
                                (x.SRHD.小計 - (x.SRHD.通常税率対象金額 + x.SRHD.軽減税率対象金額)) * -1,
                        伝票金額 =
                            x.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                                x.SRHD.小計 : x.SRHD.小計 * -1,
                        // No-86 End
                    });

            // ヘッダ情報整形
            var srdata =
                wkData
                    .GroupJoin(context.M73_ZEI.Where(w => w.削除日時 == null),
                        x => context.M73_ZEI.Where(w => w.削除日時 == null && w.適用開始日付 <= x.支払年月日).Max(m => m.適用開始日付),
                        y => y.適用開始日付,
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(), (a, b) => new { Data = a.x, ZEI = b })
                    .GroupBy(g => new
                    {
                        g.Data.自社コード,
                        g.Data.支払年月,
                        g.Data.支払締日,
                        g.Data.支払先コード,
                        g.Data.支払先枝番,
                        g.Data.回数,
                        支払年月日 = g.Data.支払年月日,
                        g.Data.集計開始日,
                        g.Data.集計最終日,
                        g.Data.支払消費税区分,
                        g.ZEI.消費税率,
                        g.ZEI.軽減税率,
                        g.Data.消費税丸め区分
                    })
                    .Select(x => new S02_SHRHD
                    {
                        自社コード = x.Key.自社コード,
                        支払年月 = x.Key.支払年月,
                        支払締日 = x.Key.支払締日,
                        支払先コード = x.Key.支払先コード,
                        支払先枝番 = x.Key.支払先枝番,
                        支払日 = 支払日,
                        回数 = x.Key.回数,
                        支払年月日 = x.Key.支払年月日,
                        集計開始日 = x.Key.集計開始日,
                        集計最終日 = x.Key.集計最終日,
                        前月残高 = befShiCnt == null ? 0 : befShiCnt.当月支払額,
                        出金額 = syukin == null ? 0 : syukin.合計金額,
                        繰越残高 = 0,
                        通常税率対象金額 = (long)x.Sum(s => s.Data.通常税率対象金額),
                        軽減税率対象金額 = (long)x.Sum(s => s.Data.軽減税率対象金額),
                        値引額 = 0,
                        非課税支払額 = (long)x.Sum(s => s.Data.伝票非課税金額),
                        支払額 = (long)x.Sum(s => s.Data.伝票金額),
                        // No-135-2 Mod Start
                        通常税率消費税 = x.Key.支払消費税区分 == (int)CommonConstants.消費税区分.ID01_一括 ?
                                x.Key.消費税丸め区分 == (int)CommonConstants.税区分.ID01_切捨て ?
                                    x.Sum(s => s.Data.通常税率対象金額) > 0 ?
                                        (long)Math.Floor((double)(x.Sum(s => s.Data.通常税率対象金額) * x.Key.消費税率 / (double)100)) :
                                        (long)Math.Ceiling((double)(x.Sum(s => s.Data.通常税率対象金額) * x.Key.消費税率 / (double)100)) :
                                x.Key.消費税丸め区分 == (int)CommonConstants.税区分.ID02_四捨五入 ?
                                    (long)Math.Round((double)(x.Sum(s => s.Data.通常税率対象金額) * x.Key.消費税率 / (double)100)) :
                                x.Key.消費税丸め区分 == (int)CommonConstants.税区分.ID03_切上げ ?
                                    x.Sum(s => s.Data.通常税率対象金額) > 0 ?
                                        (long)Math.Ceiling((double)(x.Sum(s => s.Data.通常税率対象金額) * x.Key.消費税率 / (double)100)) :
                                        (long)Math.Floor((double)(x.Sum(s => s.Data.通常税率対象金額) * x.Key.消費税率 / (double)100)) :
                                0 :
                                (long)x.Sum(s => s.Data.通常税率消費税),
                        軽減税率消費税 = x.Key.支払消費税区分 == (int)CommonConstants.消費税区分.ID01_一括 ?
                                x.Key.消費税丸め区分 == (int)CommonConstants.税区分.ID01_切捨て ?
                                    x.Sum(s => s.Data.軽減税率対象金額) > 0 ?
                                        (long)Math.Floor((double)(x.Sum(s => s.Data.軽減税率対象金額) * x.Key.軽減税率 / (double)100)) :
                                        (long)Math.Ceiling((double)(x.Sum(s => s.Data.軽減税率対象金額) * x.Key.軽減税率 / (double)100)) :
                                x.Key.消費税丸め区分 == (int)CommonConstants.税区分.ID02_四捨五入 ?
                                    (long)Math.Round((double)(x.Sum(s => s.Data.軽減税率対象金額) * x.Key.軽減税率 / (double)100)) :
                                x.Key.消費税丸め区分 == (int)CommonConstants.税区分.ID03_切上げ ?
                                    x.Sum(s => s.Data.軽減税率対象金額) > 0 ?
                                        (long)Math.Ceiling((double)(x.Sum(s => s.Data.軽減税率対象金額) * x.Key.軽減税率 / (double)100)) :
                                        (long)Math.Floor((double)(x.Sum(s => s.Data.軽減税率対象金額) * x.Key.軽減税率 / (double)100)) :
                                0 :
                                (long)x.Sum(s => s.Data.軽減税率消費税),
                        // No-135-2 Mod End
                        消費税 = 0,
                        当月支払額 = 0,
                        登録者 = userId,
                        登録日時 = DateTime.Now
                    })
                    .FirstOrDefault();

            if (srdata == null)
            {
                // 空のヘッダ情報を作成
                srdata =
                    context.M01_TOK
                        .Where(w => w.削除日時 == null && w.取引先コード == targetJis.取引先コード && w.枝番 == targetJis.枝番)
                        .ToList()
                        .Select(x => new S02_SHRHD
                        {
                            自社コード = myCompanyCode,
                            支払年月 = yearMonth,
                            支払締日 = x.Ｓ締日 ?? 31,
                            支払先コード = x.取引先コード,
                            支払先枝番 = x.枝番,
                            支払日 = 支払日,
                            回数 = cnt,
                            支払年月日 = AppCommon.GetClosingDate(yearMonth / 100, yearMonth % 100, x.Ｓ入金日１ ?? 31, 0),
                            集計開始日 = targetStDate,
                            集計最終日 = targetEdDate,
                            前月残高 = x.Ｓ締日 == 0 ? 0 : befShiCnt == null ? 0 : befShiCnt.当月支払額,
                            出金額 = syukin == null ? 0 : syukin.合計金額,
                            登録者 = userId,
                            登録日時 = DateTime.Now
                        })
                        .FirstOrDefault();

            }

            // 繰越残高を設定
            srdata.繰越残高 = srdata.前月残高 - srdata.出金額;
            // 消費税を設定
            srdata.消費税 = srdata.通常税率消費税 + srdata.軽減税率消費税;
            // 支払額を設定
            srdata.当月支払額 = srdata.繰越残高 + srdata.支払額 + srdata.消費税;

            return srdata;

        }

        /// <summary>
        /// 前回支払情報取得
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">会社名コード</param>
        /// <param name="yearMonth">作成年月</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="cnt">回数</param>
        public S02_SHRHD getLastPaymentInfo(TRAC3Entities context, int company, int yearMonth, int? code, int? eda, int cnt)
        {
            // No-100 Mod Start
            // 前回支払情報取得
            DateTime befCntMonth = new DateTime(yearMonth / 100, yearMonth % 100, 1);
            if (cnt == 1)
            {
                befCntMonth = new DateTime(yearMonth / 100, yearMonth % 100, 1).AddMonths(-1);
            }
            var befShiCnt =
                context.S02_SHRHD
                    .Where(w => w.自社コード == company &&
                        w.支払年月 == (befCntMonth.Year * 100 + befCntMonth.Month) &&
                        w.支払先コード == code &&
                        w.支払先枝番 == eda)
                    .OrderByDescending(o => o.回数)
                    .FirstOrDefault();

            return befShiCnt;
            // No-100 Mod End

        }

        /// <summary>
        /// 出金情報取得
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">会社名コード</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        public T12_PAY_Search_Extension getPaymentInfo(TRAC3Entities context, int company, int? code, int? eda, DateTime? targetStDate, DateTime? targetEdDate)
        {
            // No-100 Mod Start
            // 出金額取得
            var syukin =
                context.T12_PAYHD
                    .Where(w => w.削除日時 == null &&
                        w.出金元自社コード == company &&
                        (w.出金日 >= targetStDate && w.出金日 <= targetEdDate) &&
                        w.得意先コード == code && w.得意先枝番 == eda)
                    .Join(context.T12_PAYDTL.Where(w => w.削除日時 == null),
                        x => x.伝票番号,
                        y => y.伝票番号,
                        (x, y) => new { PAYHD = x, PAYDTL = y })
                    .GroupBy(g => new { g.PAYHD.得意先コード, g.PAYHD.得意先枝番 })
                    .Select(s => new
                    {
                        得意先コード = s.Key.得意先コード,
                        得意先枝番 = s.Key.得意先枝番,
                        合計金額 = s.Sum(sum => sum.PAYDTL.金額)
                    })
                    .FirstOrDefault();
            // No-100 Mod End

            T12_PAY_Search_Extension result = new T12_PAY_Search_Extension();

            if (syukin == null)
            {
                return null;
            }

            result.得意先コード = syukin.得意先コード ?? 0;
            result.得意先枝番 = syukin.得意先枝番 ?? 0;
            result.合計金額 = syukin.合計金額;

            return result;

        }

        #endregion

        #region 支払明細情報取得
        /// <summary>
        /// 支払明細情報取得
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">自社コード</param>
        /// <param name="yearMonth">作成年月</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="cnt">回数</param>
        /// <param name="targetStDate">集計期間開始</param>
        /// <param name="targetEdDate">集計期間終了</param>
        /// <param name="paymentDate">入金日</param>
        /// <param name="userId">ログインユーザID</param>
        public List<S02_SHRDTL> getDetailInfo(TRAC3Entities context, int company, int yearMonth, int code, int eda, int cnt, DateTime? targetStDate, DateTime? targetEdDate, DateTime paymentDate, int userId)
        {
            int 仕入先入金日 = int.Parse(paymentDate.ToString("yyyyMMdd"));

            // 基本情報
            var srList =
                context.T03_SRHD
                    .Where(w => w.削除日時 == null &&
                        w.会社名コード == company &&
                        w.仕入先コード == code &&
                        w.仕入先枝番 == eda &&
                        w.仕入日 >= targetStDate && w.仕入日 <= targetEdDate)
                // No-63 Start
                    .Join(context.T03_SRDTL.Where(w => w.削除日時 == null),
                // No-63 End
                        x => x.伝票番号,
                        y => y.伝票番号,
                        (x, y) => new { SRHD = x, SRDTL = y })
                    .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                        x => new { コード = x.SRHD.仕入先コード, 枝番 = x.SRHD.仕入先枝番 },
                        y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                        (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (a, b) => new { a.x.SRHD, a.x.SRDTL, TOK = b })
                    .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                        x => x.SRDTL.品番コード,
                        y => y.品番コード,
                        (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (c, d) => new { c.x.SRHD, c.x.SRDTL, c.x.TOK, HIN = d })
                    .GroupJoin(context.M73_ZEI.Where(w => w.削除日時 == null),
                        x => context.M73_ZEI.Where(w => w.削除日時 == null && w.適用開始日付 <= x.SRHD.仕入日).Max(m => m.適用開始日付),
                        y => y.適用開始日付,
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(),
                        (e, f) => new { e.x.SRHD, e.x.SRDTL, e.x.TOK, e.x.HIN, ZEI = f });

            var dtlList =
                srList.ToList()
                    .Select(x => new S02_SHRDTL
                    {
                        自社コード = x.SRHD.会社名コード,
                        支払年月 = yearMonth,
                        支払締日 = x.TOK.Ｓ締日 ?? 31,
                        支払先コード = x.SRHD.仕入先コード,
                        支払先枝番 = x.SRHD.仕入先枝番,
                        支払日 = 仕入先入金日,
                        回数 = cnt,
                        行 = 0,
                        伝票番号 = x.SRHD.伝票番号,
                        仕入日 = x.SRHD.仕入日,
                        品番コード = x.SRDTL.品番コード,
                        自社品名 = !string.IsNullOrEmpty(x.SRDTL.自社品名) ? x.SRDTL.自社品名 : x.HIN.自社品名,                // No.390 Add
                        // No-86 Start
                        数量 =
                            x.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                                x.SRDTL.数量 : x.SRDTL.数量 * -1,
                        // No-86 End
                        単価 = x.SRDTL.単価,
                        // No-63,86 Start
                        金額 =
                            x.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                                x.SRDTL.金額 : x.SRDTL.金額 * -1,
                        // No-63,86 End
                        // No-86 Start
                        消費税 = (
                                x.TOK.Ｓ税区分ID == (int)CommonConstants.税区分.ID01_切捨て ?
                                   (int)Math.Floor((double)(x.SRDTL.金額 *
                                       (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                       x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                       0) / (double)100)) :
                                x.TOK.Ｓ税区分ID == (int)CommonConstants.税区分.ID02_四捨五入 ?
                                   (int)Math.Round((double)(x.SRDTL.金額 *
                                       (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                       x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                       0) / (double)100), 0, MidpointRounding.AwayFromZero) :
                                x.TOK.Ｓ税区分ID == (int)CommonConstants.税区分.ID03_切上げ ?
                                   (int)Math.Ceiling((double)(x.SRDTL.金額 *
                                       (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                       x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                       0) / (double)100)) :
                                    0
                                 ) * (x.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ? 1 : -1),
                        // No-86 End
                        摘要 = x.SRDTL.摘要,
                        登録者 = userId,
                        登録日時 = DateTime.Now,
                        伝票区分 = (int)CommonConstants.支払伝票区分.仕入伝票,

                    });

            // No-84 Start
            // 揚り情報取得(支払明細)
            List<S02_SHRDTL> dtlListAgr = setDetailAgrInfo(context, company, yearMonth, code, eda, cnt, targetStDate, targetEdDate, paymentDate, userId);

            // 仕入情報と揚り情報を結合する
            var dtlListAll = (
                dtlList.ToList()
                    .Concat(dtlListAgr
                        ))
                    .OrderBy(o => o.仕入日).ThenBy(o => o.伝票番号).ThenBy(o => o.行);

            return dtlListAll.ToList();
            // No-84 End

        }

        /// <summary>
        /// 支払明細情報取得(販社)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="myCompanyCode">自社コード</param>
        /// <param name="yearMonth">支払年月</param>
        /// <param name="salesCompanyCode">販社コード(M70_JIS)</param>
        /// <param name="cnt">回数</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        /// <param name="paymentDate">入金日</param>
        /// <param name="userId">ログインユーザID</param>
        public List<S02_SHRDTL> getDetailInfoHan(TRAC3Entities context, int myCompanyCode, int yearMonth, int salesCompanyCode, int cnt, DateTime? targetStDate, DateTime? targetEdDate, DateTime paymentDate, int userId)
        {
            int 販社入金日 = int.Parse(paymentDate.ToString("yyyyMMdd"));

            // 基本情報
            var srList =
                context.T03_SRHD_HAN
                    .Where(w => w.削除日時 == null &&
                        w.会社名コード == myCompanyCode &&
                        w.仕入先コード == salesCompanyCode &&
                        w.仕入日 >= targetStDate && w.仕入日 <= targetEdDate)
                // No-63 Start
                    .Join(context.T03_SRDTL_HAN.Where(w => w.削除日時 == null),
                // No-63 End
                        x => x.伝票番号,
                        y => y.伝票番号,
                        (x, y) => new { SRHD = x, SRDTL = y })
                    .Join(context.M70_JIS.Where(w => w.削除日時 == null),
                        x => x.SRHD.仕入先コード,
                        y => y.自社コード,
                        (x, y) => new { x.SRHD, x.SRDTL, JIS = y })
                    .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                        x => new { コード = (int)x.JIS.取引先コード, 枝番 = (int)x.JIS.枝番 },
                        y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                        (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (a, b) => new { a.x.SRHD, a.x.SRDTL, TOK = b })
                    .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                        x => x.SRDTL.品番コード,
                        y => y.品番コード,
                        (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (c, d) => new { c.x.SRHD, c.x.SRDTL, c.x.TOK, HIN = d })
                    .GroupJoin(context.M73_ZEI.Where(w => w.削除日時 == null),
                        x => context.M73_ZEI.Where(w => w.削除日時 == null && w.適用開始日付 <= x.SRHD.仕入日).Max(m => m.適用開始日付),
                        y => y.適用開始日付,
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(),
                        (e, f) => new { e.x.SRHD, e.x.SRDTL, e.x.TOK, e.x.HIN, ZEI = f });

            var dtlList =
                srList.ToList()
                    .Select(x => new S02_SHRDTL
                    {
                        自社コード = x.SRHD.会社名コード,
                        支払年月 = yearMonth,
                        支払締日 = x.TOK.Ｓ締日 ?? 31,
                        支払先コード = x.TOK.取引先コード,
                        支払先枝番 = x.TOK.枝番,
                        支払日 = 販社入金日,
                        回数 = cnt,
                        行 = 0,
                        伝票番号 = x.SRHD.伝票番号,
                        仕入日 = x.SRHD.仕入日,
                        品番コード = x.SRDTL.品番コード,
                        自社品名 = !string.IsNullOrEmpty(x.SRDTL.自社品名) ? x.SRDTL.自社品名 : x.HIN.自社品名,                // No.390 Add
                        // No-86 Start
                        数量 =
                            x.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                                x.SRDTL.数量 : x.SRDTL.数量 * -1,
                        // No-86 End
                        単価 = x.SRDTL.単価,
                        // No-63,86 Start
                        金額 =
                            x.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                                x.SRDTL.金額 : x.SRDTL.金額 * -1,
                        // No-63,86 End
                        // No-86 Start
                        消費税 = (
                                x.TOK.Ｓ税区分ID == (int)CommonConstants.税区分.ID01_切捨て ?
                                   (int)Math.Floor((double)(x.SRDTL.金額 *
                                       (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                       x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                       0) / (double)100)) :
                                x.TOK.Ｓ税区分ID == (int)CommonConstants.税区分.ID02_四捨五入 ?
                                   (int)Math.Round((double)(x.SRDTL.金額 *
                                       (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                       x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                       0) / (double)100), 0, MidpointRounding.AwayFromZero) :
                                x.TOK.Ｓ税区分ID == (int)CommonConstants.税区分.ID03_切上げ ?
                                   (int)Math.Ceiling((double)(x.SRDTL.金額 *
                                       (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                       x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                       0) / (double)100)) :
                                    0
                                 ) * (x.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ? 1 : -1),
                        // No-86 End
                        摘要 = x.SRDTL.摘要,
                        登録者 = userId,
                        登録日時 = DateTime.Now,
                        伝票区分 = (int)CommonConstants.支払伝票区分.仕入伝票,
                    });

            return dtlList.ToList();

        }
        #endregion

        #region 揚り情報取得
        // No-84 Start
        /// <summary>
        /// 揚り情報取得(支払ヘッダ)
        /// <summary>
        /// 支払ヘッダ登録処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">自社コード</param>
        /// <param name="yearMonth">作成年月</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="cnt">回数</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        /// <param name="paymentDate">入金日</param>
        /// <param name="userId">ログインユーザID</param>
        private List<SHR03010_SRDataMember> setHeadAgrInfo(TRAC3Entities context, int company, int yearMonth, int code, int eda, int cnt, DateTime? targetStDate, DateTime? targetEdDate, DateTime paymentDate, int userId)
        {
            int 仕入先入金日 = int.Parse(paymentDate.ToString("yyyyMMdd"));

            // 揚り情報取得
            var agrList =
                context.T04_AGRHD
                    .Where(w => w.削除日時 == null &&
                        w.会社名コード == company &&
                        w.外注先コード == code &&
                        w.外注先枝番 == eda &&
                        w.仕上り日 >= targetStDate && w.仕上り日 <= targetEdDate)
                    .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                        x => new { コード = x.外注先コード, 枝番 = x.外注先枝番 },
                        y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                        (x, y) => new { AGRHD = x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (a, b) => new { a.AGRHD,  TOK = b });
                    

            var dtlAgrList =
                agrList.ToList()
                    .Select(x => new SHR03010_SRDataMember
                    {
                        伝票番号 = x.AGRHD.伝票番号,
                        会社名コード = x.AGRHD.会社名コード,
                        仕入日 = x.AGRHD.仕上り日,
                        入力区分 = (int)CommonConstants.入力区分.仕入入力,
                        仕入区分 = (int)CommonConstants.仕入区分.通常,
                        仕入先コード = x.AGRHD.外注先コード,
                        仕入先枝番 = x.AGRHD.外注先枝番,
                        入荷先コード = x.AGRHD.入荷先コード,
                        発注番号 = null,
                        備考 = x.AGRHD.備考,
                        元伝票番号 = null,
                        通常税率対象金額 = x.AGRHD.小計,
                        軽減税率対象金額 = 0,
                        非課税金額 = 0,
                        通常税率消費税 = x.AGRHD.消費税,
                        軽減税率消費税 = 0,
                        消費税 = x.AGRHD.消費税,
                        当月支払額 = x.AGRHD.総合計,

                        Ｓ締日 = x.TOK.Ｓ締日,
                        Ｓ入金日１ = x.TOK.Ｓ入金日１,
                        Ｓ支払消費税区分 = x.TOK.Ｓ支払消費税区分,
                        Ｓ税区分ID = x.TOK.Ｓ税区分ID
                    });

            return dtlAgrList.ToList();
        }

        /// <summary>
        /// 揚り情報取得(支払明細)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">自社コード</param>
        /// <param name="yearMonth">作成年月</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="cnt">回数</param>
        /// <param name="targetStDate">集計期間開始</param>
        /// <param name="targetEdDate">集計期間終了</param>
        /// <param name="paymentDate">入金日</param>
        /// <param name="userId">ログインユーザID</param>
        private List<S02_SHRDTL> setDetailAgrInfo(TRAC3Entities context, int company, int yearMonth, int code, int eda, int cnt, DateTime? targetStDate, DateTime? targetEdDate, DateTime paymentDate, int userId)
        {
            int 仕入先入金日 = int.Parse(paymentDate.ToString("yyyyMMdd"));

            // 揚り情報取得
            var agrList =
                context.T04_AGRHD
                    .Where(w => w.削除日時 == null &&
                        w.会社名コード == company &&
                        w.外注先コード == code &&
                        w.外注先枝番 == eda &&
                        w.仕上り日 >= targetStDate && w.仕上り日 <= targetEdDate)
                    .Join(context.T04_AGRDTL.Where(w => w.削除日時 == null),
                        x => x.伝票番号,
                        y => y.伝票番号,
                        (x, y) => new { AGRHD = x, AGRDTL = y })
                    .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                        x => new { コード = x.AGRHD.外注先コード, 枝番 = x.AGRHD.外注先枝番 },
                        y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                        (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (a, b) => new { a.x.AGRHD, a.x.AGRDTL, TOK = b })
                    .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                        x => x.AGRDTL.品番コード,
                        y => y.品番コード,
                        (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (c, d) => new { c.x.AGRHD, c.x.AGRDTL, c.x.TOK, HIN = d })
                    .GroupJoin(context.M73_ZEI.Where(w => w.削除日時 == null),
                        x => context.M73_ZEI.Where(w => w.削除日時 == null && w.適用開始日付 <= x.AGRHD.仕上り日).Max(m => m.適用開始日付),
                        y => y.適用開始日付,
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(),
                    (e, f) => new { e.x.AGRHD, e.x.AGRDTL, e.x.TOK, e.x.HIN, ZEI = f });

            var dtlAgrList =
                agrList.ToList()
                    .Select(x => new S02_SHRDTL
                    {
                        自社コード = x.AGRHD.会社名コード,
                        支払年月 = yearMonth,
                        支払締日 = x.TOK.Ｓ締日 ?? 31,
                        支払先コード = x.AGRHD.外注先コード,
                        支払先枝番 = x.AGRHD.外注先枝番,
                        支払日 = 仕入先入金日,
                        回数 = cnt,
                        行 = 0,
                        伝票番号 = x.AGRHD.伝票番号,
                        仕入日 = x.AGRHD.仕上り日,
                        品番コード = x.AGRDTL.品番コード,
                        数量 = x.AGRDTL.数量 ?? 0,
                        単価 = x.AGRDTL.単価 ?? 0,
                        金額 = x.AGRDTL.金額 ?? 0,
                        消費税 =
                                x.TOK.Ｓ税区分ID == (int)CommonConstants.税区分.ID01_切捨て ?
                                   (int)Math.Floor((double)(x.AGRDTL.金額 *
                                       (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                       x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                       0) / (double)100)) :
                                x.TOK.Ｓ税区分ID == (int)CommonConstants.税区分.ID02_四捨五入 ?
                                   (int)Math.Round((double)(x.AGRDTL.金額 *
                                       (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                       x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                       0) / (double)100), 0, MidpointRounding.AwayFromZero) :
                                x.TOK.Ｓ税区分ID == (int)CommonConstants.税区分.ID03_切上げ ?
                                   (int)Math.Ceiling((double)(x.AGRDTL.金額 *
                                       (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                       x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                       0) / (double)100)) :
                                0,
                        摘要 = x.AGRDTL.摘要,
                        登録者 = userId,
                        登録日時 = DateTime.Now,
                        伝票区分 = (int)CommonConstants.支払伝票区分.揚り伝票,
                    });

            return dtlAgrList.ToList();    
        }
        // No-84 End
        #endregion

        #region 支払ヘッダ更新処理
        /// <summary>
        /// 支払ヘッダ更新処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="hdData"></param>
        private void S02_SHRHD_Update(TRAC3Entities context, S02_SHRHD hdData)
        {
            var shrhd =
                context.S02_SHRHD.Where(w =>
                    w.自社コード == hdData.自社コード &&
                    w.支払年月 == hdData.支払年月 &&
                    w.支払先コード == hdData.支払先コード &&
                    w.支払先枝番 == hdData.支払先枝番 &&
                    w.支払日 == hdData.支払日 &&
                    w.回数 == hdData.回数)
                    .FirstOrDefault();

            if (shrhd == null)
            {
                // 登録なしなので登録をおこなう
                S02_SHRHD data = new S02_SHRHD();

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
                data.通常税率対象金額 = hdData.通常税率対象金額;
                data.軽減税率対象金額 = hdData.軽減税率対象金額;
                data.値引額 = hdData.値引額;
                data.非課税支払額 = hdData.非課税支払額;
                data.支払額 = hdData.支払額;
                data.通常税率消費税 = hdData.通常税率消費税;
                data.軽減税率消費税 = hdData.軽減税率消費税;
                data.消費税 = hdData.消費税;
                data.当月支払額 = hdData.当月支払額;
                
                data.登録者 = hdData.登録者;
                data.登録日時 = hdData.登録日時;

                context.S02_SHRHD.ApplyChanges(data);

            }
            else
            {
                // 登録済みなので更新をおこなう
                shrhd.支払年月日 = hdData.支払年月日;
                shrhd.集計開始日 = hdData.集計開始日;
                shrhd.集計最終日 = hdData.集計最終日;
                shrhd.前月残高 = hdData.前月残高;
                shrhd.出金額 = hdData.出金額;
                shrhd.繰越残高 = hdData.繰越残高;
                shrhd.通常税率対象金額 = hdData.通常税率対象金額;
                shrhd.軽減税率対象金額 = hdData.軽減税率対象金額;
                shrhd.値引額 = hdData.値引額;
                shrhd.非課税支払額 = hdData.非課税支払額;
                shrhd.支払額 = hdData.支払額;
                shrhd.通常税率消費税 = hdData.通常税率消費税;
                shrhd.軽減税率消費税 = hdData.軽減税率消費税;
                shrhd.消費税 = hdData.消費税;
                shrhd.当月支払額 = hdData.当月支払額;

                shrhd.AcceptChanges();

            }

        }
        #endregion

        #region 支払明細更新処理
        /// <summary>
        /// 支払明細更新処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="list"></param>
        private void S02_SHRDTL_Update(TRAC3Entities context, List<S02_SHRDTL> list)
        {
            int rowCnt = 1;
            foreach (var dtlData in list)
            {
                // 登録済みのデータを削除
                var delList =
                    context.S02_SHRDTL
                        .Where(w =>
                            w.自社コード == dtlData.自社コード &&
                            w.支払年月 == dtlData.支払年月 &&
                            w.支払締日 == dtlData.支払締日 &&
                            w.支払先コード == dtlData.支払先コード &&
                            w.支払先枝番 == dtlData.支払先枝番 &&
                            w.支払日 == dtlData.支払日 &&
                            w.回数 == dtlData.回数);

                foreach (var delData in delList)
                    context.S02_SHRDTL.DeleteObject(delData);

                // 作成データの登録
                dtlData.行 = rowCnt++;
                context.S02_SHRDTL.ApplyChanges(dtlData);

            }

        }
        #endregion

        #region << サービス処理関連 >>

        /// <summary>
        /// 締日に対する集計期間を取得する
        /// </summary>
        /// <param name="作成年月"></param>
        /// <param name="targetRow"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        private void getClosingDatePriod(TRAC3Entities context, int 作成年月, SHR03010_SearchMember targetRow, out DateTime startDate, out DateTime endDate, out DateTime paymentDate)
        {
            int year = 作成年月 / 100;
            int month = 作成年月 % 100;

            // 対象取引先のサイト設定で取得入金年月が変わる
            var tok =
                context.M01_TOK
                    .Where(w => w.削除日時 == null && w.取引先コード == targetRow.支払先コード && w.枝番 == targetRow.支払先枝番)
                    .FirstOrDefault();

            DateTime closingDate = AppCommon.GetClosingDate(year, month, tok.Ｓ締日 ?? 31, 0);

            // 入金日の算出
            try
            {
                paymentDate =
                    AppCommon.GetClosingDate(year, month, tok.Ｓ入金日１ ?? CommonConstants.DEFAULT_CLOSING_DAY, tok.Ｓサイト１ ?? 0);    // No-170 Mod
            }
            catch
            {
                // 基本的にあり得ないがこの場合は当月末日を指定
                paymentDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            }

            // 集計期間設定
            startDate = closingDate.AddDays(1).AddMonths(-1);
            endDate = closingDate;

        }

        /// <summary>
        /// データ行をクラスメンバーへ変換をおこなう
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private SHR03010_SearchMember getConvertSearchMember(DataRow row)
        {
            SHR03010_SearchMember sm = new SHR03010_SearchMember();

            sm.ID = row["ID"].ToString();
            sm.支払先コード = int.Parse(row["支払先コード"].ToString());
            sm.支払先枝番 = int.Parse(row["支払先枝番"].ToString());
            sm.支払先名 = row["支払先名"].ToString();
            sm.締日 = AppCommon.IntParse(row["締日"].ToString());
            sm.区分 = row["区分"].ToString();
            sm.開始日付1 = ParseDate(row["開始日付1"]);
            sm.終了日付1 = ParseDate(row["終了日付1"]);
            sm.開始日付2 = ParseDate(row["開始日付2"]);
            sm.終了日付2 = ParseDate(row["終了日付2"]);
            sm.開始日付3 = ParseDate(row["開始日付3"]);
            sm.終了日付3 = ParseDate(row["終了日付3"]);
            sm.クリア開始日付 = (DateTime)ParseDate(row["クリア開始日付"]);
            sm.クリア終了日付 = (DateTime)ParseDate(row["クリア終了日付"]);
            sm.入金日 = (DateTime)ParseDate(row["入金日"]);

            return sm;

        }

        /// <summary>
        /// オブジェクト型を日付型に変換して返す
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private DateTime? ParseDate(object date)
        {
            DateTime wkDate;

            if (date == null)
                return null;

            return DateTime.TryParse(date.ToString(), out wkDate) ? wkDate : (DateTime?)null;

        }

        #endregion

    }

}