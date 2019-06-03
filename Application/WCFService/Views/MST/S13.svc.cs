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
    public class S13 : IS13 {

        /// <summary>
        /// S13_DRVのデータ取得
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p発地ID">発地ID</param>
        /// <param name="p着地ID">着地ID</param>
        /// <param name="p商品ID">商品ID</param>
        /// <returns>S13_DRV_Member</returns>
        public List<S13_DRV_Member> GetData(int p乗務員ID, int p集計年月 )
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from s13 in context.S13_DRV
                           from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == s13.乗務員KEY)
                           where (s13.乗務員KEY == (from drv in context.M04_DRV where drv.乗務員ID == p乗務員ID select drv.乗務員KEY).FirstOrDefault()
                           && s13.集計年月 == p集計年月 )
                           select new S13_DRV_Member
                           {
							   乗務員KEY = m04.乗務員KEY,
                               集計年月 = s13.集計年月,
                               登録日時 = s13.登録日時,
                               更新日時 = s13.更新日時,
                               自社部門ID = s13.自社部門ID,
                               車種ID = s13.車種ID,
                               車輌KEY = s13.車輌KEY,
                               営業日数 = s13.営業日数,
                               稼動日数 = s13.稼動日数,
                               走行ＫＭ = s13.走行ＫＭ,
                               実車ＫＭ = s13.実車ＫＭ,
                               輸送屯数 = s13.輸送屯数,
                               運送収入 = s13.運送収入,
                               燃料Ｌ = s13.燃料Ｌ,
                               一般管理費 = s13.一般管理費,
                               拘束時間 = s13.拘束時間,
                               運転時間 = s13.運転時間,
                               高速時間 = s13.高速時間,
                               作業時間 = s13.作業時間,
                               待機時間 = s13.待機時間,
                               休憩時間 = s13.休憩時間,
                               残業時間 = s13.残業時間,
                               深夜時間 = s13.深夜時間,


                           }).AsQueryable();;
                return ret.ToList();
			}
        }
       
        /// <summary>
        /// S13_DRVの新規追加
        /// </summary>
        /// <param name="s13drvs">S13_DRV_Member</param>
        public void Insert(S13_DRV_Member s13drvs)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                S13_DRV s13 = new S13_DRV();

                s13.乗務員KEY = s13drvs.乗務員KEY;
                s13.集計年月 = s13drvs.集計年月;
                s13.登録日時 = s13drvs.登録日時;
                s13.更新日時 = s13drvs.更新日時;
                s13.自社部門ID = s13drvs.自社部門ID;
                s13.車種ID = s13drvs.車種ID;
                s13.車輌KEY = s13drvs.車輌KEY;
                s13.営業日数 = s13drvs.営業日数;
                s13.稼動日数 = s13drvs.稼動日数;
                s13.走行ＫＭ = s13drvs.走行ＫＭ;
                s13.実車ＫＭ = s13drvs.実車ＫＭ;
                s13.輸送屯数 = s13drvs.輸送屯数;
                s13.運送収入 = s13drvs.運送収入;
                s13.燃料Ｌ = s13drvs.燃料Ｌ;
                s13.一般管理費 = s13drvs.一般管理費;
                s13.拘束時間 = s13drvs.拘束時間;
                s13.運転時間 = s13drvs.運転時間;
                s13.高速時間 = s13drvs.高速時間;
                s13.作業時間 = s13drvs.作業時間;
                s13.待機時間 = s13drvs.待機時間;
                s13.休憩時間 = s13drvs.休憩時間;
                s13.残業時間 = s13drvs.残業時間;
                s13.深夜時間 = s13drvs.深夜時間;

                
                try
                {
                    // newのエンティティに対してはAcceptChangesで新規追加となる
                    context.S13_DRV.ApplyChanges(s13);
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
        /// S13_DRVの更新
        /// </summary>
        /// <param name="s13drvs">S13_DRV_Member</param>
        public void Update(S13_DRV_Member s13drvs)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // トランザクションのインスタンス化(開始)
                using (var tran = new TransactionScope())
                {
                    context.Connection.Open();

                    //更新行を特定
                    var ret = from x in context.S13_DRV
                              where (x.乗務員KEY == s13drvs.乗務員KEY
                              && x.集計年月 == s13drvs.集計年月)
                              select x;
                    var s13 = ret.FirstOrDefault();


                    if ((s13 != null))
                    {
                        s13.乗務員KEY = s13drvs.乗務員KEY;
                        s13.集計年月 = s13drvs.集計年月;
                        s13.登録日時 = s13drvs.登録日時;
                        s13.更新日時 = s13drvs.更新日時;
                        s13.自社部門ID = s13drvs.自社部門ID;
                        s13.車種ID = s13drvs.車種ID;
                        s13.車輌KEY = s13drvs.車輌KEY;
                        s13.営業日数 = s13drvs.営業日数;
                        s13.稼動日数 = s13drvs.稼動日数;
                        s13.走行ＫＭ = s13drvs.走行ＫＭ;
                        s13.実車ＫＭ = s13drvs.実車ＫＭ;
                        s13.輸送屯数 = s13drvs.輸送屯数;
                        s13.運送収入 = s13drvs.運送収入;
                        s13.燃料Ｌ = s13drvs.燃料Ｌ;
                        s13.一般管理費 = s13drvs.一般管理費;
                        s13.拘束時間 = s13drvs.拘束時間;
                        s13.運転時間 = s13drvs.運転時間;
                        s13.高速時間 = s13drvs.高速時間;
                        s13.作業時間 = s13drvs.作業時間;
                        s13.待機時間 = s13drvs.待機時間;
                        s13.休憩時間 = s13drvs.休憩時間;
                        s13.残業時間 = s13drvs.残業時間;
                        s13.深夜時間 = s13drvs.深夜時間;

                        s13.AcceptChanges();
                    }
                    //else
                    //{
                    //    context.S13_DRV.AddObject(
                    //        new S13_DRV()
                    //        {
                    //            乗務員KEY = (from drv in context.M04_DRV where drv.乗務員ID == s13drvs.乗務員KEY select drv.乗務員KEY).FirstOrDefault(),
                    //            集計年月 = s13drvs.集計年月,
                    //            登録日時 = s13drvs.登録日時,
                    //            更新日時 = s13drvs.更新日時,
                    //            自社部門ID = s13drvs.自社部門ID,
                    //            車種ID = s13drvs.車種ID,
                    //            車輌KEY = s13drvs.車輌KEY,
                    //            営業日数 = s13drvs.営業日数,
                    //            稼動日数 = s13drvs.稼動日数,
                    //            走行ＫＭ = s13drvs.走行ＫＭ,
                    //            実車ＫＭ = s13drvs.実車ＫＭ,
                    //            輸送屯数 = s13drvs.輸送屯数,
                    //            運送収入 = s13drvs.運送収入,
                    //            燃料Ｌ = s13drvs.燃料Ｌ,
                    //            一般管理費 = s13drvs.一般管理費,
                    //            拘束時間 = s13drvs.拘束時間,
                    //            運転時間 = s13drvs.運転時間,
                    //            高速時間 = s13drvs.高速時間,
                    //            作業時間 = s13drvs.作業時間,
                    //            待機時間 = s13drvs.待機時間,
                    //            休憩時間 = s13drvs.休憩時間,
                    //            残業時間 = s13drvs.残業時間,
                    //            深夜時間 = s13drvs.深夜時間,
                    //        });

                    //    context.SaveChanges();
                    //    //var p2 = (from x in context.S13_DRV where x.得意先KEY == s13drvs.得意先KEY && x.集計年月 == s13drvs.集計年月 && x.回数 == s13drvs.回数 select x.得意先KEY).FirstOrDefault();
                    //    //s13drvs.得意先KEY = p2;
                    //}

                    context.SaveChanges();

                    tran.Complete();
                }
            }
        }

        /// <summary>
        /// S13_DRVの物理削除
        /// </summary>
        /// <param name="s13drvs">S13_DRV_Member</param>
        public void Delete(int? p乗務員ID, int? p集計年月 )
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.S13_DRV
                          where (x.乗務員KEY == (from drv in context.M04_DRV where drv.乗務員ID == p乗務員ID select drv.乗務員KEY).FirstOrDefault()
                                 && x.集計年月 == p集計年月 )
						  orderby x.乗務員KEY, x.集計年月
                          select x;
                var s13 = ret.FirstOrDefault();

                context.DeleteObject(s13);
                context.SaveChanges();
            }
        }
        
        /// <summary>
        /// 得意先別車種別単価一覧表プレビュー用出力
        /// 得意先別車種別単価一覧表CSV用出力
        /// </summary>
        /// <returns></returns>
        /// <param name="s13drvs">S13_drvs__Member</param>
        public List<S13_DRV_Member_Preview_csv> GetSearchListData(string p乗務員IDFrom, string p乗務員IDTo, string p処理年月From, string p処理年月To, int[] i乗務員List)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from s13 in context.S13_DRV
                             from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == s13.乗務員KEY)
                             //where s13.削除日付 == null

                             select new S13_DRV_Member_Preview_csv
                           {
                               乗務員KEY = m04.乗務員ID,
                               集計年月 = s13.集計年月,
                               登録日時 = s13.登録日時,
                               更新日時 = s13.更新日時,
                               自社部門ID = s13.自社部門ID,
                               車種ID = s13.車種ID,
                               車輌KEY = s13.車輌KEY,
                               営業日数 = s13.営業日数,
                               稼動日数 = s13.稼動日数,
                               走行ＫＭ = s13.走行ＫＭ,
                               実車ＫＭ = s13.実車ＫＭ,
                               輸送屯数 = s13.輸送屯数,
                               運送収入 = s13.運送収入,
                               燃料Ｌ = s13.燃料Ｌ,
                               一般管理費 = s13.一般管理費,
                               拘束時間 = s13.拘束時間,
                               運転時間 = s13.運転時間,
                               高速時間 = s13.高速時間,
                               作業時間 = s13.作業時間,
                               待機時間 = s13.待機時間,
                               休憩時間 = s13.休憩時間,
                               残業時間 = s13.残業時間,
                               深夜時間 = s13.深夜時間,

                           });


                if (!string.IsNullOrEmpty(p乗務員IDFrom))
                {
                    int ip乗務員IDFrom = AppCommon.IntParse(p乗務員IDFrom);
                    query = query.Where(c => c.乗務員KEY >= ip乗務員IDFrom);
                }
                if (!string.IsNullOrEmpty(p乗務員IDTo))
                {
                    int ip乗務員IDTo = AppCommon.IntParse(p乗務員IDTo);
                    query = query.Where(c => c.乗務員KEY <= ip乗務員IDTo);
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

                if (i乗務員List.Length > 0)
                {
                    var intCause = i乗務員List;

                    query = query.Union(from s13 in context.S13_DRV
                                        from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == s13.乗務員KEY)
                                        where intCause.Contains(m04.乗務員KEY)
                                        select new S13_DRV_Member_Preview_csv
                                    {

                                        乗務員KEY = m04.乗務員ID,
                                        集計年月 = s13.集計年月,
                                        登録日時 = s13.登録日時,
                                        更新日時 = s13.更新日時,
                                        自社部門ID = s13.自社部門ID,
                                        車種ID = s13.車種ID,
                                        車輌KEY = s13.車輌KEY,
                                        営業日数 = s13.営業日数,
                                        稼動日数 = s13.稼動日数,
                                        走行ＫＭ = s13.走行ＫＭ,
                                        実車ＫＭ = s13.実車ＫＭ,
                                        輸送屯数 = s13.輸送屯数,
                                        運送収入 = s13.運送収入,
                                        燃料Ｌ = s13.燃料Ｌ,
                                        一般管理費 = s13.一般管理費,
                                        拘束時間 = s13.拘束時間,
                                        運転時間 = s13.運転時間,
                                        高速時間 = s13.高速時間,
                                        作業時間 = s13.作業時間,
                                        待機時間 = s13.待機時間,
                                        休憩時間 = s13.休憩時間,
                                        残業時間 = s13.残業時間,
                                        深夜時間 = s13.深夜時間,
                                    });
                }


                //削除データ検索条件

                    query = query.OrderBy(c => (c.乗務員KEY));
                
                return query.ToList();
            }
        }

    }
}
