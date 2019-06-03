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
    public interface IM78 {

    }

    // データメンバー定義
    [DataContract]
    public class M78_SYK_Member {
		[DataMember] public int 出勤区分ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
        [DataMember] public string 出勤区分名 { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
    }

    [DataContract]
    public class M78_SYK_All
    {
        public int 出勤区分ID { get; set; }
        public DateTime? 登録日時 { get; set; }
        public DateTime? 更新日時 { get; set; }
        public string 出勤区分名 { get; set; }
        public DateTime? 削除日付 { get; set; }
    }


}
