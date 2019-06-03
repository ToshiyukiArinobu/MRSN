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
    public interface IS14 {

    }
    
    // データメンバー定義
    [DataContract]
    public class S14_CAR_Member
    {
        [DataMember]
        public int 車輌KEY { get; set; }
        [DataMember]
        public int 集計年月 { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
        [DataMember]
        public int? 自社部門ID { get; set; }
        [DataMember]
        public int? 車種ID { get; set; }
        [DataMember]
        public int? 乗務員KEY { get; set; }
        [DataMember]
        public int 営業日数 { get; set; }
        [DataMember]
        public int 稼動日数 { get; set; }
        [DataMember]
        public int 走行ＫＭ { get; set; }
        [DataMember]
        public int 実車ＫＭ { get; set; }
        [DataMember]
        public decimal 輸送屯数 { get; set; }
        [DataMember]
        public decimal 運送収入 { get; set; }
        [DataMember]
        public decimal 燃料Ｌ { get; set; }
        [DataMember]
        public decimal 一般管理費 { get; set; }
        [DataMember]
        public decimal 拘束時間 { get; set; }
        [DataMember]
        public decimal 運転時間 { get; set; }
        [DataMember]
        public decimal 高速時間 { get; set; }
        [DataMember]
        public decimal 作業時間 { get; set; }
        [DataMember]
        public decimal 待機時間 { get; set; }
        [DataMember]
        public decimal 休憩時間 { get; set; }
        [DataMember]
        public decimal 残業時間 { get; set; }
        [DataMember]
        public decimal 深夜時間 { get; set; }
    }

    
    // データメンバー定義
    [DataContract]
    public class S14_CAR_Member_Preview_csv {
        [DataMember]
        public int 車輌KEY { get; set; }
        [DataMember]
        public int 集計年月 { get; set; }
        [DataMember]
        public DateTime? 登録日時 { get; set; }
        [DataMember]
        public DateTime? 更新日時 { get; set; }
        [DataMember]
        public int? 自社部門ID { get; set; }
        [DataMember]
        public int? 車種ID { get; set; }
        [DataMember]
        public int? 乗務員KEY { get; set; }
        [DataMember]
        public int 営業日数 { get; set; }
        [DataMember]
        public int 稼動日数 { get; set; }
        [DataMember]
        public int 走行ＫＭ { get; set; }
        [DataMember]
        public int 実車ＫＭ { get; set; }
        [DataMember]
        public decimal 輸送屯数 { get; set; }
        [DataMember]
        public decimal 運送収入 { get; set; }
        [DataMember]
        public decimal 燃料Ｌ { get; set; }
        [DataMember]
        public decimal 一般管理費 { get; set; }
        [DataMember]
        public decimal 拘束時間 { get; set; }
        [DataMember]
        public decimal 運転時間 { get; set; }
        [DataMember]
        public decimal 高速時間 { get; set; }
        [DataMember]
        public decimal 作業時間 { get; set; }
        [DataMember]
        public decimal 待機時間 { get; set; }
        [DataMember]
        public decimal 休憩時間 { get; set; }
        [DataMember]
        public decimal 残業時間 { get; set; }
        [DataMember]
        public decimal 深夜時間 { get; set; }
    }


}
