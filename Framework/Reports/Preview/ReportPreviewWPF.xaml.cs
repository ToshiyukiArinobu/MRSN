using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
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
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Markup;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Reflection;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.IO.Packaging;
using System.Printing;
using System.Threading;
using System.Text.RegularExpressions;

using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Enterprise;
using SAPBusinessObjects.WPF.Viewer;

using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.Controls;
using KyoeiSystem.Framework.Windows.ViewBase;

using iTextSharp.text;
using iTextSharp.text.pdf;


namespace KyoeiSystem.Framework.Reports.Preview
{

	#region レポートパラメータクラス
	/// <summary>
	/// レポートパラメータ（CrystalReportsのレポート定義で設定されたパラメータ）
	/// </summary>
	public class ReportParameter
	{
		/// <summary>
		/// パラメータ名
		/// </summary>
		public string PNAME = string.Empty;
		/// <summary>
		/// パラメータ値
		/// </summary>
		public object VALUE = null;
	}
	#endregion

	#region レポートフィールド情報クラス
	/// <summary>
	/// セクション情報定義体（デザイナー専用）
	/// </summary>
	public class SectInfo
	{
		public string name;
		public int height;
		public int width;
		public double offset;
	}

	/// <summary>
	/// レポートオブジェクトタイプ（デザイナー専用）
	/// </summary>
	public enum ObjectInfoType
	{
		/// <summary>
		/// 未定義
		/// </summary>
		Null,
		/// <summary>
		/// テキスト
		/// </summary>
		Label,
		/// <summary>
		/// 枠線
		/// </summary>
		Box,
		/// <summary>
		/// 縦線
		/// </summary>
		VLine,
		/// <summary>
		/// 横線
		/// </summary>
		HLine,
	}
	/// <summary>
	/// レポートオブジェクト情報定義体（デザイナー専用）
	/// </summary>
	public class ObjectInfo
	{
		/// <summary>
		/// オブジェクト名
		/// </summary>
		public string name;
		/// <summary>
		/// セクション名
		/// </summary>
		public string sectionname;
		/// <summary>
		/// 複数セクションへの拡張
		/// </summary>
		public bool extendSection = false;
		/// <summary>
		/// 最終セクション名（複数セクション拡張時）
		/// </summary>
		public string endSectionName;
		/// <summary>
		/// 表示・非表示
		/// </summary>
		public bool visible;
		/// <summary>
		/// レポートオブジェクトタイプ
		/// </summary>
		public ObjectInfoType objectType = ObjectInfoType.Null;
		private object _ctl;
		/// <summary>
		/// デザイナエリアのコントロール情報
		/// </summary>
		public object control
		{
			get { return _ctl; }
			set
			{
				_ctl = value;
				objectType = ObjectInfoType.Null;
				RptObjectLabel obj = _ctl as RptObjectLabel;
				if (_ctl == null)
					return;
				objectType = obj.ObjectType;
				_height = obj.Height;
				_width = obj.Width;
				_top = (double)(obj.GetValue(Canvas.TopProperty));
				_left = (double)(obj.GetValue(Canvas.LeftProperty));
				obj.SetValue(Canvas.ZIndexProperty, 2);

				return;
			}
		}
		double _top;
		/// <summary>
		/// オブジェクト選択用矩形の座標（TOP）
		/// </summary>
		public double top
		{
			get { return _top; }
			set
			{
				_top = value;
				if (_ctl == null)
					return;
				RptObjectLabel lbl = _ctl as RptObjectLabel;
				lbl.SetValue(Canvas.TopProperty, value);
			}
		}
		double _left;
		/// <summary>
		/// オブジェクト選択用矩形の座標（LEFT）
		/// </summary>
		public double left
		{
			get { return _left; }
			set
			{
				_left = value;
				if (_ctl == null)
					return;
				RptObjectLabel lbl = _ctl as RptObjectLabel;
				lbl.SetValue(Canvas.LeftProperty, value);
			}
		}
		double _width;
		/// <summary>
		/// オブジェクト選択用矩形のサイズ（WIDTH）
		/// </summary>
		public double width
		{
			get { return _width; }
			set
			{
				_width = value;
				if (_ctl == null)
					return;
				RptObjectLabel lbl = _ctl as RptObjectLabel;
				lbl.Width = value;
			}
		}
		double _height;
		/// <summary>
		/// オブジェクト選択用矩形のサイズ（HEIGHT）
		/// </summary>
		public double height
		{
			get { return _height; }
			set
			{
				_height = value;
				if (_ctl == null)
					return;
				RptObjectLabel lbl = _ctl as RptObjectLabel;
				lbl.Height = value;
			}
		}
		/// <summary>
		/// オブジェクト選択用矩形の座標（BOTTOM）
		/// </summary>
		public double bottom
		{
			get { return _top + _height; }
		}
		/// <summary>
		/// オブジェクト選択用矩形の座標（RIGHT）
		/// </summary>
		public double right
		{
			get { return _left + _width; }
		}
	}
	#endregion

	/// <summary>
	/// ReportWindowBase.xaml の相互作用ロジック
	/// </summary>
	public partial class ReportPreviewWPF : WindowGeneralBase
	{
		double _top;
		/// <summary>
		/// デバッグ時座標確認用プロパティ
		/// </summary>
		public double 座標TOP
		{
			get { return _top; }
			set
			{
				_top = value;
				NotifyPropertyChanged();
			}
		}
		double _left;
		/// <summary>
		/// デバッグ時座標確認用プロパティ
		/// </summary>
		public double 座標LEFT
		{
			get { return _left; }
			set
			{
				_left = value;
				NotifyPropertyChanged();
			}
		}

		#region 線種クラス
		/// <summary>
		/// 線種クラス
		/// </summary>
		public class LineStyleComboItem : System.ComponentModel.INotifyPropertyChanged
		{
			/// <summary>
			/// 線種
			/// </summary>
			public LineStyle _lineStyle;
			/// <summary>
			/// 名前
			/// </summary>
			public string _name;
			/// <summary>
			/// 線種（バインド用）
			/// </summary>
			public LineStyle LineStyle
			{
				get { return this._lineStyle; }
				set
				{
					this._lineStyle = value;
					this.NotifyPropertyChanged();
				}
			}
			public string Name;

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
		#endregion

		#region 表示倍率クラス
		/// <summary>
		/// 表示倍率クラス
		/// </summary>
		public class PreviewRate : INotifyPropertyChanged
		{
			private string _倍率名;
			/// <summary>
			/// 倍率名
			/// </summary>
			public string 倍率名 { get { return this._倍率名; } set { this._倍率名 = value; NotifyPropertyChanged(); } }
			private double _倍率;
			/// <summary>
			/// 倍率
			/// </summary>
			public double 倍率 { get { return this._倍率; } set { this._倍率 = value; NotifyPropertyChanged(); } }

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
		#endregion

		#region 表示倍率
		private PreviewRate _表示倍率 = null;
		/// <summary>
		/// 表示倍率（バインド用）
		/// </summary>
		public PreviewRate 表示倍率
		{
			get { return this._表示倍率; }
			set
			{
				this._表示倍率 = value;
				NotifyPropertyChanged();
				if (this.targetReportDocument != null)
				{
					double newzoom = value.倍率;
					if (this.zoom != newzoom)
					{
						this.reportViewer.ViewerCore.Zoom((int)(value.倍率 * 100));
						this.zoom = newzoom;
						this.stLayout.ScaleX = zoom * drawScaleX;
						this.stLayout.ScaleY = zoom * drawScaleY;
					}
					if (this.movingFrame.Visibility == System.Windows.Visibility.Visible)
					{
						this.movingFrame.SetValue(Canvas.TopProperty, this.movingTopLeft.Y * this.zoom + this.offsetY);
						this.movingFrame.SetValue(Canvas.LeftProperty, this.movingTopLeft.X * this.zoom + this.offsetX);
						this.movingFrame.Width = this.movingFrameWidth * this.zoom * dpiX / 1440;
						this.movingFrame.Height = this.movingFrameHeight * this.zoom * dpiY / 1440;
					}
				}
			}
		}
		private List<PreviewRate> _表示倍率リスト = new List<PreviewRate>();
		public List<PreviewRate> 表示倍率リスト
		{
			get { return this._表示倍率リスト; }
			set { this._表示倍率リスト = value; NotifyPropertyChanged(); }
		}
		#endregion

		#region プリンター
		private List<string> _printerList = null;
		/// <summary>
		/// プリンターリスト（バインド用）
		/// </summary>
		public List<string> PrinterList
		{
			get { return this._printerList; }
			set { this._printerList = value; NotifyPropertyChanged(); }
		}
		private string _selectedPrinter = null;
		/// <summary>
		/// 選択されたプリンター（バインド用）
		/// </summary>
		public string SelectedPrinter
		{
			get { return this._selectedPrinter; }
			set
			{
				this._selectedPrinter = value;
				NotifyPropertyChanged();
				if (this.targetReportDocument != null)
				{
					this.targetReportDocument.PrintOptions.PrinterName = this._selectedPrinter;
					if (reportviewInitialized)
					{
						this.PreviewRefresh();
					}
				}
				this.printerName = value;
			}
		}
		#endregion

		#region オブジェクトツリー
		private List<TreeNode> _tree;
		/// <summary>
		/// レポートオブジェクトツリーデータ
		/// </summary>
		public List<TreeNode> Tree
		{
			get
			{
				return this._tree;
			}
			set
			{
				this._tree = value;
				this.NotifyPropertyChanged();
			}
		}
		/// <summary>
		/// レポートオブジェクト検索
		/// </summary>
		/// <param name="name">レポートオブジェクト名</param>
		/// <param name="tree">レポートオブジェクトツリーデータ</param>
		/// <returns>レポートオブジェクトノード</returns>
		public TreeNode FindNode(string name, List<TreeNode> tree = null)
		{
			if (tree == null) { tree = this.Tree; }
			foreach (TreeNode node in tree)
			{
				if (node.Name == name)
				{
					return node;
				}
				TreeNode item = this.FindNode(name, node.Children);
				if (item != null)
				{
					return item;
				}
			}
			return null;
		}

		#endregion

		#region オブジェクトのプロパティ

