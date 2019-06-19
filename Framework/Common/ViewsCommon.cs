using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Data;

namespace KyoeiSystem.Framework.Common
{
	/// <summary>
	/// 画面モジュール間で共有するオブジェクト
	/// </summary>
	public class ViewsCommon
	{
		/// <summary>
		/// ライセンスDBの有無を指示するフラグ
		/// </summary>
		public bool WithLicenseDB = false;

		class MSGMAS_Member
		{
			public string 種別 { get; set; }
			public int コード { get; set; }
			public string メッセージ { get; set; }
			public string サブメッセージ { get; set; }
		}

		List<MSGMAS_Member> msglist = new List<MSGMAS_Member>();

		/// <summary>
		/// データアクセス定義情報バージョン（未使用）
		/// </summary>
		public System.Version version = null;
		/// <summary>
		/// データアクセス定義情報
		/// </summary>
		public DataAccessConfig DacConf = null;

		object applicationData = null;
		/// <summary>
		/// アプリケーションによる共有オブジェクト
		/// </summary>
		public object AppData
		{
			get { return applicationData; }
			set { this.applicationData = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="appdata">アプリケーション共有オブジェクト</param>
		public ViewsCommon(object appdata)
		{
			this.AppData = appdata;
		}

		/// <summary>
		/// データベースアクセス定義情報をロードする
		/// </summary>
		/// <param name="ver">データアクセス定義情報バージョン（未実装のため不要）</param>
		public void Initialize(System.Version ver = null)
		{
			if (ver != null)
			{
				this.version = ver;
			}
			//DacConf = Utility.LoadDataAccessConfig();
		}

		/// <summary>
		/// メッセージリストを共有オブジェクトにセットアップします。
		/// </summary>
		/// <remarks>
		/// 引数として渡すDataTableの内容は以下の項目が必要です。
		/// <para>・種別（文字列）："E", "W" など、メッセージの種別を示す文字列</para>
		/// <para>・コード（数値）：メッセージのコード</para>
		/// <para>・メッセージ（文字列）：メッセージの主文</para>
		/// <para>・サブメッセージ（文字列）：メッセージの副文</para>
		/// GetMessage()メソッドにより、種別とコードの組合せからメッセージの主文と副文を結合した文字列が取得されます。
		/// </remarks>
		/// <param name="table">画面モジュールにて使用するメッセージリスト</param>
		public void SetupMessageList(System.Data.DataTable table)
		{
			foreach (System.Data.DataRow row in table.Rows)
			{
				MSGMAS_Member dat = new MSGMAS_Member();
				dat.種別 = (string)row["種別"];
				dat.コード = (int)row["コード"];
				dat.メッセージ = (string)row["メッセージ"];
				if (row["サブメッセージ"] == null)
				{
					dat.サブメッセージ = string.Empty;
				}
				else
				{
					dat.サブメッセージ = (string)row["サブメッセージ"];
				}
				this.msglist.Add(dat);
			}

		}

		/// <summary>
		/// 指定されたメッセージコードに対応するメッセージを取得する
		/// </summary>
		/// <param name="messagecode">メッセージコード（種別＋コード）</param>
		/// <returns>メッセージ文</returns>
		public string GetMessage(string messagecode)
		{
			int code = 0;
			if (int.TryParse(messagecode.Substring(1), out code))
			{
				return GetMessage(messagecode.Substring(0, 1), code);
			}
			return messagecode;
		}

		/// <summary>
		/// 指定されたメッセージコードに対応するメッセージを取得する
		/// </summary>
		/// <param name="kind">種別</param>
		/// <param name="code">コード</param>
		/// <returns>メッセージ文</returns>
		public string GetMessage(string kind, int code)
		{
			string msg;

			MSGMAS_Member msgdat = msglist.Where(m => m.種別 == kind && m.コード == code).FirstOrDefault();
			if (msgdat != null)
			{
				msg = msgdat.メッセージ + msgdat.サブメッセージ;
			}
			else
			{
				msg = "システムエラー(メッセージコード不明)";
			}

			return msg;
		}
	}

}
