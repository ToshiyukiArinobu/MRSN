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
    public class M79 : IM79 {

        /// <summary>
        /// M79_ZKBのデータ取得
        /// </summary>
        /// <param name="p出勤区分ID">出勤区分ID</param>
        /// <returns>M79_ZKB_Member</returns>
        public M79_ZKB_Member GetData(int p出勤区分ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m79 in context.M79_ZKB
                           where m79.税区分ID == p出勤区分ID
						   orderby m79.税区分ID
                           select new M79_ZKB_Member
                           {
							   出勤区分ID = m79.税区分ID,
                               登録日時 = m79.登録日時,
                               更新日時 = m79.更新日時,
                               税区分 = m79.税区分名,
                           });
                return ret.FirstOrDefault();
            }
        }

        /// <summary>
        /// M79_ZKBの新規追加
        /// </summary>
        /// <param name="m79zkb">M79_ZKB_Member</param>
        public void Insert(M79_ZKB_Member m79zkb)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                M79_ZKB m79 = new M79_ZKB();
                m79.税区分ID = m79zkb.出勤区分ID;
                m79.登録日時 = m79zkb.登録日時;
                m79.更新日時 = m79zkb.更新日時;
                m79.税区分名 = m79zkb.税区分;
                try
                {
                    // newﾉｴﾝﾃｨﾃｨﾆ対ｼﾃﾊAcceptChangesﾃﾞ新規追加ﾄﾅﾙ
                    context.M79_ZKB.ApplyChanges(m79);
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
        /// M79_ZKBの更新
        /// </summary>
        /// <param name="m79zkb">M79_ZKB_Member</param>
        public void Update(M79_ZKB_Member m79zkb)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //更新行ｦ特定
                var ret = from x in context.M79_ZKB
                          where (x.税区分ID == m79zkb.出勤区分ID)
						  orderby x.税区分ID
                          select x;
                var m79 = ret.FirstOrDefault();
				m79.税区分ID = m79zkb.出勤区分ID;
                m79.登録日時 = m79zkb.登録日時;
                m79.更新日時 = DateTime.Now;
                m79.税区分名 = m79zkb.税区分;

                m79.AcceptChanges();
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M79_ZKBの物理削除
        /// </summary>
        /// <param name="m79zkb">M79_ZKB_Member</param>
        public void Delete(M79_ZKB_Member M75skk)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M79_ZKB
						  where (x.税区分ID == M75skk.出勤区分ID)
						  orderby x.税区分ID
                          select x;
                var m79 = ret.FirstOrDefault();

                context.DeleteObject(m79);
                context.SaveChanges();
            }
        }

    }
}
