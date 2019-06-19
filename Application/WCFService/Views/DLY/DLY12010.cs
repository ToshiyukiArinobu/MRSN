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
    /// <summary>
    /// 売上明細問い合わせ用データメンバー
    /// </summary>
    [DataContract]
    public class DLY12010_Member
    {
        [DataMember]
        public int 明細番号 { get; set; }
        [DataMember]
        public int 明細行 { get; set; }
        [DataMember]
        public string 入力区分 { get; set; }
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
        public int 売上金額計 { get; set; }
        [DataMember]
        public decimal d売上金額計 { get; set; }
        [DataMember]
        public int 請求消費税 { get; set; }
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
        public decimal d支払割増１ { get; set; }
        [DataMember]
        public int 支払割増２ { get; set; }
        [DataMember]
        public decimal d支払割増２ { get; set; }
		[DataMember]
		public decimal 支払金額計 { get; set; }
		[DataMember]
        public int 支払消費税 { get; set; }
		[DataMember]
		public int 社内区分 { get; set; }
		[DataMember]
		public string 社内区分名 { get; set; }
		[DataMember]
        public int 請求税区分 { get; set; }
        [DataMember]
        public int 支払税区分 { get; set; }
        [DataMember]
        public int 売上未定区分 { get; set; }
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
        public string 表示順序1 { get; set; }
        [DataMember]
        public string 表示順序2 { get; set; }
        [DataMember]
        public string 表示順序3 { get; set; }
        [DataMember]
        public string 表示順序4 { get; set; }
        [DataMember]
        public string 表示順序5 { get; set; }
        [DataMember]
        public decimal 単価 { get; set; }
        [DataMember]
        public int? 発地ID { get; set; }
        [DataMember]
        public int? 着地ID { get; set; }
        [DataMember]
        public int? 商品ID { get; set; }
        [DataMember]
        public int 自社部門 { get; set; }
        [DataMember]
        public int 請求運賃計算区分ID { get; set; }
        [DataMember]
        public int 支払運賃計算区分ID { get; set; }


    }

    public class DLY12010
    {
        const string SelectedChar = "a";
        const string UnselectedChar = "";

        public class MasterList_Member
        {
            public string 選択 { get; set; }
            public int コード { get; set; }
            public string 表示名 { get; set; }
            public int 締日 { get; set; }
        }

        public List<MasterList_Member> GetMasterList(string param)
        {
            List<MasterList_Member> result = new List<MasterList_Member>();

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                string selsts = UnselectedChar;
                context.Connection.Open();
                switch (param)
                {
                    case "得意先":
                        result = (from mst in context.M01_TOK
                                  where mst.削除日付 == null
                                  && (mst.取引区分 == 0 || mst.取引区分 == 1)
                                  orderby mst.得意先ID
                                  select new MasterList_Member
                                  {
                                      選択 = selsts,
                                      コード = mst.得意先ID,
                                      表示名 = mst.得意先名１,
                                      締日 = mst.Ｔ締日,
                                  }
                                  ).ToList();
                        break;
                    case "支払先":
                        result = (from mst in context.M01_TOK
                                  where mst.削除日付 == null
                                  && (mst.取引区分 == 0 || mst.取引区分 == 2)
                                  orderby mst.得意先ID
                                  select new MasterList_Member
                                  {
                                      選択 = selsts,
                                      コード = mst.得意先ID,
                                      表示名 = mst.得意先名１,
                                      締日 = mst.Ｔ締日,
                                  }
                                  ).ToList();
                        break;
                    case "仕入先":
                        result = (from mst in context.M01_TOK
                                  where mst.削除日付 == null
                                  && (mst.取引区分 == 0 || mst.取引区分 == 3)
                                  orderby mst.得意先ID
                                  select new MasterList_Member
                                  {
                                      選択 = selsts,
                                      コード = mst.得意先ID,
                                      表示名 = mst.得意先名１,
                                      締日 = mst.Ｔ締日,
                                  }
                                  ).ToList();
                        break;
                    case "乗務員":
                        result = (from mst in context.M04_DRV
                                  where mst.削除日付 == null
                                  orderby mst.乗務員ID
                                  select new MasterList_Member
                                  {
                                      選択 = selsts,
                                      コード = mst.乗務員ID,
                                      表示名 = mst.乗務員名,
                                  }
                                  ).ToList();
                        break;
                    case "車輌":
                        result = (from mst in context.M05_CAR
                                  where mst.削除日付 == null
                                  orderby mst.車輌ID
                                  select new MasterList_Member
                                  {
                                      選択 = selsts,
                                      コード = mst.車輌ID,
                                      表示名 = mst.車輌番号,
                                  }
                                  ).ToList();
                        break;
                    case "車種":
                        result = (from mst in context.M06_SYA
                                  where mst.削除日付 == null
                                  orderby mst.車種ID
                                  select new MasterList_Member
                                  {
                                      選択 = selsts,
                                      コード = mst.車種ID,
                                      表示名 = mst.車種名,
                                  }
                                  ).ToList();
                        break;
                    case "発地":
                    case "着地":
                        result = (from mst in context.M08_TIK
                                  where mst.削除日付 == null
                                  orderby mst.発着地ID
                                  select new MasterList_Member
                                  {
                                      選択 = selsts,
                                      コード = mst.発着地ID,
                                      表示名 = mst.発着地名,
                                  }
                                  ).ToList();
                        break;
                    case "商品":
                        result = (from mst in context.M09_HIN
                                  where mst.削除日付 == null
                                  orderby mst.商品ID
                                  select new MasterList_Member
                                  {
                                      選択 = selsts,
                                      コード = mst.商品ID,
                                      表示名 = mst.商品名,
                                  }
                                  ).ToList();
                        break;
                }
            }

            return result;
        }


        public class DLY12010_TOTAL_Member
        {
            public decimal 売上金額合計 { get; set; }
            public decimal 請求割増１合計 { get; set; }
            public decimal 請求割増２合計 { get; set; }
            public decimal 通行料合計 { get; set; }
            public decimal 売上金額総合計 { get; set; }
            public decimal 数量合計 { get; set; }
            public decimal 重量合計 { get; set; }
            public decimal 支払社内合計 { get; set; }
            public decimal 支払通行料合計 { get; set; }
            public decimal 支払金額総合計 { get; set; }
        }
        public class DLY12010_DATASET
        {
            public List<DLY12010_Member> DataList = new List<DLY12010_Member>();
            public List<DLY12010_TOTAL_Member> TotalList = new List<DLY12010_TOTAL_Member>();
        }

        /// <summary>
        /// 売上明細問い合わせリスト取得
        /// </summary>
        /// <param name="p得意先ID">得意先ID(未選択の場合はnull)</param>
        /// <param name="p支払先ID">支払先ID(未選択の場合はnull)</param>
        /// <param name="p請求内訳ID">請求内訳ID(未選択の場合はnull)</param>
        /// <param name="p検索日付From">検索日付From(未選択の場合はnull)</param>
        /// <param name="p検索日付To">検索日付To(未選択の場合はnull)</param>
        /// <param name="p検索日付区分">検索日付区分</param>
        /// <param name="p自社部門ID">自社部門ID(未選択の場合はnull)</param>
        /// <param name="p売上未定区分">売上未定区分(0:全件 1:未定のみ 2:確定のみ 3:金額が未入力のみ)</param>
        /// <param name="p商品名">商品名</param>
        /// <param name="p発地名">発地名</param>
        /// <param name="p着地名">着地名</param>
        /// <param name="p請求摘要">請求摘要</param>
        /// <param name="p社内備考">社内備考</param>
        /// <returns>DLY12010_Memberのリスト</returns>
        public DLY12010_DATASET GetListDLY12010( int? p担当者ID, 
            int? p乗務員ID, int? p車輌ID, int? p請求内訳ID
            , DateTime? p検索日付From, DateTime? p検索日付To, int p検索日付区分
            , int? p自社部門ID, int? p売上未定区分
            , string p商品名, string p発地名, string p着地名, string p請求摘要, string p社内備考, int p締日
            , int p得意先FROM, int p得意先TO
            , int p支払先FROM, int p支払先TO
            , int p仕入先FROM, int p仕入先TO
            , int p乗務員FROM, int p乗務員TO
            , int p車輌FROM, int p車輌TO
            , int p車種FROM, int p車種TO
            , int p発地FROM, int p発地TO
            , int p着地FROM, int p着地TO
            , int p商品FROM, int p商品TO
            , int?[] p得意先
            , int?[] p支払先
            , int?[] p仕入先
            , int?[] p乗務員
            , int?[] p車輌
            , int?[] p車種
            , int?[] p発地
            , int?[] p着地
            , int?[] p商品
            )
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
                DLY12010_DATASET result = new DLY12010_DATASET();
                var query = (from t01 in context.T01_TRN.Where(x => (x.入力区分 != 3 || (x.入力区分 == 3 && x.明細行 != 1)) && (x.車輌KEY > 0 || x.乗務員KEY > 0) )
                             from tok in context.M01_TOK.Where(x => x.得意先KEY == t01.得意先KEY && (x.取引区分 == 0 || x.取引区分 == 1)).DefaultIfEmpty()
                             from m10 in context.M10_UHK.Where(x => x.得意先KEY == t01.得意先KEY && x.請求内訳ID == t01.請求内訳ID).DefaultIfEmpty()
                             from m06 in context.M06_SYA.Where(x => x.車種ID == t01.車種ID).DefaultIfEmpty()
                             from shr in context.M01_TOK.Where(x => x.得意先KEY == t01.支払先KEY && (x.取引区分 == 0 || x.取引区分 == 2)).DefaultIfEmpty()
                             from m04 in context.M04_DRV.Where(x => x.乗務員KEY == t01.乗務員KEY).DefaultIfEmpty()
                             from m05 in context.M05_CAR.Where(x => x.車輌KEY == t01.車輌KEY).DefaultIfEmpty()
                             where (p担当者ID == null || t01.入力者ID == p担当者ID)
                                && (p乗務員ID == null || m04.乗務員ID == p乗務員ID)
                                && (p車輌ID == null || m05.車輌ID == p車輌ID)
                                && (p自社部門ID == 0 || t01.自社部門ID == p自社部門ID)
                                && (p締日 < 0 || tok.Ｔ締日 == p締日)
                             && (string.IsNullOrEmpty(p発地名) || t01.発地名.Contains(p発地名))
                             && (string.IsNullOrEmpty(p着地名) || t01.着地名.Contains(p着地名))
                             && (string.IsNullOrEmpty(p商品名) || t01.商品名.Contains(p商品名))
                             && (string.IsNullOrEmpty(p請求摘要) || t01.請求摘要.Contains(p請求摘要))
                             && (string.IsNullOrEmpty(p社内備考) || t01.社内備考.Contains(p社内備考))
							 && ((p売上未定区分 == null) || (p売上未定区分 == 0) || (p売上未定区分 == 1 && t01.売上未定区分 == 0) || (p売上未定区分 == 2 && t01.売上未定区分 == 1) || (p売上未定区分 == 3 && (t01.売上金額 + t01.通行料 + t01.請求割増１ + t01.請求割増２) == 0))
                             select new DLY12010_Member
                             {
                                 明細番号 = t01.明細番号,
                                 明細行 = t01.明細行,
                                 社内区分 = t01.社内区分,
								 社内区分名 = t01.社内区分 == 1 ? "社内" : "",
								 入力区分 = t01.入力区分 == 1 ? "日報入力" : t01.入力区分 == 2 ? "売上入力" : t01.入力区分 == 3 ? "内訳入力" : "",
                                 請求日付 = t01.請求日付,
                                 支払日付 = t01.支払日付,
                                 配送日付 = t01.配送日付,
                                 配送時間 = t01.配送時間,
                                 得意先ID = tok.得意先ID,
                                 得意先名 = tok.略称名,
                                 請求内訳ID = t01.請求内訳ID,
                                 請求内訳名 = m10.請求内訳名,
                                 車輌ID = m05.車輌ID,
                                 車輌番号 = t01.車輌番号,
                                 車種ID = t01.車種ID,
                                 車種名 = m06.車種名,
                                 支払先ID = shr.得意先ID,
                                 支払先略称名 = shr.略称名,
                                 乗務員ID = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 支払先名２次 = t01.支払先名２次,
                                 実運送乗務員 = t01.実運送乗務員,
                                 乗務員連絡先 = t01.乗務員連絡先,
                                 数量 = t01.数量,
                                 単位 = t01.単位,
                                 重量 = t01.重量,
                                 単価 = t01.売上単価,
                                 走行ＫＭ = t01.走行ＫＭ,
                                 実車ＫＭ = t01.実車ＫＭ,
                                 売上単価 = t01.売上単価,
                                 売上金額 = t01.売上金額,
                                 d売上金額 = t01.売上金額,
                                 売上金額計 = t01.売上金額 + t01.通行料 + t01.請求割増１ + t01.請求割増２,
                                 d売上金額計 = t01.売上金額 + t01.通行料 + t01.請求割増１ + t01.請求割増２,
                                 通行料 = t01.通行料,
                                 d通行料 = t01.通行料,
                                 請求割増１ = t01.請求割増１,
                                 d請求割増１ = t01.請求割増１,
                                 請求割増２ = t01.請求割増２,
                                 d請求割増２ = t01.請求割増２,
                                 請求消費税 = t01.請求消費税,
                                 支払単価 = t01.支払単価,
                                 支払金額 = t01.支払金額,
                                 d支払金額 = t01.支払金額,
                                 支払通行料 = t01.支払通行料,
                                 d支払通行料 = t01.支払通行料,
                                 支払割増１ = t01.支払割増１,
                                 d支払割増１ = t01.支払割増１,
                                 支払割増２ = t01.支払割増２,
                                 d支払割増２ = t01.支払割増２,
								 支払金額計 = t01.支払金額 + t01.支払割増１ + t01.支払割増２ + t01.支払通行料,
                                 支払消費税 = t01.支払消費税,
                                 請求税区分 = t01.請求税区分,
                                 支払税区分 = t01.支払税区分,
                                 売上未定区分 = t01.売上未定区分,
                                 支払未定区分 = t01.支払未定区分,
                                 商品ID = t01.商品ID,
                                 商品名 = t01.商品名,
                                 発地ID = t01.発地ID,
                                 発地名 = t01.発地名,
                                 着地ID = t01.着地ID,
                                 着地名 = t01.着地名,
                                 請求摘要 = t01.請求摘要,
                                 社内備考 = t01.社内備考,
                                 S締日 = tok.Ｓ締日,
                                 T締日 = tok.Ｔ締日,
                                 自社部門 = t01.自社部門ID == null ? 0 : t01.自社部門ID,
                                 請求運賃計算区分ID = t01.請求運賃計算区分ID,
                                 支払運賃計算区分ID = t01.支払運賃計算区分ID,
                             }).AsQueryable();

                List<DLY12010_Member> ret;
                if (p検索日付From != null || p検索日付To != null)
                {
                    switch (p検索日付区分)
                    {
                        case 0:
                            // 請求日付
                            ret = (from q in query
                                   where (p検索日付From == null || q.請求日付 >= p検索日付From)
                                      && (p検索日付To == null || q.請求日付 <= p検索日付To)
                                   select q
                                   ).ToList();
                            break;
                        case 1:
                            // 支払日付
                            ret = (from q in query
                                   where (p検索日付From == null || q.支払日付 >= p検索日付From)
                                      && (p検索日付To == null || q.支払日付 <= p検索日付To)
                                   select q
                                   ).ToList();
                            break;
                        case 2:
                            // 配送日付
                            ret = (from q in query
                                   where (p検索日付From == null || q.配送日付 >= p検索日付From)
                                      && (p検索日付To == null || q.配送日付 <= p検索日付To)
                                   select q
                                   ).ToList();
                            break;
                        default:
                            ret = query.ToList();
                            break;
                    }
                }
                else
                {
                    ret = query.ToList();
                }

                // コードの範囲指定での絞り込み
                if (p得意先FROM >= 0)
                    ret = ret.Where(x => x.得意先ID >= p得意先FROM).ToList();
                if (p得意先TO >= 0)
                    ret = ret.Where(x => x.得意先ID <= p得意先TO).ToList();
                if (p支払先FROM >= 0)
                    ret = ret.Where(x => x.支払先ID >= p支払先FROM).ToList();
                if (p支払先TO >= 0)
                    ret = ret.Where(x => x.支払先ID <= p支払先TO).ToList();
                if (p乗務員FROM >= 0)
                    ret = ret.Where(x => x.乗務員ID >= p乗務員FROM).ToList();
                if (p乗務員TO >= 0)
                    ret = ret.Where(x => x.乗務員ID <= p乗務員TO).ToList();
                if (p車輌FROM >= 0)
                    ret = ret.Where(x => x.車輌ID >= p車輌FROM).ToList();
                if (p車輌TO >= 0)
                    ret = ret.Where(x => x.車輌ID <= p車輌TO).ToList();
                if (p車種FROM >= 0)
                    ret = ret.Where(x => x.車種ID >= p車種FROM).ToList();
                if (p車種TO >= 0)
                    ret = ret.Where(x => x.車種ID <= p車種TO).ToList();
                if (p発地FROM >= 0)
                    ret = ret.Where(x => x.発地ID >= p発地FROM).ToList();
                if (p発地TO >= 0)
                    ret = ret.Where(x => x.発地ID <= p発地TO).ToList();
                if (p着地FROM >= 0)
                    ret = ret.Where(x => x.着地ID >= p着地FROM).ToList();
                if (p着地TO >= 0)
                    ret = ret.Where(x => x.着地ID <= p着地TO).ToList();
                if (p商品FROM >= 0)
                    ret = ret.Where(x => x.商品ID >= p商品FROM).ToList();
                if (p商品TO >= 0)
                    ret = ret.Where(x => x.商品ID <= p商品TO).ToList();

                // コード一覧の絞込み
                if (p得意先.Length > 0)
                    ret = ret.Where(x => p得意先.Contains(x.得意先ID)).ToList();
                if (p支払先.Length > 0)
                    ret = ret.Where(x => p支払先.Contains(x.支払先ID)).ToList();
                if (p仕入先.Length > 0)
                    ret = ret.Where(x => p仕入先.Contains(x.支払先ID)).ToList();
                if (p乗務員.Length > 0)
                    ret = ret.Where(x => p乗務員.Contains(x.乗務員ID)).ToList();
                if (p車輌.Length > 0)
                    ret = ret.Where(x => p車輌.Contains(x.車輌ID)).ToList();
                if (p車種.Length > 0)
                    ret = ret.Where(x => p車種.Contains(x.車種ID)).ToList();
                if (p発地.Length > 0)
                    ret = ret.Where(x => p発地.Contains((int?)x.発地ID)).ToList();
                if (p着地.Length > 0)
                    ret = ret.Where(x => p着地.Contains((int?)x.着地ID)).ToList();
                if (p商品.Length > 0)
                    ret = ret.Where(x => p商品.Contains((int?)x.商品ID)).ToList();

                //合計計算
                result.DataList = ret;
                result.TotalList.Add(new DLY12010_TOTAL_Member());
                foreach (var rec in result.DataList)
                {
                    result.TotalList[0].売上金額合計 += rec.売上金額;
                    result.TotalList[0].請求割増１合計 += rec.請求割増１;
                    result.TotalList[0].請求割増２合計 += rec.請求割増２;
                    result.TotalList[0].通行料合計 += rec.通行料;
                    result.TotalList[0].売上金額総合計 += rec.売上金額 + rec.請求割増１ + rec.請求割増２ + rec.通行料;

                    result.TotalList[0].支払社内合計 += rec.支払金額;
                    result.TotalList[0].重量合計 += rec.重量;
                    result.TotalList[0].数量合計 += rec.数量;
                    result.TotalList[0].支払通行料合計 += rec.支払通行料;
                    result.TotalList[0].支払金額総合計 += rec.支払金額 + rec.支払通行料;
                }
                return result;
            }
        }

        /// <summary>
        /// 売上明細問い合わせリスト取得
        /// </summary>
        /// <returns>DLY12010_Memberのリスト</returns>
        public List<DLY12010_Member> GetListDLY12010_PRT( int? p担当者ID, 
            int? p乗務員ID, int? p車輌ID, int? p請求内訳ID
            , DateTime? p検索日付From, DateTime? p検索日付To, int p検索日付区分
            , int? p自社部門ID, int? p売上未定区分
            , string p商品名, string p発地名, string p着地名, string p請求摘要, string p社内備考
            , string s表示順序1, string s表示順序2, string s表示順序3, string s表示順序4, string s表示順序5
            , int p得意先FROM, int p得意先TO
            , int p支払先FROM, int p支払先TO
            , int p仕入先FROM, int p仕入先TO
            , int p乗務員FROM, int p乗務員TO
            , int p車輌FROM, int p車輌TO
            , int p車種FROM, int p車種TO
            , int p発地FROM, int p発地TO
            , int p着地FROM, int p着地TO
            , int p商品FROM, int p商品TO
            , int?[] p得意先
            , int?[] p支払先
            , int?[] p仕入先
            , int?[] p乗務員
            , int?[] p車輌
            , int?[] p車種
            , int?[] p発地
            , int?[] p着地
            , int?[] p商品
            )
        {

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                DLY12010_DATASET result = new DLY12010_DATASET();
                var query = (from t01 in context.T01_TRN.Where(x => (x.入力区分 != 3 || (x.入力区分 == 3 && x.明細行 != 1)) && (x.乗務員KEY > 0 || x.車輌KEY > 0) )
                             from tok in context.M01_TOK.Where(x => x.得意先KEY == t01.得意先KEY && (x.取引区分 == 0 || x.取引区分 == 1)).DefaultIfEmpty()
                             from m10 in context.M10_UHK.Where(x => x.得意先KEY == t01.得意先KEY && x.請求内訳ID == t01.請求内訳ID).DefaultIfEmpty()
                             from m06 in context.M06_SYA.Where(x => x.車種ID == t01.車種ID).DefaultIfEmpty()
                             from shr in context.M01_TOK.Where(x => x.得意先KEY == t01.支払先KEY && (x.取引区分 == 0 || x.取引区分 == 2)).DefaultIfEmpty()
                             from m04 in context.M04_DRV.Where(x => x.乗務員KEY == t01.乗務員KEY).DefaultIfEmpty()
                             from m05 in context.M05_CAR.Where(x => x.車輌KEY == t01.車輌KEY).DefaultIfEmpty()
                             where (p担当者ID == null || t01.入力者ID == p担当者ID)
                                && (p乗務員ID == null || m04.乗務員ID == p乗務員ID)
                                && (p車輌ID == null || m05.車輌ID == p車輌ID)
                                && (p自社部門ID == 0 || t01.自社部門ID == p自社部門ID)
                             && (string.IsNullOrEmpty(p発地名) || t01.発地名.Contains(p発地名))
                             && (string.IsNullOrEmpty(p着地名) || t01.着地名.Contains(p着地名))
                             && (string.IsNullOrEmpty(p商品名) || t01.商品名.Contains(p商品名))
                             && (string.IsNullOrEmpty(p請求摘要) || t01.請求摘要.Contains(p請求摘要))
                             && (string.IsNullOrEmpty(p社内備考) || t01.社内備考.Contains(p社内備考))
                             && ((p売上未定区分 == null) || (p売上未定区分 == 0) || (p売上未定区分 == 1 && t01.売上未定区分 == 1) || (p売上未定区分 == 2 && t01.売上未定区分 == 0) || (p売上未定区分 == 3 && t01.売上金額 == 0))
                             select new DLY12010_Member
                             {
                                 明細番号 = t01.明細番号,
                                 明細行 = t01.明細行,
                                 社内区分 = t01.社内区分,
								 入力区分 = t01.入力区分 == 1 ? "日報入力" : t01.入力区分 == 2 ? "売上入力" : t01.入力区分 == 3 ? "内訳入力" : "",
                                 請求日付 = t01.請求日付,
                                 支払日付 = t01.支払日付,
                                 配送日付 = t01.配送日付,
                                 配送時間 = t01.配送時間,
                                 得意先ID = tok.得意先ID,
                                 得意先名 = tok.略称名,
                                 請求内訳ID = t01.請求内訳ID,
                                 請求内訳名 = m10.請求内訳名,
                                 車輌ID = m05.車輌ID,
                                 車輌番号 = t01.車輌番号,
                                 車種ID = t01.車種ID,
                                 車種名 = m06.車種名,
                                 支払先ID = shr.得意先ID,
                                 支払先略称名 = shr.略称名,
                                 乗務員ID = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 支払先名２次 = t01.支払先名２次,
                                 実運送乗務員 = t01.実運送乗務員,
                                 乗務員連絡先 = t01.乗務員連絡先,
                                 数量 = t01.数量,
                                 単位 = t01.単位,
                                 重量 = t01.重量,
                                 走行ＫＭ = t01.走行ＫＭ,
                                 実車ＫＭ = t01.実車ＫＭ,
                                 売上単価 = t01.売上単価,
                                 売上金額 = t01.売上金額,
                                 通行料 = t01.通行料,
                                 請求割増１ = t01.請求割増１,
                                 請求割増２ = t01.請求割増２,
                                 請求消費税 = t01.請求消費税,
                                 支払単価 = t01.支払単価,
                                 支払金額 = t01.支払金額,
                                 支払通行料 = t01.支払通行料,
                                 支払割増１ = t01.支払割増１,
                                 支払割増２ = t01.支払割増２,
								 支払金額計 = t01.支払金額 + t01.支払割増１ + t01.支払割増２ + t01.支払通行料,
								 支払消費税 = t01.支払消費税,
                                 売上未定区分 = t01.売上未定区分,
                                 支払未定区分 = t01.支払未定区分,
                                 商品ID = t01.商品ID,
                                 商品名 = t01.商品名,
                                 発地ID = t01.発地ID,
                                 発地名 = t01.発地名,
                                 着地ID = t01.着地ID,
                                 着地名 = t01.着地名,
                                 請求摘要 = t01.請求摘要,
                                 社内備考 = t01.社内備考,
                                 S締日 = tok.Ｓ締日,
                                 T締日 = tok.Ｔ締日,
                                 表示順序1 = s表示順序1,
                                 表示順序2 = s表示順序2,
                                 表示順序3 = s表示順序3,
                                 表示順序4 = s表示順序4,
                                 表示順序5 = s表示順序5,
                                 自社部門 = t01.自社部門ID == null ? 0 : t01.自社部門ID,
                             }).AsQueryable();

                List<DLY12010_Member> ret;
                if (p検索日付From != null || p検索日付To != null)
                {
                    switch (p検索日付区分)
                    {
                        case 0:
                            // 請求日付
                            ret = (from q in query
                                   where (p検索日付From == null || q.請求日付 >= p検索日付From)
                                      && (p検索日付To == null || q.請求日付 <= p検索日付To)
                                   select q
                                   ).ToList();
                            break;
                        case 1:
                            // 支払日付
                            ret = (from q in query
                                   where (p検索日付From == null || q.支払日付 >= p検索日付From)
                                      && (p検索日付To == null || q.支払日付 <= p検索日付To)
                                   select q
                                   ).ToList();
                            break;
                        case 2:
                            // 配送日付
                            ret = (from q in query
                                   where (p検索日付From == null || q.配送日付 >= p検索日付From)
                                      && (p検索日付To == null || q.配送日付 <= p検索日付To)
                                   select q
                                   ).ToList();
                            break;
                        default:
                            ret = query.ToList();
                            break;
                    }
                }
                else
                {
                    ret = query.ToList();
                }

                // コードの範囲指定での絞り込み
                if (p得意先FROM >= 0)
                    ret = ret.Where(x => x.得意先ID >= p得意先FROM).ToList();
                if (p得意先TO >= 0)
                    ret = ret.Where(x => x.得意先ID <= p得意先TO).ToList();
                if (p支払先FROM >= 0)
                    ret = ret.Where(x => x.支払先ID >= p支払先FROM).ToList();
                if (p支払先TO >= 0)
                    ret = ret.Where(x => x.支払先ID <= p支払先TO).ToList();
                if (p乗務員FROM >= 0)
                    ret = ret.Where(x => x.乗務員ID >= p乗務員FROM).ToList();
                if (p乗務員TO >= 0)
                    ret = ret.Where(x => x.乗務員ID <= p乗務員TO).ToList();
                if (p車輌FROM >= 0)
                    ret = ret.Where(x => x.車輌ID >= p車輌FROM).ToList();
                if (p車輌TO >= 0)
                    ret = ret.Where(x => x.車輌ID <= p車輌TO).ToList();
                if (p車種FROM >= 0)
                    ret = ret.Where(x => x.車種ID >= p車種FROM).ToList();
                if (p車種TO >= 0)
                    ret = ret.Where(x => x.車種ID <= p車種TO).ToList();
                if (p発地FROM >= 0)
                    ret = ret.Where(x => x.発地ID >= p発地FROM).ToList();
                if (p発地TO >= 0)
                    ret = ret.Where(x => x.発地ID <= p発地TO).ToList();
                if (p着地FROM >= 0)
                    ret = ret.Where(x => x.着地ID >= p着地FROM).ToList();
                if (p着地TO >= 0)
                    ret = ret.Where(x => x.着地ID <= p着地TO).ToList();
                if (p商品FROM >= 0)
                    ret = ret.Where(x => x.商品ID >= p商品FROM).ToList();
                if (p商品TO >= 0)
                    ret = ret.Where(x => x.商品ID <= p商品TO).ToList();

                // コード一覧の絞込み
                if (p得意先.Length > 0)
                    ret = ret.Where(x => p得意先.Contains(x.得意先ID)).ToList();
                if (p支払先.Length > 0)
                    ret = ret.Where(x => p支払先.Contains(x.支払先ID)).ToList();
                if (p仕入先.Length > 0)
                    ret = ret.Where(x => p仕入先.Contains(x.支払先ID)).ToList();
                if (p乗務員.Length > 0)
                    ret = ret.Where(x => p乗務員.Contains(x.乗務員ID)).ToList();
                if (p車輌.Length > 0)
                    ret = ret.Where(x => p車輌.Contains(x.車輌ID)).ToList();
                if (p車種.Length > 0)
                    ret = ret.Where(x => p車種.Contains(x.車種ID)).ToList();
                if (p発地.Length > 0)
                    ret = ret.Where(x => p発地.Contains((int?)x.発地ID)).ToList();
                if (p着地.Length > 0)
                    ret = ret.Where(x => p着地.Contains((int?)x.着地ID)).ToList();
                if (p商品.Length > 0)
                    ret = ret.Where(x => p商品.Contains((int?)x.商品ID)).ToList();


                return ret;
            }
        }

        /// <summary>
        /// 売上明細問い合わせリスト取得
        /// </summary>
        /// <returns>DLY12010_Memberのリスト</returns>
        public List<DLY12010_Member> GetListDLY12010_CSV(
            int? p乗務員ID, int? p車輌ID, int? p請求内訳ID
            , DateTime? p検索日付From, DateTime? p検索日付To, int p検索日付区分
            , int? p自社部門ID, int? p売上未定区分
            , string p商品名, string p発地名, string p着地名, string p請求摘要, string p社内備考
            , string s表示順序1, string s表示順序2, string s表示順序3, string s表示順序4, string s表示順序5
            , int p得意先FROM, int p得意先TO
            , int p支払先FROM, int p支払先TO
            , int p仕入先FROM, int p仕入先TO
            , int p乗務員FROM, int p乗務員TO
            , int p車輌FROM, int p車輌TO
            , int p車種FROM, int p車種TO
            , int p発地FROM, int p発地TO
            , int p着地FROM, int p着地TO
            , int p商品FROM, int p商品TO
            , int?[] p得意先
            , int?[] p支払先
            , int?[] p仕入先
            , int?[] p乗務員
            , int?[] p車輌
            , int?[] p車種
            , int?[] p発地
            , int?[] p着地
            , int?[] p商品
            )
        {

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                DLY12010_DATASET result = new DLY12010_DATASET();
                var query = (from t01 in context.T01_TRN.Where(x => (x.入力区分 != 3 || (x.入力区分 == 3 && x.明細行 != 1)) && (x.車輌KEY > 0 || x.乗務員KEY > 0))
                             from tok in context.M01_TOK.Where(x => x.得意先KEY == t01.得意先KEY && (x.取引区分 == 0 || x.取引区分 == 1)).DefaultIfEmpty()
                             from m10 in context.M10_UHK.Where(x => x.得意先KEY == t01.得意先KEY && x.請求内訳ID == t01.請求内訳ID).DefaultIfEmpty()
                             from m06 in context.M06_SYA.Where(x => x.車種ID == t01.車種ID).DefaultIfEmpty()
                             from shr in context.M01_TOK.Where(x => x.得意先KEY == t01.支払先KEY && (x.取引区分 == 0 || x.取引区分 == 2)).DefaultIfEmpty()
                             from m04 in context.M04_DRV.Where(x => x.乗務員KEY == t01.乗務員KEY).DefaultIfEmpty()
                             from m05 in context.M05_CAR.Where(x => x.車輌KEY == t01.車輌KEY).DefaultIfEmpty()
                             where (p乗務員ID == null || m04.乗務員ID == p乗務員ID)
                                && (p車輌ID == null || m05.車輌ID == p車輌ID)
                                && (p自社部門ID == 0 || t01.自社部門ID == p自社部門ID)
                             && (string.IsNullOrEmpty(p発地名) || t01.発地名.Contains(p発地名))
                             && (string.IsNullOrEmpty(p着地名) || t01.着地名.Contains(p着地名))
                             && (string.IsNullOrEmpty(p商品名) || t01.商品名.Contains(p商品名))
                             && (string.IsNullOrEmpty(p請求摘要) || t01.請求摘要.Contains(p請求摘要))
                             && (string.IsNullOrEmpty(p社内備考) || t01.社内備考.Contains(p社内備考))
							 && ((p売上未定区分 == null) || (p売上未定区分 == 0) || (p売上未定区分 == 1 && t01.売上未定区分 == 0) || (p売上未定区分 == 2 && t01.売上未定区分 == 1) || (p売上未定区分 == 3 && (t01.売上金額 + t01.通行料 + t01.請求割増１ + t01.請求割増２) == 0))
                             select new DLY12010_Member
                             {
                                 明細番号 = t01.明細番号,
                                 明細行 = t01.明細行,
								 入力区分 = t01.入力区分 == 1 ? "日報入力" : t01.入力区分 == 2 ? "売上入力" : t01.入力区分 == 3 ? "内訳入力" : "",
                                 請求日付 = t01.請求日付,
                                 支払日付 = t01.支払日付,
                                 配送日付 = t01.配送日付,
                                 配送時間 = t01.配送時間,
                                 得意先ID = tok.得意先ID,
                                 得意先名 = tok.略称名,
                                 請求内訳ID = t01.請求内訳ID,
                                 請求内訳名 = m10.請求内訳名,
                                 車輌ID = m05.車輌ID,
                                 車輌番号 = t01.車輌番号,
                                 車種ID = t01.車種ID,
                                 車種名 = m06.車種名,
                                 支払先ID = shr.得意先ID,
                                 支払先略称名 = shr.略称名,
                                 乗務員ID = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 支払先名２次 = t01.支払先名２次,
                                 実運送乗務員 = t01.実運送乗務員,
                                 乗務員連絡先 = t01.乗務員連絡先,
                                 数量 = t01.数量,
                                 単位 = t01.単位,
                                 重量 = t01.重量,
                                 走行ＫＭ = t01.走行ＫＭ,
                                 実車ＫＭ = t01.実車ＫＭ,
                                 売上単価 = t01.売上単価,
                                 売上金額 = t01.売上金額,
                                 通行料 = t01.通行料,
                                 請求割増１ = t01.請求割増１,
                                 請求割増２ = t01.請求割増２,
                                 請求消費税 = t01.請求消費税,
                                 支払単価 = t01.支払単価,
                                 支払金額 = t01.支払金額,
                                 支払通行料 = t01.支払通行料,
                                 支払割増１ = t01.支払割増１,
                                 支払割増２ = t01.支払割増２,
                                 支払消費税 = t01.支払消費税,
								 支払金額計 = t01.支払金額 + t01.支払割増１ + t01.支払割増２ + t01.支払通行料,
								 社内区分 = t01.社内区分,
                                 請求税区分 = t01.請求税区分,
                                 支払税区分 = t01.支払税区分,
                                 売上未定区分 = t01.売上未定区分,
                                 支払未定区分 = t01.支払未定区分,
                                 商品ID = t01.商品ID,
                                 商品名 = t01.商品名,
                                 発地ID = t01.発地ID,
                                 発地名 = t01.発地名,
                                 着地ID = t01.着地ID,
                                 着地名 = t01.着地名,
                                 請求摘要 = t01.請求摘要,
                                 社内備考 = t01.社内備考,
                                 S締日 = tok.Ｓ締日,
                                 T締日 = tok.Ｔ締日,
                                 表示順序1 = s表示順序1,
                                 表示順序2 = s表示順序2,
                                 表示順序3 = s表示順序3,
                                 表示順序4 = s表示順序4,
                                 表示順序5 = s表示順序5,
                                 自社部門 = t01.自社部門ID == null ? 0 : t01.自社部門ID,
                                 請求運賃計算区分ID = t01.請求運賃計算区分ID,
                                 支払運賃計算区分ID = t01.支払運賃計算区分ID,
                             }).AsQueryable();

                List<DLY12010_Member> ret;
                if (p検索日付From != null || p検索日付To != null)
                {
                    switch (p検索日付区分)
                    {
                        case 0:
                            // 請求日付
                            ret = (from q in query
                                   where (p検索日付From == null || q.請求日付 >= p検索日付From)
                                      && (p検索日付To == null || q.請求日付 <= p検索日付To)
                                   select q
                                   ).ToList();
                            break;
                        case 1:
                            // 支払日付
                            ret = (from q in query
                                   where (p検索日付From == null || q.支払日付 >= p検索日付From)
                                      && (p検索日付To == null || q.支払日付 <= p検索日付To)
                                   select q
                                   ).ToList();
                            break;
                        case 2:
                            // 配送日付
                            ret = (from q in query
                                   where (p検索日付From == null || q.配送日付 >= p検索日付From)
                                      && (p検索日付To == null || q.配送日付 <= p検索日付To)
                                   select q
                                   ).ToList();
                            break;
                        default:
                            ret = query.ToList();
                            break;
                    }
                }
                else
                {
                    ret = query.ToList();
                }

                // コードの範囲指定での絞り込み
                if (p得意先FROM >= 0)
                    ret = ret.Where(x => x.得意先ID >= p得意先FROM).ToList();
                if (p得意先TO >= 0)
                    ret = ret.Where(x => x.得意先ID <= p得意先TO).ToList();
                if (p支払先FROM >= 0)
                    ret = ret.Where(x => x.支払先ID >= p支払先FROM).ToList();
                if (p支払先TO >= 0)
                    ret = ret.Where(x => x.支払先ID <= p支払先TO).ToList();
                if (p乗務員FROM >= 0)
                    ret = ret.Where(x => x.乗務員ID >= p乗務員FROM).ToList();
                if (p乗務員TO >= 0)
                    ret = ret.Where(x => x.乗務員ID <= p乗務員TO).ToList();
                if (p車輌FROM >= 0)
                    ret = ret.Where(x => x.車輌ID >= p車輌FROM).ToList();
                if (p車輌TO >= 0)
                    ret = ret.Where(x => x.車輌ID <= p車輌TO).ToList();
                if (p車種FROM >= 0)
                    ret = ret.Where(x => x.車種ID >= p車種FROM).ToList();
                if (p車種TO >= 0)
                    ret = ret.Where(x => x.車種ID <= p車種TO).ToList();
                if (p発地FROM >= 0)
                    ret = ret.Where(x => x.発地ID >= p発地FROM).ToList();
                if (p発地TO >= 0)
                    ret = ret.Where(x => x.発地ID <= p発地TO).ToList();
                if (p着地FROM >= 0)
                    ret = ret.Where(x => x.着地ID >= p着地FROM).ToList();
                if (p着地TO >= 0)
                    ret = ret.Where(x => x.着地ID <= p着地TO).ToList();
                if (p商品FROM >= 0)
                    ret = ret.Where(x => x.商品ID >= p商品FROM).ToList();
                if (p商品TO >= 0)
                    ret = ret.Where(x => x.商品ID <= p商品TO).ToList();

                // コード一覧の絞込み
                if (p得意先.Length > 0)
                    ret = ret.Where(x => p得意先.Contains(x.得意先ID)).ToList();
                if (p支払先.Length > 0)
                    ret = ret.Where(x => p支払先.Contains(x.支払先ID)).ToList();
                if (p仕入先.Length > 0)
                    ret = ret.Where(x => p仕入先.Contains(x.支払先ID)).ToList();
                if (p乗務員.Length > 0)
                    ret = ret.Where(x => p乗務員.Contains(x.乗務員ID)).ToList();
                if (p車輌.Length > 0)
                    ret = ret.Where(x => p車輌.Contains(x.車輌ID)).ToList();
                if (p車種.Length > 0)
                    ret = ret.Where(x => p車種.Contains(x.車種ID)).ToList();
                if (p発地.Length > 0)
                    ret = ret.Where(x => p発地.Contains((int?)x.発地ID)).ToList();
                if (p着地.Length > 0)
                    ret = ret.Where(x => p着地.Contains((int?)x.着地ID)).ToList();
                if (p商品.Length > 0)
                    ret = ret.Where(x => p商品.Contains((int?)x.商品ID)).ToList();


                return ret;
            }
        }


        public void UpdateColumn(int p明細番号, int p明細行, string colname, object val, int kingaku, decimal tanka)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                //var query = (from t01 in context.T01_TRN


                // トランザクションのインスタンス化(開始)
                using (var tran = new TransactionScope())
                {
                    DateTime updtime = DateTime.Now;
                    string sql = string.Empty;
                    if (colname == "売上金額")
                    {
                        sql = string.Format("UPDATE T01_TRN SET {0} = '{1}' , 売上単価 = '{4}' WHERE 明細番号 = {2} AND 明細行 = {3}"
                                            , colname, val.ToString(), p明細番号, p明細行, tanka.ToString());
                    }
                    if (colname == "売上単価")
                    {
                        sql = string.Format("UPDATE T01_TRN SET {0} = '{1}' , 売上金額 = '{4}' WHERE 明細番号 = {2} AND 明細行 = {3}"
                                            , colname, val.ToString(), p明細番号, p明細行, kingaku.ToString());
                    }
                    if (colname == "支払金額")
                    {
                        sql = string.Format("UPDATE T01_TRN SET {0} = '{1}' , 支払単価 = '{4}' WHERE 明細番号 = {2} AND 明細行 = {3}"
                                            , colname, val.ToString(), p明細番号, p明細行, tanka.ToString());
                    }
                    if (colname == "支払単価")
                    {
                        sql = string.Format("UPDATE T01_TRN SET {0} = '{1}' , 支払金額 = '{4}' WHERE 明細番号 = {2} AND 明細行 = {3}"
                                            , colname, val.ToString(), p明細番号, p明細行, kingaku.ToString());
                    }
                    else if (colname == "Goukei")
                    {
                        sql = string.Format("UPDATE T01_TRN SET 売上金額 = '{4}' , 売上単価 = '{5}' WHERE 明細番号 = {2} AND 明細行 = {3}"
                                            , colname, val.ToString(), p明細番号, p明細行, kingaku.ToString(), tanka.ToString());
                    }
                    else if (colname == "Total")
                    {
                        sql = string.Format("UPDATE T01_TRN SET 支払金額 = '{4}' , 支払単価 = '{5}' WHERE 明細番号 = {2} AND 明細行 = {3}"
                                            , colname, val.ToString(), p明細番号, p明細行, kingaku.ToString(), tanka.ToString());
                    }
                    else
                    {
                        sql = string.Format("UPDATE T01_TRN SET {0} = '{1}' , 支払金額 = '{4}' , 支払単価 = '{5}' WHERE 明細番号 = {2} AND 明細行 = {3}"
                                            , colname, val.ToString(), p明細番号, p明細行, kingaku.ToString(), tanka.ToString());
                    }
                    context.Connection.Open();
                    int count = context.ExecuteStoreCommand(sql);
                    // トリガが定義されていると、更新結果は複数行になる
                    if (count > 0)
                    {
                        tran.Complete();
                    }
                    else
                    {
                        // 更新行なし
                        throw new Framework.Common.DBDataNotFoundException();
                    }

                }
            }
        }

        public void UpdateColumn2(int p明細番号, int p明細行, string colname, object val)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                //var query = (from t01 in context.T01_TRN


                // トランザクションのインスタンス化(開始)
                using (var tran = new TransactionScope())
                {
                    if (colname == "請求年月日")
                    {
                        colname = "請求日付";
                    }
                    else if (colname == "支払年月日")
                    {
                        colname = "支払日付";
                    }
                    else if (colname == "配送年月日")
                    {
                        colname = "配送日付";
                    }

                    DateTime updtime = DateTime.Now;
                    string sql = string.Empty;
                    sql = string.Format("UPDATE T01_TRN SET {0} = '{1}' WHERE 明細番号 = {2} AND 明細行 = {3}"
                                        , colname, val.ToString(), p明細番号, p明細行);
                    context.Connection.Open();
                    int count = context.ExecuteStoreCommand(sql);
                    // トリガが定義されていると、更新結果は複数行になる
                    if (count > 0)
                    {
                        tran.Complete();
                    }
                    else
                    {
                        // 更新行なし
                        throw new Framework.Common.DBDataNotFoundException();
                    }

                }
            }
        }


        #region 単価・金額計算用関数

        public class DLY01010_TANKA
        {
            public int Kubun { get; set; }
            public decimal? Tanka { get; set; }
            public decimal? Kingaku { get; set; }
        }

        //請求用【売上金額・売上単価】計算
        public List<DLY01010_TANKA> GetUnitCostTOK(int p計算区分, int p請求支払区分, int p得意先ID, int p発地ID, int p着地ID, int p商品ID, int p車種ID, int p走行ＫＭ, decimal p重量, decimal p数量 , int pflg)
        {
            /*
             コード	表示名
                0	手入力
                1	数量計算
                2	重量計算
                3	運賃タリフ
                4	地区単価
                5	車種運賃
                6	個建単価
             */
            DLY01010_TANKA result = new DLY01010_TANKA();
            result.Kubun = 0;
            result.Tanka = -1;
            result.Kingaku = -1;
            switch (p計算区分)
            {
                case 3:
                    GetUnitCostTariff(result, p請求支払区分, p得意先ID, p重量, p走行ＫＭ , pflg);
                    break;
                case 4:
                    GetUnitCostArea(result, p請求支払区分, p得意先ID, p発地ID, p着地ID, p商品ID, p重量, p数量 , pflg);
                    break;
                case 5:
                    GetUnitCostCar(result, p請求支払区分, p得意先ID, p発地ID, p着地ID, p車種ID, p重量, p数量 , pflg);
                    break;
                case 6:
                    GetUnitCostMatrix(result, p請求支払区分, p得意先ID, p着地ID, p重量, p数量);
                    break;
            }

            return new List<DLY01010_TANKA>() { result };
        }

        //支払用【支払金額・支払単価】計算
        public List<DLY01010_TANKA> GetUnitCostSHR(int p計算区分, int p請求支払区分, int p得意先ID, int p発地ID, int p着地ID, int p商品ID, int p車種ID, int p走行ＫＭ, decimal p重量, decimal p数量 , int pflg)
        {
            /*
             コード	表示名
                0	手入力
                1	数量計算
                2	重量計算
                3	運賃タリフ
                4	地区単価
                5	車種運賃
                6	個建単価
             */
            DLY01010_TANKA result = new DLY01010_TANKA();
            result.Kubun = 1;
            result.Tanka = -1;
            result.Kingaku = -1;
            switch (p請求支払区分)
            {
                case 3:
                    GetUnitCostTariff(result, p請求支払区分, p得意先ID, p重量, p走行ＫＭ , pflg);
                    break;
                case 4:
                    GetUnitCostArea(result, p請求支払区分, p得意先ID, p発地ID, p着地ID, p商品ID, p重量, p数量 , pflg);
                    break;
                case 5:
                    GetUnitCostCar(result, p請求支払区分, p得意先ID, p発地ID, p着地ID, p車種ID, p重量, p数量 , pflg);
                    break;
                case 6:
                    GetUnitCostMatrix(result, p請求支払区分, p得意先ID, p着地ID, p重量, p数量);
                    break;
            }

            return new List<DLY01010_TANKA>() { result };
        }

        /// <summary>
        /// 距離と重量による運賃計算
        /// </summary>
        /// <param name="result"></param>
        /// <param name="p請求支払区分"></param>
        /// <param name="p得意先ID"></param>
        /// <param name="p重量"></param>
        /// <param name="p走行ＫＭ"></param>
        public void GetUnitCostTariff(DLY01010_TANKA result, int p請求支払区分, int p得意先ID, decimal p重量, int p走行ＫＭ , int pflg)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                p重量 *= 1000;
                context.Connection.Open();
                int tcode;
                if (pflg == 0)
                {
                    tcode = (from k in context.M01_TOK
                             where k.得意先ID == p得意先ID
                                && k.削除日付 == null
                             select k.Ｔ路線計算年度).FirstOrDefault();
                }
                else
                {
                    tcode = (from k in context.M01_TOK
                             where k.得意先ID == p得意先ID
                                && k.削除日付 == null
                             select k.Ｔ路線計算年度).FirstOrDefault();
                }
                decimal? kingaku = (from r in context.M50_RTBL
                                    where r.タリフコード == tcode
                                       && r.距離 >= p走行ＫＭ
                                       && r.重量 >= p重量
                                       && r.削除日付 == null
                                    orderby r.距離, r.重量
                                    select r.運賃).FirstOrDefault();
                if (kingaku != null)
                {
                    result.Kingaku = (decimal)kingaku;
                }

            }

            return;
        }

        /// <summary>
        /// 商品と地区による運賃計算
        /// </summary>
        /// <param name="result"></param>
        /// <param name="p請求支払区分"></param>
        /// <param name="p得意先ID"></param>
        /// <param name="p発地ID"></param>
        /// <param name="p着地ID"></param>
        /// <param name="p商品ID"></param>
        /// <param name="p重量"></param>
        /// <param name="p数量"></param>
        public void GetUnitCostArea(DLY01010_TANKA result, int p請求支払区分, int p得意先ID, int p発地ID, int p着地ID, int p商品ID, decimal p重量, decimal p数量 , int pflg)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                int tcode = (from k in context.M01_TOK
                             where k.得意先ID == p得意先ID
                             && k.削除日付 == null
                             select k.得意先KEY).FirstOrDefault();

                if (pflg == 0)
                {
                    var tankadata = (from r in context.M02_TTAN1
                                     where r.得意先KEY == tcode
                                        && r.商品ID == p商品ID
                                        && (r.発地ID == p発地ID || r.発地ID == 0)
                                        && (r.着地ID == p着地ID || r.着地ID == 0)
                                        && r.削除日付 == null
                                     orderby r.発地ID descending, r.着地ID descending
                                     select r).FirstOrDefault();

                    if (tankadata != null)
                    {
                        if (tankadata.計算区分 == 0)
                        {
                            result.Tanka = tankadata.売上単価;
                            result.Kingaku = p数量 * result.Tanka;
                        }
                        else if (tankadata.計算区分 == 1)
                        {
                            result.Tanka = tankadata.売上単価;
                            result.Kingaku = p重量 * result.Tanka;
                        }
                        else
                        {
                            result.Tanka = tankadata.売上単価;
                            result.Kingaku = p重量 * p数量 * result.Tanka;
                        }
                    }
                    else
                    {
                        result.Tanka = 0;
                        result.Kingaku = 0;
                    }
                }
                else
                {
                    var tankadata = (from r in context.M03_YTAN1
                                     where r.支払先KEY == tcode
                                        && r.商品ID == p商品ID
                                        && (r.発地ID == p発地ID || r.発地ID == 0)
                                        && (r.着地ID == p着地ID || r.着地ID == 0)
                                        && r.削除日付 == null
                                     orderby r.発地ID descending, r.着地ID descending
                                     select r).FirstOrDefault();


                    if (tankadata != null)
                    {
                        if (tankadata.計算区分 == 0)
                        {
                            result.Tanka = tankadata.支払単価;
                            result.Kingaku = p数量 * result.Tanka;
                        }
                        else if (tankadata.計算区分 == 1)
                        {
                            result.Tanka = tankadata.支払単価;
                            result.Kingaku = p重量 * result.Tanka;
                        }
                        else
                        {
                            result.Tanka = tankadata.支払単価 == null ? -1 : (decimal)tankadata.支払単価;
                            result.Kingaku = p重量 * p数量 * result.Tanka;
                        }
                    }
                    else
                    {
                        result.Tanka = 0;
                        result.Kingaku = 0;
                    }
                }

            }

            return;
        }

        /// <summary>
        /// 車種と地区による運賃計算
        /// </summary>
        /// <param name="result"></param>
        /// <param name="p請求支払区分"></param>
        /// <param name="p得意先ID"></param>
        /// <param name="p発地ID"></param>
        /// <param name="p着地ID"></param>
        /// <param name="p車種ID"></param>
        public void GetUnitCostCar(DLY01010_TANKA result, int p請求支払区分, int p得意先ID, int p発地ID, int p着地ID, int p車種ID, decimal p重量, decimal p数量 , int pflg)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                int tcode = (from k in context.M01_TOK
                             where k.得意先ID == p得意先ID
                             && k.削除日付 == null
                             select k.得意先KEY).FirstOrDefault();

                if (pflg == 0)
                {
                    var tankadata = (from r in context.M02_TTAN2
                                     where r.得意先KEY == tcode
                                        && r.車種ID == p車種ID
                                        && (r.発地ID == p発地ID || r.発地ID == 0)
                                        && (r.着地ID == p着地ID || r.着地ID == 0)
                                        && r.削除日付 == null
                                     orderby r.発地ID descending, r.着地ID descending
                                     select r).FirstOrDefault();

                    if (tankadata != null)
                    {
                        result.Tanka = tankadata.売上単価;
                        result.Kingaku = p重量 * p数量 * result.Tanka;
                    }
                }
                else
                {
                    var tankadata = (from r in context.M03_YTAN2
                                     where r.支払先KEY == tcode
                                        && r.車種ID == p車種ID
                                        && (r.発地ID == p発地ID || r.発地ID == 0)
                                        && (r.着地ID == p着地ID || r.着地ID == 0)
                                        && r.削除日付 == null
                                     orderby r.発地ID descending, r.着地ID descending
                                     select r).FirstOrDefault();

                    if (tankadata != null)
                    {
                        result.Tanka = tankadata.支払単価;
                        result.Kingaku = p重量 * p数量 * result.Tanka;
                    }
                }
            }

            return;
        }

        /// <summary>
        /// 個建運賃テーブルによる計算
        /// </summary>
        /// <param name="result"></param>
        /// <param name="p請求支払区分"></param>
        /// <param name="p得意先ID"></param>
        /// <param name="p着地ID"></param>
        /// <param name="p重量"></param>
        /// <param name="p数量"></param>
        public void GetUnitCostMatrix(DLY01010_TANKA result, int p請求支払区分, int p得意先ID, int p着地ID, decimal p重量, decimal p数量)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                int tcode = (from k in context.M01_TOK
                             where k.得意先ID == p得意先ID
                             && k.削除日付 == null
                             select k.得意先KEY).FirstOrDefault();

                p重量 *= 1000;
                var tankadata = (from r in context.M02_TTAN4
                                 where r.得意先KEY == tcode
                                    && r.重量 >= p重量
                                    && r.個数 >= p数量
                                    && (r.着地ID == p着地ID || r.着地ID == 0)
                                    && r.削除日付 == null
                                 orderby r.重量, r.個数, r.着地ID descending
                                 select r).FirstOrDefault();

                if (tankadata != null)
                {
                    result.Tanka = tankadata.個建金額 == 0 ? tankadata.個建単価 : 0;
                    result.Kingaku = (tankadata.個建単価 * (decimal)p数量) + tankadata.個建金額;
                }

            }

            return;
        }

        #endregion

    }
}