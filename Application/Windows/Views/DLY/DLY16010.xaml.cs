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
    /// MCustomer.xaml の相互作用ロジック
    /// </summary>
    public partial class DLY16010 : RibbonWindowViewBase
    {

        public class DLY16010_Member : INotifyPropertyChanged
        {
            private int _明細番号;
			private int _明細行;
			private DateTime? _支払日付;
			private DateTime? _手形決済日;
			private int _支払先ID;
			private string _支払先名;
			private string _支払区分;
			private decimal _入出金金額;
			private string _摘要名;
			private DateTime? _検索日付From;
			private DateTime? _検索日付To;
			private string _検索日付選択;


            public int 明細番号 { get { return _明細番号; } set { _明細番号 = value; NotifyPropertyChanged(); } }
            public int 明細行 { get { return _明細行; } set { _明細行 = value; NotifyPropertyChanged(); } }
            public DateTime? 支払日付 { get { return _支払日付; } set { _支払日付 = value; NotifyPropertyChanged(); } }
            public DateTime? 手形決済日 { get { return _手形決済日; } set { _手形決済日 = value; NotifyPropertyChanged(); } }
            public int 支払先ID { get { return _支払先ID; } set { _支払先ID = value; NotifyPropertyChanged(); } }
            public string 支払先名 { get { return _支払先名; } set { _支払先名 = value; NotifyPropertyChanged(); } }
            public string 支払区分 { get { return _支払区分; } set { _支払区分 = value; NotifyPropertyChanged(); } }
            public decimal 入出金金額 { get { return _入出金金額; } set { _入出金金額 = value; NotifyPropertyChanged(); } }
            public string 摘要名 { get { return _摘要名; } set { _摘要名 = value; NotifyPropertyChanged(); } }
            public DateTime? 検索日付From { get { return _検索日付From; } set { _検索日付From = value; NotifyPropertyChanged(); } }
            public DateTime? 検索日付To { get { return _検索日付To; } set { _検索日付To = value; NotifyPropertyChanged(); } }
            public string 検索日付選択 { get { return _検索日付選択; } set { _検索日付選択 = value; NotifyPropertyChanged(); } }

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

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;

        /// <summary>
        /// 画面固有設定項目のクラス定義
        /// ※ 必ず public で定義する。
        /// </summary>
        public class ConfigDLY16010 : FormConfigBase
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
        public ConfigDLY16010 frmcfg = null;

        public byte[] sp_Config = null;

        #endregion

        #region Member

        const string SelectedChar = "a";
        const string UnselectedChar = "";
        const string ReportFileName = @"Files\DLY\DLY16010.rpt";

        private const string SHRCHE_DLY16010 = "DLY16010";
        private const string SHRCHE_DLY16010_Pri = "DLY16010_Pri";
        private const string DLY16010_UPDATE = "DLY16010_UPDATE";
        private const string rptFullPathName_PIC = @"Files\DLY\DLY16010.rpt";

        #endregion


        // SPREADのCELLに移動したとき入力前に表示されていた文字列保存用
        string _originalText = null;


        #region バインド用プロパティ

        private string _担当者ID = null;
        public string 担当者ID
        {
            set { _担当者ID = value; NotifyPropertyChanged(); }
            get { return _担当者ID; }
        }

        private int _取引区分 = 4;
        public int 取引区分
        {
            get { return this._取引区分; }
            set { this._取引区分 = value; NotifyPropertyChanged(); }
        }

        private int _検索日付選択 = 0;
        public int 検索日付選択
        {
            set { _検索日付選択 = value; NotifyPropertyChanged(); }
            get { return _検索日付選択; }
        }

        private string _検索日付From = null;
        public string 検索日付From
        {
            get { return this._検索日付From; }
            set { this._検索日付From = value; NotifyPropertyChanged(); }
        }

        private string _検索日付To = null;
        public string 検索日付To
        {
            get { return this._検索日付To; }
            set { this._検索日付To = value; NotifyPropertyChanged(); }
        }

        private string _得意先ID;
        public string 得意先ID
        {
            set { _得意先ID = value; NotifyPropertyChanged(); }
            get { return _得意先ID; }
        }

        private decimal _合計金額;
        public decimal 合計金額
        {
            set { _合計金額 = value; NotifyPropertyChanged(); }
            get { return _合計金額; }
        }

        private decimal _出金予定額;
        public decimal 出金予定額
        {
            set { _出金予定額 = value; NotifyPropertyChanged(); }
            get { return _出金予定額; }
        }

        private decimal _既出金額;
        public decimal 既出金額
        {
            set { _既出金額 = value; NotifyPropertyChanged(); }
            get { return _既出金額; }
        }

        private decimal _出金相殺;
        public decimal 出金相殺
        {
            set { _出金相殺 = value; NotifyPropertyChanged(); }
            get { return _出金相殺; }
        }

        private decimal _出金合計;
        public decimal 出金合計
        {
            set { _出金合計 = value; NotifyPropertyChanged(); }
            get { return _出金合計; }
        }

        // 検索した結果データ
        //private DataTable _dUriageDataResult = null;
        //public DataTable 売上明細データ検索結果
        //{
        //    get { return this._dUriageDataResult; }
        //    set
        //    {
        //        this._dUriageDataResult = value;
        //        if (value == null)
        //        {
        //            this.売上明細データ = null;
        //        }
        //        else
        //        {
        //            this.売上明細データ = value.Copy();
        //        }
        //        NotifyPropertyChanged();
        //        //NotifyPropertyChanged("売上明細データ");
        //    }
        //}

        private List<DLY16010_Member> _dUriageData = null;
        public List<DLY16010_Member> 売上明細データ
        {
            get
            {
                return this._dUriageData;
            }
            set
            {
                this._dUriageData = value;
                this.sp売上明細データ.ItemsSource = value;
                NotifyPropertyChanged();
            }
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
            set { _表示固定列数 = value; NotifyPropertyChanged(); SetupSpreadFixedColumn(this.sp売上明細データ, value); }
        }

		private int _入金区分Combo = 0;
		public int 入金区分Combo
		{
			get { return _入金区分Combo; }
			set { _入金区分Combo = value; NotifyPropertyChanged(); }
		}

		private string _摘要指定 = "";
		public string 摘要指定
		{
			get { return _摘要指定; }
			set { _摘要指定 = value; NotifyPropertyChanged(); }
		}



        #endregion

        #region コンボボックス用バインディング

        /// <summary>
        /// コンボボックス用
        /// </summary>

		//private UcLabelComboBox[] orderComboboxes = new UcLabelComboBox[] { null, null, null, null, null };

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

        #endregion

        #region DLY16010

        /// <summary>
        /// 伝票入力
        /// </summary>
        public DLY16010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

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
                    var wnd = GetWindow(this._gcSpreadGrid);

                    DLY09010 frm = new DLY09010();
                    frm.初期明細番号 = (int?)mNo;
					frm.ShowDialog(wnd);

					if (frm.IsUpdated)
					{
						// 日報側で更新された場合、再検索を実行する
						var parent = ViewBaseCommon.FindVisualParent<DLY16010>(this._gcSpreadGrid);
						parent.Button_Click_1(null, null);
					}

                }
            }
        }
        #endregion

        #region LOAD時

        /// <summary>
        /// 画面読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            // 初期状態を保存（SPREADリセット時にのみ使用する）
            this.sp_Config = AppCommon.SaveSpConfig(this.sp売上明細データ);

            //F1(検索)機能
            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { null, typeof(SCH01010) });
            base.MasterMaintenanceWindowList.Add("M72_TNT", new List<Type> { null, typeof(SCH23010) });
			AppCommon.SetutpComboboxList(this.Cmb_検索日付, false);
			AppCommon.SetutpComboboxList(this.c入金区分, false);

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigDLY16010)ucfg.GetConfigValue(typeof(ConfigDLY16010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigDLY16010();
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
                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;
                this.Cmb_検索日付.SelectedIndex = frmcfg.区分1;
                this.検索日付From = frmcfg.集計期間From;
                this.検索日付To = frmcfg.集計期間To;
            }
            #endregion

			AppCommon.LoadSpConfig(this.sp売上明細データ, frmcfg.spConfig20180118 != null ? frmcfg.spConfig20180118 : this.sp_Config);
			sp売上明細データ.InputBindings.Add(new KeyBinding(sp売上明細データ.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));


            ButtonCellType btn = this.sp売上明細データ.Columns[0].CellType as ButtonCellType;
            btn.Command = new cmd売上詳細表示(sp売上明細データ);


            //ComboBoxに値を設定する
            GetComboBoxItems();

            sp売上明細データ.RowCount = 0;

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

            this.textbox検索日付From.SetFocus();
        }

        #endregion

        #region コンボボックスの中身

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

        #endregion

        #region コンボボックスの表示順指定

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

        #endregion

        #region エラーメッセージ定義

        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            this.Message = base.ErrorMessage;
            base.SetFreeForInput();
        }

        #endregion

        #region データ受信メソッド

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
                case SHRCHE_DLY16010:
                    base.SetFreeForInput();
                    var ds = data as DataSet;
                    if (ds == null)
                    {
                        this.売上明細データ = null;
                    }
                    else
                    {
                        売上明細データ = (List<DLY16010_Member>)AppCommon.ConvertFromDataTable(typeof(List<DLY16010_Member>), ds.Tables["DataList"]);

                        if (this.売上明細データ.Count == 0)
                        {
                            Summary();
                            this.ErrorMessage = "該当するデータはありません。";
                            return;
                        }

                        DataReSort();
                        //Perform_Pickup();
                        Summary();
                        textbox検索日付From.Focus();
                    }
                    break;

                //プレビュー出力用
                case SHRCHE_DLY16010_Pri:
                    DispPreviw(tbl);
                    break;
				case DLY16010_UPDATE:
					if (CloseFlg) { CloseFlg = false; this.Close(); }
					break;
            }

        }

        #endregion

        #region 合計計算

        // 合計計算
        void Summary()
        {
            合計金額 = 0m;

			if (sp売上明細データ.Columns[1].Name == null)
			{
				return;
			}

			DataTable 印刷データ = new DataTable("印刷データ");

			Dictionary<string, string> changecols = new Dictionary<string, string>()
			{
			};

			AppCommon.ConvertSpreadDataToTable<DLY16010_Member>(this.sp売上明細データ, 印刷データ, changecols);

			合計金額 = AppCommon.DecimalParse(印刷データ.Compute("Sum(入出金金額)", null).ToString());

        }

        #endregion

        #region DataReSort

        void DataReSort()
        {
            if (売上明細データ == null)
            {
                return;
            }
            if (売上明細データ.Count == 0)
            {
                return;
            }
            this.sp売上明細データ.SortDescriptions.Clear();
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
                            this.sp売上明細データ.SortDescriptions.Add(sort);
                            sort = new SpreadSortDescription();
                            sort.Direction = (this.表示順方向[ix]) ? ListSortDirection.Descending : ListSortDirection.Ascending;
                            sort.ColumnName = "明細行";
                            this.sp売上明細データ.SortDescriptions.Add(sort);
                            break;
                        case "指定日付":
                            sort.ColumnName = (Cmb_検索日付.Combo_SelectedItem as CodeData).表示名 + "日付";
                            this.sp売上明細データ.SortDescriptions.Add(sort);
                            break;
                        case "運行者名":
                            sort.ColumnName = "乗務員名";
                            this.sp売上明細データ.SortDescriptions.Add(sort);
                            break;
                        case "出金金額":
                            sort.ColumnName = "入出金金額";
                            this.sp売上明細データ.SortDescriptions.Add(sort);
                            break;
                        default:
                            sort.ColumnName = cd.表示名;
                            this.sp売上明細データ.SortDescriptions.Add(sort);
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

        #endregion

        #region リボン

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
            if (this.sp売上明細データ.ActiveCellPosition.Row < 0)
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
            if (this.売上明細データ == null || this.売上明細データ.Count == 0)
            {
                MessageBox.Show("検索データがありません。");
                return;
            }

            DataTable CSVデータ = new DataTable();
            //リストをデータテーブルへ
            AppCommon.ConvertToDataTable(売上明細データ, CSVデータ);


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
                CSVData.SaveCSV(CSVデータ, sfd.FileName, true, true, false, ',');
                MessageBox.Show("CSVファイルの出力が完了しました。");
            }
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

        /// <summary>
        /// F11　リボン終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            this.Close();
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

            if (this.売上明細データ == null)
			{
				this.ErrorMessage = "印刷データがありません。";
                return;
            }
            if (this.売上明細データ.Count == 0)
            {
                this.ErrorMessage = "印刷データがありません。";
                return;
            }
            try
            {
                base.SetBusyForInput();
                var parms = new List<Framework.Reports.Preview.ReportParameter>()
				{
					new Framework.Reports.Preview.ReportParameter(){ PNAME="日付区分", VALUE=(this.Cmb_検索日付.Text==null?"":this.Cmb_検索日付.Text)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="日付FROM", VALUE=(this.検索日付From==null?"":this.検索日付From)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="日付TO", VALUE=(this.検索日付To==null?"":this.検索日付To)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="支払先指定", VALUE=(this.txt支払先指定.Text2==null?"":this.txt支払先指定.Text2)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="表示順序", VALUE=string.Format("{0} {1} {2} {3} {4}", 表示順名[0], 表示順名[1], 表示順名[2], 表示順名[3], 表示順名[4])},
				};
                KyoeiSystem.Framework.Reports.Preview.ReportPreview view = null;

				DataTable 印刷データ = new DataTable("入金伝票一覧");
                //リストをデータテーブルへ
                //AppCommon.ConvertToDataTable(売上明細データ, 印刷データ);

				Dictionary<string, string> changecols = new Dictionary<string, string>()
				{
				};

				AppCommon.ConvertSpreadDataToTable<DLY16010_Member>(this.sp売上明細データ, 印刷データ, changecols);


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

        #endregion

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
                KyoeiSystem.Framework.Reports.Preview.ReportPreview view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
                view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
                view.MakeReport("出金伝票問合せ", rptFullPathName_PIC, 0, 0, 0);
                view.SetReportData(tbl);
				view.ShowPreview();
				view.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 日付処理

        /// <summary>
        /// 検索日付From
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LostFocus_1(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 検索日付To
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LostFocus_2(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region 検索用ボタン

        /// <summary>
        /// 検索用ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!base.CheckAllValidation())
                {
                    this.ErrorMessage = "入力内容に誤りがあります。";
                    MessageBox.Show("入力内容に誤りがあります。");
                    return;
                }

				sp売上明細データ.FilterDescriptions.Clear();

                if (ExpSyousai.IsExpanded == true)
                {
                    ExpSyousai.IsExpanded = false;
                }


				int 日付区分C;
				日付区分C = Cmb_検索日付.SelectedIndex + 1;
				int 入金区分C;
				入金区分C = c入金区分.SelectedIndex;
                DateTime? p検索日付From = null;
                DateTime? p検索日付To = null;
                int? 得意先ID = null;
                int? 担当者ID = null;
                int iwk;
                if (int.TryParse(this.得意先ID, out iwk) == true)
                {
                    得意先ID = iwk;
                }
                if (int.TryParse(this.担当者ID, out iwk) == true)
                {
                    担当者ID = iwk;
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
				= new CommunicationObject(MessageType.RequestData, "DLY16010", 担当者ID, 得意先ID, p検索日付From, p検索日付To, 検索日付選択, 入金区分C, 摘要指定);

                base.SendRequest(com);
                base.SetBusyForInput();
                return;
            }
            catch
            {
                throw;
            }
        }

        #endregion


        #region SPREAD CellEnter

        private void sp売上明細データ_CellEnter(object sender, SpreadCellEnterEventArgs e)
        {
            var grid = sender as GcSpreadGrid;
            if (grid == null) return;
            if (grid.RowCount == 0) return;
            this._originalText = grid.Cells[e.Row, e.Column].Text;
        }

        #endregion


        string CellName = string.Empty;
        string CellText = string.Empty;
        decimal Cell入出金金額 = 0;


        #region SPREAD CellEditEnding

        private void sp売上明細データ_CellEditEnding(object sender, SpreadCellEditEndingEventArgs e)
        {
            if (e.EditAction == SpreadEditAction.Cancel)
            {
                return;
            }
            CellName = e.CellPosition.ColumnName;
            CellText = sp売上明細データ.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;

            Cell入出金金額 = AppCommon.DecimalParse(sp売上明細データ.Cells[e.CellPosition.Row, "入出金金額"].Text);

			//スプレッドコンボイベント関連付け解除			
			if (sp売上明細データ[e.CellPosition].InheritedCellType is GrapeCity.Windows.SpreadGrid.ComboBoxCellType)
			{
				GrapeCity.Windows.SpreadGrid.Editors.GcComboBox gccmb = sp売上明細データ.EditElement as GrapeCity.Windows.SpreadGrid.Editors.GcComboBox;
				if (gccmb != null)
				{
					gccmb.SelectionChanged -= comboEdit_SelectionChanged;
				}
			}

			if (sp売上明細データ[e.CellPosition].InheritedCellType is GrapeCity.Windows.SpreadGrid.CheckBoxCellType)
			{
				GrapeCity.Windows.SpreadGrid.Editors.CheckBoxEditElement gcchk = sp売上明細データ.EditElement as GrapeCity.Windows.SpreadGrid.Editors.CheckBoxEditElement;
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
			if (sp売上明細データ.ActiveCell.IsEditing)
			{
				sp売上明細データ.CommitCellEdit();
			}
		}

		private void checkEdit_Checked(object sender, RoutedEventArgs e)
		{
			sp売上明細データ.CommitCellEdit();
		}

		private void checkEdit_Unchecked(object sender, RoutedEventArgs e)
		{
			sp売上明細データ.CommitCellEdit();
		}		


        #region SPREAD セル内容変更
        private void sp売上明細データ_CellEditEnded(object sender, GrapeCity.Windows.SpreadGrid.SpreadCellEditEndedEventArgs e)
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
                string ctext = sp売上明細データ.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
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

                base.SendRequest(new CommunicationObject(MessageType.UpdateData, DLY16010_UPDATE, row.Cells[colM.Index].Value, row.Cells[colL.Index].Value, e.CellPosition.ColumnName, val));


                合計金額 += AppCommon.DecimalParse(row.Cells[this.sp売上明細データ.Columns["入出金金額"].Index].Value.ToString()) - Cell入出金金額;

                //Button_Click_1(null , null);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = "入力内容が不正です。";
            }
        }

        #endregion

        #region 表示項目位置リセット

        private void ColumnResert_Click(object sender, RoutedEventArgs e)
        {
            AppCommon.LoadSpConfig(this.sp売上明細データ, this.sp_Config);
            DataReSort();
        }

        #endregion

        #region 表示順方向

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            DataReSort();
        }

        #endregion

        #region 表示固定列数

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

        #endregion

        #region Window_Closed

        private void MainWindow_Closed(object sender, EventArgs e)
        {
			this.sp売上明細データ.InputBindings.Clear();
			this.売上明細データ = null;
			
			if (ucfg != null)
            {
                if (frmcfg == null) { frmcfg = new ConfigDLY16010(); }
                frmcfg.Top = this.Top;
                frmcfg.Left = this.Left;
                frmcfg.Width = this.Width;
                frmcfg.Height = this.Height;
                frmcfg.表示順 = this.表示順;
                frmcfg.表示順方向 = this.表示順方向;
                frmcfg.区分1 = this.Cmb_検索日付.SelectedIndex;
                frmcfg.集計期間From = this.検索日付From;
                frmcfg.集計期間To = this.検索日付To;
                frmcfg.spConfig20180118 = AppCommon.SaveSpConfig(this.sp売上明細データ);

                ucfg.SetConfigValue(frmcfg);
            }

        }

        #endregion

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

        #region DisplayDetail()

        private void DisplayDetail()
        {
            int rowNo = this.sp売上明細データ.ActiveCellPosition.Row;
            var row = this.sp売上明細データ.Rows[rowNo];
            var mNo = row.Cells[sp売上明細データ.Columns["明細番号"].Index].Value;
            DLY09010 frm = new DLY09010();
            frm.初期明細番号 = (int?)mNo;
			frm.ShowDialog(this);
			if (frm.IsUpdated)
			{
				// 日報側で更新された場合、再検索を実行する
				this.Button_Click_1(null, null);
			}

        }

        #endregion

		//private void SortItemCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
		//{

		//	DataReSort();
		//}

        private void sp売上明細データ_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Delete && sp売上明細データ.EditElement == null)
			{
				e.Handled = true;
			}
			if (e.Key == Key.V && (((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) != KeyStates.Down) || ((Keyboard.GetKeyStates(Key.RightCtrl) & KeyStates.Down) != KeyStates.Down)))
			{
				e.Handled = true;
			}
        }

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			if (sp売上明細データ.ActiveCell != null && sp売上明細データ.ActiveCell.IsEditing)
			{
				CloseFlg = true;
				sp売上明細データ.CommitCellEdit();
				if (CloseFlg) { e.Cancel = true; }
				return;
			}
		}

		private void sp売上明細データ_CellBeginEdit(object sender, SpreadCellBeginEditEventArgs e)
		{
			EditFlg = true;

		}

		/// <summary>			
		/// sp売上明細データ_EditElementShowing			
		/// スプレッドコンボイベント関連付け			
		/// デザイン画面でイベント追加			
		/// </summary>			
		/// <param name="sender"></param>			
		/// <param name="e"></param>			
		void sp売上明細データ_EditElementShowing(object sender, EditElementShowingEventArgs e)
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

		private void sp売上明細データ_RowCollectionChanged(object sender, SpreadCollectionChangedEventArgs e)
		{
			if (sp売上明細データ.Columns[1].Name == null)
			{
				return;
			}
			if (sp売上明細データ.Rows.Count() > 0)
			{
				Summary();
			}

		}			

    }
}
