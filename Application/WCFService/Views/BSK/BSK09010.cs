﻿using System;
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
            public int 自社コード { get; set; }        // No.227,228 Add
            public string 自社名 { get; set; }         // No.227,228 Add
            public string 得意先コード { get; set; }   // No.223 Mod
            public string 得意先名 { get; set; }
            public string 入金日 { get; set; }         // No-168 Mod
            public string 支払日 { get; set; }         // No-168 Mod
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
                        .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                            x => x.SEI.自社コード,
                            y => y.自社コード,
                            (x, y) => new { x, y })
                        .SelectMany(z => z.y.DefaultIfEmpty(),
                            (e, f) => new { e.x.TOK, e.x.SEI, e.x.SHR, JIS = f})
                        .Where(w => w.SEI != null && w.SHR != null)
                        .Select(s => new
                            {
                                s.JIS.自社コード,            // No.227,228 Add
                                s.JIS.自社名,                // No.227,228 Add
                                s.TOK.取引先コード,
                                s.TOK.枝番,
                                取引先名 = s.TOK.略称名,     // No.229 Mod
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
                            自社コード = s.自社コード,
                            自社名 = s.自社名,
                            得意先コード = string.Format("{0:D4} - {0:D2}", s.取引先コード, s.枝番),     // No.223 Mod
                            得意先名 = s.取引先名,
                            入金日 = DateTime.TryParseExact(s.入金日.ToString(), "yyyyMMdd", null, DateTimeStyles.None, out wdt) ? wdt.ToShortDateString() : DateTime.Now.ToShortDateString(),        // No-168 Mod
                            支払日 = DateTime.TryParseExact(s.支払日.ToString(), "yyyyMMdd", null, DateTimeStyles.None, out wdt) ? wdt.ToShortDateString() : DateTime.Now.ToShortDateString(),        // No-168 Mod
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