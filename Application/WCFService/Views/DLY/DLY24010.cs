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
using System.Data.Objects.SqlClient;

namespace KyoeiSystem.Application.WCFService
{
	public class DLY24010
	{
		const string SelectedChar = "a";
		const string UnselectedChar = "";

		public class MasterList_Member
		{
			public string 選択 { get; set; }
			public int コード { get; set; }
			public string 表示名 { get; set; }
			// 取引先のみ
			public int 締日 { get; set; }
		}

		public class DLY24010_Member
		{
			[DataMember]
			public int 明細番号 { get; set; }
			[DataMember]
			public int 明細行 { get; set; }
			[DataMember]
			public DateTime 請求日付 { get; set; }
			[DataMember]
			public DateTime? 支払日付 { get; set; }
			[DataMember]
			public DateTime 配送日付 { get; set; }
			[DataMember]
			public decimal? 配送時間 { get; set; }
			[DataMember]
			public int? 得意先ID { get; set; }
			[DataMember]
			public string 得意先名 { get; set; }
			[DataMember]
			public int? 請求内訳ID { get; set; }
			[DataMember]
			public string 請求内訳名 { get; set; }
			[DataMember]
			public int? 車輌ID { get; set; }
			[DataMember]
			public string 車輌番号 { get; set; }
			[DataMember]
			public int? 車種ID { get; set; }
			[DataMember]
			public string 車種名 { get; set; }
			[DataMember]
			public int? 支払先ID { get; set; }
			[DataMember]
			public string 支払先略称名 { get; set; }
			[DataMember]
			public int? 乗務員ID { get; set; }
			[DataMember]
			public string 乗務員名 { get; set; }
			[DataMember]
			public string 支払先名２次 { get; set; }
			[DataMember]
			public string 実運送乗務員 { get; set; }
			[DataMember]
			public string 乗務員連絡先 { get; set; }
			[DataMember]
			public decimal 数量 { get; set; }
			[DataMember]
			public string 単位 { get; set; }
			[DataMember]
			public decimal 重量 { get; set; }
			[DataMember]
			public decimal 才数 { get; set; }
			[DataMember]
			public int 走行ＫＭ { get; set; }
			[DataMember]
			public int 実車ＫＭ { get; set; }
			[DataMember]
			public decimal 売上単価 { get; set; }
			[DataMember]
			public int 売上金額 { get; set; }
			[DataMember]
			public decimal d売上金額 { get; set; }
			[DataMember]
			public int 通行料 { get; set; }
			[DataMember]
			public decimal d通行料 { get; set; }
			[DataMember]
			public int 請求割増１ { get; set; }
			[DataMember]
			public decimal d請求割増１ { get; set; }
			[DataMember]
			public int 請求割増２ { get; set; }
			[DataMember]
			public decimal d請求割増２ { get; set; }
			[DataMember]
			public int 請求消費税 { get; set; }
			[DataMember]
			public int 売上金額計 { get; set; }
			[DataMember]
			public decimal d売上金額計 { get; set; }
			[DataMember]
			public decimal 支払単価 { get; set; }
			[DataMember]
			public int 支払金額 { get; set; }
			[DataMember]
			public decimal d支払金額 { get; set; }
			[DataMember]
			public int 支払通行料 { get; set; }
			[DataMember]
			public decimal d支払通行料 { get; set; }
			[DataMember]
			public int 支払割増１ { get; set; }
			[DataMember]
			public int 支払割増２ { get; set; }
			[DataMember]
			public int 支払消費税 { get; set; }
			[DataMember]
			public int 支払金額計 { get; set; }
			[DataMember]
			public int 差益 { get; set; }
			[DataMember]
			public int 社内区分 { get; set; }
			[DataMember]
			public int 請求税区分 { get; set; }
			[DataMember]
			public int 支払税区分 { get; set; }
			[DataMember]
			public int 売上未定区分 { get; set; }
			[DataMember]
			public int 確認名称区分 { get; set; }
			[DataMember]
			public int 支払未定区分 { get; set; }
			[DataMember]
			public string 商品名 { get; set; }
			[DataMember]
			public string 発地名 { get; set; }
			[DataMember]
			public string 着地名 { get; set; }
			[DataMember]
			public string 請求摘要 { get; set; }
			[DataMember]
			public string 社内備考 { get; set; }
			[DataMember]
			public int? S締日 { get; set; }
			[DataMember]
			public int? T締日 { get; set; }
			[DataMember]
			public string 入力区分 { get; set; }
			[DataMember]
			public string 未定区分名 { get; set; }
			[DataMember]
			public string 確認名称 { get; set; }
			[DataMember]
			public string 社内区分名 { get; set; }
			[DataMember]
			public string 請求税区分名 { get; set; }
			[DataMember]
			public string 請求年月日 { get; set; }
			[DataMember]
			public string 支払年月日 { get; set; }
			[DataMember]
			public string 配送年月日 { get; set; }
			[DataMember]
			public int 発地ID { get; set; }
			[DataMember]
			public int 着地ID { get; set; }
			[DataMember]
			public int 商品ID { get; set; }
			[DataMember]
			public int 自社部門 { get; set; }
			[DataMember]
			public int 請求運賃計算区分ID { get; set; }
			[DataMember]
			public int 支払運賃計算区分ID { get; set; }
			[DataMember]
			public string 割増名称1 { get; set; }
			[DataMember]
			public string 割増名称2 { get; set; }
		}
		public class DLY24010_DATASET
		{
			public List<DLY24010_Member> DataList = new List<DLY24010_Member>();
		}

