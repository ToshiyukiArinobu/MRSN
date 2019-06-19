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
    public class M15 : IM15 {

        /// <summary>
        /// M15_KOMのデータ取得
        /// </summary>
        /// <param name="pプログラムID">pプログラムID</param>
        /// <param name="p項目ID">項目ID</param>
        /// <returns>M15_KOM_Member</returns>
        public M15_KOM_Member GetData(string pプログラムID, int p項目ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m15 in context.M15_KOM
                           where (m15.プログラムID == pプログラムID && m15.項目ID == p項目ID)
                           select new M15_KOM_Member
                           {
                               プログラムID = m15.プログラムID,
                               項目ID = m15.項目ID,
                               明細区分 = m15.明細区分,
                               項目名 = m15.項目名,
                               項目変数名 = m15.項目変数名,
                               H = m15.H,
                               A1 = m15.A1,
                               A2 = m15.A2,
                               B1 = m15.B1,
                               B2 = m15.B2,
                               T1 = m15.T1,
                               T2 = m15.T2,
                           });
                return ret.FirstOrDefault();
            }
        }

        /// <summary>
        /// M15_KOMの新規追加
        /// </summary>
        /// <param name="m15kom">M15_KOM_Member</param>
        public void Insert(M15_KOM_Member m15kom)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                M15_KOM m15 = new M15_KOM();
                m15.プログラムID = m15kom.プログラムID;
                m15.項目ID = m15kom.項目ID;
                m15.明細区分 = m15kom.明細区分;
                m15.項目名 = m15kom.項目名;
                m15.項目変数名 = m15kom.項目変数名;
                m15.H = m15kom.H;
                m15.A1 = m15kom.A1;
                m15.A2 = m15kom.A2;
                m15.B1 = m15kom.B1;
                m15.B2 = m15kom.B2;
                m15.T1 = m15kom.T1;
                m15.T2 = m15kom.T2;
                try
                {
                    // newのエンティティに対してはAcceptChangesで新規追加となる
                    context.M15_KOM.ApplyChanges(m15);
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
        /// M15_KOMの更新
        /// </summary>
        /// <param name="m15kom">M15_KOM_Member</param>
        public void Update(M15_KOM_Member m15kom)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //更新行を特定
                var ret = from x in context.M15_KOM
                          where (x.プログラムID == m15kom.プログラムID && x.項目ID == m15kom.項目ID)
                          orderby x.プログラムID, x.項目ID
                          select x;
                var m15 = ret.FirstOrDefault();
                m15.プログラムID = m15kom.プログラムID;
                m15.項目ID = m15kom.項目ID;
                m15.明細区分 = m15kom.明細区分;
                m15.項目名 = m15kom.項目名;
                m15.項目変数名 = m15kom.項目変数名;
                m15.H = m15kom.H;
                m15.A1 = m15kom.A1;
                m15.A2 = m15kom.A2;
                m15.B1 = m15kom.B1;
                m15.B2 = m15kom.B2;
                m15.T1 = m15kom.T1;
                m15.T2 = m15kom.T2;

                m15.AcceptChanges();
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M15_KOMの物理削除
        /// </summary>
        /// <param name="m15kom">M15_KOM_Member</param>
        public void Delete(M15_KOM_Member m15kom)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M15_KOM
                          where (x.プログラムID == m15kom.プログラムID && x.項目ID == m15kom.項目ID)
                          orderby x.プログラムID, x.項目ID
                          select x;
                var m15 = ret.FirstOrDefault();

                context.DeleteObject(m15);
                context.SaveChanges();
            }
        }

    }
}
