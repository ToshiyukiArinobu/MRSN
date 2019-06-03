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
    public class M75 : IM75 {

        /// <summary>
        /// M75_SKKのデータ取得
        /// </summary>
        /// <param name="p表示順ID">表示順ID</param>
        /// <returns>M75_SKK_Member</returns>
        public M75_SKK_Member GetData(int p表示順ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m75 in context.M75_SKK
                           where m75.表示順ID == p表示順ID
                           orderby m75.表示順ID
                           select new M75_SKK_Member
                           {
                               表示順ID = m75.表示順ID,
                               登録日時 = m75.登録日時,
                               更新日時 = m75.更新日時,
                               経費項目ID = m75.経費項目ID,
                               支払先ID = m75.支払先ID,
                           });
                return ret.FirstOrDefault();
            }
        }

        /// <summary>
        /// M75_SKKの新規追加
        /// </summary>
        /// <param name="M75skk">M75_SKK_Member</param>
        public void Insert(M75_SKK_Member m75skk)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                M75_SKK m75 = new M75_SKK();
                m75.表示順ID = m75skk.表示順ID;
                m75.登録日時 = m75skk.登録日時;
                m75.更新日時 = m75skk.更新日時;
                m75.経費項目ID = m75skk.経費項目ID;
                m75.支払先ID = m75skk.支払先ID;
                try
                {
                    // newﾉｴﾝﾃｨﾃｨﾆ対ｼﾃﾊAcceptChangesﾃﾞ新規追加ﾄﾅﾙ
                    context.M75_SKK.ApplyChanges(m75);
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
        /// M75_SKKの更新
        /// </summary>
        /// <param name="M75skk">M75_SKK_Member</param>
        public void Update(M75_SKK_Member m75skk)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //更新行ｦ特定
                var ret = from x in context.M75_SKK
                          where (x.表示順ID == m75skk.表示順ID)
                          orderby x.表示順ID
                          select x;
                var m75 = ret.FirstOrDefault();
                m75.表示順ID = m75skk.表示順ID;
                m75.登録日時 = m75skk.登録日時;
                m75.更新日時 = DateTime.Now;
                m75.経費項目ID = m75skk.経費項目ID;
                m75.支払先ID = m75skk.支払先ID;

                m75.AcceptChanges();
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M75_SKKの物理削除
        /// </summary>
        /// <param name="M75skk">M75_SKK_Member</param>
        public void Delete(M75_SKK_Member M75skk)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M75_SKK
                          where (x.表示順ID == M75skk.表示順ID)
                          orderby x.表示順ID
                          select x;
                var m75 = ret.FirstOrDefault();

                context.DeleteObject(m75);
                context.SaveChanges();
            }
        }

    }
}
