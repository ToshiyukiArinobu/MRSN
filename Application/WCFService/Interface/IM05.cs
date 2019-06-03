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
    public interface IM05 {

    }
    
    // データメンバー定義
    [DataContract]
    public class M05_CAR_Member {
		[DataMember] public int 車輌ID { get; set; }
        [DataMember] public int 車輌KEY { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public string 車輌番号 { get; set; }
		[DataMember] public int? 自社部門ID { get; set; }
        [DataMember] public int? 車種ID { get; set; }
		[DataMember] public int? 乗務員ID { get; set; }
		[DataMember] public int 乗務員KEY { get; set; }
		[DataMember] public int 運輸局ID { get; set; }
		[DataMember] public int 自傭区分 { get; set; }
		[DataMember] public int 廃車区分 { get; set; }
		[DataMember] public string 廃車区分表示 { get; set; }        
		[DataMember] public DateTime? 廃車日 { get; set; }
		[DataMember] public DateTime? 次回車検日 { get; set; }
		[DataMember] public string 車輌登録番号 { get; set; }
		[DataMember] public DateTime? 登録日 { get; set; }
		[DataMember] public int 初年度登録年 { get; set; }
		[DataMember] public int 初年度登録月 { get; set; }
		[DataMember] public string 自動車種別 { get; set; }
		[DataMember] public string 用途 { get; set; }
		[DataMember] public string 自家用事業用 { get; set; }
		[DataMember] public string 車体形状 { get; set; }
		[DataMember] public string 車名 { get; set; }
		[DataMember] public string 型式 { get; set; }
		[DataMember] public int 乗車定員 { get; set; }
		[DataMember] public int 車輌重量 { get; set; }
		[DataMember] public int 車輌最大積載量 { get; set; }
		[DataMember] public int 車輌総重量 { get; set; }
		[DataMember] public int 車輌実積載量 { get; set; }
		[DataMember] public string 車台番号 { get; set; }
		[DataMember] public string 原動機型式 { get; set; }
		[DataMember] public int 長さ { get; set; }
		[DataMember] public int 幅 { get; set; }
		[DataMember] public int 高さ { get; set; }
		[DataMember] public int 総排気量 { get; set; }
		[DataMember] public string 燃料種類 { get; set; }
		[DataMember] public int 現在メーター { get; set; }
		[DataMember] public int デジタコCD { get; set; }
		[DataMember] public string 所有者名 { get; set; }
		[DataMember] public string 所有者住所 { get; set; }
		[DataMember] public string 使用者名 { get; set; }
		[DataMember] public string 使用者住所 { get; set; }
		[DataMember] public string 使用本拠位置 { get; set; }
		[DataMember] public string 備考 { get; set; }
		[DataMember] public string 型式指定番号 { get; set; }
		[DataMember] public int 前前軸重 { get; set; }
		[DataMember] public int 前後軸重 { get; set; }
		[DataMember] public int 後前軸重 { get; set; }
		[DataMember] public int 後後軸重 { get; set; }
		[DataMember] public int CO2区分 { get; set; }
		[DataMember] public decimal 基準燃費 { get; set; }
		[DataMember] public decimal 黒煙規制値 { get; set; }
		[DataMember] public int? G車種ID { get; set; }
        [DataMember] public int? 規制区分ID { get; set; }
        [DataMember] public decimal 燃料費単価 { get; set; }
        [DataMember] public decimal 油脂費単価 { get; set; }
        [DataMember] public decimal 修繕費単価 { get; set; }
        [DataMember] public decimal タイヤ費単価 { get; set; }
        [DataMember] public decimal 車検費単価 { get; set; }
        [DataMember] public int? 固定自動車税 { get; set; }
        [DataMember] public decimal? d固定自動車税 { get; set; }
        [DataMember] public int? 固定重量税 { get; set; }
        [DataMember] public decimal? d固定重量税 { get; set; }
        [DataMember] public int? 固定取得税 { get; set; }
        [DataMember] public decimal? d固定取得税 { get; set; }
        [DataMember] public int? 固定自賠責保険 { get; set; }
        [DataMember] public decimal? d固定自賠責保険 { get; set; }
        [DataMember] public int? 固定車輌保険 { get; set; }
        [DataMember] public decimal? d固定車輌保険 { get; set; }
        [DataMember] public int? 固定対人保険 { get; set; }
        [DataMember] public decimal? d固定対人保険 { get; set; }
        [DataMember] public int? 固定対物保険 { get; set; }
        [DataMember] public decimal? d固定対物保険 { get; set; }
        [DataMember] public int? 固定貨物保険 { get; set; }
        [DataMember] public decimal? d固定貨物保険 { get; set; }
        //追加分
        [DataMember] public DateTime? 削除日付 { get; set; }
        [DataMember] public string 乗務員名 { get; set; }
        [DataMember] public string 運輸局名 { get; set; }
        [DataMember] public string 自社部門名 { get; set; }
        [DataMember] public string 車種名 { get; set; }

	}

    [DataContract]
    public class M05_CAR_Member_SCH
    {
        [DataMember]
        public int 車輌ID { get; set; }
        [DataMember]
        public string 車輌番号 { get; set; }
        [DataMember]
        public string 乗務員名 { get; set; }
        [DataMember]
        public string 廃車区分 { get; set; }
    }

    [DataContract]
    public class M05_CDT1_Member {
		public int 車輌ID { get; set; }
		public DateTime 配置年月日 { get; set; }
		public DateTime? 登録日時 { get; set; }
		public DateTime? 更新日時 { get; set; }
		public string 営業所名 { get; set; }
		public string 転出先 { get; set; }
        public DateTime? 削除日付 { get; set; }
	}

    [DataContract]
    public class M05_CDT2_Member {
		[DataMember] public int 車輌ID { get; set; }
		[DataMember] public int 車輌KEY { get; set; }
		[DataMember] public DateTime 加入年月日 { get; set; }
		[DataMember] public DateTime 期限 { get; set; }
        [DataMember] public string Str加入年月日 { get; set; }
        [DataMember] public string Str期限年月日 { get; set; }
		[DataMember] public string 契約先 { get; set; }
		[DataMember] public string 保険証番号 { get; set; }
		[DataMember] public int 月数 { get; set; }
        [DataMember] public string S_月数 { get; set; }
	}

    [DataContract]
    public class M05_CDT3_Member {
		[DataMember] public int 車輌ID { get; set; }
		[DataMember] public int 車輌KEY { get; set; }
		[DataMember] public DateTime 加入年月日 { get; set; }
		[DataMember] public DateTime 期限 { get; set; }
        [DataMember] public string Str加入年月日 { get; set; }
        [DataMember] public string Str期限年月日 { get; set; }
		[DataMember] public string 契約先 { get; set; }
		[DataMember] public string 保険証番号 { get; set; }
		[DataMember] public int 月数 { get; set; }
        [DataMember] public string S_月数 { get; set; }
	}

    [DataContract]
    public class M05_CDT4_Member {
		[DataMember] public int 車輌ID { get; set; }
		[DataMember] public int 車輌KEY { get; set; }
		[DataMember] public int 年度 { get; set; }
		[DataMember] public int? 自動車税年月 { get; set; }
        [DataMember] public int? 自動車税 { get; set; }
        [DataMember] public decimal? d自動車税 { get; set; }
        [DataMember] public int? 重量税年月 { get; set; }
        [DataMember] public int? 重量税 { get; set; }
        [DataMember] public decimal? d重量税 { get; set; }
        [DataMember] public string S_年度 { get; set; }
        [DataMember] public string S_自動車税年月 { get; set; }
        [DataMember] public string S_自動車税 { get; set; }
        [DataMember] public string S_重量税年月 { get; set; }
        [DataMember] public string S_重量税 { get; set; }
	}

    [DataContract]
    public class M05_TEN_Member {
		[DataMember] public int 車輌ID { get; set; }
		[DataMember] public int 年月 { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public int 点検日 { get; set; }
		[DataMember] public string チェック { get; set; }
		[DataMember] public int エアコン区分 { get; set; }
		[DataMember] public string エアコン備考 { get; set; }
		[DataMember] public int 異音区分 { get; set; }
		[DataMember] public string 異音備考 { get; set; }
		[DataMember] public int 排気区分 { get; set; }
		[DataMember] public string 排気備考 { get; set; }
		[DataMember] public int 燃費区分 { get; set; }
		[DataMember] public string 燃費備考 { get; set; }
		[DataMember] public int その他区分 { get; set; }
		[DataMember] public string その他備考 { get; set; }
		[DataMember] public int? 乗務員ID { get; set; }
		[DataMember] public string 乗務員名 { get; set; }
		[DataMember] public string 整備指示 { get; set; }
		[DataMember] public DateTime? 指示日付 { get; set; }
		[DataMember] public string 整備部品 { get; set; }
		[DataMember] public DateTime? 部品日付 { get; set; }
		[DataMember] public string 整備結果 { get; set; }
		[DataMember] public DateTime? 結果日付 { get; set; }
	}

    //M06020 プレビュー出力用のメンバー


    [DataContract]
    public class M05_CAR_ichiran_Pre_Member {

		[DataMember] public int 車輌ID { get; set; }
		[DataMember] public string 車輌番号 { get; set; }
        [DataMember] public string 車輌登録番号 { get; set; }
        [DataMember] public int? 車種ID { get; set; }
        [DataMember] public string 車種名 { get; set; }
        [DataMember] public int? 乗務員KEY { get; set; }
        [DataMember] public string 乗務員名 { get; set; }
		[DataMember] public int? 自社部門ID { get; set; }
        [DataMember] public string 自社部門名 { get; set; }
		[DataMember] public int 運輸局ID { get; set; }
        [DataMember] public string 運輸局名 { get; set; }
		[DataMember] public string 廃車区分表示 { get; set; }        
		[DataMember] public DateTime? 廃車日 { get; set; }
        [DataMember] public DateTime? 登録日 { get; set; }
		[DataMember] public DateTime? 次回車検日 { get; set; }
		[DataMember] public string 自動車種別 { get; set; }
		[DataMember] public string 車体形状 { get; set; }
        [DataMember] public string 型式 { get; set; }
        [DataMember] public int 車輌重量 { get; set; }
        [DataMember] public int 車輌総重量 { get; set; }
        [DataMember] public int 車輌最大積載量 { get; set; }
        [DataMember] public int 車輌実積載量 { get; set; }
		[DataMember] public string 車名 { get; set; }
		[DataMember] public int 総排気量 { get; set; }
		[DataMember] public int 現在メーター { get; set; }
        [DataMember] public int 廃車区分 { get; set; }
        
	}

}
