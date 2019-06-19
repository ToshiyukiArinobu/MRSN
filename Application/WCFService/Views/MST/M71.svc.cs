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
    public class M71 : IM71
    {

        public class 問合せ用
        {
            [DataMember]
            public int 自社部門ID { get; set; }
            [DataMember]
            public string 自社部門名 { get; set; }
        }

        public List<問合せ用> GetBumon()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m71 in context.M71_BUM
                             where m71.削除日付 == null
                             select new 問合せ用
                             {
                                 自社部門ID = m71.自社部門ID,
                                 自社部門名 = m71.自社部門名,
                             }).AsQueryable();

                return query.ToList();
            }
        }



        /// <summary>
        /// M71_BUMのデータ取得
        /// </summary>
        /// <param name="p自社ID">自社ID</param>
        /// <returns>M71_BUM_Member</returns>
        public List<M71_BUM_Member> GetData(int? p自社部門ID, int pオプションコード)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m71 in context.M71_BUM
                             //where m71.削除日付 == null
                             select new M71_BUM_Member
                             {
                                 自社部門ID = m71.自社部門ID,
                                 登録日時 = m71.登録日時,
                                 更新日時 = m71.更新日時,
                                 自社部門名 = m71.自社部門名,
                                 かな読み = m71.かな読み,
                                 法人ナンバー = m71.法人ナンバー,
                                 削除日付 = m71.削除日付,
                             }).AsQueryable();

                //データが1件もない状態で<< < > >>を押された時の処理
				if ((p自社部門ID == null || p自社部門ID == 0) && query.Where(c => c.削除日付 == null).Count() == 0)
                {
                    return null;
                }

                if (p自社部門ID != null)
                {
                    if (p自社部門ID == -1)
                    {
                        //全件取得
                        return query.ToList();
                    }

                    if (pオプションコード == 0)
                    {
                        query = query.Where(c => c.自社部門ID == p自社部門ID);
                    }

                    else if (pオプションコード == -1)
                    {
                        //p車種IDの1つ前のIDを取得
                        query = query.Where(c => (c.削除日付 == null));

                        query = query.Where(c => c.自社部門ID < p自社部門ID);
                        if (query.Count() >= 2)
                        {
                            query = query.Where(c => c.自社部門ID < p自社部門ID);
                        }
                        query = query.OrderByDescending(c => c.自社部門ID);
                    }
                    else
                    {
                        //p車種IDの1つ後のIDを取得
                        query = query.Where(c => (c.削除日付 == null));
                        query = query.Where(c => c.自社部門ID > p自社部門ID);

                        if (query.Count() >= 2)
                        {
                            query = query.Where(c => c.自社部門ID > p自社部門ID);
                        }
                        query = query.OrderBy(c => c.自社部門ID);
                    }
                }
                else
                {

                    if (pオプションコード == 0)
                    {
                        query = query.Where(c => (c.削除日付 == null));
                        //車種IDの先頭のIDを取得
                        query = query.OrderBy(c => c.自社部門ID);
                    }
                    else if (pオプションコード == 1)
                    {
                        query = query.Where(c => (c.削除日付 == null));
                        //車種IDの最後のIDを取得
                        query = query.OrderByDescending(c => c.自社部門ID);
                    }
                    else
                    {
                        //pオプションコード == 2
                        query = query.Where(c => (c.削除日付 == null));
                        query = query.OrderBy(c => c.自社部門ID < p自社部門ID + 1);
                    }
                }

                var ret = query.FirstOrDefault();
                List<M71_BUM_Member> result = new List<M71_BUM_Member>();
                if (ret != null)
                {
                    result.Add(ret);
                }
                return query.ToList();
            }
        }


        /// <summary>
        /// M71_BUMの更新
        /// </summary>
        /// <param name="m71BUM">M71_BUM_Member</param>
        public int Update(int p自社部門ID, string p自社部門名, string pかな読み, string p法人ナンバー, bool pMaintenanceFlg, bool pGetNextNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                if (pGetNextNumber)
                {
                    p自社部門ID = GetNextNumber();
                }

                //更新行を特定
                var ret = from x in context.M71_BUM
                          where (x.自社部門ID == p自社部門ID)
                          orderby x.自社部門ID
                          select x;
                var m71 = ret.FirstOrDefault();

                if (m71 == null)
                {
                    M71_BUM m71j = new M71_BUM();
                    m71j.自社部門ID = p自社部門ID;
                    m71j.登録日時 = DateTime.Now;
                    m71j.更新日時 = DateTime.Now;
                    m71j.自社部門名 = p自社部門名;
                    m71j.かな読み = pかな読み;
                    m71j.法人ナンバー = p法人ナンバー;
                    m71j.削除日付 = null;
                    context.M71_BUM.ApplyChanges(m71j);
                }
                else
                {
                    if (pMaintenanceFlg)
                    {
                        return -1;
                    }

                    m71.自社部門ID = p自社部門ID;
                    m71.更新日時 = DateTime.Now;
                    m71.自社部門名 = p自社部門名;
                    m71.かな読み = pかな読み;
                    m71.法人ナンバー = p法人ナンバー;
                    m71.削除日付 = null;
                    m71.AcceptChanges();
                }
                context.SaveChanges();
            }
            return 1;
        }

        /// <summary>
        /// M71_BUMの論理削除
        /// </summary>
        /// <param name="m71BUM">M71_BUM_Member</param>
        public void Delete(int p自社部門ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M71_BUM
                          where (x.自社部門ID == p自社部門ID)
                          orderby x.自社部門ID
                          select x;
                var m71 = ret.FirstOrDefault();
                if (m71 != null)
                {
                    m71.削除日付 = DateTime.Now;
                    m71.AcceptChanges();
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M71_BUMのID自動採番
        /// </summary>
        /// <returns></returns>
        public int GetNextNumber()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //最大ID行を特定
                var query = from x in context.M71_BUM
                            select x.自社部門ID;

                int iMaxID;
                if (query.Count() == 0)
                {
                    iMaxID = 0;
                }
                else
                {
                    iMaxID = query.Max();
                }

                return iMaxID + 1;
            }
        }

        /// <summary>
        /// M71_BUMの検索データ取得
        /// </summary>
        /// <param name="p発着地ID">自社部門ID</param>
        /// <returns>M71_BUM_Member</returns>
        public List<M71_BUM_Search_Member> GetSearchData(int? p自社部門ID, int pOptiion)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m71 in context.M71_BUM
                           where m71.削除日付 == null
                           select new M71_BUM_Search_Member
                           {
                               自社部門ID = m71.自社部門ID,
                               自社部門名 = m71.自社部門名,
                               かな読み = m71.かな読み,
                           }).AsQueryable();

                if (p自社部門ID != null)
                {
                    if (pOptiion == 0)
                    {
                        if (-1 != p自社部門ID)
                        {
                            ret = ret.Where(c => c.自社部門ID == p自社部門ID);
                        }
                    }
                }


                return ret.ToList();
            }
        }

        /// <summary>
        /// 自社部門マスタ一覧検索データ取得
        /// </summary>
        /// <returns></returns>
        public List<M71_BUM_Member> GetSearchDataForList(string 自社部門コードFROM, string 自社部門コードTO, int[] i自社部門IDList, string 表示方法)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();


                var ret = (from m71 in context.M71_BUM
                           where m71.削除日付 == null
                           select new M71_BUM_Member
                           {
                               自社部門ID = m71.自社部門ID,
                               登録日時 = m71.登録日時,
                               更新日時 = m71.更新日時,
                               自社部門名 = m71.自社部門名,
                               かな読み = m71.かな読み,
                               法人ナンバー = m71.法人ナンバー,
                           }).AsQueryable();

                if (!(string.IsNullOrEmpty(自社部門コードFROM + 自社部門コードTO) && i自社部門IDList.Length == 0))
                {

                    if (string.IsNullOrEmpty(自社部門コードFROM + 自社部門コードTO))
                    {

                        ret = ret.Where(c => c.自社部門ID >= int.MaxValue);
                    }
                    if (!string.IsNullOrEmpty(自社部門コードFROM))
                    {
                        int i自社部門コードFROM = AppCommon.IntParse(自社部門コードFROM);
                        ret = ret.Where(c => c.自社部門ID >= i自社部門コードFROM);
                    }
                    if (!string.IsNullOrEmpty(自社部門コードTO))
                    {
                        int i自社部門コードTO = AppCommon.IntParse(自社部門コードTO);
                        ret = ret.Where(c => c.自社部門ID <= i自社部門コードTO);
                    }

                    if (i自社部門IDList.Length > 0)
                    {
                        var intCause = i自社部門IDList;

                        ret = ret.Union(from m71 in context.M71_BUM
                                        where m71.削除日付 == null && intCause.Contains(m71.自社部門ID)
                                        select new M71_BUM_Member
                                        {
                                            自社部門ID = m71.自社部門ID,
                                            登録日時 = m71.登録日時,
                                            更新日時 = m71.更新日時,
                                            自社部門名 = m71.自社部門名,
                                            かな読み = m71.かな読み,
                                            法人ナンバー = m71.法人ナンバー,
                                        });
                    }

                }

                ret = ret.Distinct();

                if (表示方法 == "0")
                {
                    ret.OrderBy(c => c.自社部門ID);
                }
                else
                {
                    ret.OrderBy(c => c.かな読み);
                }


                return ret.ToList();
            }
        }

    }
}
