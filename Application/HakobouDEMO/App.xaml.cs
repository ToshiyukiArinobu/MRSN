using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

using KyoeiSystem.Application.Windows.Views;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Specialized;
using KyoeiSystem.Framework.Common;
using System.Data.SqlClient;
using System.Data.EntityClient;
using KyoeiSystem.Framework.Windows.ViewBase;


namespace Hakobo
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		// ログ出力用のインスタンス
		AppLogger applog = null;

		void StartupMain(object sender, StartupEventArgs e)
		{
			try
			{
				AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
				this.DispatcherUnhandledException += App_DispatcherUnhandledException;

				//自分自身のAssemblyを取得
				System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
				System.Version ver = asm.GetName().Version; ;

				this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

				bool IsNeedLicenseCheck = true;
				var plist = (NameValueCollection)ConfigurationManager.GetSection("serviceSettings");
				if (plist != null)
				{
					if (plist["mode"] == CommonConst.WithoutLicenceDBMode)
					{
						IsNeedLicenseCheck = false;
					}
				}
				while (true)
				{

					AppCommonData appcmn = null;
					if (IsNeedLicenseCheck)
					{
						// ログインできたら今までと同様スタートアップ画面を表示する。
						LicenseLogin licLogin = new LicenseLogin();
						this.applog = licLogin.appLog;
						licLogin.viewsCommData.WithLicenseDB = true;
						licLogin.viewsCommData.AppData = new AppCommonData();
						licLogin.ShowDialog();
						if (licLogin.IsLoginCompleted)
						{
							appcmn = new AppCommonData()
							{
								CommonDB_CustomerCd = (licLogin.viewsCommData.AppData as AppCommonData).CommonDB_CustomerCd,
								UserDB_DBId = (licLogin.viewsCommData.AppData as AppCommonData).UserDB_DBId,
								UserDB_DBName = (licLogin.viewsCommData.AppData as AppCommonData).UserDB_DBName,
								UserDB_DBPass = (licLogin.viewsCommData.AppData as AppCommonData).UserDB_DBPass,
								UserDB_DBServer = (licLogin.viewsCommData.AppData as AppCommonData).UserDB_DBServer,
								CommonDB_LastAccessDateTime = (licLogin.viewsCommData.AppData as AppCommonData).CommonDB_LastAccessDateTime,
								CommonDB_LimitDate = (licLogin.viewsCommData.AppData as AppCommonData).CommonDB_LimitDate,
								CommonDB_LoginFlg = (licLogin.viewsCommData.AppData as AppCommonData).CommonDB_LoginFlg,
								CommonDB_RegDate = (licLogin.viewsCommData.AppData as AppCommonData).CommonDB_RegDate,
								CommonDB_StartDate = (licLogin.viewsCommData.AppData as AppCommonData).CommonDB_StartDate,
								CommonDB_UserId = (licLogin.viewsCommData.AppData as AppCommonData).CommonDB_UserId,
							};
						}
						else
						{
							Application.Current.Shutdown(0);
						}
					}
					else
					{
						SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
						sqlBuilder = AppCommon.MakeSqlConnectString();
						if (sqlBuilder == null)
						{
							// 接続文字列が設定されていない場合
							DBSetup dbform = new DBSetup();
							if (dbform.ShowDialog() == true)
							{
								sqlBuilder = dbform.sqlBuilder;
							}
							else
							{
								Environment.Exit(0);
								return;
							}
						}
						appcmn = new AppCommonData()
						{
							UserDB_DBServer = Utility.Encrypt(sqlBuilder.DataSource),
							UserDB_DBName = Utility.Encrypt(sqlBuilder.InitialCatalog),
							UserDB_DBId = Utility.Encrypt(sqlBuilder.UserID),
							UserDB_DBPass = Utility.Encrypt(sqlBuilder.Password),
						};
					}
					{
						LOGIN login = new LOGIN();
						this.applog = login.appLog;
						login.viewsCommData.WithLicenseDB = IsNeedLicenseCheck;
						login.viewsCommData.AppData = appcmn;
						login.SetupConnectStringuserDB(appcmn.UserDB_DBServer, appcmn.UserDB_DBName, appcmn.UserDB_DBId, appcmn.UserDB_DBPass);
						login.appLog.Info("START Ver.{0}", ver);

						StartupWindow start = new StartupWindow();
						start.viewsCommData.WithLicenseDB = IsNeedLicenseCheck;
						start.viewsCommData = login.viewsCommData;
						start.ConnString = login.ConnString;
						// スタートアップ画面起動
						start.ShowDialog();
						login.viewsCommData = start.viewsCommData;

						if (start.IsConnected)
						{
							// ログイン画面起動
							login.ShowDialog();
							if (login.IsLoggedIn)
							{
								WindowControler menu = new WindowControler();
								this.applog = menu.appLog;
								menu.Closed += menu_Closed;
								menu.viewsCommData.WithLicenseDB = IsNeedLicenseCheck;
								//menu.Topmost = true;
								menu.Show(login);
								//menu.Topmost = false;
							}
							else
							{
								if (login.IsReload)
								{
									continue;
								}
							}
						}
						else
						{
							login.Close();
							if (start.IsReload)
							{
								continue;
							}
						}

					}
					break;
				}
			}
			catch (Exception ex)
			{
				if (this.applog != null)
					applog.Error("画面で取得できなかった例外発生", ex);
				MessageBox.Show(string.Format("例外が発生しました。\r\nエラーメッセージ：{0}", ex.Message), "例外発生");
				//Environment.Exit(1);
			}
		}

		void menu_Closed(object sender, EventArgs e)
		{
            //Environment.Exit(0);
            //▼終了せずに再度スタートを行う
            StartupMain(null, null);
		}

		/// <summary>
		/// 処理継続可能な例外をキャッチする
		/// （本来は各画面側でキャッチすべき例外が拾いきれていないケースを救済する）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			string errorMember = e.Exception.TargetSite.Name;
			string errorMessage = e.Exception.Message;
			string message = string.Format("例外が発生しました。\r\nエラーメッセージ：{0}", errorMessage);
			if (this.applog != null)
				applog.Error("画面で取得できなかった例外発生(App_DispatcherUnhandledException)", e.Exception);
			MessageBox.Show(message, "例外発生");
			e.Handled = true;
		}

		/// <summary>
		/// 処理継続不可能な例外をキャッチする
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			var exception = e.ExceptionObject as Exception;
			if (exception == null)
			{
				MessageBox.Show("システム例外が発生しました。");
				Environment.Exit(1);
				return;
			}

			string errorMember = exception.TargetSite.Name;
			string errorMessage = exception.Message;
			string message = string.Format("例外が{0}で発生しました。プログラムは終了します。\r\nエラーメッセージ：{1}", errorMember, errorMessage);
			MessageBox.Show(message, "UnhandledException", MessageBoxButton.OK, MessageBoxImage.Stop);
		}

	}
}
