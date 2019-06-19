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
    public interface IM92 {

     

        ///// <summary>
        ///// M92_KZEIの新規追加
        ///// </summary>
        ///// <param name="M92zei">M92_KZEI_Member</param>
        //[OperationContract]
        //void Insert(M92_KZEI_Member M92zei);

        ///// <summary>
        ///// M92_KZEIの更新
        ///// </summary>
        ///// <param name="M92zei">M92_KZEI_Member</param>
        //[OperationContract]
        //void Update(M92_KZEI_Member M92zei);

        ///// <summary>
        ///// M92_KZEIの物理削除
        ///// </summary>
        ///// <param name="M92zei">M92_KZEI_Member</param>
        //[OperationContract]
        //void Delete(M92_KZEI_Member M92zei);
    }

    // データメンバー定義
    [DataContract]
    public class M92_KZEI_Member {
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public DateTime? 適用開始年月日 { get; set; }
		[DataMember] public decimal? 軽油引取税率 { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
	}

    [DataContract]
    public class M92_KZEI_Member_SCH
    {
		[DataMember] public DateTime? 適用開始年月日 { get; set; }
		[DataMember] public decimal? 軽油引取税率 { get; set; }
    }

    [DataContract]
    public class M92_KZEI_Member_Ichiran
    {
		[DataMember] public string 適用開始年月日 { get; set; }
		[DataMember] public decimal? 軽油引取税率 { get; set; }
    }

}
