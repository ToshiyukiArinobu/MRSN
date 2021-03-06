﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 商品移動/振替入力サービスクラス
    /// </summary>
    public class DLY04010 : BaseService
    {
        #region 定数定義

        /// <summary>ヘッダテーブル名</summary>
        private const string TABLE_HEADER = "T05_IDOHD";
        /// <summary>明細テーブル名</summary>
        private const string TABLE_DETAIL = "T05_IDODTL";

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
            // No-154 Add Start
            public string 自社色 { get; set; }
            public string 自社色名 { get; set; }
            // No-154 Add Start
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

            List<T05_IDOHD_Extension> hdList = getM05_IDOHD_Extension(companyCode, slipNumber);
            List<T05_IDODTL_Extension> dtlList = getT05_IDODTL_Extension(slipNumber);

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
                    hd.移動区分 = CommonConstants.移動区分.通常移動.GetHashCode();

                    hdList.Add(hd);

                }
                else
                {
                    // 入力アリの場合は番号誤りなので検索を終了する
                    return null;
                }

            }

            int rowCnt = 1;
            foreach (T05_IDODTL_Extension row in dtlList)
                row.行番号 = rowCnt++;

            // Datatable変換
            DataTable dthd = KESSVCEntry.ConvertListToDataTable(hdList);
            DataTable dtdtl = KESSVCEntry.ConvertListToDataTable(dtlList);

            //// 不足レコードの補充
            //T05_IDOHD idohd = convertDataRowToT05_IDOHD_Entity(dthd.Rows[0]);
            //for (int i = 10 - dtdtl.Rows.Count; i >= 0; i--)
            //{
            //    DataRow row = dtdtl.NewRow();
            //    row["伝票番号"] = idohd.伝票番号;
            //    row["行番号"] = 10 - i;

            //    dtdtl.Rows.Add(row);

            //}

            dthd.TableName = TABLE_HEADER;
            t05ds.Tables.Add(dthd);

            dtdtl.TableName = TABLE_DETAIL;
            t05ds.Tables.Add(dtdtl);

            return t05ds;

        }
        #endregion

        #region 移動情報登録更新
        /// <summary>
        /// 移動入力情報を登録・更新する
        /// </summary>
        /// <param name="ds">
        /// 移動データセット
        /// [0:T04_AGRHD、1:T04_AGRDTL]
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
                    S04Service = new S04(context, userId, S04.機能ID.商品移動入力);

                    try
                    {
                        DataRow hdRow = ds.Tables[TABLE_HEADER].Rows[0];
                        T05_IDOHD idohd = convertDataRowToT05_IDOHD_Entity(hdRow);
                        DataTable dtlTbl = ds.Tables[TABLE_DETAIL];

                        // 1>> ヘッダ情報更新
                        setT05_IDOHD_Update(idohd);

                        // 2>> 明細情報更新
                        setT05_IDODTL_Update(idohd, dtlTbl);

                        // 3>> 在庫情報更新
                        // (出荷元からの引落し)
                        if (idohd.出荷元倉庫コード != null)
                        {
                            setS03_STOK_Update(context, idohd, dtlTbl, true);           // No-258 Mod
                            setS04_HISTORY_Update(context, idohd, dtlTbl, true);
                        }

                        // (出荷先への積上げ)
                        if (idohd.出荷先倉庫コード != null)
                        {
                            setS03_STOK_Update(context, idohd, dtlTbl, false);           // No-258 Mod
                            setS04_HISTORY_Update(context, idohd, dtlTbl, false);
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

        #endregion

        #region << 移動ヘッダ情報 >>

        /// <summary>
        /// 移動ヘッダ情報を取得する
        /// </summary>
        /// <returns></returns>
        private List<T05_IDOHD> getM05_IDOHDList()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var result = context.T05_IDOHD
                                .Where(w => w.削除日時 == null);

                return result.ToList();

            }

        }

        /// <summary>
        /// 移動ヘッダ情報を取得する
        /// </summary>
        /// <param name="companyCode">会社名コード</param>
        /// <returns></returns>
        private List<T05_IDOHD> getM05_IDOHD(string companyCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int code;
                if (int.TryParse(companyCode, out code))
                {
                    var result = context.T05_IDOHD.Where(w => w.削除日時 == null && w.会社名コード == code);

                    return result.ToList();

                }
                else
                {
                    return new List<T05_IDOHD>();
                }

            }

        }

        /// <summary>
        /// 移動ヘッダ情報を取得する
        /// </summary>
        /// <param name="companyCode">会社名コード</param>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        private List<T05_IDOHD> getM05_IDOHD(string companyCode, string slipNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int code, num;
                if (int.TryParse(companyCode, out code) && int.TryParse(slipNumber, out num))
                {
                    var result = context.T05_IDOHD.Where(w => w.削除日時 == null && w.会社名コード == code && w.伝票番号 == num);

                    return result.ToList();

                }
                else
                {
                    return new List<T05_IDOHD>();
                }

            }

        }

        /// <summary>
        /// 移動ヘッダ情報を取得する
        /// </summary>
        /// <param name="companyCode">会社名コード</param>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        private List<T05_IDOHD_Extension> getM05_IDOHD_Extension(string companyCode, string slipNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int code, num;
                if (int.TryParse(companyCode, out code) && int.TryParse(slipNumber, out num))
                {
                    var result =
                        context.T05_IDOHD
                            .Where(w => w.削除日時 == null && w.会社名コード == code && w.伝票番号 == num)
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
        /// 移動明細情報を取得する
        /// </summary>
        /// <returns></returns>
        private List<T05_IDODTL> getT05_IDODTLList()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var result = context.T05_IDODTL
                                .Where(w => w.削除日時 == null)
                                .OrderBy(o => o.伝票番号)
                                .ThenBy(t => t.行番号);

                return result.ToList();

            }

        }

        /// <summary>
        /// 移動明細情報を取得する
        /// </summary>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        private List<T05_IDODTL> getT05_IDODTL(string slipNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int num;
                if (int.TryParse(slipNumber, out num))
                {
                    var result = context.T05_IDODTL
                                    .Where(w => w.削除日時 == null && w.伝票番号 == num)
                                    .OrderBy(o => o.伝票番号)
                                    .ThenBy(t => t.行番号);

                    return result.ToList();

                }
                else
                {
                    return new List<T05_IDODTL>();
                }

            }

        }

        /// <summary>
        /// 移動明細情報(品番情報含む)を取得する
        /// </summary>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        private List<T05_IDODTL_Extension> getT05_IDODTL_Extension(string slipNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int num;
                if (int.TryParse(slipNumber, out num))
                {
                    var result =
                        // No-154 Mod Start
                        context.T05_IDODTL.Where(w => w.削除日時 == null && w.伝票番号 == num)
                            .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                                x => x.品番コード,
                                y => y.品番コード,
                                (a, b) => new { a, b })
                            .SelectMany(x => x.b.DefaultIfEmpty(), (x, y) => new { IDODTL = x, HIN = y })
                            .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null), x => x.HIN.自社色, y => y.色コード, (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(), (c, d) => new { c.x.IDODTL, c.x.HIN, IRO = d })
                            .Select(x => new T05_IDODTL_Extension
                            {
                                伝票番号 = x.IDODTL.a.伝票番号,
                                行番号 = x.IDODTL.a.行番号,
                                品番コード = x.IDODTL.a.品番コード,
                                自社品番 = x.HIN.自社品番,
                                自社品名 = x.HIN.自社品名,
                                自社色 = x.HIN.自社色,
                                自社色名 = x.IRO != null ? x.IRO.色名称 : string.Empty,
                                賞味期限 = x.IDODTL.a.賞味期限,
                                数量 = x.IDODTL.a.数量,
                                摘要 = x.IDODTL.a.摘要,
                                消費税区分 = x.HIN.消費税区分 ?? 0,
                                商品分類 = x.HIN.商品分類 ?? 0
                            })
                            .OrderBy(o => o.伝票番号)
                            .ThenBy(t => t.行番号);
                    // No-154 Mod End

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
        private void setT05_IDODTL_Update(T05_IDOHD idohd, DataTable dt)
        {
            // 登録済みデータの物理削除(del-ins)
            T05Service.T05_IDODTL_DeleteRecords(idohd.伝票番号);

            int rowIdx = 1;
            // 明細追加
            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;

                T05_IDODTL dtlData = convertDataRowToT05_IDODTL_Entity(row);

                if (dtlData.品番コード <= 0)
                    continue;

                T05_IDODTL srdtl = new T05_IDODTL();
                srdtl.伝票番号 = dtlData.伝票番号;
                srdtl.行番号 = rowIdx;
                srdtl.品番コード = dtlData.品番コード;
                srdtl.賞味期限 = dtlData.賞味期限;
                srdtl.数量 = dtlData.数量;
                srdtl.摘要 = dtlData.摘要;

                T05Service.T05_IDODTL_Update(srdtl);

                rowIdx++;

            }

        }
        #endregion

        #endregion

        #region << 在庫更新 >>

        /// <summary>
        /// 在庫情報の更新をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="idohd">移動ヘッダデータ</param>
        /// <param name="dtlTbl">移動明細データテーブル</param>
        /// <param name="isSubtract">減算フラグ(True:減算,False:減算しない)</param>
        private void setS03_STOK_Update(TRAC3Entities context, T05_IDOHD idohd, DataTable dtlTbl, bool isSubtract)          // No-258 Mod
        {
            foreach (DataRow row in dtlTbl.Rows)
            {
                T05_IDODTL dtlRow = convertDataRowToT05_IDODTL_Entity(row);

                if (dtlRow.品番コード <= 0)
                    continue;

                int souko = isSubtract ? (int)idohd.出荷元倉庫コード : (int)idohd.出荷先倉庫コード;

                #region 在庫数計算
                decimal stockQty = 0;
                decimal oldstockQty = 0;
                bool iskigenChangeFlg = false;    // 賞味期限変更フラグ    No-258 Add

                if (row.RowState == DataRowState.Deleted)
                {
                    // 数量分在庫数を加減算
                    stockQty = dtlRow.数量 * (isSubtract ? -1 : 1);
                }
                else if (row.RowState == DataRowState.Added)
                {
                    // 数量分在庫数を加減算
                    stockQty = dtlRow.数量 * (isSubtract ? -1 : 1);
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
                    // No-258 Mod End

                }
                else
                {
                    // 対象なし(DataRowState.Unchanged)
                    continue;
                }
                #endregion

                // No-258 Add Start
                // 賞味期限が変更された場合
                if (iskigenChangeFlg == true)
                {
                    DateTime dt;
                    S03_STOK oldStok = new S03_STOK();
                    oldStok.倉庫コード = souko;
                    oldStok.品番コード = dtlRow.品番コード;
                    oldStok.賞味期限 = row["賞味期限", DataRowVersion.Original] == DBNull.Value ? AppCommon.DateTimeToDate(null, DateTime.MaxValue) :
                                        DateTime.TryParse(row["賞味期限", DataRowVersion.Original].ToString(), out dt) ? dt : AppCommon.DateTimeToDate(null, DateTime.MaxValue);
                    oldStok.在庫数 = oldstockQty;

                    // 旧賞味期限の在庫を更新
                    S03Service.S03_STOK_Update(oldStok);
                }
                // No-258 Add End

                // 対象データ設定
                S03_STOK stok = new S03_STOK();
                stok.倉庫コード = souko;
                stok.品番コード = dtlRow.品番コード;
                stok.賞味期限 = AppCommon.DateTimeToDate(dtlRow.賞味期限, DateTime.MaxValue);
                stok.在庫数 = stockQty;

                // 新賞味期限の在庫を更新
                S03Service.S03_STOK_Update(stok);
                
                // 変更状態を確定
                context.SaveChanges();              // No-258 Add

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
            // No-258 Mod Start
            if (isSubtract == true)
            {
                // 登録済み入出庫データの削除
                int intSlipNumber = idohd.伝票番号;
                // 入出庫データの物理削除
                S04Service.PhysicalDeletionProductHistory(context, intSlipNumber, (int)S04.機能ID.商品移動入力);
            }

            // 不要レコード除去
            DataTable dtlTblTmp = dtlTbl.Clone();
            foreach (DataRow row in dtlTbl.Rows)
            {
                T05_IDODTL dtlRow = convertDataRowToT05_IDODTL_Entity(row);

                if (dtlRow.品番コード <= 0)
                {
                    continue;
                }

                dtlTblTmp.ImportRow(row);
            }

            // 入出庫データ作成単位に集約    
            var dtlTblWk = dtlTblTmp.AsEnumerable()
                            .Where(x => x.RowState != DataRowState.Deleted)
                            .GroupBy(g => new { 伝票番号 = g.Field<int>("伝票番号")
                                                , 品番コード = g.Field<int>("品番コード")
                                                , 賞味期限 = g.Field<DateTime?>("賞味期限") })
                            .Select(s => new T05_IDODTL
                            {
                                伝票番号 = s.Key.伝票番号,
                                品番コード = s.Key.品番コード,
                                賞味期限 = s.Key.賞味期限,
                                数量 = s.Sum(m => m.Field<decimal>("数量"))
                            })
                            .ToList();

            foreach (T05_IDODTL row in dtlTblWk)
            {
                int souko = isSubtract ? (int)idohd.出荷元倉庫コード : (int)idohd.出荷先倉庫コード;

                decimal stockQtyhist = 0;                               // No-155 Add
                stockQtyhist = row.数量;
                S04_HISTORY history = new S04_HISTORY();

                history.入出庫日 = idohd.日付;
                history.入出庫時刻 = com.GetDbDateTime().TimeOfDay;
                history.倉庫コード = souko;
                history.入出庫区分 = !isSubtract ? (int)CommonConstants.入出庫区分.ID01_入庫 : (int)CommonConstants.入出庫区分.ID02_出庫;
                history.品番コード = row.品番コード;
                history.賞味期限 = row.賞味期限;
                history.数量 = decimal.ToInt32(stockQtyhist);         // No-155 Mod
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
            // No-258 Mod End
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
            T05_IDODTL agrdtl = new T05_IDODTL();
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

            agrdtl.伝票番号 = ParseNumeric<int>(wkRow["伝票番号"]);
            agrdtl.行番号 = ParseNumeric<int>(wkRow["行番号"]);
            agrdtl.品番コード = ParseNumeric<int>(wkRow["品番コード"]);
            if (wkRow["賞味期限"] != null && string.IsNullOrEmpty(wkRow["賞味期限"].ToString()))
                agrdtl.賞味期限 = null;
            else
                agrdtl.賞味期限 = DateTime.Parse(wkRow["賞味期限"].ToString());
            agrdtl.数量 = ParseNumeric<decimal>(wkRow["数量"]);
            agrdtl.摘要 = wkRow["摘要"].ToString();

            return agrdtl;

        }

        #endregion

        #region << 在庫チェック >>
        // No-222 Add Start

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

                DataTable dt = ds.Tables[TABLE_DETAIL];
                List<T05_IDODTL> dtlList = getDetailDataList(dt);

                // 入荷先から対象の倉庫を取得

                Common com = new Common();
                int intSouk = int.Parse(strStoreHouseCode);

                foreach (T05_IDODTL row in dtlList)
                {
                    if (string.IsNullOrEmpty(row.品番コード.ToString()))
                    {
                        continue;
                    }

                    decimal nowStockQty = 0;
                    if (!com.CheckStokItemQty(intSouk, row.品番コード, row.賞味期限, out nowStockQty, row.数量))
                    {
                        // キー：行番号、値：エラーメッセージ
                        resultDic.Add(row.行番号, string.Format("在庫数が不足しています。(現在庫数：{0:#,0.##})", nowStockQty));
                    }

                }

                return resultDic;
            }
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
        // No-222 Add End
        #endregion

    }

}
