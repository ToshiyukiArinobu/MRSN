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
    public class T03 : IT03 {

        /// <summary>
        /// T03_KTRNのリスト取得
        /// </summary>
        /// <param name="p明細番号">明細番号</param>
        /// <param name="p明細行">明細行</param>
        /// <returns>T03_KTRN_MemberのList</returns>
        public List<T03_KTRN_Member> GetList(int p明細番号, int? p明細行)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

				var ret = (from t03 in context.T03_KTRN
						   where t03.明細番号 == p明細番号
						   select new T03_KTRN_Member
						   {
							   //明細番号 = t03.明細番号,
							   //明細行 = t03.明細行,
							   //登録日時 = t03.登録日時,
							   //更新日時 = t03.更新日時,
							   //明細区分 = t03.明細区分,
							   //入力区分 = t03.入力区分,
							   //経費発生日 = t03.経費発生日,
							   //車輌ID = t03.車輌ID,
							   //車輌番号 = t03.車輌番号,
							   //メーター = t03.メーター,
							   //乗務員ID = t03.乗務員ID,
							   //支払先ID = t03.支払先ID,
							   //自社部門ID = t03.自社部門ID,
							   //経費項目ID = t03.経費項目ID,
							   //経費補助名称 = t03.経費補助名称,
							   //単価 = t03.単価,
							   //内軽油税分 = t03.内軽油税分,
							   //数量 = t03.数量,
							   //金額 = t03.金額,
							   //収支区分 = t03.収支区分,
							   //摘要ID = t03.摘要ID,
							   //摘要名 = t03.摘要名,
							   //入力者ID = t03.入力者ID,

						   });
				if (p明細行 == null)
				{
					return ret.ToList();
				}
				else
				{
					return ret.Where(x => x.明細行 == p明細行).ToList();
				}
			}
        }

        /// <summary>
        /// T03_KTRNの新規追加
        /// </summary>
        /// <param name="t03ktrn">T03_KTRN_Member</param>
        public void Insert(T03_KTRN_Member t03ktrn)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                T03_KTRN t03 = new T03_KTRN();
                t03.明細番号 = t03ktrn.明細番号;
                t03.明細行 = t03ktrn.明細行;
                t03.登録日時 = DateTime.Now;
                t03.更新日時 = DateTime.Now;
                t03.明細区分 = t03ktrn.明細区分;
                t03.入力区分 = t03ktrn.入力区分;
                t03.経費発生日 = t03ktrn.経費発生日;
                t03.車輌ID = t03ktrn.車輌ID;
                t03.車輌番号 = t03ktrn.車輌番号;
                t03.乗務員KEY = t03ktrn.乗務員ID;
                t03.支払先KEY = t03ktrn.支払先ID;
                t03.自社部門ID = t03ktrn.自社部門ID;
                t03.経費項目ID = t03ktrn.経費項目ID;
                t03.経費補助名称 = t03ktrn.経費補助名称;
                t03.単価 = t03ktrn.単価;
                t03.内軽油税分 = t03ktrn.内軽油税分;
                t03.数量 = t03ktrn.数量;
                t03.金額 = t03ktrn.金額;
                t03.収支区分 = t03ktrn.収支区分;
                t03.摘要ID = t03ktrn.摘要ID;
                t03.摘要名 = t03ktrn.摘要名;
                
                //未更新フィールド
                //入力者ID = t03ktrn.入力者ID;

                try
                {
                    // newのエンティティに対してはAcceptChangesで新規追加となる
                    context.T03_KTRN.ApplyChanges(t03);
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
        /// T03_KTRNの更新
        /// </summary>
        /// <param name="t03ktrn">T03_KTRN_Member</param>
        public void Update(T03_KTRN_Member t03ktrn)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                    
                //更新行を特定
                var ret = from x in context.T03_KTRN
                          where x.明細番号 == t03ktrn.明細番号
                            && x.明細行 == t03ktrn.明細行
                          orderby x.明細番号, x.明細行
                          select x;
                var t03 = ret.FirstOrDefault();

                t03.更新日時 = DateTime.Now;
                t03.明細区分 = t03ktrn.明細区分;
                t03.入力区分 = t03ktrn.入力区分;
                t03.経費発生日 = t03ktrn.経費発生日;
                t03.車輌ID = t03ktrn.車輌ID;
                t03.車輌番号 = t03ktrn.車輌番号;
                t03.乗務員KEY = t03ktrn.乗務員ID;
                t03.支払先KEY = t03ktrn.支払先ID;
                t03.自社部門ID = t03ktrn.自社部門ID;
                t03.経費項目ID = t03ktrn.経費項目ID;
                t03.経費補助名称 = t03ktrn.経費補助名称;
                t03.単価 = t03ktrn.単価;
                t03.内軽油税分 = t03ktrn.内軽油税分;
                t03.数量 = t03ktrn.数量;
                t03.金額 = t03ktrn.金額;
                t03.収支区分 = t03ktrn.収支区分;
                t03.摘要ID = t03ktrn.摘要ID;
                t03.摘要名 = t03ktrn.摘要名;

                //未更新フィールド
                //入力者ID = t03ktrn.入力者ID;

                t03.AcceptChanges();
                context.SaveChanges();
            }
        }

        /// <summary>
        /// T03_KTRNの物理削除
        /// </summary>
        /// <param name="t03ktrn">T03_KTRN_Member</param>
        public void Delete(T03_KTRN_Member t03ktrn)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.T03_KTRN
                          where x.明細番号 == t03ktrn.明細番号
                            && x.明細行 == t03ktrn.明細行
                          orderby x.明細番号, x.明細行
                          select x;
                var t03 = ret.FirstOrDefault();

                context.DeleteObject(t03);
                context.SaveChanges();
            }
        }

    }
}
