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
    /// 車種統計表
    /// </summary>
    public partial class SRY13010 : WindowReportBase
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
        public class ConfigSRY13010 : FormConfigBase
        {
            public int? 作成年 { get; set; }
            public int? 作成月 { get; set; }
            public int? 終了作成年 { get; set; }
            public int? 終了作成月 { get; set; }
            public int? 締日 { get; set; }
            public int 区分1 { get; set; }
            public int 区分2 { get; set; }
            public int 区分3 { get; set; }
            public int 区分4 { get; set; }
            public int 区分5 { get; set; }
            public bool? チェック { get; set; }
        }
        /// ※ 必ず public で定義する。
        public ConfigSRY13010 frmcfg = null;

        #endregion

        #region 定数定義
        

        // データアクセス定義名
        private const string SEARCH_SRY13010 = "SEARCH_SRY13010";
        // データアクセス定義名
        private const string SEARCH_SRY13010_CSV = "SEARCH_SRY13010_CSV";
        // 車種統計表　レポート定数
        private const string rptFullPathName_PIC = @"Files\SRY\SRY13010.rpt";

        #endregion

        #region バインド用プロパティ

        // データバインド用変数
        private string _車種ピックアップ = string.Empty;
        public string 車種ピックアップ
        {
            set { _車種ピックアップ = value; NotifyPropertyChanged(); }
            get { return _車種ピックアップ; }
        }

        private string _車種From = string.Empty;
        public string 車種From
        {
            set { _車種From = value; NotifyPropertyChanged(); }
            get { return _車種From; }
        }

        private string _車種To = string.Empty;
        public string 車種To
        {
            set
            { _車種To = value; NotifyPropertyChanged(); }
            get { return _車種To; }
        }

        private DateTime? _集計期間From = null;
        public DateTime? 集計期間From
        {
            set { _集計期間From = value; NotifyPropertyChanged(); }
            get { return _集計期間From; }
        }

        private DateTime? _集計期間To = null;
        public DateTime? 集計期間To
        {
            set { _集計期間To = value; NotifyPropertyChanged(); }
            get { return _集計期間To; }
        }

        private int _表示順序 = 0;
        public int 表示順序
        {
            set { _表示順序 = value; NotifyPropertyChanged(); }
            get { return _表示順序; }
        }

        private int? _開始年;
        public int? 開始年
        {
            set { _開始年 = value; NotifyPropertyChanged(); }
            get { return _開始年; }
        }

        private int? _開始月;
        public int? 開始月
        {
            set { _開始月 = value; NotifyPropertyChanged(); }
            get { return _開始月; }
        }

        private int? _終了年;
        public int? 終了年
        {
            set { _終了年 = value; NotifyPropertyChanged(); }
            get { return _終了年; }
        }

        private int? _終了月;
        public int? 終了月
        {
            set { _終了月 = value; NotifyPropertyChanged(); }
            get { return _終了月; }
        }

        private int? _作成締日 = 31;
        public int? 作成締日
        {
            set { _作成締日 = value; NotifyPropertyChanged(); }
            get { return _作成締日; }
        }

        private int _作成区分;
        public int 作成区分
        {
            set { _作成区分 = value; NotifyPropertyChanged(); }
            get { return _作成区分; }
        }

        #endregion

        #region SRY13010

        /// <summary>
        /// 車種統計表
        /// </summary>
        public SRY13010()
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
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigSRY13010)ucfg.GetConfigValue(typeof(ConfigSRY13010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigSRY13010();
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
                this.Width = frmcfg.Width;
                this.Height = frmcfg.Height;
                this.開始年 = frmcfg.作成年;
                this.開始月 = frmcfg.作成月;
                this.終了年 = frmcfg.終了作成年;
                this.終了月 = frmcfg.終了作成月;
            }
            #endregion

            //F1 車種 
            base.MasterMaintenanceWindowList.Add("M06_SYA", new List<Type> { null, typeof(SCH05010) });
			SetFocusToTopControl();
        }

        #endregion

        #region エラーメッセージ

        /// <summary>
        /// データアクセスエラー受信イベント
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            // 基底クラスのエラー受信イベントを呼び出します。
            base.OnReveivedError(message);
            // 個別にエラー処理が必要な場合、ここに記述してください。

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
                    //締日プレビュー出力用
                    case SEARCH_SRY13010:
                        DispPreviw(tbl);
                        break;

                    //締日CSV出力用
                    case SEARCH_SRY13010_CSV:
                        OutPutCSV(tbl);
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
        /// F1 リボン　検索
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
                    SCH05010 srch = new SCH05010();
                    switch (uctext.DataAccessName)
                    {
                        case "M06_SYA":
                            srch.MultiSelect = false;
                            break;
                        default:
                            srch.MultiSelect = true;
                            break;
                    }
                    Framework.Windows.Controls.UcLabelTwinTextBox dmy = new Framework.Windows.Controls.UcLabelTwinTextBox();
                    srch.TwinTextBox = dmy;
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
            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }

            //作成年月
            if (開始年 == null || 開始月 == null || 終了年 == null || 終了月 == null)
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                MessageBox.Show("作成年月は入力必須項目です。");
                return;
            }

            DateTime d開始年月日;
            DateTime d終了年月日;
            if (開始月.ToString().Length == 1)
            {
                d開始年月日 = Convert.ToDateTime(開始年.ToString() + "/0" + 開始月.ToString() + "/01");
            }
            else
            {
                d開始年月日 = Convert.ToDateTime(開始年.ToString() + "/" + 開始月.ToString() + "/01");
            }
            if (終了月.ToString().Length == 1)
            {
                d終了年月日 = Convert.ToDateTime(終了年.ToString() + "/0" + 終了月.ToString() + "/01");
            }
            else
            {
                d終了年月日 = Convert.ToDateTime(終了年.ToString() + "/" + 終了月.ToString() + "/01");
            }



            //車種リスト作成
            int?[] i車種List = new int?[0];
            if (!string.IsNullOrEmpty(車種ピックアップ))
            {
                string[] 車種List = 車種ピックアップ.Split(',');
                i車種List = new int?[車種List.Length];

                for (int i = 0; i < 車種List.Length; i++)
                {
                    string str = 車種List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "支払先指定の形式が不正です。";
                        return;
                    }
                    i車種List[i] = code;
                }
            }

            //帳票出力用
			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SRY13010_CSV, new object[] { 車種From , 車種To , i車種List , 車種ピックアップ,
                                                                                                             d開始年月日, d終了年月日
                                                                                                            }));
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


            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }

            //作成年月
            if (開始年 == null || 開始月 == null || 終了年 == null || 終了月 == null)
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                MessageBox.Show("作成年月は入力必須項目です。");
                return;
            }

            DateTime d開始年月日;
            DateTime d終了年月日;
            if (開始月.ToString().Length == 1)
            {
                d開始年月日 = Convert.ToDateTime(開始年.ToString() + "/0" + 開始月.ToString() + "/01");
            }
            else
            {
                d開始年月日 = Convert.ToDateTime(開始年.ToString() + "/" + 開始月.ToString() + "/01");
            }
            if (終了月.ToString().Length == 1)
            {
                d終了年月日 = Convert.ToDateTime(終了年.ToString() + "/0" + 終了月.ToString() + "/01");
            }
            else
            {
                d終了年月日 = Convert.ToDateTime(終了年.ToString() + "/" + 終了月.ToString() + "/01");
            }



            //車種リスト作成
            int?[] i車種List = new int?[0];
            if (!string.IsNullOrEmpty(車種ピックアップ))
            {
                string[] 車種List = 車種ピックアップ.Split(',');
                i車種List = new int?[車種List.Length];

                for (int i = 0; i < 車種List.Length; i++)
                {
                    string str = 車種List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "支払先指定の形式が不正です。";
                        return;
                    }
                    i車種List[i] = code;
                }
            }


            //帳票出力用
			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SRY13010, new object[] { 車種From , 車種To , i車種List , 車種ピックアップ,
                                                                                                             d開始年月日, d終了年月日
                                                                                                            }));
        }

        /// <summary>
        /// F11 終了
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
                //印刷処理
                KyoeiSystem.Framework.Reports.Preview.ReportPreview view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
                //第1引数　帳票タイトル
                //第2引数　帳票ファイルPass
                //第3以上　帳票の開始点(0で良い)
                view.MakeReport("車種統計表", rptFullPathName_PIC, 0, 0, 0);
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

        #region 日付処理

        //作成年がNullの場合は今年の年を挿入
        private void Lost_Kaishi_Year(object sender, RoutedEventArgs e)
        {
            if (開始年 == null)
            {
                string Date;
                int iDate;
                DateTime YYYY;
                YYYY = DateTime.Today;
                Date = Convert.ToString(YYYY);
                Date = Date.Substring(0, 4);
                iDate = Convert.ToInt32(Date);
                開始年 = iDate;
            }

        }

        //作成月がNullの場合は今月の月を挿入
        private void Lost_Kaishi_Month(object sender, RoutedEventArgs e)
        {
            if (開始月 == null)
            {
                string Date;
                int iDate;
                DateTime MM;
                MM = DateTime.Today;
                Date = Convert.ToString(MM);
                Date = Date.Substring(5, 2);
                iDate = Convert.ToInt32(Date);
                開始月 = iDate;
            }
        }

        //作成年がNullの場合は今年の年を挿入
        private void Lost_Syuryo_Year(object sender, RoutedEventArgs e)
        {
            if (終了年 == null)
            {
                string Date;
                int iDate;
                DateTime YYYY;
                YYYY = DateTime.Today;
                Date = Convert.ToString(YYYY);
                Date = Date.Substring(0, 4);
                iDate = Convert.ToInt32(Date);
                終了年 = iDate;
            }

        }

        #endregion

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            frmcfg.作成年 = this.開始年;
            frmcfg.作成月 = this.開始月;
            frmcfg.終了作成年 = this.終了年;
            frmcfg.終了作成月 = this.終了月;
            ucfg.SetConfigValue(frmcfg);
        }

        private void F8(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

				var yesno = MessageBox.Show("プレビューを表示しますか？", "確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
				if (yesno == MessageBoxResult.No)
				{
					return;
				}
                if (終了月 == null)
                {
                    string Date;
                    int iDate;
                    DateTime MM;
                    MM = DateTime.Today;
                    Date = Convert.ToString(MM);
                    Date = Date.Substring(5, 2);
                    iDate = Convert.ToInt32(Date);
                    終了月 = iDate;
                }
                OnF8Key(sender, null);
            }
        }
    }
}
