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
    public interface IM04 {

	}

    // データメンバー定義
    /// <summary>
    /// M04_DRV_Member
    /// </summary>
	[DataContract]
	public class M04_DRV_Member
	{
		public int 乗務員ID { get; set; }
		public Nullable<System.DateTime> 登録日時 { get; set; }
		public Nullable<System.DateTime> 更新日時 { get; set; }
		public string 乗務員名 { get; set; }
		public int 自傭区分 { get; set; }
		public int 就労区分 { get; set; }
		public string かな読み { get; set; }
		public Nullable<System.DateTime> 生年月日 { get; set; }
		public Nullable<System.DateTime> 入社日 { get; set; }
		public int? 自社部門ID { get; set; }
		public decimal 歩合率 { get; set; }
		public Nullable<int> デジタコCD { get; set; }
		public int 性別区分 { get; set; }
		public string 郵便番号 { get; set; }
		public string 住所１ { get; set; }
		public string 住所２ { get; set; }
		public string 電話番号 { get; set; }
		public string 携帯電話 { get; set; }
		public string 業務種類 { get; set; }
		public Nullable<System.DateTime> 選任年月日 { get; set; }
		public Nullable<int> 血液型 { get; set; }
		public string 免許証番号 { get; set; }
		public Nullable<System.DateTime> 免許証取得年月日 { get; set; }
		public Nullable<int> 免許種類1 { get; set; }
		public Nullable<int> 免許種類2 { get; set; }
		public Nullable<int> 免許種類3 { get; set; }
		public Nullable<int> 免許種類4 { get; set; }
		public Nullable<int> 免許種類5 { get; set; }
		public Nullable<int> 免許種類6 { get; set; }
		public Nullable<int> 免許種類7 { get; set; }
		public Nullable<int> 免許種類8 { get; set; }
		public Nullable<int> 免許種類9 { get; set; }
		public Nullable<int> 免許種類10 { get; set; }
		public string 免許証条件 { get; set; }
		public Nullable<int> 職種分類区分 { get; set; }
		public string 職種分類 { get; set; }
		public Nullable<int> 作成番号 { get; set; }
		public Nullable<int> 撮影年 { get; set; }
		public Nullable<int> 撮影月 { get; set; }
		public Nullable<System.DateTime> 免許有効年月日1 { get; set; }
		public string 免許有効番号1 { get; set; }
		public Nullable<System.DateTime> 免許有効年月日2 { get; set; }
		public string 免許有効番号2 { get; set; }
		public Nullable<System.DateTime> 免許有効年月日3 { get; set; }
		public string 免許有効番号3 { get; set; }
		public Nullable<System.DateTime> 免許有効年月日4 { get; set; }
		public string 免許有効番号4 { get; set; }
		public Nullable<System.DateTime> 履歴年月日1 { get; set; }
		public string 履歴1 { get; set; }
		public Nullable<System.DateTime> 履歴年月日2 { get; set; }
		public string 履歴2 { get; set; }
		public Nullable<System.DateTime> 履歴年月日3 { get; set; }
		public string 履歴3 { get; set; }
		public Nullable<System.DateTime> 履歴年月日4 { get; set; }
		public string 履歴4 { get; set; }
		public Nullable<System.DateTime> 履歴年月日5 { get; set; }
		public string 履歴5 { get; set; }
		public Nullable<System.DateTime> 履歴年月日6 { get; set; }
		public string 履歴6 { get; set; }
		public Nullable<System.DateTime> 履歴年月日7 { get; set; }
		public string 履歴7 { get; set; }
		public Nullable<int> 経験種類1 { get; set; }
		public Nullable<int> 経験定員1 { get; set; }
		public Nullable<decimal> 経験積載量1 { get; set; }
		public Nullable<int> 経験年1 { get; set; }
		public Nullable<int> 経験月1 { get; set; }
		public string 経験事業所1 { get; set; }
		public Nullable<int> 経験種類2 { get; set; }
		public Nullable<int> 経験定員2 { get; set; }
		public Nullable<decimal> 経験積載量2 { get; set; }
		public Nullable<int> 経験年2 { get; set; }
		public Nullable<int> 経験月2 { get; set; }
		public string 経験事業所2 { get; set; }
		public Nullable<int> 経験種類3 { get; set; }
		public Nullable<int> 経験定員3 { get; set; }
		public Nullable<decimal> 経験積載量3 { get; set; }
		public Nullable<int> 経験年3 { get; set; }
		public Nullable<int> 経験月3 { get; set; }
		public string 経験事業所3 { get; set; }
		public Nullable<System.DateTime> 資格賞罰年月日1 { get; set; }
		public string 資格賞罰名1 { get; set; }
		public string 資格賞罰内容1 { get; set; }
		public Nullable<System.DateTime> 資格賞罰年月日2 { get; set; }
		public string 資格賞罰名2 { get; set; }
		public string 資格賞罰内容2 { get; set; }
		public Nullable<System.DateTime> 資格賞罰年月日3 { get; set; }
		public string 資格賞罰名3 { get; set; }
		public string 資格賞罰内容3 { get; set; }
		public Nullable<System.DateTime> 資格賞罰年月日4 { get; set; }
		public string 資格賞罰名4 { get; set; }
		public string 資格賞罰内容4 { get; set; }
		public Nullable<System.DateTime> 資格賞罰年月日5 { get; set; }
		public string 資格賞罰名5 { get; set; }
		public string 資格賞罰内容5 { get; set; }
		public Nullable<int> 事業者コード { get; set; }
		public Nullable<System.DateTime> 健康保険加入日 { get; set; }
		public string 健康保険番号 { get; set; }
		public Nullable<System.DateTime> 厚生年金加入日 { get; set; }
		public string 厚生年金番号 { get; set; }
		public Nullable<System.DateTime> 雇用保険加入日 { get; set; }
		public string 雇用保険番号 { get; set; }
		public Nullable<System.DateTime> 労災保険加入日 { get; set; }
		public string 労災保険番号 { get; set; }
		public Nullable<System.DateTime> 厚生年金基金加入日 { get; set; }
		public string 厚生年金基金番号 { get; set; }
		public Nullable<int> 通勤時間 { get; set; }
		public Nullable<int> 通勤分 { get; set; }
		public string 家族連絡 { get; set; }
		public Nullable<int> 住居の種類 { get; set; }
		public string 通勤方法 { get; set; }
		public string 家族氏名1 { get; set; }
		public Nullable<System.DateTime> 家族生年月日1 { get; set; }
		public string 家族続柄1 { get; set; }
		public Nullable<int> 家族血液型1 { get; set; }
		public string 家族その他1 { get; set; }
		public string 家族氏名2 { get; set; }
		public Nullable<System.DateTime> 家族生年月日2 { get; set; }
		public string 家族続柄2 { get; set; }
		public Nullable<int> 家族血液型2 { get; set; }
		public string 家族その他2 { get; set; }
		public string 家族氏名3 { get; set; }
		public Nullable<System.DateTime> 家族生年月日3 { get; set; }
		public string 家族続柄3 { get; set; }
		public Nullable<int> 家族血液型3 { get; set; }
		public string 家族その他3 { get; set; }
		public string 家族氏名4 { get; set; }
		public Nullable<System.DateTime> 家族生年月日4 { get; set; }
		public string 家族続柄4 { get; set; }
		public Nullable<int> 家族血液型4 { get; set; }
		public string 家族その他4 { get; set; }
		public string 家族氏名5 { get; set; }
		public Nullable<System.DateTime> 家族生年月日5 { get; set; }
		public string 家族続柄5 { get; set; }
		public Nullable<int> 家族血液型5 { get; set; }
		public string 家族その他5 { get; set; }
		public Nullable<System.DateTime> 退職年月日 { get; set; }
		public string 退職理由 { get; set; }
		public string 特記事項1 { get; set; }
		public string 特記事項2 { get; set; }
		public string 特記事項3 { get; set; }
		public string 特記事項4 { get; set; }
		public string 特記事項5 { get; set; }
		public Nullable<System.DateTime> 健康診断年月日1 { get; set; }
		public Nullable<System.DateTime> 健康診断年月日2 { get; set; }
		public Nullable<System.DateTime> 健康診断年月日3 { get; set; }
		public Nullable<System.DateTime> 健康診断年月日4 { get; set; }
		public Nullable<System.DateTime> 健康診断年月日5 { get; set; }
		public string 健康状態 { get; set; }
		public int 水揚連動区分 { get; set; }
		public Nullable<int> 自社ID { get; set; }
		public string 個人ナンバー { get; set; }
		public Nullable<System.DateTime> 削除日付 { get; set; }
		public int 乗務員KEY { get; set; }
		public Nullable<int> 固定給与 { get; set; }
		public Nullable<int> 固定賞与積立金 { get; set; }
		public Nullable<int> 固定福利厚生費 { get; set; }
		public Nullable<int> 固定法定福利費 { get; set; }
		public Nullable<int> 固定退職引当金 { get; set; }
		public Nullable<int> 固定労働保険 { get; set; }

	}
    
    /// <summary>
    /// M04_DDT1_Member
    /// </summary>
    [DataContract]
	public class M04_DDT1_Member
	{
		public int 乗務員KEY { get; set; }
		public int 明細行 { get; set; }
		public Nullable<System.DateTime> 登録日時 { get; set; }
		public Nullable<System.DateTime> 更新日時 { get; set; }
		public Nullable<System.DateTime> 実施年月日 { get; set; }
		public string 実施年月日str { get; set; }
		public Nullable<int> 対象種類 { get; set; }
		public string 所見摘要 { get; set; }
		public Nullable<System.DateTime> 削除日付 { get; set; }
	}

	[DataContract]
	public class M04_DDT1_All
	{
		public int 乗務員ID { get; set; }
		public string 乗務員名 { get; set; }
		public Nullable<System.DateTime> 実施年月日 { get; set; }
		public string 実施年月日str { get; set; }
		public Nullable<int> 対象種類 { get; set; }
		public string 所見摘要 { get; set; }
		public int 乗務員KEY { get; set; }
	}

    /// <summary>
    /// M04_DDT2_Member
    /// </summary>
    [DataContract]
	public class M04_DDT2_Member
	{
		public int 乗務員KEY { get; set; }
		public int 明細行 { get; set; }
		public Nullable<System.DateTime> 登録日時 { get; set; }
		public Nullable<System.DateTime> 更新日時 { get; set; }
		public Nullable<System.DateTime> 発生年月日 { get; set; }
		public string 発生年月日str { get; set; }
		public string 概要処置 { get; set; }
		public Nullable<System.DateTime> 削除日付 { get; set; }
	}

	[DataContract]
	public class M04_DDT2_All
	{
		public int 乗務員ID { get; set; }
		public string 乗務員名 { get; set; }
		public Nullable<System.DateTime> 発生年月日 { get; set; }
		public string 発生年月日str { get; set; }
		public string 概要処置 { get; set; }
		public int 乗務員KEY { get; set; }
	}


    /// <summary>
    /// M04_DDT3_Member
    /// </summary>
    [DataContract]
	public class M04_DDT3_Member
	{
		public int 乗務員KEY { get; set; }
		public int 明細行 { get; set; }
		public Nullable<System.DateTime> 登録日時 { get; set; }
		public Nullable<System.DateTime> 更新日時 { get; set; }
		public Nullable<System.DateTime> 実施年月日 { get; set; }
		public string 実施年月日str { get; set; }
		public Nullable<int> 教育種類 { get; set; }
		public string 教育内容 { get; set; }
		public Nullable<System.DateTime> 削除日付 { get; set; }
	}

	[DataContract]
	public class M04_DDT3_All
	{
		public int 乗務員ID { get; set; }
		public string 乗務員名 { get; set; }
		public Nullable<System.DateTime> 実施年月日 { get; set; }
		public string 実施年月日str { get; set; }
		public Nullable<int> 教育種類 { get; set; }
		public string 教育内容 { get; set; }
		public int 乗務員KEY { get; set; }
	}

    /// <summary>
    /// M04_DRD_Member
    /// </summary>
    [DataContract]
	public class M04_DRD_Member : M04_DRD
	{
	}

    /// <summary>
    /// M04_DRSB_Member
    /// </summary>
    [DataContract]
	public class M04_DRSB_Member : M04_DRSB
	{
	}

    /// <summary>
    /// M04_DRVPIC_Member
    /// </summary>
    [DataContract]
	public class M04_DRVPIC_Member : M04_DRVPIC
    {
    }
    
    /// <summary>
    /// SCH04010の一覧表示用
    /// </summary>
    [DataContract]
    public class SCH04010_Member
    {
        [DataMember]
        public int 乗務員ID { get; set; }
        [DataMember]
        public string 乗務員名 { get; set; }
        [DataMember]
        public string かな読み { get; set; }
    }

    /// <summary>
    /// W_JMI11010ストアド用
    /// </summary>
    [DataContract]
    public class W_JMI11010_Member {
        [DataMember] public int H_集計年月 { get; set; }
		[DataMember] public bool D_合計区分 { get; set; }
		[DataMember] public int? D_乗務員ID { get; set; }
		[DataMember] public string D_乗務員名 { get; set; }
		[DataMember] public string D_車輌番号 { get; set; }
		[DataMember] public string D_車種名 { get; set; }
		[DataMember] public int? D_運送収入 { get; set; }
		[DataMember] public int? D_燃料代軽油 { get; set; }
		[DataMember] public int? D_燃料代ガソリン { get; set; }
		[DataMember] public int? D_油脂費 { get; set; }
		[DataMember] public int? D_外注 { get; set; }
        [DataMember] public int? D_タイヤ費 { get; set; }
        [DataMember] public int? D_修理費 { get; set; }
        [DataMember] public int? D_部品代 { get; set; }
        [DataMember] public int? D_現金高速代 { get; set; }
        [DataMember] public int? D_ETC { get; set; }
        [DataMember] public int? D_その他1 { get; set; }
        [DataMember] public int? D_その他2 { get; set; }
        [DataMember] public int? D_諸口1 { get; set; }
        [DataMember] public int? D_諸口2 { get; set; }
        [DataMember] public int? D_諸口3 { get; set; }
        [DataMember] public int? D_給与固定 { get; set; }
        [DataMember] public int? D_給与変動 { get; set; }
        [DataMember] public int? D_賞与積立 { get; set; }
        [DataMember] public int? D_福利厚生費 { get; set; }
        [DataMember] public int? D_法定福利費 { get; set; }
        [DataMember] public int? D_退職引当金 { get; set; }
        [DataMember] public int? D_労働保険 { get; set; }
        [DataMember] public int? D_減価償却費 { get; set; }
        [DataMember] public int? D_自動車税 { get; set; }
        [DataMember] public int? D_重量税 { get; set; }
        [DataMember] public int? D_取得税 { get; set; }
        [DataMember] public int? D_自賠責保険 { get; set; }
        [DataMember] public int? D_車輌保険 { get; set; }
        [DataMember] public int? D_対人保険 { get; set; }
        [DataMember] public int? D_対物保険 { get; set; }
        [DataMember] public int? D_荷物保険 { get; set; }
        [DataMember] public int? D_一般管理費 { get; set; }
        [DataMember] public int? D_稼働日数 { get; set; }
        [DataMember] public int? D_実車km { get; set; }
        [DataMember] public int? D_空車km { get; set; }
        [DataMember] public int? D_走行km { get; set; }
        [DataMember] public int? D_燃料L { get; set; }
	}


    public class M04_DRV_All_Data
    {
        //基礎情報
        public DateTime? 作成年月日 { get; set; }
        public int 作成番号 { get; set; }
        public string ふりがな { get; set; }
        public string 氏名 { get; set; }
        public string 性別 { get; set; }
        public string 血液型 { get; set; }
        public DateTime? 生年月日 { get; set; }
        public int? 年齢 { get; set; }
        public string 現住所 { get; set; }
        public string TEL { get; set; }
        public DateTime? 雇用年月日 { get; set; }
        public DateTime? 選任年月日 { get; set; }
        public string 雇用の状況 { get; set; }
        public byte[] 写真 { get; set; }
        public string 撮影年月 { get; set; }

        //運転免許諸関係
        public string 免許証番号 { get; set; }
        public DateTime? 取得年月日 { get; set; }
        public string 免許証種類1 { get; set; }
        public string 免許証種類2 { get; set; }
        public string 免許証種類3 { get; set; }
        public string 免許証種類4 { get; set; }
        public string 免許証種類5 { get; set; }
        public string 免許証種類6 { get; set; }
        public string 免許証種類7 { get; set; }
        public string 免許証種類8 { get; set; }
        public string 免許証種類9 { get; set; }
        public string 免許証種類10 { get; set; }
        public string 条件 { get; set; }
        public DateTime? 有効期限1 { get; set; }
        public DateTime? 有効期限2 { get; set; }
        public DateTime? 有効期限3 { get; set; }
        public DateTime? 有効期限4 { get; set; }
        public string 有効期限番号1 { get; set; }
        public string 有効期限番号2 { get; set; }
        public string 有効期限番号3 { get; set; }
        public string 有効期限番号4 { get; set; }

        //事故・違反歴
        public DateTime? 発生年月日1 { get; set; }
        public DateTime? 発生年月日2 { get; set; }
        public DateTime? 発生年月日3 { get; set; }
        public DateTime? 発生年月日4 { get; set; }
        public DateTime? 発生年月日5 { get; set; }
        public DateTime? 発生年月日6 { get; set; }
        public string 事故概要1 { get; set; }
        public string 事故概要2 { get; set; }
        public string 事故概要3 { get; set; }
        public string 事故概要4 { get; set; }
        public string 事故概要5 { get; set; }
        public string 事故概要6 { get; set; }

        //適性診断・受診状況
        public DateTime? 適性年月日1 { get; set; }
        public DateTime? 適性年月日2 { get; set; }
        public DateTime? 適性年月日3 { get; set; }
        public string 摘要種類1_1 { get; set; }
        public string 摘要種類1_2 { get; set; }
        public string 摘要種類1_3 { get; set; }
        public string 摘要種類1_4 { get; set; }
        public string 摘要種類1_5 { get; set; }
        public string 摘要種類1_6 { get; set; }
        public string 摘要種類2_1 { get; set; }
        public string 摘要種類2_2 { get; set; }
        public string 摘要種類2_3 { get; set; }
        public string 摘要種類2_4 { get; set; }
        public string 摘要種類2_5 { get; set; }
        public string 摘要種類2_6 { get; set; }
        public string 摘要種類3_1 { get; set; }
        public string 摘要種類3_2 { get; set; }
        public string 摘要種類3_3 { get; set; }
        public string 摘要種類3_4 { get; set; }
        public string 摘要種類3_5 { get; set; }
        public string 摘要種類3_6 { get; set; }
        public string 所見摘要1 { get; set; }
        public string 所見摘要2 { get; set; }
        public string 所見摘要3 { get; set; }

        //特別教育・実施状況
        public DateTime? 特別年月日1 { get; set; }
        public DateTime? 特別年月日2 { get; set; }
        public DateTime? 特別年月日3 { get; set; }
        public string 特別種類1_1 { get; set; }
        public string 特別種類1_2 { get; set; }
        public string 特別種類1_3 { get; set; }
        public string 特別種類2_1 { get; set; }
        public string 特別種類2_2 { get; set; }
        public string 特別種類2_3 { get; set; }
        public string 特別種類3_1 { get; set; }
        public string 特別種類3_2 { get; set; }
        public string 特別種類3_3 { get; set; }
        public string 教育内容1 { get; set; }
        public string 教育内容2 { get; set; }
        public string 教育内容3 { get; set; }

        //健康診断・受診状況
        public DateTime? 健康年月日1 { get; set; }
        public DateTime? 健康年月日2 { get; set; }
        public DateTime? 健康年月日3 { get; set; }
        public DateTime? 健康年月日4 { get; set; }
        public DateTime? 健康年月日5 { get; set; }
        public string 健康状態 { get; set; }

        //***************************************************************************

        //履歴
        public DateTime? 履歴年月日1 { get; set; }
        public DateTime? 履歴年月日2 { get; set; }
        public DateTime? 履歴年月日3 { get; set; }
        public DateTime? 履歴年月日4 { get; set; }
        public DateTime? 履歴年月日5 { get; set; }
        public DateTime? 履歴年月日6 { get; set; }
        public DateTime? 履歴年月日7 { get; set; }
        public string 最終学歴1 { get; set; }
        public string 最終学歴2 { get; set; }
        public string 最終学歴3 { get; set; }
        public string 最終学歴4 { get; set; }
        public string 最終学歴5 { get; set; }
        public string 最終学歴6 { get; set; }
        public string 最終学歴7 { get; set; }

        //運転経験
        public string 自動車種類1_1 { get; set; }
        public string 自動車種類1_2 { get; set; }
        public string 自動車種類1_3 { get; set; }
        public string 自動車種類2_1 { get; set; }
        public string 自動車種類2_2 { get; set; }
        public string 自動車種類2_3 { get; set; }
        public string 自動車種類3_1 { get; set; }
        public string 自動車種類3_2 { get; set; }
        public string 自動車種類3_3 { get; set; }
        public int? 定員1 { get; set; }
        public int? 定員2 { get; set; }
        public int? 定員3 { get; set; }
        public decimal? 積載量1 { get; set; }
        public decimal? 積載量2 { get; set; }
        public decimal? 積載量3 { get; set; }
        public int? 経験年1 { get; set; }
        public int? 経験年2 { get; set; }
        public int? 経験年3 { get; set; }
        public int? 経験月1 { get; set; }
        public int? 経験月2 { get; set; }
        public int? 経験月3 { get; set; }
        public string 名称1 { get; set; }
        public string 名称2 { get; set; }
        public string 名称3 { get; set; }

        //資格・賞罰関係
        public DateTime? 資格年月日1 { get; set; }
        public DateTime? 資格年月日2 { get; set; }
        public DateTime? 資格年月日3 { get; set; }
        public DateTime? 資格年月日4 { get; set; }
        public DateTime? 資格年月日5 { get; set; }
        public string 資格名称1 { get; set; }
        public string 資格名称2 { get; set; }
        public string 資格名称3 { get; set; }
        public string 資格名称4 { get; set; }
        public string 資格名称5 { get; set; }
        public string 資格内容1 { get; set; }
        public string 資格内容2 { get; set; }
        public string 資格内容3 { get; set; }
        public string 資格内容4 { get; set; }
        public string 資格内容5 { get; set; }

        //保険関係
        public string 保険種類1 { get; set; }
        public string 保険種類2 { get; set; }
        public string 保険種類3 { get; set; }
        public string 保険種類4 { get; set; }
        public string 保険種類5 { get; set; }
        public DateTime? 加入年月日1 { get; set; }
        public DateTime? 加入年月日2 { get; set; }
        public DateTime? 加入年月日3 { get; set; }
        public DateTime? 加入年月日4 { get; set; }
        public DateTime? 加入年月日5 { get; set; }
        public string 保険番号1 { get; set; }
        public string 保険番号2 { get; set; }
        public string 保険番号3 { get; set; }
        public string 保険番号4 { get; set; }
        public string 保険番号5 { get; set; }

        //家族状況
        public string 氏名1 { get; set; }
        public string 氏名2 { get; set; }
        public string 氏名3 { get; set; }
        public string 氏名4 { get; set; }
        public string 氏名5 { get; set; }
        public DateTime? 生年月日1 { get; set; }
        public DateTime? 生年月日2 { get; set; }
        public DateTime? 生年月日3 { get; set; }
        public DateTime? 生年月日4 { get; set; }
        public DateTime? 生年月日5 { get; set; }
        public string 続柄1 { get; set; }
        public string 続柄2 { get; set; }
        public string 続柄3 { get; set; }
        public string 続柄4 { get; set; }
        public string 続柄5 { get; set; }
        public string 血液型1 { get; set; }
        public string 血液型2 { get; set; }
        public string 血液型3 { get; set; }
        public string 血液型4 { get; set; }
        public string 血液型5 { get; set; }
        public string その他1 { get; set; }
        public string その他2 { get; set; }
        public string その他3 { get; set; }
        public string その他4 { get; set; }
        public string その他5 { get; set; }

        //住居状況
        public int? 通行時 { get; set; }
        public int? 通行分 { get; set; }
        //public string TEL { get; set; }
        public string 通勤方法 { get; set; }
        public string 住居の種類 { get; set; }

        //退職または解雇理由
        public DateTime? 退職日 { get; set; }
        public string 退職理由 { get; set; }
        public string 特記事項 { get; set; }
        public int 乗務員KEY { get; set; }
        public int 乗務員ID { get; set; }
        public int 就労区分 { get; set; }
        public string 事業所名 { get; set; }
        public string 営業所名 { get; set; }
        public string 家族連絡 { get; set; }
    }

}
