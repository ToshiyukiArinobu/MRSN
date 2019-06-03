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
    public interface IM01 {

        /// <summary>
        /// M01_TOKのデータ取得
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <returns>M01_TOK_Member</returns>
        [OperationContract]
		List<M01_TOK_Member> GetDataTok(int? p取引先ID, int? pオプションコード);

		/// <summary>
		/// M01_TOKのデータ取得
		/// </summary>
		/// <param name="p取引先FROM">取引先ID</param>
		/// <param name="p取引先TO">取引先ID</param>
		/// <returns>M01_TOK_Member</returns>
		[OperationContract]
		List<M01_TOK_Member> GetRange(int p取引先FROM, int p取引先TO);

		/// <summary>
		/// M01_TOKの名前取得
		/// </summary>
		/// <param name="p取引先ID">取引先ID</param>
		/// <returns>M01_TOK_Member</returns>
		[OperationContract]
		List<M01_TOK_Member> GetName(int p取引先ID);

        /// <summary>
        /// 得意先検索画面用
        /// </summary>
        /// <param name="pかな読み"></param>
        /// <param name="p取引先ID"></param>
        /// <returns></returns>
        List<M01_TOK_Member_SCH> GetTokuDataSCH(string pかな読み, int p取引先ID);

        /// <summary>
        /// 支払先検索画面用
        /// </summary>
        /// <param name="pかな読み"></param>
        /// <param name="p取引先ID"></param>
        /// <returns></returns>
        List<M01_TOK_Member_SCH> GetShiharaiDataSCH(string pかな読み, int p取引先ID);

		/// <summary>
        /// M01_TOKの新規追加
        /// </summary>
        /// <param name="m01tok">M01_TOK_Member</param>
        [OperationContract]
        int Insert(M01_TOK_Member m01tok, bool pMaintenanceFlg, bool pGetNextNumber);

        /// <summary>
        /// M01_TOKの更新
        /// </summary>
        /// <param name="m01tok">M01_TOK_Member</param>
        void Update(M01_TOK_Member m01tok);

        /// <summary>
        /// M01_TOKの物理削除
        /// </summary>
        /// <param name="m01tok">M01_TOK_Member</param>
        void Delete(M01_TOK_Member m01tok);

    }

    // データメンバー定義
    /// <summary>
    /// M01_TOK_Member
    /// </summary>
    [DataContract]
    public class M01_TOK_Member {
		[DataMember] public int 取引先ID { get; set; }
		[DataMember] public int 得意先KEY { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
	    [DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public string 取引先名１ { get; set; }
		[DataMember] public string 取引先名２ { get; set; }
	    [DataMember] public string 略称名 { get; set; }
	    [DataMember] public int 取引区分 { get; set; }
	    [DataMember] public int Ｔ締日 { get; set; }
	    [DataMember] public int Ｓ締日 { get; set; }
	    [DataMember] public string かな読み { get; set; }
	    [DataMember] public string 郵便番号 { get; set; }
	    [DataMember] public string 住所１ { get; set; }
	    [DataMember] public string 住所２ { get; set; }
	    [DataMember] public string 電話番号 { get; set; }
	    [DataMember] public string ＦＡＸ { get; set; }
	    [DataMember] public int Ｔ集金日 { get; set; }
	    [DataMember] public int Ｔサイト日 { get; set; }
	    [DataMember] public int Ｔ税区分ID { get; set; }
	    [DataMember] public int Ｔ締日期首残 { get; set; }
	    [DataMember] public int Ｔ月次期首残 { get; set; }
	    [DataMember] public int Ｔ路線計算年度 { get; set; }
	    [DataMember] public int Ｔ路線計算率 { get; set; }
	    [DataMember] public int Ｔ路線計算まるめ { get; set; }
	    [DataMember] public int Ｓ集金日 { get; set; }
	    [DataMember] public int Ｓサイト日 { get; set; }
	    [DataMember] public int Ｓ税区分ID { get; set; }
	    [DataMember] public int Ｓ締日期首残 { get; set; }
	    [DataMember] public int Ｓ月次期首残 { get; set; }
	    [DataMember] public int Ｓ路線計算年度 { get; set; }
	    [DataMember] public int Ｓ路線計算率 { get; set; }
	    [DataMember] public int Ｓ路線計算まるめ { get; set; }
	    [DataMember] public int ラベル区分 { get; set; }
	    [DataMember] public int? 親子区分ID { get; set; }
	    [DataMember] public int? 親ID { get; set; }
	    [DataMember] public int 請求内訳管理区分 { get; set; }
	    [DataMember] public int? 自社部門ID { get; set; }
	    [DataMember] public int 請求運賃計算区分ID { get; set; }
	    [DataMember] public int 支払運賃計算区分ID { get; set; }
	    [DataMember] public int 請求書区分ID { get; set; }
	    [DataMember] public int? 請求書発行元ID { get; set; }
	    [DataMember] public DateTime? 削除日付 { get; set; }
        [DataMember] public Boolean  取引停止区分 { get; set; } // 追加
    }

    // データメンバー定義
    /// <summary>
    /// M01_KISember
    /// </summary>
    [DataContract]
    public class M01_KIS_Member
    {
        [DataMember]
        public int? 期首年月 { get; set; }
        [DataMember]
        public int 取引先ID { get; set; }
        [DataMember]
        public string 取引先名 { get; set; }
        [DataMember]
        public string 取引区分 { get; set; }
        [DataMember]
        public decimal Ｔ締日期首残 { get; set; }
        [DataMember]
        public decimal Ｔ月次期首残 { get; set; }
        [DataMember]
        public decimal Ｓ締日期首残 { get; set; }
        [DataMember]
        public decimal Ｓ月次期首残 { get; set; }
        [DataMember]
        public DateTime? 登録日時 { get; set; }
        [DataMember]
        public DateTime? 更新日時 { get; set; }
        [DataMember]
        public Boolean Ｔ締日期首残EditFlg { get; set; }
        [DataMember]
        public Boolean Ｔ月次期首残EditFlg { get; set; }
        [DataMember]
        public Boolean Ｓ締日期首残EditFlg { get; set; }
        [DataMember]
        public Boolean Ｓ月次期首残EditFlg { get; set; }
    }

    // データメンバー定義
    /// <summary>
    /// M01_DEL_TOK_Member
    /// </summary>
    [DataContract]
    public class M01_DEL_TOK_Member
    {
        [DataMember]
        public bool ResurrectionCheckbox { get; set; }
        [DataMember]
        public int 取引先ID { get; set; }
        [DataMember]
        public int 取引先KEY { get; set; }
        [DataMember]
        public DateTime? 登録日時 { get; set; }
        [DataMember]
        public DateTime? 更新日時 { get; set; }
        [DataMember]
        public string 取引先名１ { get; set; }
        [DataMember]
        public string 取引先名２ { get; set; }
        [DataMember]
        public string 略称名 { get; set; }
        [DataMember]
        public int 取引区分 { get; set; }
        [DataMember]
        public string 取引区分名称 { get; set; }
        [DataMember]
        public int Ｔ締日 { get; set; }
        [DataMember]
        public int Ｓ締日 { get; set; }
        [DataMember]
        public string かな読み { get; set; }
        [DataMember]
        public string 郵便番号 { get; set; }
        [DataMember]
        public string 住所１ { get; set; }
        [DataMember]
        public string 住所２ { get; set; }
        [DataMember]
        public string 電話番号 { get; set; }
        [DataMember]
        public string ＦＡＸ { get; set; }
        [DataMember]
        public int Ｔ集金日 { get; set; }
        [DataMember]
        public int Ｔサイト日 { get; set; }
        [DataMember]
        public int Ｔ税区分ID { get; set; }
        [DataMember]
        public int Ｔ締日期首残 { get; set; }
        [DataMember]
        public int Ｔ月次期首残 { get; set; }
        [DataMember]
        public int Ｔ路線計算年度 { get; set; }
        [DataMember]
        public int Ｔ路線計算率 { get; set; }
        [DataMember]
        public int Ｔ路線計算まるめ { get; set; }
        [DataMember]
        public int Ｓ集金日 { get; set; }
        [DataMember]
        public int Ｓサイト日 { get; set; }
        [DataMember]
        public int Ｓ税区分ID { get; set; }
        [DataMember]
        public int Ｓ締日期首残 { get; set; }
        [DataMember]
        public int Ｓ月次期首残 { get; set; }
        [DataMember]
        public int Ｓ路線計算年度 { get; set; }
        [DataMember]
        public int Ｓ路線計算率 { get; set; }
        [DataMember]
        public int Ｓ路線計算まるめ { get; set; }
        [DataMember]
        public int ラベル区分 { get; set; }
        [DataMember]
        public int? 親子区分ID { get; set; }
        [DataMember]
        public int? 親ID { get; set; }
        [DataMember]
        public int 請求内訳管理区分 { get; set; }
        [DataMember]
        public int? 自社部門ID { get; set; }
        [DataMember]
        public int 請求運賃計算区分ID { get; set; }
        [DataMember]
        public int 支払運賃計算区分ID { get; set; }
        [DataMember]
        public int 請求書区分ID { get; set; }
        [DataMember]
        public int? 請求書発行元ID { get; set; }
        [DataMember]
        public DateTime? 削除日付 { get; set; }
        [DataMember]
        public string 法人ナンバー { get; set; }
    }
    // データメンバー定義
    /// <summary>
    /// M01_KEY_TOK_Member
    /// </summary>
    [DataContract]
    public class M01_KEY_TOK_Member
    {
        [DataMember]
        public int KEY_ID { get; set; }
    }

    /// <summary>
    /// M01_TOK_Member_SCH
    /// </summary>
    [DataContract]
    public class M01_TOK_Member_SCH
    {
        [DataMember]
        public int 取引先ID { get; set; }
        [DataMember]
        public string 取引先名１ { get; set; }
        [DataMember]
        public string かな読み { get; set; }
        // 追加
        [DataMember]
        public int 取引停止区分 { get; set; }
    }

    [DataContract]
    public class M01_TOK_AllMember_SCH
    {
        [DataMember]
        public int 締日 { get; set; }
        [DataMember]
        public int 集金日 { get; set; }
        [DataMember]
        public int サイト { get; set; }
    }

}
