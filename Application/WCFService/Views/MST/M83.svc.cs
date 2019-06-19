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
    public class M83 : IM83 {

        /// <summary>
        /// M83_UKEのデータ取得
        /// </summary>
        /// <param name="p運賃計算区分ID">運賃計算区分ID</param>
        /// <returns>M83_UKE_Member</returns>
        public M83_UKE_Member GetData(int p運賃計算区分ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m83 in context.M83_UKE
                           where m83.運賃計算区分ID == p運賃計算区分ID
                           orderby m83.運賃計算区分ID
                           select new M83_UKE_Member
                           {
                               運賃計算区分ID = m83.運賃計算区分ID,
                               登録日時 = m83.登録日時,
                               更新日時 = m83.更新日時,
                               運賃計算区分 = m83.運賃計算区分,
                           });
                return ret.FirstOrDefault();
            }
        }

        /// <summary>
        /// M83_UKEの新規追加
        /// </summary>
        /// <param name="m83uke">M83_UKE_Member</param>
        public void Insert(M83_UKE_Member m83uke)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                M83_UKE m83 = new M83_UKE();
                m83.運賃計算区分ID = m83uke.運賃計算区分ID;
                m83.登録日時 = m83uke.登録日時;
                m83.更新日時 = m83uke.更新日時;
                m83.運賃計算区分 = m83uke.運賃計算区分;
                try
                {
                    // newﾉｴﾝﾃｨﾃｨﾆ対ｼﾃﾊAcceptChangesﾃﾞ新規追加ﾄﾅﾙ
                    context.M83_UKE.ApplyChanges(m83);
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
        /// M83_UKEの更新
        /// </summary>
        /// <param name="m83uke">M83_UKE_Member</param>
        public void Update(M83_UKE_Member m83uke)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //更新行ｦ特定
                var ret = from x in context.M83_UKE
                          where (x.運賃計算区分ID == m83uke.運賃計算区分ID)
                          orderby x.運賃計算区分ID
                          select x;
                var m83 = ret.FirstOrDefault();
                m83.運賃計算区分ID = m83uke.運賃計算区分ID;
                m83.登録日時 = m83uke.登録日時;
                m83.更新日時 = DateTime.Now;
                m83.運賃計算区分 = m83uke.運賃計算区分;

                m83.AcceptChanges();
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M83_UKEの物理削除
        /// </summary>
        /// <param name="m83uke">M83_UKE_Member</param>
        public void Delete(M83_UKE_Member m83uke)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M83_UKE
                          where (x.運賃計算区分ID == m83uke.運賃計算区分ID)
                          orderby x.運賃計算区分ID
                          select x;
                var m83 = ret.FirstOrDefault();

                context.DeleteObject(m83);
                context.SaveChanges();
            }
        }

    }
}
