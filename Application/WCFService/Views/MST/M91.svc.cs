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
    public class M91 : IM91 {

        int p支払先KEY;

        /// <summary>
        /// M91_OTANのデータ取得
        /// </summary>
        /// <param name="p得意先ID">得意先ID</param>
        /// <returns>M91_OTAN_Member</returns>
        public List<M91_OTAN_Member> GetData(int? p得意先ID , DateTime? p適用開始年月日 , int pOptiion)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m91 in context.M91_OTAN
                           join m01 in context.M01_TOK on m91.支払先KEY equals m01.得意先KEY
                           where m01.得意先ID == p得意先ID
                           //where m91.削除日付 == null
                           select new M91_OTAN_Member
                           {
                               得意先ID=m01.得意先ID,
                               適用開始年月日=m91.適用開始年月日,
                               燃料単価 = m91.燃料単価,
                               登録日時 = m91.登録日時,
                               更新日時 = m91.更新日時,
                               削除日付 = m91.削除日付,
                           }).AsQueryable();

                if (p適用開始年月日 != null)
                {
                    if (pOptiion == 0)
                    {
                        ret = ret.Where(c => c.適用開始年月日 == p適用開始年月日);
                    }
                    else if (pOptiion == -1)
                    {
                        //自社IDの1つ前のIDを取得
                        ret = ret.Where(c => c.適用開始年月日 <= p適用開始年月日);
                        if (ret.Count() >= 2)
                        {
                            ret = ret.Where(c => c.適用開始年月日 < p適用開始年月日);
                        }
                        ret = ret.OrderByDescending(c => c.適用開始年月日);
                    }
                    else
                    {
                        //自社IDの1つ後のIDを取得
                        ret = ret.Where(c => c.適用開始年月日 >= p適用開始年月日);
                        if (ret.Count() >= 2)
                        {
                            ret = ret.Where(c => c.適用開始年月日 > p適用開始年月日);
                        }
                        ret = ret.OrderBy(c => c.適用開始年月日);
                    }
                }
                else
                {
                    if (pOptiion == 0)
                    {
                        ret = ret.OrderBy(c => c.適用開始年月日);
                    }
                    else if (pOptiion == 1)
                    {
                        //自社IDの最後のIDを取得
                        ret = ret.OrderByDescending(c => c.適用開始年月日);
                    }

                }

                return ret.ToList();
			}
        }
        
        /// <summary>
        /// M91_OTANの更新
        /// </summary>
        /// <param name="m91OTAN">M91_OTAN_Member</param>
        public void Update(int p得意先ID, DateTime p適用開始年月日, decimal? p燃料単価 )
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //得意先keyを特定
                var ret2 = from x in context.M01_TOK
                            where (x.得意先ID == p得意先ID)
                              select x;
                var m01 = ret2.FirstOrDefault();

                if (m01 == null)
                {
                    p支払先KEY = 0;
                }
                else
                {
                    p支払先KEY = m01.得意先KEY;
                }

                //更新行を特定
                var ret = from x in context.M91_OTAN
                          where (x.支払先KEY == p支払先KEY && x.適用開始年月日 == p適用開始年月日)
                          orderby x.支払先KEY, x.適用開始年月日
                          select x;
                var m91 = ret.FirstOrDefault();

                if (m91 == null)
                {
                    M91_OTAN m91in = new M91_OTAN();

                    m91in.支払先KEY = p支払先KEY;
                    m91in.適用開始年月日 = p適用開始年月日;
                    m91in.登録日時 = DateTime.Now;
                    m91in.更新日時 = DateTime.Now;
                    m91in.燃料単価 = p燃料単価;
                    m91in.削除日付 = null;
                    context.M91_OTAN.ApplyChanges(m91in);
                }
                else
                {
                    m91.更新日時 = DateTime.Now;
                    m91.燃料単価 = p燃料単価;
                    m91.削除日付 = null;
                    m91.AcceptChanges();
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// M91_OTANの削除
        /// </summary>
        /// <param name="m91OTAN">M91_OTAN_Member</param>
        public void Delete(int p得意先ID, DateTime p適用開始年月日)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();


                //得意先keyを特定
                var ret2 = from x in context.M01_TOK
                           where (x.得意先ID == p得意先ID)
                           select x;
                var m01 = ret2.FirstOrDefault();

                if (m01 == null)
                {
                    p支払先KEY = 0;
                }
                else
                {
                    p支払先KEY = m01.得意先KEY;
                }

                //削除行を特定
                var ret = from x in context.M91_OTAN
                          where (x.支払先KEY == p支払先KEY && x.適用開始年月日 == p適用開始年月日)
                          orderby x.支払先KEY
                          select x;
                var m91 = ret.FirstOrDefault();
                if (m91 != null)
                {
                    context.DeleteObject(m91);
                }
                context.SaveChanges();

            }
        }

        /// <summary>
        /// M91_OTANの検索データ取得
        /// </summary>
        /// <param name="p支払先ID">支払先ID</param>
        /// <returns>M91_OTAN_Member</returns>
        public List<M91_OTAN_Member> GetSearchData(int? p得意先ID, DateTime p適用開始年月日, int pOptiion)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m91 in context.M91_OTAN
                           join m01 in context.M01_TOK  on  m91.支払先KEY equals m01.得意先KEY
                        ///   where m91.削除日付 == null
                           select new M91_OTAN_Member
                           {
                               支払先KEY = m91.支払先KEY,
                               適用開始年月日= m91.適用開始年月日,
                               燃料単価 = m91.燃料単価,
                               得意先ID = m01.得意先ID,
                           }).AsQueryable();

                if (p得意先ID != null)
                {
                    if (pOptiion == 0)
                    {
                        if (-1 != p得意先ID)
                        {
                            ret = ret.Where(c => c.得意先ID == p得意先ID);
                        }
                    }
                    else if (pOptiion > 0)
                    {
                        //p支払先IDの1つ後のIDを取得
                        ret = ret.Where(c => c.得意先ID > p得意先ID);
                        ret = ret.OrderBy(c => c.得意先ID);
                    }
                    else if (pOptiion < 0)
                    {
                        //p得意先IDの1つ前のIDを取得
                        ret = ret.Where(c => c.得意先ID < p得意先ID);
                        ret = ret.OrderByDescending(c => c.得意先ID);
                    }
                }
                else
                {
                    if (pOptiion == 0)
                    {
                        //得意先IDの先頭のIDを取得
                        ret = ret.OrderBy(c => c.得意先ID);
                    }
                    else if (pOptiion == 1)
                    {
                        //得意先IDの最後のIDを取得
                        ret = ret.OrderByDescending(c => c.得意先ID);
                    }
                }


                return ret.ToList();
            }
        }

        /// <summary>
        /// 燃料単価マスタ一覧検索データ取得
        /// </summary>
        /// <returns></returns>
        public List<M91_OTAN_Member> GetSearchDataForList(string 得意先ID_FROM, string 得意先ID_TO , int[] i支払先List,  string 表示方法)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();



                var ret = (from m91 in context.M91_OTAN
                           join m01 in context.M01_TOK on m91.支払先KEY equals m01.得意先KEY
                           where m01.削除日付 == null
                           select new M91_OTAN_Member
                           {
                               得意先ID = m01.得意先ID,
                               略称名 = m01.略称名,
                               適用開始年月日 = m91.適用開始年月日,
                               燃料単価 = m91.燃料単価,
                               登録日時 = m91.登録日時,
                               更新日時 = m91.更新日時,
                               支払先KEY = m91.支払先KEY,

                               
                           }).AsQueryable();

                if (!(string.IsNullOrEmpty(得意先ID_FROM + 得意先ID_TO) && i支払先List.Length == 0))
                {

                    if (string.IsNullOrEmpty(得意先ID_FROM + 得意先ID_TO))
                    {

                        ret = ret.Where(c => c.得意先ID >= int.MaxValue);
                    }
                    if (!string.IsNullOrEmpty(得意先ID_FROM))
                    {
                        int i得意先ID_FROM = AppCommon.IntParse(得意先ID_FROM);
                        ret = ret.Where(c => c.得意先ID >= i得意先ID_FROM);
                    }
                    if (!string.IsNullOrEmpty(得意先ID_TO))
                    {
                        int i得意先ID_TO = AppCommon.IntParse(得意先ID_TO);
                        ret = ret.Where(c => c.得意先ID <= i得意先ID_TO);
                    }
                      if (i支払先List.Length > 0)
                    {
                        var intCause = i支払先List;

                        ret = ret.Union(from m91 in context.M91_OTAN
                                        join m01 in context.M01_TOK on m91.支払先KEY equals m01.得意先KEY
                                        where m01.削除日付 == null && intCause.Contains(m01.得意先ID)
                                        select new M91_OTAN_Member
                                        {
                                            得意先ID = m01.得意先ID,
                                            略称名 = m01.略称名,
                                            適用開始年月日 = m91.適用開始年月日,
                                            燃料単価 = m91.燃料単価,
                                            登録日時 = m91.登録日時,
                                            更新日時 = m91.更新日時,
                                            支払先KEY = m91.支払先KEY,
                                        });
                      }
                }
                ret = ret.Distinct();

                
                if (表示方法 == "0")
                {
                    ret = ret.OrderBy(c => c.適用開始年月日);
                }
                else
                {
                    ret = ret.OrderByDescending(c => c.適用開始年月日);
                }
                

                return ret.ToList();
            }
        }

    }
}
