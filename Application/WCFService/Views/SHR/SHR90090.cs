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
    /// 支払データ削除サービスクラス
    /// </summary>
    public class SHR90090
    {
        #region 項目クラス定義

        /// <summary>
        /// SHR90090 支払一覧検索メンバー
        /// </summary>
        public class SearchDataMember
        {
            public bool 印刷区分 { get; set; }
            public string ID { get; set; }  // No.233 Add
            public int 自社コード { get; set; }
            public int 締日 { get; set; }
            public int 支払年月 { get; set; }
            public int 得意先コード { get; set; }
            public int 得意先枝番 { get; set; }
            public string 得意先名 { get; set; }
            public int 回数 { get; set; }
            public string 集計期間 { get; set; }
            public long 当月支払額 { get; set; }
            public string 郵便番号 { get; set; }
            public string 住所１ { get; set; }
            public string 住所２ { get; set; }
            public string 電話番号 { get; set; }
            public int 支払日 { get; set; }
        }
        #endregion


        #region 支払データ検索情報取得
        /// <summary>
        /// 支払データ検索情報取得
        /// </summary>
        /// <param name="condition">
        ///  == 検索条件 ==
        ///  自社コード
        ///  作成年月日
        ///  作成年月
        ///  作成締日
        ///  得意先コード
        ///  得意先枝番
        /// </param>
        /// <returns></returns>
        public List<SearchDataMember> GetDataList(Dictionary<string, string> condition)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try
                {
                    // パラメータの型変換
                    int ival;
                    int myCompany = int.Parse(condition["自社コード"]);
                    int createYM = int.Parse(condition["作成年月"].Replace("/", ""));
                    int? closingDate = int.TryParse(condition["作成締日"], out ival) ? ival : (int?)null;
                    int? customerCd = int.TryParse(condition["得意先コード"], out ival) ? ival : (int?)null;
                    int? customerEda = int.TryParse(condition["得意先枝番"], out ival) ? ival : (int?)null;

                    var result =
                        context.S02_SHRHD
                            .Where(w => w.自社コード == myCompany && w.支払年月 == createYM && (closingDate == null || w.支払締日 == closingDate))
                            .Join(context.M01_TOK.Where(w => w.削除日時 == null),
                                x => new { コード = x.支払先コード, 枝番 = x.支払先枝番 },
                                y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                                (x, y) => new { x, y })
                            .Select(x => new { SHRHD = x.x, TOK = x.y })
                            .OrderBy(o => o.SHRHD.支払年月日)
                            .ThenBy(t => t.SHRHD.支払先コード)
                            .ThenBy(t => t.SHRHD.支払先枝番)
                            .ToList();

                    // 取引先の指定があれば条件を追加
                    if (customerCd != null && customerEda != null)
                    {
                        result = result.Where(w =>
                                w.SHRHD.支払先コード == customerCd &&
                                w.SHRHD.支払先枝番 == customerEda)
                            .ToList();
                    }
                    else if (customerCd != null)
                    {
                        result = result.Where(w =>
                                w.SHRHD.支払先コード == customerCd)
                            .ToList();
                    }


                    // 返却用にデータを整形
                    var dataList =
                        result.Select(x => new SearchDataMember
                        {
                            印刷区分 = false,
                            ID = string.Format("{0:D4} - {1:D2}", x.SHRHD.支払先コード, x.SHRHD.支払先枝番),   // No.223 Add
                            自社コード = x.SHRHD.自社コード,
                            締日 = x.SHRHD.支払締日,
                            支払年月 = x.SHRHD.支払年月,
                            得意先コード = x.SHRHD.支払先コード,
                            得意先枝番 = x.SHRHD.支払先枝番,
                            得意先名 = x.TOK.略称名,
                            回数 = x.SHRHD.回数,
                            集計期間 = string.Format("{0:yyyy/MM/dd}～{1:yyyy/MM/dd}", x.SHRHD.集計開始日, x.SHRHD.集計最終日),
                            当月支払額 = x.SHRHD.当月支払額,
                            郵便番号 = x.TOK.郵便番号,
                            住所１ = x.TOK.住所１,
                            住所２ = x.TOK.住所２,
                            電話番号 = x.TOK.電話番号,
                            支払日 = x.SHRHD.支払日
                        })
                        .ToList();

                    return dataList.ToList();

                }
                catch (System.ArgumentException agex)
                {
                    throw agex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }
        #endregion


        #region 支払データ削除
        /// <summary>
        /// 支払データ削除
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public bool DataDelete(DataSet ds)
        {
            DataTable tbl = ds.Tables[0];
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    try
                    {
                        foreach (DataRow row in tbl.Rows)
                        {
                            SearchDataMember mem = getSearchDataMemberRow(row);
                            if (mem.印刷区分 == false)
                            {
                                //印刷区分falseは削除対象外
                                continue;
                            }

                            var delList =
                            context.S02_SHRDTL
                                .Where(w =>
                                    w.自社コード == mem.自社コード &&
                                    w.支払年月 == mem.支払年月 &&
                                    w.支払締日 == mem.締日 &&
                                    w.支払先コード == mem.得意先コード &&
                                    w.支払先枝番 == mem.得意先枝番 &&
                                    w.支払日 == mem.支払日 &&
                                    w.回数 == mem.回数);

                            foreach (var delData in delList)
                            {
                                context.S02_SHRDTL.DeleteObject(delData);
                            }

                            var delHead =
                            context.S02_SHRHD
                                .Where(w =>
                                    w.自社コード == mem.自社コード &&
                                    w.支払年月 == mem.支払年月 &&
                                    w.支払締日 == mem.締日 &&
                                    w.支払先コード == mem.得意先コード &&
                                    w.支払先枝番 == mem.得意先枝番 &&
                                    w.支払日 == mem.支払日 &&
                                    w.回数 == mem.回数);
                            foreach (var del in delHead)
                            {
                                context.S02_SHRHD.DeleteObject(del);
                            }
                        }
                        context.SaveChanges();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                    tran.Commit(); 
                }
            }

            return true;
        }
        #endregion
        
        #region << サービス処理関連 >>

        /// <summary>
        /// データ行を検索メンバに変換して返す
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private SearchDataMember getSearchDataMemberRow(DataRow row)
        {
            SearchDataMember mem = new SearchDataMember();

            mem.印刷区分 = bool.Parse(row["印刷区分"].ToString());
            mem.自社コード = int.Parse(row["自社コード"].ToString());
            mem.支払年月 = int.Parse(row["支払年月"].ToString());
            mem.締日 = int.Parse(row["締日"].ToString());
            mem.得意先コード = int.Parse(row["得意先コード"].ToString());
            mem.得意先枝番 = int.Parse(row["得意先枝番"].ToString());
            mem.得意先名 = row["得意先名"].ToString();
            mem.回数 = int.Parse(row["回数"].ToString());
            mem.集計期間 = row["集計期間"].ToString();
            mem.当月支払額 = long.Parse(row["当月支払額"].ToString());
            mem.郵便番号 = row["郵便番号"].ToString();
            mem.住所１ = row["住所１"].ToString();
            mem.住所２ = row["住所２"].ToString();
            mem.電話番号 = row["電話番号"].ToString();
            mem.支払日 = int.Parse(row["支払日"].ToString());

            return mem;

        }

        #endregion

    }

}