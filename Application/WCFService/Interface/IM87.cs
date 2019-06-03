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
    public interface IM87 {

        /// <summary>
        /// M87_CNTLのデータ取得
        /// </summary>
        /// <param name="p管理ID">管理ID</param>
        /// <returns>M87_CNTL_Member</returns>
        [OperationContract]
        List<M87_CNTL_Member> GetData(int? p管理ID, int pOption);

        ///// <summary>
        ///// M87_CNTLの新規追加
        ///// </summary>
        ///// <param name="m87cntl">M87_CNTL_Member</param>
        //[OperationContract]
        //void Insert(M87_CNTL_Member m87cntl);

        /// <summary>
        /// M87_CNTLの更新
        /// </summary>
        /// <param name="m87cntl">M87_CNTL_Member</param>
        //[OperationContract]
        //void Update(M87_CNTL_Member m87cntl);

        ///// <summary>
        ///// M87_CNTLの物理削除
        ///// </summary>
        ///// <param name="m87cntl">M87_CNTL_Member</param>
        //[OperationContract]
        //void Delete(M87_CNTL_Member m87cntl);

    }

    // データメンバー定義
    [DataContract]
    public class M87_CNTL_Member {
		[DataMember] public int 管理ID { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public int? 得意先管理処理年月 { get; set; }
		[DataMember] public int? 支払先管理処理年月 { get; set; }
		[DataMember] public int? 車輌管理処理年月 { get; set; }
		[DataMember] public int? 運転者管理処理年月 { get; set; }
		[DataMember] public int? 更新年月 { get; set; }
		[DataMember] public int? 決算月 { get; set; }
		[DataMember] public int? 得意先自社締日 { get; set; }
		[DataMember] public int? 支払先自社締日 { get; set; }
		[DataMember] public int? 運転者自社締日 { get; set; }
		[DataMember] public int? 車輌自社締日 { get; set; }
		[DataMember] public int? 自社支払日 { get; set; }
		[DataMember] public int? 自社サイト { get; set; }
		[DataMember] public int? 未定区分 { get; set; }
		[DataMember] public int? 部門管理区分 { get; set; }
		[DataMember] public string 割増料金名１ { get; set; }
		[DataMember] public string 割増料金名２ { get; set; }
        [DataMember] public string 確認名称 { get; set; }
		[DataMember] public int? 得意先ID区分 { get; set; }
		[DataMember] public int? 支払先ID区分 { get; set; }
		[DataMember] public int? 乗務員ID区分 { get; set; }
		[DataMember] public int? 車輌ID区分 { get; set; }
		[DataMember] public int? 車種ID区分 { get; set; }
        [DataMember] public int? 発着地ID区分 { get; set; }
        [DataMember] public int? 品名ID区分 { get; set; }
        [DataMember] public int? 摘要ID区分 { get; set; }
		[DataMember] public int? 期首年月 { get; set; }
        [DataMember] public int? 売上消費税端数区分 { get; set; }
        [DataMember] public int? 支払消費税端数区分 { get; set; }
        [DataMember] public int? 金額計算端数区分 { get; set; }
        [DataMember]
        public int? 出力プリンター設定 { get; set; }
        [DataMember]
        public int? 自動学習区分 { get; set; }
        [DataMember]
        public int? 月次集計区分 { get; set; }
        [DataMember]
        public int? 距離転送区分 { get; set; }
        [DataMember]
        public int? 番号通知区分 { get; set; }
        [DataMember]
        public int? 通行料転送区分 { get; set; }
        [DataMember]
        public int? 路線計算区分 { get; set; }
        [DataMember]
        public int? Ｇ期首月日 { get; set; }
        [DataMember]
        public int? Ｇ期末月日 { get; set; }
        [DataMember]
        public int? 請求書区分 { get; set; }
        [DataMember]
        public DateTime? 削除日付 { get; set; }





	}

}
