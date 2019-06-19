using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 揚り依頼サービスクラス
    /// </summary>
    public class DLY07010
    {
        #region << メンバクラス定義 >>

        /// <summary>
        /// 揚り依頼検索メンバクラス
        /// </summary>
        public class DLY07010_SearchMember
        {
            public long SEQ { get; set; }
            public DateTime 依頼日 { get; set; }
            public string 取引先コード { get; set; }
            public string 枝番 { get; set; }
            public string 得意先名 { get; set; }
            public int 品番コード { get; set; }
            public string 自社品番 { get; set; }
            public string 自社品名 { get; set; }
            public int 依頼数 { get; set; }
            public int 仕上数 { get; set; }
        }


        #endregion


        #region 揚り依頼検索情報取得
        /// <summary>
        /// 揚り依頼検索情報を取得する
        /// </summary>
        /// <returns></returns>
        public List<DLY07010_SearchMember> GetData(string productCode, string code, string eda)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var result =
                    context.T04_AGRWK
                            .Where(w => w.削除日時 == null && w.依頼数 != w.仕上数)
                        .GroupJoin(context.M01_TOK,
                            x => new { x.取引先コード, x.枝番 },
                            y => new { y.取引先コード, y.枝番 },
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(),
                            (a, b) => new { AWK = a.x, TOK = b })
                        .GroupJoin(context.M09_HIN
                                .Where(w => w.商品形態分類 == (int)CommonConstants.商品形態分類.SET品),
                            x => x.AWK.品番コード,
                            y => y.品番コード,
                            (x, y) => new { x, y })
                        .SelectMany(x => x.y.DefaultIfEmpty(),
                            (c, d) => new { c.x.AWK, c.x.TOK, HIN = d })
                        .ToList()
                        .Select(s => new DLY07010_SearchMember
                        {
                            SEQ = s.AWK.SEQ,
                            依頼日 = s.AWK.依頼日,
                            取引先コード = s.AWK.取引先コード.ToString(),
                            枝番 = s.AWK.枝番.ToString(),
                            得意先名 = s.TOK != null ? s.TOK.得意先名１ : string.Empty,
                            品番コード = s.AWK.品番コード,
                            自社品番 = s.HIN != null ? s.HIN.自社品番 : string.Empty,
                            自社品名 = s.HIN != null ? s.HIN.自社品名 : string.Empty,
                            依頼数 = s.AWK.依頼数,
                            仕上数 = s.AWK.仕上数
                        });

                // 検索絞り込み
                if (!string.IsNullOrEmpty(productCode))
                    result = result.Where(w => w.自社品番 == productCode);

                if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(eda))
                    result = result.Where(w => w.取引先コード == code && w.枝番 == eda);

                return result.ToList();

            }

        }
        #endregion

        #region 揚り依頼登録更新
        /// <summary>
        /// 揚り依頼情報の登録・更新をおこなう
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="userId"></param>
        public void Update(DataSet ds, int userId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    DataTable dt = ds.Tables[0];

                    try
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            // 変更なしデータは処理しない
                            if (row.RowState == DataRowState.Unchanged)
                                continue;

                            T04_AGRWK wkData = convertDataRowToT04_AGRWK_Entity(row);

                            // 品番コード未設定データは処理しない
                            if (wkData.品番コード == 0)
                                continue;

                            // 揚り依頼データを参照
                            var agrwk =
                                context.T04_AGRWK.Where(w => w.SEQ == wkData.SEQ)
                                    .FirstOrDefault();

                            if (agrwk == null || row.RowState == DataRowState.Added)
                            {
                                T04_AGRWK awk = new T04_AGRWK();
                                awk.SEQ = wkData.SEQ;
                                awk.依頼日 = wkData.依頼日;
                                awk.取引先コード = wkData.取引先コード;
                                awk.枝番 = wkData.枝番;
                                awk.品番コード = wkData.品番コード;
                                awk.依頼数 = wkData.依頼数;
                                awk.仕上数 = wkData.仕上数;

                                awk.登録者 = userId;
                                awk.登録日時 = DateTime.Now;
                                awk.最終更新者 = userId;
                                awk.最終更新日時 = DateTime.Now;

                                context.T04_AGRWK.ApplyChanges(awk);

                            }
                            else if (row.RowState == DataRowState.Deleted)
                            {
                                agrwk.削除者 = userId;
                                agrwk.削除日時 = DateTime.Now;

                                agrwk.AcceptChanges();

                            }
                            else if (row.RowState == DataRowState.Modified)
                            {
                                agrwk.依頼日 = wkData.依頼日;
                                agrwk.取引先コード = wkData.取引先コード;
                                agrwk.枝番 = wkData.枝番;
                                agrwk.品番コード = wkData.品番コード;
                                agrwk.依頼数 = wkData.依頼数;
                                agrwk.仕上数 = wkData.仕上数;
                                agrwk.最終更新者 = userId;
                                agrwk.最終更新日時 = DateTime.Now;

                                agrwk.AcceptChanges();

                            }

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


        #region DataRow to EntityClass
        /// <summary>
        /// データ行を揚り依頼エンティティに変換して返す
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private T04_AGRWK convertDataRowToT04_AGRWK_Entity(DataRow row)
        {
            DataRowVersion ver = row.RowState == DataRowState.Deleted ? DataRowVersion.Original : DataRowVersion.Current;
            T04_AGRWK awk = new T04_AGRWK();

            awk.SEQ = AppCommon.LongParse(row["SEQ", ver].ToString());
            awk.依頼日 = AppCommon.ObjectToDate(row["依頼日", ver]) != null ? (DateTime)AppCommon.ObjectToDate(row["依頼日", ver]) : DateTime.Now;
            awk.取引先コード = AppCommon.IntParse(row["取引先コード", ver].ToString());
            awk.枝番 = AppCommon.IntParse(row["枝番", ver].ToString());
            awk.品番コード = AppCommon.IntParse(row["品番コード", ver].ToString());
            awk.依頼数 = AppCommon.IntParse(row["依頼数", ver].ToString());
            awk.仕上数 = AppCommon.IntParse(row["仕上数", ver].ToString());

            return awk;

        }
        #endregion

    }

}
