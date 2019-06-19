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
    public interface IT03
    {

    }

    // データメンバー定義
    /// <summary>
    /// T03_KTRN_Member
    /// </summary>
    [DataContract]
    public class T03_KTRN_Member
    {
        [DataMember] public int 明細番号 { get; set; }
        [DataMember] public int 明細行 { get; set; }
        [DataMember] public DateTime? 登録日時 { get; set; }
        [DataMember] public DateTime? 更新日時 { get; set; }
        [DataMember] public int 明細区分 { get; set; }
        [DataMember] public int 入力区分 { get; set; }
        [DataMember] public DateTime? 経費発生日 { get; set; }
        [DataMember] public int? 車輌ID { get; set; }
        [DataMember] public string 車輌番号 { get; set; }
        [DataMember] public int? 乗務員ID { get; set; }
        [DataMember] public int? 支払先ID { get; set; }
        [DataMember] public int? 自社部門ID { get; set; }
        [DataMember] public int? 経費項目ID { get; set; }
        [DataMember] public string 経費補助名称 { get; set; }
        [DataMember] public decimal 単価 { get; set; }
        [DataMember] public decimal? 内軽油税分 { get; set; }
        [DataMember] public decimal? 数量 { get; set; }
        [DataMember] public int? 金額 { get; set; }
        [DataMember] public int? 収支区分 { get; set; }
        [DataMember] public int? 摘要ID { get; set; }
        [DataMember] public string 摘要名 { get; set; }
        [DataMember] public int? 入力者ID { get; set; }
    }


    // データメンバー定義
    /// <summary>
    /// T03_KTRN_Member
    /// </summary>
    [DataContract]
    public class T03_KTRN_Member_Pre
    {
        [DataMember] public int 明細番号 { get; set; }
        [DataMember] public int 明細行 { get; set; }
        [DataMember] public DateTime? 登録日時 { get; set; }
        [DataMember] public DateTime? 更新日時 { get; set; }
        [DataMember] public int 明細区分 { get; set; }
        [DataMember] public int 入力区分 { get; set; }
        [DataMember] public DateTime? 経費発生日 { get; set; }
        [DataMember] public int? 車輌ID { get; set; }
        [DataMember] public string 車輌番号 { get; set; }
        [DataMember] public int? メーター { get; set; }
        [DataMember] public int? 乗務員ID { get; set; }
        [DataMember] public int? 支払先ID { get; set; }
        [DataMember] public int? 自社部門ID { get; set; }
        [DataMember] public int? 経費項目ID { get; set; }
        [DataMember] public string 経費補助名称 { get; set; }
        [DataMember] public decimal? 単価 { get; set; }
        [DataMember] public decimal? 内軽油税分 { get; set; }
        [DataMember] public decimal? 数量 { get; set; }
        [DataMember] public int? 金額 { get; set; }
        [DataMember] public int? 収支区分 { get; set; }
        [DataMember] public string 収支区分表示 { get; set; }
        [DataMember] public int? 摘要ID { get; set; }
        [DataMember] public string 摘要名 { get; set; }
        [DataMember] public int? 入力者ID { get; set; }
        [DataMember] public string 乗務員名 { get; set; }
        [DataMember] public string 支払先名 { get; set; }
        [DataMember] public string 経費項目名 { get; set; }
        [DataMember] public int S締日 { get; set; }
        [DataMember] public int? 対象年 { get; set; }
        [DataMember] public int? 対象月 { get; set; }
        [DataMember] public DateTime? 集計 { get; set; }
        [DataMember] public int 社内区分 { get; set; }
    }
}


