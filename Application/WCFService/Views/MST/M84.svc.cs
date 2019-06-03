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
    public class M84 : IM84 {

        /// <summary>
        /// M84_RIKのデータ取得
        /// </summary>
        /// <param name="p運輸局ID">運輸局ID</param>
        /// <returns>M84_RIK_Member</returns>
        public List<M84_RIK_Member> GetData(int? p運輸局ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m84 in context.M84_RIK
                           orderby m84.運輸局ID
                           select new M84_RIK_Member
                           {
                               運輸局ID = m84.運輸局ID,
                               登録日時 = m84.登録日時,
                               更新日時 = m84.更新日時,
                               運輸局名 = m84.運輸局名,
                           }).AsQueryable();

                if (!(p運輸局ID == null))
                {
                    ret = ret.Where(c => c.運輸局ID == p運輸局ID);
                }


                return ret.ToList();
            }
        }

        /// <summary>
        /// M84_RIKの新規追加
        /// </summary>
        /// <param name="m84rik">M84_RIK_Member</param>
        public void Insert(M84_RIK_Member m84rik)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                M84_RIK m84 = new M84_RIK();
                m84.運輸局ID = m84rik.運輸局ID;
                m84.登録日時 = m84rik.登録日時;
                m84.更新日時 = m84rik.更新日時;
                m84.運輸局名 = m84rik.運輸局名;
                try
                {
                    // newﾉｴﾝﾃｨﾃｨﾆ対ｼﾃﾊAcceptChangesﾃﾞ新規追加ﾄﾅﾙ
                    context.M84_RIK.ApplyChanges(m84);
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
        /// M84_RIKの更新
        /// </summary>
        /// <param name="m84rik">M84_RIK_Member</param>
        public void Update(M84_RIK_Member m84rik)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //更新行ｦ特定
                var ret = from x in context.M84_RIK
                          where (x.運輸局ID == m84rik.運輸局ID)
                          orderby x.運輸局ID
                          select x;
                var m84 = ret.FirstOrDefault();
                m84.運輸局ID = m84rik.運輸局ID;
                m84.登録日時 = m84rik.登録日時;
                m84.更新日時 = DateTime.Now;
                m84.運輸局名 = m84rik.運輸局名;

                m84.AcceptChanges();
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M84_RIKの物理削除
        /// </summary>
        /// <param name="m84rik">M84_RIK_Member</param>
        public void Delete(M84_RIK_Member m84rik)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M84_RIK
                          where (x.運輸局ID == m84rik.運輸局ID)
                          orderby x.運輸局ID
                          select x;
                var m84 = ret.FirstOrDefault();

                context.DeleteObject(m84);
                context.SaveChanges();
            }
        }

    }
}
