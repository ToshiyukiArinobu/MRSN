using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.Controls;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using System.Configuration;

namespace KyoeiSystem.Framework.Windows.ViewBase
{
	/// <summary>
	/// 画面基底クラス
	/// </summary>
	public abstract class WindowViewBase : Window, IWindowViewBase
	{
		public event FinishWindowClosedHandler OnFinishWindowClosed;
		public event FinishWindowRenderedHandler OnFinishWindowRendered;

		public bool IsLoadFinished { get; set; }
		public bool IsRenderFinished { get; set; }


		private const int WM_CHAR = 0x102;
		private const int WM_IME_COMPOSITION = 0x10F;
		private const int GCS_RESULTREADSTR = 0x0200;

		// コンテキスト・ハンドルの取得
		[DllImport("Imm32.dll")]
		private static extern int ImmGetContext(int hWnd);

		// コンテキスト・ハンドルの解放
		[DllImport("Imm32.dll")]
		private static extern int ImmReleaseContext(int hWnd, int hIMC);

		// IMEより読みなどの文字列を取得する
		[DllImport("Imm32.dll")]
		private static extern int ImmGetCompositionString(int hIMC, int dwIndex, StringBuilder lpBuf, int dwBufLen);

		// IMEの状態取得
		[DllImport("Imm32.dll")]
		private static extern int ImmGetOpenStatus(int hIMC);

		/// <summary>
		/// 現在Close中かどうかを示すフラグ
		/// </summary>
		public new bool IsClosing { get; set; }

		/// <summary>
		/// DB接続用
		/// </summary>
		public string ConnString { get; set; }

		/// <summary>
		/// 画面共有オブジェクト
		/// </summary>
		public ViewsCommon viewsCommData { get; set; }

		/// <summary>
		/// スレッド管理機能
		/// </summary>
		public ThreadManeger threadmgr;

		private string windowTitle = string.Empty;
		/// <summary>
		/// 画面タイトル
		/// </summary>
		public string WindowTitle
		{
			get { return this.windowTitle; }
			set { this.windowTitle = value; NotifyPropertyChanged(); }
		}

		/// <summary>
		/// マスターメンテナンス画面呼出用ハンドラのコレクション
		/// </summary>
		public Dictionary<string, List<Type>> MasterMaintenanceWindowList = new Dictionary<string, List<Type>>();
		private Dictionary<Key, Action<object, KeyEventArgs>> funckeymethodlist = new Dictionary<Key, Action<object, KeyEventArgs>>();
		private Dictionary<string, string[]> ribbonbuttonlinks = new Dictionary<string, string[]>();

		private string _message = string.Empty;
		/// <summary>
		/// 画面に定義されたメッセージ領域に表示するメッセージ（エラーメッセージ以外の用途）
		/// </summary>
		public string Message
		{
			get
			{
				return this._message;
			}
			set
			{
				this._message = value;
				NotifyPropertyChanged();
			}
		}

		private Visibility _errmessageVisibility = Visibility.Collapsed;
		/// <summary>
		/// 画面に定義されたエラーメッセージ領域のコントロールの表示・非表示のフラグ
		/// </summary>
		public Visibility ErrorMessageVisibility
		{
			get { return this._errmessageVisibility; }
			set { this._errmessageVisibility = value; NotifyPropertyChanged(); }
		}
		private string _errormessage = string.Empty;
		/// <summary>
		/// 画面に定義されたエラーメッセージ領域に表示するメッセージ
		/// </summary>
		public string ErrorMessage
		{
			get { return this._errormessage; }
			set
			{
				this._errormessage = value;
				NotifyPropertyChanged();
				if (string.IsNullOrWhiteSpace(value))
				{
					this.ErrorMessageVisibility = System.Windows.Visibility.Collapsed;
				}
				else
				{
					this.ErrorMessageVisibility = System.Windows.Visibility.Visible;
				}
			}
		}

		private Visibility _maintenanceModeVisibility = Visibility.Collapsed;
		/// <summary>
		/// 画面処理モードの表示・非表示のフラグ
		/// </summary>
		public Visibility MaintenanceModeVisibility
		{
			get { return this._maintenanceModeVisibility; }
			set { this._maintenanceModeVisibility = value; NotifyPropertyChanged(); }
		}

