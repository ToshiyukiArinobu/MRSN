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
using System.Data;
using System.Data.SqlClient;

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;

namespace KyoeiSystem.Application.Windows.Views
{

	/// <summary>
	/// MCustomer.xaml の相互作用ロジック
	/// </summary>
	public partial class SCH04010 : WindowMasterSearchBase
	{

		/// <summary>
		/// 初期化
		/// </summary>
		public SCH04010()
		{
			InitializeComponent();
			this.DataContext = this;
			this.Topmost = true;
		}

		/// <summary>
		/// Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MinWindow_Loaded_1(object sender, RoutedEventArgs e)
		{
			//紙芝居時のGrid設定
			//MotoGrid();	

			//画面サイズをタスクバーをのぞいた状態で表示させる
			this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;

			// 初期フォーカスの設定を行う
			this.ShimeiKana.Focus();
		}


		/// <summary>
		/// Grid内でEnter押下でタブ移動
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				Control s = e.Source as Control;
				if (s is Button)
				{
					//// クリックのときはイベント発生
					//s.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, s));
					this.ShimeiKana.Focus();
				}
				else
				{
					s.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
				}
				e.Handled = true;
			}
			else if (e.Key == Key.F5)
			{
				// 検索
				this.SearchButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.SearchButton));
			}
			else if (e.Key == Key.F11)
			{
				// 選択
				this.OkButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.OkButton));
			}
			else if (e.Key == Key.F12)
			{
				// 閉じる
				this.CancelButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.CancelButton));
			}
		}

		/// <summary>
		/// 検索ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			//GridOutPut(GetQuery(), GetOrderQuery());
		}

		/// <summary>
		/// 検索項目のクエリ作成
		/// </summary>
		/// <returns>整形後のクエリ</returns>
		private string GetQuery()
		{
			string Query = "";

			Query = TagetQueryAddAnd(Query,GetShimeiQuery());
			Query = TagetQueryAddAnd(Query, GetSelectNameQuery());

			if (Query != "")
			{
				Query = " AND " + Query;
			}

			return Query;
		}

		/// <summary>
		/// オーダー項目の取得
		/// </summary>
		/// <returns>オーダーするカラム名</returns>
		private string GetOrderQuery()
		{
			string Query = "";

			switch (this.OrderColumn.SelectedIndex)
			{
			case 0:
				Query = " 乗務員ID ";
				break;
			case 1:
				Query = " 乗務員名 ";
				break;
			case 2:
				Query = " かな読み ";
				break;
			}

			return Query;
			
		}

		/// <summary>
		/// 頭文字のクエリ作成
		/// </summary>
		/// <returns>整形後のクエリ</returns>
		private string GetSelectNameQuery()
		{
			string Query = "";

			switch (this.SelectName.SelectedIndex)
			{
			//全て
			case 0:
				break;
			//あ
			case 1:
				Query = " かな読み LIKE 'ｱ%' OR  かな読み LIKE 'ｲ%' OR  かな読み LIKE 'ｳ%' OR  かな読み LIKE 'ｴ%' OR  かな読み LIKE 'ｵ%' ";
				break;
			//か
			case 2:
				Query = " かな読み LIKE 'ｶ%' OR  かな読み LIKE 'ｷ%' OR  かな読み LIKE 'ｸ%' OR  かな読み LIKE 'ｹ%' OR  かな読み LIKE 'ｺ%' ";
				break;
			//さ
			case 3:
				Query = " かな読み LIKE 'ｻ%' OR  かな読み LIKE 'ｼ%' OR  かな読み LIKE 'ｽ%' OR  かな読み LIKE 'ｾ%' OR  かな読み LIKE 'ｿ%' ";
				break;
			//た
			case 4:
				Query = " かな読み LIKE 'ﾀ%' OR  かな読み LIKE 'ﾁ%' OR  かな読み LIKE 'ﾂ%' OR  かな読み LIKE 'ﾃ%' OR  かな読み LIKE 'ﾄ%' ";
				break;
			//な
			case 5:
				Query = " かな読み LIKE 'ﾅ%' OR  かな読み LIKE 'ﾆ%' OR  かな読み LIKE 'ﾇ%' OR  かな読み LIKE 'ﾈ%' OR  かな読み LIKE 'ﾉ%' ";
				break;
			//は
			case 6:
				Query = " かな読み LIKE 'ﾊ%' OR  かな読み LIKE 'ﾋ%' OR  かな読み LIKE 'ﾌ%' OR  かな読み LIKE 'ﾍ%' OR  かな読み LIKE 'ﾎ%' ";
				break;
			//ま
			case 7:
				Query = " かな読み LIKE 'ﾏ%' OR  かな読み LIKE 'ﾐ%' OR  かな読み LIKE 'ﾑ%' OR  かな読み LIKE 'ﾒ%' OR  かな読み LIKE 'ﾓ%' ";
				break;
			//や
			case 8:
				Query = " かな読み LIKE 'ﾔ%' OR  かな読み LIKE 'ﾕ%' OR  かな読み LIKE 'ﾖ%' ";
				break;
			//ら
			case 9:
				Query = " かな読み LIKE ﾗ% OR  かな読み LIKE ﾘ% OR  かな読み LIKE ﾙ% OR  かな読み LIKE ﾚ% OR  かな読み LIKE ﾛ% ";
				break;
			//わ
			case 10:
				Query = " かな読み LIKE ﾜ% OR  かな読み LIKE ｦ% OR  かな読み LIKE ﾝ% ";
				break;
			}
			return Query;
		}

		/// <summary>
		/// 氏名のクエリ作成
		/// </summary>
		/// <returns>整形後のクエリ</returns>
		private string GetShimeiQuery()
		{
			string Query = "";

			if (this.ShimeiKana.Text != "")
			{
                if (this.KanaKanji.Text != "")
				{
                    Query = string.Format(" かな読み LIKE '%{0}%' AND  乗務員名 LIKE '%{1}%' ", this.ShimeiKana.Text, this.KanaKanji.Text);
				}
				else
				{
                    Query = string.Format(" かな読み LIKE '%{0}%' ", this.ShimeiKana.Text);
				}
			}
			else
			{
                if (this.KanaKanji.Text != "")
				{
                    Query = string.Format(" 乗務員名 LIKE '%{0}%' ", this.KanaKanji.Text);
				}

			}

			return Query;
		}

		/// <summary>
		/// クエリにANDを加える
		/// </summary>
		/// <param name="Query"></param>
		/// <param name="TempQuery"></param>
		/// <returns></returns>
		private string TagetQueryAddAnd(string Query, string TempQuery)
		{
			if (TempQuery != "")
			{
				if (Query != "")
				{
					Query = Query + " AND " + TempQuery;
				}
				else
				{
					Query = TempQuery;
				}
			}

			return Query;
		}




		private void SearchButton_MouseEnter(object sender, MouseEventArgs e)
		{
			this.SearchButton.FontSize = 12;
			this.SearchButton.Content = "検索";
		}

		private void SearchButton_MouseLeave(object sender, MouseEventArgs e)
		{
			this.SearchButton.FontSize = 9;
			this.SearchButton.Content = "\n\n\nF5";
		}

		private void OkButton_MouseEnter(object sender, MouseEventArgs e)
		{
			this.OkButton.FontSize = 12;
			this.OkButton.Content = "選択";
		}

		private void OkButton_MouseLeave(object sender, MouseEventArgs e)
		{
			this.OkButton.FontSize = 9;
			this.OkButton.Content = "\n\n\nF11";
		}

		private void CancelButton_MouseEnter(object sender, MouseEventArgs e)
		{
			this.CancelButton.FontSize = 12;
			this.CancelButton.Content = "閉じる";
		}

		private void CancelButton_MouseLeave(object sender, MouseEventArgs e)
		{
			this.CancelButton.FontSize = 9;
			this.CancelButton.Content = "\n\n\nF12";
		}

		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("選択ボタンクリックイベント");
			Close();
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			//MessageBox.Show("閉じるボタンクリックイベント");
			Close();
		}

	}
}
