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
    public interface IM73 {

     

        ///// <summary>
        ///// M73_ZEIの新規追加
        ///// </summary>
        ///// <param name="m73zei">M73_ZEI_Member</param>
        //[OperationContract]
        //void Insert(M73_ZEI_Member m73zei);

        ///// <summary>
        ///// M73_ZEIの更新
        ///// </summary>
        ///// <param name="m73zei">M73_ZEI_Member</param>
        //[OperationContract]
        //void Update(M73_ZEI_Member m73zei);

        ///// <summary>
        ///// M73_ZEIの物理削除
        ///// </summary>
        ///// <param name="m73zei">M73_ZEI_Member</param>
        //[OperationContract]
        //void Delete(M73_ZEI_Member m73zei);
    }

    // データメンバー定義
    [DataContract]
    public class M73_ZEI_Member {
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public DateTime? 適用開始日付 { get; set; }
		[DataMember] public int? 消費税率 { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
	}

    [DataContract]
    public class M73_ZEI_Member_SCH
    {
        [DataMember] public DateTime? 適用開始日付 { get; set; }
        [DataMember] public int? 消費税率 { get; set; }
    }

    [DataContract]
    public class M73_ZEI_Member_Ichiran
    {
        [DataMember] public string 適用開始日付 { get; set; }
        [DataMember] public int? 消費税率 { get; set; }
    }

}
