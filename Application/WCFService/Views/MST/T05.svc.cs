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
    public class T05 : IT05 {

        /// <summary>
        /// T05_TTRNのリスト取得
        /// </summary>
        /// <param name="p乗務員ID">乗務員ID</param>
        /// <param name="p配車日付">配車日付</param>
        /// <returns>T05_TTRN_MemberのList</returns>
        public List<T05_TTRN_Member> GetList(int p乗務員ID, DateTime p配車日付)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from t05 in context.T05_TTRN
                           where (t05.乗務員KEY == p乗務員ID && t05.配車日付 == p配車日付)
						   select new T05_TTRN_Member
						   {
                               乗務員ID         = t05.乗務員KEY,
                                配車日付        = t05.配車日付,
                                登録日時        = t05.登録日時,
                                更新日時        = t05.更新日時,
                                明細区分        = t05.明細区分,
                                車輌ID          = t05.車輌ID,
                                車輌番号        = t05.車輌番号,
                                行先            = t05.行先,
                                指示項目        = t05.指示項目,
                                自社部門ID      = t05.自社部門ID,
						   }).ToList();
				return ret;
			}
        }

        /// <summary>
        /// T05_TTRNの新規追加
        /// </summary>
        /// <param name="t05ttrn">T05_TTRN_Member</param>
        public void Insert(T05_TTRN_Member t05ttrn)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                T05_TTRN t05 = new T05_TTRN();
                t05.乗務員KEY       = t05ttrn.乗務員ID;
                t05.配車日付        = t05ttrn.配車日付;
                t05.登録日時        = t05ttrn.登録日時;
                t05.更新日時        = t05ttrn.更新日時;
                t05.明細区分        = t05ttrn.明細区分;
                t05.車輌ID          = t05ttrn.車輌ID;
                t05.車輌番号        = t05ttrn.車輌番号;
                t05.行先            = t05ttrn.行先;
                t05.指示項目        = t05ttrn.指示項目;
                t05.自社部門ID      = t05ttrn.自社部門ID;
                
                try
                {
                    // newのエンティティに対してはAcceptChangesで新規追加となる
                    context.T05_TTRN.ApplyChanges(t05);
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
        /// T05_TTRNの更新
        /// </summary>
        /// <param name="t05ttrn">T05_TTRN_Member</param>
        public void Update(T05_TTRN_Member t05ttrn)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                    
                //更新行を特定
                var ret = from x in context.T05_TTRN
                          where x.乗務員KEY == t05ttrn.乗務員ID
                            && x.配車日付 == t05ttrn.配車日付
                          orderby x.乗務員KEY, x.配車日付
                          select x;
                var t05 = ret.FirstOrDefault();

                t05.更新日時 = DateTime.Now;
                t05.乗務員KEY = t05ttrn.乗務員ID;
                t05.配車日付 = t05ttrn.配車日付;
                t05.登録日時 = t05ttrn.登録日時;
                t05.明細区分 = t05ttrn.明細区分;
                t05.車輌ID = t05ttrn.車輌ID;
                t05.車輌番号 = t05ttrn.車輌番号;
                t05.行先 = t05ttrn.行先;
                t05.指示項目 = t05ttrn.指示項目;
                t05.自社部門ID = t05ttrn.自社部門ID;

                t05.AcceptChanges();
                context.SaveChanges();
            }
        }

        /// <summary>
        /// T05_TTRNの物理削除
        /// </summary>
        /// <param name="t05ttrn">T05_TTRN_Member</param>
        public void Delete(T05_TTRN_Member t05ttrn)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.T05_TTRN
                          where x.乗務員KEY == t05ttrn.乗務員ID
                            && x.配車日付 == t05ttrn.配車日付
                          orderby x.乗務員KEY, x.配車日付
                          select x;
                var t05 = ret.FirstOrDefault();

                context.DeleteObject(t05);
                context.SaveChanges();
            }
        }
                
    }
}
