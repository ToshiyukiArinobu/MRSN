using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using CrystalDecisions.CrystalReports.Engine;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;

using KyoeiSystem.Framework.Common;
using System.Diagnostics;
using CrystalDecisions.Shared;
using System.IO;
using System.Data;

namespace KyoeiSystem.Framework.Reports.Preview
{
	public class ReportPDF
	{

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
			}
		}

		public ReportPDF()
		{
		}

		#region レポート定義セット

		public void MakeReport(string reportName, string reportfile, double marginLeft = 0, double marginTop = 0, int pageRowCount = 0)
		{
			try
			{
				this.targetReportDocument = new ReportDocument();
				this.targetReportDocument.PrintOptions.DissociatePageSizeAndPrinterPaperSize = false;
				this.targetReportDocument.Load(reportfile);
				this.targetReportDocument.SummaryInfo.ReportTitle = reportName;
				this.reportFilePath = reportfile;
				this.ReportName = reportName;

			}
			catch (Exception ex)
			{
				throw ex;
			}
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
				throw ex;
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
				throw ex;
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
				throw ex;
			}
		}

		public void SetReportData(object data)
		{
			try
			{
				_ReportData = data;
				this.targetReportDocument.SetDataSource(data as Enumerable);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region PDF関連

		/// <summary>
		/// PDF生成＆表示
		/// </summary>
		public void PreviewPDF(int i部数 = 1)
		{
			try
			{
				string docfnm = string.Empty;
				DirectoryInfo dir = new DirectoryInfo(basedir);
				if (!dir.Exists)
				{
					dir.Create();
				}
				string outfnm = string.Format(@"{0}\{1}.pdf", dir.FullName, this.ReportName);

				MergePDFs(printfiles.ToArray(), outfnm);

				ProcessStartInfo pi = new ProcessStartInfo()
				{
					UseShellExecute = true,
					FileName = outfnm,
					Verb = "open",
				};

				var prc = Process.Start(pi);
				if (prc != null)
				{
					prc.WaitForExit();
				}
				//else
				//{
				//	var prc2 = GetProcessesByWindowTitle(string.Format(@"{0}.pdf", this.ReportName)).FirstOrDefault();
				//	if (prc2 != null)
				//	{
				//		AppLogger.Instance.Debug("Process is found");
				//		prc2.WaitForExit();
				//	}
				//	else
				//	{
				//		AppLogger.Instance.Debug("Process is not found");
				//	}
				//}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public System.Diagnostics.Process[] GetProcessesByWindowTitle(string windowTitle)
		{
			System.Collections.ArrayList list = new System.Collections.ArrayList();

			//すべてのプロセスを列挙する
			foreach (System.Diagnostics.Process p
				in System.Diagnostics.Process.GetProcesses())
			{
				//指定された文字列がメインウィンドウのタイトルに含まれているか調べる
				if (p.MainWindowTitle.Contains(windowTitle))
				{
					//含まれていたら、コレクションに追加
					list.Add(p);
				}
			}

			//コレクションを配列にして返す
			return (System.Diagnostics.Process[])
				list.ToArray(typeof(System.Diagnostics.Process));
		}

		public string GetPdfViewerPath()
		{
			string path = String.Empty;
			Microsoft.Win32.RegistryKey rKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\Acrobat.exe");
			if (rKey == null) rKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\AcroRd32.exe");

			try
			{
				// レジストリから読み込み
				path = rKey.GetValue("").ToString();
			}
			catch (System.Security.SecurityException) { throw; } //レジストリキーからの読み取りに必要なアクセス許可がユーザーにありません。
			catch (System.UnauthorizedAccessException) { throw; } //必要なレジストリ権限がユーザーにありません。
			//catch (System.ObjectDisposedException) { throw; } //破棄されたキーを参照した場合
			//catch (System.IO.IOException) { throw; }
			catch
			{
				throw new ApplicationException("AdobeR AcrobatR もしくは ReaderR がインストールされていないため、PDFファイルの印刷ができません。");
			}
			finally
			{
				rKey.Close();
				rKey = null;
			}

			return path;
		}


		/// <summary>
		/// 複数PDFのマージ
		/// </summary>
		/// <param name="fileNames"></param>
		/// <param name="outFile"></param>
		public void MergePDFs(String[] fileNames, string outFile)
		{
			try
			{
				int pageOffset = 0;
				int f = 0;

				Document document = null;
				PdfCopy writer = null;

				while (f < fileNames.Length)
				{
					using (PdfReader reader = new PdfReader(fileNames[f]))
					{
						reader.ConsolidateNamedDestinations();
						int n = reader.NumberOfPages;
						pageOffset += n;
						if (f == 0)
						{
							document = new Document(reader.GetPageSizeWithRotation(1));
							writer = new PdfCopy(document, new FileStream(outFile, FileMode.Create));
							document.Open();
						}

						for (int i = 0; i < n; )
						{
							++i;
							if (writer != null)
							{
								PdfImportedPage page = writer.GetImportedPage(reader, i);
								writer.AddPage(page);
							}
						}

						//PRAcroForm form = reader.AcroForm;
						//if (form != null && writer != null)
						//{
						//	writer.Close();
						//	writer.Dispose();
						//	writer = null;
						//}
					}
					f++;
				}
				if (document != null)
				{
					document.Close();
					document = null;
				}
				if (writer != null)
				{
					writer.Close();
					writer.Dispose();
					writer = null;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		/// <summary>
		/// PDFファイル作成（各請求書毎）
		/// </summary>
		public void PrintOutPDF()
		{
			if (this.targetReportDocument == null)
			{
				throw new ReportException(CommonConst.ErrReportObjectNotReady, new NullReferenceException());
			}
			try
			{
				DirectoryInfo dir = new DirectoryInfo(basedir);
				if (!dir.Exists)
				{
					dir.Create();
				}

				string filepath = string.Format(basedir + @"\wk_{0:D5}.pdf", this.printfiles.Count);
				this.printfiles.Add(filepath);

				targetReportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, filepath);

				targetReportDocument.Close();
				targetReportDocument.Dispose();
				targetReportDocument = null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion


	}
}
