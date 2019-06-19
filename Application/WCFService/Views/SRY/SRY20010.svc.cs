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
using System.Web.UI.WebControls;


namespace KyoeiSystem.Application.WCFService
{
    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コード、svc、および config ファイルで同時にクラス名 "DataDriveLogService" を変更できます。
    // 注意: このサービスをテストするために WCF テスト クライアントを起動するには、ソリューション エクスプローラーで DataDriveLogService.svc または DataDriveLogService.svc.cs を選択し、デバッグを開始してください。
    public class SRY20010
    {

        /// <summary>
        /// SRY20010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class SRY20010_Member_day
        {
            [DataMember]
            public DateTime? 日付 { get; set; }
        }

        /// <summary>
        /// SRY20010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class SRY20010_Member_CAR
        {
            public int?  車輌ID { get; set; }
            public int? 車輌KEY { get; set; }
            public string 車輌番号 { get; set; }
            public string 車種名 { get; set; }
            public int? 自社部門コード { get; set; }
            public decimal? 目標燃費 { get; set; }
            public decimal? 前回メーター { get; set; }
        }

        /// <summary>
        /// SRY20010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class SRY20010_Member
        {
            public DateTime? 日付 { get; set; }
            public int?[] 車輌指定 { get; set; }
            public int? 車輌コード { get; set; }
            public string 車輌番号 { get; set; }
            public string 車種名 { get; set; }
            public decimal? 前回KM { get; set; }
            public decimal? 目標燃費 { get; set; }
            public decimal? 給油時メーター { get; set; }
            public decimal? 走行KM { get; set; }
            public decimal? 燃料L { get; set; }
            public decimal? 燃費 { get; set; }
            public decimal? 対目標増減 { get; set; }
            public string 対象年月 { get; set; }
            public DateTime rpt集計開始日 { get; set; }
            public DateTime rpt集計終了日 { get; set; }
            public int? 自社部門コード { get; set; }
            public string 自社部門名 { get; set; }
        }

        /// <summary>
        /// SRY20010  CSV
        /// </summary>
        [DataContract]
        public class SRY20010_Member_CSV
        {
            public DateTime? 日付 { get; set; }
            public int?[] 車輌指定 { get; set; }
            public int? 車輌コード { get; set; }
            public string 車輌番号 { get; set; }
            public string 車種名 { get; set; }
            public decimal? 前回KM { get; set; }
            public decimal? 目標燃費 { get; set; }
            public decimal? 給油時メーター { get; set; }
            public decimal? 走行KM { get; set; }
            public decimal? 燃料L { get; set; }
            public decimal? 燃費 { get; set; }
            public decimal? 対目標増減 { get; set; }
            public int? 自社部門コード { get; set; }
            public string 自社部門名 { get; set; }
        }

        #region 得意先売上日計表プレビュー
        /// <summary>
        /// SRY20010 得意先売上日計表プレビュー
        /// </summary>
        /// <param name="p商品ID">得意先コード</param>
        /// <returns>S02</returns>
        public List<SRY20010_Member> GetDataList(string p車輌From, string p車輌To, int?[] i車輌List, string p作成締日, string p作成年, string p作成月, DateTime p集計期間To, DateTime p集計期間From, int? p自社部門コード, string 部門名)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                //支払先指定　表示用
                string 支払先指定表示 = string.Empty;

                //query格納LIST
                List<SRY20010_Member> retList = new List<SRY20010_Member>();
                //日付格納LIST
                List<SRY20010_Member_day> retList_day = new List<SRY20010_Member_day>();
                List<SRY20010_Member_CAR> carList = new List<SRY20010_Member_CAR>();
                //日付取得LIST
                for (DateTime dDate = p集計期間From; dDate <= p集計期間To; dDate = dDate.AddDays(1))
                {
                    retList_day.Add(new SRY20010_Member_day() { 日付 = dDate });
                }

                int i作成年月 = p作成月.Length == 1 ? Convert.ToInt32(p作成年 + "0" + p作成月) : Convert.ToInt32(p作成年 + p作成月);

