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
    public class DLY13010
    {
        const string SelectedChar = "a";
        const string UnselectedChar = "";

        public class MasterList_Member
        {
            public string 選択 { get; set; }
            public int コード { get; set; }
            public string 表示名 { get; set; }
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
                    //case "得意先":
                    //    result = (from mst in context.M01_TOK
                    //              where mst.取引区分 == 0 || mst.取引区分 == 1
                    //                 && mst.削除日付 == null
                    //              orderby mst.得意先ID
                    //              select new MasterList_Member
                    //              {
                    //                  選択 = selsts,
                    //                  コード = mst.得意先ID,
                    //                  表示名 = mst.得意先名１,
                    //              }
                    //              ).ToList();
                    //    break;
                    //case "支払先":
                    //    result = (from mst in context.M01_TOK
                    //              where mst.取引区分 == 0 || mst.取引区分 == 2
                    //                 && mst.削除日付 == null
                    //              orderby mst.得意先ID
                    //              select new MasterList_Member
                    //              {
                    //                  選択 = selsts,
                    //                  コード = mst.得意先ID,
                    //                  表示名 = mst.得意先名１,
                    //              }
                    //              ).ToList();
                    //    break;
                    //case "仕入先":
                    //    result = (from mst in context.M01_TOK
                    //              where mst.取引区分 == 0 || mst.取引区分 == 3
                    //                 && mst.削除日付 == null
                    //              orderby mst.得意先ID
                    //              select new MasterList_Member
                    //              {
                    //                  選択 = selsts,
                    //                  コード = mst.得意先ID,
                    //                  表示名 = mst.得意先名１,
                    //              }
                    //              ).ToList();
                    //    break;
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
                    //case "車種":
                    //    result = (from mst in context.M06_SYA
                    //              where mst.削除日付 == null
                    //              orderby mst.車種ID
                    //              select new MasterList_Member
                    //              {
                    //                  選択 = selsts,
                    //                  コード = mst.車種ID,
                    //                  表示名 = mst.車種名,
                    //              }
                    //              ).ToList();
                    //    break;
                    //case "発地":
                    //    result = (from mst in context.M08_TIK
                    //              where mst.削除日付 == null
                    //              orderby mst.発着地ID
                    //              select new MasterList_Member
                    //              {
                    //                  選択 = selsts,
                    //                  コード = mst.発着地ID,
                    //                  表示名 = mst.発着地名,
                    //              }).ToList();
                    //    break;
                    //case "着地":
                    //    result = (from mst in context.M08_TIK
                    //              where mst.削除日付 == null
                    //              orderby mst.発着地ID
                    //              select new MasterList_Member
                    //              {
                    //                  選択 = selsts,
                    //                  コード = mst.発着地ID,
                    //                  表示名 = mst.発着地名,
                    //              }
                    //              ).ToList();
                    //    break;
                    //case "商品":
                    //    result = (from mst in context.M09_HIN
                    //              where mst.削除日付 == null
                    //              orderby mst.商品ID
                    //              select new MasterList_Member
                    //              {
                    //                  選択 = selsts,
                    //                  コード = mst.商品ID,
                    //                  表示名 = mst.商品名,
                    //              }
                    //              ).ToList();
                    //    break;
                }
            }
            return result;
        }

        public class DLY13010_TOTAL_Member
        {
            //public decimal 現金通行料合計 { get; set; }
            //public decimal プレート合計 { get; set; }
            //public decimal フェリー代合計 { get; set; }
            //public decimal 運行費合計 { get; set; }
            //public decimal 電話代合計 { get; set; }
            //public decimal その他合計 { get; set; }
            //public decimal 燃料代合計 { get; set; }
            //public decimal 稼動金額合計 { get; set; }
            //public decimal 走行ｋｍ合計 { get; set; }
            //public decimal 実車ｋｍ合計 { get; set; }
            //public decimal 輸送屯数合計 { get; set; }
        }

        public class DLY13010_DATASET
        {
            public List<DLY13010_Member> DataList = new List<DLY13010_Member>();
            public List<DLY13010_TOTAL_Member> TotalList = new List<DLY13010_TOTAL_Member>();
        }

        /// <summary>
        /// 売上明細問い合わせリスト取得
        /// </summary>
        /// <param name="p検索日付From">検索日付From(未選択の場合はnull)</param>
        /// <param name="p検索日付To">検索日付To(未選択の場合はnull)</param>
        /// <param name="p検索日付区分">検索日付区分</param>
        /// <param name="p売上未定区分">売上未定区分(0:全件 1:未定のみ 2:確定のみ 3:金額が未入力のみ)</param>
        /// <returns>DLY13010_Memberのリスト</returns>
        public DLY13010_DATASET GetListDLY13010( int? p担当者ID,
            int? p車輌ID, int? p乗務員ID
            , DateTime? p検索日付From, DateTime? p検索日付To, int p検索日付区分
            , int? p自社部門ID, int? p売上未定区分
            , string p商品名, string p発地名, string p着地名, string p請求摘要, string p社内備考
            , string 表示順指定0, string 表示順指定1, string 表示順指定2, string 表示順指定3, string 表示順指定4, string 自社部門Value
            , int p乗務員FROM, int p乗務員TO
            , int p車輌FROM, int p車輌TO
            , int?[] p乗務員
            , int?[] p車輌
            )
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                DLY13010_DATASET result = new DLY13010_DATASET();
                var query = (from t02 in context.T02_UTRN.Where(x => x.明細行 == 1)
                             from m04 in context.M04_DRV.Where(x => x.乗務員KEY == t02.乗務員KEY).DefaultIfEmpty()
                             from m05 in context.M05_CAR.Where(x => x.車輌KEY == t02.車輌KEY).DefaultIfEmpty()
                             from m78 in context.M78_SYK.Where(x => x.出勤区分ID == t02.出勤区分ID ).DefaultIfEmpty()
                             where (p担当者ID == null || p担当者ID == 0 || t02.入力者ID == p担当者ID)
                                && (p自社部門ID == null || p自社部門ID == 0 || t02.自社部門ID == p自社部門ID)
                                && (p車輌ID == null || p車輌ID == 0 || m05.車輌ID == p車輌ID)
                             select new DLY13010_Member
                             {
                                 出庫日付 = t02.実運行日開始,
                                 帰庫日付 = t02.実運行日終了,
                                 出勤区分 = m78.出勤区分名,
                                 出社時間 = t02.出庫時間 == null ? 0 : t02.出庫時間,
                                 退社時間 = t02.帰庫時間 == null ? 0 : t02.帰庫時間,
                                 自社部門ID = t02.自社部門ID,
                                 乗務員ID = m04.乗務員ID,
                                 運転者名 = m04.乗務員名,
                                 車輌ID = m05.車輌ID,
                                 車輌番号 = t02.車輌番号,
                                 車種ID = t02.車種ID,
                                 拘束時間 = (decimal)t02.拘束時間 == null ? 0 : (decimal)t02.拘束時間,
                                 運転時間 = (decimal)t02.運転時間 == null ? 0 : (decimal)t02.運転時間,
                                 作業時間 = (decimal)t02.作業時間 == null ? 0 : (decimal)t02.作業時間,
                                 待機時間 = (decimal)t02.待機時間 == null ? 0 : (decimal)t02.待機時間,
                                 休憩時間 = (decimal)t02.休憩時間 == null ? 0 : (decimal)t02.休憩時間,
                                 残業時間 = (decimal)t02.残業時間 == null ? 0 : (decimal)t02.残業時間,
                                 深夜時間 = (decimal)t02.深夜時間 == null ? 0 : (decimal)t02.深夜時間,
                                 走行ｋｍ = t02.走行ＫＭ == null ? 0 : t02.走行ＫＭ,
                                 実車ｋｍ = t02.実車ＫＭ == null ? 0 : t02.実車ＫＭ,
                                 輸送屯数 = t02.輸送屯数 == null ? 0 : t02.輸送屯数,
                                 出庫ｋｍ = t02.出庫ＫＭ == null ? 0 : t02.出庫ＫＭ,
                                 帰庫ｋｍ = t02.帰庫ＫＭ == null ? 0 : t02.帰庫ＫＭ,
                                 明細番号 = t02.明細番号,
                                 明細行 = t02.明細行,
                                 明細区分 = t02.明細区分,
                                 入力区分 = t02.入力区分,
                                 s入力区分 = t02.入力区分 == 1 ? "運転日報" : t02.入力区分 == 2 ? "日報入力" : "",
                                 検索日付From = p検索日付From,
                                 検索日付To = p検索日付To,
                                 部門指定 = 自社部門Value,
                                 表示順序 = 表示順指定0 + " ," + 表示順指定1 + " ," + 表示順指定2 + " ," + 表示順指定3 + " ," + 表示順指定4,
                             }).Distinct().AsQueryable();

                List<DLY13010_Member> ret = null;
                if (p車輌ID != null || p乗務員ID != null || p検索日付From != null || p検索日付To != null)
                {
                    //車輌検索
                    if (p車輌ID != null)
                        query = query.Where(c => c.車輌ID == p車輌ID);
                    //乗員検索
                    if (p乗務員ID != null)
                        query = query.Where(c => c.乗務員ID == p乗務員ID);
                    //d適用開始日Fromの値がNULLの時
                    if (p検索日付From != null)
                        query = query.Where(c => c.出庫日付 >= p検索日付From);
                    //d適用開始日Fromの値がNULLの時
                    if (p検索日付To != null)
                        query = query.Where(c => c.出庫日付 <= p検索日付To);
                    //d適用開始日From、d適用開始日Toの値がある時
                    if (p検索日付From != null && p検索日付To != null)
                        query = query.Where(c => c.出庫日付 >= p検索日付From && c.出庫日付 <= p検索日付To);
                    ret = query.ToList();
                }
                else
                {
                    ret = query.ToList();
                }

                // コードの範囲指定での絞り込み
				if (p乗務員FROM >= 0)
                    ret = query.Where(x => x.乗務員ID >= p乗務員FROM).ToList();
				if (p乗務員TO >= 0)
                    ret = query.Where(x => x.乗務員ID <= p乗務員TO).ToList();
				if (p車輌FROM >= 0)
                    ret = query.Where(x => x.車輌ID >= p車輌FROM).ToList();
				if (p車輌TO >= 0)
                    ret = query.Where(x => x.車輌ID <= p車輌TO).ToList();

				// コード一覧の絞込み
				if (p乗務員.Length > 0)
                    ret = query.Where(x => p乗務員.Contains(x.乗務員ID)).ToList();
				if (p車輌.Length > 0)
                    ret = query.Where(x => p車輌.Contains(x.車輌ID)).ToList();
                
                result.DataList = ret.ToList();
                return result;
                
                //合計計算処理
                //result.TotalList.Add(new DLY13010_TOTAL_Member());
                //foreach (var rec in result.DataList)
                //{
                //result.TotalList[0].現金通行料合計 += rec.売上金額;
                //result.TotalList[0].プレート合計 += rec.通行料;
                //result.TotalList[0].フェリー代合計 += rec.支払金額;
                //result.TotalList[0].電話代合計 += rec.支払通行料;
                //result.TotalList[0].稼動金額合計 += (rec.売上金額 + rec.通行料) - (rec.支払金額 + rec.支払通行料);
                //}
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

                    if (colname == "出庫日付")
                    {
                        colname = "実運行日開始";
                    }
                    else if (colname == "帰庫日付")
                    {
                        colname = "実運行日終了";
                    }
                    else if (colname == "出社時間")
                    {
                        colname = "出庫時間";
                    }
                    else if (colname == "退社時間")
                    {
                        colname = "帰庫時間";
                    }

                    sql = string.Format("UPDATE T02_UTRN SET {0} = '{1}' WHERE 明細番号 = {2} AND 明細行 = {3}"
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
    }
}