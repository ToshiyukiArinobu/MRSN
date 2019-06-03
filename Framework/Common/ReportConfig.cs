using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;

namespace KyoeiSystem.Framework.Common
{
	/// <summary>
	/// 印刷用設定情報（現在未使用）
	/// </summary>
	public class ReportConfig
	{
		/// <summary>
		/// 設定名
		/// </summary>
		public string ReportName = string.Empty;
		/// <summary>
		/// 帳票タイトル
		/// </summary>
		public string ReportTitle = string.Empty;
		/// <summary>
		/// 帳票定義ファイル名
		/// </summary>
		public string ReportFileName = string.Empty;

		/// <summary>
		/// プリンター名
		/// </summary>
		public string PrinterName = string.Empty;
		/// <summary>
		/// 用紙サイズ
		/// </summary>
		public PaperSize PaperSize = null;
		/// <summary>
		/// 用紙種別
		/// </summary>
		public PaperKind PaperKind = PaperKind.A4;
		/// <summary>
		/// 用紙トレイ情報
		/// </summary>
		public PaperSource PaperSource = null;
		/// <summary>
		/// 余白情報
		/// </summary>
		public Margins Margin = new Margins(0, 0, 0, 0);
	}
}