                context.Connection.Open();

                //前回データ取得
                var car = (from m05 in context.M05_CAR.Where(c => c.削除日付 == null)
                           join m06 in context.M06_SYA on m05.車種ID equals m06.車種ID into m06Group
                           from m06g in m06Group.DefaultIfEmpty()
                           join m13 in context.M13_MOK.Where(c => c.年月 == i作成年月) on m05.車輌KEY equals m13.車輌KEY into m13Group
                           from m13g in m13Group.DefaultIfEmpty()
                           join t03 in context.T03_KTRN.Where(t01 => t01.経費項目ID == 401 && t01.経費発生日 < p集計期間From) on m05.車輌KEY equals t03.車輌ID into t03Group
                           from t03g in t03Group.DefaultIfEmpty()
                           select new SRY20010_Member_CAR
                               {
                                車輌ID = m05.車輌ID,
                                車輌KEY = m05.車輌KEY,
                                車輌番号 = m05.車輌番号,
                                車種名 = m06g.車種名,
                                自社部門コード = m05.自社部門ID,
                                目標燃費 = m13g.目標燃費,
                                前回メーター = t03g.メーター,
                               }).AsQueryable();

                var carMax = (from cars in car
                              group cars by new { cars.車輌ID , cars.車輌KEY , cars.車輌番号 , cars.車種名 , cars.自社部門コード , cars.目標燃費 } into Group
                              select new
                              {
                                  車輌ID = Group.Key.車輌ID,
                                  車輌KEY = Group.Key.車輌KEY,
                                  車輌番号 = Group.Key.車輌番号,
                                  車種名 = Group.Key.車種名,
                                  自社部門コード = Group.Key.自社部門コード,
                                  目標燃費 = Group.Key.目標燃費,
                                  前回メーター = Group.Max(c => c.前回メーター),

                              }).AsQueryable();
                
                //締日集計処理
                var query = (from y01 in retList_day
                             from m05 in carMax
                             join t03 in context.T03_KTRN.Where(t01 => t01.経費発生日 >= p集計期間From && t01.経費発生日 <= p集計期間To) on m05.車輌KEY equals t03.車輌ID into t03Group
                             where  t03Group.Where(t03 => t03.車輌ID == m05.車輌KEY).Any() == true
                             orderby m05.車輌ID
                             select new SRY20010_Member
                             {
                                 日付 = y01.日付,
                                 車輌コード = m05.車輌ID,
                                 車輌番号 = m05.車輌番号 == null ? "" : m05.車輌番号,
                                 車種名 = m05.車種名 == null ? "" : m05.車種名,
                                 給油時メーター = t03Group.Where(t03 => t03.経費発生日 == y01.日付 && t03.車輌ID == m05.車輌KEY).Min(t01 => t01.メーター) == null ? 0
                                                : t03Group.Where(t03 => t03.経費発生日 == y01.日付 && t03.車輌ID == m05.車輌KEY).Min(t01 => t01.メーター),
                                 目標燃費 = m05.目標燃費 == null ? 0 : m05.目標燃費,
                                 前回KM = m05.前回メーター,
                                 燃料L = t03Group.Where(t03 => t03.経費発生日 == y01.日付 && t03.経費項目ID == 401).Sum(t03 => t03.数量),
                                 自社部門コード = m05.自社部門コード,
                                 対象年月 = p作成年 + " 年 　　" + p作成月 + "月度",
                                 rpt集計開始日 = p集計期間From,
                                 rpt集計終了日 = p集計期間To,

                             }).AsQueryable();



