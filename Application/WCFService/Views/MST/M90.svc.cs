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
    public class M90 : IM90 {

        public List<M90_ZEI_Member> M90GetData()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<M90_ZEI_Member> retList = new List<M90_ZEI_Member>();
                context.Connection.Open();

                //消費税マスタデータ取得
                var ret = (from x in context.M73_ZEI
                           where x.削除日時 == null
                           select new M90_ZEI_Member
                           {
                               適用開始日付 = x.適用開始日付,
                               登録日時 = x.登録日時,
                               更新日時 = x.更新日時,
                               消費税率 = x.消費税率,
                               削除日時 = x.削除日時,
                           }).AsQueryable();
                retList = ret.ToList();
                return retList;
            }
        }
        

        /// <summary>
        /// F9(登録ボタン)での登録
        /// </summary>
        /// <param name = "pタリフコード">タリフコード</param>
        /// <param name = "p距離">距離</param>
        /// <param name = "p重量">重量</param>
        public void M90_Insert(DataSet ds)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {    
                 // トランザクションのインスタンス化(開始)
                using (var tran = new TransactionScope())
                {
                    try
                    {
                        List<M90_ZEI_Member> retList = new List<M90_ZEI_Member>();
                        context.Connection.Open();
                        DataTable dt = ds.Tables["消費税マスタCSV登録"];

                        int 消費税率;
                        int Cnt = 0;
                        string 適用開始日付 = string.Empty;
                        string 削除日付 = string.Empty;
                        DateTime 登録日時, 更新日時;

                        string sql = string.Empty;
                        foreach (DataRow row in dt.Rows)
                        {
                            適用開始日付 = dt.Rows[Cnt]["適用開始日付"].ToString().Substring(0, 10);
                            登録日時 = Convert.ToDateTime(dt.Rows[Cnt]["登録日時"].ToString());
                            更新日時 = Convert.ToDateTime(dt.Rows[Cnt]["更新日時"].ToString());
                            消費税率 = Convert.ToInt32(dt.Rows[Cnt]["消費税率"].ToString());
                            削除日付 = dt.Rows[Cnt]["削除日付"].ToString() == "" ? "NULL" : dt.Rows[Cnt]["削除日付"].ToString();

                            //新規行登録処理
                            sql = string.Empty;
                            sql = string.Format("INSERT INTO M73_ZEI (適用開始日付,登録日時,更新日時,消費税率,削除日時)VALUES('{0}','{1}','{2}','{3}',{4})"
                                                , 適用開始日付, 登録日時, 更新日時, 消費税率, 削除日付);
                            int count = context.ExecuteStoreCommand(sql);
                            Cnt += 1;
                        }
                        tran.Complete();
                        return;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("シートの中で重複したデータがあるため、登録できませんでした。");
                        return;
                        throw ex;
                    }
                }
            }
        }



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
        //public void Delete(int iタリフID)
        //{
        //    using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
        //    {
        //        context.Connection.Open();
        //        DateTime d削除日付 = DateTime.Now;

        //        //削除行を特定
        //        var query = (from x in context.M50_RTBL
        //                    where (x.タリフコード == iタリフID)
        //                    orderby x.タリフコード
        //                    select new M50_RTBL_Member
        //                    {
        //                        タリフID = x.タリフコード,
        //                        距離 = x.距離,
        //                        重量 = x.重量,
        //                        運賃 = x.運賃,
        //                    }).ToList();
                
        //        using (var tran = new TransactionScope())
        //        {

        //            foreach (var data in query)
        //            {
        //                string sql = string.Empty;
        //                sql = string.Format("UPDATE M50_RTBL SET M50_RTBL.削除日付 = '{3}'  WHERE M50_RTBL.タリフコード = '{0}' AND M50_RTBL.距離 = '{1}' AND M50_RTBL.重量 = '{2}'"
        //                                   , data.タリフID, data.距離, data.重量 , d削除日付);
        //                int count = context.ExecuteStoreCommand(sql);
        //            }
        //            tran.Complete();
        //        }



        //    }
        //}



       

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


        
    }
}
