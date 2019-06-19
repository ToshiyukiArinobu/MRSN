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
using System.Data.SqlClient;


namespace KyoeiSystem.Application.WCFService
{
    public class TKS05010_KIKAN
    {
        [DataMember]
        public int 得意先ID { get; set; }
        [DataMember]
        public int 得意先KEY { get; set; }
        [DataMember]
        public string 得意先名 { get; set; }
        [DataMember]
        public int? 親子区分ID { get; set; }
        [DataMember]
        public string 新規区分 { get; set; }
        [DataMember]
        public int? 親ID { get; set; }
        [DataMember]
        public int Ｔ税区分ID { get; set; }
        [DataMember]
        public int 得意先締日 { get; set; }
        [DataMember]
        public decimal 締日期首残 { get; set; }
        [DataMember]
        public decimal 月次期首残 { get; set; }
        [DataMember]
		public int 前月データ区分 { get; set; }
		[DataMember]
		public DateTime? 前月開始日付 { get; set; }
		[DataMember]
		public DateTime? 前月終了日付 { get; set; }
		[DataMember]
		public DateTime? 前月月次開始日付 { get; set; }
		[DataMember]
		public DateTime? 前月月次終了日付 { get; set; }
		[DataMember]
		public DateTime? 開始日付1 { get; set; }
		[DataMember]
		public DateTime? 終了日付1 { get; set; }
        [DataMember]
        public DateTime? 開始日付2 { get; set; }
        [DataMember]
        public DateTime? 終了日付2 { get; set; }
        [DataMember]
        public DateTime? 開始日付3 { get; set; }
        [DataMember]
        public DateTime? 終了日付3 { get; set; }
        [DataMember]
        public string str開始日付1 { get; set; }
        [DataMember]
        public string str終了日付1 { get; set; }
        [DataMember]
        public string str開始日付2 { get; set; }
        [DataMember]
        public string str終了日付2 { get; set; }
        [DataMember]
        public string str開始日付3 { get; set; }
        [DataMember]
		public string str終了日付3 { get; set; }
		[DataMember]
		public DateTime? 開始月次日付 { get; set; }
		[DataMember]
		public DateTime? 終了月次日付 { get; set; }
		[DataMember]
		public DateTime? 開始月次入金日付 { get; set; }
		[DataMember]
		public DateTime? 終了月次入金日付 { get; set; }
        [DataMember]
        public DateTime? クリア開始日付 { get; set; }
        [DataMember]
        public DateTime? クリア終了日付 { get; set; }
        [DataMember]
        public string strクリア開始日付 { get; set; }
        [DataMember]
        public string strクリア終了日付 { get; set; }


    }

	/// <summary>
	/// 請求書関連機能
	/// </summary>
	public class TKS05010
	{

        class WORK_MEISAI
        {
            public int 得意先ID { get; set; }
            public int 得意先KEY { get; set; }
            public int 回数 { get; set; }
            public int 締日 { get; set; }
            public int? 親子区分ID { get; set; }
            public int? 親ID { get; set; }
            public DateTime? 締集計開始日 { get; set; }
            public DateTime? 締集計終了日 { get; set; }
            public int? 消費税率 { get; set; }
            public int Ｔ税区分ID { get; set; }
            public DateTime? 消費税適用日 { get; set; }
            public decimal 締日前月残高 { get; set; }
            public decimal 締日入金現金 { get; set; }
            public decimal 締日入金手形 { get; set; }
            public decimal 締日入金その他 { get; set; }
            public decimal 締日売上金額 { get; set; }
            public decimal 締日通行料 { get; set; }
            public decimal 締日課税売上 { get; set; }
            public decimal 締日非課税売上 { get; set; }
            public decimal 締日消費税 { get; set; }
            public decimal 締日内傭車売上 { get; set; }
            public decimal 締日内傭車料 { get; set; }

            public decimal 締日未定件数 { get; set; }
            public decimal 締日件数 { get; set; }
        }

        class WORK_TOOTAL
        {
            public int 得意先ID { get; set; }
            public int 得意先KEY { get; set; }
            public DateTime 消費税適用日 { get; set; }
            public int 消費税 { get; set; }
            public string 得意先名 { get; set; }
            public int 得意先締日 { get; set; }
            public int 当月請求合計 { get; set; }
            public int 当月売上額 { get; set; }
            public int 当月通行料 { get; set; }
            public int 当月課税金額 { get; set; }
            public int 当月非課税金額 { get; set; }
            public int 当月消費税 { get; set; }
            public int 前月繰越額 { get; set; }
            public int 当月入金額 { get; set; }
            public int 当月入金調整額 { get; set; }
            public int 差引繰越額 { get; set; }
            public int 売上金額計1 { get; set; }
            public int 売上金額計2 { get; set; }
            public int 売上金額計3 { get; set; }
            public int 消費税率_BEFORE { get; set; }
            public int 消費税率_AFTER { get; set; }
            public int 計算済売上金額 { get; set; }
            public int 連番 { get; set; }
            public int Ｔ税区分ID { get; set; }
            public int 請求書区分ID { get; set; }
            public string 請求年月 { get; set; }
        }

		class UWKLIST
		{
			public int 得意先ID;
			public int 請求書ID;
			public int 内訳ID;
		}
		class TOKLIST
		{
			public int 得意先ID;
			public int 得意先KEY;
			public bool 親;
		}


        class DateFromTo
        {
            public bool Result;
            public DateTime DATEFrom;
            public DateTime DATETo;
            public int Kikan;
        }
        class DateList
        {
            public bool Result;
            public DateTime[] dDATEList;
            public int Kikan;
        }
        