                if (!(i車輌List.Length == 0))
                {

                    //車輌コードが検索対象に入っていない時全データ取得
                    if (string.IsNullOrEmpty(p車輌From + p車輌To))
                    {
                        query = query.Where(c => c.車輌コード >= int.MaxValue);
                    }

                    //車輌From処理　Min値
                    if (!string.IsNullOrEmpty(p車輌From))
                    {
                        int? i車輌FROM = AppCommon.IntParse(p車輌From);
                        query = query.Where(c => c.車輌コード >= i車輌FROM);
                    }

                    //車輌To処理　Max値
                    if (!string.IsNullOrEmpty(p車輌To))
                    {
                        int? i車輌TO = AppCommon.IntParse(p車輌To);
                        query = query.Where(c => c.車輌コード <= i車輌TO);
                    }

                    if (p自社部門コード != null)
                    {
                        query = query.Where(c => c.自社部門コード == p自社部門コード);
                    }


                    if (i車輌List.Length > 0)
                    {
                        var intCause = i車輌List;

                        ////締日集計処理
                        query = query.Union(from y01 in retList_day
                                            from m05 in carMax
                                            join t03 in context.T03_KTRN.Where(t01 => t01.経費発生日 >= p集計期間From && t01.経費発生日 <= p集計期間To) on m05.車輌KEY equals t03.車輌ID into t03Group
											where t03Group.Where(t03 => t03.車輌ID == m05.車輌KEY).Any() == true && intCause.Contains(m05.車輌ID)

                                            orderby m05.車輌ID

                                            select new SRY20010_Member
                                            {
                                                日付 = y01.日付,
                                                車輌コード = m05.車輌ID,
                                                車輌番号 = m05.車輌番号 == null ? "" : m05.車輌番号,
                                                車種名 = m05.車種名 == "" ? "" : m05.車種名,
                                                給油時メーター = t03Group.Where(t03 => t03.経費発生日 == y01.日付 && t03.車輌ID == m05.車輌KEY).Min(t01 => t01.メーター) == null ? 0
                                                               : t03Group.Where(t03 => t03.経費発生日 == y01.日付 && t03.車輌ID == m05.車輌KEY).Min(t01 => t01.メーター),
                                                目標燃費 = m05.目標燃費 == null ? 0 : m05.目標燃費,
                                                前回KM = m05.前回メーター,
                                                燃料L = t03Group.Where(t03 => t03.経費発生日 == y01.日付 && t03.経費項目ID == 401).Sum(t03 => t03.数量),
                                                自社部門コード = m05.自社部門コード,
                                                対象年月 = p作成年 + " 年 " + p作成月 + " 月度",
                                                rpt集計開始日 = p集計期間From,
                                                rpt集計終了日 = p集計期間To,

                                            });

                        if (p自社部門コード != null)
                        {
                            query = query.Where(c => c.自社部門コード == p自社部門コード);
                        }

                    }

                }
                else
                {
                    //車輌範囲の指定が空の場合の処理

                    if (!string.IsNullOrEmpty(p車輌From))
					{
						int? i車輌FROM = AppCommon.IntParse(p車輌From);
						query = query.Where(c => c.車輌コード >= i車輌FROM);
                    }

                    if (!string.IsNullOrEmpty(p車輌To))
					{
						int? i車輌TO = AppCommon.IntParse(p車輌To);
						query = query.Where(c => c.車輌コード <= i車輌TO);
                    }

					//if (string.IsNullOrEmpty(p車輌From) && string.IsNullOrEmpty(p車輌To))
					//{
					//	query = query.Where(c => c.車輌コード >= int.MinValue && c.車輌コード <= int.MaxValue);
					//}

                    if (p自社部門コード != null)
                    {
                        query = query.Where(c => c.自社部門コード == p自社部門コード);
                    }

                }


                //支払先指定の表示
                if (i車輌List.Length > 0)
                {
                    for (int it = 0; it < query.Count(); it++)
                    {
                        支払先指定表示 = 支払先指定表示 + i車輌List[it].ToString();

                        if (it < i車輌List.Length)
                        {

                            if (it == i車輌List.Length - 1)
                            {
                                break;
                            }

                            支払先指定表示 = 支払先指定表示 + ",";

                        }

                        if (i車輌List.Length == 1)
                        {
                            break;
                        }

                    }
                }

