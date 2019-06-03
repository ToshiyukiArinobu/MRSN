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
using KyoeiSystem;

namespace KyoeiSystem.Framework.Windows.Controls
{
	/// <summary>
	/// UcLabelDatePicker.xaml の相互作用ロジック
	/// </summary>
	public partial class UcLabelDatePicker : FrameworkControl
	{
		public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
			"LabelText",
			typeof(object),
			typeof(UcLabelDatePicker),
			new UIPropertyMetadata(string.Empty)
		);

		[BindableAttribute(true)]
		public object LabelText
		{
			get { return (object)GetValue(LabelTextProperty); }
			set { SetValue(LabelTextProperty, value); }
		}

		public static readonly DependencyProperty SelectedDate1Property = DependencyProperty.Register(
				"SelectedDate1",
				typeof(object),
				typeof(UcLabelDatePicker),
				new FrameworkPropertyMetadata(new PropertyChangedCallback(UcLabelDatePicker.OnSelectedDate1Changed))
			);

		[BindableAttribute(true)]
		public object SelectedDate1
		{
			get { return GetValue(SelectedDate1Property); }
			set { SetValue(SelectedDate1Property, value); }
		}

		private static void OnSelectedDate1Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			if (obj.GetType() == typeof(UcLabelDatePicker))
			{
				(obj as UcLabelDatePicker).SelectedDate1 = args.NewValue;
			}
		}

		public static readonly DependencyProperty SelectedDate2Property = DependencyProperty.Register(
				"SelectedDate2",
				typeof(object),
				typeof(UcLabelDatePicker),
				new FrameworkPropertyMetadata(new PropertyChangedCallback(UcLabelDatePicker.OnSelectedDate2Changed))
			);

		[BindableAttribute(true)]
		public object SelectedDate2
		{
			get { return GetValue(SelectedDate2Property); }
			set { SetValue(SelectedDate2Property, value); }
		}

		private static void OnSelectedDate2Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			if (obj.GetType() == typeof(UcLabelDatePicker))
			{
				(obj as UcLabelDatePicker).SelectedDate2 = args.NewValue;
			}
		}

		/// <summary>
		/// 初期化
		/// </summary>
		public UcLabelDatePicker()
		{
			InitializeComponent();
		}

		/// <summary>
		/// DatePickerの初期状態
		/// </summary>
		private DatePickerStates pickerState = DatePickerStates.Single;

		/// <summary>
		/// DatePickerの状態
		/// </summary>
		public enum DatePickerStates {
 			/// <summary>
 			/// LableとDatePicker
 			/// </summary>
			Single,
			/// <summary>
			/// 範囲を指定する
			/// </summary>
			Two
		}

		/// <summary>
		/// ２つ目のDataPickerの表示の有無
		/// </summary>
		public DatePickerStates DatePickerState
		{
			set
			{
				pickerState = value;
				if (pickerState == DatePickerStates.Single)
				{
					this.DoubleGrid.Visibility = Visibility.Collapsed;
				}
				else
				{
					this.DoubleGrid.Visibility = Visibility.Visible;
				}
			}
			get { return this.pickerState;}
		}

		/// <summary>
		/// バリデータチェックフラグ
		/// </summary>
		public bool ValidatorError = false;

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
				this.FirstDatePicker.IsRequired = value;
				if (this.SecondDatePicker.Visibility == System.Windows.Visibility.Visible)
				{
					this.SecondDatePicker.IsRequired = value;
				}
			}
		}


		public new void Focus()
		{
			this.FirstDatePicker.Focus();
		}

		public void Focus2()
		{
			this.SecondDatePicker.Focus();
		}

		/// <summary>
		/// DataPickerロストフォーカス
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FirstDatePicker_LostFocus(object sender, RoutedEventArgs e)
		{
			if (pickerState == DatePickerStates.Two & this.FirstDatePicker.cSelectedDate != null & this.SecondDatePicker.cSelectedDate != null)
			{
				DateTime? StartDay = this.FirstDatePicker.cSelectedDate;
				DateTime? EndDay = this.SecondDatePicker.cSelectedDate;

				if (StartDay > EndDay)
				{
					this.FirstDatePicker.ValidatorError = true;
					this.SecondDatePicker.ValidatorError = true;

					MessageBox.Show("開始日と終了日の関係が正しくありません。");
				}
				else
				{
					this.FirstDatePicker.ValidatorError = false;
					this.SecondDatePicker.ValidatorError = false;
				}
			}
		}

		#region Label Property

		#region Background
		/// <summary>
		/// コントロールの背景を表すブラシを取得または設定します。
		/// </summary>
		[Category("ブラシ")]
		public Brush Label_Background
		{
			get { return this.cLabel.cBackground; }
			set { this.cLabel.cBackground = value; }
		}
		#endregion

		#region DataContext
		/// <summary>
		/// データ バインディングに参加すると要素のデータ コンテキストを取得または設定します
		/// </summary>
		[Category("動作")]
		public object Label_DataContext
		{
			get { return this.cLabel.cDataContext; }
			set { this.cLabel.cDataContext = value; }
		}
		#endregion

		#region CommandBindings
		/// <summary>
		/// この要素に関連付けられている CommandBinding のオブジェクトのコレクションを取得します。
		/// CommandBinding は、この要素に対するコマンドを有効にし、コマンド、イベント、およびこの要素に接続するハンドラー間の連結を宣言します。
		/// </summary>
		[Category("動作")]
		public CommandBindingCollection Label_CommandBindings
		{
			get { return this.cLabel.cCommandBindings; }
			set { }
		}
		#endregion

		#region CommandBindings
		/// <summary>
		/// ContentControl のコンテンツを取得または設定します。
		/// </summary>
		[Category("レイアウト")]
		public object Label_Content
		{
			get { return this.cLabel.cContent; }
			set { this.cLabel.cContent = value; }
		}
		#endregion

		#region Dispatcher
		/// <summary>
		/// この DispatcherObject が関連付けられている Dispatcher を取得します
		/// </summary>
		[Category("動作")]
		public object Label_Dispatcher
		{
			get { return this.cLabel.cDispatcher; }
			set { }
		}
		#endregion

		#region Focusable
		/// <summary>
		/// 要素がフォーカスを得ることができるかどうかを示す値を取得または設定します。これは 依存関係プロパティです
		/// </summary>
		[Category("動作")]
		public bool Label_Focusable
		{
			get { return this.cLabel.cFocusable; }
			set { this.cLabel.cFocusable = value; }
		}
		#endregion

		#region FontFamily
		/// <summary>
		/// コントロールのフォント ファミリを取得または設定します
		/// </summary>
		[Category("動作")]
		public FontFamily Label_FontFamily
		{
			get { return this.cLabel.cFontFamily; }
			set { this.cLabel.cFontFamily = value; }
		}
		#endregion

		#region FontSize
		/// <summary>
		/// フォント サイズを取得または設定します
		/// </summary>
		[Category("レイアウト")]
		public double Label_FontSize
		{
			get { return this.cLabel.cFontSize; }
			set { this.cLabel.cFontSize = value; }
		}
		#endregion

		#region FontStretch
		/// <summary>
		/// 画面上でフォントを縮小または拡大する度合いを取得または設定します。 
		/// </summary>
		[Category("動作")]
		public FontStretch Label_FontStretch
		{
			get { return this.cLabel.cFontStretch; }
			set { this.cLabel.cFontStretch = value; }
		}
		#endregion

		#region FontStyle
		/// <summary>
		/// フォント スタイルを取得または設定します。
		/// </summary>
		[Category("動作")]
		public FontStyle Label_FontStyle
		{
			get { return this.cLabel.cFontStyle; }
			set { this.cLabel.cFontStyle = value; }
		}
		#endregion

		#region FontWeight
		/// <summary>
		/// 画面上でフォントを縮小または拡大する度合いを取得または設定します。 
		/// </summary>
		[Category("動作")]
		public FontWeight Label_FontWeight
		{
			get { return this.cLabel.cFontWeight; }
			set { this.cLabel.cFontWeight = value; }
		}
		#endregion

		#region Foreground
		/// <summary>
		/// 前景色を表すブラシを取得または設定します
		/// </summary>
		[Category("ブラシ")]
		public Brush Label_Foreground
		{
			get { return this.cLabel.cForeground; }
			set { this.cLabel.cForeground = value; }
		}
		#endregion

		#region HasContent
		/// <summary>
		/// ContentControl にコンテンツが含まれているかどうかを示す値を取得します。 
		/// </summary>
		[Category("動作")]
		public bool Label_HasContent
		{
			get { return this.cLabel.cHasContent; }
			set { }
		}
		#endregion

		#region Height
		/// <summary>
		/// 要素の提案された高さを取得または設定します
		/// </summary>
		[Category("動作")]
		public double Label_Height
		{
			get { return this.cLabel.cHeight; }
			set { this.cLabel.cHeight = value; }
		}
		#endregion

		#region HorizontalContentAlignment
		/// <summary>
		/// コントロールのコンテンツの水平方向の配置を取得または設定します
		/// </summary>
		[Category("表示")]
		public HorizontalAlignment Label_HorizontalContentAlignment
		{
			get { return this.cLabel.cHorizontalContentAlignment; }
			set { this.cLabel.cHorizontalContentAlignment = value; }
		}
		#endregion

		#region IsFocused
		/// <summary>
		/// この要素に論理フォーカスがあるかどうかを示す値を取得します。
		/// これは 依存関係プロパティです。
		/// 
		/// </summary>
		[Category("動作")]
		public bool Label_IsFocused
		{
			get { return this.cLabel.cIsFocused; }
			set { }
		}
		#endregion

		#region IsTabStop
		/// <summary>
		/// コントロールがタブ ナビゲーションに含まれるかどうかを示す値を取得または設定します
		/// </summary>
		[Category("動作")]
		public bool Label_IsTabStop
		{
			get { return this.cLabel.cIsTabStop; }
			set { this.cLabel.cIsTabStop = value; }
		}
		#endregion

		#region IsVisible
		/// <summary>
		/// この要素が ユーザー インターフェイス (UI)に表示されるかどうかを示す値を取得します。
		/// これは 依存関係プロパティです。
		/// </summary>
		[Category("動作")]
		public bool Label_IsVisible
		{
			get { return this.cLabel.cIsVisible; }
			set { }
		}
		#endregion

		#region Margin
		/// <summary>
		/// この要素が ユーザー インターフェイス 
		/// (UI)に表示されるかどうかを示す値を取得します。
		/// これは 依存関係プロパティです。
		/// </summary>
		[Category("レイアウト")]
		public Thickness Label_Margin
		{
			get { return this.cLabel.cMargin; }
			set { this.cLabel.cMargin = value; }
		}
		#endregion

		#region MaxHeight
		/// <summary>
		/// 要素の高さの最大値を取得または設定します
		/// </summary>
		[Category("レイアウト")]
		public double Label_MaxHeight
		{
			get { return this.cLabel.cMaxHeight; }
			set { this.cLabel.cMaxHeight = value; }
		}
		#endregion

		#region MaxWidth
		/// <summary>
		/// 要素の高さの最大値を取得または設定します
		/// </summary>
		[Category("レイアウト")]
		public double Label_MaxWidth
		{
			get { return this.cLabel.cMaxWidth; }
			set { this.cLabel.cMaxWidth = value; }
		}
		#endregion

		#region MinHeight
		/// <summary>
		/// 要素の高さの最大値を取得または設定します
		/// </summary>
		[Category("レイアウト")]
		public double Label_MinHeight
		{
			get { return this.cLabel.cMinHeight; }
			set { this.cLabel.cMinHeight = value; }
		}
		#endregion

		#region MinWidth
		/// <summary>
		/// 要素の高さの最小値を取得または設定します 
		/// </summary>
		[Category("レイアウト")]
		public double Label_MinWidth
		{
			get { return this.cLabel.cMinWidth; }
			set { this.cLabel.cMinWidth = value; }
		}
		#endregion

		#region Name
		/// <summary>
		/// 要素の ID の名前を取得または設定します。名前は XAML のプロセッサで処理中に作成されると分離コードを、
		/// イベント ハンドラー コードなどのマークアップ要素を参照できるように参照を提供します。
		/// </summary>
		[Category("共通")]
		public string Label_Name
		{
			get { return this.cLabel.cName; }
			set { this.cLabel.cName = value; }
		}
		#endregion

		#region Padding
		/// <summary>
		/// コントロール内のスペースを取得または設定します
		/// </summary>
		[Category("動作")]
		public Thickness Label_Padding
		{
			get { return this.cLabel.cPadding; }
			set { this.cLabel.cPadding = value; }
		}
		#endregion

		#region Parent
		/// <summary>
		/// この要素の logical parent の要素を取得します。
		/// </summary>
		[Category("動作")]
		public DependencyObject Label_Parent
		{
			get { return this.cLabel.cParent; }
			set { }
		}
		#endregion

		#region TabIndex
		/// <summary>
		/// ユーザー インターフェイス (UI)のこの要素に対して表示される
		/// ツール ヒントのオブジェクトを取得または設定します
		/// </summary>
		[Category("動作")]
		public int Label_TabIndex
		{
			get { return this.cLabel.cTabIndex; }
			set { this.cLabel.cTabIndex = value; }
		}
		#endregion

		#region ToolTip
		/// <summary>
		/// ユーザー インターフェイス (UI)のこの要素に対して表示されるツール ヒントのオ
		/// ブジェクトを取得または設定します
		/// </summary>
		[Category("動作")]
		public object Label_ToolTip
		{
			get { return this.cLabel.cToolTip; }
			set { this.cLabel.cToolTip = value; }
		}
		#endregion

		#region VerticalContentAlignment
		/// <summary>
		/// コントロールのコンテンツの垂直方向の配置を取得または設定します。
		/// </summary>
		[Category("レイアウト")]
		public VerticalAlignment Label_VerticalContentAlignment
		{
			get { return this.cLabel.cVerticalContentAlignment; }
			set { this.cLabel.cVerticalContentAlignment = value; }
		}
		#endregion

		#region Visibility
		/// <summary>
		/// この要素の ユーザー インターフェイス (UI) 表現を取得または設定します。
		/// </summary>
		[Category("レイアウト")]
		public Visibility Label_Visibility
		{
			get { return this.cLabel.cVisibility; }
			set { this.cLabel.cVisibility = value; }
		}
		#endregion

		#region Width
		/// <summary>
		/// 要素の幅を取得または設定します。 
		/// </summary>
		[Category("レイアウト")]
		public double Label_Width
		{
			get { return this.cLabel.cWidth; }
			set { this.cLabel.cWidth = value; }
		}
		#endregion

		#endregion

        #region DatePicker Property

        #region Background
        /// <summary>
        /// コントロールの背景を表すブラシを取得または設定します。 (Control から継承されます。)
        /// </summary>
        [Category("レイアウト")]
        public Brush FirstDate_Background
        {
            set { FirstDatePicker.cBackground = value; }
            get { return FirstDatePicker.cBackground; }
        }
        #endregion

        #region FirstDate_ommandBindings
        /// <summary>
        /// この要素に関連付けられている FirstDate_ommandBinding のオブジェクトのコレクションを取得します。 
        /// </summary>
        [Category("動作")]
        public CommandBindingCollection FirstDate_CommandBindings
        {
            set { }
            get { return FirstDatePicker.cCommandBindings; }
        }
        #endregion

        #region DataContext
        /// <summary>
        /// データ バインディングに参加すると要素のデータ コンテキストを取得または設定します。 
        /// </summary>
        [Category("動作")]
        public object FirstDate_DataContext
        {
            set { FirstDatePicker.cDataContext = value; }
            get { return FirstDatePicker.cDataContext; }
        }
        #endregion

        #region Dispatcher
        /// <summary>
        /// この DispatcherObject が関連付けられている Dispatcher を取得します。 
        /// </summary>
        [Category("動作")]
        public object FirstDate_Dispatcher
        {
            set { }
            get { return FirstDatePicker.cDispatcher; }
        }
        #endregion

        #region Effect
        /// <summary>
        /// ビットマップ効果を UIElementに適用する取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public System.Windows.Media.Effects.Effect FirstDate_Effect
        {
            set { FirstDatePicker.cEffect = value; }
            get { return FirstDatePicker.cEffect; }
        }
        #endregion

        #region Focusable
        /// <summary>
        /// 要素がフォーカスを得ることができるかどうかを示す値を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public bool FirstDate_Focusable
        {
            set { FirstDatePicker.cFocusable = value; }
            get { return FirstDatePicker.cFocusable; }
        }
        #endregion

        #region FontFamily
        /// <summary>
        /// コントロールのフォント ファミリを取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public FontFamily FirstDate_FontFamily
        {
            set { FirstDatePicker.cFontFamily = value; }
            get { return FirstDatePicker.cFontFamily; }
        }
        #endregion

        #region FontSize
        /// <summary>
        /// コントロールのフォント ファミリを取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public double FirstDate_FontSize
        {
            set { FirstDatePicker.cFontSize = value; }
            get { return FirstDatePicker.cFontSize; }
        }
        #endregion

        #region FontStretch
        /// <summary>
        /// 画面上でフォントを縮小または拡大する度合いを取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public FontStretch FirstDate_FontStretch
        {
            set { FirstDatePicker.cFontStretch = value; }
            get { return FirstDatePicker.cFontStretch; }
        }
        #endregion

        #region FontStyle
        /// <summary>
        /// フォント スタイルを取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public FontStyle FirstDate_FontStyle
        {
            set { FirstDatePicker.cFontStyle = value; }
            get { return FirstDatePicker.cFontStyle; }
        }
        #endregion

        #region FontWeight
        /// <summary>
        ///	指定したフォントの太さを取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public FontWeight FirstDate_FontWeight
        {
            set { FirstDatePicker.cFontWeight = value; }
            get { return FirstDatePicker.cFontWeight; }
        }
        #endregion

        #region Foreground
        /// <summary>
        /// 要素の提案された高さを取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public Brush FirstDate_Foreground
        {
            set { FirstDatePicker.cForeground = value; }
            get { return FirstDatePicker.cForeground; }
        }
        #endregion

        #region Height
        /// <summary>
        /// 要素の提案された高さを取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public double FirstDate_Height
        {
            set { FirstDatePicker.cHeight = value; }
            get { return FirstDatePicker.cHeight; }
        }
        #endregion

        #region HorizontalAlignment
        /// <summary>
        /// この要素が、Panel またはアイテム コントロールのような親要素内に構成されるときに適用される水平方向の配置特性を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public HorizontalAlignment FirstDate_HorizontalAlignment
        {
            set { FirstDatePicker.cHorizontalAlignment = value; }
            get { return FirstDatePicker.cHorizontalAlignment; }
        }
        #endregion

        #region IsEnabled
        /// <summary>
        /// この要素が ユーザー インターフェイス (UI)で有効になっているかどうかを示す値を取得または設定します
        /// </summary>
        [Category("デザイン")]
        public bool FirstDate_IsEnabled
        {
            set { FirstDatePicker.cIsEnabled = value; }
            get { return FirstDatePicker.cIsEnabled; }
        }
        #endregion

        #region IsFocused
        /// <summary>
        /// この要素に論理フォーカスがあるかどうかを示す値を取得します。これは 依存関係プロパティです。
        /// </summary>
        [Category("デザイン")]
        public bool FirstDate_IsFocused
        {
            set { }
            get { return FirstDatePicker.cIsFocused; }
        }
        #endregion

        #region Margin
        /// <summary>
        /// 要素の前辺を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public Thickness FirstDate_Margin
        {
            set { FirstDatePicker.cMargin = value; }
            get { return FirstDatePicker.cMargin; }
        }
        #endregion

        #region MaxHeight
        /// <summary>
        /// 要素の高さの最大値を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public double FirstDate_MaxHeight
        {
            set { FirstDatePicker.cMaxHeight = value; }
            get { return FirstDatePicker.cMaxHeight; }
        }
        #endregion

        #region MaxWidth
        /// <summary>
        /// 要素の幅の最大値を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public double FirstDate_MaxWidth
        {
            set { FirstDatePicker.cMaxWidth = value; }
            get { return FirstDatePicker.cMaxWidth; }
        }
        #endregion

        #region MinHeight
        /// <summary>
        /// 要素の高さの最小値を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public double FirstDate_MinHeight
        {
            set { FirstDatePicker.cMinHeight = value; }
            get { return FirstDatePicker.cMinHeight; }
        }
        #endregion

        #region MinWidth
        /// <summary>
        /// 要素の幅の最小値を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public double FirstDate_MinWidth
        {
            set { FirstDatePicker.cMinWidth = value; }
            get { return FirstDatePicker.cMinWidth; }
        }
        #endregion

        #region Name
        /// <summary>
        /// 要素の ID の名前を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public string FirstDate_Name
        {
            set { FirstDatePicker.cName = value; }
            get { return FirstDatePicker.cName; }
        }
        #endregion

        #region Padding
        /// <summary>
        /// コントロール内のスペースを取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public Thickness FirstDate_Padding
        {
            set { FirstDatePicker.cPadding = value; }
            get { return FirstDatePicker.cPadding; }
        }
        #endregion

        #region Parent
        /// <summary>
        /// この要素の logical parent の要素を取得します。
        /// </summary>
        [Category("デザイン")]
        public DependencyObject FirstDate_Parent
        {
            set { }
            get { return FirstDatePicker.cParent; }
        }
        #endregion

        #region SelectedDate
        /// <summary>
        /// 現在選択されている日付を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public DateTime? FirstDate_SelectedDate
        {
            set { FirstDatePicker.cSelectedDate = value; }
            get { return FirstDatePicker.cSelectedDate; }
        }
        #endregion

        #region SelectedDateFormat
        /// <summary>
        /// 選択した日付を表示するために使用される形式を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public DatePickerFormat FirstDate_SelectedDateFormat
        {
            set { FirstDatePicker.cSelectedDateFormat = value; }
            get { return FirstDatePicker.cSelectedDateFormat; }
        }
        #endregion

        #region TabIndex
        /// <summary>
        /// ユーザーが Tab キーを使用してコントロール間を移動するときに、要素がフォーカスを受け取る順序を決定する値を取得または設定します
        /// </summary>
        [Category("デザイン")]
        public int FirstDate_TabIndex
        {
            set { FirstDatePicker.cTabIndex = value; }
            get { return FirstDatePicker.cTabIndex; }
        }
        #endregion

        #region Text
        /// <summary>
        /// DatePicker によって表示されるテキストを取得するか、選択した日付を設定します。
        /// </summary>
        [Category("デザイン")]
        public string FirstDate_Text
        {
            set { FirstDatePicker.cText = value; }
            get { return FirstDatePicker.cText; }
        }
        #endregion

        #region ToolTip
        /// <summary>
        /// ユーザー インターフェイス (UI)のこの要素に対して表示されるツール ヒントのオブジェクトを取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public object FirstDate_ToolTip
        {
            set { FirstDatePicker.cToolTip = value; }
            get { return FirstDatePicker.cToolTip; }
        }
        #endregion

        #region Width
        /// <summary>
        /// 要素の幅を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public double FirstDate_Width
        {
            set { FirstDatePicker.cWidth = value; }
            get { return FirstDatePicker.cWidth; }
        }
        #endregion

        #endregion

        #region DatePicker Property

        #region Background
        /// <summary>
        /// コントロールの背景を表すブラシを取得または設定します。 (Control から継承されます。)
        /// </summary>
        [Category("レイアウト")]
        public Brush SecondDate_Background
        {
            set { SecondDatePicker.cBackground = value; }
            get { return SecondDatePicker.cBackground; }
        }
        #endregion

        #region SecondDate_ommandBindings
        /// <summary>
        /// この要素に関連付けられている SecondDate_ommandBinding のオブジェクトのコレクションを取得します。 
        /// </summary>
        [Category("動作")]
        public CommandBindingCollection SecondDate_CommandBindings
        {
            set { }
            get { return SecondDatePicker.cCommandBindings; }
        }
        #endregion

        #region DataContext
        /// <summary>
        /// データ バインディングに参加すると要素のデータ コンテキストを取得または設定します。 
        /// </summary>
        [Category("動作")]
        public object SecondDate_DataContext
        {
            set { SecondDatePicker.cDataContext = value; }
            get { return SecondDatePicker.cDataContext; }
        }
        #endregion

        #region Dispatcher
        /// <summary>
        /// この DispatcherObject が関連付けられている Dispatcher を取得します。 
        /// </summary>
        [Category("動作")]
        public object SecondDate_Dispatcher
        {
            set { }
            get { return SecondDatePicker.cDispatcher; }
        }
        #endregion

        #region Effect
        /// <summary>
        /// ビットマップ効果を UIElementに適用する取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public System.Windows.Media.Effects.Effect SecondDate_Effect
        {
            set { SecondDatePicker.cEffect = value; }
            get { return SecondDatePicker.cEffect; }
        }
        #endregion

        #region Focusable
        /// <summary>
        /// 要素がフォーカスを得ることができるかどうかを示す値を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public bool SecondDate_Focusable
        {
            set { SecondDatePicker.cFocusable = value; }
            get { return SecondDatePicker.cFocusable; }
        }
        #endregion

        #region FontFamily
        /// <summary>
        /// コントロールのフォント ファミリを取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public FontFamily SecondDate_FontFamily
        {
            set { SecondDatePicker.cFontFamily = value; }
            get { return SecondDatePicker.cFontFamily; }
        }
        #endregion

        #region FontSize
        /// <summary>
        /// コントロールのフォント ファミリを取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public double SecondDate_FontSize
        {
            set { SecondDatePicker.cFontSize = value; }
            get { return SecondDatePicker.cFontSize; }
        }
        #endregion

        #region FontStretch
        /// <summary>
        /// 画面上でフォントを縮小または拡大する度合いを取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public FontStretch SecondDate_FontStretch
        {
            set { SecondDatePicker.cFontStretch = value; }
            get { return SecondDatePicker.cFontStretch; }
        }
        #endregion

        #region FontStyle
        /// <summary>
        /// フォント スタイルを取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public FontStyle SecondDate_FontStyle
        {
            set { SecondDatePicker.cFontStyle = value; }
            get { return SecondDatePicker.cFontStyle; }
        }
        #endregion

        #region FontWeight
        /// <summary>
        ///	指定したフォントの太さを取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public FontWeight SecondDate_FontWeight
        {
            set { SecondDatePicker.cFontWeight = value; }
            get { return SecondDatePicker.cFontWeight; }
        }
        #endregion

        #region Foreground
        /// <summary>
        /// 要素の提案された高さを取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public Brush SecondDate_Foreground
        {
            set { SecondDatePicker.cForeground = value; }
            get { return SecondDatePicker.cForeground; }
        }
        #endregion

        #region Height
        /// <summary>
        /// 要素の提案された高さを取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public double SecondDate_Height
        {
            set { SecondDatePicker.cHeight = value; }
            get { return SecondDatePicker.cHeight; }
        }
        #endregion

        #region HorizontalAlignment
        /// <summary>
        /// この要素が、Panel またはアイテム コントロールのような親要素内に構成されるときに適用される水平方向の配置特性を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public HorizontalAlignment SecondDate_HorizontalAlignment
        {
            set { SecondDatePicker.cHorizontalAlignment = value; }
            get { return SecondDatePicker.cHorizontalAlignment; }
        }
        #endregion

        #region IsEnabled
        /// <summary>
        /// この要素が ユーザー インターフェイス (UI)で有効になっているかどうかを示す値を取得または設定します
        /// </summary>
        [Category("デザイン")]
        public bool SecondDate_IsEnabled
        {
            set { SecondDatePicker.cIsEnabled = value; }
            get { return SecondDatePicker.cIsEnabled; }
        }
        #endregion

        #region IsFocused
        /// <summary>
        /// この要素に論理フォーカスがあるかどうかを示す値を取得します。これは 依存関係プロパティです。
        /// </summary>
        [Category("デザイン")]
        public bool SecondDate_IsFocused
        {
            set { }
            get { return SecondDatePicker.cIsFocused; }
        }
        #endregion

        #region Margin
        /// <summary>
        /// 要素の前辺を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public Thickness SecondDate_Margin
        {
            set { SecondDatePicker.cMargin = value; }
            get { return SecondDatePicker.cMargin; }
        }
        #endregion

        #region MaxHeight
        /// <summary>
        /// 要素の高さの最大値を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public double SecondDate_MaxHeight
        {
            set { SecondDatePicker.cMaxHeight = value; }
            get { return SecondDatePicker.cMaxHeight; }
        }
        #endregion

        #region MaxWidth
        /// <summary>
        /// 要素の幅の最大値を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public double SecondDate_MaxWidth
        {
            set { SecondDatePicker.cMaxWidth = value; }
            get { return SecondDatePicker.cMaxWidth; }
        }
        #endregion

        #region MinHeight
        /// <summary>
        /// 要素の高さの最小値を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public double SecondDate_MinHeight
        {
            set { SecondDatePicker.cMinHeight = value; }
            get { return SecondDatePicker.cMinHeight; }
        }
        #endregion

        #region MinWidth
        /// <summary>
        /// 要素の幅の最小値を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public double SecondDate_MinWidth
        {
            set { SecondDatePicker.cMinWidth = value; }
            get { return SecondDatePicker.cMinWidth; }
        }
        #endregion

        #region Name
        /// <summary>
        /// 要素の ID の名前を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public string SecondDate_Name
        {
            set { SecondDatePicker.cName = value; }
            get { return SecondDatePicker.cName; }
        }
        #endregion

        #region Padding
        /// <summary>
        /// コントロール内のスペースを取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public Thickness SecondDate_Padding
        {
            set { SecondDatePicker.cPadding = value; }
            get { return SecondDatePicker.cPadding; }
        }
        #endregion

        #region Parent
        /// <summary>
        /// この要素の logical parent の要素を取得します。
        /// </summary>
        [Category("デザイン")]
        public DependencyObject SecondDate_Parent
        {
            set { }
            get { return SecondDatePicker.cParent; }
        }
        #endregion

        #region SelectedDate
        /// <summary>
        /// 現在選択されている日付を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public DateTime? SecondDate_SelectedDate
        {
            set { SecondDatePicker.cSelectedDate = value; }
            get { return SecondDatePicker.cSelectedDate; }
        }
        #endregion

        #region SelectedDateFormat
        /// <summary>
        /// 選択した日付を表示するために使用される形式を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public DatePickerFormat SecondDate_SelectedDateFormat
        {
            set { SecondDatePicker.cSelectedDateFormat = value; }
            get { return SecondDatePicker.cSelectedDateFormat; }
        }
        #endregion

        #region TabIndex
        /// <summary>
        /// ユーザーが Tab キーを使用してコントロール間を移動するときに、要素がフォーカスを受け取る順序を決定する値を取得または設定します
        /// </summary>
        [Category("デザイン")]
        public int SecondDate_TabIndex
        {
            set { SecondDatePicker.cTabIndex = value; }
            get { return SecondDatePicker.cTabIndex; }
        }
        #endregion

        #region Text
        /// <summary>
        /// DatePicker によって表示されるテキストを取得するか、選択した日付を設定します。
        /// </summary>
        [Category("デザイン")]
        public string SecondDate_Text
        {
            set { SecondDatePicker.cText = value; }
            get { return SecondDatePicker.cText; }
        }
        #endregion

        #region ToolTip
        /// <summary>
        /// ユーザー インターフェイス (UI)のこの要素に対して表示されるツール ヒントのオブジェクトを取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public object SecondDate_ToolTip
        {
            set { SecondDatePicker.cToolTip = value; }
            get { return SecondDatePicker.cToolTip; }
        }
        #endregion

        #region Width
        /// <summary>
        /// 要素の幅を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public double SecondDate_Width
        {
            set { SecondDatePicker.cWidth = value; }
            get { return SecondDatePicker.cWidth; }
        }
        #endregion

        #endregion


	}
}
