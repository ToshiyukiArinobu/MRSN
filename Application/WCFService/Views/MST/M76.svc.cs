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
    public class M76 : IM76 {

        /// <summary>
        /// M76_DBUのデータ取得
        /// </summary>
        /// <param name="p歩合計算区分ID">歩合計算区分ID</param>
        /// <returns>M76_DBU_Member</returns>
        public M76_DBU_Member GetData(int p歩合計算区分ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m76 in context.M76_DBU
                           where m76.歩合計算区分ID == p歩合計算区分ID
                           orderby m76.歩合計算区分ID
                           select new M76_DBU_Member
                           {
                               歩合計算区分ID = m76.歩合計算区分ID,
                               登録日時 = m76.登録日時,
                               更新日時 = m76.更新日時,
                               歩合計算名 = m76.歩合計算名,
                           });
                return ret.FirstOrDefault();
            }
        }

        /// <summary>
        /// M76_DBUの新規追加
        /// </summary>
        /// <param name="m76dbu">M76_DBU_Member</param>
        public void Insert(M76_DBU_Member m76dbu)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                M76_DBU m76 = new M76_DBU();
                m76.歩合計算区分ID = m76dbu.歩合計算区分ID;
                m76.登録日時 = m76dbu.登録日時;
                m76.更新日時 = m76dbu.更新日時;
                m76.歩合計算名 = m76dbu.歩合計算名;
                try
                {
                    // newﾉｴﾝﾃｨﾃｨﾆ対ｼﾃﾊAcceptChangesﾃﾞ新規追加ﾄﾅﾙ
                    context.M76_DBU.ApplyChanges(m76);
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
        /// M76_DBUの更新
        /// </summary>
        /// <param name="m76dbu">M76_DBU_Member</param>
        public void Update(M76_DBU_Member m76dbu)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //更新行ｦ特定
                var ret = from x in context.M76_DBU
                          where (x.歩合計算区分ID == m76dbu.歩合計算区分ID)
                          orderby x.歩合計算区分ID
                          select x;
                var m76 = ret.FirstOrDefault();
                m76.歩合計算区分ID = m76dbu.歩合計算区分ID;
                m76.登録日時 = m76dbu.登録日時;
                m76.更新日時 = DateTime.Now;
                m76.歩合計算名 = m76dbu.歩合計算名;

                m76.AcceptChanges();
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M76_DBUの物理削除
        /// </summary>
        /// <param name="m76dbu">M76_DBU_Member</param>
        public void Delete(M76_DBU_Member M75skk)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M76_DBU
                          where (x.歩合計算区分ID == M75skk.歩合計算区分ID)
                          orderby x.歩合計算区分ID
                          select x;
                var m76 = ret.FirstOrDefault();

                context.DeleteObject(m76);
                context.SaveChanges();
            }
        }

    }
}
