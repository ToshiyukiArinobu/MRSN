using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KyoeiSystem.Framework.Common
{
	/// <summary>
	/// 共通定数定義クラス（リソース定義に移行する可能性があります）
	/// </summary>
	public static partial class CommonConst
    {
		// ■ エラーメッセージ

		/// <summary>
		/// エラーメッセージ：レポート定義ファイルがない
		/// </summary>
		public const string ErrReportObjectNotReady = "Report object is not ready";
		/// <summary>
		/// エラーメッセージ：CSVファイルアクセスエラー
		/// </summary>
		public const string ErrCSVFile = "CSV File access error";
		/// <summary>
		/// エラーメッセージ：データベース接続エラー
		/// </summary>
		public static string DBOpenError = "Database Open Error";
		/// <summary>
		/// エラーメッセージ：データベースアクセスエラー
		/// </summary>
		public static string DBAccessError = "Database Access Error";

	}

	public static partial class CommonConst
	{
		// ■ エラーメッセージ以外

		/// <summary>
		/// レポートプレビュー表示用埋め込み文字
		/// </summary>
		public const char ReportFieldFiller = 'X';

		/// <summary>
		/// ライセンスDBを必要としないモード（LAN内専用モード固定）
		/// </summary>
		public static string WithoutLicenceDBMode = "withoutldb";
		/// <summary>
		/// LAN内専用モード（ライセンスDBあり：WCF稼動版の社内開発時）
		/// </summary>
		public static string LocalDBMode = "local";
	}

}
