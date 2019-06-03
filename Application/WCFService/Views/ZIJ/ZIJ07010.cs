using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 商品移動/振替入力問合せサービスクラス
    /// </summary>
    public class ZIJ07010
    {
        #region 項目クラス定義

        /// <summary>
        /// ZIJ07010 検索メンバー
        /// </summary>
        public class SearchDataMember
        {
            public long SEQ { get; set; }
            public string 入出庫日 { get; set; }
            public string 入出庫区分 { get; set; }
            public string 倉庫 { get; set; }
            public int 品番コード { get; set; }
            public string 自社品番 { get; set; }
            public string 自社品名 { get; set; }
            public string 自社色 { get; set; }
            public DateTime? 賞味期限 { get; set; }
            public int? 数量 { get; set; }
            public string 単位 { get; set; }
            public string 伝票番号 { get; set; }
            public string 作成機能 { get; set; }
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
            DateTime strMoveDate = DateTime.Parse(paramDic["入出庫開始日"]),
                endMoveDate = DateTime.Parse(paramDic["入出庫終了日"]);
            string myProduct = paramDic["自社品番"];
            int? moveKbn = int.TryParse(paramDic["入出庫区分"], out ival) ? ival : (int?)null,
                stokpile = int.TryParse(paramDic["倉庫コード"], out ival) ? ival : (int?)null;

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                var hstData =
                    context.S04_HISTORY.Where(w => w.削除日時 == null)
                        .GroupJoin(context.M22_SOUK.Where(w => w.削除日時 == null),
                            x => x.倉庫コード, y => y.倉庫コード, (x, y) => new { x, y })
                        .SelectMany(z => z.y.DefaultIfEmpty(), (a, b) => new { HIS = a.x, SOUK = b })
                        .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                            x => x.HIS.品番コード, y => y.品番コード, (x, y) => new { x, y })
                        .SelectMany(z => z.y.DefaultIfEmpty(), (c, d) => new { c.x.HIS, c.x.SOUK, HIN = d })
                        .GroupJoin(context.M06_IRO,
                            x => x.HIN.自社色,
                            y => y.色コード,
                            (x, y) => new { x, y })
                        .SelectMany(z => z.y.DefaultIfEmpty(),
                            (e, f) => new { e.x.HIS, e.x.SOUK, e.x.HIN, IRO = f });

                #region 検索条件適用

                // 入出庫日付
                if (strMoveDate != null)
                    hstData = hstData.Where(w => w.HIS.入出庫日 >= strMoveDate);
                if (endMoveDate != null)
                    hstData = hstData.Where(w => w.HIS.入出庫日 <= endMoveDate);

                // 入出庫区分
                if (moveKbn > 0)
                    hstData = hstData.Where(w => w.HIS.入出庫区分 == moveKbn);

                // 倉庫
                if (stokpile != null)
                    hstData = hstData.Where(w => w.HIS.倉庫コード == stokpile);

                // 自社品番
                if (!string.IsNullOrEmpty(myProduct))
                    hstData = hstData.Where(w => w.HIN.自社品番 == myProduct);

                #endregion

                // 出力結果を整形
                var result = 
                    hstData
                        .OrderBy(o => o.HIS.入出庫日)
                        .ThenBy(t => t.HIS.入出庫時刻)
                        .ThenBy(t => t.HIS.SEQ)
                        .ToList()
                        .Select(x => new SearchDataMember
                        {
                            SEQ = x.HIS.SEQ,
                            入出庫日 = x.HIS.入出庫日.ToString("yyyy/MM/dd"),
                            //入出庫時刻 = x.HIS.入出庫時刻.ToString(@"hh\:mm\:ss"),
                            入出庫区分 =
                                x.HIS.入出庫区分 == (int)CommonConstants.入出庫区分.ID01_入庫 ? CommonConstants.入出庫区分_入庫 :
                                x.HIS.入出庫区分 == (int)CommonConstants.入出庫区分.ID02_出庫 ? CommonConstants.入出庫区分_出庫 :
                                string.Empty,
                            倉庫 = x.SOUK.倉庫名,
                            品番コード = x.HIS.品番コード,
                            自社品番 = x.HIN != null ? x.HIN.自社品番 : string.Empty,
                            自社品名 = x.HIN != null ? x.HIN.自社品名 : string.Empty,
                            自社色 = x.IRO != null ? x.IRO.色名称 : string.Empty,
                            賞味期限 = x.HIS.賞味期限,
                            数量 = x.HIS.数量,
                            単位 = x.HIN.単位,
                            伝票番号 = x.HIS.伝票番号.ToString(),
                            作成機能 =
                                x.HIS.作成機能ID == (int)CommonConstants.作成機能ID.ID01_仕入 ? CommonConstants.作成機能ID_仕入 :
                                x.HIS.作成機能ID == (int)CommonConstants.作成機能ID.ID02_揚り ? CommonConstants.作成機能ID_揚り :
                                x.HIS.作成機能ID == (int)CommonConstants.作成機能ID.ID03_売上 ? CommonConstants.作成機能ID_売上 :
                                x.HIS.作成機能ID == (int)CommonConstants.作成機能ID.ID04_移動 ? CommonConstants.作成機能ID_移動 :
                                x.HIS.作成機能ID == (int)CommonConstants.作成機能ID.ID91_仕入返品 ? CommonConstants.作成機能ID_仕入返品 :
                                x.HIS.作成機能ID == (int)CommonConstants.作成機能ID.ID93_売上返品 ? CommonConstants.作成機能ID_売上返品 :
                                string.Empty
                        });

                return result.ToList();

            }

        }

    }

}