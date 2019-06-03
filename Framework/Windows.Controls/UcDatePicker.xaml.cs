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
	public partial class UcDatePicker : FrameworkControl
	{
		public static readonly DependencyProperty SelectedDateProperty = DependencyProperty.Register(
				"SelectedDate",
				typeof(object),
				typeof(UcDatePicker),
				new FrameworkPropertyMetadata(
						new PropertyChangedCallback(UcDatePicker.OnSelectedDateChanged)
				)
		);

		[BindableAttribute(true)]
		public object SelectedDate
		{
			get { return GetValue(SelectedDateProperty); }
			set
			{
				SetValue(SelectedDateProperty, value);
			}
		}

		private static void OnSelectedDateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			if (obj.GetType() == typeof(UcDatePicker))
			{
				(obj as UcDatePicker).SelectedDate = args.NewValue;
			}
		}

		private DateTime? BeforeSelectedDate;

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
			}
		}

		private Brush _originBackground;
		private Brush _originBorderBrush;
		private Thickness _originBorderThickness;
		private object _originTooltip = string.Empty;
		private string _validationStatus = string.Empty;
		private string internalValidationStatus
		{
			get { return this._validationStatus; }
			set
			{
				this._validationStatus = value;
				if (value == string.Empty)
				{
					this.cDatePicker.BorderBrush = this._originBorderBrush;
					this.cDatePicker.BorderThickness = this._originBorderThickness;
					this.cDatePicker.ToolTip = _originTooltip;
				}
				else
				{
					this.cDatePicker.BorderBrush = new SolidColorBrush(System.Windows.Media.Colors.Red);
					this.cDatePicker.BorderThickness = new Thickness(2);
					this.cDatePicker.ToolTip = value;
				}
			}
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
		/// 初期化
		/// </summary>
		public UcDatePicker()
		{
			InitializeComponent();
			this.SelectedDate = DateTime.Today;
			this._originBackground = this.cDatePicker.Background;
			this._originBorderBrush = this.cDatePicker.BorderBrush;
			this._originBorderThickness = this.cDatePicker.BorderThickness;
			this._originTooltip = this.cDatePicker.ToolTip;
		}

		public new void Focus()
		{
			this.cDatePicker.Focus();
		}

		/// <summary>
		/// ロードイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrameworkControl_Loaded_1(object sender, RoutedEventArgs e)
		{
			//this.CheckValidation();
		}


		#region DatePicker Property

		#region Background
		/// <summary>
		/// コントロールの背景を表すブラシを取得または設定します。 (Control から継承されます。)
		/// </summary>
		[Category("レイアウト")]
		public Brush cBackground
		{
			set { cDatePicker.Background = value; }
			get { return cDatePicker.Background; }
		}
		#endregion

		#region CommandBindings
		/// <summary>
		/// この要素に関連付けられている CommandBinding のオブジェクトのコレクションを取得します。 
		/// </summary>
		[Category("動作")]
		public CommandBindingCollection cCommandBindings
		{
			set {  }
			get { return cDatePicker.CommandBindings; }
		}
		#endregion

		#region DataContext
		/// <summary>
		/// データ バインディングに参加すると要素のデータ コンテキストを取得または設定します。 
		/// </summary>
		[Category("動作")]
		public object cDataContext
		{
			set { cDatePicker.DataContext = value; }
			get { return cDatePicker.DataContext; }
		}
		#endregion

		#region Dispatcher
		/// <summary>
		/// この DispatcherObject が関連付けられている Dispatcher を取得します。 
		/// </summary>
		[Category("動作")]
		public object cDispatcher
		{
			set { }
			get { return cDatePicker.Dispatcher; }
		}
		#endregion

		#region Effect
		/// <summary>
		/// ビットマップ効果を UIElementに適用する取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public System.Windows.Media.Effects.Effect cEffect
		{
			set { cDatePicker.Effect = value; }
			get { return cDatePicker.Effect; }
		}
		#endregion

		#region Focusable
		/// <summary>
		/// 要素がフォーカスを得ることができるかどうかを示す値を取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public bool cFocusable
		{
			set { cDatePicker.Focusable = value; }
			get { return cDatePicker.Focusable; }
		}
		#endregion

		#region FontFamily
		/// <summary>
		/// コントロールのフォント ファミリを取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public FontFamily cFontFamily
		{
			set { cDatePicker.FontFamily = value; }
			get { return cDatePicker.FontFamily; }
		}
		#endregion

		#region FontSize
		/// <summary>
		/// コントロールのフォント ファミリを取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public double cFontSize
		{
			set { cDatePicker.FontSize = value; }
			get { return cDatePicker.FontSize; }
		}
		#endregion

		#region FontStretch
		/// <summary>
		/// 画面上でフォントを縮小または拡大する度合いを取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public FontStretch cFontStretch
		{
			set { cDatePicker.FontStretch = value; }
			get { return cDatePicker.FontStretch; }
		}
		#endregion

		#region FontStyle
		/// <summary>
		/// フォント スタイルを取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public FontStyle cFontStyle
		{
			set { cDatePicker.FontStyle = value; }
			get { return cDatePicker.FontStyle; }
		}
		#endregion

		#region FontWeight
		/// <summary>
		///	指定したフォントの太さを取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public FontWeight cFontWeight
		{
			set { cDatePicker.FontWeight = value; }
			get { return cDatePicker.FontWeight; }
		}
		#endregion

		#region Foreground
		/// <summary>
		/// 要素の提案された高さを取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public Brush cForeground
		{
			set { cDatePicker.Foreground = value; }
			get { return cDatePicker.Foreground; }
		}
		#endregion

		#region Height
		/// <summary>
		/// 要素の提案された高さを取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public double cHeight
		{
			set { cDatePicker.Height = value; }
			get { return cDatePicker.Height; }
		}
		#endregion

		#region HorizontalAlignment
		/// <summary>
		/// この要素が、Panel またはアイテム コントロールのような親要素内に構成されるときに適用される水平方向の配置特性を取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public HorizontalAlignment cHorizontalAlignment
		{
			set { cDatePicker.HorizontalAlignment = value; }
			get { return cDatePicker.HorizontalAlignment; }
		}
		#endregion

		#region IsEnabled
		/// <summary>
		/// この要素が ユーザー インターフェイス (UI)で有効になっているかどうかを示す値を取得または設定します
		/// </summary>
		[Category("デザイン")]
		public bool cIsEnabled
		{
			set { cDatePicker.IsEnabled = value; }
			get { return cDatePicker.IsEnabled; }
		}
		#endregion

		#region IsFocused
		/// <summary>
		/// この要素に論理フォーカスがあるかどうかを示す値を取得します。これは 依存関係プロパティです。
		/// </summary>
		[Category("デザイン")]
		public bool cIsFocused
		{
			set { }
			get { return cDatePicker.IsFocused; }
		}
		#endregion

		#region Margin
		/// <summary>
		/// 要素の前辺を取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public Thickness cMargin
		{
			set { cDatePicker.Margin = value; }
			get { return cDatePicker.Margin; }
		}
		#endregion

		#region MaxHeight
		/// <summary>
		/// 要素の高さの最大値を取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public double cMaxHeight
		{
			set { cDatePicker.MaxHeight = value; }
			get { return cDatePicker.MaxHeight; }
		}
		#endregion

		#region MaxWidth
		/// <summary>
		/// 要素の幅の最大値を取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public double cMaxWidth
		{
			set { cDatePicker.MaxWidth = value; }
			get { return cDatePicker.MaxWidth; }
		}
		#endregion

		#region MinHeight
		/// <summary>
		/// 要素の高さの最小値を取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public double cMinHeight
		{
			set { cDatePicker.MinHeight = value; }
			get { return cDatePicker.MinHeight; }
		}
		#endregion

		#region MinWidth
		/// <summary>
		/// 要素の幅の最小値を取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public double cMinWidth
		{
			set { cDatePicker.MinWidth = value; }
			get { return cDatePicker.MinWidth; }
		}
		#endregion

		#region Name
		/// <summary>
		/// 要素の ID の名前を取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public string cName
		{
			set { cDatePicker.Name = value; }
			get { return cDatePicker.Name; }
		}
		#endregion

		#region Padding
		/// <summary>
		/// コントロール内のスペースを取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public Thickness cPadding
		{
			set { cDatePicker.Padding = value; }
			get { return cDatePicker.Padding; }
		}
		#endregion

		#region Parent
		/// <summary>
		/// この要素の logical parent の要素を取得します。
		/// </summary>
		[Category("デザイン")]
		public DependencyObject cParent
		{
			set { }
			get { return cDatePicker.Parent; }
		}
		#endregion

		#region SelectedDate
		/// <summary>
		/// 現在選択されている日付を取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public DateTime? cSelectedDate
		{
			set { cDatePicker.SelectedDate = value; }
			get { return cDatePicker.SelectedDate; }
		}
		#endregion

		#region SelectedDateFormat
		/// <summary>
		/// 選択した日付を表示するために使用される形式を取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public DatePickerFormat cSelectedDateFormat
		{
			set { cDatePicker.SelectedDateFormat = value; }
			get { return cDatePicker.SelectedDateFormat; }
		}
		#endregion

		#region TabIndex
		/// <summary>
		/// ユーザーが Tab キーを使用してコントロール間を移動するときに、要素がフォーカスを受け取る順序を決定する値を取得または設定します
		/// </summary>
		[Category("デザイン")]
		public int cTabIndex
		{
			set { cDatePicker.TabIndex = value; }
			get { return cDatePicker.TabIndex; }
		}
		#endregion

		#region Text
		/// <summary>
		/// DatePicker によって表示されるテキストを取得するか、選択した日付を設定します。
		/// </summary>
		[Category("デザイン")]
		public string cText
		{
			set { cDatePicker.Text = value; }
			get { return cDatePicker.Text; }
		}
		#endregion

		#region ToolTip
		/// <summary>
		/// ユーザー インターフェイス (UI)のこの要素に対して表示されるツール ヒントのオブジェクトを取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public object cToolTip
		{
			set { cDatePicker.ToolTip = value; }
			get { return cDatePicker.ToolTip; }
		}
		#endregion

		#region Width
		/// <summary>
		/// 要素の幅を取得または設定します。
		/// </summary>
		[Category("デザイン")]
		public double cWidth
		{
			set { cDatePicker.Width = value; }
			get { return cDatePicker.Width; }
		}
		#endregion

		#endregion

		/// <summary>
		/// LostFocus
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cDatePicker_LostFocus_1(object sender, RoutedEventArgs e)
		{
			if (this.CheckValidation() && this.SelectedDate != null)
				this.cDatePicker.Text = ((DateTime)this.SelectedDate).ToString();
		}

		/// <summary>
		/// 日付を整形する
		/// </summary>
		/// <param name="InputNumber">入力された数値</param>
		/// <param name="TargetDay">元とする日付</param>
		private void DatePickerFormat(string InputNumber,DateTime? TargetDay)
		{
			int Year = 0;
			int Month = 0;
			int Day = 0;
			try
			{
				switch (InputNumber.Length)
				{
				case 1:
					InputDate(TargetDay.Value.Year, TargetDay.Value.Month, int.Parse(InputNumber));
					break;
				case 2:
					InputDate(TargetDay.Value.Year, TargetDay.Value.Month, int.Parse(InputNumber));
					break;
				case 3:
					Month = int.Parse(InputNumber.ToString().Substring(0,1));
					Day = int.Parse(InputNumber.ToString().Substring(1,2));
					InputDate(TargetDay.Value.Year, Month, Day);
					break;
				case 4:
					Month = int.Parse(InputNumber.ToString().Substring(0,2));
					Day = int.Parse(InputNumber.ToString().Substring(2,2));
					InputDate(TargetDay.Value.Year, Month, Day);
					break;
				case 6:
					Year = int.Parse(TargetDay.Value.Year.ToString().Substring(0,2)) * 100 + int.Parse(InputNumber.Substring(0,2));
					Month = int.Parse(InputNumber.ToString().Substring(2,2));
					Day = int.Parse(InputNumber.ToString().Substring(4,2));
					InputDate(Year, Month, Day);
					break;
				case 8:
					Year =  int.Parse(InputNumber.Substring(0,4));
					Month = int.Parse(InputNumber.ToString().Substring(4,2));
					Day = int.Parse(InputNumber.ToString().Substring(6,2));
					InputDate(Year, Month, Day);
					break;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// DataPickerに日付を格納する
		/// </summary>
		/// <param name="Year">年</param>
		/// <param name="Month">月</param>
		/// <param name="Day">日</param>
		private void InputDate(int Year, int Month, int Day)
		{
			cDatePicker.SelectedDate = new DateTime(Year, Month, Day);
		}

		/// <summary>
		/// 日付にならない数値が入力された場合
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cDatePicker_DateValidationError_1(object sender, DatePickerDateValidationErrorEventArgs e)
		{
			if (this.BeforeSelectedDate == null)
			{
				//DatePickerFormat(cDatePicker.Text, DateTime.Today);
			}
			else
			{
				//DatePickerFormat(cDatePicker.Text,this.BeforeSelectedDate);
			}
		}

		private void cDatePicker_GotKeyboardFocus_1(object sender, KeyboardFocusChangedEventArgs e)
		{
			this.BeforeSelectedDate = this.cDatePicker.SelectedDate;
		}

		public override bool CheckValidation()
		{
			bool chk = true;
			try
			{
				if (SelectedDate == null || ((SelectedDate is string) && string.IsNullOrWhiteSpace((string)SelectedDate)))
				{
					if (IsRequired)
					{
						internalValidationStatus = Validator.ValidationMessage.ErrEmpty;
						return false;
					}
					else
					{
						internalValidationStatus = string.Empty;
						return true;
					}
				}

				DateTime date;
				chk = DateTime.TryParse(this.cText, out date);
				if (chk == true)
				{
					internalValidationStatus = string.Empty;
				}
				else
				{
					internalValidationStatus = Validator.ValidationMessage.ErrDate;
				}
			}
			catch (Exception)
			{
				internalValidationStatus = Validator.ValidationMessage.ErrDate;
			}

			return chk;
		}

		public override string GetValidationMessage()
		{
			return internalValidationStatus;
		}

		private void DatePicker_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			TraversalRequest vector = null;
			switch (e.Key)
			{
			case Key.Enter:
				vector = new TraversalRequest(FocusNavigationDirection.Next);
				break;
			case Key.Down:
				vector = new TraversalRequest(FocusNavigationDirection.Next);
				break;
			case Key.Up:
				vector = new TraversalRequest(FocusNavigationDirection.Previous);
				break;
			//case Key.Right:
			//	vector = new TraversalRequest(FocusNavigationDirection.Next);
			//	break;
			//case Key.Left:
			//	vector = new TraversalRequest(FocusNavigationDirection.Previous);
			//	break;
			}
			FocusControl.SetFocusWithOrder(vector);
		}
	}
}
