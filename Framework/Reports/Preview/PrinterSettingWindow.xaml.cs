using System;
using System.Collections.Generic;
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
using System.Printing;
using System.Drawing.Printing;

using KyoeiSystem.Framework.Windows.ViewBase;
using CrystalDecisions.CrystalReports.Engine;
using KyoeiSystem.Framework.Windows.Controls;

namespace KyoeiSystem.Framework.Reports.Preview
{
	/// <summary>
	/// PrinterSettingWindow
	/// </summary>
	public partial class PrinterSettingWindow : WindowGeneralBase
	{

		#region プリンタ名
		private Visibility _printerNameVisibility = Visibility.Collapsed;
		public Visibility PrinterNameVisibility
		{
			get { return _printerNameVisibility; }
			set { _printerNameVisibility = value; NotifyPropertyChanged(); }
		}

		private string _selectedPrinterName = string.Empty;
		public string SelectedPrinterName
		{
			get { return _selectedPrinterName; }
			set
			{
				if (PrinterNames.Contains(value))
				{
					_selectedPrinterName = value;
				}
				else
				{
					_selectedPrinterName = defaultPrinterName;
				}
				NotifyPropertyChanged();
			}
		}
		private string defaultPrinterName = string.Empty;
		private List<string> _printerNames;
		public List<string> PrinterNames
		{
			get { return this._printerNames; }
			set { this._printerNames = value; this.NotifyPropertyChanged(); }
		}
		#endregion

		#region 用紙サイズ

		// 既製値用
		private string _paperSizeName;
		public string PaperSizeName
		{
			get
			{
				return this._paperSizeName;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					this.IsCustomPageSize = true;
				}
				else
				{
					this._paperSizeName = value;
					this.NotifyPropertyChanged();
					var psz = (from p in PaperSizes where p.PaperName == this.PaperSizeName select p).FirstOrDefault();
					if (psz != null)
					{
						this.CustomHeight = psz.Height;
						this.CustomWidth = psz.Width;
					}
				}
			}
		}
		private List<PaperSize> _paperSizes;
		public List<PaperSize> PaperSizes
		{
			get
			{
				return this._paperSizes;
			}
			set
			{
				this._paperSizes = value;
				this.NotifyPropertyChanged();
			}
		}
		private bool _isCustomPageSize = false;
		public bool IsCustomPageSize
		{
			get
			{
				return this._isCustomPageSize;
			}
			set
			{
				this._isCustomPageSize = value;
				this.NotifyPropertyChanged();
				IsCustomPageSizeEnabled = value == null ? false : (bool)value;
				IsPageSizeEnabled = IsCustomPageSizeEnabled ? false : true;
			}
		}
		private bool _isPageSizeEnabled = true;
		private bool IsPageSizeEnabled
		{
			get
			{
				return this._isPageSizeEnabled;
			}
			set
			{
				this._isPageSizeEnabled = value;
				this.cmbPaparSize.Combo_IsEnabled = value;
				this.NotifyPropertyChanged();
			}
		}
		private bool _isCustomPageSizeEnabled = false;
		public bool IsCustomPageSizeEnabled
		{
			get
			{
				return this._isCustomPageSizeEnabled;
			}
			set
			{
				this._isCustomPageSizeEnabled = value;
				this.NotifyPropertyChanged();
			}
		}
		// ユーザ定義用
		private int _customHeight = 0;
		public int CustomHeight
		{
			get
			{
				return this._customHeight;
			}
			set
			{
				this._customHeight = value;
				this.NotifyPropertyChanged();
			}
		}
		private int _customWidth = 0;
		public int CustomWidth
		{
			get
			{
				return this._customWidth;
			}
			set
			{
				this._customWidth = value;
				this.NotifyPropertyChanged();
			}
		}

