using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Security;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Web;

namespace KyoeiSystem.Framework.Net
{
	/// <summary>
	/// HTTP通信の応答クラス
	/// </summary>
	public class HttpResult
	{
		/// <summary>
		/// 要求したURI
		/// </summary>
		public Uri uri;
		/// <summary>
		/// 通信結果（HTTPステータスコードの文字列）
		/// </summary>
		public string status;
		/// <summary>
		/// HTTPステータスコード
		/// </summary>
		public HttpStatusCode statusCode;
		/// <summary>
		/// 通信エラー発生時のエラーメッセージ
		/// </summary>
		public string errors;
		/// <summary>
		/// 応答メッセージのヘッダ部
		/// </summary>
		public Dictionary<string, string> header = new Dictionary<string, string>();
		/// <summary>
		/// 応答メッセージのコンテンツタイプ
		/// </summary>
		public string contentType1;
		/// <summary>
		/// 応答メッセージのコンテンツサブタイプ
		/// </summary>
		public string contentSubType;
		/// <summary>
		/// 応答メッセージのコンテンツタイプのオプション
		/// </summary>
		public string contentTypeOption;
		/// <summary>
		/// バイナリストリームとして受信した応答のコンテンツボディ
		/// </summary>
		public byte[] contntsBin;
		/// <summary>
		/// テキストとして受信した応答メッセージのエンコード
		/// </summary>
		public System.Text.Encoding enc;
		/// <summary>
		/// テキストとして受信した応答メッセージのコンテンツボディ
		/// </summary>
		public string contentsText;
	}

	/// <summary>
	/// HTTP通信クラス
	/// </summary>
	public class Http : IDisposable
	{
		// 自己署名証明書をOKとする場合は false とするが、そのまま運用してはならない。
		private bool IsServerCertValidate = true;

		const string HttpError = "Error";
		const string HttpOK = "OK";

		private string defaultCharset = "utf-8";

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Http()
		{
			// サーバー証明書に対する処置
			ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(OnRemoteCertificateValidationCallback);

		}

		/// <summary>
		/// サーバー証明書に対する処置の有効化または無効化を制御する
		/// </summary>
		/// <param name="onoff">true:チェックする／false:チェックしない</param>
		public void SetServerCertValidate(bool onoff)
		{
			IsServerCertValidate = onoff;
		}

		/// <summary>
		/// エンコードする際のデフォルトの文字コードを指定する
		/// </summary>
		/// <param name="charset"></param>
		public void SetDefaultCharset(string charset)
		{
			this.defaultCharset = charset;
		}
		#endregion

		#region サーバー証明書チェックコールバック処理
		/// <summary>
		/// 証明書チェックコールバック処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="certificate"></param>
		/// <param name="chain"></param>
		/// <param name="sslPolicyErrors"></param>
		/// <returns>判定結果</returns>
		private bool OnRemoteCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (IsServerCertValidate)
			{
				if (sslPolicyErrors == SslPolicyErrors.None) { return true; }
				if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateChainErrors) { return true; }

