using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyoeiSystem.Framework.Net
{
	/// <summary>
	/// TCP通信例外
	/// </summary>
	public class TcpException : Exception
	{
		/// <summary>
		/// コンストラクタ（引数なし）
		/// </summary>
		public TcpException()
			: base()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="msg">メッセージ</param>
		public TcpException(string msg)
			: base(msg)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="msg">メッセージ</param>
		/// <param name="ex">内部例外</param>
		public TcpException(string msg, Exception ex)
			: base(msg, ex)
		{
		}
	}

	/// <summary>
	/// SMTP通信例外
	/// </summary>
	public class SmtpException : TcpException
	{
		/// <summary>
		/// コンストラクタ（引数なし）
		/// </summary>
		public SmtpException()
			: base()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="msg">メッセージ</param>
		public SmtpException(string msg)
			: base(msg)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="msg">メッセージ</param>
		/// <param name="ex">内部例外</param>
		public SmtpException(string msg, Exception ex)
			: base(msg, ex)
		{
		}
	}

	/// <summary>
	/// POP通信例外
	/// </summary>
	public class PopException : TcpException
	{
		/// <summary>
		/// コンストラクタ（引数なし）
		/// </summary>
		public PopException()
			: base()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="msg">メッセージ</param>
		public PopException(string msg)
			: base(msg)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="msg">メッセージ</param>
		/// <param name="ex">内部例外</param>
		public PopException(string msg, Exception ex)
			: base(msg, ex)
		{
		}
	}

	/// <summary>
	/// WebAPI通信例外
	/// </summary>
	public class WebAPIFormatException : TcpException
	{
		private const string basemessage = "API用BODYのフォーマットがJson形式として正しくありません。";
		/// <summary>
		/// コンストラクタ（引数なし）
		/// </summary>
		public WebAPIFormatException()
			: base(basemessage)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">メッセージ</param>
		public WebAPIFormatException(string message)
			: base(string.Format("{0}\r\n{1}", basemessage, message))
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="ex">内部例外</param>
		public WebAPIFormatException(string message, Exception ex)
			: base(string.Format("{0}\r\n{1}", basemessage, message), ex)
		{
		}
	}

	/// <summary>
	/// マイナンバーAPI通信例外
	/// </summary>
	public class MyNumberAPIException : TcpException
	{
		/// <summary>
		/// コンストラクタ（引数なし）
		/// </summary>
		public MyNumberAPIException()
			: base()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="msg">メッセージ</param>
		public MyNumberAPIException(string msg)
			: base(msg)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="msg">メッセージ</param>
		/// <param name="ex">内部例外</param>
		public MyNumberAPIException(string msg, Exception ex)
			: base(msg, ex)
		{
		}
	}

	/// <summary>
	/// マイナンバー通信設定例外
	/// </summary>
	public class MyNumberAPIConfigException : MyNumberAPIException
	{
		/// <summary>
		/// コンストラクタ（引数なし）
		/// </summary>
		public MyNumberAPIConfigException()
			: base()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="msg">メッセージ</param>
		public MyNumberAPIConfigException(string msg)
			: base(msg)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="msg">メッセージ</param>
		/// <param name="ex">内部例外</param>
		public MyNumberAPIConfigException(string msg, Exception ex)
			: base(msg, ex)
		{
		}
	}

	/// <summary>
	/// マイナンバー通信証明書例外
	/// </summary>
	public class MyNumberAPICertException : MyNumberAPIException
	{
		/// <summary>
		/// コンストラクタ（引数なし）
		/// </summary>
		public MyNumberAPICertException()
			: base()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="msg">メッセージ</param>
		public MyNumberAPICertException(string msg)
			: base(msg)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="msg">メッセージ</param>
		/// <param name="ex">内部例外</param>
		public MyNumberAPICertException(string msg, Exception ex)
			: base(msg, ex)
		{
		}
	}
}
