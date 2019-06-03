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
    public interface IM06 {

        /// <summary>
        /// M06_SYAのデータ取得
        /// </summary>
        /// <param name="p車種ID">車種ID</param>
        /// <returns>M06_SYA_Member</returns>
        [OperationContract]
        List<M06_SYA_Member> GetData(int? p車種ID , int pオプションコード);

        ///// <summary>
        ///// M06_SYAの削除済データ取得
        ///// </summary>
        ///// <param name="p車種ID">車種ID</param>
        ///// <returns>M06_SYA_Member</returns>
        //[OperationContract]
        //List<M06_SYA_Member> GetDataDeleteSerch(int? p車種ID, int pオプションコード);

        /// <summary>
        /// M06_SYAのF1データ検索
        /// </summary>
        /// <param name="p車種ID">車種ID</param>
        /// <returns>M06_SYA_Member</returns>
        [OperationContract]
        List<M06_SYA_ichiran_Member> F1_Kensaku(int p確定コード);

        /// <summary>
        /// M06_SYAの検索データ取得(プレビュー用)
        /// </summary>
        /// <param name="p車種ID">車種ID</param>
        /// <returns>M06_SYA_Member</returns>
        [OperationContract]
        List<M06_SYA_ichiran_Member> GetSearchListData(string p車種ID, string p車種IDTo, int[] i車種List);

        /// <summary>
        /// M06_SYAの更新
        /// </summary>
        /// <param name="m06sya">M06_SYA_Member</param>
        [OperationContract]
        int Update(int p車種ID, string p車種名, decimal p積載重量, bool pMaintenanceFlg, bool pGetNextNumber);

        /// <summary>
        /// M06_SYAの物理削除
        /// </summary>
        /// <param name="m06sya">M06_SYA_Member</param>
        [OperationContract]
        void Delete(int p車種ID);

    }

    // データメンバー定義
    [DataContract]
    public class M06_SYA_Member {
		[DataMember] public int 車種ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public string 車種名 { get; set; }
		[DataMember] public decimal? 積載重量 { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
	}

    // F1キー検索用データ、プレビュー用データメンバー定義
    [DataContract]
    public class M06_SYA_ichiran_Member {
		[DataMember] public int 車種ID { get; set; }
		[DataMember] public string 車種名 { get; set; }
		[DataMember] public decimal? 積載重量 { get; set; }
        
	}

}
