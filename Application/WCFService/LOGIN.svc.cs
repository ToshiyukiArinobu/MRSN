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
    /// <summary>
    /// LOGIN　メンバー
    /// </summary>
    [DataContract]
    public class LOGIN_Member
    {
        [DataMember]
        public int 担当者ID { get; set; }
        [DataMember]
        public string パスワード { get; set; }
        [DataMember]
        public string 担当者名 { get; set; }
    }
    public class LOGIN
    {

        /// <summary>
        /// M09_HINのデータ取得
        /// </summary>
        /// <param name="p商品ID">商品ID</param>
        /// <returns>LOGIN_Member</returns>
        public List<LOGIN_Member> Login(string s担当者ID, string sパスワード)
        {
            int i担当者ID = int.Parse(s担当者ID);

            using (TRAC3Entities context = new TRAC3Entities())
            {
                context.Connection.Open();

                var query = (from m72 in context.M72_TNT
                             where(m72.担当者ID == i担当者ID && m72.パスワード == sパスワード)
                             select new LOGIN_Member
                             {
                                担当者名 = m72.担当者名,
                                担当者ID = m72.担当者ID,
                                パスワード = m72.パスワード,
                             }).AsQueryable();
                return query.ToList();
            }

        }
    }
}
