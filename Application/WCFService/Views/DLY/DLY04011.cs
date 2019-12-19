using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 商品振替入力サービスクラス
    /// </summary>
    public class DLY04011 : BaseService
    {
        #region 定数定義

        /// <summary>ヘッダテーブル名</summary>
        private const string TABLE_HEADER = "T05_IDOHD";
        /// <summary>明細テーブル名</summary>
        private const string SYUKO_TABLE_DETAIL = "T05_SYUKO_IDODTL";

        private const string NYUKO_TABLE_DETAIL = "T05_NYUKO_IDODTL";

        #endregion

        #region 列挙型定義

        /// <summary>
        /// 商品分類 内包データ
        /// </summary>
        private enum 商品分類 : int
        {
            食品 = 1,
            繊維 = 2,
            その他 = 3
        }

        #endregion

        #region 拡張クラス定義

        /// <summary>
        /// 移動ヘッダ情報拡張クラス
        /// </summary>
        public class T05_IDOHD_Extension
        {
            public int 伝票番号 { get; set; }
            public int 会社名コード { get; set; }
            public DateTime 日付 { get; set; }
            public int 移動区分 { get; set; }
            public string 出荷元倉庫コード { get; set; }
            public string 出荷先倉庫コード { get; set; }
        }

        /// <summary>
        /// 移動明細情報拡張クラス
        /// </summary>
        public class T05_IDODTL_Extension : T05_IDODTL
        {
            // REMARKS:Entityを基本に不足情報を補完する
            public string 自社品番 { get; set; }
            public string 自社品名 { get; set; }
            /// <summary>0:対象外、1:対象</summary>
            public int 消費税区分 { get; set; }
            /// <summary>1:食品、2:繊維、3:その他</summary>
            public int 商品分類 { get; set; }
            public string 自社色 { get; set; }
            public string 自社色名 { get; set; }
        }

        #endregion

        #region << サービス定義 >>

        /// <summary>移動情報サービス</summary>
        T05 T05Service;
        /// <summary>在庫情報サービス</summary>
        S03 S03Service;
        /// <summary>入出庫履歴サービス</summary>
        S04 S04Service;

        #endregion

        #region << 移動入力関連 >>

        #region 移動情報検索
        /// <summary>
        /// 移動検索情報を取得する
        /// </summary>
        /// <param name="companyCode">自社コード(会社名コード)</param>
        /// <param name="slipNumber">伝票番号</param>
        /// <param name="userId">ログインユーザID</param>
        /// <returns></returns>
        public DataSet GetData(string companyCode, string slipNumber, int userId)
        {
            DataSet t05ds = new DataSet();

            List<T05_IDOHD_Extension> hdList = getT05_IDOHD_Extension(companyCode, slipNumber);
            List<T05_IDODTL_Extension> dtlList = getT05_IDODTL_Extension(companyCode, slipNumber);

            if (hdList.Count == 0)
            {
                if (string.IsNullOrEmpty(slipNumber))
                {
                    // 未入力の場合は新規伝票として作成
                    M88 svc = new M88();
                    int code = int.Parse(companyCode);

                    // 新規伝票番号を取得して設定
                    T05_IDOHD_Extension hd = new T05_IDOHD_Extension();
                    hd.伝票番号 = svc.getNextNumber(CommonConstants.明細番号ID.ID01_売上_仕入_移動, userId);
                    hd.会社名コード = code;
                    hd.日付 = DateTime.Now;
                    hd.移動区分 = CommonConstants.移動区分.振替移動.GetHashCode();

                    hdList.Add(hd);
                }
                else
                {
                    // 入力アリの場合は番号誤りなので検索を終了する
                    return null;
                }
            }

            // Datatableに変換
            DataTable dthd = KESSVCEntry.ConvertListToDataTable(hdList);
            DataTable dtInDtl = KESSVCEntry.ConvertListToDataTable(dtlList.Where(c => c.行番号 == 1).ToList());
            DataTable dtOutDtl = KESSVCEntry.ConvertListToDataTable(dtlList.Where(c => c.行番号 == 2).ToList());

            // テーブル名を設定
            dthd.TableName = TABLE_HEADER;
            dtInDtl.TableName = NYUKO_TABLE_DETAIL;
            dtOutDtl.TableName = SYUKO_TABLE_DETAIL;

            // データセットに追加
            t05ds.Tables.Add(dthd);
            t05ds.Tables.Add(dtInDtl);
            t05ds.Tables.Add(dtOutDtl);

            return t05ds;

        }
        #endregion

        #endregion

        #region << 移動ヘッダ情報 >>

        /// <summary>
        /// 移動ヘッダ情報を取得する
        /// </summary>
        /// <param name="companyCode">会社名コード</param>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        private List<T05_IDOHD_Extension> getT05_IDOHD_Extension(string companyCode, string slipNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int code, num;
                if (int.TryParse(companyCode, out code) && int.TryParse(slipNumber, out num))
                {
                    var result =
                        context.T05_IDOHD
                            .Where(w => w.削除日時 == null && w.会社名コード == code && w.伝票番号 == num && w.移動区分 == 4)
                            .ToList()
                            .Select(x => new T05_IDOHD_Extension
                            {
                                伝票番号 = x.伝票番号,
                                会社名コード = x.会社名コード,
                                日付 = x.日付,
                                移動区分 = x.移動区分,
                                出荷元倉庫コード = x.出荷元倉庫コード.ToString(),
                                出荷先倉庫コード = x.出荷先倉庫コード.ToString()
                            });

                    return result.ToList();

                }
                else
                {
                    return new List<T05_IDOHD_Extension>();
                }
            }
        }

        #region 移動ヘッダ情報更新
        /// <summary>
        /// 移動ヘッダ情報の更新をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="hdRow"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private void setT05_IDOHD_Update(T05_IDOHD t05Data)
        {
            // データなしの為追加
            T05_IDOHD idohd = new T05_IDOHD();

            idohd.伝票番号 = t05Data.伝票番号;
            idohd.会社名コード = t05Data.会社名コード;
            idohd.日付 = t05Data.日付;
            idohd.移動区分 = t05Data.移動区分;
            idohd.出荷元倉庫コード = t05Data.出荷元倉庫コード;
            idohd.出荷先倉庫コード = t05Data.出荷先倉庫コード;

            T05Service.T05_IDOHD_Update(idohd);

        }
        #endregion

        #endregion

        #region << 移動明細情報  >>

        /// <summary>
        /// 移動明細情報(品番情報含む)を取得する
        /// </summary>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        private List<T05_IDODTL_Extension> getT05_IDODTL_Extension(string companyCode, string slipNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int num;
                int code;
                if (int.TryParse(companyCode, out code) && int.TryParse(slipNumber, out num))
                {
                    var result =
                        context.T05_IDOHD
                            .Where(w => w.削除日時 == null && w.会社名コード == code && w.伝票番号 == num && w.移動区分 == 4)
                            .Join(context.T05_IDODTL.Where(w => w.削除日時 == null),
                                x => x.伝票番号,
                                y => y.伝票番号,
                                (x, y) => new { IDOHD = x, IDODTL = y })
                            .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                                x => x.IDODTL.品番コード,
                                y => y.品番コード,
                                (a, b) => new { a, b })
                            .SelectMany(x => x.b.DefaultIfEmpty(), (x, y) => new { x.a.IDOHD, x.a.IDODTL, HIN = y })
                            .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null),
                                x => x.HIN.自社色,
                                y => y.色コード,
                                (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(), (c, d) => new { c.x.IDOHD, c.x.IDODTL, c.x.HIN, IRO = d })
                            .Select(x => new T05_IDODTL_Extension
                            {
                                伝票番号 = x.IDODTL.伝票番号,
                                行番号 = x.IDODTL.行番号,
                                品番コード = x.IDODTL.品番コード,
                                自社品番 = x.HIN.自社品番,
                                自社品名 = x.HIN.自社品名,
                                自社色 = x.HIN.自社色,
                                自社色名 = x.IRO != null ? x.IRO.色名称 : string.Empty,
                                賞味期限 = x.IDODTL.賞味期限,
                                数量 = x.IDODTL.数量,
                                摘要 = x.IDODTL.摘要,
                                消費税区分 = x.HIN.消費税区分 ?? 0,
                                商品分類 = x.HIN.商品分類 ?? 0
                            })
                            .OrderBy(o => o.伝票番号)
                            .ThenBy(t => t.行番号);
                    return result.ToList();

                }
                else
                {
                    return new List<T05_IDODTL_Extension>();
                }

            }

        }

        #region 移動明細情報更新
        /// <summary>
        /// 移動明細情報の更新をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dt"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private void setT05_IDODTL_Update(T05_IDOHD idohd, List<T05_IDODTL> idoDtl)
        {
            // 登録済みデータの物理削除(del-ins)
            T05Service.T05_IDODTL_DeleteRecords(idohd.伝票番号);

            // 入庫の摘要を出庫にもセット
            string p摘要 = idoDtl.Where(c => c.行番号 == 1).Select(c => c.摘要).FirstOrDefault();

            // 明細追加
            foreach (var row in idoDtl)
            {

                if (row.品番コード <= 0)
                    continue;

                T05_IDODTL updRow = new T05_IDODTL();
                updRow.伝票番号 = row.伝票番号;
                updRow.行番号 = row.行番号;
                updRow.品番コード = row.品番コード;
                updRow.賞味期限 = row.賞味期限;
                updRow.数量 = row.数量;
                updRow.摘要 = p摘要;

                T05Service.T05_IDODTL_Update(updRow);
            }

        }
        #endregion

        #endregion

        #region 移動情報登録更新
        /// <summary>
        /// 移動入力情報を登録・更新する
        /// </summary>
        /// <param name="ds">
        /// 移動データセット
        /// [0:T05_IDOHD、1:T05_IDODTL]
        /// </param>
        /// <param name="userId">ログインユーザID</param>
        /// <returns></returns>
        public void Update(DataSet ds, int userId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    T05Service = new T05(context, userId);
                    S03Service = new S03(context, userId);
                    S04Service = new S04(context, userId, S04.機能ID.振替入力);

                    try
                    {
                        DataRow hdRow = ds.Tables[TABLE_HEADER].Rows[0];
                        T05_IDOHD 振替Hd = convertDataRowToT05_IDOHD_Entity(hdRow);
                        DataTable 振替出庫Dtl = ds.Tables[SYUKO_TABLE_DETAIL];
                        DataTable 振替入庫Dtl = ds.Tables[NYUKO_TABLE_DETAIL];


                        List<T05_IDODTL> updList = new List<T05_IDODTL>();
                        updList.Add(convertDataRowToT05_IDODTL_Entity(振替出庫Dtl.Rows[0]));
                        updList.Add(convertDataRowToT05_IDODTL_Entity(振替入庫Dtl.Rows[0]));

                        // 1>> ヘッダ情報更新
                        setT05_IDOHD_Update(振替Hd);

                        // 2>> 明細情報更新
                        setT05_IDODTL_Update(振替Hd, updList);

                        // 3>> 在庫情報更新
                        // (出荷元からの引落し)
                        if (振替Hd.出荷元倉庫コード != null)
                        {
                            setS03_STOK_Update(context, hdRow, 振替出庫Dtl, true);
                            setS04_HISTORY_Update(context, 振替Hd, 振替出庫Dtl, true);
                        }

                        // (出荷先への積上げ)
                        if (振替Hd.出荷先倉庫コード != null)
                        {
                            setS03_STOK_Update(context, hdRow, 振替入庫Dtl, false);
                            setS04_HISTORY_Update(context, 振替Hd, 振替入庫Dtl, false);
                        }

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

        }
        #endregion

        #region 移動情報削除
        /// <summary>
        /// 移動入力情報を削除する
        /// </summary>
        /// <param name="ds">
        /// 移動データセット
        /// [0:T05_IDOHD、1:T05_IDODTL]
        /// </param>
        /// <param name="userId">ログインユーザID</param>
        /// <returns></returns>
        public void Delete(string slipNumber, int userId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    T05Service = new T05(context, userId);
                    S03Service = new S03(context, userId);
                    S04Service = new S04(context, userId, S04.機能ID.振替入力);

                    int number = 0;
                    if (int.TryParse(slipNumber, out number))
                    {

                        try
                        {
                            var idohd =
                               context.T05_IDOHD
                                   .Where(x => x.削除日時 == null &&
                                       x.伝票番号 == number)
                                        .FirstOrDefault();

                            var idodtl =
                               context.T05_IDODTL
                                   .Where(x => x.削除日時 == null &&
                                       x.伝票番号 == number)
                                        .ToList();

                            // 伝票データが存在しない場合は処理しない。
                            if (idodtl == null || idodtl.Count == 0) return;

                            // 1>> ヘッダ情報削除
                            T05Service.T05_IDOHD_Delete(number);

                            // 2>> 明細情報削除
                            T05Service.T05_IDODTL_Delete(number);

                            // 3>> 在庫情報削除
                            #region 在庫調整

                            foreach (T05_IDODTL row in idodtl)
                            {

                                int souko = row.行番号 == 2 ? (int)idohd.出荷元倉庫コード : (int)idohd.出荷先倉庫コード;

                                // 対象データ設定
                                S03_STOK stok = new S03_STOK();
                                stok.倉庫コード = souko;
                                stok.品番コード = row.品番コード;
                                stok.賞味期限 = AppCommon.DateTimeToDate(row.賞味期限, DateTime.MaxValue);
                                stok.在庫数 = row.行番号 == 2 ? row.数量 : row.数量 * (-1);

                                // 旧賞味期限の在庫を更新
                                S03Service.S03_STOK_Update(stok);

                                // 変更状態を確定
                                context.SaveChanges();

                            }

                            #endregion

                            // 4>> 入出庫データの物理削除
                            S04Service.PhysicalDeletionProductHistory(context, number, (int)S04.機能ID.振替入力);

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
                    else
                    {
                        throw new KeyNotFoundException("伝票番号が正しくありません");
                    }

                }

            }

        }
        #endregion

        #region << 在庫更新 >>

        /// <summary>
        /// 在庫情報の更新をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="idohd">移動ヘッダデータ</param>
        /// <param name="dtlTbl">移動明細データテーブル</param>
        /// <param name="isSubtract">減算フラグ(True:減算,False:減算しない)</param>
        private void setS03_STOK_Update(TRAC3Entities context, DataRow hdTbl, DataTable dtlTbl, bool isSubtract)
        {

            T05_IDOHD idohd = convertDataRowToT05_IDOHD_Entity(hdTbl);

            foreach (DataRow row in dtlTbl.Rows)
            {
                T05_IDODTL dtlRow = convertDataRowToT05_IDODTL_Entity(row);

                if (dtlRow.品番コード <= 0)
                    continue;

                int souko = isSubtract ? (int)idohd.出荷元倉庫コード : (int)idohd.出荷先倉庫コード;

                #region 在庫数計算
                decimal stockQty = 0;
                decimal oldstockQty = 0;
                bool isSamekigenFlg = false;    // 賞味期限同一フラグ 
                bool isSameSoukFlg = false;    // 倉庫同一フラグ
                bool isSameSyohinFlg = false;    // 商品同一フラグ

                if (hdTbl.RowState == DataRowState.Modified)
                {
                    isSameSoukFlg = isSubtract ? CheckSameValue(hdTbl, "出荷元倉庫コード") : CheckSameValue(hdTbl, "出荷先倉庫コード");
                }

                if (row.RowState == DataRowState.Added)
                {
                    // 数量分在庫数を加減算
                    stockQty = dtlRow.数量 * (isSubtract ? -1 : 1);
                }
                else if (row.RowState == DataRowState.Modified || (hdTbl.RowState == DataRowState.Modified && !isSameSoukFlg))
                {
                    // 明細を変更または前回と倉庫が異なる場合
                    isSamekigenFlg = CheckSameValue(row, "賞味期限");
                    isSameSyohinFlg = CheckSameValue(row, "品番コード");

                    // 賞味期限、倉庫、商品が変更された場合、過去在庫を取得
                    if (!isSamekigenFlg || !isSameSoukFlg || !isSameSyohinFlg)
                    {
                        // 　減算フラグ(True:減算)の場合：旧賞味期限の在庫を加算、新賞味期限の在庫を減算する
                        // 　減算フラグ(True:False)の場合：旧賞味期限の在庫を減算、新賞味期限の在庫を加算する

                        // 旧賞味期限の在庫数
                        oldstockQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]) * (isSubtract ? 1 : -1);
                        // 新賞味期限の在庫数
                        stockQty = dtlRow.数量 * (isSubtract ? -1 : 1);
                    }
                    else
                    {
                        // 数量が変更された場合
                        decimal orgQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]);
                        stockQty = (dtlRow.数量 - orgQty) * (isSubtract ? -1 : 1);
                    }

                }
                else
                {
                    // 明細対象なし(DataRowState.Unchanged)
                    continue;
                }
                #endregion

                // 賞味期限、倉庫、商品が変更された場合
                if ((row.RowState == DataRowState.Modified && (!isSamekigenFlg || !isSameSoukFlg)) || (hdTbl.RowState == DataRowState.Modified && !isSameSyohinFlg))
                {
                    DateTime dt;
                    S03_STOK oldStok = new S03_STOK();
                    oldStok.倉庫コード = isSubtract ? int.Parse(hdTbl["出荷元倉庫コード", DataRowVersion.Original].ToString()) : int.Parse(hdTbl["出荷先倉庫コード", DataRowVersion.Original].ToString());
                    oldStok.品番コード = int.Parse(row["品番コード", DataRowVersion.Original].ToString());
                    oldStok.賞味期限 = row["賞味期限", DataRowVersion.Original] == DBNull.Value ? AppCommon.DateTimeToDate(null, DateTime.MaxValue) :
                                        DateTime.TryParse(row["賞味期限", DataRowVersion.Original].ToString(), out dt) ? dt : AppCommon.DateTimeToDate(null, DateTime.MaxValue);
                    oldStok.在庫数 = oldstockQty;

                    // 旧賞味期限の在庫を更新
                    S03Service.S03_STOK_Update(oldStok);
                }

                // 対象データ設定
                S03_STOK stok = new S03_STOK();
                stok.倉庫コード = souko;
                stok.品番コード = dtlRow.品番コード;
                stok.賞味期限 = AppCommon.DateTimeToDate(dtlRow.賞味期限, DateTime.MaxValue);
                stok.在庫数 = stockQty;

                // 新賞味期限の在庫を更新
                S03Service.S03_STOK_Update(stok);

                // 変更状態を確定
                context.SaveChanges();

            }

        }

        /// <summary>
        /// 入出庫履歴の登録・更新をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="idohd">移動ヘッダデータ</param>
        /// <param name="dtlTbl">移動明細データテーブル</param>
        /// <param name="orghd">変更前仕入ヘッダデータ</param>
        /// <param name="isSubtract">減算フラグ(True:減算,False:減算しない)</param>
        private void setS04_HISTORY_Update(TRAC3Entities context, T05_IDOHD idohd, DataTable dtlTbl, bool isSubtract)
        {

            if (isSubtract == true)
            {
                // 登録済み入出庫データの削除
                int intSlipNumber = idohd.伝票番号;
                // 入出庫データの物理削除
                S04Service.PhysicalDeletionProductHistory(context, intSlipNumber, (int)S04.機能ID.振替入力);
            }

            List<T05_IDODTL> t05dtl = getDetailDataList(dtlTbl);

            foreach (T05_IDODTL row in t05dtl)
            {
                int souko = isSubtract ? (int)idohd.出荷元倉庫コード : (int)idohd.出荷先倉庫コード;

                S04_HISTORY history = new S04_HISTORY();
                history.入出庫日 = idohd.日付;
                history.入出庫時刻 = com.GetDbDateTime().TimeOfDay;
                history.倉庫コード = souko;
                history.入出庫区分 = !isSubtract ? (int)CommonConstants.入出庫区分.ID01_入庫 : (int)CommonConstants.入出庫区分.ID02_出庫;
                history.品番コード = row.品番コード;
                history.賞味期限 = row.賞味期限;
                history.数量 = decimal.ToInt32(row.数量);
                history.伝票番号 = row.伝票番号;

                Dictionary<string, string> hstDic = new Dictionary<string, string>()
                    {
                        { S04.COLUMNS_NAME_入出庫日, history.入出庫日.ToString("yyyy/MM/dd") },
                        { S04.COLUMNS_NAME_倉庫コード, history.倉庫コード.ToString() },
                        { S04.COLUMNS_NAME_伝票番号, history.伝票番号.ToString() },
                        { S04.COLUMNS_NAME_品番コード, history.品番コード.ToString() },
                    };

                // 履歴作成
                S04Service.CreateProductHistory(history);
            }
        }

        #endregion

        #region << 在庫チェック >>

        /// <summary>
        /// 在庫数チェックを行う
        /// </summary>
        /// <param name="strStoreHouseCode">倉庫コード</param>
        /// <param name="ds">データセット</param>
        /// <param name="intUserId">ユーザID</param>
        /// <returns></returns>
        public Dictionary<int, string> CheckStockQty(string strStoreHouseCode, DataSet ds, int intUserId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {

                Dictionary<int, string> resultDic = new Dictionary<int, string>();

                DataRow hdRow = ds.Tables[TABLE_HEADER].Rows[0];
                DataRow outDtRow = ds.Tables[SYUKO_TABLE_DETAIL].Rows[0];
                DataRow inDtRow = ds.Tables[NYUKO_TABLE_DETAIL].Rows[0];


                // 入荷先から対象の倉庫を取得
                Common com = new Common();
                int intSouk = int.Parse(strStoreHouseCode);

                // 出庫在庫チェック
                T05_IDODTL outRow = convertDataRowToT05_IDODTL_Entity(outDtRow);
                T05_IDODTL inRow = convertDataRowToT05_IDODTL_Entity(inDtRow);

                decimal sNowStockQty = 0;
                decimal outStockQty = 0;
                decimal preOutStockQty = 0;

                // 数量のみ変更する場合、前回数量を加味する
                if (CheckSameValue(hdRow, "出荷元倉庫コード") && CheckSameValue(outDtRow, "賞味期限") && CheckSameValue(outDtRow, "品番コード"))
                {
                    preOutStockQty = ParseNumeric<decimal>(outDtRow["数量", DataRowVersion.Original]);
                }

                outStockQty = outRow.数量 - preOutStockQty;

                if (!com.CheckStokItemQty(intSouk, outRow.品番コード, outRow.賞味期限, out sNowStockQty, outStockQty))
                {
                    // キー：行番号、値：エラーメッセージ
                    resultDic.Add(outRow.行番号, string.Format("出庫の在庫数が不足しています。(現在庫数：{0:#,0.##})", sNowStockQty));
                }

                // 在庫を減らす場合,入庫在庫チェック(編集時)
                if (hdRow.RowState == DataRowState.Modified || inDtRow.RowState == DataRowState.Modified)
                {


                    int org倉庫コード = ParseNumeric<int>(hdRow["出荷先倉庫コード", DataRowVersion.Original]);
                    int org品番コード = ParseNumeric<int>(inDtRow["品番コード", DataRowVersion.Original]);
                    DateTime? org賞味期限 = inDtRow["賞味期限", DataRowVersion.Original] != null && string.IsNullOrEmpty(inDtRow["賞味期限", DataRowVersion.Original].ToString())
                        ? org賞味期限 = null : org賞味期限 = DateTime.Parse(inDtRow["賞味期限", DataRowVersion.Original].ToString());

                    decimal inStockQty = 0;
                    decimal preInStockQty = 0;

                    // 数量のみ変更する場合、前回数量を加味する
                    if (CheckSameValue(hdRow, "出荷先倉庫コード") && CheckSameValue(inDtRow, "賞味期限") && CheckSameValue(inDtRow, "品番コード"))
                    {
                        preInStockQty = ParseNumeric<decimal>(outDtRow["数量", DataRowVersion.Original]);
                    }

                    inStockQty = preInStockQty - inRow.数量;

                    decimal nNowStockQty = 0;
                    if (inStockQty > 0 && !com.CheckStokItemQty(org倉庫コード, org品番コード, org賞味期限, out nNowStockQty, inStockQty))
                    {
                        // キー：行番号、値：エラーメッセージ
                        resultDic.Add(inRow.行番号, string.Format("入庫の在庫数が不足しています。(現在庫数：{0:#,0.##})", nNowStockQty));
                    }
                }

                return resultDic;
            }
        }


        /// <summary>
        /// 削除時に入庫戻しの在庫数チェックを行う
        /// </summary>
        /// <param name="strStoreHouseCode">倉庫コード</param>
        /// <param name="ds">データセット</param>
        /// <param name="intUserId">ユーザID</param>
        /// <returns></returns>
        public Dictionary<int, string> CheckDeleteStockQty(int number)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {

                Dictionary<int, string> resultDic = new Dictionary<int, string>();

                // 入庫戻しの在庫チェック
                Common com = new Common();

                var idohd =
                   context.T05_IDOHD
                       .Where(x => x.削除日時 == null &&
                              x.伝票番号 == number)
                              .FirstOrDefault();

                var idodtl =
                   context.T05_IDODTL
                       .Where(x => x.削除日時 == null &&
                              x.伝票番号 == number && x.行番号 == 1)
                              .FirstOrDefault();

                if (idodtl == null) return resultDic;

                decimal nNowStockQty = 0;
                if (!com.CheckStokItemQty((int)idohd.出荷先倉庫コード, idodtl.品番コード, idodtl.賞味期限, out nNowStockQty, idodtl.数量))
                {
                    // キー：行番号、値：エラーメッセージ
                    resultDic.Add(idodtl.行番号, string.Format("入庫の在庫数が不足しています。(現在庫数：{0:#,0.##})", nNowStockQty));
                }

                return resultDic;

            }
        }


        #endregion

        #region << 処理関連 >>

        /// <summary>
        /// DataRow型をT05_IDOHDエンティティに変換する
        /// </summary>
        /// <param name="drow"></param>
        /// <returns></returns>
        private T05_IDOHD convertDataRowToT05_IDOHD_Entity(DataRow drow)
        {
            T05_IDOHD agrhd = new T05_IDOHD();
            int iVal;

            agrhd.伝票番号 = ParseNumeric<int>(drow["伝票番号"]);
            agrhd.会社名コード = ParseNumeric<int>(drow["会社名コード"]);
            agrhd.日付 = DateTime.Parse(string.Format("{0:yyyy/MM/dd}", drow["日付"]));
            agrhd.移動区分 = ParseNumeric<int>(drow["移動区分"]);
            agrhd.出荷元倉庫コード = int.TryParse(drow["出荷元倉庫コード"].ToString(), out iVal) ? (int?)iVal : null;
            agrhd.出荷先倉庫コード = int.TryParse(drow["出荷先倉庫コード"].ToString(), out iVal) ? (int?)iVal : null;

            return agrhd;

        }

        /// <summary>
        /// DataRow型をT05_IDODTLに変換する
        /// </summary>
        /// <param name="drow"></param>
        /// <returns></returns>
        private T05_IDODTL convertDataRowToT05_IDODTL_Entity(DataRow drow)
        {
            T05_IDODTL t05dtl = new T05_IDODTL();

            t05dtl.伝票番号 = ParseNumeric<int>(drow["伝票番号"]);
            t05dtl.行番号 = ParseNumeric<int>(drow["行番号"]);
            t05dtl.品番コード = ParseNumeric<int>(drow["品番コード"]);
            if (drow["賞味期限"] != null && string.IsNullOrEmpty(drow["賞味期限"].ToString()))
            {
                t05dtl.賞味期限 = null;
            }
            else
            {
                t05dtl.賞味期限 = DateTime.Parse(drow["賞味期限"].ToString());
            }
            t05dtl.数量 = ParseNumeric<decimal>(drow["数量"]);
            t05dtl.摘要 = drow["摘要"].ToString();

            return t05dtl;

        }

        /// <summary>
        /// T05_IDODTLデータ型変換(DataTable→List)
        /// </summary>
        /// <param name="DataTable">データテーブル</param>
        /// <returns></returns>        
        private List<T05_IDODTL> getDetailDataList(DataTable dt)
        {
            var resultList =
                dt.Select("", "", DataViewRowState.CurrentRows).AsEnumerable()
                .Where(c => !string.IsNullOrEmpty(c.Field<string>("自社品番")))
                    .Select(s => new T05_IDODTL
                    {
                        伝票番号 = s.Field<int>("伝票番号"),
                        行番号 = s.Field<int>("行番号"),
                        品番コード = s.Field<int>("品番コード"),
                        賞味期限 = s.Field<DateTime?>("賞味期限"),
                        数量 = s.Field<decimal>("数量"),
                        摘要 = s.Field<string>("摘要")
                    })
                    .ToList();

            return resultList;

        }

        /// <summary>
        /// 編集時、対象の項目が前回の値と同じかチェック
        /// </summary>
        /// <param name="dr">DataRow</param>
        /// <param name="drColumn">項目名</param>
        /// <returns></returns>

        private bool CheckSameValue(DataRow dr, string drColumn)
        {
            bool result = false;

            if (dr.RowState == DataRowState.Modified)
            {
                result = dr[drColumn].Equals(dr[drColumn, DataRowVersion.Original]);
            }

            return result;

        }

        #endregion

    }

}
