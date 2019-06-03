using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
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
using System.Diagnostics;
using Microsoft.Win32;
using System.Windows.Threading;
using System.Collections.Specialized;
using System.Configuration;
using System.Collections.ObjectModel;

namespace TCPTEST
{
	#region バインド用データクラス
	/// <summary>
	/// バインド用データクラス
	/// </summary>
	class TcpInterface : INotifyPropertyChanged
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

		#region HTTP機能関連用変数

		private string _URIString = string.Empty;
		public string URIString
		{
			get { return _URIString; }
			set { _URIString = value; NotifyPropertyChanged(); }
		}

		private ObservableCollection<HttpInterface> _HttpUriParameters = new ObservableCollection<HttpInterface>();
		public ObservableCollection<HttpInterface> HttpUriParameters
		{
			get { return _HttpUriParameters; }
			set { _HttpUriParameters = value; NotifyPropertyChanged(); }
		}

		private HttpInterface _SelectedParameter = null;
		public HttpInterface SelectedParameter
		{
			get { return _SelectedParameter; }
			set { _SelectedParameter = value; NotifyPropertyChanged(); }
		}

		private ObservableCollection<HttpInterface> _HttpResponses = new ObservableCollection<HttpInterface>();
		public ObservableCollection<HttpInterface> HttpResponses
		{
			get { return _HttpResponses; }
			set { _HttpResponses = value; NotifyPropertyChanged(); }
		}

		private HttpInterface _SelectedResponse = null;
		public HttpInterface SelectedResponse
		{
			get { return _SelectedResponse; }
			set { _SelectedResponse = value; NotifyPropertyChanged(); }
		}

		private ObservableCollection<HttpInterface> _HttpReqParameters = new ObservableCollection<HttpInterface>();
		public ObservableCollection<HttpInterface> HttpReqParameters
		{
			get { return _HttpReqParameters; }
			set { _HttpReqParameters = value; NotifyPropertyChanged(); }
		}

		private HttpInterface _SelectedReqParameter = null;
		public HttpInterface SelectedReqParameter
		{
			get { return _SelectedReqParameter; }
			set { _SelectedReqParameter = value; NotifyPropertyChanged(); }
		}

		private string _userID = string.Empty;
		public string userID
		{
			get { return _userID; }
			set { _userID = value; NotifyPropertyChanged(); }
		}
		private string _passwd = string.Empty;
		public string passwd
		{
			get { return _passwd; }
			set { _passwd = value; NotifyPropertyChanged(); }
		}
		private string _status = string.Empty;
		public string status
		{
			get { return _status; }
			set { _status = value; NotifyPropertyChanged(); }
		}
		private string _HeaderText = string.Empty;
		public string HeaderText
		{
			get { return _HeaderText; }
			set { _HeaderText = value; NotifyPropertyChanged(); }
		}
		private string _ResponseText = string.Empty;
		public string ResponseText
		{
			get { return _ResponseText; }
			set { _ResponseText = value; NotifyPropertyChanged(); }
		}
		private string _ptime;
		public string ptime
		{
			get { return _ptime; }
			set { _ptime = value; NotifyPropertyChanged(); }
		}
		private Brush _HttpLogColor = Brushes.White;
		public Brush HttpLogColor
		{
			get { return _HttpLogColor; }
			set { _HttpLogColor = value; NotifyPropertyChanged(); }
		}

		private bool _IsBasicAuthEnabled = false;
		public bool IsBasicAuthEnabled
		{
			get { return _IsBasicAuthEnabled; }
			set { _IsBasicAuthEnabled = value; NotifyPropertyChanged(); }
		}

		private Visibility _HttpResJsonVisibility = Visibility.Collapsed;
		public Visibility HttpResJsonVisibility
		{
			get { return _HttpResJsonVisibility; }
			set { _HttpResJsonVisibility = value; NotifyPropertyChanged(); }
		}

		#endregion

		#region HTTP・メール共通の項目
		private string _clientCertSerialNo = string.Empty;
		public string ClientCertSerialNo
		{
			get { return _clientCertSerialNo; }
			set { _clientCertSerialNo = value; NotifyPropertyChanged(); }
		}

		private bool _IsCheckServerCert = false;
		public bool IsCheckServerCert
		{
			get { return _IsCheckServerCert; }
			set { _IsCheckServerCert = value; NotifyPropertyChanged(); }
		}
		private bool _IsCheckClientCert = false;
		public bool IsCheckClientCert
		{
			get { return _IsCheckClientCert; }
			set { _IsCheckClientCert = value; NotifyPropertyChanged(); }
		}
		#endregion

