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

        /// <summary>新製品ヘッダ テーブル名</summary>
        private const string M10_HEADER_TABLE_NAME = "M10_HD";
        /// <summary>新製品構成品 テーブル名</summary>
        private const string M10_DETAIL_TABLE_NAME = "M10_DTL";
        /// <summary>新製品資材 テーブル名</summary>
        private const string M10_ZHIZAI_TABLE_NAME = "M10_SHIZAI";
        /// <summary>新製品そのた テーブル名</summary>
        private const string M10_ETC_TABLE_NAME = "M10_ETC";

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

        /// <summary>
        /// 新製品情報取得
        /// </summary>
        /// <param name="pSetId"></param>
        /// <returns></returns>
        public DataSet GetNewShin(int pSetId)
        {
            DataSet dsM10NewShin = new DataSet();

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                List<M10_NEWSHINHD> hdList = context.M10_NEWSHINHD.Where(c => c.SETID == pSetId).ToList();
                List<M10_NEWSHINDTL> dtlList = context.M10_NEWSHINDTL.Where(c => c.SETID == pSetId).OrderBy(o => o.構成行).ToList();
                List<M10_NEWSHIZAI> shizaiList = context.M10_NEWSHIZAI.Where(c => c.SETID == pSetId).OrderBy(o => o.行番号).ToList();
                List<M10_NEWETC> etcList = context.M10_NEWETC.Where(c => c.SETID == pSetId).OrderBy(o => o.行番号).ToList();


                // Datatable変換
                DataTable dthd = KESSVCEntry.ConvertListToDataTable(hdList);
                DataTable dtdtl = KESSVCEntry.ConvertListToDataTable(dtlList);
                DataTable dtshizai = KESSVCEntry.ConvertListToDataTable(shizaiList);
                DataTable dtetc = KESSVCEntry.ConvertListToDataTable(etcList);

                dthd.TableName = M10_HEADER_TABLE_NAME;
                dsM10NewShin.Tables.Add(dthd);

                dtdtl.TableName = M10_DETAIL_TABLE_NAME;
                dsM10NewShin.Tables.Add(dtdtl);

                dtshizai.TableName = M10_ZHIZAI_TABLE_NAME;
                dsM10NewShin.Tables.Add(dtshizai);

                dtetc.TableName = M10_ETC_TABLE_NAME;
                dsM10NewShin.Tables.Add(dtetc);

                return dsM10NewShin;
            }
        }

        #region 情報登録
        /// <summary>
        /// 情報登録・更新
        /// </summary>
        /// <param name="data"></param>
        /// <param name="loginUserId"></param>
        public bool Update(bool pInsertFlg, int pSETID, string pセット品番, string pセット品名, int p食品割増率, decimal p販社販売価格, decimal p得意先販売価格, DataSet pds, int loginUserId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();


                if (pInsertFlg)
                {
                    // 登録データなし＝新規登録
                    M10_NEWSHINHD regist = new M10_NEWSHINHD();

                    regist.セット品番 = pセット品番;
                    regist.セット品名 = pセット品名;
                    regist.食品割増率 = p食品割増率;
                    regist.得意先販売価格 = p得意先販売価格;
                    regist.販社販売価格 = p販社販売価格;
                    regist.登録者 = loginUserId;
                    regist.登録日時 = DateTime.Now;
                    regist.最終更新者 = loginUserId;
                    regist.最終更新日時 = DateTime.Now;

                    context.M10_NEWSHINHD.ApplyChanges(regist);
                    //SETIDがIdentityのため確定
                    context.SaveChanges();

                    var iSETID = context.M10_NEWSHINHD.Max(m => m.SETID);

                    InsertSubTable(context, iSETID, pds, loginUserId);

                }
                else
                {
                    //構成品・資材・その他のテーブルはDelete-Insert
                    var delDtl = context.M10_NEWSHINDTL.Where(w => w.SETID == pSETID);
                    foreach (var dtl in delDtl)
                    {
                        context.M10_NEWSHINDTL.DeleteObject(dtl);
                    }
                    var delShizai = context.M10_NEWSHIZAI.Where(w => w.SETID == pSETID);
                    foreach (var dtl in delShizai)
                    {
                        context.M10_NEWSHIZAI.DeleteObject(dtl);
                    }
                    var delETC = context.M10_NEWETC.Where(w => w.SETID == pSETID);
                    foreach (var dtl in delETC)
                    {
                        context.M10_NEWETC.DeleteObject(dtl);
                    }
                    context.SaveChanges();

                    // データ更新
                    // 対象データ取得
                    var SHIN = context.M10_NEWSHINHD.Where(w => w.SETID == pSETID).FirstOrDefault();

                    SHIN.セット品番 = pセット品番;
                    SHIN.セット品名 = pセット品名;
                    SHIN.食品割増率 = p食品割増率;
                    SHIN.得意先販売価格 = p得意先販売価格;
                    SHIN.販社販売価格 = p販社販売価格;
                    SHIN.最終更新者 = loginUserId;
                    SHIN.最終更新日時 = DateTime.Now;

                    SHIN.AcceptChanges();

                    InsertSubTable(context, pSETID, pds, loginUserId);
                }

                context.SaveChanges();

            }
            return true;
        }

        /// <summary>
        /// サブテーブル挿入
        /// </summary>
        /// <param name="context"></param>
        /// <param name="piSetID"></param>
        /// <param name="pds"></param>
        /// <param name="loginUserId"></param>
        private void InsertSubTable(TRAC3Entities context, int piSetID, DataSet pds, int loginUserId)
        {
            DataTable dtDtl = pds.Tables[0];

            for (int i = 0; dtDtl.Rows.Count > i; i++)
            {
                // 登録データなし＝新規登録
                M10_NEWSHINDTL registDtl = new M10_NEWSHINDTL();

                DataRow dr = dtDtl.Rows[i];

                registDtl.SETID = piSetID;
                registDtl.自社品番 = dr["自社品番"].ToString();
                registDtl.色コード = dr["色コード"].ToString();
                registDtl.自社品名 = dr["自社品名"].ToString();
                registDtl.原価 = dr["原価"] == DBNull.Value ? 0 : (Decimal)dr["原価"];
                registDtl.必要数量 = dr["数量"] == DBNull.Value ? 0 : (Decimal)dr["数量"];
                registDtl.仕入先名 = dr["仕入先"].ToString();
                registDtl.構成行 = i + 1;
                registDtl.登録者 = loginUserId;
                registDtl.登録日時 = DateTime.Now;
                registDtl.最終更新者 = loginUserId;
                registDtl.最終更新日時 = DateTime.Now;

                context.M10_NEWSHINDTL.ApplyChanges(registDtl);
            }

            DataTable dtShizai = pds.Tables[1];

            for (int i = 0; dtShizai.Rows.Count > i; i++)
            {
                // 登録データなし＝新規登録
                M10_NEWSHIZAI registShizai = new M10_NEWSHIZAI();

                DataRow dr = dtShizai.Rows[i];

                registShizai.SETID = piSetID;
                registShizai.資材名 = dr["資材"].ToString();
                registShizai.自社品番 = dr["自社品番"].ToString();
                registShizai.自社品名 = dr["自社品名"].ToString();
                registShizai.原価 = dr["原価"] == DBNull.Value ? 0 : (Decimal)dr["原価"];
                registShizai.入数 = dr["数量"] == DBNull.Value ? 0 : (Decimal)dr["数量"];
                registShizai.仕入先名 = dr["仕入先"].ToString();
                registShizai.行番号 = i + 1;
                registShizai.登録者 = loginUserId;
                registShizai.登録日時 = DateTime.Now;
                registShizai.最終更新者 = loginUserId;
                registShizai.最終更新日時 = DateTime.Now;

                context.M10_NEWSHIZAI.ApplyChanges(registShizai);
            }

            DataTable dtETC = pds.Tables[2];

            for (int i = 0; dtETC.Rows.Count > i; i++)
            {
                // 登録データなし＝新規登録
                M10_NEWETC registETC = new M10_NEWETC();

                DataRow dr = dtETC.Rows[i];

                registETC.SETID = piSetID;
                registETC.内容 = dr["内容"].ToString();
                registETC.原価 = dr["原価"] == DBNull.Value ? 0 : (Decimal)dr["原価"];
                registETC.入数 = dr["数量"] == DBNull.Value ? 0 : (Decimal)dr["数量"];
                registETC.行番号 = i + 1;
                registETC.登録者 = loginUserId;
                registETC.登録日時 = DateTime.Now;
                registETC.最終更新者 = loginUserId;
                registETC.最終更新日時 = DateTime.Now;

                context.M10_NEWETC.ApplyChanges(registETC);
            }
        }
        #endregion

        #region 削除
        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="data"></param>
        /// <param name="loginUserId"></param>
        public bool Delete(int pSETID, int loginUserId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();


                //構成品・資材・その他のテーブルはDelete-Insert
                var delDtl = context.M10_NEWSHINDTL.Where(w => w.SETID == pSETID);
                foreach (var dtl in delDtl)
                {
                    context.M10_NEWSHINDTL.DeleteObject(dtl);
                }
                var delShizai = context.M10_NEWSHIZAI.Where(w => w.SETID == pSETID);
                foreach (var dtl in delShizai)
                {
                    context.M10_NEWSHIZAI.DeleteObject(dtl);
                }
                var delETC = context.M10_NEWETC.Where(w => w.SETID == pSETID);
                foreach (var dtl in delETC)
                {
                    context.M10_NEWETC.DeleteObject(dtl);
                }

                var SHIN = context.M10_NEWSHINHD.Where(w => w.SETID == pSETID).FirstOrDefault();
                context.M10_NEWSHINHD.DeleteObject(SHIN);

                context.SaveChanges();
            }
            return true;
        }

        #endregion

        #region 新製品検索画面
        /// <summary>
        /// 新製品検索画面用データ取得
        /// </summary>
        /// <returns></returns>
        public List<M10_NEWSHINHD> GetSearchData()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 品番情報取得
                var result = (from x in context.M10_NEWSHINHD
                              select x);


                return result.ToList();
            }
        }
        #endregion
    }
}