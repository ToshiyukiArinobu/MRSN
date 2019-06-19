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
    public class M81 : IM81 {

        /// <summary>
        /// M81_OYKのデータ取得
        /// </summary>
        /// <param name="p親子区分ID">親子区分ID</param>
        /// <returns>M81_OYK_Member</returns>
        public M81_OYK_Member GetData(int p親子区分ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m81 in context.M81_OYK
                           where m81.親子区分ID == p親子区分ID
                           orderby m81.親子区分ID
                           select new M81_OYK_Member
                           {
                               親子区分ID = m81.親子区分ID,
                               登録日時 = m81.登録日時,
                               更新日時 = m81.更新日時,
                               親子区分 = m81.親子区分名,
                           });
                return ret.FirstOrDefault();
            }
        }

        /// <summary>
        /// M81_OYKの新規追加
        /// </summary>
        /// <param name="m81oyk">M81_OYK_Member</param>
        public void Insert(M81_OYK_Member m81oyk)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                M81_OYK m81 = new M81_OYK();
                m81.親子区分ID = m81oyk.親子区分ID;
                m81.登録日時 = m81oyk.登録日時;
                m81.更新日時 = m81oyk.更新日時;
				m81.親子区分名 = m81oyk.親子区分;
                try
                {
                    // newﾉｴﾝﾃｨﾃｨﾆ対ｼﾃﾊAcceptChangesﾃﾞ新規追加ﾄﾅﾙ
                    context.M81_OYK.ApplyChanges(m81);
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
        /// M81_OYKの更新
        /// </summary>
        /// <param name="m81oyk">M81_OYK_Member</param>
        public void Update(M81_OYK_Member m81oyk)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //更新行ｦ特定
                var ret = from x in context.M81_OYK
                          where (x.親子区分ID == m81oyk.親子区分ID)
                          orderby x.親子区分ID
                          select x;
                var m81 = ret.FirstOrDefault();
                m81.親子区分ID = m81oyk.親子区分ID;
                m81.登録日時 = m81oyk.登録日時;
                m81.更新日時 = DateTime.Now;
				m81.親子区分名 = m81oyk.親子区分;

                m81.AcceptChanges();
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M81_OYKの物理削除
        /// </summary>
        /// <param name="m81oyk">M81_OYK_Member</param>
        public void Delete(M81_OYK_Member m81oyk)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M81_OYK
                          where (x.親子区分ID == m81oyk.親子区分ID)
                          orderby x.親子区分ID
                          select x;
                var m81 = ret.FirstOrDefault();

                context.DeleteObject(m81);
                context.SaveChanges();
            }
        }

    }
}
