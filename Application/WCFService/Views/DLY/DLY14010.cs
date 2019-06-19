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
    /// 経費伝票問い合わせ用データメンバー
    /// </summary>
    [DataContract]
    public class DLY14010_Member
    {
        [DataMember]
        public int 明細番号 { get; set; }
        [DataMember]
        public int 明細行 { get; set; }
        [DataMember]
        public int? 明細区分 { get; set; }
        [DataMember]
        public DateTime? 日付 { get; set; }
        [DataMember]
        public string 経費先名 { get; set; }
        [DataMember]
        public int? 車輌ID { get; set; }
        [DataMember]
        public string 車輌番号 { get; set; }
        [DataMember]
        public string 乗務員名 { get; set; }
        [DataMember]
        public int? メーター { get; set; }
        [DataMember]
        public int? 乗務員ID { get; set; }
        [DataMember]
        public int? 支払先ID { get; set; }
        [DataMember]
        public int? 自社部門ID { get; set; }
        [DataMember]
        public int? 経費項目ID { get; set; }
        [DataMember]
        public string 経費項目 { get; set; }
        [DataMember]
        public string 経費補助名称 { get; set; }
        [DataMember]
        public decimal 単価 { get; set; }
        [DataMember]
        public decimal? 内軽油税分 { get; set; }
        [DataMember]
        public decimal d内軽油税分 { get; set; }
        [DataMember]
        public decimal? 数量 { get; set; }
        [DataMember]
        public decimal d数量 { get; set; }
        [DataMember]
        public decimal? 金額 { get; set; }
        [DataMember]
        public decimal d金額 { get; set; }
        [DataMember]
        public decimal? 収支区分 { get; set; }
        [DataMember]
        public decimal? 摘要ID { get; set; }
        [DataMember]
        public string 摘要名 { get; set; }
        [DataMember]
        public decimal? 入力者ID { get; set; }
        [DataMember]
        public DateTime? 日付From { get; set; }
        [DataMember]
        public DateTime? 日付To { get; set; }
        [DataMember]
        public Int32? 経費先指定ID { get; set; }
        [DataMember]
        public string 経費先指定名 { get; set; }
        [DataMember]
        public string 経費項目指定 { get; set; }
        [DataMember]
        public string 入力区分 { get; set; }
    }



    /// <summary>
    /// 経費伝票問い合わせ用データメンバー
    /// </summary>
    [DataContract]
    public class DLY14010_Member_CSV
    {
        [DataMember]
        public DateTime? 日付 { get; set; }
        [DataMember]
        public int? 経費先ID { get; set; }
        [DataMember]
        public string 経費先名 { get; set; }
        [DataMember]
        public string 車輌番号 { get; set; }
        [DataMember]
        public int? メーター { get; set; }
        [DataMember]
        public string 乗務員名 { get; set; }
        [DataMember]
        public int? 経費項目ID { get; set; }
        [DataMember]
        public string 経費項目 { get; set; }
        [DataMember]
        public string 経費補助名称 { get; set; }
        [DataMember]
        public decimal? 単価 { get; set; }
        [DataMember]
        public decimal? 内軽油税分 { get; set; }
        [DataMember]
        public decimal? 数量 { get; set; }
        [DataMember]
        public decimal? 金額 { get; set; }
        [DataMember]
        public decimal? 収支区分 { get; set; }
        [DataMember]
        public string 摘要名 { get; set; }
        [DataMember]
        public int 明細番号 { get; set; }
        [DataMember]
        public int 明細行 { get; set; }
    }

	public class DLY14010
	{
		const string SelectedChar = "a";
		const string UnselectedChar = "";

		public class MasterList_Member
		{
			public string 選択 { get; set; }
			public int コード { get; set; }
			public string 表示名 { get; set; }
		}



		public class DLY14010_TOTAL_Member
		{
			public decimal 数量合計 { get; set; }
			public decimal 金額合計 { get; set; }
		}
		public class DLY14010_DATASET
		{
			public List<DLY14010_Member> DataList = new List<DLY14010_Member>();
			public List<DLY14010_TOTAL_Member> TotalList = new List<DLY14010_TOTAL_Member>();
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
		/// <returns>DLY14010_Memberのリスト</returns>
        public DLY14010_DATASET GetListDLY14010(int? p担当者ID, int? p乗務員ID, int? p車輌ID,
			    int? p得意先ID, string s経費先名, int? p支払先ID, int? p請求内訳ID
			    , DateTime? p検索日付From, DateTime? p検索日付To, int p検索日付区分
			    , int? p経費項目ID, int? p売上未定区分
			    , string p商品名, string p発地名, string p着地名, string p請求摘要, string p社内備考, string s経費項目指定, string p経費補助名称, string p摘要
			    )
		    {

			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();
				DLY14010_DATASET result = new DLY14010_DATASET();

                var query = (from t03 in context.T03_KTRN
                             join m01 in context.M01_TOK.Where(x => x.削除日付 == null) on t03.支払先KEY equals m01.得意先KEY into m01Group
                             join m04 in context.M04_DRV.Where(x => x.削除日付 == null) on t03.乗務員KEY equals m04.乗務員KEY into m04Group
                             join m07 in context.M07_KEI.Where(x => x.削除日付 == null) on t03.経費項目ID equals m07.経費項目ID into m07Group
                             join m05 in context.M05_CAR.Where(x => x.削除日付 == null) on t03.車輌ID equals m05.車輌KEY into m05Group
							 where (p担当者ID == null || t03.入力者ID == p担当者ID) && (p経費補助名称 == "" || t03.経費補助名称.Contains(p経費補助名称))
									&& (p摘要 == "" || t03.摘要名.Contains(p摘要))
                             select new DLY14010_Member
                             {
                                 明細番号 = t03.明細番号,
                                 明細行 = t03.明細行,
                                 明細区分 = t03.明細区分,
                                 日付  = t03.経費発生日,
                                 車輌ID = m05Group.FirstOrDefault().車輌ID,
                                 車輌番号 = t03.車輌番号,
                                 メーター = t03.メーター,
                                 乗務員ID = m04Group.FirstOrDefault().乗務員ID,
                                 支払先ID = m01Group.FirstOrDefault().得意先ID,
                                 自社部門ID = t03.自社部門ID,
                                 経費項目ID = t03.経費項目ID,
                                 経費補助名称 = t03.経費補助名称,
                                 単価 = t03.単価,
                                 内軽油税分 = t03.内軽油税分,
                                 d内軽油税分 = t03.内軽油税分 == null ? 0 : (decimal)t03.内軽油税分,
                                 数量 = t03.数量,
                                 d数量 = t03.数量 == null ? 0 : (decimal)t03.数量,
                                 金額 = t03.金額,
                                 d金額 = t03.金額 == null ? 0 : (decimal)t03.金額,
                                 収支区分 = t03.収支区分,
                                 摘要ID = t03.摘要ID,
                                 摘要名 = t03.摘要名,
                                 入力者ID = t03.入力者ID,
                                 経費先名 = m01Group.FirstOrDefault().略称名,
                                 乗務員名 = m04Group.FirstOrDefault().乗務員名,
                                 経費項目 = m07Group.FirstOrDefault().経費項目名,
                                 入力区分 = t03.入力区分 == 0 ? "経費伝票" : "運転日報",
                                 日付From = p検索日付From,
                                 日付To = p検索日付To,
                                 経費先指定ID = p得意先ID,
                                 経費先指定名 = s経費先名,
                                 経費項目指定 = s経費項目指定,
                                 

                             }).AsQueryable();

                if (p乗務員ID != null && p乗務員ID != 0)
                {
                    query = query.Where(x => x.乗務員ID == p乗務員ID);
                }
                if (p車輌ID != null && p車輌ID != 0)
                {
                    query = query.Where(x => x.車輌ID == p車輌ID);
                }

                if (p検索日付From != null)
                {
                    query = query.Where(x => x.日付 >= p検索日付From);
                }
                if (p検索日付To != null)
                {
                    query = query.Where(x => x.日付 <= p検索日付To);
                }

                if (p得意先ID != null)
                {
                    query = query.Where(x => x.支払先ID == p得意先ID);
                }

                if (p経費項目ID != null && p経費項目ID != 0)
                {
                    query = query.Where(x => x.経費項目ID == p経費項目ID);
                }

				List<DLY14010_Member> ret;

				ret = query.ToList();

				result.DataList = ret;
				result.TotalList.Add(new DLY14010_TOTAL_Member());
				foreach (var rec in result.DataList)
				{
                    result.TotalList[0].数量合計 += (decimal)rec.d数量;
                    result.TotalList[0].金額合計 += (decimal)rec.d金額;
				}

				return result;
			}
		}

        /// <summary>
        /// 売上明細問い合わせリスト取得
        /// </summary>
        /// <returns>DLY14010_Memberの印刷</returns>
        public List<DLY14010_Member> GetListDLY14010_PRT(
                int? p得意先ID, string s経費先名, int? p支払先ID, int? p請求内訳ID
                , DateTime? p検索日付From, DateTime? p検索日付To, int p検索日付区分
                , int? p経費項目ID, int? p売上未定区分
                , string p商品名, string p発地名, string p着地名, string p請求摘要, string p社内備考, string s経費項目指定
                )
        {

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                DLY14010_DATASET result = new DLY14010_DATASET();

                var query = (from t03 in context.T03_KTRN
                             join m01 in context.M01_TOK.Where(x => x.削除日付 == null) on t03.支払先KEY equals m01.得意先KEY into m01Group
                             join m04 in context.M04_DRV.Where(x => x.削除日付 == null) on t03.乗務員KEY equals m04.乗務員KEY into m04Group
                             join m07 in context.M07_KEI.Where(x => x.削除日付 == null) on t03.経費項目ID equals m07.経費項目ID into m07Group
                             join m05 in context.M05_CAR.Where(x => x.削除日付 == null) on t03.車輌ID equals m05.車輌KEY into m05Group
                             select new DLY14010_Member
                             {
                                 明細番号 = t03.明細番号,
                                 明細行 = t03.明細行,
                                 明細区分 = t03.明細区分,
                                 日付 = t03.経費発生日,
                                 車輌ID = m05Group.FirstOrDefault().車輌ID,
                                 車輌番号 = t03.車輌番号,
                                 メーター = t03.メーター,
                                 乗務員ID = m04Group.FirstOrDefault().乗務員ID,
                                 支払先ID = m01Group.FirstOrDefault().得意先ID,
                                 自社部門ID = t03.自社部門ID,
                                 経費項目ID = t03.経費項目ID,
                                 経費補助名称 = t03.経費補助名称,
                                 単価 = t03.単価,
                                 内軽油税分 = t03.内軽油税分,
                                 数量 = t03.数量,
                                 金額 = t03.金額,
                                 収支区分 = t03.収支区分,
                                 摘要ID = t03.摘要ID,
                                 摘要名 = t03.摘要名,
                                 入力者ID = t03.入力者ID,
                                 経費先名 = m01Group.FirstOrDefault().略称名,
                                 乗務員名 = m04Group.FirstOrDefault().乗務員名,
                                 経費項目 = m07Group.FirstOrDefault().経費項目名,

                                 日付From = p検索日付From,
                                 日付To = p検索日付To,
                                 経費先指定ID = p得意先ID,
                                 経費先指定名 = s経費先名,
                                 経費項目指定 = s経費項目指定,

                             }).AsQueryable();


                if (p検索日付From != null)
                {
                    query = query.Where(x => x.日付 >= p検索日付From);
                }
                if (p検索日付To != null)
                {
                    query = query.Where(x => x.日付 <= p検索日付To);
                }

                if (p得意先ID != null)
                {
                    query = query.Where(x => x.支払先ID == p得意先ID);
                }

                if (p経費項目ID != null && p経費項目ID != 0)
                {
                    query = query.Where(x => x.経費項目ID == p経費項目ID);
                }

                List<DLY14010_Member> ret;

                ret = query.ToList();

                return ret;

                //result.DataList = ret;
                //result.TotalList.Add(new DLY14010_TOTAL_Member());
                //foreach (var rec in result.DataList)
                //{
                //    result.TotalList[0].数量合計 += (decimal)rec.数量;
                //    result.TotalList[0].金額合計 += (decimal)rec.金額;
                //}

                //return result;
            }
        }

        /// <summary>
        /// 売上明細問い合わせリスト取得
        /// </summary>
        /// <returns>DLY14010_Memberの印刷</returns>
        public List<DLY14010_Member_CSV> GetListDLY14010_CSV(
				int? p担当者ID, int? p乗務員ID, int? p車輌ID,
			    int? p得意先ID, string s経費先名, int? p支払先ID, int? p請求内訳ID
			    , DateTime? p検索日付From, DateTime? p検索日付To, int p検索日付区分
			    , int? p経費項目ID, int? p売上未定区分
			    , string p商品名, string p発地名, string p着地名, string p請求摘要, string p社内備考, string s経費項目指定, string p経費補助名称, string p摘要
                )
        {

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                DLY14010_DATASET result = new DLY14010_DATASET();

				var query = (from t03 in context.T03_KTRN
							 join m01 in context.M01_TOK.Where(x => x.削除日付 == null) on t03.支払先KEY equals m01.得意先KEY into m01Group
							 join m04 in context.M04_DRV.Where(x => x.削除日付 == null) on t03.乗務員KEY equals m04.乗務員KEY into m04Group
							 join m07 in context.M07_KEI.Where(x => x.削除日付 == null) on t03.経費項目ID equals m07.経費項目ID into m07Group
							 join m05 in context.M05_CAR.Where(x => x.削除日付 == null) on t03.車輌ID equals m05.車輌KEY into m05Group
							 where (p担当者ID == null || t03.入力者ID == p担当者ID) && (p経費補助名称 == "" || t03.経費補助名称.Contains(p経費補助名称))
									&& (p摘要 == "" || t03.摘要名.Contains(p摘要)) && (p乗務員ID == null || m04Group.FirstOrDefault().乗務員ID == p乗務員ID)
									&& (p車輌ID == null || m05Group.FirstOrDefault().車輌ID == p車輌ID)
                             select new DLY14010_Member_CSV
                             {
                                 明細番号 = t03.明細番号,
                                 明細行 = t03.明細行,
                                 日付 = t03.経費発生日,
                                 車輌番号 = t03.車輌番号,
                                 メーター = t03.メーター,
                                 経費補助名称 = t03.経費補助名称,
                                 単価 = t03.単価,
                                 内軽油税分 = t03.内軽油税分,
                                 数量 = t03.数量,
                                 金額 = t03.金額,
                                 収支区分 = t03.収支区分,
                                 摘要名 = t03.摘要名,
                                 経費先名 = m01Group.FirstOrDefault().略称名,
                                 乗務員名 = m04Group.FirstOrDefault().乗務員名,
                                 経費項目 = m07Group.FirstOrDefault().経費項目名,
                                 経費先ID = m01Group.FirstOrDefault().得意先ID,
                                 経費項目ID = t03.経費項目ID,
                             }).AsQueryable();


                if (p検索日付From != null)
                {
                    query = query.Where(x => x.日付 >= p検索日付From);
                }
                if (p検索日付To != null)
                {
                    query = query.Where(x => x.日付 <= p検索日付To);
                }

                if (p得意先ID != null)
                {
                    query = query.Where(x => x.経費先ID == p得意先ID);
                }

                if (p経費項目ID != null && p経費項目ID != 0)
                {
                    query = query.Where(x => x.経費項目ID == p経費項目ID);
                }

                List<DLY14010_Member_CSV> ret;

                ret = query.ToList();

                return ret;

                //result.DataList = ret;
                //result.TotalList.Add(new DLY14010_TOTAL_Member());
                //foreach (var rec in result.DataList)
                //{
                //    result.TotalList[0].数量合計 += (decimal)rec.数量;
                //    result.TotalList[0].金額合計 += (decimal)rec.金額;
                //}

                //return result;
            }
        }
		public void UpdateColumn(int p明細番号, int p明細行, string colname, object val)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				// トランザクションのインスタンス化(開始)
				using (var tran = new TransactionScope())
				{
					DateTime updtime = DateTime.Now;
					string sql = string.Empty;

					sql = string.Format("UPDATE T03_KTRN SET {0} = '{1}' WHERE 明細番号 = {2} AND 明細行 = {3}"
										,colname, val.ToString(), p明細番号, p明細行);
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


	}
}