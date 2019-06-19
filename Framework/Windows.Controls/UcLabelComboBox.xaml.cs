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
using System.Data;

using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;


namespace KyoeiSystem.Framework.Windows.Controls
{
    /// <summary>
    /// UcLabelComboBox用データクラス
    /// </summary>
    public class CodeData : System.ComponentModel.INotifyPropertyChanged
    {
        //private string namearea;
        //private string function;
        //private string category;
        private int code;
        private int order;
        private string name;
        /// <summary>
        /// コード
        /// </summary>
        public int コード
        {
            get { return code; }
            set { code = value; NotifyPropertyChanged("コード"); }
        }
        /// <summary>
        /// 表示順
        /// </summary>
        public int 表示順
        {
            get { return order; }
            set { order = value; NotifyPropertyChanged("表示順"); }
        }
        /// <summary>
        /// 表示名
        /// </summary>
        public string 表示名
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged("表示名"); }
        }

        #region INotifyPropertyChanged メンバー
        /// <summary>
        /// Binding機能対応（プロパティの変更通知イベント）
        /// </summary>
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Binding機能対応（プロパティの変更通知イベント送信）
        /// </summary>
        /// <param name="propertyName">Bindingプロパティ名</param>
        public void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }

    /// <summary>
    /// UcLabelComboBox.xaml の相互作用ロジック
    /// </summary>
    public partial class UcLabelComboBox : FrameworkControl
    {
        delegate void ThreadMessageDelegate(CommunicationObject data);
        /// <summary>
        /// コンボボックス用アイテムプロパティ
        /// </summary>
        public static readonly DependencyProperty ComboboxItemsProperty = DependencyProperty.Register(
                "ComboboxItems",
                typeof(object),
                typeof(UcLabelComboBox),
                new FrameworkPropertyMetadata(
                        new PropertyChangedCallback(UcLabelComboBox.OnComboboxItemsChanged)
                )
            );
        private static void OnComboboxItemsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (obj.GetType() == typeof(UcLabelComboBox))
            {
                (obj as UcLabelComboBox).ComboboxItems = args.NewValue;
            }
        }

        /// <summary>
        /// コンボボックス用アイテム
        /// </summary>
        public object ComboboxItems
        {
            get { return GetValue(ComboboxItemsProperty); }
            set { SetValue(ComboboxItemsProperty, value); }
        }

        /// <summary>
        /// コンボボックス用テキストプロパティ
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(UcLabelComboBox),
            new UIPropertyMetadata(string.Empty)
        );

        /// <summary>
        /// コンボボックス用テキスト
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #region SelectedIndex

        /// <summary>
        /// コンボボックス用選択アイテムインデックス値プロパティ
        /// </summary>
        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(
                "SelectedIndex",
                typeof(int),
                typeof(UcLabelComboBox),
                new FrameworkPropertyMetadata(
                        new PropertyChangedCallback(UcLabelComboBox.OnSelectedIndexPropertyChanged)
                )
            );
        private static void OnSelectedIndexPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (obj.GetType() == typeof(UcLabelComboBox))
            {
                (obj as UcLabelComboBox).SelectedIndex = (int)args.NewValue;
            }
        }

        /// <summary>
        /// コンボボックス用選択アイテムインデックス値
        /// </summary>
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); this.Combo_SelectedIndex = value; NotifyPropertyChanged(); }

        }

        #endregion

        #region SelectedValue

        /// <summary>
        /// コンボボックス用選択アイテム値プロパティ
        /// </summary>
        public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.Register(
                "SelectedValue",
                typeof(object),
                typeof(UcLabelComboBox),
                new FrameworkPropertyMetadata(
                        new PropertyChangedCallback(UcLabelComboBox.OnSelectedValuePropertyChanged)
                )
            );
        private static void OnSelectedValuePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (obj.GetType() == typeof(UcLabelComboBox))
            {
                (obj as UcLabelComboBox).SelectedValue = args.NewValue;
            }
        }

        /// <summary>
        /// コンボボックス用選択アイテム値
        /// </summary>
        public object SelectedValue
        {
            get { return GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); this.Combo_SelectedValue = value; NotifyPropertyChanged(); }

        }

        #endregion

        /// <summary>
        /// コンボボックスデータアイテム初期化完了イベント
        /// </summary>
        public static readonly RoutedEvent DataListInitializedEvent = EventManager.RegisterRoutedEvent(
            "DataListInitialized", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UcLabelComboBox)
            );

        /// <summary>
        /// コンボボックスデータアイテム初期化完了イベントハンドラー
        /// </summary>
        public event RoutedEventHandler DataListInitialized
        {
            add { AddHandler(DataListInitializedEvent, value); }
            remove { RemoveHandler(DataListInitializedEvent, value); }
        }


        private bool isWaitForLoaded = false;
        //private ThreadManeger thmgr = null;
        //public DataAccessConfig Daccfg
        //{
        //	get
        //	{
        //		if (this.thmgr == null)
        //		{
        //			return null;
        //		}
        //		return this.thmgr.daccfg;

        //	}
        //	set
        //	{
        //		if (this.thmgr != null)
        //		{
        //			this.thmgr.daccfg = value;
        //		}
        //	}
        //}

        List<CodeData> _codelist = new List<CodeData>();
        /// <summary>
        /// コードデータ一覧
        /// </summary>
        public List<CodeData> CodeList
        {
            get { return _codelist; }
            set { _codelist = value; NotifyPropertyChanged("CodeList"); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UcLabelComboBox()
        {
            InitializeComponent();
            //this.Loaded += UcLabelComboBox_Loaded;
        }

        /// <summary>
        /// コンボボックス選択変更イベント
        /// </summary>
        public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent(
            "SelectionChanged", RoutingStrategy.Bubble, typeof(SelectionChangedEventHandler), typeof(UcLabelComboBox));

        /// <summary>
        /// コンボボックス選択変更イベントハンドラー
        /// </summary>
        public event SelectionChangedEventHandler SelectionChanged
        {
            add { AddHandler(SelectionChangedEvent, value); }
            remove { RemoveHandler(SelectionChangedEvent, value); }
        }

        private void cCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Text = this.cComboBox.Text;
            RaiseEvent(new SelectionChangedEventArgs(SelectionChangedEvent, e.RemovedItems, e.AddedItems));
        }

        /// <summary>
        /// コンボボックスにフォーカスを設定する
        /// </summary>
        public new void Focus()
        {
            this.cComboBox.Focus();
        }

        /// <summary>
        /// データアクセスクラスを介してコンボボックス用コードデータ取得を要求する
        /// </summary>
        public void GetComboboxList()
        {
            try
            {
                if (appLog != null) { appLog.Debug("<UC> UcLabelComboBox_Loaded"); }
                if (this.ComboListingParams.StartsWith("@") == true)
                {
                    this.Combo_DisplayMemberPath = "表示名";
                    this.Combo_SelectedValuePath = "コード";
                    this.ComboboxItems = this.CodeList;
                    if (isWaitForLoaded)
                    {
                        isWaitForLoaded = false;
                    }
                    var com = new CommunicationObject(MessageType.RequestData, "UcCOMBO", this.ComboListingParams);
                    com.connection = this.ConnectStringUserDB;
                    this.thmgr.SendRequest(com);

                }
            }
            catch (Exception)
            {
                thmgr = null;
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (thmgr != null)
            {
                thmgr.OnReceived += new MessageReceiveHandler(OnReceived);
                //try
                //{
                //	thmgr = new ThreadManeger(base.viewsCommData.DacConf, base.appLog);
                //}
                //catch (Exception)
                //{
                //	return;
                //}
            }
        }

        ///// <summary>
        ///// コントロールが破棄されるときのイベント
        ///// </summary>
        //public override void OnUnload()
        //{
        //	base.OnUnload();
        //	//this.Loaded -= UcLabelComboBox_Loaded;
        //	if (appLog != null) { appLog.Debug("<UC> UcUnloading"); }
        //	if (thmgr == null)
        //	{
        //		return;
        //	}
        //	thmgr.OnReceived -= new MessageReceiveHandler(OnReceived);
        //	thmgr.Dispose();
        //	thmgr = null;
        //}

        private void OnReceived(CommunicationObject message)
        {
            if (appLog != null) { base.appLog.Debug("<UC> {0}:受信({1})", this.GetType().Name, message.mType); }
            switch (message.mType)
            {
                case MessageType.ResponseData:
                    Dispatcher.Invoke(new ThreadMessageDelegate(setReceivedData), message);
                    break;
                case MessageType.Error:
                    Dispatcher.Invoke(new ThreadMessageDelegate(setErrorProc), message);
                    break;
            }
        }

        void setErrorProc(CommunicationObject message)
        {
            this.CodeList.Clear();
            this.SelectedIndex = -1;
            RaiseEvent(new RoutedEventArgs(UcLabelComboBox.DataListInitializedEvent));
        }

        /// <summary>
        /// 取得データの取り込み
        /// </summary>
        /// <param name="message"></param>
        void setReceivedData(CommunicationObject message)
        {
            try
            {
                this.CodeList.Clear();
                var data = message.GetResultData();
                DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
                if (tbl != null)
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        CodeData cdt = new CodeData() { コード = (int)row["コード"], 表示順 = (int)row["表示順"], 表示名 = (string)row["表示名"] };
                        this.CodeList.Add(cdt);
                    }
                }
                this.SelectedIndex = 0;
            }
            catch (Exception)
            {
                // マスターからの取得エラー
                this.SelectedIndex = -1;
            }
            RaiseEvent(new RoutedEventArgs(UcLabelComboBox.DataListInitializedEvent));

        }



        #region cItemsSource
        ///// <summary>
        ///// 表示させるテーブル
        ///// </summary>
        ////[Category("動作")]
        //public DataTable Combo_ItemsSource
        //{
        //	get { return (this.cComboBox.ItemsSource is DataTable) ? (DataTable)this.cComboBox.ItemsSource : null; }
        //	set { this.cComboBox.ItemsSource = ((IListSource)value).GetList(); }		
        //}
        #endregion

        /// <summary>
        /// データテーブルをソートする
        /// </summary>
        /// <param name="TargetTable">対象テーブル</param>
        /// <param name="TargetColumn">対象カラム</param>
        /// <returns>ソート後のテーブル</returns>
        public DataTable TableSort(DataTable TargetTable, string TargetColumn)
        {
            DataTable MakeTable = TargetTable.Clone();

            //DataTableにソートメソッドが存在しないためViewにしてソートする。
            DataView dv = new DataView(TargetTable);
            dv.Sort = TargetColumn;

            foreach (DataRowView drv in dv)
            {
                MakeTable.ImportRow(drv.Row);
            }

            return MakeTable;
        }
        /// <summary>
        /// 値検証を行う
        /// </summary>
        /// <returns></returns>
        public override bool CheckValidation()
        {
            if (string.IsNullOrEmpty(this.cComboBox.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #region ComboBox Property

        private string _comboListingParams = string.Empty;
        /// <summary>
        /// コンボボックスのコードデータをデータアクセスを介して取得する場合の要求パラメータ
        /// </summary>
        public string ComboListingParams
        {
            set { this._comboListingParams = value; }
            get { return this._comboListingParams; }
        }

        #region Background
        /// <summary>
        /// コントロールの背景を表すブラシを取得または設定します。 (Control から継承されます。) 
        /// </summary>
        [Category("ブラシ")]
        public Brush Combo_Background
        {
            get { return cComboBox.Background; }
            set { cComboBox.Background = value; }

        }
        #endregion

        #region DataContext
        /// <summary>
        /// データ バインディングに参加すると要素のデータ コンテキストを取得または設定します。 
        /// </summary>
        [Category("動作")]
        public object Combo_DataContext
        {
            get { return cComboBox.DataContext; }
            set { cComboBox.DataContext = value; }

        }
        #endregion

        #region Dispatcher
        /// <summary>
        /// この DispatcherObject が関連付けられている Dispatcher を取得します。
        /// </summary>
        [Category("動作")]
        public System.Windows.Threading.Dispatcher Combo_Dispatcher
        {
            get { return cComboBox.Dispatcher; }
            set { }

        }
        #endregion

        #region DisplayMemberPath
        /// <summary>
        /// ソース オブジェクトの値にパスをオブジェクトのビジュアル表現として実行するように取得または設定します
        /// </summary>
        [Category("動作")]
        public string Combo_DisplayMemberPath
        {
            set { cComboBox.DisplayMemberPath = value; }
            get { return cComboBox.DisplayMemberPath; }
        }
        #endregion

        #region Effect
        /// <summary>
        /// ビットマップ効果を UIElementに適用する取得または設定します。
        /// </summary>
        [Category("動作")]
        public System.Windows.Media.Effects.Effect Combo_Effect
        {
            get { return cComboBox.Effect; }
            set { cComboBox.Effect = value; }

        }
        #endregion

        #region FontFamily
        /// <summary>
        /// コントロールのフォント ファミリを取得または設定します。
        /// </summary>
        [Category("動作")]
        public System.Windows.Media.FontFamily Combo_FontFamily
        {
            get { return cComboBox.FontFamily; }
            set { cComboBox.FontFamily = value; }

        }
        #endregion

        #region FontSize
        /// <summary>
        /// フォント サイズを取得または設定します。
        /// </summary>
        [Category("動作")]
        public double Combo_FontSize
        {
            get { return cComboBox.FontSize; }
            set { cComboBox.FontSize = value; }

        }
        #endregion

        #region FontStretch
        /// <summary>
        /// 画面上でフォントを縮小または拡大する度合いを取得または設定します。
        /// </summary>
        [Category("動作")]
        public FontStretch Combo_FontStretch
        {
            get { return cComboBox.FontStretch; }
            set { cComboBox.FontStretch = value; }

        }
        #endregion

        #region FontStyle
        /// <summary>
        /// フォント スタイルを取得または設定します
        /// </summary>
        [Category("動作")]
        public FontStyle Combo_FontStyle
        {
            get { return cComboBox.FontStyle; }
            set { cComboBox.FontStyle = value; }

        }
        #endregion

        #region FontWeight
        /// <summary>
        /// 指定したフォントの太さを取得または設定します
        /// </summary>
        [Category("動作")]
        public FontWeight Combo_FontWeight
        {
            get { return cComboBox.FontWeight; }
            set { cComboBox.FontWeight = value; }

        }
        #endregion

        #region Foreground
        /// <summary>
        /// 前景色を表すブラシを取得または設定します
        /// </summary>
        [Category("動作")]
        public Brush Combo_Foreground
        {
            get { return cComboBox.Foreground; }
            set { cComboBox.Foreground = value; }

        }
        #endregion

        #region HasItems
        /// <summary>
        /// ItemsControl は、項目が含まれているかどうかを示す値を取得します
        /// </summary>
        [Category("動作")]
        public bool Combo_HasItems
        {
            get { return cComboBox.HasItems; }
            set { }

        }
        #endregion

        #region Height
        /// <summary>
        /// 要素の提案された高さを取得または設定します
        /// </summary>
        [Category("動作")]
        public double Combo_Height
        {
            get { return cComboBox.Height; }
            set { cComboBox.Height = value; }

        }
        #endregion

        #region IsEditable
        /// <summary>
        /// ComboBox のテキスト ボックス内のテキストの編集を有効または無効にする値を取得または設定します。
        /// </summary>
        [Category("動作")]
        public bool Combo_IsEditable
        {
            get { return cComboBox.IsEditable; }
            set { cComboBox.IsEditable = value; }

        }
        #endregion

        #region IsEnabled
        /// <summary>
        /// この要素が ユーザー インターフェイス (UI)で有効になっているかどうかを示す値を取得または設定します。
        /// </summary>
        [Category("動作")]
        public bool Combo_IsEnabled
        {
            get { return cComboBox.IsEnabled; }
            set { cComboBox.IsEnabled = value; }

        }
        #endregion

        #region IsFocused
        /// <summary>
        /// この要素に論理フォーカスがあるかどうかを示す値を取得します。これは 依存関係プロパティです。
        /// </summary>
        [Category("動作")]
        public bool Combo_IsFocused
        {
            get { return cComboBox.IsFocused; }
            set { }

        }
        #endregion

        #region IsReadOnly
        /// <summary>
        /// 選択専用モードを有効にする値を取得または設定します。選択専用モードでは、コンボ ボックスの内容は選択可能ですが、編集することはできません。
        /// </summary>
        [Category("動作")]
        public bool Combo_IsReadOnly
        {
            get { return cComboBox.IsReadOnly; }
            set { cComboBox.IsReadOnly = value; }

        }
        #endregion

        #region IsTabStop
        /// <summary>
        /// コントロールがタブ ナビゲーションに含まれるかどうかを示す値を取得または設定します。
        /// </summary>
        [Category("動作")]
        public bool Combo_IsTabStop
        {
            get { return cComboBox.IsTabStop; }
            set { cComboBox.IsTabStop = value; }

        }
        #endregion

        #region Margin
        /// <summary>
        /// 要素の前辺を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public Thickness Combo_Margin
        {
            get { return cComboBox.Margin; }
            set { cComboBox.Margin = value; }

        }
        #endregion

        #region MaxDropDownHeight
        /// <summary>
        /// コンボ ボックス ドロップダウンの最大の高さを取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public double Combo_MaxDropDownHeight
        {
            get { return cComboBox.MaxDropDownHeight; }
            set { cComboBox.MaxDropDownHeight = value; }

        }
        #endregion

        #region MaxHeight
        /// <summary>
        /// 要素の高さの最小値を取得または設定します。
        /// </summary>
        [Category("デザイン")]
        public double Combo_MaxHeight
        {
            get { return cComboBox.MaxHeight; }
            set { cComboBox.MaxHeight = value; }

        }
        #endregion

        #region MaxWidth
        /// <summary>
        /// 要素の幅の最大値を取得または設定します。 
        /// </summary>
        [Category("デザイン")]
        public double Combo_MaxWidth
        {
            get { return cComboBox.MaxWidth; }
            set { cComboBox.MaxWidth = value; }

        }
        #endregion

        #region MinHeight
        /// <summary>
        /// 要素の高さの最大値を取得または設定します
        /// </summary>
        [Category("デザイン")]
        public double Combo_MinHeight
        {
            get { return cComboBox.MinHeight; }
            set { cComboBox.MinHeight = value; }

        }
        #endregion

        #region MinWidth
        /// <summary>
        /// 要素の幅の最小値を取得または設定します 
        /// </summary>
        [Category("デザイン")]
        public double Combo_MinWidth
        {
            get { return cComboBox.MinWidth; }
            set { cComboBox.MinWidth = value; }

        }
        #endregion

        #region Padding
        /// <summary>
        /// コントロール内のスペースを取得または設定します。
        /// </summary>
        [Category("動作")]
        public Thickness Combo_Padding
        {
            get { return cComboBox.Padding; }
            set { cComboBox.Padding = value; }

        }
        #endregion

        #region Parent
        /// <summary>
        /// この要素の logical parent の要素を取得します。
        /// </summary>
        [Category("動作")]
        public DependencyObject Combo_Parent
        {
            get { return cComboBox.Parent; }
            set { }

        }
        #endregion

        #region SelectedIndex
        /// <summary>
        /// 現在選択されている最初の項目のインデックスを取得または設定します。選択範囲が空の場合は -1 を返します。
        /// </summary>
        [Category("動作")]
        public int Combo_SelectedIndex
        {
            get { return cComboBox.SelectedIndex; }
            set { cComboBox.SelectedIndex = value; }

        }
        #endregion

        #region SelectedItem
        /// <summary>
        /// 現在選択されている最初の項目を取得または設定します。選択範囲が空の場合は null を返します 
        /// </summary>
        [Category("動作")]
        public object Combo_SelectedItem
        {
            get { return cComboBox.SelectedItem; }
            set { cComboBox.SelectedItem = value; }

        }
        #endregion

        #region SelectedValue
        /// <summary>
        /// SelectedValuePath を使用して取得される SelectedItem の値を取得または設定します
        /// </summary>
        [Category("動作")]
        public object Combo_SelectedValue
        {
            get { return cComboBox.SelectedValue; }
            set { cComboBox.SelectedValue = value; }

        }
        #endregion

        #region SelectedValuePath
        /// <summary>
        /// SelectedItem から SelectedValue を取得するために使用するパスを取得または設定します。
        /// </summary>
        [Category("動作")]
        public string Combo_SelectedValuePath
        {
            get { return cComboBox.SelectedValuePath; }
            set { cComboBox.SelectedValuePath = value; }

        }
        #endregion

        #region TabIndex
        /// <summary>
        /// ユーザーが Tab キーを使用してコントロール間を移動するときに、要素がフォーカスを受け取る順序を決定する値を取得または設定します
        /// </summary>
        [Category("動作")]
        public int Combo_TabIndex
        {
            get { return cComboBox.TabIndex; }
            set { cComboBox.TabIndex = value; }

        }
        #endregion

        #region Text
        /// <summary>
        /// 現在選択されている項目のテキストを取得または設定します。
        /// </summary>
        [Category("動作")]
        public string Combo_Text
        {
            get { return cComboBox.Text; }
            set { cComboBox.Text = value; }

        }
        #endregion

        #region ToolTip
        /// <summary>
        /// ユーザー インターフェイス (UI)のこの要素に対して表示されるツール ヒントのオブジェクトを取得または設定します
        /// </summary>
        [Category("動作")]
        public object Combo_ToolTip
        {
            get { return cComboBox.ToolTip; }
            set { cComboBox.ToolTip = value; }

        }
        #endregion

        #region Width
        /// <summary>
        /// 要素の幅を取得または設定します。
        /// </summary>
        [Category("動作")]
        public double Combo_Width
        {
            get { return cComboBox.Width; }
            set { cComboBox.Width = value; }

        }
        #endregion

        #endregion

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

        #region Context
        /// <summary>
        /// ContentControl のコンテンツを取得または設定します。
        /// </summary>
        [Category("レイアウト")]
        public object Label_Context
        {
            get { return this.cLabel.cContent; }
            set { this.cLabel.cContent = value; }
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

        #region Visibility
        /// <summary>
        /// この要素が ユーザー インターフェイス (UI)に表示されるかどうかを示す値を取得します。
        /// これは 依存関係プロパティです。
        /// </summary>
        [Category("動作")]
        public Visibility Label_Visibility
        {
            set { cLabel.Visibility = value; }
            get { return cLabel.Visibility; }
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
            get { return this.cLabel.Padding; }
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

        private void Combobox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FocusControl.SetFocusWithOrder(new TraversalRequest(FocusNavigationDirection.Next));
                UIElement element = Keyboard.FocusedElement as UIElement;
                e.Handled = true;
            }

        }

    }
}