		private string _maintenanceMode = string.Empty;
		/// <summary>
		/// 画面処理モード（追加・編集等）
		/// </summary>
		public string MaintenanceMode
		{
			get { return this._maintenanceMode; }
			set
			{
				this._maintenanceMode = value;
				NotifyPropertyChanged();
				if (string.IsNullOrWhiteSpace(value))
				{
					this.MaintenanceModeVisibility = System.Windows.Visibility.Collapsed;
				}
				else
				{
					this.MaintenanceModeVisibility = System.Windows.Visibility.Visible;
				}
			}
		}

		private delegate void FunctionKeyHandler(object sender, KeyEventArgs e);

		private Dictionary<MessageType, Action<CommunicationObject>> functionList
			= new Dictionary<MessageType, Action<CommunicationObject>>();

		private List<string> RequestList = new List<string>();

		/// <summary>
		/// Log出力用インスタンス
		/// </summary>
		public AppLogger appLog = AppLogger.Instance;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="appdata">アプリケーション共有オブジェクト</param>
		public WindowViewBase(object appdata = null)
			: base()
		{
			IsLoadFinished = false;
			IsRenderFinished = false;
			this.IsClosing = false;

			InitializeBase();

		}

		void InitializeBase()
		{
			this.Initialized += WindowViewBase_Initialized;
			this.ContentRendered += WindowViewBase_ContentRendered;
			this.Closing += WindowClosing;
			this.Closed += WindowViewBase_Closed;
			this.Loaded += WindowViewBase_Loaded;
			this.PreviewKeyDown += Window_PreviewKeyDown;
			this.MouseLeftButtonDown += (sender, e) => this.DragMove();

			if (this.Resources == null)
			{
				System.IO.FileInfo fi = new System.IO.FileInfo("./WindowStyle.xaml");
				if (fi.Exists)
				{
					this.Resources = new ResourceDictionary();
					this.Resources.Source = new Uri(fi.FullName);
				}
			}

		}

		void WindowViewBase_ContentRendered(object sender, EventArgs e)
		{
			this.ContentRendered -= WindowViewBase_ContentRendered;

			IsRenderFinished = true;
			if (OnFinishWindowRendered != null)
			{
				OnFinishWindowRendered(this);
			}
		}

		private void WindowViewBase_Initialized(object sender, EventArgs e)
		{
			this.WindowTitle = base.Title;
			object appdata = null;
			if (this.Owner is WindowViewBase)
			{
				appdata = (this.Owner as WindowViewBase).viewsCommData.AppData;
			}
			else if (this.Owner is RibbonWindowViewBase)
			{
				appdata = (this.Owner as RibbonWindowViewBase).viewsCommData.AppData;
			}
			if (this.viewsCommData == null)
			{
				this.viewsCommData = new ViewsCommon(appdata);
				this.viewsCommData.Initialize();
			}

			threadmgr = new ThreadManeger(this.viewsCommData.DacConf, this.appLog);
			threadmgr.Name = this.GetType().Name;
			threadmgr.OnReceived += new MessageReceiveHandler(OnReceived);
			ViewBaseCommon.SetConfigToControls(this, this.viewsCommData);

			functionList.Add(MessageType.ResponseData, this.OnReceivedResponseData);
			functionList.Add(MessageType.Error, this.OnReveivedError);
			functionList.Add(MessageType.ResponseStored, this.OnReceivedResponseStored);
			functionList.Add(MessageType.ResponseWithFree, this.OnReceivedResponseData);
			functionList.Add(MessageType.ErrorWithFree, this.OnReveivedError);
			functionList.Add(MessageType.TimerLoop, this.OnReceivedTimer);

			funckeyInitialize();
		}

		/// <summary>
		/// 画面がロードされたときに発生するイベント
		/// </summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		public virtual void WindowViewBase_Loaded(object sender, RoutedEventArgs e)
		{
			appLog.Info("<FW> ■ {0} Loaded", string.IsNullOrWhiteSpace(this.Title) ? this.GetType().Name : this.Title);

			//this.WindowStyle = System.Windows.WindowStyle.None;

			// WINDOWメッセージハンドリングが必要な場合
			//HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
			//source.AddHook(new HwndSourceHook(WndProc));

			foreach (var ctl in ViewBaseCommon.FindLogicalChildList<FrameworkControl>(this, false))
			{
				ctl.ConnectStringUserDB = this.ConnString;
			}

			SetFocusToTopControl();

			IsLoadFinished = true;
		}


