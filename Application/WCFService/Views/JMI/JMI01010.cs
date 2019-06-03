using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Objects;
using System.Data;
using System.Data.Common;
using System.Transactions;
using System.Collections;
using System.Data.Entity;

namespace KyoeiSystem.Application.WCFService
{

	/// <summary>
	/// JMI01010  印刷　メンバー
	/// </summary>
	[DataContract]
	public class JMI01010_Member
	{
		[DataMember]
		public DateTime? 基点日 { get; set; }
		[DataMember]
		public int? 期間 { get; set; }
		[DataMember]
		public DateTime 開始日 { get; set; }
		[DataMember]
		public DateTime 終了日 { get; set; }
		[DataMember]
		public string IDFROM { get; set; }
		[DataMember]
		public string IDTO { get; set; }
		[DataMember]
		public string 乗務員指定 { get; set; }
		[DataMember]
		public int? 乗務員コード { get; set; }
		[DataMember]
		public int? 出勤区分 { get; set; }
		[DataMember]
		public string 乗務員名 { get; set; }
		[DataMember]
		public string[] 日 { get; set; }
		[DataMember]
		public int 出勤 { get; set; }
		[DataMember]
		public int 休出 { get; set; }
		[DataMember]
		public int 公出 { get; set; }
		[DataMember]
		public int 土出 { get; set; }
		[DataMember]
		public int 振出 { get; set; }
		[DataMember]
		public int 欠勤 { get; set; }
		[DataMember]
		public int 有給 { get; set; }
		[DataMember]
		public int 他計 { get; set; }
		[DataMember]
		public int 合計 { get; set; }
	}

	/// <summary>
	/// JMI01011  印刷　メンバー
	/// </summary>
	[DataContract]
	public class JMI01010_JMI
	{
		[DataMember]
		public int 乗務員コード { get; set; }
		public int? 連携コード { get; set; }
		public string 乗務員名 { get; set; }
	}

	/// <summary>
	/// JMI01011  印刷　メンバー
	/// </summary>
	[DataContract]
	public class JMI01010_date
	{
		[DataMember]
		public int 乗務員コード { get; set; }
		public int[] 日 { get; set; }
	}

	/// <summary>
	/// JMI01011 　出勤区分
	/// </summary>
	[DataContract]
	public class JMI01010_syukin
	{
		[DataMember]
		public int? コード { get; set; }
		public string 出勤区分名 { get; set; }
	}

	/// <summary>
	/// JMI01010  CSV　メンバー
	/// </summary>
	[DataContract]
	public class JMI01010_Member_CSV
	{
		[DataMember]
		public DateTime 集計 { get; set; }
		[DataMember]
		public int コード { get; set; }
		[DataMember]
		public string 得意先名 { get; set; }
		[DataMember]
		public string 乗務員１ { get; set; }
		[DataMember]
		public string 乗務員２ { get; set; }
		[DataMember]
		public string 発地名 { get; set; }
		[DataMember]
		public string 着地名 { get; set; }
		[DataMember]
		public string 車番 { get; set; }
		[DataMember]
		public string 品名 { get; set; }
		[DataMember]
		public decimal 数量 { get; set; }
		[DataMember]
		public decimal 重量 { get; set; }
		[DataMember]
		public int 支払消費税 { get; set; }
		[DataMember]
		public int 支払通行料 { get; set; }
		[DataMember]
		public decimal 支払単価 { get; set; }
		[DataMember]
		public int 支払金額 { get; set; }
		[DataMember]
		public int 売上金額 { get; set; }
		[DataMember]
		public int 差益 { get; set; }
		[DataMember]
		public string 摘要 { get; set; }
		[DataMember]
		public int 締日 { get; set; }
		[DataMember]
		public int 社内区分 { get; set; }

	}


	public class JMI01010
	{
		#region 印刷
		/// <summary>
		/// JMI01010 印刷
		/// </summary>
		/// <param name="p商品ID">乗務員コード</param>
		/// <returns>T01</returns>
		public DataTable GetDataList(string p乗務員From, string p乗務員To, int?[] i乗務員List, DateTime d集計期間From, DateTime d集計期間To, string s乗務員ピックアップ)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				DataTable dtTest = new DataTable();
				int cnt = 0;
				ArrayList strData = new ArrayList();

