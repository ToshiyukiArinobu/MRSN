using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;


namespace KyoeiSystem.Application.Windows.Views
{
    using System.Text;
    using FwRepPreview = KyoeiSystem.Framework.Reports.Preview;
    using WinForms = System.Windows.Forms;

    /// <summary>
    /// 担当者・得意先別売上統計表 フォームクラス
    /// </summary>
    public partial class BSK04010 : WindowReportBase
    {
        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;

        /// <summary>
        /// 画面固有設定項目のクラス定義
        /// ※ 必ず public で定義する。
        /// </summary>
        public class ConfigBSK04010 : FormConfigBase
        {
            public byte[] spConfig = null;
        }

        /// ※ 必ず public で定義する。
        public ConfigBSK04010 frmcfg = null;
        CommonConfig ccfg = null;
        // SPREAD初期状態保存用
        public byte[] spConfig = null;

        #endregion

        #region << 定数定義 >>
        private const string GET_JISHA_INFO = "BSK04010_GetJisInfo";
        private const string GET_PRINT_LIST_MONTH = "BSK04010_GetPrintList_Month";
        private const string GET_PRINT_LIST_DAY = "BSK04010_GetPrintList_Day";
        private const string GET_CSV_LIST_MONTH = "BSK04010_GetCsvList_Month";
        private const string GET_CSV_LIST_DAY = "BSK04010_GetCsvList_Day";

        /// <summary>帳票定義体ファイルパス</summary>
        private const string ReportFileName_Month = @"Files\BSK\BSK04010m.rpt";
        private const string ReportFileName_Day = @"Files\BSK\BSK04010d.rpt";

        /// <summary>初期決算月</summary>
        private const int DEFAULT_SETTLEMENT_MONTH = 3;

        // 画面パラメータ名
        private const string PARAMS_NAME_FISCAL_YEAR = "処理年度";
        private const string PARAMS_NAME_COMPANY  = "自社コード";
        private const string PARAMS_NAME_TANTOU = "担当者コード";
        private const string PARAMS_NAME_CUSTOMER_CODE  = "得意先コード";
        private const string PARAMS_NAME_CUSTOMER_EDA  = "得意先枝番";
        private const string PARAMS_NAME_START_YM = "作成開始年月";
        private const string PARAMS_NAME_END_YM = "作成終了年月";
        private const string PARAMS_NAME_CREATE_YM = "作成月";
        private const string PARAMS_NAME_URIAGESAKI = "売上先";
        private const string PARAMS_NAME_OUTPUT_KIND = "出力帳票";
        private const string PARAMS_NAME_CREATE_TYPE = "作成区分";


        // 帳票パラメータ名
        private const string REPORT_PARAM_NAME_PRIOD_START = "期間開始";
        private const string REPORT_PARAM_NAME_PRIOD_END = "期間終了";
        private const string REPORT_PARAM_NAME_END_DAY = "終了日";
        private const string REPORT_PARAM_NAME_YEAR_MONTH01 = "集計年月１";
        private const string REPORT_PARAM_NAME_YEAR_MONTH02 = "集計年月２";
        private const string REPORT_PARAM_NAME_YEAR_MONTH03 = "集計年月３";
        private const string REPORT_PARAM_NAME_YEAR_MONTH04 = "集計年月４";
        private const string REPORT_PARAM_NAME_YEAR_MONTH05 = "集計年月５";
        private const string REPORT_PARAM_NAME_YEAR_MONTH06 = "集計年月６";
        private const string REPORT_PARAM_NAME_YEAR_MONTH07 = "集計年月７";
        private const string REPORT_PARAM_NAME_YEAR_MONTH08 = "集計年月８";
        private const string REPORT_PARAM_NAME_YEAR_MONTH09 = "集計年月９";
        private const string REPORT_PARAM_NAME_YEAR_MONTH10 = "集計年月１０";
        private const string REPORT_PARAM_NAME_YEAR_MONTH11 = "集計年月１１";
        private const string REPORT_PARAM_NAME_YEAR_MONTH12 = "集計年月１２";

        #endregion

