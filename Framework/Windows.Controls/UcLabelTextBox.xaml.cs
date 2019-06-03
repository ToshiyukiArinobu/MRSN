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
using System.Media;
using System.Reflection;



namespace KyoeiSystem.Framework.Windows.Controls
{
	/// <summary>
	/// UcLabelTextBox.xaml の相互作用ロジック
	/// </summary>
	public partial class UcLabelTextBox : FrameworkControl
	{
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
			"Text",
			typeof(string),
			typeof(UcLabelTextBox),
			new UIPropertyMetadata(string.Empty)
		);

		[BindableAttribute(true)]
		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		//public static readonly DependencyProperty ValidationProperty = DependencyProperty.Register(
		//	"Validation",
		//	typeof(string),
		//	typeof(UcLabelTextBox),
		//	new FrameworkPropertyMetadata(new PropertyChangedCallback(UcLabelTextBox.OnValidationChanged))
		//	);

		//private static void OnValidationChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		//{
		//	if (obj.GetType() == typeof(UcLabelTextBox))
		//	{
		//		(obj as UcLabelTextBox).Validation = (string)args.NewValue;
		//	}
		//}

		//[BindableAttribute(true)]
		//public string Validation
		//{
		//	get { return this.cTextBox.Validation; }
		//	set
		//	{
		//		this.cTextBox.Validation = value;
		//		SetValue(ValidationProperty, value);
		//	}
		//}

		[Category("動作")]
		public Validator.ValidationTypes ValidationType
		{
			set { this.cTextBox.ValidationType = value; }
			get { return this.cTextBox.ValidationType; }
		}

		[Category("動作")]
		public string Mask
		{
			get { return this.cTextBox.Mask; }
			set
			{
				this.cTextBox.Mask = value;
			}
		}

		# region AutoSelectプロパティ
		[Category("動作")]
		public OnOff AutoSelect
		{
			get { return this.cTextBox.AutoSelect; }
			set
			{
				this.cTextBox.AutoSelect = value;
			}
		}
		#endregion

		//public string DecimalFormat
		//{
		//	get { return this.cTextBox.DecimalFormat; }
		//	set { this.cTextBox.DecimalFormat = value; }
		//}

		[Category("動作")]
		public string DataAccessName
		{
			get
			{
				return this.cTextBox.DataAccessName;
			}
			set
			{
				this.cTextBox.DataAccessName = value;
			}
		}

		[Category("動作")]
		public bool IsModified
		{
			set { this.cTextBox.IsModified = value; }
			get { return this.cTextBox.IsModified; }
		}

		[Category("動作")]
		public bool DefaultDateIsToday
		{
			get { return this.cTextBox.DefaultDateIsToday; }
			set { this.cTextBox.DefaultDateIsToday = value; }
		}

