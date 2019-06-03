using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 在庫参照サービスクラス
    /// </summary>
    public class SCHS03_STOK
    {
        #region << モデルクラス定義 >>

        /// <summary>
        /// 在庫検索メンバクラス
        /// </summary>
        public class Search_S03_STOK
        {
            //public string 倉庫コード { get; set; }
            //public string 倉庫名 { get; set; }
            public int 品番コード { get; set; }
            public string 自社品番 { get; set; }
            public string 自社品名 { get; set; }
            public string 自社色名 { get; set; }
            public DateTime? 賞味期限 { get; set; }
            public decimal 在庫数 { get; set; }
            public string ブランド名 { get; set; }
            public string シリーズ名 { get; set; }
            public string 品群名 { get; set; }
        }

        #endregion

        #region 在庫参照画面データ取得
        /// <summary>
        /// 在庫参照データを取得する
        /// </summary>
        /// <param name="stockpileCode"></param>
        /// <param name="paramDic"></param>
        /// <returns></returns>
        public List<Search_S03_STOK> GetData(int stockpileCode, Dictionary<string, string> paramDic)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                DateTime maxDate = AppCommon.GetMaxDate();

                var result =
                    context.S03_STOK
                        .Where(w => w.削除日時 == null && w.倉庫コード == stockpileCode)
                        .Join(context.V_M09_HIN,
                            x => x.品番コード,
                            y => y.品番コード,
                            (x, y) => new { STOK = x, HIN = y })
                        .ToList()
                        .Select(x => new Search_S03_STOK
                        {
                            品番コード = x.STOK.品番コード,
                            自社品番 = x.HIN.自社品番 ?? string.Empty,
                            自社品名 = x.HIN.自社品名 ?? string.Empty,
                            自社色名 = x.HIN.自社色名 ?? string.Empty,
                            賞味期限 = x.STOK.賞味期限 == maxDate ? (DateTime?)null : x.STOK.賞味期限,
                            在庫数 = x.STOK.在庫数,
                            ブランド名 = x.HIN.ブランド名 ?? string.Empty,
                            シリーズ名 = x.HIN.シリーズ名 ?? string.Empty,
                            品群名 = x.HIN.品群名 ?? string.Empty
                        });

                #region 画面入力条件適用

                string productCode = paramDic["品番"],
                    productName = paramDic["品名"],
                    colorName = paramDic["色名"],
                    brandName = paramDic["ブランド"],
                    seriesName = paramDic["シリーズ"],
                    hingunName = paramDic["品群"];

                if (!string.IsNullOrEmpty(productCode))
                    result = result.Where(x => x.自社品番.Contains(productCode));

                if (!string.IsNullOrEmpty(productName))
                    result = result.Where(x => x.自社品名.Contains(productName));

                if (!string.IsNullOrEmpty(colorName))
                    result = result.Where(x => x.自社色名.Contains(colorName));

                if (!string.IsNullOrEmpty(brandName))
                    result = result.Where(x => x.ブランド名.Contains(brandName));

                if (!string.IsNullOrEmpty(seriesName))
                    result = result.Where(x => x.シリーズ名.Contains(seriesName));

                if (!string.IsNullOrEmpty(hingunName))
                    result = result.Where(x => x.品群名.Contains(hingunName));

                #endregion


                return result.ToList();

            }

        }
        #endregion

    }

}