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
using KyoeiSystem.Framework.Common;

namespace KyoeiSystem.Application.WCFService
{

    #region SRY1201g_Member　経費合計

    //経費合計
    public class SRY12010g_Member
    {
        [DataMember]
        public int? 車種ID { get; set; }
        [DataMember]
        public decimal 経費合計 { get; set; }
    }

    #endregion


	//レポート表示用メンバー
	public class SRY12010_Member
	{
		[DataMember]
		public int? 車種ID { get; set; }
		[DataMember]
		public string 車種名 { get; set; }
		[DataMember]
		public int? 台数 { get; set; }
		[DataMember]
		public decimal 拘束H { get; set; }
		[DataMember]
		public decimal 運転H { get; set; }
		[DataMember]
		public decimal 高速H { get; set; }
		[DataMember]
		public decimal 作業H { get; set; }
		[DataMember]
		public decimal 待機H { get; set; }
		[DataMember]
		public decimal 休憩H { get; set; }
		[DataMember]
		public decimal 残業H { get; set; }
		[DataMember]
		public decimal 深夜H { get; set; }
		[DataMember]
		public int 走行KM { get; set; }
		[DataMember]
		public int 実車KM { get; set; }
		[DataMember]
		public decimal 輸送屯数 { get; set; }
		[DataMember]
		public decimal 運送収入 { get; set; }
		[DataMember]
		public decimal 燃料L { get; set; }
		[DataMember]
		public decimal 経費合計 { get; set; }
		[DataMember]
		public string 集計年月From { get; set; }
		[DataMember]
		public string 集計年月To { get; set; }
		[DataMember]
		public string 表示順序 { get; set; }
		[DataMember]
		public string 車種指定 { get; set; }
		[DataMember]
		public string 車種ﾋﾟｯｸｱｯﾌﾟ { get; set; }
	}

	//レポート表示用メンバー
	public class SRY12010_Member_CSV
	{
		[DataMember]
		public int? 車種ID { get; set; }
		[DataMember]
		public string 車種名 { get; set; }
		[DataMember]
		public int? 台数 { get; set; }
		[DataMember]
		public decimal 拘束H { get; set; }
		[DataMember]
		public decimal 運転H { get; set; }
		[DataMember]
		public decimal 高速H { get; set; }
		[DataMember]
		public decimal 作業H { get; set; }
		[DataMember]
		public decimal 待機H { get; set; }
		[DataMember]
		public decimal 休憩H { get; set; }
		[DataMember]
		public decimal 残業H { get; set; }
		[DataMember]
		public decimal 深夜H { get; set; }
		[DataMember]
		public int 走行KM { get; set; }
		[DataMember]
		public int 実車KM { get; set; }
		[DataMember]
		public decimal 輸送屯数 { get; set; }
		[DataMember]
		public decimal 運送収入 { get; set; }
		[DataMember]
		public decimal 燃料L { get; set; }
		[DataMember]
		public decimal 経費合計 { get; set; }
		[DataMember]
		public string 集計年月From { get; set; }
		[DataMember]
		public string 集計年月To { get; set; }
		[DataMember]
		public string 表示順序 { get; set; }
		[DataMember]
		public string 車種指定 { get; set; }
		[DataMember]
		public string 車種ﾋﾟｯｸｱｯﾌﾟ { get; set; }
	}


    public class SRY12010
    {
        #region SRY12010 印刷

