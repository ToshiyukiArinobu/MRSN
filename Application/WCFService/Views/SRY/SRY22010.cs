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


    public class SRY22010
    {
        public class SERCHE_SRY22010
        {
            [DataMember]
            public int 車輌コード { get; set; }
            [DataMember]
            public int? 自社部門ID { get; set; }
            [DataMember]
            public string 車番 { get; set; }
            [DataMember]
            public string 車種 { get; set; }
            [DataMember]
            public string チェック { get; set; }
            [DataMember]
            public int 点検日 { get; set; }
            [DataMember]
            public string 廃車 { get; set; }
            [DataMember]
            public int? 年月 { get; set; }
        }

        public class SERCHE_SRY22020
        {
            [DataMember]
            public int? チェック日 { get; set; }
            [DataMember]
            public int? エアコン区分 { get; set; }
            [DataMember]
            public int? 異音区分 { get; set; }
            [DataMember]
            public int? 排気区分 { get; set; }
            [DataMember]
            public int? 燃費区分 { get; set; }
            [DataMember]
            public int? その他区分 { get; set; }
            [DataMember]
            public string エアコン備考 { get; set; }
            [DataMember]
            public string 異音備考 { get; set; }
            [DataMember]
            public string 排気備考 { get; set; }
            [DataMember]
            public string 燃費備考 { get; set; }
            [DataMember]
            public string その他備考 { get; set; }
            [DataMember]
            public int? 乗務員ID { get; set; }
            [DataMember]
            public string 乗務員名 { get; set; }
            [DataMember]
            public DateTime? 指示日付 { get; set; }
            [DataMember]
            public string 整備指示 { get; set; }
            [DataMember]
            public DateTime? 部品日付 { get; set; }
            [DataMember]
            public string 整備部品 { get; set; }
            [DataMember]
            public DateTime? 結果日付 { get; set; }
            [DataMember]
            public string 整備結果 { get; set; }
        }

        public class SERCHE_SRY22020_SRY
        {
            [DataMember]
            public int 車輌KEY { get; set; }
            [DataMember]
            public int 乗務員KEY { get; set; }
        }

        public class SERCHE_SRY22010_OUTPUT
        {
            [DataMember] public string 年度 { get; set; }
            [DataMember] public int 車輌KEY { get; set; }
            [DataMember] public string 車輌番号 { get; set; }
            [DataMember] public string 排ガス規制形式 { get; set; }
            [DataMember] public decimal 基準燃費 { get; set; }
            [DataMember] public decimal 黒煙規制値 { get; set; }
            [DataMember] public int? 自社部門ID { get; set; }
            [DataMember] public string 日付1 { get; set; }
            [DataMember] public string 日付2 { get; set; }
            [DataMember] public string 日付3 { get; set; }
            [DataMember] public string 日付4 { get; set; }
            [DataMember] public string 日付5 { get; set; }
            [DataMember] public string 日付6 { get; set; }
            [DataMember] public string 日付7 { get; set; }
            [DataMember] public string 日付8 { get; set; }
            [DataMember] public string 日付9 { get; set; }
            [DataMember] public string 日付10 { get; set; }
            [DataMember] public string 日付11 { get; set; }
            [DataMember] public string 日付12 { get; set; }
            [DataMember] public string エアコン1 { get; set; }
            [DataMember] public string エアコン2 { get; set; }
            [DataMember] public string エアコン3 { get; set; }
            [DataMember] public string エアコン4 { get; set; }
            [DataMember] public string エアコン5 { get; set; }
            [DataMember] public string エアコン6 { get; set; }
            [DataMember] public string エアコン7 { get; set; }
            [DataMember] public string エアコン8 { get; set; }
            [DataMember] public string エアコン9 { get; set; }
            [DataMember] public string エアコン10 { get; set; }
            [DataMember] public string エアコン11 { get; set; }
            [DataMember] public string エアコン12 { get; set; }
            [DataMember] public string 異音1 { get; set; }
            [DataMember] public string 異音2 { get; set; }
            [DataMember] public string 異音3 { get; set; }
            [DataMember] public string 異音4 { get; set; }
            [DataMember] public string 異音5 { get; set; }
            [DataMember] public string 異音6 { get; set; }
            [DataMember] public string 異音7 { get; set; }
            [DataMember] public string 異音8 { get; set; }
            [DataMember] public string 異音9 { get; set; }
            [DataMember] public string 異音10 { get; set; }
            [DataMember] public string 異音11 { get; set; }
            [DataMember] public string 異音12 { get; set; }
            [DataMember] public string 排気ガス1 { get; set; }
            [DataMember] public string 排気ガス2 { get; set; }
            [DataMember] public string 排気ガス3 { get; set; }
            [DataMember] public string 排気ガス4 { get; set; }
            [DataMember] public string 排気ガス5 { get; set; }
            [DataMember] public string 排気ガス6 { get; set; }
            [DataMember] public string 排気ガス7 { get; set; }
            [DataMember] public string 排気ガス8 { get; set; }
            [DataMember] public string 排気ガス9 { get; set; }
            [DataMember] public string 排気ガス10 { get; set; }
            [DataMember] public string 排気ガス11 { get; set; }
            [DataMember] public string 排気ガス12 { get; set; }
            [DataMember] public string 燃費1 { get; set; }
            [DataMember] public string 燃費2 { get; set; }
            [DataMember] public string 燃費3 { get; set; }
            [DataMember] public string 燃費4 { get; set; }
            [DataMember] public string 燃費5 { get; set; }
            [DataMember] public string 燃費6 { get; set; }
            [DataMember] public string 燃費7 { get; set; }
            [DataMember] public string 燃費8 { get; set; }
            [DataMember] public string 燃費9 { get; set; }
            [DataMember] public string 燃費10 { get; set; }
            [DataMember] public string 燃費11 { get; set; }
            [DataMember] public string 燃費12 { get; set; }
            [DataMember] public string その他1 { get; set; }
            [DataMember] public string その他2 { get; set; }
            [DataMember] public string その他3 { get; set; }
            [DataMember] public string その他4 { get; set; }
            [DataMember] public string その他5 { get; set; }
            [DataMember] public string その他6 { get; set; }
            [DataMember] public string その他7 { get; set; }
            [DataMember] public string その他8 { get; set; }
            [DataMember] public string その他9 { get; set; }
            [DataMember] public string その他10 { get; set; }
            [DataMember] public string その他11 { get; set; }
            [DataMember] public string その他12 { get; set; }
            [DataMember] public int 月1 { get; set; }
            [DataMember] public int 月2 { get; set; }
            [DataMember] public int 月3 { get; set; }
            [DataMember] public int 月4 { get; set; }
            [DataMember] public int 月5 { get; set; }
            [DataMember] public int 月6 { get; set; }
            [DataMember] public int 月7 { get; set; }
            [DataMember] public int 月8 { get; set; }
            [DataMember] public int 月9 { get; set; }
            [DataMember] public int 月10 { get; set; }
            [DataMember] public int 月11 { get; set; }
            [DataMember] public int 月12 { get; set; }
        }

        public class SERCHE_M87_CNTL_OUTPUT
        {
            [DataMember] public int? G期首月日 { get; set; }
            [DataMember] public int? G期末月日 { get; set; }
        }


        /// <summary>
        /// LOAD時にセットするデータ
        /// </summary>
        /// <returns></returns>
        public List<SERCHE_SRY22010> LOAD_GetData(int? p自社部門ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                DateTime Today = DateTime.Today;
                DateTime Wk;
                if (DateTime.TryParse(Today.Year.ToString() + "/" + Today.Month.ToString() + "/01", out Wk))
                {
                    Today = Wk;
                }
                var query = (from m05 in context.M05_CAR.Where(c => c.削除日付 == null)
                             from m06 in context.M06_SYA.Where(c => c.車種ID == m05.車種ID)
                             from m05t in context.M05_TEN.Where(c => c.車輌KEY == m05.車輌KEY).DefaultIfEmpty()
                             where m05.削除日付 == null && m06.削除日付 == null
                             select new SERCHE_SRY22010
                             {
                                 車輌コード = m05.車輌ID,
                                 自社部門ID = m05.自社部門ID,
                                 車番 = m05.車輌番号,
                                 車種 = m06.車種名,
                                 廃車 = m05.廃車区分 == 0 ? string.Empty : m05.廃車日 <= Today ? "廃車" : string.Empty,

                             }).Distinct().AsQueryable();
                int init;
                if (int.TryParse(p自社部門ID.ToString(), out init))
                {
                    query = query.Where(c => c.自社部門ID == p自社部門ID);
                }

                return query.ToList();
            }
        }

        /// <summary>
        /// 印刷時の月範囲
        /// </summary>
        /// <returns></returns>
        public List<SERCHE_M87_CNTL_OUTPUT> OUTPUT_SRY22010_M87_CNTL()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m87 in context.M87_CNTL
                             select new SERCHE_M87_CNTL_OUTPUT
                             {
                                 G期首月日 = m87.Ｇ期首月日 == 0 ? 4 : m87.Ｇ期首月日,
                                 G期末月日 = m87.Ｇ期末月日 == 0 ? 3 : m87.Ｇ期末月日,
                             }).AsQueryable();
              
                return query.ToList();
            }
        }

        #region 作成年月でデータ取得

        /// <summary>
        /// 作成年月でデータ取得
        /// </summary>
        /// <param name="s作成年月"></param>
        /// <returns></returns>
        public List<SERCHE_SRY22010> SEARCH_GetData(int? p自社部門ID, string s作成年月)
        {
            int i年月 = Convert.ToInt32(s作成年月.Substring(0, 4) + s作成年月.Substring(5, 2));
            DateTime dToday = Convert.ToDateTime(s作成年月 + "/01"); 
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

				var query = (from m05 in context.M05_CAR.Where(c => c.削除日付 == null)
                             from m06 in context.M06_SYA.Where(c => c.車種ID == m05.車種ID)
                             from m05t in context.M05_TEN.Where(c => c.車輌KEY == m05.車輌KEY && c.年月 == i年月).DefaultIfEmpty()
                             where m05.削除日付 == null && m06.削除日付 == null
                             select new SERCHE_SRY22010
                             {
                                 車輌コード = m05.車輌ID,
                                 自社部門ID = m05.自社部門ID,
                                 車番 = m05.車輌番号,
                                 車種 = m06.車種名,
                                 チェック = m05t.チェック,
                                 点検日 = m05t.点検日 == null ? 0 : m05t.点検日,
                                 廃車 = m05.廃車区分 == 0 ? string.Empty : m05.廃車日 <= dToday ? "廃車" : string.Empty,

                             }).Distinct().AsQueryable();
                int init;
                if (int.TryParse(p自社部門ID.ToString(), out init))
                {
                    query = query.Where(c => c.自社部門ID == p自社部門ID);
                }

                return query.ToList();
            }
        }

        /// <summary>
        /// LOAD時にセットするデータ
        /// </summary>
        /// <returns></returns>
        public List<SERCHE_SRY22020> LOAD_GetData2(int p車輌ID, int p作成年月)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m05 in context.M05_CAR.Where(c => c.車輌ID == p車輌ID && c.削除日付 == null)
                             from m05t in context.M05_TEN.Where(c => c.車輌KEY == m05.車輌KEY)
                             from m04 in context.M04_DRV.Where(c => c.乗務員KEY == m05t.乗務員KEY && c.削除日付 == null).DefaultIfEmpty()
                             where m05.削除日付 == null && m05t.年月 == p作成年月 && m05.削除日付 == null 
                             select new SERCHE_SRY22020
                             {
                                 チェック日 = m05t.点検日,
                                 エアコン区分 = m05t.エアコン区分,
                                 異音区分 = m05t.異音区分,
                                 排気区分 = m05t.排気区分,
                                 燃費区分 = m05t.燃費区分,
                                 その他区分 = m05t.その他区分,
                                 エアコン備考 = m05t.エアコン備考,
                                 異音備考 = m05t.異音備考,
                                 排気備考 = m05t.排気備考,
                                 燃費備考 = m05t.燃費備考,
                                 その他備考 = m05t.その他備考,
                                 乗務員ID = m04.乗務員ID,
                                 乗務員名 = m04.乗務員名,
                                 指示日付 = m05t.指示日付,
                                 整備指示 = m05t.整備指示,
                                 部品日付 = m05t.部品日付,
                                 整備部品 = m05t.整備部品,
                                 結果日付 = m05t.結果日付,
                                 整備結果 = m05t.整備結果,
                             }).Distinct().AsQueryable();
                return query.ToList();
            }
        }

        #endregion

        #region データ登録

        // <summary>
        // データ登録
        // </summary>
        // <returns></returns>
        public void INSERT_GetData(int p車輌ID, int p作成年月, int pチェック日, int? pエアコン区分, string pエアコン備考, int? p異音区分, string p異音備考, int? p排気区分, string p排気備考, int? p燃費区分, string p燃費備考,
                                                    int? pその他区分, string pその他備考, int? p乗務員ID, string p乗務員名, string p整備指示, DateTime? p指示日付, string p整備部品, DateTime? p部品日付, string p整備結果, DateTime? p結果日付)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //更新行を特定
                var ret = from m05 in context.M05_CAR.Where(c => c.車輌ID == p車輌ID)
                          from m05t in context.M05_TEN.Where(c => c.車輌KEY == m05.車輌KEY)
                          where m05.削除日付 == null && m05t.年月 == p作成年月
                          select m05t;
                var Query = ret.FirstOrDefault();

                var sha = (from m05 in context.M05_CAR.Where(c => c.車輌ID == p車輌ID)
                           from m04 in context.M04_DRV.Where(c => c.乗務員ID == p乗務員ID)
                           select new SERCHE_SRY22020_SRY
                           {
                               車輌KEY = m05.車輌KEY,
                               乗務員KEY = m04.乗務員KEY ,
                           }).AsQueryable();
                var Sharyo = sha.FirstOrDefault();

                string チェック;
                if (pエアコン区分 == 0 && p異音区分 == 0 && p排気区分 == 0 && p燃費区分 == 0 && pその他区分 == 0)
                {
                    チェック = "○";
                }
                else
                {
                    チェック = "×";
                }

                if (Query == null)
                {
                    M05_TEN m05ten = new M05_TEN();
                    m05ten.車輌KEY = Sharyo.車輌KEY;
                    m05ten.年月 = p作成年月;
                    m05ten.登録日時 = DateTime.Now;
                    m05ten.更新日時 = DateTime.Now;
                    m05ten.点検日 = pチェック日;
                    m05ten.チェック = チェック;
                    m05ten.エアコン区分 = pエアコン区分;
                    m05ten.エアコン備考 = pエアコン備考;
                    m05ten.異音区分 = p異音区分;
                    m05ten.異音備考 = p異音備考;
                    m05ten.排気区分 = p排気区分;
                    m05ten.排気備考 = p排気備考;
                    m05ten.燃費区分 = p燃費区分;
                    m05ten.燃費備考 = p燃費備考;
                    m05ten.その他区分 = pその他区分;
                    m05ten.その他備考 = pその他備考;
                    m05ten.乗務員KEY = Sharyo.乗務員KEY;
                    m05ten.乗務員名 = p乗務員名;
                    m05ten.指示日付 = p指示日付;
                    m05ten.整備指示 = p整備指示;
                    m05ten.部品日付 = p部品日付;
                    m05ten.整備部品 = p整備部品;
                    m05ten.結果日付 = p結果日付;
                    m05ten.整備結果 = p整備結果;
                    context.M05_TEN.ApplyChanges(m05ten);
                }
                else
                {

                    Query.車輌KEY = Sharyo.車輌KEY;
                    Query.年月 = p作成年月;
                    Query.登録日時 = DateTime.Now;
                    Query.更新日時 = DateTime.Now;
                    Query.点検日 = pチェック日;
                    Query.チェック = チェック;
                    Query.エアコン区分 = pエアコン区分;
                    Query.エアコン備考 = pエアコン備考;
                    Query.異音区分 = p異音区分;
                    Query.異音備考 = p異音備考;
                    Query.排気区分 = p排気区分;
                    Query.排気備考 = p排気備考;
                    Query.燃費区分 = p燃費区分;
                    Query.燃費備考 = p燃費備考;
                    Query.その他区分 = pその他区分;
                    Query.その他備考 = pその他備考;
                    Query.乗務員KEY = Sharyo.乗務員KEY;
                    Query.乗務員名 = p乗務員名;
                    Query.指示日付 = p指示日付;
                    Query.整備指示 = p整備指示;
                    Query.部品日付 = p部品日付;
                    Query.整備部品 = p整備部品;
                    Query.結果日付 = p結果日付;
                    Query.整備結果 = p整備結果;
                    Query.AcceptChanges();
                }
                context.SaveChanges();

                return;
            }
        }

        #endregion

        #region データ削除

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="p車輌ID"></param>
        /// <param name="p作成年月"></param>
        /// <returns></returns>
        public void DELETE_GetData(int p車輌ID, int p作成年月)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                using (var tran = new TransactionScope())
                {
                    var query = (from m05 in context.M05_CAR.Where(c => c.車輌ID == p車輌ID && c.削除日付 == null)
                                 from m05t in context.M05_TEN.Where(c => c.車輌KEY == m05.車輌KEY)
                                 select m05t);

                    if (query.Count() > 0)
                    {
                        foreach (var rec in query)
                        {
                            context.DeleteObject(rec);
                            context.SaveChanges();
                            tran.Complete();
                        }
                    }
                }

                return;
            }
        }

        #endregion


        public List<SERCHE_SRY22010_OUTPUT> SERCHE_OUTPUT(int? p自社部門ID　, string p作成年 , string p開始月 , string p終了月)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                DateTime Today = DateTime.Today;
                DateTime Wk;
                if (DateTime.TryParse(p作成年.ToString() + "/" + p開始月.ToString() + "/01", out Wk))
                {
                    Today = Wk;
                }
				var query = (from m05 in context.M05_CAR.Where(c => c.削除日付 == null)
                             from m06 in context.M06_SYA.Where(c => c.車種ID == m05.車種ID)
                             from m05t in context.M05_TEN.Where(c => c.車輌KEY == m05.車輌KEY).DefaultIfEmpty()
                             where m05.削除日付 == null && m06.削除日付 == null
                             select new SERCHE_SRY22010_OUTPUT
                             {
                                 年度 = p作成年 + "年度",
                                 車輌番号 = m05.車輌番号,
                                 車輌KEY = m05.車輌KEY,
                                 自社部門ID = m05.自社部門ID,
                                 排ガス規制形式 = m05.型式,
                                 基準燃費 = m05.基準燃費,
                                 黒煙規制値 = m05.黒煙規制値,
                                 日付1 = null,
                                 日付2 = null,
                                 日付3 = null,
                                 日付4 = null,
                                 日付5 = null,
                                 日付6 = null,
                                 日付7 = null,
                                 日付8 = null,
                                 日付9 = null,
                                 日付10 = null,
                                 日付11 = null,
                                 日付12 = null, 
                                 エアコン1 = null,
                                 エアコン2 = null,
                                 エアコン3 = null,
                                 エアコン4 = null,
                                 エアコン5 = null,
                                 エアコン6 = null,
                                 エアコン7 = null,
                                 エアコン8 = null,
                                 エアコン9 = null,
                                 エアコン10 = null,
                                 エアコン11 = null,
                                 エアコン12 = null,
                                 異音1 = null,
                                 異音2 = null,
                                 異音3 = null,
                                 異音4 = null,
                                 異音5 = null,
                                 異音6 = null,
                                 異音7 = null,
                                 異音8 = null,
                                 異音9 = null,
                                 異音10 = null,
                                 異音11 = null,
                                 異音12 = null,           
                                 排気ガス1 = null,
                                 排気ガス2 = null,
                                 排気ガス3 = null,
                                 排気ガス4 = null,
                                 排気ガス5 = null,
                                 排気ガス6 = null,
                                 排気ガス7 = null,
                                 排気ガス8 = null,
                                 排気ガス9 = null,
                                 排気ガス10 = null,
                                 排気ガス11 = null,
                                 排気ガス12 = null,                          
                                 燃費1 = null,
                                 燃費2 = null,
                                 燃費3 = null,
                                 燃費4 = null,
                                 燃費5 = null,
                                 燃費6 = null,
                                 燃費7 = null,
                                 燃費8 = null,
                                 燃費9 = null,
                                 燃費10 = null,
                                 燃費11 = null,
                                 燃費12 = null,
                                 その他1 = null,
                                 その他2 = null,
                                 その他3 = null,
                                 その他4 = null,
                                 その他5 = null,
                                 その他6 = null,
                                 その他7 = null,
                                 その他8 = null,
                                 その他9 = null,
                                 その他10 = null,
                                 その他11 = null,
                                 その他12 = null,
                             }).Distinct().AsQueryable();
                int init;
                if (int.TryParse(p自社部門ID.ToString(), out init))
                {
                    query = query.Where(c => c.自社部門ID == p自社部門ID);
                }

                int 集計年月;
                int RowCount = 0;
                List<SERCHE_SRY22010_OUTPUT> queryList = query.ToList();

                int[] Year = new int[12];
                int[] Month = new int[12];
                int[] Sakuseinengetu = new int[12];
                int 月, 年;
                月 = AppCommon.IntParse(p開始月);
                年 = AppCommon.IntParse(p作成年);
                for (int i = 0; i <= 11; i++)
                {
                    if (月 == 13)
                    {
                        年 = AppCommon.IntParse(p作成年) + 1;
                        月 = 1;
                    }
                    if (i == 0)
                    {
                        Year[i] = 年;
                        Month[i] = 月;
                        Sakuseinengetu[i] = Month[i].ToString().Length == 1 ? AppCommon.IntParse(Year[i].ToString() + "0" + Month[i].ToString()) : AppCommon.IntParse(Year[i].ToString() + Month[i].ToString());
                    }
                    else
                    {
                        Year[i] = 年;
                        Month[i] = 月;
                        Sakuseinengetu[i] = Month[i].ToString().Length == 1 ? AppCommon.IntParse(Year[i].ToString() + "0" + Month[i].ToString()) : AppCommon.IntParse(Year[i].ToString() + Month[i].ToString());
                    }
                    月++;
                }
                


                if (queryList.Count != 0)
                {
                    foreach (var Rows in queryList)
                    {
                        queryList[RowCount].月1 = Month[0];
                        queryList[RowCount].月2 = Month[1];
                        queryList[RowCount].月3 = Month[2];
                        queryList[RowCount].月4 = Month[3];
                        queryList[RowCount].月5 = Month[4];
                        queryList[RowCount].月6 = Month[5];
                        queryList[RowCount].月7 = Month[6];
                        queryList[RowCount].月8 = Month[7];
                        queryList[RowCount].月9 = Month[8];
                        queryList[RowCount].月10 = Month[9];
                        queryList[RowCount].月11 = Month[10];
                        queryList[RowCount].月12 = Month[11];

                        RowCount++;
                    }
                    int 作成年月;
                    RowCount = 0;
                    foreach (var Rows in queryList)
                    {
                        for (int i = 1; i < 12; i++)
                        {
                            作成年月 = Sakuseinengetu[i - 1];
                            var ret = (from m05t in context.M05_TEN.Where(c => c.年月 == 作成年月 && c.車輌KEY == Rows.車輌KEY) select m05t).AsQueryable();
                            var retList = ret.FirstOrDefault();
                            if (ret.Count() == 1)
                            {
                                switch (i)
                                {
                                    case 1:
                                        queryList[RowCount].日付1 = retList.点検日.ToString() == string.Empty ? "　" : retList.点検日.ToString();
                                        queryList[RowCount].エアコン1 = retList.エアコン区分 == 0 ? "○" : "×";
                                        queryList[RowCount].異音1 = retList.異音区分 == 0 ? "○" : "×";
                                        queryList[RowCount].排気ガス1 = retList.排気区分 == 0 ? "○" : "×";
                                        queryList[RowCount].燃費1 = retList.燃費区分 == 0 ? "○" : "×";
                                        queryList[RowCount].その他1 = retList.その他区分 == 0 ? "○" : "×";
                                        break;

                                    case 2:
                                        queryList[RowCount].日付2 = retList.点検日.ToString() == string.Empty ? "　" : retList.点検日.ToString();
                                        queryList[RowCount].エアコン2 = retList.エアコン区分 == 0 ? "○" : "×";
                                        queryList[RowCount].異音2 = retList.異音区分 == 0 ? "○" : "×";
                                        queryList[RowCount].排気ガス2 = retList.排気区分 == 0 ? "○" : "×";
                                        queryList[RowCount].燃費2 = retList.燃費区分 == 0 ? "○" : "×";
                                        queryList[RowCount].その他2 = retList.その他区分 == 0 ? "○" : "×";
                                        break;

                                    case 3:
                                        queryList[RowCount].日付3 = retList.点検日.ToString() == string.Empty ? "　" : retList.点検日.ToString();
                                        queryList[RowCount].エアコン3 = retList.エアコン区分 == 0 ? "○" : "×";
                                        queryList[RowCount].異音3 = retList.異音区分 == 0 ? "○" : "×";
                                        queryList[RowCount].排気ガス3 = retList.排気区分 == 0 ? "○" : "×";
                                        queryList[RowCount].燃費3 = retList.燃費区分 == 0 ? "○" : "×";
                                        queryList[RowCount].その他3 = retList.その他区分 == 0 ? "○" : "×";
                                        break;

                                    case 4:
                                        queryList[RowCount].日付4 = retList.点検日.ToString() == string.Empty ? "　" : retList.点検日.ToString();
                                        queryList[RowCount].エアコン4 = retList.エアコン区分 == 0 ? "○" : "×";
                                        queryList[RowCount].異音4 = retList.異音区分 == 0 ? "○" : "×";
                                        queryList[RowCount].排気ガス4 = retList.排気区分 == 0 ? "○" : "×";
                                        queryList[RowCount].燃費4 = retList.燃費区分 == 0 ? "○" : "×";
                                        queryList[RowCount].その他4 = retList.その他区分 == 0 ? "○" : "×";
                                        break;

                                    case 5:
                                        queryList[RowCount].日付5 = retList.点検日.ToString() == string.Empty ? "　" : retList.点検日.ToString();
                                        queryList[RowCount].エアコン5 = retList.エアコン区分 == 0 ? "○" : "×";
                                        queryList[RowCount].異音5 = retList.異音区分 == 0 ? "○" : "×";
                                        queryList[RowCount].排気ガス5 = retList.排気区分 == 0 ? "○" : "×";
                                        queryList[RowCount].燃費5 = retList.燃費区分 == 0 ? "○" : "×";
                                        queryList[RowCount].その他5 = retList.その他区分 == 0 ? "○" : "×";
                                        break;

                                    case 6:
                                        queryList[RowCount].日付6 = retList.点検日.ToString() == string.Empty ? "　" : retList.点検日.ToString();
                                        queryList[RowCount].エアコン6 = retList.エアコン区分 == 0 ? "○" : "×";
                                        queryList[RowCount].異音6 = retList.異音区分 == 0 ? "○" : "×";
                                        queryList[RowCount].排気ガス6 = retList.排気区分 == 0 ? "○" : "×";
                                        queryList[RowCount].燃費6 = retList.燃費区分 == 0 ? "○" : "×";
                                        queryList[RowCount].その他6 = retList.その他区分 == 0 ? "○" : "×";
                                        break;

                                    case 7:
                                        queryList[RowCount].日付7 = retList.点検日.ToString() == string.Empty ? "　" : retList.点検日.ToString();
                                        queryList[RowCount].エアコン7 = retList.エアコン区分 == 0 ? "○" : "×";
                                        queryList[RowCount].異音7 = retList.異音区分 == 0 ? "○" : "×";
                                        queryList[RowCount].排気ガス7 = retList.排気区分 == 0 ? "○" : "×";
                                        queryList[RowCount].燃費7 = retList.燃費区分 == 0 ? "○" : "×";
                                        queryList[RowCount].その他7 = retList.その他区分 == 0 ? "○" : "×";
                                        break;

                                    case 8:
                                        queryList[RowCount].日付8 = retList.点検日.ToString() == string.Empty ? "　" : retList.点検日.ToString();
                                        queryList[RowCount].エアコン8 = retList.エアコン区分 == 0 ? "○" : "×";
                                        queryList[RowCount].異音8 = retList.異音区分 == 0 ? "○" : "×";
                                        queryList[RowCount].排気ガス8 = retList.排気区分 == 0 ? "○" : "×";
                                        queryList[RowCount].燃費8 = retList.燃費区分 == 0 ? "○" : "×";
                                        queryList[RowCount].その他8 = retList.その他区分 == 0 ? "○" : "×";
                                        break;

                                    case 9:
                                        queryList[RowCount].日付9 = retList.点検日.ToString() == string.Empty ? "　" : retList.点検日.ToString();
                                        queryList[RowCount].エアコン9 = retList.エアコン区分 == 0 ? "○" : "×";
                                        queryList[RowCount].異音9 = retList.異音区分 == 0 ? "○" : "×";
                                        queryList[RowCount].排気ガス9 = retList.排気区分 == 0 ? "○" : "×";
                                        queryList[RowCount].燃費9 = retList.燃費区分 == 0 ? "○" : "×";
                                        queryList[RowCount].その他9 = retList.その他区分 == 0 ? "○" : "×";
                                        break;

                                    case 10:
                                        queryList[RowCount].日付10 = retList.点検日.ToString() == string.Empty ? "　" : retList.点検日.ToString();
                                        queryList[RowCount].エアコン10 = retList.エアコン区分 == 0 ? "○" : "×";
                                        queryList[RowCount].異音10 = retList.異音区分 == 0 ? "○" : "×";
                                        queryList[RowCount].排気ガス10 = retList.排気区分 == 0 ? "○" : "×";
                                        queryList[RowCount].燃費10 = retList.燃費区分 == 0 ? "○" : "×";
                                        queryList[RowCount].その他10 = retList.その他区分 == 0 ? "○" : "×";
                                        break;

                                    case 11:
                                        queryList[RowCount].日付11 = retList.点検日.ToString() == string.Empty ? "　" : retList.点検日.ToString();
                                        queryList[RowCount].エアコン11 = retList.エアコン区分 == 0 ? "○" : "×";
                                        queryList[RowCount].異音11 = retList.異音区分 == 0 ? "○" : "×";
                                        queryList[RowCount].排気ガス11 = retList.排気区分 == 0 ? "○" : "×";
                                        queryList[RowCount].燃費11 = retList.燃費区分 == 0 ? "○" : "×";
                                        queryList[RowCount].その他11 = retList.その他区分 == 0 ? "○" : "×";
                                        break;

                                    case 12:
                                        queryList[RowCount].日付12 = retList.点検日.ToString() == string.Empty ? "　" : retList.点検日.ToString();
                                        queryList[RowCount].エアコン12 = retList.エアコン区分 == 0 ? "○" : "×";
                                        queryList[RowCount].異音12 = retList.異音区分 == 0 ? "○" : "×";
                                        queryList[RowCount].排気ガス12 = retList.排気区分 == 0 ? "○" : "×";
                                        queryList[RowCount].燃費12 = retList.燃費区分 == 0 ? "○" : "×";
                                        queryList[RowCount].その他12 = retList.その他区分 == 0 ? "○" : "×";
                                        break;

                                }
                            }
                        }
                        RowCount++;
                    }
                }



                return queryList;

            }
        }
    }
}