        #region << クラス変数定義 >>
        /// <summary>
        /// M70_JISMember
        /// </summary>
        public class M70_JISMember
        {
            public int 自社コード { get; set; }
            public string 自社名 { get; set; }
            public int 取引先コード { get; set; }
            public int 枝番 { get; set; }
            public int 決算月 { get; set; }
        }

        /// <summary>検索時パラメータ保持用</summary>
        Dictionary<string, string> paramDic = new Dictionary<string, string>();

        #endregion

        #region << 画面初期処理 >>

        /// <summary>
        /// 得意先・商品別売上統計表 コンストラクタ
        /// </summary>
        public BSK04010()
        {
            InitializeComponent();
            this.DataContext = this;

        }

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigBSK04010)ucfg.GetConfigValue(typeof(ConfigBSK04010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigBSK04010();
                ucfg.SetConfigValue(frmcfg);
                frmcfg.spConfig = this.spConfig;
            }
            else
            {
                //表示できるかチェック
                var WidthCHK = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - frmcfg.Left;
                if (WidthCHK > 10)
                {
                    this.Left = frmcfg.Left;
                }
                //表示できるかチェック
                var HeightCHK = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - frmcfg.Top;
                if (HeightCHK > 10)
                {
                    this.Top = frmcfg.Top;
                }
                this.Width = frmcfg.Width;
                this.Height = frmcfg.Height;
            }

            // 権限設定を呼び出す(ucfgを取得した後のに入れる)
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));

            #endregion

            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });
            base.MasterMaintenanceWindowList.Add("M72_TNT", new List<Type> { typeof(MST23010), typeof(SCHM72_TNT) });
            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { typeof(MST01010), typeof(SCHM01_TOK) });

            // 画面初期化
            ScreenClear();

            // コントロール初期化
            InitControl();

            SetFocusToTopControl();

        }

        #endregion

        #region << データ受信 >>

        /// <summary>
        /// 取得データの取り込み
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            try
            {
                this.ErrorMessage = string.Empty;

                base.SetFreeForInput();

                var data = message.GetResultData();
                DataTable tbl = (data is DataTable) ? (data as DataTable) : null;

                switch (message.GetMessageName())
                {
                    case GET_JISHA_INFO:
                        // 作成期間の再設定
                        set決算月(tbl);
                        break;
                    case GET_CSV_LIST_MONTH:
                    case GET_CSV_LIST_DAY:
                        outputCsv(tbl);
                        break;

                    // (月別)売上統計表
                    case GET_PRINT_LIST_MONTH:
                        outputReport(tbl);
                        break;

                    // (日別)売上統計表
                    case GET_PRINT_LIST_DAY:
                        outputReport(tbl);
                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                base.SetFreeForInput();
                this.ErrorMessage = ex.Message;
            }

        }

        /// <summary>
        /// データ受信エラー
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            base.SetFreeForInput();
            base.OnReveivedError(message);
            this.ErrorMessage = (string)message.GetResultData();
        }

        #endregion

        #region << リボン >>

        #region F01 マスタ検索
        /// <summary>
        /// F1　リボン　マスタ検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            try
            {
                var ctl = FocusManager.GetFocusedElement(this);
                var uctext = ViewBaseCommon.FindVisualParent<M01_TOK_TextBox>(ctl as UIElement);

                if (uctext != null)
                {
                    uctext.OpenSearchWindow(this);

                }
                else
                {
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);
                }

            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }

        }
        #endregion

        #region F5 CSV出力
        /// <summary>
        /// F5　リボン　CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            // 入力チェック
            if (!CheckFormValidation())
                return;

            // パラメータ情報設定
            setSearchParams();

            if (this.rdo出力帳票.Text == "0")
            {
                // (月別)
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        GET_CSV_LIST_MONTH,
                        paramDic
                    ));
            }
            else
            {
                // (日別)
                base.SendRequest(
                 new CommunicationObject(
                     MessageType.RequestData,
                     GET_CSV_LIST_DAY,
                     paramDic
                 ));
            }

            base.SetBusyForInput();

        }
        #endregion

        #region F8 印刷
        /// <summary>
        /// F8　リボン　印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            // 入力チェック
            if (!CheckFormValidation())
                return;

            // 作成期間整合性チェック
            if (!CheckPeriodValidation())
                return;

            // パラメータ情報設定
            setSearchParams();

            if (this.rdo出力帳票.Text == "0")
            {
                // (月別)
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        GET_PRINT_LIST_MONTH,
                        paramDic
                    ));
            }
            else
            {
                // (日別)
                base.SendRequest(
                 new CommunicationObject(
                     MessageType.RequestData,
                     GET_PRINT_LIST_DAY,
                     paramDic
                 ));
            }

            base.SetBusyForInput();

        }

        #endregion

        #region F11 終了
        /// <summary>
        /// F11　リボン　終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            this.Close();
        }
        #endregion

        #endregion

        #region << 機能処理関連 >>
        #region 画面初期化
        /// <summary>
        /// 画面初期化
        /// </summary>
        private void ScreenClear()
        {
            this.MaintenanceMode = null;

            this.txt自社.Text1 = string.Empty;
            this.txt担当者.Text1 = string.Empty;
            this.rdo売上先.Text = "0";
            this.txt得意先.Text1 = string.Empty;
            this.txt得意先.Text2 = string.Empty;
            this.txt作成月.Text = string.Empty;
            this.rdo出力帳票.Text = "0";
            this.cmb作成区分.SelectedIndex = 0;

            ResetAllValidation();

        }
        #endregion

        #region コントロール初期化
        private void InitControl()
        {
            // 処理年度の初期値設定
            this.txt処理年度.Text = get処理年度(DateTime.Now.Year, DateTime.Now.Month, DEFAULT_SETTLEMENT_MONTH).ToString();
            
            // 対象自社の初期設定
            this.txt自社.Text1 = ccfg.自社コード.ToString();

            // 担当者の初期設定
            this.txt担当者.Text1 = ccfg.ユーザID.ToString();

            // 作成月の初期設定
            this.txt作成月.Text = DateTime.Now.Month.ToString("D2");
            
            // 作成区分コンボボックス設定
            AppCommon.SetutpComboboxList(this.cmb作成区分, false);

            // 作成期間の初期設定
            get決算月();

        }
        #endregion

        #region 業務入力チェック
        /// <summary>
        /// 業務入力チェックをおこなう
        /// </summary>
        /// <returns></returns>
        private bool CheckFormValidation()
        {
            int ival = 0;

            if (string.IsNullOrEmpty(txt処理年度.Text))
            {
                txt処理年度.Focus();
                ErrorMessage = "処理年度が入力されていません。";
                return false;
            }

            // 月別
            if (this.rdo出力帳票.Text == "0")
            {
                if (string.IsNullOrEmpty(txt作成期間.Text1) || string.IsNullOrEmpty(txt作成期間.Text2))
                {
                    txt作成期間.Focus();
                    ErrorMessage = "作成期間が入力されていません。";
                    return false;
                }
            }
            // 日別
            else
            {
                if (string.IsNullOrEmpty(txt作成月.Text))
                {
                    txt作成月.Focus();
                    ErrorMessage = "作成月が入力されていません。";
                    return false;
                }

                int nYear = Int32.TryParse(txt処理年度.Text, out ival)? ival : 0;
                int nMonth = Int32.TryParse(txt作成月.Text, out ival)? ival : 0;
                DateTime targetYearMonth = new DateTime(nYear, nMonth,1);
                
                if (DateTime.Now < targetYearMonth)
                {
                    txt作成月.Focus();
                    ErrorMessage = "作成月が未来日です。";
                    return false;
                }
            }

            return true;

        }
        #endregion

        #region 作成期間整合性チェック
        /// <summary>
        /// 作成期間整合性チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckPeriodValidation()
        {
            // 日別
            if (this.rdo出力帳票.Text == "1")
            {
                return true;
            }
            
            DateTime dWk;
            StringBuilder startYM = new StringBuilder();
            StringBuilder endYM = new StringBuilder();
            startYM.Append(this.txt作成期間.Text1).Append("/01");
            endYM.Append(this.txt作成期間.Text2).Append("/01");
            DateTime dStartYm = DateTime.TryParse(startYM.ToString(), out dWk)? dWk: DateTime.Today;
            DateTime dEndYm = DateTime.TryParse(endYM.ToString(), out dWk) ? dWk : DateTime.Today;

            if (dStartYm > dEndYm)
            {
                txt作成期間.Focus();
                ErrorMessage = "作成期間が不正です。";
                return false;
            }

            int priodMonth = (dEndYm.Year - dStartYm.Year) * 12 + (dEndYm.Month - dStartYm.Month);
            if (priodMonth > 11)
            {
                txt作成期間.Focus();
                ErrorMessage = "作成期間が不正です。";
                return false;
            }

            return true;
        }
        #endregion

        #region パラメータ設定
        /// <summary>
        /// パラメータを設定する
        /// </summary>
        private void setSearchParams()
        {
            paramDic.Clear();

            paramDic.Add(PARAMS_NAME_FISCAL_YEAR, txt処理年度.Text);
            paramDic.Add(PARAMS_NAME_COMPANY, txt自社.Text1 == null ? null : txt自社.Text1);
            paramDic.Add(PARAMS_NAME_TANTOU, txt担当者.Text1 == null ? null : txt担当者.Text1);
            paramDic.Add(PARAMS_NAME_CUSTOMER_CODE, txt得意先.Text1 == null ? null : txt得意先.Text1);
            paramDic.Add(PARAMS_NAME_CUSTOMER_EDA, txt得意先.Text2 == null ? null : txt得意先.Text2);
            paramDic.Add(PARAMS_NAME_START_YM, txt作成期間.Text1 == null ? null : txt作成期間.Text1);
            paramDic.Add(PARAMS_NAME_END_YM, txt作成期間.Text2 == null ? null : txt作成期間.Text2);
            paramDic.Add(PARAMS_NAME_CREATE_YM, txt作成月.Text == null ? null : txt作成月.Text);
            paramDic.Add(PARAMS_NAME_URIAGESAKI, rdo売上先.Text);
            paramDic.Add(PARAMS_NAME_CREATE_TYPE, cmb作成区分.SelectedIndex.ToString());

        }
        #endregion

        #region 決算月取得
        /// <summary>
        /// 対象自社の決算月を取得する
        /// </summary>
        private void get決算月()
        {
            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    GET_JISHA_INFO,
                    txt自社.Text1
                ));
        }
        #endregion

        #region 決算月の設定
        /// <summary>
        /// 決算月の算出
        /// </summary>
        /// <param name="dt"></param>
        private void set決算月(DataTable dt)
        {
            if (dt == null)
            {
                return;
            }

            List<M70_JISMember>jisList = (List<M70_JISMember>)AppCommon.ConvertFromDataTable(typeof(List<M70_JISMember>), dt);

            int val = 1;
            int n決算月 = Int32.TryParse(jisList[0].決算月.ToString(), out val) ? val : DEFAULT_SETTLEMENT_MONTH;
            
            int n処理年度 = get処理年度(DateTime.Now.Year, DateTime.Now.Month, n決算月);

            // 処理年度の設定
            this.txt処理年度.Text = n処理年度.ToString();


            // 決算月から作成期間を算出する
            int nYear = n決算月 < 4 ? n処理年度 + 1 : n処理年度;
            DateTime dEndMonth = new DateTime(nYear, n決算月, 1);
            DateTime dStartMonth = dEndMonth.AddMonths(-11);
            this.txt作成期間.Text1 = dStartMonth.ToString("yyyy/MM");
            this.txt作成期間.Text2 = dEndMonth.ToString("yyyy/MM");
            
        }
        #endregion

        #region 決算年度算出
        /// <summary>
        /// 決算年度を算出して返す
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="settlementMonth">決算月</param>
        /// <returns></returns>
        private int get処理年度(int year, int month, int settlementMonth)
        {
            int n処理年度 = year;
            // 決算月以前の場合は前年を年度として指定
            if (month < settlementMonth)
                n処理年度 -= 1;

            return n処理年度;

        }
        #endregion

        #region (月)帳票パラメータ取得
        /// <summary>
        /// 帳票パラメータを取得する
        /// </summary>
        /// <returns></returns>
        private List<FwRepPreview.ReportParameter> getMonthPrintParameter()
        {
            int    mCounter = 1;
            DateTime dtVal;
            DateTime? stDate = DateTime.TryParse(txt作成期間.Text1, out dtVal) ? dtVal : (DateTime?)null;
            DateTime? edDate = DateTime.TryParse(txt作成期間.Text2, out dtVal) ? dtVal : (DateTime?)null;
            
            if (stDate == null || edDate == null)
            {
                return null;
            }
            // 末日を設定
            edDate = edDate.Value.AddMonths(1).AddDays(-1);
            
            var parms = new List<FwRepPreview.ReportParameter>()
            {
                // 印字パラメータ設定
                new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_PRIOD_START, VALUE = stDate.Value},
                new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_PRIOD_END, VALUE = edDate.Value},
                new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH01, VALUE = stDate.Value},
                new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH02, VALUE = stDate.Value.AddMonths(mCounter++)},
                new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH03, VALUE = stDate.Value.AddMonths(mCounter++)},
                new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH04, VALUE = stDate.Value.AddMonths(mCounter++)},
                new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH05, VALUE = stDate.Value.AddMonths(mCounter++)},
                new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH06, VALUE = stDate.Value.AddMonths(mCounter++)},
                new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH07, VALUE = stDate.Value.AddMonths(mCounter++)},
                new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH08, VALUE = stDate.Value.AddMonths(mCounter++)},
                new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH09, VALUE = stDate.Value.AddMonths(mCounter++)},
                new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH10, VALUE = stDate.Value.AddMonths(mCounter++)},
                new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH11, VALUE = stDate.Value.AddMonths(mCounter++)},
                new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH12, VALUE = stDate.Value.AddMonths(mCounter++)},
            };
            
            return parms;
        }
        #endregion

        #region (日別)帳票パラメータ取得
        /// <summary>
        /// 帳票パラメータを取得する
        /// </summary>
        /// <returns></returns>
        private List<FwRepPreview.ReportParameter> getDayPrintParameter()
        {
            DateTime dtVal;
            StringBuilder sbCreateYm = new StringBuilder();
            sbCreateYm.Append(txt処理年度.Text).Append("/").Append(txt作成月.Text).Append("/01");

            DateTime? stDate = DateTime.TryParse(sbCreateYm.ToString(), out dtVal) ? dtVal : (DateTime?)null;
            DateTime? edDate = stDate != null ? stDate.Value.AddMonths(1).AddDays(-1) : (DateTime?)null;

            if (stDate == null || edDate == null)
            {
                return null;
            }

            var parms = new List<FwRepPreview.ReportParameter>()
            {
                // 印字パラメータ設定
                new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_PRIOD_START, VALUE = stDate.Value},
                new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_PRIOD_END, VALUE = edDate.Value},
                new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_END_DAY, VALUE = edDate.Value.Day},
            };

            return parms;

        }
        #endregion

        #region ＣＳＶデータ出力
        /// <summary>
        /// ＣＳＶデータの出力をおこなう
        /// </summary>
        /// <param name="tbl"></param>
        private void outputCsv(DataTable tbl)
        {
            if (tbl == null || tbl.Rows.Count == 0)
            {
                MessageBox.Show("出力対象のデータがありません。");
                return;
            }

            WinForms.SaveFileDialog sfd = new WinForms.SaveFileDialog();
            // はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = @"C:\";
            // [ファイルの種類]に表示される選択肢を指定する
            sfd.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
            // 「CSVファイル」が選択されているようにする
            sfd.FilterIndex = 1;
            // タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください";
            // ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == WinForms.DialogResult.OK)
            {
                // CSVファイル出力
                CSVData.SaveCSV(tbl, sfd.FileName, true, true, false, ',');
                MessageBox.Show("CSVファイルの出力が完了しました。");
            }

        }
        #endregion

        #region 帳票出力
        /// <summary>
        /// 帳票の印刷処理をおこなう
        /// </summary>
        /// <param name="tbl"></param>
        private void outputReport(DataTable tbl)
        {
            string reportFileName;
            List<FwRepPreview.ReportParameter> parms;

            PrinterDriver ret = AppCommon.GetPrinter(frmcfg.PrinterName);
            if (ret.Result == false)
            {
                this.ErrorMessage = "プリンタドライバーがインストールされていません！";
                return;
            }
            frmcfg.PrinterName = ret.PrinterName;

            if (tbl == null || tbl.Rows.Count == 0)
            {
                this.ErrorMessage = "印刷データがありません。";
                return;
            }

            try
            {
                base.SetBusyForInput();

                // 月別
                if (rdo出力帳票.Text == "0")
                {
                    parms = getMonthPrintParameter();
                    reportFileName = ReportFileName_Month;
                }
                // 日別
                else
                {
                    parms = getDayPrintParameter();
                    reportFileName = ReportFileName_Day;
                    DateTime endDay = (DateTime)parms[1].VALUE;
                    switch (endDay.Day)
                    {
                        case 28:
                            tbl.Columns.Remove("集計売上額２９");
                            tbl.Columns.Remove("集計売上額３０");
                            tbl.Columns.Remove("集計売上額３１");
                            break;
                        case 29:
                            tbl.Columns.Remove("集計売上額３０");
                            tbl.Columns.Remove("集計売上額３１");
                            break;
                        case 30:
                            tbl.Columns.Remove("集計売上額３１");
                            break;
                        default:
                            break;
                    }
                }
                
                DataTable 印刷データ = tbl.Copy();
                印刷データ.TableName = "担当者・得意先別売上統計表";

                FwRepPreview.ReportPreview view = new FwRepPreview.ReportPreview();
                view.MakeReport(印刷データ.TableName, reportFileName, 0, 0, 0);
                view.SetReportData(印刷データ);

                view.SetupParmeters(parms);

                base.SetFreeForInput();

                view.PrinterName = frmcfg.PrinterName;
                view.IsCustomMode = true;
                setPrinterInfoA3(view);
                view.ShowPreview();
                view.Close();
                frmcfg.PrinterName = view.PrinterName;

            }
            catch (Exception ex)
            {
                base.SetFreeForInput();
                this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
                appLog.Error("担当者・得意先別売上統計表の印刷時に例外が発生しました。", ex);
            }

        }
        #endregion

        #region A3用紙設定
        /// <summary>
        /// A3用紙設定をプリンタに設定する
        /// </summary>
        /// <param name="view"></param>
        private void setPrinterInfoA3(FwRepPreview.ReportPreview view)
        {
            view.PrinterInfo = new FwRepPreview.FwPrinterInfo();
            view.PrinterInfo.paperSizeName = "A3";
            view.PrinterInfo.landscape = true;
            view.PrinterInfo.margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);

        }
        #endregion

        #endregion

        #region Window_Closed
        /// <summary>
        /// 画面が閉じられた時、データを保持する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);

        }
        #endregion

        #region << コントロールイベント処理 >>
        #region 対象自社が変更された時のイベント処理
        /// <summary>
        /// 対象自社が変更された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt自社_cText1Changed(object sender, RoutedEventArgs e)
        {
            txt担当者.Text1 = string.Empty;
            txt担当者.Text2 = string.Empty;
        }
        #endregion

        #region 売上先が変更された時のイベント処理
        /// <summary>
        /// 売上先が変更された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdo売上先_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            this.txt得意先.Text1 = string.Empty;
            this.txt得意先.Text2 = string.Empty;

            // 得意先
            if (this.rdo売上先.Text == "0")
            {
                this.txt得意先.LinkItem = "0,3";
            }
            // 販社
            else if (this.rdo売上先.Text == "1")
            {
                this.txt得意先.LinkItem = "4";
            }
            // 両方
            else
            {
                this.txt得意先.LinkItem = "0,3,4";
            }
        }
        #endregion

        #endregion
    }

}
