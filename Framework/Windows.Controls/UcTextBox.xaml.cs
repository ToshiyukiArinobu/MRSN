using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

using KyoeiSystem.Framework.Windows;
using System.Windows.Interop;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace KyoeiSystem.Framework.Windows.Controls
{
	/// <summary>
	/// UcTextBox.xaml の相互作用ロジック
	/// </summary>
	public partial class UcTextBox : FrameworkControl
	{
		public bool IsImeInputing = false;

		private Validator.ValidationTypes _validationType;
        private Brush _originForeground;
		private Brush _originBackground;
		private Brush _originBorderBrush;
		private Thickness _originBorderThickness;
		private object _originTooltip = string.Empty;
		private string _validationStatus = string.Empty;
		private string _originalText = string.Empty;
		private string _validationCustomChars = string.Empty;
		private bool _isUpDownHandling = true;
		private bool _isMouseDownFlag = false;

		private string internalValidationStatus
		{
			get { return this._validationStatus; }
			set
			{
				this._validationStatus = value;
				if (value == string.Empty)
				{
					cTextBox.BorderBrush = this._originBorderBrush;
					cTextBox.BorderThickness = this._originBorderThickness;
					cTextBox.ToolTip = _originTooltip;
				}
				else
				{
					cTextBox.BorderBrush = new SolidColorBrush(System.Windows.Media.Colors.Red);
					cTextBox.BorderThickness = new Thickness(1.7);
                    //cTextBox.ToolTip = value;
                    if (this.ToolTip != null)
                    {
                        cTextBox.ToolTip = value + string.Format("({0:@})", this.ToolTip.ToString().Replace(Environment.NewLine, ""));
                    }
                    else
                    {
                        cTextBox.ToolTip = value;
                    }
				}
			}
		}

		private bool _isModified = false;

		private IMETypes imeType = IMETypes.Off;
		private List<string> _valueList = null;

		// TwinTextboxのコード側の場合のみ trueがセットされる
		public bool IsCodeTextKey = false;
		public bool IsCodeExist = true;

		public bool IsUpDownHandling
		{
			set { this._isUpDownHandling = value; }
			get { return this._isUpDownHandling; }
		}

		public bool IsModified
		{
            set { this._isModified = value; }
            get { return this._isModified; }
		}

		public new bool IsEnabled
		{
			get { return this.cTextBox.IsEnabled; }
			set { this.cTextBox.IsEnabled = value; }
		}

		public new bool Focusable
		{
			get { return this.cTextBox.Focusable; }
			set { this.cTextBox.Focusable = value; }
		}

		private string _dataAccessName = string.Empty;
		public string DataAccessName
		{
			get { return this._dataAccessName; }
			set
			{
				this._dataAccessName = value;
				NotifyPropertyChanged();
			}
		}

		public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
			"Text",
			typeof(string),
			typeof(UcTextBox),
			new UIPropertyMetadata(string.Empty)
			);

		[BindableAttribute(true)]
		public string Text
		{
			get { return this.cText; }
			set
			{
				this.cText = value;
				SetValue(TextProperty, this.cText);
			}
		}

		public static readonly DependencyProperty ValidationProperty = DependencyProperty.Register(
			"Validation",
			typeof(string),
			typeof(UcTextBox),
			new UIPropertyMetadata(string.Empty)
			);

		[BindableAttribute(true)]
		public string Validation
		{
			get { return this.internalValidationStatus; }
			set
			{
				this.internalValidationStatus = value;
				SetValue(ValidationProperty, value);
			}
		}

		# region Maskプロパティ
		private string _mask = string.Empty;
		[Category("動作")]
		public string Mask
		{
			get { return this._mask; }
			set
			{
				this._mask = value;
			}
		}
		#endregion

		private bool _defaultDateIsToday = false;
		[Category("動作")]
		public bool DefaultDateIsToday
		{
			get { return _defaultDateIsToday; }
			set { this._defaultDateIsToday = value; }
		}

		# region AutoSelectプロパティ
		private OnOff _AutoSelect = OnOff.On;
		[Category("動作")]
		public OnOff AutoSelect
		{
			get { return this._AutoSelect; }
			set
			{
				this._AutoSelect = value;
			}
		}
		#endregion

		[Category("動作")]
		public string CustomChars
		{
			get { return this._validationCustomChars; }
			set
			{
				this._validationCustomChars = value;
			}
		}

		//public string DecimalFormat
		//{
		//	get { return this.Mask; }
		//	set { this.Mask = value; this.cTextBox.MaxLength = value.Length; }
		//}

		/// <summary>
		/// TextBox(UserControl)
		/// </summary>
		public UcTextBox()
			: base()
		{
			InitializeComponent();

			TextCompositionManager.AddPreviewTextInputHandler(this.cTextBox, OnPreviewTextInput);
			TextCompositionManager.AddPreviewTextInputStartHandler(this.cTextBox, OnPreviewTextInputStart);
			TextCompositionManager.AddPreviewTextInputUpdateHandler(this.cTextBox, OnPreviewTextInputUpdate);
			TextCompositionManager.AddTextInputUpdateHandler(this.cTextBox, OnTextInputUpdate);

            this._originForeground = this.cTextBox.Foreground;
			this._originBackground = this.cTextBox.Background;
			this._originBorderBrush = this.cTextBox.BorderBrush;
			this._originBorderThickness = this.cTextBox.BorderThickness;
			this._originTooltip = this.cTextBox.ToolTip;
		}

		static class VarDump
		{
			/// <summary>
			/// クラスのフィールドをダンプする
			/// </summary>
			/// <param name="obj">ダンプ対象</param>
			/// <returns>フィールドのダンプリスト</returns>
			public static string Dump(object obj)
			{
				StringBuilder sb = new StringBuilder();
				FieldInfo[] infos = obj.GetType().GetFields();
				foreach (var info in infos)
				{
					sb.AppendLine(string.Format("{0}=[{1}]", info.Name, info.GetValue(obj).ToString()));
				}
				PropertyInfo[] props = obj.GetType().GetProperties();
				foreach (var prop in props)
				{
					sb.AppendLine(string.Format("{0}=[{1}]", prop.Name, prop.GetValue(obj).ToString()));
				}

				return sb.ToString();
			}

		}

		private void OnTextInputUpdate(object sender, TextCompositionEventArgs e)
		{
			Debug.WriteLine("<1> OnTextInputUpdate ----[{0}]", e.TextComposition.CompositionText);
			//this.appLog.Debug("▲ OnTextInputUpdate [{0}] [{1}]", e.TextComposition.CompositionText, this.cTextBox.Text);
			if (e.TextComposition.CompositionText.Length == 0)
				IsImeInputing = false;
		}

		private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			Debug.WriteLine("<2> OnPreviewTextInput ----[{0}]", e.TextComposition.CompositionText);
			//this.appLog.Debug("▲ OnPreviewTextInput [{0}] [{1}]", e.TextComposition.CompositionText, this.cTextBox.Text);
			IsImeInputing = false;
		}
		private void OnPreviewTextInputStart(object sender, TextCompositionEventArgs e)
		{
			Debug.WriteLine("<3> OnPreviewTextInputStart ----[{0}]", e.TextComposition.CompositionText);
			//this.appLog.Debug("▲ OnPreviewTextInputStart [{0}] [{1}]", e.TextComposition.CompositionText, this.cTextBox.Text);

			if (InputMethod.Current.ImeState == InputMethodState.On)
			{
				IsImeInputing = true;
			}
		}
		private void OnPreviewTextInputUpdate(object sender, TextCompositionEventArgs e)
		{
			Debug.WriteLine("<4> OnPreviewTextInputUpdate ----[{0}]", e.TextComposition.CompositionText);
			//this.appLog.Debug("▲ OnPreviewTextInputUpdate [{0}] [{1}]", e.TextComposition.CompositionText, this.cTextBox.Text);
			if (e.TextComposition.CompositionText.Length == 0)
				IsImeInputing = false;
			//if (this.cMaxLength > 0)
			//{
			//	string estr = string.Empty;
			//	if (e.TextComposition != null && string.IsNullOrEmpty(e.TextComposition.CompositionText) != true)
			//	{
			//		estr += e.TextComposition.CompositionText.Last();
			//	}
			//	string text = this.cText + estr;
			//	Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
			//	int num = sjisEnc.GetByteCount(text);
			//	if (num > this.cMaxLength)
			//	{
			//		e.Handled = true;
			//		return;
			//	}
			//}
		}

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);
			if (string.IsNullOrEmpty(this.Mask))
			{
				return;
			}
		}

		public new void Focus()
		{
			Keyboard.Focus(this.cTextBox);
		}

		/// <summary>
		/// 値チェック
		/// </summary>
		[Category("動作")]
		public Validator.ValidationTypes ValidationType
		{
			set { _validationType = value; }
			get { return _validationType; }
		}

		/// <summary>
		/// 値チェックメッセージ
		/// </summary>
		[Category("動作")]
		public string ValidationStatus
		{
			get { return this._validationStatus; }
		}

		/// <summary>
		/// IME設定
		/// </summary>
		[Category("動作")]
        public IMETypes ImeType
		{
			get { return this.imeType; }
			set { this.imeType = value; }
		}

		public List<string> ValueList
		{
			get { return this._valueList; }
			set { this._valueList = value; }
		}

		private bool _isRequired = false;
		/// <summary>
		/// 入力必須項目であるかどうかを指定します。
		/// </summary>
		[Category("動作")]
		public bool IsRequired
		{
			get
			{
				return this._isRequired;
			}
			set
			{
				this._isRequired = value;
				NotifyPropertyChanged();
			}
		}

		public bool MasterCheckEnaled = true;

		#region Event

		/// <summary>
		/// cTextChangeイベントの作成
		/// </summary>
		public static readonly RoutedEvent cTextChangedEvent = EventManager.RegisterRoutedEvent("cTextChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UcTextBox));

		private void TextBox_Initialized(object sender, EventArgs e)
		{
		}

		private void cTextBox_Loaded(object sender, RoutedEventArgs e)
		{
			//HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(Window.GetWindow(this)).Handle);
			//source.AddHook(new HwndSourceHook(WndProc));
		}

		public event RoutedEventHandler cTextChanged
		{
			add { AddHandler(cTextChangedEvent, value); }
			remove { RemoveHandler(cTextChangedEvent, value); }
		}

		/// <summary>
		/// テキスト変更チェック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			Debug.WriteLine("{0} cTextBox_TextChanged ----[{1}]", IsImeInputing ? "□" : "■", VarDump.Dump(e));
			//this.appLog.Debug("{0} cTextBox_TextChanged", IsImeInputing ? "□" : "■");
            try
            {
                if (imeType != IMETypes.Off && IsImeInputing) return;

				if (cAcceptsReturn && Text == "\r\n")
				{
					Text = this._originalText;
				}

				if (string.IsNullOrEmpty(Text))
				{
					this.Text = null;
					if (this.IsRequired != true)
					{
						ResetValidation();
					}
				}
				else
				{
					string txt = string.Empty;
					txt = this.Text;
					bool chk = Validator.InputCheck(this.ValidationType, txt, this.CustomChars, this.ValueList);
					Debug.WriteLine(string.Format("Validator.InputCheck: {0}", chk));
					if (chk != true)
					{
						//this.appLog.Debug("---- cTextBox_TextChanged Validation NG : {0}", this.cTextBox.Text);
						e.Handled = true;
						return;
					}
				}

				RoutedEventArgs NewEventargs = new RoutedEventArgs(UcTextBox.cTextChangedEvent);
				RaiseEvent(NewEventargs);

                //CheckValidation();
            }
            catch
            {
                return;
            }
		}

		private void cTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			//this.appLog.Debug("cTextBox_PreviewTextInput [{0}][{1}]", this.cTextBox.Text, e.Text);

			if (e.Text == "\r")
			{
				//this.appLog.Debug("cTextBox_PreviewTextInput ENTERキー");
				return;
			}

			// ValidationTypeに応じてキー入力の有効無効化
			bool chk = Validator.InputCheck(this.ValidationType, e.Text, this.CustomChars, this.ValueList);
			if (chk != true)
			{
				e.Handled = true;
				return;
			}
			string text = this.cTextBox.Text;
			int start = this.cTextBox.SelectionStart;
			int len = this.cTextBox.SelectionLength;
			if (len > 0)
			{
				text = text.Remove(start, len);
				text = text.Insert(start, e.Text);
				// 数値タイプのとき、. を入力されたらキーカーソル位置をそこに移動する。
				if (e.Text == ".")
				{
					switch (this.ValidationType)
					{
					case Validator.ValidationTypes.Decimal:
					case Validator.ValidationTypes.SignedDecimal:
					case Validator.ValidationTypes.Number:
					case Validator.ValidationTypes.SignedNumber:
						int ppos = this.cTextBox.Text.IndexOf('.');
						if (ppos > 0)
						{
							this.cTextBox.SelectionStart = ppos + 1;
							e.Handled = true;
							return;
						}
						break;
					}
				}

				// 日付タイプの形式をチェックする。
				if (FormatCheck(this.ValidationType, ref text) != true)
				{
					e.Handled = true;
					return;
				}

				//this.cTextBox.SelectionLength = 0;
				//this.cTextBox.Text = text;
				//this.cTextBox.SelectionStart = start + 1;
				//e.Handled = true;
				return;
			}
			else
			{
				if (this.cMaxLength > 0)
				{
					if (InputMethod.Current.ImeState == InputMethodState.Off)
					{
						text = text.Insert(start, e.Text);
					}
					Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
					int num = sjisEnc.GetByteCount(text);
					if (num > this.cMaxLength)
					{
						e.Handled = true;
						return;
					}
				}
				else
				{
					text = text.Insert(start, e.Text);
				}
				if (e.Text == ".")
				{
					switch (this.ValidationType)
					{
					case Validator.ValidationTypes.Decimal:
					case Validator.ValidationTypes.SignedDecimal:
					case Validator.ValidationTypes.Number:
					case Validator.ValidationTypes.SignedNumber:
						int ppos = this.cTextBox.Text.IndexOf('.');
						if (ppos > 0)
						{
							this.cTextBox.SelectionStart = ppos + 1;
							e.Handled = true;
							return;
						}
						break;
					}
				}
				// 日付と数値タイプの入力形式をチェックする。
				if (FormatCheck(this.ValidationType, ref text) != true)
				{
					e.Handled = true;
					return;
				}
				else
				{
					//if (text == this.cTextBox.Text)
					//{
					//	e.Handled = true;
					//	return;
					//}
				}
			}
		}

		private bool FormatCheck(Validator.ValidationTypes vtype, ref string text)
		{
			bool chk = true;

			string[] strlist;
			switch (vtype)
			{
			case Validator.ValidationTypes.Date:
				strlist = text.Split('/');
				if (text.StartsWith("/"))
				{
					return false;
				}
				if (strlist.Length > 3)
				{
					return false;
				}
				switch (strlist.Length)
				{
				case 1:
					if (strlist[0].Length > 8)
					{
						return false;
					}
					break;
				case 2:
					if (strlist[0].Length > 4)
					{
						return false;
					}
					if (strlist[1].Length > 2)
					{
						return false;
					}
					break;
				case 3:
					if (strlist[0].Length > 4)
					{
						return false;
					}
					if (strlist[1].Length > 2)
					{
						return false;
					}
					if (strlist[2].Length > 2)
					{
						return false;
					}
					break;
				}
				break;
			case Validator.ValidationTypes.DateYYYYMM:
				strlist = text.Split('/');
				if (text.StartsWith("/"))
				{
					return false;
				}
				if (strlist.Length > 2)
				{
					return false;
				}
				switch (strlist.Length)
				{
				case 1:
					if (strlist[0].Length > 6)
					{
						return false;
					}
					break;
				case 2:
					if (strlist[0].Length > 4)
					{
						return false;
					}
					if (strlist[1].Length > 2)
					{
						return false;
					}
					break;
				}
				break;
			case Validator.ValidationTypes.DateMMDD:
				strlist = text.Split('/');
				if (text.StartsWith("/"))
				{
					return false;
				}
				if (strlist.Length > 2)
				{
					return false;
				}
				break;
			case Validator.ValidationTypes.Integer:
			case Validator.ValidationTypes.SignedInteger:
			case Validator.ValidationTypes.Decimal:
			case Validator.ValidationTypes.SignedDecimal:
			case Validator.ValidationTypes.Number:
			case Validator.ValidationTypes.SignedNumber:
				// まだマイナスのみしか入力していなければOKとみなす。
				if (text.Trim() == "-" || text.Trim() == "+")
				{
					return true;
				}
				// MaxValue及びMinValueの値により判定
				// 値の範囲指定がある場合、追加のチェックを行う。
				//if (string.IsNullOrWhiteSpace(MinValue) != true || string.IsNullOrWhiteSpace(MaxValue) != true)
				//{
				//	string range = Validator.CheckRange(this.ValidationType, text, this.Mask, this.MinValue, this.MaxValue);
				//	if (string.IsNullOrWhiteSpace(range) != true)
				//	{
				//		internalValidationStatus = range;
				//		//return false;
				//	}
				//}
				
				// 整数部の桁数
				int txtlen = text.Split(new char[] { '.' })[0].Replace(",", "").Replace("-", "").Replace("+", "").Length;
				int maxlen = -1;
				if (string.IsNullOrWhiteSpace(MinValue) != true)
				{
					int mlen = MinValue.Split(new char[] { '.' })[0].Replace(",", "").Replace("-", "").Replace("+", "").Length;
					if (mlen > maxlen)
					{
						maxlen = mlen;
					}
				}
				if (string.IsNullOrWhiteSpace(MaxValue) != true)
				{
					int mlen = MaxValue.Split(new char[] { '.' })[0].Replace(",", "").Replace("-", "").Replace("+", "").Length;
					if (mlen > maxlen)
					{
						maxlen = mlen;
					}
				}
				if (maxlen >= 0 && txtlen > maxlen)
				{
					return false;
				}
				// bindingのFormatに準拠しているかどうかチェック
				Binding bind = null;
				var parent = FindVisualParent<FrameworkControl>(this);
				if (parent != null)
				{
					if (parent is UcLabelTextBox)
					{
						//bind = parent.GetBindingExpression(UcLabelTextBox.TextProperty).ParentBinding;
					}
				}
				else
				{
					var bexp = this.GetBindingExpression(UcTextBox.TextProperty);
					if (bexp != null)
					{
						bind = this.GetBindingExpression(UcTextBox.TextProperty).ParentBinding;
					}
				}
				int len = 0;
				if (string.IsNullOrWhiteSpace(this.Mask) != true && this.Mask.Contains('.'))
				{
					string[] numstr = this.Mask.Split('.');
					len = numstr[numstr.Length - 1].Length;
				}
				if (bind != null)
				{
					string fmt = null;
					fmt = bind.StringFormat;
					if (string.IsNullOrWhiteSpace(fmt) != true && fmt.Contains('.'))
					{
						string[] numstr = fmt.Split('.');
						len = numstr[numstr.Length - 1].Length;
					}
				}
				strlist = text.Split('.');
				if (strlist.Length > 2)
				{
					return false;
				}
				if (strlist.Length == 2)
				{
					if (strlist[1].Contains(","))
					{
						return false;
					}
					if (strlist[1].Length > len)
					{
						text = text.Remove(text.Length - 1);
						//return false;
					}
				}
				break;
			}

			return chk;
		}

		/// <summary>
		/// マウスダウンによってフォーカスを得た場合、TextBoxのGotFocusで全選択したものが解除されるため
		/// マウスダウンを無効化してTextBoxのFocusイベントを起こす。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cTextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			//e.Handled = true;
			cTextBox.Focus();

		}

		/// <summary>
		/// 論理フォーカスを取得したときに発生します。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cTextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			//this.appLog.Debug("cTextBox_GotFocus text：[{0}]", this.cTextBox.Text);
			if (Mouse.LeftButton == MouseButtonState.Pressed)
			{
				if (AutoSelect == OnOff.On)
				{
					cTextBox.SelectAll();
					e.Handled = true;
				}
				_isMouseDownFlag = true;
			}
		}

		private void cTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			//this.appLog.Debug("cTextBox_GotKeyboardFocus text：[{0}]", this.cTextBox.Text);
			this._originalText = cTextBox.Text;
			IMEChange();
			cTextBox.Background = new SolidColorBrush(System.Windows.Media.Colors.Orange);
			if (AutoSelect == OnOff.On)
			{
				cTextBox.SelectAll();
				if (Mouse.LeftButton == MouseButtonState.Pressed)
				{
					_isMouseDownFlag = true;
				}
			}
		}

		/// <summary>
        /// IMEの切り替え
        /// </summary>
        private void IMEChange()
		{

            //初期化
            InputMethod im = InputMethod.Current;
            im.ImeState = InputMethodState.Off;
            ImeConversionModeValues icmv = ImeConversionModeValues.Alphanumeric;
            im.ImeConversionMode = icmv;

            switch(ImeType) {
			case IMETypes.Off:
				InputMethod.Current.ImeState = InputMethodState.Off;
				break;
			case IMETypes.Native:
				InputMethod.Current.ImeState = InputMethodState.On;
				icmv = ImeConversionModeValues.Native | ImeConversionModeValues.FullShape;
				break;
			case IMETypes.Katakana:
				im.ImeState = InputMethodState.On;
				icmv = ImeConversionModeValues.Katakana | ImeConversionModeValues.FullShape | ImeConversionModeValues.Native;
				break;
			case IMETypes.HankakuKatakana:
				im.ImeState = InputMethodState.On;
				icmv = ImeConversionModeValues.Katakana | ImeConversionModeValues.Native;
				break;
			}
            im.ImeConversionMode = icmv;
        }


		/// <summary>
		/// 論理フォーカスを失ったときに発生します。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			cTextBox.Background = _originBackground;
			this.IsModified = (this._originalText == cTextBox.Text) ? false : true;
		}

		private void cTextBox_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			ApplyFormat();
		}

		public void ApplyFormat()
		{
			string text = this.cTextBox.Text;
			if (string.IsNullOrWhiteSpace(text))
			{
				CheckValidation();
				return;
			}
			try
			{
				switch (this.ValidationType)
				{
				default:
					break;
				case Validator.ValidationTypes.Integer:
					text = text.Replace(",", "");
					int i = int.Parse(text, System.Globalization.NumberStyles.Number);
					this.cTextBox.Text = i.ToString(Mask);
					break;
                case Validator.ValidationTypes.SignedInteger:
                    text = text.Replace(",", "");
                    int j = int.Parse(text, System.Globalization.NumberStyles.Number);
                    this.cTextBox.Text = j.ToString(Mask);
                    break;
				case Validator.ValidationTypes.Decimal:
				case Validator.ValidationTypes.Number:
				case Validator.ValidationTypes.SignedDecimal:
				case Validator.ValidationTypes.SignedNumber:
					text = text.Replace(",", "");
					string[] textp = text.Split(new char[] { '.' });
					string[] maskp = Mask.Split(new char[] { '.' });
					if (text.Contains('.') && Mask.Contains('.'))
					{
						int tlen = textp[textp.Length - 1].Length;
						int mlen = maskp[maskp.Length - 1].Length;
						if (tlen > mlen)
						{
							text = text.Substring(0, text.Length - (tlen - mlen));
						}
					}
					decimal d = decimal.Parse(text);
					this.cTextBox.Text = d.ToString(Mask);
					break;
				case Validator.ValidationTypes.DateYYYYMM:
					if (text.Contains("/") != true)
					{
						// 区切り文字を含まず数字のみが入力された場合
						if (text.Length > 6 || text.Length == 0)
						{
							break;
						}
						if (text.Length < 6)
						{
							if (text.Length == 1)
							{
								text = "0" + text;
							}
							// 6桁に満たない部分(左側)を補完する
							if (this.DefaultDateIsToday || string.IsNullOrWhiteSpace(this._originalText))
							{
								// 当日の日付で補完
								string today = DateTime.Today.ToString("yyyyMM");
								text = today.Substring(0, 6 - text.Length) + text;
							}
							else
							{
								// 既に入力されていた内容で補完
								string work = this._originalText.Replace("/", "");
								if (work.Length == 6)
								{
									text = work.Substring(0, 6 - text.Length) + text;
								}
							}
						}
						text = text.Insert(4, "/");
						this.cTextBox.Text = text;
					}
					DateTime dt = new DateTime();
					if (DateTime.TryParse(text + "/01", out dt) == true)
					{
						this.cTextBox.Text = dt.ToString(Mask);
					}
					else
					{
						this.cTextBox.Text = text;
					}
					break;
				case Validator.ValidationTypes.Date:
				case Validator.ValidationTypes.DateMMDD:
					if (text.Contains("/") != true)
					{
						// 区切り文字を含まず数字のみが入力された場合
						if (text.Length > 8 || text.Length == 0)
						{
							break;
						}
						if (text.Length < 8)
						{
							if (text.Length == 1 || text.Length == 3)
							{
								text = "0" + text;
							}
							// 8桁に満たない部分(左側)を補完する
							if (this.DefaultDateIsToday || string.IsNullOrWhiteSpace(this._originalText))
							{
								// 当日の日付で補完
								string today = DateTime.Today.ToString("yyyyMMdd");
								text = today.Substring(0, 8 - text.Length) + text;
							}
							else
							{
								// 既に入力されていた内容で補完
								string work = this._originalText.Replace("/", "");
								if (work.Length == 8)
								{
									text = work.Substring(0, 8 - text.Length) + text;
								}
							}
						}
						text = text.Insert(6, "/").Insert(4, "/");
						this.cTextBox.Text = text;
					}
					DateTime dtM = new DateTime();
					if (DateTime.TryParse(text, out dtM) == true)
					{
						this.cTextBox.Text = dtM.ToString(Mask);
					}
					else
					{
					}
					break;
				case Validator.ValidationTypes.Time:
				case Validator.ValidationTypes.DateTime:
					DateTime tm = DateTime.Parse(text);
					this.cTextBox.Text = tm.ToString(Mask);
					break;
				}
				bool chk = CheckValidation();
				if (!chk)
				{
					return;
				}
			}
			catch (Exception ex)
			{
				// 入力チェックしているので、変換時に例外は発生しないはず。
			}

		}

		// Ctrl キーが押されているかどうか
		private bool IsCtrlKeyPressed
		{
			get
			{
				return 
				 (Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) == KeyStates.Down ||
				 (Keyboard.GetKeyStates(Key.RightCtrl) & KeyStates.Down) == KeyStates.Down;
			}
		}

		// ValidationTypeに応じて入力可能な文字のみをClipboardから抽出する
		private string GetClipString()
		{
			string str = string.Empty;
			foreach (var s in Clipboard.GetText())
			{
				if (s == '.')
				{
					if (this.ValidationType == Validator.ValidationTypes.Integer
					 || this.ValidationType == Validator.ValidationTypes.SignedInteger)
					{
						break;
					}
				}
				bool chk = Validator.InputCheck(this.ValidationType, new string(new char[] { s }), this.CustomChars, this.ValueList);
				if (chk == true)
				{
					str += s;
				}
			}
			return str;
		}

		#endregion

		/// <summary>
		/// Enter、Tab、矢印キーによるフォーカス移動
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void  cTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			TraversalRequest vector = null;
			switch (e.Key)
			{
			case Key.Enter:
				if (cAcceptsReturn == true)
				{
					// 改行許可の場合はフォーカス移動しない
					return;
				}
				vector = new TraversalRequest(FocusNavigationDirection.Next);
				break;
			case Key.Down:
				if (IsUpDownHandling)
					vector = new TraversalRequest(FocusNavigationDirection.Next);
				break;
			case Key.Up:
				if (IsUpDownHandling)
					vector = new TraversalRequest(FocusNavigationDirection.Previous);
				break;
			//左右の矢印キーではフィールドを移動しない
			//case Key.Right:
			//	if (cTextBox.SelectionStart < cTextBox.Text.Length)
			//	{
			//		return;
			//	}
			//	vector = new TraversalRequest(FocusNavigationDirection.Next);
			//	break;
			//case Key.Left:
			//	if (cTextBox.SelectionLength > 0 || cTextBox.SelectionStart > 0)
			//	{
			//		return;
			//	}
			//	vector = new TraversalRequest(FocusNavigationDirection.Previous);
			//	break;
			case Key.Space:
				switch (ValidationType)
				{
				case Validator.ValidationTypes.Date:
				case Validator.ValidationTypes.DateMMDD:
				case Validator.ValidationTypes.DateTime:
				case Validator.ValidationTypes.DateYYYYMM:
				case Validator.ValidationTypes.Decimal:
				case Validator.ValidationTypes.Integer:
				case Validator.ValidationTypes.Number:
				case Validator.ValidationTypes.Time:
				case Validator.ValidationTypes.SignedInteger:
				case Validator.ValidationTypes.SignedDecimal:
				case Validator.ValidationTypes.SignedNumber:
					e.Handled = true;
					break;
				case Validator.ValidationTypes.CustomAutoComplete:
					if (Regex.IsMatch(" ", CustomChars) != true)
					{
						e.Handled = true;
					}
					break;
				}
				break;
			case Key.Back:
				switch (ValidationType)
				{
				case Validator.ValidationTypes.Decimal:
				case Validator.ValidationTypes.Number:
				case Validator.ValidationTypes.SignedDecimal:
				case Validator.ValidationTypes.SignedNumber:
					if (cTextBox.SelectionLength == 0 && cTextBox.SelectionStart > 0)
					{
						if (cTextBox.Text[cTextBox.SelectionStart - 1] == '.')
						{
							cTextBox.SelectionStart--;
							e.Handled = true;
						}
					}
					break;
				}
				break;
			case Key.Delete:
				break;
			case Key.ImeProcessed:
				switch (ValidationType)
				{
				case Validator.ValidationTypes.Date:
				case Validator.ValidationTypes.DateMMDD:
				case Validator.ValidationTypes.DateTime:
				case Validator.ValidationTypes.DateYYYYMM:
				case Validator.ValidationTypes.Decimal:
				case Validator.ValidationTypes.Integer:
				case Validator.ValidationTypes.Number:
				case Validator.ValidationTypes.Time:
				case Validator.ValidationTypes.SignedInteger:
				case Validator.ValidationTypes.SignedDecimal:
				case Validator.ValidationTypes.SignedNumber:
					e.Handled = true;
					break;
				}
				break;
			case Key.V:
				if (!IsCtrlKeyPressed)
				{
					break;
				}
				// Ctrl+V で貼り付けるとき
				string str = string.Empty;
				switch (ValidationType)
				{
				case Validator.ValidationTypes.Date:
				case Validator.ValidationTypes.DateMMDD:
				case Validator.ValidationTypes.DateTime:
				case Validator.ValidationTypes.DateYYYYMM:
				case Validator.ValidationTypes.Time:
					str = GetClipString();
					this.Text = str;
					e.Handled = true;
					break;
				case Validator.ValidationTypes.Integer:
				case Validator.ValidationTypes.SignedInteger:
					str = GetClipString();
					this.Text = str;
					e.Handled = true;
					break;
				case Validator.ValidationTypes.Decimal:
				case Validator.ValidationTypes.Number:
				case Validator.ValidationTypes.SignedDecimal:
				case Validator.ValidationTypes.SignedNumber:
					str = GetClipString();
					string[] strlist = str.Split(new char[] { '.' });
					if (strlist.Length > 1)
					{
						str = string.Format("{0}.{1}", strlist[0], strlist[1]);
					}
					this.Text = str;
					e.Handled = true;
					break;
				}
				break;
			}
			if (vector != null)
			{
				this.Text = cTextBox.Text;
				FocusControl.SetFocusWithOrder(vector);
				UIElement element = Keyboard.FocusedElement as UIElement;
				//if (element is Button || element is ComboBox)
				{
					e.Handled = true;
				}
			}
		}

		public void SetFocus()
		{
			Keyboard.Focus(this.cTextBox);
		}

		public override bool CheckValidation()
		{
			//this.appLog.Debug("CheckValidation [{0}]", this.cTextBox.Text);
			bool result = true;
			string status = string.Empty;
			//this.ApplyFormat();
			if (IsRequired)
			{
				if (string.IsNullOrWhiteSpace(this.cTextBox.Text))
				{
					status = Validator.ValidationMessage.ErrEmpty;
				}
			}
			if (string.IsNullOrEmpty(status))
			{
				//長さチェック
				status = Validator.CheckLength(this.cMaxLength, this.cTextBox.Text);
				if (string.IsNullOrWhiteSpace(status))
				{
					//validatorの動作
					status = Validator.Check(this.ValidationType, this.cTextBox.Text, this.Mask, this.ValueList);
				}
			}
			if (string.IsNullOrEmpty(status) && IsCodeTextKey && !IsCodeExist && MasterCheckEnaled && string.IsNullOrEmpty(this.cTextBox.Text) != true)
			{
				status = Validator.ValidationMessage.ErrNotFound;
			}

			if (string.IsNullOrEmpty(status))
			{
				// 値の範囲指定がある場合、追加のチェックを行う。
				if (string.IsNullOrWhiteSpace(MinValue) != true || string.IsNullOrWhiteSpace(MaxValue) != true)
				{
					status = Validator.CheckRange(this.ValidationType, this.cTextBox.Text, this.Mask, this.MinValue, this.MaxValue);
				}
				if (string.IsNullOrEmpty(status))
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			else
			{
				result = false;
			}

			internalValidationStatus = status;

			Debug.WriteLine(string.Format("<CheckValidation> status:[{0}]", status));

			return result;
		}

		public override string GetValidationMessage()
		{
			return this.internalValidationStatus;
		}

		public void SetValidationMessage(string message)
		{
			this.internalValidationStatus = message;
		}

		public void ResetValidation()
		{
			this.internalValidationStatus = string.Empty;
		}

		#region TextBox Property

		string _MinValue = null;
		string _MaxValue = null;
		[Category("動作")]
		public string MinValue
		{
			get { return _MinValue; }
			set { _MinValue = value; }
		}
		[Category("動作")]
		public string MaxValue
		{
			get { return _MaxValue; }
			set { _MaxValue = value; }
		}


		/// <summary>
		/// AcceptsReturn 依存プロパティ
		/// Enter キーが押されたとき、テキスト編集コントロールがどのように反応するかを示す値を取得または設定します
		/// </summary>
		#region AcceptsReturn
		[Category("動作")]
		public bool cAcceptsReturn
		{
			get { return cTextBox.AcceptsReturn; }
			set { cTextBox.AcceptsReturn = value; IsUpDownHandling = !value; AutoSelect = value ? OnOff.Off : AutoSelect; }
		}
		#endregion

		/// <summary>
		/// AcceptsTab 依存プロパティ
		/// Tab キーが押されたとき、テキスト編集コントロールがどのように反応するかを示す値を取得または設定します
		/// </summary>
		#region AcceptsTab

		[Category("動作")]
		public bool cAcceptsTab
		{
			get { return cTextBox.AcceptsTab; }
			set { cTextBox.AcceptsTab = value; }
		}
		#endregion

		/// <summary>
		/// Background 
		/// コントロールの背景を表すブラシを取得または設定します
		/// </summary>
		#region Background

		[Category("ブラシ")]
		public Brush cBackground
		{
			get { return cTextBox.Background; }
			set { this._originBackground = cTextBox.Background = value; }
		}
		#endregion

        /// <summary>
        /// Foreground
        /// 前景色を表すブラシを取得または設定します
        /// </summary>
        #region Foreground

        [Category("ブラシ")]
        public Brush cForeground
        {
            get { return cTextBox.Foreground; }
            set { this._originForeground = cTextBox.Foreground = value; }
        }
        #endregion

        /// <summary>
		/// Background 
		/// コントロールの境界線を表すブラシを取得または設定します
		/// </summary>
		#region BorderBrush
		[Category("ブラシ")]
		public Brush cBorderBrush
		{
			get { return cTextBox.BorderBrush; }
			set { this._originBorderBrush = cTextBox.BorderBrush = value; }
		}
		#endregion

		/// <summary>
		/// Background 
		/// コントロールの境界線の幅を表すブラシを取得または設定します
		/// </summary>
		#region BorderThickness
		[Category("ブラシ")]
		public Thickness cBorderThickness
		{
			get { return cTextBox.BorderThickness; }
			set { this._originBorderThickness = cTextBox.BorderThickness = value; }
		}
		#endregion

		/// <summary>
		/// HorizontalAlignment 
		/// この要素が、Panel またはアイテム コントロールのような親要素内に構成されるときに適用される水平方向の配置特性を取得または設定します
		/// </summary>
		#region HorizontalAlignment

		[Category("デザイン")]
		public HorizontalAlignment cHorizontalAlignment
		{
			get { return cTextBox.HorizontalAlignment; }
			set { cTextBox.HorizontalAlignment = value; }
		}
		#endregion

		/// <summary>
        /// VerticalAlignment 
		/// この要素が、Panel またはアイテム コントロールのような親要素内に構成されるときに適用される水平方向の配置特性を取得または設定します
        /// </summary>
        #region VerticalAlignment
        [Category("デザイン")]
		public VerticalAlignment cVerticalAlignment
		{
			get { return cTextBox.VerticalAlignment; }
			set { cTextBox.VerticalAlignment = value; }
		}
		#endregion

     
  		/// <summary>
		/// HorizontalContentAlignment 
        /// コントロールのコンテンツの水平方向の配置を取得または設定します。 (Control から継承されます。) 
		/// </summary>
		#region HorizontalContentAlignment
		[Category("デザイン")]
        public HorizontalAlignment cHorizontalContentAlignment
		{
            get { return cTextBox.HorizontalContentAlignment; }
            set { cTextBox.HorizontalContentAlignment = value; }
		}
		#endregion

        #region VerticalContentAlignment
        [Category("デザイン")]
        public VerticalAlignment cVerticalContentAlignment
        {
            get { return cTextBox.VerticalContentAlignment; }
            set { cTextBox.VerticalContentAlignment = value; }
        }
        #endregion

		/// <summary>
		/// IsReadOnly 依存プロパティ
		/// テキスト編集コントロールを操作するユーザーに対して、コントロールが読み取り専用であるかどうかを示す値を取得または設定します
		/// </summary>
		#region
		[Category("動作")]
		public bool cIsReadOnly
		{
			get { return cTextBox.IsReadOnly; }
			set { cTextBox.IsReadOnly = value; this.cIsEnabled = !value; }
		}
		#endregion

        /// <summary>
        /// IsEnabled 依存プロパティ
        /// この要素がユーザー インターフェイス (UI) で有効かどうかを示す値を取得または設定します。 これは、依存関係プロパティです。
        /// </summary>
		#region
		[Category("動作")]
		public bool cIsEnabled
		{
			get { return cTextBox.IsEnabled; }
			set { cTextBox.IsEnabled = value; cTextBox.Foreground = new SolidColorBrush(System.Windows.Media.Colors.Black); }
		}
		#endregion

		/// <summary>
		/// MaxLength 依存プロパティ
		/// テキスト ボックスに手動で入力できる最大文字数を取得または設定します
		/// </summary>
		#region
		[Category("動作")]
		public Int32 cMaxLength
		{
			get { return cTextBox.MaxLength; }
			set { cTextBox.MaxLength = value; }
		}
		#endregion

		/// <summary>
		/// MinLines 依存プロパティ
		/// 表示行の最小数を取得または設定します。
		/// </summary>
		#region
		[Category("動作")]
		public Int32 cMinLines
		{
			get { return cTextBox.MinLines; }
			set { cTextBox.MinLines = value; }
		}
		#endregion

		/// <summary>
		/// SelectedText 依存プロパティ
		/// テキスト ボックス内の現在の選択範囲のコンテンツを取得または設定します。
		/// </summary>
		#region
		[Category("動作")]
		public string cSelectedText
		{
			get { return cTextBox.SelectedText; }
			set { cTextBox.SelectedText = value; }
		}
		#endregion

		/// <summary>
		/// SelectionStart 依存プロパティ
		/// 現在の選択範囲の先頭の文字インデックスを取得または設定します。
		/// </summary>
		#region
		[Category("動作")]
		public Int32 cSelectionStart
		{
			get { return cTextBox.SelectionStart; }
			set { cTextBox.SelectionStart = value; }
		}
		#endregion

		/// <summary>
		/// Text 依存プロパティ
		/// テキスト ボックスのテキスト コンテンツを取得または設定します。
		/// </summary>
		#region

		[Category("表示")]
		public string cText
		{
			get { return cTextBox.Text; }
			set { cTextBox.Text = value; NotifyPropertyChanged(); }
		}
		#endregion

		/// <summary>
		/// TextAlignment 依存プロパティ
		/// テキスト ボックスのテキスト コンテンツを取得または設定します。
		/// </summary>
		#region
		[Category("表示")]
		public TextAlignment cTextAlignment
		{
			get { return cTextBox.TextAlignment; }
			set { cTextBox.TextAlignment = value; }
		}
		#endregion

		/// <summary>
		/// TextWrapping 依存プロパティ
		/// テキスト ボックスのテキスト折り返し方法を取得または設定します。
		/// </summary>
		#region
		[Category("動作")]
		public TextWrapping cTextWrapping
		{
			get { return cTextBox.TextWrapping; }
			set { cTextBox.TextWrapping = value; }
		}

		#endregion

		/// <summary>
		/// ToolTip 依存プロパティ
		/// ユーザー インターフェイス (UI)のこの要素に対して表示されるツール ヒントのオブジェクトを取得または設定します。
		/// </summary>
		#region
		[Category("動作")]
		public object cToolTip
		{
			get { return cTextBox.ToolTip; }
            set { cTextBox.ToolTip = value; }
		}

		#endregion

        /// <summary>
        /// Visibility 依存プロパティ
        /// この要素の ユーザー インターフェイス (UI) 表現を取得または設定します。
        /// </summary>
        #region
        [Category("デザイン")]
        public Visibility cVisibility
        {
            get { return cTextBox.Visibility; }
            set { cTextBox.Visibility = value; }
        }

        #endregion


		/// <summary>
		/// Width プロパティ
		/// 要素の幅を取得または設定します。
		/// </summary>
		#region
		[Category("動作")]
		public double cWidth
		{
			get { return cTextBox.Width; }
			set { cTextBox.Width = value; }
		}

		#endregion


		#endregion



	}
}
