using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    using debugLog = System.Diagnostics.Debug;

    /// <summary>
    /// 製品原価計算表サービスクラス
    /// </summary>
    public class BSK06010
    {
        #region << メンバクラス定義 >>

        /// <summary>
        /// 製品原価計算表メンバクラス
        /// </summary>
        public class BSK06010_ShinMember
        {

            public int 品番コード { get; set; }
            public string 自社品番 { get; set; }
            public string 自社品名 { get; set; }
            public string 色コード { get; set; }
            public string 色名称 { get; set; }
            public decimal? 原価 { get; set; }
            public decimal? 数量 { get; set; }
            public decimal? 金額 { get; set; }
            public string 仕入先 { get; set; }
            public int 明細区分 { get; set; }  // 1:構成品、2:資材、3:その他

        }

        /// <summary>
        /// 
        /// </summary>
        public class M09_HIN_Extension : M09_HIN
        {
            // REMARKS:不足項目のみ定義
            public int 仕入先コード { get; set; }
            public int 仕入先枝番 { get; set; }
            public string 仕入先名称 { get; set; }
            public string 色名称 { get; set; }

        }

        /// <summary>
        /// 仕入先売価情報メンバクラス
        /// </summary>
        public class M03_BAIKA_Extension : M03_BAIKA
        {
            public string 仕入先名称 { get; set; }
            public int 明細区分 { get; set; }  // 1:構成品、2:資材、3:その他
        }

        #endregion

        #region << 定数定義 >>
        #endregion

        Common com = new Common();


        #region 構成品を取得する
        /// <summary>
        /// 構成品を取得する
        /// </summary>
        /// <param name="paramDic">パラメータDic</param>
        /// <returns></returns>
        public List<BSK06010_ShinMember> GetShinDataList(int p品番コード)
        {

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var result =
                    context.M10_SHIN
                        .Where(w => w.品番コード == p品番コード && w.削除日時 == null)
                        .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                                x => x.構成品番コード,
                                y => y.品番コード,
                                (a, b) => new { a, b })
                            .SelectMany(x => x.b.DefaultIfEmpty(), (x, y) => new { SHIN = x, HIN = y })
                            .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null),
                                x => x.HIN.自社色,
                                y => y.色コード,
                                (c, d) => new { c.SHIN, c.HIN, d })
                            .SelectMany(x => x.d.DefaultIfEmpty(), (x, y) => new { x.SHIN, x.HIN, IRO = y })
                            .GroupJoin(context.M03_BAIKA.Where(w => w.削除日時 == null),
                                   x => x.HIN.品番コード,
                                   y => y.品番コード, (c, d) => new { c, d })
                                .Select(x => new { x.c.SHIN, x.c.HIN, x.c.IRO, SBAI = x.d.OrderBy(c => c.単価).ThenBy(c => c.仕入先コード).FirstOrDefault() })
                                .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                                   x => new { コード = x.SBAI.仕入先コード, 枝番 = x.SBAI.枝番 },
                                   y => new { コード = y.取引先コード, 枝番 = y.枝番 }, (e, f) => new { e, f })
                                .Select(x => new { x.e.SHIN, x.e.HIN, x.e.IRO, x.e.SBAI, SIR = x.f.FirstOrDefault() })
                            .Select(x => new BSK06010_ShinMember
                            {
                                品番コード = x.SHIN.a.構成品番コード,
                                自社品番 = x.HIN.自社品番,
                                自社品名 = x.HIN.自社品名,
                                色コード = x.HIN.自社色,
                                色名称 = x.IRO.色名称,
                                原価 = x.SBAI == null ? x.HIN.原価 ?? 0 : x.SBAI.単価 ?? 0,
                                数量 = x.SHIN.a.使用数量,
                                金額 = Math.Ceiling((x.SBAI == null ? x.HIN.原価 ?? 0 : x.SBAI.単価 ?? 0) * x.SHIN.a.使用数量 * 10) / 10,
                                仕入先 = x.SIR == null ? string.Empty : x.SIR.略称名,
                                明細区分 = 1,

                            }).ToList();

                return result;

            }

        }
        #endregion



        /// <summary>
        /// 指定自社品番より品番情報を取得する
        /// </summary>
        /// <param name="pCode"></param>
        /// <param name="customerId"></param>
        /// <param name="customerEda"></param>
        /// <returns></returns>
        public List<M09_HIN_Extension> GetProductData(string pCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 品番情報取得
                var result = context.M09_HIN.Where(x => x.削除日時 == null && x.自社品番 == pCode)
                                .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null),
                                   x => x.自社色,
                                   y => y.色コード,
                                   (a, b) => new { a, b })
                                .SelectMany(z => z.b.DefaultIfEmpty(), (x, y) => new { HIN = x.a, IRO = y })
                                .GroupJoin(context.M03_BAIKA.Where(w => w.削除日時 == null),
                                   x => x.HIN.品番コード,
                                   y => y.品番コード, (c, d) => new { c, d })
                                .Select(x => new { x.c.HIN, x.c.IRO, SBAI = x.d.OrderBy(c => c.単価).ThenBy(c => c.仕入先コード).FirstOrDefault() })
                                .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                                   x => new { コード = x.SBAI.仕入先コード, 枝番 = x.SBAI.枝番 },
                                   y => new { コード = y.取引先コード, 枝番 = y.枝番 }, (e, f) => new { e, f })
                                .Select(x => new { x.e.HIN, x.e.IRO, x.e.SBAI, SIR = x.f.FirstOrDefault() })
                                .Select(x => new M09_HIN_Extension
                                {
                                    品番コード = x.HIN.品番コード,
                                    自社品番 = x.HIN.自社品番,
                                    自社色 = x.HIN.自社色,
                                    色名称 = x.IRO.色名称,
                                    商品形態分類 = x.HIN.商品形態分類,
                                    商品分類 = x.HIN.商品分類,
                                    大分類 = x.HIN.大分類,
                                    中分類 = x.HIN.中分類,
                                    ブランド = x.HIN.ブランド,
                                    シリーズ = x.HIN.シリーズ,
                                    品群 = x.HIN.品群,
                                    自社品名 = x.HIN.自社品名,
                                    単位 = x.HIN.単位,
                                    原価 = x.SBAI == null ? x.HIN.原価 : x.SBAI.単価,
                                    加工原価 = x.HIN.加工原価,
                                    卸値 = x.HIN.卸値,
                                    売価 = x.HIN.売価,
                                    掛率 = x.HIN.掛率,
                                    消費税区分 = x.HIN.消費税区分,
                                    備考１ = x.HIN.備考１,
                                    備考２ = x.HIN.備考２,
                                    返却可能期限 = x.HIN.返却可能期限,
                                    ＪＡＮコード = x.HIN.ＪＡＮコード,
                                    仕入先コード = x.SBAI == null ? 0 : x.SBAI.仕入先コード,
                                    仕入先枝番 = x.SBAI == null ? 0 : x.SBAI.枝番,
                                    仕入先名称 = x.SIR == null ? string.Empty : x.SIR.略称名,
                                });


                return result.ToList();
            }
        }

        /// <summary>
        /// 品番コードより仕入先売価情報を取得する
        /// </summary>
        /// <param name="pHinCd"></param>
        /// <param name="p区分">1:構成品、2:資材</param>
        /// <returns></returns>
        public List<M03_BAIKA_Extension> GetBaikaData(int? pHinCd, int p明細区分)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();


                // 仕入先売価情報取得
                var resultRow = context.M03_BAIKA.Where(w => w.削除日時 == null && w.品番コード == pHinCd)
                                .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                                   x => new { コード = x.仕入先コード, 枝番 = x.枝番 },
                                   y => new { コード = y.取引先コード, 枝番 = y.枝番 }, (a, b) => new { a, b })
                                .SelectMany(z => z.b.DefaultIfEmpty(), (x, y) => new { BAIKA = x.a, TOK = y })
                                .OrderBy(c => c.BAIKA.単価).ThenBy(c => c.BAIKA.仕入先コード)
                                .Select(x => new M03_BAIKA_Extension
                                {
                                    単価 = x.BAIKA.単価 ?? 0,
                                    仕入先コード = x.BAIKA.仕入先コード,
                                    枝番 = x.BAIKA.枝番,
                                    仕入先名称 = x.TOK.略称名,
                                    明細区分 = p明細区分,
                                }).FirstOrDefault();

                List<M03_BAIKA_Extension> BaikaList = new List<M03_BAIKA_Extension>();

                if (resultRow != null)
                    BaikaList.Add(resultRow);

                return BaikaList;
            }
        }
    }
}