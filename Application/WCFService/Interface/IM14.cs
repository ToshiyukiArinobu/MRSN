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
    public interface IM14 {
        
        /// <summary>
        /// M14_GSYAのデータ取得
        /// </summary>
        /// <param name="m14gsya">G車種ID</param>
        /// <returns>M14_GSYA_Member</returns>
        [OperationContract]
        List<M14_GSYA_Member> GetData(int? pG車種ID, int pオプションコード);

        /// <summary>
        /// M14_GSYAの更新
        /// </summary>
        /// <param name="m14gsya">M14_GSYA_Member</param>
        [OperationContract]
        int Update(int? pG車種ID, string pG車種名, string 略称名, decimal? CO2排出係数１, decimal? CO2排出係数２, int? 事業用区分, int? ディーゼル区分, int? 小型普通貨物区分, int? 低公害者区分, bool pMaintenanceFlg, bool pGetNextNumber);

        /// <summary>
        /// M14_GSYAの物理削除
        /// </summary>
        /// <param name="m14gsya">M14_GSYA_Member</param>
        [OperationContract]
        void Delete(int? pG車種ID);

        /// <summary>
        /// M14_GSYAのデータ取得
        /// </summary>
        /// <param name="m14gsya">G車種ID</param>
        /// <returns>M14_GSYA_Member</returns>
        [OperationContract]
        List<M14_GSYA_SCH_Member> M14_GSYA_SCH(int? pG車種ID, int pオプションコード);

    }

    // データメンバー定義
    [DataContract]
    public class M14_GSYA_Member {
		[DataMember] public int G車種ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public string G車種名 { get; set; }
		[DataMember] public string 略称名 { get; set; }
		[DataMember] public decimal? CO2排出係数１ { get; set; }
		[DataMember] public decimal? CO2排出係数２ { get; set; }
		[DataMember] public int? 事業用区分 { get; set; }
		[DataMember] public int? ディーゼル区分 { get; set; }
		[DataMember] public int? 小型普通区分 { get; set; }
		[DataMember] public int? 低公害区分 { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
	}

    // データメンバー定義
    [DataContract]
    public class M14_GSYA_SCH_Member
    {
		[DataMember] public int G車種ID { get; set; }
		[DataMember] public string 事業用区分 { get; set; }
        [DataMember] public string G車種名 { get; set; }
	}

}
