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
    public interface IM81 {
        
        /// <summary>
        /// M81_OYKのデータ取得
        /// </summary>
        /// <param name="p親子区分ID">親子区分ID</param>
        /// <returns>M81_OYK_Member</returns>
        [OperationContract]
        M81_OYK_Member GetData(int p親子区分ID);

        /// <summary>
        /// M81_OYKの新規追加
        /// </summary>
        /// <param name="m81oyk">M81_OYK_Member</param>
        [OperationContract]
        void Insert(M81_OYK_Member m81oyk);

        /// <summary>
        /// M81_OYKの更新
        /// </summary>
        /// <param name="m81oyk">M81_OYK_Member</param>
        [OperationContract]
        void Update(M81_OYK_Member m81oyk);

        /// <summary>
        /// M81_OYKの物理削除
        /// </summary>
        /// <param name="m81oyk">M81_OYK_Member</param>
        [OperationContract]
        void Delete(M81_OYK_Member m81oyk);
    }

    // データメンバー定義
    [DataContract]
    public class M81_OYK_Member {
		[DataMember] public int 親子区分ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public string 親子区分 { get; set; }
	}

}
