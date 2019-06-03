using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Diagnostics;
using System.Reflection;

using KyoeiSystem.Framework.Common;
using System.ServiceModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Net.Security;
using System.Xml.Serialization;

namespace KyoeiSystem.Framework.Core
{
	public partial class ThreadManeger
	{
		private void WcfClient(object param)
		{
			// オレオレ証明書に対する処置
			ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(OnRemoteCertificateValidationCallback);

			ThreadContext thc = param as ThreadContext;
			thc.isRunnig = true;

			string dataname = thc.commdata.GetMessageName();
			this.appLog.Debug("<DA> DataAccessThread ({0})", dataname);
			object[] reqparams = thc.commdata.GetParameters();
			string fmt = string.Empty;
			for (int i = 0; i < reqparams.Length; i++)
			{
				fmt += (string.IsNullOrEmpty(fmt) ? "" : ", ") + string.Format("p{1}={0}{1}{2}", '{', i, '}');
			}
			if (!string.IsNullOrEmpty(fmt))
			{
				this.appLog.Debug("<DA> parameter:" + fmt, reqparams);
			}

			try
			{
				if (string.IsNullOrWhiteSpace(dataname))
				{
					throw new FWThreadCoreDataException(ConstDataAccess.ErrDataTypeUnknown);
				}

				string kind;
				if (thc.commdata.mType == MessageType.RequestLicense)
				{
					kind = "common";
				}
				else
				{
					kind = "user";
				}
				string cstr = thc.commdata.connection;

				List<string> types = new List<string>();
				var values = new List<object>();
				foreach (var item in thc.commdata.GetParameters())
				{
					if (item == null)
					{
						types.Add(string.Empty);
						values.Add(null);
					}
					else
					{
						types.Add(item.GetType().FullName);
						values.Add(GetSerializedArray(item));
					}
				}
				// ターゲットのメソッドを呼び出す
				var result = GetClient().CallFunction(dataname, kind, cstr, types.ToArray(), values.ToArray());
				//var tp = GetClient().GetType();
				//MethodInfo mi = tp.GetMethod("CallFunction");
				//var result = mi.Invoke(null, new object[] { dataname, kind, cstr, types.ToArray(), values.ToArray(), });

				if (result != null)
				{
					byte[] binret = result as byte[];
					if (binret != null)
					{
						// バイナリで返ってきた応答はDataTableまたはDataSetである。
						byte id = binret[0];
						if (id == 0x01)
						{
							XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
							var strm = new System.IO.MemoryStream(binret, 1, binret.Length - 1);
							DataTable value = (DataTable)serializer.Deserialize(strm);
							strm.Close();
							result = value;
						}
						else
						{
							XmlSerializer serializer = new XmlSerializer(typeof(DataSet));
							var strm = new System.IO.MemoryStream(binret, 1, binret.Length - 1);
							DataSet value = (DataSet)serializer.Deserialize(strm);
							strm.Close();
							result = value;
						}
					}
				}

				this.appLog.Debug("<DA> Method: {0}.{1}, return: {2}", dataname, ""
					, (result == null) ? "null" : result.GetType().FullName
					);

				// 結果セットを送信
				MessageType mtp;
				if (thc.commdata.mType == MessageType.RequestDataWithBusy)
				{
					mtp = MessageType.ResponseWithFree;
				}
				else
				{
					mtp = MessageType.ResponseData;
				}
				this.OnReceived.Invoke(new CommunicationObject(mtp, dataname, result));

			}
			catch (Exception ex)
			{
				// エラーを通知
				string msg;
				MessageErrorType errtype = MessageErrorType.SystemError;
				if (ex.InnerException != null)
				{
					msg = ex.InnerException.Message;
					if (ex.InnerException is DBOpenException)
					{
						errtype = MessageErrorType.DBConnectError;
					}
					else if (ex.InnerException is DBGetException)
					{
						errtype = MessageErrorType.DBGetError;
					}
					else if (ex.InnerException is DBPutException)
					{
						errtype = MessageErrorType.DBUpdateError;
					}
					else if (ex.InnerException is DBUpdateConflictException)
					{
						errtype = MessageErrorType.UpdateConflict;
					}
					else
					{
						switch (thc.commdata.mType)
						{
						case MessageType.RequestData:
							errtype = MessageErrorType.DBGetError;
							break;
						case MessageType.UpdateData:
							errtype = MessageErrorType.DBUpdateError;
							break;
						case MessageType.CallStoredProcedure:
							errtype = MessageErrorType.DataError;
							break;
						default:
							errtype = MessageErrorType.SystemError;
							break;
						}
					}
				}
				else
				{
					msg = ex.Message;
				}
				this.appLog.Error(string.Format("<DataAccess> 例外発生：REQ={0}, message={1}", dataname, msg), ex);
				MessageType mtp;
				if (thc.commdata.mType == MessageType.RequestDataWithBusy)
				{
					mtp = MessageType.ErrorWithFree;
				}
				else
				{
					mtp = MessageType.Error;
				}
				this.OnReceived.Invoke(new CommunicationObject(mtp, errtype, dataname, string.Format("{0} [{1}]", ConstDataAccess.Error, msg)));
			}
			thc.isRunnig = false;
		}

