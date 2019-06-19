using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Objects;
using System.Data;
using System.Data.Common;
using System.Transactions;

namespace KyoeiSystem.Application.WCFService
{
	public class TKS13010
	{
		public class TKS13010_Member
		{
			//public int 明細番号 { get; set; }
			//public int 明細行 { get; set; }
			public int H_得意先ID { get; set; }
			public string H_取引先名１ { get; set; }
			public string H_取引先名２ { get; set; }
			public int H_Ｔ締日 { get; set; }
			public DateTime? D_請求日付 { get; set; }
			public DateTime? D_支払日付 { get; set; }
			public DateTime? D_配送日付 { get; set; }
			public decimal? D_配送時間 { get; set; }
			public string D_得意先略称名 { get; set; }
			public string D_請求内訳名 { get; set; }
			public int? D_車輌ID { get; set; }
			public string D_車種名 { get; set; }
			public string D_支払先略称名 { get; set; }
			public string D_乗務員名 { get; set; }
			public int? D_自社部門ID { get; set; }
			public string D_車輌番号 { get; set; }
			public string D_支払先名２次 { get; set; }
			public string D_実運送乗務員 { get; set; }
			public string D_乗務員連絡先 { get; set; }
			public decimal? D_数量 { get; set; }
			public string D_単位 { get; set; }
			public decimal? D_重量 { get; set; }
			public decimal? D_走行ＫＭ { get; set; }
			public decimal? D_実車ＫＭ { get; set; }
			public decimal? D_売上単価 { get; set; }
			public decimal? D_売上金額 { get; set; }
			public decimal? D_通行料 { get; set; }
			public decimal? D_請求割増１ { get; set; }
			public decimal? D_請求割増２ { get; set; }
			public decimal? D_請求消費税 { get; set; }
			public decimal? D_売上合計 { get; set; }
			public decimal? D_支払単価 { get; set; }
			public decimal? D_支払金額 { get; set; }
			public decimal? D_支払通行料 { get; set; }
			public decimal? D_支払割増１ { get; set; }
			public decimal? D_支払割増２ { get; set; }
			public decimal? D_支払消費税 { get; set; }
			public decimal? D_水揚金額 { get; set; }
			public int? D_社内区分 { get; set; }
			public int? D_請求税区分 { get; set; }
			public int? D_支払未定区分 { get; set; }
			public string D_商品名 { get; set; }
			public string D_発地名 { get; set; }
			public string D_着地名 { get; set; }
			public string D_請求摘要 { get; set; }
			public string D_社内備考 { get; set; }

		}
		/// <summary>
		/// 売上明細書
		/// </summary>
		/// <returns></returns>
		public List<TKS13010_Member> GetListTKS13010(string pickup得意先, int? p得意先FROM, int? p得意先TO, int? p締日, DateTime? p作成年月, DateTime? p集計期間From, DateTime? p集計期間To)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

                var ret = (from t01 in context.T01_TRN.Where(t01 => (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 == 1))
						   from tok in context.M01_TOK.Where(x => x.得意先KEY == t01.得意先KEY && (x.取引区分 == 0 || x.取引区分 == 1))
						   from m10 in context.M10_UHK.Where(x => x.請求内訳ID == t01.請求内訳ID).DefaultIfEmpty()
						   from m06 in context.M06_SYA.Where(x => x.車種ID == t01.車種ID).DefaultIfEmpty()
						   from shr in context.M01_TOK.Where(x => x.得意先KEY == t01.支払先KEY && (x.取引区分 == 0 || x.取引区分 == 2)).DefaultIfEmpty()
						   from m04 in context.M04_DRV.Where(x => x.乗務員KEY == t01.乗務員KEY).DefaultIfEmpty()
						   //where (p得意先ID == null || tok.得意先ID == p得意先ID)
						   //   && (p請求内訳ID == null || t01.請求内訳ID == p請求内訳ID)
						   //   && (p支払先ID == null || shr.得意先ID == p支払先ID)
						   //&& ((p請求日付From == null && p請求日付To == null) || (t01.請求日付 >= p請求日付From && t01.請求日付 <= p請求日付To))
						   //&& ((p支払日付From == null && p支払日付To == null) || (t01.支払日付 >= p支払日付From && t01.支払日付 <= p支払日付To))
						   //&& ((p配送日付From == null && p配送日付To == null) || (t01.配送日付 >= p配送日付From && t01.配送日付 <= p配送日付To))
						   //&& (p自社部門ID == null || t01.自社部門ID == p自社部門ID)
						   select new TKS13010_Member
						   {
							   D_請求日付 = t01.請求日付,
							   D_支払日付 = t01.支払日付,
							   D_配送日付 = t01.配送日付,
							   D_配送時間 = t01.配送時間,
							   H_得意先ID = tok.得意先ID,
							   H_取引先名１ = tok.得意先名１,
							   H_取引先名２ = tok.得意先名２,
							   D_請求内訳名 = m10.請求内訳名,
							   D_車輌ID = t01.車輌KEY,
							   D_車輌番号 = t01.車輌番号,
							   D_車種名 = m06.車種名,
							   D_支払先略称名 = shr.略称名,
							   D_乗務員名 = m04.乗務員名,
							   D_支払先名２次 = t01.支払先名２次,
							   D_実運送乗務員 = t01.実運送乗務員,
							   D_乗務員連絡先 = t01.乗務員連絡先,
							   D_数量 = t01.数量,
							   D_単位 = t01.単位,
							   D_重量 = t01.重量,
							   D_走行ＫＭ = t01.走行ＫＭ,
							   D_実車ＫＭ = t01.実車ＫＭ,
							   D_売上単価 = t01.売上単価,
							   D_売上金額 = t01.売上金額,
							   D_通行料 = t01.通行料,
							   D_請求割増１ = t01.請求割増１,
							   D_請求割増２ = t01.請求割増２,
							   D_請求消費税 = t01.請求消費税,
							   D_支払単価 = t01.支払単価,
							   D_支払金額 = t01.支払金額,
							   D_支払通行料 = t01.支払通行料,
							   D_支払割増１ = t01.支払割増１,
							   D_支払割増２ = t01.支払割増２,
							   D_支払消費税 = t01.支払消費税,
							   D_社内区分 = t01.社内区分,
							   D_請求税区分 = t01.請求税区分,
							   D_支払未定区分 = t01.支払未定区分,
							   D_商品名 = t01.商品名,
							   D_発地名 = t01.発地名,
							   D_着地名 = t01.着地名,
							   D_請求摘要 = t01.請求摘要,
							   D_社内備考 = t01.社内備考,
						   }).ToList();
				return ret;
			}

		}

	}
}