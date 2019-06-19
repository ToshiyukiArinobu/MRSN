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
    public interface IM90030
    {

    }

    /// <summary>
    /// 取引先マスタメンバー
    /// </summary>
    [DataContract]
    public class CSVDATA01
    {
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
    }

    /// <summary>
    /// 請求内訳名
    /// </summary>
    [DataContract]
    public class CSVDATA02
    {
        [DataMember] public int 得意先ID { get; set; }
        [DataMember] public int 請求内訳ID { get; set; }
        [DataMember] public DateTime? 登録日時 { get; set; }
        [DataMember] public DateTime? 更新日時 { get; set; }
        [DataMember] public string 請求内訳名 { get; set; }
        [DataMember] public string かな読み { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
    }

    /// <summary>
    /// 発着地マスタメンバー
    /// </summary>
    [DataContract]
    public class CSVDATA03
    {
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

    /// <summary>
    /// 乗務員マスタメンバー
    /// </summary>
    [DataContract]
    public class CSVDATA05
    {
        [DataMember] public int 乗務員ID { get; set; }
        [DataMember] public Nullable<System.DateTime> 登録日時 { get; set; }
        [DataMember] public Nullable<System.DateTime> 更新日時 { get; set; }
        [DataMember] public string 乗務員名 { get; set; }
        [DataMember] public int 自傭区分 { get; set; }
        [DataMember] public int 就労区分 { get; set; }
        [DataMember] public string かな読み { get; set; }
        [DataMember] public Nullable<System.DateTime> 生年月日 { get; set; }
        [DataMember] public Nullable<System.DateTime> 入社日 { get; set; }
        [DataMember] public int 自社部門ID { get; set; }
        [DataMember] public decimal 歩合率 { get; set; }
        [DataMember] public Nullable<int> デジタコCD { get; set; }
        [DataMember] public int 性別区分 { get; set; }
        [DataMember] public string 郵便番号 { get; set; }
        [DataMember] public string 住所１ { get; set; }
        [DataMember] public string 住所２ { get; set; }
        [DataMember] public string 電話番号 { get; set; }
        [DataMember] public string 携帯電話 { get; set; }
        [DataMember] public string 業務種類 { get; set; }
        [DataMember] public Nullable<System.DateTime> 選任年月日 { get; set; }
        [DataMember] public Nullable<int> 血液型 { get; set; }
        [DataMember] public string 免許証番号 { get; set; }
        [DataMember] public Nullable<System.DateTime> 免許証取得年月日 { get; set; }
        [DataMember] public Nullable<int> 免許種類1 { get; set; }
        [DataMember] public Nullable<int> 免許種類2 { get; set; }
        [DataMember] public Nullable<int> 免許種類3 { get; set; }
        [DataMember] public Nullable<int> 免許種類4 { get; set; }
        [DataMember] public Nullable<int> 免許種類5 { get; set; }
        [DataMember] public Nullable<int> 免許種類6 { get; set; }
        [DataMember] public Nullable<int> 免許種類7 { get; set; }
        [DataMember] public Nullable<int> 免許種類8 { get; set; }
        [DataMember] public Nullable<int> 免許種類9 { get; set; }
        [DataMember] public Nullable<int> 免許種類10 { get; set; }
        [DataMember] public string 免許証条件 { get; set; }
        [DataMember] public Nullable<int> 職種分類区分 { get; set; }
        [DataMember] public string 職種分類 { get; set; }
        [DataMember] public Nullable<int> 作成番号 { get; set; }
        [DataMember] public Nullable<int> 撮影年 { get; set; }
        [DataMember] public Nullable<int> 撮影月 { get; set; }
        [DataMember] public Nullable<System.DateTime> 免許有効年月日1 { get; set; }
        [DataMember] public string 免許有効番号1 { get; set; }
        [DataMember] public Nullable<System.DateTime> 免許有効年月日2 { get; set; }
        [DataMember] public string 免許有効番号2 { get; set; }
        [DataMember] public Nullable<System.DateTime> 免許有効年月日3 { get; set; }
        [DataMember] public string 免許有効番号3 { get; set; }
        [DataMember] public Nullable<System.DateTime> 免許有効年月日4 { get; set; }
        [DataMember] public string 免許有効番号4 { get; set; }
        [DataMember] public Nullable<System.DateTime> 履歴年月日1 { get; set; }
        [DataMember] public string 履歴1 { get; set; }
        [DataMember] public Nullable<System.DateTime> 履歴年月日2 { get; set; }
        [DataMember] public string 履歴2 { get; set; }
        [DataMember] public Nullable<System.DateTime> 履歴年月日3 { get; set; }
        [DataMember] public string 履歴3 { get; set; }
        [DataMember] public Nullable<System.DateTime> 履歴年月日4 { get; set; }
        [DataMember] public string 履歴4 { get; set; }
        [DataMember] public Nullable<System.DateTime> 履歴年月日5 { get; set; }
        [DataMember] public string 履歴5 { get; set; }
        [DataMember] public Nullable<System.DateTime> 履歴年月日6 { get; set; }
        [DataMember] public string 履歴6 { get; set; }
        [DataMember] public Nullable<System.DateTime> 履歴年月日7 { get; set; }
        [DataMember] public string 履歴7 { get; set; }
        [DataMember] public Nullable<int> 経験種類1 { get; set; }
        [DataMember] public Nullable<int> 経験定員1 { get; set; }
        [DataMember] public Nullable<decimal> 経験積載量1 { get; set; }
        [DataMember] public Nullable<int> 経験年1 { get; set; }
        [DataMember] public Nullable<int> 経験月1 { get; set; }
        [DataMember] public string 経験事業所1 { get; set; }
        [DataMember] public Nullable<int> 経験種類2 { get; set; }
        [DataMember] public Nullable<int> 経験定員2 { get; set; }
        [DataMember] public Nullable<decimal> 経験積載量2 { get; set; }
        [DataMember] public Nullable<int> 経験年2 { get; set; }
        [DataMember] public Nullable<int> 経験月2 { get; set; }
        [DataMember] public string 経験事業所2 { get; set; }
        [DataMember] public Nullable<int> 経験種類3 { get; set; }
        [DataMember] public Nullable<int> 経験定員3 { get; set; }
        [DataMember] public Nullable<decimal> 経験積載量3 { get; set; }
        [DataMember] public Nullable<int> 経験年3 { get; set; }
        [DataMember] public Nullable<int> 経験月3 { get; set; }
        [DataMember] public string 経験事業所3 { get; set; }
        [DataMember] public Nullable<System.DateTime> 資格賞罰年月日1 { get; set; }
        [DataMember] public string 資格賞罰名1 { get; set; }
        [DataMember] public string 資格賞罰内容1 { get; set; }
        [DataMember] public Nullable<System.DateTime> 資格賞罰年月日2 { get; set; }
        [DataMember] public string 資格賞罰名2 { get; set; }
        [DataMember] public string 資格賞罰内容2 { get; set; }
        [DataMember] public Nullable<System.DateTime> 資格賞罰年月日3 { get; set; }
        [DataMember] public string 資格賞罰名3 { get; set; }
        [DataMember] public string 資格賞罰内容3 { get; set; }
        [DataMember] public Nullable<System.DateTime> 資格賞罰年月日4 { get; set; }
        [DataMember] public string 資格賞罰名4 { get; set; }
        [DataMember] public string 資格賞罰内容4 { get; set; }
        [DataMember] public Nullable<System.DateTime> 資格賞罰年月日5 { get; set; }
        [DataMember] public string 資格賞罰名5 { get; set; }
        [DataMember] public string 資格賞罰内容5 { get; set; }
        [DataMember] public Nullable<int> 事業者コード { get; set; }
        [DataMember] public Nullable<System.DateTime> 健康保険加入日 { get; set; }
        [DataMember] public string 健康保険番号 { get; set; }
        [DataMember] public Nullable<System.DateTime> 厚生年金加入日 { get; set; }
        [DataMember] public string 厚生年金番号 { get; set; }
        [DataMember] public Nullable<System.DateTime> 雇用保険加入日 { get; set; }
        [DataMember] public string 雇用保険番号 { get; set; }
        [DataMember] public Nullable<System.DateTime> 労災保険加入日 { get; set; }
        [DataMember] public string 労災保険番号 { get; set; }
        [DataMember] public Nullable<System.DateTime> 厚生年金基金加入日 { get; set; }
        [DataMember] public string 厚生年金基金番号 { get; set; }
        [DataMember] public Nullable<int> 通勤時間 { get; set; }
        [DataMember] public Nullable<int> 通勤分 { get; set; }
        [DataMember] public string 家族連絡 { get; set; }
        [DataMember] public Nullable<int> 住居の種類 { get; set; }
        [DataMember] public string 通勤方法 { get; set; }
        [DataMember] public string 家族氏名1 { get; set; }
        [DataMember] public Nullable<System.DateTime> 家族生年月日1 { get; set; }
        [DataMember] public string 家族続柄1 { get; set; }
        [DataMember] public Nullable<int> 家族血液型1 { get; set; }
        [DataMember] public string 家族その他1 { get; set; }
        [DataMember] public string 家族氏名2 { get; set; }
        [DataMember] public Nullable<System.DateTime> 家族生年月日2 { get; set; }
        [DataMember] public string 家族続柄2 { get; set; }
        [DataMember] public Nullable<int> 家族血液型2 { get; set; }
        [DataMember] public string 家族その他2 { get; set; }
        [DataMember] public string 家族氏名3 { get; set; }
        [DataMember] public Nullable<System.DateTime> 家族生年月日3 { get; set; }
        [DataMember] public string 家族続柄3 { get; set; }
        [DataMember] public Nullable<int> 家族血液型3 { get; set; }
        [DataMember] public string 家族その他3 { get; set; }
        [DataMember] public string 家族氏名4 { get; set; }
        [DataMember] public Nullable<System.DateTime> 家族生年月日4 { get; set; }
        [DataMember] public string 家族続柄4 { get; set; }
        [DataMember] public Nullable<int> 家族血液型4 { get; set; }
        [DataMember] public string 家族その他4 { get; set; }
        [DataMember] public string 家族氏名5 { get; set; }
        [DataMember] public Nullable<System.DateTime> 家族生年月日5 { get; set; }
        [DataMember] public string 家族続柄5 { get; set; }
        [DataMember] public Nullable<int> 家族血液型5 { get; set; }
        [DataMember] public string 家族その他5 { get; set; }
        [DataMember] public Nullable<System.DateTime> 退職年月日 { get; set; }
        [DataMember] public string 退職理由 { get; set; }
        [DataMember] public string 特記事項1 { get; set; }
        [DataMember] public string 特記事項2 { get; set; }
        [DataMember] public string 特記事項3 { get; set; }
        [DataMember] public string 特記事項4 { get; set; }
        [DataMember] public string 特記事項5 { get; set; }
        [DataMember] public Nullable<System.DateTime> 健康診断年月日1 { get; set; }
        [DataMember] public Nullable<System.DateTime> 健康診断年月日2 { get; set; }
        [DataMember] public Nullable<System.DateTime> 健康診断年月日3 { get; set; }
        [DataMember] public Nullable<System.DateTime> 健康診断年月日4 { get; set; }
        [DataMember] public Nullable<System.DateTime> 健康診断年月日5 { get; set; }
        [DataMember] public string 健康状態 { get; set; }
        [DataMember] public int 水揚連動区分 { get; set; }
        [DataMember] public Nullable<int> 自社ID { get; set; }
        [DataMember] public string 個人ナンバー { get; set; }
        [DataMember] public Nullable<System.DateTime> 削除日付 { get; set; }
        [DataMember] public int 乗務員KEY { get; set; }
        [DataMember] public Nullable<int> 固定給与 { get; set; }
        [DataMember] public Nullable<int> 固定賞与積立金 { get; set; }
        [DataMember] public Nullable<int> 固定福利厚生費 { get; set; }
        [DataMember] public Nullable<int> 固定法定福利費 { get; set; }
        [DataMember] public Nullable<int> 固定退職引当金 { get; set; }
        [DataMember] public Nullable<int> 固定労働保険 { get; set; }
    }

    /// <summary>
    /// 車輌マスタメンバー
    /// </summary>
    [DataContract]
    public class CSVDATA04
    {
        [DataMember] public int 車輌ID { get; set; }
        [DataMember] public int 車輌KEY { get; set; }
        [DataMember] public DateTime? 登録日時 { get; set; }
        [DataMember] public DateTime? 更新日時 { get; set; }
        [DataMember] public string 車輌番号 { get; set; }
        [DataMember] public int? 自社部門ID { get; set; }
        [DataMember] public int 車種ID { get; set; }
        [DataMember] public int 乗務員ID { get; set; }
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
        [DataMember] public int? 固定重量税 { get; set; }
        [DataMember] public int? 固定取得税 { get; set; }
        [DataMember] public int? 固定自賠責保険 { get; set; }
        [DataMember] public int? 固定車輌保険 { get; set; }
        [DataMember] public int? 固定対人保険 { get; set; }
        [DataMember] public int? 固定対物保険 { get; set; }
        [DataMember] public int? 固定貨物保険 { get; set; }
        //追加分
        [DataMember] public DateTime? 削除日付 { get; set; }
        [DataMember] public string 乗務員名 { get; set; }
        [DataMember] public string 運輸局名 { get; set; }
        [DataMember] public string 自社部門名 { get; set; }
        [DataMember] public string 車種名 { get; set; }
    }

    /// <summary>
    /// 車種マスタメンバー
    /// </summary>
    [DataContract]
    public class CSVDATA06
    {
        [DataMember] public int 車種ID { get; set; }
        [DataMember] public DateTime? 登録日時 { get; set; }
        [DataMember] public DateTime? 更新日時 { get; set; }
        [DataMember] public string 車種名 { get; set; }
        [DataMember] public decimal? 積載重量 { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
    }

    /// <summary>
    /// 商品マスタメンバー
    /// </summary>
    [DataContract]
    public class CSVDATA07
    {
        [DataMember] public int 商品ID { get; set; }
        [DataMember] public DateTime? 登録日時 { get; set; }
        [DataMember] public DateTime? 更新日時 { get; set; }
        [DataMember] public string 商品名 { get; set; }
        [DataMember] public string かな読み { get; set; }
        [DataMember] public string 単位 { get; set; }
        [DataMember] public decimal? 商品重量 { get; set; }
        [DataMember] public decimal? 商品才数 { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
    }

    /// <summary>
    /// 摘要マスタメンバー
    /// </summary>
    [DataContract]
    public class CSVDATA08
    {
        [DataMember] public int 摘要ID { get; set; }
        [DataMember] public DateTime? 登録日時 { get; set; }
        [DataMember] public DateTime? 更新日時 { get; set; }
        [DataMember] public string 摘要名 { get; set; }
        [DataMember] public string かな読み { get; set; }
        [DataMember] public DateTime? 削除日付 { get; set; }
    }

}
