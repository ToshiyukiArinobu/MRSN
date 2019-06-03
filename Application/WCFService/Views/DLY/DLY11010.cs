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
    /// 納品書出力サービスクラス
    /// </summary>
    public class DLY11010
    {
        #region 項目クラス定義

        /// <summary>
        /// 帳票項目定義クラス
        /// </summary>
        public class PrintoutMember
        {
            public string 出荷主 { get; set; }
            public string 納品先名１ { get; set; }
            public string 納品先名２ { get; set; }
            public string 納品先郵便番号 { get; set; }
            public string 納品先住所１ { get; set; }
            public string 納品先住所２ { get; set; }
            public string 納品先TEL { get; set; }
            public string 出荷日 { get; set; }
            //public string 納品日 { get; set; }
            public string 自社名 { get; set; }
            public string 自社郵便番号 { get; set; }
            public string 自社住所１ { get; set; }
            public string 自社住所２ { get; set; }
            public string 自社TEL { get; set; }
            public string 自社FAX { get; set; }
            public string 伝票番号 { get; set; }
            public string 納品伝票番号 { get; set; }
            public int? 行番号１ { get; set; }
            public string 品番１ { get; set; }
            public string 品名１ { get; set; }
            public decimal? 数量１ { get; set; }
            public string 単位１ { get; set; }
            public string 摘要１ { get; set; }
            public int? 行番号２ { get; set; }
            public string 品番２ { get; set; }
            public string 品名２ { get; set; }
            public decimal? 数量２ { get; set; }
            public string 単位２ { get; set; }
            public string 摘要２ { get; set; }
            public int? 行番号３ { get; set; }
            public string 品番３ { get; set; }
            public string 品名３ { get; set; }
            public decimal? 数量３ { get; set; }
            public string 単位３ { get; set; }
            public string 摘要３ { get; set; }
            public int? 行番号４ { get; set; }
            public string 品番４ { get; set; }
            public string 品名４ { get; set; }
            public decimal? 数量４ { get; set; }
            public string 単位４ { get; set; }
            public string 摘要４ { get; set; }
            public int? 行番号５ { get; set; }
            public string 品番５ { get; set; }
            public string 品名５ { get; set; }
            public decimal? 数量５ { get; set; }
            public string 単位５ { get; set; }
            public string 摘要５ { get; set; }
            public int? 行番号６ { get; set; }
            public string 品番６ { get; set; }
            public string 品名６ { get; set; }
            public decimal? 数量６ { get; set; }
            public string 単位６ { get; set; }
            public string 摘要６ { get; set; }
            public int? 行番号７ { get; set; }
            public string 品番７ { get; set; }
            public string 品名７ { get; set; }
            public decimal? 数量７ { get; set; }
            public string 単位７ { get; set; }
            public string 摘要７ { get; set; }
            public int? 行番号８ { get; set; }
            public string 品番８ { get; set; }
            public string 品名８ { get; set; }
            public decimal? 数量８ { get; set; }
            public string 単位８ { get; set; }
            public string 摘要８ { get; set; }
            public int? 行番号９ { get; set; }
            public string 品番９ { get; set; }
            public string 品名９ { get; set; }
            public decimal? 数量９ { get; set; }
            public string 単位９ { get; set; }
            public string 摘要９ { get; set; }
            public int? 行番号１０ { get; set; }
            public string 品番１０ { get; set; }
            public string 品名１０ { get; set; }
            public decimal? 数量１０ { get; set; }
            public string 単位１０ { get; set; }
            public string 摘要１０ { get; set; }
        }

        /// <summary>
        /// 明細情報の定義クラス
        /// </summary>
        public class DetailExtension
        {
            public string 品番 { get; set; }
            public string 品名 { get; set; }
            public decimal 数量 { get; set; }
            public string 単位 { get; set; }
            public string 摘要 { get; set; }
        }

        #endregion

        /// <summary>
        /// 納品書印刷データ取得
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<PrintoutMember> GetPrintData(string 売上日From, string 売上日To, string 得意先コード, string 得意先枝番, string 伝票番号From, string 伝票番号To)
        {
            List<PrintoutMember> result = new List<PrintoutMember>();

            // パラメータの型変換
            DateTime dateVal;
            int iVal;

            if (!DateTime.TryParse(売上日From, out dateVal))
                throw new Exception("必須パラメータ不足");

            DateTime DateFrom = dateVal;

            if (!DateTime.TryParse(売上日To, out dateVal))
                throw new Exception("必須パラメータ不足");

            DateTime DateTo = dateVal;
            int? code = int.TryParse(得意先コード, out iVal) ? iVal : (int?)null;
            int? eda = int.TryParse(得意先枝番, out iVal) ? iVal : (int?)null;
            int? denFrom = int.TryParse(伝票番号From, out iVal) ? iVal : (int?)null;
            int? denTo = int.TryParse(伝票番号To, out iVal) ? iVal : (int?)null;

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                // ヘッダ情報取得
                var urhdList =
                    context.T02_URHD
                        .Where(w => w.削除日時 == null && w.売上日 >= DateFrom && w.売上日 <= DateTo);

                // 入力パラメータによるデータ絞込み
                if (code != null && eda != null)
                    urhdList.Where(w => w.得意先コード == code && w.得意先枝番 == eda);

                if (denFrom != null)
                    urhdList = urhdList.Where(w => w.伝票番号 >= denFrom);

                if (denTo != null)
                    urhdList = urhdList.Where(w => w.伝票番号 <= denTo);

                foreach (T02_URHD hdRow in urhdList.ToList())
                {
                    PrintoutMember prtMem = new PrintoutMember();

                    var jis_nusi =
                        context.M70_JIS
                            .Where(w => w.削除日時 == null && w.自社コード == hdRow.出荷元コード)
                            .FirstOrDefault();

                    var jis_jisya =
                        context.M70_JIS
                            .Where(w => w.削除日時 == null && w.自社コード == hdRow.会社名コード)
                            .FirstOrDefault();

                    var syuk =
                        context.M21_SYUK
                            .Where(w => w.削除日時 == null && w.出荷先コード == hdRow.出荷先コード)
                            .FirstOrDefault();

                    // ヘッダ情報を設定
                    setPrintHeaderData(prtMem, hdRow, jis_nusi, jis_jisya, syuk);

                    // 明細情報を設定
                    var urdtlList =
                        context.T02_URDTL.Where(w => w.削除日時 == null && w.伝票番号 == hdRow.伝票番号)
                            .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                                x => x.品番コード,
                                y => y.品番コード,
                                (x, y) => new { x, y })
                            .SelectMany(z => z.y.DefaultIfEmpty(),
                                (a, b) => new { URDTL = a.x, HIN = b })
                            .GroupJoin(context.M10_TOKHIN.Where(w => w.削除日時 == null),
                                x => x.URDTL.品番コード,
                                y => y.品番コード,
                                (x, y) => new { x, y })
                            .SelectMany(z => z.y.DefaultIfEmpty(),
                                (c, d) => new { c.x.URDTL, c.x.HIN, TOKHIN = d })
                            .Select(x => new DetailExtension
                            {
                                品番 = x.TOKHIN == null ? x.HIN.自社品番 : x.TOKHIN.得意先品番コード,
                                品名 = x.TOKHIN == null ? x.HIN.自社品名 : string.Empty,
                                数量 = x.URDTL.数量,
                                単位 = x.URDTL.単位,
                                摘要 = x.URDTL.摘要
                            });

                    // 得意先品番(自社品番)の数量集計データを作成
                    var query =
                        urdtlList.GroupBy(g => new { g.品番, g.品名, g.単位, g.摘要 })
                            .Select(x => new DetailExtension
                            {
                                品番 = x.Key.品番,
                                品名 = x.Key.品名,
                                数量 = x.Sum(s => s.数量),
                                単位 = x.Key.単位,
                                摘要 = x.Key.摘要
                            });

                    int rowNum = 1;
                    foreach (DetailExtension dtlRow in query)
                    {
                        setPrintDetailData(prtMem, dtlRow, rowNum);
                        rowNum++;
                    }

                    result.Add(prtMem);

                }

            }

            return result;

        }

        #region ヘッダ情報を印刷データに格納
        /// <summary>
        /// ヘッダ情報を印刷データに格納する
        /// </summary>
        /// <param name="prtMem"></param>
        /// <param name="hdRow"></param>
        /// <param name="jis_nusi"></param>
        /// <param name="jis"></param>
        /// <param name="syuk"></param>
        private void setPrintHeaderData(PrintoutMember prtMem, T02_URHD hdRow, M70_JIS jis_nusi, M70_JIS jis, M21_SYUK syuk)
        {
            prtMem.出荷主 = jis_nusi == null ? string.Empty : jis_nusi.自社名;
            prtMem.納品先名１ = syuk.出荷先名１;
            prtMem.納品先名２ = syuk.出荷先名２;
            prtMem.納品先郵便番号 = syuk.出荷先郵便番号;
            prtMem.納品先住所１ = syuk.出荷先住所１;
            prtMem.納品先住所２ = syuk.出荷先住所２;
            prtMem.納品先TEL = syuk.出荷先電話番号;
            prtMem.出荷日 = hdRow.出荷日.ToString("yyyy/MM/dd");
            //prtMem.納品日 = ;// TODO:管理していない為出力内容から除外
            prtMem.自社名 = jis.自社名;
            prtMem.自社郵便番号 = jis.郵便番号;
            prtMem.自社住所１ = jis.住所１;
            prtMem.自社住所２ = jis.住所２;
            prtMem.自社TEL = jis.電話番号;
            prtMem.自社FAX = jis.ＦＡＸ;
            prtMem.伝票番号 = hdRow.伝票番号.ToString();
            prtMem.納品伝票番号 = hdRow.納品伝票番号.ToString();

        }
        #endregion

        #region 明細情報を印刷データに格納
        /// <summary>
        /// 明細情報を印刷データに格納する
        /// </summary>
        /// <param name="prtMem"></param>
        /// <param name="dtlRow"></param>
        /// <param name="rowNum"></param>
        private void setPrintDetailData(PrintoutMember prtMem, DetailExtension dtlRow, int rowNum)
        {
            switch (rowNum)
            {
                case 1:
                    prtMem.行番号１ = rowNum;
                    prtMem.品番１ = dtlRow.品番;
                    prtMem.品名１ = dtlRow.品名;
                    prtMem.数量１ = dtlRow.数量;
                    prtMem.単位１ = dtlRow.単位;
                    prtMem.摘要１ = dtlRow.摘要;
                    break;

                case 2:
                    prtMem.行番号２ = rowNum;
                    prtMem.品番２ = dtlRow.品番;
                    prtMem.品名２ = dtlRow.品名;
                    prtMem.数量２ = dtlRow.数量;
                    prtMem.単位２ = dtlRow.単位;
                    prtMem.摘要２ = dtlRow.摘要;
                    break;

                case 3:
                    prtMem.行番号３ = rowNum;
                    prtMem.品番３ = dtlRow.品番;
                    prtMem.品名３ = dtlRow.品名;
                    prtMem.数量３ = dtlRow.数量;
                    prtMem.単位３ = dtlRow.単位;
                    prtMem.摘要３ = dtlRow.摘要;
                    break;

                case 4:
                    prtMem.行番号４ = rowNum;
                    prtMem.品番４ = dtlRow.品番;
                    prtMem.品名４ = dtlRow.品名;
                    prtMem.数量４ = dtlRow.数量;
                    prtMem.単位４ = dtlRow.単位;
                    prtMem.摘要４ = dtlRow.摘要;
                    break;

                case 5:
                    prtMem.行番号５ = rowNum;
                    prtMem.品番５ = dtlRow.品番;
                    prtMem.品名５ = dtlRow.品名;
                    prtMem.数量５ = dtlRow.数量;
                    prtMem.単位５ = dtlRow.単位;
                    prtMem.摘要５ = dtlRow.摘要;
                    break;

                case 6:
                    prtMem.行番号６ = rowNum;
                    prtMem.品番６ = dtlRow.品番;
                    prtMem.品名６ = dtlRow.品名;
                    prtMem.数量６ = dtlRow.数量;
                    prtMem.単位６ = dtlRow.単位;
                    prtMem.摘要６ = dtlRow.摘要;
                    break;

                case 7:
                    prtMem.行番号７ = rowNum;
                    prtMem.品番７ = dtlRow.品番;
                    prtMem.品名７ = dtlRow.品名;
                    prtMem.数量７ = dtlRow.数量;
                    prtMem.単位７ = dtlRow.単位;
                    prtMem.摘要７ = dtlRow.摘要;
                    break;

                case 8:
                    prtMem.行番号８ = rowNum;
                    prtMem.品番８ = dtlRow.品番;
                    prtMem.品名８ = dtlRow.品名;
                    prtMem.数量８ = dtlRow.数量;
                    prtMem.単位８ = dtlRow.単位;
                    prtMem.摘要８ = dtlRow.摘要;
                    break;

                case 9:
                    prtMem.行番号９ = rowNum;
                    prtMem.品番９ = dtlRow.品番;
                    prtMem.品名９ = dtlRow.品名;
                    prtMem.数量９ = dtlRow.数量;
                    prtMem.単位９ = dtlRow.単位;
                    prtMem.摘要９ = dtlRow.摘要;
                    break;

                case 10:
                    prtMem.行番号１０ = rowNum;
                    prtMem.品番１０ = dtlRow.品番;
                    prtMem.品名１０ = dtlRow.品名;
                    prtMem.数量１０ = dtlRow.数量;
                    prtMem.単位１０ = dtlRow.単位;
                    prtMem.摘要１０ = dtlRow.摘要;
                    break;

            }

        }
        #endregion


    }

}