				return false;
			}
			else
			{
				// サーバー証明書をチェックしないモードの場合、
				// 信頼できない証明書(自己証明書)でもOKとするため、無条件にOK(true)を返す。
				return true;
			}
		}
		#endregion

		#region クライアント証明書一覧チェックテスト
		//public void test()
		//{
		//	X509Store store = new X509Store("MyCertStore", StoreLocation.LocalMachine);
		//	store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

		//	X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
		//	X509Certificate2Collection fcollection = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
		//	X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(fcollection, "Test Certificate Select", "Select a certificate from the following list to get information on that certificate", X509SelectionFlag.MultiSelection);

		//	foreach (X509Certificate2 x509 in scollection)
		//	{
		//		try
		//		{
		//			byte[] rawdata = x509.RawData;
		//			Console.WriteLine("Content Type: {0}{1}", X509Certificate2.GetCertContentType(rawdata), Environment.NewLine);
		//			Console.WriteLine("Friendly Name: {0}{1}", x509.FriendlyName, Environment.NewLine);
		//			Console.WriteLine("Certificate Verified?: {0}{1}", x509.Verify(), Environment.NewLine);
		//			Console.WriteLine("Simple Name: {0}{1}", x509.GetNameInfo(X509NameType.SimpleName, true), Environment.NewLine);
		//			Console.WriteLine("Signature Algorithm: {0}{1}", x509.SignatureAlgorithm.FriendlyName, Environment.NewLine);
		//			Console.WriteLine("Private Key: {0}{1}", x509.PrivateKey.ToXmlString(false), Environment.NewLine);
		//			Console.WriteLine("Public Key: {0}{1}", x509.PublicKey.Key.ToXmlString(false), Environment.NewLine);
		//			Console.WriteLine("Certificate Archived?: {0}{1}", x509.Archived, Environment.NewLine);
		//			Console.WriteLine("Length of Raw Data: {0}{1}", x509.RawData.Length, Environment.NewLine);
		//			X509Certificate2UI.DisplayCertificate(x509);
		//			x509.Reset();
		//		}
		//		catch (CryptographicException)
		//		{
		//			System.Diagnostics.Debug.WriteLine("Information could not be written out for this certificate.");
		//		}
		//	}
		//	store.Close();
		//}
		#endregion

		#region パラメータを付加したURIを編集
		private Uri makeUri(string URI, string[] paramlist = null)
		{
			if (paramlist != null && paramlist.Length > 0)
			{
				string uri = URI;
				string dlmtr = "?";
				foreach (var p in paramlist)
				{
					uri += dlmtr + p;
					dlmtr = "&";
				}
				return new Uri(uri);
			}
			else
			{
				return new Uri(URI);
			}
		}
		#endregion

		#region リクエスト用ヘッダを編集（既定のヘッダは配列ではなく個別のプロパティでセットする）
		private void makeRequestHeaders(HttpWebRequest http, Dictionary<string, string> headers)
		{
			if (headers == null)
			{
				return;
			}
			foreach (var item in headers)
			{
				switch (item.Key.ToLower())
				{
				default:
					http.Headers.Add(item.Key, item.Value);
					break;
				case "accept":
					http.Accept = item.Value;
					break;
				case "connection":
					http.Connection = item.Value;
					break;
				case "content-length":
					http.ContentLength = long.Parse(item.Value);
					break;
				case "content-type":
					http.ContentType = item.Value;
					break;
				case "date":
					http.Date = DateTime.Parse(item.Value);
					break;
				case "expect":
					http.Expect = item.Value;
					break;
				case "host":
					http.Host = item.Value;
					break;
				case "if-modified-since":
					http.IfModifiedSince = DateTime.Parse(item.Value);
					break;
				case "Referer":
					http.Referer = item.Value;
					break;
				case "transfer-encoding":
					http.TransferEncoding = item.Value;
					break;
				case "user-agent":
					http.UserAgent = item.Value;
					break;
				}
			}
		}
		#endregion

		#region GETメッセージ（ファイルダウンロード機能兼用）
		/// <summary>
		/// 指定URIのコンテンツを取得する。（ファイル受信機能兼用）
		/// </summary>
		/// <param name="URI">URI文字列</param>
		/// <param name="paramlist">URLパラメータ配列</param>
		/// <param name="headers">送信ヘッダ配列</param>
		/// <param name="certname">クライアント証明書シリアル番号</param>
		/// <param name="userid">Basic認証用ユーザID</param>
		/// <param name="passwd">Basic認証用パスワード</param>
		/// <returns>通信結果</returns>
		public HttpResult GetContents(string URI, string[] paramlist = null, Dictionary<string, string> headers = null, string certname = null, string userid = null, string passwd = null)
		{
			return CallMethod("GET", URI, paramlist, headers, null, certname, userid, passwd);
		}

		#endregion

		#region アップロード（CGI経由のみ）
		/// <summary>
		/// 指定されたアップロード用スクリプトを介して、ファイルをサーバに転送(アップロード)する。
		/// </summary>
		/// <param name="URI">URI文字列</param>
		/// <param name="localfile">アップロードファイル名</param>
		/// <param name="paramlist">URLパラメータ配列</param>
		/// <param name="certname">クライアント証明書シリアル番号</param>
		/// <param name="userid">Basic認証用ユーザID</param>
		/// <param name="passwd">Basic認証用パスワード</param>
		/// <returns>通信結果</returns>
		public HttpResult Upload(string URI, string localfile, string[] paramlist = null, string certname = null, string userid = null, string passwd = null)
		{
			HttpResult result = new HttpResult();
			try
			{
				result.uri = makeUri(URI, paramlist);

				WebClient wc = new WebClient();
				if (string.IsNullOrWhiteSpace(userid) != true)
				{
					// Basic認証のみをサポート
					wc.Credentials = new System.Net.NetworkCredential(userid, passwd);
				}
				byte[] res = wc.UploadFile(URI, localfile);
				// アップロード用スクリプトの実行結果を解析する。
				result.contentsText = System.Text.Encoding.ASCII.GetString(res);
				if (result.contentsText.Contains("complete"))
				{
					result.status = HttpOK;
				}
				else
				{
					result.status = HttpError;
				}
			}
			catch (WebException ex)
			{
				result.status = ex.Status.ToString();
				result.errors = ex.Message + (ex.InnerException != null ? "\r\n" + ex.InnerException.Message : string.Empty);
			}
			catch (Exception ex)
			{
				result.status = HttpError;
				result.errors = ex.Message + (ex.InnerException != null ? "\r\n" + ex.InnerException.Message : string.Empty);
			}
			finally
			{
			}
			return result;

		}

		/// <summary>
		/// 指定されたアップロード用スクリプトに対してPOSTメッセージを介して、ファイルをサーバに転送(アップロード)する。
		/// </summary>
		/// <param name="URI">URI文字列</param>
		/// <param name="localfile">アップロードファイル名</param>
		/// <param name="paramlist">URLパラメータ配列</param>
		/// <param name="certname">クライアント証明書シリアル番号</param>
		/// <param name="userid">Basic認証用ユーザID</param>
		/// <param name="passwd">Basic認証用パスワード</param>
		/// <returns>通信結果</returns>
		private HttpResult UploadByPOST(string URI, string localfile, string[] paramlist = null, string certname = null, string userid = null, string passwd = null)
		{
			HttpResult result = new HttpResult();
			try
			{
				throw new TcpException("UploadByPOST は未実装の機能です。Upload または CallPost を使用してください。");

				//result.uri = makeUri(URI, paramlist);

				// マルチパートでFROM変数をセットしてメッセージをPOSTする

			}
			catch (WebException ex)
			{
				result.status = ex.Status.ToString();
				result.errors = ex.Message + (ex.InnerException != null ? "\r\n" + ex.InnerException.Message : string.Empty);
			}
			catch (Exception ex)
			{
				result.status = HttpError;
				result.errors = ex.Message + (ex.InnerException != null ? "\r\n" + ex.InnerException.Message : string.Empty);
			}
			finally
			{
			}
			return result;

		}
		#endregion

		#region PUTメッセージ
		/// <summary>
		/// 指定URIのコンテンツをPUTメッセージにて取得する。
		/// </summary>
		/// <param name="URI">URI文字列</param>
		/// <param name="paramlist">URLパラメータ配列</param>
		/// <param name="headers">送信ヘッダ配列</param>
		/// <param name="putMsg">POSTで送信するメッセージボディ</param>
		/// <param name="certname">クライアント証明書シリアル番号</param>
		/// <param name="userid">Basic認証用ユーザID</param>
		/// <param name="passwd">Basic認証用パスワード</param>
		/// <returns>通信結果</returns>
		public HttpResult CallPut(string URI, string[] paramlist = null, Dictionary<string, string> headers = null, string putMsg = null, string certname = null, string userid = null, string passwd = null)
		{
			return CallMethod("PUT", URI, paramlist, headers, putMsg, certname, userid, passwd);
		}
		#endregion

		#region POSTメッセージ
		/// <summary>
		/// 指定URIのコンテンツをPOSTメッセージにて取得する。
		/// </summary>
		/// <param name="URI">URI文字列</param>
		/// <param name="paramlist">URLパラメータ配列</param>
		/// <param name="headers">送信ヘッダ配列</param>
		/// <param name="postMsg">POSTで送信するメッセージボディ</param>
		/// <param name="certname">クライアント証明書シリアル番号</param>
		/// <param name="userid">Basic認証用ユーザID</param>
		/// <param name="passwd">Basic認証用パスワード</param>
		/// <returns>通信結果</returns>
		public HttpResult CallPost(string URI, string[] paramlist = null, Dictionary<string, string> headers = null, string postMsg = null, string certname = null, string userid = null, string passwd = null)
		{
			return CallMethod("POST", URI, paramlist, headers, postMsg, certname, userid, passwd);
		}
		#endregion

		#region DELETEメッセージ
		/// <summary>
		/// 指定URIのコンテンツをDELETEメッセージにて取得する。
		/// </summary>
		/// <param name="URI">URI文字列</param>
		/// <param name="paramlist">URLパラメータ配列</param>
		/// <param name="headers">送信ヘッダ配列</param>
		/// <param name="postMsg">POSTで送信するメッセージボディ</param>
		/// <param name="certname">クライアント証明書シリアル番号</param>
		/// <param name="userid">Basic認証用ユーザID</param>
		/// <param name="passwd">Basic認証用パスワード</param>
		/// <returns>通信結果</returns>
		public HttpResult CallDelete(string URI, string[] paramlist = null, Dictionary<string, string> headers = null, string postMsg = null, string certname = null, string userid = null, string passwd = null)
		{
			return CallMethod("DELETE", URI, paramlist, headers, postMsg, certname, userid, passwd);
		}
		#endregion

		#region サーバーメソッド呼び出し
		private HttpResult CallMethod(string method, string URI, string[] paramlist = null, Dictionary<string, string> headers = null, string postMsg = null, string certname = null, string userid = null, string passwd = null)
		{
			HttpResult result = new HttpResult();
			try
			{
				result.uri = makeUri(URI, paramlist);

				byte[] reqcontents = System.Text.Encoding.UTF8.GetBytes(string.IsNullOrWhiteSpace(postMsg) ? string.Empty : postMsg);

				HttpWebRequest http = System.Net.WebRequest.Create(result.uri) as HttpWebRequest;
				if (http != null)
				{
					if (string.IsNullOrWhiteSpace(certname) != true)
					{
						var clientCertificate = Cert.GetCert(certname);
						http.ClientCertificates.Add(clientCertificate);
					}
					if (string.IsNullOrWhiteSpace(userid) != true)
					{
						// Basic認証のみをサポート
						http.Credentials = new System.Net.NetworkCredential(userid, passwd);
					}
					http.Method = method;
					makeRequestHeaders(http, headers);
					http.UserAgent = "KyoeiSystem.Framework.Net.HttpAgent";
					if (method != "GET" && reqcontents.Length > 0)
					{
						http.ContentLength = reqcontents.Length;
						using (Stream requestStream = http.GetRequestStream())
						{
							requestStream.Write(reqcontents, 0, reqcontents.Length);
						}
					}
					HttpWebResponse webres = (System.Net.HttpWebResponse)http.GetResponse();
					result.statusCode = webres.StatusCode;
					result.status = webres.StatusCode.ToString();
					for (int i = 0; i < webres.Headers.Count; i++)
					{
						string val = string.Empty;
						string dlmtr = "";
						foreach (string str in webres.Headers.GetValues(i))
						{
							val += dlmtr + str;
							dlmtr = ", ";
						}
						result.header.Add(webres.Headers.Keys[i], val);
					}

					List<byte> resContents = new List<byte>();
					using (Stream st = webres.GetResponseStream())
					{
						// Binaryで読み込む
						using (BinaryReader sr = new BinaryReader(st))
						{
							for (int i = 0; true; i += 4096)
							{
								byte[] buf = sr.ReadBytes(4096);
								if (buf.Length == 0)
								{
									break;
								}
								resContents.AddRange(buf);
							}
						}
					}

					string estr = defaultCharset;
					foreach (var item in result.header.Values)
					{
						Match mymatch = Regex.Match(item, @"charset\s*=\s*([-_\w]+)", RegexOptions.IgnoreCase);
						if (mymatch.Success)
						{
							estr = mymatch.Groups[1].Value;
							break;
						}
					}
					if (resContents.Count > 0)
					{
						string[] ctypes = result.header["Content-Type"].Split(new char[] { '/' });
						result.contentType1 = ctypes[0];
						if (ctypes.Length == 2)
						{
							bool istext = false;
							string[] subtypes = ctypes[1].Split(new char[] { ';' });
							result.contentSubType = subtypes[0];
							if (subtypes.Length > 1)
							{
								result.contentTypeOption = subtypes[1];
							}
							// Content-Typeがtextの場合
							if (ctypes[0].ToLower() == "text")
							{
								istext = true;
								if (subtypes[0] == "html")
								{
									// METAタグを読み込んで、文字コードを解析する
									var hdstr = Encoding.ASCII.GetString(resContents.ToArray());
									Match mymatch = Regex.Match(hdstr, @"<meta\s+[^>]*charset\s*=\s*([-_\w]+)", RegexOptions.IgnoreCase);
									if (mymatch.Success)
									{
										estr = mymatch.Groups[1].Value;
									}
								}
							}
							else if (ctypes[0].ToLower() == "application" && subtypes[0].ToLower() == "json")
							{
								istext = true;
							}
							if (istext)
							{
								result.enc = System.Text.Encoding.GetEncoding(estr);
								result.contentsText = result.enc.GetString(resContents.ToArray());
							}
							else
							{
								result.contntsBin = resContents.ToArray();
							}
						}
						else
						{
							result.contntsBin = resContents.ToArray();
						}
					}
				}
			}
			catch (WebException ex)
			{
				result.status = ex.Status.ToString();
				var res = (ex.Response as HttpWebResponse);
				if (res != null)
				{
					result.statusCode = res.StatusCode;
					result.status = result.statusCode.ToString();
				}
				result.errors = string.Format("[{1}]{0}\r\n", result.uri, method) + ex.Message + (ex.InnerException != null ? "\r\n" + ex.InnerException.Message : string.Empty);
				//throw new TcpException("通信時エラーが発生しました。", ex);
			}
			catch (Exception ex)
			{
				result.status = HttpError;
				result.errors = string.Format("[{1}]{0}\r\n", result.uri, method) + ex.Message + (ex.InnerException != null ? "\r\n" + ex.InnerException.Message : string.Empty);
			}
			finally
			{
			}
			return result;
		}
		#endregion

		#region IDisposable メンバー
		/// <summary>
		/// インスタンス解放
		/// </summary>
		public void Dispose()
		{
		}

		#endregion
	}
}
