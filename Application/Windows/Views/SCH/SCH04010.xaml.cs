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
using System.Text.RegularExpressions;

namespace KyoeiSystem.Application.Windows.Views
{

	/// <summary>
	/// 乗務員マスタ検索画面
	/// </summary>
	public partial class SCH04010 : WindowMasterSearchBase
	{
		private const string GETDRIVERLIST = "SCH04010";
		DataTable tbl検索結果 = null;

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;

        //<summary>
        //画面固有設定項目のクラス定義
        //※ 必ず public で定義する。
        //</summary>
        public class ConfigSCH04010 : FormConfigBase
        {
            //public bool[] 表示順方向 { get; set; }
            /// コンボボックスの位置
            public int 表示順Combo { get; set; }
            public int Combo { get; set; }
            public int 就労Combo { get; set; }
        }

        /// ※ 必ず public で定義する。
        public ConfigSCH04010 frmcfg = null;

        #endregion
		
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

		private string _氏名読み = string.Empty;
		public string 氏名読み
		{
			get { return this._氏名読み; }
			set { this._氏名読み = value; NotifyPropertyChanged(); }
		}

		private string _氏名漢字 = string.Empty;
		public string 氏名漢字
		{
			get { return this._氏名漢字; }
			set { this._氏名漢字 = value; NotifyPropertyChanged(); }
		}

		private int _表示順 = 0;
		public int 表示順
		{
			get { return this._表示順; }
			set { this._表示順 = value; NotifyPropertyChanged(); }
		}

		private int _音順選択位置 = 0;
		public int 音順選択位置
		{
			get { return this._音順選択位置; }
			set { this._音順選択位置 = value; NotifyPropertyChanged(); }
		}

        private int _就労区分 = 0;
        public int 就労区分
        {
            get { return this._就労区分; }
            set { this._就労区分 = value; NotifyPropertyChanged(); }
        }

