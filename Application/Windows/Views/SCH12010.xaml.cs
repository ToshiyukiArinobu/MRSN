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
	public partial class SCH12010 : WindowMasterSearchBase
	{
        private const string TabelNm = "M71_JIS_SCH";

        private string _確定コード = string.Empty;
        public string 確定コード
        {
            get { return this._確定コード; }
            set { this._確定コード = value; NotifyPropertyChanged(); }
        }

        //データグリッドバインド用データテーブル
        private DataTable _jishaData = new DataTable();
        public DataTable JishaData
        {
            get { return this._jishaData; }
            set
            {
                this._jishaData = value;
                NotifyPropertyChanged();
            }
        }

        public SCH12010()
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
            //初期データ取得
            GridOutPut();
		}

		/// <summary>
		/// データの取得
		/// </summary>
		private void GridOutPut()
		{
            int jisyaid = -1;
            if (!int.TryParse(this._確定コード, out jisyaid))
            {
                jisyaid = -1;
            }

            try
            {
                //自社名マスタ
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TabelNm, new object[] { jisyaid }));
            }
            catch (Exception)
            {
                return;
            }
		}

        /// <summary>
        /// データ取得エラー時処理
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            MessageBox.Show(ErrorMessage);
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
			case TabelNm:
				JishaData = tbl;
				break;
			default:
				break;
			}
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
			GridOutPut();
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
			// グリッドの中で選択された行があるかどうかをチェックして終了
			Close();
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

	}
}