		private string _selectedFont = null;
		/// <summary>
		/// 選択されたフォント名
		/// </summary>
		public string SelectedFont
		{
			get
			{
				return this._selectedFont;
			}
			set
			{
				this._selectedFont = value;
				this.NotifyPropertyChanged();
			}
		}
		private string _selectFontSize = string.Empty;
		/// <summary>
		/// 選択されたフォントサイズ
		/// </summary>
		public string SelectedFontSize
		{
			get
			{
				return this._selectFontSize;
			}
			set
			{
				this._selectFontSize = value;
				this.NotifyPropertyChanged();
			}
		}
		private bool _selectedFontUnderLine = false;
		/// <summary>
		/// 選択されたフォント下線有無
		/// </summary>
		public bool SelectedFontUnderLine
		{
			get
			{
				return this._selectedFontUnderLine;
			}
			set
			{
				this._selectedFontUnderLine = value;
				this.NotifyPropertyChanged();
			}
		}
		private bool _selectedFontBold = false;
		/// <summary>
		/// 選択されたフォントの強調有無
		/// </summary>
		public bool SelectedFontBold
		{
			get
			{
				return this._selectedFontBold;
			}
			set
			{
				this._selectedFontBold = value;
				this.NotifyPropertyChanged();
			}
		}
		private bool _selectedFontItalic = false;
		/// <summary>
		/// 選択されたフォントの斜体有無
		/// </summary>
		public bool SelectedFontItalic
		{
			get
			{
				return this._selectedFontItalic;
			}
			set
			{
				this._selectedFontItalic = value;
				this.NotifyPropertyChanged();
			}
		}

		private List<LineStyleComboItem> lsComboList = new List<LineStyleComboItem>()
		{
			new LineStyleComboItem(){LineStyle = LineStyle.NoLine, Name="なし"},
			//new LineStyleComboItem(){LineStyle = LineStyle.BlankLine, Name="非表示"},
			new LineStyleComboItem(){LineStyle = LineStyle.SingleLine, Name="実線"},
			new LineStyleComboItem(){LineStyle = LineStyle.DashLine, Name="破線"},
			new LineStyleComboItem(){LineStyle = LineStyle.DotLine, Name="点線"},
			new LineStyleComboItem(){LineStyle = LineStyle.DoubleLine, Name="二重線"},
		};
		private List<string> _lsComboL = new List<string>();
		private List<string> _lsComboR = new List<string>();
		private List<string> _lsComboT = new List<string>();
		private List<string> _lsComboB = new List<string>();
		/// <summary>
		/// 枠線種（左）
		/// </summary>
		public List<string> lsComboL
		{
			get { return this._lsComboL; }
			set
			{
				this._lsComboL = value;
				this.NotifyPropertyChanged();
			}
		}
		/// <summary>
		/// 枠線種（右）
		/// </summary>
		public List<string> lsComboR
		{
			get { return this._lsComboR; }
			set
			{
				this._lsComboR = value;
				this.NotifyPropertyChanged();
			}
		}
		/// <summary>
		/// 枠線種（上）
		/// </summary>
		public List<string> lsComboT
		{
			get { return this._lsComboT; }
			set
			{
				this._lsComboT = value;
				this.NotifyPropertyChanged();
			}
		}
		/// <summary>
		/// 枠線種（下）
		/// </summary>
		public List<string> lsComboB
		{
			get { return this._lsComboB; }
			set
			{
				this._lsComboB = value;
				this.NotifyPropertyChanged();
			}
		}
		private string _selectedLS_L = string.Empty;
		private string _selectedLS_R = string.Empty;
		private string _selectedLS_T = string.Empty;
		private string _selectedLS_B = string.Empty;
		/// <summary>
		/// 選択された枠線種（左）
		/// </summary>
		public string SelectedLS_L
		{
			get { return this._selectedLS_L; }
			set
			{
				this._selectedLS_L = value;
				this.NotifyPropertyChanged();
			}
		}
		/// <summary>
		/// 選択された枠線種（右）
		/// </summary>
		public string SelectedLS_R
		{
			get { return this._selectedLS_R; }
			set
			{
				this._selectedLS_R = value;
				this.NotifyPropertyChanged();
			}
		}
		/// <summary>
		/// 選択された枠線種（上）
		/// </summary>
		public string SelectedLS_T
		{
			get { return this._selectedLS_T; }
			set
			{
				this._selectedLS_T = value;
				this.NotifyPropertyChanged();
			}
		}
		/// <summary>
		/// 選択された枠線種（下）
		/// </summary>
		public string SelectedLS_B
		{
			get { return this._selectedLS_B; }
			set
			{
				this._selectedLS_B = value;
				this.NotifyPropertyChanged();
			}
		}

		private List<ComboboxColor> _foreColorList;
		/// <summary>
		/// 前景色
		/// </summary>
		public List<ComboboxColor> ForeColorList
		{
			get
			{
				return this._foreColorList;
			}
			set
			{
				this._foreColorList = value;
				this.NotifyPropertyChanged();
			}
		}
		private ComboboxColor _selectedForeColor;
		/// <summary>
		/// 選択された前景色
		/// </summary>
		public ComboboxColor SelectedForeColor
		{
			get { return _selectedForeColor; }
			set
			{
				_selectedForeColor = value;
				this.NotifyPropertyChanged();
			}
		}
		/// <summary>
		/// 前景色を設定する
		/// </summary>
		/// <param name="name">色名</param>
		public void SetSelectedForeColor(string name)
		{
			foreach (ComboboxColor clr in this.ForeColorList)
			{
				if (clr.Text == name)
				{
					this.SelectedForeColor = clr;
					return;
				}
			}
			SelectedForeColor = null;
		}
		private List<ComboboxColor> _frameColorList;
		/// <summary>
		/// 枠色
		/// </summary>
		public List<ComboboxColor> FrameColorList
		{
			get
			{
				return this._frameColorList;
			}
			set
			{
				this._frameColorList = value;
				this.NotifyPropertyChanged();
			}
		}
		private ComboboxColor _selectedFrameColor;
		/// <summary>
		/// 選択された枠色
		/// </summary>
		public ComboboxColor SelectedFrameColor
		{
			get { return _selectedFrameColor; }
			set
			{
				_selectedFrameColor = value;
				this.NotifyPropertyChanged();
			}
		}
		/// <summary>
		/// 枠色を設定する
		/// </summary>
		/// <param name="name">色名</param>
		public void SetSelectedFrameColor(string name)
		{
			foreach (ComboboxColor clr in this.FrameColorList)
			{
				if (clr.Text == name)
				{
					this.SelectedFrameColor = clr;
					return;
				}
			}
			SelectedFrameColor = null;
		}
		#endregion

		#region ページ制御関連

		private bool _buttonBackIsEnabled = false;
		/// <summary>
		/// 戻るボタンの有効化
		/// </summary>
		public bool ButtonBackIsEnabled
		{
			get
			{
				return this._buttonBackIsEnabled;
			}
			set
			{
				this._buttonBackIsEnabled = value;
				this.NotifyPropertyChanged();
			}
		}

		private int _currentPage = 0;
		/// <summary>
		/// 現在ページ番号
		/// </summary>
		public int CurrentPage
		{
			get { return _currentPage; }
			set { _currentPage = value; NotifyPropertyChanged(); }
		}

		private int _lastPage = 0;
		/// <summary>
		/// 最大ページ数
		/// </summary>
		public int LastPage
		{
			get { return _lastPage; }
			set { _lastPage = value; NotifyPropertyChanged(); }
		}

		#endregion

		#region 内部変数
		bool reportviewInitialized = false;

		List<ReportParameter> parameters = null;

		/// <summary>
		/// CrystalReportsの定義オブジェクト
		/// </summary>
		private ReportDocument targetReportDocument = null;
		private List<ReportObject> historys = new List<ReportObject>();

		private int PreviewWidth = 1000;
		private int PreviewHeight = 1000;

		private double movingFrameWidth = 0;
		private double movingFrameHeight = 0;
		private double zoom = 1.0;

		/// <summary>
		/// CrystalReportsのレポート定義ファイルパス
		/// </summary>
		private string reportFilePath = string.Empty;
		/// <summary>
		/// 印刷先のプリンター名
		/// 空文字の場合は「通常使用するプリンター」を対象とする
		/// </summary>
		private string printerName = string.Empty;

		//private bool IsDesignable = false;
		/// <summary>
		/// レポート名
		/// </summary>
		public string ReportName
		{
			get;
			set;
		}

		/// <summary>
		/// 印刷先のプリンター名
		/// <remarks>
		/// 空文字の場合は「通常使用するプリンター」を対象とする
		/// </remarks>
		/// </summary>
		public string PrinterName
		{
			get { return this.printerName; }
			set
			{
				this.printerName = null;
				foreach (var prt in this.PrinterList)
				{
					if (value == prt)
					{
						this.printerName = value;
						break;
					}
				}
				if (string.IsNullOrWhiteSpace(this.printerName))
				{
					System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
					this.printerName = pd.PrinterSettings.PrinterName;
				}
				this.SelectedPrinter = this.printerName;
			}
		}

		private ReportObject selectedObject = null;

		// マウスでフィールドを移動している間のフラグ
		// 画面解像度
		private float dpiX, dpiY;
		// ViewerのTopLeft位置
		private double offsetX = 6;
		private double offsetY = 30;
		private Point viewerTopLeft = new Point(26, 11);
		private Point movingTopLeft = new Point(0, 0);
		private Point lastPosition = new Point(0, 0);
		private bool isMoving = false;

		// 一回でも変更した場合 true にする
		private bool IsChanged = false;

		SectInfo[] sectInfos = null;
		List<Label> sectLabels = new List<Label>();

		List<ObjectInfo> objectList = new List<ObjectInfo>();

		// デザインエリアのフォントの拡大率
		const double fontSizeScale = 19.2;
		// デザインエリアの座標系縮小率
		const double drawScaleX = 0.069;
		const double drawScaleY = 0.073;

		string cursorObjectName = string.Empty;
		const double innerFrameMargin = 20.0;

		#endregion