		#region メール機能関連用変数
		private string _mailstatus = string.Empty;
		public string MailStatus
		{
			get { return _mailstatus; }
			set { _mailstatus = value; NotifyPropertyChanged(); }
		}
		private string _SmtpServer;
		public string SmtpServer
		{
			get { return _SmtpServer; }
			set { _SmtpServer = value; NotifyPropertyChanged(); }
		}
		private int _SmtpPort;
		public int SmtpPort
		{
			get { return _SmtpPort; }
			set { _SmtpPort = value; NotifyPropertyChanged(); }
		}
		private string _SmtpUserId;
		public string SmtpUserId
		{
			get { return _SmtpUserId; }
			set { _SmtpUserId = value; NotifyPropertyChanged(); }
		}
		private string _SmtpPasswd;
		public string SmtpPasswd
		{
			get { return _SmtpPasswd; }
			set { _SmtpPasswd = value; NotifyPropertyChanged(); }
		}
		private KyoeiSystem.Framework.Net.SmtpAuthMethod _SmtpAuth = KyoeiSystem.Framework.Net.SmtpAuthMethod.None;
		public KyoeiSystem.Framework.Net.SmtpAuthMethod SmtpAuth
		{
			get { return _SmtpAuth; }
			set
			{
				_SmtpAuth = value;
				NotifyPropertyChanged();
				switch (value)
				{
				default:
					IsSmtpAuth = false;
					break;
				case KyoeiSystem.Framework.Net.SmtpAuthMethod.Login:
					IsSmtpAuth = true;
					IsSmtpAuthLogin = true;
					break;
				case KyoeiSystem.Framework.Net.SmtpAuthMethod.Plain:
					IsSmtpAuth = true;
					IsSmtpAuthPlain = true;
					break;
				case KyoeiSystem.Framework.Net.SmtpAuthMethod.CramMd5:
					IsSmtpAuth = true;
					IsSmtpAuthCramMd5 = true;
					break;
				}
			}
		}
		private List<KyoeiSystem.Framework.Net.SecureMode> _secureModeList = new List<KyoeiSystem.Framework.Net.SecureMode>()
		{
			KyoeiSystem.Framework.Net.SecureMode.None,
			KyoeiSystem.Framework.Net.SecureMode.SSL2,
			KyoeiSystem.Framework.Net.SecureMode.SSL3,
			KyoeiSystem.Framework.Net.SecureMode.TLS,
			KyoeiSystem.Framework.Net.SecureMode.STARTTLS,
		};
		public List<KyoeiSystem.Framework.Net.SecureMode> SecureModeList
		{
			get { return _secureModeList; }
			protected set { }
		}
		private KyoeiSystem.Framework.Net.SecureMode _SmtpSecureMode = KyoeiSystem.Framework.Net.SecureMode.None;
		public KyoeiSystem.Framework.Net.SecureMode SmtpSecureMode
		{
			get { return _SmtpSecureMode; }
			set { _SmtpSecureMode = value; NotifyPropertyChanged(); }
		}
		private KyoeiSystem.Framework.Net.SecureMode _PopSecureMode = KyoeiSystem.Framework.Net.SecureMode.None;
		public KyoeiSystem.Framework.Net.SecureMode PopSecureMode
		{
			get { return _PopSecureMode; }
			set { _PopSecureMode = value; NotifyPropertyChanged(); }
		}
		private bool _IsSmtpAuth = false;
		public bool IsSmtpAuth
		{
			get { return _IsSmtpAuth; }
			set { _IsSmtpAuth = value; NotifyPropertyChanged(); IsSmtpAuthEnabled = value; }
		}
		private bool _IsSmtpAuthCramMd5 = false;
		public bool IsSmtpAuthCramMd5
		{
			get { return _IsSmtpAuthCramMd5; }
			set { _IsSmtpAuthCramMd5 = value; NotifyPropertyChanged(); }
		}
		private bool _IsSmtpAuthLogin = false;
		public bool IsSmtpAuthLogin
		{
			get { return _IsSmtpAuthLogin; }
			set { _IsSmtpAuthLogin = value; NotifyPropertyChanged(); }
		}
		private bool _IsSmtpAuthPlain = false;
		public bool IsSmtpAuthPlain
		{
			get { return _IsSmtpAuthPlain; }
			set { _IsSmtpAuthPlain = value; NotifyPropertyChanged(); }
		}

		private bool _IsPopBeforeSmtp = false;
		public bool IsPopBeforeSmtp
		{
			get { return _IsPopBeforeSmtp; }
			set { _IsPopBeforeSmtp = value; NotifyPropertyChanged(); IsPopEnabled = value; }
		}
		private bool _IsPopEnabled = false;
		public bool IsPopEnabled
		{
			get { return _IsPopEnabled; }
			set { _IsPopEnabled = value; NotifyPropertyChanged(); }
		}
		private bool _IsSmtpAuthEnabled = false;
		public bool IsSmtpAuthEnabled
		{
			get { return _IsSmtpAuthEnabled; }
			set { _IsSmtpAuthEnabled = value; NotifyPropertyChanged(); }
		}
		private string _PopServer;
		public string PopServer
		{
			get { return _PopServer; }
			set { _PopServer = value; NotifyPropertyChanged(); }
		}
		private int _PopPort;
		public int PopPort
		{
			get { return _PopPort; }
			set { _PopPort = value; NotifyPropertyChanged(); }
		}
		public KyoeiSystem.Framework.Net.PopProtocol PopProtocol = KyoeiSystem.Framework.Net.PopProtocol.POP3;
		private bool _IsPOP3 = true;
		public bool IsPOP3
		{
			get { return _IsPOP3; }
			set
			{
				_IsPOP3 = value;
				NotifyPropertyChanged();
				if (value)
					PopProtocol = KyoeiSystem.Framework.Net.PopProtocol.POP3;
			}
		}
		private bool _IsIMAP = false;
		public bool IsIMAP
		{
			get { return _IsIMAP; }
			set
			{
				_IsPOP3 = value;
				NotifyPropertyChanged();
				if (value)
					PopProtocol = KyoeiSystem.Framework.Net.PopProtocol.IMAP;
			}
		}
		public KyoeiSystem.Framework.Net.PopAuthMethod PopAuthMethod = KyoeiSystem.Framework.Net.PopAuthMethod.Standard;
		private bool _IsPopAuthStd = true;
		public bool IsPopAuthStd
		{
			get { return _IsPopAuthStd; }
			set
			{
				_IsPopAuthStd = value;
				NotifyPropertyChanged();
				if (value)
					PopAuthMethod = KyoeiSystem.Framework.Net.PopAuthMethod.Standard;
			}
		}
		private bool _IsPopAuthCramMd5 = false;
		public bool IsPopAuthCramMd5
		{
			get { return _IsPopAuthCramMd5; }
			set
			{
				_IsPopAuthCramMd5 = value;
				NotifyPropertyChanged();
				if (value)
					PopAuthMethod = KyoeiSystem.Framework.Net.PopAuthMethod.CramMd5;
			}
		}
		private bool _IsPopAuthAPOP = false;
		public bool IsPopAuthAPOP
		{
			get { return _IsPopAuthAPOP; }
			set
			{
				_IsPopAuthAPOP = value;
				NotifyPropertyChanged();
				if (value)
					PopAuthMethod = KyoeiSystem.Framework.Net.PopAuthMethod.APOP;
			}
		}
		private bool _IsPopAuthNTLM = false;
		public bool IsPopAuthNTLM
		{
			get { return _IsPopAuthNTLM; }
			set
			{
				_IsPopAuthNTLM = value;
				NotifyPropertyChanged();
				if (value)
					PopAuthMethod = KyoeiSystem.Framework.Net.PopAuthMethod.NTLM;
			}
		}
		private string _PopUserId;
		public string PopUserId
		{
			get { return _PopUserId; }
			set { _PopUserId = value; NotifyPropertyChanged(); }
		}
		private string _PopPasswd;
		public string PopPasswd
		{
			get { return _PopPasswd; }
			set { _PopPasswd = value; NotifyPropertyChanged(); }
		}
		private int _PopDelayTime = 300;
		public int PopDelayTime
		{
			get { return _PopDelayTime; }
			set { _PopDelayTime = value; NotifyPropertyChanged(); }
		}

