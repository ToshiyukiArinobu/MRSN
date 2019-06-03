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
using System.Data.SqlClient;
using System.Data;

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;


namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
	/// 請求書発行画面
	/// </summary>
	public partial class TKS01010 : WindowReportBase
	{
		private const string UriageTabelNm = "T01_TRN";

		/// <summary>
		/// 請求書発行
		/// </summary>
		public TKS01010()
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

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

//		/// <summary>
//		/// 接続とDataSetの作成
//		/// </summary>
//		/// <param name="SarchSql">整形後のWhere部分のSQL</param>
//		/// <param name="SeikyuFirst">開始請求日</param>
//		/// <param name="SeikyuSecond">終了請求日</param>
//		void SearchColumn(string SarchSql,string SeikyuFirst,string SeikyuSecond)
//		{
//			string conString = string.Format("Provider=SQLOLEDB;Data Source={0};initial catalog=TRAC3;persist security info=True;user id=trac3;password=Kyoei7511;", serverNm);
//			OleDbConnection objConn = new OleDbConnection(conString);
//			objConn.Open();

//			string dsSql = string.Format(@"SELECT
//							2 AS 請求書,
//							M01_TOK.取引先ID AS ID,
//							M01_TOK.かな読み AS 取引先,
//							M01_TOK.郵便番号 AS 郵便番号,
//							M01_TOK.住所１ AS 住所1,
//							M01_TOK.住所２ AS 住所2,
//							M01_TOK.電話番号 AS 電話番号,
//							SUM(ISNULL(T01_TRN.売上金額, 0) + ISNULL(T01_TRN.通行料, 0) + 
//								CASE WHEN M01_TOK.Ｔ税区分ID <= 1 THEN FLOOR(ISNULL(T01_TRN.売上金額, 0) * dbo.F01_ZEI(T01_TRN.請求日付))
//										WHEN M01_TOK.Ｔ税区分ID = 2 THEN CEILING(ISNULL(T01_TRN.売上金額, 0) * dbo.F01_ZEI(T01_TRN.請求日付))
//										WHEN M01_TOK.Ｔ税区分ID = 3 THEN CAST(ROUND(ISNULL(T01_TRN.売上金額, 0) * dbo.F01_ZEI(T01_TRN.請求日付),0) AS INT)
//										WHEN M01_TOK.Ｔ税区分ID = 4 THEN CEILING(ISNULL(T01_TRN.売上金額, 0) / (100 + dbo.F01_ZEI(T01_TRN.請求日付)) * dbo.F01_ZEI(T01_TRN.請求日付))
//										WHEN M01_TOK.Ｔ税区分ID = 5 THEN FLOOR(ISNULL(T01_TRN.売上金額, 0) * dbo.F01_ZEI(T01_TRN.請求日付))
//										WHEN M01_TOK.Ｔ税区分ID = 6 THEN CEILING(ISNULL(T01_TRN.売上金額, 0) * dbo.F01_ZEI(T01_TRN.請求日付))
//										WHEN M01_TOK.Ｔ税区分ID = 7 THEN CAST(ROUND(ISNULL(T01_TRN.売上金額, 0) * dbo.F01_ZEI(T01_TRN.請求日付),0) AS INT)
//										WHEN M01_TOK.Ｔ税区分ID = 8 THEN 0 END) AS 請求額
//						FROM
//							T01_TRN
//							LEFT JOIN M01_TOK ON T01_TRN.得意先ID = M01_TOK.取引先ID
//						WHERE
//							{0}
//							T01_TRN.請求日付 BETWEEN {1} AND {2}				
//						GROUP BY
//							M01_TOK.取引先ID,
//							M01_TOK.かな読み,
//							M01_TOK.郵便番号,
//							M01_TOK.住所１,
//							M01_TOK.住所２,
//							M01_TOK.電話番号", SarchSql,SeikyuFirst,SeikyuSecond);


//			OleDbCommand selectCmd = new OleDbCommand();
//			selectCmd.Connection = objConn;
//			selectCmd.CommandText = dsSql;

//			//SqlDataAdapter adapter = new SqlDataAdapter(ds, objConn);
//			OleDbDataAdapter adapter = new OleDbDataAdapter();
//			adapter.SelectCommand = selectCmd;

//			adapter.FillSchema(ds, SchemaType.Source, DataTabelNm);
//			adapter.Fill(ds, DataTabelNm);
			
//			//insert,update,deleteのsql文を自動生成
//			cmdBuilder = new OleDbCommandBuilder(adapter);
//			adapter.InsertCommand = cmdBuilder.GetInsertCommand();
//			adapter.UpdateCommand = cmdBuilder.GetUpdateCommand();
//			adapter.DeleteCommand = cmdBuilder.GetDeleteCommand();

//			//DataGridに表示対象のDataTabelとAdapterを渡す
//			this.DataGrid.ItemSources = ds.Tables[DataTabelNm];
//		}

		/// <summary>
		/// 検索ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{

			////DataTabel初期化
			//if (ds.Tables[DataTabelNm] != null)
			//{
			//	ds.Tables[DataTabelNm].Clear();
			//}

			//string SeikyuFirst = "'" + this.SeikyuuTaisyouKikan_First.SelectedDate.ToString() + ".000'";
			//string SeikyuSecond = "'" + this.SeikyuuTaisyouKikan_Second.SelectedDate.ToString() + ".000'";

			//SeikyuFirst = SeikyuFirst.Replace("/", "-");
			//SeikyuSecond = SeikyuSecond.Replace("/", "-");
		

			//try
			//{
			//	string pick = JudgPickUp();
			//	string pickHani = JudgPickUpHani();

			//	//項目が全て未入力の場合は全て表示
			//	if (pick == "" & pickHani == "")
			//	{
			//		targetSql = "";

			//	}
			//	else
			//	{
			//		if (pick != "" & pickHani != "")
			//		{
			//			targetSql = string.Format("{0} AND {1} AND", pick, pickHani);
			//		}
			//		else
			//		{
			//			targetSql = string.Format("{0}{1} AND", pick, pickHani);
			//		}
			//	}

			//	SearchColumn(targetSql,SeikyuFirst,SeikyuSecond);
			//}
			//catch (Exception)
			//{
			//}

		}

		/// <summary>
		/// ピックアップ判定の整形
		/// </summary>
		/// <returns></returns>
		private string JudgPickUp()
		{
			string stPick = "";

			//if (this.PickUpSitei.FirstText_Text == "")
			//{
			//	return stPick;
			//}

			//stPick = this.PickUpSitei.FirstText_Text.Replace(",", " OR T01_TRN.得意先ID = ");
			//stPick = string.Format("T01_TRN.得意先ID ={0}", stPick);

			return stPick;

		}

		/// <summary>
		/// ピックアップ範囲の整形
		/// </summary>
		/// <returns>範囲指定の整形文字列</returns>
		private string JudgPickUpHani()
		{

			string stPickHani = "";

			//string stFirst = this.PickUpHaniSitei.FirstText_Text;
			//string stSecond = this.PickUpHaniSitei.SecondText_Text;

			//if (stFirst != "")
			//{
			//	if (stSecond != "")
			//	{
			//		stPickHani = string.Format("'{0}' <= T01_TRN.得意先ID AND T01_TRN.得意先ID <= '{1}'", stFirst, stSecond);
			//		return stPickHani;
			//	}
			//	else
			//	{
			//		stPickHani = string.Format("'{0}' <= T01_TRN.得意先ID", stFirst);
			//		return stPickHani;
			//	}
			//}
			//else
			//{
			//	if (stSecond != "")
			//	{
			//		stPickHani = string.Format("T01_TRN.得意先ID　<= '{0}'", stSecond);
			//		return stPickHani;
			//	}
			//	else
			//	{
			//		return stPickHani;
			//	}
			//}

			return stPickHani;
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
	}
}
