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
    public interface IS01 {

    }
    
    // データメンバー定義
    [DataContract]
    public class S01_TOKS_Member
    {
        [DataMember]
        public int 得意先KEY { get; set; }
        [DataMember]
        public int 集計年月 { get; set; }
        [DataMember]
        public int 回数 { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
        [DataMember]
        public DateTime? 締集計開始日 { get; set; }
        [DataMember]
        public DateTime? 締集計終了日 { get; set; }
        [DataMember]
        public decimal 締日前月残高 { get; set; }
        [DataMember]
        public decimal 締日入金現金 { get; set; }
        [DataMember]
        public decimal 締日入金手形 { get; set; }
        [DataMember]
        public decimal 締日入金その他 { get; set; }
        [DataMember]
        public decimal 締日売上金額 { get; set; }
        [DataMember]
        public decimal 締日通行料 { get; set; }
        [DataMember]
        public decimal 締日課税売上 { get; set; }
        [DataMember]
        public decimal 締日非課税売上 { get; set; }
        [DataMember]
        public decimal 締日消費税 { get; set; }
        [DataMember]
        public decimal 締日内傭車売上 { get; set; }
        [DataMember]
        public decimal 締日内傭車料 { get; set; }
        [DataMember]
        public decimal 締日未定件数 { get; set; }
        [DataMember]
        public decimal 締日件数 { get; set; }
        [DataMember]
        public int 締日 { get; set; }
    }

    
    // データメンバー定義
    [DataContract]
    public class S01_TOKS_Member_Preview_csv {
        [DataMember]
        public int 得意先KEY { get; set; }
        [DataMember]
        public string 得意先名 { get; set; }
        [DataMember]
        public int 集計年月 { get; set; }
        [DataMember]
        public int 回数 { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
        [DataMember]
        public DateTime? 締集計開始日 { get; set; }
        [DataMember]
        public DateTime? 締集計終了日 { get; set; }
        [DataMember]
        public decimal 締日前月残高 { get; set; }
        [DataMember]
        public decimal 締日入金現金 { get; set; }
        [DataMember]
        public decimal 締日入金手形 { get; set; }
        [DataMember]
        public decimal 締日入金その他 { get; set; }
        [DataMember]
        public decimal 締日売上金額 { get; set; }
        [DataMember]
        public decimal 締日通行料 { get; set; }
        [DataMember]
        public decimal 締日課税売上 { get; set; }
        [DataMember]
        public decimal 締日非課税売上 { get; set; }
        [DataMember]
        public decimal 締日消費税 { get; set; }
        [DataMember]
        public decimal 締日内傭車売上 { get; set; }
        [DataMember]
        public decimal 締日内傭車料 { get; set; }
        [DataMember]
        public decimal 締日未定件数 { get; set; }
        [DataMember]
        public decimal 締日件数 { get; set; }
        [DataMember]
        public int 締日 { get; set; }
    }


}
