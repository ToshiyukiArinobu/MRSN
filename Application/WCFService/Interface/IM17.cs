using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace KyoeiSystem.Application.WCFService
{
    // アクセスインターフェース定義
    [ServiceContract]
    public interface IM17 {
        
        /// <summary>
        /// M17_CYSNのデータ取得
        /// </summary>
        /// <param name="p規制区分ID">規制区分ID</param>
        /// <returns>M17_CYSN_Member</returns>
        [OperationContract]
        M17_CYSN_Member GetData(int p車両ID, int p年月);

        /// <summary>
        /// M17_CYSNの新規追加
        /// </summary>
        /// <param name="m17cysn">M17_CYSN_Member</param>
        [OperationContract]
        void Insert(M17_CYSN_Member m17cysn);

        /// <summary>
        /// M17_CYSNの更新
        /// </summary>
        /// <param name="m17cysn">M17_CYSN_Member</param>
        [OperationContract]
        void Update(M17_CYSN_Member m17cysn);

        /// <summary>
        /// M17_CYSNの物理削除
        /// </summary>
        /// <param name="m17cysn">M17_CYSN_Member</param>
        [OperationContract]
        void Delete(M17_CYSN_Member m17cysn);
        
    }

    // データメンバー定義
    [DataContract]
    public class M17_CYSN_Member {
		[DataMember] public int 車両KEY { get; set; }
		[DataMember] public int 年月 { get; set; }
        [DataMember] public decimal 売上予算 { get; set; }
        [DataMember] public decimal 粗利予算 { get; set; }
    }

}