				dtTest.Columns.Add("乗務員ID", typeof(Int32));
				dtTest.Columns.Add("乗務員名", typeof(string));
				//
				for (DateTime x = d集計期間From; x <= d集計期間To; cnt += 1)
				{
					strData.Add(x.Date.ToString());
					//strData[cnt] = x.Date.ToString();

					dtTest.Columns.Add((cnt + 1).ToString(), typeof(string));
					x = x.AddDays(1);
				}
				for (int i = dtTest.Columns.Count; i < 33; i++)
				{
					dtTest.Columns.Add((cnt + 1).ToString(), typeof(string));
					cnt++;
				}
				// 主キーの設定
				dtTest.PrimaryKey = new DataColumn[] { dtTest.Columns["乗務員ID"] };


				List<JMI01010_date> retDateList = new List<JMI01010_date>();

				List<JMI01010_Member> retDrvList = new List<JMI01010_Member>();
				context.Connection.Open();

				//データがある乗務員のみ列挙
				//int[] DRV_IDList;
				//全件表示
				var query1 = (from drv in context.M04_DRV.Where(drv => drv.削除日付 == null && (drv.退職年月日 == null || drv.退職年月日 > d集計期間From)).DefaultIfEmpty()
							  orderby drv.乗務員ID
							  select new JMI01010_JMI
							  {
								  乗務員コード = drv.乗務員ID,
								  乗務員名 = drv.乗務員名,
							  }
							  ).AsQueryable();


				if (!(string.IsNullOrEmpty(p乗務員From + p乗務員To) && i乗務員List.Length == 0))
				{

					//乗務員が検索対象に入っていない時全データ取得
					if (string.IsNullOrEmpty(p乗務員From + p乗務員To))
					{
						query1 = query1.Where(c => c.乗務員コード >= int.MaxValue);
					}

					//乗務員From処理　Min値
					if (!string.IsNullOrEmpty(p乗務員From))
					{
						int i乗務員FROM;
						int.TryParse(p乗務員From, out i乗務員FROM);
						query1 = query1.Where(c => c.乗務員コード >= i乗務員FROM);
					}

					//乗務員To処理　Max値
					if (!string.IsNullOrEmpty(p乗務員To))
					{
						int i乗務員TO;
						int.TryParse(p乗務員To, out i乗務員TO);
						query1 = query1.Where(c => c.乗務員コード <= i乗務員TO);
					}

					if (i乗務員List.Length > 0)
					{
						var intCause = i乗務員List;
						query1 = query1.Union(from m04 in context.M04_DRV.Where(drv => drv.削除日付 == null && (drv.退職年月日 == null || drv.退職年月日 > d集計期間From))
											  where intCause.Contains(m04.乗務員ID)
											  select new JMI01010_JMI
											  {
												  乗務員コード = m04.乗務員ID,
												  乗務員名 = m04.乗務員名,

											  });
						//乗務員From処理　Min値
						if (!string.IsNullOrEmpty(p乗務員From))
						{
							int i乗務員FROM;
							int.TryParse(p乗務員From, out i乗務員FROM);
							query1 = query1.Where(c => c.乗務員コード >= i乗務員FROM);
						}

						//乗務員To処理　Max値
						if (!string.IsNullOrEmpty(p乗務員To))
						{
							int i乗務員TO;
							int.TryParse(p乗務員To, out i乗務員TO);
							query1 = query1.Where(c => c.乗務員コード <= i乗務員TO);
						}
					}
				}
				else
				{
					query1 = query1.Where(c => c.乗務員コード > int.MinValue && c.乗務員コード < int.MaxValue);
				}

				if (i乗務員List.Length != 0)
				{
					int?[] intCause = i乗務員List;
					query1 = query1.Union(from drv in context.M04_DRV.Where(drv => drv.削除日付 == null && (drv.退職年月日 == null || drv.退職年月日 > d集計期間From)).DefaultIfEmpty()
										  where intCause.Contains(drv.乗務員ID)
										  orderby drv.乗務員ID
										  select new JMI01010_JMI
										  {
											  乗務員コード = drv.乗務員ID,
											  乗務員名 = drv.乗務員名,
										  });
				}

				query1 = query1.Distinct();

				List<JMI01010_JMI> query1LIST;

				query1LIST = query1.ToList();

