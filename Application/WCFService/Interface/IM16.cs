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
    public interface IM16 {
        
        /// <summary>
        /// M16_BYSNのデータ取得
        /// </summary>
        /// <param name="p規制区分ID">規制区分ID</param>
        /// <returns>M16_BYSN_Member</returns>
        [OperationContract]
        M16_BYSN_Member GetData(int p車両ID, int p年月);

        /// <summary>
        /// M16_BYSNの新規追加
        /// </summary>
        /// <param name="m16bysn">M16_BYSN_Member</param>
        [OperationContract]
        void Insert(M16_BYSN_Member m16bysn);

        /// <summary>
        /// M16_BYSNの更新
        /// </summary>
        /// <param name="m16bysn">M16_BYSN_Member</param>
        [OperationContract]
        void Update(M16_BYSN_Member m16bysn);

        /// <summary>
        /// M16_BYSNの物理削除
        /// </summary>
        /// <param name="m16bysn">M16_BYSN_Member</param>
        [OperationContract]
        void Delete(M16_BYSN_Member m16bysn);
        
    }

    // データメンバー定義
    [DataContract]
    public class M16_BYSN_Member {
		[DataMember] public int 部門ID { get; set; }
		[DataMember] public int 年月 { get; set; }
        [DataMember] public decimal 売上予算 { get; set; }
        [DataMember] public decimal 粗利予算 { get; set; }
    }

}