		private PageSettings _pageSettings = null;
		public PageSettings PageSettings
		{
			get { return this._pageSettings; }
			set
			{
				this._pageSettings = value;
				if (value != null)
				{
					if (value.PrinterSettings != null && PrinterNames.Contains(value.PrinterSettings.PrinterName))
					{
						this.SelectedPrinterName = value.PrinterSettings.PrinterName;
						this.PaperSizeName = value.PaperSize.PaperName;
						this.PaperSourceName = value.PaperSource.SourceName;
					}
					else
					{
						this.SelectedPrinterName = defaultPrinterName;
						InitPaperName();
						InitPaperSource();
					}
					this.MarginTop = value.Margins.Top;
					this.MarginBottom = value.Margins.Bottom;
					this.MarginLeft = value.Margins.Left;
					this.MarginRight = value.Margins.Right;
				}
			}
		}

		#endregion

		#region 用紙トレイ
		private string _paperSourceName;
		public string PaperSourceName
		{
			get
			{
				return this._paperSourceName;
			}
			set
			{
				this._paperSourceName = value;
				this.NotifyPropertyChanged();
			}
		}
		private List<PaperSource> _paperSources;
		public List<PaperSource> PaperSources
		{
			get
			{
				return this._paperSources;
			}
			set
			{
				this._paperSources = value;
				this.NotifyPropertyChanged();
			}
		}
		#endregion

		#region マージン
		private Margins _margins = new Margins();
		public Margins Margins
		{
			get { return _margins; }
			set
			{
				_margins = value;
				NotifyPropertyChanged();
				NotifyPropertyChanged("MarginLeft");
				NotifyPropertyChanged("MarginRight");
				NotifyPropertyChanged("MarginTop");
				NotifyPropertyChanged("MarginBottom");

			}
		}
		public int MarginLeft
		{
			get { return _margins.Left; }
			set { _margins.Left = value; NotifyPropertyChanged(); }
		}
		public int MarginRight
		{
			get { return _margins.Right; }
			set { _margins.Right = value; NotifyPropertyChanged(); }
		}
		public int MarginTop
		{
			get { return _margins.Top; }
			set { _margins.Top = value; NotifyPropertyChanged(); }
		}
		public int MarginBottom
		{
			get { return _margins.Bottom; }
			set { _margins.Bottom = value; NotifyPropertyChanged(); }
		}

		#endregion

		#region 印刷設定
		private bool _Is印刷指示 = false;
		public bool Is印刷指示
		{
			get { return _Is印刷指示; }
			set { _Is印刷指示 = value; NotifyPropertyChanged(); }
		}
		private bool _全ページ印刷 = true;
		public bool 全ページ印刷
		{
			get { return _全ページ印刷; }
			set
			{
				_全ページ印刷 = value;
				NotifyPropertyChanged();
				if (value)
				{
					Page開始 = Page終了 = 0;
					if (this.PageSettings != null && this.PageSettings.PrinterSettings != null)
					{
						this.PageSettings.PrinterSettings.PrintRange = PrintRange.AllPages;
						this.PageSettings.PrinterSettings.FromPage = 0;
						this.PageSettings.PrinterSettings.ToPage = 0;
					}
				}
				else
				{
					if (this.PageSettings != null && this.PageSettings.PrinterSettings != null)
					{
						this.PageSettings.PrinterSettings.PrintRange = PrintRange.SomePages;
						this.PageSettings.PrinterSettings.FromPage = Page開始;
						this.PageSettings.PrinterSettings.ToPage = Page終了;
					}
				}
			}
		}
		private int _Page開始 = 0;
		public int Page開始
		{
			get { return _Page開始; }
			set
			{
				_Page開始 = value;
				NotifyPropertyChanged();
				if (value != 0)
				{
					全ページ印刷 = false;
				}
				if (this.PageSettings != null && this.PageSettings.PrinterSettings != null)
				{
					this.PageSettings.PrinterSettings.FromPage = Page開始;
				}
			}
		}
		private int _Page終了 = 0;
		public int Page終了
		{
			get { return _Page終了; }
			set
			{
				_Page終了 = value;
				NotifyPropertyChanged();
				if (value != 0)
				{
					全ページ印刷 = false;
				}
				if (this.PageSettings != null && this.PageSettings.PrinterSettings != null)
				{
					this.PageSettings.PrinterSettings.ToPage = Page終了;
				}
			}
		}
		private short _印刷部数 = 1;
		public short 印刷部数
		{
			get { return _印刷部数; }
			set
			{
				_印刷部数 = value;
				NotifyPropertyChanged();
				if (this.PageSettings != null && this.PageSettings.PrinterSettings != null)
				{
					this.PageSettings.PrinterSettings.Copies = value;
				}
			}
		}

