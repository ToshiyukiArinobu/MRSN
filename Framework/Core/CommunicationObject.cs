using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Specialized;

namespace KyoeiSystem.Framework.Core
{
	/// <summary>
	/// スレッド間通信用メッセージ
	/// </summary>
	public class CommunicationObject : MarshalByRefObject
	{
		/// <summary>
		/// メッセージ送信要求スレッド情報
		/// </summary>
		private Thread ReqesterThreadID;
		///// <summary>
		///// メッセージ応答スレッド情報
		///// </summary>
		//private Thread AnswerThreadID;

		/// <summary>
		/// メッセージ名
		/// </summary>
		private string Name = string.Empty;
		/// <summary>
		/// メッセージデータ
		/// </summary>
		private object data = null;
		/// <summary>
		/// メッセージタイプ
		/// </summary>
		public MessageType mType = MessageType.None;
		/// <summary>
		/// エラーの種類
		/// </summary>
		public MessageErrorType ErrorType = MessageErrorType.NoError;
		/// <summary>
		/// DB接続文字列
		/// </summary>
		public string connection = string.Empty;

		/// <summary>
		/// メッセージ名を取得する
		/// </summary>
		/// <returns>メッセージ名</returns>
		public string GetMessageName()
		{
			return this.Name;
		}

		/// <summary>
		/// 実行結果として受信したオブジェクトを取得する
		/// </summary>
		/// <remarks>
		/// データアクセスの各機能を実行した結果として受信したオブジェクトがListまたはArrayである場合、DataTable形式に変換されます。
		/// ただし、各コレクションの要素のメンバーは、getterのあるプロパティのみが対象となります。
		/// </remarks>
		/// <returns>受信した結果オブジェクト</returns>
		public object GetResultData()
		{
			object[] data = GetParameters();
			if (data == null)
			{
				return null;
			}
			if (data.Length>0)
			{
				return data[0];
			}
			return null;
		}

		/// <summary>
		/// パラメータとして指定されたオブジェクトを取得する
		/// </summary>
		/// <returns>データアクセス機能に渡される引数オブジェクトのコレクション</returns>
		public object[] GetParameters()
		{
			if (this.data == null)
			{
				return null;
			}
			if (this.data.GetType() == typeof(CommunicationData))
			{
				var param = (this.data as CommunicationData).Parameters;
				List<object> keys = new List<object>();
				Type tp = param.GetType();
				if (tp == typeof(List<ComColumn>))
				{
					foreach (ComColumn rkey in param)
					{
						keys.Add(rkey.Value);
					}
					return keys.ToArray();
				}
				else
				{
					return new object[] { (param[0] as ComColumn).Value };
				}
			}
			return null;
		}

		/// <summary>
		/// コンストラクタ（通常）
		/// </summary>
		/// <param name="mtype">メッセージタイプ</param>
		/// <param name="messagename">メッセージ名</param>
		/// <param name="parameters">データアクセス機能に渡される引数オブジェクト（可変長）</param>
		public CommunicationObject(MessageType mtype, string messagename, params object[] parameters)
		{
			this.Initialize(mtype, MessageErrorType.NoError, messagename, parameters);
		}

		/// <summary>
		/// コンストラクタ（エラー時）
		/// </summary>
		/// <remarks>
		/// 呼び出し元に返すオブジェクトは、現状、エラーメッセージのみです。
		/// </remarks>
		/// <param name="mtype">メッセージタイプ</param>
		/// <param name="errType">エラー種別</param>
		/// <param name="messagename">メッセージ名</param>
		/// <param name="parameters">呼び出し元に返すオブジェクト</param>
		public CommunicationObject(MessageType mtype, MessageErrorType errType, string messagename, params object[] parameters)
		{
			this.Initialize(mtype, errType, messagename, parameters);
		}

		/// <summary>
		/// インスタンスの初期化を行います。
		/// </summary>
		/// <param name="mtype">メッセージタイプ</param>
		/// <param name="errType"></param>
		/// <param name="messagename">メッセージ名</param>
		/// <param name="parameters"></param>
		private void Initialize(MessageType mtype, MessageErrorType errType, string messagename, params object[] parameters)
		{
			this.mType = mtype;
			this.ErrorType = errType;
			this.Name = messagename;
			this.data = new CommunicationData();
			if (parameters != null)
			{
				ComColumn rkey;
				if (parameters.GetType().IsArray)
				{
					object[] keylist = parameters;
					int kno = 0;
					foreach (object okey in keylist)
					{
						rkey = new ComColumn() { Name = "key" + kno.ToString(), Value = okey };
						(this.data as CommunicationData).Parameters.Add(rkey);
						kno++;
					}
				}
				else
				{
					rkey = new ComColumn() { Name = "key", Value = parameters };
					(this.data as CommunicationData).Parameters.Add(rkey);
				}
			}
			ReqesterThreadID = Thread.CurrentThread;
		}
	}
}
