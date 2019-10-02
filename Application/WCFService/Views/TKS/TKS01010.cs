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
	/// 請求締集計サービス
	/// </summary>
	public class TKS01010
    {
        #region << 項目クラス定義 >>

        /// <summary>
        /// 請求対象検索メンバークラス
        /// </summary>
        public class TKS01010_SearchMember
        {
            public string ID { get; set; }
            public int 得意先コード { get; set; }
            public int 得意先枝番 { get; set; }
            public string 得意先名 { get; set; }
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

        #endregion

        #region 拡張クラス定義
        /// <summary>
        /// 入金検索クラス定義
        /// </summary>
        public class T11_NYKN_Search_Extension
        {
            public int 得意先コード { get; set; }
            public int 得意先枝番 { get; set; }
            public int 合計金額 { get; set; }
        }
        #endregion

        #region 請求集計対象の得意先リストを取得
        /// <summary>
        /// 請求集計対象の得意先リストを取得する
        /// </summary>
        /// <param name="自社コード"></param>
        /// <param name="作成年月"></param>
        /// <param name="作成締日"></param>
        /// <param name="得意先コード"></param>
        /// <param name="得意先枝番"></param>
        /// <returns></returns>
        public List<TKS01010_SearchMember> GetListData(string 自社コード, string 作成年月, string 作成締日, string 得意先コード, string 得意先枝番)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                #region パラメータ型変換

                int iVal;
                int company = int.TryParse(自社コード, out iVal) ? iVal : -1;
                int yearMonth = int.TryParse(作成年月.Replace("/", ""), out iVal) ? iVal : -1;
                int? closingDays = int.TryParse(作成締日, out iVal) ? iVal : (int?)null;
                int? code = int.TryParse(得意先コード, out iVal) ? iVal : (int?)null;
                int? eda = int.TryParse(得意先枝番, out iVal) ? iVal : (int?)null;

                #endregion

                #region 検索対象の請求情報取得

                var SEIHD =
                    context.S01_SEIHD.Where(w => w.自社コード == company && w.請求年月 == yearMonth);

                // 作成締日絞込
                if (closingDays != null)
                    SEIHD = SEIHD.Where(w => w.請求締日 == closingDays);

                // 得意先(請求先)絞込
                if (code != null && eda != null)
                    SEIHD = SEIHD.Where(w => w.請求先コード == code && w.請求先枝番 == eda);

                #endregion

                #region 検索対象の取引先情報取得

                // 検索対象の取引区分リスト
                List<int> kbnList = new List<int>() {
                    CommonConstants.取引区分.得意先.GetHashCode(),
                    CommonConstants.取引区分.相殺.GetHashCode(),
                    CommonConstants.取引区分.販社.GetHashCode()
                };

                var TOK =
                    context.M01_TOK
                        .Where(w =>
                            w.削除日時 == null &&
                            w.担当会社コード == company &&
                            kbnList.Contains(w.取引区分) &&
                            (w.Ｔ締日 > 0 || w.Ｔ締日 == null));

                // 作成締日絞込
                if (closingDays != null)
                    TOK = TOK.Where(w => w.Ｔ締日 == closingDays);

                // 得意先(請求先)絞込
                // No-100 Mod Start
                if (code != null && eda != null)
                {
                    TOK = TOK.Where(w => w.取引先コード == code && w.枝番 == eda);
                }
                else if (code != null)
                {
                    TOK = TOK.Where(w => w.取引先コード == code);
                }
                // No-100 Mod End

                #endregion

                #region 検索情報の取得
                var result = TOK
                    .GroupJoin(SEIHD.Where(w => w.回数 == 1),
                        x => new { コード = x.取引先コード, 枝番 = x.枝番 },
                        y => new { コード = y.請求先コード, 枝番 = y.請求先枝番 },
                        (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (a, b) => new { TOK = a.x, SH1 = b })
                    .GroupJoin(SEIHD.Where(w => w.回数 == 2),
                        x => new { コード = x.TOK.取引先コード, 枝番 = x.TOK.枝番 },
                        y => new { コード = y.請求先コード, 枝番 = y.請求先枝番 },
                        (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (c, d) => new { c.x.TOK, c.x.SH1, SH2 = d })
                    .GroupJoin(SEIHD.Where(w => w.回数 == 3),
                        x => new { コード = x.TOK.取引先コード, 枝番 = x.TOK.枝番 },
                        y => new { コード = y.請求先コード, 枝番 = y.請求先枝番 },
                        (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (e, f) => new { e.x.TOK, e.x.SH1, e.x.SH2, SH3 = f })
                    .ToList()
                    .Select(x => new TKS01010_SearchMember
                    {
                        ID = string.Format("{0:D3} - {1:D2}", x.TOK.取引先コード, x.TOK.枝番),
                        得意先コード = x.TOK.取引先コード,
                        得意先枝番 = x.TOK.枝番,
                        得意先名 = x.TOK.得意先名１,
                        締日 = x.TOK.Ｔ締日 ?? 31,
                        区分 = x.SH1 == null ? "新規" : "",
                        開始日付1 = x.SH1 == null ? (DateTime?)null : x.SH1.集計開始日,
                        終了日付1 = x.SH1 == null ? (DateTime?)null : x.SH1.集計最終日,
                        開始日付2 = x.SH2 == null ? (DateTime?)null : x.SH2.集計開始日,
                        終了日付2 = x.SH2 == null ? (DateTime?)null : x.SH2.集計最終日,
                        開始日付3 = x.SH3 == null ? (DateTime?)null : x.SH3.集計開始日,
                        終了日付3 = x.SH3 == null ? (DateTime?)null : x.SH3.集計最終日
                    })
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

        #region 請求締集計処理
        /// <summary>
        /// 請求締集計処理
        /// </summary>
        /// <param name="tbl">集計対象データテーブル</param>
        /// <param name="作成年月"></param>
        public void BillingAggregation(DataSet ds, int 会社コード, int 作成年月, int userId)
        {
            DataTable tbl = ds.Tables[0];
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                foreach (DataRow row in tbl.Rows)
                {
                    TKS01010_SearchMember mem = getConvertSearchMember(row);
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
        /// <param name="yearMonth">請求年月</param>
        /// <param name="mem">請求リスト行</param>
        /// <param name="cnt">請求回数</param>
        /// <param name="userId">ログインユーザID</param>
        public void getAggregateData(TRAC3Entities context, int company, int yearMonth, TKS01010_SearchMember mem, int cnt, int userId)
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

            if (tokdata.JIS != null && tokdata.JIS.自社区分 == CommonConstants.自社区分.販社.GetHashCode())
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

        #region 請求ヘッダ登録処理
        /// <summary>
        /// 請求ヘッダ登録処理
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
            S01_SEIHD urdata = getHeaderInfo(context, company, yearMonth, code, eda, cnt, targetStDate, targetEdDate, paymentDate, userId);

            // 都度請求の場合はヘッダデータを作成しない
            if (urdata == null)
            {
                return;
            }

            // ヘッダ情報登録
            S01_SEIHD_Update(context, urdata);

        }

        /// <summary>
        /// 請求ヘッダ登録処理(販社)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="myCompanyCode">自社コード</param>
        /// <param name="yearMonth">請求年月(yyyymm)</param>
        /// <param name="salesCompanyCode">販社コード(M70_JIS)</param>
        /// <param name="cnt">回数</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        /// <param name="paymentDate">入金日</param>
        /// <param name="userId">ログインユーザID</param>
        private void setHeaderInfoHan(TRAC3Entities context, int myCompanyCode, int yearMonth, int salesCompanyCode, int cnt, DateTime? targetStDate, DateTime? targetEdDate, DateTime paymentDate, int userId)
        {
            // ヘッダ情報取得(販社)
            S01_SEIHD urdata = getHeaderInfoHan(context, myCompanyCode, yearMonth, salesCompanyCode, cnt, targetStDate, targetEdDate, paymentDate, userId);

            // 都度請求の場合はヘッダデータを作成しない
            if (urdata == null)
            {
                return;
            }

            // ヘッダ情報登録
            S01_SEIHD_Update(context, urdata);

        }

        #endregion

        #region 請求明細登録処理
        /// <summary>
        /// 請求明細登録処理
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
            List<S01_SEIDTL> dtlList = getDetailInfo(context, company, yearMonth, code, eda, cnt, targetStDate, targetEdDate, paymentDate, userId);
            
            // 明細情報の登録
            S01_SEIDTL_Update(context, dtlList.ToList());

        }

        /// <summary>
        /// 請求明細登録処理(販社)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="myCompanyCode">自社コード</param>
        /// <param name="yearMonth">請求年月</param>
        /// <param name="salesCompanyCode">販社コード(M70_JIS)</param>
        /// <param name="cnt">回数</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        /// <param name="paymentDate">入金日</param>
        /// <param name="userId">ログインユーザID</param>
        private void setDetailInfoHan(TRAC3Entities context, int myCompanyCode, int yearMonth, int salesCompanyCode, int cnt, DateTime? targetStDate, DateTime? targetEdDate, DateTime paymentDate, int userId)
        {
            // 明細情報取得
            List<S01_SEIDTL> dtlList = getDetailInfoHan(context, myCompanyCode, yearMonth, salesCompanyCode, cnt, targetStDate, targetEdDate, paymentDate, userId);

            // 明細情報の登録
            S01_SEIDTL_Update(context, dtlList.ToList());

        }

        #endregion

        #region 請求ヘッダ情報取得
        /// <summary>
        /// 請求ヘッダ情報取得
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
        public S01_SEIHD getHeaderInfo(TRAC3Entities context, int company, int yearMonth, int code, int eda, int cnt, DateTime? targetStDate, DateTime? targetEdDate, DateTime paymentDate, int userId)
        {
            int 得意先入金日 = int.Parse(paymentDate.ToString("yyyyMMdd"));

            // No-100 Mod Start
            // 前回請求情報取得
            var befSeiCnt = getLastChargeInfo(context, company, yearMonth, code, eda, cnt);

            // 入金情報取得
            var nyukin = getPaymentInfo(context, company, code, eda, targetStDate, targetEdDate);
            // No-100 Mod End

            // 基本情報
            var urList =
                context.T02_URHD
                    .Where(w => w.削除日時 == null &&
                        w.会社名コード == company &&
                        w.得意先コード == code &&
                        w.得意先枝番 == eda &&
                        w.売上日 >= targetStDate && w.売上日 <= targetEdDate)
                    .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                        x => new { コード = x.得意先コード, 枝番 = x.得意先枝番 },
                        y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                        (x, y) => new { URHD = x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (a, b) => new { a.URHD, TOK = b });

            var wkData =
                urList
                    .GroupJoin(context.M73_ZEI.Where(w => w.削除日時 == null),
                        x => context.M73_ZEI.Where(w => w.削除日時 == null && w.適用開始日付 <= x.URHD.売上日).Max(m => m.適用開始日付),
                        y => y.適用開始日付,
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(),
                        (e, f) => new { e.x.URHD, e.x.TOK, ZEI = f })
                    .ToList()
                    .Select(x => new
                    {
                        自社コード = x.URHD.会社名コード,
                        請求年月 = yearMonth,
                        請求締日 = x.TOK.Ｔ締日 ?? 31,
                        請求先コード = x.URHD.得意先コード,
                        請求先枝番 = x.URHD.得意先枝番,
                        回数 = cnt,
                        請求年月日 = AppCommon.GetClosingDate(yearMonth / 100, yearMonth % 100, x.TOK.Ｔ締日 ?? 31, 0),
                        集計開始日 = targetStDate,
                        集計最終日 = targetEdDate,
                        支払消費税区分 = x.TOK.Ｔ消費税区分,
                        // No-94 Add Start
                        消費税丸め区分 = x.TOK.Ｔ税区分ID,
                        通常税率対象金額 =
                            x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                                x.URHD.通常税率対象金額 : x.URHD.通常税率対象金額 * -1,
                        軽減税率対象金額 =
                            x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                                x.URHD.軽減税率対象金額 : x.URHD.軽減税率対象金額 * -1,
                        通常税率消費税 =
                            x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                                x.URHD.通常税率消費税 : x.URHD.通常税率消費税 * -1,
                        軽減税率消費税 =
                            x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                                x.URHD.軽減税率消費税 : x.URHD.軽減税率消費税 * -1,
                        伝票非課税金額 =
                            x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                                (x.URHD.小計 - (x.URHD.通常税率対象金額 + x.URHD.軽減税率対象金額)) :
                                (x.URHD.小計 - (x.URHD.通常税率対象金額 + x.URHD.軽減税率対象金額)) * -1,
                        伝票金額 =
                            x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                                x.URHD.小計 : x.URHD.小計 * -1,
                        // No-94 Add End
                    });

            // ヘッダ情報整形
            var urdata =
                wkData
                    .GroupJoin(context.M73_ZEI.Where(w => w.削除日時 == null),
                        x => context.M73_ZEI.Where(w => w.削除日時 == null && w.適用開始日付 <= x.請求年月日).Max(m => m.適用開始日付),
                        y => y.適用開始日付,
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(), (a, b) => new { Data = a.x, ZEI = b })
                    .GroupBy(g => new
                    {
                        g.Data.自社コード,
                        g.Data.請求年月,
                        g.Data.請求締日,
                        g.Data.請求先コード,
                        g.Data.請求先枝番,
                        g.Data.回数,
                        g.Data.請求年月日,
                        g.Data.集計開始日,
                        g.Data.集計最終日,
                        g.Data.支払消費税区分,
                        g.ZEI.消費税率,
                        // No-94 Add Start
                        g.ZEI.軽減税率,
                        g.Data.消費税丸め区分
                        // No-94 Add End
                    })
                    .Select(x => new S01_SEIHD
                    {
                        自社コード = x.Key.自社コード,
                        請求年月 = x.Key.請求年月,
                        請求締日 = x.Key.請求締日,
                        請求先コード = x.Key.請求先コード,
                        請求先枝番 = x.Key.請求先枝番,
                        入金日 = 得意先入金日,
                        回数 = x.Key.回数,
                        請求年月日 = x.Key.請求年月日,
                        集計開始日 = x.Key.集計開始日,
                        集計最終日 = x.Key.集計最終日,
                        // No-100 Mod Start
                        前月残高 = x.Key.請求締日 == 0 ? 0 : befSeiCnt == null ? 0 : befSeiCnt.当月請求額,
                        入金額 = nyukin == null ? 0 : nyukin.合計金額,
                        繰越残高 = 0,
                        // No-100 Mod End
                        // No-94 Mod Start
                        売上額 = (long)x.Sum(s => s.Data.伝票金額),
                        値引額 = 0,
                        非税売上額 = (long)x.Sum(s => s.Data.伝票非課税金額),
                        消費税 = 0,
                        // No-94 Mod End
                        // No-94 Add Start
                        通常税率対象金額 = (long)x.Sum(s => s.Data.通常税率対象金額),
                        軽減税率対象金額 = (long)x.Sum(s => s.Data.軽減税率対象金額),
                        // No.135-1 Mod Start
                        通常税率消費税 =
                            x.Key.支払消費税区分 == (int)CommonConstants.消費税区分.ID01_一括 ?
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
                        軽減税率消費税 =
                            x.Key.支払消費税区分 == (int)CommonConstants.消費税区分.ID01_一括 ?
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
                        // No.135-1 Mod End
                        // No-94 Add End
                        当月請求額 = 0,
                        登録者 = userId,
                        登録日時 = DateTime.Now
                    })
                    .FirstOrDefault();

            if (urdata == null)
            {
                // 空のヘッダ情報を作成
                urdata =
                    context.M01_TOK
                        .Where(w => w.削除日時 == null && w.取引先コード == code && w.枝番 == eda)
                        .ToList()
                        .Select(x => new S01_SEIHD
                        {
                            自社コード = company,
                            請求年月 = yearMonth,
                            請求締日 = x.Ｔ締日 ?? 31,
                            請求先コード = x.取引先コード,
                            請求先枝番 = x.枝番,
                            入金日 = 得意先入金日,
                            回数 = cnt,
                            請求年月日 = AppCommon.GetClosingDate(yearMonth / 100, yearMonth % 100, x.Ｔ締日 ?? 31, 0),
                            集計開始日 = targetStDate,
                            集計最終日 = targetEdDate,
                            // No-100 Add Start
                            前月残高 = x.Ｔ締日 == 0 ? 0 : befSeiCnt == null ? 0 : befSeiCnt.当月請求額,
                            入金額 = nyukin == null ? 0 : nyukin.合計金額,
                            繰越残高 = 0,
                            // No-100 Add End
                            登録者 = userId,
                            登録日時 = DateTime.Now
                        })
                        .FirstOrDefault();

                // 都度請求の場合は空ヘッダデータを作成しない
                if (urdata.請求締日 == 0)
                    return null;

            }

            // 繰越残高を設定
            urdata.繰越残高 = urdata.前月残高 - urdata.入金額;
            // 消費税を設定
            urdata.消費税 = urdata.通常税率消費税 + urdata.軽減税率消費税;
            // 請求額を設定
            urdata.当月請求額 = urdata.繰越残高 + urdata.売上額 + urdata.消費税;

            return urdata;

        }

        /// <summary>
        /// 請求ヘッダ情報取得(販社)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="myCompanyCode">自社コード</param>
        /// <param name="yearMonth">請求年月(yyyymm)</param>
        /// <param name="salesCompanyCode">販社コード(M70_JIS)</param>
        /// <param name="cnt">回数</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        /// <param name="paymentDate">入金日</param>
        /// <param name="userId">ログインユーザID</param>
        public S01_SEIHD getHeaderInfoHan(TRAC3Entities context, int myCompanyCode, int yearMonth, int salesCompanyCode, int cnt, DateTime? targetStDate, DateTime? targetEdDate, DateTime paymentDate, int userId)
        {
            int 販社入金日 = int.Parse(paymentDate.ToString("yyyyMMdd"));

            // 自社マスタ(販社情報)
            var targetJis =
                context.M70_JIS
                    .Where(w => w.削除日時 == null && w.自社コード == salesCompanyCode)
                    .First();

            // No-100 Mod Start
            // 前回請求情報取得
            var befSeiCnt = getLastChargeInfo(context, myCompanyCode, yearMonth, targetJis.取引先コード, targetJis.枝番, cnt);

            // 入金情報取得
            var nyukin = getPaymentInfo(context, myCompanyCode, targetJis.取引先コード, targetJis.枝番, targetStDate, targetEdDate);
            // No-100 Mod End

            // 基本情報
            var urList =
                context.T02_URHD_HAN
                    .Where(w => w.削除日時 == null &&
                        w.会社名コード == myCompanyCode &&
                        w.販社コード == salesCompanyCode &&
                        w.売上日 >= targetStDate && w.売上日 <= targetEdDate)
                    .Join(context.M70_JIS.Where(w => w.削除日時 == null),
                        x => x.販社コード,
                        y => y.自社コード,
                        (x, y) => new { URHD = x, JIS = y })
                    .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                        x => new { コード = (int)x.JIS.取引先コード, 枝番 = (int)x.JIS.枝番 },
                        y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                        (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (a, b) => new { a.x.URHD, a.x.JIS, TOK = b });

            var wkData =
                urList
                    .GroupJoin(context.M73_ZEI.Where(w => w.削除日時 == null),
                        x => context.M73_ZEI.Where(w => w.削除日時 == null && w.適用開始日付 <= x.URHD.売上日).Max(m => m.適用開始日付),
                        y => y.適用開始日付,
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(),
                        (e, f) => new { e.x.URHD, e.x.TOK, ZEI = f })
                    .ToList()
                    .Select(x => new
                    {
                        自社コード = x.URHD.会社名コード,
                        請求年月 = yearMonth,
                        請求締日 = x.TOK.Ｔ締日 ?? 31,
                        請求先コード = x.TOK.取引先コード,
                        請求先枝番 = x.TOK.枝番,
                        回数 = cnt,
                        請求年月日 = AppCommon.GetClosingDate(yearMonth / 100, yearMonth % 100, x.TOK.Ｔ締日 ?? 31, 0),
                        集計開始日 = targetStDate,
                        集計最終日 = targetEdDate,
                        支払消費税区分 = x.TOK.Ｔ消費税区分,
                        // No-94 Add Start
                        消費税丸め区分 = x.TOK.Ｔ税区分ID,
                        通常税率対象金額 =
                            x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                                x.URHD.通常税率対象金額 : x.URHD.通常税率対象金額 * -1,
                        軽減税率対象金額 =
                            x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                                x.URHD.軽減税率対象金額 : x.URHD.軽減税率対象金額 * -1,
                        通常税率消費税 =
                            x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                                x.URHD.通常税率消費税 : x.URHD.通常税率消費税 * -1,
                        軽減税率消費税 =
                            x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                                x.URHD.軽減税率消費税 : x.URHD.軽減税率消費税 * -1,
                        伝票非課税金額 =
                            x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                                (x.URHD.小計 - (x.URHD.通常税率対象金額 + x.URHD.軽減税率対象金額)) :
                                (x.URHD.小計 - (x.URHD.通常税率対象金額 + x.URHD.軽減税率対象金額)) * -1,
                        伝票金額 =
                            x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                                x.URHD.小計 : x.URHD.小計 * -1,
                        // No-94 Add End
                    });

            // ヘッダ情報整形
            var urdata =
                wkData
                    .GroupJoin(context.M73_ZEI.Where(w => w.削除日時 == null),
                        x => context.M73_ZEI.Where(w => w.削除日時 == null && w.適用開始日付 <= x.請求年月日).Max(m => m.適用開始日付),
                        y => y.適用開始日付,
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(), (a, b) => new { Data = a.x, ZEI = b })
                    .GroupBy(g => new
                    {
                        g.Data.自社コード,
                        g.Data.請求年月,
                        g.Data.請求締日,
                        g.Data.請求先コード,
                        g.Data.請求先枝番,
                        g.Data.回数,
                        g.Data.請求年月日,
                        g.Data.集計開始日,
                        g.Data.集計最終日,
                        g.Data.支払消費税区分,
                        g.ZEI.消費税率,
                        // No-94 Add Start
                        g.ZEI.軽減税率,
                        g.Data.消費税丸め区分
                        // No-94 Add End
                    })
                    .Select(x => new S01_SEIHD
                    {
                        自社コード = x.Key.自社コード,
                        請求年月 = x.Key.請求年月,
                        請求締日 = x.Key.請求締日,
                        請求先コード = x.Key.請求先コード,
                        請求先枝番 = x.Key.請求先枝番,
                        入金日 = 販社入金日,
                        回数 = x.Key.回数,
                        請求年月日 = x.Key.請求年月日,
                        集計開始日 = x.Key.集計開始日,
                        集計最終日 = x.Key.集計最終日,
                        // No-100 Mod Start
                        前月残高 = x.Key.請求締日 == 0 ? 0 : befSeiCnt == null ? 0 : befSeiCnt.当月請求額,
                        入金額 = nyukin == null ? 0 : nyukin.合計金額,
                        繰越残高 = 0,
                        // No-100 Mod End
                        // No-94 Mod Start
                        売上額 = (long)x.Sum(s => s.Data.伝票金額),
                        値引額 = 0,
                        非税売上額 = (long)x.Sum(s => s.Data.伝票非課税金額),
                        消費税 = 0,
                        // No-94 Mod End
                        // No-94 Add Start
                        通常税率対象金額 = (long)x.Sum(s => s.Data.通常税率対象金額),
                        軽減税率対象金額 = (long)x.Sum(s => s.Data.軽減税率対象金額),
                        // No.135-1 Mod Start
                        通常税率消費税 =
                            x.Key.支払消費税区分 == (int)CommonConstants.消費税区分.ID01_一括 ?
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
                        軽減税率消費税 =
                            x.Key.支払消費税区分 == (int)CommonConstants.消費税区分.ID01_一括 ?
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
                        // No.135-1 Mod End
                        // No-94 Add End
                        当月請求額 = 0,
                        登録者 = userId,
                        登録日時 = DateTime.Now
                    })
                    .FirstOrDefault();

            if (urdata == null)
            {
                // 空のヘッダ情報を作成
                urdata =
                    context.M01_TOK
                        .Where(w => w.削除日時 == null && w.取引先コード == targetJis.取引先コード && w.枝番 == targetJis.枝番)
                        .ToList()
                        .Select(x => new S01_SEIHD
                        {
                            自社コード = myCompanyCode,
                            請求年月 = yearMonth,
                            請求締日 = x.Ｔ締日 ?? 31,
                            請求先コード = x.取引先コード,
                            請求先枝番 = x.枝番,
                            入金日 = 販社入金日,
                            回数 = cnt,
                            請求年月日 = AppCommon.GetClosingDate(yearMonth / 100, yearMonth % 100, x.Ｔ締日 ?? 31, 0),
                            集計開始日 = targetStDate,
                            集計最終日 = targetEdDate,
                            // No-100 Add Start
                            前月残高 = x.Ｔ締日 == 0 ? 0 : befSeiCnt == null ? 0 : befSeiCnt.当月請求額,
                            入金額 = nyukin == null ? 0 : nyukin.合計金額,
                            繰越残高 = 0,
                            // No-100 Add End
                            登録者 = userId,
                            登録日時 = DateTime.Now
                        })
                        .FirstOrDefault();

                // No-100 Add Start
                // 都度請求の場合は空ヘッダデータを作成しない
                if (urdata.請求締日 == 0)
                    return null;
                // No-100 Add End
            }

            // 繰越残高を設定
            urdata.繰越残高 = urdata.前月残高 - urdata.入金額;
            // 消費税を設定
            urdata.消費税 = urdata.通常税率消費税 + urdata.軽減税率消費税;
            // 請求額を設定
            urdata.当月請求額 = urdata.繰越残高 + urdata.売上額 + urdata.消費税;

            return urdata;

        }

        /// <summary>
        /// 前回請求情報取得
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">会社名コード</param>
        /// <param name="yearMonth">作成年月</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="cnt">回数</param>
        public S01_SEIHD getLastChargeInfo(TRAC3Entities context, int company, int yearMonth, int? code, int? eda, int cnt)
        {
            // No-100 Mod Start
            // 前回請求情報取得
            DateTime befCntMonth = new DateTime(yearMonth / 100, yearMonth % 100, 1);
            if (cnt == 1)
            {
                befCntMonth = new DateTime(yearMonth / 100, yearMonth % 100, 1).AddMonths(-1);
            }

            var befSeiCnt =
                context.S01_SEIHD
                    .Where(w => w.自社コード == company &&
                        w.請求年月 == (befCntMonth.Year * 100 + befCntMonth.Month) &&
                        w.請求先コード == code &&
                        w.請求先枝番 == eda)
                    .OrderByDescending(o => o.回数)
                    .FirstOrDefault();

            return befSeiCnt;
            // No-100 Mod End

        }

        /// <summary>
        /// 入金情報取得
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">会社名コード</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        public T11_NYKN_Search_Extension getPaymentInfo(TRAC3Entities context, int company, int? code, int? eda, DateTime? targetStDate, DateTime? targetEdDate)
        {
            // No-100 Mod Start
            // 入金額取得
            var nyukin =
                context.T11_NYKNHD
                    .Where(w => w.削除日時 == null &&
                        w.入金先自社コード == company &&
                        (w.入金日 >= targetStDate && w.入金日 <= targetEdDate) &&
                        w.得意先コード == code && w.得意先枝番 == eda)
                    .Join(context.T11_NYKNDTL.Where(w => w.削除日時 == null),
                        x => x.伝票番号,
                        y => y.伝票番号,
                        (x, y) => new { NYKNHD = x, NYKNDTL = y })
                    .GroupBy(g => new { g.NYKNHD.得意先コード, g.NYKNHD.得意先枝番 })
                    .Select(s => new
                    {
                        得意先コード = s.Key.得意先コード,
                        得意先枝番 = s.Key.得意先枝番,
                        合計金額 = s.Sum(sum => sum.NYKNDTL.金額)
                    })
                    .FirstOrDefault();
            // No-100 Mod End

            T11_NYKN_Search_Extension result = new T11_NYKN_Search_Extension(); 

            if (nyukin == null)
            {
                return null;
            }

            result.得意先コード = nyukin.得意先コード ?? 0;
            result.得意先枝番 = nyukin.得意先枝番 ?? 0;
            result.合計金額 = nyukin.合計金額;

            return result;

        }

        #endregion

        #region 請求明細情報取得
        /// <summary>
        /// 請求明細情報取得
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
        public List<S01_SEIDTL> getDetailInfo(TRAC3Entities context, int company, int yearMonth, int code, int eda, int cnt, DateTime? targetStDate, DateTime? targetEdDate, DateTime paymentDate, int userId)
        {
            int 得意先入金日 = int.Parse(paymentDate.ToString("yyyyMMdd"));

            // 基本情報
            var urList =
                context.T02_URHD
                    .Where(w => w.削除日時 == null &&
                        w.会社名コード == company &&
                        w.得意先コード == code &&
                        w.得意先枝番 == eda &&
                        w.売上日 >= targetStDate && w.売上日 <= targetEdDate)
                    .Join(context.T02_URDTL.Where(w => w.削除日時 == null),
                        x => x.伝票番号,
                        y => y.伝票番号,
                        (x, y) => new { URHD = x, URDTL = y })
                    .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                        x => new { コード = x.URHD.得意先コード, 枝番 = x.URHD.得意先枝番 },
                        y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                        (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (a, b) => new { a.x.URHD, a.x.URDTL, TOK = b })
                    .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                        x => x.URDTL.品番コード,
                        y => y.品番コード,
                        (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (c, d) => new { c.x.URHD, c.x.URDTL, c.x.TOK, HIN = d })
                    .GroupJoin(context.M73_ZEI.Where(w => w.削除日時 == null),
                        x => context.M73_ZEI.Where(w => w.削除日時 == null && w.適用開始日付 <= x.URHD.売上日).Max(m => m.適用開始日付),
                        y => y.適用開始日付,
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(),
                        (e, f) => new { e.x.URHD, e.x.URDTL, e.x.TOK, e.x.HIN, ZEI = f });

            var dtlList =
                urList.ToList()
                    .Select(x => new S01_SEIDTL
                    {
                        自社コード = x.URHD.会社名コード,
                        請求年月 = yearMonth,
                        請求締日 = x.TOK.Ｔ締日 ?? 31,
                        請求先コード = x.URHD.得意先コード,
                        請求先枝番 = x.URHD.得意先枝番,
                        入金日 = 得意先入金日,
                        回数 = cnt,
                        行 = 0,
                        伝票番号 = x.URHD.伝票番号,
                        売上日 = x.URHD.売上日,
                        品番コード = x.URDTL.品番コード,
                        // No-80 Start
                        数量 = x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                            x.URDTL.数量 : x.URDTL.数量 * -1,
                        // No-80 End
                        単価 = x.URDTL.単価,
                        // No-80 Start
                        金額 = x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                            x.URDTL.金額 ?? 0 : (x.URDTL.金額 ?? 0) * -1,
                        消費税 = (
                                x.TOK.Ｔ税区分ID == (int)CommonConstants.税区分.ID01_切捨て ?
                                   (int)Math.Floor((double)(x.URDTL.金額 *
                                        (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                        x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                        0) / (double)100)) :
                                x.TOK.Ｔ税区分ID == (int)CommonConstants.税区分.ID02_四捨五入 ?
                                   (int)Math.Round((double)(x.URDTL.金額 *
                                        (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                        x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                        0) / (double)100), 0, MidpointRounding.AwayFromZero) :
                                x.TOK.Ｔ税区分ID == (int)CommonConstants.税区分.ID03_切上げ ?
                                   (int)Math.Ceiling((double)(x.URDTL.金額 *
                                        (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                        x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                        0) / (double)100)) :
                                    0
                                ) * (x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ? 1 : -1),
                        // No-80 End
                        摘要 = x.URDTL.摘要,
                        登録者 = userId,
                        登録日時 = DateTime.Now
                    });

            return dtlList.ToList();

        }

        /// <summary>
        /// 請求明細情報取得(販社)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="myCompanyCode">自社コード</param>
        /// <param name="yearMonth">請求年月</param>
        /// <param name="salesCompanyCode">販社コード(M70_JIS)</param>
        /// <param name="cnt">回数</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        /// <param name="paymentDate">入金日</param>
        /// <param name="userId">ログインユーザID</param>
        public List<S01_SEIDTL> getDetailInfoHan(TRAC3Entities context, int myCompanyCode, int yearMonth, int salesCompanyCode, int cnt, DateTime? targetStDate, DateTime? targetEdDate, DateTime paymentDate, int userId)
        {
            int 販社入金日 = int.Parse(paymentDate.ToString("yyyyMMdd"));

            // 基本情報
            var urList =
                context.T02_URHD_HAN
                    .Where(w => w.削除日時 == null &&
                        w.会社名コード == myCompanyCode &&
                        w.販社コード == salesCompanyCode &&
                        w.売上日 >= targetStDate && w.売上日 <= targetEdDate)
                    .Join(context.T02_URDTL_HAN.Where(w => w.削除日時 == null),
                        x => x.伝票番号,
                        y => y.伝票番号,
                        (x, y) => new { URHD = x, URDTL = y })
                    .Join(context.M70_JIS.Where(w => w.削除日時 == null),
                        x => x.URHD.販社コード,
                        y => y.自社コード,
                        (x, y) => new { x.URHD, x.URDTL, JIS = y })
                    .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                        x => new { コード = (int)x.JIS.取引先コード, 枝番 = (int)x.JIS.枝番 },
                        y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                        (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (a, b) => new { a.x.URHD, a.x.URDTL, TOK = b })
                    .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                        x => x.URDTL.品番コード,
                        y => y.品番コード,
                        (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (c, d) => new { c.x.URHD, c.x.URDTL, c.x.TOK, HIN = d })
                    .GroupJoin(context.M73_ZEI.Where(w => w.削除日時 == null),
                        x => context.M73_ZEI.Where(w => w.削除日時 == null && w.適用開始日付 <= x.URHD.売上日).Max(m => m.適用開始日付),
                        y => y.適用開始日付,
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(),
                        (e, f) => new { e.x.URHD, e.x.URDTL, e.x.TOK, e.x.HIN, ZEI = f });

            var dtlList =
                urList.ToList()
                    .Select(x => new S01_SEIDTL
                    {
                        自社コード = x.URHD.会社名コード,
                        請求年月 = yearMonth,
                        請求締日 = x.TOK.Ｔ締日 ?? 31,
                        請求先コード = x.TOK.取引先コード,
                        請求先枝番 = x.TOK.枝番,
                        入金日 = 販社入金日,
                        回数 = cnt,
                        行 = 0,
                        伝票番号 = x.URHD.伝票番号,
                        売上日 = x.URHD.売上日,
                        品番コード = x.URDTL.品番コード,
                        // No-80 Start
                        数量 = x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                            x.URDTL.数量 : x.URDTL.数量 * -1,
                        // No-80 End
                        単価 = x.URDTL.単価,
                        // No-80 Start
                        金額 = x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                            x.URDTL.金額 ?? 0 : (x.URDTL.金額 ?? 0) * -1,
                        消費税 = (
                            x.TOK.Ｔ税区分ID == (int)CommonConstants.税区分.ID01_切捨て ?
                               (int)Math.Floor((double)(x.URDTL.金額 *
                                    (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                    x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                    0) / (double)100)) :
                            x.TOK.Ｔ税区分ID == (int)CommonConstants.税区分.ID02_四捨五入 ?
                               (int)Math.Round((double)(x.URDTL.金額 *
                                    (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                    x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                    0) / (double)100), 0, MidpointRounding.AwayFromZero) :
                            x.TOK.Ｔ税区分ID == (int)CommonConstants.税区分.ID03_切上げ ?
                               (int)Math.Ceiling((double)(x.URDTL.金額 *
                                    (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                    x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                    0) / (double)100)) :
                                0) * (x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ? 1 : -1),
                        // No-80 End
                        摘要 = x.URDTL.摘要,
                        登録者 = userId,
                        登録日時 = DateTime.Now
                    });

            return dtlList.ToList();

        }

        #endregion

        #region 請求ヘッダ更新処理
        /// <summary>
        /// 請求ヘッダ更新処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="hdData"></param>
        private void S01_SEIHD_Update(TRAC3Entities context, S01_SEIHD hdData)
        {
            var seihd =
                context.S01_SEIHD.Where(w =>
                    w.自社コード == hdData.自社コード &&
                    w.請求年月 == hdData.請求年月 &&
                    w.請求先コード == hdData.請求先コード &&
                    w.請求先枝番 == hdData.請求先枝番 &&
                    w.入金日 == hdData.入金日 &&
                    w.回数 == hdData.回数)
                    .FirstOrDefault();

            if (seihd == null)
            {
                // 登録なしなので登録をおこなう
                S01_SEIHD data = new S01_SEIHD();

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
                // No-100 Add Start
                data.入金額 = hdData.入金額;
                data.繰越残高 = hdData.繰越残高;
                // No-100 Add End
                data.売上額 = hdData.売上額;
                data.値引額 = hdData.値引額;
                data.非税売上額 = hdData.非税売上額;
                // No-94 Add Start
                data.通常税率対象金額 = hdData.通常税率対象金額;
                data.軽減税率対象金額 = hdData.軽減税率対象金額;
                data.通常税率消費税 = hdData.通常税率消費税;
                data.軽減税率消費税 = hdData.軽減税率消費税;
                // No-94 Add End
                data.消費税 = hdData.消費税;
                data.当月請求額 = hdData.当月請求額;
                data.登録者 = hdData.登録者;
                data.登録日時 = hdData.登録日時;

                context.S01_SEIHD.ApplyChanges(data);

            }
            else
            {
                // 登録済みなので更新をおこなう
                seihd.請求年月日 = hdData.請求年月日;
                seihd.集計開始日 = hdData.集計開始日;
                seihd.集計最終日 = hdData.集計最終日;
                seihd.前月残高 = hdData.前月残高;
                // No-100 Add Start
                seihd.入金額 = hdData.入金額;
                seihd.繰越残高 = hdData.繰越残高;
                // No-100 Add End
                seihd.売上額 = hdData.売上額;
                seihd.値引額 = hdData.値引額;
                seihd.非税売上額 = hdData.非税売上額;
                // No-94 Add Start
                seihd.通常税率対象金額 = hdData.通常税率対象金額;
                seihd.軽減税率対象金額 = hdData.軽減税率対象金額;
                seihd.通常税率消費税 = hdData.通常税率消費税;
                seihd.軽減税率消費税 = hdData.軽減税率消費税;
                // No-94 Add End
                seihd.消費税 = hdData.消費税;
                seihd.当月請求額 = hdData.当月請求額;
                seihd.登録者 = hdData.登録者;
                seihd.登録日時 = hdData.登録日時;

                seihd.AcceptChanges();

            }

        }
        #endregion

        #region 請求明細更新処理
        /// <summary>
        /// 請求明細更新処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="list"></param>
        private void S01_SEIDTL_Update(TRAC3Entities context, List<S01_SEIDTL> list)
        {
            int rowCnt = 1;
            foreach (var dtlData in list)
            {
                // 登録済みのデータを削除
                var delList =
                    context.S01_SEIDTL
                        .Where(w =>
                            w.自社コード == dtlData.自社コード &&
                            w.請求年月 == dtlData.請求年月 &&
                            w.請求締日 == dtlData.請求締日 &&
                            w.請求先コード == dtlData.請求先コード &&
                            w.請求先枝番 == dtlData.請求先枝番 &&
                            w.入金日 == dtlData.入金日 &&
                            w.回数 == dtlData.回数);

                foreach (var delData in delList)
                    context.S01_SEIDTL.DeleteObject(delData);

                // 作成データの登録
                dtlData.行 = rowCnt++;
                context.S01_SEIDTL.ApplyChanges(dtlData);

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
        private void getClosingDatePriod(TRAC3Entities context, int 作成年月, TKS01010_SearchMember targetRow, out DateTime startDate, out DateTime endDate, out DateTime paymentDate)
        {
            int year = 作成年月 / 100;
            int month = 作成年月 % 100;

            // 対象取引先のサイト設定で取得入金年月が変わる
            var tok =
                context.M01_TOK
                    .Where(w => w.削除日時 == null && w.取引先コード == targetRow.得意先コード && w.枝番 == targetRow.得意先枝番)
                    .FirstOrDefault();

            DateTime closingDate = AppCommon.GetClosingDate(year, month, tok.Ｔ締日 ?? 31, 0);

            // 入金日の算出
            try
            {
                // No-100 Mod Start
                DateTime baseDate = new DateTime(year, month, tok.Ｔ入金日１ ?? 31);
                baseDate = baseDate.AddMonths(tok.Ｔサイト１ ?? 0);
                int intNyukinMonth = int.Parse(baseDate.Month.ToString());
                int intNyukinYear = int.Parse(baseDate.Year.ToString());
                paymentDate = AppCommon.GetClosingDate(intNyukinYear, intNyukinMonth, tok.Ｔ入金日１ ?? 31, 0);
                // No-100 Mod End

            }
            catch
            {
                // 基本的にあり得ないがこの場合は当月末日を指定
                paymentDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            }

            // 集計期間設定
            // No-100 Mod Start
            startDate = closingDate.AddDays(1).AddMonths(-1);
            endDate = closingDate;
            // No-100 Mod End

        }

        /// <summary>
        /// データ行をクラスメンバーへ変換をおこなう
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private TKS01010_SearchMember getConvertSearchMember(DataRow row)
        {
            TKS01010_SearchMember sm = new TKS01010_SearchMember();

            sm.ID = row["ID"].ToString();
            sm.得意先コード = int.Parse(row["得意先コード"].ToString());
            sm.得意先枝番 = int.Parse(row["得意先枝番"].ToString());
            sm.得意先名 = row["得意先名"].ToString();
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
