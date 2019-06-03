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
    public interface IM02 {

        /// <summary>
        /// M02_TTAN1のデータ取得《MST19010 マスター用》
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p発地ID">発地ID</param>
        /// <param name="p着地ID">着地ID</param>
        /// <param name="p商品ID">商品ID</param>
        /// <returns>M03_YTAN1_Member</returns>
        [OperationContract]
        List<M02_TTAN1_Member> GetDataTtan1(int p取引先ID, int p発地ID, int p着地ID, int p商品ID);

        /// <summary>
        /// M02_TTAN1のデータ取得《MST19020 一覧表用、プレビュー用》
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p発地ID">発地ID</param>
        /// <param name="p着地ID">着地ID</param>
        /// <param name="p商品ID">商品ID</param>
        /// <returns>M03_YTAN1_Member</returns>
        [OperationContract]
        List<M02_TTAN1_ichiran_Member> GetDataTtan_Ichiran_Pre(string p取引先ID, string p発地ID, string p着地ID, string p商品ID);

        /// <summary>
        /// M02_TTAN1の新規追加
        /// </summary>
        /// <param name="m03ytan1">M03_YTAN1_Member</param>
        [OperationContract]
        void InsertTtan1(M02_TTAN1_Member m02ttan1);

        ///// <summary>
        ///// M02_TTAN1の更新
        ///// </summary>
        ///// <param name="m03ytan1">M03_YTAN1_Member</param>
        //[OperationContract]
        //void UpdateTtan1(int p取引先ID, int p発地ID, int p着地ID, int p商品ID, decimal? p売上単価, string p計算区分);

        /// <summary>
        /// M02_TTAN1の物理削除
        /// </summary>
        /// <param name="m03ytan1">M03_YTAN1_Member</param>
        [OperationContract]
        void DeleteTtan1(int p取引先ID, int p発地ID, int p着地ID, int p商品ID);


        ///// <summary>
        ///// M02_TTAN1の新規追加
        ///// </summary>
        ///// <param name="m02ttan1">M02_TTAN1_Member</param>
        //[OperationContract]
        //void Insert(M02_TTAN1_Member m02ttan1);

        ///// <summary>
        ///// M02_TTAN1の更新
        ///// </summary>
        ///// <param name="m02ttan1">M02_TTAN1_Member</param>
        //[OperationContract]
        //void Update(M02_TTAN1_Member m02ttan1);

        ///// <summary>
        ///// M02_TTAN1の物理削除
        ///// </summary>
        ///// <param name="m02ttan1">M02_TTAN1_Member</param>
        //[OperationContract]
        //void Delete(M02_TTAN1_Member m02ttan1);

        /// <summary>
        /// M02_TTAN2のデータ取得
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p発地ID">発地ID</param>
        /// <param name="p着地ID">着地ID</param>
        /// <param name="p商品ID">商品ID</param>
        /// <returns>M02_TTAN2_Member</returns>
        [OperationContract]
        List<M02_TTAN2_Member> GetDataTTAN2(int p取引先ID, int p車種ID, int p発地ID, int p着地ID);

        /// <summary>
        /// M02_TTAN2の新規追加
        /// </summary>
        /// <param name="m02ttan2">M02_TTAN2_Member</param>
        [OperationContract]
        void InsertTTAN2(M02_TTAN2_Member m02ttan2);

        /// <summary>
        /// M02_TTAN2の新規・更新
        /// </summary>
        ///// <param name="m02ttan2">M02_TTAN2_Member</param>
        [OperationContract]
        void UpdateTTAN2(int p取引先ID, int p車種ID, int p発地ID, int p着地ID, decimal ip支払単価);

        /// <summary>
        /// M02_TTAN2の物理削除
        /// </summary>
        /// <param name="m02ttan2">M02_TTAN2_Member</param>
        [OperationContract]
        void DeleteTTAN2(int p取引先ID, int p車種ID, int p発地ID, int p着地ID);

        /// <summary>
        /// M02_TTAN2のプレビュー
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p発地ID">発地ID</param>
        /// <param name="p着地ID">着地ID</param>
        /// <param name="p商品ID">商品ID</param>
        /// <returns>M02_TTAN2_Member</returns>
        [OperationContract]
        List<M02_TTAN2_Member_Preview_csv> GetSearchListData(string p取引先ID, string p車種ID, string p発地ID, string p着地ID);

        /// <summary>
        /// M02_TTAN3のデータ取得
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p距離">距離</param>
        /// <param name="p重量">重量</param>
        /// <returns>M02_TTAN3_Member</returns>
        [OperationContract]
        List<M02_TTAN3_Member> GetDataTTAN3(int p取引先ID, int p距離, decimal p重量);

        /// <summary>
        /// M02_TTAN3の新規追加
        /// </summary>
        /// <param name="m02ttan3">M02_TTAN3_Member</param>
        [OperationContract]
        void InsertTTAN3(M02_TTAN3_Member m02ttan3);

        /// <summary>
        /// M02_TTAN3の更新
        /// </summary>
        /// <param name="m02ttan3">M02_TTAN3_Member</param>
        [OperationContract]
        void UpdateTTAN3(M02_TTAN3_Member m02ttan3);

        /// <summary>
        /// M02_TTAN3の物理削除
        /// </summary>
        /// <param name="m02ttan3">M02_TTAN3_Member</param>
        [OperationContract]
        void DeleteTTAN3(M02_TTAN3_Member m02ttan3);

        /// <summary>
        /// M02_TTAN4のデータ取得
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p距離">距離</param>
        /// <param name="p重量">重量</param>
        /// <returns>M02_TTAN4_Member</returns>
        [OperationContract]
        List<M02_TTAN4_Member> GetDataTTAN4(int? p取引先ID, decimal? p重量, decimal? p個数, int? p着地ID, bool 重量チェックボックス値, bool 個数チェックボックス値);

        /// <summary>
        /// M02_TTAN4の新規追加
        /// </summary>
        /// <param name="m02ttan4">M02_TTAN4_Member</param>
        [OperationContract]
        void InsertTTAN4(M02_TTAN4_Member m02ttan4);

        /// <summary>
        /// M02_TTAN4の新規・更新
        /// </summary>
        /// <param name="m02ttan4">M02_TTAN4_Member</param>
        [OperationContract]
        void UpdateTTAN4(int p取引先ID, int p着地ID, decimal p重量, decimal p個数, decimal 個建単価, int 個建金額);

        /// <summary>
        /// M02_TTAN4の新規・更新
        /// </summary>
        /// <param name="m02ttan4">M02_TTAN4_Member</param>
        //[OperationContract]
        //void spUpdateTTAN4(int p取引先ID, decimal p重量, decimal p個数, int p着地ID, decimal 個建単価, int 個建金額);

        /// <summary>
        /// M02_TTAN4の物理削除
        /// </summary>
        /// <param name="m02ttan4">M02_TTAN4_Member</param>
        [OperationContract]
        void DeleteTTAN4(int p取引先ID, int p着地ID, decimal p重量, decimal p個数);

        /// <summary>
        /// M02_TTAN2のプレビュー
        /// </summary>
        /// <param name="p取引先ID">取引先ID</param>
        /// <param name="p着地ID">着地ID</param>
        /// <returns>M02_TTAN2_Member</returns>
        [OperationContract]
        List<M02_TTAN4_Member_Preview_csv> GetSearchListData_M02_TTAN4(string p取引先ID, string p着地ID);

    }

    //マスター用メンバー
    [DataContract]
    public class M02_TTAN1_Member
    {
        [DataMember]
        public int 取引先ID { get; set; }
        [DataMember]
        public int 発地ID { get; set; }
        [DataMember]
        public int 着地ID { get; set; }
        [DataMember]
        public int 商品ID { get; set; }
        [DataMember]
        public DateTime? 登録日時 { get; set; }
        [DataMember]
        public DateTime? 更新日時 { get; set; }
        [DataMember]
        public decimal? 売上単価 { get; set; }
        [DataMember]
        public int? 計算区分 { get; set; }
        [DataMember]
        public DateTime? 削除日付 { get; set; }

    }

    //MST19020 一覧データ、プレビュー、CSV出力用のメンバー
    [DataContract]
    public class M02_TTAN1_ichiran_Member
    {
        [DataMember] public int 取引先ID { get; set; }
        [DataMember] public string 得意先名１ { get; set; }
        [DataMember] public string 得意先名２ { get; set; }
        [DataMember] public int 発地ID { get; set; }
        [DataMember] public string 発地名 { get; set; }
        [DataMember] public int 着地ID { get; set; }
        [DataMember] public string 着地名 { get; set; }
        [DataMember] public int 商品ID { get; set; }
        [DataMember] public string 商品名 { get; set; }
        [DataMember] public decimal? 売上単価 { get; set; }
        [DataMember] public int? 計算区分 { get; set; }
        [DataMember] public string 計算区分名称 { get; set; }

    }



    [DataContract]
    public class M02_TTAN2_Member {
		[DataMember] public int 取引先ID { get; set; }
		[DataMember] public int 車種ID { get; set; }
		[DataMember] public int 発地ID { get; set; }
		[DataMember] public int 着地ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public decimal 売上単価 { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
	}

    //MST19020 印刷プレビューメンバー

    [DataContract]
    public class M02_TTAN2_Member_Preview_csv {
		[DataMember] public int 取引先ID { get; set; }
        [DataMember] public string 取引名１ { get; set; }
        [DataMember] public string 取引名２ { get; set; }
		[DataMember] public int 車種ID { get; set; }
        [DataMember] public string 車種名 { get; set; }
		[DataMember] public int 発地ID { get; set; }
        [DataMember] public string 発地名 { get; set; }
		[DataMember] public int 着地ID { get; set; }
        [DataMember] public string 着地名 { get; set; }
		[DataMember] public decimal 売上単価 { get; set; }
	}

    [DataContract]
    public class M02_TTAN3_Member {
		[DataMember] public int 取引先ID { get; set; }
		[DataMember] public int 距離 { get; set; }
		[DataMember] public decimal 重量 { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public int? 運賃 { get; set; }
        [DataMember] public string 取引先名1 { get; set; }
        [DataMember] public string 取引先名2 { get; set; }

	}

    [DataContract]
    public class M02_TTAN4_Member {
		[DataMember] public int 取引先ID { get; set; }
        [DataMember] public string 取引先名 { get; set; }
		[DataMember] public decimal 重量 { get; set; }
		[DataMember] public decimal 個数 { get; set; }
		[DataMember] public int 着地ID { get; set; }
        [DataMember] public string 着地名 { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public decimal 個建単価 { get; set; }
        [DataMember] public int 個建金額 { get; set; }
        [DataMember] public decimal d個建金額 { get; set; }
        [DataMember] public int 運賃 { get; set; }
        [DataMember] public decimal d運賃 { get; set; }
        [DataMember] public string S_個建単価 { get; set; }
        [DataMember] public string S_個建金額 { get; set; }
        [DataMember] public string S_運賃 { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
	}

    [DataContract]
    public class M02_TTAN4_Member_Preview_csv {
		[DataMember] public int 得意先ID { get; set; }
        [DataMember] public string 得意先１ { get; set; }
        [DataMember] public string 得意先２ { get; set; }
		[DataMember] public decimal 重量 { get; set; }
        [DataMember] public decimal 個数 { get; set; }
		[DataMember] public int 着地ID { get; set; }
        [DataMember] public string 着地名 { get; set; }
		[DataMember] public decimal 個建単価 { get; set; }
        [DataMember] public int 個建金額 { get; set; }
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
