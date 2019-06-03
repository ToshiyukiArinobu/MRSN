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
    public class MST90050_TOK
    {
        [DataMember]
        public int 得意先KEY { get; set; }
        [DataMember]
        public int 得意先ID { get; set; }
        [DataMember]
        public string 略称名 { get; set; }
        [DataMember]
        public string 得意先名１ { get; set; }
        [DataMember]
        public string 得意先名２ { get; set; }
        [DataMember]
        public int 取引区分 { get; set; }
    }

    public class MST90050_DRV
    {
        [DataMember]
        public int 乗務員KEY { get; set; }
        [DataMember]
        public int 乗務員ID { get; set; }
        [DataMember]
        public string 乗務員名 { get; set; }
    }

    public class MST90050_CAR
    {
        [DataMember]
        public int 車輌KEY { get; set; }
        [DataMember]
        public int 車輌ID { get; set; }
        [DataMember]
        public string 車輌番号 { get; set; }
    }

    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コード、svc、および config ファイルで同時にクラス名 "DataDriveLogService" を変更できます。
    // 注意: このサービスをテストするために WCF テスト クライアントを起動するには、ソリューション エクスプローラーで DataDriveLogService.svc または DataDriveLogService.svc.cs を選択し、デバッグを開始してください。
    public class M90050 : IM90050
    {

        /// <summary>
        /// M90050_TOKのデータ取得
        /// </summary>
        public List<MST90050_TOK> MST90050_TOK()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from tok in context.M01_TOK
                             where tok.削除日付 == null
                             select new MST90050_TOK
                             {
                                 得意先KEY = tok.得意先KEY,
                                 得意先ID = tok.得意先ID,
                                 略称名 = tok.略称名,
                                 得意先名１ = tok.得意先名１,
                                 得意先名２ = tok.得意先名２,
                                 取引区分 = tok.取引区分,

                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90050_DRVのデータ取得
        /// </summary>
        public List<MST90050_DRV> MST90050_DRV()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from drv in context.M04_DRV
                             where drv.削除日付 == null
                             select new MST90050_DRV
                             {
                                 乗務員KEY = drv.乗務員KEY,
                                 乗務員ID = drv.乗務員ID,
                                 乗務員名 = drv.乗務員名,
                                 
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90050_CARのデータ取得
        /// </summary>
        public List<MST90050_CAR> MST90050_CAR()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from car in context.M05_CAR
                             where  car.削除日付 == null
                             select new MST90050_CAR
                             {
                                 車輌KEY = car.車輌KEY,
                                 車輌ID = car.車輌ID,
                                 車輌番号 = car.車輌番号,
                                 
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90050_TRNのデータ取得
        /// </summary>
        public List<M90050_TRN> SEARCH_MST90050_00(List<int> KeyList, List<int> KeyList2)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from trn in context.T01_TRN
                             where KeyList.Contains(trn.明細番号) && KeyList2.Contains(trn.明細行)
                             select new M90050_TRN
                             {
                                 明細番号 = trn.明細番号,
                                 明細行 = trn.明細行,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90050_UTRNのデータ取得
        /// </summary>
        public List<M90050_UTRN> SEARCH_MST90050_01()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from utrn in context.T02_UTRN
                             select new M90050_UTRN
                             {
                                 明細番号 = utrn.明細番号,
                                 明細行 = utrn.明細行,
                             }).Take(1).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90050_KTRNのデータ取得
        /// </summary>
        public List<M90050_KTRN> SEARCH_MST90050_02()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from ktrn in context.T03_KTRN
                             select new M90050_KTRN
                             {
                                 明細番号 = ktrn.明細番号,
                                 明細行 = ktrn.明細行,
                             }).Take(1).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90050_NYUKデータ取得
        /// </summary>
        public List<M90050_NYUK> SEARCH_MST90050_03()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from nyuk in context.T04_NYUK
                             select new M90050_NYUK
                             {
                                 明細番号 = nyuk.明細番号,
                                 明細行 = nyuk.明細行,
                             }).Take(1).ToList();
                return query;
            }
        }






        /// <summary>
        /// データ取得
        /// </summary>
        public void SEARCH_MST900501(DataSet ds, int no)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                try
                {
                    using (DbTransaction transaction = context.Connection.BeginTransaction())
                    {
                        //変数宣言
                        DataTable dt;
                        dt = ds.Tables["CSV取り込み"];
                        DateTime 登録日時, 更新日時;
                        DateTime? 削除日時, 削除日付;
                        DateTime CheckDate;
                        int init;
                        decimal dec;
                        int Cnt = 0;
                        string sql = string.Empty;
                        int count;

                        switch (no)
                        {
                            case 0:
                                #region TRN
                                foreach (DataRow row in dt.Rows)
                                {

                                    // 新番号取得
                                    var 明細番号M = (from n in context.M88_SEQ
                                                 where n.明細番号ID == 1
                                                 select n
                                                    ).FirstOrDefault();
                                    if (明細番号M == null)
                                    {
                                        break;
                                    }
                                    int p明細番号 = 明細番号M.現在明細番号 + 1;
                                    明細番号M.現在明細番号 = p明細番号;
                                    明細番号M.AcceptChanges();

                                    //IDからKEYを取得
                                    int? code = null;
                                    decimal? nulldec = null;

                                    code = row["得意先KEY"].ToString() == "" ? code : Convert.ToInt32(row["得意先KEY"].ToString());
                                    row["得意先KEY"] = (from m01 in context.M01_TOK where m01.得意先ID == code select m01.得意先KEY).Any() == false ? 0 : (from m01 in context.M01_TOK where m01.得意先ID == code select m01.得意先KEY).First();
                                    code = null;
                                    code = row["車輌KEY"].ToString() == "" ? code : Convert.ToInt32(row["車輌KEY"].ToString());
                                    row["車輌KEY"] = (from m05 in context.M05_CAR where m05.車輌ID == code select m05.車輌KEY).Any() == false ? 0 : (from m05 in context.M05_CAR where m05.車輌ID == code select m05.車輌KEY).First();
                                    code = null;
                                    code = row["支払先KEY"].ToString() == "" ? code : Convert.ToInt32(row["支払先KEY"].ToString());
                                    row["支払先KEY"] = (from m01 in context.M01_TOK where m01.得意先ID == code select m01.得意先KEY).Any() == false ? 0 : (from m01 in context.M01_TOK where m01.得意先ID == code select m01.得意先KEY).First();
                                    code = null;
                                    code = row["乗務員KEY"].ToString() == "" ? code : Convert.ToInt32(row["乗務員KEY"].ToString());
                                    row["乗務員KEY"] = (from m04 in context.M04_DRV where m04.乗務員ID == code select m04.乗務員KEY).Any() == false ? 0 : (from m04 in context.M04_DRV where m04.乗務員ID == code select m04.乗務員KEY).First();

                                    T01_TRN t01 = new T01_TRN();
                                    code = null;
                                    nulldec = null;

                                    t01.明細番号 = p明細番号;
                                    t01.明細行 = 1;
                                    t01.登録日時 = DateTime.Now;
                                    t01.更新日時 = DateTime.Now;
                                    t01.明細区分 = 1;
                                    t01.入力区分 = 3;
                                    t01.請求日付 = Convert.ToDateTime(row["請求日付"].ToString());
                                    t01.支払日付 = Convert.ToDateTime(row["支払日付"].ToString());
                                    t01.配送日付 = Convert.ToDateTime(row["配送日付"].ToString());
                                    t01.配送時間 = Convert.ToDecimal(row["配送時間"].ToString());
                                    t01.得意先KEY = Convert.ToInt32(row["得意先KEY"].ToString());
                                    t01.請求内訳ID = row["請求内訳ID"].ToString() == "" ? code : Convert.ToInt32(row["請求内訳ID"].ToString());
                                    t01.車輌KEY = row["車輌KEY"].ToString() == "" ? code : Convert.ToInt32(row["車輌KEY"].ToString());
                                    t01.車種ID = row["車種ID"].ToString() == "" ? code : Convert.ToInt32(row["車種ID"].ToString());
                                    t01.支払先KEY = row["支払先KEY"].ToString() == "" ? code : Convert.ToInt32(row["支払先KEY"].ToString());
                                    t01.乗務員KEY = row["乗務員KEY"].ToString() == "" ? code : Convert.ToInt32(row["乗務員KEY"].ToString());
                                    t01.自社部門ID = row["自社部門ID"].ToString() == "" ? 0 : Convert.ToInt32(row["自社部門ID"].ToString());
                                    t01.車輌番号 = Convert.ToString(row["車輌番号"].ToString());
                                    t01.支払先名２次 = Convert.ToString(row["支払先名２次"].ToString());
                                    t01.実運送乗務員 = Convert.ToString(row["実運送乗務員"].ToString());
                                    t01.乗務員連絡先 = Convert.ToString(row["乗務員連絡先"].ToString());
                                    t01.請求運賃計算区分ID = row["請求運賃計算区分ID"].ToString() == "" ? 0 : Convert.ToInt32(row["請求運賃計算区分ID"].ToString());
                                    t01.支払運賃計算区分ID = row["支払運賃計算区分ID"].ToString() == "" ? 0 : Convert.ToInt32(row["支払運賃計算区分ID"].ToString());
                                    t01.数量 = row["数量"].ToString() == "" ? 0 : Convert.ToDecimal(row["数量"].ToString());
                                    t01.単位 = Convert.ToString(row["単位"].ToString());
                                    t01.重量 = row["重量"].ToString() == "" ? 0 : Convert.ToDecimal(row["重量"].ToString());
                                    t01.走行ＫＭ = row["走行ＫＭ"].ToString() == "" ? 0 : Convert.ToInt32(row["走行ＫＭ"].ToString());
                                    t01.実車ＫＭ = row["実車ＫＭ"].ToString() == "" ? 0 : Convert.ToInt32(row["実車ＫＭ"].ToString());
                                    t01.待機時間 = row["待機時間"].ToString() == "" ? 0 : Convert.ToDecimal(row["待機時間"].ToString());
                                    t01.売上単価 = row["売上単価"].ToString() == "" ? 0 : Convert.ToDecimal(row["売上単価"].ToString());
                                    t01.売上金額 = row["売上金額"].ToString() == "" ? 0 : Convert.ToInt32(row["売上金額"].ToString());
                                    t01.通行料 = row["通行料"].ToString() == "" ? 0 : Convert.ToInt32(row["通行料"].ToString());
                                    t01.請求割増１ = row["請求割増１"].ToString() == "" ? 0 : Convert.ToInt32(row["請求割増１"].ToString());
                                    t01.請求割増２ = row["請求割増２"].ToString() == "" ? 0 : Convert.ToInt32(row["請求割増２"].ToString());
                                    t01.請求消費税 = row["請求消費税"].ToString() == "" ? 0 : Convert.ToInt32(row["請求消費税"].ToString());
                                    t01.支払単価 = row["支払単価"].ToString() == "" ? 0 : Convert.ToDecimal(row["支払単価"].ToString());
                                    t01.支払金額 = row["支払金額"].ToString() == "" ? 0 : Convert.ToInt32(row["支払金額"].ToString());
                                    t01.支払通行料 = row["支払通行料"].ToString() == "" ? 0 : Convert.ToInt32(row["支払通行料"].ToString());
                                    t01.支払割増１ = row["支払割増１"].ToString() == "" ? 0 : Convert.ToInt32(row["支払割増１"].ToString());
                                    t01.支払割増２ = row["支払割増２"].ToString() == "" ? 0 : Convert.ToInt32(row["支払割増２"].ToString());
                                    t01.支払消費税 = row["支払消費税"].ToString() == "" ? 0 : Convert.ToInt32(row["支払消費税"].ToString());
                                    t01.水揚金額 = row["水揚金額"].ToString() == "" ? 0 : Convert.ToInt32(row["水揚金額"].ToString());
                                    t01.社内区分 = row["社内区分"].ToString() == "" ? 0 : Convert.ToInt32(row["社内区分"].ToString());
                                    t01.請求税区分 = row["請求税区分"].ToString() == "" ? 0 : Convert.ToInt32(row["請求税区分"].ToString());
                                    t01.支払税区分 = row["支払税区分"].ToString() == "" ? 0 : Convert.ToInt32(row["支払税区分"].ToString());
                                    t01.商品ID = row["商品ID"].ToString() == "" ? code : Convert.ToInt32(row["商品ID"].ToString());
                                    t01.商品名 = Convert.ToString(row["商品名"].ToString());
                                    t01.発地ID = row["発地ID"].ToString() == "" ? code : Convert.ToInt32(row["発地ID"].ToString());
                                    t01.発地名 = Convert.ToString(row["発地名"].ToString());
                                    t01.着地ID = row["着地ID"].ToString() == "" ? code : Convert.ToInt32(row["着地ID"].ToString());
                                    t01.着地名 = Convert.ToString(row["着地名"].ToString());
                                    t01.請求摘要ID = row["請求摘要ID"].ToString() == "" ? code : Convert.ToInt32(row["請求摘要ID"].ToString());
                                    t01.請求摘要 = Convert.ToString(row["請求摘要"].ToString());
                                    t01.社内備考ID = row["社内備考ID"].ToString() == "" ? code : Convert.ToInt32(row["社内備考ID"].ToString());
                                    t01.社内備考 = Convert.ToString(row["社内備考"].ToString());
                                    t01.入力者ID = row["入力者ID"].ToString() == "" ? code : Convert.ToInt32(row["入力者ID"].ToString());

                                    context.T01_TRN.ApplyChanges(t01);
                                    context.SaveChanges();


                                    t01.明細番号 = p明細番号;
                                    t01.明細行 = 2;
                                    t01.登録日時 = DateTime.Now;
                                    t01.更新日時 = DateTime.Now;
                                    t01.明細区分 = 1;
                                    t01.入力区分 = 3;
                                    t01.請求日付 = Convert.ToDateTime(row["請求日付"].ToString());
                                    t01.支払日付 = Convert.ToDateTime(row["支払日付"].ToString());
                                    t01.配送日付 = Convert.ToDateTime(row["配送日付"].ToString());
                                    t01.配送時間 = Convert.ToDecimal(row["配送時間"].ToString());
                                    t01.得意先KEY = Convert.ToInt32(row["得意先KEY"].ToString());
                                    t01.請求内訳ID = row["請求内訳ID"].ToString() == "" ? code : Convert.ToInt32(row["請求内訳ID"].ToString());
                                    t01.車輌KEY = row["車輌KEY"].ToString() == "" ? code : Convert.ToInt32(row["車輌KEY"].ToString());
                                    t01.車種ID = row["車種ID"].ToString() == "" ? code : Convert.ToInt32(row["車種ID"].ToString());
                                    t01.支払先KEY = row["支払先KEY"].ToString() == "" ? code : Convert.ToInt32(row["支払先KEY"].ToString());
                                    t01.乗務員KEY = row["乗務員KEY"].ToString() == "" ? code : Convert.ToInt32(row["乗務員KEY"].ToString());
                                    t01.自社部門ID = row["自社部門ID"].ToString() == "" ? 0 : Convert.ToInt32(row["自社部門ID"].ToString());
                                    t01.車輌番号 = Convert.ToString(row["車輌番号"].ToString());
                                    t01.支払先名２次 = Convert.ToString(row["支払先名２次"].ToString());
                                    t01.実運送乗務員 = Convert.ToString(row["実運送乗務員"].ToString());
                                    t01.乗務員連絡先 = Convert.ToString(row["乗務員連絡先"].ToString());
                                    t01.請求運賃計算区分ID = row["請求運賃計算区分ID"].ToString() == "" ? 0 : Convert.ToInt32(row["請求運賃計算区分ID"].ToString());
                                    t01.支払運賃計算区分ID = row["支払運賃計算区分ID"].ToString() == "" ? 0 : Convert.ToInt32(row["支払運賃計算区分ID"].ToString());
                                    t01.数量 = row["数量"].ToString() == "" ? 0 : Convert.ToDecimal(row["数量"].ToString());
                                    t01.単位 = Convert.ToString(row["単位"].ToString());
                                    t01.重量 = row["重量"].ToString() == "" ? 0 : Convert.ToDecimal(row["重量"].ToString());
                                    t01.走行ＫＭ = row["走行ＫＭ"].ToString() == "" ? 0 : Convert.ToInt32(row["走行ＫＭ"].ToString());
                                    t01.実車ＫＭ = row["実車ＫＭ"].ToString() == "" ? 0 : Convert.ToInt32(row["実車ＫＭ"].ToString());
                                    t01.待機時間 = row["待機時間"].ToString() == "" ? 0 : Convert.ToDecimal(row["待機時間"].ToString());
                                    t01.売上単価 = row["売上単価"].ToString() == "" ? 0 : Convert.ToDecimal(row["売上単価"].ToString());
                                    t01.売上金額 = row["売上金額"].ToString() == "" ? 0 : Convert.ToInt32(row["売上金額"].ToString());
                                    t01.通行料 = row["通行料"].ToString() == "" ? 0 : Convert.ToInt32(row["通行料"].ToString());
                                    t01.請求割増１ = row["請求割増１"].ToString() == "" ? 0 : Convert.ToInt32(row["請求割増１"].ToString());
                                    t01.請求割増２ = row["請求割増２"].ToString() == "" ? 0 : Convert.ToInt32(row["請求割増２"].ToString());
                                    t01.請求消費税 = row["請求消費税"].ToString() == "" ? 0 : Convert.ToInt32(row["請求消費税"].ToString());
                                    t01.支払単価 = row["支払単価"].ToString() == "" ? 0 : Convert.ToDecimal(row["支払単価"].ToString());
                                    t01.支払金額 = row["支払金額"].ToString() == "" ? 0 : Convert.ToInt32(row["支払金額"].ToString());
                                    t01.支払通行料 = row["支払通行料"].ToString() == "" ? 0 : Convert.ToInt32(row["支払通行料"].ToString());
                                    t01.支払割増１ = row["支払割増１"].ToString() == "" ? 0 : Convert.ToInt32(row["支払割増１"].ToString());
                                    t01.支払割増２ = row["支払割増２"].ToString() == "" ? 0 : Convert.ToInt32(row["支払割増２"].ToString());
                                    t01.支払消費税 = row["支払消費税"].ToString() == "" ? 0 : Convert.ToInt32(row["支払消費税"].ToString());
                                    t01.水揚金額 = row["水揚金額"].ToString() == "" ? 0 : Convert.ToInt32(row["水揚金額"].ToString());
                                    t01.社内区分 = row["社内区分"].ToString() == "" ? 0 : Convert.ToInt32(row["社内区分"].ToString());
                                    t01.請求税区分 = row["請求税区分"].ToString() == "" ? 0 : Convert.ToInt32(row["請求税区分"].ToString());
                                    t01.支払税区分 = row["支払税区分"].ToString() == "" ? 0 : Convert.ToInt32(row["支払税区分"].ToString());
                                    t01.商品ID = row["商品ID"].ToString() == "" ? code : Convert.ToInt32(row["商品ID"].ToString());
                                    t01.商品名 = Convert.ToString(row["商品名"].ToString());
                                    t01.発地ID = row["発地ID"].ToString() == "" ? code : Convert.ToInt32(row["発地ID"].ToString());
                                    t01.発地名 = Convert.ToString(row["発地名"].ToString());
                                    t01.着地ID = row["着地ID"].ToString() == "" ? code : Convert.ToInt32(row["着地ID"].ToString());
                                    t01.着地名 = Convert.ToString(row["着地名"].ToString());
                                    t01.請求摘要ID = row["請求摘要ID"].ToString() == "" ? code : Convert.ToInt32(row["請求摘要ID"].ToString());
                                    t01.請求摘要 = Convert.ToString(row["請求摘要"].ToString());
                                    t01.社内備考ID = row["社内備考ID"].ToString() == "" ? code : Convert.ToInt32(row["社内備考ID"].ToString());
                                    t01.社内備考 = Convert.ToString(row["社内備考"].ToString());
                                    t01.入力者ID = row["入力者ID"].ToString() == "" ? code : Convert.ToInt32(row["入力者ID"].ToString());

                                    context.T01_TRN.ApplyChanges(t01);
                                    context.SaveChanges();


                                }
                                break;
                                #endregion

                            case 1:
                                #region UTRN
                                foreach (DataRow row in dt.Rows)
                                {
                                    // 新番号取得
                                    var 明細番号M = (from n in context.M88_SEQ
                                                 where n.明細番号ID == 1
                                                 select n
                                                    ).FirstOrDefault();
                                    if (明細番号M == null)
                                    {
                                        break;
                                    }
                                    int p明細番号 = 明細番号M.現在明細番号 + 1;
                                    明細番号M.現在明細番号 = p明細番号;
                                    明細番号M.AcceptChanges();


                                    //IDからKEYを取得
                                    int? code = null;
                                    decimal? nulldec = null;
                                    code = row["車輌KEY"].ToString() == "" ? code : Convert.ToInt32(row["車輌KEY"].ToString());
                                    row["車輌KEY"] = (from m05 in context.M05_CAR where m05.車輌ID == code select m05.車輌KEY).Any() == false ? 0 : (from m05 in context.M05_CAR where m05.車輌ID == code select m05.車輌KEY).First();
                                    code = null;
                                    code = row["乗務員KEY"].ToString() == "" ? code : Convert.ToInt32(row["乗務員KEY"].ToString());
                                    row["乗務員KEY"] = (from m04 in context.M04_DRV where m04.乗務員ID == code select m04.乗務員KEY).Any() == false ? 0 : (from m04 in context.M04_DRV where m04.乗務員ID == code select m04.乗務員KEY).First();

                                    T02_UTRN t02 = new T02_UTRN();
                                    code = null;
                                    nulldec = null;
                                    t02.明細番号 = p明細番号;
                                    t02.明細行 = 1;
                                    t02.登録日時 = DateTime.Now;
                                    t02.更新日時 = DateTime.Now;
                                    t02.明細区分 = 1;
                                    t02.入力区分 = 2;
                                    t02.実運行日開始 = Convert.ToDateTime(row["実運行日開始"].ToString());
                                    t02.実運行日終了 = Convert.ToDateTime(row["実運行日終了"].ToString());
                                    t02.車輌KEY = row["車輌KEY"].ToString() == "" ? code : Convert.ToInt32(row["車輌KEY"].ToString());
                                    t02.乗務員KEY = row["乗務員KEY"].ToString() == "" ? code : Convert.ToInt32(row["乗務員KEY"].ToString());
                                    t02.車種ID = row["車種ID"].ToString() == "" ? code : Convert.ToInt32(row["車種ID"].ToString());
                                    t02.車輌番号 = Convert.ToString(row["車輌番号"].ToString());
                                    t02.自社部門ID = row["自社部門ID"].ToString() == "" ? code : Convert.ToInt32(row["自社部門ID"].ToString());
                                    t02.出庫時間 = row["出庫時間"].ToString() == "" ? nulldec : Convert.ToDecimal(row["出庫時間"].ToString());
                                    t02.帰庫時間 = row["帰庫時間"].ToString() == "" ? nulldec : Convert.ToDecimal(row["帰庫時間"].ToString());
                                    t02.出勤区分ID = row["出勤区分ID"].ToString() == "" ? 0 : Convert.ToInt32(row["出勤区分ID"].ToString());
                                    t02.拘束時間 = row["拘束時間"].ToString() == "" ? nulldec : Convert.ToDecimal(row["拘束時間"].ToString());
                                    t02.運転時間 = row["運転時間"].ToString() == "" ? nulldec : Convert.ToDecimal(row["運転時間"].ToString());
                                    t02.高速時間 = row["高速時間"].ToString() == "" ? nulldec : Convert.ToDecimal(row["高速時間"].ToString());
                                    t02.作業時間 = row["作業時間"].ToString() == "" ? nulldec : Convert.ToDecimal(row["作業時間"].ToString());
                                    t02.待機時間 = row["待機時間"].ToString() == "" ? nulldec : Convert.ToDecimal(row["待機時間"].ToString());
                                    t02.休憩時間 = row["休憩時間"].ToString() == "" ? nulldec : Convert.ToDecimal(row["休憩時間"].ToString());
                                    t02.残業時間 = row["残業時間"].ToString() == "" ? nulldec : Convert.ToDecimal(row["残業時間"].ToString());
                                    t02.深夜時間 = row["深夜時間"].ToString() == "" ? nulldec : Convert.ToDecimal(row["深夜時間"].ToString());
                                    t02.走行ＫＭ = row["走行ＫＭ"].ToString() == "" ? 0 : Convert.ToInt32(row["走行ＫＭ"].ToString());
                                    t02.実車ＫＭ = row["実車ＫＭ"].ToString() == "" ? 0 : Convert.ToInt32(row["実車ＫＭ"].ToString());
                                    t02.輸送屯数 = row["輸送屯数"].ToString() == "" ? 0 : Convert.ToDecimal(row["輸送屯数"].ToString());
                                    t02.出庫ＫＭ = row["出庫ＫＭ"].ToString() == "" ? 0 : Convert.ToInt32(row["出庫ＫＭ"].ToString());
                                    t02.帰庫ＫＭ = row["帰庫ＫＭ"].ToString() == "" ? 0 : Convert.ToInt32(row["帰庫ＫＭ"].ToString());
                                    t02.備考 = Convert.ToString(row["備考"].ToString());
                                    t02.勤務開始日 = Convert.ToDateTime(row["勤務開始日"].ToString());
                                    t02.勤務終了日 = Convert.ToDateTime(row["勤務終了日"].ToString());
                                    t02.労務日 = Convert.ToDateTime(row["労務日"].ToString());
                                    t02.入力者ID = row["入力者ID"].ToString() == "" ? code : Convert.ToInt32(row["入力者ID"].ToString());

                                    context.T02_UTRN.ApplyChanges(t02);
                                    context.SaveChanges();
                                    Cnt += 1;
                                }
                                break;
                                #endregion


                            case 2:
                                #region KTRN 　注：データベースが車輌IDになっている。
                                foreach (DataRow row in dt.Rows)
                                {
                                    // 新番号取得
                                    var 明細番号M = (from n in context.M88_SEQ
                                                 where n.明細番号ID == 1
                                                 select n
                                                    ).FirstOrDefault();
                                    if (明細番号M == null)
                                    {
                                        break;
                                    }
                                    int p明細番号 = 明細番号M.現在明細番号 + 1;
                                    明細番号M.現在明細番号 = p明細番号;
                                    明細番号M.AcceptChanges();

                                    //IDからKEYを取得
                                    int? code = null;
                                    decimal? nulldec = null;
                                    code = row["車輌ID"].ToString() == "" ? code : Convert.ToInt32(row["車輌ID"].ToString());
                                    row["車輌ID"] = (from m05 in context.M05_CAR where m05.車輌ID == code select m05.車輌KEY).Any() == false ? 0 : (from m05 in context.M05_CAR where m05.車輌ID == code select m05.車輌KEY).First();
                                    code = null;
                                    code = row["支払先KEY"].ToString() == "" ? code : Convert.ToInt32(row["支払先KEY"].ToString());
                                    row["支払先KEY"] = (from m01 in context.M01_TOK where m01.得意先ID == code select m01.得意先KEY).Any() == false ? 0 : (from m01 in context.M01_TOK where m01.得意先ID == code select m01.得意先KEY).First();
                                    code = null;
                                    code = row["乗務員KEY"].ToString() == "" ? code : Convert.ToInt32(row["乗務員KEY"].ToString());
                                    row["乗務員KEY"] = (from m04 in context.M04_DRV where m04.乗務員ID == code select m04.乗務員KEY).Any() == false ? 0 : (from m04 in context.M04_DRV where m04.乗務員ID == code select m04.乗務員KEY).First();

                                    T03_KTRN t03 = new T03_KTRN();
                                    code = null;
                                    nulldec = null;

                                    t03.明細番号 = p明細番号;
                                    t03.明細行 = 1;
                                    t03.登録日時 = DateTime.Now;
                                    t03.更新日時 = DateTime.Now;
                                    t03.明細区分 = 1;
                                    t03.入力区分 = 1;
                                    t03.経費発生日 = Convert.ToDateTime(row["経費発生日"].ToString());
                                    t03.車輌ID = row["車輌ID"].ToString() == "" ? code : Convert.ToInt32(row["車輌ID"].ToString());
                                    t03.車輌番号 = Convert.ToString(row["車輌番号"].ToString());
                                    t03.メーター = row["メーター"].ToString() == "" ? code : Convert.ToInt32(row["メーター"].ToString());
                                    t03.乗務員KEY = row["乗務員KEY"].ToString() == "" ? code : Convert.ToInt32(row["乗務員KEY"].ToString());
                                    t03.支払先KEY = row["支払先KEY"].ToString() == "" ? code : Convert.ToInt32(row["支払先KEY"].ToString());
                                    t03.自社部門ID = row["自社部門ID"].ToString() == "" ? code : Convert.ToInt32(row["自社部門ID"].ToString());
                                    t03.経費項目ID = row["経費項目ID"].ToString() == "" ? code : Convert.ToInt32(row["経費項目ID"].ToString());
                                    t03.経費補助名称 = Convert.ToString(row["経費補助名称"].ToString());
                                    t03.単価 = row["単価"].ToString() == "" ? 0 : Convert.ToDecimal(row["単価"].ToString());
                                    t03.内軽油税分 = row["内軽油税分"].ToString() == "" ? nulldec : Convert.ToDecimal(row["内軽油税分"].ToString());
                                    t03.数量 = row["数量"].ToString() == "" ? nulldec : Convert.ToDecimal(row["数量"].ToString());
                                    t03.金額 = row["金額"].ToString() == "" ? code : Convert.ToInt32(row["金額"].ToString());
                                    t03.収支区分 = row["収支区分"].ToString() == "" ? code : Convert.ToInt32(row["収支区分"].ToString());
                                    t03.摘要ID = row["摘要ID"].ToString() == "" ? code : Convert.ToInt32(row["収支区分"].ToString());
                                    t03.摘要名 = Convert.ToString(row["摘要名"].ToString());
                                    t03.入力者ID = row["入力者ID"].ToString() == "" ? code : Convert.ToInt32(row["入力者ID"].ToString());

                                    context.T03_KTRN.ApplyChanges(t03);
                                    context.SaveChanges();
                                    Cnt += 1;
                                }
                                break;
                                #endregion

                            case 3:
                                #region NTRN
                                foreach (DataRow row in dt.Rows)
                                {
                                    // 新番号取得
                                    var 明細番号M = (from n in context.M88_SEQ
                                                 where n.明細番号ID == 1
                                                 select n
                                                    ).FirstOrDefault();
                                    if (明細番号M == null)
                                    {
                                        break;
                                    }
                                    int p明細番号 = 明細番号M.現在明細番号 + 1;
                                    明細番号M.現在明細番号 = p明細番号;
                                    明細番号M.AcceptChanges();


                                    //IDからKEYを取得
                                    int? code = null;
                                    decimal? nulldec = null;
                                    code = row["得意先KEY"].ToString() == "" ? code : Convert.ToInt32(row["得意先KEY"].ToString());
                                    row["得意先KEY"] = (from m01 in context.M01_TOK where m01.得意先ID == code select m01.得意先KEY).Any() == false ? 0 : (from m01 in context.M01_TOK where m01.得意先ID == code select m01.得意先KEY).First();

                                    T04_NYUK t04 = new T04_NYUK();
                                    code = null;
                                    nulldec = null;

                                    t04.明細番号 = p明細番号;
                                    t04.明細行 = 1;
                                    t04.登録日時 = DateTime.Now;
                                    t04.更新日時 = DateTime.Now;
                                    t04.明細区分 = Convert.ToInt32(row["明細区分"].ToString());
                                    t04.入出金日付 = Convert.ToDateTime(row["入出金日付"].ToString());
                                    t04.取引先KEY = Convert.ToInt32(row["得意先KEY"].ToString());
                                    t04.入出金区分 = row["入出金区分"].ToString() == "" ? 0 : Convert.ToInt32(row["入出金区分"].ToString());
                                    t04.入出金金額 = row["入出金金額"].ToString() == "" ? 0 : Convert.ToInt32(row["入出金金額"].ToString());
                                    t04.摘要ID = row["摘要ID"].ToString() == "" ? code : Convert.ToInt32(row["摘要ID"].ToString());
                                    t04.摘要名 = Convert.ToString(row["摘要名"].ToString());
                                    t04.手形日付 = Convert.ToDateTime(row["手形日付"].ToString());
                                    t04.入力者ID = row["入力者ID"].ToString() == "" ? code : Convert.ToInt32(row["入力者ID"].ToString());

                                    context.T04_NYUK.ApplyChanges(t04);
                                    context.SaveChanges();
                                    Cnt += 1;
                                }
                                break;
                                #endregion


                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }
        }

    }
}
