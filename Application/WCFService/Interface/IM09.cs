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
    public interface IM09 {   
    }

    // データメンバー定義
    [DataContract]
    public class M09_HIN_Member {
		[DataMember] public int 商品ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public string 商品名 { get; set; }
		[DataMember] public string かな読み { get; set; }
		[DataMember] public string 単位 { get; set; }
		[DataMember] public decimal? 商品重量 { get; set; }
		[DataMember] public decimal? 商品才数 { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
        [DataMember] public int? 編集区分 { get; set; }
        
	}

    [DataContract]
    public class M09_HIN_Member_SCH
    {
        [DataMember]
        public int 商品ID { get; set; }
        [DataMember]
        public string 商品名 { get; set; }
        [DataMember]
        public string かな読み { get; set; }
    }

}
