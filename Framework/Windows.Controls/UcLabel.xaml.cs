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
	/// UcLabel.xaml の相互作用ロジック
	/// </summary>
	public partial class UcLabel : UserControl
	{

		/// <summary>
		/// 初期化
		/// </summary>
		public UcLabel()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
				"LabelText",
				typeof(object),
				typeof(UcLabel),
				new UIPropertyMetadata(string.Empty)
		);

		[BindableAttribute(true)]
		public object LabelText
		{
			get { return (object)GetValue(LabelTextProperty); }
			set { SetValue(LabelTextProperty, value); }
		}

		#region Label Property

		#region Background
		/// <summary>
		/// コントロールの背景を表すブラシを取得または設定します。
		/// </summary>
		[Category("ブラシ")]
		public  Brush cBackground
		{
			get { return this.cBorder.Background; }
			set { this.cBorder.Background = value; }
		}
		#endregion

		#region DataContext
		/// <summary>
		/// データ バインディングに参加すると要素のデータ コンテキストを取得または設定します
		/// </summary>
		[Category("動作")]
		public object cDataContext
		{
			get { return this.cLabel.DataContext; }
			set { this.cLabel.DataContext = value; }
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
			get { return this.cLabel.CommandBindings; }
			set {}
		}
		#endregion
		
		#region CommandBindings
		/// <summary>
		/// ContentControl のコンテンツを取得または設定します。
		/// </summary>
		[Category("レイアウト")]
		public object cContent
		{
			get { return this.cLabel.Content; }
			set { this.cLabel.Content = value; }
		}
		#endregion
	
		#region Dispatcher
		/// <summary>
		/// この DispatcherObject が関連付けられている Dispatcher を取得します
		/// </summary>
		[Category("動作")]
		public object cDispatcher
		{
			get { return this.cLabel.Dispatcher; }
			set {  }
		}
		#endregion

		#region Focusable
		/// <summary>
		/// 要素がフォーカスを得ることができるかどうかを示す値を取得または設定します。これは 依存関係プロパティです
		/// </summary>
		[Category("動作")]
		public bool cFocusable
		{
			get { return this.cLabel.Focusable; }
			set { this.cLabel.Focusable = value; }
		}
		#endregion

		#region FontFamily
		/// <summary>
		/// コントロールのフォント ファミリを取得または設定します
		/// </summary>
		[Category("動作")]
		public FontFamily cFontFamily
		{
			get { return this.cLabel.FontFamily; }
			set { this.cLabel.FontFamily = value; }
		}
		#endregion
		
		#region FontSize
		/// <summary>
		/// フォント サイズを取得または設定します
		/// </summary>
		[Category("レイアウト")]
		public double cFontSize
		{
			get { return this.cLabel.FontSize; }
			set { this.cLabel.FontSize = value; }
		}
		#endregion
	
		#region FontStretch
		/// <summary>
		/// 画面上でフォントを縮小または拡大する度合いを取得または設定します。 
		/// </summary>
		[Category("動作")]
		public FontStretch cFontStretch
		{
			get { return this.cLabel.FontStretch; }
			set { this.cLabel.FontStretch = value; }
		}
		#endregion

		#region FontStyle
		/// <summary>
		/// フォント スタイルを取得または設定します。
		/// </summary>
		[Category("動作")]
		public FontStyle cFontStyle
		{
			get { return this.cLabel.FontStyle; }
			set { this.cLabel.FontStyle = value; }
		}
		#endregion
	
		#region FontWeight
		/// <summary>
		/// 画面上でフォントを縮小または拡大する度合いを取得または設定します。 
		/// </summary>
		[Category("動作")]
		public FontWeight cFontWeight
		{
			get { return this.cLabel.FontWeight; }
			set { this.cLabel.FontWeight = value; }
		}
		#endregion
	
		#region Foreground
		/// <summary>
		/// 前景色を表すブラシを取得または設定します
		/// </summary>
		[Category("ブラシ")]
		public Brush cForeground
		{
			get { return this.cLabel.Foreground; }
			set { this.cLabel.Foreground = value; }
		}
		#endregion

		#region HasContent
		/// <summary>
		/// ContentControl にコンテンツが含まれているかどうかを示す値を取得します。 
		/// </summary>
		[Category("動作")]
		public bool cHasContent
		{
			get { return this.cLabel.HasContent; }
			set {  }
		}
		#endregion
	
		#region Height
		/// <summary>
		/// 要素の提案された高さを取得または設定します
		/// </summary>
		[Category("動作")]
		public double cHeight
		{
			get { return this.cBorder.Height; }
            set { this.cBorder.Height = value; }
		}
		#endregion
	
		#region HorizontalContentAlignment
		/// <summary>
		/// コントロールのコンテンツの水平方向の配置を取得または設定します
		/// </summary>
		[Category("表示")]
		public HorizontalAlignment cHorizontalContentAlignment
		{
            get { return this.cLabel.HorizontalContentAlignment; }
            set { this.cLabel.HorizontalContentAlignment = value; }
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
			get { return this.cLabel.IsFocused; }
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
			get { return this.cLabel.IsTabStop; }
			set { this.cLabel.IsTabStop = value; }
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
			get { return this.cLabel.IsVisible; }
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
            get { return this.cBorder.Margin; }
            set { this.cBorder.Margin = value; }
		}
		#endregion
	
		#region MaxHeight
		/// <summary>
		/// 要素の高さの最大値を取得または設定します
		/// </summary>
		[Category("レイアウト")]
		public double cMaxHeight
		{
            get { return this.cBorder.MaxHeight; }
            set { this.cBorder.MaxHeight = value; }
		}
		#endregion
	
		#region MaxWidth
		/// <summary>
		/// 要素の高さの最大値を取得または設定します
		/// </summary>
		[Category("レイアウト")]
		public double cMaxWidth
		{
            get { return this.cBorder.MaxWidth; }
            set { this.cBorder.MaxWidth = value; }
		}
		#endregion
	
		#region MinHeight
		/// <summary>
		/// 要素の高さの最大値を取得または設定します
		/// </summary>
		[Category("レイアウト")]
		public double cMinHeight
		{
            get { return this.cBorder.MinHeight; }
            set { this.cBorder.MinHeight = value; }
		}
		#endregion

		#region MinWidth
		/// <summary>
		/// 要素の高さの最小値を取得または設定します 
		/// </summary>
		[Category("レイアウト")]
		public double cMinWidth
		{
            get { return this.cBorder.MinWidth; }
            set { this.cBorder.MinWidth = value; }
		}
		#endregion

		#region Name
		/// <summary>
		/// 要素の ID の名前を取得または設定します。名前は XAML のプロセッサで処理中に作成されると分離コードを、
		/// イベント ハンドラー コードなどのマークアップ要素を参照できるように参照を提供します。
		/// </summary>
		[Category("共通")]
		public string cName
		{
			get { return this.cLabel.Name; }
			set { this.cLabel.Name = value; }
		}
		#endregion

		#region Padding
		/// <summary>
		/// コントロール内のスペースを取得または設定します
		/// </summary>
		[Category("動作")]
		public Thickness cPadding
		{
			get { return this.cLabel.Padding; }
			set { this.cLabel.Padding = value; }
		}
		#endregion
	
		#region Parent
		/// <summary>
		/// この要素の logical parent の要素を取得します。
		/// </summary>
		[Category("動作")]
		public DependencyObject cParent
		{
			get { return this.cLabel.Parent; }
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
			get { return this.cLabel.TabIndex; }
			set { this.cLabel.TabIndex = value; }
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
			get { return this.cLabel.ToolTip; }
			set { this.cLabel.ToolTip = value; }
		}
		#endregion

		#region VerticalContentAlignment
		/// <summary>
		/// コントロールのコンテンツの垂直方向の配置を取得または設定します。
		/// </summary>
		[Category("レイアウト")]
		public VerticalAlignment cVerticalContentAlignment
		{
			get { return this.cLabel.VerticalContentAlignment; }
			set { this.cLabel.VerticalContentAlignment = value; }
		}
		#endregion

		#region Visibility
		/// <summary>
		/// この要素の ユーザー インターフェイス (UI) 表現を取得または設定します。
		/// </summary>
		[Category("レイアウト")]
		public Visibility cVisibility
		{
			get { return this.cBorder.Visibility; }
			set { this.cBorder.Visibility = value; }
		}
		#endregion
	
		#region Width
		/// <summary>
		/// 要素の幅を取得または設定します。 
		/// </summary>
		[Category("レイアウト")]
		public double cWidth
		{
			get { return this.cBorder.Width; }
            set { this.cBorder.Width = value; }
		}
		#endregion

		#endregion


	}
}
