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
    public interface IM79 {

        /// <summary>
        /// M79_ZKBのデータ取得
        /// </summary>
        /// <param name="p出勤区分ID">出勤区分ID</param>
        /// <returns>M79_ZKB_Member</returns>
        [OperationContract]
        M79_ZKB_Member GetData(int p出勤区分ID);

        /// <summary>
        /// M79_ZKBの新規追加
        /// </summary>
        /// <param name="m79zkb">M79_ZKB_Member</param>
        [OperationContract]
        void Insert(M79_ZKB_Member m79zkb);

        /// <summary>
        /// M79_ZKBの更新
        /// </summary>
        /// <param name="m79zkb">M79_ZKB_Member</param>
        [OperationContract]
        void Update(M79_ZKB_Member m79zkb);

        /// <summary>
        /// M79_ZKBの物理削除
        /// </summary>
        /// <param name="m79zkb">M79_ZKB_Member</param>
        [OperationContract]
        void Delete(M79_ZKB_Member M75skk);
        
    }

    // データメンバー定義
    [DataContract]
    public class M79_ZKB_Member {
		[DataMember] public int 出勤区分ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public string 税区分 { get; set; }
	}

}
