using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KyoeiSystem.Framework.Core
{
	/// <summary>
	/// WCFサービスのインターフェース定義
	/// </summary>
	[ServiceContract]
	public interface IKESService
	{
		/// <summary>
		/// WCFサービス機能呼び出し用メソッド
		/// </summary>
		/// <param name="fname">機能名</param>
		/// <param name="kind">機能タイプ</param>
		/// <param name="cstr">接続情報</param>
		/// <param name="ptypes">パラメータタイプ</param>
		/// <param name="plist">パラメータ</param>
		/// <returns>各機能の戻り値</returns>
		[OperationContract]
		object CallFunction(string fname, string kind, string cstr, string[] ptypes, object[] plist);

	}

	/// <summary>
	/// 複数のソリューションに対応するが、WCFサービスの名前空間は固定しておく。
	/// </summary>
	public static class DataAccessConst
	{
		/// <summary>
		/// アプリケーションWCFクラスライブラリのNamespace名省略時の値
		/// </summary>
		public static string APLWCFNAMESPACE = "KyoeiSystem.Application.WCFService";
		/// <summary>
		/// アプリケーションWCFクラスライブラリのDLL名省略時の値
		/// </summary>
		public static string APLWCFDLL = APLWCFNAMESPACE + ".dll";
		/// <summary>
		/// アプリケーションDACクラス名省略時の値
		/// </summary>
		public static string DACCLASSNAME = APLWCFNAMESPACE + ".DataAccessCfg";
		/// <summary>
		/// アプリケーションWCFサービス名省略時の値
		/// </summary>
		public static string SCVCLASSNAME = APLWCFNAMESPACE + ".KESSVCEntry";
		/// <summary>
		/// DACエントリー情報取得用メソッド名
		/// </summary>
		public static string GETCONFIGMETHODNAME = "GetDacCfg";
	}

	/// <summary>
	/// スレッド間通信メッセージタイプ
	/// </summary>
	public enum MessageType
	{
		/// <summary>
		/// メッセージ内容なし（Default）
		/// </summary>
		None,

		//----- 要求メッセージ -----
		/// <summary>
		/// 指定時間待機（ミリ秒）
		/// </summary>
		TimerLoop,
		/// <summary>
		/// 指定条件データの取得要求（Dataset）
		/// </summary>
		RequestData,
		/// <summary>
		/// 指定条件データの取得要求（Dataset変換なし）
		/// </summary>
		RequestDataNoManage,
		/// <summary>
		/// 更新データ（Dataset）
		/// </summary>
		UpdateData,
		/// <summary>
		/// ストアドプロシージャ呼出
		/// </summary>
		CallStoredProcedure,

		/// <summary>
		/// データアクセス要求（画面のビジー・フリー制御）
		/// </summary>
		RequestDataWithBusy,

		/// <summary>
		/// ライセンスDBアクセス専用
		/// </summary>
		RequestLicense,

		//----- 応答メッセージ -----
		/// <summary>
		/// 取得結果データ（WCF）
		/// </summary>
		ResponseData,
		/// <summary>
		/// 更新結果データ（WCF）
		/// </summary>
		ResponsePut,
		/// <summary>
		/// ストアド呼出の結果
		/// </summary>
		ResponseStored,

		/// <summary>
		/// RequestDataWithBusyに対する応答
		/// </summary>
		ResponseWithFree,

		/// <summary>
		/// エラー通知
		/// </summary>
		Error,
		/// <summary>
		/// RequestDataWithBusyに対するエラー応答
		/// </summary>
		ErrorWithFree,
	}

	/// <summary>
	/// データアクセスクラスの実行時エラーのタイプ
	/// </summary>
	public enum MessageErrorType
	{
		/// <summary>
		/// 正常（エラーなし）
		/// </summary>
		NoError,
		/// <summary>
		/// 該当データなし
		/// </summary>
		DataNotFound,
		/// <summary>
		/// 他のトランザクションとの衝突により更新不可
		/// </summary>
		UpdateConflict,
		/// <summary>
		/// データアクセスエラー
		/// </summary>
		DataError,
		/// <summary>
		/// データベース接続エラー
		/// </summary>
		DBConnectError,
		/// <summary>
		/// データ取得エラー
		/// </summary>
		DBGetError,
		/// <summary>
		/// データ更新エラー
		/// </summary>
		DBUpdateError,
		/// <summary>
		/// システムエラー
		/// </summary>
		SystemError,
	}

	/// <summary>
	/// スレッド関連情報
	/// </summary>
	/// <remarks>
	/// 画面アプリケーションとデータアクセス処理のスレッド間通信を制御するための情報を保持します。
	/// ThreadManagerにて使用する情報であり、アプリケーション側でアクセスする必要はありません。
	/// </remarks>
	public class ThreadContext
	{
		/// <summary>
		/// ワーカースレッド情報
		/// </summary>
		public Thread _thread = null;
		/// <summary>
		/// ワーカースレッドの実行状態
		/// </summary>
		public bool isRunnig = false;
		/// <summary>
		/// ワーカースレッドへの停止要求フラグ
		/// </summary>
		public volatile bool stopflag = false;
		/// <summary>
		/// メッセージ送信要求フラグ
		/// </summary>
		public volatile bool requestflag = false;
		/// <summary>
		/// スレッド間通信で使用するオブジェクト
		/// </summary>
		public CommunicationObject commdata = null;
	}

	/// <summary>
	/// インターフェースオブジェクト
	/// </summary>
	public class CommunicationData
	{
		/// <summary>
		/// パラメータオブジェクトのコレクション
		/// </summary>
		public List<ComColumn> Parameters = new List<ComColumn>();
	}

	/// <summary>
	/// パラメータオブジェクト
	/// </summary>
	public class ComColumn
	{
		/// <summary>
		/// キー名
		/// </summary>
		public string Name = string.Empty;
		/// <summary>
		/// オブジェクト値
		/// </summary>
		public object Value = null;
	}

}
