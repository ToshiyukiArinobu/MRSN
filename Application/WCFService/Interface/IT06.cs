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
    public interface IT06 {

        /// <summary>
        /// T06_KYUSのリスト取得
        /// </summary>
        /// <param name="p車輌ID">車輌ID</param>
        /// <param name="p休車開始日付">休車開始日付</param>
        /// <returns>T06_KYUS_MemberのList</returns>
        [OperationContract]
        List<T06_KYUS_Member> GetList(int p車輌ID, DateTime p休車開始日付);

        /// <summary>
        /// T06_KYUSの新規追加
        /// </summary>
        /// <param name="t06kyus">T06_KYUS_Member</param>
        [OperationContract]
        void Insert(T06_KYUS_Member t06kyus);

        /// <summary>
        /// T06_KYUSの更新
        /// </summary>
        /// <param name="t06kyus">T06_KYUS_Member</param>
        [OperationContract]
        void Update(T06_KYUS_Member t06kyus);

        /// <summary>
        /// T06_KYUSの物理削除
        /// </summary>
        /// <param name="t06kyus">T06_KYUS_Member</param>
        [OperationContract]
        void Delete(T06_KYUS_Member t06kyus);
       
    }

    // データメンバー定義
    [DataContract]
    public class T06_KYUS_Member {
		[DataMember] public int 車輌ID { get; set; }
		[DataMember] public DateTime 休車開始日付 { get; set; }
		[DataMember] public DateTime? 休車終了日付 { get; set; }
		[DataMember] public int? 明細区分 { get; set; }
		[DataMember] public string 車輌番号 { get; set; }
		[DataMember] public int? 休車事由 { get; set; }
	}

    ///// <summary>
    ///// SCH04010の一覧表示用
    ///// </summary>
    //[DataContract]
    //public class SCH04010_Member
    //{
    //    /// <summary>
    //    /// ff
    //    /// </summary>
    //    [DataMember]
    //    public int 乗務員ID { get; set; }
    //    [DataMember]
    //    public string 乗務員名 { get; set; }
    //    [DataMember]
    //    public string かな読み { get; set; }

    //}

}
