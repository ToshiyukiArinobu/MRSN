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
    public interface IM13 {
        
        /// <summary>
        /// M13_MOKのデータ取得
        /// </summary>
        /// <param name="p規制区分ID">規制区分ID</param>
        /// <returns>M13_MOK_Member</returns>
        [OperationContract]
        M13_MOK_Member GetData(int p車両ID, int p年月);

        /// <summary>
        /// M13_MOKの新規追加
        /// </summary>
        /// <param name="m13mok">M13_MOK_Member</param>
        [OperationContract]
        void Insert(M13_MOK_Member m13mok);

        /// <summary>
        /// M13_MOKの更新
        /// </summary>
        /// <param name="m13mok">M13_MOK_Member</param>
        [OperationContract]
        void Update(M13_MOK_Member m13mok);

        /// <summary>
        /// M13_MOKの物理削除
        /// </summary>
        /// <param name="m13mok">M13_MOK_Member</param>
        [OperationContract]
        void Delete(M13_MOK_Member m13mok);
        
    }

    // データメンバー定義
    [DataContract]
    public class M13_MOK_Member {
		[DataMember] public int 車両ID { get; set; }
		[DataMember] public int 年月 { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public decimal? 目標燃費 { get; set; }
	}

}
