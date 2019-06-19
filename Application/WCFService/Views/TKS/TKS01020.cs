﻿using System;
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
    /// 請求書発行サービスクラス
    /// </summary>
    public class TKS01020
    {
        #region 定数定義

        /// <summary>請求書印刷 ヘッダーテーブル名</summary>
        private const string PRINT_HEADER_TABLE_NAME = "TKS01020_H請求書";
        /// <summary>請求書印刷 明細テーブル名</summary>
        private const string PRINT_DETAIL_TABLE_NAME = "TKS01020_D請求書";

        #endregion

        #region 項目クラス定義

        /// <summary>
        /// TKS01020 請求一覧検索メンバー
        /// </summary>
        public class SearchDataMember
        {
            public bool 印刷区分 { get; set; }
            public int 得意先コード { get; set; }
            public int 得意先枝番 { get; set; }
            public string 得意先名 { get; set; }
            public int 回数 { get; set; }
            public string 集計期間 { get; set; }
            public long 当月請求額 { get; set; }
            public string 郵便番号 { get; set; }
            public string 住所１ { get; set; }
            public string 住所２ { get; set; }
            public string 電話番号 { get; set; }
            public int 入金日 { get; set; }
        }

        /// <summary>
        /// TKS01020_H請求書
        /// </summary>
        private class PrintHeaderMember
        {
            /// <summary>
            /// 請求先で改ページさせる為のキー文字列
            /// </summary>
            public string PagingKey { get; set; }
            public string 自社コード { get; set; }
            public string 請求年月 { get; set; }
            public string 請求先コード { get; set; }
            public string 請求先枝番 { get; set; }
            public string 得意先コード { get; set; }
            public string 得意先枝番 { get; set; }
            public int 回数 { get; set; }

            public int 請求年 { get; set; }
            public int 請求月 { get; set; }
            public string 請求先郵便番号 { get; set; }
            public string 請求先住所１ { get; set; }
            public string 請求先住所２ { get; set; }
            public string 得意先名称 { get; set; }
            public string 自社名称 { get; set; }
            public string 自社郵便番号 { get; set; }
            public string 自社住所 { get; set; }
            public string 自社TEL { get; set; }
            public string 自社FAX { get; set; }
            public string 締日 { get; set; }
            public string 発行日付 { get; set; }
            public decimal 前回請求額 { get; set; }
            public decimal 今回入金額 { get; set; }
            public decimal 繰越残高 { get; set; }
            public decimal 御買上額 { get; set; }
            /// <summary>
            /// (通常の)消費税額を設定
            /// </summary>
            public decimal 消費税S { get; set; }
            /// <summary>
            /// (軽減税率の)消費税額を設定
            /// </summary>
            public decimal 消費税K { get; set; }
            public decimal 今回請求額 { get; set; }
            /// <summary>
            /// 振込先の文字列を設定
            /// ○○銀行 ○○支店 口座種別：〇〇 口座番号：ｘｘｘｘｘｘｘ　口座名義：ｘｘｘｘｘｘｘ
            /// </summary>
            public string 振込先 { get; set; }
        }

        /// <summary>
        /// TKS01020_D請求書
        /// </summary>
        private class PrintDetailMember
        {
            public string PagingKey { get; set; }
            public string 自社コード { get; set; }
            public string 請求年月 { get; set; }
            public string 請求先コード { get; set; }
            public string 請求先枝番 { get; set; }
            public string 得意先コード { get; set; }
            public string 得意先枝番 { get; set; }
            public int 回数 { get; set; }

            public string 伝票番号 { get; set; }
            public string 売上日 { get; set; }
            public string 品番名称 { get; set; }
            public decimal 数量 { get; set; }
            public decimal 単価 { get; set; }
            public int 金額 { get; set; }
            /// <summary>
            /// 軽減税率適用の場合に「*」を設定
            /// </summary>
            public string 軽減税率適用 { get; set; }

        }

        #endregion


        #region 請求一覧検索情報取得
        /// <summary>
        /// 請求一覧検索情報取得
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
                    int closingDate = int.Parse(condition["作成締日"]);
                    int? customerCd = int.TryParse(condition["得意先コード"], out ival) ? ival : (int?)null;
                    int? customerEda = int.TryParse(condition["得意先枝番"], out ival) ? ival : (int?)null;

                    var result =
                        context.S01_SEIHD
                            .Where(w => w.自社コード == myCompany && w.請求年月 == createYM && w.請求締日 == closingDate)
                            .Join(context.M01_TOK.Where(w => w.削除日時 == null),
                                x => new { コード = x.請求先コード, 枝番 = x.請求先枝番 },
                                y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                                (x, y) => new { x, y })
                            .Select(x => new { SEIHD = x.x, TOK = x.y })
                            .OrderBy(o => o.SEIHD.請求年月日)
                            .ThenBy(t => t.SEIHD.請求先コード)
                            .ThenBy(t => t.SEIHD.請求先枝番)
                            .ToList();

                    // 取引先の指定があれば条件を追加
                    if (customerCd != null && customerEda != null)
                        result = result.Where(w =>
                                w.SEIHD.請求先コード == customerCd &&
                                w.SEIHD.請求先枝番 == customerEda)
                            .ToList();

                    // 返却用にデータを整形
                    var dataList =
                        result.Select(x => new SearchDataMember
                        {
                            印刷区分 = true,
                            得意先コード = x.SEIHD.請求先コード,
                            得意先枝番 = x.SEIHD.請求先枝番,
                            得意先名 = x.TOK.得意先名１,
                            回数 = x.SEIHD.回数,
                            集計期間 = string.Format("{0:yyyy/MM/dd}～{1:yyyy/MM/dd}", x.SEIHD.集計開始日, x.SEIHD.集計最終日),
                            当月請求額 = x.SEIHD.当月請求額,
                            郵便番号 = x.TOK.郵便番号,
                            住所１ = x.TOK.住所１,
                            住所２ = x.TOK.住所２,
                            電話番号 = x.TOK.電話番号,
                            入金日 = x.SEIHD.入金日
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

        #region 請求書印字データ取得
        /// <summary>
        /// 請求書印字データを取得する
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
        /// <param name="ds">
        /// [0]請求一覧データ
        /// </param>
        /// <returns></returns>
        public DataSet GetPrintData(Dictionary<string, string> condition, DataSet ds)
        {
            DataSet dsResult = new DataSet();
            DataTable tbl = ds.Tables[0];

            // パラメータの型変換
            int ival;
            int myCompany = int.Parse(condition["自社コード"]);
            int createYM = int.Parse(condition["作成年月"].Replace("/", ""));
            DateTime printDate = DateTime.Parse(condition["作成年月日"]);
            int closingDate = int.Parse(condition["作成締日"]);
            int? customerCd = int.TryParse(condition["得意先コード"], out ival) ? ival : (int?)null;
            int? customerEda = int.TryParse(condition["得意先枝番"], out ival) ? ival : (int?)null;

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                foreach (DataRow row in tbl.Rows)
                {
                    SearchDataMember mem = getSearchDataMemberRow(row);

                    if (mem.印刷区分 == false)
                        continue;

                    // 前月情報を取得
                    DateTime befYearMonth = new DateTime(createYM / 100, createYM % 100, 1).AddMonths(-1);
                    int iBefYearMonth = befYearMonth.Year * 100 + befYearMonth.Month;

                    #region 必要情報の取得

                    // 対象の請求ヘッダを取得
                    var seihd =
                        context.S01_SEIHD.Where(w =>
                                w.自社コード == myCompany &&
                                w.請求年月 == createYM &&
                                w.請求締日 == closingDate &&
                                w.請求先コード == mem.得意先コード &&
                                w.請求先枝番 == mem.得意先枝番 &&
                                w.入金日 == mem.入金日 &&
                                w.回数 == mem.回数)
                            .FirstOrDefault();

                    // 取引先情報を取得
                    var tok =
                        context.M01_TOK.Where(w =>
                                w.削除日時 == null &&
                                w.取引先コード == seihd.請求先コード &&
                                w.枝番 == seihd.請求先枝番)
                            .FirstOrDefault();

                    // 前月の入金日
                    int befPaymentDate = iBefYearMonth * 100 + tok.Ｔ入金日１ ?? 31;

                    // 前月の請求ヘッダを取得
                    var befSeihd =
                        context.S01_SEIHD.Where(w =>
                                w.自社コード == myCompany &&
                                w.請求年月 == iBefYearMonth &&
                                w.請求締日 == closingDate &&
                                w.請求先コード == mem.得意先コード &&
                                w.請求先枝番 == mem.得意先枝番 &&
                                w.入金日 == befPaymentDate &&
                                w.回数 == mem.回数)
                            .FirstOrDefault();

                    #endregion

                    // 前月の締期間を算出
                    DateTime befEndDate = AppCommon.GetClosingDate(befYearMonth.Year, befYearMonth.Month, closingDate, 0);
                    DateTime befStrDate = befEndDate.AddMonths(-1).AddDays(-1);

                    // 各入金額を取得する
                    long 前月入金額 = getNyukinData(context, seihd.自社コード, seihd.請求先コード, seihd.請求先枝番, befStrDate, befEndDate);
                    long 今月入金額 = getNyukinData(context, seihd.自社コード, seihd.請求先コード, seihd.請求先枝番, (DateTime)seihd.集計開始日, (DateTime)seihd.集計最終日);

                    #region 帳票ヘッダ情報取得
                    var hdResult =
                        context.S01_SEIHD.Where(w =>
                                w.自社コード == myCompany &&
                                w.請求年月 == createYM &&
                                w.請求締日 == closingDate &&
                                w.請求先コード == mem.得意先コード &&
                                w.請求先枝番 == mem.得意先枝番 &&
                                w.入金日 == mem.入金日 &&
                                w.回数 == mem.回数)
                            .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                                x => new { コード = x.請求先コード, 枝番 = x.請求先枝番 },
                                y => new { コード = y.取引先コード, 枝番 = y.枝番 },
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (a, b) => new { SEIHD = a.x, TOK = b })
                            .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                                x => x.SEIHD.自社コード,
                                y => y.自社コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (c, d) => new { c.x.SEIHD, c.x.TOK, JIS = d })
                            .ToList()
                        .Select(x => new PrintHeaderMember
                        {
                            PagingKey = string.Concat(x.SEIHD.請求先コード, "-", x.SEIHD.請求先枝番, "-", x.SEIHD.入金日, ">", x.SEIHD.回数),
                            自社コード = x.SEIHD.自社コード.ToString(),
                            請求年月 = x.SEIHD.請求年月.ToString(),
                            請求先コード = x.SEIHD.請求先コード.ToString(),
                            請求先枝番 = x.SEIHD.請求先枝番.ToString(),
                            得意先コード = x.SEIHD.請求先コード.ToString(),
                            得意先枝番 = x.SEIHD.請求先枝番.ToString(),
                            回数 = x.SEIHD.回数,
                            請求年 = x.SEIHD.請求年月 / 100,
                            請求月 = x.SEIHD.請求年月 % 100,
                            請求先郵便番号 = x.TOK.郵便番号,
                            請求先住所１ = x.TOK.住所１,
                            請求先住所２ = x.TOK.住所２,
                            得意先名称 = x.TOK.得意先名１,
                            自社名称 = x.JIS.自社名,
                            自社郵便番号 = x.JIS.郵便番号,
                            自社住所 = x.JIS.住所１.Trim() + x.JIS.住所２.Trim(),
                            自社TEL = x.JIS.電話番号,
                            自社FAX = x.JIS.ＦＡＸ,
                            締日 = (x.TOK.Ｔ締日 >= 31) ? "末" : x.TOK.Ｔ締日.ToString(),
                            発行日付 = printDate.ToString("yyyy/MM/dd"),
                            前回請求額 = x.SEIHD.前月残高,
                            今回入金額 = 今月入金額,
                            繰越残高 = (前月入金額 - (befSeihd != null ? befSeihd.当月請求額 : 0)),
                            御買上額 = x.SEIHD.売上額,
                            消費税S = x.SEIHD.消費税,
                            消費税K = 0,
                            今回請求額 = x.SEIHD.当月請求額,
                            振込先 = x.JIS.振込銀行１
                        });
                    #endregion

                    #region 帳票明細情報取得
                    var dtlResult =
                        context.S01_SEIDTL.Where(w =>
                                w.自社コード == myCompany &&
                                w.請求年月 == createYM &&
                                w.請求締日 == closingDate &&
                                w.請求先コード == mem.得意先コード &&
                                w.請求先枝番 == mem.得意先枝番 &&
                                w.入金日 == mem.入金日 &&
                                w.回数 == mem.回数)
                            .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                                x => x.品番コード,
                                y => y.品番コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (a, b) => new { SDTL = a.x, HIN = b })
                            .ToList()
                            .Select(x => new PrintDetailMember
                            {
                                PagingKey = string.Concat(x.SDTL.請求先コード, "-", x.SDTL.請求先枝番, "-", x.SDTL.入金日, ">", x.SDTL.回数),
                                自社コード = x.SDTL.自社コード.ToString(),
                                請求年月 = x.SDTL.請求年月.ToString(),
                                請求先コード = x.SDTL.請求先コード.ToString(),
                                請求先枝番 = x.SDTL.請求先枝番.ToString(),
                                得意先コード = x.SDTL.請求先コード.ToString(),
                                得意先枝番 = x.SDTL.請求先枝番.ToString(),
                                回数 = x.SDTL.回数,

                                伝票番号 = x.SDTL.伝票番号.ToString(),
                                売上日 = x.SDTL.売上日.ToString("yyyy/MM/dd"),
                                品番名称 = x.HIN.自社品名,
                                数量 = x.SDTL.数量,
                                単価 = x.SDTL.単価,
                                金額 = x.SDTL.金額,
                                軽減税率適用 = x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? "*" : ""
                            });
                    #endregion

                    DataTable hdDt = KESSVCEntry.ConvertListToDataTable<PrintHeaderMember>(hdResult.AsQueryable().ToList());
                    hdDt.TableName = PRINT_HEADER_TABLE_NAME;
                    DataTable dtlDt = KESSVCEntry.ConvertListToDataTable<PrintDetailMember>(dtlResult.AsQueryable().ToList());
                    dtlDt.TableName = PRINT_DETAIL_TABLE_NAME;

                    if (dsResult.Tables.Contains(hdDt.TableName))
                    {
                        // ２件目以降
                        dsResult.Tables[PRINT_HEADER_TABLE_NAME].Merge(hdDt);
                        dsResult.Tables[PRINT_DETAIL_TABLE_NAME].Merge(dtlDt);

                    }
                    else
                    {
                        // １件目
                        dsResult.Tables.Add(hdDt);
                        dsResult.Tables.Add(dtlDt);

                    }

                }

            }

            return dsResult;

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
            mem.得意先コード = int.Parse(row["得意先コード"].ToString());
            mem.得意先枝番 = int.Parse(row["得意先枝番"].ToString());
            mem.得意先名 = row["得意先名"].ToString();
            mem.回数 = int.Parse(row["回数"].ToString());
            mem.集計期間 = row["集計期間"].ToString();
            mem.当月請求額 = long.Parse(row["当月請求額"].ToString());
            mem.郵便番号 = row["郵便番号"].ToString();
            mem.住所１ = row["住所１"].ToString();
            mem.住所２ = row["住所２"].ToString();
            mem.電話番号 = row["電話番号"].ToString();
            mem.入金日 = int.Parse(row["入金日"].ToString());

            return mem;

        }

        /// <summary>
        /// 指定期間の入金額を返す
        /// </summary>
        /// <param name="context"></param>
        /// <param name="自社コード"></param>
        /// <param name="請求先コード"></param>
        /// <param name="請求先枝番"></param>
        /// <param name="集計期間開始"></param>
        /// <param name="集計期間終了"></param>
        /// <returns></returns>
        private long getNyukinData(TRAC3Entities context, int 自社コード, int 請求先コード, int 請求先枝番, DateTime 集計期間開始, DateTime 集計期間終了)
        {
            var result =
                context.T11_NYKNHD.Where(w =>
                            w.削除日時 == null &&
                            w.入金先自社コード == 自社コード &&
                            w.得意先コード == 請求先コード &&
                            w.得意先枝番 == 請求先枝番 &&
                            w.入金日 >= 集計期間開始 &&
                            w.入金日 <= 集計期間終了)
                    .Join(context.T11_NYKNDTL.Where(w => w.削除日時 == null),
                        x => x.伝票番号,
                        y => y.伝票番号,
                        (x, y) => new { NHD = x, NDTL = y })
                    .Select(x => new
                    {
                        x.NHD.入金先自社コード,
                        x.NHD.得意先コード,
                        x.NHD.得意先枝番,
                        x.NDTL.金額
                    })
                    .GroupBy(g => new { g.入金先自社コード, g.得意先コード, g.得意先枝番 })
                    .Select(x => x.Sum(s => s.金額))
                    .Cast<Int64>()
                    .SingleOrDefault();

            return result;

        }

        #endregion

    }

}