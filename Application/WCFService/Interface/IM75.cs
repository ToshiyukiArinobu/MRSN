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
    public interface IM75 {
        
        /// <summary>
        /// M75_SKKのデータ取得
        /// </summary>
        /// <param name="p表示順ID">表示順ID</param>
        /// <returns>M75_SKK_Member</returns>
        [OperationContract]
        M75_SKK_Member GetData(int p表示順ID);

        /// <summary>
        /// M75_SKKの新規追加
        /// </summary>
        /// <param name="M75skk">M75_SKK_Member</param>
        [OperationContract]
        void Insert(M75_SKK_Member m75skk);

        /// <summary>
        /// M75_SKKの更新
        /// </summary>
        /// <param name="M75skk">M75_SKK_Member</param>
        [OperationContract]
        void Update(M75_SKK_Member m75skk);

        /// <summary>
        /// M75_SKKの物理削除
        /// </summary>
        /// <param name="M75skk">M75_SKK_Member</param>
        [OperationContract]
        void Delete(M75_SKK_Member M75skk);
        
    }

    // データメンバー定義
    [DataContract]
    public class M75_SKK_Member {
		[DataMember] public int 表示順ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public int? 経費項目ID { get; set; }
		[DataMember] public int? 支払先ID { get; set; }
	}

}
