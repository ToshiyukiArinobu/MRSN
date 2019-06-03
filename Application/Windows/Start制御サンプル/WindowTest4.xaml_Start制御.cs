using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using System.Windows.Markup;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;
using KyoeiSystem.Framework.Reports;
using KyoeiSystem.Application.Windows.Views;
using KyoeiSystem.Framework.Reports.Preview;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Threading.Tasks;

namespace KyoeiSystem.Application.Windows.Views
{

	/// <summary>
	/// テスト用初期画面
	/// </summary>
	public partial class WindowTest4 : WindowGeneralBase
	{
		private void Start(string cname, Dictionary<string, object> plist = null)
		{
			AppCommon.Start(cname, plist);
		}

		private void Start(Type ctype, Dictionary<string, object> plist = null)
		{
			AppCommon.Start(ctype, plist);
		}

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
			public string[] gamenID;
		}
		/// ※ 必ず public で定義する。
		public ConfigWindowTest frmcfg = null;
		#endregion

		#region 定数定義

		//private const string ReqLOGIN = "SEARCH_LOGIN";
		private const string ReqLOGOUT = "Logout";
		private const string ReqIMAGE = "MaineImage";
		private const string Oshirase = "Oshirase";
		private const string USER_LOGOUT = "updateLogout";
		private const string MsgShow = "MessgeShow";

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

		private string _DayOfWeek = "(" + ("日月火水木金土").Substring(AppCommon.IntParse(DateTime.Now.DayOfWeek.ToString("d")), 1) + ")";
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


		#endregion

		private bool UserLoggedOff = false;
		private bool IsLoggingOffUser = false;
		private bool LicenseLoggedOff = false;
		private bool IsLoggingOffLicense = false;

		#region WindowTest4

		public WindowTest4()
			: base()
		{
			InitializeComponent();
			this.DataContext = this;

			DispatcherTimer timer = new DispatcherTimer();
			timer.Tick += timer_Tick;
			timer.Interval = new TimeSpan(0, 0, 1);
			timer.Start();
		}

		protected override void OnGotFocus(RoutedEventArgs e)
		{
			base.OnGotFocus(e);
			this.Topmost = true;
			this.Activate();
			this.Topmost = false;
			this.Focus();

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

			Keyboard.Focus(Menu1);

		}

		#endregion

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

		#region その他

