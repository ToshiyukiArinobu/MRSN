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
            public string 得意先名１ { get; set; }
            public string 得意先名２ { get; set; }
            public string 得意先郵便番号 { get; set; }
            public string 得意先住所１ { get; set; }
            public string 得意先住所２ { get; set; }
            public string 得意先TEL { get; set; }
            public string 得意先FAX { get; set; }
            public string 納品先名１ { get; set; }
            public string 納品先名２ { get; set; }
            public string 納品先郵便番号 { get; set; }
            public string 納品先住所１ { get; set; }
            public string 納品先住所２ { get; set; }
            public string 納品先TEL { get; set; }
            public string 納品先FAX { get; set; }
            public string 出荷日 { get; set; }
            public string 出荷元名１ { get; set; }
            public string 出荷元名２ { get; set; }
            public string 出荷元郵便番号 { get; set; }
            public string 出荷元住所１ { get; set; }
            public string 出荷元住所２ { get; set; }
            public string 出荷元TEL { get; set; }
            public string 出荷元FAX { get; set; }
            //public string 納品日 { get; set; }
            public string 自社名 { get; set; }
            public string 自社郵便番号 { get; set; }
            public string 自社住所１ { get; set; }
            public string 自社住所２ { get; set; }
            public string 自社TEL { get; set; }
            public string 自社FAX { get; set; }
            public string 伝票番号 { get; set; }
            public string 納品伝票番号 { get; set; }
            public string 伝票備考 { get; set; }
            public int? 行番号１ { get; set; }
            public string 品番１ { get; set; }
            public string 品名１ { get; set; }
            public decimal? 数量１ { get; set; }
            public decimal? 単価１ { get; set; }
            public int? 金額１ { get; set; }
            public string 単位１ { get; set; }
            public string 摘要１ { get; set; }
            public string 明細品番１ { get; set; }
            public string 明細品名１ { get; set; }
            public decimal? 明細数量１ { get; set; }
            public string 明細単位１ { get; set; }
            public string 明細単価１ { get; set; }
            public string 明細摘要１ { get; set; }
            public int? 行番号２ { get; set; }
            public string 品番２ { get; set; }
            public string 品名２ { get; set; }
            public decimal? 数量２ { get; set; }
            public string 単位２ { get; set; }
            public decimal? 単価２ { get; set; }
            public int? 金額２ { get; set; }
            public string 摘要２ { get; set; }
            public string 明細品番２ { get; set; }
            public string 明細品名２ { get; set; }
            public decimal? 明細数量２ { get; set; }
            public string 明細単位２ { get; set; }
            public string 明細単価２ { get; set; }
            public string 明細摘要２ { get; set; }
            public int? 行番号３ { get; set; }
            public string 品番３ { get; set; }
            public string 品名３ { get; set; }
            public decimal? 数量３ { get; set; }
            public string 単位３ { get; set; }
            public decimal? 単価３ { get; set; }
            public int? 金額３ { get; set; }
            public string 摘要３ { get; set; }
            public string 明細品番３ { get; set; }
            public string 明細品名３ { get; set; }
            public decimal? 明細数量３ { get; set; }
            public string 明細単位３ { get; set; }
            public string 明細単価３ { get; set; }
            public string 明細摘要３ { get; set; }
            public int? 行番号４ { get; set; }
            public string 品番４ { get; set; }
            public string 品名４ { get; set; }
            public decimal? 数量４ { get; set; }
            public string 単位４ { get; set; }
            public decimal? 単価４ { get; set; }
            public int? 金額４ { get; set; }
            public string 摘要４ { get; set; }
            public string 明細品番４ { get; set; }
            public string 明細品名４ { get; set; }
            public decimal? 明細数量４ { get; set; }
            public string 明細単位４ { get; set; }
            public string 明細単価４ { get; set; }
            public string 明細摘要４ { get; set; }
            public int? 行番号５ { get; set; }
            public string 品番５ { get; set; }
            public string 品名５ { get; set; }
            public decimal? 数量５ { get; set; }
            public string 単位５ { get; set; }
            public decimal? 単価５ { get; set; }
            public int? 金額５ { get; set; }
            public string 摘要５ { get; set; }
            public string 明細品番５ { get; set; }
            public string 明細品名５ { get; set; }
            public decimal? 明細数量５ { get; set; }
            public string 明細単位５ { get; set; }
            public string 明細単価５ { get; set; }
            public string 明細摘要５ { get; set; }
            public int? 行番号６ { get; set; }
            public string 品番６ { get; set; }
            public string 品名６ { get; set; }
            public decimal? 数量６ { get; set; }
            public string 単位６ { get; set; }
            public decimal? 単価６ { get; set; }
            public int? 金額６ { get; set; }
            public string 摘要６ { get; set; }
            public string 明細品番６ { get; set; }
            public string 明細品名６ { get; set; }
            public decimal? 明細数量６ { get; set; }
            public string 明細単位６ { get; set; }
            public string 明細単価６ { get; set; }
            public string 明細摘要６ { get; set; }
            public int? 行番号７ { get; set; }
            public string 品番７ { get; set; }
            public string 品名７ { get; set; }
            public decimal? 数量７ { get; set; }
            public string 単位７ { get; set; }
            public decimal? 単価７ { get; set; }
            public int? 金額７ { get; set; }
            public string 摘要７ { get; set; }
            public string 明細品番７ { get; set; }
            public string 明細品名７ { get; set; }
            public decimal? 明細数量７ { get; set; }
            public string 明細単位７ { get; set; }
            public string 明細単価７ { get; set; }
            public string 明細摘要７ { get; set; }
            public int? 行番号８ { get; set; }
            public string 品番８ { get; set; }
            public string 品名８ { get; set; }
            public decimal? 数量８ { get; set; }
            public string 単位８ { get; set; }
            public decimal? 単価８ { get; set; }
            public int? 金額８ { get; set; }
            public string 摘要８ { get; set; }
            public string 明細品番８ { get; set; }
            public string 明細品名８ { get; set; }
            public decimal? 明細数量８ { get; set; }
            public string 明細単位８ { get; set; }
            public string 明細単価８ { get; set; }
            public string 明細摘要８ { get; set; }
            public int? 行番号９ { get; set; }
            public string 品番９ { get; set; }
            public string 品名９ { get; set; }
            public decimal? 数量９ { get; set; }
            public string 単位９ { get; set; }
            public decimal? 単価９ { get; set; }
            public int? 金額９ { get; set; }
            public string 摘要９ { get; set; }
            public string 明細品番９ { get; set; }
            public string 明細品名９ { get; set; }
            public decimal? 明細数量９ { get; set; }
            public string 明細単位９ { get; set; }
            public string 明細単価９ { get; set; }
            public string 明細摘要９ { get; set; }
            public int? 行番号１０ { get; set; }
            public string 品番１０ { get; set; }
            public string 品名１０ { get; set; }
            public decimal? 数量１０ { get; set; }
            public string 単位１０ { get; set; }
            public decimal? 単価１０ { get; set; }
            public int? 金額１０ { get; set; }
            public string 摘要１０ { get; set; }
            public string 明細品番１０ { get; set; }
            public string 明細品名１０ { get; set; }
            public decimal? 明細数量１０ { get; set; }
            public string 明細単位１０ { get; set; }
            public string 明細単価１０ { get; set; }
            public string 明細摘要１０ { get; set; }
        }

        /// <summary>
        /// 明細情報の定義クラス
        /// </summary>
        public class DetailExtension
        {
            public int 行番号 { get; set; }             // No-174 Add
            public string 品番 { get; set; }
            public string 得意先品番 { get; set; }
            public string 品名 { get; set; }
            public decimal 数量 { get; set; }
            public string 単位 { get; set; }
            public decimal 単価 { get; set; }
            public int? 金額 { get; set; }
            public string 摘要 { get; set; }
            // 20190902 add-s  CB軽減税率対応
            public int? 消費税区分 { get; set; }
            // 20190902 add-e  CB軽減税率対応
        }

        #endregion

        /// <summary>
        /// 納品書印刷データ取得
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        //20190919 mod-s CB 軽減税率対応 ccfg.自社区分 が　0の場合は現状のまま　1の場合はT02_URHDの会社名コードとccfg.自社コードが一致す
        //public List<PrintoutMember> GetPrintData(string 売上日From, string 売上日To, string 得意先コード, string 得意先枝番, string 伝票番号From, string 伝票番号To)
        public List<PrintoutMember> GetPrintData(
            string 売上日From, string 売上日To, 
            string 得意先コード, string 得意先枝番, 
            string 伝票番号From, string 伝票番号To,
            int? 自社コード)
        //20190919 mod-e CB 軽減税率対応
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
                {
                    urhdList = urhdList.Where(w => w.得意先コード == code && w.得意先枝番 == eda);
                }
                else if (code != null)
                {
                    urhdList = urhdList.Where(w => w.得意先コード == code);
                }

                if (denFrom != null)
                    urhdList = urhdList.Where(w => w.伝票番号 >= denFrom);

                if (denTo != null)
                    urhdList = urhdList.Where(w => w.伝票番号 <= denTo);

                //20190919 add-s CB 軽減税率対応
                //ccfg.自社区分 が　0の場合は現状のままccfg.自社区分が1の場合は、
                //T02_URHDの会社名コードとccfg.自社コードが一致する
                if (自社コード != null)
                    urhdList = urhdList.Where(w => w.会社名コード == 自社コード);
                //20190919 add-e CB 軽減税率対応

                foreach (T02_URHD hdRow in urhdList.ToList())
                {
                    PrintoutMember prtMem = new PrintoutMember();

                    var Tok =
                        context.M01_TOK
                            .Where(w => w.削除日時 == null && w.取引先コード == hdRow.得意先コード && w.枝番 == hdRow.得意先枝番)
                            .FirstOrDefault();

                    var jis_nusi =
                        context.M70_JIS
                            .Where(w => w.削除日時 == null && w.自社コード == hdRow.出荷元コード)
                            .FirstOrDefault();

                    var jis_jisya =
                        context.M70_JIS
                            .Where(w => w.削除日時 == null && w.自社コード == hdRow.会社名コード)
                            .FirstOrDefault();

                    var syukS =
                        context.M01_TOK
                            .Where(w => w.削除日時 == null && w.取引先コード == hdRow.出荷先コード && w.枝番 == hdRow.出荷先枝番)
                            .FirstOrDefault();

                    var syukM =
                      context.M01_TOK
                          .Where(w => w.削除日時 == null && w.取引先コード == hdRow.出荷元コード && w.枝番 == hdRow.出荷元枝番)
                          .FirstOrDefault();

                    // ヘッダ情報を設定
                    setPrintHeaderData(prtMem, hdRow, Tok, jis_nusi, jis_jisya, syukS, syukM);

                    // 納品書明細情報データを作成
                    var urdtlList =
                        context.T02_URDTL.Where(w => w.削除日時 == null && w.伝票番号 == hdRow.伝票番号)
                            .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                                x => x.品番コード,
                                y => y.品番コード,
                                (x, y) => new { x, y })
                            .SelectMany(z => z.y.DefaultIfEmpty(),
                                (a, b) => new { URDTL = a.x, HIN = b })
                            .GroupJoin(context.M10_TOKHIN.Where(w => w.削除日時 == null),
                                x => new { 品番 = x.URDTL.品番コード, 得意先 = hdRow.得意先コード, 枝番 = hdRow.得意先枝番 },      // No-174 Mod
                                y => new { 品番 = y.品番コード, 得意先 = y.取引先コード, 枝番 = y.枝番 },                          // No-174 Mod
                                (x, y) => new { x, y })
                            .SelectMany(z => z.y.DefaultIfEmpty(),
                                (c, d) => new { c.x.URDTL, c.x.HIN, TOKHIN = d })
                            .Select(x => new DetailExtension
                            {
                                行番号 = x.URDTL.行番号,          // No-174 Add
                                品番 = x.HIN.自社品番,
                                得意先品番 = x.TOKHIN == null ? x.HIN.自社品番 : x.TOKHIN.得意先品番コード,
                                品名 = x.HIN.自社品名,
                                数量 = x.URDTL.数量,
                                単位 = x.URDTL.単位,
                                単価 = x.URDTL.単価,
                                金額 = x.URDTL.金額,

                                //20190902 CB add & mod-s
                                //摘要 = x.URDTL.摘要
                                摘要 = x.URDTL.摘要,
                                消費税区分 = x.HIN.消費税区分
                                //20190902 CB add & mod-e
                            });

                    //// 得意先品番(自社品番)の数量集計データを作成
                    //var query =
                    //    urdtlList.GroupBy(g => new { g.得意先品番, g.単価 })
                    //        .Select(x => new DetailExtension
                    //        {
                    //            品番 = x.Key.得意先品番,
                    //            得意先品番 = string.Empty,
                    //            品名 = x.FirstOrDefault().品名,
                    //            数量 = x.Sum(s => s.数量),
                    //            単位 = x.FirstOrDefault().単位,
                    //            単価 = x.Sum(s => s.単価),
                    //            金額 = x.Sum(s => s.金額),

                    //            //20190902 add & mod-s CB軽減税率対応
                    //            //摘要 = x.FirstOrDefault().摘要
                    //            摘要 = x.FirstOrDefault().摘要,
                    //            消費税区分 = x.FirstOrDefault().消費税区分
                    //            //20190902 add & mod-e CB軽減税率対応
                    //        });

                    // 納品書（下部）のデータ設定
                    int rowNum = 1;
                    foreach (DetailExtension dtlRow in urdtlList.OrderBy(o => o.行番号))      // No-174 Mod
                    {
                        setPrintNouhinData(prtMem, dtlRow, rowNum);
                        rowNum++;
                    }

                    // 納品書（上部）のデータ設定
                    rowNum = 1;
                    foreach (DetailExtension dtlRow in urdtlList.OrderBy(o => o.行番号))      // No-174 Mod
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
        private void setPrintHeaderData(PrintoutMember prtMem, T02_URHD hdRow, M01_TOK Tok, M70_JIS jis_nusi, M70_JIS jis, M01_TOK syukS, M01_TOK syukM)
        {
            prtMem.出荷主 = hdRow.出荷元名;
            prtMem.納品先名１ = hdRow.出荷先名;
            if (Tok != null)
            {
                prtMem.得意先名１ = Tok.得意先名１;
                prtMem.得意先名２ = Tok.得意先名２;
                prtMem.得意先郵便番号 = Tok.郵便番号;
                prtMem.得意先住所１ = Tok.住所１;
                prtMem.得意先住所２ = Tok.住所２;
                prtMem.得意先TEL = Tok.電話番号;
                prtMem.得意先FAX = Tok.ＦＡＸ;
            }     
            prtMem.納品先名１ = hdRow.出荷先名;
            if (syukS != null)
            {
                prtMem.納品先名１ = syukS.得意先名１;
                prtMem.納品先名２ = syukS.得意先名２;
                prtMem.納品先郵便番号 = syukS.郵便番号;
                prtMem.納品先住所１ = syukS.住所１;
                prtMem.納品先住所２ = syukS.住所２;
                prtMem.納品先TEL = syukS.電話番号;
                prtMem.納品先FAX = syukS.ＦＡＸ;
            }
            prtMem.出荷日 = hdRow.出荷日.ToString("yyyy/MM/dd");
            prtMem.出荷元名１ = hdRow.出荷元名;
            if (syukM != null)
            {
                prtMem.出荷元名２ = syukM.得意先名２;
                prtMem.出荷元郵便番号 = syukM.郵便番号;
                prtMem.出荷元住所１ = syukM.住所１;
                prtMem.出荷元住所２ = syukM.住所２;
                prtMem.出荷元TEL = syukM.電話番号;
                prtMem.出荷元FAX = syukM.ＦＡＸ;
            }
            prtMem.自社名 = jis.自社名;
            prtMem.自社郵便番号 = jis.郵便番号;
            prtMem.自社住所１ = jis.住所１;
            prtMem.自社住所２ = jis.住所２;
            prtMem.自社TEL = jis.電話番号;
            prtMem.自社FAX = jis.ＦＡＸ;
            prtMem.伝票番号 = hdRow.伝票番号.ToString();
            prtMem.納品伝票番号 = hdRow.納品伝票番号.ToString();
            prtMem.伝票備考 = hdRow.備考;

        }
        #endregion

        #region 納品書明細情報を印刷データに格納
        /// <summary>
        /// 納品書明細情報を印刷データに格納する
        /// </summary>
        /// <param name="prtMem"></param>
        /// <param name="dtlRow"></param>
        /// <param name="rowNum"></param>
        private void setPrintNouhinData(PrintoutMember prtMem, DetailExtension dtlRow, int rowNum)
        {

            //20190902 add-s CB軽減税率対応
            string 消費税区分wk = "";
            if (dtlRow.消費税区分 == (int)CommonConstants.商品消費税区分.軽減税率)          
                消費税区分wk = "軽 ";
            else if (dtlRow.消費税区分 == (int)CommonConstants.商品消費税区分.非課税)          
                消費税区分wk = "非 ";
            //20190902 add-e CB軽減税率対応

            switch (rowNum)
            {
                case 1:
                    prtMem.行番号１ = rowNum;
                    prtMem.品番１ = dtlRow.品番;
                    prtMem.品名１ = dtlRow.品名;
                    prtMem.数量１ = dtlRow.数量;
                    prtMem.単位１ = dtlRow.単位;
                    prtMem.単価１ = dtlRow.単価;
                    prtMem.金額１ = dtlRow.金額;
                    //20190902 mod-s CB軽減税率対応
                    //prtMem.摘要１ = dtlRow.摘要;
                    prtMem.摘要１ = 消費税区分wk + dtlRow.摘要;
                    //20190902 CB mod-s
                    break;

                case 2:
                    prtMem.行番号２ = rowNum;
                    prtMem.品番２ = dtlRow.品番;
                    prtMem.品名２ = dtlRow.品名;
                    prtMem.数量２ = dtlRow.数量;
                    prtMem.単位２ = dtlRow.単位;
                    prtMem.単価２ = dtlRow.単価;
                    prtMem.金額２ = dtlRow.金額;
                    //20190902 mod-s CB軽減税率対応
                    //prtMem.摘要２ = dtlRow.摘要;
                    prtMem.摘要２ = 消費税区分wk + dtlRow.摘要;
                    //20190902 CB mod-s
                    break;

                case 3:
                    prtMem.行番号３ = rowNum;
                    prtMem.品番３ = dtlRow.品番;
                    prtMem.品名３ = dtlRow.品名;
                    prtMem.数量３ = dtlRow.数量;
                    prtMem.単位３ = dtlRow.単位;
                    prtMem.単価３ = dtlRow.単価;
                    prtMem.金額３ = dtlRow.金額;
                    //20190902 mod-s CB軽減税率対応
                    //prtMem.摘要３ = dtlRow.摘要;
                    prtMem.摘要３ = 消費税区分wk + dtlRow.摘要;
                    //20190902 CB mod-s
                    break;

                case 4:
                    prtMem.行番号４ = rowNum;
                    prtMem.品番４ = dtlRow.品番;
                    prtMem.品名４ = dtlRow.品名;
                    prtMem.数量４ = dtlRow.数量;
                    prtMem.単位４ = dtlRow.単位;
                    prtMem.単価４ = dtlRow.単価;
                    prtMem.金額４ = dtlRow.金額;
                    //20190902 mod-s CB軽減税率対応
                    //prtMem.摘要４ = dtlRow.摘要;
                    prtMem.摘要４ = 消費税区分wk + dtlRow.摘要;
                    //20190902 CB mod-s
                    break;

                case 5:
                    prtMem.行番号５ = rowNum;
                    prtMem.品番５ = dtlRow.品番;
                    prtMem.品名５ = dtlRow.品名;
                    prtMem.数量５ = dtlRow.数量;
                    prtMem.単位５ = dtlRow.単位;
                    prtMem.単価５ = dtlRow.単価;
                    prtMem.金額５ = dtlRow.金額;
                    //20190902 mod-s CB軽減税率対応
                    //prtMem.摘要５ = dtlRow.摘要;
                    prtMem.摘要５ = 消費税区分wk + dtlRow.摘要;
                    //20190902 CB mod-s
                    break;

                case 6:
                    prtMem.行番号６ = rowNum;
                    prtMem.品番６ = dtlRow.品番;
                    prtMem.品名６ = dtlRow.品名;
                    prtMem.数量６ = dtlRow.数量;
                    prtMem.単位６ = dtlRow.単位;
                    prtMem.単価６ = dtlRow.単価;
                    prtMem.金額６ = dtlRow.金額;
                    //20190902 mod-s CB軽減税率対応
                    //prtMem.摘要６ = dtlRow.摘要;
                    prtMem.摘要６ = 消費税区分wk + dtlRow.摘要;
                    //20190902 CB mod-s
                    break;

                case 7:
                    prtMem.行番号７ = rowNum;
                    prtMem.品番７ = dtlRow.品番;
                    prtMem.品名７ = dtlRow.品名;
                    prtMem.数量７ = dtlRow.数量;
                    prtMem.単位７ = dtlRow.単位;
                    prtMem.単価７ = dtlRow.単価;
                    prtMem.金額７ = dtlRow.金額;
                    //20190902 mod-s CB軽減税率対応
                    //prtMem.摘要７ = dtlRow.摘要;
                    prtMem.摘要７ = 消費税区分wk + dtlRow.摘要;
                    //20190902 CB mod-s
                    break;

                case 8:
                    prtMem.行番号８ = rowNum;
                    prtMem.品番８ = dtlRow.品番;
                    prtMem.品名８ = dtlRow.品名;
                    prtMem.数量８ = dtlRow.数量;
                    prtMem.単位８ = dtlRow.単位;
                    prtMem.単価８ = dtlRow.単価;
                    prtMem.金額８ = dtlRow.金額;
                    //20190902 mod-s CB軽減税率対応
                    //prtMem.摘要８ = dtlRow.摘要;
                    prtMem.摘要８ = 消費税区分wk + dtlRow.摘要;
                    //20190902 CB mod-s
                    break;

                case 9:
                    prtMem.行番号９ = rowNum;
                    prtMem.品番９ = dtlRow.品番;
                    prtMem.品名９ = dtlRow.品名;
                    prtMem.数量９ = dtlRow.数量;
                    prtMem.単位９ = dtlRow.単位;
                    prtMem.単価９ = dtlRow.単価;
                    prtMem.金額９ = dtlRow.金額;
                    //20190902 mod-s CB軽減税率対応
                    //prtMem.摘要９ = dtlRow.摘要;
                    prtMem.摘要９ = 消費税区分wk + dtlRow.摘要;
                    //20190902 CB mod-s
                    break;

                case 10:
                    prtMem.行番号１０ = rowNum;
                    prtMem.品番１０ = dtlRow.品番;
                    prtMem.品名１０ = dtlRow.品名;
                    prtMem.数量１０ = dtlRow.数量;
                    prtMem.単位１０ = dtlRow.単位;
                    prtMem.単価１０ = dtlRow.単価;
                    prtMem.金額１０ = dtlRow.金額;
                    //20190902 mod-s CB軽減税率対応
                    //prtMem.摘要１０ = dtlRow.摘要;
                    prtMem.摘要１０ = 消費税区分wk + dtlRow.摘要;
                    //20190902 CB mod-s
                    break;

            }

        }


        /// <summary>
        /// 出荷明細情報を印刷データに格納する
        /// </summary>
        /// <param name="prtMem"></param>
        /// <param name="dtlRow"></param>
        /// <param name="rowNum"></param>
        private void setPrintDetailData(PrintoutMember prtMem, DetailExtension dtlRow, int rowNum)
        {
            switch (rowNum)
            {
                case 1:
                    prtMem.明細品番１ = dtlRow.品番;
                    prtMem.明細品名１ = dtlRow.品名;
                    prtMem.明細数量１ = dtlRow.数量;
                    prtMem.明細単位１ = dtlRow.単位;
                    prtMem.明細摘要１ = dtlRow.摘要;
                    break;

                case 2:
                    prtMem.明細品番２ = dtlRow.品番;
                    prtMem.明細品名２ = dtlRow.品名;
                    prtMem.明細数量２ = dtlRow.数量;
                    prtMem.明細単位２ = dtlRow.単位;
                    prtMem.明細摘要２ = dtlRow.摘要;
                    break;

                case 3:
                    prtMem.明細品番３ = dtlRow.品番;
                    prtMem.明細品名３ = dtlRow.品名;
                    prtMem.明細数量３ = dtlRow.数量;
                    prtMem.明細単位３ = dtlRow.単位;
                    prtMem.明細摘要３ = dtlRow.摘要;
                    break;

                case 4:
                    prtMem.明細品番４ = dtlRow.品番;
                    prtMem.明細品名４ = dtlRow.品名;
                    prtMem.明細数量４ = dtlRow.数量;
                    prtMem.明細単位４ = dtlRow.単位;
                    prtMem.明細摘要４ = dtlRow.摘要;
                    break;

                case 5:
                    prtMem.明細品番５ = dtlRow.品番;
                    prtMem.明細品名５ = dtlRow.品名;
                    prtMem.明細数量５ = dtlRow.数量;
                    prtMem.明細単位５ = dtlRow.単位;
                    prtMem.明細摘要５ = dtlRow.摘要;
                    break;

                case 6:
                    prtMem.明細品番６ = dtlRow.品番;
                    prtMem.明細品名６ = dtlRow.品名;
                    prtMem.明細数量６ = dtlRow.数量;
                    prtMem.明細単位６ = dtlRow.単位;
                    prtMem.明細摘要６ = dtlRow.摘要;
                    break;

                case 7:
                    prtMem.明細品番７ = dtlRow.品番;
                    prtMem.明細品名７ = dtlRow.品名;
                    prtMem.明細数量７ = dtlRow.数量;
                    prtMem.明細単位７ = dtlRow.単位;
                    prtMem.明細摘要７ = dtlRow.摘要;
                    break;

                case 8:
                    prtMem.明細品番８ = dtlRow.品番;
                    prtMem.明細品名８ = dtlRow.品名;
                    prtMem.明細数量８ = dtlRow.数量;
                    prtMem.明細単位８ = dtlRow.単位;
                    prtMem.明細摘要８ = dtlRow.摘要;
                    break;

                case 9:
                    prtMem.明細品番９ = dtlRow.品番;
                    prtMem.明細品名９ = dtlRow.品名;
                    prtMem.明細数量９ = dtlRow.数量;
                    prtMem.明細単位９ = dtlRow.単位;
                    prtMem.明細摘要９ = dtlRow.摘要;
                    break;

                case 10:
                    prtMem.明細品番１０ = dtlRow.品番;
                    prtMem.明細品名１０ = dtlRow.品名;
                    prtMem.明細数量１０ = dtlRow.数量;
                    prtMem.明細単位１０ = dtlRow.単位;
                    prtMem.明細摘要１０ = dtlRow.摘要;
                    break;

            }

        }
        #endregion


    }

}