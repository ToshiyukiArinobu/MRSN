using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KyoeiSystem.Framework.Net
{
	/// <summary>
	/// 証明書制御クラス
	/// </summary>
	public static class Cert
	{
		/// <summary>
		/// インストール済みの証明書リストを取得する
		/// </summary>
		/// <returns>証明書リスト</returns>
		public static X509Certificate2Collection GetCertCollection()
		{
			X509Store store = null;
			try
			{
				store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
				store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);
				return store.Certificates;
			}
			catch (Exception)
			{
				return new X509Certificate2Collection();
			}
			finally
			{
				if (store != null) store.Close();
			}

		}

		/// <summary>
		/// シリアル番号をもとに証明書のインスタンスを取得する
		/// </summary>
		/// <param name="serialno">シリアル番号</param>
		/// <returns>証明書インスタンス</returns>
		public static X509Certificate2 GetCert(string serialno)
		{
			X509Store store = null;
			try
			{
				store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
				store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);
				var list = store.Certificates.Find(X509FindType.FindBySerialNumber, serialno, false);
				//var list = store.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, subjectname, false);
				if (list.Count > 0)
				{
					// シリアル番号で検索するので、1件しかヒットしないはず
					return list[0];
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
			finally
			{
				if (store != null) store.Close();
			}

		}

	}
}
