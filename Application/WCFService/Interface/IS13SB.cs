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
    public interface IS13SB {

    }
    
    // データメンバー定義
    [DataContract]
    public class S13_DRVSB_Member
    {
        [DataMember]
        public int 乗務員KEY { get; set; }
        [DataMember]
        public int 集計年月 { get; set; }
        [DataMember]
        public int 経費項目ID { get; set; }
        [DataMember]
        public DateTime? 登録日時 { get; set; }
		[DataMember]
        public DateTime? 更新日時 { get; set; }
        [DataMember]
        public string 経費項目名 { get; set; }
        [DataMember]
        public int? 固定変動区分 { get; set; }
        [DataMember]
        public decimal 金額 { get; set; }
        [DataMember]
        public int? 経費区分 { get; set; }
    }

    
    // データメンバー定義
    [DataContract]
    public class S13_DRVSB_Member_Preview_csv {
        [DataMember]
        public int 乗務員KEY { get; set; }
        [DataMember]
        public int 集計年月 { get; set; }
        [DataMember]
        public DateTime? 登録日時 { get; set; }
        [DataMember]
        public DateTime? 更新日時 { get; set; }
        [DataMember]
        public string 経費項目名 { get; set; }
        [DataMember]
        public int? 固定変動区分 { get; set; }
        [DataMember]
        public decimal 金額 { get; set; }
        [DataMember]
        public int? 経費区分 { get; set; }
    }


}