		// WINDOWメッセージハンドリングが必要な場合
		//private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		//{
		//	switch (msg)
		//	{
		//	case WM_IME_COMPOSITION:
		//		break;

		//	case WM_CHAR:  // '半角英数字
		//		break;
		//	}

		//	return IntPtr.Zero;
		//}

		/// <summary>
		/// 画面を表示する
		/// </summary>
		/// <param name="wnd"></param>
		public void Show(Window wnd = null)
		{
			this.Owner = wnd != null && wnd.IsActive ? wnd : null;
			setConfigFromParent(wnd);
			base.Show();
		}

		/// <summary>
		/// 画面を表示する（ダイアログモード）
		/// </summary>
		/// <param name="wnd">呼び出し元Window</param>
		/// <returns>ダイアログ処理結果</returns>
		public bool? ShowDialog(Window wnd = null)
		{
			this.Owner = wnd != null && wnd.IsActive ? wnd : null;
			setConfigFromParent(wnd);
			return base.ShowDialog();
		}

		void setConfigFromParent(Window wnd)
		{
			if (wnd is IWindowViewBase)
			{
				this.viewsCommData = (wnd as IWindowViewBase).viewsCommData;
				this.ConnString = (wnd as IWindowViewBase).ConnString;
			}

		}

		void funckeyInitialize()
		{
			funckeymethodlist = new Dictionary<Key, Action<object, KeyEventArgs>>()
			{
				{ Key.F1, OnF1Key },
				{ Key.F2, OnF2Key },
				{ Key.F3, OnF3Key },
				{ Key.F4, OnF4Key },
				{ Key.F5, OnF5Key },
				{ Key.F6, OnF6Key },
				{ Key.F7, OnF7Key },
				{ Key.F8, OnF8Key },
				{ Key.F9, OnF9Key },
				{ Key.F10, OnF10Key },
				{ Key.F11, OnF11Key },
				{ Key.F12, OnF12Key },
			};
		}

		protected override void OnContentRendered(EventArgs e)
		{
			base.OnContentRendered(e);

			IsRenderFinished = true;
		}

		/// <summary>
		/// 画面を閉じる時のイベント
		/// </summary>
		/// <param name="sender">イベント送信オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		public void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.IsClosing = true;
			ViewBaseCommon.WindowClosing(this);

			appLog.Info("<FW> □ {0} Closing", string.IsNullOrWhiteSpace(this.Title) ? this.GetType().Name : this.Title);
		}

		/// <summary>
		/// 画面閉じるときの最終イベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void WindowViewBase_Closed(object sender, EventArgs e)
		{
			this.Initialized -= WindowViewBase_Initialized;
			this.Closing -= WindowClosing;
			this.Closed -= WindowViewBase_Closed;
			this.Loaded -= WindowViewBase_Loaded;
			this.PreviewKeyDown -= Window_PreviewKeyDown;
			this.MouseLeftButtonDown -= (dummy, arg) => this.DragMove();
			
			this.SetFreeForInput();

			if (this.threadmgr != null)
			{
				this.threadmgr.Dispose();
			}
			var ctls = ViewBaseCommon.FindLogicalChildList<FrameworkControl>(this, false);
			foreach (var ctl in ctls)
			{
				if (ctl.thmgr != null)
				{
					ctl.thmgr.Dispose();
					ctl.thmgr = null;
				}
			}
			if (OnFinishWindowClosed != null)
			{
				OnFinishWindowClosed(this);
			}
		}

		/// <summary>
		/// タイマーループを開始する
		/// </summary>
		/// <param name="time"></param>
		public void TimerLoopStart(int time)
		{
			this.threadmgr.TimerLoopStart(time);
		}

		/// <summary>
		/// タイマーループを停止する
		/// </summary>
		public void TimerLoopStop()
		{
			this.threadmgr.TimerLoopStop();
		}

