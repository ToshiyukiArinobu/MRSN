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
    public interface IM50 {
        
    

    }

    // データメンバー定義
    [DataContract]
    public class M50_RTBL_Member {
        [DataMember] public int? タリフID { get; set; }
		[DataMember] public int 距離 { get; set; }
		[DataMember] public int 重量 { get; set; }
        [DataMember] public int? 運賃 { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
	}

    // データメンバー定義
    [DataContract]
    public class M50_RTBL_Member1
    {
        [DataMember]
        public int? タリフID { get; set; }
        [DataMember]
        public int 距離 { get; set; }
    }

    // データメンバー定義
    [DataContract]
    public class M50_RTBL_Member2
    {
        [DataMember]
        public int? タリフID { get; set; }
        [DataMember]
        public int 重量 { get; set; }
    }

}
