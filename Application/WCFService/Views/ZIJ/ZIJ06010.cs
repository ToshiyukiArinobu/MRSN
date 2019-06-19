using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 商品移動/振替入力問合せサービスクラス
    /// </summary>
    public class ZIJ06010
    {
        #region 項目クラス定義

        /// <summary>
        /// ZIJ06010 検索メンバー
        /// </summary>
        public class SearchDataMember
        {
            public DateTime 移動日 { get; set; }
            public string 伝票番号 { get; set; }
            public string 自社名 { get; set; }
            public string 移動区分 { get; set; }
            public string 移動元倉庫 { get; set; }
            public string 移動先倉庫 { get; set; }
            public int 品番コード { get; set; }
            public string 自社品番 { get; set; }
            public string 商品名 { get; set; }
            public DateTime? 賞味期限 { get; set; }
            public decimal 数量 { get; set; }
            public string 単位 { get; set; }
        }

        #endregion

        /// <summary>
        /// 商品移動問合せ検索情報取得
        /// </summary>
        /// <param name="paramDic">検索条件辞書</param>
        /// <returns></returns>
        public List<SearchDataMember> GetDataList(Dictionary<string, string> paramDic)
        {
            // 型変換作業用変数
            int ival;

            // 入力パラメータを展開
            DateTime strMoveDate = DateTime.Parse(paramDic["移動開始日"]),
                endMoveDate = DateTime.Parse(paramDic["移動終了日"]);
            string myProduct = paramDic["自社品番"];
            int? consignor = int.TryParse(paramDic["出荷元倉庫"], out ival) ? ival : (int?)null,
                distination = int.TryParse(paramDic["出庫先倉庫"], out ival) ? ival : (int?)null;

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                var idoData =
                    context.T05_IDOHD
                        .Where(w => w.削除日時 == null && w.日付 >= strMoveDate && w.日付 <= endMoveDate)
                    .Join(context.T05_IDODTL.Where(w => w.削除日時 == null),
                        x => x.伝票番号, y => y.伝票番号, (a, b) => new { IHD = a, IDTL = b })
                    .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                        x => x.IHD.会社名コード, y => y.自社コード, (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(), (c, d) => new { c.x.IHD, c.x.IDTL, JIS = d })
                    .GroupJoin(context.M22_SOUK.Where(w => w.削除日時 == null),
                        x => x.IHD.出荷元倉庫コード, y => y.倉庫コード, (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(), (e, f) => new { e.x.IHD, e.x.IDTL, e.x.JIS, SOUK_A = f })
                    .GroupJoin(context.M22_SOUK.Where(w => w.削除日時 == null),
                        x => x.IHD.出荷先倉庫コード, y => y.倉庫コード, (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(), (g, h) => new { g.x.IHD, g.x.IDTL, g.x.JIS, g.x.SOUK_A, SOUK_B = h })
                    .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                        x => x.IDTL.品番コード, y => y.品番コード, (x, y) => new { x, y })
                    .SelectMany(z => z.y.DefaultIfEmpty(), (i, j) => new { i.x.IHD, i.x.IDTL, i.x.JIS, i.x.SOUK_A, i.x.SOUK_B, HIN = j });

                #region 検索条件適用

                // 自社品番
                if (!string.IsNullOrEmpty(myProduct))
                    idoData = idoData.Where(w => w.HIN.自社品番 == myProduct);

                // 出荷元倉庫
                if (consignor != null)
                    idoData = idoData.Where(w => w.IHD.出荷元倉庫コード == consignor);

                // 出荷先倉庫
                if (distination != null)
                    idoData = idoData.Where(w => w.IHD.出荷先倉庫コード == distination);

                #endregion

                // 出力結果を整形
                var result =
                    idoData
                        .OrderBy(o => o.IHD.日付)
                        .ThenBy(t => t.IHD.会社名コード)
                        .ThenBy(t => t.IHD.伝票番号)
                        .ThenBy(t => t.IDTL.行番号)
                        .ToList()
                        .Select(x => new SearchDataMember
                        {
                            移動日 = x.IHD.日付,
                            伝票番号 = x.IHD.伝票番号.ToString(),
                            自社名 = x.JIS.自社名,
                            移動区分 = x.IHD.移動区分 == (int)CommonConstants.移動区分.通常移動 ? CommonConstants.移動区分_通常移動 :
                                       x.IHD.移動区分 == (int)CommonConstants.移動区分.売上移動 ? CommonConstants.移動区分_売上移動 :
                                       x.IHD.移動区分 == (int)CommonConstants.移動区分.調整移動 ? CommonConstants.移動区分_調整移動 :
                                       string.Empty,
                            移動元倉庫 = x.SOUK_A != null ? x.SOUK_A.倉庫名 : string.Empty,
                            移動先倉庫 = x.SOUK_B != null ? x.SOUK_B.倉庫名 : string.Empty,
                            品番コード = x.IDTL.品番コード,
                            自社品番 = x.HIN.自社品番,
                            商品名 = x.HIN.自社品名,
                            賞味期限 = x.IDTL.賞味期限,
                            数量 = x.IDTL.数量,
                            単位 = x.HIN.単位
                        });

                return result.ToList();

            }

        }

    }

}