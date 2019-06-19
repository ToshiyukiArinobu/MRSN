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
    /// UcLabelTextRadioButton.xaml の相互作用ロジック
    /// </summary>
    public partial class UcLabelTextRadioButton : FrameworkControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(UcLabelTextRadioButton),
            new UIPropertyMetadata(string.Empty)
        );

        [BindableAttribute(true)]
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
                if (string.IsNullOrEmpty(value))
                {
                    foreach (RadioButton rbtn in this._buttonList)
                    {
                        rbtn.IsChecked = null;
                    }
                }
            }
        }

        private List<RadioButton> _buttonList = new List<RadioButton>();

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

        /// <summary>
        /// Label+TextBox+RadioButton
        /// </summary>
        public UcLabelTextRadioButton()
        {
            InitializeComponent();
            this._buttonList.Add(this.cRadio1);
            this._buttonList.Add(this.cRadio2);
            this._buttonList.Add(this.cRadio3);
            this._buttonList.Add(this.cRadio4);
            this._buttonList.Add(this.cRadio5);
            this._buttonList.Add(this.cRadio6);
            this._buttonList.Add(this.cRadio7);
            this._buttonList.Add(this.cRadio8);
            cTextBox.ValidationType = Validator.ValidationTypes.StringList;
            this.cRadio1.Click += cRadio_Click;
            this.cRadio2.Click += cRadio_Click;
            this.cRadio3.Click += cRadio_Click;
            this.cRadio4.Click += cRadio_Click;
            this.cRadio5.Click += cRadio_Click;
            this.cRadio6.Click += cRadio_Click;
            this.cRadio7.Click += cRadio_Click;
            this.cRadio8.Click += cRadio_Click;
            SetupTextList();
            this.Text = string.Empty;
        }

        //public override void OnUnload()
        //{
        //	base.OnUnload();
        //	this.cRadio1.Click -= cRadio_Click;
        //	this.cRadio2.Click -= cRadio_Click;
        //	this.cRadio3.Click -= cRadio_Click;
        //	this.cRadio4.Click -= cRadio_Click;
        //	this.cRadio5.Click -= cRadio_Click;
        //	this.cRadio6.Click -= cRadio_Click;
        //	this.cRadio7.Click -= cRadio_Click;
        //	this.cRadio8.Click -= cRadio_Click;
        //}

        void cRadio_Click(object sender, RoutedEventArgs e)
        {
            TraversalRequest vector = new TraversalRequest(FocusNavigationDirection.Next);
            UIElement element = Keyboard.FocusedElement as UIElement;
            if (element != null)
            {
                SkipRadioButton(element, vector);
                element = Keyboard.FocusedElement as UIElement;
                Type tp = element.GetType();
                if (tp.Name.StartsWith("Ribbon"))
                {
                    // リボンコントロールに移動したら入力可能な先頭のフィールドに強制移動する
                    FocusControl.SetFocusToTopControl(Window.GetWindow(element));
                }
            }
        }

        private bool SkipRadioButton(UIElement element, TraversalRequest vector)
        {
            if (element != null)
            {
                element.MoveFocus(vector);
                element = Keyboard.FocusedElement as UIElement;
                if (element.GetType() == typeof(RadioButton))
                {
                    return SkipRadioButton(element, vector);
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public List<RadioButton> GetRadioButtonList()
        {
            return this._buttonList;
        }

        private void SetupTextList()
        {
            if (cTextBox.ValueList == null)
            {
                cTextBox.ValueList = new List<string>();
            }
            else
            {
                cTextBox.ValueList.Clear();
            }
            foreach (RadioButton rbtn in this._buttonList)
            {
                if (rbtn.Visibility == System.Windows.Visibility.Visible)
                {
                    if (rbtn.Content != null && rbtn.Content.GetType() == typeof(string))
                    {
                        if ((rbtn.Content as string).Length > 0)
                        {
                            cTextBox.ValueList.Add((rbtn.Content as string).Substring(0, 1));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ラジオボタンの数を指定
        /// </summary>
        public enum RadioButtonCount
        {
            /// <summary>
            /// １つ
            /// </summary>
            One,
            /// <summary>
            /// ２つ
            /// </summary>
            Two,
            /// <summary>
            /// ３つ
            /// </summary>
            Three,
            /// <summary>
            /// ４つ
            /// </summary>
            Four,
            /// <summary>
            /// ５つ
            /// </summary>
            Five,
            /// <summary>
            /// ６つ
            /// </summary>
            Six,
            /// <summary>
            /// ７つ
            /// </summary>
            Seven,
            /// <summary>
            /// ８つ
            /// </summary>
            Eight
        }

        //入力数値の範囲指定
        private int StartNm { get; set; }
        private int EndNm { get; set; }

        /// <summary>
        /// ラジオボタンを１からスタートさせる。
        /// </summary>
        private bool _RadioOneStart = false;
        public bool RadioOneStart
        {
            get { return this._RadioOneStart; }
            set
            {
                this._RadioOneStart = value;
                if (value == true)
                {
                    this.cRadio1.Visibility = Visibility.Collapsed;
                    StartNm = StartNm + 1;
                    EndNm = EndNm + 1;
                }
                else
                {
                    this.cRadio1.Visibility = Visibility.Visible;
                    StartNm = 0;
                    EndNm = EndNm + 0;
                }
            }
        }

        /// <summary>
        /// ラジオボタンの表示個数を格納する変数
        /// </summary>
        private RadioButtonCount RadioCount = RadioButtonCount.One;

        /// <summary>
        /// ラジオボタンの初期選択を格納する変数
        /// </summary>
        private RadioButtonCount RadioSelect = RadioButtonCount.One;

        /// <summary>
        /// ラジオボタンの個数を選択
        /// </summary>
        [Category("デザイン")]
        public RadioButtonCount RadioViewCount
        {
            set
            {
                InitializationRadioButton();
                RadioCount = value;

                switch (RadioCount)
                {
                    case RadioButtonCount.One:
                        ViewRadioOneStart();
                        EndNm = EndNm;
                        break;
                    case RadioButtonCount.Two:
                        ViewRadioOneStart();
                        cRadio2.Visibility = Visibility.Visible;
                        EndNm = EndNm + 1;
                        break;
                    case RadioButtonCount.Three:
                        ViewRadioOneStart();
                        cRadio2.Visibility = Visibility.Visible;
                        cRadio3.Visibility = Visibility.Visible;
                        EndNm = EndNm + 2;
                        break;
                    case RadioButtonCount.Four:
                        ViewRadioOneStart();
                        cRadio2.Visibility = Visibility.Visible;
                        cRadio3.Visibility = Visibility.Visible;
                        cRadio4.Visibility = Visibility.Visible;
                        EndNm = EndNm + 3;
                        break;
                    case RadioButtonCount.Five:
                        ViewRadioOneStart();
                        cRadio2.Visibility = Visibility.Visible;
                        cRadio3.Visibility = Visibility.Visible;
                        cRadio4.Visibility = Visibility.Visible;
                        cRadio5.Visibility = Visibility.Visible;
                        EndNm = EndNm + 4;
                        break;
                    case RadioButtonCount.Six:
                        ViewRadioOneStart();
                        cRadio2.Visibility = Visibility.Visible;
                        cRadio3.Visibility = Visibility.Visible;
                        cRadio4.Visibility = Visibility.Visible;
                        cRadio5.Visibility = Visibility.Visible;
                        cRadio6.Visibility = Visibility.Visible;
                        EndNm = EndNm + 5;
                        break;
                    case RadioButtonCount.Seven:
                        ViewRadioOneStart();
                        cRadio2.Visibility = Visibility.Visible;
                        cRadio3.Visibility = Visibility.Visible;
                        cRadio4.Visibility = Visibility.Visible;
                        cRadio5.Visibility = Visibility.Visible;
                        cRadio6.Visibility = Visibility.Visible;
                        cRadio7.Visibility = Visibility.Visible;
                        EndNm = EndNm + 6;
                        break;
                    case RadioButtonCount.Eight:
                        ViewRadioOneStart();
                        cRadio2.Visibility = Visibility.Visible;
                        cRadio3.Visibility = Visibility.Visible;
                        cRadio4.Visibility = Visibility.Visible;
                        cRadio5.Visibility = Visibility.Visible;
                        cRadio6.Visibility = Visibility.Visible;
                        cRadio7.Visibility = Visibility.Visible;
                        cRadio8.Visibility = Visibility.Visible;
                        EndNm = EndNm + 7;
                        break;
                }
                SetupTextList();
            }
            get { return RadioCount; }
        }

        /// <summary>
        /// ラジオボタンの初期選択を選択
        /// </summary>
        [Category("デザイン")]
        public RadioButtonCount RadioSelectButton
        {
            set
            {
                RadioSelect = value;
                switch (RadioSelect)
                {
                    case RadioButtonCount.One:
                        cRadio1.IsChecked = true;
                        break;
                    case RadioButtonCount.Two:
                        cRadio2.IsChecked = true;
                        break;
                    case RadioButtonCount.Three:
                        cRadio3.IsChecked = true;
                        break;
                    case RadioButtonCount.Four:
                        cRadio4.IsChecked = true;
                        break;
                    case RadioButtonCount.Five:
                        cRadio5.IsChecked = true;
                        break;
                    case RadioButtonCount.Six:
                        cRadio6.IsChecked = true;
                        break;
                    case RadioButtonCount.Seven:
                        cRadio7.IsChecked = true;
                        break;
                    case RadioButtonCount.Eight:
                        cRadio8.IsChecked = true;
                        break;
                }
            }
            get { return RadioSelect; }
        }

        /// <summary>
        /// ラジオボタンの初期化
        /// </summary>
        private void InitializationRadioButton()
        {
            cRadio1.Visibility = Visibility.Collapsed;
            cRadio2.Visibility = Visibility.Collapsed;
            cRadio3.Visibility = Visibility.Collapsed;
            cRadio4.Visibility = Visibility.Collapsed;
            cRadio5.Visibility = Visibility.Collapsed;
            cRadio6.Visibility = Visibility.Collapsed;
            cRadio7.Visibility = Visibility.Collapsed;
            cRadio8.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// ラジオボタンのスタート項目の表示の有無
        /// </summary>
        private void ViewRadioOneStart()
        {
            if (this.RadioOneStart == true)
            {
                cRadio1.Visibility = Visibility.Collapsed;
            }
            else
            {
                cRadio1.Visibility = Visibility.Visible;
            }
        }

        public override bool CheckValidation()
        {
            return this.cTextBox.CheckValidation();
        }

        public override string GetValidationMessage()
        {
            return this.cTextBox.GetValidationMessage();
        }


        /// <summary>
        /// ラジオボタンの変化を取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // WPFのRadioButtonは IsChecked の Binding が効かないバグがあるため、
            // チェックされたボタン以外を全てオフにする
            foreach (var ctl in ((sender as RadioButton).Parent as StackPanel).Children)
            {
                // 所属する親コンテナからRadioButtonを探す
                if (ctl.GetType() == typeof(RadioButton))
                {
                    if ((ctl as RadioButton).Content == (sender as RadioButton).Content)
                    {

                        if (this.cRadio1.IsChecked == true)
                        {
                            this.Text = "0";
                        }
                        else if (this.cRadio2.IsChecked == true)
                        {
                            this.Text = "1";
                        }
                        else if (this.cRadio3.IsChecked == true)
                        {
                            this.Text = "2";
                        }
                        else if (this.cRadio4.IsChecked == true)
                        {
                            this.Text = "3";
                        }
                        else if (this.cRadio5.IsChecked == true)
                        {
                            this.Text = "4";
                        }
                        else if (this.cRadio6.IsChecked == true)
                        {
                            this.Text = "5";
                        }
                        else if (this.cRadio7.IsChecked == true)
                        {
                            this.Text = "6";
                        }
                        else
                        {
                            this.Text = "7";
                        }
                        // イベントのターゲットならそのまま
                        continue;
                    }
                    // イベントのターゲット以外のRadioButtonはオフにする
                    (ctl as RadioButton).IsChecked = false;
                }
            }
        }

        #region Label_Content
        /// <summary>
        /// LabelのContent
        /// </summary>
        [Category("デザイン")]
        public string Label_Content
        {
            get { return (string)cLabel.LabelText; }
            set { cLabel.LabelText = value; }
        }
        #endregion

        #region Label_Width
        /// <summary>
        /// LabelのWidth
        /// </summary>
        [Category("デザイン")]
        public double Label_Width
        {
            set { cLabel.cWidth = value; }
            get { return cLabel.cWidth; }

        }
        #endregion

        #region Label_Visibility
        /// <summary>
        /// LabelのVisibility
        /// </summary>
        [Category("デザイン")]
        public Visibility Label_Visibility
        {
            set { cLabel.cVisibility = value; }
            get { return cLabel.cVisibility; }
        }
        #endregion


        #region TextBox Property


        /// <summary>
        /// AcceptsReturn 依存プロパティ
        /// Enter キーが押されたとき、テキスト編集コントロールがどのように反応するかを示す値を取得または設定します
        /// </summary>
        #region AcceptsReturn
        [Category("動作")]
        public bool TextAcceptsReturn
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
        public bool TextAcceptsTab
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
        public Brush TextBackground
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
        public HorizontalAlignment TextHorizontalAlignment
        {
            get { return cTextBox.cHorizontalAlignment; }
            set { cTextBox.cHorizontalAlignment = value; }
        }
        #endregion

        /// <summary>
        /// VerticalAlignment 
        /// この要素が、Panel またはアイテム コントロールのような親要素内に構成されるときに適用される水平方向の配置特性を取得または設定します
        /// </summary>
        #region VerticalAlignment
        [Category("デザイン")]
        public VerticalAlignment TextVerticalAlignment
        {
            get { return cTextBox.cVerticalAlignment; }
            set { cTextBox.cVerticalAlignment = value; }
        }
        #endregion


        /// <summary>
        /// HorizontalContentAlignment 
        /// コントロールのコンテンツの水平方向の配置を取得または設定します。 (Control から継承されます。) 
        /// </summary>
        #region HorizontalContentAlignment
        [Category("デザイン")]
        public HorizontalAlignment TextHorizontalContentAlignment
        {
            get { return this.cTextBox.cHorizontalContentAlignment; }
            set { this.cTextBox.cHorizontalContentAlignment = value; }
        }
        #endregion

        /// <summary>
        /// コントロールのコンテンツの垂直方向の配置を取得または設定します。
        /// </summary>
        #region VerticalContentAlignment
        [Category("デザイン")]
        public VerticalAlignment Text1VerticalContentAlignment
        {
            get { return this.cTextBox.cVerticalContentAlignment; }
            set { this.cTextBox.cVerticalContentAlignment = value; }
        }
        #endregion

        /// <summary>
        /// IsReadOnly 依存プロパティ
        /// テキスト編集コントロールを操作するユーザーに対して、コントロールが読み取り専用であるかどうかを示す値を取得または設定します
        /// </summary>
        #region
        [Category("動作")]
        public bool TextIsReadOnly
        {
            get { return cTextBox.cIsReadOnly; }
            set { cTextBox.cIsReadOnly = value; }
        }
        #endregion


        ///// <summary>
        ///// MaxLength 依存プロパティ
        ///// テキスト ボックスに手動で入力できる最大文字数を取得または設定します
        ///// </summary>
        //#region
        //[Category("動作")]
        //public Int32 TextMaxLength
        //{
        //	get { return cTextBox.cMaxLength; }
        //	set { cTextBox.cMaxLength = value; }
        //}
        //#endregion

        /// <summary>
        /// MinLines 依存プロパティ
        /// 表示行の最小数を取得または設定します。
        /// </summary>
        #region
        [Category("動作")]
        public Int32 TextMinLines
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
        public string TextSelectedText
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
        public Int32 TextSelectionStart
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
        public TextAlignment TextAlignment
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
        public TextWrapping TextWrapping
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
        public object TextToolTip
        {
            get { return cTextBox.cToolTip; }
            set { cTextBox.cToolTip = value; }
        }

        #endregion

        /// <summary>
        /// Visibility 依存プロパティ
        /// この要素の ユーザー インターフェイス (UI) 表現を取得または設定します。
        /// </summary>
        #region
        [Category("デザイン")]
        public Visibility TextVisibility
        {
            get { return cTextBox.cVisibility; }
            set { cTextBox.cVisibility = value; }
        }

        #endregion


        /// <summary>
        /// Width プロパティ
        /// 要素の幅を取得または設定します。
        /// </summary>
        #region
        [Category("動作")]
        public double TextWidth
        {
            get { return cTextBox.cWidth; }
            set { cTextBox.cWidth = value; }
        }

        #endregion

        #endregion

        private string _cGroupName = string.Empty;
        public string cGroupName
        {
            get { return this._cGroupName; }
            set
            {
                this._cGroupName = value;
                foreach (RadioButton rbtn in this._buttonList)
                {
                    rbtn.GroupName = value;
                }
            }
        }

        #region RadioOne_Content
        /// <summary>
        /// 1つ目のラジオボタンにCountを入力
        /// </summary>
        [Category("デザイン")]
        public string RadioOne_Content
        {
            get { return (string)cRadio1.Content; }
            set { cRadio1.Content = "0:" + value; SetupTextList(); }
        }
        #endregion

        #region RadioSecond_Content
        /// <summary>
        /// 2つ目のラジオボタンにCountを入力
        /// </summary>
        [Category("デザイン")]
        public string RadioSecond_Content
        {
            get { return (string)cRadio2.Content; }
            set { cRadio2.Content = "1:" + value; SetupTextList(); }
        }
        #endregion

        #region RadioThird_Content
        /// <summary>
        /// 3つ目のラジオボタンにCountを入力
        /// </summary>
        [Category("デザイン")]
        public string RadioThird_Content
        {
            get { return (string)cRadio3.Content; }
            set { cRadio3.Content = "2:" + value; SetupTextList(); }
        }
        #endregion

        #region RadioFourth_Content
        /// <summary>
        /// 4つ目のラジオボタンにCountを入力
        /// </summary>
        [Category("デザイン")]
        public string RadioFourth_Content
        {
            get { return (string)cRadio4.Content; }
            set { cRadio4.Content = "3:" + value; SetupTextList(); }
        }
        #endregion

        #region RadioFifth_Content
        /// <summary>
        /// 5つ目のラジオボタンにCountを入力
        /// </summary>
        [Category("デザイン")]
        public string RadioFifth_Content
        {
            get { return (string)cRadio5.Content; }
            set { cRadio5.Content = "4:" + value; SetupTextList(); }
        }
        #endregion

        #region RadioSixth_Content
        /// <summary>
        /// 6つ目のラジオボタンにCountを入力
        /// </summary>
        [Category("デザイン")]
        public string RadioSixth_Content
        {
            get { return (string)cRadio6.Content; }
            set { cRadio6.Content = "5:" + value; SetupTextList(); }
        }
        #endregion

        #region RadioSeventh_Content
        /// <summary>
        /// 7つ目のラジオボタンにCountを入力
        /// </summary>
        [Category("デザイン")]
        public string RadioSeventh_Content
        {
            get { return (string)cRadio7.Content; }
            set { cRadio7.Content = "6:" + value; SetupTextList(); }
        }
        #endregion

        #region RadioEighth_Content
        /// <summary>
        /// 8つ目のラジオボタンにCountを入力
        /// </summary>
        [Category("デザイン")]
        public string RadioEighth_Content
        {
            get { return (string)cRadio8.Content; }
            set { cRadio8.Content = "7:" + value; SetupTextList(); }
        }
        #endregion

        private void cTextBox_cTextChanged_1(object sender, RoutedEventArgs e)
        {
            switch (this.cTextBox.cText)
            {
                case "0":
                    this.cRadio1.IsChecked = true;
                    break;
                case "1":
                    this.cRadio2.IsChecked = true;
                    break;
                case "2":
                    this.cRadio3.IsChecked = true;
                    break;
                case "3":
                    this.cRadio4.IsChecked = true;
                    break;
                case "4":
                    this.cRadio5.IsChecked = true;
                    break;
                case "5":
                    this.cRadio6.IsChecked = true;
                    break;
                case "6":
                    this.cRadio7.IsChecked = true;
                    break;
                case "7":
                    this.cRadio8.IsChecked = true;
                    break;
                default:
                    foreach (RadioButton rbtn in this._buttonList)
                    {
                        rbtn.IsChecked = null;
                    }
                    break;
            }
        }

    }
}
