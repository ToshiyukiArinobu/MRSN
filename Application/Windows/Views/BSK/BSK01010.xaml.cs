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
    using FwRepPreview = KyoeiSystem.Framework.Reports.Preview;
    using WinForms = System.Windows.Forms;

    /// <summary>
    /// 得意先・商品別売上統計表 フォームクラス
    /// </summary>
    public partial class BSK01010 : WindowReportBase
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
        public class ConfigBSK01010 : FormConfigBase
        {
            public byte[] spConfig = null;
        }

        /// ※ 必ず public で定義する。
        public ConfigBSK01010 frmcfg = null;
        CommonConfig ccfg = null;
        // SPREAD初期状態保存用
        public byte[] spConfig = null;

        #endregion

        #region 一覧データテーブル定義

        private DataTable _searchList;
        public DataTable SearchList
        {
            get { return _searchList; }
            set
            {
                _searchList = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region << 定数定義 >>

        private const string GET_PRINT_LIST = "BSK01010_GetPrintList";
        private const string GET_CSV_LIST = "BSK01010_GetCsvList";

        /// <summary>帳票定義体ファイルパス</summary>
        private const string ReportFileName = @"Files\BSK\BSK01010.rpt";

        /// <summary>初期決算月</summary>
        private const int DEFAULT_SETTLEMENT_MONTH = 3;

        // 画面パラメータ名
        private const string PARAMS_NAME_FISCAL_YEAR = "処理年度";
        private const string PARAMS_NAME_COMPANY  = "自社コード";
        private const string PARAMS_NAME_CUSTOMER_CODE  = "得意先コード";
        private const string PARAMS_NAME_CUSTOMER_EDA  = "得意先枝番";
        private const string PARAMS_NAME_OUTPUT = "出力項目";

        // 帳票パラメータ名
        private const string REPORT_PARAM_NAME_PRIOD_START = "期間開始";
        private const string REPORT_PARAM_NAME_PRIOD_END = "期間終了";
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
        private const string REPORT_PARAM_OUTPUT_MONEY = "(金額)";
        private const string REPORT_PARAM_OUTPUT_NUM = "(数量)";

        #endregion

        #region << クラス変数定義 >>

        /// <summary>検索時パラメータ保持用</summary>
        Dictionary<string, string> paramDic = new Dictionary<string, string>();

        #endregion


        #region << 画面初期処理 >>

        /// <summary>
        /// 得意先・商品別売上統計表 コンストラクタ
        /// </summary>
        public BSK01010()
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
            frmcfg = (ConfigBSK01010)ucfg.GetConfigValue(typeof(ConfigBSK01010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigBSK01010();
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
            // 登録ボタン設定
            if (!権限Get.Authority_Update_Button(ccfg, this.GetType().Name))
            {
                DataUpdateVisible = System.Windows.Visibility.Hidden;
            }

            if (frmcfg.spConfig != null)
            {
                //AppCommon.LoadSpConfig(this.sp請求データ一覧, frmcfg.spConfig);
            }

            #endregion

            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { typeof(MST01010), typeof(SCHM01_TOK) });
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });

            ScreenClear();

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
                    case GET_CSV_LIST:
                        outputCsv(tbl);
                        break;

                    case GET_PRINT_LIST:
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
            if (!formValidation())
                return;

            // パラメータ情報設定
            setSearchParams();

            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    GET_CSV_LIST,
                    paramDic
                ));

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
            if (!formValidation())
                return;

            // パラメータ情報設定
            setSearchParams();

            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    GET_PRINT_LIST,
                    paramDic
                ));

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

            // 自社の入力値クリア
            this.MyCompany.Text1 = string.Empty;

            // 処理年度の初期値設定
            int fiscalYear = getFiscalYear(DateTime.Now.Year, DateTime.Now.Month, DEFAULT_SETTLEMENT_MONTH);
            this.FiscalYear.Text = fiscalYear.ToString();

            // 得意先の入力値クリア
            this.Customer.Text1 = string.Empty;
            this.Customer.Text2 = string.Empty;

            ResetAllValidation();
            SetFocusToTopControl();

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
        private int getFiscalYear(int year, int month, int settlementMonth)
        {
            int fiscalYear = year;
            // 決算月以前の場合は前年を年度として指定
            if (month <= settlementMonth)
                fiscalYear -= 1;

            return fiscalYear;

        }
        #endregion

        #region 業務入力チェック
        /// <summary>
        /// 業務入力チェックをおこなう
        /// </summary>
        /// <returns></returns>
        private bool formValidation()
        {
            if (string.IsNullOrEmpty(FiscalYear.Text))
            {
                FiscalYear.Focus();
                ErrorMessage = "処理年度が入力されていません。";
                return false;
            }

            if (string.IsNullOrEmpty(MyCompany.Text1))
            {
                MyCompany.Focus();
                ErrorMessage = "対象自社が設定されていません。";
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

            paramDic.Add(PARAMS_NAME_FISCAL_YEAR, FiscalYear.Text);
            paramDic.Add(PARAMS_NAME_COMPANY, MyCompany.Text1);
            paramDic.Add(PARAMS_NAME_CUSTOMER_CODE, Customer.Text1);
            paramDic.Add(PARAMS_NAME_CUSTOMER_EDA, Customer.Text2);
            paramDic.Add(PARAMS_NAME_OUTPUT, radioSelect.Text);
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
            int year = int.Parse(paramDic["処理年度"].Replace("/", "")),
                pMonth = DEFAULT_SETTLEMENT_MONTH,
                pYear = pMonth < 4 ? year + 1 : year,
                mCounter = 1;

            DateTime lastMonth = new DateTime(pYear, pMonth, 1);
            DateTime targetMonth = lastMonth.AddMonths(-11);

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

            // CSV出力用に列名を編集する
            changeColumnsName(tbl);

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

                Dictionary<string, DateTime> printParams = getPrintParameter();

                var parms = new List<FwRepPreview.ReportParameter>()
                {
                    #region 印字パラメータ設定
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_PRIOD_START, VALUE = printParams[REPORT_PARAM_NAME_PRIOD_START]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_PRIOD_END, VALUE = printParams[REPORT_PARAM_NAME_PRIOD_END]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH01, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH01]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH02, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH02]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH03, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH03]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH04, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH04]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH05, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH05]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH06, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH06]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH07, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH07]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH08, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH08]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH09, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH09]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH10, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH10]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH11, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH11]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH12, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH12]},
                    new FwRepPreview.ReportParameter(){ PNAME = PARAMS_NAME_OUTPUT, VALUE = radioSelect.Text == "1" ? REPORT_PARAM_OUTPUT_MONEY : REPORT_PARAM_OUTPUT_NUM},
                    #endregion
                };

                DataTable 印刷データ = tbl.Copy();
                印刷データ.TableName = "得意先・商品別売上統計表";

                FwRepPreview.ReportPreview view = new FwRepPreview.ReportPreview();
                view.MakeReport(印刷データ.TableName, ReportFileName, 0, 0, 0);
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
                appLog.Error("得意先・商品別売上統計表の印刷時に例外が発生しました。", ex);
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
            Dictionary<string, DateTime> printParams = getPrintParameter();

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

        #endregion

        #region Window_Closed
        /// <summary>
        /// 画面が閉じられた時、データを保持する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            SearchList = null;

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);

        }
        #endregion

    }

}
