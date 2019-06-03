using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;

using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Net;

namespace TCPTEST
{
	#region Json形式のデータに対するシリアライザを使う場合に使用するクラス群
	// ■ Json形式のデータに対するシリアライザを使う場合に使用するクラス群

	// オービックのAPIで使用するデータの項目名は辞書化されている
	// 主要な項目は以下のとおり。
	// ・personid         ：個人ID（オービック側がマッチングIDと紐付けるために発行したID（新規登録時に返される値））
	// ・matchingid       ：マッチングID（API利用元：ユーザが発行した任意の文字列）
	// ・individualnumber ：個人番号（マイナンバー12桁）

	#region 認証用インターフェース
	[System.Runtime.Serialization.DataContractAttribute()]
	public partial class CertificateTokenResponse : IJsonData
	{

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string certificate_token;
	}

	[System.Runtime.Serialization.DataContractAttribute()]
	public partial class AccessTokenRequest : IJsonData
	{
		[System.Runtime.Serialization.DataMemberAttribute()]
		public string certificate_token;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string access_key;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string useraccount;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string password;
	}

	[System.Runtime.Serialization.DataContractAttribute()]
	public partial class AccessTokenResponse : IJsonData
	{

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string access_token;
	}

	#endregion

	#region 会社情報
	//[System.Runtime.Serialization.DataContractAttribute()]
	//public partial class CorpInfoResponse : IJsonData
	//{

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string corporatename;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string identitykey;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string sendername;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string confirmationcount;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string employeeidentitypictureattachedflag;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string mateindividualnumberpictureattachedflag;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string mateidentitypictureattachedflag;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string dependentindividualnumberpictureattachedflag;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string individualpersonpayeeidentitypictureattachedflag;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string isautocreatepassword;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string validlimit;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string managementuppercount;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string managementcount;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string employeemanagementcount;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string individualpersonpayeemanagementcount;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string individualnumberkeepingcount;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string employeeindividualnumberkeepingcount;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string individualpersonpayeeindividualnumberkeepingcount;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string certificateissuecount;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string certificatecount;
	//}
	#endregion

	#region ユーザ一情報関連
	//// ユーザー情報一覧応答
	//[System.Runtime.Serialization.DataContractAttribute()]
	//public partial class UserListResponse : IJsonData
	//{

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string nextcursor;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public Users[] users;
	//}

	//// ユーザ新規登録要求
	//[System.Runtime.Serialization.DataContractAttribute()]
	//public partial class UserAddRequest : IJsonData
	//{
	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public Users[] users;
	//}

	//// ユーザー情報
	//[System.Runtime.Serialization.DataContractAttribute(Name = "users")]
	//public partial class Users : IJsonData
	//{

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string userid;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string userpassword;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string usertype;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string useraccount;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string name;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string namekana;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string mailaddress;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string isvalid;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string islockout;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string isaccountvalidlimit;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string accountvalidlimit;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string targettype;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string employeetargetgrouptype;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string employeetargetgroupname;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string individualpersonpayeetargetgrouptype;

	//	[System.Runtime.Serialization.DataMemberAttribute()]
	//	public string individualpersonpayeetargetgroupname;
	//}

	#endregion

	#region 従業員情報関連
	[System.Runtime.Serialization.DataContractAttribute()]
	public partial class EmployeeListResponse : IJsonData
	{
		[System.Runtime.Serialization.DataMemberAttribute()]
		public string nextcursor;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public Employee[] employees;
	}

	// Type created for JSON at <<root>> --> employees
	[System.Runtime.Serialization.DataContractAttribute(Name = "employees")]
	public partial class Employee : IJsonData
	{

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string personid;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string matchingid;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string usertype;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string individualnumbersubmitmethod;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string useraccount;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string userpassword;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string employeeno;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string name;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string namekana;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string mailaddress;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string belongname;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string isvalid;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string islockout;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string isaccountvalidlimit;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string accountvalidlimit;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string targettype;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string employeetargetgrouptype;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string employeetargetgroupname;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string individualpersonpayeetargetgrouptype;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string individualpersonpayeetargetgroupname;

	}

	[System.Runtime.Serialization.DataContractAttribute()]
	public partial class PersonAddResponse : IJsonData
	{
		[System.Runtime.Serialization.DataMemberAttribute()]
		public string[] personid;
	}

	#endregion

	#region 扶養家族関連
	[System.Runtime.Serialization.DataContractAttribute()]
	public partial class FamiliesListResponse : IJsonData
	{

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string nextcursor;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public Families[] families;
	}

	[System.Runtime.Serialization.DataContractAttribute(Name = "families")]
	public partial class Families : IJsonData
	{

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string employeepersonid;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string personid;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string matchingid;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string name;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string familydivision;
	}


	#endregion

	#region 個人番号データ関連
	[System.Runtime.Serialization.DataContractAttribute()]
	public partial class IndividualListResponse : IJsonData
	{
		[System.Runtime.Serialization.DataMemberAttribute()]
		public Individualnumbers[] individualnumbers;
	}

	[System.Runtime.Serialization.DataContractAttribute(Name = "individualnumbers")]
	public partial class Individualnumbers : IJsonData
	{

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string personid;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string matchingid;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string individualnumber;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string confirmername1;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string confirmationdate1;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string confirmername2;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string confirmationdate2;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string individualnumberpictureattachedflag;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string identitypictureattachedflag;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public Pictureinfos[] pictureinfos;
	}

