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
    /// 買掛データ一覧サービス
    /// </summary>
    public class SHR02010
    {
        #region << 列挙型定義 >>
        #endregion

        #region 拡張クラス定義
        /// <summary>
        /// SHR02010_買掛データ一覧表帳票項目定義
        /// </summary>
        private class PrintMember
        {
            public int 自社コード { get; set; }
            public string 自社名 { get; set; }
            public int 得意先コード { get; set; }
            public int 得意先枝番 { get; set; }
            public string 得意先名称 { get; set; }
            public DateTime 日付 { get; set; }
            public string s日付 { get; set; }
            public int 伝票番号 { get; set; }
            public int 行番号 { get; set; }
            public string 自社品番 { get; set; }
            public string 自社色 { get; set; }
            public string 自社品名 { get; set; }
            public decimal 数量 { get; set; }
            public string 単位 { get; set; }
            public decimal 単価 { get; set; }
            public int 金額 { get; set; }
            public int 通常税率消費税 { get; set; }
            public int 軽減税率消費税 { get; set; }
            public int 出金金額 { get; set; }
            public int 前月繰越 { get; set; }
            public int 残高 { get; set; }
        }

        /// <summary>
        /// 買掛データ登録用クラス定義
        /// </summary>
        public class S09_KAIKAKE_Extension
        {
            public int 自社コード { get; set; }
            public int 得意先コード { get; set; }
            public int 得意先枝番 { get; set; }
            public DateTime 日付 { get; set; }
            public int 伝票番号 { get; set; }
            public int 行番号 { get; set; }
            public int 品番コード { get; set; }
            public int 金種コード { get; set; }
            public decimal 数量 { get; set; }
            public decimal 単価 { get; set; }
            public int 金額 { get; set; }
            public int 通常税率消費税 { get; set; }
            public int 軽減税率消費税 { get; set; }
            public int 出金額 { get; set; }
            public int 前月繰越 { get; set; }
            public int 残高 { get; set; }
            public int 作成機能ID { get; set; }
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
            DataTable dt = AccountsPayable(condition);

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
            DataTable dt = AccountsPayable(condition);

            return dt;

        }
        #endregion

        #region 買掛登録処理
        /// <summary>
        /// 買掛登録処理
        /// </summary>
        /// <param name="condition">検索条件</param>
        /// <returns></returns>
        public DataTable AccountsPayable(Dictionary<string, string> condition)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // 検索パラメータを展開
                int myCompany, createYearMonth, userId;
                int? customerCode, customerEda;

                getFormParams(condition, out myCompany, out createYearMonth, out customerCode, out customerEda, out userId);

                try
                {
                    context.Connection.Open();

                    // 対象として取引区分：仕入先、加工先、相殺を対象とする
                    List<int> kbnList = new List<int>() { (int)CommonConstants.取引区分.仕入先, (int)CommonConstants.取引区分.加工先, (int)CommonConstants.取引区分.相殺 };

                    // 買掛得意先を取得
                    var Tok = context.M01_TOK.Where(w => w.削除日時 == null && kbnList.Contains(w.取引区分) && w.担当会社コード == myCompany);

                    var jisTok =
                      context.M70_JIS
                          .Where(w => w.削除日時 == null && w.自社区分 == (int)CommonConstants.自社区分.自社)
                          .FirstOrDefault();

                    // 会社コードが販社の場合、自社(マルセン)を仕入先として追加する
                    if (myCompany != jisTok.自社コード)
                    {
                        Tok = Tok.Union(context.M01_TOK.Where(w => w.削除日時 == null && w.取引先コード == jisTok.取引先コード && w.枝番 == jisTok.枝番));
                    }

                    List<M01_TOK> tokList = Tok.ToList();

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

                    DateTime targetStDate = new DateTime(createYearMonth / 100, createYearMonth % 100, 1);
                    DateTime targetEdDate = new DateTime(createYearMonth / 100, createYearMonth % 100, 1).AddMonths(1).AddDays(-1);

                    try
                    {
                        foreach (var tok in tokList)
                        {
                            // 販社・通常買掛作成分岐処理
                            branchAccountsPay(context, myCompany, targetStDate, targetEdDate, tok.取引先コード, tok.枝番, userId);
                        }

                        context.SaveChanges();

                        // 買掛データ取得
                        DataTable dt = getData(context, myCompany, targetStDate, targetEdDate, customerCode, customerEda);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region 買掛登録分岐処理
        /// <summary>
        /// 買掛登録分岐処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">自社コード</param>
        /// <param name="yearMonth">請求年月</param>
        /// <param name="code">取引先コード</param>
        /// <param name="eda">枝番</param>
        /// <param name="userId">ログインユーザID</param>
        public void branchAccountsPay(TRAC3Entities context, int company, DateTime targetStDate, DateTime targetEdDate, int code, int eda, int userId)
        {

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
                // 買掛情報の登録（販社）
                setAccountsPayHan(context, company, tokdata.JIS.自社コード, targetStDate, targetEdDate, code, eda, userId);
            }
            else
            {
                // 買掛情報の登録
                setAccountsPay(context, company, code, eda, targetStDate, targetEdDate, userId);
            }

        }
        #endregion

        #region 買掛データ登録処理
        /// <summary>
        /// 買掛データ登録処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">自社コード</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        /// <param name="userId">ログインユーザID</param>
        private void setAccountsPay(TRAC3Entities context, int company, int code, int eda, DateTime targetStDate, DateTime? targetEdDate, int userId)
        {

            // 仕入情報取得
            List<S09_KAIKAKE_Extension> srList = getSrInfo(context, company, code, eda, targetStDate, targetEdDate, userId);

            // 揚り情報取得
            List<S09_KAIKAKE_Extension> agrList = getAgrInfo(context, company, code, eda, targetStDate, targetEdDate, userId);

            // 出金情報取得
            List<S09_KAIKAKE_Extension> syukkinList = getWithdrawalInfo(context, company, code, eda, targetStDate, targetEdDate);

            // 買掛情報整形
            var accountsPayList = srList.Concat(agrList).Concat(syukkinList).OrderBy(c => c.日付).ThenBy(c => c.伝票番号).ThenBy(c => c.行番号).ToList();

            // データがなくても繰越残高を作成する
            if (accountsPayList == null)
            {
                accountsPayList = new List<S09_KAIKAKE_Extension>();
            }

            // 前月残高の再設定
            S09_KAIKAKE_Extension befData = getLastAccountsPay(context, company, targetStDate, code, eda);
            accountsPayList.Insert(0, befData);

            int 残高 = befData.前月繰越;

            // 既に登録されている買掛情報を削除
            S09_KAIKAKE_Delete(context, company, code, eda, targetStDate, targetEdDate);

            // 買掛情報を登録
            foreach (S09_KAIKAKE_Extension row in accountsPayList)
            {

                // 最初の行のみ消費税を設定
                if (accountsPayList.Where(c => c.伝票番号 == row.伝票番号).First().行番号 != row.行番号)
                {
                    row.通常税率消費税 = 0;
                    row.軽減税率消費税 = 0;
                }

                // 残高の再計算
                残高 = 残高 + (row.金額 + row.通常税率消費税 + row.軽減税率消費税) - row.出金額;
                row.残高 = 残高;

                S09_KAIKAKE_Update(context, row, userId);
            }
        }

        /// <summary>
        /// 買掛データ登録処理(販社)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="myCompanyCode">自社コード</param>
        /// /// <param name="salesCompanyCode">販社コード(M70_JIS)</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        /// /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="userId">ログインユーザID</param>
        private void setAccountsPayHan(TRAC3Entities context, int myCompanyCode, int salesCompanyCode, DateTime targetStDate, DateTime targetEdDate, int? code, int? eda, int userId)
        {

            // 自社マスタ(自社情報)
            var targetJis =
                context.M70_JIS
                    .Where(w => w.削除日時 == null && w.自社コード == salesCompanyCode)
                    .First();

            // 仕入情報取得(販社)
            List<S09_KAIKAKE_Extension> srHanList = getHanSrInfo(context, myCompanyCode, salesCompanyCode, targetStDate, targetEdDate, userId);

            // 揚り情報取得
            List<S09_KAIKAKE_Extension> agrList = getAgrInfo(context, myCompanyCode, code, eda, targetStDate, targetEdDate, userId);

            //  出金情報取得
            List<S09_KAIKAKE_Extension> syukkinList = getWithdrawalInfo(context, myCompanyCode, targetJis.取引先コード, targetJis.枝番, targetStDate, targetEdDate);

            // 買掛情報整形
            var accountsPayHanList = srHanList.Concat(agrList).Concat(syukkinList).OrderBy(c => c.日付).ThenBy(c => c.伝票番号).ThenBy(c => c.行番号).ToList();

            // データがなくても繰越残高を作成する
            if (accountsPayHanList == null)
            {
                accountsPayHanList = new List<S09_KAIKAKE_Extension>();
            }

            // 前回買掛情報取得し結合
            S09_KAIKAKE_Extension befData = getLastAccountsPay(context, myCompanyCode, targetStDate, code, eda);
            accountsPayHanList.Insert(0, befData);

            int 残高 = befData.前月繰越;

            // 既に登録されている買掛情報を削除
            S09_KAIKAKE_Delete(context, myCompanyCode, code, eda, targetStDate, targetEdDate);

            // 買掛情報を登録
            foreach (S09_KAIKAKE_Extension row in accountsPayHanList)
            {
                // 最初の行のみ消費税を設定
                if (accountsPayHanList.Where(c => c.伝票番号 == row.伝票番号).First().行番号 != row.行番号)
                {
                    row.通常税率消費税 = 0;
                    row.軽減税率消費税 = 0;
                }

                // 残高の再計算
                if (row.前月繰越 == 0)
                {
                    残高 = 残高 + (row.金額 + row.通常税率消費税 + row.軽減税率消費税) - row.出金額;
                    row.残高 = 残高;
                }

                S09_KAIKAKE_Update(context, row, userId);
            }

        }

        #endregion

        #region 前月情報取得

        /// <summary>
        /// 前月情報取得
        /// Accounts Payable：買掛金
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">会社名コード</param>
        /// <param name="targetStDate">集計開始年月</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">枝番</param>
        /// <returns></returns>
        public S09_KAIKAKE_Extension getLastAccountsPay(TRAC3Entities context, int company, DateTime targetStDate, int? code, int? eda)
        {
            // 前月開始日
            DateTime befTargetStDate = targetStDate.AddMonths(-1);

            var befAccountsPay =
                context.S09_KAIKAKE
                    .Where(w => w.自社コード == company &&
                        w.日付 >= befTargetStDate && w.日付 < targetStDate &&
                        w.得意先コード == code &&
                        w.得意先枝番 == eda)
                    .OrderByDescending(o => o.日付)
                    .FirstOrDefault();

            // 前月繰越行に整形
            S09_KAIKAKE_Extension ret = new S09_KAIKAKE_Extension();
            ret.自社コード = company;
            ret.日付 = new DateTime(targetStDate.Year, targetStDate.Month, 1);
            ret.得意先コード = code ?? 0;
            ret.得意先枝番 = eda ?? 0;
            ret.伝票番号 = 0;
            ret.行番号 = 0;
            ret.前月繰越 = befAccountsPay == null ? 0 : befAccountsPay.残高;
            ret.残高 = befAccountsPay == null ? 0 : befAccountsPay.残高;

            return ret;
        }

        #endregion

        #region 買掛テーブル更新処理
        /// <summary>
        /// 買掛テーブル更新処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="hdData"></param>
        private void S09_KAIKAKE_Update(TRAC3Entities context, S09_KAIKAKE_Extension srData, int userId)
        {
            //INSERTで登録する

            S09_KAIKAKE data = new S09_KAIKAKE();
            data.自社コード = srData.自社コード;
            data.得意先コード = srData.得意先コード;
            data.得意先枝番 = srData.得意先枝番;
            data.日付 = srData.日付;
            data.伝票番号 = srData.伝票番号;
            data.行番号 = srData.行番号;
            data.品番コード = srData.品番コード;
            data.金種コード = srData.金種コード;
            data.数量 = srData.数量;
            data.単価 = srData.単価;
            data.金額 = srData.金額;
            data.通常税率消費税 = srData.通常税率消費税;
            data.軽減税率消費税 = srData.軽減税率消費税;
            data.出金額 = srData.出金額;
            data.前月繰越 = srData.前月繰越;
            data.残高 = srData.残高;
            data.登録者 = userId;
            data.作成機能ID = srData.作成機能ID;
            data.登録日時 = DateTime.Now;

            context.S09_KAIKAKE.ApplyChanges(data);

        }
        #endregion

        #region 買掛テーブル削除処理
        /// <summary>
        /// 買掛テーブル削除処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="myCompanyCode"></param>
        /// <param name="code"></param>
        /// <param name="eda"></param>
        /// <param name="targetStDate"></param>
        /// <param name="targetEdDate"></param>
        private void S09_KAIKAKE_Delete(TRAC3Entities context, int myCompanyCode, int? code, int? eda,
                                        DateTime? targetStDate, DateTime? targetEdDate)
        {

            var delData =
          context.S09_KAIKAKE.Where(w => w.自社コード == myCompanyCode &&
                        (w.日付 >= targetStDate && w.日付 <= targetEdDate) &&
                        w.得意先コード == code && w.得意先枝番 == eda).ToList();

            foreach (S09_KAIKAKE dtl in delData)
            {
                context.S09_KAIKAKE.DeleteObject(dtl);
            }

        }
        #endregion

        #region 買掛一覧データ取得
        /// <summary>
        /// 買掛一覧データ取得
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">会社コード</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        /// <param name="customerCode">得意先コード</param>
        /// <param name="customerEda">得意先枝番</param>
        /// <returns></returns>
        private DataTable getData(TRAC3Entities context, int company, DateTime targetStDate, DateTime targetEdDate, int? customerCode, int? customerEda)
        {
            DataTable dt = new DataTable();

            // 自社マスタ
            var targetJis =
                context.M70_JIS
                    .Where(w => w.削除日時 == null && w.自社コード == company)
                    .First();

            // 金種(名称)データ取得
            var goldType =
                context.M99_COMBOLIST
                    .Where(w => w.分類 == "随時" && w.機能 == "出金問合せ" && w.カテゴリ == "金種");

            var KaikakeList = context.S09_KAIKAKE
                    .Where(w => w.自社コード == company &&
                         w.日付 >= targetStDate && w.日付 <= targetEdDate &&
                        w.得意先コード == (customerCode == null ? w.得意先コード : customerCode) &&
                        w.得意先枝番 == (customerEda == null ? w.得意先枝番 : customerEda))
                        .GroupJoin(context.M01_TOK.Where(c => c.削除日時 == null),
                        x => new { コード = x.得意先コード, 枝番 = x.得意先枝番 },
                        y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                        (x, y) => new { x, y })
                        .SelectMany(m => m.y.DefaultIfEmpty(), (a, b) => new { KAIKAKE = a.x, TOK = b })
                    .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                        x => x.KAIKAKE.品番コード,
                        y => y.品番コード,
                        (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (c, d) => new { c.x.KAIKAKE, c.x.TOK, HIN = d })
                    .GroupJoin(context.M06_IRO.Where(c => c.削除日時 == null),
                    x => x.HIN.自社色,
                    y => y.色コード,
                    (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (e, f) => new { e.x.KAIKAKE, e.x.TOK, e.x.HIN, IRO = f })
                    .Select(x => new PrintMember
                    {
                        自社コード = x.KAIKAKE.自社コード,
                        自社名 = targetJis.自社名,
                        得意先コード = x.KAIKAKE.得意先コード,
                        得意先枝番 = x.KAIKAKE.得意先枝番,
                        得意先名称 = x.TOK.略称名,
                        日付 = x.KAIKAKE.日付,
                        伝票番号 = x.KAIKAKE.伝票番号,
                        行番号 = x.KAIKAKE.行番号,
                        自社色 = x.KAIKAKE.金種コード == 0 ? x.IRO.色名称 : string.Empty,
                        自社品番 = x.HIN.自社品番,
                        自社品名 = x.KAIKAKE.金種コード == 0 ? x.HIN.自社品名 : goldType.Where(c => c.コード == x.KAIKAKE.金種コード).Select(c => c.表示名).FirstOrDefault(),
                        数量 = x.KAIKAKE.数量,
                        単位 = x.HIN.単位,
                        単価 = x.KAIKAKE.単価,
                        金額 = x.KAIKAKE.金額,
                        通常税率消費税 = x.KAIKAKE.通常税率消費税,
                        軽減税率消費税 = x.KAIKAKE.軽減税率消費税,
                        出金金額 = x.KAIKAKE.出金額,
                        前月繰越 = x.KAIKAKE.前月繰越,
                        残高 = x.KAIKAKE.残高,
                    })
                    .ToList();

            // 日付をCSV出力用に整形
            foreach (var row in KaikakeList)
            {
                row.s日付 = row.日付.ToString("yyyy/MM/dd");
            }

            var resultList = KaikakeList.OrderBy(c => c.自社コード).ThenBy(c => c.得意先コード).ThenBy(c => c.得意先枝番).ThenBy(c => c.日付)
                .ThenBy(c => c.伝票番号).ThenBy(c => c.行番号).ToList();
            dt = KESSVCEntry.ConvertListToDataTable<PrintMember>(resultList.ToList());


            return dt;
        }
        #endregion

        #region 入金情報取得

        /// <summary>
        /// 出金情報取得
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">会社名コード</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        public List<S09_KAIKAKE_Extension> getWithdrawalInfo(TRAC3Entities context, int company, int? code, int? eda, DateTime? targetStDate, DateTime? targetEdDate)
        {
            // 出金額取得
            var syukkinList =
                context.T12_PAYHD
                    .Where(w => w.削除日時 == null &&
                        w.出金元自社コード == company &&
                        (w.出金日 >= targetStDate && w.出金日 <= targetEdDate))
                    .Join(context.T12_PAYDTL.Where(w => w.削除日時 == null),
                        x => x.伝票番号,
                        y => y.伝票番号,
                        (x, y) => new { PAYHD = x, PAYDTL = y })
                    .GroupJoin(context.M70_JIS.Where(c => c.削除日時 == null),
                        x => x.PAYHD.出金先販社コード,
                        y => y.自社コード,
                        (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (c, d) => new { c.x.PAYHD, c.x.PAYDTL, TOKJIS = d })
                    .Select(s => new S09_KAIKAKE_Extension
                    {
                        自社コード = s.PAYHD.出金元自社コード,
                        日付 = s.PAYHD.出金日,
                        伝票番号 = s.PAYDTL.伝票番号,
                        行番号 = s.PAYDTL.行番号,
                        品番コード = 0,
                        金種コード = s.PAYDTL.金種コード,
                        得意先コード = s.PAYHD.得意先コード != null ? (int)s.PAYHD.得意先コード : (int)s.TOKJIS.取引先コード,
                        得意先枝番 = s.PAYHD.得意先枝番 != null ? (int)s.PAYHD.得意先枝番 : (int)s.TOKJIS.枝番,
                        出金額 = s.PAYDTL.金額
                    })
                    .ToList();

            syukkinList = syukkinList.Where(c => c.得意先コード == code && c.得意先枝番 == eda).ToList();

            return syukkinList;

        }

        #endregion

        #region 売上取得

        /// <summary>
        /// 売上情報取得
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">会社名コード</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        /// <param name="userId">ログインユーザID</param>
        /// <returns></returns>
        public List<S09_KAIKAKE_Extension> getSrInfo(TRAC3Entities context, int company, int code, int eda, DateTime? targetStDate, DateTime? targetEdDate, int userId)
        {

            // 基本情報
            List<S09_KAIKAKE_Extension> urList =
                context.T03_SRHD
                    .Where(w => w.削除日時 == null &&
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
                        (x, y) => new { x.SRHD, x.SRDTL, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (a, b) => new { a.SRHD, a.SRDTL, TOK = b }).ToList()
            .Select(x => new S09_KAIKAKE_Extension
            {
                自社コード = x.SRHD.会社名コード,
                得意先コード = x.TOK.取引先コード,
                得意先枝番 = x.TOK.枝番,
                日付 = x.SRHD.仕入日,
                伝票番号 = x.SRDTL.伝票番号,
                行番号 = x.SRDTL.行番号,
                品番コード = x.SRDTL.品番コード,
                //金種コード = ,
                数量 = x.SRDTL.数量,
                単価 = x.SRDTL.単価,
                金額 = x.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                        x.SRDTL.金額 : x.SRDTL.金額 * -1,
                通常税率消費税 = x.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                        x.SRHD.通常税率消費税 ?? 0 : (x.SRHD.通常税率消費税 ?? 0) * -1,
                軽減税率消費税 = x.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                        x.SRHD.軽減税率消費税 ?? 0 : (x.SRHD.軽減税率消費税 ?? 0) * -1,
                出金額 = 0,
                前月繰越 = 0,
                残高 = 0,
                作成機能ID = 1,
            }).ToList();

            return urList;

        }

        #endregion

        #region 販社売上取得

        /// <summary>
        /// 仕入情報取得(販社)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="myCompanyCode">会社名コード</param>
        /// <param name="salesCompanyCode">販社コード(M70_JIS)</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        /// <param name="userId">ログインユーザID</param>
        /// <returns></returns>
        public List<S09_KAIKAKE_Extension> getHanSrInfo(TRAC3Entities context, int myCompanyCode, int salesCompanyCode, DateTime? targetStDate, DateTime? targetEdDate, int userId)
        {

            // 基本情報
            List<S09_KAIKAKE_Extension> srList =
                context.T03_SRHD_HAN
                    .Where(w => w.削除日時 == null &&
                        w.会社名コード == myCompanyCode &&
                        w.仕入先コード == salesCompanyCode &&
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
                        (a, b) => new { a.x.SRHD, a.x.SRDTL, a.x.JIS, TOK = b }).ToList()
                    .Select(x => new S09_KAIKAKE_Extension
                    {
                        自社コード = x.SRHD.会社名コード,
                        得意先コード = x.TOK.取引先コード,
                        得意先枝番 = x.TOK.枝番,
                        日付 = x.SRHD.仕入日,
                        伝票番号 = x.SRDTL.伝票番号,
                        行番号 = x.SRDTL.行番号,
                        品番コード = x.SRDTL.品番コード,
                        数量 = x.SRDTL.数量,
                        単価 = x.SRDTL.単価,
                        金額 = x.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                                x.SRDTL.金額 : x.SRDTL.金額 * -1,
                        通常税率消費税 = x.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                                x.SRHD.通常税率消費税 ?? 0 : (x.SRHD.通常税率消費税 ?? 0) * -1,
                        軽減税率消費税 = x.SRHD.仕入区分 < (int)CommonConstants.仕入区分.返品 ?
                                x.SRHD.軽減税率消費税 ?? 0 : (x.SRHD.軽減税率消費税 ?? 0) * -1,
                        出金額 = 0,
                        前月繰越 = 0,
                        残高 = 0,
                        作成機能ID = 1,
                    }).ToList();

            return srList;

        }

        #endregion

        #region 揚り取得

        /// <summary>
        /// 揚り情報取得
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">自社コード</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="targetStDate">集計期間開始</param>
        /// <param name="targetEdDate">集計期間終了</param>
        /// <param name="userId">ログインユーザID</param>
        /// <returns></returns>
        public List<S09_KAIKAKE_Extension> getAgrInfo(TRAC3Entities context, int company, int? code, int? eda, DateTime? targetStDate, DateTime? targetEdDate, int userId)
        {

            // 基本情報
            List<S09_KAIKAKE_Extension> agrList =
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
                    .Select(x => new S09_KAIKAKE_Extension
                    {
                        自社コード = x.AGRHD.会社名コード,
                        得意先コード = x.AGRHD.外注先コード,
                        得意先枝番 = x.AGRHD.外注先枝番,
                        日付 = x.AGRHD.仕上り日,
                        伝票番号 = x.AGRHD.伝票番号,
                        行番号 = x.AGRDTL.行番号,
                        品番コード = x.AGRDTL.品番コード,
                        数量 = x.AGRDTL.数量 ?? 0,
                        単価 = x.AGRDTL.単価 ?? 0,
                        金額 = x.AGRDTL.金額 ?? 0,
                        通常税率消費税 = x.AGRHD.消費税 ?? 0,
                        軽減税率消費税 = 0,
                        出金額 = 0,
                        前月繰越 = 0,
                        残高 = 0,
                        作成機能ID = 2,
                    }).ToList();

            return agrList;

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
            out int userId)
        {
            int ival = -1;

            myCompany = int.Parse(condition["自社コード"]);
            createYearMonth = int.Parse(condition["作成年月"].Replace("/", ""));
            customerCode = int.TryParse(condition["得意先コード"], out ival) ? ival : (int?)null;
            customerEda = int.TryParse(condition["得意先枝番"], out ival) ? ival : (int?)null;
            userId = int.Parse(condition["userId"]);
        }
        #endregion

    }

}
