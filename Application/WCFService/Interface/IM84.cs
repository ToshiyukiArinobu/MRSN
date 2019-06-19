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
    public interface IM84 {

        /// <summary>
        /// M84_RIKのデータ取得
        /// </summary>
        /// <param name="p運輸局ID">運輸局ID</param>
        /// <returns>M84_RIK_Member</returns>
        [OperationContract]
        List<M84_RIK_Member> GetData(int? p運輸局ID);

        /// <summary>
        /// M84_RIKの新規追加
        /// </summary>
        /// <param name="m84rik">M84_RIK_Member</param>
        [OperationContract]
        void Insert(M84_RIK_Member m84rik);

        /// <summary>
        /// M84_RIKの更新
        /// </summary>
        /// <param name="m84rik">M84_RIK_Member</param>
        [OperationContract]
        void Update(M84_RIK_Member m84rik);

        /// <summary>
        /// M84_RIKの物理削除
        /// </summary>
        /// <param name="m84rik">M84_RIK_Member</param>
        [OperationContract]
        void Delete(M84_RIK_Member m84rik);
        
    }

    // データメンバー定義
    [DataContract]
    public class M84_RIK_Member {
		[DataMember] public int 運輸局ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public string 運輸局名 { get; set; }
	}

}