	[System.Runtime.Serialization.DataContractAttribute(Name = "pictureinfos")]
	public partial class Pictureinfos : IJsonData
	{

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string pictureid;

		[System.Runtime.Serialization.DataMemberAttribute()]
		public string picturetype;
	}

	#endregion

	#endregion

	/// <summary>
	/// ユーザ（会社）毎に管理するユーザ固有の情報
	/// </summary>
	public class MyNumberAPIConfig
	{
		public string TenantKey { get; set; }
		public string AccessKey { get; set; }
		public string AccountID { get; set; }
		public string AccountPassword { get; set; }

		public string ClientCertSerialNo { get; set; }
	}

	/// <summary>
	/// MyNumberサービスWebAPI
	/// </summary>
	public class MyNumberAPI : IDisposable
	{
		#region 定数
		private const string constCertSerialNo = "certSerialNo";
		private const string constTenantKey = "tenantKey";
		private const string constAccessKey = "accessKey";
		private const string constAccoundId = "accoundId";
		private const string constAccountPass = "accountPass";
		private const string const参照処理 = "reffer data";
		#endregion

		#region 公開プロパティ
		public MyNumberAPIConfig ApiConfig { get; set; }
		public StringBuilder ApiLog = new StringBuilder();
		public string AccessToken { get; set; }
		#endregion

		#region 内部変数
		protected bool isLoggedOn = false;
		private HttpResult httpResult = null;

		public string UriBase { get; set; }
		public string UriPathCertificateToken { get; set; }
		public string UriPathAccessToken { get; set; }
		public string UriPathEmployees { get; set; }
		public string UriPathFamilies { get; set; }
		public string UriPathIndividuals { get; set; }
		#endregion

		public MyNumberAPI(MyNumberAPIConfig config)
		{
			this.ApiConfig = config;
		}

		#region IDisposable メンバー

		public void Dispose()
		{
		}

		#endregion

