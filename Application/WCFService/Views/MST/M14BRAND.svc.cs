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
    public class M14BRAND
    {
        //メンバー
        public class M14_BRAND_Member
        {
            public string ブランドコード { get; set; }
            public string ブランド名 { get; set; }
            public string かな読み { get; set; }
            public int? 登録担当者 { get; set; }
            public int? 更新担当者 { get; set; }
            public DateTime? 登録日時 { get; set; }
            public DateTime? 更新日時 { get; set; }
            public DateTime? 削除日時 { get; set; }
        }

        //F1検索メンバー
        public class M14_BRAND_Member_SCH
        {
            public string ブランドコード { get; set; }
            public string ブランド名 { get; set; }
            public string かな読み { get; set; }
        }


        /// <summary>
        /// M14_BRANDのデータ取得
        /// </summary>
        /// <param name="担当者ID">担当者ID</param>
        /// <returns>M14_BRAND_Member</returns>
        public List<M14_BRAND_Member> GetData(string pブランドコード)
        {

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m14 in context.M14_BRAND
                           where m14.ブランドコード == pブランドコード
                           select new M14_BRAND_Member

                           {
                               ブランドコード = m14.ブランドコード,
                               ブランド名 = m14.ブランド名,
                               かな読み = m14.かな読み,
                               登録日時 = m14.登録日時,
                               更新日時 = m14.最終更新日時,
                               削除日時 = m14.削除日時,
                           }).AsQueryable();
                return ret.ToList();
            }
        }

        /// <summary>
        /// M14_BRANDの更新
        /// </summary>
        /// <param name="m14">M14_BRAND_Member</param>
        public int Update(string pブランドコード, string pブランド名, string pかな読み, int ユーザーID, bool pMaintenanceFlg, bool pGetNextNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //if (pGetNextNumber)
                //{
                //    pブランドコード = GetNextNumber();
                //}

                //更新行を特定
                var ret = from x in context.M14_BRAND
                          where (x.ブランドコード == pブランドコード)
                          orderby x.ブランドコード
                          select x;
                var m14 = ret.FirstOrDefault();

                if (m14 == null)
                {
                    M14_BRAND m14Brd = new M14_BRAND();
                    m14Brd.ブランドコード = pブランドコード;
                    m14Brd.ブランド名 = pブランド名;
                    m14Brd.かな読み = pかな読み;
                    m14Brd.登録者 = ユーザーID;
                    m14Brd.最終更新者 = ユーザーID;
                    m14Brd.登録日時 = DateTime.Now;
                    m14Brd.最終更新日時 = DateTime.Now;
                    m14Brd.削除者 = null;
                    m14Brd.削除日時 = null;

                    context.M14_BRAND.ApplyChanges(m14Brd);
                }
                else
                {
                    if (pMaintenanceFlg)
                    {
                        return -1;
                    }
                    m14.ブランドコード = pブランドコード;
                    m14.ブランド名 = pブランド名;
                    m14.かな読み = pかな読み;
                    m14.最終更新者 = ユーザーID;
                    m14.最終更新日時 = DateTime.Now;
                    m14.削除者 = null;
                    m14.削除日時 = null;
                    m14.AcceptChanges();
                }

                context.SaveChanges();
            }
            return 1;
        }

        /// <summary>
        /// M14_BRANDの削除
        /// </summary>
        /// <param name="m08tik">M14_BRAND_Member</param>
        public void Delete(string pブランドコード, int pDeleteUserID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M14_BRAND
                          where (x.ブランドコード == pブランドコード)
                          orderby x.ブランドコード
                          select x;
                var m14 = ret.FirstOrDefault();
                if (m14 != null)
                {
                    m14.削除日時 = DateTime.Now;
                    m14.削除者 = pDeleteUserID;
                    m14.AcceptChanges();
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M14_BRANDの検索データ取得
        /// </summary>
        /// <param name="p担当者ID">担当者ID</param>
        /// <returns>M14_BRAND_Member</returns>
        public List<M14_BRAND_Member_SCH> GetDataSCH(string pブランドコード, int pOptiion)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m14 in context.M14_BRAND
                           where m14.削除日時 == null
                           select new M14_BRAND_Member_SCH
                           {
                               ブランドコード = m14.ブランドコード,
                               ブランド名 = m14.ブランド名,
                               かな読み = m14.かな読み,
                           }).AsQueryable();

                if (pブランドコード != null)
                {
                    if (pOptiion == 0)
                    {
                        ret = ret.Where(c => c.ブランドコード.StartsWith(pブランドコード));
                    }
                }


                return ret.ToList();
            }
        }

        /// <summary>
        /// ブランドマスタ一覧検索データ取得
        /// </summary>
        /// <returns></returns>
        public List<M14_BRAND_Member> GetSearchDataForList(string ブランドコードFROM, string ブランドコードTO, string ブランド名指定, string 表示方法)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m14 in context.M14_BRAND
                           where m14.削除日時 == null
                           select new M14_BRAND_Member
                           {
                               ブランドコード = m14.ブランドコード,
                               ブランド名 = m14.ブランド名,
                               かな読み = m14.かな読み,
                               登録担当者 = m14.登録者,
                               更新担当者 = m14.最終更新者,
                               登録日時 = m14.登録日時,
                               更新日時 = m14.最終更新日時,
                               削除日時 = m14.削除日時,
                           }).AsQueryable();

                if (!(string.IsNullOrEmpty(ブランドコードFROM + ブランドコードTO) && string.IsNullOrEmpty(ブランド名指定)))
                {
                    if (!string.IsNullOrEmpty(ブランドコードFROM))
                    {
                        ret = ret.Where(c => c.ブランドコード.CompareTo(ブランドコードFROM) >= 0);
                    }
                    if (!string.IsNullOrEmpty(ブランドコードTO))
                    {
                        ret = ret.Where(c => c.ブランドコード.CompareTo(ブランドコードTO) <= 0);
                    }

                    if (!string.IsNullOrEmpty(ブランド名指定))
                    {

                        ret = ret.Where(c => c.ブランド名.Contains(ブランド名指定));
                    }
                }

                ret = ret.Distinct();

                if (表示方法 == "0")
                {
                    ret = ret.OrderBy(c => c.ブランドコード);
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
