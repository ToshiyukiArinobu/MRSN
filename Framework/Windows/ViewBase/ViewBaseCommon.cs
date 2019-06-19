using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Input;
using System.Windows.Threading;
using System.Reflection;
using System.Windows.Media;

using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.Controls;
using System.Data;
using System.Diagnostics;
using System.Deployment.Application;
using System.Windows.Data;
using System.Windows.Markup;
using System.Data.SqlClient;
using System.Data.EntityClient;

namespace KyoeiSystem.Framework.Windows.ViewBase
{
	/// <summary>
	/// メッセージ受信通知用デリゲート
	/// </summary>
	/// <param name="data">受信データ</param>
	delegate void ReceivedDelegate(CommunicationObject data);

	/// <summary>
	/// 画面規定クラス用共通機能
	/// </summary>
	public static class ViewBaseCommon
	{

		/// <summary>
		/// 画面クラスが取得済みのConfigの内容を配下の子コントロールに引き継ぐ
		/// </summary>
		/// <param name="target">Configを設定済みの親コントロール（主に画面クラス）</param>
		/// <param name="cfg">引継ぎ元のConfigのインスタンス</param>
		public static void SetConfigToControls(DependencyObject target, ViewsCommon cfg)
		{
			foreach (var child in LogicalTreeHelper.GetChildren(target))
			{
				if (child is DependencyObject)
				{
					if (child is FrameworkControl)
					{
						(child as FrameworkControl).SetConfig(cfg);
					}
					SetConfigToControls((DependencyObject)child, cfg);
				}
			}
		}

