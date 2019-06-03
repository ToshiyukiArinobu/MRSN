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
    public interface IM74 {

        /// <summary>
        /// M74_KGRPのデータ取得
        /// </summary>
        /// <param name="p適用開始日付">適用開始日付</param>
        /// <returns>M74_KGRP_Member</returns>
        [OperationContract]
        //M74_KGRP_Member GetData(int pグループ権限ID);
        List<M74_KGRP_DISP_Member> GetData(int pグループ権限ID);

        /// <summary>
        /// M74_KGRPの新規追加
        /// </summary>
        /// <param name="M74kgrp">M74_KGRP_Member</param>
        [OperationContract]
        void Insert(M74_KGRP_Member M74kgrp);

        /// <summary>
        /// M74_KGRPの更新
        /// </summary>
        /// <param name="M74kgrp">M74_KGRP_Member</param>
        [OperationContract]
        void Update(M74_KGRP_Member M74kgrp);

        /// <summary>
        /// M74_KGRPの物理削除
        /// </summary>
        /// <param name="M74kgrp">M74_KGRP_Member</param>
        [OperationContract]
        void Delete(M74_KGRP_Member M74kgrp);

    }

    // データメンバー定義
    [DataContract]
    public class M74_KGRP_Member {
		[DataMember] public int グループ権限ID { get; set; }
        [DataMember] public string プログラムID { get; set; }
        [DataMember] public Boolean 使用可能FLG { get; set; }
        [DataMember] public Boolean データ更新FLG { get; set; }      
        [DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
    }

    // データメンバー定義
    [DataContract(Name = "M74_KGRP_DISP_Member")]
    public class M74_KGRP_DISP_Member
    {
        [DataMember] public int TabID { get; set; }
        [DataMember] public string メニュー名称 { get; set; }
        [DataMember] public int? No { get; set; }
        [DataMember] public string プログラム名称 { get; set; }
        [DataMember] public int グループ権限ID { get; set; }
        [DataMember] public string プログラムID { get; set; }
        [DataMember] public Boolean 使用可能FLG { get; set; }
        [DataMember] public Boolean データ更新FLG { get; set; }
        [DataMember] public DateTime? 登録日時 { get; set; }
        [DataMember] public DateTime? 更新日時 { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
    }

}
