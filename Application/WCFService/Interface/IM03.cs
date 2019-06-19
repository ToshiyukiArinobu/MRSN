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
    public interface IM03 {

        /// <summary>
        /// M03_YNENのデータ取得
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p適用開始日">適用開始日</param>
        /// <returns>M03_YNEN_Member</returns>
		[OperationContract]
        List<M03_YNEN_Member> GetDataYnen(int p取引先ID, DateTime p適用開始日);

        /// <summary>
        /// M03_YNENの新規追加
        /// </summary>
        /// <param name="m03ynen">M03_YNEN_Member</param>
		[OperationContract]
        void InsertYnen(M03_YNEN_Member m03ynen);

        /// <summary>
        /// M03_YNENの更新
        /// </summary>
        /// <param name="m03ynen">M03_YNEN_Member</param>
		[OperationContract]
        void UpdateYnen(M03_YNEN_Member m03ynen);

        /// <summary>
        /// M03_YNENの物理削除
        /// </summary>
        /// <param name="m03ynen">M03_YNEN_Member</param>
		[OperationContract]
        void DeleteYnen(M03_YNEN_Member m03ynen);

        /// <summary>
        /// M03_YTAN1のデータ取得《MST19010 マスター用》
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p発地ID">発地ID</param>
        /// <param name="p着地ID">着地ID</param>
        /// <param name="p商品ID">商品ID</param>
        /// <returns>M03_YTAN1_Member</returns>
		[OperationContract]
        List<M03_YTAN1_Member> GetDataYtan1(int p取引先ID, int p発地ID, int p着地ID, int p商品ID);

        /// <summary>
        /// M03_YTAN1のデータ取得《MST19020 一覧表用、プレビュー用》
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p発地ID">発地ID</param>
        /// <param name="p着地ID">着地ID</param>
        /// <param name="p商品ID">商品ID</param>
        /// <returns>M03_YTAN1_Member</returns>
        [OperationContract]
        List<M03_YTAN1_ichiran_Member> GetDataYtan_Ichiran_Pre(string p取引先ID, string p発地ID, string p着地ID, string p商品ID);

        /// <summary>
        /// M03_YTAN1の新規追加
        /// </summary>
        /// <param name="m03ytan1">M03_YTAN1_Member</param>
		[OperationContract]
        void InsertYtan1(M03_YTAN1_Member m03ytan1);

        /// <summary>
        /// M03_YTAN1の更新
        /// </summary>
        /// <param name="m03ytan1">M03_YTAN1_Member</param>
		[OperationContract]
        void UpdateYtan1(int p取引先ID, int p発地ID, int p着地ID, int p商品ID, decimal? p支払単価, string p計算区分);

        /// <summary>
        /// M03_YTAN1の物理削除
        /// </summary>
        /// <param name="m03ytan1">M03_YTAN1_Member</param>
		[OperationContract]
        void DeleteYtan1(int p取引先ID, int p発地ID, int p着地ID, int p商品ID);
        
        /// <summary>
        /// M03_YTAN2のデータ取得
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p車種ID">p車種ID</param>
        /// <param name="p発地ID">発地ID</param>
        /// <param name="p着地ID">着地ID</param>
        /// <returns>M03_YTAN2_Member</returns>
		[OperationContract]
        List<M03_YTAN2_Member> GetDataYtan2(int p取引先ID, int p車種ID, int p発地ID, int p着地ID);


        /// <summary>
        /// M03_YTAN2の更新
        /// </summary>
        /// <param name="m03ytan2">M03_YTAN2_Member</param>
		[OperationContract]
        void UpdateYtan2(int p取引先ID, int p車種ID, int p発地ID, int p着地ID, decimal ip売上単価);

        /// <summary>
        /// M03_YTAN2の物理削除
        /// </summary>
        /// <param name="m03ytan2">M03_YTAN2_Member</param>
		[OperationContract]
        void DeleteYtan2(int p取引先ID, int p車種ID, int p発地ID, int p着地ID);

        /// <summary>
        /// M03_YTAN2のプレビュー、CSV出力データ取得
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p車種ID">p車種ID</param>
        /// <param name="p発地ID">発地ID</param>
        /// <param name="p着地ID">着地ID</param>
        /// <returns>M03_YTAN2_Member</returns>
        [OperationContract]
        List<M03_YTAN2_Member_Preview_csv> GetSearchListData(string p取引先ID, string p車種ID, string p発地ID, string p着地ID);
        
        
    }

    // データメンバー定義
    [DataContract]
    public class M03_YNEN_Member {
		[DataMember] public int 取引先ID { get; set; }   
		[DataMember] public DateTime 適用開始日 { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public decimal 軽油単価 { get; set; }
		[DataMember] public decimal 軽油税単価 { get; set; }
	}
    //マスター用メンバー
    [DataContract]
    public class M03_YTAN1_Member {
		[DataMember] public int 取引先ID { get; set; }
		[DataMember] public int 発地ID { get; set; }
		[DataMember] public int 着地ID { get; set; }
		[DataMember] public int 商品ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public decimal? 支払単価 { get; set; }
		[DataMember] public int? 計算区分 { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }

	}

    //MST19020 一覧データ、プレビュー、CSV出力用のメンバー
    [DataContract]
    public class M03_YTAN1_ichiran_Member {
		[DataMember] public int 取引先ID { get; set; }
        [DataMember] public string 支払先名１ { get; set; }
        [DataMember] public string 支払先名２ { get; set; }
		[DataMember] public int 発地ID { get; set; }
        [DataMember] public string 発地名 { get; set; }
		[DataMember] public int 着地ID { get; set; }
        [DataMember] public string 着地名 { get; set; }
		[DataMember] public int 商品ID { get; set; }
        [DataMember] public string 商品名 { get; set; }
		[DataMember] public decimal? 支払単価 { get; set; }
        [DataMember] public int? 計算区分 { get; set; }
        [DataMember] public string 計算区分名称 { get; set; }
       
	}


    [DataContract]
    public class M03_YTAN2_Member_Preview_csv {
		[DataMember] public int 取引先ID { get; set; }
        [DataMember] public string 取引名１ { get; set; }
        [DataMember] public string 取引名２ { get; set; }
		[DataMember] public int 車種ID { get; set; }
        [DataMember] public string 車種名 { get; set; }
		[DataMember] public int 発地ID { get; set; }
        [DataMember] public string 発地名 { get; set; }
		[DataMember] public int 着地ID { get; set; }
        [DataMember] public string 着地名 { get; set; }
		[DataMember] public decimal 支払単価 { get; set; }
	}


    //SCH19010検索用
    [DataContract]
    public class M03_YTAN1_SCH_Member {
        [DataMember] public int 取引先ID { get; set; }
        [DataMember] public string 取引先名 { get; set; }
        [DataMember] public int 発地ID { get; set; }
        [DataMember] public string 発地名 { get; set; }
        [DataMember] public int 着地ID { get; set; }
        [DataMember] public string 着地名 { get; set; }
        [DataMember] public int 商品ID { get; set; }
        [DataMember] public string 商品名 { get; set; }

    }

    [DataContract]
    public class M03_YTAN2_Member {
		[DataMember] public int 取引先ID { get; set; }
		[DataMember] public int 車種ID { get; set; }
		[DataMember] public int 発地ID { get; set; }
		[DataMember] public int 着地ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public decimal 支払単価 { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
	}

    [DataContract]
    public class M03_YTAN3_Member {
		[DataMember] public int 取引先ID { get; set; }
		[DataMember] public int 距離 { get; set; }
		[DataMember] public decimal 重量 { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public int? 運賃 { get; set; }
	}

    [DataContract]
    public class M03_YTAN4_Member {
		[DataMember] public int 取引先ID { get; set; }
		[DataMember] public decimal 重量 { get; set; }
		[DataMember] public decimal 個数 { get; set; }
		[DataMember] public int 着地ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public decimal? 個建単価 { get; set; }
		[DataMember] public int? 個建金額 { get; set; }
	}

    //***支払先個建単価メンバー***//
    [DataContract]
    public class M03_YTAN4_MEMBER
    {
		[DataMember] public int 取引先ID { get; set; }
        [DataMember] public string 取引先名 { get; set; }
		[DataMember] public decimal 重量 { get; set; }
		[DataMember] public decimal 個数 { get; set; }
		[DataMember] public int 着地ID { get; set; }
        [DataMember] public string 着地名 { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public decimal? 個建単価 { get; set; }
        [DataMember] public int? 個建金額 { get; set; }
        [DataMember] public decimal? d個建金額 { get; set; }
        [DataMember] public int? 運賃 { get; set; }
        [DataMember] public decimal? d運賃 { get; set; }
        [DataMember] public string S_個建単価 { get; set; }
        [DataMember] public string S_個建金額 { get; set; }
        [DataMember] public string S_運賃 { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
	}

    //***支払先個建単価印刷csv出力メンバー***//
    [DataContract]
    public class M03_YTAN4_Member_Preview_csv
    {
		[DataMember] public int 支払先ID { get; set; }
        [DataMember] public string 得意先１ { get; set; }
        [DataMember] public string 得意先２ { get; set; }
		[DataMember] public decimal 重量 { get; set; }
        [DataMember] public decimal 個数 { get; set; }
		[DataMember] public int 着地ID { get; set; }
        [DataMember] public string 着地名 { get; set; }
		[DataMember] public decimal? 個建単価 { get; set; }
        [DataMember] public int? 個建金額 { get; set; }
	}

    //***支払先別距離別単価マスター印刷csv出力メンバー***//
    [DataContract]
    public class M03_YTAN3_Member_Preview_csv
    {
		[DataMember] public int 支払先ID { get; set; }
        [DataMember] public string 支払先名１ { get; set; }
        [DataMember] public string 支払先名２ { get; set; }
        [DataMember] public int 距離 { get; set; }
        [DataMember] public decimal 重量 { get; set; }
		[DataMember] public int? 支払運賃 { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
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
