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
    public class S02 : IS02 {

        /// <summary>
        /// S02_YOSSのデータ取得
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p発地ID">発地ID</param>
        /// <param name="p着地ID">着地ID</param>
        /// <param name="p商品ID">商品ID</param>
        /// <returns>S02_YOSS_Member</returns>
        public List<S02_YOSS_Member> GetData(int p得意先ID, int p集計年月, int p回数)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from s01 in context.S02_YOSS
                           from m01 in context.M01_TOK.Where(m01 => m01.得意先KEY == s01.支払先KEY)
                           where (s01.支払先KEY == (from tok in context.M01_TOK where tok.得意先ID == p得意先ID select tok.得意先KEY).FirstOrDefault()
                           && s01.集計年月 == p集計年月 && s01.回数 == p回数 )
                           select new S02_YOSS_Member
                           {
                               支払先KEY = m01.得意先ID,
                               集計年月 = s01.集計年月,
                               回数 = s01.回数,
                               登録日時 = s01.登録日時,
                               更新日時 = s01.更新日時,
                               締集計開始日 = s01.締集計開始日,
                               締集計終了日 = s01.締集計終了日,
                               締日前月残高 = s01.締日前月残高,
                               締日入金現金 = s01.締日入金現金,
                               締日入金手形 = s01.締日入金手形,
                               締日入金その他 = s01.締日入金その他,
                               締日売上金額 = s01.締日売上金額,
                               締日通行料 = s01.締日通行料,
                               締日課税売上 = s01.締日課税売上,
                               締日非課税売上 = s01.締日非課税売上,
                               締日消費税 = s01.締日消費税,
                               締日内傭車売上 = s01.締日内傭車売上,
                               締日内傭車料 = s01.締日内傭車料,
                               締日未定件数 = s01.締日未定件数,
                               締日件数 = s01.締日件数,
                               締日 = s01.締日,
                           }).AsQueryable();;
                return ret.ToList();
			}
        }
       
        /// <summary>
        /// S02_YOSSの新規追加
        /// </summary>
        /// <param name="s01toks">S02_YOSS_Member</param>
        public void Insert(S02_YOSS_Member s01toks)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                S02_YOSS s01 = new S02_YOSS();
                s01.支払先KEY = s01toks.支払先KEY;
                s01.集計年月 = s01toks.集計年月;
                s01.回数 = s01toks.回数;
                s01.登録日時 = s01toks.登録日時;
                s01.更新日時 = s01toks.更新日時;
                s01.締集計開始日 = s01toks.締集計開始日;
                s01.締集計終了日 = s01toks.締集計終了日;
                s01.締日前月残高 = s01toks.締日前月残高;
                s01.締日入金現金 = s01toks.締日入金現金;
                s01.締日入金手形 = s01toks.締日入金手形;
                s01.締日入金その他 = s01toks.締日入金その他;
                s01.締日売上金額 = s01toks.締日売上金額;
                s01.締日通行料 = s01toks.締日通行料;
                s01.締日課税売上 = s01toks.締日課税売上;
                s01.締日非課税売上 = s01toks.締日非課税売上;
                s01.締日消費税 = s01toks.締日消費税;
                s01.締日内傭車売上 = s01toks.締日内傭車売上;
                s01.締日内傭車料 = s01toks.締日内傭車料;
                s01.締日未定件数 = s01toks.締日未定件数;
                s01.締日件数 = s01toks.締日件数;
                s01.締日 = s01toks.締日;
                
                try
                {
                    // newのエンティティに対してはAcceptChangesで新規追加となる
                    context.S02_YOSS.ApplyChanges(s01);
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
        /// S02_YOSSの更新
        /// </summary>
        /// <param name="s01toks">S02_YOSS_Member</param>
        public void Update(S02_YOSS_Member s01toks)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // トランザクションのインスタンス化(開始)
                using (var tran = new TransactionScope())
                {
                    context.Connection.Open();

                    //更新行を特定
                    var ret = from x in context.S02_YOSS
                              where (x.支払先KEY == (from tok in context.M01_TOK where tok.得意先ID == s01toks.支払先KEY select tok.得意先KEY).FirstOrDefault()
                              && x.集計年月 == s01toks.集計年月 && x.回数 == s01toks.回数)
                              select x;
                    var s01 = ret.FirstOrDefault();


                    if ((s01 != null))
                    {
                        s01.支払先KEY = (from tok in context.M01_TOK where tok.得意先ID == s01toks.支払先KEY select tok.得意先KEY).FirstOrDefault();
                        s01.集計年月 = s01toks.集計年月;
                        s01.回数 = s01toks.回数;
                        s01.登録日時 = s01toks.登録日時;
                        s01.更新日時 = s01toks.更新日時;
                        s01.締集計開始日 = s01toks.締集計開始日;
                        s01.締集計終了日 = s01toks.締集計終了日;
                        s01.締日前月残高 = s01toks.締日前月残高;
                        s01.締日入金現金 = s01toks.締日入金現金;
                        s01.締日入金手形 = s01toks.締日入金手形;
                        s01.締日入金その他 = s01toks.締日入金その他;
                        s01.締日売上金額 = s01toks.締日売上金額;
                        s01.締日通行料 = s01toks.締日通行料;
                        s01.締日課税売上 = s01toks.締日課税売上;
                        s01.締日非課税売上 = s01toks.締日非課税売上;
                        s01.締日消費税 = s01toks.締日消費税;
                        s01.締日内傭車売上 = s01toks.締日内傭車売上;
                        s01.締日内傭車料 = s01toks.締日内傭車料;
                        s01.締日未定件数 = s01toks.締日未定件数;
                        s01.締日件数 = s01toks.締日件数;
                        s01.締日 = s01toks.締日;

                        s01.AcceptChanges();
                    }
                    else
                    {
                        context.S02_YOSS.AddObject(
                            new S02_YOSS()
                            {

                                支払先KEY = (from tok in context.M01_TOK where tok.得意先ID == s01toks.支払先KEY select tok.得意先KEY).FirstOrDefault(),
                                集計年月 = s01toks.集計年月,
                                回数 = s01toks.回数,
                                登録日時 = s01toks.登録日時,
                                更新日時 = s01toks.更新日時,
                                締集計開始日 = s01toks.締集計開始日,
                                締集計終了日 = s01toks.締集計終了日,
                                締日前月残高 = s01toks.締日前月残高,
                                締日入金現金 = s01toks.締日入金現金,
                                締日入金手形 = s01toks.締日入金手形,
                                締日入金その他 = s01toks.締日入金その他,
                                締日売上金額 = s01toks.締日売上金額,
                                締日通行料 = s01toks.締日通行料,
                                締日課税売上 = s01toks.締日課税売上,
                                締日非課税売上 = s01toks.締日非課税売上,
                                締日消費税 = s01toks.締日消費税,
                                締日内傭車売上 = s01toks.締日内傭車売上,
                                締日内傭車料 = s01toks.締日内傭車料,
                                締日未定件数 = s01toks.締日未定件数,
                                締日件数 = s01toks.締日件数,
                                締日 = s01toks.締日,
                            }
                            );

                        context.SaveChanges();
                        //var p2 = (from x in context.S02_YOSS where x.得意先KEY == s01toks.得意先KEY && x.集計年月 == s01toks.集計年月 && x.回数 == s01toks.回数 select x.得意先KEY).FirstOrDefault();
                        //s01toks.得意先KEY = p2;
                    }

                    context.SaveChanges();

                    tran.Complete();
                }
            }
        }

        /// <summary>
        /// S02_YOSSの物理削除
        /// </summary>
        /// <param name="s01toks">S02_YOSS_Member</param>
        public void Delete(int? p得意先ID, int? p集計年月, int? p回数)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.S02_YOSS
                          where (x.支払先KEY == (from tok in context.M01_TOK where tok.得意先ID == p得意先ID select tok.得意先KEY).FirstOrDefault()
                                 && x.集計年月 == p集計年月 && x.回数 == p回数)
                          orderby x.支払先KEY, x.集計年月, x.回数
                          select x;
                var s01 = ret.FirstOrDefault();

                context.DeleteObject(s01);
                context.SaveChanges();
            }
        }
        
        /// <summary>
        /// 得意先別車種別単価一覧表プレビュー用出力
        /// 得意先別車種別単価一覧表CSV用出力
        /// </summary>
        /// <returns></returns>
        /// <param name="s01toks">S02_toks__Member</param>
        public List<S02_YOSS_Member_Preview_csv> GetSearchListData(string p得意先IDFrom, string p得意先IDTo, string p処理年月From, string p処理年月To, string p回数From, string p回数To, int[] i得意先List)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from s01 in context.S02_YOSS
                             from m01 in context.M01_TOK.Where(m01 => m01.得意先KEY == s01.支払先KEY)
                             //where s01.削除日付 == null

                             select new S02_YOSS_Member_Preview_csv
                           {
                               支払先KEY = m01.得意先ID,
                               得意先名 = m01.略称名,
                               集計年月 = s01.集計年月,
                               回数 = s01.回数,
                               締集計開始日 = s01.締集計開始日,
                               締集計終了日 = s01.締集計終了日,
                               締日前月残高 = s01.締日前月残高,
                               締日入金現金 = s01.締日入金手形,
                               締日入金手形 = s01.締日入金手形,
                               締日入金その他 = s01.締日入金その他,
                               締日売上金額 = s01.締日売上金額,
                               締日通行料 = s01.締日通行料,
                               締日非課税売上 = s01.締日非課税売上,
                               締日課税売上 = s01.締日課税売上,
                               締日消費税 = s01.締日消費税,
                               締日内傭車売上 = s01.締日内傭車売上,
                               締日内傭車料 = s01.締日内傭車料,
                               締日未定件数 = s01.締日未定件数,
                               締日件数 = s01.締日件数,

                           });


                if (!string.IsNullOrEmpty(p得意先IDFrom))
                {
                    int ip得意先IDFrom = AppCommon.IntParse(p得意先IDFrom);
                    query = query.Where(c => c.支払先KEY >= ip得意先IDFrom);
                }
                if (!string.IsNullOrEmpty(p得意先IDTo))
                {
                    int ip得意先IDTo = AppCommon.IntParse(p得意先IDTo);
                    query = query.Where(c => c.支払先KEY <= ip得意先IDTo);
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

                if (!string.IsNullOrEmpty(p回数From))
                {
                    int ip回数From = AppCommon.IntParse(p回数From);
                    query = query.Where(c => c.回数 >= ip回数From);
                }
                if (!string.IsNullOrEmpty(p回数To))
                {
                    int ip回数To = AppCommon.IntParse(p回数To);
                    query = query.Where(c => c.集計年月 <= ip回数To);
                }


                if (i得意先List.Length > 0)
                {
                    var intCause = i得意先List;

                    query = query.Union(from s01 in context.S02_YOSS
                                        from m01 in context.M01_TOK.Where(m01 => m01.得意先KEY == s01.支払先KEY)
                                        where intCause.Contains(m01.得意先KEY)
                                        select new S02_YOSS_Member_Preview_csv
                                    {

                                        支払先KEY = m01.得意先ID,
                                        得意先名 = m01.略称名,
                                        集計年月 = s01.集計年月,
                                        回数 = s01.回数,
                                        締集計開始日 = s01.締集計開始日,
                                        締集計終了日 = s01.締集計終了日,
                                        締日前月残高 = s01.締日前月残高,
                                        締日入金現金 = s01.締日入金手形,
                                        締日入金手形 = s01.締日入金手形,
                                        締日入金その他 = s01.締日入金その他,
                                        締日売上金額 = s01.締日売上金額,
                                        締日通行料 = s01.締日通行料,
                                        締日非課税売上 = s01.締日非課税売上,
                                        締日課税売上 = s01.締日課税売上,
                                        締日消費税 = s01.締日消費税,
                                        締日内傭車売上 = s01.締日内傭車売上,
                                        締日内傭車料 = s01.締日内傭車料,
                                        締日未定件数 = s01.締日未定件数,
                                        締日件数 = s01.締日件数,
                                    });
                }


                //削除データ検索条件

                query = query.OrderBy(c => (c.支払先KEY));
                
                return query.ToList();
            }
        }

    }
}