        private int _データ選択位置 = -1;
		public int データ選択位置
		{
			get { return this._データ選択位置; }
			set { this._データ選択位置 = value; NotifyPropertyChanged(); }
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
                    this.UcGrid.SelectionMode = DataGridSelectionMode.Extended;
                }
                else
                {
                    this.UcGrid.SelectionMode = DataGridSelectionMode.Single;
                }
            }
        }

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
		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
            this.OkButton.FontSize = 9;
            this.OkButton.Content = "\n\n\n選択(F11)";
            this.CancelButton.FontSize = 9;
            this.CancelButton.Content = "\n\n\n終了(F1)";

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigSCH04010)ucfg.GetConfigValue(typeof(ConfigSCH04010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigSCH04010();
				//画面サイズをタスクバーをのぞいた状態で表示させる
				this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;

				//メイン画面と子画面が被ることなく表示できるかチェック
				if (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width < 1024 + 342)
				{
					//画面の左端に表示させる
					this.Left = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width;
				}
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
				this.表示順 = frmcfg.表示順Combo;
				this.音順選択位置 = frmcfg.Combo;
                this.就労区分 = frmcfg.就労Combo;
            }
            #endregion

			// 初期フォーカスの設定を行う
			this.ShimeiKana.Focus();

			AppCommon.SetutpComboboxList(this.cmb表示順, false);
			AppCommon.SetutpComboboxList(this.cmb50音, false);
            AppCommon.SetutpComboboxList(this.cmb就労, false);
            GetDataRequest();
		}



		private void GetDataRequest()
		{
			// 絞り込みはローカルで実装するので、一覧取得は全件で要求する
			base.SendRequest(new CommunicationObject(MessageType.RequestData, GETDRIVERLIST, null, null));
		}

		public override void OnF11Key(object sender, KeyEventArgs e)
			{
				OkButton_Click(sender, null);
			}
		public override void OnF1Key(object sender, KeyEventArgs e)
			{
				CancelButton_Click(sender, null);
			}

		/// <summary>
		/// 検索ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SrchButton_Click(object sender, RoutedEventArgs e)
		{
			GetDataRequest();
		}

		/// <summary>
		/// 確定ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            CloseDataSelected();
		}

		/// <summary>
		/// キャンセルボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
            if (this.UcGrid.SelectedItems.Count == 0)
            {
                return;
            }

            this.SelectedCodeList = string.Empty;
            try
            {
                List<string> work = new List<string>();
                string delmtr = "";
                foreach (DataRowView row in this.UcGrid.SelectedItems)
                {
                    this.SelectedCodeList += delmtr + (row as DataRowView).Row["乗務員ID"].ToString();
                    delmtr = ",";
                }
                //IInputElement element = Keyboard.FocusedElement;
                //var s = PresentationSource.FromDependencyObject(element as DependencyObject);
                //var eventArgs = new KeyEventArgs(Keyboard.PrimaryDevice, s, (int)System.DateTime.Now.Ticks, Key.Enter);
                //eventArgs.RoutedEvent = Keyboard.PreviewKeyDownEvent;
                //InputManager.Current.ProcessInput(eventArgs);
                //eventArgs.RoutedEvent = Keyboard.PreviewKeyUpEvent;
                //InputManager.Current.ProcessInput(eventArgs);

                this.TwinTextBox.Text1 = SearchResult.Rows[データ選択位置]["乗務員ID"].ToString();
            }
            catch (Exception)
            {
            }
            this.DialogResult = true;
            this.Close();

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

		public override void OnReveivedError(CommunicationObject message)
		{
			base.OnReveivedError(message);
			this.Message = base.ErrorMessage;
		}

		public override void OnReceivedResponseData(CommunicationObject message)
		{
			var data = message.GetResultData();
			DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
			switch (message.GetMessageName())
			{
			case GETDRIVERLIST:
				tbl検索結果 = tbl;
				PickupAndSort();
				break;
			}
		}

		void PickupAndSort()
		{
			if (tbl検索結果 == null)
			{
				this.SearchResult = null;
				return;
			}

			Pickup();
			Sort();
		}

		private void Pickup_TextChanged(object sender, RoutedEventArgs e)
		{
			Pickup();
		}

		private void order_changed(object sender, SelectionChangedEventArgs e)
		{
			Sort();
		}

		private void pickup50on_changed(object sender, SelectionChangedEventArgs e)
		{
			Pickup();
		}

		string[] Pickup50onList = 
				{
					@"",
					@"あいうえお",
					@"かきくけこ",
					@"さしすせそ",
					@"たちつてと",
					@"なにぬねの",
					@"はひふへほ",
					@"まみむめも",
					@"やゆよ",
					@"らりるれろ",
					@"わをん",
				};

        private void Workon_TextChanged(object sender, RoutedEventArgs e)
        {
            Pickup();
        }

        private void Workon_changed(object sender, SelectionChangedEventArgs e)
        {
            Pickup();
        }
        string[] WorkonList = 
				{
					@"0",
					@"1",
				};


		// 検索結果の絞り込み
		void Pickup()
		{
			if (tbl検索結果 == null)
			{
				return;
			}
			this.SearchResult = tbl検索結果.Copy();

			string selectstr = string.Empty;
			string AND = "";

            if (this.音順選択位置 > 0)
            {
                // 0 の時は「全て」なのでそのまま
                // 1以上の時のみ絞り込む
                selectstr += "(";
                foreach (var chr in Pickup50onList[this.音順選択位置])
                {
                    selectstr += string.Format("{0}かな読み LIKE '{1}%'", AND, chr);
                    AND = " OR ";
                }
                selectstr += ")";
                AND = " AND ";
            }

            if (this.就労区分 > 0)
            {
                // 0 の時は「就業者」
                // 1 の時は「就業者以外」
                foreach (var chr in WorkonList[this.就労区分])
                {
                    if (chr == '0')
                    {
                        selectstr += string.Format("{0}就労区分 = {1}", AND,chr);
                    }
                    else
                    {
                        selectstr += string.Format("{0}就労区分 <> 0", AND);
                    }
                }
                AND = " AND ";
            }

            if (string.IsNullOrWhiteSpace(this.氏名読み) != true)
			{
                selectstr += string.Format("{0}かな読み LIKE '%{1}%'", AND, this.氏名読み);
				AND = " AND ";
			}
			if (string.IsNullOrWhiteSpace(this.氏名漢字) != true)
			{
                selectstr += string.Format("{0}乗務員名 LIKE '%{1}%'", AND, this.氏名漢字);
				AND = " AND ";
			}
			var q = (from x in tbl検索結果.Select(selectstr).AsEnumerable()
					 select new SRCH_DATA
					 {
						 id = (int)x["乗務員ID"],
						 name = (string)(x.IsNull("乗務員名") ? string.Empty : x["乗務員名"]),
						 kana = (string)(x.IsNull("かな読み") ? string.Empty : x["かな読み"]),
					 }).AsQueryable();
			SearchResult.Rows.Clear();
			foreach (var row in tbl検索結果.Select(selectstr).ToList())
			{
				SearchResult.ImportRow(row);
			}
		}

		class SRCH_DATA
		{
			public int id;
			public string name;
			public string kana;
		}

		// 検索結果の並べ替え
		void Sort()
		{
			if (SearchResult == null)
			{
				return;
			}
			var q = (from x in SearchResult.AsEnumerable()
					 select new SRCH_DATA
					 {
						 id = (int)x["乗務員ID"],
						 name = (string)(x.IsNull("乗務員名") ? string.Empty : x["乗務員名"]),
						 kana = (string)(x.IsNull("かな読み") ? string.Empty : x["かな読み"]),
					 }).AsQueryable();
			List<SRCH_DATA> data;
			switch (this.表示順)
			{
			case 0:		// コード
				data = q.OrderBy(x => x.id).ToList();
				break;
			case 1:		// 氏名
				data = q.OrderBy(x => x.name).ToList();
				break;
			default:	// 氏名読み
				data = q.OrderBy(x => x.kana).ToList();
				break;
			}
			SearchResult.Rows.Clear();
			foreach (var row in data)
			{
				SearchResult.Rows.Add(row.id, row.name, row.kana);
			}
		}

		#region Window_Closed
		//画面が閉じられた時、データを保持する
		private void Window_Closed(object sender, EventArgs e)
		{
			frmcfg.Top = this.Top;
			frmcfg.Left = this.Left;
			frmcfg.Height = this.Height;
			frmcfg.Width = this.Width;
			frmcfg.表示順Combo = this.表示順;
			frmcfg.Combo = this.音順選択位置;
            frmcfg.就労Combo = this.就労区分;
            ucfg.SetConfigValue(frmcfg);

		}
		#endregion

        private void UcGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CloseDataSelected();
            }
        }
	}
}
