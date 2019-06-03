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
    public interface IM76 {

        /// <summary>
        /// M76_DBUのデータ取得
        /// </summary>
        /// <param name="p歩合計算区分ID">歩合計算区分ID</param>
        /// <returns>M76_DBU_Member</returns>
        [OperationContract]
        M76_DBU_Member GetData(int p歩合計算区分ID);

        /// <summary>
        /// M76_DBUの新規追加
        /// </summary>
        /// <param name="m76dbu">M76_DBU_Member</param>
        [OperationContract]
        void Insert(M76_DBU_Member m76dbu);

        /// <summary>
        /// M76_DBUの更新
        /// </summary>
        /// <param name="m76dbu">M76_DBU_Member</param>
        [OperationContract]
        void Update(M76_DBU_Member m76dbu);

        /// <summary>
        /// M76_DBUの物理削除
        /// </summary>
        /// <param name="m76dbu">M76_DBU_Member</param>
        [OperationContract]
        void Delete(M76_DBU_Member M75skk);
        
    }

    // データメンバー定義
    [DataContract]
    public class M76_DBU_Member {
		[DataMember] public int 歩合計算区分ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public string 歩合計算名 { get; set; }
	}

}
