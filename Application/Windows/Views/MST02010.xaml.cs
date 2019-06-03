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


namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
	/// 請求内訳マスタ入力
	/// </summary>
	public partial class MST02010 : WindowMasterMainteBase
	{
		private const string TargetTableNm = "M10_UHK";

		private string _得意先コード = string.Empty;
		public string 得意先コード
		{
			get { return this._得意先コード; }
			set { this._得意先コード = value; NotifyPropertyChanged(); }
		}
		private string _取引先コード = string.Empty;
		public string 取引先コード
		{
			get { return this._取引先コード; }
			set { this._取引先コード = value; NotifyPropertyChanged(); }
		}
		private DataRow _mstData;
		public DataRow MstData
		{
			get { return this._mstData; }
			set
			{
				this._mstData = value;
				NotifyPropertyChanged();
				if (value != null)
				{
					this.取引先コード = string.Format("{0}", value["取引先ID"]);
				}
			}
		}

		/// <summary>
		/// 請求内訳マスタ入力
		/// </summary>
		public MST02010()
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
			case Key.F5:
				this.CsvSyuturyoku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.CsvSyuturyoku));
				break;
			case Key.F7:
				this.Purebyu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Purebyu));
				break;
			case Key.F8:
				this.Insatu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Insatu));
				break;
			case Key.F11:
				this.Syuuryou.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Syuuryou));
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
				triCd = int.Parse(this.取引先コード);
				//取引先コード指定の検索
				base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { triCd, 0 }));
			}
			catch (Exception)
			{
				return;
			}
		}

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
				MstData = null;
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
				base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 0 }));
			}
			catch (Exception)
			{
				return;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BeforeIdButton_Click(object sender, RoutedEventArgs e)
		{
			int triCD = 0;
			try
			{
				triCD = int.Parse(this.取引先コード);
				//前データ検索
				base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { triCD, -1 }));
			}
			catch (Exception)
			{
				return;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NextIdButton_Click(object sender, RoutedEventArgs e)
		{
			int triCD = 0;
			try
			{
				triCD = int.Parse(this.取引先コード);
				//次データ検索
				base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { triCD, 1 }));
			}
			catch (Exception)
			{
				return;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LastIdButoon_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				//最後尾検索
				base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 1 }));
			}
			catch (Exception)
			{
				return;
			}
		}


		/// <summary>
		/// 取引先コードの入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TorikisakiCD_LostFocus(object sender, RoutedEventArgs e)
		{
			DataRequest();
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
		/// F5 リボン　CSV出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CsvSyuturyoku_Click_1(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("CSV出力ボタンが押されました。");
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
