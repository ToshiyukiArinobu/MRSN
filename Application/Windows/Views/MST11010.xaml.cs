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
    /// コース配車マスタ入力
	/// </summary>
	public partial class MST11010 : WindowMasterMainteBase
	{
        //対象テーブル
        //private const string TargetTableNm = "M10_UHK";

        #region バインド用プロパティ
        private string _コース配車コード = string.Empty;
        public string コース配車コード
        {
            get { return this._コース配車コード; }
            set { this._コース配車コード = value; NotifyPropertyChanged(); }
        }
        private string _コース配車名 = string.Empty;
        public string コース配車名
        {
            get { return this._コース配車名; }
            set { this._コース配車名 = value; NotifyPropertyChanged(); }
        }
        private string _得意先コード = string.Empty;
        public string 得意先コード
        {
            get { return this._得意先コード; }
            set { this._得意先コード = value; NotifyPropertyChanged(); }
        }
        private string _得意先名 = string.Empty;
        public string 得意先名
        {
            get { return this._得意先名; }
            set { this._得意先名 = value; NotifyPropertyChanged(); }
        }
        private string _請求内訳コード = string.Empty;
        public string 請求内訳コード
        {
            get { return this._請求内訳コード; }
            set { this._請求内訳コード = value; NotifyPropertyChanged(); }
        }
        private string _請求内訳 = string.Empty;
        public string 請求内訳
        {
            get { return this._請求内訳; }
            set { this._請求内訳 = value; NotifyPropertyChanged(); }
        }
        private string _支払先コード = string.Empty;
        public string 支払先コード
        {
            get { return this._支払先コード; }
            set { this._支払先コード = value; NotifyPropertyChanged(); }
        }
        private string _支払先名 = string.Empty;
        public string 支払先名
        {
            get { return this._支払先名; }
            set { this._支払先名 = value; NotifyPropertyChanged(); }
        }
        private string _車輌コード = string.Empty;
        public string 車輌コード
        {
            get { return this._車輌コード; }
            set { this._車輌コード = value; NotifyPropertyChanged(); }
        }
        private string _車輌名 = string.Empty;
        public string 車輌名
        {
            get { return this._車輌名; }
            set { this._車輌名 = value; NotifyPropertyChanged(); }
        }
        private string _乗務員コード = string.Empty;
        public string 乗務員コード
        {
            get { return this._乗務員コード; }
            set { this._乗務員コード = value; NotifyPropertyChanged(); }
        }
        private string _乗務員名 = string.Empty;
        public string 乗務員名
        {
            get { return this._乗務員名; }
            set { this._乗務員名 = value; NotifyPropertyChanged(); }
        }
        private string _発地コード = string.Empty;
        public string 発地コード
        {
            get { return this._発地コード; }
            set { this._発地コード = value; NotifyPropertyChanged(); }
        }
        private string _発地名 = string.Empty;
        public string 発地名
        {
            get { return this._発地名; }
            set { this._発地名 = value; NotifyPropertyChanged(); }
        }
        private string _着地コード = string.Empty;
        public string 着地コード
        {
            get { return this._着地コード; }
            set { this._着地コード = value; NotifyPropertyChanged(); }
        }
        private string _着地名 = string.Empty;
        public string 着地名
        {
            get { return this._着地名; }
            set { this._着地名 = value; NotifyPropertyChanged(); }
        }
        private string _売上金額 = string.Empty;
        public string 売上金額
        {
            get { return this._売上金額; }
            set { this._売上金額 = value; NotifyPropertyChanged(); }
        }
        private string _通行料 = string.Empty;
        public string 通行料
        {
            get { return this._通行料; }
            set { this._通行料 = value; NotifyPropertyChanged(); }
        }

        private string _支払水揚金額 = string.Empty;
        public string 支払水揚金額
        {
            get { return this._支払水揚金額; }
            set { this._支払水揚金額 = value; NotifyPropertyChanged(); }
        }

        private string _支払通行料 = string.Empty;
        public string 支払通行料
        {
            get { return this._支払通行料; }
            set { this._支払通行料 = value; NotifyPropertyChanged(); }
        }

        private string _自社部門コード = string.Empty;
        public string 自社部門コード
        {
            get { return this._自社部門コード; }
            set { this._自社部門コード = value; NotifyPropertyChanged(); }
        }
        private string _自社部門名 = string.Empty;
        public string 自社部門名
        {
            get { return this._自社部門名; }
            set { this._自社部門名 = value; NotifyPropertyChanged(); }
        }

        private string _明細区分 = string.Empty;
        public string 明細区分
        {
            get { return this._明細区分; }
            set { this._明細区分 = value; NotifyPropertyChanged(); }
        }

        #endregion
        /// <summary>
        /// コース配車マスタ入力
		/// </summary>
        public MST11010()
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
                    //	ボタンのクリック時のイベントを呼び出します
                    this.RibbonKensaku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonKensaku));
                    break;
                case Key.F2:
                    this.RibbonNyuuryoku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonNyuuryoku));
                    break;
                case Key.F6:
                    this.RibbonIChiran.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonIChiran));
                    break;
                case Key.F9:
                    this.Touroku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Touroku));
                    break;
                case Key.F10:
                    this.Clear.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Clear));
                    break;
                case Key.F11:
                    this.Syuuryou.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Syuuryou));
                    break;
                case Key.F12:
                    this.Delete.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Delete));
                    break;
			}
		}

		private void SearchButton_Click_1(object sender, RoutedEventArgs e)
		{
			DataRequest();
		}

		void DataRequest()
		{
			int triCd = 0;
			try
			{
			}
			catch (Exception)
			{
				return;
			}
		}

        /// <summary>
        /// データ受信メソッド
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
				case "":
					break;
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
			MessageBox.Show(ErrorMessage);
		}

		/// <summary>
		/// 最初のIDを表示するボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FistIdButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				//先頭データ検索
			}
			catch (Exception)
			{
				return;
			}
		}

		/// <summary>
		/// １つ前のIDを表示するボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BeforeIdButton_Click(object sender, RoutedEventArgs e)
		{
			int triCD = 0;
			try
			{
				//前データ検索
			}
			catch (Exception)
			{
				return;
			}
		}

		/// <summary>
		/// １つ次のIDを表示するボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NextIdButton_Click(object sender, RoutedEventArgs e)
		{
			int triCD = 0;
			try
			{
				//次データ検索
			}
			catch (Exception)
			{
				return;
			}
		}

		/// <summary>
		/// 最後尾のIDを表示するボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void LastIdButoon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //最後尾検索
            }
            catch (Exception)
            {
                return;
            }
        }

        #region リボン

        /// <summary>
        /// F1　リボン検索ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonKensaku_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("マスタ検索ボタンが押されました。");
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
        /// F6 マスタ一覧ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonIchiran_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("マスタ一覧ボタンが押されました。");
        }
        /// <summary>
        /// F9　マスタ登録ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Touroku_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("マスタ登録ボタンが押されました。");
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
        /// F10 リボン入力取消ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("入力取消ボタンが押されました。");
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
        /// F12　リボンマスタ削除ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("マスタ削除ボタンが押されました。");
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
        private void Purebyu_Click_1(object sender, RoutedEventArgs e)
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
