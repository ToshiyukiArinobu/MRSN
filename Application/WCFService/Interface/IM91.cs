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
    public interface IM91 {

        
    }

    // データメンバー定義
    [DataContract]
    public class M91_OTAN_Member {
		[DataMember] public int 支払先KEY { get; set; }
        [DataMember] public DateTime? 適用開始年月日 { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public decimal? 燃料単価 { get; set; }
        [DataMember] public int 得意先ID { get; set; }
        [DataMember] public string 略称名 { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }

	}
    
}
