using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 仕入返品サービスクラス
    /// </summary>
    public class DLY01020 : DLY01010
    {
        #region << 定数定義 >>

        /// <summary>仕入ヘッダ テーブル名</summary>
        private const string T03_HEADER_TABLE_NAME = "T03_SRHD";
        /// <summary>仕入明細 テーブル名</summary>
        private const string T03_DETAIL_TABLE_NAME = "T03_SRDTL";
        /// <summary>消費税 テーブル名</summary>
        private const string M73_TABLE_NAME = "M73_ZEI";

        #endregion


        #region 仕入返品情報検索
        /// <summary>
        /// 仕入検索情報を取得する
        /// </summary>
        /// <param name="companyCode">自社コード</param>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        public DataSet ReturnsSearch(string companyCode, string slipNumber, int userId)
        {
            DataSet t03ds = new DataSet();

            List<T03.T03_SRHD_RT_Extension> hdList = getM03_SRHD_RT_Extension(companyCode, slipNumber, userId);
            List<T03.T03_SRDTL_RT_Extension> dtlList = getT03_SRDTL_RT_Extension(slipNumber);
            M73 taxService = new M73();
            List<M73_ZEI> taxList = taxService.GetDataList();

            if (hdList.Count == 0)
                return t03ds;

            // Datatable変換
            DataTable dthd = KESSVCEntry.ConvertListToDataTable(hdList);
            DataTable dtdtl = KESSVCEntry.ConvertListToDataTable(dtlList);
            DataTable dttax = KESSVCEntry.ConvertListToDataTable(taxList);

            dthd.TableName = T03_HEADER_TABLE_NAME;
            t03ds.Tables.Add(dthd);

            dtdtl.TableName = T03_DETAIL_TABLE_NAME;
            t03ds.Tables.Add(dtdtl);

            dttax.TableName = M73_TABLE_NAME;
            t03ds.Tables.Add(dttax);

            return t03ds;

        }
        #endregion

        #region << 仕入返品入力情報取得 >>

        #region 仕入ヘッダ情報(返品)取得
        /// <summary>
        /// 仕入ヘッダ情報を取得する
        /// </summary>
        /// <param name="companyCode">会社名コード</param>
        /// <param name="slipNumber">伝票番号</param>
        /// <param name="userId">ログインユーザID</param>
        /// <returns></returns>
        private List<T03.T03_SRHD_RT_Extension> getM03_SRHD_RT_Extension(string companyCode, string slipNumber, int userId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int code, num;
                if (int.TryParse(companyCode, out code) && int.TryParse(slipNumber, out num))
                {
                    var result =
                        context.T03_SRHD.Where(w => w.削除日時 == null && w.会社名コード == code && w.伝票番号 == num)
                            .GroupJoin(context.T03_SRHD.Where(w => w.削除日時 == null),
                                x => x.元伝票番号,
                                y => y.伝票番号,
                                (x, y) => new { x, y })
                            .SelectMany(s => s.y.DefaultIfEmpty(), (a, b) => new { SRTHD = a.x, SRHD = b })
                            .ToList();

                    if (result.Select(s => s.SRTHD.仕入区分).FirstOrDefault() == CommonConstants.仕入区分.返品.GetHashCode())
                    {
                        // 返品レコードとしてデータを作成
                        var resultRthd =
                            result.Select(x => new T03.T03_SRHD_RT_Extension
                            {
                                伝票番号 = x.SRTHD.伝票番号.ToString(),
                                会社名コード = x.SRTHD.会社名コード.ToString(),
                                仕入日 = x.SRTHD.仕入日,
                                入力区分 = x.SRTHD.入力区分,
                                仕入区分 = x.SRTHD.仕入区分,
                                仕入先コード = x.SRTHD.仕入先コード.ToString(),
                                仕入先枝番 = x.SRTHD.仕入先枝番.ToString(),
                                入荷先コード = x.SRTHD.入荷先コード.ToString(),
                                発注番号 = x.SRTHD.発注番号.ToString(),
                                備考 = x.SRTHD.備考,
                                消費税 = x.SRTHD.消費税,
                                元伝票番号 = x.SRHD == null ? "" : x.SRHD.伝票番号.ToString(),
                                元仕入日 = x.SRHD == null ? (DateTime?)null : x.SRHD.仕入日,
                                データ状態 = false
                            })
                            .ToList();

                        return resultRthd;

                    }
                    else
                    {
                        // 仕入レコードとしてデータを作成
                        // ①新規伝票番号を取得
                        M88 seqSv = new M88();
                        int newSeqNum = seqSv.getNextNumber(CommonConstants.明細番号ID.ID01_売上_仕入_移動, userId);
                        DateTime retDate = (DateTime)AppCommon.DateTimeToDate(DateTime.Now);

                        var resultSrhd =
                            result.Select(x => new T03.T03_SRHD_RT_Extension
                            {
                                伝票番号 = newSeqNum.ToString(),
                                会社名コード = x.SRTHD.会社名コード.ToString(),
                                仕入日 = retDate,
                                入力区分 = x.SRTHD.入力区分,
                                仕入区分 = CommonConstants.仕入区分.返品.GetHashCode(),
                                仕入先コード = x.SRTHD.仕入先コード.ToString(),
                                仕入先枝番 = x.SRTHD.仕入先枝番.ToString(),
                                入荷先コード = x.SRTHD.入荷先コード.ToString(),
                                発注番号 = x.SRTHD.発注番号.ToString(),
                                備考 = x.SRTHD.備考,
                                消費税 = x.SRTHD.消費税,
                                元伝票番号 = x.SRTHD.伝票番号.ToString(),
                                元仕入日 = x.SRTHD.仕入日,
                                データ状態 = true
                            })
                            .ToList();

                        return resultSrhd;

                    }

                }
                else
                {
                    return new List<T03.T03_SRHD_RT_Extension>();

                }

            }

        }
        #endregion

        #region 仕入詳細(返品)情報取得
        /// <summary>
        /// 仕入詳細(返品)情報(品番情報含む)を取得する
        /// </summary>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        private List<T03.T03_SRDTL_RT_Extension> getT03_SRDTL_RT_Extension(string slipNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int num;
                if (int.TryParse(slipNumber, out num))
                {
                    // 元伝票の明細番号を取得
                    var 元伝票番号 =
                        context.T03_SRHD.Where(w => w.削除日時 == null && w.伝票番号 == num)
                            .Select(s => s.元伝票番号)
                            .FirstOrDefault();

                    var result =
                        context.T03_SRDTL.Where(w => w.削除日時 == null && w.伝票番号 == num)
                            .GroupJoin(context.T03_SRDTL.Where(w => w.削除日時 == null && w.伝票番号 == 元伝票番号),
                                x => x.行番号,
                                y => y.行番号,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (a, b) => new { SRTDTL = a.x, SRDTL = b })
                            .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                                x => x.SRTDTL.品番コード,
                                y => y.品番コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(),
                                (c, d) => new { c.x.SRTDTL, c.x.SRDTL, HIN = d })
                            .OrderBy(o => o.SRTDTL.伝票番号)
                            .ThenBy(t => t.SRTDTL.行番号)
                            .ToList();

                    if (context.T03_SRHD.Where(w => w.伝票番号 == num).Select(s => s.仕入区分).FirstOrDefault() == CommonConstants.仕入区分.返品.GetHashCode())
                    {
                        // 返品レコードとしてデータを作成
                        var resultRtDtl =
                            result.Select(x => new T03.T03_SRDTL_RT_Extension
                            {
                                伝票番号 = x.SRTDTL.伝票番号,
                                行番号 = x.SRTDTL.行番号,
                                品番コード = x.SRTDTL.品番コード,
                                自社品番 = x.HIN.自社品番,
                                自社品名 = x.HIN.自社品名,
                                賞味期限 = x.SRTDTL.賞味期限,
                                仕入数量 = x.SRDTL == null ? (decimal?)null : x.SRDTL.数量,
                                数量 = x.SRTDTL.数量,
                                単位 = x.SRTDTL.単位,
                                単価 = x.SRTDTL.単価,
                                金額 = x.SRTDTL.金額,
                                摘要 = x.SRTDTL.摘要,
                                消費税区分 = x.HIN.消費税区分 ?? 0,
                                商品分類 = x.HIN.商品分類 ?? 0
                            })
                            .ToList();

                        return resultRtDtl;

                    }
                    else
                    {
                        // 仕入データとしてデータを作成
                        var resultSrDtl =
                            result.Select(x => new T03.T03_SRDTL_RT_Extension
                            {
                                伝票番号 = x.SRTDTL.伝票番号,
                                行番号 = x.SRTDTL.行番号,
                                品番コード = x.SRTDTL.品番コード,
                                自社品番 = x.HIN.自社品番,
                                自社品名 = x.HIN.自社品名,
                                賞味期限 = x.SRTDTL.賞味期限,
                                仕入数量 = x.SRTDTL.数量,
                                数量 = x.SRTDTL.数量,
                                単位 = x.SRTDTL.単位,
                                単価 = x.SRTDTL.単価,
                                金額 = x.SRTDTL.金額,
                                摘要 = x.SRTDTL.摘要,
                                消費税区分 = x.HIN.消費税区分 ?? 0,
                                商品分類 = x.HIN.商品分類 ?? 0
                            })
                            .ToList();

                        return resultSrDtl;

                    }

                }
                else
                {
                    return new List<T03.T03_SRDTL_RT_Extension>();
                }

            }

        }
        #endregion

        #endregion

        #region 仕入返品入力情報登録・更新
        /// <summary>
        /// 仕入返品入力情報を登録・更新する
        /// </summary>
        /// <param name="ds">
        /// 仕入データセット
        /// [0:T03_SRHD、1:T03_SRDTL]
        /// </param>
        /// <param name="userId">ユーザID</param>
        /// <returns></returns>
        public int ReturnsUpdate(DataSet ds, int userId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    T03Service = new T03(context, userId);
                    S03Service = new S03(context, userId);
                    S04Service = new S04(context, userId, S04.機能ID.仕入返品);

                    try
                    {
                        DataTable hdTable = ds.Tables[T03_HEADER_TABLE_NAME];
                        DataTable dtlTable = ds.Tables[T03_DETAIL_TABLE_NAME];

                        if (hdTable == null || hdTable.Rows.Count == 0)
                            throw new Exception("仕入データの形式が正しくない為、登録処理をおこなう事ができませんでした。");

                        // 1>> ヘッダ情報更新
                        T03_SRHD shd = setT03_SRHD_Update(hdTable.Rows[0]);

                        // 2>> 明細情報更新
                        setT03_SRDTL_Update(shd, dtlTable);

                        // 3>> 在庫情報更新
                        setS03_STOK_Update(shd, dtlTable);

                        // 4>> 入出庫履歴の作成
                        setS04_HISTORY_Create(shd, dtlTable);

                        // 変更状態を確定
                        context.SaveChanges();

                        tran.Commit();

                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }

                }

            }

            return 1;

        }
        #endregion


        #region << 処理関連 >>

        #region << 仕入ヘッダ更新処理 >>

        /// <summary>
        /// 仕入ヘッダ情報の更新をおこなう
        /// </summary>
        /// <param name="row">仕入ヘッダデータ行</param>
        /// <returns></returns>
        protected T03_SRHD setT03_SRHD_Update(DataRow row)
        {
            // 入力情報
            T03_SRHD t03Data = convertDataRowToT03_SRHD_Entity(row);
            // 仕入ヘッダの登録を実行
            T03Service.T03_SRHD_Update(t03Data);

            return t03Data;

        }
        #endregion

        #region << 仕入明細更新処理 >>
        /// <summary>
        /// 仕入明細情報の更新をおこなう
        /// </summary>
        /// <param name="srhd">仕入ヘッダデータ</param>
        /// <param name="dt">仕入明細データテーブル</param>
        /// <returns></returns>
        protected List<T03_SRDTL> setT03_SRDTL_Update(T03_SRHD shd, DataTable dt)
        {
            // 登録済データの削除
            T03Service.T03_SRDTL_DeleteRecords(shd.伝票番号);

            List<T03_SRDTL> resultList = new List<T03_SRDTL>();

            // 明細追加
            foreach (DataRow row in dt.Rows)
            {
                // Del-Insの為削除されたレコードは不要
                if (row.RowState == DataRowState.Deleted)
                    continue;

                T03_SRDTL dtlData = convertDataRowToT03_SRDTL_Entity(row);
                dtlData.伝票番号 = shd.伝票番号;

                if (dtlData.品番コード <= 0)
                    continue;

                // 明細データの登録実行
                T03Service.T03_SRDTL_Update(dtlData);

                resultList.Add(dtlData);

            }

            return resultList;

        }

        #endregion

        #region << 在庫情報(返品)更新処理 >>
        /// <summary>
        /// 在庫情報の更新をおこなう
        /// </summary>
        /// <param name="srhd">仕入ヘッダデータ</param>
        /// <param name="dt">仕入明細データテーブル</param>
        private void setS03_STOK_Update(T03_SRHD srhd, DataTable dt)
        {
            // 会社名から対象の倉庫を取得
            int souk = T03Service.get倉庫コード(srhd.入荷先コード);

            foreach (DataRow row in dt.Rows)
            {
                T03_SRDTL srdtl = convertDataRowToT03_SRDTL_Entity(row);

                // 未入力レコードはスキップ
                if (srdtl.品番コード <= 0)
                    continue;

                // 元伝票からの返品対象外商品なので処理しない
                if (row.RowState == DataRowState.Deleted)
                    continue;

                decimal stockQty = 0;

                // 在庫調整数計算
                if (srhd.仕入区分 == (int)CommonConstants.仕入区分.返品)
                {
                    // 設定数量分を在庫から減算(返品)
                    stockQty = srdtl.数量 * -1;

                }
                else
                {
                    // 上記以外の場合は処理なし
                    continue;
                }

                // 在庫作成・更新
                S03_STOK stok = new S03_STOK();

                stok.倉庫コード = souk;
                stok.品番コード = srdtl.品番コード;
                stok.賞味期限 = AppCommon.DateTimeToDate(srdtl.賞味期限, DateTime.MaxValue);
                stok.在庫数 = stockQty;

                S03Service.S03_STOK_Update(stok);

            }

        }

        #endregion

        #region << 入出庫履歴更新処理 >>
        /// <summary>
        /// 入出庫履歴の登録・更新をおこなう
        /// </summary>
        /// <param name="srhd">仕入ヘッダデータ</param>
        /// <param name="dtlTable">仕入明細データテーブル</param>
        protected void setS04_HISTORY_Create(T03_SRHD srhd, DataTable dtlTable)
        {
            foreach (DataRow row in dtlTable.Rows)
            {
                // 仕入明細データ取得
                T03_SRDTL srdtl = convertDataRowToT03_SRDTL_Entity(row);

                // 商品未設定レコードは処理しない
                if (srdtl.品番コード <= 0)
                    continue;

                // 元伝票からの返品対象外商品なので処理しない
                if (row.RowState == DataRowState.Deleted)
                    continue;

                S04_HISTORY history = new S04_HISTORY();

                history.入出庫日 = srhd.仕入日;
                history.入出庫時刻 = com.GetDbDateTime().TimeOfDay;
                history.倉庫コード = T03Service.get倉庫コード(srhd.入荷先コード);
                history.入出庫区分 = (int)S04Service.getInboundType(row, "数量", srdtl.数量);
                history.品番コード = srdtl.品番コード;
                history.賞味期限 = srdtl.賞味期限;
                history.数量 = Math.Abs(decimal.ToInt32(srdtl.数量));
                history.伝票番号 = srhd.伝票番号;

                Dictionary<string, string> hstDic = new Dictionary<string, string>()
                    {
                        { S04.COLUMNS_NAME_入出庫日, history.入出庫日.ToString("yyyy/MM/dd") },
                        { S04.COLUMNS_NAME_倉庫コード, history.倉庫コード.ToString() },
                        { S04.COLUMNS_NAME_伝票番号, history.伝票番号.ToString() },
                        { S04.COLUMNS_NAME_品番コード, history.品番コード.ToString() }
                    };

                if (row.RowState == DataRowState.Added)
                {
                    // 仕入作成の為、履歴作成
                    S04Service.CreateProductHistory(history);
                }
                else if (row.RowState == DataRowState.Deleted)
                {
                    S04Service.DeleteProductHistory(hstDic);
                }
                else if (row.RowState == DataRowState.Modified)
                {
                    // 仕入更新の為、履歴更新
                    S04Service.UpdateProductHistory(history, hstDic);
                }
                else
                {
                    // 対象なし(DataRowState.Unchanged)
                    continue;
                }

            }

        }
        #endregion


        #endregion

    }

}