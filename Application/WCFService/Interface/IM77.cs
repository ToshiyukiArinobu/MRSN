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
    public interface IM77 {

        /// <summary>
        /// M77_TRHのデータ取得
        /// </summary>
        /// <param name="p取引区分ID">取引区分ID</param>
        /// <returns>M77_TRH_Member</returns>
        [OperationContract]
        M77_TRH_Member GetData(int p取引区分ID);

        /// <summary>
        /// M77_TRHの新規追加
        /// </summary>
        /// <param name="m77trh">M77_TRH_Member</param>
        [OperationContract]
        void Insert(M77_TRH_Member m77trh);

        /// <summary>
        /// M77_TRHの更新
        /// </summary>
        /// <param name="m77trh">M77_TRH_Member</param>
        [OperationContract]
        void Update(M77_TRH_Member m77trh);

        /// <summary>
        /// M77_TRHの物理削除
        /// </summary>
        /// <param name="m77trh">M77_TRH_Member</param>
        [OperationContract]
        void Delete(M77_TRH_Member M75skk);
        
    }

    // データメンバー定義
    [DataContract]
    public class M77_TRH_Member {
		[DataMember] public int 取引区分ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public string 歩合計算名 { get; set; }
	}

}
