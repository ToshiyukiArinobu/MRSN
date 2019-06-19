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
    public class DLY15010
    {
        #region 入金CSVデータ取得
        public class DLY15010_GetData_CSV
        {
            public int? 得意先ID { get; set; }
            public DateTime? 入金日付 { get; set; }
            public string 得意先名 { get; set; }
            public int? 入金区分 { get; set; }
            public decimal? 入金金額 { get; set; }
            public decimal? 入金合計 { get; set; }
            public string 摘要名 { get; set; }
            public DateTime? 手形日付 { get; set; }
            public int? 明細番号 { get; set; }
            public int? 明細行 { get; set; }
        }
        #endregion

        public class DLY15010_DATASET
        {
            public List<DLY15010_PREVIEW> DataList = new List<DLY15010_PREVIEW>();
            public List<DLY15010_TOTAL_Member> TotalList = new List<DLY15010_TOTAL_Member>();
        }

        public class DLY15010_TOTAL_Member
        {
            public int 入金金額 { get; set; }
        }

        #region T04_NYUKのリスト取得
        /// <summary>
        /// T04_NYUKのリスト取得
        /// </summary>
        /// <param name="p明細番号">明細番号</param>
        /// <param name="p明細行">明細行</param>
        /// <returns>Member_T01_NYUK</returns>
        public DLY15010_DATASET DLY15010_GetData(int? p担当者ID, string p得意先ID, string p得意先名, DateTime? d適用開始日From, DateTime? d適用開始日To, int 日付区分combo
                                                , int p入金区分, string 表示順指定0, string 表示順指定1, string 表示順指定2, string 表示順指定3, string 表示順指定4, string p摘要指定)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                DLY15010_DATASET result = new DLY15010_DATASET();
                context.Connection.Open();

                //入金一覧クエリ
                var query = (from t04 in context.T04_NYUK
                             from m01 in context.M01_TOK.Where(m01 => m01.削除日付 == null).DefaultIfEmpty()
                             where t04.取引先KEY == m01.得意先KEY && t04.明細区分 == 2 &&
							 (p担当者ID == null || t04.入力者ID == p担当者ID) && (p入金区分 == 0 || t04.入出金区分 == p入金区分)
							 && (p摘要指定 == "" || t04.摘要名.Contains(p摘要指定))
                             select new DLY15010_PREVIEW
                             {
                                 得意先ID = m01.得意先ID,
                                 入金日付 = t04.入出金日付 == null ? (DateTime)t04.入出金日付.Value : (DateTime)t04.入出金日付,
                                 得意先名 = m01.得意先名１,
                                 入金区分 = t04.入出金区分 == 1 ? "現金" : t04.入出金区分 == 2 ? "振込み" : t04.入出金区分 == 3 ? "小切手" :
                                            t04.入出金区分 == 4 ? "手形" : t04.入出金区分 == 5 ? "相殺" : t04.入出金区分 == 6 ? "調整" :
                                            t04.入出金区分 == 7 ? "その他" : t04.入出金区分 == 8 ? "値引き" : t04.入出金区分 == 9 ? "手数料" : "",
                                 入金金額 = t04.入出金金額 == null ? 0 : t04.入出金金額,
                                 摘要名 = t04.摘要名,
                                 入金合計 = 0,
                                 手形日付 = t04.手形日付 == null ? (DateTime)t04.手形日付.Value : (DateTime)t04.手形日付,
                                 明細番号 = t04.明細番号,
                                 明細行 = t04.明細行,
                                 得意先指定 = p得意先ID + "　" + p得意先名,
                                 検索日付From = d適用開始日From,
                                 検索日付To = d適用開始日To,
                                 検索日付選択 = 日付区分combo == 1 ? "入金日付" : 日付区分combo == 2 ? "手形日付" : "入金日付",
                                 表示順序 = 表示順指定0 +", "+ 表示順指定1 +", "+ 表示順指定2 +", "+ 表示順指定3 +", "+ 表示順指定4,
                             }).AsQueryable();

                //日付区分選択条件
                switch (日付区分combo)
                {
                    //入金日付条件
                    case 1:

                        if(d適用開始日From == null && d適用開始日To == null)
                        {
                            //d適用開始日From、d適用開始日Toの値がNULLの時
                            query = query.Where(c => c.入金日付 >= DateTime.MinValue && c.入金日付 <= DateTime.MaxValue);
                        }
                        else if(d適用開始日From == null)
                        {
                            //d適用開始日Fromの値がNULLの時
                            query = query.Where(c => c.入金日付 <= d適用開始日To);
                        }
                        else if(d適用開始日To == null)
                        {
                            //d適用開始日Fromの値がNULLの時
                            query = query.Where(c => c.入金日付 >= d適用開始日From);
                        }
                        else
                        {
                            //d適用開始日From、d適用開始日Toの値がある時
                            query = query.Where(c => c.入金日付 >= d適用開始日From && c.入金日付 <= d適用開始日To);
                        }
                        break;

                    //手形決済日条件
                    case 2:
                        
                        if(d適用開始日From == null && d適用開始日To == null)
                        {
                            //d適用開始日From、d適用開始日Toの値がNULLの時
                            query = query.Where(c => c.手形日付 >= DateTime.MinValue && c.手形日付 <= DateTime.MaxValue);
                        }
                        else if(d適用開始日From == null)
                        {
                            //d適用開始日Fromの値がNULLの時
                            query = query.Where(c => c.手形日付 <= d適用開始日To);
                        }
                        else if(d適用開始日To == null)
                        {
                            //d適用開始日Fromの値がNULLの時
                            query = query.Where(c => c.手形日付 >= d適用開始日From);
                        }
                        else
                        {
                            //d適用開始日From、d適用開始日Toの値がある時
                            query = query.Where(c => c.手形日付 >= d適用開始日From && c.手形日付 <= d適用開始日To);
                        }
                        break;
                }

                //得意先選択条件
                if (!string.IsNullOrEmpty(p得意先ID))
                {
                    int? ip得意先ID = Convert.ToInt32(p得意先ID);
                    query = query.Where(c => c.得意先ID == ip得意先ID);
                }

                result.DataList = query.ToList();
                result.TotalList.Add(new DLY15010_TOTAL_Member());
                foreach (var rec in result.DataList)
                {

                    result.TotalList[0].入金金額 += Convert.ToInt32(rec.入金金額);
                }

                return result;

            }
        }
        #endregion

        #region T04_NYUKのリストCSVデータ取得
        /// <summary>
        /// T04_NYUKのリスト取得
        /// </summary>
        /// <param name="p明細番号">明細番号</param>
        /// <param name="p明細行">明細行</param>
        /// <returns>Member_T01_NYUK</returns>
        public List<DLY15010_GetData_CSV> DLY15010_CSV(string p得意先ID, string p得意先名, DateTime? d適用開始日From, DateTime? d適用開始日To, int 日付区分combo)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<DLY15010_GetData_CSV> retList = new List<DLY15010_GetData_CSV>();
                context.Connection.Open();

                //入金一覧クエリ
                var query = (from m01 in context.M01_TOK.Where(m01 => m01.削除日付 == null)
                             from t04 in context.T04_NYUK.DefaultIfEmpty()
                             where t04.取引先KEY == m01.得意先KEY && t04.明細区分 == 2
                             select new DLY15010_GetData_CSV
                             {
                                 得意先ID = m01.得意先ID,
                                 入金日付 = t04.入出金日付 == null ? (DateTime)t04.入出金日付.Value : (DateTime)t04.入出金日付,
                                 得意先名 = m01.得意先名１,
                                 入金区分 = t04.入出金区分,
                                 入金金額 = t04.入出金金額 == null ? 0 : t04.入出金金額,
                                 摘要名 = t04.摘要名,
                                 入金合計 = 0,
                                 手形日付 = t04.手形日付 == null ? (DateTime)t04.手形日付.Value : (DateTime)t04.手形日付,
                                 明細番号 = t04.明細番号,
                                 明細行 = t04.明細行,
                             }).AsQueryable();

                //日付区分選択条件
                switch (日付区分combo)
                {
                    //入金日付条件
                    case 1:

                        if (d適用開始日From == null && d適用開始日To == null)
                        {
                            //d適用開始日From、d適用開始日Toの値がNULLの時
                            query = query.Where(c => c.入金日付 >= DateTime.MinValue && c.入金日付 <= DateTime.MaxValue);
                        }
                        else if (d適用開始日From == null)
                        {
                            //d適用開始日Fromの値がNULLの時
                            query = query.Where(c => c.入金日付 <= d適用開始日To);
                        }
                        else if (d適用開始日To == null)
                        {
                            //d適用開始日Fromの値がNULLの時
                            query = query.Where(c => c.入金日付 >= d適用開始日From);
                        }
                        else
                        {
                            //d適用開始日From、d適用開始日Toの値がある時
                            query = query.Where(c => c.入金日付 >= d適用開始日From && c.入金日付 <= d適用開始日To);
                        }
                        break;

                    //手形決済日条件
                    case 2:

                        if (d適用開始日From == null && d適用開始日To == null)
                        {
                            //d適用開始日From、d適用開始日Toの値がNULLの時
                            query = query.Where(c => c.手形日付 >= DateTime.MinValue && c.手形日付 <= DateTime.MaxValue);
                        }
                        else if (d適用開始日From == null)
                        {
                            //d適用開始日Fromの値がNULLの時
                            query = query.Where(c => c.手形日付 <= d適用開始日To);
                        }
                        else if (d適用開始日To == null)
                        {
                            //d適用開始日Fromの値がNULLの時
                            query = query.Where(c => c.手形日付 >= d適用開始日From);
                        }
                        else
                        {
                            //d適用開始日From、d適用開始日Toの値がある時
                            query = query.Where(c => c.手形日付 >= d適用開始日From && c.手形日付 <= d適用開始日To);
                        }
                        break;
                }

                //得意先選択条件
                if (!string.IsNullOrEmpty(p得意先ID))
                {
                    int? ip得意先ID = Convert.ToInt32(p得意先ID);
                    query = query.Where(c => c.得意先ID == ip得意先ID);
                }


                //入金合計計算用にリスト追加
                List<DLY15010_GetData_CSV> ResultSet = new List<DLY15010_GetData_CSV>();
                ResultSet = query.ToList();
                int 入金金額 = 0;

                //入金金額の合計を計算
                for (int i = 0; i < ResultSet.Count; i++)
                {
                    入金金額 = 入金金額 + Convert.ToInt32(ResultSet[i].入金金額);
                }

                //計算結果をリストの入金合計に追加
                for (int i = 0; i < ResultSet.Count; i++)
                {
                    ResultSet[i].入金合計 = 入金金額;
                }

                //retListにrsをセットする
                retList = ResultSet;
                return retList;
            }
        }
        #endregion

        #region SPREAD登録・修正
        /// <summary>
        /// SPREAD登録・修正
        /// </summary>
        /// <param name="p明細番号">明細番号</param>
        /// <param name="p明細行">明細行</param>
        /// <returns>Member_T01_NYUK</returns>
        public void UpdateColumn(int p明細番号, int p明細行, string colname, object val)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // トランザクションのインスタンス化(開始)
                using (var tran = new TransactionScope())
                {
                    if(colname == "入金金額")
                    {
                        colname = "入出金金額";
                    }

                    DateTime updtime = DateTime.Now;
                    string sql = string.Empty;

                    sql = string.Format("UPDATE T04_NYUK SET {0} = '{1}' WHERE 明細番号 = {2} AND 明細行 = {3}"
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
        #endregion
    }
}