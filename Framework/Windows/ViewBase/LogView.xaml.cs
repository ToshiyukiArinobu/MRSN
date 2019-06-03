using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
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

namespace KyoeiSystem.Framework.Windows.ViewBase
{
	/// <summary>
	/// ログ表示画面
	/// </summary>
	public partial class LogView : WindowGeneralBase
	{
		private string logfilepath = string.Empty;
		/// <summary>
		/// ログファイル名
		/// </summary>
		public string LogFilePath
		{
			get { return logfilepath; }
			set { logfilepath = value; NotifyPropertyChanged(); }
		}

		private FlowDocument doc = null;
		/// <summary>
		/// ログファイルドキュメント
		/// </summary>
		public FlowDocument LogDocument
		{
			get { return doc; }
			set { doc = value; NotifyPropertyChanged(); }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public LogView()
		{
			InitializeComponent();
			this.DataContext = this;
			this.LogFilePath = this.appLog.LogFilePath();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			LoadLogFile();
		}

		void LoadLogFile()
		{
			try
			{
				FileInfo fi = new FileInfo(this.LogFilePath);
				Stream s = fi.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

				using (StreamReader rdr = new StreamReader(s, System.Text.Encoding.GetEncoding("shift-jis")))
				{
					var doc = new FlowDocument();
					var p = new Paragraph();
					p.FontFamily = new FontFamily("MS Gothic");
					p.FontSize = 11;
					while (!rdr.EndOfStream)
					{
						string line = rdr.ReadLine();
						p.Inlines.Add(line);
						if (line.IndexOf("] DEBUG ") > 0)
						{
							//p.Inlines.LastInline.Background = Brushes.LightSkyBlue;
						}
						else if ((line.IndexOf("] ERROR ") > 0) || (line.IndexOf("] FATAL ") > 0))
						{
							p.Inlines.LastInline.Background = Brushes.LightPink;
						}
						else if (line.IndexOf("] INFO  ") > 0)
						{
							//p.Inlines.LastInline.Background = Brushes.LightYellow;
						}
						p.Inlines.Add(new LineBreak());
					}
					doc.Blocks.Add(p);
					this.LogDocument = doc;
				}
				s.Close();
			}
			catch (Exception)
			{
			}
		}

		private void Close_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void Reflesh_Click(object sender, RoutedEventArgs e)
		{
			LoadLogFile();
		}
	}
}
