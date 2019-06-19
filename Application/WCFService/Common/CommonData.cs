using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Data;

// 20150811 wada add
using System.Data.SqlClient;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using KyoeiSystem.Framework.Common;
using System.Collections.Specialized;
using System.Configuration;

namespace KyoeiSystem.Application.WCFService
{
	/// <summary>
	/// 20150806 wada add WCF接続情報共有
	/// </summary>
	public static class CommonData
	{
		static public string ConnectStringCommonDB;
		static public string ConnectStringUserDB;

        // string型のデータは暗号化した値を格納
		static public string CommonDB_DBServer;                 // DB接続先
		static public string CommonDB_DBName;                   // DB名
		static public string CommonDB_DBId;                     // DB接続ID
		static public string CommonDB_DBPass;                   // DB接続パスワード

		static public string UserDB_DBServer;                 // DB接続先
		static public string UserDB_DBName;                   // DB名
		static public string UserDB_DBId;                     // DB接続ID
		static public string userDB_DBPass;                   // DB接続パスワード

		/// <summary>
        /// 接続文字列生成（本体DB）
        /// </summary>
        /// <returns>接続文字列</returns>
        static public string TRAC3_GetConnectionString()
        {
			SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
			sqlBuilder.ConnectionString = Utility.Decrypt(ConnectStringUserDB);
			sqlBuilder.TrustServerCertificate = true;
			EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
			entityBuilder.Provider = "System.Data.SqlClient";
			entityBuilder.ProviderConnectionString = sqlBuilder.ConnectionString;

			string metadata = "9O9NVsTUbdOcxcpxsNC1RdrYhy+/wziZV2frmoEC35geldL0dwiTKI/fkP5ynrLf5MNsMqzc6vkDfjZJDIYxiOu/xSjTIUhSKY4mZqOYTSsoKy8SHwFFlN+La616LmLfZg90l50CouHAeiw8Q0/Hyg==";
			entityBuilder.Metadata = Utility.Decrypt(metadata);
			return entityBuilder.ToString();

        }

        /// <summary>
        /// /// <summary>
        /// 接続文字列生成（本体DB）
        /// </summary>
        /// <param name="constr">暗号化接続文字列</param>
        /// <returns>接続文字列</returns>
        static public string TRAC3_SetConnectionString(string constr)
        {
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            sqlBuilder.ConnectionString = Utility.Decrypt(constr);
            sqlBuilder.TrustServerCertificate = true;
            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
            entityBuilder.Provider = "System.Data.SqlClient";
            entityBuilder.ProviderConnectionString = sqlBuilder.ConnectionString;

            string metadata = "9O9NVsTUbdOcxcpxsNC1RdrYhy+/wziZV2frmoEC35geldL0dwiTKI/fkP5ynrLf5MNsMqzc6vkDfjZJDIYxiOu/xSjTIUhSKY4mZqOYTSsoKy8SHwFFlN+La616LmLfZg90l50CouHAeiw8Q0/Hyg==";
            entityBuilder.Metadata = Utility.Decrypt(metadata);
            return entityBuilder.ToString();

        }


		/// <summary>
		/// 接続文字列生成（本体DB）SQL直接接続用
		/// </summary>
		/// <returns>接続文字列</returns>
		static public string TRAC3_SQL_GetConnectionString()
		{
			SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
			sqlBuilder.ConnectionString = Utility.Decrypt(ConnectStringUserDB);
			return "Data Source=" + sqlBuilder.DataSource.ToString() + ";Initial Catalog=" + sqlBuilder.InitialCatalog.ToString() + ";Integrated Security=" + sqlBuilder.IntegratedSecurity.ToString() + ";Persist Security Info=" + sqlBuilder.PersistSecurityInfo.ToString() + ";User ID=" + sqlBuilder.UserID.ToString() + ";Password=" + sqlBuilder.Password.ToString() + ";";
		}

        /// <summary>
        /// 接続文字列生成（共通DB）
        /// </summary>
        /// <returns>接続文字列</returns>
        static public string COMMON_GetConnectionString()
        {
			// config で切り替え
			var plist = (NameValueCollection)ConfigurationManager.GetSection("serviceSettings");
			if (plist == null)
			{
				return string.Empty;
			}
			string dbconnstr = plist["connectionString"];
			if (plist["localdb"] == "encrypt")
			{
				// 暗号化された文字列を復号化
				dbconnstr = Utility.Decrypt(dbconnstr);
				dbconnstr = dbconnstr.Replace("&quot;", "\"");
			}
			else
			{
				// 平分で取得
			}

			return dbconnstr;

        }
    }


}
