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

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;
using KyoeiSystem.Application.Windows.Views;
using GrapeCity.Windows.SpreadGrid;
using KyoeiSystem.Framework.Windows.Controls;

namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
	/// 売上明細問合せ
	/// </summary>
    public partial class DLY14010 : RibbonWindowViewBase
	{

        public class DLY14010_Member : INotifyPropertyChanged
        {
            private int _明細番号;
			private int _明細行;
			private int? _明細区分;
			private DateTime? _日付;
			private string _経費先名;
			private int? _車輌ID;
			private string _車輌番号;
			private string _乗務員名;
			private int? _メーター { get; set; }
			private int? _乗務員ID;
			private int? _支払先ID;
			private int? _自社部門ID;
			private int? _経費項目ID;
			private string _経費項目;
			private string _経費補助名称;
			private decimal? _単価;
			private decimal? _内軽油税分;
			private decimal _d内軽油税分;
			private decimal? _数量;
			private decimal _d数量;
			private decimal? _金額;
			private decimal _d金額;
			private decimal? _収支区分;
			private decimal? _摘要ID;
			private string _摘要名;
			private decimal? _入力者ID;
			private DateTime? _日付From;
			private DateTime? _日付To;
			private Int32? _経費先指定ID;
			private string _経費先指定名;
			private string _経費項目指定;
			private string _入力区分;


            public int 明細番号 { get { return _明細番号; } set { _明細番号 = value; NotifyPropertyChanged(); } }
            public int 明細行 { get { return _明細行; } set { _明細行 = value; NotifyPropertyChanged(); } }
            public int? 明細区分 { get { return _明細区分; } set { _明細区分 = value; NotifyPropertyChanged(); } }
            public DateTime? 日付 { get { return _日付; } set { _日付 = value; NotifyPropertyChanged(); } }
            public string 経費先名 { get { return _経費先名; } set { _経費先名 = value; NotifyPropertyChanged(); } }
            public int? 車輌ID { get { return _車輌ID; } set { _車輌ID = value; NotifyPropertyChanged(); } }
            public string 車輌番号 { get { return _車輌番号; } set { _車輌番号 = value; NotifyPropertyChanged(); } }
            public string 乗務員名 { get { return _乗務員名; } set { _乗務員名 = value; NotifyPropertyChanged(); } }
            public int? メーター { get { return _メーター; } set { _メーター = value; NotifyPropertyChanged(); } }
            public int? 乗務員ID { get { return _乗務員ID; } set { _乗務員ID = value; NotifyPropertyChanged(); } }
            public int? 支払先ID { get { return _支払先ID; } set { _支払先ID = value; NotifyPropertyChanged(); } }
            public int? 自社部門ID { get { return _自社部門ID; } set { _自社部門ID = value; NotifyPropertyChanged(); } }
            public int? 経費項目ID { get { return _経費項目ID; } set { _経費項目ID = value; NotifyPropertyChanged(); } }
            public string 経費項目 { get { return _経費項目; } set { _経費項目 = value; NotifyPropertyChanged(); } }
            public string 経費補助名称 { get { return _経費補助名称; } set { _経費補助名称 = value; NotifyPropertyChanged(); } }
            public decimal? 単価 { get { return _単価; } set { _単価 = value; NotifyPropertyChanged(); } }
            public decimal? 内軽油税分 { get { return _内軽油税分; } set { _内軽油税分 = value; NotifyPropertyChanged(); } }
            public decimal d内軽油税分 { get { return _d内軽油税分; } set { _d内軽油税分 = value; NotifyPropertyChanged(); } }
            public decimal? 数量 { get { return _数量; } set { _数量 = value; NotifyPropertyChanged(); } }
            public decimal d数量 { get { return _d数量; } set { _d数量 = value; NotifyPropertyChanged(); } }
            public decimal? 金額 { get { return _金額; } set { _金額 = value; NotifyPropertyChanged(); } }
            public decimal d金額 { get { return _d金額; } set { _d金額 = value; NotifyPropertyChanged(); } }
            public decimal? 収支区分 { get { return _収支区分; } set { _収支区分 = value; NotifyPropertyChanged(); } }
            public decimal? 摘要ID { get { return _摘要ID; } set { _摘要ID = value; NotifyPropertyChanged(); } }
            public string 摘要名 { get { return _摘要名; } set { _摘要名 = value; NotifyPropertyChanged(); } }
            public decimal? 入力者ID { get { return _入力者ID; } set { _入力者ID = value; NotifyPropertyChanged(); } }
            public DateTime? 日付From { get { return _日付From; } set { _日付From = value; NotifyPropertyChanged(); } }
            public DateTime? 日付To { get { return _日付To; } set { _日付To = value; NotifyPropertyChanged(); } }
            public Int32? 経費先指定ID { get { return _経費先指定ID; } set { _経費先指定ID = value; NotifyPropertyChanged(); } }
            public string 経費先指定名 { get { return _経費先指定名; } set { _経費先指定名 = value; NotifyPropertyChanged(); } }
            public string 経費項目指定 { get { return _経費項目指定; } set { _経費項目指定 = value; NotifyPropertyChanged(); } }
            public string 入力区分 { get { return _入力区分; } set { _入力区分 = value; NotifyPropertyChanged(); } }


            #region INotifyPropertyChanged メンバー
            /// <summary>
            /// Binding機能対応（プロパティの変更通知イベント）
            /// </summary>
            public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
            /// <summary>
            /// Binding機能対応（プロパティの変更通知イベント送信）
            /// </summary>
            /// <param name="propertyName">Bindingプロパティ名</param>
            public void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
                }
            }
            #endregion

        }

		const string SelectedChar = "a";
        const string UnselectedChar = "";
        const string ReportFileName = @"Files\DLY\DLY14010.rpt";

        #region データアクセス用ID
        private const string GET_DATA = "DLY14010";
        private const string GET_DATA_PRT = "DLY14010_PRT";
        private const string GET_DATA_CSV = "DLY14010_CSV";
        private const string UPDATE_ROW = "DLY14010_UPDATE";
        //レポート
        private const string rptFullPathName_PIC = @"Files\DLY\DLY14010.rpt";
		#endregion

        // SPREADのCELLに移動したとき入力前に表示されていた文字列保存用
        string _originalText = null;

		#region 画面設定項目
		/// <summary>
		/// ユーザ設定項目
		/// </summary>
		UserConfig ucfg = null;

		/// <summary>
		/// 画面固有設定項目のクラス定義
		/// ※ 必ず public で定義する。
		/// </summary>
		public class ConfigDLY14010 : FormConfigBase
		{
			public string[] 表示順 { get; set; }
			public bool[] 表示順方向 { get; set; }
			// コンボボックスの位置
			public int 自社部門index { get; set; }

			public byte[] spConfig20180118 = null;

            public string 作成年 { get; set; }
            public string 作成月 { get; set; }
            public string 締日 { get; set; }
            public string 集計期間From { get; set; }
            public string 集計期間To { get; set; }
            public int 区分1 { get; set; }
            public int 区分2 { get; set; }
            public int 区分3 { get; set; }
            public int 区分4 { get; set; }
            public int 区分5 { get; set; }
            public bool? チェック { get; set; }
		}

		/// ※ 必ず public で定義する。
		public ConfigDLY14010 frmcfg = null;

		public byte[] sp_Config = null;

		#endregion

		#region 明細クリック時のアクション定義
		/// <summary>
		/// 明細クリック時のアクション定義
		/// </summary>
		public class cmd売上詳細表示 : ICommand
		{
			private GcSpreadGrid _gcSpreadGrid;
			public cmd売上詳細表示(GcSpreadGrid gcSpreadGrid)
			{
				this._gcSpreadGrid = gcSpreadGrid;
			}
			public bool CanExecute(object parameter)
			{
				return true;
			}
			public event EventHandler CanExecuteChanged;
			public void OnCanExecuteChanged()
			{
				if (CanExecuteChanged != null)
					CanExecuteChanged(this, EventArgs.Empty);
			}
			public void Execute(object parameter)
			{
                CellCommandParameter cellCommandParameter = (CellCommandParameter)parameter;
                if (cellCommandParameter.Area == SpreadArea.Cells)
                {
                    int rowNo = cellCommandParameter.CellPosition.Row;
                    var row = this._gcSpreadGrid.Rows[rowNo];
                    var mNo = row.Cells[this._gcSpreadGrid.Columns["明細番号"].Index].Value;
                    var gNo = row.Cells[this._gcSpreadGrid.Columns["明細行"].Index].Value;
                    var wnd = GetWindow(this._gcSpreadGrid);
                    
                    // 入力区分により起動する画面を分けるとしたらココ
                    var kbn = row.Cells[this._gcSpreadGrid.Columns["入力区分"].Index].Value;

                        DLY07010 frm = new DLY07010();
                        frm.初期明細番号 = (int?)mNo;
                        frm.初期行番号 = (int?)gNo;
						frm.ShowDialog(wnd);
						if (frm.IsUpdated)
						{
							// 日報側で更新された場合、再検索を実行する
							var parent = ViewBaseCommon.FindVisualParent<DLY14010>(this._gcSpreadGrid);
							parent.Button_Click_1(null, null);
						}



                }
			}
		}
		#endregion


		/// <summary>
		///　メンバー変数
		/// </summary>
		#region Member

		//private UcLabelComboBox[] orderComboboxes = new UcLabelComboBox[] { null, null, null, null, null };

		#endregion


        #region BindingMember

        private int? _乗務員ID = null;
        public int? 乗務員ID
        {
            set
            {
                _乗務員ID = value;
                NotifyPropertyChanged();
            }
            get { return _乗務員ID; }
        }

        private int? _車輌ID = null;
        public int? 車輌ID
        {
            set
            {
                _車輌ID = value;
                NotifyPropertyChanged();
            }
            get { return _車輌ID; }
        }

        private string _担当者ID = null;
        public string 担当者ID
        {
            set { _担当者ID = value; NotifyPropertyChanged(); }
            get { return _担当者ID; }
        }

		bool _請求内訳条件Enabled = false;
		public bool 請求内訳条件Enabled
		{
			get { return this._請求内訳条件Enabled; }
			set { this._請求内訳条件Enabled = value; NotifyPropertyChanged(); }
		}

		string _検索日付選択 = null;
		public string 検索日付選択
		{
			get { return this._検索日付選択; }
			set { this._検索日付選択 = value; NotifyPropertyChanged(); }
		}

		string _検索日付From = null;
		public string 検索日付From
		{
			get { return this._検索日付From; }
			set { this._検索日付From = value; NotifyPropertyChanged(); }
		}

		string _検索日付To = null;
		public string 検索日付To
		{
			get { return this._検索日付To; }
			set { this._検索日付To = value; NotifyPropertyChanged(); }
		}

		string _入力区分 = string.Empty;
		public string 入力区分
		{
			get { return this._入力区分; }
			set { this._入力区分 = value; NotifyPropertyChanged(); }
		}

		string _経費補助名称 = string.Empty;
		public string 経費補助名称
		{
			get { return this._経費補助名称; }
			set { this._経費補助名称 = value; NotifyPropertyChanged(); }
		}

		string _摘要 = string.Empty;
		public string 摘要
		{
			get { return this._摘要; }
			set { this._摘要 = value; NotifyPropertyChanged(); }
		}


		//string _検索ボタンラベル = "検 索";
		//public string 検索ボタンラベル
		//{
		//	get { return this._検索ボタンラベル; }
		//	set
		//	{
		//		this._検索ボタンラベル = value;
		//		NotifyPropertyChanged();
		//	}
		//}

		string[] _表示順 = new string[] { "", "", "", "", "", };
		public string[] 表示順
		{
			get { return this._表示順; }
			set { this._表示順 = value; NotifyPropertyChanged(); }
		}

		string[] _表示順名 = new string[] { "", "", "", "", "", };
		public string[] 表示順名
		{
			get { return this._表示順名; }
			set { this._表示順名 = value; NotifyPropertyChanged(); }
		}

		bool[] _表示順方向 = new bool[] { false, false, false, false, false };
		public bool[] 表示順方向
		{
			get { return this._表示順方向; }
			set { this._表示順方向 = value; NotifyPropertyChanged(); }
		}

        private string _得意先ID = null;
        public string 得意先ID
        {
            set
            {
                _得意先ID = value;
                NotifyPropertyChanged();
            }
            get { return _得意先ID; }
        }

        private string _得意先名 = null;
        public string 得意先名
        {
            set
            {
                _得意先名 = value;
                NotifyPropertyChanged();
            }
            get { return _得意先名; }
        }

		private string _請求内訳ID = null;
		public string 請求内訳ID
        {
            set
            {
                _請求内訳ID = value;
                NotifyPropertyChanged();
            }
            get { return _請求内訳ID; }
        }

		private string _支払先ID = null;
		public string 支払先ID
        {
            set
            {
                _支払先ID = value;
                NotifyPropertyChanged();
            }
            get { return _支払先ID; }
        }

        private string _経費項目ID = null;
        public string 経費項目ID
        {
            set
            {
                _経費項目ID = value;
                NotifyPropertyChanged();
            }
            get { return _経費項目ID; }
        }

        private string _経費項目指定 = null;
        public string 経費項目指定
        {
            set
            {
                _経費項目指定 = value;
                NotifyPropertyChanged();
            }
            get { return _経費項目指定; }
        }

        

        private string _発地名 = "";
        public string 発地名
        {
            set
            {
                _発地名 = value;
                NotifyPropertyChanged();
            }
            get { return _発地名; }
        }
        private string _着地名 = "";
        public string 着地名
        {
            set
            {
                _着地名 = value;
                NotifyPropertyChanged();
            }
            get { return _着地名; }
        }

        private string _商品名 = "";
        public string 商品名
        {
            set
            {
                _商品名 = value;
                NotifyPropertyChanged();
            }
            get { return _商品名; }
        }

        private string _請求摘要 = "";
        public string 請求摘要
        {
            set
            {
                _請求摘要 = value;
                NotifyPropertyChanged();
            }
            get { return _請求摘要; }
        }

        private string _社内備考 = "";
        public string 社内備考
        {
            set
            {
                _社内備考 = value;
                NotifyPropertyChanged();
            }
            get { return _社内備考; }
        }
        
        private int? _売上未定区分 = null;
        public int? 売上未定区分
        {
            set
            {
                _売上未定区分 = value;
                NotifyPropertyChanged();
            }
            get { return _売上未定区分; }
        }


		// 検索した結果データ
        //private DataTable _dUriageDataResult = null;
        //public DataTable 経費明細データ検索結果
        //{
        //    get
        //    {
        //        return this._dUriageDataResult;
        //    }
        //    set
        //    {
        //        this._dUriageDataResult = value;
        //        if (value == null)
        //        {
        //            this.経費明細データ = null;
        //        }
        //        else
        //        {
        //            this.経費明細データ = value.Copy();
        //        }
        //        NotifyPropertyChanged();
        //        //NotifyPropertyChanged("経費明細データ");
        //    }
        //}

		private List<DLY14010_Member> _dUriageData = null;
        public List<DLY14010_Member> 経費明細データ
		{
			get
			{
				return this._dUriageData;
			}
			set
			{
				this._dUriageData = value;
	            this.sp経費明細データ.ItemsSource = value;
				NotifyPropertyChanged();
			}
		}

		private decimal _金額合計 = 0;
		public decimal 金額合計
		{
			get { return _金額合計; }
			set { _金額合計 = value; NotifyPropertyChanged(); }
		}

		private decimal _数量合計 = 0;
		public decimal 数量合計
		{
			get { return _数量合計; }
			set { _数量合計 = value; NotifyPropertyChanged(); }
		}


		private bool _isExpanded = false;
		public bool IsExpanded
		{
			get { return _isExpanded; }
			set { _isExpanded = value; NotifyPropertyChanged(); }
		}

		private string _表示固定列数 = "5";
		public string 表示固定列数
		{
			get { return _表示固定列数; }
			set { _表示固定列数 = value; NotifyPropertyChanged(); SetupSpreadFixedColumn(this.sp経費明細データ, value); }
		}

        private int _取引区分 = 4;
        public int 取引区分
        {
            get { return _取引区分; }
            set { _取引区分 = value; NotifyPropertyChanged(); }
        }

        #endregion

		/// <summary>
		/// 売上明細問合せ
		/// </summary>
		public DLY14010()
		{
			InitializeComponent();
			this.DataContext = this;
		}


		/// <summary>
		/// 画面読み込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			// 初期状態を保存（SPREADリセット時にのみ使用する）
			this.sp_Config = AppCommon.SaveSpConfig(this.sp経費明細データ);

			// コンテキストメニューの作成

			base.MasterMaintenanceWindowList.Add("M01_TOK_TOKU_SCH", new List<Type> { null, typeof(SCH01010) });
			base.MasterMaintenanceWindowList.Add("M01_TOK_SHIHARAI_SCH", new List<Type> { null, typeof(SCH01010) });
            base.MasterMaintenanceWindowList.Add("M01_ZEN_SHIHARAI", new List<Type> { null, typeof(SCH01010) });
			base.MasterMaintenanceWindowList.Add("M10_UHK_UC", new List<Type> { null, typeof(SCH02020) });
            base.MasterMaintenanceWindowList.Add("M72_TNT", new List<Type> { null, typeof(SCH23010) });
            base.MasterMaintenanceWindowList.Add("M04_DRV", new List<Type> { null, typeof(SCH04010) });
            base.MasterMaintenanceWindowList.Add("M05_CAR", new List<Type> { null, typeof(SCH06010) });

			#region 設定項目取得
			ucfg = AppCommon.GetConfig(this);
			frmcfg = (ConfigDLY14010)ucfg.GetConfigValue(typeof(ConfigDLY14010));
			if (frmcfg == null)
			{
                frmcfg = new ConfigDLY14010();
                ucfg.SetConfigValue(frmcfg);
                frmcfg.spConfig20180118 = this.sp_Config;
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
				this.Width = frmcfg.Width;
				this.Height = frmcfg.Height;
                this.cmb経費指定.SelectedIndex = frmcfg.区分1;
                this.検索日付From = frmcfg.集計期間From;
                this.検索日付To = frmcfg.集計期間To;

			}
			#endregion

			AppCommon.LoadSpConfig(this.sp経費明細データ, frmcfg.spConfig20180118 != null ? frmcfg.spConfig20180118 : this.sp_Config);


			//ComboBoxに値を設定する
            GetComboBoxItems();

			sp経費明細データ.RowCount = 0;


			if (frmcfg.表示順 != null)
			{
				if (frmcfg.表示順.Length == 5)
				{
					this.表示順 = frmcfg.表示順;
				}
			}
			if (frmcfg.表示順方向 != null)
			{
				if (frmcfg.表示順方向.Length == 5)
				{
					this.表示順方向 = frmcfg.表示順方向;
				}
			}

			ButtonCellType btn = this.sp経費明細データ.Columns[0].CellType as ButtonCellType;
			btn.Command = new cmd売上詳細表示(sp経費明細データ);


			this.textbox検索日付From.SetFocus();

		}

		/// <summary>
		/// コンボボックスのアイテムをDBから取得
		/// </summary>
		private void GetComboBoxItems()
		{
			//orderComboboxes[0] = this.cmb表示順指定0;
			//orderComboboxes[1] = this.cmb表示順指定1;
			//orderComboboxes[2] = this.cmb表示順指定2;
			//orderComboboxes[3] = this.cmb表示順指定3;
			//orderComboboxes[4] = this.cmb表示順指定4;

			//AppCommon.SetutpComboboxList(this.cmb検索日付種類, false);
			//AppCommon.SetutpComboboxList(this.cmb部門指定, false);

			AppCommon.SetutpComboboxList(this.cmb表示順指定0, false);
			AppCommon.SetutpComboboxList(this.cmb表示順指定1, false);
			AppCommon.SetutpComboboxList(this.cmb表示順指定2, false);
			AppCommon.SetutpComboboxList(this.cmb表示順指定3, false);
			AppCommon.SetutpComboboxList(this.cmb表示順指定4, false);
			//this.cmb表示順指定0.SelectionChanged += cmb表示順指定_SelectionChanged;
			//this.cmb表示順指定1.SelectionChanged += cmb表示順指定_SelectionChanged;
			//this.cmb表示順指定2.SelectionChanged += cmb表示順指定_SelectionChanged;
			//this.cmb表示順指定3.SelectionChanged += cmb表示順指定_SelectionChanged;
			//this.cmb表示順指定4.SelectionChanged += cmb表示順指定_SelectionChanged;

		}

		/// <summary>
		/// ピックアップ指定のGridの読み込み
		/// </summary>
		private void GetPickupCodeList()
		{
			try
			{
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        public override void OnReveivedError(CommunicationObject message)
        {
			base.OnReveivedError(message);
			this.Message = base.ErrorMessage;
			base.SetFreeForInput();
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
                case GET_DATA:
                    base.SetFreeForInput();
                    var ds = data as DataSet;
                    if (ds == null)
                    {
                        this.経費明細データ = null;
                    }
                    else
                    {
                        経費明細データ = (List<DLY14010_Member>)AppCommon.ConvertFromDataTable(typeof(List<DLY14010_Member>), ds.Tables["DataList"]);

                        if (this.経費明細データ.Count == 0)
                        {
                            Summary();
                            this.ErrorMessage = "該当するデータはありません。";
                            return;
                        }

                        DataReSort();
                        Summary();
                        textbox検索日付From.Focus();
                    }
                    break;
                case GET_DATA_PRT:
                    DispPreviw(tbl);
                    break;

                case GET_DATA_CSV:
                    OutPutCSV(tbl);
                    break;
				case UPDATE_ROW: 
					if (CloseFlg) { CloseFlg = false; this.Close(); }
					break;


			}

        }

		/// <summary>
		/// F1 マスタ検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF1Key(object sender, KeyEventArgs e)
		{
			try
			{
				ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);
			}
			catch (Exception ex)
			{
				appLog.Error("検索画面起動エラー", ex);
				this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";
			}
		}

		/// <summary>
		/// 詳細表示
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF4Key(object sender, KeyEventArgs e)
		{
            if (this.sp経費明細データ.ActiveCellPosition.Row < 0)
            {
                MessageBox.Show("検索データがありません。");
                return;
            }
			DisplayDetail();
		}

		#region CSVファイル出力
		/// <summary>
		/// CSV出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF5Key(object sender, KeyEventArgs e)
        {

            DateTime? p検索日付From = null;
            DateTime? p検索日付To = null;
            int p検索日付区分 = 0;
            int? 得意先ID = null;
            int? 支払先ID = null;
            int? 請求内訳ID = null;
            int p経費項目ID = 0;

            int iwk;
            if (int.TryParse(this.得意先ID, out iwk) == true)
            {
                得意先ID = iwk;
            }
            if (int.TryParse(this.支払先ID, out iwk) == true)
            {
                支払先ID = iwk;
            }
            if (int.TryParse(this.請求内訳ID, out iwk) == true)
            {
                請求内訳ID = iwk;
            }
            if (int.TryParse(this.経費項目ID, out p経費項目ID) != true)
            {
                p経費項目ID = 0;
            }

            if (int.TryParse(this.検索日付選択, out p検索日付区分) != true)
            {
                p検索日付区分 = 0;
            }
            DateTime dtwk;
            if (DateTime.TryParse(this.検索日付From, out dtwk) == true)
            {
                p検索日付From = dtwk;
            }
            if (DateTime.TryParse(this.検索日付To, out dtwk) == true)
            {
                p検索日付To = dtwk;
            }


			CommunicationObject com
				= new CommunicationObject(MessageType.RequestData, "DLY14010_CSV", 担当者ID, 乗務員ID, 車輌ID,
					得意先ID, 得意先名, 支払先ID, 請求内訳ID, p検索日付From, p検索日付To, p検索日付区分, p経費項目ID, this.売上未定区分
					, 商品名, 発地名, 着地名, 請求摘要, 社内備考, 経費項目指定, 経費補助名称, 摘要
					);


            base.SendRequest(com);
		}
		#endregion

		/// <summary>
		/// 印刷
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF8Key(object sender, KeyEventArgs e)
		{
            PrintOut();

		}

        void PrintOut()
		{
			PrinterDriver ret = AppCommon.GetPrinter(frmcfg.PrinterName);
			if (ret.Result == false)
			{
				this.ErrorMessage = "プリンタドライバーがインストールされていません！";
				return;
			}
			frmcfg.PrinterName = ret.PrinterName;

            if (this.経費明細データ == null)
			{
				this.ErrorMessage = "印刷データがありません。";
                return;
            }
            if (this.経費明細データ.Count == 0)
            {
                this.ErrorMessage = "印刷データがありません。";
                return;
            }
            try
            {
                base.SetBusyForInput();
                var parms = new List<Framework.Reports.Preview.ReportParameter>()
				{
					new Framework.Reports.Preview.ReportParameter(){ PNAME="日付区分", VALUE=(this.cmb経費指定.Text==null?"":this.cmb経費指定.Text)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="日付FROM", VALUE=(this.検索日付From==null?"":this.検索日付From)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="日付TO", VALUE=(this.検索日付To==null?"":this.検索日付To)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="経費先指定", VALUE=(this.コード指定.Text2==null?"":this.コード指定.Text2)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="経費項目指定", VALUE=(this.cmb経費指定.Text==null?"":this.cmb経費指定.Text)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="表示順序", VALUE=string.Format("{0} {1} {2} {3} {4}", 表示順名[0], 表示順名[1], 表示順名[2], 表示順名[3], 表示順名[4])},
				};
                KyoeiSystem.Framework.Reports.Preview.ReportPreview view = null;

				DataTable 印刷データ = new DataTable("経費伝票一覧");
                //リストをデータテーブルへ
                //AppCommon.ConvertToDataTable(経費明細データ, 印刷データ);


				Dictionary<string, string> changecols = new Dictionary<string, string>()
				{
					{ "d金額", "金額" },
					{ "d数量", "数量" },
				};
				AppCommon.ConvertSpreadDataToTable<DLY14010_Member>(this.sp経費明細データ, 印刷データ, changecols);


				view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
				view.MakeReport(印刷データ.TableName, ReportFileName, 0, 0, 0);
				view.SetReportData(印刷データ);

				view.SetupParmeters(parms);

				base.SetFreeForInput();

				view.PrinterName = frmcfg.PrinterName;
				view.ShowPreview();
				view.Close();
				frmcfg.PrinterName = view.PrinterName;

            }
            catch (Exception ex)
            {
                base.SetFreeForInput();
                this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
                appLog.Error("得意先売上明細書の印刷時に例外が発生しました。", ex);
            }
        }

		/// <summary>
		/// F11　リボン終了
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF11Key(object sender, KeyEventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// 検索ボタン クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
            if (!base.CheckAllValidation())
            {
                this.ErrorMessage = "入力内容に誤りがあります。";
                MessageBox.Show("入力内容に誤りがあります。");
                return;
			}
			sp経費明細データ.FilterDescriptions.Clear();

            if (ExpSyousai.IsExpanded == true)
            {
                ExpSyousai.IsExpanded = false;
            }

			DateTime? p検索日付From = null;
			DateTime? p検索日付To = null;
			int p検索日付区分 = 0;
			int? 得意先ID = null;
            int? 担当者ID = null;
			int? 支払先ID = null;
			int? 請求内訳ID = null;
            int p経費項目ID = 0;

			int iwk;
			if (int.TryParse(this.得意先ID, out iwk) == true)
			{
				得意先ID = iwk;
			}
            if (int.TryParse(this.担当者ID, out iwk) == true)
            {
                担当者ID = iwk;
            }
			if (int.TryParse(this.支払先ID, out iwk) == true)
			{
				支払先ID = iwk;
			}
			if (int.TryParse(this.請求内訳ID, out iwk) == true)
			{
				請求内訳ID = iwk;
			}
            if (int.TryParse(this.経費項目ID, out p経費項目ID) != true)
			{
                p経費項目ID = 0;
			}

			if (int.TryParse(this.検索日付選択, out p検索日付区分) != true)
			{
				p検索日付区分 = 0;
			}
			DateTime dtwk;
			if (DateTime.TryParse(this.検索日付From, out dtwk) == true)
			{
				p検索日付From = dtwk;
			}
			if (DateTime.TryParse(this.検索日付To, out dtwk) == true)
			{
				p検索日付To = dtwk;
			}


			CommunicationObject com
				= new CommunicationObject(MessageType.RequestData, "DLY14010", 担当者ID, 乗務員ID, 車輌ID,
                    得意先ID, 得意先名, 支払先ID, 請求内訳ID, p検索日付From, p検索日付To, p検索日付区分, p経費項目ID, this.売上未定区分
                    , 商品名, 発地名, 着地名, 請求摘要, 社内備考, 経費項目指定, 経費補助名称, 摘要
					);

			base.SendRequest(com);
			base.SetBusyForInput();
		}

		public override void OnF10Key(object sender, KeyEventArgs e)
		{
			base.OnF10Key(sender, e);
		}

		private void cmb表示順指定_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int sel = (sender as UcLabelComboBox).SelectedIndex;
			int cnt = 0;
			// 同じ項目を2回以上指定できないようにする
			//foreach (var cmb in this.orderComboboxes)
			foreach (var cmb in new UcLabelComboBox[] { this.cmb表示順指定0, this.cmb表示順指定1, this.cmb表示順指定2, this.cmb表示順指定3, this.cmb表示順指定4, })
			{
				if (cmb.SelectedIndex < 1)
				{
					continue;
				}
				if (cmb.SelectedIndex == sel)
				{
					cnt++;
					if (cnt > 1)
					{
						MessageBox.Show("既に指定されています。");
						(sender as UcLabelComboBox).SelectedIndex = 0;
						return;
					}
				}
			}
		}

		private void SortButton_Click(object sender, RoutedEventArgs e)
		{
			DataReSort();
		}


		void DataReSort()
        {
            if (sp経費明細データ == null)
            {
                return;
            }
            if (sp経費明細データ.Rows.Count == 0)
            {
                return;
            }
            this.sp経費明細データ.SortDescriptions.Clear();
            UcLabelComboBox[] cmblist = { this.cmb表示順指定0, this.cmb表示順指定1, this.cmb表示順指定2, this.cmb表示順指定3, this.cmb表示順指定4, };
            int ix = -1;
            foreach (var cmb in cmblist)
            {
                ix++;
                CodeData cd = cmb.Combo_SelectedItem as CodeData;
                if (cd == null)
                {
                    continue;
                }
                if (cd.表示名 == "設定なし")
                {
                    continue;
                }
                try
                {
                    var sort = new SpreadSortDescription();
                    sort.Direction = (this.表示順方向[ix]) ? ListSortDirection.Descending : ListSortDirection.Ascending;
                    switch (cd.表示名)
                    {
                        case "入力順":
                            sort.ColumnName = "明細番号";
                            this.sp経費明細データ.SortDescriptions.Add(sort);
                            sort = new SpreadSortDescription();
                            sort.Direction = (this.表示順方向[ix]) ? ListSortDirection.Descending : ListSortDirection.Ascending;
                            sort.ColumnName = "明細行";
                            this.sp経費明細データ.SortDescriptions.Add(sort);
                            break;
                        case "指定日付":
                            sort.ColumnName = (cmb経費指定.Combo_SelectedItem as CodeData).表示名 + "日付";
                            this.sp経費明細データ.SortDescriptions.Add(sort);
                            break;
                        case "運行者名":
                            sort.ColumnName = "乗務員名";
                            this.sp経費明細データ.SortDescriptions.Add(sort);
                            break;
                        default:
                            sort.ColumnName = cd.表示名;
                            this.sp経費明細データ.SortDescriptions.Add(sort);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    appLog.Error("売上明細データSPREADのソート時に例外が発生しました。", ex);
                    //this.ErrorMessage = "データの並び替え中にシステムエラーが発生しました。サポートにお問い合わせください。";
                }
            }
		}


		// 合計計算
		void Summary()
		{
			金額合計 = 0m;
			数量合計 = 0m;

			if (sp経費明細データ.Columns[1].Name == null)
			{
				return;
			}

			DataTable 印刷データ = new DataTable("印刷データ");

			Dictionary<string, string> changecols = new Dictionary<string, string>()
			{
			};

			AppCommon.ConvertSpreadDataToTable<DLY14010_Member>(this.sp経費明細データ, 印刷データ, changecols);

			金額合計 = AppCommon.DecimalParse(印刷データ.Compute("Sum(d金額)", null).ToString());
			数量合計 = AppCommon.DecimalParse(印刷データ.Compute("Sum(d数量)", null).ToString());

		}

		/// <summary>
		/// 部門のコンボボックスのリスト準備完了時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmb経費指定_DataListInitialized(object sender, RoutedEventArgs e)
		{
			this.経費項目ID = "0";
            this.cmb経費指定.SelectedIndex = 0;
		}


        #region SPREAD CellEnter

        private void sp経費明細データ_CellEnter(object sender, SpreadCellEnterEventArgs e)
        {
            var grid = sender as GcSpreadGrid;
            if (grid == null) return;
            if (grid.RowCount == 0) return;
            this._originalText = grid.Cells[e.Row, e.Column].Text;
        }

        #endregion

        string CellName = string.Empty;
        string CellText = string.Empty;
        decimal Cell金額 = 0;
        decimal Cell数量 = 0;


        #region SPREAD CellEditEnding

        private void sp経費明細データ_CellEditEnding(object sender, SpreadCellEditEndingEventArgs e)
        {
            if (e.EditAction == SpreadEditAction.Cancel)
            {
                return;
            }
            CellName = e.CellPosition.ColumnName;
            CellText = sp経費明細データ.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;

            Cell金額 = AppCommon.DecimalParse(sp経費明細データ.Cells[e.CellPosition.Row, "d金額"].Text);
            Cell数量 = AppCommon.DecimalParse(sp経費明細データ.Cells[e.CellPosition.Row, "d数量"].Text);

			//スプレッドコンボイベント関連付け解除			
			if (sp経費明細データ[e.CellPosition].InheritedCellType is GrapeCity.Windows.SpreadGrid.ComboBoxCellType)
			{
				GrapeCity.Windows.SpreadGrid.Editors.GcComboBox gccmb = sp経費明細データ.EditElement as GrapeCity.Windows.SpreadGrid.Editors.GcComboBox;
				if (gccmb != null)
				{
					gccmb.SelectionChanged -= comboEdit_SelectionChanged;
				}
			}

			if (sp経費明細データ[e.CellPosition].InheritedCellType is GrapeCity.Windows.SpreadGrid.CheckBoxCellType)
			{
				GrapeCity.Windows.SpreadGrid.Editors.CheckBoxEditElement gcchk = sp経費明細データ.EditElement as GrapeCity.Windows.SpreadGrid.Editors.CheckBoxEditElement;
				if (gcchk != null)
				{
					gcchk.Checked -= checkEdit_Checked;
					gcchk.Unchecked -= checkEdit_Unchecked;
				}
			}			

        }

        #endregion


		/// <summary>				
		/// comboEdit_SelectionChanged				
		/// スプレッドコンボリストチェンジイベント				
		/// </summary>				
		/// <param name="sender"></param>				
		/// <param name="e"></param>				
		private void comboEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sp経費明細データ.ActiveCell.IsEditing)
			{
				sp経費明細データ.CommitCellEdit();
			}
		}

		private void checkEdit_Checked(object sender, RoutedEventArgs e)
		{
			sp経費明細データ.CommitCellEdit();
		}

		private void checkEdit_Unchecked(object sender, RoutedEventArgs e)
		{
			sp経費明細データ.CommitCellEdit();
		}	


		private void sp経費明細データ_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
		{
			if (e.EditAction == SpreadEditAction.Cancel)
			{
				return;
			}
			var gcsp = (sender as GcSpreadGrid);
			if (gcsp == null)
			{
				return;
			}

			try
            {
                string cname = e.CellPosition.ColumnName;
                string ctext = sp経費明細データ.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
                ctext = ctext == null ? string.Empty : ctext;
				if (cname == CellName && ctext == CellText && (!(gcsp[e.CellPosition].InheritedCellType is GrapeCity.Windows.SpreadGrid.ComboBoxCellType) && !(gcsp[e.CellPosition].InheritedCellType is GrapeCity.Windows.SpreadGrid.CheckBoxCellType)))
				{
					if (CloseFlg) { CloseFlg = false; }
					// セルの値が変化していなければ何もしない	
					return;
				}		


                var row = gcsp.Rows[e.CellPosition.Row];
                object val = row.Cells[e.CellPosition.Column].Value;
                val = val == null ? "" : val;
                if (cname.Contains("年月日") == true)
                {
                    AppCommon.SpreadYMDCellCheck(sender, e, this._originalText);
                    cname = cname.Replace("年月日", "日付");
                    DateTime dt;
                    if (DateTime.TryParse(row.Cells[e.CellPosition.Column].Text, out dt) == true)
                    {
                        val = dt;
                    }
                    else
                    {
                        this.ErrorMessage = "正しい日付を入力してください。";
                        return;
                    }
                }

				var colM = gcsp.Columns.Where(x => x.Name == "明細番号").FirstOrDefault();
				if (colM == null)
				{
					throw new Exception("システムエラー");
				}
				var colL = gcsp.Columns.Where(x => x.Name == "明細行").FirstOrDefault();
				if (colL == null)
				{
					throw new Exception("システムエラー");
				}

                string 項目名 = e.CellPosition.ColumnName;
                if (!String.IsNullOrEmpty(e.CellPosition.ColumnName))
                {
                    if (e.CellPosition.ColumnName[0].ToString() == "d")
                    {
                        項目名 = e.CellPosition.ColumnName.Substring(1);
                    };
                }

                base.SendRequest(new CommunicationObject(MessageType.UpdateData, UPDATE_ROW, row.Cells[colM.Index].Value, row.Cells[colL.Index].Value, 項目名, val));
                //Button_Click_1(null, null);

                金額合計 += row.Cells[this.sp経費明細データ.Columns["d金額"].Index].Value == null ? 0 : AppCommon.DecimalParse(row.Cells[this.sp経費明細データ.Columns["d金額"].Index].Value.ToString()) - Cell金額;
                数量合計 += row.Cells[this.sp経費明細データ.Columns["d数量"].Index].Value == null ? 0 : AppCommon.DecimalParse(row.Cells[this.sp経費明細データ.Columns["d数量"].Index].Value.ToString()) - Cell数量;
            }
			catch (Exception ex)
			{
				this.ErrorMessage = "入力内容が不正です。";
			}
		}

		private void 得意先指定_LostFocus(object sender, RoutedEventArgs e)
		{
			if (IsExpanded)
			{
                btnKensaku.Focus();
			}
			else
            {
                btnKensaku.Focus();
			}
		}

		/// <summary>
		/// 締日の入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PickUpSime_cTextChanged(object sender, RoutedEventArgs e)
		{
			var txtbx = (sender as UcLabelTextBox);
			if (string.IsNullOrWhiteSpace(txtbx.Text))
			{
				return;
			}
			txtbx.CheckValidation();
		}

		private void PickUpSime_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter || e.Key == Key.Tab)
			{
				e.Handled = true;
			}
		}

		private void 得意先指定_TextChanged(object sender, RoutedEventArgs e)
		{
			this.請求内訳ID = string.Empty;
		}

		#region Window_Closed
		//画面が閉じられた時、データを保持する
		private void Window_Closed(object sender, EventArgs e)
		{
			this.sp経費明細データ.InputBindings.Clear();
			this.経費明細データ = null;

			if (ucfg != null)
			{
				if (frmcfg == null) { frmcfg = new ConfigDLY14010(); }
				frmcfg.Top = this.Top;
				frmcfg.Left = this.Left;
				frmcfg.Width = this.Width;
				frmcfg.Height = this.Height;
				frmcfg.表示順 = this.表示順;
				frmcfg.表示順方向 = this.表示順方向;
                frmcfg.区分1 = this.cmb経費指定.SelectedIndex;
                frmcfg.集計期間From = this.検索日付From;
                frmcfg.集計期間To = this.検索日付To;
				this.経費明細データ = null;
				frmcfg.spConfig20180118 = AppCommon.SaveSpConfig(this.sp経費明細データ);

				ucfg.SetConfigValue(frmcfg);
			}
		}
		#endregion


		private void ColumnResert_Click(object sender, RoutedEventArgs e)
		{
			AppCommon.LoadSpConfig(this.sp経費明細データ, this.sp_Config);
			DataReSort();
			this.表示固定列数 = this.sp経費明細データ.FrozenColumnCount.ToString();
			数量合計 = 0;
			金額合計 = 0;
		}

		private void SetupSpreadFixedColumn(GcSpreadGrid gcsp, string colNum)
		{
			if (string.IsNullOrWhiteSpace(colNum))
			{
				return;
			}
			int cno;
			if (int.TryParse(colNum, out cno) != true)
			{
				return;
			}
			if (cno < 1)
			{
				return;
			}
			gcsp.FrozenColumnCount = cno;
		}

		private void DisplayDetail()
		{
			int rowNo = this.sp経費明細データ.ActiveCellPosition.Row;
			var row = this.sp経費明細データ.Rows[rowNo];
			var mNo = row.Cells[sp経費明細データ.Columns["明細番号"].Index].Value;
			var gNo = row.Cells[sp経費明細データ.Columns["明細行"].Index].Value;
            DLY07010 frm = new DLY07010();
			frm.初期明細番号 = (int?)mNo;
			frm.初期行番号 = (int?)gNo;
			frm.ShowDialog(this);
			if (frm.IsUpdated)
			{
				// 日報側で更新された場合、再検索を実行する
				this.Button_Click_1(null, null);
			}
		}

        #region プレビュー画面
        /// <summary>
        /// プレビュー画面表示
        /// </summary>
        /// <param name="tbl"></param>
        private void DispPreviw(DataTable tbl)
        {
            try
            {
                if (tbl.Rows.Count < 1)
                {
                    this.ErrorMessage = "対象データが存在しません。";
                    return;
                }
                //印刷処理
                KyoeiSystem.Framework.Reports.Preview.ReportPreview view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
                //第1引数　帳票タイトル
                //第2引数　帳票ファイルPass
                //第3以上　帳票の開始点(0で良い)
                view.MakeReport("経費伝票問合せ", rptFullPathName_PIC, 0, 0, 0);
                //帳票ファイルに送るデータ。
                //帳票データの列と同じ列名を保持したDataTableを引数とする
                view.SetReportData(tbl);
				view.ShowPreview();
				view.Close();

                // 印刷した場合
                if (view.IsPrinted)
                {
                    //印刷した場合はtrueを返す
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CSVファイル出力
        /// <summary>
        /// CSVファイル出力
        /// </summary>
        /// <param name="tbl"></param>
        private void OutPutCSV(DataTable tbl)
        {
            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            //はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = @"C:\";
            //[ファイルの種類]に表示される選択肢を指定する
            sfd.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
            //「CSVファイル」が選択されているようにする
            sfd.FilterIndex = 1;
            //タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //CSVファイル出力
                CSVData.SaveCSV(tbl, sfd.FileName, true, true, false, ',');
                MessageBox.Show("CSVファイルの出力が完了しました。");
            }
        }
        #endregion

		//private void SortItemCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
		//{
		//	DataReSort();
		//}

        private void sp経費明細データ_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Delete && sp経費明細データ.EditElement == null)
			{
				e.Handled = true;
			}
			if (e.Key == Key.V && (((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) != KeyStates.Down) || ((Keyboard.GetKeyStates(Key.RightCtrl) & KeyStates.Down) != KeyStates.Down)))
			{
				e.Handled = true;
			}
        }

        private void LastField_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var ctl = sender as Framework.Windows.Controls.UcLabelTwinTextBox;
                if (ctl == null)
                {
                    return;
                }
                e.Handled = true;
                bool chk = ctl.CheckValidation();
                if (chk == true)
                {
                    Keyboard.Focus(this.btnKensaku);
                }
                else
                {
                    ctl.Focus();
                    this.ErrorMessage = ctl.GetValidationMessage();
                }
            }
        }

		private void sp経費明細データ_CellBeginEdit(object sender, SpreadCellBeginEditEventArgs e)
		{
			EditFlg = true;
		}

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			if (sp経費明細データ.ActiveCell != null && sp経費明細データ.ActiveCell.IsEditing)
			{
				CloseFlg = true;
				sp経費明細データ.CommitCellEdit();
				if (CloseFlg) { e.Cancel = true; }
				return;
			}	
		}


		/// <summary>				
		/// sp売上明細データ_EditElementShowing				
		/// スプレッドコンボイベント関連付け				
		/// デザイン画面でイベント追加				
		/// </summary>				
		/// <param name="sender"></param>				
		/// <param name="e"></param>				
		void sp経費明細データ_EditElementShowing(object sender, EditElementShowingEventArgs e)
		{
			if (e.EditElement is GrapeCity.Windows.SpreadGrid.Editors.GcComboBox)
			{
				GrapeCity.Windows.SpreadGrid.Editors.GcComboBox gccmb = e.EditElement as GrapeCity.Windows.SpreadGrid.Editors.GcComboBox;
				if (gccmb != null)
				{
					gccmb.SelectionChanged += comboEdit_SelectionChanged;
				}
			}

			if (e.EditElement is GrapeCity.Windows.SpreadGrid.Editors.CheckBoxEditElement)
			{
				GrapeCity.Windows.SpreadGrid.Editors.CheckBoxEditElement gcchk = e.EditElement as GrapeCity.Windows.SpreadGrid.Editors.CheckBoxEditElement;
				if (gcchk != null)
				{
					gcchk.Checked += checkEdit_Checked;
					gcchk.Unchecked += checkEdit_Unchecked;
				}
			}
		}

		private void sp経費明細データ_RowCollectionChanged(object sender, SpreadCollectionChangedEventArgs e)
		{
			if (sp経費明細データ.Columns[1].Name == null)
			{
				return;
			}
			if (sp経費明細データ.Rows.Count() > 0)
			{
				Summary();
			}
		}				
				



	} 


}
