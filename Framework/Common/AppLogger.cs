using System;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using log4net;

namespace KyoeiSystem.Framework.Common
{
	/// <summary>
	/// アプリケーションログクラス
	/// <para>
	/// ログの出力を取りまとめる機能として利用できます。詳細はlog4net(NuGetから取得したパッケージ)の機能を参照してください。
	/// </para>
	/// </summary>
	public class AppLogger
	{
		//NLog.Logger logger = LogManager.GetCurrentClassLogger();
		ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		#region インスタンスのシングルトン化
		private static AppLogger mInstance;

		private AppLogger()
		{
		}

		public static AppLogger Instance
		{
			get
			{
				if (mInstance == null) mInstance = new AppLogger();
				return mInstance;
			}
		}
		#endregion
		///// <summary>
		///// コンストラクタ
		///// </summary>
		//public AppLogger()
		//{
		//}

		//~AppLogger()
		//{
		//}

		#region 各レベル毎のログ出力メソッド
		/// <summary>
		/// ログ出力（FATAL）
		/// </summary>
		/// <param name="msg">メッセージ</param>
		public void Fatal(string msg)
		{
			logger.Fatal(msg);
		}

		/// <summary>
		/// ログ出力（FATAL）
		/// </summary>
		/// <param name="msg">メッセージ</param>
		/// <param name="ex">継承例外</param>
		public void Fatal(string msg, Exception ex)
		{
			logger.Fatal(msg, ex);
		}

		/// <summary>
		/// ログ出力（FATAL）
		/// </summary>
		/// <param name="fmt">編集フォーマット</param>
		/// <param name="args">編集パラメータ</param>
		public void Fatal(string fmt, params object[] args)
		{
			logger.Fatal(string.Format(fmt, args));
		}

		/// <summary>
		/// ログ出力（ERROR）
		/// </summary>
		/// <param name="msg">メッセージ</param>
		public void Error(string msg)
		{
			logger.Error(msg);
		}

		/// <summary>
		/// ログ出力（ERROR）
		/// </summary>
		/// <param name="msg">メッセージ</param>
		/// <param name="ex">継承例外</param>
		public void Error(string msg, Exception ex)
		{
			logger.Error(msg, ex);
		}

		/// <summary>
		/// ログ出力（ERROR）
		/// </summary>
		/// <param name="fmt">編集フォーマット</param>
		/// <param name="args">編集パラメータ</param>
		public void Error(string fmt, params object[] args)
		{
			logger.Error(string.Format(fmt, args));
		}

		/// <summary>
		/// ログ出力（WARN）
		/// </summary>
		/// <param name="msg">メッセージ</param>
		public void Warn(string msg)
		{
			logger.Warn(msg);
		}

		/// <summary>
		/// ログ出力（WARN）
		/// </summary>
		/// <param name="msg">メッセージ</param>
		/// <param name="ex">継承例外</param>
		public void Warn(string msg, Exception ex)
		{
			logger.Warn(msg, ex);
		}

		/// <summary>
		/// ログ出力（WARN）
		/// </summary>
		/// <param name="fmt">編集フォーマット</param>
		/// <param name="args">編集パラメータ</param>
		public void Warn(string fmt, params object[] args)
		{
			logger.Warn(string.Format(fmt, args));
		}

		/// <summary>
		/// ログ出力（INFO）
		/// </summary>
		/// <param name="msg">メッセージ</param>
		public void Info(string msg)
		{
			logger.Info(msg);
		}

		/// <summary>
		/// ログ出力（INFO）
		/// </summary>
		/// <param name="msg">メッセージ</param>
		/// <param name="ex">継承例外</param>
		public void Info(string msg, Exception ex)
		{
			logger.Info(msg, ex);
		}

		/// <summary>
		/// ログ出力（INFO）
		/// </summary>
		/// <param name="fmt">編集フォーマット</param>
		/// <param name="args">編集パラメータ</param>
		public void Info(string fmt, params object[] args)
		{
			logger.Info(string.Format(fmt, args));
		}

		/// <summary>
		/// ログ出力（DEBUG）
		/// </summary>
		/// <param name="msg">メッセージ</param>
		public void Debug(string msg)
		{
			logger.Debug(msg);
		}

		/// <summary>
		/// ログ出力（DEBUG）
		/// </summary>
		/// <param name="msg">メッセージ</param>
		/// <param name="ex">継承例外</param>
		public void Debug(string msg, Exception ex)
		{
			logger.Debug(msg, ex);
		}

		/// <summary>
		/// ログ出力（DEBUG）
		/// </summary>
		/// <param name="fmt">編集フォーマット</param>
		/// <param name="args">編集パラメータ</param>
		public void Debug(string fmt, params object[] args)
		{
			logger.Debug(string.Format(fmt, args));
		}
		#endregion

		#region ログファイルパスの取得
		/// <summary>
		/// ログファイルパス取得
		/// </summary>
		/// <returns>ログファイルパス</returns>
		public string LogFilePath()
		{
			string path = string.Empty;

			foreach (var appender in logger.Logger.Repository.GetAppenders())
			{
				if (appender is log4net.Appender.FileAppender)
				{
					return (appender as log4net.Appender.FileAppender).File;
				}
			}
			return path;
		}
		#endregion

		#region 保存期限を過ぎたログファイルの削除
		/// <summary>
		/// 保存期限を過ぎたログファイルを削除します。
		/// <para>Appenderは、日付毎にファイル名を切り替えるモードであることが前提であり、当該フォルダにあるファイル全てを対象として削除します。
		/// 保存期限は、App.config等に記述したlog4netの設定でAppenderのMaxSizeRollBackupsの値によります。</para>
		/// </summary>
		public void CleanUp()
		{
			try
			{
				int keep_days = 1;

				var repo = this.logger.Logger.Repository;
				var app = repo.GetAppenders().Where(x => x.GetType() == typeof(log4net.Appender.RollingFileAppender)).FirstOrDefault();
				if (app != null)
				{
					var appender = app as log4net.Appender.RollingFileAppender;
					if (appender.RollingStyle != log4net.Appender.RollingFileAppender.RollingMode.Date)
					{
						// ログファイル切替モードが「日付」でなければ何もしない
						return;
					}
					string file = appender.File;
					string directory = Path.GetDirectoryName(file);
					if (string.IsNullOrEmpty(directory))
					{
						return;
					}

					var dirInfo = new DirectoryInfo(directory);
					if (!dirInfo.Exists)
					{
						return;
					}

					// MaxSizeRollBackups を最大ログ日数として代用する。
					keep_days = appender.MaxSizeRollBackups;
					if (keep_days <= 0)
					{
						return;
					}
					string limit = DateTime.Now.AddDays(-keep_days).ToString("yyyyMMdd");
					foreach (var info in dirInfo.GetFiles("*"))
					{
						string ymd = info.LastWriteTime.ToString("yyyyMMdd");
						if (string.Compare(ymd, limit) < 0)
						{
							Info("古いログを削除 ({0})", info.Name);
							info.Delete();
						}
					}
				}

			}
			catch (Exception ex)
			{
				this.Error("ログファイルアーカイブの削除でエラーが発生しました。", ex);
			}
		}

		#endregion

	}
}
