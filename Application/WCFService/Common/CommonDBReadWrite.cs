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
using KyoeiSystem.Framework.Common;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 20150807 wada add 共通DBアクセスクラス
    /// </summary>
	public class CommonDBReadWrite
	{
        // ライセンス認証用メンバ
        public class License_Member
		{
            public int? 顧客コード { get; set; }
            public string ユーザーID { get; set; }
            public string パスワード { get; set; }
            public int? ログインフラグ { get; set; }
            public DateTime? アクセス時間 { get; set; }
            public string DB接続先 { get; set; }
            public string ユーザーDB { get; set; }
            public string DBログインID { get; set; }
            public string DBパスワード { get; set; }
            public DateTime? 開始日 { get; set; }
            public DateTime? 有効期限 { get; set; }
            public DateTime? 登録日 { get; set; }
		}

        //お知らせ情報取得
        public class Message_Member
        {
            public string メッセージ { get; set; }
        }

        /// <summary>
        /// ライセンス認証用ログイン処理
        /// </summary>
        /// <param name="pユーザーID">ユーザーID</param>
        /// <returns>License_Member</returns>
        public List<License_Member> LicenseLogin(string pユーザーID)
		{
            try
            {
                using (COMMONDBEntities context = new COMMONDBEntities(CommonData.COMMON_GetConnectionString()))
                {
                    context.Connection.Open();

                    // まずはユーザーが存在するかどうかをチェックする。
                    var ret = (from m in context.COMMON_TABLE
                               where m.ユーザーID == pユーザーID
                               select new License_Member
                               {
                                   顧客コード = m.顧客コード,
                                   ユーザーID = m.ユーザーID,
                                   パスワード = m.パスワード,
                                   ログインフラグ = m.ログインフラグ,
                                   アクセス時間 = m.アクセス時間,
                                   DB接続先 = m.DB接続先,
                                   ユーザーDB = m.ユーザーDB,
                                   DBログインID = m.DBログインID,
                                   DBパスワード = m.DBパスワード,
                                   開始日 = m.開始日,
                                   有効期限 = m.有効期限,
                                   登録日 = m.登録日
                               }).FirstOrDefault();
                    if (ret != null)
                    {
                        // 次にログインフラグがONになっていないか、ONになっていても一定時間(5分)が経過しているかを
                        // チェックする。
                        var checkDate = DateTime.Now.AddMinutes(-5);
                        var accessDate = ret.アクセス時間;
                        bool result = false;

                        if (ret.ログインフラグ == 0)
                        {
                            result = true;
                        }
                        else if (ret.ログインフラグ == 1 && accessDate < checkDate)
                        {
                            result = true;
                        }
                        if (!result)
                        {
                            //データが該当しなければ【-1：対象ユーザーはログイン処理中】を返す
                            ret.顧客コード = -1;
                            return new List<License_Member>() { ret };
                            throw new Framework.Common.DBDataNotFoundException();
                        }
                        else
                        {
                            // 最後に開始日、有効期限が範囲内にあるかをチェックする。
                            var nowDate = DateTime.Now;
                            var startDate = ret.開始日;
                            var limitDate = ret.有効期限;
                            bool result2 = false;

                            if (startDate <= nowDate && limitDate >= nowDate)
                            {
                                result2 = true;
                            }
                            if (!result2)
                            {
                                //データが該当しなければ【-2：対象ユーザーは使用可能な状態になっていません。】を返す
                                ret.顧客コード = -2;
                                return new List<License_Member>() { ret };
                                throw new Framework.Common.DBDataNotFoundException();
                            }
                            else
                            {
                                // 20150909 wada modify フラグを立てるのはライセンスでないLOGIN画面の
                                // 描画完了時に変更
                                //// ログインフラグを立てる。
                                //updateLogin(pユーザーID);
                                return new List<License_Member>() { ret };
                            }
                        }
                    }
                    else
                    {
                        //データが該当しなければ【-99999：対象データなし】を返す
                        License_Member nullData = new License_Member()
                        {
                            顧客コード = -99999,
                        };
                        return new List<License_Member>() { nullData };
                        throw new Framework.Common.DBDataNotFoundException();
                    }
                }
            }
            catch (EntityException ex)
            {
                //データが該当しなければ【-90001：DB接続エラー】を返す
                License_Member nullData = new License_Member()
                {
                    顧客コード = -90001,
                };
                return new List<License_Member>() { nullData };
            }
		}


        public void updateAccessDateTime(string pユーザーID)
        {
            using (COMMONDBEntities context = new COMMONDBEntities(CommonData.COMMON_GetConnectionString()))
            {
                context.Connection.Open();
                var nowDateTime = DateTime.Now;

                var ret = (from x in context.COMMON_TABLE
                           where x.ユーザーID == pユーザーID
                            select x).FirstOrDefault();
                ret.アクセス時間 = nowDateTime;

                ret.AcceptChanges();
                context.SaveChanges();
            }
        }

        public void updateLogin(string pユーザーID)
        {
            using (COMMONDBEntities context = new COMMONDBEntities(CommonData.COMMON_GetConnectionString()))
            {
                context.Connection.Open();
                var nowDateTime = DateTime.Now;

                var ret = (from x in context.COMMON_TABLE
                           where x.ユーザーID == pユーザーID
                           select x).FirstOrDefault();
                if (ret != null)
                {
                    ret.ログインフラグ = 1;
                    ret.アクセス時間 = nowDateTime;
                    ret.AcceptChanges();
                    context.SaveChanges();
                }
            }
        }

        public void updateLogout(string pユーザーID)
        {
            using (COMMONDBEntities context = new COMMONDBEntities(CommonData.COMMON_GetConnectionString()))
            {
                context.Connection.Open();
                var nowDateTime = DateTime.Now;

                var ret = (from x in context.COMMON_TABLE
                           where x.ユーザーID == pユーザーID
                           select x).FirstOrDefault();
                if (ret != null)
                {
                    ret.ログインフラグ = 0;
                    ret.AcceptChanges();
                    context.SaveChanges();
                }
            }
        }


        public List<Message_Member> MessgeShow(string pLicenseID)
        {

            using (COMMONDBEntities context = new COMMONDBEntities(CommonData.COMMON_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from x in context.COMMON_TABLE
                           where x.ユーザーID == pLicenseID
                           select new Message_Member
                           {
                               メッセージ = x.メッセージ1,
                           }).FirstOrDefault();

                return new List<Message_Member>() { ret };

            }

        }

    

	}
}