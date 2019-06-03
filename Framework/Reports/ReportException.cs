using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyoeiSystem.Framework.Reports
{
	/// <summary>
	/// レポートクラス関連の例外クラス
	/// </summary>
	class ReportException : Exception
	{
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ReportException()
		{
		}

		/// <summary>
		/// コンストラクタ：メッセージ指定
		/// </summary>
		/// <param name="message">メッセージ</param>
		public ReportException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// コンストラクタ：メッセージ及び内部例外指定
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="innerException">内部例外</param>
		public ReportException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

	}
}
