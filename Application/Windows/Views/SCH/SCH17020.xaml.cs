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
	public partial class SCH17020 : WindowMasterSearchBase
	{
        private const string TabelNm = "M01_TOK_TOKU_SCH";
        private const string COLUM_ID = "取引先ID";
        private const string COLUM_NAME = "取引先名１";
        private const string COLUM_KANA = "かな読み";

         //
        //データグリッドバインド用データテーブル
        private DataTable _SearchResult = null;
        public DataTable SearchResult
        {
            get { return this._SearchResult; }
            set
            {
                this._SearchResult = value;
                NotifyPropertyChanged();
            }
        }
        private string _かな読み = string.Empty;
        public string かな読み
        {
            get { return this._かな読み; }
            set { this._かな読み = value; NotifyPropertyChanged(); }
        }
        private string _確定コード = string.Empty;
        public string 確定コード
        {
            get { return this._確定コード; }
            set { this._確定コード = value; NotifyPropertyChanged(); }
        }

        public SCH17020()
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
            // 初期フォーカスの設定を行う
            this.txtKana.Focus();
            GridOutPut();
            //画面サイズをタスクバーをのぞいた状態で表示させる
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;

            //メイン画面と子画面が被ることなく表示できるかチェック
            if (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width < 1024 + 342)
            {
                //画面の左端に表示させる
                this.Left = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width;
            }
        }

        /// <summary>
        /// データの取得
        /// </summary>
        private void GridOutPut()
        {
            int searchId = -1;
            if (!int.TryParse(this._確定コード, out searchId))
            {
                searchId = -1;
            }

            try
            {
                //商品マスタ
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TabelNm, new object[] { かな読み, searchId }));
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
                    //Gridのバインド変数に代入
                    SearchResult = tbl;

                    DataRow r = null;
                    //かな読みの条件で抽出
                    if (!string.IsNullOrEmpty(かな読み))
                    {
                        DataTable dt = SearchResult.Clone();

                        foreach (DataRow dtRow in SearchResult.Select("かな読み LIKE '%" + かな読み + "%'"))
                        {
                            r = dt.NewRow();
                            for (int n = 0; n < dtRow.ItemArray.Length; n++)
                            {
                                r[n] = dtRow[n];
                            }
                            dt.Rows.Add(r);
                        }
                        SearchResult = dt;
                    }
                    SearchGrid.SelectedIndex = 0;

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
                    //s.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, s));
                    this.txtKana.Focus();
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
            SetDataTwinText();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// グリッドダブルクリック時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SetDataTwinText();
        }

        /// <summary>
        /// データを呼び出し画面に戻して閉じる
        /// </summary>
        private void SetDataTwinText()
        {
            if (SearchGrid.SelectedIndex < 0)
            {
                return;
            }

            this.TwinTextBox.Text1 = SearchResult.Rows[SearchGrid.SelectedIndex][COLUM_ID].ToString();
            this.DialogResult = true;
            this.Close();
        }

        /// <summary>
        /// 表示順コンボボックス選択変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderColumn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchResult == null)
            {
                return;
            }

            DataView view = new DataView(SearchResult);

            switch (OrderColumn.SelectedIndex)
            {
                case 0: //コード
                default:
                    view.Sort = COLUM_ID;
                    break;
                case 1: //商品名
                    view.Sort = COLUM_NAME;
                    break;
                case 2:　//商品よみ
                    view.Sort = COLUM_KANA;
                    break;
            }

            SearchResult = view.ToTable();
            SearchGrid.SelectedIndex = 0;
        }

    }
}

