using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    using System.Data;
    using Const = CommonConstants;

    /// <summary>
    /// 入金情報サービスクラス
    /// </summary>
    public class T11
    {
        #region 定数定義

        /// <summary>ヘッダテーブル名</summary>
        private const string TABLE_HEADER = "T11_NYKNHD";
        /// <summary>明細テーブル名</summary>
        private const string TABLE_DETAIL = "T11_NYKNDTL";

        /// <summary>明細の最大行数</summary>
        private const int MAX_ROW_COUNT = 10;

        #endregion

        #region << 拡張クラス定義 >>

        public class T11_NYKNHD_Extension
        {
            public int 伝票番号 { get; set; }
            public string 入金先自社コード { get; set; }
            public DateTime 入金日 { get; set; }
            public string 入金元販社コード { get; set; }
            public string 得意先コード { get; set; }
            public string 得意先枝番 { get; set; }
        }

        public class T11_NYKNDTL_Extension
        {
            public int 伝票番号 { get; set; }
            public int 行番号 { get; set; }
            public string 金種コード { get; set; }
            public int 金額 { get; set; }
            public DateTime? 期日 { get; set; }
            public string 摘要 { get; set; }
        }

        #endregion


        #region << 入金入力関連 >>

        /// <summary>
        /// 入金入力 検索情報を取得する
        /// </summary>
        /// <param name="companyCode">会社名コード(入金先自社コード)</param>
        /// <param name="slipNumber">伝票番号</param>
        /// <param name="userId">ログインユーザID</param>
        /// <returns></returns>
        public DataSet GetData(string companyCode, string slipNumber, int userId)
        {
            DataSet t11ds = new DataSet();

            List<T11_NYKNHD_Extension> hdList = getM11_NYKNHD_Extension(companyCode, slipNumber);
            List<T11_NYKNDTL_Extension> dtlList = getT11_NYKNDTL_Extension(slipNumber);

            if (hdList.Count == 0)
            {
                if (string.IsNullOrEmpty(slipNumber))
                {
                    // 伝票番号未入力の場合は新規伝票扱いとしてデータを作成
                    M88 svc = new M88();
                    int code = int.Parse(companyCode);

                    // 新規伝票番号を取得して設定
                    T11_NYKNHD_Extension hd = new T11_NYKNHD_Extension();
                    hd.伝票番号 = svc.getNextNumber(Const.明細番号ID.ID05_入金, userId);
                    hd.入金先自社コード = companyCode;
                    hd.入金日 = DateTime.Now;

                    hdList.Add(hd);

                }
                else
                {
                    // 伝票番号誤りの場合は検索を終了する
                    return null;
                }

            }

            int rowCnt = 1;
            foreach (T11_NYKNDTL_Extension row in dtlList)
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
        /// 入金入力情報を登録・更新する
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

                        setT11_NYKNHD_Update(context, hdRow, userId);

                        // 2>> 明細情報更新
                        setT11_NYKNDTL_Update(context, ds.Tables[TABLE_DETAIL], userId);

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


        #region << 入金ヘッダ >>

        /// <summary>
        /// 入金ヘッダ情報を取得する
        /// </summary>
        /// <param name="companyCode">会社名コード(入金先自社コード)</param>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        private List<T11_NYKNHD_Extension> getM11_NYKNHD_Extension(string companyCode, string slipNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int code, num;
                if (int.TryParse(companyCode, out code) && int.TryParse(slipNumber, out num))
                {
                    var result =
                        context.T11_NYKNHD
                            .Where(w => w.削除日時 == null && w.入金先自社コード == code && w.伝票番号 == num)
                            .ToList()
                            .Select(x => new T11_NYKNHD_Extension
                            {
                                伝票番号 = x.伝票番号,
                                入金先自社コード = x.入金先自社コード.ToString(),
                                入金日 = x.入金日,
                                入金元販社コード = x.入金元販社コード.ToString(),
                                得意先コード = x.得意先コード.ToString(),
                                得意先枝番 = x.得意先枝番.ToString()
                            });

                    return result.ToList();

                }
                else
                {
                    return new List<T11_NYKNHD_Extension>();
                }

            }

        }

        /// <summary>
        /// 入金ヘッダ情報の更新をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="hdRow"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private int setT11_NYKNHD_Update(TRAC3Entities context, DataRow hdRow, int userId)
        {
            // 入力情報
            T11_NYKNHD t11Data = convertDataRowToT11_NYKNHD_Entity(hdRow);

            // 登録データ取得
            var hdData = context.T11_NYKNHD
                            .Where(w => w.伝票番号 == t11Data.伝票番号)
                            .FirstOrDefault();

            if (hdData == null)
            {
                // データなしの為追加
                T11_NYKNHD nyknhd = new T11_NYKNHD();

                nyknhd.伝票番号 = t11Data.伝票番号;
                nyknhd.入金先自社コード  = t11Data.入金先自社コード;
                nyknhd.入金日 = t11Data.入金日;
                nyknhd.入金元販社コード = t11Data.入金元販社コード;
                nyknhd.得意先コード = t11Data.得意先コード;
                nyknhd.得意先枝番 = t11Data.得意先枝番;
                nyknhd.登録者 = userId;
                nyknhd.登録日時 = DateTime.Now;
                nyknhd.最終更新者 = userId;
                nyknhd.最終更新日時 = DateTime.Now;

                context.T11_NYKNHD.ApplyChanges(nyknhd);

            }
            else
            {
                // データを更新
                hdData.伝票番号 = t11Data.伝票番号;
                hdData.入金先自社コード = t11Data.入金先自社コード;
                hdData.入金日 = t11Data.入金日;
                hdData.入金元販社コード = t11Data.入金元販社コード;
                hdData.得意先コード = t11Data.得意先コード;
                hdData.得意先枝番 = t11Data.得意先枝番;
                hdData.最終更新者 = userId;
                hdData.最終更新日時 = DateTime.Now;
                hdData.削除者 = null;
                hdData.削除日時 = null;

                hdData.AcceptChanges();

            }

            return 1;

        }

        #endregion

        #region << 入金明細 >>

        /// <summary>
        /// 入金明細情報を取得する
        /// </summary>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        private List<T11_NYKNDTL_Extension> getT11_NYKNDTL_Extension(string slipNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int num;
                if (int.TryParse(slipNumber, out num))
                {
                    var result =
                        context.T11_NYKNDTL.Where(w => w.削除日時 == null && w.伝票番号 == num)
                            .ToList()
                            .Select(x => new T11_NYKNDTL_Extension
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
                    return new List<T11_NYKNDTL_Extension>();
                }

            }

        }

        /// <summary>
        /// 入金明細情報の更新をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dt"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private int setT11_NYKNDTL_Update(TRAC3Entities context, DataTable dt, int userId)
        {
            // 登録済みデータを物理削除
            int i伝票番号 = AppCommon.IntParse(dt.DataSet.Tables[TABLE_HEADER].Rows[0]["伝票番号"].ToString());

            var delData = context.T11_NYKNDTL.Where(w => w.伝票番号 == i伝票番号).ToList();
            if (delData != null)
            {
                foreach (T11_NYKNDTL dtl in delData)
                    context.T11_NYKNDTL.DeleteObject(dtl);

                context.SaveChanges();

            }

            int rowIdx = 1;
            // 明細追加
            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;

                T11_NYKNDTL nykndtl = new T11_NYKNDTL();
                T11_NYKNDTL dtlData = convertDataRowToT11_NYKNDTL_Entity(row);

                if (dtlData.金種コード <= 0)
                    continue;

                nykndtl.伝票番号 = dtlData.伝票番号;
                nykndtl.行番号 = rowIdx;
                nykndtl.金種コード = dtlData.金種コード;
                nykndtl.金額 = dtlData.金額;
                nykndtl.期日 = dtlData.期日;
                nykndtl.摘要 = dtlData.摘要;
                nykndtl.登録者 = userId;
                nykndtl.登録日時 = DateTime.Now;
                nykndtl.最終更新者 = userId;
                nykndtl.最終更新日時 = DateTime.Now;

                context.T11_NYKNDTL.ApplyChanges(nykndtl);

                rowIdx++;

            }

            context.SaveChanges();

            return 1;

        }

        #endregion


        #region << 処理関連 >>

        /// <summary>
        /// DataRow型をT11_NYKNHDエンティティに変換する
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private T11_NYKNHD convertDataRowToT11_NYKNHD_Entity(DataRow dataRow)
        {
            T11_NYKNHD nyknhd = new T11_NYKNHD();
            int iVal;

            nyknhd.伝票番号 = AppCommon.IntParse(dataRow["伝票番号"].ToString());
            nyknhd.入金先自社コード = AppCommon.IntParse(dataRow["入金先自社コード"].ToString());
            nyknhd.入金日 = DateTime.Parse(string.Format("{0:yyyy/MM/dd}", dataRow["入金日"]));
            nyknhd.入金元販社コード = int.TryParse(dataRow["入金元販社コード"].ToString(), out iVal) ? (int?)iVal : null;
            nyknhd.得意先コード = int.TryParse(dataRow["得意先コード"].ToString(), out iVal) ? (int?)iVal : null;
            nyknhd.得意先枝番 = int.TryParse(dataRow["得意先枝番"].ToString(), out iVal) ? (int?)iVal : null;

            return nyknhd;

        }

        /// <summary>
        /// DataRow型をT11_NYKNDTLに変換する
        /// </summary>
        /// <param name="drow"></param>
        /// <returns></returns>
        private T11_NYKNDTL convertDataRowToT11_NYKNDTL_Entity(DataRow drow)
        {
            T11_NYKNDTL nykndtl = new T11_NYKNDTL();
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

            nykndtl.伝票番号 =AppCommon.IntParse(wkRow["伝票番号"].ToString());
            nykndtl.行番号 = AppCommon.IntParse(wkRow["行番号"].ToString());
            nykndtl.金種コード = AppCommon.IntParse(wkRow["金種コード"].ToString());
            nykndtl.金額 = AppCommon.IntParse(wkRow["金額"].ToString());
            if (wkRow["期日"] != null && string.IsNullOrEmpty(wkRow["期日"].ToString()))
                nykndtl.期日 = null;
            else
                nykndtl.期日 = DateTime.Parse(wkRow["期日"].ToString());
            nykndtl.摘要 = wkRow["摘要"].ToString();

            return nykndtl;

        }


        #endregion

    }

}