		#region 外部インターフェース項目
		/// <summary>
		/// 印刷を実行したかどうかの状態
		/// </summary>
		public bool IsPrinted = false;
		/// <summary>
		/// レポート定義ファイルを保存したかどうかの状態
		/// </summary>
		public bool IsSaved = false;
		/// <summary>
		/// 再表示を要求するかどうかのフラグ
		/// <remarks>
		/// 変更した内容を取り消して再表示する場合にセットし、呼び出し元が再度表示する。
		/// </remarks>
		/// </summary>
		public bool IsReload = false;
		/// <summary>
		/// 初期状態へのリセットを要求するかどうかのフラグ
		/// <remarks>
		/// 保存済みの変更を含めて取り消ししてからインストール時の状態に戻す場合にセットし、呼び出し元が復旧処理を行う
		/// </remarks>
		/// </summary>
		public bool IsInitialRecovered = false;
		#endregion

		#region 画面初期処理

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ReportPreviewWPF()
		{
			InitializeComponent();

			this.表示倍率リスト = new List<PreviewRate>()
			{
				new	PreviewRate(){ 倍率=0.75D, 倍率名="75%",},
				new	PreviewRate(){ 倍率=1.0D, 倍率名="100%",},
				new	PreviewRate(){ 倍率=1.5D, 倍率名="150%",},
				new	PreviewRate(){ 倍率=2.0D, 倍率名="200%",},
				new	PreviewRate(){ 倍率=4.0D, 倍率名="400%",},
			};
			this.表示倍率 = (from x in this.表示倍率リスト where x.倍率 == 1.0D select x).FirstOrDefault();

			this.PrinterList = new List<string>();
			foreach (string pnm in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
			{
				this.PrinterList.Add(pnm);
			}

			foreach (var item in lsComboList)
			{
				lsComboL.Add(item.Name);
				lsComboR.Add(item.Name);
				lsComboT.Add(item.Name);
				lsComboB.Add(item.Name);
			}

			this.propertyPanel.Visibility = System.Windows.Visibility.Collapsed;
			this.DataContext = this;

			var wih = new System.Windows.Interop.WindowInteropHelper(this);
			var desktop = System.Drawing.Graphics.FromHwnd(wih.Handle);
			this.dpiX = desktop.DpiX;
			this.dpiY = desktop.DpiY;
			this.movingFrame.Visibility = System.Windows.Visibility.Collapsed;

		}

		private void ComboBox_Loaded(object sender, RoutedEventArgs e)
		{
			//var cbox = sender as ComboBox;
			//if (cbox != null && cbox.Tag != null)
			//{
			//	var tbox = cbox.Template.FindName("PART_EditableTextBox", cbox) as TextBox;
			//	int len;
			//	if (tbox != null && int.TryParse(cbox.Tag.ToString(), out len))
			//		tbox.MaxLength = len;
			//}
		}

		/// <summary>
		/// 画面表示（通常ダイアログモード）
		/// </summary>
		/// <returns></returns>
		public bool? ShowDesigner()
		{
			if (this.targetReportDocument == null)
			{
			}
			bool? ret = ShowDialog();
			return ret;
		}

		/// <summary>
		/// 画面Load時のイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//画面サイズをタスクバーをのぞいた状態で表示させる
			this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;

			this.reportViewer.ShowToolbar = false;
			this.propertyPanel.Visibility = System.Windows.Visibility.Visible;
			this.toolpannel.Visibility = System.Windows.Visibility.Visible;

			List<ComboboxColor> clist = new List<ComboboxColor>();
			List<ComboboxColor> clistF = new List<ComboboxColor>();

			PropertyInfo[] colorProps = typeof(Colors).GetProperties();
			foreach (PropertyInfo info in colorProps)
			{
				System.Windows.Media.Color clr = (System.Windows.Media.Color)ColorConverter.ConvertFromString(info.Name);
				System.Drawing.Color dclr = System.Drawing.Color.FromName(info.Name);
				ComboboxColor cmbclr = new ComboboxColor { Color = clr, DrawingColor = dclr, Text = info.Name };
				clist.Add(cmbclr);
				clistF.Add(cmbclr);
			}
			this.ForeColorList = clist;
			this.FrameColorList = clistF;
			this.Tree = new List<TreeNode>();

			// デザインエリアのピックアップフレームの調整
			if (this.grDesigner != null)
			{
				double height = grdViewArea.ActualHeight;
				foreach (var row in grdViewArea.RowDefinitions)
				{
					if (row.Name == this.grDesigner.Name)
					{
						break;
					}
					height -= row.ActualHeight;
				}
				// スプリッター分の補正
				height -= 10;
				this.grDesigner.MaxHeight = height;
				this.grDesigner.Height = new GridLength(height);
			}
		}
		#endregion

		#region レポート初期処理

