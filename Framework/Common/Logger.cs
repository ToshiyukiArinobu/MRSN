using System;
using System.Text;
using System.Threading;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace KyoeiSystem.Framework.Common
{
	/// <summary>
	/// ログ出力ベースクラス
	/// </summary>
	public class Logger
	{
		#region メンバ変数

		/// <summary>排他制御用MUTEX</summary>
		private Mutex m_Mutex;

		/// <summary>ログファイル格納パス名</summary>
		protected string m_LogPath;
		/// <summary>ファイルプレフィクス</summary>
		protected string m_FilePrefix;

		/// <summary>ログファイルエンコード</summary>
		protected System.Text.Encoding m_Encoding = System.Text.Encoding.UTF8;

		#endregion

		#region 定数

		/// <summary>ログフォルダ名</summary>
		private const string LOG_DIR_NAME = "LOG";
		/// <summary>ログファイル拡張子</summary>
		private const string LOGFILE_EXTENSION = ".txt";
		/// <summary>gzip圧縮ファイル拡張子</summary>
		private const string GZIPFILE_EXTENSION = ".gz";

		#endregion

		#region プロパティ

		/// <summary>
		/// ログエンコーディング
		/// </summary>
		public Encoding Encode
		{
			set
			{
				m_Encoding = value;
			}
			get
			{
				return m_Encoding;
			}
		}

		#endregion

		#region コンストラクタ

		/// <summary>
		/// 新しいインスタンスを初期化します
		/// </summary>
		/// <param name="log_name">ログ識別名</param>
		public Logger(string log_name = null)
		{
			//ミューテックス初期化
			if (string.IsNullOrEmpty(log_name))
			{
				log_name = "Log";
			}
			m_Mutex = new Mutex(false, log_name);

			string exe_dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

			m_LogPath = exe_dir + "\\" + LOG_DIR_NAME;

			// ログパス取得
			if (Directory.Exists(m_LogPath) == false)
			{
				Directory.CreateDirectory(m_LogPath);
			}
			if (m_LogPath[m_LogPath.Length - 1] != '\\')
			{
				m_LogPath = m_LogPath + "\\";
			}
			// ファイルプレフィクス
			m_FilePrefix = log_name;

			m_Encoding = System.Text.Encoding.GetEncoding("shift-jis");
		}

		#endregion

		#region メソッド

		/// <summary>
		/// ログファイルへ文字列の書き込みを行います(スレッドID無し)
		/// </summary>
		/// <remarks>文字列の前に日付時刻を付加して、ログファイルへ書き込みます。
		/// <para>UDP送信用のUdpClientとIPEndPointが設定されている場合は、IPEndPointで指定される宛先に対して、UDP送信も行います。</para>
		/// </remarks>
		/// <param name="msg">書き込み文字列</param>
		protected void WriteLog(string msg)
		{
			try
			{
				DateTime now = DateTime.Now;

				m_Mutex.WaitOne();

				// ファイル名作成
				string file_name = string.Format("{0}{1}_{2:yyyyMMdd}{3}", m_LogPath, m_FilePrefix, now,LOGFILE_EXTENSION);

				
				// ログ書き込み
				string log_str = string.Format("{0:yyyy/MM/dd HH:mm:ss.fff}|{1}", now, msg);
				using (StreamWriter sw = new StreamWriter(file_name, true, m_Encoding))
				{
					sw.WriteLine(log_str);
				}
			}
			catch
			{

			}
			finally
			{
				m_Mutex.ReleaseMutex();
			}
		}

		/// <summary>
		/// ログファイルへ文字列の書き込みを行います
		/// </summary>
		/// <remarks>フォーマットと引数から生成された文字列をログファイルへ書き込みます。
		/// <para>UDP送信用のUdpClientとIPEndPointが設定されている場合は、IPEndPointで指定される宛先に対して、UDP送信も行います。</para>
		/// </remarks>
		/// <param name="msg">書き込み文字列</param>
		protected void WriteLogFormat(string fmt, params object[] args)
		{
			try
			{
				if (fmt != string.Empty)
				{
					WriteLog(string.Format(fmt, args));
				}
			}
			catch
			{
			}
		}
		/// <summary>
		/// ログファイルへ文字列の書き込みを行います
		/// </summary>
		/// <remarks>文字列の前に日付時刻とスレッドIDを付加して、ログファイルへ書き込みます。
		/// <para>UDP送信用のUdpClientとIPEndPointが設定されている場合は、IPEndPointで指定される宛先に対して、UDP送信も行います。</para>
		/// </remarks>
		/// <param name="sMsg">書き込み文字列</param>
		protected void Write(string msg)
		{
			try
			{
				if (msg != string.Empty)
				{
					WriteLog("[" + Thread.CurrentThread.ManagedThreadId.ToString("d3") + "]" + msg);
				}
			}
			catch
			{
			}
		}

		/// <summary>
		/// ログファイルへ文字列の書き込みを行います(例外付き)
		/// </summary>
		/// <remarks>文字列の前に日付時刻とスレッドIDを付加して、ログファイルへ書き込みます。
		/// <para>UDP送信用のUdpClientとIPEndPointが設定されている場合は、IPEndPointで指定される宛先に対して、UDP送信も行います。</para>
		/// </remarks>
		/// <param name="msg">書き込み文字列</param>
		protected void Write(string msg, Exception ex)
		{
			try
			{
				if (msg != string.Empty)
				{
					WriteLog("[" + Thread.CurrentThread.ManagedThreadId.ToString("d3") + "]" + msg + "\r\n" + ex.ToString());
				}
			}
			catch
			{
			}
		}

		/// <summary>
		/// ログファイルへ文字列の書き込みを行います
		/// </summary>
		/// <remarks>文字列の前に日付時刻とスレッドIDを付加して、ログファイルへ書き込みます。
		/// <para>UDP送信用のUdpClientとIPEndPointが設定されている場合は、IPEndPointで指定される宛先に対して、UDP送信も行います。</para>
		/// </remarks>
		/// <param name="msg">書き込み文字列</param>
		protected void WriteFormat(string fmt, params object[] args)
		{
			try
			{
				if (fmt != string.Empty)
				{
					WriteLog("[" + Thread.CurrentThread.ManagedThreadId.ToString("d3") + "]" + string.Format(fmt, args));
				}
			}
			catch
			{
			}
		}

		#endregion

		#region staticメソッド

		/// <summary>
		/// ログファイルの削除を行います
		/// </summary>
		/// <param name="sHome">ログディレクトリ</param>
		/// <param name="keep_days">保存日数</param>
		/// <remarks>呼び出した日から保存日数より前のログデータを削除します。</remarks>
		protected static void DeleteLog(string path, int keep_days)
		{
			try
			{
				//削除対象日付決定
				string del_date = string.Format("{0:yyyyMMdd}", DateTime.Today.AddDays(-keep_days));
				//アプリログ削除
				foreach (string fname in Directory.GetFiles(path))
				{
					string tmp = fname.Replace(LOGFILE_EXTENSION, string.Empty).Replace(GZIPFILE_EXTENSION , string.Empty);
					string log_date = tmp.Substring(tmp.Length - 8, 8);
					string ext = Path.GetExtension(fname);
					if ((ext.Equals(LOGFILE_EXTENSION) || ext.Equals(GZIPFILE_EXTENSION)) 
												&& string.Compare(log_date, del_date) < 0)
					{
						File.Delete(fname);
					}
				}
			}
			catch
			{
			}
		}

		/// <summary>
		/// ログファイルの圧縮を行います
		/// </summary>
		/// <param name="sAppName">アプリケーション名</param>
		/// <param name="sHome">ログディレクトリ</param>
		/// <param name="cmp_date">圧縮対象日付</param>
		/// <remarks>指定した日より過去のログデータを圧縮します。</remarks>
		protected static void CompressLog(string path, DateTime cmp_date)
		{
			try
			{
				byte[] buf = new byte[4096];
				int len;
				string date = cmp_date.AddDays(-1).ToString("yyyyMMdd");

				//アプリログ圧縮
				foreach (string fname in Directory.GetFiles(path))
				{
					string tmp = fname.Replace(LOGFILE_EXTENSION, string.Empty);
					string log_date = tmp.Substring(tmp.Length - 8, 8);
					string ext = Path.GetExtension(fname);
					if (ext.Equals(LOGFILE_EXTENSION) && string.Compare(log_date, date) <= 0)
					{
						// 圧縮
						string dst_fname = fname + GZIPFILE_EXTENSION;
						using (FileStream rstrm = new FileStream(fname, FileMode.Open, FileAccess.Read, FileShare.Read),
										   wstrm = new FileStream(dst_fname, FileMode.Create))
						using (GZipStream gzstrm = new GZipStream(wstrm, CompressionMode.Compress))
						{
							while ((len = rstrm.Read(buf, 0, buf.Length)) > 0)
								gzstrm.Write(buf, 0, len);
						}
						File.Delete(fname);
					}
				}
			}
			catch
			{
			}
		}

		#endregion

	}
}
