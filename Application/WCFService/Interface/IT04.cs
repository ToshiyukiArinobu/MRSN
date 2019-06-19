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
    public interface IT04 {

        /// <summary>
        /// T04_NYUKのリスト取得
        /// </summary>
        /// <param name="p明細番号">明細番号</param>
        /// <param name="p明細行">明細行</param>
        /// <returns>T04_NYUK_MemberのList</returns>
        [OperationContract]
        List<T04_NYUK_Member> GetList(int p明細番号, int p明細行);

        /// <summary>
        /// T04_NYUKの新規追加
        /// </summary>
        /// <param name="t04nyuk">T04_NYUK_Member</param>
        [OperationContract]
        void Insert(T04_NYUK_Member t04nyuk);

        /// <summary>
        /// T04_NYUKの更新
        /// </summary>
        /// <param name="t04nyuk">T04_NYUK_Member</param>
        [OperationContract]
        void Update(T04_NYUK_Member t04nyuk);

        /// <summary>
        /// T04_NYUKの物理削除
        /// </summary>
        /// <param name="t04nyuk">T04_NYUK_Member</param>
        [OperationContract]
        void Delete(T04_NYUK_Member t04nyuk);
        
    }

    // データメンバー定義
    /// <summary>
    /// T04_NYUK_Member
    /// </summary>
    [DataContract]
    public class T04_NYUK_Member {
		[DataMember] public int 明細番号 { get; set; }
		[DataMember] public int 明細行 { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public int 明細区分 { get; set; }
		[DataMember] public DateTime? 入出金日付 { get; set; }
		[DataMember] public int 取引先ID { get; set; }
		[DataMember] public int 入出金区分 { get; set; }
		[DataMember] public int 入出金金額 { get; set; }
		[DataMember] public int? 摘要ID { get; set; }
		[DataMember] public string 摘要名 { get; set; }
		[DataMember] public DateTime? 手形日付 { get; set; }
		[DataMember] public int? 入力者ID { get; set; }
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
