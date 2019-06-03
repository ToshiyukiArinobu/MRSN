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
    public interface IT07 {

    }

    // データメンバー定義
    /// <summary>
    /// T03_KTRN_Member
    /// </summary>
    [DataContract]
    public class t03_KTRN_Member {
        [DataMember] public int 明細番号 { get; set; }
        [DataMember] public int 明細行 { get; set; }
        [DataMember] public DateTime? 登録日時 { get; set; }
        [DataMember] public DateTime? 更新日時 { get; set; }
        [DataMember] public int 明細区分 { get; set; }
        [DataMember] public int 入力区分 { get; set; }
        [DataMember] public DateTime? 経費発生日 { get; set; }
        [DataMember] public string str経費発生日 { get; set; }
        [DataMember] public int? メーター { get; set; }
        [DataMember] public int? 車輌ID { get; set; }
        [DataMember] public string 車輌番号 { get; set; }
        [DataMember] public int? 乗務員ID { get; set; }
        [DataMember] public string 運行者名 { get; set; }
        [DataMember] public int? 支払先ID { get; set; }
        [DataMember] public int 取引区分 { get; set; }
        [DataMember] public string 支払先名 { get; set; }
        [DataMember] public int? 自社部門ID { get; set; }
        [DataMember] public string 自社部門名 { get; set; }
        [DataMember] public int? 経費項目ID { get; set; }
        [DataMember] public string 経費項目名 { get; set; }
        [DataMember] public string 経費補助名称 { get; set; }
        [DataMember] public decimal? 単価 { get; set; }
        [DataMember] public DateTime 摘要開始年月日 { get; set; }
        [DataMember] public decimal 燃料単価 { get; set; }
        [DataMember] public decimal? 内軽油税分 { get; set; }
        [DataMember] public decimal? 数量 { get; set; }
        [DataMember] public int? 金額 { get; set; }
        [DataMember] public int? 収支区分 { get; set; }
        [DataMember] public int? 摘要ID { get; set; }
        [DataMember] public string 摘要名 { get; set; }
        [DataMember] public int? 入力者ID { get; set; }
    }

}
