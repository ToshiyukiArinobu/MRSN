using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyoeiSystem.Framework.Common
{
	/// <summary>
	/// データベース接続例外クラス
	/// </summary>
	public class DBOpenException : Exception
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public DBOpenException()
			: base()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">例外メッセージ</param>
		public DBOpenException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="ex">内部例外</param>
		public DBOpenException(Exception ex)
			: base(CommonConst.DBOpenError, ex)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">例外メッセージ</param>
		/// <param name="ex">内部例外</param>
		public DBOpenException(string message, Exception ex)
			: base(message, ex)
		{
		}
	}

	/// <summary>
	/// 該当データが存在しない場合の例外クラス
	/// </summary>
	public class DBDataNotFoundException : Exception
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public DBDataNotFoundException()
			: base()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">例外メッセージ</param>
		public DBDataNotFoundException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">例外メッセージ</param>
		/// <param name="ex">内部例外</param>
		public DBDataNotFoundException(string message, Exception ex)
			: base(message, ex)
		{
		}
	}

	/// <summary>
	/// データ取得例外
	/// </summary>
	public class DBGetException : Exception
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public DBGetException()
			: base()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">例外メッセージ</param>
		public DBGetException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">例外メッセージ</param>
		/// <param name="ex">内部例外</param>
		public DBGetException(string message, Exception ex)
			: base(message, ex)
		{
		}
	}

	/// <summary>
	/// データ更新例外
	/// </summary>
	public class DBPutException : Exception
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public DBPutException()
			: base()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">例外メッセージ</param>
		public DBPutException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">例外メッセージ</param>
		/// <param name="ex">内部例外</param>
		public DBPutException(string message, Exception ex)
			: base(message, ex)
		{
		}
	}

	/// <summary>
	/// データ更新例外
	/// </summary>
	public class DBUpdateConflictException : Exception
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public DBUpdateConflictException()
			: base()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">例外メッセージ</param>
		public DBUpdateConflictException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">例外メッセージ</param>
		/// <param name="ex">内部例外</param>
		public DBUpdateConflictException(string message, Exception ex)
			: base(message, ex)
		{
		}
	}

	/// <summary>
	/// データアクセス例外
	/// </summary>
	public class DBAccessException : Exception
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public DBAccessException()
			: base()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">例外メッセージ</param>
		public DBAccessException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="ex">内部例外</param>
		public DBAccessException(Exception ex)
			: base(CommonConst.DBAccessError, ex)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">例外メッセージ</param>
		/// <param name="ex">内部例外</param>
		public DBAccessException(string message, Exception ex)
			: base(message, ex)
		{
		}
	}

	/// <summary>
	/// データアクセス制御例外
	/// </summary>
	public class FWThreadManagerException : Exception
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FWThreadManagerException()
			: base()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">例外メッセージ</param>
		public FWThreadManagerException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">例外メッセージ</param>
		/// <param name="ex">内部例外</param>
		public FWThreadManagerException(string message, Exception ex)
			: base(message, ex)
		{
		}
	}

	/// <summary>
	/// データアクセス実行例外
	/// </summary>
	public class FWThreadCoreDataException : Exception
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FWThreadCoreDataException()
			: base()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">例外メッセージ</param>
		public FWThreadCoreDataException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">例外メッセージ</param>
		/// <param name="ex">内部例外</param>
		public FWThreadCoreDataException(string message, Exception ex)
			: base(message, ex)
		{
		}
	}

	/// <summary>
	/// CSVファイルアクセス例外
	/// </summary>
	public class CSVException : Exception
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CSVException()
			: base()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">例外メッセージ</param>
		public CSVException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="message">例外メッセージ</param>
		/// <param name="ex">内部例外</param>
		public CSVException(string message, Exception ex)
			: base(message, ex)
		{
		}

	}
}