		#endregion

		#region OKボタン機能名
		private string _OKボタン機能名 = "保存";
		public string OKボタン機能名
		{
			get { return _OKボタン機能名; }
			set
			{
				_OKボタン機能名 = value;
				NotifyPropertyChanged();
				Is印刷指示 = value.Contains("印刷");
			}
		}
		#endregion

		public PrinterSettingWindow()
		{
			InitializeComponent();
			this.DataContext = this;

			this.PrinterNames = new List<string>();
			this.PaperSizes = new List<PaperSize>();
			this.PaperSources = new List<PaperSource>();
			List<string> pnmlist = new List<string>();
			PrintQueue defaultPrinter = new LocalPrintServer().DefaultPrintQueue;
			this.defaultPrinterName = defaultPrinter.FullName;
			foreach (string item in PrinterSettings.InstalledPrinters)
			{
				pnmlist.Add(item);
			}
			this.PrinterNames = (from p in pnmlist orderby p select p).ToList();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			
		}

		private void PrinterName_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.PageSettings.PrinterSettings = new PrinterSettings();
			this.PageSettings.PrinterSettings.PrinterName = this.SelectedPrinterName;

			var psz = new List<PaperSize>();
			foreach (PaperSize item in this.PageSettings.PrinterSettings.PaperSizes)
			{
				psz.Add(item);
			}
			this.PaperSizes = psz.OrderBy(x => x.PaperName).ToList();

