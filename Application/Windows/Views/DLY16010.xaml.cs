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

using System.IO;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;
using KyoeiSystem.Framework.Reports.Preview;

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;
using KyoeiSystem.Application.Windows.Views;

namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
	/// MCustomer.xaml の相互作用ロジック
	/// </summary>
    public partial class DLY16010 : RibbonWindowViewBase
	{
		/// <summary>
		///　メンバー変数
		/// </summary>
		#region Member

        DataSet dset = new DataSet("ReportData");
        DataTable tbl = new DataTable();

        //乗務員運行表
        private string csvFullPathDLY = @"Files\T_DLY16010.csv";
        private string rptFullPathNameDLY = @"Files\DLY16010.rpt";

		#endregion

        #region Binding用データ
            private string _運行日付 = "";
            public string 運行日付
            {
                set
                {
                    _運行日付 = value;
                    NotifyPropertyChanged();
                }
                get { return _運行日付; }
            }

            private int? _開始乗務員ID = null;
            public int? 開始乗務員ID
            {
                set
                {
                    _開始乗務員ID = value;
                    NotifyPropertyChanged();
                }
                get { return _開始乗務員ID; }
            }

            private int? _終了乗務員ID = null;
            public int? 終了乗務員ID
            {
                set
                {
                    _終了乗務員ID = value;
                    NotifyPropertyChanged();
                }
                get { return _終了乗務員ID; }
            }

            private int? _開始車種ID = null;
            public int? 開始車種ID
            {
                set
                {
                    _開始車種ID = value;
                    NotifyPropertyChanged();
                }
                get { return _開始車種ID; }
            }

            private int? _終了車種ID = null;
            public int? 終了車種ID
            {
                set
                {
                    _終了車種ID = value;
                    NotifyPropertyChanged();
                }
                get { return _終了車種ID; }
            }

            private int? _開始車輌ID = null;
            public int? 開始車輌ID
            {
                set
                {
                    _開始車輌ID = value;
                    NotifyPropertyChanged();
                }
                get { return _開始車輌ID; }
            }

            private int? _終了車輌ID = null;
            public int? 終了車輌ID
            {
                set
                {
                    _終了車輌ID = value;
                    NotifyPropertyChanged();
                }
                get { return _終了車輌ID; }
            }

            private string _乗務員ピックアップ = "";
            public string 乗務員ピックアップ
            {
                set
                {
                    _乗務員ピックアップ = value;
                    NotifyPropertyChanged();
                }
                get { return _乗務員ピックアップ; }
            }

            private string _車種ピックアップ = "";
            public string 車種ピックアップ
            {
                set
                {
                    _車種ピックアップ = value;
                    NotifyPropertyChanged();
                }
                get { return _車種ピックアップ; }
            }

            private string _車輌ピックアップ = "";
            public string 車輌ピックアップ
            {
                set
                {
                    _車輌ピックアップ = value;
                    NotifyPropertyChanged();
                }
                get { return _車輌ピックアップ; }
            }

            private bool isIDSelected = true;
            public bool IsIDSelected
            {
                get
                {
                    return this.isIDSelected;
                }
                set
                {
                    this.isIDSelected = value;
                    NotifyPropertyChanged();
                }
            }
            private bool isNameSelected = false;
            public bool IsNameSelected
            {
                get
                {
                    return this.isNameSelected;
                }
                set
                {
                    this.isNameSelected = value;
                    NotifyPropertyChanged();
                }
            }

            private string currenttime = string.Empty;
            public string CurrentTime
            {
                get
                {
                    return this.currenttime;
                }
                set
                {
                    this.currenttime = value;
                    NotifyPropertyChanged();
                }
            }
            private DataTable makeTable = new DataTable();
            public DataTable MakeTable
            {
                get
                {
                    return this.makeTable;
                }
                set
                {
                    this.makeTable = value;
                    NotifyPropertyChanged();
                }
            }
            private string tokuisakiID;
            public string TokuisakiID
            {
                get
                {
                    return this.tokuisakiID;
                }
                set
                {
                    this.tokuisakiID = value;
                    NotifyPropertyChanged();
                }
            }
            private string driverID;
            public string DriverID
            {
                get
                {
                    return this.driverID;
                }
                set
                {
                    this.driverID = value;
                    NotifyPropertyChanged();
                }
            }

            private string changedColumns = string.Empty;
            public string ChangedColumns
            {
                get
                {
                    return this.changedColumns;
                }
                set
                {
                    this.changedColumns = value;
                    NotifyPropertyChanged();
                }
            }

            private string _message = string.Empty;
            public string Message
            {
                get
                {
                    return this._message;
                }
                set
                {
                    this._message = value;
                    NotifyPropertyChanged();
                }
            }

            private string _test = string.Empty;
            public string TEST
            {
                get
                {
                    return this._test;
                }
                set
                {
                    this._test = value;
                    NotifyPropertyChanged();
                }
            }
        
        #endregion

        /// <summary>
		/// 運転日報入力
		/// </summary>
		public DLY16010()
            :base()
		{
			InitializeComponent();
            this.DataContext = this;
		}

		/// <summary>
		/// 画面読み込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainWindow_Loaded_1(object sender, RoutedEventArgs e)
		{
			//画面サイズをタスクバーをのぞいた状態で表示させる
			this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;
		}

        /// <summary>
        /// タイマーイベント受信時の処理
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedTimer(CommunicationObject message)
        {
            string str = string.Empty;
            str = string.Format("{0:yyyy/MM/dd HH:mm.ss}", DateTime.Now);
            this.CurrentTime = str;
        }

        public override void OnReveivedError(CommunicationObject message)
        {
            //base.OnReveivedError(message);
            //this.Message = base.ErrorMesasge;
        }

        /// <summary>
        /// 取得データの取り込み
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
			var data = message.GetResultData();
			this.MakeTable = (data is DataTable) ? (data as DataTable) : null;
			if (this.MakeTable != null)
            {
                ViewDataTabel();
            }
        }

        /// <summary>
        /// 表示するテーブルを帳票に渡す
        /// </summary>
        private void ViewDataTabel()
        {

            KyoeiSystem.Framework.Reports.Preview.ReportPreview view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();

            dset.Tables.Add(this.MakeTable);
            dset.DataSetName = "乗務員運行表";
            view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
            view.MakeReport(dset.DataSetName, rptFullPathNameDLY, 0, 0, 0);
            view.SetReportData(dset);
            view.ShowPreview();

            //dset.Tables.Add(csv.LoadTable(csvFullPathDLY, "乗務員運行表"));
            //dset.DataSetName = dset.Tables[0].TableName;
            //view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
            //view.MakeReport(dset.DataSetName, rptFullPathNameDLY, 0, 0, 0);
            //view.SetReportData(dset);
            //view.ShowPreview();
           
        }

		//<summary>
		//キーボードで押下されたkeyはここで拾えます
		//PreviewKeyDown="Window_PreviewKeyDown"をxamlのwindowプロパティに追加
		//</summary>
		private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			Control s = e.Source as Control;
			switch (e.Key)
			{
			case Key.F1:
				//	ボタンのクリック時のイベントを呼び出します
				this.RibbonKensaku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonKensaku));
				break;
			case Key.F2:
				this.RibbonNyuuryoku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonNyuuryoku));
				break;
			case Key.F5:
				this.CsvSyuturyoku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.CsvSyuturyoku));
				break;
			case Key.F7:
				this.Insatu_pure.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Insatu_pure));
				break;
			case Key.F8:
				this.Insatu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Insatu));
				break;
			case Key.F11:
				this.Syuuryou.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Syuuryou));
				break;
			}

		}

		/// <summary>
		/// F1　リボン検索ボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RibbonKensaku_Click_1(object sender, RoutedEventArgs e)
		{
			SCH04010 page = new SCH04010();
			page.Show();
		}

		/// <summary>
		/// F2　リボンマスタ入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RibbonNyuuryoku_Click_1(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("マスタ入力ボタンが押されました。");
		}


		/// <summary>
		/// F5 リボン　CSV出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CsvSyuturyoku_Click_1(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("CSV出力ボタンが押されました。");
		}

		private void Insatu_pure_Click_1(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("プレビューボタンが押されました。");

		}

		/// <summary>
		/// F8 リボン　印刷
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Insatu_Click_1(object sender, RoutedEventArgs e)
		{
            MakeKeyControl();
            string test = this.乗務員ピックアップ;
            

            CommunicationObject com;
            com = new CommunicationObject(MessageType.CallStoredProcedure, "DLY16010",
                new object[] { this.運行日付, this.開始乗務員ID, this.終了乗務員ID, this.開始車種ID, this.終了車種ID, this.開始車輌ID, this.終了車輌ID, this.乗務員ピックアップ, this.車種ピックアップ, this.車輌ピックアップ });
            base.SendRequest(com);		
		}

        /// <summary>
        /// Keyとなる項目を対応プロパティに格納する
        /// </summary>
        private void MakeKeyControl()
        {
            //BData.運行日付 = this.SearchDay.SecondDate_SelectedDate == null ? "" : string.Format("{0:yyyyMMdd}", this.SearchDay.FirstDate_SelectedDate);
            //BData.開始乗務員ID = this.CarCrewRange.FirstText_Text == "" ? null as int? : int.Parse(this.CarCrewRange.FirstText_Text);
            //BData.開始車種ID = this.VehicleRange.FirstText_Text == "" ? null as int? : int.Parse(this.VehicleRange.FirstText_Text);
            //BData.開始車輌ID = this.CarModelRange.FirstText_Text == "" ? null as int? : int.Parse(this.CarModelRange.FirstText_Text);
            //BData.終了乗務員ID = this.CarCrewRange.SecondText_Text == "" ? null as int? : int.Parse(this.CarCrewRange.SecondText_Text);
            //BData.終了車種ID = this.VehicleRange.SecondText_Text == "" ? null as int? : int.Parse(this.VehicleRange.SecondText_Text);
            //BData.終了車輌ID = this.CarModelRange.SecondText_Text == "" ? null as int? : int.Parse(this.CarModelRange.SecondText_Text);
            //BData.乗務員ピックアップ = this.CarCrew.FirstText_Text;
            //BData.車種ピックアップ = this.Vehicle.FirstText_Text;
            //BData.車輌ピックアップ = this.CarModel.FirstText_Text;

        }

		/// <summary>
		/// F11　リボン終了
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Syuuryou_Click_1(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// リボン便利リンク　検索ボタン押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Kensaku_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
		{
			Process.Start("http://www.yahoo.co.jp/");
		}

		/// <summary>
		/// リボン便利リンク　道路情報ボタン押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DouroJyouhou_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
		{
			Process.Start("http://www.jartic.or.jp/");

		}

		/// <summary>
		/// リボン便利リンク　道路ナビボタン押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DouroNabi_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
		{
			Process.Start("http://highway.drivenavi.net/");
		}

		/// <summary>
		/// リボン便利リンク　渋滞情報ボタン押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void JyuutaiJyouhou_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
		{
			Process.Start("http://www.mapfan.com/");
		}

		/// <summary>
		/// リボン便利リンク　天気ボタン押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Tenki_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
		{
			Process.Start("http://weathernews.jp/");
		}

		/// <summary>
		/// リボン　WebHomeボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RibbonButton_WebHome_Click_1(object sender, RoutedEventArgs e)
		{
			Process Pro = new Process();

			try
			{
				Pro.StartInfo.UseShellExecute = false;
				Pro.StartInfo.FileName = "C:\\Program Files (x86)/Internet Explorer/iexplore.exe";
				Pro.StartInfo.CreateNoWindow = true;
				Pro.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// リボン　メールボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RibbonButton_Meil_Click_1(object sender, RoutedEventArgs e)
		{
			Process Pro = new Process();

			try
			{
				Pro.StartInfo.UseShellExecute = false;
				Pro.StartInfo.FileName = "C://Program Files (x86)//Windows Live//Mail//wlmail.exe";
				Pro.StartInfo.CreateNoWindow = true;
				Pro.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// リボン　電卓ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RibbonButton_Dentaku_Click_1(object sender, RoutedEventArgs e)
		{
			Process Pro = new Process();

			try
			{
				Pro.StartInfo.UseShellExecute = false;
				Pro.StartInfo.FileName = "C://Windows//System32/calc.exe";
				Pro.StartInfo.CreateNoWindow = true;
				Pro.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
