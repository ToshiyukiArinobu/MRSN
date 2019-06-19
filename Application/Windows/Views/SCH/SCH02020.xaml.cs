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
	public partial class SCH02020 : WindowMasterSearchBase
	{
        private const string TabelNm = "M10_UHK_SCH";
        private const string COLUM_ID = "請求内訳ID";
        private const string COLUM_NAME = "請求内訳名";
        private const string COLUM_JIGYOU = "かな読み";

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;

        //<summary>
        //画面固有設定項目のクラス定義
        //※ 必ず public で定義する。
        //</summary>
        public class ConfigSCH22020 : FormConfigBase
        {
            //public bool[] 表示順方向 { get; set; }
            /// コンボボックスの位置
            public int Combo { get; set; }
        }

        /// ※ 必ず public で定義する。
        public ConfigSCH22020 frmcfg = null;

        #endregion

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

        private string _得意先コード = string.Empty;
        public string 得意先コード
        {
            get { return this._得意先コード; }
            set { this._得意先コード = value; NotifyPropertyChanged(); }
        }

        private string _ｶﾅ読み検索 = string.Empty;
        public string ｶﾅ読み検索
        {
            get { return this._ｶﾅ読み検索; }
            set { this._ｶﾅ読み検索 = value; NotifyPropertyChanged(); }
        }
        private string _確定コード = string.Empty;
        public string 確定コード
        {
            get { return this._確定コード; }
            set { this._確定コード = value; NotifyPropertyChanged(); }
        }
        //private int _表示順 = 0;
        //public int 表示順
        //{
        //    get { return this._表示順; }
        //    set { this._表示順 = value; NotifyPropertyChanged(); }
        //}
        
        //データグリッドバインド用データテーブル
        private DataTable _tokuiData = new DataTable();
        public DataTable TokuiData
        {
            get { return this._tokuiData; }
            set
            {
                this._tokuiData = value;
                NotifyPropertyChanged();
            }
        }


        public string SelectedCodeList = string.Empty;
        private bool _multiSelect = false;
        public bool MultiSelect
        {
            get { return this._multiSelect; }
            set
            {
                this._multiSelect = value;
                if (value == true)
                {
                    this.SearchGrid.SelectionMode = DataGridSelectionMode.Extended;
                }
                else
                {
                    this.SearchGrid.SelectionMode = DataGridSelectionMode.Single;
                }
            }
        }


        public SCH02020()
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
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
            this.OkButton.FontSize = 9;
            this.OkButton.Content = "\n\n\n選択(F11)";
            this.CancelButton.FontSize = 9;
            this.CancelButton.Content = "\n\n\n終了(F12)";

            if (this.TwinTextBox.LinkItem != null)
            {
                //TwinTextLinkItem設定
                得意先コード = this.TwinTextBox.LinkItem.ToString();
                //TwinTokuisaki.Text1IsReadOnly = true;
            }

            //初期データ表示
            GridOutPut();

            //画面サイズをタスクバーをのぞいた状態で表示させる
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;

            //メイン画面と子画面が被ることなく表示できるかチェック
            if (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width < 1024 + 342)
            {
                //画面の左端に表示させる
                this.Left = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width;
            }
            //コンボボックスのSelectionChangedを設定する。
            this.OrderColumn.SelectionChanged += this.OrderColumn_SelectionChanged;

            AppCommon.SetutpComboboxList(this.OrderColumn, false);

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigSCH22020)ucfg.GetConfigValue(typeof(ConfigSCH22020));
            if (frmcfg == null)
            {
                frmcfg = new ConfigSCH22020();
                ucfg.SetConfigValue(frmcfg);
            }
            else
            {
                //表示できるかチェック
                var WidthCHK = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - frmcfg.Left;
                if (WidthCHK > 10)
                {
                    this.Left = frmcfg.Left;
                }
                //表示できるかチェック
                var HeightCHK = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - frmcfg.Top;
                if (HeightCHK > 10)
                {
                    this.Top = frmcfg.Top;
                }
                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;
                this.OrderColumn.SelectedIndex = frmcfg.Combo;



            }
            #endregion

            //#region 表示順序

            //if (SearchResult == null)
            //{
            //    return;
            //}

            //DataView view = new DataView(SearchResult);

            //switch (OrderColumn.SelectedIndex)
            //{
            //    case 0: //コード
            //    default:
            //        view.Sort = COLUM_ID;
            //        break;
            //    case 1: //商品名
            //        view.Sort = COLUM_NAME;
            //        break;
            //    case 2:　//商品よみ
            //        view.Sort = COLUM_JIGYOU;
            //        break;
            //}

            //SearchResult = view.ToTable();

            //#endregion

		}

		/// <summary>
		/// データの取得
		/// </summary>
		private void GridOutPut()
		{

            int iTokuisaki = 0;
            int toriid = 0;

            if (int.TryParse(得意先コード, out iTokuisaki))
            {
            }
            if (int.TryParse(確定コード, out toriid))
            {
            }
                try
                {
                    //取引先マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TabelNm, new object[] { iTokuisaki, toriid, ｶﾅ読み検索 }));
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
                    if (!string.IsNullOrEmpty(ｶﾅ読み検索))
                    {
                        DataTable dt = SearchResult.Clone();

                        foreach (DataRow dtRow in SearchResult.Select("かな読み LIKE '%" + ｶﾅ読み検索 + "%'"))
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
                                view.Sort = COLUM_JIGYOU;
                                break;
                        }

                        SearchResult = view.ToTable();

                    SearchGrid.SelectedIndex = 0;

				break;

			default:
				break;
			}
        }


        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            CloseDataSelected();
        }

        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            Close();
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
			else if (e.Key == Key.F11)
			{
				// 選択
				this.OkButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.OkButton));
			}
			else if (e.Key == Key.F1)
			{
				// 閉じる
				this.CancelButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.CancelButton));
			}
		}


        private void OkButton_MouseEnter(object sender, MouseEventArgs e)
        {
            this.OkButton.FontSize = 12;
            this.OkButton.Content = "選択";
        }

        private void OkButton_MouseLeave(object sender, MouseEventArgs e)
        {
            this.OkButton.FontSize = 9;
            this.OkButton.Content = "\n\n\n選択(F11)";
        }

        private void CancelButton_MouseEnter(object sender, MouseEventArgs e)
        {
            this.CancelButton.FontSize = 12;
            this.CancelButton.Content = "閉じる";
        }

        private void CancelButton_MouseLeave(object sender, MouseEventArgs e)
        {
            this.CancelButton.FontSize = 9;
            this.CancelButton.Content = "\n\n\n終了(F1)";
        }

		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
            // グリッドの中で選択された行があるかどうかをチェックして終了
            CloseDataSelected();
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

        /// <summary>
        /// グリッドダブルクリック時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CloseDataSelected();
        }

        /// <summary>
        /// データを呼び出し画面に戻して閉じる
        /// </summary>
        private void CloseDataSelected()
        {
            if (this.SearchGrid.SelectedItems.Count == 0)
            {
                return;
            }

            this.SelectedCodeList = string.Empty;
            try
            {
                List<string> work = new List<string>();
                string delmtr = "";
                foreach (DataRowView row in this.SearchGrid.SelectedItems)
                {
                    this.SelectedCodeList += delmtr + (row as DataRowView).Row["請求内訳ID"].ToString();
                    delmtr = ",";
                }
                //IInputElement element = Keyboard.FocusedElement;
                //var s = PresentationSource.FromDependencyObject(element as DependencyObject);
                //var eventArgs = new KeyEventArgs(Keyboard.PrimaryDevice, s, (int)System.DateTime.Now.Ticks, Key.Enter);
                //eventArgs.RoutedEvent = Keyboard.PreviewKeyDownEvent;
                //InputManager.Current.ProcessInput(eventArgs);
                //eventArgs.RoutedEvent = Keyboard.PreviewKeyUpEvent;
                //InputManager.Current.ProcessInput(eventArgs);

                this.TwinTextBox.Text1 = SearchResult.Rows[SearchGrid.SelectedIndex]["請求内訳ID"].ToString();
            }
            catch (Exception)
            {
            }
            this.DialogResult = true;
            this.Close();

        }

        private void TextKana_cTextChanged(object sender, RoutedEventArgs e)
        {
            GridOutPut();

        }

        private void TwinTokuisaki_cText1Changed(object sender, RoutedEventArgs e)
        {
            GridOutPut();

        }

        private void TextKakutei_cTextChanged(object sender, RoutedEventArgs e)
        {
            GridOutPut();
        }

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
                case 1: //車種名
                    view.Sort = COLUM_NAME;
                    break;
                case 2:　//事業者区分
                    view.Sort = COLUM_JIGYOU;
                    break;
            }

            SearchResult = view.ToTable();
            SearchGrid.SelectedIndex = 0;
        }

        #region Mindoow_Closed
        //画面が閉じられた時、データを保持する
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            frmcfg.Combo = this.OrderColumn.SelectedIndex;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion

        private void SearchGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CloseDataSelected();
            }
        }

	}
}
