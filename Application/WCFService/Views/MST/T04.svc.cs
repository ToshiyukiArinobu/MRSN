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
    public class T04 : IT04 {

        /// <summary>
        /// T04_NYUKのリスト取得
        /// </summary>
        /// <param name="p明細番号">明細番号</param>
        /// <param name="p明細行">明細行</param>
        /// <returns>T04_NYUK_MemberのList</returns>
        public List<T04_NYUK_Member> GetList(int p明細番号, int p明細行)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from t04 in context.T04_NYUK
                           where (t04.明細番号 == p明細番号 && t04.明細行 == p明細行)
						   select new T04_NYUK_Member
						   {
                                明細番号			= t04.明細番号,
                                明細行              = t04.明細行,
                                登録日時            = t04.登録日時,
                                更新日時            = t04.更新日時,
                                明細区分            = t04.明細区分,
                                入出金日付          = t04.入出金日付,
                                取引先ID            = t04.取引先KEY,
                                入出金区分          = t04.入出金区分,
                                入出金金額          = t04.入出金金額,
                                摘要ID              = t04.摘要ID,
                                摘要名              = t04.摘要名,
                                手形日付            = t04.手形日付,
                                入力者ID            = t04.入力者ID,
						   }).ToList();
				return ret;
			}
        }

        /// <summary>
        /// T04_NYUKの新規追加
        /// </summary>
        /// <param name="t04nyuk">T04_NYUK_Member</param>
        public void Insert(T04_NYUK_Member t04nyuk)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                T04_NYUK t04 = new T04_NYUK();
                t04.明細番号			= t04nyuk.明細番号;
                t04.明細行              = t04nyuk.明細行;
                t04.登録日時            = t04nyuk.登録日時;
                t04.更新日時            = t04nyuk.更新日時;
                t04.明細区分            = t04nyuk.明細区分;
                t04.入出金日付          = t04nyuk.入出金日付;
                t04.取引先KEY           = t04nyuk.取引先ID;
                t04.入出金区分          = t04nyuk.入出金区分;
                t04.入出金金額          = t04nyuk.入出金金額;
                t04.摘要ID              = t04nyuk.摘要ID;
                t04.摘要名              = t04nyuk.摘要名;
                t04.手形日付            = t04nyuk.手形日付;
                t04.入力者ID            = t04nyuk.入力者ID;

                try
                {
                    // newのエンティティに対してはAcceptChangesで新規追加となる
                    context.T04_NYUK.ApplyChanges(t04);
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
        /// T04_NYUKの更新
        /// </summary>
        /// <param name="t04nyuk">T04_NYUK_Member</param>
        public void Update(T04_NYUK_Member t04nyuk)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                    
                //更新行を特定
                var ret = from x in context.T04_NYUK
                          where x.明細番号 == t04nyuk.明細番号
                            && x.明細行 == t04nyuk.明細行
                          orderby x.明細番号, x.明細行
                          select x;
                var t04 = ret.FirstOrDefault();

                t04.更新日時 = DateTime.Now;
                t04.明細番号 = t04nyuk.明細番号;
                t04.明細行 = t04nyuk.明細行;
                t04.登録日時 = t04nyuk.登録日時;
                t04.明細区分 = t04nyuk.明細区分;
                t04.入出金日付 = t04nyuk.入出金日付;
                t04.取引先KEY = t04nyuk.取引先ID;
                t04.入出金区分 = t04nyuk.入出金区分;
                t04.入出金金額 = t04nyuk.入出金金額;
                t04.摘要ID = t04nyuk.摘要ID;
                t04.摘要名 = t04nyuk.摘要名;
                t04.手形日付 = t04nyuk.手形日付;
                t04.入力者ID = t04nyuk.入力者ID;

                t04.AcceptChanges();
                context.SaveChanges();
            }
        }

        /// <summary>
        /// T04_NYUKの物理削除
        /// </summary>
        /// <param name="t04nyuk">T04_NYUK_Member</param>
        public void Delete(T04_NYUK_Member t04nyuk)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.T04_NYUK
                          where x.明細番号 == t04nyuk.明細番号
                            && x.明細行 == t04nyuk.明細行
                          orderby x.明細番号, x.明細行
                          select x;
                var t04 = ret.FirstOrDefault();

                context.DeleteObject(t04);
                context.SaveChanges();
            }
        }
                
    }
}
