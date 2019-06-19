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
    public class M82 : IM82 {

        /// <summary>
        /// M82_SEIのデータ取得
        /// </summary>
        /// <param name="p請求書区分ID">請求書区分ID</param>
        /// <returns>M82_SEI_Member</returns>
        public M82_SEI_Member GetData(int p請求書区分ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m82 in context.M82_SEI
                           where m82.請求書区分ID == p請求書区分ID
                           orderby m82.請求書区分ID
                           select new M82_SEI_Member
                           {
                               請求書区分ID = m82.請求書区分ID,
                               登録日時 = m82.登録日時,
                               更新日時 = m82.更新日時,
                               請求書名 = m82.請求書名,
                           });
                return ret.FirstOrDefault();
            }
        }

        /// <summary>
        /// M82_SEIの新規追加
        /// </summary>
        /// <param name="m82sei">M82_SEI_Member</param>
        public void Insert(M82_SEI_Member m82sei)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                M82_SEI m82 = new M82_SEI();
                m82.請求書区分ID = m82sei.請求書区分ID;
                m82.登録日時 = m82sei.登録日時;
                m82.更新日時 = m82sei.更新日時;
                m82.請求書名 = m82sei.請求書名;
                try
                {
                    // newﾉｴﾝﾃｨﾃｨﾆ対ｼﾃﾊAcceptChangesﾃﾞ新規追加ﾄﾅﾙ
                    context.M82_SEI.ApplyChanges(m82);
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
        /// M82_SEIの更新
        /// </summary>
        /// <param name="m82sei">M82_SEI_Member</param>
        public void Update(M82_SEI_Member m82sei)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //更新行ｦ特定
                var ret = from x in context.M82_SEI
                          where (x.請求書区分ID == m82sei.請求書区分ID)
                          orderby x.請求書区分ID
                          select x;
                var m82 = ret.FirstOrDefault();
                m82.請求書区分ID = m82sei.請求書区分ID;
                m82.登録日時 = m82sei.登録日時;
                m82.更新日時 = DateTime.Now;
                m82.請求書名 = m82sei.請求書名;

                m82.AcceptChanges();
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M82_SEIの物理削除
        /// </summary>
        /// <param name="m82sei">M82_SEI_Member</param>
        public void Delete(M82_SEI_Member m82sei)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M82_SEI
                          where (x.請求書区分ID == m82sei.請求書区分ID)
                          orderby x.請求書区分ID
                          select x;
                var m82 = ret.FirstOrDefault();

                context.DeleteObject(m82);
                context.SaveChanges();
            }
        }

    }
}
