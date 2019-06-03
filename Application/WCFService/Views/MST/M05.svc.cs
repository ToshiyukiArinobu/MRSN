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
    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コード、svc、および config ファイルで同時にクラス名 "DataDriveLogService" を変更できます。
    // 注意: このサービスをテストするために WCF テスト クライアントを起動するには、ソリューション エクスプローラーで DataDriveLogService.svc または DataDriveLogService.svc.cs を選択し、デバッグを開始してください。
    public class M05 : IM05
    {

        int num;

        #region M05_CAR
        /// <summary>
        /// M05_CARのデータ取得
        /// </summary>
        /// <param name="p車輌ID">車輌ID</param>
        /// <returns>M05_CAR_Member</returns>
        public List<M05_CAR_Member> GetDataCar(int p車輌ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m05 in context.M05_CAR
                           from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == m05.乗務員KEY).DefaultIfEmpty()
                           where m05.車輌ID == p車輌ID && m05.削除日付 == null
                           select new M05_CAR_Member
                           {
                               車輌ID = m05.車輌ID,
                               車輌KEY = m05.車輌KEY,
                               登録日時 = m05.登録日時,
                               更新日時 = m05.更新日時,
                               車輌番号 = m05.車輌番号,
                               自社部門ID = m05.自社部門ID,
                               車種ID = m05.車種ID == null ? 0 : m05.車種ID,
                               乗務員ID = m04.乗務員ID,
                               運輸局ID = m05.運輸局ID,
                               自傭区分 = m05.自傭区分,
                               廃車区分 = m05.廃車区分,
                               廃車日 = m05.廃車日,
                               次回車検日 = m05.次回車検日,
                               車輌登録番号 = m05.車輌登録番号,
                               登録日 = m05.登録日,
                               初年度登録年 = m05.初年度登録年,
                               初年度登録月 = m05.初年度登録月,
                               自動車種別 = m05.自動車種別,
                               用途 = m05.用途,
                               自家用事業用 = m05.自家用事業用,
                               車体形状 = m05.車体形状,
                               車名 = m05.車名,
                               型式 = m05.型式,
                               乗車定員 = m05.乗車定員,
                               車輌重量 = m05.車輌重量,
                               車輌最大積載量 = m05.車輌最大積載量,
                               車輌総重量 = m05.車輌総重量,
                               車輌実積載量 = m05.車輌実積載量,
                               車台番号 = m05.車台番号,
                               原動機型式 = m05.原動機型式,
                               長さ = m05.長さ,
                               幅 = m05.幅,
                               高さ = m05.高さ,
                               総排気量 = m05.総排気量,
                               燃料種類 = m05.燃料種類,
                               現在メーター = m05.現在メーター,
                               デジタコCD = m05.デジタコCD,
                               所有者名 = m05.所有者名,
                               所有者住所 = m05.所有者住所,
                               使用者名 = m05.使用者名,
                               使用者住所 = m05.使用者住所,
                               使用本拠位置 = m05.使用本拠位置,
                               備考 = m05.備考,
                               型式指定番号 = m05.型式指定番号,
                               前前軸重 = m05.前前軸重,
                               前後軸重 = m05.前後軸重,
                               後前軸重 = m05.後前軸重,
                               後後軸重 = m05.後後軸重,
                               CO2区分 = m05.CO2区分,
                               基準燃費 = m05.基準燃費,
                               黒煙規制値 = m05.黒煙規制値,
                               G車種ID = m05.G車種ID,
                               規制区分ID = m05.規制区分ID,
                               燃料費単価 = m05.燃料費単価,
                               油脂費単価 = m05.油脂費単価,
                               タイヤ費単価 = m05.タイヤ費単価,
                               修繕費単価 = m05.修繕費単価,
                               車検費単価 = m05.車検費単価,
                               固定自動車税 = m05.固定自動車税,
                               固定重量税 = m05.固定重量税,
                               固定取得税 = m05.固定取得税,
                               固定自賠責保険 = m05.固定自賠責保険,
                               固定車輌保険 = m05.固定車輌保険,
                               固定対人保険 = m05.固定対人保険,
                               固定対物保険 = m05.固定対物保険,
                               固定貨物保険 = m05.固定貨物保険,
                               削除日付 = m05.削除日付
                           });
                return ret.ToList();
            }
        }


		/// <summary>
		/// M05_CARの類似データ取得
		/// </summary>
		/// <param name="p車輌ID">車輌ID</param>
		/// <returns>M05_CAR_Member</returns>
		public List<M05_CAR_Member> RGetDataCar(int? p車輌ID)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

				var ret = (from m05 in context.M05_CAR
						   from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == m05.乗務員KEY).DefaultIfEmpty()
						   where m05.車輌ID == p車輌ID && m05.削除日付 == null
						   select new M05_CAR_Member
						   {
							   車輌ID = m05.車輌ID,
							   車輌KEY = m05.車輌KEY,
							   登録日時 = m05.登録日時,
							   更新日時 = m05.更新日時,
							   車輌番号 = m05.車輌番号,
							   自社部門ID = m05.自社部門ID,
							   車種ID = m05.車種ID == null ? 0 : m05.車種ID,
							   乗務員ID = m04.乗務員ID,
							   運輸局ID = m05.運輸局ID,
							   自傭区分 = m05.自傭区分,
							   廃車区分 = m05.廃車区分,
							   廃車日 = m05.廃車日,
							   次回車検日 = m05.次回車検日,
							   車輌登録番号 = m05.車輌登録番号,
							   登録日 = m05.登録日,
							   初年度登録年 = m05.初年度登録年,
							   初年度登録月 = m05.初年度登録月,
							   自動車種別 = m05.自動車種別,
							   用途 = m05.用途,
							   自家用事業用 = m05.自家用事業用,
							   車体形状 = m05.車体形状,
							   車名 = m05.車名,
							   型式 = m05.型式,
							   乗車定員 = m05.乗車定員,
							   車輌重量 = m05.車輌重量,
							   車輌最大積載量 = m05.車輌最大積載量,
							   車輌総重量 = m05.車輌総重量,
							   車輌実積載量 = m05.車輌実積載量,
							   車台番号 = m05.車台番号,
							   原動機型式 = m05.原動機型式,
							   長さ = m05.長さ,
							   幅 = m05.幅,
							   高さ = m05.高さ,
							   総排気量 = m05.総排気量,
							   燃料種類 = m05.燃料種類,
							   現在メーター = m05.現在メーター,
							   デジタコCD = m05.デジタコCD,
							   所有者名 = m05.所有者名,
							   所有者住所 = m05.所有者住所,
							   使用者名 = m05.使用者名,
							   使用者住所 = m05.使用者住所,
							   使用本拠位置 = m05.使用本拠位置,
							   備考 = m05.備考,
							   型式指定番号 = m05.型式指定番号,
							   前前軸重 = m05.前前軸重,
							   前後軸重 = m05.前後軸重,
							   後前軸重 = m05.後前軸重,
							   後後軸重 = m05.後後軸重,
							   CO2区分 = m05.CO2区分,
							   基準燃費 = m05.基準燃費,
							   黒煙規制値 = m05.黒煙規制値,
							   G車種ID = m05.G車種ID,
							   規制区分ID = m05.規制区分ID,
							   燃料費単価 = m05.燃料費単価,
							   油脂費単価 = m05.油脂費単価,
							   タイヤ費単価 = m05.タイヤ費単価,
							   修繕費単価 = m05.修繕費単価,
							   車検費単価 = m05.車検費単価,
							   固定自動車税 = m05.固定自動車税,
							   固定重量税 = m05.固定重量税,
							   固定取得税 = m05.固定取得税,
							   固定自賠責保険 = m05.固定自賠責保険,
							   固定車輌保険 = m05.固定車輌保険,
							   固定対人保険 = m05.固定対人保険,
							   固定対物保険 = m05.固定対物保険,
							   固定貨物保険 = m05.固定貨物保険,
							   削除日付 = m05.削除日付
						   });
				return ret.ToList();
			}
		}

        /// <summary>
        /// M05_CARのデータ取得ボタン用
        /// </summary>
        /// <param name="p車輌コード"></param>
        /// <param name="pオプションコード"></param>
        /// <returns></returns>
        public List<M05_CAR_Member> GetDataCarBtn(int? p車輌ID, int pオプションコード)
        {
            {
                using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
                {
                    context.Connection.Open();

                    var query = (from m05 in context.M05_CAR
                                 from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == m05.乗務員KEY).DefaultIfEmpty()
                                 where m05.削除日付 == null
                                 select new M05_CAR_Member
                                 {
                                     車輌ID = m05.車輌ID,
                                     車輌KEY = m05.車輌KEY,
                                     登録日時 = m05.登録日時,
                                     更新日時 = m05.更新日時,
                                     車輌番号 = m05.車輌番号,
                                     自社部門ID = m05.自社部門ID,
                                     車種ID = m05.車種ID,
                                     乗務員ID = m04.乗務員ID,
                                     運輸局ID = m05.運輸局ID,
                                     自傭区分 = m05.自傭区分,
                                     廃車区分 = m05.廃車区分,
                                     廃車日 = m05.廃車日,
                                     次回車検日 = m05.次回車検日,
                                     車輌登録番号 = m05.車輌登録番号,
                                     登録日 = m05.登録日,
                                     初年度登録年 = m05.初年度登録年,
                                     初年度登録月 = m05.初年度登録月,
                                     自動車種別 = m05.自動車種別,
                                     用途 = m05.用途,
                                     自家用事業用 = m05.自家用事業用,
                                     車体形状 = m05.車体形状,
                                     車名 = m05.車名,
                                     型式 = m05.型式,
                                     乗車定員 = m05.乗車定員,
                                     車輌重量 = m05.車輌重量,
                                     車輌最大積載量 = m05.車輌最大積載量,
                                     車輌総重量 = m05.車輌総重量,
                                     車輌実積載量 = m05.車輌実積載量,
                                     車台番号 = m05.車台番号,
                                     原動機型式 = m05.原動機型式,
                                     長さ = m05.長さ,
                                     幅 = m05.幅,
                                     高さ = m05.高さ,
                                     総排気量 = m05.総排気量,
                                     燃料種類 = m05.燃料種類,
                                     現在メーター = m05.現在メーター,
                                     デジタコCD = m05.デジタコCD,
                                     所有者名 = m05.所有者名,
                                     所有者住所 = m05.所有者住所,
                                     使用者名 = m05.使用者名,
                                     使用者住所 = m05.使用者住所,
                                     使用本拠位置 = m05.使用本拠位置,
                                     備考 = m05.備考,
                                     型式指定番号 = m05.型式指定番号,
                                     前前軸重 = m05.前前軸重,
                                     前後軸重 = m05.前後軸重,
                                     後前軸重 = m05.後前軸重,
                                     後後軸重 = m05.後後軸重,
                                     CO2区分 = m05.CO2区分,
                                     基準燃費 = m05.基準燃費,
                                     黒煙規制値 = m05.黒煙規制値,
                                     G車種ID = m05.G車種ID,
                                     規制区分ID = m05.規制区分ID,
                                     燃料費単価 = m05.燃料費単価,
                                     油脂費単価 = m05.油脂費単価,
                                     タイヤ費単価 = m05.タイヤ費単価,
                                     修繕費単価 = m05.修繕費単価,
                                     車検費単価 = m05.車検費単価,
                                     固定自動車税 = m05.固定自動車税,
                                     固定重量税 = m05.固定重量税,
                                     固定取得税 = m05.固定取得税,
                                     固定自賠責保険 = m05.固定自賠責保険,
                                     固定車輌保険 = m05.固定車輌保険,
                                     固定対人保険 = m05.固定対人保険,
                                     固定対物保険 = m05.固定対物保険,
                                     固定貨物保険 = m05.固定貨物保険,
                                     削除日付 = m05.削除日付
                                 }).AsQueryable();


                    //データが1件もない状態で<< < > >>を押された時の処理
                    if ((p車輌ID == null || p車輌ID == 0) && query.Count() == 0)
                    {
                        return null;
                    }

                    if (p車輌ID != null)
                    {
                        if (p車輌ID == -1)
                        {
                            //全件取得
                            return query.ToList();
                        }

                        if (pオプションコード == 0)
                        {
                            //p車種IDの対象レコードを取得
                            query = query.Where(c => c.削除日付 == null);
                            query = query.Where(c => c.車輌ID == p車輌ID);
                        }
                        else if (pオプションコード == -1)
                        {
                            //p車種IDの1つ前のIDを取得
                            query = query.Where(c => c.削除日付 == null);
                            query = query.Where(c => c.車輌ID <= p車輌ID);
                            if (query.Count() >= 2)
                            {
                                query = query.Where(c => c.車輌ID < p車輌ID);
                            }
                            query = query.OrderByDescending(c => c.車輌ID);
                        }
                        else
                        {
                            //p車種IDDの1つ後のIDを取得
                            query = query.Where(c => c.削除日付 == null);
                            query = query.Where(c => c.車輌ID >= p車輌ID);
                            if (query.Count() >= 2)
                            {
                                query = query.Where(c => c.車輌ID > p車輌ID);
                            }
                            query = query.OrderBy(c => c.車輌ID);
                        }
                    }
                    else
                    {
                        if (pオプションコード == 0)
                        {
                            //車種IDの先頭のIDを取得
                            query = query.Where(c => c.削除日付 == null);
                            query = query.OrderBy(c => c.車輌ID);
                        }
                        else if (pオプションコード == 1)
                        {
                            //車種IDの最後のIDを取得
                            query = query.Where(c => c.削除日付 == null);
                            query = query.OrderByDescending(c => c.車輌ID);
                        }

                    }

                    var ret = query.FirstOrDefault();
                    List<M05_CAR_Member> result = new List<M05_CAR_Member>();
                    if (ret != null)
                    {
                        result.Add(ret);
                    }
                    return result;

                }
            }
        }

        /// <summary>
        /// M05_CAR検索画面用
        /// </summary>
        /// <param name="p車輌ID"></param>
        /// <param name="p廃車"></param>
        /// <returns></returns>
        public List<M05_CAR_Member_SCH> GetDataCarSCH(int p車輌ID, int p廃車区分)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                if (p車輌ID == -1)
                {
                    var ret = (from m05 in context.M05_CAR
                               from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == m05.乗務員KEY).DefaultIfEmpty()
                               where m05.車輌ID > 0
                               select new M05_CAR_Member_SCH
                               {
                                   車輌ID = m05.車輌ID,
                                   車輌番号 = m05.車輌番号,
                                   乗務員名 = m04.乗務員名,
                                   廃車区分 = m05.廃車区分 == 0 ? string.Empty : "廃車済"
                               }).AsQueryable();
                    if (p廃車区分 == 0)
                    {
                        ret = ret.Where(M05_CAR_Member_SCH => M05_CAR_Member_SCH.廃車区分 == string.Empty);
                    }
                    
                    return ret.ToList();

                }
                else
                {
                    var ret = (from m05 in context.M05_CAR
                               from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == m05.乗務員KEY).DefaultIfEmpty()
                               where m05.車輌ID > 0 && m05.車輌ID == p車輌ID
                               select new M05_CAR_Member_SCH
                               {
                                   車輌ID = m05.車輌ID,
                                   車輌番号 = m05.車輌番号,
                                   乗務員名 = m04.乗務員名,
                                   廃車区分 = m05.廃車区分 == 0 ? string.Empty : "廃車済"
                               }).AsQueryable();

                    if (p廃車区分 == 0)
                    {
                        ret = ret.Where(M05_CAR_Member_SCH => M05_CAR_Member_SCH.廃車区分 == string.Empty);
                    }
                    return ret.ToList();
                }
            }
        }

        /// <summary>
        /// M05_CARのデータ取得
        /// </summary>
        /// <param name="p車輌ID">車輌ID</param>
        /// <returns>M05_CAR_Member</returns>
        public List<M05_CAR_Member> GetDataCarIchiran(int p車輌IDFrom, int p車輌IDTo, int p自社部門From, int p自社部門To, string p廃車)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<M05_CAR_Member> retList = new List<M05_CAR_Member>();
                context.Connection.Open();


                //全件表示
                var query = (from m05 in context.M05_CAR
                             where m05.削除日付 == null
                             select new M05_CAR_Member
                             {
                                 車輌ID = m05.車輌ID,
                                 車輌KEY = m05.車輌KEY,
                                 車輌番号 = m05.車輌番号,
                                 自社部門ID = m05.自社部門ID,
                                 車種ID = m05.車種ID,
                                 乗務員ID = m05.乗務員KEY,
                                 運輸局ID = m05.運輸局ID,
                                 自傭区分 = m05.自傭区分,
                                 廃車区分 = m05.廃車区分,
                                 廃車区分表示 = m05.廃車区分 == 0 ? "稼働中" : "廃車済",
                                 廃車日 = m05.廃車日,
                                 次回車検日 = m05.次回車検日,
                                 車輌登録番号 = m05.車輌登録番号,
                                 登録日 = m05.登録日,
                                 初年度登録年 = m05.初年度登録年,
                                 初年度登録月 = m05.初年度登録月,
                                 自動車種別 = m05.自動車種別,
                                 用途 = m05.用途,
                                 自家用事業用 = m05.自家用事業用,
                                 車体形状 = m05.車体形状,
                                 車名 = m05.車名,
                                 型式 = m05.型式,
                                 乗車定員 = m05.乗車定員,
                                 車輌重量 = m05.車輌重量,
                                 車輌最大積載量 = m05.車輌最大積載量,
                                 車輌総重量 = m05.車輌総重量,
                                 車輌実積載量 = m05.車輌実積載量,
                                 車台番号 = m05.車台番号,
                                 原動機型式 = m05.原動機型式,
                                 長さ = m05.長さ,
                                 幅 = m05.幅,
                                 高さ = m05.高さ,
                                 総排気量 = m05.総排気量,
                                 燃料種類 = m05.燃料種類,
                                 現在メーター = m05.現在メーター,
                                 デジタコCD = m05.デジタコCD,
                                 所有者名 = m05.所有者名,
                                 所有者住所 = m05.所有者住所,
                                 使用者名 = m05.使用者名,
                                 使用者住所 = m05.使用者住所,
                                 使用本拠位置 = m05.使用本拠位置,
                                 備考 = m05.備考,
                                 型式指定番号 = m05.型式指定番号,
                                 前前軸重 = m05.前前軸重,
                                 前後軸重 = m05.前後軸重,
                                 後前軸重 = m05.後前軸重,
                                 後後軸重 = m05.後後軸重,
                                 CO2区分 = m05.CO2区分,
                                 基準燃費 = m05.基準燃費,
                                 黒煙規制値 = m05.黒煙規制値,
                                 G車種ID = m05.G車種ID,
                                 規制区分ID = m05.規制区分ID,
                                 燃料費単価 = m05.燃料費単価,
                                 油脂費単価 = m05.油脂費単価,
                                 タイヤ費単価 = m05.タイヤ費単価,
                                 修繕費単価 = m05.修繕費単価,
                                 車検費単価 = m05.車検費単価,
                                 削除日付 = m05.削除日付

                             }).AsQueryable();

                //車輌ID
                query = query.Where(c => (c.車輌ID >= p車輌IDFrom && c.車輌ID <= p車輌IDTo));
                //自社部門ID
                query = query.Where(c => (c.自社部門ID >= p自社部門From && c.自社部門ID <= p自社部門To));


                //廃車区分処理
                switch (p廃車)
                {
                    case "0":
                        //全件表示 何もしない
                        break;

                    case "1":
                        //稼働中のみ表示
                        query = query.Where(c => c.廃車区分 == 0);
                        break;

                    case "2":
                        //稼働中のみ表示
                        query = query.Where(c => c.廃車区分 == 1);
                        break;
                    default:
                        break;
                }

                //データをリスト化
                retList = query.ToList();

                return retList;
            }
        }


        /// <summary>
        /// 現在の登録件数取得
        /// </summary>
        /// <returns></returns>
        public int GetMaxMeisaiCount()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                //context.Connection.Open();
                int query = (from m05 in context.M05_CAR where m05.削除日付 == null select m05.車輌ID).Distinct().Count();
                return query;
            }
        }

        /// <summary>
        /// M05_CARの新規追加
        /// </summary>
        /// <param name="m05car">M05_CAR_Member</param>
        public void Insert(M05_CAR_Member m05car)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var m04ID = (from m04 in context.M04_DRV
                             where m04.乗務員ID == m05car.乗務員ID
                             select m04.乗務員KEY).FirstOrDefault();

                M05_CAR m05 = new M05_CAR();
                m05.車輌ID = m05car.車輌ID;
                m05.登録日時 = DateTime.Now;
                m05.更新日時 = DateTime.Now;
                m05.車輌番号 = m05car.車輌番号;
                m05.自社部門ID = m05car.自社部門ID;
                m05.車種ID = m05car.車種ID;
                m05.乗務員KEY = m04ID;
                m05.運輸局ID = m05car.運輸局ID;
                m05.自傭区分 = m05car.自傭区分;
                m05.廃車区分 = m05car.廃車区分;
                m05.廃車日 = m05car.廃車日;
                m05.次回車検日 = m05car.次回車検日;
                m05.車輌登録番号 = m05car.車輌登録番号;
                m05.登録日 = m05car.登録日;
                m05.初年度登録年 = m05car.初年度登録年;
                m05.初年度登録月 = m05car.初年度登録月;
                m05.自動車種別 = m05car.自動車種別;
                m05.用途 = m05car.用途;
                m05.自家用事業用 = m05car.自家用事業用;
                m05.車体形状 = m05car.車体形状;
                m05.車名 = m05car.車名;
                m05.型式 = m05car.型式;
                m05.乗車定員 = m05car.乗車定員;
                m05.車輌重量 = m05car.車輌重量;
                m05.車輌最大積載量 = m05car.車輌最大積載量;
                m05.車輌総重量 = m05car.車輌総重量;
                m05.車輌実積載量 = m05car.車輌実積載量;
                m05.車台番号 = m05car.車台番号;
                m05.原動機型式 = m05car.原動機型式;
                m05.長さ = m05car.長さ;
                m05.幅 = m05car.幅;
                m05.高さ = m05car.高さ;
                m05.総排気量 = m05car.総排気量;
                m05.燃料種類 = m05car.燃料種類;
                m05.現在メーター = m05car.現在メーター;
                m05.デジタコCD = m05car.デジタコCD;
                m05.所有者名 = m05car.所有者名;
                m05.所有者住所 = m05car.所有者住所;
                m05.使用者名 = m05car.使用者名;
                m05.使用者住所 = m05car.使用者住所;
                m05.使用本拠位置 = m05car.使用本拠位置;
                m05.備考 = m05car.備考;
                m05.型式指定番号 = m05car.型式指定番号;
                m05.前前軸重 = m05car.前前軸重;
                m05.前後軸重 = m05car.前後軸重;
                m05.後前軸重 = m05car.後前軸重;
                m05.後後軸重 = m05car.後後軸重;
                m05.CO2区分 = m05car.CO2区分;
                m05.基準燃費 = m05car.基準燃費;
                m05.黒煙規制値 = m05car.黒煙規制値;
                m05.G車種ID = m05car.G車種ID;
                m05.規制区分ID = m05car.規制区分ID;
                m05.燃料費単価 = m05car.燃料費単価;
                m05.油脂費単価 = m05car.油脂費単価;
                m05.タイヤ費単価 = m05car.タイヤ費単価;
                m05.修繕費単価 = m05car.修繕費単価;
                m05.車検費単価 = m05car.車検費単価;
                try
                {
                    // newのエンティティに対してはAcceptChangesで新規追加となる
                    context.M05_CAR.ApplyChanges(m05);
                    context.SaveChanges();
                }
                catch (UpdateException ex)
                {
                    // PKey違反等
                    Console.WriteLine(ex);
                    throw ex;
                }
            }
        }

        /// <summary>
        /// M05_CARの更新
        /// </summary>
        /// <param name="m05car">M05_CAR_Member</param>
        public int UpdateCar(M05_CAR_Member m05car, bool pMaintenanceFlg, bool pGetNextNumber, List<M05_CDT2_Member> pmM05Cdt2, List<M05_CDT3_Member> pmM05Cdt3, List<M05_CDT4_Member> pmM05Cdt4)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                if (pGetNextNumber)
                {
                    m05car.車輌ID = GetNextID();
                }

                var m04ID = (from m04 in context.M04_DRV
                             where m04.乗務員ID == m05car.乗務員ID
                             select m04.乗務員KEY).FirstOrDefault();


                //更新行を特定
                var ret = from x in context.M05_CAR
                          from m04 in context.M04_DRV.Where(j => j.乗務員KEY == x.乗務員KEY).DefaultIfEmpty()
                          where x.車輌ID == m05car.車輌ID
                          orderby x.車輌ID
                          select x;
                var m05 = ret.FirstOrDefault();

                if (m05 == null)
                {
                    Insert(m05car);
                    UpdateCdt2(pmM05Cdt2 , m05car.車輌KEY);
                    UpdateCdt3(pmM05Cdt3 , m05car.車輌KEY);
                    UpdateCdt4(pmM05Cdt4 , m05car.車輌KEY);
                    return 1;
                }
                if (pMaintenanceFlg)
                {
                    if (pGetNextNumber)
                    {
                        return UpdateCar(m05car
                                          , pMaintenanceFlg
                                          , pGetNextNumber
                                          , pmM05Cdt2
                                          , pmM05Cdt3
                                          , pmM05Cdt4);
                    }

                    return -1;
                }

                m05.更新日時 = DateTime.Now;
                m05.車輌番号 = m05car.車輌番号;
                m05.自社部門ID = m05car.自社部門ID;
                m05.車種ID = m05car.車種ID;
                m05.乗務員KEY = m04ID;
                m05.運輸局ID = m05car.運輸局ID;
                m05.自傭区分 = m05car.自傭区分;
                m05.廃車区分 = m05car.廃車区分;
                m05.廃車日 = m05car.廃車日;
                m05.次回車検日 = m05car.次回車検日;
                m05.車輌登録番号 = m05car.車輌登録番号;
                m05.登録日 = m05car.登録日;
                m05.初年度登録年 = m05car.初年度登録年;
                m05.初年度登録月 = m05car.初年度登録月;
                m05.自動車種別 = m05car.自動車種別;
                m05.用途 = m05car.用途;
                m05.自家用事業用 = m05car.自家用事業用;
                m05.車体形状 = m05car.車体形状;
                m05.車名 = m05car.車名;
                m05.型式 = m05car.型式;
                m05.乗車定員 = m05car.乗車定員;
                m05.車輌重量 = m05car.車輌重量;
                m05.車輌最大積載量 = m05car.車輌最大積載量;
                m05.車輌総重量 = m05car.車輌総重量;
                m05.車輌実積載量 = m05car.車輌実積載量;
                m05.車台番号 = m05car.車台番号;
                m05.原動機型式 = m05car.原動機型式;
                m05.長さ = m05car.長さ;
                m05.幅 = m05car.幅;
                m05.高さ = m05car.高さ;
                m05.総排気量 = m05car.総排気量;
                m05.燃料種類 = m05car.燃料種類;
                m05.現在メーター = m05car.現在メーター;
                m05.デジタコCD = m05car.デジタコCD;
                m05.所有者名 = m05car.所有者名;
                m05.所有者住所 = m05car.所有者住所;
                m05.使用者名 = m05car.使用者名;
                m05.使用者住所 = m05car.使用者住所;
                m05.使用本拠位置 = m05car.使用本拠位置;
                m05.備考 = m05car.備考;
                m05.型式指定番号 = m05car.型式指定番号;
                m05.前前軸重 = m05car.前前軸重;
                m05.前後軸重 = m05car.前後軸重;
                m05.後前軸重 = m05car.後前軸重;
                m05.後後軸重 = m05car.後後軸重;
                m05.CO2区分 = m05car.CO2区分;
                m05.基準燃費 = m05car.基準燃費;
                m05.黒煙規制値 = m05car.黒煙規制値;
                m05.G車種ID = m05car.G車種ID;
                m05.規制区分ID = m05car.規制区分ID;
                m05.燃料費単価 = m05car.燃料費単価;
                m05.油脂費単価 = m05car.油脂費単価;
                m05.タイヤ費単価 = m05car.タイヤ費単価;
                m05.修繕費単価 = m05car.修繕費単価;
                m05.車検費単価 = m05car.車検費単価;
                m05.固定自動車税 = m05car.固定自動車税;
                m05.固定重量税 = m05car.固定重量税;
                m05.固定取得税 = m05car.固定取得税;
                m05.固定自賠責保険 = m05car.固定自賠責保険;
                m05.固定車輌保険 = m05car.固定車輌保険;
                m05.固定対人保険 = m05car.固定対人保険;
                m05.固定対物保険 = m05car.固定対物保険;
                m05.固定貨物保険 = m05car.固定貨物保険;
                m05.AcceptChanges();
                context.SaveChanges();

                UpdateCdt2(pmM05Cdt2 ,m05car.車輌KEY);
                UpdateCdt3(pmM05Cdt3 ,m05car.車輌KEY);
                UpdateCdt4(pmM05Cdt4 ,m05car.車輌KEY);
            }

            return 1;

        }

        /// <summary>
        /// M05_CARの論理削除
        /// </summary>
        /// <param name="m05car">M05_CAR_Member</param>
        public void DeleteCar(M05_CAR_Member m05car)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var Target_m05 = (from m05 in context.M05_CAR.Where(c => c.車輌KEY == m05car.車輌KEY) select m05).FirstOrDefault();

                //該当データが無い場合は何もしない
                if (Target_m05 == null)
                {
                    return;
                }

                Target_m05.車輌ID = (m05car.車輌KEY * -1);
                Target_m05.削除日付 = DateTime.Now;

                context.SaveChanges();
            }
        }

        /// <summary>
        /// M05_CARの自動採番
        /// </summary>
        /// <returns></returns>
        public int GetNextID()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //最大ID行を特定
                var query = from x in context.M05_CAR
                            select x.車輌ID;

                int iMaxID;
                if (query.Count() == 0)
                {
                    iMaxID = 0;
                }
                else
                {
                    iMaxID = query.Max();
                }

                return iMaxID + 1;
            }
        }
        #endregion

        #region M05_CDT1
        /// <summary>
        /// M05_CDT1のデータ取得
        /// </summary>
        /// <param name="p車輌ID">車輌ID</param>
        /// <param name="p配置年月日">配置年月日</param>
        /// <returns>M05_CDT1_Member</returns>
        public M05_CDT1_Member GetDataCdt1(int p車輌ID, DateTime p配置年月日)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m05cdt1 in context.M05_CDT1
                           from m05 in context.M05_CAR.Where(j => j.車輌KEY == m05cdt1.車輌KEY).DefaultIfEmpty()
                           where (m05.車輌ID == p車輌ID && m05cdt1.配置年月日 == p配置年月日)
                           select new M05_CDT1_Member
                           {
                               車輌ID = m05.車輌ID,
                               配置年月日 = m05cdt1.配置年月日,
                               登録日時 = m05cdt1.登録日時,
                               更新日時 = m05cdt1.更新日時,
                               営業所名 = m05cdt1.営業所名,
                               転出先 = m05cdt1.転出先,
                           });
                return ret.FirstOrDefault();
            }
        }

        /// <summary>
        /// M05_CDT1の新規追加
        /// </summary>
        /// <param name="m05cdt1">M05_CDT1_Member</param>
        public void Insert(M05_CDT1_Member m05cdt1)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                M05_CDT1 m05 = new M05_CDT1();
                m05.車輌KEY = m05cdt1.車輌ID;
                m05.配置年月日 = m05cdt1.配置年月日;
                m05.登録日時 = m05cdt1.登録日時;
                m05.更新日時 = m05cdt1.更新日時;
                m05.営業所名 = m05cdt1.営業所名;
                m05.転出先 = m05cdt1.転出先;
                try
                {
                    // newのエンティティに対してはAcceptChangesで新規追加となる
                    context.M05_CDT1.ApplyChanges(m05);
                    context.SaveChanges();
                }
                catch (UpdateException ex)
                {
                    // PKey違反等
                    Console.WriteLine(ex);
                }
            }
        }

        /// <summary>
        /// M05_CDT1の更新
        /// </summary>
        /// <param name="m05cdt1">M05_CDT1_Member</param>
        public void Update(M05_CDT1_Member m05cdt1)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //更新行を特定
                var ret = from x in context.M05_CDT1
                          where (x.車輌KEY == m05cdt1.車輌ID && x.配置年月日 == m05cdt1.配置年月日)
                          orderby x.車輌KEY
                          select x;
                var m05 = ret.FirstOrDefault();

                m05.車輌KEY = m05cdt1.車輌ID;
                m05.配置年月日 = m05cdt1.配置年月日;
                m05.登録日時 = m05cdt1.登録日時;
                m05.更新日時 = DateTime.Now;
                m05.営業所名 = m05cdt1.営業所名;
                m05.転出先 = m05cdt1.転出先;

                m05.AcceptChanges();
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M05_CDT1の物理削除
        /// </summary>
        /// <param name="m05cdt1">M05_CDT1_Member</param>
        public void Delete(M05_CDT1_Member m05cdt1)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M05_CDT1
                          where (x.車輌KEY == m05cdt1.車輌ID && x.配置年月日 == m05cdt1.配置年月日)
                          orderby x.車輌KEY
                          select x;
                var m05 = ret.FirstOrDefault();

                context.DeleteObject(m05);
                context.SaveChanges();
            }
        }
        #endregion

        #region M05_CDT2
        /// <summary>
        /// M05_CDT2のデータ取得
        /// </summary>
        /// <param name="p車輌ID">車輌ID</param>
        /// <param name="p加入年月日">加入年月日</param>
        /// <returns>M05_CDT2_Member</returns>
        public List<M05_CDT2_Member> GetDataCdt2(int p車輌ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m05cdt2 in context.M05_CDT2
                           from m05 in context.M05_CAR.Where(j => j.車輌KEY == m05cdt2.車輌KEY)
                           where (m05.車輌ID == p車輌ID)
                           select new M05_CDT2_Member
                           {
                               車輌ID = m05.車輌ID,
                               車輌KEY = m05.車輌KEY,
                               加入年月日 = m05cdt2.加入年月日,
                               期限 = m05cdt2.期限,
                               契約先 = m05cdt2.契約先,
                               保険証番号 = m05cdt2.保険証番号,
                               月数 = m05cdt2.月数,
                           }).ToList();
                foreach (var rec in ret)
                {
                    rec.Str加入年月日 = rec.加入年月日 == null ? "" : ((DateTime)(rec.加入年月日)).ToString("yyyy/MM/dd");
                    rec.Str期限年月日 = rec.期限 == null ? "" : ((DateTime)(rec.期限)).ToString("yyyy/MM/dd");
                    rec.S_月数 = rec.月数.ToString();
                }

                return ret;
            }
        }

        /// <summary>
        /// M05_CDT2の新規追加
        /// </summary>
        /// <param name="m05cdt2">M05_CDT2_Member</param>
        public void InsertCdt2(M05_CDT2_Member m05cdt2)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                DateTime dtResult;
                DateTime dtResult2;

                context.Connection.Open();


                M05_CDT2 m05 = new M05_CDT2();
                m05.車輌KEY = m05cdt2.車輌KEY;
                //m05.加入年月日 = DateTime.Parse(m05cdt2.Str加入年月日);
                m05.加入年月日 = DateTime.TryParse(m05cdt2.Str加入年月日, out dtResult) ? dtResult : DateTime.Now;
                m05.登録日時 = DateTime.Now;
                m05.更新日時 = DateTime.Now;
                //m05.期限 = DateTime.Parse(m05cdt2.Str期限年月日);
                m05.期限 = DateTime.TryParse(m05cdt2.Str期限年月日, out dtResult2) ? dtResult2 : DateTime.Now; 
                m05.契約先 = m05cdt2.契約先;
                m05.保険証番号 = m05cdt2.保険証番号;
                m05.月数 = int.TryParse(m05cdt2.S_月数 , out num) == true ? num : 0;
                try
                {
                    // newのエンティティに対してはAcceptChangesで新規追加となる
                    context.M05_CDT2.ApplyChanges(m05);
                    context.SaveChanges();
                }
                catch (UpdateException ex)
                {
                    // PKey違反等
                    Console.WriteLine(ex);

                }
            }

        }

        /// <summary>
        /// M05_CDT2の更新
        /// </summary>
        /// <param name="m05cdt2">M05_CDT2_Member</param>
        public void UpdateCdt2(List<M05_CDT2_Member> pm05cdt2 , int 車輌Key)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int i車輌Key = 車輌Key;

                // トランザクションのインスタンス化(開始)
                using (var tran = new TransactionScope())
                {
                    string sql = string.Empty;
                    sql = string.Format("DELETE M05_CDT2 WHERE M05_CDT2.車輌KEY = '" + i車輌Key + "' ");
                    int count = context.ExecuteStoreCommand(sql);
                    tran.Complete();
                }

                foreach (M05_CDT2_Member m05cdt2 in pm05cdt2)
                {
                    DateTime dtResult;
                    DateTime dtResult2;
                    //更新行を特定
                    var ret = from x in context.M05_CDT2
                              where (x.車輌KEY == m05cdt2.車輌KEY && x.加入年月日 == m05cdt2.加入年月日)
                              orderby x.車輌KEY
                              select x;
                    var m05 = ret.FirstOrDefault();
                    M05_CDT2 im05 = new M05_CDT2();
                    im05.車輌KEY = m05cdt2.車輌KEY;
                    //im05.加入年月日 = DateTime.Parse(m05cdt2.Str加入年月日);
                    im05.加入年月日 = DateTime.TryParse(m05cdt2.Str加入年月日, out dtResult) ? dtResult : DateTime.Now;
                    //im05.期限 = DateTime.Parse(m05cdt2.Str期限年月日);
                    im05.期限 = DateTime.TryParse(m05cdt2.Str期限年月日, out dtResult2) ? dtResult2 : DateTime.Now;
                    im05.契約先 = m05cdt2.契約先;
                    im05.保険証番号 = m05cdt2.保険証番号;
                    im05.月数 = int.TryParse(m05cdt2.S_月数, out num) == true ? num : 0;
                    context.M05_CDT2.ApplyChanges(im05);
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M05_CDT2の物理削除
        /// </summary>
        /// <param name="m05cdt2">M05_CDT2_Member</param>
        public void DeleteCdt2(List<M05_CDT2_Member> pm05cdt2)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                foreach (M05_CDT2_Member m05cdt2 in pm05cdt2)
                {
                    //削除行を特定
                    var ret = from x in context.M05_CDT2
                              where (x.車輌KEY == m05cdt2.車輌KEY && x.加入年月日 == m05cdt2.加入年月日)
                              orderby x.車輌KEY
                              select x;
                    var m05 = ret.FirstOrDefault();

                    context.DeleteObject(m05);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// M05_CDT2の論理削除
        /// </summary>
        /// <param name="m05cdt3">M05_CDT2_Member</param>
        public void DeleteCdt2All(int p車輌KEY)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = (from x in context.M05_CDT2
                           where (x.車輌KEY == p車輌KEY)
                           orderby x.車輌KEY
                           select x).ToList();

                foreach (M05_CDT2 m05 in ret)
                {
                    m05.削除日付 = DateTime.Now;
                    m05.AcceptChanges();
                }
                context.SaveChanges();
            }
        }
        #endregion

        #region M05_CDT3
        /// <summary>
        /// M05_CDT3のデータ取得
        /// </summary>
        /// <param name="p車輌ID">車輌ID</param>
        /// <param name="p加入年月日">加入年月日</param>
        /// <returns>M05_CDT3_Member</returns>
        public List<M05_CDT3_Member> GetDataCdt3(int p車輌ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m05cdt3 in context.M05_CDT3
                           from m05 in context.M05_CAR.Where(j => j.車輌KEY == m05cdt3.車輌KEY)
                           where (m05.車輌ID == p車輌ID)
                           select new M05_CDT3_Member
                           {
                               車輌ID = m05.車輌ID,
                               車輌KEY = m05.車輌KEY,
                               加入年月日 = m05cdt3.加入年月日,
                               期限 = m05cdt3.期限,
                               契約先 = m05cdt3.契約先,
                               保険証番号 = m05cdt3.保険証番号,
                               月数 = m05cdt3.月数,
                           }).ToList();
                foreach (var rec in ret)
                {
                    rec.Str加入年月日 = rec.加入年月日 == null ? "" : ((DateTime)(rec.加入年月日)).ToString("yyyy/MM/dd");
                    rec.Str期限年月日 = rec.期限 == null ? "" : ((DateTime)(rec.期限)).ToString("yyyy/MM/dd");
                    rec.S_月数 = rec.月数.ToString();
                }
                return ret;
            }
        }

        /// <summary>
        /// M05_CDT3の新規追加
        /// </summary>
        /// <param name="m05cdt3">M05_CDT3_Member</param>
        public void InsertCdt3(M05_CDT3_Member m05cdt3)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                DateTime dtResult;
                DateTime dtResult2;

                context.Connection.Open();

                M05_CDT3 m05 = new M05_CDT3();
                m05.車輌KEY = m05cdt3.車輌KEY;
                //m05.加入年月日 = DateTime.Parse(m05cdt3.Str加入年月日);
                m05.加入年月日 = DateTime.TryParse(m05cdt3.Str加入年月日, out dtResult) ? dtResult : DateTime.Now;
                m05.登録日時 = DateTime.Now;
                m05.更新日時 = DateTime.Now;
                //m05.期限 = DateTime.Parse(m05cdt3.Str期限年月日);
                m05.期限 = DateTime.TryParse(m05cdt3.Str期限年月日, out dtResult2) ? dtResult2 : DateTime.Now; 
                m05.契約先 = m05cdt3.契約先;
                m05.保険証番号 = m05cdt3.保険証番号;
                m05.月数 = int.TryParse(m05cdt3.S_月数, out num) == true ? num : 0;
                try
                {
                    // newのエンティティに対してはAcceptChangesで新規追加となる
                    context.M05_CDT3.ApplyChanges(m05);
                    context.SaveChanges();
                }
                catch (UpdateException ex)
                {
                    // PKey違反等
                    Console.WriteLine(ex);
                }
            }
        }


        /// <summary>
        /// M05_CDT3の更新
        /// </summary>
        /// <param name="m05cdt3">M05_CDT3_Member</param>
        public void UpdateCdt3(List<M05_CDT3_Member> pm05cdt3 , int 車輌Key)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();



                int i車輌Key = 車輌Key;

                // トランザクションのインスタンス化(開始)
                using (var tran = new TransactionScope())
                {
                    string sql = string.Empty;
                    sql = string.Format("DELETE M05_CDT3 WHERE M05_CDT3.車輌KEY = '" + i車輌Key + "' ");
                    int count = context.ExecuteStoreCommand(sql);
                    tran.Complete();
                }

                DateTime dtResult;
                DateTime dtResult2;

                foreach (M05_CDT3_Member m05cdt3 in pm05cdt3)
                {
                    //更新行を特定
                    var ret = from x in context.M05_CDT3
                              where (x.車輌KEY == m05cdt3.車輌KEY && x.加入年月日 == m05cdt3.加入年月日)
                              orderby x.車輌KEY
                              select x;
                    var m05 = ret.FirstOrDefault();


                    M05_CDT3 im05 = new M05_CDT3();
                    im05.車輌KEY = m05cdt3.車輌KEY;
                    //im05.加入年月日 = DateTime.Parse(m05cdt3.Str加入年月日);
                    im05.加入年月日 = DateTime.TryParse(m05cdt3.Str加入年月日, out dtResult) ? dtResult : DateTime.Now;
                    //im05.期限 = DateTime.Parse(m05cdt3.Str期限年月日);
                    im05.期限 = DateTime.TryParse(m05cdt3.Str期限年月日, out dtResult2) ? dtResult2 : DateTime.Now;
                    im05.契約先 = m05cdt3.契約先;
                    im05.保険証番号 = m05cdt3.保険証番号;
                    im05.月数 = int.TryParse(m05cdt3.S_月数, out num) == true ? num : 0;
                    context.M05_CDT3.ApplyChanges(im05);
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M05_CDT3の物理削除
        /// </summary>
        /// <param name="m05cdt3">M05_CDT3_Member</param>
        public void DeleteCdt3(List<M05_CDT3_Member> pm05cdt3)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                foreach (M05_CDT3_Member m05cdt3 in pm05cdt3)
                {
                    //削除行を特定
                    var ret = from x in context.M05_CDT3
                              where (x.車輌KEY == m05cdt3.車輌ID && x.加入年月日 == m05cdt3.加入年月日)
                              orderby x.車輌KEY
                              select x;
                    var m05 = ret.FirstOrDefault();

                    context.DeleteObject(m05);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// M05_CDT3の論理削除
        /// </summary>
        /// <param name="m05cdt3">M05_CDT4_Member</param>
        public void DeleteCdt3All(int p車輌KEY)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = (from x in context.M05_CDT3
                           where (x.車輌KEY == p車輌KEY)
                           orderby x.車輌KEY
                           select x).ToList();

                foreach (M05_CDT3 m05 in ret)
                {
                    m05.削除日付 = DateTime.Now;

                    m05.AcceptChanges();
                }
                context.SaveChanges();
            }
        }
        #endregion

        #region M05_CDT4
        /// <summary>
        /// M05_CDT4のデータ取得
        /// </summary>
        /// <param name="p車輌ID">車輌ID</param>
        /// <returns>M05_CDT4_Member</returns>
        public List<M05_CDT4_Member> GetDataCdt4(int p車輌ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m05cdt4 in context.M05_CDT4
                           from m05 in context.M05_CAR.Where(j => j.車輌KEY == m05cdt4.車輌KEY)
                           where m05.車輌ID == p車輌ID && m05cdt4.削除日付 == null
                           orderby m05cdt4.年度 descending
                           select new M05_CDT4_Member
                           {
                               車輌ID = m05.車輌ID,
                               車輌KEY = m05.車輌KEY,
                               年度 = m05cdt4.年度,
                               自動車税年月 = m05cdt4.自動車税年月,
                               自動車税 = m05cdt4.自動車税,
                               d自動車税 = m05cdt4.自動車税,
                               重量税年月 = m05cdt4.重量税年月,
                               重量税 = m05cdt4.重量税,
                               d重量税 = m05cdt4.重量税,

                           }).ToList();

                foreach (var rec in ret)
                {
                    rec.S_年度 = rec.年度.ToString();
                    rec.S_自動車税年月 = rec.自動車税年月 == null ? "0" : rec.自動車税年月.ToString();
                    rec.S_自動車税 = rec.自動車税 == null ? "0" : rec.自動車税.ToString();
                    rec.S_重量税年月 = rec.重量税年月 == null ? "0" : rec.重量税年月.ToString();
                    rec.S_重量税 = rec.重量税 == null ? "0" : rec.重量税.ToString();
                }
                return ret;
            }
        }

        /// <summary>
        /// M05_CDT4の新規追加
        /// </summary>
        /// <param name="m05cdt3">M05_CDT4_Member</param>
        public void InsertCdt4(M05_CDT4_Member m05cdt4)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                M05_CDT4 m05 = new M05_CDT4();
                m05.車輌KEY = m05cdt4.車輌KEY;
                m05.年度 = int.TryParse(m05cdt4.S_年度 , out num) == true ? num : 0;
                m05.自動車税年月 = int.TryParse(m05cdt4.S_自動車税年月, out num) == true ? num : 0;
                m05.自動車税 = int.TryParse(m05cdt4.S_自動車税, out num) == true ? num : 0;
                m05.重量税年月 = int.TryParse(m05cdt4.S_重量税年月, out num) == true ? num : 0;
                m05.重量税 = int.TryParse(m05cdt4.S_重量税, out num) == true ? num : 0;


                try
                {
                    // newのエンティティに対してはAcceptChangesで新規追加となる
                    context.M05_CDT4.ApplyChanges(m05);
                    context.SaveChanges();
                }
                catch (UpdateException ex)
                {
                    // PKey違反等
                    Console.WriteLine(ex);
                }
            }
        }


        /// <summary>
        /// M05_CDT4の更新
        /// </summary>
        /// <param name="m05cdt3">M05_CDT4_Member</param>
        public void UpdateCdt4(List<M05_CDT4_Member> pm05cdt4, int 車輌Key)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int i車輌Key = 車輌Key;

                // トランザクションのインスタンス化(開始)
                using (var tran = new TransactionScope())
                {
                    string sql = string.Empty;
                    sql = string.Format("DELETE M05_CDT4 WHERE M05_CDT4.車輌KEY = '" + i車輌Key + "' ");
                    int count = context.ExecuteStoreCommand(sql);
                    tran.Complete();
                }


                foreach (M05_CDT4_Member m05cdt4 in pm05cdt4)
                {
                    //更新行を特定
                    var ret = from x in context.M05_CDT4
                              where (x.車輌KEY == m05cdt4.車輌KEY && x.年度 == m05cdt4.年度)
                              select x;
                    var m05 = ret.FirstOrDefault();


                    M05_CDT4 im05 = new M05_CDT4();
                    im05.車輌KEY = m05cdt4.車輌KEY;
                    im05.年度 = int.TryParse(m05cdt4.S_年度, out num) == true ? num : 0;
                    im05.自動車税年月 = int.TryParse(m05cdt4.S_自動車税年月, out num) == true ? num : 0;
                    im05.自動車税 = m05cdt4.自動車税;
                    im05.重量税年月 = int.TryParse(m05cdt4.S_重量税年月, out num) == true ? num : 0;
                    im05.重量税 = m05cdt4.重量税;
                    context.M05_CDT4.ApplyChanges(im05);

                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M05_CDT4の物理削除
        /// </summary>
        /// <param name="m05cdt3">M05_CDT4_Member</param>
        public void DeleteCdt4(List<M05_CDT4_Member> pm05cdt4)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                foreach (M05_CDT4_Member m05cdt4 in pm05cdt4)
                {
                    //削除行を特定
                    var ret = from x in context.M05_CDT4
                              where (x.車輌KEY == m05cdt4.車輌KEY && x.年度 == m05cdt4.年度)
                              orderby x.車輌KEY
                              select x;
                    var m05 = ret.FirstOrDefault();

                    m05.削除日付 = DateTime.Now;

                    m05.AcceptChanges();
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// M05_CDT4の論理削除
        /// </summary>
        /// <param name="m05cdt3">M05_CDT4_Member</param>
        public void DeleteCdt4All(int p車輌KEY)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = (from x in context.M05_CDT4
                           where (x.車輌KEY == p車輌KEY)
                           orderby x.車輌KEY
                           select x).ToList();

                foreach (M05_CDT4 m05 in ret)
                {
                    m05.削除日付 = DateTime.Now;

                    m05.AcceptChanges();
                }
                context.SaveChanges();
            }
        }
        #endregion

        /// <summary>
        /// M05_TENのデータ取得
        /// </summary>
        /// <param name="p車輌ID">車輌ID</param>
        /// <param name="p年月">年月</param>
        /// <returns>M05_TEN_Member</returns>
        public M05_TEN_Member GetDataTen(int p車輌ID, int p年月)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m05ten in context.M05_TEN
                           from m05 in context.M05_CAR.Where(j => j.車輌KEY == m05ten.車輌KEY).DefaultIfEmpty()
                           where (m05ten.車輌KEY == p車輌ID && m05ten.年月 == p年月)
                           select new M05_TEN_Member
                           {
                               車輌ID = m05.車輌ID,
                               年月 = m05ten.年月,
                               登録日時 = m05ten.登録日時,
                               更新日時 = m05ten.更新日時,
                               点検日 = m05ten.点検日,
                               チェック = m05ten.チェック,
                               その他備考 = m05ten.その他備考,
                               乗務員ID = m05ten.乗務員KEY,
                               乗務員名 = m05ten.乗務員名,
                               整備指示 = m05ten.整備指示,
                               指示日付 = m05ten.指示日付,
                               整備部品 = m05ten.整備部品,
                               部品日付 = m05ten.部品日付,
                               整備結果 = m05ten.整備結果,
                               結果日付 = m05ten.結果日付,
                           });
                return ret.FirstOrDefault();
            }
        }


        /// <summary>
        /// M05_TENの新規追加
        /// </summary>
        /// <param name="m05ten">M05_TEN_Member</param>
        public void Insert(M05_TEN_Member m05ten)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                M05_TEN m05 = new M05_TEN();
                m05.車輌KEY = m05ten.車輌ID;
                m05.年月 = m05ten.年月;
                m05.登録日時 = m05ten.登録日時;
                m05.更新日時 = m05ten.更新日時;
                m05.点検日 = m05ten.点検日;
                m05.チェック = m05ten.チェック;
                m05.エアコン区分 = m05ten.エアコン区分;
                m05.エアコン備考 = m05ten.エアコン備考;
                m05.異音区分 = m05ten.異音区分;
                m05.異音備考 = m05ten.異音備考;
                m05.排気区分 = m05ten.排気区分;
                m05.排気備考 = m05ten.排気備考;
                m05.燃費区分 = m05ten.燃費区分;
                m05.燃費備考 = m05ten.燃費備考;
                m05.その他区分 = m05ten.その他区分;
                m05.その他備考 = m05ten.その他備考;
                m05.乗務員KEY = m05ten.乗務員ID;
                m05.乗務員名 = m05ten.乗務員名;
                m05.整備指示 = m05ten.整備指示;
                m05.指示日付 = m05ten.指示日付;
                m05.整備部品 = m05ten.整備部品;
                m05.部品日付 = m05ten.部品日付;
                m05.整備結果 = m05ten.整備結果;
                m05.結果日付 = m05ten.結果日付;
                try
                {
                    // newのエンティティに対してはAcceptChangesで新規追加となる
                    context.M05_TEN.ApplyChanges(m05);
                    context.SaveChanges();
                }
                catch (UpdateException ex)
                {
                    // PKey違反等
                    Console.WriteLine(ex);
                }
            }
        }

        /// <summary>
        /// M05_TENの更新
        /// </summary>
        /// <param name="m05ten">M05_TEN_Member</param>
        public void Update(M05_TEN_Member m05ten)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //更新行を特定
                var ret = from x in context.M05_TEN
                          where (x.車輌KEY == m05ten.車輌ID && x.年月 == m05ten.年月)
                          orderby x.車輌KEY
                          select x;
                var m05 = ret.FirstOrDefault();

                m05.車輌KEY = m05ten.車輌ID;
                m05.年月 = m05ten.年月;
                m05.登録日時 = m05ten.登録日時;
                m05.更新日時 = DateTime.Now;
                m05.点検日 = m05ten.点検日;
                m05.チェック = m05ten.チェック;
                m05.エアコン区分 = m05ten.エアコン区分;
                m05.エアコン備考 = m05ten.エアコン備考;
                m05.異音区分 = m05ten.異音区分;
                m05.異音備考 = m05ten.異音備考;
                m05.排気区分 = m05ten.排気区分;
                m05.排気備考 = m05ten.排気備考;
                m05.燃費区分 = m05ten.燃費区分;
                m05.燃費備考 = m05ten.燃費備考;
                m05.その他区分 = m05ten.その他区分;
                m05.その他備考 = m05ten.その他備考;
                m05.乗務員KEY = m05ten.乗務員ID;
                m05.乗務員名 = m05ten.乗務員名;
                m05.整備指示 = m05ten.整備指示;
                m05.指示日付 = m05ten.指示日付;
                m05.整備部品 = m05ten.整備部品;
                m05.部品日付 = m05ten.部品日付;
                m05.整備結果 = m05ten.整備結果;
                m05.結果日付 = m05ten.結果日付;

                m05.AcceptChanges();
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M05_TENの物理削除
        /// </summary>
        /// <param name="m05ten">M05_TEN_Member</param>
        public void Delete(M05_TEN_Member m05ten)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M05_TEN
                          where (x.車輌KEY == m05ten.車輌ID && x.年月 == m05ten.年月)
                          orderby x.車輌KEY
                          select x;
                var m05 = ret.FirstOrDefault();

                context.DeleteObject(m05);
                context.SaveChanges();
            }
        }


        /// <summary>
        /// M05_CARのプレビュー出力データ取得
        /// </summary>
        /// <param name="p車輌ID">車輌ID</param>
        /// <returns>M05_CAR_Member</returns>
        public List<M05_CAR_ichiran_Pre_Member> GetSearchListData(string p車輌IDFrom, string p車輌IDTo, string p自社部門From, string p自社部門To, string p廃車, int[] i車輌List)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<M05_CAR_ichiran_Pre_Member> retList = new List<M05_CAR_ichiran_Pre_Member>();
                context.Connection.Open();


                //全件表示
                var query = (from m05 in context.M05_CAR
                             from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == m05.乗務員KEY).DefaultIfEmpty()
                             from m71 in context.M71_BUM.Where(m71 => m71.自社部門ID == m05.自社部門ID).DefaultIfEmpty()
                             from m84 in context.M84_RIK.Where(m84 => m84.運輸局ID == m05.運輸局ID).DefaultIfEmpty()
                             from m06 in context.M06_SYA.Where(m06 => m06.車種ID == m05.車種ID).DefaultIfEmpty()
                             where m05.削除日付 == null

                             select new M05_CAR_ichiran_Pre_Member
                             {

                                 車輌ID = m05.車輌ID,
                                 車輌番号 = m05.車輌番号,
                                 自社部門ID = m05.自社部門ID,
                                 車種ID = m05.車種ID,
                                 乗務員KEY = m04.乗務員ID == null ? 0 : m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 運輸局ID = m05.運輸局ID,
                                 //自傭区分 = m05.自傭区分,
                                 廃車区分 = m05.廃車区分,
                                 廃車区分表示 = m05.廃車区分 == 0 ? "稼働中" : "廃車済",
                                 廃車日 = m05.廃車日,
                                 次回車検日 = m05.次回車検日,
                                 車輌登録番号 = m05.車輌登録番号,
                                 登録日 = m05.登録日,
                                 //初年度登録年 = m05.初年度登録年,
                                 //初年度登録月 = m05.初年度登録月,
                                 自動車種別 = m05.自動車種別,
                                 //用途 = m05.用途,
                                 //自家用事業用 = m05.自家用事業用,
                                 車体形状 = m05.車体形状,
                                 車名 = m05.車名,
                                 型式 = m05.型式,
                                 //乗車定員 = m05.乗車定員,
                                 車輌重量 = m05.車輌重量,
                                 車輌最大積載量 = m05.車輌最大積載量,
                                 車輌総重量 = m05.車輌総重量,
                                 車輌実積載量 = m05.車輌実積載量,
                                 //車台番号 = m05.車台番号,
                                 //原動機型式 = m05.原動機型式,
                                 //長さ = m05.長さ,
                                 //幅 = m05.幅,
                                 //高さ = m05.高さ,
                                 総排気量 = m05.総排気量,
                                 //燃料種類 = m05.燃料種類,
                                 現在メーター = m05.現在メーター,
                                 //デジタコCD = m05.デジタコCD,
                                 //所有者名 = m05.所有者名,
                                 //所有者住所 = m05.所有者住所,
                                 //使用者名 = m05.使用者名,
                                 //使用者住所 = m05.使用者住所,
                                 //使用本拠位置 = m05.使用本拠位置,
                                 //備考 = m05.備考,
                                 //型式指定番号 = m05.型式指定番号,
                                 //前前軸重 = m05.前前軸重,
                                 //前後軸重 = m05.前後軸重,
                                 //後前軸重 = m05.後前軸重,
                                 //後後軸重 = m05.後後軸重,
                                 //CO2区分 = m05.CO2区分,
                                 //基準燃費 = m05.基準燃費,
                                 //黒煙規制値 = m05.黒煙規制値,
                                 //G車種ID = m05.G車種ID,
                                 //規制区分ID = m05.規制区分ID,
                                 //燃料費単価 = m05.燃料費単価,
                                 //油脂費単価 = m05.油脂費単価,
                                 //タイヤ費単価 = m05.タイヤ費単価,
                                 //修繕費単価 = m05.修繕費単価,
                                 //車検費単価 = m05.車検費単価,
                                 //削除日付 = m05.削除日付,
                                 運輸局名 = m84.運輸局名,
                                 自社部門名 = m71.自社部門名,
                                 車種名 = m06.車種名,

                             }).AsQueryable();


                if (!(string.IsNullOrEmpty(p車輌IDFrom + p車輌IDTo + p自社部門From + p自社部門To) && i車輌List.Length == 0))
                {
                    if (string.IsNullOrEmpty(p車輌IDFrom + p車輌IDTo + p自社部門From + p自社部門To))
                    {

                        query = query.Where(c => c.車種ID >= int.MaxValue);
                    }

                    //車輌ID検索条件
                    if (!string.IsNullOrEmpty(p車輌IDFrom))
                    {
                        int ip車輌IDFrom = AppCommon.IntParse(p車輌IDFrom);
                        query = query.Where(c => c.車輌ID >= ip車輌IDFrom);
                    }

                    if (!string.IsNullOrEmpty(p車輌IDTo))
                    {
                        int ip車輌IDTo = AppCommon.IntParse(p車輌IDTo);
                        query = query.Where(c => c.車輌ID <= ip車輌IDTo);
                    }

                    //自社部門ID検索条件
                    if (!string.IsNullOrEmpty(p自社部門From))
                    {
                        int ip自社部門From = AppCommon.IntParse(p自社部門From);
                        query = query.Where(c => c.自社部門ID >= ip自社部門From);
                    }

                    if (!string.IsNullOrEmpty(p自社部門To))
                    {
                        int ip自社部門To = AppCommon.IntParse(p自社部門To);
                        query = query.Where(c => c.自社部門ID <= ip自社部門To);
                    }

                    if (i車輌List.Length > 0)
                    {
                        var intCause = i車輌List;
                        query = query.Union(from m05 in context.M05_CAR
                                            from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == m05.乗務員KEY).DefaultIfEmpty()
                                            from m71 in context.M71_BUM.Where(m71 => m71.自社部門ID == m05.自社部門ID).DefaultIfEmpty()
                                            from m84 in context.M84_RIK.Where(m84 => m84.運輸局ID == m05.運輸局ID).DefaultIfEmpty()
                                            from m06 in context.M06_SYA.Where(m06 => m06.車種ID == m05.車種ID).DefaultIfEmpty()
                                            where m05.削除日付 == null && intCause.Contains(m05.車輌ID)
                                            select new M05_CAR_ichiran_Pre_Member
                                            {
                                                車輌ID = m05.車輌ID,
                                                車輌番号 = m05.車輌番号,
                                                自社部門ID = m05.自社部門ID,
                                                車種ID = m05.車種ID,
                                                乗務員KEY = m04.乗務員ID == null ? 0 : m04.乗務員ID,
                                                乗務員名 = m04.乗務員名,
                                                運輸局ID = m05.運輸局ID,
                                                //自傭区分 = m05.自傭区分,
                                                廃車区分 = m05.廃車区分,
                                                廃車区分表示 = m05.廃車区分 == 0 ? "稼働中" : "廃車済",
                                                廃車日 = m05.廃車日,
                                                次回車検日 = m05.次回車検日,
                                                車輌登録番号 = m05.車輌登録番号,
                                                登録日 = m05.登録日,
                                                //初年度登録年 = m05.初年度登録年,
                                                //初年度登録月 = m05.初年度登録月,
                                                自動車種別 = m05.自動車種別,
                                                //用途 = m05.用途,
                                                //自家用事業用 = m05.自家用事業用,
                                                車体形状 = m05.車体形状,
                                                車名 = m05.車名,
                                                型式 = m05.型式,
                                                //乗車定員 = m05.乗車定員,
                                                車輌重量 = m05.車輌重量,
                                                車輌最大積載量 = m05.車輌最大積載量,
                                                車輌総重量 = m05.車輌総重量,
                                                車輌実積載量 = m05.車輌実積載量,
                                                //車台番号 = m05.車台番号,
                                                //原動機型式 = m05.原動機型式,
                                                //長さ = m05.長さ,
                                                //幅 = m05.幅,
                                                //高さ = m05.高さ,
                                                総排気量 = m05.総排気量,
                                                //燃料種類 = m05.燃料種類,
                                                現在メーター = m05.現在メーター,
                                                //デジタコCD = m05.デジタコCD,
                                                //所有者名 = m05.所有者名,
                                                //所有者住所 = m05.所有者住所,
                                                //使用者名 = m05.使用者名,
                                                //使用者住所 = m05.使用者住所,
                                                //使用本拠位置 = m05.使用本拠位置,
                                                //備考 = m05.備考,
                                                //型式指定番号 = m05.型式指定番号,
                                                //前前軸重 = m05.前前軸重,
                                                //前後軸重 = m05.前後軸重,
                                                //後前軸重 = m05.後前軸重,
                                                //後後軸重 = m05.後後軸重,
                                                //CO2区分 = m05.CO2区分,
                                                //基準燃費 = m05.基準燃費,
                                                //黒煙規制値 = m05.黒煙規制値,
                                                //G車種ID = m05.G車種ID,
                                                //規制区分ID = m05.規制区分ID,
                                                //燃料費単価 = m05.燃料費単価,
                                                //油脂費単価 = m05.油脂費単価,
                                                //タイヤ費単価 = m05.タイヤ費単価,
                                                //修繕費単価 = m05.修繕費単価,
                                                //車検費単価 = m05.車検費単価,
                                                //削除日付 = m05.削除日付,
                                                運輸局名 = m84.運輸局名,
                                                自社部門名 = m71.自社部門名,
                                                車種名 = m06.車種名,
                                            });
                    }
                }
                query = query.Distinct();

                //廃車区分処理
                switch (p廃車)
                {
                    case "0":
                        //全件表示 何もしない
                        break;

                    case "1":
                        //稼働中のみ表示
                        query = query.Where(c => c.廃車区分 == 0);
                        break;

                    case "2":
                        //稼働済みのみ表示
                        query = query.Where(c => c.廃車区分 == 1);
                        break;

                    default:
                        break;
                }

                //データをリスト化
                retList = query.ToList();

                return retList;
            }
        }

        /// <summary>
        /// 一括車輌固定データ取得
        /// </summary>
        /// <param name="p車輌ID"></param>
        /// <returns></returns>
        public List<M05_CAR_Member> GetDataKoteihi()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m05 in context.M05_CAR
                           where m05.削除日付 == null
                           select new M05_CAR_Member
                           {
                               車輌ID = m05.車輌ID,
                               車輌番号 = m05.車輌番号,
                               固定自動車税 = m05.固定自動車税,
                               固定重量税 = m05.固定重量税,
                               固定取得税 = m05.固定取得税,
                               固定自賠責保険 = m05.固定自賠責保険,
                               固定車輌保険 = m05.固定車輌保険,
                               固定対人保険 = m05.固定対人保険,
                               固定対物保険 = m05.固定対物保険,
                               固定貨物保険 = m05.固定貨物保険,

                               d固定自動車税 = m05.固定自動車税,
                               d固定重量税 = m05.固定重量税,
                               d固定取得税 = m05.固定取得税,
                               d固定自賠責保険 = m05.固定自賠責保険,
                               d固定車輌保険 = m05.固定車輌保険,
                               d固定対人保険 = m05.固定対人保険,
                               d固定対物保険 = m05.固定対物保険,
                               d固定貨物保険 = m05.固定貨物保険,
                           });
                return ret.ToList();
            }
        }

        /// <summary>
        /// 一括車輌固定データ更新
        /// </summary>
        /// <param name="pmM05"></param>
        public void UpdateKoteihi(List<M05_CAR_Member> pmM05)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                foreach (M05_CAR_Member mM05 in pmM05)
                {
                    var ret = from x in context.M05_CAR
                              where x.削除日付 == null && x.車輌ID == mM05.車輌ID
                              && (x.固定自動車税 != mM05.d固定自動車税 || 
                                  x.固定重量税 != mM05.d固定重量税 || 
                                  x.固定取得税 != mM05.d固定取得税 || 
                                  x.固定自賠責保険 != mM05.d固定自賠責保険 || 
                                  x.固定車輌保険 != mM05.d固定車輌保険 || 
                                  x.固定対人保険 != mM05.d固定対人保険 || 
                                  x.固定対物保険 != mM05.d固定対物保険 || 
                                  x.固定貨物保険 != mM05.d固定貨物保険 ) 
                              select x;
                    var m05 = ret.FirstOrDefault();

                    if (m05 == null)
                    {
                        continue;
                    }

                    m05.固定自動車税 = mM05.d固定自動車税 == null ? null : (int?)mM05.d固定自動車税;
                    m05.固定重量税 = mM05.d固定重量税 == null ? null : (int?)mM05.d固定重量税;
                    m05.固定取得税 = mM05.d固定取得税 == null ? null : (int?)mM05.d固定取得税;
                    m05.固定自賠責保険 = mM05.d固定自賠責保険 == null ? null : (int?)mM05.d固定自賠責保険;
                    m05.固定車輌保険 = mM05.d固定車輌保険 == null ? null : (int?)mM05.d固定車輌保険;
                    m05.固定対人保険 = mM05.d固定対人保険 == null ? null : (int?)mM05.d固定対人保険;
                    m05.固定対物保険 = mM05.d固定対物保険 == null ? null : (int?)mM05.d固定対物保険;
                    m05.固定貨物保険 = mM05.d固定貨物保険 == null ? null : (int?)mM05.d固定貨物保険;

                    m05.AcceptChanges();
                }
                context.SaveChanges();
            }

        }


		/// <summary>
		/// M05_DRVのID変換
		/// </summary>
		/// <param name="m01tok">M01_TOK_Member</param>
		public int CAR_ID_CHG(int oldID, int newID)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

				//更新行を特定
				var ret = from x in context.M05_CAR
						  where x.車輌ID == oldID && x.削除日付 == null
						  orderby x.車輌ID
						  select x;
				//更新行を特定
				var ret2 = from x in context.M05_CAR
						   where x.車輌ID == newID
						   orderby x.車輌ID
						   select x;
				if (ret2.Count() != 0)
				{
					return -1;
				}
				if (ret.Count() == 0)
				{
					return 0;
				}

				var m05 = ret.FirstOrDefault();
				m05.車輌ID = newID;
				m05.AcceptChanges();
				context.SaveChanges();
				return newID;
			}
		}

    }
}
