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
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;

using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using System.Collections.Specialized;


namespace KyoeiSystem.Framework.Windows.Controls
{
	/// <summary>
	/// UcLabelTwinTextBox.xaml の相互作用ロジック
	/// </summary>
	public partial class UcLabelTwinTextBox : FrameworkControl
	{
		delegate void ThreadMessageDelegate(CommunicationObject data);
		/// <summary>
		/// ラベル文字列プロパティの登録
		/// </summary>
		public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
			"LabelText",
			typeof(object),
			typeof(UcLabelTwinTextBox),
			new UIPropertyMetadata(string.Empty)
		);

		/// <summary>
		/// ラベル文字列
		/// </summary>
		[BindableAttribute(true)]
		public object LabelText
		{
			get { return (object)GetValue(LabelTextProperty); }
			set { SetValue(LabelTextProperty, value); }
		}

		#region Text1Property
		/// <summary>
		/// テキストボックス１の文字列プロパティの登録
		/// </summary>
		public static readonly DependencyProperty Text1Property = DependencyProperty.Register(
				"Text1",
				typeof(string),
				typeof(UcLabelTwinTextBox),
				new FrameworkPropertyMetadata(new PropertyChangedCallback(UcLabelTwinTextBox.OnText1Changed))
			);

		/// <summary>
		/// テキストボックス１の文字列
		/// </summary>
		[BindableAttribute(true)]
		public string Text1
		{
			get { return (string)GetValue(Text1Property); }
			set { SetValue(Text1Property, value); }
		}

