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

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;
using KyoeiSystem.Framework.Windows.Controls;


namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 得意先売上明細書画面
    /// </summary>
    public partial class DLY24010 : WindowReportBase
    {
        #region 定数定義

        //CSV出力用定数
        private const string SEARCH_DLY24010_CSV = "SEARCH_DLY24010_CSV";
        //プレビュー出力用定数
        private const string SEARCH_DLY24010 = "SEARCH_DLY24010";
        //レポート
        private const string rptFullPathName_PIC = @"Files\DLY\DLY24010.rpt";
        //Spread
        private const string SPREAD_DLY24010 = "SPREAD_DLY24010";

        #endregion

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;

        /// <summary>
        /// 画面固有設定項目のクラス定義
        /// ※ 必ず public で定義する。
        /// </summary>
        public class ConfigDLY24010 : FormConfigBase
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
        public ConfigDLY24010 frmcfg = null;

        #endregion

        #region バインド用プロパティ


		private DateTime? _検索日付From = null;
		public DateTime? 検索日付From
        {
			set { _検索日付From = value; NotifyPropertyChanged(); }
			get { return _検索日付From; }
        }
		private DateTime? _検索日付To = null;
		public DateTime? 検索日付To
        {
			set { _検索日付To = value; NotifyPropertyChanged(); }
			get { return _検索日付To; }
        }

        DateTime? _出力日付 = DateTime.Today;
        public DateTime? 出力日付
        {
            get { return this._出力日付; }
            set { this._出力日付 = value; NotifyPropertyChanged(); }
        }

		private int _表示区分 = 1;
		public int 表示区分
		{
			set { _表示区分 = value; NotifyPropertyChanged(); }
			get { return _表示区分; }
		}

		private int _抽出区分 = 0;
		public int 抽出区分
		{
			set { _抽出区分 = value; NotifyPropertyChanged(); }
			get { return _抽出区分; }
		}

		private int _取引区分 = 1;
        public int 取引区分
        {
            get { return this._取引区分; }
            set { this._取引区分 = value; NotifyPropertyChanged(); }
        }

		private int? _担当者ID = null;
		public int? 担当者ID
        {
			get { return this._担当者ID; }
			set { this._担当者ID = value; NotifyPropertyChanged(); }
        }

		

        #endregion

        #region LOAD処理

        /// <summary>
        /// 得意先売上明細書
        /// </summary>
        public DLY24010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// 画面読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonWindow_Loaded_1(object sender, RoutedEventArgs e)
		{
			
			base.MasterMaintenanceWindowList.Add("M72_TNT", new List<Type> { null, typeof(SCH23010) });

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigDLY24010)ucfg.GetConfigValue(typeof(ConfigDLY24010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigDLY24010();
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
				//this.作成締日 = frmcfg.締日;
				//this.作成年 = frmcfg.作成年;
				//this.作成月 = frmcfg.作成月;
				//this.集計期間From = frmcfg.集計期間From;
				//this.集計期間To = frmcfg.集計期間To;
				//this.表示区分_Cmb.SelectedIndex = frmcfg.区分1;
            }
            #endregion

            //得意先ID用
            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { null, typeof(SCH01010) });

			SetFocusToTopControl();
        }

        #endregion

        #region 画面クリア

        private void ScreenClear()
        {

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
                var data = message.GetResultData();

                if (data is DataTable)
                {
                    DataTable tbl = data as DataTable;

                    switch (message.GetMessageName())
                    {
                        //検索結果取得時
                        case SEARCH_DLY24010:
                            DispPreviw(tbl);
                            break;

                        case SEARCH_DLY24010_CSV:
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
        #endregion

        #region エラーメッセージ表示

        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            this.ErrorMessage = (string)message.GetResultData();
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
                    srch.multi = 0;
                    srch.表示区分 = 1;
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
        /// F5　CSV出力
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

			if (string.IsNullOrEmpty(textbox検索日付From.Text))
			{
				検索日付From = null;
			}
			if (string.IsNullOrEmpty(textbox検索日付To.Text))
			{
				検索日付To = null;
			}
			if (検索日付From == null || 検索日付To == null)
            {
                this.ErrorMessage = "集計期間は入力必須項目です。";
                MessageBox.Show("集計期間は入力必須項目です。");
                return;
            }


			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_DLY24010_CSV, new object[] { 検索日付From, 検索日付To, 抽出区分, 担当者ID }));
        }
       
        /// <summary>
        /// F8　リボン印刷
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

			if (string.IsNullOrEmpty(textbox検索日付From.Text))
			{
				検索日付From = null;
			}
			if (string.IsNullOrEmpty(textbox検索日付To.Text))
			{
				検索日付To = null;
			}
            if (検索日付From == null || 検索日付To == null)
            {
                this.ErrorMessage = "集計期間は入力必須項目です。";
                MessageBox.Show("集計期間は入力必須項目です。");
                return;
            }

			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_DLY24010, new object[] { 検索日付From, 検索日付To, 抽出区分, 担当者ID }));
        }

        /// <summary>
        /// F11　リボン終了
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
				string Tyusyutu_Nm = "";
				switch (抽出区分)
				{
				case 0:
					Tyusyutu_Nm = "全件";
					break;
				case 1:
					Tyusyutu_Nm = "自社のみ";
					break;
				case 2:
					Tyusyutu_Nm = "傭車のみ";
					break;
				}


				var parms = new List<Framework.Reports.Preview.ReportParameter>()
				{
					new Framework.Reports.Preview.ReportParameter(){ PNAME="日付FROM", VALUE=(this.検索日付From==null?"":textbox検索日付From.Text)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="日付TO", VALUE=(this.検索日付To==null?"":textbox検索日付To.Text)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="担当者指定ID", VALUE=(txtbox担当者指定.Text1??"")},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="担当者指定名", VALUE=(txtbox担当者指定.Text2??"")},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="抽出区分", VALUE=(Tyusyutu_Nm)},
				};
                KyoeiSystem.Framework.Reports.Preview.ReportPreview view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
                //第1引数　帳票タイトル
                //第2引数　帳票ファイルPass
                //第3以上　帳票の開始点(0で良い)
                view.MakeReport("チェックリスト", rptFullPathName_PIC, 0, 0, 0);
                //帳票ファイルに送るデータ。
                //帳票データの列と同じ列名を保持したDataTableを引数とする
				view.SetReportData(tbl);
				view.SetupParmeters(parms);
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

        #region Mindoow_Closed
        //画面が閉じられた時、データを保持する
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
			//frmcfg.締日 = this.作成締日;
			//frmcfg.作成年 = this.作成年;
			//frmcfg.作成月 = this.作成月;
			//frmcfg.区分1 = this.表示区分_Cmb.SelectedIndex;
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

		private void txtbox担当者指定_PreviewKeyDown(object sender, KeyEventArgs e)
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

    }
}