		#region Config関連
		/// <summary>
		/// Configの読込
		/// </summary>
		public void LoadConfig()
		{
			// WebAPIのURL等、API固定の情報は EXEのConfigから取得する
			//    ⇒ インストール時に上書きされる
			// 各ユーザ固有のテナントキー～パスワードはWindowsログインユーザのConfigから取得する
			//    ⇒ インストール時に上書きされない
			if (this.ApiConfig == null)
			{
				this.ApiConfig = new MyNumberAPIConfig();
			}
			var plist = (NameValueCollection)ConfigurationManager.GetSection("myNumberApiSettings");
			if (plist == null)
			{
				throw new MyNumberAPIException("アプリケーション設定ファイルが正しくセットアップされていません。");
			}
			this.UriBase = plist["UriBase"];
			if (string.IsNullOrWhiteSpace(this.UriBase))
			{
				throw new MyNumberAPIException("UriBaseが正しくセットアップされていません。");
			}
			this.UriPathCertificateToken = plist["UriPathCertificateToken"];
			if (string.IsNullOrWhiteSpace(this.UriPathCertificateToken))
			{
				throw new MyNumberAPIException("UriPathCertificateTokenが正しくセットアップされていません。");
			}
			this.UriPathAccessToken = plist["UriPathAccessToken"];
			if (string.IsNullOrWhiteSpace(this.UriPathAccessToken))
			{
				throw new MyNumberAPIException("UriPathAccessTokenが正しくセットアップされていません。");
			}
			this.UriPathEmployees = plist["UriPathEmployees"];
			if (string.IsNullOrWhiteSpace(this.UriPathEmployees))
			{
				throw new MyNumberAPIException("UriPathEmployeesが正しくセットアップされていません。");
			}
			this.UriPathFamilies = plist["UriPathFamilies"];
			if (string.IsNullOrWhiteSpace(this.UriPathFamilies))
			{
				throw new MyNumberAPIException("UriPathFamiliesが正しくセットアップされていません。");
			}
			this.UriPathIndividuals = plist["UriPathIndividuals"];
			if (string.IsNullOrWhiteSpace(this.UriPathIndividuals))
			{
				throw new MyNumberAPIException("UriPathIndividualsが正しくセットアップされていません。");
			}
			try
			{
				FileInfo fi = new System.IO.FileInfo(GetLocalConfigFilePath());
				if (fi.Exists)
				{
					ConfigXmlDocument cdoc = new ConfigXmlDocument();
					cdoc.Load(fi.FullName);
					var cfg = cdoc.SelectNodes("/configuration/settings/add");
					foreach (var item in cfg)
					{
						var key = (item as XmlElement).Attributes.GetNamedItem("key").Value;
						var val = (item as XmlElement).Attributes.GetNamedItem("value").Value;
						switch (key)
						{
						case constCertSerialNo:
							this.ApiConfig.ClientCertSerialNo = val;
							break;
						case constTenantKey:
							this.ApiConfig.TenantKey = val;
							break;
						case constAccessKey:
							this.ApiConfig.AccessKey = val;
							break;
						case constAccoundId:
							this.ApiConfig.AccountID = val;
							break;
						case constAccountPass:
							this.ApiConfig.AccountPassword = Utility.Decrypt(val);
							break;
						}
					}
					//if (string.IsNullOrWhiteSpace(this.ApiConfig.ClientCertSerialNo))
					//{
					//	throw new MyNumberAPICertException("クライアント証明書が設定されていません。");
					//}
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

		/// <summary>
		/// Configの保存（ユーザ固有情報のみ）
		/// </summary>
		public void SaveConfig()
		{
			try
			{
				FileInfo fi = new System.IO.FileInfo(GetLocalConfigFilePath());
				if (fi.Exists != true)
				{
					if (fi.Directory.Exists != true)
					{
						fi.Directory.Create();
					}
				}
				ConfigXmlDocument cdoc = new ConfigXmlDocument();
				var cfg = cdoc.CreateElement("configuration");
				var apps = cdoc.CreateElement("settings");
				Func<string, string, bool> addItem = (key, val) =>
				{
					var node = cdoc.CreateElement("add");
					var attrK = cdoc.CreateAttribute("key");
					attrK.InnerText = key;
					var attrV = cdoc.CreateAttribute("value");
					attrV.InnerText = val;
					node.Attributes.Append(attrK);
					node.Attributes.Append(attrV);
					apps.AppendChild(node);
					return true;
				};
				addItem(constCertSerialNo, ApiConfig.ClientCertSerialNo);
				addItem(constTenantKey, ApiConfig.TenantKey);
				addItem(constAccessKey, ApiConfig.AccessKey);
				addItem(constAccoundId, ApiConfig.AccountID);
				if (ApiConfig.AccountPassword != null)
				{
					addItem(constAccountPass, Utility.Encrypt(ApiConfig.AccountPassword));
				}
				cdoc.AppendChild(cfg);
				cfg.AppendChild(apps);
				cdoc.Save(fi.FullName);
			}
			catch (Exception ex)
			{
				throw new Exception("ユーザ情報の保存に失敗しました。", ex);
			}
		}

		/// <summary>
		/// Configファイルのパスを取得する
		/// </summary>
		/// <returns></returns>
		private string GetLocalConfigFilePath()
		{
			// ファイル名
			// {Systemドライブ}:\ユーザー\{ユーザ名}\AppData\Local\KyoeiSystem\MyNumber\local.config
			// 
			var dir = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			var exeasm = System.Reflection.Assembly.GetEntryAssembly();
			var assemblyTitle = exeasm.GetCustomAttributes(typeof(AssemblyTitleAttribute), true).Single() as AssemblyTitleAttribute;
			FileInfo fi = new System.IO.FileInfo(dir + @"\KyoeiSystem\" + assemblyTitle.Title + @"\local.config");

			return fi.FullName;
		}
		#endregion

		#region WebAPI実行パラメータチェック
		private const string ErrMynumberInfo = "MyNumberAPI接続情報が必要です。";
		public bool CheckConfig(bool NoExeption = true)
		{
			if (this.ApiConfig == null)
			{
				string msg = ErrMynumberInfo;
				ApiLog.AppendLine(msg);
				if (NoExeption)
					return false;
				throw new MyNumberAPIException(msg);
			}

			if (string.IsNullOrWhiteSpace(ApiConfig.TenantKey) || string.IsNullOrWhiteSpace(ApiConfig.AccessKey))
			{
				string msg = ErrMynumberInfo;
				ApiLog.AppendLine(msg);
				if (NoExeption)
					return false;
				throw new MyNumberAPIException(msg);
			}
			if (string.IsNullOrWhiteSpace(ApiConfig.AccountID) || string.IsNullOrWhiteSpace(ApiConfig.AccountPassword))
			{
				string msg = ErrMynumberInfo;
				ApiLog.AppendLine(msg);
				if (NoExeption)
					return false;
				throw new MyNumberAPIException(msg);
			}
			if (string.IsNullOrWhiteSpace(ApiConfig.ClientCertSerialNo))
			{
				string msg = "クライアント証明書が必要です。";
				ApiLog.AppendLine(msg);
				if (NoExeption)
					return false;
				throw new MyNumberAPIException(msg);
			}

			return true;
		}
		#endregion

		#region 認証手順
		/// <summary>
		/// 認証手順
		/// </summary>
		/// <returns></returns>
		private bool LogOn()
		{
			isLoggedOn = false;

			CheckConfig(false);

			// ログオン手順実行
			try
			{
				DateTime p0 = DateTime.Now;
				DateTime p1 = DateTime.Now;
				DateTime p2 = DateTime.Now;
				TimeSpan ts;

				ApiLog.Clear();

				string reqmsg = string.Empty;

				using (KyoeiSystem.Framework.Net.Http http = new KyoeiSystem.Framework.Net.Http())
				{
					ApiLog.AppendLine("<CertificateToken>");

					var prmlist = new string[] { "tenant_key=" + ApiConfig.TenantKey };

					CertificateTokenResponse certTkn = null;
					AccessTokenResponse accssTkn = null;

					string uri = this.UriBase + this.UriPathCertificateToken;
					ApiLog.AppendLine(uri);

					var headers = new Dictionary<string, string>();
					headers.Add("Content-Type", "application/json");

					// クライアント証明書が必要（オービックの場合はここだけで良い：他の呼び出しで指定してもエラーにはならない）
					// オービックから発行されたクライアント証明書を事前にインストールしておく必要がある
					httpResult = http.GetContents(URI: uri, paramlist: prmlist, headers: headers, certname: ApiConfig.ClientCertSerialNo);

					p1 = DateTime.Now;
					ts = p1 - p0;
					string status = httpResult.status;
					ApiLog.AppendFormat("{1}\r\n 通信時間：{0}msec\r\n", ts.TotalMilliseconds, status);
					switch (httpResult.statusCode)
					{
					case System.Net.HttpStatusCode.Created:
						if (httpResult.header.ContainsKey("Content-Type") && httpResult.header["Content-Type"].Contains("/json"))
						{
							certTkn = KyoeiSystem.Framework.Net.Json.ToData<CertificateTokenResponse>(httpResult.contentsText);
						}
						ApiLog.AppendFormat("{0}\r\n", httpResult.contentsText);
						break;
					case System.Net.HttpStatusCode.Forbidden:
						// ほぼ証明書エラー
						ApiLog.AppendFormat("{0}\r\n", httpResult.errors);
						throw new MyNumberAPIException("マイナンバーシステムへの接続を拒否されました。");
					default:
						ApiLog.AppendFormat("{0}\r\n", httpResult.errors);
						throw new MyNumberAPIException("マイナンバーシステムへの接続に失敗しました。");
					}

					if (certTkn != null)
					{
						// accesstokenを取得できた場合
						ApiLog.AppendLine("<AccessToken>");

						var data = new AccessTokenRequest()
						{
							certificate_token = certTkn.certificate_token,
							access_key = ApiConfig.AccessKey,
							useraccount = ApiConfig.AccountID,
							password = ApiConfig.AccountPassword,
						};
						reqmsg = KyoeiSystem.Framework.Net.Json.FromData<AccessTokenRequest>(data);
						uri = this.UriBase + this.UriPathAccessToken;
						ApiLog.AppendLine(uri);

						// 指定URIへファイルを送信する
						httpResult = http.CallPost(URI: uri, paramlist: prmlist, postMsg: reqmsg, headers: headers, certname: null);
						p2 = DateTime.Now;
						ts = p2 - p1;
						status = httpResult.status;
						ApiLog.AppendFormat("{1}\r\n 通信時間：{0}msec\r\n", ts.TotalMilliseconds, status);
						if (httpResult.statusCode == System.Net.HttpStatusCode.Created)
						{
							if (httpResult.contentType1 == "application" && httpResult.contentSubType == "json")
							{
								accssTkn = KyoeiSystem.Framework.Net.Json.ToData<AccessTokenResponse>(httpResult.contentsText);
								this.AccessToken = accssTkn.access_token;
							}
							isLoggedOn = true;
							ApiLog.AppendFormat("{0}\r\n", httpResult.contentsText);
						}
						else
						{
							ApiLog.AppendFormat("{0}\r\n", httpResult.errors);
							throw new MyNumberAPIException("マイナンバーシステムへの接続に失敗しました。");
						}
					}
				}
			}
			catch (MyNumberAPIException ex)
			{
				throw ex;
			}
			catch (TcpException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new MyNumberAPIException("マイナンバーシステムへの接続に失敗しました。", ex);
			}
			finally
			{
			}

			return isLoggedOn;
		}
		#endregion

		#region ログオフ（実質不要）
		private void LogOff()
		{
			if (isLoggedOn)
			{
				// ログオフ手順実行
				// 本来なら、一時的に取得したトークンを解放するが
				// 一定時間経過したらサーバー側で自動的に解放されるので
				// 何もしなくてよいはず。（仕様書に記載されている）
				try
				{
					DateTime p0 = DateTime.Now;
					DateTime p1 = DateTime.Now;
					TimeSpan ts;

					string reqmsg = string.Empty;

					using (KyoeiSystem.Framework.Net.Http http = new KyoeiSystem.Framework.Net.Http())
					{
						ApiLog.AppendLine("<LogOff>");

						var prmlist = new string[] {
							"tenant_key=" + ApiConfig.TenantKey,
							"access_token=" + this.AccessToken,
						};

						string uri = this.UriBase + this.UriPathAccessToken;
						ApiLog.AppendLine(uri);

						var headers = new Dictionary<string, string>();
						headers.Add("Content-Type", "application/json");
						httpResult = http.CallDelete(URI: uri, paramlist: prmlist, headers: headers, certname: null);
						p1 = DateTime.Now;
						ts = p1 - p0;
						string status = httpResult.status;
						ApiLog.AppendFormat("{1}\r\n 通信時間：{0}msec\r\n", ts.TotalMilliseconds, status);
						if ((status == "NoContent"))
						{
							ApiLog.AppendFormat("{0}\r\n", status);
						}
						else
						{
							ApiLog.AppendFormat("{0}\r\n", httpResult.errors);
						}

					}
					return;
				}
				catch (Exception)
				{
					// 例外が発生しても何もしない。
				}
				finally
				{
					isLoggedOn = false;
				}

			}
		}
		#endregion

		#region 従業員一覧取得
		public List<Employee> GetEmployeeList(bool continuation = false)
		{
			try
			{
				DateTime p0 = DateTime.Now;
				DateTime p1 = DateTime.Now;
				TimeSpan ts;

				List<Employee> employeeList = new List<Employee>();

				CheckConfig(false);

				if (isLoggedOn != true)
				{
					// 未ログオンならログオンする
					LogOn();
				}
				string reqmsg = string.Empty;

				using (KyoeiSystem.Framework.Net.Http http = new KyoeiSystem.Framework.Net.Http())
				{
					ApiLog.AppendLine("<EmployeeList>");

					string cursor = "0";
					do
					{
						// 応答データの nextcursor が 0 になるまで以下を繰り返す
						// count は最大 100 で、それ以上を指定したらエラー

						var prmlist = new string[] {
							"tenant_key=" + ApiConfig.TenantKey,
							"count=100",
							"cursor=" + cursor,
							"processingcontent=" + const参照処理,
						};

						string uri = this.UriBase + this.UriPathEmployees;
						ApiLog.AppendLine(uri);

						var headers = new Dictionary<string, string>();
						headers.Add("Content-Type", "application/json");
						headers.Add("Authorization", this.AccessToken);
						httpResult = http.GetContents(URI: uri, paramlist: prmlist, headers: headers, certname: null);
						string status = httpResult.status;
						switch (httpResult.statusCode)
						{
						case System.Net.HttpStatusCode.OK:	// 200 Succeed
							if (httpResult.contentType1 == "application" && httpResult.contentSubType == "json")
							{
								var emps = KyoeiSystem.Framework.Net.Json.ToData<EmployeeListResponse>(httpResult.contentsText);
								if (emps == null)
								{
									cursor = "0";
								}
								else
								{
									cursor = string.IsNullOrWhiteSpace(emps.nextcursor) ? "0" : emps.nextcursor;
									if (emps.employees == null)
									{
										cursor = "0";
									}
									else
									{
										employeeList.AddRange(emps.employees);
									}
								}
							}
							else
							{
								throw new MyNumberAPIException("サーバーからの応答形式が不一致のため従業員情報が正常に受信できません。");
							}
							ApiLog.AppendFormat("{0}\r\n", httpResult.contentsText);
							break;
						case System.Net.HttpStatusCode.NotFound:
							// データが1件もないときに NotFound が返ってくる
							ApiLog.AppendFormat("{0}\r\n", httpResult.errors);
							cursor = "0";
							break;
						default:
							ApiLog.AppendFormat("{0}\r\n", httpResult.errors);
							cursor = "0";
							break;
						}
					} while (cursor != "0");

				}
				p1 = DateTime.Now;
				ts = p1 - p0;
				ApiLog.AppendFormat("{1}\r\n 通信時間：{0}msec\r\n", ts.TotalMilliseconds, httpResult.status);
				return employeeList;
			}
			catch (MyNumberAPIException ex)
			{
				throw ex;
			}
			catch (TcpException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new MyNumberAPIException("従業員情報取得時に例外が発生しました。", ex);
			}
			finally
			{
				if (continuation != true)
					LogOff();
			}

		}
		#endregion

		#region 扶養家族取得
		public List<Families> GetFamilies(bool continuation = false)
		{
			try
			{
				DateTime p0 = DateTime.Now;
				DateTime p1 = DateTime.Now;
				TimeSpan ts;

				List<Families> families = new List<Families>();

				CheckConfig(false);

				if (isLoggedOn != true)
				{
					// 未ログオンならログオンする
					LogOn();
				}
				string reqmsg = string.Empty;

				using (KyoeiSystem.Framework.Net.Http http = new KyoeiSystem.Framework.Net.Http())
				{
					ApiLog.AppendLine("<FamiliesList>");

					string cursor = "0";
					do
					{
						var prmlist = new string[] {
							"tenant_key=" + ApiConfig.TenantKey,
							"count=100",
							"cursor=" + cursor,
							"processingcontent=" + const参照処理,
						};

						string uri = this.UriBase + this.UriPathFamilies;
						ApiLog.AppendLine(uri);

						var headers = new Dictionary<string, string>();
						headers.Add("Content-Type", "application/json");
						headers.Add("Authorization", this.AccessToken);
						httpResult = http.GetContents(URI: uri, paramlist: prmlist, headers: headers, certname: null);
						string status = httpResult.status;
						switch (httpResult.statusCode)
						{
						case System.Net.HttpStatusCode.OK:	// 200 Succeed
							if (httpResult.contentType1 == "application" && httpResult.contentSubType == "json")
							{
								var fam = KyoeiSystem.Framework.Net.Json.ToData<FamiliesListResponse>(httpResult.contentsText);
								if (fam == null)
								{
									cursor = "0";
								}
								else
								{
									cursor = string.IsNullOrWhiteSpace(fam.nextcursor) ? "0" : fam.nextcursor;
									if (fam.families == null)
									{
										cursor = "0";
									}
									else
									{
										families.AddRange(fam.families);
									}
								}
							}
							else
							{
								throw new MyNumberAPIException("サーバーからの応答形式が不一致のため扶養家族情報が正常に受信できません。");
							}
							ApiLog.AppendFormat("{0}\r\n", httpResult.contentsText);
							break;
						case System.Net.HttpStatusCode.NotFound:
							// データが1件もないときに NotFound が返ってくる
							ApiLog.AppendFormat("{0}\r\n", httpResult.errors);
							cursor = "0";
							break;
						default:
							ApiLog.AppendFormat("{0}\r\n", httpResult.errors);
							cursor = "0";
							break;
						}
					} while (cursor != "0");

				}
				p1 = DateTime.Now;
				ts = p1 - p0;
				ApiLog.AppendFormat("{1}\r\n 通信時間：{0}msec\r\n", ts.TotalMilliseconds, httpResult.status);

				return families;
			}
			catch (MyNumberAPIException ex)
			{
				throw ex;
			}
			catch (TcpException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new MyNumberAPIException("扶養家族情報取得時に例外が発生しました。", ex);
			}
			finally
			{
				if (continuation != true)
					LogOff();
			}
		}
		#endregion

		#region 個人番号データ取得
		public List<Individualnumbers> GetIndividual(Dictionary<string, string> parsons, bool continuation = false)
		{
			try
			{
				DateTime p0 = DateTime.Now;
				DateTime p1 = DateTime.Now;
				TimeSpan ts;

				List<Individualnumbers> individuals = new List<Individualnumbers>();

				CheckConfig(false);

				if (isLoggedOn != true)
				{
					// 未ログオンならログオンする
					LogOn();
				}
				string reqmsg = string.Empty;

				using (KyoeiSystem.Framework.Net.Http http = new KyoeiSystem.Framework.Net.Http())
				{
					ApiLog.AppendLine("<IndividualList>");

					foreach (var item in parsons)
					{
						string personid = item.Key;
						string matchingid = item.Value;
						if (string.IsNullOrWhiteSpace(personid))
						{
							continue;
						}
						if (string.IsNullOrWhiteSpace(matchingid))
						{
							continue;
						}

						var prmlist = new string[] {
							"tenant_key=" + ApiConfig.TenantKey,
							"matchingid=" + matchingid,
							"processingcontent=" + const参照処理,
						};

						string uri = this.UriBase + this.UriPathIndividuals + "/" + personid;
						ApiLog.AppendLine(uri);

						var headers = new Dictionary<string, string>();
						headers.Add("Content-Type", "application/json");
						headers.Add("Authorization", this.AccessToken);
						httpResult = http.GetContents(URI: uri, paramlist: prmlist, headers: headers, certname: null);
						string status = httpResult.status;
						switch (httpResult.statusCode)
						{
						case System.Net.HttpStatusCode.OK:	// 200 Succeed
							if (httpResult.contentType1 == "application" && httpResult.contentSubType == "json")
							{
								var mnum = KyoeiSystem.Framework.Net.Json.ToData<IndividualListResponse>(httpResult.contentsText);
								if (mnum != null)
								{
									individuals.AddRange(mnum.individualnumbers);
								}
							}
							else
							{
								throw new MyNumberAPIException("サーバーからの応答形式が不一致のため扶養家族情報が正常に受信できません。");
							}
							ApiLog.AppendFormat("{0}\r\n", httpResult.contentsText);
							break;
						case System.Net.HttpStatusCode.NotFound:
							// データが1件もないときに NotFound が返ってくる
							ApiLog.AppendFormat("{0}\r\n", httpResult.errors);
							break;
						default:
							// その他のステータスの場合は処理続行不可能とし、中断する
							ApiLog.AppendFormat("{0}\r\n", httpResult.errors);
							throw new MyNumberAPIException(httpResult.errors);
						}
					}

				}
				p1 = DateTime.Now;
				ts = p1 - p0;
				ApiLog.AppendFormat("{1}\r\n 通信時間：{0}msec\r\n", ts.TotalMilliseconds, httpResult.status);
				return individuals;
			}
			catch (MyNumberAPIException ex)
			{
				throw ex;
			}
			catch (TcpException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new MyNumberAPIException("扶養家族情報取得時に例外が発生しました。", ex);
			}
			finally
			{
				if (continuation != true)
					LogOff();
			}
		}
		#endregion

		#region 会社情報取得（テストのみ）
		//public CorpInfoResponse GetCorpInfo()
		//{
		//	try
		//	{
		//		DateTime p0 = DateTime.Now;
		//		DateTime p1 = DateTime.Now;
		//		TimeSpan ts;

		//		ApiLog = string.Empty;

		//		CorpInfoResponse corpInfo = null;

		//		CheckConfig();

		//		if (isLoggedOn != true)
		//		{
		//			// 未ログオンならログオンする
		//			if (LogOn() != true)
		//			{
		//			}
		//		}
		//		string reqmsg = string.Empty;

		//		using (KyoeiSystem.Framework.Net.Http http = new KyoeiSystem.Framework.Net.Http())
		//		{
		//			ApiLog += "<CorpInfo>\r\n";

		//			var prmlist = new string[] { "tenant_key=" + ApiConfig.TenantKey, "processingcontent=" + "test" };

		//			string uri = ApiConfig.UriBase + ApiConfig.UriPathCorpInfo;
		//			var headers = new Dictionary<string, string>();
		//			headers.Add("Content-Type", "application/json");
		//			headers.Add("Authorization", this.accessToken);
		//			httpResult = http.GetContents(URI: uri, paramlist: prmlist, headers: headers, certfile: null);
		//			p1 = DateTime.Now;
		//			ts = p1 - p0;
		//			string status = httpResult.status;
		//			ApiLog += string.Format("{1}\r\n 通信時間：{0}msec\r\n", ts.TotalMilliseconds, status);
		//			if (status == "OK")
		//			{
		//				if (httpResult.header.ContainsKey("Content-Type") && httpResult.header["Content-Type"].Contains("/json"))
		//				{
		//					corpInfo = KyoeiSystem.Framework.Net.Json.ToData<CorpInfoResponse>(httpResult.contentsText);
		//				}
		//				ApiLog += string.Format("{0}\r\n", httpResult.contentsText);
		//			}
		//			else
		//			{
		//				ApiLog += string.Format("{0}\r\n", httpResult.errors);
		//			}

		//		}
		//		return corpInfo;
		//	}
		//	catch (TcpException ex)
		//	{
		//		throw ex;
		//	}
		//	catch (Exception ex)
		//	{
		//		throw new MyNumberAPIException("会社情報取得時に例外が発生しました。", ex);
		//	}
		//	finally
		//	{
		//		//LogOff();
		//	}
		//}
		#endregion

		#region 従業員追加（テスト用）
		//public List<string> AddEmployee(List<Employee> empdata, bool continuation = true)
		//{
		//	try
		//	{
		//		DateTime p0 = DateTime.Now;
		//		DateTime p1 = DateTime.Now;
		//		TimeSpan ts;

		//		ApiLog = string.Empty;
		//		List<string> employeeList = new List<string>();

		//		CheckConfig();

		//		if (isLoggedOn != true)
		//		{
		//			// 未ログオンならログオンする
		//			LogOn();
		//		}
		//		EmployeeListResponse empreq = new EmployeeListResponse()
		//		{
		//			employees = empdata.ToArray(),
		//		};
		//		string reqmsg = string.Empty;
		//		using (KyoeiSystem.Framework.Net.Http http = new KyoeiSystem.Framework.Net.Http())
		//		{
		//			ApiLog += "<AddEmployees>\r\n";

		//			var prmlist = new string[] {
		//				"tenant_key=" + ApiConfig.TenantKey,
		//				"processingcontent=" + "test",
		//			};

		//			string uri = this.UriBase + this.UriPathEmployees;
		//			ApiLog += uri + "\r\n";

		//			var headers = new Dictionary<string, string>();
		//			headers.Add("Content-Type", "application/json");
		//			headers.Add("Authorization", this.AccessToken);
		//			var msg = KyoeiSystem.Framework.Net.Json.FromData<EmployeeListResponse>(empreq);

		//			httpResult = http.CallPost(URI: uri, paramlist: prmlist, headers: headers, postMsg: msg, certname: null);
		//			p1 = DateTime.Now;
		//			ts = p1 - p0;
		//			string status = httpResult.status;
		//			ApiLog += string.Format("{1}\r\n 通信時間：{0}msec\r\n", ts.TotalMilliseconds, status);
		//			if ((status == "OK") || (status == "Created"))
		//			{
		//				if (httpResult.header.ContainsKey("Content-Type") && httpResult.header["Content-Type"].Contains("/json"))
		//				{
		//					var emps = KyoeiSystem.Framework.Net.Json.ToData<PersonAddResponse>(httpResult.contentsText);
		//					if (emps != null && emps.personid != null)
		//					{
		//						employeeList.AddRange(emps.personid);
		//					}
		//				}
		//				ApiLog += string.Format("{0}\r\n", httpResult.contentsText);
		//			}
		//			else
		//			{
		//				ApiLog += string.Format("{0}\r\n", httpResult.errors);
		//			}

		//		}

		//		return employeeList;
		//	}
		//	catch (TcpException ex)
		//	{
		//		throw ex;
		//	}
		//	catch (Exception ex)
		//	{
		//		throw new MyNumberAPIException("従業員追加時に例外が発生しました。", ex);
		//	}
		//	finally
		//	{
		//		if (continuation != true)
		//			LogOff();
		//	}
		//}
		#endregion

		#region 扶養家族追加（テスト用）
		//public List<string> AddFamilies(List<Families> data, bool continuation = true)
		//{
		//	try
		//	{
		//		DateTime p0 = DateTime.Now;
		//		DateTime p1 = DateTime.Now;
		//		TimeSpan ts;

		//		ApiLog = string.Empty;
		//		List<string> personList = new List<string>();

		//		CheckConfig();

		//		if (isLoggedOn != true)
		//		{
		//			// 未ログオンならログオンする
		//			LogOn();
		//		}
		//		FamiliesListResponse empreq = new FamiliesListResponse()
		//		{
		//			families = data.ToArray(),
		//		};
		//		string reqmsg = string.Empty;
		//		using (KyoeiSystem.Framework.Net.Http http = new KyoeiSystem.Framework.Net.Http())
		//		{
		//			ApiLog += "<AddFamilies>\r\n";

		//			var prmlist = new string[] {
		//				"tenant_key=" + ApiConfig.TenantKey,
		//				"processingcontent=" + "test",
		//			};

		//			string uri = this.UriBase + this.UriPathFamilies;
		//			ApiLog += uri + "\r\n";

		//			var headers = new Dictionary<string, string>();
		//			headers.Add("Content-Type", "application/json");
		//			headers.Add("Authorization", this.AccessToken);
		//			var msg = KyoeiSystem.Framework.Net.Json.FromData<FamiliesListResponse>(empreq);

		//			httpResult = http.CallPost(URI: uri, paramlist: prmlist, headers: headers, postMsg: msg, certname: null);
		//			p1 = DateTime.Now;
		//			ts = p1 - p0;
		//			string status = httpResult.status;
		//			ApiLog += string.Format("{1}\r\n 通信時間：{0}msec\r\n", ts.TotalMilliseconds, status);
		//			if ((status == "OK") || (status == "Created"))
		//			{
		//				if (httpResult.header.ContainsKey("Content-Type") && httpResult.header["Content-Type"].Contains("/json"))
		//				{
		//					var fmlys = KyoeiSystem.Framework.Net.Json.ToData<PersonAddResponse>(httpResult.contentsText);
		//					if (fmlys != null && fmlys.personid != null)
		//					{
		//						personList.AddRange(fmlys.personid);
		//					}
		//				}
		//				ApiLog += string.Format("{0}\r\n", httpResult.contentsText);
		//			}
		//			else
		//			{
		//				ApiLog += string.Format("{0}\r\n", httpResult.errors);
		//			}

		//		}

		//		return personList;
		//	}
		//	catch (TcpException ex)
		//	{
		//		throw ex;
		//	}
		//	catch (Exception ex)
		//	{
		//		throw new MyNumberAPIException("従業員追加時に例外が発生しました。", ex);
		//	}
		//	finally
		//	{
		//		if (continuation != true)
		//			LogOff();
		//	}
		//}
		#endregion

		#region ユーザー一覧取得（テスト用）
		//public List<Users> GetUserList()
		//{
		//	try
		//	{
		//		DateTime p0 = DateTime.Now;
		//		DateTime p1 = DateTime.Now;
		//		TimeSpan ts;

		//		ApiLog = string.Empty;
		//		List<Users> userList = new List<Users>();

		//		CheckConfig();

		//		if (isLoggedOn != true)
		//		{
		//			// 未ログオンならログオンする
		//			LogOn();
		//		}
		//		string reqmsg = string.Empty;
		//		using (KyoeiSystem.Framework.Net.Http http = new KyoeiSystem.Framework.Net.Http())
		//		{
		//			ApiLog += "<UserList>\r\n";

		//			string cursor = "0";

		//			do
		//			{
		//				var prmlist = new string[] {
		//						"tenant_key=" + ApiConfig.TenantKey,
		//						"count=100",
		//						"cursor=" + cursor,
		//						"processingcontent=" + "test",
		//					};

		//				string uri = ApiConfig.UriBase + ApiConfig.UriPathUser;
		//				var headers = new Dictionary<string, string>();
		//				headers.Add("Content-Type", "application/json");
		//				headers.Add("Authorization", this.accessToken);

		//				httpResult = http.GetContents(URI: uri, paramlist: prmlist, headers: headers, certfile: null);
		//				p1 = DateTime.Now;
		//				ts = p1 - p0;
		//				string status = httpResult.status;
		//				ApiLog += string.Format("{1}\r\n 通信時間：{0}msec\r\n", ts.TotalMilliseconds, status);
		//				if ((status == "OK") || (status == "Created"))
		//				{
		//					if (httpResult.header.ContainsKey("Content-Type") && httpResult.header["Content-Type"].Contains("/json"))
		//					{
		//						var users = KyoeiSystem.Framework.Net.Json.ToData<UserListResponse>(httpResult.contentsText);
		//						cursor = users.nextcursor;
		//						userList.AddRange(users.users);
		//					}
		//					ApiLog += string.Format("{0}\r\n", httpResult.contentsText);
		//				}
		//				else
		//				{
		//					ApiLog += string.Format("{0}\r\n", httpResult.errors);
		//					break;
		//				}
		//			} while (cursor != "0");

		//		}

		//		return userList;
		//	}
		//	catch (TcpException ex)
		//	{
		//		throw ex;
		//	}
		//	catch (Exception ex)
		//	{
		//		throw new MyNumberAPIException("ユーザー情報取得時に例外が発生しました。", ex);
		//	}
		//	finally
		//	{
		//		LogOff();
		//	}
		//}
		#endregion

		#region ユーザー追加（テスト用）
		//public UserListResponse AddUser(UserAddRequest userdata)
		//{
		//	try
		//	{
		//		DateTime p0 = DateTime.Now;
		//		DateTime p1 = DateTime.Now;
		//		TimeSpan ts;

		//		ApiLog = string.Empty;
		//		UserListResponse usrList = null;

		//		CheckConfig();

		//		if (isLoggedOn != true)
		//		{
		//			// 未ログオンならログオンする
		//			LogOn();
		//		}
		//		string reqmsg = string.Empty;
		//		using (KyoeiSystem.Framework.Net.Http http = new KyoeiSystem.Framework.Net.Http())
		//		{
		//			ApiLog += "<AddUser>\r\n";

		//			var prmlist = new string[] {
		//				"tenant_key=" + ApiConfig.TenantKey,
		//				"processingcontent=" + "test",
		//			};

		//			string uri = ApiConfig.UriBase + ApiConfig.UriPathUser;
		//			var headers = new Dictionary<string, string>();
		//			headers.Add("Content-Type", "application/json");
		//			headers.Add("Authorization", this.accessToken);
		//			var msg = KyoeiSystem.Framework.Net.Json.FromData<UserAddRequest>(userdata);

		//			httpResult = http.CallPost(URI: uri, paramlist: prmlist, headers: headers, postMsg: msg, certfile: null);
		//			p1 = DateTime.Now;
		//			ts = p1 - p0;
		//			string status = httpResult.status;
		//			ApiLog += string.Format("{1}\r\n 通信時間：{0}msec\r\n", ts.TotalMilliseconds, status);
		//			if ((status == "OK") || (status == "Created"))
		//			{
		//				if (httpResult.header.ContainsKey("Content-Type") && httpResult.header["Content-Type"].Contains("/json"))
		//				{
		//					usrList = KyoeiSystem.Framework.Net.Json.ToData<UserListResponse>(httpResult.contentsText);
		//				}
		//				ApiLog += string.Format("{0}\r\n", httpResult.contentsText);
		//			}
		//			else
		//			{
		//				ApiLog += string.Format("{0}\r\n", httpResult.errors);
		//			}

		//		}

		//		return usrList;
		//	}
		//	catch (TcpException ex)
		//	{
		//		throw ex;
		//	}
		//	catch (Exception ex)
		//	{
		//		throw new MyNumberAPIException("ユーザー追加時に例外が発生しました。", ex);
		//	}
		//	finally
		//	{
		//		LogOff();
		//	}
		//}
		#endregion


	}
}