        public List<SRY12010_Member> SRY12010_GetDataHinList(string s車種From, string s車種To, int?[] i車種List, string p集計期間From, string p集計期間To, int i表示順序)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {


                string s集計期間From, s集計期間To;
                s集計期間From = p集計期間From.ToString().Substring(0, 4) + "年" + p集計期間From.ToString().Substring(4, 2) + "月度";
                s集計期間To = p集計期間To.ToString().Substring(0, 4) + "年" + p集計期間To.ToString().Substring(4, 2) + "月度";
                int i集計期間From, i集計期間To;
                i集計期間From = Convert.ToInt32(p集計期間From);
                i集計期間To = Convert.ToInt32(p集計期間To);

                List<SRY12010_Member> retList = new List<SRY12010_Member>();
                context.Connection.Open();

                string 車種ﾋﾟｯｸｱｯﾌﾟ指定 = string.Empty;

                //車種別経費合計
                var goukei = (from s14 in context.S14_CAR.Where(c => c.集計年月 >= i集計期間From && c.集計年月 <= i集計期間To)
                              join s14c in context.S14_CARSB.Where(c => c.集計年月 >= i集計期間From && c.集計年月 <= i集計期間To) on new { s14.車輌KEY, s14.集計年月 } equals new { s14c.車輌KEY , s14c.集計年月 } into Group
                              from s14Group in Group
                              group new { s14, s14Group } by s14.車種ID into Group
                              select new SRY12010g_Member
                              {
                                  車種ID = Group.Key,
                                  経費合計 = Group.Sum(c => c.s14Group.金額),
                              }).AsQueryable();

                var query = (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                             join s14 in context.S14_CAR.Where(c => c.集計年月 >= i集計期間From && c.集計年月 <= i集計期間To) on m06.車種ID equals s14.車種ID into Group
                             from s14Group in Group
                             join s14g in goukei on s14Group.車種ID equals s14g.車種ID into s14gGroup
                             from s14GGroup in s14gGroup
                             group new { m06 , s14Group , s14GGroup } by new { m06.車種ID , m06.車種名 , s14GGroup.経費合計 } into grGroup 
                             select new SRY12010_Member
                             {
                                 車種ID = grGroup.Key.車種ID,
                                 車種名 = grGroup.Key.車種名,
                                 台数　= grGroup.Count(),
                                 拘束H = grGroup.Sum(c => c.s14Group.拘束時間) == null ? 0 : grGroup.Sum(c => c.s14Group.拘束時間),
                                 運転H = grGroup.Sum(c => c.s14Group.運転時間) == null ? 0 : grGroup.Sum(c => c.s14Group.運転時間),
                                 高速H = grGroup.Sum(c => c.s14Group.高速時間) == null ? 0 : grGroup.Sum(c => c.s14Group.高速時間),
                                 作業H = grGroup.Sum(c => c.s14Group.作業時間) == null ? 0 : grGroup.Sum(c => c.s14Group.作業時間),
                                 待機H = grGroup.Sum(c => c.s14Group.待機時間) == null ? 0 : grGroup.Sum(c => c.s14Group.待機時間),
                                 休憩H = grGroup.Sum(c => c.s14Group.休憩時間) == null ? 0 : grGroup.Sum(c => c.s14Group.休憩時間),
                                 残業H = grGroup.Sum(c => c.s14Group.残業時間) == null ? 0 : grGroup.Sum(c => c.s14Group.残業時間),
                                 深夜H = grGroup.Sum(c => c.s14Group.深夜時間) == null ? 0 : grGroup.Sum(c => c.s14Group.深夜時間),
                                 走行KM = grGroup.Sum(c => c.s14Group.走行ＫＭ) == null ? 0 : grGroup.Sum(c => c.s14Group.走行ＫＭ),
                                 実車KM = grGroup.Sum(c => c.s14Group.実車ＫＭ) == null ? 0 : grGroup.Sum(c => c.s14Group.実車ＫＭ),
                                 輸送屯数 = grGroup.Sum(c => c.s14Group.輸送屯数) == null ? 0 : grGroup.Sum(c => c.s14Group.輸送屯数),
                                 運送収入 = grGroup.Sum(c => c.s14Group.運送収入) == null ? 0 : grGroup.Sum(c => c.s14Group.運送収入),
                                 燃料L = grGroup.Sum(c => c.s14Group.燃料Ｌ) == null ? 0 : grGroup.Sum(c => c.s14Group.燃料Ｌ),
                                 経費合計 = grGroup.Key.経費合計,
                                 集計年月From = s集計期間From,
                                 集計年月To = s集計期間To,
                                 車種指定 = s車種From + "～" + s車種To,
                                 車種ﾋﾟｯｸｱｯﾌﾟ = 車種ﾋﾟｯｸｱｯﾌﾟ指定 == "" ? "無" : 車種ﾋﾟｯｸｱｯﾌﾟ指定,
                                 表示順序 = i表示順序 == 0 ? "ID順" : i表示順序 == 1 ? "車種名順" : "運送収入順",
                             }).AsQueryable();

                if (!string.IsNullOrEmpty(s車種From + s車種To) && i車種List.Length == 0)
                {
                    //車種Fromで絞込み
                    if (!string.IsNullOrEmpty(s車種From))
                    {
                        int i車種From = AppCommon.IntParse(s車種From);
                        query = query.Where(c => c.車種ID >= i車種From);
                    }
                    //車種Toで絞込み
                    if (!string.IsNullOrEmpty(s車種To))
                    {
                        int i車種To = AppCommon.IntParse(s車種To);
                        query = query.Where(c => c.車種ID <= i車種To);
                    }
                    //車種ﾋﾟｯｸｱｯﾌﾟ
                    if (i車種List.Length > 0)
                    {
                        var intCause = i車種List;
                        query = query.Union(from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
                                            join s14 in context.S14_CAR.Where(c => c.集計年月 >= i集計期間From && c.集計年月 <= i集計期間To) on m06.車種ID equals s14.車種ID into Group
                                            from s14Group in Group
                                            join s14g in goukei on s14Group.車種ID equals s14g.車種ID into s14gGroup
                                            from s14GGroup in s14gGroup
                                            group new { m06, s14Group, s14GGroup } by new { m06.車種ID, m06.車種名, s14GGroup.経費合計 } into grGroup
                                            where intCause.Contains(grGroup.Key.車種ID)
                                            select new SRY12010_Member
                                            {
                                                車種ID = grGroup.Key.車種ID,
                                                車種名 = grGroup.Key.車種名,
                                                台数 = grGroup.Count(),
                                                拘束H = grGroup.Sum(c => c.s14Group.拘束時間) == null ? 0 : grGroup.Sum(c => c.s14Group.拘束時間),
                                                運転H = grGroup.Sum(c => c.s14Group.運転時間) == null ? 0 : grGroup.Sum(c => c.s14Group.運転時間),
                                                高速H = grGroup.Sum(c => c.s14Group.高速時間) == null ? 0 : grGroup.Sum(c => c.s14Group.高速時間),
                                                作業H = grGroup.Sum(c => c.s14Group.作業時間) == null ? 0 : grGroup.Sum(c => c.s14Group.作業時間),
                                                待機H = grGroup.Sum(c => c.s14Group.待機時間) == null ? 0 : grGroup.Sum(c => c.s14Group.待機時間),
                                                休憩H = grGroup.Sum(c => c.s14Group.休憩時間) == null ? 0 : grGroup.Sum(c => c.s14Group.休憩時間),
                                                残業H = grGroup.Sum(c => c.s14Group.残業時間) == null ? 0 : grGroup.Sum(c => c.s14Group.残業時間),
                                                深夜H = grGroup.Sum(c => c.s14Group.深夜時間) == null ? 0 : grGroup.Sum(c => c.s14Group.深夜時間),
                                                走行KM = grGroup.Sum(c => c.s14Group.走行ＫＭ) == null ? 0 : grGroup.Sum(c => c.s14Group.走行ＫＭ),
                                                実車KM = grGroup.Sum(c => c.s14Group.実車ＫＭ) == null ? 0 : grGroup.Sum(c => c.s14Group.実車ＫＭ),
                                                輸送屯数 = grGroup.Sum(c => c.s14Group.輸送屯数) == null ? 0 : grGroup.Sum(c => c.s14Group.輸送屯数),
                                                運送収入 = grGroup.Sum(c => c.s14Group.運送収入) == null ? 0 : grGroup.Sum(c => c.s14Group.運送収入),
                                                燃料L = grGroup.Sum(c => c.s14Group.燃料Ｌ) == null ? 0 : grGroup.Sum(c => c.s14Group.燃料Ｌ),
                                                経費合計 = grGroup.Key.経費合計,
                                                集計年月From = s集計期間From,
                                                集計年月To = s集計期間To,
                                                表示順序 = i表示順序 == 0 ? "ID順" : i表示順序 == 1 ? "車種名順" : "運送収入順",
                                                車種指定 = s車種From + "～" + s車種To,
                                                車種ﾋﾟｯｸｱｯﾌﾟ = 車種ﾋﾟｯｸｱｯﾌﾟ指定 == "" ? "無" : 車種ﾋﾟｯｸｱｯﾌﾟ指定,
                                            });
                    }
                }
                else
                {
                    //** From,To,ﾋﾟｯｸｱｯﾌﾟがNullの場合全件出力**//
                    query = query.Where(c => c.車種ID >= int.MinValue && c.車種ID <= int.MaxValue); 
                }

                //乗務員指定の表示
                if (i車種List.Length > 0)
                {
                    for (int i = 0; i < query.Count(); i++)
                    {
                        車種ﾋﾟｯｸｱｯﾌﾟ指定 = 車種ﾋﾟｯｸｱｯﾌﾟ指定 + i車種List[i].ToString();

                        if (i < i車種List.Length)
                        {

                            if (i == i車種List.Length - 1)
                            {
                                break;
                            }

                            車種ﾋﾟｯｸｱｯﾌﾟ指定 = 車種ﾋﾟｯｸｱｯﾌﾟ指定 + ",";

                        }

                        if (i車種List.Length == 1)
                        {
                            break;
                        }

                    }
                }

                //表示順序
                switch (i表示順序)
                {
                    case 0://車種IDを昇順
                        query = query.OrderBy(c => c.車種ID);
                        break;
                    case 1://車種名を昇順
                        query = query.OrderBy(c => c.車種名);
                        break;
                    case 2://運送収入を降順
                        query = query.OrderByDescending(c => c.運送収入);
                        break;
                }

                //retList = query.ToList();
                foreach (var rec in query)
                {
                    // 各時間項目の時分を、分に変換する。
                    rec.拘束H = (decimal)LinqSub.時間TO分(rec.拘束H);
                    rec.運転H = (decimal)LinqSub.時間TO分(rec.運転H);
                    rec.高速H = (decimal)LinqSub.時間TO分(rec.高速H);
                    rec.作業H = (decimal)LinqSub.時間TO分(rec.作業H);
                    rec.待機H = (decimal)LinqSub.時間TO分(rec.待機H);
                    rec.休憩H = (decimal)LinqSub.時間TO分(rec.休憩H);
                    rec.残業H = (decimal)LinqSub.時間TO分(rec.残業H);
                    rec.深夜H = (decimal)LinqSub.時間TO分(rec.深夜H);

                    retList.Add(rec);
                }

                return retList;
           
               
            }
        }
        #endregion


