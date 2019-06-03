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
    /// <summary>
    /// 請求書関連機能
    /// </summary>
    public class SHR02010
    {

        class WORK_TOOTAL
        {
            public int W_当月支払合計 = 0;
            public int W_当月支払額 = 0;
            public int W_当月支払通行料 = 0;
            public int W_当月課税金額 = 0;
            public int W_当月非課税金額 = 0;
            public int W_当月支払消費税 = 0;
            public int W_前月繰越額 = 0;
            public int W_当月入金額 = 0;
            public int W_当月入金調整額 = 0;
            public int W_差引繰越額 = 0;
            public int W_支払金額計1 = 0;
            public int W_支払金額計2 = 0;
            public int W_支払金額計3 = 0;
            public int W_消費税率_BEFORE = 0;
            public int W_消費税率_AFTER = 0;
            public int W_計算済支払金額 = 0;
            public int W_連番 = 0;
            public int W_支払先ID = 0;
            public int W_請求内訳ID = 0;
            public int W_Ｔ税区分ID = 0;
            public int W_請求書区分ID = 0;
            public string W_支払年月 = string.Empty;
        }

        class UWKLIST
        {
            public int 支払先ID;
            public int 請求書ID;
            public int 内訳ID;
        }
        class TOKLIST
        {
            public int 支払先ID;
            public int 支払先KEY;
            public bool 親;
        }

        /// <summary>
        /// 請求書データ作成＆一覧取得
        /// </summary>
        /// <param name="p端末ID">端末ID</param>
        /// <param name="p得意先ピックアップ">得意先ピックアップ</param>
        /// <param name="p得意先範囲指定From">得意先範囲指定From</param>
        /// <param name="p得意先範囲指定To">得意先範囲指定To</param>
        /// <param name="p作成締日">作成締日</param>
        /// <param name="p作成年月">作成年月</param>
        /// <param name="p請求対象期間From">請求対象期間From</param>
        /// <param name="p請求対象期間To">請求対象期間To</param>
        /// <param name="p出力対象">出力対象</param>
        /// <param name="p請求対象">請求対象</param>
        /// <returns>W_TKS01010_01_Memberのリスト</returns>得意先From, 得意先To, i得意先List, 作成締日, 集計期間From, 集計期間To, 表示区分
        public List<W_SHR02010_02_Member> GetListSHR02010(int? p支払先範囲指定From, int? p支払先範囲指定To, string p支払先ピックアップ, int? p作成締日, DateTime? p請求対象期間From, DateTime? p請求対象期間To, int 社内区分
            )
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                using (DbTransaction transaction = context.Connection.BeginTransaction())
                {
                    int[] tokuList = null;
                    if (string.IsNullOrWhiteSpace(p支払先ピックアップ) != true)
                    {
                        string[] wk = p支払先ピックアップ.Split(',');
                        tokuList = new int[wk.Length];
                        for (int i = 0; i < wk.Length; i++)
                        {
                            tokuList[i] = AppCommon.IntParse(wk[i]);
                        }
                    }

                    string Str自社名 = (from jis in context.M70_JIS.Where(c => c.削除日付 == null) select jis.自社名).FirstOrDefault();

                    // 消費税端数処理区分（共通）
                    int? 共通消費税区分ID = (from c in context.M87_CNTL where c.削除日付 == null select c.売上消費税端数区分).FirstOrDefault();
                    if (共通消費税区分ID == null) { 共通消費税区分ID = 0; }
                    // 明細を取得
                    var cur = (from trn in context.T01_TRN.Where(t01 => (t01.入力区分 != 3) || (t01.入力区分 == 3 && t01.明細行 != 1))
                               from shr in context.M01_TOK.Where(x => x.得意先KEY == trn.支払先KEY && (x.取引区分 == 0 || x.取引区分 == 2))
                               from tok in context.M01_TOK.Where(x => x.得意先KEY == trn.得意先KEY).DefaultIfEmpty()
                               from drv in context.M04_DRV.Where(x => x.乗務員KEY == trn.乗務員KEY).DefaultIfEmpty()
                               from car in context.M05_CAR.Where(x => x.車輌KEY == trn.車輌KEY).DefaultIfEmpty()

                               where trn.支払日付 >= p請求対象期間From && trn.支払日付 <= p請求対象期間To
                                  && shr.Ｓ締日 == p作成締日
                               orderby shr.得意先ID, trn.請求日付
                               select new W_SHR02010_02_Member
                               {
                                   支払先ID = shr.得意先ID,
                                   取引先名 = shr.略称名,
                                   得意先名 = tok.略称名,
                                   T締日 = shr.Ｔ締日,
                                   支払日付 = trn.支払日付,
                                   乗務員名 = drv.乗務員名,
                                   車輌番号 = trn.車輌番号,
                                   数量 = trn.数量,
                                   重量 = trn.重量,
                                   支払単価 = trn.支払単価,
                                   支払金額 = trn.支払金額 + trn.支払割増１ + trn.支払割増２,
                                   支払通行料 = trn.支払通行料,
                                   支払割増1 = trn.支払割増１,
                                   支払割増2 = trn.支払割増２,
                                   支払消費税 = trn.支払消費税,
                                   差益 = (trn.売上金額 + trn.請求割増１ + trn.請求割増２ + trn.通行料) - (trn.支払金額 + trn.支払割増１ + trn.支払割増２ + trn.支払通行料),
                                   商品名 = trn.商品名,
                                   発地名 = trn.発地名,
                                   着地名 = trn.着地名,
                                   社内備考 = trn.社内備考,
                                   親子区分ID = shr.親子区分ID == null ? 0 : (int)shr.親子区分ID,
                                   親ID = shr.親ID == null ? 0 : (int)shr.親ID,
                                   請求内訳管理区分 = shr.請求内訳管理区分,
                                   請求内訳ID = trn.請求内訳ID == null ? 0 : (int)trn.請求内訳ID,
                                   請求税区分 = trn.請求税区分,
                                   支払税区分 = trn.支払税区分,
                                   支払金額計1 = trn.支払金額 + trn.支払割増１ + trn.支払割増２,
                                   支払金額計2 = trn.支払金額 + trn.支払割増１ + trn.支払割増２ + trn.支払通行料,
                                   支払金額計3 = 0,
                                   売上金額 = trn.売上金額 + trn.請求割増１ + trn.請求割増２,
                                   当月請求合計 = 0,
                                   当月売上額 = 0,
                                   当月課税金額 = 0,
                                   当月消費税 = 0,
                                   当月通行料 = 0,
                                   当月非課税金額 = 0,
                                   Ｓ税区分ID = shr.Ｓ税区分ID,
                                   請求書区分ID = shr.請求書区分ID,
                                   摘要名 = trn.請求摘要,
                                   日付From = p請求対象期間From,
                                   日付To = p請求対象期間To,
                                   請求通行料 = trn.通行料,
                                   自社名 = Str自社名,
                               }
                               ).AsQueryable();
                    var list = new List<W_SHR02010_02_Member>();
                    int tokfrom = p支払先範囲指定From != null ? (int)p支払先範囲指定From : int.MinValue;
                    int tokto = p支払先範囲指定To != null ? (int)p支払先範囲指定To : int.MaxValue;
                    if (tokuList != null)
                    {
                        list = cur.Where(x => x.支払先ID >= tokfrom
                                           && x.支払先ID <= tokto
                                           && tokuList.Contains(x.支払先ID)
                                        )
                                        .OrderBy(x => x.支払先ID)
                                        .ToList();
                    }
                    else
                    {
                        list = cur.Where(x => x.支払先ID >= tokfrom
                                           && x.支払先ID <= tokto
                                        )
                                        .OrderBy(x => x.支払先ID)
                                        .ToList();
                    }
                    foreach (var rec in list)
                    {
                        rec.消費税用年月 = ((DateTime)rec.支払日付).ToString("yyyy/MM");
                        if (rec.Ｓ税区分ID == 0)
                        {
                            rec.Ｓ税区分ID = 共通消費税区分ID ?? 0;
                        }
                        if (rec.請求内訳管理区分 == 1)
                        {
                            // 内訳の分は消費税計算区分を「伝票単位」から「請求単位」に置き換える
                            switch (rec.Ｓ税区分ID)
                            {
                                case 5:
                                    rec.Ｓ税区分ID = 1;
                                    break;
                                case 6:
                                    rec.Ｓ税区分ID = 2;
                                    break;
                                case 7:
                                    rec.Ｓ税区分ID = 3;
                                    break;
                            }
                        }
                    }

                    var zeilist = (from zei in context.M73_ZEI orderby zei.適用開始日付 select zei).ToList();

                    List<WORK_TOOTAL> totals = new List<WORK_TOOTAL>();

                    // 各データの消費税計算
                    var list_u = (from u in list
                                  group u by new { u.支払先ID, u.請求内訳ID, u.請求書区分ID } into grp
                                  select new UWKLIST
                                  {
                                      支払先ID = grp.Key.支払先ID,
                                      内訳ID = (int)grp.Key.請求内訳ID,
                                      請求書ID = grp.Key.請求書区分ID,
                                  }).ToList();
                    WORK_TOOTAL totalwk = null;

                    foreach (var urec in list_u)
                    {
                        totalwk = null;
                        totalwk = new WORK_TOOTAL();
                        foreach (var rec in list.Where(x => x.支払先ID == urec.支払先ID && x.請求書区分ID == urec.請求書ID))
                        {


                            totalwk.W_請求書区分ID = rec.請求書区分ID;
                            totalwk.W_請求内訳ID = (int)rec.請求内訳ID;
                            totalwk.W_支払先ID = rec.支払先ID;
                            totalwk.W_支払年月 = ((DateTime)rec.支払日付).ToString("yyyyMM");
                            totalwk.W_Ｔ税区分ID = rec.Ｓ税区分ID;

                            // 請求税区分：0 課税、1 非課税
                            if (rec.支払税区分 == 0)
                            {
                                DateTime dtResult;
                                DateTime.TryParse(string.Format("{0}/{1}/01", totalwk.W_支払年月.Substring(0, 4), totalwk.W_支払年月.Substring(4)), out dtResult);
                                // 合計ワークを再チェック
                                // var zei_w = (from z in zeilist where z.適用開始日付 <= DateTime.Parse(string.Format("{0}/{1}/01", totalwk.W_支払年月.Substring(0, 4), totalwk.W_支払年月.Substring(4))) orderby z.適用開始日付 descending select z.消費税率).FirstOrDefault();
                                var zei_w = (from z in zeilist where z.適用開始日付 <= dtResult orderby z.適用開始日付 descending select z.消費税率).FirstOrDefault();

                                decimal zei = (zei_w == null) ? 0m : ((decimal)zei_w / 100m);
                                // 請求ごとの消費税の場合は計算する
                                switch (rec.Ｓ税区分ID)
                                {
                                    case 1:
                                    case 5:
                                        // 切捨て
                                        rec.当月課税金額 = (int)rec.支払金額 + (int)rec.支払割増1 + (int)rec.支払割増2;
                                        rec.支払消費税 = (int)Math.Floor((decimal)rec.当月課税金額 * zei);
                                        break;
                                    case 2:
                                    case 6:
                                        // 切上げ
                                        rec.当月課税金額 = (int)rec.支払金額 + (int)rec.支払割増1 + (int)rec.支払割増2;
                                        rec.支払消費税 = (int)Math.Ceiling((decimal)rec.当月課税金額 * zei);
                                        break;
                                    case 3:
                                    case 7:
                                        // 四捨五入
                                        rec.当月課税金額 = (int)rec.支払金額 + (int)rec.支払割増1 + (int)rec.支払割増2;
                                        rec.支払消費税 = (int)Math.Round((decimal)rec.当月課税金額 * zei, MidpointRounding.AwayFromZero);
                                        break;
                                    default:
                                        // 税なしまたは税込み
                                        rec.当月非課税金額 = (int)rec.支払金額 + (int)rec.支払割増1 + (int)rec.支払割増2;
                                        rec.支払消費税 = 0;
                                        break;
                                }
                            }
                            else
                            {
                                rec.当月非課税金額 = (int)rec.支払金額 + (int)rec.支払割増1 + (int)rec.支払割増2;
                                rec.支払消費税 = 0;
                            }
                            totalwk.W_当月課税金額 += (int)rec.当月課税金額;
                            totalwk.W_当月非課税金額 += (int)rec.当月非課税金額;
                            totalwk.W_当月支払消費税 += (int)rec.支払消費税;
                            totalwk.W_当月支払額 += (int)rec.支払金額計1;
                            totalwk.W_前月繰越額 = 0;
                            totalwk.W_当月支払通行料 += rec.支払通行料 == null ? 0 : (int)rec.支払通行料;
                        }

                        #region 請求書と同様のやり方で作成 2017-07-10

                        if (totalwk != null)
                        {
                            var temp = (from x in list
                                        where x.支払先ID == totalwk.W_支払先ID
                                        group x by new { x.支払金額計1, x.Ｓ税区分ID, x.支払税区分, x.消費税用年月 } into grp
                                        select new
                                        {
                                            年月 = grp.Key.消費税用年月,
                                            Ｓ税区分ID = grp.Key.Ｓ税区分ID,
                                            支払税区分 = grp.Key.支払税区分,
                                            支払金額 = grp.Sum(s => s.支払金額計1),
                                        });

                            int W_当月消費税 = 0;
                            foreach (var rec in temp)
                            {

                                if (rec.支払税区分 == 0)
                                {
                                    DateTime Wk;
                                    DateTime zday = DateTime.TryParse(rec.年月, out Wk) ? Wk : DateTime.Now;

                                    var zei_w = (from z in zeilist where z.適用開始日付 <= zday orderby z.適用開始日付 descending select z.消費税率).FirstOrDefault();
                                    decimal zei = (zei_w == null) ? 0m : ((decimal)zei_w / 100m);
                                    switch (rec.Ｓ税区分ID)
                                    {
                                        case 1:
                                            // 切捨て
                                            W_当月消費税 += (int)Math.Floor((decimal)rec.支払金額 * zei);
                                            totalwk.W_当月支払消費税 = W_当月消費税;
                                            break;
                                        case 2:
                                            // 切上げ
                                            W_当月消費税 += (int)Math.Ceiling((decimal)rec.支払金額 * zei);
                                            totalwk.W_当月支払消費税 = W_当月消費税;
                                            break;
                                        case 3:
                                            // 四捨五入
                                            W_当月消費税 += (int)Math.Round((decimal)rec.支払金額 * zei, MidpointRounding.AwayFromZero);
                                            totalwk.W_当月支払消費税 = W_当月消費税;
                                            break;
                                        default:
                                            // 8は税なし
                                            break;
                                    }
                                }
                                // 請求合計再計算
                                totalwk.W_当月支払合計 = totalwk.W_当月支払額 + totalwk.W_当月支払消費税 + totalwk.W_当月支払通行料;
                                // 合計値の追加
                                totals.Add(totalwk);
                            }
                        }

                        #endregion
                    }



                    foreach (var rec in list)
                    {
                        var total = (from t in totals
                                     where t.W_支払先ID == rec.支払先ID
                                        && t.W_請求書区分ID == rec.請求書区分ID
                                     select t
                                     ).FirstOrDefault();
                        //switch (total.W_Ｔ税区分ID)
                        //{
                        //    case 1:
                        //    case 2:
                        //    case 3:
                        //    case 4:
                        //        // 合計請求書で消費税計算する場合は明細の消費税をクリア
                        //        total.W_当月支払消費税 = 0;
                        //        break;
                        //}
                        rec.当月消費税 = total.W_当月支払消費税;
                        rec.当月売上額 = total.W_当月支払額;
                        rec.当月課税金額 = total.W_当月課税金額;
                        rec.当月非課税金額 = total.W_当月非課税金額;
                        rec.当月請求合計 = total.W_当月支払合計;
                        rec.当月通行料 = total.W_当月支払通行料;
                    }

                    foreach (var Row in list)
                    {
                        //親IDが入力されていなければ親・一般なので消費税は変更させない
                        if (Row.親ID != 0)
                        {
                            //子のデータから親を特定
                            var oyako = (from m01 in context.M01_TOK.Where(c => c.得意先KEY == Row.親ID) select m01).ToList();
                            if (oyako.Count != 0)
                            {
                                //親子区分 == 1は親(税一括)なので消費税を子には反映させない
                                if (oyako[0].親子区分ID == 1)
                                {
                                    //消費税クリア
                                    Row.支払消費税 = 0;
                                    Row.当月消費税 = 0;
                                }
                            }
                        }
                    }

                    list = list.OrderBy(c => c.支払先ID).ThenBy(c => c.支払日付).ToList();
                    return list.ToList();
                }
            }

        }
    }
}