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
    public interface IS12 {

    }
    
    // データメンバー定義
    [DataContract]
    public class S12_YOSG_Member
    {
        [DataMember]
        public int 支払先KEY { get; set; }
        [DataMember]
        public int 集計年月 { get; set; }
        [DataMember]
        public int 回数 { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
        [DataMember]
        public DateTime? 月集計開始日 { get; set; }
        [DataMember]
        public DateTime? 月集計終了日 { get; set; }
        [DataMember]
        public decimal 月次前月残高 { get; set; }
        [DataMember]
        public decimal 月次入金現金 { get; set; }
        [DataMember]
        public decimal 月次入金手形 { get; set; }
        [DataMember]
        public decimal 月次入金その他 { get; set; }
        [DataMember]
        public decimal 月次売上金額 { get; set; }
        [DataMember]
        public decimal 月次通行料 { get; set; }
        [DataMember]
        public decimal 月次課税売上 { get; set; }
        [DataMember]
        public decimal 月次非課税売上 { get; set; }
        [DataMember]
        public decimal 月次消費税 { get; set; }
        [DataMember]
        public decimal 月次内傭車売上 { get; set; }
        [DataMember]
        public decimal 月次内傭車料 { get; set; }
        [DataMember]
        public decimal 月次未定件数 { get; set; }
        [DataMember]
        public decimal 月次件数 { get; set; }
        [DataMember]
        public int 締日 { get; set; }
    }

    
    // データメンバー定義
    [DataContract]
    public class S12_YOSG_Member_Preview_csv {
        [DataMember]
        public int 支払先KEY { get; set; }
        [DataMember]
        public string 得意先名 { get; set; }
        [DataMember]
        public int 集計年月 { get; set; }
        [DataMember]
        public int 回数 { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
        [DataMember]
        public DateTime? 月集計開始日 { get; set; }
        [DataMember]
        public DateTime? 月集計終了日 { get; set; }
        [DataMember]
        public decimal 月次前月残高 { get; set; }
        [DataMember]
        public decimal 月次入金現金 { get; set; }
        [DataMember]
        public decimal 月次入金手形 { get; set; }
        [DataMember]
        public decimal 月次入金その他 { get; set; }
        [DataMember]
        public decimal 月次売上金額 { get; set; }
        [DataMember]
        public decimal 月次通行料 { get; set; }
        [DataMember]
        public decimal 月次課税売上 { get; set; }
        [DataMember]
        public decimal 月次非課税売上 { get; set; }
        [DataMember]
        public decimal 月次消費税 { get; set; }
        [DataMember]
        public decimal 月次内傭車売上 { get; set; }
        [DataMember]
        public decimal 月次内傭車料 { get; set; }
        [DataMember]
        public decimal 月次未定件数 { get; set; }
        [DataMember]
        public decimal 月次件数 { get; set; }
        [DataMember]
        public int 締日 { get; set; }
    }


}