		/// <summary>
		/// フォーカスを先頭のコントロールに移動する
		/// </summary>
		/// <param name="target">Windowインスタンス</param>
		/// <returns>true:移動した、false:移動していない</returns>
		public static bool SetFocusToTopControl(DependencyObject target)
		{
			foreach (var child in LogicalTreeHelper.GetChildren(target))
			{
				if (child is DependencyObject)
				{
					if (child is Control)
					{
						//Debug.WriteLine("SetFocusToTopControl : {0} ({1})", child, (child as Control).Visibility);
						if ((child as Control).IsEnabled != true || (child as Control).Visibility != Visibility.Visible)
						{
							continue;
						}
					}
					if (child.GetType() == typeof(TextBox))
					{
                        if (((child as TextBox).IsEnabled && !(child as TextBox).IsReadOnly) && (child as TextBox).Focusable && (child as TextBox).Visibility == Visibility.Visible)
						{
							Keyboard.Focus(child as TextBox);
							//(child as TextBox).Focus();
							return true;
						}
					}
					else if (child.GetType() == typeof(PasswordBox))
					{
                        if ((child as PasswordBox).IsEnabled && (child as PasswordBox).Focusable)
						{
							Keyboard.Focus(child as PasswordBox);
							return true;
						}
					}
					else if (child.GetType() == typeof(DatePicker))
					{
						if ((child as DatePicker).IsEnabled && (child as DatePicker).Focusable)
						{
							Keyboard.Focus(child as DatePicker);
							return true;
						}
					}
					else if (child.GetType() == typeof(ComboBox))
					{
                        if (((child as ComboBox).IsEnabled && !(child as ComboBox).IsReadOnly) && (child as ComboBox).Focusable)
						{
							Keyboard.Focus(child as ComboBox);
							return true;
						}
					}
					if (SetFocusToTopControl((DependencyObject)child))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// 親コントロールが非表示の場合、子コントロールの状態を非表示にする
		/// </summary>
		/// <param name="target">親コントロール</param>
		/// <param name="flg">親コントロールのVisibility</param>
		public static void ChangePanelVisibility(DependencyObject target,Visibility flg)
		{
			if (target.GetType().Name.StartsWith("Ribbon"))
			{
				return;
			}
			foreach (var child in LogicalTreeHelper.GetChildren(target))
			{
				if (child is DependencyObject)
				{
					if (child is Panel)
					{
						if ((child as Panel).Visibility == Visibility.Visible)
						{
							flg = Visibility.Visible;
						}
						else
						{
							flg = Visibility.Collapsed;
						}

						ChangePanelVisibility((DependencyObject)child, flg);
					}
					else if(child is Control)
					{
						if ((child as Control).Visibility == Visibility.Visible)
						{
							(child as Control).Visibility = flg;
						}
					}
				}
			}
		}

		/// <summary>
		/// キー項目としてマークされた項目の入力可否を切り替える
		/// </summary>
		/// <param name="flag">true:入力可、false:入力不可</param>
		/// <param name="target">Windowインスタンス</param>
		public static void ChangeKeyItemChangeable(bool flag, DependencyObject target)
		{
			if (target.GetType().Name.StartsWith("Ribbon"))
			{
				return;
			}
			foreach (var child in LogicalTreeHelper.GetChildren(target))
			{
				if (child is DependencyObject)
				{
					if (child is UcLabelTextRadioButton)
					{
						foreach (RadioButton btn in (child as UcLabelTextRadioButton).GetRadioButtonList())
						{
							if (btn.Visibility != Visibility.Visible)
							{
								continue;
							}
							if ((child as UcLabelTextRadioButton).IsKeyItem)
							{
								btn.IsEnabled = flag;
							}
							else
							{
								btn.IsEnabled = !flag;
							}
						}
					}
					else if (child is FrameworkControl)
					{
						if ((child as FrameworkControl).IsTagetForEnabler == true)
						{
							if ((child as FrameworkControl).IsKeyItem == true)
							{
								(child as FrameworkControl).IsEnabled = flag;
							}
							else
							{
								(child as FrameworkControl).IsEnabled = !flag;
							}
						}
					}
					else if (child is Button)
					{
						//(child as Button).IsEnabled = !flag;
						(child as Button).IsEnabled = !flag;
					}
					ChangeKeyItemChangeable(flag, (DependencyObject)child);
				}
			}
		}

		/// <summary>
		/// キー項目としてマークされたコントロールの値チェック結果を取得する
		/// </summary>
		/// <param name="target">親コントロール（主に画面）</param>
		/// <param name="checkOnly">チェックのみを行う(true)か、エラー時にフォーカスを移動する(false)かどうかを指定するフラグ</param>
		/// <returns></returns>
		public static bool CheckKeyItemValidation(DependencyObject target, bool checkOnly = false)
		{
			bool result = true;
			foreach (var child in LogicalTreeHelper.GetChildren(target))
			{
				if (child is DependencyObject)
				{
					if (child is FrameworkControl)
					{
						if ((child as FrameworkControl).IsKeyItem)
						{
							bool chk = true;
							MethodInfo mi = child.GetType().GetMethod("CheckValidation");
							if (mi != null)
							{
								chk = (bool)mi.Invoke(child, new object[] { });
							}
							if (chk == false)
							{
								if (!checkOnly)
								{
									SetFocusToTopControl(child as DependencyObject);
								}
								return false;
							}
						}
					}
					else
					{
						result = CheckKeyItemValidation((DependencyObject)child, checkOnly);
						if (result != true)
						{
							return false;
						}
					}
				}
			}
			return result;
		}

		/// <summary>
		/// 指定されたコントロールの値チェック結果を取得する
		/// </summary>
		/// <param name="target">値チェック対象のコントロール</param>
		/// <param name="setfocustop">チェック後にフォーカスを画面先頭フィールドに移動するかどうかの指定</param>
		/// <returns>チェック結果：true=OK、false=NG</returns>
		public static string CheckAllValidation(DependencyObject target, bool setfocustop = true)
		{
			string result = string.Empty;
			foreach (var child in LogicalTreeHelper.GetChildren(target))
			{
				if (child is DependencyObject)
				{
					if (child is FrameworkControl)
					{
						if ((child as FrameworkControl).IsKeyItem != true)
						{
							bool chk = true;
							chk = (child as FrameworkControl).CheckValidation();
							if (chk == false)
							{
								result = (child as FrameworkControl).GetValidationMessage();
								if (setfocustop)
								{
									SetFocusToTopControl(child as DependencyObject);
								}
								return result;
							}
						}
					}
					else
					{
						result = CheckAllValidation((DependencyObject)child, setfocustop);
						if (string.IsNullOrWhiteSpace(result) != true)
						{
							return result;
						}
					}
				}
			}
			return result;
		}

		/// <summary>
		/// 指定されたコントロールに所属するコントロールの値チェック結果をリセットする
		/// </summary>
		/// <param name="target">値チェック結果のリセット対象コントロール</param>
		public static void ResetAllValidation(DependencyObject target)
		{
			foreach (var child in LogicalTreeHelper.GetChildren(target))
			{
				if (child is DependencyObject)
				{
					if (child is UcTextBox)
					{
						(child as UcTextBox).ResetValidation();
					}
					else
					{
						ResetAllValidation((DependencyObject)child);
					}
				}
			}
			return;
		}

		/// <summary>
		/// Windowを入力不可にする（マウスカーソルを砂時計にする）
		/// </summary>
		/// <param name="wnd">制御対象のWindow</param>
		public static void SetBusyForInput(Window wnd)
		{
			wnd.IsEnabled = false;
			Mouse.OverrideCursor = Cursors.Wait;
		}

		/// <summary>
		/// Windowを入力可能にする（マウスカーソルを戻す）
		/// </summary>
		/// <param name="wnd">制御対象のWindow</param>
		public static void SetFreeForInput(Window wnd)
		{
			wnd.IsEnabled = true;
			Mouse.OverrideCursor = null;
		}

		/// <summary>
		/// データ処理要求の結果受信イベントの処理
		/// </summary>
		/// <param name="message">データ処理要求に対する結果オブジェクト</param>
		/// <param name="disp">呼び出し元スレッドのDispatcher</param>
		/// <param name="functionList">データ処理実行中スレッドのコレクション</param>
		public static void OnReceived(CommunicationObject message, Dispatcher disp, Dictionary<MessageType, Action<CommunicationObject>> functionList)
		{
			if (functionList.ContainsKey(message.mType))
			{
				Action<CommunicationObject> act = functionList[message.mType];
				disp.Invoke(new ReceivedDelegate(act), message);
			}
		}

		/// <summary>
		/// データ処理要求のエラー結果受信イベントの処理（エラーメッセージの取得）
		/// </summary>
		/// <param name="message">データ処理要求に対する結果オブジェクト</param>
		/// <param name="viewcomm">画面共有オブジェクト（エラーメッセージ取得用）</param>
		/// <returns>エラーメッセージ</returns>
		public static string OnReveivedError(CommunicationObject message, ViewsCommon viewcomm)
		{
			string errmsg = viewcomm.GetMessage("F", 999);
			if (message.GetResultData().GetType() == typeof(string))
			{
				switch (message.ErrorType)
				{
				case MessageErrorType.DataError:
					errmsg = viewcomm.GetMessage("F", 999);
					break;
				case MessageErrorType.DBConnectError:
					errmsg = viewcomm.GetMessage("F", 1);
					break;
				case MessageErrorType.DataNotFound:
					errmsg = viewcomm.GetMessage("E", 1);
					break;
				case MessageErrorType.DBGetError:
					errmsg = viewcomm.GetMessage("F", 999);
					break;
				case MessageErrorType.UpdateConflict:
					errmsg = viewcomm.GetMessage("W", 1);
					break;
				case MessageErrorType.DBUpdateError:
					errmsg = viewcomm.GetMessage("F", 2);
					break;
				default:
					errmsg = viewcomm.GetMessage("F", 999);
					break;
				}
			}
			return errmsg;
		}

		/// <summary>
		/// Fキーに対応する処理を呼び出す。
		/// </summary>
		/// <param name="wnd">Windowオブジェクト</param>
		/// <param name="sender">イベント発生コントロール</param>
		/// <param name="e">イベント引数</param>
		/// <param name="funclist">Fキー機能リスト</param>
		/// <returns>ファンクションキーとして処理したかどうかのフラグ</returns>
		public static bool CallFunctionKeyMethod(Window wnd, object sender, KeyEventArgs e, Dictionary<Key, Action<object, KeyEventArgs>> funclist)
		{
			bool isFkey = true;
			bool isIme = false;
			Action<object, KeyEventArgs> func = null;
			Key key = e.Key;
			if (key == Key.ImeProcessed)
			{
				isIme = true;
				key = e.ImeProcessedKey;
                return isIme;
			}
			switch (key)
			{
			case Key.L:
				// Ctrl + Alt + Shift + L で現行ログを表示する
				if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Alt | ModifierKeys.Shift))
				{
					e.Handled = true;
					LogView lview = new LogView();
					lview.Show();
				}
				break;
			case Key.V:
				// Ctrl + Alt + Shift + V で現行バージョンを表示する
				if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Alt | ModifierKeys.Shift))
				{
					e.Handled = true;
					ShowVersion();
				}
				break;
			case Key.F1:
			case Key.F2:
			case Key.F3:
			case Key.F4:
			case Key.F5:
			case Key.F6:
			case Key.F7:
			case Key.F8:
			case Key.F9:
			case Key.F10:
			case Key.F11:
			case Key.F12:
				var ctl = FocusManager.GetFocusedElement(wnd);
				if (ctl is TextBox)
				{
					var uctxt = FindVisualParent<UcTextBox>(ctl as TextBox);
					if (uctxt != null)
					{
						if (isIme)
						{
							return false;
						}
						uctxt.ApplyFormat();
					}
				}
				if (Keyboard.Modifiers == ModifierKeys.None)
				{
					e.Handled = true;
					if (funclist.ContainsKey(key))
					{
						func = funclist[key];
						func(sender, e);
					}
				}
				break;
			case Key.System:
				// F10がaltの代替キー処理されてしまうことの対策
				if (e.SystemKey == Key.F10)
				{
					var ctl1 = FocusManager.GetFocusedElement(wnd);
					if (ctl1 is TextBox)
					{
						var uctxt = FindVisualParent<UcTextBox>(ctl1 as TextBox);
						if (uctxt != null)
						{
							if (isIme)
							{
								return false;
							}
							uctxt.ApplyFormat();
						}
					}
					if (Keyboard.Modifiers == ModifierKeys.None)
					{
						e.Handled = true;
						if (funclist.ContainsKey(Key.F10))
						{
							func = funclist[Key.F10];
							func(sender, e);
						}
					}
				}
				break;
			}


