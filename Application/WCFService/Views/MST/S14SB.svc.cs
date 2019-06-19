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
    public class S14SB : IS14SB {

        /// <summary>
        /// S14_CARSBのデータ取得 変動データ取得
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p発地ID">発地ID</param>
        /// <param name="p着地ID">着地ID</param>
        /// <param name="p商品ID">商品ID</param>
        /// <returns>S14_CARSB_Member</returns>
        public List<S14_CARSB_Member> GetData_Hendo(int p車輌ID, int p集計年月)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                
                var ret = (from s14SB in context.S14_CARSB
                           from m05 in context.M05_CAR.Where(m05 => m05.車輌KEY == s14SB.車輌KEY)
                           join m07 in context.M07_KEI.Where(c => c.削除日付 == null) on s14SB.経費項目ID equals m07.経費項目ID into m07Group
                           from m07g in m07Group.DefaultIfEmpty()
                           orderby m07g.経費区分, s14SB.経費項目ID
                           where (s14SB.車輌KEY == (from drv in context.M05_CAR where drv.車輌ID == p車輌ID select drv.車輌KEY).FirstOrDefault()
                           && s14SB.集計年月 == p集計年月 && m07g.経費区分 != 3 && m07g.固定変動区分 == 1)

                           select new S14_CARSB_Member
                           {
                               車輌KEY = m05.車輌KEY,
                               集計年月 = s14SB.集計年月,
                               経費項目ID = s14SB.経費項目ID,
                               登録日時 = s14SB.登録日時,
                               更新日時 = s14SB.更新日時,
                               経費項目名 = s14SB.経費項目名,
                               固定変動区分 = s14SB.固定変動区分,
                               金額 = s14SB.金額,
                               経費区分 = m07g.経費区分,

                           }).AsQueryable(); ;
                return ret.ToList();
            }
        }

        /// <summary>
        /// S14_CARSBのデータ取得 人件費データ取得
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p発地ID">発地ID</param>
        /// <param name="p着地ID">着地ID</param>
        /// <param name="p商品ID">商品ID</param>
        /// <returns>S14_CARSB_Member</returns>
        public List<S14_CARSB_Member> GetData_Jinken(int p車輌ID, int p集計年月)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from s14SB in context.S14_CARSB
                           from m05 in context.M05_CAR.Where(m05 => m05.車輌KEY == s14SB.車輌KEY)
                           join m07 in context.M07_KEI.Where(c => c.削除日付 == null) on s14SB.経費項目ID equals m07.経費項目ID into m07Group
                           from m07g in m07Group.DefaultIfEmpty()
                           orderby m07g.経費区分, s14SB.経費項目ID
                           where (s14SB.車輌KEY == (from drv in context.M05_CAR where drv.車輌ID == p車輌ID select drv.車輌KEY).FirstOrDefault()
                           && s14SB.集計年月 == p集計年月 && m07g.経費区分 == 3)

                           select new S14_CARSB_Member
                           {
                               車輌KEY = m05.車輌KEY,
                               集計年月 = s14SB.集計年月,
                               経費項目ID = s14SB.経費項目ID,
                               登録日時 = s14SB.登録日時,
                               更新日時 = s14SB.更新日時,
                               経費項目名 = s14SB.経費項目名,
                               固定変動区分 = s14SB.固定変動区分,
                               金額 = s14SB.金額,
                               経費区分 = m07g.経費区分,

                           }).AsQueryable(); ;
                return ret.ToList();
            }
        }

        /// <summary>
        /// S14_CARSBのデータ取得 固定項目取得
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p発地ID">発地ID</param>
        /// <param name="p着地ID">着地ID</param>
        /// <param name="p商品ID">商品ID</param>
        /// <returns>S14_CARSB_Member</returns>
        public List<S14_CARSB_Member> GetData_Kotei(int p車輌ID, int p集計年月)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from s14SB in context.S14_CARSB
                           from m05 in context.M05_CAR.Where(m05 => m05.車輌KEY == s14SB.車輌KEY)
                           join m07 in context.M07_KEI.Where(c => c.削除日付 == null) on s14SB.経費項目ID equals m07.経費項目ID into m07Group
                           from m07g in m07Group.DefaultIfEmpty()
                           orderby m07g.経費区分, s14SB.経費項目ID
                           where (s14SB.車輌KEY == (from drv in context.M05_CAR where drv.車輌ID == p車輌ID select drv.車輌KEY).FirstOrDefault()
                           && s14SB.集計年月 == p集計年月 && m07g.経費区分 != 3 && m07g.固定変動区分 == 0)

                           select new S14_CARSB_Member
                           {
                               車輌KEY = m05.車輌KEY,
                               集計年月 = s14SB.集計年月,
                               経費項目ID = s14SB.経費項目ID,
                               登録日時 = s14SB.登録日時,
                               更新日時 = s14SB.更新日時,
                               経費項目名 = s14SB.経費項目名,
                               固定変動区分 = s14SB.固定変動区分,
                               金額 = s14SB.金額,
                               経費区分 = m07g.経費区分,

                           }).AsQueryable(); ;
                return ret.ToList();
            }
        }


        /// <summary>
        /// S14_CARSBの新規追加
        /// </summary>
        /// <param name="s14SBdrvs">S14_CARSB_Member</param>
        public void Insert(S14_CARSB_Member s14SBdrvs)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                S14_CARSB s14SB = new S14_CARSB();

                s14SB.車輌KEY = s14SBdrvs.車輌KEY;
                s14SB.集計年月 = s14SBdrvs.集計年月;
                s14SB.経費項目ID = s14SBdrvs.経費項目ID;
                s14SB.登録日時 = s14SBdrvs.登録日時;
                s14SB.更新日時 = s14SBdrvs.更新日時;
                s14SB.経費項目名 = s14SBdrvs.経費項目名;
                s14SB.固定変動区分 = s14SBdrvs.固定変動区分;
                s14SB.金額 = s14SBdrvs.金額;

                try
                {
                    // newのエンティティに対してはAcceptChangesで新規追加となる
                    context.S14_CARSB.ApplyChanges(s14SB);
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
        /// S14_CARSBの更新 変動項目更新
        /// </summary>
        /// <param name="s14SBdrvs">S14_CARSB_Member</param>
        public void Update_Hendo(S14_CAR_Member s14drv, List<S14_CARSB_Member> s14SBHen, List<S14_CARSB_Member> s14SBJin, List<S14_CARSB_Member> s14SBKotei)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // トランザクションのインスタンス化(開始)
                using (var tran = new TransactionScope())
                {
                    context.Connection.Open();

                    var ret = (from x in context.S14_CARSB
                               where x.車輌KEY == s14drv.車輌KEY && x.集計年月 == s14drv.集計年月
                               select x).ToList();
                    foreach (var rec1 in ret)
                    {
                        context.DeleteObject(rec1);
                    }

                    foreach (S14_CARSB_Member ddt in s14SBHen)
                    {
                        var dat = new S14_CARSB()
                        {
                            車輌KEY = ddt.車輌KEY,
                            集計年月 = ddt.集計年月,
                            経費項目ID = ddt.経費項目ID,
                            登録日時 = ddt.登録日時,
                            更新日時 = DateTime.Now,
                            経費項目名 = ddt.経費項目名,
                            固定変動区分 = ddt.固定変動区分,
                            金額 = ddt.金額,
                        };
                        context.S14_CARSB.ApplyChanges(dat);
                    }

                    foreach (S14_CARSB_Member ddt in s14SBJin)
                    {
                        var dat = new S14_CARSB()
                        {
                            車輌KEY = ddt.車輌KEY,
                            集計年月 = ddt.集計年月,
                            経費項目ID = ddt.経費項目ID,
                            登録日時 = ddt.登録日時,
                            更新日時 = DateTime.Now,
                            経費項目名 = ddt.経費項目名,
                            固定変動区分 = ddt.固定変動区分,
                            金額 = ddt.金額,
                        };
                        context.S14_CARSB.ApplyChanges(dat);
                    }

                    foreach (S14_CARSB_Member ddt in s14SBKotei)
                    {
                        var dat = new S14_CARSB()
                        {
                            車輌KEY = ddt.車輌KEY,
                            集計年月 = ddt.集計年月,
                            経費項目ID = ddt.経費項目ID,
                            登録日時 = ddt.登録日時,
                            更新日時 = DateTime.Now,
                            経費項目名 = ddt.経費項目名,
                            固定変動区分 = ddt.固定変動区分,
                            金額 = ddt.金額,
                        };
                        context.S14_CARSB.ApplyChanges(dat);
                    }

                    context.SaveChanges();
                    tran.Complete();
                }
            }
        }

		/// <summary>
		/// S14_CARSBの物理削除
		/// </summary>
		/// <param name="s14SBdrvs">S14_CARSB_Member</param>
		public void Delete_Hendo(int? p車輌ID, int? p集計年月)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

				try
				{
					//削除行を特定
					var ret = from x in context.S14_CARSB
							  where (x.車輌KEY == (from drv in context.M05_CAR where drv.車輌ID == p車輌ID select drv.車輌KEY).FirstOrDefault()
									 && x.集計年月 == p集計年月)
							  orderby x.車輌KEY, x.集計年月
							  select x;
					foreach (var row in ret)
					{
						context.DeleteObject(row);
					}
					context.SaveChanges();
				}
				catch (Exception e)
				{ 

				}

			}
		}


		/// <summary>
		/// S14_CARSBの物理削除
		/// </summary>
		/// <param name="s14SBdrvs">S14_CARSB_Member</param>
		public void Delete(int? p車輌ID, int? p集計年月)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

				//削除行を特定
				var ret = from x in context.S14_CARSB
						  where (x.車輌KEY == (from drv in context.M05_CAR where drv.車輌ID == p車輌ID select drv.車輌KEY).FirstOrDefault()
								 && x.集計年月 == p集計年月)
						  orderby x.車輌KEY, x.集計年月
						  select x;
				var s14SB = ret.FirstOrDefault();

				context.DeleteObject(s14SB);
				context.SaveChanges();
			}
		}
        
        /// <summary>
        /// 得意先別車種別単価一覧表プレビュー用出力
        /// 得意先別車種別単価一覧表CSV用出力
        /// </summary>
        /// <returns></returns>
        /// <param name="s14SBdrvs">S14SB_drvs__Member</param>
        public List<S14_CARSB_Member_Preview_csv> GetSearchListData(string p車輌IDFrom, string p車輌IDTo, string p処理年月From, string p処理年月To, int[] i乗務員List)
        {
            //using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            //{
            //    context.Connection.Open();


            //    var query = (from s14SB in context.S14_CARSB
            //               from m04 in context.M05_CAR.Where(m04 => m04.車輌KEY == s14SB.車輌KEY)
            //               join m07 in context.M07_KEI on s14SB.経費項目ID equals m07.経費項目ID into m07Group
            //               where (s14SB.車輌KEY == (from drv in context.M05_CAR where drv.車輌ID == p車輌ID select drv.車輌KEY).FirstOrDefault()
            //               && s14SB.集計年月 == p集計年月)
            //               select new S14_CARSB_Member_Preview_csv
            //               {
            //                   車輌KEY = m04.車輌KEY,
            //                   集計年月 = s14SB.集計年月,
            //                   経費項目ID = s14SB.経費項目ID,
            //                   登録日時 = s14SB.登録日時,
            //                   更新日時 = s14SB.更新日時,
            //                   経費項目名 = s14SB.経費項目名,
            //                   固定変動区分 = s14SB.固定変動区分,
            //                   金額 = s14SB.金額,
            //                   経費区分 = m07Group.Min(m07g => m07g.経費区分)

            //               }).AsQueryable(); ;
                
                


            //    if (!string.IsNullOrEmpty(p車輌IDFrom))
            //    {
            //        int ip車輌IDFrom = AppCommon.IntParse(p車輌IDFrom);
            //        query = query.Where(c => c.車輌KEY >= ip車輌IDFrom);
            //    }
            //    if (!string.IsNullOrEmpty(p車輌IDTo))
            //    {
            //        int ip車輌IDTo = AppCommon.IntParse(p車輌IDTo);
            //        query = query.Where(c => c.車輌KEY <= ip車輌IDTo);
            //    }

            //    if (!string.IsNullOrEmpty(p処理年月From))
            //    {
            //        int ip処理年月From = AppCommon.IntParse(p処理年月From);
            //        query = query.Where(c => c.集計年月 >= ip処理年月From);
            //    }
            //    if (!string.IsNullOrEmpty(p処理年月To))
            //    {
            //        int ip処理年月To = AppCommon.IntParse(p処理年月To);
            //        query = query.Where(c => c.集計年月 <= ip処理年月To);
            //    }

            //    if (i乗務員List.Length > 0)
            //    {
            //        var intCause = i乗務員List;

            //        query = query.Union(from s14SB in context.S14_CARSB
            //                            from m04 in context.M05_CAR.Where(m04 => m04.車輌KEY == s14SB.車輌KEY)
            //                            where intCause.Contains(m04.車輌KEY)
            //                            select new S14_CARSB_Member_Preview_csv
            //                        {

            //                            車輌KEY = m04.車輌ID,
            //                            集計年月 = s14SB.集計年月,
            //                            登録日時 = s14SB.登録日時,
            //                            更新日時 = s14SB.更新日時,
            //                        });
            //    }


            //    //削除データ検索条件

            //        query = query.OrderBy(c => (c.車輌KEY));
            var query = new List<S14_CARSB_Member_Preview_csv>();
                return query.ToList();
            //}
        }

    }
}