		private static void OnText1Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			if (obj.GetType() == typeof(UcLabelTwinTextBox))
			{
				(obj as UcLabelTwinTextBox).Text1 = (string)args.NewValue;
			}
		}
		#endregion

		#region Text2Property
		/// <summary>
		/// テキストボックス２の文字列プロパティの登録
		/// </summary>
		public static readonly DependencyProperty Text2Property = DependencyProperty.Register(
				"Text2",
				typeof(string),
				typeof(UcLabelTwinTextBox),
				new FrameworkPropertyMetadata(new PropertyChangedCallback(UcLabelTwinTextBox.OnText2Changed))
			);

		/// <summary>
		/// テキストボックス２の文字列
		/// </summary>
		[BindableAttribute(true)]
		public string Text2
		{
			get { return (string)GetValue(Text2Property); }
			set { SetValue(Text2Property, value); }
		}

		private static void OnText2Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			if (obj.GetType() == typeof(UcLabelTwinTextBox))
			{
				(obj as UcLabelTwinTextBox).Text2 = (string)args.NewValue;
			}
		}
		#endregion

		#region Text3Property
		/// <summary>
		/// テキストボックス３の文字列プロパティの登録
		/// </summary>
		public static readonly DependencyProperty Text3Property = DependencyProperty.Register(
				"Text3",
				typeof(string),
				typeof(UcLabelTwinTextBox),
				new FrameworkPropertyMetadata(new PropertyChangedCallback(UcLabelTwinTextBox.OnText3Changed))
			);

		/// <summary>
		/// テキストボックス３の文字列
		/// </summary>
		[BindableAttribute(true)]
		public string Text3
		{
			get { return (string)GetValue(Text3Property); }
			set { SetValue(Text3Property, value); }
		}

		private static void OnText3Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			if (obj.GetType() == typeof(UcLabelTwinTextBox))
			{
				(obj as UcLabelTwinTextBox).Text3 = (string)args.NewValue;
			}
		}
		#endregion

		#region LinkItemProperty
		/// <summary>
		/// LinkedItemプロパティの登録
		/// </summary>
		public static readonly DependencyProperty LinkItemProperty = DependencyProperty.Register(
				"LinkItem",
				typeof(object),
				typeof(UcLabelTwinTextBox),
				new FrameworkPropertyMetadata(new PropertyChangedCallback(UcLabelTwinTextBox.OnLinkItemChanged))
			);

		/// <summary>
		/// LinkedItemプロパティ
		/// </summary>
		[BindableAttribute(true)]
		public object LinkItem
		{
			get { return GetValue(LinkItemProperty); }
			set { SetValue(LinkItemProperty, value); }
		}

		private static void OnLinkItemChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			if (obj.GetType() == typeof(UcLabelTwinTextBox))
			{
				(obj as UcLabelTwinTextBox).LinkItem = args.NewValue;
			}
		}
		#endregion


		private Dictionary<string, string> _menberlist = new Dictionary<string, string>();


		/// <summary>
		/// 値の受信イベントの登録
		/// </summary>
		public static readonly RoutedEvent ValueReceivedEvent = EventManager.RegisterRoutedEvent(
			"ValueReceived", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UcLabelTwinTextBox)
			);

		/// <summary>
		/// 値の受信イベント
		/// </summary>
		public event RoutedEventHandler ValueReceived
		{
			add { AddHandler(ValueReceivedEvent, value); }
			remove { RemoveHandler(ValueReceivedEvent, value); }
		}

		private bool isWaitForLoaded = false;

		private string _dataAccessName = string.Empty;
		/// <summary>
		/// Fキーに割り当てる場合のデータアクセス名を取得および設定します
		/// </summary>
		public string DataAccessName
		{
			get { return this._dataAccessName; }
			set
			{
				this._dataAccessName = value;
				//if (string.IsNullOrEmpty(value))
				//{
				//	this.ValueText.Focusable = true;
				//	//this.ValueText.cIsReadOnly = false;
				//	this.ValueText.IsEnabled = true;
				//}
				//else
				//{
				//	this.ValueText.Focusable = false;
				//	//this.ValueText.cIsReadOnly = true;
				//	this.ValueText.IsEnabled = false;
				//}
				NotifyPropertyChanged();
			}
		}

		//private string _masterTableKey = null;
		//public string MasterTableKey
		//{
		//	get { return this._masterTableKey; }
		//	set { this._masterTableKey = value; NotifyPropertyChanged(); }
		//}

		//private string _masterTableSubKey = null;
		//public string MasterTableSubKey
		//{
		//	get { return this._masterTableSubKey; }
		//	set { this._masterTableSubKey = value; NotifyPropertyChanged(); }
		//}

		private string _outputColumnName = null;
		/// <summary>
		/// （廃止されたプロパティ）表示項目名
		/// </summary>
		[Obsolete("DataAccessNameで共通処理となったため、指定しても意味がないものとなりました。")]
		public string OutputColumnName
		{
			get { return this._outputColumnName; }
			set { this._outputColumnName = value; NotifyPropertyChanged(); }
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
				this.CodeText.IsRequired = value;
			}
		}

		private bool _masterCheckEnaled = true;

		/// <summary>
		/// 入力したコードがマスターに存在することをチェックするかどうかを取得または設定します
		/// </summary>
		[Category("動作")]
		public bool MasterCheckEnabled
		{
			get
			{
				return this._masterCheckEnaled;
			}
			set
			{
				this._masterCheckEnaled = value;
				NotifyPropertyChanged();
				this.CodeText.MasterCheckEnaled = value;
			}
		}
		/// <summary>
		/// テキストボックス１の変更イベントの登録
		/// </summary>
		public static readonly RoutedEvent cText1ChangedEvent = EventManager.RegisterRoutedEvent("cText1Changed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UcLabelTwinTextBox));

		/// <summary>
		/// テキストボックス１の変更イベント
		/// </summary>
		public event RoutedEventHandler cText1Changed
		{
			add { AddHandler(cText1ChangedEvent, value); }
			remove { RemoveHandler(cText1ChangedEvent, value); }
		}

		private void Textbox1_TextChanged(object sender, RoutedEventArgs e)
		{
			if (appLog != null) { appLog.Debug("<UC> CodeText_Changed [{0}] -> [{1}]", this.Text1, this.CodeText.Text); }
			this.Text1 = this.CodeText.Text;
			GetValueData();

			RoutedEventArgs NewEventargs = new RoutedEventArgs(UcLabelTwinTextBox.cText1ChangedEvent);
			RaiseEvent(NewEventargs);
		}

		/// <summary>
		/// テキストボックス２の変更イベントの登録
		/// </summary>
		public static readonly RoutedEvent cText2ChangedEvent = EventManager.RegisterRoutedEvent("cText2Changed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UcLabelTwinTextBox));

		/// <summary>
		/// テキストボックス２の変更イベント
		/// </summary>
		public event RoutedEventHandler cText2Changed
		{
			add { AddHandler(cText2ChangedEvent, value); }
			remove { RemoveHandler(cText2ChangedEvent, value); }
		}

		private void Textbox2_TextChanged(object sender, RoutedEventArgs e)
		{
			RoutedEventArgs NewEventargs = new RoutedEventArgs(UcLabelTwinTextBox.cText2ChangedEvent);
			RaiseEvent(NewEventargs);
		}

		# region AutoSelectプロパティ
		/// <summary>
		/// テキストボックス１のフォーカス取得時に全選択とするかどうかを取得または設定します
		/// </summary>
		[Category("動作")]
		public OnOff AutoSelect1
		{
			get { return this.CodeText.AutoSelect; }
			set
			{
				this.CodeText.AutoSelect = value;
			}
		}
		/// <summary>
		/// テキストボックス２のフォーカス取得時に全選択とするかどうかを取得または設定します
		/// </summary>
		[Category("動作")]
		public OnOff AutoSelect2
		{
			get { return this.ValueText.AutoSelect; }
			set
			{
				this.ValueText.AutoSelect = value;
			}
		}
		#endregion

		private OnOff _dataAccessMode = OnOff.On;
		/// <summary>
		/// 
		/// </summary>
		[Category("動作")]
		public OnOff DataAccessMode
		{
			get { return _dataAccessMode; }
			set
			{
				_dataAccessMode = value;
			}
		}

		/// <summary>
		/// テキストボックス１のフォーカス取得後から変更されたことがあるかどうかを取得または設定します
		/// </summary>
		[Category("動作")]
		public bool IsModified1
		{
			set { this.CodeText.IsModified = value; }
			get { return this.CodeText.IsModified; }
		}

		/// <summary>
		/// テキストボックス２のフォーカス取得後から変更されたことがあるかどうかを取得または設定します
		/// </summary>
		[Category("動作")]
		public bool IsModified2
		{
			set { this.ValueText.IsModified = value; }
			get { return this.ValueText.IsModified; }
		}

		private string _beforeCode = string.Empty;
		/// <summary>
		/// マスター検索画面呼び出し前のコード
		/// </summary>
		public string BeforeCode
		{
			set { _beforeCode = value; }
			get { return _beforeCode; }
		}
		private string _afterCode = string.Empty;
		/// <summary>
		/// マスター検索画面呼び出し後のコード
		/// </summary>
		public string AfterCode
		{
			set { _afterCode = value; IsModified1 = (this.BeforeCode != value) ? true : false; }
			get { return _afterCode; }
		}

		/// <summary>
		/// マスター検索画面呼び出し前後で変化があったかどうかを取得します
		/// </summary>
		[Category("動作")]
		public bool IsModified
		{
			get { return (this.BeforeCode != this.AfterCode); }
		}


		///<summary>
		/// Label + TextBox + TextBox
		///</summary>
		public UcLabelTwinTextBox()
		{
			InitializeComponent();
			//this.Loaded += UcLabelTwinTextBox_Loaded;
			this.CodeText.IsCodeTextKey = true;

		}

		protected override void OnRender(DrawingContext drawingContext)
		{
			try
			{
				if (appLog != null) { appLog.Debug("<UC> UcLabelTwinTextBox_OnRender"); }
				if (thmgr == null)
				{
					thmgr = new ThreadManeger(base.viewsCommData.DacConf, base.appLog);
					thmgr.OnReceived += new MessageReceiveHandler(OnReceived);
				}
				if (isWaitForLoaded)
				{
					isWaitForLoaded = false;
					GetValueData();
				}
			}
			catch (Exception)
			{
			}
		}

		/// <summary>
		/// 選択されたデータの中から指定された項目名の値を取得します
		/// </summary>
		/// <param name="name">項目名</param>
		/// <returns>指定項目の値</returns>
		public string GetColumnValue(string name)
		{
			var dat = this._menberlist.Where(x => x.Key == name).FirstOrDefault();
			return dat.Value;
		}

		/// <summary>
		/// 値の検証を行います。
		/// </summary>
		/// <returns>検証結果</returns>
		public override bool CheckValidation()
		{
			return this.CodeText.CheckValidation();
		}

		/// <summary>
		/// 検証結果メッセージを取得します
		/// </summary>
		/// <returns>検証結果メッセージ</returns>
		public override string GetValidationMessage()
		{
			return this.CodeText.GetValidationMessage();
		}


		private void CodeText_LostFocus(object sender, RoutedEventArgs e)
		{
			if (this.CodeText.IsModified)
			{
				if (appLog != null) { appLog.Debug("<UC> CodeText_LostFocus [{0}] ({1})", this.Text1, this.CodeText.Text); }
				GetValueData();
			}
		}

		/// <summary>
		/// フォーカスを設定します（SetFocusメソッドと同じ）
		/// </summary>
		public void Focus()
		{
			SetFocus();
		}

		/// <summary>
		/// フォーカスを設定します（Focusメソッドと同じ）
		/// </summary>
		public void SetFocus()
		{
			this.CodeText.SetFocus();
		}

		private void CodeText_Changed(object sender, RoutedEventArgs e)
		{
			// 処理内容はTextbox1_TextChangedに移動
		}

		private void GetValueData()
		{
			if (string.IsNullOrWhiteSpace(this.DataAccessName))
			{
				return;
			}
			if (this.DataAccessMode == OnOff.Off)
			{
				return;
			}
			if (string.IsNullOrWhiteSpace(this.CodeText.Text))
			{
				this.Text2 = string.Empty;
				return;
			}
			try
			{
				if (appLog != null) { appLog.Debug("<UC> GetValueData START ({0}, {1})", this.DataAccessName, this.LinkItem); }

				this._menberlist.Clear();
				if (this.thmgr == null || string.IsNullOrWhiteSpace(this.ConnectStringUserDB))
				{
					isWaitForLoaded = true;
					this.Text2 = string.Empty;
					this.Text3 = string.Empty;
					//RaiseEvent(new RoutedEventArgs(UcLabelTwinTextBox.ValueReceivedEvent));
					return;
				}
				if (string.IsNullOrWhiteSpace(this.Text1))
				{
					this.Text2 = string.Empty;
					this.Text3 = string.Empty;
					RaiseEvent(new RoutedEventArgs(UcLabelTwinTextBox.ValueReceivedEvent));
					return;
				}
				string keystr = string.Empty;
				if (this.Text1.GetType() == typeof(string))
				{
					keystr = (string)this.Text1;
				}
				else
				{
					keystr = string.Format("{0}", this.Text1);
				}

				if (IsWindowClosing)
				{
					return;
				}

				CommunicationObject com;
				com = new CommunicationObject(MessageType.RequestData, "UcMST", keystr, this.DataAccessName, this.LinkItem);
				com.connection = this.ConnectStringUserDB;
				this.thmgr.SendRequest(com);
			}
			catch (Exception)
			{
				this.Text2 = string.Empty;
				this.Text3 = string.Empty;
				RaiseEvent(new RoutedEventArgs(UcLabelTwinTextBox.ValueReceivedEvent));
			}
		}
		/// <summary>
		/// データアクセス要求の結果受信イベント
		/// </summary>
		/// <param name="message">受信メッセージ</param>
		public override void OnReceived(CommunicationObject message)
		{
			base.OnReceived(message);

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
			this.Text2 = string.Empty;
			this.Text3 = string.Empty;
			this.CodeText.IsCodeExist = false;
			this.CheckValidation();
			this._menberlist.Clear();
			RaiseEvent(new RoutedEventArgs(UcLabelTwinTextBox.ValueReceivedEvent));
		}

		/// <summary>
		/// 取得データの取り込み
		/// </summary>
		/// <param name="message"></param>
		void setReceivedData(CommunicationObject message)
		{
			try
			{
				this._menberlist.Clear();
				var data = message.GetResultData();
				DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
				if (tbl == null)
				{
					this.CodeText.IsCodeExist = false;
					this.Text2 = string.Empty;
					this.Text3 = string.Empty;
				}
				else
				{
					if (tbl.Rows.Count > 0)
					{
						this.CodeText.IsCodeExist = true;
						if (tbl.Rows[0].IsNull("名称"))
						{
							this.Text2 = string.Empty;
							this.Text3 = string.Empty;
						}
						else
						{
							string[] textlist = (tbl.Rows[0]["名称"] as string).Split(new char[] { '\t' });
							this.Text2 = textlist[0];
							if (textlist.Length == 1)
							{
								this.Text2 = textlist[0];
							}
							else if (textlist.Length == 2)
							{
								this.Text2 = textlist[0];
								this.Text3 = textlist[1];
							}
							else if (textlist.Length > 2)
							{
								foreach (string col in textlist)
								{
									string[] val = col.Split(new char[] { '=' });
									this._menberlist.Add(val[0], val[1]);
								}
								if (this._menberlist.Count >= 2)
								{
									this.Text2 = this._menberlist.ToArray()[0].Value;
									this.Text3 = this._menberlist.ToArray()[1].Value;
								}
							}
							else
							{
								this.Text2 = string.Empty;
								this.Text3 = string.Empty;
							}
						}
					}
					else
					{
						this.CodeText.IsCodeExist = false;
						this.Text2 = string.Empty;
						this.Text3 = string.Empty;
					}
				}
			}
			catch (Exception)
			{
				this.CodeText.IsCodeExist = false;
				// マスターからの取得エラー
				this.Text2 = string.Empty;
				this.Text3 = string.Empty;
			}
			this.CheckValidation();

			RaiseEvent(new RoutedEventArgs(UcLabelTwinTextBox.ValueReceivedEvent));

		}
		/// <summary>
		/// ラベルの表示設定
		/// </summary>
		public Visibility LabelVisibility
		{
			get { return this.cLabel.Visibility; }
			set { this.cLabel.Visibility = value; NotifyPropertyChanged(); }
		}

		private Visibility _label1Visibility;
		/// <summary>
		/// ラベル１の表示設定
		/// </summary>
		public Visibility Label1Visibility
		{
			get { return this._label1Visibility; }
			set { this._label1Visibility = value; NotifyPropertyChanged(); }
		}
		private string _label1Text;
		/// <summary>
		/// ラベル１のテキスト
		/// </summary>
		public string Label1Text
		{
			get { return this._label1Text; }
			set
			{
				this._label1Text = value;
				NotifyPropertyChanged();
				if (string.IsNullOrEmpty(this._label1Text))
				{
					this.Label1Visibility = System.Windows.Visibility.Collapsed;
				}
				else
				{
					this.Label1Visibility = System.Windows.Visibility.Visible;
				}
			}
		}

		private Visibility _label2Visibility;
		/// <summary>
		/// ラベル２の表示設定
		/// </summary>
		public Visibility Label2Visibility
		{
			get { return this._label2Visibility; }
			set { this._label2Visibility = value; NotifyPropertyChanged(); }
		}
		private string _label2Text;
		/// <summary>
		/// ラベル２のテキスト
		/// </summary>
		public string Label2Text
		{
			get { return this._label2Text; }
			set
			{
				this._label2Text = value;
				NotifyPropertyChanged();
				if (string.IsNullOrEmpty(this._label2Text))
				{
					this.Label2Visibility = System.Windows.Visibility.Collapsed;
				}
				else
				{
					this.Label2Visibility = System.Windows.Visibility.Visible;
				}
			}
		}
		/// <summary>
		/// テキストボックス１の値検証メッセージを設定する
		/// </summary>
		/// <param name="message">メッセージ</param>
		public void SetValidationMessage1(string message)
		{
			this.CodeText.SetValidationMessage(message);
		}

		/// <summary>
		/// テキストボックス２の値検証メッセージを設定する
		/// </summary>
		/// <param name="message">メッセージ</param>
		public void SetValidationMessage2(string message)
		{
			this.ValueText.SetValidationMessage(message);
		}

		/// <summary>
		/// テキストボックス１の表示成型用マスク文字列
		/// </summary>
		[Category("動作")]
		public string Text1Mask
		{
			get { return CodeText.Mask; }
			set { CodeText.Mask = value; }
		}

		/// <summary>
		/// テキストボックス２の表示成型用マスク文字列
		/// </summary>
		[Category("動作")]
		public string Text2Mask
		{
			get { return  ValueText.Mask; }
			set { ValueText.Mask = value; }
		}

		/////<summary>
		/////Label　プロパティ
		/////</summary>
		#region Label Property

		/// <summary>
		/// コントロールの背景を表すブラシを取得または設定します。
		/// </summary>
		#region Background
		[Category("ブラシ")]
		public Brush Label_Background
		{
			get { return this.cLabel.cBackground; }
			set { this.cLabel.cBackground = value; }
		}
		#endregion

		/// <summary>
		/// データ バインディングに参加すると要素のデータ コンテキストを取得または設定します
		/// </summary>
		#region DataContext
		[Category("動作")]
		public object Label_DataContext
		{
			get { return this.cLabel.cDataContext; }
			set { this.cLabel.cDataContext = value; }
		}
		#endregion

		/// <summary>
		/// ContentControl のコンテンツを取得または設定します。
		/// </summary>
		#region Context
		[Category("レイアウト")]
		public object Label_Context
		{
			get { return this.cLabel.cContent; }
			set { this.cLabel.cContent = value; }
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
			get { return this.cLabel.cCommandBindings; }
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
			get { return this.cLabel.cDispatcher; }
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
			get { return this.cLabel.cFocusable; }
			set { this.cLabel.cFocusable = value; }
		}
		#endregion

		/// <summary>
		/// コントロールのフォント ファミリを取得または設定します
		/// </summary>
		#region FontFamily
		[Category("動作")]
		public FontFamily Label_FontFamily
		{
			get { return this.cLabel.cFontFamily; }
			set { this.cLabel.cFontFamily = value; }
		}
		#endregion

		/// <summary>
		/// フォント サイズを取得または設定します
		/// </summary>
		#region FontSize
		[Category("レイアウト")]
		public double Label_FontSize
		{
			get { return this.cLabel.cFontSize; }
			set { this.cLabel.cFontSize = value; }
		}
		#endregion

		/// <summary>
		/// 画面上でフォントを縮小または拡大する度合いを取得または設定します。 
		/// </summary>
		#region FontStretch
		[Category("動作")]
		public FontStretch Label_FontStretch
		{
			get { return this.cLabel.cFontStretch; }
			set { this.cLabel.cFontStretch = value; }
		}
		#endregion

		/// <summary>
		/// フォント スタイルを取得または設定します。
		/// </summary>
		#region FontStyle
		[Category("動作")]
		public FontStyle Label_FontStyle
		{
			get { return this.cLabel.cFontStyle; }
			set { this.cLabel.cFontStyle = value; }
		}
		#endregion

		/// <summary>
		/// 画面上でフォントを縮小または拡大する度合いを取得または設定します。 
		/// </summary>
		#region FontWeight
		[Category("動作")]
		public FontWeight Label_FontWeight
		{
			get { return this.cLabel.cFontWeight; }
			set { this.cLabel.cFontWeight = value; }
		}
		#endregion

		/// <summary>
		/// 前景色を表すブラシを取得または設定します
		/// </summary>
		#region Foreground
		[Category("ブラシ")]
		public Brush Label_Foreground
		{
			get { return this.cLabel.cForeground; }
			set { this.cLabel.cForeground = value; }
		}
		#endregion

		/// <summary>
		/// ContentControl にコンテンツが含まれているかどうかを示す値を取得します。 
		/// </summary>
		#region HasContent
		[Category("動作")]
		public bool Label_HasContent
		{
			get { return this.cLabel.cHasContent; }
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
			get { return this.cLabel.cHeight; }
			set { this.cLabel.cHeight = value; }
		}
		#endregion

		/// <summary>
		/// コントロールのコンテンツの水平方向の配置を取得または設定します
		/// </summary>
		#region HorizontalContentAlignment
		[Category("表示")]
		public HorizontalAlignment Label_HorizontalContentAlignment
		{
			get { return this.cLabel.cHorizontalContentAlignment; }
			set { this.cLabel.cHorizontalContentAlignment = value; }
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
			get { return this.cLabel.cIsFocused; }
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
			get { return this.cLabel.cIsTabStop; }
			set { this.cLabel.cIsTabStop = value; }
		}
		#endregion

		/// <summary>
		/// この要素が ユーザー インターフェイス (UI)に表示されるかどうかを示す値を取得します。
		/// これは 依存関係プロパティです。
		/// </summary>
		//#region IsVisible
		//[Category("動作")]
		//public bool Label_IsVisible
		//{
		//	get { return this.cLabel.cIsVisible; }
		//	set { }
		//}
		//#endregion
		[Category("動作")]
		public Visibility Label_Visibility
		{
			get { return this.cLabel.cVisibility; }
			set { this.cLabel.cVisibility = value; }
		}


		/// <summary>
		/// この要素が ユーザー インターフェイス 
		/// (UI)に表示されるかどうかを示す値を取得します。
		/// これは 依存関係プロパティです。
		/// </summary>
		#region Margin
		[Category("レイアウト")]
		public Thickness Label_Margin
		{
			get { return this.cLabel.cMargin; }
			set { this.cLabel.cMargin = value; }
		}
		#endregion

		/// <summary>
		/// 要素の高さの最大値を取得または設定します
		/// </summary>
		#region MaxHeight
		[Category("レイアウト")]
		public double Label_MaxHeight
		{
			get { return this.cLabel.cMaxHeight; }
			set { this.cLabel.cMaxHeight = value; }
		}
		#endregion

		/// <summary>
		/// 要素の高さの最大値を取得または設定します
		/// </summary>
		#region MaxWidth
		[Category("レイアウト")]
		public double Label_MaxWidth
		{
			get { return this.cLabel.cMaxWidth; }
			set { this.cLabel.cMaxWidth = value; }
		}
		#endregion

		/// <summary>
		/// 要素の高さの最大値を取得または設定します
		/// </summary>
		#region MinHeight
		[Category("レイアウト")]
		public double Label_MinHeight
		{
			get { return this.cLabel.cMinHeight; }
			set { this.cLabel.cMinHeight = value; }
		}
		#endregion

		/// <summary>
		/// 要素の高さの最小値を取得または設定します 
		/// </summary>
		#region MinWidth
		[Category("レイアウト")]
		public double Label_MinWidth
		{
			get { return this.cLabel.cMinWidth; }
			set { this.cLabel.cMinWidth = value; }
		}
		#endregion

		/// <summary>
		/// 要素の ID の名前を取得または設定します。名前は XAML のプロセッサで処理中に作成されると分離コードを、
		/// イベント ハンドラー コードなどのマークアップ要素を参照できるように参照を提供します。
		/// </summary>
		#region Name
		[Category("共通")]
		public string Label_Name
		{
			get { return this.cLabel.cName; }
			set { this.cLabel.cName = value; }
		}
		#endregion

		/// <summary>
		/// コントロール内のスペースを取得または設定します
		/// </summary>
		#region Padding
		[Category("動作")]
		public Thickness Label_Padding
		{
			get { return this.cLabel.cPadding; }
			set { this.cLabel.cPadding = value; }
		}
		#endregion

		/// <summary>
		/// この要素の logical parent の要素を取得します。
		/// </summary>
		#region Parent
		[Category("動作")]
		public DependencyObject Label_Parent
		{
			get { return this.cLabel.cParent; }
			set { }
		}
		#endregion

		/// <summary>
		/// ユーザー インターフェイス (UI)のこの要素に対して表示される
		/// ツール ヒントのオブジェクトを取得または設定します
		/// </summary>
		#region TabIndex
		[Category("動作")]
		public int Label_TabIndex
		{
			get { return this.cLabel.cTabIndex; }
			set { this.cLabel.cTabIndex = value; }
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
			get { return this.cLabel.cToolTip; }
			set { this.cLabel.cToolTip = value; }
		}
		#endregion

		/// <summary>
		/// コントロールのコンテンツの垂直方向の配置を取得または設定します。
		/// </summary>
		#region VerticalContentAlignment
		[Category("レイアウト")]
		public VerticalAlignment Label_VerticalContentAlignment
		{
			get { return this.cLabel.cVerticalContentAlignment; }
			set { this.cLabel.cVerticalContentAlignment = value; }
		}
		#endregion

		/// <summary>
		/// 要素の幅を取得または設定します。 
		/// </summary>
		#region Width
		[Category("レイアウト")]
		public double Label_Width
		{
			get { return this.cLabel.cWidth; }
			set { this.cLabel.cWidth = value; }
		}
		#endregion

		#endregion

		/////<summary>
		/////CodeText　プロパティ
		/////</summary>
		#region CodeText Property

		/// <summary>
		/// Width
		/// </summary>
		#region Width
		[Category("動作")]
		public double Text1Width
		{
			get { return CodeText.Width; }
			set { CodeText.Width = CodeText.MaxWidth = CodeText.MinWidth = value; }
		}
		#endregion

		/// <summary>
		/// ValidationType
		/// </summary>
		#region ValidationType
		[Category("動作")]
		public Validator.ValidationTypes Text1ValidationType
		{
			get { return CodeText.ValidationType; }
			set { CodeText.ValidationType = value; }
		}
		//[Category("動作")]
		//public Validator.ValidationTypes Text2ValidationType
		//{
		//	get { return ValueText.ValidationType; }
		//	set { ValueText.ValidationType = value; }
		//}
		#endregion

		/// <summary>
		/// テキストボックス１のImeType
		/// </summary>
		#region ImeType
		[Category("動作")]
		public IMETypes Text1IMEType
		{
			get { return CodeText.ImeType; }
			set { CodeText.ImeType = value; }
		}
		/// <summary>
		/// テキストボックス２のImeType
		/// </summary>
		[Category("動作")]
		public IMETypes Text2IMEType
		{
			get { return ValueText.ImeType; }
			set { ValueText.ImeType = value; }
		}
		#endregion


		/// <summary>
		/// AcceptsReturn 依存プロパティ
		/// Enter キーが押されたとき、テキスト編集コントロールがどのように反応するかを示す値を取得または設定します
		/// </summary>
		#region AcceptsReturn
		[Category("動作")]
		public bool Text1AcceptsReturn
		{
			get { return CodeText.cAcceptsReturn; }
			set { CodeText.cAcceptsReturn = value; }
		}
		#endregion

		/// <summary>
		/// AcceptsTab 依存プロパティ
		/// Tab キーが押されたとき、テキスト編集コントロールがどのように反応するかを示す値を取得または設定します
		/// </summary>
		#region AcceptsTab
		[Category("動作")]
		public bool Text1AcceptsTab
		{
			get { return CodeText.cAcceptsTab; }
			set { CodeText.cAcceptsTab = value; }
		}
		#endregion

        /// <summary>
        /// Foreground
        /// 前景色を表すブラシを取得または設定します
        /// </summary>
        #region Foreground
        [Category("ブラシ")]
        public Brush Text1Foreground
        {
            get { return CodeText.cForeground; }
            set { CodeText.cForeground = value; }
        }
        #endregion

        /// <summary>
		/// Background 
		/// コントロールの背景を表すブラシを取得または設定します
		/// </summary>
		#region Background
		[Category("ブラシ")]
		public Brush Text1Background
		{
			get { return CodeText.cBackground; }
			set { CodeText.cBackground = value; }
		}
		#endregion

		/// <summary>
		/// BorderBrush 
		/// ValueTextのBorderのブラシを取得または設定します
		/// </summary>
		#region BorderBrush
		public Thickness Text1BorderThickness
		{
			get { return CodeText.cBorderThickness; }
			set { CodeText.cBorderThickness = value; }
		}
		#endregion

		/// <summary>
		/// BorderBrush 
		/// ValueTextのBorderのブラシを取得または設定します
		/// </summary>
		#region BorderBrush
		public Brush Text1BorderBrush
		{
			get { return CodeText.cBorderBrush; }
			set { CodeText.cBorderBrush = value; }
		}
		#endregion

		/// <summary>
		/// IsReadOnly 依存プロパティ
		/// テキスト編集コントロールを操作するユーザーに対して、コントロールが読み取り専用であるかどうかを示す値を取得または設定します
		/// </summary>
		#region
		[Category("動作")]
		public bool Text1IsReadOnly
		{
			get { return CodeText.cIsReadOnly; }
			set { CodeText.cIsReadOnly = value; }
		}
		#endregion


		/// <summary>
		/// MaxLength 依存プロパティ
		/// テキスト ボックスに手動で入力できる最大文字数を取得または設定します
		/// </summary>
		#region
		[Category("動作")]
		public Int32 Text1MaxLength
		{
			get { return CodeText.cMaxLength; }
			set { CodeText.cMaxLength = value; }
		}
		#endregion

		/// <summary>
		/// MinLines 依存プロパティ
		/// 表示行の最小数を取得または設定します。
		/// </summary>
		#region
		[Category("動作")]
		public Int32 Text1MinLines
		{
			get { return CodeText.cMinLines; }
			set { CodeText.cMinLines = value; }
		}
		#endregion

		/// <summary>
		/// SelectedText 依存プロパティ
		/// テキスト ボックス内の現在の選択範囲のコンテンツを取得または設定します。
		/// </summary>
		#region
		[Category("動作")]
		public string Text1SelectedText
		{
			get { return CodeText.cSelectedText; }
			set { CodeText.cSelectedText = value; }
		}
		#endregion

		/// <summary>
		/// SelectionStart 依存プロパティ
		/// 現在の選択範囲の先頭の文字インデックスを取得または設定します。
		/// </summary>
		#region
		[Category("動作")]
		public Int32 Text1SelectionStart
		{
			get { return CodeText.cSelectionStart; }
			set { CodeText.cSelectionStart = value; }
		}
		#endregion

		/// <summary>
		/// TextAlignment 依存プロパティ
		/// テキスト ボックスのテキスト コンテンツを取得または設定します。
		/// </summary>
		#region
		[Category("表示")]
		public TextAlignment Text1TextAlignment
		{
			get { return CodeText.cTextAlignment; }
			set { CodeText.cTextAlignment = value; }
		}
		#endregion

		/// <summary>
		/// TextWrapping 依存プロパティ
		/// テキスト ボックスのテキスト折り返し方法を取得または設定します。
		/// </summary>
		#region
		[Category("動作")]
		public TextWrapping Text1TextWrapping
		{
			get { return CodeText.cTextWrapping; }
			set { CodeText.cTextWrapping = value; }
		}

		#endregion

		/// <summary>
		/// ToolTip 依存プロパティ
		/// ユーザー インターフェイス (UI)のこの要素に対して表示されるツール ヒントのオブジェクトを取得または設定します。
		/// </summary>
		#region
		[Category("動作")]
		public object Text1ToolTip
		{
			get { return CodeText.cToolTip; }
			set { CodeText.cToolTip = value; }
		}

		#endregion


		/// <summary>
		/// Visibility 依存プロパティ
		/// この要素の ユーザー インターフェイス (UI) 表現を取得または設定します。
		/// </summary>
		#region
		[Category("デザイン")]
		public Visibility Text1Visibility
		{
			get { return CodeText.cVisibility; }
			set { CodeText.cVisibility = value; }
		}
		#endregion

		/// <summary>
		/// HorizontalAlignment 依存プロパティ
		/// </summary>
		#region
		[Category("デザイン")]
		public HorizontalAlignment Text1HorizontalAlignment
		{
			get { return CodeText.cHorizontalAlignment; }
			set { CodeText.cHorizontalAlignment = value; }
		}
		#endregion

		/// <summary>
		/// VerticalAlignment  依存プロパティ
		/// </summary>
		#region
		[Category("デザイン")]
		public VerticalAlignment Text1VerticalAlignment
		{
			get { return CodeText.cVerticalAlignment; }
			set { CodeText.cVerticalAlignment = value; }
		}
		#endregion

		/// <summary>
		/// HorizontalContentAlignment  依存プロパティ
		/// </summary>
		#region
		[Category("デザイン")]
		public HorizontalAlignment Text1HorizontalContentAlignment
		{
			get { return CodeText.cHorizontalContentAlignment; }
			set { CodeText.cHorizontalContentAlignment = value; }
		}
		#endregion

        /// <summary>
        /// コントロールのコンテンツの垂直方向の配置を取得または設定します。
        /// </summary>
        #region VerticalContentAlignment
        [Category("デザイン")]
        public VerticalAlignment Text1VerticalContentAlignment
        {
            get { return this.CodeText.cVerticalContentAlignment; }
            set { this.CodeText.cVerticalContentAlignment = value; }
        }
        #endregion

		#endregion

		/////<summary>
		/////ValueText　プロパティ
		/////</summary>
		#region ValueText Property

		/// <summary>
		/// Width
		/// </summary>
		#region Width
		[Category("動作")]
		public double Text2Width
		{
			get { return ValueText.Width; }
			set { ValueText.Width = value; }
		}
		#endregion

		/// <summary>
		/// ValidationType
		/// </summary>
		#region
		[Category("動作")]
		public Validator.ValidationTypes Text2ValidationType
		{
			get { return ValueText.ValidationType; }
			set { ValueText.ValidationType = value; }
		}
		#endregion

		/// <summary>
		/// AcceptsReturn 依存プロパティ
		/// Enter キーが押されたとき、テキスト編集コントロールがどのように反応するかを示す値を取得または設定します
		/// </summary>
		#region AcceptsReturn
		[Category("動作")]
		public bool Text2AcceptsReturn
		{
			get { return ValueText.cAcceptsReturn; }
			set { ValueText.cAcceptsReturn = value; }
		}
		#endregion

		/// <summary>
		/// AcceptsTab 依存プロパティ
		/// Tab キーが押されたとき、テキスト編集コントロールがどのように反応するかを示す値を取得または設定します
		/// </summary>
		#region AcceptsTab
		[Category("動作")]
		public bool Text2AcceptsTab
		{
			get { return ValueText.cAcceptsTab; }
			set { ValueText.cAcceptsTab = value; }
		}
		#endregion

        /// <summary>
        /// Foreground
        /// 前景色を表すブラシを取得または設定します
        /// </summary>
        #region Foreground
        [Category("ブラシ")]
        public Brush Text2Foreground
        {
            get { return ValueText.cForeground; }
            set { ValueText.cForeground = value; }
        }
        #endregion

		/// <summary>
		/// Background 
		/// コントロールの背景を表すブラシを取得または設定します
		/// </summary>
		#region Background
		[Category("ブラシ")]
		public Brush Text2Background
		{
			get { return ValueText.cBackground; }
			set { ValueText.cBackground = value; }
		}
		#endregion

		/// <summary>
		/// BorderBrush 
		/// ValueTextのBorderのブラシを取得または設定します
		/// </summary>
		#region BorderBrush
		public Brush Text2BorderBrush
		{
			get { return ValueText.cBorderBrush; }
			set { ValueText.cBorderBrush = value; }
		}
		#endregion

		/// <summary>
		/// BorderBrush 
		/// ValueTextのBorderのブラシを取得または設定します
		/// </summary>
		#region BorderBrush
		public Thickness Text2BorderThickness
		{
			get { return ValueText.cBorderThickness; }
			set { ValueText.cBorderThickness = value; }
		}
		#endregion

		/// <summary>
		/// IsReadOnly 依存プロパティ
		/// テキスト編集コントロールを操作するユーザーに対して、コントロールが読み取り専用であるかどうかを示す値を取得または設定します
		/// </summary>
		#region
		[Category("動作")]
		public bool Text2IsReadOnly
		{
			get { return ValueText.cIsReadOnly; }
			set 
			{
				if (value != true)
				{
					this.ValueText.Focusable = true;
					//this.ValueText.cIsReadOnly = false;
					this.ValueText.IsEnabled = true;
				}
				else
				{
					this.ValueText.Focusable = false;
					//this.ValueText.cIsReadOnly = true;
					this.ValueText.IsEnabled = false;
				}
			}
		}
		#endregion


		/// <summary>
		/// MaxLength 依存プロパティ
		/// テキスト ボックスに手動で入力できる最大文字数を取得または設定します
		/// </summary>
		#region
		[Category("動作")]
		public Int32 Text2MaxLength
		{
			get { return ValueText.cMaxLength; }
			set { ValueText.cMaxLength = value; }
		}
		#endregion

		/// <summary>
		/// MinLines 依存プロパティ
		/// 表示行の最小数を取得または設定します。
		/// </summary>
		#region
		[Category("動作")]
		public Int32 Text2MinLines
		{
			get { return ValueText.cMinLines; }
			set { ValueText.cMinLines = value; }
		}
		#endregion

		/// <summary>
		/// SelectedText 依存プロパティ
		/// テキスト ボックス内の現在の選択範囲のコンテンツを取得または設定します。
		/// </summary>
		#region
		[Category("動作")]
		public string Text2SelectedText
		{
			get { return ValueText.cSelectedText; }
			set { ValueText.cSelectedText = value; }
		}
		#endregion

		/// <summary>
		/// SelectionStart 依存プロパティ
		/// 現在の選択範囲の先頭の文字インデックスを取得または設定します。
		/// </summary>
		#region
		[Category("動作")]
		public Int32 Text2SelectionStart
		{
			get { return ValueText.cSelectionStart; }
			set { ValueText.cSelectionStart = value; }
		}
		#endregion

		/// <summary>
		/// TextAlignment 依存プロパティ
		/// テキスト ボックスのテキスト コンテンツを取得または設定します。
		/// </summary>
		#region
		[Category("表示")]
		public TextAlignment Text2TextAlignment
		{
			get { return ValueText.cTextAlignment; }
			set { ValueText.cTextAlignment = value; }
		}
		#endregion

		/// <summary>
		/// TextWrapping 依存プロパティ
		/// テキスト ボックスのテキスト折り返し方法を取得または設定します。
		/// </summary>
		#region
		[Category("動作")]
		public TextWrapping Text2TextWrapping
		{
			get { return ValueText.cTextWrapping; }
			set { ValueText.cTextWrapping = value; }
		}
		#endregion

		/// <summary>
		/// ToolTip 依存プロパティ
		/// ユーザー インターフェイス (UI)のこの要素に対して表示されるツール ヒントのオブジェクトを取得または設定します。
		/// </summary>
		#region
		[Category("動作")]
		public object Text2ToolTip
		{
			get { return ValueText.cToolTip; }
			set { ValueText.cToolTip = value; }
		}

		#endregion

		/// <summary>
		/// Visibility 依存プロパティ
		/// この要素の ユーザー インターフェイス (UI) 表現を取得または設定します。
		/// </summary>
		#region
		[Category("デザイン")]
		public Visibility Text2Visibility
		{
			get { return ValueText.cVisibility; }
			set { ValueText.cVisibility = value; }
		}
		#endregion

		/// <summary>
		/// HorizontalAlignment 依存プロパティ
		/// </summary>
		#region
		[Category("デザイン")]
		public HorizontalAlignment Text2HorizontalAlignment
		{
			get { return ValueText.cHorizontalAlignment; }
			set { ValueText.cHorizontalAlignment = value; }
		}
		#endregion

		/// <summary>
		/// VerticalAlignment  依存プロパティ
		/// </summary>
		#region
		[Category("デザイン")]
		public VerticalAlignment Text2VerticalAlignment
		{
			get { return ValueText.cVerticalAlignment; }
			set { ValueText.cVerticalAlignment = value; }
		}
		#endregion

		/// <summary>
		/// HorizontalContentAlignment  依存プロパティ
		/// </summary>
		#region
		[Category("デザイン")]
		public HorizontalAlignment Text2HorizontalContentAlignment
		{
			get { return ValueText.cHorizontalContentAlignment; }
			set { ValueText.cHorizontalContentAlignment = value; }
		}
		#endregion

        /// <summary>
        /// コントロールのコンテンツの垂直方向の配置を取得または設定します。
        /// </summary>
        #region VerticalContentAlignment
        [Category("デザイン")]
        public VerticalAlignment Text2VerticalContentAlignment
        {
            get { return this.ValueText.cVerticalContentAlignment; }
            set { this.ValueText.cVerticalContentAlignment = value; }
        }
        #endregion

		#endregion

	}
}
