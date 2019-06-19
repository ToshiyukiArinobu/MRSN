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

    public class MST90020_TOK
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

    public class MST90020_DRV
    {
        [DataMember]
        public int 乗務員KEY { get; set; }
        [DataMember]
        public int 乗務員ID { get; set; }
        [DataMember]
        public string 乗務員名 { get; set; }
    }

    public class MST90020_CAR
    {
        [DataMember]
        public int 車輌KEY { get; set; }
        [DataMember]
        public int 車輌ID { get; set; }
        [DataMember]
        public string 車輌番号 { get; set; }
    }

    // 20150723 wada add 発着地ID取得用
    public class MST90020_TIK
    {
        [DataMember]
        public int 発着地ID { get; set; }
        [DataMember]
        public string 発着地名 { get; set; }
    }

    // 20150723 wada add 商品ID取得用
    public class MST90020_HIN
    {
        [DataMember]
        public int 商品ID { get; set; }
        [DataMember]
        public string 商品名 { get; set; }
    }

    // 20150723 wada add 車種ID取得用
    public class MST90020_SYA
    {
        [DataMember]
        public int 車種ID { get; set; }
        [DataMember]
        public string 車種名 { get; set; }
    }

    // 20150723 wada add 摘要ID取得用
    public class MST90020_TEK
    {
        [DataMember]
        public int 摘要ID { get; set; }
        [DataMember]
        public string 摘要名 { get; set; }
    }

    // 20150723 wada add 摘要ID取得用
    public class MST90020_KEY
    {
        [DataMember]
        public int Key { get; set; }
    }


    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コード、svc、および config ファイルで同時にクラス名 "DataDriveLogService" を変更できます。
    // 注意: このサービスをテストするために WCF テスト クライアントを起動するには、ソリューション エクスプローラーで DataDriveLogService.svc または DataDriveLogService.svc.cs を選択し、デバッグを開始してください。
    public class M90020 : IM90020
    {
        #region 参照

        /// <summary>
        /// M90050_TRNのデータ取得
        /// </summary>
        public List<COLS> SEARCH_MST90060_00(string pテーブル名)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                try
                {
                    //var work = context.ExecuteStoreQuery<COLS>("select c.name as \"name\", type_name(c.system_type_id) as \"systype\"  FROM Sys.Columns c WHERE object_id = object_id('" + pテーブル名 + "')");
                    var work = context.ExecuteStoreQuery<COLS>("select c.name as \"name\", type_name(c.system_type_id) as \"systype\", ep.value as \"avalue\" FROM Sys.Columns c left outer join sys.extended_properties ep on c.object_id = ep.major_id and c.column_id = ep.minor_id WHERE object_id = object_id('" + pテーブル名 + "')");
                    List<COLS> query = work.ToList();
                    return query;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }


        /// <summary>
        /// M90050_TOKのデータ取得
        /// </summary>
        public List<MST90020_TOK> MST90020_TOK()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from tok in context.M01_TOK
                             where tok.削除日付 == null
                             select new MST90020_TOK
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
        public List<MST90020_DRV> MST90020_DRV()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from drv in context.M04_DRV
                             where drv.削除日付 == null
                             select new MST90020_DRV
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
        public List<MST90020_CAR> MST90020_CAR()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from car in context.M05_CAR
                             where car.削除日付 == null
                             select new MST90020_CAR
                             {
                                 車輌KEY = car.車輌KEY,
                                 車輌ID = car.車輌ID,
                                 車輌番号 = car.車輌番号,

                             }).ToList();
                return query;
            }
        }


        public List<MST90020_TIK> MST90020_TIK()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from q in context.M08_TIK
                             where q.削除日付 == null
                             select new MST90020_TIK
                             {
                                 発着地ID = q.発着地ID,
                                 発着地名 = q.発着地名
                             }).ToList();
                return query;
            }
        }

        public List<MST90020_HIN> MST90020_HIN()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from q in context.M09_HIN
                             where q.削除日付 == null
                             select new MST90020_HIN
                             {
                                 商品ID = q.商品ID,
                                 商品名 = q.商品名
                             }).ToList();
                return query;
            }
        }

        public List<MST90020_SYA> MST90020_SYA()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from q in context.M06_SYA
                             where q.削除日付 == null
                             select new MST90020_SYA
                             {
                                 車種ID = q.車種ID,
                                 車種名 = q.車種名
                             }).ToList();
                return query;
            }
        }

        public List<MST90020_TEK> MST90020_TEK()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from q in context.M11_TEK
                             where q.削除日付 == null
                             select new MST90020_TEK
                             {
                                 摘要ID = q.摘要ID,
                                 摘要名 = q.摘要名
                             }).ToList();
                return query;
            }
        }






        /// <summary>
        /// M90020_ZEIのデータ取得
        /// </summary>
        public List<M90020_TOK> SEARCH_MST90020_00(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m01 in context.M01_TOK
                             select new M90020_TOK
                             {
                                 得意先ID = m01.得意先ID,
                             }).OrderBy(c => c.得意先ID).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_ZEIのデータ取得
        /// </summary>
        public List<M90020_TTAN1> SEARCH_MST90020_01()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m02_t1 in context.M02_TTAN1
                             from m01 in context.M01_TOK.Where(c => c.得意先KEY == m02_t1.得意先KEY).DefaultIfEmpty()
                             select new M90020_TTAN1
                             {
                                 得意先ID = m01.得意先ID == null ? 0 : m01.得意先ID,
                                 得意先KEY = m02_t1.得意先KEY,
                                 発地ID = m02_t1.発地ID,
                                 着地ID = m02_t1.着地ID,
                                 商品ID = m02_t1.商品ID,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_ZEIのデータ取得
        /// </summary>
        public List<M90020_TTAN2> SEARCH_MST90020_02()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m02_t2 in context.M02_TTAN2
                             from m01 in context.M01_TOK.Where(c => c.得意先KEY == m02_t2.得意先KEY).DefaultIfEmpty()
                             select new M90020_TTAN2
                             {
                                 得意先ID = m01.得意先ID == null ? 0 : m01.得意先ID,
                                 得意先KEY = m02_t2.得意先KEY,
                                 車種ID = m02_t2.車種ID,
                                 発地ID = m02_t2.発地ID,
                                 着地ID = m02_t2.着地ID,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_ZEIのデータ取得
        /// </summary>
        public List<M90020_TTAN3> SEARCH_MST90020_03()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m02_t3 in context.M02_TTAN3
                             from m01 in context.M01_TOK.Where(c => c.得意先KEY == m02_t3.得意先KEY).DefaultIfEmpty()
                             select new M90020_TTAN3
                             {
                                 得意先ID = m01.得意先ID == null ? 0 : m01.得意先ID,
                                 得意先KEY = m02_t3.得意先KEY,
                                 距離 = m02_t3.距離,
                                 重量 = m02_t3.重量,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_ZEIのデータ取得
        /// </summary>
        public List<M90020_TTAN4> SEARCH_MST90020_04()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m02_t4 in context.M02_TTAN4
                             from m01 in context.M01_TOK.Where(c => c.得意先KEY == m02_t4.得意先KEY).DefaultIfEmpty()
                             select new M90020_TTAN4
                             {
                                 得意先ID = m01.得意先ID == null ? 0 : m01.得意先ID,
                                 得意先KEY = m02_t4.得意先KEY,
                                 重量 = m02_t4.重量,
                                 個数 = m02_t4.個数,
                                 着地ID = m02_t4.着地ID,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_ZEIのデータ取得
        /// </summary>
        public List<M90020_YTAN1> SEARCH_MST90020_05()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m03_y1 in context.M03_YTAN1
                             from m01 in context.M01_TOK.Where(c => c.得意先KEY == m03_y1.支払先KEY).DefaultIfEmpty()
                             select new M90020_YTAN1
                             {
                                 支払先ID = m01.得意先ID == null ? 0 : m01.得意先ID,
                                 支払先KEY = m03_y1.支払先KEY,
                                 発地ID = m03_y1.発地ID,
                                 着地ID = m03_y1.着地ID,
                                 商品ID = m03_y1.商品ID
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_ZEIのデータ取得
        /// </summary>
        public List<M90020_YTAN2> SEARCH_MST90020_06()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m03_y2 in context.M03_YTAN2
                             from m01 in context.M01_TOK.Where(c => c.得意先KEY == m03_y2.支払先KEY).DefaultIfEmpty()
                             select new M90020_YTAN2
                             {
                                 支払先ID = m01.得意先ID == null ? 0 : m01.得意先ID,
                                 支払先KEY = m03_y2.支払先KEY,
                                 車種ID = m03_y2.車種ID,
                                 発地ID = m03_y2.発地ID,
                                 着地ID = m03_y2.着地ID
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_ZEIのデータ取得
        /// </summary>
        public List<M90020_YTAN3> SEARCH_MST90020_07()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m03_y3 in context.M03_YTAN3
                             from m01 in context.M01_TOK.Where(c => c.得意先KEY == m03_y3.支払先KEY).DefaultIfEmpty()
                             select new M90020_YTAN3
                             {
                                 支払先ID = m01.得意先ID == null ? 0 : m01.得意先ID,
                                 支払先KEY = m03_y3.支払先KEY,
                                 距離 = m03_y3.距離,
                                 重量 = m03_y3.重量,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_ZEIのデータ取得
        /// </summary>
        public List<M90020_YTAN4> SEARCH_MST90020_08()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m03_y4 in context.M03_YTAN4
                             from m01 in context.M01_TOK.Where(c => c.得意先KEY == m03_y4.支払先KEY).DefaultIfEmpty()
                             select new M90020_YTAN4
                             {
                                 支払先ID = m01.得意先ID == null ? 0 : m01.得意先ID,
                                 支払先KEY = m03_y4.支払先KEY,
                                 重量 = m03_y4.重量,
                                 個数 = m03_y4.個数,
                                 着地ID = m03_y4.着地ID
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M04_DRVのデータ取得
        /// </summary>
        public List<M90020_DRV> SEARCH_MST90020_09(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m04 in context.M04_DRV
                             select new M90020_DRV
                             {
                                 乗務員ID = m04.乗務員ID,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M04_DDT1のデータ取得
        /// </summary>
        public List<M90020_DDT1> SEARCH_MST90020_10()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m04d in context.M04_DDT1
                             from m04 in context.M04_DRV.Where(c => c.乗務員KEY == m04d.乗務員KEY).DefaultIfEmpty()
                             select new M90020_DDT1
                             {
                                 乗務員ID = m04.乗務員ID == null ? 0 : m04.乗務員ID,
                                 乗務員KEY = m04d.乗務員KEY,
                                 明細行 = m04d.明細行,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M04_DDT2のデータ取得
        /// </summary>
        public List<M90020_DDT2> SEARCH_MST90020_11()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m04d in context.M04_DDT2
                             from m04 in context.M04_DRV.Where(c => c.乗務員KEY == m04d.乗務員KEY).DefaultIfEmpty()
                             select new M90020_DDT2
                             {
                                 乗務員ID = m04.乗務員ID == null ? 0 : m04.乗務員ID,
                                 乗務員KEY = m04d.乗務員KEY,
                                 明細行 = m04d.明細行,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M04_DDT3のデータ取得
        /// </summary>
        public List<M90020_DDT3> SEARCH_MST90020_12()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m04d in context.M04_DDT3
                             from m04 in context.M04_DRV.Where(c => c.乗務員KEY == m04d.乗務員KEY).DefaultIfEmpty()
                             select new M90020_DDT3
                             {
                                 乗務員ID = m04.乗務員ID == null ? 0 : m04.乗務員ID,
                                 乗務員KEY = m04d.乗務員KEY,
                                 明細行 = m04d.明細行,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M04_DRDのデータ取得
        /// </summary>
        public List<M90020_DRD> SEARCH_MST90020_13()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m04d in context.M04_DRD
                             from m04 in context.M04_DRV.Where(c => c.乗務員KEY == m04d.乗務員KEY).DefaultIfEmpty()
                             from m05 in context.M05_CAR.Where(c => c.車輌KEY == m04d.車輌KEY).DefaultIfEmpty()
                             select new M90020_DRD
                             {
                                 乗務員KEY = m04d.乗務員KEY,
                                 乗務員ID = m04.乗務員ID == null ? 0 : m04.乗務員ID,
                                 車輌KEY = m04d.車輌KEY,
                                 車輌ID = m05.車輌ID == null ? 0 : m05.車輌ID,
                                 集計年月 = m04d.集計年月,

                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M04_DRSBのデータ取得
        /// </summary>
        public List<M90020_DRSB> SEARCH_MST90020_14()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m04_drsb in context.M04_DRSB
                             from m04 in context.M04_DRV.Where(c => c.乗務員KEY == m04_drsb.乗務員KEY).DefaultIfEmpty()
                             select new M90020_DRSB
                             {
                                 乗務員ID = m04.乗務員ID == null ? 0 : m04.乗務員ID,
                                 乗務員KEY = m04_drsb.乗務員KEY,
                                 集計年月 = m04_drsb.集計年月,
                                 経費項目ID = m04_drsb.経費項目ID,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M04_DRVPICのデータ取得
        /// </summary>
        public List<M90020_DRVPIC> SEARCH_MST90020_15()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m04 in context.M04_DRVPIC
                             select new M90020_DRVPIC
                             {
                                 乗務員KEY = m04.乗務員KEY,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M05_CARのデータ取得
        /// </summary>
        public List<M90020_CAR> SEARCH_MST90020_16(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m05 in context.M05_CAR
                             from m04 in context.M04_DRV.Where(c => c.乗務員KEY == m05.乗務員KEY).DefaultIfEmpty()
                             select new M90020_CAR
                             {
                                 車輌ID = m05.車輌ID,
                                 車輌KEY = m05.車輌KEY,
                                 乗務員ID = m04.乗務員ID == null ? 0 : m04.乗務員ID,
                                 乗務員KEY = m05.乗務員KEY
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_ZEIのデータ取得
        /// </summary>
        public List<M90020_CDT1> SEARCH_MST90020_17()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m05_c1 in context.M05_CDT1
                             from m05 in context.M05_CAR.Where(c => c.車輌KEY == m05_c1.車輌KEY).DefaultIfEmpty()
                             select new M90020_CDT1
                             {
                                 車輌ID = m05.車輌ID == null ? 0 : m05.車輌ID,
                                 車輌KEY = m05_c1.車輌KEY,
                                 配置年月日 = m05_c1.配置年月日,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_ZEIのデータ取得
        /// </summary>
        public List<M90020_CDT2> SEARCH_MST90020_18()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m05_c2 in context.M05_CDT2
                             from m05 in context.M05_CAR.Where(c => c.車輌KEY == m05_c2.車輌KEY).DefaultIfEmpty()
                             select new M90020_CDT2
                             {
                                 車輌ID = m05.車輌ID == null ? 0 : m05.車輌ID,
                                 車輌KEY = m05_c2.車輌KEY,
                                 加入年月日 = m05_c2.加入年月日,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_ZEIのデータ取得
        /// </summary>
        public List<M90020_CDT3> SEARCH_MST90020_19()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m05_c3 in context.M05_CDT3
                             from m05 in context.M05_CAR.Where(c => c.車輌KEY == m05_c3.車輌KEY).DefaultIfEmpty()
                             select new M90020_CDT3
                             {
                                 車輌ID = m05.車輌ID == null ? 0 : m05.車輌ID,
                                 車輌KEY = m05_c3.車輌KEY,
                                 加入年月日 = m05_c3.加入年月日,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_ZEIのデータ取得
        /// </summary>
        public List<M90020_CDT4> SEARCH_MST90020_20()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m05_c4 in context.M05_CDT4
                             from m05 in context.M05_CAR.Where(c => c.車輌KEY == m05_c4.車輌KEY).DefaultIfEmpty()
                             select new M90020_CDT4
                             {
                                 車輌ID = m05.車輌ID == null ? 0 : m05.車輌ID,
                                 車輌KEY = m05_c4.車輌KEY,
                                 年度 = m05_c4.年度,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_ZEIのデータ取得
        /// </summary>
        public List<M90020_TEN> SEARCH_MST90020_21()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m05_ten in context.M05_TEN
                             from m05 in context.M05_CAR.Where(c => c.車輌KEY == m05_ten.車輌KEY).DefaultIfEmpty()
                             select new M90020_TEN
                             {
                                 車輌ID = m05.車輌ID == null ? 0 : m05.車輌ID,
                                 車輌KEY = m05_ten.車輌KEY,
                                 年月 = m05_ten.年月,
                                 点検日 = m05_ten.点検日,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M06_SYAのデータ取得
        /// </summary>
        public List<M90020_SYA> SEARCH_MST90020_22(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m06 in context.M06_SYA
                             select new M90020_SYA
                             {
                                 車種ID = m06.車種ID,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_KEIのデータ取得
        /// </summary>
        public List<M90020_KEI> SEARCH_MST90020_23(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m07 in context.M07_KEI
                             where KeyList.Contains(m07.経費項目ID)
                             select new M90020_KEI
                             {
                                 経費項目ID = m07.経費項目ID,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_KEIのデータ取得
        /// </summary>
        public List<M90020_KOU> SEARCH_MST90020_24()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m07 in context.M07_KOU
                             from m05 in context.M05_CAR.Where(c => c.車輌KEY == m07.車輌KEY).DefaultIfEmpty()
                             select new M90020_KOU
                             {
                                 車輌ID = m05.車輌ID == null ? 0 : m05.車輌ID,
                                 経費項目ID = m07.経費項目ID,
                                 車輌KEY = m07.車輌KEY,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M08_TIKのデータ取得
        /// </summary>
        public List<M90020_TIK> SEARCH_MST90020_25(int[] ListID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m08 in context.M08_TIK
                             where ListID.Contains(m08.発着地ID)
                             select new M90020_TIK
                             {
                                 発着地ID = m08.発着地ID,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_HINのデータ取得
        /// </summary>
        public List<M90020_HIN> SEARCH_MST90020_26(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m09 in context.M09_HIN
                             where KeyList.Contains(m09.商品ID)
                             select new M90020_HIN
                             {
                                 商品ID = m09.商品ID,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M10_UHKのデータ取得
        /// </summary>
        public List<M90020_UHK> SEARCH_MST90020_27()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m10 in context.M10_UHK
                             from m01 in context.M01_TOK.Where(c => c.得意先KEY == m10.得意先KEY).DefaultIfEmpty()
                             select new M90020_UHK
                             {
                                 得意先ID = m01.得意先ID == null ? 0 : m01.得意先ID,
                                 得意先KEY = m10.得意先KEY,
                                 請求内訳ID = m10.請求内訳ID,
                             }).ToList();
                return query;
            }
        }


        /// <summary>
        /// M90020_TEKのデータ取得
        /// </summary>
        public List<M90020_TEK> SEARCH_MST90020_28(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m11 in context.M11_TEK
                             select new M90020_TEK
                             {
                                 摘要ID = m11.摘要ID
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M12_KISのデータ取得
        /// </summary>
        public List<M90020_KIS> SEARCH_MST90020_29(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m12 in context.M12_KIS
                             select new M90020_KIS
                             {
                                 規制区分ID = m12.規制区分ID,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M13_MOKのデータ取得
        /// </summary>
        public List<M90020_MOK> SEARCH_MST90020_30()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m13 in context.M13_MOK
                             from m05 in context.M05_CAR.Where(c => c.車輌KEY == m13.車輌KEY).DefaultIfEmpty()
                             select new M90020_MOK
                             {
                                 車輌ID = m05.車輌ID == null ? 0 : m05.車輌ID,
                                 車輌KEY = m13.車輌KEY,
                                 年月 = m13.年月,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M14_GSYAのデータ取得
        /// </summary>
        public List<M90020_GSYA> SEARCH_MST90020_31(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m14 in context.M14_GSYA
                             select new M90020_GSYA
                             {
                                 G車種ID = m14.G車種ID,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_BUMのデータ取得
        /// </summary>
        public List<M90020_JIS> SEARCH_MST90020_33(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m70 in context.M70_JIS
                             select new M90020_JIS
                             {
                                 自社ID = m70.自社ID
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_BUMのデータ取得
        /// </summary>
        public List<M90020_BUM> SEARCH_MST90020_34(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m71 in context.M71_BUM
                             select new M90020_BUM
                             {
                                 自社部門ID = m71.自社部門ID
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_BUMのデータ取得
        /// </summary>
        public List<M90020_TNT> SEARCH_MST90020_35(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m72 in context.M72_TNT
                             select new M90020_TNT
                             {
                                 担当者ID = m72.担当者ID,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_BUMのデータ取得
        /// </summary>
        public List<M90020_DBU> SEARCH_MST90020_37(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m76 in context.M76_DBU
                             select new M90020_DBU
                             {
                                 歩合計算区分ID = m76.歩合計算区分ID
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M77_TRHのデータ取得
        /// </summary>
        public List<M90020_TRH> SEARCH_MST90020_38(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m77 in context.M77_TRH
                             select new M90020_TRH
                             {
                                 取引区分ID = m77.取引区分ID,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M78_SYKのデータ取得
        /// </summary>
        public List<M90020_SYK> SEARCH_MST90020_39(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m78 in context.M78_SYK
                             select new M90020_SYK
                             {
                                 出勤区分ID = m78.出勤区分ID,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_BUMのデータ取得
        /// </summary>
        public List<M90020_ZKB> SEARCH_MST90020_40(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m79 in context.M79_ZKB
                             select new M90020_ZKB
                             {
                                 税区分ID = m79.税区分ID,
                             }).ToList();
                return query;
            }
        }


        /// <summary>
        /// M90020_BUMのデータ取得
        /// </summary>
        public List<M90020_OYK> SEARCH_MST90020_41(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m81 in context.M81_OYK
                             select new M90020_OYK
                             {
                                 親子区分ID = m81.親子区分ID,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_BUMのデータ取得
        /// </summary>
        public List<M90020_SEI> SEARCH_MST90020_42(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m82 in context.M82_SEI
                             select new M90020_SEI
                             {
                                 請求書区分ID = m82.請求書区分ID,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M83_UKEのデータ取得
        /// </summary>
        public List<M90020_UKE> SEARCH_MST90020_43(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m83 in context.M83_UKE
                             select new M90020_UKE
                             {
                                 運賃計算区分ID = m83.運賃計算区分ID,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M84_RIKのデータ取得
        /// </summary>
        public List<M90020_RIK> SEARCH_MST90020_44(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m84 in context.M84_RIK
                             select new M90020_RIK
                             {
                                 運輸局ID = m84.運輸局ID,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_ZEIのデータ取得
        /// </summary>
        public List<M90020_CNTL> SEARCH_MST90020_45(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m87 in context.M87_CNTL
                             select new M90020_CNTL
                             {
                                 管理ID = m87.管理ID
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M88_SEQのデータ取得
        /// </summary>
        public List<M90020_SEQ> SEARCH_MST90020_46(int[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m88 in context.M88_SEQ
                             select new M90020_SEQ
                             {
                                 明細番号ID = m88.明細番号ID,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90_GRIDのデータ取得
        /// </summary>
        public List<M90020_GRID> SEARCH_MST90020_47()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m90 in context.M90_GRID
                             select new M90020_GRID
                             {
                                 担当者ID = m90.担当者ID,
                                 GRIDID = m90.GRIDID,
                                 画面ID = m90.画面ID,
                                 列名 = m90.列名,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M91_OTANのデータ取得
        /// </summary>
        public List<M90020_OTAN> SEARCH_MST90020_48()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m91 in context.M91_OTAN
                             from m01 in context.M01_TOK.Where(c => c.得意先KEY == m91.支払先KEY).DefaultIfEmpty()
                             select new M90020_OTAN
                             {
                                 支払先ID = m01.得意先ID == null ? 0 : m01.得意先ID,
                                 適用開始年月日 = m91.適用開始年月日,
                                 支払先KEY = m91.支払先KEY,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_ZEIのデータ取得
        /// </summary>
        public List<M90020_KZEI> SEARCH_MST90020_49(DateTime[] keyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m92 in context.M92_KZEI
                             select new M90020_KZEI
                             {
                                 適用開始年月日 = m92.適用開始年月日,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// M90020_ZEIのデータ取得
        /// </summary>
        public List<M90020_ZEI> SEARCH_MST90020(DateTime[] KeyList)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m73 in context.M73_ZEI
                             select new M90020_ZEI
                             {
                                 適用開始日付 = m73.適用開始日付,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// T90020_T01_TRN
        /// </summary>
        public List<T90020_T01_TRN> SEARCH_TRN90020_00()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from t01 in context.T01_TRN
                             select new T90020_T01_TRN
                             {
                                 明細番号 = t01.明細番号,
                                 明細行 = t01.明細行,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// T90020_T02_UTRN
        /// </summary>
        public List<T90020_T02_UTRN> SEARCH_TRN90020_01()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from t02 in context.T02_UTRN
                             select new T90020_T02_UTRN
                             {
                                 明細番号 = t02.明細番号,
                                 明細行 = t02.明細行,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// T90020_T03_KTRAN
        /// </summary>
        public List<T90020_T03_KTRAN> SEARCH_TRN90020_02()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from t03 in context.T03_KTRN
                             select new T90020_T03_KTRAN
                             {
                                 明細番号 = t03.明細番号,
                                 明細行 = t03.明細行,
                             }).ToList();
                return query;
            }
        }

        /// <summary>
        /// T90020_T04_NYUK
        /// </summary>
        public List<T90020_T04_NYUK> SEARCH_TRN90020_03()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from t04 in context.T04_NYUK
                             select new T90020_T04_NYUK
                             {
                                 明細番号 = t04.明細番号,
                                 明細行 = t04.明細行,
                             }).ToList();
                return query;
            }
        }

        #endregion

        /// <summary>
        /// M73_ZEIのデータ取得
        /// </summary>
        public void SEARCH_MST900201(DataSet ds, int no, int check)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                try
                {
                    using (DbTransaction transaction = context.Connection.BeginTransaction())
                    {
                        #region 登録処理

                        //変数宣言
                        DataTable dt;
                        dt = ds.Tables["CSV取り込み"];

                        switch (no)
                        {
                            case 0:
                                #region
                                foreach (DataRow row in dt.Rows)
                                {
                                    // 20150715 wada add エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
//得意先ID,登録日時,更新日時,得意先名１,得意先名２,略称名,取引区分,Ｔ締日,Ｓ締日,かな読み,郵便番号,住所１,住所２,電話番号,ＦＡＸ,Ｔ集金日,Ｔサイト日,Ｔ税区分ID,Ｔ締日期首残,Ｔ月次期首残,Ｔ路線計算年度,Ｔ路線計算率,Ｔ路線計算まるめ,Ｓ集金日,Ｓサイト日,Ｓ税区分ID,Ｓ締日期首残,Ｓ月次期首残,Ｓ路線計算年度,Ｓ路線計算率,Ｓ路線計算まるめ,ラベル区分,親子区分ID,親ID,請求内訳管理区分,自社部門ID,請求運賃計算区分ID,支払運賃計算区分ID,請求書区分ID,請求書発行元ID,法人ナンバー,削除日付
                                        

                                        //sql = string.Format("INSERT INTO M01_TOK(得意先ID,登録日時,更新日時,得意先名１,得意先名２,略称名,取引区分,Ｔ締日,Ｓ締日,かな読み,郵便番号,住所１,住所２,電話番号,ＦＡＸ,Ｔ集金日,Ｔサイト日,Ｔ税区分ID,Ｔ締日期首残,Ｔ月次期首残,Ｔ路線計算年度,Ｔ路線計算率,Ｔ路線計算まるめ,Ｓ集金日,Ｓサイト日,Ｓ税区分ID,Ｓ締日期首残,Ｓ月次期首残,Ｓ路線計算年度,Ｓ路線計算率,Ｓ路線計算まるめ,ラベル区分,親子区分ID,親ID,請求内訳管理区分,自社部門ID,請求運賃計算区分ID,支払運賃計算区分ID,請求書区分ID,請求書発行元ID,法人ナンバー,削除日付")
                                        //    , colname, val.ToString(), p明細番号, p明細行, tanka.ToString());
                                        M01_TOK m01 = new M01_TOK();
                                        m01.得意先ID = ConvertToInt(row["得意先ID"]);

                                        // 登録／更新日時はトリガーで更新される模様
                                        m01.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m01.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);

                                        m01.得意先名１ = ConvertToStringNullable(row["得意先名１"]);
                                        m01.得意先名２ = ConvertToStringNullable(row["得意先名２"]);
                                        m01.略称名 = ConvertToStringNullable(row["略称名"]);
                                        m01.取引区分 = ConvertToInt(row["取引区分"]);
                                        m01.Ｔ締日 = ConvertToInt(row["Ｔ締日"]);
                                        m01.Ｓ締日 = ConvertToInt(row["Ｓ締日"]);
                                        m01.かな読み = ConvertToStringNullable(row["かな読み"]);
                                        m01.郵便番号 = ConvertToStringNullable(row["郵便番号"]);
                                        m01.住所１ = ConvertToStringNullable(row["住所１"]);
                                        m01.住所２ = ConvertToStringNullable(row["住所２"]);
                                        m01.電話番号 = ConvertToStringNullable(row["電話番号"]);
                                        m01.ＦＡＸ = ConvertToStringNullable(row["ＦＡＸ"]);
                                        m01.Ｔ集金日 = ConvertToInt(row["Ｔ集金日"]);
                                        m01.Ｔサイト日 = ConvertToInt(row["Ｔサイト日"]);
                                        m01.Ｔ税区分ID = ConvertToInt(row["Ｔ税区分ID"]);
                                        m01.Ｔ締日期首残 = ConvertToInt(row["Ｔ締日期首残"]);
                                        m01.Ｔ月次期首残 = ConvertToInt(row["Ｔ月次期首残"]);
                                        m01.Ｔ路線計算年度 = ConvertToInt(row["Ｔ路線計算年度"]);
                                        m01.Ｔ路線計算率 = ConvertToInt(row["Ｔ路線計算率"]);
                                        m01.Ｔ路線計算まるめ = ConvertToInt(row["Ｔ路線計算まるめ"]);
                                        m01.Ｓ集金日 = ConvertToInt(row["Ｓ集金日"]);
                                        m01.Ｓサイト日 = ConvertToInt(row["Ｓサイト日"]);
                                        m01.Ｓ税区分ID = ConvertToInt(row["Ｓ税区分ID"]);
                                        m01.Ｓ締日期首残 = ConvertToInt(row["Ｓ締日期首残"]);
                                        m01.Ｓ月次期首残 = ConvertToInt(row["Ｓ月次期首残"]);
                                        m01.Ｓ路線計算年度 = ConvertToInt(row["Ｓ路線計算年度"]);
                                        m01.Ｓ路線計算率 = ConvertToInt(row["Ｓ路線計算率"]);
                                        m01.Ｓ路線計算まるめ = ConvertToInt(row["Ｓ路線計算まるめ"]);
                                        m01.ラベル区分 = ConvertToInt(row["ラベル区分"]);
                                        m01.親子区分ID = ConvertToIntNullable(row["親子区分ID"]);
                                        m01.親ID = ConvertToIntNullable(row["親ID"]);
                                        m01.請求内訳管理区分 = ConvertToInt(row["請求内訳管理区分"]);
                                        m01.自社部門ID = ConvertToIntNullable(row["自社部門ID"]);
                                        m01.請求運賃計算区分ID = ConvertToInt(row["請求運賃計算区分ID"]);
                                        m01.支払運賃計算区分ID = ConvertToInt(row["支払運賃計算区分ID"]);
                                        m01.請求書区分ID = ConvertToInt(row["請求書区分ID"]);
                                        m01.請求書発行元ID = ConvertToIntNullable(row["請求書発行元ID"]);
                                        m01.法人ナンバー = ConvertToStringNullable(row["法人ナンバー"]);
                                        m01.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);

                                        context.M01_TOK.ApplyChanges(m01);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;
                                #endregion



                            // 20150729～ wada modify case 0と同様に展開
                            case 1:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        int i得意先KEY = ConvertToInt(row["得意先KEY"]);
                                        var ret = (from m01 in context.M01_TOK
                                                   where m01.得意先ID == i得意先KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m01.得意先KEY,
                                                   }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i得意先KEY = keys.Key;
                                        }

                                        M02_TTAN1 m02_t1 = new M02_TTAN1();
                                        m02_t1.得意先KEY = i得意先KEY;
                                        m02_t1.発地ID = ConvertToInt(row["発地ID"]);
                                        m02_t1.着地ID = ConvertToInt(row["着地ID"]);
                                        m02_t1.商品ID = ConvertToInt(row["商品ID"]);
                                        m02_t1.売上単価 = ConvertToDecimalNullable(row["売上単価"]);
                                        m02_t1.計算区分 = ConvertToIntNullable(row["計算区分"]);
                                        context.M02_TTAN1.ApplyChanges(m02_t1);

                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 2:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        int i得意先KEY = ConvertToInt(row["得意先KEY"]);
                                        var ret = (from m01 in context.M01_TOK
                                                   where m01.得意先ID == i得意先KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m01.得意先KEY,
                                                   }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i得意先KEY = keys.Key;
                                        }

                                        M02_TTAN2 m02_t2 = new M02_TTAN2();
                                        m02_t2.得意先KEY = i得意先KEY;
                                        m02_t2.発地ID = ConvertToInt(row["発地ID"]);
                                        m02_t2.着地ID = ConvertToInt(row["着地ID"]);
                                        m02_t2.車種ID = ConvertToInt(row["車種ID"]);
                                        m02_t2.売上単価 = ConvertToDecimal(row["売上単価"]);
                                        context.M02_TTAN2.ApplyChanges(m02_t2);


                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 3:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        int i得意先KEY = ConvertToInt(row["得意先KEY"]);
                                        var ret = (from m01 in context.M01_TOK
                                                   where m01.得意先ID == i得意先KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m01.得意先KEY,
                                                   }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i得意先KEY = keys.Key;
                                        }

                                        M02_TTAN3 m02_t3 = new M02_TTAN3();
                                        m02_t3.得意先KEY = i得意先KEY;
                                        m02_t3.距離 = ConvertToInt(row["距離"]);
                                        m02_t3.重量 = ConvertToDecimal(row["重量"]);
                                        m02_t3.運賃 = ConvertToIntNullable(row["運賃"]);
                                        context.M02_TTAN3.ApplyChanges(m02_t3);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 4:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        int i得意先KEY = ConvertToInt(row["得意先KEY"]);
                                        var ret = (from m01 in context.M01_TOK
                                                   where m01.得意先ID == i得意先KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m01.得意先KEY,
                                                   }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i得意先KEY = keys.Key;
                                        }

                                        M02_TTAN4 m02_t4 = new M02_TTAN4();
                                        m02_t4.得意先KEY = i得意先KEY;
                                        m02_t4.重量 = ConvertToDecimal(row["重量"]);
                                        m02_t4.個数 = ConvertToDecimal(row["個数"]);
                                        m02_t4.着地ID = ConvertToInt(row["着地ID"]);
                                        m02_t4.個建単価 = ConvertToDecimal(row["個建単価"]);
                                        m02_t4.個建金額 = ConvertToInt(row["個建金額"]);
                                        context.M02_TTAN4.ApplyChanges(m02_t4);

                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 5:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        int i支払先KEY = ConvertToInt(row["支払先KEY"]);
                                        var ret = (from m01 in context.M01_TOK
                                                   where m01.得意先ID == i支払先KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m01.得意先KEY,
                                                   }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i支払先KEY = keys.Key;
                                        }

                                        M03_YTAN1 m03_y1 = new M03_YTAN1();
                                        m03_y1.支払先KEY = i支払先KEY;
                                        m03_y1.発地ID = ConvertToInt(row["発地ID"]);
                                        m03_y1.着地ID = ConvertToInt(row["着地ID"]);
                                        m03_y1.商品ID = ConvertToInt(row["商品ID"]);
                                        m03_y1.支払単価 = ConvertToDecimalNullable(row["支払単価"]);
                                        m03_y1.計算区分 = ConvertToIntNullable(row["計算区分"]);
                                        context.M03_YTAN1.ApplyChanges(m03_y1);

                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 6:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {

                                        int i支払先KEY = ConvertToInt(row["支払先KEY"]);
                                        var ret = (from m01 in context.M01_TOK
                                                   where m01.得意先ID == i支払先KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m01.得意先KEY,
                                                   }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i支払先KEY = keys.Key;
                                        }

                                        M03_YTAN2 m03_y2 = new M03_YTAN2();
                                        m03_y2.支払先KEY = i支払先KEY;
                                        m03_y2.車種ID = ConvertToInt(row["車種ID"]);
                                        m03_y2.発地ID = ConvertToInt(row["発地ID"]);
                                        m03_y2.着地ID = ConvertToInt(row["着地ID"]);
                                        m03_y2.支払単価 = ConvertToDecimal(row["支払単価"]);
                                        context.M03_YTAN2.ApplyChanges(m03_y2);

                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 7:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        int i支払先KEY = ConvertToInt(row["支払先KEY"]);
                                        var ret = (from m01 in context.M01_TOK
                                                   where m01.得意先ID == i支払先KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m01.得意先KEY,
                                                   }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i支払先KEY = keys.Key;
                                        }

                                        M03_YTAN3 m03_y3 = new M03_YTAN3();
                                        m03_y3.支払先KEY = i支払先KEY;
                                        m03_y3.距離 = ConvertToInt(row["距離"]);
                                        m03_y3.重量 = ConvertToDecimal(row["重量"]);
                                        m03_y3.運賃 = ConvertToInt(row["運賃"]);
                                        context.M03_YTAN3.ApplyChanges(m03_y3);

                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 8:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        int i支払先KEY = ConvertToInt(row["支払先KEY"]);
                                        var ret = (from m01 in context.M01_TOK
                                                   where m01.得意先ID == i支払先KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m01.得意先KEY,
                                                   }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i支払先KEY = keys.Key;
                                        }

                                        M03_YTAN4 m03_y4 = new M03_YTAN4();
                                        m03_y4.支払先KEY = i支払先KEY;
                                        m03_y4.重量 = ConvertToDecimal(row["重量"]);
                                        m03_y4.個数 = ConvertToDecimal(row["個数"]);
                                        m03_y4.着地ID = ConvertToInt(row["着地ID"]);
                                        m03_y4.個建単価 = ConvertToDecimalNullable(row["個建単価"]);
                                        m03_y4.個建金額 = ConvertToIntNullable(row["個建金額"]);
                                        context.M03_YTAN4.ApplyChanges(m03_y4);

                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 9:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        M04_DRV m04 = new M04_DRV();
                                        m04.乗務員ID = ConvertToInt(row["乗務員ID"]);
                                        m04.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m04.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
                                        m04.乗務員名 = ConvertToString(row["乗務員名"]);
                                        m04.自傭区分 = ConvertToInt(row["自傭区分"]);
                                        m04.就労区分 = ConvertToInt(row["就労区分"]);
                                        m04.かな読み = ConvertToStringNullable(row["かな読み"]);
                                        m04.自社部門ID = ConvertToInt(row["自社部門ID"]);
                                        m04.歩合率 = ConvertToDecimal(row["歩合率"]);
                                        m04.性別区分 = ConvertToInt(row["性別区分"]);
                                        m04.郵便番号 = ConvertToStringNullable(row["郵便番号"]);
                                        m04.住所１ = ConvertToStringNullable(row["住所１"]);
                                        m04.住所２ = ConvertToStringNullable(row["住所２"]);
                                        m04.電話番号 = ConvertToStringNullable(row["電話番号"]);
                                        m04.携帯電話 = ConvertToStringNullable(row["携帯電話"]);
                                        m04.業務種類 = ConvertToStringNullable(row["業務種類"]);
                                        m04.血液型 = ConvertToIntNullable(row["血液型"]);
                                        m04.免許証番号 = ConvertToStringNullable(row["免許証番号"]);
                                        m04.免許証条件 = ConvertToStringNullable(row["免許証条件"]);
                                        m04.職種分類 = ConvertToStringNullable(row["職種分類"]);
                                        m04.免許有効番号1 = ConvertToStringNullable(row["免許有効番号1"]);
                                        m04.免許有効番号2 = ConvertToStringNullable(row["免許有効番号2"]);
                                        m04.免許有効番号3 = ConvertToStringNullable(row["免許有効番号3"]);
                                        m04.免許有効番号4 = ConvertToStringNullable(row["免許有効番号4"]);
                                        m04.履歴1 = ConvertToStringNullable(row["履歴1"]);
                                        m04.履歴2 = ConvertToStringNullable(row["履歴2"]);
                                        m04.履歴3 = ConvertToStringNullable(row["履歴3"]);
                                        m04.履歴4 = ConvertToStringNullable(row["履歴4"]);
                                        m04.履歴5 = ConvertToStringNullable(row["履歴5"]);
                                        m04.履歴6 = ConvertToStringNullable(row["履歴6"]);
                                        m04.履歴7 = ConvertToStringNullable(row["履歴7"]);
                                        m04.経験積載量1 = ConvertToDecimalNullable(row["経験積載量1"]);
                                        m04.経験事業所1 = ConvertToStringNullable(row["経験事業所1"]);
                                        m04.経験積載量2 = ConvertToDecimalNullable(row["経験積載量2"]);
                                        m04.経験事業所2 = ConvertToStringNullable(row["経験事業所2"]);
                                        m04.経験積載量3 = ConvertToDecimalNullable(row["経験積載量3"]);
                                        m04.経験事業所3 = ConvertToStringNullable(row["経験事業所3"]);
                                        m04.資格賞罰名1 = ConvertToStringNullable(row["資格賞罰名1"]);
                                        m04.資格賞罰内容1 = ConvertToStringNullable(row["資格賞罰内容1"]);
                                        m04.資格賞罰名2 = ConvertToStringNullable(row["資格賞罰名2"]);
                                        m04.資格賞罰内容2 = ConvertToStringNullable(row["資格賞罰内容2"]);
                                        m04.資格賞罰名3 = ConvertToStringNullable(row["資格賞罰名3"]);
                                        m04.資格賞罰内容3 = ConvertToStringNullable(row["資格賞罰内容3"]);
                                        m04.資格賞罰名4 = ConvertToStringNullable(row["資格賞罰内容4"]);
                                        m04.資格賞罰内容4 = ConvertToStringNullable(row["資格賞罰内容4"]);
                                        m04.資格賞罰名5 = ConvertToStringNullable(row["資格賞罰内容5"]);
                                        m04.資格賞罰内容5 = ConvertToStringNullable(row["資格賞罰内容5"]);
                                        m04.健康保険番号 = ConvertToStringNullable(row["健康保険番号"]);
                                        m04.厚生年金番号 = ConvertToStringNullable(row["厚生年金番号"]);
                                        m04.雇用保険番号 = ConvertToStringNullable(row["雇用保険番号"]);
                                        m04.労災保険番号 = ConvertToStringNullable(row["労災保険番号"]);
                                        m04.厚生年金基金番号 = ConvertToStringNullable(row["厚生年金基金番号"]);
                                        m04.家族連絡 = ConvertToStringNullable(row["家族連絡"]);
                                        m04.通勤方法 = ConvertToStringNullable(row["通勤方法"]);
                                        m04.家族氏名1 = ConvertToStringNullable(row["家族氏名1"]);
                                        m04.家族続柄1 = ConvertToStringNullable(row["家族続柄1"]);
                                        m04.家族その他1 = ConvertToStringNullable(row["家族その他1"]);
                                        m04.家族氏名2 = ConvertToStringNullable(row["家族氏名2"]);
                                        m04.家族続柄2 = ConvertToStringNullable(row["家族続柄2"]);
                                        m04.家族その他2 = ConvertToStringNullable(row["家族その他2"]);
                                        m04.家族氏名3 = ConvertToStringNullable(row["家族氏名3"]);
                                        m04.家族続柄3 = ConvertToStringNullable(row["家族続柄3"]);
                                        m04.家族その他3 = ConvertToStringNullable(row["家族その他3"]);
                                        m04.家族氏名4 = ConvertToStringNullable(row["家族氏名4"]);
                                        m04.家族続柄4 = ConvertToStringNullable(row["家族続柄4"]);
                                        m04.家族その他4 = ConvertToStringNullable(row["家族その他4"]);
                                        m04.家族氏名5 = ConvertToStringNullable(row["家族氏名5"]);
                                        m04.家族続柄5 = ConvertToStringNullable(row["家族続柄5"]);
                                        m04.家族その他5 = ConvertToStringNullable(row["家族その他5"]);
                                        m04.退職理由 = ConvertToStringNullable(row["退職理由"]);
                                        m04.特記事項1 = ConvertToStringNullable(row["特記事項1"]);
                                        m04.特記事項2 = ConvertToStringNullable(row["特記事項2"]);
                                        m04.特記事項3 = ConvertToStringNullable(row["特記事項3"]);
                                        m04.特記事項4 = ConvertToStringNullable(row["特記事項4"]);
                                        m04.特記事項5 = ConvertToStringNullable(row["特記事項5"]);
                                        m04.健康状態 = ConvertToStringNullable(row["健康状態"]);
                                        m04.水揚連動区分 = ConvertToInt(row["水揚連動区分"]);
                                        m04.自社ID = ConvertToIntNullable(row["自社ID"]);
                                        m04.個人ナンバー = ConvertToStringNullable(row["特記事項5"]);
                                        m04.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);
                                        m04.デジタコCD = ConvertToIntNullable(row["デジタコCD"]);
                                        m04.免許種類1 = ConvertToIntNullable(row["免許種類1"]);
                                        m04.免許種類2 = ConvertToIntNullable(row["免許種類2"]);
                                        m04.免許種類3 = ConvertToIntNullable(row["免許種類3"]);
                                        m04.免許種類4 = ConvertToIntNullable(row["免許種類4"]);
                                        m04.免許種類5 = ConvertToIntNullable(row["免許種類5"]);
                                        m04.免許種類6 = ConvertToIntNullable(row["免許種類6"]);
                                        m04.免許種類7 = ConvertToIntNullable(row["免許種類7"]);
                                        m04.免許種類8 = ConvertToIntNullable(row["免許種類8"]);
                                        m04.免許種類9 = ConvertToIntNullable(row["免許種類9"]);
                                        m04.免許種類10 = ConvertToIntNullable(row["免許種類10"]);
                                        m04.職種分類区分 = ConvertToIntNullable(row["職種分類区分"]);
                                        m04.作成番号 = ConvertToIntNullable(row["作成番号"]);
                                        m04.撮影年 = ConvertToIntNullable(row["撮影年"]);
                                        m04.撮影月 = ConvertToIntNullable(row["撮影月"]);
                                        m04.経験種類1 = ConvertToIntNullable(row["経験種類1"]);
                                        m04.経験種類2 = ConvertToIntNullable(row["経験種類2"]);
                                        m04.経験種類3 = ConvertToIntNullable(row["経験種類3"]);
                                        m04.経験定員1 = ConvertToIntNullable(row["経験定員1"]);
                                        m04.経験定員2 = ConvertToIntNullable(row["経験定員2"]);
                                        m04.経験定員3 = ConvertToIntNullable(row["経験定員3"]);
                                        m04.経験年1 = ConvertToIntNullable(row["経験年1"]);
                                        m04.経験年2 = ConvertToIntNullable(row["経験年2"]);
                                        m04.経験年3 = ConvertToIntNullable(row["経験年3"]);
                                        m04.経験月1 = ConvertToIntNullable(row["経験月1"]);
                                        m04.経験月2 = ConvertToIntNullable(row["経験月2"]);
                                        m04.経験月3 = ConvertToIntNullable(row["経験月3"]);
                                        m04.事業者コード = ConvertToIntNullable(row["事業者コード"]);
                                        m04.通勤時間 = ConvertToIntNullable(row["通勤時間"]);
                                        m04.通勤分 = ConvertToIntNullable(row["通勤分"]);
                                        m04.住居の種類 = ConvertToIntNullable(row["住居の種類"]);
                                        m04.家族血液型1 = ConvertToIntNullable(row["家族血液型1"]);
                                        m04.家族血液型2 = ConvertToIntNullable(row["家族血液型2"]);
                                        m04.家族血液型3 = ConvertToIntNullable(row["家族血液型3"]);
                                        m04.家族血液型4 = ConvertToIntNullable(row["家族血液型4"]);
                                        m04.家族血液型5 = ConvertToIntNullable(row["家族血液型5"]);
                                        m04.固定給与 = ConvertToIntNullable(row["固定給与"]);
                                        m04.固定賞与積立金 = ConvertToIntNullable(row["固定賞与積立金"]);
                                        m04.固定福利厚生費 = ConvertToIntNullable(row["固定福利厚生費"]);
                                        m04.固定法定福利費 = ConvertToIntNullable(row["固定法定福利費"]);
                                        m04.固定退職引当金 = ConvertToIntNullable(row["固定退職引当金"]);
                                        m04.固定労働保険 = ConvertToIntNullable(row["固定労働保険"]);
                                        m04.自社ID = ConvertToIntNullable(row["自社ID"]);
                                        m04.生年月日 = ConvertToDateTimeNullable(row["生年月日"]);
                                        m04.入社日 = ConvertToDateTimeNullable(row["入社日"]);
                                        m04.選任年月日 = ConvertToDateTimeNullable(row["選任年月日"]);
                                        m04.免許証取得年月日 = ConvertToDateTimeNullable(row["免許証取得年月日"]);
                                        m04.免許有効年月日1 = ConvertToDateTimeNullable(row["免許有効年月日1"]);
                                        m04.免許有効年月日2 = ConvertToDateTimeNullable(row["免許有効年月日2"]);
                                        m04.免許有効年月日3 = ConvertToDateTimeNullable(row["免許有効年月日3"]);
                                        m04.免許有効年月日4 = ConvertToDateTimeNullable(row["免許有効年月日4"]);
                                        m04.履歴年月日1 = ConvertToDateTimeNullable(row["履歴年月日1"]);
                                        m04.履歴年月日2 = ConvertToDateTimeNullable(row["履歴年月日2"]);
                                        m04.履歴年月日3 = ConvertToDateTimeNullable(row["履歴年月日3"]);
                                        m04.履歴年月日4 = ConvertToDateTimeNullable(row["履歴年月日4"]);
                                        m04.履歴年月日5 = ConvertToDateTimeNullable(row["履歴年月日5"]);
                                        m04.履歴年月日6 = ConvertToDateTimeNullable(row["履歴年月日6"]);
                                        m04.履歴年月日7 = ConvertToDateTimeNullable(row["履歴年月日7"]);
                                        m04.資格賞罰年月日1 = ConvertToDateTimeNullable(row["資格賞罰年月日1"]);
                                        m04.資格賞罰年月日2 = ConvertToDateTimeNullable(row["資格賞罰年月日2"]);
                                        m04.資格賞罰年月日3 = ConvertToDateTimeNullable(row["資格賞罰年月日3"]);
                                        m04.資格賞罰年月日4 = ConvertToDateTimeNullable(row["資格賞罰年月日4"]);
                                        m04.資格賞罰年月日5 = ConvertToDateTimeNullable(row["資格賞罰年月日5"]);
                                        m04.健康保険加入日 = ConvertToDateTimeNullable(row["健康保険加入日"]);
                                        m04.厚生年金加入日 = ConvertToDateTimeNullable(row["厚生年金加入日"]);
                                        m04.雇用保険加入日 = ConvertToDateTimeNullable(row["雇用保険加入日"]);
                                        m04.労災保険加入日 = ConvertToDateTimeNullable(row["労災保険加入日"]);
                                        m04.厚生年金基金加入日 = ConvertToDateTimeNullable(row["厚生年金基金加入日"]);
                                        m04.家族生年月日1 = ConvertToDateTimeNullable(row["家族生年月日1"]);
                                        m04.家族生年月日2 = ConvertToDateTimeNullable(row["家族生年月日2"]);
                                        m04.家族生年月日3 = ConvertToDateTimeNullable(row["家族生年月日3"]);
                                        m04.家族生年月日4 = ConvertToDateTimeNullable(row["家族生年月日4"]);
                                        m04.家族生年月日5 = ConvertToDateTimeNullable(row["家族生年月日5"]);
                                        m04.退職年月日 = ConvertToDateTimeNullable(row["退職年月日"]);
                                        m04.健康診断年月日1 = ConvertToDateTimeNullable(row["健康診断年月日1"]);
                                        m04.健康診断年月日2 = ConvertToDateTimeNullable(row["健康診断年月日2"]);
                                        m04.健康診断年月日3 = ConvertToDateTimeNullable(row["健康診断年月日3"]);
                                        m04.健康診断年月日4 = ConvertToDateTimeNullable(row["健康診断年月日4"]);
                                        m04.健康診断年月日5 = ConvertToDateTimeNullable(row["健康診断年月日5"]);
                                        context.M04_DRV.ApplyChanges(m04);

                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 10:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {

                                        int i乗務員KEY = ConvertToInt(row["乗務員KEY"]);
                                        var ret = (from m04 in context.M04_DRV
                                                   where m04.乗務員ID == i乗務員KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m04.乗務員KEY,
                                                   }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i乗務員KEY = keys.Key;
                                        }

                                        M04_DDT1 m04_ddt1 = new M04_DDT1();
                                        m04_ddt1.乗務員KEY = i乗務員KEY;
                                        m04_ddt1.明細行 = ConvertToInt(row["明細行"]);
                                        m04_ddt1.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m04_ddt1.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
                                        m04_ddt1.実施機関名 = ConvertToStringNullable(row["実施機関名"]);
                                        m04_ddt1.所見摘要 = ConvertToStringNullable(row["所見摘要"]);
                                        m04_ddt1.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);
                                        m04_ddt1.対象種類 = ConvertToIntNullable(row["対象種類"]);
                                        m04_ddt1.実施年月日 = ConvertToDateTimeNullable(row["実施年月日"]);
                                        context.M04_DDT1.ApplyChanges(m04_ddt1);

                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 11:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {

                                        int i乗務員KEY = ConvertToInt(row["乗務員KEY"]);
                                        var ret = (from m04 in context.M04_DRV
                                                   where m04.乗務員ID == i乗務員KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m04.乗務員KEY,
                                                   }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i乗務員KEY = keys.Key;
                                        }

                                        M04_DDT2 m04_ddt2 = new M04_DDT2();
                                        m04_ddt2.乗務員KEY = i乗務員KEY;
                                        m04_ddt2.明細行 = ConvertToInt(row["明細行"]);
                                        m04_ddt2.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m04_ddt2.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
                                        m04_ddt2.発生年月日 = ConvertToDateTimeNullable(row["発生年月日"]);
                                        m04_ddt2.概要処置 = ConvertToStringNullable(row["概要処置"]);
                                        m04_ddt2.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);
                                        m04_ddt2.発生年月日 = ConvertToDateTimeNullable(row["発生年月日"]);
                                        context.M04_DDT2.ApplyChanges(m04_ddt2);

                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 12:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {

                                        int i乗務員KEY = ConvertToInt(row["乗務員KEY"]);
                                        var ret = (from m04 in context.M04_DRV
                                                   where m04.乗務員ID == i乗務員KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m04.乗務員KEY,
                                                   }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i乗務員KEY = keys.Key;
                                        }

                                        M04_DDT3 m04_ddt3 = new M04_DDT3();
                                        m04_ddt3.乗務員KEY = i乗務員KEY;
                                        m04_ddt3.明細行 = ConvertToInt(row["明細行"]);
                                        m04_ddt3.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m04_ddt3.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
                                        m04_ddt3.実施年月日 = ConvertToDateTimeNullable(row["実施年月日"]);
                                        m04_ddt3.教育種類 = ConvertToIntNullable(row["教育種類"]);
                                        m04_ddt3.教育内容 = ConvertToStringNullable(row["教育内容"]);
                                        m04_ddt3.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);
                                        m04_ddt3.教育種類 = ConvertToIntNullable(row["教育種類"]);
                                        m04_ddt3.実施年月日 = ConvertToDateTimeNullable(row["実施年月日"]);
                                        context.M04_DDT3.ApplyChanges(m04_ddt3);

                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 13:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        int i乗務員KEY = ConvertToInt(row["乗務員KEY"]);
                                        var ret = (from m04 in context.M04_DRV
                                                   where m04.乗務員ID == i乗務員KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m04.乗務員KEY,
                                                   }).AsQueryable();

                                        int i車輌KEY = ConvertToInt(row["車輌KEY"]);
                                        var ret1 = (from m05 in context.M05_CAR
                                                    where m05.車輌ID == i車輌KEY
                                                    select new MST90020_KEY
                                                    {
                                                        Key = m05.車輌KEY,
                                                    }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i乗務員KEY = keys.Key;
                                        }

                                        foreach (var keys in ret1)
                                        {
                                            i車輌KEY = keys.Key;
                                        }


                                        M04_DRD m04_drd = new M04_DRD();
                                        m04_drd.乗務員KEY = i乗務員KEY;
                                        m04_drd.集計年月 = ConvertToInt(row["集計年月"]);
                                        m04_drd.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m04_drd.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
                                        m04_drd.自社部門ID = ConvertToInt(row["自社部門ID"]);
                                        m04_drd.車種ID = ConvertToInt(row["車種ID"]);
                                        m04_drd.車輌KEY = i車輌KEY;
                                        m04_drd.営業日数 = ConvertToInt(row["営業日数"]);
                                        m04_drd.稼働日数 = ConvertToInt(row["稼働日数"]);
                                        m04_drd.走行ＫＭ = ConvertToInt(row["走行ＫＭ"]);
                                        m04_drd.実車ＫＭ = ConvertToInt(row["実車ＫＭ"]);
                                        m04_drd.輸送屯数 = ConvertToDecimal(row["輸送屯数"]);
                                        m04_drd.運送収入 = ConvertToInt(row["運送収入"]);
                                        m04_drd.燃料Ｌ = ConvertToInt(row["燃料Ｌ"]);
                                        m04_drd.一般管理費 = ConvertToInt(row["一般管理費"]);
                                        m04_drd.拘束時間 = ConvertToDecimal(row["拘束時間"]);
                                        m04_drd.運転時間 = ConvertToDecimal(row["運転時間"]);
                                        m04_drd.高速時間 = ConvertToDecimal(row["高速時間"]);
                                        m04_drd.作業時間 = ConvertToDecimal(row["作業時間"]);
                                        m04_drd.待機時間 = ConvertToDecimal(row["待機時間"]);
                                        m04_drd.休憩時間 = ConvertToDecimal(row["休憩時間"]);
                                        m04_drd.残業時間 = ConvertToDecimal(row["残業時間"]);
                                        m04_drd.深夜時間 = ConvertToDecimal(row["深夜時間"]);
                                        m04_drd.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);
                                        context.M04_DRD.ApplyChanges(m04_drd);

                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 14:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {

                                        int i乗務員KEY = ConvertToInt(row["乗務員KEY"]);
                                        var ret = (from m04 in context.M04_DRV
                                                   where m04.乗務員ID == i乗務員KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m04.乗務員KEY,
                                                   }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i乗務員KEY = keys.Key;
                                        }

                                        M04_DRSB m04_dbsb = new M04_DRSB();
                                        m04_dbsb.乗務員KEY = i乗務員KEY;
                                        m04_dbsb.集計年月 = ConvertToInt(row["集計年月"]);
                                        m04_dbsb.経費項目ID = ConvertToInt(row["経費項目ID"]);
                                        m04_dbsb.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m04_dbsb.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
                                        m04_dbsb.経費項目名 = ConvertToString(row["経費項目名"]);
                                        m04_dbsb.固定変動区分 = ConvertToInt(row["固定変動区分"]);
                                        m04_dbsb.金額 = ConvertToInt(row["金額"]);
                                        m04_dbsb.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);
                                        context.M04_DRSB.ApplyChanges(m04_dbsb);

                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 15:

                                // 処理なし
                                break;

                            case 16:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        int i乗務員KEY = ConvertToInt(row["乗務員KEY"]);
                                        var ret = (from m04 in context.M04_DRV
                                                   where m04.乗務員ID == i乗務員KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m04.乗務員KEY,
                                                   }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i乗務員KEY = keys.Key;
                                        }

                                        M05_CAR m05 = new M05_CAR();
                                        m05.車輌ID = ConvertToInt(row["車輌ID"]);
                                        m05.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m05.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
                                        m05.車輌番号 = ConvertToString(row["車輌番号"]);
                                        m05.車種ID = ConvertToIntNullable(row["車種ID"]);
                                        m05.乗務員KEY = i乗務員KEY;
                                        m05.運輸局ID = ConvertToInt(row["運輸局ID"]);
                                        m05.自傭区分 = ConvertToInt(row["自傭区分"]);
                                        m05.廃車区分 = ConvertToInt(row["廃車区分"]);
                                        m05.車輌登録番号 = ConvertToStringNullable(row["車輌登録番号"]);
                                        m05.初年度登録年 = ConvertToInt(row["初年度登録年"]);
                                        m05.初年度登録月 = ConvertToInt(row["初年度登録月"]);
                                        m05.自動車種別 = ConvertToStringNullable(row["自動車種別"]);
                                        m05.用途 = ConvertToStringNullable(row["用途"]);
                                        m05.自家用事業用 = ConvertToStringNullable(row["自家用事業用"]);
                                        m05.車体形状 = ConvertToStringNullable(row["車体形状"]);
                                        m05.車名 = ConvertToStringNullable(row["車名"]);
                                        m05.型式 = ConvertToStringNullable(row["型式"]);
                                        m05.乗車定員 = ConvertToInt(row["乗車定員"]);
                                        m05.車輌重量 = ConvertToInt(row["車輌重量"]);
                                        m05.車輌最大積載量 = ConvertToInt(row["車輌最大積載量"]);
                                        m05.車輌総重量 = ConvertToInt(row["車輌総重量"]);
                                        m05.車輌実積載量 = ConvertToInt(row["車輌実積載量"]);
                                        m05.車台番号 = ConvertToStringNullable(row["車台番号"]);
                                        m05.原動機型式 = ConvertToStringNullable(row["原動機型式"]);
                                        m05.長さ = ConvertToInt(row["長さ"]);
                                        m05.幅 = ConvertToInt(row["幅"]);
                                        m05.高さ = ConvertToInt(row["高さ"]);
                                        m05.総排気量 = ConvertToInt(row["総排気量"]);
                                        m05.燃料種類 = ConvertToStringNullable(row["燃料種類"]);
                                        m05.現在メーター = ConvertToInt(row["現在メーター"]);
                                        m05.デジタコCD = ConvertToInt(row["デジタコCD"]);
                                        m05.所有者名 = ConvertToStringNullable(row["所有者名"]);
                                        m05.所有者住所 = ConvertToStringNullable(row["所有者住所"]);
                                        m05.使用者名 = ConvertToStringNullable(row["使用者名"]);
                                        m05.使用者住所 = ConvertToStringNullable(row["使用者住所"]);
                                        m05.使用本拠位置 = ConvertToStringNullable(row["使用本拠位置"]);
                                        m05.備考 = ConvertToStringNullable(row["備考"]);
                                        m05.型式指定番号 = ConvertToStringNullable(row["型式指定番号"]);
                                        m05.前前軸重 = ConvertToInt(row["前前軸重"]);
                                        m05.前後軸重 = ConvertToInt(row["前後軸重"]);
                                        m05.後前軸重 = ConvertToInt(row["後前軸重"]);
                                        m05.後後軸重 = ConvertToInt(row["後後軸重"]);
                                        m05.燃料費単価 = ConvertToDecimal(row["燃料費単価"]);
                                        m05.油脂費単価 = ConvertToDecimal(row["油脂費単価"]);
                                        m05.タイヤ費単価 = ConvertToDecimal(row["タイヤ費単価"]);
                                        m05.修繕費単価 = ConvertToDecimal(row["修繕費単価"]);
                                        m05.車検費単価 = ConvertToDecimal(row["車検費単価"]);
                                        m05.CO2区分 = ConvertToInt(row["CO2区分"]);
                                        m05.基準燃費 = ConvertToDecimal(row["基準燃費"]);
                                        m05.黒煙規制値 = ConvertToDecimal(row["黒煙規制値"]);
                                        m05.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);
                                        m05.自社部門ID = ConvertToIntNullable(row["自社部門ID"]);
                                        m05.G車種ID = ConvertToIntNullable(row["G車種ID"]);
                                        m05.規制区分ID = ConvertToIntNullable(row["規制区分ID"]);
                                        m05.固定自動車税 = ConvertToIntNullable(row["固定自動車税"]);
                                        m05.固定重量税 = ConvertToIntNullable(row["固定重量税"]);
                                        m05.固定取得税 = ConvertToIntNullable(row["固定取得税"]);
                                        m05.固定自賠責保険 = ConvertToIntNullable(row["固定自賠責保険"]);
                                        m05.固定車輌保険 = ConvertToIntNullable(row["固定車輌保険"]);
                                        m05.固定対人保険 = ConvertToIntNullable(row["固定対人保険"]);
                                        m05.固定対物保険 = ConvertToIntNullable(row["固定対物保険"]);
                                        m05.固定貨物保険 = ConvertToIntNullable(row["固定貨物保険"]);
                                        m05.廃車日 = ConvertToDateTimeNullable(row["廃車日"]);
                                        m05.次回車検日 = ConvertToDateTimeNullable(row["次回車検日"]);
                                        m05.登録日 = ConvertToDateTimeNullable(row["登録日"]);
                                        context.M05_CAR.ApplyChanges(m05);

                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 17:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {

                                        int i車輌KEY = ConvertToInt(row["車輌KEY"]);
                                        var ret = (from m05 in context.M05_CAR
                                                    where m05.車輌ID == i車輌KEY
                                                    select new MST90020_KEY
                                                    {
                                                        Key = m05.車輌KEY,
                                                    }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i車輌KEY = keys.Key;
                                        }


                                        M05_CDT1 m05_c1 = new M05_CDT1();
                                        m05_c1.車輌KEY = i車輌KEY;
                                        m05_c1.配置年月日 = ConvertToDateTime(row["配置年月日"]);
                                        m05_c1.営業所名 = ConvertToStringNullable(row["営業所名"]);
                                        m05_c1.転出先 = ConvertToStringNullable(row["転出先"]);
                                        context.M05_CDT1.ApplyChanges(m05_c1);

                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 18:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        int i車輌KEY = ConvertToInt(row["車輌KEY"]);
                                        var ret = (from m05 in context.M05_CAR
                                                   where m05.車輌ID == i車輌KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m05.車輌KEY,
                                                   }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i車輌KEY = keys.Key;
                                        }


                                        M05_CDT2 m05_c2 = new M05_CDT2();
                                        m05_c2.車輌KEY = i車輌KEY;
                                        m05_c2.加入年月日 = ConvertToDateTime(row["加入年月日"]);
                                        m05_c2.期限 = ConvertToDateTime(row["期限"]);
                                        m05_c2.契約先 = ConvertToStringNullable(row["契約先"]);
                                        m05_c2.保険証番号 = ConvertToStringNullable(row["保険証番号"]);
                                        m05_c2.月数 = ConvertToInt(row["月数"]);
                                        context.M05_CDT2.ApplyChanges(m05_c2);

                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 19:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        int i車輌KEY = ConvertToInt(row["車輌KEY"]);
                                        var ret = (from m05 in context.M05_CAR
                                                   where m05.車輌ID == i車輌KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m05.車輌KEY,
                                                   }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i車輌KEY = keys.Key;
                                        }

                                        M05_CDT3 m05_c3 = new M05_CDT3();
                                        m05_c3.車輌KEY = i車輌KEY;
                                        m05_c3.加入年月日 = ConvertToDateTime(row["加入年月日"]);
                                        m05_c3.期限 = ConvertToDateTime(row["期限"]);
                                        m05_c3.契約先 = ConvertToStringNullable(row["契約先"]);
                                        m05_c3.保険証番号 = ConvertToStringNullable(row["保険証番号"]);
                                        m05_c3.月数 = ConvertToInt(row["月数"]);
                                        context.M05_CDT3.ApplyChanges(m05_c3);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 20:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        int i車輌KEY = ConvertToInt(row["車輌KEY"]);
                                        var ret = (from m05 in context.M05_CAR
                                                   where m05.車輌ID == i車輌KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m05.車輌KEY,
                                                   }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i車輌KEY = keys.Key;
                                        }

                                        M05_CDT4 m05_c4 = new M05_CDT4();
                                        m05_c4.車輌KEY = i車輌KEY;
                                        m05_c4.年度 = ConvertToInt(row["年度"]);
                                        m05_c4.自動車税年月 = ConvertToIntNullable(row["自動車税年月"]);
                                        m05_c4.自動車税 = ConvertToIntNullable(row["自動車税"]);
                                        m05_c4.重量税年月 = ConvertToIntNullable(row["重量税年月"]);
                                        m05_c4.重量税 = ConvertToIntNullable(row["重量税"]);
                                        context.M05_CDT4.ApplyChanges(m05_c4);

                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 21:


                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        int i乗務員KEY = ConvertToInt(row["乗務員KEY"]);
                                        var ret = (from m04 in context.M04_DRV
                                                   where m04.乗務員ID == i乗務員KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m04.乗務員KEY,
                                                   }).AsQueryable();

                                        int i車輌KEY = ConvertToInt(row["車輌KEY"]);
                                        var ret1 = (from m05 in context.M05_CAR
                                                    where m05.車輌ID == i車輌KEY
                                                    select new MST90020_KEY
                                                    {
                                                        Key = m05.車輌KEY,
                                                    }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i乗務員KEY = keys.Key;
                                        }

                                        foreach (var keys in ret1)
                                        {
                                            i車輌KEY = keys.Key;
                                        }

                                        M05_TEN m05_ten = new M05_TEN();
                                        m05_ten.車輌KEY = i車輌KEY;
                                        m05_ten.年月 = ConvertToInt(row["年月"]);
                                        m05_ten.点検日 = ConvertToInt(row["点検日"]);
                                        m05_ten.チェック = ConvertToStringNullable(row["チェック"]);
                                        m05_ten.エアコン区分 = ConvertToIntNullable(row["エアコン区分"]);
                                        m05_ten.エアコン備考 = ConvertToStringNullable(row["エアコン備考"]);
                                        m05_ten.異音備考 = ConvertToStringNullable(row["異音備考"]);
                                        m05_ten.排気区分 = ConvertToIntNullable(row["排気区分"]);
                                        m05_ten.排気備考 = ConvertToStringNullable(row["排気備考"]);
                                        m05_ten.燃費区分 = ConvertToIntNullable(row["燃費区分"]);
                                        m05_ten.燃費備考 = ConvertToStringNullable(row["燃費備考"]);
                                        m05_ten.その他区分 = ConvertToIntNullable(row["その他区分"]);
                                        m05_ten.その他備考 = ConvertToStringNullable(row["その他備考"]);
                                        m05_ten.乗務員KEY = i乗務員KEY;
                                        m05_ten.乗務員名 = ConvertToStringNullable(row["乗務員名"]);
                                        m05_ten.整備指示 = ConvertToStringNullable(row["整備指示"]);
                                        m05_ten.指示日付 = ConvertToDateTimeNullable(row["指示日付"]);
                                        m05_ten.整備部品 = ConvertToStringNullable(row["整備部品"]);
                                        m05_ten.部品日付 = ConvertToDateTimeNullable(row["部品日付"]);
                                        m05_ten.整備結果 = ConvertToStringNullable(row["整備結果"]);
                                        m05_ten.結果日付 = ConvertToDateTimeNullable(row["結果日付"]);
                                        context.M05_TEN.ApplyChanges(m05_ten);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 22:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        M06_SYA m06 = new M06_SYA();
                                        m06.車種ID = ConvertToInt(row["車種ID"]);
                                        m06.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m06.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
                                        m06.車種名 = ConvertToStringNullable(row["車種名"]);
                                        m06.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);
                                        m06.積載重量 = ConvertToDecimalNullable(row["積載重量"]);
                                        context.M06_SYA.ApplyChanges(m06);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 23:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        M07_KEI m07 = new M07_KEI();
                                        m07.経費項目ID = ConvertToInt(row["経費項目ID"]);
                                        m07.経費項目名 = ConvertToStringNullable(row["経費項目名"]);
                                        m07.固定変動区分 = ConvertToInt(row["固定変動区分"]);
                                        m07.編集区分 = ConvertToIntNullable(row["編集区分"]);
                                        m07.グリーン区分 = ConvertToIntNullable(row["グリーン区分"]);
                                        m07.経費区分 = ConvertToIntNullable(row["経費区分"]);
                                        context.M07_KEI.ApplyChanges(m07);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 24:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        int i車輌KEY = ConvertToInt(row["車輌KEY"]);
                                        var ret = (from m05 in context.M05_CAR
                                                   where m05.車輌ID == i車輌KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m05.車輌KEY,
                                                   }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i車輌KEY = keys.Key;
                                        }


                                        M07_KOU m07_kou = new M07_KOU();
                                        m07_kou.経費項目ID = ConvertToInt(row["経費項目ID"]);
                                        m07_kou.車輌KEY = i車輌KEY;
                                        m07_kou.交換期間 = ConvertToIntNullable(row["交換期間"]);
                                        m07_kou.交換距離 = ConvertToIntNullable(row["交換距離"]);
                                        context.M07_KOU.ApplyChanges(m07_kou);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 25:

                                foreach (DataRow row in dt.Rows)
                                {
									try
									{
										// エラーでない行を登録対象とする。
										if (!row.HasErrors)
										{
											M08_TIK m08 = new M08_TIK();
											m08.発着地ID = ConvertToInt(row["発着地ID"]);
											m08.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
											m08.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
											m08.発着地名 = ConvertToStringNullable(row["発着地名"]);
											m08.かな読み = ConvertToStringNullable(row["かな読み"]);
											m08.郵便番号 = ConvertToStringNullable(row["郵便番号"]);
											m08.住所１ = ConvertToStringNullable(row["住所１"]);
											m08.住所２ = ConvertToStringNullable(row["住所２"]);
											m08.電話番号 = ConvertToStringNullable(row["電話番号"]);
											m08.ＦＡＸ番号 = ConvertToStringNullable(row["ＦＡＸ番号"]);
											m08.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);
											m08.タリフ距離 = ConvertToIntNullable(row["タリフ距離"]);
											m08.配送エリアID = ConvertToIntNullable(row["配送エリアID"]);
											context.M08_TIK.ApplyChanges(m08);
										}
									}
									catch (Exception ex)
									{ }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 26:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        M09_HIN m09 = new M09_HIN();
                                        m09.商品ID = ConvertToInt(row["商品ID"]);
                                        m09.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m09.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
                                        m09.商品名 = ConvertToString(row["商品名"]);
                                        m09.単位 = ConvertToStringNullable(row["単位"]);
                                        m09.商品重量 = ConvertToDecimalNullable(row["商品重量"]);
                                        m09.商品才数 = ConvertToDecimalNullable(row["商品才数"]);
                                        m09.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);
                                        context.M09_HIN.ApplyChanges(m09);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 27:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        int i得意先KEY = ConvertToInt(row["得意先KEY"]);
                                        var ret = (from m01 in context.M01_TOK
                                                   where m01.得意先ID == i得意先KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m01.得意先KEY,
                                                   }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i得意先KEY = keys.Key;
                                        }

                                        M10_UHK m10 = new M10_UHK();
                                        m10.得意先KEY = i得意先KEY;
                                        m10.請求内訳ID = ConvertToInt(row["請求内訳ID"]);
                                        m10.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m10.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
                                        m10.請求内訳名 = ConvertToString(row["請求内訳名"]);
                                        m10.かな読み = ConvertToStringNullable(row["かな読み"]);
                                        m10.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);
                                        context.M10_UHK.ApplyChanges(m10);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 28:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        M11_TEK m11 = new M11_TEK();
                                        m11.摘要ID = ConvertToInt(row["摘要ID"]);
                                        m11.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m11.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
                                        m11.かな読み = ConvertToStringNullable(row["かな読み"]);
                                        m11.摘要名 = ConvertToString(row["摘要名"]);
                                        m11.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);
                                        context.M11_TEK.ApplyChanges(m11);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 29:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        M12_KIS m12 = new M12_KIS();
                                        m12.規制区分ID = ConvertToInt(row["規制区分ID"]);
                                        m12.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m12.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
                                        m12.規制名 = ConvertToStringNullable(row["規制名"]);
                                        m12.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);
                                        context.M12_KIS.ApplyChanges(m12);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 30:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        int i車輌KEY = ConvertToInt(row["車輌KEY"]);
                                        var ret = (from m05 in context.M05_CAR
                                                   where m05.車輌ID == i車輌KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m05.車輌KEY,
                                                   }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i車輌KEY = keys.Key;
                                        }

                                        M13_MOK m13 = new M13_MOK();
                                        m13.車輌KEY = i車輌KEY;
                                        m13.年月 = ConvertToInt(row["年月"]);
                                        m13.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m13.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
                                        m13.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);
                                        m13.目標燃費 = ConvertToDecimalNullable(row["目標燃費"]);
                                        context.M13_MOK.ApplyChanges(m13);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 31:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        M14_GSYA m14 = new M14_GSYA();
                                        m14.G車種ID = ConvertToInt(row["G車種ID"]);
                                        m14.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m14.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
                                        m14.G車種名 = ConvertToString(row["G車種名"]);
                                        m14.略称名 = ConvertToStringNullable(row["略称名"]);
                                        m14.CO2排出係数１ = ConvertToDecimal(row["CO2排出係数１"]);
                                        m14.CO2排出係数２ = ConvertToDecimal(row["CO2排出係数２"]);
                                        m14.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);
                                        m14.事業用区分 = ConvertToIntNullable(row["事業用区分"]);
                                        m14.ディーゼル区分 = ConvertToIntNullable(row["ディーゼル区分"]);
                                        m14.小型普通区分 = ConvertToIntNullable(row["小型普通区分"]);
                                        m14.低公害区分 = ConvertToIntNullable(row["低公害区分"]);
                                        m14.CO2排出係数１ = ConvertToDecimalNullable(row["CO2排出係数１"]);
                                        m14.CO2排出係数２ = ConvertToDecimalNullable(row["CO2排出係数２"]);
                                        context.M14_GSYA.ApplyChanges(m14);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 32:

                                // 処理なし
                                break;

                            case 33:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        M70_JIS m70 = new M70_JIS();
                                        m70.自社ID = ConvertToInt(row["自社ID"]);
                                        m70.自社名 = ConvertToString(row["自社名"]);
                                        m70.代表者名 = ConvertToString(row["代表者名"]);
                                        m70.郵便番号 = ConvertToString(row["郵便番号"]);
                                        m70.住所１ = ConvertToString(row["住所１"]);
                                        m70.住所２ = ConvertToString(row["住所２"]);
                                        m70.電話番号 = ConvertToString(row["電話番号"]);
                                        m70.ＦＡＸ = ConvertToString(row["ＦＡＸ"]);
                                        m70.振込銀行１ = ConvertToStringNullable(row["振込銀行１"]);
                                        m70.振込銀行２ = ConvertToStringNullable(row["振込銀行２"]);
                                        m70.振込銀行３ = ConvertToStringNullable(row["振込銀行３"]);
                                        m70.法人ナンバー = ConvertToStringNullable(row["法人ナンバー"]);
                                        context.M70_JIS.ApplyChanges(m70);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 34:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        M71_BUM m71 = new M71_BUM();
                                        m71.自社部門ID = ConvertToInt(row["自社部門ID"]);
                                        m71.自社部門名 = ConvertToString(row["自社部門名"]);
                                        m71.法人ナンバー = ConvertToStringNullable(row["法人ナンバー"]);
                                        m71.かな読み = ConvertToStringNullable(row["かな読み"]);
                                        context.M71_BUM.ApplyChanges(m71);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 35:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        M72_TNT m72 = new M72_TNT();
                                        m72.担当者ID = ConvertToInt(row["担当者ID"]);
                                        m72.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m72.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
                                        m72.担当者名 = ConvertToString(row["担当者名"]);
                                        m72.かな読み = ConvertToStringNullable(row["かな読み"]);
                                        m72.パスワード = ConvertToString(row["パスワード"]);
                                        m72.グループ権限ID = ConvertToInt(row["グループ権限ID"]);
                                        m72.自社部門ID = ConvertToInt(row["自社部門ID"]);
                                        context.M72_TNT.ApplyChanges(m72);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 36:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        M73_ZEI m73 = new M73_ZEI();
                                        m73.適用開始日付 = ConvertToDateTime(row["適用開始日付"]);
                                        m73.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m73.更新日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m73.消費税率 = ConvertToIntNullable(row["消費税率"]);
                                        m73.削除日時 = ConvertToDateTimeNullable(row["削除日時"]);
                                        context.M73_ZEI.ApplyChanges(m73);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 37:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        M76_DBU m76 = new M76_DBU();
                                        m76.歩合計算区分ID = ConvertToInt(row["歩合計算区分ID"]);
                                        m76.歩合計算名 = ConvertToString(row["歩合計算名"]);
                                        context.M76_DBU.ApplyChanges(m76);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 38:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        M77_TRH m77 = new M77_TRH();
                                        m77.取引区分ID = ConvertToInt(row["取引区分ID"]);
                                        m77.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m77.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
                                        m77.取引区分名 = ConvertToString(row["取引区分名"]);
                                        m77.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);
                                        context.M77_TRH.ApplyChanges(m77);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 39:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        M78_SYK m78 = new M78_SYK();
                                        m78.出勤区分ID = ConvertToInt(row["出勤区分ID"]);
                                        m78.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m78.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
                                        m78.出勤区分名 = ConvertToString(row["出勤区分名"]);
                                        m78.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);
                                        context.M78_SYK.ApplyChanges(m78);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 40:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        M79_ZKB m79 = new M79_ZKB();
                                        m79.税区分ID = ConvertToInt(row["税区分ID"]);
                                        m79.税区分名 = ConvertToString(row["税区分名"]);
                                        context.M79_ZKB.ApplyChanges(m79);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 41:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        M81_OYK m81 = new M81_OYK();
                                        m81.親子区分ID = ConvertToInt(row["親子区分ID"]);
                                        m81.親子区分名 = ConvertToString(row["親子区分名"]);
                                        context.M81_OYK.ApplyChanges(m81);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 42:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        M82_SEI m82 = new M82_SEI();
                                        m82.請求書区分ID = ConvertToInt(row["請求書区分ID"]);
                                        m82.請求書名 = ConvertToString(row["請求書名"]);
                                        context.M82_SEI.ApplyChanges(m82);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 43:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        M83_UKE m83 = new M83_UKE();
                                        m83.運賃計算区分ID = ConvertToInt(row["運賃計算区分ID"]);
                                        m83.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m83.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
                                        m83.運賃計算区分 = ConvertToString(row["運賃計算区分"]);
                                        m83.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);
                                        context.M83_UKE.ApplyChanges(m83);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 44:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        M84_RIK m84 = new M84_RIK();
                                        m84.運輸局ID = ConvertToInt(row["運輸局ID"]);
                                        m84.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m84.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
                                        m84.運輸局名 = ConvertToString(row["運輸局名"]);
                                        m84.法人ナンバー = ConvertToStringNullable(row["法人ナンバー"]);
                                        m84.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);
                                        context.M84_RIK.ApplyChanges(m84);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 45:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        M87_CNTL m87 = new M87_CNTL();
                                        m87.管理ID = ConvertToInt(row["管理ID"]);
                                        m87.得意先管理処理年月 = ConvertToIntNullable(row["得意先管理処理年月"]);
                                        m87.支払先管理処理年月 = ConvertToIntNullable(row["支払先管理処理年月"]);
                                        m87.車輌管理処理年月 = ConvertToIntNullable(row["車輌管理処理年月"]);
                                        m87.運転者管理処理年月 = ConvertToIntNullable(row["運転者管理処理年月"]);
                                        m87.更新年月 = ConvertToIntNullable(row["更新年月"]);
                                        m87.決算月 = ConvertToIntNullable(row["決算月"]);
                                        m87.得意先自社締日 = ConvertToIntNullable(row["得意先自社締日"]);
                                        m87.支払先自社締日 = ConvertToIntNullable(row["支払先自社締日"]);
                                        m87.運転者自社締日 = ConvertToIntNullable(row["運転者自社締日"]);
                                        m87.車輌自社締日 = ConvertToIntNullable(row["車輌自社締日"]); ;
                                        m87.自社支払日 = ConvertToIntNullable(row["自社支払日"]);
                                        m87.自社サイト = ConvertToIntNullable(row["自社サイト"]);
                                        m87.未定区分 = ConvertToIntNullable(row["未定区分"]);
                                        m87.部門管理区分 = ConvertToIntNullable(row["部門管理区分"]);
                                        m87.割増料金名１ = ConvertToStringNullable(row["割増料金名１"]);
                                        m87.割増料金名２ = ConvertToStringNullable(row["割増料金名２"]);
                                        m87.得意先ID区分 = ConvertToIntNullable(row["得意先ID区分"]);
                                        m87.支払先ID区分 = ConvertToIntNullable(row["支払先ID区分"]);
                                        m87.乗務員ID区分 = ConvertToIntNullable(row["乗務員ID区分"]);
                                        m87.車輌ID区分 = ConvertToIntNullable(row["車輌ID区分"]);
                                        m87.車種ID区分 = ConvertToIntNullable(row["車種ID区分"]);
                                        m87.発着地ID区分 = ConvertToIntNullable(row["発着地ID区分"]);
                                        m87.品名ID区分 = ConvertToIntNullable(row["品名ID区分"]);
                                        m87.摘要ID区分 = ConvertToIntNullable(row["摘要ID区分"]);
                                        m87.期首年月 = ConvertToIntNullable(row["期首年月"]);
                                        m87.売上消費税端数区分 = ConvertToIntNullable(row["売上消費税端数区分"]);
                                        m87.支払消費税端数区分 = ConvertToIntNullable(row["支払消費税端数区分"]);
                                        m87.金額計算端数区分 = ConvertToIntNullable(row["金額計算端数区分"]);
                                        m87.出力プリンター設定 = ConvertToIntNullable(row["出力プリンター設定"]);
                                        m87.自動学習区分 = ConvertToIntNullable(row["自動学習区分"]);
                                        m87.月次集計区分 = ConvertToIntNullable(row["月次集計区分"]);
                                        m87.距離転送区分 = ConvertToIntNullable(row["距離転送区分"]);
                                        m87.番号通知区分 = ConvertToIntNullable(row["番号通知区分"]);
                                        m87.通行料転送区分 = ConvertToIntNullable(row["通行料転送区分"]);
                                        m87.路線計算区分 = ConvertToIntNullable(row["路線計算区分"]);
                                        m87.Ｇ期首月日 = ConvertToIntNullable(row["Ｇ期首月日"]);
                                        m87.Ｇ期末月日 = ConvertToIntNullable(row["Ｇ期末月日"]);
                                        m87.請求書区分 = ConvertToIntNullable(row["請求書区分"]);
                                        context.M87_CNTL.ApplyChanges(m87);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 46:

                                // 取込対象外のマスタ
                                //foreach (DataRow row in dt.Rows)
                                //{
                                //    // エラーでない行を登録対象とする。
                                //    if (!row.HasErrors)
                                //    {
                                //        M88_SEQ m88 = new M88_SEQ();
                                //        m88.明細番号ID = ConvertToInt(row["明細番号ID"]);
                                //        m88.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                //        m88.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
                                //        m88.現在明細番号 = ConvertToInt(row["現在明細番号"]);
                                //        m88.最大明細番号 = ConvertToInt(row["最大明細番号"]);
                                //        context.M88_SEQ.ApplyChanges(m88);
                                //    }
                                //}
                                //        context.SaveChanges();
                                //transaction.Commit();
                                break;

                            case 47:

                                // 取込対象外のマスタ
                                //foreach (DataRow row in dt.Rows)
                                //{
                                //    // エラーでない行を登録対象とする。
                                //    if (!row.HasErrors)
                                //    {
                                //        M90_GRID m90 = new M90_GRID();
                                //        m90.担当者ID = ConvertToInt(row["担当者ID"]);
                                //        m90.画面ID = ConvertToString(row["画面ID"]);
                                //        m90.GRIDID = ConvertToString(row["GRIDID"]);
                                //        m90.列名 = ConvertToString(row["列名"]);
                                //        m90.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);
                                //        m90.表示順 = ConvertToIntNullable(row["表示順"]);
                                //        m90.表示フラグ = ConvertToIntNullable(row["表示フラグ"]);
                                //        context.M90_GRID.ApplyChanges(m90);
                                //    }
                                //}
                                //        context.SaveChanges();
                                //transaction.Commit();
                                break;

                            case 48:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        int i支払先KEY = ConvertToInt(row["支払先KEY"]);
                                        var ret = (from m01 in context.M01_TOK
                                                   where m01.得意先ID == i支払先KEY
                                                   select new MST90020_KEY
                                                   {
                                                       Key = m01.得意先KEY,
                                                   }).AsQueryable();

                                        foreach (var keys in ret)
                                        {
                                            i支払先KEY = keys.Key;
                                        }

                                        M91_OTAN m91 = new M91_OTAN();
                                        m91.適用開始年月日 = ConvertToDateTime(row["適用開始年月日"]);
                                        m91.支払先KEY = i支払先KEY;
                                        m91.登録日時 = ConvertToDateTimeNullable(row["登録日時"]);
                                        m91.更新日時 = ConvertToDateTimeNullable(row["更新日時"]);
                                        m91.削除日付 = ConvertToDateTimeNullable(row["削除日付"]);
                                        m91.燃料単価 = ConvertToDecimalNullable(row["燃料単価"]);
                                        context.M91_OTAN.ApplyChanges(m91);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;

                            case 49:

                                foreach (DataRow row in dt.Rows)
                                {
                                    // エラーでない行を登録対象とする。
                                    if (!row.HasErrors)
                                    {
                                        M92_KZEI m92 = new M92_KZEI();
                                        m92.適用開始年月日 = ConvertToDateTime(row["適用開始年月日"]);
                                        m92.軽油引取税率 = ConvertToDecimalNullable(row["軽油引取税率"]);
                                        context.M92_KZEI.ApplyChanges(m92);
                                    }
                                }
                                context.SaveChanges();
                                transaction.Commit();
                                break;
                        }
                        return;

                        #endregion
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
