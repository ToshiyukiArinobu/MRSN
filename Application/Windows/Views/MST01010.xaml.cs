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
	/// 取引先マスターメンテ
	/// </summary>
	public partial class MST01010 : WindowMasterMainteBase
	{
		private const string TargetTableNm = "M_M01_TOK";

		private string _customerID = string.Empty;
		public string CustomerID
		{
			get { return this._customerID; }
			set { this._customerID = value; NotifyPropertyChanged(); }
		}
		private DataRow _rowM01tok;
		public DataRow RowM01TOK
		{
			get { return this._rowM01tok; }
			set
			{
				this._rowM01tok = value;
				NotifyPropertyChanged();
				if (value != null)
				{
					this.CustomerID = string.Format("{0}", value["取引先ID"]);
				}
			}
		}
		
		private string _担当部門名 = string.Empty;
		public string 担当部門名
		{
			get { return this._担当部門名; }
			set { this._担当部門名 = value; NotifyPropertyChanged(); }
		}
		private string _親マスタコード名 = string.Empty;
		public string 親マスタコード名
		{
			get { return this._親マスタコード名; }
			set { this._親マスタコード名 = value; NotifyPropertyChanged(); }
		}


		/// <summary>
		/// 取引先マスタ問合せ
		/// </summary>
		public MST01010()
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
			//画面サイズをタスクバーをのぞいた状態で表示させる
			this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;
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
				this.RibbonItiran.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonItiran));
				break;
			case Key.F9:
				this.MasterTouroku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.MasterTouroku));
				break;
			//F10にする。
			case Key.F13:
				this.RibbonSakujyo.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonSakujyo));
				break;
			case Key.F11:
				this.Syuuryou.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Syuuryou));
				break;
			case Key.F12:
				this.RibbonSakujyo.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonSakujyo));
				break;
			}
		}

		private void SearchButton_Click_1(object sender, RoutedEventArgs e)
		{
		}


		#region Function

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

		//F1　リボン検索ボタン
		private void RibbonKensaku_Click_1(object sender, RoutedEventArgs e)
		{
			//		TantouIchiran page = new TantouIchiran();
			//		page.Show();
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
		/// F6 リボン　マスタ一覧
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RibbonItiran_Click_1(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("マスタ一覧ボタンが押されました。");
		}

		/// <summary>
		/// F12 リボン　マスタ削除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RibbonSakujyo_Click_1(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("マスタ削除ボタンが押されました。");
		}

		/// <summary>
		/// F9　リボン伝票登録
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DenpyouTouroku_Click_1(object sender, RoutedEventArgs e)
		{
			Update();
		}

		/// <summary>
		/// F13　リボン入力取消し　
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

		public override void OnReceivedResponseData(CommunicationObject message)
		{
			base.OnReceivedResponseData(message);
			try
			{
				var data = message.GetResultData();
				DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
				switch (message.GetMessageName())
				{
				case "":
					break;
				}

				if (tbl.Rows.Count > 0)
				{
					RowM01TOK = tbl.Rows[0];
				}
				else
				{
					RowM01TOK = null;
				}

			}
			catch (Exception ex)
			{
				RowM01TOK = null;
				this.ErrorMessage = ex.Message;
			}
		}

		public override void OnReveivedError(CommunicationObject message)
		{
			base.OnReveivedError(message);
			MessageBox.Show(ErrorMessage);
		}

		private void CustomerCd_LostFocus(object sender, RoutedEventArgs e)
		{
			int cstmid = 0;
			try
			{
				cstmid = int.Parse(this.CustomerID);
				//取引先マスタ
				base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { cstmid, 0 }));
			}
			catch (Exception)
			{
				return;
			}
		}

		private void Button1st_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				//取引先マスタ
				base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 0 }));
			}
			catch (Exception)
			{
				return;
			}
		}

		private void ButtonPrev_Click(object sender, RoutedEventArgs e)
		{
			int cstmid = 0;
			try
			{
				cstmid = int.Parse(this.CustomerID);
				//取引先マスタ
				base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { cstmid, -1 }));
			}
			catch (Exception)
			{
				return;
			}
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			int cstmid = 0;
			try
			{
				cstmid = int.Parse(this.CustomerID);
				//取引先マスタ
				base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { cstmid, 1 }));
			}
			catch (Exception)
			{
				return;
			}
		}

		private void ButtonLast_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				//取引先マスタ
				base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 1 }));
			}
			catch (Exception)
			{
				return;
			}
		}

		private void Update()
		{
			try
			{
				int cstmid;
				cstmid = int.Parse(this.CustomerID);

				DataTable tori = new DataTable(TargetTableNm);
				foreach (DataColumn col in this.RowM01TOK.Table.Columns)
				{
					DataColumn newcol = new DataColumn(col.ColumnName, col.DataType);
					newcol.AllowDBNull = col.AllowDBNull;
					tori.Columns.Add(newcol);
				}
				DataRow row = tori.NewRow();
				foreach (DataColumn col in tori.Columns)
				{
					row[col.ColumnName] = this.RowM01TOK[col.ColumnName];
				}
				tori.Rows.Add(row);

				SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNm, tori));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


	}
}
