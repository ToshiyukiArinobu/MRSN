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


namespace KyoeiSystem.Application.WCFService
{
    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コード、svc、および config ファイルで同時にクラス名 "DataDriveLogService" を変更できます。
    // 注意: このサービスをテストするために WCF テスト クライアントを起動するには、ソリューション エクスプローラーで DataDriveLogService.svc または DataDriveLogService.svc.cs を選択し、デバッグを開始してください。
    public class M17 : IM17 {

        public class SERCHE_MST32010
        {
            public int 車輌KEY { get; set; }
            public int 車輌コード { get; set; }
            public string 車番 { get; set; }
            public decimal 月1 { get; set; }
            public decimal 月2 { get; set; }
            public decimal 月3 { get; set; }
            public decimal 月4 { get; set; }
            public decimal 月5 { get; set; }
            public decimal 月6 { get; set; }
            public decimal 月7 { get; set; }
            public decimal 月8 { get; set; }
            public decimal 月9 { get; set; }
            public decimal 月10 { get; set; }
            public decimal 月11 { get; set; }
            public decimal 月12 { get; set; }
            public int 年月1 { get; set; }
            public int 年月2 { get; set; }
            public int 年月3 { get; set; }
            public int 年月4 { get; set; }
            public int 年月5 { get; set; }
            public int 年月6 { get; set; }
            public int 年月7 { get; set; }
            public int 年月8 { get; set; }
            public int 年月9 { get; set; }
            public int 年月10 { get; set; }
            public int 年月11 { get; set; }
            public int 年月12 { get; set; }
            public int? Flg { get; set; }
        }

        public class UPDATE_MST32010
        {
            [DataMember] public int 車輌KEY { get; set; }
        }

        #region LOAD時

        /// <summary>
        /// LOAD時にセットするデータ
        /// </summary>
        /// <returns></returns>
        public List<SERCHE_MST32010> LOAD_GetData()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m17 in context.M17_CYSN
                             from m05 in context.M05_CAR.Where(c => c.車輌KEY == m17.車輌KEY)
                             from m06 in context.M06_SYA.Where(c => c.車種ID == m05.車種ID)
                             select new SERCHE_MST32010
                             {
                                 車輌コード = m05.車輌ID,
                                 車番 = m05.車輌番号,

                             }).Distinct().AsQueryable();

