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
    public interface IT02 {
        
    }

    // データメンバー定義
    /// <summary>
    /// T02_UTRN_Member
    /// </summary>
    [DataContract]
    public class T02_UTRN_Member {
		[DataMember] public int 明細番号 { get; set; }
		[DataMember] public int 明細行 { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public int 明細区分 { get; set; }
		[DataMember] public int 入力区分 { get; set; }
		[DataMember] public DateTime? 実運行日開始 { get; set; }
		[DataMember] public DateTime? 実運行日終了 { get; set; }
		[DataMember] public int? 車輌ID { get; set; }
		[DataMember] public int? 乗務員ID { get; set; }
		[DataMember] public int? 車種ID { get; set; }
		[DataMember] public string 車輌番号 { get; set; }
		[DataMember] public int? 自社部門ID { get; set; }
		[DataMember] public decimal? 出庫時間 { get; set; }
		[DataMember] public decimal? 帰庫時間 { get; set; }
		[DataMember] public int 出勤区分ID { get; set; }
		[DataMember] public decimal? 拘束時間 { get; set; }
		[DataMember] public decimal? 運転時間 { get; set; }
		[DataMember] public decimal? 高速時間 { get; set; }
		[DataMember] public decimal? 作業時間 { get; set; }
		[DataMember] public decimal? 待機時間 { get; set; }
		[DataMember] public decimal? 休憩時間 { get; set; }
		[DataMember] public decimal? 残業時間 { get; set; }
		[DataMember] public decimal? 深夜時間 { get; set; }
		[DataMember] public int 走行ＫＭ { get; set; }
		[DataMember] public int 実車ＫＭ { get; set; }
		[DataMember] public decimal 輸送屯数 { get; set; }
		[DataMember] public int 出庫ＫＭ { get; set; }
		[DataMember] public int 帰庫ＫＭ { get; set; }
		[DataMember] public string 備考 { get; set; }
		[DataMember] public DateTime 勤務開始日 { get; set; }
		[DataMember] public DateTime 勤務終了日 { get; set; }
		[DataMember] public DateTime 労務日 { get; set; }
		[DataMember] public int? 入力者ID { get; set; }
	}

    

}
