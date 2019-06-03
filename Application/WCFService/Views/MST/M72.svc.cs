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
using KyoeiSystem.Application.WCFService;


namespace KyoeiSystem.Application.WCFService
{
    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コード、svc、および config ファイルで同時にクラス名 "DataDriveLogService" を変更できます。
    // 注意: このサービスをテストするために WCF テスト クライアントを起動するには、ソリューション エクスプローラーで DataDriveLogService.svc または DataDriveLogService.svc.cs を選択し、デバッグを開始してください。
    public class M72
    {

        //メンバー
        public class M72_TNT_Member
        {
            public int 担当者ID { get; set; }
            public string 担当者名 { get; set; }
            public string かな読み { get; set; }
            public string パスワード { get; set; }
            public int グループ権限ID { get; set; }
            public string 個人ナンバー { get; set; }
            public int? 自社コード { get; set; }
            public string 自社名 { get; set; }
            public string 設定項目 { get; set; }
            public int? 登録担当者 { get; set; }
            public int? 更新担当者 { get; set; }
            public DateTime? 登録日時 { get; set; }
            public DateTime? 更新日時 { get; set; }
            public DateTime? 削除日時 { get; set; }
        }

        //メンバー
        public class M72_TNT_Member_List
        {
            public int 担当者ID { get; set; }
            public string 担当者名 { get; set; }
            public string かな読み { get; set; }
            public string パスワード { get; set; }
            public int グループ権限ID { get; set; }
            public string 個人ナンバー { get; set; }
            public int? 自社コード { get; set; }
            public string 自社名 { get; set; }
            public string 設定項目 { get; set; }
        }

        //F1検索メンバー
        public class M72_TNT_Member_SCH
        {
            public int? 担当者ID { get; set; }
            public string 担当者名 { get; set; }
            public string かな読み { get; set; }
        }

        /// <summary>
        /// M72_TNTのデータ取得
        /// </summary>
        /// <param name="担当者ID">担当者ID</param>
        /// <param name="pオプション">
        ///   id=null option=-1:先頭データ取得
        ///   -1:前データ取得
        ///    0:指定コード取得
        ///    1:次データ取得
        ///    id=null option=1:最終データ取得
        /// </param>
        /// <returns>M72_TNT_Member</returns>
        public List<M72_TNT_Member> GetData(string 担当者ID, int pOptiion)
        {
            int init;
            int? p担当者ID = null;
            if (int.TryParse(担当者ID, out init) == true)
            {
                p担当者ID = init;
            }

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m72 in context.M72_TNT.Where(c => c.担当者ID < CommonConstants.SUPECIAL_USER_ID)
                           from m70 in context.M70_JIS.Where(c => (c.自社コード == m72.自社コード) && (c.削除日時 == null)).DefaultIfEmpty()
                           //where m72.削除日付 == null
                           select new M72_TNT_Member
                           {
                               担当者ID = m72.担当者ID,
                               登録日時 = m72.登録日時,
                               更新日時 = m72.最終更新日時,
                               担当者名 = m72.担当者名,
                               かな読み = m72.かな読み,
                               パスワード = m72.パスワード,
                               グループ権限ID = m72.グループ権限ID,
                               個人ナンバー = m72.個人ナンバー,
                               自社コード = m72.自社コード,
                               自社名 = m70.自社名,
                               設定項目 = null,
                               削除日時 = m72.削除日時,
                           }).AsQueryable();

                //データが1件もない状態で<< < > >>を押された時の処理
                if ((p担当者ID == null || p担当者ID == 0) && ret.Where(c => c.削除日時 == null).Count() == 0)
                {
                    return null;
                }

