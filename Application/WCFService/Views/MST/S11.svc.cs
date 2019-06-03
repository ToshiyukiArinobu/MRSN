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
    public class S11 : IS11 {

        /// <summary>
        /// S11_TOKGのデータ取得
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p発地ID">発地ID</param>
        /// <param name="p着地ID">着地ID</param>
        /// <param name="p商品ID">商品ID</param>
        /// <returns>S11_TOKG_Member</returns>
        public List<S11_TOKG_Member> GetData(int p得意先ID, int p集計年月, int p回数)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from s11 in context.S11_TOKG
                           from m01 in context.M01_TOK.Where(m01 => m01.得意先KEY == s11.得意先KEY)
                           where (s11.得意先KEY == (from tok in context.M01_TOK where tok.得意先ID == p得意先ID select tok.得意先KEY).FirstOrDefault()
                           && s11.集計年月 == p集計年月 && s11.回数 == p回数 )
                           select new S11_TOKG_Member
                           {
                               得意先KEY = m01.得意先ID,
                               集計年月 = s11.集計年月,
                               回数 = s11.回数,
                               登録日時 = s11.登録日時,
                               更新日時 = s11.更新日時,
                               月集計開始日 = s11.月集計開始日,
                               月集計終了日 = s11.月集計終了日,
                               月次前月残高 = s11.月次前月残高,
                               月次入金現金 = s11.月次入金現金,
                               月次入金手形 = s11.月次入金手形,
                               月次入金その他 = s11.月次入金その他,
                               月次売上金額 = s11.月次売上金額,
                               月次通行料 = s11.月次通行料,
                               月次課税売上 = s11.月次課税売上,
                               月次非課税売上 = s11.月次非課税売上,
                               月次消費税 = s11.月次消費税,
                               月次内傭車売上 = s11.月次内傭車売上,
                               月次内傭車料 = s11.月次内傭車料,
                               月次未定件数 = s11.月次未定件数,
                               月次件数 = s11.月次件数,
                               締日 = s11.締日,
                           }).AsQueryable();;
                return ret.ToList();
			}
        }
       
        /// <summary>
        /// S11_TOKGの新規追加
        /// </summary>
        /// <param name="s11tokｇ">S11_TOKG_Member</param>
        public void Insert(S11_TOKG_Member s11tokｇ)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                S11_TOKG s11 = new S11_TOKG();
				s11.得意先KEY = s11tokｇ.得意先KEY;
                s11.集計年月 = s11tokｇ.集計年月;
                s11.回数 = s11tokｇ.回数;
                s11.登録日時 = s11tokｇ.登録日時;
                s11.更新日時 = s11tokｇ.更新日時;
                s11.月集計開始日 = s11tokｇ.月集計開始日;
                s11.月集計終了日 = s11tokｇ.月集計終了日;
                s11.月次前月残高 = s11tokｇ.月次前月残高;
                s11.月次入金現金 = s11tokｇ.月次入金現金;
                s11.月次入金手形 = s11tokｇ.月次入金手形;
                s11.月次入金その他 = s11tokｇ.月次入金その他;
                s11.月次売上金額 = s11tokｇ.月次売上金額;
                s11.月次通行料 = s11tokｇ.月次通行料;
                s11.月次課税売上 = s11tokｇ.月次課税売上;
                s11.月次非課税売上 = s11tokｇ.月次非課税売上;
                s11.月次消費税 = s11tokｇ.月次消費税;
                s11.月次内傭車売上 = s11tokｇ.月次内傭車売上;
                s11.月次内傭車料 = s11tokｇ.月次内傭車料;
                s11.月次未定件数 = s11tokｇ.月次未定件数;
                s11.月次件数 = s11tokｇ.月次件数;
                s11.締日 = s11tokｇ.締日;
                
                try
                {
                    // newのエンティティに対してはAcceptChangesで新規追加となる
                    context.S11_TOKG.ApplyChanges(s11);
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
        /// S11_TOKGの更新
        /// </summary>
        /// <param name="s11tokｇ">S11_TOKG_Member</param>
        public void Update(S11_TOKG_Member s11tokg)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // トランザクションのインスタンス化(開始)
                using (var tran = new TransactionScope())
                {
                    context.Connection.Open();

                    //更新行を特定
                    var ret = from x in context.S11_TOKG
                              where (x.得意先KEY == (from tok in context.M01_TOK where tok.得意先ID == s11tokg.得意先KEY select tok.得意先KEY).FirstOrDefault()
                                    && x.集計年月 == s11tokg.集計年月 && x.回数 == s11tokg.回数)
                              select x;
                    var s11 = ret.FirstOrDefault();

                    if ((s11 != null))
                    {
                        s11.得意先KEY = (from tok in context.M01_TOK where tok.得意先ID == s11tokg.得意先KEY select tok.得意先KEY).FirstOrDefault();
                        s11.集計年月 = s11tokg.集計年月;
                        s11.回数 = s11tokg.回数;
                        s11.登録日時 = s11tokg.登録日時;
                        s11.更新日時 = s11tokg.更新日時;
                        s11.月集計開始日 = s11tokg.月集計開始日;
                        s11.月集計終了日 = s11tokg.月集計終了日;
                        s11.月次前月残高 = s11tokg.月次前月残高;
                        s11.月次入金現金 = s11tokg.月次入金現金;
                        s11.月次入金手形 = s11tokg.月次入金手形;
                        s11.月次入金その他 = s11tokg.月次入金その他;
                        s11.月次売上金額 = s11tokg.月次売上金額;
                        s11.月次通行料 = s11tokg.月次通行料;
                        s11.月次課税売上 = s11tokg.月次課税売上;
                        s11.月次非課税売上 = s11tokg.月次非課税売上;
                        s11.月次消費税 = s11tokg.月次消費税;
                        s11.月次内傭車売上 = s11tokg.月次内傭車売上;
                        s11.月次内傭車料 = s11tokg.月次内傭車料;
                        s11.月次未定件数 = s11tokg.月次未定件数;
                        s11.月次件数 = s11tokg.月次件数;
                        s11.締日 = s11tokg.締日;

                        s11.AcceptChanges();
                    }
                    else
                    {
                        context.S11_TOKG.AddObject(
                            new S11_TOKG()
                            {

                                得意先KEY = (from tok in context.M01_TOK where tok.得意先ID == s11tokg.得意先KEY select tok.得意先KEY).FirstOrDefault(),
                                集計年月 = s11tokg.集計年月,
                                回数 = s11tokg.回数,
                                登録日時 = s11tokg.登録日時,
                                更新日時 = s11tokg.更新日時,
                                月集計開始日 = s11tokg.月集計開始日,
                                月集計終了日 = s11tokg.月集計終了日,
                                月次前月残高 = s11tokg.月次前月残高,
                                月次入金現金 = s11tokg.月次入金現金,
                                月次入金手形 = s11tokg.月次入金手形,
                                月次入金その他 = s11tokg.月次入金その他,
                                月次売上金額 = s11tokg.月次売上金額,
                                月次通行料 = s11tokg.月次通行料,
                                月次課税売上 = s11tokg.月次課税売上,
                                月次非課税売上 = s11tokg.月次非課税売上,
                                月次消費税 = s11tokg.月次消費税,
                                月次内傭車売上 = s11tokg.月次内傭車売上,
                                月次内傭車料 = s11tokg.月次内傭車料,
                                月次未定件数 = s11tokg.月次未定件数,
                                月次件数 = s11tokg.月次件数,
                                締日 = s11tokg.締日,
                            }
                            );

                        context.SaveChanges();
                        //var p2 = (from x in context.S11_TOKG where x.得意先KEY == s11tokg.得意先KEY && x.集計年月 == s11tokg.集計年月 && x.回数 == s11tokg.回数 select x.得意先KEY).FirstOrDefault();
                        //s11tokg.得意先KEY = p2;
                    }

                    context.SaveChanges();

                    tran.Complete();
                }
            }
        }

        /// <summary>
        /// S11_TOKGの物理削除
        /// </summary>
        /// <param name="s11tokｇ">S11_TOKG_Member</param>
        public void Delete(int? p得意先ID, int? p集計年月, int? p回数)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.S11_TOKG
                          where (x.得意先KEY == (from tok in context.M01_TOK where tok.得意先ID == p得意先ID select tok.得意先KEY).FirstOrDefault()
                          && x.集計年月 == p集計年月 && x.回数 == p回数 )
						  orderby x.得意先KEY, x.集計年月, x.回数
                          select x;
                var s11 = ret.FirstOrDefault();

                context.DeleteObject(s11);
                context.SaveChanges();
            }
        }
        
        /// <summary>
        /// 得意先別車種別単価一覧表プレビュー用出力
        /// 得意先別車種別単価一覧表CSV用出力
        /// </summary>
        /// <returns></returns>
        /// <param name="s11tokｇ">S11_toks__Member</param>
        public List<S11_TOKG_Member_Preview_csv> GetSearchListData(string p得意先IDFrom, string p得意先IDTo, string p処理年月From, string p処理年月To, string p回数From, string p回数To, int[] i得意先List)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from s11 in context.S11_TOKG
                             from m01 in context.M01_TOK.Where(m01 => m01.得意先KEY == s11.得意先KEY)
                             //where s11.削除日付 == null

                             select new S11_TOKG_Member_Preview_csv
                           {
                               得意先KEY = m01.得意先ID,
                               得意先名 = m01.略称名,
                               集計年月 = s11.集計年月,
                               回数 = s11.回数,
                               月集計開始日 = s11.月集計開始日,
                               月集計終了日 = s11.月集計終了日,
                               月次前月残高 = s11.月次前月残高,
                               月次入金現金 = s11.月次入金手形,
                               月次入金手形 = s11.月次入金手形,
                               月次入金その他 = s11.月次入金その他,
                               月次売上金額 = s11.月次売上金額,
                               月次通行料 = s11.月次通行料,
                               月次非課税売上 = s11.月次非課税売上,
                               月次課税売上 = s11.月次課税売上,
                               月次消費税 = s11.月次消費税,
                               月次内傭車売上 = s11.月次内傭車売上,
                               月次内傭車料 = s11.月次内傭車料,
                               月次未定件数 = s11.月次未定件数,
                               月次件数 = s11.月次件数,

                           });


                if (!string.IsNullOrEmpty(p得意先IDFrom))
                {
                    int ip得意先IDFrom = AppCommon.IntParse(p得意先IDFrom);
                    query = query.Where(c => c.得意先KEY >= ip得意先IDFrom);
                }
                if (!string.IsNullOrEmpty(p得意先IDTo))
                {
                    int ip得意先IDTo = AppCommon.IntParse(p得意先IDTo);
                    query = query.Where(c => c.得意先KEY <= ip得意先IDTo);
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

                    query = query.Union(from s11 in context.S11_TOKG
                                        from m01 in context.M01_TOK.Where(m01 => m01.得意先KEY == s11.得意先KEY)
                                        where intCause.Contains(m01.得意先KEY)
                                        select new S11_TOKG_Member_Preview_csv
                                    {

                                        得意先KEY = m01.得意先ID,
                                        得意先名 = m01.略称名,
                                        集計年月 = s11.集計年月,
                                        回数 = s11.回数,
                                        月集計開始日 = s11.月集計開始日,
                                        月集計終了日 = s11.月集計終了日,
                                        月次前月残高 = s11.月次前月残高,
                                        月次入金現金 = s11.月次入金手形,
                                        月次入金手形 = s11.月次入金手形,
                                        月次入金その他 = s11.月次入金その他,
                                        月次売上金額 = s11.月次売上金額,
                                        月次通行料 = s11.月次通行料,
                                        月次非課税売上 = s11.月次非課税売上,
                                        月次課税売上 = s11.月次課税売上,
                                        月次消費税 = s11.月次消費税,
                                        月次内傭車売上 = s11.月次内傭車売上,
                                        月次内傭車料 = s11.月次内傭車料,
                                        月次未定件数 = s11.月次未定件数,
                                        月次件数 = s11.月次件数,
                                    });
                }


                //削除データ検索条件

                    query = query.OrderBy(c => (c.得意先KEY));
                
                return query.ToList();
            }
        }

    }
}
