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
    public interface IM08 {

        /// <summary>
        /// M08_TIKのデータ取得
        /// </summary>
        /// <param name="p発着地ID">発着地ID</param>
        /// <returns>M08_TIK_Member</returns>
        [OperationContract]
        List<M08_TIK_Member> GetData(int? p発着地ID, int pOption);

        /// <summary>
        /// M08_TIKの更新
        /// </summary>
        /// <param name="m08tik">M08_TIK_Member</param>
        [OperationContract]
        int Update(int p発着地ID, string p発着地名, string pかな読み, int? pタリフ距離, string p郵便番号, string p住所１, string p住所２, string p電話番号, string pＦＡＸ番号, int? p配送エリアID, bool pMaintenanceFlg, bool pGetNextNumber);
        
        /// <summary>
        /// M08_TIKの論理削除
        /// </summary>
        /// <param name="m08tik">M08_TIK_Member</param>
        [OperationContract]
        void Delete(int p発着地ID);

        /// <summary>
        /// M08_TIKのID自動採番
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int GetNextNumber();

        /// <summary>
        /// 発着地マスタ一覧検索データ取得
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<M08_TIK_Member> GetSearchDataForList(string 発着地コードFROM, string 発着地コードTO, int[] i発着地IDList, string 指定エリアコード, string 表示方法);
        
    }

    // データメンバー定義
    [DataContract]
    public class M08_TIK_Member {
		[DataMember] public int 発着地ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public string 発着地名 { get; set; }
		[DataMember] public string かな読み { get; set; }
		[DataMember] public int? タリフ距離 { get; set; }
		[DataMember] public string 郵便番号 { get; set; }
		[DataMember] public string 住所１ { get; set; }
		[DataMember] public string 住所２ { get; set; }
		[DataMember] public string 電話番号 { get; set; }
		[DataMember] public string ＦＡＸ番号 { get; set; }
		[DataMember] public int? 配送エリアID { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
	}
    [DataContract]
    public class M08_TIK_Search_Member {
		[DataMember] public int 発着地ID { get; set; }
		[DataMember] public string 発着地名 { get; set; }
		[DataMember] public string かな読み { get; set; }
	}
    
}
