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
                                kbnList.Contains(w.取引区分) &&
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
                    TOK = TOK.Where(w => w.取引先コード == code && w.枝番 == eda);

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
                        ID = string.Format("{0:D3} - {1:D2}", x.TOK.取引先コード, x.TOK.枝番),
                        支払先コード = x.TOK.取引先コード,
                        支払先枝番 = x.TOK.枝番,
                        支払先名 = x.TOK.得意先名１,
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
            int 仕入先入金日 = int.Parse(paymentDate.ToString("yyyyMMdd"));

            // 基本情報
            var srList =
                context.T03_SRHD
                    .Where(w => w.削除日時 == null &&
                        w.仕入区分 == (int)CommonConstants.仕入区分.通常 &&
                        w.会社名コード == company &&
                        w.仕入先コード == code &&
                        w.仕入先枝番 == eda &&
                        w.仕入日 >= targetStDate && w.仕入日 <= targetEdDate)
                    .Join(context.T03_SRDTL.Where(w => w.削除日時 == null),
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
                        (c, d) => new { c.x.SRHD, c.x.SRDTL, c.x.TOK, HIN = d });

            // 前月情報取得
            DateTime befMonth = new DateTime(yearMonth / 100, yearMonth % 100, 1).AddMonths(-1);
            var befSei =
                context.S02_SHRHD
                    .Where(w => w.自社コード == company && w.支払年月 == (befMonth.Year * 100 + befMonth.Month) && w.支払先コード == code && w.支払先枝番 == eda)
                    .GroupBy(g => new { g.自社コード, g.支払先コード, g.支払先枝番, g.支払締日 })
                    .FirstOrDefault();

            var wkData =
                srList
                    .GroupJoin(context.M73_ZEI.Where(w => w.削除日時 == null),
                        x => context.M73_ZEI.Where(w => w.削除日時 == null && w.適用開始日付 <= x.SRHD.仕入日).Max(m => m.適用開始日付),
                        y => y.適用開始日付,
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(),
                        (e, f) => new { e.x.SRHD, e.x.SRDTL, e.x.TOK, e.x.HIN, ZEI = f })
                    .ToList()
                    .Select(x => new
                    {
                        自社コード = x.SRHD.会社名コード,
                        支払年月 = yearMonth,
                        支払締日 = x.TOK.Ｓ締日 ?? 31,
                        支払先コード = x.SRHD.仕入先コード,
                        支払先枝番 = x.SRHD.仕入先枝番,
                        回数 = cnt,
                        支払年月日 = AppCommon.GetClosingDate(yearMonth / 100, yearMonth % 100, x.TOK.Ｓ入金日１ ?? 31, 0),
                        集計開始日 = targetStDate,
                        集計最終日 = targetEdDate,
                        支払消費税区分 = x.TOK.Ｓ支払消費税区分,
                        消費税区分 = x.HIN.消費税区分,
                        金額 = x.SRDTL.金額,
                        消費税 =
                            x.TOK.Ｓ税区分ID == (int)CommonConstants.税区分.ID01_切捨て ?
                                Math.Floor((double)(x.SRDTL.金額 *
                                    (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                    x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                    0) / 100)) :
                            x.TOK.Ｓ税区分ID == (int)CommonConstants.税区分.ID02_四捨五入 ?
                                Math.Round((double)(x.SRDTL.金額 *
                                    (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                    x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                    0) / 100), 0, MidpointRounding.AwayFromZero) :
                            x.TOK.Ｓ税区分ID == (int)CommonConstants.税区分.ID03_切上げ ?
                                Math.Ceiling((double)(x.SRDTL.金額 *
                                    (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                    x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                    0) / 100)) : 0
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
                        g.ZEI.消費税率
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
                        前月残高 = befSei == null ? 0 : befSei.Sum(s => s.当月支払額),
                        支払額 = (long)x.Sum(s => s.Data.金額),
                        値引額 = 0,
                        非課税支払額 =
                            x.Where(y => y.Data.消費税区分 == (int)CommonConstants.商品消費税区分.非課税)
                                .Sum(m => m.Data.金額),
                        消費税 =
                            x.Key.支払消費税区分 == (int)CommonConstants.消費税区分.ID01_一括 ?
                                (long)(x.Sum(s => s.Data.金額) * x.Key.消費税率 / 100) :
                                (long)x.Sum(s => s.Data.消費税),
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
                            登録者 = userId,
                            登録日時 = DateTime.Now
                        })
                        .FirstOrDefault();

            }

            // 支払額を設定
            srdata.当月支払額 = srdata.支払額 + srdata.消費税;

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
            int 支払日 = int.Parse(paymentDate.ToString("yyyyMMdd"));

            // 自社マスタ
            var targetJis =
                context.M70_JIS
                    .Where(w => w.削除日時 == null && w.自社コード == paymentCompanyCode)
                    .First();

            // 基本情報
            var srList =
                context.T03_SRHD_HAN
                    .Where(w => w.削除日時 == null &&
                        w.仕入区分 == (int)CommonConstants.仕入区分.通常 &&
                        w.会社名コード == myCompanyCode &&
                        w.仕入先コード == paymentCompanyCode &&
                        w.仕入日 >= targetStDate && w.仕入日 <= targetEdDate)
                    .Join(context.T03_SRDTL_HAN.Where(w => w.削除日時 == null),
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
                        (a, b) => new { a.x.SRHD, a.x.SRDTL, a.x.JIS, TOK = b })
                    .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                        x => x.SRDTL.品番コード,
                        y => y.品番コード,
                        (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (c, d) => new { c.x.SRHD, c.x.SRDTL, c.x.JIS, c.x.TOK, HIN = d });

            // 前月情報取得
            DateTime befMonth = new DateTime(yearMonth / 100, yearMonth % 100, 1).AddMonths(-1);
            var befSei =
                context.S02_SHRHD
                    .Where(w => w.自社コード == myCompanyCode &&
                        w.支払年月 == (befMonth.Year * 100 + befMonth.Month) &&
                        w.支払先コード == targetJis.取引先コード &&
                        w.支払先枝番 == targetJis.枝番)
                    .GroupBy(g => new { g.自社コード, g.支払先コード, g.支払先枝番, g.支払締日 })
                    .FirstOrDefault();

            var wkData =
                srList
                    .GroupJoin(context.M73_ZEI.Where(w => w.削除日時 == null),
                        x => context.M73_ZEI.Where(w => w.削除日時 == null && w.適用開始日付 <= x.SRHD.仕入日).Max(m => m.適用開始日付),
                        y => y.適用開始日付,
                        (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(),
                        (e, f) => new { e.x.SRHD, e.x.SRDTL, e.x.TOK, e.x.HIN, ZEI = f })
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
                        消費税区分 = x.HIN.消費税区分,
                        金額 = x.SRDTL.金額,
                        消費税 =
                            x.TOK.Ｓ税区分ID == (int)CommonConstants.税区分.ID01_切捨て ?
                                Math.Floor((double)(x.SRDTL.金額 *
                                    (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                    x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                    0) / 100)) :
                            x.TOK.Ｓ税区分ID == (int)CommonConstants.税区分.ID02_四捨五入 ?
                                Math.Round((double)(x.SRDTL.金額 *
                                    (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                    x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                    0) / 100), 0, MidpointRounding.AwayFromZero) :
                            x.TOK.Ｓ税区分ID == (int)CommonConstants.税区分.ID03_切上げ ?
                                Math.Ceiling((double)(x.SRDTL.金額 *
                                    (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                    x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                    0) / 100)) :
                                0
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
                        g.ZEI.消費税率
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
                        前月残高 = befSei == null ? 0 : befSei.Sum(s => s.当月支払額),
                        支払額 = (long)x.Sum(s => s.Data.金額),
                        値引額 = 0,
                        非課税支払額 =
                            x.Where(y => y.Data.消費税区分 == (int)CommonConstants.商品消費税区分.非課税)
                                .Sum(m => m.Data.金額),
                        消費税 =
                            x.Key.支払消費税区分 == (int)CommonConstants.消費税区分.ID01_一括 ?
                                (long)(x.Sum(s => s.Data.金額) * x.Key.消費税率 / 100) :
                                (long)x.Sum(s => s.Data.消費税),
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
                            登録者 = userId,
                            登録日時 = DateTime.Now
                        })
                        .FirstOrDefault();

            }

            // 支払額を設定
            srdata.当月支払額 = srdata.支払額 + srdata.消費税;

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
            int 仕入先入金日 = int.Parse(paymentDate.ToString("yyyyMMdd"));

            // 基本情報
            var srList =
                context.T03_SRHD
                    .Where(w => w.削除日時 == null &&
                        w.仕入区分 == (int)CommonConstants.仕入区分.通常 &&
                        w.会社名コード == company &&
                        w.仕入先コード == code &&
                        w.仕入先枝番 == eda &&
                        w.仕入日 >= targetStDate && w.仕入日 <= targetEdDate)
                    .Join(context.T02_URDTL.Where(w => w.削除日時 == null),
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
                        数量 = x.SRDTL.数量,
                        単価 = x.SRDTL.単価,
                        金額 = x.SRDTL.金額 ?? 0,
                        消費税 =
                            x.TOK.Ｓ税区分ID == (int)CommonConstants.税区分.ID01_切捨て ?
                               (int)Math.Floor((double)(x.SRDTL.金額 *
                                   (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                   x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                   0) / 100)) :
                            x.TOK.Ｓ税区分ID == (int)CommonConstants.税区分.ID02_四捨五入 ?
                               (int)Math.Round((double)(x.SRDTL.金額 *
                                   (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                   x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                   0) / 100), 0, MidpointRounding.AwayFromZero) :
                            x.TOK.Ｓ税区分ID == (int)CommonConstants.税区分.ID03_切上げ ?
                               (int)Math.Ceiling((double)(x.SRDTL.金額 *
                                   (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                   x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                   0) / 100)) :
                                0,
                        摘要 = x.SRDTL.摘要,
                        登録者 = userId,
                        登録日時 = DateTime.Now
                    });

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
            int 販社入金日 = int.Parse(paymentDate.ToString("yyyyMMdd"));

            // 基本情報
            var srList =
                context.T03_SRHD_HAN
                    .Where(w => w.削除日時 == null &&
                        w.仕入区分 == (int)CommonConstants.仕入区分.通常 &&
                        w.会社名コード == myCompanyCode &&
                        w.仕入先コード == salesCompanyCode &&
                        w.仕入日 >= targetStDate && w.仕入日 <= targetEdDate)
                    .Join(context.T02_URDTL_HAN.Where(w => w.削除日時 == null),
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
                        数量 = x.SRDTL.数量,
                        単価 = x.SRDTL.単価,
                        金額 = x.SRDTL.金額 ?? 0,
                        消費税 =
                            x.TOK.Ｓ税区分ID == (int)CommonConstants.税区分.ID01_切捨て ?
                               (int)Math.Floor((double)(x.SRDTL.金額 *
                                   (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                   x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                   0) / 100)) :
                            x.TOK.Ｓ税区分ID == (int)CommonConstants.税区分.ID02_四捨五入 ?
                               (int)Math.Round((double)(x.SRDTL.金額 *
                                   (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                   x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                   0) / 100), 0, MidpointRounding.AwayFromZero) :
                            x.TOK.Ｓ税区分ID == (int)CommonConstants.税区分.ID03_切上げ ?
                               (int)Math.Ceiling((double)(x.SRDTL.金額 *
                                   (x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.通常税率 ? x.ZEI.消費税率 :
                                   x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? x.ZEI.軽減税率 :
                                   0) / 100)) : 0,
                        摘要 = x.SRDTL.摘要,
                        登録者 = userId,
                        登録日時 = DateTime.Now
                    });

            // 明細情報の登録
            S02_SHRDTL_Update(context, dtlList.ToList());

        }

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
                data.支払額 = hdData.支払額;
                data.値引額 = hdData.値引額;
                data.非課税支払額 = hdData.非課税支払額;
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
                shrhd.支払額 = hdData.支払額;
                shrhd.値引額 = hdData.値引額;
                shrhd.非課税支払額 = hdData.非課税支払額;
                shrhd.消費税 = hdData.消費税;
                shrhd.当月支払額 = hdData.当月支払額;
                shrhd.登録者 = hdData.登録者;
                shrhd.登録日時 = hdData.登録日時;

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
                paymentDate = AppCommon.GetClosingDate(year, month, tok.Ｓ入金日１ ?? 31, 0);
                paymentDate = paymentDate.AddMonths(tok.Ｓサイト１ ?? 0);

            }
            catch
            {
                // 基本的にあり得ないがこの場合は当月末日を指定
                paymentDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            }

            // 集計期間設定
            startDate = closingDate.AddMonths(-1);
            endDate = closingDate.AddDays(-1);

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