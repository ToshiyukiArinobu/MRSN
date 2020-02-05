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
	/// ��ʊ��N���X�iRibbon����j
	/// </summary>
	public abstract class RibbonWindowViewBase : RibbonWindow, IWindowViewBase
	{
		public event FinishWindowClosedHandler OnFinishWindowClosed;
		public event FinishWindowRenderedHandler OnFinishWindowRendered;

		public bool IsLoadFinished { get; set; }
		public bool IsRenderFinished { get; set; }

		/// <summary>
		/// ���ݕ����悤�Ƃ��Ă��邩�ǂ����������t���O
		/// </summary>
		public new bool IsClosing { get; set; }

		/// <summary>
		/// DB�ڑ��p
		/// </summary>
		public string ConnString { get; set; }

		/// <summary>
		/// ��ʋ��L�I�u�W�F�N�g
		/// </summary>
		public ViewsCommon viewsCommData { get; set; }

		/// <summary>
		/// �X���b�h�Ǘ��@�\
		/// </summary>
		public ThreadManeger threadmgr = null;

		/// <summary>
		/// �}�X�^�[�����e�i���X��ʌďo�p�n���h���̃R���N�V����
		/// </summary>
		public Dictionary<string, List<Type>> MasterMaintenanceWindowList = new Dictionary<string, List<Type>>();
		private Dictionary<Key, Action<object, KeyEventArgs>> funckeymethodlist = new Dictionary<Key, Action<object, KeyEventArgs>>();
		private Dictionary<string, string[]> ribbonbuttonlinks = new Dictionary<string, string[]>();


		private string _message = string.Empty;
		/// <summary>
		/// ��ʂɒ�`���ꂽ���b�Z�[�W�̈�ɕ\�����郁�b�Z�[�W�i�G���[���b�Z�[�W�ȊO�̗p�r�j
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
		/// ��ʂɒ�`���ꂽ�G���[���b�Z�[�W�̈�̃R���g���[���̕\���E��\���̃t���O
		/// </summary>
		public Visibility ErrorMessageVisibility
		{
			get { return this._errmessageVisibility; }
			set { this._errmessageVisibility = value; NotifyPropertyChanged(); }
		}
		private string _errormessage = string.Empty;
		/// <summary>
		/// ��ʂɒ�`���ꂽ�G���[���b�Z�[�W�̈�ɕ\�����郁�b�Z�[�W
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
		/// ��ʏ������[�h�̕\���E��\���̃t���O
		/// </summary>
		public Visibility MaintenanceModeVisibility
		{
			get { return this._maintenanceModeVisibility; }
			set { this._maintenanceModeVisibility = value; NotifyPropertyChanged(); }
		}

		private string _maintenanceMode = string.Empty;
		/// <summary>
		/// ��ʏ������[�h�i�ǉ��E�ҏW���j
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
					if (value.Contains("�V"))
					{
						this.MaintenanceModeForeground = new SolidColorBrush(Colors.White);
						this.MaintenanceModeBackground = new SolidColorBrush(Colors.Tomato);
					}
					else if (value.Contains("��"))
					{
						this.MaintenanceModeForeground = new SolidColorBrush(Colors.White);
						this.MaintenanceModeBackground = new SolidColorBrush(Colors.CornflowerBlue);
					}
                    else if (value.Contains("�m"))
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
		/// �����e�i���X���[�h�̕����\���F
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
		/// �����e�i���X���[�h�̔w�i�\���F
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
        /// �����֌W
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
		/// ���O�o�̓N���X
		/// </summary>
		public AppLogger appLog = AppLogger.Instance;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="appdata">�A�v���P�[�V�������L�I�u�W�F�N�g</param>
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
			appLog.Info("<FW> �� {0} Unloaded", string.IsNullOrWhiteSpace(this.Title) ? this.GetType().Name : this.Title);
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

        #region "�N���[�Y�J�n"
        public Boolean CloseFlg = false;   // ��ʃN���[�Y�t���O
        public Boolean EditFlg = false;    // �Z���ҏW���t���O
        /// <summary>
        /// RibbonWindowViewBase_Closing
        /// �h������̃N���[�Y�����ōŏ��Ɏ��s����郁�\�b�h
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		void RibbonWindowViewBase_Closing(object sender, CancelEventArgs e)
		{
            // �X�v���b�h�Z���ҏW�����f
            // var EditFlg = sender.GetType().GetField("EditFlg", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            //if (EditFlg != null && (Boolean)EditFlg.GetValue(sender))
            //{
            //    return;
            //}
            if (EditFlg)
            {
                // �X�v���b�h�Z���ҏW�t���O
                EditFlg = false;
                return;
            }

			this.IsClosing = true;
			ViewBaseCommon.WindowClosing(this);

			appLog.Info("<FW> �� {0} Closing", string.IsNullOrWhiteSpace(this.Title) ? this.GetType().Name : this.Title);
			if (this.threadmgr != null)
			{ 
				this.threadmgr.Dispose();
			}
		}
        #endregion

        void RibbonWindowViewBase_Loaded(object sender, RoutedEventArgs e)
		{
			appLog.Info("<FW> �� {0} Loaded", string.IsNullOrWhiteSpace(this.Title) ? this.GetType().Name : this.Title);

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
                // ToolTip�p�ǉ� ST
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
                    // Code����
                    if (tltbox.Text1ToolTip != null && tltbox.Text1IsReadOnly == false)
                    {
                        tltbox.Text1ToolTip = Convert.ToString(tltbox.Text1ToolTip) + Environment.NewLine + (ToolTipSet(tltbox)[0] == string.Empty ? null : ToolTipSet(tltbox)[0]);
                    }
                    else if (tltbox.Text1IsReadOnly == false)
                    {
                        tltbox.Text1ToolTip = ToolTipSet(tltbox)[0] == string.Empty ? null : ToolTipSet(tltbox)[0];
                    }

                    // Value����
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
                // ToolTip�p�ǉ� ED
            }

			SetFocusToTopControl();

            //// �����֌W ���[�h���ccfg���ݒ肳��邽�߃x�[�X�ł͔��f�s��
            //string PGID = this.GetType().Name;
            //object ccfg_tmp = sender.GetType().GetField("ccfg",
            //                            System.Reflection.BindingFlags.Public |
            //                            System.Reflection.BindingFlags.Instance |
            //                            System.Reflection.BindingFlags.NonPublic).GetValue(sender);
            //if (ccfg_tmp != null)
            //{
            //    CommonConfig ccfg = new CommonConfig();
            //    ccfg.���[�UID = (int)ccfg_tmp.GetType().GetField("���[�UID").GetValue(ccfg_tmp);
            //    ccfg.���[�U�� = (string)ccfg_tmp.GetType().GetField("���[�U��").GetValue(ccfg_tmp);
            //    ccfg.���� = (int)ccfg_tmp.GetType().GetField("����").GetValue(ccfg_tmp);
            //    ccfg.���O�C������ = (DateTime)ccfg_tmp.GetType().GetField("���O�C������").GetValue(ccfg_tmp);
            //    ccfg.���O�A�E�g���� = (DateTime)ccfg_tmp.GetType().GetField("���O�A�E�g����").GetValue(ccfg_tmp);
            //    ccfg.���C�Z���XID = (string)ccfg_tmp.GetType().GetField("���C�Z���XID").GetValue(ccfg_tmp);
            //    //�����֌W�ǉ�
            //    ccfg.�v���O����ID = (string[])ccfg_tmp.GetType().GetField("�v���O����ID").GetValue(ccfg_tmp);
            //    ccfg.�g�p�\FLG = (Boolean[])ccfg_tmp.GetType().GetField("�g�p�\FLG").GetValue(ccfg_tmp);
            //    ccfg.�f�[�^�X�VFLG = (Boolean[])ccfg_tmp.GetType().GetField("�f�[�^�X�VFLG").GetValue(ccfg_tmp);
            //    if (!Authority_Update_Button(ccfg, PGID))
            //    {
            //        this.DataUpdateVisible = Visibility.Hidden;
            //    }
            //}

            IsLoadFinished = true;

		}

        #region "ToolTip�ݒ� �ǉ�"
        /// <summary>
        /// UcLabelTextBox�p
        /// </summary>
        /// <param name="Con"></param>
        /// <returns></returns>
        private string ToolTipSet(UcLabelTextBox Con)
        {
            return ToolTipSetString(Con.ValidationType, Con.cMaxLength, Con.MaxValue, Con.MinValue, Con.Mask);
        }

        /// <summary>
        /// UcLabelTwinTextBox�p
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
        /// UcTextBox�p
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
        /// <param name="ConVal">�R���g���[���^�C�v</param>
        /// <param name="MaxLength">�ő包</param>
        /// <param name="MaxValue">�ő�l</param>
        /// <param name="MinValue">�ŏ��l</param>
        /// <param name="Mask">�}�X�N������</param>
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
                    RetVal += "���t����͂��܂��B";
                    if (Mask != null && Mask.Length > 0)
                    {
                        RetVal += Environment.NewLine;
                        RetVal += string.Format("�`����{0:@}�ł��B", Mask.ToUpper());
                    }
                    break;

                case Validator.ValidationTypes.DateTime:
                    RetVal += "���t�^���Ԃ���͂��܂��B";
                    if (Mask != null && Mask.Length > 0)
                    {
                        RetVal += Environment.NewLine;
                        RetVal += string.Format("�`����{0:@}�ł��B", Mask.ToUpper());
                    }
                    break;

                case Validator.ValidationTypes.Time:
                    RetVal += "���Ԃ���͂��܂��B";
                    if (Mask != null && Mask.Length > 0)
                    {
                        RetVal += Environment.NewLine;
                        RetVal += string.Format("�`����{0:@}�ł��B", Mask.ToUpper());
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
                        SignedString = " �}�C�i�X���͕s��";
                    }
                    
                    if (SeisuLen.Length > 0 && SyosuLen.Length > 0)
                    {
                        RetVal += string.Format("�ő吮��{0:@}���A����{1:@}��", SeisuLen, SyosuLen);
                    }else if(SeisuLen.Length > 0 ){
                        RetVal += string.Format("�ő吮��{0:@}��", SeisuLen);
                    }
                    else if (SyosuLen.Length >0 ) {
                        RetVal += string.Format("�ő召��{0:@}��", SyosuLen);
                    }

                    if (MaxLength > 0 && RetVal.Length == 0)
                    {
                        RetVal += string.Format("�ő�{0:#}��", MaxLength);
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
                            RetVal += string.Format("�ő�S�p{0:#}�����A���p{1:#}����", MaxLength / 2, MaxLength);
                        }
                        else
                        {
                            RetVal += string.Format("�ő唼�p{0:#}����", MaxLength);
                        }
                    }
                    break;
            }

            //var MaxMin = string.Empty;
            //if (MinValue != null && MinValue.Length > 0)
            //{
            //    MaxMin += string.Format("�ŏ��l��{0:@}�ł��B", MinValue);
            //}
            //if (MaxValue != null && MaxValue.Length > 0) 
            //{
            //    if (MaxMin.Length > 0) { MaxMin += Environment.NewLine; }
            //    MaxMin += string.Format("�ő�l��{0:@}�ł��B", MaxValue);
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
            // ToolTip�p
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

				// �������[�h�\���i�u�V�K�v�E�u�ҏW�v�j�p���x���̒ǉ�
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
			// ���{���̊g���{�^���ɑΉ�����@�\�̃o�C���f�B���O
			// AppConfig�Œ�`���ꂽ���e�𓖂Ă͂߂�B
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
		/// ��ʂ�\������
		/// </summary>
		/// <param name="wnd">�Ăяo����Window</param>
		public void Show(Window wnd = null)
		{
			try
			{
				//bool flag;
				//// �~���[�e�b�N�X�𐶐�����
				//this.mutex = new Mutex(false, @"Global\" + this.Title, out flag);
				//if (flag != true)
				//{
				//	throw new ApplicationException();
				//}
                // �����֌W
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
                    if (ccfg_tmp != null && ccfg_tmp.GetType().GetField("�v���O����ID") != null)
                    {
                        CommonConfig ccfg = new CommonConfig();
                        ccfg.���[�UID = (int)ccfg_tmp.GetType().GetField("���[�UID").GetValue(ccfg_tmp);
                        ccfg.���[�U�� = (string)ccfg_tmp.GetType().GetField("���[�U��").GetValue(ccfg_tmp);
                        ccfg.���� = (int)ccfg_tmp.GetType().GetField("����").GetValue(ccfg_tmp);
                        ccfg.���O�C������ = (DateTime)ccfg_tmp.GetType().GetField("���O�C������").GetValue(ccfg_tmp);
                        ccfg.���O�A�E�g���� = (DateTime)ccfg_tmp.GetType().GetField("���O�A�E�g����").GetValue(ccfg_tmp);
                        ccfg.���C�Z���XID = (string)ccfg_tmp.GetType().GetField("���C�Z���XID").GetValue(ccfg_tmp);
                        //�����֌W�ǉ�
                        ccfg.�v���O����ID = (string[])ccfg_tmp.GetType().GetField("�v���O����ID").GetValue(ccfg_tmp);
                        ccfg.�g�p�\FLG = (Boolean[])ccfg_tmp.GetType().GetField("�g�p�\FLG").GetValue(ccfg_tmp);
                        ccfg.�f�[�^�X�VFLG = (Boolean[])ccfg_tmp.GetType().GetField("�f�[�^�X�VFLG").GetValue(ccfg_tmp);
                        if (!Authority_Disp_Close(ccfg, PGID))
                        {
                            return;
                        }
                    }
                }
			}
			catch (ApplicationException)
			{
				// �O���[�o���E�~���[�e�b�N�X�̑��d�N���֎~
				MessageBox.Show("���łɋN�����Ă��܂��B", "���d�N���֎~");
				return;
			}
			this.Owner = wnd != null && wnd.IsActive ? wnd : null;
			setConfigFromParent(wnd);
			base.Show();
		}

        private class CommonConfig
        {
            public int ���[�UID = -1;
            public string ���[�U�� = string.Empty;
            public int ���� = 0;
            public DateTime ���O�C������;
            public DateTime ���O�A�E�g����;
            public string ���C�Z���XID;
            //�����֌W�ǉ�
            //public Dictionary<string, ����> ����List = new Dictionary<string, ����>();
            public string[] �v���O����ID;
            public Boolean[] �g�p�\FLG;
            public Boolean[] �f�[�^�X�VFLG;
        }

        /// <summary>
        /// �����}�X�^����ʂ��\���\��
        /// </summary>
        /// <param name="AuthorityData"></param>
        /// <param name="���ID"></param>
        /// <returns>�\���\=True�A�\���s�\=False</returns>
        private Boolean Authority_Disp_Close(CommonConfig AuthorityData, string ���ID)
        {
            Boolean RetVal = true;
            try
            {
                string[] �v���O����ID = AuthorityData.�v���O����ID.ToArray();
                foreach (var ID in AuthorityData.�v���O����ID)
                {
                    if (ID == ���ID)
                    {
                        RetVal = AuthorityData.�g�p�\FLG[Array.IndexOf(�v���O����ID, ���ID)];
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
        /// �����}�X�^���o�^�{�^�����\���\��
        /// </summary>
        /// <param name="AuthorityData"></param>
        /// <param name="���ID"></param>
        /// <returns>�\���\=True�A�\���s�\=False</returns>
        private Boolean Authority_Update_Button(CommonConfig AuthorityData, string ���ID)
        {
            Boolean RetVal = true;
            try
            {
                string[] �v���O����ID = AuthorityData.�v���O����ID.ToArray();
                foreach (var ID in AuthorityData.�v���O����ID)
                {
                    if (ID == ���ID)
                    {
                        RetVal = AuthorityData.�f�[�^�X�VFLG[Array.IndexOf(�v���O����ID, ���ID)];
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
		/// ��ʂ�\������i�_�C�A���O���[�h�j
		/// </summary>
		/// <param name="wnd">�Ăяo����Window</param>
		/// <returns>�_�C�A���O��������</returns>
		public bool? ShowDialog(Window wnd = null)
		{
			try
			{
				//bool flag;
				//// �~���[�e�b�N�X�𐶐�����
				//this.mutex = new Mutex(false, @"Global\" + this.Title, out flag);
				//if (flag != true)
				//{
				//	throw new ApplicationException();
				//}

                // �����֌W
                if (wnd.GetType().GetField("ccfg", 
                                            System.Reflection.BindingFlags.Public | 
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic) != null){

                    string PGID = this.GetType().Name;
                    object ccfg_tmp = wnd.GetType().GetField("ccfg", 
                                                        System.Reflection.BindingFlags.Public | 
                                                        System.Reflection.BindingFlags.Instance |
                                                        System.Reflection.BindingFlags.NonPublic).GetValue(wnd);
                    if (ccfg_tmp != null && ccfg_tmp.GetType().GetField("�v���O����ID") != null)
                    {
                        CommonConfig ccfg = new CommonConfig();
                        ccfg.���[�UID = (int)ccfg_tmp.GetType().GetField("���[�UID").GetValue(ccfg_tmp);
                        ccfg.���[�U�� = (string)ccfg_tmp.GetType().GetField("���[�U��").GetValue(ccfg_tmp);
                        ccfg.���� = (int)ccfg_tmp.GetType().GetField("����").GetValue(ccfg_tmp);
                        ccfg.���O�C������ = (DateTime)ccfg_tmp.GetType().GetField("���O�C������").GetValue(ccfg_tmp);
                        ccfg.���O�A�E�g���� = (DateTime)ccfg_tmp.GetType().GetField("���O�A�E�g����").GetValue(ccfg_tmp);
                        ccfg.���C�Z���XID = (string)ccfg_tmp.GetType().GetField("���C�Z���XID").GetValue(ccfg_tmp);
                        //�����֌W�ǉ�
                        ccfg.�v���O����ID = (string[])ccfg_tmp.GetType().GetField("�v���O����ID").GetValue(ccfg_tmp);
                        ccfg.�g�p�\FLG = (Boolean[])ccfg_tmp.GetType().GetField("�g�p�\FLG").GetValue(ccfg_tmp);
                        ccfg.�f�[�^�X�VFLG =  (Boolean[])ccfg_tmp.GetType().GetField("�f�[�^�X�VFLG").GetValue(ccfg_tmp);
                        if (!Authority_Disp_Close(ccfg, PGID))
                        {
                            return false;
                        }
                    }
                }
            }
			catch (ApplicationException)
			{
				// �O���[�o���E�~���[�e�b�N�X�̑��d�N���֎~
				MessageBox.Show("���łɋN�����Ă��܂��B", "���d�N���֎~");
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

			appLog.Info("<FW> �� {0} Close", string.IsNullOrWhiteSpace(this.Title) ? this.GetType().Name : this.Title);
			//if (this.mutex != null)
			//{
			//	try
			//	{
			//		appLog.Debug("<FW> �� {0} ReleaseMutex", string.IsNullOrWhiteSpace(this.Title) ? this.GetType().Name : this.Title);
			//		this.mutex.ReleaseMutex();
			//		this.mutex.Close();
			//		this.mutex = null;
			//	}
			//	catch (Exception ex)
			//	{
			//		appLog.Debug("<FW> �� {0} ReleaseMutex", ex);
			//	}
			//}
			if (this.threadmgr != null)
			{
				appLog.Debug("<FW> �� {0} threadmgr.Dispose", string.IsNullOrWhiteSpace(this.Title) ? this.GetType().Name : this.Title);
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
		/// �^�C�}�[���[�v���J�n����
		/// </summary>
		/// <param name="time">�C�x���g��������</param>
		public void TimerLoopStart(int time)
		{
			this.threadmgr.TimerLoopStart(time);
		}

		/// <summary>
		/// �^�C�}�[���[�v���~����
		/// </summary>
		public void TimerLoopStop()
		{
			this.threadmgr.TimerLoopStop();
		}

		/// <summary>
		/// �f�[�^�����v���𑗐M����
		/// </summary>
		/// <param name="comobj">�f�[�^�A�N�Z�X�v���I�u�W�F�N�g</param>
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
		/// �f�[�^�����v���̔z����܂Ƃ߂đ��M����
		/// </summary>
		/// <param name="comobjlist">�f�[�^�A�N�Z�X�v���I�u�W�F�N�g�̔z��</param>
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
		/// �f�[�^��M�C�x���g
		/// </summary>
		/// <param name="message">�f�[�^�A�N�Z�X���ʃI�u�W�F�N�g</param>
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

		private void OnResponseEmpty(CommunicationObject message /* �����̓_�~�[ */)
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
		/// �^�C�}�[�C�x���g��M���̏���
		/// </summary>
		/// <param name="message">�^�C�}�[�C�x���g�I�u�W�F�N�g</param>
		public virtual void OnReceivedTimer(CommunicationObject message)
		{
		}

		/// <summary>
		/// �G���[��M���̏���
		/// </summary>
		/// <param name="message">�f�[�^�A�N�Z�X���ʃI�u�W�F�N�g</param>
		public virtual void OnReveivedError(CommunicationObject message)
		{
			this.ErrorMessage = ViewBaseCommon.OnReveivedError(message, this.viewsCommData);
		}

		/// <summary>
		/// �擾�f�[�^�̎�荞��
		/// </summary>
		/// <param name="message">��M�f�[�^</param>
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
		/// �X�g�A�h�̎��s���ʎ�M����
		/// </summary>
		/// <param name="message">��M�f�[�^</param>
		public virtual void OnReceivedResponseStored(CommunicationObject message)
		{
		}

		/// <summary>
		/// �t�H�[�J�X��擪�̃R���g���[���Ɉړ�����
		/// </summary>
		/// <returns>true:�ړ������Afalse:�ړ����Ă��Ȃ�</returns>
		public bool SetFocusToTopControl()
		{
			return ViewBaseCommon.SetFocusToTopControl(this);
		}

		/// <summary>
		/// �L�[���ڂƂ��ă}�[�N���ꂽ���ڂ̓��͉ۂ�؂�ւ���
		/// </summary>
		/// <param name="flag">true:���͉Afalse:���͕s��</param>
		public void ChangeKeyItemChangeable(bool flag)
		{
			ViewBaseCommon.ChangeKeyItemChangeable(flag, this);
		}

		/// <summary>
		/// Window����Validator�v���p�e�B�����S���ڂ��`�F�b�N����B
		/// </summary>
		/// <returns>true:�S����OK�Afalse:�`�F�b�NNG���ڂ���</returns>
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
		/// �L�[���ڂƂ��ă}�[�N���ꂽ�R���g���[���̒l�`�F�b�N�����s���A���ʂ��擾����
		/// </summary>
		/// <param name="checkOnly">true�w��̏ꍇ�A�G���[�t�B�[���h�Ƀt�H�[�J�X���ړ����Ȃ�</param>
		/// <returns>�`�F�b�N���ʁFtrue=OK�Afalse=NG</returns>
		public bool CheckKeyItemValidation(bool checkOnly = false)
		{
			return ViewBaseCommon.CheckKeyItemValidation(this, checkOnly);
		}

		/// <summary>
		/// Window����Validation�̃`�F�b�N���ʂ�S�ă��Z�b�g����B
		/// </summary>
		public void ResetAllValidation()
		{
			ViewBaseCommon.ResetAllValidation(this);
            ErrorMessage = string.Empty;
		}

		/// <summary>
		/// Window����͕s�ɂ���i�}�E�X�J�[�\���������v�ɂ���j
		/// </summary>
		public void SetBusyForInput()
		{
			ViewBaseCommon.SetBusyForInput(this);
		}

		/// <summary>
		/// Window����͉\�ɂ���i�}�E�X�J�[�\����߂��j
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
            // �����֌W�iF9�EF12�j
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
		/// F1�L�[�����p���\�b�h
		/// </summary>
		/// <param name="sender">�C�x���g�����I�u�W�F�N�g</param>
		/// <param name="e">�C�x���g����</param>
		public virtual void OnF1Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// F2�L�[�����p���\�b�h
		/// </summary>
		/// <param name="sender">�C�x���g�����I�u�W�F�N�g</param>
		/// <param name="e">�C�x���g����</param>
		public virtual void OnF2Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// F3�L�[�����p���\�b�h
		/// </summary>
		/// <param name="sender">�C�x���g�����I�u�W�F�N�g</param>
		/// <param name="e">�C�x���g����</param>
		public virtual void OnF3Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// F4�L�[�����p���\�b�h
		/// </summary>
		/// <param name="sender">�C�x���g�����I�u�W�F�N�g</param>
		/// <param name="e">�C�x���g����</param>
		public virtual void OnF4Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// F5�L�[�����p���\�b�h
		/// </summary>
		/// <param name="sender">�C�x���g�����I�u�W�F�N�g</param>
		/// <param name="e">�C�x���g����</param>
		public virtual void OnF5Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// F6�L�[�����p���\�b�h
		/// </summary>
		/// <param name="sender">�C�x���g�����I�u�W�F�N�g</param>
		/// <param name="e">�C�x���g����</param>
		public virtual void OnF6Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// F7�L�[�����p���\�b�h
		/// </summary>
		/// <param name="sender">�C�x���g�����I�u�W�F�N�g</param>
		/// <param name="e">�C�x���g����</param>
		public virtual void OnF7Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// F8�L�[�����p���\�b�h
		/// </summary>
		/// <param name="sender">�C�x���g�����I�u�W�F�N�g</param>
		/// <param name="e">�C�x���g����</param>
		public virtual void OnF8Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// F9�L�[�����p���\�b�h
		/// </summary>
		/// <param name="sender">�C�x���g�����I�u�W�F�N�g</param>
		/// <param name="e">�C�x���g����</param>
		public virtual void OnF9Key(object sender, KeyEventArgs e)
		{
        }

		/// <summary>
		/// F10�L�[�����p���\�b�h
		/// </summary>
		/// <param name="sender">�C�x���g�����I�u�W�F�N�g</param>
		/// <param name="e">�C�x���g����</param>
		public virtual void OnF10Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// F11�L�[�����p���\�b�h
		/// </summary>
		/// <param name="sender">�C�x���g�����I�u�W�F�N�g</param>
		/// <param name="e">�C�x���g����</param>
		public virtual void OnF11Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// F12�L�[�����p���\�b�h
		/// </summary>
		/// <param name="sender">�C�x���g�����I�u�W�F�N�g</param>
		/// <param name="e">�C�x���g����</param>
		public virtual void OnF12Key(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// ���{���{�^���N���b�N
		/// </summary>
		/// <param name="sender">�C�x���g�����I�u�W�F�N�g</param>
		/// <param name="e">�C�x���g����</param>
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
				MessageBox.Show(string.Format("���s�ł��܂���B\r\n���R�F{0}\r\n�T�|�[�g�ɂ��A�����������B", ex.Message));
			}
		}

		/// <summary>
		/// �w�肳�ꂽ�R���g���[���̓����ɑ��݂���DataGrid��T���A����DataGridRow���擾����
		/// </summary>
		/// <param name="curctl">�����ΏۃR���g���[��</param>
		/// <returns>��������</returns>
		public DataGridRow GetDataGridRow(object curctl)
		{
			return ViewBaseCommon.GetDataGridRow(curctl as DependencyObject);
		}

		/// <summary>
		/// �w�肳�ꂽ�J�����y��Row�̏�񂩂�DataGrid��Cell���擾����
		/// </summary>
		/// <param name="col">DataGrid��Column���</param>
		/// <param name="grow">DataGrid��Row�I�u�W�F�N�g</param>
		/// <returns>��������</returns>
		public DataGridCell GetDataGridCell(DataGridColumn col, DataGridRow grow)
		{
			return ViewBaseCommon.GetDataGridCell(col, grow);
		}

		/// <summary>
		/// ���݃t�H�[�J�X�̂���DataGrid��DataGridRow���擾����
		/// </summary>
		/// <param name="curctl">�����ΏۃR���g���[��</param>
		/// <returns>��������</returns>
		public DataRowView GetCurrentDataGridRow(object curctl)
		{
			return ViewBaseCommon.GetCurrentDataGridRow(curctl as DependencyObject);
		}

		/// <summary>
		/// ���݃t�H�[�J�X�̂���DataGrid���擾����
		/// </summary>
		/// <param name="curctl">�����ΏۃR���g���[��</param>
		/// <returns>��������</returns>
		public DataGrid GetCurrentDataGrid(object curctl)
		{
			return ViewBaseCommon.GetCurrentDataGrid(curctl as DependencyObject);
		}

		/// <summary>
		/// �t�H�[�J�X�̂���R���g���[����DataGrid�ł���ꍇ�A�I������Ă���Cell���擾����
		/// </summary>
		/// <param name="curctl">�����ΏۃR���g���[��</param>
		/// <returns>��������</returns>
		public DataGridCell GetCurrentDataGridCell(object curctl)
		{
			return ViewBaseCommon.GetCurrentDataGridCell(curctl as DependencyObject);
		}

		/// <summary>
		/// DataGridCell�̃}�E�X�N���b�N�C�x���g�̏����iCell�̃t�H�[�J�X�ړ�����������j
		/// </summary>
		/// <param name="sender">�C�x���g�����R���g���[��</param>
		/// <param name="e">�C�x���g����</param>
		public void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			ViewBaseCommon.DataGridCell_PreviewMouseLeftButtonDown(sender, e);
		}

		/// <summary>
		/// DataGrid�̒�����w�肳�ꂽ�^�C�v�̃R���g���[�����擾����
		/// </summary>
		/// <typeparam name="T">�����Ώۂ̃R���g���[���^�C�v</typeparam>
		/// <param name="cell">DataGrid��Cell�I�u�W�F�N�g</param>
		/// <returns>��������</returns>
		public T FindUIElementInDataGridCell<T>(DataGridCell cell) where T : FrameworkElement
		{
			return ViewBaseCommon.FindUIElementInDataGridCell<T>(cell);
		}

		/// <summary>
		/// DataGrid�̒�����w�肳�ꂽ�^�C�v�̃R���g���[���̃R���N�V�������擾����
		/// </summary>
		/// <typeparam name="T">�����Ώۂ̃R���g���[���^�C�v</typeparam>
		/// <param name="cell">DataGrid��Cell�I�u�W�F�N�g</param>
		/// <returns>��������</returns>
		public List<T> FindUIElementListInDataGridCell<T>(DataGridCell cell) where T : FrameworkElement
		{
			return ViewBaseCommon.FindUIElementListInDataGridCell<T>(cell);
		}

		/// <summary>
		/// DataGrid�̎w��Z���̘g���𐳏�l�F�ɂ���
		/// </summary>
		/// <param name="cell">DataGrid��Cell�I�u�W�F�N�g</param>
		public void SetValidDataGridCell(DataGridCell cell)
		{
			ViewBaseCommon.SetValidDataGridCell(cell);
		}

		/// <summary>
		/// DataGrid�̎w��Z���̘g�����ُ�l�F�ɂ���
		/// </summary>
		/// <param name="cell">DataGrid��Cell�I�u�W�F�N�g</param>
		public void SetInvalidDataGridCell(DataGridCell cell)
		{
			ViewBaseCommon.SetInvalidDataGridCell(cell);
		}

		/// <summary>
		/// �����Ŏw�肳�ꂽ�ڑ���������Z�b�g�A�b�v����
		/// </summary>
		/// <param name="server">�T�[�o�[��</param>
		/// <param name="db">Database��</param>
		/// <param name="user">���[�U��</param>
		/// <param name="passwd">�p�X���[�h</param>
		public void SetupConnectStringuserDB(string server, string db, string user, string passwd)
		{
			ViewBaseCommon.SetupConnectStringuserDB(this, server, db, user, passwd);
		}

		#region INotifyPropertyChanged �����o�[
		/// <summary>
		/// Binding�@�\�Ή��i�v���p�e�B�̕ύX�ʒm�C�x���g�j
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;
		/// <summary>
		/// Binding�@�\�Ή��i�v���p�e�B�̕ύX�ʒm�C�x���g���M�j
		/// </summary>
		/// <param name="propertyName">Binding�v���p�e�B��</param>
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
           