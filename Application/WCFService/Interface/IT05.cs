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
    public interface IT05 {

        /// <summary>
        /// T05_TTRNのリスト取得
        /// </summary>
        /// <param name="p乗務員ID">乗務員ID</param>
        /// <param name="p配車日付">配車日付</param>
        /// <returns>T05_TTRN_MemberのList</returns>
        [OperationContract]
        List<T05_TTRN_Member> GetList(int p乗務員ID, DateTime p配車日付);

        /// <summary>
        /// T05_TTRNの新規追加
        /// </summary>
        /// <param name="t05ttrn">T05_TTRN_Member</param>
        [OperationContract]
        void Insert(T05_TTRN_Member t05ttrn);

        /// <summary>
        /// T05_TTRNの更新
        /// </summary>
        /// <param name="t05ttrn">T05_TTRN_Member</param>
        [OperationContract]
        void Update(T05_TTRN_Member t05ttrn);

        /// <summary>
        /// T05_TTRNの物理削除
        /// </summary>
        /// <param name="t05ttrn">T05_TTRN_Member</param>
        [OperationContract]
        void Delete(T05_TTRN_Member t05ttrn);
       
    }

    // データメンバー定義
    /// <summary>
    /// T05_TTRN_Member
    /// </summary>
    [DataContract]
    public class T05_TTRN_Member {
		[DataMember] public int 乗務員ID { get; set; }
		[DataMember] public System.DateTime 配車日付 { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public int 明細区分 { get; set; }
		[DataMember] public int? 車輌ID { get; set; }
		[DataMember] public string 車輌番号 { get; set; }
		[DataMember] public string 行先 { get; set; }
		[DataMember] public string 指示項目 { get; set; }
		[DataMember] public int 自社部門ID { get; set; }
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
