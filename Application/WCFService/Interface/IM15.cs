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
    public interface IM15 {
        
        /// <summary>
        /// M15_KOMのデータ取得
        /// </summary>
        /// <param name="pプログラムID">pプログラムID</param>
        /// <param name="p項目ID">項目ID</param>
        /// <returns>M15_KOM_Member</returns>
        [OperationContract]
        M15_KOM_Member GetData(string pプログラムID, int p項目ID);

        /// <summary>
        /// M15_KOMの新規追加
        /// </summary>
        /// <param name="m15kom">M15_KOM_Member</param>
        [OperationContract]
        void Insert(M15_KOM_Member m15kom);

        /// <summary>
        /// M15_KOMの更新
        /// </summary>
        /// <param name="m15kom">M15_KOM_Member</param>
        [OperationContract]
        void Update(M15_KOM_Member m15kom);

        /// <summary>
        /// M15_KOMの物理削除
        /// </summary>
        /// <param name="m15kom">M15_KOM_Member</param>
        [OperationContract]
        void Delete(M15_KOM_Member m15kom);
    }

    // データメンバー定義
    [DataContract]
    public class M15_KOM_Member {
		[DataMember] public string プログラムID { get; set; }
		[DataMember] public int 項目ID { get; set; }
		[DataMember] public int 明細区分 { get; set; }
		[DataMember] public string 項目名 { get; set; }
		[DataMember] public string 項目変数名 { get; set; }
		[DataMember] public int? H { get; set; }
		[DataMember] public int? A1 { get; set; }
		[DataMember] public int? A2 { get; set; }
		[DataMember] public int? B1 { get; set; }
		[DataMember] public int? B2 { get; set; }
		[DataMember] public int? T1 { get; set; }
		[DataMember] public int? T2 { get; set; }
	}
}