		/// <summary>
		/// 締日集計処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(TKS31010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 乗務員マスタ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_4(object sender, RoutedEventArgs e)
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
		/// 乗務員マスタ一括入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_乗務員一括入力(object sender, RoutedEventArgs e)
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
		/// 請求書発行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_6(object sender, RoutedEventArgs e)
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
		/// 取引先マスタ問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_8(object sender, RoutedEventArgs e)
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
		/// 取引先マスタ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_9(object sender, RoutedEventArgs e)
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
		/// 請求内訳マスタ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_10(object sender, RoutedEventArgs e)
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
		/// 日報入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_11(object sender, RoutedEventArgs e)
		{
			//try
			//{
			//	Start(typeof(DLY05010));
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}
		}


		private void Button_Click_12(object sender, RoutedEventArgs e)
		{
		}


		/// <summary>
		/// メインメニュー
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_13(object sender, RoutedEventArgs e)
		{
			try
			{
				MessageBox.Show("未作成");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		/// <summary>
		/// 車輌月次経費入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_14(object sender, RoutedEventArgs e)
		{
			//try
			//{
			//    Start(typeof(SRY06010));
			//}
			//catch (Exception ex)
			//{
			//    MessageBox.Show(ex.Message);
			//}
		}

		/// <summary>
		/// 乗務員運行表印刷
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_15(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(DLY16010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 乗務員収支実績表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_16(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(JMI11010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(TKS13010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

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

		#region マスタ[MST]

		/// <summary>
		/// 発着地マスタ呼び出し
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST03(object sender, RoutedEventArgs e)
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
		/// 車種マスタ呼び出し
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_05(object sender, RoutedEventArgs e)
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
		/// 車両マスタ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST06(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST06010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 車輌固定費一括入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST0603(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST06030));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 商品マスタ呼び出し      
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_07(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST07010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 摘要マスタ呼び出し
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_08(object sender, RoutedEventArgs e)
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
		/// 経費項目マスタ呼び出し
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST09(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST09010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}



		/// <summary>
		/// 自社部門マスタ呼び出し
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST10(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST10010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// コース配車マスタ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST11(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST11010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 自社名マスタ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST12(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST12010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 消費税率マスタ呼び出し
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST13(object sender, RoutedEventArgs e)
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
		/// 支払先別軽油マスタ呼び出し
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST14(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST14010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		/// <summary>
		/// 得意先別車種別単価マスタ呼び出し
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST16(object sender, RoutedEventArgs e)
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
		/// 得意先別距離別運賃マスタ呼び出し
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST17(object sender, RoutedEventArgs e)
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
		/// 得意先別個建単価マスタ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST18(object sender, RoutedEventArgs e)
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
		/// 得意先別品名単価マスタ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST19(object sender, RoutedEventArgs e)
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
		/// 支払先別車種単価マスタ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST20(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST20010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 支払先別距離別運賃マスタ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST21(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST21010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 支払先別個建単価マスタ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST22(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST22010));
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
		private void Button_Click_MST23(object sender, RoutedEventArgs e)
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
		/// カレンダーマスタ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST24(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST24010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 得意先月次集計Ｆ修正
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST25(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST25010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 支払先月次集計Ｆ修正
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST26(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST26010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 得意先別消費税集計Ｆ修正
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST27(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST27010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// グリーン経営車種マスタ保守
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST28(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST28010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 規制マスタ保守
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST29(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST29010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 基礎情報設定呼び出し
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST30(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST30010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 燃費目標マスタ保守
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST31(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST31010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 車輌別売上予算マスタ保守
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST32(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST32010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 部門別売上予算マスタ保守
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST33(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST33010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Button_Click_MST90010(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST90010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// CSV取り込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST90020(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST90020));
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
		private void Button_Click_MST90030(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST90030));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// データ一括削除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST90040(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST90040));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		#endregion

		#region 問合せ画面[MSTXX020]
		/// <summary>
		/// 車種マスタ問い合わせ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST05_2(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST05020));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 車輌マスタ問い合わせ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST06_2(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST06020));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		/// <summary>
		/// 商品マスタ問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST07_2(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST07020));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 摘要マスタ問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST08_2(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST08020));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 自社部門マスタ問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST10_2(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST10020));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// コース配車マスタ問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST11_2(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST11020));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 自社名マスタ問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST12_2(object sender, RoutedEventArgs e)
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
		/// 消費税マスタ問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST13_2(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST13020));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 支払先別軽油マスタ問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST14_2(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST14020));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 得意先別車種別単価マスタ入力問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST16_2(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST16020));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 得意先別距離別単価マスタ入力問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST17_2(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST17020));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


		/// <summary>
		/// 得意先別個建単価マスタ問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST18_2(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST18020));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 支払先別地区単価マスタ問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST19_2(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST19020));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 支払先別車種別単価マスタ入力問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST20_2(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST20020));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 支払先別距離別単価マスタ入力問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST21_2(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST21020));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 支払先別個建単価マスタ問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST22_2(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST22020));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 担当者マスタ問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_MST23_2(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST23020));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


		#endregion

		#region 検索画面[SCH]
		/// <summary>
		/// 車種検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SCH05(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SCH05010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 車輌検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SCH06(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SCH06010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 商品検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SCH07(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SCH07010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 摘要検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SCH08(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SCH08010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}

		/// <summary>
		/// 自社部門検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SCH10(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SCH10010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


		/// <summary>
		/// コース配車検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SCH11(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SCH11010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 自社名検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SCH12(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SCH12010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}



		/// <summary>
		/// 担当者検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SCH13(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SCH13010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}

		/// <summary>
		/// 得意先検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SCH14(object sender, RoutedEventArgs e)
		{
			try
			{
				Dictionary<string, object> plist = new Dictionary<string, object>()
				{
					{ "表示区分", 1 },
				};
				Start(typeof(SCH01010), plist);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 支払先検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SCH15(object sender, RoutedEventArgs e)
		{
			try
			{
				Dictionary<string, object> plist = new Dictionary<string, object>()
				{
					{ "表示区分", 2 },
				};
				Start(typeof(SCH01010), plist);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 仕入先検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SCH16(object sender, RoutedEventArgs e)
		{
			try
			{
				Dictionary<string, object> plist = new Dictionary<string, object>()
				{
					{ "表示区分", 3 },
				};
				Start(typeof(SCH01010), plist);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


		/// <summary>
		/// 経費先コード検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SCH17(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SCH01030));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Button_Click_SCH28(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SCH28010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Button_Click_SCH29(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SCH29010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		#endregion

		#region 日次[DLY]

		/// <summary>
		/// 運転日報画面ボタン押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DLY01(object sender, RoutedEventArgs e)
		{
			try
			{
				Dictionary<string, object> plist = new Dictionary<string, object>()
				{
					{ "初期明細番号", 1 },
				};
				Start(typeof(DLY01010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


		/// <summary>
		/// 売上伝票入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DLY02(object sender, RoutedEventArgs e)
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
		/// 日報入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DLY03(object sender, RoutedEventArgs e)
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
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DLY05(object sender, RoutedEventArgs e)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DLY06(object sender, RoutedEventArgs e)
		{
		}

		/// <summary>
		/// 経費伝票入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DLY07(object sender, RoutedEventArgs e)
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
		/// 入金伝票入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DLY08(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(DLY08010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 出金伝票入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DLY09(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(DLY09010));
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
		private void Button_Click_DLY10(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(DLY10010));
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
		private void Button_Click_DLY11(object sender, RoutedEventArgs e)
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
		/// 乗務員・車輌明細問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DLY12(object sender, RoutedEventArgs e)
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
		/// 運転日報明細問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DLY13(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(DLY13010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 経費明細問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DLY14(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(DLY14010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 入金明細問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DLY15(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(DLY15010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 出金明細問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DLY16(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(DLY16010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		/// <summary>
		/// 運行指示書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DLY19(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(DLY19010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 車輌運行表印刷
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DLY20(object sender, RoutedEventArgs e)
		{
		}

		/// <summary>
		/// 乗務員運行表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DLY21(object sender, RoutedEventArgs e)
		{
		}

		/// <summary>
		/// 運転作業日報印刷
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DLY22(object sender, RoutedEventArgs e)
		{
		}

		/// <summary>
		/// 日別売上管理表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DLY23(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(DLY23010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Button_Click_DLY99(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(DLY99999));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		#endregion

		#region 乗務員管理[JMI]
		/// <summary>
		/// 乗務員管理表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_JMI01(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(JMI01010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 乗務員管理表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_JMI02(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(JMI02010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 乗務員労務管理表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_JMI03(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(JMI03010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 乗務員管理合計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_JMI04(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(JMI04010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


		/// <summary>
		/// 乗務員売上明細書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_JMI05(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(JMI05010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 乗務員売上日計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_JMI06(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(JMI06010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		/// <summary>
		/// 乗務員売上合計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_JMI07(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(JMI07010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 乗務員状況履歴
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_JMI08(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(JMI08010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		/// <summary>
		/// 乗務員月次集計
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_JMI09(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(JMI09010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 乗務員月別収支合計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_JMI12(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(JMI12010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 乗務員別収支合計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_JMI13(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(JMI13010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 乗務員運転免許管理表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_JMI14(object sender, RoutedEventArgs e)
		{
			//try
			//{
			//	Start(typeof(JMI14010));
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}
		}

		/// <summary>
		/// 乗務員月次経費入力画面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_JMI10(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(JMI10010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 乗務員月次経費入力画面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_JMI11(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(JMI11010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		#endregion

		#region 車輌管理[SRY]
		/// <summary>
		/// 車輌管理表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SRY01(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY01010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 車輌売上明細表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SRY02(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY02010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 車輌別日計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SRY03(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY03010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 車種別日計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SRY04(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY04010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 車輌月次集計
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SRY05(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY05010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 車輌月次経費入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SRY06(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY06010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 車輌収支実績表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SRY07(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY07010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 車輌収支合計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SRY08(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY08010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 車輌合計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SRY09(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY09010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 車輌統計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SRY10(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY10010));
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
		private void Button_Click_SRY11(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY11010));
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
		private void Button_Click_SRY12(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY12010));
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
		private void Button_Click_SRY13(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY13010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 輸送実績報告書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SRY14(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY14010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		/// <summary>
		/// 車輌別収支合計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SRY15(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY15010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 車輌別経費明細一覧表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SRY16(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY16010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 車輌管理台帳
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SRY17(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY17010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 車輌別燃料消費量累計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SRY18(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY18010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 車輌別燃料消費量実績表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SRY19(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY19010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 燃費管理表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SRY20(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY20010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 車検予定管理表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SRY21(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY21010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 車輌点検表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SRY22(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY22010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 付表作成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SRY23(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY23010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 稼働日数管理表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SRY24(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SRY24010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		#endregion

		#region 支払先管理[SHR]

		/// <summary>
		/// 配送依頼書印刷
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SHR01(object sender, RoutedEventArgs e)
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
		/// 支払先明細書印刷
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SHR02(object sender, RoutedEventArgs e)
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
		/// 支払経費明細書印刷
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SHR03(object sender, RoutedEventArgs e)
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
		/// 支払月次集計
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SHR04(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SHR04010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 支払先合計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SHR05(object sender, RoutedEventArgs e)
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
		/// 支払先累積表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SHR06(object sender, RoutedEventArgs e)
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

		/// <summary>
		/// 支払先残高問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SHR07(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SHR07010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 支払先一覧表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SHR08(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SHR08010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 支払先予定表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SHR09(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SHR09010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 支払先別支払先日計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SHR10(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SHR10010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 取引先別管理表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SHR12(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SHR12010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 取引先別相殺管理表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SHR13(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(SHR13010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		#endregion

		#region 年次[NNG]

		/// <summary>
		/// 得意先月別合計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_NNG01(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(NNG01010));
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
		private void Button_Click_NNG02(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(NNG02010));
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
		private void Button_Click_NNG03(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(NNG03010));
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
		private void Button_Click_NNG04(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(NNG04010));
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
		private void Button_Click_NNG05(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(NNG05010));
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
		private void Button_Click_NNG06(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(NNG06010));
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
		private void Button_Click_NNG07(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(NNG07010));
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
		private void Button_Click_NNG08(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(NNG08010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		#endregion

		#region 得意先管理[TKS]

		/// <summary>
		/// 内訳請求書発行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_TKS02(object sender, RoutedEventArgs e)
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
		/// 得意先売上明細書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_TKS03(object sender, RoutedEventArgs e)
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
		/// 得意先別売上日計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_TKS04(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(TKS04010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 得意先別売上日計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_TKS05(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(TKS05010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 売上合計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_TKS06(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(TKS06010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 売上累計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_TKS07(object sender, RoutedEventArgs e)
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
		/// 請求一覧表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_TKS08(object sender, RoutedEventArgs e)
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
		/// 得意先残高問い合わせ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_TKS09(object sender, RoutedEventArgs e)
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
		/// 入金滞留管理表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_TKS10(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(TKS10010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 回収予定表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_TKS11(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(TKS11010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 取引先別管理表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_TKS12(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(TKS12010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


		/// <summary>
		/// 取引先別相殺管理表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_TKS14(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(TKS14010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 売上分析グラフ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_TKS15(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(TKS15010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		/// <summary>
		/// 売上分析グラフ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_TKS16(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(TKS16010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 売上分析グラフ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_TKS17(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(TKS17010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 部門別日計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_TKS18(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(TKS18010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 車輌別日計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_TKS19(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(TKS19010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 得意先別部門別売上管理表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_TKS20(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(TKS20010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 部門別売上推移表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_TKS21(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(TKS21010));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


		#endregion

		#region KEY変換[KEY_Chage]
		/// <summary>
		/// Key変換
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_KeyChange(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(KeyChage));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


		#endregion

		private void ExitButtonClicked(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

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

		private void CalendarClicked(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(Calendar));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		#region ﾎｰﾑﾍﾟｰｼﾞ

		private void HomeButtonClicked(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.kyoeisystem.co.jp/");
		}

		#endregion


		private void Button_Click_TKS99(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(TKS99999));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Button_Click_DL99(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(DLY99999));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Button_Click_MST90050(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST90060));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}

		private void Button_Click_MST90090(object sender, RoutedEventArgs e)
		{
			try
			{
				Start(typeof(MST90090));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}

		#region メニューリボンクリック

		private void Tab1Click(object sender, RoutedEventArgs e)
		{
			this.Tab1.IsSelected = true;
			Keyboard.Focus(Menu1);
		}

		private void Tab2Click(object sender, RoutedEventArgs e)
		{
			this.Tab2.IsSelected = true;
			Keyboard.Focus(Menu2);
		}

		private void Tab3Click(object sender, RoutedEventArgs e)
		{
			this.Tab3.IsSelected = true;
			Keyboard.Focus(Menu3);
		}

		private void Tab4Click(object sender, RoutedEventArgs e)
		{
			this.Tab4.IsSelected = true;
			Keyboard.Focus(Menu4);
		}

		private void Tab5Click(object sender, RoutedEventArgs e)
		{
			this.Tab5.IsSelected = true;
			Keyboard.Focus(Menu5);
		}

		private void Tab6Click(object sender, RoutedEventArgs e)
		{
			this.Tab6.IsSelected = true;
			Keyboard.Focus(Menu6);
		}

		private void Tab7Click(object sender, RoutedEventArgs e)
		{
			this.Tab7.IsSelected = true;
			Keyboard.Focus(Menu7);
		}

		private void Tab8Click(object sender, RoutedEventArgs e)
		{
			this.Tab8.IsSelected = true;
			Keyboard.Focus(Menu8);
		}

		private void Tab9Click(object sender, RoutedEventArgs e)
		{
			this.Tab9.IsSelected = true;
			Keyboard.Focus(Menu9);
		}

		private void Tab10Click(object sender, RoutedEventArgs e)
		{
			this.Tab10.IsSelected = true;
			Keyboard.Focus(Menu10);
		}

		private void Tab11Click(object sender, RoutedEventArgs e)
		{
			this.Tab11.IsSelected = true;
			Keyboard.Focus(Menu11);
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
				this.Close();
			}
			else if (e.Key == Key.Home)
			{
				HomeButtonClicked(null, null);
			}
			//else if (e.Key == Key.Insert)
			//{
			//	CfgUpdateClicked(null, null);
			//}
			else if (e.Key == Key.Escape)
			{
				// WindowScreen(null, null);
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
			this.ScreenName.Content = ScreenName;

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
						case 4:
							ImgName = "MST0";
							break;
						case 5:
							ImgName = "MST1";
							break;
						case 6:
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
					case "JMI":
						Tab5Click(null, null);
						break;
					case "SRY0":
						Tab6Click(null, null);
						break;
					case "SRY1":
						Tab7Click(null, null);
						break;
					case "MST0":
						Tab8Click(null, null);
						break;
					case "MST1":
						Tab9Click(null, null);
						break;
					case "NNG":
						Tab10Click(null, null);
						break;
					case "MST2":
						Tab11Click(null, null);
						break;

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
							Tab1Click(null, null);

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
						Keyboard.Focus(DLY10010);
						break;
					case "Menu3":
						Keyboard.Focus(TKS01010);
						break;
					case "Menu4":
						Keyboard.Focus(SHR02010);
						break;
					case "Menu5":
						Keyboard.Focus(JMI01010);
						break;
					case "Menu6":
						Keyboard.Focus(SRY01010);
						break;
					case "Menu7":
						Keyboard.Focus(SRY04010);
						break;
					case "Menu8":
						Keyboard.Focus(MST01010);
						break;
					case "Menu9":
						Keyboard.Focus(MST06030);
						break;
					case "Menu10":
						Keyboard.Focus(NNG01010);
						break;
					case "Menu11":
						Keyboard.Focus(MST90030);
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

		private void KeybordLost(object sender, RoutedEventArgs e)
		{
			((Button)sender).BorderBrush = Brushes.White;
		}


	}
}