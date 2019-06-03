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
	public class COLS
	{
		private string _name;
		private string _systype;
		private string _avalue;
		public string name { get { return _name; } set { _name = value;} }
		public string systype { get { return _systype; } set { _systype = value; } }
		public string avalue { get { return _avalue; } set { _avalue = value; } }
	}

	public class MST90060_TOK
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

    public class MST90060_DRV
    {
        [DataMember]
        public int 乗務員KEY { get; set; }
        [DataMember]
        public int 乗務員ID { get; set; }
        [DataMember]
        public string 乗務員名 { get; set; }
    }

    public class MST90060_CAR
    {
        [DataMember]
        public int 車輌KEY { get; set; }
        [DataMember]
        public int 車輌ID { get; set; }
        [DataMember]
        public string 車輌番号 { get; set; }
    }

    // 20150723 wada add 摘要ID取得用
    public class MST90060_KEY
    {
        [DataMember]
        public int Key { get; set; }
    }

    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コード、svc、および config ファイルで同時にクラス名 "DataDriveLogService" を変更できます。
    // 注意: このサービスをテストするために WCF テスト クライアントを起動するには、ソリューション エクスプローラーで DataDriveLogService.svc または DataDriveLogService.svc.cs を選択し、デバッグを開始してください。
    public class M90060 : IM90050
    {

        /// <summary>
        /// M90050_TOKのデータ取得
        /// </summary>
        public List<MST90060_TOK> MST90060_TOK()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from tok in context.M01_TOK
                             where tok.削除日付 == null
                             select new MST90060_TOK
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
        public List<MST90060_DRV> MST90060_DRV()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from drv in context.M04_DRV
                             where drv.削除日付 == null
                             select new MST90060_DRV
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
        public List<MST90060_CAR> MST90060_CAR()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from car in context.M05_CAR
                             where  car.削除日付 == null
                             select new MST90060_CAR
                             {
                                 車輌KEY = car.車輌KEY,
                                 車輌ID = car.車輌ID,
                                 車輌番号 = car.車輌番号,
                                 
                             }).ToList();
                return query;

				var spl = "ELECT * FROM Sys.Columns WHERE id = object_id('テーブル名')";

            }
        }



        /// <summary>
        /// M90050_UTRNのデータ取得
        /// </summary>
        public List<M90050_TRN> SEARCH_MST90060_00()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from trn in context.T01_TRN
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
        public List<M90050_UTRN> SEARCH_MST90060_01()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from utrn in context.T02_UTRN
                             select new M90050_UTRN
                             {
                                 明細番号 = utrn.明細番号,
                                 明細行 = utrn.明細行,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90050_KTRNのデータ取得
        /// </summary>
        public List<M90050_KTRN> SEARCH_MST90060_02()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from ktrn in context.T03_KTRN
                             select new M90050_KTRN
                             {
                                 明細番号 = ktrn.明細番号,
                                 明細行 = ktrn.明細行,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90050_NYUKデータ取得
        /// </summary>
        public List<M90050_NYUK> SEARCH_MST90060_03()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from nyuk in context.T04_NYUK
                             select new M90050_NYUK
                             {
                                 明細番号 = nyuk.明細番号,
                                 明細行 = nyuk.明細行,
                             }).ToList();
                return query;
            }
        }





        /// <summary>
        /// データ取得
        /// </summary>
        public void SEARCH_MST900601(DataSet ds, int no)
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
                       
                        switch (no)
                        {
                            case 0:
                                #region TRN


                                foreach (DataRow row in dt.Rows)
                                {

                                    // 20150715 wada add エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
										int i明細番号 = ConvertToInt(row["明細番号"]);
										int i明細行 = ConvertToInt(row["明細行"]);
										// 新番号取得
                                        var 明細番号M = (from n in context.M88_SEQ
                                                     where n.明細番号ID == 1
                                                     select n
                                                        ).FirstOrDefault();
										if (明細番号M.現在明細番号 < i明細番号)
                                        {
                                            int p明細番号 = i明細番号 + 1;
                                            明細番号M.現在明細番号 = p明細番号;
                                            明細番号M.AcceptChanges();
                                        }
										if (i明細番号 == 0)
										{
											i明細番号 = 明細番号M.現在明細番号 + 1;
											i明細行 = 1;

											int p明細番号 = i明細番号;
											明細番号M.現在明細番号 = p明細番号;
											明細番号M.AcceptChanges();
										}

                                        int i得意先KEY = ConvertToInt(row["得意先KEY"]);
                                        int i支払先KEY = ConvertToInt(row["支払先KEY"]);
                                        int i車輌KEY = ConvertToInt(row["車輌KEY"]);
                                        int i乗務員KEY = ConvertToInt(row["乗務員KEY"]);
                                        //得意先
                                        var ret = (from m01 in context.M01_TOK
                                                   where m01.得意先ID == i得意先KEY
                                                   select new MST90060_KEY
                                                   {
                                                       Key = m01.得意先KEY,
                                                   }).AsQueryable();
                                        foreach (var keys in ret)
                                        {
                                            i得意先KEY = keys.Key;
                                        }
                                        //支払先
                                        var ret1 = (from m01 in context.M01_TOK
                                                    where m01.得意先ID == i支払先KEY
                                                    select new MST90060_KEY
                                                    {
                                                        Key = m01.得意先KEY,
                                                    }).AsQueryable();
                                        foreach (var keys in ret1)
                                        {
                                            i支払先KEY = keys.Key;
                                        }
                                        //車輌
                                        var ret2 = (from m05 in context.M05_CAR
                                                    where m05.車輌ID == i車輌KEY
                                                    select new MST90060_KEY
                                                    {
                                                        Key = m05.車輌KEY,
                                                    }).AsQueryable();
                                        foreach (var keys in ret2)
                                        {
                                            i車輌KEY = keys.Key;
                                        }
                                        //乗務員
                                        var ret3 = (from m04 in context.M04_DRV
                                                    where m04.乗務員ID == i乗務員KEY
                                                    select new MST90060_KEY
                                                    {
                                                        Key = m04.乗務員KEY,
                                                    }).AsQueryable();
                                        foreach (var keys in ret3)
                                        {
                                            i乗務員KEY = keys.Key;
                                        }
                                        

                                        T01_TRN t01 = new T01_TRN();
										t01.明細番号 = i明細番号; //ConvertToInt(row["明細番号"]);
										t01.明細行 = i明細行;  //ConvertToInt(row["明細行"]);
                                        t01.登録日時 = DateTime.Now;
                                        t01.更新日時 = DateTime.Now;
                                        t01.明細区分 = ConvertToInt(row["明細区分"]);
                                        t01.入力区分 = ConvertToInt(row["入力区分"]);
                                        t01.請求日付 = ConvertToDateTime(row["請求日付"]);
                                        t01.支払日付 = ConvertToDateTime(row["支払日付"]);
                                        t01.配送日付 = ConvertToDateTime(row["配送日付"]);
                                        t01.配送時間 = ConvertToDecimal(row["配送時間"]);
                                        t01.得意先KEY = i得意先KEY;
                                        t01.請求内訳ID = ConvertToIntNullable(row["請求内訳ID"]);
                                        t01.車輌KEY = i車輌KEY;
                                        t01.車種ID = ConvertToIntNullable(row["車種ID"]);
                                        t01.支払先KEY = i支払先KEY;
                                        t01.乗務員KEY = i乗務員KEY;
                                        t01.自社部門ID = ConvertToInt(row["自社部門ID"]);
                                        t01.車輌番号 = ConvertToStringNullable(row["車輌番号"]);
                                        t01.支払先名２次 = ConvertToStringNullable(row["支払先名２次"]);
                                        t01.実運送乗務員 = ConvertToStringNullable(row["実運送乗務員"]);
                                        t01.乗務員連絡先 = ConvertToStringNullable(row["乗務員連絡先"]);
                                        t01.請求運賃計算区分ID = ConvertToInt(row["請求運賃計算区分ID"]);
                                        t01.支払運賃計算区分ID = ConvertToInt(row["支払運賃計算区分ID"]);
                                        t01.数量 = ConvertToDecimal(row["数量"]);
                                        t01.単位 = ConvertToStringNullable(row["単位"]);
                                        t01.重量 = ConvertToDecimal(row["重量"]);
                                        t01.走行ＫＭ = ConvertToInt(row["走行ＫＭ"]);
                                        t01.実車ＫＭ = ConvertToInt(row["実車ＫＭ"]);
                                        t01.待機時間 = ConvertToDecimal(row["重量"]);
                                        t01.売上単価 = ConvertToDecimal(row["売上単価"]);
                                        t01.売上金額 = ConvertToInt(row["売上金額"]);
                                        t01.通行料 = ConvertToInt(row["通行料"]);
                                        t01.請求割増１ = ConvertToInt(row["請求割増１"]);
                                        t01.請求割増２ = ConvertToInt(row["請求割増２"]);
                                        t01.請求消費税 = ConvertToInt(row["請求消費税"]);
                                        t01.支払単価 = ConvertToDecimal(row["支払単価"]);
                                        t01.支払金額 = ConvertToInt(row["支払金額"]);
                                        t01.支払通行料 = ConvertToInt(row["支払通行料"]);
                                        t01.支払割増１ = ConvertToInt(row["支払割増１"]);
                                        t01.支払割増２ = ConvertToInt(row["支払割増２"]);
                                        t01.支払消費税 = ConvertToInt(row["支払消費税"]);
                                        t01.水揚金額 = ConvertToInt(row["水揚金額"]);
                                        t01.社内区分 = ConvertToInt(row["社内区分"]);
                                        t01.請求税区分 = ConvertToInt(row["請求税区分"]);
                                        t01.支払税区分 = ConvertToInt(row["支払税区分"]);
                                        t01.商品ID = ConvertToIntNullable(row["商品ID"]);
                                        t01.商品名 = ConvertToStringNullable(row["商品名"]);
                                        t01.発地ID = ConvertToIntNullable(row["発地ID"]);
                                        t01.発地名 = ConvertToStringNullable(row["発地名"]);
                                        t01.着地ID = ConvertToIntNullable(row["着地ID"]);
                                        t01.着地名 = ConvertToStringNullable(row["着地名"]);
                                        t01.請求摘要ID = ConvertToIntNullable(row["請求摘要ID"]);
                                        t01.請求摘要 = ConvertToStringNullable(row["請求摘要"]);
                                        t01.社内備考ID = ConvertToIntNullable(row["社内備考ID"]);
                                        t01.社内備考 = ConvertToStringNullable(row["社内備考"]);
                                        t01.入力者ID = ConvertToIntNullable(row["入力者ID"]);

                                        context.T01_TRN.ApplyChanges(t01);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;
                                #endregion

                            case 1:
                                #region UTRN
                                foreach (DataRow row in dt.Rows)
                                {
                                    // 20150715 wada add エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {

										int i明細番号 = ConvertToInt(row["明細番号"]);
										int i明細行 = ConvertToInt(row["明細行"]);
                                        // 新番号取得
                                        var 明細番号M = (from n in context.M88_SEQ
                                                     where n.明細番号ID == 1
                                                     select n
                                                        ).FirstOrDefault();
                                        if (明細番号M.現在明細番号 < i明細番号)
                                        {
                                            int p明細番号 = i明細番号 + 1;
                                            明細番号M.現在明細番号 = p明細番号;
                                            明細番号M.AcceptChanges();
										}
										if (i明細番号 == 0)
										{
											i明細番号 = 明細番号M.現在明細番号 + 1;
											i明細行 = 1;

											int p明細番号 = i明細番号;
											明細番号M.現在明細番号 = p明細番号;
											明細番号M.AcceptChanges();
										}

                                        int i車輌KEY = ConvertToInt(row["車輌KEY"]);
                                        int i乗務員KEY = ConvertToInt(row["乗務員KEY"]);
                                        //車輌
                                        var ret = (from m05 in context.M05_CAR
                                                   where m05.車輌ID == i車輌KEY
                                                   select new MST90060_KEY
                                                   {
                                                       Key = m05.車輌KEY,
                                                   }).AsQueryable();
                                        foreach (var keys in ret)
                                        {
                                            i車輌KEY = keys.Key;
                                        }
                                        //乗務員
                                        var ret1 = (from m04 in context.M04_DRV
                                                    where m04.乗務員ID == i乗務員KEY
                                                    select new MST90060_KEY
                                                    {
                                                        Key = m04.乗務員KEY,
                                                    }).AsQueryable();
                                        foreach (var keys in ret1)
                                        {
                                            i乗務員KEY = keys.Key;
                                        }


                                        T02_UTRN t02 = new T02_UTRN();
										t02.明細番号 = i明細番号; // ConvertToInt(row["明細番号"]);
										t02.明細行 = i明細行; // ConvertToInt(row["明細行"]);
                                        t02.登録日時 = DateTime.Now;
                                        t02.更新日時 = DateTime.Now;
                                        t02.明細区分 = 1;
                                        t02.入力区分 = 2;
                                        t02.実運行日開始 = ConvertToDateTimeNullable(row["実運行日開始"]);
                                        t02.実運行日終了 = ConvertToDateTimeNullable(row["実運行日終了"]);
                                        t02.車輌KEY = i車輌KEY;
                                        t02.乗務員KEY = i乗務員KEY;
                                        t02.車種ID = ConvertToIntNullable(row["車種ID"]);
                                        t02.車輌番号 = ConvertToStringNullable(row["車輌番号"]);
                                        t02.自社部門ID = ConvertToIntNullable(row["自社部門ID"]);
                                        t02.出庫時間 = ConvertToDecimal(row["出庫時間"]);
                                        t02.帰庫時間 = ConvertToDecimal(row["帰庫時間"]);
                                        t02.出勤区分ID = ConvertToInt(row["出勤区分ID"]);
                                        t02.拘束時間 = ConvertToDecimal(row["拘束時間"]);
                                        t02.運転時間 = ConvertToDecimal(row["運転時間"]);
                                        t02.高速時間 = ConvertToDecimal(row["高速時間"]);
                                        t02.作業時間 = ConvertToDecimal(row["作業時間"]);
                                        t02.待機時間 = ConvertToDecimal(row["待機時間"]);
                                        t02.休憩時間 = ConvertToDecimal(row["休憩時間"]);
                                        t02.残業時間 = ConvertToDecimal(row["残業時間"]);
                                        t02.深夜時間 = ConvertToDecimal(row["深夜時間"]);
                                        t02.走行ＫＭ = ConvertToInt(row["走行ＫＭ"]);
                                        t02.実車ＫＭ = ConvertToInt(row["実車ＫＭ"]);
                                        t02.輸送屯数 = ConvertToDecimal(row["輸送屯数"]);
                                        t02.出庫ＫＭ = ConvertToInt(row["出庫ＫＭ"]);
                                        t02.帰庫ＫＭ = ConvertToInt(row["帰庫ＫＭ"]);
                                        t02.備考 = ConvertToStringNullable(row["備考"]);
                                        t02.勤務開始日 = ConvertToDateTime(row["勤務開始日"]);
                                        t02.勤務終了日 = ConvertToDateTime(row["勤務終了日"]);
                                        t02.労務日 = ConvertToDateTime(row["労務日"]);
                                        t02.入力者ID = ConvertToIntNullable(row["入力者ID"]);
                                        context.T02_UTRN.ApplyChanges(t02);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;
                                #endregion

                            case 2:
                                #region KTRN 　注：データベースが車輌IDになっている。
                                foreach (DataRow row in dt.Rows)
                                {
                                    // 20150715 wada add エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {

										int i明細番号 = ConvertToInt(row["明細番号"]);
										int i明細行 = ConvertToInt(row["明細行"]);
                                        // 新番号取得
                                        var 明細番号M = (from n in context.M88_SEQ
                                                     where n.明細番号ID == 1
                                                     select n
                                                        ).FirstOrDefault();
                                        if (明細番号M.現在明細番号 < i明細番号)
                                        {
                                            int p明細番号 = i明細番号;
                                            明細番号M.現在明細番号 = p明細番号;
                                            明細番号M.AcceptChanges();
										}
										if (i明細番号 == 0)
										{
											i明細番号 = 明細番号M.現在明細番号 + 1;
											i明細行 = 1;

											int p明細番号 = i明細番号;
											明細番号M.現在明細番号 = p明細番号;
											明細番号M.AcceptChanges();
										}

                                        int i支払先KEY = ConvertToInt(row["支払先KEY"]);
                                        int i乗務員KEY = ConvertToInt(row["乗務員KEY"]);
                                        int i車輌KEY = ConvertToInt(row["車輌ID"]);
                                        //支払先
                                        var ret = (from m01 in context.M01_TOK
                                                   where m01.得意先ID == i支払先KEY
                                                   select new MST90060_KEY
                                                   {
                                                       Key = m01.得意先KEY,
                                                   }).AsQueryable();
                                        foreach (var keys in ret)
                                        {
                                            i支払先KEY = keys.Key;
                                        }
                                        //乗務員
                                        var ret1 = (from m04 in context.M04_DRV
                                                    where m04.乗務員ID == i乗務員KEY
                                                    select new MST90060_KEY
                                                    {
                                                        Key = m04.乗務員KEY,
                                                    }).AsQueryable();
                                        foreach (var keys in ret1)
                                        {
                                            i乗務員KEY = keys.Key;
                                        }
                                        //車輌
                                        var ret2 = (from m05 in context.M05_CAR
                                                   where m05.車輌ID == i車輌KEY
                                                   select new MST90060_KEY
                                                   {
                                                       Key = m05.車輌KEY,
                                                   }).AsQueryable();
                                        foreach (var keys in ret2)
                                        {
                                            i車輌KEY = keys.Key;
                                        }


                                        T03_KTRN t03 = new T03_KTRN();
										t03.明細番号 = i明細番号; // ConvertToInt(row["明細番号"]);
										t03.明細行 = i明細行; // ConvertToInt(row["明細行"]);
                                        t03.登録日時 = DateTime.Now;
                                        t03.更新日時 = DateTime.Now;
                                        t03.明細区分 = 1;
                                        t03.入力区分 = 1;
                                        t03.経費発生日 = ConvertToDateTimeNullable(row["経費発生日"]);
                                        t03.車輌ID = i車輌KEY;
                                        t03.車輌番号 = ConvertToStringNullable(row["車輌番号"]);
                                        t03.メーター = ConvertToIntNullable(row["メーター"]);
                                        t03.乗務員KEY = i乗務員KEY;
                                        t03.支払先KEY = i支払先KEY;
                                        t03.自社部門ID = ConvertToIntNullable(row["自社部門ID"]);
                                        t03.経費項目ID = ConvertToIntNullable(row["経費項目ID"]);
                                        t03.経費補助名称 = ConvertToStringNullable(row["経費補助名称"]);
                                        t03.単価 = ConvertToDecimal(row["単価"]);
                                        t03.内軽油税分 = ConvertToDecimal(row["内軽油税分"]);
                                        t03.数量 = ConvertToDecimal(row["数量"]);
                                        t03.金額 = ConvertToInt(row["金額"]);
                                        t03.収支区分 = ConvertToIntNullable(row["収支区分"]);
                                        t03.摘要ID = ConvertToIntNullable(row["摘要ID"]);
                                        t03.摘要名 = ConvertToStringNullable(row["摘要名"]);
                                        t03.入力者ID = ConvertToIntNullable(row["入力者ID"]);

                                        context.T03_KTRN.ApplyChanges(t03);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;
                                #endregion

                            case 3:
                                #region NTRN
                                foreach (DataRow row in dt.Rows)
                                {
                                    // 20150715 wada add エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
										int i明細番号 = ConvertToInt(row["明細番号"]);
										int i明細行 = ConvertToInt(row["明細行"]);
                                        // 新番号取得
                                        var 明細番号M = (from n in context.M88_SEQ
                                                     where n.明細番号ID == 1
                                                     select n
                                                        ).FirstOrDefault();
                                        if (明細番号M.現在明細番号 < i明細番号)
                                        {
                                            int p明細番号 = 明細番号M.現在明細番号 + 1;
                                            明細番号M.現在明細番号 = p明細番号;
                                            明細番号M.AcceptChanges();
										}
										if (i明細番号 == 0)
										{
											i明細番号 = 明細番号M.現在明細番号 + 1;
											i明細行 = 1;

											int p明細番号 = i明細番号;
											明細番号M.現在明細番号 = p明細番号;
											明細番号M.AcceptChanges();
										}

                                        int i得意先KEY = ConvertToInt(row["取引先KEY"]);
                                        //得意先
                                        var ret = (from m01 in context.M01_TOK
                                                   where m01.得意先ID == i得意先KEY
                                                   select new MST90060_KEY
                                                   {
                                                       Key = m01.得意先KEY,
                                                   }).AsQueryable();
                                        foreach (var keys in ret)
                                        {
                                            i得意先KEY = keys.Key;
                                        }
                                       
                                        T04_NYUK t04 = new T04_NYUK();
										t04.明細番号 = i明細番号; // ConvertToInt(row["明細番号"]);
										t04.明細行 = i明細行; // ConvertToInt(row["明細行"]);
                                        t04.登録日時 = DateTime.Now;
                                        t04.更新日時 = DateTime.Now;
                                        t04.明細区分 = ConvertToInt(row["明細区分"]);
                                        t04.入出金日付 = ConvertToDateTimeNullable(row["入出金日付"]);
                                        t04.取引先KEY = i得意先KEY;
                                        t04.入出金区分 = ConvertToInt(row["入出金区分"]);
                                        t04.入出金金額 = ConvertToInt(row["入出金金額"]);
                                        t04.摘要ID = ConvertToIntNullable(row["摘要ID"]);
                                        t04.摘要名 = ConvertToStringNullable(row["摘要名"]);
                                        t04.手形日付 = ConvertToDateTimeNullable(row["手形日付"]);
                                        t04.入力者ID = ConvertToIntNullable(row["入力者ID"]);

                                        context.T04_NYUK.ApplyChanges(t04);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
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

        /// <summary>
        /// 20150731 wada add
        /// int型に変換する。空欄の場合はNULLを返す。
        /// </summary>
        /// <param name="row">対象の値</param>
        /// <returns>変換後の値(空欄の場合はNULL)</returns>
        private int? ConvertToIntNullable(object row)
        {
            if (row == null)
            {
                return null;
            }
            else
            {
                if (row.ToString() == string.Empty)
                {
                    return null;
                }
                else
                {
                    return Convert.ToInt32(row.ToString());
                }
            }
        }

        /// <summary>
        /// 20150731 wada add
        /// int型に変換する。空欄の場合は0を返す。
        /// </summary>
        /// <param name="row">対象の値</param>
        /// <returns>変換後の値(空欄の場合は0)</returns>
        private int ConvertToInt(object row)
        {
            if (row == null)
            {
                return 0;
            }
            else
            {
                if (row.ToString() == string.Empty)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(row.ToString());
                }
            }
        }

        /// <summary>
        /// 20150731 wada add
        /// string型に変換する。空欄の場合はNULLを返す。
        /// </summary>
        /// <param name="row">対象の値</param>
        /// <returns>変換後の値(空欄の場合はNULL)</returns>
        private string ConvertToStringNullable(object row)
        {
            if (row == null)
            {
                return null;
            }
            else
            {
                if (row.ToString() == string.Empty)
                {
                    return null;
                }
                else
                {
                    return row.ToString();
                }
            }
        }

        /// <summary>
        /// 20150731 wada add
        /// string型に変換する。空欄の場合は空文字を返す。
        /// </summary>
        /// <param name="row">対象の値</param>
        /// <returns>変換後の値(空欄の場合は空文字)</returns>
        private string ConvertToString(object row)
        {
            if (row == null)
            {
                return string.Empty;
            }
            else
            {
                if (row.ToString() == string.Empty)
                {
                    return string.Empty;
                }
                else
                {
                    return row.ToString();
                }
            }
        }

        /// <summary>
        /// 20150731 wada add
        /// datetime型に変換する。空欄の場合はNULLを返す。
        /// </summary>
        /// <param name="row">対象の値</param>
        /// <returns>変換後の値(空欄の場合はNULL)</returns>
        private DateTime? ConvertToDateTimeNullable(object row)
        {
            if (row == null)
            {
                return null;
            }
            else
            {
                if (row.ToString() == string.Empty)
                {
                    return null;
                }
                else
                {
                    return Convert.ToDateTime(row.ToString());
                }
            }
        }

        /// <summary>
        /// 20150731 wada add
        /// datetime型に変換する。空欄の場合は1900年1月1日を返す。
        /// </summary>
        /// <param name="row">対象の値</param>
        /// <returns>変換後の値(空欄の場合は1900年1月1日)</returns>
        private DateTime ConvertToDateTime(object row)
        {
            if (row == null)
            {
                return new DateTime(1900, 1, 1);
            }
            else
            {
                if (row.ToString() == string.Empty)
                {
                    return new DateTime(1900, 1, 1);
                }
                else
                {
                    return Convert.ToDateTime(row.ToString());
                }
            }
        }

        /// <summary>
        /// 20150731 wada add
        /// decimal型に変換する。空欄の場合はNULLを返す。
        /// </summary>
        /// <param name="row">対象の値</param>
        /// <returns>変換後の値(空欄の場合はNULL)</returns>
        private decimal? ConvertToDecimalNullable(object row)
        {
            if (row == null)
            {
                return null;
            }
            else
            {
                if (row.ToString() == string.Empty)
                {
                    return null;
                }
                else
                {
                    return Convert.ToDecimal(row.ToString());
                }
            }
        }

        /// <summary>
        /// 20150731 wada add
        /// decimal型に変換する。空欄の場合は0を返す。
        /// </summary>
        /// <param name="row">対象の値</param>
        /// <returns>変換後の値(空欄の場合は0)</returns>
        private decimal ConvertToDecimal(object row)
        {
            if (row == null)
            {
                return 0;
            }
            else
            {
                if (row.ToString() == string.Empty)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDecimal(row.ToString());
                }
            }
        }

    }
}

