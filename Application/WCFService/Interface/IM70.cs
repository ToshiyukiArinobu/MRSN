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
    public interface IM70 {
     
    }


    [DataContract]
    public class M70_JIS_Member {
		[DataMember] public int 自社ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public string 自社名 { get; set; }
		[DataMember] public string 代表者名 { get; set; }
		[DataMember] public string 郵便番号 { get; set; }
		[DataMember] public string 住所１ { get; set; }
		[DataMember] public string 住所２ { get; set; }
		[DataMember] public string 電話番号 { get; set; }
		[DataMember] public string ＦＡＸ { get; set; }
		[DataMember] public string 振込銀行１ { get; set; }
		[DataMember] public string 振込銀行２ { get; set; }
		[DataMember] public string 振込銀行３ { get; set; }
        [DataMember] public string 法人ナンバー { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
        [DataMember] public Byte[] 画像 { get; set; }
	}

    [DataContract]
    public class M70_JIS_Member_SCH
    {
		[DataMember] public int 自社ID { get; set; }
        [DataMember] public string 自社名 { get; set; }
  		[DataMember] public string 代表者名 { get; set; }
}
    

}
