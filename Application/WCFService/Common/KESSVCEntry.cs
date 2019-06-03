using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using System.ComponentModel;

namespace KyoeiSystem.Application.WCFService
{
	/// <summary>
	/// WCFサービス実行機能
	/// </summary>
	public partial class KESSVCEntry : KyoeiSystem.Framework.Core.IKESService
	{

		/// <summary>
		/// 指定されたIDに対応する機能の呼び出し
		/// </summary>
		/// <param name="fname">機能名</param>
		/// <param name="kind">機能タイプ（未使用）</param>
		/// <param name="cstr">接続情報</param>
		/// <param name="ptypes">パラメータタイプ（未使用）</param>
		/// <param name="plist">パラメータ</param>
		/// <returns>各機能の戻り値</returns>
		public object CallFunction(string fname, string kind, string constr, string[] ptypes, object[] plist)
		{
			object result = null;

			try
			{
				WCFDataAccessConfig cfg = DataAccessCfg.GetDacCfg(fname);
				if (cfg == null)
				{
					throw new Exception(fname + "_DacIsNull");
				}

				Type svctype = Type.GetType(cfg.Namespace + "." + cfg.ServiceClass);
				if (svctype == null)
				{
					throw new Exception(fname + "_NotFound");
				}

				// 接続情報をセット
				KyoeiSystem.Application.WCFService.CommonData.ConnectStringUserDB = constr;

				// クラスのインスタンスを生成
				var da = Activator.CreateInstance(svctype);

				// 実行すべきメソッドの情報を取得
				MethodInfo svcMethodInfo = svctype.GetMethod(cfg.MethodName);
				if (svcMethodInfo == null)
				{
					throw new Exception("UnknownMethodName");
				}
				// 引数を準備
				ParameterInfo[] parray = svcMethodInfo.GetParameters();
				int idx = 0;
				List<object> parameters = new List<object>();
				foreach (object prm in plist)
				{
					try
					{
						if (idx >= parray.Length)
						{
							break;
						}
						// 受け取ったパラメータをメソッドの引数に変換
						// 引数がDataTableの場合は配列のインスタンスに変換
						parameters.Add(ConvertFromDataTable(parray[idx++].ParameterType, prm));
					}
                    //catch (Exception ex)
                    catch
					{
						//string msg = ex.Message;
						//throw new Exception("Invalid Parameter", ex);
					}
				}

				// ターゲットのメソッドを呼び出す
				var dat = svcMethodInfo.Invoke(da, parameters.ToArray());

				// 戻り値を処理する
				if (dat != null)
				{
					// データメンバーからデータテーブルのカラムを作成
					if (dat.GetType().IsGenericType)
					{
						Type[] ts = dat.GetType().GetGenericArguments();
						DataTable table = new DataTable(fname);
						setupColumnsByProperties(ts[0], table);
						convertToDataTable(dat, table);
						result = table;
					}
					else
					{
						if (dat.GetType().IsNested)
						{
							DataSet ds = new DataSet(fname);
							foreach (var fld in dat.GetType().GetFields())
							{
								var datN = fld.GetValue(dat);
								Type[] ts2 = datN.GetType().GetGenericArguments();
								DataTable table = ds.Tables.Add(fld.Name);
								setupColumnsByProperties(ts2[0], table);
								convertToDataTable(datN, table);
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
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
			}

			return result;
		}

		private static void setupColumnsByProperties(Type tp, DataTable tbl)
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

		public static void convertToDataTable(object data, DataTable tbl)
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
				throw new FWThreadCoreDataException("cannot convert to DataTable", ex);
			}
		}

		private static void GetValues(PropertyInfo[] props, object data, DataTable tbl)
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

		private static object ConvertFromDataTable(Type ptype, object param)
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
				throw new FWThreadCoreDataException("Cannot convert from DataTable", ex);
			}
		}

		private static object SetValues(Type arraytp, DataRow row)
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
				throw new FWThreadCoreDataException("Cannot convert from DataTable", ex);
			}
			return item;
		}

        /// <summary>
        /// リスト型データをデータテーブルに変換して返す
        /// </summary>
        /// <typeparam name="T">リストのデータ型</typeparam>
        /// <param name="data">リストデータ</param>
        /// <returns></returns>
        public static DataTable ConvertListToDataTable<T>(List<T> data)
        {
            var properties = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();

            // テーブル列を生成
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            // データを追加
            foreach (T item in data)
            {
                var row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

	}

}