		private string _mailTo;
		public string MailTo
		{
			get { return _mailTo; }
			set { _mailTo = value; NotifyPropertyChanged(); }
		}
		private string _mailCc;
		public string MailCc
		{
			get { return _mailCc; }
			set { _mailCc = value; NotifyPropertyChanged(); }
		}
		private string _mailFrom;
		public string MailFrom
		{
			get { return _mailFrom; }
			set { _mailFrom = value; NotifyPropertyChanged(); }
		}
		private string _mailSender;
		public string MailSender
		{
			get { return _mailSender; }
			set { _mailSender = value; NotifyPropertyChanged(); }
		}
		private string _mailBcc;
		public string MailBcc
		{
			get { return _mailBcc; }
			set { _mailBcc = value; NotifyPropertyChanged(); }
		}
		private string _MailReplyTo;
		public string MailReplyTo
		{
			get { return _MailReplyTo; }
			set { _MailReplyTo = value; NotifyPropertyChanged(); }
		}
		private string _mailSubject;
		public string MailSubject
		{
			get { return _mailSubject; }
			set { _mailSubject = value; NotifyPropertyChanged(); }
		}
		private string[] _mailFiles;
		public string[] MailFiles
		{
			get { return _mailFiles; }
			set { _mailFiles = value; NotifyPropertyChanged(); }
		}
		private string _mailbodytext;
		public string MailBodyText
		{
			get { return _mailbodytext; }
			set { _mailbodytext = value; NotifyPropertyChanged(); }
		}
		private Brush _MailLogColor = Brushes.White;
		public Brush MailLogColor
		{
			get { return _MailLogColor; }
			set { _MailLogColor = value; NotifyPropertyChanged(); }
		}
		private string _maillog;
		public string MailLog
		{
			get { return _maillog; }
			set { _maillog = value; NotifyPropertyChanged(); }
		}

		public string errors = string.Empty;

		#endregion

