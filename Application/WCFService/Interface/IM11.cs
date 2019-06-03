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
    public interface IM11
    {


    }

    // データメンバー定義
    [DataContract]
    public class M11_TEK_Member
    {
        [DataMember]
        public int 摘要ID { get; set; }
        [DataMember]
        public DateTime? 登録日時 { get; set; }
        [DataMember]
        public DateTime? 更新日時 { get; set; }
        [DataMember]
        public string 摘要名 { get; set; }
        [DataMember]
        public string かな読み { get; set; }
        [DataMember]
        public DateTime? 削除日付 { get; set; }
    }
    [DataContract]
    public class M11_TEK_Search_Member
    {
        [DataMember]
        public int 摘要ID { get; set; }
        [DataMember]
        public string 摘要名 { get; set; }
        [DataMember]
        public string かな読み { get; set; }
    }

}
