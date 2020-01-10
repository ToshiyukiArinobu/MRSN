using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace KyoeiSystem.Application.Windows.Views
{
   
    /// <summary>
    /// MeinMenu画面
    /// </summary>
	public partial class MAINMANU : WindowGeneralBase
    {
        #region メニューの起動

        private void Start(string cname, Dictionary<string, object> plist = null)
        {
            Window wnd = AppCommon.Start(cname, plist) as Window;
            if (wnd != null)
                wnd.Closed += frm_Closed;
        }

        private void Start(Type ctype, Dictionary<string, object> plist = null)
        {
            Window wnd = AppCommon.Start(ctype, plist) as Window;
            if (wnd != null)
                wnd.Closed += frm_Closed;
        }

        void frm_Closed(object sender, EventArgs e)
        {
            // ハンドラの削除
            (sender as Window).Closed -= frm_Closed;
        }

        #endregion

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        public UserConfig usercfg = null;
		public CommonConfig ccfg = null;

		/// <summary>
		/// 画面固有設定項目のクラス定義
		/// ※ 必ず public で定義する。
		/// </summary>
        public class ConfigWindowTest : FormConfigBase
        {
			public string[] gamenID ;
		}
        /// ※ 必ず public で定義する。
        public ConfigWindowTest frmcfg = null;

        /// <summary>
        /// ログイン権限の使用可能機能定義テーブル
        /// </summary>
        private DataTable AuthFuncTbl;

        /// <summary>
        /// 自社販社区分 内包データ
        /// </summary>
        private enum 自社販社区分 : int
        {
            自社 = 0,
            販社 = 1
        }
        #endregion

        #region 定数定義

        private const string ReqLOGOUT = "Logout";
        private const string ReqIMAGE = "MaineImage";
        private const string Oshirase = "Oshirase";
		private const string USER_LOGOUT = "updateLogout";
        private const string MsgShow = "MessgeShow";

        /// <summary>権限テーブル列名：タブグループ番号</summary>
        private const string AUTH_COLUMN_TAB_NUM = "TabNum";
        /// <summary>権限テーブル列名：プログラムID</summary>
        private const string AUTH_COLUMN_PROGRAM_ID = "ProgramID";
        /// <summary>権限テーブル列名：使用可能FLG</summary>
        private const string AUTH_COLUMN_ENABLE_FUNC = "FuncEnable";
        /// <summary>権限テーブル列名：更新可能FLG</summary>
        private const string AUTH_COLUMN_ENABLE_UPDATE = "UpdateEnable";

        #endregion

		#region バージョンプロパティ

		// バージョン番号は、ClickOnceからインストールした場合とローカル起動した場合で
		// それぞれのバージョンが取得される。
		// 画面のロード時に AppCommon.GetAppCommonData(this).Version を取得しておく。
		private System.Version _version = new Version();
		public System.Version Version
		{
			get { return this._version; }
			set
			{
				this._version = value;
				this.TitleString = string.Format("HAKOBO.NET {0}", value);
				this.VersionString = string.Format("Ver:{0}", value);
			}
		}
		private string _titleString = null;
		public string TitleString
		{
			get { return this._titleString; }
			set { this._titleString = value; NotifyPropertyChanged(); }
		}
		private string _versionString = null;
		public string VersionString
		{
			get { return this._versionString; }
			set { this._versionString = value; NotifyPropertyChanged(); }
		}
		#endregion

		#region プロパティ等

		private string _DayOfWeek = "(" + ("日月火水木金土").Substring(AppCommon.IntParse(DateTime.Now.DayOfWeek.ToString("d")),1) + ")";
        public string DayOfWeek
        {
            get { return this._DayOfWeek; }
            set { this._DayOfWeek = value; NotifyPropertyChanged(); }
        }
        public int Flgs = 0;

        private BitmapImage _screenImg;
        public BitmapImage ScreenImg
        {
            get { return this._screenImg; }
            set
            {
                this._screenImg = value;
                NotifyPropertyChanged();
            }
        }

        private DateTime _NowTime = DateTime.Today;
        public DateTime NowTime
        {
            get { return this._NowTime; }
            set { this._NowTime = value; NotifyPropertyChanged(); }
        }

        private string _CompanyName;
        public string CompanyName
        {
            get { return this._CompanyName; }
            set { this._CompanyName = value; NotifyPropertyChanged(); }
        }

        private string _メモ;
        public string メモ
        {
            get { return this._メモ; }
            set { this._メモ = value; NotifyPropertyChanged(); }
        }

        private string _メッセージ;
        public string メッセージ
        {
            get { return this._メッセージ; }
            set { this._メッセージ = value; NotifyPropertyChanged(); }
        }

        private byte[] _driverPicData;
        public byte[] DriverPicData
        {
            get { return this._driverPicData; }
            set
            {
                this._driverPicData = value;
                NotifyPropertyChanged();
            }
        }

        private int _testcombovalue = -2;
        public int testcombovalue
        {
            get
            {
                return this._testcombovalue;
            }
            set
            {
                this._testcombovalue = value;
                NotifyPropertyChanged();
            }
        }

        private bool isIDSelected = true;
        public bool IsIDSelected
        {
            get
            {
                return this.isIDSelected;
            }
            set
            {
                this.isIDSelected = value;
                NotifyPropertyChanged();
            }
        }
        private bool isNameSelected = false;
        public bool IsNameSelected
        {
            get
            {
                return this.isNameSelected;
            }
            set
            {
                this.isNameSelected = value;
                NotifyPropertyChanged();
            }
        }
        private string[] _paramYM = { string.Empty, string.Empty };
        public string paramYYYY
        {
            get { return _paramYM[0]; }
            set { _paramYM[0] = value; NotifyPropertyChanged(); }
        }
        private object _drv_id;
        public object TBLDriverID
        {
            get { return _drv_id; }
            set { _drv_id = value; NotifyPropertyChanged(); }
        }
        public string paramMM
        {
            get { return _paramYM[1]; }
            set { _paramYM[1] = value; NotifyPropertyChanged(); }
        }

        private string currenttime = string.Empty;
        public string CurrentTime
        {
            get
            {
                return this.currenttime;
            }
            set
            {
                this.currenttime = value;
                NotifyPropertyChanged();
            }
        }

        private DataTable griddata = new DataTable();
        public DataTable TESTDATA
        {
            get
            {
                return this.griddata;
            }
            set
            {
                this.griddata = value;
                NotifyPropertyChanged();
            }
        }
        private string tokuisakiID;
        public string TokuisakiID
        {
            get
            {
                return this.tokuisakiID;
            }
            set
            {
                this.tokuisakiID = value;
                NotifyPropertyChanged();
            }
        }
        private string _tantoID;
        public string 担当者ID
        {
            get
            {
                return this._tantoID;
            }
            set
            {
                this._tantoID = value;
                NotifyPropertyChanged();
            }
        }

        private string _LicenseID;
        public string LicenseID
        {
            get
            {
                return this._LicenseID;
            }
            set
            {
                this._LicenseID = value; NotifyPropertyChanged();
            }
        }

        private string _tantoName;
        public string 担当者名
        {
            get
            {
                return this._tantoName;
            }
            set
            {
                this._tantoName = value;
                NotifyPropertyChanged();
            }
        }

        private string _tantoInfomation;
        public string tantoInfomation
        {
            get
            {
                return this._tantoInfomation;
            }
            set
            {
                this._tantoInfomation = value;
                NotifyPropertyChanged();
            }

        }

        private string changedColumns = string.Empty;
        public string ChangedColumns
        {
            get
            {
                return this.changedColumns;
            }
            set
            {
                this.changedColumns = value;
                NotifyPropertyChanged();
            }
        }
        private byte[] _pic;
        public byte[] Picture
        {
            get
            {
                return this._pic;
            }
            set
            {
                this._pic = value;
                NotifyPropertyChanged();
            }
        }

        private DateTime? _dateTest = null;
        public DateTime? DateTest
        {
            get { return _dateTest; }
            set { _dateTest = value; NotifyPropertyChanged(); }
        }

        private string _SystemToDay = string.Empty;
        public string SystemToDay
        {
            get { return _SystemToDay; }
            set { _SystemToDay = value; NotifyPropertyChanged(); }
        }

        private string _SystemTime = string.Empty;
        public string SystemTime
        {
            get { return _SystemTime; }
            set { _SystemTime = value; NotifyPropertyChanged(); }
        }

        private DataTable _dtOshirase = null;
        public DataTable dtOshirase
        {
            get { return _dtOshirase; }
            set { _dtOshirase = value; NotifyPropertyChanged(); }
        }

        private bool UserLoggedOff = false;
        private bool IsLoggingOffUser = false;
        private bool LicenseLoggedOff = false;
        private bool IsLoggingOffLicense = false;

        #endregion

        #region MAINMANU

        public MAINMANU()
            : base()
        {
            InitializeComponent();
            this.DataContext = this;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.Timer.Content = DateTime.Now.ToString("HH:mm:ss");
        }

        #endregion

        #region Load時

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
			this.Version = AppCommon.GetAppCommonData(this).Version;

			// ログイン更新定期実行用（デフォルトは４分：この値を変更する場合はWCF側の5分制御の調整が必要）
			int tim = 240;	// 4分（秒単位：過度に大きな値は無意味）
			var plist = (NameValueCollection)ConfigurationManager.GetSection("serviceSettings");
			if (plist != null)
			{
				if (this.viewsCommData.WithLicenseDB != true)
				{
					// ローカル実行時は定期的に更新する意味がない
					tim = 0;
				}
			}
			if (tim > 0)
			{
				base.TimerLoopStart(tim * 1000);
			}

			//M70から自社画像を取得しメニューに設定
            base.SendRequest(new CommunicationObject(MessageType.RequestData, ReqIMAGE, new object[] { }));

            #region 設定項目取得
            usercfg = AppCommon.GetConfig(this);
            ccfg = (CommonConfig)usercfg.GetConfigValue(typeof(CommonConfig));
            frmcfg = (ConfigWindowTest)usercfg.GetConfigValue(typeof(ConfigWindowTest));
            担当者ID = ccfg.ユーザID.ToString();
            担当者名 = ccfg.ユーザ名.ToString();
            LicenseID = ccfg.ライセンスID;
            tantoInfomation = " LoginName : " + 担当者名.ToString();

            CreateAuthDataTable(ccfg);
            #endregion

            if (frmcfg == null)
            {
                frmcfg = new ConfigWindowTest();
                usercfg.SetConfigValue(frmcfg);

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
                メモ = frmcfg.Memo;

            }

            this.appLog.Debug("テストメニュ開始");
            int id;
            if (int.TryParse(this.担当者ID, out id) != true)
            {
                MessageBox.Show("担当者IDは数値を入力してください");
                return;
            }

			if (this.viewsCommData.WithLicenseDB == true)
			{
				SendRequest(new CommunicationObject(MessageType.RequestLicense, MsgShow, LicenseID));
			}


            ///* 権限によりボタン制御追加 */
            ///* ボタン使用可否設定 */
            Authority_Set(this.ccfg);

            // メニューフォーカスは表示先頭に付与する
            if (RibbonF1.Visibility != System.Windows.Visibility.Hidden)
                Keyboard.Focus(Menu1);
            else if (RibbonF2.Visibility != System.Windows.Visibility.Hidden)
                Keyboard.Focus(Menu2);
            else if (RibbonF3.Visibility != System.Windows.Visibility.Hidden)
                Keyboard.Focus(Menu3);
            else if (RibbonF4.Visibility != System.Windows.Visibility.Hidden)
                Keyboard.Focus(Menu4);
            else if (RibbonF5.Visibility != System.Windows.Visibility.Hidden)
                Keyboard.Focus(Menu5);
            else if (RibbonF6.Visibility != System.Windows.Visibility.Hidden)
                Keyboard.Focus(Menu6);
            else if (RibbonF7.Visibility != System.Windows.Visibility.Hidden)
                Keyboard.Focus(Menu7);
            else if (RibbonF8.Visibility != System.Windows.Visibility.Hidden)
                Keyboard.Focus(Menu8);
            else if (RibbonF9.Visibility != System.Windows.Visibility.Hidden)
                Keyboard.Focus(Menu9);
            else if (RibbonF10.Visibility != System.Windows.Visibility.Hidden)
                Keyboard.Focus(Menu10);
            else if (RibbonF11.Visibility != System.Windows.Visibility.Hidden)
                Keyboard.Focus(Menu11);
            else { }

        }

        #endregion

        #region Window_Closing

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
			if (UserLoggedOff && LicenseLoggedOff)
			{
				base.TimerLoopStop();
				return;
			}

			// ユーザ操作によるクローズ時は、通信処理のために一旦キャンセルする。
			// その後、通信が終了後に再度内部処理によりでクローズイベントが起こる。
			// UserLoggedOff と LicenseLoggedOff は、その制御のためのスイッチとして使う。
			e.Cancel = true;

			if (IsLoggingOffUser != true)
			{
				IsLoggingOffUser = true;
				CfgUpdateClicked(null, null);
			}

			if (IsLoggingOffLicense != true)
			{
				IsLoggingOffLicense = true;
				if (this.viewsCommData.WithLicenseDB == true)
				{
					var apcmn = AppCommon.GetAppCommonData(this);
					SendRequest(new CommunicationObject(MessageType.RequestLicense, USER_LOGOUT, apcmn.CommonDB_UserId));
				}
				else
				{
					LicenseLoggedOff = true;
				}
			}
		}

        private void OnColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            this.ChangedColumns = string.Format("{0} -> {1}", e.Column.ColumnName, e.ProposedValue);
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Cancel)
            {
                return;
            }
        }

        #endregion

        #region LicenseDB(タイマーイベント)

        /// <summary>
        /// タイマーイベント受信時の処理
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedTimer(CommunicationObject message)
        {
			if (this.viewsCommData.WithLicenseDB == true)
			{
				var apcmn = AppCommon.GetAppCommonData(this);
				this.CurrentTime = string.Format("{0:yyyy/MM/dd HH:mm.ss}", DateTime.Now);
				SendRequest(new CommunicationObject(MessageType.RequestLicense, "updateAccessDateTime", apcmn.CommonDB_UserId));
			}
        }

        #endregion

        #region 日次[F1]

        /// <summary>
        /// 仕入入力ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_DLY01010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(DLY01010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 仕入入力(返品)ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_DLY01020(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(DLY01020));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

		/// <summary>
		/// 揚り入力ボタン押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Click_DLY02010(object sender, RoutedEventArgs e)
		{
			try
			{
                Start(typeof(DLY02010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
        /// 売上入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_DLY03010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(DLY03010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 売上入力(返品)ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_DLY03020(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(DLY03020));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 商品移動/振替入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_DLY04010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(DLY04010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 振替入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_DLY04011(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(DLY04011));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 入金入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_DLY05010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(DLY05010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 出金入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_DLY06010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(DLY06010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 揚り依頼入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_DLY07010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(DLY07010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 販社売上修正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_DLY12010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(DLY12010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_DLY08010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(DLY08010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_DLY09010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(DLY09010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_DLY19010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(DLY19010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_DLY23010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(DLY23010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Click_DLY24010(object sender, RoutedEventArgs e)
		{
			try
			{
                //Start(typeof(DLY24010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        #endregion

        #region 随時[F2]

        /// <summary>
        /// 仕入データ問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_ZIJ01010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(ZIJ01010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 支払明細問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_ZIJ02010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(ZIJ02010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 入金問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_ZIJ03010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(ZIJ03010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 出金問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_ZIJ04010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(ZIJ04010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 売上明細問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_ZIJ05010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(ZIJ05010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 商品移動/振替入力問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_ZIJ06010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(ZIJ06010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 商品入出荷問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_ZIJ07010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(ZIJ07010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 商品在庫問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_ZIJ09010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(ZIJ09010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 揚り明細問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_ZIJ10010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(ZIJ10010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region 得意先管理[F3]
        /// <summary>
        /// 売上データ一覧表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_TKS02010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(TKS02010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 売掛台帳
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_TKS02011(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(TKS02011));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 納品書出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_DLY11010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(DLY11010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 請求締集計
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_TKS01010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(TKS01010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 請求書発行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_TKS01020(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(TKS01020));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 請求一覧表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_TKS03010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(TKS03010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 入金予定表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_TKS07010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(TKS07010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 入金予定実績表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_TKS08010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(TKS08010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 都度請求締集計
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_TKS09010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(TKS09010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 確定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_TKS90010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(TKS90010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region 支払先管理[F4]
        /// <summary>
        /// 仕入データ一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_SHR01010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(SHR01010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 買掛台帳
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_SHR02010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(SHR02010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 支払締集計
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_SHR03010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(SHR03010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 支払明細表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_SHR05020(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(SHR05020));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 支払一覧表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_SHR05010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(SHR05010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 出金予定表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_SHR06010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(SHR06010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region 倉庫管理[F5]
        /// <summary>
        /// 乗務員管理表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_ZIK01010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(ZIK01010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 棚卸入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_ZIK02010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(ZIK02010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 差異表印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_ZIK03010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(ZIK03010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 棚卸入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_ZIK04010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(ZIK04010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 在庫残高一覧表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_ZIK05010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(ZIK05010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #endregion

        #region 分析資料[F6]

        /// <summary>
        /// 得意先・商品別売上統計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_BSK01010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(BSK01010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 得意先別売上統計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_BSK02010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(BSK02010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// シリーズ･商品別売上統計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_BSK03010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(BSK03010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 担当者･得意先別売上統計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_BSK04010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(BSK04010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 年次販社売上調整
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_BSK05010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(BSK05010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 相殺請求管理表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_BSK09010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(BSK09010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region 車種管理[F7] ※コメントアウト

        /// <summary>
        /// 車種別日計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_SRY04010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(SRY04010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 車種収支実績表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_SRY11010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(SRY11010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 車種合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_SRY12010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(SRY12010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 車種統計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_SRY13010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(SRY13010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region マスタ1[F8]

        /// <summary>
        /// 取引先マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST01010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(MST01010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 出荷先マスタ呼び出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST01020(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(MST01020));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 品番マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST02010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(MST02010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 担当者マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST23010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(MST23010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// セット品番マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST03010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(MST03010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 商品中分類マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST04010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(MST04010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ブランドマスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST04020(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(MST04020));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// シリーズマスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST04021(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(MST04021));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 品群マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST04022(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(MST04022));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 色マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST05010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(MST05010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST06010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST06010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST07010(object sender, RoutedEventArgs e)
        {
            try
            {
               //Start(typeof(MST07010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 摘要マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST08010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(MST08010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST09010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST09010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 倉庫マスタ呼び出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST12020(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(MST12020));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST14010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST14010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 仕入先商品売価設定マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST17010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(MST17010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 得意先商品売価設定マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST18010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(MST18010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 外注先商品売価設定マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST19010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(MST19010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 品番マスタ一括修正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST02011(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(MST02011));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 取引先マスタ一括修正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST01011(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(MST01011));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 製品原価一括修正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST03011(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(MST03011));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST20010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST20010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST21010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST21010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST22010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST22010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST31010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST31010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST32010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST32010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST33010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST33010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Click_MST34010(object sender, RoutedEventArgs e)
		{
			try
			{
                //Start(typeof(MST34010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST01015(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST01015));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region マスタ2[F9]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST06030(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST06030));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST10010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST10010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST12010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST12010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 消費税率マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST13010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(MST13010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 自社マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST16010(object sender, RoutedEventArgs e)
        {
            try
            {
                Start(typeof(MST16010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST25010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST25010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST26010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST26010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST28010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST28010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST29010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST29010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST30010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST30010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #endregion

        #region 年次[F10] ※コメントアウト

        /// <summary>
        /// 得意先月別合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_NNG01010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(NNG01010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 支払先月別合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_NNG02010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(NNG02010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 乗務員月別合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_NNG03010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(NNG03010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 車輌月別合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_NNG04010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(NNG04010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 車種月別合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_NNG05010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(NNG05010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 部門別売上合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_NNG06010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(NNG06010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 部門別売上日計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_NNG07010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(NNG07010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 部門月別合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_NNG08010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(NNG08010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region その他[F11] ※コメントアウト

        /// <summary>
        /// 請求書レイアウト設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST90010(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST90010));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// CSV一括出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST90030(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST90030));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// マスタCSV取込
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST90090(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST90090));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 伝票CSV取込
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_MST90060(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start(typeof(MST90060));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Click_Dmy(object sender, RoutedEventArgs e)
        {
            // ダミークリックイベント
        }

        #endregion

        #region 権限設定用
        /// <summary>
        /// 権限設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MST90100_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.ccfg.権限 != 0)
                {
                    MessageBox.Show("起動する権限が有りません。","権限確認",MessageBoxButton.OK,MessageBoxImage.Information);
                }
                else
                {
                    Start(typeof(MST90100));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 権限マスタよりボタンの表示・非表示を設定
        /// </summary>
        /// <param name="AuthorityData"></param>
        private void Authority_Set(CommonConfig AuthorityData)
        {
            try
            {
                if (this.ccfg.権限 != 0)
                {
                    Kengen.Visibility = System.Windows.Visibility.Hidden;
                }

                // 20190726CB-S
                // ログインユーザーの自社コードが"マルセン"ではない場合DLY12010(販社売上修正)を使用不可にする
                if (this.ccfg.自社販社区分 == (int)自社販社区分.自社)
                {
                    ZIJ01010.Content = "    1. 仕入問合せ";
                }
                else
                {
                    ZIJ01010.Content = "    1. マルセン自動仕入問合せ";
                    DLY12010.Visibility = System.Windows.Visibility.Hidden;
                }
                // 20190726CB-E

                if (AuthorityData.プログラムID != null)
                {
                    string[] プログラムID = AuthorityData.プログラムID.ToArray();
                    foreach (var ID in AuthorityData.プログラムID)
                    {
                        try
                        {
                            Button btn = (Button)this.FindName(ID);
                            if (!AuthorityData.使用可能FLG[Array.IndexOf(プログラムID, ID)])
                            {
                                btn.Visibility = System.Windows.Visibility.Hidden;
                            }
                        }
                        catch
                        {
                            continue;
                        }

                    }
                }

                #region タブボタンの表示表示設定
                // REMARKS:ループ最大値にはファンクションの最大値を設定
                for(int funcNum = 1; funcNum < 12; funcNum++)
                {
                    // 権限がシス管の場合は常にtrue、それ以外の場合は表示機能の存在チェック
                    bool isEnableFunc = AuthorityData.権限 == 0 ? true : isCheckMenuFuncVisibled(funcNum);
                    RibbonGroup wkRibbon;
                    Button wkButton;

                    #region 作業用変数に各ファンクションコントロールを割り当て
                    switch (funcNum)
                    {
                        case 1:
                            wkRibbon = RibbonF1;
                            wkButton = Menu1;
                            // メニューに紐づく各イベントを除去(共通部はswitch後に除去)
                            Menu1.Click -= Tab1Click;
                            break;

                        case 2:
                            wkRibbon = RibbonF2;
                            wkButton = Menu2;
                            Menu2.Click -= Tab2Click;
                            break;

                        case 3:
                            wkRibbon = RibbonF3;
                            wkButton = Menu3;
                            Menu3.Click -= Tab3Click;
                            break;

                        case 4:
                            wkRibbon = RibbonF4;
                            wkButton = Menu4;
                            Menu4.Click -= Tab4Click;
                            break;

                        case 5:
                            wkRibbon = RibbonF5;
                            wkButton = Menu5;
                            Menu5.Click -= Tab5Click;
                            break;

                        case 6:
                            wkRibbon = RibbonF6;
                            wkButton = Menu6;
                            Menu6.Click -= Tab6Click;
                            break;

                        case 7:
                            wkRibbon = RibbonF7;
                            wkButton = Menu7;
                            Menu7.Click -= Tab7Click;
                            break;

                        case 8:
                            wkRibbon = RibbonF8;
                            wkButton = Menu8;
                            Menu8.Click -= Tab8Click;
                            break;

                        case 9:
                            wkRibbon = RibbonF9;
                            wkButton = Menu9;
                            Menu9.Click -= Tab9Click;
                            break;

                        case 10:
                            wkRibbon = RibbonF10;
                            wkButton = Menu10;
                            Menu10.Click -= Tab10Click;
                            break;

                        case 11:
                            wkRibbon = RibbonF11;
                            wkButton = Menu11;
                            Menu11.Click -= Tab11Click;
                            break;

                        case 12:
                            //wkRibbon = RibbonF12;
                            //wkButton = Menu12;
                            //Menu12.Click -= Tab12Click;
                            //break;

                        default:
                            continue;

                    }
                    #endregion

                    // XAML上で非表示設定されている場合は結果反映をおこなわない
                    if (wkRibbon.Visibility == System.Windows.Visibility.Hidden)
                        continue;

                    wkRibbon.Visibility = isEnableFunc ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
                    if (wkRibbon.Visibility == System.Windows.Visibility.Hidden)
                    {
                        // メニューに紐づく各イベントを除去
                        wkButton.GotFocus -= KeyboardChange;
                        wkButton.PreviewKeyDown -= OnKeyDown;
                        wkButton.LostFocus -= KeybordLost;
                    }

                }
                #endregion

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 権限機能テーブルの作成
        /// </summary>
        /// <param name="AuthorityData"></param>
        /// <returns></returns>
        private DataTable CreateAuthDataTable(CommonConfig AuthorityData)
        {
            AuthFuncTbl = new DataTable();
            AuthFuncTbl.Columns.Add(AUTH_COLUMN_TAB_NUM, typeof(int));
            AuthFuncTbl.Columns.Add(AUTH_COLUMN_PROGRAM_ID, typeof(string));
            AuthFuncTbl.Columns.Add(AUTH_COLUMN_ENABLE_FUNC, typeof(bool));
            AuthFuncTbl.Columns.Add(AUTH_COLUMN_ENABLE_UPDATE, typeof(bool));

            for (int i = 0; i < AuthorityData.プログラムID.Length; i++)
            {
                DataRow nRow = AuthFuncTbl.NewRow();
                nRow[AUTH_COLUMN_TAB_NUM] = AuthorityData.タブグループ番号[i];
                nRow[AUTH_COLUMN_PROGRAM_ID] = AuthorityData.プログラムID[i];
                nRow[AUTH_COLUMN_ENABLE_FUNC] = AuthorityData.使用可能FLG[i];
                nRow[AUTH_COLUMN_ENABLE_UPDATE] = AuthorityData.データ更新FLG[i];

                AuthFuncTbl.Rows.Add(nRow);

            }

            return AuthFuncTbl;

        }

        /// <summary>
        /// 対象のタブ番号に属する機能が１つ以上存在するかチェックする
        /// </summary>
        /// <param name="tabNum">(権限)タブグループ番号</param>
        /// <returns></returns>
        private bool isCheckMenuFuncVisibled(int tabNum)
        {
            if (AuthFuncTbl.AsEnumerable().Any())
            {
                // 権限テーブル作成済
                var funcCount =
                    AuthFuncTbl.AsEnumerable()
                        .Where(w =>
                            w.Field<int>(AUTH_COLUMN_TAB_NUM) == tabNum &&
                            w.Field<bool>(AUTH_COLUMN_ENABLE_FUNC))
                        .Count();

                return funcCount > 0;

            }
            else
            {
                // 権限テーブルの再作成を試みる
                CreateAuthDataTable(ccfg);

                if (AuthFuncTbl.AsEnumerable().Any())
                    // 作成できた場合はチェック処理へ
                    return isCheckMenuFuncVisibled(tabNum);

                else
                    // 作成できなかった場合は不可として返す
                    return false;

            }

        }

        #endregion

        #region データ受信メソッド

        public override void OnReceivedResponseData(CommunicationObject message)
        {
            var data = message.GetResultData();
            DataTable tbl = (data is DataTable) ? data as DataTable : null;
            switch (message.GetMessageName())
            {


                case ReqLOGOUT:
					UserLoggedOff = true;
                    this.Close();
                    break;

                case ReqIMAGE:

                    //免許、車検の更新表示
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, Oshirase, new object[] { }));

                    break;
                case MsgShow:
                    メッセージ = tbl.Rows[0]["メッセージ"].ToString() == "" ? string.Empty : tbl.Rows[0]["メッセージ"].ToString();
                    if (メッセージ != string.Empty)
                    {
                        rowDefinition.Height = new GridLength((double)450);

                        Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
                        int MegLen = sjisEnc.GetByteCount(メッセージ);

                        StoryBd.Stop();
                        MsgScllol.From = (double)0.0;
                        MsgScllol.To = (double)(1140.0 + (MegLen * 8));
                        StoryBd.Begin();
                    }
                    else
                    {
                        rowDefinition.Height = new GridLength((double)480);
                        StoryBd.Stop();
                    }
                    break;

                case Oshirase:
                    dtOshirase = tbl;
                    //int i = 0;
                    //foreach (var Rows in dtOshirase.Rows)
                    //{
                    //    DateTime Kigen = DateTime.Parse(dtOshirase.Rows[i]["期限"].ToString());
                    //    if (Kigen < DateTime.Today)
                    //    {
                    //        dgMain.RowStyle.
                    //    }
                    //    else
                    //    {
                    //        dgMain.Background = new SolidColorBrush(Colors.White);
                    //    }
                    //}
                    break;
				//ログアウト
				case USER_LOGOUT:
					LicenseLoggedOff = true;
					this.Close();
					break;
			}
        }

        #endregion

        #region エラー受信メソッド

        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            this.Message = base.ErrorMessage;
			switch (message.GetMessageName())
			{
			case ReqLOGOUT:
				UserLoggedOff = true;
				this.Close();
				break;
			case USER_LOGOUT:
				LicenseLoggedOff = true;
				this.Close();
				break;
			}
        }

        #endregion

        #region Config保存

        private void CfgUpdateClicked(object sender, RoutedEventArgs e)
        {
            int id;
            if (int.TryParse(this.担当者ID, out id) != true)
            {
                //MessageBox.Show("担当者IDは数値を入力してください");
                return;
            }
            ccfg = (CommonConfig)usercfg.GetConfigValue(typeof(CommonConfig));
            ccfg.ログアウト時刻 = DateTime.Now;
            ccfg.ユーザID = id;
            ccfg.ユーザ名 = "";
            usercfg.SetConfigValue(ccfg);
            frmcfg.gamenID = new string[] { "newG", "newG", "newG", "newG", "newG", "newG", "newG", };
			frmcfg.Top = this.Top;
			frmcfg.Left = this.Left;
			frmcfg.Memo = this.メモ;
			usercfg.SetConfigValue(frmcfg);
            SendRequest(new CommunicationObject(MessageType.UpdateData, ReqLOGOUT, id, AppCommon.GetConfig(this)));
        }

        #endregion

        #region メニューリボンクリック

        private void Tab1Click(object sender, RoutedEventArgs e)
        {
            if (RibbonF1.Visibility == System.Windows.Visibility.Hidden)
                return;

            this.Tab1.IsSelected = true;
            Keyboard.Focus(Menu1);
        }

        private void Tab2Click(object sender, RoutedEventArgs e)
        {
            if (RibbonF2.Visibility == System.Windows.Visibility.Hidden)
                return;

            this.Tab2.IsSelected = true;
            Keyboard.Focus(Menu2);
        }

        private void Tab3Click(object sender, RoutedEventArgs e)
        {
            if (RibbonF3.Visibility == System.Windows.Visibility.Hidden)
                return;

            this.Tab3.IsSelected = true;
            Keyboard.Focus(Menu3);
        }

        private void Tab4Click(object sender, RoutedEventArgs e)
        {
            if (RibbonF4.Visibility == System.Windows.Visibility.Hidden)
                return;

            this.Tab4.IsSelected = true;
            Keyboard.Focus(Menu4);
        }

        private void Tab5Click(object sender, RoutedEventArgs e)
        {
            if (RibbonF5.Visibility == System.Windows.Visibility.Hidden)
                return;

            this.Tab5.IsSelected = true;
            Keyboard.Focus(Menu5);
        }

        private void Tab6Click(object sender, RoutedEventArgs e)
        {
            if (RibbonF6.Visibility == System.Windows.Visibility.Hidden)
                return;

            this.Tab6.IsSelected = true;
            Keyboard.Focus(Menu6);
        }

        private void Tab7Click(object sender, RoutedEventArgs e)
        {
            if (RibbonF7.Visibility == System.Windows.Visibility.Hidden)
                return;

            this.Tab7.IsSelected = true;
            Keyboard.Focus(Menu7);
        }

        private void Tab8Click(object sender, RoutedEventArgs e)
        {
            if (RibbonF8.Visibility == System.Windows.Visibility.Hidden)
                return;

            this.Tab8.IsSelected = true;
            Keyboard.Focus(Menu8);
        }

        private void Tab9Click(object sender, RoutedEventArgs e)
        {
            if (RibbonF9.Visibility == System.Windows.Visibility.Hidden)
                return;

            this.Tab9.IsSelected = true;
            Keyboard.Focus(Menu9);
        }

        private void Tab10Click(object sender, RoutedEventArgs e)
        {
            if (RibbonF10.Visibility == System.Windows.Visibility.Hidden)
                return;

            this.Tab10.IsSelected = true;
            Keyboard.Focus(Menu10);
        }

        private void Tab11Click(object sender, RoutedEventArgs e)
        {
            if (RibbonF11.Visibility == System.Windows.Visibility.Hidden)
                return;

            this.Tab11.IsSelected = true;
            Keyboard.Focus(Menu11);
        }

        /// <summary>
        /// ホームページ接続
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomeButtonClicked(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.kyoeisystem.co.jp/");
        }

        /// <summary>
        /// ログアウト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButtonClicked(object sender, RoutedEventArgs e)
        {
            closingFormProc();

        }


        #endregion

        #region メニューリボンファンクション

        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            Tab1Click(null, null);
        }

        public override void OnF2Key(object sender, KeyEventArgs e)
        {
            Tab2Click(null, null);
        }

        public override void OnF3Key(object sender, KeyEventArgs e)
        {
            Tab3Click(null, null);
        }

        public override void OnF4Key(object sender, KeyEventArgs e)
        {
            Tab4Click(null, null);
        }

        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            Tab5Click(null, null);
        }

        public override void OnF6Key(object sender, KeyEventArgs e)
        {
            Tab6Click(null, null);
        }

        public override void OnF7Key(object sender, KeyEventArgs e)
        {
            Tab7Click(null, null);
        }

        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            Tab8Click(null, null);
        }

        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            Tab9Click(null, null);
        }

        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            Tab10Click(null, null);
        }

        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            Tab11Click(null, null);
        }
       
        #endregion

        #region Headerキー

        private void AppClose(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.End)
            {
                closingFormProc();
            }
            else if (e.Key == Key.Home)
            {
                HomeButtonClicked(null, null);
            }
        }

        #endregion

        #region レイアウト表示

        private void ScreenChange(object sender, MouseEventArgs e)
        {
            string MenuName = ((Button)sender).Name == string.Empty ? string.Empty : ((Button)sender).Name.Substring(0, 4);
            if (MenuName == "Menu")
            {
                MenuName = ((Button)sender).Name;
                switch (MenuName)
                {
                    case "Menu1":
                        Tab1Click(null, null);
                        break;
                    case "Menu2":
                        Tab2Click(null, null);
                        break;
                    case "Menu3":
                        Tab3Click(null, null);
                        break;
                    case "Menu4":
                        Tab4Click(null, null);
                        break;
                    case "Menu5":
                        Tab5Click(null, null);
                        break;
                    case "Menu6":
                        Tab6Click(null, null);
                        break;
                    case "Menu7":
                        Tab7Click(null, null);
                        break;
                    case "Menu8":
                        Tab8Click(null, null);
                        break;
                    case "Menu9":
                        Tab9Click(null, null);
                        break;
                    case "Menu10":
                        Tab10Click(null, null);
                        break;
                    case "Menu11":
                        Tab11Click(null, null);
                        break;
                }
                return;
            }
            string ImgName = ((Button)sender).Name;
            string ScreenName = ((Button)sender).Content.ToString();
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("DisplayImage/ScreenImage/" + ImgName + ".jpg", UriKind.Relative);
            image.EndInit();
            ScreenImg = image;

        }

        private void KeyboardChange(object sender, RoutedEventArgs e)
        {
            ((Button)sender).BorderThickness = new Thickness(2);
            ((Button)sender).BorderBrush = Brushes.DarkBlue;
            ScreenChange(sender, null);
        }

        #endregion

        #region フォーカス制御

        private void OnKeyUP(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                string ImgName = ((Button)sender).Name == string.Empty ? string.Empty : ((Button)sender).Name.Substring(0, 3);
                if (ImgName != string.Empty)
                {
                    if (ImgName == "DLY" || ImgName == "SRY" || ImgName == "MST")
                    {
                        int Flg = AppCommon.IntParse(((Button)sender).DataContext.ToString());
                        switch (Flg)
                        {
                            case 0:
                                ImgName = "DLY0";
                                break;
                            case 1:
                                ImgName = "DLY1";
                                break;
                            case 2:
                                ImgName = "SRY0";
                                break;
                            case 3:
                                ImgName = "SRY1";
                                break;
                            case 7:
                                ImgName = "MST0";
                                break;
                            case 8:
                                ImgName = "MST1";
                                break;
                            case 9:
                                ImgName = "MST2";
                                break;
                        }
                    }
                    switch (ImgName)
                    {
                        case "DLY0":
                            Tab1Click(null, null);
                            break;
                        case "DLY1":
                            Tab2Click(null, null);
                            break;
                        case "TKS":
                            Tab3Click(null, null);
                            break;
                        case "SHR":
                            Tab4Click(null, null);
                            break;
                        case "ZIK":
                            Tab5Click(null, null);
                            break;
                        case "BSK":
                            Tab6Click(null, null);
                            break;
                        //case "SRY1":
                        //    Tab7Click(null, null);
                        //    break;
                        case "MST0":
                            Tab8Click(null, null);
                            break;
                        case "MST1":
                            Tab9Click(null, null);
                            break;
                        //case "NNG":
                        //    Tab10Click(null, null);
                        //    break;
                        //case "MST2":
                        //    Tab11Click(null, null);
                        //    break;

                    }
                }
            }
        }


        private void MemoDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Right || e.Key == Key.Down || e.Key == Key.Left)
            {
                KeyEventArgs e1 = new KeyEventArgs(
                    e.KeyboardDevice,
                    e.InputSource,
                    e.Timestamp,
                    Key.None);
                e1.RoutedEvent = Keyboard.KeyDownEvent;
                InputManager.Current.ProcessInput(e1);
                return;
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {

                UIElement element = sender as UIElement;
                if (element != null)
                {
                    bool isShiftPressed = ((e.KeyboardDevice.Modifiers & ModifierKeys.Shift) != 0);

                    FocusNavigationDirection dir;
                    if (isShiftPressed)
                    {
                        dir = FocusNavigationDirection.Previous;
                    }
                    else
                    {
                        dir = FocusNavigationDirection.First;
                    }

                    element.MoveFocus(new TraversalRequest(dir));

                    e.Handled = true;
                }

            }
            else if (e.Key == Key.Right)
            {
                string MenuName = ((Button)sender).Name == string.Empty ? string.Empty : ((Button)sender).Name;
                if (MenuName != string.Empty)
                {
                    switch (MenuName)
                    {
                        case "Menu11":
                            UIElement element = sender as UIElement;
                            if (element != null)
                            {
                                bool isShiftPressed = ((e.KeyboardDevice.Modifiers & ModifierKeys.Shift) != 0);

                                FocusNavigationDirection dir;
                                if (isShiftPressed)
                                {
                                    dir = FocusNavigationDirection.Previous;
                                }
                                else
                                {
                                    dir = FocusNavigationDirection.First;
                                }

                                element.MoveFocus(new TraversalRequest(dir));

                                e.Handled = true;
                                //Tab1Click(null, null);
                               
                            }
                            break;

                    }
                }
            }


            if (e.Key == Key.Down)
            {
                KeyEventArgs e1 = new KeyEventArgs(
                    e.KeyboardDevice,
                    e.InputSource,
                    e.Timestamp,
                    Key.Up);

                string MenuName = ((Button)sender).Name == string.Empty ? string.Empty : ((Button)sender).Name;
                if (MenuName != string.Empty)
                {
                    switch (MenuName)
                    {
                        case "Menu1":
                            Keyboard.Focus(DLY01010);
                            break;
                        case "Menu2":
                            //Keyboard.Focus(DLY10010);
                            break;
                        case "Menu3":
                            Keyboard.Focus(TKS01010);
                            break;
                        case "Menu4":
                            //Keyboard.Focus(SHR02010);
                            break;
                        case "Menu5":
                            //Keyboard.Focus(JMI01010);
                            break;
                        case "Menu6":
                            //Keyboard.Focus(SRY01010);
                            break;
                        case "Menu7":
                            //Keyboard.Focus(SRY04010);
                            break;
                        case "Menu8":
                            //Keyboard.Focus(MST01010);
                            break;
                        case "Menu9":
                            //Keyboard.Focus(MST06030);
                            break;
                        case "Menu10":
                            //Keyboard.Focus(NNG01010);
                            break;
                        case "Menu11":
                            //Keyboard.Focus(MST90030);
                            break;
                    }
                    e1.RoutedEvent = Keyboard.KeyDownEvent;
                    InputManager.Current.ProcessInput(e1);
                    return;

                }
            }
        
        }

        //画面サイズ最大化処理
        //private void WindowScreen(object sender, RoutedEventArgs e)
        //{

        //    if (this.WindowState == WindowState.Normal)
        //    {
        //        this.WindowState = WindowState.Maximized;
        //    }
        //    else
        //    {
        //        this.WindowState = WindowState.Normal;
        //        this.Width = 1024;
        //        this.Height = 768;
        //        this.MinWidth = 1024;
        //        this.MinHeight = 768;
        //    }
        //}

        //メニューヘッダー部をダブルクリックで最大化
        //private void DoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    if (e.ClickCount == 2)
        //    {
        //        WindowScreen(null, null);
        //    }
        //}

        #endregion

        #region KeybordLost

        /// <summary>
        /// LostFocusでボーダーをリセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeybordLost(object sender, RoutedEventArgs e)
        {
            ((Button)sender).BorderBrush = Brushes.White;
        }

        #endregion

        /// <summary>
        /// ログアウト時の処理
        /// </summary>
        private void closingFormProc()
        {
            this.Close();
        }


    }
}