		// コンストラクタ
		public TcpInterface()
		{
		}
	}
	#endregion

	#region INotifyPropertyChanged メンバー
	public class HttpInterface : INotifyPropertyChanged
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

		private int _index = 0;
		public int INDEX
		{
			get { return _index; }
			set { _index = value; NotifyPropertyChanged(); }
		}

		private string _key = null;
		public string KEY
		{
			get { return _key; }
			set { _key = value; NotifyPropertyChanged(); }
		}

		private string _value = null;
		public string VALUE
		{
			get { return _value; }
			set { _value = value; NotifyPropertyChanged(); }
		}

	}
	#endregion

	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		TcpInterface contxt = new TcpInterface();

		private bool inproc = false;

		#region HTTP通信テスト用
		string LocalDirBase = @"c:\temp\http\";
		string GetFileDir = string.Empty;
		string PutFileDir = string.Empty;

		string downloadUriBase = string.Empty;
		string uploadUri = string.Empty;
		string account = string.Empty;
		string haikiFile = string.Empty;
		string torimodosiFile = string.Empty;
		string haikiLocal = string.Empty;
		string torimodosiLocal = string.Empty;
		string putFileLocal = string.Empty;
		string putFile = string.Empty;
		string accessToken = string.Empty;
		#endregion

		#region MyNumberテスト用
		MyNumberAPIConfig mynumcfg = null;
		MyNumberAPI mynumApi = null;
		#endregion

		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = contxt;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			string[] cmds = System.Environment.GetCommandLineArgs();

			// HTTPテスト用
			setupHttpTest();

			// メールテスト用
			setupMailTest();

			// 時刻表示用
			DispatcherTimer timer = new DispatcherTimer();
			timer.Interval = new TimeSpan(0, 0, 0, 0, 300);
			timer.Tick += new EventHandler(Time_Tick);
			timer.Start();
		}

		#region HTTPテスト用
		private void setupHttpTest()
		{
			GetFileDir = LocalDirBase + @"get\";
			PutFileDir = LocalDirBase + @"put\";
			contxt.URIString = @"https://mcss-demo.obc-service.biz/api/Authentication/CertificateToken";
			contxt.ClientCertSerialNo = @"";
			contxt.IsCheckClientCert = false;

			contxt.HttpUriParameters.Clear();
			contxt.HttpUriParameters = new ObservableCollection<HttpInterface>()
			{
				new HttpInterface() { KEY = "tenant_key", VALUE = "j4izysfjyr2h81uj140aklmpeazqevou32vbf3gguar70fpfkpeclq0yhsk3" },
			};
			contxt.HttpReqParameters.Clear();
			contxt.HttpReqParameters.Add(new HttpInterface() { KEY = "access_key", VALUE = "hj8FVVdxECNmvsfQj376uVtWfmN9subEhuuXstVFExFkfvSFcT8iZ5UmSbsA957bs953XipS7ssWg8HKu8WymTFez87g3mxA", });
			contxt.HttpReqParameters.Add(new HttpInterface() { KEY = "useraccount", VALUE = "kyoei", });
			contxt.HttpReqParameters.Add(new HttpInterface() { KEY = "password", VALUE = "xiTwGb", });
			RefleshGridIndex();

			mynumcfg = new MyNumberAPIConfig()
			{
				ClientCertSerialNo = contxt.ClientCertSerialNo,
				TenantKey = "j4izysfjyr2h81uj140aklmpeazqevou32vbf3gguar70fpfkpeclq0yhsk3",
				AccessKey = "hj8FVVdxECNmvsfQj376uVtWfmN9subEhuuXstVFExFkfvSFcT8iZ5UmSbsA957bs953XipS7ssWg8HKu8WymTFez87g3mxA",
				AccountID = "kyoei",
				AccountPassword = "xiTwGb",
			};
			mynumApi = new MyNumberAPI(mynumcfg);

		}
		#endregion

		#region Mailテスト用
		private void setupMailTest()
		{
			contxt.SmtpServer = "SMTPサーバー";
			contxt.SmtpPort = 465;
			contxt.SmtpSecureMode = KyoeiSystem.Framework.Net.SecureMode.TLS;
			contxt.SmtpAuth = KyoeiSystem.Framework.Net.SmtpAuthMethod.Login;
			contxt.SmtpUserId = "SMTPユーザ";
			contxt.IsPopBeforeSmtp = false;
			contxt.PopServer = "POPサーバー";
			contxt.PopPort = 995;
			contxt.PopSecureMode = KyoeiSystem.Framework.Net.SecureMode.TLS;
			contxt.PopAuthMethod = KyoeiSystem.Framework.Net.PopAuthMethod.Standard;
			contxt.PopUserId = "POPユーザ";
			contxt.IsCheckClientCert = false;
			contxt.ClientCertSerialNo = @"";

			contxt.MailFrom = "xxxx@gmail.com";
			contxt.MailTo = "";
			contxt.MailCc = "";
			contxt.MailBcc = "";
			contxt.MailReplyTo = "";
			contxt.MailSender = "";
			contxt.MailSubject = "サブジェクト";
			contxt.MailBodyText = "てすと本文\r\n\r\nほんほん\r\n.\r\nぶんぶん";
		}
		#endregion

		private void Time_Tick(object sender, EventArgs e)
		{
			this.contxt.ptime = DateTime.Now.ToString("HH:mm:ss");
		}

		#region クライアント証明書選択
		/// <summary>
		/// クライアント証明書選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NyNumCert_Click(object sender, RoutedEventArgs e)
		{
			CertListWindow certw = new CertListWindow();
			if (certw.ShowDialog() == true)
			{
				contxt.ClientCertSerialNo = certw.SelectedCertSerialNo;
				this.mynumcfg.ClientCertSerialNo = contxt.ClientCertSerialNo;
			}
		}
		#endregion

		#region HTTP機能関連

		#region Webページ取得
		private void Brows_Click(object sender, RoutedEventArgs e)
		{
			if (inproc)
			{
				MessageBox.Show("HTTP通信処理中");
				return;
			}
			try
			{
				inproc = true;
				contxt.HttpResponses.Clear();

				contxt.HttpLogColor = Brushes.White;
				contxt.ResponseText = "* 通信中 *";
				contxt.HeaderText = string.Empty;

				contxt.passwd = contxt.IsBasicAuthEnabled ? htmlPasswd.Password : string.Empty;
				contxt.status = string.Empty;
				DateTime p0 = DateTime.Now;
				DateTime p1 = DateTime.Now;

				var prmlist = (from p in contxt.HttpUriParameters
							   where string.IsNullOrWhiteSpace(p.KEY) != true
							   select p.KEY + (string.IsNullOrWhiteSpace(p.VALUE) ? string.Empty : ("=" + p.VALUE))
							   ).ToArray();


				Task.Run(() =>
				{
					try
					{
						Dispatcher dsp = this.Dispatcher;
						contxt.status = "receiving...";
						using (KyoeiSystem.Framework.Net.Http http = new KyoeiSystem.Framework.Net.Http())
						{
							var res = http.GetContents(URI: contxt.URIString, paramlist: prmlist, certname: contxt.ClientCertSerialNo, userid: contxt.userID, passwd: contxt.passwd);
							p1 = DateTime.Now;
							TimeSpan ts0 = p1 - p0;
							contxt.status = res.status;
							if ((res.status == "OK") || (res.status == "Created"))
							{
								foreach (var item in res.header)
								{
									contxt.HeaderText += string.Format("{0}:{1}\r\n", item.Key, item.Value);
								}
								contxt.ResponseText = res.contentsText;
								if (res.header.ContainsKey("Content-Type") && res.header["Content-Type"].Contains("/json"))
								{
									var json = KyoeiSystem.Framework.Net.Json.ToData<CertificateTokenResponse>(res.contentsText);
									var data = new Dictionary<string, string>();
									data.Add("certificate_token", json.certificate_token);
									dsp.BeginInvoke(new WriteJsonCollectionDelegate(WriteJsonCollection), data);
								}
								// 成功
								contxt.HttpLogColor = Brushes.LightBlue;
							}
							else
							{
								contxt.ResponseText = string.Format("* 通信時間：{0}msec\r\n{1}", ts0.TotalMilliseconds, res.errors);
								// 失敗
								contxt.HttpLogColor = Brushes.Yellow;
							}
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
					finally
					{
						inproc = false;
					}
				});
			}
			catch (Exception ex)
			{
				contxt.ResponseText.Insert(0, ex.Message);
				inproc = false;
			}
		}

		delegate void WriteJsonCollectionDelegate(Dictionary<string, string> data);
		private void WriteJsonCollection(Dictionary<string, string> data)
		{
			foreach (var item in data)
			{
				contxt.HttpResponses.Add(new HttpInterface() { KEY = item.Key, VALUE = item.Value, });
			}
		}
		#endregion

		#region ファイルダウンロード
		private void FileGet_Click(object sender, RoutedEventArgs e)
		{
			if (inproc)
			{
				MessageBox.Show("HTTP通信処理中");
				return;
			}
			try
			{
				inproc = true;
				contxt.HttpResponses.Clear();

				contxt.HttpLogColor = Brushes.White;
				contxt.ResponseText = "* 通信中 *";
				contxt.HeaderText = string.Empty;

				contxt.passwd = htmlPasswd.Password;
				contxt.status = string.Empty;

				DateTime p0 = DateTime.Now;
				DateTime p1 = DateTime.Now;
				DateTime p2 = DateTime.Now;

				Task.Run(() =>
				{
					try
					{
						using (KyoeiSystem.Framework.Net.Http http = new KyoeiSystem.Framework.Net.Http())
						{
							contxt.status = "receiving...";
							// 指定URIからファイルを受信する
							var res = http.GetContents(URI: contxt.URIString, paramlist: null, certname: contxt.ClientCertSerialNo, userid: contxt.userID, passwd: contxt.passwd);
							p1 = DateTime.Now;
							contxt.status = res.status;
							if ((res.status == "OK") || (res.status == "Created"))
							{
								foreach (var item in res.header)
								{
									contxt.HeaderText += string.Format("{0}:{1}\r\n", item.Key, item.Value);
								}

								// 受信データをファイルに保存
								var file = this.GetFileDir + res.uri.LocalPath;
								FileInfo fi = new FileInfo(file);
								if (!fi.Directory.Exists)
								{
									fi.Directory.Create();
								}
								using (BinaryWriter bw = new BinaryWriter(new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write)))
								{
									bw.Write(res.contntsBin);
								}
								p2 = DateTime.Now;
								TimeSpan ts0 = p1 - p0;
								TimeSpan ts1 = p2 - p1;
								contxt.ResponseText = string.Format("* 受信ファイルを保存しました。\r\n* {0}\r\n* {1} bytes  通信時間：{2}msec  ファイル書き込み：{3}msec", fi.FullName, res.contntsBin.Length, ts0.TotalMilliseconds, ts1.TotalMilliseconds);
								// 成功
								contxt.HttpLogColor = Brushes.LightBlue;
							}
							else
							{
								TimeSpan ts0 = p1 - p0;
								contxt.ResponseText = string.Format("* 通信時間：{0}msec\r\n{1}", ts0.TotalMilliseconds, res.errors);
								// 失敗
								contxt.HttpLogColor = Brushes.Yellow;
							}
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
					finally
					{
						inproc = false;
					}
				});
			}
			catch (Exception ex)
			{
				contxt.ResponseText = ex.Message;
				inproc = false;
			}
		}
		#endregion

		#region ファイルアップロード
		private void FilePut_Click(object sender, RoutedEventArgs e)
		{
			if (inproc)
			{
				MessageBox.Show("HTTP通信処理中");
				return;
			}
			try
			{
				inproc = true;
				contxt.HttpResponses.Clear();

				contxt.HttpLogColor = Brushes.White;
				contxt.ResponseText = "* 通信中 *";
				contxt.HeaderText = string.Empty;

				contxt.passwd = htmlPasswd.Password;
				OpenFileDialog fd = new OpenFileDialog();
				if (fd.ShowDialog() != true)
				{
					contxt.ResponseText = "中止しました";
					inproc = false;
					return;
				}

				// 受信データをファイルに保存
				var file = fd.FileName;
				FileInfo fi = new FileInfo(file);
				if (!fi.Exists)
				{
					MessageBox.Show(string.Format("ファイル{0}が存在しません。", fi.FullName));
					contxt.ResponseText = "中止しました";
					inproc = false;
					return;
				}

				var prmlist = (from p in contxt.HttpUriParameters
							   where string.IsNullOrWhiteSpace(p.KEY) != true
							   select p.KEY + (string.IsNullOrWhiteSpace(p.VALUE) ? string.Empty : ("=" + p.VALUE))
							   ).ToArray();

				contxt.status = string.Empty;
				DateTime p1 = DateTime.Now;
				DateTime p2 = DateTime.Now;

				Task.Run(() =>
				{
					try
					{
						using (KyoeiSystem.Framework.Net.Http http = new KyoeiSystem.Framework.Net.Http())
						{
							contxt.status = "sending...";
							// 指定URIへファイルを送信する
							var res = http.Upload(URI: contxt.URIString, localfile: fi.FullName, paramlist: prmlist, certname: contxt.ClientCertSerialNo, userid: contxt.userID, passwd: contxt.passwd);
							p2 = DateTime.Now;
							contxt.status = res.statusCode.ToString();
							if (res.status == "OK")
							{
								foreach (var item in res.header)
								{
									contxt.HeaderText += string.Format("{0}:{1}\r\n", item.Key, item.Value);
								}
								TimeSpan ts = p2 - p1;

								contxt.ResponseText = string.Format("* ファイルを送信しました。\r\n* {0}\r\n* {1} bytes  通信時間：{2}msec\r\n", fi.FullName, fi.Length, ts.TotalMilliseconds);
								contxt.ResponseText += res.contentsText;
								// 成功
								contxt.HttpLogColor = Brushes.LightBlue;
							}
							else
							{
								contxt.ResponseText = "* " + res.errors;
								// 失敗
								contxt.HttpLogColor = Brushes.Yellow;
							}
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
					finally
					{
						inproc = false;
					}
				});

			}
			catch (Exception ex)
			{
				contxt.ResponseText = ex.Message;
				inproc = false;
			}
		}
				
		#endregion

		#region メッセージPOST
		private void POST_Click(object sender, RoutedEventArgs e)
		{
			if (inproc)
			{
				MessageBox.Show("HTTP通信処理中");
				return;
			}
			try
			{
				inproc = true;
				Dispatcher dsp = this.Dispatcher;

				contxt.HttpLogColor = Brushes.White;
				contxt.ResponseText = "* 通信中 *";
				contxt.HeaderText = string.Empty;

				contxt.status = string.Empty;
				DateTime p1 = DateTime.Now;
				DateTime p2 = DateTime.Now;
				var prmlist = (from p in contxt.HttpUriParameters
							   where string.IsNullOrWhiteSpace(p.KEY) != true
							   select p.KEY + (string.IsNullOrWhiteSpace(p.VALUE) ? string.Empty : ("=" + p.VALUE))
							   ).ToArray();
				Task.Run(() =>
				{
					try
					{
						var data = new AccessTokenRequest();
						foreach (var item in contxt.HttpReqParameters)
						{
							switch (item.KEY)
							{
							case "access_key":
								data.access_key = item.VALUE;
								break;
							case "useraccount":
								data.useraccount = item.VALUE;
								break;
							case "password":
								data.password = item.VALUE;
								break;
							case "certificate_token":
								data.certificate_token = item.VALUE;
								break;
							}
						}
						string postmsg = KyoeiSystem.Framework.Net.Json.FromData<AccessTokenRequest>(data);
						using (KyoeiSystem.Framework.Net.Http http = new KyoeiSystem.Framework.Net.Http())
						{
							contxt.status = "sending...";
							// 指定URIへファイルを送信する
							var headers = new Dictionary<string, string>();
							headers.Add("Content-Type", "application/json");

							var res = http.CallPost(URI: contxt.URIString, paramlist: prmlist, postMsg: postmsg, headers: headers, certname: contxt.ClientCertSerialNo, userid: contxt.userID, passwd: contxt.passwd);
							p2 = DateTime.Now;
							contxt.status = res.statusCode.ToString();
							if ((res.status == "OK") || (res.status == "Created"))
							{
								foreach (var item in res.header)
								{
									contxt.HeaderText += string.Format("{0}:{1}\r\n", item.Key, item.Value);
								}
								TimeSpan ts = p2 - p1;

								if (res.header.ContainsKey("Content-Type") && res.header["Content-Type"].Contains("/json"))
								{
									var json = KyoeiSystem.Framework.Net.Json.ToData<AccessTokenResponse>(res.contentsText);
									var list = new Dictionary<string, string>();
									list.Add("access_token", json.access_token);
									dsp.BeginInvoke(new WriteJsonCollectionDelegate(WriteJsonCollection), list);
								}
								contxt.ResponseText = res.contentsText;
								// 成功
								contxt.HttpLogColor = Brushes.LightBlue;
							}
							else
							{
								contxt.ResponseText = "* " + res.errors;
								// 失敗
								contxt.HttpLogColor = Brushes.Yellow;
							}
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
					finally
					{
						inproc = false;
					}
				});

			}
			catch (Exception ex)
			{
				contxt.ResponseText = ex.Message;
				inproc = false;
			}
		}

		#endregion

		#region グリッド処理
		private void ParamDelete_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var cur = contxt.SelectedParameter;
				if (cur != null)
				{
					contxt.HttpUriParameters.Remove(cur);
					RefleshGridIndex();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void ParamAdd_Click(object sender, RoutedEventArgs e)
		{
			contxt.HttpUriParameters.Add(new HttpInterface() { });
			RefleshGridIndex();
		}

		private void ReqParamDelete_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var cur = contxt.SelectedReqParameter;
				if (cur != null)
				{
					contxt.HttpReqParameters.Remove(cur);
					RefleshGridIndex();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void ReqParamAdd_Click(object sender, RoutedEventArgs e)
		{
			contxt.HttpReqParameters.Add(new HttpInterface() { });
			RefleshGridIndex();
		}

		private void RefleshGridIndex()
		{
			foreach (var item in contxt.HttpUriParameters)
			{
				item.INDEX = contxt.HttpUriParameters.IndexOf(item) + 1;
			}
			foreach (var item in contxt.HttpReqParameters)
			{
				item.INDEX = contxt.HttpReqParameters.IndexOf(item) + 1;
			}
		}

		private void HTTPTEST_Click(object sender, RoutedEventArgs e)
		{
			setupHttpTest();
		}

		private void resText_Checked(object sender, RoutedEventArgs e)
		{
			contxt.HttpResJsonVisibility = System.Windows.Visibility.Collapsed;
		}

		private void resText_UnChecked(object sender, RoutedEventArgs e)
		{
			contxt.HttpResJsonVisibility = System.Windows.Visibility.Visible;
		}

		private void CopyParam_Click(object sender, RoutedEventArgs e)
		{
			if (contxt.SelectedResponse == null)
			{
				MessageBox.Show("選択されていません");
				return;
			}
			contxt.HttpReqParameters.Add(new HttpInterface() { INDEX = contxt.HttpReqParameters.Count + 1, KEY = contxt.SelectedResponse.KEY, VALUE = contxt.SelectedResponse.VALUE, });
			RefleshGridIndex();
		}
		#endregion

		#region マイナンバーAPIテスト
		/// <summary>
		/// MuNumberTEST1
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NyNumTest1_Click(object sender, RoutedEventArgs e)
		{
			contxt.HeaderText = string.Empty;
			contxt.ResponseText = string.Empty;
			this.mynumApi = null;
			try
			{
				this.mynumApi = new MyNumberAPI(this.mynumcfg);
				this.mynumApi.UriBase = "https://mcss-demo.obc-service.biz/";
				this.mynumApi.UriPathCertificateToken = "api/Authentication/CertificateToken";
				this.mynumApi.UriPathAccessToken = "api/Authentication/accessToken";
				//this.mynumApi.UriPathCorpInfo = "api/Corp/CorpInfo";
				this.mynumApi.UriPathEmployees = "api/HumanResource/Employees";
				this.mynumApi.UriPathFamilies = "api/HumanResource/Families";
				//this.mynumApi.UriPathUser = "api/Authentication/Users";

				var emps = this.mynumApi.GetEmployeeList(true);
				var families = this.mynumApi.GetFamilies();
				contxt.ResponseText = this.mynumApi.ApiLog.ToString();
				this.mynumApi = null;
			}
			catch (Exception ex)
			{
				if (mynumApi != null)
				{
					contxt.ResponseText = this.mynumApi.ApiLog.ToString();
				}
				MessageBox.Show(ex.Message + (ex.InnerException != null ? "\r\n" + ex.InnerException.Message : string.Empty));
			}
			finally
			{
			}
		}

		/// <summary>
		/// MuNumberTEST2
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NyNumTest2_Click(object sender, RoutedEventArgs e)
		{
			//this.mynumApi = new MyNumberAPI(this.mynumcfg);

			//int i = 40;
			//for (int j = 0; j < 12; j++)
			//{
			//	int max = i + 10;
			//	List<Employee> empdata = new List<Employee>();
			//	for (; i < max; i++)
			//	{
			//		var item = new Employee()
			//		{
			//			matchingid = string.Format("mid{0:D04}", i),
			//			usertype = "0",
			//			individualnumbersubmitmethod = "1",
			//			useraccount = string.Format("user{0:D04}", i),
			//			employeeno = string.Format("emp{0:D04}", i),
			//			name = Microsoft.VisualBasic.Strings.StrConv(string.Format("従業員{0:D04}", i), Microsoft.VisualBasic.VbStrConv.Wide),
			//		};
			//		empdata.Add(item);
			//	}
			//	var uinfo = this.mynumApi.AddEmployee(empdata);
			//	contxt.ResponseText = this.mynumApi.ApiLog;
			//}
		}

		/// <summary>
		/// MuNumberTEST3
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NyNumTest3_Click(object sender, RoutedEventArgs e)
		{
			//try
			//{
			//	this.mynumApi = new MyNumberAPI(this.mynumcfg);
			//	UserAddRequest userdata = new UserAddRequest()
			//	{
			//		users = new Users[]
			//		{
			//			 new Users(){
			//				usertype = "0",
			//				useraccount = "testuser3",
			//				userpassword = "testPass3",
			//				name = "mitsuyama3",
			//				namekana = "ミツママ",
			//				mailaddress = "",
			//				isvalid = "",
			//				isaccountvalidlimit = "",
			//				accountvalidlimit = "",
			//				targettype = "",
			//				employeetargetgrouptype = "",
			//				employeetargetgroupname = "",
			//				individualpersonpayeetargetgrouptype = "",
			//				individualpersonpayeetargetgroupname = "",
			//			 },
			//		},
			//	};
			//	var uinfo = this.mynumApi.AddUser(userdata);
			//	contxt.ResponseText = this.mynumApi.ApiLog;
			//}
			//catch (Exception ex)
			//{
			//}
			//finally
			//{
			//}

		}


		#endregion

		#endregion

		#region メール機能関連
		private void SendMail_Click(object sender, RoutedEventArgs e)
		{
			if (inproc)
			{
				MessageBox.Show("メール送信処理中");
				return;
			}

			try
			{
				inproc = true;

				contxt.MailLogColor = Brushes.White;
				contxt.MailLog = "* 通信中 *";
				Task.Run(() =>
				{
					contxt.SmtpPasswd = this.smtpPasswd.Password;
					contxt.PopPasswd = this.popPasswd.Password;
					KyoeiSystem.Framework.Net.SmtpAuthMethod smtpauth = KyoeiSystem.Framework.Net.SmtpAuthMethod.None;
					if (contxt.IsSmtpAuth)
					{
						if (contxt.IsSmtpAuthCramMd5)
						{
							smtpauth = KyoeiSystem.Framework.Net.SmtpAuthMethod.CramMd5;
						}
						else if (contxt.IsSmtpAuthLogin)
						{
							smtpauth = KyoeiSystem.Framework.Net.SmtpAuthMethod.Login;
						}
						else if (contxt.IsSmtpAuthPlain)
						{
							smtpauth = KyoeiSystem.Framework.Net.SmtpAuthMethod.Plain;
						}
					}

					KyoeiSystem.Framework.Net.Mail mail = new KyoeiSystem.Framework.Net.Mail();
					KyoeiSystem.Framework.Net.MailParameters prms = new KyoeiSystem.Framework.Net.MailParameters()
					{
						SmtpServer = contxt.SmtpServer,
						SmtpPort = contxt.SmtpPort,
						SmtpSecureMode = contxt.SmtpSecureMode,
						SmtpAuth = smtpauth,
						SmtpUserId = contxt.SmtpUserId,
						SmtpPasswd = contxt.SmtpPasswd,
						IsPopBeforeSmtp = contxt.IsPopBeforeSmtp,
						PopServer = contxt.IsPopBeforeSmtp ? contxt.PopServer : string.Empty,
						PopPort = contxt.IsPopBeforeSmtp ? contxt.PopPort : 0,
						PopSecureMode = contxt.PopSecureMode,
						PopProtocol = contxt.PopProtocol,
						PopAuth = contxt.PopAuthMethod,
						PopUserId = contxt.IsPopBeforeSmtp ? contxt.PopUserId : string.Empty,
						PopPasswd = contxt.IsPopBeforeSmtp ? contxt.PopPasswd : string.Empty,
						PopDelayTime = contxt.IsPopBeforeSmtp ? contxt.PopDelayTime : 0,
						IsServerCertValidate = contxt.IsCheckServerCert,
						IsClientCertValidate = contxt.IsCheckClientCert,
						ClientCertSerialNo = contxt.ClientCertSerialNo,

						From = contxt.MailFrom,
						To = string.IsNullOrWhiteSpace(contxt.MailTo) ? new string[] { } : contxt.MailTo.Split(new char[] { ',', }, StringSplitOptions.RemoveEmptyEntries),
						Cc = string.IsNullOrWhiteSpace(contxt.MailCc) ? new string[] { } : contxt.MailCc.Split(new char[] { ',', }, StringSplitOptions.RemoveEmptyEntries),
						Bcc = string.IsNullOrWhiteSpace(contxt.MailBcc) ? new string[] { } : contxt.MailBcc.Split(new char[] { ',', }, StringSplitOptions.RemoveEmptyEntries),
						Subject = contxt.MailSubject,
						ReplyTo = contxt.MailReplyTo,
						Sender = contxt.MailSender,
						Body = contxt.MailBodyText,
						Files = contxt.MailFiles,
						//ExtHeaders = new string[] { "X-Mailer: KyoeiSystem.Framework.Net.Mail", },
					};
					bool result = mail.SendMail(prms);
					if (result)
					{
						// 成功
						contxt.MailLogColor = Brushes.LightBlue;
					}
					else
					{
						// 失敗
						contxt.MailLogColor = Brushes.Yellow;
					}
					contxt.MailLog = prms.status + "\r\n" + prms.logs;
					inproc = false;
				});
			}
			catch (Exception)
			{
				inproc = false;
			}
			finally
			{
			}
		}

		private void MailFile_PreviewDragEnter(object sender, DragEventArgs e)
		{
			// ファイルをドロップされた場合のみ e.Handled を True にする
			if (sender is TextBox)
			{
				if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop, true))
				{
					e.Effects = System.Windows.DragDropEffects.Copy;
				}
				else
				{
					e.Effects = System.Windows.DragDropEffects.None;
				}
				e.Handled = false;
			}
			else
			{
				e.Handled = e.Data.GetDataPresent(DataFormats.FileDrop);
			}
		}

		private void MailFile_Drop(object sender, DragEventArgs e)
		{
			string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
			if (files != null)
			{
				List<string> curfiles = new List<string>();
				if (contxt.MailFiles != null)
				{
					curfiles.AddRange(contxt.MailFiles);
				}
				foreach (var file in files)
				{
					if (curfiles.Contains(file) != true)
					{
						curfiles.Add(file);
					}
				}
				contxt.MailFiles = curfiles.ToArray();
			}
		}

		private void btnCertFile_Click(object sender, RoutedEventArgs e)
		{
			CertListWindow certw = new CertListWindow();
			if (certw.ShowDialog() == true)
			{
				contxt.ClientCertSerialNo = certw.SelectedCertSerialNo;
			}

		}

		private void btnFileClear_Click(object sender, RoutedEventArgs e)
		{
			contxt.MailFiles = new string[] { };
		}

		#endregion


	}
}
