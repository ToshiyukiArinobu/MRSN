using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 仕入入力 サービスクラス
    /// </summary>
    public class DLY01010 : BaseService
    {
        #region << 定数定義 >>

        /// <summary>仕入ヘッダ テーブル名</summary>
        private const string T03_HEADER_TABLE_NAME = "T03_SRHD";
        /// <summary>仕入明細 テーブル名</summary>
        private const string T03_DETAIL_TABLE_NAME = "T03_SRDTL";
        /// <summary>消費税 テーブル名</summary>
        private const string M73_TABLE_NAME = "M73_ZEI";

        #endregion

        #region << サービス定義 >>

        /// <summary>仕入情報サービス</summary>
        protected T03 T03Service;
        /// <summary>在庫情報サービス</summary>
        protected S03 S03Service;
        /// <summary>入出庫履歴サービス</summary>
        protected S04 S04Service;

        #endregion


        #region 仕入入力検索情報取得
        /// <summary>
        /// 仕入入力検索情報を取得する
        /// </summary>
        /// <param name="companyCode">自社コード</param>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        public DataSet GetData(string companyCode, string slipNumber, int userId)
        {
            DataSet t03ds = new DataSet();

            M73 taxService = new M73();

            List<T03.T03_SRHD_Extension> hdList = getM03_SRHD_Extension(companyCode, slipNumber);
            List<T03.T03_SRDTL_Extension> dtlList = getT03_SRDTL_Extension(slipNumber);
            List<M73_ZEI> taxList = taxService.GetDataList();

            if (hdList.Count == 0)
            {
                if (string.IsNullOrEmpty(slipNumber))
                {
                    // 伝票番号未入力の場合は新規伝票扱いとする
                    M88 svc = new M88();
                    int code = int.Parse(companyCode);

                    T03.T03_SRHD_Extension hd = new T03.T03_SRHD_Extension();
                    hd.伝票番号 = svc.getNextNumber(CommonConstants.明細番号ID.ID01_売上_仕入_移動, userId);
                    hd.会社名コード = code.ToString();
                    hd.仕入日 = com.GetDbDateTime();

                    hdList.Add(hd);

                }
                else
                {
                    // 指定伝票が見つからない場合
                    return null;

                }

            }

            int rowCnt = 1;
            foreach (T03_SRDTL row in dtlList)
                row.行番号 = rowCnt++;

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

        #region << 仕入入力情報取得 >>

        #region 仕入ヘッダ情報取得
        /// <summary>
        /// 仕入ヘッダ情報を取得する
        /// </summary>
        /// <param name="companyCode">会社名コード</param>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        private List<T03.T03_SRHD_Extension> getM03_SRHD_Extension(string companyCode, string slipNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int code, num;
                if (int.TryParse(companyCode, out code) && int.TryParse(slipNumber, out num))
                {
                    var result = context.T03_SRHD.Where(w => w.削除日時 == null && w.仕入区分 == (int)CommonConstants.仕入区分.通常 && w.会社名コード == code && w.伝票番号 == num)
                                    .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                                        x => new { 取引先コード = x.仕入先コード, 枝番 = x.仕入先枝番 },
                                        y => new { 取引先コード = y.取引先コード, 枝番 = y.枝番 },
                                        (p, q) => new { p, q })
                                    .SelectMany(g => g.q.DefaultIfEmpty(), (a, b) => new { a, b })
                                    .ToList()
                                    .Select(z => new T03.T03_SRHD_Extension
                                    {
                                        伝票番号 = z.a.p.伝票番号,
                                        会社名コード = z.a.p.会社名コード.ToString(),
                                        仕入日 = z.a.p.仕入日,
                                        入力区分 = z.a.p.入力区分,
                                        仕入区分 = z.a.p.仕入区分,
                                        仕入先コード = z.a.p.仕入先コード.ToString(),
                                        仕入先枝番 = z.a.p.仕入先枝番.ToString(),
                                        入荷先コード = z.a.p.入荷先コード.ToString(),
                                        発注番号 = z.a.p.発注番号 != null ? z.a.p.発注番号.ToString() : string.Empty,
                                        備考 = z.a.p.備考,
                                        消費税 = z.a.p.消費税 ?? 0,
                                        Ｓ支払消費税区分 = z.b.Ｓ支払消費税区分,
                                        Ｓ税区分ID = z.b.Ｓ支払区分
                                    });

                    return result.ToList();

                }
                else
                {
                    return new List<T03.T03_SRHD_Extension>();

                }

            }


        }
        #endregion

        #region 仕入明細情報取得
        /// <summary>
        /// 仕入詳細情報(品番情報含む)を取得する
        /// </summary>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        private List<T03.T03_SRDTL_Extension> getT03_SRDTL_Extension(string slipNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int num;
                if (int.TryParse(slipNumber, out num))
                {
                    var result =
                        context.T03_SRDTL.Where(w => w.削除日時 == null && w.伝票番号 == num)
                            .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                                x => x.品番コード,
                                y => y.品番コード,
                                (a, b) => new { a, b })
                            .SelectMany(x => x.b.DefaultIfEmpty(), (x, y) => new { SRDTL = x, HIN = y })
                            .Select(x => new T03.T03_SRDTL_Extension
                            {
                                伝票番号 = x.SRDTL.a.伝票番号,
                                行番号 = x.SRDTL.a.行番号,
                                品番コード = x.SRDTL.a.品番コード,
                                自社品番 = x.HIN.自社品番,
                                自社品名 = x.HIN.自社品名,
                                賞味期限 = x.SRDTL.a.賞味期限,
                                数量 = x.SRDTL.a.数量,
                                単位 = x.SRDTL.a.単位,
                                単価 = x.SRDTL.a.単価,
                                金額 = x.SRDTL.a.金額,
                                摘要 = x.SRDTL.a.摘要,
                                消費税区分 = x.HIN.消費税区分 ?? 0,
                                商品分類 = x.HIN.商品分類 ?? 0
                            })
                            .OrderBy(o => o.伝票番号)
                            .ThenBy(t => t.行番号);

                    return result.ToList();

                }
                else
                {
                    return new List<T03.T03_SRDTL_Extension>();
                }

            }

        }
        #endregion

        #endregion

        #region 仕入入力情報登録・更新
        /// <summary>
        /// 仕入入力情報を登録・更新する
        /// </summary>
        /// <param name="ds">
        /// 仕入データセット
        /// [0:T03_SRHD、1:T03_SRDTL]
        /// </param>
        /// <param name="userId">ユーザID</param>
        /// <returns></returns>
        public int Update(DataSet ds, int userId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    T03Service = new T03(context, userId);
                    S03Service = new S03(context, userId);
                    S04Service = new S04(context, userId, S04.機能ID.仕入入力);

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

        #region 仕入入力情報削除
        /// <summary>
        /// 仕入入力情報の削除をおこなう
        /// </summary>
        /// <param name="slipNumber">伝票番号</param>
        /// <param name="userId">ユーザID</param>
        /// <returns></returns>
        public int Delete(string slipNumber, int userId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    int number = 0;
                    if (!int.TryParse(slipNumber, out number))
                    {
                        S03Service = new S03(context, userId);
                        S04Service = new S04(context, userId, S04.機能ID.仕入入力);

                        try
                        {
                            // ①仕入ヘッダ論理削除
                            T03_SRHD hdData = T03Service.T03_SRHD_Delete(number);

                            // ②仕入明細論理削除
                            List<T03_SRDTL> dtlList = T03Service.T03_SRDTL_Delete(number);

                            // ③在庫更新
                            foreach (T03_SRDTL row in dtlList)
                            {
                                S03_STOK stok = new S03_STOK();
                                stok.倉庫コード = T03Service.get倉庫コード(hdData.会社名コード);
                                stok.品番コード = row.品番コード;
                                stok.賞味期限 = AppCommon.DateTimeToDate(row.賞味期限, DateTime.MaxValue);
                                stok.在庫数 = row.数量 * -1;

                                S03Service.S03_STOK_Update(stok);

                            }

                            // ④入出庫履歴作成
                            DataTable wkTbl = KESSVCEntry.ConvertListToDataTable<T03_SRDTL>(dtlList);
                            foreach (DataRow row in wkTbl.Rows)
                            {
                                // 削除分を判定させる為、RowStateを変更する
                                row.Delete();
                            }
                            setS04_HISTORY_Create(hdData, wkTbl);

                        }
                        catch
                        {
                            tran.Rollback();
                            throw new Exception("削除処理実行中にエラーが発生しました。");

                        }

                    }
                    else
                    {
                        throw new KeyNotFoundException("伝票番号が正しくありません");
                    }

                    tran.Commit();

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
        private T03_SRHD setT03_SRHD_Update(DataRow row)
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
        private List<T03_SRDTL> setT03_SRDTL_Update(T03_SRHD shd, DataTable dt)
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

        #region << 在庫情報更新処理 >>
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

                decimal stockQty = 0;

                // 在庫調整数計算
                if (srhd.仕入区分 == (int)CommonConstants.仕入区分.通常)
                {
                    #region 通常仕入
                    if (row.RowState == DataRowState.Deleted)
                    {
                        // 数量分在庫数を減算
                        if (row.HasVersion(DataRowVersion.Original))
                        {
                            // オリジナルが存在する場合はその数量を優先する
                            stockQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]);
                        }
                        else
                            stockQty = srdtl.数量;

                        // 仕入減算なので反転させる
                        stockQty = stockQty * -1;

                    }
                    else if (row.RowState == DataRowState.Added)
                    {
                        // 数量分在庫数を加算
                        stockQty = srdtl.数量;

                    }
                    else if (row.RowState == DataRowState.Modified)
                    {
                        // オリジナル(変更前数量)と比較して差分数量を加減算
                        if (row.HasVersion(DataRowVersion.Original))
                        {
                            decimal orgQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]);
                            stockQty = srdtl.数量 - orgQty;

                        }

                    }
                    else
                    {
                        // 対象なし(DataRowState.Unchanged)
                        continue;
                    }
                    #endregion

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
        private void setS04_HISTORY_Create(T03_SRHD srhd, DataTable dtlTable)
        {
            foreach (DataRow row in dtlTable.Rows)
            {
                // 仕入明細データ取得
                T03_SRDTL srdtl = convertDataRowToT03_SRDTL_Entity(row);

                // 商品未設定レコードは処理しない
                if (srdtl.品番コード <= 0)
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


        #region DataRow to EntityClass
        /// <summary>
        /// DataRow型をT03_SRHDに変換する
        /// </summary>
        /// <param name="drow"></param>
        /// <returns></returns>
        protected T03_SRHD convertDataRowToT03_SRHD_Entity(DataRow drow)
        {
            T03_SRHD srhd = new T03_SRHD();
            int ival = 0;

            srhd.伝票番号 = ParseNumeric<int>(drow["伝票番号"]);
            srhd.会社名コード = ParseNumeric<int>(drow["会社名コード"]);
            if (drow["仕入日"] != null && !string.IsNullOrEmpty(drow["仕入日"].ToString()))
                srhd.仕入日 = DateTime.Parse(string.Format("{0:yyyy/MM/dd}", drow["仕入日"]));
            srhd.入力区分 = ParseNumeric<int>(drow["入力区分"]);
            srhd.仕入区分 = ParseNumeric<int>(drow["仕入区分"]);
            srhd.仕入先コード = ParseNumeric<int>(drow["仕入先コード"]);
            srhd.仕入先枝番 = ParseNumeric<int>(drow["仕入先枝番"]);
            srhd.入荷先コード = ParseNumeric<int>(drow["入荷先コード"]);
            srhd.発注番号 = int.TryParse(drow["発注番号"].ToString(), out ival) ? ival : (int?)null;
            srhd.備考 = drow["備考"].ToString();
            // REMARKS:カラムが無い場合は参照しない
            srhd.元伝票番号 = drow.Table.Columns.Contains("元伝票番号") ?
                (int.TryParse(drow["元伝票番号"].ToString(), out ival) ? ival : (int?)null) : (int?)null;
            srhd.消費税 = ParseNumeric<int>(drow["消費税"]);

            return srhd;

        }

        /// <summary>
        /// DataRow型をT03_SRDTLに変換する
        /// </summary>
        /// <param name="drow"></param>
        /// <returns></returns>
        protected T03_SRDTL convertDataRowToT03_SRDTL_Entity(DataRow drow)
        {
            T03_SRDTL srdtl = new T03_SRDTL();
            DataRow wkRow = drow.Table.Clone().NewRow();

            if (drow.RowState == DataRowState.Deleted)
            {   // 対象が削除行の場合
                // 対象データの参照ができるようにする
                DataTable wkTbl = drow.Table.Copy();
                wkTbl.RejectChanges();

                var orgCode = drow["品番コード", DataRowVersion.Original];

                foreach (DataRow dr in wkTbl.Select(string.Format("品番コード = {0}", orgCode)))
                {
                    wkRow.ItemArray = dr.ItemArray;
                    break;  // 複数取る事はないと思うが念の為
                }

            }
            else
            {
                wkRow.ItemArray = drow.ItemArray;
            }

            srdtl.伝票番号 = ParseNumeric<int>(wkRow["伝票番号"]);
            srdtl.行番号 = ParseNumeric<int>(wkRow["行番号"]);
            srdtl.品番コード = ParseNumeric<int>(wkRow["品番コード"]);
            if (wkRow["賞味期限"] != null && string.IsNullOrEmpty(wkRow["賞味期限"].ToString()))
                srdtl.賞味期限 = null;
            else
                srdtl.賞味期限 = DateTime.Parse(wkRow["賞味期限"].ToString());
            srdtl.数量 = ParseNumeric<decimal>(wkRow["数量"]);
            srdtl.単位 = wkRow["単位"].ToString();
            srdtl.単価 = ParseNumeric<decimal>(wkRow["単価"]);
            srdtl.金額 = ParseNumeric<int>(wkRow["金額"]);
            srdtl.摘要 = wkRow["摘要"].ToString();

            return srdtl;

        }
        #endregion

        #endregion

    }

}