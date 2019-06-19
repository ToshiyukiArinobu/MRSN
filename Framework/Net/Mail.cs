using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace KyoeiSystem.Framework.Net
{
	/// <summary>
	/// 通信時のセキュリティモード
	/// </summary>
	public enum SecureMode
	{
		/// <summary>
		/// なし
		/// </summary>
		None,
		/// <summary>
		/// SSL 2.x
		/// </summary>
		SSL2,
		/// <summary>
		/// SSL 3.x
		/// </summary>
		SSL3,
		/// <summary>
		/// TLS 1.x
		/// </summary>
		TLS,
		/// <summary>
		/// STARTTLS
		/// </summary>
		STARTTLS,
	}

	/// <summary>
	/// SMTPの認証方法を表す列挙体
	/// </summary>
	public enum SmtpAuthMethod
	{
		/// <summary>
		/// 認証を行わない
		/// </summary>
		None,
		/// <summary>
		/// AUTH PLAIN
		/// </summary>
		Plain,
		/// <summary>
		/// AUTH LOGIN
		/// </summary>
		Login,
		/// <summary>
		/// AUTH CRAM-MD5
		/// </summary>
		CramMd5,
	}

	/// <summary>
	/// POP通信プロトコル
	/// </summary>
	public enum PopProtocol
	{
		/// <summary>
		/// POP3
		/// </summary>
		POP3,
		/// <summary>
		/// IMAP（サポート対象外）
		/// </summary>
		IMAP,
	}
	/// <summary>
	/// POP3の認証方法を表す列挙体
	/// </summary>
	public enum PopAuthMethod
	{
		/// <summary>
		/// 標準
		/// </summary>
		Standard,
		/// <summary>
		/// AUTH PLAIN
		/// </summary>
		APOP,
		/// <summary>
		/// AUTH CRAM-MD5
		/// </summary>
		CramMd5,
		/// <summary>
		/// NTLM（Windowsログイン）
		/// </summary>
		NTLM,
	}

	/// <summary>
	/// メール送信機能用パラメータクラス
	/// </summary>
	public class MailParameters
	{
		/// <summary>
		/// SMTPサーバー名またはIPアドレス
		/// </summary>
		public string SmtpServer;
		/// <summary>
		/// SMTPサーバーポート番号
		/// </summary>
		public int SmtpPort;
		/// <summary>
		/// SMTPセキュリティモード
		/// </summary>
		public SecureMode SmtpSecureMode = SecureMode.None;
		/// <summary>
		/// SMTPログインユーザID
		/// </summary>
		public string SmtpUserId;
		/// <summary>
		/// SMTPログインパスワード
		/// </summary>
		public string SmtpPasswd;
		/// <summary>
		/// SMTP認証プロトコル
		/// </summary>
		public SmtpAuthMethod SmtpAuth = SmtpAuthMethod.None;
		/// <summary>
		/// サーバー証明書の有効性チェックするかどうか
		/// </summary>
		public bool IsServerCertValidate = false;
		/// <summary>
		/// クライアント証明書を使用するかどうか
		/// </summary>
		public bool IsClientCertValidate = false;
		/// <summary>
		/// クライアント証明書シリアルNo
		/// </summary>
		public string ClientCertSerialNo = string.Empty;
		/// <summary>
		/// POP before SMTP の有無
		/// </summary>
		public bool IsPopBeforeSmtp = false;
		/// <summary>
		/// POPサーバー名またはIPアドレス
		/// </summary>
		public string PopServer;
		/// <summary>
		/// POPサーバーポート番号
		/// </summary>
		public int PopPort;
		/// <summary>
		/// POPセキュリティモード
		/// </summary>
		public SecureMode PopSecureMode = SecureMode.None;
		/// <summary>
		/// POP通信プロトコル
		/// </summary>
		public PopProtocol PopProtocol = PopProtocol.POP3;
		/// <summary>
		/// POPログインユーザID
		/// </summary>
		public string PopUserId;
		/// <summary>
		/// POPログインパスワード
		/// </summary>
		public string PopPasswd;
		/// <summary>
		/// 認証プロトコル
		/// </summary>
		public PopAuthMethod PopAuth = PopAuthMethod.Standard;
		/// <summary>
		/// POP接続後の待ち時間
		/// </summary>
		public int PopDelayTime = 300;

		/// <summary>
		/// Toフィールド配列
		/// </summary>
		public string[] To;
		/// <summary>
		/// Ccフィールド配列
		/// </summary>
		public string[] Cc;
		/// <summary>
		/// Bccフィールド配列
		/// </summary>
		public string[] Bcc;
		/// <summary>
		/// Fromフィールド
		/// </summary>
		public string From;
		/// <summary>
		/// Senderフィールド
		/// </summary>
		public string Sender;
		/// <summary>
		/// ReplyToフィールド
		/// </summary>
		public string ReplyTo;
		/// <summary>
		/// Subjectフィールド
		/// </summary>
		public string Subject;
		/// <summary>
		/// 追加ヘッダ配列（接頭子 X- のみ）
		/// </summary>
		public string[] ExtHeaders;
		/// <summary>
		/// 添付ファイル名配列
		/// </summary>
		public string[] Files;
		/// <summary>
		/// 本文
		/// </summary>
		public string Body;
		/// <summary>
		/// 送信結果ステータス
		/// </summary>
		public string status;
		/// <summary>
		/// 通信ログ
		/// </summary>
		public string logs;
	}

	/// <summary>
	/// メール送信機能
	/// </summary>
	public class Mail : IDisposable
	{
		class SmtpCommData
		{
			public int code;
			public string received;
			public TcpClient socket = null;
			public Stream stream = null;
			public StreamReader reader = null;
			public StreamWriter writer = null;
		}
		#region フィールド
		private bool IsServerCertValidate = false;

		private SmtpCommData smtpcom = null;

		private Encoding _encoding;
		private StringBuilder logs = new StringBuilder();
		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Mail()
		{
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


		#region メッセージ送信
		/// <summary>
		/// 指定されたパラメータの内容でメールを送信する（エンコードはISO-2022-JPのみをサポート）
		/// </summary>
		/// <param name="mailParams">メール送信パラメータ</param>
		/// <returns>送信結果（true：正常、false：失敗）</returns>
		public bool SendMail(MailParameters mailParams)
		{
			// ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
			// ■
			// ■ System.Net.Mail.SmtpClient は、SMTP.SSLに対応していないため使用不可なので、
			// ■ いろんなパターンに対応するため、TcpClientを使ってプロトコルを実装する。
			// ■
			// ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■

			bool result = false;
			mailParams.logs = string.Empty;
			try
			{
				// ISO-2022-JPでエンコードする
				_encoding = System.Text.Encoding.GetEncoding(50220);

				if (mailParams.IsPopBeforeSmtp)
				{
					// POP before SMTP 指定の場合
					PopBeforeSmtp(mailParams);
					if (mailParams.PopDelayTime > 0)
					{
						System.Threading.Thread.Sleep(mailParams.PopDelayTime);
					}
				}

				// SMTPサーバーと接続する
				smtpcom = SmtpConnect(mailParams);

				// EHLO
				SendHeloCommand(System.Net.Dns.GetHostName());

				if (mailParams.SmtpSecureMode == SecureMode.STARTTLS)
				{
					// TLSの実装------------------------------------------------------------------
					// STARTTLSの送信
					var rcode = SmtpSendAndReceive("STARTTLS" + "\r\n");
					if ((rcode / 100) != 2)
					{
						throw new SmtpException("エラー:" + rcode);
					}

					// SSLの開始
					smtpcom.stream = new System.Net.Security.SslStream(smtpcom.socket.GetStream());
					((System.Net.Security.SslStream)smtpcom.stream).AuthenticateAsClient(mailParams.SmtpServer);
					smtpcom.stream.ReadTimeout = 5000;
					smtpcom.stream.WriteTimeout = 500;
					smtpcom.reader = new System.IO.StreamReader(smtpcom.stream, _encoding);
					smtpcom.writer = new System.IO.StreamWriter(smtpcom.stream, _encoding);
				}

				
				// AUTH
				Authenticate(mailParams.SmtpAuth, mailParams.SmtpUserId, mailParams.SmtpPasswd);
				// MAIL FROM（RCPTより先に送信する）
				SendMailFromCommand(mailParams.From);
				// RCPT TO
				SendRcptToCommand(mailParams.To, mailParams.Cc, mailParams.Bcc);
				// DATA（メールヘッダ＆ボディ）
				SendDataCommand(mailParams);

				// QUIT
				SendQuitCommand();

				result = true;
				mailParams.status = "OK";

			}
			catch (Exception ex)
			{
				mailParams.status = "NG : " + ex.Message + ((ex.InnerException == null) ? "" : ("\r\n" + ex.InnerException.Message));
				result = false;
			}
			finally
			{
				if (smtpcom != null)
				{
					if (smtpcom.socket != null)
					{
						smtpcom.socket.Close();
					}
				}
			}
			mailParams.logs = logs.ToString();
			return result;
		}


		/// <summary>
		/// SMTPサーバーと接続する
		/// </summary>
		private SmtpCommData SmtpConnect(MailParameters mailParams)
		{
			this.IsServerCertValidate = mailParams.IsServerCertValidate;
			smtpcom = new SmtpCommData();

			smtpcom.socket = new TcpClient();

			// SMTPサーバーと接続する
			smtpcom.socket.Connect(mailParams.SmtpServer, mailParams.SmtpPort);
			logs.AppendLine("SMTP: Connected.");

			X509CertificateCollection clientCertificateCollection = new X509CertificateCollection();
			if (mailParams.IsClientCertValidate)
			{
				var clientCertificate = Cert.GetCert(mailParams.ClientCertSerialNo);
				clientCertificateCollection.Add(clientCertificate);
			}

			// サーバーとデータの送受信を行うストリームを取得する
			// 通信開始(SSL有り)
			switch (mailParams.SmtpSecureMode)
			{
			case SecureMode.SSL2:	// SSL2で運用しているサーバは存在しないはずだが、一応対応しておく
				smtpcom.stream = new System.Net.Security.SslStream(smtpcom.socket.GetStream(), false, ServerCertificateValidation);
				((System.Net.Security.SslStream)smtpcom.stream).AuthenticateAsClient(mailParams.SmtpServer, clientCertificateCollection, SslProtocols.Ssl2, false);
				logs.AppendLine("SMTP: socket is over SSL2.");
				break;

			case SecureMode.SSL3:	// SSL3で運用しているサーバはあるかもしれない
				smtpcom.stream = new System.Net.Security.SslStream(smtpcom.socket.GetStream(), false, ServerCertificateValidation);
				((System.Net.Security.SslStream)smtpcom.stream).AuthenticateAsClient(mailParams.SmtpServer, clientCertificateCollection, SslProtocols.Ssl3, false);
				logs.AppendLine("SMTP: socket is over SSL3.");
				break;

			case SecureMode.TLS:	// TLSは現状では主流
			case SecureMode.STARTTLS:
				smtpcom.stream = new System.Net.Security.SslStream(smtpcom.socket.GetStream(), false, ServerCertificateValidation);
				((System.Net.Security.SslStream)smtpcom.stream).AuthenticateAsClient(mailParams.SmtpServer, clientCertificateCollection, SslProtocols.Tls, false);

				logs.AppendLine("SMTP: socket is over TLS.");
				break;

			case SecureMode.None:
				smtpcom.stream = smtpcom.socket.GetStream();
				logs.AppendLine("SMTP: socket unsecure.");
				break;
			}
			smtpcom.stream.ReadTimeout = 5000;
			smtpcom.stream.WriteTimeout = 500;
			smtpcom.reader = new System.IO.StreamReader(smtpcom.stream, _encoding);
			smtpcom.writer = new System.IO.StreamWriter(smtpcom.stream, _encoding);

			//サーバーからのはじめのメッセージを受信
			var ret = ReceiveData();
			if (ret != 220)
				throw new SmtpException(smtpcom.received);

			return smtpcom;
		}

		#region 証明書チェックコールバック処理
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="certificate"></param>
		/// <param name="chain"></param>
		/// <param name="sslPolicyErrors"></param>
		/// <returns></returns>
		private bool ServerCertificateValidation(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (IsServerCertValidate)
			{
				if (sslPolicyErrors == SslPolicyErrors.None) { return true; }
				if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateChainErrors) { return true; }

				return false;
			}
			else
			{
				return true;
			}
		}
		#endregion

		/// <summary>
		/// SMTPサーバーに文字列を送信し、応答を受信する
		/// </summary>
		/// <param name="message">送信する文字列</param>
		/// <returns>受信した応答</returns>
		private int SmtpSendAndReceive(string message)
		{
			SendData(message);
			var rcode = ReceiveData();

			return rcode;
		}

		#endregion

		#region SMTP通信
		//サーバーからデータを受信する
		private int ReceiveData()
		{
			StringBuilder buffer = new StringBuilder();
			string line;
			do
			{
				//1行読み取る
				line = smtpcom.reader.ReadLine();
				if (line == null)
				{
					break;
				}
				buffer.Append(line).Append("\r\n");
				//表示
				logs.AppendLine("< " + line);
			}
			while (System.Text.RegularExpressions.Regex.IsMatch(line, @"^\d+-"));

			//受信したメッセージとコードをフィールドに入れる
			smtpcom.received = buffer.ToString();
			if (buffer.Length >= 3)
			{
				if (int.TryParse(smtpcom.received.Substring(0, 3), out smtpcom.code) != true)
				{
					smtpcom.code = -1;
				}
			}
			else
			{
				smtpcom.code = -1;
			}
			return smtpcom.code;
		}

		//サーバーにデータを送信する
		private void SendData(string str)
		{
			//送信
			smtpcom.writer.Write(str);
			smtpcom.writer.Flush();

			logs.Append("> " + str);
		}

		//エンコードし、Base64に変換
		private string GetBase64String(string str)
		{
			return Convert.ToBase64String(_encoding.GetBytes(str));
		}

		//EHLOコマンドを送る
		private void SendHeloCommand(string domainName)
		{
			var code = SmtpSendAndReceive("EHLO " + domainName + "\r\n");
			if (code != 250)
				throw new SmtpException(smtpcom.received);
		}

		//MAIL FROMコマンドを送る
		private void SendMailFromCommand(string fromAddress)
		{
			var code = SmtpSendAndReceive("MAIL FROM:<" + fromAddress + ">\r\n");
			if (code != 250)
				throw new SmtpException(smtpcom.received);
		}

		//RCPT TOコマンドを送る
		private void SendRcptToCommand(string[] tolist, string[] cclist, string[] bcclist)
		{
			// To
			foreach (var to in (tolist ?? new string[] { }))
			{
				var code = SmtpSendAndReceive("RCPT TO:<" + to.Trim() + ">\r\n");
				if (code < 250 || 251 < code)
					throw new SmtpException(smtpcom.received);

			}
			// Cc
			foreach (var cc in (cclist ?? new string[] { }))
			{
				var code = SmtpSendAndReceive("RCPT TO:<" + cc.Trim() + ">\r\n");
				if (code < 250 || 251 < code)
					throw new SmtpException(smtpcom.received);

			}
			// Bcc
			foreach (var bcc in (bcclist ?? new string[] { }))
			{
				var code = SmtpSendAndReceive("RCPT TO:<" + bcc.Trim() + ">\r\n");
				if (code < 250 || 251 < code)
					throw new SmtpException(smtpcom.received);

			}
		}

		//DATAコマンドを送る
		private void SendDataCommand(MailParameters mailParams)
		{
			//送信データを作成する
			string data = CreateSendStringDataFromMessage(mailParams);

			// データコマンド送信
			var code = SmtpSendAndReceive("DATA\r\n");
			if (code != 354)
				throw new SmtpException(smtpcom.received);

			// データ本体送信
			code = SmtpSendAndReceive(data);
			if (code != 250)
				throw new SmtpException(smtpcom.received);
		}

		/// <summary>
		/// メール送信データ編集（ヘッダ＆ボディ）
		/// </summary>
		/// <param name="mailParams"></param>
		/// <returns></returns>
		private string CreateSendStringDataFromMessage(MailParameters mailParams)
		{
			StringBuilder data = new StringBuilder();

			// ヘッダ部
			data.Append("From: ").Append(mailParams.From.Trim()).Append("\r\n");
			string prefix;
			if (mailParams.To != null && mailParams.To.Length > 0)
			{
				prefix = "To: ";
				foreach (var to in mailParams.To)
				{
					data.Append(prefix).Append(to.Trim());
					prefix = ",";
				}
				data.Append("\r\n");
			}
			if (mailParams.Cc != null && mailParams.Cc.Length > 0)
			{
				prefix = "Cc: ";
				foreach (var cc in mailParams.Cc)
				{
					data.Append(prefix).Append(cc.Trim());
					prefix = ",";
				}
				data.Append("\r\n");
			}
			// Bccは送信しても消されるので編集すること自体が無意味（RCPT TOコマンドで送信済み）

			// ReplyTo
			if (string.IsNullOrWhiteSpace(mailParams.ReplyTo) != true)
			{
				data.Append("ReplyTo: ").Append(mailParams.ReplyTo.Trim()).Append("\r\n");
			}
			// Sender
			if (string.IsNullOrWhiteSpace(mailParams.Sender) != true)
			{
				data.Append("Sender: ").Append(mailParams.Sender.Trim()).Append("\r\n");
			}
			if (mailParams.ExtHeaders != null)
			{
				foreach (var ext in mailParams.ExtHeaders)
				{
					if (string.IsNullOrWhiteSpace(ext))
					{
						// 空文字は無視
						continue;
					}
					if (ext.StartsWith("X-") != true || ext.Contains(": ") != true)
					{
						throw new SmtpException("Invalid format in Optional-header[" + ext + "]");
					}
					data.Append(ext + "\r\n");
				}
			}

			//件名をBase64でエンコード
			data.Append("Subject: =?" + _encoding.BodyName + "?B?").Append(GetBase64String(mailParams.Subject)).Append("?=").Append("\r\n");
			data.Append("MIME-Version: 1.0\r\n");
			data.Append("Content-Transfer-Encoding: 7bit\r\n");
			if (mailParams.Files == null || mailParams.Files.Length == 0)
			{
				// 添付ファイルがない場合（テキスト本文のみ）
				data.Append("Content-Type: text/plain; charset=" + _encoding.BodyName + "\r\n");
				// ヘッダとボディの区切りのための改行
				data.Append("\r\n");
				// ボディ部
				data.Append(mailParams.Body.Replace("\r\n.\r\n", "\r\n..\r\n"));
			}
			else
			{
				string boundary = string.Empty;
				{
					// boundary文字列を乱数から生成(まともな乱数にするため、System.Randomは使用禁止)
					System.Security.Cryptography.RNGCryptoServiceProvider rnd = new System.Security.Cryptography.RNGCryptoServiceProvider();
					byte[] bytes = new byte[24];
					rnd.GetBytes(bytes);
					boundary = "_PART_" + Convert.ToBase64String(bytes, Base64FormattingOptions.InsertLineBreaks);
				}
				// 注意：boundaryの文字列は""で囲むのがRFC的に正しい。
				data.Append("Content-Type: multipart/mixed; boundary=\"" + boundary + "\"\r\n");
				// ヘッダとボディの区切りのための改行
				data.Append("\r\n");
				// 本文は textパート
				data.Append("--" + boundary + "\r\n");
				data.Append("Content-Type: text/plain; charset=" + _encoding.BodyName + "\r\n");
				data.Append("\r\n");
				data.Append(mailParams.Body.Replace("\r\n.\r\n", "\r\n..\r\n"));

				// 添付ファイル
				foreach (var file in mailParams.Files)
				{
					FileInfo fi = new FileInfo(file);
					byte[] fdata;
					FileStream fs = new FileStream(file, FileMode.Open);
					long length = fs.Length;
					if (length > int.MaxValue)
					{
						fs.Dispose();
						throw new SmtpException("too large file");
					}
					else
					{
						fdata = new byte[length];
						fs.Read(fdata, 0, (int)length);
						fs.Dispose();
					}
					string b64 = "Content-type: unknown;\r\n";
					string fstr = Convert.ToBase64String(_encoding.GetBytes(fi.Name), Base64FormattingOptions.InsertLineBreaks);
					b64 += " name=" + "=?ISO-2022-JP?B?" + fstr + "?=" + "\r\n";
					b64 += "Content-Transfer-Encoding: base64\r\nContent-Disposition: attachment;\r\n";
					b64 += " filename=" + "=?ISO-2022-JP?B?" + fstr + "?=" + "\r\n\r\n";
					{
						b64 += Convert.ToBase64String(fdata, Base64FormattingOptions.InsertLineBreaks);
					}
					data.Append("\r\n\r\n--" + boundary + "\r\n");
					data.Append(b64);
				}
				// multipart の終端
				data.Append("\r\n\r\n--" + boundary + "--\r\n");
			}

			// メールデータの終端
			data.Append("\r\n.\r\n");

			return data.ToString();
		}

		//QUITコマンドを送る
		private void SendQuitCommand()
		{
			var code = SmtpSendAndReceive("QUIT\r\n");
			logs.AppendLine("SMTP: disconnected.");
			// 終了時のエラー判定は無視する
			//if (code != 221)
			//    throw new SmtpException(smtpcom.received);
		}

		//認証を行う
		private void Authenticate(SmtpAuthMethod AuthMethod, string user, string passwd)
		{
			switch (AuthMethod)
			{
			default:
				logs.AppendLine("SMTP: Skipped Authentication.");
				break;
			case SmtpAuthMethod.Plain:
				AuthenticateWithAuthPlain(user, passwd);
				break;
			case SmtpAuthMethod.Login:
				AuthenticateWithAuthLogin(user, passwd);
				break;
			case SmtpAuthMethod.CramMd5:
				AuthenticateWithCramMd5(user, passwd);
				break;
			}
		}

		//AUTH PLAINで認証を行う
		private void AuthenticateWithAuthPlain(string user, string passwd)
		{
			var code = SmtpSendAndReceive("AUTH PLAIN\r\n");
			if (code == 502)
			{
				//認証の必要なし
				return;
			}
			if (code != 334)
			{
				// 認証プロトコルエラー
				throw new TcpException(smtpcom.received);
			}

			string str = user + '\0' + user + '\0' + passwd;
			code = SmtpSendAndReceive(GetBase64String(str) + "\r\n");
			if (code != 235)
			{
				// 認証エラー
				throw new TcpException(smtpcom.received);
			}
		}

		//AUTH LOGINで認証を行う
		private void AuthenticateWithAuthLogin(string user, string passwd)
		{
			var code = SmtpSendAndReceive("AUTH LOGIN\r\n");
			if (code == 502)
			{
				//認証の必要なし
				return;
			}
			if (code != 334)
			{
				// 認証プロトコルエラー
				throw new TcpException(smtpcom.received);
			}

			code = SmtpSendAndReceive(GetBase64String(user) + "\r\n");
			if (code != 334)
				throw new TcpException(smtpcom.received);

			code = SmtpSendAndReceive(GetBase64String(passwd) + "\r\n");
			if (code != 235)
				throw new TcpException(smtpcom.received);
		}

		//CRAM-MD5で認証を行う
		private void AuthenticateWithCramMd5(string user, string passwd)
		{
			var code = SmtpSendAndReceive("AUTH CRAM-MD5\r\n");
			if (code == 502)
			{
				//認証の必要なし
				return;
			}
			if (code != 334)
			{
				// 認証プロトコルエラー
				throw new TcpException(smtpcom.received);
			}

			code = SmtpSendAndReceive(CreateCramMd5ResponseString(smtpcom.received.Substring(4), user, passwd) + "\r\n");
			if (code != 235)
				throw new TcpException(smtpcom.received);
		}

		//CRAM-MD5で返す文字列を計算する
		private static string CreateCramMd5ResponseString(string challenge, string username, string password)
		{
			//デコードする
			byte[] decCha = Convert.FromBase64String(challenge);
			//passwordをキーとしてHMAC-MD5で暗号化する
			System.Security.Cryptography.HMACMD5 hmacMd5 = new System.Security.Cryptography.HMACMD5(Encoding.UTF8.GetBytes(password));
			byte[] encCha = hmacMd5.ComputeHash(decCha);
			hmacMd5.Clear();
			//16進数の文字列にする
			string hexCha = BitConverter.ToString(encCha).Replace("-", "").ToLower();
			//usernameを付ける
			hexCha = username + " " + hexCha;
			//Base64で文字列にする
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(hexCha));
		}
		#endregion

		#region POPサーバー接続（SMTP接続前）
		private void PopBeforeSmtp(MailParameters mailParams)
		{

			Stream stream = null;
			System.Net.Sockets.TcpClient popclient = null;
			try
			{
				string rstr;
				popclient = new System.Net.Sockets.TcpClient();

				// POPサーバーに接続
				popclient.Connect(mailParams.PopServer, mailParams.PopPort);
				logs.AppendLine("POP: Connected.");

				X509CertificateCollection clientCertificateCollection = new X509CertificateCollection();
				if (mailParams.IsClientCertValidate)
				{
					var clientCertificate = Cert.GetCert(mailParams.ClientCertSerialNo);
					clientCertificateCollection.Add(clientCertificate);
				}

				// サーバーとデータの送受信を行うストリームを取得する
				// 通信開始(SSL有り)
				switch (mailParams.PopSecureMode)
				{
				case SecureMode.SSL2:	// SSL2で運用しているサーバは存在しないはずだが、一応対応しておく
					stream = new System.Net.Security.SslStream(popclient.GetStream(), false, ServerCertificateValidation);
					((System.Net.Security.SslStream)stream).AuthenticateAsClient(mailParams.SmtpServer, clientCertificateCollection, SslProtocols.Ssl2, false);
					logs.AppendLine("POP: socket is over SSL2.");
					break;

				case SecureMode.SSL3:	// SSL3で運用しているサーバはあるかもしれない
					stream = new System.Net.Security.SslStream(popclient.GetStream(), false, ServerCertificateValidation);
					((System.Net.Security.SslStream)stream).AuthenticateAsClient(mailParams.SmtpServer, clientCertificateCollection, SslProtocols.Ssl3, false);
					logs.AppendLine("POP: socket is over SSL3.");
					break;

				case SecureMode.TLS:	// TLSは現状では主流
				case SecureMode.STARTTLS:
					stream = new System.Net.Security.SslStream(popclient.GetStream(), false, ServerCertificateValidation);
					((System.Net.Security.SslStream)stream).AuthenticateAsClient(mailParams.SmtpServer, clientCertificateCollection, SslProtocols.Tls, false);

					logs.AppendLine("POP: socket is over TLS.");
					break;

				case SecureMode.None:
					stream = popclient.GetStream();
					logs.AppendLine("POP: socket unsecure.");
					break;
				}
				stream.ReadTimeout = 5000;
				stream.WriteTimeout = 500;

				//サーバーからのはじめのメッセージを受信

				// POPサーバー接続時のレスポンス受信
				string connectstr = PopWriteAndRead(stream, "");
				if (connectstr.StartsWith("+OK") != true)
				{
					throw new PopException("POPサーバー接続エラー");
				}

				switch (mailParams.PopAuth)
				{
				case PopAuthMethod.Standard:
					// ユーザIDの送信
					rstr = PopWriteAndRead(stream, "USER " + mailParams.PopUserId + "\r\n");
					if (rstr.StartsWith("+OK") != true)
					{
						throw new PopException("ユーザIDエラー");
					}

					// パスワードの送信
					rstr = PopWriteAndRead(stream, "PASS " + mailParams.PopPasswd + "\r\n");
					if (rstr.StartsWith("+OK") != true)
					{
						throw new PopException("パスワードエラー");
					}
					break;
				case PopAuthMethod.APOP:
					// APOP用のタイムスタンプ文字列を取得しておく
					var timestamp = GetAPopTimeStamp(connectstr);
					if (string.IsNullOrWhiteSpace(timestamp))
					{
						throw new PopException("APOP未対応");
					}
					Byte[] byt = System.Text.Encoding.ASCII.GetBytes(string.Format("<{0}>{1}", mailParams.PopUserId, mailParams.PopPasswd));
					System.Security.Cryptography.MD5CryptoServiceProvider md5 =
						new System.Security.Cryptography.MD5CryptoServiceProvider();
					Byte[] res = md5.ComputeHash(byt);
					string aps = BitConverter.ToString(res).Replace("-", "").ToLower();
					rstr = PopWriteAndRead(stream, "APOP " + mailParams.PopUserId + " " + aps + "\r\n");
					if (rstr.StartsWith("+OK") != true)
					{
						throw new PopException("ユーザIDまたはパスワードエラー");
					}
					break;
				case PopAuthMethod.NTLM:
					// ユーザIDの送信
					rstr = PopWriteAndRead(stream, "USER " + mailParams.PopUserId + "\r\n");
					if (rstr.StartsWith("+OK") != true)
					{
						throw new PopException("ユーザIDエラー");
					}

					// パスワードの送信
					rstr = PopWriteAndRead(stream, "PASS " + mailParams.PopPasswd + "\r\n");
					if (rstr.StartsWith("+OK") != true)
					{
						throw new PopException("パスワードエラー");
					}
					break;
				case PopAuthMethod.CramMd5:
					rstr = PopWriteAndRead(stream, "AUTH CRAM-MD5\r\n");
					if (rstr.StartsWith("+OK") != true)
					{
						throw new PopException("CRAM-MD5未対応");
					}

					rstr = PopWriteAndRead(stream, CreateCramMd5ResponseString(rstr.Substring(4), mailParams.PopUserId, mailParams.PopPasswd) + "\r\n");
					if (rstr.StartsWith("+OK") != true)
					{
						throw new PopException("認証エラー");
					}
					break;
				}

				// ステータスの送信
				rstr = PopWriteAndRead(stream, "STAT" + "\r\n");
				if (rstr.StartsWith("+OK") != true)
				{
					throw new PopException("STATエラー");
				}

				// 終了の送信
				rstr = PopWriteAndRead(stream, "QUIT" + "\r\n");
				// 戻り値は無視
			}
			catch (PopException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new PopException("内部例外発生", ex);
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
					stream.Dispose();
				}
				if (popclient != null)
				{
					popclient.Close();
				}
			}
		}

		//APOP認証用のタイムスタンプを切り取る
		private string GetAPopTimeStamp(string receiveStr)
		{
			receiveStr = receiveStr.Trim();
			System.Text.RegularExpressions.Regex reg
			  = new System.Text.RegularExpressions.Regex(@"(?<TimeStamp>\<.*\>?)");
			System.Text.RegularExpressions.Match match = reg.Match(receiveStr);

			string timeStamp = "";
			if (match.Success == true)
			{
				timeStamp = match.Groups["TimeStamp"].Value;
			}
			return timeStamp;
		}

		/// <summary>
		/// POPサーバ送受信
		/// </summary>
		/// <param name="stream">ストリーム</param>
		/// <param name="req">リクエスト</param>
		/// <returns>レスポンス</returns>
		//private string PopWriteAndRead(StreamReader reader, StreamWriter writer, string req)
		private string PopWriteAndRead(Stream stream, string req)
		{
			// POPサーバへリクエスト送信
			if (string.IsNullOrWhiteSpace(req) != true)
			{
				byte[] buf = System.Text.ASCIIEncoding.ASCII.GetBytes(req);
				stream.Write(buf, 0, buf.Length);
				logs.Append("> " + req);
				if (req.EndsWith("\n") != true)
				{
					logs.AppendLine();
				}
			}

			// POPサーバからのレスポンス受信
			//string line = reader.ReadLine();
			List<byte> rbuf = new List<byte>();
			int r = 0;
			do
			{
				r = stream.ReadByte();
				if (r < 0)
				{
					break;
				}
				rbuf.Add((byte)r);
			} while (r != '\n');
			string line = System.Text.ASCIIEncoding.ASCII.GetString(rbuf.ToArray(), 0, rbuf.Count);
			logs.Append("< " + line);
			if (line.EndsWith("\n") != true)
			{
				logs.AppendLine();
			}

			// レスポンス返信
			return line;
		}
		#endregion


	}
}
