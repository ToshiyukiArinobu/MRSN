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
    public class M92 : IM92 {

        /// <summary>
        /// M92_KZEIのデータ取得
        /// </summary>
        public List<M92_KZEI_Member> GetData(DateTime? p適用開始年月日, int? pオプションコード)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from M92 in context.M92_KZEI
                             select new M92_KZEI_Member
                             {
                                 適用開始年月日 = M92.適用開始年月日,
                                 軽油引取税率 = M92.軽油引取税率,
                                 削除日付 = M92.削除日付,
                             }).AsQueryable();

                if (p適用開始年月日 != null)
                {
                    if (pオプションコード == 0)
                    {
                        query = query.Where(c => c.適用開始年月日 == p適用開始年月日);
                    }
                    else if (pオプションコード == -1)
                    {
                        //自社IDの1つ前のIDを取得
                        query = query.Where(c => c.適用開始年月日 <= p適用開始年月日);
                        if (query.Count() >= 2)
                        {
                            query = query.Where(c => c.適用開始年月日 < p適用開始年月日);
                        }
                        query = query.OrderByDescending(c => c.適用開始年月日);
                    }
                    else
                    {
                        //自社IDの1つ後のIDを取得
                        query = query.Where(c => c.適用開始年月日 >= p適用開始年月日);
                        if (query.Count() >= 2)
                        {
                            query = query.Where(c => c.適用開始年月日 > p適用開始年月日);
                        }
                        query = query.OrderBy(c => c.適用開始年月日);
                    }
                }
                else
                {
                    if (pオプションコード == 0)
                    {
                        //自社IDの先頭のIDを取得
                        query = query.OrderBy(c => c.適用開始年月日);
                    }
                    else if (pオプションコード == 1)
                    {

                        query = query.Where(c => c.削除日付 == null);
                        query = query.OrderByDescending(c => c.適用開始年月日);
                        if (pオプションコード == 0)
                        {
                            query = query.OrderBy(c => c.適用開始年月日);
                        }
                    }
                }

                var ret = query.FirstOrDefault();
                List<M92_KZEI_Member> result = new List<M92_KZEI_Member>();
                if (ret != null)
                {
                    result.Add(ret);
                }
                return query.ToList();
            }
        }

        /// <summary>
        /// M92_KZEIの更新
        /// </summary>
        /// <param name="M92zei">M92_KZEI_Member</param>
        public void Update(DateTime p適用開始年月日, decimal p軽油引取税率)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                try
                {

                    //更新行を特定
                    var ret = from x in context.M92_KZEI
                              where (x.適用開始年月日 == p適用開始年月日)
                              select x;
                    var M92 = ret.FirstOrDefault();

                    if (M92 == null)
                    {
                        M92_KZEI M92Zei = new M92_KZEI();
                        M92Zei.登録日時 = DateTime.Now;
                        M92Zei.更新日時 = DateTime.Now;
                        M92Zei.適用開始年月日 = p適用開始年月日;
                        M92Zei.軽油引取税率 = p軽油引取税率;
                        context.M92_KZEI.ApplyChanges(M92Zei);
                    }
                    else
                    {
                        M92.更新日時 = DateTime.Now;
                        M92.適用開始年月日 = p適用開始年月日;
                        M92.軽油引取税率 = p軽油引取税率;
                        M92.AcceptChanges();
                    }
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
             
            }
        }

        /// <summary>
        /// M92_KZEIの物理削除
        /// </summary>
        /// <param name="M92zei">M92_KZEI_Member</param>
        public void Delete(DateTime p適用開始年月日)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M92_KZEI
                          where (x.適用開始年月日 == p適用開始年月日)
                          orderby x.適用開始年月日
                          select x;
                var M92 = ret.FirstOrDefault();

                if(M92 != null)
                {
                    context.DeleteObject(M92);
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M92_KZEIの一覧表示
        /// </summary>
        /// <summary>
        public List<M92_KZEI_Member> GetDataHinList(DateTime dDayFrom , DateTime dDayTo)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<M92_KZEI_Member> retList = new List<M92_KZEI_Member>();
                context.Connection.Open();


                //全件表示
                var query = (from M92 in context.M92_KZEI
                             select new M92_KZEI_Member
                             {
                                 登録日時 = M92.登録日時,
                                 更新日時 = M92.更新日時,
                                 適用開始年月日 = M92.適用開始年月日,
                                 軽油引取税率 = M92.軽油引取税率,
                                 削除日付 = M92.削除日付,
                             }).AsQueryable();
                query = query.Where(c => c.適用開始年月日 >= dDayFrom && c.適用開始年月日 <= dDayTo);
           
                //結果をリスト化
                retList = query.ToList();
                return retList;
            }
        }

    }
}