                query = query.Distinct();
                query.OrderBy(q => new { q.車輌コード, q.日付 });

                List<SRY20010_Member> queryLIST = query.ToList();

                queryLIST = query.ToList();

                int? i車輌コード = 0;
                decimal? i走行キロ = 0;
                for (int i = 0; i < queryLIST.Count; i++)
                {
                    if(queryLIST[i].車輌コード != i車輌コード)
                    {
                        i走行キロ = queryLIST[i].前回KM;
                    }
                    i車輌コード = queryLIST[i].車輌コード;

                    if (queryLIST[i].給油時メーター != null && queryLIST[i].給油時メーター != 0)
                    {
                        queryLIST[i].走行KM = queryLIST[i].給油時メーター - (i走行キロ == null ? 0 : i走行キロ);
                        i走行キロ = queryLIST[i].給油時メーター;
                    }
                    if (queryLIST[i].燃料L != null && queryLIST[i].燃料L != 0 && queryLIST[i].走行KM != null)
                    {
                        queryLIST[i].燃費 = (queryLIST[i].走行KM / queryLIST[i].燃料L);
                        if (queryLIST[i].燃費 == null)
                        {
                            queryLIST[i].燃費 = Math.Round(AppCommon.DecimalParse((queryLIST[i].走行KM / queryLIST[i].燃料L).ToString()), 0, MidpointRounding.AwayFromZero);
                        }
                        queryLIST[i].対目標増減 = queryLIST[i].燃費 - queryLIST[i].目標燃費;
                    }
                }

