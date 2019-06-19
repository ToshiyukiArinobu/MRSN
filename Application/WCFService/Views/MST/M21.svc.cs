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
    public class M21
    {

        /// <summary>
        /// 出荷先マスタ 表示項目定義クラス
        /// </summary>
        public class M21_SYUK_Member
        {
            [DataMember]
            public string 出荷先コード { get; set; }
            [DataMember]
            public string 出荷先名１ { get; set; }
            [DataMember]
            public string 出荷先名２ { get; set; }
            [DataMember]
            public string 出荷先カナ { get; set; }
            [DataMember]
            public string 出荷先郵便番号 { get; set; }
            [DataMember]
            public string 出荷先住所１ { get; set; }
            [DataMember]
            public string 出荷先住所２ { get; set; }
            [DataMember]
            public string 出荷先電話番号 { get; set; }
            [DataMember]
            public string 備考１ { get; set; }
            [DataMember]
            public string 備考２ { get; set; }
            //[DataMember]
            //public int? 論理削除 { get; set; }
            [DataMember]
            public DateTime? 削除日時 { get; set; }
            [DataMember]
            public string 削除者 { get; set; }
            [DataMember]
            public DateTime? 登録日時 { get; set; }
            [DataMember]
            public string 登録者 { get; set; }
            [DataMember]
            public DateTime? 最終更新日時 { get; set; }
            [DataMember]
            public string 最終更新者 { get; set; }
        }

        /// <summary>
        /// 出荷先マスタリスト 表示項目定義クラス
        /// </summary>
        public class M21_SYUK_Search_Member
        {
            [DataMember]
            public string 出荷先コード { get; set; }
            [DataMember]
            public string 出荷先名１ { get; set; }
            [DataMember]
            public string 出荷先名２ { get; set; }
            [DataMember]
            public string 出荷先カナ { get; set; }
            [DataMember]
            public string 出荷先郵便番号 { get; set; }
            [DataMember]
            public string 出荷先住所１ { get; set; }
            [DataMember]
            public string 出荷先住所２ { get; set; }
            [DataMember]
            public string 出荷先電話番号 { get; set; }
            [DataMember]
            public string 備考１ { get; set; }
            [DataMember]
            public string 備考２ { get; set; }
            //[DataMember]
            //public int? 論理削除 { get; set; }
        }

        /// <summary>
        /// 出荷先マスタ リスト出力用クラス
        /// </summary>
        public class M21_SYUK_List_Member
        {
            [DataMember]
            public string 出荷先コード { get; set; }
            [DataMember]
            public string 出荷先名１ { get; set; }
            [DataMember]
            public string 出荷先名２ { get; set; }
            [DataMember]
            public string 出荷先カナ { get; set; }
            [DataMember]
            public string 出荷先郵便番号 { get; set; }
            [DataMember]
            public string 出荷先住所１ { get; set; }
            [DataMember]
            public string 出荷先住所２ { get; set; }
            [DataMember]
            public string 出荷先電話番号 { get; set; }
            [DataMember]
            public string 備考１ { get; set; }
            [DataMember]
            public string 備考２ { get; set; }
            //[DataMember]
            //public int? 論理削除 { get; set; }
            [DataMember]
            public DateTime? 削除日時 { get; set; }
            [DataMember]
            public int? 削除者 { get; set; }
            [DataMember]
            public DateTime? 登録日時 { get; set; }
            [DataMember]
            public int? 登録者 { get; set; }
            [DataMember]
            public DateTime? 最終更新日時 { get; set; }
            [DataMember]
            public int? 最終更新者 { get; set; }
        }

        /// <summary>
        /// M21_SYUKのデータ取得
        /// </summary>
        /// <param name="担当者ID">担当者ID</param>
        /// <returns>M21_SYUK_Member</returns>
        public List<M21_SYUK_Member> GetData(string p出荷先コード, int pOptiion)
        {

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

            var ret = (from m21 in context.M21_SYUK
                           from m72_IN in context.M72_TNT.Where(c => c.担当者ID == m21.登録者).DefaultIfEmpty()
                           from m72_DEL in context.M72_TNT.Where(c => c.担当者ID == m21.削除者).DefaultIfEmpty()
                           from m72_UP in context.M72_TNT.Where(c => c.担当者ID == m21.最終更新者).DefaultIfEmpty()
                       where m21.出荷先コード == p出荷先コード
                           //where m21.削除日付 == null
                           select new M21_SYUK_Member
                           {
                               出荷先コード = m21.出荷先コード,
                               出荷先名１ = m21.出荷先名１,
                               出荷先名２ = m21.出荷先名２,
                               出荷先カナ = m21.出荷先カナ,
                               出荷先郵便番号 = m21.出荷先郵便番号,
                               出荷先住所１ = m21.出荷先住所１,
                               出荷先住所２ = m21.出荷先住所２,
                               出荷先電話番号 = m21.出荷先電話番号,
                               備考１ = m21.備考１,
                               備考２ = m21.備考２,
                               //論理削除 = m21.論理削除,
                               削除日時 = m21.削除日時,
                               削除者 = m72_DEL.担当者名,
                               登録日時 = m21.登録日時,
                               登録者 = m72_IN.担当者名 ,
                               最終更新日時 = m21.最終更新日時,
                               最終更新者 = m72_UP.担当者名,
                           }).AsQueryable();
                return ret.ToList();
            }
        }

        /* キーがvarcharの時ページング不要
        /// <summary>
        /// M21_SYUKのデータ取得
        /// </summary>
        /// <param name="p出荷先コード">p出荷先コード</param>
        /// <returns>M21_SYUK_Member</returns>
        public List<M21_SYUK_Member> GetPagingCode(string p出荷先コード, int pOptiion)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                
                var ret = (from m21 in context.M21_SYUK
                           from m72_IN in context.M72_TNT.Where(c => c.担当者ID == m21.登録者).DefaultIfEmpty()
                           from m72_DEL in context.M72_TNT.Where(c => c.担当者ID == m21.削除者).DefaultIfEmpty()
                           from m72_UP in context.M72_TNT.Where(c => c.担当者ID == m21.最終更新者).DefaultIfEmpty()
                           //where m21.削除日付 == null
                           select new M21_SYUK_Member
                           {
                               出荷先コード = m21.出荷先コード,
                               出荷先名１ = m21.出荷先名１,
                               出荷先名２ = m21.出荷先名２,
                               出荷先カナ = m21.出荷先カナ,
                               出荷先郵便番号 = m21.出荷先郵便番号,
                               出荷先住所１ = m21.出荷先住所１,
                               出荷先住所２ = m21.出荷先住所２,
                               出荷先電話番号 = m21.出荷先電話番号,
                               備考１ = m21.備考１,
                               備考２ = m21.備考２,
                               //論理削除 = m21.論理削除,
                               削除日時 = m21.削除日時,
                               削除者名 = m72_DEL.担当者名,
                               登録日時 = m21.登録日時,
                               登録者名 = m72_IN.担当者名 ,
                               最終更新日時 = m21.最終更新日時,
                               最終更新者名 = m72_UP.担当者名,
                           }).AsQueryable();

                //データが1件もない状態で<< < > >>を押された時の処理
                if ((p出荷先コード == null) && ret.Where(c => c.削除日時 == null).Count() == 0)
                {
                    return null;
                }

                if (p出荷先コード != null)
                {
                    if (pOptiion == 0)
                    {

                        ret = ret.Where(c => c.出荷先コード == null);
                        ret = ret.Where(c => c.出荷先コード == p出荷先コード);

                    }
                    else if (pOptiion > 0)
                    {
                        //p出荷先コードの1つ後のIDを取得
                        ret = ret.Where(c => c.削除日時 == null);
                        ret = ret.Where(c => c.出荷先コード.CompareTo(p出荷先コード) > 0);
                        ret = ret.OrderBy(c => c.出荷先コード);
                    }
                    else if (pOptiion < 0)
                    {
                        //p出荷先コードの1つ前のIDを取得
                        ret = ret.Where(c => c.削除日時 == null);
                        ret = ret.Where(c => c.出荷先コード.CompareTo(p出荷先コード) < 0);
                        ret = ret.OrderByDescending(c => c.出荷先コード);
                    }
                }
                else
                {
                    if (pOptiion == 0)
                    {
                        //出荷先コードの先頭のIDを取得
                        ret = ret.Where(c => c.削除日時 == null);
                        ret = ret.OrderBy(c => c.出荷先コード);
                    }
                    else if (pOptiion == 1)
                    {
                        //出荷先コードの最後のIDを取得
                        ret = ret.Where(c => c.削除日時 == null);
                        ret = ret.OrderByDescending(c => c.出荷先コード);
                    }
                }

                return ret.ToList();
            }
        }
         */

        /// <summary>
        /// M21_SYUKの更新
        /// </summary>
        /// <param name="m21tik">M21_SYUK_Member</param>
        public int Update(M21_SYUK_Member upDateData, int ユーザID, bool pMaintenanceFlg, bool pGetNextNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //if (pGetNextNumber)
                //{
                //    p出荷先コード = GetNextNumber();
                //}

               
                //DateTime? p削除日時 = null;
                //int? p削除者 = null;
                //論理削除が9の場合の削除日、削除者を設定する
                //if (upDateData.論理削除 == 9)
                //{
                //    p削除日時 = DateTime.Now;
                //        p削除者 = ユーザID;
                //}


                //更新行を特定
                var ret = from x in context.M21_SYUK
                          where (x.出荷先コード == upDateData.出荷先コード)
                          orderby x.出荷先コード
                          select x;
                var m21 = ret.FirstOrDefault();

                if (m21 == null)
                {
                    M21_SYUK m21in = new M21_SYUK();
                    m21in.出荷先コード = upDateData.出荷先コード;
                    m21in.出荷先名１ = upDateData.出荷先名１;
                    m21in.出荷先名２ = upDateData.出荷先名２;
                    m21in.出荷先カナ = upDateData.出荷先カナ;
                    m21in.出荷先郵便番号 = upDateData.出荷先郵便番号;
                    m21in.出荷先住所１ = upDateData.出荷先住所１;
                    m21in.出荷先住所２ = upDateData.出荷先住所２;
                    m21in.出荷先電話番号 = upDateData.出荷先電話番号;
                    m21in.備考１ = upDateData.備考１;
                    m21in.備考２ = upDateData.備考２;
                    //m21in.論理削除 = upDateData.論理削除;
                    m21in.削除日時 = null;
                    m21in.削除者 = null;
                    m21in.登録日時 = DateTime.Now;
                    m21in.登録者 = ユーザID;
                    m21in.最終更新日時 = DateTime.Now;
                    m21in.最終更新者 = ユーザID;
                    context.M21_SYUK.ApplyChanges(m21in);
                }
                else
                {
                    if (pMaintenanceFlg)
                    {
                        return -1;
                    }

                    m21.出荷先コード = upDateData.出荷先コード;
                    m21.出荷先名１ = upDateData.出荷先名１;
                    m21.出荷先名２ = upDateData.出荷先名２;
                    m21.出荷先カナ = upDateData.出荷先カナ;
                    m21.出荷先郵便番号 = upDateData.出荷先郵便番号;
                    m21.出荷先住所１ = upDateData.出荷先住所１;
                    m21.出荷先住所２ = upDateData.出荷先住所２;
                    m21.出荷先電話番号 = upDateData.出荷先電話番号;
                    m21.備考１ = upDateData.備考１;
                    m21.備考２ = upDateData.備考２;
                    //m21.論理削除 = upDateData.論理削除;
                    m21.削除日時 = null;
                    m21.削除者 = null;
                    m21.最終更新日時 = DateTime.Now;
                    m21.最終更新者 = ユーザID;
                    m21.AcceptChanges();
                }

                context.SaveChanges();
            }
            return 1;
        }

        /// <summary>
        /// M21_SYUKの削除
        /// </summary>
        /// <param name="m21tik">M21_SYUK_Member</param>
        public void Delete(string p出荷先コード, int ユーザID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M21_SYUK
                          where (x.出荷先コード == p出荷先コード)
                          orderby x.出荷先コード
                          select x;
                var m21 = ret.FirstOrDefault();
                if (m21 != null)
                {
                    m21.削除日時 = DateTime.Now;
                    m21.削除者 = ユーザID;
                    m21.AcceptChanges();
                }
                context.SaveChanges();
            }
        }

        ///// <summary>
        ///// M21_SYUKのID自動採番
        ///// </summary>
        ///// <returns></returns>
        //public int GetNextNumber()
        //{
        //    using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
        //    {
        //        context.Connection.Open();

        //        //最大ID行を特定
        //        var query = from x in context.M21_SYUK
        //                    select x.出荷先コード;

        //        int iMaxID;
        //        if (query.Count() == 0)
        //        {
        //            iMaxID = 0;
        //        }
        //        else
        //        {
        //            iMaxID = query.Max();
        //        }

        //        return iMaxID + 1;
        //    }
        //}

        /// <summary>
        /// M21_SYUKの検索データ取得
        /// </summary>
        /// <param name="p摘要ID">摘要ID</param>
        /// <returns>M21_SYUK_Member</returns>
        public List<M21_SYUK_Search_Member> GetSearchData(string p出荷先コード, int pOptiion)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m21 in context.M21_SYUK
                           from m72_IN in context.M72_TNT.Where(c => c.担当者ID == m21.登録者).DefaultIfEmpty()
                           from m72_DEL in context.M72_TNT.Where(c => c.担当者ID == m21.削除者).DefaultIfEmpty()
                           from m72_UP in context.M72_TNT.Where(c => c.担当者ID == m21.最終更新者).DefaultIfEmpty()
                           where m21.削除日時 == null
                           select new M21_SYUK_Search_Member
                           {
                               出荷先コード = m21.出荷先コード,
                               出荷先名１ = m21.出荷先名１,
                               出荷先名２ = m21.出荷先名２,
                               出荷先カナ = m21.出荷先カナ,
                               出荷先郵便番号 = m21.出荷先郵便番号,
                               出荷先住所１ = m21.出荷先住所１,
                               出荷先住所２ = m21.出荷先住所２,
                               出荷先電話番号 = m21.出荷先電話番号,
                               備考１ = m21.備考１,
                               備考２ = m21.備考２,
                               //論理削除 = m21.論理削除,
                               //削除日時 = m21.削除日時,
                               //削除者 = m72_DEL.担当者名,
                               //登録日時 = m21.登録日時,
                               //登録者 = m72_IN.担当者名,
                               //最終更新日時 = m21.最終更新日時,
                               //最終更新者 = m72_UP.担当者名,
                           }).AsQueryable();

                if (!string.IsNullOrEmpty(p出荷先コード))
                {
                    if (pOptiion == 0)
                    {
                        //ret = ret.Where(c => c.出荷先コード == p出荷先コード);
                        ret = ret.Where(c => c.出荷先コード.StartsWith(p出荷先コード));
                    }
                }


                return ret.ToList();
            }
        }

        /// <summary>
        /// 出荷先マスタ一覧検索データ取得
        /// </summary>
        /// <returns></returns>
        public List<M21_SYUK_List_Member> GetSearchDataForList(string 出荷先コードFROM, string 出荷先コードTO, string 出荷先名指定, string 表示方法)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();


                var ret = (from m21 in context.M21_SYUK
                           where m21.削除日時 == null
                           select new M21_SYUK_List_Member
                           {
                               出荷先コード = m21.出荷先コード,
                               出荷先名１ = m21.出荷先名１,
                               出荷先名２ = m21.出荷先名２,
                               出荷先カナ = m21.出荷先カナ,
                               出荷先郵便番号 = m21.出荷先郵便番号,
                               出荷先住所１ = m21.出荷先住所１,
                               出荷先住所２ = m21.出荷先住所２,
                               出荷先電話番号 = m21.出荷先電話番号,
                               備考１ = m21.備考１,
                               備考２ = m21.備考２,
                               削除日時 = m21.削除日時,
                               削除者 = m21.削除者,
                               登録日時 = m21.登録日時,
                               登録者 = m21.登録者,
                               最終更新日時 = m21.最終更新日時,
                               最終更新者 = m21.最終更新者,
                               
                           }).AsQueryable();

                var retList = ret;

                if (!(string.IsNullOrEmpty(出荷先コードFROM + 出荷先コードTO) && string.IsNullOrEmpty(出荷先名指定)))
                {
                    if (!string.IsNullOrEmpty(出荷先コードFROM))
                    {
                        //int i出荷先コードFROM = AppCommon.IntParse(出荷先コードFROM);
                        ret = ret.Where(c => c.出荷先コード .CompareTo(出荷先コードFROM) >= 0);
                        retList = ret;
                    }
                    if (!string.IsNullOrEmpty(出荷先コードTO))
                    {
                        //int i出荷先コードTO = AppCommon.IntParse(出荷先コードTO);
                        ret = ret.Where(c => c.出荷先コード.CompareTo(出荷先コードTO) <= 0);
                        retList = ret;
                    }


                    if (!string.IsNullOrEmpty(出荷先名指定))
                    {
                        ret = ret.Where(c => c.出荷先名１.Contains(出荷先名指定));
                        ret =ret.Union(retList.Where(c => c.出荷先名２.Contains(出荷先名指定)));
                    }

                }

                ret = ret.Distinct();

                if (表示方法 == "0")
                {
                    ret = ret.OrderBy(c => c.出荷先コード);
                }
                else
                {
                    ret = ret.OrderBy(c => c.出荷先カナ);
                }


                return ret.ToList();
            }
        }

    }
}