			return isFkey;
		}

		/// <summary>
		/// バージョン表示
		/// </summary>
		private static void ShowVersion()
		{
			//自分自身のAssemblyを取得
			System.Reflection.Assembly asm = System.Reflection.Assembly.GetEntryAssembly();
			string appname = asm.GetName().Name;
			System.IO.FileInfo fi = new System.IO.FileInfo(asm.Location);
			string appdir = fi.Directory.FullName;
			string datadir;
			System.Version ver;
			if (ApplicationDeployment.IsNetworkDeployed)
			{
				datadir = ApplicationDeployment.CurrentDeployment.DataDirectory;
				ver = ApplicationDeployment.CurrentDeployment.CurrentVersion;
			}
			else
			{
				datadir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
				ver = asm.GetName().Version;
			}

			MessageBox.Show(string.Format("Assembly: {0}\r\nversion:{1}\r\nApplication Path: {2}\r\nData Folder:{3}"
				, appname, ver, appdir, datadir));

			Process.Start(@"explorer", appdir);
			Process.Start(@"explorer", datadir);
		}

		/// <summary>
		/// バージョン取得用メソッド
		/// </summary>
		/// <returns>バージョン番号文字列</returns>
		public static System.Version GetVersion()
		{
			System.Version ver;
			if (ApplicationDeployment.IsNetworkDeployed)
			{
				ver = ApplicationDeployment.CurrentDeployment.CurrentVersion;
			}
			else
			{
				System.Reflection.Assembly asm = System.Reflection.Assembly.GetEntryAssembly();
				ver = asm.GetName().Version;
			}
			return ver;
		}

