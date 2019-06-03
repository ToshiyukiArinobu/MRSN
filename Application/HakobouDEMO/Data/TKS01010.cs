using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakobo.Data
{
	public class V_TKS01010
	{
		public string 端末ID { get; set; }
		public int 連番 { get; set; }
		public int H_得意先ID { get; set; }
		public string H_取引先名1 { get; set; }
		public string H_取引先名2 { get; set; }
		public int? H_T締日 { get; set; }
		public string H_郵便番号 { get; set; }
		public string H_住所1 { get; set; }
		public string H_住所2 { get; set; }
		public string H_電話番号 { get; set; }
		public string H_FAX { get; set; }
		public int? H_自社ID { get; set; }
		public string H_自社名 { get; set; }
		public string H_代表者名 { get; set; }
		public string H_自社郵便番号 { get; set; }
		public string H_自社住所1 { get; set; }
		public string H_自社住所2 { get; set; }
		public string H_自社電話番号 { get; set; }
		public string H_自社FAX { get; set; }
		public int? H_当月請求合計 { get; set; }
		public int? H_当月売上額 { get; set; }
		public int? H_当月通行料 { get; set; }
		public int? H_当月課税金額 { get; set; }
		public int? H_当月非課税金額 { get; set; }
		public int? H_当月消費税 { get; set; }
		public int? H_前月繰越額 { get; set; }
		public int? H_当月入金額 { get; set; }
		public int? H_当月入金調整額 { get; set; }
		public int? H_差引繰越額 { get; set; }
		public string H_振込銀行1 { get; set; }
		public string H_振込銀行2 { get; set; }
		public string H_振込銀行3 { get; set; }
		public DateTime? D_請求日付 { get; set; }
		public decimal? D_配送時間 { get; set; }
		public string D_得意先略称名 { get; set; }
		public string D_請求内訳名 { get; set; }
		public int? D_車輌ID { get; set; }
		public string D_車種名 { get; set; }
		public string D_支払先略称名 { get; set; }
		public string D_乗務員名 { get; set; }
		public string D_車輌番号 { get; set; }
		public string D_支払先名2次 { get; set; }
		public string D_実運送乗務員 { get; set; }
		public string D_乗務員連絡先 { get; set; }
		public decimal? D_数量 { get; set; }
		public string D_単位 { get; set; }
		public decimal? D_重量 { get; set; }
		public int? D_走行KM { get; set; }
		public int? D_実車KM { get; set; }
		public decimal? D_売上単価 { get; set; }
		public int? D_売上金額 { get; set; }
		public int? D_通行料 { get; set; }
		public int? D_請求割増1 { get; set; }
		public int? D_請求割増2 { get; set; }
		public int? D_請求消費税 { get; set; }
		public int? D_売上金額計1 { get; set; }
		public int? D_売上金額計2 { get; set; }
		public int? D_売上金額計3 { get; set; }
		public int? D_社内区分 { get; set; }
		public int? D_請求税区分 { get; set; }
		public int? D_商品ID { get; set; }
		public string D_商品名 { get; set; }
		public int? D_発地ID { get; set; }
		public string D_発地名 { get; set; }
		public int? D_着地ID { get; set; }
		public string D_着地名 { get; set; }
		public int? D_請求摘要ID { get; set; }
		public string D_請求摘要 { get; set; }
		public int? D_社内備考ID { get; set; }
		public string D_社内備考 { get; set; }
		public int 請求税区分 { get; set; }
		public int Ｔ税区分ID { get; set; }
		public int 請求書区分ID { get; set; }
		public int 請求書区分 { get; set; }
	}

}