		#region SRY12010 CSV

		public List<SRY12010_Member> SRY12010_GetData_CSV(string s車種From, string s車種To, int?[] i車種List, string p集計期間From, string p集計期間To, int i表示順序)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{


				string s集計期間From, s集計期間To;
				s集計期間From = p集計期間From.ToString().Substring(0, 4) + "年" + p集計期間From.ToString().Substring(4, 2) + "月度";
				s集計期間To = p集計期間To.ToString().Substring(0, 4) + "年" + p集計期間To.ToString().Substring(4, 2) + "月度";
				int i集計期間From, i集計期間To;
				i集計期間From = Convert.ToInt32(p集計期間From);
				i集計期間To = Convert.ToInt32(p集計期間To);

				List<SRY12010_Member> retList = new List<SRY12010_Member>();
				context.Connection.Open();

				string 車種ﾋﾟｯｸｱｯﾌﾟ指定 = string.Empty;

				//車種別経費合計
				var goukei = (from s14 in context.S14_CAR.Where(c => c.集計年月 >= i集計期間From && c.集計年月 <= i集計期間To)
							  join s14c in context.S14_CARSB.Where(c => c.集計年月 >= i集計期間From && c.集計年月 <= i集計期間To) on new { s14.車輌KEY, s14.集計年月 } equals new { s14c.車輌KEY, s14c.集計年月 } into Group
							  from s14Group in Group
							  group new { s14, s14Group } by s14.車種ID into Group
							  select new SRY12010g_Member
							  {
								  車種ID = Group.Key,
								  経費合計 = Group.Sum(c => c.s14Group.金額),
							  }).AsQueryable();

				var query = (from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
							 join s14 in context.S14_CAR.Where(c => c.集計年月 >= i集計期間From && c.集計年月 <= i集計期間To) on m06.車種ID equals s14.車種ID into Group
							 from s14Group in Group
							 join s14g in goukei on s14Group.車種ID equals s14g.車種ID into s14gGroup
							 from s14GGroup in s14gGroup
							 group new { m06, s14Group, s14GGroup } by new { m06.車種ID, m06.車種名, s14GGroup.経費合計 } into grGroup
							 select new SRY12010_Member
							 {
								 車種ID = grGroup.Key.車種ID,
								 車種名 = grGroup.Key.車種名,
								 台数 = grGroup.Count(),
								 拘束H = grGroup.Sum(c => c.s14Group.拘束時間) == null ? 0 : grGroup.Sum(c => c.s14Group.拘束時間),
								 運転H = grGroup.Sum(c => c.s14Group.運転時間) == null ? 0 : grGroup.Sum(c => c.s14Group.運転時間),
								 高速H = grGroup.Sum(c => c.s14Group.高速時間) == null ? 0 : grGroup.Sum(c => c.s14Group.高速時間),
								 作業H = grGroup.Sum(c => c.s14Group.作業時間) == null ? 0 : grGroup.Sum(c => c.s14Group.作業時間),
								 待機H = grGroup.Sum(c => c.s14Group.待機時間) == null ? 0 : grGroup.Sum(c => c.s14Group.待機時間),
								 休憩H = grGroup.Sum(c => c.s14Group.休憩時間) == null ? 0 : grGroup.Sum(c => c.s14Group.休憩時間),
								 残業H = grGroup.Sum(c => c.s14Group.残業時間) == null ? 0 : grGroup.Sum(c => c.s14Group.残業時間),
								 深夜H = grGroup.Sum(c => c.s14Group.深夜時間) == null ? 0 : grGroup.Sum(c => c.s14Group.深夜時間),
								 走行KM = grGroup.Sum(c => c.s14Group.走行ＫＭ) == null ? 0 : grGroup.Sum(c => c.s14Group.走行ＫＭ),
								 実車KM = grGroup.Sum(c => c.s14Group.実車ＫＭ) == null ? 0 : grGroup.Sum(c => c.s14Group.実車ＫＭ),
								 輸送屯数 = grGroup.Sum(c => c.s14Group.輸送屯数) == null ? 0 : grGroup.Sum(c => c.s14Group.輸送屯数),
								 運送収入 = grGroup.Sum(c => c.s14Group.運送収入) == null ? 0 : grGroup.Sum(c => c.s14Group.運送収入),
								 燃料L = grGroup.Sum(c => c.s14Group.燃料Ｌ) == null ? 0 : grGroup.Sum(c => c.s14Group.燃料Ｌ),
								 経費合計 = grGroup.Key.経費合計,
								 集計年月From = s集計期間From,
								 集計年月To = s集計期間To,
								 車種指定 = s車種From + "～" + s車種To,
								 車種ﾋﾟｯｸｱｯﾌﾟ = 車種ﾋﾟｯｸｱｯﾌﾟ指定 == "" ? "無" : 車種ﾋﾟｯｸｱｯﾌﾟ指定,
								 表示順序 = i表示順序 == 0 ? "ID順" : i表示順序 == 1 ? "車種名順" : "運送収入順",
							 }).AsQueryable();

				if (!string.IsNullOrEmpty(s車種From + s車種To) && i車種List.Length == 0)
				{
					//車種Fromで絞込み
					if (!string.IsNullOrEmpty(s車種From))
					{
						int i車種From = AppCommon.IntParse(s車種From);
						query = query.Where(c => c.車種ID >= i車種From);
					}
					//車種Toで絞込み
					if (!string.IsNullOrEmpty(s車種To))
					{
						int i車種To = AppCommon.IntParse(s車種To);
						query = query.Where(c => c.車種ID <= i車種To);
					}
					//車種ﾋﾟｯｸｱｯﾌﾟ
					if (i車種List.Length > 0)
					{
						var intCause = i車種List;
						query = query.Union(from m06 in context.M06_SYA.Where(c => c.削除日付 == null)
											join s14 in context.S14_CAR.Where(c => c.集計年月 >= i集計期間From && c.集計年月 <= i集計期間To) on m06.車種ID equals s14.車種ID into Group
											from s14Group in Group
											join s14g in goukei on s14Group.車種ID equals s14g.車種ID into s14gGroup
											from s14GGroup in s14gGroup
											group new { m06, s14Group, s14GGroup } by new { m06.車種ID, m06.車種名, s14GGroup.経費合計 } into grGroup
											where intCause.Contains(grGroup.Key.車種ID)
											select new SRY12010_Member
											{
												車種ID = grGroup.Key.車種ID,
												車種名 = grGroup.Key.車種名,
												台数 = grGroup.Count(),
												拘束H = grGroup.Sum(c => c.s14Group.拘束時間) == null ? 0 : grGroup.Sum(c => c.s14Group.拘束時間),
												運転H = grGroup.Sum(c => c.s14Group.運転時間) == null ? 0 : grGroup.Sum(c => c.s14Group.運転時間),
												高速H = grGroup.Sum(c => c.s14Group.高速時間) == null ? 0 : grGroup.Sum(c => c.s14Group.高速時間),
												作業H = grGroup.Sum(c => c.s14Group.作業時間) == null ? 0 : grGroup.Sum(c => c.s14Group.作業時間),
												待機H = grGroup.Sum(c => c.s14Group.待機時間) == null ? 0 : grGroup.Sum(c => c.s14Group.待機時間),
												休憩H = grGroup.Sum(c => c.s14Group.休憩時間) == null ? 0 : grGroup.Sum(c => c.s14Group.休憩時間),
												残業H = grGroup.Sum(c => c.s14Group.残業時間) == null ? 0 : grGroup.Sum(c => c.s14Group.残業時間),
												深夜H = grGroup.Sum(c => c.s14Group.深夜時間) == null ? 0 : grGroup.Sum(c => c.s14Group.深夜時間),
												走行KM = grGroup.Sum(c => c.s14Group.走行ＫＭ) == null ? 0 : grGroup.Sum(c => c.s14Group.走行ＫＭ),
												実車KM = grGroup.Sum(c => c.s14Group.実車ＫＭ) == null ? 0 : grGroup.Sum(c => c.s14Group.実車ＫＭ),
												輸送屯数 = grGroup.Sum(c => c.s14Group.輸送屯数) == null ? 0 : grGroup.Sum(c => c.s14Group.輸送屯数),
												運送収入 = grGroup.Sum(c => c.s14Group.運送収入) == null ? 0 : grGroup.Sum(c => c.s14Group.運送収入),
												燃料L = grGroup.Sum(c => c.s14Group.燃料Ｌ) == null ? 0 : grGroup.Sum(c => c.s14Group.燃料Ｌ),
												経費合計 = grGroup.Key.経費合計,
												集計年月From = s集計期間From,
												集計年月To = s集計期間To,
												表示順序 = i表示順序 == 0 ? "ID順" : i表示順序 == 1 ? "車種名順" : "運送収入順",
												車種指定 = s車種From + "～" + s車種To,
												車種ﾋﾟｯｸｱｯﾌﾟ = 車種ﾋﾟｯｸｱｯﾌﾟ指定 == "" ? "無" : 車種ﾋﾟｯｸｱｯﾌﾟ指定,
											});
					}
				}
				else
				{
					//** From,To,ﾋﾟｯｸｱｯﾌﾟがNullの場合全件出力**//
					query = query.Where(c => c.車種ID >= int.MinValue && c.車種ID <= int.MaxValue);
				}

				//乗務員指定の表示
				if (i車種List.Length > 0)
				{
					for (int i = 0; i < query.Count(); i++)
					{
						車種ﾋﾟｯｸｱｯﾌﾟ指定 = 車種ﾋﾟｯｸｱｯﾌﾟ指定 + i車種List[i].ToString();

						if (i < i車種List.Length)
						{

							if (i == i車種List.Length - 1)
							{
								break;
							}

							車種ﾋﾟｯｸｱｯﾌﾟ指定 = 車種ﾋﾟｯｸｱｯﾌﾟ指定 + ",";

						}

						if (i車種List.Length == 1)
						{
							break;
						}

					}
				}

				//表示順序
				switch (i表示順序)
				{
				case 0://車種IDを昇順
					query = query.OrderBy(c => c.車種ID);
					break;
				case 1://車種名を昇順
					query = query.OrderBy(c => c.車種名);
					break;
				case 2://運送収入を降順
					query = query.OrderByDescending(c => c.運送収入);
					break;
				}

                //retList = query.ToList();
                foreach (var rec in query)
                {
                    // 各時間項目の時分を、分に変換する。
                    rec.拘束H = (decimal)LinqSub.時間TO分(rec.拘束H);
                    rec.運転H = (decimal)LinqSub.時間TO分(rec.運転H);
                    rec.高速H = (decimal)LinqSub.時間TO分(rec.高速H);
                    rec.作業H = (decimal)LinqSub.時間TO分(rec.作業H);
                    rec.待機H = (decimal)LinqSub.時間TO分(rec.待機H);
                    rec.休憩H = (decimal)LinqSub.時間TO分(rec.休憩H);
                    rec.残業H = (decimal)LinqSub.時間TO分(rec.残業H);
                    rec.深夜H = (decimal)LinqSub.時間TO分(rec.深夜H);

                    retList.Add(rec);
                }

                return retList;


			}
		}
		#endregion
    }
}