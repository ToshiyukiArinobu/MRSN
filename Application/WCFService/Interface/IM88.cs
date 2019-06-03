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
    public interface IM88 {
        
        /// <summary>
        /// M88_SEQのデータ取得
        /// </summary>
        /// <param name="p明細番号ID">明細番号ID</param>
        /// <returns>M88_SEQ_Member</returns>
        [OperationContract]
        M88_SEQ_Member GetData(int p明細番号ID);

        /// <summary>
        /// M88_SEQの新規追加
        /// </summary>
        /// <param name="m88seq">M88_SEQ_Member</param>
        [OperationContract]
        void Insert(M88_SEQ_Member m88seq);

        /// <summary>
        /// M88_SEQの更新
        /// </summary>
        /// <param name="m88seq">M88_SEQ_Member</param>
        [OperationContract]
        void Update(M88_SEQ_Member m88seq);

        /// <summary>
        /// M88_SEQの物理削除
        /// </summary>
        /// <param name="m88seq">M88_SEQ_Member</param>
        [OperationContract]
        void Delete(M88_SEQ_Member m88seq);
    }

    // データメンバー定義
    [DataContract]
    public class M88_SEQ_Member {
		[DataMember] public int 明細番号ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public int 現在明細番号 { get; set; }
		[DataMember] public int 最大明細番号 { get; set; }
	}

}