				//乗務員レコード追加
				int i乗務員 = 0;
				//DataTable dtTest = new DataTable();
				for (int i = 0; i < query1LIST.Count; i++)
				{
					i乗務員 = query1LIST[i].乗務員コード;
					DataRow dr = dtTest.NewRow();

					dr["乗務員ID"] = query1LIST[i].乗務員コード;
					dr["乗務員名"] = query1LIST[i].乗務員名;

					dtTest.Rows.Add(dr);
				}

				//UTRNデータ抽出
				var query = (from t02 in context.T02_UTRN
							 from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == t02.乗務員KEY).DefaultIfEmpty()
							 from q in query1.Where(c => c.乗務員コード == m04.乗務員ID)
							 where t02.勤務開始日 <= d集計期間To && t02.勤務終了日 >= d集計期間From
							 select new JMI01010_Member
							 {
								 開始日 = (DateTime)d集計期間From,
								 終了日 = (DateTime)d集計期間To,
								 IDFROM = p乗務員From,
								 IDTO = p乗務員To,
								 乗務員コード = m04.乗務員ID,
								 乗務員名 = m04.乗務員名,
								 出勤区分 = t02.出勤区分ID,
								 基点日 = t02.勤務開始日 < d集計期間From ? d集計期間From : t02.勤務開始日,
								 期間 = EntityFunctions.DiffDays((t02.勤務開始日 < d集計期間From ? d集計期間From : t02.勤務開始日), (t02.勤務終了日 > d集計期間To ? d集計期間To : t02.勤務終了日)) + 1,
							 }).ToList();


				try
				{
					//出勤区分更新
					//i乗務員 = 0;
					//DataSet dstest = new DataSet
					//DataTable dtTest = new DataTable();
					for (int i = 0; i < query.Count; i++)
					{
						DateTime Kitenbi = (DateTime)query[i].基点日;
						for (int ii = 0; ii < query[i].期間; ii++)
						{
							if (query[i].乗務員コード != null)
							{
								DataRow targetRow = dtTest.Rows.Find(query[i].乗務員コード);
								//if (query[i].出勤区分 >= 1 && query[i].出勤区分 <= 7)
								//{
								//    targetRow[Convert.ToString(Kitenbi.Day + ii)] = query2.Where(x => x.コード == query[i].出勤区分);
								//}else
								//{
								//    targetRow[Convert.ToString(Kitenbi.Day + ii)] = "他";
								//}
								if (Kitenbi.AddDays(ii) <= d集計期間To)
								{
									//d集計期間From.Day
									TimeSpan ts = Kitenbi.AddDays(ii) - d集計期間From.AddDays(-1);
									targetRow[Convert.ToString(ts.Days)] = query[i].出勤区分;
								}
							}
						}
					}
				}
				catch (Exception ex)
				{ }

				//出勤区分合計算出
				int MAXCOL = dtTest.Columns.Count;

				//出勤区分取得

				var query2 = (from syk in context.M78_SYK.Where(syk => syk.削除日付 == null).DefaultIfEmpty()
							  orderby syk.出勤区分ID
							  select new JMI01010_syukin
							  {
								  コード = syk.出勤区分ID,
								  出勤区分名 = syk.出勤区分名,
							  }
							  ).AsQueryable();

				//テーブルに合計列追加
				var querywhere = query2.Where(x => x.コード == 1).ToList();
				//1～7個目
				for (int i = 0; i < 8; i++)
				{
					dtTest.Columns.Add(("合計" + i).ToString(), typeof(Int32));

					//querywhere = query2.Where(x => x.コード == i).ToList();
					//if(querywhere != null)
					//{
					//    dtTest.Columns.Add(querywhere[0].出勤区分名 , typeof(Int32));
					//}else
					//{
					//    dtTest.Columns.Add( "" , typeof(Int32));
					//}
				}
				dtTest.Columns.Add("他計", typeof(Int32));
				dtTest.Columns.Add("合計", typeof(Int32));
				//dtTest.Columns.Add("他計", typeof(Int32));
				//dtTest.Columns.Add("合計", typeof(Int32));

