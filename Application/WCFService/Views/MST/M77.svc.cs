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
    public class M77 : IM77 {

        /// <summary>
        /// M77_TRHのデータ取得
        /// </summary>
        /// <param name="p取引区分ID">取引区分ID</param>
        /// <returns>M77_TRH_Member</returns>
        public M77_TRH_Member GetData(int p取引区分ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m77 in context.M77_TRH
                           where m77.取引区分ID == p取引区分ID
                           orderby m77.取引区分ID
                           select new M77_TRH_Member
                           {
                               取引区分ID = m77.取引区分ID,
                               登録日時 = m77.登録日時,
                               更新日時 = m77.更新日時,
                               歩合計算名 = m77.取引区分名,
                           });
                return ret.FirstOrDefault();
            }
        }

        /// <summary>
        /// M77_TRHの新規追加
        /// </summary>
        /// <param name="m77trh">M77_TRH_Member</param>
        public void Insert(M77_TRH_Member m77trh)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                M77_TRH m77 = new M77_TRH();
                m77.取引区分ID = m77trh.取引区分ID;
                m77.登録日時 = m77trh.登録日時;
                m77.更新日時 = m77trh.更新日時;
				m77.取引区分名 = m77trh.歩合計算名;
                try
                {
                    // newﾉｴﾝﾃｨﾃｨﾆ対ｼﾃﾊAcceptChangesﾃﾞ新規追加ﾄﾅﾙ
                    context.M77_TRH.ApplyChanges(m77);
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
        /// M77_TRHの更新
        /// </summary>
        /// <param name="m77trh">M77_TRH_Member</param>
        public void Update(M77_TRH_Member m77trh)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //更新行ｦ特定
                var ret = from x in context.M77_TRH
                          where (x.取引区分ID == m77trh.取引区分ID)
                          orderby x.取引区分ID
                          select x;
                var m77 = ret.FirstOrDefault();
                m77.取引区分ID = m77trh.取引区分ID;
                m77.登録日時 = m77trh.登録日時;
                m77.更新日時 = DateTime.Now;
				m77.取引区分名 = m77trh.歩合計算名;

                m77.AcceptChanges();
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M77_TRHの物理削除
        /// </summary>
        /// <param name="m77trh">M77_TRH_Member</param>
        public void Delete(M77_TRH_Member M75skk)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M77_TRH
                          where (x.取引区分ID == M75skk.取引区分ID)
                          orderby x.取引区分ID
                          select x;
                var m77 = ret.FirstOrDefault();

                context.DeleteObject(m77);
                context.SaveChanges();
            }
        }

    }
}
