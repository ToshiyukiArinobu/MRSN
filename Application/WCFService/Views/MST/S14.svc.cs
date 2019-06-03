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
    public class S14 : IS14 {

        /// <summary>
        /// S14_CARのデータ取得
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p発地ID">発地ID</param>
        /// <param name="p着地ID">着地ID</param>
        /// <param name="p商品ID">商品ID</param>
        /// <returns>S14_CAR_Member</returns>
        public List<S14_CAR_Member> GetData(int p車輌ID, int p集計年月 )
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from s14 in context.S14_CAR
                           from m05 in context.M05_CAR.Where(m05 => m05.車輌KEY == s14.車輌KEY)
                           where (s14.車輌KEY == (from drv in context.M05_CAR where drv.車輌ID == p車輌ID select drv.車輌KEY).FirstOrDefault()
                           && s14.集計年月 == p集計年月 )
                           select new S14_CAR_Member
                           {
							   車輌KEY = m05.車輌KEY,
                               集計年月 = s14.集計年月,
                               登録日時 = s14.登録日時,
                               更新日時 = s14.更新日時,
                               自社部門ID = s14.自社部門ID,
                               車種ID = s14.車種ID,
                               乗務員KEY = s14.乗務員KEY,
                               営業日数 = s14.営業日数,
                               稼動日数 = s14.稼動日数,
                               走行ＫＭ = s14.走行ＫＭ,
                               実車ＫＭ = s14.実車ＫＭ,
                               輸送屯数 = s14.輸送屯数,
                               運送収入 = s14.運送収入,
                               燃料Ｌ = s14.燃料Ｌ,
                               一般管理費 = s14.一般管理費,
                               拘束時間 = s14.拘束時間,
                               運転時間 = s14.運転時間,
                               高速時間 = s14.高速時間,
                               作業時間 = s14.作業時間,
                               待機時間 = s14.待機時間,
                               休憩時間 = s14.休憩時間,
                               残業時間 = s14.残業時間,
                               深夜時間 = s14.深夜時間,


                           }).AsQueryable();;
                return ret.ToList();
			}
        }
       
        /// <summary>
        /// S14_CARの新規追加
        /// </summary>
        /// <param name="s14drvs">S14_CAR_Member</param>
        public void Insert(S14_CAR_Member s14drvs)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                S14_CAR s14 = new S14_CAR();

                s14.車輌KEY = s14drvs.車輌KEY;
                s14.集計年月 = s14drvs.集計年月;
                s14.登録日時 = s14drvs.登録日時;
                s14.更新日時 = s14drvs.更新日時;
                s14.自社部門ID = s14drvs.自社部門ID;
                s14.車種ID = s14drvs.車種ID;
                s14.車輌KEY = s14drvs.車輌KEY;
                s14.営業日数 = s14drvs.営業日数;
                s14.稼動日数 = s14drvs.稼動日数;
                s14.走行ＫＭ = s14drvs.走行ＫＭ;
                s14.実車ＫＭ = s14drvs.実車ＫＭ;
                s14.輸送屯数 = s14drvs.輸送屯数;
                s14.運送収入 = s14drvs.運送収入;
                s14.燃料Ｌ = s14drvs.燃料Ｌ;
                s14.一般管理費 = s14drvs.一般管理費;
                s14.拘束時間 = s14drvs.拘束時間;
                s14.運転時間 = s14drvs.運転時間;
                s14.高速時間 = s14drvs.高速時間;
                s14.作業時間 = s14drvs.作業時間;
                s14.待機時間 = s14drvs.待機時間;
                s14.休憩時間 = s14drvs.休憩時間;
                s14.残業時間 = s14drvs.残業時間;
                s14.深夜時間 = s14drvs.深夜時間;

                
                try
                {
                    // newのエンティティに対してはAcceptChangesで新規追加となる
                    context.S14_CAR.ApplyChanges(s14);
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
        /// S14_CARの更新
        /// </summary>
        /// <param name="s14drvs">S14_CAR_Member</param>
        public void Update(S14_CAR_Member s14drvs)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // トランザクションのインスタンス化(開始)
                using (var tran = new TransactionScope())
                {
                    context.Connection.Open();

                    //更新行を特定
                    var ret = from x in context.S14_CAR
                              where (x.車輌KEY == s14drvs.車輌KEY
                              && x.集計年月 == s14drvs.集計年月)
                              select x;
                    var s14 = ret.FirstOrDefault();


                    if ((s14 != null))
                    {
                        s14.車輌KEY = s14drvs.車輌KEY;
                        s14.集計年月 = s14drvs.集計年月;
                        s14.登録日時 = s14drvs.登録日時;
                        s14.更新日時 = s14drvs.更新日時;
                        s14.自社部門ID = s14drvs.自社部門ID;
                        s14.車種ID = s14drvs.車種ID;
                        s14.乗務員KEY = s14drvs.乗務員KEY;
                        s14.営業日数 = s14drvs.営業日数;
                        s14.稼動日数 = s14drvs.稼動日数;
                        s14.走行ＫＭ = s14drvs.走行ＫＭ;
                        s14.実車ＫＭ = s14drvs.実車ＫＭ;
                        s14.輸送屯数 = s14drvs.輸送屯数;
                        s14.運送収入 = s14drvs.運送収入;
                        s14.燃料Ｌ = s14drvs.燃料Ｌ;
                        s14.一般管理費 = s14drvs.一般管理費;
                        s14.拘束時間 = s14drvs.拘束時間;
                        s14.運転時間 = s14drvs.運転時間;
                        s14.高速時間 = s14drvs.高速時間;
                        s14.作業時間 = s14drvs.作業時間;
                        s14.待機時間 = s14drvs.待機時間;
                        s14.休憩時間 = s14drvs.休憩時間;
                        s14.残業時間 = s14drvs.残業時間;
                        s14.深夜時間 = s14drvs.深夜時間;

                        s14.AcceptChanges();
                    }
                    //else
                    //{
                    //    context.S14_CAR.AddObject(
                    //        new S14_CAR()
                    //        {
                    //            車輌KEY = (from drv in context.M05_CAR where drv.車輌ID == s14drvs.車輌KEY select drv.車輌KEY).FirstOrDefault(),
                    //            集計年月 = s14drvs.集計年月,
                    //            登録日時 = s14drvs.登録日時,
                    //            更新日時 = s14drvs.更新日時,
                    //            自社部門ID = s14drvs.自社部門ID,
                    //            車種ID = s14drvs.車種ID,
                    //            車輌KEY = s14drvs.車輌KEY,
                    //            営業日数 = s14drvs.営業日数,
                    //            稼動日数 = s14drvs.稼動日数,
                    //            走行ＫＭ = s14drvs.走行ＫＭ,
                    //            実車ＫＭ = s14drvs.実車ＫＭ,
                    //            輸送屯数 = s14drvs.輸送屯数,
                    //            運送収入 = s14drvs.運送収入,
                    //            燃料Ｌ = s14drvs.燃料Ｌ,
                    //            一般管理費 = s14drvs.一般管理費,
                    //            拘束時間 = s14drvs.拘束時間,
                    //            運転時間 = s14drvs.運転時間,
                    //            高速時間 = s14drvs.高速時間,
                    //            作業時間 = s14drvs.作業時間,
                    //            待機時間 = s14drvs.待機時間,
                    //            休憩時間 = s14drvs.休憩時間,
                    //            残業時間 = s14drvs.残業時間,
                    //            深夜時間 = s14drvs.深夜時間,
                    //        });

                    //    context.SaveChanges();
                    //    //var p2 = (from x in context.S14_CAR where x.得意先KEY == s14drvs.得意先KEY && x.集計年月 == s14drvs.集計年月 && x.回数 == s14drvs.回数 select x.得意先KEY).FirstOrDefault();
                    //    //s14drvs.得意先KEY = p2;
                    //}

                    context.SaveChanges();

                    tran.Complete();
                }
            }
        }

        /// <summary>
        /// S14_CARの物理削除
        /// </summary>
        /// <param name="s14drvs">S14_CAR_Member</param>
        public void Delete(int? p車輌ID, int? p集計年月 )
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.S14_CAR
                          where (x.車輌KEY == (from drv in context.M05_CAR where drv.車輌ID == p車輌ID select drv.車輌KEY).FirstOrDefault()
                                 && x.集計年月 == p集計年月 )
						  orderby x.車輌KEY, x.集計年月
                          select x;
                var s14 = ret.FirstOrDefault();

                context.DeleteObject(s14);
                context.SaveChanges();
            }
        }
        
        /// <summary>
        /// 得意先別車種別単価一覧表プレビュー用出力
        /// 得意先別車種別単価一覧表CSV用出力
        /// </summary>
        /// <returns></returns>
        /// <param name="s14drvs">S14_drvs__Member</param>
        public List<S14_CAR_Member_Preview_csv> GetSearchListData(string p車輌IDFrom, string p車輌IDTo, string p処理年月From, string p処理年月To, int[] i車輌List)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from s14 in context.S14_CAR
                             from m05 in context.M05_CAR.Where(m05 => m05.車輌KEY == s14.車輌KEY)
                             //where s14.削除日付 == null

                             select new S14_CAR_Member_Preview_csv
                           {
                               車輌KEY = m05.車輌ID,
                               集計年月 = s14.集計年月,
                               登録日時 = s14.登録日時,
                               更新日時 = s14.更新日時,
                               自社部門ID = s14.自社部門ID,
                               車種ID = s14.車種ID,
                               乗務員KEY = s14.乗務員KEY,
                               営業日数 = s14.営業日数,
                               稼動日数 = s14.稼動日数,
                               走行ＫＭ = s14.走行ＫＭ,
                               実車ＫＭ = s14.実車ＫＭ,
                               輸送屯数 = s14.輸送屯数,
                               運送収入 = s14.運送収入,
                               燃料Ｌ = s14.燃料Ｌ,
                               一般管理費 = s14.一般管理費,
                               拘束時間 = s14.拘束時間,
                               運転時間 = s14.運転時間,
                               高速時間 = s14.高速時間,
                               作業時間 = s14.作業時間,
                               待機時間 = s14.待機時間,
                               休憩時間 = s14.休憩時間,
                               残業時間 = s14.残業時間,
                               深夜時間 = s14.深夜時間,

                           });


                if (!string.IsNullOrEmpty(p車輌IDFrom))
                {
                    int ip車輌IDFrom = AppCommon.IntParse(p車輌IDFrom);
                    query = query.Where(c => c.車輌KEY >= ip車輌IDFrom);
                }
                if (!string.IsNullOrEmpty(p車輌IDTo))
                {
                    int ip車輌IDTo = AppCommon.IntParse(p車輌IDTo);
                    query = query.Where(c => c.車輌KEY <= ip車輌IDTo);
                }

                if (!string.IsNullOrEmpty(p処理年月From))
                {
                    int ip処理年月From = AppCommon.IntParse(p処理年月From);
                    query = query.Where(c => c.集計年月 >= ip処理年月From);
                }
                if (!string.IsNullOrEmpty(p処理年月To))
                {
                    int ip処理年月To = AppCommon.IntParse(p処理年月To);
                    query = query.Where(c => c.集計年月 <= ip処理年月To);
                }

                if (i車輌List.Length > 0)
                {
                    var intCause = i車輌List;

                    query = query.Union(from s14 in context.S14_CAR
                                        from m05 in context.M05_CAR.Where(m05 => m05.車輌KEY == s14.車輌KEY)
                                        where intCause.Contains(m05.車輌KEY)
                                        select new S14_CAR_Member_Preview_csv
                                    {

                                        車輌KEY = m05.車輌ID,
                                        集計年月 = s14.集計年月,
                                        登録日時 = s14.登録日時,
                                        更新日時 = s14.更新日時,
                                        自社部門ID = s14.自社部門ID,
                                        車種ID = s14.車種ID,
                                        乗務員KEY = s14.乗務員KEY,
                                        営業日数 = s14.営業日数,
                                        稼動日数 = s14.稼動日数,
                                        走行ＫＭ = s14.走行ＫＭ,
                                        実車ＫＭ = s14.実車ＫＭ,
                                        輸送屯数 = s14.輸送屯数,
                                        運送収入 = s14.運送収入,
                                        燃料Ｌ = s14.燃料Ｌ,
                                        一般管理費 = s14.一般管理費,
                                        拘束時間 = s14.拘束時間,
                                        運転時間 = s14.運転時間,
                                        高速時間 = s14.高速時間,
                                        作業時間 = s14.作業時間,
                                        待機時間 = s14.待機時間,
                                        休憩時間 = s14.休憩時間,
                                        残業時間 = s14.残業時間,
                                        深夜時間 = s14.深夜時間,
                                    });
                }


                //削除データ検索条件

                    query = query.OrderBy(c => (c.車輌KEY));
                
                return query.ToList();
            }
        }

    }
}