		/// <summary>
		/// Label+TextBox UserControl
		/// </summary>
		public UcLabelTextBox()
		{
			InitializeComponent();
			this.Text = string.Empty;
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
				this.cTextBox.IsRequired = value;
			}
		}

		public static readonly RoutedEvent cTextChangedEvent = EventManager.RegisterRoutedEvent("cTextChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UcLabelTextBox));

		public event RoutedEventHandler cTextChanged
		{
			add { AddHandler(cTextChangedEvent, value); }
			remove { RemoveHandler(cTextChangedEvent, value); }
		}

		private void Textbox_TextChanged(object sender, RoutedEventArgs e)
		{
			RoutedEventArgs NewEventargs = new RoutedEventArgs(UcLabelTextBox.cTextChangedEvent);
			RaiseEvent(NewEventargs);
		}


		public override bool CheckValidation()
		{
			return this.cTextBox.CheckValidation();
		}

		public override string GetValidationMessage()
		{
			return this.cTextBox.GetValidationMessage();
		}

		#region Label Property

		/// <summary>
		/// コントロールの背景を表すブラシを取得または設定します。
		/// </summary>
		#region Background
		[Category("ブラシ")]
		public Brush Label_Background
		{
			get	{ return this.cLabel.Background; }
			set { this.cLabel.Background = value; }
		}
		#endregion

		/// <summary>
		/// データ バインディングに参加すると要素のデータ コンテキストを取得または設定します
		/// </summary>
		#region DataContext
		[Category("動作")]
		public object Label_DataContext
		{
			get { return this.cLabel.DataContext; }
			set { this.cLabel.DataContext = value; }
		}
		#endregion

		/// <summary>
		/// ContentControl のコンテンツを取得または設定します。
		/// </summary>
		#region Context
		[Category("レイアウト")]
		public object Label_Context
		{
			get { return this.cLabel.LabelText; }
			set { this.cLabel.LabelText = value; }
		}
		#endregion


		/// <summary>
		/// この要素に関連付けられている CommandBinding のオブジェクトのコレクションを取得します。
		/// CommandBinding は、この要素に対するコマンドを有効にし、コマンド、イベント、およびこの要素に接続するハンドラー間の連結を宣言します。
		/// </summary>
		#region CommandBindings
		[Category("動作")]
		public CommandBindingCollection Label_CommandBindings
		{
			get { return this.cLabel.CommandBindings; }
			set { }
		}
		#endregion

		/// <summary>
		/// この DispatcherObject が関連付けられている Dispatcher を取得します
		/// </summary>
		#region Dispatcher
		[Category("動作")]
		public object Label_Dispatcher
		{
			get { return this.cLabel.Dispatcher; }
			set { }
		}
		#endregion

		/// <summary>
		/// 要素がフォーカスを得ることができるかどうかを示す値を取得または設定します。これは 依存関係プロパティです
		/// </summary>
		#region Focusable
		[Category("動作")]
		public bool Label_Focusable
		{
			get { return this.cLabel.Focusable; }
			set { this.cLabel.Focusable = value; }
		}
		#endregion

		/// <summary>
		/// コントロールのフォント ファミリを取得または設定します
		/// </summary>
		#region FontFamily
		[Category("動作")]
		public FontFamily Label_FontFamily
		{
			get { return this.cLabel.FontFamily; }
			set { this.cLabel.FontFamily = value; }
		}
		#endregion

		/// <summary>
		/// フォント サイズを取得または設定します
		/// </summary>
		#region FontSize
		[Category("レイアウト")]
		public double Label_FontSize
		{
			get { return this.cLabel.FontSize; }
			set { this.cLabel.FontSize = value; }
		}
		#endregion

		/// <summary>
		/// 画面上でフォントを縮小または拡大する度合いを取得または設定します。 
		/// </summary>
		#region FontStretch
		[Category("動作")]
		public FontStretch Label_FontStretch
		{
			get { return this.cLabel.FontStretch; }
			set { this.cLabel.FontStretch = value; }
		}
		#endregion

		/// <summary>
		/// フォント スタイルを取得または設定します。
		/// </summary>
		#region FontStyle
		[Category("動作")]
		public FontStyle Label_FontStyle
		{
			get { return this.cLabel.FontStyle; }
			set { this.cLabel.FontStyle = value; }
		}
		#endregion

		/// <summary>
		/// 画面上でフォントを縮小または拡大する度合いを取得または設定します。 
		/// </summary>
		#region FontWeight
		[Category("動作")]
		public FontWeight Label_FontWeight
		{
			get { return this.cLabel.FontWeight; }
			set { this.cLabel.FontWeight = value; }
		}
		#endregion

		/// <summary>
		/// 前景色を表すブラシを取得または設定します
		/// </summary>
		#region Foreground
		[Category("ブラシ")]
		public Brush Label_Foreground
		{
			get { return this.cLabel.Foreground; }
			set { this.cLabel.Foreground = value; }
		}
		#endregion

		/// <summary>
		/// ContentControl にコンテンツが含まれているかどうかを示す値を取得します。 
		/// </summary>
		#region HasContent
		[Category("動作")]
		public bool Label_HasContent
		{
			get { return this.cLabel.HasContent; }
			set { }
		}
		#endregion

		/// <summary>
		/// 要素の提案された高さを取得または設定します
		/// </summary>
		#region Height
		[Category("動作")]
		public double Label_Height
		{
			get { return this.cLabel.Height; }
			set { this.cLabel.Height = value; }
		}
		#endregion

		/// <summary>
		/// コントロールのコンテンツの水平方向の配置を取得または設定します
		/// </summary>
		#region HorizontalContentAlignment
		[Category("表示")]
		public HorizontalAlignment Label_HorizontalContentAlignment
		{
			get { return this.cLabel.HorizontalContentAlignment; }
			set { this.cLabel.HorizontalContentAlignment = value; }
		}
		#endregion

		/// <summary>
		/// この要素に論理フォーカスがあるかどうかを示す値を取得します。
		/// これは 依存関係プロパティです。
		/// </summary>
		#region IsFocused
		[Category("動作")]
		public bool Label_IsFocused
		{
			get { return this.cLabel.IsFocused; }
			set { }
		}
		#endregion

		/// <summary>
		/// コントロールがタブ ナビゲーションに含まれるかどうかを示す値を取得または設定します
		/// </summary>
		#region IsTabStop
		[Category("動作")]
		public bool Label_IsTabStop
		{
			get { return this.cLabel.IsTabStop; }
			set { this.cLabel.IsTabStop = value; }
		}
		#endregion

		/// <summary>
		/// この要素が ユーザー インターフェイス (UI)に表示されるかどうかを示す値を取得します。
		/// これは 依存関係プロパティです。
		/// </summary>
		#region IsVisible
		[Category("動作")]
		public bool Label_cIsVisible
		{
			get { return this.cLabel.IsVisible; }
			set { }
		}
		#endregion

		/// <summary>
		/// この要素が ユーザー インターフェイス (UI)に表示されるかどうかを示す値を取得します。
		/// これは 依存関係プロパティです。
		/// </summary>
		#region Visibility
		[Category("動作")]
		public Visibility Label_Visibility
		{
			get { return this.cLabel.Visibility; }
			set { this.cLabel.Visibility = value; }
		}
		#endregion

		/// <summary>
		/// この要素が ユーザー インターフェイス 
		/// (UI)に表示されるかどうかを示す値を取得します。
		/// これは 依存関係プロパティです。
		/// </summary>
		#region Margin
		[Category("レイアウト")]
		public Thickness Label_Margin
		{
			get { return this.cLabel.Margin; }
			set { this.cLabel.Margin = value; }
		}
		#endregion

		/// <summary>
		/// 要素の高さの最大値を取得または設定します
		/// </summary>
		#region MaxHeight
		[Category("レイアウト")]
		public double Label_MaxHeight
		{
			get { return this.cLabel.MaxHeight; }
			set { this.cLabel.MaxHeight = value; }
		}
		#endregion

		/// <summary>
		/// 要素の高さの最大値を取得または設定します
		/// </summary>
		#region MaxWidth
		[Category("レイアウト")]
		public double Label_MaxWidth
		{
			get { return this.cLabel.MaxWidth; }
			set { this.cLabel.MaxWidth = value; }
		}
		#endregion

		/// <summary>
		/// コントロール内のスペースを取得または設定します
		/// </summary>
		#region Padding
		[Category("動作")]
		public Thickness Label_Padding
		{
			get { return this.cLabel.Padding; }
			set { this.cLabel.Padding = value; }
		}
		#endregion

		/// <summary>
		/// この要素の logical parent の要素を取得します。
		/// </summary>
		#region Parent
		[Category("動作")]
		public DependencyObject Label_Parent
		{
			get { return this.cLabel.Parent; }
			set { }
		}
		#endregion

		/// <summary>
		/// ユーザー インターフェイス (UI)のこの要素に対して表示されるツール ヒントのオ
		/// ブジェクトを取得または設定します
		/// </summary>
		#region ToolTip
		[Category("動作")]
		public object Label_ToolTip
		{
			get { return this.cLabel.ToolTip; }
			set { this.cLabel.ToolTip = value; }
		}
		#endregion

		/// <summary>
		/// コントロールのコンテンツの垂直方向の配置を取得または設定します。
		/// </summary>
		#region VerticalContentAlignment
		[Category("レイアウト")]
		public VerticalAlignment Label_VerticalContentAlignment
		{
			get { return this.cLabel.VerticalContentAlignment; }
			set { this.cLabel.VerticalContentAlignment = value; }
		}
		#endregion

		/// <summary>
		/// 要素の幅を取得または設定します。 
		/// </summary>
		#region Width
		[Category("レイアウト")]
		public double Label_Width
		{
			get { return this.cLabel.Width; }
			set { this.cLabel.Width = value; }
		}
		#endregion

		#endregion

		#region TextBox Property

		[Category("動作")]
		public string MinValue
		{
			get { return cTextBox.MinValue; }
			set { cTextBox.MinValue = value; }
		}
		[Category("動作")]
		public string MaxValue
		{
			get { return cTextBox.MaxValue; }
			set { cTextBox.MaxValue = value; }
		}

		public new bool Focusable
		{
			get { return this.cTextBox.Focusable; }
			set { this.cTextBox.Focusable = value; }
		}

		public new void Focus()
		{
			this.cTextBox.Focus();
		}


		#region TEXT
		[Category("動作")]
		public string cText
		{
			get { return cTextBox.cText; }
			set { cTextBox.cText = value; }
		}
		#endregion

		/// <summary>
		/// AcceptsReturn 依存プロパティ
		/// Enter キーが押されたとき、テキスト編集コントロールがどのように反応するかを示す値を取得または設定します
		/// </summary>
		#region AcceptsReturn
		[Category("動作")]
		public bool cAcceptsReturn
		{
			get { return cTextBox.cAcceptsReturn; }
			set { cTextBox.cAcceptsReturn = value; }
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
			get { return cTextBox.cAcceptsTab; }
			set { cTextBox.cAcceptsTab = value; }
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
			get { return cTextBox.cBackground; }
			set { cTextBox.cBackground = value; }
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
			get { return cTextBox.cHorizontalAlignment; }
			set { cTextBox.cHorizontalAlignment = value; }
		}
		#endregion

		/// <summary>
		/// VerticalAlignment  依存プロパティ
		/// </summary>
		#region
		[Category("デザイン")]
		public VerticalAlignment cVerticalAlignment
		{
			get { return cTextBox.cVerticalAlignment; }
			set { cTextBox.cVerticalAlignment = value; }
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
			get { return cTextBox.cIsReadOnly; }
			set { cTextBox.cIsReadOnly = value; }
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
			get { return cTextBox.cMaxLength; }
			set { cTextBox.cMaxLength = value; }
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
			get { return cTextBox.cMinLines; }
			set { cTextBox.cMinLines = value; }
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
			get { return cTextBox.cSelectedText; }
			set { cTextBox.cSelectedText = value; }
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
			get { return cTextBox.cSelectionStart; }
			set { cTextBox.cSelectionStart = value; }
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
			get { return cTextBox.cTextAlignment; }
			set { cTextBox.cTextAlignment = value; }
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
			get { return cTextBox.cTextWrapping; }
			set { cTextBox.cTextWrapping = value; }
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
			get { return cTextBox.cToolTip; }
			set { cTextBox.cToolTip = value; }
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
			get { return cTextBox.cWidth; }
			set { cTextBox.cWidth = value; }
		}
		#endregion

        /// <summary>
        /// ValidationType
        /// </summary>
        #region
        [Category("動作")]
        public Validator.ValidationTypes cValidationType
        {
            get { return cTextBox.ValidationType; }
            set { cTextBox.ValidationType = value; }
        }
        #endregion

        /// <summary>
        /// IMEType
        /// </summary>
        #region
        [Category("動作")]
        public IMETypes ImeType
        {
            get { return cTextBox.ImeType; }
            set { cTextBox.ImeType = value; }
        }
        #endregion

        /// <summary>
        /// コントロールのコンテンツの水平方向の配置を取得または設定します
        /// </summary>
        #region HorizontalContentAlignment
        [Category("表示")]
        public HorizontalAlignment cHorizontalContentAlignment
        {
            get { return this.cTextBox.cHorizontalContentAlignment; }
            set { this.cTextBox.cHorizontalContentAlignment = value; }
        }
        #endregion

        /// <summary>
        /// コントロールのコンテンツの垂直方向の配置を取得または設定します。
        /// </summary>
        #region VerticalContentAlignment
        [Category("レイアウト")]
        public VerticalAlignment cVerticalContentAlignment
        {
            get { return this.cTextBox.cVerticalContentAlignment; }
            set { this.cTextBox.cVerticalContentAlignment = value; }
        }
        #endregion

        #region cBorderThickness
        /// <summary>
        /// テキストボックスの枠線の太さを取得または設定します。
        /// </summary>
        public Thickness cBorderThickness
        {
            get { return cTextBox.cBorderThickness; }
            set { this.cTextBox.cBorderThickness = value; }
        }
        #endregion

        #endregion

    }
}
