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
        public class BSK06010_SearchMember
        {
            public string 決算対象年月 { get; set; }
            public long 決算調整前金額 { get; set; }
            public long 決算調整見込金額 { get; set; }
            public long? 決算調整後金額 { get; set; }
        }


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
            public int 区分 { get; set; }  // 1:構成品、2:資材、3:その他

        }

        #endregion

        #region << 定数定義 >>

        /// <summary>送信パラメータ 対象販社</summary>
        private string PARAM_NAME_COMPANY = "対象販社";
        /// <summary>送信パラメータ 対象年度</summary>
        private string PARAM_NAME_YEAR = "処理年度";
        /// <summary>送信パラメータ 調整比率</summary>
        private string PARAM_NAME_RATE = "調整比率";

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

                            .Select(x => new BSK06010_ShinMember
                            {
                                品番コード = x.SHIN.a.構成品番コード,
                                自社品番 = x.HIN.自社品番,
                                自社品名 = x.HIN.自社品名,
                                色コード = x.HIN.自社色,
                                色名称 = x.IRO.色名称,
                                原価 = x.HIN.原価 ?? 0,
                                数量 = x.SHIN.a.使用数量,
                                金額 = Math.Ceiling((x.HIN.原価 ?? 0) * x.SHIN.a.使用数量 * 10) / 10,
                                区分 = 1,
                            }).ToList();

                return result;

            }

        }
        #endregion

        #region セット品番より品番コードを取得する

        /// <summary>
        ///セット品番より品番コードを取得する
        /// </summary>
        /// <param name="pCode"></param>
        /// <param name="customerId"></param>
        /// <param name="customerEda"></param>
        /// <returns></returns>
        public List<BSK06010_ShinMember> GetSetHinProduct(string pCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {


                var result = context.M09_HIN
                            .Where(x => x.削除日時 == null && x.自社品番 == pCode && x.商品形態分類 == (int)CommonConstants.商品形態分類.SET品)
                            .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null),
                                       x => x.自社色,
                                       y => y.色コード,
                                      (a, b) => new { a, b })
                            .SelectMany(x => x.b.DefaultIfEmpty(), (x, y) => new { HIN = x, IRO = y })
                            .Select(x => new BSK06010_ShinMember
                            {
                                品番コード = x.HIN.a.品番コード,
                                自社品番 = x.HIN.a.自社品番,
                                自社品名 = x.HIN.a.自社品名,
                                色コード = x.HIN.a.自社色,
                                色名称 = x.IRO.色名称,
                                原価 = x.HIN.a.原価 ?? 0,
                                数量 = 0,
                                金額 = 0,
                                区分 = 1,
                            }).ToList();


                return result;

            }

        }
        #endregion


    }

}