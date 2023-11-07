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
    using KyoeiSystem.Framework.Windows.Controls;
    using FwReportPreview = KyoeiSystem.Framework.Reports.Preview.ReportPreview;
    using FwPreview = KyoeiSystem.Framework.Reports.Preview;
    using WinForms = System.Windows.Forms;

    /// <summary>
    /// 買掛台帳画面クラス
    /// </summary>
    public partial class SHR02011 : WindowReportBase
    {
        #region << 列挙型定義 >>

        /// <summary>
        /// 自社販社区分 内包データ
        /// </summary>
        private enum 自社販社区分 : int
        {
            自社 = 0,
            販社 = 1
        }


        #endregion

        #region 定数定義
        /// <summary>買掛データ集計、出力データ取得処理</summary>
        private const string ACCOUNTS_PAYABLE_PRT = "SHR02011_GetPrintData";

        /// <summary>買掛データ集計、CSVファイル出力データ取得</summary>
        private const string ACCOUNTS_PAYABLE_PRT_CSV = "SHR02011_GetCsvData";

        /// <summary>帳票定義ファイル 格納パス</summary>
        private const string ReportTemplateFileName = @"Files\SHR\SHR02011.rpt";

        #endregion

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;
        CommonConfig ccfg = null;

        /// <summary>
        /// 画面固有設定項目のクラス定義
        /// ※ 必ず public で定義する。
        /// </summary>
        public class ConfigSHR02010 : FormConfigBase
        {

        }
        /// ※ 必ず public で定義する。
        public ConfigSHR02010 frmcfg = null;

        #endregion

        #region バインド用プロパティ
              private int _出力期間 = 1;
        public int 出力期間
        {
            get { return _出力期間; }
            set
            {
                _出力期間 = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region << 初期表示処理 >>

        /// <summary>
        /// 買掛台帳 コンストラクタ
        /// </summary>
        public SHR02011()
        {
            InitializeComponent();
            this.DataContext = this;

        }

        /// <summary>
        /// 画面読み込み後のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigSHR02010)ucfg.GetConfigValue(typeof(ConfigSHR02010));
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));

            if (frmcfg == null)
            {
                frmcfg = new ConfigSHR02010();
                ucfg.SetConfigValue(frmcfg);

            }
            else
            {
                // 表示できるかチェック
                var WidthCHK = WinForms.Screen.PrimaryScreen.Bounds.Width - frmcfg.Left;
                if (WidthCHK > 10)
                    this.Left = frmcfg.Left;

                // 表示できるかチェック
                var HeightCHK = WinForms.Screen.PrimaryScreen.Bounds.Height - frmcfg.Top;
                if (HeightCHK > 10)
                    this.Top = frmcfg.Top;

                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;

            }
            #endregion

            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { null, typeof(SCHM01_TOK) });
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { null, typeof(SCHM70_JIS) });


            // 初期値設定
            myCompany.Text1 = ccfg.自社コード.ToString();
            myCompany.IsEnabled = ccfg.自社販社区分 == 自社販社区分.自社.GetHashCode();

            CreateYearMonth.Text = DateTime.Now.ToString("yyyy/MM");

            SetFocusToTopControl();
            ErrorMessage = string.Empty;

        }

        #endregion

        #region データ受信メソッド
        /// <summary>
        /// データ受信メソッド
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            try
            {
                base.SetFreeForInput();

                var data = message.GetResultData();

                if (data is DataTable)
                {
                    DataTable tbl = data as DataTable;

                    switch (message.GetMessageName())
                    {
                        case ACCOUNTS_PAYABLE_PRT:
                            DispPreviw(tbl);
                            break;

                        case ACCOUNTS_PAYABLE_PRT_CSV:
                            OutPutCSV(tbl);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }

        }

        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            this.ErrorMessage = (string)message.GetResultData();
        }

        #endregion

        #region リボン

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
                var m01Text = ViewBaseCommon.FindVisualParent<M01_TOK_TextBox>(ctl as UIElement);

                if (m01Text == null)
                {
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                }
                else
                {
                    m01Text.OpenSearchWindow(this);

                }

            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }

        }


        /// <summary>
        /// F5　リボン　CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }

            if (!CheckFormValid())
            {
                return;
            }

            if (!CheckInputErr())
            {
                MessageBox.Show("入力エラーがあります。");
                return;
            }

            Dictionary<string, string> paramDic = createParamDic();

            base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestDataWithBusy,
                        ACCOUNTS_PAYABLE_PRT_CSV,
                        new object[]{
                            paramDic
                        }));

            base.SetBusyForInput();
        }

        /// <summary>
        /// F8　リボン　印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            if (F8.IsEnabled == false) return;

            PrinterDriver ret = AppCommon.GetPrinter(frmcfg.PrinterName);
            if (ret.Result == false)
            {
                this.ErrorMessage = "プリンタドライバーがインストールされていません！";
                return;
            }
            frmcfg.PrinterName = ret.PrinterName;

            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }

            if (!CheckFormValid())
            {
                return;
            }

            if (!CheckInputErr())
            {
                MessageBox.Show("入力エラーがあります。");
                return;
            }

            Dictionary<string, string> paramDic = createParamDic();

            base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestDataWithBusy,
                        ACCOUNTS_PAYABLE_PRT,
                        new object[]{
                            paramDic
                        }));

            base.SetBusyForInput();

        }

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

        #region プレビュー画面
        /// <summary>
        /// プレビュー画面表示
        /// </summary>
        /// <param name="tbl"></param>
        private void DispPreviw(DataTable tbl)
        {
            try
            {
                if (tbl.Rows.Count < 1)
                {
                    this.ErrorMessage = "対象データが存在しません。";
                    return;
                }

                // 印刷処理
                FwReportPreview view = new FwReportPreview();

                // 印字用にパラメータを編集
                int yearMonth = int.Parse(CreateYearMonth.Text.Replace("/", ""));
                int year = yearMonth / 100;
                int month = yearMonth % 100;

                var parms = new List<FwPreview.ReportParameter>()
                {
                    new FwPreview.ReportParameter(){ PNAME="自社名", VALUE=(this.myCompany.Text2)},
                    new FwPreview.ReportParameter(){ PNAME="支払年", VALUE=(year)},
                    new FwPreview.ReportParameter(){ PNAME="支払月", VALUE=(month)},
                    new FwPreview.ReportParameter(){ PNAME="得意先名", VALUE=(this.Customer.Label2Text)},
                };

                // 第1引数　帳票タイトル
                // 第2引数　帳票ファイルPass
                // 第3以上　帳票の開始点(0で良い)
                view.MakeReport("買掛台帳", ReportTemplateFileName, 0, 0, 0);
                // 帳票ファイルに送るデータ。
                // 帳票データの列と同じ列名を保持したDataTableを引数とする
                view.SetReportData(tbl);
                view.PrinterName = frmcfg.PrinterName;
                view.SetupParmeters(parms);
                view.ShowPreview();
                view.Close();
                frmcfg.PrinterName = view.PrinterName;

                // 印刷した場合
                if (view.IsPrinted)
                {
                    // 印刷した場合はtrueを返す
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region CSVファイル出力
        /// <summary>
        /// CSVファイル出力
        /// </summary>
        /// <param name="tbl"></param>
        private void OutPutCSV(DataTable tbl)
        {
            if (tbl.Rows.Count < 1)
            {
                this.ErrorMessage = "対象データが存在しません。";
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
                // カラム名を変更
                tbl.Columns["通常税率消費税"].ColumnName = "消費税(通常)";
                tbl.Columns["軽減税率消費税"].ColumnName = "消費税(軽減)";
                tbl.Columns["得意先名称"].ColumnName = "支払先名";

                // フォーマットした日付を出力
                tbl.Columns.Remove("日付");
                tbl.Columns["s日付"].ColumnName = "日付";

                // CSVファイル出力
                CSVData.SaveCSV(tbl, sfd.FileName, true, true, false, ',');
                MessageBox.Show("CSVファイルの出力が完了しました。");
            }

        }
        #endregion

        #region Mindoow_Closed
        /// <summary>
        /// 画面が閉じられた時、データを保持する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;

            ucfg.SetConfigValue(frmcfg);

        }
        #endregion

        #region 業務バリデーションチェックをおこなう
        /// <summary>
        /// 業務バリデーションチェックをおこなう
        /// </summary>
        /// <returns></returns>
        private bool CheckFormValid()
        {
            // 自社コードの必須入力チェック
            if (string.IsNullOrEmpty(myCompany.Text1))
            {
                myCompany.SetFocus();

                ErrorMessage = "自社コードが入力されていません。";
                return false;
            }

            // 作成年月の必須入力チェック
            if (string.IsNullOrEmpty(CreateYearMonth.Text))
            {
                CreateYearMonth.Focus();
                ErrorMessage = "作成年月が入力されていません。";
                return false;
            }
            return true;
        }
        #endregion

        #region 入力チェック
        /// <summary>
        /// 入力チェック
        /// </summary>
        /// <returns>true:OK　false:NG</returns>
        private bool CheckInputErr()
        {
            int p会社コード;
            if (!int.TryParse(myCompany.Text1, out p会社コード))
            {
                myCompany.SetFocus();
                ErrorMessage = "自社コードが設定されていません。";
                return false;
            }

            DateTime p作成年月日;
            if (!DateTime.TryParse(CreateYearMonth.Text, out p作成年月日))
            {
                CreateYearMonth.Focus();
                ErrorMessage = "作成年月の内容が正しくありません。";
                return false;
            }

            if (出力期間 == 2 && string.IsNullOrEmpty(Customer.Text1))
            {
                Customer.Focus();
                ErrorMessage = "支払先締期間で出力する場合は支払先をしていしてください。";
                return false;
            }

            return true;
        }
        #endregion

        #region 検索条件を作成して返す
        /// <summary>
        /// 検索条件を作成して返す
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> createParamDic()
        {
            Dictionary<string, string> paramDic = new Dictionary<string, string>();

            paramDic.Add("自社コード", myCompany.Text1);
            paramDic.Add("作成年月", CreateYearMonth.Text);
            paramDic.Add("得意先コード", Customer.Text1 == null ? null : Customer.Text1);
            paramDic.Add("得意先枝番", Customer.Text2 == null ? null : Customer.Text2);
            paramDic.Add("userId", ccfg.ユーザID.ToString());
            paramDic.Add("出力期間", 出力期間.ToString());

            return paramDic;

        }
        #endregion

    }

}
