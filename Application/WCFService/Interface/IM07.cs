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
    public interface IM07 {


        
    }

    // データメンバー定義
    [DataContract]
    public class M07_KEI_Member {
		[DataMember] public int 経費項目ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
        [DataMember] public string 経費項目名 { get; set; }
        [DataMember] public int? 経費区分 { get; set; }
        [DataMember] public int 固定変動区分 { get; set; }
		[DataMember] public int? 編集区分 { get; set; }
        [DataMember] public int? グリーン区分 { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
    }

    public class M07_KEI_Search_Member
    {
        [DataMember] public int 経費項目ID { get; set; }
        [DataMember] public string 経費項目名 { get; set; }
        [DataMember] public int? 経費区分 { get; set; }
        [DataMember] public int? 固定変動区分 { get; set; }
    }

    // データメンバー定義
    [DataContract]
    public class M07_KEI_PRINT
    {
        [DataMember]
        public int 経費項目ID { get; set; }
        [DataMember]
        public string 経費項目名 { get; set; }
        [DataMember]
        public int? 経費区分 { get; set; }
        [DataMember]
        public string 経費区分名 { get; set; }
        [DataMember]
        public string 固定変動区分名 { get; set; }
    }

}
