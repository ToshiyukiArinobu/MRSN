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

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;
using KyoeiSystem.Application.Windows.Views;

namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
	/// MCustomer.xaml の相互作用ロジック
	/// </summary>
    public partial class DLY08010 : RibbonWindowViewBase
	{
		/// <summary>
		///　メンバー変数
		/// </summary>
		#region Member
		
		#endregion

		/// <summary>
		/// 伝票入力
		/// </summary>
		public DLY08010()
		{
			InitializeComponent();		
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
				this.GyouAdd.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.GyouAdd));
				break;
			case Key.F6:
				this.ItiranHyouji.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.ItiranHyouji));
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
		private void GyouAdd_Click_1(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("行追加ボタンが押されました。");
		}

		/// <summary>
		/// F5 リボン請求表更新ボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ItiranHyouji_Click_1(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("一覧表示ボタンが押されました。");
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
