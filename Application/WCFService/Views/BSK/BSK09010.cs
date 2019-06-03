using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 相殺請求管理表 サービスクラス
    /// </summary>
    public class BSK09010
    {
        #region << メンバクラス定義 >>

        #region 帳票印刷メンバ
        /// <summary>
        /// 帳票印刷メンバクラス
        /// </summary>
        public class BSK09010_PrintMember
        {
            public int 得意先コード { get; set; }
            public int 得意先枝番 { get; set; }
            public string 得意先名 { get; set; }
            public DateTime 入金日 { get; set; }
            public DateTime 支払日 { get; set; }
            public long 請求予定額 { get; set; }
            public long 支払予定額 { get; set; }
            public long 相殺請求予定額 { get; set; }
        }
        #endregion

        #endregion

        #region データ取得
        /// <summary>
        /// 出力情報を取得する
        /// </summary>
        /// <param name="paramDic"></param>
        /// <returns></returns>
        public List<BSK09010_PrintMember> GetPrintList(Dictionary<string, string> paramDic)
        {
            // パラメータ展開
            int yearMonth = int.Parse(paramDic["対象年月"].Replace("/", ""));

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var baseList =
                    context.M01_TOK
                        .Where(w => w.削除日時 == null && w.取引区分 == (int)CommonConstants.取引区分.相殺)
                        .GroupJoin(context.S01_SEIHD.Where(w => w.請求年月 == yearMonth),
                            x => new { c = x.取引先コード, e = x.枝番 },
                            y => new { c = y.請求先コード, e = y.請求先枝番 },
                            (x, y) => new { x, y })
                        .SelectMany(z => z.y.DefaultIfEmpty(),
                            (a, b) => new { TOK = a.x, SEI = b })
                        .GroupJoin(context.S02_SHRHD.Where(w => w.支払年月 == yearMonth),
                            x => new { c = x.TOK.取引先コード, e = x.TOK.枝番 },
                            y => new { c = y.支払先コード, e = y.支払先枝番 },
                            (x, y) => new { x, y })
                        .SelectMany(z => z.y.DefaultIfEmpty(),
                            (c, d) => new { c.x.TOK, c.x.SEI, SHR = d })
                        .Where(w => w.SEI != null && w.SHR != null)
                        .Select(s => new
                            {
                                s.TOK.取引先コード,
                                s.TOK.枝番,
                                取引先名 = s.TOK.得意先名１,
                                s.SEI.入金日,
                                s.SHR.支払日,
                                s.SEI.当月請求額,
                                支払予定額 = s.SHR.支払額 + s.SHR.消費税
                            });
                DateTime wdt;
                var resultList =
                    baseList
                        .ToList()
                        .Select(s => new BSK09010_PrintMember
                        {
                            得意先コード = s.取引先コード,
                            得意先枝番 = s.枝番,
                            得意先名 = s.取引先名,
                            入金日 = DateTime.TryParseExact(s.入金日.ToString(), "yyyyMMdd", null, DateTimeStyles.None, out wdt) ? wdt : DateTime.Now,
                            支払日 = DateTime.TryParseExact(s.支払日.ToString(), "yyyyMMdd", null, DateTimeStyles.None, out wdt) ? wdt : DateTime.Now,
                            請求予定額 = s.当月請求額,
                            支払予定額 = s.支払予定額,
                            相殺請求予定額 = s.当月請求額 - s.支払予定額
                        });

                return resultList.ToList();

            }

        }
        #endregion

    }

}