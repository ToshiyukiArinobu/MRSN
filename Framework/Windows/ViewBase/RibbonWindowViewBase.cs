using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls.Ribbon;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Data;
using System.Configuration;
using System.Collections.Specialized;

using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.Controls;
using System.Threading;
using System.Windows.Interop;

namespace KyoeiSystem.Framework.Windows.ViewBase
{
	/// <summary>
	/// 画面基底クラス（Ribbonあり）
	/// </summary>
	public abstract class RibbonWindowViewBase : RibbonWindow, IWindowViewBase
	{
		public event FinishWindowClosedHandler OnFinishWindowClosed;
		public event FinishWindowRenderedHandler OnFinishWindowRendered;

		public bool IsLoadFinished { get; set; }
		public bool IsRenderFinished { get; set; }

		/// <summary>
		/// 現在閉じられようとしているかどうかを示すフラグ
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
		public ThreadManeger threadmgr = null;

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
				if (value == null)
				{
					//this.MaintenanceModeVisibility = System.Windows.Visibility.Visible;
					this.MaintenanceModeForeground = new SolidColorBrush(Colors.Black);
					this.MaintenanceModeBackground = new SolidColorBrush(Colors.Transparent);
				}
				else
				{
					//this.MaintenanceModeVisibility = System.Windows.Visibility.Visible;
					if (value.Contains("新"))
					{
						this.MaintenanceModeForeground = new SolidColorBrush(Colors.White);
						this.MaintenanceModeBackground = new SolidColorBrush(Colors.Tomato);
					}
					else if (value.Contains("編"))
					{
						this.MaintenanceModeForeground = new SolidColorBrush(Colors.White);
						this.MaintenanceModeBackground = new SolidColorBrush(Colors.CornflowerBlue);
					}
                    else if (value.Contains("確"))
                    {
                        this.MaintenanceModeForeground = new SolidColorBrush(Colors.White);
                        this.MaintenanceModeBackground = new SolidColorBrush(Colors.Orange);
                    }
					else
					{
						this.MaintenanceModeForeground = new SolidColorBrush(Colors.Black);
						this.MaintenanceModeBackground = new SolidColorBrush(Colors.Transparent);
					}
				}
			}
		}

		private Brush _maintenanceModeForeground = new SolidColorBrush(Colors.Black);
		/// <summary>
		/// メンテナンスモードの文字表示色
		/// </summary>
		public Brush MaintenanceModeForeground
		{
			get { return this._maintenanceModeForeground; }
			set
			{
				this._maintenanceModeForeground = value;
				NotifyPropertyChanged();
			}
		}
		private Brush _maintenanceModeBackground = new SolidColorBrush(Colors.White);
		/// <summary>
		/// メンテナンスモードの背景表示色
		/// </summary>
		public Brush MaintenanceModeBackground
		{
			get { return this._maintenanceModeBackground; }
			set
			{
				this._maintenanceModeBackground = value;
				NotifyPropertyChanged();
			}
		}

        /// <summary>
        /// 権限関係
        /// </summary>
        private Visibility _dataUpdateVisible = Visibility.Visible;
        public Visibility DataUpdateVisible
        {
            get { return this._dataUpdateVisible; }
            set
            {
                this._dataUpdateVisible = value;
                NotifyPropertyChanged();

            }
        }
		private delegate void FunctionKeyHandler(object sender, KeyEventArgs e);

		private Dictionary<MessageType, Action<CommunicationObject>> functionList
			= new Dictionary<MessageType, Action<CommunicationObject>>();

		private List<string> RequestList = new List<string>();

		private Mutex mutex = null;

		/// <summary>
		/// ログ出力クラス
		/// </summary>
		public AppLogger appLog = AppLogger.Instance;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="appdata">アプリケーション共有オブジェクト</param>
		public RibbonWindowViewBase(object appdata = null)
			: base()
		{
			IsLoadFinished = false;
			IsRenderFinished = false;
			this.IsClosing = false;

			InitializeBase();
		}

		void InitializeBase()
		{
			this.Unloaded += RibbonWindowViewBase_Unloaded;
			this.Initialized += RibbonWindowViewBase_Initialized;
			this.Closing += RibbonWindowViewBase_Closing;
			this.Closed += RibbonWindowViewBase_Closed;
			this.Loaded += RibbonWindowViewBase_Loaded;
			this.PreviewKeyDown += Window_PreviewKeyDown;
			this.PreviewMouseUp += RibbonWindowViewBase_PreviewMouseUp;
			this.PreviewGotKeyboardFocus += RibbonWindowViewBase_PreviewGotKeyboardFocus;

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

		void RibbonWindowViewBase_Unloaded(object sender, RoutedEventArgs e)
		{
			appLog.Info("<FW> □ {0} Unloaded", string.IsNullOrWhiteSpace(this.Title) ? this.GetType().Name : this.Title);
			this.Unloaded -= RibbonWindowViewBase_Unloaded;

			if (OnFinishWindowClosed != null)
			{
				OnFinishWindowClosed(this);
			}
		}

		private void RibbonWindowViewBase_Initialized(object sender, EventArgs e)
		{
			RibbonWindowViewBase_Startup();

		}

        #region "クローズ開始"
        public Boolean CloseFlg = false;   // 画面クルーズフラグ
        public Boolean EditFlg = false;    // セル編集中フラグ
        /// <summary>
        /// RibbonWindowViewBase_Closing
        /// 派生からのクローズ処理で最初に実行されるメソッド
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		void RibbonWindowViewBase_Closing(object sender, CancelEventArgs e)
		{
            // スプレッドセル編集中判断
            // var EditFlg = sender.GetType().GetField("EditFlg", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            //if (EditFlg != null && (Boolean)EditFlg.GetValue(sender))
            //{
            //    return;
            //}
            if (EditFlg)
            {
                // スプレッドセル編集フラグ
                EditFlg = false;
                return;
            }

			this.IsClosing = true;
			ViewBaseCommon.WindowClosing(this);

			appLog.Info("<FW> □ {0} Closing", string.IsNullOrWhiteSpace(this.Title) ? this.GetType().Name : this.Title);
			if (this.threadmgr != null)
			{ 
				this.threadmgr.Dispose();
			}
		}
        #endregion

        void RibbonWindowViewBase_Loaded(object sender, RoutedEventArgs e)
		{
			appLog.Info("<FW> ■ {0} Loaded", string.IsNullOrWhiteSpace(this.Title) ? this.GetType().Name : this.Title);

			//HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
			//source.AddHook(new HwndSourceHook(WndProc));
			foreach (var ctl in ViewBaseCommon.FindLogicalChildList<FrameworkControl>(this, false))
			{
				ctl.ConnectStringUserDB = this.ConnString;
				var cbox = ctl as UcLabelComboBox;
				if (cbox != null)
				{
					cbox.GetComboboxList();
				}
                // ToolTip用追加 ST
                UcLabelTextBox tlbox = ctl as UcLabelTextBox;
                if (tlbox != null && tlbox.cIsReadOnly == false)
                {
                    tlbox.Foreground = new SolidColorBrush(Colors.Black);
                    if (tlbox.ToolTip != null)
                    {
                        tlbox.ToolTip = Convert.ToString(tlbox.ToolTip) + Environment.NewLine + (ToolTipSet(tlbox) == string.Empty ? null : ToolTipSet(tlbox));
                    }
                    else
                    {
                        tlbox.ToolTip = (ToolTipSet(tlbox) == string.Empty ? null : ToolTipSet(tlbox));
                    }
                    tlbox.Label_ToolTip = null;
                    //tlbox.ToolTip = ToolTipSet(tlbox) == string.Empty ? null : ToolTipSet(tlbox);
                }
                UcLabelTwinTextBox tltbox = ctl as UcLabelTwinTextBox;
                if (tltbox != null && (tltbox.Text1IsReadOnly == false || tltbox.Text2IsReadOnly == false) && tltbox.Name != "Root")
                {
                    tltbox.Foreground = new SolidColorBrush(Colors.Black);
                    // Code部分
                    if (tltbox.Text1ToolTip != null && tltbox.Text1IsReadOnly == false)
                    {
                        tltbox.Text1ToolTip = Convert.ToString(tltbox.Text1ToolTip) + Environment.NewLine + (ToolTipSet(tltbox)[0] == string.Empty ? null : ToolTipSet(tltbox)[0]);
                    }
                    else if (tltbox.Text1IsReadOnly == false)
                    {
                        tltbox.Text1ToolTip = ToolTipSet(tltbox)[0] == string.Empty ? null : ToolTipSet(tltbox)[0];
                    }

                    // Value部分
                    if (tltbox.Text2ToolTip != null && tltbox.Text2IsReadOnly == false)
                    {
                        tltbox.Text2ToolTip = Convert.ToString(tltbox.Text2ToolTip) + Environment.NewLine + (ToolTipSet(tltbox)[1] == string.Empty ? null : ToolTipSet(tltbox)[1]);
                    }
                    else if (tltbox.Text2IsReadOnly == false)
                    {
                        tltbox.Text2ToolTip = ToolTipSet(tltbox)[1] == string.Empty ? null : ToolTipSet(tltbox)[1];
                    }
                    tltbox.Label_ToolTip = null;
                    //tltbox.ToolTip = tltbox.Text1ToolTip + " " + tltbox.Text2ToolTip;
                }
                UcTextBox tbox = ctl as UcTextBox;
                if (tbox != null && tbox.cIsReadOnly == false && tbox.Name != "cTextBox" && tbox.Name != "Root")
                {
                    tbox.Foreground = new SolidColorBrush(Colors.Black);
                    if (tbox.Name != "CodeText" && tbox.Name != "ValueText")
                    {
                        if (tbox.ToolTip != null)
                        {
                            tbox.ToolTip = Convert.ToString(tbox.ToolTip) + Environment.NewLine + (ToolTipSet(tbox) == string.Empty ? null : ToolTipSet(tbox));
                        }
                        else
                        {
                            tbox.ToolTip = ToolTipSet(tbox) == string.Empty ? null : ToolTipSet(tbox);
                        }
                    }
                    else
                    {
                        // "CodeText","ValueText"
                        if (tbox.cToolTip == null || tbox.cToolTip.ToString().Length == 0)
                        {
                            tbox.ToolTip = ToolTipSet(tbox) == string.Empty ? null : ToolTipSet(tbox);
                        }
                        else
                        {
                            tbox.ToolTip = tbox.cToolTip;
                        }
                    }
                    //tbox.ToolTip = ToolTipSet(tbox) == string.Empty ? null : ToolTipSet(tbox);
                }
                // ToolTip用追加 ED
            }

			SetFocusToTopControl();

            //// 権限関係 ロード後にccfgが設定されるためベースでは判断不可
            //string PGID = this.GetType().Name;
            //object ccfg_tmp = sender.GetType().GetField("ccfg",
            //                            System.Reflection.BindingFlags.Public |
            //                            System.Reflection.BindingFlags.Instance |
            //                            System.Reflection.BindingFlags.NonPublic).GetValue(sender);
            //if (ccfg_tmp != null)
            //{
            //    CommonConfig ccfg = new CommonConfig();
            //    ccfg.ユーザID = (int)ccfg_tmp.GetType().GetField("ユーザID").GetValue(ccfg_tmp);
            //    ccfg.ユーザ名 = (string)ccfg_tmp.GetType().GetField("ユーザ名").GetValue(ccfg_tmp);
            //    ccfg.権限 = (int)ccfg_tmp.GetType().GetField("権限").GetValue(ccfg_tmp);
            //    ccfg.ログイン時刻 = (DateTime)ccfg_tmp.GetType().GetField("ログイン時刻").GetValue(ccfg_tmp);
            //    ccfg.ログアウト時刻 = (DateTime)ccfg_tmp.GetType().GetField("ログアウト時刻").GetValue(ccfg_tmp);
            //    ccfg.ライセンスID = (string)ccfg_tmp.GetType().GetField("ライセンスID").GetValue(ccfg_tmp);
            //    //権限関係追加
            //    ccfg.プログラムID = (string[])ccfg_tmp.GetType().GetField("プログラムID").GetValue(ccfg_tmp);
            //    ccfg.使用可能FLG = (Boolean[])ccfg_tmp.GetType().GetField("使用可能FLG").GetValue(ccfg_tmp);
            //    ccfg.データ更新FLG = (Boolean[])ccfg_tmp.GetType().GetField("データ更新FLG").GetValue(ccfg_tmp);
            //    if (!Authority_Update_Button(ccfg, PGID))
            //    {
            //        this.DataUpdateVisible = Visibility.Hidden;
            //    }
            //}

            IsLoadFinished = true;

		}

        #region "ToolTip設定 追加"
        /// <summary>
        /// UcLabelTextBox用
        /// </summary>
        /// <param name="Con"></param>
        /// <returns></returns>
        private string ToolTipSet(UcLabelTextBox Con)
        {
            return ToolTipSetString(Con.ValidationType, Con.cMaxLength, Con.MaxValue, Con.MinValue, Con.Mask);
        }

        /// <summary>
        /// UcLabelTwinTextBox用
        /// </summary>
        /// <param name="Con"></param>
        /// <returns></returns>
        private string[] ToolTipSet(UcLabelTwinTextBox Con)
        {
            string[] RetVal = new string[2];

            RetVal[0] = ToolTipSetString(Con.Text1ValidationType, Con.Text1MaxLength, Con.Text1Mask);
            RetVal[1] = ToolTipSetString(Con.Text2ValidationType, Con.Text2MaxLength, Con.Text1Mask);

            return RetVal;
        }

        /// <summary>
        /// UcTextBox用
        /// </summary>
        /// <param name="Con"></param>
        /// <returns></returns>
        private string ToolTipSet(UcTextBox Con)
        {
            return ToolTipSetString(Con.ValidationType, Con.cMaxLength, Con.MaxValue, Con.MinValue, Con.Mask);
        }

        /// <summary>
        /// ToolTipSetString
        /// </summary>
        /// <param name="ConVal">コントロールタイプ</param>
        /// <param name="MaxLength">最大桁</param>
        /// <param name="MaxValue">最大値</param>
        /// <param name="MinValue">最少値</param>
        /// <param name="Mask">マスク文字列</param>
        /// <returns></returns>
        private string ToolTipSetString(Validator.ValidationTypes ConVal,
                                        int MaxLength = 0,
                                        string MaxValue = null,
                                        string MinValue = null,
                                        string Mask = null)
        {
            var RetVal = string.Empty;
            var SeisuLen = string.Empty;
            var SyosuLen = string.Empty;
            var SignedString = string.Empty;

            switch (ConVal)
            {
                case Validator.ValidationTypes.Date:
                case Validator.ValidationTypes.DateYYYYMM:
                case Validator.ValidationTypes.DateMMDD:
                    RetVal += "日付を入力します。";
                    if (Mask != null && Mask.Length > 0)
                    {
                        RetVal += Environment.NewLine;
                        RetVal += string.Format("形式は{0:@}です。", Mask.ToUpper());
                    }
                    break;

                case Validator.ValidationTypes.DateTime:
                    RetVal += "日付／時間を入力します。";
                    if (Mask != null && Mask.Length > 0)
                    {
                        RetVal += Environment.NewLine;
                        RetVal += string.Format("形式は{0:@}です。", Mask.ToUpper());
                    }
                    break;

                case Validator.ValidationTypes.Time:
                    RetVal += "時間を入力します。";
                    if (Mask != null && Mask.Length > 0)
                    {
                        RetVal += Environment.NewLine;
                        RetVal += string.Format("形式は{0:@}です。", Mask.ToUpper());
                    }
                    break;

                case Validator.ValidationTypes.Integer:
                case Validator.ValidationTypes.SignedInteger:
                case Validator.ValidationTypes.Number:
                case Validator.ValidationTypes.Decimal:
                case Validator.ValidationTypes.SignedDecimal:
                case Validator.ValidationTypes.SignedNumber:
                    if (MaxValue != null && MaxValue.Length > 0)
                    {
                        var SEISU = 0;
                        var SYOSU = 0;
                        if (MaxValue.IndexOf(".") > 0)
                        {
                            SEISU = Convert.ToInt32(MaxValue.Substring(0,MaxValue.IndexOf(".")));
                            SYOSU = Convert.ToInt32(MaxValue.Substring(MaxValue.IndexOf(".") + 1));
                            if (SEISU > 0) { SeisuLen = MaxValue.IndexOf(".").ToString(); }
                            if (SYOSU > 0) { SyosuLen = ((MaxValue.Length - MaxValue.IndexOf(".")) - 1).ToString(); }
                        }
                        else
                        {
                            SeisuLen = MaxValue.Length.ToString();
                        }
                    }
                    if (MinValue != null && Convert.ToDecimal(MinValue) >= 0)
                    {
                        SignedString = " マイナス入力不可";
                    }
                    
                    if (SeisuLen.Length > 0 && SyosuLen.Length > 0)
                    {
                        RetVal += string.Format("最大整数{0:@}桁、小数{1:@}桁", SeisuLen, SyosuLen);
                    }else if(SeisuLen.Length > 0 ){
                        RetVal += string.Format("最大整数{0:@}桁", SeisuLen);
                    }
                    else if (SyosuLen.Length >0 ) {
                        RetVal += string.Format("最大小数{0:@}桁", SyosuLen);
                    }

                    if (MaxLength > 0 && RetVal.Length == 0)
                    {
                        RetVal += string.Format("最大{0:#}桁", MaxLength);
                    }

                    if (SignedString.Length > 0)
                    {
                        RetVal += SignedString;
                    }

                    break;

                case Validator.ValidationTypes.None:
                case Validator.ValidationTypes.ASCII:
                case Validator.ValidationTypes.Nihongo:
                case Validator.ValidationTypes.String:
                    if (MaxLength > 0)
                    {
                        if ((MaxLength / 2) > 0)
                        {
                            RetVal += string.Format("最大全角{0:#}文字、半角{1:#}文字", MaxLength / 2, MaxLength);
                        }
                        else
                        {
                            RetVal += string.Format("最大半角{0:#}文字", MaxLength);
                        }
                    }
                    break;
            }

            //var MaxMin = string.Empty;
            //if (MinValue != null && MinValue.Length > 0)
            //{
            //    MaxMin += string.Format("最少値は{0:@}です。", MinValue);
            //}
            //if (MaxValue != null && MaxValue.Length > 0) 
            //{
            //    if (MaxMin.Length > 0) { MaxMin += Environment.NewLine; }
            //    MaxMin += string.Format("最大値は{0:@}です。", MaxValue);
            //}

            //if (RetVal.Length > 0 && MaxMin.Length > 0) { RetVal += Environment.NewLine; }
            //RetVal += MaxMin;

            return RetVal;
        }
        #endregion

        protected override void OnContentRendered(EventArgs e)
		{
			base.OnContentRendered(e);

			IsRenderFinished = true;
			if (OnFinishWindowRendered != null)
			{
				OnFinishWindowRendered(this);
			}
		}

        //private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		//{
		//	return IntPtr.Zero;
		//}

		void RibbonWindowViewBase_Startup()
		{
			object appdata = null;
			if (this.Owner is IWindowViewBase)
			{
				appdata = (this.Owner as IWindowViewBase).viewsCommData.AppData;
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
			RibbonInitialize();

			this.ErrorMessage = string.Empty;
			this.MaintenanceMode = string.Empty;
		}

		void rbn_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
		}

		void rbn_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
		}

		void rbn_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
		}

		void RibbonWindowViewBase_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
            // ToolTip用
            if (!IsLoadFinished) { e.Handled = true; return; }

            var ctl = e.NewFocus;
			if (ctl is Control)
			{
				if ((ctl as Control).ToolTip == null)
				{
					this.ErrorMessage = string.Empty;
				}
				else
				{
					if ((ctl as Control).ToolTip is string)
					{
						this.ErrorMessage = (ctl as Control).ToolTip as string;
					}
				}
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

		void RibbonInitialize()
		{
			Ribbon ribbon = SearchRibbon(this);
			if (ribbon != null)
			{
				ribbon.PreviewMouseDoubleClick += rbn_PreviewMouseDoubleClick;
				ribbon.PreviewMouseRightButtonDown += rbn_PreviewMouseRightButtonDown;
				ribbon.PreviewMouseRightButtonUp += rbn_PreviewMouseRightButtonUp;

				if (ribbon.ApplicationMenu == null)
				{
					ribbon.ApplicationMenu = new RibbonApplicationMenu();
				}
				ribbon.ApplicationMenu.Visibility = System.Windows.Visibility.Collapsed;

				// 処理モード表示（「新規」・「編集」）用ラベルの追加
				//RibbonTab rtab = SearchFirstRibbonTab(ribbon);
				//if (rtab != null)
				//{
				//	RibbonGroup grp = new RibbonGroup();
				//	Label label = new Label();
				//	label.FontSize = 20;
				//	label.FontWeight = FontWeights.Bold;
				//	label.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
				//	label.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
				//	label.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
				//	label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
				//	label.SetBinding(ContentProperty, "MaintenanceMode");
				//	label.SetBinding(ForegroundProperty, "MaintenanceModeForeground");
				//	label.SetBinding(BackgroundProperty, "MaintenanceModeBackground");
				//	grp.Background = new SolidColorBrush(Colors.White);
				//	grp.BorderBrush = Brushes.Black;
				//	grp.BorderThickness = new Thickness(1.5);
				//	grp.SetBinding(VisibilityProperty, "MaintenanceModeVisibility");
				//	grp.Width = 80;
				//	grp.Items.Insert(0, label);
				//	rtab.Items.Insert(0, grp);
				//}

				ribbon.Focusable = false;
				ribbon.IsTabStop = false;
				UnfocusableRibbonTabs(ribbon);
			}
			// リボンの拡張ボタンに対応する機能のバインディング
			// AppConfigで定義された内容を当てはめる。
			var ribbonbutonHelpSettings = (NameValueCollection)ConfigurationManager.GetSection("ribbonbutonHelpSettings");
			foreach (string key in ribbonbutonHelpSettings)
			{
				string value = ribbonbutonHelpSettings[key];
				int ixL = value.IndexOf("${");
				int ixR = value.IndexOf("}");
				if (ixL >= 0 && ixR > (ixL + 2))
				{
					string envnm = value.Substring(ixL + 2, ixR - ixL - 2);
					string envstr = System.Environment.GetEnvironmentVariable(envnm);
					if (string.IsNullOrEmpty(envstr))
					{
						value = value.Replace("${" + envnm + "}", "");
					}
					else
					{
						value = value.Replace("${" + envnm + "}", envstr);
					}
				}
				ribbonbuttonlinks.Add(key, new string[] { value });
			}

		}

		private Ribbon SearchRibbon(DependencyObject target)
		{
			foreach (var child in LogicalTreeHelper.GetChildren(target))
			{
				if (child is DependencyObject)
				{
					if (child is Ribbon)
					{
						return child as Ribbon;
					}
					Ribbon ribbon = SearchRibbon((DependencyObject)child);
					if (ribbon != null)
					{
						return ribbon;
					}
				}
			}
			return null;
		}

		private RibbonTab SearchFirstRibbonTab(DependencyObject target)
		{
			foreach (var child in LogicalTreeHelper.GetChildren(target))
			{
				if (child is DependencyObject)
				{
					if (child is RibbonTab)
					{
						return child as RibbonTab;
					}
					RibbonTab rtab = SearchFirstRibbonTab((DependencyObject)child);
					if (rtab != null)
					{
						return rtab;
					}
				}
			}
			return null;
		}

		private RibbonTab UnfocusableRibbonTabs(DependencyObject target)
		{
			foreach (var child in LogicalTreeHelper.GetChildren(target))
			{
				if (child is DependencyObject)
				{
					if (child is RibbonTab)
					{
						(child as RibbonTab).Focusable = false;
						(child as RibbonTab).IsTabStop = false;
					}
					else if (child is RibbonGroup)
					{
						(child as RibbonGroup).Focusable = false;
						(child as RibbonGroup).IsTabStop = false;
					}
					else if (child is RibbonButton)
					{
						(child as RibbonButton).Focusable = false;
						(child as RibbonButton).IsTabStop = false;
					}
					RibbonTab rtab = UnfocusableRibbonTabs((DependencyObject)child);
				}
			}
			return null;
		}

		/// <summary>
		/// 画面を表示する
		/// </summary>
		/// <param name="wnd">呼び出し元Window</param>
		public void Show(Window wnd = null)
		{
			try
			{
				//bool flag;
				//// ミューテックスを生成する
				//this.mutex = new Mutex(false, @"Global\" + this.Title, out flag);
				//if (flag != true)
				//{
				//	throw new ApplicationException();
				//}
                // 権限関係
                if (wnd.GetType().GetField("ccfg",
                                            System.Reflection.BindingFlags.Public |
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic) != null)
                {

                    string PGID = this.GetType().Name;
                    object ccfg_tmp = wnd.GetType().GetField("ccfg",
                                                        System.Reflection.BindingFlags.Public |
                                                        System.Reflection.BindingFlags.Instance |
                                                        System.Reflection.BindingFlags.NonPublic).GetValue(wnd);
                    if (ccfg_tmp != null && ccfg_tmp.GetType().GetField("プログラムID") != null)
                    {
                        CommonConfig ccfg = new CommonConfig();
                        ccfg.ユーザID = (int)ccfg_tmp.GetType().GetField("ユーザID").GetValue(ccfg_tmp);
                        ccfg.ユーザ名 = (string)ccfg_tmp.GetType().GetField("ユーザ名").GetValue(ccfg_tmp);
                        ccfg.権限 = (int)ccfg_tmp.GetType().GetField("権限").GetValue(ccfg_tmp);
                        ccfg.ログイン時刻 = (DateTime)ccfg_tmp.GetType().GetField("ログイン時刻").GetValue(ccfg_tmp);
                        ccfg.ログアウト時刻 = (DateTime)ccfg_tmp.GetType().GetField("ログアウト時刻").GetValue(ccfg_tmp);
                        ccfg.ライセンスID = (string)ccfg_tmp.GetType().GetField("ライセンスID").GetValue(ccfg_tmp);
                        //権限関係追加
                        ccfg.プログラムID = (string[])ccfg_tmp.GetType().GetField("プログラムID").GetValue(ccfg_tmp);
                        ccfg.使用可能FLG = (Boolean[])ccfg_tmp.GetType().GetField("使用可能FLG").GetValue(ccfg_tmp);
                        ccfg.データ更新FLG = (Boolean[])ccfg_tmp.GetType().GetField("データ更新FLG").GetValue(ccfg_tmp);
                        if (!Authority_Disp_Close(ccfg, PGID))
                        {
                            return;
                        }
                    }
                }
			}
			catch (ApplicationException)
			{
				// グローバル・ミューテックスの多重起動禁止
				MessageBox.Show("すでに起動しています。", "多重起動禁止");
				return;
			}
			this.Owner = wnd != null && wnd.IsActive ? wnd : null;
			setConfigFromParent(wnd);
			base.Show();
		}

        private class CommonConfig
        {
            public int ユーザID = -1;
            public string ユーザ名 = string.Empty;
            public int 権限 = 0;
            public DateTime ログイン時刻;
            public DateTime ログアウト時刻;
            public string ライセンスID;
            //権限関係追加
            //public Dictionary<string, 権限> 権限List = new Dictionary<string, 権限>();
            public string[] プログラムID;
            public Boolean[] 使用可能FLG;
            public Boolean[] データ更新FLG;
        }

        /// <summary>
        /// 権限マスタより画面が表示可能か
        /// </summary>
        /// <param name="AuthorityData"></param>
        /// <param name="画面ID"></param>
        /// <returns>表示可能=True、表示不可能=False</returns>
        private Boolean Authority_Disp_Close(CommonConfig AuthorityData, string 画面ID)
        {
            Boolean RetVal = true;
            try
            {
                string[] プログラムID = AuthorityData.プログラムID.ToArray();
                foreach (var ID in AuthorityData.プログラムID)
                {
                    if (ID == 画面ID)
                    {
                        RetVal = AuthorityData.使用可能FLG[Array.IndexOf(プログラムID, 画面ID)];
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return RetVal;
        }

        /// <summary>
        /// 権限マスタより登録ボタンが表示可能か
        /// </summary>
        /// <param name="AuthorityData"></param>
        /// <param name="画面ID"></param>
        /// <returns>表示可能=True、表示不可能=False</returns>
        private Boolean Authority_Update_Button(CommonConfig AuthorityData, string 画面ID)
        {
            Boolean RetVal = true;
            try
            {
                string[] プログラムID = AuthorityData.プログラムID.ToArray();
                foreach (var ID in AuthorityData.プログラムID)
                {
                    if (ID == 画面ID)
                    {
                        RetVal = AuthorityData.データ更新FLG[Array.IndexOf(プログラムID, 画面ID)];
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return RetVal;
        }

		/// <summary>
		/// 画面を表示する（ダイアログモード）
		/// </summary>
		/// <param name="wnd">呼び出し元Window</param>
		/// <returns>ダイアログ処理結果</returns>
		public bool? ShowDialog(Window wnd = null)
		{
			try
			{
				//bool flag;
				//// ミューテックスを生成する
				//this.mutex = new Mutex(false, @"Global\" + this.Title, out flag);
				//if (flag != true)
				//{
				//	throw new ApplicationException();
				//}

                // 権限関係
                if (wnd.GetType().GetField("ccfg", 
                                            System.Reflection.BindingFlags.Public | 
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic) != null){

                    string PGID = this.GetType().Name;
                    object ccfg_tmp = wnd.GetType().GetField("ccfg", 
                                                        System.Reflection.BindingFlags.Public | 
                                                        System.Reflection.BindingFlags.Instance |
                                                        System.Reflection.BindingFlags.NonPublic).GetValue(wnd);
                    if (ccfg_tmp != null && ccfg_tmp.GetType().GetField("プログラムID") != null)
                    {
                        CommonConfig ccfg = new CommonConfig();
                        ccfg.ユーザID = (int)ccfg_tmp.GetType().GetField("ユーザID").GetValue(ccfg_tmp);
                        ccfg.ユーザ名 = (string)ccfg_tmp.GetType().GetField("ユーザ名").GetValue(ccfg_tmp);
                        ccfg.権限 = (int)ccfg_tmp.GetType().GetField("権限").GetValue(ccfg_tmp);
                        ccfg.ログイン時刻 = (DateTime)ccfg_tmp.GetType().GetField("ログイン時刻").GetValue(ccfg_tmp);
                        ccfg.ログアウト時刻 = (DateTime)ccfg_tmp.GetType().GetField("ログアウト時刻").GetValue(ccfg_tmp);
                        ccfg.ライセンスID = (string)ccfg_tmp.GetType().GetField("ライセンスID").GetValue(ccfg_tmp);
                        //権限関係追加
                        ccfg.プログラムID = (string[])ccfg_tmp.GetType().GetField("プログラムID").GetValue(ccfg_tmp);
                        ccfg.使用可能FLG = (Boolean[])ccfg_tmp.GetType().GetField("使用可能FLG").GetValue(ccfg_tmp);
                        ccfg.データ更新FLG =  (Boolean[])ccfg_tmp.GetType().GetField("データ更新FLG").GetValue(ccfg_tmp);
                        if (!Authority_Disp_Close(ccfg, PGID))
                        {
                            return false;
                        }
                    }
                }
            }
			catch (ApplicationException)
			{
				// グローバル・ミューテックスの多重起動禁止
				MessageBox.Show("すでに起動しています。", "多重起動禁止");
				return false;
			}
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void RibbonWindowViewBase_Closed(object sender, EventArgs e)
		{
			this.Initialized -= RibbonWindowViewBase_Initialized;
			this.Closing -= RibbonWindowViewBase_Closing;
			this.Closed -= RibbonWindowViewBase_Closed;
			this.Loaded -= RibbonWindowViewBase_Loaded;
			this.PreviewKeyDown -= Window_PreviewKeyDown;
			this.PreviewMouseUp -= RibbonWindowViewBase_PreviewMouseUp;
			this.PreviewGotKeyboardFocus -= RibbonWindowViewBase_PreviewGotKeyboardFocus;

			this.SetFreeForInput();

			appLog.Info("<FW> □ {0} Close", string.IsNullOrWhiteSpace(this.Title) ? this.GetType().Name : this.Title);
			//if (this.mutex != null)
			//{
			//	try
			//	{
			//		appLog.Debug("<FW> □ {0} ReleaseMutex", string.IsNullOrWhiteSpace(this.Title) ? this.GetType().Name : this.Title);
			//		this.mutex.ReleaseMutex();
			//		this.mutex.Close();
			//		this.mutex = null;
			//	}
			//	catch (Exception ex)
			//	{
			//		appLog.Debug("<FW> □ {0} ReleaseMutex", ex);
			//	}
			//}
			if (this.threadmgr != null)
			{
				appLog.Debug("<FW> □ {0} threadmgr.Dispose", string.IsNullOrWhiteSpace(this.Title) ? this.GetType().Name : this.Title);
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

		}

		//public void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
		//{
		//}

		/// <summary>
		/// タイマーループを開始する
		/// </summary>
		/// <param name="time">イベント周期時間</param>
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
		/// <param name="comobj">データアクセス要求オブジェクト</param>
		public void SendRequest(CommunicationObject comobj)
		{
			if (this.IsClosing) return;

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
		/// データ処理要求の配列をまとめて送信する
		/// </summary>
		/// <param name="comobjlist">データアクセス要求オブジェクトの配列</param>
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

						//RequestList.Add(comobj.GetMessageName());
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
		/// <param name="message">データアクセス結果オブジェクト</param>
		public void OnReceived(CommunicationObject message)
		{
			appLog.Debug("<FW> {0} Received {1}({2})", this.GetType().Name, message.mType, message.GetMessageName());
			lock (RequestList)
			{
				//RequestList.Remove(message.GetMessageName());
				//if (RequestList.Count == 0)
				//{
				//	Dispatcher.Invoke(new ReceivedDelegate(OnRequestEmpty), message);
				//}
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
		/// <param name="message">タイマーイベントオブジェクト</param>
		public virtual void OnReceivedTimer(CommunicationObject message)
		{
		}

		/// <summary>
		/// エラー受信時の処理
		/// </summary>
		/// <param name="message">データアクセス結果オブジェクト</param>
		public virtual void OnReveivedError(CommunicationObject message)
		{
			this.ErrorMessage = ViewBaseCommon.OnReveivedError(message, this.viewsCommData);
		}

		/// <summary>
		/// 取得データの取り込み
		/// </summary>
		/// <param name="message">受信データ</param>
		public virtual void OnReceivedResponseData(CommunicationObject message)
		{
			if (message.mType == MessageType.ResponseWithFree)
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
		/// キー項目としてマークされたコントロールの値チェックを実行し、結果を取得する
		/// </summary>
		/// <param name="checkOnly">true指定の場合、エラーフィールドにフォーカスを移動しない</param>
		/// <returns>チェック結果：true=OK、false=NG</returns>
		public bool CheckKeyItemValidation(bool checkOnly = false)
		{
			return ViewBaseCommon.CheckKeyItemValidation(this, checkOnly);
		}

		/// <summary>
		/// Window内のValidationのチェック結果を全てリセットする。
		/// </summary>
		public void ResetAllValidation()
		{
			ViewBaseCommon.ResetAllValidation(this);
            ErrorMessage = string.Empty;
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


		void RibbonWindowViewBase_PreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			//if (e.Source is FrameworkControl)
			//{
			//	this.ErrorMessage = string.Empty;
			//}
		}

		private void Window_PreviewKeyDown(object sender, KeyEventArgs e)		
        {
			if (IsRenderFinished != true)
			{
				e.Handled = true;
				return;
			}

			this.ErrorMessage = string.Empty;
            // 権限関係（F9・F12）
            Visibility DataUpdateVisible = (Visibility)sender.GetType().GetProperty("DataUpdateVisible").GetValue(sender, null);
            if (DataUpdateVisible == Visibility.Hidden && (e.Key == Key.F9 || e.Key == Key.F12)) { e.Handled = true; return; }

			ViewBaseCommon.CallFunctionKeyMethod(this, sender, e, this.funckeymethodlist);
		}

        private static bool DelegateToSearchCriteria(System.Reflection.MemberInfo objMemberInfo, Object objSearch)
        {
            // Compare the name of the member function with the filter criteria.
            if (objMemberInfo.Name.ToString() == objSearch.ToString())
                return true;
            else
                return false;
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
		/// <param name="sender">イベント発生オブジェクト</param>
		/// <param name="e">イベント引数</param>
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
		/// リボンボタンクリック
		/// </summary>
		/// <param name="sender">イベント発生オブジェクト</param>
		/// <param name="e">イベント引数</param>
		public virtual void RibbonButton_Click(object sender, RoutedEventArgs e)
		{
			if ((e.Source is RibbonButton) != true)
			{
				return;
			}
			var ctl = FocusManager.GetFocusedElement(this);
			if (ctl is TextBox)
			{
				var uctxt = ViewBaseCommon.FindVisualParent<UcTextBox>(ctl as TextBox);
				if (uctxt != null)
				{
					uctxt.ApplyFormat();
				}
			}

			RibbonButton rbtn = e.Source as RibbonButton;
			var func = funckeymethodlist.Where(x => x.Key.ToString() == rbtn.KeyTip).FirstOrDefault().Value;
			if (func != null)
			{
				func(sender, null);
				return;
			}

			try
			{
				if (ribbonbuttonlinks.ContainsKey(rbtn.KeyTip) != true)
				{
					return;
				}
				string[] proc = ribbonbuttonlinks[rbtn.KeyTip];
				if (proc.Length > 1)
				{
					string args = proc[1];
					Process.Start(proc[0], args);
				}
				else
				{
					Process.Start(proc[0]);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("実行できません。\r\n理由：{0}\r\nサポートにご連絡ください。", ex.Message));
			}
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
		/// フォーカスのあるコントロールがDataGridである場合、選択されているCellを取得する
		/// </summary>
		/// <param name="curctl">検索対象コントロール</param>
		/// <returns>検索結果</returns>
		public DataGridCell GetCurrentDataGridCell(object curctl)
		{
			return ViewBaseCommon.GetCurrentDataGridCell(curctl as DependencyObject);
		}

		/// <summary>
		/// DataGridCellのマウスクリックイベントの処理（Cellのフォーカス移動を強制する）
		/// </summary>
		/// <param name="sender">イベント発生コントロール</param>
		/// <param name="e">イベント引数</param>
		public void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			ViewBaseCommon.DataGridCell_PreviewMouseLeftButtonDown(sender, e);
		}

		/// <summary>
		/// DataGridの中から指定されたタイプのコントロールを取得する
		/// </summary>
		/// <typeparam name="T">検索対象のコントロールタイプ</typeparam>
		/// <param name="cell">DataGridのCellオブジェクト</param>
		/// <returns>検索結果</returns>
		public T FindUIElementInDataGridCell<T>(DataGridCell cell) where T : FrameworkElement
		{
			return ViewBaseCommon.FindUIElementInDataGridCell<T>(cell);
		}

		/// <summary>
		/// DataGridの中から指定されたタイプのコントロールのコレクションを取得する
		/// </summary>
		/// <typeparam name="T">検索対象のコントロールタイプ</typeparam>
		/// <param name="cell">DataGridのCellオブジェクト</param>
		/// <returns>検索結果</returns>
		public List<T> FindUIElementListInDataGridCell<T>(DataGridCell cell) where T : FrameworkElement
		{
			return ViewBaseCommon.FindUIElementListInDataGridCell<T>(cell);
		}

		/// <summary>
		/// DataGridの指定セルの枠線を正常値色にする
		/// </summary>
		/// <param name="cell">DataGridのCellオブジェクト</param>
		public void SetValidDataGridCell(DataGridCell cell)
		{
			ViewBaseCommon.SetValidDataGridCell(cell);
		}

		/// <summary>
		/// DataGridの指定セルの枠線を異常値色にする
		/// </summary>
		/// <param name="cell">DataGridのCellオブジェクト</param>
		public void SetInvalidDataGridCell(DataGridCell cell)
		{
			ViewBaseCommon.SetInvalidDataGridCell(cell);
		}

		/// <summary>
		/// 引数で指定された接続文字列をセットアップする
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
		/// <param name="propertyName">Bindingプロパティ名</param>
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
           