		/// <summary>
		/// 売上明細問い合わせリスト取得
		/// </summary>
		/// <returns>DLY24010_Memberのリスト</returns>
		public List<DLY24010_Member> GetDataList(DateTime? p検索日付From, DateTime? p検索日付To, int? p抽出区分, int? 入力者ID)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();
				var mitei = (from cmb in context.M99_COMBOLIST
						   where cmb.分類 == "日次" && cmb.機能 == "運転日報入力" && cmb.カテゴリ == "未定区分"
						   select cmb);
				var zei = (from cmb in context.M99_COMBOLIST
						   where cmb.分類 == "日次" && cmb.機能 == "運転日報入力" && cmb.カテゴリ == "税区分"
						   select cmb);
				var shanai = (from cmb in context.M99_COMBOLIST
							  where cmb.分類 == "日次" && cmb.機能 == "運転日報入力" && cmb.カテゴリ == "社内区分"
							  select cmb);
                var kakunin = (from cmb in context.M99_COMBOLIST
                               where cmb.分類 == "マスタ" && cmb.機能 == "基礎情報設定" && cmb.カテゴリ == "確認名称"
                               select cmb);
				

				var cntl = (from a in context.M87_CNTL where a.管理ID == 1 select a);
				

				var query = (from t01 in context.T01_TRN
							 //join fuku_hai in context.T01_TRN.Where(c => (c.入力区分 == 3 && c.明細行 != 1) && c.支払先KEY > 0 ) on t01.明細番号 equals fuku_hai.明細番号 into fuku_haiGroup
							 //join fuku_hai_yos in context.M01_TOK on fuku_haiGroup.Select(c => c.支払先KEY).FirstOrDefault() equals fuku_hai_yos.得意先KEY into fuku_hai_yosGroup
							 from tok in context.M01_TOK.Where(x => x.得意先KEY == t01.得意先KEY && (x.取引区分 == 0 || x.取引区分 == 1)).DefaultIfEmpty()
							 from m10 in context.M10_UHK.Where(x => x.得意先KEY == t01.得意先KEY && x.請求内訳ID == t01.請求内訳ID).DefaultIfEmpty()
							 from m06 in context.M06_SYA.Where(x => x.車種ID == t01.車種ID).DefaultIfEmpty()
							 from shr in context.M01_TOK.Where(x => x.得意先KEY == t01.支払先KEY && (x.取引区分 == 0 || x.取引区分 == 2)).DefaultIfEmpty()
							 from m04 in context.M04_DRV.Where(x => x.乗務員KEY == t01.乗務員KEY).DefaultIfEmpty()
							 from hin in context.M09_HIN.Where(x => x.商品ID == t01.商品ID).DefaultIfEmpty()
							 from car in context.M05_CAR.Where(x => x.車輌KEY == t01.車輌KEY).DefaultIfEmpty()
							 from z in zei.Where(x => x.コード == t01.請求税区分).DefaultIfEmpty()
							 from m in mitei.Where(x => x.コード == t01.売上未定区分).DefaultIfEmpty()
							 from s in shanai.Where(x => x.コード == t01.社内区分).DefaultIfEmpty()
                             from x in kakunin.Where(x => x.コード == t01.確認名称区分).DefaultIfEmpty()
							 where (t01.請求日付 >= p検索日付From && t01.請求日付 <= p検索日付To) && (p抽出区分 == 0 || (p抽出区分 == 1 && t01.乗務員KEY > 0) || (p抽出区分 == 2 && t01.支払先KEY > 0))
							 && ((入力者ID == null || 入力者ID == 0) || t01.入力者ID == 入力者ID)
							 //&& (p社内区分 == null || p社内区分 == 0 || (p社内区分 == 1 && t01.社内区分 == 0) || (p社内区分 == 2 && t01.社内区分 == 1))
							 //【T01.確認名称区分】0が未受領,1が受領になります。
							 orderby t01.請求日付, t01.明細番号, t01.明細行
                             select new DLY24010_Member
							 {
								 明細番号 = t01.明細番号,
								 明細行 = t01.明細行,
								 請求日付 = t01.請求日付,
								 支払日付 = t01.支払日付,
								 配送日付 = t01.配送日付,
								 配送時間 = t01.配送時間,
								 得意先ID = tok.得意先ID,
								 得意先名 = tok.略称名,
								 請求内訳ID = t01.請求内訳ID,
								 請求内訳名 = m10.請求内訳名,
								 車輌ID = car.車輌ID,
								 車輌番号 = t01.車輌番号,
								 車種ID = t01.車種ID,
								 車種名 = m06.車種名,
								 支払先ID = shr.得意先ID,
								 支払先略称名 = shr.略称名,
								 //支払先略称名 = fuku_haiGroup.Count() == 0 ? shr.略称名 : fuku_haiGroup.Count() == 1 ? fuku_hai_yosGroup.Select(c => c.略称名).FirstOrDefault() : SqlFunctions.StringConvert((double)fuku_haiGroup.Count()).Trim() + "件の傭車先使用",
								 乗務員ID = m04.乗務員ID,
								 乗務員名 = (m04 == null || m04.乗務員ID == null || m04.乗務員ID == 0) ? string.Empty : m04.乗務員名,
								 支払先名２次 = t01.支払先名２次,
								 実運送乗務員 = t01.実運送乗務員,
								 乗務員連絡先 = t01.乗務員連絡先,
								 数量 = t01.数量,
								 単位 = t01.単位,
								 重量 = t01.重量,
								 才数 = (hin.商品才数 == null ? 0.0m : (decimal)hin.商品才数),
								 走行ＫＭ = t01.走行ＫＭ,
								 実車ＫＭ = t01.実車ＫＭ,
								 売上単価 = t01.売上単価,
                                 売上金額 = t01.売上金額,
                                 d売上金額 = t01.売上金額,
                                 通行料 = t01.通行料,
                                 d通行料 = t01.通行料,
                                 請求割増１ = t01.請求割増１,
                                 d請求割増１ = t01.請求割増１,
                                 請求割増２ = t01.請求割増２,
                                 d請求割増２ = t01.請求割増２,
                                 請求消費税 = t01.請求消費税,
                                 売上金額計 = t01.売上金額 + t01.通行料 + t01.請求割増１ + t01.請求割増２,
                                 d売上金額計 = t01.売上金額 + t01.通行料 + t01.請求割増１ + t01.請求割増２,
                                 支払単価 = t01.支払単価,
								 支払金額 = (t01.支払先KEY > 0 ? t01.支払金額 : 0),
								 d支払金額 = (t01.支払先KEY > 0 ? t01.支払金額 : 0),
								 支払通行料 = (t01.支払先KEY > 0 ? t01.支払通行料 : 0),
								 d支払通行料 = (t01.支払先KEY > 0 ? t01.支払通行料 : 0),
								 支払割増１ = (t01.支払先KEY > 0 ? t01.支払割増１ : 0),
								 支払割増２ = (t01.支払先KEY > 0 ? t01.支払割増２ : 0),
								 支払金額計 = (t01.支払先KEY > 0 ? (t01.支払金額 + t01.支払通行料 + t01.支払割増１ + t01.支払割増２) : 0),
								 支払消費税 = t01.支払消費税,
								 差益 = (t01.入力区分 == 3 && t01.明細行 == 1) ? 0 : (t01.売上金額 + t01.通行料 + t01.請求割増１ + t01.請求割増２) - (t01.支払先KEY > 0 ? (t01.支払金額 + t01.支払割増１ + t01.支払割増２ + t01.支払通行料) : 0),
								 社内区分 = t01.社内区分,
								 請求税区分 = t01.請求税区分,
								 支払税区分 = t01.支払税区分,
								 売上未定区分 = t01.売上未定区分,
								 支払未定区分 = t01.支払未定区分,
                                 確認名称区分 = t01.確認名称区分,
								 商品名 = t01.商品名,
								 発地名 = t01.発地名,
								 着地名 = t01.着地名,
								 請求摘要 = t01.請求摘要,
								 社内備考 = t01.社内備考,
								 S締日 = tok.Ｓ締日,
								 T締日 = tok.Ｔ締日,
								 入力区分 = t01.入力区分 == 1 ? "日報" : (t01.入力区分 == 2 ? "売上" : (t01.入力区分 == 3 ? "内訳" : "")),
								 請求税区分名 = z.表示名,
								 社内区分名 = t01.社内区分 == 0 ? "" : "社内",
								 未定区分名 = m.表示名,
                                 確認名称 = x.表示名,
								 商品ID = t01.商品ID == null ? 0 : (int)t01.商品ID,
								 発地ID = t01.発地ID == null ? 0 : (int)t01.発地ID,
								 着地ID = t01.着地ID == null ? 0 : (int)t01.着地ID,
                                 自社部門 = t01.自社部門ID == null ? 0 : t01.自社部門ID,
                                 請求運賃計算区分ID = t01.請求運賃計算区分ID,
                                 支払運賃計算区分ID = t01.支払運賃計算区分ID,
								 割増名称1 =  cntl.Select(c => c.割増料金名１).FirstOrDefault(),
								 割増名称2 =  cntl.Select(c => c.割増料金名２).FirstOrDefault(),
							 }).AsQueryable();

