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
    public interface IM72 {

        
    }

    // データメンバー定義
    [DataContract]
    public class M72_TNT_Member {
		[DataMember] public int 担当者ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public string 担当者名 { get; set; }
		[DataMember] public string かな読み { get; set; }
		[DataMember] public string パスワード { get; set; }
        [DataMember] public int グループ権限ID { get; set; }
        [DataMember] public string 個人ナンバー { get; set; }
		[DataMember] public byte[] 設定項目 { get; set; }
		[DataMember] public DateTime? 削除日付 { get; set; }
    }

    //権限関係追加
    [DataContract]
    public class M74_AUTHORITY_Member
    {
        [DataMember] public string プログラムID { get; set; }
        [DataMember] public Boolean 使用可能FLG { get; set; }
        [DataMember] public Boolean データ更新FLG { get; set; }
    }

    [DataContract]
    public class M72_TNT_Member_SCH
    {
        [DataMember]
        public int 担当者ID { get; set; }
        [DataMember]
        public string 担当者名 { get; set; }
        [DataMember]
        public string かな読み { get; set; }
    }
    
}