		public void MakeReport(string reportName, string reportfile, double marginLeft, double marginTop, int pageRowCount)
		{
			try
			{
				FileInfo fi = new FileInfo(reportfile);
				fi.CopyTo(reportfile + ".ORG", true);
				this.viewerTopLeft.Y = marginTop;
				this.viewerTopLeft.X = marginLeft;

				this.targetReportDocument = new ReportDocument();
				if (string.IsNullOrWhiteSpace(this.PrinterName) != true)
				{
					this.targetReportDocument.PrintOptions.PrinterName = this.PrinterName;
				}
				this.targetReportDocument.Load(reportfile);
				this.targetReportDocument.SummaryInfo.ReportTitle = reportName;
				this.reportFilePath = reportfile;
				this.ReportName = reportName;
				this.Title = reportName;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void SetupParmeters(List<ReportParameter> prms)
		{
			try
			{
				parameters = prms;
				this.targetReportDocument.Refresh();
				foreach (var prm in prms)
				{
					try
					{
						this.targetReportDocument.SetParameterValue(prm.PNAME, prm.VALUE);
					}
					catch (Exception)
					{
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void reportViewer_Loaded(object sender, RoutedEventArgs e)
		{
			DisplayObjectTree();

			DisplayLayout();

			reportviewInitialized = true;
		}

		void ViewerCore_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			this.PreviewWidth = (int)this.reportViewer.ViewerCore.ActualWidth;
			this.PreviewHeight = (int)this.reportViewer.ViewerCore.ActualHeight;
			//this.canvasPreview.Width = this.PreviewWidth;
			//this.canvasPreview.Height = this.PreviewHeight;
		}


		public void SetReportData(DataSet data)
		{
			try
			{
				this.targetReportDocument.SetDataSource(data);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void SetReportData(DataTable data)
		{
			try
			{
				this.targetReportDocument.SetDataSource(data);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void SetReportData(object data)
		{
			try
			{
				this.targetReportDocument.SetDataSource(data as Enumerable);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		#endregion

		#region オブジェクトツリー制御
		/// <summary>
		/// レポートオブジェクトのツリービュー表示
		/// </summary>
		private void DisplayObjectTree()
		{
			try
			{
				this.reportViewer.ViewerCore.ReportSource = this.targetReportDocument;
				this.reportViewer.ShowLogo = false;

				var x = this.targetReportDocument.ReportDefinition.ReportObjects;

				List<TreeNode> root = new List<TreeNode>();
				TreeNode nodeSect;
				TreeNode nodeData;
				TreeNode nodeLines;
				TreeNode nodeFixed;
				TerminalNode termnode;

				foreach (CrystalDecisions.CrystalReports.Engine.Section sect in this.targetReportDocument.ReportDefinition.Sections)
				{
					bool sectvisible = !(sect.SectionFormat.EnableSuppress);
					if (sect.SectionFormat.EnableSuppress || sect.Height == 0)
					{
						continue;
					}

					nodeSect = new TreeNode() { Name = sect.Name, SectionName = sect.Name, Visible = sectvisible };
					root.Add(nodeSect);
					nodeData = null;
					nodeFixed = null;
					nodeLines = null;
					foreach (CrystalDecisions.CrystalReports.Engine.ReportObject item in sect.ReportObjects)
					{
						Type tp = item.GetType();
						try
						{
							bool nodevisible = !(item.ObjectFormat.EnableSuppress);
							termnode = new TerminalNode() { Name = item.Name, SectionName = sect.Name, Visible = nodevisible, Y = item.Top, X = item.Left };
							switch (item.Kind)
							{
							case CrystalDecisions.Shared.ReportObjectKind.FieldObject:
                            // 追加
                            case CrystalDecisions.Shared.ReportObjectKind.BlobFieldObject:
                                if (nodeData == null)
								{
									nodeData = new TreeNode() { Name = "データフィールド", SectionName = sect.Name };
									nodeSect.Children.Add(nodeData);
								}
								nodeData.Children.Add(termnode);
								break;
							case CrystalDecisions.Shared.ReportObjectKind.FieldHeadingObject:
							case CrystalDecisions.Shared.ReportObjectKind.TextObject:
                            // 追加
                            //case CrystalDecisions.Shared.ReportObjectKind.PictureObject:
                                if (nodeFixed == null)
								{
									nodeFixed = new TreeNode() { Name = "固定テキスト", SectionName = sect.Name };
									nodeSect.Children.Add(nodeFixed);
								}
								nodeFixed.Children.Add(termnode);
								break;
							case CrystalDecisions.Shared.ReportObjectKind.BoxObject:
							case CrystalDecisions.Shared.ReportObjectKind.LineObject:
								if (nodeLines == null)
								{
									nodeLines = new TreeNode() { Name = "線", SectionName = sect.Name };
									nodeSect.Children.Add(nodeLines);
								}
								nodeLines.Children.Add(termnode);
								break;
							}
						}
						catch (Exception ex)
						{
						}
					}
				}

				this.Tree.Clear();
				foreach (TreeNode ndR in root)
				{
					foreach (TreeNode nd in ndR.Children)
					{
						if (nd.Children.Count == 0)
						{
							ndR.Children.Remove(nd);
							continue;
						}
						nd.Children.Sort(comparePosition);
					}
					if (ndR.Children.Count > 0)
					{
						this.Tree.Add(ndR);
					}
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		int comparePosition(TreeNode a, TreeNode b)
		{
			// 名前順に並べる
			return string.Compare(a.Name, b.Name);

			// 座標順に並べる
			//int dy = a.Y - b.Y;
			//int dx = a.X - b.X;

			//if (dy != 0)
			//{
			//	return dy;
			//}
			//return dx;
		}

		/// <summary>
		/// ツリービューでの選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvReportObject_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			try
			{
				var tvitem = e.Source as TreeView;
				if (tvitem != null) tvitem.BringIntoView();
				tvReportObject.Focus();
				TreeNode item = this.tvReportObject.SelectedItem as TreeNode;
				foreach (ReportObject obj in this.targetReportDocument.ReportDefinition.ReportObjects)
				{
					if (obj.Name == item.Name)
					{
						//selectedObject = obj;
						DisplaySelectedInfo(item.Name);
						DisplayLayoutObject(selectedObject, true);
						return;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// オブジェクトの表示状態切り替え
		/// </summary>
		/// <param name="cbx"></param>
		/// <param name="visible"></param>
		private void ChangeVisibleObjectByCheckbox(CheckBox cbx, bool visible)
		{
			try
			{
				var dat = cbx.Parent;
				var ctx = (dat as StackPanel).DataContext;
				var item = this.targetReportDocument.ReportDefinition.ReportObjects[(ctx as TreeNode).Name];
				item.ObjectFormat.EnableSuppress = visible;
				DisplayLayoutObject(item, visible);
				PreviewRefresh();
				IsChanged = true;
			}
			catch (Exception)
			{
				return;
			}

			return;
		}

		/// <summary>
		/// オブジェクトの非表示切り替え
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			ChangeVisibleObjectByCheckbox(sender as CheckBox, true);
		}

		/// <summary>
		/// オブジェクトの表示切替
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			ChangeVisibleObjectByCheckbox(sender as CheckBox, false);
		}


		#endregion

		#region デザインエリア制御

		/// <summary>
		/// デザインエリア表示
		/// </summary>
		private void DisplayLayout()
		{
			this.rptLayout.Height = this.targetReportDocument.PrintOptions.PageContentHeight;
			this.rptLayout.Width = this.targetReportDocument.PrintOptions.PageContentWidth;

			//this.variablesectionHeight = 0;
			double offset = 0;
			sectInfos = new SectInfo[this.targetReportDocument.ReportDefinition.Sections.Count];
			sectLabels = new List<Label>();

			int idx = 0;
			foreach (CrystalDecisions.CrystalReports.Engine.Section sect in this.targetReportDocument.ReportDefinition.Sections)
			{
				sectInfos[idx] = new SectInfo() { name = sect.Name, height = sect.Height, offset = offset, width = (int)this.rptLayout.Width, };
				if (sect.SectionFormat.EnableSuppress || sect.Height == 0)
				{
					idx++;
					continue;
				}

				Label slbl = new Label();
				slbl.Name = sect.Name;
				slbl.Height = 256;
				slbl.Width = this.rptLayout.Width;
				slbl.BorderThickness = new Thickness(20);
				slbl.BorderBrush = Brushes.LightBlue;
				slbl.Background = Brushes.Aquamarine;
				slbl.Foreground = Brushes.Red;
				slbl.Content = string.Format("{0}", sect.Name);
				slbl.FontSize = 128;
				slbl.FontWeight = FontWeights.Bold;
				slbl.FontStyle = FontStyles.Italic;
				slbl.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
				slbl.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
				slbl.Padding = new Thickness(50, 0, 0, 0);

				slbl.MouseDown += slbl_MouseDown;
				sectLabels.Add(slbl);

				this.rptLayout.Children.Add(slbl);
				slbl.SetValue(Canvas.TopProperty, (double)(offset));
				slbl.SetValue(Canvas.LeftProperty, (double)(0));
				slbl.SetValue(Canvas.ZIndexProperty, 99);

				offset += slbl.Height;
				sectInfos[idx].offset = offset;
				offset += (sect.Height);

				idx++;
			}
			rptLayout.Height = offset;

			this.stLayout.ScaleX = drawScaleX;
			this.stLayout.ScaleY = drawScaleY;
			

			foreach (CrystalDecisions.CrystalReports.Engine.Section sect in this.targetReportDocument.ReportDefinition.Sections)
			{
				offset = (from s in sectInfos where s.name == sect.Name select s.offset).First();
				foreach (CrystalDecisions.CrystalReports.Engine.ReportObject item in sect.ReportObjects)
				{
					bool visible = !(item.ObjectFormat.EnableSuppress);
					var oinfo = new ObjectInfo()
					{
						name = item.Name, sectionname = sect.Name, visible = visible, 
						control = null,
					};
					objectList.Add(oinfo);
					DisplayLayoutObject(item, visible);
				}
			}

		}

		/// <summary>
		/// レポートオブジェクトのコンテンツをデザインエリアに表示
		/// </summary>
		/// <param name="item"></param>
		/// <param name="visible"></param>
		private void DisplayLayoutObject(ReportObject item, bool visible)
		{
			try
			{
				var oinfo = (from o in objectList where o.name == item.Name select o).First();
				double offset = (from s in sectInfos where s.name == oinfo.sectionname select s.offset).First();

				RptObjectLabel robj = new RptObjectLabel(item, sectInfos, oinfo, appLog);
				if (oinfo.control != null)
				{
					this.rptLayout.Children.Remove((UIElement)oinfo.control);
				}
				this.rptLayout.Children.Add(robj);
				oinfo.control = robj;

			}
			catch (Exception ex)
			{
			}

		}

		/// <summary>
		/// デザインエリアのマウス移動時のイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rptLayout_MouseMove(object sender, MouseEventArgs e)
		{
			if (isMoving)
			{
				return;
			}

			UIElement el = sender as UIElement;
			var pos = e.GetPosition(el);
			// 優先順は、縦線 ⇒ 横線 ⇒ フィールド ⇒ 枠線
			var item = (from c in objectList where c.objectType == ObjectInfoType.VLine && (c.top - innerFrameMargin) <= pos.Y && (c.bottom + (innerFrameMargin * 2)) >= pos.Y && (c.left - innerFrameMargin) <= pos.X && (c.right + (innerFrameMargin * 2)) >= pos.X orderby c.top, c.left select c).FirstOrDefault();
			if (item == null)
			{
				item = (from c in objectList where c.objectType == ObjectInfoType.HLine && (c.top - innerFrameMargin) <= pos.Y && (c.bottom + (innerFrameMargin * 2)) >= pos.Y && (c.left - innerFrameMargin) <= pos.X && (c.right + (innerFrameMargin * 2)) >= pos.X orderby c.top, c.left select c).FirstOrDefault();
			}
			if (item == null)
			{
				item = (from c in objectList where c.objectType == ObjectInfoType.Label && (c.top - innerFrameMargin) <= pos.Y && (c.bottom + (innerFrameMargin * 2)) >= pos.Y && (c.left - innerFrameMargin) <= pos.X && (c.right + (innerFrameMargin * 2)) >= pos.X orderby c.top, c.left select c).FirstOrDefault();
			}
			if (item == null)
			{
				item = (from c in objectList where c.objectType == ObjectInfoType.Box && (c.top - innerFrameMargin) <= pos.Y && (c.bottom + (innerFrameMargin * 2)) >= pos.Y && (c.left - innerFrameMargin) <= pos.X && (c.right + (innerFrameMargin * 2)) >= pos.X orderby c.top, c.left select c).FirstOrDefault();
			}
			if (item == null)
			{
				this.movingFrame.Visibility = Visibility.Collapsed;
			}
			else
			{
				cursorObjectName = item.name;
				DisplaySelectedInfo(cursorObjectName);
				this.movingFrame.SetValue(Canvas.TopProperty, (item.top - innerFrameMargin));
				this.movingFrame.SetValue(Canvas.LeftProperty, (item.left - innerFrameMargin));
				this.movingFrame.Height = item.height + (innerFrameMargin * 2);
				this.movingFrame.Width = item.width + (innerFrameMargin * 2);
				this.movingFrame.Visibility = Visibility.Visible;
			}

		}

		void slbl_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Label lbl = sender as Label;
		}

		private void rptLayout_ScaleChanged(object sender, EventArgs e)
		{
			rptLayoutBack.Width = this.rptLayout.Width * this.stLayout.ScaleX + this.rptLayout.Margin.Left + this.rptLayout.Margin.Right;
			rptLayoutBack.Height = this.rptLayout.Height * this.stLayout.ScaleY + this.rptLayout.Margin.Top + this.rptLayout.Margin.Bottom;
		}

		#region オブジェクト移動用フレーム処理

		/// <summary>
		/// マウスボタンダウン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void movingFrame_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(cursorObjectName))
			{
				return;
			}
			if (isMoving)
			{
				return;
			}
			UIElement el = sender as UIElement;
			if (el != null)
			{
				isMoving = true;
				this.lastPosition = e.GetPosition(el);
				el.CaptureMouse();
			}
		}

		/// <summary>
		/// マウスボタンアップ
		/// </summary>
		/// <param name="sender">イベント受信対象</param>
		/// <param name="e">イベントパラメータ</param>
		private void movingFrame_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (isMoving)
			{
				UIElement el = sender as UIElement;
				var pos = e.GetPosition(el);
				//if (pos.X != this.lastPosition.X || pos.Y != this.lastPosition.Y)
				{
					el.ReleaseMouseCapture();
					isMoving = false;
					MoveObject();
				}
			}
		}

		/// <summary>
		/// 移動用のフレームを掴んだ状態でマウス移動（ドラッグ）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void movingFrame_MouseMove(object sender, MouseEventArgs e)
		{
			if (isMoving)
			{
				Point pt = Mouse.GetPosition(this.rptLayout);
				UIElement el = sender as UIElement;

				double top = pt.Y - this.lastPosition.Y;
				double left = pt.X - this.lastPosition.X;
				var item = objectList.Where(x => x.name == this.selectedObject.Name).First();
				var sect = this.sectInfos.Where(s => s.name == item.sectionname).First();
				double bottom = top + item.height;
				double right = left + item.width;
				if (!item.extendSection)
				{
					// １つのセクション内のオブジェクト
					// 上の限界域
					if (sect.offset >= (top + innerFrameMargin))
					{
						top = sect.offset - innerFrameMargin;
					}
					else if ((sect.offset + sect.height) < (top - innerFrameMargin + this.movingFrame.Height))
					{
						top = sect.offset + sect.height + innerFrameMargin - this.movingFrame.Height;
					}
					else
					{
						// 下の限界域
						if ((sect.offset + sect.height) < bottom)
							top = sect.offset + sect.height - item.height;
					}
				}
				else
				{
					// 複数のセクションにまたがるオブジェクト
					var sectE = this.sectInfos.Where(s => s.name == item.endSectionName).First();
					// 上の限界域
					if (sect.offset >= (top + innerFrameMargin))
					{
						// 下の限界域
						if (bottom < sectE.offset)
						{
							e.Handled = true;
							return;
						}
						top = sect.offset - innerFrameMargin;
					}
					else if ((sect.offset + sect.height) < (top + innerFrameMargin))
					{
						// 下の限界域
						if (bottom > (sectE.offset + sectE.height))
						{
							e.Handled = true;
							return;
						}
						top = sect.offset + sect.height - innerFrameMargin;
					}
					else
					{
						// 下の限界域
						if (bottom > (sectE.offset + sectE.height))
						{
							top = sectE.offset + sectE.height - item.height - innerFrameMargin;
						}
						if (bottom < sectE.offset)
						{
							e.Handled = true;
							return;
						}
					}
				}
				// 左の限界域
				if ((left + innerFrameMargin) < 0)
					left = 0 - innerFrameMargin;
				// 右の限界域
				if ((sect.width - 200) <= left)
					left = sect.width - 200;

				Canvas.SetLeft(el, left);
				Canvas.SetTop(el, top);

				座標LEFT = left;
				座標TOP = top;
			}

			return;
		}

		private void movingFrame_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				if (isMoving)
				{
					isMoving = false;
					this.movingFrame.ReleaseMouseCapture();
					this.movingFrame.Visibility = System.Windows.Visibility.Collapsed;
				}
			}
			else if (e.Key == Key.Subtract || e.Key == Key.Add || e.Key == Key.OemMinus || e.Key == Key.OemPlus)
			{
				e.Handled = true;
			}
		}

		#endregion

		#region レポートオブジェクトの変更処理

		/// <summary>
		/// オブジェクトの移動
		/// </summary>
		private void MoveObject()
		{
			if (this.selectedObject == null)
			{
				return;
			}
			try
			{
				ReportObject cur = this.selectedObject;

				int top = Convert.ToInt32(this.movingFrame.GetValue(Canvas.TopProperty)) + (int)innerFrameMargin;
				int left = Convert.ToInt32(this.movingFrame.GetValue(Canvas.LeftProperty)) + (int)innerFrameMargin;
				int bottom = top + Convert.ToInt32(this.movingFrame.Height) - (int)(innerFrameMargin * 2);
				int right = left + Convert.ToInt32(this.movingFrame.Width) - (int)(innerFrameMargin * 2);

				var item = (from o in this.objectList where o.name == cur.Name select o).FirstOrDefault();
				item.top = top;
				item.left = left;

				// セクションのY方向オフセット補正
				var si = (from s in this.sectInfos where s.name == item.sectionname select s).First();
				top -= (int)si.offset;
				bottom -= (int)si.offset;
				if (top == cur.Top && left == cur.Left)
				{
					// 位置が変化しない場合はプロパティの入力フィールドに移動
					this.pTop.SetFocus();
					Point pt = this.pTop.PointFromScreen(new Point(0.0d, 0.0d));
					User32.MouseMove.SetPosition((int)Math.Abs(pt.X) + 2, (int)Math.Abs(pt.Y) + 2);
					return;
				}

				switch (cur.Kind)
				{
				case ReportObjectKind.BoxObject:
					BoxObject box = cur as BoxObject;
					box.Top = top;
					if (item.extendSection)
					{
						var sectE = (from s in this.sectInfos where s.name == item.endSectionName select s).First();
						bottom = (bottom + (int)si.offset) - (int)sectE.offset;
						if (bottom < 0)
							bottom = 0;
					}
					box.Bottom = bottom;
					box.Left = left;
					box.Right = right;

					break;
				case ReportObjectKind.LineObject:
					this.pTop.Text = string.Format("{0}", top);
					this.pLeft.Text = string.Format("{0}", left);
					if (item.extendSection)
					{
						var sectE = (from s in this.sectInfos where s.name == item.endSectionName select s).First();
						bottom = (bottom + (int)si.offset) - (int)sectE.offset;
						if (bottom < 0)
							bottom = 0;
					}
					if (item.objectType == ObjectInfoType.VLine)
					{
						this.pRight.Text = this.pLeft.Text;
						this.pBottom.Text = string.Format("{0}", bottom);
					}
					else
					{
						this.pRight.Text = string.Format("{0}", right);
						this.pBottom.Text = this.pTop.Text;
					}
					this.btnItemProperyUpdate_Click(null, null);
					return;

					break;
				case ReportObjectKind.FieldHeadingObject:
					FieldHeadingObject fldh = cur as FieldHeadingObject;
					fldh.Top = top;
					fldh.Left = left;

					break;
				case ReportObjectKind.FieldObject:
					FieldObject fld = cur as FieldObject;
					fld.Top = top;
					fld.Left = left;

					break;
				case ReportObjectKind.TextObject:
					TextObject txt = cur as TextObject;
					txt.Top = top;
					txt.Left = left;

					break;
				case ReportObjectKind.PictureObject:
					PictureObject pic = cur as PictureObject;
					pic.Top = top;
					pic.Left = left;

					break;
                // 追加
                case ReportObjectKind.BlobFieldObject:
                    BlobFieldObject Blob = cur as BlobFieldObject;
                    Blob.Top = top;
                    Blob.Left = left;

                    break;
                default:
					break;
				}
				this.spProperties.Visibility = Visibility.Collapsed;

				PreviewRefresh();
				IsChanged = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show("ReportRefresh error:" + ex.Message);
			}

			return;

		}

		#endregion

		/// <summary>
		/// イベント：CrystalReportプレビューのフィールド選択時に発生する。
		/// </summary>
		/// <param name="sender">イベント受信対象</param>
		/// <param name="e">イベントパラメータ</param>
		private void reportViewer_SelectionChange(object sender, RoutedEventArgs e)
		{
		}

		#region レポートオブジェクトの情報表示

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		private void DisplaySelectedInfo(string name)
		{
			var ctl = (from c in objectList where c.name == name select c).FirstOrDefault();
			if (ctl == null)
			{
				return;
			}
			selectedObject = this.targetReportDocument.ReportDefinition.ReportObjects[name];

			this.pFieldName.Content = name;
			this.pTop.cText = string.Empty;
			this.pLeft.cText = string.Empty;
			this.pWidth.cText = string.Empty;
			this.pHeight.cText = string.Empty;
			this.pTextValue.Text = string.Empty;

			try
			{
				double height = 0;
				string sectname = string.Empty;
				ReportObject cur = null;
				foreach (CrystalDecisions.CrystalReports.Engine.Section sect in targetReportDocument.ReportDefinition.Sections)
				{
					if (sect.Name == name)
					{
						this.spProperties.Visibility = Visibility.Collapsed;
						return;
					}
					// 現Sectionからオブジェクトを検索
					foreach (ReportObject obj in sect.ReportObjects)
					{
						if (obj.Name == name)
						{
							cur = obj;
							sectname = sect.Name;
							break;
						}
					}
					if (cur != null)
					{
						break;
					}

					if (sect.Height > 0)
					{
						height += ((double)sect.Height * (double)dpiY) / 1440;
					}
				}
				if (cur == null)
				{
					return;
				}

				// TreeViewと同調させる
				TreeNode selnode = this.FindNode(name);
				if (selnode == null)
				{
					return;
				}
				selnode.IsSelected = true;
				

				this.SelectedLS_R = GetLineStyleName(cur.Border.RightLineStyle);
				this.SelectedLS_L = GetLineStyleName(cur.Border.LeftLineStyle);
				this.SelectedLS_T = GetLineStyleName(cur.Border.TopLineStyle);
				this.SelectedLS_B = GetLineStyleName(cur.Border.BottomLineStyle);
				this.pThickness.Text = "";
				switch (cur.Kind)
				{
				case ReportObjectKind.FieldHeadingObject:
					FieldHeadingObject fldh = cur as FieldHeadingObject;
					this.pTop.cText = fldh.Top.ToString();
					this.pLeft.cText = fldh.Left.ToString();
					this.pWidth.cText = fldh.Width.ToString();
					this.pHeight.cText = fldh.Height.ToString();
					this.SetSelectedForeColor(fldh.Color.Name);
					this.SetSelectedFrameColor(fldh.Border.BorderColor.Name);
					this.pTextValue.Text = fldh.Text;
					this.pTextValue.IsEnabled = true;
					this.spProperties.Visibility = Visibility.Visible;
					this.spWH.Visibility = Visibility.Visible;
					this.spBR.Visibility = Visibility.Collapsed;
					this.chkFrame.Visibility = Visibility.Visible;
					this.colorFore.Visibility = Visibility.Visible;
					this.colorFrame.Visibility = Visibility.Visible;
					this.fontFrame.Visibility = Visibility.Visible;
					this.cboxColorBorder.Visibility = System.Windows.Visibility.Visible;
					this.SelectedFont = fldh.Font.FontFamily.Name;
					this.SelectedFontSize = fldh.Font.Size.ToString();
					this.SelectedFontUnderLine = fldh.Font.Style.HasFlag(System.Drawing.FontStyle.Underline) ? true : false;
					this.SelectedFontBold = fldh.Font.Style.HasFlag(System.Drawing.FontStyle.Bold) ? true : false;
					this.SelectedFontItalic = fldh.Font.Style.HasFlag(System.Drawing.FontStyle.Italic) ? true : false;
					this.spTH.Visibility = System.Windows.Visibility.Collapsed;
					this.spCorner.Visibility = System.Windows.Visibility.Collapsed;

					break;
				case ReportObjectKind.FieldObject:
					FieldObject fld = cur as FieldObject;
					this.pTop.cText = fld.Top.ToString();
					this.pLeft.cText = fld.Left.ToString();
					this.pWidth.cText = fld.Width.ToString();
					this.pHeight.cText = fld.Height.ToString();
					this.SetSelectedForeColor(fld.Color.Name);
					this.SetSelectedFrameColor(fld.Border.BorderColor.Name);
					this.pTextValue.Text = fld.DataSource.FormulaName;
					this.pTextValue.IsEnabled = false;
					this.spProperties.Visibility = Visibility.Visible;
					this.spWH.Visibility = Visibility.Visible;
					this.spBR.Visibility = Visibility.Collapsed;
					this.colorFore.Visibility = Visibility.Visible;
					this.chkFrame.Visibility = Visibility.Visible;
					this.colorFrame.Visibility = Visibility.Visible;
					this.fontFrame.Visibility = Visibility.Visible;
					this.cboxColorBorder.Visibility = System.Windows.Visibility.Visible;
					this.SelectedFont = fld.Font.FontFamily.Name;
					this.SelectedFontSize = fld.Font.Size.ToString();
					this.SelectedFontUnderLine = fld.Font.Style.HasFlag(System.Drawing.FontStyle.Underline) ? true : false;
					this.SelectedFontBold = fld.Font.Style.HasFlag(System.Drawing.FontStyle.Bold) ? true : false;
					this.SelectedFontItalic = fld.Font.Style.HasFlag(System.Drawing.FontStyle.Italic) ? true : false;
					this.spTH.Visibility = System.Windows.Visibility.Collapsed;
					this.spCorner.Visibility = System.Windows.Visibility.Collapsed;

					break;
				case ReportObjectKind.TextObject:
					TextObject txt = cur as TextObject;
					this.pTop.cText = txt.Top.ToString();
					this.pLeft.cText = txt.Left.ToString();
					this.pWidth.cText = txt.Width.ToString();
					this.pHeight.cText = txt.Height.ToString();
					
					this.SetSelectedForeColor(txt.Color.Name);
					this.SetSelectedFrameColor(txt.Border.BorderColor.Name);
					this.pTextValue.IsEnabled = true;
					this.pTextValue.Text = txt.Text;
					this.spProperties.Visibility = Visibility.Visible;
					this.spWH.Visibility = Visibility.Visible;
					this.spBR.Visibility = Visibility.Collapsed;
					this.colorFore.Visibility = Visibility.Visible;
					this.chkFrame.Visibility = Visibility.Visible;
					this.colorFrame.Visibility = Visibility.Visible;
					this.fontFrame.Visibility = Visibility.Visible;
					this.cboxColorBorder.Visibility = System.Windows.Visibility.Visible;
					this.SelectedFont = txt.Font.FontFamily.Name;
					this.SelectedFontSize = txt.Font.Size.ToString();
					this.SelectedFontUnderLine = txt.Font.Style.HasFlag(System.Drawing.FontStyle.Underline) ? true : false;
					this.SelectedFontBold = txt.Font.Style.HasFlag(System.Drawing.FontStyle.Bold) ? true : false;
					this.SelectedFontItalic = txt.Font.Style.HasFlag(System.Drawing.FontStyle.Italic) ? true : false;
					this.spTH.Visibility = System.Windows.Visibility.Collapsed;
					this.spCorner.Visibility = System.Windows.Visibility.Collapsed;

					this.SelectedForeColor.DrawingColor = txt.Color;
					break;
				case ReportObjectKind.BoxObject:
					BoxObject box = cur as BoxObject;
					this.pTop.cText = box.Top.ToString();
					this.pLeft.cText = box.Left.ToString();
					this.pBottom.cText = box.Bottom.ToString();
					this.pRight.cText = box.Right.ToString();
					this.SetSelectedFrameColor(box.Border.BorderColor.Name);
					this.pTextValue.Text = string.Empty;
					this.pTextValue.IsEnabled = false;
					this.spProperties.Visibility = Visibility.Visible;
					this.spWH.Visibility = Visibility.Collapsed;
					//this.spBR.Visibility = cur.Name.Contains("縦線") || cur.Name.Contains("横線") || cur.Name.Contains("枠線") ? Visibility.Visible : Visibility.Collapsed;
					//this.pBottom.IsEnabled = cur.Name.Contains("縦線") || cur.Name.Contains("枠線");
					//this.pRight.IsEnabled = cur.Name.Contains("横線") || cur.Name.Contains("枠線");
					this.spBR.Visibility = System.Windows.Visibility.Visible;
					this.pBottom.IsEnabled = true;
					this.pRight.IsEnabled = true;
					this.chkFrame.Visibility = Visibility.Collapsed;
					this.colorFore.Visibility = Visibility.Collapsed;
					this.colorFrame.Visibility = Visibility.Visible;
					this.fontFrame.Visibility = Visibility.Collapsed;
					this.pThickness.Text = box.LineThickness.ToString();
					// 角丸の値はCOMオブジェクトから取得する方法しかない
					var bf = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
					MethodInfo mi = box.ObjectFormat.GetType().GetMethod("get_RasReportObject", bf);
					ParameterInfo[] plist = mi.GetParameters();
					object ret = mi.Invoke(box.ObjectFormat, new object[] { });
					this.pCornerH.Text = ((CrystalDecisions.ReportAppServer.ReportDefModel.BoxObject)ret).CornerEllipseHeight.ToString();
					this.pCornerW.Text = ((CrystalDecisions.ReportAppServer.ReportDefModel.BoxObject)ret).CornerEllipseWidth.ToString();
					this.spTH.Visibility = System.Windows.Visibility.Visible;
					this.spCorner.Visibility = cur.Name.Contains("枠線") ? Visibility.Visible : Visibility.Collapsed;

					break;
				case ReportObjectKind.LineObject:
					LineObject line = cur as LineObject;
					this.pTop.cText = line.Top.ToString();
					this.pLeft.cText = line.Left.ToString();
					this.pBottom.cText = line.Bottom.ToString();
					this.pRight.cText = line.Right.ToString();
					this.SetSelectedFrameColor(line.Border.BorderColor.Name);
					this.pTextValue.Text = string.Empty;
					this.pTextValue.IsEnabled = false;
					this.spProperties.Visibility = Visibility.Visible;
					this.spWH.Visibility = Visibility.Collapsed;
					this.spBR.Visibility = Visibility.Visible;
					this.pBottom.IsEnabled = (line.Left == line.Right);
					this.pRight.IsEnabled = (line.Top == line.Bottom);
					this.chkFrame.Visibility = Visibility.Collapsed;
					this.colorFore.Visibility = Visibility.Collapsed;
					this.colorFrame.Visibility = Visibility.Visible;
					this.fontFrame.Visibility = Visibility.Collapsed;
					this.pThickness.Text = line.LineThickness.ToString();
					this.spTH.Visibility = System.Windows.Visibility.Visible;
					this.spCorner.Visibility = System.Windows.Visibility.Collapsed;

					break;
				case ReportObjectKind.PictureObject:
					PictureObject pic = cur as PictureObject;
					this.pTop.cText = pic.Top.ToString();
					this.pLeft.cText = pic.Left.ToString();
					this.pWidth.cText = pic.Width.ToString();
					this.pHeight.cText = pic.Height.ToString();
					this.SetSelectedFrameColor(pic.Border.BorderColor.Name);
					this.pTextValue.IsEnabled = false;
					this.pTextValue.Text = string.Empty;
					this.spProperties.Visibility = Visibility.Visible;
					this.spWH.Visibility = Visibility.Visible;
					this.spBR.Visibility = System.Windows.Visibility.Visible;
					this.pBottom.IsEnabled = true;
					this.pRight.IsEnabled = true;
					this.chkFrame.Visibility = Visibility.Visible;
					this.colorFore.Visibility = System.Windows.Visibility.Collapsed;
					this.spTH.Visibility = System.Windows.Visibility.Visible;
					this.spCorner.Visibility = System.Windows.Visibility.Collapsed;
                    break;

                // 追加
                case ReportObjectKind.BlobFieldObject:
                    BlobFieldObject blob = cur as BlobFieldObject;
                    this.pTop.cText = blob.Top.ToString();
                    this.pLeft.cText = blob.Left.ToString();
                    this.pWidth.cText = blob.Width.ToString();
                    this.pHeight.cText = blob.Height.ToString();
                    this.SetSelectedFrameColor(blob.Border.BorderColor.Name);
                    this.pTextValue.IsEnabled = false;
                    this.pTextValue.Text = string.Empty;
                    this.spProperties.Visibility = Visibility.Visible;
                    this.spWH.Visibility = Visibility.Visible;
                    this.spBR.Visibility = Visibility.Collapsed;
                    this.pBottom.IsEnabled = true;
                    this.pRight.IsEnabled = true;
                    this.chkFrame.Visibility = Visibility.Collapsed;
                    this.colorFore.Visibility = Visibility.Collapsed;
                    this.colorFrame.Visibility = Visibility.Collapsed;
                    this.spTH.Visibility = Visibility.Collapsed;
                    this.spCorner.Visibility = Visibility.Collapsed;
                    this.fontFrame.Visibility = Visibility.Collapsed;
                    break;

				default:
					this.spProperties.Visibility = Visibility.Collapsed;
					this.pTextValue.IsEnabled = false;
					this.pTextValue.Text = string.Empty;
					this.spWH.Visibility = Visibility.Collapsed;
					this.spBR.Visibility = Visibility.Visible;
					this.chkFrame.Visibility = Visibility.Collapsed;
					this.cboxColorBorder.Visibility = System.Windows.Visibility.Visible;
					this.colorFore.Visibility = System.Windows.Visibility.Collapsed;
					this.spCorner.Visibility = System.Windows.Visibility.Collapsed;
					break;
				}
				//this.movingTopLeft.Y += height;
				var item = objectList.Where(x => x.name == cur.Name).FirstOrDefault();
				if (item != null)
				{
					this.movingFrame.SetValue(Canvas.TopProperty, (item.top - innerFrameMargin));
					this.movingFrame.SetValue(Canvas.LeftProperty, (item.left - innerFrameMargin));
					this.movingFrame.Height = item.height + (innerFrameMargin * 2);
					this.movingFrame.Width = item.width + (innerFrameMargin * 2);
					this.movingFrame.Visibility = Visibility.Visible;
				}

			}
			catch (Exception )
			{
				//MessageBox.Show(ex.GetType().Name);
			}
		}

		private string GetLineStyleName(LineStyle ls)
		{
			string name = string.Empty;
			foreach (var item in lsComboList)
			{
				if (item.LineStyle == ls)
				{
					name = item.Name;
					break;
				}
			}
			return name;
		}
		private LineStyle GetLineStyle(string name)
		{
			LineStyle style = LineStyle.NoLine;
			foreach (var item in lsComboList)
			{
				if (item.Name == name)
				{
					style = item.LineStyle;
					break;
				}
			}
			return style;
		}

		#endregion

		#region レポートビューア処理

		/// <summary>
		/// イベント：CrystalReportプレビューのページブロッククリック時に発生する。
		/// </summary>
		/// <param name="sender">イベント受信対象</param>
		/// <param name="e">イベントパラメータ</param>
		private void reportViewer_ClickPage(object sender, SAPBusinessObjects.WPF.Viewer.PageMouseEventArgs e)
		{
			try
			{
				var oinfo = (from o in this.objectList where o.name == e.ObjectInfo.Name select o).FirstOrDefault();
				if (oinfo != null)
				{
					//selectedObject = this.targetReportDocument.ReportDefinition.ReportObjects[e.ObjectInfo.Name];
					this.DisplaySelectedInfo(e.ObjectInfo.Name);
					DisplayLayoutObject(selectedObject, true);
				}
				else
				{
					e.Handled = true;
				}
			}
			catch (Exception)
			{
				//MessageBox.Show(ex.Message);
			}
		}

		private void PreviewRefresh()
		{
			this.reportViewer.ViewerCore.ReuseParameterWhenRefresh = true;
            //this.reportViewer.ViewerCore.RefreshReport();
            try
            {
                this.reportViewer.ViewerCore.RefreshReport();
            }
            catch (Exception ex) { var data = ex.Data; MessageBox.Show("データメンバーとレイアウトフィールド名称が一致しません。","エラーメッセージ", MessageBoxButton.OK, MessageBoxImage.Error); }
			if (this.CurrentPage < 1)
			{
				this.CurrentPage = 1;
			}
			this.reportViewer.ViewerCore.ShowNthPage(this.CurrentPage);
		}

		private void reportViewer_ViewChange(object sender, RoutedEventArgs e)
		{
			if (this.LastPage > 0)
			{
				return;
			}
			this.reportViewer.ViewerCore.ShowLastPage();
			this.LastPage = this.reportViewer.ViewerCore.GetCurrentPageNumber();
			if (this.LastPage > 1)
			{
				this.reportViewer.ViewerCore.ShowFirstPage();
			}
			this.CurrentPage = this.reportViewer.ViewerCore.GetCurrentPageNumber();
			this.reportViewer.Focus();
		}

		#endregion

		#region 各ボタンの処理

		private void Button_Prevs_Click(object sender, RoutedEventArgs e)
		{
			if (this.LastPage > 1)
			{
				this.reportViewer.ViewerCore.ShowPreviousPage();
				this.CurrentPage = this.reportViewer.ViewerCore.GetCurrentPageNumber();
			}
		}

		private void Button_Next_Click(object sender, RoutedEventArgs e)
		{
			if (this.CurrentPage < this.LastPage)
			{
				this.reportViewer.ViewerCore.ShowNextPage();
				this.CurrentPage = this.reportViewer.ViewerCore.GetCurrentPageNumber();
			}
		}

		/// <summary>
		/// やり直しボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Reload_Click(object sender, RoutedEventArgs e)
		{
			if (IsChanged)
			{
				var res = MessageBox.Show("変更した内容がありますが、\r\n破棄しますか？", "確認", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
				if (res == MessageBoxResult.Cancel)
				{
					return;
				}
			}
			FileInfo fi = new FileInfo(reportFilePath + ".ORG");
			fi.CopyTo(reportFilePath, true);
			this.DialogResult = null;
			IsReload = true;
			this.Close();
		}

		/// <summary>
		/// 取消ボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Cancel_Click(object sender, RoutedEventArgs e)
		{
			if (IsChanged)
			{
				var res = MessageBox.Show("変更した内容がありますが、\r\n破棄しますか？", "取消確認", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
				if (res == MessageBoxResult.Cancel)
				{
					return;
				}
			}
			FileInfo fi = new FileInfo(reportFilePath + ".ORG");
			fi.CopyTo(reportFilePath, true);
			this.DialogResult = false;
			IsReload = false;
			this.Close();
		}

		/// <summary>
		/// 保存ボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Save_Click(object sender, RoutedEventArgs e)
		{
			if (this.reportFilePath == string.Empty)
			{
				return;
			}
			try
			{
				if (!IsChanged)
				{
					var res = MessageBox.Show("変更した内容がありません、\r\n強制的に保存しますか？", "終了確認", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
					if (res == MessageBoxResult.Cancel)
					{
						return;
					}
				}
				this.targetReportDocument.SaveAs(this.reportFilePath);
				this.DialogResult = true;
				this.IsInitialRecovered = false;
				IsSaved = true;
				this.Close();
			}
			catch (Exception)
			{
			}
		}

		/// <summary>
		/// 全て元に戻す
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Initialize_Click(object sender, RoutedEventArgs e)
		{
			FileInfo fi = new FileInfo(reportFilePath);
			DirectoryInfo di = new DirectoryInfo(@".\Files\Original");
			if (di.Exists != true)
			{
				MessageBox.Show("初期定義ファイルが見つかりません。");
				return;
			}
			FileInfo fiORG = new FileInfo(di.FullName + @"\" + fi.Name);
			if (fiORG.Exists != true)
			{
				MessageBox.Show("初期定義ファイルが見つかりません。");
				return;
			}

			var res = MessageBox.Show("変更された全ての内容を破棄し\r\nインストール時の状態に戻します。\r\nよろしいですか？", "定義ファイルの初期化", MessageBoxButton.YesNo, MessageBoxImage.Warning);
			if (res != MessageBoxResult.Yes)
			{
				return;
			}
			fiORG.CopyTo(reportFilePath, true);
			this.DialogResult = true;
			IsReload = false;
			IsSaved = true;
			this.IsInitialRecovered = true;
			this.Close();
		}

		/// <summary>
		/// 印刷ボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Printout_Click(object sender, RoutedEventArgs e)
		{
			PrinterSettings printers = new PrinterSettings();
			PageSettings pages;
			pages = new PageSettings();
			targetReportDocument.PrintOptions.CopyTo(printers, pages);
			if (string.IsNullOrWhiteSpace(this.PrinterName) != true)
			{
				printers.PrinterName = this.PrinterName;
			}
			printers.Collate = true;
			if (printers.PrintRange == PrintRange.AllPages)
			{
				printers.FromPage = printers.ToPage = 0;
			}

			System.Windows.Forms.PrintDialog pDialog = new System.Windows.Forms.PrintDialog();

			var print = pDialog.ShowDialog();
			if (print == System.Windows.Forms.DialogResult.OK)
			{
				printers = pDialog.PrinterSettings;
				pages.PrinterSettings = printers;
				targetReportDocument.PrintToPrinter(printers, pages, false);
				MessageBox.Show("印刷しました。");
			}
		}

		/// <summary>
		/// 閉じるボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Close_Click(object sender, RoutedEventArgs e)
		{
			if (IsChanged)
			{
				var res = MessageBox.Show("変更した内容がありますが、\r\n破棄しますか？", "終了確認", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
				if (res == MessageBoxResult.Cancel)
				{
					return;
				}
			}
			this.DialogResult = this.IsSaved;
			this.Close();
		}

		#endregion

		#endregion

		#region プロパティエリア制御

		private void FontBtn_Click(object sender, RoutedEventArgs e)
		{
			FontDialogEx dlg = new FontDialogEx();
			double fsize = Convert.ToDouble(this.SelectedFontSize);
			dlg.DFont = new ToolFont(this.SelectedFont, Convert.ToDouble(this.SelectedFontSize), false, false);
			bool? ret = dlg.ShowDialog();
			if (ret == true)
			{
				var font = dlg.DFont;
				this.SelectedFontSize = dlg.DFont.FontSize.ToString();
				this.SelectedFont = dlg.DFont.FontFamily.FamilyNames[dlg.FXmlLanguage];
			}
			FocusControl.SetFocusWithOrder(new TraversalRequest(FocusNavigationDirection.Next));
		}


		private void propertyItem_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				e.Handled = true;
				FocusControl.SetFocusWithOrder(new TraversalRequest(FocusNavigationDirection.Next));
			}
		}

		/// <summary>
		/// フィールドのプロパティ変更内容を適用する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnItemProperyUpdate_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(this.pFieldName.Content as string))
				{
					return;
				}

				// 位置
				int left = int.Parse(this.pLeft.cText, System.Globalization.NumberStyles.Integer);
				int top = int.Parse(this.pTop.cText, System.Globalization.NumberStyles.Integer);
				int height = 0;
				int width = 0;
				int right = 0;
				int bottom = 0;
				int thickness = 0;
				bool bColorF = false;
				bool bColorB = false;

				System.Drawing.FontStyle fstyle = this.SelectedFontUnderLine ? System.Drawing.FontStyle.Underline : System.Drawing.FontStyle.Regular;
				fstyle |= this.SelectedFontBold ? System.Drawing.FontStyle.Bold : fstyle;
				fstyle |= this.SelectedFontItalic ? System.Drawing.FontStyle.Italic : fstyle;

				var oinfo = this.objectList.Where(o => o.name == this.selectedObject.Name).First();
				var sect = sectInfos.Where(x => x.name == oinfo.sectionname).First();

				if (top > (sect.height))
				{
					MessageBox.Show("上座標が配置可能な領域を超えます。");
					return;
				}
				if (left > (sect.width - 200))
				{
					MessageBox.Show("左座標が配置可能な領域を超えます。");
					return;
				}

				// セクションをまたがるオブジェクトの場合
				var exsect = sectInfos.Where(x => x.name == oinfo.endSectionName && oinfo.extendSection == true).FirstOrDefault();
				if (exsect == null)
				{
					exsect = sect;
				}

				switch (this.selectedObject.Kind)
				{
				case ReportObjectKind.FieldHeadingObject:
				case ReportObjectKind.FieldObject:
				case ReportObjectKind.TextObject:
				case ReportObjectKind.PictureObject:
                case ReportObjectKind.BlobFieldObject:  // 追加
					height = int.Parse(this.pHeight.cText, System.Globalization.NumberStyles.Integer);
					width = int.Parse(this.pWidth.cText, System.Globalization.NumberStyles.Integer);
					if ((top + height) > sect.height)
					{
						MessageBox.Show("高さが配置可能な領域を超えます。");
						return;
					}
					break;
				case ReportObjectKind.BoxObject:
					thickness = int.Parse(this.pThickness.cText, System.Globalization.NumberStyles.Integer);
					if (thickness < 10)
					{
						MessageBox.Show("線の太さは10以上を指定してください。");
						return;
					}
					right = int.Parse(this.pRight.cText, System.Globalization.NumberStyles.Integer);
					bottom = int.Parse(this.pBottom.cText, System.Globalization.NumberStyles.Integer);
					if ((bottom) > (exsect.height))
					{
						MessageBox.Show("配置可能な領域を超えます。\r\n上下座標または線太さを調整してください。");
						return;
					}

					break;
				case ReportObjectKind.LineObject:
					thickness = int.Parse(this.pThickness.cText, System.Globalization.NumberStyles.Integer);
					if (thickness < 10)
					{
						MessageBox.Show("線の太さは10以上を指定してください。");
						return;
					}
					right = int.Parse(this.pRight.cText, System.Globalization.NumberStyles.Integer);
					bottom = int.Parse(this.pBottom.cText, System.Globalization.NumberStyles.Integer);
					if (oinfo.objectType == ObjectInfoType.VLine)
					{
						right = left + thickness;
					}
					if (oinfo.objectType == ObjectInfoType.HLine)
					{
						bottom = top + thickness;
					}
					if ((bottom) > (exsect.height))
					{
						MessageBox.Show("配置可能な領域を超えます。\r\n上下座標または線太さを調整してください。");
						return;
					}
					break;
				}


				switch (this.selectedObject.Kind)
				{
				case ReportObjectKind.FieldHeadingObject:
					this.selectedObject.Top = top;
					this.selectedObject.Left = left;
					(this.selectedObject as FieldHeadingObject).Height = height;
					(this.selectedObject as FieldHeadingObject).Width = width;
					(this.selectedObject as FieldHeadingObject).ApplyFont(new System.Drawing.Font(this.SelectedFont, (float)Convert.ToInt16(SelectedFontSize), fstyle));
					bColorF = true;
					bColorB = true;
					break;
				case ReportObjectKind.FieldObject:
					this.selectedObject.Top = top;
					this.selectedObject.Left = left;
					(this.selectedObject as FieldObject).Height = height;
					(this.selectedObject as FieldObject).Width = width;
					(this.selectedObject as FieldObject).ApplyFont(new System.Drawing.Font(this.SelectedFont, (float)Convert.ToInt16(SelectedFontSize), fstyle));
					bColorF = true;
					bColorB = true;
					break;
				case ReportObjectKind.TextObject:
					this.selectedObject.Top = top;
					this.selectedObject.Left = left;
					(this.selectedObject as TextObject).Height = height;
					(this.selectedObject as TextObject).Width = width;
					(this.selectedObject as TextObject).ApplyFont(new System.Drawing.Font(this.SelectedFont, (float)Convert.ToInt16(SelectedFontSize), fstyle));
					bColorF = true;
					bColorB = true;
					break;
				case ReportObjectKind.PictureObject:
					this.selectedObject.Top = top;
					this.selectedObject.Left = left;
					(this.selectedObject as PictureObject).Height = height;
					(this.selectedObject as PictureObject).Width = width;
					bColorB = true;
					break;
				case ReportObjectKind.BoxObject:
					this.selectedObject.Top = top;
					this.selectedObject.Left = left;
					(this.selectedObject as BoxObject).Right = right;
					(this.selectedObject as BoxObject).Bottom = bottom;
					(this.selectedObject as BoxObject).LineThickness = thickness;
					var bf = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
					MethodInfo mi = (this.selectedObject as BoxObject).ObjectFormat.GetType().GetMethod("get_RasReportObject", bf);
					object ret = mi.Invoke((this.selectedObject as BoxObject).ObjectFormat, new object[] { });
					((CrystalDecisions.ReportAppServer.ReportDefModel.BoxObject)ret).CornerEllipseHeight = Convert.ToInt32(this.pCornerH.Text);
					((CrystalDecisions.ReportAppServer.ReportDefModel.BoxObject)ret).CornerEllipseWidth = Convert.ToInt32(this.pCornerW.Text);
					bColorB = true;
					break;
				case ReportObjectKind.LineObject:
					var lobj = (this.selectedObject as LineObject);
					try
					{
						MethodInfo romi = ((DrawingObject)(lobj)).GetType().BaseType.GetMethod("get_RasObject", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
						CrystalDecisions.ReportAppServer.ReportDefModel.ISCRLineObject ro = (CrystalDecisions.ReportAppServer.ReportDefModel.ISCRLineObject)romi.Invoke(lobj, new object[] { });
						//縦線か横線か判定
						if (lobj.Right == lobj.Left)
						{
							// 縦線
							ro.Left = left;
							ro.Right = left;
							ro.Top = top;
							ro.Bottom = bottom;
						}
						else
						{
							// 横線
							ro.Left = left;
							ro.Right = right;
							ro.Top = top;
							ro.Bottom = top;
						}
						ro.LineThickness = thickness;
					}
					catch (Exception ex)
					{
					}
					bColorB = true;
					break;
                //　追加
                case ReportObjectKind.BlobFieldObject:
                    this.selectedObject.Top = top;
                    this.selectedObject.Left = left;
                    (this.selectedObject as BlobFieldObject).Height = height;
                    (this.selectedObject as BlobFieldObject).Width = width;
                    bColorB = true;
                    break;
                }

				// 前景色
				if (bColorF && this.SelectedForeColor != null)
				{
					System.Drawing.Color clr = this.SelectedForeColor.DrawingColor;
					switch (this.selectedObject.Kind)
					{
					case ReportObjectKind.FieldHeadingObject:
						(this.selectedObject as FieldHeadingObject).Color = clr;
						break;
					case ReportObjectKind.FieldObject:
						(this.selectedObject as FieldObject).Color = clr;
						break;
					case ReportObjectKind.TextObject:
						(this.selectedObject as TextObject).Color = clr;
						break;
					case ReportObjectKind.BoxObject:
						(this.selectedObject as BoxObject).Border.BorderColor = clr;
						break;
					case ReportObjectKind.PictureObject:
						(this.selectedObject as PictureObject).Border.BorderColor = clr;
						break;
					case ReportObjectKind.LineObject:
						//(this.selectedObject as LineObject).LineColor = clr;
						break;
                    case ReportObjectKind.BlobFieldObject: // 追加
                        (this.selectedObject as BlobFieldObject).Border.BorderColor = clr;
                        break;
                    }
				}
				// 背景色
				if (bColorB && this.SelectedFrameColor != null)
				{
					System.Drawing.Color clr = this.SelectedFrameColor.DrawingColor;
					switch (this.selectedObject.Kind)
					{
					case ReportObjectKind.FieldHeadingObject:
                        (this.selectedObject as FieldHeadingObject).Border.BorderColor = clr;
						break;
					case ReportObjectKind.FieldObject:
						(this.selectedObject as FieldObject).Border.BorderColor = clr;
						break;
					case ReportObjectKind.TextObject:
						(this.selectedObject as TextObject).Border.BorderColor = clr;
						break;
					case ReportObjectKind.BoxObject:
						(this.selectedObject as BoxObject).Border.BorderColor = clr;
						break;
					case ReportObjectKind.PictureObject:
						(this.selectedObject as PictureObject).Border.BorderColor = clr;
						break;
					case ReportObjectKind.LineObject:
						(this.selectedObject as LineObject).LineColor = clr;
						break;
                    case ReportObjectKind.BlobFieldObject: // 追加
                        (this.selectedObject as BlobFieldObject).Border.BorderColor = clr;
                        break;
                    }
				}

				// 枠
				if (this.chkFrame.Visibility == Visibility.Visible)
				{
					this.selectedObject.Border.LeftLineStyle = GetLineStyle(SelectedLS_L);
					this.selectedObject.Border.TopLineStyle = GetLineStyle(SelectedLS_T);
					this.selectedObject.Border.RightLineStyle = GetLineStyle(SelectedLS_R);
					this.selectedObject.Border.BottomLineStyle = GetLineStyle(SelectedLS_B);
				}
				// テキスト
				if (this.selectedObject.Kind == ReportObjectKind.TextObject)
				{
					(this.selectedObject as TextObject).Text = this.pTextValue.Text;
				}
				else if (this.selectedObject.Kind == ReportObjectKind.FieldHeadingObject)
				{
					(this.selectedObject as FieldHeadingObject).Text = this.pTextValue.Text;
				}
				PreviewRefresh();
				IsChanged = true;
				DisplayLayoutObject(this.selectedObject, true);
				DisplaySelectedInfo(this.pFieldName.Content as string);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		#endregion

		#region 表示ページ制御
		private void CurrentPage_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if ((Regex.Match(e.Text, @"^[0-9\r]")).Success != true)
			{
				e.Handled = true;
				return;
			}
			if (e.Text == "\r")
			{
				if (this.CurrentPage <= this.LastPage)
				{
					this.reportViewer.ViewerCore.ShowNthPage(this.CurrentPage);
				}

			}
		}

		private void CurrentPage_GotFocus(object sender, RoutedEventArgs e)
		{
			var ctl = sender as TextBox;
			ctl.SelectAll();
		}
		#endregion

		#region 表示モード制御
		private void modeDesign_Checked(object sender, RoutedEventArgs e)
		{
			if (this.grDesigner != null)
			{
				double height = grdViewArea.ActualHeight;
				foreach (var row in grdViewArea.RowDefinitions)
				{
					if (row.Name == this.grDesigner.Name)
					{
						break;
					}
					height -= row.ActualHeight;
				}
				this.grDesigner.Height = new GridLength(height);
			}
		}

		private void modePreview_Checked(object sender, RoutedEventArgs e)
		{
			if (this.grDesigner != null)
			{
				double height = 0;
				foreach (var row in grdViewArea.RowDefinitions)
				{
					if (row.Name == this.grDesigner.Name)
					{
						break;
					}
					height += row.ActualHeight;
				}
				this.grDesigner.Height = new GridLength(height);
			}
		}

		private void viewSplitter_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
		{
			this.chkDesignner.IsChecked = null;                                                                                                                                                                  
			this.chkPreview.IsChecked = null;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
		}
		                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
		Cursor defCsr;
		private void splitter_MouseEnter(object sender, MouseEventArgs e)
		{
			defCsr = (sender as GridSplitter).Cursor;
			(sender as GridSplitter).Cursor = Cursors.ScrollNS;
		}

		private void splitter_MouseLeave(object sender, MouseEventArgs e)
		{
		}

		#endregion

	}


}
