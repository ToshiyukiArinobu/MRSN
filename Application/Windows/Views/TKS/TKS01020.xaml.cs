using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.Controls;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace KyoeiSystem.Application.Windows.Views
{
    using FwPreview = KyoeiSystem.Framework.Reports.Preview;

    /// <summary>
    /// 請求書発行画面
    /// </summary>
    public partial class TKS01020 : WindowReportBase
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
        public class ConfigTKS01020 : FormConfigBase
        {
            public byte[] spConfig = null;
        }

        /// ※ 必ず public で定義する。
        public ConfigTKS01020 frmcfg = null;
        CommonConfig ccfg = null;
        // SPREAD初期状態保存用
        public byte[] spConfig = null;

        #endregion

        #region << 列挙型定義 >>

        /// <summary>
        /// データグリッドの列定義
        /// </summary>
        private enum GridColumnsMapping : int
        {
            印刷区分 = 0,
            得意先コード = 1,
            得意先枝番 = 2,
            得意先名 = 3,
            回数 = 4,
            集計期間 = 5,
            当月請求額 = 6,
            郵便番号 = 7,
            住所１ = 8,
            住所２ = 9,
            電話番号 = 10
        }

        /// <summary>
        /// 自社販社区分 内包データ
        /// </summary>
        private enum 自社販社区分 : int
        {
            自社 = 0,
            販社 = 1
        }

        #endregion

        #region << 定数定義 >>

        private const string TKS01020_SEARCHLIST = "TKS01020_GetDataList";
        private const string TKS01020_GETPRINGDATA = "TKS01020_GetPrintData";

        /// <summary>納品書 帳票定義パス</summary>
        private const string REPORT_FILE_PATH = @"Files\TKS\TKS01020.rpt";

        /// <summary>帳票の(１枚あたり)最大出力行数</summary>
        private const int MAX_PRINT_ROW_COUNT = 50;

        #endregion

        #region << バインディングデータ >>

        /// <summary>
        /// 検索条件
        /// </summary>
        private Dictionary<string, string> condition = new Dictionary<string, string>();

        /// <summary>
        /// グリッドバインドデータ
        /// </summary>
        private DataTable _請求書一覧データ = null;
        public DataTable 請求書一覧データ
        {
            get { return this._請求書一覧データ; }
            set
            {
                this._請求書一覧データ = value;
                NotifyPropertyChanged();
            }
        }

        // TODO:Validateionエラー回避用
        public int 締日 { get; set; }

        #endregion

        #region プリンタ名

        private string _selectedPrinterName = string.Empty;
        public string SelectedPrinterName
        {
            get { return _selectedPrinterName; }
            set
            {
                _selectedPrinterName = value;
                NotifyPropertyChanged();
                if (this.frmcfg == null)
                {
                    this.frmcfg = new ConfigTKS01020();
                }
                this.frmcfg.PrinterName = value;
            }
        }
        private List<string> _printerNames;
        public List<string> PrinterNames
        {
            get
            {
                return this._printerNames;
            }
            set
            {
                this._printerNames = value;
                this.NotifyPropertyChanged();
            }
        }
        #endregion

        #region << 画面表示初期処理 >>

        /// <summary>
        /// 請求書発行
        /// </summary>
        public TKS01020()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.sp請求データ一覧.RowCount = 0;
            //this.spConfig = AppCommon.SaveSpConfig(this.sp請求データ一覧);

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            //frmcfg = (ConfigTKS01020)ucfg.GetConfigValue(typeof(ConfigTKS01020));
            if (frmcfg == null)
            {
                frmcfg = new ConfigTKS01020();
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
            if (frmcfg.spConfig != null)
            {
                AppCommon.LoadSpConfig(this.sp請求データ一覧, frmcfg.spConfig);
            }

            // 権限設定を呼び出す(ucfgを取得した後のに入れる)
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            // 登録ボタン設定
            if (!権限Get.Authority_Update_Button(ccfg, this.GetType().Name))
            {
                DataUpdateVisible = System.Windows.Visibility.Hidden;
            }

            this.PrinterNames = new List<string>();
            foreach (string pnm in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                this.PrinterNames.Add(pnm);
            }
            this.SelectedPrinterName = frmcfg.PrinterName;
            #endregion

            //this.sp請求データ一覧.PreviewKeyDown += sp請求データ一覧_PreviewKeyDown;

            // コントロール初期値の設定
            setDefaultControlValue();

            this.MyCompany.IsEnabled = (ccfg.自社販社区分 == 自社販社区分.自社.GetHashCode());

            ScreenClear();

        }

        #endregion

        #region << 画面表示初期化 >>

        /// <summary>
        /// コントロールに初期値の設定をおこなう
        /// </summary>
        private void setDefaultControlValue()
        {
            DateTime beforeDate = DateTime.Now.AddMonths(-1);

            this.MyCompany.Text1 = ccfg.自社コード.ToString();
            this.PrintDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.ClosingDate.Text = string.Empty;
            this.CreateYearMonth.Text = DateTime.Now.ToString("yyyy/MM");

        }

        /// <summary>
        /// 画面表示内容の初期化をおこなう
        /// </summary>
        private void ScreenClear()
        {
            this.MaintenanceMode = null;
            this.sp請求データ一覧.RowCount = 0;
            ResetAllValidation();
            SetFocusToTopControl();
        }

        #endregion

        #region << データ受信処理 >>

        /// <summary>
        /// 取得データの取り込み
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            try
            {
                base.ErrorMessage = string.Empty;

                var data = message.GetResultData();

                switch (message.GetMessageName())
                {
                    case TKS01020_SEARCHLIST:
                        DataTable dt = data as DataTable;
                        if (dt == null)
                        {
                            this.請求書一覧データ = null;
                            base.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
                            break;
                        }
                        else
                        {
                            this.請求書一覧データ = dt;
                            if (this.請求書一覧データ.Rows.Count == 0)
                                base.ErrorMessage = "指定された条件の請求データはありません。";

                        }
                        break;

                    case TKS01020_GETPRINGDATA:
                        DataSet ds = data as DataSet;

                        if (ds.Tables.Count < 1)
                        {
                            base.ErrorMessage = "対象データが存在しません。";
                            return;
                        }

                        PrintPreview(ds);
                        break;

                }

            }
            catch (Exception ex)
            {
                base.ErrorMessage = ex.Message;
            }

        }

        /// <summary>
        /// サービスでエラーが発生した場合の処理
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            base.ErrorMessage = (string)message.GetResultData();
        }

        #endregion

        #region << リボン >>

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
                var tokText = ViewBaseCommon.FindVisualParent<M01_TOK_TextBox>(ctl as UIElement);

                if (tokText == null)
                {
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                }
                else
                {
                    tokText.OpenSearchWindow(this);

                }

            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }

        }

        /// <summary>
        /// F8　リボン　印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            PrinterDriver ret = AppCommon.GetPrinter(frmcfg.PrinterName);
            if (ret.Result == false)
            {
                base.ErrorMessage = "プリンタドライバーがインストールされていません！";
                return;
            }
            frmcfg.PrinterName = ret.PrinterName;

            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }

            if (this.請求書一覧データ == null)
            {
                base.ErrorMessage = "印刷データを取得していません。";
                return;
            }

            int cnt = 0;
            foreach (DataRow rec in this.請求書一覧データ.Rows)
            {
                if (rec.IsNull("印刷区分") != true && (bool)rec["印刷区分"] == true)
                    cnt++;
            }
            if (cnt == 0)
            {
                base.ErrorMessage = "印刷対象が選択されていません。";
                return;
            }

            _請求書一覧データ.AcceptChanges();
            DataSet ds = new DataSet();
            ds.Tables.Add(_請求書一覧データ.Copy());

            // REMARKS:条件は検索実行時に使用したものを使う
            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    TKS01020_GETPRINGDATA,
                    condition,
                    ds));

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

        #region << コントロールイベント処理 >>

        /// <summary>
        /// 検索ボタンが押下された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!this.CheckAllValidation())
                {
                    MessageBox.Show("入力エラーがあります。", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!checkFormValidation())
                {
                    MessageBox.Show(base.ErrorMessage, "入力エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // 検索条件を設定
                setConditionItems();

                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestDataWithBusy,
                        TKS01020_SEARCHLIST,
                        condition));

            }
            catch (Exception)
            {
                return;
            }

        }

        /// <summary>
        /// 全チェック変更ボタン押下時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PrintAll_Click(object sender, RoutedEventArgs e)
        {
            if (this.請求書一覧データ == null)
                return;

            Button btn = sender as Button;
            foreach (DataRow row in this.請求書一覧データ.Rows)
                row["印刷区分"] = (btn.Name == btnAllOn.Name);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColumnReset_Click(object sender, RoutedEventArgs e)
        {
            AppCommon.LoadSpConfig(this.sp請求データ一覧, this.spConfig);
            ScreenClear();

        }

        #region Window_Closed
        //画面が閉じられた時、データを保持する
        private void Window_Closed(object sender, EventArgs e)
        {
            請求書一覧データ = null;
            sp請求データ一覧.InputBindings.Clear();

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            this.請求書一覧データ = null;
            frmcfg.spConfig = AppCommon.SaveSpConfig(this.sp請求データ一覧);
            ucfg.SetConfigValue(frmcfg);

        }
        #endregion

        #endregion

        #region << 機能処理関連 >>

        /// <summary>
        /// 検索時入力チェック処理
        /// </summary>
        /// <returns></returns>
        private bool checkFormValidation()
        {
            if (string.IsNullOrEmpty(MyCompany.Text1))
            {
                MyCompany.Focus();
                base.ErrorMessage = "自社コードは必須入力項目です。";
                return false;
            }

            if (string.IsNullOrEmpty(CreateYearMonth.Text))
            {
                CreateYearMonth.Focus();
                base.ErrorMessage = "作成年月は必須入力項目です。";
                return false;
            }

            if (string.IsNullOrEmpty(ClosingDate.Text))
            {
                ClosingDate.Focus();
                base.ErrorMessage = "作成締日は必須入力項目です。";
                return false;
            }

            return true;

        }

        /// <summary>
        /// 検索条件をディクショナリに設定する
        /// </summary>
        private void setConditionItems()
        {
            condition.Clear();

            condition.Add("自社コード", this.MyCompany.Text1);
            condition.Add("作成年月日", this.PrintDate.Text);
            condition.Add("作成年月", this.CreateYearMonth.Text);
            condition.Add("作成締日", this.ClosingDate.Text);
            condition.Add("得意先コード", this.Customer.Text1);
            condition.Add("得意先枝番", this.Customer.Text2);

        }

        /// <summary>
        /// 帳票出力処理
        /// </summary>
        /// <param name="ds"></param>
        private void PrintPreview(DataSet ds)
        {
            // 印刷処理
            FwPreview.ReportPreview view = new FwPreview.ReportPreview();
            view.PrinterName = frmcfg.PrinterName;
            // 第1引数　帳票タイトル
            // 第2引数　帳票ファイルPass
            // 第3以上　帳票の開始点(0で良い)
            view.MakeReport("納品書", REPORT_FILE_PATH);

            var parms = new List<FwPreview.ReportParameter>()
                {
                    new FwPreview.ReportParameter(){ PNAME="出力日付", VALUE=(this.PrintDate.Text)},
                    new FwPreview.ReportParameter(){ PNAME="行数１", VALUE=(MAX_PRINT_ROW_COUNT)},// ページあたり行数
                    new FwPreview.ReportParameter(){ PNAME="最大行数", VALUE=(MAX_PRINT_ROW_COUNT)},// ページあたり行数
                    //new FwPreview.ReportParameter(){ PNAME="行数２", VALUE=(ds.Tables[0].Rows.Count)},
                };

            // 帳票ファイルに送るデータ。
            // 帳票データの列と同じ列名を保持したDataSetを引数とする
            view.SetReportData(ds);
            view.PrinterName = frmcfg.PrinterName;
            view.SetupParmeters(parms);
            view.ShowPreview();
            view.Close();
            frmcfg.PrinterName = view.PrinterName;

        }

        #endregion

    }

}