		/// <summary>
		/// マスター検索画面の呼び出しを行う
		/// </summary>
		/// <param name="wnd">呼び出し元Window</param>
		/// <param name="srchwndTypeList">マスター種別の一覧</param>
		public static void CallMasterSearch(Window wnd, Dictionary<string, List<Type>> srchwndTypeList)
		{
			UcLabelTwinTextBox twintextbox = ViewBaseCommon.GetCurrentTwinText();
			if (twintextbox == null)
			{
				return;
			}
			if (srchwndTypeList == null)
			{
				return;
			}
			try
			{
				var wndtp = srchwndTypeList.Where(x => x.Key == twintextbox.DataAccessName).FirstOrDefault();
				if ((wndtp.Value is List<Type>) != true)
				{
					return;
				}
				Type tp = (wndtp.Value as List<Type>)[1];
				if (tp == null)
				{
					return;
				}
				if (tp.BaseType != typeof(WindowMasterSearchBase))
				{
					ViewBaseException ex = new ViewBaseException(string.Format("{0}がWindowMasterSearchBaseの派生クラスではないため処理できません。", tp));
					throw ex;
				}

				twintextbox.BeforeCode = twintextbox.Text1;

				WindowMasterSearchBase mstsrch = Activator.CreateInstance(tp) as WindowMasterSearchBase;
				mstsrch.TwinTextBox = twintextbox;
				if (mstsrch.ShowDialog(wnd) == true)
				{
					twintextbox.AfterCode = twintextbox.Text1;
					// 検索結果を選択して閉じられた場合、該当するTwinTextboxにPreviewKeyDownイベントを送信する。
					IInputElement element = Keyboard.FocusedElement;
					var s = PresentationSource.FromDependencyObject(element as DependencyObject);
					var eventArgs = new KeyEventArgs(Keyboard.PrimaryDevice, s, (int)System.DateTime.Now.Ticks, Key.Enter);
					eventArgs.RoutedEvent = Keyboard.PreviewKeyDownEvent;
					InputManager.Current.ProcessInput(eventArgs);
					eventArgs.RoutedEvent = Keyboard.PreviewKeyUpEvent;
					InputManager.Current.ProcessInput(eventArgs);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// マスターメンテナンス画面の呼び出しを行う
		/// </summary>
		/// <param name="mntwndTypes">マスター種別の一覧</param>
		public static void CallMasterMainte(Dictionary<string, List<Type>> mntwndTypes)
		{
			UcLabelTwinTextBox twintextbox = ViewBaseCommon.GetCurrentTwinText();
			if (twintextbox == null)
			{
				return;
			}
			if (mntwndTypes == null)
			{
				return;
			}
			try
			{
				var wndtp = mntwndTypes.Where(x => x.Key == twintextbox.DataAccessName).FirstOrDefault();
				if ((wndtp.Value is List<Type>) != true)
				{
					return;
				}
				Type tp = (wndtp.Value as List<Type>)[0];
				if (tp == null)
				{
					return;
				}
				WindowMasterMainteBase mstmnt = Activator.CreateInstance(tp) as WindowMasterMainteBase;
                var wnd = Window.GetWindow(twintextbox);
                mstmnt.ShowDialog(wnd);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 指定されたコントロールが所属するDataGridを取得する
		/// </summary>
		/// <param name="curctl">検索対象コントロール</param>
		/// <returns>検索結果</returns>
		public static DataGrid GetCurrentDataGrid(DependencyObject curctl)
		{
			if (curctl is DataGrid)
			{
				return curctl as DataGrid;
			}
			else
			{
				return FindVisualParent<DataGrid>(curctl);
			}
		}

		/// <summary>
		/// フォーカスのあるコントロールがDataGridである場合、選択されているCellを取得する
		/// </summary>
		/// <param name="curctl">検索対象コントロール</param>
		/// <returns>検索結果</returns>
		public static DataGridCell GetCurrentDataGridCell(DependencyObject curctl)
		{
			var parent = GetCurrentDataGrid(curctl);
			if (parent == null)
			{
				return null;
			}
			return parent.SelectedItem as DataGridCell;
		}

		/// <summary>
		/// フォーカスのあるDataGridのDataGridRowを取得する
		/// </summary>
		/// <param name="curctl">検索対象コントロール</param>
		/// <returns>検索結果</returns>
		public static DataRowView GetCurrentDataGridRow(DependencyObject curctl)
		{
			try
			{
				DataGrid grid = GetCurrentDataGrid(curctl);
				if (grid != null)
				{
					object item = grid.CurrentItem;
					return (item is DataRowView) ? (item as DataRowView) : null;
				}
				else
				{
					return null;
				}
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>
		/// 現在フォーカスのあるDataGridのDataGridRowを取得する
		/// </summary>
		/// <param name="curctl">検索対象コントロール</param>
		/// <returns>検索結果</returns>
		public static DataGridRow GetDataGridRow(DependencyObject curctl)
		{
			try
			{
				var parent = FindVisualParent<DataGridRow>(curctl);
				return parent;
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>
		/// 引数のオブジェクトの指定した型の親コントロールを探す。
		/// </summary>
		/// <typeparam name="T">検索対象のコントロールタイプ</typeparam>
		/// <param name="child">検索開始コントロール</param>
		/// <returns>検索結果</returns>
		public static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
		{
			// 連続でセル移動したとき、child が null になる場合がある（詳細は未検証）
			if (child == null)
			{
				return null;
			}

			DependencyObject parentObject = VisualTreeHelper.GetParent(child);

			if (parentObject == null)
			{
				return null;
			}

			T parent = parentObject as T;
			if (parent != null)
			{
				return parent;
			}
			else
			{
				return FindVisualParent<T>(parentObject);
			}
		}

		/// <summary>
		/// 指定されたタイプの子コントロールを検索する
		/// </summary>
		/// <typeparam name="T">検索対象のコントロールタイプ</typeparam>
		/// <param name="parent">検索開始コントロール</param>
		/// <returns>検索結果</returns>
		public static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
		{
			List<DependencyObject> childObjects = new List<DependencyObject>();
			for (int ix = 0; ix < VisualTreeHelper.GetChildrenCount(parent); ix++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(parent, ix);
				if (child == null)
				{
					continue;
				}
				if (child is T)
				{
					if (child is UIElement)
					{
						if ((child as UIElement).IsEnabled != true)
						{
							continue;
						}
					}
					return child as T;
				}
				childObjects.Add(child);
			}
			foreach (DependencyObject childObj in childObjects)
			{
				DependencyObject child = FindVisualChild<T>(childObj) as T;
				if (child is T)
				{
					return child as T;
				}
			}
			return null;
		}

		/// <summary>
		/// 指定されたタイプの子コントロールのコレクションを取得する
		/// </summary>
		/// <typeparam name="T">検索対象のコントロールタイプ</typeparam>
		/// <param name="parent">検索開始コントロール</param>
		/// <param name="enablledOnly">検索対象をIsEnabledのみとするかどうか</param>
		/// <returns>検索結果コレクション</returns>
		public static List<T> FindLogicalChildList<T>(DependencyObject parent, bool enablledOnly = true) where T : DependencyObject
		{
			List<T> childlist = new List<T>();
			List<DependencyObject> childObjects = new List<DependencyObject>();
			foreach(var child in LogicalTreeHelper.GetChildren(parent))
			{
				if (child is T)
				{
					if (child is UIElement)
					{
						if (enablledOnly && (child as UIElement).IsEnabled != true)
						{
							continue;
						}
					}
					childlist.Add(child as T);
				}
				if (child is DependencyObject)
				{
					childObjects.Add(child as DependencyObject);
				}
			}
			foreach (DependencyObject childObj in childObjects)
			{
				childlist.AddRange(FindLogicalChildList<T>(childObj, enablledOnly));
			}
			return childlist;
		}

		/// <summary>
		/// フォーカスのあるTwinTextBoxのコントロールを取得する
		/// </summary>
		/// <returns>TwinTextboxのオブジェクト</returns>
		public static UcLabelTwinTextBox GetCurrentTwinText()
		{
			IInputElement ctl = Keyboard.FocusedElement;
			if (!(ctl is TextBox))
			{
				return null;
			}
			var ctlct = (ctl as TextBox).Parent;
			if (!(ctlct is Grid))
			{
				return null;
			}
			var ucctl = (ctlct as Grid).Parent;
			if (!(ucctl is UcTextBox))
			{
				return null;
			}
			var ucctlg = (ucctl as UcTextBox).Parent;
			if (!(ucctlg is Grid))
			{
				return null;
			}
			var twintxt = (ucctlg as Grid).Parent;
			if (!(twintxt is UcLabelTwinTextBox))
			{
				return null;
			}
			return twintxt as UcLabelTwinTextBox;
		}

		/// <summary>
		/// DataGridCellのマウスクリックイベントの処理（Cellのフォーカス移動を強制する）
		/// </summary>
		/// <param name="sender">イベント発生コントロール</param>
		/// <param name="e">イベント引数</param>
		public static void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			DataGridCell cell = sender as DataGridCell;
			if (cell != null && !cell.IsEditing && !cell.IsReadOnly)
			{
				if (!cell.IsFocused)
				{
					cell.Focus();
				}
				DataGrid dataGrid = FindVisualParent<DataGrid>(cell);
				if (dataGrid != null)
				{
					if (dataGrid.SelectionUnit != DataGridSelectionUnit.FullRow)
					{
						if (!cell.IsSelected)
							cell.IsSelected = true;
					}
					else
					{
						DataGridRow row = FindVisualParent<DataGridRow>(cell);
						if (row != null && !row.IsSelected)
						{
							row.IsSelected = true;
						}
					}
				}
			}

		}

		/// <summary>
		/// DataGridの中から指定されたタイプのコントロールを取得する
		/// </summary>
		/// <typeparam name="T">検索対象のコントロールタイプ</typeparam>
		/// <param name="cell">検索開始コントロール</param>
		/// <returns>検索結果</returns>
		public static T FindUIElementInDataGridCell<T>(DataGridCell cell) where T : FrameworkElement
		{
			try
			{
				var child = FindVisualChild<T>(cell);
				return child as T;
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>
		/// DataGridの中から指定されたタイプのコントロールを取得する
		/// </summary>
		/// <typeparam name="T">検索対象のコントロールタイプ</typeparam>
		/// <param name="cell">検索開始コントロール</param>
		/// <returns>検索結果</returns>
		public static List<T> FindUIElementListInDataGridCell<T>(DataGridCell cell) where T : FrameworkElement
		{
			try
			{
				return FindLogicalChildList<T>(cell);
			}
			catch (Exception)
			{
				return new List<T>();
			}
		}

		/// <summary>
		/// 指定されたカラム及びRowの情報からDataGridのCellを取得する
		/// </summary>
		/// <param name="col">DataGridのColumn情報</param>
		/// <param name="grow">DataGridのRowオブジェクト</param>
		/// <returns></returns>
		public static DataGridCell GetDataGridCell(DataGridColumn col, DataGridRow grow)
		{

			DataGridCell cell = FindVisualParent<DataGridCell>(col.GetCellContent(grow));

			return cell;
		}

		/// <summary>
		/// DataGridの指定セルの枠線を正常値色にする
		/// </summary>
		/// <param name="cell">DataGridのCellオブジェクト</param>
		public static void SetValidDataGridCell(DataGridCell cell)
		{
			cell.BorderBrush = Brushes.Transparent;
			cell.BorderThickness = new Thickness(1);
		}

		/// <summary>
		/// DataGridの指定セルの枠線を異常値色にする
		/// </summary>
		/// <param name="cell">DataGridのCellオブジェクト</param>
		public static void SetInvalidDataGridCell(DataGridCell cell)
		{
			cell.BorderBrush = Brushes.Red;
			cell.BorderThickness = new Thickness(1.3);
		}

		/// <summary>
		/// 画面が閉じられようとしたときに各子要素に通知します。
		/// </summary>
		/// <param name="target">画面</param>
		public static void WindowClosing(DependencyObject target)
		{
			foreach (var child in LogicalTreeHelper.GetChildren(target))
			{
				if (child is DependencyObject)
				{
					if (child is FrameworkControl)
					{
						(child as FrameworkControl).IsWindowClosing = true;
					}
					WindowClosing((DependencyObject)child);
				}
			}
		}

		/// <summary>
		/// 接続文字列取得
		/// </summary>
		/// <param name="wnd">画面</param>
		/// <returns>接続文字列</returns>
		public static string GetConnectString(Window wnd)
		{
			var view = wnd as IWindowViewBase;
			return view.ConnString;
		}

		/// <summary>
		/// 画面の接続文字列フィールドに引数で指定された接続文字列をセットアップする
		/// </summary>
		/// <param name="wnd">画面</param>
		/// <param name="server">サーバー名</param>
		/// <param name="db">Database名</param>
		/// <param name="user">ユーザ名</param>
		/// <param name="passwd">パスワード</param>
		public static void SetupConnectStringuserDB(IWindowViewBase wnd, string server, string db, string user, string passwd)
		{
			try
			{
				SqlConnectionStringBuilder sqlBuilder = MakeConnectString(Utility.Decrypt(server), Utility.Decrypt(db), Utility.Decrypt(user), Utility.Decrypt(passwd));

				wnd.ConnString = Utility.Encrypt(sqlBuilder.ToString());

			}
			catch (Exception ex)
			{
			}
		}

		/// <summary>
		/// 引数で指定された値から接続文字列を生成する。
		/// </summary>
		/// <param name="server">サーバー名</param>
		/// <param name="db">Database名</param>
		/// <param name="user">ユーザ名</param>
		/// <param name="passwd">パスワード</param>
		/// <returns>接続文字列</returns>
		public static SqlConnectionStringBuilder MakeConnectString(string server, string db, string user, string passwd)
		{
			SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
			sqlBuilder.DataSource = server;
			sqlBuilder.InitialCatalog = db;
			sqlBuilder.IntegratedSecurity = false;
			sqlBuilder.PersistSecurityInfo = false;
			sqlBuilder.UserID = user;
			sqlBuilder.Password = passwd;
			sqlBuilder.MultipleActiveResultSets = true;
			// SQLSERVER暗号化通信設定
			sqlBuilder.Encrypt = true;
			sqlBuilder.TrustServerCertificate = false;
			sqlBuilder.ApplicationName = "EntityFramework";

			return sqlBuilder;
		}
	}

	/// <summary>
	/// フォントサイズ自動変換用コンバータ（現在未使用）
	/// </summary>
	public class FontFamilyToNameConverter : IValueConverter
	{
		/// <summary>
		/// 変換メソッド（現在未使用）
		/// </summary>
		/// <param name="value">値</param>
		/// <param name="targetType">ターゲット型</param>
		/// <param name="parameter">変換パラメータ</param>
		/// <param name="culture">カルチャー</param>
		/// <returns>変換結果</returns>
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var v = value as FontFamily;
			var currentLang = XmlLanguage.GetLanguage(culture.IetfLanguageTag);
			return v.FamilyNames.FirstOrDefault(o => o.Key == currentLang).Value ?? v.Source;
		}

		/// <summary>
		/// 変換メソッドコールバック（現在未使用）
		/// </summary>
		/// <param name="value">値</param>
		/// <param name="targetType">ターゲット型</param>
		/// <param name="parameter">変換パラメータ</param>
		/// <param name="culture">カルチャー</param>
		/// <returns>変換結果</returns>
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

}
