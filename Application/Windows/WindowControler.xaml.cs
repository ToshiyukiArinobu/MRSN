using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Data;

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;
using System.Reflection;
using KyoeiSystem.Application.Windows.Views;
using System.Collections.Specialized;
using System.Configuration;
using System.Threading;


namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
	/// 
	/// </summary>
	public partial class WindowControler : WindowMenuBase
	{
		#region 画面起動制御用

		delegate object ModuleActivate(string modname, Dictionary<string, object> plist);
		// 排他制御用
		object modctlex = new object();
		// 二重起動抑止用
		//KyoeiSystem.Application.Common.IpcMessageSender<string> ipcsvr = null;

		// 実行中の画面クラスリスト
		Dictionary<string, object> ModuleList = new Dictionary<string, object>();

		// 画面クラスのタイプリスト
		List<Type> viewtypes = new List<Type>();

		#endregion

		/// <summary>
		/// 
		/// </summary>
		public WindowControler()
			: base()
		{
			InitializeComponent();

			// 画面クラスのリスト作成
            Assembly oAssembly = typeof(MAINMANU).Assembly;
			foreach (var tp in oAssembly.GetTypes())
			{
				try
				{
					if (isWindow(tp))
					{
						viewtypes.Add(tp);
						continue;
					}
				}
				catch
				{
				}
			}
		}

		bool isWindow(Type tp)
		{
			if (tp == null)
			{
				return false;
			}
			if (tp == typeof(Window))
			{
				return true;
			}

			return isWindow(tp.BaseType);
		}

		/// <summary>
		/// 画面読み込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.getConfigForStartWait();

			AppCommon.GetAppCommonData(this)._CtlWindow = this;
            Task.Run(() => { Start(typeof(MAINMANU).Name); });
		}

		#region 画面起動制御用

		// 画面表示ボタン連打を効かないようにするための待ち時間
		//private const int WAITTIME_DEFAULT = 300;
		//private const int WAITTIME_DEFAULT_CLOSED = 100;
		//private int waitForStart = WAITTIME_DEFAULT;
		//private int waitForClosed = WAITTIME_DEFAULT_CLOSED;
		private void getConfigForStartWait()
		{
			//try
			//{
			//	var plist = (NameValueCollection)ConfigurationManager.GetSection("serviceSettings");
			//	var wtimecfg = plist["menuWaitTime"];
			//	if (string.IsNullOrWhiteSpace(wtimecfg) != true)
			//	{
			//		int waittime = WAITTIME_DEFAULT;
			//		if (int.TryParse(wtimecfg, out waittime))
			//		{
			//			this.waitForStart = waittime < 100 ? 100 : waittime;
			//		}
			//	}
			//	//wtimecfg = plist["menuWaitTimeClosed"];
			//	//if (string.IsNullOrWhiteSpace(wtimecfg) != true)
			//	//{
			//	//	int waittime = WAITTIME_DEFAULT_CLOSED;
			//	//	if (int.TryParse(wtimecfg, out waittime))
			//	//	{
			//	//		this.waitForClosed = waittime < 100 ? 100 : waittime;
			//	//	}
			//	//}
			//}
			//catch (Exception ex)
			//{
			//	appLog.Error("画面表示後待機時間取得エラー", ex);
			//	waitForStart = WAITTIME_DEFAULT;
			//}
			//finally
			//{
			//	appLog.Info("画面表示後待機時間:{0}msec", waitForStart);
			//}
		}


		/// <summary>
		/// 画面表示リクエストの処理
		/// </summary>
		/// <param name="modname"></param>
		public object Start(string modname, Dictionary<string, object> plist = null)
		{
			if (string.IsNullOrWhiteSpace(modname))
			{
				return null;
			}
			try
			{
				appLog.Info("Start:[{0}]", modname);
				//Thread.Sleep(waitForStart);

				object module = null;
				var dispatcher = System.Windows.Application.Current.Dispatcher;
				if (ModuleList.ContainsKey(modname))
				{
					var wnd = ModuleList[modname];
					if (wnd is IWindowViewBase)
					{
						if ((wnd as IWindowViewBase).IsClosing)
						{
							return null;
						}
					}
					// 既に起動済み（画面最小化含む）
					if (dispatcher.CheckAccess())
						module = this.Activate(modname, plist);
					else
						module = dispatcher.Invoke(new ModuleActivate(OnActivate), modname, plist);
				}
				else
				{
					// 未実行または閉じられている
					if (dispatcher.CheckAccess())
						module = this.LoadModule(modname, plist);
					else
						module = dispatcher.Invoke(new ModuleActivate(OnLoadModule), modname, plist);
				}
				return module;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		/// <summary>
		/// 画面モジュール実行イベント
		/// </summary>
		/// <param name="modname"></param>
		private object OnLoadModule(string modname, Dictionary<string, object> plist = null)
		{
			return this.LoadModule(modname, plist);
		}

		/// <summary>
		/// 未実行画面モジュールの実行
		/// </summary>
		/// <param name="mname"></param>
		public object LoadModule(string mname, Dictionary<string, object> plist = null)
		{
			appLog.Info("<IPC> 実行要求モジュール:{0}", mname);
			try
			{
				lock (modctlex)
				{

					string machine = Environment.MachineName;
					Type tp = (from x in viewtypes where x.Name == mname select x).FirstOrDefault();
					if (tp == null)
					{
						return null;
					}
                    var menu = (from m in ModuleList where m.Key == typeof(MAINMANU).Name select m.Value).FirstOrDefault() as MAINMANU;

					object mod = System.Activator.CreateInstance(tp);
					if (plist != null)
					{
						try
						{
							foreach (var prm in plist)
							{
								FieldInfo fld = mod.GetType().GetField(prm.Key, BindingFlags.Public | BindingFlags.Instance);
								if (fld == null)
								{
									PropertyInfo prp = mod.GetType().GetProperty(prm.Key, BindingFlags.Public | BindingFlags.Instance);
									if (prp == null)
									{
										appLog.Error("<IPC> メンバーへの値代入失敗[{0}]なし", prm.Key);
										continue;
									}
									else
									{
										prp.SetValue(mod, prm.Value);
									}
								}
								else
								{
									fld.SetValue(mod, prm.Value);
								}
							}
						}
						catch (Exception ex)
						{
							appLog.Error("<IPC> メンバーへの値代入失敗[{0}]", ex.Message);
						}
					}
					if (mod is WindowViewBase)
					{
						if (menu != null)
						{
							menu.IsEnabled = false;
						}
						(mod as WindowViewBase).OnFinishWindowRendered += OnChildFinishWindowRendered;
						(mod as WindowViewBase).Closed += this.OnChildClosed;
						(mod as WindowViewBase).OnFinishWindowClosed += OnChildFinishWindowClosed;
						(mod as WindowViewBase).Topmost = true;
						(mod as WindowViewBase).Show(this);
						(mod as WindowViewBase).Topmost = false;
						(mod as WindowViewBase).Focus();
					}
					else if (mod is RibbonWindowViewBase)
					{
						if (menu != null)
						{
							menu.IsEnabled = false;
						}

						(mod as RibbonWindowViewBase).OnFinishWindowRendered += OnChildFinishWindowRendered;
						(mod as RibbonWindowViewBase).Closed += this.OnChildClosed;
						(mod as RibbonWindowViewBase).OnFinishWindowClosed += OnChildFinishWindowClosed;
						(mod as RibbonWindowViewBase).Topmost = true;
						(mod as RibbonWindowViewBase).Show(this);
						(mod as RibbonWindowViewBase).Topmost = false;
						(mod as RibbonWindowViewBase).Focus();
					}
					else if (mod is Window)
					{
						if (menu != null)
						{
							menu.IsEnabled = false;
						}

						(mod as Window).Closed += this.OnChildClosed;
						(mod as Window).Topmost = true;
						(mod as Window).Show();
						(mod as Window).Topmost = false;
						(mod as Window).Focus();
					}
					else
					{
						return mod;
					}
					var child = FindFocusedChild(mod as Window);
					if (child != null)
					{
						Keyboard.Focus(child);
					}
					ModuleList.Add(mname, mod);
					return mod;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		//void OnChildLoaded(object sender, RoutedEventArgs e)
		//{
		//	appLog.Debug("<IPC> OnChildLoaded [{0}]", sender.GetType().Name);
		//	var menu = (from m in ModuleList where m.Key == typeof(WindowTest4).Name select m.Value).FirstOrDefault() as WindowTest4;
		//	if (menu != null)
		//	{
		//		menu.IsEnabled = true;
		//	}
		//}

		void OnChildFinishWindowRendered(object sender)
		{
			appLog.Debug("<IPC> OnChildFinishWindowRendered [{0}]", sender.GetType().Name);
			if (sender is IWindowViewBase)
			{
				(sender as IWindowViewBase).OnFinishWindowRendered -= OnChildFinishWindowRendered;
			}
            var menu = (from m in ModuleList where m.Key == typeof(MAINMANU).Name select m.Value).FirstOrDefault() as MAINMANU;
			if (menu != null)
			{
				menu.IsEnabled = true;
			}
		}

		void OnChildFinishWindowClosed(object sender)
		{
			Type stype = sender.GetType();
			appLog.Info("<IPC> モジュール {0} OnChildFinishWindowClosed.", stype.Name);
			lock (modctlex)
			{
				if (sender is WindowViewBase)
				{
					(sender as WindowViewBase).OnFinishWindowClosed -= this.OnChildFinishWindowClosed;
				}
				else if (sender is RibbonWindowViewBase)
				{
					(sender as RibbonWindowViewBase).OnFinishWindowClosed -= this.OnChildFinishWindowClosed;
				}

				// 実行中リストから削除
				if (ModuleList.ContainsKey(stype.Name))
				{
					ModuleList[stype.Name] = null;
					ModuleList.Remove(stype.Name);


				}
				if (ModuleList.Count() == 0)
				{
					// 全ての画面が閉じられたら自身も終了
					this.Close();
				}
				else
				{
					// 閉じられた画面がメニューの場合、残っている画面を全て強制終了させる
                    if (stype == typeof(MAINMANU))
					{
						var wlist = ModuleList.Values.ToArray();
						foreach (Window wnd in wlist)
						{
							wnd.Close();
						}
					}
				}
			}
		}

		/// <summary>
		/// 実行した画面のCloseイベントで実行中モジュールリストから削除する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnChildClosed(object sender, EventArgs e)
		{
			Type stype = sender.GetType();
			appLog.Info("<IPC> モジュール {0} Closed.", stype.Name);
			lock (modctlex)
			{
				if (sender is WindowViewBase)
				{
					(sender as WindowViewBase).Closed -= this.OnChildClosed;
				}
				else if (sender is RibbonWindowViewBase)
				{
					(sender as RibbonWindowViewBase).Closed -= this.OnChildClosed;
				}
				else if (sender is Window)
				{
					(sender as Window).Closed -= this.OnChildClosed;
				}

				// 実行中リストから削除
				if (ModuleList.ContainsKey(stype.Name))
				{
					ModuleList[stype.Name] = null;
					ModuleList.Remove(stype.Name);

				}
				if (ModuleList.Count() == 0)
				{
					// 全ての画面が閉じられたら自身も終了
					this.Close();
				}
				else
				{
					// 閉じられた画面がメニューの場合、残っている画面を全て強制終了させる
                    if (stype == typeof(MAINMANU))
					{
						var wlist = ModuleList.Values.ToArray();
						foreach (Window wnd in wlist)
						{
							wnd.Close();
						}
					}
				}
			}
		}

		void Dispatcher_ShutdownFinished(object sender, EventArgs e)
		{
			Type stype = sender.GetType();
			appLog.Info("<IPC> モジュール {0} ShutdownFinished.", stype.Name);
			if (sender is WindowViewBase)
			{
				(sender as WindowViewBase).Dispatcher.ShutdownFinished -= Dispatcher_ShutdownFinished;
			}
			else if (sender is RibbonWindowViewBase)
			{
				(sender as RibbonWindowViewBase).Dispatcher.ShutdownFinished -= Dispatcher_ShutdownFinished;
			}
			else if (sender is Window)
			{
				(sender as Window).Dispatcher.ShutdownFinished -= Dispatcher_ShutdownFinished;
			}
		}

		/// <summary>
		/// 実行中画面再表示イベント
		/// </summary>
		/// <param name="modname"></param>
		private object OnActivate(string modname, Dictionary<string, object> plist = null)
		{
			return this.Activate(modname, plist);
		}

		/// <summary>
		/// 実行中モジュールリストにある場合は再表示する
		/// </summary>
		/// <param name="modname"></param>
		public object Activate(string modname, Dictionary<string, object> plist = null)
		{
			appLog.Info("<IPC> 再表示要求モジュール:{0}", modname);
			try
			{
				object mod = ModuleList[modname];
				if (mod is Window)
				{
					if ((mod as Window).WindowState == System.Windows.WindowState.Minimized)
						(mod as Window).WindowState = System.Windows.WindowState.Normal;
					(mod as Window).Topmost = true;
					(mod as Window).Activate();
					(mod as Window).Topmost = false;
					(mod as Window).Focus();
					var child = FindFocusedChild(mod as RibbonWindowViewBase);
					if (child != null)
					{
						Keyboard.Focus(child);
					}
				}
				return mod;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		UIElement FindFocusedChild(DependencyObject parent)
		{
			List<DependencyObject> childObjects = new List<DependencyObject>();
			for (int ix = 0; ix < VisualTreeHelper.GetChildrenCount(parent); ix++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(parent, ix);
				if (child == null)
				{
					continue;
				}
				if (child is UIElement)
				{
					if (child is UIElement)
					{
						if ((child as UIElement).IsFocused == true)
						{
							return child as UIElement;
						}
					}
				}
				childObjects.Add(child);
			}
			foreach (DependencyObject childObj in childObjects)
			{
				DependencyObject child = FindFocusedChild(childObj);
				if (child is UIElement)
				{
					return child as UIElement;
				}
			}
			return null;
		}

		public object GetModule(string modname)
		{
			if (ModuleList.ContainsKey(modname))
			{
				object mod = ModuleList[modname];
				return mod;
			}
			else
			{
				return null;
			}
		}

		#endregion

	}
}