			var pss = new List<PaperSource>();
			foreach (PaperSource item in this.PageSettings.PrinterSettings.PaperSources)
			{
				pss.Add(item);
			}
			this.PaperSources = pss.OrderBy(x => x.SourceName).ToList();
			InitPaperName();
			InitPaperSource();
		}

		private void LastField_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				e.Handled = true;
				btnSave.Focus();
			}
		}

		private void Save_Click(object sender, RoutedEventArgs e)
		{
			if (this.Page開始 > this.Page終了)
			{
				MessageBox.Show("正しい印刷ページ範囲を指定してください。", "エラー", MessageBoxButton.OK);
				return;
			}
			if (this.印刷部数 < 1)
			{
				MessageBox.Show("正しい印刷部数を指定してください。", "エラー", MessageBoxButton.OK);
				return;
			}

			this.PageSettings.PrinterSettings.PrinterName = this.SelectedPrinterName;

			if (this.IsCustomPageSize)
			{
				this.PageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Custom", this.CustomWidth, this.CustomHeight);
				this.PageSettings.PaperSize.RawKind = 256;
			}
			else
			{
				var psz = (from p in PaperSizes where p.PaperName == this.PaperSizeName select p).FirstOrDefault();
				if (psz == null)
				{
					MessageBox.Show("用紙サイズを選択してください。", "エラー", MessageBoxButton.OK);
					return;
				}
				this.PageSettings.PaperSize = psz;
			}

			var psc = (from p in PaperSources where p.SourceName == this.PaperSourceName select p).FirstOrDefault();
			if (psc == null)
			{
                // TODO:プリンタがPDFの場合は設定不要⇒プリンタ名に"PDF"を含む場合は無視する
                if (this.PageSettings.PrinterSettings.PrinterName.IndexOf("PDF") < 0)
                {
                    MessageBox.Show("用紙トレイを選択してください。", "エラー", MessageBoxButton.OK);
                    return;
                }
			}
			this.PageSettings.PaperSource = psc;

			this.PageSettings.Margins = this.Margins;

			if (this.PageSettings.PrinterSettings.PrintRange == PrintRange.AllPages)
			{
				this.PageSettings.PrinterSettings.FromPage = 0;
				this.PageSettings.PrinterSettings.ToPage = 0;
			}
			this.DialogResult = true;
			//Close();
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			//Close();
		}

		private void CustomPage_Click(object sender, RoutedEventArgs e)
		{
			if ((sender as KyoeiSystem.Framework.Windows.Controls.UcCheckBox).IsChecked ?? false)
			{
				this.txtCustomHeight.SetFocus();
			}
		}

		public bool SettingDialogStandard(PageSettings pages)
		{
			System.Windows.Forms.PrintDialog prtdlg = new System.Windows.Forms.PrintDialog();
			if (pages.PrinterSettings.PrintRange == PrintRange.AllPages)
			{
				pages.PrinterSettings.FromPage = 0;
				pages.PrinterSettings.ToPage = 0;
			}
			prtdlg.PrinterSettings = pages.PrinterSettings;
			prtdlg.AllowCurrentPage = false;		// 使えないので必ずfalse
			prtdlg.AllowPrintToFile = false;		// 使えないので必ずfalse
			prtdlg.AllowSelection = false;			// 使えないので必ずfalse
			prtdlg.AllowSomePages = true;
			var result = prtdlg.ShowDialog();
			if (result != System.Windows.Forms.DialogResult.OK)
			{
				return false;
			}
			pages.PrinterSettings = prtdlg.PrinterSettings;
			if (pages.PrinterSettings.PrintRange == PrintRange.AllPages)
			{
				pages.PrinterSettings.FromPage = 0;
				pages.PrinterSettings.ToPage = 0;
			}
			this.Margins = pages.Margins;
			//if (this.IsCustomPageSize)
			//{
			//	System.Windows.Forms.PageSetupDialog pgdlg = new System.Windows.Forms.PageSetupDialog();
			//	pgdlg.PageSettings = pages;
			//	pgdlg.EnableMetric = true;
			//	pgdlg.PrinterSettings = pages.PrinterSettings;
			//	result = pgdlg.ShowDialog();
			//	if (result != System.Windows.Forms.DialogResult.OK)
			//	{
			//		return false;
			//	}
			//	pages = pgdlg.PageSettings;
			//}
			this.PageSettings = pages;
			if (PrinterNames.Contains(pages.PrinterSettings.PrinterName))
			{
				this.SelectedPrinterName = pages.PrinterSettings.PrinterName;
			}
			else
			{
				this.SelectedPrinterName = defaultPrinterName;
				InitPaperName();
				InitPaperSource();
			}
			this.PaperSizeName = pages.PaperSize.PaperName;
			this.PaperSourceName = pages.PaperSource.SourceName;
			this.Margins = pages.Margins;
			this.CustomWidth = pages.PaperSize.Width;
			this.CustomHeight = pages.PaperSize.Height;

			return true;
		}

		public bool SettingDialogCustom(PageSettings pages)
		{
			this.PrinterNameVisibility = System.Windows.Visibility.Visible;
			if (pages == null)
			{
				throw new ReportException("システムエラー(プリンター設定なし)");
			}
			else
			{
				if (pages.PrinterSettings == null)
				{
					throw new ReportException("システムエラー(プリンター設定なし)");
				}
			}
			this.PageSettings = pages;
			if (PrinterNames.Contains(pages.PrinterSettings.PrinterName))
			{
				this.SelectedPrinterName = pages.PrinterSettings.PrinterName;
				this.PaperSizeName = pages.PaperSize.PaperName;
				this.PaperSourceName = pages.PaperSource.SourceName;
			}
			else
			{
				this.SelectedPrinterName = defaultPrinterName;
				InitPaperName();
				InitPaperSource();
			}
			this.IsCustomPageSize = (this.PaperSizeName == "Custom");
			this.Margins = pages.Margins;

			this.Landscape.IsChecked = pages.Landscape;
			switch (pages.PrinterSettings.LandscapeAngle)
			{
			case 0:
				this.Landscape0.IsChecked = true;
				break;
			case 90:
				this.Landscape90.IsChecked = true;
				break;
			case 180:
				this.Landscape180.IsChecked = true;
				break;
			case 270:
				this.Landscape270.IsChecked = true;
				break;
			}
			this.Landscape.Content = string.Format("回転", pages.PrinterSettings.LandscapeAngle);

			PrinterName_SelectionChanged(null, null);

			return this.ShowDialog() ?? false;
		}

		private void InitPaperName()
		{
			var psz = (from p in this.PaperSizes where p.PaperName == this.PageSettings.PaperSize.PaperName select p).FirstOrDefault();
			if (psz == null)
			{
				var pslist = (from p in this.PaperSizes where p.Kind == this.PageSettings.PaperSize.Kind select p).ToList();
				if (pslist.Count == 1)
				{
					psz = pslist[0];
				}
				else
				{
					int h = 0, w = 0;
					foreach (var item in this.PaperSizes)
					{
						if (item.Height == this.PageSettings.PaperSize.Height && item.Width == this.PageSettings.PaperSize.Width)
						{
							psz = item;
							break;
						}
						if (h == 0 && w == 0)
						{
							h = this.PageSettings.PaperSize.Height;
							w = this.PageSettings.PaperSize.Width;
						}
						int dh = Math.Abs(item.Height - this.PageSettings.PaperSize.Height);
						int dw = Math.Abs(item.Width - this.PageSettings.PaperSize.Width);
						if ((dh > Math.Abs(h - this.PageSettings.PaperSize.Height)) && (dw > Math.Abs(w - this.PageSettings.PaperSize.Width)))
						{
							h = this.PageSettings.PaperSize.Height;
							w = this.PageSettings.PaperSize.Width;
							psz = item;
						}
					}
				}
			}
			if (psz != null)
			{
				if (this.PageSettings.PaperSize.Kind == PaperKind.Custom)
				{
					this.PaperSizeName = this.PageSettings.PaperSize.PaperName;
					this.CustomWidth = this.PageSettings.PaperSize.Width;
					this.CustomHeight = this.PageSettings.PaperSize.Height;
				}
				else
				{
					this.PaperSizeName = psz.PaperName;
					this.CustomHeight = psz.Height;
					this.CustomWidth = psz.Width;
				}
			}
		}

		private void InitPaperSource()
		{
			var psc = (from p in this.PaperSources where p.SourceName == this.PageSettings.PaperSource.SourceName select p).FirstOrDefault();
			if (psc == null)
			{
				if (this.PaperSources.Count == 1)
				{
					psc = this.PaperSources[0];
				}
				else
				{
					psc = (from p in this.PaperSources where p.Kind == PaperSourceKind.AutomaticFeed select p).FirstOrDefault();
					if (psc == null)
					{
						psc = (from p in this.PaperSources where p.Kind == PaperSourceKind.FormSource select p).FirstOrDefault();
					}
					else
					{
						psc = this.PaperSources[0];
					}
				}
			}
			if (psc != null)
			{
				this.PaperSourceName = psc.SourceName;
			}
			else
			{
				this.PaperSourceName = string.Empty;
			}
		}

		private void Landscape_Checked(object sender, RoutedEventArgs e)
		{
			//this.Landscape0.Visibility = System.Windows.Visibility.Visible;
			//this.Landscape90.Visibility = System.Windows.Visibility.Visible;
			//this.Landscape180.Visibility = System.Windows.Visibility.Visible;
			//this.Landscape270.Visibility = System.Windows.Visibility.Visible;
			this.PageSettings.Landscape = true;
		}

		private void Landscape_UnChecked(object sender, RoutedEventArgs e)
		{
			//this.Landscape0.Visibility = System.Windows.Visibility.Hidden;
			//this.Landscape90.Visibility = System.Windows.Visibility.Hidden;
			//this.Landscape180.Visibility = System.Windows.Visibility.Hidden;
			//this.Landscape270.Visibility = System.Windows.Visibility.Hidden;
			this.PageSettings.Landscape = false;
		}


	}
}
 