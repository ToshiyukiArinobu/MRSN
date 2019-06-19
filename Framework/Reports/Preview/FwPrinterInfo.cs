using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyoeiSystem.Framework.Reports.Preview
{
	/// <summary>
	/// プリンター用設定項目
	/// </summary>
	public class FwPrinterInfo
	{
		public string printerName { get; set; }

		//public PageSettings pageSettings { get; set; }

		public string paperSourceName { get; set; }
		public string paperSizeName { get; set; }
		public bool isCustomMode { get; set; }
		public bool isCustomPaper { get; set; }
		public Margins margins { get; set; }
		public int paperWidth { get; set; }
		public int paperHeight { get; set; }
		public bool landscape { get; set; }
	}

	/// <summary>
	/// 印刷設定画面動作モード
	/// </summary>
	public enum PrinterSettingMode
	{
		/// <summary>
		/// 設定保存のみ
		/// </summary>
		Save,
		/// <summary>
		/// 印刷ページ範囲入力
		/// </summary>
		Print,
	}

}