				List<DLY24010_Member> ret;
				//if (p検索日付From != null || p検索日付To != null)
				//{
				//	switch (p検索日付区分)
				//	{
				//	case 0:
				//		// 請求日付
				//		ret = (from q in query
				//			   where (p検索日付From == null || q.請求日付 >= p検索日付From)
				//				  && (p検索日付To == null || q.請求日付 <= p検索日付To)
				//			   select q
				//			   ).ToList();
				//		break;
				//	case 1:
				//		// 支払日付
				//		ret = (from q in query
				//			   where (p検索日付From == null || q.支払日付 >= p検索日付From)
				//				  && (p検索日付To == null || q.支払日付 <= p検索日付To)
				//			   select q
				//			   ).ToList();
				//		break;
				//	case 2:
				//		// 配送日付
				//		ret = (from q in query
				//			   where (p検索日付From == null || q.配送日付 >= p検索日付From)
				//				  && (p検索日付To == null || q.配送日付 <= p検索日付To)
				//			   select q
				//			   ).ToList();
				//		break;
				//	default:
				//		ret = query.ToList();
				//		break;
				//	}
				//}
				//else
				//{
				//	ret = query.ToList();
				//}

				ret = query.ToList();

				foreach (var rec in ret)
				{
					rec.請求年月日 = rec.請求日付.ToString("yyyy/MM/dd");
					rec.支払年月日 = rec.支払日付 == null ? "" : ((DateTime)(rec.支払日付)).ToString("yyyy/MM/dd");
					rec.配送年月日 = rec.配送日付.ToString("yyyy/MM/dd");
				}

				return ret;
			}
		}

	}
}