		/// <summary>
        /// 得意先期間一覧取得
        /// </summary>
        /// <param name="p端末ID">端末ID</param>
        /// <returns>W_TKS05010_01_Memberのリスト</returns>
        public List<TKS05010_KIKAN> TKS05010_KIKAN_SET (string p得意先ピックアップ, int?[] i得意先List, int? p得意先範囲指定From, int? p得意先範囲指定To, int? p作成締日, int? p作成年月, DateTime p作成年月日, int 計算期間の再計算)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                try
                {
                    using (DbTransaction transaction = context.Connection.BeginTransaction())
                    {
                        int 作成年, 作成月;
                        作成年 = p作成年月日.Year;
                        作成月 = p作成年月日.Month;

                        int 前月年月;
                        前月年月 = (p作成年月日.AddMonths(-1).Year * 100) + (p作成年月日.AddMonths(-1).Month);

                        // 明細を取得　集計済みの期間と前月の最終終了日を取得
                        var query = (from tok in context.M01_TOK.Where(x => x.削除日付 == null && (x.取引区分 == 0 || x.取引区分 == 1))
									 join s01_1 in context.S01_TOKS.Where(x => x.集計年月 == p作成年月 && x.回数 == 1) on tok.得意先KEY equals s01_1.得意先KEY into s01_1Group
									 from s01_1g in s01_1Group.DefaultIfEmpty()
									 join s11_1 in context.S11_TOKG.Where(x => x.集計年月 == p作成年月 && x.回数 == 1) on tok.得意先KEY equals s11_1.得意先KEY into s11_1Group
									 from s11_1g in s11_1Group.DefaultIfEmpty()
									 join s01_2 in context.S01_TOKS.Where(x => x.集計年月 == p作成年月 && x.回数 == 2) on tok.得意先KEY equals s01_2.得意先KEY into s01_2Group
                                     from s01_2g in s01_2Group.DefaultIfEmpty()
                                     join s01_3 in context.S01_TOKS.Where(x => x.集計年月 == p作成年月 && x.回数 == 3) on tok.得意先KEY equals s01_3.得意先KEY into s01_3Group
                                     from s01_3g in s01_3Group.DefaultIfEmpty()
									 join s01zen in context.S01_TOKS.Where(x => x.集計年月 == 前月年月) on tok.得意先KEY equals s01zen.得意先KEY into s01zenGroup
									 from s01zeng in s01zenGroup.OrderByDescending(x => x.回数).Take(1).DefaultIfEmpty()
									 join s11zen in context.S11_TOKG.Where(x => x.集計年月 == 前月年月) on tok.得意先KEY equals s11zen.得意先KEY into s11zenGroup
									 from s11zeng in s11zenGroup.OrderByDescending(x => x.回数).Take(1).DefaultIfEmpty()
									 select new TKS05010_KIKAN
                                     {
                                         得意先KEY = tok.得意先KEY,
                                         得意先ID = tok.得意先ID,
                                         新規区分 = s01_1Group.Any() == true ? "" : "新規",
                                         得意先締日 = tok.Ｔ締日,
                                         得意先名 = tok.略称名,
                                         締日期首残 = tok.Ｔ締日期首残,
                                         月次期首残 = tok.Ｔ月次期首残,
                                         親子区分ID = tok.親子区分ID,
                                         親ID = tok.親ID,
										 Ｔ税区分ID = tok.Ｔ税区分ID,
										 前月開始日付 = s01zeng.締集計開始日,
										 前月終了日付 = s01zeng.締集計終了日,
										 前月月次開始日付 = s11zeng.月集計開始日,
										 前月月次終了日付 = s11zeng.月集計終了日,
										 開始日付1 = s01_1g.締集計開始日,
                                         終了日付1 = s01_1g.締集計終了日,
                                         開始日付2 = s01_2g.締集計開始日,
                                         終了日付2 = s01_2g.締集計終了日,
                                         開始日付3 = s01_3g.締集計開始日,
                                         終了日付3 = s01_3g.締集計開始日,
										 開始月次日付 = s11_1g.月集計開始日,
										 終了月次日付 = s11_1g.月集計終了日,

                                     }).AsQueryable();

                        var query2 = query;
                        if (!(string.IsNullOrEmpty(p得意先範囲指定From.ToString() + p得意先範囲指定To.ToString()) && i得意先List.Length == 0))
                        {

                            if (string.IsNullOrEmpty(p得意先範囲指定From.ToString() + p得意先範囲指定To.ToString()))
                            {
                                query2 = query2.Where(c => c.得意先ID >= int.MaxValue);
                            }
                            if (p得意先範囲指定From != null && p得意先範囲指定From != 0)
                            {
                                query2 = query.Where(c => c.得意先ID >= p得意先範囲指定From);
                            }
                            if (p得意先範囲指定To != null && p得意先範囲指定To != 0)
                            {
                                query2 = query2.Where(c => c.得意先ID <= p得意先範囲指定To);
                            }

                            if (i得意先List.Length > 0)
                            {
                                var intCause = i得意先List;

                                query2 = query2.Union(from q in query
                                                      where intCause.Contains(q.得意先ID)
                                                      select q
                                                      );
                            }

                        }

                        query2 = query2.Distinct();


						int? 自社締日 = (from m87 in context.M87_CNTL
									 where m87.管理ID == 1
									 select m87.得意先自社締日).FirstOrDefault();
						int? 月次集計区分 = (from m87 in context.M87_CNTL
									 where m87.管理ID == 1
									   select m87.月次集計区分).FirstOrDefault();
                        
                        //
                        //int? sime = 自社締日;
                        //if (自社締日 > DateTime.DaysInMonth(p作成年月日.Year, p作成年月日.Month))
                        //{
                        //    sime = DateTime.DaysInMonth(p作成年月日.Year, p作成年月日.Month);
                        //}
                        //DateFromTo ret_Getuji = GetDateFromTo(Convert.ToInt32(作成年), Convert.ToInt32(作成月), Convert.ToInt32(sime));
                        DateFromTo ret_Getuji = GetDateFromTo(Convert.ToInt32(作成年), Convert.ToInt32(作成月), Convert.ToInt32(自社締日));
						DateTime? 開始月次日付 = null;
						DateTime? 終了月次日付 = null;
						DateTime? 開始月次入金日付 = null;
						DateTime? 終了月次入金日付 = null;
						if (ret_Getuji.Result == true)
						{
							開始月次日付 = ret_Getuji.DATEFrom;
							終了月次日付 = ret_Getuji.DATETo;
							開始月次入金日付 = ret_Getuji.DATEFrom;
							終了月次入金日付 = ret_Getuji.DATETo;
                        }

                        if (p作成締日 != null)
                        {
                            query2 = query2.Where(c => c.得意先締日 == p作成締日);
                        }
                        query2 = query2.OrderBy(c => c.得意先ID);

                        var result = query2.ToList();
                        foreach (var row in result)
                        {
							//row.開始月次日付 = 開始月次日付;
							//row.終了月次日付 = 終了月次日付;
							row.開始月次入金日付 = 開始月次入金日付;
							row.終了月次入金日付 = 終了月次入金日付;
                            
                            if (row.前月終了日付 == null)
                            {
                                //
                                DateFromTo ret = GetDateFromTo(Convert.ToInt32(作成年), Convert.ToInt32(作成月), Convert.ToInt32(row.得意先締日));
                                if (ret.Result == false)
                                {
                                    row.クリア開始日付 = null;
                                    row.クリア終了日付 = null;
                                }
                                else
                                {
                                    row.クリア開始日付 = ret.DATEFrom;
                                    row.クリア終了日付 = ret.DATETo;
                                }

                            }
                            else
                            {
                                DateFromTo ret = GetDateFromTo(Convert.ToInt32(作成年), Convert.ToInt32(作成月), Convert.ToInt32(row.得意先締日));
                                if (ret.Result == false)
                                {
                                    row.クリア開始日付 = null;
                                    row.クリア終了日付 = null;
                                }
                                else
                                {
                                    if (row.クリア終了日付 == null)
                                    {
                                        row.クリア終了日付 = ret.DATETo;
                                    }
                                    row.クリア開始日付 = ((DateTime)row.前月終了日付).AddDays(1);
                                }
                            }

							//集計期間の再計算
                            if (計算期間の再計算 == 1)
                            {
                                if (row.前月終了日付 == null)
                                {
                                    //
                                    DateFromTo ret = GetDateFromTo(Convert.ToInt32(作成年), Convert.ToInt32(作成月), Convert.ToInt32(row.得意先締日));
                                    if (ret.Result == false)
                                    {
                                        row.開始日付1 = null;
                                        row.終了日付1 = null;
                                    }
                                    else
                                    {
                                        row.開始日付1 = ret.DATEFrom;
                                        row.終了日付1 = ret.DATETo;
                                    }

                                }
                                else
                                {
                                    DateFromTo ret = GetDateFromTo(Convert.ToInt32(作成年), Convert.ToInt32(作成月), Convert.ToInt32(row.得意先締日));
                                    if (ret.Result == false)
                                    {
                                        row.開始日付1 = null;
                                        row.終了日付1 = null;
                                    }
                                    else
                                    {
                                        row.終了日付1 = ret.DATETo;
                                        row.開始日付1 = ((DateTime)row.前月終了日付).AddDays(1);
                                    }
                                }

                                row.開始日付2 = null;
                                row.終了日付2 = null;
                                row.開始日付3 = null;
                                row.終了日付3 = null;

								row.開始月次日付 = 開始月次日付;
								row.終了月次日付 = 終了月次日付;

								#region 月次集計区分
								//月次集計区分
								if (月次集計区分 == 0)
								{
									row.開始月次日付 = row.開始日付1;
									if (row.終了日付3 != null)
									{
										row.終了月次日付 = row.終了日付3;
									}
									else
									{
										if (row.終了日付2 != null)
										{
											row.終了月次日付 = row.終了日付2;
										}
										else
										{
											if (row.終了日付1 != null)
											{
												row.終了月次日付 = row.終了日付1;
											}
										}
									}
								}
								#endregion
                            }
                            else
                            {
								if (row.前月終了日付 == null && row.開始日付1 == null)
								{
									//
									DateFromTo ret = GetDateFromTo(Convert.ToInt32(作成年), Convert.ToInt32(作成月), Convert.ToInt32(row.得意先締日));
									if (ret.Result == false)
									{
										row.開始日付1 = null;
										row.終了日付1 = null;
									}
									else
									{
										row.開始日付1 = ret.DATEFrom;
										row.終了日付1 = ret.DATETo;
									}

								}
								else
								{
									if (row.前月終了日付 != null && row.開始日付1 == null)
									{
										DateFromTo ret = GetDateFromTo(Convert.ToInt32(作成年), Convert.ToInt32(作成月), Convert.ToInt32(row.得意先締日));
										if (ret.Result == false)
										{
											row.開始日付1 = null;
											row.終了日付1 = null;
										}
										else
										{
											if (row.終了日付1 == null)
											{
												row.終了日付1 = ret.DATETo;
											}
											row.開始日付1 = ((DateTime)row.前月終了日付).AddDays(1);
										}

									}
									if (row.開始日付2 != null && row.終了日付2 == null)
									{
										DateFromTo ret = GetDateFromTo(Convert.ToInt32(作成年), Convert.ToInt32(作成月), Convert.ToInt32(row.得意先締日));
										if (ret.Result == false)
										{
											row.終了日付2 = null;
										}
										else
										{
											row.終了日付2 = ret.DATETo;
										}
									}
									if (row.開始日付3 != null && row.終了日付3 == null)
									{
										DateFromTo ret = GetDateFromTo(Convert.ToInt32(作成年), Convert.ToInt32(作成月), Convert.ToInt32(row.得意先締日));
										if (ret.Result == false)
										{
											row.終了日付3 = null;
										}
										else
										{
											row.終了日付3 = ret.DATETo;
										}
									}
								}

								if (row.開始月次日付 == null)
								{
									row.開始月次日付 = 開始月次日付;
								}
								if (row.終了月次日付 == null)
								{
									row.終了月次日付 = 終了月次日付;
								}

								#region 月次集計区分
								//月次集計区分
								if (月次集計区分 == 0)
								{
									row.開始月次日付 = row.開始日付1;
									if (row.終了日付3 != null)
									{
										row.終了月次日付 = row.終了日付3;
									}
									else
									{
										if (row.終了日付2 != null)
										{
											row.終了月次日付 = row.終了日付2;
										}
										else
										{
											if (row.終了日付1 != null)
											{
												row.終了月次日付 = row.終了日付1;
											}
										}
									}
								}
								#endregion
							}
                        }

                        foreach (var row in result)
                        {
                            row.str開始日付1 = row.開始日付1 == null ? "" : ((DateTime)(row.開始日付1)).ToString("yyyy/MM/dd");
                            row.str開始日付2 = row.開始日付2 == null ? "" : ((DateTime)(row.開始日付2)).ToString("yyyy/MM/dd");
                            row.str開始日付3 = row.開始日付3 == null ? "" : ((DateTime)(row.開始日付3)).ToString("yyyy/MM/dd");
                            row.str終了日付1 = row.終了日付1 == null ? "" : ((DateTime)(row.終了日付1)).ToString("yyyy/MM/dd");
                            row.str終了日付2 = row.終了日付2 == null ? "" : ((DateTime)(row.終了日付2)).ToString("yyyy/MM/dd");
                            row.str終了日付3 = row.終了日付3 == null ? "" : ((DateTime)(row.終了日付3)).ToString("yyyy/MM/dd");
                            row.strクリア開始日付 = row.クリア開始日付 == null ? "" : ((DateTime)(row.クリア開始日付)).ToString("yyyy/MM/dd");
                            row.strクリア終了日付 = row.クリア終了日付 == null ? "" : ((DateTime)(row.クリア終了日付)).ToString("yyyy/MM/dd");
                        }


                        return result;

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        /// <summary>
        /// 得意先集計
        /// </summary>
        /// <param name="p端末ID">端末ID</param>
        /// <returns>W_TKS05010_01_Memberのリスト</returns>
        public int TKS05010_SYUKEI(List<TKS05010_KIKAN> p得意先List, int? p作成年月, DateTime p作成年月日)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                try
                {
					//using (DbTransaction transaction = context.Connection.BeginTransaction())
					//{
                        DateTime Date日付1, Date日付2, Date日付3, Date日付4, Date日付5 , Date日付6;
                        foreach (var row in p得意先List)
                        {
                            row.開始日付1 = DateTime.TryParse(row.str開始日付1, out Date日付1) == true ? Date日付1 : (DateTime?)null;
                            row.開始日付2 = DateTime.TryParse(row.str開始日付2, out Date日付2) == true ? Date日付2 : (DateTime?)null;
                            row.開始日付3 = DateTime.TryParse(row.str開始日付3, out Date日付3) == true ? Date日付3 : (DateTime?)null;
                            row.終了日付1 = DateTime.TryParse(row.str終了日付1, out Date日付4) == true ? Date日付4 : (DateTime?)null;
                            row.終了日付2 = DateTime.TryParse(row.str終了日付2, out Date日付5) == true ? Date日付5 : (DateTime?)null;
                            row.終了日付3 = DateTime.TryParse(row.str終了日付3, out Date日付6) == true ? Date日付6 : (DateTime?)null;
                        }


                        //期首を算出
                        int i期首年月;
                        var query_m87 = from m87 in context.M87_CNTL where m87.管理ID == 1 select (int)m87.期首年月;
                        i期首年月 = query_m87.First();


                        #region 締日

                        #region 集計1回目

                        // 明細を取得
                        var query = (from tok in p得意先List.Where(c => c.開始日付1 != null && c.終了日付1 != null)
									 from trn in context.V_TRN明細消費税付.Where(c => c.得意先KEY == tok.得意先KEY && c.消費税率 != null && c.請求日付 >= tok.開始日付1 && c.請求日付 <= tok.終了日付1)
                                     from cnt in context.M87_CNTL.Where(c => c.管理ID == 1).DefaultIfEmpty()
									 //where trn.請求日付 >= tok.開始日付1 && trn.請求日付 <= tok.終了日付1
                                     select new WORK_MEISAI
                                     {
                                         得意先ID = tok.得意先ID,
                                         得意先KEY = tok.得意先KEY,
                                         締日前月残高 = tok.締日期首残,
                                         親子区分ID = tok.親子区分ID,
                                         親ID = tok.親ID,
                                         締集計開始日 = tok.開始日付1,
                                         締集計終了日 = tok.終了日付1,
                                         消費税適用日 = trn.適用開始日付,
                                         消費税率 = trn.消費税率,
                                         Ｔ税区分ID = tok.Ｔ税区分ID == 0 ? (int)cnt.売上消費税端数区分 : tok.Ｔ税区分ID,
                                         締日消費税 = 0,

                                         締日売上金額 = (trn.入力区分 != 3 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                        (trn.入力区分 == 3 && trn.明細行 == 1 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                        0,
                                         締日通行料 = (trn.入力区分 != 3 && trn.社内区分 == 0) == true ? trn.通行料 :
                                                        (trn.入力区分 == 3 && trn.明細行 == 1 && trn.社内区分 == 0) == true ? trn.通行料 :
                                                        0,
                                         締日課税売上 = (trn.請求税区分 == 0 && trn.入力区分 != 3 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                        (trn.請求税区分 == 0 && trn.入力区分 == 3 && trn.明細行 == 1 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                        0,
                                         締日非課税売上 = (trn.請求税区分 == 1 && trn.入力区分 != 3 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                        (trn.請求税区分 == 1 && trn.入力区分 == 3 && trn.明細行 == 1 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                        0,
                                         締日内傭車売上 = (trn.支払先KEY != 0 && trn.支払先KEY != null && trn.入力区分 != 3) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ + trn.通行料 :
                                                        (trn.支払先KEY != 0 && trn.支払先KEY != null && trn.入力区分 == 3 && trn.明細行 != 1) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ + trn.通行料 :
                                                        0,
                                         締日内傭車料 = (trn.支払先KEY != 0 && trn.支払先KEY != null && trn.入力区分 != 3) == true ? trn.支払金額 + trn.支払通行料 :
                                                        (trn.支払先KEY != 0 && trn.支払先KEY != null && trn.入力区分 == 3 && trn.明細行 != 1) == true ? trn.支払金額 + trn.支払通行料 :
                                                        0,

										 締日件数 = 1,
										 締日未定件数 = trn.売上未定区分 == 0 ? 0 : 1,

                                     }).ToList();

						query = query.Union(from tok in p得意先List
                                     join nyuk in context.T04_NYUK.Where(c => c.明細区分 == 2) on tok.得意先KEY equals nyuk.取引先KEY into Group
                                     from cnt in context.M87_CNTL.Where(c => c.管理ID == 1).DefaultIfEmpty()
                                     select new WORK_MEISAI
                                     {
                                         得意先ID = tok.得意先ID,
                                         得意先KEY = tok.得意先KEY,
                                         親子区分ID = tok.親子区分ID,
                                         親ID = tok.親ID,
                                         締日前月残高 = tok.締日期首残,
                                         締集計開始日 = tok.開始日付1,
                                         締集計終了日 = tok.終了日付1,
                                         Ｔ税区分ID = tok.Ｔ税区分ID == 0 ? (int)cnt.売上消費税端数区分 : tok.Ｔ税区分ID,
                                         締日消費税 = 0,

                                         締日入金現金 = Group.Where(c => c.入出金日付 >= tok.開始日付1 && c.入出金日付 <= tok.終了日付1 && (c.入出金区分 == 1 || c.入出金区分 == 2 || c.入出金区分 == 3)) != null ? Group.Where(c => c.入出金日付 >= tok.開始日付1 && c.入出金日付 <= tok.終了日付1 && (c.入出金区分 == 1 || c.入出金区分 == 2 || c.入出金区分 == 3)).Sum(c => c.入出金金額) : 0,
                                         締日入金手形 = Group.Where(c => c.入出金日付 >= tok.開始日付1 && c.入出金日付 <= tok.終了日付1 && (c.入出金区分 == 4)) != null ? Group.Where(c => c.入出金日付 >= tok.開始日付1 && c.入出金日付 <= tok.終了日付1 && (c.入出金区分 == 4)).Sum(c => c.入出金金額) : 0,
                                         締日入金その他 = Group.Where(c => c.入出金日付 >= tok.開始日付1 && c.入出金日付 <= tok.終了日付1 && (c.入出金区分 != 1 && c.入出金区分 != 2 && c.入出金区分 != 3 && c.入出金区分 != 4)) != null ? Group.Where(c => c.入出金日付 >= tok.開始日付1 && c.入出金日付 <= tok.終了日付1 && (c.入出金区分 != 1 && c.入出金区分 != 2 && c.入出金区分 != 3 && c.入出金区分 != 4)).Sum(c => c.入出金金額) : 0,

                                     }).ToList();

                        //明細消費税を計算
                       //var query2 = query.Where(c => c.Ｔ税区分ID >= 4 && c.Ｔ税区分ID <= 7);
                        List<WORK_MEISAI> query2 = new List<WORK_MEISAI>();
                        foreach (WORK_MEISAI row in query)
                        {
                            if (row.消費税率 != null)
                            {
                                switch (row.Ｔ税区分ID)
                                {
                                    case 4:
                                        break;
                                    case 5:
                                        row.締日消費税 = Math.Floor((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 6:
                                        row.締日消費税 = Math.Ceiling((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 7:
                                        row.締日消費税 = Math.Round((decimal)(row.締日課税売上 * row.消費税率) / 100, MidpointRounding.AwayFromZero);
                                        break;
                                }
                            }
                            query2.Add(row);
                        }

                        
                        //消費税率ごとに集計
                        var query3 =  (from q in query2
                                       //group q by new { q.得意先KEY, q.得意先ID, q.消費税適用日, q.Ｔ税区分ID, q.消費税率, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID, q.締日入金現金, q.締日入金手形, q.締日入金その他 } into qGroup
                                       group q by new { q.得意先KEY, q.得意先ID, q.消費税適用日, q.Ｔ税区分ID, q.消費税率, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID } into qGroup
                                       select new WORK_MEISAI
                                      {
                                          得意先KEY = qGroup.Key.得意先KEY,
                                          得意先ID = qGroup.Key.得意先ID,
                                          親子区分ID = qGroup.Key.親子区分ID,
                                          親ID = qGroup.Key.親ID,
                                          締日前月残高 = qGroup.Key.締日前月残高,
                                          締集計開始日 = qGroup.Key.締集計開始日,
                                          締集計終了日 = qGroup.Key.締集計終了日,
                                          Ｔ税区分ID = qGroup.Key.Ｔ税区分ID,
                                          消費税適用日 = qGroup.Key.消費税適用日,
                                          消費税率 = qGroup.Key.消費税率,
                                          締日課税売上 = qGroup.Select(c => c.締日課税売上).Sum(),
                                          締日消費税 = qGroup.Select(c => c.締日消費税).Sum(),
                                          締日通行料 = qGroup.Select(c => c.締日通行料).Sum(),
                                          締日内傭車売上 = qGroup.Select(c => c.締日内傭車売上).Sum(),
                                          締日内傭車料 = qGroup.Select(c => c.締日内傭車料).Sum(),
                                          締日売上金額 = qGroup.Select(c => c.締日売上金額).Sum(),
                                          締日非課税売上 = qGroup.Select(c => c.締日非課税売上).Sum(),
                                          締日入金現金 = qGroup.Select(c => c.締日入金現金).Sum(),
                                          締日入金手形 = qGroup.Select(c => c.締日入金手形).Sum(),
										  締日入金その他 = qGroup.Select(c => c.締日入金その他).Sum(),
										  締日件数 = qGroup.Select(c => c.締日件数).Sum(),
										  締日未定件数 = qGroup.Select(c => c.締日未定件数).Sum(),

                                      }).AsQueryable();
                        //税率ごとに消費税計算
                        List<WORK_MEISAI> query4 = new List<WORK_MEISAI>();
                        foreach (WORK_MEISAI row in query3)
                        {
                            if (row.消費税率 != null)
                            {

                                switch (row.Ｔ税区分ID)
                                {
                                    case 1:
                                        row.締日消費税 = Math.Floor((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 2:
                                        row.締日消費税 = Math.Ceiling((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 3:
                                        row.締日消費税 = Math.Round((decimal)(row.締日課税売上 * row.消費税率) / 100, MidpointRounding.AwayFromZero);
                                        break;
                                }
                            }
                            query4.Add(row);
                        }

                        //親子までの集計データ
                        var query5 = (from q in query4
                                      //group q by new { q.得意先KEY, q.得意先ID, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID, q.締日入金現金, q.締日入金手形, q.締日入金その他 } into qGroup
                                      group q by new { q.得意先KEY, q.得意先ID, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID } into qGroup
                                      select new WORK_MEISAI
                                      {
                                          得意先KEY = qGroup.Key.得意先KEY,
                                          得意先ID = qGroup.Key.得意先ID,
                                          親子区分ID = qGroup.Key.親子区分ID,
                                          親ID = qGroup.Key.親ID,
                                          締日前月残高 = qGroup.Key.締日前月残高,
                                          締集計開始日 = qGroup.Key.締集計開始日,
                                          締集計終了日 = qGroup.Key.締集計終了日,
                                          締日課税売上 = qGroup.Select(c => c.締日課税売上).Sum(),
                                          締日消費税 = qGroup.Select(c => c.締日消費税).Sum(),
                                          締日通行料 = qGroup.Select(c => c.締日通行料).Sum(),
                                          締日内傭車売上 = qGroup.Select(c => c.締日内傭車売上).Sum(),
                                          締日内傭車料 = qGroup.Select(c => c.締日内傭車料).Sum(),
                                          締日売上金額 = qGroup.Select(c => c.締日売上金額).Sum(),
                                          締日非課税売上 = qGroup.Select(c => c.締日非課税売上).Sum(),
                                          締日入金現金 = qGroup.Select(c => c.締日入金現金).Sum(),
                                          締日入金手形 = qGroup.Select(c => c.締日入金手形).Sum(),
										  締日入金その他 = qGroup.Select(c => c.締日入金その他).Sum(),
										  締日件数 = qGroup.Select(c => c.締日件数).Sum(),
										  締日未定件数 = qGroup.Select(c => c.締日未定件数).Sum(),
                                      }).ToList();


                        ////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //親子区分計算　

                        var queryOYA = (from q in query4.Where(c => c.親子区分ID == 1 || c.親子区分ID == 2)
                                        select q).ToList();

                        queryOYA = queryOYA.Union(from q in query4.Where(c => c.親子区分ID == 3)
                                                  join q2 in
                                                      (from oya in queryOYA
                                                       group oya by new { oya.得意先KEY, oya.得意先ID, oya.親子区分ID, oya.親ID, oya.締日前月残高, oya.Ｔ税区分ID } into oyaGroup
                                                       select new { oyaGroup.Key.得意先KEY, oyaGroup.Key.得意先ID, oyaGroup.Key.親子区分ID, oyaGroup.Key.親ID, oyaGroup.Key.締日前月残高, oyaGroup.Key.Ｔ税区分ID }) on q.親ID equals q2.得意先KEY

                                                  select new WORK_MEISAI
                                                  {
                                                      得意先KEY = q2.得意先KEY,
                                                      得意先ID = q2.得意先ID,
                                                      親子区分ID = q2.親子区分ID,
                                                      親ID = q2.親ID,
                                                      締日前月残高 = q2.締日前月残高,
                                                      締集計開始日 = q.締集計開始日,
                                                      締集計終了日 = q.締集計終了日,
                                                      Ｔ税区分ID = q2.Ｔ税区分ID,
                                                      消費税適用日 = q.消費税適用日,
                                                      消費税率 = q.消費税率,
                                                      締日課税売上 = q.締日課税売上,
                                                      締日消費税 = q.締日消費税,
                                                      締日通行料 = q.締日通行料,
                                                      締日内傭車売上 = q.締日内傭車売上,
                                                      締日内傭車料 = q.締日内傭車料,
                                                      締日売上金額 = q.締日売上金額,
                                                      締日非課税売上 = q.締日非課税売上,
                                                      締日入金現金 = q.締日入金現金,
                                                      締日入金手形 = q.締日入金手形,
													  締日入金その他 = q.締日入金その他,
													  締日件数 = q.締日件数,
													  締日未定件数 = q.締日未定件数,
                                                  }).ToList();


                        queryOYA = (from q in queryOYA
                                      //group q by new { q.得意先KEY, q.得意先ID, q.消費税適用日, q.Ｔ税区分ID, q.消費税率, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID, q.締日入金現金, q.締日入金手形, q.締日入金その他 } into qGroup
                                      group q by new { q.得意先KEY, q.得意先ID, q.消費税適用日, q.Ｔ税区分ID, q.消費税率, q.締日前月残高, q.親子区分ID, q.親ID } into qGroup
                                      select new WORK_MEISAI
                                      {
                                          得意先KEY = qGroup.Key.得意先KEY,
                                          得意先ID = qGroup.Key.得意先ID,
                                          親子区分ID = qGroup.Key.親子区分ID,
                                          親ID = qGroup.Key.親ID,
										  締日前月残高 = qGroup.Key.締日前月残高,
										  締集計開始日 = query4.Where(c => c.得意先KEY == qGroup.Key.得意先KEY).Select(c => c.締集計開始日).FirstOrDefault(),
										  締集計終了日 = query4.Where(c => c.得意先KEY == qGroup.Key.得意先KEY).Select(c => c.締集計終了日).FirstOrDefault(),
										  //締集計開始日 = qGroup.Key.締集計開始日,
										  //締集計終了日 = qGroup.Key.締集計終了日,
                                          Ｔ税区分ID = qGroup.Key.Ｔ税区分ID,
                                          消費税適用日 = qGroup.Key.消費税適用日,
                                          消費税率 = qGroup.Key.消費税率,
                                          締日課税売上 = qGroup.Select(c => c.締日課税売上).Sum(),
                                          締日消費税 = qGroup.Select(c => c.締日消費税).Sum(),
                                          締日通行料 = qGroup.Select(c => c.締日通行料).Sum(),
                                          締日内傭車売上 = qGroup.Select(c => c.締日内傭車売上).Sum(),
                                          締日内傭車料 = qGroup.Select(c => c.締日内傭車料).Sum(),
                                          締日売上金額 = qGroup.Select(c => c.締日売上金額).Sum(),
                                          締日非課税売上 = qGroup.Select(c => c.締日非課税売上).Sum(),
                                          締日入金現金 = qGroup.Select(c => c.締日入金現金).Sum(),
                                          締日入金手形 = qGroup.Select(c => c.締日入金手形).Sum(),
										  締日入金その他 = qGroup.Select(c => c.締日入金その他).Sum(),
										  締日件数 = qGroup.Select(c => c.締日件数).Sum(),
										  締日未定件数 = qGroup.Select(c => c.締日未定件数).Sum(),

                                      }).ToList();


                        //var queryOYAa = (from q in query4.Where(c => c.親子区分ID == 1 || c.親子区分ID == 2)
                        //                join q2 in query4.Where(c => c.親子区分ID == 3) on q.得意先KEY equals q2.親ID into q2Group
                        //              from q2g in q2Group.DefaultIfEmpty()
                        //                select new WORK_MEISAI 
                        //              {
                        //                  得意先KEY = q.得意先KEY,
                        //                  得意先ID = q.得意先ID,
                        //                  親子区分ID = q.親子区分ID,
                        //                  消費税適用日 = q2g == null ? null : q2g.消費税適用日,
                        //                  消費税率 = q2g == null ? null : q2g.消費税率,
                        //                  Ｔ税区分ID = q.Ｔ税区分ID,
                        //                  親ID = q.親ID,
                        //                  締日前月残高 = q.締日前月残高,
                        //                  締集計開始日 = q.締集計開始日,
                        //                  締集計終了日 = q.締集計終了日,
                        //                  締日課税売上 = q.締日課税売上 + (q2g == null ? 0 : q2g.締日課税売上),
                        //                  締日消費税 = q.締日消費税 + (q2g == null ? 0 : q2g.締日消費税),
                        //                  締日通行料 = q.締日通行料 + (q2g == null ? 0 : q2g.締日通行料),
                        //                  締日内傭車売上 = q.締日内傭車売上 + (q2g == null ? 0 : q2g.締日内傭車売上),
                        //                  締日内傭車料 = q.締日内傭車料 + (q2g == null ? 0 : q2g.締日内傭車料),
                        //                  締日売上金額 = q.締日売上金額 + (q2g == null ? 0 : q2g.締日売上金額),
                        //                  締日非課税売上 = q.締日非課税売上 + (q2g == null ? 0 : q2g.締日非課税売上),
                        //                   締日入金現金 = q.締日入金現金,
                        //                   締日入金手形 = q.締日入金手形,
                        //                   締日入金その他 = q.締日入金その他,
                        //                  締日件数 = q.締日件数 + (q2g == null ? 0 : q2g.締日件数),
                        //              }).ToList();

                        //親子区分計算 税一括計算
                        List<WORK_MEISAI> queryOYA2 = new List<WORK_MEISAI>();
                        foreach (WORK_MEISAI row in queryOYA)
                        {
                            if (row.親子区分ID == 1)
                            {
                                switch (row.Ｔ税区分ID)
                                {
                                    case 1:
                                        row.締日消費税 = row.消費税率 == null ? 0 : Math.Floor((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 2:
                                        row.締日消費税 = row.消費税率 == null ? 0 : Math.Ceiling((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 3:
                                        row.締日消費税 = row.消費税率 == null ? 0 : Math.Round((decimal)(row.締日課税売上 * row.消費税率) / 100, MidpointRounding.AwayFromZero);
                                        break;
                                }
                            }
                            queryOYA2.Add(row);
                        }

                        //親子区分計算
                        if (queryOYA2 != null)
                        {
                            queryOYA2 = (from q in queryOYA2
                                         //group q by new { q.得意先KEY, q.得意先ID, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID, q.締日入金現金, q.締日入金手形, q.締日入金その他 } into qGroup
                                         group q by new { q.得意先KEY, q.得意先ID, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID } into qGroup
                                         select new WORK_MEISAI
                                             {
                                                 得意先KEY = qGroup.Key.得意先KEY,
                                                 得意先ID = qGroup.Key.得意先ID,
                                                 親子区分ID = qGroup.Key.親子区分ID,
                                                 親ID = qGroup.Key.親ID,
                                                 締日前月残高 = qGroup.Key.締日前月残高,
                                                 締集計開始日 = qGroup.Key.締集計開始日,
                                                 締集計終了日 = qGroup.Key.締集計終了日,
                                                 締日課税売上 = qGroup.Select(c => c.締日課税売上).Sum(),
                                                 締日消費税 = qGroup.Select(c => c.締日消費税).Sum(),
                                                 締日通行料 = qGroup.Select(c => c.締日通行料).Sum(),
                                                 締日内傭車売上 = qGroup.Select(c => c.締日内傭車売上).Sum(),
                                                 締日内傭車料 = qGroup.Select(c => c.締日内傭車料).Sum(),
                                                 締日売上金額 = qGroup.Select(c => c.締日売上金額).Sum(),
                                                 締日非課税売上 = qGroup.Select(c => c.締日非課税売上).Sum(),
                                                 締日入金現金 = qGroup.Select(c => c.締日入金現金).Sum(),
                                                 締日入金手形 = qGroup.Select(c => c.締日入金手形).Sum(),
												 締日入金その他 = qGroup.Select(c => c.締日入金その他).Sum(),
												 締日件数 = qGroup.Select(c => c.締日件数).Sum(),
												 締日未定件数 = qGroup.Select(c => c.締日未定件数).Sum(),
                                             }).ToList();

                            //親とそれ以外を結合

                            query5 = (query5.Where(c => c.親子区分ID != 1 && c.親子区分ID != 2)).ToList();
                            queryOYA2 = queryOYA2.Union(query5.Where(c => c.親子区分ID != 1 && c.親子区分ID != 2)).ToList();
                        }

                        //var queryOYA3 = queryOYA2;


                        #region バグ修正箇所

                        //var q_1 = (from tok in p得意先List
                        //            join q in queryOYA2 on tok.得意先KEY equals q.得意先KEY into qGroup
                        //            from qG in qGroup.DefaultIfEmpty()

                        //           join trn in context.V_TRN明細消費税付.Where(c => (c.入力区分 == 1 && c.社内区分 == 0) || (c.入力区分 == 3 && c.明細行 == 1 && c.社内区分 == 0))
                        //                     on tok.得意先KEY equals trn == null ? null : trn.得意先KEY into trnGroup
                        //           //where trng != null

                        //            //join trn2 in context.V_TRN明細消費税付.Where(c => c.売上未定区分 == 1 && ((c.入力区分 == 1 && c.社内区分 == 0) || (c.入力区分 == 3 && c.明細行 == 1 && c.社内区分 == 0)))
                        //            //          on tok.得意先KEY equals trn2 == null ? null : trn2.得意先KEY into trn2Group
                        //            //from trn2g in trn2Group.DefaultIfEmpty()
                        //            //where trn2g != null
                        //            select new WORK_MEISAI
                        //            {
                        //                得意先KEY = tok.得意先KEY,
                        //                締日件数 = trnGroup.Where(c => c.請求日付 >= tok.開始日付1 && c.請求日付 <= tok.終了日付1).Count() == null ? 0 : trnGroup.Where(c => c.請求日付 >= tok.開始日付1 && c.請求日付 <= tok.終了日付1).Count(),
                        //                //締日未定件数 = trn2Group.Where(c => c.請求日付 >= tok.開始日付1 && c.請求日付 <= tok.終了日付1).Count() == null ? 0 : trn2Group.Where(c => c.請求日付 >= tok.開始日付1 && c.請求日付 <= tok.終了日付1).Count(),
                        //            }).ToList();

                        var queryOYA3 = (from tok in p得意先List
                                    join q in queryOYA2 on tok.得意先KEY equals q.得意先KEY into qGroup
                                    from qG in qGroup.DefaultIfEmpty()

								   //join trn in context.V_TRN明細消費税付.Where(c => (c.入力区分 == 1 && c.社内区分 == 0) || (c.入力区分 == 3 && c.明細行 == 1 && c.社内区分 == 0))
								   //		  on tok.得意先KEY equals trn == null ? null : trn.得意先KEY into trnGroup
								   ////from trng in trnGroup
								   // //where trng != null

									//join trn2 in context.V_TRN明細消費税付.Where(c => c.売上未定区分 == 1 && ((c.入力区分 == 1 && c.社内区分 == 0) || (c.入力区分 == 3 && c.明細行 == 1 && c.社内区分 == 0)))
									//		  on tok.得意先KEY equals trn2 == null ? null : trn2.得意先KEY into trn2Group
                                    //where trn2g != null
                                    select new WORK_MEISAI
                                    {
                                        得意先KEY = tok.得意先KEY,
                                        得意先ID = tok.得意先ID,
                                        親子区分ID = tok.親子区分ID,
                                        親ID = tok.親ID,
                                        締日前月残高 = qG == null ? 0 : qG.締日前月残高,
                                        締集計開始日 = tok.開始日付1,
                                        締集計終了日 = tok.終了日付1,
                                        締日課税売上 = qG == null ? 0 : qG.締日課税売上,
                                        締日消費税 = qG == null ? 0 : qG.締日消費税,
                                        締日通行料 = qG == null ? 0 : qG.締日通行料,
                                        締日内傭車売上 = qG == null ? 0 : qG.締日内傭車売上,
                                        締日内傭車料 = qG == null ? 0 : qG.締日内傭車料,
                                        締日売上金額 = qG == null ? 0 : qG.締日売上金額,
                                        締日非課税売上 = qG == null ? 0 : qG.締日非課税売上,
										締日件数 = qG == null ? 0 : qG.締日件数,
										締日未定件数 = qG == null ? 0 : qG.締日未定件数,
										//締日件数 = trnGroup.Where(c => c.請求日付 >= tok.開始日付1 && c.請求日付 <= tok.終了日付1).Count() == null ? 0 : trnGroup.Where(c => c.請求日付 >= tok.開始日付1 && c.請求日付 <= tok.終了日付1).Count(),
										//締日未定件数 = trn2Group.Where(c => c.請求日付 >= tok.開始日付1 && c.請求日付 <= tok.終了日付1).Count() == null ? 0 : trn2Group.Where(c => c.請求日付 >= tok.開始日付1 && c.請求日付 <= tok.終了日付1).Count(),
                                        締日入金現金 = qG.締日入金現金,
                                        締日入金手形 = qG.締日入金手形,
                                        締日入金その他 = qG.締日入金その他,
                                    }).ToList();


                        #endregion



                        //前残算出
                        
                        var query6 = (from q in queryOYA3
                                      from tok in context.M01_TOK.Where(c => q.得意先KEY == c.得意先KEY).DefaultIfEmpty()
                                      from s01 in context.S01_TOKS.Where(c => c.得意先KEY == q.得意先KEY && c.集計年月 < p作成年月 && c.集計年月 >= i期首年月).OrderByDescending(c => c.集計年月).ThenByDescending(c => c.回数).Take(1).DefaultIfEmpty()
                                      //join s01 in context.S01_TOKS.Where(c => c.集計年月 < p作成年月 && c.集計年月 >= i期首年月).OrderByDescending(c => c.集計年月).OrderByDescending(c => c.回数) on q.得意先KEY equals s01.得意先KEY into s01Group
                                      select new WORK_MEISAI
                                      {
                                          得意先KEY = q.得意先KEY,
                                          得意先ID = q.得意先ID,
                                          締日課税売上 = q.締日課税売上,
                                          締日消費税 = q.締日消費税,
                                          締日通行料 = q.締日通行料,
                                          締日内傭車売上 = q.締日内傭車売上,
                                          締日内傭車料 = q.締日内傭車料,
                                          締日売上金額 = q.締日売上金額,
                                          締日非課税売上 = q.締日非課税売上,
                                          締集計開始日 = q.締集計開始日,
                                          締集計終了日 = q.締集計終了日,

                                          //締日前月残高 = s01.集計年月,
                                          締日前月残高 = s01 == null ? tok.Ｔ締日期首残 : s01.締日前月残高 - s01.締日入金現金 - s01.締日入金手形 - s01.締日入金その他 + s01.締日売上金額 + s01.締日通行料 + s01.締日消費税,

                                          //context.S01_TOKS.Where(c => c.得意先KEY == q.得意先KEY && c.集計年月 < p作成年月 && c.集計年月 >= i期首年月).OrderByDescending(c => c.集計年月).OrderByDescending(c => c.回数).Take(1).Select(c => c.締日前月残高 - c.締日入金現金 - c.締日入金手形 - c.締日入金その他 + c.締日売上金額 + c.締日通行料 + c.締日消費税),
                                          //締日前月残高 = context.S01_TOKS.Where(c => c.得意先KEY == q.得意先KEY && c.集計年月 < p作成年月 && c.集計年月 >= i期首年月).Count() == 0 ? tokg.Ｔ締日期首残 : 
                                          //              context.S01_TOKS.Where(c => c.得意先KEY == q.得意先KEY && c.集計年月 < p作成年月 && c.集計年月 >= i期首年月).OrderByDescending(c => c.集計年月).OrderByDescending(c => c.回数).Select(c => c.締日前月残高 - c.締日入金現金 - c.締日入金手形 - c.締日入金その他 + c.締日売上金額 + c.締日通行料 + c.締日消費税),
                                          //s01Group.Take(1).Select(c => c.締日前月残高).Sum() + s01Group.Take(1).Select(c => c.締日売上金額).Sum() + s01Group.Take(1).Select(c => c.締日通行料).Sum() + s01Group.Take(1).Select(c => c.締日消費税).Sum()
                                          //              - s01Group.Take(1).Select(c => c.締日入金現金).Sum() - s01Group.Take(1).Select(c => c.締日入金手形).Sum() - s01Group.Take(1).Select(c => c.締日入金その他).Sum(),

                                          //締日前月残高 = s01Group.Any() == false ? q.締日前月残高 : s01Group.Sum(c => c.締日前月残高),
                                          締日件数 = q.締日件数,
                                          締日未定件数 = q.締日未定件数,
                                          締日入金現金 = q.締日入金現金,
                                          締日入金手形 = q.締日入金手形,
                                          締日入金その他 = q.締日入金その他,

                                      }).ToList();


                        //削除行を特定
                        int[] lst;
                        lst = (from q in p得意先List select q.得意先KEY).ToArray();

                        var ret = from x in context.S01_TOKS
                                  where x.集計年月 == p作成年月 && (x.回数 == 1 || x.回数 > 3) && lst.Contains(x.得意先KEY)
                                  select x;

                        foreach (var r in ret)
                        {
                            context.DeleteObject(r);
                        }
                        context.SaveChanges();


						do { }
						while ((from x in context.S01_TOKS
								where x.集計年月 == p作成年月 && (x.回数 == 1 || x.回数 > 3) && lst.Contains(x.得意先KEY)
								select x).Count() != 0);
						

						//sqlbulukcopy準備
						DataTable dt = new DataTable("S01_TOKS");
						dt.Columns.Add("得意先KEY", Type.GetType("System.Int32"));
						dt.Columns.Add("集計年月", Type.GetType("System.Int32"));
						dt.Columns.Add("回数", Type.GetType("System.Int32"));
						dt.Columns.Add("登録日時", Type.GetType("System.DateTime"));
						dt.Columns.Add("更新日時", Type.GetType("System.DateTime"));
						dt.Columns.Add("締集計開始日", Type.GetType("System.DateTime"));
						dt.Columns.Add("締集計終了日", Type.GetType("System.DateTime"));
						dt.Columns.Add("締日前月残高", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日入金現金", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日入金手形", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日入金その他", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日売上金額", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日通行料", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日課税売上", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日非課税売上", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日消費税", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日内傭車売上", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日内傭車料", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日未定件数", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日件数", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日", Type.GetType("System.Int32"));
						foreach (var r in query6)
						{
							DataRow dr = dt.NewRow();
							dr[0] = r.得意先KEY;
							dr[1] = (int)p作成年月;
							dr[2] = 1;
							dr[3] = DateTime.Now;
							dr[4] = DateTime.Now;
							dr[5] = (object)r.締集計開始日 ?? DBNull.Value;
							dr[6] = (object)r.締集計終了日 ?? DBNull.Value;
							dr[7] = r.締日前月残高;
							dr[8] = r.締日入金現金;
							dr[9] = r.締日入金手形;
							dr[10] = r.締日入金その他;
							dr[11] = r.締日売上金額;
							dr[12] = r.締日通行料;
							dr[13] = r.締日課税売上;
							dr[14] = r.締日非課税売上;
							dr[15] = r.締日消費税;
							dr[16] = r.締日内傭車売上;
							dr[17] = r.締日内傭車料;
							dr[18] = r.締日未定件数;
							dr[19] = r.締日件数;
							dr[20] = r.締日;
							dt.Rows.Add(dr);
						}


						try //SQL_BULK_COPY書込み
						{
							var connect = CommonData.TRAC3_SQL_GetConnectionString();
							using (var bulkCopy = new SqlBulkCopy(connect))
							{
								bulkCopy.DestinationTableName = dt.TableName; // テーブル名をSqlBulkCopyに教える
								bulkCopy.WriteToServer(dt); // bulkCopy実行
							}
						}
						catch (Exception e)
						{
						}

                        //データ書き込み
						//foreach (var r in query6)
						//{
						//	S01_TOKS s01 = new S01_TOKS();
						//	s01.得意先KEY = r.得意先KEY;
						//	s01.集計年月 = (int)p作成年月;
						//	s01.回数 = 1;
						//	s01.更新日時 = DateTime.Now;
						//	s01.締集計開始日 = r.締集計開始日;
						//	s01.締集計終了日 = r.締集計終了日;
						//	s01.締日 = r.締日;
						//	s01.締日課税売上 = r.締日課税売上;
						//	s01.締日件数 = r.締日件数;
						//	s01.締日消費税 = r.締日消費税;
						//	s01.締日前月残高 = r.締日前月残高;
						//	s01.締日通行料 = r.締日通行料;
						//	s01.締日内傭車売上 = r.締日内傭車売上;
						//	s01.締日内傭車料 = r.締日内傭車料;
						//	s01.締日入金その他 = r.締日入金その他;
						//	s01.締日入金現金 = r.締日入金現金;
						//	s01.締日入金手形 = r.締日入金手形;
						//	s01.締日売上金額 = r.締日売上金額;
						//	s01.締日非課税売上 = r.締日非課税売上;
						//	s01.締日未定件数 = r.締日未定件数;
						//	s01.締日件数 = r.締日件数;
						//	s01.登録日時 = DateTime.Now;
						//	s01.締日入金現金 = r.締日入金現金;
						//	s01.締日入金手形 = r.締日入金手形;
						//	s01.締日入金その他 = r.締日入金その他;
						//	context.S01_TOKS.ApplyChanges(s01);
						//}

						//context.SaveChanges();

                        //transaction.Commit();

                        #endregion


                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        #region 集計2回目

                        // 明細を取得
                        var query_2_ = (from tok in p得意先List.Where(c => c.開始日付2 != null && c.終了日付2 != null)
										from trn in context.V_TRN明細消費税付.Where(c => c.得意先KEY == tok.得意先KEY && c.請求日付 >= tok.開始日付2 && c.請求日付 <= tok.終了日付2)
                                     from cnt in context.M87_CNTL.Where(c => c.管理ID == 1).DefaultIfEmpty()
									 //where trn.請求日付 >= tok.開始日付2 && trn.請求日付 <= tok.終了日付2
                                     select new WORK_MEISAI
                                     {
                                         得意先ID = tok.得意先ID,
                                         得意先KEY = tok.得意先KEY,
                                         締日前月残高 = tok.締日期首残,
                                         親子区分ID = tok.親子区分ID,
                                         親ID = tok.親ID,
                                         締集計開始日 = tok.開始日付2,
                                         締集計終了日 = tok.終了日付2,
                                         消費税適用日 = trn.適用開始日付,
                                         消費税率 = trn.消費税率,
                                         Ｔ税区分ID = tok.Ｔ税区分ID == 0 ? (int)cnt.売上消費税端数区分 : tok.Ｔ税区分ID,
                                         締日消費税 = 0,

                                         締日売上金額 = (trn.入力区分 != 3 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                        (trn.入力区分 == 3 && trn.明細行 == 1 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                        0,
                                         締日通行料 = (trn.入力区分 != 3 && trn.社内区分 == 0) == true ? trn.通行料 :
                                                        (trn.入力区分 == 3 && trn.明細行 == 1 && trn.社内区分 == 0) == true ? trn.通行料 :
                                                        0,
                                         締日課税売上 = (trn.請求税区分 == 0 && trn.入力区分 != 3 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                        (trn.請求税区分 == 0 && trn.入力区分 == 3 && trn.明細行 == 1 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                        0,
                                         締日非課税売上 = (trn.請求税区分 == 1 && trn.入力区分 != 3 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                        (trn.請求税区分 == 1 && trn.入力区分 == 3 && trn.明細行 == 1 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                        0,
                                         締日内傭車売上 = (trn.支払先KEY != 0 && trn.支払先KEY != null && trn.入力区分 != 3) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ + trn.通行料 :
                                                        (trn.支払先KEY != 0 && trn.支払先KEY != null && trn.入力区分 == 3 && trn.明細行 != 1) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ + trn.通行料 :
                                                        0,
                                         締日内傭車料 = (trn.支払先KEY != 0 && trn.支払先KEY != null && trn.入力区分 != 3) == true ? trn.支払金額 + trn.支払通行料 :
                                                        (trn.支払先KEY != 0 && trn.支払先KEY != null && trn.入力区分 == 3 && trn.明細行 != 1) == true ? trn.支払金額 + trn.支払通行料 :
														0,
										 締日件数 = 1,
										 締日未定件数 = trn.売上未定区分 == 0 ? 0 : 1,

                                     }).ToList();

                        query_2_ = query_2_.Union(from tok in p得意先List
                                            from nyuk in context.T04_NYUK.Where(c => c.取引先KEY == tok.得意先KEY && c.明細区分 == 2)
                                            from cnt in context.M87_CNTL.Where(c => c.管理ID == 1).DefaultIfEmpty()
                                            where nyuk.入出金日付 >= tok.開始日付2 && nyuk.入出金日付 <= tok.終了日付2
                                            select new WORK_MEISAI
                                            {
                                                得意先ID = tok.得意先ID,
                                                得意先KEY = tok.得意先KEY,
                                                親子区分ID = tok.親子区分ID,
                                                親ID = tok.親ID,
                                                締日前月残高 = tok.締日期首残,
                                                締集計開始日 = tok.開始日付2,
                                                締集計終了日 = tok.終了日付2,
                                                Ｔ税区分ID = tok.Ｔ税区分ID == 0 ? (int)cnt.売上消費税端数区分 : tok.Ｔ税区分ID,
                                                締日消費税 = 0,

                                                締日入金現金 = (nyuk.入出金区分 == 1 || nyuk.入出金区分 == 2 || nyuk.入出金区分 == 3) == true ? nyuk.入出金金額 : 0,
                                                締日入金手形 = (nyuk.入出金区分 == 4) == true ? nyuk.入出金金額 : 0,
                                                締日入金その他 = (nyuk.入出金区分 != 1 && nyuk.入出金区分 != 2 && nyuk.入出金区分 != 3 && nyuk.入出金区分 != 4) == true ? nyuk.入出金金額 : 0,

                                            }).ToList();

                        //明細消費税を計算
                        //var query_2_2 = query_2_.Where(c => c.Ｔ税区分ID >= 4 && c.Ｔ税区分ID <= 7);
                        List<WORK_MEISAI> query_2_2 = new List<WORK_MEISAI>();
                        foreach (WORK_MEISAI row in query_2_)
                        {
                            if (row.消費税率 != null)
                            {
                                switch (row.Ｔ税区分ID)
                                {
                                    case 4:
                                        break;
                                    case 5:
                                        row.締日消費税 = Math.Floor((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 6:
                                        row.締日消費税 = Math.Ceiling((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 7:
                                        row.締日消費税 = Math.Round((decimal)(row.締日課税売上 * row.消費税率) / 100, MidpointRounding.AwayFromZero);
                                        break;
                                }
                            }
                            query_2_2.Add(row);
                        }


                        //消費税率ごとに集計
                        var query_2_3 = (from q in query_2_2
                                      group q by new { q.得意先KEY, q.得意先ID, q.消費税適用日, q.Ｔ税区分ID, q.消費税率, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID } into qGroup
                                      select new WORK_MEISAI
                                      {
                                          得意先KEY = qGroup.Key.得意先KEY,
                                          得意先ID = qGroup.Key.得意先ID,
                                          親子区分ID = qGroup.Key.親子区分ID,
                                          親ID = qGroup.Key.親ID,
                                          締日前月残高 = qGroup.Key.締日前月残高,
                                          締集計開始日 = qGroup.Key.締集計開始日,
                                          締集計終了日 = qGroup.Key.締集計終了日,
                                          Ｔ税区分ID = qGroup.Key.Ｔ税区分ID,
                                          消費税適用日 = qGroup.Key.消費税適用日,
                                          消費税率 = qGroup.Key.消費税率,
                                          締日課税売上 = qGroup.Select(c => c.締日課税売上).Sum(),
                                          締日消費税 = qGroup.Select(c => c.締日消費税).Sum(),
                                          締日通行料 = qGroup.Select(c => c.締日通行料).Sum(),
                                          締日内傭車売上 = qGroup.Select(c => c.締日内傭車売上).Sum(),
                                          締日内傭車料 = qGroup.Select(c => c.締日内傭車料).Sum(),
                                          締日売上金額 = qGroup.Select(c => c.締日売上金額).Sum(),
										  締日非課税売上 = qGroup.Select(c => c.締日非課税売上).Sum(),
										  締日件数 = qGroup.Select(c => c.締日件数).Sum(),
										  締日未定件数 = qGroup.Select(c => c.締日未定件数).Sum(),

                                      }).AsQueryable();
                        //税率ごとに消費税計算
                        List<WORK_MEISAI> query_2_4 = new List<WORK_MEISAI>();
                        foreach (WORK_MEISAI row in query_2_3)
                        {
                            if (row.消費税率 != null)
                            {
                                switch (row.Ｔ税区分ID)
                                {
                                    case 1:
                                        row.締日消費税 = Math.Floor((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 2:
                                        row.締日消費税 = Math.Ceiling((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 3:
                                        row.締日消費税 = Math.Round((decimal)(row.締日課税売上 * row.消費税率) / 100, MidpointRounding.AwayFromZero);
                                        break;
                                }
                            }
                            query_2_4.Add(row);
                        }

                        //親子までの集計データ
                        var query_2_5 = (from q in query_2_4
                                      group q by new { q.得意先KEY, q.得意先ID, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID } into qGroup
                                      select new WORK_MEISAI
                                      {
                                          得意先KEY = qGroup.Key.得意先KEY,
                                          得意先ID = qGroup.Key.得意先ID,
                                          親子区分ID = qGroup.Key.親子区分ID,
                                          親ID = qGroup.Key.親ID,
                                          締日前月残高 = qGroup.Key.締日前月残高,
                                          締集計開始日 = qGroup.Key.締集計開始日,
                                          締集計終了日 = qGroup.Key.締集計終了日,
                                          締日課税売上 = qGroup.Select(c => c.締日課税売上).Sum(),
                                          締日消費税 = qGroup.Select(c => c.締日消費税).Sum(),
                                          締日通行料 = qGroup.Select(c => c.締日通行料).Sum(),
                                          締日内傭車売上 = qGroup.Select(c => c.締日内傭車売上).Sum(),
                                          締日内傭車料 = qGroup.Select(c => c.締日内傭車料).Sum(),
                                          締日売上金額 = qGroup.Select(c => c.締日売上金額).Sum(),
										  締日非課税売上 = qGroup.Select(c => c.締日非課税売上).Sum(),
										  締日件数 = qGroup.Select(c => c.締日件数).Sum(),
										  締日未定件数 = qGroup.Select(c => c.締日未定件数).Sum(),

                                      }).ToList();

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //親子区分計算　



                        var query_2_OYA = (from q in query_2_4.Where(c => c.親子区分ID == 1 || c.親子区分ID == 2)
                                        select q).ToList();

                        query_2_OYA = query_2_OYA.Union(from q in query_2_4.Where(c => c.親子区分ID == 3)
                                                        join q2 in
                                                            (from oya in query_2_OYA
                                                       group oya by new { oya.得意先KEY, oya.得意先ID, oya.親子区分ID, oya.親ID, oya.締日前月残高, oya.Ｔ税区分ID } into oyaGroup
                                                       select new { oyaGroup.Key.得意先KEY, oyaGroup.Key.得意先ID, oyaGroup.Key.親子区分ID, oyaGroup.Key.親ID, oyaGroup.Key.締日前月残高, oyaGroup.Key.Ｔ税区分ID }) on q.親ID equals q2.得意先KEY
                                                  select new WORK_MEISAI
                                                  {
                                                      得意先KEY = q2.得意先KEY,
                                                      得意先ID = q2.得意先ID,
                                                      親子区分ID = q2.親子区分ID,
                                                      親ID = q2.親ID,
                                                      締日前月残高 = q2.締日前月残高,
                                                      締集計開始日 = q.締集計開始日,
                                                      締集計終了日 = q.締集計終了日,
                                                      Ｔ税区分ID = q2.Ｔ税区分ID,
                                                      消費税適用日 = q.消費税適用日,
                                                      消費税率 = q.消費税率,
                                                      締日課税売上 = q.締日課税売上,
                                                      締日消費税 = q.締日消費税,
                                                      締日通行料 = q.締日通行料,
                                                      締日内傭車売上 = q.締日内傭車売上,
                                                      締日内傭車料 = q.締日内傭車料,
                                                      締日売上金額 = q.締日売上金額,
                                                      締日非課税売上 = q.締日非課税売上,
                                                      締日入金現金 = q.締日入金現金,
                                                      締日入金手形 = q.締日入金手形,
													  締日入金その他 = q.締日入金その他,
													  締日件数 = q.締日件数,
													  締日未定件数 = q.締日未定件数,
                                                  }).ToList();


                        query_2_OYA = (from q in query_2_OYA
                                    //group q by new { q.得意先KEY, q.得意先ID, q.消費税適用日, q.Ｔ税区分ID, q.消費税率, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID, q.締日入金現金, q.締日入金手形, q.締日入金その他 } into qGroup
                                    group q by new { q.得意先KEY, q.得意先ID, q.消費税適用日, q.Ｔ税区分ID, q.消費税率, q.締日前月残高, q.親子区分ID, q.親ID } into qGroup
                                    select new WORK_MEISAI
                                    {
                                        得意先KEY = qGroup.Key.得意先KEY,
                                        得意先ID = qGroup.Key.得意先ID,
                                        親子区分ID = qGroup.Key.親子区分ID,
                                        親ID = qGroup.Key.親ID,
										締日前月残高 = qGroup.Key.締日前月残高,
										締集計開始日 = query_2_4.Where(c => c.得意先KEY == qGroup.Key.得意先KEY).Select(c => c.締集計開始日).FirstOrDefault(),
										締集計終了日 = query_2_4.Where(c => c.得意先KEY == qGroup.Key.得意先KEY).Select(c => c.締集計終了日).FirstOrDefault(),
										//締集計開始日 = qGroup.Key.締集計開始日,
										//締集計終了日 = qGroup.Key.締集計終了日,
                                        Ｔ税区分ID = qGroup.Key.Ｔ税区分ID,
                                        消費税適用日 = qGroup.Key.消費税適用日,
                                        消費税率 = qGroup.Key.消費税率,
                                        締日課税売上 = qGroup.Select(c => c.締日課税売上).Sum(),
                                        締日消費税 = qGroup.Select(c => c.締日消費税).Sum(),
                                        締日通行料 = qGroup.Select(c => c.締日通行料).Sum(),
                                        締日内傭車売上 = qGroup.Select(c => c.締日内傭車売上).Sum(),
                                        締日内傭車料 = qGroup.Select(c => c.締日内傭車料).Sum(),
                                        締日売上金額 = qGroup.Select(c => c.締日売上金額).Sum(),
                                        締日非課税売上 = qGroup.Select(c => c.締日非課税売上).Sum(),
                                        締日入金現金 = qGroup.Select(c => c.締日入金現金).Sum(),
                                        締日入金手形 = qGroup.Select(c => c.締日入金手形).Sum(),
										締日入金その他 = qGroup.Select(c => c.締日入金その他).Sum(),
										締日件数 = qGroup.Select(c => c.締日件数).Sum(),
										締日未定件数 = qGroup.Select(c => c.締日未定件数).Sum(),

                                    }).ToList();



                        //親子区分計算 税一括計算
                        List<WORK_MEISAI> query_2_OYA2 = new List<WORK_MEISAI>();
                        foreach (WORK_MEISAI row in query_2_OYA)
                        {
                            if (row.親子区分ID == 1)
                            {
                                switch (row.Ｔ税区分ID)
                                {
                                    case 1:
                                        row.締日消費税 = row.消費税率 == null ? 0 : Math.Floor((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 2:
                                        row.締日消費税 = row.消費税率 == null ? 0 : Math.Ceiling((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 3:
                                        row.締日消費税 = row.消費税率 == null ? 0 : Math.Round((decimal)(row.締日課税売上 * row.消費税率) / 100, MidpointRounding.AwayFromZero);
                                        break;
                                }
                            }
                            query_2_OYA2.Add(row);
                        }

                        //親子区分計算
                        if (query_2_OYA2 != null)
                        {
                            query_2_OYA2 = (from q in query_2_OYA2
                                            group q by new { q.得意先KEY, q.得意先ID, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID } into qGroup
                                            select new WORK_MEISAI
                                            {
                                                得意先KEY = qGroup.Key.得意先KEY,
                                                得意先ID = qGroup.Key.得意先ID,
                                                親子区分ID = qGroup.Key.親子区分ID,
                                                親ID = qGroup.Key.親ID,
                                                締日前月残高 = qGroup.Key.締日前月残高,
                                                締集計開始日 = qGroup.Key.締集計開始日,
                                                締集計終了日 = qGroup.Key.締集計終了日,
                                                締日課税売上 = qGroup.Select(c => c.締日課税売上).Sum(),
                                                締日消費税 = qGroup.Select(c => c.締日消費税).Sum(),
                                                締日通行料 = qGroup.Select(c => c.締日通行料).Sum(),
                                                締日内傭車売上 = qGroup.Select(c => c.締日内傭車売上).Sum(),
                                                締日内傭車料 = qGroup.Select(c => c.締日内傭車料).Sum(),
                                                締日売上金額 = qGroup.Select(c => c.締日売上金額).Sum(),
												締日非課税売上 = qGroup.Select(c => c.締日非課税売上).Sum(),
												締日件数 = qGroup.Select(c => c.締日件数).Sum(),
												締日未定件数 = qGroup.Select(c => c.締日未定件数).Sum(),

                                            }).ToList();

                            //親とそれ以外を結合

                            query_2_5 = (query_2_5.Where(c => c.親子区分ID != 1 && c.親子区分ID != 2)).ToList();
                            query_2_OYA2 = query_2_OYA2.Union(query_2_5.Where(c => c.親子区分ID != 1 && c.親子区分ID != 2)).ToList();
                        }

                        var query_2_OYA3 = (from tok in p得意先List
                                            join q in query_2_OYA2 on tok.得意先KEY equals q.得意先KEY into qGroup
                                         from qG in qGroup.DefaultIfEmpty()

										 //join trn in context.V_TRN明細消費税付.Where(c => (c.入力区分 == 1 && c.社内区分 == 0) || (c.入力区分 == 3 && c.明細行 == 1 && c.社内区分 == 0))
										 //		  on tok.得意先KEY equals trn == null ? null : trn.得意先KEY into trnGroup
										 ////from trng in trnGroup
										 ////where trng != null

										 //join trn2 in context.V_TRN明細消費税付.Where(c => c.売上未定区分 == 1 && ((c.入力区分 == 1 && c.社内区分 == 0) || (c.入力区分 == 3 && c.明細行 == 1 && c.社内区分 == 0)))
										 //		  on tok.得意先KEY equals trn2 == null ? null : trn2.得意先KEY into trn2Group
                                         //where trn2g != null
                                         select new WORK_MEISAI
                                         {
                                             得意先KEY = tok.得意先KEY,
                                             得意先ID = tok.得意先ID,
                                             親子区分ID = tok.親子区分ID,
                                             親ID = tok.親ID,
                                             締日前月残高 = qG == null ? 0 : qG.締日前月残高,
                                             締集計開始日 = tok.開始日付2,
                                             締集計終了日 = tok.終了日付2,
                                             締日課税売上 = qG == null ? 0 : qG.締日課税売上,
                                             締日消費税 = qG == null ? 0 : qG.締日消費税,
                                             締日通行料 = qG == null ? 0 : qG.締日通行料,
                                             締日内傭車売上 = qG == null ? 0 : qG.締日内傭車売上,
                                             締日内傭車料 = qG == null ? 0 : qG.締日内傭車料,
                                             締日売上金額 = qG == null ? 0 : qG.締日売上金額,
                                             締日非課税売上 = qG == null ? 0 : qG.締日非課税売上,
											 締日件数 = qG == null ? 0 : qG.締日件数,
											 締日未定件数 = qG == null ? 0 : qG.締日未定件数,
											 //締日件数 = trnGroup.Where(c => c.請求日付 >= tok.開始日付2 && c.請求日付 <= tok.終了日付2).Count() == null ? 0 : trnGroup.Where(c => c.請求日付 >= tok.開始日付2 && c.請求日付 <= tok.終了日付2).Count(),
											 //締日未定件数 = trn2Group.Where(c => c.請求日付 >= tok.開始日付2 && c.請求日付 <= tok.終了日付2).Count() == null ? 0 : trn2Group.Where(c => c.請求日付 >= tok.開始日付2 && c.請求日付 <= tok.終了日付2).Count(),
                                         }).ToList();

                        //前残算出

                        var query_2_6 = (from q in query_2_OYA3
                                      join s01 in context.S01_TOKS.Where(c => c.集計年月 == p作成年月 && c.回数 == 1) on q.得意先KEY equals s01.得意先KEY into s01Group
                                      from s01g in s01Group.DefaultIfEmpty()
                                      select new WORK_MEISAI
                                      {
                                          得意先KEY = q.得意先KEY,
                                          得意先ID = q.得意先ID,
                                          締日課税売上 = q.締日課税売上,
                                          締日消費税 = q.締日消費税,
                                          締日通行料 = q.締日通行料,
                                          締日内傭車売上 = q.締日内傭車売上,
                                          締日内傭車料 = q.締日内傭車料,
                                          締日売上金額 = q.締日売上金額,
                                          締日非課税売上 = q.締日非課税売上,
                                          締集計開始日 = q.締集計開始日,
                                          締集計終了日 = q.締集計終了日,
                                          締日前月残高 = s01g == null ? q.締日前月残高 : s01g.締日前月残高 + s01g.締日売上金額 + s01g.締日通行料 + s01g.締日消費税
                                                        - s01g.締日入金現金 - s01g.締日入金手形 - s01g.締日入金その他,
                                          締日件数 = q.締日件数,
                                          締日未定件数 = q.締日未定件数,

                                      }).ToList();




                        //削除行を特定
                        lst = (from q in p得意先List select q.得意先KEY).ToArray();

                        ret = from x in context.S01_TOKS
                                where x.集計年月 == p作成年月 && x.回数 == 2 && lst.Contains(x.得意先KEY)
                                select x;

                        foreach (var r in ret)
                        {
                            context.DeleteObject(r);
                        }
                        context.SaveChanges();


						do { }
						while ((from x in context.S01_TOKS
								where x.集計年月 == p作成年月 && x.回数 == 2 && lst.Contains(x.得意先KEY)
								select x).Count() != 0);

						//sqlbulukcopy準備
						dt = new DataTable("S01_TOKS");
						dt.Columns.Add("得意先KEY", Type.GetType("System.Int32"));
						dt.Columns.Add("集計年月", Type.GetType("System.Int32"));
						dt.Columns.Add("回数", Type.GetType("System.Int32"));
						dt.Columns.Add("登録日時", Type.GetType("System.DateTime"));
						dt.Columns.Add("更新日時", Type.GetType("System.DateTime"));
						dt.Columns.Add("締集計開始日", Type.GetType("System.DateTime"));
						dt.Columns.Add("締集計終了日", Type.GetType("System.DateTime"));
						dt.Columns.Add("締日前月残高", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日入金現金", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日入金手形", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日入金その他", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日売上金額", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日通行料", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日課税売上", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日非課税売上", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日消費税", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日内傭車売上", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日内傭車料", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日未定件数", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日件数", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日", Type.GetType("System.Int32"));
						foreach (var r in query_2_6)
						{
							DataRow dr = dt.NewRow();
							dr[0] = r.得意先KEY;
							dr[1] = (int)p作成年月;
							dr[2] = 2;
							dr[3] = DateTime.Now;
							dr[4] = DateTime.Now;
							dr[5] = (object)r.締集計開始日 ?? DBNull.Value;
							dr[6] = (object)r.締集計終了日 ?? DBNull.Value;
							dr[7] = r.締日前月残高;
							dr[8] = r.締日入金現金;
							dr[9] = r.締日入金手形;
							dr[10] = r.締日入金その他;
							dr[11] = r.締日売上金額;
							dr[12] = r.締日通行料;
							dr[13] = r.締日課税売上;
							dr[14] = r.締日非課税売上;
							dr[15] = r.締日消費税;
							dr[16] = r.締日内傭車売上;
							dr[17] = r.締日内傭車料;
							dr[18] = r.締日未定件数;
							dr[19] = r.締日件数;
							dr[20] = r.締日;
							dt.Rows.Add(dr);
						}

						try //SQL_BULK_COPY書込み
						{
							var connect = CommonData.TRAC3_SQL_GetConnectionString();
							using (var bulkCopy = new SqlBulkCopy(connect))
							{
								bulkCopy.DestinationTableName = dt.TableName; // テーブル名をSqlBulkCopyに教える
								bulkCopy.WriteToServer(dt); // bulkCopy実行
							}
						}
						catch (Exception e)
						{
						}

						////データ書き込み
						//foreach (var r in query_2_6)
						//{
						//	S01_TOKS s01 = new S01_TOKS();
						//	s01.得意先KEY = r.得意先KEY;
						//	s01.集計年月 = (int)p作成年月;
						//	s01.回数 = 2;
						//	s01.更新日時 = DateTime.Now;
						//	s01.締集計開始日 = r.締集計開始日;
						//	s01.締集計終了日 = r.締集計終了日;
						//	s01.締日 = r.締日;
						//	s01.締日課税売上 = r.締日課税売上;
						//	s01.締日件数 = r.締日件数;
						//	s01.締日消費税 = r.締日消費税;
						//	s01.締日前月残高 = r.締日前月残高;
						//	s01.締日通行料 = r.締日通行料;
						//	s01.締日内傭車売上 = r.締日内傭車売上;
						//	s01.締日内傭車料 = r.締日内傭車料;
						//	s01.締日入金その他 = r.締日入金その他;
						//	s01.締日入金現金 = r.締日入金現金;
						//	s01.締日入金手形 = r.締日入金手形;
						//	s01.締日売上金額 = r.締日売上金額;
						//	s01.締日非課税売上 = r.締日非課税売上;
						//	s01.締日未定件数 = r.締日未定件数;
						//	s01.締日件数 = r.締日件数;
						//	s01.登録日時 = DateTime.Now;
						//	context.S01_TOKS.ApplyChanges(s01);
						//}
						//context.SaveChanges();

                        //transaction.Commit();

                        #endregion


                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        #region 集計3回目

                        // 明細を取得
                        var query_3_ = (from tok in p得意先List.Where(c => c.開始日付3 != null && c.終了日付3 != null)
										from trn in context.V_TRN明細消費税付.Where(c => c.得意先KEY == tok.得意先KEY && c.請求日付 >= tok.開始日付3 && c.請求日付 <= tok.終了日付3)
                                        from cnt in context.M87_CNTL.Where(c => c.管理ID == 1).DefaultIfEmpty()
										//where trn.請求日付 >= tok.開始日付3 && trn.請求日付 <= tok.終了日付3
                                        select new WORK_MEISAI
                                        {
                                            得意先ID = tok.得意先ID,
                                            得意先KEY = tok.得意先KEY,
                                            締日前月残高 = tok.締日期首残,
                                            親子区分ID = tok.親子区分ID,
                                            親ID = tok.親ID,
                                            締集計開始日 = tok.開始日付3,
                                            締集計終了日 = tok.終了日付3,
                                            消費税適用日 = trn.適用開始日付,
                                            消費税率 = trn.消費税率,
                                            Ｔ税区分ID = tok.Ｔ税区分ID == 0 ? (int)cnt.売上消費税端数区分 : tok.Ｔ税区分ID,
                                            締日消費税 = 0,

                                            締日売上金額 = (trn.入力区分 != 3 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                           (trn.入力区分 == 3 && trn.明細行 == 1 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                           0,
                                            締日通行料 = (trn.入力区分 != 3 && trn.社内区分 == 0) == true ? trn.通行料 :
                                                           (trn.入力区分 == 3 && trn.明細行 == 1 && trn.社内区分 == 0) == true ? trn.通行料 :
                                                           0,
                                            締日課税売上 = (trn.請求税区分 == 0 && trn.入力区分 != 3 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                           (trn.請求税区分 == 0 && trn.入力区分 == 3 && trn.明細行 == 1 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                           0,
                                            締日非課税売上 = (trn.請求税区分 == 1 && trn.入力区分 != 3 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                           (trn.請求税区分 == 1 && trn.入力区分 == 3 && trn.明細行 == 1 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                           0,
                                            締日内傭車売上 = (trn.支払先KEY != 0 && trn.支払先KEY != null && trn.入力区分 != 3) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ + trn.通行料 :
                                                           (trn.支払先KEY != 0 && trn.支払先KEY != null && trn.入力区分 == 3 && trn.明細行 != 1) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ + trn.通行料 :
                                                           0,
                                            締日内傭車料 = (trn.支払先KEY != 0 && trn.支払先KEY != null && trn.入力区分 != 3) == true ? trn.支払金額 + trn.支払通行料 :
                                                           (trn.支払先KEY != 0 && trn.支払先KEY != null && trn.入力区分 == 3 && trn.明細行 != 1) == true ? trn.支払金額 + trn.支払通行料 :
														   0,
											締日件数 = 1,
											締日未定件数 = trn.売上未定区分 == 0 ? 0 : 1,

                                        }).ToList();

                        query_3_ = query_3_.Union(from tok in p得意先List
                                                  from nyuk in context.T04_NYUK.Where(c => c.取引先KEY == tok.得意先KEY && c.明細区分 == 2)
                                                  from cnt in context.M87_CNTL.Where(c => c.管理ID == 1).DefaultIfEmpty()
                                                  where nyuk.入出金日付 >= tok.開始日付3 && nyuk.入出金日付 <= tok.終了日付3
                                                  select new WORK_MEISAI
                                                  {
                                                      得意先ID = tok.得意先ID,
                                                      得意先KEY = tok.得意先KEY,
                                                      親子区分ID = tok.親子区分ID,
                                                      親ID = tok.親ID,
                                                      締日前月残高 = tok.締日期首残,
                                                      締集計開始日 = tok.開始日付3,
                                                      締集計終了日 = tok.終了日付3,
                                                      Ｔ税区分ID = tok.Ｔ税区分ID == 0 ? (int)cnt.売上消費税端数区分 : tok.Ｔ税区分ID,
                                                      締日消費税 = 0,

                                                      締日入金現金 = (nyuk.入出金区分 == 1 || nyuk.入出金区分 == 2 || nyuk.入出金区分 == 3) == true ? nyuk.入出金金額 : 0,
                                                      締日入金手形 = (nyuk.入出金区分 == 4) == true ? nyuk.入出金金額 : 0,
                                                      締日入金その他 = (nyuk.入出金区分 != 1 && nyuk.入出金区分 != 2 && nyuk.入出金区分 != 3 && nyuk.入出金区分 != 4) == true ? nyuk.入出金金額 : 0,

                                                  }).ToList();

                        //明細消費税を計算
                        //var query_3_2 = query_3_.Where(c => c.Ｔ税区分ID >= 4 && c.Ｔ税区分ID <= 7);
                        List<WORK_MEISAI> query_3_2 = new List<WORK_MEISAI>();
                        foreach (WORK_MEISAI row in query_3_)
                        {
                            if (row.消費税率 != null)
                            {
                                switch (row.Ｔ税区分ID)
                                {
                                    case 4:
                                        break;
                                    case 5:
                                        row.締日消費税 = Math.Floor((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 6:
                                        row.締日消費税 = Math.Ceiling((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 7:
                                        row.締日消費税 = Math.Round((decimal)(row.締日課税売上 * row.消費税率) / 100, MidpointRounding.AwayFromZero);
                                        break;
                                }
                            }
                            query_3_2.Add(row);
                        }


                        //消費税率ごとに集計
                        var query_3_3 = (from q in query_3_2
                                         group q by new { q.得意先KEY, q.得意先ID, q.消費税適用日, q.Ｔ税区分ID, q.消費税率, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID } into qGroup
                                         select new WORK_MEISAI
                                         {
                                             得意先KEY = qGroup.Key.得意先KEY,
                                             得意先ID = qGroup.Key.得意先ID,
                                             親子区分ID = qGroup.Key.親子区分ID,
                                             親ID = qGroup.Key.親ID,
                                             締日前月残高 = qGroup.Key.締日前月残高,
                                             締集計開始日 = qGroup.Key.締集計開始日,
                                             締集計終了日 = qGroup.Key.締集計終了日,
                                             Ｔ税区分ID = qGroup.Key.Ｔ税区分ID,
                                             消費税適用日 = qGroup.Key.消費税適用日,
                                             消費税率 = qGroup.Key.消費税率,
                                             締日課税売上 = qGroup.Select(c => c.締日課税売上).Sum(),
                                             締日消費税 = qGroup.Select(c => c.締日消費税).Sum(),
                                             締日通行料 = qGroup.Select(c => c.締日通行料).Sum(),
                                             締日内傭車売上 = qGroup.Select(c => c.締日内傭車売上).Sum(),
                                             締日内傭車料 = qGroup.Select(c => c.締日内傭車料).Sum(),
                                             締日売上金額 = qGroup.Select(c => c.締日売上金額).Sum(),
											 締日非課税売上 = qGroup.Select(c => c.締日非課税売上).Sum(),
											 締日件数 = qGroup.Select(c => c.締日件数).Sum(),
											 締日未定件数 = qGroup.Select(c => c.締日未定件数).Sum(),

                                         }).AsQueryable();
                        //税率ごとに消費税計算
                        List<WORK_MEISAI> query_3_4 = new List<WORK_MEISAI>();
                        foreach (WORK_MEISAI row in query_3_3)
                        {
                            if (row.消費税率 != null)
                            {
                                switch (row.Ｔ税区分ID)
                                {
                                    case 1:
                                        row.締日消費税 = Math.Floor((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 2:
                                        row.締日消費税 = Math.Ceiling((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 3:
                                        row.締日消費税 = Math.Round((decimal)(row.締日課税売上 * row.消費税率) / 100, MidpointRounding.AwayFromZero);
                                        break;
                                }
                            }
                            query_3_4.Add(row);
                        }

                        //親子までの集計データ
                        var query_3_5 = (from q in query_3_4
                                         group q by new { q.得意先KEY, q.得意先ID, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID } into qGroup
                                         select new WORK_MEISAI
                                         {
                                             得意先KEY = qGroup.Key.得意先KEY,
                                             得意先ID = qGroup.Key.得意先ID,
                                             親子区分ID = qGroup.Key.親子区分ID,
                                             親ID = qGroup.Key.親ID,
                                             締日前月残高 = qGroup.Key.締日前月残高,
                                             締集計開始日 = qGroup.Key.締集計開始日,
                                             締集計終了日 = qGroup.Key.締集計終了日,
                                             締日課税売上 = qGroup.Select(c => c.締日課税売上).Sum(),
                                             締日消費税 = qGroup.Select(c => c.締日消費税).Sum(),
                                             締日通行料 = qGroup.Select(c => c.締日通行料).Sum(),
                                             締日内傭車売上 = qGroup.Select(c => c.締日内傭車売上).Sum(),
                                             締日内傭車料 = qGroup.Select(c => c.締日内傭車料).Sum(),
                                             締日売上金額 = qGroup.Select(c => c.締日売上金額).Sum(),
											 締日非課税売上 = qGroup.Select(c => c.締日非課税売上).Sum(),
											 締日件数 = qGroup.Select(c => c.締日件数).Sum(),
											 締日未定件数 = qGroup.Select(c => c.締日未定件数).Sum(),

                                         }).ToList();

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //親子区分計算　


                        var query_3_OYA = (from q in query_3_4.Where(c => c.親子区分ID == 1 || c.親子区分ID == 2)
                                           select q).ToList();

                        query_3_OYA = query_3_OYA.Union(from q in query_3_4.Where(c => c.親子区分ID == 3)
                                                        join q2 in
                                                            (from oya in query_3_OYA
                                                       group oya by new { oya.得意先KEY, oya.得意先ID, oya.親子区分ID, oya.親ID, oya.締日前月残高, oya.Ｔ税区分ID } into oyaGroup
                                                       select new { oyaGroup.Key.得意先KEY, oyaGroup.Key.得意先ID, oyaGroup.Key.親子区分ID, oyaGroup.Key.親ID, oyaGroup.Key.締日前月残高, oyaGroup.Key.Ｔ税区分ID }) on q.親ID equals q2.得意先KEY

                                                        select new WORK_MEISAI
                                                        {
                                                            得意先KEY = q2.得意先KEY,
                                                            得意先ID = q2.得意先ID,
                                                            親子区分ID = q2.親子区分ID,
                                                            親ID = q2.親ID,
                                                            締日前月残高 = q2.締日前月残高,
                                                            締集計開始日 = q.締集計開始日,
                                                            締集計終了日 = q.締集計終了日,
                                                            Ｔ税区分ID = q2.Ｔ税区分ID,
                                                            消費税適用日 = q.消費税適用日,
                                                            消費税率 = q.消費税率,
                                                            締日課税売上 = q.締日課税売上,
                                                            締日消費税 = q.締日消費税,
                                                            締日通行料 = q.締日通行料,
                                                            締日内傭車売上 = q.締日内傭車売上,
                                                            締日内傭車料 = q.締日内傭車料,
                                                            締日売上金額 = q.締日売上金額,
                                                            締日非課税売上 = q.締日非課税売上,
                                                            締日入金現金 = q.締日入金現金,
                                                            締日入金手形 = q.締日入金手形,
															締日入金その他 = q.締日入金その他,
															締日件数 = q.締日件数,
															締日未定件数 = q.締日未定件数,
                                                        }).ToList();


                        query_3_OYA = (from q in query_3_OYA
                                       //group q by new { q.得意先KEY, q.得意先ID, q.消費税適用日, q.Ｔ税区分ID, q.消費税率, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID, q.締日入金現金, q.締日入金手形, q.締日入金その他 } into qGroup
                                       group q by new { q.得意先KEY, q.得意先ID, q.消費税適用日, q.Ｔ税区分ID, q.消費税率, q.締日前月残高, q.親子区分ID, q.親ID } into qGroup
                                       select new WORK_MEISAI
                                       {
                                           得意先KEY = qGroup.Key.得意先KEY,
                                           得意先ID = qGroup.Key.得意先ID,
                                           親子区分ID = qGroup.Key.親子区分ID,
                                           親ID = qGroup.Key.親ID,
										   締日前月残高 = qGroup.Key.締日前月残高,
										   締集計開始日 = query_3_4.Where(c => c.得意先KEY == qGroup.Key.得意先KEY).Select(c => c.締集計開始日).FirstOrDefault(),
										   締集計終了日 = query_3_4.Where(c => c.得意先KEY == qGroup.Key.得意先KEY).Select(c => c.締集計終了日).FirstOrDefault(),
										   //締集計開始日 = qGroup.Key.締集計開始日,
										   //締集計終了日 = qGroup.Key.締集計終了日,
                                           Ｔ税区分ID = qGroup.Key.Ｔ税区分ID,
                                           消費税適用日 = qGroup.Key.消費税適用日,
                                           消費税率 = qGroup.Key.消費税率,
                                           締日課税売上 = qGroup.Select(c => c.締日課税売上).Sum(),
                                           締日消費税 = qGroup.Select(c => c.締日消費税).Sum(),
                                           締日通行料 = qGroup.Select(c => c.締日通行料).Sum(),
                                           締日内傭車売上 = qGroup.Select(c => c.締日内傭車売上).Sum(),
                                           締日内傭車料 = qGroup.Select(c => c.締日内傭車料).Sum(),
                                           締日売上金額 = qGroup.Select(c => c.締日売上金額).Sum(),
                                           締日非課税売上 = qGroup.Select(c => c.締日非課税売上).Sum(),
                                           締日入金現金 = qGroup.Select(c => c.締日入金現金).Sum(),
                                           締日入金手形 = qGroup.Select(c => c.締日入金手形).Sum(),
										   締日入金その他 = qGroup.Select(c => c.締日入金その他).Sum(),
										   締日件数 = qGroup.Select(c => c.締日件数).Sum(),
										   締日未定件数 = qGroup.Select(c => c.締日未定件数).Sum(),

                                       }).ToList();



                        //親子区分計算 税一括計算
                        List<WORK_MEISAI> query_3_OYA2 = new List<WORK_MEISAI>();
                        foreach (WORK_MEISAI row in query_3_OYA)
                        {
                            if (row.親子区分ID == 1)
                            {
                                switch (row.Ｔ税区分ID)
                                {
                                    case 1:
                                        row.締日消費税 = row.消費税率 == null ? 0 : Math.Floor((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 2:
                                        row.締日消費税 = row.消費税率 == null ? 0 : Math.Ceiling((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 3:
                                        row.締日消費税 = row.消費税率 == null ? 0 : Math.Round((decimal)(row.締日課税売上 * row.消費税率) / 100, MidpointRounding.AwayFromZero);
                                        break;
                                }
                            }
                            query_3_OYA2.Add(row);
                        }

                        //親子区分計算
                        if (query_3_OYA2 != null)
                        {
                            query_3_OYA2 = (from q in query_3_OYA2
                                            group q by new { q.得意先KEY, q.得意先ID, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID } into qGroup
                                            select new WORK_MEISAI
                                            {
                                                得意先KEY = qGroup.Key.得意先KEY,
                                                得意先ID = qGroup.Key.得意先ID,
                                                親子区分ID = qGroup.Key.親子区分ID,
                                                親ID = qGroup.Key.親ID,
                                                締日前月残高 = qGroup.Key.締日前月残高,
                                                締集計開始日 = qGroup.Key.締集計開始日,
                                                締集計終了日 = qGroup.Key.締集計終了日,
                                                締日課税売上 = qGroup.Select(c => c.締日課税売上).Sum(),
                                                締日消費税 = qGroup.Select(c => c.締日消費税).Sum(),
                                                締日通行料 = qGroup.Select(c => c.締日通行料).Sum(),
                                                締日内傭車売上 = qGroup.Select(c => c.締日内傭車売上).Sum(),
                                                締日内傭車料 = qGroup.Select(c => c.締日内傭車料).Sum(),
                                                締日売上金額 = qGroup.Select(c => c.締日売上金額).Sum(),
												締日非課税売上 = qGroup.Select(c => c.締日非課税売上).Sum(),
												締日件数 = qGroup.Select(c => c.締日件数).Sum(),
												締日未定件数 = qGroup.Select(c => c.締日未定件数).Sum(),

                                            }).ToList();

                            //親とそれ以外を結合

                            query_3_5 = (query_3_5.Where(c => c.親子区分ID != 1 && c.親子区分ID != 2)).ToList();
                            query_3_OYA2 = query_3_OYA2.Union(query_3_5.Where(c => c.親子区分ID != 1 && c.親子区分ID != 2)).ToList();
                        }


                        var query_3_OYA3 = (from tok in p得意先List
                                         join q in query_3_OYA2 on tok.得意先KEY equals q.得意先KEY into qGroup
                                         from qG in qGroup.DefaultIfEmpty()

										 //join trn in context.V_TRN明細消費税付.Where(c => (c.入力区分 == 1 && c.社内区分 == 0) || (c.入力区分 == 3 && c.明細行 == 1 && c.社内区分 == 0))
										 //		  on tok.得意先KEY equals trn == null ? null : trn.得意先KEY into trnGroup
										 ////from trng in trnGroup
										 ////where trng != null

										 //join trn2 in context.V_TRN明細消費税付.Where(c => c.売上未定区分 == 1 && ((c.入力区分 == 1 && c.社内区分 == 0) || (c.入力区分 == 3 && c.明細行 == 1 && c.社内区分 == 0)))
										 //		  on tok.得意先KEY equals trn2 == null ? null : trn2.得意先KEY into trn2Group
                                         //where trn2g != null
                                         select new WORK_MEISAI
                                         {
                                             得意先KEY = tok.得意先KEY,
                                             得意先ID = tok.得意先ID,
                                             親子区分ID = tok.親子区分ID,
                                             親ID = tok.親ID,
                                             締日前月残高 = qG == null ? 0 : qG.締日前月残高,
                                             締集計開始日 = tok.開始日付3,
                                             締集計終了日 = tok.終了日付3,
                                             締日課税売上 = qG == null ? 0 : qG.締日課税売上,
                                             締日消費税 = qG == null ? 0 : qG.締日消費税,
                                             締日通行料 = qG == null ? 0 : qG.締日通行料,
                                             締日内傭車売上 = qG == null ? 0 : qG.締日内傭車売上,
                                             締日内傭車料 = qG == null ? 0 : qG.締日内傭車料,
                                             締日売上金額 = qG == null ? 0 : qG.締日売上金額,
                                             締日非課税売上 = qG == null ? 0 : qG.締日非課税売上,
											 締日件数 = qG == null ? 0 : qG.締日件数,
											 締日未定件数 = qG == null ? 0 : qG.締日未定件数,
											 //締日件数 = trnGroup.Where(c => c.請求日付 >= tok.開始日付3 && c.請求日付 <= tok.終了日付3).Count() == null ? 0 : trnGroup.Where(c => c.請求日付 >= tok.開始日付3 && c.請求日付 <= tok.終了日付3).Count(),
											 //締日未定件数 = trn2Group.Where(c => c.請求日付 >= tok.開始日付3 && c.請求日付 <= tok.終了日付3).Count() == null ? 0 : trn2Group.Where(c => c.請求日付 >= tok.開始日付3 && c.請求日付 <= tok.終了日付3).Count(),
                                         }).ToList();
 


                        //前残算出

                        var query_3_6 = (from q in query_3_OYA3
                                         join s01 in context.S01_TOKS.Where(c => c.集計年月 == p作成年月 && c.回数 == 2) on q.得意先KEY equals s01.得意先KEY into s01Group
                                         from s01g in s01Group.DefaultIfEmpty()
                                         select new WORK_MEISAI
                                         {
                                             得意先KEY = q.得意先KEY,
                                             得意先ID = q.得意先ID,
                                             締日課税売上 = q.締日課税売上,
                                             締日消費税 = q.締日消費税,
                                             締日通行料 = q.締日通行料,
                                             締日内傭車売上 = q.締日内傭車売上,
                                             締日内傭車料 = q.締日内傭車料,
                                             締日売上金額 = q.締日売上金額,
                                             締日非課税売上 = q.締日非課税売上,
                                             締集計開始日 = q.締集計開始日,
                                             締集計終了日 = q.締集計終了日,

                                             締日前月残高 = s01g == null ? q.締日前月残高 : s01g.締日前月残高 + s01g.締日売上金額 + s01g.締日通行料 + s01g.締日消費税
                                                           - s01g.締日入金現金 - s01g.締日入金手形 - s01g.締日入金その他,

                                             締日件数 = q.締日件数,
                                             締日未定件数 = q.締日未定件数,

                                         }).ToList();




                        //削除行を特定
                        lst = (from q in p得意先List select q.得意先KEY).ToArray();

                        ret = from x in context.S01_TOKS
                              where x.集計年月 == p作成年月 && x.回数 == 3 && lst.Contains(x.得意先KEY)
                              select x;

                        foreach (var r in ret)
                        {
                            context.DeleteObject(r);
                        }
                        context.SaveChanges();

						do { }
						while ((from x in context.S01_TOKS
								where x.集計年月 == p作成年月 && x.回数 == 3 && lst.Contains(x.得意先KEY)
								select x).Count() != 0);

						
						//sqlbulukcopy準備
						dt = new DataTable("S01_TOKS");
						dt.Columns.Add("得意先KEY", Type.GetType("System.Int32"));
						dt.Columns.Add("集計年月", Type.GetType("System.Int32"));
						dt.Columns.Add("回数", Type.GetType("System.Int32"));
						dt.Columns.Add("登録日時", Type.GetType("System.DateTime"));
						dt.Columns.Add("更新日時", Type.GetType("System.DateTime"));
						dt.Columns.Add("締集計開始日", Type.GetType("System.DateTime"));
						dt.Columns.Add("締集計終了日", Type.GetType("System.DateTime"));
						dt.Columns.Add("締日前月残高", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日入金現金", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日入金手形", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日入金その他", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日売上金額", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日通行料", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日課税売上", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日非課税売上", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日消費税", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日内傭車売上", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日内傭車料", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日未定件数", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日件数", Type.GetType("System.Decimal"));
						dt.Columns.Add("締日", Type.GetType("System.Int32"));
						foreach (var r in query_3_6)
						{
							DataRow dr = dt.NewRow();
							dr[0] = r.得意先KEY;
							dr[1] = (int)p作成年月;
							dr[2] = 3;
							dr[3] = DateTime.Now;
							dr[4] = DateTime.Now;
							dr[5] = (object)r.締集計開始日 ?? DBNull.Value;
							dr[6] = (object)r.締集計終了日 ?? DBNull.Value;
							dr[7] = r.締日前月残高;
							dr[8] = r.締日入金現金;
							dr[9] = r.締日入金手形;
							dr[10] = r.締日入金その他;
							dr[11] = r.締日売上金額;
							dr[12] = r.締日通行料;
							dr[13] = r.締日課税売上;
							dr[14] = r.締日非課税売上;
							dr[15] = r.締日消費税;
							dr[16] = r.締日内傭車売上;
							dr[17] = r.締日内傭車料;
							dr[18] = r.締日未定件数;
							dr[19] = r.締日件数;
							dr[20] = r.締日;
							dt.Rows.Add(dr);
						}

						try //SQL_BULK_COPY書込み
						{
							var connect = CommonData.TRAC3_SQL_GetConnectionString();
							using (var bulkCopy = new SqlBulkCopy(connect))
							{
								bulkCopy.DestinationTableName = dt.TableName; // テーブル名をSqlBulkCopyに教える
								bulkCopy.WriteToServer(dt); // bulkCopy実行
							}
						}
						catch (Exception e)
						{
						}



						////データ書き込み
						//foreach (var r in query_3_6)
						//{
						//	S01_TOKS s01 = new S01_TOKS();
						//	s01.得意先KEY = r.得意先KEY;
						//	s01.集計年月 = (int)p作成年月;
						//	s01.回数 = 3;
						//	s01.更新日時 = DateTime.Now;
						//	s01.締集計開始日 = r.締集計開始日;
						//	s01.締集計終了日 = r.締集計終了日;
						//	s01.締日 = r.締日;
						//	s01.締日課税売上 = r.締日課税売上;
						//	s01.締日件数 = r.締日件数;
						//	s01.締日消費税 = r.締日消費税;
						//	s01.締日前月残高 = r.締日前月残高;
						//	s01.締日通行料 = r.締日通行料;
						//	s01.締日内傭車売上 = r.締日内傭車売上;
						//	s01.締日内傭車料 = r.締日内傭車料;
						//	s01.締日入金その他 = r.締日入金その他;
						//	s01.締日入金現金 = r.締日入金現金;
						//	s01.締日入金手形 = r.締日入金手形;
						//	s01.締日売上金額 = r.締日売上金額;
						//	s01.締日非課税売上 = r.締日非課税売上;
						//	s01.締日未定件数 = r.締日未定件数;
						//	s01.締日件数 = r.締日件数;
						//	s01.登録日時 = DateTime.Now;
						//	context.S01_TOKS.ApplyChanges(s01);
						//}
						//context.SaveChanges();

                        //transaction.Commit();

                        #endregion


                        #endregion



                        #region 月次

                        #region 集計1回目

                        // 明細を取得
                        var queryG = (from tok in p得意先List.Where(c => c.開始月次日付 != null && c.終了月次日付 != null)
									  from trn in context.V_TRN明細消費税付.Where(c => c.得意先KEY == tok.得意先KEY && c.請求日付 >= tok.開始月次日付 && c.請求日付 <= tok.終了月次日付)
                                     from cnt in context.M87_CNTL.Where(c => c.管理ID == 1).DefaultIfEmpty()
									 //where trn.請求日付 >= tok.開始月次日付 && trn.請求日付 <= tok.終了月次日付
                                     select new WORK_MEISAI
                                     {
                                         得意先ID = tok.得意先ID,
                                         得意先KEY = tok.得意先KEY,
                                         締日前月残高 = tok.締日期首残,
                                         親子区分ID = tok.親子区分ID,
                                         親ID = tok.親ID,
                                         締集計開始日 = tok.開始月次日付,
                                         締集計終了日 = tok.終了月次日付,
                                         消費税適用日 = trn.適用開始日付,
                                         消費税率 = trn.消費税率,
                                         Ｔ税区分ID = tok.Ｔ税区分ID == 0 ? (int)cnt.売上消費税端数区分 : tok.Ｔ税区分ID,
                                         締日消費税 = 0,

                                         締日売上金額 = (trn.入力区分 != 3 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                        (trn.入力区分 == 3 && trn.明細行 == 1 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                        0,
                                         締日通行料 = (trn.入力区分 != 3 && trn.社内区分 == 0) == true ? trn.通行料 :
                                                        (trn.入力区分 == 3 && trn.明細行 == 1 && trn.社内区分 == 0) == true ? trn.通行料 :
                                                        0,
                                         締日課税売上 = (trn.請求税区分 == 0 && trn.入力区分 != 3 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                        (trn.請求税区分 == 0 && trn.入力区分 == 3 && trn.明細行 == 1 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                        0,
                                         締日非課税売上 = (trn.請求税区分 == 1 && trn.入力区分 != 3 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                        (trn.請求税区分 == 1 && trn.入力区分 == 3 && trn.明細行 == 1 && trn.社内区分 == 0) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ :
                                                        0,
                                         締日内傭車売上 = (trn.支払先KEY != 0 && trn.支払先KEY != null && trn.入力区分 != 3) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ + trn.通行料 :
                                                        (trn.支払先KEY != 0 && trn.支払先KEY != null && trn.入力区分 == 3 && trn.明細行 != 1) == true ? trn.売上金額 + trn.請求割増１ + trn.請求割増２ + trn.通行料 :
                                                        0,
                                         締日内傭車料 = (trn.支払先KEY != 0 && trn.支払先KEY != null && trn.入力区分 != 3) == true ? trn.支払金額 + trn.支払通行料 :
                                                        (trn.支払先KEY != 0 && trn.支払先KEY != null && trn.入力区分 == 3 && trn.明細行 != 1) == true ? trn.支払金額 + trn.支払通行料 :
														0,
										 締日件数 = 1,
										 締日未定件数 = trn.売上未定区分 == 0 ? 0 : 1,

                                     }).ToList();

                        queryG = queryG.Union(from tok in p得意先List
                                            join nyuk in context.T04_NYUK.Where(c => c.明細区分 == 2) on tok.得意先KEY equals nyuk.取引先KEY into Group
                                            from cnt in context.M87_CNTL.Where(c => c.管理ID == 1).DefaultIfEmpty()
                                            select new WORK_MEISAI
                                            {
                                                得意先ID = tok.得意先ID,
                                                得意先KEY = tok.得意先KEY,
                                                親子区分ID = tok.親子区分ID,
                                                親ID = tok.親ID,
                                                締日前月残高 = tok.締日期首残,
                                                締集計開始日 = tok.開始月次日付,
                                                締集計終了日 = tok.終了月次日付,
                                                Ｔ税区分ID = tok.Ｔ税区分ID == 0 ? (int)cnt.売上消費税端数区分 : tok.Ｔ税区分ID,
                                                締日消費税 = 0,

												締日入金現金 = Group.Where(c => c.入出金日付 >= tok.開始月次入金日付 && c.入出金日付 <= tok.終了月次入金日付 && (c.入出金区分 == 1 || c.入出金区分 == 2 || c.入出金区分 == 3)) != null ? Group.Where(c => c.入出金日付 >= tok.開始月次入金日付 && c.入出金日付 <= tok.終了月次入金日付 && (c.入出金区分 == 1 || c.入出金区分 == 2 || c.入出金区分 == 3)).Sum(c => c.入出金金額) : 0,
												締日入金手形 = Group.Where(c => c.入出金日付 >= tok.開始月次入金日付 && c.入出金日付 <= tok.終了月次入金日付 && (c.入出金区分 == 4)) != null ? Group.Where(c => c.入出金日付 >= tok.開始月次入金日付 && c.入出金日付 <= tok.終了月次入金日付 && (c.入出金区分 == 4)).Sum(c => c.入出金金額) : 0,
												締日入金その他 = Group.Where(c => c.入出金日付 >= tok.開始月次入金日付 && c.入出金日付 <= tok.終了月次入金日付 && (c.入出金区分 != 1 && c.入出金区分 != 2 && c.入出金区分 != 3 && c.入出金区分 != 4)) != null ? Group.Where(c => c.入出金日付 >= tok.開始月次入金日付 && c.入出金日付 <= tok.終了月次入金日付 && (c.入出金区分 != 1 && c.入出金区分 != 2 && c.入出金区分 != 3 && c.入出金区分 != 4)).Sum(c => c.入出金金額) : 0,
                                            }).ToList();

                        //明細消費税を計算
                        //var queryG2 = queryG.Where(c => c.Ｔ税区分ID >= 4 && c.Ｔ税区分ID <= 7);
                        List<WORK_MEISAI> queryG2 = new List<WORK_MEISAI>();
                        foreach (WORK_MEISAI row in queryG)
                        {
                            if (row.消費税率 != null)
                            {
                                switch (row.Ｔ税区分ID)
                                {
                                    case 4:
                                        break;
                                    case 5:
                                        row.締日消費税 = Math.Floor((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 6:
                                        row.締日消費税 = Math.Ceiling((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 7:
                                        row.締日消費税 = Math.Round((decimal)(row.締日課税売上 * row.消費税率) / 100, MidpointRounding.AwayFromZero);
                                        break;
                                }
                            }
                            queryG2.Add(row);
                        }


                        //消費税率ごとに集計
                        var queryG3 = (from q in queryG2
                                       //group q by new { q.得意先KEY, q.得意先ID, q.消費税適用日, q.Ｔ税区分ID, q.消費税率, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID, q.締日入金現金, q.締日入金手形, q.締日入金その他 } into qGroup
                                       group q by new { q.得意先KEY, q.得意先ID, q.消費税適用日, q.Ｔ税区分ID, q.消費税率, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID } into qGroup
                                       select new WORK_MEISAI
                                      {
                                          得意先KEY = qGroup.Key.得意先KEY,
                                          得意先ID = qGroup.Key.得意先ID,
                                          親子区分ID = qGroup.Key.親子区分ID,
                                          親ID = qGroup.Key.親ID,
                                          締日前月残高 = qGroup.Key.締日前月残高,
                                          締集計開始日 = qGroup.Key.締集計開始日,
                                          締集計終了日 = qGroup.Key.締集計終了日,
                                          Ｔ税区分ID = qGroup.Key.Ｔ税区分ID,
                                          消費税適用日 = qGroup.Key.消費税適用日,
                                          消費税率 = qGroup.Key.消費税率,
                                          締日課税売上 = qGroup.Select(c => c.締日課税売上).Sum(),
                                          締日消費税 = qGroup.Select(c => c.締日消費税).Sum(),
                                          締日通行料 = qGroup.Select(c => c.締日通行料).Sum(),
                                          締日内傭車売上 = qGroup.Select(c => c.締日内傭車売上).Sum(),
                                          締日内傭車料 = qGroup.Select(c => c.締日内傭車料).Sum(),
                                          締日売上金額 = qGroup.Select(c => c.締日売上金額).Sum(),
                                          締日非課税売上 = qGroup.Select(c => c.締日非課税売上).Sum(),
                                          締日入金現金 = qGroup.Select(c => c.締日入金現金).Sum(),
                                          締日入金手形 = qGroup.Select(c => c.締日入金手形).Sum(),
										  締日入金その他 = qGroup.Select(c => c.締日入金その他).Sum(),
										  締日件数 = qGroup.Select(c => c.締日件数).Sum(),
										  締日未定件数 = qGroup.Select(c => c.締日未定件数).Sum(),
                                      }).AsQueryable();
                        //税率ごとに消費税計算
                        List<WORK_MEISAI> queryG4 = new List<WORK_MEISAI>();
                        foreach (WORK_MEISAI row in queryG3)
                        {
                            if (row.消費税率 != null)
                            {
                                switch (row.Ｔ税区分ID)
                                {
                                    case 1:
                                        row.締日消費税 = Math.Floor((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 2:
                                        row.締日消費税 = Math.Ceiling((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 3:
                                        row.締日消費税 = Math.Round((decimal)(row.締日課税売上 * row.消費税率) / 100, MidpointRounding.AwayFromZero);
                                        break;
                                }
                            }
                            queryG4.Add(row);
                        }

                        //親子までの集計データ
                        var queryG5 = (from q in queryG4
                                       //group q by new { q.得意先KEY, q.得意先ID, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID, q.締日入金現金, q.締日入金手形, q.締日入金その他 } into qGroup
                                       group q by new { q.得意先KEY, q.得意先ID, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID } into qGroup
                                       select new WORK_MEISAI
                                      {
                                          得意先KEY = qGroup.Key.得意先KEY,
                                          得意先ID = qGroup.Key.得意先ID,
                                          親子区分ID = qGroup.Key.親子区分ID,
                                          親ID = qGroup.Key.親ID,
                                          締日前月残高 = qGroup.Key.締日前月残高,
                                          締集計開始日 = qGroup.Key.締集計開始日,
                                          締集計終了日 = qGroup.Key.締集計終了日,
                                          締日課税売上 = qGroup.Select(c => c.締日課税売上).Sum(),
                                          締日消費税 = qGroup.Select(c => c.締日消費税).Sum(),
                                          締日通行料 = qGroup.Select(c => c.締日通行料).Sum(),
                                          締日内傭車売上 = qGroup.Select(c => c.締日内傭車売上).Sum(),
                                          締日内傭車料 = qGroup.Select(c => c.締日内傭車料).Sum(),
                                          締日売上金額 = qGroup.Select(c => c.締日売上金額).Sum(),
                                          締日非課税売上 = qGroup.Select(c => c.締日非課税売上).Sum(),
                                          締日入金現金 = qGroup.Select(c => c.締日入金現金).Sum(),
                                          締日入金手形 = qGroup.Select(c => c.締日入金手形).Sum(),
										  締日入金その他 = qGroup.Select(c => c.締日入金その他).Sum(),
										  締日件数 = qGroup.Select(c => c.締日件数).Sum(),
										  締日未定件数 = qGroup.Select(c => c.締日未定件数).Sum(),
                                      }).ToList();


                        ////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //親子区分計算　


                        var queryGOYA = (from q in queryG4.Where(c => c.親子区分ID == 1 || c.親子区分ID == 2)
                                           select q).ToList();

                        queryGOYA = queryGOYA.Union(from q in queryG4.Where(c => c.親子区分ID == 3)
                                                    join q2 in
                                                            (from oya in queryGOYA
                                                       group oya by new { oya.得意先KEY, oya.得意先ID, oya.親子区分ID, oya.親ID, oya.締日前月残高, oya.Ｔ税区分ID } into oyaGroup
                                                       select new { oyaGroup.Key.得意先KEY, oyaGroup.Key.得意先ID, oyaGroup.Key.親子区分ID, oyaGroup.Key.親ID, oyaGroup.Key.締日前月残高, oyaGroup.Key.Ｔ税区分ID }) on q.親ID equals q2.得意先KEY
                                                        select new WORK_MEISAI
                                                        {
                                                            得意先KEY = q2.得意先KEY,
                                                            得意先ID = q2.得意先ID,
                                                            親子区分ID = q2.親子区分ID,
                                                            親ID = q2.親ID,
                                                            締日前月残高 = q2.締日前月残高,
                                                            締集計開始日 = q.締集計開始日,
                                                            締集計終了日 = q.締集計終了日,
                                                            Ｔ税区分ID = q2.Ｔ税区分ID,
                                                            消費税適用日 = q.消費税適用日,
                                                            消費税率 = q.消費税率,
                                                            締日課税売上 = q.締日課税売上,
                                                            締日消費税 = q.締日消費税,
                                                            締日通行料 = q.締日通行料,
                                                            締日内傭車売上 = q.締日内傭車売上,
                                                            締日内傭車料 = q.締日内傭車料,
                                                            締日売上金額 = q.締日売上金額,
                                                            締日非課税売上 = q.締日非課税売上,
                                                            締日入金現金 = q.締日入金現金,
                                                            締日入金手形 = q.締日入金手形,
															締日入金その他 = q.締日入金その他,
															締日件数 = q.締日件数,
															締日未定件数 = q.締日未定件数,
                                                        }).ToList();


                        queryGOYA = (from q in queryGOYA
                                       //group q by new { q.得意先KEY, q.得意先ID, q.消費税適用日, q.Ｔ税区分ID, q.消費税率, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID, q.締日入金現金, q.締日入金手形, q.締日入金その他 } into qGroup
                                       group q by new { q.得意先KEY, q.得意先ID, q.消費税適用日, q.Ｔ税区分ID, q.消費税率, q.締日前月残高, q.親子区分ID, q.親ID } into qGroup
                                       select new WORK_MEISAI
                                       {
                                           得意先KEY = qGroup.Key.得意先KEY,
                                           得意先ID = qGroup.Key.得意先ID,
                                           親子区分ID = qGroup.Key.親子区分ID,
                                           親ID = qGroup.Key.親ID,
										   締日前月残高 = qGroup.Key.締日前月残高,
										   締集計開始日 = queryG4.Where(c => c.得意先KEY == qGroup.Key.得意先KEY).Select(c => c.締集計開始日).FirstOrDefault(),
										   締集計終了日 = queryG4.Where(c => c.得意先KEY == qGroup.Key.得意先KEY).Select(c => c.締集計終了日).FirstOrDefault(),
										   //締集計開始日 = qGroup.Key.締集計開始日,
										   //締集計終了日 = qGroup.Key.締集計終了日,
                                           Ｔ税区分ID = qGroup.Key.Ｔ税区分ID,
                                           消費税適用日 = qGroup.Key.消費税適用日,
                                           消費税率 = qGroup.Key.消費税率,
                                           締日課税売上 = qGroup.Select(c => c.締日課税売上).Sum(),
                                           締日消費税 = qGroup.Select(c => c.締日消費税).Sum(),
                                           締日通行料 = qGroup.Select(c => c.締日通行料).Sum(),
                                           締日内傭車売上 = qGroup.Select(c => c.締日内傭車売上).Sum(),
                                           締日内傭車料 = qGroup.Select(c => c.締日内傭車料).Sum(),
                                           締日売上金額 = qGroup.Select(c => c.締日売上金額).Sum(),
                                           締日非課税売上 = qGroup.Select(c => c.締日非課税売上).Sum(),
                                           締日入金現金 = qGroup.Select(c => c.締日入金現金).Sum(),
                                           締日入金手形 = qGroup.Select(c => c.締日入金手形).Sum(),
										   締日入金その他 = qGroup.Select(c => c.締日入金その他).Sum(),
										   締日件数 = qGroup.Select(c => c.締日件数).Sum(),
										   締日未定件数 = qGroup.Select(c => c.締日未定件数).Sum(),

                                       }).ToList();



                        //親子区分計算 税一括計算
                        List<WORK_MEISAI> queryGOYA2 = new List<WORK_MEISAI>();
                        foreach (WORK_MEISAI row in queryGOYA)
                        {
                            if (row.親子区分ID == 1)
                            {
                                switch (row.Ｔ税区分ID)
                                {
                                    case 1:
                                        row.締日消費税 = row.消費税率 == null ? 0 : Math.Floor((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 2:
                                        row.締日消費税 = row.消費税率 == null ? 0 : Math.Ceiling((decimal)(row.締日課税売上 * row.消費税率) / 100);
                                        break;
                                    case 3:
                                        row.締日消費税 = row.消費税率 == null ? 0 : Math.Round((decimal)(row.締日課税売上 * row.消費税率) / 100, MidpointRounding.AwayFromZero);
                                        break;
                                }
                            }
                            queryGOYA2.Add(row);
                        }

                        //親子区分計算
                        if (queryGOYA2 != null)
                        {
                            queryGOYA2 = (from q in queryGOYA2
                                          //group q by new { q.得意先KEY, q.得意先ID, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID, q.締日入金現金, q.締日入金手形, q.締日入金その他 } into qGroup
                                          group q by new { q.得意先KEY, q.得意先ID, q.締集計開始日, q.締集計終了日, q.締日前月残高, q.親子区分ID, q.親ID } into qGroup
                                          select new WORK_MEISAI
                                          {
                                              得意先KEY = qGroup.Key.得意先KEY,
                                              得意先ID = qGroup.Key.得意先ID,
                                              親子区分ID = qGroup.Key.親子区分ID,
                                              親ID = qGroup.Key.親ID,
                                              締日前月残高 = qGroup.Key.締日前月残高,
                                              締集計開始日 = qGroup.Key.締集計開始日,
                                              締集計終了日 = qGroup.Key.締集計終了日,
                                              締日課税売上 = qGroup.Select(c => c.締日課税売上).Sum(),
                                              締日消費税 = qGroup.Select(c => c.締日消費税).Sum(),
                                              締日通行料 = qGroup.Select(c => c.締日通行料).Sum(),
                                              締日内傭車売上 = qGroup.Select(c => c.締日内傭車売上).Sum(),
                                              締日内傭車料 = qGroup.Select(c => c.締日内傭車料).Sum(),
                                              締日売上金額 = qGroup.Select(c => c.締日売上金額).Sum(),
                                              締日非課税売上 = qGroup.Select(c => c.締日非課税売上).Sum(),
                                              締日入金現金 = qGroup.Select(c => c.締日入金現金).Sum(),
                                              締日入金手形 = qGroup.Select(c => c.締日入金手形).Sum(),
											  締日入金その他 = qGroup.Select(c => c.締日入金その他).Sum(),
											  締日件数 = qGroup.Select(c => c.締日件数).Sum(),
											  締日未定件数 = qGroup.Select(c => c.締日未定件数).Sum(),
                                          }).ToList();

                            //親とそれ以外を結合

                            queryG5 = (queryG5.Where(c => c.親子区分ID != 1 && c.親子区分ID != 2)).ToList();
                            queryGOYA2 = queryGOYA2.Union(queryG5.Where(c => c.親子区分ID != 1 && c.親子区分ID != 2)).ToList();
                        }


                        var queryGOYA3 = (from tok in p得意先List
                                        join q in queryGOYA2 on tok.得意先KEY equals q.得意先KEY into qGroup
                                        from qG in qGroup.DefaultIfEmpty()

										  //join trn in context.V_TRN明細消費税付.Where(c => (c.入力区分 == 1 && c.社内区分 == 0) || (c.入力区分 == 3 && c.明細行 == 1 && c.社内区分 == 0))
										  //		  on tok.得意先KEY equals trn == null ? null : trn.得意先KEY into trnGroup
										  ////from trng in trnGroup
										  ////where trng != null

										  //join trn2 in context.V_TRN明細消費税付.Where(c => c.売上未定区分 == 1 && ((c.入力区分 == 1 && c.社内区分 == 0) || (c.入力区分 == 3 && c.明細行 == 1 && c.社内区分 == 0)))
										  //		  on tok.得意先KEY equals trn2 == null ? null : trn2.得意先KEY into trn2Group
                                          //where trn2g != null
                                         select new WORK_MEISAI
                                        {
                                            得意先KEY = tok.得意先KEY,
                                            得意先ID = tok.得意先ID,
                                            親子区分ID = tok.親子区分ID,
                                            親ID = tok.親ID,
                                            締日前月残高 = qG == null ? 0 : qG.締日前月残高,
                                            締集計開始日 = tok == null ? null : tok.開始月次日付,
                                            締集計終了日 = tok == null ? null : tok.終了月次日付,
                                            締日課税売上 = qG == null ? 0 : qG.締日課税売上,
                                            締日消費税 = qG == null ? 0 : qG.締日消費税,
                                            締日通行料 = qG == null ? 0 : qG.締日通行料,
                                            締日内傭車売上 = qG == null ? 0 : qG.締日内傭車売上,
                                            締日内傭車料 = qG == null ? 0 : qG.締日内傭車料,
                                            締日売上金額 = qG == null ? 0 : qG.締日売上金額,
                                            締日非課税売上 = qG == null ? 0 : qG.締日非課税売上,
											締日件数 = qG == null ? 0 : qG.締日件数,
											締日未定件数 = qG == null ? 0 : qG.締日未定件数,
											//締日件数 = trnGroup.Where(c => c.請求日付 >= tok.開始月次日付 && c.請求日付 <= tok.終了月次日付).Count() == null ? 0 : trnGroup.Where(c => c.請求日付 >= tok.開始月次日付 && c.請求日付 <= tok.終了月次日付).Count(),
											//締日未定件数 = trn2Group.Where(c => c.請求日付 >= tok.開始月次日付 && c.請求日付 <= tok.終了月次日付).Count() == null ? 0 : trn2Group.Where(c => c.請求日付 >= tok.開始月次日付 && c.請求日付 <= tok.終了月次日付).Count(),
                                            締日入金現金 = qG.締日入金現金,
                                            締日入金手形 = qG.締日入金手形,
                                            締日入金その他 = qG.締日入金その他,

                                        }).ToList();


                        //前残算出

                        var queryG6 = (from q in queryGOYA3
                                       from tok in context.M01_TOK.Where(c => q.得意先KEY == c.得意先KEY).DefaultIfEmpty()
                                       from s11 in context.S11_TOKG.Where(c => c.得意先KEY == q.得意先KEY && c.集計年月 < p作成年月 && c.集計年月 >= i期首年月).OrderByDescending(c => c.集計年月).ThenByDescending(c => c.回数).Take(1).DefaultIfEmpty()

                                           //join s01 in context.S11_TOKG.Where(c => c.集計年月 < p作成年月 && c.集計年月 >= i期首年月).OrderByDescending(c => c.集計年月).OrderByDescending(c => c.回数) on q.得意先KEY equals s01.得意先KEY into s01Group
                                      select new WORK_MEISAI
                                      {
                                          得意先KEY = q.得意先KEY,
                                          得意先ID = q.得意先ID,
                                          締日課税売上 = q.締日課税売上,
                                          締日消費税 = q.締日消費税,
                                          締日通行料 = q.締日通行料,
                                          締日内傭車売上 = q.締日内傭車売上,
                                          締日内傭車料 = q.締日内傭車料,
                                          締日売上金額 = q.締日売上金額,
                                          締日非課税売上 = q.締日非課税売上,
                                          締集計開始日 = q.締集計開始日,
                                          締集計終了日 = q.締集計終了日,

                                          締日前月残高 = s11 == null ? tok.Ｔ月次期首残 : s11.月次前月残高 - s11.月次入金現金 - s11.月次入金手形 - s11.月次入金その他 + s11.月次売上金額 + s11.月次通行料 + s11.月次消費税,
                                          //締日前月残高 = s01Group.Take(1).Select(c => c.月次前月残高).Sum() + s01Group.Take(1).Select(c => c.月次売上金額).Sum() + s01Group.Take(1).Select(c => c.月次通行料).Sum() + s01Group.Take(1).Select(c => c.月次消費税).Sum()
                                          //              - s01Group.Take(1).Select(c => c.月次入金現金).Sum() - s01Group.Take(1).Select(c => c.月次入金手形).Sum() - s01Group.Take(1).Select(c => c.月次入金その他).Sum(),

                                          締日件数 = q.締日件数,
                                          締日未定件数 = q.締日未定件数,
                                          締日入金現金 = q.締日入金現金,
                                          締日入金手形 = q.締日入金手形,
                                          締日入金その他 = q.締日入金その他,
                                      }).ToList();




                        //削除行を特定
                        lst = (from q in p得意先List select q.得意先KEY).ToArray();

                        var retG = from x in context.S11_TOKG
                                  where x.集計年月 == p作成年月 && lst.Contains(x.得意先KEY)
                                  select x;

                        foreach (var r in retG)
                        {
                            context.DeleteObject(r);
                        }
                        context.SaveChanges();


						do { }
						while ((from x in context.S11_TOKG
								where x.集計年月 == p作成年月 && lst.Contains(x.得意先KEY)
								select x).Count() != 0);


						//sqlbulukcopy準備
						DataTable dtg = new DataTable("S11_TOKG");
						dtg.Columns.Add("得意先KEY", Type.GetType("System.Int32"));
						dtg.Columns.Add("集計年月", Type.GetType("System.Int32"));
						dtg.Columns.Add("回数", Type.GetType("System.Int32"));
						dtg.Columns.Add("登録日時", Type.GetType("System.DateTime"));
						dtg.Columns.Add("更新日時", Type.GetType("System.DateTime"));
						dtg.Columns.Add("締集計開始日", Type.GetType("System.DateTime"));
						dtg.Columns.Add("締集計終了日", Type.GetType("System.DateTime"));
						dtg.Columns.Add("締日前月残高", Type.GetType("System.Decimal"));
						dtg.Columns.Add("締日入金現金", Type.GetType("System.Decimal"));
						dtg.Columns.Add("締日入金手形", Type.GetType("System.Decimal"));
						dtg.Columns.Add("締日入金その他", Type.GetType("System.Decimal"));
						dtg.Columns.Add("締日売上金額", Type.GetType("System.Decimal"));
						dtg.Columns.Add("締日通行料", Type.GetType("System.Decimal"));
						dtg.Columns.Add("締日課税売上", Type.GetType("System.Decimal"));
						dtg.Columns.Add("締日非課税売上", Type.GetType("System.Decimal"));
						dtg.Columns.Add("締日消費税", Type.GetType("System.Decimal"));
						dtg.Columns.Add("締日内傭車売上", Type.GetType("System.Decimal"));
						dtg.Columns.Add("締日内傭車料", Type.GetType("System.Decimal"));
						dtg.Columns.Add("締日未定件数", Type.GetType("System.Decimal"));
						dtg.Columns.Add("締日件数", Type.GetType("System.Decimal"));
						dtg.Columns.Add("締日", Type.GetType("System.Int32"));
						foreach (var r in queryG6)
						{
							DataRow dr = dtg.NewRow();
							dr[0] = r.得意先KEY;
							dr[1] = (int)p作成年月;
							dr[2] = 1;
							dr[3] = DateTime.Now;
							dr[4] = DateTime.Now;
							dr[5] = (object)r.締集計開始日 ?? DBNull.Value;
							dr[6] = (object)r.締集計終了日 ?? DBNull.Value;
							dr[7] = r.締日前月残高;
							dr[8] = r.締日入金現金;
							dr[9] = r.締日入金手形;
							dr[10] = r.締日入金その他;
							dr[11] = r.締日売上金額;
							dr[12] = r.締日通行料;
							dr[13] = r.締日課税売上;
							dr[14] = r.締日非課税売上;
							dr[15] = r.締日消費税;
							dr[16] = r.締日内傭車売上;
							dr[17] = r.締日内傭車料;
							dr[18] = r.締日未定件数;
							dr[19] = r.締日件数;
							dr[20] = r.締日;
							dtg.Rows.Add(dr);
						}


						try //SQL_BULK_COPY書込み
						{
							var connect = CommonData.TRAC3_SQL_GetConnectionString();
							using (var bulkCopy = new SqlBulkCopy(connect))
							{
								bulkCopy.DestinationTableName = dtg.TableName; // テーブル名をSqlBulkCopyに教える
								bulkCopy.WriteToServer(dtg); // bulkCopy実行
							}
						}
						catch (Exception e)
						{
						}


						////データ書き込み
						//foreach (var r in queryG6)
						//{
						//	S11_TOKG s11 = new S11_TOKG();
						//	s11.得意先KEY = r.得意先KEY;
						//	s11.集計年月 = (int)p作成年月;
						//	s11.回数 = 1;
						//	s11.更新日時 = DateTime.Now;
						//	s11.月集計開始日 = r.締集計開始日;
						//	s11.月集計終了日 = r.締集計終了日;
						//	s11.締日 = r.締日;
						//	s11.月次課税売上 = r.締日課税売上;
						//	s11.月次件数 = r.締日件数;
						//	s11.月次消費税 = r.締日消費税;
						//	s11.月次前月残高 = r.締日前月残高;
						//	s11.月次通行料 = r.締日通行料;
						//	s11.月次内傭車売上 = r.締日内傭車売上;
						//	s11.月次内傭車料 = r.締日内傭車料;
						//	s11.月次入金その他 = r.締日入金その他;
						//	s11.月次入金現金 = r.締日入金現金;
						//	s11.月次入金手形 = r.締日入金手形;
						//	s11.月次売上金額 = r.締日売上金額;
						//	s11.月次非課税売上 = r.締日非課税売上;
						//	s11.月次未定件数 = r.締日未定件数;
						//	s11.月次件数 = r.締日件数;
						//	s11.登録日時 = DateTime.Now;
						//	context.S11_TOKG.ApplyChanges(s11);
						//}
						//context.SaveChanges();

						//transaction.Commit();

                        #endregion



                        #endregion


                        var result = query.ToList();
                        return 1;

					//}
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



		static string FillSpaceChar(string col)
		{
			return string.IsNullOrWhiteSpace(col) ? string.Empty : col;
		}


        /// <summary>　中村作成
        /// 年、月、締日で開始日付、終了日付を返す
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        static DateFromTo GetDateFromTo(int iYear, int iMonth, int iSime)
        {
            var ret = new DateFromTo();

            try
            {
                if (iYear < 2 || (iMonth < 1 || iMonth > 12) || (iSime < 1 || iSime > 31))
                {
                    ret.Result = false;
                    return ret;
                }

                if (iSime < 31)
                {

                    //string Date = iYear.ToString() + "/" + iMonth.ToString() + "/" + 01;
                    DateTime dYMD = Convert.ToDateTime(iYear.ToString() + "/" + iMonth.ToString() + "/" + 01);
                    dYMD = dYMD.AddDays(-1);
                    int iSimechk = dYMD.Day;
                    if (iSimechk < iSime + 1)
                    {
                        dYMD = dYMD.AddDays(1);
                        ret.DATEFrom = dYMD;
                    }
                    else
                    {
                        ret.DATEFrom = Convert.ToDateTime(dYMD.Year.ToString() + "/" + dYMD.Month.ToString() + "/" + (iSime + 1).ToString());
                    }

                    dYMD = Convert.ToDateTime(iYear.ToString() + "/" + iMonth.ToString() + "/" + 01);
                    dYMD = dYMD.AddMonths(1).AddDays(-1);
                    iSimechk = dYMD.Day;
                    if (iSimechk < iSime)
                    {
                        ret.DATETo = dYMD;
                    }
                    else
                    {
                        ret.DATETo = Convert.ToDateTime(iYear.ToString() + "/" + iMonth.ToString() + "/" + iSime.ToString());
                    }
                    TimeSpan span = ret.DATETo - ret.DATEFrom;
                    ret.Kikan = span.Days;
                    ret.Result = true;
                }
                else if (iSime == 31)
                {
                    DateTime dYMD = Convert.ToDateTime(iYear.ToString() + "/" + iMonth.ToString() + "/" + 01);
                    ret.DATEFrom = dYMD;
                    ret.DATETo = dYMD.AddMonths(1).AddDays(-1);
                    ret.Result = true;

                }
                else
                {
                    ret.Result = false;
                }

                return ret;
            }

            catch (Exception ex)
            {
                ret.Result = false;
                return ret;
            }
        }

        /// <summary>　中村作成
        /// 開始、終了日付から、日のリストを返す。
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        static DateList GetDateList(DateTime dDateFrom, DateTime dDateTo)
        {
            var ret = new DateList();

            try
            {
                if (dDateFrom > dDateTo)
                {
                    ret.Result = false;
                    return ret;
                }

                int cnt = 0;
                for (DateTime dDate = dDateFrom; dDate <= dDateTo; cnt += 1)
                {
                    ret.dDATEList[cnt] = dDate;
                    dDate = dDate.AddDays(1);
                }
                TimeSpan span = dDateTo - dDateFrom;
                ret.Kikan = span.Days;
                ret.Result = true;
                return ret;
            }

            catch (Exception ex)
            {
                ret.Result = false;
                return ret;
            }
        }

	}
}