		/// <summary>
		/// データ処理要求を送信する
		/// </summary>
		/// <param name="comobj"></param>
		public void SendRequest(CommunicationObject comobj)
		{
			bool busy = false;

			try
			{
				if (comobj.mType == MessageType.RequestLicense)
				{
					var plist = (NameValueCollection)ConfigurationManager.GetSection("serviceSettings");
					comobj.connection = plist["common"];
				}
				else
				{
					comobj.connection = this.ConnString;
				}
				if (comobj.mType == MessageType.RequestDataWithBusy)
				{
					var prgrs = ViewBaseCommon.FindLogicalChildList<ProgressBar>(this);
					foreach (var item in prgrs)
					{
						item.IsEnabled = true;
						item.Visibility = System.Windows.Visibility.Visible;
					}
					this.SetBusyForInput();
					busy = true;
				}

				lock (RequestList)
				{
					RequestList.Add(comobj.GetMessageName());
					if (RequestList.Count == 1)
					{
						//this.SetBusyForInput();
					}
					this.threadmgr.SendRequest(comobj);
				}
			}
			catch (Exception ex)
			{
				if (busy)
				{
					this.SetFreeForInput();
				}
			}
		}

		/// <summary>
		/// データ処理要求を送信する（複数の要求一括）
		/// </summary>
		/// <param name="comobjlist"></param>
		public void SendRequest(CommunicationObject[] comobjlist)
		{
			if (this.IsClosing) return;
			bool busy = false;

			lock (RequestList)
			{
				try
				{
					foreach (var comobj in comobjlist)
					{
						if (comobj.mType == MessageType.RequestLicense)
						{
							var plist = (NameValueCollection)ConfigurationManager.GetSection("serviceSettings");
							comobj.connection = plist["common"];
						}
						else
						{
							comobj.connection = this.ConnString;
						}
						if (comobj.mType == MessageType.RequestDataWithBusy)
						{
							if (!busy)
							{
								var prgrs = ViewBaseCommon.FindLogicalChildList<ProgressBar>(this);
								foreach (var item in prgrs)
								{
									item.IsEnabled = true;
									item.Visibility = System.Windows.Visibility.Visible;
								}
								this.SetBusyForInput();
								busy = true;
							}
						}

						RequestList.Add(comobj.GetMessageName());
						this.threadmgr.SendRequest(comobj);
					}
				}
				catch (Exception ex)
				{
					if (busy)
					{
						this.SetFreeForInput();
					}
				}
			}
		}

		/// <summary>
		/// データ受信イベント
		/// </summary>
		/// <param name="message"></param>
		public void OnReceived(CommunicationObject message)
		{
			appLog.Debug("<FW> {0} Received {1}({2})", this.GetType().Name, message.mType, message.GetMessageName());

			lock (RequestList)
			{
				RequestList.Remove(message.GetMessageName());
			}
			Dispatcher.Invoke(new ReceivedDelegate(OnResponseEmpty), message);
			ViewBaseCommon.OnReceived(message, Dispatcher, this.functionList);
		}

		private void OnResponseEmpty(CommunicationObject message /* 引数はダミー */)
		{
			if (message.mType == MessageType.ResponseWithFree
				|| message.mType == MessageType.ErrorWithFree)
			{
				this.SetFreeForInput();
				var prgrs = ViewBaseCommon.FindLogicalChildList<ProgressBar>(this);
				foreach (var item in prgrs)
				{
					item.IsEnabled = false;
					item.Visibility = System.Windows.Visibility.Collapsed;
				}
			}
		}

		/// <summary>
		/// タイマーイベント受信時の処理
		/// </summary>
		/// <param name="message"></param>
		public virtual void OnReceivedTimer(CommunicationObject message)
		{
		}

		/// <summary>
		/// エラー受信時の処理
		/// </summary>
		/// <param name="message"></param>
		public virtual void OnReveivedError(CommunicationObject message)
		{
			this.ErrorMessage = ViewBaseCommon.OnReveivedError(message, this.viewsCommData);
		}

		/// <summary>
		/// データアクセスの結果受信
		/// </summary>
		/// <param name="message">受信データ</param>
		public virtual void OnReceivedResponseData(CommunicationObject message)
		{
		}

		/// <summary>
		/// ストアドの実行結果受信処理
		/// </summary>
		/// <param name="message">受信データ</param>
		public virtual void OnReceivedResponseStored(CommunicationObject message)
		{
		}

		/// <summary>
		/// フォーカスを先頭のコントロールに移動する
		/// </summary>
		/// <returns>true:移動した、false:移動していない</returns>
		public bool SetFocusToTopControl()
		{
			return ViewBaseCommon.SetFocusToTopControl(this);
		}

