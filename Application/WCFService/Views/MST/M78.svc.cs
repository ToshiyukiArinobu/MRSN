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
    public class M78 : IM78 {

        /// <summary>
        /// M78_SYKのデータ取得
        /// </summary>
        /// <param name="p出勤区分ID">出勤区分ID</param>
        /// <returns>M78_SYK_Member</returns>
        public M78_SYK_Member GetData(int p出勤区分ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m78 in context.M78_SYK
                           where m78.出勤区分ID == p出勤区分ID
                           orderby m78.出勤区分ID
                           select new M78_SYK_Member
                           {
                               出勤区分ID = m78.出勤区分ID,
                               登録日時 = m78.登録日時,
                               更新日時 = m78.更新日時,
                               出勤区分名 = m78.出勤区分名,
                               削除日付 = m78.削除日付,
                           });
                return ret.FirstOrDefault();
            }
        }

        /// <summary>
        /// M78_SYKのリストデータ取得
        /// </summary>
        /// <param name="p出勤区分ID">出勤区分ID</param>
        /// <returns>M78_SYK_Member</returns>
        public List<M78_SYK_All> GetAllData()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from syk in context.M78_SYK
                           where syk.削除日付 == null
                           orderby syk.出勤区分ID
                           select new M78_SYK_All()
                           {
                               出勤区分ID = syk.出勤区分ID,
                               出勤区分名 = syk.出勤区分名,
                               登録日時 = syk.登録日時,
                               更新日時 = syk.更新日時,
                               削除日付 = syk.削除日付,
                           }
                           ).ToList();
				for (int cnt = ret.Count() ; cnt < 15; cnt++)
				{
					ret.Add(new M78_SYK_All() { 出勤区分ID = cnt, 出勤区分名 = ""});
				}
                return ret;
            }
        }

        /// <summary>
        /// M78_SYKのリスト登録
        /// </summary>
        /// <param name="p出勤区分ID">出勤区分ID</param>
        /// <returns>M78_SYK_Member</returns>
        public void GetListSYK(List<M78_SYK_Member> m78syk)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // トランザクションのインスタンス化(開始)
                using (var tran = new TransactionScope())
                {

                    context.Connection.Open();

                    #region M78_SYK(出勤区分)
                    var ret1 = (from x in context.M78_SYK
                                select x).ToList();
                    foreach (var rec in ret1)
                    {
                        context.DeleteObject(rec);
                    }
                    int no = 1;
                    foreach (M78_SYK_Member syk in m78syk.OrderBy(x => x.出勤区分ID))
                    {
                        var dat = new M78_SYK()
                        {
                            出勤区分ID = syk.出勤区分ID,
                            出勤区分名 = syk.出勤区分名,
                            登録日時 = syk.登録日時,
                            更新日時 = DateTime.Now,
                            削除日付 = null
                        };
                        context.M78_SYK.ApplyChanges(dat);

                    }
                    context.SaveChanges();

                    tran.Complete();
                }
                #endregion

            }
        }

        /// <summary>
        /// M78_SYKの新規追加
        /// </summary>
        /// <param name="m78syk">M78_SYK_Member</param>
        public void Insert(M78_SYK_Member m78syk)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                M78_SYK m78 = new M78_SYK();
                m78.出勤区分ID = m78syk.出勤区分ID;
                m78.登録日時 = m78syk.登録日時;
                m78.更新日時 = m78syk.更新日時;
                m78.出勤区分名 = m78syk.出勤区分名;
                try
                {
                    // newﾉｴﾝﾃｨﾃｨﾆ対ｼﾃﾊAcceptChangesﾃﾞ新規追加ﾄﾅﾙ
                    context.M78_SYK.ApplyChanges(m78);
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
        /// M78_SYKの更新
        /// </summary>
        /// <param name="m78syk">M78_SYK_Member</param>
        public void Update(M78_SYK_Member m78syk)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //更新行ｦ特定
                var ret = from x in context.M78_SYK
                          where (x.出勤区分ID == m78syk.出勤区分ID)
                          orderby x.出勤区分ID
                          select x;
                var m78 = ret.FirstOrDefault();
                m78.出勤区分ID = m78syk.出勤区分ID;
                m78.登録日時 = m78syk.登録日時;
                m78.更新日時 = DateTime.Now;
                m78.出勤区分名 = m78syk.出勤区分名;

                m78.AcceptChanges();
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M78_SYKの物理削除
        /// </summary>
        /// <param name="m78syk">M78_SYK_Member</param>
        public void Delete(M78_SYK_Member M75skk)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M78_SYK
                          where (x.出勤区分ID == M75skk.出勤区分ID)
                          orderby x.出勤区分ID
                          select x;
                var m78 = ret.FirstOrDefault();

                context.DeleteObject(m78);
                context.SaveChanges();
            }
        }

    }
}