		private IKESService GetClient()
		{
			// return new KESSVC.KESServiceClient();

			var plist = (NameValueCollection)ConfigurationManager.GetSection("serviceSettings");
			var address = plist["service"];

			var binding = new WSHttpBinding("WSHttpBinding_IKESService");

			// サーバー証明書を使ったトランスポートセキュリティを有効にする
			binding.Security.Mode = SecurityMode.Transport;
			binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;

			var client = ChannelFactory<KyoeiSystem.Framework.Core.IKESService>.CreateChannel(
				binding,
				new EndpointAddress(
					new Uri(address),
					EndpointIdentity.CreateDnsIdentity("localhost")
				)
				);

			//Assembly sAssembly = Assembly.LoadFrom(DataAccessConst.APLWCFDLL);
			//Type stype = sAssembly.GetType("KyoeiSystem.Framework.Core.IKESService");
			//var factoryType = typeof(ChannelFactory<>).MakeGenericType(stype);
			//var client = (IKESService)Activator.CreateInstance(factoryType, new object[] { binding, new EndpointAddress(new Uri(address), EndpointIdentity.CreateDnsIdentity("localhost")) });

			return client;
		}

		private static ChannelFactory CreateChannel(string interfaceName, string endpoint)
		{
			Type myInterfaceType = Type.GetType("IKESService");
			var factoryType = typeof(ChannelFactory<>).MakeGenericType(myInterfaceType);
			return (ChannelFactory)Activator.CreateInstance(factoryType, new object[] { new BasicHttpBinding(), new EndpointAddress(endpoint) });
		}

		private static byte[] GetSerializedArray(object objGraph)
		{
			try
			{
				if (objGraph is DataSet)
				{
					if (string.IsNullOrWhiteSpace((objGraph as DataSet).DataSetName))
					{
						(objGraph as DataSet).DataSetName = "DATASET";
						for (int i = 0; i < (objGraph as DataSet).Tables.Count; i++)
						{
							if (string.IsNullOrWhiteSpace((objGraph as DataSet).Tables[i].TableName))
							{
								(objGraph as DataSet).Tables[i].TableName = string.Format("DATATABLE{0}", i);
							}
						}
					}
				}
				else if (objGraph is DataTable)
				{
					if (string.IsNullOrWhiteSpace((objGraph as DataTable).TableName))
					{
						(objGraph as DataTable).TableName = "DATATABLE";
					}
				}
				else if (objGraph is DataRow)
				{
					if (string.IsNullOrWhiteSpace((objGraph as DataRow).Table.TableName))
					{
						(objGraph as DataRow).Table.TableName = "DATATABLE";
					}
					DataTable tbl = (objGraph as DataRow).Table.Copy();
					objGraph = tbl;
				}
				byte[] barray;
				System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(objGraph.GetType());
				var strm = new System.IO.MemoryStream();
				serializer.Serialize(strm, objGraph);
				barray = strm.ToArray();
				strm.Close();
				return barray;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private static long GetSerializedLength(object objGraph)
		{
			byte[] barray;
			//if ((objGraph is int))
			//{
			//	return sizeof(int);
			//}
			System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(objGraph.GetType());
			var strm = new System.IO.MemoryStream();
			serializer.Serialize(strm, objGraph);
			barray = strm.ToArray();
			strm.Close();
			return barray.LongLength;
		}

		/// <summary>
		/// 証明書チェックコールバック処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="certificate"></param>
		/// <param name="chain"></param>
		/// <param name="sslPolicyErrors"></param>
		/// <returns></returns>
		private bool OnRemoteCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

	}
}
