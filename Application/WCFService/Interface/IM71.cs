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
    public interface IM71 {
     
    }

    // データメンバー定義
    [DataContract]
    public class M71_BUM_Member {
		[DataMember] public int 自社部門ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public string 自社部門名 { get; set; }
		[DataMember] public string かな読み { get; set; }
        [DataMember] public string 法人ナンバー { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }

	}

    [DataContract]
    public class M71_BUM_Search_Member
    {
        [DataMember]
        public int 自社部門ID { get; set; }
        [DataMember]
        public string 自社部門名 { get; set; }
        [DataMember]
        public string かな読み { get; set; }
    }


}
