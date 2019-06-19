using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KyoeiSystem.Framework.Net;
using System.Collections;

namespace MyNumber
{
	#region バインド用データクラス

	#region マイナンバー画面用バインドデータクラス
	class MyNumberWindowData : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged メンバー

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		#endregion

		private string _TenantKey;
		public string TenantKey
		{
			get { return _TenantKey; }
			set { _TenantKey = value; NotifyPropertyChanged(); }
		}
		private string _AccessKey;
		public string AccessKey
		{
			get { return _AccessKey; }
			set { _AccessKey = value; NotifyPropertyChanged(); }
		}
		private string _AccountID;
		public string AccountID
		{
			get { return _AccountID; }
			set { _AccountID = value; NotifyPropertyChanged(); }
		}
		private string _AccountPassword;
		public string AccountPassword
		{
			get { return _AccountPassword; }
			set { _AccountPassword = value; NotifyPropertyChanged(); }
		}

		private ObservableCollection<CertInfo> _CertInfoList = new ObservableCollection<CertInfo>();
		public ObservableCollection<CertInfo> CertInfoList
		{
			get { return _CertInfoList; }
			set { _CertInfoList = value; NotifyPropertyChanged(); }
		}

		private CertInfo _selectedCertInfo = null;
		public CertInfo SelectedCertInfo
		{
			get { return _selectedCertInfo; }
			set { _selectedCertInfo = value; NotifyPropertyChanged(); }
		}
		private bool _needClientCertSet = false;
		public bool NeedClientCertSet
		{
			get { return _needClientCertSet; }
			set { _needClientCertSet = value; NotifyPropertyChanged(); }
		}
		private string _Message = string.Empty;
		public string Message
		{
			get { return _Message; }
			set { _Message = value; NotifyPropertyChanged(); this.apistatus = value; }
		}

		private bool _IsEditable = true;
		public bool IsEditable
		{
			get { return _IsEditable; }
			set { _IsEditable = value; NotifyPropertyChanged(); }
		}
		private bool _IsExecutable = false;
		public bool IsExecutable
		{
			get { return _IsExecutable; }
			set
			{
				_IsExecutable = value;
				NotifyPropertyChanged();
				if (value)
				{
					this.Message = "接続情報を確認して、実行ボタンを押してください。";
				}
			}
		}
		private int _Progress = 0;
		public int Progress
		{
			get { return _Progress; }
			set { _Progress = value; NotifyPropertyChanged(); }
		}
		private int _ProgressMaxValue = 10;
		public int ProgressMaxValue
		{
			get { return _ProgressMaxValue; }
			set { _ProgressMaxValue = value; NotifyPropertyChanged(); }
		}
		private bool _inProgress = false;
		public bool InProgress
		{
			get { return _inProgress; }
			set { _inProgress = value; NotifyPropertyChanged(); }
		}
		private Visibility _CertListVisibility = Visibility.Collapsed;
		public Visibility CertListVisibility
		{
			get { return _CertListVisibility; }
			set { _CertListVisibility = value; NotifyPropertyChanged(); }
		}
		private Visibility _CertSubjectVisibility = Visibility.Visible;
		public Visibility CertSubjectVisibility
		{
			get { return _CertSubjectVisibility; }
			set { _CertSubjectVisibility = value; NotifyPropertyChanged(); }
		}
		private string _certSubject = string.Empty;
		public string CertSubject
		{
			get { return _certSubject; }
			set { _certSubject = value; NotifyPropertyChanged(); }
		}

		private Visibility _apiVisibility = Visibility.Visible;
		public Visibility apiVisibility
		{
			get { return _apiVisibility; }
			set { _apiVisibility = value; NotifyPropertyChanged(); }
		}
		private string _apistatus = string.Empty;
		public string apistatus
		{
			get { return _apistatus; }
			set { _apistatus = value; NotifyPropertyChanged(); }
		}
		private string _apilog = string.Empty;
		public string apilog
		{
			get { return _apilog; }
			set { _apilog = value; NotifyPropertyChanged(); }
		}

