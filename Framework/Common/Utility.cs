using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Deployment.Application;

namespace KyoeiSystem.Framework.Common
{
	/// <summary>
	/// ユーティリティクラス
	/// </summary>
	public static class Utility
	{
		// 128bit(16byte)のIV（初期ベクタ）とKey（暗号キー）
		// これを変更すると、暗号化した文字列を再設定することになる
		private const string AesIV = @"P@sasRTER+$%532G";
		private const string AesKey = @"ASSK!&%S341!$1EE";
		private static string BaseDataDir
			= ApplicationDeployment.IsNetworkDeployed ? ApplicationDeployment.CurrentDeployment.DataDirectory : @"."
			;
		private static string ApplicationCofigFileName
			= BaseDataDir + @"\ApplicationConfig.xml";
		private static string DataAccessCofigFileName
			= BaseDataDir + @"\WCFDataAccess.xml";

		/// <summary>
		/// 文字列をAESで暗号化する
		/// </summary>
		/// <param name="text">暗号化対象文字列（平分）</param>
		/// <param name="key">暗号化キー</param>
		/// <returns>暗号化した文字列</returns>
		public static string Encrypt(string text, string key = null)
		{
			// AES暗号化サービスプロバイダ
			AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
			aes.BlockSize = 128;
			aes.KeySize = 128;
			aes.IV = Encoding.UTF8.GetBytes(AesIV);
			string keystr = string.IsNullOrWhiteSpace(key) ? AesKey : key;
			int kdiff = AesKey.Length - keystr.Length;
			if (kdiff > 0)
			{
				keystr += AesKey.Substring(0,kdiff);
			}
			aes.Key = Encoding.UTF8.GetBytes(keystr);
			aes.Mode = CipherMode.CBC;
			aes.Padding = PaddingMode.PKCS7;

			// 文字列をバイト型配列に変換
			byte[] src = Encoding.Unicode.GetBytes(text);

			// 暗号化する
			using (ICryptoTransform encrypt = aes.CreateEncryptor())
			{
				byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);

				// バイト型配列からBase64形式の文字列に変換
				return Convert.ToBase64String(dest);
			}
		}

		/// <summary>
		/// 文字列をAESで復号化
		/// </summary>
		/// <param name="text">暗号化された文字列</param>
		/// <param name="key">暗号化キー</param>
		/// <returns>複合化した文字列</returns>
		public static string Decrypt(string text, string key = null)
		{
			// AES暗号化サービスプロバイダ
			AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
			aes.BlockSize = 128;
			aes.KeySize = 128;
			aes.IV = Encoding.UTF8.GetBytes(AesIV);
			string keystr = string.IsNullOrWhiteSpace(key) ? AesKey : key;
			int kdiff = AesKey.Length - keystr.Length;
			if (kdiff > 0)
			{
				keystr += AesKey.Substring(0, kdiff);
			}
			aes.Key = Encoding.UTF8.GetBytes(keystr);
			aes.Mode = CipherMode.CBC;
			aes.Padding = PaddingMode.PKCS7;

			// Base64形式の文字列からバイト型配列に変換
			byte[] src = System.Convert.FromBase64String(text);

			// 複号化する
			using (ICryptoTransform decrypt = aes.CreateDecryptor())
			{
				byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
				return Encoding.Unicode.GetString(dest);
			}
		}


	}
}
