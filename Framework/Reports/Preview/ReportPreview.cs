using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;

using KyoeiSystem.Framework.Common;
using System.Diagnostics;
using CrystalDecisions.Shared;
using System.Drawing.Printing;
using KyoeiSystem.Framework.Windows.ViewBase;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Configuration;
using System.Collections.Specialized;

namespace KyoeiSystem.Framework.Reports.Preview
{
	/// <summary>
	/// CrystalReports用プレビュー及び印刷機能
	/// <remarks>
	/// <para>★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★</para>
	/// <para>印刷ジョブのキュータイトルは、以下の条件でのみ指定できる。</para>
	/// <para>画面：Form</para>
	/// <para>レポートプレビューコントロール：Form版またはWPF版</para>
	/// <para>（Form版のプレビューコントロールは、PrintMode = PrintToPrinter である必要がある。）</para>
	/// <para>印刷時に指定したときに選択したプリンターを取得するためには、独自に印刷ダイアログを開くしかないため
	/// プレビューの印刷ボタンを乗っ取る必要がある。</para>
	/// <para>なお、プレビューコントロールは、Form版とWPF版で異なるため、同じRPTからプレビューしても
	/// どちらかが ##### のような表示になる可能性がある。その状況は動作するPCによっても異なる。</para>
	/// ★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★</para>
	/// </remarks>
	/// </summary>
	public partial class ReportPreview : Form
	{
		#region 内部変数

		List<ReportParameter> parameters = null;
		private ReportDocument targetReportDocument = null;

		private object _ReportData = null;

		/// <summary>
		/// CrystalReportsのレポート定義ファイルパス
		/// </summary>
		private string reportFilePath = string.Empty;
		/// <summary>
		/// 印刷先のプリンター名
		/// 空文字の場合は「通常使用するプリンター」を対象とする
		/// </summary>
		private string _printerName = string.Empty;

		public bool IsPrinted = false;

		private List<string> printfiles = new List<string>();
		private string basedir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + @"\HakoboWork";

		public string ReportName
		{
			get;
			set;
		}
		public FwPrinterInfo _printerInfo = null;
		public FwPrinterInfo PrinterInfo
		{
			get { return _printerInfo; }
			set
			{
				_printerInfo = value;
				if (value != null)
				{
					if (cboxCustom != null)
					{
						cboxCustom.IsChecked = value.isCustomMode;
					}
				}
			}
		}

		private string defaultPrinterName = string.Empty;

		/// <summary>
		/// 印刷先のプリンター名
		/// 空文字の場合は「通常使用するプリンター」を対象とする
		/// </summary>
		public string PrinterName
		{
			get { return this._printerName; }
			set
			{
				if (PrinterList.Contains(value))
				{
					this._printerName = value;
				}
				else
				{
					this._printerName = defaultPrinterName;
				}

				//if (string.IsNullOrWhiteSpace(this._printerName))
				//{
				//	System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
				//	this._printerName = pd.PrinterSettings.PrinterName;
				//}
				//if (this.PrinterInfo != null)
				//{
				//	if (this.PageSettings != null)
				//	{
				//		if (this.PageSettings.PrinterSettings != null)
				//		{
				//			this.PageSettings.PrinterSettings.PrinterName = value;

				//		}
				//	}
				//}

			}
		}

        private string _paperSourceName = string.Empty;

		private List<string> _printerList = null;
		public List<string> PrinterList
		{
			get { return this._printerList; }
			set { this._printerList = value; }
		}
		private PrinterSettings _printerSettings = null;
		public PrinterSettings PrinterSettings
		{
			get { return _printerSettings; }
			set
			{
				_printerSettings = value;
				if (value == null)
				{
					PrinterName = null;
				}
				else
				{
					PrinterName = value.PrinterName;
				}
			}
		}

		private PageSettings _pageSettings = null;
		public PageSettings PageSettings
		{
			get { return _pageSettings; }
			set
			{
				_pageSettings = value;
				if (value == null)
				{
					PrinterSettings = null;
				}
				else
				{
					PrinterSettings = value.PrinterSettings;
				}
			}
		}

		public List<System.Drawing.Printing.PaperSize> PaperSizes;
		public List<System.Drawing.Printing.PaperSource> PaperSources;

		private bool _isCustomizeAvailable = false;
		public bool IsCustomizeAvailable
		{
			get { return _isCustomizeAvailable; }
			set { _isCustomizeAvailable = value; }
		}

