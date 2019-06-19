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
    public class M50 : IM50 {

		/// <summary>
		/// M50の次データ取得
		/// </summary>
		/// <param name="p距離">距離</param>
		/// <param name="p重量">重量</param>
		/// <returns>M50_RTBL_Member</returns>
		public List<M50_RTBL_Member> M50_NEXT(int iタリフコード)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

				var ret = (from x in context.M50_RTBL.Distinct()
						   where (iタリフコード == 0 || x.タリフコード > iタリフコード)
						   orderby x.タリフコード descending
						   select new M50_RTBL_Member
						   {
							   タリフID = x.タリフコード,

						   }).Distinct().ToList();
				ret = ret.OrderBy(c => c.タリフID).Take(1).ToList();
				return ret;
			}
		}

		/// <summary>
		/// M50の前データ取得
		/// </summary>
		/// <param name="p距離">距離</param>
		/// <param name="p重量">重量</param>
		/// <returns>M50_RTBL_Member</returns>
		public List<M50_RTBL_Member> M50_BEFORE(int iタリフコード)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

				var ret = (from x in context.M50_RTBL.Distinct()
						   where (iタリフコード == 0 || x.タリフコード < iタリフコード)
						   orderby x.タリフコード
						   select new M50_RTBL_Member
						   {
							   タリフID = x.タリフコード,
						   }).Distinct().ToList();

				if (iタリフコード == 0)
				{
					ret = ret.OrderBy(c => c.タリフID).Take(1).ToList();
				}
				else
				{
					ret = ret.OrderByDescending(c => c.タリフID).Take(1).ToList();
				}
				return ret;
			}
		}

		/// <summary>
		/// M50_RTBLのデータ取得
		/// </summary>
		/// <param name="p距離">距離</param>
		/// <param name="p重量">重量</param>
		/// <returns>M50_RTBL_Member</returns>
		public List<M50_RTBL_Member> GetData(int iタリフコード)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

				var ret = (from x in context.M50_RTBL.Distinct()
						   where (x.タリフコード == iタリフコード)
						   select new M50_RTBL_Member
						   {

							   重量 = x.重量,
							   距離 = x.距離,
							   運賃 = x.運賃,
							   削除日付 = x.削除日付,
						   }).Distinct().ToList();
				return ret;
			}
		}

        /// <summary>
        /// M50_RTBLのデータ取得
        /// </summary>
        /// <param name="p距離">距離</param>
        /// <param name="p重量">重量</param>
        /// <returns>M50_RTBL_Member</returns>
        public List<M50_RTBL_Member> GetRData(int iタリフコード)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from x in context.M50_RTBL.Distinct()
                           where (x.タリフコード == iタリフコード)
                           select new M50_RTBL_Member
                           {
                               重量 = x.重量,
                           }).Distinct().ToList();
                return ret;
            }
        }


        /// <summary>
        /// M50_RTBLのデータ取得
        /// </summary>
        /// <param name="p距離">距離</param>
        /// <param name="p重量">重量</param>
        /// <returns>M50_RTBL_Member</returns>
        public List<M50_RTBL_Member> GetDataColCount(int iタリフコード)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from x in context.M50_RTBL.Distinct()
                           where (x.タリフコード == iタリフコード)
                           orderby (x.重量)
                           select new M50_RTBL_Member
                           {
                               重量 = x.重量,
                           }).Distinct().OrderBy(c => c.重量).ToList();
                return ret;
            }
        }

        /// <summary>
        /// M50_RTBLのデータ取得
        /// </summary>
        /// <param name="p距離">距離</param>
        /// <param name="p重量">重量</param>
        /// <returns>M50_RTBL_Member</returns>
        public List<M50_RTBL_Member> GetDataRowCount(int iタリフコード)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from x in context.M50_RTBL.Distinct()
                           where (x.タリフコード == iタリフコード)
                           orderby (x.重量)
                           select new M50_RTBL_Member
                           {
                               距離 = x.距離,
                           }).Distinct().OrderBy(c => c.距離).ToList();
                return ret;
            }
        }

        /// <summary>
        /// F9(登録ボタン)での登録
        /// </summary>
        /// <param name = "pタリフコード">タリフコード</param>
        /// <param name = "p距離">距離</param>
        /// <param name = "p重量">重量</param>
        public void Insert(DataSet ds)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {    
                context.Connection.Open();
                 // トランザクションのインスタンス化(開始)
                using (var tran = new TransactionScope())
                {

                    try
                    {
                        DataTable dt = ds.Tables["タリフ登録"];
                        int タリフID, 距離, 重量, 運賃;
                        int Cnt = 0;
                        DateTime 登録日時, 更新日時, 削除日付;
                        削除日付 = DateTime.Now;
                        登録日時 = DateTime.Now;
                        更新日時 = DateTime.Now;
                        int Val = 0;
                        int iタリフID = Convert.ToInt32(dt.Rows[0]["タリフID"].ToString());

                        var query = from x in context.M50_RTBL
                                     where x.タリフコード == iタリフID
                                     select x;
                        //新規登録
                        if (query.Count() == 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                タリフID = Convert.ToInt32(dt.Rows[Cnt]["タリフID"].ToString());
                                距離 = Convert.ToInt32(dt.Rows[Cnt]["距離"].ToString());
                                重量 = Convert.ToInt32(dt.Rows[Cnt]["重量"].ToString());
                                運賃 = Convert.ToInt32(dt.Rows[Cnt]["運賃"].ToString());
                                string sql = string.Empty;
                                sql = string.Format("INSERT INTO M50_RTBL (タリフコード,距離,重量,登録日時,更新日時,運賃,削除日付)VALUES('{0}','{1}','{2}','{3}','{4}','{5}',NULL)"
                                                   , タリフID, 距離, 重量, 登録日時, 更新日時, 運賃);

                                int count = context.ExecuteStoreCommand(sql);
                                Cnt += 1;
                            }
                            tran.Complete();
                            return;
                        }
                        else//編集
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                if (Val == 0)
                                {
                                    string Del = string.Empty;
                                    Del = string.Format("DELETE FROM M50_RTBL WHERE M50_RTBL.タリフコード = '" + iタリフID + "'");
                                    int Delete = context.ExecuteStoreCommand(Del);
                                }

                                Val += 1;

                                タリフID = Convert.ToInt32(dt.Rows[Cnt]["タリフID"].ToString());
                                距離 = Convert.ToInt32(dt.Rows[Cnt]["距離"].ToString());
                                重量 = Convert.ToInt32(dt.Rows[Cnt]["重量"].ToString());
                                運賃 = Convert.ToInt32(dt.Rows[Cnt]["運賃"].ToString());
                                string sql = string.Empty;
                                sql = string.Format("INSERT INTO M50_RTBL (タリフコード,距離,重量,更新日時,運賃)VALUES('{0}','{1}','{2}','{3}','{4}')"
                                                   , タリフID, 距離, 重量, 更新日時, 運賃);

                                int count = context.ExecuteStoreCommand(sql);
                                Cnt += 1;
                            }
                            tran.Complete();
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }


        ///// <summary>
        ///// M50_RTBLの新規追加
        ///// </summary>
        ///// <param name="m50rtbl">M50_RTBL_Member</param>
        //public void Insert(M50_RTBL_Member m50rtbl)
        //{
        //    using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
        //    {
        //        context.Connection.Open();

        //        M50_RTBL m50 = new M50_RTBL();
        //        m50.距離 = m50rtbl.距離;
        //        m50.重量 = m50rtbl.重量;
        //        m50.登録日時 = m50rtbl.登録日時;
        //        m50.更新日時 = m50rtbl.更新日時;
        //        try
        //        {
        //            // newのエンティティに対してはAcceptChangesで新規追加となる
        //            context.M50_RTBL.ApplyChanges(m50);
        //            context.SaveChanges();
        //        }
        //        catch (UpdateException ex)
        //        {
        //            // PKey違反等
        //            Console.WriteLine(ex);
        //        }
        //    }
        //}



        /// <summary>
        /// M50_RTBLの更新
        /// </summary>
        /// <param name="m50rtbl">M50_RTBL_Member</param>
        public void Update(int iタリフID , int i重量 ,int i距離, int i運賃)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                //更新行を特定
                var ret = from x in context.M50_RTBL
                          where (x.タリフコード == iタリフID && x.距離 == i距離 && x.重量 == i重量)
                          orderby x.距離, x.重量
                          select x;
                var m50 = ret.FirstOrDefault();
                if (m50!= null)
                {
                    m50.運賃 = i運賃;
                    m50.更新日時 = DateTime.Now;
                    m50.AcceptChanges();

                }
                else
                {

                    M50_RTBL m50r = new M50_RTBL();
                    m50r.タリフコード = iタリフID;
                    m50r.距離 = i距離;
                    m50r.重量 = i重量;
                    m50r.運賃 = i運賃;
                    m50r.登録日時 = DateTime.Now;
                    context.M50_RTBL.ApplyChanges(m50r);
                }
                context.SaveChanges();
            }
        }




        /// <summary>
        /// M50_RTBLの物理削除
        /// </summary>
        /// <param name="m50RTBL">M50_RTBL_Member</param>
        public void Delete(int iタリフID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                DateTime d削除日付 = DateTime.Now;

                //削除行を特定
                var query = (from x in context.M50_RTBL
                            where (x.タリフコード == iタリフID)
                            orderby x.タリフコード
                            select new M50_RTBL_Member
                            {
                                タリフID = x.タリフコード,
                                距離 = x.距離,
                                重量 = x.重量,
                                運賃 = x.運賃,
                            }).ToList();
                
                using (var tran = new TransactionScope())
                {

                    foreach (var data in query)
                    {
                        string sql = string.Empty;
                        sql = string.Format("UPDATE M50_RTBL SET M50_RTBL.削除日付 = '{3}'  WHERE M50_RTBL.タリフコード = '{0}' AND M50_RTBL.距離 = '{1}' AND M50_RTBL.重量 = '{2}'"
                                           , data.タリフID, data.距離, data.重量 , d削除日付);
                        int count = context.ExecuteStoreCommand(sql);
                    }
                    tran.Complete();
                }



            }
        }



       

        /// <summary>
        /// M50_RTBLの距離の指定削除
        /// </summary>
        /// <param name="m50RTBL">M50_RTBL_Member</param>
        public void Kyori_Delete(int iタリフID, int i距離)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = from x in context.M50_RTBL.Distinct()
                            where (x.タリフコード == iタリフID && x.距離 == i距離)
                            orderby x.タリフコード
                            select x;

                int query_count = query.Count();
                int cnt = 0;
                
                for (int i = 0; i < query_count; i++)
                {
                    var m50 = query.FirstOrDefault();
                    cnt = cnt + 1;
                    if (cnt == 1)
                    {
                        context.DeleteObject(m50);
                    }
                    cnt = 0;
                    context.SaveChanges();
                }
                
            }
        }

        /// <summary>
        /// M50_RTBLの重量の指定削除
        /// </summary>
        /// <param name="m50RTBL">M50_RTBL_Member</param>
        public void Jyuryou_Delete(int iタリフID, int i重量)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = from x in context.M50_RTBL.Distinct()
                             where (x.タリフコード == iタリフID && x.重量 == i重量)
                             orderby x.タリフコード
                             select x;

                int query_count = query.Count();
                int cnt = 0;

                for (int i = 0; i < query_count; i++)
                {
                    var m50 = query.FirstOrDefault();
                    cnt = cnt + 1;
                    if (cnt == 1)
                    {
                        context.DeleteObject(m50);
                    }
                    cnt = 0;

                    context.SaveChanges();
                }
                
            }
        }


        /// <summary>
        /// M02_TTAN3の印刷プレビュー　&& CSV出力
        /// </summary>
        /// <param name="m02ttan3">M50_RTBL_Member</param>
        public List<M50_RTBL_Member> GetDataHinList(int? iタリフコード , int[] iタリフList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<M50_RTBL_Member> retList = new List<M50_RTBL_Member>();
                context.Connection.Open();


                //全件表示
                var query = (from x in context.M50_RTBL
                             select new M50_RTBL_Member
                             {
                                 タリフID = x.タリフコード,
                                 運賃 = x.運賃,
                                 距離 = x.距離,
                                 重量 = x.重量,
                             });
                    if (iタリフコード == 0 && iタリフList.Length == 0)
                    {
                        query = query.Where(c => c.タリフID >= int.MinValue && c.タリフID <= int.MaxValue);
                    }
                    else
                    {
                        query = query.Where(c => c.タリフID == iタリフコード);
                    }
                    if (iタリフList.Length > 0)
                    {
                        var intCause = iタリフList;

                        query = (from x in context.M50_RTBL
                                 where intCause.Contains(x.タリフコード) 
                                 select new M50_RTBL_Member
                                 {
                                     タリフID = x.タリフコード,
                                     運賃 = x.運賃,
                                     距離 = x.距離,
                                     重量 = x.重量,
                                 });
                    }
                        //結果をリスト化
                        retList = query.ToList();
                        return retList;
            }
        }

    }
}
