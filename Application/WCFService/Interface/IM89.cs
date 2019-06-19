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
    public interface IM89 {
        
        /// <summary>
        /// M89_KENALLのデータ取得
        /// </summary>
        /// <param name="p明細番号ID">明細番号ID</param>
        /// <returns>M89_KENALL_Member</returns>
        [OperationContract]
        M89_KENALL_Member GetData(string p明細番号ID);

		///// <summary>
		///// M89_KENALLの新規追加
		///// </summary>
		///// <param name="m89seq">M89_KENALL_Member</param>
		//[OperationContract]
		//void Insert(M89_KENALL_Member m89seq);

		///// <summary>
		///// M89_KENALLの更新
		///// </summary>
		///// <param name="m89seq">M89_KENALL_Member</param>
		//[OperationContract]
		//void Update(M89_KENALL_Member m89seq);

		///// <summary>
		///// M89_KENALLの物理削除
		///// </summary>
		///// <param name="m89seq">M89_KENALL_Member</param>
		//[OperationContract]
		//void Delete(M89_KENALL_Member m89seq);
    }

    // データメンバー定義
    [DataContract]
    public class M89_KENALL_Member {
		[DataMember] public int 明細番号ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public int 現在明細番号 { get; set; }
		[DataMember] public int 最大明細番号 { get; set; }
	}

}
