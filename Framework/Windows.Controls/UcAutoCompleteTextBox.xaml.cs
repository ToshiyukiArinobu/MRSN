using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace KyoeiSystem.Framework.Windows.Controls
{
	/// <summary>
	/// USER32API用クラス
	/// </summary>
    public class User32
    {
		/// <summary>
		/// マウス移動制御クラス
		/// </summary>
        public class MouseMove
        {
            [DllImport("User32.dll")]
            private static extern bool SetCursorPos(int X, int Y);

            /// <summary>
            /// マウスカーソルの位置を設定します。
            /// </summary>
            /// <param name="a">X 座標を指定します。</param>
            /// <param name="b">Y 座標を指定します。</param>
            public static void SetPosition(int a, int b)
            {
                SetCursorPos(a, b);
            }
        }
    }


	#region オートコンプリート専用データクラス
	/// <summary>
	/// オートコンプリート専用データクラス
	/// </summary>
	public class ComboBoxItemData : INotifyPropertyChanged
	{
		private string key = string.Empty;
		private string[] _columns = null;
		private DataRow val = null;
		private List<int> _displayColWidth = new List<int>(5) { 0, 0, 0, 0, 0 };
		private List<HorizontalAlignment> _displayColHorizontalAlignment = new List<HorizontalAlignment>(5) { HorizontalAlignment.Left, HorizontalAlignment.Left, HorizontalAlignment.Left, HorizontalAlignment.Left, HorizontalAlignment.Left, };
		private List<Visibility> _colVisibility = new List<Visibility>(5) { Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed };
		/// <summary>
		/// 表示テキストを取得または設定します。
		/// </summary>
		public string DisplayText
		{
			get { return this.key; }
			set { this.key = value; }
		}
		/// <summary>
		/// カラム名を取得または設定します。
		/// </summary>
		public string[] columns
		{
			get { return this._columns; }
			set { this._columns = value; }
		}
		private List<object> _columnValues = new List<object>(5) { null, null, null, null, null, };
		/// <summary>
		/// カラム値を取得または設定します。
		/// </summary>
		public List<object> ColumnValues
		{
			get { return this._columnValues; }
			set { this._columnValues = value; NotifyPropertyChanged(); }
		}
		/// <summary>
		/// カラム表示状態を取得または設定します。
		/// </summary>
		public List<Visibility> ColVisibility
		{
			get { return this._colVisibility; }
			set { this._colVisibility = value; NotifyPropertyChanged(); }
		}
		/// <summary>
		/// 表示対象カラム名を取得または設定します。カラムを複数指定する場合は"|"で区切ります。
		/// </summary>
		public string DisplayColumns
		{
			get
			{
				if (columns == null)
					return string.Empty;
				string val = string.Empty;
				foreach (var str in columns)
				{
					val += str + "|";
				}
				return val.TrimEnd('|');
			}
			set
			{
				columns = value.Split('|');
				for (int i = 0; i < columns.Length; i++)
				{
					ColVisibility[i] = Visibility.Visible;
				}
				NotifyPropertyChanged();
			}
		}

		/// <summary>
		/// カラム幅を取得または設定します。
		/// </summary>
		public List<int> DisplayColWidth
		{
			get { return this._displayColWidth; }
			set { this._displayColWidth = value; NotifyPropertyChanged(); }
		}

		/// <summary>
		/// 各カラムの横位置を取得または設定します。
		/// </summary>
		public List<HorizontalAlignment> DisplayColHorizontalAlignment
		{
			//(DayOfWeek)Enum.Parse(typeof(DayOfWeek), str);
			get { return this._displayColHorizontalAlignment; }
			set { this._displayColHorizontalAlignment = value; NotifyPropertyChanged(); }
		}

		/// <summary>
		/// DataRow型で全カラムの値を取得または設定します。
		/// </summary>
		public DataRow Value
		{
			get { return this.val; }
			set
			{
				this.val = value;
				int ix = 0;
				if (columns != null)
				{
					foreach (var col in columns)
					{
						ColumnValues[ix++] = value[col];
					}
				}
			}
		}

		#region INotifyPropertyChanged メンバー
		public event PropertyChangedEventHandler PropertyChanged;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="propertyName"></param>
		public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		#endregion
	}
	#endregion

	/// <summary>
	/// UcAutoCompleteTextBoxの実装クラス
	/// </summary>
	public partial class UcAutoCompleteTextBox : FrameworkControl
	{
		#region Members

		private delegate void TextChangedCallback();
        private SearchModeTypes searchMode = SearchModeTypes.PARTIAL_MATCH;

		#endregion

		#region Constructor
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public UcAutoCompleteTextBox()
		{
			InitializeComponent();

			comboBox.ItemsSource = this.ComboboxItemList;
		}

		#endregion


		#region イベント
		/// <summary>
		/// 値が選択されたときに発生するイベント
		/// </summary>
		public static readonly RoutedEvent ValueSelectedEvent = EventManager.RegisterRoutedEvent(
			"ValueSelected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UcAutoCompleteTextBox)
			);

		/// <summary>
		/// 値が選択されたときに発生するイベントを取得または設定します。
		/// </summary>
		public event RoutedEventHandler ValueSelected
		{
			add { AddHandler(ValueSelectedEvent, value); }
			remove { RemoveHandler(ValueSelectedEvent, value); }
		}

		#endregion

		#region プロパティ
		/// <summary>
		/// 現在の文字列
		/// </summary>
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
				"Text",
				typeof(string),
				typeof(UcAutoCompleteTextBox),
				new UIPropertyMetadata(string.Empty)
			);

		/// <summary>
		/// 現在の文字列を取得または設定します。
		/// </summary>
		[BindableAttribute(true)]
		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set
			{
				SetValue(TextProperty, value);
				if (string.IsNullOrWhiteSpace(value) != true)
				{
					var rows = (from rec in this.allItems where rec.Value[this.DisplayMemberName].ToString() == value select rec.Value).ToArray();
					if (rows.Count() == 1)
					{
						this.SelectedItem = rows[0];
					}
					else
					{
						this.SelectedItem = null;
					}
				}
				else
				{
					this.SelectedItem = null;
				}
				this.CheckValidation();
			}
		}

		//public static readonly DependencyProperty BranchFromProperty = DependencyProperty.Register(
		//		"BranchFrom",
		//		typeof(string),
		//		typeof(UcAutoCompleteTextBox),
		//		new UIPropertyMetadata(string.Empty)
		//	);

		//[BindableAttribute(true)]
		//public string BranchFrom
		//{
		//	get { return (string)GetValue(BranchFromProperty); }
		//	set
		//	{
		//		SetValue(BranchFromProperty, value);
		//	}
		//}
		//public static readonly DependencyProperty BranchToProperty = DependencyProperty.Register(
		//		"BranchTo",
		//		typeof(string),
		//		typeof(UcAutoCompleteTextBox),
		//		new UIPropertyMetadata(string.Empty)
		//	);

		//[BindableAttribute(true)]
		//public string BranchTo
		//{
		//	get { return (string)GetValue(BranchToProperty); }
		//	set
		//	{
		//		SetValue(BranchToProperty, value);
		//	}
		//}

		//public static readonly DependencyProperty Flag1Property = DependencyProperty.Register(
		//		"Flag1",
		//		typeof(bool),
		//		typeof(UcAutoCompleteTextBox),
		//		new UIPropertyMetadata(true)
		//	);
		//[BindableAttribute(true)]
		//public bool Flag1
		//{
		//	get { return (bool)GetValue(Flag1Property); }
		//	set
		//	{
		//		SetValue(Flag1Property, value);
		//	}
		//}

		//public static readonly DependencyProperty Flag2Property = DependencyProperty.Register(
		//		"Flag2",
		//		typeof(bool),
		//		typeof(UcAutoCompleteTextBox),
		//		new UIPropertyMetadata(true)
		//	);
		//[BindableAttribute(true)]
		//public bool Flag2
		//{
		//	get { return (bool)GetValue(Flag2Property); }
		//	set
		//	{
		//		SetValue(Flag2Property, value);
		//	}
		//}

		# region AutoSelectOnMouseClickプロパティ
		private OnOff _autoSelectOnMouseClick = OnOff.On;
		[Category("動作")]
		public OnOff AutoSelectOnMouseClick
		{
			get { return this._autoSelectOnMouseClick; }
			set
			{
				this._autoSelectOnMouseClick = value;
			}
		}
		#endregion

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

		private bool _isDropDownOnFocus = false;
		/// <summary>
		/// 設定値を無視するようになりました。規定値は false のみです。
		/// </summary>
		[Category("廃止されたプロパティ")]
		public bool IsDropDownOnFocus
		{
			get
			{
				return this._isDropDownOnFocus;
			}
			set
			{
				this._isDropDownOnFocus = value;
			}
		}

		public List<ComboBoxItemData> allItems_E = new List<ComboBoxItemData>();
		public List<ComboBoxItemData> allItems = new List<ComboBoxItemData>();
		public List<ComboBoxItemData> comboboxItems = null;
		public List<ComboBoxItemData> ComboboxItemList
		{
			get { return this.comboboxItems; }
			set { this.comboboxItems = value; }
		}

		// 以下のプロパティは廃止
		//private string _branchColName = null;
		//public string BranchColName
		//{
		//	get { return this._branchColName; }
		//	set { this._branchColName = value; SetupList(); }
		//}

		//private string _flag1ColName = null;
		//public string Flag1ColName
		//{
		//	get { return this._flag1ColName; }
		//	set { this._flag1ColName = value; SetupList(); }
		//}
		//private string _flag2ColName = null;
		//public string Flag2ColName
		//{
		//	get { return this._flag2ColName; }
		//	set { this._flag2ColName = value; SetupList(); }
		//}

		public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
				"Items",
				typeof(DataTable),
				typeof(UcAutoCompleteTextBox),
				new UIPropertyMetadata(null)
			);

		public DataTable Items
		{
			get { return (DataTable)GetValue(ItemsProperty); }
			set
			{
				SetValue(ItemsProperty,value);
				allItems_E.Clear();
				foreach (DataRow row in value.Rows)
				{
					allItems_E.Add(new ComboBoxItemData() { DisplayText = row[DisplayMemberName].ToString(), DisplayColumns = this.DisplayColumns, DisplayColWidth = this._displayColWidth, DisplayColHorizontalAlignment = this._displayColHorizontalAlignment, Value = row });
				}
				SetupList();
				if (this.cTextBox.IsFocused)
				{
					this.SelectedItem = null;
					this.TextChanged();
				}
			}
		}

		public void SetupList()
		{
			IQueryable<ComboBoxItemData> query;
			//if (string.IsNullOrWhiteSpace(BranchColName))
			{
				query = (from r in allItems_E
						 select r).AsQueryable();
			}
			//else
			//{
			//	int wk;
			//	int efrom, eto;
			//	string[] strs;
			//	if (string.IsNullOrWhiteSpace(this.BranchFrom))
			//	{
			//		efrom = -1;
			//	}
			//	else
			//	{
			//		strs = this.BranchFrom.Split(' ');
			//		efrom = int.TryParse(strs[0], out wk) ? wk : -1;
			//	}
			//	if (string.IsNullOrWhiteSpace(this.BranchTo))
			//	{
			//		eto = -1;
			//	}
			//	else
			//	{
			//		strs = this.BranchTo.Split(' ');
			//		eto = int.TryParse(strs[0], out wk) ? wk : -1;
			//	}
			//	if (efrom < 0)
			//		efrom = eto;
			//	if (eto < 0)
			//		eto = efrom;
			//	if (efrom > eto)
			//	{
			//		wk = eto;
			//		eto = efrom;
			//		efrom = wk;
			//	}
			//	if (efrom == 0 || eto == 0)
			//	{
			//		query = (from r in allItems_E
			//				 select r).AsQueryable();
			//	}
			//	else
			//	{
			//		query = (from r in allItems_E
			//				 where (int)r.Value[BranchColName] >= efrom
			//					&& (int)r.Value[BranchColName] <= eto
			//				 select r).AsQueryable();
			//	}
			//}
			//if ((string.IsNullOrWhiteSpace(Flag1ColName) != true) && (string.IsNullOrWhiteSpace(Flag2ColName) != true))
			//{
			//	if (Flag1 && Flag2)
			//	{
			//		// 結局全件になるのでフラグでは絞らない
			//	}
			//	else if (Flag1 && !Flag2)
			//	{
			//		query = (from r in query
			//				 where (bool)r.Value[Flag1ColName] == Flag1
			//				 select r).AsQueryable();
			//	}
			//	else if (!Flag1 && Flag2)
			//	{
			//		query = (from r in query
			//				 where (bool)r.Value[Flag2ColName] == Flag2
			//				 select r).AsQueryable();
			//	}
			//	else
			//	{
			//		query = (from r in query
			//				 where (bool)r.Value[Flag1ColName] == Flag1
			//					&& (bool)r.Value[Flag2ColName] == Flag2
			//				 select r).AsQueryable();
			//	}
			//}
			//else if ((string.IsNullOrWhiteSpace(Flag1ColName) != true) && (string.IsNullOrWhiteSpace(Flag2ColName) == true))
			//{
			//	query = (from r in query
			//			 where (bool)r.Value[Flag1ColName] == Flag1
			//			 select r).AsQueryable();
			//}
			//else if ((string.IsNullOrWhiteSpace(Flag1ColName) == true) && (string.IsNullOrWhiteSpace(Flag2ColName) != true))
			//{
			//	query = (from r in query
			//			 where (bool)r.Value[Flag2ColName] == Flag2
			//			 select r).AsQueryable();
			//}
			//else
			//{
			//	// フラグでは絞らない
			//}

			allItems.Clear();
			comboBox.Items.Clear();
			bool isexist = false;
			foreach (var row in query)
			{
				var p = (from r in allItems where r.DisplayText == row.DisplayText select r.DisplayText).Count();
				if (p > 0)
				{
					continue;
				}
				allItems.Add(new ComboBoxItemData() { DisplayText = row.DisplayText, DisplayColumns = row.DisplayColumns, DisplayColWidth = row.DisplayColWidth, DisplayColHorizontalAlignment = row.DisplayColHorizontalAlignment, Value = row.Value });
				comboBox.Items.Add(new ComboBoxItemData() { DisplayText = row.DisplayText, DisplayColumns = row.DisplayColumns, DisplayColWidth = row.DisplayColWidth, DisplayColHorizontalAlignment = row.DisplayColHorizontalAlignment, Value = row.Value });
				if (this.SelectedItem != null)
				{
					if (this.SelectedItem.Equals(row.Value))
					{
						isexist = true;
					}
				}
			}
			if (isexist != true)
			{
				this.SelectedItem = null;
				this.Text = string.Empty;
			}
		}

		private string _selectColName= null;
		public string SelectColName
		{
			get { return this._selectColName; }
			set { this._selectColName = value; }
		}
		private string _displayColumns = null;
		public string DisplayColumns
		{
			get { return this._displayColumns; }
			set { this._displayColumns = value; }
		}

		// エラーメッセージ
		private string _messageOnNotFound = "一致する項目がありません。";
		private string _messageOnDuplicate = "指定が不完全です。";
		//private string _messageOnNotSelected = "指定が不完全です。";
		public string MessageOnNotFound
		{
			get { return this._messageOnNotFound; }
			set { this._messageOnNotFound = value; }
		}
		public string MessageOnDuplicate
		{
			get { return this._messageOnDuplicate; }
			set { this._messageOnDuplicate = value; }
		}
		//public string MessageOnNotSelected
		//{
		//	get { return this._messageOnNotSelected; }
		//	set { this._messageOnNotSelected = value; }
		//}

		private List<int> _displayColWidth = new List<int>(5) { 0, 0, 0, 0, 0 };
		public string DisplayColWidth
		{
			get
			{
				string val = string.Empty;
				foreach (var item in _displayColWidth)
				{
					val += item.ToString() + ",";
				}
				return val.TrimEnd(',');
			}
			set
			{
				string[] vals = value.Split(',');
				int cnt = 0;
				foreach (var item in vals)
				{
					int wk = 0;
					_displayColWidth[cnt] = int.TryParse(item, out wk) ? wk : 0;
					cnt++;
					if (cnt >= _displayColWidth.Capacity)
					{
						break;
					}
				}
			}
		}

		private List<HorizontalAlignment> _displayColHorizontalAlignment
			= new List<HorizontalAlignment>(5) {
				HorizontalAlignment.Left,
				HorizontalAlignment.Left,
				HorizontalAlignment.Left,
				HorizontalAlignment.Left,
				HorizontalAlignment.Left,
			};
		public string DisplayColHorizontalAlignment
		{
			//(DayOfWeek)Enum.Parse(typeof(DayOfWeek), str);
			get
			{
				string val = string.Empty;
				foreach (var item in _displayColWidth)
				{
					val += item.ToString() + ",";
				}
				return val.TrimEnd(',');
			}
			set
			{
				string[] vals = value.Split(',');
				int cnt = 0;
				foreach (var item in vals)
				{
					HorizontalAlignment wk = HorizontalAlignment.Left;
					_displayColHorizontalAlignment[cnt] = Enum.TryParse(item, out wk) ? wk : HorizontalAlignment.Right;
					cnt++;
					if (cnt >= _displayColHorizontalAlignment.Capacity)
					{
						break;
					}
				}
			}
		}

		private string _displayMemberName = string.Empty;
		public string DisplayMemberName
		{
			get { return this._displayMemberName; }
			set { this._displayMemberName = value; if (string.IsNullOrWhiteSpace(DisplayColumns) == true) DisplayColumns = value; }
		}

		private DataRow _selecteditem = null;
		public DataRow SelectedItem
		{
			get { return this._selecteditem; }
			set { this._selecteditem = value; }
		}

		public int DelayTime
		{
			get { return 0; }
			set {  }
		}

		public int Threshold
		{
			get { return 0; }
			set { }
		}

        [Category("動作")]
        public SearchModeTypes SearchMode
        {
            set { searchMode = value; }
            get { return searchMode; }
        }

		[Category("動作")]
		public Validator.ValidationTypes ValidationType
		{
			get { return cTextBox.ValidationType; }
			set { cTextBox.ValidationType = value; }
		}

		[Category("動作")]
		public string CustomChars
		{
			get { return cTextBox.CustomChars; }
			set { cTextBox.CustomChars = value; }
		}

		#endregion

		#region Methods and Events

		private void comboBox_Loaded(object sender, RoutedEventArgs e)
		{
			//TextBox TxtBox = (TextBox)comboBox.Template.FindName("PART_EditableTextBox", comboBox);
			//TxtBox.IsEnabled = false;
			if (_displayColWidth[0] == 0)
			{
				DisplayColWidth = this.comboBox.ActualWidth.ToString();
			}
		}

		public void SetFocus()
		{
			this.cTextBox.SetFocus();
		}

		public bool ComboboxOpened
		{
			get { return this.comboBox.IsDropDownOpen; }
            set{ this.comboBox.IsDropDownOpen = value;}
		}

		void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			//List<Key> keylist = new List<Key>
			//{
			//	Key.Enter,
			//	Key.Back,
			//	Key.Delete,
			//	Key.Insert,
			//	Key.Home,
			//	Key.End,
			//	Key.Tab,
			//	Key.Up,
			//	Key.Down,
			//	Key.Left,
			//	Key.Right,
			//	Key.D0,
			//	Key.D1,
			//	Key.D2,
			//	Key.D3,
			//	Key.D4,
			//	Key.D5,
			//	Key.D6,
			//	Key.D7,
			//	Key.D8,
			//	Key.D9,
			//	Key.NumPad0,
			//	Key.NumPad1,
			//	Key.NumPad2,
			//	Key.NumPad3,
			//	Key.NumPad4,
			//	Key.NumPad5,
			//	Key.NumPad6,
			//	Key.NumPad7,
			//	Key.NumPad8,
			//	Key.NumPad9,
			//};
			//if (this.ImeType == IMETypes.Off)
			//{
			//	if (e.Key == Key.ImeProcessed)
			//	{
			//		e.Handled = true;
			//		return;
			//	}
			//	if (keylist.Contains(e.Key) != true)
			//	{
			//		e.Handled = true;
			//		return;
			//	}
			//}
			if (e.Key == Key.Down)
			{
				if (comboBox.Items.Count > 0)
				{
					TextChanged();
					IsInternalFocusing = true;
					comboBox.IsDropDownOpen = comboBox.HasItems;
					Keyboard.Focus(this.comboBox);
				}
				else
				{
					e.Handled = true;
					FocusControl.SetFocusWithOrder(new TraversalRequest(FocusNavigationDirection.Next));
					cTextBox.CheckValidation();
				}
			}
			else if (e.Key == Key.Enter)
			{
				e.Handled = true;
				FocusControl.SetFocusWithOrder(new TraversalRequest(FocusNavigationDirection.Next));
				cTextBox.CheckValidation();
				comboBox.IsDropDownOpen = false;
			}
			else if (e.Key == Key.Tab)
			{
				cTextBox.CheckValidation();
				comboBox.IsDropDownOpen = false;
			}
			else if (e.Key == Key.Up)
			{
				e.Handled = true;
				FocusControl.SetFocusWithOrder(new TraversalRequest(FocusNavigationDirection.Previous));
				cTextBox.CheckValidation();
				comboBox.IsDropDownOpen = false;
			}
			else if (e.Key == Key.Space)
			{
				if (this.ValidationType == Validator.ValidationTypes.NumberAutoComplete)
				{
					e.Handled = true;
				}
			}
		}
		
		public bool IsInternalFocusing = false;

		private void textBox_GotFocus(object sender, RoutedEventArgs e)
		{
			Debug.WriteLine("■ textBox_GotFocus");
			TextChanged();
			comboBox.IsDropDownOpen = IsDropDownOnFocus && comboBox.HasItems;
		}

		private void FrameworkControl_GotFocus(object sender, RoutedEventArgs e)
		{
			Debug.WriteLine(string.Format("■ FrameworkControl_GotFocus : [{0}]", sender.ToString()));
		}

		private void FrameworkControl_LostFocus(object sender, RoutedEventArgs e)
		{
			//if (IsInternalFocusing)
			//{
			//	e.Handled = true;
			//}
			Debug.WriteLine(string.Format("{1} FrameworkControl_LostFocus : [{0}]", sender.ToString(), IsInternalFocusing ? "●":"○"));
			//if (this.comboBox.IsFocused != true && this.cTextBox.IsFocused != true)
			//{
			//	this.comboBox.IsDropDownOpen = false;
			//}
		}

		void comboBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Back || e.Key == Key.Delete)
			{
				IsInternalFocusing = true;
				this.cTextBox.SetFocus();
			}
			else if (e.Key == Key.Up)
			{
				if (this.comboBox.SelectedIndex == 0)
				{
					IsInternalFocusing = true;
					this.cTextBox.SetFocus();
				}
			}
			else if (e.Key == Key.Enter)
			{
				this.cTextBox.Focusable = false;
				FocusControl.SetFocusWithOrder(new TraversalRequest(FocusNavigationDirection.Next));
				this.cTextBox.Focusable = true;
			}
		}

		private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Debug.WriteLine("■ comboBox_SelectionChanged : comboBox.SelectedItem is null ? {0}", comboBox.SelectedItem == null);
			if (null != comboBox.SelectedItem)
			{
				this.Text = (comboBox.SelectedItem as ComboBoxItemData).DisplayText;
				SelectedItem = (comboBox.SelectedValue as DataRow);
				comboBox.IsDropDownOpen = false;
				this.cTextBox.Focusable = false;

				IsInternalFocusing = false;

				FocusControl.SetFocusWithOrder(new TraversalRequest(FocusNavigationDirection.Next));
				this.cTextBox.Focusable = true;
				RaiseEvent(new RoutedEventArgs(UcAutoCompleteTextBox.ValueSelectedEvent));
			}
		}

		private void TextChanged()
		{
			try
			{
				Debug.WriteLine("■■■■ TextChanged");
				switch (SearchMode)
				{
				case SearchModeTypes.FORWARD_MATCH:
					comboBox.Items.Clear();
					foreach (var row in (from x in this.allItems where string.IsNullOrWhiteSpace(Text) || x.DisplayText.StartsWith(Text) select x))
					{
						comboBox.Items.Add(row);
					}
					break;
				case SearchModeTypes.BACKWARD_MATCH:
					comboBox.Items.Clear();
					foreach (var row in (from x in this.allItems where string.IsNullOrWhiteSpace(Text) || x.DisplayText.EndsWith(Text) select x))
					{
						comboBox.Items.Add(row);
					}
					break;
				case SearchModeTypes.PARTIAL_MATCH:
					comboBox.Items.Clear();
					foreach (var row in (from x in this.allItems where string.IsNullOrWhiteSpace(Text) || x.DisplayText.Contains(Text) select x))
					{
						comboBox.Items.Add(row);
					}
					break;
				default:
					break;
				}
				comboBox.IsDropDownOpen = comboBox.Items.Count > 0 ? true : false;
                if (comboBox.IsDropDownOpen)
                {
                    Point pt = this.PointFromScreen(new Point(0.0d, 0.0d));
                    User32.MouseMove.SetPosition((int)Math.Abs(pt.X) + 2, (int)Math.Abs(pt.Y) + 2);
                }
			}
			catch { }
		}

		void textBox_cTextChanged(object sender, RoutedEventArgs e)
		{
			if (this.cTextBox.cTextBox.IsFocused)
			{
				this.SelectedItem = null;
				TextChanged();
			}
		}

		protected override Size ArrangeOverride(Size arrangeSize)
		{
			cTextBox.Arrange(new Rect(arrangeSize));
			comboBox.Arrange(new Rect(arrangeSize));
			return base.ArrangeOverride(arrangeSize);
		}

		#endregion

		public override bool CheckValidation()
		{
			return this.cTextBox.CheckValidation();
		}

		public override string GetValidationMessage()
		{
			return this.cTextBox.GetValidationMessage();
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

		#endregion

		private void cTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var ctl = sender as UcTextBox;
			if (ctl == null)
				return;
			if (ctl.IsFocused != true)
			{
				this.cTextBox.SetFocus();
				comboBox.IsDropDownOpen = IsDropDownOnFocus;

				if (this.AutoSelectOnMouseClick == OnOff.Off)
				{
					e.Handled = false;
				}
				else
				{
					e.Handled = true;
				}
			}
		}

		private void cTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			bool chk = Validator.InputCheck(this.ValidationType, e.Text, this.CustomChars, null);
			if (!chk)
			{
				e.Handled = true;
			}
		}

		private void cTextBox_MouseWheel(object sender, MouseWheelEventArgs e)
		{
			if (comboBox.Items.Count > 0)
			{
				TextChanged();
				if (comboBox.Items.Count > 0)
				{
					comboBox.IsDropDownOpen = true;
				}
			}
		}

		private void comboBox_GotFocus(object sender, RoutedEventArgs e)
		{
			this.cTextBox.ResetValidation();
		}

	
	}
}