		/// <summary>
		/// 親Panelが非表示の場合、子コントロールを非表示にする
		/// </summary>
		public void ChangePanelVisibility()
		{
			ViewBaseCommon.ChangePanelVisibility(this, Visibility.Visible);
		}


		/// <summary>
		/// キー項目としてマークされた項目の入力可否を切り替える
		/// </summary>
		/// <param name="flag">true:入力可、false:入力不可</param>
		public void ChangeKeyItemChangeable(bool flag)
		{
			ViewBaseCommon.ChangeKeyItemChangeable(flag, this);
		}

		/// <summary>
		/// Window内のValidatorプロパティを持つ全項目をチェックする。
		/// </summary>
		/// <returns>true:全項目OK、false:チェックNG項目あり</returns>
		public bool CheckAllValidation(bool setfocustop = false)
		{
			this.ErrorMessage = ViewBaseCommon.CheckAllValidation(this, setfocustop);
			if (string.IsNullOrWhiteSpace(this.ErrorMessage))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Window内のValidationのチェック結果を全てリセットする。
		/// </summary>
		public void ResetAllValidation()
		{
			ViewBaseCommon.ResetAllValidation(this);
		}

		/// <summary>
		/// Windowを入力不可にする（マウスカーソルを砂時計にする）
		/// </summary>
		public void SetBusyForInput()
		{
			ViewBaseCommon.SetBusyForInput(this);
		}

		/// <summary>
		/// Windowを入力可能にする（マウスカーソルを戻す）
		/// </summary>
		public void SetFreeForInput()
		{
			ViewBaseCommon.SetFreeForInput(this);
		}

		//<summary>
		//キーボードで押下されたkeyはここで拾えます
		//PreviewKeyDown="Window_PreviewKeyDown"をxamlのwindowプロパティに追加
		//</summary>
		/// <param name="sender">イベント発生オブジェクト</param>
		/// <param name="e">イベント引数</param>
		private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (IsRenderFinished != true)
			{
				e.Handled = true;
				return;
			}

			this.ErrorMessage = string.Empty;
			ViewBaseCommon.CallFunctionKeyMethod(this, sender, e, this.funckeymethodlist);
		}

		/// <summary>
		/// F1キー処理用メソッド
		/// </summary>
		/// <param name="sender">イベント発生オブジェクト</param>
		/// <param name="e">イベント引数</param>
		public virtual void OnF1Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// F2キー処理用メソッド
		/// </summary>
		/// <param name="sender">イベント発生オブジェクト</param>
		/// <param name="e">イベント引数</param>
		public virtual void OnF2Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// F3キー処理用メソッド
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public virtual void OnF3Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// F4キー処理用メソッド
		/// </summary>
		/// <param name="sender">イベント発生オブジェクト</param>
		/// <param name="e">イベント引数</param>
		public virtual void OnF4Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// F5キー処理用メソッド
		/// </summary>
		/// <param name="sender">イベント発生オブジェクト</param>
		/// <param name="e">イベント引数</param>
		public virtual void OnF5Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// F6キー処理用メソッド
		/// </summary>
		/// <param name="sender">イベント発生オブジェクト</param>
		/// <param name="e">イベント引数</param>
		public virtual void OnF6Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// F7キー処理用メソッド
		/// </summary>
		/// <param name="sender">イベント発生オブジェクト</param>
		/// <param name="e">イベント引数</param>
		public virtual void OnF7Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// F8キー処理用メソッド
		/// </summary>
		/// <param name="sender">イベント発生オブジェクト</param>
		/// <param name="e">イベント引数</param>
		public virtual void OnF8Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// F9キー処理用メソッド
		/// </summary>
		/// <param name="sender">イベント発生オブジェクト</param>
		/// <param name="e">イベント引数</param>
		public virtual void OnF9Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// F10キー処理用メソッド
		/// </summary>
		/// <param name="sender">イベント発生オブジェクト</param>
		/// <param name="e">イベント引数</param>
		public virtual void OnF10Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// F11キー処理用メソッド
		/// </summary>
		/// <param name="sender">イベント発生オブジェクト</param>
		/// <param name="e">イベント引数</param>
		public virtual void OnF11Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// F12キー処理用メソッド
		/// </summary>
		/// <param name="sender">イベント発生オブジェクト</param>
		/// <param name="e">イベント引数</param>
		public virtual void OnF12Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// 指定されたコントロールの内側に存在するDataGridを探し、そのDataGridRowを取得する
		/// </summary>
		/// <param name="curctl">検索対象コントロール</param>
		/// <returns>検索結果</returns>
		public DataGridRow GetDataGridRow(object curctl)
		{
			return ViewBaseCommon.GetDataGridRow(curctl as DependencyObject);
		}

