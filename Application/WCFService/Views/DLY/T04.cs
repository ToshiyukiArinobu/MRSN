﻿using KyoeiSystem.Application.WCFService;
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
        /// <summary>部材明細テーブル名</summary>
        private const string TABLE_MAISAI_EXTENT = "T04_AGRDTB_Extension";
        /// <summary>在庫テーブル名</summary>
        private const string TABLE_STOCK = "M73_ZEI";
        /// <summary>確定データ テーブル名</summary>
        private const string TABLE_FIX = "S11_KAKUTEI";

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
            public int 商品形態分類 { get; set; }          // No-279 Add
        }

        /// <summary>
        /// 揚り部材明細情報拡張クラス
        /// </summary>
        public class T04_AGRDTB_Extension
        {
            public int セット品番コード { get; set; }
            public int 品番コード { get; set; }
            public string 自社品番 { get; set; }
            public string 自社品名 { get; set; }
            public string 自社色 { get; set; }
            public string 自社色名 { get; set; }
            public DateTime? 賞味期限 { get; set; }
            public decimal 数量 { get; set; }
            public string 単位 { get; set; }
            public decimal 必要数量 { get; set; }
            public decimal 在庫数量 { get; set; }
            public int 行番号 { get; set; }
            public int 部材行番号 { get; set; }
            /// <summary>1:食品、2:繊維、3:その他</summary>
            public int 商品分類 { get; set; }
        }

        #endregion

        #region << サービス定義 >>

        /// <summary>移動情報サービス</summary>
        T05 T05Service;
        /// <summary>在庫情報サービス</summary>
        S03 S03Service;
        /// <summary>入出庫履歴サービス</summary>
        S04 S04Service;

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
            List<DLY03010.S11_KAKUTEI_INFO> fixList = getS11_KAKUTEI_Extension(hdList);

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
            DataTable dtfix = KESSVCEntry.ConvertListToDataTable(fixList);

            dthd.TableName = TABLE_HEADER;
            t04ds.Tables.Add(dthd);

            dtdtl.TableName = TABLE_DETAIL;
            t04ds.Tables.Add(dtdtl);

            dtdtb.TableName = TABLE_MAISAI;
            t04ds.Tables.Add(dtdtb);

            dttax.TableName = TABLE_STOCK;
            t04ds.Tables.Add(dttax);

            dtfix.TableName = TABLE_FIX;
            t04ds.Tables.Add(dtfix);

            return t04ds;

        }

        /// <summary>
        /// 揚り入力情報を登録・更新する
        /// </summary>
        /// <param name="ds">
        /// 揚りデータセット
        /// [0:T04_AGRHD、1:T04_AGRDTL]
        /// </param>
        /// <param name="ds">データセット</param>
        /// <param name="userId">ログインユーザID</param>
        /// <returns></returns>
        public int Update(DataSet ds, int userId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                using (var tran = context.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    S03Service = new S03(context, userId);
                    S04Service = new S04(context, userId, S04.機能ID.揚り入力);

                    try
                    {
                        DataRow hdRow = ds.Tables[TABLE_HEADER].Rows[0];
                        DataTable dtDetail = ds.Tables[TABLE_DETAIL];
                        DataTable dtAgrdtb = ds.Tables[TABLE_MAISAI_EXTENT];
                        
                        // 1>> ヘッダ情報更新
                        setT04_AGRHD_Update(context, hdRow, userId);

                        // 2>> 明細情報更新
                        setT04_AGRDTL_Update(context, dtDetail, userId);

                        // 3>> 部材明細更新
                        setT04_AGRDTB_Update(context, ParseNumeric<int>(hdRow["伝票番号"]), dtAgrdtb, userId);

                        // 4>> 在庫情報更新
                        // 部材在庫更新
                        //   入出庫履歴の削除
                        setS04_HISTORY_Delete(context, ds);                     // No-258 Add
                        //   在庫の更新(部材)
                        setS03_STOK_DTB_Update(context, ds, userId);            // No-258 Mod
                        //   入出庫履歴の更新(部材)
                        setS04_HISTORY_DTB_Update(context, ds, userId);         // No-258 Mod

                        context.SaveChanges();      // No.124 Add

                        // セット品在庫更新
                        //   在庫の更新(セット品)
                        setS03_STOK_Update(context, ds, userId);                // No-258 Mod    
                        //   入出庫履歴の更新(セット品)
                        setS04_HISTORY_Update(context, ds, userId);             // No-258 Mod

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

                                    // No.156-5 Mod Start
                                    Dictionary<string, string> hstDic = new Dictionary<string, string>()
                                    {
                                        { S04.COLUMNS_NAME_入出庫日, t04agrhd.仕上り日.ToString("yyyy/MM/dd") },
                                        { S04.COLUMNS_NAME_倉庫コード, souk.ToString() },
                                        { S04.COLUMNS_NAME_伝票番号, data.伝票番号.ToString() },
                                        { S04.COLUMNS_NAME_品番コード, data.品番コード.ToString() }
                                    };

                                    // 履歴削除を実行
                                    historyService.DeleteProductHistory(hstDic);
                                    // No.156-5 Mod End
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
                                // No.156-5 Mod Start
                                Dictionary<string, string> hstDic = new Dictionary<string, string>()
                                    {
                                        { S04.COLUMNS_NAME_入出庫日, t04agrhd.仕上り日.ToString("yyyy/MM/dd") },
                                        { S04.COLUMNS_NAME_倉庫コード, souk.ToString() },
                                        { S04.COLUMNS_NAME_伝票番号, agrdtl.伝票番号.ToString() },
                                        { S04.COLUMNS_NAME_品番コード, agrdtl.品番コード.ToString() }
                                    };

                                // 履歴削除を実行
                                historyService.DeleteProductHistory(hstDic);
                                // No.156-5 Mod End

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
                // No.112 Add Start
                srhd.小計 = t04Data.小計;
                srhd.総合計 = t04Data.総合計;
                // No.112 Add End
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
                // No.112 Add Start
                hdData.小計 = t04Data.小計;
                hdData.総合計 = t04Data.総合計;
                // No.112 Add End
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
                                    自社品名 = !string.IsNullOrEmpty(g.e.DTB.自社品名) ? g.e.DTB.自社品名 : g.e.HIN.自社品名,       // No.391 Add
                                    自社色 = g.e.HIN.自社色,
                                    自社色名 = h.色名称,
                                    賞味期限 = g.e.DTB.賞味期限,
                                    数量 = g.e.DTB.数量 ?? 0,
                                    単位 = g.e.HIN.単位,
                                    行番号 = g.e.DTB.行番号, 
                                    部材行番号 = g.e.DTB.部材行番号,
                                    商品分類 = g.e.HIN.商品分類 ?? 3 
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
                                    必要数量 = l == null ? 0 : l.使用数量,
                                    行番号 = k.i.行番号,
                                    部材行番号 = k.i.部材行番号, 
                                    商品分類 = k.i.商品分類
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
                                    在庫数量 = p == null ? 0m : p.在庫数,
                                    行番号 = o.m.行番号,
                                    部材行番号 = o.m.部材行番号,
                                    商品分類 = o.m.商品分類
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
        /// <param name="expirationDate">賞味期限</param>
        /// <returns></returns>
        public List<T04_AGRDTB_Extension> getT04_AGRDTB_Create(string productCode, string companyCode, DateTime expirationDate)
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
                            .GroupJoin(context.S03_STOK.Where(w => w.削除日時 == null && w.賞味期限 >= expirationDate),
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
                                        在庫数量 = f == null ? 0m : f.在庫数,
                                        部材行番号 = e.SHIN.部品行,
                                        商品分類 = e.HIN.商品分類 ?? 3
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
                                        在庫数量 = i.g.在庫数量,
                                        行番号 = 1,
                                        部材行番号 = i.g.部材行番号,
                                        商品分類 = i.g.商品分類
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
                                        在庫数量 = d == null ? 0m : d.在庫数,
                                        商品分類 = c.HIN.商品分類 ?? 3
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
                                        在庫数量 = g.g.在庫数量,
                                        行番号 = 1,
                                        部材行番号 = 1,
                                        商品分類 = g.g.商品分類
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
        /// <param name="dt"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private int setT04_AGRDTB_Update(TRAC3Entities context, int slipNumber, DataTable dt, int userId)
        {
            // 部材明細の対象伝票データを削除
            var delResult =
                context.T04_AGRDTB.Where(w => w.伝票番号 == slipNumber);
            foreach (var data in delResult)
                context.T04_AGRDTB.DeleteObject(data);

            foreach (DataRow row in dt.Rows)
            {
                int dtlRowNum = 1;
                
                T04_AGRDTB dtb = new T04_AGRDTB();
                dtb = convertDataRowToT04_AGRDTB_Entity(row, slipNumber);

                // 下記以外はconvertDataRowToT04_AGRDTB_Entityで移送済み
                dtb.伝票番号 = slipNumber;
                dtb.登録者 = userId;
                dtb.登録日時 = DateTime.Now;
                dtb.最終更新者 = userId;
                dtb.最終更新日時 = DateTime.Now;

                context.T04_AGRDTB.ApplyChanges(dtb);

                dtlRowNum++;

            }

            context.SaveChanges();

            return 1;

        }

        #endregion

        #region 確定情報を取得する
        /// <summary>
        /// 確定情報を取得する
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="urhdList"></param>
        /// <returns></returns>
        private List<DLY03010.S11_KAKUTEI_INFO> getS11_KAKUTEI_Extension(List<T04_AGRHD_Extension> hdList)
        {
            if (!hdList.Any())
            {
                return new List<DLY03010.S11_KAKUTEI_INFO>();
            }

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int company = hdList[0].会社名コード;
                int code = hdList[0].外注先コード;
                int eda = hdList[0].外注先枝番;

                // 取引先情報
                //var tokData = context.M01_TOK.Where(w => w.担当会社コード == company && w.削除日時 == null);
                var tokData = context.M01_TOK.Where(w => w.取引先コード == code && w.枝番 == eda && w.削除日時 == null);

                // 外注先確定データ
                var shiFix =
                    //tokData.Where(w => w.取引先コード == code && w.枝番 == eda)
                    tokData
                        .GroupJoin(context.S11_KAKUTEI.Where(w => w.確定区分 == (int)CommonConstants.確定区分.支払),
                            x => new { jis = x.担当会社コード, tKbn = x.取引区分, closeDay = (int)x.Ｓ締日 },
                            y => new { jis = y.自社コード, tKbn = y.取引区分, closeDay = y.締日 },
                            (x, y) => new { x, y })
                        .SelectMany(z => z.y.DefaultIfEmpty(),
                            (a, b) => new { TOK_S = a.x, FIX_S = b })
                        .Select(s => new DLY03010.S11_KAKUTEI_INFO
                        {
                            取引先コード = s.TOK_S.取引先コード,
                            枝番 = s.TOK_S.枝番,
                            取引区分 = s.TOK_S.取引区分,
                            確定区分 = s.FIX_S.確定区分,
                            確定日 = s.FIX_S.確定日
                        })
                        .Union
                        //(tokData.Where(w => w.取引先コード == code && w.枝番 == eda && w.取引区分 == (int)CommonConstants.取引区分.相殺)
                        (tokData.Where(w => w.取引区分 == (int)CommonConstants.取引区分.相殺)
                        .GroupJoin(context.S11_KAKUTEI.Where(w => w.確定区分 == (int)CommonConstants.確定区分.請求),
                            x => new { jis = x.担当会社コード, tKbn = x.取引区分, closeDay = (int)x.Ｔ締日 },
                            y => new { jis = y.自社コード, tKbn = y.取引区分, closeDay = y.締日 },
                            (x, y) => new { x, y })
                        .SelectMany(z => z.y.DefaultIfEmpty(),
                            (c, d) => new { TOK_SO = c.x, FIX_SO = d })
                        .Select(s => new DLY03010.S11_KAKUTEI_INFO
                        {
                            取引先コード = s.TOK_SO.取引先コード,
                            枝番 = s.TOK_SO.枝番,
                            取引区分 = s.TOK_SO.取引区分,
                            確定区分 = s.FIX_SO.確定区分,
                            確定日 = s.FIX_SO.確定日
                        }));

                return shiFix.ToList();
            }
        }
        #endregion

        #region << 在庫更新 >>

        // No-258 Mod Start
        /// <summary>
        /// 在庫情報の更新をおこなう(セット品)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ds">
        /// 揚りデータセット
        /// [0:T04_AGRHD、1:T04_AGRDTL]
        /// </param>
        /// <param name="context"></param>
        /// <param name="ds"></param>
        /// <param name="userId">ユーザID</param>
        /// <returns></returns>
        private int setS03_STOK_Update(TRAC3Entities context, DataSet ds, int userId)
        {
            // 揚りヘッダ
            DataRow hrow = ds.Tables[TABLE_HEADER].Rows[0];
            T04_AGRHD hdRow = convertDataRowToT04_AGRHD_Entity(hrow);
            int 倉庫コード = get倉庫コード(context, hdRow.入荷先コード);

            // 揚り明細
            DataTable dtlTbl = ds.Tables[TABLE_DETAIL];
            foreach (DataRow row in dtlTbl.Rows)
            {
                T04_AGRDTL dtlRow = convertDataRowToT04_AGRDTL_Entity(row);

                if (dtlRow.品番コード <= 0)
                    continue;

                bool iskigenChangeFlg = false;    // 賞味期限変更フラグ    No-258 Add
                decimal stockQty = 0;
                decimal oldstockQty = 0;

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

                    if (!row["賞味期限"].Equals(row["賞味期限", DataRowVersion.Original]))
                    {
                        // 賞味期限が変更された場合
                        iskigenChangeFlg = true;
                        // 旧賞味期限の在庫数(減算)
                        oldstockQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]) * -1;
                        // 新賞味期限の在庫数(加算)
                        stockQty = (dtlRow.数量 ?? 0);
                    }
                    else
                    {
                        // 数量が変更された場合
                        decimal orgQty = ParseNumeric<decimal>(row["数量", DataRowVersion.Original]);
                        stockQty = (dtlRow.数量 ?? 0) - orgQty;
                    }

                }

                // 賞味期限が変更された場合
                if (iskigenChangeFlg == true)
                {
                    DateTime dt;
                    S03_STOK oldStok = new S03_STOK();
                    oldStok.倉庫コード = 倉庫コード;
                    oldStok.品番コード = dtlRow.品番コード;
                    oldStok.賞味期限 = row["賞味期限", DataRowVersion.Original] == DBNull.Value ? AppCommon.DateTimeToDate(null, DateTime.MaxValue) :
                                        DateTime.TryParse(row["賞味期限", DataRowVersion.Original].ToString(), out dt) ? dt : AppCommon.DateTimeToDate(null, DateTime.MaxValue);
                    oldStok.在庫数 = oldstockQty;

                    // 旧賞味期限の在庫を更新
                    S03Service.S03_STOK_Update(oldStok);
                }

                // 対象データ設定
                if (row.RowState != DataRowState.Unchanged)         // No.156-5 Add
                {
                    S03_STOK stok = new S03_STOK();
                    stok.倉庫コード = 倉庫コード;
                    stok.品番コード = dtlRow.品番コード;
                    stok.賞味期限 = AppCommon.DateTimeToDate(dtlRow.賞味期限, DateTime.MaxValue);
                    stok.在庫数 = stockQty;

                    // 在庫更新
                    S03Service.S03_STOK_Update(stok);
                }

                // 変更状態を確定
                context.SaveChanges();
            }

            return 1;

        }

        /// <summary>
        /// 入出庫履歴の削除をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ds">
        /// 揚りデータセット
        /// [0:T04_AGRHD、1:T04_AGRDTL]
        /// </param>
        /// <param name="context"></param>
        /// <param name="ds"></param>
        /// <param name="userId">ユーザID</param>
        /// <returns></returns>
        private int setS04_HISTORY_Delete(TRAC3Entities context, DataSet ds)
        {
            // 揚りヘッダ
            DataRow hrow = ds.Tables[TABLE_HEADER].Rows[0];
            T04_AGRHD hdRow = convertDataRowToT04_AGRHD_Entity(hrow);
            int intSlipNumber = hdRow.伝票番号;

            // 入出庫データの物理削除
            S04Service.PhysicalDeletionProductHistory(context, intSlipNumber, (int)S04.機能ID.揚り入力);

            return 1;

        }

        /// <summary>
        /// 入出庫履歴の登録・更新をおこなう
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ds">
        /// 揚りデータセット
        /// [0:T04_AGRHD、1:T04_AGRDTL]
        /// </param>
        /// <param name="context"></param>
        /// <param name="ds"></param>
        /// <param name="userId">ユーザID</param>
        /// <returns></returns>
        private int setS04_HISTORY_Update(TRAC3Entities context, DataSet ds, int userId)
        {
            // 揚りヘッダ
            DataRow hrow = ds.Tables[TABLE_HEADER].Rows[0];
            T04_AGRHD hdRow = convertDataRowToT04_AGRHD_Entity(hrow);
            int 倉庫コード = get倉庫コード(context, hdRow.入荷先コード);

            // 揚り明細（DataRowStateの判定に使用）
            DataTable dtlTbl = ds.Tables[TABLE_DETAIL];
            DataTable dtAgrdtb = ds.Tables[TABLE_MAISAI_EXTENT];

            // 不要レコード除去
            DataTable dtlTblTmp = dtlTbl.Clone();
            foreach (DataRow row in dtlTbl.Rows)
            {
                T04_AGRDTL dtlRow = convertDataRowToT04_AGRDTL_Entity(row);

                if (dtlRow.品番コード <= 0)
                {
                    continue;
                }

                dtlTblTmp.ImportRow(row);
            }

            // 入出庫データ作成単位に集約    
            var dtlTblWk = dtlTblTmp.AsEnumerable()
                            .Where(x => x.RowState != DataRowState.Deleted)
                            .GroupBy(g => new
                            {
                                品番コード = g.Field<int>("品番コード"),
                                賞味期限 = g.Field<DateTime?>("賞味期限")
                            })
                            .Select(s => new T04_AGRDTL
                            {
                                品番コード = s.Key.品番コード,
                                賞味期限 = s.Key.賞味期限,
                                数量 = s.Sum(m => m.Field<decimal>("数量"))
                            })
                            .ToList();

            foreach (T04_AGRDTL row in dtlTblWk)
            {
                decimal stockQtyhist = 0;                               // No-155 Add
                stockQtyhist = row.数量 ?? 0;
                S04_HISTORY history = new S04_HISTORY();

                history.入出庫日 = hdRow.仕上り日;
                history.入出庫時刻 = DateTime.Now.TimeOfDay;
                history.倉庫コード = 倉庫コード;
                history.入出庫区分 = (int)CommonConstants.入出庫区分.ID01_入庫;
                history.品番コード = row.品番コード;
                history.賞味期限 = row.賞味期限;
                history.数量 = decimal.ToInt32(stockQtyhist);         // No-155 Mod
                history.伝票番号 = hdRow.伝票番号;

                Dictionary<string, string> hstDic = new Dictionary<string, string>()
                    {
                        { S04.COLUMNS_NAME_入出庫日, history.入出庫日.ToString("yyyy/MM/dd") },
                        { S04.COLUMNS_NAME_倉庫コード, history.倉庫コード.ToString() },
                        { S04.COLUMNS_NAME_伝票番号, history.伝票番号.ToString() },
                        { S04.COLUMNS_NAME_品番コード, history.品番コード.ToString() },
                    };

                // 揚り作成の為、履歴作成
                S04Service.CreateProductHistory(history);
            }

            return 1;

        }

        /// <summary>
        /// 在庫の登録・更新をおこなう(部材明細)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ds">
        /// 揚りデータセット
        /// [0:T04_AGRHD、1:T04_AGRDTL]
        /// </param>
        /// <param name="context"></param>
        /// <param name="ds"></param>
        /// <param name="userId">ログインユーザID</param>
        /// <returns></returns>
        private int setS03_STOK_DTB_Update(TRAC3Entities context, DataSet ds, int userId)
        {
            // 揚りヘッダ
            DataRow hrow = ds.Tables[TABLE_HEADER].Rows[0];
            T04_AGRHD hdRow = convertDataRowToT04_AGRHD_Entity(hrow);
            int 倉庫コード = get倉庫コード(context, hdRow.入荷先コード);

            // 揚り明細（DataRowStateの判定に使用）
            DataTable dtlTbl = ds.Tables[TABLE_DETAIL];
            DataTable dtAgrdtb = ds.Tables[TABLE_MAISAI_EXTENT];

            // 揚り明細レコード数繰り返す
            foreach (DataRow rowDtl in dtlTbl.Rows)
            {
                T04_AGRDTL rowAgrDtl = convertDataRowToT04_AGRDTL_Entity(rowDtl);
                int slipNumber = rowAgrDtl.伝票番号;

                if (rowAgrDtl.品番コード <= 0)
                {
                    continue;
                }

                // 揚り部材明細より行番号に紐づく構成品情報を取得する
                var varRow = dtAgrdtb.AsEnumerable()
                    .Where(w => w.Field<int>("行番号") == rowAgrDtl.行番号)
                    .ToArray();

                // 揚り部材明細レコード数繰り返す
                foreach (DataRow rowAgrdtb in varRow)
                {
                    T04_AGRDTB dtlAgrdtbRow = convertDataRowToT04_AGRDTB_Entity(rowAgrdtb, slipNumber);

                    decimal stockQty = 0;
                    int intQuantity = 0;         // No.131 Mod

                    intQuantity = Convert.ToInt32(rowAgrdtb["数量"]);

                    if (rowDtl.RowState == DataRowState.Added)
                    {
                        // 数量分在庫数を減算
                        stockQty = intQuantity * -1;      // No-64
                    }
                    else if (rowDtl.RowState == DataRowState.Deleted)
                    {
                        // 数量分在庫数を加算
                        stockQty = intQuantity;
                    }

                    if (rowDtl.RowState == DataRowState.Added || rowDtl.RowState == DataRowState.Deleted)         // No.156-5 Add
                    {
                        // 対象データ設定
                        S03_STOK stok = new S03_STOK();
                        stok.倉庫コード = 倉庫コード;
                        stok.品番コード = dtlAgrdtbRow.品番コード;
                        stok.賞味期限 = AppCommon.DateTimeToDate(dtlAgrdtbRow.賞味期限, DateTime.MaxValue);
                        stok.在庫数 = stockQty;

                        // 在庫更新
                        S03Service.S03_STOK_Update(stok);

                    }
                }
                // 変更状態を確定
                context.SaveChanges();
            }

            return 1;

        }

        /// <summary>
        /// 入出庫履歴の登録をおこなう(部材明細)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ds">
        /// 揚りデータセット
        /// [0:T04_AGRHD、1:T04_AGRDTL]
        /// </param>
        /// <param name="context"></param>
        /// <param name="ds"></param>
        /// <param name="userId">ログインユーザID</param>
        /// <returns></returns>
        private int setS04_HISTORY_DTB_Update(TRAC3Entities context, DataSet ds, int userId)
        {
            // 揚りヘッダ
            DataRow hrow = ds.Tables[TABLE_HEADER].Rows[0];
            T04_AGRHD hdRow = convertDataRowToT04_AGRHD_Entity(hrow);

            int 倉庫コード = get倉庫コード(context, hdRow.入荷先コード);

            // 揚り明細（DataRowStateの判定に使用）
            DataTable dtlTbl = ds.Tables[TABLE_DETAIL];
            DataTable dtAgrdtb = ds.Tables[TABLE_MAISAI_EXTENT];

            // 全セット品番の構成品を入出庫単位に集約する
            var dtlTblWk = dtlTbl.AsEnumerable()
                            .Where(x => x.RowState != DataRowState.Deleted )   
                            .Join(dtAgrdtb.AsEnumerable(),
                                x => new { 行番号 = x.Field<int>("行番号") },
                                y => new { 行番号 = y.Field<int>("行番号") },
                                (x, y) => new { AGRDTL = x, AGRDTB = y })
                            .GroupBy(g => new
                            {
                                品番コード = g.AGRDTB.Field<int>("品番コード"),
                                賞味期限 = g.AGRDTB.Field<DateTime?>("賞味期限")
                            })
                            .Select(s => new T04_AGRDTB
                            {
                                品番コード = s.Key.品番コード,
                                賞味期限 = s.Key.賞味期限,
                                数量 = s.Sum(m => m.AGRDTB.Field<decimal>("数量"))
                            })
                            .ToList();

            foreach (T04_AGRDTB row in dtlTblWk)
            {
                DateTime dt = AppCommon.DateTimeToDate(row.賞味期限, DateTime.MaxValue);
                    
                S04_HISTORY history = new S04_HISTORY();

                history.入出庫日 = hdRow.仕上り日;
                history.入出庫時刻 = DateTime.Now.TimeOfDay;
                history.倉庫コード = 倉庫コード;
                history.入出庫区分 = (int)Const.入出庫区分.ID02_出庫;      // No.156-5 Mod
                history.品番コード = row.品番コード;
                if (dt == DateTime.MaxValue.Date)
                {
                    history.賞味期限 = null;
                }
                else
                {
                    history.賞味期限 = dt;
                }

                history.数量 = Decimal.ToInt32(row.数量 ?? 0);
                history.伝票番号 = hdRow.伝票番号;

                // No.156-5 Mod Start
                Dictionary<string, string> hstDic = new Dictionary<string, string>()
                {
                   { S04.COLUMNS_NAME_入出庫日, string.Format("{0:yyyy/MM/dd}", hrow["仕上り日", DataRowVersion.Original]) },
                   { S04.COLUMNS_NAME_倉庫コード, get倉庫コード(context, Convert.ToInt32(hrow["入荷先コード", DataRowVersion.Original])).ToString()},
                   { S04.COLUMNS_NAME_伝票番号,  history.伝票番号.ToString() },
                   { S04.COLUMNS_NAME_品番コード, history.品番コード.ToString() },
                   { S04.COLUMNS_NAME_入出庫区分, history.入出庫区分.ToString() }
                };

                // 揚り作成の為、履歴作成
                S04Service.CreateProductHistory(history);
                // No.156-5 Mod End

            }

            return 1;

        }
        // No-258 Mod End

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
            // No.112 Add Start
            agrhd.小計 = ParseNumeric<int>(drow["小計"]);
            agrhd.総合計 = ParseNumeric<int>(drow["総合計"]);
            // No.112 Add End

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
        /// DataRow型をT04_AGRDTBに変換する
        /// </summary>
        /// <param name="drow"></param>
        /// <returns></returns>
        protected T04_AGRDTB convertDataRowToT04_AGRDTB_Entity(DataRow row, int intslipNumber)
        {
            T04_AGRDTB agrdtb = new T04_AGRDTB();

            agrdtb.伝票番号 = intslipNumber;
            agrdtb.行番号 = ParseNumeric<int>(row["行番号"]);
            agrdtb.部材行番号 = ParseNumeric<int>(row["部材行番号"]);
            agrdtb.品番コード = ParseNumeric<int>(row["品番コード"]);
            agrdtb.自社品名 = row["自社品名"].ToString();                 // No.391 Add
            if (row["賞味期限"] == null || string.IsNullOrEmpty(row["賞味期限"].ToString()))
            {
                agrdtb.賞味期限 = null;
            }
            else
            {
                agrdtb.賞味期限 = DateTime.Parse(row["賞味期限"].ToString());
            }
            agrdtb.数量 = ParseNumeric<decimal>(row["数量"]);

            return agrdtb;

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

        /// <summary>
        /// セット品マスタの構成品情報取得
        /// </summary>
        /// <param name="p品番コード"></param>
        /// <returns></returns>
        public List<M10_SHIN> GetM10_Shin(int p品番コード)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var m10 = context.M10_SHIN
                    .Where(w => w.削除日時 == null && w.品番コード == p品番コード)
                    .ToList();

                return m10;
            }

        }

        /// <summary>
        /// セット品マスタの構成品情報取得（複数品番）
        /// </summary>
        /// <param name="dsAgrDtl">揚り部材明細データセット</param>
        /// <returns></returns>
        public List<M10_SHIN> GetM10_ShinForDataTable(DataSet dsAgrDtl)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                DataTable dtAgrDtl = dsAgrDtl.Tables[TABLE_DETAIL];

                var m10 =
                    dtAgrDtl.AsEnumerable()
                        .Join(context.M10_SHIN.Where(w => w.削除日時 == null),
                            x => new { 品番コード = x.Field<int>("品番コード") },
                            y => new { 品番コード = y.品番コード },
                            (a, b) => new { AGRDTL = a, SHIN = b })
                        .Select(z => new M10_SHIN
                        {
                            品番コード = z.SHIN.品番コード,
                            部品行 = z.SHIN.部品行,
                            構成品番コード = z.SHIN.構成品番コード,
                            使用数量 = z.SHIN.使用数量
                        })
                        .ToList();

                return m10;
            }

        }

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

        #region << 在庫チェック >>
        // No-222 Add Start

        /// <summary>
        /// 在庫数チェックを行う
        /// </summary>
        /// <param name="ds">データセット</param>
        /// <param name="intUserId">ユーザID</param>
        /// <returns></returns>
        public Dictionary<string, string> CheckStockQty(DataSet ds, int intUserId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                Dictionary<string, string> resultDic = new Dictionary<string, string>();

                // 揚りヘッダ.入荷先より、倉庫コードを取得する
                DataRow hrow = ds.Tables[TABLE_HEADER].Rows[0];
                T04_AGRHD hdRow = convertDataRowToT04_AGRHD_Entity(hrow);
                int intSouk = get倉庫コード(context, hdRow.入荷先コード);

                // 揚り部材明細を取得する
                DataTable dtAgrdtb = ds.Tables[TABLE_MAISAI_EXTENT];
                // 構成品番・賞味期限毎に数量を集約する
                List<T04_AGRDTB> dtlList = getDetailDataList(dtAgrdtb);

                Common com = new Common();

                foreach (T04_AGRDTB row in dtlList)
                {
                    if (string.IsNullOrEmpty(row.品番コード.ToString()))
                    {
                        continue;
                    }

                    if (row.数量 == null || row.数量 == 0)
                    {
                        continue;
                    }

                    decimal nowStockQty = 0;
                    if (!com.CheckStokItemQty(intSouk, row.品番コード, row.賞味期限, out nowStockQty, row.数量 ?? 0))
                    {
                        // キー：品番コード+賞味期限、値：エラーメッセージ
                        string strDicKey = string.Concat(row.品番コード.ToString(), "-", row.賞味期限.ToString());
                        resultDic.Add(strDicKey, string.Format("在庫数が不足しています。(現在庫数：{0:#,0.##})", nowStockQty));
                    }

                }

                return resultDic;
            }
        }

        /// <summary>
        /// T04_AGRDTB集約・データ型変換(DataTable→List)
        /// </summary>
        /// <param name="DataTable">データテーブル</param>
        /// <returns></returns>        
        private List<T04_AGRDTB> getDetailDataList(DataTable dt)
        {
            var resultList =
                dt.Select("", "", DataViewRowState.CurrentRows).AsEnumerable().Where(w => w.Field<int?>("品番コード") != null)       // No.423 Mod
                    .GroupBy(g => new { 品番コード = g.Field<int>("品番コード"), 賞味期限 = g.Field<DateTime?>("賞味期限") })
                    .Select(s => new T04_AGRDTB
                    {
                        品番コード = s.Key.品番コード,
                        賞味期限 = s.Key.賞味期限,
                        数量 = s.Sum(m => m.Field<decimal>("数量"))
                    })
                    .ToList();

            return resultList;

        }

        // No-222 Add End
        #endregion

    }



}