using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    using System.Data;
    using Const = CommonConstants;

    /// <summary>
    /// 出金情報サービスクラス
    /// </summary>
    public class T12
    {
        #region 定数定義

        /// <summary>ヘッダテーブル名</summary>
        private const string TABLE_HEADER = "T12_PAYHD";
        /// <summary>明細テーブル名</summary>
        private const string TABLE_DETAIL = "T12_PAYDTL";

        /// <summary>明細の最大行数</summary>
        private const int MAX_ROW_COUNT = 10;

        #endregion

        #region << 拡張クラス定義 >>

        public class T12_PAYNHD_Extension
        {
            public int 伝票番号 { get; set; }
            public string 出金元自社コード { get; set; }
            public DateTime 出金日 { get; set; }
            public string 出金先販社コード { get; set; }
            public string 得意先コード { get; set; }
            public string 得意先枝番 { get; set; }
        }

        public class T12_PAYDTL_Extension
        {
            public int 伝票番号 { get; set; }
            public int 行番号 { get; set; }
            public string 金種コード { get; set; }
            public int 金額 { get; set; }
            public DateTime? 期日 { get; set; }
            public string 摘要 { get; set; }
        }

        #endregion


        #region << 出金入力関連 >>

        /// <summary>
        /// 支払入力 検索情報を取得する
        /// </summary>
        /// <param name="companyCode">会社名コード(出金先自社コード)</param>
        /// <param name="slipNumber">伝票番号</param>
        /// <param name="userId">ログインユーザID</param>
        /// <returns></returns>
        public DataSet GetData(string companyCode, string slipNumber, int userId)
        {
            DataSet t11ds = new DataSet();

            List<T12_PAYNHD_Extension> hdList = getS02_PAYHD_Extension(companyCode, slipNumber);
            List<T12_PAYDTL_Extension> dtlList = getS02_PAYDTL_Extension(slipNumber);

            if (hdList.Count == 0)
            {
                if (string.IsNullOrEmpty(slipNumber))
                {
                    // 伝票番号未入力の場合は新規伝票扱いとして作成
                    M88 svc = new M88();
                    int code = int.Parse(companyCode);

                    // 新規伝票番号を取得して設定
                    T12_PAYNHD_Extension hd = new T12_PAYNHD_Extension();
                    hd.伝票番号 = svc.getNextNumber(Const.明細番号ID.ID06_出金, userId);
                    hd.出金元自社コード = companyCode;
                    hd.出金日 = DateTime.Now;

                    hdList.Add(hd);

                }
                else
                {
                    // 未登録伝票なので検索終了
                    return null;
                }

            }

            int rowCnt = 1;
            foreach (T12_PAYDTL_Extension row in dtlList)
                row.行番号 = rowCnt++;

            // Datatable変換
            DataTable dthd = KESSVCEntry.ConvertListToDataTable(hdList);
            DataTable dtdtl = KESSVCEntry.ConvertListToDataTable(dtlList);

            dthd.TableName = TABLE_HEADER;
            t11ds.Tables.Add(dthd);

            dtdtl.TableName = TABLE_DETAIL;
            t11ds.Tables.Add(dtdtl);

            return t11ds;

        }

        /// <summary>
        /// 支払入力情報を登録・更新する
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int Update(DataSet ds, int userId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    try
                    {
                        // 1>> ヘッダ情報更新
                        DataRow hdRow = ds.Tables[TABLE_HEADER].Rows[0];

                        setT12_PAYHD_Update(context, hdRow, userId);

                        // 2>> 明細情報更新
                        setS02_PAYDTL_Update(context, ds.Tables[TABLE_DETAIL], userId);

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

        /// <summary>
        /// 出金入力情報を論理削除する
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int Delete(DataSet ds, int userId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    try
                    {
                        // 1>> ヘッダ情報論理削除
                        DataRow hdRow = ds.Tables[TABLE_HEADER].Rows[0];

                        setT12_PAYHD_Delete(context, hdRow, userId);

                        // 2>> 明細情報論理削除
                        setT12_PAYDTL_Delete(context, ds.Tables[TABLE_DETAIL], userId);

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


        #region << 出金ヘッダ >>

        /// <summary>
        /// 出金ヘッダ情報を取得する
        /// </summary>
        /// <param name="companyCode">会社名コード(出金元自社コード)</param>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        private List<T12_PAYNHD_Extension> getS02_PAYHD_Extension(string companyCode, string slipNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int code, num;
                if (int.TryParse(companyCode, out code) && int.TryParse(slipNumber, out num))
                {
                    var result =
                        context.T12_PAYHD
                            .Where(w => w.削除日時 == null && w.出金元自社コード == code && w.伝票番号 == num)
                            .ToList()
                            .Select(x => new T12_PAYNHD_Extension
                            {
                                伝票番号 = x.伝票番号,
                                出金元自社コード = x.出金元自社コード.ToString(),
                                出金日 = x.出金日,
                                出金先販社コード = x.出金先販社コード.ToString(),
                                得意先コード = x.得意先コード.ToString(),
                                得意先枝番 = x.得意先枝番.ToString()
                            });

                    return result.ToList();

                }
                else
                {
                    return new List<T12_PAYNHD_Extension>();
                }

            }

        }

        /// <summary>
        /// 出金ヘッダ情報の更新をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="hdRow"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private int setT12_PAYHD_Update(TRAC3Entities context, DataRow hdRow, int userId)
        {
            // 入力情報
            T12_PAYHD t12Data = convertDataRowToT12_PAYHD_Entity(hdRow);

            // 登録データ取得
            var hdData = context.T12_PAYHD
                            .Where(w => w.伝票番号 == t12Data.伝票番号)
                            .FirstOrDefault();

            if (hdData == null)
            {
                // データなしの為追加
                T12_PAYHD nyknhd = new T12_PAYHD();

                nyknhd.伝票番号 = t12Data.伝票番号;
                nyknhd.出金元自社コード  = t12Data.出金元自社コード;
                nyknhd.出金日 = t12Data.出金日;
                nyknhd.出金先販社コード = t12Data.出金先販社コード;
                nyknhd.得意先コード = t12Data.得意先コード;
                nyknhd.得意先枝番 = t12Data.得意先枝番;
                nyknhd.登録者 = userId;
                nyknhd.登録日時 = DateTime.Now;
                nyknhd.最終更新者 = userId;
                nyknhd.最終更新日時 = DateTime.Now;

                context.T12_PAYHD.ApplyChanges(nyknhd);

            }
            else
            {
                // データを更新
                hdData.伝票番号 = t12Data.伝票番号;
                hdData.出金元自社コード = t12Data.出金元自社コード;
                hdData.出金日 = t12Data.出金日;
                hdData.出金先販社コード = t12Data.出金先販社コード;
                hdData.得意先コード = t12Data.得意先コード;
                hdData.得意先枝番 = t12Data.得意先枝番;
                hdData.最終更新者 = userId;
                hdData.最終更新日時 = DateTime.Now;
                hdData.削除者 = null;
                hdData.削除日時 = null;

                hdData.AcceptChanges();

            }

            return 1;

        }

        /// <summary>
        /// 出金ヘッダ情報の論理削除をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="hdRow"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private int setT12_PAYHD_Delete(TRAC3Entities context, DataRow hdRow, int userId)
        {
            // 入力情報
            T12_PAYHD t12Data = convertDataRowToT12_PAYHD_Entity(hdRow);

            // 登録データ取得
            var hdData = context.T12_PAYHD
                            .Where(w => w.伝票番号 == t12Data.伝票番号)
                            .FirstOrDefault();

            if (hdData != null)
            {
                // データを論理削除
                hdData.削除者 = userId;
                hdData.削除日時 = DateTime.Now;

                hdData.AcceptChanges();
            }

            return 1;

        }
        #endregion

        #region << 出金明細 >>

        /// <summary>
        /// 出金明細情報を取得する
        /// </summary>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        private List<T12_PAYDTL_Extension> getS02_PAYDTL_Extension(string slipNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int num;
                if (int.TryParse(slipNumber, out num))
                {
                    var result =
                        context.T12_PAYDTL.Where(w => w.削除日時 == null && w.伝票番号 == num)
                            .ToList()
                            .Select(x => new T12_PAYDTL_Extension
                            {
                                伝票番号 = x.伝票番号,
                                行番号 = x.行番号,
                                金種コード = x.金種コード.ToString(),
                                金額 = x.金額,
                                期日 = x.期日,
                                摘要 = x.摘要
                            })
                            .OrderBy(o => o.伝票番号)
                            .ThenBy(t => t.行番号);

                    return result.ToList();

                }
                else
                {
                    return new List<T12_PAYDTL_Extension>();
                }

            }

        }

        /// <summary>
        /// 出金明細情報の更新をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dt"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private int setS02_PAYDTL_Update(TRAC3Entities context, DataTable dt, int userId)
        {
            // 登録済みデータを物理削除
            int i伝票番号 = AppCommon.IntParse(dt.DataSet.Tables[TABLE_HEADER].Rows[0]["伝票番号"].ToString());

            var delData = context.T12_PAYDTL.Where(w => w.伝票番号 == i伝票番号).ToList();
            if (delData != null)
            {
                foreach (T12_PAYDTL dtl in delData)
                    context.T12_PAYDTL.DeleteObject(dtl);

                context.SaveChanges();

            }

            int rowIdx = 1;
            // 明細追加
            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;

                T12_PAYDTL paydtl = new T12_PAYDTL();
                T12_PAYDTL dtlData = convertDataRowToT12_PAYDTL_Entity(row);

                if (dtlData.金種コード <= 0)
                    continue;

                paydtl.伝票番号 = dtlData.伝票番号;
                paydtl.行番号 = rowIdx;
                paydtl.金種コード = dtlData.金種コード;
                paydtl.金額 = dtlData.金額;
                paydtl.期日 = dtlData.期日;
                paydtl.摘要 = dtlData.摘要;
                paydtl.登録者 = userId;
                paydtl.登録日時 = DateTime.Now;
                paydtl.最終更新者 = userId;
                paydtl.最終更新日時 = DateTime.Now;

                context.T12_PAYDTL.ApplyChanges(paydtl);

                rowIdx++;

            }

            context.SaveChanges();

            return 1;

        }

        /// <summary>
        /// 出金明細情報の論理削除をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dt"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private int setT12_PAYDTL_Delete(TRAC3Entities context, DataTable dt, int userId)
        {
            // 登録済みデータを物理削除
            int i伝票番号 = AppCommon.IntParse(dt.DataSet.Tables[TABLE_HEADER].Rows[0]["伝票番号"].ToString());

            var delData = context.T12_PAYDTL.Where(w => w.伝票番号 == i伝票番号).ToList();
            if (delData != null)
            {
                foreach (T12_PAYDTL dtl in delData)
                {
                    dtl.削除者 = userId;
                    dtl.削除日時 = DateTime.Now;
                    dtl.AcceptChanges();
                }
            }

            return 1;

        }

        #endregion


        #region << 処理関連 >>

        /// <summary>
        /// DataRow型をS02_PAYHDエンティティに変換する
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private T12_PAYHD convertDataRowToT12_PAYHD_Entity(DataRow dataRow)
        {
            T12_PAYHD payhd = new T12_PAYHD();
            int iVal;

            payhd.伝票番号 = AppCommon.IntParse(dataRow["伝票番号"].ToString());
            payhd.出金元自社コード = AppCommon.IntParse(dataRow["出金元自社コード"].ToString());
            payhd.出金日 = DateTime.Parse(string.Format("{0:yyyy/MM/dd}", dataRow["出金日"]));
            payhd.出金先販社コード = int.TryParse(dataRow["出金先販社コード"].ToString(), out iVal) ? (int?)iVal : null;
            payhd.得意先コード = int.TryParse(dataRow["得意先コード"].ToString(), out iVal) ? (int?)iVal : null;
            payhd.得意先枝番 = int.TryParse(dataRow["得意先枝番"].ToString(), out iVal) ? (int?)iVal : null;

            return payhd;

        }

        /// <summary>
        /// DataRow型をS02_PAYDTLに変換する
        /// </summary>
        /// <param name="drow"></param>
        /// <returns></returns>
        private T12_PAYDTL convertDataRowToT12_PAYDTL_Entity(DataRow drow)
        {
            T12_PAYDTL paydtl = new T12_PAYDTL();
            DataRow wkRow = drow.Table.Clone().NewRow();

            if (drow.RowState == DataRowState.Deleted)
            {   // 対象が削除行の場合
                // 対象データの参照ができるようにする
                DataTable wkTbl = drow.Table.Copy();
                wkTbl.RejectChanges();

                var orgCode = drow["行番号", DataRowVersion.Original];

                foreach (DataRow dr in wkTbl.Select(string.Format("行番号 = {0}", orgCode)))
                {
                    wkRow.ItemArray = dr.ItemArray;
                    break;  // 複数取る事はないと思うが念の為
                }

            }
            else
            {
                wkRow.ItemArray = drow.ItemArray;
            }

            paydtl.伝票番号 =AppCommon.IntParse(wkRow["伝票番号"].ToString());
            paydtl.行番号 = AppCommon.IntParse(wkRow["行番号"].ToString());
            paydtl.金種コード = AppCommon.IntParse(wkRow["金種コード"].ToString());
            paydtl.金額 = AppCommon.IntParse(wkRow["金額"].ToString());
            if (wkRow["期日"] != null && string.IsNullOrEmpty(wkRow["期日"].ToString()))
                paydtl.期日 = null;
            else
                paydtl.期日 = DateTime.Parse(wkRow["期日"].ToString());
            paydtl.摘要 = wkRow["摘要"].ToString();

            return paydtl;

        }


        #endregion

    }

}