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
    public interface IM83 {
        
        /// <summary>
        /// M83_UKEのデータ取得
        /// </summary>
        /// <param name="p運賃計算区分ID">運賃計算区分ID</param>
        /// <returns>M83_UKE_Member</returns>
        [OperationContract]
        M83_UKE_Member GetData(int p運賃計算区分ID);

        /// <summary>
        /// M83_UKEの新規追加
        /// </summary>
        /// <param name="m83uke">M83_UKE_Member</param>
        [OperationContract]
        void Insert(M83_UKE_Member m83uke);

        /// <summary>
        /// M83_UKEの更新
        /// </summary>
        /// <param name="m83uke">M83_UKE_Member</param>
        [OperationContract]
        void Update(M83_UKE_Member m83uke);

        /// <summary>
        /// M83_UKEの物理削除
        /// </summary>
        /// <param name="m83uke">M83_UKE_Member</param>
        [OperationContract]
        void Delete(M83_UKE_Member m83uke);

    }

    // データメンバー定義
    [DataContract]
    public class M83_UKE_Member {
		[DataMember] public int 運賃計算区分ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public string 運賃計算区分 { get; set; }
	}

}
