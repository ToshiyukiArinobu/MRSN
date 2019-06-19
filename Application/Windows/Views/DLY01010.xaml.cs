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

using System.IO;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;
using System.Data;


using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;


namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
	/// MCustomer.xaml の相互作用ロジック
	/// </summary>
	public partial class DLY01010 : RibbonWindowViewBase
	{
		#region Const
		private const string UriageTableNm = "DLY10010_1";
		private const string UnkouTableNm = "DLY10010_2";
		private const string KeihiTableNm = "DLY10010_3";
		#endregion

		#region BindingMember

		private DataSet dsMain = new DataSet();
		private DataTable _dUriageData = null;
		private DataTable _dLogData = null;
		private DataTable _dKeihiData = null;
		[BindableAttribute(true)]
		public DataTable DUriageData
		{
			get
			{
				return this._dUriageData;
			}
			set
			{
				this._dUriageData = value;
				//this.DataGridUriage.ItemSources = value;
				NotifyPropertyChanged();
			}
		}
		[BindableAttribute(true)]
		public DataTable DLogData
		{
			get
			{
				return this._dLogData;
			}
			set
			{
				this._dLogData = value;
				NotifyPropertyChanged();
			}
		}
		[BindableAttribute(true)]
		public DataTable DKeihiData
		{
			get
			{
				return this._dKeihiData;
			}
			set
			{
				this._dKeihiData = value;
				//this.KeihiGrid.ItemSources = value;
				NotifyPropertyChanged();
			}
		}

		private string _detailsNumber = string.Empty;
		[BindableAttribute(true)]
		public string DetailsNumber
		{
			get
			{
				return this._detailsNumber;
			}
			set
			{
				this._detailsNumber = value;
				NotifyPropertyChanged();
			}
		}

		private int _ineNumber = 0;
		public int LineNumber
		{
			get
			{
				return _ineNumber;
			}
			set
			{
				this._ineNumber = value;
				NotifyPropertyChanged();
			}
		}

		private string driverID = string.Empty;
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

		#endregion

		public object _登録日時;
		public object 登録日時
		{
			get { return _登録日時; }
			set { _登録日時 = value; NotifyPropertyChanged(); }
		}

		/// <summary>
		/// 運転日報入力
		/// </summary>
		public DLY01010()
		{
			InitializeComponent();
			this.DataContext = this;

		}

		/// <summary>
		/// ロードイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//画面サイズをタスクバーをのぞいた状態で表示させる
			this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;
		}

		/// <summary>
		/// 取得データの取り込み
		/// </summary>
		/// <param name="message"></param>
		public override void OnReceivedResponseData(CommunicationObject message)
		{
			var data = message.GetResultData();
			DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
			switch (message.GetMessageName())
			{
			case UriageTableNm:
				this.DUriageData = tbl;
				break;
			case UnkouTableNm:
				this.DLogData = tbl;
				break;
			case KeihiTableNm:
				this.DKeihiData = tbl;
				break;
			}
			tbl.ColumnChanged += new DataColumnChangeEventHandler(OnColumnChanged);
			this.dsMain.Tables.Add(tbl);

		}

		private void OnColumnChanged(object sender, DataColumnChangeEventArgs e)
		{
			this.ChangedColumns = string.Format("{0} -> {1}", e.Column.ColumnName, e.ProposedValue);
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
			case Key.F3:
				this.KeihiTuika.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.KeihiTuika));
				break;
			case Key.F5:
				this.UriageKeireki.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.UriageKeireki));
				break;
			case Key.F9:
				this.DenpyouTouroku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.DenpyouTouroku));
				break;
			//F10にする。
			case Key.F12:
				this.Torikesi.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Torikesi));
				break;
			case Key.F11:
				this.Syuuryou.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Syuuryou));
				break;

			case Key.Q:
				//Expander展開

				if (SiharaiKanrenExpander.IsExpanded == true)
				{
					SiharaiKanrenExpander.IsExpanded = false;
				}
				else
				{
					SiharaiKanrenExpander.IsExpanded = true;
				}
				break;
			}

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// 内容クリアボタン押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ClearButton_Click_1(object sender, RoutedEventArgs e)
		{
		}

		/// <summary>
		/// 行追加ボタン　クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ColumnAddButton_Click_1(object sender, RoutedEventArgs e)
		{

		}

		/// <summary>
		/// 明細番号ロストフォーカス
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void He_MeisaiBangou_LostFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				int dnum = 0;
				if(!string.IsNullOrEmpty(this.DetailsNumber))
				{
					dnum = int.Parse(this.DetailsNumber);
				}
				if (this.Gyou.Text == "")
				{
					this.LineNumber = 1;
				}
				else
				{
					this.LineNumber = int.Parse(this.Gyou.Text as string);
			
				}
				CommunicationObject[] comlist = {
												new CommunicationObject(MessageType.RequestData, UriageTableNm, new object[] { dnum, null }),
												new CommunicationObject(MessageType.RequestData, UnkouTableNm, new object[] { dnum, null }),
												new CommunicationObject(MessageType.RequestData, KeihiTableNm, new object[] { dnum, null }),
											};
				dsMain.Tables.Clear();
				foreach (CommunicationObject com in comlist)
				{
					SendRequest(com);
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
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
		/// F3　リボン経費追加ボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void KeihiTuika_Click_1(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("経費追加ボタンが押されました。");
		}

		/// <summary>
		/// F5 リボン売上履歴ボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UriageKeireki_Click_1(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("売上履歴ボタンが押されました。");
		}

		/// <summary>
		/// F9　リボン伝票登録
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DenpyouTouroku_Click_1(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("伝票登録ボタンが押されました。");
		}

		/// <summary>
		/// F10　リボン入力取消し　
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Torikesi_Click_1(object sender, RoutedEventArgs e)
		{

			MessageBox.Show("取消しボタンが押されました。");
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
		/// ドロップ中のチェック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RibbonGroup_PreviewDragOver_1(object sender, DragEventArgs e)
		{
			//ファイル形式の場合
			if (e.Data.GetData(DataFormats.FileDrop) != null)
			{
				e.Effects = DragDropEffects.Copy;
			}
			else
			{
				e.Effects = DragDropEffects.None;
			}

			e.Handled = true;
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

		/// <summary>
		/// ドロップされた場合の処理　便利リンク
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RibbonGroup_Drop_1(object sender, DragEventArgs e)
		{


			string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
			if (files != null)
			{
				// 最初のファイルのPathを表示

				RibbonButton newButton = new RibbonButton();
				newButton.Label = files[0].Substring(27);


				//ドロップのイメージ挿入
				newButton.LargeImageSource = new BitmapImage(new Uri(/*files[0]*/"Picture\\Plas.bmp", UriKind.Relative));
				this.Ribbontest1.Items.Add(newButton);

			}
		}

		/// <summary>
		/// オプションタブ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RibbonOpusyonKinou_Drop_1(object sender, DragEventArgs e)
		{
			string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
			if (files != null)
			{
				// 最初のファイルのPathを表示

				RibbonButton newButton = new RibbonButton();
				newButton.Label = files[0].Substring(27);

				//ドロップのイメージ挿入
				newButton.LargeImageSource = new BitmapImage(new Uri(/*files[0]*/"Picture\\Plas.bmp", UriKind.Relative));

				this.RibbonOpusyonKinou.Items.Add(newButton);
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Gyou_LostFocus_1(object sender, RoutedEventArgs e)
		{

		}

		private void He_JyoumuinNm_LostFocus(object sender, RoutedEventArgs e)
		{

		}

	

	}
}
