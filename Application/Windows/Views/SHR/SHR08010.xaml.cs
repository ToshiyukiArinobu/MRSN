using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;


using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using KyoeiSystem.Framework.Windows.Controls;

namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 支払一覧表画面
    /// </summary>
    public partial class SHR08010 : WindowReportBase
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
        public class ConfigSHR08010 : FormConfigBase
        {
            public string 作成年 { get; set; }
            public string 作成月 { get; set; }
            public DateTime? 集計期間From { get; set; }
            public DateTime? 集計期間To { get; set; }
        }
        /// ※ 必ず public で定義する。
        public ConfigSHR08010 frmcfg = null;

        #endregion

        #region 定数定義
        //締日データ　帳票出力
        private const string SEARCH_SHR08010 = "SEARCH_SHR08010";
        //締日データ　CSV出力
        private const string SEARCH_SHR08010_CSV = "SEARCH_SHR08010_CSV";
        
        //月次データ　帳票出力
        private const string SEARCH_SHR08010g = "SEARCH_SHR08010g";
        //月次データ　CSV出力
        private const string SEARCH_SHR08010g_CSV = "SEARCH_SHR08010g_CSV";
        
        //レポート名
        private const string rptFullPathName_PIC = @"Files\SHR\SHR08010.rpt";

        #endregion

        #region バインド用プロパティ

        private string _支払先ピックアップ = string.Empty;
        public string 支払先ピックアップ
        {
            set { _支払先ピックアップ = value; NotifyPropertyChanged(); }
            get { return _支払先ピックアップ; }
        }
        private string _支払先From = string.Empty;
        public string 支払先From
        {
            set { _支払先From = value; NotifyPropertyChanged(); }
            get { return _支払先From; }
        }
        private string _支払先To = string.Empty;
        public string 支払先To
        {
            set { _支払先To = value; NotifyPropertyChanged(); }
            get { return _支払先To; }
        }
                
        private string _作成年 = string.Empty;
        public string 作成年
        {
            set { _作成年 = value; NotifyPropertyChanged(); }
            get { return _作成年; }
        }

        private string _作成月 = string.Empty;
        public string 作成月
        {
            set { _作成月 = value; NotifyPropertyChanged(); }
            get { return _作成月; }
        }

        private string _作成締日 = string.Empty;
        public string 作成締日
        {
            set { _作成締日 = value; NotifyPropertyChanged(); }
            get { return _作成締日; }
        }

        private bool _全締日集計 = true;
        public bool 全締日集計
        {
            set { _全締日集計 = value; NotifyPropertyChanged(); }
            get { return _全締日集計; }
        }

        private int _集計区分;
        public int 集計区分
        {
            set { this._集計区分 = value; NotifyPropertyChanged(); }
            get { return this._集計区分; }
        }

        private int _表示区分;
        public int 表示区分
        {
            set { this._表示区分 = value; NotifyPropertyChanged(); }
            get { return this._表示区分; }
        }

        private int _表示順序;
        public int 表示順序
        {
            set { _表示順序 = value; NotifyPropertyChanged(); }
            get { return _表示順序; }
        }

        private int _取引区分 = 4;
        public int 取引区分
        {
            get { return this._取引区分; }
            set { this._取引区分 = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region SHR08010
        
        /// <summary>
        /// 支払一覧表画面
        /// </summary>
        public SHR08010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #endregion

        #region Load

        /// <summary>
        /// 画面読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 画面が表示される最後の段階で処理すべき内容があれば、ここに記述します。
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigSHR08010)ucfg.GetConfigValue(typeof(ConfigSHR08010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigSHR08010();
                ucfg.SetConfigValue(frmcfg);
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
                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;
                this.作成年 = frmcfg.作成年;
                this.作成月 = frmcfg.作成月;
            }
            #endregion

            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { null, typeof(SCH01010) });
            AppCommon.SetutpComboboxList(this.集計区分_Cmb, false);
            AppCommon.SetutpComboboxList(this.表示区分_Cmb, false);
            AppCommon.SetutpComboboxList(this.表示順序_Cmb, false);
			SetFocusToTopControl();
        }

        #endregion

        #region エラー表示
        /// <summary>
        /// データアクセスエラー受信イベント
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            MessageBox.Show(ErrorMessage);
        }
        #endregion

        #region データ受信メソッド

        /// <summary>
        /// 取得データの正常受信時のイベント
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            try
            {
                var data = message.GetResultData();
                DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
                switch (message.GetMessageName())
                {
                    //検索結果取得時 締日データ
                    case SEARCH_SHR08010:
                        DispPreviw(tbl);
                        break;

                    case SEARCH_SHR08010_CSV:
                        OutPutCSV(tbl);
                        break;

                    //検索結果取得時 月次データ
                    case SEARCH_SHR08010g:
                        DispPreviw(tbl);
                        break;

                    case SEARCH_SHR08010g_CSV:
                        OutPutCSV(tbl);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region リボン
        /// <summary>
        /// F1 マスタ検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            try
            {
                var ctl = FocusManager.GetFocusedElement(this);
                if (ctl is TextBox)
                {
                    var uctext = ViewBaseCommon.FindVisualParent<UcTextBox>(ctl as UIElement);
                    if (uctext == null)
                    {
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(uctext.DataAccessName))
                    {
                        ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                        return;
                    }
                    SCH01010 srch = new SCH01010();
                    switch (uctext.DataAccessName)
                    {
                        case "M01_TOK":
                            srch.MultiSelect = false;
                            break;
                        default:
                            srch.MultiSelect = true;
                            break;
                    }
                    Framework.Windows.Controls.UcLabelTwinTextBox dmy = new Framework.Windows.Controls.UcLabelTwinTextBox();
                    srch.TwinTextBox = dmy;
                    srch.multi = 1;
                    srch.表示区分 = 2;
                    var ret = srch.ShowDialog(this);
                    if (ret == true)
                    {
                        uctext.Text = srch.SelectedCodeList;
                        FocusControl.SetFocusWithOrder(new TraversalRequest(FocusNavigationDirection.Next));
                    }
                }
            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }
        }


        /// <summary>
        /// F5 CSVファイル出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            //作成年月
            string s作成年月;
            int 集計年月;

            if (string.IsNullOrEmpty(作成年) || string.IsNullOrEmpty(作成月))
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                MessageBox.Show("作成年月は入力必須項目です。");
                return;
            }
            else
            {
                s作成年月 = 作成年 + 作成月;
                集計年月 = Convert.ToInt32(s作成年月);
            }

            if (作成締日 == "" && C締日.IsChecked == false)
            {
                this.ErrorMessage = "作成締日は入力必須項目です。";
                MessageBox.Show("作成締日は入力必須項目です。");
                return;
            }

            if (string.IsNullOrEmpty(作成締日) && 全締日集計 == false || !string.IsNullOrEmpty(作成締日) && 全締日集計 == true)
            {
                this.ErrorMessage = "作成締日の入力形式が不正です。";
                MessageBox.Show("作成締日の入力形式が不正です。");
                SetFocusToTopControl();
                return;
            }

            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }

            //コンボボックスのIndexを取得
            int Cmb_集計区分, Cmb_表示区分, Cmd_表示順序;
            Cmb_集計区分 = 集計区分_Cmb.SelectedIndex;
            Cmb_表示区分 = 表示区分_Cmb.SelectedIndex;
            Cmd_表示順序 = 表示順序_Cmb.SelectedIndex;

            //支払先リスト作成
            int?[] i支払先List = new int?[0];
            if (!string.IsNullOrEmpty(支払先ピックアップ))
            {
                string[] 支払先List = 支払先ピックアップ.Split(',');
                i支払先List = new int?[支払先List.Length];

                for (int i = 0; i < 支払先List.Length; i++)
                {
                    string str = 支払先List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "支払先指定の形式が不正です。";
                        return;
                    }
                    i支払先List[i] = code;
                }
            }

            if (Cmb_集計区分 == 0)
            {
                //締日データ
				base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SHR08010_CSV, new object[] { 支払先From, 支払先To, i支払先List, 作成締日, 全締日集計, 集計年月, Cmb_集計区分, Cmb_表示区分, 作成年, 作成月, Cmd_表示順序 }));
            }
            else
            {
                //月次データ
				base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SHR08010g_CSV, new object[] { 支払先From, 支払先To, i支払先List, 作成締日, 全締日集計, 集計年月, Cmb_集計区分, Cmb_表示区分, 作成年, 作成月, Cmd_表示順序 }));
            }
        }


        /// <summary>
        /// F8 リボン　印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
		{
			PrinterDriver ret = AppCommon.GetPrinter(frmcfg.PrinterName);
			if (ret.Result == false)
			{
				this.ErrorMessage = "プリンタドライバーがインストールされていません！";
				return;
			}
			frmcfg.PrinterName = ret.PrinterName;


            //作成年月
            string s作成年月;
            int 集計年月;

            if (string.IsNullOrEmpty(作成年) || string.IsNullOrEmpty(作成月))
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                MessageBox.Show("作成年月は入力必須項目です。");
                return;
            }
            else
            {
                s作成年月 = 作成年 + 作成月;
                集計年月 = Convert.ToInt32(s作成年月);
            }

            if (string.IsNullOrEmpty(作成締日) && 全締日集計 == false)
            {
                this.ErrorMessage = "作成締日の入力形式が不正です。";
                MessageBox.Show("作成締日の入力形式が不正です。");
                SetFocusToTopControl();
                return;
            }

            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }

            //コンボボックスのIndexを取得
            int Cmb_集計区分, Cmb_表示区分, Cmd_表示順序;
            Cmb_集計区分 = 集計区分_Cmb.SelectedIndex;
            Cmb_表示区分 = 表示区分_Cmb.SelectedIndex;
            Cmd_表示順序 = 表示順序_Cmb.SelectedIndex;

            //支払先リスト作成
            int?[] i支払先List = new int?[0];
            if (!string.IsNullOrEmpty(支払先ピックアップ))
            {
                string[] 支払先List = 支払先ピックアップ.Split(',');
                i支払先List = new int?[支払先List.Length];

                for (int i = 0; i < 支払先List.Length; i++)
                {
                    string str = 支払先List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "支払先指定の形式が不正です。";
                        return;
                    }
                    i支払先List[i] = code;
                }
            }

            if (Cmb_集計区分 == 0)
            {
                //締日データ
				base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SHR08010, new object[] { 支払先From, 支払先To, i支払先List, 作成締日, 全締日集計, 集計年月, Cmb_集計区分, Cmb_表示区分, 作成年, 作成月, Cmd_表示順序 }));
                                                                                                                                           
            }
            else
            {
                //月次データ
				base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SHR08010g, new object[] { 支払先From, 支払先To, i支払先List, 作成締日, 全締日集計, 集計年月, Cmb_集計区分, Cmb_表示区分, 作成年, 作成月, Cmd_表示順序 }));

            }
        }

        //リボン
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
                //印刷処理
                KyoeiSystem.Framework.Reports.Preview.ReportPreview view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
                //第1引数　帳票タイトル
                //第2引数　帳票ファイルPass
                //第3以上　帳票の開始点(0で良い)
                view.MakeReport("支払先一覧表", rptFullPathName_PIC, 0, 0, 0);
                //帳票ファイルに送るデータ。
                //帳票データの列と同じ列名を保持したDataTableを引数とする
				view.SetReportData(tbl);
				view.PrinterName = frmcfg.PrinterName;
				view.ShowPreview();
				view.Close();
				frmcfg.PrinterName = view.PrinterName;

                // 印刷した場合
                if (view.IsPrinted)
                {
                    //印刷した場合はtrueを返す
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

            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            //はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = @"C:\";
            //[ファイルの種類]に表示される選択肢を指定する
            sfd.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
            //「CSVファイル」が選択されているようにする
            sfd.FilterIndex = 1;
            //タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //CSVファイル出力
                CSVData.SaveCSV(tbl, sfd.FileName, true, true, false, ',');
                MessageBox.Show("CSVファイルの出力が完了しました。");
            }
        }
        #endregion

        #region MainWindow_Closed
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            //画面が閉じられた時、データを保持する
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            frmcfg.作成年 = this.作成年;
            frmcfg.作成月 = this.作成月;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion

        #region Enterキー(F8)

        private void F8(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
			{
				var yesno = MessageBox.Show("プレビューを表示しますか？", "確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
				if (yesno == MessageBoxResult.No)
				{
					return;
				}

                OnF8Key(sender, null);
            }
        }

        #endregion

        #region LostFocusで現在年月取得

        //作成年取得
        private void Lost_Year(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(作成年))
            {
                作成年 = DateTime.Today.Year.ToString();
            }
        }

        //作成月取得
        private void Lost_Manth(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(作成月))
            {
                作成月 = DateTime.Today.Month.ToString();
            }
        }

        //作成締日取得
        private void Lost_Shime(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(作成締日))
            {
                C締日.IsChecked = true;
            }
            else
            {
                C締日.IsChecked = false;
            }
        }

        #endregion
    }
}