		private bool _isCustomMode = false;
		public bool IsCustomMode
		{
			get { return _isCustomMode; }
			set { _isCustomMode = value; }
		}

		public bool _isDotPrinter = false;
		public bool IsDotPrinter
		{
			get { return _isDotPrinter; }
			set { _isDotPrinter = value; this.IsCustomMode = value; }
		}

		System.Windows.Controls.ToolBar crviewToolbar = null;
		System.Windows.Controls.Button originalPrintbtn = null;

		private System.Windows.Controls.Button btnPrintOut = null;
		private System.Windows.Controls.Button btnClose = null;
		//private System.Windows.Controls.Button btnPaperSize = null;
		private KyoeiSystem.Framework.Windows.Controls.UcCheckBox cboxCustom = null;

		System.Windows.Thickness marginCustomButton = new System.Windows.Thickness(5, 0, 5, 0);
		System.Windows.Thickness marginCloseButton = new System.Windows.Thickness(5, 0, 5, 0);
		private int posCutomButton = 3;
		private int posCloseButton = 10;

		#endregion

		#region 画面クラス初期化

		public ReportPreview(System.Windows.Window wnd = null)
		{
			InitializeComponent();

			this.CRVIEWER.ShowCopyButton = false;
			this.CRVIEWER.ShowExportButton = false;
			this.CRVIEWER.ShowLogo = false;
			this.CRVIEWER.ShowOpenFileButton = false;
			this.CRVIEWER.ShowPrintButton = false;
			this.CRVIEWER.ShowRefreshButton = false;
			this.CRVIEWER.ShowToggleSidePanelButton = false;
			this.CRVIEWER.ViewerCore.Zoom(75);
			this.CRVIEWER.ToggleSidePanel = SAPBusinessObjects.WPF.Viewer.Constants.SidePanelKind.None;

			if (wnd != null)
			{
				this.Top = (int)wnd.Top;
				this.Left = (int)wnd.Left;
				this.Height = (int)wnd.Height;
				this.Width = (int)wnd.Width;
			}

			var plist = (NameValueCollection)ConfigurationManager.GetSection("previewSettings");
			if (plist != null)
			{
				if (plist["customize"] == "available")
				{
					IsCustomizeAvailable = true;
				}
				LoadToolItemProperty(plist["customButton"], marginCustomButton, ref posCutomButton);
				LoadToolItemProperty(plist["closeButton"], marginCloseButton, ref posCloseButton);
			}
			SetupPrintButton(this.CRVIEWER);

			System.Printing.PrintQueue defaultPrinter = new System.Printing.LocalPrintServer().DefaultPrintQueue;
			this.defaultPrinterName = defaultPrinter.FullName;

			this.PrinterList = new List<string>();
			foreach (string pnm in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
			{
				this.PrinterList.Add(pnm);
			}
			
		}

		void LoadToolItemProperty(string prpty, System.Windows.Thickness margins, ref int positioin)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(prpty) != true)
				{
					string[] sizes = prpty.Split(new string[] { ",", }, StringSplitOptions.None);
					if (sizes.Length > 0)
					{
						if (string.IsNullOrWhiteSpace(sizes[0]) != true)
						{
							positioin = Convert.ToInt32(sizes[0]);
						}
					}
					if (sizes.Length > 1)
					{
						margins.Left = margins.Right = Convert.ToInt32(sizes[1]);
					}
					if (sizes.Length > 2)
					{
						margins.Right = Convert.ToInt32(sizes[2]);
					}
				}
			}
			catch (Exception ex)
			{
#if DEBUG
				MessageBox.Show("AppConfig ERROR : " + ex.Message);
#endif
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			this.Enabled = true;
			System.Windows.Input.Mouse.OverrideCursor = null;
			if (this.IsCustomizeAvailable)
			{
				makeCustomButton();
			}
			if (this.PrinterInfo != null && this.cboxCustom != null)
			{
				this.cboxCustom.IsChecked = this.PrinterInfo.isCustomMode;
			}

			if (this.targetReportDocument == null)
			{
				throw new ReportException(CommonConst.ErrReportObjectNotReady, new NullReferenceException());
			}
			try
			{
				this.CRVIEWER.ViewerCore.EnableDrillDown = false;

				if (this.targetReportDocument == null)
				{
					throw new ReportException(CommonConst.ErrReportObjectNotReady, new NullReferenceException());
				}
				try
				{
					this.CRVIEWER.ViewerCore.ReportSource = this.targetReportDocument;
					if (this.PrinterInfo == null)
					{
						this.PrinterInfo = InitializeSettings();
					}
					else
					{
						if (this.PageSettings != null)
						{
							if (this.PageSettings.PrinterSettings != null)
							{
								this.PageSettings.PrinterSettings.PrintRange = PrintRange.AllPages;
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("システムエラーです。");
			}

		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			this.Enabled = true;
			System.Windows.Input.Mouse.OverrideCursor = null;

			crviewToolbar = ViewBaseCommon.FindLogicalChildList<System.Windows.Controls.ToolBar>(this.CRVIEWER as System.Windows.DependencyObject).First();
			crviewToolbar.Items.Clear();

			if (btnPrintOut != null)
			{
				AppLogger.Instance.Debug("btnPrintOut is alive");
				btnPrintOut.Click -= ReportPreviewPrintButton_Click;
				btnPrintOut = null;
			}
			if (btnClose != null)
			{
				AppLogger.Instance.Debug("btnClose is alive");
				btnClose.Click -= closebtn_Click;
				btnClose = null;
			}
			if (cboxCustom != null)
			{
				AppLogger.Instance.Debug("cboxCustom is alive");
				cboxCustom.Checked -= cbox_Checked;
				cboxCustom.UnChecked -= cbox_Unchecked;
				cboxCustom = null;
			}
			if (targetReportDocument != null)
			{
				AppLogger.Instance.Debug("targetReportDocument is alive");
				targetReportDocument.Close();
				targetReportDocument.Dispose();
				targetReportDocument = null;
			}
			if (_ReportData is DataTable)
			{
				(_ReportData as DataTable).Dispose();
			}
			else  if (_ReportData is DataSet)
			{
				var ds = (_ReportData as DataSet);
				foreach (DataTable tbl in ds.Tables)
				{
					tbl.Dispose();
				}
				ds.Tables.Clear();
				ds.Dispose();
			}
			_ReportData = null;

		}

		#endregion

		#region レポート定義セット

		public void MakeReportDot(string reportName, string reportfile, double marginLeft = 0, double marginTop = 0, int pageRowCount = 0)
		{
			IsCustomMode = true;
			MakeReport(reportName, reportfile, marginLeft, marginTop, pageRowCount);
			//// レポート定義の「ページオプション」の「書式設定ページサイズと用紙サイズを別個に設定する」をfalseにしておく
			//this.targetReportDocument.PrintOptions.DissociatePageSizeAndPrinterPaperSize = false;
		}

		public void MakeReport(string reportName, string reportfile, double marginLeft = 0, double marginTop = 0, int pageRowCount = 0)
		{
			try
			{
				this.targetReportDocument = new ReportDocument();
				// レポート定義の「ページオプション」の「書式設定ページサイズと用紙サイズを別個に設定する」をfalseにしておく
				this.targetReportDocument.PrintOptions.DissociatePageSizeAndPrinterPaperSize = false;
				//if (string.IsNullOrWhiteSpace(this.PrinterName) != true)
				//{
				//	this.targetReportDocument.PrintOptions.PrinterName = this.PrinterName;
				//}
				this.targetReportDocument.Load(reportfile);
				this.targetReportDocument.SummaryInfo.ReportTitle = reportName;
				this.reportFilePath = reportfile;
				this.ReportName = reportName;
				this.Text = reportName;

				InitializeSettings();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportName">帳票名</param>
        /// <param name="reportfile">レポートファイルパス</param>
        /// <param name="paperSourceName">プリンタ出力トレイ名</param>
        /// <param name="marginLeft">左マージン</param>
        /// <param name="marginTop">上マージン</param>
        /// <param name="pageRowCount">ページ行数</param>
        public void MakeReport(string reportName, string reportfile, string paperSourceName, double marginLeft = 0, double marginTop = 0, int pageRowCount = 0)
        {
            try
            {
                this.targetReportDocument = new ReportDocument();
                // レポート定義の「ページオプション」の「書式設定ページサイズと用紙サイズを別個に設定する」をfalseにしておく
                this.targetReportDocument.PrintOptions.DissociatePageSizeAndPrinterPaperSize = false;
                //if (string.IsNullOrWhiteSpace(this.PrinterName) != true)
                //{
                //	this.targetReportDocument.PrintOptions.PrinterName = this.PrinterName;
                //}
                this.targetReportDocument.Load(reportfile);
                this.targetReportDocument.SummaryInfo.ReportTitle = reportName;
                this.reportFilePath = reportfile;
                this.ReportName = reportName;
                this._paperSourceName = paperSourceName;
                this.Text = reportName;

                InitializeSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

		private FwPrinterInfo InitializeSettings()
		{
			// 一旦、レポート定義から設定情報を取得しておく
			PrinterSettings prts = new PrinterSettings();
			PageSettings pages = new PageSettings();
			if (this.targetReportDocument != null)
			{
				this.targetReportDocument.PrintOptions.CopyTo(prts, pages);
			}
			if (string.IsNullOrWhiteSpace(this.PrinterName) || this.PrinterList.Contains(this.PrinterName) != true)
			{
				this._printerName = this.defaultPrinterName;
			}
			prts.PrinterName = this.PrinterName;
			pages.PrinterSettings = prts;

			this.PageSettings = pages;

			this.PaperSizes = new List<System.Drawing.Printing.PaperSize>();
			foreach (System.Drawing.Printing.PaperSize item in prts.PaperSizes)
			{
				this.PaperSizes.Add(item);
			}

			this.PaperSources = new List<System.Drawing.Printing.PaperSource>();
			foreach (System.Drawing.Printing.PaperSource item in prts.PaperSources)
			{
				this.PaperSources.Add(item);
			}

			InitPaperName();
			InitPaperSource();

			return new FwPrinterInfo()
				{
					printerName = this.PageSettings.PrinterSettings.PrinterName,
					//pageSettings = pages,
					paperSourceName = this.PageSettings.PaperSource.SourceName,
					margins = this.PageSettings.Margins,
					paperHeight = this.PageSettings.PaperSize.Height,
					paperWidth = this.PageSettings.PaperSize.Width,
					paperSizeName = this.PageSettings.PaperSize.PaperName,
					isCustomPaper = (this.PageSettings.PaperSize.PaperName == "Custom"),
					isCustomMode = this.IsCustomMode,
					landscape = this.PageSettings.Landscape,
				};
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
			//if (psz != null)
			//{
			//	if (this.PageSettings.PaperSize.Kind == PaperKind.Custom)
			//	{
			//		this.PaperSizeName = this.PageSettings.PaperSize.PaperName;
			//		this.CustomWidth = this.PageSettings.PaperSize.Width;
			//		this.CustomHeight = this.PageSettings.PaperSize.Height;
			//	}
			//	else
			//	{
			//		this.PaperSizeName = psz.PaperName;
			//		this.CustomHeight = psz.Height;
			//		this.CustomWidth = psz.Width;
			//	}
			//}
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
			//if (psc != null)
			//{
			//	this.PaperSourceName = psc.SourceName;
			//}
			//else
			//{
			//	this.PaperSourceName = string.Empty;
			//}
		}

		#endregion

		#region データセット
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

		public void SetReportData(DataSet data)
		{
			try
			{
				_ReportData = data;
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
				_ReportData = data;
				this.targetReportDocument.SetDataSource(data);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		//public void SetReportData(object data)
		//{
		//	try
		//	{
		//		_ReportData = data;
		//		this.targetReportDocument.SetDataSource(data as Enumerable);
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show(ex.Message);
		//	}
		//}

		#endregion

		#region プレビュー開始

		public bool? ShowPreview(System.Windows.Window wnd = null)
		{
			base.ShowDialog();

			return true;
		}

		#endregion

		//#region PDF関連

		///// <summary>
		///// PDF生成＆表示
		///// </summary>
		//public void PreviewPDF(int i部数 = 1)
		//{
		//	try
		//	{
		//		string docfnm = string.Empty;
		//		DirectoryInfo dir = new DirectoryInfo(basedir);
		//		if (!dir.Exists)
		//		{
		//			dir.Create();
		//		}
		//		string outfnm = string.Format(@"{0}\{1}.pdf", dir.FullName, this.ReportName);

		//		MergePDFs(printfiles.ToArray(), outfnm);

		//		var prc = Process.Start(outfnm);
		//		prc.WaitForExit();
		//		this.Close();

		//	}
		//	catch (Exception ex)
		//	{
		//		throw ex;
		//	}
		//}

		//public string GetPdfViewerPath()
		//{
		//	string path = String.Empty;
		//	Microsoft.Win32.RegistryKey rKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\Acrobat.exe");
		//	if (rKey == null) rKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\AcroRd32.exe");

		//	try
		//	{
		//		// レジストリから読み込み
		//		path = rKey.GetValue("").ToString();
		//	}
		//	catch (System.Security.SecurityException) { throw; } //レジストリキーからの読み取りに必要なアクセス許可がユーザーにありません。
		//	catch (System.UnauthorizedAccessException) { throw; } //必要なレジストリ権限がユーザーにありません。
		//	//catch (System.ObjectDisposedException) { throw; } //破棄されたキーを参照した場合
		//	//catch (System.IO.IOException) { throw; }
		//	catch
		//	{
		//		throw new ApplicationException("AdobeR AcrobatR もしくは ReaderR がインストールされていないため、PDFファイルの印刷ができません。");
		//	}
		//	finally
		//	{
		//		rKey.Close();
		//		rKey = null;
		//	}

		//	return path;
		//}


		///// <summary>
		///// 複数PDFのマージ
		///// </summary>
		///// <param name="fileNames"></param>
		///// <param name="outFile"></param>
		//public void MergePDFs(String[] fileNames, string outFile)
		//{
		//	try
		//	{
		//		int pageOffset = 0;
		//		int f = 0;

		//		Document document = null;
		//		PdfCopy writer = null;

		//		while (f < fileNames.Length)
		//		{
		//			PdfReader reader = new PdfReader(fileNames[f]);
		//			reader.ConsolidateNamedDestinations();
		//			int n = reader.NumberOfPages;
		//			pageOffset += n;
		//			if (f == 0)
		//			{
		//				document = new Document(reader.GetPageSizeWithRotation(1));
		//				writer = new PdfCopy(document, new FileStream(outFile, FileMode.Create));
		//				document.Open();
		//			}

		//			for (int i = 0; i < n; )
		//			{
		//				++i;
		//				if (writer != null)
		//				{
		//					PdfImportedPage page = writer.GetImportedPage(reader, i);
		//					writer.AddPage(page);
		//				}
		//			}

		//			PRAcroForm form = reader.AcroForm;
		//			if (form != null && writer != null)
		//			{
		//				writer.Close();
		//			}
		//			f++;
		//		}
		//		if (document != null)
		//		{
		//			document.Close();
		//			document = null;
		//		}
		//		if (writer != null)
		//		{
		//			writer.Close();
		//			writer = null;
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		throw ex;
		//	}

		//}

		///// <summary>
		///// PDFファイル作成（各請求書毎）
		///// </summary>
		//public void PrintOutPDF()
		//{
		//	if (this.targetReportDocument == null)
		//	{
		//		throw new ReportException(CommonConst.ErrReportObjectNotReady, new NullReferenceException());
		//	}
		//	try
		//	{
		//		DirectoryInfo dir = new DirectoryInfo(basedir);
		//		if (!dir.Exists)
		//		{
		//			dir.Create();
		//		}

		//		string filepath = string.Format(basedir + @"\wk_{0:D5}.pdf", this.printfiles.Count);
		//		this.printfiles.Add(filepath);

		//		targetReportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, filepath);

		//		targetReportDocument.Close();
		//		targetReportDocument.Dispose();
		//		targetReportDocument = null;
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show(ex.Message);
		//	}
		//}

		//#endregion

		#region 印刷ボタン関連

		private void SetupPrintButton(SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer target)
		{
			crviewToolbar = ViewBaseCommon.FindLogicalChildList<System.Windows.Controls.ToolBar>(target as System.Windows.DependencyObject).First();
			// ToolBarは必ず１つだけ存在する
			foreach (var item in crviewToolbar.Items)
			{
				originalPrintbtn = item as System.Windows.Controls.Button;
				if (originalPrintbtn == null)
				{
					continue;
				}
				if (originalPrintbtn.ToolTip == null)
				{
					continue;
				}
				if ((originalPrintbtn.ToolTip as string).Contains("印刷") || (originalPrintbtn.ToolTip as string).Contains("Print"))
				{
					originalPrintbtn.Visibility = System.Windows.Visibility.Collapsed;
					originalPrintbtn.IsEnabled = false;
					break;
				}
				else
				{
					originalPrintbtn = null;
				}
			}

			if (originalPrintbtn == null)
			{
				return;
			}

			// 置換用印刷ボタン
			btnPrintOut = new System.Windows.Controls.Button()
			{
				Height = originalPrintbtn.Height,
				Width = originalPrintbtn.Width,
				Content = originalPrintbtn.Content,
				Padding = originalPrintbtn.Padding,
				Margin = originalPrintbtn.Margin,
				//Foreground = obtn.Foreground,
				Background = originalPrintbtn.Background,
				ToolTip = "印刷します。",
				Language = originalPrintbtn.Language,
				Template = originalPrintbtn.Template,
				HorizontalAlignment = originalPrintbtn.HorizontalAlignment,
				HorizontalContentAlignment = originalPrintbtn.HorizontalContentAlignment,
				ClickMode = originalPrintbtn.ClickMode,
				Style = originalPrintbtn.Style,
				Tag = originalPrintbtn.Tag,
			};

			crviewToolbar.Items.Insert(1, btnPrintOut);
			btnPrintOut.Click += ReportPreviewPrintButton_Click;

			// 閉じるボタン
			btnClose = new System.Windows.Controls.Button()
			{
				BorderBrush = System.Windows.Media.Brushes.Black,
				BorderThickness = new System.Windows.Thickness(1, 1, 1, 1),
				Content = " 閉じる ",
				Height = originalPrintbtn.Height,
				Padding = new System.Windows.Thickness(1, 1, 1, 1),
				Margin = marginCloseButton,
				//Foreground = obtn.Foreground,
				//Background = obtn.Background,
				ToolTip = "この画面を閉じます",
				Language = originalPrintbtn.Language,
				HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
				HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
				ClickMode = originalPrintbtn.ClickMode,
			};
			btnClose.Click += closebtn_Click;
			if (posCloseButton < crviewToolbar.Items.Count)
			{
				crviewToolbar.Items.Insert(posCloseButton, btnClose);
			}
			else
			{
				crviewToolbar.Items.Add(btnClose);
			}


			return;
		}

		private void makeCustomButton()
		{
			if (crviewToolbar == null || originalPrintbtn == null)
			{
				return;
			}
			if (cboxCustom == null)
			{
				// カスタム用紙設定ボタン
				cboxCustom = new KyoeiSystem.Framework.Windows.Controls.UcCheckBox()
				{
					cContent = "ｶｽﾀﾑ用紙指定",
					Height = originalPrintbtn.Height,
					Margin = marginCustomButton,
					VerticalAlignment = System.Windows.VerticalAlignment.Center,
					VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
					HorizontalAlignment = originalPrintbtn.HorizontalAlignment,
					HorizontalContentAlignment = originalPrintbtn.HorizontalAlignment,
					ToolTip = "カスタム用紙サイズを指定します。"
							+ "\r\nプリンターの用紙一覧に定義されていないサイズを指定できます。",
					IsChecked = this.IsCustomMode,
				};
				cboxCustom.Checked += cbox_Checked;
				cboxCustom.UnChecked += cbox_Unchecked;
				int pos = 0;
				if (posCutomButton < crviewToolbar.Items.Count)
				{
					foreach (System.Windows.Controls.Control item in crviewToolbar.Items)
					{
						if (item.IsEnabled)
						{
							pos++;
						}
					}
					crviewToolbar.Items.Insert(posCutomButton, cboxCustom);
				}
				else
				{
					crviewToolbar.Items.Add(cboxCustom);
				}
			}
		}

		void cbox_Unchecked(object sender, System.Windows.RoutedEventArgs e)
		{
			this.IsCustomMode = false;
			if (this.PrinterInfo != null)
			{
				this.PrinterInfo.isCustomMode = false;
			}
		}

		void cbox_Checked(object sender, System.Windows.RoutedEventArgs e)
		{
			this.IsCustomMode = true;
			if (this.PrinterInfo != null)
			{
				this.PrinterInfo.isCustomMode = true;
			}
		}

		/// <summary>
		/// 閉じるボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void closebtn_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// 印刷ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ReportPreviewPrintButton_Click(object sender, EventArgs e)
		{
			if (IsCustomMode)
			{
				bool result = SetupPrinterInfo(this.PrinterInfo, PrinterSettingMode.Print);
				if (result != true)
				{
					this.IsPrinted = false;
					return;
				}
				PrintOutToPrinter(this.PrinterInfo, this.PageSettings.PrinterSettings.FromPage, this.PageSettings.PrinterSettings.ToPage, this.PageSettings.PrinterSettings.Copies);
				this.IsPrinted = true;
				MessageBox.Show("印刷しました。");
			}
			else
			{
				PageSettings pages = ApplySettings(this.PrinterInfo);
				pages.PrinterSettings.Collate = true;
				if (pages.PrinterSettings.PrintRange == PrintRange.AllPages)
				{
					pages.PrinterSettings.FromPage = pages.PrinterSettings.ToPage = 0;
				}

				PrintDialog pDialog = new PrintDialog();
				pDialog.PrinterSettings = pages.PrinterSettings;
				pDialog.AllowCurrentPage = false;		// 使えないので必ずfalse
				pDialog.AllowPrintToFile = false;		// 使えないので必ずfalse
				pDialog.AllowSelection = false;			// 使えないので必ずfalse
				pDialog.AllowSomePages = true;

				DialogResult print = pDialog.ShowDialog();
				if (print == System.Windows.Forms.DialogResult.OK)
				{
					pages.PrinterSettings = pDialog.PrinterSettings;
					this.PageSettings.PrinterSettings = pages.PrinterSettings;

					//try
					//{
					//	targetReportDocument.PrintOptions.CopyFrom(pages.PrinterSettings, pages);
					//	if (this.CRVIEWER.ViewerCore.ReportSource != null)
					//	{
					//		this.CRVIEWER.ViewerCore.ReuseParameterWhenRefresh = true;
					//		this.CRVIEWER.ViewerCore.RefreshReport();
					//	}
					//}
					//catch (Exception ex)
					//{
					//}

					targetReportDocument.PrintToPrinter(pages.PrinterSettings, pages, false);
					this.PrinterInfo = new FwPrinterInfo()
					{
						printerName = pages.PrinterSettings.PrinterName,
						//pageSettings = pages,
						paperSizeName = pages.PaperSize.PaperName,
						paperSourceName = pages.PaperSource.SourceName,
						paperWidth = pages.PaperSize.Width,
						paperHeight = pages.PaperSize.Height,
						margins = pages.Margins,
						isCustomPaper = pages.PaperSize.PaperName == "Custom" ? true : false,
						isCustomMode = this.IsCustomMode,
					};
					this.PrinterName = pages.PrinterSettings.PrinterName;
					this.IsPrinted = true;
					MessageBox.Show("印刷しました。");
				}
			}
		}

		#endregion


		#region プリンター設定情報

		public FwPrinterInfo GetPrinterInfo()
		{
			return this.PrinterInfo;
		}

		public bool SetupPrinterInfo(FwPrinterInfo prtInfo, PrinterSettingMode saveOrPrint = PrinterSettingMode.Save)
		{
			if (prtInfo == null)
			{
				prtInfo = InitializeSettings();
			}
			this.PrinterInfo = prtInfo;
			bool result;
			PrinterSettingWindow ps = new PrinterSettingWindow();
			ps.Title = string.Format("{0}用プリンター設定", this.ReportName);
			ps.OKボタン機能名 = saveOrPrint == PrinterSettingMode.Save ? "保存" : "印刷";

			PageSettings pages = ApplySettings(prtInfo);

			if (this.IsCustomMode)
			{
				result = ps.SettingDialogCustom(pages);
			}
			else
			{
				result = ps.SettingDialogStandard(pages);
			}
			if (result)
			{
				this.PageSettings = ps.PageSettings;
				this.PrinterInfo.printerName = ps.PageSettings.PrinterSettings.PrinterName;
				this.PrinterInfo.paperSizeName = ps.PaperSizeName;
				this.PrinterInfo.paperSourceName = ps.PaperSourceName;
				this.PrinterInfo.paperWidth = ps.CustomWidth;
				this.PrinterInfo.paperHeight = ps.CustomHeight;
				this.PrinterInfo.margins = ps.PageSettings.Margins;
				this.PrinterInfo.landscape = ps.PageSettings.Landscape;
				this.PrinterInfo.isCustomPaper = ps.IsCustomPageSize;
			}
			return result;
			
		}

		private PageSettings ApplySettings(FwPrinterInfo prtInfo, int fromPage = 0, int toPage = 0, int copies = 1)
		{
			try
			{
				this.PrinterInfo = prtInfo;

				PageSettings pages = new PageSettings();
				PrinterSettings prts = new PrinterSettings();
				// 変更対象のプロパティを調整
				// プリンタの設定をする。
				//prts.PrinterName = PrinterInfo.printerName;       // プリンタ名
				if (fromPage == 0 && toPage == 0)
				{
					// 全て印刷する場合
					prts.FromPage = 0;
					prts.ToPage = 0;
					prts.PrintRange = PrintRange.AllPages;
				}
				else
				{
					// 印刷範囲を指定する場合
					if (fromPage > toPage)
					{
						throw new ReportException("印刷するページ範囲を正しく指定してください。");
					}
                    if (fromPage == 0 || toPage == 0)
					{
						throw new ReportException("印刷するページ範囲を正しく指定してください。");
					}
					prts.PrintRange = PrintRange.SomePages;
					prts.FromPage = fromPage;
					prts.ToPage = toPage;
				}

				prts.PrinterName = PrinterInfo.printerName;
				prts.Copies = (short)copies;
				prts.Collate = false;
				pages.PrinterSettings = prts;

				foreach (System.Drawing.Printing.PaperSource item in prts.PaperSources)
				{
                    if (string.IsNullOrEmpty(_paperSourceName))
                    {
                        if (item.SourceName == PrinterInfo.paperSourceName)
                        {
                            pages.PaperSource = item;
                            break;
                        }
                    }
                    else
                    {
                        if (item.SourceName == _paperSourceName)
                        {
                            pages.PaperSource = item;
                            break;
                        }
                    }
				}

				// 用紙サイズがカスタムかどうか
				if (PrinterInfo.isCustomPaper)
				{
					pages.PaperSize = new System.Drawing.Printing.PaperSize("Custom", (int)PrinterInfo.paperWidth, (int)PrinterInfo.paperHeight);
					pages.PaperSize.RawKind = 256;
				}
				else
				{
					foreach (System.Drawing.Printing.PaperSize item in prts.PaperSizes)
					{
						if (item.PaperName == PrinterInfo.paperSizeName)
						{
							pages.PaperSize = item;
							break;
						}
					}
				}
				if (PrinterInfo.margins != null)
				{
					pages.Margins = PrinterInfo.margins;
				}

				pages.Landscape = prtInfo.landscape;

				return pages;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region 印刷

		/// <summary>
		/// 印刷実行（ユーザー定義用紙も反映）
		/// </summary>
		/// <param name="dataTable">プリンタ設定情報</param>
		/// <param name="fromPage">開始ページ（全て印刷の場合はNULL）</param>
		/// <param name="toPage">終了ページ（全て印刷の場合はNULL）</param>
		public void PrintOutToPrinter(FwPrinterInfo prtInfo, int fromPage = 0, int toPage = 0, int copies = 1)
		{
			//IsCustomMode = false;
			//IsDotPrinter = false;
			//// ■ 仮対処 
			//ShowPreview();
			//return;


			bool needsetting = false;
			if (prtInfo == null)
			{
				needsetting = true;
			}
			else
			{
				this.PageSettings = ApplySettings(prtInfo, fromPage, toPage, copies);
				if (this.PageSettings == null)
				{
					needsetting = true;
				}
				else
				{
					if (this.PageSettings.PrinterSettings == null)
					{
						needsetting = true;
					}
					else
					{
						if (prtInfo.printerName != this.PageSettings.PrinterSettings.PrinterName)
						{
							needsetting = true;
						}
						else
						{
							if (this.PrinterList.Contains(prtInfo.printerName) != true)
							{
								needsetting = true;
							}
						}
					}
				}
			}

			if (needsetting)
			{
				bool result = SetupPrinterInfo(prtInfo, PrinterSettingMode.Print);
				if (result != true)
				{
					this.IsPrinted = false;
					this.DialogResult = DialogResult.Cancel;
					return;
				}
			}
			else
			{
				this.PrinterInfo = prtInfo;
			}

			try
			{
				PageSettings pages = ApplySettings(this.PrinterInfo, fromPage, toPage, copies);

				targetReportDocument.PrintOptions.CopyFrom(pages.PrinterSettings, pages);
				if (this.CRVIEWER.ViewerCore.ReportSource != null)
				{
					this.CRVIEWER.ViewerCore.ReuseParameterWhenRefresh = true;
					this.CRVIEWER.ViewerCore.RefreshReport();
				}
				// 印刷処理を実行する。プリンタの設定、ページ設定を渡す。
				targetReportDocument.PrintToPrinter(pages.PrinterSettings, pages, true);

				this.IsPrinted = true;
			}
			catch (Exception ex)
			{
				this.IsPrinted = false;
				throw ex;
			}
		}

		#endregion

	}
}
