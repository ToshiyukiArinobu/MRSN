using KyoeiSystem.Application.WCFService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    using Const = CommonConstants;

    /// <summary>
    /// 揚り情報サービスクラス
    /// </summary>
    public class T04
    {
        #region 定数定義

        /// <summary>ヘッダテーブル名</summary>
        private const string TABLE_HEADER = "T04_AGRHD";
        /// <summary>明細テーブル名</summary>
        private const string TABLE_DETAIL = "T04_AGRDTL";
        /// <summary>部材明細テーブル名</summary>
        private const string TABLE_MAISAI = "T04_AGRDTB";
        /// <summary>在庫テーブル名</summary>
        private const string TABLE_STOCK = "M73_ZEI";

        #endregion

        #region 拡張クラス定義

        /// <summary>
        /// 揚りヘッダ情報拡張クラス
        /// </summary>
        public class T04_AGRHD_Extension : T04_AGRHD
        {
            // REMARKS:Entityを基本に不足情報を補完する
            /// <summary>1:一括、2:個別</summary>
            public int Ｓ支払消費税区分 { get; set; }
            /// <summary>1:切捨て、2:四捨五入、3:切上げ、9:税なし</summary>
            public int Ｓ税区分ID { get; set; }
        }

        /// <summary>
        /// 揚り明細情報拡張クラス
        /// </summary>
        public class T04_AGRDTL_Extension : T04_AGRDTL
        {
            // REMARKS:Entityを基本に不足情報を補完する
            public string 自社品番 { get; set; }
            public string 自社品名 { get; set; }
            /// <summary>0:対象外、1:対象</summary>
            public int 消費税区分 { get; set; }
            /// <summary>1:食品、2:繊維、3:その他</summary>
            public int 商品分類 { get; set; }
            // No-65 Start
            public string 自社色 { get; set; }
            public string 自社色名 { get; set; }
            // No-65 End
        }

        /// <summary>
        /// 揚り部材明細情報拡張クラス
        /// </summary>
        public class T04_AGRDTB_Extension
        {
            public int セット品番コード { get; set; }
            public string 自社品番 { get; set; }
            public string 自社品名 { get; set; }
            public string 自社色 { get; set; }
            public string 自社色名 { get; set; }
            public int 品番コード { get; set; }
            public DateTime? 賞味期限 { get; set; }
            public decimal 数量 { get; set; }
            public string 単位 { get; set; }
            public decimal 必要数量 { get; set; }
            public decimal 在庫数量 { get; set; }
        }

        #endregion


        #region << 揚り入力関連 >>

        /// <summary>
        /// 揚り検索情報を取得する
        /// </summary>
        /// <param name="companyCode">自社コード(会社名コード)</param>
        /// <param name="slipNumber">伝票番号</param>
        /// <param name="userId">ログインユーザID</param>
        /// <returns></returns>
        public DataSet GetData(string companyCode, string slipNumber, int userId)
        {
            DataSet t04ds = new DataSet();

            List<T04_AGRHD_Extension> hdList = getM04_AGRHD_Extension(companyCode, slipNumber);
            List<T04_AGRDTL_Extension> dtlList = getT04_AGRDTL_Extension(slipNumber);
            List<T04_AGRDTB_Extension> dtbList = null;
            M73 taxService = new M73();
            List<M73_ZEI> taxList = taxService.GetDataList();

            if (hdList.Count == 0)
            {
                if (string.IsNullOrEmpty(slipNumber))
                {
                    // 伝票番号未入力の場合は新規伝票扱いとして作成
                    M88 svc = new M88();
                    int code = int.Parse(companyCode);

                    // 新規伝票番号を取得して設定
                    T04_AGRHD_Extension hd = new T04_AGRHD_Extension();
                    hd.伝票番号 = svc.getNextNumber(Const.明細番号ID.ID02_揚り, userId);
                    hd.会社名コード = code;
                    hd.仕上り日 = DateTime.Now;

                    hdList.Add(hd);

                }
                else
                {
                    // 未登録伝票なので検索終了
                    return null;
                }

            }

            int rowCnt = 1;
            foreach (T04_AGRDTL row in dtlList)
                row.行番号 = rowCnt++;

            T04_AGRHD wkhd = hdList[0] as T04_AGRHD;
            dtbList = getT04_AGRDTB_Extension(slipNumber, wkhd.入荷先コード);

            // Datatable変換
            DataTable dthd = KESSVCEntry.ConvertListToDataTable(hdList);
            DataTable dtdtl = KESSVCEntry.ConvertListToDataTable(dtlList);
            DataTable dtdtb = KESSVCEntry.ConvertListToDataTable(dtbList);
            DataTable dttax = KESSVCEntry.ConvertListToDataTable(taxList);

            dthd.TableName = TABLE_HEADER;
            t04ds.Tables.Add(dthd);

            dtdtl.TableName = TABLE_DETAIL;
            t04ds.Tables.Add(dtdtl);

            dtdtb.TableName = TABLE_MAISAI;
            t04ds.Tables.Add(dtdtb);

            dttax.TableName = TABLE_STOCK;
            t04ds.Tables.Add(dttax);

            return t04ds;

        }

        /// <summary>
        /// 揚り入力情報を登録・更新する
        /// </summary>
        /// <param name="ds">
        /// 揚りデータセット
        /// [0:T04_AGRHD、1:T04_AGRDTL]
        /// </param>
        /// <param name="userId">ログインユーザID</param>
        /// <returns></returns>
        public int Update(DataSet ds, int userId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    try
                    {
                        // 1>> ヘッダ情報更新
                        DataRow hdRow = ds.Tables[TABLE_HEADER].Rows[0];
                        setT04_AGRHD_Update(context, hdRow, userId);

                        // 2>> 明細情報更新
                        setT04_AGRDTL_Update(context, ds.Tables[TABLE_DETAIL], userId);

                        // 3>> 部材明細更新
                        setT04_AGRDTB_Update(context, ParseNumeric<int>(hdRow["伝票番号"]), userId);

                        // 4>> 在庫情報更新
                        setS03_STOK_DTB_Update(context, ds, userId);
                        setS03_STOK_Update(context, ds, userId);

                        // 変更状態を確定
                        context.SaveChanges();

                        tran.Commit();

                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }

                }

            }

            return 1;

        }

        /// <summary>
        /// 揚り入力情報の削除処理をおこなう
        /// </summary>
        /// <param name="伝票番号">伝票番号</param>
        /// <param name="ユーザID">ログインユーザID</param>
        /// <returns></returns>
        public int Delete(string 伝票番号, int ユーザID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    S04 historyService = new S04(context, ユーザID, S04.機能ID.揚り入力);

                    int num;
                    try
                    {
                        if (int.TryParse(伝票番号, out num))
                        {
                            #region AGRHD取得
                            var t04agrhd =
                                context.T04_AGRHD
                                        .Where(w => w.伝票番号 == num)
                                        .FirstOrDefault();

                            if (t04agrhd != null)
                            {
                                t04agrhd.削除者 = ユーザID;
                                t04agrhd.削除日時 = DateTime.Now;
                                t04agrhd.AcceptChanges();

                            }
                            #endregion

                            #region AGRDTL取得
                            var t04agrdtlList =
                                context.T04_AGRDTL
                                        .Where(w => w.伝票番号 == num)
                                        .ToList();

                            if (t04agrdtlList != null && t04agrdtlList.Count > 0)
                            {
                                foreach (var t04agrdtl in t04agrdtlList)
                                {
                                    t04agrdtl.削除者 = ユーザID;
                                    t04agrdtl.削除日時 = DateTime.Now;
                                    t04agrdtl.AcceptChanges();
                                }

                            }
                            #endregion

                            #region AGRDTB取得
                            var t04agrdtbList =
                                context.T04_AGRDTB
                                    .Where(w => w.伝票番号 == num)
                                    .ToList();

                            if (t04agrdtbList != null && t04agrdtbList.Count > 0)
                            {
                                foreach (var t04agrdtb in t04agrdtbList)
                                {
                                    t04agrdtb.削除者 = ユーザID;
                                    t04agrdtb.削除日時 = DateTime.Now;
                                    t04agrdtb.AcceptChanges();
                                }

                            }
                            #endregion

                            // 会社名から対象の倉庫を取得
                            var companyCode =
                                    context.T04_AGRHD
                                        .Where(x => x.削除日時 == null && x.伝票番号 == num)
                                        .Select(s => s.入荷先コード)
                                        .FirstOrDefault();
                            var souk = get倉庫コード(context, companyCode);

                            #region STOK更新(構成品)
                            foreach (var data in context.T04_AGRDTB.Where(w => w.伝票番号 == num))
                            {
                                DateTime maxDate = AppCommon.DateTimeToDate(data.賞味期限, DateTime.MaxValue);
                                var stkResult =
                                    context.S03_STOK
                                        .Where(w => w.削除日時 == null &&
                                            w.倉庫コード == souk &&
                                            w.品番コード == data.品番コード &&
                                            w.賞味期限 == maxDate)
                                        .FirstOrDefault();

                                if (stkResult != null)
                                {
                                    stkResult.在庫数 += data.数量 ?? 0;
                                    stkResult.最終更新者 = ユーザID;
                                    stkResult.最終更新日時 = DateTime.Now;

                                    stkResult.AcceptChanges();

                                    #region 入出庫データ作成

                                    S04_HISTORY history = new S04_HISTORY();

                                    history.入出庫日 = t04agrhd.仕上り日;
                                    history.入出庫時刻 = DateTime.Now.TimeOfDay;
                                    history.倉庫コード = souk;
                                    history.入出庫区分 = (int)Const.入出庫区分.ID01_入庫;
                                    history.品番コード = data.品番コード;
                                    history.数量 = Math.Abs(decimal.ToInt32(data.数量 ?? 0));
                                    history.伝票番号 = data.伝票番号;

                                    historyService.CreateProductHistory(history);

                                    #endregion

                                }

                            }
                            #endregion

                            #region STOK更新(セット商品)
                            foreach (T04_AGRDTL agrdtl in t04agrdtlList)
                            {
                                DateTime maxDate = AppCommon.DateTimeToDate(agrdtl.賞味期限, DateTime.MaxValue);
                                var stkResult =
                                    context.S03_STOK
                                        .Where(w => w.削除日時 == null &&
                                            w.倉庫コード == souk &&
                                            w.品番コード == agrdtl.品番コード &&
                                            w.賞味期限 == maxDate)
                                        .FirstOrDefault();

                                if (stkResult != null)
                                {
                                    stkResult.在庫数 -= agrdtl.数量 ?? 0;
                                    stkResult.最終更新者 = ユーザID;
                                    stkResult.最終更新日時 = DateTime.Now;

                                    stkResult.AcceptChanges();

                                }

                                #region 入出庫データ作成

                                S04_HISTORY history = new S04_HISTORY();

                                history.入出庫日 = t04agrhd.仕上り日;
                                history.入出庫時刻 = DateTime.Now.TimeOfDay;
                                history.倉庫コード = souk;
                                history.入出庫区分 = (int)Const.入出庫区分.ID02_出庫;
                                history.品番コード = agrdtl.品番コード;
                                history.数量 = Math.Abs(decimal.ToInt32(agrdtl.数量 ?? 0));
                                history.伝票番号 = agrdtl.伝票番号;

                                historyService.CreateProductHistory(history);

                                #endregion

                            }
                            #endregion

                            // 変更状態を確定
                            context.SaveChanges();

                            tran.Commit();

                        }

                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }

                }

            }

            return 1;

        }

        #endregion


        #region << 揚りヘッダ情報 >>

        /// <summary>
        /// 揚りヘッダ情報を取得する
        /// </summary>
        /// <returns></returns>
        private List<T04_AGRHD> getM03_SRHDList()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var result = context.T04_AGRHD
                                .Where(w => w.削除日時 == null);

                return result.ToList();

            }

        }

        /// <summary>
        /// 揚りヘッダ情報を取得する
        /// </summary>
        /// <param name="companyCode">会社名コード</param>
        /// <returns></returns>
        private List<T04_AGRHD> getM03_SRHD(string companyCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int code;
                if (int.TryParse(companyCode, out code))
                {
                    var result = context.T04_AGRHD.Where(w => w.削除日時 == null && w.会社名コード == code);

                    return result.ToList();

                }
                else
                {
                    return new List<T04_AGRHD>();
                }

            }

        }

        /// <summary>
        /// 揚りヘッダ情報を取得する
        /// </summary>
        /// <param name="companyCode">会社名コード</param>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        private List<T04_AGRHD> getM03_SRHD(string companyCode, string slipNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int code, num;
                if (int.TryParse(companyCode, out code) && int.TryParse(slipNumber, out num))
                {
                    var result = context.T04_AGRHD.Where(w => w.削除日時 == null && w.会社名コード == code && w.伝票番号 == num);

                    return result.ToList();

                }
                else
                {
                    return new List<T04_AGRHD>();
                }

            }


        }

        /// <summary>
        /// 揚りヘッダ情報(品番情報含む)を取得する
        /// </summary>
        /// <param name="companyCode">会社名コード</param>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        private List<T04_AGRHD_Extension> getM04_AGRHD_Extension(string companyCode, string slipNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int code, num;
                if (int.TryParse(companyCode, out code) && int.TryParse(slipNumber, out num))
                {
                    var result = context.T04_AGRHD.Where(w => w.削除日時 == null && w.会社名コード == code && w.伝票番号 == num)
                                    .GroupJoin(context.M01_TOK.Where(w => w.削除日時 == null),
                                        x => new { 取引先コード = x.外注先コード, 枝番 = x.外注先枝番 },
                                        y => new { 取引先コード = y.取引先コード, 枝番 = y.枝番 },
                                        (p, q) => new { p, q })
                                    .SelectMany(g => g.q.DefaultIfEmpty(), (a, b) => new { a, b })
                                    .Select(z => new T04_AGRHD_Extension
                                    {
                                        伝票番号 = z.a.p.伝票番号,
                                        会社名コード = z.a.p.会社名コード,
                                        仕上り日 = z.a.p.仕上り日,
                                        加工区分 = z.a.p.加工区分,
                                        外注先コード = z.a.p.外注先コード,
                                        外注先枝番 = z.a.p.外注先枝番,
                                        入荷先コード = z.a.p.入荷先コード,
                                        備考 = z.a.p.備考,
                                        消費税 = z.a.p.消費税,
                                        // No-96 Mod Start
                                        Ｓ支払消費税区分 = z.b == null ? 0 : z.b.Ｓ支払消費税区分,
                                        Ｓ税区分ID = z.b == null ? 0 : z.b.Ｓ支払区分
                                        // No-96 Mod End
                                    });

                    return result.ToList();

                }
                else
                {
                    return new List<T04_AGRHD_Extension>();

                }

            }

        }

        /// <summary>
        /// 揚りヘッダ情報の更新をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="hdRow"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private int setT04_AGRHD_Update(TRAC3Entities context, DataRow hdRow, int userId)
        {
            // 入力情報
            T04_AGRHD t04Data = convertDataRowToT04_AGRHD_Entity(hdRow);

            // 登録データ取得
            var hdData = context.T04_AGRHD
                            .Where(w => w.伝票番号 == t04Data.伝票番号)
                            .FirstOrDefault();

            if (hdData == null)
            {
                // データなしの為追加
                T04_AGRHD srhd = new T04_AGRHD();

                srhd.伝票番号 = t04Data.伝票番号;
                srhd.会社名コード = t04Data.会社名コード;
                srhd.仕上り日 = t04Data.仕上り日;
                srhd.加工区分 = t04Data.加工区分;
                srhd.外注先コード = t04Data.外注先コード;
                srhd.外注先枝番 = t04Data.外注先枝番;
                srhd.入荷先コード = t04Data.入荷先コード;
                srhd.備考 = t04Data.備考;
                srhd.消費税 = t04Data.消費税;
                srhd.登録者 = userId;
                srhd.登録日時 = DateTime.Now;
                srhd.最終更新者 = userId;
                srhd.最終更新日時 = DateTime.Now;

                context.T04_AGRHD.ApplyChanges(srhd);

            }
            else
            {
                // データを更新
                hdData.会社名コード = t04Data.会社名コード;
                hdData.仕上り日 = t04Data.仕上り日;
                hdData.加工区分 = t04Data.加工区分;
                hdData.外注先コード = t04Data.外注先コード;
                hdData.外注先枝番 = t04Data.外注先枝番;
                hdData.入荷先コード = t04Data.入荷先コード;
                hdData.備考 = t04Data.備考;
                hdData.消費税 = t04Data.消費税;
                hdData.最終更新者 = userId;
                hdData.最終更新日時 = DateTime.Now;
                hdData.削除者 = null;
                hdData.削除日時 = null;

                hdData.AcceptChanges();

            }

            return 1;

        }


        #endregion

        #region << 揚り明細情報  >>

        /// <summary>
        /// 揚り明細情報を取得する
        /// </summary>
        /// <returns></returns>
        private List<T04_AGRDTL> getT04_AGRDTLList()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var result = context.T04_AGRDTL
                                .Where(w => w.削除日時 == null)
                                .OrderBy(o => o.伝票番号)
                                .ThenBy(t => t.行番号);

                return result.ToList();

            }

        }

        /// <summary>
        /// 揚り明細情報を取得する
        /// </summary>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        private List<T04_AGRDTL> getT04_AGRDTL(string slipNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int num;
                if (int.TryParse(slipNumber, out num))
                {
                    var result = context.T04_AGRDTL
                                    .Where(w => w.削除日時 == null && w.伝票番号 == num)
                                    .OrderBy(o => o.伝票番号)
                                    .ThenBy(t => t.行番号);

                    return result.ToList();

                }
                else
                {
                    return new List<T04_AGRDTL>();
                }

            }

        }

        /// <summary>
        /// 揚り明細情報(品番情報含む)を取得する
        /// </summary>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        private List<T04_AGRDTL_Extension> getT04_AGRDTL_Extension(string slipNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int num;
                if (int.TryParse(slipNumber, out num))
                {
                    var result =
                        context.T04_AGRDTL.Where(w => w.削除日時 == null && w.伝票番号 == num)
                            .GroupJoin(context.M09_HIN.Where(w => w.削除日時 == null),
                                x => x.品番コード,
                                y => y.品番コード,
                                (a, b) => new { a, b })
                            // No-65 Start
                            .SelectMany(x => x.b.DefaultIfEmpty(), (x, y) => new { AGRDTL = x, HIN = y })
                            .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null),
                                x => x.HIN.自社色,
                                y => y.色コード,
                                (c, d) => new { c.AGRDTL, c.HIN, d })
                            .SelectMany(x => x.d.DefaultIfEmpty(), (x, y) => new { x.AGRDTL, x.HIN, IRO = y })
                            // No-65 End
                            .Select(x => new T04_AGRDTL_Extension
                            {
                                伝票番号 = x.AGRDTL.a.伝票番号,
                                行番号 = x.AGRDTL.a.行番号,
                                品番コード = x.AGRDTL.a.品番コード,
                                自社品番 = x.HIN.自社品番,
                                自社品名 = x.HIN.自社品名,
                                賞味期限 = x.AGRDTL.a.賞味期限,
                                数量 = x.AGRDTL.a.数量,
                                単位 = x.AGRDTL.a.単位,
                                単価 = x.AGRDTL.a.単価,
                                金額 = x.AGRDTL.a.金額,
                                摘要 = x.AGRDTL.a.摘要,
                                // No-65 Start
                                自社色 = x.HIN.自社色,
                                自社色名 = x.IRO.色名称,
                                // No-65 End
                                消費税区分 = x.HIN.消費税区分 ?? 0,
                                商品分類 = x.HIN.商品分類 ?? 0
                            })
                            .OrderBy(o => o.伝票番号)
                            .ThenBy(t => t.行番号);

                    return result.ToList();

                }
                else
                {
                    return new List<T04_AGRDTL_Extension>();
                }

            }

        }

        /// <summary>
        /// 揚り明細情報の更新をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dt"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private int setT04_AGRDTL_Update(TRAC3Entities context, DataTable dt, int userId)
        {
            // 登録済みデータを物理削除
            int i伝票番号 = ParseNumeric<int>(dt.DataSet.Tables[TABLE_HEADER].Rows[0]["伝票番号"]);

            var delData = context.T04_AGRDTL.Where(w => w.伝票番号 == i伝票番号).ToList();
            if (delData != null)
            {
                foreach (T04_AGRDTL dtl in delData)
                    context.T04_AGRDTL.DeleteObject(dtl);

                context.SaveChanges();

            }

            int rowIdx = 1;
            // 明細追加
            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;

                T04_AGRDTL srdtl = new T04_AGRDTL();
                T04_AGRDTL dtlData = convertDataRowToT04_AGRDTL_Entity(row);

                if (dtlData.品番コード <= 0)
                    continue;

                srdtl.伝票番号 = dtlData.伝票番号;
                srdtl.行番号 = rowIdx;
                srdtl.品番コード = dtlData.品番コード;
                srdtl.賞味期限 = dtlData.賞味期限;
                srdtl.数量 = dtlData.数量;
                srdtl.単位 = dtlData.単位;
                srdtl.単価 = dtlData.単価;
                srdtl.金額 = dtlData.金額;
                srdtl.摘要 = dtlData.摘要;
                srdtl.登録者 = userId;
                srdtl.登録日時 = DateTime.Now;
                srdtl.最終更新者 = userId;
                srdtl.最終更新日時 = DateTime.Now;

                context.T04_AGRDTL.ApplyChanges(srdtl);

                rowIdx++;

            }

            context.SaveChanges();

            return 1;

        }

        #endregion

        #region << 揚り部材明細情報 >>

        /// <summary>
        /// 揚り部材明細情報を取得する
        /// </summary>
        /// <returns></returns>
        private List<T04_AGRDTB> getT04_AGRDTBList()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var result = context.T04_AGRDTB
                                .Where(w => w.削除日時 == null)
                                .OrderBy(o => o.伝票番号)
                                .ThenBy(t => t.行番号);

                return result.ToList();

            }

        }

        /// <summary>
        /// 揚り部材明細情報を取得する
        /// </summary>
        /// <param name="slipNumber">伝票番号</param>
        /// <returns></returns>
        private List<T04_AGRDTB> getT04_AGRDTB(string slipNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int snum;
                if (int.TryParse(slipNumber, out snum))
                {
                    var result = context.T04_AGRDTB
                                    .Where(w => w.削除日時 == null &&
                                        w.伝票番号 == snum)
                                    .OrderBy(o => o.伝票番号)
                                    .ThenBy(t => t.行番号)
                                    .ThenBy(t => t.部材行番号);

                    return result.ToList();

                }
                else
                {
                    return new List<T04_AGRDTB>();
                }

            }

        }

        /// <summary>
        /// 揚り部材明細情報を取得する
        /// </summary>
        /// <param name="slipNumber">伝票番号</param>
        /// <param name="companyCode">入荷先コード</param>
        /// <returns></returns>
        public List<T04_AGRDTB_Extension> getT04_AGRDTB_Extension(string slipNumber, int companyCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int snum;
                DateTime maxDate = (DateTime)AppCommon.DateTimeToDate(DateTime.MaxValue);

                if (int.TryParse(slipNumber, out snum))
                {
                    // 対象伝票の揚り部材明細を定義
                    var result =
                        context.T04_AGRDTL.Where(w => w.削除日時 == null && w.伝票番号 == snum)
                            .Join(context.T04_AGRDTB.Where(w => w.削除日時 == null && w.伝票番号 == snum),
                                x => new { x.伝票番号, x.行番号 },
                                y => new { y.伝票番号, y.行番号 },
                                (a, b) => new { DTL = a, DTB = b })
                            .Join(context.M09_HIN.Where(w => w.削除日時 == null),
                                x => x.DTB.品番コード,
                                y => y.品番コード,
                                (c, d) => new { c.DTL, c.DTB, HIN = d })
                            .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null),
                                x => x.HIN.自社色,
                                y => y.色コード,
                                (e, f) => new { e, f })
                            .SelectMany(x => x.f.DefaultIfEmpty(),
                                (g, h) => new
                                {
                                    セット品番コード = g.e.DTL.品番コード,
                                    品番コード = g.e.DTB.品番コード,
                                    自社品番 = g.e.HIN.自社品番,
                                    自社品名 = g.e.HIN.自社品名,
                                    自社色 = g.e.HIN.自社色,
                                    自社色名 = h.色名称,
                                    賞味期限 = g.e.DTB.賞味期限,
                                    数量 = g.e.DTB.数量 ?? 0,
                                    単位 = g.e.HIN.単位
                                })
                            .GroupJoin(context.M10_SHIN.Where(w => w.削除日時 == null),
                                x => new { 親品番 = x.セット品番コード, 子品番 = x.品番コード },
                                y => new { 親品番 = y.品番コード, 子品番 = y.構成品番コード },
                                (i, j) => new { i, j })
                            .SelectMany(x => x.j.DefaultIfEmpty(),
                                (k, l) => new
                                {
                                    セット品番コード = k.i.セット品番コード,
                                    品番コード = k.i.品番コード,
                                    自社品番 = k.i.自社品番,
                                    自社品名 = k.i.自社品名,
                                    自社色 = k.i.自社色,
                                    自社色名 = k.i.自社色名,
                                    賞味期限 = k.i.賞味期限,
                                    数量 = k.i.数量,
                                    単位 = k.i.単位,
                                    必要数量 = l == null ? 0 : l.使用数量
                                })
                            .GroupJoin(context.S03_STOK.Where(w => w.削除日時 == null),
                                x => new { 会社コード = companyCode, 品番 = x.品番コード, 賞味期限 = (x.賞味期限 ?? maxDate) },
                                y => new { 会社コード = y.倉庫コード, 品番 = y.品番コード, 賞味期限 = y.賞味期限 },
                                (m, n) => new { m, n })
                            .SelectMany(x => x.n.DefaultIfEmpty(),
                                (o, p) => new T04_AGRDTB_Extension
                                {
                                    セット品番コード = o.m.セット品番コード,
                                    品番コード = o.m.品番コード,
                                    自社品番 = o.m.自社品番,
                                    自社品名 = o.m.自社品名,
                                    自社色 = o.m.自社色,
                                    自社色名 = o.m.自社色名,
                                    賞味期限 = o.m.賞味期限,
                                    数量 = o.m.数量,
                                    単位 = o.m.単位,
                                    必要数量 = o.m.必要数量,
                                    在庫数量 = p == null ? 0m : p.在庫数
                                });

                    return result.ToList();

                }
                else
                {
                    return new List<T04_AGRDTB_Extension>();
                }

            }

        }

        /// <summary>
        /// 揚り部材明細情報を作成する
        /// </summary>
        /// <param name="productCode">品番コード</param>
        /// <param name="companyCode">入荷先コード</param>
        /// <returns></returns>
        public List<T04_AGRDTB_Extension> getT04_AGRDTB_Create(string productCode, string companyCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int pnum, code;
                DateTime maxDate = AppCommon.GetMaxDate();

                if (int.TryParse(productCode, out pnum) && int.TryParse(companyCode, out code))
                {
                    var result =
                        context.M10_SHIN.Where(w => w.削除日時 == null && w.品番コード == pnum)
                            .Join(context.M09_HIN.Where(w => w.削除日時 == null),
                                x => x.構成品番コード,
                                y => y.品番コード,
                                (c, d) => new { SHIN = c, HIN = d })
                            .GroupJoin(context.S03_STOK.Where(w => w.削除日時 == null),
                                x => new { 会社コード = code, 品番コード = x.SHIN.構成品番コード },
                                y => new { 会社コード = y.倉庫コード, y.品番コード },
                                (c, d) => new { c.SHIN, c.HIN, d })
                            .SelectMany(x => x.d.DefaultIfEmpty(),
                                (e, f) => new
                                    {
                                        セット品番コード = e.SHIN.品番コード,
                                        自社品番 = e.HIN.自社品番,
                                        自社品名 = e.HIN.自社品名,
                                        自社色 = e.HIN.自社色,
                                        品番コード = e.SHIN.構成品番コード,
                                        賞味期限 = f.賞味期限,
                                        単位 = e.HIN.単位,
                                        必要数量 = e.SHIN.使用数量,
                                        在庫数量 = f == null ? 0m : f.在庫数
                                    })
                            .GroupJoin(
                                context.M06_IRO.Where(w => w.削除日時 == null),
                                x => x.自社色,
                                y => y.色コード,
                                (g, h) => new { g, h })
                            .SelectMany(x => x.h.DefaultIfEmpty(),
                                (i, j) => new T04_AGRDTB_Extension
                                    {
                                        セット品番コード = i.g.セット品番コード,
                                        自社品番 = i.g.自社品番,
                                        自社品名 = i.g.自社品名,
                                        自社色 = i.g.自社色,
                                        自社色名 = j.色名称,
                                        品番コード = i.g.品番コード,
                                        賞味期限 = i.g.賞味期限,
                                        //数量 = i.g.数量,
                                        単位 = i.g.単位,
                                        必要数量 = i.g.必要数量,
                                        在庫数量 = i.g.在庫数量
                                    });

                    if (result.ToList().Count == 0)
                    {
                        // セット品番マスタなしとして品番情報のみ取得する
                        result =
                            context.M09_HIN.Where(w => w.削除日時 == null && w.品番コード == pnum)
                                .GroupJoin(context.S03_STOK.Where(w => w.削除日時 == null),
                                    x => new { 会社コード = code, 品番コード = x.品番コード },
                                    y => new { 会社コード = y.倉庫コード, y.品番コード },
                                    (a, b) => new { HIN = a, d = b })
                                .SelectMany(x => x.d.DefaultIfEmpty(),
                                    (c, d) => new
                                    {
                                        セット品番コード = c.HIN.品番コード,
                                        自社品番 = c.HIN.自社品番,
                                        自社品名 = c.HIN.自社品名,
                                        自社色 = c.HIN.自社色,
                                        品番コード = c.HIN.品番コード,
                                        賞味期限 = d.賞味期限,
                                        単位 = c.HIN.単位,
                                        在庫数量 = d == null ? 0m : d.在庫数
                                    })
                                .GroupJoin(
                                    context.M06_IRO.Where(w => w.削除日時 == null),
                                    x => x.自社色,
                                    y => y.色コード,
                                    (e, f) => new { g = e, h = f })
                                .SelectMany(x => x.h.DefaultIfEmpty(),
                                    (g, h) => new T04_AGRDTB_Extension
                                    {
                                        セット品番コード = g.g.セット品番コード,
                                        自社品番 = g.g.自社品番,
                                        自社品名 = g.g.自社品名,
                                        自社色 = g.g.自社色,
                                        自社色名 = h.色名称,
                                        品番コード = g.g.品番コード,
                                        賞味期限 = g.g.賞味期限,
                                        //数量 = i.g.数量,
                                        単位 = g.g.単位,
                                        必要数量 = 0m,
                                        在庫数量 = g.g.在庫数量
                                    });

                    }

                    return result.ToList();

                }
                else
                {
                    return new List<T04_AGRDTB_Extension>();
                }

            }

        }

        /// <summary>
        /// 揚り部材明細情報の更新をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="slipNumber">伝票番号</param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private int setT04_AGRDTB_Update(TRAC3Entities context, int slipNumber, int userId)
        {
            // 部材明細の対象伝票データを削除
            var delResult =
                context.T04_AGRDTB.Where(w => w.伝票番号 == slipNumber);
            foreach (var data in delResult)
                context.T04_AGRDTB.DeleteObject(data);

            // 対象伝票の明細情報を取得
            var dtlResult =
                context.T04_AGRDTL.Where(w => w.伝票番号 == slipNumber)
                    .OrderBy(o => o.行番号);

            foreach (var data in dtlResult)
            {
                int dtlRowNum = 1;

                // セット品番情報を取得
                var setResult =
                    context.M10_SHIN
                        .Where(w => w.削除日時 == null && w.品番コード == data.品番コード)
                        .OrderBy(o => o.部品行);

                if (setResult == null || setResult.Count() == 0)
                {
                    // セット品番情報なし
                    // ⇒自身の情報を部材明細として設定
                    T04_AGRDTB dtb = new T04_AGRDTB();

                    dtb.伝票番号 = data.伝票番号;
                    dtb.行番号 = data.行番号;
                    dtb.部材行番号 = dtlRowNum;
                    dtb.品番コード = data.品番コード;
                    dtb.賞味期限 = data.賞味期限;
                    dtb.数量 = data.数量;
                    dtb.登録者 = userId;
                    dtb.登録日時 = DateTime.Now;
                    dtb.最終更新者 = userId;
                    dtb.最終更新日時 = DateTime.Now;

                    context.T04_AGRDTB.ApplyChanges(dtb);

                    dtlRowNum++;

                }
                else
                {
                    foreach (var set in setResult)
                    {
                        T04_AGRDTB dtb = new T04_AGRDTB();

                        dtb.伝票番号 = data.伝票番号;
                        dtb.行番号 = data.行番号;
                        dtb.部材行番号 = dtlRowNum;
                        dtb.品番コード = set.構成品番コード;
                        dtb.賞味期限 = null;
                        dtb.数量 = set.使用数量 * data.数量;
                        dtb.登録者 = userId;
                        dtb.登録日時 = DateTime.Now;
                        dtb.最終更新者 = userId;
                        dtb.最終更新日時 = DateTime.Now;

                        context.T04_AGRDTB.ApplyChanges(dtb);

                        dtlRowNum++;

                    }

                }

            }

            context.SaveChanges();

            return 1;

        }

        #endregion


        #region << 在庫更新 >>

        /// <summary>
        /// 在庫情報の更新をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ds">
        /// 揚りデータセット
        /// [0:T04_AGRHD、1:T04_AGRDTL]
        /// </param>
        /// <param name="userId">ユーザID</param>
        /// <returns></returns>
        private int setS03_STOK_Update(TRAC3Entities context, DataSet ds, int userId)
        {
            S04 historyService = new S04(context, userId, S04.機能ID.揚り入力);

            DataRow hrow = ds.Tables[TABLE_HEADER].Rows[0];
            T04_AGRHD hdRow = convertDataRowToT04_AGRHD_Entity(hrow);

            int 倉庫コード = get倉庫コード(context, hdRow.入荷先コード);

            DataTable dtlTbl = ds.Tables[TABLE_DETAIL];
            foreach (DataRow row in dtlTbl.Rows)
            {
                T04_AGRDTL dtlRow = convertDataRowToT04_AGRDTL_Entity(row);

                if (dtlRow.品番コード <= 0)
                    continue;

                S03_STOK stok = new S03_STOK();
                DateTime dt = AppCommon.DateTimeToDate(dtlRow.賞味期限, DateTime.MaxValue);

                var dS03 = context.S03_STOK
                                .Where(x =>
                                    x.倉庫コード == 倉庫コード &&
                                    x.品番コード == dtlRow.品番コード &&
                                    x.賞味期限 == dt)
                                .FirstOrDefault();
                bool isAddData = (dS03 == null);
                decimal stockQty = 0;

                if (row.RowState == DataRowState.Deleted)
                {
                    // 数量分在庫数を減算
                    stockQty = (dtlRow.数量 ?? 0) * -1;
                }
                else if (row.RowState == DataRowState.Added)
                {
                    // 数量分在庫数を加算
                    stockQty = dtlRow.数量 ?? 0;

                }
                else if (row.RowState == DataRowState.Modified)
                {
                    // オリジナル(変更前数量)と比較して差分数量を加減算
                    if (row.HasVersion(DataRowVersion.Original))
                    {
                        decimal orgQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]);
                        stockQty = (dtlRow.数量 ?? 0) - orgQty;
                    }

                }
                else
                {
                    // 対象なし(DataRowState.Unchanged)
                    continue;
                }

                #region 入出庫データ作成

                S04_HISTORY history = new S04_HISTORY();

                history.入出庫日 = hdRow.仕上り日;
                history.入出庫時刻 = DateTime.Now.TimeOfDay;
                history.倉庫コード = 倉庫コード;
                history.入出庫区分 = (int)Const.入出庫区分.ID01_入庫;
                history.品番コード = dtlRow.品番コード;
                history.賞味期限 = dtlRow.賞味期限 == DateTime.MaxValue ? (DateTime?)null : dtlRow.賞味期限;
                history.数量 = Math.Abs(decimal.ToInt32(stockQty));
                history.伝票番号 = hdRow.伝票番号;

                historyService.CreateProductHistory(history);

                #endregion

                // 対象データ設定
                if (isAddData)
                {
                    stok.倉庫コード = 倉庫コード;
                    stok.品番コード = dtlRow.品番コード;
                    stok.賞味期限 = dtlRow.賞味期限 ?? DateTime.MaxValue;
                    stok.在庫数 = stockQty;
                    stok.登録者 = userId;
                    stok.登録日時 = DateTime.Now;
                    stok.最終更新者 = userId;
                    stok.最終更新日時 = DateTime.Now;

                    context.S03_STOK.ApplyChanges(stok);

                }
                else
                {
                    dS03.在庫数 = dS03.在庫数 + stockQty;
                    dS03.最終更新者 = userId;
                    dS03.最終更新日時 = DateTime.Now;
                    dS03.削除者 = null;
                    dS03.削除日時 = null;

                    dS03.AcceptChanges();

                }

            }

            return 1;

        }

        /// <summary>
        /// 在庫情報の更新(部材明細)をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ds">
        /// 揚りデータセット
        /// [0:T04_AGRHD、1:T04_AGRDTL]
        /// </param>
        /// <param name="userId">ログインユーザID</param>
        /// <returns></returns>
        private int setS03_STOK_DTB_Update(TRAC3Entities context, DataSet ds, int userId)
        {
            S04 historyService = new S04(context, userId, S04.機能ID.揚り入力);

            DataRow hrow = ds.Tables[TABLE_HEADER].Rows[0];
            T04_AGRHD hdRow = convertDataRowToT04_AGRHD_Entity(hrow);

            int 倉庫コード = get倉庫コード(context, hdRow.入荷先コード);

            DataTable dtlTbl = ds.Tables[TABLE_DETAIL];
            foreach (DataRow row in dtlTbl.Rows)
            {
                T04_AGRDTL dtlRow = convertDataRowToT04_AGRDTL_Entity(row);

                if (dtlRow.品番コード <= 0)
                    continue;

                // 対象のセット品番を取得
                var agrdtbList =
                    context.M10_SHIN
                        .Where(w => w.削除日時 == null && w.品番コード == dtlRow.品番コード);

                // 構成品番がない場合は処理しない
                if (agrdtbList.Count() == 0)
                    continue;

                // 構成品番毎に在庫処理をおこなう
                foreach (var data in agrdtbList)
                {
                    S03_STOK stok = new S03_STOK();
                    DateTime dt = AppCommon.DateTimeToDate(dtlRow.賞味期限, DateTime.MaxValue);

                    // 賞味期限の一番古い在庫を取得
                    var targetStok =
                        context.S03_STOK
                            .Where(x =>
                                x.倉庫コード == 倉庫コード &&
                                x.品番コード == data.構成品番コード)
                            .OrderBy(o => o.賞味期限)
                            .FirstOrDefault();

                    bool isAddData = (targetStok == null);
                    decimal stockQty = 0;
                    int intQuantity = Convert.ToInt32(row["数量"]);         // No-64

                    if (row.RowState == DataRowState.Deleted)
                    {
                        // 数量分在庫数を加算
                        stockQty = data.使用数量 * intQuantity;             // No-64
                    }
                    if (row.RowState == DataRowState.Added)
                    {
                        // 数量分在庫数を減算
                        stockQty = (data.使用数量 * intQuantity) * -1;      // No-64
                    }
                    // No-87 Start    
                    //if (row.RowState == DataRowState.Modified)
                    //{
                    //    // オリジナル(変更前数量)と比較して差分数量を加減算
                    //    if (row.HasVersion(DataRowVersion.Original))
                    //    {
                    //        decimal orgQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]);
                    //        stockQty = (data.使用数量 * orgQty) - (data.使用数量 * intQuantity);
                    //    }
                    //}
                    // No-87 End                    
                    else
                    {
                        // 対象なし(DataRowState.Unchanged or Modified)
                        continue;
                    }

                    #region 入出庫データ作成

                    S04_HISTORY history = new S04_HISTORY();

                    history.入出庫日 = hdRow.仕上り日;
                    history.入出庫時刻 = DateTime.Now.TimeOfDay;
                    history.倉庫コード = 倉庫コード;
                    history.入出庫区分 = stockQty >= 0 ? (int)Const.入出庫区分.ID01_入庫 : (int)Const.入出庫区分.ID02_出庫;
                    history.品番コード = data.構成品番コード;
                    //20190724CB-S
                    //history.賞味期限 = targetStok.賞味期限;
                    if (targetStok == null)
                    {
                        history.賞味期限 = null;
                    }
                    else
                    {
                        history.賞味期限 = targetStok.賞味期限;
                    }
                    //20190724CB-E
                    history.数量 = Math.Abs(decimal.ToInt32(stockQty));
                    history.伝票番号 = hdRow.伝票番号;

                    historyService.CreateProductHistory(history);

                    #endregion

                    // 対象データ設定
                    if (isAddData)
                    {
                        stok.倉庫コード = 倉庫コード;
                        // 20190724CB-S
                        //stok.品番コード = targetStok.品番コード;
                        stok.品番コード = data.構成品番コード;
                        //stok.賞味期限 = AppCommon.DateTimeToDate(targetStok.賞味期限, DateTime.MaxValue);
                        stok.賞味期限 = DateTime.MaxValue;
                        // 20190724CB-E
                        stok.在庫数 = stockQty;
                        stok.登録者 = userId;
                        stok.登録日時 = DateTime.Now;
                        stok.最終更新者 = userId;
                        stok.最終更新日時 = DateTime.Now;

                        context.S03_STOK.ApplyChanges(stok);

                    }
                    else
                    {
                        targetStok.在庫数 = targetStok.在庫数 + stockQty;
                        targetStok.最終更新者 = userId;
                        targetStok.最終更新日時 = DateTime.Now;
                        targetStok.削除者 = null;
                        targetStok.削除日時 = null;

                        targetStok.AcceptChanges();

                    }

                }

            }

            return 1;

        }

        #endregion


        #region << 処理関連 >>

        /// <summary>
        /// 会社名コードから該当の倉庫コードを取得する
        /// </summary>
        /// <param name="context"></param>
        /// <param name="会社名コード">M70_JIS</param>
        /// <returns></returns>
        private int get倉庫コード(TRAC3Entities context, int 会社名コード)
        {
            var souk =
                context.M22_SOUK
                    .Where(w => w.削除日時 == null && w.場所会社コード == 会社名コード && w.寄託会社コード == 会社名コード)
                    .Select(s => s.倉庫コード)
                    .FirstOrDefault();

            return souk;

        }

        /// <summary>
        /// DataRow型をT04_AGRHDエンティティに変換する
        /// </summary>
        /// <param name="drow"></param>
        /// <returns></returns>
        private T04_AGRHD convertDataRowToT04_AGRHD_Entity(DataRow drow)
        {
            T04_AGRHD agrhd = new T04_AGRHD();

            agrhd.伝票番号 = ParseNumeric<int>(drow["伝票番号"]);
            agrhd.会社名コード = ParseNumeric<int>(drow["会社名コード"]);
            if (drow["仕上り日"] != null && !string.IsNullOrEmpty(drow["仕上り日"].ToString()))
                agrhd.仕上り日 = DateTime.Parse(string.Format("{0:yyyy/MM/dd}", drow["仕上り日"]));
            agrhd.加工区分 = ParseNumeric<int>(drow["加工区分"]);
            agrhd.外注先コード = ParseNumeric<int>(drow["外注先コード"]);
            agrhd.外注先枝番 = ParseNumeric<int>(drow["外注先枝番"]);
            agrhd.入荷先コード = ParseNumeric<int>(drow["入荷先コード"]);
            agrhd.備考 = drow["備考"].ToString();
            agrhd.消費税 = ParseNumeric<int>(drow["消費税"]);

            return agrhd;

        }

        /// <summary>
        /// DataRow型をT04_AGRDTLに変換する
        /// </summary>
        /// <param name="drow"></param>
        /// <returns></returns>
        private T04_AGRDTL convertDataRowToT04_AGRDTL_Entity(DataRow drow)
        {
            T04_AGRDTL agrdtl = new T04_AGRDTL();
            DataRow wkRow = drow.Table.Clone().NewRow();

            if (drow.RowState == DataRowState.Deleted)
            {   // 対象が削除行の場合
                // 対象データの参照ができるようにする
                DataTable wkTbl = drow.Table.Copy();
                wkTbl.RejectChanges();

                var orgCode = drow["品番コード", DataRowVersion.Original];

                foreach (DataRow dr in wkTbl.Select(string.Format("品番コード = {0}", orgCode)))
                {
                    wkRow.ItemArray = dr.ItemArray;
                    break;  // 複数取る事はないと思うが念の為
                }

            }
            else
            {
                wkRow.ItemArray = drow.ItemArray;
            }

            agrdtl.伝票番号 = ParseNumeric<int>(wkRow["伝票番号"]);
            agrdtl.行番号 = ParseNumeric<int>(wkRow["行番号"]);
            agrdtl.品番コード = ParseNumeric<int>(wkRow["品番コード"]);
            if (wkRow["賞味期限"] != null && string.IsNullOrEmpty(wkRow["賞味期限"].ToString()))
                agrdtl.賞味期限 = null;
            else
                agrdtl.賞味期限 = DateTime.Parse(wkRow["賞味期限"].ToString());
            agrdtl.数量 = ParseNumeric<decimal>(wkRow["数量"]);
            agrdtl.単位 = wkRow["単位"].ToString();
            agrdtl.単価 = ParseNumeric<decimal>(wkRow["単価"]);
            agrdtl.金額 = ParseNumeric<int>(wkRow["金額"]);
            agrdtl.摘要 = wkRow["摘要"].ToString();

            return agrdtl;

        }

        /// <summary>
        /// オブジェクトを数値型に変換した結果を返す
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        private T ParseNumeric<T>(object obj) where T : struct
        {
            if (obj == null)
                return default(T);

            if (typeof(T) == typeof(int))
            {
                int val = -1;
                if (!int.TryParse(obj.ToString(), out val))
                    return default(T);

                return (T)((object)val);

            }
            else if (typeof(T) == typeof(long))
            {
                long val = -1;
                if (!long.TryParse(obj.ToString(), out val))
                    return default(T);

                return (T)((object)val);

            }
            else if (typeof(T) == typeof(double))
            {
                double val = -1;
                if (!double.TryParse(obj.ToString(), out val))
                    return default(T);

                return (T)((object)val);

            }
            else if (typeof(T) == typeof(decimal))
            {
                decimal val = -1;
                if (!decimal.TryParse(obj.ToString(), out val))
                    return default(T);

                return (T)((object)val);

            }

            return default(T);

        }

        #endregion

        // 20190528CB-S
        /// <summary>
        /// セット品番の構成品登録件数取得
        /// </summary>
        /// <param name="p品番コード"></param>
        /// <returns></returns>
        public int M10_GetCount(int p品番コード)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var m10count = context.M10_SHIN
                    .Where(w => w.品番コード == p品番コード);


                return m10count.Count();
            }

        }

        // 20190528CB-E

        // 20190724CB-S
        /// <summary>
        /// 登録データの商品がS03_STOKに存在するかのチェック
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool STOK_CHECK(DataSet ds, int userId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //登録時に存在しない在庫が含まれる場合falseを返す
                bool result = true;
               
                try
                {
                    DataRow hrow = ds.Tables[TABLE_HEADER].Rows[0];
                    T04_AGRHD hdRow = convertDataRowToT04_AGRHD_Entity(hrow);

                    int 倉庫コード = get倉庫コード(context, hdRow.入荷先コード);

                    DataTable dtlTbl = ds.Tables[TABLE_DETAIL];
                    foreach (DataRow row in dtlTbl.Rows)
                    {
                        T04_AGRDTL dtlRow = convertDataRowToT04_AGRDTL_Entity(row);

                        if (dtlRow.品番コード <= 0)
                            continue;

                        // 対象のセット品番を取得
                        var agrdtbList =
                            context.M10_SHIN
                                .Where(w => w.削除日時 == null && w.品番コード == dtlRow.品番コード);

                        // 構成品番がない場合は処理しない
                        if (agrdtbList.Count() == 0)
                            continue;

                        // 構成品番毎に在庫処理をおこなう
                        foreach (var data in agrdtbList)
                        {
                            S03_STOK stok = new S03_STOK();
                            DateTime dt = AppCommon.DateTimeToDate(dtlRow.賞味期限, DateTime.MaxValue);

                            // 賞味期限の一番古い在庫を取得
                            var targetStok =
                                context.S03_STOK
                                    .Where(x =>
                                        x.倉庫コード == 倉庫コード &&
                                        x.品番コード == data.構成品番コード)
                                    .OrderBy(o => o.賞味期限)
                                    .FirstOrDefault();

                            if (targetStok == null) 
                            {
                                result = false;
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                       
                }

                return result;

            }

            

        }

        // 20190724CB-E

    }



}