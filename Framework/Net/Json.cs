using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.IO;

namespace KyoeiSystem.Framework.Net
{
	/// <summary>
	/// Jsonデータのシリアライザ利用のためのインターフェース
	/// </summary>
	public interface IJsonData
	{
	}

	/// <summary>
	/// Json形式のデータ変換機能（シリアライザによる）
	/// </summary>
	public static class Json
	{
		/// <summary>
		/// Json形式の文字列からC#で扱うデータに変換する
		/// </summary>
		/// <typeparam name="T">内部データ型</typeparam>
		/// <param name="jsontext">Json形式文字列</param>
		/// <returns>デシリアライズした内部データ型のコレクション</returns>
		public static T ToData<T>(string jsontext) where T : IJsonData
		{
			try
			{
				byte[] contents = System.Text.Encoding.UTF8.GetBytes(jsontext);
				MemoryStream strm = new MemoryStream();
				strm.Write(contents, 0, contents.Length);
				System.Runtime.Serialization.Json.DataContractJsonSerializer ser = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));

				strm.Position = 0;
				T p2 = (T)ser.ReadObject(strm);

				return p2;
			}
			catch (Exception ex)
			{
				throw new WebAPIFormatException(string.Format("{0}にデシリアライズ化できません。", typeof(T)), ex);
			}
			finally
			{
			}
		}

		/// <summary>
		/// C#のデータからJson形式の文字列に変換する
		/// </summary>
		/// <typeparam name="T">内部データ型</typeparam>
		/// <param name="data">シリアライズ化対象の内部データ型データコレクション</param>
		/// <returns>Json形式の文字列</returns>
		public static string FromData<T>(T data) where T : IJsonData
		{
			try
			{
				MemoryStream strm = new MemoryStream();
				System.Runtime.Serialization.Json.DataContractJsonSerializer ser = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
				ser.WriteObject(strm, data);
				strm.Position = 0;
				StreamReader sr = new StreamReader(strm);
				string jsontext = sr.ReadToEnd();

				return jsontext;
			}
			catch (Exception ex)
			{
				throw new WebAPIFormatException(string.Format("{0}をJson形式にシリアライズ化できません。", typeof(T)), ex);
			}
			finally
			{
			}
		}

	}
}
