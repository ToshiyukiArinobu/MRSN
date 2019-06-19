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
    /// 車輌合計表画面
    /// </summary>
    public partial class SRY09010 : WindowReportBase
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
        public class ConfigSRY09010 : FormConfigBase
        {
            public int? 作成年 { get; set; }
            public int? 作成月 { get; set; }
            public int? 締日 { get; set; }
            public DateTime? 集計期間From { get; set; }
            public DateTime? 集計期間To { get; set; }
            public int 区分1 { get; set; }
            public int 区分2 { get; set; }
            public int 区分3 { get; set; }
            public int 区分4 { get; set; }
            public int 区分5 { get; set; }
            public bool? チェック { get; set; }
        }
        /// ※ 必ず public で定義する。
        public ConfigSRY09010 frmcfg = null;

        #endregion

        #region 定数定義

        // データアクセス定義名
        private const string SEARCH_SRY09010 = "SEARCH_SRY09010";
        // データアクセス定義名
        private const string SEARCH_SRY09010_CSV = "SEARCH_SRY09010_CSV";
        // 車輌管理台帳　レポート定数
        private const string rptFullPathName_PIC = @"Files\SRY\SRY09010.rpt";

        #endregion

        #region バインド用プロパティ

        // データバインド用変数
        private string _車輌ピックアップ = string.Empty;
        public string 車輌ピックアップ
        {
            set { _車輌ピックアップ = value; NotifyPropertyChanged(); }
            get { return _車輌ピックアップ; }
        }

        private string _車輌From = string.Empty;
        public string 車輌From
        {
            set { _車輌From = value; NotifyPropertyChanged(); }
            get { return _車輌From; }
        }

        private string _車輌To = string.Empty;
        public string 車輌To
        {
            set
            { _車輌To = value; NotifyPropertyChanged(); }
            get { return _車輌To; }
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

        private int? _作成年 = null;
        public int? 作成年
        {
            set { _作成年 = value; NotifyPropertyChanged(); }
            get { return _作成年; }
        }

        private int? _作成月 = null;
        public int? 作成月
        {
            set { _作成月 = value; NotifyPropertyChanged(); }
            get { return _作成月; }
        }

        private int? _作成締日 = null;
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

        #region SRY09010

        /// <summary>
        /// 車輌合計表画面
        /// </summary>
        public SRY09010()
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
            frmcfg = (ConfigSRY09010)ucfg.GetConfigValue(typeof(ConfigSRY09010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigSRY09010();
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
                this.作成締日 = frmcfg.締日;
                this.作成年 = frmcfg.作成年;
                this.作成月 = frmcfg.作成月;
                this.集計期間From = frmcfg.集計期間From;
                this.集計期間To = frmcfg.集計期間To;
                this.表示順序_Cmb.SelectedIndex = frmcfg.区分1;

            }
            #endregion

            //F1 車輌 
            base.MasterMaintenanceWindowList.Add("M05_CAR", new List<Type> { null, typeof(SCH06010) });
            AppCommon.SetutpComboboxList(this.表示順序_Cmb, false);
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
                    case SEARCH_SRY09010:
                        DispPreviw(tbl);
                        break;

                    //締日CSV出力用
                    case SEARCH_SRY09010_CSV:
						if (tbl == null)
						{
							break;
						}
						tbl.Columns.Remove("集計年月From");
						tbl.Columns.Remove("集計年月To");
						tbl.Columns.Remove("表示順序");
						tbl.Columns.Remove("車輌指定");
						tbl.Columns.Remove("車輌ﾋﾟｯｸｱｯﾌﾟ");
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
                    SCH06010 srch = new SCH06010();
                    switch (uctext.DataAccessName)
                    {
                        case "M05_CAR":
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

            //作成締日
            if (作成締日 == null)
            {
                this.ErrorMessage = "作成締日は入力必須項目です。";
                MessageBox.Show("作成締日は入力必須項目です。");
                return;
            }

            //作成年月
            if (作成年 == null || 作成月 == null)
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                MessageBox.Show("作成年月は入力必須項目です。");
                return;
            }

            //集計期間
            if (集計期間From == null || 集計期間To == null)
            {
                this.ErrorMessage = "集計期間は入力必須項目です。";
                MessageBox.Show("集計期間は入力必須項目です。");
                return;
            }

            int i表示順序 = 表示順序_Cmb.SelectedIndex;
            string p集計期間From, p集計期間To;
            p集計期間From = 集計期間From.ToString().Substring(0, 4) + 集計期間From.ToString().Substring(5, 2);
            p集計期間To = 集計期間To.ToString().Substring(0, 4) + 集計期間To.ToString().Substring(5, 2);

            //車輌リスト作成
            int?[] i車輌List = new int?[0];
            if (!string.IsNullOrEmpty(車輌ピックアップ))
            {
                string[] 車輌List = 車輌ピックアップ.Split(',');
                i車輌List = new int?[車輌List.Length];

                for (int i = 0; i < 車輌List.Length; i++)
                {
                    string str = 車輌List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "車輌指定の形式が不正です。";
                        return;
                    }
                    i車輌List[i] = code;
                }
            }

            //帳票出力用
			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SRY09010_CSV, new object[] { 車輌From , 車輌To , i車輌List , 
                                                                                                             p集計期間From , p集計期間To ,
                                                                                                             i表示順序 
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

            //作成締日
            if (作成締日 == null)
            {
                this.ErrorMessage = "作成締日は入力必須項目です。";
                MessageBox.Show("作成締日は入力必須項目です。");
                return;
            }

            //作成年月
            if (作成年 == null || 作成月 == null)
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                MessageBox.Show("作成年月は入力必須項目です。");
                return;
            }

            //集計期間
            if (集計期間From == null || 集計期間To == null)
            {
                this.ErrorMessage = "集計期間は入力必須項目です。";
                MessageBox.Show("集計期間は入力必須項目です。");
                return;
            }

            int i表示順序 = 表示順序_Cmb.SelectedIndex;
            string p集計期間From, p集計期間To;
            p集計期間From = 集計期間From.ToString().Substring(0, 4) + 集計期間From.ToString().Substring(5, 2);
            p集計期間To = 集計期間To.ToString().Substring(0, 4) + 集計期間To.ToString().Substring(5, 2);

            //車輌リスト作成
            int?[] i車輌List = new int?[0];
            if (!string.IsNullOrEmpty(車輌ピックアップ))
            {
                string[] 車輌List = 車輌ピックアップ.Split(',');
                i車輌List = new int?[車輌List.Length];

                for (int i = 0; i < 車輌List.Length; i++)
                {
                    string str = 車輌List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "車輌指定の形式が不正です。";
                        return;
                    }
                    i車輌List[i] = code;
                }
            }

            //帳票出力用
			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SRY09010, new object[] { 車輌From , 車輌To , i車輌List , 
                                                                                                             p集計期間From , p集計期間To ,
                                                                                                             i表示順序 
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
                view.MakeReport("車輌合計表", rptFullPathName_PIC, 0, 0, 0);
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


        private void Lost_Simebi(object sender, RoutedEventArgs e)
        {
            if (作成締日 == null)
            {
                作成締日 = 31;
            }
        }

        //作成年がNullの場合は今年の年を挿入
        private void Lost_Year(object sender, RoutedEventArgs e)
        {
            if (作成年 == null)
            {
                作成年 = Convert.ToInt32(DateTime.Today.ToString().Substring(0, 4));
            }

        }

        //作成月がNullの場合は今月の月を挿入
        private void Lost_Month(object sender, RoutedEventArgs e)
        {
            if (作成月 == null)
            {
                作成月 = Convert.ToInt32(DateTime.Today.ToString().Substring(5, 2));
            }

            //メソッドで期間計算
            DateFromTo ret = AppCommon.GetDateFromTo(Convert.ToInt32(作成年), Convert.ToInt32(作成月), Convert.ToInt32(作成締日));
            if (ret.Result == false || ret.Kikan > 31)
            {
                return;
            }
            集計期間From = ret.DATEFrom;
            集計期間To = ret.DATETo;

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

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            frmcfg.締日 = this.作成締日;
            frmcfg.作成年 = this.作成年;
            frmcfg.作成月 = this.作成月;
            frmcfg.集計期間From = this.集計期間From;
            frmcfg.集計期間To = this.集計期間To;
            frmcfg.区分1 = this.表示順序_Cmb.SelectedIndex;
            ucfg.SetConfigValue(frmcfg);
        }

    }
}
