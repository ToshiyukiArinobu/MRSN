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
    public interface IM10 {

        /// <summary>
        /// M10_UHKのデータ取得
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <returns>M10_UHK_Member</returns>
		[OperationContract]
		List<M10_UHK_Member> GetData(int? p取引先ID,int? p請求コード, int? option);

        /// <summary>
        /// M10_UHKの更新
        /// </summary>
        /// <param name="m10uhk">M10_UHK_Member</param>
        [OperationContract]
        void Update(int i得意先コード , int i請求内訳コード,string str請求内訳名,string strかな読み);

        /// <summary>
        /// M10_UHKの物理削除
        /// </summary>
        /// <param name="m10uhk">M10_UHK_Member</param>
        [OperationContract]
        void Delete(int i得意先コード, int i請求内訳コード);

        /// <summary>
        /// 検索画面用データ取得
        /// </summary>
        /// <param name="p得意先ID"></param>
        /// <returns></returns>
        [OperationContract]
        List<M10_UHK_SCH_Member> GetDataSCH(int p得意先ID, int p確定コード, string pかな読み);
        
    }

    // データメンバー定義
    [DataContract]
    public class M10_UHK_Member {
		[DataMember] public int 得意先ID { get; set; }
		[DataMember] public int 請求内訳ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public string 請求内訳名 { get; set; }
		[DataMember] public string かな読み { get; set; }
		[DataMember] public DateTime? 削除日付 { get; set; }
	}

    // 検索画面用データメンバー定義
    [DataContract]
    public class M10_UHK_SCH_Member {
		[DataMember] public int 請求内訳ID { get; set; }
		[DataMember] public string 請求内訳名 { get; set; }
		[DataMember] public string かな読み { get; set; }
	}

}
