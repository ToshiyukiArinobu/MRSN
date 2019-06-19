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
    public interface IM82 {

        /// <summary>
        /// M82_SEIのデータ取得
        /// </summary>
        /// <param name="p請求書区分ID">請求書区分ID</param>
        /// <returns>M82_SEI_Member</returns>
        [OperationContract]
        M82_SEI_Member GetData(int p請求書区分ID);

        /// <summary>
        /// M82_SEIの新規追加
        /// </summary>
        /// <param name="m82sei">M82_SEI_Member</param>
        [OperationContract]
        void Insert(M82_SEI_Member m82sei);

        /// <summary>
        /// M82_SEIの更新
        /// </summary>
        /// <param name="m82sei">M82_SEI_Member</param>
        [OperationContract]
        void Update(M82_SEI_Member m82sei);

        /// <summary>
        /// M82_SEIの物理削除
        /// </summary>
        /// <param name="m82sei">M82_SEI_Member</param>
        [OperationContract]
        void Delete(M82_SEI_Member m82sei);
        
    }

    // データメンバー定義
    [DataContract]
    public class M82_SEI_Member {
		[DataMember] public int 請求書区分ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public string 請求書名 { get; set; }
	}

}
