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
using System.Collections.Specialized;
using System.Configuration;

using KyoeiSystem.Framework.Common;

namespace KyoeiSystem.Framework.Core
{
	public partial class ThreadManeger
	{

		private void DataAccessThread(object param)
		{
			// config で切り替え
			var plist = (NameValueCollection)ConfigurationManager.GetSection("serviceSettings");
			if (plist["mode"] == CommonConst.LocalDBMode || plist["mode"] == CommonConst.WithoutLicenceDBMode)
			{
				// ローカルDLL呼び出しモード
				DataAccessLocal(param);
			}
			else
			{
				// WCFサービス呼び出しモード
				WcfClient(param);
			}

		}

		/// <summary>
		/// データアクセスメソッドの実行
		/// </summary>
		/// <param name="param">スレッド起動時に渡されるパラメータ(ThreadContext)</param>
		private void DataAccessLocal(object param)
		{
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
				List<object> reqdata = new List<object>();
				reqdata.AddRange(thc.commdata.GetParameters().ToList());

				Assembly sAssembly = Assembly.LoadFrom(DataAccessConst.APLWCFDLL);
				Type stype = sAssembly.GetType(DataAccessConst.DACCLASSNAME);
				MethodInfo miSE = stype.GetMethod(DataAccessConst.GETCONFIGMETHODNAME);
				var cfg = miSE.Invoke(null, new object[] { dataname, }) as KyoeiSystem.Framework.Common.WCFDataAccessConfig;
				if (cfg == null)
				{
					throw new FWThreadCoreDataException(string.Format("{0} : {1}", dataname, ConstDataAccess.ErrUnknownMethod));
				}

				Assembly oAssembly = Assembly.LoadFrom(cfg.Dll);

				// 接続情報をセット
				setupConnectString(oAssembly, thc.commdata);

				// データサービスのクラスを取得
				string svcname = cfg.Namespace + "." + cfg.ServiceClass;
				Type svctype = oAssembly.GetType(svcname);
				var da = Activator.CreateInstance(svctype);

				// データ登録メソッドを取得
				MethodInfo svcMethodInfo = null;
				ParameterInfo[] plist = null;

				svcMethodInfo = svctype.GetMethod(cfg.MethodName);
				if (svcMethodInfo == null)
				{
					this.appLog.Debug("<DA> Method: {0}.{1}", cfg.ServiceClass, cfg.MethodName);
					throw new FWThreadCoreDataException(ConstDataAccess.ErrUnknownMethod);
				}
				plist = svcMethodInfo.GetParameters();

				// 引数を準備
				int idx = 0;
				List<object> parameters = new List<object>();
				foreach (object prm in reqdata)
				{
					try
					{
						// 引数がDataTableの場合は配列のインスタンスに変換
						parameters.Add(ConvertFromDataTable(plist[idx++].ParameterType, prm));
					}
					catch (Exception ex)
					{
						string msg = ex.Message;
					}
				}

				// ターゲットのメソッドを呼び出す
				var dat = svcMethodInfo.Invoke(da, parameters.ToArray());

				this.appLog.Debug("<DA> Method: {0}.{1}, return: {2}", cfg.ServiceClass, cfg.MethodName
					, (dat == null) ? "null" : (dat.GetType().IsGenericType) ?
						"List<" + (dat.GetType().GetGenericArguments()[0 ].Name) + ">" : (dat)
					);

				object result = null;
				// 戻り値を処理する
				if (dat != null)
				{
					if (thc.commdata.mType == MessageType.RequestDataNoManage)
					{
						result = dat;
					}
					else
					{
						// データメンバーからデータテーブルのカラムを作成
                        if (dat.GetType().IsGenericType)
						{
                            var openType = dat.GetType().GetGenericTypeDefinition();
                            if (openType == typeof(Dictionary<,>))
                            {
                                // ディクショナリはそのまま返す
                                result = dat;
                            }
                            else
                            {
                                Type[] ts = dat.GetType().GetGenericArguments();
                                DataTable table = new DataTable(dataname);
                                setupColumnsByProperties(ts[0], table);
                                convertToDataTable(dat, table);
                                result = table;

                            }

                        }
						else
						{
							if (dat.GetType().IsNested)
							{
								DataSet ds = new DataSet(dataname);
								foreach (var fld in dat.GetType().GetFields())
								{
									var datN = fld.GetValue(dat);
									Type[] ts2 = datN.GetType().GetGenericArguments();
									DataTable table = ds.Tables.Add(fld.Name);
									setupColumnsByProperties(ts2[0], table);
									convertToDataTable(datN, table);
									//result = table;
								}
								result = ds;
							}
							else
							{
								result = dat;
							}
						}
					}
				}
				else
				{
					result = null;
				}

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

		private void setupConnectString(Assembly asm, CommunicationObject comobj)
		{
			try
			{
				// データサービスのクラスを取得
				Type cmntype = asm.GetType("KyoeiSystem.Application.WCFService.CommonData");
				string fnm = string.Empty;
				if (comobj.mType == MessageType.RequestLicense)
				{
					fnm = "ConnectStringCommonDB";
				}
				else
				{
					fnm = "ConnectStringUserDB";
				}
				FieldInfo fld = cmntype.GetField(fnm);
				fld.SetValue(null, comobj.connection);
			}
			catch (Exception ex)
			{
			}
		}

		private void setupColumnsByProperties(Type tp, DataTable tbl)
		{

			//メンバを取得する
			PropertyInfo[] props = tp.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach (PropertyInfo pi in props)
			{
				Type t = pi.PropertyType;
				if (t.Name.Contains("ChangeTracker"))
					continue;
				bool isnullable = false;
				if (t.IsGenericType)
				{
					isnullable = t.GetGenericTypeDefinition() == typeof(Nullable<>);
					if (isnullable)
					{
						t = Nullable.GetUnderlyingType(t);
					}
				}
				DataColumn col = new DataColumn(pi.Name, t);
				col.AllowDBNull = true;
				tbl.Columns.Add(col);
			}
		}

		private void convertToDataTable(object data, DataTable tbl)
		{
			try
			{
				BindingFlags bindf = BindingFlags.Public | BindingFlags.Instance;
				Type tp = data.GetType();
				if (tp.IsArray)
				{
					// 単純配列型（XXX[]）の場合
					PropertyInfo arrayp = tp.GetProperty("Item");
					var ary = (object[])data;
					for (int ix = 0; ix < ary.Length; ix++)
					{
						GetValues(ary[ix].GetType().GetProperties(bindf), ary[ix], tbl);
					}
				}
				else
				{
					if (tp.IsGenericType && typeof(List<>).IsAssignableFrom(tp.GetGenericTypeDefinition()))
					{
						// ジェネリックコレクション List<XXX>の場合
						Type ts = tp.GetProperty("Item").PropertyType;
						PropertyInfo arrayc = tp.GetProperty("Count");
						int cnt = (int)arrayc.GetValue(data, null);
						for (int ix = 0; ix < cnt; ix++)
						{
							PropertyInfo arrayp = tp.GetProperty("Item");
							var ary = arrayp.GetValue(data, new object[] { ix });
							GetValues(ary.GetType().GetProperties(bindf), ary, tbl);
						}
					}
					else
					{
						// ジェネリックコレクション List<XXX>ではない場合
						GetValues(tp.GetProperties(bindf), data, tbl);
					}
				}

			}
			catch (Exception ex)
			{
				throw new FWThreadCoreDataException(ConstDataAccess.Error, ex);
			}
		}

		private void GetValues(PropertyInfo[] props, object data, DataTable tbl)
		{
			DataRow row = tbl.NewRow();
			//メンバを取得する
			foreach (PropertyInfo pi in props)
			{
				if (pi.Name.Contains("ChangeTrack"))
					continue;
				var col = pi.GetValue(data);
				if (col == null)
				{
					row[pi.Name] = DBNull.Value;
				}
				else
				{
					row[pi.Name] = pi.GetValue(data);
				}
			}
			tbl.Rows.Add(row);
			row.AcceptChanges();
		}

		private object ConvertFromDataTable(Type ptype, object param)
		{
			try
			{
				if (param == null)
				{
					return param;
				}

				if ((param is DataRow) != true && (param is DataTable) != true)
				{
					return param;
				}

				if (ptype.IsArray)
				{
					// 単純配列型（XXX[]）の場合
					List<object> data = new List<object>();
					if (param is DataRow)
					{
						if ((param as DataRow).RowState != DataRowState.Deleted)
						{
							data.Add(SetValues(ptype, param as DataRow));
						}
					}
					else if (param is DataTable)
					{
						foreach (DataRow row in (param as DataTable).Rows)
						{
							if (row.RowState != DataRowState.Deleted)
							{
								data.Add(SetValues(ptype.GetProperty("Item").PropertyType, row));
							}
						}
					}
					return data.ToArray();
				}
				else
				{
					if (ptype.IsGenericType && typeof(List<>).IsAssignableFrom(ptype.GetGenericTypeDefinition()))
	                                                                                                                   				{
						object dat = Activator.CreateInstance(ptype);
						MethodInfo mAdd = dat.GetType().GetMethod("Add");
						// ジェネリックコレクション List<XXX>の場合
						if (param is DataRow)
						{
							if ((param as DataRow).RowState != DataRowState.Deleted)
							{
								mAdd.Invoke(dat, new object[] { SetValues(ptype.GetProperty("Item").PropertyType, param as DataRow) });
							}
						}
						else if (param is DataTable)
						{
							foreach (DataRow row in (param as DataTable).Rows)
							{
								if (row.RowState != DataRowState.Deleted)
								{
									//dat.Add(SetValues(ptype.GetProperty("Item").PropertyType, row));
									mAdd.Invoke(dat, new object[] { SetValues(ptype.GetProperty("Item").PropertyType, row) });
								}
							}
						}
						return dat;
					}
					else
					{
						if (param is DataRow)
						{
							if ((param as DataRow).RowState != DataRowState.Deleted)
							{
								return SetValues(ptype, (param as DataRow));
							}
							else
							{
								return null;
							}
						}
						else if (param is DataTable)
						{
							if ((param as DataTable).Rows.Count > 0)
							{
								if ((param as DataTable).Rows[0].RowState != DataRowState.Deleted)
								{
									// 配列ではないので、Rowsの1件目のみを非コレクション型として渡す
									return SetValues(ptype, (param as DataTable).Rows[0]);
								}
								else
								{
									return null;
								}
							}
							else
							{
								return null;
							}
						}
						else
						{
							return param;
						}
					}
				}

			}
			catch (Exception ex)
			{
				FWThreadCoreDataException newex = new FWThreadCoreDataException(ConstDataAccess.Error, ex);
				throw newex;
			}
		}

		private object SetValues(Type arraytp, DataRow row)
		{
			object item = Activator.CreateInstance(arraytp);
			//メンバを取得する
			try
			{
				PropertyInfo[] props = arraytp.GetProperties(BindingFlags.Public | BindingFlags.Instance);
				foreach (PropertyInfo pi in props)
				{
					Type prptype;
					object col;
					prptype = pi.PropertyType;
					if (row.Table.Columns.Contains(pi.Name))
					{
						if (row.IsNull(pi.Name))
						{
							col = null;
						}
						else
						{
							col = row[pi.Name];
						}
						pi.SetValue(item, col);
					}
				}
			}
			catch (Exception ex)
			{
				FWThreadCoreDataException newex = new FWThreadCoreDataException(ConstDataAccess.Error, ex);
				throw newex;
			}
			return item;
		}


	}
}
