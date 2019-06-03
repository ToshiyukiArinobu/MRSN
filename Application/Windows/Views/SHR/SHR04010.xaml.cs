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
using System.Data.SqlClient;
using System.Data;

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;
using KyoeiSystem.Framework.Windows.Controls;
using GrapeCity.Windows.SpreadGrid;
using System.Linq.Expressions;
using System.IO;


namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
	/// 請求書発行画面
	/// </summary>
	public partial class SHR04010 : WindowReportBase
	{

		public class SHR04010_KIKAN : INotifyPropertyChanged
		{
			public int _支払先ID { get; set; }
			public int _支払先KEY { get; set; }
			public string _支払先名 { get; set; }
			public int? _親子区分ID { get; set; }
			public string _新規区分 { get; set; }
			public int? _親ID { get; set; }
			public int _Ｓ税区分ID { get; set; }
			public int _支払先締日 { get; set; }
			public decimal _締日期首残 { get; set; }
			public decimal _月次期首残 { get; set; }
			public int _前月データ区分 { get; set; }
			public DateTime? _前月開始日付 { get; set; }
			public DateTime? _前月終了日付 { get; set; }
			public DateTime? _開始日付1 { get; set; }
			public DateTime? _終了日付1 { get; set; }
			public DateTime? _開始日付2 { get; set; }
			public DateTime? _終了日付2 { get; set; }
			public DateTime? _開始日付3 { get; set; }
			public DateTime? _終了日付3 { get; set; }
			public string _str開始日付1 { get; set; }
			public string _str終了日付1 { get; set; }
			public string _str開始日付2 { get; set; }
			public string _str終了日付2 { get; set; }
			public string _str開始日付3 { get; set; }
			public string _str終了日付3 { get; set; }
			public DateTime? _開始月次日付 { get; set; }
			public DateTime? _終了月次日付 { get; set; }
			public DateTime? _クリア開始日付 { get; set; }
			public DateTime? _クリア終了日付 { get; set; }
			public string _strクリア開始日付 { get; set; }
			public string _strクリア終了日付 { get; set; }

			public int 支払先ID { get { return _支払先ID; } set { _支払先ID = value; NotifyPropertyChanged(); } }
			public int 支払先KEY { get { return _支払先KEY; } set { _支払先KEY = value; NotifyPropertyChanged(); } }
			public string 支払先名 { get { return _支払先名; } set { _支払先名 = value; NotifyPropertyChanged(); } }
			public int? 親子区分ID { get { return _親子区分ID; } set { _親子区分ID = value; NotifyPropertyChanged(); } }
			public string 新規区分 { get { return _新規区分; } set { _新規区分 = value; NotifyPropertyChanged(); } }
			public int? 親ID { get { return _親ID; } set { _親ID = value; NotifyPropertyChanged(); } }
			public int Ｓ税区分ID { get { return _Ｓ税区分ID; } set { _Ｓ税区分ID = value; NotifyPropertyChanged(); } }
			public int 支払先締日 { get { return _支払先締日; } set { _支払先締日 = value; NotifyPropertyChanged(); } }
			public decimal 締日期首残 { get { return _締日期首残; } set { _締日期首残 = value; NotifyPropertyChanged(); } }
			public decimal 月次期首残 { get { return _月次期首残; } set { _月次期首残 = value; NotifyPropertyChanged(); } }
			public int 前月データ区分 { get { return _前月データ区分; } set { _前月データ区分 = value; NotifyPropertyChanged(); } }
			public DateTime? 前月開始日付 { get { return _前月開始日付; } set { _前月開始日付 = value; NotifyPropertyChanged(); } }
			public DateTime? 前月終了日付 { get { return _前月終了日付; } set { _前月終了日付 = value; NotifyPropertyChanged(); } }
			public DateTime? 開始日付1 { get { return _開始日付1; } set { _開始日付1 = value; NotifyPropertyChanged(); } }
			public DateTime? 終了日付1 { get { return _終了日付1; } set { _終了日付1 = value; NotifyPropertyChanged(); } }
			public DateTime? 開始日付2 { get { return _開始日付2; } set { _開始日付2 = value; NotifyPropertyChanged(); } }
			public DateTime? 終了日付2 { get { return _終了日付2; } set { _終了日付2 = value; NotifyPropertyChanged(); } }
			public DateTime? 開始日付3 { get { return _開始日付3; } set { _開始日付3 = value; NotifyPropertyChanged(); } }
			public DateTime? 終了日付3 { get { return _終了日付3; } set { _終了日付3 = value; NotifyPropertyChanged(); } }
			public string str開始日付1 { get { return _str開始日付1; } set { _str開始日付1 = value; NotifyPropertyChanged(); } }
			public string str終了日付1 { get { return _str終了日付1; } set { _str終了日付1 = value; NotifyPropertyChanged(); } }
			public string str開始日付2 { get { return _str開始日付2; } set { _str開始日付2 = value; NotifyPropertyChanged(); } }
			public string str終了日付2 { get { return _str終了日付2; } set { _str終了日付2 = value; NotifyPropertyChanged(); } }
			public string str開始日付3 { get { return _str開始日付3; } set { _str開始日付3 = value; NotifyPropertyChanged(); } }
			public string str終了日付3 { get { return _str終了日付3; } set { _str終了日付3 = value; NotifyPropertyChanged(); } }
			public DateTime? 開始月次日付 { get { return _開始月次日付; } set { _開始月次日付 = value; NotifyPropertyChanged(); } }
			public DateTime? 終了月次日付 { get { return _終了月次日付; } set { _終了月次日付 = value; NotifyPropertyChanged(); } }
			public DateTime? クリア開始日付 { get { return _クリア開始日付; } set { _クリア開始日付 = value; NotifyPropertyChanged(); } }
			public DateTime? クリア終了日付 { get { return _クリア終了日付; } set { _クリア終了日付 = value; NotifyPropertyChanged(); } }
			public string strクリア開始日付 { get { return _strクリア開始日付; } set { _strクリア開始日付 = value; NotifyPropertyChanged(); } }
			public string strクリア終了日付 { get { return _strクリア終了日付; } set { _strクリア終了日付 = value; NotifyPropertyChanged(); } }

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
		public class ConfigSHR04010 : FormConfigBase
		{
			public byte[] spConfig = null;
		}

		/// ※ 必ず public で定義する。
		public ConfigSHR04010 frmcfg = null;

		// SPREAD初期状態保存用
		public byte[] spConfig = null;

		#endregion

		private const string GET_CNTL = "M87_CNTL";
        private const string GET_RPT = "M99_RPT_GET";
        private const string SHR04010_KIKAN_SET = "SHR04010_KIKAN_SET";
        private const string SHR04010_KIKAN_SET2 = "SHR04010_KIKAN_SET2";
        private const string SHR04010_SYUKEI = "SHR04010_SYUKEI";

		// SPREADのCELLに移動したとき入力前に表示されていた文字列保存用
		string _originalText = null;

		// 請求書
		private string rptBill_A = @"Files\BillReportA.rpt";
		private string rptBill_B = @"Files\BillReportB.rpt";
		private string rptBill_T = @"Files\BillReportT.rpt";
		private string rptBill_K = @"Files\BillReportK.rpt";
		private string rptBill_C = @"Files\BillReportC.rpt";

		string 端末ID = Environment.MachineName;
		string _p支払先ピックアップ = string.Empty;
		public string 支払先ピックアップ
		{
			get { return this._p支払先ピックアップ; }
			set
			{
				this._p支払先ピックアップ = value;
				NotifyPropertyChanged();
			}
		}

		string _p支払先範囲指定From;
		public string 支払先範囲指定From
		{
			get { return this._p支払先範囲指定From; }
			set
			{
				this._p支払先範囲指定From = value;
				NotifyPropertyChanged();
			}
		}

		string _p支払先範囲指定To;
		public string 支払先範囲指定To
		{
			get { return this._p支払先範囲指定To; }
			set
			{
				this._p支払先範囲指定To = value;
				NotifyPropertyChanged();
			}
		}

		string _p作成締日;
		public string 作成締日
		{
			get { return this._p作成締日; }
			set
			{
				this._p作成締日 = value;
				NotifyPropertyChanged();
			}
		}

		string _p作成年月;
		public string 作成年月
		{
			get { return this._p作成年月; }
			set
			{
				this._p作成年月 = value;
				NotifyPropertyChanged();
			}
		}

		string _p請求対象期間From;
		public string 請求対象期間From
		{
			get { return this._p請求対象期間From; }
			set
			{
				this._p請求対象期間From = value;
				NotifyPropertyChanged();
			}
		}

		string _p請求対象期間To;
		public string 請求対象期間To
		{
			get { return this._p請求対象期間To; }
			set
			{
				this._p請求対象期間To = value;
				NotifyPropertyChanged();
			}
		}

		string _出力日付 = DateTime.Today.ToString("yyyy/MM/dd");
		public string 出力日付
		{
			get { return this._出力日付; }
			set
			{
				this._出力日付 = value;
				NotifyPropertyChanged();
			}
		}

		int _複写枚数 = 1;
		public int 複写枚数
		{
			get { return this._複写枚数; }
			set { this._複写枚数 = value; NotifyPropertyChanged(); }
		}

		int _出力対象 = 0;
		public int 出力対象
		{
			get { return this._出力対象; }
			set { this._出力対象 = value; NotifyPropertyChanged(); }
		}
		int _請求書発行状態 = 0;
		public int 請求書発行状態
		{
			get { return this._請求書発行状態; }
			set { this._請求書発行状態 = value; NotifyPropertyChanged(); }
		}


		private List<SHR04010_KIKAN> _請求書一覧データ = null;
		public List<SHR04010_KIKAN> 請求書一覧データ
		{
			get { return this._請求書一覧データ; }
			set
			{
				this._請求書一覧データ = value;
				if (value == null)
				{
					this.sp請求データ一覧.ItemsSource = null;
				}
				else
				{
					this.sp請求データ一覧.ItemsSource = value;
				}
				NotifyPropertyChanged();

			}
		}

		private DataTable _請求書一覧TBL = null;
		public DataTable 請求書一覧TBL
		{
			get { return this._請求書一覧TBL; }
			set
			{
				this._請求書一覧TBL = value;
				NotifyPropertyChanged();
			}
		}



        private int _表示区分 = 3;
        public int 表示区分
        {
            get { return this._表示区分; }
            set { this._表示区分 = value; NotifyPropertyChanged(); }
        }

        private int _取引区分 = 4;
        public int 取引区分
        {
            get { return this._取引区分; }
            set { this._取引区分 = value; NotifyPropertyChanged(); }
        }

        private int _計算期間の再計算 = 0;
        public int 計算期間の再計算
        {
            get { return this._計算期間の再計算; }
            set { this._計算期間の再計算 = value; NotifyPropertyChanged(); }
        }


        #region 明細クリック時のアクション定義
        /// <summary>
        /// 明細クリック時のアクション定義
        /// </summary>
        public class cmd期間クリア : ICommand
        {
            private GcSpreadGrid _gcSpreadGrid;
            public cmd期間クリア(GcSpreadGrid gcSpreadGrid)
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
                    var wnd = GetWindow(this._gcSpreadGrid);

					row.Cells[this._gcSpreadGrid.Columns["str開始日付1"].Index].Value = row.Cells[this._gcSpreadGrid.Columns["strクリア開始日付"].Index].Value;
					row.Cells[this._gcSpreadGrid.Columns["str終了日付1"].Index].Value = row.Cells[this._gcSpreadGrid.Columns["strクリア終了日付"].Index].Value;
					//row.Cells[this._gcSpreadGrid.Columns["開始日付1"].Index].Value = row.Cells[this._gcSpreadGrid.Columns["クリア開始日付"].Index].Value;
					//row.Cells[this._gcSpreadGrid.Columns["終了日付1"].Index].Value = row.Cells[this._gcSpreadGrid.Columns["クリア終了日付"].Index].Value;
                    //row.Cells[this._gcSpreadGrid.Columns["開始日付2"].Index].Value = null;
                    //row.Cells[this._gcSpreadGrid.Columns["終了日付2"].Index].Value = null;
                    //row.Cells[this._gcSpreadGrid.Columns["開始日付3"].Index].Value = null;
                    //row.Cells[this._gcSpreadGrid.Columns["終了日付3"].Index].Value = null;

                }
            }
        }
        #endregion


		/// <summary>
		/// 請求書発行
		/// </summary>
		public SHR04010()
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
			this.sp請求データ一覧.RowCount = 0;
			this.spConfig = AppCommon.SaveSpConfig(this.sp請求データ一覧);

			#region 設定項目取得
			ucfg = AppCommon.GetConfig(this);
			frmcfg = (ConfigSHR04010)ucfg.GetConfigValue(typeof(ConfigSHR04010));
			if (frmcfg == null)
			{
				frmcfg = new ConfigSHR04010();
				ucfg.SetConfigValue(frmcfg);
				frmcfg.spConfig = this.spConfig;
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
			}
			if (frmcfg.spConfig != null)
			{
				AppCommon.LoadSpConfig(this.sp請求データ一覧, frmcfg.spConfig);
			}
			#endregion

			this.sp請求データ一覧.PreviewKeyDown += sp請求データ一覧_PreviewKeyDown;

            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { null, typeof(SCH01010) });
			base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_CNTL, 1, 0));
			base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_RPT));

            ButtonCellType btn = this.sp請求データ一覧.Columns[3].CellType as ButtonCellType;
            btn.Command = new cmd期間クリア(sp請求データ一覧);

			ScreenClear();
		}

		private void sp請求データ一覧_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Delete)
			{
				e.Handled = true;
			}
		}

		private void ScreenClear()
		{
			this.MaintenanceMode = null;
			this.sp請求データ一覧.RowCount = 0;
			ResetAllValidation();
			SetFocusToTopControl();
		}

		public override void OnReveivedError(CommunicationObject message)
		{
			base.OnReveivedError(message);
			this.ErrorMessage = (string)message.GetResultData();
		}

		string WarimasiName1 = string.Empty;
		string WarimasiName2 = string.Empty;

		/// <summary>
		/// 取得データの取り込み
		/// </summary>
		/// <param name="message"></param>
		public override void OnReceivedResponseData(CommunicationObject message)
		{
			try
			{
				this.ErrorMessage = string.Empty;

				base.SetFreeForInput();

				var data = message.GetResultData();
				DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
				switch (message.GetMessageName())
				{
				case GET_CNTL:
					this.WarimasiName1 = AppCommon.GetWarimasiName1(tbl);
					this.WarimasiName2 = AppCommon.GetWarimasiName2(tbl);
					break;
				case GET_RPT:
					GetReportFile(tbl);
					break;
                case SHR04010_KIKAN_SET:
					//this.請求書一覧データ = tbl;
					if (tbl == null)
					{
						this.sp請求データ一覧.ItemsSource = null;
						this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
						return;
					}
					else
					{
						請求書一覧データ = (List<SHR04010_KIKAN>)AppCommon.ConvertFromDataTable(typeof(List<SHR04010_KIKAN>), tbl);
						//this.sp請求データ一覧.ItemsSource = this.請求書一覧データ.DefaultView;
						if (tbl.Rows.Count > 0)
						{
						}
						else
						{
							this.ErrorMessage = "指定された条件の請求データはありません。";
						}
					}
					break;
                case SHR04010_SYUKEI:
                    MessageBoxResult result = MessageBox.Show("集計が終了しました。\n\r終了しても宜しいでしょうか?"
                       , "確認"
                       , MessageBoxButton.YesNo
                       , MessageBoxImage.Question);
                    //OKならクリア
                    if (result == MessageBoxResult.Yes)
                    {
                        this.Close();
                    }
                    break;
				}
			}
			catch (Exception ex)
			{
				this.ErrorMessage = ex.Message;
			}
		}


		private void GetReportFile(DataTable tbl)
		{
			foreach (DataRow row in tbl.Rows)
			{
				string filepath = string.Format(@"Files\{0}.rpt", row["帳票ID"]);
				byte[] dat = (byte[])row["レポート定義データ"];
				FileStream filest = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
				filest.Write(dat, 0, dat.Length);
				filest.Close();
			}
		}

        /// <summary>
        /// 検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.CheckAllValidation() != true)
                {
                    MessageBox.Show("入力エラーがあります。");
                    return;
                }
                string pickup支払先 = this.支払先ピックアップ;
                int? p支払先FROM;
                int? p支払先TO;
                int? p締日;
                int? p作成年月;
                DateTime p作成年月日;

                if (string.IsNullOrWhiteSpace(this.支払先範囲指定From))
                {
                    p支払先FROM = null;
                }
                else
                {
                    p支払先FROM = AppCommon.IntParse(this.支払先範囲指定From);
                }
                if (string.IsNullOrWhiteSpace(this.支払先範囲指定To))
                {
                    p支払先TO = null;
                }
                else
                {
                    p支払先TO = AppCommon.IntParse(this.支払先範囲指定To);
                }
                if (string.IsNullOrWhiteSpace(this.作成締日))
                {
                    p締日 = null;
                }
                else
                {
                    p締日 = AppCommon.IntParse(this.作成締日);
                }
                if (string.IsNullOrWhiteSpace(this.作成年月))
                {
                    MessageBox.Show("入力エラーがあります。");
                    return;
                }
                else
                {
                    DateTime Wk;
                    p作成年月日 = DateTime.TryParse(this.作成年月 , out Wk) ? Wk : DateTime.Today;
                    p作成年月 = p作成年月日.Year * 100 + p作成年月日.Month;
                }

                //支払先リスト作成
                int?[] i支払先List = new int?[0];
                if (!string.IsNullOrEmpty(支払先ピックアップ))
                {
                    string[] 支払先List = 支払先ピックアップ.Split(',');
                    i支払先List = new int?[支払先List.Length];

                    for (int i = 0; i < 支払先List.Length; i++)
                    {
                        string str = 支払先List[i];
                        int code;
                        if (!int.TryParse(str, out code))
                        {
                            this.ErrorMessage = "支払先指定の形式が不正です。";
                            return;
                        }
                        i支払先List[i] = code;
                    }
                }

                base.SendRequest(new CommunicationObject(MessageType.RequestData, SHR04010_KIKAN_SET, 支払先ピックアップ, i支払先List, p支払先FROM, p支払先TO, p締日, p作成年月, p作成年月日, 計算期間の再計算));
            }
            catch (Exception)
            {
                return;
            }

        }

        /// <summary>
        /// クリアボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear()
        {
            try
            {

                string pickup支払先 = this.支払先ピックアップ;
                int? p支払先FROM;
                int? p支払先TO;
                int? p締日;
                int? p作成年月;
                DateTime p作成年月日;

                if (string.IsNullOrWhiteSpace(this.支払先範囲指定From))
                {
                    p支払先FROM = null;
                }
                else
                {
                    p支払先FROM = AppCommon.IntParse(this.支払先範囲指定From);
                }
                if (string.IsNullOrWhiteSpace(this.支払先範囲指定To))
                {
                    p支払先TO = null;
                }
                else
                {
                    p支払先TO = AppCommon.IntParse(this.支払先範囲指定To);
                }
                if (string.IsNullOrWhiteSpace(this.作成締日))
                {
                    p締日 = null;
                }
                else
                {
                    p締日 = AppCommon.IntParse(this.作成締日);
                }
                if (string.IsNullOrWhiteSpace(this.作成年月))
                {
                    MessageBox.Show("入力エラーがあります。");
                    return;
                }
                else
                {
                    DateTime Wk;
                    p作成年月日 = DateTime.TryParse(this.作成年月, out Wk) ? Wk : DateTime.Today;
                    p作成年月 = p作成年月日.Year * 100 + p作成年月日.Month;
                }
                base.SendRequest(new CommunicationObject(MessageType.RequestData, SHR04010_KIKAN_SET, 支払先ピックアップ, p支払先FROM, p支払先TO, p締日, p作成年月, p作成年月日, 計算期間の再計算));
            }
            catch (Exception)
            {
                return;
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
                var ctl = FocusManager.GetFocusedElement(this);
                if (ctl is TextBox)
                {
                    var uctext = ViewBaseCommon.FindVisualParent<UcTextBox>(ctl as UIElement);
                    if (uctext == null)
                    {
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(uctext.DataAccessName))
                    {
                        ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                        return;
                    }
                    SCH01010 srch = new SCH01010();
                    switch (uctext.DataAccessName)
                    {
                        case "M01_TOK":
                            srch.MultiSelect = false;
                            break;
                        default:
                            srch.MultiSelect = true;
                            break;
                    }
                    Framework.Windows.Controls.UcLabelTwinTextBox dmy = new Framework.Windows.Controls.UcLabelTwinTextBox();
                    srch.TwinTextBox = dmy;
                    srch.multi = 1;
                    srch.表示区分 = 3;
                    var ret = srch.ShowDialog(this);
                    if (ret == true)
                    {
                        uctext.Text = srch.SelectedCodeList;
                        FocusControl.SetFocusWithOrder(new TraversalRequest(FocusNavigationDirection.Next));
                    }
                }
			}
			catch (Exception ex)
			{
				appLog.Error("検索画面起動エラー", ex);
				ErrorMessage = "システムエラーです。サポートへご連絡ください。";
			}
		}

		/// <summary>
		/// F9　リボン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF9Key(object sender, KeyEventArgs e)
		{
            //if (this.請求書一覧データ == null)
            //{
            //    this.ErrorMessage = "印刷データを取得していません。";
            //    return;
            //}
            //int cnt = 0;
            //foreach(DataRow rec in this.請求書一覧データ.Rows)
            //{
            //    if (rec.IsNull("印刷区分") != true && (bool)rec["印刷区分"] == true)
            //    {
            //        cnt++;
            //    }
            //}
            //if (cnt == 0)
            //{
            //    this.ErrorMessage = "印刷対象が選択されていません。";
            //    return;
            //}


            //if (string.IsNullOrWhiteSpace(this.作成年月))
            //{
            //    MessageBox.Show("入力エラーがあります。");
            //    return;
            //}
            //else
            //{
            //}

            if (this.CheckAllValidation() != true)
            {
                MessageBox.Show("入力エラーがあります。");
                return;
            }

            if (sp請求データ一覧.Rows.Count == 0)
            {
                this.ErrorMessage = "指定された条件の請求データはありません。";
                MessageBox.Show("指定された条件の請求データはありません。");
                return;
            }

            int? p作成年月;
            DateTime p作成年月日;
            DateTime Wk;
            p作成年月日 = DateTime.TryParse(this.作成年月 , out Wk) ? Wk : DateTime.Today;
            p作成年月 = p作成年月日.Year * 100 + p作成年月日.Month;


			try
			{
				請求書一覧TBL = new DataTable();
				//リストをデータテーブルへ
				AppCommon.ConvertToDataTable(請求書一覧データ, 請求書一覧TBL);
			}
			catch (Exception)
			{
				return;
			};


			try
			{
				base.SendRequest(new CommunicationObject(MessageType.RequestData, SHR04010_SYUKEI, this.請求書一覧TBL, p作成年月, p作成年月日));
				base.SetBusyForInput();
			}
			catch (Exception)
			{
				return;
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



		private void ReportYM_Lostfocus(object sender, RoutedEventArgs e)
		{
			期間編集();
		}

		private void 締日_LostFocus(object sender, RoutedEventArgs e)
		{
			期間編集();
		}

		void 期間編集()
		{
			if (string.IsNullOrWhiteSpace(作成年月) == true || string.IsNullOrWhiteSpace(作成締日) == true)
			{
				this.請求対象期間From = string.Empty;
				this.請求対象期間To = string.Empty;
				return;
			}
			try
			{
				int day;
				if (int.TryParse(作成締日, out day) != true)
				{
					day = 0;
				}

				DateTime ymd1;
				DateTime ymd2;
				bool flg = DateTime.TryParse(string.Format("{0}/{1}", 作成年月, day), out ymd2);
				if (flg == true)
				{
					ymd1 = ymd2.AddMonths(-1);
					ymd1 = ymd1.AddDays(1);
				}
				else
				{
                    DateTime Wk;
					ymd2 = DateTime.TryParse(string.Format("{0}/1", 作成年月), out Wk) ? Wk.AddMonths(1).AddDays(-1) : DateTime.Today;
					ymd1 = ymd2.AddMonths(-1);
					ymd1 = ymd1.AddDays(1);
				}
				if (day == 31)
				{
                    DateTime Wk;
					ymd1 = DateTime.TryParse(string.Format("{0}/1", 作成年月) , out Wk) ? Wk : DateTime.Today;
				}

				this.請求対象期間From = ymd1.ToString("yyyy/MM/dd");
				this.請求対象期間To = ymd2.ToString("yyyy/MM/dd");
			}
			catch (Exception)
			{
			}
		}

		#region Window_Closed
		//画面が閉じられた時、データを保持する
		private void Window_Closed(object sender, EventArgs e)
		{
			請求書一覧データ = null;
			this.sp請求データ一覧.InputBindings.Clear();

			frmcfg.Top = this.Top;
			frmcfg.Left = this.Left;
			frmcfg.Height = this.Height;
			frmcfg.Width = this.Width;
			frmcfg.spConfig = AppCommon.SaveSpConfig(this.sp請求データ一覧);
			ucfg.SetConfigValue(frmcfg);

		}
		#endregion

		private void Button_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var ctl = sender as Button;
			if (ctl != null)
			{
				ctl.IsEnabled = true;
			}
		}

		private void ColumnReset_Click(object sender, RoutedEventArgs e)
		{
			AppCommon.LoadSpConfig(this.sp請求データ一覧, this.spConfig);
			ScreenClear();
		}

		string CellName = string.Empty;
		string CellText = string.Empty;
		private void sp請求データ一覧_CellEditEnding(object sender, SpreadCellEditEndingEventArgs e)
		{

			if (e.EditAction == SpreadEditAction.Cancel)
			{
				return;
			}
			CellName = e.CellPosition.ColumnName;
			CellText = sp請求データ一覧.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
		}

		private void sp請求データ一覧_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
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
				string ctext = sp請求データ一覧.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
                ctext = ctext == null ? string.Empty : ctext;
				if (cname == CellName && ctext == CellText)
				{
					// セルの値が変化していなければ何もしない
					return;
				}

				var row = gcsp.Rows[e.CellPosition.Row];
				object val = row.Cells[e.CellPosition.Column].Value;
                val = val == null ? "" : val;
				if (cname.Contains("開始日付") == true)
				{
					AppCommon.SpreadYMDCellCheck(sender, e, this._originalText);

					//cname = cname.Replace("年月日", "日付");

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
				if (cname.Contains("終了日付") == true)
				{
					AppCommon.SpreadYMDCellCheck(sender, e, this._originalText);

					//cname = cname.Replace("年月日", "日付");

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


			}
			catch (Exception ex)
			{
				this.ErrorMessage = "入力内容が不正です。";
			}
		}

		private void sp請求データ一覧_CellEnter(object sender, SpreadCellEnterEventArgs e)
		{

			var grid = sender as GcSpreadGrid;
			if (grid == null) return;
			if (grid.RowCount == 0) return;
			this._originalText = grid.Cells[e.Row, e.Column].Text;
		}


	}

}
