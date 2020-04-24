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
        /// <summary>確定データ テーブル名</summary>
        private const string S11_TABLE_NAME = "S11_KAKUTEI";

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
            List<DLY03010.S11_KAKUTEI_INFO> fixList = getS11_KAKUTEI_Extension(hdList);

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
            DataTable dtfix = KESSVCEntry.ConvertListToDataTable(fixList);

            dthd.TableName = T03_HEADER_TABLE_NAME;
            t03ds.Tables.Add(dthd);

            dtdtl.TableName = T03_DETAIL_TABLE_NAME;
            t03ds.Tables.Add(dtdtl);

            dttax.TableName = M73_TABLE_NAME;
            t03ds.Tables.Add(dttax);

            dtfix.TableName = S11_TABLE_NAME;
            t03ds.Tables.Add(dtfix);

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
                                        // No-94 Add Start
                                        通常税率対象金額 = z.a.p.通常税率対象金額 ?? 0,
                                        軽減税率対象金額 = z.a.p.軽減税率対象金額 ?? 0,
                                        通常税率消費税 = z.a.p.通常税率消費税 ?? 0,
                                        軽減税率消費税 = z.a.p.軽減税率消費税 ?? 0,
                                        // No-94 Add End
                                        // No-95 Add Start
                                        小計 = z.a.p.小計 ?? 0,
                                        総合計 = z.a.p.総合計 ?? 0,
                                        // No-95 Add End
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
                        // No-59 Add Start
                            .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null),
                                x => x.HIN.自社色,
                                y => y.色コード,
                                (c, d) => new { c.SRDTL, c.HIN, d })
                            .SelectMany(x => x.d.DefaultIfEmpty(), (x, y) => new { x.SRDTL, x.HIN, IRO = y })
                        // No-59 Add End

                            .Select(x => new T03.T03_SRDTL_Extension
                            {
                                伝票番号 = x.SRDTL.a.伝票番号,
                                行番号 = x.SRDTL.a.行番号,
                                品番コード = x.SRDTL.a.品番コード,
                                自社品番 = x.HIN.自社品番,
                                自社品名 = !string.IsNullOrEmpty(x.SRDTL.a.自社品名) ? x.SRDTL.a.自社品名 : x.HIN.自社品名,                // No.390 Mod
                                賞味期限 = x.SRDTL.a.賞味期限,
                                数量 = x.SRDTL.a.数量,
                                単位 = x.SRDTL.a.単位,
                                単価 = x.SRDTL.a.単価,
                                金額 = x.SRDTL.a.金額,
                                税区分 =           // No-94 Add
                                    x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率 ? CommonConstants.消費税区分略称_軽減税率 :
                                    x.HIN.消費税区分 == (int)CommonConstants.商品消費税区分.非課税 ? CommonConstants.消費税区分略称_非課税 : string.Empty,
                                摘要 = x.SRDTL.a.摘要,
                                消費税区分 = x.HIN.消費税区分 ?? 0,
                                商品分類 = x.HIN.商品分類 ?? 0,

                                // 20190705CB-S
                                自社色 = x.HIN.自社色,                        // No-59 Mod
                                自社色名 = x.IRO.色名称                       // No-59 Mod
                                // 20190705CB-E

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

        #region 確定情報を取得する
        /// <summary>
        /// 確定情報を取得する
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="urhdList"></param>
        /// <returns></returns>
        private List<DLY03010.S11_KAKUTEI_INFO> getS11_KAKUTEI_Extension(List<T03.T03_SRHD_Extension> hdList)
        {
            if (!hdList.Any())
            {
                return new List<DLY03010.S11_KAKUTEI_INFO>();
            }

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int val;
                int company = int.TryParse(hdList[0].会社名コード, out val) ? val : -1;
                int code = int.TryParse(hdList[0].仕入先コード, out val) ? val : -1;
                int eda = int.TryParse(hdList[0].仕入先枝番, out val) ? val : -1;
                List<int> kbnList = new List<int>() { (int)CommonConstants.取引区分.仕入先, (int)CommonConstants.取引区分.相殺 };

                // 取引先情報
                var tokData = context.M01_TOK.Where(w => w.取引先コード == code && w.枝番 == eda && w.削除日時 == null);

                // 仕入先確定データ
                var shiFix =
                    tokData
                        .GroupJoin(context.S11_KAKUTEI.Where(w => w.確定区分 == (int)CommonConstants.確定区分.支払),
                            x => new { jis = x.担当会社コード, tKbn = x.取引区分, closeDay = (int)x.Ｓ締日 },
                            y => new { jis = y.自社コード, tKbn = y.取引区分, closeDay = y.締日 },
                            (x, y) => new { x, y })
                        .SelectMany(z => z.y.DefaultIfEmpty(),
                            (a, b) => new { TOK_S = a.x, FIX_S = b })
                        .Select(s => new DLY03010.S11_KAKUTEI_INFO
                        {
                            取引先コード = s.TOK_S.取引先コード,
                            枝番 = s.TOK_S.枝番,
                            取引区分 = s.TOK_S.取引区分,
                            確定区分 = s.FIX_S.確定区分,
                            確定日 = s.FIX_S.確定日
                        })
                        .Union
                        (tokData.Where(w => w.取引区分 == (int)CommonConstants.取引区分.相殺)
                        .GroupJoin(context.S11_KAKUTEI.Where(w => w.確定区分 == (int)CommonConstants.確定区分.請求),
                            x => new { jis = x.担当会社コード, tKbn = x.取引区分, closeDay = (int)x.Ｔ締日 },
                            y => new { jis = y.自社コード, tKbn = y.取引区分, closeDay = y.締日 },
                            (x, y) => new { x, y })
                        .SelectMany(z => z.y.DefaultIfEmpty(),
                            (c, d) => new { TOK_SO = c.x, FIX_SO = d })
                        .Select(s => new DLY03010.S11_KAKUTEI_INFO
                        {
                            取引先コード = s.TOK_SO.取引先コード,
                            枝番 = s.TOK_SO.枝番,
                            取引区分 = s.TOK_SO.取引区分,
                            確定区分 = s.FIX_SO.確定区分,
                            確定日 = s.FIX_SO.確定日
                        }));

                return shiFix.ToList();
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
                        setS03_STOK_Update(context, shd, dtlTable);         // No-258 Mod

                        // 4>> 入出庫履歴の作成
                        setS04_HISTORY_Create(context, shd, dtlTable, hdTable.Rows[0]);  // No.156-1 Mod,No-258 Mod

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
                    if (int.TryParse(slipNumber, out number))
                    {
                        T03Service = new T03(context, userId);      // No.156-1 Add
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
                            wkTbl.AcceptChanges();      // No.156-1 Add
                            foreach (DataRow row in wkTbl.Rows)
                            {
                                // 削除分を判定させる為、RowStateを変更する
                                row.Delete();
                            }
                            setS04_HISTORY_Create(context, hdData, wkTbl, null);     // No.156-1 Mod,No-258 Mod

                            // 変更状態を確定
                            context.SaveChanges();      // No.156-1 Add
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
        /// <param name="context"></param>
        /// <param name="srhd">仕入ヘッダデータ</param>
        /// <param name="dt">仕入明細データテーブル</param>
        private void setS03_STOK_Update(TRAC3Entities context, T03_SRHD srhd, DataTable dt)         // No-258 Mod
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
                decimal oldstockQty = 0;            // 変更前数量            No-258 Add
                bool iskigenChangeFlg = false;      // 賞味期限変更フラグ    No-258 Add

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
                        // No-258 Mod Start
                        if (!row["賞味期限"].Equals(row["賞味期限", DataRowVersion.Original]))
                        {
                            // 賞味期限が変更された場合
                            // 　減算フラグ(True:減算)の場合：旧賞味期限の在庫を加算、新賞味期限の在庫を減算する
                            // 　減算フラグ(True:False)の場合：旧賞味期限の在庫を減算、新賞味期限の在庫を加算する
                            iskigenChangeFlg = true;
                            // 旧賞味期限の在庫数
                            oldstockQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]) * -1;
                            // 新賞味期限の在庫数
                            stockQty = srdtl.数量;
                        }
                        else
                        {
                            // 数量が変更された場合
                            decimal orgQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]);
                            stockQty = srdtl.数量 - orgQty;
                        }
                        // No-258 Mod End
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

                // No-258 Add Start
                // 賞味期限が変更された場合
                if (iskigenChangeFlg == true)
                {
                    DateTime dtKigen;
                    S03_STOK oldStok = new S03_STOK();
                    oldStok.倉庫コード = souk;
                    oldStok.品番コード = srdtl.品番コード;
                    oldStok.賞味期限 = row["賞味期限", DataRowVersion.Original] == DBNull.Value ? AppCommon.DateTimeToDate(null, DateTime.MaxValue) :
                                        DateTime.TryParse(row["賞味期限", DataRowVersion.Original].ToString(), out dtKigen) ? dtKigen : AppCommon.DateTimeToDate(null, DateTime.MaxValue);
                    oldStok.在庫数 = oldstockQty;

                    // 旧賞味期限の在庫を更新
                    S03Service.S03_STOK_Update(oldStok);
                }
                // No-258 Add End

                // 在庫作成・更新
                S03_STOK stok = new S03_STOK();
                stok.倉庫コード = souk;
                stok.品番コード = srdtl.品番コード;
                stok.賞味期限 = AppCommon.DateTimeToDate(srdtl.賞味期限, DateTime.MaxValue);
                stok.在庫数 = stockQty;

                S03Service.S03_STOK_Update(stok);

                // 変更状態を確定
                context.SaveChanges();              // No-258 Add
            }

        }

        #endregion

        #region << 入出庫履歴更新処理 >>
        /// <summary>
        /// 入出庫履歴の登録・更新をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="srhd">仕入ヘッダデータ</param>
        /// <param name="dtlTable">仕入明細データテーブル</param>
        /// <param name="orghd">変更前仕入ヘッダデータ</param>
        private void setS04_HISTORY_Create(TRAC3Entities context, T03_SRHD srhd, DataTable dtlTable, DataRow orghd)     // No-258 Mod
        {
            // No-258 Mod Start
            // 登録済み入出庫データの削除
            int intSlipNumber = srhd.伝票番号;
            // 入出庫データの物理削除
            S04Service.PhysicalDeletionProductHistory(context, intSlipNumber, (int)S04.機能ID.仕入入力);

            // 不要レコード除去
            DataTable dtlTblTmp = dtlTable.Clone();
            foreach (DataRow row in dtlTable.Rows)
            {
                T03_SRDTL dtlRow = convertDataRowToT03_SRDTL_Entity(row);

                if (dtlRow.品番コード <= 0)
                {
                    continue;
                }

                dtlTblTmp.ImportRow(row);
            }

            // 入出庫データ作成単位に集約    
            var dtlTblWk = dtlTblTmp.AsEnumerable()
                            .Where(x => x.RowState != DataRowState.Deleted)
                            .GroupBy(g => new
                            {
                                伝票番号 = g.Field<int>("伝票番号"),
                                品番コード = g.Field<int>("品番コード"),
                                賞味期限 = g.Field<DateTime?>("賞味期限")
                            })
                            .Select(s => new T03_SRDTL
                            {
                                伝票番号 = s.Key.伝票番号,
                                品番コード = s.Key.品番コード,
                                賞味期限 = s.Key.賞味期限,
                                数量 = s.Sum(m => m.Field<decimal>("数量"))
                            })
                            .ToList();

            foreach (T03_SRDTL row in dtlTblWk)
            {
                decimal stockQtyhist = 0;                               // No-155 Add
                stockQtyhist = row.数量;
                S04_HISTORY history = new S04_HISTORY();

                history.入出庫日 = srhd.仕入日;
                history.入出庫時刻 = com.GetDbDateTime().TimeOfDay;
                history.倉庫コード = T03Service.get倉庫コード(srhd.入荷先コード);
                history.入出庫区分 = (int)CommonConstants.入出庫区分.ID01_入庫;
                history.品番コード = row.品番コード;
                history.賞味期限 = row.賞味期限;
                history.数量 = decimal.ToInt32(row.数量);
                history.伝票番号 = srhd.伝票番号;

                Dictionary<string, string> hstDic = new Dictionary<string, string>()
                        {
                            // No.156-1 Mod Start
                            { S04.COLUMNS_NAME_入出庫日, orghd == null? 
                                                            history.入出庫日.ToString("yyyy/MM/dd") : string.Format("{0:yyyy/MM/dd}", orghd["仕入日", DataRowVersion.Original])},
                            { S04.COLUMNS_NAME_倉庫コード, orghd == null? 
                                                            history.倉庫コード.ToString() : 
                                                            orghd["入荷先コード", DataRowVersion.Original] == DBNull.Value? 
                                                            null : T03Service.get倉庫コード(Convert.ToInt32(orghd["入荷先コード", DataRowVersion.Original])).ToString() },
                            { S04.COLUMNS_NAME_伝票番号, orghd == null? 
                                                            history.伝票番号.ToString() : orghd["伝票番号", DataRowVersion.Original].ToString() },
                            { S04.COLUMNS_NAME_品番コード,  history.品番コード.ToString() }
                            // No.156-1 Mod End
                        };

                // 履歴作成
                S04Service.CreateProductHistory(history);
            }
            // No-258 Mod End

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
            // No-94 Add Start
            srhd.通常税率対象金額 = ParseNumeric<int>(drow["通常税率対象金額"]);
            srhd.軽減税率対象金額 = ParseNumeric<int>(drow["軽減税率対象金額"]);
            srhd.通常税率消費税 = ParseNumeric<int>(drow["通常税率消費税"]);
            srhd.軽減税率消費税 = ParseNumeric<int>(drow["軽減税率消費税"]);
            // No-94 Add End
            // No-95 Add Start
            srhd.小計 = ParseNumeric<int>(drow["小計"]);
            srhd.総合計 = ParseNumeric<int>(drow["総合計"]);
            // No-95 Add End
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
            srdtl.自社品名 = wkRow["自社品名"].ToString();
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