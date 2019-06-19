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
using System.Windows.Threading;
using System.Diagnostics;
using System.ComponentModel;

using System.IO;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;
using System.Data;


using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;
using KyoeiSystem.Framework.Windows.Controls;
using GrapeCity.Windows.SpreadGrid;
using System.Reflection;

namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
	/// MCustomer.xaml の相互作用ロジック
	/// </summary>
    public partial class DLY08010 : RibbonWindowViewBase
    {
        #region SPREADクリック時に入力項目を展開する際にイベント完了を待つ機能用
        public void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrames), frame);
            Dispatcher.PushFrame(frame);
        }

        public object ExitFrames(object frames)
        {
            ((DispatcherFrame)frames).Continue = false;
            return null;
        }
        #endregion

        public class DLY08010_Member : INotifyPropertyChanged
        {
            public int _明細番号;
            public int 明細番号 { get { return _明細番号; } set { _明細番号 = value; NotifyPropertyChanged(); } }
            public int _明細行;
            public int 明細行  { get { return _明細行; } set { _明細行 = value; NotifyPropertyChanged(); } }
            public int _明細区分;
            public int 明細区分 { get { return _明細区分; } set { _明細区分 = value; NotifyPropertyChanged(); } }
            public int _得意先ID;
            public int 得意先ID { get { return _得意先ID; } set { _得意先ID = value; NotifyPropertyChanged(); } }
            public int _得意先KEY;
            public int 得意先KEY { get { return _得意先KEY; } set { _得意先KEY = value; NotifyPropertyChanged(); } }
            public DateTime? _入出金日付;
            public DateTime? 入出金日付 { get { return _入出金日付; } set { _入出金日付 = value; NotifyPropertyChanged(); } }
            public int _入金区分;
            public int 入金区分 { get { return _入金区分; } set { _入金区分 = value; NotifyPropertyChanged(); } }
            public int _入金金額;
            public int 入金金額 { get { return _入金金額; } set { _入金金額 = value; NotifyPropertyChanged(); } }
            public decimal _d入金金額;
            public decimal d入金金額 { get { return _d入金金額; } set { _d入金金額 = value; NotifyPropertyChanged(); } }
            public int? _摘要ID;
            public int? 摘要ID { get { return _摘要ID; } set { _摘要ID = value; NotifyPropertyChanged(); } }
            public string _摘要;
            public string 摘要 { get { return _摘要; } set { _摘要 = value; NotifyPropertyChanged(); } }
            public DateTime? _手形期日;
            public DateTime? 手形期日  { get { return _手形期日; } set { _手形期日 = value; NotifyPropertyChanged(); } }
            public int? _入力者ID;
            public int? 入力者ID  { get { return _入力者ID; } set { _入力者ID = value; NotifyPropertyChanged(); } }
            public string _Str手形期日;
            public string Str手形期日  { get { return _Str手形期日; } set { _Str手形期日 = value; NotifyPropertyChanged(); } }

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
        public class ConfigDLY08010 : FormConfigBase
        {
			public int 番号通知区分 = 1;
			public int 最終伝票表示区分 = 0;
		}

        /// ※ 必ず public で定義する。
        public ConfigDLY08010 frmcfg = null;

        #endregion

        #region 定数定義
        //締日・集金日・サイト取得
        private const string M01ALLTableNm = "M01_ALLData";
        //Spread表示データ取得
        private const string GetDetailsNumber = "GetDetailsNumber";
        //入金予定額
        private const string DLY08010_NData = "DLY08010_NData";
        //残高表
        private const string DLY08010_OData = "DLY08010_OData";
        //自動裁判
        private const string New_Details = "New_Details";
        //SPREADのF1照会
        private const string GetTekiyoName = "DLY08010_TEKIYO_NAME";
        //登録・変更
        private const string DLY08010_UPDATE = "DLY08010_UPDATE";
        //削除
        private const string DLY08010_DELETE = "DLY08010_DELETE";
        private const string GetMeisaiNo = "DLY08010_GETNO";
        private const string GetMaxMeisaiNo = "DLY08010_GetMaxMeisaiNo";
        #endregion

		#region Binding(バインド)
        CommonConfig ccfg = null;
        ///*****************< DObject >****************///

        #region 基礎Binding

        private List<DLY08010_Member> _dUriageData2;
        public List<DLY08010_Member> 入金明細リスト
        {
            //get { return this._dUriageData2; }
            //set { this._dUriageData2 = value; NotifyPropertyChanged(); }
            get
            {
                return this._dUriageData2;
            }
            set
            {
                this._dUriageData2 = value;
                if (value == null)
                {
                    this.spNyukin.ItemsSource = null;
                }
                else
                {
                    this.spNyukin.ItemsSource = value;
                }
                NotifyPropertyChanged();
            }

        }

        private int? _登録件数 = null;
        public int? 登録件数
        {
            get { return this._登録件数; }
            set { this._登録件数 = value; NotifyPropertyChanged(); }
        }

        private int? _入力者ID;
        public int? 入力者ID
        {
            get { return this._入力者ID; }
            set { this._入力者ID = value; NotifyPropertyChanged(); }
        }

        private int? _DetailsNumber;
        public int? DetailsNumber
        {
            get { return this._DetailsNumber; }
            set { this._DetailsNumber = value; NotifyPropertyChanged(); }
        }

        private int? _明細番号;
        public int? 明細番号
        {
            get { return this._明細番号; }
            set { this._明細番号 = value; NotifyPropertyChanged(); }
        }

        private int? _明細行;
        public int? 明細行
        {
            get { return this._明細行; }
            set { this._明細行 = value; NotifyPropertyChanged(); }
        }

        private int? _得意先ID = null;
        public int? 得意先ID
        {
            get { return this._得意先ID; }
            set { this._得意先ID = value; NotifyPropertyChanged(); }
        }

        private string _締日 = string.Empty;
        public string 締日
        {
            get { return this._締日; }
            set { this._締日 = value; NotifyPropertyChanged(); }
        }

        private string _集金日 = string.Empty;
        public string 集金日
        {
            get { return this._集金日; }
            set { this._集金日 = value; NotifyPropertyChanged(); }
        }

        private string _サイト = string.Empty;
        public string サイト
        {
            get { return this._サイト; }
            set { this._サイト = value; NotifyPropertyChanged(); }
        }

        private DateTime? _入金日付;
        public DateTime? 入金日付
        {
            get { return this._入金日付; }
            set { this._入金日付 = value; NotifyPropertyChanged(); }
        }

        private DateTime? _入出金日付;
        public DateTime? 入出金日付
        {
            get { return this._入出金日付; }
            set { this._入出金日付 = value; NotifyPropertyChanged(); }
        }

        private int? _入金金額;
        public int? 入金金額
        {
            get { return this._入金金額; }
            set { this._入金金額 = value; NotifyPropertyChanged(); }
        }

        private int? _摘要ID;
        public int? 摘要ID
        {
            get { return this._摘要ID; }
            set { this._摘要ID = value; NotifyPropertyChanged(); }
        }

        private string _摘要;
        public string 摘要
        {
            get { return this._摘要; }
            set { this._摘要 = value; NotifyPropertyChanged(); }
        }

        private int? _入金区分 = 0;
        public int? 入金区分
        {
            get { return _入金区分; }
            set { _入金区分 = value; NotifyPropertyChanged(); }
        }

        private DateTime? _手形期日;
        public DateTime? 手形期日
        {
            get { return _手形期日; }
            set { _手形期日 = value; NotifyPropertyChanged(); }
        }

        private int? _ineNumber = null;
        public int? LineNumber
        {
            get { return _ineNumber; }
            set { this._ineNumber = value; NotifyPropertyChanged(); }
        }

        private int _得意先コード = 0;
        public int 得意先コード
        {
            get { return _得意先コード; }
            set { this._得意先コード = value; NotifyPropertyChanged(); }
        }

        private int _取引区分 = 1;
        public int 取引区分
        {
            get { return _取引区分; }
            set { this._取引区分 = value; NotifyPropertyChanged(); }
        }

		private int _番号通知区分 = 1;
		public int 番号通知区分
		{
			get { return _番号通知区分; }
			set { _番号通知区分 = value; NotifyPropertyChanged(); }
		}
		private int _最終伝票表示区分 = 0;
		public int 最終伝票表示区分
		{
			get { return _最終伝票表示区分; }
			set { _最終伝票表示区分 = value; NotifyPropertyChanged(); }
		}

        #endregion

        ///*****************<Calculate>*****************///

        #region 合計Binding

        private int _入金予定額 = 0;
        public int 入金予定額
        {
            get { return _入金予定額; }
            set { this._入金予定額 = value; NotifyPropertyChanged(); }
        }

        private int _既入金額 = 0;
        public int 既入金額
        {
            get { return _既入金額; }
            set { this._既入金額 = value; NotifyPropertyChanged(); }
        }

        private int _出金相殺 = 0;
        public int 出金相殺
        {
            get { return _出金相殺; }
            set { this._出金相殺 = value; NotifyPropertyChanged(); }
        }

        private int _入金合計 = 0;
        public int 入金合計
        {
            get { return _入金合計; }
            set { this._入金合計 = value; NotifyPropertyChanged(); }
        }

        #endregion

        ///*****************<DataTable>*****************///

        #region テーブルBinding

        public byte[] spConfig = null;
        public byte[] spConfig2 = null;

        private DataTable _dUriageData = null;
        public DataTable 入金明細データ
        {
            get { return this._dUriageData; }
            set { this._dUriageData = value; NotifyPropertyChanged(); }
            //get
            //{
            //    return this._dUriageData;
            //}
            //set
            //{
            //    this._dUriageData = value;
            //    if (value == null)
            //    {
            //        this.spNyukin.ItemsSource = null;
            //    }
            //    else
            //    {
            //        this.spNyukin.ItemsSource = value.DefaultView;
            //    }
            //    NotifyPropertyChanged();
            //}
        }
        private DataTable _dUriageDataResult = null;
        public DataTable 入金明細データ検索結果
        {
            get { return this._dUriageDataResult; }
            set { this._dUriageDataResult = value; NotifyPropertyChanged(); }
        }

        private DataTable _oldData = null;
        public DataTable 過去入金明細データ
        {
            get { return this._oldData; }
            set { this._oldData = value; NotifyPropertyChanged(); }
        }

        private DataTable _oldResult = null;
        public DataTable 過去入金明細データ検索結果
        {
            get { return this._oldResult; }
            set { this._oldResult = value; NotifyPropertyChanged(); }
        }

        enum DataGetMode
        {
            first,
            last,
            previous,
            next,
            number,
        }
        DataGetMode datagetmode;

        #endregion


        #endregion

        public int? 初期明細番号 = null;
		public int? 初期行番号 = null;
		public bool IsUpdated = false;
		public DateTime 初期日付 = DateTime.Today;

        #region DLY08010

        /// <summary>
		/// 伝票入力
		/// </summary>
		public DLY08010()
		{
			InitializeComponent();
            this.DataContext = this;
		}

        #endregion

        /// <summary>
        /// 明細クリック時のアクション定義
        /// </summary>
        /// <summary>
        /// 明細クリック時のアクション定義
        /// </summary>
        public class cmd売上削除 : ICommand
        {
            private GcSpreadGrid _gcSpreadGrid;
            public cmd売上削除(GcSpreadGrid gcSpreadGrid)
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
                try
                {
                    this._gcSpreadGrid.IsEnabled = false;
                    CellCommandParameter cellCommandParameter = (CellCommandParameter)parameter;
                    if (cellCommandParameter.Area == SpreadArea.Cells
                        && cellCommandParameter.CellPosition.Row >= 0
                        && cellCommandParameter.CellPosition.Row < this._gcSpreadGrid.Rows.Count
                        )
                    {
                        this._gcSpreadGrid.SelectedItems.Remove(cellCommandParameter.CellPosition.Row);
                        this._gcSpreadGrid.Rows.Remove(cellCommandParameter.CellPosition.Row);
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    this._gcSpreadGrid.IsEnabled = true;
                }
            }
        }

        #region LOAD時

        /// <summary>
		/// 画面読み込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainWindow_Loaded_1(object sender, RoutedEventArgs e)
		{
            ScreenClear();

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            #region "権限関係"
            // 登録ボタン設定
            if (!権限Get.Authority_Update_Button(ccfg, this.GetType().Name))
            {
                // RibbonWindowViewBaseのプロパティに設定
                DataUpdateVisible = System.Windows.Visibility.Hidden;
            }
            #endregion
            入力者ID = ccfg.ユーザID;
            frmcfg = (ConfigDLY08010)ucfg.GetConfigValue(typeof(ConfigDLY08010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigDLY08010();
                ucfg.SetConfigValue(frmcfg);
            }
            else
            {
                //表示できるかチェック
                var WidthCHK = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - frmcfg.Left;
                if (WidthCHK > 100)
                {
                    this.Left = frmcfg.Left;
                }
                //表示できるかチェック
                var HeightCHK = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - frmcfg.Top;
                if (HeightCHK > 100)
                {
                    this.Top = frmcfg.Top;
                }
                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;
            }

            #endregion

			番号通知区分 = frmcfg.番号通知区分;
			最終伝票表示区分 = frmcfg.最終伝票表示区分;

            // 初期状態を保存（SPREADリセット時にのみ使用する）
            this.spConfig = AppCommon.SaveSpConfig(this.spNyukin);
            this.spConfig2 = AppCommon.SaveSpConfig(this.spOldNyukin);
            AppCommon.LoadSpConfig(this.spNyukin, this.spConfig);
            AppCommon.LoadSpConfig(this.spOldNyukin, this.spConfig2);
            base.MasterMaintenanceWindowList.Add("M01_TOK_TOKU_SCH", new List<Type> { null, typeof(SCH01010) });
            AppCommon.SetutpComboboxListToCell(this.spNyukin, "入金区分", "日次", "入金伝票入力", "入金区分", false);

			spNyukin.InputBindings.Add(new KeyBinding(spNyukin.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));

            ButtonCellType btn1 = this.spNyukin.Columns[0].CellType as ButtonCellType;
            btn1.Command = new cmd売上削除(spNyukin);

            if (初期明細番号 != null)
            {
                DetailsNumber = 初期明細番号;
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
                GetMeisaiData();
				return;
            }

			ChangeKeyItemChangeable(true);

			if (最終伝票表示区分 == 1)
			{
				datagetmode = DataGetMode.last;
				base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMeisaiNo, new object[] { 0, 3 }));
			}
		}

        #endregion

        #region エラーメッセージ

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        /// <param name="message"></param>
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
                case GetDetailsNumber:
                    base.SetFreeForInput();
                    var ds = data as DataSet;
                    this.ErrorMessage = string.Empty;
                    if (tbl.Rows.Count == 0)
                    {
                        if (this.MaintenanceMode != AppConst.MAINTENANCEMODE_ADD)
                        {
                            this.IDetailsNumber.Focus();
                            this.ErrorMessage = "該当する明細番号はありません。";
                            return;
                        }
                    }
                    else
                    {
                        this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
                        入力者ID = AppCommon.IntParse(tbl.Rows[0]["入力者ID"].ToString());
                    }

                    入金明細リスト = (List<DLY08010_Member>)AppCommon.ConvertFromDataTable(typeof(List<DLY08010_Member>), tbl);

                    if (MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
                    {
                        //新規データならSpreadに新規行を1件作成
                        spNyukin.Rows.AddNew();
                        //this.入金明細データ.Rows.InsertAt(this.入金明細データ.NewRow(), 0);
                        this.spNyukin.Select(new CellRange(0, 0), SelectionType.New);
                        this.spNyukin[0, spNyukin.Columns["明細番号"].Index].Value = this.DetailsNumber;
                        this.spNyukin[0, spNyukin.Columns["明細行"].Index].Value = 1;         
                    }
                    else
                    {
                        this.得意先ID = AppCommon.IntParse(tbl.Rows[0]["得意先ID"].ToString());
                        if (DBNull.Value.Equals(tbl.Rows[0]["入出金日付"]))
                        {
                            this.入金日付 = null;
                        }else
                        {
                            DateTime Wk;
							this.入金日付 = DateTime.TryParse(tbl.Rows[0]["入出金日付"].ToString(), out Wk) ? Wk : 初期日付;
                        }
                        //int MaxRow = spNyukin.RowCount;
                        //this.入金明細データ.Rows.InsertAt(this.入金明細データ.NewRow(), MaxRow);
                        //this.spNyukin.Select(new CellRange(0, 0), SelectionType.New);
                        //foreach (var row in spNyukin.Rows)
                        //{
                        //    spNyukin[MaxRow, spNyukin.Columns["明細番号"].Index].Value = this.DetailsNumber;
                        //    int num = Convert.ToInt32(spNyukin[MaxRow - 1, spNyukin.Columns["明細行"].Index].Value);
                        //    spNyukin[MaxRow, spNyukin.Columns["明細行"].Index].Value = num + 1;
                        //    spNyukin[MaxRow, spNyukin.Columns["入出金日付"].Index].Value = Convert.ToDateTime(this.Nyukinbi.Text);                            
                        //    //spNyukin[MaxRow, spNyukin.Columns["入出金日付"].Index].Value = spNyukin[MaxRow - 1, spNyukin.Columns["入出金日付"].Index].Value;
                        //}
                        //#region 画面LOCK
                        //this.LabelTextShiharaiId.IsKeyItem = true;
                        //this.Nyukinbi.IsKeyItem = true;
                        //#endregion
                        Calculate();              
                    }
                    //if (MaintenanceMode == AppConst.MAINTENANCEMODE_EDIT)
                    //{
                        //spNyukin.Locked = false;
                    //}
                    ChangeKeyItemChangeable(false);
                    SetFocusToTopControl();
                    break;

                case M01ALLTableNm:
                    if (tbl.Rows.Count > 0)
                    {
                        締日 = tbl.Rows[0]["締日"].ToString();
                        集金日 = tbl.Rows[0]["集金日"].ToString();
                        サイト = tbl.Rows[0]["サイト"].ToString();
                        DoEvents();
                        //if (MaintenanceMode == AppConst.MAINTENANCEMODE_EDIT)
                        //{
                        if (!base.CheckAllValidation())
                        {
                            this.ErrorMessage = "入力内容に誤りがあります。";
                            MessageBox.Show("入力内容に誤りがあります。");
                            SetFocusToTopControl();
                            return;
                        }
                            OgetData();
                        //}
                    }
                    break;

                case New_Details:
                    DetailsNumber = (int)data;
					this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, GetDetailsNumber, new object[] { DetailsNumber }));
                    break;

                case GetTekiyoName:
                    CatchupTekiyoName(tbl);
                    break;      

                case DLY08010_NData:
                    入金予定額 = Convert.ToInt32(tbl.Rows[0]["予定入金金額"].ToString());
                    既入金額 = Convert.ToInt32(tbl.Rows[0]["既入金額"].ToString());
                    出金相殺 = Convert.ToInt32(tbl.Rows[0]["出金相殺"].ToString());
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    spNyukin.Locked = false;
                    break;

                case DLY08010_OData:
                    this.過去入金明細データ検索結果 = (data as DataTable);
                    this.spOldNyukin.ItemsSource = this.過去入金明細データ検索結果.DefaultView;
                    break;

				case DLY08010_DELETE:
					IsUpdated = true;
                    MessageBox.Show("データを削除しました。");
                    ScreenClear();
                    break;

				case DLY08010_UPDATE:
					IsUpdated = true;
                    if (番号通知区分 == 1)
                    {
                        MessageBox.Show(string.Format("明細番号 {0} で登録しました。", (int)data));
                    }
                    ScreenClear();
                    break;

                case GetMeisaiNo:
                    if (data == null)
                    {
                        return;
                    }
                    int no = (int)data;
                    if (no < 1)
                    {
                        // データが無かった場合
                        switch (datagetmode)
                        {
                            case DataGetMode.first:
                            case DataGetMode.last:
                                this.ErrorMessage = "データがありません。";
                                break;
                            case DataGetMode.previous:
                                this.ErrorMessage = "先頭のデータです。";
                                break;
                            case DataGetMode.next:
                                this.ErrorMessage = "最後のデータです。";
                                break;
                            case DataGetMode.number:
                                break;
                        }
                        return;

                    }
                    if (no != this.DetailsNumber)
                    {
                        this.DetailsNumber = no;
                        GetMeisaiData();
                    }
                    break;

                case GetMaxMeisaiNo:
                    登録件数 = (int)data;
                    break;
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

            object elmnt = FocusManager.GetFocusedElement(this);

            var spgrid = ViewBaseCommon.FindVisualParent<GcSpreadGrid>(elmnt as Control);
            if (spgrid != null)
            {
                int actrow = spgrid.ActiveRowIndex;
                if (spgrid.ActiveColumn.Name == "摘要ID")
                {
                    Framework.Windows.Controls.UcLabelTwinTextBox dmy = new Framework.Windows.Controls.UcLabelTwinTextBox();
                    SCH08010 srch = new SCH08010();
                    srch.TwinTextBox = dmy;

                    if (srch.ShowDialog(this) == true)
                    {
                        spgrid.Cells[actrow, "摘要ID"].Text = dmy.Text1;
                        int sid = AppCommon.IntParse(dmy.Text1);
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, GetTekiyoName, sid, spgrid.ActiveRow.Index));
                    }
                }
            }
            else
            {
                ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);
            }
           
        }

        /// <summary>
        /// F9  登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            //フォーカス移動を行いSPREADの内容を確定させます
            SetFocusToTopControl();

            if (MaintenanceMode == null)
            {
                return;
            }
            if (!base.CheckAllValidation())
            {
                this.ErrorMessage = "入力内容に誤りがあります。";
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }
            var yesno = MessageBox.Show("入力内容を登録しますか？", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (yesno == MessageBoxResult.No)
            {
                return;
            }

            
            int pno = 0;
            if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
            {
                pno = -1;
                入力者ID = ccfg.ユーザID;
                foreach (DLY08010_Member row in 入金明細リスト)
                {
                    row.入力者ID = 入力者ID;
                }
            }
            else
            {
                foreach (DLY08010_Member row in 入金明細リスト)
                {
                    row.入力者ID = 入力者ID;
                }
            }


            bool gyochk = false;
            foreach (DLY08010_Member row in 入金明細リスト)
            {
                //row["d入金金額"]

                try
                {
                    row.入金金額 = row.d入金金額 == null ? 0 : AppCommon.IntParse(row.d入金金額.ToString());
                }
                catch (Exception)
                {
                    row.入金金額 = 0;
                }
                row.得意先ID = (int)得意先ID;
                row.入出金日付 = 入金日付;
                if (row.入金区分 != 0)
                {
                    gyochk = true;
                }
            };

            try
            {
                入金明細データ  = new DataTable();
                //リストをデータテーブルへ
                AppCommon.ConvertToDataTable(入金明細リスト, 入金明細データ);
            }
            catch
            {
                return;
            };

            if (入金明細データ.Rows.Count == 0 || gyochk == false)
            {
                this.ErrorMessage = "入力内容に誤りがあります。";
                MessageBox.Show("入力内容に誤りがあります。");
                return;
            }

            CommunicationObject com = new CommunicationObject(MessageType.RequestData, DLY08010_UPDATE, pno, this.入金明細データ);
            base.SendRequest(com);
			初期日付 = 入金日付 ?? DateTime.Today;
        }


        /// <summary>
        /// F10 入力取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("保存せずに入力を取り消してよろしいですか？"
                             , "確認"
                             , MessageBoxButton.YesNo
                             , MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                this.ScreenClear();
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
        /// F12  削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF12Key(object sender, KeyEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("表示されている情報を削除しますか？"
                            , "確認"
                            , MessageBoxButton.YesNo
                            , MessageBoxImage.Question
                            , MessageBoxResult.No);

            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            if (MaintenanceMode != AppConst.MAINTENANCEMODE_EDIT)
            {
                this.ErrorMessage = "登録データでは無いため削除できません";
                return;
            }

            //明細番号を元にデータを削除
            base.SendRequest(new CommunicationObject(MessageType.UpdateData, DLY08010_DELETE, new object[] { this.DetailsNumber }));
        }

        #endregion

        #region クリア

        /// <summary>
        /// データクリア
        /// </summary>
        public void ScreenClear()
        {
            //DetailsNumber = null;
            入金日付 = null;
            得意先ID = null;
            締日 = string.Empty;
            集金日 = string.Empty;
            サイト = string.Empty;
            入金予定額 = 0;
            既入金額 = 0;
            出金相殺 = 0;
            入金合計 = 0;
            明細番号 = null;
            DetailsNumber = null;
            //this.LabelTextShiharaiId.IsKeyItem = false;
            //this.Nyukinbi.IsKeyItem = false;

            //this.spNyukin.ItemsSource = null;
            入金明細リスト = null;

            this.spOldNyukin.ItemsSource = null;
            this.MaintenanceMode = string.Empty;
            this.spNyukin.Locked = true;
            ChangeKeyItemChangeable(true);
            Txt登録件数.Focusable = false;
            SetFocusToTopControl();

            //現在の登録件数を表示
            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMaxMeisaiNo));
        }

        #endregion

        #region 位置情報リセット

        /// <summary>
        /// 位置情報リセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColumnResert_Click(object sender, RoutedEventArgs e)
        {
            AppCommon.LoadSpConfig(this.spNyukin, this.spConfig);
        }

        #endregion

        #region Spread内でのF1機能

        /// <summary>
        /// Spred内でのF1機能
        /// </summary>
        /// <param name="tbl"></param>
        void CatchupTekiyoName(DataTable tbl)
        {
            if (tbl == null || tbl.Rows.Count == 0)
            {
                return;
            }
            int row = (int)tbl.Rows[0]["row"];
            //string id = string.Empty;
            string name = string.Empty;
            if(tbl.Rows[0].IsNull("摘要"))
            {

            }
            else
            {
                //id = (string)tbl.Rows[0]["摘要ID"];
                name = (string)tbl.Rows[0]["摘要"];
            }
            //spNyukin.Cells[row, "摘要ID"].Value = id;
            spNyukin.Cells[row, "摘要"].Value = name;
        }

        #endregion

        #region 自動採番

        /// <summary>
        /// 自動採番
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TokCode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetMeisaiData();
            }
        }

        #endregion

        /// <summary>
        /// GetMeisaiData
        /// </summary>
        void GetMeisaiData()
        {
            try
            {
                string 明細No = this.IDetailsNumber.Text;

                int i明細No;

                if (int.TryParse(明細No, out i明細No))
                {
                    this.DetailsNumber = i明細No;
                }
                if (DetailsNumber == null)
                {
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, New_Details, new object[] { }));
                }
                else
                {
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, GetDetailsNumber, new object[] { DetailsNumber, 0 }));
                }
            }
            catch (ExcelOpenException ex)
            {
                this.ErrorMessage = ex.Message;
            }
        }

        #region 行追加

        /// <summary>
        /// Spreadデータが書き換えられた時　新規行を追加 & 入金合計の計算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcspNyukin_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
        {
            string _originalText = null;
            Calculate();
            //NewRows();
            AppCommon.SpreadYMDCellChecks(sender, e, _originalText);
            #region SpreadFChange
            var grid = sender as GcSpreadGrid;
            if (e.CellPosition.ColumnName == "摘要ID" && grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Value != "")
            {
                var text = grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
                if (string.IsNullOrWhiteSpace(text) == true) return;

                int kid = AppCommon.IntParse(text);
                grid.Cells[e.CellPosition.Row, "摘要ID"].Value = kid;
                base.SendRequest(new CommunicationObject(MessageType.RequestData, GetTekiyoName, kid, e.CellPosition.Row));
            }
            #endregion
        }


        public void Calculate()
        {
            int LoopCount = 0;
            decimal num = 0;
            string a = string.Empty;

            foreach (var row in spNyukin.Rows)
            {
                num += Convert.ToDecimal(spNyukin[LoopCount, spNyukin.Columns["d入金金額"].Index].Value);
                LoopCount += 1;
            }

            入金合計 = Convert.ToInt32(num);
        }

        public void NewRows()
        {
            int MaxRow = spNyukin.RowCount;
            if (spNyukin[MaxRow - 1, spNyukin.Columns["d入金金額"].Index].Value != null && (int)spNyukin[MaxRow - 1, spNyukin.Columns["入金区分"].Index].Value != 0)
            {
                //this.入金明細データ.Rows.InsertAt(this.入金明細データ.NewRow(), MaxRow);

                int icol = spNyukin.ActiveCellPosition.Column;
                int irow = spNyukin.ActiveCellPosition.Row;
                spNyukin.Rows.AddNew();
                spNyukin.ActiveCellPosition = new CellPosition(irow, icol);

                //DataRow modRow = null;
                //modRow = this.入金明細データ.NewRow();
                //this.入金明細データ.Rows.Add(modRow);

                foreach (var row in spNyukin.Rows)
                {
                    spNyukin[MaxRow, spNyukin.Columns["明細番号"].Index].Value = this.DetailsNumber;
                    int num = Convert.ToInt32(spNyukin[MaxRow - 1, spNyukin.Columns["明細行"].Index].Value);
                    spNyukin[MaxRow, spNyukin.Columns["明細行"].Index].Value = num + 1;
                    spNyukin[MaxRow, spNyukin.Columns["入出金日付"].Index].Value = Convert.ToDateTime(this.Nyukinbi.Text);
                }
            }
        }


        #endregion

        #region PreviewKeyDown


        private void txtboxdate_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.Nyukinbi.Text == "")
                {
                    //入金日付 = Convert.ToDateTime(DateTime.Today.Year.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Day.ToString());
					入金日付 = 初期日付;
                }
            }
        }


        /// <summary>
        /// 得意先でEnterを押下された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!base.CheckAllValidation())
                {
                    this.ErrorMessage = "入力内容に誤りがあります。";
                    MessageBox.Show("入力内容に誤りがあります。");
                    SetFocusToTopControl();
                    return;
                }
                if (締日 != "")
                {
                    OgetData();
                }
            }
        }


        public void OgetData()
        {
          
            //DateTime 入出金日付;
            int iサイト = Convert.ToInt32(サイト);
            //if (this.Nyukinbi.Text == "")
            //{
            //    入出金日付 = Convert.ToDateTime(DateTime.Today.Year.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Day.ToString());
            //    入金日付 = 入出金日付;
            //}
            //else
            //{
            //    入出金日付 = DateTime.Parse(this.Nyukinbi.Text);
            //}

            if (this.入金日付 == null)
            {
                this.ErrorMessage = "入金日付が未入力です";
                return;
            }
            if (this.得意先ID == null)
            {
                this.ErrorMessage = "得意先が未入力です";
                return;
            }

            if (MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
            {
                if (this.spNyukin.Rows.Count == 0)
                {
                    //新規データならSpreadに新規行を1件作成
                    //this.入金明細データ.Rows.InsertAt(this.入金明細データ.NewRow(), 0);

                    //int icol = spNyukin.ActiveCellPosition.Column;
                    //int irow = spNyukin.ActiveCellPosition.Row;
                    入金明細リスト =  new List<DLY08010_Member>();
                    spNyukin.Rows.AddNew();
                    //spNyukin.ActiveCellPosition = new CellPosition(irow, icol);

                    this.spNyukin.Select(new CellRange(0, 0), SelectionType.New);
                    this.spNyukin[0, spNyukin.Columns["明細番号"].Index].Value = this.DetailsNumber;
                    this.spNyukin[0, spNyukin.Columns["明細行"].Index].Value = 1;
                }

                this.spNyukin[0, spNyukin.Columns["入出金日付"].Index].Value = 入出金日付;
                this.spNyukin[0, spNyukin.Columns["得意先ID"].Index].Value = this.得意先ID;
            }
            //入金予定額取得
            base.SendRequest(new CommunicationObject(MessageType.RequestData, DLY08010_NData, new object[] { 得意先ID, 入金日付, DetailsNumber }));
            //残高表取得
            base.SendRequest(new CommunicationObject(MessageType.RequestData, DLY08010_OData, new object[] { 得意先ID, 入金日付, iサイト }));
        }

        #endregion

        #region TextChanged

        /// <summary>
        /// F1情報を元に締日・集金日・サイトを取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelTextChanged(object sender, RoutedEventArgs e)
        {
            base.SendRequest(new CommunicationObject(MessageType.RequestData, M01ALLTableNm, new object[] { 得意先ID, 0 }));
        }
   
        #endregion

        private void FirstIdButton_Click(object sender, RoutedEventArgs e)
        {
            datagetmode = DataGetMode.first;
            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMeisaiNo, 0, 0));
        }

        private void BeforeIdButton_Click(object sender, RoutedEventArgs e)
        {
            datagetmode = DataGetMode.previous;
            
                base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMeisaiNo, DetailsNumber, 1));
        }

        private void NextIdButton_Click(object sender, RoutedEventArgs e)
        {
            datagetmode = DataGetMode.next;
                base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMeisaiNo, DetailsNumber, 2));
        }

        private void LastIdButoon_Click(object sender, RoutedEventArgs e)
        {
            datagetmode = DataGetMode.last;
            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMeisaiNo, new object[] { 0, 3 }));
        }

        private void Button_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == false)
            {
                (sender as Button).IsEnabled = true;
            }
        }

        //private void spread_CellEnter(object sender, SpreadCellEnterEventArgs e)
        //{
        //    var grid = sender as GcSpreadGrid;
        //    if (grid == null) return;
        //    if (grid.RowCount == 0) return;
        //    this._originalText = grid.Cells[e.Row, e.Column].Text;
        //}

        //Deleteキー対処
        private void sp入金データPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Enter)
            {
                Calculate();

                if (spNyukin.ActiveCellPosition.ColumnName == "入金区分")
                {
                    System.Windows.Forms.SendKeys.SendWait("{F4}");
                }
                if (spNyukin.ActiveCellPosition.ColumnName == "Str手形期日" && spNyukin.ActiveCellPosition.Row == (spNyukin.RowCount - 1))
                {
                    NewRows();
                }

            }
        }

        private void RibbonTab_PreviewMouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Ribbon_PreviewMouseRightButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Window_Close(object sender, EventArgs e)
		{
			spNyukin.InputBindings.Clear();
			spOldNyukin.InputBindings.Clear();
			this.入金明細リスト = null;
			this.過去入金明細データ = null;

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
			frmcfg.Width = this.Width;
			frmcfg.番号通知区分 = 番号通知区分;
			frmcfg.最終伝票表示区分 = 最終伝票表示区分;
            ucfg.SetConfigValue(frmcfg);

        }


    }
}
