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
        private const string ReportFileName_3Month = @"Files\BSK\BSK04010m_3Month.rpt";
        private const string ReportFileName_6Month = @"Files\BSK\BSK04010m_6Month.rpt";
        private const string ReportFileName_Day = @"Files\BSK\BSK04010d.rpt";

        // 画面パラメータ名
        private const string PARAMS_NAME_FISCAL_YEAR = "処理年度";
        private const string PARAMS_NAME_COMPANY = "自社コード";
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

        /// <summary>
        /// 年度指定期間
        /// </summary>
        public class FiscalPeriod
        {
            public DateTime PeriodStart;
            public DateTime PeriodEnd;
        }
        /// <summary>
        /// コンボボックス用
        /// </summary>
        public class ComboBoxClass
        {
            public int コード { get; set; }
            public string 名称 { get; set; }

        }
        #endregion

        #region << 列挙型定義 >>
        /// <summary>
        /// 自社販社区分 内包データ
        /// </summary>
        private enum 自社販社区分 : int
        {
            自社 = 0,
            販社 = 1
        }
        //出力期間コンボ用
        private ComboBoxClass[] _PeriodStatus
            = { 
				  new ComboBoxClass() { コード = 0, 名称 = "1年間", },
				  new ComboBoxClass() { コード = 1, 名称 = "6ヶ月", },
				  new ComboBoxClass() { コード = 2, 名称 = "3ヶ月", },
			  };
        public ComboBoxClass[] PeriodStatus
        {
            get { return _PeriodStatus; }
            set { _PeriodStatus = value; NotifyPropertyChanged(); }
        }
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
                    case GET_CSV_LIST_MONTH:
                        changeColumnsName(tbl);
                        outputCsv(tbl);
                        break;
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
            
            // 対象自社の初期設定
            this.txt自社.Text1 = ccfg.自社コード.ToString();
            this.txt自社.IsEnabled = ccfg.自社販社区分 == 自社販社区分.自社.GetHashCode();  // No.353 Mod

            // 処理年度の初期値設定
            // No.401 Mod Start
            this.FiscalYear.Text = string.Format("{0}/{1}", DateTime.Now.Year, DateTime.Now.Month);
            FiscalPeriod period = getFiscalFromTo(this.FiscalYear.Text);
            this.PeriodYM.Text = string.Format("月度 : {0}～{1}月度", period.PeriodStart.ToString("yyyy/MM"), period.PeriodEnd.ToString("yyyy/MM"));
            
            // 作成月の初期設定
            this.txt作成月.Text = string.Format("{0}/{1}", DateTime.Now.Year, DateTime.Now.Month);
            // No.401 Mod End

            // 作成区分コンボボックス設定
            AppCommon.SetutpComboboxList(this.cmb作成区分, false);

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

            // key項目のエラーチェック                // No.407 Add
            if (!base.CheckKeyItemValidation())
                return false;

            // Validationチェック                   　// No.407 Mod
            if (!CheckAllValidation(true))
            {
                return false;
            }

            // 月別
            if (this.rdo出力帳票.Text == "0")
            {
                if (string.IsNullOrEmpty(FiscalYear.Text))
                {
                    FiscalYear.Focus();
                    ErrorMessage = "作成期間(月別用)が入力されていません。";
                    return false;
                }
                
            }
            // 日別
            else
            {
                if (string.IsNullOrEmpty(txt作成月.Text))
                {
                    txt作成月.Focus();
                    ErrorMessage = "作成月(日別用)が入力されていません。";
                    return false;
                }

                // No.401 Mod Start
                string[] yearMonth = this.txt作成月.Text.Split('/');
                DateTime targetYearMonth = new DateTime(Int32.TryParse(yearMonth[0], out ival) ? ival : -1,
                                                        Int32.TryParse(yearMonth[1], out ival) ? ival : -1,
                                                        1);
                // No.401 Mod End

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

        #region パラメータ設定
        /// <summary>
        /// パラメータを設定する
        /// </summary>
        private void setSearchParams()
        {
            paramDic.Clear();

            paramDic.Add(PARAMS_NAME_FISCAL_YEAR, FiscalYear.Text);
            paramDic.Add(PARAMS_NAME_COMPANY, txt自社.Text1 == null ? null : txt自社.Text1);
            paramDic.Add(PARAMS_NAME_TANTOU, txt担当者.Text1 == null ? null : txt担当者.Text1);
            paramDic.Add(PARAMS_NAME_CUSTOMER_CODE, txt得意先.Text1 == null ? null : txt得意先.Text1);
            paramDic.Add(PARAMS_NAME_CUSTOMER_EDA, txt得意先.Text2 == null ? null : txt得意先.Text2);
            // No.401 Mod Start
            FiscalPeriod period = new FiscalPeriod();
            period = getFiscalFromTo(this.FiscalYear.Text);
            paramDic.Add(PARAMS_NAME_START_YM, period.PeriodStart.ToShortDateString());
            paramDic.Add(PARAMS_NAME_END_YM, period.PeriodEnd.ToShortDateString());
            paramDic.Add(PARAMS_NAME_CREATE_YM, this.txt作成月.Text);
            // No.401 Mod End
            
            paramDic.Add(PARAMS_NAME_URIAGESAKI, rdo売上先.Text);
            paramDic.Add(PARAMS_NAME_CREATE_TYPE, cmb作成区分.SelectedIndex.ToString());

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
            DateTime? stDate = Convert.ToDateTime(paramDic[PARAMS_NAME_START_YM]);     // No.401 Mod
            DateTime? edDate = Convert.ToDateTime(paramDic[PARAMS_NAME_END_YM]);       // No.401 Mod

            if (stDate == null || edDate == null)
            {
                return null;
            }
            
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
            sbCreateYm.Append(txt作成月.Text).Append("/01");       // No.401 Mod

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
        #region 列名編集
        /// <summary>
        /// テーブル列名をCSV出力用に変更して返す
        /// </summary>
        /// <param name="tbl"></param>
        /// <returns></returns>
        private void changeColumnsName(DataTable tbl)
        {
            Dictionary<string, DateTime> printParams = getPrintParameter();     // No.402 Mod
            //3・6ヶ月の場合カラムを削除しておく。
            switch (cmdPeriod.SelectedValue.ToString())
            {
                case "1": //6ヶ月

                    tbl.Columns.Remove("集計売上額０７");
                    tbl.Columns.Remove("集計売上額０８");
                    tbl.Columns.Remove("集計売上額０９");
                    tbl.Columns.Remove("集計売上額１０");
                    tbl.Columns.Remove("集計売上額１１");
                    tbl.Columns.Remove("集計売上額１２");
                    break;
                case "2": //3ヶ月
                    tbl.Columns.Remove("集計売上額０４");
                    tbl.Columns.Remove("集計売上額０５");
                    tbl.Columns.Remove("集計売上額０６");
                    tbl.Columns.Remove("集計売上額０７");
                    tbl.Columns.Remove("集計売上額０８");
                    tbl.Columns.Remove("集計売上額０９");
                    tbl.Columns.Remove("集計売上額１０");
                    tbl.Columns.Remove("集計売上額１１");
                    tbl.Columns.Remove("集計売上額１２");
                    break;
            }
            foreach (DataColumn col in tbl.Columns)
            {
                switch (col.ColumnName)
                {
                    case "集計売上額０１":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH01].ToString("yyyy年M月");
                        break;

                    case "集計売上額０２":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH02].ToString("yyyy年M月");
                        break;

                    case "集計売上額０３":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH03].ToString("yyyy年M月");
                        break;

                    case "集計売上額０４":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH04].ToString("yyyy年M月");
                        break;

                    case "集計売上額０５":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH05].ToString("yyyy年M月");
                        break;

                    case "集計売上額０６":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH06].ToString("yyyy年M月");
                        break;

                    case "集計売上額０７":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH07].ToString("yyyy年M月");
                        break;

                    case "集計売上額０８":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH08].ToString("yyyy年M月");
                        break;

                    case "集計売上額０９":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH09].ToString("yyyy年M月");
                        break;

                    case "集計売上額１０":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH10].ToString("yyyy年M月");
                        break;

                    case "集計売上額１１":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH11].ToString("yyyy年M月");
                        break;

                    case "集計売上額１２":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH12].ToString("yyyy年M月");
                        break;

                }

            }

        }
        #endregion
        #region 帳票パラメータ取得
        /// <summary>
        /// 帳票パラメータを取得する
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, DateTime> getPrintParameter()
        {
            // 期間を算出
            int mCounter = 1;

            DateTime targetMonth = Convert.ToDateTime(paramDic[PARAMS_NAME_START_YM]);   // No.402 Mod
            DateTime lastMonth = Convert.ToDateTime(paramDic[PARAMS_NAME_END_YM]);       // No.402 Mod

            Dictionary<string, DateTime> printDic = new Dictionary<string, DateTime>();
            printDic.Add(REPORT_PARAM_NAME_PRIOD_START, targetMonth);
            printDic.Add(REPORT_PARAM_NAME_PRIOD_END, lastMonth);
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH01, targetMonth);
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH02, targetMonth.AddMonths(mCounter++));
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH03, targetMonth.AddMonths(mCounter++));
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH04, targetMonth.AddMonths(mCounter++));
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH05, targetMonth.AddMonths(mCounter++));
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH06, targetMonth.AddMonths(mCounter++));
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH07, targetMonth.AddMonths(mCounter++));
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH08, targetMonth.AddMonths(mCounter++));
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH09, targetMonth.AddMonths(mCounter++));
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH10, targetMonth.AddMonths(mCounter++));
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH11, targetMonth.AddMonths(mCounter++));
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH12, lastMonth);

            return printDic;

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
                    string selectValue = cmdPeriod.SelectedValue.ToString();
                    switch (selectValue)
                    {
                        case "1":
                            reportFileName = ReportFileName_6Month;
                            break;
                        case "2":
                            reportFileName = ReportFileName_3Month;
                            break;
                        case "0":
                        default:
                            reportFileName = ReportFileName_Month;
                            break;
                    }
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

        #region 出力帳票が変更された時のイベント処理  // No.401 Add
        /// <summary>
        /// 出力帳票が変更された時のイベント処理(ラジオボタンクリック)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdo出力帳票_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            if (this.rdo出力帳票.Text == "0")
            {
                this.FiscalYear.IsEnabled = true;
                this.txt作成月.IsEnabled = false;
            }
            else if (this.rdo出力帳票.Text == "1")
            {
                this.FiscalYear.IsEnabled = false;
                this.txt作成月.IsEnabled = true;
            }
        }
        #endregion

        #region 出力帳票が変更された時のイベント処理  // No.401 Add
        /// <summary>
        /// 出力帳票が変更された時のイベント処理(テキストエリア変更)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdo出力帳票_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == "0")
            {
                this.FiscalYear.IsEnabled = true;
                this.txt作成月.IsEnabled = false;
            }
            else if (e.Text == "1")
            {
                this.FiscalYear.IsEnabled = false;
                this.txt作成月.IsEnabled = true;
            }
        }
        #endregion

        #region 作成期間が変更された時のイベント処理  // No.401 Add
        /// <summary>
        /// 作成期間が変更された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FiscalYear_cTextChanged(object sender, RoutedEventArgs e)
        {
            FiscalPeriod period = new FiscalPeriod();
            // 作成期間を再計算
            period = getFiscalFromTo(this.FiscalYear.Text);
            if (period == null)
            {
                this.PeriodYM.Text = string.Empty;
            }
            else
            {
                this.PeriodYM.Text = string.Format("月度 : {0}～{1}月度", period.PeriodStart.ToString("yyyy/MM"), period.PeriodEnd.ToString("yyyy/MM"));
            }
        }
        #endregion
        /// <summary>
        /// 出力期間コンボボックス変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPeriod_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            FiscalPeriod period = new FiscalPeriod();

            // 年度指定期間を再計算
            period = getFiscalFromTo(this.FiscalYear.Text);
            if (period == null)
            {
                this.PeriodYM.Text = string.Empty;
            }
            else
            {
                this.PeriodYM.Text = string.Format("月度 : {0}～{1}月度", period.PeriodStart.ToString("yyyy/MM"), period.PeriodEnd.ToString("yyyy/MM"));
            }
        }
        #region 年度指定期間の設定
        // No.398 Add Start
        /// <summary>
        /// 年度指定期間の設定
        /// </summary>
        /// <param name="fiscalYm">年度指定yyyy/MM</param>
        /// <returns></returns>
        public FiscalPeriod getFiscalFromTo(string fiscalYm)
        {
            if (string.IsNullOrEmpty(fiscalYm))
            {
                return null;
            }

            FiscalPeriod ret = new FiscalPeriod();
            int ival = -1;
            string[] yearMonth = fiscalYm.Split('/');
            DateTime wkDt = new DateTime(Int32.TryParse(yearMonth[0], out ival) ? ival : -1,
                                         Int32.TryParse(yearMonth[1], out ival) ? ival : -1,
                                         1);
            if (wkDt == null)
            {
                return null;
            }

            string selectValue = cmdPeriod.SelectedValue.ToString();
            switch (selectValue)
            {
                case "1"://6ヶ月
                    ret.PeriodStart = wkDt.AddMonths(-5);
                    ret.PeriodEnd = wkDt.AddMonths(1).AddDays(-1);
                    break;
                case "2"://3ヶ月
                    ret.PeriodStart = wkDt.AddMonths(-2);
                    ret.PeriodEnd = wkDt.AddMonths(1).AddDays(-1);
                    break;
                case "0": //1年
                default:
                    // 年度指定の値から過去12か月が指定期間
                    ret.PeriodStart = wkDt.AddMonths(-11);
                    ret.PeriodEnd = wkDt.AddMonths(1).AddDays(-1);
                    break;
            }

            return ret;
        }
        // No.398 Add End
        #endregion
        #endregion

    }

}
