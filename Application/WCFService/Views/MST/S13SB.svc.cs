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
    public class S13SB : IS13SB {

        /// <summary>
        /// S13_DRVSBのデータ取得 変動データ取得
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p発地ID">発地ID</param>
        /// <param name="p着地ID">着地ID</param>
        /// <param name="p商品ID">商品ID</param>
        /// <returns>S13_DRVSB_Member</returns>
        public List<S13_DRVSB_Member> GetData_Hendo(int p乗務員ID, int p集計年月)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from s13SB in context.S13_DRVSB
                           from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == s13SB.乗務員KEY)
                           join m07 in context.M07_KEI.Where(c => c.削除日付 == null) on s13SB.経費項目ID equals m07.経費項目ID into m07Group
                           from m07g in m07Group.DefaultIfEmpty()
                           orderby m07g.経費区分, s13SB.経費項目ID
                           where (s13SB.乗務員KEY == (from drv in context.M04_DRV where drv.乗務員ID == p乗務員ID select drv.乗務員KEY).FirstOrDefault()
                           && s13SB.集計年月 == p集計年月 && m07g.経費区分 != 3 && m07g.固定変動区分 == 1)

                           select new S13_DRVSB_Member
                           {
                               乗務員KEY = m04.乗務員KEY,
                               集計年月 = s13SB.集計年月,
                               経費項目ID = s13SB.経費項目ID,
                               登録日時 = s13SB.登録日時,
                               更新日時 = s13SB.更新日時,
                               経費項目名 = s13SB.経費項目名,
                               固定変動区分 = s13SB.固定変動区分,
                               金額 = s13SB.金額,
                               経費区分 = m07g.経費区分,

                           }).AsQueryable(); ;
                return ret.ToList();
            }
        }

        /// <summary>
        /// S13_DRVSBのデータ取得 人件費データ取得
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p発地ID">発地ID</param>
        /// <param name="p着地ID">着地ID</param>
        /// <param name="p商品ID">商品ID</param>
        /// <returns>S13_DRVSB_Member</returns>
        public List<S13_DRVSB_Member> GetData_Jinken(int p乗務員ID, int p集計年月)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from s13SB in context.S13_DRVSB
                           from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == s13SB.乗務員KEY)
                           join m07 in context.M07_KEI.Where(c => c.削除日付 == null) on s13SB.経費項目ID equals m07.経費項目ID into m07Group
                           from m07g in m07Group.DefaultIfEmpty()
                           orderby m07g.経費区分, s13SB.経費項目ID
                           where (s13SB.乗務員KEY == (from drv in context.M04_DRV where drv.乗務員ID == p乗務員ID select drv.乗務員KEY).FirstOrDefault()
                           && s13SB.集計年月 == p集計年月 && m07g.経費区分 == 3)

                           select new S13_DRVSB_Member
                           {
                               乗務員KEY = m04.乗務員KEY,
                               集計年月 = s13SB.集計年月,
                               経費項目ID = s13SB.経費項目ID,
                               登録日時 = s13SB.登録日時,
                               更新日時 = s13SB.更新日時,
                               経費項目名 = s13SB.経費項目名,
                               固定変動区分 = s13SB.固定変動区分,
                               金額 = s13SB.金額,
                               経費区分 = m07g.経費区分,

                           }).AsQueryable(); ;
                return ret.ToList();
            }
        }

        /// <summary>
        /// S13_DRVSBのデータ取得 固定項目取得
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p発地ID">発地ID</param>
        /// <param name="p着地ID">着地ID</param>
        /// <param name="p商品ID">商品ID</param>
        /// <returns>S13_DRVSB_Member</returns>
        public List<S13_DRVSB_Member> GetData_Kotei(int p乗務員ID, int p集計年月)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from s13SB in context.S13_DRVSB
                           from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == s13SB.乗務員KEY)
                           join m07 in context.M07_KEI.Where(c => c.削除日付 == null) on s13SB.経費項目ID equals m07.経費項目ID into m07Group
                           from m07g in m07Group.DefaultIfEmpty()
                           orderby m07g.経費区分, s13SB.経費項目ID
                           where (s13SB.乗務員KEY == (from drv in context.M04_DRV where drv.乗務員ID == p乗務員ID select drv.乗務員KEY).FirstOrDefault()
                           && s13SB.集計年月 == p集計年月 && m07g.経費区分 != 3 && m07g.固定変動区分 == 0)

                           select new S13_DRVSB_Member
                           {
                               乗務員KEY = m04.乗務員KEY,
                               集計年月 = s13SB.集計年月,
                               経費項目ID = s13SB.経費項目ID,
                               登録日時 = s13SB.登録日時,
                               更新日時 = s13SB.更新日時,
                               経費項目名 = s13SB.経費項目名,
                               固定変動区分 = s13SB.固定変動区分,
                               金額 = s13SB.金額,
                               経費区分 = m07g.経費区分,

                           }).AsQueryable(); ;
                return ret.ToList();
            }
        }


        /// <summary>
        /// S13_DRVSBの新規追加
        /// </summary>
        /// <param name="s13SBdrvs">S13_DRVSB_Member</param>
        public void Insert(S13_DRVSB_Member s13SBdrvs)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                S13_DRVSB s13SB = new S13_DRVSB();

                s13SB.乗務員KEY = s13SBdrvs.乗務員KEY;
                s13SB.集計年月 = s13SBdrvs.集計年月;
                s13SB.経費項目ID = s13SBdrvs.経費項目ID;
                s13SB.登録日時 = s13SBdrvs.登録日時;
                s13SB.更新日時 = s13SBdrvs.更新日時;
                s13SB.経費項目名 = s13SBdrvs.経費項目名;
                s13SB.固定変動区分 = s13SBdrvs.固定変動区分;
                s13SB.金額 = s13SBdrvs.金額;

                try
                {
                    // newのエンティティに対してはAcceptChangesで新規追加となる
                    context.S13_DRVSB.ApplyChanges(s13SB);
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
        /// S13_DRVSBの更新 変動項目更新
        /// </summary>
        /// <param name="s13SBdrvs">S13_DRVSB_Member</param>
        public void Update_Hendo(S13_DRV_Member s13drv, List<S13_DRVSB_Member> s13SBHen, List<S13_DRVSB_Member> s13SBJin, List<S13_DRVSB_Member> s13SBKotei)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // トランザクションのインスタンス化(開始)
                using (var tran = new TransactionScope())
                {
                    context.Connection.Open();

                    var ret = (from x in context.S13_DRVSB
                               where x.乗務員KEY == s13drv.乗務員KEY && x.集計年月 == s13drv.集計年月
                               select x).ToList();
                    foreach (var rec1 in ret)
                    {
                        context.DeleteObject(rec1);
                    }

                    foreach (S13_DRVSB_Member ddt in s13SBHen)
                    {
                        var dat = new S13_DRVSB()
                        {
                            乗務員KEY = ddt.乗務員KEY,
                            集計年月 = ddt.集計年月,
                            経費項目ID = ddt.経費項目ID,
                            登録日時 = ddt.登録日時,
                            更新日時 = DateTime.Now,
                            経費項目名 = ddt.経費項目名,
                            固定変動区分 = ddt.固定変動区分,
                            金額 = ddt.金額,
                        };
                        context.S13_DRVSB.ApplyChanges(dat);
                    }

                    foreach (S13_DRVSB_Member ddt in s13SBJin)
                    {
                        var dat = new S13_DRVSB()
                        {
                            乗務員KEY = ddt.乗務員KEY,
                            集計年月 = ddt.集計年月,
                            経費項目ID = ddt.経費項目ID,
                            登録日時 = ddt.登録日時,
                            更新日時 = DateTime.Now,
                            経費項目名 = ddt.経費項目名,
                            固定変動区分 = ddt.固定変動区分,
                            金額 = ddt.金額,
                        };
                        context.S13_DRVSB.ApplyChanges(dat);
                    }

                    foreach (S13_DRVSB_Member ddt in s13SBKotei)
                    {
                        var dat = new S13_DRVSB()
                        {
                            乗務員KEY = ddt.乗務員KEY,
                            集計年月 = ddt.集計年月,
                            経費項目ID = ddt.経費項目ID,
                            登録日時 = ddt.登録日時,
                            更新日時 = DateTime.Now,
                            経費項目名 = ddt.経費項目名,
                            固定変動区分 = ddt.固定変動区分,
                            金額 = ddt.金額,
                        };
                        context.S13_DRVSB.ApplyChanges(dat);
                    }

                    context.SaveChanges();
                    tran.Complete();
                }
            }
        }

		/// <summary>
		/// S13_DRVSBの物理削除
		/// </summary>
		/// <param name="s13SBdrvs">S13_DRVSB_Member</param>
		public void Delete_Hendo(int? p乗務員ID, int? p集計年月)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

				//削除行を特定
				var ret = from x in context.S13_DRVSB
						  where (x.乗務員KEY == (from drv in context.M04_DRV where drv.乗務員ID == p乗務員ID select drv.乗務員KEY).FirstOrDefault()
								 && x.集計年月 == p集計年月)
						  orderby x.乗務員KEY, x.集計年月
						  select x;

				foreach (var row in ret)
				{
					context.DeleteObject(row);
				}

				context.SaveChanges();
			}
		}

		/// <summary>
		/// S13_DRVSBの物理削除
		/// </summary>
		/// <param name="s13SBdrvs">S13_DRVSB_Member</param>
		public void Delete(int? p乗務員ID, int? p集計年月)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

				//削除行を特定
				var ret = from x in context.S13_DRVSB
						  where (x.乗務員KEY == (from drv in context.M04_DRV where drv.乗務員ID == p乗務員ID select drv.乗務員KEY).FirstOrDefault()
								 && x.集計年月 == p集計年月)
						  orderby x.乗務員KEY, x.集計年月
						  select x;
				var s13SB = ret.FirstOrDefault();

				context.DeleteObject(s13SB);
				context.SaveChanges();
			}
		}
        
        /// <summary>
        /// 得意先別車種別単価一覧表プレビュー用出力
        /// 得意先別車種別単価一覧表CSV用出力
        /// </summary>
        /// <returns></returns>
        /// <param name="s13SBdrvs">S13SB_drvs__Member</param>
        public List<S13_DRVSB_Member_Preview_csv> GetSearchListData(string p乗務員IDFrom, string p乗務員IDTo, string p処理年月From, string p処理年月To, int[] i乗務員List)
        {
            //using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            //{
            //    context.Connection.Open();


            //    var query = (from s13SB in context.S13_DRVSB
            //               from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == s13SB.乗務員KEY)
            //               join m07 in context.M07_KEI on s13SB.経費項目ID equals m07.経費項目ID into m07Group
            //               where (s13SB.乗務員KEY == (from drv in context.M04_DRV where drv.乗務員ID == p乗務員ID select drv.乗務員KEY).FirstOrDefault()
            //               && s13SB.集計年月 == p集計年月)
            //               select new S13_DRVSB_Member_Preview_csv
            //               {
            //                   乗務員KEY = m04.乗務員KEY,
            //                   集計年月 = s13SB.集計年月,
            //                   経費項目ID = s13SB.経費項目ID,
            //                   登録日時 = s13SB.登録日時,
            //                   更新日時 = s13SB.更新日時,
            //                   経費項目名 = s13SB.経費項目名,
            //                   固定変動区分 = s13SB.固定変動区分,
            //                   金額 = s13SB.金額,
            //                   経費区分 = m07Group.Min(m07g => m07g.経費区分)

            //               }).AsQueryable(); ;
                
                


            //    if (!string.IsNullOrEmpty(p乗務員IDFrom))
            //    {
            //        int ip乗務員IDFrom = AppCommon.IntParse(p乗務員IDFrom);
            //        query = query.Where(c => c.乗務員KEY >= ip乗務員IDFrom);
            //    }
            //    if (!string.IsNullOrEmpty(p乗務員IDTo))
            //    {
            //        int ip乗務員IDTo = AppCommon.IntParse(p乗務員IDTo);
            //        query = query.Where(c => c.乗務員KEY <= ip乗務員IDTo);
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

            //        query = query.Union(from s13SB in context.S13_DRVSB
            //                            from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == s13SB.乗務員KEY)
            //                            where intCause.Contains(m04.乗務員KEY)
            //                            select new S13_DRVSB_Member_Preview_csv
            //                        {

            //                            乗務員KEY = m04.乗務員ID,
            //                            集計年月 = s13SB.集計年月,
            //                            登録日時 = s13SB.登録日時,
            //                            更新日時 = s13SB.更新日時,
            //                        });
            //    }


            //    //削除データ検索条件

            //        query = query.OrderBy(c => (c.乗務員KEY));
            var query = new List<S13_DRVSB_Member_Preview_csv>();
                return query.ToList();
            //}
        }

    }
}
