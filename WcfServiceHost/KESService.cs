using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml.Serialization;
using KyoeiSystem.Application.WCFService;
using KyoeiSystem.Framework.Common;

namespace WcfServiceHost
{
	/// <summary>
	/// 運坊専用WCFサービス
	/// </summary>
	public class KESService : IKESService
	{
		AppLogger applog = AppLogger.Instance;
		/// <summary>
		/// WCFサービス入り口用メソッド
		/// </summary>
		/// <param name="fname"></param>
		/// <param name="kind"></param>
		/// <param name="cstr"></param>
		/// <param name="ptypes"></param>
		/// <param name="plist"></param>
		/// <returns></returns>
		public object CallFunction(string fname, string kind, string cstr, string[] ptypes, object[] plist)
		{
			string clientaddr = "unknown";

			try
			{
				MessageProperties messageProperties = OperationContext.Current.IncomingMessageProperties;

				var endpointProperties = messageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
				if (endpointProperties != null)
				{
					clientaddr = string.Format("{0}:{1}", endpointProperties.Address, endpointProperties.Port);
				}

				applog.Info("called [{0}] from {1}", fname, clientaddr);
				int len = 0;
				// パラメータのデシリアライズ
				List<object> parameters = new List<object>();
				for (int i = 0; i < ptypes.Length; i++)
				{
					if (plist[i] == null)
					{
						parameters.Add(null);
						continue;
					}
					var bin = plist[i] as byte[];
					Type tp = null;
					if (ptypes[i].Contains("DataTable"))
					{
						tp = typeof(DataTable);
					}
					else if (ptypes[i].Contains("DataSet"))
					{
						tp = typeof(DataSet);
					}
					else if (ptypes[i].Contains("DataRow"))
					{
						tp = typeof(DataTable);
					}
					else
					{
						tp = Type.GetType(ptypes[i]);
					}
					if (tp == null)
					{
						parameters.Add(bin);
						continue;
					}
					XmlSerializer serializer = new XmlSerializer(tp);
					var strm = new System.IO.MemoryStream(bin);
					var value = serializer.Deserialize(strm);
					strm.Close();
					parameters.Add(value);
					len += bin.Length;
				}
				applog.Debug("[{0}] parameter : {1} bytes.", fname, len);
				// 各メソッドの呼び出し
				var svc = new KESSVCEntry();
				var result = svc.CallFunction(fname, kind, cstr, ptypes, parameters.ToArray());

				// 戻り値のシリアライズ
				if (result != null)
				{
					List<byte> data = new List<byte>();
					if (result is DataTable)
					{
						// TableNameが空だったら名前を付与する
						if (string.IsNullOrWhiteSpace((result as DataTable).TableName))
						{
							(result as DataTable).TableName = fname;
						}
						data.Add(0x01);
						data.AddRange(GetSerializedArray(result));
						applog.Info("complete [{0}] {1} bytes.", fname, data.Count());
						return data.ToArray();
					}
					else if (result is DataSet)
					{
						// DataSetNameが空だったら名前を付与する
						if (string.IsNullOrWhiteSpace((result as DataSet).DataSetName))
						{
							(result as DataSet).DataSetName = fname;
						}
						foreach (DataTable tbl in (result as DataSet).Tables)
						{
							// TableNameが空だったら名前を付与する
							int tblidx = 0;
							if (string.IsNullOrWhiteSpace(tbl.TableName))
							{
								tbl.TableName = string.Format("Table{1}", tblidx++);
							}
						}
						data.Add(0x02);
						data.AddRange(GetSerializedArray(result));
						applog.Info("complete [{0}] {1} bytes.", fname, data.Count());
						return data.ToArray();
					}
				}

				applog.Info("complete [{0}] {1} bytes.", fname, result == null ? 0 : GetSerializedArray(result).Length);
				return result;

			}
			catch (Exception ex)
			{
				applog.Error(string.Format("[{0}]", fname), ex);
				throw ex;
			}
			finally
			{
			}
		}

		private byte[] GetSerializedArray(object objGraph)
		{
			byte[] barray;
			System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(objGraph.GetType());
			var strm = new System.IO.MemoryStream();
			serializer.Serialize(strm, objGraph);
			barray = strm.ToArray();
			strm.Close();
			return barray;
		}

	}
}
