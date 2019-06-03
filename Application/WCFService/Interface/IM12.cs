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
    public interface IM12 {

        /// <summary>
        /// M12_KISのデータ取得
        /// </summary>
        /// <param name="p規制区分ID">G車種ID</param>
        /// <returns>M12_KIS_Member</returns>
        [OperationContract]
        List<M12_KIS_Member> GetData(int? p規制区分ID, int pオプションコード);

        /// <summary>
        /// M12_KISの更新
        /// </summary>
        /// <param name="m14gsya">M12_KIS_Member</param>
        [OperationContract]
        int Update(int? p規制区分ID, string p規制名, bool pMaintenanceFlg, bool pGetNextNumber);

        /// <summary>
        /// M12_KISの物理削除
        /// </summary>
        /// <param name="m14gsya">M12_KIS_Member</param>
        [OperationContract]
        void Delete(int? p規制区分ID);

        /// <summary>
        /// M12_KISのデータ取得
        /// </summary>
        /// <param name="p規制区分ID">G車種ID</param>
        /// <returns>M12_KIS_Member</returns>
        [OperationContract]
        List<M12_KIS_SCH_Member> M12_KIS_SCH(int? p規制区分ID, int pオプションコード);
                
    }

    // データメンバー定義
    [DataContract]
    public class M12_KIS_Member {
		[DataMember] public int 規制区分ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public string 規制名 { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
	}

    // データメンバー定義
    [DataContract]
    public class M12_KIS_SCH_Member
    {
		[DataMember] public int 規制区分ID { get; set; }
		[DataMember] public string 規制名 { get; set; }
	}

}
