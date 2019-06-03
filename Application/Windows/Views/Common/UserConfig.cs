using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XmlConfiguration;

namespace KyoeiSystem.Application.Windows.Views
{
	public class UserConfig
	{
		public System.Xml.XmlDocument config = new System.Xml.XmlDocument();

		//public List<object> ConfigList = new List<object>();

		public UserConfig()
		{
			Initialize();
		}

		public void Initialize()
		{
			try
			{
				config = new System.Xml.XmlDocument();
				var elmnt = config.CreateElement("UserConfig");
				config.AppendChild(elmnt);
				var node = config.CreateNode(System.Xml.XmlNodeType.Element, "COMMON", null);
				elmnt.AppendChild(node);
			}
			catch (Exception)
			{
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="keyname">"COMMON"または画面ID</param>
		/// <returns>該当する設定項目のインスタンス</returns>
		public object GetConfigValue(Type type)
		{
			try
			{
				string keyname = type.Name;
				var nodelist = config.GetElementsByTagName(keyname);
				if (nodelist.Count == 0)
				{
					return null;
				}
				var data = nodelist.Item(0);
				var bin = Convert.FromBase64String(data.InnerText);
				System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(type);
				var strm = new System.IO.MemoryStream(bin);
				var value = serializer.Deserialize(strm);
				strm.Close();

				return value;
			}
			catch (Exception ex)
			{
				return null;
			}

		}

		//public T GetConfigVal<T>() where T : class, new()
		//{
		//	try
		//	{
		//		var value = GetConfigValue(typeof(T));
		//		if ((value is T) != true)
		//		{
		//			value = new T();
		//		}
		//		return value as T;
		//	}
		//	catch (Exception ex)
		//	{
		//		return new T();
		//	}
		//}

		/// <summary>
		/// ユーザ設定項目を変更する
		/// </summary>
		/// <param name="keyname">"COMMON"または画面ID</param>
		/// <param name="value"></param>
		public void SetConfigValue(object value)
		{
			try
			{
				string keyname = value.GetType().Name;
				var nodelist = config.GetElementsByTagName(keyname);
				if (nodelist.Count == 0)
				{
					var elmnt = config.CreateElement(keyname);
					config.DocumentElement.AppendChild(elmnt);
					nodelist = config.GetElementsByTagName(keyname);
				}

				byte[] barray;
				System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(value.GetType());
				var strm = new System.IO.MemoryStream();
				serializer.Serialize(strm, value);
				barray = strm.ToArray();
				strm.Close();

				var dat = config.CreateCDataSection(Convert.ToBase64String(barray, Base64FormattingOptions.InsertLineBreaks));
				nodelist.Item(0).RemoveAll();
				nodelist.Item(0).AppendChild(dat);

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

	}
}