                return query.ToList();
            }
        }

        #endregion

        #region 作成年月でデータ取得

        /// <summary>
        /// 作成年月でデータ取得
        /// </summary>
        /// <param name="s作成年月"></param>
        /// <returns></returns>
        public List<SERCHE_MST32010> SEARCH_GetData(string s作成年月)
        {

            DateTime d年月 = Convert.ToDateTime(s作成年月.Substring(0, 4).ToString() + "/" + s作成年月.Substring(5, 2).ToString() + "/" + "01");
            int i年月1 = AppCommon.IntParse(d年月.Year.ToString() + d年月.ToString("MM"));
            int i年月2 = AppCommon.IntParse(d年月.AddMonths(1).Year.ToString() + d年月.AddMonths(1).ToString("MM"));
            int i年月3 = AppCommon.IntParse(d年月.AddMonths(2).Year.ToString() + d年月.AddMonths(2).ToString("MM"));
            int i年月4 = AppCommon.IntParse(d年月.AddMonths(3).Year.ToString() + d年月.AddMonths(3).ToString("MM"));
            int i年月5 = AppCommon.IntParse(d年月.AddMonths(4).Year.ToString() + d年月.AddMonths(4).ToString("MM"));
            int i年月6 = AppCommon.IntParse(d年月.AddMonths(5).Year.ToString() + d年月.AddMonths(5).ToString("MM"));
            int i年月7 = AppCommon.IntParse(d年月.AddMonths(6).Year.ToString() + d年月.AddMonths(6).ToString("MM"));
            int i年月8 = AppCommon.IntParse(d年月.AddMonths(7).Year.ToString() + d年月.AddMonths(7).ToString("MM"));
            int i年月9 = AppCommon.IntParse(d年月.AddMonths(8).Year.ToString() + d年月.AddMonths(8).ToString("MM"));
            int i年月10 = AppCommon.IntParse(d年月.AddMonths(9).Year.ToString() + d年月.AddMonths(9).ToString("MM"));
            int i年月11 = AppCommon.IntParse(d年月.AddMonths(10).Year.ToString() + d年月.AddMonths(10).ToString("MM"));
            int i年月12 = AppCommon.IntParse(d年月.AddMonths(11).Year.ToString() + d年月.AddMonths(11).ToString("MM"));


            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m05 in context.M05_CAR.Where(c => c.削除日付 == null)
                             join m17 in context.M17_CYSN on m05.車輌KEY equals m17.車輌KEY into m17Group
                           select new SERCHE_MST32010
                           {
                               車輌KEY = m05.車輌KEY,
                               車輌コード = m05.車輌ID,
                               車番 = m05.車輌番号,
                               月1 = m17Group.Where(c => c.年月 == i年月1).Select(c => c.売上予算).FirstOrDefault(),
                               月2 = m17Group.Where(c => c.年月 == i年月2).Select(c => c.売上予算).FirstOrDefault(),
                               月3 = m17Group.Where(c => c.年月 == i年月3).Select(c => c.売上予算).FirstOrDefault(),
                               月4 = m17Group.Where(c => c.年月 == i年月4).Select(c => c.売上予算).FirstOrDefault(),
                               月5 = m17Group.Where(c => c.年月 == i年月5).Select(c => c.売上予算).FirstOrDefault(),
                               月6 = m17Group.Where(c => c.年月 == i年月6).Select(c => c.売上予算).FirstOrDefault(),
                               月7 = m17Group.Where(c => c.年月 == i年月7).Select(c => c.売上予算).FirstOrDefault(),
                               月8 = m17Group.Where(c => c.年月 == i年月8).Select(c => c.売上予算).FirstOrDefault(),
                               月9 = m17Group.Where(c => c.年月 == i年月9).Select(c => c.売上予算).FirstOrDefault(),
                               月10 = m17Group.Where(c => c.年月 == i年月10).Select(c => c.売上予算).FirstOrDefault(),
                               月11 = m17Group.Where(c => c.年月 == i年月11).Select(c => c.売上予算).FirstOrDefault(),
                               月12 = m17Group.Where(c => c.年月 == i年月12).Select(c => c.売上予算).FirstOrDefault(),
                               年月1 = i年月1,
                               年月2 = i年月2,
                               年月3 = i年月3,
                               年月4 = i年月4,
                               年月5 = i年月5,
                               年月6 = i年月6,
                               年月7 = i年月7,
                               年月8 = i年月8,
                               年月9 = i年月9,
                               年月10 = i年月10,
                               年月11 = i年月11,
                               年月12 = i年月12,
                           }).AsQueryable();
                var queryLIST = query.ToList();

                return queryLIST;
            }
        }

        #endregion

        #region 先月データ取得

        /// <summary>
        /// 先月データ取得
        /// </summary>
        /// <param name="s作成年月"></param>
        /// <returns></returns>
		public List<SERCHE_MST32010> LAST_MANTH_MST32010(string s作成年月)
		{
			DateTime d年月 = Convert.ToDateTime(s作成年月.Substring(0, 4).ToString() + "/" + s作成年月.Substring(5, 2).ToString() + "/" + "01");

			int ii年月1 = AppCommon.IntParse(d年月.Year.ToString() + d年月.ToString("MM"));
			int ii年月2 = AppCommon.IntParse(d年月.AddMonths(1).Year.ToString() + d年月.AddMonths(1).ToString("MM"));
			int ii年月3 = AppCommon.IntParse(d年月.AddMonths(2).Year.ToString() + d年月.AddMonths(2).ToString("MM"));
			int ii年月4 = AppCommon.IntParse(d年月.AddMonths(3).Year.ToString() + d年月.AddMonths(3).ToString("MM"));
			int ii年月5 = AppCommon.IntParse(d年月.AddMonths(4).Year.ToString() + d年月.AddMonths(4).ToString("MM"));
			int ii年月6 = AppCommon.IntParse(d年月.AddMonths(5).Year.ToString() + d年月.AddMonths(5).ToString("MM"));
			int ii年月7 = AppCommon.IntParse(d年月.AddMonths(6).Year.ToString() + d年月.AddMonths(6).ToString("MM"));
			int ii年月8 = AppCommon.IntParse(d年月.AddMonths(7).Year.ToString() + d年月.AddMonths(7).ToString("MM"));
			int ii年月9 = AppCommon.IntParse(d年月.AddMonths(8).Year.ToString() + d年月.AddMonths(8).ToString("MM"));
			int ii年月10 = AppCommon.IntParse(d年月.AddMonths(9).Year.ToString() + d年月.AddMonths(9).ToString("MM"));
			int ii年月11 = AppCommon.IntParse(d年月.AddMonths(10).Year.ToString() + d年月.AddMonths(10).ToString("MM"));
			int ii年月12 = AppCommon.IntParse(d年月.AddMonths(11).Year.ToString() + d年月.AddMonths(11).ToString("MM"));


			d年月 = d年月.AddYears(-1);

			int i年月1 = AppCommon.IntParse(d年月.Year.ToString() + d年月.ToString("MM"));
			int i年月2 = AppCommon.IntParse(d年月.AddMonths(1).Year.ToString() + d年月.AddMonths(1).ToString("MM"));
			int i年月3 = AppCommon.IntParse(d年月.AddMonths(2).Year.ToString() + d年月.AddMonths(2).ToString("MM"));
			int i年月4 = AppCommon.IntParse(d年月.AddMonths(3).Year.ToString() + d年月.AddMonths(3).ToString("MM"));
			int i年月5 = AppCommon.IntParse(d年月.AddMonths(4).Year.ToString() + d年月.AddMonths(4).ToString("MM"));
			int i年月6 = AppCommon.IntParse(d年月.AddMonths(5).Year.ToString() + d年月.AddMonths(5).ToString("MM"));
			int i年月7 = AppCommon.IntParse(d年月.AddMonths(6).Year.ToString() + d年月.AddMonths(6).ToString("MM"));
			int i年月8 = AppCommon.IntParse(d年月.AddMonths(7).Year.ToString() + d年月.AddMonths(7).ToString("MM"));
			int i年月9 = AppCommon.IntParse(d年月.AddMonths(8).Year.ToString() + d年月.AddMonths(8).ToString("MM"));
			int i年月10 = AppCommon.IntParse(d年月.AddMonths(9).Year.ToString() + d年月.AddMonths(9).ToString("MM"));
			int i年月11 = AppCommon.IntParse(d年月.AddMonths(10).Year.ToString() + d年月.AddMonths(10).ToString("MM"));
			int i年月12 = AppCommon.IntParse(d年月.AddMonths(11).Year.ToString() + d年月.AddMonths(11).ToString("MM"));


			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

				var query = (from m05 in context.M05_CAR.Where(c => c.削除日付 == null)
							 join m17 in context.M17_CYSN on m05.車輌KEY equals m17.車輌KEY into m17Group
							 select new SERCHE_MST32010
							 {
								 車輌KEY = m05.車輌KEY,
								 車輌コード = m05.車輌ID,
								 車番 = m05.車輌番号,
								 月1 = m17Group.Where(c => c.年月 == i年月1).Select(c => c.売上予算).FirstOrDefault(),
								 月2 = m17Group.Where(c => c.年月 == i年月2).Select(c => c.売上予算).FirstOrDefault(),
								 月3 = m17Group.Where(c => c.年月 == i年月3).Select(c => c.売上予算).FirstOrDefault(),
								 月4 = m17Group.Where(c => c.年月 == i年月4).Select(c => c.売上予算).FirstOrDefault(),
								 月5 = m17Group.Where(c => c.年月 == i年月5).Select(c => c.売上予算).FirstOrDefault(),
								 月6 = m17Group.Where(c => c.年月 == i年月6).Select(c => c.売上予算).FirstOrDefault(),
								 月7 = m17Group.Where(c => c.年月 == i年月7).Select(c => c.売上予算).FirstOrDefault(),
								 月8 = m17Group.Where(c => c.年月 == i年月8).Select(c => c.売上予算).FirstOrDefault(),
								 月9 = m17Group.Where(c => c.年月 == i年月9).Select(c => c.売上予算).FirstOrDefault(),
								 月10 = m17Group.Where(c => c.年月 == i年月10).Select(c => c.売上予算).FirstOrDefault(),
								 月11 = m17Group.Where(c => c.年月 == i年月11).Select(c => c.売上予算).FirstOrDefault(),
								 月12 = m17Group.Where(c => c.年月 == i年月12).Select(c => c.売上予算).FirstOrDefault(),
								 年月1 = ii年月1,
								 年月2 = ii年月2,
								 年月3 = ii年月3,
								 年月4 = ii年月4,
								 年月5 = ii年月5,
								 年月6 = ii年月6,
								 年月7 = ii年月7,
								 年月8 = ii年月8,
								 年月9 = ii年月9,
								 年月10 = ii年月10,
								 年月11 = ii年月11,
								 年月12 = ii年月12,
							 }).AsQueryable();
				var queryLIST = query.ToList();

				return queryLIST;
			}
        }

        #endregion

        #region INSERT OR UPDATE

        /// <summary>
        /// Spread情報変更 【UPDATE】
        /// </summary>
        /// <param name="p明細番号"></param>
        /// <param name="p明細行"></param>
        /// <param name="colname"></param>
        /// <param name="val"></param>
        public void INSERT_GetData(object o車輌コード, object s車番, object s車種, string s目標燃費 , object val , string s作成年月)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {

                //*** 変数定義 ***//
                int i年月 = Convert.ToInt32(s作成年月.Substring(0, 4) + s作成年月.Substring(5, 2));
                int i車輌コード = Convert.ToInt32(o車輌コード);
                decimal d目標燃費 = Convert.ToDecimal(val);
                DateTime d更新日時 = DateTime.Now;
                DateTime d登録日時 = DateTime.Now;
                //***   終了   ***//

                using (var tran = new TransactionScope())
                {
                    var query = (from m17 in context.M17_CYSN
                                 from m05 in context.M05_CAR.Where(c => c.車輌KEY == m17.車輌KEY)
                                 from m06 in context.M06_SYA.Where(c => c.車種ID == m05.車種ID)
                                 where m05.車輌ID == i車輌コード && m17.年月 == i年月
                                 select new SERCHE_MST32010
                                 {
                                     車輌コード = m05.車輌ID,
                                     車番 = m05.車輌番号,
                                     //車種 = m06.車種名,
                                     //目標燃費 = m17.目標燃費,
                                 }).AsQueryable();

                    var ret = (from m17 in context.M17_CYSN
                               from m05 in context.M05_CAR.Where(c => c.車輌KEY == m17.車輌KEY)
                               where m05.車輌ID == i車輌コード
                               select new UPDATE_MST32010
                               {
                                   車輌KEY = m17.車輌KEY,
                               }).AsQueryable();

                    List<UPDATE_MST32010> retList = ret.ToList();
                    int 車輌KEY = retList[0].車輌KEY;
                    string sql = string.Empty;
                    if (query.Count() == 0)
                    {
                        //INSERT処理
                        sql = string.Format("INSERT INTO M17_CYSN (車輌KEY , 年月 , 登録日時 , 更新日時 , 目標燃費 , 削除日付)VALUES('{0}','{1}','{2}','','{3}',NULL)"
                                           , 車輌KEY, i年月, d登録日時, d目標燃費);
                    }
                    else
                    {
                        //UPDATE処理
                        sql = string.Format("UPDATE M17_CYSN SET 目標燃費 = '{3}' , 更新日時 = '{2}' WHERE 車輌KEY = {0} AND 年月 = {1}"
                                            , 車輌KEY, i年月, d更新日時, d目標燃費);
                    }
                    context.Connection.Open();
                    int count = context.ExecuteStoreCommand(sql);
                    // トリガが定義されていると、更新結果は複数行になる
                    if (count > 0)
                    {
                        tran.Complete();
                    }
                    else
                    {
                        // 更新行なし
                        throw new Framework.Common.DBDataNotFoundException();
                    }

                }
            }
        }

        #endregion

        /// <summary>
        /// F9(登録ボタン)での登録
        /// </summary>
        public void NINSERT_GetData(List<SERCHE_MST32010> dt, string s作成年月)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                using (var tran = new TransactionScope())
                {

                    try
                    {
                        DateTime d年月 = Convert.ToDateTime(s作成年月.Substring(0, 4) + "/" + s作成年月.Substring(5, 2) + "/01");
                        int i開始年月 = Convert.ToInt32(s作成年月.Substring(0, 4) + s作成年月.Substring(5, 2));
                        int i終了年月 = AppCommon.IntParse(d年月.AddMonths(11).Year.ToString() + d年月.AddMonths(11).ToString("MM"));

                        var del = (from m17 in context.M17_CYSN where m17.年月 >= i開始年月 && m17.年月 <= i終了年月 select m17);
                        foreach (var row in del)
                        {
                            context.DeleteObject(row);
                        };

                        foreach (var row in dt)
                        {
                            M17_CYSN m17_row = new M17_CYSN();
                            m17_row.車輌KEY = row.車輌KEY;
                            m17_row.年月 = row.年月1;
                            m17_row.売上予算 = row.月1;
                            m17_row.粗利予算 = 0;
                            // newのエンティティに対してはAcceptChangesで新規追加となる
                            context.M17_CYSN.ApplyChanges(m17_row);
                            context.SaveChanges();

                            M17_CYSN m17_row2 = new M17_CYSN(); 
                            m17_row2.車輌KEY = row.車輌KEY;
                            m17_row2.年月 = row.年月2;
                            m17_row2.売上予算 = row.月2;
                            m17_row2.粗利予算 = 0;
                            // newのエンティティに対してはAcceptChangesで新規追加となる
                            context.M17_CYSN.ApplyChanges(m17_row2);
                            context.SaveChanges();

                            M17_CYSN m17_row3 = new M17_CYSN();
                            m17_row3.車輌KEY = row.車輌KEY;
                            m17_row3.年月 = row.年月3;
                            m17_row3.売上予算 = row.月3;
                            m17_row3.粗利予算 = 0;
                            // newのエンティティに対してはAcceptChangesで新規追加となる
                            context.M17_CYSN.ApplyChanges(m17_row3);
                            context.SaveChanges();

                            M17_CYSN m17_row4 = new M17_CYSN();
                            m17_row4.車輌KEY = row.車輌KEY;
                            m17_row4.年月 = row.年月4;
                            m17_row4.売上予算 = row.月4;
                            m17_row4.粗利予算 = 0;
                            // newのエンティティに対してはAcceptChangesで新規追加となる
                            context.M17_CYSN.ApplyChanges(m17_row4);
                            context.SaveChanges();

                            M17_CYSN m17_row5 = new M17_CYSN();
                            m17_row5.車輌KEY = row.車輌KEY;
                            m17_row5.年月 = row.年月5;
                            m17_row5.売上予算 = row.月5;
                            m17_row5.粗利予算 = 0;
                            // newのエンティティに対してはAcceptChangesで新規追加となる
                            context.M17_CYSN.ApplyChanges(m17_row5);
                            context.SaveChanges();

                            M17_CYSN m17_row6 = new M17_CYSN();
                            m17_row6.車輌KEY = row.車輌KEY;
                            m17_row6.年月 = row.年月6;
                            m17_row6.売上予算 = row.月6;
                            m17_row6.粗利予算 = 0;
                            // newのエンティティに対してはAcceptChangesで新規追加となる
                            context.M17_CYSN.ApplyChanges(m17_row6);
                            context.SaveChanges();

                            M17_CYSN m17_row7 = new M17_CYSN();
                            m17_row7.車輌KEY = row.車輌KEY;
                            m17_row7.年月 = row.年月7;
                            m17_row7.売上予算 = row.月7;
                            m17_row7.粗利予算 = 0;
                            // newのエンティティに対してはAcceptChangesで新規追加となる
                            context.M17_CYSN.ApplyChanges(m17_row7);
                            context.SaveChanges();

                            M17_CYSN m17_row8 = new M17_CYSN();
                            m17_row8.車輌KEY = row.車輌KEY;
                            m17_row8.年月 = row.年月8;
                            m17_row8.売上予算 = row.月8;
                            m17_row8.粗利予算 = 0;
                            // newのエンティティに対してはAcceptChangesで新規追加となる
                            context.M17_CYSN.ApplyChanges(m17_row8);
                            context.SaveChanges();

                            M17_CYSN m17_row9 = new M17_CYSN();
                            m17_row9.車輌KEY = row.車輌KEY;
                            m17_row9.年月 = row.年月9;
                            m17_row9.売上予算 = row.月9;
                            m17_row9.粗利予算 = 0;
                            // newのエンティティに対してはAcceptChangesで新規追加となる
                            context.M17_CYSN.ApplyChanges(m17_row9);
                            context.SaveChanges();

                            M17_CYSN m17_row10 = new M17_CYSN();
                            m17_row10.車輌KEY = row.車輌KEY;
                            m17_row10.年月 = row.年月10;
                            m17_row10.売上予算 = row.月10;
                            m17_row10.粗利予算 = 0;
                            // newのエンティティに対してはAcceptChangesで新規追加となる
                            context.M17_CYSN.ApplyChanges(m17_row10);
                            context.SaveChanges();

                            M17_CYSN m17_row11 = new M17_CYSN();
                            m17_row11.車輌KEY = row.車輌KEY;
                            m17_row11.年月 = row.年月11;
                            m17_row11.売上予算 = row.月11;
                            m17_row11.粗利予算 = 0;
                            // newのエンティティに対してはAcceptChangesで新規追加となる
                            context.M17_CYSN.ApplyChanges(m17_row11);
                            context.SaveChanges();

                            M17_CYSN m17_row12 = new M17_CYSN();
                            m17_row12.車輌KEY = row.車輌KEY;
                            m17_row12.年月 = row.年月12;
                            m17_row12.売上予算 = row.月12;
                            m17_row12.粗利予算 = 0;
                            // newのエンティティに対してはAcceptChangesで新規追加となる
                            context.M17_CYSN.ApplyChanges(m17_row12);
                            context.SaveChanges();

                        };

                        tran.Complete();
                        return;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }



        /// <summary>
        /// M17_CYSNのデータ取得
        /// </summary>
        /// <param name="p規制区分ID">規制区分ID</param>
        /// <returns>M17_CYSN_Member</returns>
        public M17_CYSN_Member GetData(int p車両ID, int p年月)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m17 in context.M17_CYSN
                           where (m17.車輌KEY == p車両ID && m17.年月 == p年月)
                           select new M17_CYSN_Member
                           {
                               //車両ID = m17.車輌KEY,
                               年月 = m17.年月,
                               //登録日時 = m17.登録日時,
                               //更新日時 = m17.更新日時,
                               //目標燃費 = m17.目標燃費,
                           });
                return ret.FirstOrDefault();
            }
        }

        /// <summary>
        /// M17_CYSNの新規追加
        /// </summary>
        /// <param name="m17cysn">M17_CYSN_Member</param>
        public void Insert(M17_CYSN_Member m17cysn)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                M17_CYSN m17 = new M17_CYSN();
                //m17.車輌KEY = m17cysn.車両ID;
                m17.年月 = m17cysn.年月;
                //m17.登録日時 = m17cysn.登録日時;
                //m17.更新日時 = m17cysn.更新日時;
                //m17.目標燃費 = m17cysn.目標燃費;
                try
                {
                    // newのエンティティに対してはAcceptChangesで新規追加となる
                    context.M17_CYSN.ApplyChanges(m17);
                    context.SaveChanges();
                }
                catch (UpdateException ex)
                {
                    // PKey違反等
                    Console.WriteLine(ex);
                }
            }
        }

        /// <summary>
        /// M17_CYSNの更新
        /// </summary>
        /// <param name="m17cysn">M17_CYSN_Member</param>
        public void Update(M17_CYSN_Member m17cysn)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //更新行を特定
                var ret = from x in context.M17_CYSN
						  where (x.車輌KEY == m17cysn.車両KEY && x.年月 == m17cysn.年月)
						  orderby x.車輌KEY, x.年月
                          select x;
                var m17 = ret.FirstOrDefault();
                //m17.車輌KEY = m17cysn.車両ID;
                m17.年月 = m17cysn.年月;
                //m17.登録日時 = m17cysn.登録日時;
                //m17.更新日時 = DateTime.Now;
                //m17.目標燃費 = m17cysn.目標燃費;

                m17.AcceptChanges();
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M17_CYSNの物理削除
        /// </summary>
        /// <param name="m17cysn">M17_CYSN_Member</param>
        public void Delete(M17_CYSN_Member m17cysn)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M17_CYSN
						  where (x.車輌KEY == m17cysn.車両KEY && x.年月 == m17cysn.年月)
						  orderby x.車輌KEY, x.年月
                          select x;
                var m17 = ret.FirstOrDefault();

                context.DeleteObject(m17);
                context.SaveChanges();
            }
        }

    }
}