				//合計を追加
				int GK0 = 0, GK1 = 0, GK2 = 0, GK3 = 0, GK4 = 0, GK5 = 0, GK6 = 0, GK7 = 0, GK8 = 0, GK20 = 0;
				for (int i = 0; i < dtTest.Rows.Count; i++)
				{
					for (int ii = 2; ii < MAXCOL; ii++)
					{
						DataRow targetRow = dtTest.Rows[i];
						switch (targetRow[ii].ToString())
						{
						case "0":
							GK0++;
							break;
						case "1":
							GK1++;
							break;
						case "2":
							GK2++;
							break;
						case "3":
							GK3++;
							break;
						case "4":
							GK4++;
							break;
						case "5":
							GK5++;
							break;
						case "6":
							GK6++;
							break;
						case "7":
							GK7++;
							break;
						case "":
							break;
						case null:
							break;
						default:
							GK8++;
							break;
						}
					}

					int iii = MAXCOL;
					DataRow targetRow2;
					targetRow2 = dtTest.Rows[i];
					targetRow2[iii] = GK0;
					iii++;
					targetRow2[iii] = GK1;
					iii++;
					targetRow2[iii] = GK2;
					iii++;
					targetRow2[iii] = GK3;
					iii++;
					targetRow2[iii] = GK4;
					iii++;
					targetRow2[iii] = GK5;
					iii++;
					targetRow2[iii] = GK6;
					iii++;
					targetRow2[iii] = GK7;
					iii++;
					targetRow2[iii] = GK8;
					GK20 = GK0 + GK1 + GK2 + GK3 + GK4 + GK5 + GK6 + GK7 + GK8;
					iii++;
					targetRow2[iii] = GK20;
					GK0 = 0;
					GK1 = 0;
					GK2 = 0;
					GK3 = 0;
					GK4 = 0;
					GK5 = 0;
					GK6 = 0;
					GK7 = 0;
					GK8 = 0;
					GK20 = 0;
				}

				//出勤データをＩＤから文字へ置き換え
				//DataSet dstest = new DataSet
				//DataTable dtTest = new DataTable();
				for (int i = 0; i < query.Count; i++)
				{
					DateTime Kitenbi = (DateTime)query[i].基点日;
					for (int ii = 0; ii < query[i].期間; ii++)
					{
						TimeSpan ts = Kitenbi.AddDays(ii) - d集計期間From.AddDays(-1);
						if (Kitenbi.AddDays(ii) <= d集計期間To)
						{
							if (query[i].乗務員コード != null)
							{
								DataRow targetRow = dtTest.Rows.Find(query[i].乗務員コード);
								if (query[i].出勤区分 >= 0 && query[i].出勤区分 <= 7)
								{
									int id;
									int.TryParse(query[i].出勤区分.ToString(), out id);
									querywhere = query2.Where(x => x.コード == id).ToList();

									char c1 = querywhere[0].出勤区分名.ToString()[0];
									targetRow[Convert.ToString(ts.Days)] = c1;
								}
								else
								{
									targetRow[Convert.ToString(ts.Days)] = "他";
								}
								//targetRow[Convert.ToString(Kitenbi.Day + ii)] = query[i].出勤区分;
							}
						}
					}
				}

				//デバック用後でコメント削除
				////データがない行を削除
				//for (int i = dtTest.Rows.Count-1; i >= 0; i--)
				//{
				//    DataRow targetRow = dtTest.Rows[i];
				//    if ((targetRow[dtTest.Columns.Count - 1]).ToString() == "0")
				//    {
				//        targetRow.Delete();
				//    }
				//}


				dtTest.Columns.Add("開始日付", typeof(DateTime));
				dtTest.Columns.Add("終了日付", typeof(DateTime));
				dtTest.Columns.Add("IDFrom", typeof(String));
				dtTest.Columns.Add("IDTo", typeof(String));
				dtTest.Columns.Add("IDList", typeof(String));

				for (int x = 1; x <= 31; x += 1)
				{
					dtTest.Columns.Add(x + "日", typeof(String));
				}

				////日付の項目を追加
				//for (DateTime x = d集計期間From; x <= d集計期間To; cnt += 1)
				//{
				//    strData.Add(x.Date.ToString());
				//    //strData[cnt] = x.Date.ToString();

				//    dtTest.Columns.Add(Convert.ToString(x.Day), typeof(string));
				//    x = x.AddDays(1);
				//}
				//for (int i = dtTest.Columns.Count; i < 33; i++)
				//{
				//    dtTest.Columns.Add("", typeof(string));
				//}

				//合計項目用の列作成
				//1～7個目
				for (int i = 0; i < 8; i++)
				{
					dtTest.Columns.Add(("合計項目" + i).ToString(), typeof(String));
				}

