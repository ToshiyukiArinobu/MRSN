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
using System.Data;

using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;


namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
    /// 乗務員運転免許管理表
	/// </summary>
	public partial class JMI14010 : WindowMasterMainteBase
	{
		private const string TargetTableNm = "M01_TOK_LIST";

        private string _乗務員FROM = string.Empty;
        public string 乗務員FROM
		{
            get { return this._乗務員FROM; }
            set { this._乗務員FROM = value; NotifyPropertyChanged(); }
		}

        private string _乗務員TO = string.Empty;
        public string 乗務員TO
		{
            get { return this._乗務員TO; }
            set { this._乗務員TO = value; NotifyPropertyChanged(); }
		}

        private string _日付指定FROM = string.Empty;
        public string 日付指定FROM
        {
            get { return this._日付指定FROM; }
            set { this._日付指定FROM = value; NotifyPropertyChanged(); }
        }

        private string _日付指定TO = string.Empty;
        public string 日付指定TO
        {
            get { return this._日付指定TO; }
            set { this._日付指定TO = value; NotifyPropertyChanged(); }
        }

        private string _就労区分 = "0";
        public string 就労区分
        {
            get { return this._就労区分; }
            set { this._就労区分 = value; NotifyPropertyChanged(); }
        }

		private string _表示順序 = "0";
        public string 表示順序
		{
            get { return this._表示順序; }
            set { this._表示順序 = value; NotifyPropertyChanged(); }
		}

		private string _表示区分 = "0";
        public string 表示区分
		{
			get { return this._表示区分; }
			set { this._表示区分 = value; NotifyPropertyChanged(); }
		}

		private DataTable _mSTData;
		public DataTable MSTData
		{
			get { return this._mSTData; }
			set
			{
				this._mSTData = value;
				NotifyPropertyChanged();
			}
		}


		/// <summary>
        /// 乗務員運転免許管理表
		/// </summary>
        public JMI14010()
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
                    //照会
                    this.RibbonKensaku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonKensaku));
                    break;
                case Key.F2:
                    //入力
                    this.RibbonNyuuryoku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonNyuuryoku));
                    break;
                case Key.F4:
                    //詳細表示
                    this.RibbonSyousai.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonSyousai));
                    break;
                case Key.F11:
                    //終了
                    this.Syuuryou.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Syuuryou));
                    break;
                case Key.F5:
                    //CSV出力
                    this.CsvSyuturyoku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.CsvSyuturyoku));
                    break;
                case Key.F7:
                    //プレビュー
                    this.PreView.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.PreView));
                    break;
                case Key.F8:
                    //印刷
                    this.Insatu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Insatu));
                    break;
			}
		}


		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			SearchColumn();
		}

		/// <summary>
		/// 接続とDataSetの作成
		/// </summary>
		void SearchColumn()
		{
			
		}

		public override void OnReceivedResponseData(CommunicationObject message)
		{
			var data = message.GetResultData();
			this.MSTData = (data is DataTable) ? (data as DataTable) : null;
			switch (message.GetMessageName())
			{
			case "":
				break;
			}

			this.Message = string.Format("取得データ {0} 件", this.MSTData.Rows.Count);
		}

		public override void OnReveivedError(CommunicationObject message)
		{
			base.OnReveivedError(message);
			MessageBox.Show(ErrorMessage);
		}


		#region リボン
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
        /// F4 リボン詳細表示ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonSyousaiHyouji_Click_1(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("詳細表示ボタンが押されました。");
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
        /// F5 リボン　CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CsvSyuturyoku_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("CSV出力ボタンが押されました。");
        }


        /// <summary>
        /// F8　リボン印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Insatu_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("印刷ボタンが押されました。");
        }

        /// <summary>
        /// F7　リボンプレビュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreView_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("プレビューボタンが押されました。");
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

		#endregion

	}
}
