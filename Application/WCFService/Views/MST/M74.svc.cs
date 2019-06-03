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
    public class M74
    {
        //メンバー
        public class M74_KGRP_DISP_Member
        {
            public int グループ権限ID { get; set; }
            public string プログラムID { get; set; }
            public bool 使用可能FLG { get; set; }
            public bool データ更新FLG { get; set; }
            public DateTime? 登録日時 { get; set; }
            public DateTime? 更新日時 { get; set; }
        
        }

        //メンバー
        public class M74_KGRP_Member
        {
            public int グループ権限ID { get; set; }
            public string プログラムID { get; set; }
            public bool 使用可能FLG { get; set; }
            public bool データ更新FLG { get; set; }
            public DateTime? 登録日時 { get; set; }
            public DateTime? 更新日時 { get; set; }
            public DateTime? 削除日付 { get; set; }
        }

        /// <summary>
        /// 更新メンバクラス
        /// </summary>
        /// <remarks>
        /// 画面側で保持する全ての項目を定義
        /// </remarks>
        public class M74_KGRP_UPD_Menber
        {
            public int? TabID { get; set; }
            public string メニュー名称 { get; set; }
            public int No { get; set; }
            public string プログラム名称 { get; set; }
            public int グループ権限ID { get; set; }
            public string プログラムID { get; set; }
            public bool 使用可能FLG { get; set; }
            public bool データ更新FLG { get; set; }
            public DateTime? 登録日時 { get; set; }
            public DateTime? 更新日時 { get; set; }
        }

        /// <summary>
        /// 権限グループ名検索用
        /// </summary>
        public List<M74_KGRP_NAME> GetDataSCH()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var retIdList = (from m74 in context.M74_KGRP_NAME
                                 select m74
                           ).ToList();

                return retIdList;
            }
        }

        /// <summary>
        /// 新規ID取得
        /// </summary>
        /// <returns></returns>
        public int GetNewID()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var nameList = context.M74_KGRP_NAME;

                int result = 0;
                if (nameList.Any())
                    result = nameList.Max(m => m.グループ権限ID) + 1;

                return result;

            }

        }

        /// <summary>
        /// M74_KGRPのデータ取得
        /// </summary>
        /// <param name="p適用開始日付">適用開始日付</param>
        /// <returns>M74_KGRP_Member</returns>
        public List<M74_KGRP_DISP_Member> GetData(int pグループ権限ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var resultList =
                    context.M74_KGRP
                        .Where(w =>
                            w.削除日付 == null &&
                            w.グループ権限ID == pグループ権限ID)
                        .Select(s => new M74_KGRP_DISP_Member
                            {
                                グループ権限ID = s.グループ権限ID,
                                プログラムID = s.プログラムID,
                                使用可能FLG = s.使用可能FLG == 1 ? true : false,
                                データ更新FLG = s.データ更新FLG == 1 ? true : false,
                                登録日時 = s.登録日時,
                                更新日時 = s.更新日時,
                            })
                        .OrderBy(o => o.グループ権限ID)
                        .ToList();

                return resultList;

            }

        }

        /// <summary>
        /// M74_KGRPの新規追加
        /// </summary>
        /// <param name="M74kgrp">M74_KGRP_Member</param>
        //public void Insert(M74_KGRP_Member M74kgrp)
        //{
        //    using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
        //    {
        //        context.Connection.Open();

        //        M74_KGRP m74 = new M74_KGRP();
        //        m74.グループ権限ID = M74kgrp.グループ権限ID;
        //        m74.登録日時 = M74kgrp.登録日時;
        //        m74.更新日時 = M74kgrp.更新日時;

        //        try
        //        {
        //            // newﾉｴﾝﾃｨﾃｨﾆ対ｼﾃﾊAcceptChangesﾃﾞ新規追加ﾄﾅﾙ
        //            context.M74_KGRP.ApplyChanges(m74);
        //            context.SaveChanges();
        //        }
        //        catch (UpdateException ex)
        //        {
        //            // PKey違反等
        //            Console.WriteLine(ex);
        //        }
        //    }
        //}

        /// <summary>
        /// M74_KGRPの更新
        /// </summary>
        /// <param name="M74kgrp">M74_KGRP_Member</param>
        //public void Update(M74_KGRP_Member M74kgrp)
        //{
        //    using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
        //    {
        //        context.Connection.Open();

        //        //更新行ｦ特定
        //        var m74 =
        //            context.M74_KGRP
        //                .Where(w => w.グループ権限ID == M74kgrp.グループ権限ID)
        //                .FirstOrDefault();

        //        m74.グループ権限ID = M74kgrp.グループ権限ID;
        //        m74.登録日時 = M74kgrp.登録日時;
        //        m74.更新日時 = DateTime.Now;

        //        m74.AcceptChanges();
        //        context.SaveChanges();
        //    }
        //}

        /// <summary>
        /// M74_KGRPの物理削除
        /// </summary>
        /// <param name="M74kgrp">M74_KGRP_Member</param>
        //public void Delete(M74_KGRP_Member M74kgrp)
        //{
        //    using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
        //    {
        //        context.Connection.Open();

        //        // 削除行を特定
        //        var m74 =
        //            context.M74_KGRP
        //                .Where(w => w.グループ権限ID == M74kgrp.グループ権限ID)
        //                .FirstOrDefault();

        //        context.DeleteObject(m74);
        //        context.SaveChanges();

        //    }

        //}

        /// <summary>
        /// 権限マスタの更新
        /// 削除→追加(Delete→Insert)
        /// </summary>
        /// <param name="M74kgrp"></param>
        public void UPD_Data(List<M74_KGRP_UPD_Menber> pmM74, int pグループ権限ID, string pM74KGRPName)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // データ削除
                using (var tran = new TransactionScope())
                {
                    string sql = string.Empty;
                    sql = string.Format("DELETE M74_KGRP WHERE M74_KGRP.グループ権限ID = {0} ", pグループ権限ID);
                    int count = context.ExecuteStoreCommand(sql);
                    tran.Complete();
                }

                #region 権限グループ名マスタ登録・更新
                //var M74Name = (from x in context.M74_KGRP_NAME
                //              where x.グループ権限ID == pグループ権限ID
                //              select x).FirstOrDefault();

                var M74Name =
                    context.M74_KGRP_NAME
                        .Where(w => w.グループ権限ID == pグループ権限ID)
                        .FirstOrDefault();

                if (M74Name != null)
                {
                    //Update
                    M74Name.グループ権限名 = pM74KGRPName;

                    M74Name.AcceptChanges();
                    context.SaveChanges();
                }
                else
                {
                    //Insert

                    M74_KGRP_NAME m74ins = new M74_KGRP_NAME()
                    {
                        グループ権限ID = pグループ権限ID,
                        グループ権限名 = pM74KGRPName,
                    };

                    try
                    {
                        // newのエンティティに対してはAcceptChangesで新規追加となる
                        context.M74_KGRP_NAME.ApplyChanges(m74ins);
                        context.SaveChanges();
                    }
                    catch (UpdateException ex)
                    {
                        // PKey違反等
                        Console.WriteLine(ex);
                    }
                }
                #endregion

                #region 権限マスタ登録・更新
                try
                {
                    // 新規追加
                    foreach (M74_KGRP_UPD_Menber mM74 in pmM74)
                    {
                        M74_KGRP m74ins = new M74_KGRP()
                        {
                            グループ権限ID = mM74.グループ権限ID,
                            プログラムID = mM74.プログラムID,
                            タブグループ番号 = mM74.TabID,
                            使用可能FLG = mM74.使用可能FLG == true ? 1 : 0,
                            データ更新FLG = mM74.データ更新FLG == true ? 1 : 0,
                            登録日時 = DateTime.Today,
                            更新日時 = DateTime.Today,
                            削除日付 = null
                        };

                        // newのエンティティに対してはAcceptChangesで新規追加となる
                        context.M74_KGRP.ApplyChanges(m74ins);

                    }

                }
                catch (UpdateException ex)
                {
                    // PKey違反等
                    Console.WriteLine(ex);
                    throw ex;
                }

                context.SaveChanges();


                #endregion

            }

        }

    }
}