                return queryLIST;

            }

        }
        #endregion


        #region 得意先売上日計表CSV
        /// <summary>
        /// SRY20010 得意先売上日計表CSV
        /// </summary>
        /// <param name="p商品ID">得意先コード</param>
        /// <returns>S02</returns>
        public List<SRY20010_Member_CSV> GetDataList_CSV(string p車輌From, string p車輌To, int?[] i車輌List, string p作成締日, string p作成年, string p作成月, DateTime p集計期間To, DateTime p集計期間From, int? p自社部門コード, string 部門名)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                //支払先指定　表示用
                string 支払先指定表示 = string.Empty;

                //query格納LIST
                List<SRY20010_Member_CSV> retList = new List<SRY20010_Member_CSV>();
                //日付格納LIST
                List<SRY20010_Member_day> retList_day = new List<SRY20010_Member_day>();

                int i作成年月 = p作成月.Length == 1 ? Convert.ToInt32(p作成年 + "0" + p作成月) : Convert.ToInt32(p作成年 + p作成月);

                //日付取得LIST
                for (DateTime dDate = p集計期間From; dDate <= p集計期間To; dDate = dDate.AddDays(1))
                {
                    retList_day.Add(new SRY20010_Member_day() { 日付 = dDate });
                }

                context.Connection.Open();

                //前回データ取得
                var car = (from m05 in context.M05_CAR.Where(c => c.削除日付 == null)
                           join m06 in context.M06_SYA on m05.車種ID equals m06.車種ID into m06Group
                           from m06g in m06Group.DefaultIfEmpty()
                           join m13 in context.M13_MOK.Where(c => c.年月 == i作成年月) on m05.車輌KEY equals m13.車輌KEY into m13Group
                           from m13g in m13Group.DefaultIfEmpty()
                           join t03 in context.T03_KTRN.Where(t01 => t01.経費項目ID == 401 && t01.経費発生日 < p集計期間From) on m05.車輌KEY equals t03.車輌ID into t03Group
                           from t03g in t03Group.DefaultIfEmpty()
                           select new SRY20010_Member_CAR
                           {
                               車輌ID = m05.車輌ID,
                               車輌KEY = m05.車輌KEY,
                               車輌番号 = m05.車輌番号,
                               車種名 = m06g.車種名,
                               自社部門コード = m05.自社部門ID,
                               目標燃費 = m13g.目標燃費,
                               前回メーター = t03g.メーター,
                           }).AsQueryable();

                var carMax = (from cars in car
                              group cars by new { cars.車輌ID, cars.車輌KEY, cars.車輌番号, cars.車種名, cars.自社部門コード, cars.目標燃費 } into Group
                              select new
                              {
                                  車輌ID = Group.Key.車輌ID,
                                  車輌KEY = Group.Key.車輌KEY,
                                  車輌番号 = Group.Key.車輌番号,
                                  車種名 = Group.Key.車種名,
                                  自社部門コード = Group.Key.自社部門コード,
                                  目標燃費 = Group.Key.目標燃費,
                                  前回メーター = Group.Max(c => c.前回メーター),

                              }).AsQueryable();


                //締日集計処理
                var query = (from y01 in retList_day
                             from m05 in car
                             join t03 in context.T03_KTRN.Where(t01 => t01.経費発生日 >= p集計期間From && t01.経費発生日 <= p集計期間To) on m05.車輌KEY equals t03.車輌ID into t03Group
                             where t03Group.Where(t03 => t03.車輌ID == m05.車輌KEY).Any() == true
                             orderby m05.車輌ID

                             select new SRY20010_Member_CSV
                             {
                                 日付 = y01.日付,
                                 車輌コード = m05.車輌ID,
                                 車輌番号 = m05.車輌番号 == null ? "" : m05.車輌番号,
                                 車種名 = m05.車種名 == null ? "" : m05.車種名,
                                 給油時メーター = t03Group.Where(t03 => t03.経費発生日 == y01.日付 && t03.車輌ID == m05.車輌KEY).Min(t01 => t01.メーター) == null ? 0
                                                : t03Group.Where(t03 => t03.経費発生日 == y01.日付 && t03.車輌ID == m05.車輌KEY).Min(t01 => t01.メーター),
                                 目標燃費 = m05.目標燃費 == null ? 0 : m05.目標燃費,
                                 前回KM = m05.前回メーター,
                                 燃料L = t03Group.Where(t03 => t03.経費発生日 == y01.日付 && t03.経費項目ID == 401).Sum(t03 => t03.数量),
                                 自社部門コード = m05.自社部門コード,

                             }).AsQueryable();



                if (!(i車輌List.Length == 0))
                {

                    //車輌コードが検索対象に入っていない時全データ取得
                    if (string.IsNullOrEmpty(p車輌From + p車輌To))
                    {
                        query = query.Where(c => c.車輌コード >= int.MaxValue);
                    }

                    //車輌From処理　Min値
                    if (!string.IsNullOrEmpty(p車輌From))
                    {
                        int? i車輌FROM = AppCommon.IntParse(p車輌From);
                        query = query.Where(c => c.車輌コード >= i車輌FROM);
                    }

                    //車輌To処理　Max値
                    if (!string.IsNullOrEmpty(p車輌To))
                    {
                        int? i車輌TO = AppCommon.IntParse(p車輌To);
                        query = query.Where(c => c.車輌コード <= i車輌TO);
                    }

                    if (p自社部門コード != null)
                    {
                        query = query.Where(c => c.自社部門コード == p自社部門コード);
                    }


                    if (i車輌List.Length > 0)
                    {
                        var intCause = i車輌List;

                        ////締日集計処理
                        query = query.Union(from y01 in retList_day
                                            from m05 in car
                                            join t03 in context.T03_KTRN.Where(t01 => t01.経費発生日 >= p集計期間From && t01.経費発生日 <= p集計期間To) on m05.車輌KEY equals t03.車輌ID into t03Group
                                            from Group in t03Group
                                            where t03Group.Where(t03 => t03.車輌ID == m05.車輌KEY).Any() == true && intCause.Contains(m05.車輌ID)
                                             
                                            orderby m05.車輌ID

                                            select new SRY20010_Member_CSV
                                            {
                                                日付 = y01.日付,
                                                車輌コード = m05.車輌ID,
                                                車輌番号 = m05.車輌番号 == null ? "" : m05.車輌番号,
                                                車種名 = m05.車種名 == "" ? "" : m05.車種名,
                                                給油時メーター = t03Group.Where(t03 => t03.経費発生日 == y01.日付 && t03.車輌ID == m05.車輌KEY).Min(t01 => t01.メーター) == null ? 0
                                                               : t03Group.Where(t03 => t03.経費発生日 == y01.日付 && t03.車輌ID == m05.車輌KEY).Min(t01 => t01.メーター),
                                                目標燃費 = m05.目標燃費 == null ? 0 : m05.目標燃費,
                                                前回KM = m05.前回メーター,
                                                燃料L = t03Group.Where(t03 => t03.経費発生日 == y01.日付 && t03.経費項目ID == 401).Sum(t03 => t03.数量),
                                                自社部門コード = Group.自社部門ID,

                                            });

                        if (p自社部門コード != null)
                        {
                            query = query.Where(c => c.自社部門コード == p自社部門コード);
                        }

                    }

                }
                else
                {
                    //車輌範囲の指定が空の場合の処理

					if (!string.IsNullOrEmpty(p車輌From))
					{
						int? i車輌FROM = AppCommon.IntParse(p車輌From);
						query = query.Where(c => c.車輌コード >= i車輌FROM);
					}

					if (!string.IsNullOrEmpty(p車輌To))
					{
						int? i車輌TO = AppCommon.IntParse(p車輌To);
						query = query.Where(c => c.車輌コード <= i車輌TO);
					}

					//if (string.IsNullOrEmpty(p車輌From) && string.IsNullOrEmpty(p車輌To))
					//{
					//	query = query.Where(c => c.車輌コード >= int.MinValue && c.車輌コード <= int.MaxValue);
					//}

                    if (p自社部門コード != null)
                    {
                        query = query.Where(c => c.自社部門コード == p自社部門コード);
                    }

                }


                //支払先指定の表示
                if (i車輌List.Length > 0)
                {
                    for (int it = 0; it < query.Count(); it++)
                    {
                        支払先指定表示 = 支払先指定表示 + i車輌List[it].ToString();

                        if (it < i車輌List.Length)
                        {

                            if (it == i車輌List.Length - 1)
                            {
                                break;
                            }

                            支払先指定表示 = 支払先指定表示 + ",";

                        }

                        if (i車輌List.Length == 1)
                        {
                            break;
                        }

                    }
                }

                query = query.Distinct();
                query.OrderBy(q => new { q.車輌コード, q.日付 });

                List<SRY20010_Member_CSV> queryLIST = query.ToList();

                queryLIST = query.ToList();

                int? i車輌コード = 0;
                decimal? i走行キロ = 0;
                for (int i = 0; i < queryLIST.Count; i++)
                {
                    if (queryLIST[i].車輌コード != i車輌コード)
                    {
                        i走行キロ = queryLIST[i].前回KM;
                    }
                    i車輌コード = queryLIST[i].車輌コード;

                    if (queryLIST[i].給油時メーター != null && queryLIST[i].給油時メーター != 0)
                    {
                        queryLIST[i].走行KM = queryLIST[i].給油時メーター - (i走行キロ == null ? 0 : i走行キロ);
                        i走行キロ = queryLIST[i].給油時メーター;
                    }
                    if (queryLIST[i].燃料L != null && queryLIST[i].燃料L != 0 && queryLIST[i].走行KM != null)
                    {
                        queryLIST[i].燃費 = (queryLIST[i].走行KM / queryLIST[i].燃料L);
                        if (queryLIST[i].燃費 == null)
                        {
                            queryLIST[i].燃費 = Math.Round(AppCommon.DecimalParse((queryLIST[i].走行KM / queryLIST[i].燃料L).ToString()), 0, MidpointRounding.AwayFromZero);
                        }
                        queryLIST[i].対目標増減 = queryLIST[i].燃費 - queryLIST[i].目標燃費;
                    }
                }

                return queryLIST;

            }

        }
        #endregion
    }
}
