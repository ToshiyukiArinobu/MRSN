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
	/// 運転日報関連機能
	/// </summary>
	public class DLY02015
	{

		public class T01_TRN_DATA : T01_TRN_Member
		{
			public int? 行番号SUB { get; set; }
			public int 担当部門 { get; set; }
			public string 請求内訳名 { get; set; }
            //public DateTime? 実運行日開始 { get; set; }
            //public DateTime? 実運行日終了 { get; set; }
            //public decimal? 出庫時間 { get; set; }
            //public decimal? 帰庫時間 { get; set; }
            //public int 出勤区分ID { get; set; }
            //public decimal? 拘束時間 { get; set; }
            //public decimal? 運転時間 { get; set; }
            //public decimal? 高速時間 { get; set; }
            //public decimal? 作業時間 { get; set; }
			//public decimal? 待機時間 { get; set; }
            //public decimal? 休憩時間 { get; set; }
            //public decimal? 残業時間 { get; set; }
            //public decimal? 深夜時間 { get; set; }
            //public decimal? 輸送屯数 { get; set; }
            //public int? 出庫ＫＭ { get; set; }
            //public int? 帰庫ＫＭ { get; set; }
			public string 備考 { get; set; }
            //public DateTime? 勤務開始日 { get; set; }
            //public DateTime? 勤務終了日 { get; set; }
			public DateTime? 労務日 { get; set; }
            public int? 請求内訳管理区分 { get; set; }
            public string 車種名 { get; set; }
            public string 支払先名 { get; set; }

		}

        //public class T02_UTRN_DATA : T02_UTRN_Member
        //{
        //    public int? 得意先ID { get; set; }
        //    public int? 請求内訳ID { get; set; }
        //    public string 請求内訳名 { get; set; }
        //    public int? 支払先ID { get; set; }
        //    public string 支払先名２次 { get; set; }
        //    public string 乗務員連絡先 { get; set; }
        //    public int? 請求運賃計算区分ID { get; set; }
        //    public int? 支払運賃計算区分ID { get; set; }
        //    public decimal? 数量 { get; set; }
        //    public string 単位 { get; set; }
        //    public decimal? 重量 { get; set; }
        //    public decimal? 売上単価 { get; set; }
        //    public int? 売上金額 { get; set; }
        //    public int? 通行料 { get; set; }
        //    public int? 請求割増１ { get; set; }
        //    public int? 請求割増２ { get; set; }
        //    public int? 請求消費税 { get; set; }
        //    public decimal? 支払単価 { get; set; }
        //    public int? 支払金額 { get; set; }
        //    public int? 支払通行料 { get; set; }
        //    public int? 支払割増１ { get; set; }
        //    public int? 支払割増２ { get; set; }
        //    public int? 支払消費税 { get; set; }
        //    public int? 水揚金額 { get; set; }
        //    public int? 社内区分 { get; set; }
        //    public int? 請求税区分 { get; set; }
        //    public int? 支払税区分 { get; set; }
        //    public int? 売上未定区分 { get; set; }
        //    public int? 支払未定区分 { get; set; }
        //    public int? 商品ID { get; set; }
        //    public string 商品名 { get; set; }
        //    public int? 発地ID { get; set; }
        //    public string 発地名 { get; set; }
        //    public int? 着地ID { get; set; }
        //    public string 着地名 { get; set; }
        //    public int? 請求摘要ID { get; set; }
        //    public string 請求摘要 { get; set; }
        //    public int? 社内備考ID { get; set; }
        //    public string 社内備考 { get; set; }

        //    public string 得意先名 { get; set; }
        //    public string 乗務員名 { get; set; }
        //    public string 経費名称 { get; set; }
        //}


		public class T03_SHR_NAME
		{
			public int row { get; set; }
			public int 支払先ID { get; set; }
			public string 支払先名 { get; set; }
		}

		public class T03_TEKIYO_NAME
		{
			public int row { get; set; }
			public int 摘要ID { get; set; }
			public string 摘要名 { get; set; }
		}

		public class T03_MEISAI_NO
		{
			public int? 明細番号 { get; set; }
			public int? 明細行 { get; set; }
		}


		#region 運転日報（基本データ）
		/// <summary>
		/// T01_TRN+T02_UTRNのリスト取得
		/// </summary>
		/// <param name="p明細番号">明細番号</param>
		/// <param name="p明細行">明細行</param>
		/// <returns>T01_TRN_MemberのList</returns>
		public List<T01_TRN_DATA> GetListDrive(int? p明細番号, int? p明細行)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

				// 注意：T02が駆動表（明細行は１件のみ）
				var ret = (from t01 in context.T01_TRN.Where(x => x.入力区分 == 2)
						   from uwk in context.M10_UHK.Where(x => x.得意先KEY == t01.得意先KEY && x.請求内訳ID == t01.請求内訳ID && x.削除日付 == null).DefaultIfEmpty()
						   from drv in context.M04_DRV.Where(x => x.乗務員KEY == t01.乗務員KEY).DefaultIfEmpty()
						   from tok in context.M01_TOK.Where(x => x.得意先KEY == t01.得意先KEY).DefaultIfEmpty()
						   from shr in context.M01_TOK.Where(x => x.得意先KEY == t01.支払先KEY).DefaultIfEmpty()
						   from sya in context.M06_SYA.Where(x => x.車種ID == t01.車種ID).DefaultIfEmpty()
						   from car in context.M05_CAR.Where(x => x.車輌KEY == t01.車輌KEY).DefaultIfEmpty()
						   where t01.明細番号 == p明細番号 && t01.明細行 == p明細行
						   orderby t01.明細行
						   select new T01_TRN_DATA
						   {
							   明細番号 = t01.明細番号,
							   明細行 = (t01.明細行 == null) ? 0 : (int)t01.明細行,
							   行番号SUB = (t01.明細行 == null) ? (int?)null : t01.明細行,
							   登録日時 = t01.登録日時,
							   更新日時 = t01.更新日時,
							   明細区分 = t01.明細区分,
							   入力区分 = t01.入力区分,
							   請求日付 = t01.請求日付,
							   支払日付 = t01.支払日付,
							   配送日付 = t01.配送日付,
							   配送時間 = t01.配送時間,
							   得意先ID = tok.得意先ID,
							   請求内訳ID = (t01.請求内訳ID == null || t01.請求内訳ID == 0) ? null : t01.請求内訳ID,
							   請求内訳管理区分 = tok.請求内訳管理区分,
							   請求内訳名 = uwk.請求内訳名,
							   車輌ID = car.車輌ID,
                               車種ID = t01.車種ID,
                               車種名 = sya.車種名,
                               支払先ID = shr.得意先ID,
                               支払先名 = shr.略称名,
                               乗務員ID = drv.乗務員ID,
                               乗務員名 = drv.乗務員名,
							   担当部門 = (t01.自社部門ID == null || (int)t01.自社部門ID == 0) ? 0 : (int)t01.自社部門ID,
							   自社部門ID = (t01.自社部門ID == null || (int)t01.自社部門ID == 0) ? 0 : (int)t01.自社部門ID,
							   車輌番号 = t01.車輌番号,
							   支払先名２次 = t01.支払先名２次,
							   実運送乗務員 = t01.実運送乗務員,
							   乗務員連絡先 = t01.乗務員連絡先,
							   請求運賃計算区分ID = t01.請求運賃計算区分ID,
							   支払運賃計算区分ID = t01.支払運賃計算区分ID,
							   数量 = t01.数量,
							   単位 = t01.単位,
							   重量 = t01.重量,
							   走行ＫＭ = t01.走行ＫＭ,
							   実車ＫＭ = t01.実車ＫＭ,
							   待機時間 = t01.待機時間,
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
							   水揚金額 = t01.水揚金額,
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
							   請求摘要ID = t01.請求摘要ID,
							   請求摘要 = t01.請求摘要,
							   社内備考ID = t01.社内備考ID,
							   社内備考 = t01.社内備考,
							   入力者ID = t01.入力者ID,
                               確認名称区分 = t01.確認名称区分,
                               
							   // -----
                               得意先名 = tok.略称名,
						   }).ToList();
				if (p明細行 == null)
				{
					return ret.ToList();
				}
				else
				{
					return ret.Where(x => x.明細行 == p明細行).ToList();
				}
			}
		}


		public List<T03_MEISAI_NO> GetMeisaiNo(int pMeisaiNo, int pMeisaiGyo, int vector)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

                var ret = (from t01 in context.T01_TRN
                           where t01.入力区分 == 2
                           select t01.明細区分);

                 //データが1件もない状態で<< < > >>を押された時の処理
                if (pMeisaiNo == 0 && ret.Count() == 0)
                {
                    return null;
                }


				int? No = 0;
				int? Gyou = 0;
				switch (vector)
				{
				case 0:		// 最小値
					No = (from t01 in context.T01_TRN
                          where t01.入力区分 == 2
						  select t01.明細番号).Min();
					Gyou = (from t01 in context.T01_TRN
                          where t01.入力区分 == 2 && t01.明細番号 == No
						  select t01.明細行).Min();
					break;
				case 1:		// ひとつ前
					No = (from t01 in context.T01_TRN
						  where t01.入力区分 == 2 && (t01.明細番号 < pMeisaiNo || (t01.明細行 < pMeisaiGyo && t01.明細番号 == pMeisaiNo))
						  select t01.明細番号).DefaultIfEmpty().Max();
					Gyou = (from t01 in context.T01_TRN
                          where t01.入力区分 == 2 && t01.明細番号 == No
						  select t01.明細行).DefaultIfEmpty().Max();
					break;
				case 2:		// ひとつ後
					No = (from t01 in context.T01_TRN
						  where t01.入力区分 == 2 && t01.明細番号 > pMeisaiNo || (t01.明細行 > pMeisaiGyo && t01.明細番号 == pMeisaiNo)
						  select t01.明細番号).DefaultIfEmpty().Min();
					Gyou = (from t01 in context.T01_TRN
                          where t01.入力区分 == 2 && t01.明細番号 == No
						  select t01.明細行).DefaultIfEmpty().Max();
					break;
				default:	// 最大
					No = (from t01 in context.T01_TRN
                          where t01.入力区分 == 2
                          select t01.明細番号).Max();
					Gyou = (from t01 in context.T01_TRN
                          where t01.入力区分 == 2 && t01.明細番号 == No
						  select t01.明細行).Max();
					break;
				}

				var newrow = new T03_MEISAI_NO();
				newrow.明細番号 = No;
				newrow.明細行 = Gyou;

				List<T03_MEISAI_NO> result = new List<T03_MEISAI_NO>();
				result.Add(newrow);
				return result;
			}
		}

		#endregion


        /// <summary>
        /// 現在の登録件数取得
        /// </summary>
        /// <returns></returns>
        public int GetMaxMeisaiCount()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                int query = (from t01 in context.T01_TRN where t01.入力区分 == 2 select t01.明細番号).Distinct().Count();
                return query;
            }
        }


		public int PutAllData(int p明細番号, int p明細行, List<T01_TRN_DATA> t01list, int 担当者ID , int p確認名称区分 )
		{
			int count = 0;

			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

				using (var tran = new TransactionScope())
				{
                    if (p明細番号 >= 0)
                    {
                        var rect01 = from x in context.T01_TRN
                                     where x.明細番号 == p明細番号 && x.明細行 == p明細行
                                     select x;
                        foreach (var rec in rect01)
                        {
                            context.DeleteObject(rec);
                        }
                    }
                    else
                    {
                        // 新番号取得
                        var 明細番号M = (from n in context.M88_SEQ
                                     where n.明細番号ID == 1
                                     select n
                                        ).FirstOrDefault();
                        if (明細番号M == null)
                        {
                            return -1;
                        }
                        p明細番号 = 明細番号M.現在明細番号 + 1;
                        明細番号M.現在明細番号 = p明細番号;
                        明細番号M.AcceptChanges();
						p明細行 = 1;
                    }

					// T01

					var t01trn = t01list[0];

                    if (t01trn.明細行 != 0 && t01trn.得意先ID != null)
					{
						var t01 = new T01_TRN();

						var tokkey = (from t in context.M01_TOK where t.得意先ID == t01trn.得意先ID select (int?)t.得意先KEY).FirstOrDefault();
						var shrkey = (from t in context.M01_TOK where t.得意先ID == t01trn.支払先ID select (int?)t.得意先KEY).FirstOrDefault();
						var drvkey = (from t in context.M04_DRV where t.乗務員ID == t01trn.乗務員ID select (int?)t.乗務員KEY).FirstOrDefault();
						var carkey = (from t in context.M05_CAR where t.車輌ID == t01trn.車輌ID select (int?)t.車輌KEY).FirstOrDefault();

						t01.明細番号 = p明細番号;
						t01.明細行 = t01trn.明細行;
                        t01.更新日時 = DateTime.Now;
                        t01.登録日時 = t01trn.登録日時 == null ? DateTime.Now : t01trn.登録日時;
                        t01.明細区分 = 1;
						t01.入力区分 = 2;
						t01.請求日付 = (DateTime)t01trn.請求日付;
						t01.支払日付 = t01trn.支払日付;
						t01.配送日付 = (DateTime)(t01trn.配送日付==null?t01trn.請求日付:(DateTime)t01trn.配送日付);
						t01.配送時間 = t01trn.配送時間;
						t01.得意先KEY = tokkey;
						t01.請求内訳ID = t01trn.請求内訳ID;
						t01.車輌KEY = carkey;
						t01.車種ID = t01trn.車種ID;
						t01.支払先KEY = shrkey;
						t01.乗務員KEY = drvkey;
                        t01.自社部門ID = t01trn.自社部門ID;
						t01.車輌番号 = t01trn.車輌番号;
						t01.支払先名２次 = t01trn.支払先名２次;
						t01.実運送乗務員 = t01trn.実運送乗務員;
						t01.乗務員連絡先 = t01trn.乗務員連絡先;
                        t01.請求運賃計算区分ID = t01trn.請求運賃計算区分ID == null ? -1 : (int)t01trn.請求運賃計算区分ID;
                        t01.支払運賃計算区分ID = t01trn.支払運賃計算区分ID == null ? -1 : (int)t01trn.支払運賃計算区分ID;
						t01.数量 = (decimal)(t01trn.数量 == null ? 0 : t01trn.数量);
						t01.単位 = t01trn.単位;
						t01.重量 = (decimal)(t01trn.重量 == null ? 0 : t01trn.重量);
						t01.走行ＫＭ = (int)(t01trn.走行ＫＭ==null?0:t01trn.走行ＫＭ);
						t01.実車ＫＭ = (int)(t01trn.実車ＫＭ==null?0:t01trn.実車ＫＭ);
						t01.待機時間 = (decimal)(t01trn.待機時間 == null ? 0 : t01trn.待機時間);
						t01.売上単価 = (decimal)(t01trn.売上単価==null?0:t01trn.売上単価);
						t01.売上金額 = (int)(t01trn.売上金額 == null ? 0 : t01trn.売上金額);
						t01.通行料 = (int)(t01trn.通行料 == null ? 0 : t01trn.通行料);
						t01.請求割増１ = (int)(t01trn.請求割増１ == null ? 0 : t01trn.請求割増１);
						t01.請求割増２ = (int)(t01trn.請求割増２==null?0:t01trn.請求割増２);
						t01.支払単価 = (decimal)(t01trn.支払単価 == null ? 0 : t01trn.支払単価);
						t01.支払金額 = (int)(t01trn.支払金額 == null ? 0 : t01trn.支払金額);
						t01.支払通行料 = (int)(t01trn.支払通行料 == null ? 0 : t01trn.支払通行料);
						t01.社内区分 = (int)(t01trn.社内区分 == null ? 1 : t01trn.社内区分);
						t01.請求税区分 = (int)(t01trn.請求税区分 == null ? 0 : t01trn.請求税区分);
						t01.支払税区分 = (int)(t01trn.支払税区分==null?0:t01trn.支払税区分);
						t01.売上未定区分 = (int)(t01trn.売上未定区分==null?0:t01trn.売上未定区分);
						t01.支払未定区分 = (int)(t01trn.支払未定区分 == null ? 0 : t01trn.支払未定区分);
						t01.商品ID = t01trn.商品ID;
						t01.商品名 = t01trn.商品名;
						t01.発地ID = t01trn.発地ID;
						t01.発地名 = t01trn.発地名;
						t01.着地ID = t01trn.着地ID;
						t01.着地名 = t01trn.着地名;
						t01.請求摘要ID = t01trn.請求摘要ID;
						t01.請求摘要 = t01trn.請求摘要;
						t01.請求消費税 = (int)(t01trn.請求消費税 == null ? 0 : t01trn.請求消費税);
						t01.支払割増１ = (int)(t01trn.支払割増１ == null ? 0 : t01trn.支払割増１);
						t01.支払割増２ = (int)(t01trn.支払割増２ == null ? 0 : t01trn.支払割増２);
						t01.支払消費税 = (int)(t01trn.支払消費税 == null ? 0 : t01trn.支払消費税);
						t01.水揚金額 = (int)(t01trn.水揚金額 == null ? 0 : t01trn.水揚金額);
						t01.社内備考ID = t01trn.社内備考ID;
						t01.社内備考 = t01trn.社内備考;
                        t01.入力者ID = 担当者ID;
                        t01.確認名称区分 = p確認名称区分;

						context.T01_TRN.ApplyChanges(t01);
					}

					context.SaveChanges();

					tran.Complete();
				}
			}

            return p明細番号;
		}

		/// <summary>
		/// 売上関連明細データ削除
		/// </summary>
		/// <param name="p明細番号"></param>
		/// <returns></returns>
		public int DeleteAllData(int p明細番号)
		{
			int count = 0;

			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();
				using (var tran = new TransactionScope())
				{
					var rect01 = from x in context.T01_TRN
								 where x.明細番号 == p明細番号 && x.入力区分 == 2
								 select x;
					foreach (var rec in rect01)
					{
						context.DeleteObject(rec);
					}

					context.SaveChanges();

					tran.Complete();
				}
			}

			return count;
		}


        #region 単価・金額計算用関数

        public class DLY01010_TANKA
        {
            public int Kubun { get; set; }
            public decimal? Tanka { get; set; }
            public decimal? Kingaku { get; set; }
        }

        public List<DLY01010_TANKA> GetUnitCost(int p計算区分, int p請求支払区分, int p得意先ID, int p発地ID, int p着地ID, int p商品ID, int p車種ID, int p走行ＫＭ, decimal p重量, decimal p数量)
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
            result.Kubun = p請求支払区分;
            result.Tanka = -1;
            result.Kingaku = -1;
            switch (p計算区分)
            {
                case 3:
                    GetUnitCostTariff(result, p請求支払区分, p得意先ID, p重量, p走行ＫＭ);
                    break;
                case 4:
                    GetUnitCostArea(result, p請求支払区分, p得意先ID, p発地ID, p着地ID, p商品ID, p重量, p数量);
                    break;
                case 5:
                    GetUnitCostCar(result, p請求支払区分, p得意先ID, p発地ID, p着地ID, p車種ID, p重量, p数量);
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
        public void GetUnitCostTariff(DLY01010_TANKA result, int p請求支払区分, int p得意先ID, decimal p重量, int p走行ＫＭ)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                p重量 *= 1000;
                decimal? kingaku = null; 
                context.Connection.Open();


                if (p請求支払区分 == 0)
                {
                    int tcode = (from k in context.M01_TOK
                                 where k.得意先ID == p得意先ID
                                 && k.削除日付 == null
                                 select k.Ｔ路線計算年度).FirstOrDefault();

                    kingaku = (from r in context.M50_RTBL
                               where r.タリフコード == tcode
                                  && r.距離 >= p走行ＫＭ
                                  && r.重量 >= p重量
                                  && r.削除日付 == null
                               orderby r.距離, r.重量
                               select r.運賃).FirstOrDefault();
                }
                if (p請求支払区分 == 1)
                {
                    int tcode = (from k in context.M01_TOK
                                 where k.得意先ID == p得意先ID
                                 && k.削除日付 == null
                                 select k.Ｓ路線計算年度).FirstOrDefault();

                    kingaku = (from r in context.M50_RTBL
                               where r.タリフコード == tcode
                                  && r.距離 >= p走行ＫＭ
                                  && r.重量 >= p重量
                                  && r.削除日付 == null
                               orderby r.距離, r.重量
                               select r.運賃).FirstOrDefault();
                }


                if (kingaku != null)
                {
                    result.Kingaku = (decimal)kingaku;
                }
                else
                {
                    result.Kingaku = 0;
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
        public void GetUnitCostArea(DLY01010_TANKA result, int p請求支払区分, int p得意先ID, int p発地ID, int p着地ID, int p商品ID, decimal p重量, decimal p数量)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                int tcode = (from k in context.M01_TOK
                             where k.得意先ID == p得意先ID
                             && k.削除日付 == null
                             select k.得意先KEY).FirstOrDefault();

                if (p請求支払区分 == 0)
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
        public void GetUnitCostCar(DLY01010_TANKA result, int p請求支払区分, int p得意先ID, int p発地ID, int p着地ID, int p車種ID, decimal p重量, decimal p数量)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                int tcode = (from k in context.M01_TOK
                             where k.得意先ID == p得意先ID
                             && k.削除日付 == null
                             select k.得意先KEY).FirstOrDefault();

                if (p請求支払区分 == 0)
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
                    else
                    {
                        result.Tanka = 0;
                        result.Kingaku = 0;
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

                if (p請求支払区分 == 0)
                {
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
                    else
                    {
                        result.Tanka = 0;
                        result.Kingaku = 0;
                    }
                }
                else
                {
                    p重量 *= 1000;
                    var tankadata = (from r in context.M03_YTAN4
                                     where r.支払先KEY == tcode
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
                    else
                    {
                        result.Tanka = 0;
                        result.Kingaku = 0;
                    }
                }
            }

            return;
        }

        #endregion


		#region 売上履歴取得

		public class DLY02015_RIREKI
		{
			public DateTime 請求日付 { get; set; }
			public string 得意先名 { get; set; }
			public string 発地名 { get; set; }
			public string 着地名 { get; set; }
			public string 商品名 { get; set; }
			public decimal 数量 { get; set; }
			public decimal 重量 { get; set; }
			public string 車番 { get; set; }
			public string 乗務員 { get; set; }
			public string 支払先名 { get; set; }
			public int 売上金額 { get; set; }
			public int 通行料 { get; set; }
			public int 支払金額 { get; set; }
			public int 支払通行料 { get; set; }
			public int 明細番号 { get; set; }
			public int 行 { get; set; }

		}

		public List<DLY02015_RIREKI> TRN_RIREKI(int? tantoID, int? tokID, int? 表示順)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

				var ret = (from t01 in context.T01_TRN.Where(c => ((c.入力区分 == 2 )) && (tantoID == null || tantoID == c.入力者ID))
						   join m01t in context.M01_TOK.Where(c => c.削除日付 == null) on t01.得意先KEY equals m01t.得意先KEY into m01tGroup
						   from m01tg in m01tGroup.DefaultIfEmpty()
						   join m01y in context.M01_TOK.Where(c => c.削除日付 == null) on t01.支払先KEY equals m01y.得意先KEY into m01yGroup
						   from m01yg in m01yGroup.DefaultIfEmpty()
						   join m04 in context.M04_DRV.Where(c => c.削除日付 == null) on t01.乗務員KEY equals m04.乗務員KEY into m04Group
						   from m04g in m04Group.DefaultIfEmpty()
						   orderby t01.明細番号 descending, t01.明細行 descending
						   where (tokID == null || tokID == m01tg.得意先ID)
						   select new DLY02015_RIREKI
						   {
							   請求日付 = t01.請求日付,
							   得意先名 = m01tg.略称名,
							   発地名 = t01.発地名,
							   着地名 = t01.着地名,
							   商品名 = t01.商品名,
							   数量 = t01.数量,
							   重量 = t01.重量,
							   車番 = t01.車輌番号,
							   乗務員 = m04g.乗務員名,
							   支払先名 = m01yg.略称名,
							   売上金額 = t01.売上金額,
							   通行料 = t01.通行料,
							   支払金額 = t01.支払金額,
							   支払通行料 = t01.支払通行料,
							   明細番号 = t01.明細番号,
							   行 = t01.明細行,

						   }).AsQueryable();

				//データが1件もない状態で<< < > >>を押された時の処理
				if (ret.Count() == 0)
				{
					return null;
				}

				if (表示順 > 0)
				{
					ret = ret.OrderByDescending(c => c.請求日付).ThenByDescending(c => c.明細番号).ThenBy(c => c.行);
				}
				else
				{
					ret = ret.OrderByDescending(c => c.明細番号).ThenBy(c => c.行);
				}

				ret = ret.Take(500);

				List<DLY02015_RIREKI> ret2 = ret.ToList();

				return ret2;
			}
		}
		#endregion
    }
}