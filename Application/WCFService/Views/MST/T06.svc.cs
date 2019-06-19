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
    public class T06 : IT06 {

        /// <summary>
        /// T06_KYUSのリスト取得
        /// </summary>
        /// <param name="p車輌ID">車輌ID</param>
        /// <param name="p休車開始日付">休車開始日付</param>
        /// <returns>T06_KYUS_MemberのList</returns>
        public List<T06_KYUS_Member> GetList(int p車輌ID, DateTime p休車開始日付)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from t06 in context.T06_KYUS
                           where (t06.車輌ID == p車輌ID && t06.休車開始日付 == p休車開始日付)
						   select new T06_KYUS_Member
						   {
                                車輌ID			= t06.車輌ID,
                                休車開始日付    = t06.休車開始日付,
                                休車終了日付    = t06.休車終了日付,
                                明細区分        = t06.明細区分,
                                車輌番号        = t06.車輌番号,
                                休車事由        = t06.休車事由,
						   }).ToList();
				return ret;
			}
        }

        /// <summary>
        /// T06_KYUSの新規追加
        /// </summary>
        /// <param name="t06kyus">T06_KYUS_Member</param>
        public void Insert(T06_KYUS_Member t06kyus)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                T06_KYUS t06 = new T06_KYUS();
                t06.車輌ID			= t06kyus.車輌ID;
                t06.休車開始日付    = t06kyus.休車開始日付;
                t06.休車終了日付    = t06kyus.休車終了日付;
                t06.明細区分        = t06kyus.明細区分;
                t06.車輌番号        = t06kyus.車輌番号;
                t06.休車事由        = t06kyus.休車事由;
                
                try
                {
                    // newのエンティティに対してはAcceptChangesで新規追加となる
                    context.T06_KYUS.ApplyChanges(t06);
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
        /// T06_KYUSの更新
        /// </summary>
        /// <param name="t06kyus">T06_KYUS_Member</param>
        public void Update(T06_KYUS_Member t06kyus)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                    
                //更新行を特定
                var ret = from x in context.T06_KYUS
                          where x.車輌ID == t06kyus.車輌ID
                            && x.休車開始日付 == t06kyus.休車開始日付
                          orderby x.車輌ID, x.休車開始日付
                          select x;
                var t06 = ret.FirstOrDefault();

                t06.車輌ID = t06kyus.車輌ID;
                t06.休車開始日付 = t06kyus.休車開始日付;
                t06.休車終了日付 = t06kyus.休車終了日付;
                t06.明細区分 = t06kyus.明細区分;
                t06.車輌番号 = t06kyus.車輌番号;
                t06.休車事由 = t06kyus.休車事由;

                t06.AcceptChanges();
                context.SaveChanges();
            }
        }

        /// <summary>
        /// T06_KYUSの物理削除
        /// </summary>
        /// <param name="t06kyus">T06_KYUS_Member</param>
        public void Delete(T06_KYUS_Member t06kyus)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.T06_KYUS
                          where x.車輌ID == t06kyus.車輌ID
                            && x.休車開始日付 == t06kyus.休車開始日付
                          orderby x.車輌ID, x.休車開始日付
                          select x;
                var t06 = ret.FirstOrDefault();

                context.DeleteObject(t06);
                context.SaveChanges();
            }
        }
                
    }
}
