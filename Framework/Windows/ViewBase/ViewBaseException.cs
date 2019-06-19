using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyoeiSystem.Framework.Windows.ViewBase
{
	/// <summary>
	/// 画面基底クラス用例外クラス
	/// </summary>
	public class ViewBaseException : Exception
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ViewBaseException()
			: base()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">例外メッセージ</param>
		public ViewBaseException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">例外メッセージ</param>
		/// <param name="ex">内部例外</param>
		public ViewBaseException(string message, Exception ex)
			: base(message, ex)
		{
		}
	}
}
