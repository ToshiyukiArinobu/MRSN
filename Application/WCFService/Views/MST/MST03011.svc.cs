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
    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コード、svc、および config ファイルで同時にクラス名 "DataDriveLogService" を変更できます。
    // 注意: このサービスをテストするために WCF テスト クライアントを起動するには、ソリューション エクスプローラーで DataDriveLogService.svc または DataDriveLogService.svc.cs を選択し、デバッグを開始してください。

    /// <summary>
    /// 品番マスタサービスクラス
    /// </summary>
    public class MST03011
    {
        public class MST03011_DATASET
        {
            public List<M06_IRO> M06List = new List<M06_IRO>();
            public List<M09_HIN> M09List = new List<M09_HIN>();
            public List<M10_SHIN> M10List = new List<M10_SHIN>();
        }
       
        /// <summary>
        /// MST03011データメンバー
        /// </summary>
        /// [DataContract]
        public class MST03011_spread
        {
            public int セット品番コード { get; set; }
            public string セット品品番 { get; set; }
            public string 自社色 { get; set; }
            public string 自社色名称 { get; set; }
            public string 自社品名 { get; set; }
            public decimal 登録原価 { get; set; }
            public decimal 登録加工原価 { get; set; }
            public decimal 登録卸値 { get; set; }
            public decimal 登録売価 { get; set; }
            public decimal 構成品原価合計 { get; set; }
            public decimal 構成品卸値合計 { get; set; }
            public decimal 構成品売価合計 { get; set; }
            public decimal 更新用原価 { get; set; }
            public decimal 更新用加工原価 { get; set; }
            public decimal 更新用卸値 { get; set; }
            public decimal 更新用売価 { get; set; }
        }

        #region << メイン処理 >>
        /// <summary>
        /// 品番データを取得する
        /// </summary>
        /// <param name="p自社品番"></param>
        /// <param name="p商品分類"></param>
        /// <param name="pブランド"></param>
        /// <param name="pシリーズ"></param>
        /// <param name="p品群"></param>
        /// <returns></returns>
        public List<MST03011_spread> GetData(string p自社品名, int p商品分類, string pブランド, string pシリーズ, string p品群)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 品番マスタよりセット品を取得する						
                var dtSetItem =
                        context.M09_HIN
                            .Where(w =>
                                w.削除日時 == null
                                && w.商品形態分類 == (int)CommonConstants.商品形態分類.SET品)
                            .ToList();

                                
                #region 入力項目による絞込

                // 品名の条件チェック
                if (!string.IsNullOrEmpty(p自社品名))
                    dtSetItem = dtSetItem.Where(w => w.自社品名 != null && w.自社品名.Contains(p自社品名)).ToList();

                // 商品分類の条件チェック
                int itemType;
                if (int.TryParse(p商品分類.ToString(), out itemType))
                {
                    if (itemType >= CommonConstants.商品分類.食品.GetHashCode())
                        dtSetItem = dtSetItem.Where(w => w.商品分類 == itemType).ToList();
                }

                // ブランドの条件チェック
                if (!string.IsNullOrEmpty(pブランド))
                    dtSetItem = dtSetItem.Where(w => w.ブランド == pブランド).ToList();

                // シリーズの条件チェック
                if (!string.IsNullOrEmpty(pシリーズ))
                    dtSetItem = dtSetItem.Where(w => w.シリーズ == pシリーズ).ToList();

                // 品群の条件チェック
                if (!string.IsNullOrEmpty(p品群))
                    dtSetItem = dtSetItem.Where(w => w.品群 == p品群).ToList();

                #endregion
                                
                var dtSetItemPrice =
                            dtSetItem.AsEnumerable()
                                .GroupJoin(context.M06_IRO
                                .Where(w => w.削除日時 == null), 
                                    x => x.自社色, 
                                    y => y.色コード, (x, y) => new { x, y })
                            .SelectMany(x => x.y.DefaultIfEmpty(), (a, b) => new { HIN = a.x, IRO = b })
                            .Select(s => new MST03011_spread
                            {
                                セット品番コード = s.HIN.品番コード,
                                セット品品番 = s.HIN.自社品番,
                                自社色 = s.HIN.自社色,
                                自社色名称 = s.IRO != null ? s.IRO.色名称 : string.Empty,
                                自社品名 = s.HIN.自社品名,
                                登録原価 = s.HIN.原価 ?? 0,
                                登録加工原価 = s.HIN.加工原価 ?? 0,
                                登録卸値 = s.HIN.卸値 ?? 0,
                                登録売価 = s.HIN.売価 ?? 0,
                                構成品原価合計 = 0,
                                構成品卸値合計 = 0,
                                構成品売価合計 = 0,
                                更新用原価 = s.HIN.原価 ?? 0,
                                更新用加工原価 = s.HIN.加工原価 ?? 0,
                                更新用卸値 = s.HIN.卸値 ?? 0,
                                更新用売価 = s.HIN.売価 ?? 0
                            })
                            .ToList();

                // セット品マスタ、品番マスタより単価合計を取得する						
                var dtSetItemPriceTotal =
                    dtSetItemPrice
                            .Join(context.M10_SHIN.Where(w => w.削除日時 == null),
                                x => new { セット品番 = x.セット品番コード },
                                y => new { セット品番 = y.品番コード },
                                (x, y) => new { SetItem = x, M10_SHIN = y })
                            .Join(context.M09_HIN.Where(w => w.削除日時 == null),
                                x => new { 品番 = x.M10_SHIN.構成品番コード },
                                y => new { 品番 = y.品番コード },
                                (x, y) => new { x.SetItem, x.M10_SHIN, M09_HIN = y })
                            .GroupBy(g => new
                            {
                                g.SetItem.セット品番コード,
                                g.SetItem.セット品品番,
                                g.SetItem.自社色,
                                g.SetItem.自社色名称,
                                g.SetItem.自社品名,
                                g.SetItem.登録原価,
                                g.SetItem.登録加工原価,
                                g.SetItem.登録卸値,
                                g.SetItem.登録売価,
                                g.SetItem.更新用原価,
                                g.SetItem.更新用加工原価,
                                g.SetItem.更新用卸値,
                                g.SetItem.更新用売価,
                            })
                            .Select(s => new MST03011_spread
                            {
                                セット品番コード = s.Key.セット品番コード,
                                セット品品番 = s.Key.セット品品番,
                                自社色 = s.Key.自社色,
                                自社色名称 = s.Key.自社色名称,
                                自社品名 = s.Key.自社品名,
                                登録原価 = s.Key.登録原価,
                                登録加工原価 = s.Key.登録加工原価,
                                登録卸値 = s.Key.登録卸値,
                                登録売価 = s.Key.登録売価,
                                構成品原価合計 = s.Sum(m => m.M09_HIN.原価 * m.M10_SHIN.使用数量) ?? 0,
                                構成品卸値合計 = s.Sum(m => m.M09_HIN.卸値 * m.M10_SHIN.使用数量) ?? 0,
                                構成品売価合計 = s.Sum(m => m.M09_HIN.売価 * m.M10_SHIN.使用数量) ?? 0,
                                更新用原価 = s.Key.更新用原価,
                                更新用加工原価 = s.Key.更新用加工原価,
                                更新用卸値 = s.Key.更新用卸値,
                                更新用売価 = s.Key.更新用売価,
                            })
                            .OrderBy(o => o.セット品番コード).ThenBy(t => t.自社品名)
                            .ToList();

                return dtSetItemPriceTotal.ToList();
                
            }
        }

        /// <summary>
        /// 商品群データの登録・更新をおこなう
        /// </summary>
        /// <param name="updData"></param>
        /// <param name="pLoginUserCode"></param>
        /// <returns></returns>
        public int Update(DataSet ds, int pLoginUserCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try
                {
                    context.Connection.Open();

                    DataTable dt = ds.Tables["MST03011_GetData"];

                    foreach (DataRow row in dt.Rows)
                    {
                        // 変更なしデータは処理対象外とする
                        if (row.RowState == DataRowState.Unchanged)
                            continue;


                        int i品番コード = int.Parse(row["セット品番コード"].ToString());
                        // 対象データ取得
                        var data =
                            context.M09_HIN
                                .Where(w => w.品番コード == i品番コード)
                                .FirstOrDefault();

                        if (data != null)
                        {
                            data.原価 = ConvertdecimalFromString(row["更新用原価"].ToString());
                            data.加工原価 = ConvertdecimalFromString(row["更新用加工原価"].ToString());
                            data.卸値 = ConvertdecimalFromString(row["更新用卸値"].ToString());
                            data.売価 = ConvertdecimalFromString(row["更新用売価"].ToString());

                            data.最終更新者 = pLoginUserCode;
                            data.最終更新日時 = DateTime.Now;

                            data.AcceptChanges();
                
                        }

                    }

                    context.SaveChanges();

                }
                catch 
                {
                    throw;
                }

            }

            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MST03011_DATASET GetMasterDataSet()
        {
            MST03011_DATASET retDataSet = new MST03011_DATASET();
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var m06 = context.M06_IRO
                            .Where(w => w.削除日時 == null)
                            .ToList();

                retDataSet.M06List = m06;

                var m09 = context.M09_HIN
                            .Where(w => w.削除日時 == null && w.商品形態分類 == (int)CommonConstants.商品形態分類.SET品)
                            .ToList();

                retDataSet.M09List = m09;

                var m10 = context.M10_SHIN
                            .Where(w => w.削除日時 == null)
                            .ToList();

                retDataSet.M10List = m10;

            }
            return retDataSet;
        }

        #endregion

        #region << 内部関数　部品群 >>
        /// <summary>
        /// 文字列からnullに変換
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        private string ConvertstringFromString(string strData)
        {
            if (string.IsNullOrEmpty(strData))
            {
                return null;
            }
            else
            {
                return strData;
            }
        }
        /// <summary>
        /// 文字列からint?に変換
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        private int? ConvertintFromString(string strData)
        {
            if (string.IsNullOrEmpty(strData))
            {
                return null;
            }

            int retData;
            if (int.TryParse(strData, out retData))
            {
                return retData;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 文字列からdecimal?に変換
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        private decimal? ConvertdecimalFromString(string strData)
        {
            if (string.IsNullOrEmpty(strData))
            {
                return null;
            }

            decimal retData;
            if (decimal.TryParse(strData, out retData))
            {
                return retData;
            }
            else
            {
                return null;
            }
        }
        #endregion

    }

}
