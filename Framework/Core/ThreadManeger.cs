using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;

using KyoeiSystem.Framework.Common;

namespace KyoeiSystem.Framework.Core
{
	/// <summary>
	/// スレッド間通信イベント用デリゲート
	/// </summary>
	/// <param name="message">受信メッセージ</param>
	public delegate void MessageReceiveHandler(CommunicationObject message);

	/// <summary>
	/// スレッド管理クラス
	/// </summary>
	/// <remarks>
	/// 各画面は、用途に応じた基底クラスを継承すれば、基底クラス内部でスレッド管理の機能を使用するため、アプリケーションから直接利用する必要はない。
	/// </remarks>
	public partial class ThreadManeger : IDisposable
	{
		/// <summary>
		/// メッセージ受信イベント
		/// </summary>
		public event MessageReceiveHandler OnReceived;
		/// <summary>
		/// スレッド名（省略可）
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// データアクセス定義情報
		/// </summary>
		//public DataAccessConfig daccfg = null;

		private AppLogger appLog = null;

		private bool powerdownflag = false;

		private List<ThreadContext> threadlist = new List<ThreadContext>();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="cfg">データアクセス定義情報</param>
		/// <param name="logger">ログ出力モジュール</param>
		public ThreadManeger(DataAccessConfig cfg = null, AppLogger logger = null)
		{
			//if (cfg != null)
			//{
			//	this.daccfg = cfg;
			//}
			if (logger != null)
			{
				appLog = logger;
			}
			else
			{
				appLog = AppLogger.Instance;
			}
			//一つ前のスタックを取得
			StackFrame callerFrame = new StackFrame(1);
			if (string.IsNullOrWhiteSpace(Name))
			{
				Name = callerFrame.GetMethod().ReflectedType.Name;
			}
			this.appLog.Debug("<TM> ThreadManeger loaded. from {0}.{1}", Name, callerFrame.GetMethod().Name);
			this.OnReceived += new MessageReceiveHandler(OnMesasageRecived);
			// 電源状態の変化イベントを登録する
			Microsoft.Win32.SystemEvents.PowerModeChanged += new Microsoft.Win32.PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
		}

		/// <summary>
		/// 電源状態の変化イベント
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">イベント引数</param>
		private void SystemEvents_PowerModeChanged(object sender, Microsoft.Win32.PowerModeChangedEventArgs e)
		{
			if (e.Mode == Microsoft.Win32.PowerModes.Suspend)
			{
				powerdownflag = true;
				appLog.Info("<TM> [{0}] 電源がサスペンド状態になりました。", string.IsNullOrWhiteSpace(Name) ? "" : Name);
			}
			else if (e.Mode == Microsoft.Win32.PowerModes.Resume)
			{
				appLog.Info("<TM> [{0}] 電源がサスペンド状態から復帰しました。", Name);
			}
		}

		/// <summary>
		/// オブジェクトを破棄する。
		/// </summary>
		public void Dispose()
		{
			this.Stop();
			// 電源状態の変化イベントを削除する
			Microsoft.Win32.SystemEvents.PowerModeChanged -= new Microsoft.Win32.PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
		}

		/// <summary>
		/// メッセージ受信ハンドラ
		/// </summary>
		/// <param name="message">受信メッセージ</param>
		private void OnMesasageRecived(CommunicationObject message)
		{
			if (message.mType != MessageType.TimerLoop)
			{
				this.appLog.Debug("<TM> OnMesasageRecived({0})", message.mType);
			}
		}

		/// <summary>
		/// タイマースレッドを開始する。
		/// </summary>
		/// <param name="timer">タイマー周期時間（ミリ秒）</param>
		public void TimerLoopStart(int timer)
		{
			this.SendRequest(new CommunicationObject(MessageType.TimerLoop, "TimerLoop", timer));
		}

		/// <summary>
		/// タイマースレッドを停止する。
		/// </summary>
		public void TimerLoopStop()
		{
			foreach (ThreadContext thc in this.threadlist.Where(t => t.commdata.mType == MessageType.TimerLoop))
			{
				if (thc._thread.IsAlive)
				{
					thc.stopflag = true;
					thc._thread.Abort();
				}
			}
		}

		/// <summary>
		/// ワーカースレッドを全て停止する。
		/// </summary>
		public void Stop()
		{
			foreach (ThreadContext thc in this.threadlist)
			{
				if (thc._thread.IsAlive)
				{
					thc.stopflag = true;
					//thc._thread.Interrupt();
				}
				thc._thread.Join(300);
			}
			threadlist.Clear();
			this.appLog.Debug("<TM> ThreadManeger all stopped.");
		}

		/// <summary>
		/// メッセージ送信要求
		/// </summary>
		/// <param name="request">送信メッセージ</param>
		public void SendRequest(CommunicationObject request)
		{
			//if (this.daccfg == null)
			//{
			//	this.daccfg = Utility.LoadDataAccessConfig();
			//}
			ThreadContext thc = new ThreadContext();
			thc.commdata = request;
			thc.requestflag = true;
			this.appLog.Debug("<TM> SendRequest({0})", request.mType);
			switch (request.mType)
			{
			default:
				return;
			case MessageType.TimerLoop:
				thc._thread = new Thread(new ParameterizedThreadStart(TimerLoop));
				thc._thread.Start(thc);
				this.threadlist.Add(thc);
				break;
			case MessageType.RequestData:
			case MessageType.RequestDataWithBusy:
			case MessageType.RequestLicense:
			case MessageType.RequestDataNoManage:
			case MessageType.UpdateData:
			case MessageType.CallStoredProcedure:
				thc._thread = new Thread(new ParameterizedThreadStart(this.DataAccessThread));
				thc._thread.Start(thc);
				this.threadlist.Add(thc);
				break;
			}

		}

		/// <summary>
		/// ワーカースレッドのタイマーイベントループ
		/// </summary>
		/// <param name="param">タイマー周期時間（ミリ秒）</param>
		private void TimerLoop(object param)
		{
			ThreadContext thc = param as ThreadContext;
			object[] plist = thc.commdata.GetParameters();
			int sleeptime = 1000;
			if (plist[0] is int)
			{
				sleeptime = (int)plist[0];
			}
			if (plist.Length > 0)
			{
				if (plist[0] is int)
				{
					sleeptime = (int)plist[0];
				} 
			}
			thc.isRunnig = true;
			string msgname = thc.commdata.GetMessageName();
			while (true)
			{
				if (thc.stopflag)
				{
					break;
				}
				Thread.Sleep(sleeptime);
				this.OnReceived.Invoke(new CommunicationObject(MessageType.TimerLoop, msgname, null));

			}
			this.appLog.Debug("<TM> TimerLoop Breaked.");
			thc.isRunnig = false;
		}

	}
}
