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
    public class TKS02011
    {
        #region << 列挙型定義 >>
        #endregion

        #region 拡張クラス定義
        /// <summary>
        /// TKS02011_売掛データ一覧表帳票項目定義
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
            public int 入金金額 { get; set; }
            public int 前月繰越 { get; set; }
            public int 残高 { get; set; }
        }

        /// <summary>
        /// 売掛データ登録用クラス定義
        /// </summary>
        public class S08_URIKAKE_Extension
        {
            public int 自社コード { get; set; }
            public int 得意先コード { get; set; }
            public int 得意先枝番 { get; set; }
            public DateTime 日付 { get; set; }
            public int 伝票番号 { get; set; }
            public int 行番号 { get; set; }
            public int 品番コード { get; set; }
            public string 自社品名 { get; set; }
            public int 金種コード { get; set; }
            public decimal 数量 { get; set; }
            public decimal 単価 { get; set; }
            public int 金額 { get; set; }
            public int 通常税率消費税 { get; set; }
            public int 軽減税率消費税 { get; set; }
            public int 入金額 { get; set; }
            public int 前月繰越 { get; set; }
            public int 残高 { get; set; }
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
            DataTable dt = AccountsReceivable(condition);

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
            DataTable dt = AccountsReceivable(condition);

            return dt;

        }
        #endregion

        #region 売掛登録処理
        /// <summary>
        /// 売掛登録処理
        /// </summary>
        /// <param name="condition">検索条件</param>
        /// <returns></returns>
        public DataTable AccountsReceivable(Dictionary<string, string> condition)
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

                    // 対象として取引区分：得意先、相殺、販社を対象とする
                    List<int> kbnList = new List<int>() { (int)CommonConstants.取引区分.得意先, (int)CommonConstants.取引区分.相殺, (int)CommonConstants.取引区分.販社 };

                    // 売掛得意先を取得
                    List<M01_TOK> tokList =
                                context.M01_TOK.Where(w => w.削除日時 == null && kbnList.Contains(w.取引区分) && w.担当会社コード == myCompany).ToList();

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
                            // 販社・通常売掛分岐処理
                            branchAccountsRec(context, myCompany, targetStDate, targetEdDate, tok.取引先コード, tok.枝番, userId);
                        }

                        context.SaveChanges();

                        // 売掛データ取得
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

        #region 売掛登録分岐処理
        /// <summary>
        /// 売掛登録分岐処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">自社コード</param>
        /// <param name="yearMonth">請求年月</param>
        /// <param name="code">取引先コード</param>
        /// <param name="eda">枝番</param>
        /// <param name="userId">ログインユーザID</param>
        public void branchAccountsRec(TRAC3Entities context, int company, DateTime targetStDate, DateTime targetEdDate, int code, int eda, int userId)
        {

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
                // 売掛情報の登録（販社）
                setAccountsRecHan(context, company, tokdata.JIS.自社コード, targetStDate, targetEdDate, code, eda, userId);
            }
            else
            {
                // 売掛情報の登録
                setAccountsRec(context, company, code, eda, targetStDate, targetEdDate, userId);
            }

        }
        #endregion

        #region 売掛データ登録処理
        /// <summary>
        /// 売掛データ登録処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">自社コード</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        /// <param name="userId">ログインユーザID</param>
        private void setAccountsRec(TRAC3Entities context, int company, int code, int eda, DateTime targetStDate, DateTime? targetEdDate, int userId)
        {

            // 売上情報取得
            List<S08_URIKAKE_Extension> uriList = getUriInfo(context, company, code, eda, targetStDate, targetEdDate, userId);

            // 入金情報取得
            List<S08_URIKAKE_Extension> nyukinList = getPaymentInfo(context, company, code, eda, targetStDate, targetEdDate);

            // 売掛情報整形
            var accountsRecList = uriList.Concat(nyukinList).OrderBy(c => c.日付).ThenBy(c => c.伝票番号).ThenBy(c => c.行番号).ToList();

            // データがなくても繰越残高を作成する
            if (accountsRecList == null)
            {
                accountsRecList = new List<S08_URIKAKE_Extension>();
            }

            // 前月残高の再設定
            S08_URIKAKE_Extension befData = getLastAccountsRec(context, company, targetStDate, code, eda);
            accountsRecList.Insert(0, befData);

            int 残高 = befData.前月繰越;

            // 既に登録されている売掛情報を削除
            S08_URIKAKE_Delete(context, company, code, eda, targetStDate, targetEdDate);

            // 売掛情報を登録
            foreach (S08_URIKAKE_Extension row in accountsRecList)
            {

                // 最初の行のみ消費税を設定
                if (accountsRecList.Where(c => c.伝票番号 == row.伝票番号).First().行番号 != row.行番号)
                {
                    row.通常税率消費税 = 0;
                    row.軽減税率消費税 = 0;
                }

                // 残高の再計算
                残高 = 残高 + (row.金額 + row.通常税率消費税 + row.軽減税率消費税) - row.入金額;
                row.残高 = 残高;

                S08_URIKAKE_Update(context, row, userId);
            }
        }

        /// <summary>
        /// 売掛データ登録処理(販社)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="myCompanyCode">自社コード</param>
        /// /// <param name="salesCompanyCode">販社コード(M70_JIS)</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        /// /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="userId">ログインユーザID</param>
        private void setAccountsRecHan(TRAC3Entities context, int myCompanyCode, int salesCompanyCode, DateTime targetStDate, DateTime targetEdDate, int? code, int? eda, int userId)
        {

            // 自社マスタ(販社情報)
            var targetJis =
                context.M70_JIS
                    .Where(w => w.削除日時 == null && w.自社コード == salesCompanyCode)
                    .First();

            // 売上情報取得(販社)
            List<S08_URIKAKE_Extension> uriHanList = getHanUriInfo(context, myCompanyCode, salesCompanyCode, targetStDate, targetEdDate, userId);

            // 入金情報取得
            List<S08_URIKAKE_Extension> nyukinHanList = getPaymentInfo(context, myCompanyCode, targetJis.取引先コード, targetJis.枝番, targetStDate, targetEdDate);

            // 売掛情報整形
            var accountsRecHanList = uriHanList.Concat(nyukinHanList).OrderBy(c => c.日付).ThenBy(c => c.伝票番号).ThenBy(c => c.行番号).ToList();

            // データがなくても繰越残高を作成する
            if (accountsRecHanList == null)
            {
                accountsRecHanList = new List<S08_URIKAKE_Extension>();
            }

            // 前回売掛情報取得し結合
            S08_URIKAKE_Extension befData = getLastAccountsRec(context, myCompanyCode, targetStDate, code, eda);
            accountsRecHanList.Insert(0, befData);

            int 残高 = befData.前月繰越;

            // 既に登録されている売掛情報を削除
            S08_URIKAKE_Delete(context, myCompanyCode, code, eda, targetStDate, targetEdDate);

            // 売掛情報を登録
            foreach (S08_URIKAKE_Extension row in accountsRecHanList)
            {
                // 最初の行のみ消費税を設定
                if (accountsRecHanList.Where(c => c.伝票番号 == row.伝票番号).First().行番号 != row.行番号)
                {
                    row.通常税率消費税 = 0;
                    row.軽減税率消費税 = 0;
                }

                // 残高の再計算
                if (row.前月繰越 == 0)
                {
                    残高 = 残高 + (row.金額 + row.通常税率消費税 + row.軽減税率消費税) - row.入金額;
                    row.残高 = 残高;
                }

                S08_URIKAKE_Update(context, row, userId);
            }

        }

        #endregion

        #region 前月情報取得

        /// <summary>
        /// 前月情報取得
        /// Accounts Receivable：売掛金
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">会社名コード</param>
        /// <param name="yearMonth">集計開始年月</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="cnt">回数</param>
        public S08_URIKAKE_Extension getLastAccountsRec(TRAC3Entities context, int company, DateTime targetStDate, int? code, int? eda)
        {
            // 前月開始日
            DateTime befTargetStDate = targetStDate.AddMonths(-1);

            var befAccountsRec =
                context.S08_URIKAKE
                    .Where(w => w.自社コード == company &&
                        w.日付 >= befTargetStDate && w.日付 < targetStDate &&
                        w.得意先コード == code &&
                        w.得意先枝番 == eda)
                    .OrderByDescending(o => o.日付)
                    .FirstOrDefault();

            // 前月繰越行に整形
            S08_URIKAKE_Extension ret = new S08_URIKAKE_Extension();
            ret.自社コード = company;
            ret.日付 = new DateTime(targetStDate.Year, targetStDate.Month, 1);
            ret.得意先コード = code ?? 0;
            ret.得意先枝番 = eda ?? 0;
            ret.伝票番号 = 0;
            ret.行番号 = 0;
            ret.前月繰越 = befAccountsRec == null ? 0 : befAccountsRec.残高;
            ret.残高 = befAccountsRec == null ? 0 : befAccountsRec.残高;

            return ret;
        }

        #endregion

        #region 売掛テーブル更新処理
        /// <summary>
        /// 売掛テーブル更新処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="hdData"></param>
        private void S08_URIKAKE_Update(TRAC3Entities context, S08_URIKAKE_Extension urData, int userId)
        {
            //INSERTで登録する

            S08_URIKAKE data = new S08_URIKAKE();
            data.自社コード = urData.自社コード;
            data.得意先コード = urData.得意先コード;
            data.得意先枝番 = urData.得意先枝番;
            data.日付 = urData.日付;
            data.伝票番号 = urData.伝票番号;
            data.行番号 = urData.行番号;
            data.品番コード = urData.品番コード;
            data.自社品名 = urData.自社品名;
            data.金種コード = urData.金種コード;
            data.数量 = urData.数量;
            data.単価 = urData.単価;
            data.金額 = urData.金額;
            data.通常税率消費税 = urData.通常税率消費税;
            data.軽減税率消費税 = urData.軽減税率消費税;
            data.入金額 = urData.入金額;
            data.前月繰越 = urData.前月繰越;
            data.残高 = urData.残高;
            data.登録者 = userId;
            data.登録日時 = DateTime.Now;

            context.S08_URIKAKE.ApplyChanges(data);

        }
        #endregion

        #region 売掛テーブル削除処理
        /// <summary>
        /// 売掛テーブル削除処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="myCompanyCode"></param>
        /// <param name="code"></param>
        /// <param name="eda"></param>
        /// <param name="targetStDate"></param>
        /// <param name="targetEdDate"></param>
        private void S08_URIKAKE_Delete(TRAC3Entities context, int myCompanyCode, int? code, int? eda,
                                        DateTime? targetStDate, DateTime? targetEdDate)
        {

            var delData =
          context.S08_URIKAKE.Where(w => w.自社コード == myCompanyCode &&
                        (w.日付 >= targetStDate && w.日付 <= targetEdDate) &&
                        w.得意先コード == code && w.得意先枝番 == eda).ToList();

            foreach (S08_URIKAKE dtl in delData)
            {
                context.S08_URIKAKE.DeleteObject(dtl);
            }

        }
        #endregion

        #region 売掛一覧データ取得
        /// <summary>
        /// 売掛一覧データ取得
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

            List<PrintMember> UrikakeList = new List<PrintMember>();                // No.386 Mod

            // 自社マスタ
            var targetJis =
                context.M70_JIS
                    .Where(w => w.削除日時 == null && w.自社コード == company)
                    .First();

            // 金種(名称)データ取得
            var goldType =
                context.M99_COMBOLIST
                    .Where(w => w.分類 == "随時" && w.機能 == "入金問合せ" && w.カテゴリ == "金種");

            var UrikakeQuery = context.S08_URIKAKE
                    .Where(w => w.自社コード == company &&
                         w.日付 >= targetStDate && w.日付 <= targetEdDate &&
                        w.得意先コード == (customerCode == null ? w.得意先コード : customerCode) &&
                        w.得意先枝番 == (customerEda == null ? w.得意先枝番 : customerEda))
                        .GroupJoin(context.M01_TOK.Where(c => c.削除日時 == null),
                        x => new { コード = x.得意先コード, 枝番 = x.得意先枝番 },
                        y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                        (x, y) => new { x, y })
                        .SelectMany(m => m.y.DefaultIfEmpty(), (a, b) => new { URIKAKE = a.x, TOK = b })
                    .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                        x => x.URIKAKE.品番コード,
                        y => y.品番コード,
                        (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (c, d) => new { c.x.URIKAKE, c.x.TOK, HIN = d })
                    .GroupJoin(context.M06_IRO.Where(c => c.削除日時 == null),
                    x => x.HIN.自社色,
                    y => y.色コード,
                    (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (e, f) => new { e.x.URIKAKE, e.x.TOK, e.x.HIN, IRO = f })
                    .Select(x => new PrintMember
                    {
                        自社コード = x.URIKAKE.自社コード,
                        自社名 = targetJis.自社名,
                        得意先コード = x.URIKAKE.得意先コード,
                        得意先枝番 = x.URIKAKE.得意先枝番,
                        得意先名称 = x.TOK.略称名,
                        日付 = x.URIKAKE.日付,
                        伝票番号 = x.URIKAKE.伝票番号,
                        行番号 = x.URIKAKE.行番号,
                        自社色 = x.URIKAKE.金種コード == 0 ? x.IRO.色名称 : string.Empty,
                        自社品番 = x.HIN.自社品番,
                        自社品名 = x.URIKAKE.金種コード == 0 ? x.URIKAKE.自社品名 : goldType.Where(c => c.コード == x.URIKAKE.金種コード).Select(c => c.表示名).FirstOrDefault(),
                        数量 = x.URIKAKE.数量,
                        単位 = x.HIN.単位,
                        単価 = x.URIKAKE.単価,
                        金額 = x.URIKAKE.金額,
                        通常税率消費税 = x.URIKAKE.通常税率消費税,
                        軽減税率消費税 = x.URIKAKE.軽減税率消費税,
                        入金金額 = x.URIKAKE.入金額,
                        前月繰越 = x.URIKAKE.前月繰越,
                        残高 = x.URIKAKE.残高,
                    })
                    .ToList();

            // 日付をCSV出力用に整形
            foreach (var row in UrikakeQuery)
            {
                row.s日付 = row.日付.ToString("yyyy/MM/dd");

                // No.386 Add Start

                if (UrikakeQuery.Where(c => c.得意先コード == row.得意先コード && c.得意先枝番 == row.得意先枝番).Count() <= 1)
                {
                    if (row.伝票番号 == 0 && row.入金金額 == 0 && row.前月繰越 == 0 && row.残高 == 0)
                        continue;
                }

                UrikakeList.Add(row);

                // No.386 Add End
            }

            var resultList = UrikakeList.OrderBy(c => c.自社コード).ThenBy(c => c.得意先コード).ThenBy(c => c.得意先枝番).ThenBy(c => c.日付)
                .ThenBy(c => c.伝票番号).ThenBy(c => c.行番号).ToList();
            dt = KESSVCEntry.ConvertListToDataTable<PrintMember>(resultList.ToList());


            return dt;
        }
        #endregion

        #region 入金情報取得

        /// <summary>
        /// 入金情報取得
        /// </summary>
        /// <param name="context"></param>
        /// <param name="company">会社名コード</param>
        /// <param name="code">得意先コード</param>
        /// <param name="eda">得意先枝番</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        public List<S08_URIKAKE_Extension> getPaymentInfo(TRAC3Entities context, int company, int? code, int? eda, DateTime? targetStDate, DateTime? targetEdDate)
        {
            // 入金額取得
            var nyukinList =
                context.T11_NYKNHD
                    .Where(w => w.削除日時 == null &&
                        w.入金先自社コード == company &&
                        (w.入金日 >= targetStDate && w.入金日 <= targetEdDate))
                    .Join(context.T11_NYKNDTL.Where(w => w.削除日時 == null),
                        x => x.伝票番号,
                        y => y.伝票番号,
                        (x, y) => new { NYKNHD = x, NYKNDTL = y })
                        .GroupJoin(context.M70_JIS.Where(c => c.削除日時 == null),
                        x => x.NYKNHD.入金元販社コード,
                        y => y.自社コード,
                        (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (c, d) => new { c.x.NYKNHD, c.x.NYKNDTL, TOKJIS = d })
                    .Select(s => new S08_URIKAKE_Extension
                    {
                        自社コード = s.NYKNHD.入金先自社コード,
                        日付 = s.NYKNHD.入金日,
                        伝票番号 = s.NYKNDTL.伝票番号,
                        行番号 = s.NYKNDTL.行番号,
                        品番コード = 0,
                        金種コード = s.NYKNDTL.金種コード,
                        得意先コード = s.NYKNHD.得意先コード != null ? (int)s.NYKNHD.得意先コード : (int)s.TOKJIS.取引先コード,
                        得意先枝番 = s.NYKNHD.得意先枝番 != null ? (int)s.NYKNHD.得意先枝番 : (int)s.TOKJIS.枝番,
                        入金額 = s.NYKNDTL.金額
                    })
                    .ToList();

            nyukinList = nyukinList.Where(c => c.得意先コード == code && c.得意先枝番 == eda).ToList();

            return nyukinList;

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
        public List<S08_URIKAKE_Extension> getUriInfo(TRAC3Entities context, int company, int code, int eda, DateTime? targetStDate, DateTime? targetEdDate, int userId)
        {

            // 基本情報
            List<S08_URIKAKE_Extension> urList =
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
                        (x, y) => new { x.URHD, x.URDTL, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(),
                        (a, b) => new { a.URHD, a.URDTL, TOK = b })
                    .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                        x => x.URDTL.品番コード,
                        y => y.品番コード,
                        (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (c, d) => new { c.x.URHD, c.x.URDTL, c.x.TOK, HIN = d })
                    .ToList()
                    .Select(x => new S08_URIKAKE_Extension
                        {
                            自社コード = x.URHD.会社名コード,
                            得意先コード = x.TOK.取引先コード,
                            得意先枝番 = x.TOK.枝番,
                            日付 = x.URHD.売上日,
                            伝票番号 = x.URDTL.伝票番号,
                            行番号 = x.URDTL.行番号,
                            品番コード = x.URDTL.品番コード,
                            自社品名 = !string.IsNullOrEmpty(x.URDTL.自社品名) ? x.URDTL.自社品名 : x.HIN.自社品名,       // No.389 Add,
                            //金種コード = ,
                            数量 = x.URDTL.数量,
                            単価 = x.URDTL.単価,
                            金額 = x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                                    x.URDTL.金額 ?? 0 : (x.URDTL.金額 ?? 0) * -1,
                            通常税率消費税 = x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                                    x.URHD.通常税率消費税 ?? 0 : (x.URHD.通常税率消費税 ?? 0) * -1,
                            軽減税率消費税 = x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                                    x.URHD.軽減税率消費税 ?? 0 : (x.URHD.軽減税率消費税 ?? 0) * -1,
                            入金額 = 0,
                            前月繰越 = 0,
                            残高 = 0,
                        }).ToList();

            return urList;

        }

        #endregion

        #region 販社売上取得

        /// <summary>
        /// 売上情報取得(販社)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="myCompanyCode">会社名コード</param>
        /// <param name="salesCompanyCode">販社コード(M70_JIS)</param>
        /// <param name="targetStDate">集計開始日</param>
        /// <param name="targetEdDate">集計終了日</param>
        /// <param name="userId">ログインユーザID</param>
        /// <returns></returns>
        public List<S08_URIKAKE_Extension> getHanUriInfo(TRAC3Entities context, int myCompanyCode, int salesCompanyCode, DateTime? targetStDate, DateTime? targetEdDate, int userId)
        {

            // 基本情報
            List<S08_URIKAKE_Extension> urList =
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
                        (a, b) => new { a.x.URHD, a.x.URDTL, a.x.JIS, TOK = b })
                    .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                        x => x.URDTL.品番コード,
                        y => y.品番コード,
                        (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (c, d) => new { c.x.URHD, c.x.URDTL, c.x.JIS, c.x.TOK, HIN = d })
                    .ToList()
                    .Select(x => new S08_URIKAKE_Extension
                        {
                            自社コード = x.URHD.会社名コード,
                            得意先コード = x.TOK.取引先コード,
                            得意先枝番 = x.TOK.枝番,
                            日付 = x.URHD.売上日,
                            伝票番号 = x.URDTL.伝票番号,
                            行番号 = x.URDTL.行番号,
                            品番コード = x.URDTL.品番コード,
                            自社品名 = !string.IsNullOrEmpty(x.URDTL.自社品名) ? x.URDTL.自社品名 : x.HIN.自社品名,       // No.389 Add,
                            数量 = x.URDTL.数量,
                            単価 = x.URDTL.単価,
                            金額 = x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                                    x.URDTL.金額 ?? 0 : (x.URDTL.金額 ?? 0) * -1,
                            通常税率消費税 = x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                                      x.URHD.通常税率消費税 ?? 0 : (x.URHD.通常税率消費税 ?? 0) * -1,
                            軽減税率消費税 = x.URHD.売上区分 < (int)CommonConstants.売上区分.通常売上返品 ?
                                    x.URHD.軽減税率消費税 ?? 0 : (x.URHD.軽減税率消費税 ?? 0) * -1,
                            入金額 = 0,
                            前月繰越 = 0,
                            残高 = 0,
                        }).ToList();

            return urList;

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
