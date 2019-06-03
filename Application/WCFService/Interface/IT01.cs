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
	public interface IT01
	{

    }

    // データメンバー定義
    /// <summary>
    /// T01_TRN_Member
    /// </summary>
    [DataContract]
	public class T01_TRN_Member
	{
        [DataMember] public int 明細番号 { get; set; }
        [DataMember] public int 明細行 { get; set; }
        [DataMember] public DateTime? 登録日時 { get; set; }
        [DataMember] public DateTime? 更新日時 { get; set; }
        [DataMember] public int 明細区分 { get; set; }
        [DataMember] public int 入力区分 { get; set; }
        [DataMember] public DateTime? 請求日付 { get; set; }
        [DataMember] public DateTime? 支払日付 { get; set; }
        [DataMember] public DateTime? 配送日付 { get; set; }
        [DataMember] public decimal? 配送時間 { get; set; }
        [DataMember] public int? 得意先ID { get; set; }
        [DataMember] public int? 請求内訳ID { get; set; }
        [DataMember] public int? 車輌ID { get; set; }
		[DataMember] public int? 車種ID { get; set; }
        [DataMember] public int? 支払先ID { get; set; }
        [DataMember] public int? 乗務員ID { get; set; }
        [DataMember] public int 自社部門ID { get; set; }
        [DataMember] public string 車輌番号 { get; set; }
        [DataMember] public string 支払先名２次 { get; set; }
        [DataMember] public string 実運送乗務員 { get; set; }
        [DataMember] public string 乗務員連絡先 { get; set; }
        [DataMember] public int? 請求運賃計算区分ID { get; set; }
        [DataMember] public int? 支払運賃計算区分ID { get; set; }
        [DataMember] public decimal? 数量 { get; set; }
        [DataMember] public string 単位 { get; set; }
        [DataMember] public decimal? 重量 { get; set; }
        [DataMember] public int? 走行ＫＭ { get; set; }
        [DataMember] public int? 実車ＫＭ { get; set; }
        [DataMember] public decimal? 待機時間 { get; set; }
        [DataMember] public decimal? 売上単価 { get; set; }
        [DataMember] public int? 売上金額 { get; set; }
        [DataMember] public int? 通行料 { get; set; }
        [DataMember] public int? 請求割増１ { get; set; }
        [DataMember] public int? 請求割増２ { get; set; }
        [DataMember] public int? 請求消費税 { get; set; }
		[DataMember] public decimal? 支払単価 { get; set; }
        [DataMember] public int? 支払金額 { get; set; }
        [DataMember] public int? 支払通行料 { get; set; }
        [DataMember] public int? 支払割増１ { get; set; }
        [DataMember] public int? 支払割増２ { get; set; }
        [DataMember] public int? 支払消費税 { get; set; }
        [DataMember] public int? 水揚金額 { get; set; }
        [DataMember] public int? 社内区分 { get; set; }
        [DataMember] public int? 請求税区分 { get; set; }
        [DataMember] public int? 支払税区分 { get; set; }
        [DataMember] public int? 売上未定区分 { get; set; }
        [DataMember] public int? 支払未定区分 { get; set; }
        [DataMember] public int? 商品ID { get; set; }
        [DataMember] public string 商品名 { get; set; }
        [DataMember] public int? 発地ID { get; set; }
        [DataMember] public string 発地名 { get; set; }
        [DataMember] public int? 着地ID { get; set; }
        [DataMember] public string 着地名 { get; set; }
        [DataMember] public int? 請求摘要ID { get; set; }
        [DataMember] public string 請求摘要 { get; set; }
        [DataMember] public int? 社内備考ID { get; set; }
        [DataMember] public string 社内備考 { get; set; }
        [DataMember] public int? 入力者ID { get; set; }
        [DataMember] public int 確認名称区分 { get; set; }

        [DataMember] public string 得意先名 { get; set; }
		[DataMember] public string 支払先名 { get; set; }

		[DataMember] public string 乗務員名 { get; set; }
        [DataMember] public string 経費名称 { get; set; }
    }

    /// <summary>
    /// T01_TSB_Member
    /// </summary>
    [DataContract]
	public class T01_TSB_Member
	{
		[DataMember] public int 明細番号 { get; set; }
		[DataMember] public int 明細行 { get; set; }
		[DataMember] public DateTime? 登録日時 { get; set; }
		[DataMember] public DateTime? 更新日時 { get; set; }
		[DataMember] public int? 自社ID { get; set; }
		[DataMember] public int? 担当者ID { get; set; }
		[DataMember] public string 自社担当者 { get; set; }
		[DataMember] public string 車種名 { get; set; }
		[DataMember] public decimal? 積地時間 { get; set; }
		[DataMember] public string 積地住所１ { get; set; }
		[DataMember] public string 積地住所２ { get; set; }
		[DataMember] public string 積地電話番号 { get; set; }
		[DataMember] public string 積地ＦＡＸ番号 { get; set; }
		[DataMember] public string 積地メモ { get; set; }
		[DataMember] public decimal? 降地時間 { get; set; }
		[DataMember] public string 降地住所１ { get; set; }
		[DataMember] public string 降地住所２ { get; set; }
		[DataMember] public string 降地電話番号 { get; set; }
		[DataMember] public string 降地ＦＡＸ番号 { get; set; }
		[DataMember] public string 降地メモ { get; set; }
		[DataMember] public int 連絡区分１ { get; set; }
		[DataMember] public int 連絡区分２ { get; set; }
	}

    /// <summary>
    /// 売上明細問い合わせ用データメンバー
    /// </summary>
    [DataContract]
    public class DLY10010_Member
    {
        [DataMember] public int 明細番号 { get; set; }
        [DataMember] public int 明細行 { get; set; }
        [DataMember] public DateTime 請求日付 { get; set; }
        [DataMember] public DateTime? 支払日付 { get; set; }
        [DataMember] public DateTime 配送日付 { get; set; }
        [DataMember] public decimal? 配送時間 { get; set; }
        [DataMember] public int? 得意先ID { get; set; }
        [DataMember] public string 得意先名 { get; set; }
        [DataMember] public int? 請求内訳ID { get; set; }
        [DataMember] public string 請求内訳名 { get; set; }
        [DataMember] public int? 車輌ID { get; set; }
        [DataMember] public string 車輌番号 { get; set; }
        [DataMember] public int? 車種ID { get; set; }
        [DataMember] public string 車種名 { get; set; }
        [DataMember] public int? 支払先ID { get; set; }
        [DataMember] public string 支払先略称名 { get; set; }
        [DataMember] public int? 乗務員ID { get; set; }
        [DataMember] public string 乗務員名 { get; set; }
        [DataMember] public string 支払先名２次 { get; set; }
        [DataMember] public string 実運送乗務員 { get; set; }
        [DataMember] public string 乗務員連絡先 { get; set; }
        [DataMember] public decimal 数量 { get; set; }
        [DataMember] public string 単位 { get; set; }
        [DataMember] public decimal 重量 { get; set; }
		[DataMember] public decimal 才数 { get; set; }
		[DataMember] public int 走行ＫＭ { get; set; }
        [DataMember] public int 実車ＫＭ { get; set; }
        [DataMember] public decimal 売上単価 { get; set; }
        [DataMember] public int 売上金額 { get; set; }
        [DataMember] public decimal d売上金額 { get; set; }
        [DataMember] public int 通行料 { get; set; }
        [DataMember] public decimal d通行料 { get; set; }
        [DataMember] public int 請求割増１ { get; set; }
        [DataMember] public decimal d請求割増１ { get; set; }
        [DataMember] public int 請求割増２ { get; set; }
        [DataMember] public decimal d請求割増２ { get; set; }
        [DataMember] public int 請求消費税 { get; set; }
        [DataMember] public int 売上金額計 { get; set; }
        [DataMember] public decimal d売上金額計 { get; set; }
		[DataMember] public decimal 支払単価 { get; set; }
		[DataMember] public int 支払金額 { get; set; }
		[DataMember] public decimal d支払金額 { get; set; }
		[DataMember] public int 支払通行料 { get; set; }
		[DataMember] public decimal d支払通行料 { get; set; }
		[DataMember] public int 支払割増１ { get; set; }
        [DataMember] public int 支払割増２ { get; set; }
		[DataMember] public int 支払消費税 { get; set; }
		[DataMember] public int 支払金額計 { get; set; }
		[DataMember] public int 差益 { get; set; }
		[DataMember] public int 社内区分 { get; set; }
        [DataMember] public int 請求税区分 { get; set; }
        [DataMember] public int 支払税区分 { get; set; }
        [DataMember] public int 売上未定区分 { get; set; }
        [DataMember] public int 確認名称区分 { get; set; }
        [DataMember] public int 支払未定区分 { get; set; }
        [DataMember] public string 商品名 { get; set; }
        [DataMember] public string 発地名 { get; set; }
        [DataMember] public string 着地名 { get; set; }
        [DataMember] public string 請求摘要 { get; set; }
        [DataMember] public string 社内備考 { get; set; }
		[DataMember] public int? S締日 { get; set; }
		[DataMember] public int? T締日 { get; set; }
		[DataMember] public string 入力区分 { get; set; }
		[DataMember] public string 未定区分名 { get; set; }
        [DataMember] public string 確認名称 { get; set; }
		[DataMember] public string 社内区分名 { get; set; }
		[DataMember] public string 請求税区分名 { get; set; }
		[DataMember] public string 請求年月日 { get; set; }
		[DataMember] public string 支払年月日 { get; set; }
		[DataMember] public string 配送年月日 { get; set; }
		[DataMember] public int 発地ID { get; set; }
		[DataMember] public int 着地ID { get; set; }
		[DataMember] public int 商品ID { get; set; }
        [DataMember] public int 自社部門 { get; set; }
        [DataMember] public int 請求運賃計算区分ID { get; set; }
        [DataMember] public int 支払運賃計算区分ID { get; set; }
    }

    /// <summary>
    /// 支払明細問い合わせ用データメンバー
    /// </summary>
    [DataContract]
    public class DLY11010_Member
    {
        [DataMember] public int 明細番号 { get; set; }
        [DataMember] public int 明細行 { get; set; }
        [DataMember] public DateTime 請求日付 { get; set; }
        [DataMember] public DateTime? 支払日付 { get; set; }
        [DataMember] public DateTime 配送日付 { get; set; }
        [DataMember] public decimal? 配送時間 { get; set; }
		[DataMember] public string 未定区分名 { get; set; }
		[DataMember] public int? 得意先ID { get; set; }
        [DataMember] public string 得意先名 { get; set; }
        [DataMember] public int? 請求内訳ID { get; set; }
        [DataMember] public string 請求内訳名 { get; set; }
        [DataMember] public int? 車輌ID { get; set; }
        [DataMember] public string 車輌番号 { get; set; }
        [DataMember] public int? 車種ID { get; set; }
        [DataMember] public string 車種名 { get; set; }
        [DataMember] public int? 支払先ID { get; set; }
        [DataMember] public string 支払先略称名 { get; set; }
        [DataMember] public int? 乗務員ID { get; set; }
        [DataMember] public string 乗務員名 { get; set; }
        [DataMember] public string 支払先名２次 { get; set; }
        [DataMember] public string 実運送乗務員 { get; set; }
        [DataMember] public string 乗務員連絡先 { get; set; }
        [DataMember] public decimal 数量 { get; set; }
        [DataMember] public string 単位 { get; set; }
        [DataMember] public decimal 重量 { get; set; }
        [DataMember] public int 走行ＫＭ { get; set; }
        [DataMember] public int 実車ＫＭ { get; set; }
        [DataMember] public decimal 売上単価 { get; set; }
        [DataMember] public int 売上金額 { get; set; }
		[DataMember] public decimal d売上金額 { get; set; }
        [DataMember] public int 通行料 { get; set; }
		[DataMember] public decimal d通行料 { get; set; }
        [DataMember] public int 請求割増１ { get; set; }
        [DataMember] public int 請求割増２ { get; set; }
        [DataMember] public int 請求消費税 { get; set; }
        [DataMember] public decimal 支払単価 { get; set; }
        [DataMember] public int 支払金額 { get; set; }
		[DataMember] public decimal d支払金額 { get; set; }
        [DataMember] public int 支払通行料 { get; set; }
		[DataMember] public decimal d支払通行料 { get; set; }
        [DataMember] public int 支払割増１ { get; set; }
        [DataMember] public int 支払割増２ { get; set; }
        [DataMember] public int 支払消費税 { get; set; }
        [DataMember] public int 社内区分 { get; set; }
        [DataMember] public int 請求税区分 { get; set; }
        [DataMember] public int 支払税区分 { get; set; }
        [DataMember] public int 売上未定区分 { get; set; }
        [DataMember] public int 支払未定区分 { get; set; }
        [DataMember] public string 商品名 { get; set; }
        [DataMember] public string 発地名 { get; set; }
        [DataMember] public string 着地名 { get; set; }
        [DataMember] public string 請求摘要 { get; set; }
        [DataMember] public string 社内備考 { get; set; }
        [DataMember] public int? S締日 { get; set; }
        [DataMember] public int? T締日 { get; set; }
        [DataMember] public decimal? 才数 { get; set; }
        [DataMember] public string 入力区分 { get; set; }
        [DataMember] public int 明細区分 { get; set; }
        [DataMember] public int 発地ID { get; set; }
        [DataMember] public int 着地ID { get; set; }
        [DataMember] public int 商品ID { get; set; }
        [DataMember] public int 走行KM { get; set; }
        [DataMember] public int 自社部門 { get; set; }
        [DataMember] public int 請求運賃計算区分ID { get; set; }
        [DataMember] public int 支払運賃計算区分ID { get; set; }
        [DataMember]
        public string 請求税区分名 { get; set; }
    }

    /// <summary>
    /// DLY11010 印刷
    /// </summary>
    [DataContract]
    public class DLY11010_Member_Pri
    {
        [DataMember] public int 明細番号 { get; set; }
        [DataMember] public int 明細行 { get; set; }
        [DataMember] public DateTime? 検索日付From { get; set; }
        [DataMember] public DateTime? 検索日付To { get; set; }
        [DataMember] public string 支払先指定 { get; set; }
        [DataMember] public string 請求先指定 { get; set; }
        [DataMember] public string 部門指定 { get; set; }
		[DataMember] public string 未定区分 { get; set; }
		[DataMember] public string 未定区分名 { get; set; }
		[DataMember] public string 文字列検索 { get; set; }
        [DataMember] public string 表示順序 { get; set; }
        [DataMember] public DateTime? 請求日付 { get; set; }
        [DataMember] public DateTime? 支払日付 { get; set; }
        [DataMember] public DateTime? 配送日付 { get; set; }
        [DataMember] public string 支払先名 { get; set; }
        [DataMember] public int? 得意先ID { get; set; }
        [DataMember] public string 得意先名 { get; set; }
        [DataMember] public string 支払先2次 { get; set; }
        [DataMember] public string 未区 { get; set; }
        [DataMember] public string 税区 { get; set; }
        [DataMember] public string 発地名 { get; set; }
        [DataMember] public string 着地名 { get; set; }
        [DataMember] public string 実運送乗務員 { get; set; }
        [DataMember] public string 乗務員連絡先 { get; set; }
        [DataMember] public string 商品名 { get; set; }
        [DataMember] public string 車輌番号 { get; set; }
        [DataMember] public decimal 走行KM { get; set; }
        [DataMember] public decimal 数量 { get; set; }
        [DataMember] public decimal 重量 { get; set; }
        [DataMember] public decimal 支払単価 { get; set; }
        [DataMember] public decimal 支払金額 { get; set; }
        [DataMember] public decimal 支払通行料 { get; set; }
        [DataMember] public decimal 請求金額 { get; set; }
        [DataMember] public decimal 請求通行料 { get; set; }
        [DataMember] public string 表示順序1 { get; set; }
        [DataMember] public string 表示順序2 { get; set; }
        [DataMember] public string 表示順序3 { get; set; }
        [DataMember] public string 表示順序4 { get; set; }
        [DataMember] public string 表示順序5 { get; set; }
        [DataMember] public int? 車輌ID { get; set; }
        [DataMember] public int? 乗務員ID { get; set; }
        [DataMember] public int? 請求内訳ID { get; set; }
        [DataMember] public int? 自社部門ID { get; set; }
        [DataMember] public decimal? 配送時間 { get; set; }
        [DataMember] public int? 発地ID { get; set; }
        [DataMember] public int? 着地ID { get; set; }
        [DataMember] public int? 商品ID { get; set; }
        [DataMember] public string 乗務員名 { get; set; }
        [DataMember] public int? 入力者ID { get; set; }
    }

    /// <summary>
    /// 請求書用（画面）データメンバー
    /// </summary>
    [DataContract]
    public class W_TKS01010_01_Member
    {
        [DataMember] public string 端末ID { get; set; }
		[DataMember] public bool 印刷区分 { get; set; }
		[DataMember] public int? 請求書発行区分 { get; set; }
		[DataMember] public int 取引先ID { get; set; }
		[DataMember] public int? 内訳ID { get; set; }
		[DataMember] public string 取引先名 { get; set; }
		[DataMember] public string 内訳名 { get; set; }
		[DataMember] public string 郵便番号 { get; set; }
		[DataMember] public string 住所１ { get; set; }
		[DataMember] public string 住所２ { get; set; }
		[DataMember] public string 電話番号 { get; set; }
		[DataMember] public int? 当月請求合計 { get; set; }
	}

    /// <summary>
    /// 請求書用（帳票）データメンバー
    /// </summary>
    [DataContract]
    public class W_TKS01010_02_Member
    {
        [DataMember] public string 端末ID { get; set; }
        [DataMember] public int 連番 { get; set; }
        [DataMember] public int H_得意先ID { get; set; }
        [DataMember] public string H_取引先名1 { get; set; }
        [DataMember] public string H_取引先名2 { get; set; }
        [DataMember] public int? H_T締日 { get; set; }
        [DataMember] public string H_郵便番号 { get; set; }
        [DataMember] public string H_住所1 { get; set; }
        [DataMember] public string H_住所2 { get; set; }
        [DataMember] public string H_電話番号 { get; set; }
        [DataMember] public string H_FAX { get; set; }
        [DataMember] public string H_控え { get; set; }       //　追加
        [DataMember] public byte[] H_ロゴ画像 { get; set; }   //　追加
        [DataMember] public int? H_自社ID { get; set; }
        [DataMember] public string H_自社名 { get; set; }
        [DataMember] public string H_代表者名 { get; set; }
        [DataMember] public string H_自社郵便番号 { get; set; }
        [DataMember] public string H_自社住所1 { get; set; }
        [DataMember] public string H_自社住所2 { get; set; }
        [DataMember] public string H_自社電話番号 { get; set; }
		[DataMember] public string H_自社FAX { get; set; }
		[DataMember] public int? H_当月請求合計 { get; set; }
        [DataMember] public int? H_当月売上額 { get; set; }
        [DataMember] public int? H_当月通行料 { get; set; }
        [DataMember] public int? H_当月課税金額 { get; set; }
        [DataMember] public int? H_当月非課税金額 { get; set; }
        [DataMember] public int? H_当月消費税 { get; set; }
        [DataMember] public int? H_前月繰越額 { get; set; }
        [DataMember] public int? H_当月入金額 { get; set; }
        [DataMember] public int? H_当月入金調整額 { get; set; }
        [DataMember] public int? H_差引繰越額 { get; set; }
        [DataMember] public string H_振込銀行1 { get; set; }
        [DataMember] public string H_振込銀行2 { get; set; }
        [DataMember] public string H_振込銀行3 { get; set; }
        [DataMember] public DateTime? D_請求日付 { get; set; }
        [DataMember] public decimal? D_配送時間 { get; set; }
        [DataMember] public string D_得意先略称名 { get; set; }
        [DataMember] public string D_請求内訳名 { get; set; }
        [DataMember] public int? D_車輌ID { get; set; }
        [DataMember] public string D_車種名 { get; set; }
        [DataMember] public string D_支払先略称名 { get; set; }
        [DataMember] public string D_乗務員名 { get; set; }
        [DataMember] public string D_車輌番号 { get; set; }
        [DataMember] public string D_支払先名2次 { get; set; }
        [DataMember] public string D_実運送乗務員 { get; set; }
        [DataMember] public string D_乗務員連絡先 { get; set; }
        [DataMember] public decimal? D_数量 { get; set; }
        [DataMember] public string D_単位 { get; set; }
        [DataMember] public decimal? D_重量 { get; set; }
        [DataMember] public int? D_走行KM { get; set; }
        [DataMember] public int? D_実車KM { get; set; }
        [DataMember] public decimal? D_売上単価 { get; set; }
        [DataMember] public int? D_売上金額 { get; set; }
        [DataMember] public int? D_通行料 { get; set; }
        [DataMember] public int? D_請求割増1 { get; set; }
        [DataMember] public int? D_請求割増2 { get; set; }
        [DataMember] public int? D_請求消費税 { get; set; }
        [DataMember] public int? D_売上金額計1 { get; set; }
        [DataMember] public int? D_売上金額計2 { get; set; }
        [DataMember] public int? D_売上金額計3 { get; set; }
        [DataMember] public int? D_社内区分 { get; set; }
        [DataMember] public int? D_請求税区分 { get; set; }
        [DataMember] public int? D_商品ID { get; set; }
        [DataMember] public string D_商品名 { get; set; }
        [DataMember] public int? D_発地ID { get; set; }
        [DataMember] public string D_発地名 { get; set; }
        [DataMember] public int? D_着地ID { get; set; }
        [DataMember] public string D_着地名 { get; set; }
        [DataMember] public int? D_請求摘要ID { get; set; }
        [DataMember] public string D_請求摘要 { get; set; }
        [DataMember] public int? D_社内備考ID { get; set; }
        [DataMember] public string D_社内備考 { get; set; }
		[DataMember] public int 請求税区分 { get; set; }
		[DataMember] public int Ｔ税区分ID { get; set; }
		[DataMember] public int 請求書区分ID { get; set; }
		[DataMember] public int D_請求内訳管理区分 { get; set; }
		[DataMember] public int? D_請求内訳ID { get; set; }
		[DataMember] public int D_親子区分ID { get; set; }
		[DataMember] public int D_親ID { get; set; }
		[DataMember] public string D_消費税用年月 { get; set; }
		[DataMember] public int? D_明細番号 { get; set; }
		[DataMember] public int? D_明細行 { get; set; }

		public W_TKS01010_02_Member HeaderCopy()
		{
			W_TKS01010_02_Member wk = new W_TKS01010_02_Member()
			{
				端末ID = this.端末ID,
				連番 = this.連番,
				H_得意先ID = this.H_得意先ID,
				H_取引先名1 = this.H_取引先名1,
				H_取引先名2 = this.H_取引先名2,
				H_T締日 = this.H_T締日,
				H_郵便番号 = this.H_郵便番号,
				H_住所1 = this.H_住所1,
				H_住所2 = this.H_住所2,
				H_電話番号 = this.H_電話番号,
				H_FAX = this.H_FAX,
                H_控え = this.H_控え,           //　追加
                H_ロゴ画像 = this.H_ロゴ画像,   //　追加
				H_自社ID = this.H_自社ID,
				H_自社名 = this.H_自社名,
				H_代表者名 = this.H_代表者名,
				H_自社郵便番号 = this.H_自社郵便番号,
				H_自社住所1 = this.H_自社住所1,
				H_自社住所2 = this.H_自社住所2,
				H_自社電話番号 = this.H_自社電話番号,
				H_自社FAX = this.H_自社FAX,
				H_当月請求合計 = this.H_当月請求合計,
				H_当月売上額 = this.H_当月売上額,
				H_当月通行料 = this.H_当月通行料,
				H_当月課税金額 = this.H_当月課税金額,
				H_当月非課税金額 = this.H_当月非課税金額,
				H_当月消費税 = this.H_当月消費税,
				H_前月繰越額 = this.H_前月繰越額,
				H_当月入金額 = this.H_当月入金額,
				H_当月入金調整額 = this.H_当月入金調整額,
				H_差引繰越額 = this.H_差引繰越額,
				H_振込銀行1 = this.H_振込銀行1,
				H_振込銀行2 = this.H_振込銀行2,
				H_振込銀行3 = this.H_振込銀行3,
				D_請求日付 = null,
				D_配送時間 = null,
				D_得意先略称名 = null,
				D_請求内訳名 = null,
				D_車輌ID = null,
				D_車種名 = null,
				D_支払先略称名 = null,
				D_乗務員名 = null,
				D_車輌番号 = null,
				D_支払先名2次 = null,
				D_実運送乗務員 = null,
				D_乗務員連絡先 = null,
				D_数量 = null,
				D_単位 = null,
				D_重量 = null,
				D_走行KM = null,
				D_実車KM = null,
				D_売上単価 = null,
				D_売上金額 = this.D_売上金額,
				D_通行料 = this.D_通行料,
				D_請求割増1 = this.D_請求割増1,
				D_請求割増2 = this.D_請求割増2,
				D_請求消費税 = this.D_請求消費税,
				D_売上金額計1 = this.D_売上金額計1,
				D_売上金額計2 = this.D_売上金額計2,
				D_売上金額計3 = this.D_売上金額計3,
				D_社内区分 = this.D_社内区分,
				D_請求税区分 = this.D_請求税区分,
				D_商品ID = null,
				D_商品名 = null,
				D_発地ID = null,
				D_発地名 = null,
				D_着地ID = null,
				D_着地名 = null,
				D_請求摘要ID = null,
				D_請求摘要 = null,
				D_社内備考ID = null,
				D_社内備考 = null,
				請求税区分 = this.請求税区分,
				Ｔ税区分ID = this.Ｔ税区分ID,
				請求書区分ID = this.請求書区分ID,
				D_請求内訳管理区分 = this.D_請求内訳管理区分,
				D_請求内訳ID = this.D_請求内訳ID,
				D_親子区分ID = this.D_親子区分ID,
				D_親ID = this.D_親ID,
                D_消費税用年月 = this.D_消費税用年月
			};
			return wk;
		}
	}


    /// <summary>
    /// 得意先売上明細書（帳票）データメンバー
    /// </summary>
    [DataContract]
    public class W_TKS03010_02_Member
    {
        [DataMember] public int 明細番号 { get; set; }
		[DataMember] public int 得意先ID { get; set; }
		[DataMember] public string 取引先名1 { get; set; }
		[DataMember] public int? T締日 { get; set; }
		[DataMember] public int? 当月請求合計 { get; set; }
		[DataMember] public int? 当月売上額 { get; set; }
		[DataMember] public int? 当月通行料 { get; set; }
		[DataMember] public int? 当月課税金額 { get; set; }
		[DataMember] public int? 当月非課税金額 { get; set; }
		[DataMember] public int? 当月消費税 { get; set; }
		[DataMember] public DateTime? 請求日付 { get; set; }
		[DataMember] public string 請求内訳名 { get; set; }
		[DataMember] public string 乗務員名 { get; set; }
		[DataMember] public string 車輌番号 { get; set; }
		[DataMember] public decimal? 数量 { get; set; }
		[DataMember] public decimal? 重量 { get; set; }
		[DataMember] public int? 売上金額 { get; set; }
		[DataMember] public int? 通行料 { get; set; }
		[DataMember] public int? 請求割増1 { get; set; }
		[DataMember] public int? 請求割増2 { get; set; }
		[DataMember] public int? 請求消費税 { get; set; }
		[DataMember] public int? 売上金額計1 { get; set; }
		[DataMember] public int? 売上金額計2 { get; set; }
		[DataMember] public int? 売上金額計3 { get; set; }
		[DataMember] public int? 請求税区分 { get; set; }
		[DataMember] public string 商品名 { get; set; }
		[DataMember] public string 発地名 { get; set; }
		[DataMember] public string 着地名 { get; set; }
		[DataMember] public string 社内備考 { get; set; }
		[DataMember] public int Ｔ税区分ID { get; set; }
		[DataMember] public int 請求書区分ID { get; set; }
		[DataMember] public int 請求内訳管理区分 { get; set; }
		[DataMember] public int? 請求内訳ID { get; set; }
		[DataMember] public int 親子区分ID { get; set; }
		[DataMember] public int 親ID { get; set; }
		[DataMember] public string 摘要名 { get; set; }
		[DataMember] public DateTime? 日付From { get; set; }
		[DataMember] public DateTime? 日付To { get; set; }
        [DataMember] public int 未定区分 { get; set; }
        [DataMember] public string 消費税用年月 { get; set; }
        public W_TKS03010_02_Member Clone()
        {
            W_TKS03010_02_Member wk = new W_TKS03010_02_Member()
            {
                得意先ID = this.得意先ID,
                取引先名1 = this.取引先名1,
                T締日 = this.T締日,
                当月請求合計 = this.当月請求合計,
                当月売上額 = this.当月売上額,
                当月通行料 = this.当月通行料,
                当月課税金額 = this.当月課税金額,
                当月非課税金額 = this.当月非課税金額,
                当月消費税 = this.当月消費税,
                請求内訳名 = this.請求内訳名,
                乗務員名 = this.乗務員名,
                車輌番号 = this.車輌番号,
                数量 = this.数量,
                重量 = this.重量,
                売上金額 = this.売上金額,
                通行料 = this.通行料,
                請求割増1 = this.請求割増1,
                請求割増2 = this.請求割増2,
                請求消費税 = this.請求消費税,
                売上金額計1 = this.売上金額計1,
                売上金額計2 = this.売上金額計2,
                売上金額計3 = this.売上金額計3,
                請求税区分 = this.請求税区分,
                商品名 = this.商品名,
                発地名 = this.発地名,
                着地名 = this.着地名,
                社内備考 = this.社内備考,
                Ｔ税区分ID = this.Ｔ税区分ID,
                請求書区分ID = this.請求書区分ID,
                請求内訳管理区分 = this.請求内訳管理区分,
                請求内訳ID = this.請求内訳ID,
                親子区分ID = this.親子区分ID,
                親ID = this.親ID,
                摘要名 = this.摘要名,
                未定区分 = this.未定区分,
                消費税用年月 = this.消費税用年月,
            };
            return wk;
        }
    }

    /// <summary>
    /// 得意先売上明細書（帳票）データメンバー
    /// </summary>
    [DataContract]
    public class W_SHR02010_02_Member
    {
        [DataMember]
        public DateTime? 支払日付 { get; set; }
        [DataMember]
        public int 支払先ID { get; set; }
        [DataMember]
        public string 取引先名 { get; set; }
        [DataMember]
        public int? T締日 { get; set; }
        [DataMember]
        public string 乗務員名 { get; set; }
        [DataMember]
        public string 車輌番号 { get; set; }
        [DataMember]
        public decimal? 数量 { get; set; }
        [DataMember]
        public decimal? 重量 { get; set; }
        [DataMember]
        public int? 売上金額 { get; set; }
        [DataMember]
        public int? 請求通行料 { get; set; }
        [DataMember]
        public int? 支払金額 { get; set; }
        [DataMember]
        public int? 支払通行料 { get; set; }
        [DataMember]
        public int? 支払割増1 { get; set; }
        [DataMember]
        public int? 支払割増2 { get; set; }
        [DataMember]
        public int? 支払消費税 { get; set; }
        [DataMember]
        public decimal 支払単価 { get; set; }
        [DataMember]
        public int? 差益 { get; set; }
        [DataMember]
        public int? 支払金額計1 { get; set; }
        [DataMember]
        public int? 支払金額計2 { get; set; }
        [DataMember]
        public int? 支払金額計3 { get; set; }
        [DataMember]
        public int? 請求税区分 { get; set; }
        [DataMember]
        public int? 支払税区分 { get; set; }
        [DataMember]
        public string 商品名 { get; set; }
        [DataMember]
        public string 発地名 { get; set; }
        [DataMember]
        public string 着地名 { get; set; }
        [DataMember]
        public string 社内備考 { get; set; }
        [DataMember]
        public int Ｓ税区分ID { get; set; }
        [DataMember]
        public int 請求書区分ID { get; set; }
        [DataMember]
        public int 請求内訳管理区分 { get; set; }
        [DataMember]
        public int? 請求内訳ID { get; set; }
        [DataMember]
        public int 親子区分ID { get; set; }
        [DataMember]
        public int 親ID { get; set; }
        [DataMember]
        public string 摘要名 { get; set; }
        [DataMember]
        public DateTime? 日付From { get; set; }
        [DataMember]
        public DateTime? 日付To { get; set; }
        [DataMember]
        public string 得意先名 { get; set; }
        [DataMember]
        public int? 当月請求合計 { get; set; }
        [DataMember]
        public int? 当月売上額 { get; set; }
        [DataMember]
        public int? 当月通行料 { get; set; }
        [DataMember]
        public int? 当月課税金額 { get; set; }
        [DataMember]
        public int? 当月非課税金額 { get; set; }
        [DataMember]
        public int? 当月消費税 { get; set; }
        [DataMember]
        public string 消費税用年月 { get; set; }
        [DataMember]
        public string 自社名 { get; set; }

        public W_SHR02010_02_Member Clone()
        {
            W_SHR02010_02_Member wk = new W_SHR02010_02_Member()
            {
                支払先ID = this.支払先ID,
                取引先名 = this.取引先名,
                T締日 = this.T締日,
                当月請求合計 = this.当月請求合計,
                当月売上額 = this.当月売上額,
                当月通行料 = this.当月通行料,
                当月課税金額 = this.当月課税金額,
                当月非課税金額 = this.当月非課税金額,
                当月消費税 = this.当月消費税,
                乗務員名 = this.乗務員名,
                車輌番号 = this.車輌番号,
                支払日付 = this.支払日付,
                数量 = this.数量,
                重量 = this.重量,
                支払金額 = this.支払金額,
                支払通行料 = this.支払通行料,
                支払割増1 = this.支払割増1,
                支払割増2 = this.支払割増2,
                支払消費税 = this.支払消費税,
                差益 = this.差益,
                支払金額計1 = this.支払金額計1,
                支払金額計2 = this.支払金額計2,
                支払金額計3 = this.支払金額計3,
                請求税区分 = this.請求税区分,
                商品名 = this.商品名,
                発地名 = this.発地名,
                着地名 = this.着地名,
                社内備考 = this.社内備考,
                Ｓ税区分ID = this.Ｓ税区分ID,
                請求書区分ID = this.請求書区分ID,
                請求内訳管理区分 = this.請求内訳管理区分,
                請求内訳ID = this.請求内訳ID,
                親子区分ID = this.親子区分ID,
                親ID = this.親ID,
                摘要名 = this.摘要名,
                得意先名 = this.得意先名,
                支払単価 = this.支払単価,
                消費税用年月 = this.消費税用年月,
                自社名 = this.自社名,
            };
            return wk;
        }
    }

    /// <summary>
    /// 運転日報明細問い合わせ　メンバー定義
    /// </summary>
    [DataContract]
    public class DLY13010_Member
    {
        [DataMember] public DateTime? 出庫日付 { get; set; }
        [DataMember] public DateTime? 帰庫日付 { get; set; }
        [DataMember] public string 出勤区分 { get; set; }
        [DataMember] public decimal? 出社時間 { get; set; }
        [DataMember] public decimal? 退社時間 { get; set; }
        [DataMember] public int? 自社部門ID { get; set; }
        [DataMember] public int? 乗務員ID { get; set; }
        [DataMember] public string 運転者名 { get; set; }
        [DataMember] public int? 車輌ID { get; set; }
        [DataMember] public string 車輌番号 { get; set; }
        [DataMember] public int? 車種ID { get; set; }
        //[DataMember] public string 車種名 { get; set; }
        //[DataMember] public decimal 稼動金額 { get; set; }
        //[DataMember] public decimal 現金通行料 { get; set; }
        //[DataMember] public decimal プレート { get; set; }
        //[DataMember] public decimal フェリー代 { get; set; }
        //[DataMember] public decimal 運行費   { get; set; }
        //[DataMember] public decimal 電話代   { get; set; }
        //[DataMember] public decimal 燃料Ｌ１ { get; set; }
        //[DataMember] public decimal 燃料代１ { get; set; }
        //[DataMember] public decimal 燃料Ｌ２ { get; set; }
        //[DataMember] public decimal 燃料代２ { get; set; }
        //[DataMember] public decimal 燃料Ｌ３ { get; set; }
        //[DataMember] public decimal 燃料代３ { get; set; }
        //[DataMember] public string  助手手当 { get; set; }
        [DataMember] public decimal 拘束時間 { get; set; }
        [DataMember] public decimal 運転時間 { get; set; }
        [DataMember] public decimal 作業時間 { get; set; }
        [DataMember] public decimal 待機時間 { get; set; }
        [DataMember] public decimal 休憩時間 { get; set; }
        [DataMember] public decimal 残業時間 { get; set; }
        [DataMember] public decimal 深夜時間 { get; set; }
        [DataMember] public decimal 走行ｋｍ { get; set; }
        [DataMember] public decimal 実車ｋｍ { get; set; }
        [DataMember] public decimal 輸送屯数 { get; set; }
        [DataMember] public decimal 出庫ｋｍ { get; set; }
        [DataMember] public decimal 帰庫ｋｍ { get; set; }
        [DataMember] public int 明細番号 { get; set; }
        [DataMember] public int 明細行 { get; set; }
        [DataMember] public int 明細区分 { get; set; }
        [DataMember] public int 入力区分 { get; set; }
        [DataMember] public string s入力区分 { get; set; }
        [DataMember] public DateTime? 検索日付From { get; set; }
        [DataMember] public DateTime? 検索日付To { get; set; }
        [DataMember] public int? 車輌指定 { get; set; }
        [DataMember] public string 部門指定 { get; set; }
        [DataMember] public string 表示順序 { get; set; }
        [DataMember] public int 表示区分No { get; set; }
    }


    /// <summary>
    /// 入金明細問い合わせ　メンバー定義
    /// </summary>
    [DataContract]
    public class DLY15010_PREVIEW
    {
        [DataMember] public int? 得意先ID { get; set; }
        [DataMember] public DateTime? 入金日付 { get; set; }
        [DataMember] public string 得意先名 { get; set; }
        [DataMember] public string 入金区分 { get; set; }
        [DataMember] public decimal? 入金金額 { get; set; }
        [DataMember] public decimal? 入金合計 { get; set; }
        [DataMember] public string 摘要名 { get; set; }
        [DataMember] public DateTime? 手形日付 { get; set; }
        [DataMember] public int? 明細番号 { get; set; }
        [DataMember] public int? 明細行 { get; set; }
        [DataMember] public int 明細区分 { get; set; }
        [DataMember] public string 得意先指定 { get; set; }
        [DataMember] public DateTime? 検索日付From { get; set; }
        [DataMember] public DateTime? 検索日付To { get; set; }
        [DataMember] public string 検索日付選択 { get; set; }
        [DataMember] public string 表示順序 { get; set; }
    }

    /// <summary>
    /// 乗務員運行表用データメンバー
    /// </summary>
    [DataContract]
    public class W_DLY16010_Member
    {
        [DataMember] public DateTime H_運行日付 { get; set; }
		[DataMember] public int D_乗務員ID { get; set; }
		[DataMember] public string D_乗務員名 { get; set; }
		[DataMember] public int? D_車種ID { get; set; }
		[DataMember] public string D_車種名 { get; set; }
		[DataMember] public int? D_車輌ID { get; set; }
		[DataMember] public string D_車輌番号 { get; set; }

		[DataMember] public int? D_得意先ID1 { get; set; }
		[DataMember] public string D_得意先名1 { get; set; }
		[DataMember] public string D_商品名1 { get; set; }
		[DataMember] public string D_発地名1 { get; set; }
        [DataMember] public string D_着地名1 { get; set; }
        [DataMember] public decimal? D_数量1 { get; set; }
        [DataMember] public decimal? D_重量1 { get; set; }
        [DataMember] public decimal? D_配送時間1 { get; set; }

        [DataMember] public int? D_得意先ID2 { get; set; }
        [DataMember] public string D_得意先名2 { get; set; }
        [DataMember] public string D_商品名2 { get; set; }
        [DataMember] public string D_発地名2 { get; set; }
        [DataMember] public string D_着地名2 { get; set; }
        [DataMember] public decimal? D_数量2 { get; set; }
        [DataMember] public decimal? D_重量2 { get; set; }
        [DataMember] public decimal? D_配送時間2 { get; set; }

        [DataMember] public int? D_得意先ID3 { get; set; }
        [DataMember] public string D_得意先名3 { get; set; }
        [DataMember] public string D_商品名3 { get; set; }
        [DataMember] public string D_発地名3 { get; set; }
        [DataMember] public string D_着地名3 { get; set; }
        [DataMember] public decimal? D_数量3 { get; set; }
        [DataMember] public decimal? D_重量3 { get; set; }
        [DataMember] public decimal? D_配送時間3 { get; set; }

        [DataMember] public int? D_得意先ID4 { get; set; }
        [DataMember] public string D_得意先名4 { get; set; }
        [DataMember] public string D_商品名4 { get; set; }
        [DataMember] public string D_発地名4 { get; set; }
        [DataMember] public string D_着地名4 { get; set; }
        [DataMember] public decimal? D_数量4 { get; set; }
        [DataMember] public decimal? D_重量4 { get; set; }
        [DataMember] public decimal? D_配送時間4 { get; set; }

        [DataMember] public int? D_得意先ID5 { get; set; }
        [DataMember] public string D_得意先名5 { get; set; }
        [DataMember] public string D_商品名5 { get; set; }
        [DataMember] public string D_発地名5 { get; set; }
        [DataMember] public string D_着地名5 { get; set; }
        [DataMember] public decimal? D_数量5 { get; set; }
        [DataMember] public decimal? D_重量5 { get; set; }
        [DataMember] public decimal? D_配送時間5 { get; set; }
	}


 
}
