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
    public class M15SERIES
    {
        //メンバー
        public class M15_SERIES_Member
        {
            public string シリーズコード { get; set; }
            public string シリーズ名 { get; set; }
            public string かな読み { get; set; }
            public int? 登録担当者 { get; set; }
            public int? 更新担当者 { get; set; }
            public DateTime? 登録日時 { get; set; }
            public DateTime? 更新日時 { get; set; }
            public DateTime? 削除日時 { get; set; }
        }

        //F1検索メンバー
        public class M15_SERIES_Member_SCH
        {
            public string シリーズコード { get; set; }
            public string シリーズ名 { get; set; }
            public string かな読み { get; set; }
        }


        /// <summary>
        /// M15_SERIESのデータ取得
        /// </summary>
        /// <param name="担当者ID">担当者ID</param>
        /// <returns>M15_SERIES_Member</returns>
        public List<M15_SERIES_Member> GetData(string pシリーズコード)
        {

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from M15 in context.M15_SERIES
                           where M15.シリーズコード == pシリーズコード
                           select new M15_SERIES_Member

                           {
                               シリーズコード = M15.シリーズコード,
                               シリーズ名 = M15.シリーズ名,
                               かな読み = M15.かな読み,
                               登録日時 = M15.登録日時,
                               更新日時 = M15.最終更新日時,
                               削除日時 = M15.削除日時,
                           }).AsQueryable();
                return ret.ToList();
            }
        }


        /// <summary>
        /// M15_SERIESの更新
        /// </summary>
        /// <param name="M15">M15_SERIES_Member</param>
        public int Update(string pシリーズコード, string pシリーズ名, string pかな読み, int ユーザーID, bool pMaintenanceFlg, bool pGetNextNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //更新行を特定
                var ret = from x in context.M15_SERIES
                          where (x.シリーズコード == pシリーズコード)
                          orderby x.シリーズコード
                          select x;
                var M15 = ret.FirstOrDefault();

                if (M15 == null)
                {
                    M15_SERIES M15Sir = new M15_SERIES();
                    M15Sir.シリーズコード = pシリーズコード;
                    M15Sir.シリーズ名 = pシリーズ名;
                    M15Sir.かな読み = pかな読み;
                    M15Sir.登録者 = ユーザーID;
                    M15Sir.最終更新者 = ユーザーID;
                    M15Sir.登録日時 = DateTime.Now;
                    M15Sir.最終更新日時 = DateTime.Now;
                    M15Sir.削除者 = null;
                    M15Sir.削除日時 = null;

                    context.M15_SERIES.ApplyChanges(M15Sir);
                }
                else
                {
                    if (pMaintenanceFlg)
                    {
                        return -1;
                    }
                    M15.シリーズコード = pシリーズコード;
                    M15.シリーズ名 = pシリーズ名;
                    M15.かな読み = pかな読み;
                    M15.最終更新者 = ユーザーID;
                    M15.最終更新日時 = DateTime.Now;
                    M15.削除者 = null;
                    M15.削除日時 = null;
                    M15.AcceptChanges();
                }

                context.SaveChanges();
            }
            return 1;
        }

        /// <summary>
        /// M15_SERIESの削除
        /// </summary>
        /// <param name="m08tik">M15_SERIES_Member</param>
        public void Delete(string pシリーズコード, int pDeleteUserID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M15_SERIES
                          where (x.シリーズコード == pシリーズコード)
                          orderby x.シリーズコード
                          select x;
                var M15 = ret.FirstOrDefault();
                if (M15 != null)
                {
                    M15.削除日時 = DateTime.Now;
                    M15.削除者 = pDeleteUserID;
                    M15.AcceptChanges();
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M15_SERIESの検索データ取得
        /// </summary>
        /// <param name="p担当者ID">担当者ID</param>
        /// <returns>M15_SERIES_Member</returns>
        public List<M15_SERIES_Member_SCH> GetDataSCH(string pシリーズコード, int pOptiion)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from M15 in context.M15_SERIES
                           where M15.削除日時 == null
                           select new M15_SERIES_Member_SCH
                           {
                               シリーズコード = M15.シリーズコード,
                               シリーズ名 = M15.シリーズ名,
                               かな読み = M15.かな読み,
                           }).AsQueryable();

                if (pシリーズコード != null)
                {
                    if (pOptiion == 0)
                    {
                        ret = ret.Where(c => c.シリーズコード.StartsWith(pシリーズコード));
                    }
                }


                return ret.ToList();
            }
        }

        /// <summary>
        /// シリーズマスタ一覧検索データ取得
        /// </summary>
        /// <returns></returns>
        public List<M15_SERIES_Member> GetSearchDataForList(string シリーズコードFROM, string シリーズコードTO, string シリーズ名指定, string 表示方法)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m15 in context.M15_SERIES
                           where m15.削除日時 == null
                           select new M15_SERIES_Member
                           {
                               シリーズコード = m15.シリーズコード,
                               シリーズ名 = m15.シリーズ名,
                               かな読み = m15.かな読み,
                               登録担当者 = m15.登録者,
                               更新担当者 = m15.最終更新者,
                               登録日時 = m15.登録日時,
                               更新日時 = m15.最終更新日時,
                               削除日時 = m15.削除日時,
                           }).AsQueryable();

                if (!(string.IsNullOrEmpty(シリーズコードFROM + シリーズコードTO) && string.IsNullOrEmpty(シリーズ名指定)))
                {
                    if (!string.IsNullOrEmpty(シリーズコードFROM))
                    {
                        ret = ret.Where(c => c.シリーズコード.CompareTo(シリーズコードFROM) >= 0);
                    }
                    if (!string.IsNullOrEmpty(シリーズコードTO))
                    {
                        ret = ret.Where(c => c.シリーズコード.CompareTo(シリーズコードTO) <= 0);
                    }

                    if (!string.IsNullOrEmpty(シリーズ名指定))
                    {
                        ret = ret.Where(c => c.シリーズ名.Contains(シリーズ名指定));
                    }
                }

                ret = ret.Distinct();

                if (表示方法 == "0")
                {
                    ret = ret.OrderBy(c => c.シリーズコード);
                }
                else
                {
                    ret = ret.OrderBy(c => c.かな読み);
                }

                return ret.ToList();
            }
        }

    }
}
