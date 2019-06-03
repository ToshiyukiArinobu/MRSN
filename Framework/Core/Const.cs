using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyoeiSystem.Framework.Core
{
	//public static class ConstThreadControl
	//{
	//	// エラーメッセージ
	//	public const string Error = "Thread control Error";
	//	public const string ErrNotInitialized = "Thread control Not Initialized";
	//	public const string ErrTimeout = "Thread timeout";
	//}

	/// <summary>
	/// 定数定義（データアクセス時エラーメッセージ）
	/// </summary>
	public static class ConstDataAccess
	{
		/// <summary>
		/// データアクセスエラー（一般）
		/// </summary>
		public const string Error = "Data Access Error";
		/// <summary>
		/// データタイプ不整合
		/// </summary>
		public const string ErrDataTypeUnknown = "Data Type is unknown";
		//public const string ErrDataNameNotfound = "Data Name is not found";
		/// <summary>
		/// データアクセスメソッド不明
		/// </summary>
		public const string ErrUnknownMethod = "Method is not found";
		//public const string ErrConnect = "Cannnot connect";
	}


}