		/// <summary>
		/// 指定されたカラム及びRowの情報からDataGridのCellを取得する
		/// </summary>
		/// <param name="col">DataGridのColumn情報</param>
		/// <param name="grow">DataGridのRowオブジェクト</param>
		/// <returns>検索結果</returns>
		public DataGridCell GetDataGridCell(DataGridColumn col, DataGridRow grow)
		{
			return ViewBaseCommon.GetDataGridCell(col, grow);
		}

		/// <summary>
		/// 現在フォーカスのあるDataGridのDataGridRowを取得する
		/// </summary>
		/// <param name="curctl">検索対象コントロール</param>
		/// <returns>検索結果</returns>
		public DataRowView GetCurrentDataGridRow(object curctl)
		{
			return ViewBaseCommon.GetCurrentDataGridRow(curctl as DependencyObject);
		}

		/// <summary>
		/// 現在フォーカスのあるDataGridを取得する
		/// </summary>
		/// <param name="curctl">検索対象コントロール</param>
		/// <returns>検索結果</returns>
		public DataGrid GetCurrentDataGrid(object curctl)
		{
			return ViewBaseCommon.GetCurrentDataGrid(curctl as DependencyObject);
		}

		/// <summary>
		/// DataGridから指定されたタイプの要素をもつCellを検索する
		/// </summary>
		/// <typeparam name="T">検索対象のコントロールタイプ</typeparam>
		/// <param name="cell">DataGridのCellオブジェクト</param>
		/// <returns>検索結果</returns>
		public T FindUIElementInDataGridCell<T>(DataGridCell cell) where T : FrameworkElement
		{
			return ViewBaseCommon.FindUIElementInDataGridCell<T>(cell);
		}

		/// <summary>
		/// DataGridから指定されたタイプの要素をもつCellを検索する
		/// </summary>
		/// <typeparam name="T">検索対象のコントロールタイプ</typeparam>
		/// <param name="cell">DataGridのCellオブジェクト</param>
		/// <returns>検索結果</returns>
		public List<T> FindUIElementListInDataGridCell<T>(DataGridCell cell) where T : FrameworkElement
		{
			return ViewBaseCommon.FindUIElementListInDataGridCell<T>(cell);
		}

		/// <summary>
		/// DataGridCellのマウスクリックイベントの処理（Cellのフォーカス移動を強制する）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			ViewBaseCommon.DataGridCell_PreviewMouseLeftButtonDown(sender, e);
		}

		/// <summary>
		/// DataGridのCellの値チェック結果（OK）をセットする
		/// </summary>
		/// <param name="cell">DataGridのCellオブジェクト</param>
		public void SetValidDataGridCell(DataGridCell cell)
		{
			ViewBaseCommon.SetValidDataGridCell(cell);
		}

		/// <summary>
		/// DataGridのCellの値チェック結果（NG）をセットする
		/// </summary>
		/// <param name="cell"></param>
		public void SetInvalidDataGridCell(DataGridCell cell)
		{
			ViewBaseCommon.SetInvalidDataGridCell(cell);
		}

		/// <summary>
		/// 接続文字列のセットアップ
		/// </summary>
		/// <param name="server">サーバー名</param>
		/// <param name="db">Database名</param>
		/// <param name="user">ユーザ名</param>
		/// <param name="passwd">パスワード</param>
		public void SetupConnectStringuserDB(string server, string db, string user, string passwd)
		{
			ViewBaseCommon.SetupConnectStringuserDB(this, server, db, user, passwd);
		}


		#region INotifyPropertyChanged メンバー
		/// <summary>
		/// Binding機能対応（プロパティの変更通知イベント）
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;
		/// <summary>
		/// Binding機能対応（プロパティの変更通知イベント送信）
		/// </summary>
		/// <param name="propertyName">プロパティ名</param>
		public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

	}

}
