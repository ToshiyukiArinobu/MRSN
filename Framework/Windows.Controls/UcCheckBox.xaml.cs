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


namespace KyoeiSystem.Framework.Windows.Controls
{
	/// <summary>
	/// UcLabel.xaml の相互作用ロジック
	/// </summary>
	public partial class UcCheckBox : FrameworkControl
	{
		/// <summary>
		/// 初期化
		/// </summary>
		public UcCheckBox()
		{
			InitializeComponent();
		}

		private void Root_Loaded(object sender, RoutedEventArgs e)
		{
			//this.IsChecked = false;
		}

		public new void Focus()
		{
			this.cCheckBox.Focus();
		}

		public static readonly RoutedEvent CheckedEvent = EventManager.RegisterRoutedEvent(
			"Checked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UcCheckBox));
		public static readonly RoutedEvent UnCheckedEvent = EventManager.RegisterRoutedEvent(
			"UnChecked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UcCheckBox));
		public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(
			"Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UcCheckBox));

		public event RoutedEventHandler Checked
		{
			add { AddHandler(CheckedEvent, value); }
			remove { RemoveHandler(CheckedEvent, value); }
		}

		public event RoutedEventHandler UnChecked
		{
			add { AddHandler(UnCheckedEvent, value); }
			remove { RemoveHandler(UnCheckedEvent, value); }
		}

		public event RoutedEventHandler Click
		{
			add { AddHandler(ClickEvent, value); }
			remove { RemoveHandler(ClickEvent, value); }
		}

		private void cb_Checked(object sender, RoutedEventArgs e)
		{
			RaiseEvent(new RoutedEventArgs(UcCheckBox.CheckedEvent));
		}

		private void cb_UnChecked(object sender, RoutedEventArgs e)
		{
			RaiseEvent(new RoutedEventArgs(UcCheckBox.UnCheckedEvent));
		}

		void cb_Click(object sender, RoutedEventArgs e)
		{
			RaiseEvent(new RoutedEventArgs(UcCheckBox.ClickEvent));
		}

		private void cCheckBox_PreviewKeyDown(object sender, KeyEventArgs e)
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
			case Key.Right:
				vector = new TraversalRequest(FocusNavigationDirection.Next);
				break;
			case Key.Left:
				vector = new TraversalRequest(FocusNavigationDirection.Previous);
				break;
			}
			FocusControl.SetFocusWithOrder(vector);
		}

		private void CheckBox_KeyDown(object sender, KeyEventArgs e)
		{
		}



		public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(
				"IsChecked",
				typeof(bool?),
				typeof(UcCheckBox),
				new UIPropertyMetadata(null)
		);

		[BindableAttribute(true)]
		public bool? IsChecked
		{
			get { return (bool?)GetValue(IsCheckedProperty); }
			set { SetValue(IsCheckedProperty, value); }
		}

		#region CheckBox Property

		#region Background
		/// <summary>
		/// コントロールの背景を表すブラシを取得または設定します。
		/// </summary>
		[Category("ブラシ")]
		public Brush cBackground
		{
			get { return this.cCheckBox.Background; }
			set { this.cCheckBox.Background = value; }
		}
		#endregion

		#region CommandBindings
		/// <summary>
		/// この要素に関連付けられている CommandBinding のオブジェクトのコレクションを取得します。
		/// CommandBinding は、この要素に対するコマンドを有効にし、コマンド、イベント、およびこの要素に接続するハンドラー間の連結を宣言します。
		/// </summary>
		[Category("動作")]
		public CommandBindingCollection cCommandBindings
		{
			get { return this.cCheckBox.CommandBindings; }
			set { }
		}
		#endregion

		#region CommandBindings
		/// <summary>
		/// ContentControl のコンテンツを取得または設定します。
		/// </summary>
		[Category("レイアウト")]
		public object cContent
		{
			get { return this.cCheckBox.Content; }
			set { this.cCheckBox.Content = value; }
		}
		#endregion

		#region Dispatcher
		/// <summary>
		/// この DispatcherObject が関連付けられている Dispatcher を取得します
		/// </summary>
		[Category("動作")]
		public object cDispatcher
		{
			get { return this.cCheckBox.Dispatcher; }
			set { }
		}
		#endregion

		#region Focusable
		/// <summary>
		/// 要素がフォーカスを得ることができるかどうかを示す値を取得または設定します。これは 依存関係プロパティです
		/// </summary>
		[Category("動作")]
		public bool cFocusable
		{
			get { return this.cCheckBox.Focusable; }
			set { this.cCheckBox.Focusable = value; }
		}
		#endregion

		#region FontFamily
		/// <summary>
		/// コントロールのフォント ファミリを取得または設定します
		/// </summary>
		[Category("動作")]
		public FontFamily cFontFamily
		{
			get { return this.cCheckBox.FontFamily; }
			set { this.cCheckBox.FontFamily = value; }
		}
		#endregion

		#region FontSize
		/// <summary>
		/// フォント サイズを取得または設定します
		/// </summary>
		[Category("レイアウト")]
		public double cFontSize
		{
			get { return this.cCheckBox.FontSize; }
			set { this.cCheckBox.FontSize = value; }
		}
		#endregion

		#region FontStretch
		/// <summary>
		/// 画面上でフォントを縮小または拡大する度合いを取得または設定します。 
		/// </summary>
		[Category("動作")]
		public FontStretch cFontStretch
		{
			get { return this.cCheckBox.FontStretch; }
			set { this.cCheckBox.FontStretch = value; }
		}
		#endregion

		#region FontStyle
		/// <summary>
		/// フォント スタイルを取得または設定します。
		/// </summary>
		[Category("動作")]
		public FontStyle cFontStyle
		{
			get { return this.cCheckBox.FontStyle; }
			set { this.cCheckBox.FontStyle = value; }
		}
		#endregion

		#region FontWeight
		/// <summary>
		/// 画面上でフォントを縮小または拡大する度合いを取得または設定します。 
		/// </summary>
		[Category("動作")]
		public FontWeight cFontWeight
		{
			get { return this.cCheckBox.FontWeight; }
			set { this.cCheckBox.FontWeight = value; }
		}
		#endregion

		#region Foreground
		/// <summary>
		/// 前景色を表すブラシを取得または設定します
		/// </summary>
		[Category("ブラシ")]
		public Brush cForeground
		{
			get { return this.cCheckBox.Foreground; }
			set { this.cCheckBox.Foreground = value; }
		}
		#endregion

		#region HasContent
		/// <summary>
		/// ContentControl にコンテンツが含まれているかどうかを示す値を取得します。 
		/// </summary>
		[Category("動作")]
		public bool cHasContent
		{
			get { return this.cCheckBox.HasContent; }
			set { }
		}
		#endregion

		#region Height
		/// <summary>
		/// 要素の提案された高さを取得または設定します
		/// </summary>
		[Category("動作")]
		public double cHeight
		{
			get { return this.cCheckBox.Height; }
			set { this.cCheckBox.Height = value; }
		}
		#endregion

		#region HorizontalContentAlignment
		/// <summary>
		/// コントロールのコンテンツの水平方向の配置を取得または設定します
		/// </summary>
		[Category("表示")]
		public HorizontalAlignment cHorizontalContentAlignment
		{
			get { return this.cCheckBox.HorizontalContentAlignment; }
			set { this.cCheckBox.HorizontalContentAlignment = value; }
		}
		#endregion

		#region
		[Category("動作")]
		public bool cIsEnabled
		{
			get { return cCheckBox.IsEnabled; }
			set { cCheckBox.IsEnabled = value; }
		}
		#endregion

		#region IsFocused
		/// <summary>
		/// この要素に論理フォーカスがあるかどうかを示す値を取得します。
		/// これは 依存関係プロパティです。
		/// 
		/// </summary>
		[Category("動作")]
		public bool cIsFocused
		{
			get { return this.cCheckBox.IsFocused; }
			set { }
		}
		#endregion

		#region IsTabStop
		/// <summary>
		/// コントロールがタブ ナビゲーションに含まれるかどうかを示す値を取得または設定します
		/// </summary>
		[Category("動作")]
		public bool cIsTabStop
		{
			get { return this.cCheckBox.IsTabStop; }
			set { this.cCheckBox.IsTabStop = value; }
		}
		#endregion

		#region IsVisible
		/// <summary>
		/// この要素が ユーザー インターフェイス (UI)に表示されるかどうかを示す値を取得します。
		/// これは 依存関係プロパティです。
		/// </summary>
		[Category("動作")]
		public bool cIsVisible
		{
			get { return this.cCheckBox.IsVisible; }
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
		public Thickness cMargin
		{
			get { return this.cCheckBox.Margin; }
			set { this.cCheckBox.Margin = value; }
		}
		#endregion

		#region MaxHeight
		/// <summary>
		/// 要素の高さの最大値を取得または設定します
		/// </summary>
		[Category("レイアウト")]
		public double cMaxHeight
		{
			get { return this.cCheckBox.MaxHeight; }
			set { this.cCheckBox.MaxHeight = value; }
		}
		#endregion

		#region MaxWidth
		/// <summary>
		/// 要素の高さの最大値を取得または設定します
		/// </summary>
		[Category("レイアウト")]
		public double cMaxWidth
		{
			get { return this.cCheckBox.MaxWidth; }
			set { this.cCheckBox.MaxWidth = value; }
		}
		#endregion

		#region MinHeight
		/// <summary>
		/// 要素の高さの最大値を取得または設定します
		/// </summary>
		[Category("レイアウト")]
		public double cMinHeight
		{
			get { return this.cCheckBox.MinHeight; }
			set { this.cCheckBox.MinHeight = value; }
		}
		#endregion

		#region MinWidth
		/// <summary>
		/// 要素の高さの最小値を取得または設定します 
		/// </summary>
		[Category("レイアウト")]
		public double cMinWidth
		{
			get { return this.cCheckBox.MinWidth; }
			set { this.cCheckBox.MinWidth = value; }
		}
		#endregion

		#region Padding
		/// <summary>
		/// コントロール内のスペースを取得または設定します
		/// </summary>
		[Category("動作")]
		public Thickness cPadding
		{
			get { return this.cCheckBox.Padding; }
			set { this.cCheckBox.Padding = value; }
		}
		#endregion

		#region Parent
		/// <summary>
		/// この要素の logical parent の要素を取得します。
		/// </summary>
		[Category("動作")]
		public DependencyObject cParent
		{
			get { return this.cCheckBox.Parent; }
			set { }
		}
		#endregion

		#region TabIndex
		/// <summary>
		/// ユーザー インターフェイス (UI)のこの要素に対して表示される
		/// ツール ヒントのオブジェクトを取得または設定します
		/// </summary>
		[Category("動作")]
		public int cTabIndex
		{
			get { return this.cCheckBox.TabIndex; }
			set { this.cCheckBox.TabIndex = value; }
		}
		#endregion

		#region ToolTip
		/// <summary>
		/// ユーザー インターフェイス (UI)のこの要素に対して表示されるツール ヒントのオ
		/// ブジェクトを取得または設定します
		/// </summary>
		[Category("動作")]
		public object cToolTip
		{
			get { return this.cCheckBox.ToolTip; }
			set { this.cCheckBox.ToolTip = value; }
		}
		#endregion

		#region VerticalContentAlignment
		/// <summary>
		/// コントロールのコンテンツの垂直方向の配置を取得または設定します。
		/// </summary>
		[Category("レイアウト")]
		public VerticalAlignment cVerticalContentAlignment
		{
			get { return this.cCheckBox.VerticalContentAlignment; }
			set { this.cCheckBox.VerticalContentAlignment = value; }
		}
		#endregion

		#region Visibility
		/// <summary>
		/// この要素の ユーザー インターフェイス (UI) 表現を取得または設定します。
		/// </summary>
		[Category("レイアウト")]
		public Visibility cVisibility
		{
			get { return this.cCheckBox.Visibility; }
			set { this.cCheckBox.Visibility = value; }
		}
		#endregion

		#region Width
		/// <summary>
		/// 要素の幅を取得または設定します。 
		/// </summary>
		[Category("レイアウト")]
		public double cWidth
		{
			get { return this.cCheckBox.Width; }
			set { this.cCheckBox.Width = value; }
		}
		#endregion

		#endregion

	}
}
