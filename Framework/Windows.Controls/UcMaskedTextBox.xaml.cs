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

namespace KyoeiSystem.Framework.Windows.Controls
{
	/// <summary>
	/// UMaskedTextBox.xaml の相互作用ロジック
	/// </summary>
	public partial class UcMaskedTextBox : FrameworkControl
	{

		private Validator.ValidationTypes _validationType;
		private Brush _originBackground;
		private Brush _originBorderBrush;
		private Thickness _originBorderThickness;
		private object _originTooltip = string.Empty;
		private string _validationStatus = string.Empty;
		private string _originalText = string.Empty;

		private string internalValidationStatus
		{
			get { return this._validationStatus; }
			set
			{
				this._validationStatus = value;
				if (value == string.Empty)
				{
					MaskedTextBox.BorderBrush = this._originBorderBrush;
					MaskedTextBox.BorderThickness = this._originBorderThickness;
					MaskedTextBox.ToolTip = _originTooltip;
				}
				else
				{
					MaskedTextBox.BorderBrush = new SolidColorBrush(System.Windows.Media.Colors.Red);
					MaskedTextBox.BorderThickness = new Thickness(1.7);
					MaskedTextBox.ToolTip = value;
				}
			}
		}

		private bool _isModified = false;

		private IMETypes imeType = IMETypes.Off;
		private List<string> _valueList = null;

		// TwinTextboxのコード側の場合のみ trueがセットされる
		public bool IsCodeTextKey = false;
		public bool IsCodeExist = true;

		public bool IsModified
		{
			get { return this._isModified; }
		}

		public new bool IsEnabled
		{
			get { return this.MaskedTextBox.IsEnabled; }
			set { this.MaskedTextBox.IsEnabled = value; }
		}

		public new bool Focusable
		{
			get { return this.MaskedTextBox.Focusable; }
			set { this.MaskedTextBox.Focusable = value; }
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
			typeof(UcMaskedTextBox),
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
			typeof(UcMaskedTextBox),
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

		private string _decimalFormat = string.Empty;
		public string DecimalFormat
		{
			get { return this._decimalFormat; }
			set { this._decimalFormat = value; this.MaskedTextBox.MaxLength = value.Length; }
		}

		/// <summary>
		/// TextBox(UserControl)
		/// </summary>
		public UcMaskedTextBox()
		{
			InitializeComponent();

			this._originBackground = this.MaskedTextBox.Background;
			this._originBorderBrush = this.MaskedTextBox.BorderBrush;
			this._originBorderThickness = this.MaskedTextBox.BorderThickness;
			this._originTooltip = this.MaskedTextBox.ToolTip;
		}

		//public static readonly DependencyProperty ExternalValidationProperty = DependencyProperty.Register(
		//	"ExternalValidation",
		//	typeof(string),
		//	typeof(UMaskedTextBox),
		//	new UIPropertyMetadata(string.Empty)
		//	);

		//private string externalValidation = string.Empty;
		//public string ExternalValidation
		//{
		//	get { return this.externalValidation; }
		//	set
		//	{
		//		this.externalValidation = value;
		//		internalValidationStatus = value;
		//	}
		//}

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
		public static readonly RoutedEvent cTextChangedEvent = EventManager.RegisterRoutedEvent("cTextChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UcMaskedTextBox));

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
		private void MaskedTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			RoutedEventArgs NewEventargs = new RoutedEventArgs(UcMaskedTextBox.cTextChangedEvent);
			RaiseEvent(NewEventargs);

			CheckValidation();
		}

		private void MaskedTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter | e.Key == Key.Tab | e.Key == Key.Up | e.Key == Key.Down)
			{
				this.Text = MaskedTextBox.Text;
			}
		}

		private void MaskedTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			this._originalText = MaskedTextBox.Text;
			IMEChange();
			MaskedTextBox.Background = new SolidColorBrush(System.Windows.Media.Colors.Orange);

