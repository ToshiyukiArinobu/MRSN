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
    /// 相殺請求管理表 フォームクラス
    /// </summary>
    public partial class BSK09010 : WindowReportBase
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
        public class ConfigBSK09010 : FormConfigBase
        {
            public byte[] spConfig = null;
        }

        /// ※ 必ず public で定義する。
        public ConfigBSK09010 frmcfg = null;
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

        private const string GET_PRINT_LIST = "BSK09010_GetPrintList";
        private const string GET_CSV_LIST = "BSK09010_GetCsvList";

        /// <summary>帳票定義体ファイルパス</summary>
        private const string ReportFileName = @"Files\BSK\BSK09010.rpt";

        // 画面パラメータ名
        private const string PARAMS_NAME_TARGET_YEAR_MONTH = "対象年月";

        // 帳票パラメータ名
        private const string REPORT_PARAM_NAME_YEAR_MONTH = "対象年月";

        #endregion

        #region << クラス変数定義 >>

        /// <summary>検索時パラメータ保持用</summary>
        Dictionary<string, string> paramDic = new Dictionary<string, string>();

        #endregion


        #region << 画面初期処理 >>

        /// <summary>
        /// 相殺請求管理表 コンストラクタ
        /// </summary>
        public BSK09010()
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
            frmcfg = (ConfigBSK09010)ucfg.GetConfigValue(typeof(ConfigBSK09010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigBSK09010();
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

            base.MasterMaintenanceWindowList.Add("M15_SERIES", new List<Type> { typeof(MST04021), typeof(SCHM15_SERIES) });
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

            // 対象年月の初期値設定
            this.YearMonth.Text = DateTime.Now.ToString("yyyy/MM");

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
            if (string.IsNullOrEmpty(YearMonth.Text))
            {
                YearMonth.Focus();
                ErrorMessage = "対象年月が入力されていません。";
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

            paramDic.Add(PARAMS_NAME_TARGET_YEAR_MONTH, YearMonth.Text);

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
            DateTime pDate = Convert.ToDateTime(paramDic[PARAMS_NAME_TARGET_YEAR_MONTH]);

            Dictionary<string, DateTime> printDic = new Dictionary<string, DateTime>();
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH, pDate);

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
                    // 印字パラメータ設定
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH]},
                };

                DataTable 印刷データ = tbl.Copy();
                印刷データ.TableName = "相殺請求管理表";

                FwRepPreview.ReportPreview view = new FwRepPreview.ReportPreview();
                view.MakeReport(印刷データ.TableName, ReportFileName, 0, 0, 0);
                view.SetReportData(印刷データ);

                view.SetupParmeters(parms);

                base.SetFreeForInput();

                view.PrinterName = frmcfg.PrinterName;
                view.IsCustomMode = true;
                view.ShowPreview();
                view.Close();
                frmcfg.PrinterName = view.PrinterName;

            }
            catch (Exception ex)
            {
                base.SetFreeForInput();
                this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
                appLog.Error("相殺請求管理表の印刷時に例外が発生しました。", ex);
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