		private bool _btnSwitchIsEnabled = false;
		public bool btnSwitchIsEnabled
		{
			get { return _btnSwitchIsEnabled; }
			set { _btnSwitchIsEnabled = value; NotifyPropertyChanged(); }
		}
	}
	#endregion

	#region CertInfo メンバー
	public class CertInfo : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged メンバー

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		#endregion

		private string _serialNo = null;
		public string SerialNo
		{
			get { return _serialNo; }
			set { _serialNo = value; NotifyPropertyChanged(); }
		}

		private string _issueName = null;
		public string IssuerName
		{
			get { return _issueName; }
			set { _issueName = value; NotifyPropertyChanged(); }
		}

		private string _formatedIssueName = null;
		public string FormatedIssuerName
		{
			get { return _formatedIssueName; }
			set { _formatedIssueName = value; NotifyPropertyChanged(); }
		}

		private string _subjectName = null;
		public string SubjectName
		{
			get { return _subjectName; }
			set { _subjectName = value; NotifyPropertyChanged(); }
		}

		private string _formatedSubjectName = null;
		public string FromatedSubjectName
		{
			get { return _formatedSubjectName; }
			set { _formatedSubjectName = value; NotifyPropertyChanged(); }
		}

		private DateTime _fromDate;
		public DateTime FromDate
		{
			get { return _fromDate; }
			set { _fromDate = value; NotifyPropertyChanged(); }
		}

		private DateTime toDate;
		public DateTime ToDate
		{
			get { return toDate; }
			set { toDate = value; NotifyPropertyChanged(); }
		}

		private bool _selectable = false;
		public bool Selectable
		{
			get { return _selectable; }
			set { _selectable = value; NotifyPropertyChanged(); }
		}

	}
	#endregion

	#endregion

	/// <summary>
	/// マイナンバー取得処理画面
	/// </summary>
	public partial class MyNumberWindow : Window
	{
		#region ローカル変数
		MyNumberWindowData cntxt = new MyNumberWindowData();

		// カラム名として使えない文字
		//private string badcolchars = "\n\t\r~()#\\/=><+-*%&|^\'\"[],";

		private MyNumberAPI mapi = null;
		private MyNumberAPIConfig mapicfg = null;

		private string FilePathEmployee = string.Empty;
		private string FilePathFamily = string.Empty;
		private string FilePathIndividual = string.Empty;

		#endregion

		#region 公開プロパティ
		private int _exitcode = -1;
		/// <summary>
		/// ＜処理結果＞true:成功、false:失敗
		/// </summary>
		public int ExitCode
		{
			get { return _exitcode; }
			protected set { _exitcode = value; }
		}
		#endregion

		#region コンストラクタ
		public MyNumberWindow()
		{
			InitializeComponent();
			this.DataContext = cntxt;

			cntxt.NeedClientCertSet = false;
		}
		#endregion

		#region フォーカス制御
		private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				UIElement element = Keyboard.FocusedElement as UIElement;
				if (element != null)
				{
					element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
					e.Handled = true;
				}
			}
		}

		public static bool SetFocusToTopControl(DependencyObject target)
		{
			foreach (var child in LogicalTreeHelper.GetChildren(target))
			{
				if (child is DependencyObject)
				{
					if (child is TextBox)
					{
						if ((child as TextBox).IsEnabled)
						{
							Keyboard.Focus(child as TextBox);
							return true;
						}
					}
					else if (child is PasswordBox)
					{
						if ((child as PasswordBox).IsEnabled)
						{
							Keyboard.Focus(child as PasswordBox);
							return true;
						}
					}
					else if (child is DatePicker)
					{
						if ((child as DatePicker).IsEnabled)
						{
							Keyboard.Focus(child as DatePicker);
							return true;
						}
					}
					else if (child is ComboBox)
					{
						if ((child as ComboBox).IsEnabled)
						{
							Keyboard.Focus(child as ComboBox);
							return true;
						}
					}
					else if (child is CheckBox)
					{
						if ((child as CheckBox).IsEnabled)
						{
							Keyboard.Focus(child as CheckBox);
							return true;
						}
					}
					else if (SetFocusToTopControl((DependencyObject)child))
					{
						return true;
					}
				}
			}
			return false;
		}
		#endregion

		#region Load時の処理
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			cntxt.Message = "処理準備中";

			SetFocusToTopControl(this);

			MyNumberAPIConfig cmdcfg = new MyNumberAPIConfig();
			try
			{
				cmdcfg = GetCommandParameters(System.Environment.GetCommandLineArgs().Skip(1));
			}
			catch (Exception ex)
			{
				// 基本情報が指定されていなければ処理続行不可能
				MessageBox.Show(ex.Message);
				this.ExitCode = 1;
				this.Close();
				return;
			}
			finally
			{
			}

			var dsp = this.Dispatcher;
			try
			{
				mapi = new MyNumberAPI(null);
				// ローカルConfigからパラメータをロードする
				mapi.LoadConfig();
				mapicfg = mapi.ApiConfig;

				// コマンドライン引数で指定されたパラメータを優先する
				if (string.IsNullOrWhiteSpace(cmdcfg.TenantKey) != true)
					mapicfg.TenantKey = cmdcfg.TenantKey;
				if (string.IsNullOrWhiteSpace(cmdcfg.AccessKey) != true)
					mapicfg.AccessKey = cmdcfg.AccessKey;
				if (string.IsNullOrWhiteSpace(cmdcfg.AccountID) != true)
					mapicfg.AccountID = cmdcfg.AccountID;
				if (string.IsNullOrWhiteSpace(cmdcfg.AccountPassword) != true)
					mapicfg.AccountPassword = cmdcfg.AccountPassword;

				cntxt.TenantKey = mapicfg.TenantKey;
				cntxt.AccessKey = mapicfg.AccessKey;
				cntxt.AccountID = mapicfg.AccountID;
				cntxt.AccountPassword = mapicfg.AccountPassword;
				passboxCert.Password = cntxt.AccountPassword;
				if (string.IsNullOrWhiteSpace(cntxt.TenantKey)
				 || string.IsNullOrWhiteSpace(cntxt.AccessKey)
				 || string.IsNullOrWhiteSpace(cntxt.AccountID)
				 || string.IsNullOrWhiteSpace(cntxt.AccountPassword)
					)
				{
					throw new MyNumberAPIException("接続情報を全て設定してください。");
				}
				var certlist = KyoeiSystem.Framework.Net.Cert.GetCertCollection();
				cntxt.SelectedCertInfo = null;
				if (string.IsNullOrWhiteSpace(mapicfg.ClientCertSerialNo))
				{
					throw new MyNumberAPICertException();
				}
				foreach (var cert in certlist)
				{
					if (mapicfg.ClientCertSerialNo == cert.SerialNumber)
					{
						cntxt.SelectedCertInfo = new CertInfo()
						{
							SerialNo = cert.SerialNumber,
							SubjectName = cert.SubjectName.Name,
							IssuerName = cert.IssuerName.Name,
							FormatedIssuerName = cert.IssuerName.Format(true).TrimEnd(new char[] { '\r', '\n', }),
							FromatedSubjectName = cert.SubjectName.Format(true).TrimEnd(new char[] { '\r', '\n', }),
							FromDate = cert.NotBefore,
							ToDate = cert.NotAfter,
							Selectable = (cert.NotAfter >= DateTime.Now),
						};
						cntxt.CertSubject = string.Empty;
						cntxt.CertSubject += string.Format("[有効期限]\r\n{0:yyyy/MM/dd}\r\n", cntxt.SelectedCertInfo.ToDate);
						cntxt.CertSubject += string.Format("[発行元]\r\n{0}\r\n", cntxt.SelectedCertInfo.FormatedIssuerName);
						cntxt.CertSubject += string.Format("[サブジェクト]\r\n{0}\r\n", cntxt.SelectedCertInfo.FromatedSubjectName);
						cntxt.CertSubject += string.Format("[シリアル番号]\r\n{0}", cntxt.SelectedCertInfo.SerialNo);

						mapicfg.ClientCertSerialNo = cntxt.SelectedCertInfo.SerialNo;

						break;
					}
				}
				checkConfigs();
			}
			catch (MyNumberAPIConfigException ex)
			{
				//dsp.Invoke(new ConfigErrorDelegate(ConfigError), string.Format("アプリケーションが正しくセットアップできていません。\r\n<詳細>\r\n{0}", ex.Message));
				dsp.Invoke(new ExecuteResultDelegate(ExecuteResult), false, string.Format("アプリケーションが正しくセットアップできていません。\r\n<詳細>\r\n{0}", ex.Message));
			}
			catch (MyNumberAPICertException)
			{
				dsp.Invoke(new ConfigErrorDelegate(ConfigCertError), "クライアント証明書を選択してください。");
			}
			//catch (MyNumberAPIException ex)
			//{
			//	dsp.Invoke(new ExecuteResultDelegate(ExecuteResult), false, string.Format("アプリケーションが正しくセットアップできていません。\r\n<詳細>\r\n{0}", ex.Message));
			//}
			catch (Exception ex)
			{
				dsp.Invoke(new ConfigErrorDelegate(ConfigError), ex.Message);
			}
			finally
			{
			}
		}

		#region コマンドライン引数処理
		/// <summary>
		/// コマンドライン引数からWebAPI用ユーザ情報を取得する
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		private MyNumberAPIConfig GetCommandParameters(IEnumerable<string> args)
		{
			Dictionary<string, string> plist = new Dictionary<string, string>()
			{
				{ "TKEY", string.Empty },
				{ "AKEY", string.Empty },
				{ "USER", string.Empty },
				{ "PASS", string.Empty },
				{ "EMPLOYEEFILE", string.Empty },
				{ "FAMILYFILE", string.Empty },
				{ "MYNUMBERFILE", string.Empty },
			};
			//var p_t = args.Where(x => Regex.IsMatch(x, "/t=", RegexOptions.IgnoreCase)).FirstOrDefault();
			//if (p_t != null)
			//{
			//	plist["TKEY"] = p_t.Remove(0, 3);
			//}
			//var p_a = args.Where(x => Regex.IsMatch(x, "/a=", RegexOptions.IgnoreCase)).FirstOrDefault();
			//if (p_a != null)
			//{
			//	plist["AKEY"] = p_a.Remove(0, 3);
			//}
			//var p_u = args.Where(x => Regex.IsMatch(x, "/u=", RegexOptions.IgnoreCase)).FirstOrDefault();
			//if (p_u != null)
			//{
			//	plist["USER"] = p_u.Remove(0, 3);
			//}
			//var p_p = args.Where(x => Regex.IsMatch(x, "/p=", RegexOptions.IgnoreCase)).FirstOrDefault();
			//if (p_p != null)
			//{
			//	plist["PASS"] = p_p.Remove(0, 3);
			//}
			var p_emp = args.Where(x => Regex.IsMatch(x, "/e=", RegexOptions.IgnoreCase)).FirstOrDefault();
			if (p_emp != null)
			{
				plist["EMPLOYEEFILE"] = p_emp.Remove(0, 3);
			}
			var p_fmly = args.Where(x => Regex.IsMatch(x, "/f=", RegexOptions.IgnoreCase)).FirstOrDefault();
			if (p_fmly != null)
			{
				plist["FAMILYFILE"] = p_fmly.Remove(0, 3);
			}
			var p_mynum = args.Where(x => Regex.IsMatch(x, "/m=", RegexOptions.IgnoreCase)).FirstOrDefault();
			if (p_mynum != null)
			{
				plist["MYNUMBERFILE"] = p_mynum.Remove(0, 3);
			}
			if (string.IsNullOrWhiteSpace(plist["EMPLOYEEFILE"])
				|| string.IsNullOrWhiteSpace(plist["FAMILYFILE"])
				|| string.IsNullOrWhiteSpace(plist["MYNUMBERFILE"])
				)
			{
				throw new Exception("コマンドライン引数が不正です。");
			}

			MyNumberAPIConfig cfg = new MyNumberAPIConfig();
			cfg.TenantKey = plist["TKEY"];
			cfg.AccessKey = plist["AKEY"];
			cfg.AccountID = plist["USER"];
			cfg.AccountPassword = plist["PASS"];
			FilePathEmployee = plist["EMPLOYEEFILE"];
			FilePathFamily = plist["FAMILYFILE"];
			FilePathIndividual = plist["MYNUMBERFILE"];

			return cfg;
		}
		#endregion

		#region Config取得時のDelegate処理
		delegate void ConfigErrorDelegate(string msg);
		delegate void WritePasswordDelegate();

		private void WritePassword()
		{
			passboxCert.Password = cntxt.AccountPassword;
		}

		private void ConfigCertError(string msg)
		{
			cntxt.Message = msg;
			MessageBox.Show(cntxt.Message);
			setupCertList();
			cntxt.apiVisibility = System.Windows.Visibility.Collapsed;
		}

		private void ConfigError(string msg)
		{
			cntxt.Message = msg;
			MessageBox.Show(cntxt.Message);
			cntxt.apiVisibility = System.Windows.Visibility.Collapsed;
		}
		#endregion

		#region クライアント証明書一覧セットアップ
		private void setupCertList()
		{
			cntxt.NeedClientCertSet = true;
			cntxt.CertSubjectVisibility = System.Windows.Visibility.Collapsed;
			cntxt.CertListVisibility = System.Windows.Visibility.Visible;

			var certlist = KyoeiSystem.Framework.Net.Cert.GetCertCollection();
			cntxt.CertInfoList.Clear();
			foreach (var cert in certlist)
			{
				cntxt.CertInfoList.Add(
					new CertInfo()
					{
						SerialNo = cert.SerialNumber,
						SubjectName = cert.SubjectName.Name,
						IssuerName = cert.IssuerName.Name,
						FormatedIssuerName = cert.IssuerName.Format(true).TrimEnd(new char[] { '\r', '\n', }),
						FromatedSubjectName = cert.SubjectName.Format(true).TrimEnd(new char[] { '\r', '\n', }),
						FromDate = cert.NotBefore,
						ToDate = cert.NotAfter,
						Selectable = (cert.NotAfter >= DateTime.Now),
					});
			}
		}
		#endregion

		#endregion

		#region 画面表示後の表示
		private void Window_ContentRendered(object sender, EventArgs e)
		{
			ExecuteGetData();
		}
		#endregion

		#region データ取得実行本体
		private void ExecuteGetData()
		{
			ExitCode = -1;
			cntxt.Progress = 0;
			cntxt.ProgressMaxValue = 14;

			if (cntxt.IsExecutable != true)
			{
				return;
			}
			if (checkConfigs() != true)
			{
				//MessageBox.Show(string.Format("クライアント証明書を選択してください。"));
				cntxt.apiVisibility = System.Windows.Visibility.Collapsed;
				cntxt.Message = "接続情報を確認してください。";
				return;
			}
			cntxt.btnSwitchIsEnabled = false;
			mapicfg.TenantKey = cntxt.TenantKey;
			mapicfg.AccessKey = cntxt.AccessKey;
			mapicfg.AccountID = cntxt.AccountID;
			mapicfg.AccountPassword = cntxt.AccountPassword;
			mapicfg.ClientCertSerialNo = cntxt.SelectedCertInfo.SerialNo;
			try
			{
				cntxt.apiVisibility = System.Windows.Visibility.Visible;
				mapi.SaveConfig();

				var dsp = this.Dispatcher;
				cntxt.InProgress = true;
				Task.Run(() =>
				{
					try
					{
						int progressval = 1;
						cntxt.IsEditable = false;
						dsp.BeginInvoke(new ProgressValueChangeDelegate(ProgressValueChange), progressval++);
						cntxt.apilog = string.Empty;

						cntxt.Message = "マイナンバーシステム接続中";
						cntxt.apilog += "> " + cntxt.Message + "\r\n";
						mapi.LogOn();
						dsp.BeginInvoke(new ProgressValueChangeDelegate(ProgressValueChange), progressval++);
						cntxt.apilog += "> " + string.Format("接続完了\r\n");

						cntxt.Message = "従業員データ取得中";
						cntxt.apilog += "> " + cntxt.Message + "\r\n";
						var empdata = mapi.GetEmployeeList(true);
						dsp.BeginInvoke(new ProgressValueChangeDelegate(ProgressValueChange), progressval++);
						cntxt.apilog += "> " + string.Format("従業員:{0}件\r\n", empdata.Count);

						Dictionary<string, EmployeeUpdateMid> updList = new Dictionary<string, EmployeeUpdateMid>();
						foreach (var item in empdata)
						{
							if (string.IsNullOrWhiteSpace(item.matchingid))
							{
								updList[item.personid] = new EmployeeUpdateMid()
								{
									oldmatchingid = item.matchingid,
									newmatchingid = string.Format("M{0}", item.employeeno),
								};
							}
						}
						if (updList.Count > 0)
						{
							cntxt.Message = "従業員データマッチングID更新中";
							cntxt.apilog += "> " + cntxt.Message + "\r\n";
							mapi.UpdateEmployeesMatchingId(updList);
							dsp.BeginInvoke(new ProgressValueChangeDelegate(ProgressValueChange), progressval++);
							
							cntxt.Message = "従業員データ取得中";
							cntxt.apilog += "> " + cntxt.Message + "\r\n";
							empdata = mapi.GetEmployeeList(true);
							dsp.BeginInvoke(new ProgressValueChangeDelegate(ProgressValueChange), progressval++);
							cntxt.apilog += "> " + string.Format("従業員:{0}件\r\n", empdata.Count);
						}
						cntxt.Message = "従業員データ保存中";
						cntxt.apilog += "> " + cntxt.Message + "\r\n";
						DataToCSV(empdata, FilePathEmployee);
						dsp.BeginInvoke(new ProgressValueChangeDelegate(ProgressValueChange), progressval++);

						cntxt.Message = "扶養家族データ取得中";
						cntxt.apilog += "> " + cntxt.Message + "\r\n";
						var fmlydata = mapi.GetFamilies(true);
						dsp.BeginInvoke(new ProgressValueChangeDelegate(ProgressValueChange), progressval++);
						cntxt.apilog += "> " + string.Format("扶養家族:{0}件\r\n", fmlydata.Count);

						Dictionary<string, FamiliesUpdateMid> updListF = new Dictionary<string, FamiliesUpdateMid>();
						// 扶養家族の所属従業員ごとに処理
						foreach (var emp in (from f in fmlydata select f.employeepersonid).Distinct())
						{
							var emid = (from e in empdata where e.personid == emp select e.matchingid).FirstOrDefault();

							int idx = 1;
							foreach (var item in fmlydata)
							{
								if (string.IsNullOrWhiteSpace(item.matchingid))
								{
									updListF[item.personid] = new FamiliesUpdateMid()
									{
										oldmatchingid = item.matchingid,
										newmatchingid = string.Format("{0}", string.Format("{0}{1:D02}", emid, idx++)),
									};
								}
							}
						}
						if (updListF.Count > 0)
						{
							cntxt.Message = "扶養家族データマッチングID更新中";
							cntxt.apilog += "> " + cntxt.Message + "\r\n";
							mapi.UpdateFamiliesMatchingId(updListF);
							dsp.BeginInvoke(new ProgressValueChangeDelegate(ProgressValueChange), progressval++);

							cntxt.Message = "扶養家族データ取得中";
							cntxt.apilog += "> " + cntxt.Message + "\r\n";
							fmlydata = mapi.GetFamilies(true);
							dsp.BeginInvoke(new ProgressValueChangeDelegate(ProgressValueChange), progressval++);
							cntxt.apilog += "> " + string.Format("扶養家族:{0}件\r\n", fmlydata.Count);
						}

						cntxt.Message = "扶養家族データ保存中";
						cntxt.apilog += "> " + cntxt.Message + "\r\n";
						DataToCSV(fmlydata, FilePathFamily);
						dsp.BeginInvoke(new ProgressValueChangeDelegate(ProgressValueChange), progressval++);

						Dictionary<string, string> parsons = new Dictionary<string, string>();
						// 従業員の個人IDとマッチングIDのセットを取得
						foreach (var item in empdata)
						{
							parsons[item.personid] = item.matchingid;
						}
						// 扶養家族の個人IDとマッチングIDのセットを取得
						foreach (var item in fmlydata)
						{
							parsons[item.personid] = item.matchingid;
						}

						// 個人IDとマッチングIDのセットから個人番号を取得
						dsp.BeginInvoke(new ProgressValueChangeDelegate(ProgressValueChange), progressval++);
						cntxt.Message = "個人番号データ取得中";
						cntxt.apilog += "> " + cntxt.Message + "\r\n";
						var mynumdata = mapi.GetIndividual(parsons);
						dsp.BeginInvoke(new ProgressValueChangeDelegate(ProgressValueChange), progressval++);
						cntxt.apilog += "> " + string.Format("個人番号:{0}件\r\n", mynumdata.Count);
						cntxt.Message = "個人番号データ保存中";
						cntxt.apilog += "> " + cntxt.Message + "\r\n";
						DataToCSV(mynumdata, FilePathIndividual);
						dsp.BeginInvoke(new ProgressValueChangeDelegate(ProgressValueChange), progressval++);

						cntxt.InProgress = false;
						cntxt.Message = string.Format("処理終了 （従業員:{0}件、扶養家族:{1}件、個人番号:{2}件）", empdata.Count, fmlydata.Count, mynumdata.Count);
						cntxt.apilog += "> " + cntxt.Message + "\r\n";

						ExitCode = 0;

					}
					catch (MyNumberAPIException ex)
					{
						ExitCode = 1;
						cntxt.Message = ex.Message;
						cntxt.apilog += ex.Message + (ex.InnerException != null ? "\r\n" + ex.InnerException.Message : string.Empty) + "\r\n";
						MessageBox.Show(ex.Message);
					}
					catch (TcpException ex)
					{
						ExitCode = 1;
						cntxt.Message = ex.Message;
						cntxt.apilog += ex.Message + (ex.InnerException != null ? "\r\n" + ex.InnerException.Message : string.Empty) + "\r\n";
						MessageBox.Show(ex.Message);
					}
					catch (IOException ex)
					{
						ExitCode = 1;
						cntxt.Message = "ファイル出力時にエラーが発生しました。\r\n" + ex.Message;
						cntxt.apilog += ex.Message + (ex.InnerException != null ? "\r\n" + ex.InnerException.Message : string.Empty) + "\r\n";
						MessageBox.Show(cntxt.Message);
					}
					catch (Exception ex)
					{
						ExitCode = 1;
						cntxt.Message = ex.Message;
						cntxt.apilog += ex.Message + (ex.InnerException != null ? "\r\n" + ex.InnerException.Message : string.Empty) + "\r\n";
						MessageBox.Show(ex.Message);
					}
					finally
					{
						cntxt.btnSwitchIsEnabled = true;
						cntxt.Message = "マイナンバーシステム切断中";
						cntxt.apilog += "> " + cntxt.Message + "\r\n";
						mapi.LogOff();
						cntxt.Message = "処理終了しました。";
						cntxt.apilog += "> " + string.Format("切断完了\r\n");
						dsp.BeginInvoke(new ProgressValueChangeDelegate(ProgressValueChange), cntxt.Progress++);

						{
							cntxt.apilog += "エラーログ：\r\n";
							cntxt.apilog += mapi.ApiLog.ToString();
						}

						cntxt.InProgress = false;
						cntxt.IsEditable = true;
						if (ExitCode == 0)
						{
							dsp.Invoke(new ExecuteResultDelegate(ExecuteResult), ExitCode);
						}
						else
						{
							// 正常終了していない場合、設定入力に移動
							//cntxt.apiVisibility = System.Windows.Visibility.Collapsed;
						}
					}
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				cntxt.apiVisibility = System.Windows.Visibility.Collapsed;
			}
			finally
			{
			}
		}

		delegate void ProgressValueChangeDelegate(int val);
		private void ProgressValueChange(int val)
		{
			cntxt.Progress = val;
		}

		delegate void ExecuteResultDelegate(int result, string msg);
		private void ExecuteResult(int result, string msg)
		{
			cntxt.Message = msg;
			cntxt.Progress = cntxt.ProgressMaxValue;
			MessageBox.Show(result == 0 ? "正常に取得しました。" : msg, "処理結果");
			this.DialogResult = result == 0;
		}
		#endregion

		#region CSV出力
		/// <summary>
		/// CSV出力
		/// </summary>
		/// <param name="data"></param>
		/// <param name="filepath"></param>
		/// <param name="columns"></param>
		private void DataToCSV(object data, string filepath, string columns = null)
		{
			try
			{
				FileInfo fi = new FileInfo(filepath);
				if (fi.Directory.Exists != true)
				{
					fi.Directory.Create();
				}
				try
				{
					using (StreamWriter writer = new StreamWriter(fi.Create(), System.Text.Encoding.GetEncoding("shift_jis")))
					{
						BindingFlags bindf = BindingFlags.Public | BindingFlags.Instance;
						Type tp = data.GetType();
						if (tp.IsArray)
						{
							// 単純配列型（XXX[]）の場合
							// カラム名行出力
							writer.WriteLine(getColumnsLine(tp.GetElementType()));
							PropertyInfo arrayp = tp.GetProperty("Item");
							var ary = (object[])data;
							// データ行出力
							for (int ix = 0; ix < ary.Length; ix++)
							{
								writer.WriteLine(GetValues(ary[ix].GetType().GetFields(bindf), ary[ix]));
							}
						}
						else
						{
							if (tp.IsGenericType && typeof(List<>).IsAssignableFrom(tp.GetGenericTypeDefinition()))
							{
								Type[] tpp = data.GetType().GetGenericArguments();
								// カラム名行出力
								writer.WriteLine(getColumnsLine(tpp[0]));

								// ジェネリックコレクション List<XXX>の場合
								Type ts = tp.GetProperty("Item").PropertyType;
								PropertyInfo arrayc = tp.GetProperty("Count");
								int cnt = (int)arrayc.GetValue(data, null);
								// データ行出力
								for (int ix = 0; ix < cnt; ix++)
								{
									PropertyInfo arrayp = tp.GetProperty("Item");
									var ary = arrayp.GetValue(data, new object[] { ix });
									writer.WriteLine(GetValues(ary.GetType().GetFields(bindf), ary));
								}
							}
							else
							{
								// コレクションではない場合

								// カラム名行出力
								writer.WriteLine(getColumnsLine(tp));
								// データ行出力
								writer.WriteLine(GetValues(tp.GetFields(bindf), data));
							}
						}
					}

				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
			}
		}

		private string GetValues(FieldInfo[] fields, object data)
		{
			string rowstr = string.Empty;
			//メンバを取得する
			foreach (FieldInfo fi in fields)
			{
				var col = fi.GetValue(data);
				rowstr += string.Format("\"{0}\",", col);
				//if (col != null)
				//{
				//	rowstr += string.Format("\"{0}\",", col);
				//}
				//rowstr += ",";
			}

			return rowstr;
		}

		private string getColumnsLine(Type tp)
		{
			string columns = string.Empty;
			//メンバを取得する
			FieldInfo[] fields = tp.GetFields(BindingFlags.Public | BindingFlags.Instance);
			foreach (FieldInfo fi in fields)
			{
				Type t = fi.FieldType;
				bool isnullable = false;
				if (t.IsGenericType)
				{
					isnullable = t.GetGenericTypeDefinition() == typeof(Nullable<>);
					if (isnullable)
					{
						t = Nullable.GetUnderlyingType(t);
					}
				}
				columns += string.Format("\"{0}\",", fi.Name);
			}

			return columns;
		}
		#endregion

		#region ユーザ操作関連

		#region キャンセルボタン
		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}
		#endregion

		#region 実行ボタン
		private void btnOK_Click(object sender, RoutedEventArgs e)
		{
			ExecuteGetData();
		}
		#endregion


		private void Window_Closing(object sender, CancelEventArgs e)
		{
			if (this.ExitCode < 0)
			{
				this.ExitCode = 9;
			}
		}

		private void btnCertList_Click(object sender, RoutedEventArgs e)
		{
			setupCertList();
			cntxt.IsExecutable = false;
		}

		private void btnCertSelect_Click(object sender, RoutedEventArgs e)
		{
			if (cntxt.SelectedCertInfo != null)
			{
				cntxt.CertSubject = string.Empty;
				cntxt.CertSubjectVisibility = System.Windows.Visibility.Visible;
				cntxt.CertSubject += string.Format("[有効期限]\r\n{0:yyyy/MM/dd}\r\n", cntxt.SelectedCertInfo.ToDate);
				cntxt.CertSubject += string.Format("[シリアル番号]\r\n{0}\r\n", cntxt.SelectedCertInfo.SerialNo);
				cntxt.CertSubject += string.Format("[発行元]\r\n{0}\r\n", cntxt.SelectedCertInfo.FormatedIssuerName);
				cntxt.CertSubject += string.Format("[サブジェクト]\r\n{0}", cntxt.SelectedCertInfo.FromatedSubjectName);
				cntxt.CertListVisibility = System.Windows.Visibility.Collapsed;
				if (mapicfg != null)
				{
					mapicfg.ClientCertSerialNo = cntxt.SelectedCertInfo.SerialNo;
					checkConfigs();
				}
			}
		}

		private void passBox_Changed(object sender, RoutedEventArgs e)
		{
			cntxt.AccountPassword = passboxCert.Password;
			checkConfigs();
		}

		private void txtChanged(object sender, TextChangedEventArgs e)
		{
			checkConfigs();
		}

		private void btnLogCopy_Click(object sender, RoutedEventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("tenantkey:[{0}]\r\n", cntxt.TenantKey);
			sb.AppendFormat("accesskey:[{0}]\r\n", cntxt.AccessKey);
			sb.AppendFormat("accouneID:[{0}]\r\n", cntxt.AccountID);
			sb.AppendFormat("password:[{0}文字]\r\n", cntxt.AccountPassword.Length);
			// パスワードは表示しない

			sb.Append(cntxt.apilog);
			Clipboard.SetText(sb.ToString());
		}

		private void btnSwitch_Click(object sender, RoutedEventArgs e)
		{
			cntxt.apiVisibility = System.Windows.Visibility.Collapsed;
		}
		#endregion

		#region 実行可能かどうかの判定
		private bool checkConfigs()
		{
			if (string.IsNullOrWhiteSpace(cntxt.TenantKey)
			 || string.IsNullOrWhiteSpace(cntxt.AccessKey)
			 || string.IsNullOrWhiteSpace(cntxt.AccountID)
			 || string.IsNullOrWhiteSpace(cntxt.AccountPassword)
			 || (cntxt.SelectedCertInfo == null)
				)
			{
				cntxt.IsExecutable = false;
			}
			else
			{
				cntxt.IsExecutable = true;
			}

			return cntxt.IsExecutable;
		}
		#endregion

	}
}