				for (int i = 0; i < dtTest.Rows.Count; i++)
				{
					DataRow targetRow = dtTest.Rows[i];
					targetRow["開始日付"] = d集計期間From;
					targetRow["終了日付"] = d集計期間To;
					targetRow["IDFrom"] = p乗務員From;
					targetRow["IDTo"] = p乗務員To;
					targetRow["IDList"] = s乗務員ピックアップ;
					//日付の項目を追加
					cnt = 0;
					for (DateTime x = d集計期間From; x <= d集計期間To; x = x.AddDays(1))
					{
						cnt++;
						targetRow[cnt.ToString() + "日"] = x.Day.ToString();

					}
					for (cnt = 0; cnt <= 7; cnt++)
					{
						querywhere = query2.Where(x => x.コード == cnt).ToList();

						if (querywhere != null)
						{
							targetRow["合計項目" + cnt.ToString()] = querywhere[0].出勤区分名;
						}
						else
						{
							targetRow["合計項目" + cnt.ToString()] = "";
						}
					}
					//for (int i = cnt; i < dtTest.Columns.Count; i++)
					//{
					//    targetRow["IDList"] = "";
					//}

				}


				////datatableをリスト化
				//List<DataRow> retList = new List<DataRow>();
				//for (int i = 0; i < dtTest.Rows.Count; i++)
				//{
				//    DataRow targetRow = dtTest.Rows[i];
				//    retList.Add(targetRow);
				//}