                if (p担当者ID != null)
                {
                    if (p担当者ID == -1)
                    {
                        //全件取得
                        return ret.ToList();
                    }

                    if (pOptiion == 0)
                    {
                        ret = ret.Where(c => c.担当者ID == p担当者ID);
                    }

                    else if (pOptiion == -1)
                    {
                        //p担当者IDの1つ前のIDを取得
                        ret = ret.Where(c => (c.削除日時 == null));

                        ret = ret.Where(c => c.担当者ID < p担当者ID);
                        if (ret.Count() >= 2)
                        {
                            ret = ret.Where(c => c.担当者ID < p担当者ID);
                        }
                        ret = ret.OrderByDescending(c => c.担当者ID);
                    }
                    else
                    {
                        //p担当者IDの1つ後のIDを取得
                        ret = ret.Where(c => (c.削除日時 == null));
                        ret = ret.Where(c => c.担当者ID > p担当者ID);

                        if (ret.Count() >= 2)
                        {
                            ret = ret.Where(c => c.担当者ID > p担当者ID);
                        }
                        ret = ret.OrderBy(c => c.担当者ID);
                    }
                }
                else
                {
                    if (pOptiion == 0)
                    {
                        ret = ret.Where(c => (c.削除日時 == null));
                        //担当者IDの先頭のIDを取得
                        ret = ret.OrderBy(c => c.担当者ID);
                    }
                    else if (pOptiion == 1)
                    {
                        ret = ret.Where(c => (c.削除日時 == null));
                        //担当者IDの最後のIDを取得
                        ret = ret.OrderByDescending(c => c.担当者ID);
                    }
                    else
                    {
                        //pオプションコード == 2
                        ret = ret.Where(c => (c.削除日時 == null));
                        ret = ret.OrderBy(c => c.担当者ID < p担当者ID + 1);
                    }
                }
                return ret.ToList();
            }
        }

        /// <summary>
        /// M72_TNTの更新
        /// </summary>
        /// <param name="m72tnt">M72_TNT_Member</param>
        public int Update(int p担当者ID, string p担当者名, string pかな読み, string pパスワード, int pグループ権限ID,
             int p自社コード, string p個人ナンバー, int ユーザーID, bool pMaintenanceFlg, bool pGetNextNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                if (pGetNextNumber)
                {
                    p担当者ID = GetNextNumber();
                }

                if (pグループ権限ID != 0)
                {
                    int k_cnt = (from x in context.M72_TNT where x.担当者ID != p担当者ID && x.担当者ID != CommonConstants.SUPECIAL_USER_ID && x.グループ権限ID == 0 select x).Count();
                    if (k_cnt < 1)
                    {
                        return -9;
                    }
                }

                //更新行を特定
                var ret = from x in context.M72_TNT
                          where (x.担当者ID == p担当者ID)
                          orderby x.担当者ID
                          select x;
                var m72 = ret.FirstOrDefault();

                if (m72 == null)
                {
                    M72_TNT m72in = new M72_TNT();
                    m72in.担当者ID = p担当者ID;
                    m72in.担当者名 = p担当者名;
                    m72in.かな読み = pかな読み;
                    m72in.パスワード = pパスワード;
                    m72in.グループ権限ID = pグループ権限ID;
                    m72in.個人ナンバー = p個人ナンバー;
                    m72in.自社コード = p自社コード;
                    m72in.登録者 = ユーザーID;
                    m72in.登録日時 = DateTime.Now;
                    m72in.最終更新者 = ユーザーID;
                    m72in.最終更新日時 = DateTime.Now;
                    m72in.削除者 = null;
                    m72in.削除日時 = null;

                    context.M72_TNT.ApplyChanges(m72in);
                }
                else
                {
                    if (pMaintenanceFlg)
                    {
                        return -1;
                    }

                    m72.担当者ID = p担当者ID;
                    m72.担当者名 = p担当者名;
                    m72.かな読み = pかな読み;
                    m72.パスワード = pパスワード;
                    m72.グループ権限ID = pグループ権限ID;
                    m72.個人ナンバー = p個人ナンバー;
                    m72.自社コード = p自社コード;
                    m72.最終更新者 = ユーザーID;
                    m72.最終更新日時 = DateTime.Now;
                    m72.削除者 = null;
                    m72.削除日時 = null;
                    m72.AcceptChanges();
                }
                context.SaveChanges();
            }
            return 1;
        }

        /// <summary>
        /// M72_TNTの削除
        /// </summary>
        /// <param name="m08tik">M72_TNT_Member</param>
        public int Delete(int p担当者ID, int ユーザーID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int k_cnt = (from x in context.M72_TNT where x.担当者ID != p担当者ID && x.担当者ID != CommonConstants.SUPECIAL_USER_ID && x.グループ権限ID == 0 select x).Count();
                if (k_cnt < 1)
                {
                    return -9;
                }

                //削除行を特定
                var ret = from x in context.M72_TNT
                          where (x.担当者ID == p担当者ID)
                          orderby x.担当者ID
                          select x;
                var m72 = ret.FirstOrDefault();
                if (m72 != null)
                {
                    m72.削除者 = ユーザーID;
                    m72.削除日時 = DateTime.Now;
                    m72.AcceptChanges();
                }
                context.SaveChanges();
                return 0;
            }
        }

        /// <summary>
        /// M72_TNTのID自動採番
        /// </summary>
        /// <returns></returns>
        public int GetNextNumber()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //最大ID行を特定
                var query = from x in context.M72_TNT
                            select x.担当者ID;

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
        /// M72_TNTの検索データ取得
        /// </summary>
        /// <param name="p担当者ID">担当者ID</param>
        /// <returns>M72_TNT_Member</returns>
        public List<M72_TNT_Member_SCH> GetDataSCH(int? p担当者ID, int pOptiion)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m72 in context.M72_TNT
                           where m72.削除日時 == null
                           select new M72_TNT_Member_SCH
                           {
                               担当者ID = m72.担当者ID,
                               担当者名 = m72.担当者名,
                               かな読み = m72.かな読み,
                           }).AsQueryable();

                if (p担当者ID != null)
                {
                    if (pOptiion == 0)
                    {
                        if (-1 != p担当者ID)
                        {
                            ret = ret.Where(c => c.担当者ID == p担当者ID);
                        }
                    }
                }
                return ret.ToList();
            }
        }

        /// <summary>
        /// 担当者マスタ一覧検索データ取得
        /// </summary>
        /// <returns></returns>
        public List<M72_TNT_Member> GetSearchDataForList(string 担当者コードFROM, string 担当者コードTO, string 担当者指定, string 表示方法)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret =
                    context.M72_TNT
                        .Where(w => w.削除日時 == null && w.担当者ID < CommonConstants.SUPECIAL_USER_ID)
                        .Select(s => new M72_TNT_Member
                            {
                                担当者ID = s.担当者ID,
                                担当者名 = s.担当者名,
                                かな読み = s.かな読み,
                                パスワード = s.パスワード,
                                グループ権限ID = s.グループ権限ID,
                                個人ナンバー = s.個人ナンバー,
                                自社コード = s.自社コード,
                                登録日時 = s.登録日時,
                                更新日時 = s.最終更新日時,
                            });

                if (!string.IsNullOrEmpty(担当者コードFROM))
                {
                    int i担当者コードFROM = AppCommon.IntParse(担当者コードFROM);
                    ret = ret.Where(c => c.担当者ID >= i担当者コードFROM);
                }

                if (!string.IsNullOrEmpty(担当者コードTO))
                {
                    int i担当者コードTO = AppCommon.IntParse(担当者コードTO);
                    ret = ret.Where(c => c.担当者ID <= i担当者コードTO);
                }

                if (!string.IsNullOrEmpty(担当者指定))
                    ret = ret.Where(c => c.担当者名.Contains(担当者指定));

                if (表示方法 == "0")
                    ret = ret.OrderBy(c => c.担当者ID);

                else
                    ret = ret.OrderBy(c => c.かな読み);

                return ret.ToList();

            }

        }

    }
}