			MaskedTextBox.SelectAll();
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
                    icmv = ImeConversionModeValues.Native | ImeConversionModeValues.FullShape | ImeConversionModeValues.Roman;
                    break;
                case IMETypes.Katakana:
                    im.ImeState = InputMethodState.On;
                    icmv = ImeConversionModeValues.Katakana | ImeConversionModeValues.FullShape | ImeConversionModeValues.Roman;
                    break;
            }
            im.ImeConversionMode = icmv;
        }


		/// <summary>
		/// 論理フォーカスを失ったときに発生します。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MaskedTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			MaskedTextBox.Background = _originBackground;
			this._isModified = (this._originalText == MaskedTextBox.Text) ? false : true;
		}

		#endregion

		/// <summary>
		/// Enter、Tab、矢印キーによるフォーカス移動
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MaskedTextBox_KeyDown(object sender, KeyEventArgs e)
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
				vector = new TraversalRequest(FocusNavigationDirection.Next);
				break;
			case Key.Up:
				vector = new TraversalRequest(FocusNavigationDirection.Previous);
				break;
			//左右の矢印キーではフィールドを移動しない
			//case Key.Right:
			//	if (MaskedTextBox.SelectionStart < MaskedTextBox.Text.Length)
			//	{
			//		return;
			//	}
			//	vector = new TraversalRequest(FocusNavigationDirection.Next);
			//	break;
			//case Key.Left:
			//	if (MaskedTextBox.SelectionLength > 0 || MaskedTextBox.SelectionStart > 0)
			//	{
			//		return;
			//	}
			//	vector = new TraversalRequest(FocusNavigationDirection.Previous);
			//	break;
			}
			if (vector != null)
			{
				FocusControl.SetFocusWithOrder(vector);
				UIElement element = Keyboard.FocusedElement as UIElement;
				if (element is Button || element is ComboBox)
				{
					e.Handled = true;
				}
			}
		}

		public void SetFocus()
		{
			Keyboard.Focus(this.MaskedTextBox);
		}

		public override bool CheckValidation()
		{
			bool result = true;
			string status = string.Empty;
			if (IsRequired)
			{
				if (string.IsNullOrWhiteSpace(this.MaskedTextBox.Text))
				{
					status = Validator.ValidationMessage.ErrEmpty;
				}
			}
			if (string.IsNullOrEmpty(status))
			{
				//長さチェック
				status = Validator.CheckLength(this.cMaxLength, this.MaskedTextBox.Text);
				if (string.IsNullOrWhiteSpace(status))
				{
					//validatorの動作
					status = Validator.Check(this.ValidationType, this.MaskedTextBox.Text, this.DecimalFormat, this.ValueList);
				}
			}
			if (string.IsNullOrEmpty(status) && IsCodeTextKey && !IsCodeExist && MasterCheckEnaled)
			{
				status = Validator.ValidationMessage.ErrNotFound;
			}

			internalValidationStatus = status;
			if (string.IsNullOrEmpty(status))
			{
				result = true;
			}
			else
			{
				result = false;
			}

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


		/// <summary>
		/// AcceptsReturn 依存プロパティ
		/// Enter キーが押されたとき、テキスト編集コントロールがどのように反応するかを示す値を取得または設定します
		/// </summary>
		#region AcceptsReturn
		[Category("動作")]
		public bool cAcceptsReturn
		{
			get { return MaskedTextBox.AcceptsReturn; }
			set { MaskedTextBox.AcceptsReturn = value; }
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
			get { return MaskedTextBox.AcceptsTab; }
			set { MaskedTextBox.AcceptsTab = value; }
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
			get { return MaskedTextBox.Background; }
			set { this._originBackground = MaskedTextBox.Background = value; }
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
			get { return MaskedTextBox.BorderBrush; }
			set { this._originBorderBrush = MaskedTextBox.BorderBrush = value; }
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
			get { return MaskedTextBox.BorderThickness; }
			set { this._originBorderThickness = MaskedTextBox.BorderThickness = value; }
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
			get { return MaskedTextBox.HorizontalAlignment; }
			set { MaskedTextBox.HorizontalAlignment = value; }
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
			get { return MaskedTextBox.VerticalAlignment; }
			set { MaskedTextBox.VerticalAlignment = value; }
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
            get { return MaskedTextBox.HorizontalContentAlignment; }
            set { MaskedTextBox.HorizontalContentAlignment = value; }
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
			get { return MaskedTextBox.IsReadOnly; }
			set { MaskedTextBox.IsReadOnly = value; this.cIsEnabled = !value; }
		}
		#endregion

		#region
		[Category("動作")]
		public bool cIsEnabled
		{
			get { return MaskedTextBox.IsEnabled; }
			set { MaskedTextBox.IsEnabled = value; MaskedTextBox.Foreground = new SolidColorBrush(System.Windows.Media.Colors.Black); }
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
			get { return MaskedTextBox.MaxLength; }
			set { MaskedTextBox.MaxLength = value; }
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
			get { return MaskedTextBox.MinLines; }
			set { MaskedTextBox.MinLines = value; }
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
			get { return MaskedTextBox.SelectedText; }
			set { MaskedTextBox.SelectedText = value; }
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
			get { return MaskedTextBox.SelectionStart; }
			set { MaskedTextBox.SelectionStart = value; }
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
			get { return MaskedTextBox.Text; }
			set { MaskedTextBox.Text = value; NotifyPropertyChanged(); }
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
			get { return MaskedTextBox.TextAlignment; }
			set { MaskedTextBox.TextAlignment = value; }
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
			get { return MaskedTextBox.TextWrapping; }
			set { MaskedTextBox.TextWrapping = value; }
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
			get { return MaskedTextBox.ToolTip; }
			set { MaskedTextBox.ToolTip = value; }
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
            get { return MaskedTextBox.Visibility; }
            set { MaskedTextBox.Visibility = value; }
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
			get { return MaskedTextBox.Width; }
			set { MaskedTextBox.Width = value; }
		}

		#endregion


		#endregion



	}
}