				//データテーブル型で返す
				return dtTest;

			}

		}
		#endregion


		#region CSV出力
		/// <summary>
		/// JMI01010 CSV出力
		/// </summary>
		/// <param name="p商品ID">乗務員コード</param>
		/// <returns>T01</returns>
		public DataTable GetDataList_CSV(string p乗務員From, string p乗務員To, int?[] i乗務員List, DateTime d集計期間From, DateTime d集計期間To, string s乗務員ピックアップ)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				DataTable dtTest = new DataTable();
				int cnt = 0;
				ArrayList strData = new ArrayList();

				dtTest.Columns.Add("乗務員ID", typeof(Int32));
				dtTest.Columns.Add("乗務員名", typeof(string));
				//
				for (DateTime x = d集計期間From; x <= d集計期間To; cnt += 1)
				{
					strData.Add(x.Date.ToString());
					//strData[cnt] = x.Date.ToString();

					dtTest.Columns.Add((d集計期間From.AddDays(cnt).Day).ToString(), typeof(string));
					x = x.AddDays(1);
				}
				for (int i = dtTest.Columns.Count; i < 33; i++)
				{
					dtTest.Columns.Add("列" + (cnt + 1).ToString(), typeof(string));
					cnt++;
				}
				// 主キーの設定
				dtTest.PrimaryKey = new DataColumn[] { dtTest.Columns["乗務員ID"] };


				List<JMI01010_date> retDateList = new List<JMI01010_date>();

				List<JMI01010_Member> retDrvList = new List<JMI01010_Member>();
				context.Connection.Open();

				//データがある乗務員のみ列挙
				//int[] DRV_IDList;
				//全件表示
				var query1 = (from drv in context.M04_DRV.Where(drv => drv.削除日付 == null && (drv.退職年月日 == null || drv.退職年月日 > d集計期間From)).DefaultIfEmpty()

							  orderby drv.乗務員ID
							  select new JMI01010_JMI
							  {
								  乗務員コード = drv.乗務員ID,
								  乗務員名 = drv.乗務員名,
							  }
							  ).AsQueryable();


				if (!(string.IsNullOrEmpty(p乗務員From + p乗務員To) && i乗務員List.Length == 0))
				{

					//乗務員が検索対象に入っていない時全データ取得
					if (string.IsNullOrEmpty(p乗務員From + p乗務員To))
					{
						query1 = query1.Where(c => c.乗務員コード >= int.MaxValue);
					}

					//乗務員From処理　Min値
					if (!string.IsNullOrEmpty(p乗務員From))
					{
						int i乗務員FROM;
						int.TryParse(p乗務員From, out i乗務員FROM);
						query1 = query1.Where(c => c.乗務員コード >= i乗務員FROM);
					}

					//乗務員To処理　Max値
					if (!string.IsNullOrEmpty(p乗務員To))
					{
						int i乗務員TO;
						int.TryParse(p乗務員To, out i乗務員TO);
						query1 = query1.Where(c => c.乗務員コード <= i乗務員TO);
					}

					if (i乗務員List.Length > 0)
					{
						var intCause = i乗務員List;
						query1 = query1.Union(from m04 in context.M04_DRV.Where(drv => drv.削除日付 == null && (drv.退職年月日 == null || drv.退職年月日 > d集計期間From))
											  where intCause.Contains(m04.乗務員ID)
											  select new JMI01010_JMI
											  {
												  乗務員コード = m04.乗務員ID,
												  乗務員名 = m04.乗務員名,

											  });
						//乗務員From処理　Min値
						if (!string.IsNullOrEmpty(p乗務員From))
						{
							int i乗務員FROM;
							int.TryParse(p乗務員From, out i乗務員FROM);
							query1 = query1.Where(c => c.乗務員コード >= i乗務員FROM);
						}

						//乗務員To処理　Max値
						if (!string.IsNullOrEmpty(p乗務員To))
						{
							int i乗務員TO;
							int.TryParse(p乗務員To, out i乗務員TO);
							query1 = query1.Where(c => c.乗務員コード <= i乗務員TO);
						}
					}
				}
				else
				{
					query1 = query1.Where(c => c.乗務員コード > int.MinValue && c.乗務員コード < int.MaxValue);
				}

				if (i乗務員List.Length != 0)
				{
					int?[] intCause = i乗務員List;
					query1 = query1.Union(from drv in context.M04_DRV.Where(drv => drv.削除日付 == null && (drv.退職年月日 == null || drv.退職年月日 > d集計期間From)).DefaultIfEmpty()
										  where intCause.Contains(drv.乗務員ID)
										  orderby drv.乗務員ID
										  select new JMI01010_JMI
										  {
											  乗務員コード = drv.乗務員ID,
											  乗務員名 = drv.乗務員名,
										  });
				}

				query1 = query1.Distinct();

				List<JMI01010_JMI> query1LIST;

				query1LIST = query1.ToList();

				//乗務員レコード追加
				int i乗務員 = 0;
				//DataTable dtTest = new DataTable();
				for (int i = 0; i < query1LIST.Count; i++)
				{
					i乗務員 = query1LIST[i].乗務員コード;
					DataRow dr = dtTest.NewRow();

					dr["乗務員ID"] = query1LIST[i].乗務員コード;
					dr["乗務員名"] = query1LIST[i].乗務員名;

					dtTest.Rows.Add(dr);
				}

				//UTRNデータ抽出
				var query = (from t02 in context.T02_UTRN
							 from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == t02.乗務員KEY).DefaultIfEmpty()
							 from q in query1.Where(c => c.乗務員コード == m04.乗務員ID)
							 where t02.勤務開始日 <= d集計期間To && t02.勤務終了日 >= d集計期間From
							 select new JMI01010_Member
							 {
								 開始日 = (DateTime)d集計期間From,
								 終了日 = (DateTime)d集計期間To,
								 IDFROM = p乗務員From,
								 IDTO = p乗務員To,
								 乗務員コード = m04.乗務員ID,
								 乗務員名 = m04.乗務員名,
								 出勤区分 = t02.出勤区分ID,
								 基点日 = t02.勤務開始日 < d集計期間From ? d集計期間From : t02.勤務開始日,
								 期間 = EntityFunctions.DiffDays((t02.勤務開始日 < d集計期間From ? d集計期間From : t02.勤務開始日), (t02.勤務終了日 > d集計期間To ? d集計期間To : t02.勤務終了日)) + 1,
							 }).ToList();


				try
				{
					//出勤区分更新
					//i乗務員 = 0;
					//DataSet dstest = new DataSet
					//DataTable dtTest = new DataTable();
					for (int i = 0; i < query.Count; i++)
					{
						DateTime Kitenbi = (DateTime)query[i].基点日;
						for (int ii = 0; ii < query[i].期間; ii++)
						{
							if (query[i].乗務員コード != null)
							{
								DataRow targetRow = dtTest.Rows.Find(query[i].乗務員コード);
								//if (query[i].出勤区分 >= 1 && query[i].出勤区分 <= 7)
								//{
								//    targetRow[Convert.ToString(Kitenbi.Day + ii)] = query2.Where(x => x.コード == query[i].出勤区分);
								//}else
								//{
								//    targetRow[Convert.ToString(Kitenbi.Day + ii)] = "他";
								//}
								if (Kitenbi.AddDays(ii) <= d集計期間To)
								{
									//d集計期間From.Day
									TimeSpan ts = Kitenbi.AddDays(ii) - d集計期間From.AddDays(-1);
									targetRow[Convert.ToString(ts.Days)] = query[i].出勤区分;
								}
							}
						}
					}
				}
				catch (Exception ex)
				{ }

				//出勤区分合計算出
				int MAXCOL = dtTest.Columns.Count;

				//出勤区分取得

				var query2 = (from syk in context.M78_SYK.Where(syk => syk.削除日付 == null).DefaultIfEmpty()
							  orderby syk.出勤区分ID
							  select new JMI01010_syukin
							  {
								  コード = syk.出勤区分ID,
								  出勤区分名 = syk.出勤区分名,
							  }
							  ).AsQueryable();

				//テーブルに合計列追加
				var querywhere = query2.Where(x => x.コード == 1).ToList();
				//1～7個目
				for (int i = 0; i < 8; i++)
				{

					querywhere = query2.Where(x => x.コード == i).ToList();
					if (querywhere != null)
					{
						dtTest.Columns.Add((querywhere[0].出勤区分名.ToString()).ToString(), typeof(String));
					}
					else
					{
						dtTest.Columns.Add(("合計項目" + i).ToString(), typeof(String));
					}

					//dtTest.Columns.Add(("合計" + i).ToString(), typeof(Int32));

					//querywhere = query2.Where(x => x.コード == i).ToList();
					//if(querywhere != null)
					//{
					//    dtTest.Columns.Add(querywhere[0].出勤区分名 , typeof(Int32));
					//}else
					//{
					//    dtTest.Columns.Add( "" , typeof(Int32));
					//}
				}
				dtTest.Columns.Add("他計", typeof(Int32));
				dtTest.Columns.Add("合計", typeof(Int32));
				//dtTest.Columns.Add("他計", typeof(Int32));
				//dtTest.Columns.Add("合計", typeof(Int32));

				//合計を追加
				int GK0 = 0, GK1 = 0, GK2 = 0, GK3 = 0, GK4 = 0, GK5 = 0, GK6 = 0, GK7 = 0, GK8 = 0, GK20 = 0;
				for (int i = 0; i < dtTest.Rows.Count; i++)
				{
					for (int ii = 2; ii < MAXCOL; ii++)
					{
						DataRow targetRow = dtTest.Rows[i];
						switch (targetRow[ii].ToString())
						{
						case "0":
							GK0++;
							break;
						case "1":
							GK1++;
							break;
						case "2":
							GK2++;
							break;
						case "3":
							GK3++;
							break;
						case "4":
							GK4++;
							break;
						case "5":
							GK5++;
							break;
						case "6":
							GK6++;
							break;
						case "7":
							GK7++;
							break;
						case "":
							break;
						case null:
							break;
						default:
							GK8++;
							break;
						}
					}

					int iii = MAXCOL;
					DataRow targetRow2;
					targetRow2 = dtTest.Rows[i];
					targetRow2[iii] = GK0;
					iii++;
					targetRow2[iii] = GK1;
					iii++;
					targetRow2[iii] = GK2;
					iii++;
					targetRow2[iii] = GK3;
					iii++;
					targetRow2[iii] = GK4;
					iii++;
					targetRow2[iii] = GK5;
					iii++;
					targetRow2[iii] = GK6;
					iii++;
					targetRow2[iii] = GK7;
					iii++;
					targetRow2[iii] = GK8;
					GK20 = GK0 + GK1 + GK2 + GK3 + GK4 + GK5 + GK6 + GK7 + GK8;
					iii++;
					targetRow2[iii] = GK20;
					GK0 = 0;
					GK1 = 0;
					GK2 = 0;
					GK3 = 0;
					GK4 = 0;
					GK5 = 0;
					GK6 = 0;
					GK7 = 0;
					GK8 = 0;
					GK20 = 0;
				}

				//出勤データをＩＤから文字へ置き換え
				//DataSet dstest = new DataSet
				//DataTable dtTest = new DataTable();
				for (int i = 0; i < query.Count; i++)
				{
					DateTime Kitenbi = (DateTime)query[i].基点日;
					for (int ii = 0; ii < query[i].期間; ii++)
					{
						TimeSpan ts = Kitenbi.AddDays(ii) - d集計期間From.AddDays(-1);
						if (Kitenbi.AddDays(ii) <= d集計期間To)
						{
							if (query[i].乗務員コード != null)
							{
								DataRow targetRow = dtTest.Rows.Find(query[i].乗務員コード);
								if (query[i].出勤区分 >= 0 && query[i].出勤区分 <= 7)
								{
									int id;
									int.TryParse(query[i].出勤区分.ToString(), out id);
									querywhere = query2.Where(x => x.コード == id).ToList();

									char c1 = querywhere[0].出勤区分名.ToString()[0];
									targetRow[Convert.ToString(ts.Days)] = c1;
								}
								else
								{
									targetRow[Convert.ToString(ts.Days)] = "他";
								}
								//targetRow[Convert.ToString(Kitenbi.Day + ii)] = query[i].出勤区分;
							}
						}
					}
				}

				//デバック用後でコメント削除
				////データがない行を削除
				//for (int i = dtTest.Rows.Count-1; i >= 0; i--)
				//{
				//    DataRow targetRow = dtTest.Rows[i];
				//    if ((targetRow[dtTest.Columns.Count - 1]).ToString() == "0")
				//    {
				//        targetRow.Delete();
				//    }
				//}


				//dtTest.Columns.Add("開始日付", typeof(DateTime));
				//dtTest.Columns.Add("終了日付", typeof(DateTime));
				//dtTest.Columns.Add("IDFrom", typeof(String));
				//dtTest.Columns.Add("IDTo", typeof(String));
				//dtTest.Columns.Add("IDList", typeof(String));

				//for (int x = 1; x <= 31; x += 1)
				//{
				//	dtTest.Columns.Add(x + "日", typeof(String));
				//}

				////日付の項目を追加
				//for (DateTime x = d集計期間From; x <= d集計期間To; cnt += 1)
				//{
				//    strData.Add(x.Date.ToString());
				//    //strData[cnt] = x.Date.ToString();

				//    dtTest.Columns.Add(Convert.ToString(x.Day), typeof(string));
				//    x = x.AddDays(1);
				//}
				//for (int i = dtTest.Columns.Count; i < 33; i++)
				//{
				//    dtTest.Columns.Add("", typeof(string));
				//}

				//合計項目用の列作成
				//1～7個目
				//for (int i = 0; i < 8; i++)
				//{
				//	querywhere = query2.Where(x => x.コード == i).ToList();
				//	if (querywhere != null)
				//	{
				//		dtTest.Columns.Add((querywhere[0].出勤区分名.ToString()).ToString(), typeof(String));
				//	}
				//	else
				//	{
				//		dtTest.Columns.Add(("合計項目" + i).ToString(), typeof(String));
				//	}
				//}

				//for (int i = 0; i < dtTest.Rows.Count; i++)
				//{
				//DataRow targetRow = dtTest.Rows[i];
				//targetRow["開始日付"] = d集計期間From;
				//targetRow["終了日付"] = d集計期間To;
				//targetRow["IDFrom"] = p乗務員From;
				//targetRow["IDTo"] = p乗務員To;
				//targetRow["IDList"] = s乗務員ピックアップ;
				//日付の項目を追加
				//cnt = 0;
				//for (DateTime x = d集計期間From; x <= d集計期間To; x = x.AddDays(1))
				//{
				//	cnt++;
				//	targetRow[cnt.ToString() + "日"] = x.Day.ToString();

				//}
				//for (cnt = 0; cnt <= 7; cnt++)
				//{
				//	querywhere = query2.Where(x => x.コード == cnt).ToList();

				//	if (querywhere != null)
				//	{
				//		targetRow["合計項目" + cnt.ToString()] = querywhere[0].出勤区分名;
				//	}
				//	else
				//	{
				//		targetRow["合計項目" + cnt.ToString()] = "";
				//	}
				//}
				//for (int i = cnt; i < dtTest.Columns.Count; i++)
				//{
				//    targetRow["IDList"] = "";
				//}

				//}


				////datatableをリスト化
				//List<DataRow> retList = new List<DataRow>();
				//for (int i = 0; i < dtTest.Rows.Count; i++)
				//{
				//    DataRow targetRow = dtTest.Rows[i];
				//    retList.Add(targetRow);
				//}


				//データテーブル型で返す
				return dtTest;

			}

		}
		#endregion
	}
}