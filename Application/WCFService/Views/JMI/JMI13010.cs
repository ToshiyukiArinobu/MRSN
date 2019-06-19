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
    public class JMI13010g_Member
    {
        [DataMember]
        public int 乗務員KEY { get; set; }
        [DataMember]
        public int 乗務員ID { get; set; }
        [DataMember]
        public int 車輌KEY { get; set; }
        [DataMember]
        public int 車輌ID { get; set; }
        [DataMember]
        public int? 車種ID { get; set; }
        [DataMember]
        public string 車輌番号 { get; set; }
        [DataMember]
        public string 主乗務員 { get; set; }
        [DataMember]
        public string 車種 { get; set; }
		[DataMember]
		public DateTime? 退職年月日 { get; set; }
        [DataMember]
        public decimal? 運送収入1 { get; set; }
        public decimal? 運送収入2 { get; set; }
        public decimal? 運送収入3 { get; set; }
        public decimal? 運送収入4 { get; set; }
        public decimal? 運送収入5 { get; set; }
        public decimal? 運送収入6 { get; set; }
        public decimal? 運送収入7 { get; set; }
        public decimal? 運送収入8 { get; set; }
        public decimal? 運送収入9 { get; set; }
        public decimal? 運送収入10 { get; set; }
        public decimal? 運送収入11 { get; set; }
        public decimal? 運送収入12 { get; set; }
        public decimal? 運送収入13 { get; set; }
        public decimal? 運送収入14 { get; set; }
        public decimal? 運送収入15 { get; set; }

        [DataMember]
        public decimal? 限界利益1 { get; set; }
        [DataMember]
        public decimal? 小計1_1 { get; set; }
        [DataMember]
        public decimal? 小計2_1 { get; set; }
        [DataMember]
        public decimal? 小計3_1 { get; set; }
        [DataMember]
        public decimal? 車輌直接費1 { get; set; }
        [DataMember]
        public decimal? 車輌直接益1 { get; set; }
        [DataMember]
        public decimal? 一般管理費1 { get; set; }
        [DataMember]
        public decimal? 当月利益1 { get; set; }

        [DataMember]
        public decimal? 限界利益2 { get; set; }
        [DataMember]
        public decimal? 小計1_2 { get; set; }
        [DataMember]
        public decimal? 小計2_2 { get; set; }
        [DataMember]
        public decimal? 小計3_2 { get; set; }
        [DataMember]
        public decimal? 車輌直接費2 { get; set; }
        [DataMember]
        public decimal? 車輌直接益2 { get; set; }
        [DataMember]
        public decimal? 一般管理費2 { get; set; }
        [DataMember]
        public decimal? 当月利益2 { get; set; }

        [DataMember]
        public decimal? 限界利益3 { get; set; }
        [DataMember]
        public decimal? 小計1_3 { get; set; }
        [DataMember]
        public decimal? 小計2_3 { get; set; }
        [DataMember]
        public decimal? 小計3_3 { get; set; }
        [DataMember]
        public decimal? 車輌直接費3 { get; set; }
        [DataMember]
        public decimal? 車輌直接益3 { get; set; }
        [DataMember]
        public decimal? 一般管理費3 { get; set; }
        [DataMember]
        public decimal? 当月利益3 { get; set; }

        [DataMember]
        public decimal? 限界利益4 { get; set; }
        [DataMember]
        public decimal? 小計1_4 { get; set; }
        [DataMember]
        public decimal? 小計2_4 { get; set; }
        [DataMember]
        public decimal? 小計3_4 { get; set; }
        [DataMember]
        public decimal? 車輌直接費4 { get; set; }
        [DataMember]
        public decimal? 車輌直接益4 { get; set; }
        [DataMember]
        public decimal? 一般管理費4 { get; set; }
        [DataMember]
        public decimal? 当月利益4 { get; set; }

        [DataMember]
        public decimal? 限界利益5 { get; set; }
        [DataMember]
        public decimal? 小計1_5 { get; set; }
        [DataMember]
        public decimal? 小計2_5 { get; set; }
        [DataMember]
        public decimal? 小計3_5 { get; set; }
        [DataMember]
        public decimal? 車輌直接費5 { get; set; }
        [DataMember]
        public decimal? 車輌直接益5 { get; set; }
        [DataMember]
        public decimal? 一般管理費5 { get; set; }
        [DataMember]
        public decimal? 当月利益5 { get; set; }

        [DataMember]
        public decimal? 限界利益6 { get; set; }
        [DataMember]
        public decimal? 小計1_6 { get; set; }
        [DataMember]
        public decimal? 小計2_6 { get; set; }
        [DataMember]
        public decimal? 小計3_6 { get; set; }
        [DataMember]
        public decimal? 車輌直接費6 { get; set; }
        [DataMember]
        public decimal? 車輌直接益6 { get; set; }
        [DataMember]
        public decimal? 一般管理費6 { get; set; }
        [DataMember]
        public decimal? 当月利益6 { get; set; }

        [DataMember]
        public decimal? 限界利益7 { get; set; }
        [DataMember]
        public decimal? 小計1_7 { get; set; }
        [DataMember]
        public decimal? 小計2_7 { get; set; }
        [DataMember]
        public decimal? 小計3_7 { get; set; }
        [DataMember]
        public decimal? 車輌直接費7 { get; set; }
        [DataMember]
        public decimal? 車輌直接益7 { get; set; }
        [DataMember]
        public decimal? 一般管理費7 { get; set; }
        [DataMember]
        public decimal? 当月利益7 { get; set; }

        [DataMember]
        public decimal? 限界利益8 { get; set; }
        [DataMember]
        public decimal? 小計1_8 { get; set; }
        [DataMember]
        public decimal? 小計2_8 { get; set; }
        [DataMember]
        public decimal? 小計3_8 { get; set; }
        [DataMember]
        public decimal? 車輌直接費8 { get; set; }
        [DataMember]
        public decimal? 車輌直接益8 { get; set; }
        [DataMember]
        public decimal? 一般管理費8 { get; set; }
        [DataMember]
        public decimal? 当月利益8 { get; set; }

        [DataMember]
        public decimal? 限界利益9 { get; set; }
        [DataMember]
        public decimal? 小計1_9 { get; set; }
        [DataMember]
        public decimal? 小計2_9 { get; set; }
        [DataMember]
        public decimal? 小計3_9 { get; set; }
        [DataMember]
        public decimal? 車輌直接費9 { get; set; }
        [DataMember]
        public decimal? 車輌直接益9 { get; set; }
        [DataMember]
        public decimal? 一般管理費9 { get; set; }
        [DataMember]
        public decimal? 当月利益9 { get; set; }

        [DataMember]
        public decimal? 限界利益10 { get; set; }
        [DataMember]
        public decimal? 小計1_10 { get; set; }
        [DataMember]
        public decimal? 小計2_10 { get; set; }
        [DataMember]
        public decimal? 小計3_10 { get; set; }
        [DataMember]
        public decimal? 車輌直接費10 { get; set; }
        [DataMember]
        public decimal? 車輌直接益10 { get; set; }
        [DataMember]
        public decimal? 一般管理費10 { get; set; }
        [DataMember]
        public decimal? 当月利益10 { get; set; }

        [DataMember]
        public decimal? 限界利益11 { get; set; }
        [DataMember]
        public decimal? 小計1_11 { get; set; }
        [DataMember]
        public decimal? 小計2_11 { get; set; }
        [DataMember]
        public decimal? 小計3_11 { get; set; }
        [DataMember]
        public decimal? 車輌直接費11 { get; set; }
        [DataMember]
        public decimal? 車輌直接益11 { get; set; }
        [DataMember]
        public decimal? 一般管理費11 { get; set; }
        [DataMember]
        public decimal? 当月利益11 { get; set; }

        [DataMember]
        public decimal? 限界利益12 { get; set; }
        [DataMember]
        public decimal? 小計1_12 { get; set; }
        [DataMember]
        public decimal? 小計2_12 { get; set; }
        [DataMember]
        public decimal? 小計3_12 { get; set; }
        [DataMember]
        public decimal? 車輌直接費12 { get; set; }
        [DataMember]
        public decimal? 車輌直接益12 { get; set; }
        [DataMember]
        public decimal? 一般管理費12 { get; set; }
        [DataMember]
        public decimal? 当月利益12 { get; set; }

        [DataMember]
        public decimal? 限界利益13 { get; set; }
        [DataMember]
        public decimal? 小計1_13 { get; set; }
        [DataMember]
        public decimal? 小計2_13 { get; set; }
        [DataMember]
        public decimal? 小計3_13 { get; set; }
        [DataMember]
        public decimal? 車輌直接費13 { get; set; }
        [DataMember]
        public decimal? 車輌直接益13 { get; set; }
        [DataMember]
        public decimal? 一般管理費13 { get; set; }
        [DataMember]
        public decimal? 当月利益13 { get; set; }

        [DataMember]
        public decimal? 限界利益14 { get; set; }
        [DataMember]
        public decimal? 小計1_14 { get; set; }
        [DataMember]
        public decimal? 小計2_14 { get; set; }
        [DataMember]
        public decimal? 小計3_14 { get; set; }
        [DataMember]
        public decimal? 車輌直接費14 { get; set; }
        [DataMember]
        public decimal? 車輌直接益14 { get; set; }
        [DataMember]
        public decimal? 一般管理費14 { get; set; }
        [DataMember]
        public decimal? 当月利益14 { get; set; }

        [DataMember]
        public decimal? 限界利益15 { get; set; }
        [DataMember]
        public decimal? 小計1_15 { get; set; }
        [DataMember]
        public decimal? 小計2_15 { get; set; }
        [DataMember]
        public decimal? 小計3_15 { get; set; }
        [DataMember]
        public decimal? 車輌直接費15 { get; set; }
        [DataMember]
        public decimal? 車輌直接益15 { get; set; }
        [DataMember]
        public decimal? 一般管理費15 { get; set; }
        [DataMember]
        public decimal? 当月利益15 { get; set; }

    }

    public class JMI13010_KEI_Member
    {
        [DataMember]
        public int 印刷グループID { get; set; }
        [DataMember]
        public int 経費ID { get; set; }
        [DataMember]
		public string 経費項目名 { get; set; }
        [DataMember]
        public decimal 金額1 { get; set; }
        public decimal 金額2 { get; set; }
        public decimal 金額3 { get; set; }
        public decimal 金額4 { get; set; }
        public decimal 金額5 { get; set; }
        public decimal 金額6 { get; set; }
        public decimal 金額7 { get; set; }
        public decimal 金額8 { get; set; }
        public decimal 金額9 { get; set; }
        public decimal 金額10 { get; set; }
        public decimal 金額11 { get; set; }
        public decimal 金額12 { get; set; }
        public decimal 金額13 { get; set; }
        public decimal 金額14 { get; set; }
        public decimal 金額15 { get; set; }
    }

	public class JMI13010_PRELIST_Member
	{
		[DataMember]
		public int 印刷グループID { get; set; }
		[DataMember]
		public int 経費ID { get; set; }
		[DataMember]
		public string 経費項目名 { get; set; }
		[DataMember]
		public int 乗務員KEY { get; set; }
		[DataMember]
		public int 乗務員ID { get; set; }
		[DataMember]
		public int 車輌KEY { get; set; }
		[DataMember]
		public int 車輌ID { get; set; }
		[DataMember]
		public decimal 金額1 { get; set; }
		public decimal 金額2 { get; set; }
		public decimal 金額3 { get; set; }
		public decimal 金額4 { get; set; }
		public decimal 金額5 { get; set; }
		public decimal 金額6 { get; set; }
		public decimal 金額7 { get; set; }
		public decimal 金額8 { get; set; }
		public decimal 金額9 { get; set; }
		public decimal 金額10 { get; set; }
		public decimal 金額11 { get; set; }
		public decimal 金額12 { get; set; }
		public decimal 金額13 { get; set; }
		public decimal 金額14 { get; set; }
		public decimal 金額15 { get; set; }
	}

	public class JMI13010_PRELIST_Member_CSV
	{
		public string 経費項目名 { get; set; }
		public int 乗務員ID { get; set; }
		public string 乗務員名 { get; set; }
		public string 項目名 { get; set; }
		public decimal 金額1 { get; set; }
		public decimal 金額2 { get; set; }
		public decimal 金額3 { get; set; }
		public decimal 金額4 { get; set; }
		public decimal 金額5 { get; set; }
		public decimal 金額6 { get; set; }
		public decimal 金額7 { get; set; }
		public decimal 金額8 { get; set; }
		public decimal 金額9 { get; set; }
		public decimal 金額10 { get; set; }
		public decimal 金額11 { get; set; }
		public decimal 金額12 { get; set; }
		public decimal 年合計 { get; set; }
		public decimal 平均 { get; set; }
		public decimal 対売上 { get; set; }
		public string 月1 { get; set; }
		public string 月2 { get; set; }
		public string 月3 { get; set; }
		public string 月4 { get; set; }
		public string 月5 { get; set; }
		public string 月6 { get; set; }
		public string 月7 { get; set; }
		public string 月8 { get; set; }
		public string 月9 { get; set; }
		public string 月10 { get; set; }
		public string 月11 { get; set; }
		public string 月12 { get; set; }
	}


    #region JMI13010_Member

    public class JMI13010_Member
    {
        [DataMember]
        public int? 乗務員ID { get; set; }
		[DataMember]
		public DateTime? 退職年月日 { get; set; }
		[DataMember]
		public string 車輌番号 { get; set; }
		[DataMember]
        public string 主乗務員 { get; set; }
		[DataMember]
		public string 車種 { get; set; }

        [DataMember]
        public decimal? 運送収入1 { get; set; }
        [DataMember]
        public decimal? 運送収入2 { get; set; }
        [DataMember]
        public decimal? 運送収入3 { get; set; }
        [DataMember]
        public decimal? 運送収入4 { get; set; }
        [DataMember]
        public decimal? 運送収入5 { get; set; }
        [DataMember]
        public decimal? 運送収入6 { get; set; }
        [DataMember]
        public decimal? 運送収入7 { get; set; }
        [DataMember]
        public decimal? 運送収入8 { get; set; }
        [DataMember]
        public decimal? 運送収入9 { get; set; }
        [DataMember]
        public decimal? 運送収入10 { get; set; }
        [DataMember]
        public decimal? 運送収入11 { get; set; }
        [DataMember]
        public decimal? 運送収入12 { get; set; }
        [DataMember]
        public decimal? 運送収入13 { get; set; }
        [DataMember]
        public decimal? 運送収入14 { get; set; }
        [DataMember]
        public decimal? 運送収入15 { get; set; }

        [DataMember]
        public decimal? 車輌直接費1 { get; set; }
        [DataMember]
        public decimal? 車輌直接益1 { get; set; }
        [DataMember]
        public decimal? 一般管理費1 { get; set; }
        [DataMember]
        public decimal? 当月利益1 { get; set; }

        [DataMember]
        public decimal? 車輌直接費2 { get; set; }
        [DataMember]
        public decimal? 車輌直接益2 { get; set; }
        [DataMember]
        public decimal? 一般管理費2 { get; set; }
        [DataMember]
        public decimal? 当月利益2 { get; set; }

        [DataMember]
        public decimal? 車輌直接費3 { get; set; }
        [DataMember]
        public decimal? 車輌直接益3 { get; set; }
        [DataMember]
        public decimal? 一般管理費3 { get; set; }
        [DataMember]
        public decimal? 当月利益3 { get; set; }

        [DataMember]
        public decimal? 車輌直接費4 { get; set; }
        [DataMember]
        public decimal? 車輌直接益4 { get; set; }
        [DataMember]
        public decimal? 一般管理費4 { get; set; }
        [DataMember]
        public decimal? 当月利益4 { get; set; }

        [DataMember]
        public decimal? 車輌直接費5 { get; set; }
        [DataMember]
        public decimal? 車輌直接益5 { get; set; }
        [DataMember]
        public decimal? 一般管理費5 { get; set; }
        [DataMember]
        public decimal? 当月利益5 { get; set; }

        [DataMember]
        public decimal? 車輌直接費6 { get; set; }
        [DataMember]
        public decimal? 車輌直接益6 { get; set; }
        [DataMember]
        public decimal? 一般管理費6 { get; set; }
        [DataMember]
        public decimal? 当月利益6 { get; set; }

        [DataMember]
        public decimal? 車輌直接費7 { get; set; }
        [DataMember]
        public decimal? 車輌直接益7 { get; set; }
        [DataMember]
        public decimal? 一般管理費7 { get; set; }
        [DataMember]
        public decimal? 当月利益7 { get; set; }

        [DataMember]
        public decimal? 車輌直接費8 { get; set; }
        [DataMember]
        public decimal? 車輌直接益8 { get; set; }
        [DataMember]
        public decimal? 一般管理費8 { get; set; }
        [DataMember]
        public decimal? 当月利益8 { get; set; }

        [DataMember]
        public decimal? 車輌直接費9 { get; set; }
        [DataMember]
        public decimal? 車輌直接益9 { get; set; }
        [DataMember]
        public decimal? 一般管理費9 { get; set; }
        [DataMember]
        public decimal? 当月利益9 { get; set; }

        [DataMember]
        public decimal? 車輌直接費10 { get; set; }
        [DataMember]
        public decimal? 車輌直接益10 { get; set; }
        [DataMember]
        public decimal? 一般管理費10 { get; set; }
        [DataMember]
        public decimal? 当月利益10 { get; set; }

        [DataMember]
        public decimal? 車輌直接費11 { get; set; }
        [DataMember]
        public decimal? 車輌直接益11 { get; set; }
        [DataMember]
        public decimal? 一般管理費11 { get; set; }
        [DataMember]
        public decimal? 当月利益11 { get; set; }

        [DataMember]
        public decimal? 車輌直接費12 { get; set; }
        [DataMember]
        public decimal? 車輌直接益12 { get; set; }
        [DataMember]
        public decimal? 一般管理費12 { get; set; }
        [DataMember]
        public decimal? 当月利益12 { get; set; }

        [DataMember]
        public decimal? 車輌直接費13 { get; set; }
        [DataMember]
        public decimal? 車輌直接益13 { get; set; }
        [DataMember]
        public decimal? 一般管理費13 { get; set; }
        [DataMember]
        public decimal? 当月利益13 { get; set; }

        [DataMember]
        public decimal? 車輌直接費14 { get; set; }
        [DataMember]
        public decimal? 車輌直接益14 { get; set; }
        [DataMember]
        public decimal? 一般管理費14 { get; set; }
        [DataMember]
        public decimal? 当月利益14 { get; set; }

        [DataMember]
        public decimal? 車輌直接費15 { get; set; }
        [DataMember]
        public decimal? 車輌直接益15 { get; set; }
        [DataMember]
        public decimal? 一般管理費15 { get; set; }
        [DataMember]
        public decimal? 当月利益15 { get; set; }




        [DataMember]
        public int 印刷グループID { get; set; }

        [DataMember]
        public int 経費ID { get; set; }
        [DataMember]
        public string 経費項目名 { get; set; }

        [DataMember]
        public decimal? 金額1 { get; set; }
        [DataMember]
        public decimal? 金額2 { get; set; }
        [DataMember]
        public decimal? 金額3 { get; set; }
        [DataMember]
        public decimal? 金額4 { get; set; }
        [DataMember]
        public decimal? 金額5 { get; set; }
        [DataMember]
        public decimal? 金額6 { get; set; }
        [DataMember]
        public decimal? 金額7 { get; set; }
        [DataMember]
        public decimal? 金額8 { get; set; }
        [DataMember]
        public decimal? 金額9 { get; set; }
        [DataMember]
        public decimal? 金額10 { get; set; }
        [DataMember]
        public decimal? 金額11 { get; set; }
        [DataMember]
        public decimal? 金額12 { get; set; }
        [DataMember]
        public decimal? 金額13 { get; set; }
        [DataMember]
        public decimal? 金額14 { get; set; }
        [DataMember]
        public decimal? 金額15 { get; set; }

        [DataMember]
        public string 年 { get; set; }
        [DataMember]
        public string 月 { get; set; }
        [DataMember]
        public string 表示順序 { get; set; }
        [DataMember]
        public string コードFrom { get; set; }
        [DataMember]
        public string コードTo { get; set; }
        [DataMember]
        public string 乗務員ﾋﾟｯｸｱｯﾌﾟ { get; set; }
    }

    #endregion

    public class JMI13010
    {
        #region JMI13010 印刷
        public List<JMI13010_Member> JMI13010_GetDataList(string s乗務員From, string s乗務員To, int?[] i乗務員List, string s乗務員List, int 年, int 月)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                DateTime dDate = new DateTime( 年,月, 1);
                int i年月1, i年月2, i年月3, i年月4, i年月5, i年月6, i年月7, i年月8, i年月9, i年月10, i年月11, i年月12;
                i年月1 = dDate.Year * 100 + dDate.Month;
                dDate = dDate.AddMonths(1);
                i年月2 = dDate.Year * 100 + dDate.Month;
                dDate = dDate.AddMonths(1);
                i年月3 = dDate.Year * 100 + dDate.Month;
                dDate = dDate.AddMonths(1);
                i年月4 = dDate.Year * 100 + dDate.Month;
                dDate = dDate.AddMonths(1);
                i年月5 = dDate.Year * 100 + dDate.Month;
                dDate = dDate.AddMonths(1);
                i年月6 = dDate.Year * 100 + dDate.Month;
                dDate = dDate.AddMonths(1);
                i年月7 = dDate.Year * 100 + dDate.Month;
                dDate = dDate.AddMonths(1);
                i年月8 = dDate.Year * 100 + dDate.Month;
                dDate = dDate.AddMonths(1);
                i年月9 = dDate.Year * 100 + dDate.Month;
                dDate = dDate.AddMonths(1);
                i年月10 = dDate.Year * 100 + dDate.Month;
                dDate = dDate.AddMonths(1);
                i年月11 = dDate.Year * 100 + dDate.Month;
                dDate = dDate.AddMonths(1);
                i年月12 = dDate.Year * 100 + dDate.Month;
                int i開始年月 = i年月1;
                int i終了年月 = i年月12; 




                List<JMI13010_Member> retList = new List<JMI13010_Member>();

                context.Connection.Open();
                try
                {
                    string 乗務員ﾋﾟｯｸｱｯﾌﾟ指定 = string.Empty;

                    var carlistz = (from m04 in context.M04_DRV.Where(c => c.削除日付 == null)
                                   join s13z in context.S13_DRV.Where(c => c.集計年月 >= i開始年月 && c.集計年月 <= i終了年月) on m04.乗務員KEY equals s13z.乗務員KEY into s13zGroup
                                   //join s13 in context.S13_DRV.Where(c => c.集計年月 >= i開始年月 && c.集計年月 <= i終了年月) on m05.車輌KEY equals s13.車輌KEY
                                    join m05 in context.M05_CAR.Where(c => c.削除日付 == null) on m04.乗務員KEY equals m05.乗務員KEY into m05Group
                                    //from m05g in m05Group.DefaultIfEmpty()
                                    //join m06 in context.M06_SYA.Where(c => c.削除日付 == null) on m05Group.Select(c => c.車種ID).DefaultIfEmpty() equals m06.車種ID into m06Group
                                    //from m06g in m06Group
									orderby m04.乗務員ID
                                   select new JMI13010g_Member 
                                   {
                                       乗務員ID = m04.乗務員ID,
                                       乗務員KEY = m04.乗務員KEY,
                                       主乗務員 = m04.乗務員名,
									   退職年月日 = m04.退職年月日,
                                       車輌KEY = m05Group.Select(c => c.車輌KEY).FirstOrDefault(),
                                       車輌ID = m05Group.Select(c => c.車輌ID).FirstOrDefault(),
                                       車種ID = m05Group.Select(c => c.車種ID).FirstOrDefault(),
                                       車輌番号 = m05Group.Select(c => c.車輌番号).FirstOrDefault(),

                                       運送収入1 = s13zGroup.Where(c => c.集計年月 == i年月1).Select(c => c.運送収入).Sum(),
                                       運送収入2 = s13zGroup.Where(c => c.集計年月 == i年月2).Select(c => c.運送収入).Sum(),
                                       運送収入3 = s13zGroup.Where(c => c.集計年月 == i年月3).Select(c => c.運送収入).Sum(),
                                       運送収入4 = s13zGroup.Where(c => c.集計年月 == i年月4).Select(c => c.運送収入).Sum(),
                                       運送収入5 = s13zGroup.Where(c => c.集計年月 == i年月5).Select(c => c.運送収入).Sum(),
                                       運送収入6 = s13zGroup.Where(c => c.集計年月 == i年月6).Select(c => c.運送収入).Sum(),
                                       運送収入7 = s13zGroup.Where(c => c.集計年月 == i年月7).Select(c => c.運送収入).Sum(),
                                       運送収入8 = s13zGroup.Where(c => c.集計年月 == i年月8).Select(c => c.運送収入).Sum(),
                                       運送収入9 = s13zGroup.Where(c => c.集計年月 == i年月9).Select(c => c.運送収入).Sum(),
                                       運送収入10 = s13zGroup.Where(c => c.集計年月 == i年月10).Select(c => c.運送収入).Sum(),
                                       運送収入11 = s13zGroup.Where(c => c.集計年月 == i年月11).Select(c => c.運送収入).Sum(),
                                       運送収入12 = s13zGroup.Where(c => c.集計年月 == i年月12).Select(c => c.運送収入).Sum(),
                                       運送収入13 = s13zGroup.Where(c => c.集計年月 >= i年月1 && c.集計年月 <= i年月12).Select(c => c.運送収入).Sum(),
                                       運送収入14 = s13zGroup.Where(c => c.集計年月 >= i年月1 && c.集計年月 <= i年月12).Select(c => c.運送収入).Sum() / 12,
                                       運送収入15 = 0,

                                       一般管理費1 = s13zGroup.Where(c => c.集計年月 == i年月1).Select(c => c.一般管理費).Sum(),
                                       一般管理費2 = s13zGroup.Where(c => c.集計年月 == i年月2).Select(c => c.一般管理費).Sum(),
                                       一般管理費3 = s13zGroup.Where(c => c.集計年月 == i年月3).Select(c => c.一般管理費).Sum(),
                                       一般管理費4 = s13zGroup.Where(c => c.集計年月 == i年月4).Select(c => c.一般管理費).Sum(),
                                       一般管理費5 = s13zGroup.Where(c => c.集計年月 == i年月5).Select(c => c.一般管理費).Sum(),
                                       一般管理費6 = s13zGroup.Where(c => c.集計年月 == i年月6).Select(c => c.一般管理費).Sum(),
                                       一般管理費7 = s13zGroup.Where(c => c.集計年月 == i年月7).Select(c => c.一般管理費).Sum(),
                                       一般管理費8 = s13zGroup.Where(c => c.集計年月 == i年月8).Select(c => c.一般管理費).Sum(),
                                       一般管理費9 = s13zGroup.Where(c => c.集計年月 == i年月9).Select(c => c.一般管理費).Sum(),
                                       一般管理費10 = s13zGroup.Where(c => c.集計年月 == i年月10).Select(c => c.一般管理費).Sum(),
                                       一般管理費11 = s13zGroup.Where(c => c.集計年月 == i年月11).Select(c => c.一般管理費).Sum(),
                                       一般管理費12 = s13zGroup.Where(c => c.集計年月 == i年月12).Select(c => c.一般管理費).Sum(),
                                       一般管理費13 = s13zGroup.Where(c => c.集計年月 >= i年月1 && c.集計年月 <= i年月12).Select(c => c.一般管理費).Sum(),
                                       一般管理費14 = s13zGroup.Where(c => c.集計年月 >= i年月1 && c.集計年月 <= i年月12).Select(c => c.一般管理費).Sum() / 12,
                                       一般管理費15 = 0,
                                   }).AsQueryable();

                    //乗務員From処理　Min値
                    if (!string.IsNullOrEmpty(s乗務員From))
                    {
                        int i乗務員From = AppCommon.IntParse(s乗務員From);
                        carlistz = carlistz.Where(c => c.乗務員ID >= i乗務員From);
                    }

                    //乗務員To処理　Max値
                    if (!string.IsNullOrEmpty(s乗務員To))
                    {
                        int i乗務員TO = AppCommon.IntParse(s乗務員To);
                        carlistz = carlistz.Where(c => c.乗務員ID <= i乗務員TO);
                    }

                    if (i乗務員List.Length > 0)
                    {
						if (string.IsNullOrEmpty(s乗務員From + s乗務員To))
						{
							carlistz = carlistz.Where(q => q.乗務員ID > int.MaxValue);
						}

                        var intCause = i乗務員List;
                        carlistz = carlistz.Union(from m04 in context.M04_DRV.Where(c => c.削除日付 == null)
                                                  join s13z in context.S13_DRV.Where(c => c.集計年月 >= i開始年月 && c.集計年月 <= i終了年月) on m04.乗務員KEY equals s13z.乗務員KEY into s13zGroup
                                                  //join s13 in context.S13_DRV.Where(c => c.集計年月 >= i開始年月 && c.集計年月 <= i終了年月) on m05.車輌KEY equals s13.車輌KEY
                                                  join m05 in context.M05_CAR.Where(c => c.削除日付 == null) on m04.乗務員KEY equals m05.乗務員KEY into m05Group
                                                  //from m05g in m05Group.DefaultIfEmpty()
                                                  //join m06 in context.M06_SYA.Where(c => c.削除日付 == null) on m05Group.Select(c => c.車種ID).DefaultIfEmpty() equals m06.車種ID into m06Group
                                                  //from m06g in m06Group
                                                  orderby m04.乗務員ID
                                                  where intCause.Contains(m04.乗務員ID)
                                                  select new JMI13010g_Member
                                                  {
                                                    乗務員ID = m04.乗務員ID,
                                                    乗務員KEY = m04.乗務員KEY,
													主乗務員 = m04.乗務員名,
													退職年月日 = m04.退職年月日,
                                                    車輌KEY = m05Group.Select(c => c.車輌KEY).FirstOrDefault(),
                                                    車輌ID = m05Group.Select(c => c.車輌ID).FirstOrDefault(),
                                                    車種ID = m05Group.Select(c => c.車種ID).FirstOrDefault(),
                                                    車輌番号 = m05Group.Select(c => c.車輌番号).FirstOrDefault(),

                                                      運送収入1 = s13zGroup.Where(c => c.集計年月 == i年月1).Select(c => c.運送収入).Sum(),
                                                      運送収入2 = s13zGroup.Where(c => c.集計年月 == i年月2).Select(c => c.運送収入).Sum(),
                                                      運送収入3 = s13zGroup.Where(c => c.集計年月 == i年月3).Select(c => c.運送収入).Sum(),
                                                      運送収入4 = s13zGroup.Where(c => c.集計年月 == i年月4).Select(c => c.運送収入).Sum(),
                                                      運送収入5 = s13zGroup.Where(c => c.集計年月 == i年月5).Select(c => c.運送収入).Sum(),
                                                      運送収入6 = s13zGroup.Where(c => c.集計年月 == i年月6).Select(c => c.運送収入).Sum(),
                                                      運送収入7 = s13zGroup.Where(c => c.集計年月 == i年月7).Select(c => c.運送収入).Sum(),
                                                      運送収入8 = s13zGroup.Where(c => c.集計年月 == i年月8).Select(c => c.運送収入).Sum(),
                                                      運送収入9 = s13zGroup.Where(c => c.集計年月 == i年月9).Select(c => c.運送収入).Sum(),
                                                      運送収入10 = s13zGroup.Where(c => c.集計年月 == i年月10).Select(c => c.運送収入).Sum(),
                                                      運送収入11 = s13zGroup.Where(c => c.集計年月 == i年月11).Select(c => c.運送収入).Sum(),
                                                      運送収入12 = s13zGroup.Where(c => c.集計年月 == i年月12).Select(c => c.運送収入).Sum(),
                                                      運送収入13 = s13zGroup.Where(c => c.集計年月 >= i年月1 && c.集計年月 <= i年月12).Select(c => c.運送収入).Sum(),
                                                    運送収入14 = s13zGroup.Where(c => c.集計年月 >= i年月1 && c.集計年月 <= i年月12).Select(c => c.運送収入).Sum() / 12,
                                                    運送収入15 = 0,

                                                      一般管理費1 = s13zGroup.Where(c => c.集計年月 == i年月1).Select(c => c.一般管理費).Sum(),
                                                      一般管理費2 = s13zGroup.Where(c => c.集計年月 == i年月2).Select(c => c.一般管理費).Sum(),
                                                      一般管理費3 = s13zGroup.Where(c => c.集計年月 == i年月3).Select(c => c.一般管理費).Sum(),
                                                      一般管理費4 = s13zGroup.Where(c => c.集計年月 == i年月4).Select(c => c.一般管理費).Sum(),
                                                      一般管理費5 = s13zGroup.Where(c => c.集計年月 == i年月5).Select(c => c.一般管理費).Sum(),
                                                      一般管理費6 = s13zGroup.Where(c => c.集計年月 == i年月6).Select(c => c.一般管理費).Sum(),
                                                      一般管理費7 = s13zGroup.Where(c => c.集計年月 == i年月7).Select(c => c.一般管理費).Sum(),
                                                      一般管理費8 = s13zGroup.Where(c => c.集計年月 == i年月8).Select(c => c.一般管理費).Sum(),
                                                      一般管理費9 = s13zGroup.Where(c => c.集計年月 == i年月9).Select(c => c.一般管理費).Sum(),
                                                      一般管理費10 = s13zGroup.Where(c => c.集計年月 == i年月10).Select(c => c.一般管理費).Sum(),
                                                      一般管理費11 = s13zGroup.Where(c => c.集計年月 == i年月11).Select(c => c.一般管理費).Sum(),
                                                      一般管理費12 = s13zGroup.Where(c => c.集計年月 == i年月12).Select(c => c.一般管理費).Sum(),
                                                      一般管理費13 = s13zGroup.Where(c => c.集計年月 >= i年月1 && c.集計年月 <= i年月12).Select(c => c.一般管理費).Sum(),
                                                    一般管理費14 = s13zGroup.Where(c => c.集計年月 >= i年月1 && c.集計年月 <= i年月12).Select(c => c.一般管理費).Sum() / 12,
                                                    一般管理費15 = 0,
                                                });
                    };

					var carlistzz = carlistz.Where(c => c.退職年月日 == null || ((((DateTime)c.退職年月日).Year * 100 + ((DateTime)c.退職年月日).Month) >= i開始年月)).ToList();

                   var carlistzzz = (from car in carlistzz
                               join m06 in context.M06_SYA.Where(c => c.削除日付 == null) on car.車種ID equals m06.車種ID into m06Group
                               select new JMI13010g_Member
                               {
                                   乗務員ID = car.乗務員ID,
                                   乗務員KEY = car.乗務員KEY,
                                   車輌KEY = car.車輌KEY,
                                   車輌ID = car.車輌ID,
                                   車種ID = car.車種ID,
                                   主乗務員 = car.主乗務員,
                                   車輌番号 = car.車輌番号,
                                   車種 = m06Group.Select(c => c.車種名).FirstOrDefault(),

                                   運送収入1 = car.運送収入1,
                                   運送収入2 = car.運送収入2,
                                   運送収入3 = car.運送収入3,
                                   運送収入4 = car.運送収入4,
                                   運送収入5 = car.運送収入5,
                                   運送収入6 = car.運送収入6,
                                   運送収入7 = car.運送収入7,
                                   運送収入8 = car.運送収入8,
                                   運送収入9 = car.運送収入9,
                                   運送収入10 = car.運送収入10,
                                   運送収入11 = car.運送収入11,
                                   運送収入12 = car.運送収入12,
                                   運送収入13 = car.運送収入13,
                                   運送収入14 = car.運送収入14,
                                   運送収入15 = car.運送収入15,

                                   一般管理費1 = car.一般管理費1,
                                   一般管理費2 = car.一般管理費2,
                                   一般管理費3 = car.一般管理費3,
                                   一般管理費4 = car.一般管理費4,
                                   一般管理費5 = car.一般管理費5,
                                   一般管理費6 = car.一般管理費6,
                                   一般管理費7 = car.一般管理費7,
                                   一般管理費8 = car.一般管理費8,
                                   一般管理費9 = car.一般管理費9,
                                   一般管理費10 = car.一般管理費10,
                                   一般管理費11 = car.一般管理費11,
                                   一般管理費12 = car.一般管理費12,
                                   一般管理費13 = car.一般管理費13,
                                   一般管理費14 = car.一般管理費14,
                                   一般管理費15 = car.一般管理費15,

                               }).ToList();

                    //carlist = (from car in carlist
                    //          join s13 in context.S13_CAR.Where(c => c.集計年月 >= i開始年月 && c.集計年月 <= i終了年月) on car.車輌KEY equals s13.車輌KEY
                    //          select new JMI13010g_Member
                    //          {
                    //              車輌KEY = car.車輌KEY,
                    //              車輌ID = car.車輌ID,
                    //              車種ID = car.車種ID,
                    //              車輌番号 = car.車輌番号,
                    //              主乗務員 = car.主乗務員,
                    //              車種 = car.車種,
                    //              運送収入1 = car.運送収入1,
                    //              運送収入2 = car.運送収入2,
                    //              運送収入3 = car.運送収入3,
                    //              運送収入4 = car.運送収入4,
                    //              運送収入5 = car.運送収入5,
                    //              運送収入6 = car.運送収入6,
                    //              運送収入7 = car.運送収入7,
                    //              運送収入8 = car.運送収入8,
                    //              運送収入9 = car.運送収入9,
                    //              運送収入10 = car.運送収入10,
                    //              運送収入11 = car.運送収入11,
                    //              運送収入12 = car.運送収入12,
                    //              運送収入13 = car.運送収入13,
                    //              運送収入14 = car.運送収入14,

                    //              一般管理費1 =  car.一般管理費1,
                    //              一般管理費2 =  car.一般管理費2,
                    //              一般管理費3 =  car.一般管理費3,
                    //              一般管理費4 =  car.一般管理費4,
                    //              一般管理費5 =  car.一般管理費5,
                    //              一般管理費6 =  car.一般管理費6,
                    //              一般管理費7 =  car.一般管理費7,
                    //              一般管理費8 =  car.一般管理費8,
                    //              一般管理費9 =  car.一般管理費9,
                    //              一般管理費10 =  car.一般管理費10,
                    //              一般管理費11 =  car.一般管理費11,
                    //              一般管理費12 =  car.一般管理費12,
                    //              一般管理費13 =  car.一般管理費13,
                    //              一般管理費14 =  car.一般管理費14,
                    //          }).AsQueryable();

                   carlistzzz.Distinct();
                   carlistzz = carlistzzz;



                    //運行費項目取得
                    var keilist = (from m07 in context.M07_KEI
                                   where m07.編集区分 == 0 && m07.固定変動区分 == 1 && m07.経費区分 != 3 && m07.削除日付 == null
                                   orderby m07.経費項目ID
                                   select new JMI12010_KEI_Member
                                   {
                                       印刷グループID = 1,
                                       経費ID = m07.経費項目ID,
                                       経費項目名 = m07.経費項目名,
                                   }).AsQueryable();

                    //運行費小計
					bool bl = (from m07 in context.M07_KEI select m07).Any(c => c.編集区分 == 0 && c.固定変動区分 == 1 && c.経費区分 != 3 && c.削除日付 == null);

                    if (bl)
                    {
                        //var a = (from m07 in context.M07_KEI
                        //         select new JMI12010_KEI_Member
                        //         {
                        //             印刷グループID = 1,
                        //             経費ID = 9999999,
                        //             経費項目名 = "【 小 計 】",
                        //         }).AsQueryable();
                        //a = a.Take(1);
                        //keilist = keilist.Union(a.AsQueryable());

                        //限界利益
                        var a = (from m07 in context.M07_KEI
								 where m07.削除日付 == null
                             select new JMI12010_KEI_Member
                             {
                                 印刷グループID = 2,
                                 経費ID = 9999999,
                                 経費項目名 = "限界利益",
                             }).AsQueryable();
                        a = a.Take(1);
                        keilist = keilist.Union(a.AsQueryable());

                    }

                    //乗務員経費
                    keilist = keilist.Union(from m07 in context.M07_KEI
											where m07.編集区分 == 0 && m07.経費区分 == 3 && m07.削除日付 == null
                                            orderby m07.経費項目ID
                                            select new JMI12010_KEI_Member
                                            {
                                                印刷グループID = 3,
                                                経費ID = m07.経費項目ID,
                                                経費項目名 = m07.経費項目名,
                                            }).AsQueryable();

                    //乗務員経費小計
                    //bl = (from m07 in context.M07_KEI select m07).Any(c => c.編集区分 == 0 && c.経費区分 == 3);

                    //if (bl)
                    //{
                    //    var a = (from m07 in context.M07_KEI
                    //             select new JMI12010_KEI_Member
                    //             {
                    //                 印刷グループID = 3,
                    //                 経費ID = 9999999,
                    //                 経費項目名 = "【 小 計 】",
                    //             }).AsQueryable();
                    //    a = a.Take(1);
                    //    keilist = keilist.Union(a.AsQueryable());

                    //}

                    //その他経費
                    keilist = keilist.Union(from m07 in context.M07_KEI
											where m07.編集区分 == 0 && m07.固定変動区分 == 0 && m07.経費区分 != 3 && m07.削除日付 == null
                                            orderby m07.経費項目ID
                                            select new JMI12010_KEI_Member
                                            {
                                                印刷グループID = 4,
                                                経費ID = m07.経費項目ID,
                                                経費項目名 = m07.経費項目名,
                                            }).AsQueryable();

                    keilist = keilist.Distinct();

                    //その他経費小計
                    //bl = (from m07 in context.M07_KEI select m07).Any(c => c.編集区分 == 0 && c.固定変動区分 == 0 && c.経費区分 != 3);

                    //if (bl)
                    //{
                    //    var a = (from m07 in context.M07_KEI
                    //             select new JMI12010_KEI_Member
                    //             {
                    //                 印刷グループID = 4,
                    //                 経費ID = 9999999,
                    //                 経費項目名 = "【 小 計 】",
                    //             }).AsQueryable();
                    //    a = a.Take(1);
                    //    keilist = keilist.Union(a.AsQueryable());

                    //}

                    keilist = keilist.OrderBy(c => c.印刷グループID).ThenBy(c => c.経費ID);





                    //経費取得
                    var prelistz = (from car in carlistz
                                    from kei in keilist
                                    orderby car.乗務員ID, kei.印刷グループID, kei.経費ID
                                    select new JMI13010_PRELIST_Member
                                    {
                                        印刷グループID = kei.印刷グループID,
                                        経費ID = kei.経費ID,
                                        経費項目名 = kei.経費項目名,
                                        車輌ID = car.車輌ID,
                                        車輌KEY = car.車輌KEY,
                                        乗務員ID = car.乗務員ID,
                                        乗務員KEY = car.乗務員KEY,
                                        //金額 = s13sgroup.Where(c => c.経費項目ID == kei.経費ID).Select(c => c.金額).FirstOrDefault() == null ? 0 : s13sgroup.Where(c => c.経費項目ID == kei.経費ID).Select(c => c.金額).FirstOrDefault(),
                                    }).ToList();

                    var prelist = (from pre in prelistz
                                   join s13 in context.S13_DRVSB.Where(c => c.集計年月 >= i開始年月 && c.集計年月 <= i終了年月) on new { a = pre.乗務員KEY, b = pre.経費ID } equals new { a = s13.乗務員KEY, b = s13.経費項目ID } into s13Group
                                   //from s13g in s13Group.DefaultIfEmpty()
                                   select new JMI13010_PRELIST_Member
                                   {
                                       印刷グループID = pre.印刷グループID,
                                       経費ID = pre.経費ID,
                                       経費項目名 = pre.経費項目名,
                                       車輌ID = pre.車輌ID,
                                       車輌KEY = pre.車輌KEY,
                                       乗務員ID = pre.乗務員ID,
                                       乗務員KEY = pre.乗務員KEY,
                                       金額1 = s13Group.Where(c => c.集計年月 == i年月1).Select(c => c.金額).Sum(),
                                       金額2 = s13Group.Where(c => c.集計年月 == i年月2).Select(c => c.金額).Sum(),
                                       金額3 = s13Group.Where(c => c.集計年月 == i年月3).Select(c => c.金額).Sum(),
                                       金額4 = s13Group.Where(c => c.集計年月 == i年月4).Select(c => c.金額).Sum(),
                                       金額5 = s13Group.Where(c => c.集計年月 == i年月5).Select(c => c.金額).Sum(),
                                       金額6 = s13Group.Where(c => c.集計年月 == i年月6).Select(c => c.金額).Sum(),
                                       金額7 = s13Group.Where(c => c.集計年月 == i年月7).Select(c => c.金額).Sum(),
                                       金額8 = s13Group.Where(c => c.集計年月 == i年月8).Select(c => c.金額).Sum(),
                                       金額9 = s13Group.Where(c => c.集計年月 == i年月9).Select(c => c.金額).Sum(),
                                       金額10 = s13Group.Where(c => c.集計年月 == i年月10).Select(c => c.金額).Sum(),
                                       金額11 = s13Group.Where(c => c.集計年月 == i年月11).Select(c => c.金額).Sum(),
                                       金額12 = s13Group.Where(c => c.集計年月 == i年月12).Select(c => c.金額).Sum(),
                                       金額13 = s13Group.Where(c => c.集計年月 >= i年月1 && c.集計年月 <= i年月12).Select(c => c.金額).Sum(),
                                       

                                   }).AsQueryable();

                    prelist = prelist.Union(from pre in prelist.Where(c => c.印刷グループID != 2)
                                            group pre by new { pre.乗務員KEY, pre.乗務員ID, pre.印刷グループID } into preGroup
                                            select new JMI13010_PRELIST_Member
                                            {
                                                印刷グループID = preGroup.Key.印刷グループID,
                                                経費ID = 9999999,
                                                経費項目名 = "【 小 計 】",
                                                乗務員ID = preGroup.Key.乗務員ID,
                                                乗務員KEY = preGroup.Key.乗務員KEY,
                                                金額1 = preGroup.Select(c => c.金額1).Sum(),
                                                金額2 = preGroup.Select(c => c.金額2).Sum(),
                                                金額3 = preGroup.Select(c => c.金額3).Sum(),
                                                金額4 = preGroup.Select(c => c.金額4).Sum(),
                                                金額5 = preGroup.Select(c => c.金額5).Sum(),
                                                金額6 = preGroup.Select(c => c.金額6).Sum(),
                                                金額7 = preGroup.Select(c => c.金額7).Sum(),
                                                金額8 = preGroup.Select(c => c.金額8).Sum(),
                                                金額9 = preGroup.Select(c => c.金額9).Sum(),
                                                金額10 = preGroup.Select(c => c.金額10).Sum(),
                                                金額11 = preGroup.Select(c => c.金額11).Sum(),
                                                金額12 = preGroup.Select(c => c.金額12).Sum(),
                                                金額13 = preGroup.Select(c => c.金額13).Sum(),
                                                金額14 = preGroup.Select(c => c.金額13).Sum() / 12,


                                            }).AsQueryable();


                    prelist = prelist.Distinct();
                    prelist = prelist.OrderBy(c => c.乗務員ID).ThenBy(c => c.印刷グループID);


                    var carlist = (from car in carlistzz
                                   join pre in prelist.Where(c => c.印刷グループID != 2) on car.乗務員KEY equals pre.乗務員KEY into preGroup
                                   select new JMI13010g_Member
                                   {
                                       乗務員KEY = car.乗務員KEY,
                                       乗務員ID = car.乗務員ID,
                                       車輌KEY = car.車輌KEY,
                                       車輌ID = car.車輌ID,
                                       車種ID = car.車種ID,
                                       車輌番号 = car.車輌番号,
                                       主乗務員 = car.主乗務員,
                                       車種 = car.車種,
                                       運送収入1 = car.運送収入1,
                                       運送収入2 = car.運送収入2,
                                       運送収入3 = car.運送収入3,
                                       運送収入4 = car.運送収入4,
                                       運送収入5 = car.運送収入5,
                                       運送収入6 = car.運送収入6,
                                       運送収入7 = car.運送収入7,
                                       運送収入8 = car.運送収入8,
                                       運送収入9 = car.運送収入9,
                                       運送収入10 = car.運送収入10,
                                       運送収入11 = car.運送収入11,
                                       運送収入12 = car.運送収入12,
                                       運送収入13 = car.運送収入13,
                                       運送収入14 = car.運送収入14,

                                       一般管理費1 = car.一般管理費1,
                                       一般管理費2 = car.一般管理費2,
                                       一般管理費3 = car.一般管理費3,
                                       一般管理費4 = car.一般管理費4,
                                       一般管理費5 = car.一般管理費5,
                                       一般管理費6 = car.一般管理費6,
                                       一般管理費7 = car.一般管理費7,
                                       一般管理費8 = car.一般管理費8,
                                       一般管理費9 = car.一般管理費9,
                                       一般管理費10 = car.一般管理費10,
                                       一般管理費11 = car.一般管理費11,
                                       一般管理費12 = car.一般管理費12,
                                       一般管理費13 = car.一般管理費13,
                                       一般管理費14 = car.一般管理費14,

                                       限界利益1 = car.運送収入1 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額1).Sum(),
                                       限界利益2 = car.運送収入2 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額2).Sum(),
                                       限界利益3 = car.運送収入3 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額3).Sum(),
                                       限界利益4 = car.運送収入4 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額4).Sum(),
                                       限界利益5 = car.運送収入5 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額5).Sum(),
                                       限界利益6 = car.運送収入6 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額6).Sum(),
                                       限界利益7 = car.運送収入7 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額7).Sum(),
                                       限界利益8 = car.運送収入8 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額8).Sum(),
                                       限界利益9 = car.運送収入9 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額9).Sum(),
                                       限界利益10 = car.運送収入10 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額10).Sum(),
                                       限界利益11 = car.運送収入11 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額11).Sum(),
                                       限界利益12 = car.運送収入12 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額12).Sum(),
                                       限界利益13 = car.運送収入13 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額13).Sum(),
                                       小計1_1 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額1).Sum(),
                                       小計1_2 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額2).Sum(),
                                       小計1_3 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額3).Sum(),
                                       小計1_4 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額4).Sum(),
                                       小計1_5 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額5).Sum(),
                                       小計1_6 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額6).Sum(),
                                       小計1_7 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額7).Sum(),
                                       小計1_8 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額8).Sum(),
                                       小計1_9 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額9).Sum(),
                                       小計1_10 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額10).Sum(),
                                       小計1_11 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額11).Sum(),
                                       小計1_12 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額12).Sum(),
                                       小計1_13 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額13).Sum(),
                                       小計1_14 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額14).Sum(),

                                       小計2_1 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額1).Sum(),
                                       小計2_2 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額2).Sum(),
                                       小計2_3 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額3).Sum(),
                                       小計2_4 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額4).Sum(),
                                       小計2_5 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額5).Sum(),
                                       小計2_6 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額6).Sum(),
                                       小計2_7 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額7).Sum(),
                                       小計2_8 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額8).Sum(),
                                       小計2_9 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額9).Sum(),
                                       小計2_10 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額10).Sum(),
                                       小計2_11 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額11).Sum(),
                                       小計2_12 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額12).Sum(),
                                       小計2_13 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額13).Sum(),
                                       小計2_14 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額14).Sum(),

                                       小計3_1 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額1).Sum(),
                                       小計3_2 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額2).Sum(),
                                       小計3_3 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額3).Sum(),
                                       小計3_4 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額4).Sum(),
                                       小計3_5 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額5).Sum(),
                                       小計3_6 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額6).Sum(),
                                       小計3_7 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額7).Sum(),
                                       小計3_8 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額8).Sum(),
                                       小計3_9 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額9).Sum(),
                                       小計3_10 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額10).Sum(),
                                       小計3_11 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額11).Sum(),
                                       小計3_12 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額12).Sum(),
                                       小計3_13 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額13).Sum(),
                                       小計3_14 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額14).Sum(),

                                   }).ToList();

                    foreach (JMI13010g_Member a in carlist)
                    {
                        if (a.運送収入1 == null) { a.運送収入1 = 0; };
                        if (a.運送収入2 == null) { a.運送収入2 = 0; };
                        if (a.運送収入3 == null) { a.運送収入3 = 0; };
                        if (a.運送収入4 == null) { a.運送収入4 = 0; };
                        if (a.運送収入5 == null) { a.運送収入5 = 0; };
                        if (a.運送収入6 == null) { a.運送収入6 = 0; };
                        if (a.運送収入7 == null) { a.運送収入7 = 0; };
                        if (a.運送収入8 == null) { a.運送収入8 = 0; };
                        if (a.運送収入9 == null) { a.運送収入9 = 0; };
                        if (a.運送収入10 == null) { a.運送収入10 = 0; };
                        if (a.運送収入11 == null) { a.運送収入11 = 0; };
                        if (a.運送収入12 == null) { a.運送収入12 = 0; };
                        if (a.運送収入13 == null) { a.運送収入13 = 0; };
                        if (a.運送収入14 == null) { a.運送収入14 = 0; };
                        if (a.運送収入15 == null) { a.運送収入15 = 0; };

                        if (a.一般管理費1 == null) { a.一般管理費1 = 0; };
                        if (a.一般管理費2 == null) { a.一般管理費2 = 0; };
                        if (a.一般管理費3 == null) { a.一般管理費3 = 0; };
                        if (a.一般管理費4 == null) { a.一般管理費4 = 0; };
                        if (a.一般管理費5 == null) { a.一般管理費5 = 0; };
                        if (a.一般管理費6 == null) { a.一般管理費6 = 0; };
                        if (a.一般管理費7 == null) { a.一般管理費7 = 0; };
                        if (a.一般管理費8 == null) { a.一般管理費8 = 0; };
                        if (a.一般管理費9 == null) { a.一般管理費9 = 0; };
                        if (a.一般管理費10 == null) { a.一般管理費10 = 0; };
                        if (a.一般管理費11 == null) { a.一般管理費11 = 0; };
                        if (a.一般管理費12 == null) { a.一般管理費12 = 0; };
                        if (a.一般管理費13 == null) { a.一般管理費13 = 0; };
                        if (a.一般管理費14 == null) { a.一般管理費14 = 0; };
                        if (a.一般管理費15 == null) { a.一般管理費15 = 0; };

                        if (a.限界利益1 == null) { a.限界利益1 = 0; };
                        if (a.限界利益2 == null) { a.限界利益2 = 0; };
                        if (a.限界利益3 == null) { a.限界利益3 = 0; };
                        if (a.限界利益4 == null) { a.限界利益4 = 0; };
                        if (a.限界利益5 == null) { a.限界利益5 = 0; };
                        if (a.限界利益6 == null) { a.限界利益6 = 0; };
                        if (a.限界利益7 == null) { a.限界利益7 = 0; };
                        if (a.限界利益8 == null) { a.限界利益8 = 0; };
                        if (a.限界利益9 == null) { a.限界利益9 = 0; };
                        if (a.限界利益10 == null) { a.限界利益10 = 0; };
                        if (a.限界利益11 == null) { a.限界利益11 = 0; };
                        if (a.限界利益12 == null) { a.限界利益12 = 0; };
                        if (a.限界利益13 == null) { a.限界利益13 = 0; };
                        if (a.限界利益14 == null) { a.限界利益14 = 0; };
                        if (a.限界利益15 == null) { a.限界利益15 = 0; };
                    }


                    //var prelistz = (from p in prelist
                    //               join s13s in context.S13_CARSB.Where(c => c.集計年月 == i年月) on new { a = p.経費ID, b = p.車輌KEY } equals new { a = s13s.経費項目ID, b = s13s.車輌KEY } into s13sgroup
                    //               from s13sg in s13sgroup.DefaultIfEmpty()
                    //               orderby p.車輌ID, p.印刷グループID, p.経費ID
                    //               select new JMI13010_PRELIST_Member
                    //               {
                    //                   印刷グループID = p.印刷グループID,
                    //                   経費ID = p.経費ID,
                    //                   経費項目名 = p.経費項目名,
                    //                   車輌ID = p.車輌ID,
                    //                   車輌KEY = p.車輌KEY,
                    //                   金額 = s13sg.金額 == null ? 0 : s13sg.金額,
                    //               }).AsQueryable();


                    var listprelist = prelist.ToList();


                    var prt_GROPID1 = listprelist.Where(c => c.印刷グループID == 1).Select(c => c.経費ID);
                    var prt_GROPID3 = listprelist.Where(c => c.印刷グループID == 3).Select(c => c.経費ID);
                    var prt_GROPID4 = listprelist.Where(c => c.印刷グループID == 4).Select(c => c.経費ID);
                    prt_GROPID1 = prt_GROPID1.Distinct();
                    prt_GROPID3 = prt_GROPID3.Distinct();
                    prt_GROPID4 = prt_GROPID4.Distinct();

                    var prt_GROPID = listprelist.Select(c => c.経費ID);
                    prt_GROPID = prt_GROPID.Distinct();

                    //印刷用データ作成
                    int[] carid = new int[6];
                    int carid1 = 0;
                    foreach (JMI13010g_Member a in carlist)
                    {
                        carid1 = a.乗務員KEY;


                            List<JMI13010_PRELIST_Member> plist = (from p in prelist
                                                                   where p.乗務員KEY == carid1
                                                                  select p).ToList();

                            JMI13010g_Member a1 = (from p in carlist where p.乗務員KEY == carid1 select p).FirstOrDefault();


                            decimal syaryoutyokusetuhi1 = 0;
                            decimal syaryoutyokusetuhi2 = 0;
                            decimal syaryoutyokusetuhi3 = 0;
                            decimal syaryoutyokusetuhi4 = 0;
                            decimal syaryoutyokusetuhi5 = 0;
                            decimal syaryoutyokusetuhi6 = 0;
                            decimal syaryoutyokusetuhi7 = 0;
                            decimal syaryoutyokusetuhi8 = 0;
                            decimal syaryoutyokusetuhi9 = 0;
                            decimal syaryoutyokusetuhi10 = 0;
                            decimal syaryoutyokusetuhi11 = 0;
                            decimal syaryoutyokusetuhi12 = 0;
                            decimal syaryoutyokusetuhi13 = 0;
                            decimal syaryoutyokusetuhi14 = 0;
                            decimal syaryoutyokusetuhi15 = 0;


                            if ((from s13s in context.S13_DRVSB
                                 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月1 && s13s.乗務員KEY == carid1
                                 select s13s.金額).Any())
                            {
                                syaryoutyokusetuhi1 = (from s13s in context.S13_DRVSB
                                                       where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月1 && s13s.乗務員KEY == carid1
                                                       select s13s.金額).Sum();
                            }

                            if ((from s13s in context.S13_DRVSB
                                 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月2 && s13s.乗務員KEY == carid1
                                 select s13s.金額).Any())
                            {
                                syaryoutyokusetuhi2 = (from s13s in context.S13_DRVSB
                                                       where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月2 && s13s.乗務員KEY == carid1
                                                       select s13s.金額).Sum();
                            }

                            if ((from s13s in context.S13_DRVSB
                                 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月3 && s13s.乗務員KEY == carid1
                                 select s13s.金額).Any())
                            {
                                syaryoutyokusetuhi3 = (from s13s in context.S13_DRVSB
                                                       where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月3 && s13s.乗務員KEY == carid1
                                                       select s13s.金額).Sum();
                            }

                            if ((from s13s in context.S13_DRVSB
                                 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月4 && s13s.乗務員KEY == carid1
                                 select s13s.金額).Any())
                            {
                                syaryoutyokusetuhi4 = (from s13s in context.S13_DRVSB
                                                       where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月4 && s13s.乗務員KEY == carid1
                                                       select s13s.金額).Sum();
                            }

                            if ((from s13s in context.S13_DRVSB
                                 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月5 && s13s.乗務員KEY == carid1
                                 select s13s.金額).Any())
                            {
                                syaryoutyokusetuhi5 = (from s13s in context.S13_DRVSB
                                                       where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月5 && s13s.乗務員KEY == carid1
                                                       select s13s.金額).Sum();
                            }

                            if ((from s13s in context.S13_DRVSB
                                 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月6 && s13s.乗務員KEY == carid1
                                 select s13s.金額).Any())
                            {
                                syaryoutyokusetuhi6 = (from s13s in context.S13_DRVSB
                                                       where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月6 && s13s.乗務員KEY == carid1
                                                       select s13s.金額).Sum();
                            }

                            if ((from s13s in context.S13_DRVSB
                                 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月7 && s13s.乗務員KEY == carid1
                                 select s13s.金額).Any())
                            {
                                syaryoutyokusetuhi7 = (from s13s in context.S13_DRVSB
                                                       where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月7 && s13s.乗務員KEY == carid1
                                                       select s13s.金額).Sum();
                            }

                            if ((from s13s in context.S13_DRVSB
                                 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月8 && s13s.乗務員KEY == carid1
                                 select s13s.金額).Any())
                            {
                                syaryoutyokusetuhi8 = (from s13s in context.S13_DRVSB
                                                       where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月8 && s13s.乗務員KEY == carid1
                                                       select s13s.金額).Sum();
                            }

                            if ((from s13s in context.S13_DRVSB
                                 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月9 && s13s.乗務員KEY == carid1
                                 select s13s.金額).Any())
                            {
                                syaryoutyokusetuhi9 = (from s13s in context.S13_DRVSB
                                                       where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月9 && s13s.乗務員KEY == carid1
                                                       select s13s.金額).Sum();
                            }

                            if ((from s13s in context.S13_DRVSB
                                 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月10 && s13s.乗務員KEY == carid1
                                 select s13s.金額).Any())
                            {
                                syaryoutyokusetuhi10 = (from s13s in context.S13_DRVSB
                                                        where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月10 && s13s.乗務員KEY == carid1
                                                       select s13s.金額).Sum();
                            }

                            if ((from s13s in context.S13_DRVSB
                                 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月11 && s13s.乗務員KEY == carid1
                                 select s13s.金額).Any())
                            {
                                syaryoutyokusetuhi11 = (from s13s in context.S13_DRVSB
                                                        where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月11 && s13s.乗務員KEY == carid1
                                                       select s13s.金額).Sum();
                            }

                            if ((from s13s in context.S13_DRVSB
                                 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月12 && s13s.乗務員KEY == carid1
                                 select s13s.金額).Any())
                            {
                                syaryoutyokusetuhi12 = (from s13s in context.S13_DRVSB
                                                        where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月12 && s13s.乗務員KEY == carid1
                                                       select s13s.金額).Sum();
                            }

                            if ((from s13s in context.S13_DRVSB
                                 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 >= i年月1 && s13s.集計年月 <= i年月12 && s13s.乗務員KEY == carid1
                                 select s13s.金額).Any())
                            {
                                syaryoutyokusetuhi13 = (from s13s in context.S13_DRVSB
                                                        where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 >= i年月1 && s13s.集計年月 <= i年月12 && s13s.乗務員KEY == carid1
                                                       select s13s.金額).Sum();
                            }

                            if ((from s13s in context.S13_DRVSB
                                 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 >= i年月1 && s13s.集計年月 <= i年月12 && s13s.乗務員KEY == carid1
                                 select s13s.金額).Any())
                            {
                                syaryoutyokusetuhi14 = (from s13s in context.S13_DRVSB
                                                        where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 >= i年月1 && s13s.集計年月 <= i年月12 && s13s.乗務員KEY == carid1
                                                        select s13s.金額).Sum() / 12;
                            }
                            if ((from s13s in context.S13_DRVSB
                                 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 >= i年月1 && s13s.集計年月 <= i年月12 && s13s.乗務員KEY == carid1
                                 select s13s.金額).Any())
                            {
                                if (a1 != null && a1.運送収入13 != 0)
                                {
                                    syaryoutyokusetuhi15 = (from s13s in context.S13_DRVSB
                                                            where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 >= i年月1 && s13s.集計年月 <= i年月12 && s13s.乗務員KEY == carid1
                                                            select s13s.金額).Sum() / (decimal)a1.運送収入13 * 100;
                                }
                            }


                            foreach (var row in plist)
                            {

                                JMI13010_Member list = new JMI13010_Member()
                                {
                                    一般管理費1 = a1.一般管理費1,
                                    一般管理費2 = a1.一般管理費2,
                                    一般管理費3 = a1.一般管理費3,
                                    一般管理費4 = a1.一般管理費4,
                                    一般管理費5 = a1.一般管理費5,
                                    一般管理費6 = a1.一般管理費6,
                                    一般管理費7 = a1.一般管理費7,
                                    一般管理費8 = a1.一般管理費8,
                                    一般管理費9 = a1.一般管理費9,
                                    一般管理費10 = a1.一般管理費10,
                                    一般管理費11 = a1.一般管理費11,
                                    一般管理費12 = a1.一般管理費12,
                                    一般管理費13 = a1.一般管理費13,
                                    一般管理費14 = a1.一般管理費14,
                                    一般管理費15 = a1.一般管理費15,

                                    印刷グループID = row.印刷グループID,

                                    運送収入1 = a1.運送収入1,
                                    運送収入2 = a1.運送収入2,
                                    運送収入3 = a1.運送収入3,
                                    運送収入4 = a1.運送収入4,
                                    運送収入5 = a1.運送収入5,
                                    運送収入6 = a1.運送収入6,
                                    運送収入7 = a1.運送収入7,
                                    運送収入8 = a1.運送収入8,
                                    運送収入9 = a1.運送収入9,
                                    運送収入10 = a1.運送収入10,
                                    運送収入11 = a1.運送収入11,
                                    運送収入12 = a1.運送収入12,
                                    運送収入13 = a1.運送収入13,
                                    運送収入14 = a1.運送収入14,
                                    運送収入15 = a1.運送収入13 == 0 ? 0 : a1.運送収入13 / a1.運送収入13,

                                    //金額1 = row.金額1,
                                    //金額2 = row.金額2,
                                    //金額3 = row.金額3,
                                    //金額4 = row.金額4,
                                    //金額5 = row.金額5,
                                    //金額6 = row.金額6,
                                    //金額7 = row.金額7,
                                    //金額8 = row.金額8,
                                    //金額9 = row.金額9,
                                    //金額10 = row.金額10,
                                    //金額11 = row.金額11,
                                    //金額12 = row.金額12,
                                    //金額13 = row.金額13,
                                    //金額14 = row.金額14,
                                    //金額15 = row.金額15,

                                    //金額1 = row.経費ID == 9999999 ?
                                    //            row.印刷グループID == 2 ?
                                    //            a1.限界利益1 :
                                    //            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車輌KEY == carid1).Select(c => c.金額1).Sum() :
                                    //           (from s13s in context.S13_CARSB
                                    //            where s13s.集計年月 == i年月1 && s13s.経費項目ID == row.経費ID && s13s.車輌KEY == carid1
                                    //            select s13s.金額).FirstOrDefault(),

                                    //金額2 = row.経費ID == 9999999 ?
                                    //            row.印刷グループID == 2 ?
                                    //            a1.限界利益2 :
                                    //            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車輌KEY == carid1).Select(c => c.金額2).Sum() :
                                    //           (from s13s in context.S13_CARSB
                                    //            where s13s.集計年月 == i年月2 && s13s.経費項目ID == row.経費ID && s13s.車輌KEY == carid1
                                    //            select s13s.金額).FirstOrDefault(),

                                    //金額3 = row.経費ID == 9999999 ?
                                    //            row.印刷グループID == 2 ?
                                    //            a1.限界利益3 :
                                    //            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車輌KEY == carid1).Select(c => c.金額3).Sum() :
                                    //           (from s13s in context.S13_CARSB
                                    //            where s13s.集計年月 == i年月3 && s13s.経費項目ID == row.経費ID && s13s.車輌KEY == carid1
                                    //            select s13s.金額).FirstOrDefault(),

                                    //金額4 = row.経費ID == 9999999 ?
                                    //            row.印刷グループID == 2 ?
                                    //            a1.限界利益4 :
                                    //            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車輌KEY == carid1).Select(c => c.金額4).Sum() :
                                    //           (from s13s in context.S13_CARSB
                                    //            where s13s.集計年月 == i年月4 && s13s.経費項目ID == row.経費ID && s13s.車輌KEY == carid1
                                    //            select s13s.金額).FirstOrDefault(),

                                    //金額5 = row.経費ID == 9999999 ?
                                    //            row.印刷グループID == 2 ?
                                    //            a1.限界利益5 :
                                    //            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車輌KEY == carid1).Select(c => c.金額5).Sum() :
                                    //            (from s13s in context.S13_CARSB
                                    //             where s13s.集計年月 == i年月5 && s13s.経費項目ID == row.経費ID && s13s.車輌KEY == carid1
                                    //             select s13s.金額).FirstOrDefault(),

                                    //金額6 = row.経費ID == 9999999 ?
                                    //            row.印刷グループID == 2 ?
                                    //            a1.限界利益6 :
                                    //            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車輌KEY == carid1).Select(c => c.金額6).Sum() :
                                    //           (from s13s in context.S13_CARSB
                                    //            where s13s.集計年月 == i年月6 && s13s.経費項目ID == row.経費ID && s13s.車輌KEY == carid1
                                    //            select s13s.金額).FirstOrDefault(),

                                    //金額7 = row.経費ID == 9999999 ?
                                    //            row.印刷グループID == 2 ?
                                    //            a1.限界利益7 :
                                    //            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車輌KEY == carid1).Select(c => c.金額7).Sum() :
                                    //           (from s13s in context.S13_CARSB
                                    //            where s13s.集計年月 == i年月7 && s13s.経費項目ID == row.経費ID && s13s.車輌KEY == carid1
                                    //            select s13s.金額).FirstOrDefault(),

                                    //金額8 = row.経費ID == 9999999 ?
                                    //            row.印刷グループID == 2 ?
                                    //            a1.限界利益8 :
                                    //            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車輌KEY == carid1).Select(c => c.金額8).Sum() :
                                    //           (from s13s in context.S13_CARSB
                                    //            where s13s.集計年月 == i年月8 && s13s.経費項目ID == row.経費ID && s13s.車輌KEY == carid1
                                    //            select s13s.金額).FirstOrDefault(),

                                    //金額9 = row.経費ID == 9999999 ?
                                    //            row.印刷グループID == 2 ?
                                    //            a1.限界利益9 :
                                    //            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車輌KEY == carid1).Select(c => c.金額9).Sum() :
                                    //            (from s13s in context.S13_CARSB
                                    //             where s13s.集計年月 == i年月9 && s13s.経費項目ID == row.経費ID && s13s.車輌KEY == carid1
                                    //             select s13s.金額).FirstOrDefault(),

                                    //金額10 = row.経費ID == 9999999 ?
                                    //            row.印刷グループID == 2 ?
                                    //            a1.限界利益10 :
                                    //            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車輌KEY == carid1).Select(c => c.金額10).Sum() :
                                    //           (from s13s in context.S13_CARSB
                                    //            where s13s.集計年月 == i年月10 && s13s.経費項目ID == row.経費ID && s13s.車輌KEY == carid1
                                    //            select s13s.金額).FirstOrDefault(),

                                    //金額11 = row.経費ID == 9999999 ?
                                    //            row.印刷グループID == 2 ?
                                    //            a1.限界利益11 :
                                    //            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車輌KEY == carid1).Select(c => c.金額11).Sum() :
                                    //           (from s13s in context.S13_CARSB
                                    //            where s13s.集計年月 == i年月11 && s13s.経費項目ID == row.経費ID && s13s.車輌KEY == carid1
                                    //            select s13s.金額).FirstOrDefault(),

                                    //金額12 = row.経費ID == 9999999 ?
                                    //            row.印刷グループID == 2 ?
                                    //            a1.限界利益12 :
                                    //            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車輌KEY == carid1).Select(c => c.金額12).Sum() :
                                    //           (from s13s in context.S13_CARSB
                                    //            where s13s.集計年月 == i年月12 && s13s.経費項目ID == row.経費ID && s13s.車輌KEY == carid1
                                    //            select s13s.金額).FirstOrDefault(),

                                    //金額13 = row.経費ID == 9999999 ?
                                    //            row.印刷グループID == 2 ?
                                    //            a1.限界利益13 :
                                    //            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車輌KEY == carid1).Select(c => c.金額13).Sum() :
                                    //           (from s13s in context.S13_CARSB
                                    //            where s13s.集計年月 >= i年月1 && s13s.集計年月 <= i年月12 && s13s.経費項目ID == row.経費ID && s13s.車輌KEY == carid1
                                    //            select s13s.金額).Sum(),

                                    //金額14 = row.経費ID == 9999999 ?
                                    //            row.印刷グループID == 2 ?
                                    //            a1.限界利益13 / 12 :
                                    //            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車輌KEY == carid1).Select(c => c.金額13).Sum() / 12 :
                                    //           (from s13s in context.S13_CARSB
                                    //            where s13s.集計年月 >= i年月1 && s13s.集計年月 <= i年月12 && s13s.経費項目ID == row.経費ID && s13s.車輌KEY == carid1
                                    //            select s13s.金額).Sum() / 12,

                                    //金額15 = row.経費ID == 9999999 ?
                                    //            row.印刷グループID == 2 ?
                                    //            a1.運送収入13 == 0 ? 0 : a1.限界利益12 / a1.運送収入13 * 100 :
                                    //            a1.運送収入13 == 0 ? 0 : prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.車輌KEY == carid1).Select(c => c.金額13).Sum() / a1.運送収入13 :
                                    //            a1.運送収入13 == 0 ? 0 :
                                    //           (from s13s in context.S13_CARSB
                                    //            where s13s.集計年月 >= i年月1 && s13s.集計年月 <= i年月12 && s13s.経費項目ID == row.経費ID && s13s.車輌KEY == carid1
                                    //            select s13s.金額).Sum()
                                    //            / a1.運送収入13 * 100,


                                    金額1 = row.経費ID == 9999999 ?
                                                row.印刷グループID == 2 ?
                                                a1.限界利益1 :
                                                row.金額1 :
                                                row.金額1,

                                    金額2 = row.経費ID == 9999999 ?
                                                row.印刷グループID == 2 ?
                                                a1.限界利益2 :
                                                row.金額2 :
                                                row.金額2,

                                    金額3 = row.経費ID == 9999999 ?
                                                row.印刷グループID == 2 ?
                                                a1.限界利益3 :
                                                row.金額3 :
                                                row.金額3,

                                    金額4 = row.経費ID == 9999999 ?
                                                row.印刷グループID == 2 ?
                                                a1.限界利益4 :
                                                row.金額4 :
                                                row.金額4,

                                    金額5 = row.経費ID == 9999999 ?
                                                row.印刷グループID == 2 ?
                                                a1.限界利益5 :
                                                row.金額5 :
                                                row.金額5,

                                    金額6 = row.経費ID == 9999999 ?
                                                row.印刷グループID == 2 ?
                                                a1.限界利益6 :
                                                row.金額6 :
                                                row.金額6,

                                    金額7 = row.経費ID == 9999999 ?
                                                row.印刷グループID == 2 ?
                                                a1.限界利益7 :
                                                row.金額7 :
                                                row.金額7,

                                    金額8 = row.経費ID == 9999999 ?
                                                row.印刷グループID == 2 ?
                                                a1.限界利益8 :
                                                row.金額8 :
                                                row.金額8,

                                    金額9 = row.経費ID == 9999999 ?
                                                row.印刷グループID == 2 ?
                                                a1.限界利益9 :
                                                row.金額9 :
                                                row.金額9,

                                    金額10 = row.経費ID == 9999999 ?
                                                row.印刷グループID == 2 ?
                                                a1.限界利益10 :
                                                row.金額10 :
                                                row.金額10,

                                    金額11 = row.経費ID == 9999999 ?
                                                row.印刷グループID == 2 ?
                                                a1.限界利益11 :
                                                row.金額11 :
                                                row.金額11,

                                    金額12 = row.経費ID == 9999999 ?
                                                row.印刷グループID == 2 ?
                                                a1.限界利益12 :
                                                row.金額12 :
                                                row.金額12,

                                    金額13 = row.経費ID == 9999999 ?
                                                row.印刷グループID == 2 ?
                                                a1.限界利益13 :
                                                row.金額13 :
                                                row.金額13,

                                    金額14 = row.経費ID == 9999999 ?
                                                row.印刷グループID == 2 ?
                                                a1.限界利益13 / 12 :
                                                row.金額13 / 12 :
                                                row.金額13 / 12,

                                    金額15 = row.経費ID == 9999999 ?
                                                row.印刷グループID == 2 ?
                                                a1.運送収入13 == 0 ? 0 : a1.限界利益13 / a1.運送収入13 * 100 :
                                                a1.運送収入13 == 0 ? 0 : row.金額13 / a1.運送収入13  * 100 :
                                                a1.運送収入13 == 0 ? 0 : row.金額13 / a1.運送収入13  * 100,


                                    //金額1 = (from p in prelist where p.車輌KEY == carid1 && p.経費ID == row.経費ID select p.金額).FirstOrDefault(),
                                    //金額2 = (from p in prelist where p.車輌KEY == carid2 && p.経費ID == row.経費ID select p.金額).FirstOrDefault(),
                                    //金額3 = (from p in prelist where p.車輌KEY == carid3 && p.経費ID == row.経費ID select p.金額).FirstOrDefault(),
                                    //金額4 = (from p in prelist where p.車輌KEY == carid4 && p.経費ID == row.経費ID select p.金額).FirstOrDefault(),
                                    //金額5 = (from p in prelist where p.車輌KEY == carid5 && p.経費ID == row.経費ID select p.金額).FirstOrDefault(),
                                    //金額6 = (from p in prelist where p.車輌KEY == carid6 && p.経費ID == row.経費ID select p.金額).FirstOrDefault(),


                                    経費ID = row.経費ID,
                                    経費項目名 = row.経費項目名,


                                    車種 = a1.車種,

                                    乗務員ID = (int?)a1.乗務員ID,

                                    車輌直接益1 = a1.運送収入1 - syaryoutyokusetuhi1,
                                    車輌直接益2 = a1.運送収入2 - syaryoutyokusetuhi2,
                                    車輌直接益3 = a1.運送収入3 - syaryoutyokusetuhi3,
                                    車輌直接益4 = a1.運送収入4 - syaryoutyokusetuhi4,
                                    車輌直接益5 = a1.運送収入5 - syaryoutyokusetuhi5,
                                    車輌直接益6 = a1.運送収入6 - syaryoutyokusetuhi6,
                                    車輌直接益7 = a1.運送収入7 - syaryoutyokusetuhi7,
                                    車輌直接益8 = a1.運送収入8 - syaryoutyokusetuhi8,
                                    車輌直接益9 = a1.運送収入9 - syaryoutyokusetuhi9,
                                    車輌直接益10 = a1.運送収入10 - syaryoutyokusetuhi10,
                                    車輌直接益11 = a1.運送収入11 - syaryoutyokusetuhi11,
                                    車輌直接益12 = a1.運送収入12 - syaryoutyokusetuhi12,
                                    車輌直接益13 = a1.運送収入13 - syaryoutyokusetuhi13,
                                    車輌直接益14 = a1.運送収入14 - syaryoutyokusetuhi14,
                                    車輌直接益15 = a1.運送収入15 - syaryoutyokusetuhi15,

                                    車輌直接費1 = syaryoutyokusetuhi1,
                                    車輌直接費2 = syaryoutyokusetuhi2,
                                    車輌直接費3 = syaryoutyokusetuhi3,
                                    車輌直接費4 = syaryoutyokusetuhi4,
                                    車輌直接費5 = syaryoutyokusetuhi5,
                                    車輌直接費6 = syaryoutyokusetuhi6,
                                    車輌直接費7 = syaryoutyokusetuhi7,
                                    車輌直接費8 = syaryoutyokusetuhi8,
                                    車輌直接費9 = syaryoutyokusetuhi9,
                                    車輌直接費10 = syaryoutyokusetuhi10,
                                    車輌直接費11 = syaryoutyokusetuhi11,
                                    車輌直接費12 = syaryoutyokusetuhi12,
                                    車輌直接費13 = syaryoutyokusetuhi13,
                                    車輌直接費14 = syaryoutyokusetuhi14,
                                    車輌直接費15 = syaryoutyokusetuhi15,
                                    
                                    車輌番号 = a1.車輌番号,
                                    
                                    主乗務員 = a1.主乗務員,

                                    当月利益1 = a1.運送収入1 - syaryoutyokusetuhi1 - a1.一般管理費1,
                                    当月利益2 = a1.運送収入2 - syaryoutyokusetuhi2 - a1.一般管理費2,
                                    当月利益3 = a1.運送収入3 - syaryoutyokusetuhi3 - a1.一般管理費3,
                                    当月利益4 = a1.運送収入4 - syaryoutyokusetuhi4 - a1.一般管理費4,
                                    当月利益5 = a1.運送収入5 - syaryoutyokusetuhi5 - a1.一般管理費5,
                                    当月利益6 = a1.運送収入6 - syaryoutyokusetuhi6 - a1.一般管理費6,
                                    当月利益7 = a1.運送収入7 - syaryoutyokusetuhi7 - a1.一般管理費7,
                                    当月利益8 = a1.運送収入8 - syaryoutyokusetuhi8 - a1.一般管理費8,
                                    当月利益9 = a1.運送収入9 - syaryoutyokusetuhi9 - a1.一般管理費9,
                                    当月利益10 = a1.運送収入10 - syaryoutyokusetuhi10 - a1.一般管理費10,
                                    当月利益11 = a1.運送収入11 - syaryoutyokusetuhi11 - a1.一般管理費11,
                                    当月利益12 = a1.運送収入12 - syaryoutyokusetuhi12 - a1.一般管理費12,
                                    当月利益13 = a1.運送収入13 - syaryoutyokusetuhi13 - a1.一般管理費13,
                                    当月利益14 = a1.運送収入14 - syaryoutyokusetuhi14 - a1.一般管理費14,
                                    当月利益15 = a1.運送収入15 - syaryoutyokusetuhi15 - a1.一般管理費15,
                                    
                                    年 = 年.ToString(),
                                    コードFrom = s乗務員From,
                                    コードTo = s乗務員To,
                                    乗務員ﾋﾟｯｸｱｯﾌﾟ = s乗務員List,
                                    月 = 月.ToString(),

                                };
                                retList.Add(list);
                            };

                            carid1 = 0;

                    };



                      return retList;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion


		#region JMI13010 CSV
		public List<JMI13010_PRELIST_Member_CSV> JMI13010_GetDataList_CSV(string s乗務員From, string s乗務員To, int?[] i乗務員List, string s乗務員List, int 年, int 月)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				DateTime dDate = new DateTime(年, 月, 1);
				int i年月1, i年月2, i年月3, i年月4, i年月5, i年月6, i年月7, i年月8, i年月9, i年月10, i年月11, i年月12;
				i年月1 = dDate.Year * 100 + dDate.Month;
				dDate = dDate.AddMonths(1);
				i年月2 = dDate.Year * 100 + dDate.Month;
				dDate = dDate.AddMonths(1);
				i年月3 = dDate.Year * 100 + dDate.Month;
				dDate = dDate.AddMonths(1);
				i年月4 = dDate.Year * 100 + dDate.Month;
				dDate = dDate.AddMonths(1);
				i年月5 = dDate.Year * 100 + dDate.Month;
				dDate = dDate.AddMonths(1);
				i年月6 = dDate.Year * 100 + dDate.Month;
				dDate = dDate.AddMonths(1);
				i年月7 = dDate.Year * 100 + dDate.Month;
				dDate = dDate.AddMonths(1);
				i年月8 = dDate.Year * 100 + dDate.Month;
				dDate = dDate.AddMonths(1);
				i年月9 = dDate.Year * 100 + dDate.Month;
				dDate = dDate.AddMonths(1);
				i年月10 = dDate.Year * 100 + dDate.Month;
				dDate = dDate.AddMonths(1);
				i年月11 = dDate.Year * 100 + dDate.Month;
				dDate = dDate.AddMonths(1);
				i年月12 = dDate.Year * 100 + dDate.Month;
				int i開始年月 = i年月1;
				int i終了年月 = i年月12;




				List<JMI13010_PRELIST_Member_CSV> retList = new List<JMI13010_PRELIST_Member_CSV>();

				context.Connection.Open();
				try
				{
					string 乗務員ﾋﾟｯｸｱｯﾌﾟ指定 = string.Empty;

					var carlistz = (from m04 in context.M04_DRV.Where(c => c.削除日付 == null)
									join s13z in context.S13_DRV.Where(c => c.集計年月 >= i開始年月 && c.集計年月 <= i終了年月) on m04.乗務員KEY equals s13z.乗務員KEY into s13zGroup
									//join s13 in context.S13_DRV.Where(c => c.集計年月 >= i開始年月 && c.集計年月 <= i終了年月) on m05.車輌KEY equals s13.車輌KEY
									join m05 in context.M05_CAR.Where(c => c.削除日付 == null) on m04.乗務員KEY equals m05.乗務員KEY into m05Group
									//from m05g in m05Group.DefaultIfEmpty()
									//join m06 in context.M06_SYA.Where(c => c.削除日付 == null) on m05Group.Select(c => c.車種ID).DefaultIfEmpty() equals m06.車種ID into m06Group
									//from m06g in m06Group
									orderby m04.乗務員ID
									select new JMI13010g_Member
									{
										乗務員ID = m04.乗務員ID,
										乗務員KEY = m04.乗務員KEY,
										主乗務員 = m04.乗務員名,
										退職年月日 = m04.退職年月日,
										車輌KEY = m05Group.Select(c => c.車輌KEY).FirstOrDefault(),
										車輌ID = m05Group.Select(c => c.車輌ID).FirstOrDefault(),
										車種ID = m05Group.Select(c => c.車種ID).FirstOrDefault(),
										車輌番号 = m05Group.Select(c => c.車輌番号).FirstOrDefault(),

										運送収入1 = s13zGroup.Where(c => c.集計年月 == i年月1).Select(c => c.運送収入).Sum(),
										運送収入2 = s13zGroup.Where(c => c.集計年月 == i年月2).Select(c => c.運送収入).Sum(),
										運送収入3 = s13zGroup.Where(c => c.集計年月 == i年月3).Select(c => c.運送収入).Sum(),
										運送収入4 = s13zGroup.Where(c => c.集計年月 == i年月4).Select(c => c.運送収入).Sum(),
										運送収入5 = s13zGroup.Where(c => c.集計年月 == i年月5).Select(c => c.運送収入).Sum(),
										運送収入6 = s13zGroup.Where(c => c.集計年月 == i年月6).Select(c => c.運送収入).Sum(),
										運送収入7 = s13zGroup.Where(c => c.集計年月 == i年月7).Select(c => c.運送収入).Sum(),
										運送収入8 = s13zGroup.Where(c => c.集計年月 == i年月8).Select(c => c.運送収入).Sum(),
										運送収入9 = s13zGroup.Where(c => c.集計年月 == i年月9).Select(c => c.運送収入).Sum(),
										運送収入10 = s13zGroup.Where(c => c.集計年月 == i年月10).Select(c => c.運送収入).Sum(),
										運送収入11 = s13zGroup.Where(c => c.集計年月 == i年月11).Select(c => c.運送収入).Sum(),
										運送収入12 = s13zGroup.Where(c => c.集計年月 == i年月12).Select(c => c.運送収入).Sum(),
										運送収入13 = s13zGroup.Where(c => c.集計年月 >= i年月1 && c.集計年月 <= i年月12).Select(c => c.運送収入).Sum(),
										運送収入14 = s13zGroup.Where(c => c.集計年月 >= i年月1 && c.集計年月 <= i年月12).Select(c => c.運送収入).Sum() / 12,
										運送収入15 = 0,

										一般管理費1 = s13zGroup.Where(c => c.集計年月 == i年月1).Select(c => c.一般管理費).Sum(),
										一般管理費2 = s13zGroup.Where(c => c.集計年月 == i年月2).Select(c => c.一般管理費).Sum(),
										一般管理費3 = s13zGroup.Where(c => c.集計年月 == i年月3).Select(c => c.一般管理費).Sum(),
										一般管理費4 = s13zGroup.Where(c => c.集計年月 == i年月4).Select(c => c.一般管理費).Sum(),
										一般管理費5 = s13zGroup.Where(c => c.集計年月 == i年月5).Select(c => c.一般管理費).Sum(),
										一般管理費6 = s13zGroup.Where(c => c.集計年月 == i年月6).Select(c => c.一般管理費).Sum(),
										一般管理費7 = s13zGroup.Where(c => c.集計年月 == i年月7).Select(c => c.一般管理費).Sum(),
										一般管理費8 = s13zGroup.Where(c => c.集計年月 == i年月8).Select(c => c.一般管理費).Sum(),
										一般管理費9 = s13zGroup.Where(c => c.集計年月 == i年月9).Select(c => c.一般管理費).Sum(),
										一般管理費10 = s13zGroup.Where(c => c.集計年月 == i年月10).Select(c => c.一般管理費).Sum(),
										一般管理費11 = s13zGroup.Where(c => c.集計年月 == i年月11).Select(c => c.一般管理費).Sum(),
										一般管理費12 = s13zGroup.Where(c => c.集計年月 == i年月12).Select(c => c.一般管理費).Sum(),
										一般管理費13 = s13zGroup.Where(c => c.集計年月 >= i年月1 && c.集計年月 <= i年月12).Select(c => c.一般管理費).Sum(),
										一般管理費14 = s13zGroup.Where(c => c.集計年月 >= i年月1 && c.集計年月 <= i年月12).Select(c => c.一般管理費).Sum() / 12,
										一般管理費15 = 0,
									}).AsQueryable();


					//乗務員From処理　Min値
					if (!string.IsNullOrEmpty(s乗務員From))
					{
						int i乗務員From = AppCommon.IntParse(s乗務員From);
						carlistz = carlistz.Where(c => c.乗務員ID >= i乗務員From);
					}

					//乗務員To処理　Max値
					if (!string.IsNullOrEmpty(s乗務員To))
					{
						int i乗務員TO = AppCommon.IntParse(s乗務員To);
						carlistz = carlistz.Where(c => c.乗務員ID <= i乗務員TO);
					}

					if (i乗務員List.Length > 0)
					{
						if (string.IsNullOrEmpty(s乗務員From + s乗務員To))
						{
							carlistz = carlistz.Where(q => q.乗務員ID > int.MaxValue);
						}
						var intCause = i乗務員List;
						carlistz = carlistz.Union(from m04 in context.M04_DRV.Where(c => c.削除日付 == null)
												  join s13z in context.S13_DRV.Where(c => c.集計年月 >= i開始年月 && c.集計年月 <= i終了年月) on m04.乗務員KEY equals s13z.乗務員KEY into s13zGroup
												  //join s13 in context.S13_DRV.Where(c => c.集計年月 >= i開始年月 && c.集計年月 <= i終了年月) on m05.車輌KEY equals s13.車輌KEY
												  join m05 in context.M05_CAR.Where(c => c.削除日付 == null) on m04.乗務員KEY equals m05.乗務員KEY into m05Group
												  //from m05g in m05Group.DefaultIfEmpty()
												  //join m06 in context.M06_SYA.Where(c => c.削除日付 == null) on m05g.車種ID equals m06.車種ID into m06Group
												  //from m06g in m06Group
												  orderby m04.乗務員ID
												  where intCause.Contains(m04.乗務員ID)
												  select new JMI13010g_Member
												  {
													  乗務員ID = m04.乗務員ID,
													  乗務員KEY = m04.乗務員KEY,
													  主乗務員 = m04.乗務員名,
													  退職年月日 = m04.退職年月日,
													  車輌KEY = m05Group.Select(c => c.車輌KEY).FirstOrDefault(),
													  車輌ID = m05Group.Select(c => c.車輌ID).FirstOrDefault(),
													  車種ID = m05Group.Select(c => c.車種ID).FirstOrDefault(),
													  車輌番号 = m05Group.Select(c => c.車輌番号).FirstOrDefault(),

													  運送収入1 = s13zGroup.Where(c => c.集計年月 == i年月1).Select(c => c.運送収入).Sum(),
													  運送収入2 = s13zGroup.Where(c => c.集計年月 == i年月2).Select(c => c.運送収入).Sum(),
													  運送収入3 = s13zGroup.Where(c => c.集計年月 == i年月3).Select(c => c.運送収入).Sum(),
													  運送収入4 = s13zGroup.Where(c => c.集計年月 == i年月4).Select(c => c.運送収入).Sum(),
													  運送収入5 = s13zGroup.Where(c => c.集計年月 == i年月5).Select(c => c.運送収入).Sum(),
													  運送収入6 = s13zGroup.Where(c => c.集計年月 == i年月6).Select(c => c.運送収入).Sum(),
													  運送収入7 = s13zGroup.Where(c => c.集計年月 == i年月7).Select(c => c.運送収入).Sum(),
													  運送収入8 = s13zGroup.Where(c => c.集計年月 == i年月8).Select(c => c.運送収入).Sum(),
													  運送収入9 = s13zGroup.Where(c => c.集計年月 == i年月9).Select(c => c.運送収入).Sum(),
													  運送収入10 = s13zGroup.Where(c => c.集計年月 == i年月10).Select(c => c.運送収入).Sum(),
													  運送収入11 = s13zGroup.Where(c => c.集計年月 == i年月11).Select(c => c.運送収入).Sum(),
													  運送収入12 = s13zGroup.Where(c => c.集計年月 == i年月12).Select(c => c.運送収入).Sum(),
													  運送収入13 = s13zGroup.Where(c => c.集計年月 >= i年月1 && c.集計年月 <= i年月12).Select(c => c.運送収入).Sum(),
													  運送収入14 = s13zGroup.Where(c => c.集計年月 >= i年月1 && c.集計年月 <= i年月12).Select(c => c.運送収入).Sum() / 12,
													  運送収入15 = 0,

													  一般管理費1 = s13zGroup.Where(c => c.集計年月 == i年月1).Select(c => c.一般管理費).Sum(),
													  一般管理費2 = s13zGroup.Where(c => c.集計年月 == i年月2).Select(c => c.一般管理費).Sum(),
													  一般管理費3 = s13zGroup.Where(c => c.集計年月 == i年月3).Select(c => c.一般管理費).Sum(),
													  一般管理費4 = s13zGroup.Where(c => c.集計年月 == i年月4).Select(c => c.一般管理費).Sum(),
													  一般管理費5 = s13zGroup.Where(c => c.集計年月 == i年月5).Select(c => c.一般管理費).Sum(),
													  一般管理費6 = s13zGroup.Where(c => c.集計年月 == i年月6).Select(c => c.一般管理費).Sum(),
													  一般管理費7 = s13zGroup.Where(c => c.集計年月 == i年月7).Select(c => c.一般管理費).Sum(),
													  一般管理費8 = s13zGroup.Where(c => c.集計年月 == i年月8).Select(c => c.一般管理費).Sum(),
													  一般管理費9 = s13zGroup.Where(c => c.集計年月 == i年月9).Select(c => c.一般管理費).Sum(),
													  一般管理費10 = s13zGroup.Where(c => c.集計年月 == i年月10).Select(c => c.一般管理費).Sum(),
													  一般管理費11 = s13zGroup.Where(c => c.集計年月 == i年月11).Select(c => c.一般管理費).Sum(),
													  一般管理費12 = s13zGroup.Where(c => c.集計年月 == i年月12).Select(c => c.一般管理費).Sum(),
													  一般管理費13 = s13zGroup.Where(c => c.集計年月 >= i年月1 && c.集計年月 <= i年月12).Select(c => c.一般管理費).Sum(),
													  一般管理費14 = s13zGroup.Where(c => c.集計年月 >= i年月1 && c.集計年月 <= i年月12).Select(c => c.一般管理費).Sum() / 12,
													  一般管理費15 = 0,
												  }).AsQueryable();
					};

					var carlistzz = carlistz.Where(c => c.退職年月日 == null || ((((DateTime)c.退職年月日).Year * 100 + ((DateTime)c.退職年月日).Month) >= i開始年月)).ToList();
					var carlistzzz = (from car in carlistzz
									  join m06 in context.M06_SYA.Where(c => c.削除日付 == null) on car.車種ID equals m06.車種ID into m06Group
									  select new JMI13010g_Member
									  {
										  乗務員ID = car.乗務員ID,
										  乗務員KEY = car.乗務員KEY,
										  車輌KEY = car.車輌KEY,
										  車輌ID = car.車輌ID,
										  車種ID = car.車種ID,
										  主乗務員 = car.主乗務員,
										  車輌番号 = car.車輌番号,
										  車種 = m06Group.Select(c => c.車種名).FirstOrDefault(),

										  運送収入1 = car.運送収入1,
										  運送収入2 = car.運送収入2,
										  運送収入3 = car.運送収入3,
										  運送収入4 = car.運送収入4,
										  運送収入5 = car.運送収入5,
										  運送収入6 = car.運送収入6,
										  運送収入7 = car.運送収入7,
										  運送収入8 = car.運送収入8,
										  運送収入9 = car.運送収入9,
										  運送収入10 = car.運送収入10,
										  運送収入11 = car.運送収入11,
										  運送収入12 = car.運送収入12,
										  運送収入13 = car.運送収入13,
										  運送収入14 = car.運送収入14,
										  運送収入15 = car.運送収入15,

										  一般管理費1 = car.一般管理費1,
										  一般管理費2 = car.一般管理費2,
										  一般管理費3 = car.一般管理費3,
										  一般管理費4 = car.一般管理費4,
										  一般管理費5 = car.一般管理費5,
										  一般管理費6 = car.一般管理費6,
										  一般管理費7 = car.一般管理費7,
										  一般管理費8 = car.一般管理費8,
										  一般管理費9 = car.一般管理費9,
										  一般管理費10 = car.一般管理費10,
										  一般管理費11 = car.一般管理費11,
										  一般管理費12 = car.一般管理費12,
										  一般管理費13 = car.一般管理費13,
										  一般管理費14 = car.一般管理費14,
										  一般管理費15 = car.一般管理費15,

									  }).ToList();

					//carlist = (from car in carlist
					//          join s13 in context.S13_CAR.Where(c => c.集計年月 >= i開始年月 && c.集計年月 <= i終了年月) on car.車輌KEY equals s13.車輌KEY
					//          select new JMI13010g_Member
					//          {
					//              車輌KEY = car.車輌KEY,
					//              車輌ID = car.車輌ID,
					//              車種ID = car.車種ID,
					//              車輌番号 = car.車輌番号,
					//              主乗務員 = car.主乗務員,
					//              車種 = car.車種,
					//              運送収入1 = car.運送収入1,
					//              運送収入2 = car.運送収入2,
					//              運送収入3 = car.運送収入3,
					//              運送収入4 = car.運送収入4,
					//              運送収入5 = car.運送収入5,
					//              運送収入6 = car.運送収入6,
					//              運送収入7 = car.運送収入7,
					//              運送収入8 = car.運送収入8,
					//              運送収入9 = car.運送収入9,
					//              運送収入10 = car.運送収入10,
					//              運送収入11 = car.運送収入11,
					//              運送収入12 = car.運送収入12,
					//              運送収入13 = car.運送収入13,
					//              運送収入14 = car.運送収入14,

					//              一般管理費1 =  car.一般管理費1,
					//              一般管理費2 =  car.一般管理費2,
					//              一般管理費3 =  car.一般管理費3,
					//              一般管理費4 =  car.一般管理費4,
					//              一般管理費5 =  car.一般管理費5,
					//              一般管理費6 =  car.一般管理費6,
					//              一般管理費7 =  car.一般管理費7,
					//              一般管理費8 =  car.一般管理費8,
					//              一般管理費9 =  car.一般管理費9,
					//              一般管理費10 =  car.一般管理費10,
					//              一般管理費11 =  car.一般管理費11,
					//              一般管理費12 =  car.一般管理費12,
					//              一般管理費13 =  car.一般管理費13,
					//              一般管理費14 =  car.一般管理費14,
					//          }).AsQueryable();

					carlistzzz.Distinct();
					carlistzz = carlistzzz;



					//運行費項目取得
					var keilist = (from m07 in context.M07_KEI
								   where m07.編集区分 == 0 && m07.固定変動区分 == 1 && m07.経費区分 != 3 && m07.削除日付 == null
								   orderby m07.経費項目ID
								   select new JMI12010_KEI_Member
								   {
									   印刷グループID = 1,
									   経費ID = m07.経費項目ID,
									   経費項目名 = m07.経費項目名,
								   }).AsQueryable();

					//運行費小計
					bool bl = (from m07 in context.M07_KEI select m07).Any(c => c.編集区分 == 0 && c.固定変動区分 == 1 && c.経費区分 != 3 && c.削除日付 == null);

					if (bl)
					{
						//var a = (from m07 in context.M07_KEI
						//         select new JMI12010_KEI_Member
						//         {
						//             印刷グループID = 1,
						//             経費ID = 9999999,
						//             経費項目名 = "【 小 計 】",
						//         }).AsQueryable();
						//a = a.Take(1);
						//keilist = keilist.Union(a.AsQueryable());

						//限界利益
						var a = (from m07 in context.M07_KEI
								 where m07.削除日付 == null
								 select new JMI12010_KEI_Member
								 {
									 印刷グループID = 2,
									 経費ID = 9999999,
									 経費項目名 = "限界利益",
								 }).AsQueryable();
						a = a.Take(1);
						keilist = keilist.Union(a.AsQueryable());

					}

					//乗務員経費
					keilist = keilist.Union(from m07 in context.M07_KEI
											where m07.編集区分 == 0 && m07.経費区分 == 3 && m07.削除日付 == null
											orderby m07.経費項目ID
											select new JMI12010_KEI_Member
											{
												印刷グループID = 3,
												経費ID = m07.経費項目ID,
												経費項目名 = m07.経費項目名,
											}).AsQueryable();

					//乗務員経費小計
					//bl = (from m07 in context.M07_KEI select m07).Any(c => c.編集区分 == 0 && c.経費区分 == 3);

					//if (bl)
					//{
					//    var a = (from m07 in context.M07_KEI
					//             select new JMI12010_KEI_Member
					//             {
					//                 印刷グループID = 3,
					//                 経費ID = 9999999,
					//                 経費項目名 = "【 小 計 】",
					//             }).AsQueryable();
					//    a = a.Take(1);
					//    keilist = keilist.Union(a.AsQueryable());

					//}

					//その他経費
					keilist = keilist.Union(from m07 in context.M07_KEI
											where m07.編集区分 == 0 && m07.固定変動区分 == 0 && m07.経費区分 != 3 && m07.削除日付 == null
											orderby m07.経費項目ID
											select new JMI12010_KEI_Member
											{
												印刷グループID = 4,
												経費ID = m07.経費項目ID,
												経費項目名 = m07.経費項目名,
											}).AsQueryable();

					keilist = keilist.Distinct();

					//その他経費小計
					//bl = (from m07 in context.M07_KEI select m07).Any(c => c.編集区分 == 0 && c.固定変動区分 == 0 && c.経費区分 != 3);

					//if (bl)
					//{
					//    var a = (from m07 in context.M07_KEI
					//             select new JMI12010_KEI_Member
					//             {
					//                 印刷グループID = 4,
					//                 経費ID = 9999999,
					//                 経費項目名 = "【 小 計 】",
					//             }).AsQueryable();
					//    a = a.Take(1);
					//    keilist = keilist.Union(a.AsQueryable());

					//}

					keilist = keilist.OrderBy(c => c.印刷グループID).ThenBy(c => c.経費ID);





					//経費取得
					var prelistz = (from car in carlistz
									from kei in keilist
									orderby car.乗務員ID, kei.印刷グループID, kei.経費ID
									select new JMI13010_PRELIST_Member
									{
										印刷グループID = kei.印刷グループID,
										経費ID = kei.経費ID,
										経費項目名 = kei.経費項目名,
										車輌ID = car.車輌ID,
										車輌KEY = car.車輌KEY,
										乗務員ID = car.乗務員ID,
										乗務員KEY = car.乗務員KEY,
										//金額 = s13sgroup.Where(c => c.経費項目ID == kei.経費ID).Select(c => c.金額).FirstOrDefault() == null ? 0 : s13sgroup.Where(c => c.経費項目ID == kei.経費ID).Select(c => c.金額).FirstOrDefault(),
									}).ToList();

					var prelist = (from pre in prelistz
								   join s13 in context.S13_DRVSB.Where(c => c.集計年月 >= i開始年月 && c.集計年月 <= i終了年月) on new { a = pre.乗務員KEY, b = pre.経費ID } equals new { a = s13.乗務員KEY, b = s13.経費項目ID } into s13Group
								   //from s13g in s13Group.DefaultIfEmpty()
								   select new JMI13010_PRELIST_Member
								   {
									   印刷グループID = pre.印刷グループID,
									   経費ID = pre.経費ID,
									   経費項目名 = pre.経費項目名,
									   車輌ID = pre.車輌ID,
									   車輌KEY = pre.車輌KEY,
									   乗務員ID = pre.乗務員ID,
									   乗務員KEY = pre.乗務員KEY,
									   金額1 = s13Group.Where(c => c.集計年月 == i年月1).Select(c => c.金額).Sum(),
									   金額2 = s13Group.Where(c => c.集計年月 == i年月2).Select(c => c.金額).Sum(),
									   金額3 = s13Group.Where(c => c.集計年月 == i年月3).Select(c => c.金額).Sum(),
									   金額4 = s13Group.Where(c => c.集計年月 == i年月4).Select(c => c.金額).Sum(),
									   金額5 = s13Group.Where(c => c.集計年月 == i年月5).Select(c => c.金額).Sum(),
									   金額6 = s13Group.Where(c => c.集計年月 == i年月6).Select(c => c.金額).Sum(),
									   金額7 = s13Group.Where(c => c.集計年月 == i年月7).Select(c => c.金額).Sum(),
									   金額8 = s13Group.Where(c => c.集計年月 == i年月8).Select(c => c.金額).Sum(),
									   金額9 = s13Group.Where(c => c.集計年月 == i年月9).Select(c => c.金額).Sum(),
									   金額10 = s13Group.Where(c => c.集計年月 == i年月10).Select(c => c.金額).Sum(),
									   金額11 = s13Group.Where(c => c.集計年月 == i年月11).Select(c => c.金額).Sum(),
									   金額12 = s13Group.Where(c => c.集計年月 == i年月12).Select(c => c.金額).Sum(),
									   金額13 = s13Group.Where(c => c.集計年月 >= i年月1 && c.集計年月 <= i年月12).Select(c => c.金額).Sum(),


								   }).AsQueryable();

					prelist = prelist.Union(from pre in prelist.Where(c => c.印刷グループID != 2)
											group pre by new { pre.乗務員KEY, pre.乗務員ID, pre.印刷グループID } into preGroup
											select new JMI13010_PRELIST_Member
											{
												印刷グループID = preGroup.Key.印刷グループID,
												経費ID = 9999999,
												経費項目名 = "【 小 計 】",
												乗務員ID = preGroup.Key.乗務員ID,
												乗務員KEY = preGroup.Key.乗務員KEY,
												金額1 = preGroup.Select(c => c.金額1).Sum(),
												金額2 = preGroup.Select(c => c.金額2).Sum(),
												金額3 = preGroup.Select(c => c.金額3).Sum(),
												金額4 = preGroup.Select(c => c.金額4).Sum(),
												金額5 = preGroup.Select(c => c.金額5).Sum(),
												金額6 = preGroup.Select(c => c.金額6).Sum(),
												金額7 = preGroup.Select(c => c.金額7).Sum(),
												金額8 = preGroup.Select(c => c.金額8).Sum(),
												金額9 = preGroup.Select(c => c.金額9).Sum(),
												金額10 = preGroup.Select(c => c.金額10).Sum(),
												金額11 = preGroup.Select(c => c.金額11).Sum(),
												金額12 = preGroup.Select(c => c.金額12).Sum(),
												金額13 = preGroup.Select(c => c.金額13).Sum(),
												金額14 = preGroup.Select(c => c.金額13).Sum() / 12,


											}).AsQueryable();


					prelist = prelist.Distinct();
					prelist = prelist.OrderBy(c => c.乗務員ID).ThenBy(c => c.印刷グループID);


					var carlist = (from car in carlistzz
								   join pre in prelist.Where(c => c.印刷グループID != 2) on car.乗務員KEY equals pre.乗務員KEY into preGroup
								   select new JMI13010g_Member
								   {
									   乗務員KEY = car.乗務員KEY,
									   乗務員ID = car.乗務員ID,
									   車輌KEY = car.車輌KEY,
									   車輌ID = car.車輌ID,
									   車種ID = car.車種ID,
									   車輌番号 = car.車輌番号,
									   主乗務員 = car.主乗務員,
									   車種 = car.車種,
									   運送収入1 = car.運送収入1,
									   運送収入2 = car.運送収入2,
									   運送収入3 = car.運送収入3,
									   運送収入4 = car.運送収入4,
									   運送収入5 = car.運送収入5,
									   運送収入6 = car.運送収入6,
									   運送収入7 = car.運送収入7,
									   運送収入8 = car.運送収入8,
									   運送収入9 = car.運送収入9,
									   運送収入10 = car.運送収入10,
									   運送収入11 = car.運送収入11,
									   運送収入12 = car.運送収入12,
									   運送収入13 = car.運送収入13,
									   運送収入14 = car.運送収入14,

									   一般管理費1 = car.一般管理費1,
									   一般管理費2 = car.一般管理費2,
									   一般管理費3 = car.一般管理費3,
									   一般管理費4 = car.一般管理費4,
									   一般管理費5 = car.一般管理費5,
									   一般管理費6 = car.一般管理費6,
									   一般管理費7 = car.一般管理費7,
									   一般管理費8 = car.一般管理費8,
									   一般管理費9 = car.一般管理費9,
									   一般管理費10 = car.一般管理費10,
									   一般管理費11 = car.一般管理費11,
									   一般管理費12 = car.一般管理費12,
									   一般管理費13 = car.一般管理費13,
									   一般管理費14 = car.一般管理費14,

									   限界利益1 = car.運送収入1 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額1).Sum(),
									   限界利益2 = car.運送収入2 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額2).Sum(),
									   限界利益3 = car.運送収入3 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額3).Sum(),
									   限界利益4 = car.運送収入4 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額4).Sum(),
									   限界利益5 = car.運送収入5 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額5).Sum(),
									   限界利益6 = car.運送収入6 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額6).Sum(),
									   限界利益7 = car.運送収入7 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額7).Sum(),
									   限界利益8 = car.運送収入8 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額8).Sum(),
									   限界利益9 = car.運送収入9 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額9).Sum(),
									   限界利益10 = car.運送収入10 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額10).Sum(),
									   限界利益11 = car.運送収入11 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額11).Sum(),
									   限界利益12 = car.運送収入12 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額12).Sum(),
									   限界利益13 = car.運送収入13 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額13).Sum(),
									   小計1_1 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額1).Sum(),
									   小計1_2 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額2).Sum(),
									   小計1_3 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額3).Sum(),
									   小計1_4 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額4).Sum(),
									   小計1_5 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額5).Sum(),
									   小計1_6 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額6).Sum(),
									   小計1_7 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額7).Sum(),
									   小計1_8 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額8).Sum(),
									   小計1_9 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額9).Sum(),
									   小計1_10 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額10).Sum(),
									   小計1_11 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額11).Sum(),
									   小計1_12 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額12).Sum(),
									   小計1_13 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額13).Sum(),
									   小計1_14 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額14).Sum(),

									   小計2_1 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額1).Sum(),
									   小計2_2 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額2).Sum(),
									   小計2_3 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額3).Sum(),
									   小計2_4 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額4).Sum(),
									   小計2_5 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額5).Sum(),
									   小計2_6 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額6).Sum(),
									   小計2_7 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額7).Sum(),
									   小計2_8 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額8).Sum(),
									   小計2_9 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額9).Sum(),
									   小計2_10 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額10).Sum(),
									   小計2_11 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額11).Sum(),
									   小計2_12 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額12).Sum(),
									   小計2_13 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額13).Sum(),
									   小計2_14 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額14).Sum(),

									   小計3_1 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額1).Sum(),
									   小計3_2 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額2).Sum(),
									   小計3_3 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額3).Sum(),
									   小計3_4 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額4).Sum(),
									   小計3_5 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額5).Sum(),
									   小計3_6 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額6).Sum(),
									   小計3_7 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額7).Sum(),
									   小計3_8 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額8).Sum(),
									   小計3_9 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額9).Sum(),
									   小計3_10 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額10).Sum(),
									   小計3_11 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額11).Sum(),
									   小計3_12 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額12).Sum(),
									   小計3_13 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額13).Sum(),
									   小計3_14 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額14).Sum(),

								   }).ToList();

					foreach (JMI13010g_Member a in carlist)
					{
						if (a.運送収入1 == null) { a.運送収入1 = 0; };
						if (a.運送収入2 == null) { a.運送収入2 = 0; };
						if (a.運送収入3 == null) { a.運送収入3 = 0; };
						if (a.運送収入4 == null) { a.運送収入4 = 0; };
						if (a.運送収入5 == null) { a.運送収入5 = 0; };
						if (a.運送収入6 == null) { a.運送収入6 = 0; };
						if (a.運送収入7 == null) { a.運送収入7 = 0; };
						if (a.運送収入8 == null) { a.運送収入8 = 0; };
						if (a.運送収入9 == null) { a.運送収入9 = 0; };
						if (a.運送収入10 == null) { a.運送収入10 = 0; };
						if (a.運送収入11 == null) { a.運送収入11 = 0; };
						if (a.運送収入12 == null) { a.運送収入12 = 0; };
						if (a.運送収入13 == null) { a.運送収入13 = 0; };
						if (a.運送収入14 == null) { a.運送収入14 = 0; };
						if (a.運送収入15 == null) { a.運送収入15 = 0; };

						if (a.一般管理費1 == null) { a.一般管理費1 = 0; };
						if (a.一般管理費2 == null) { a.一般管理費2 = 0; };
						if (a.一般管理費3 == null) { a.一般管理費3 = 0; };
						if (a.一般管理費4 == null) { a.一般管理費4 = 0; };
						if (a.一般管理費5 == null) { a.一般管理費5 = 0; };
						if (a.一般管理費6 == null) { a.一般管理費6 = 0; };
						if (a.一般管理費7 == null) { a.一般管理費7 = 0; };
						if (a.一般管理費8 == null) { a.一般管理費8 = 0; };
						if (a.一般管理費9 == null) { a.一般管理費9 = 0; };
						if (a.一般管理費10 == null) { a.一般管理費10 = 0; };
						if (a.一般管理費11 == null) { a.一般管理費11 = 0; };
						if (a.一般管理費12 == null) { a.一般管理費12 = 0; };
						if (a.一般管理費13 == null) { a.一般管理費13 = 0; };
						if (a.一般管理費14 == null) { a.一般管理費14 = 0; };
						if (a.一般管理費15 == null) { a.一般管理費15 = 0; };

						if (a.限界利益1 == null) { a.限界利益1 = 0; };
						if (a.限界利益2 == null) { a.限界利益2 = 0; };
						if (a.限界利益3 == null) { a.限界利益3 = 0; };
						if (a.限界利益4 == null) { a.限界利益4 = 0; };
						if (a.限界利益5 == null) { a.限界利益5 = 0; };
						if (a.限界利益6 == null) { a.限界利益6 = 0; };
						if (a.限界利益7 == null) { a.限界利益7 = 0; };
						if (a.限界利益8 == null) { a.限界利益8 = 0; };
						if (a.限界利益9 == null) { a.限界利益9 = 0; };
						if (a.限界利益10 == null) { a.限界利益10 = 0; };
						if (a.限界利益11 == null) { a.限界利益11 = 0; };
						if (a.限界利益12 == null) { a.限界利益12 = 0; };
						if (a.限界利益13 == null) { a.限界利益13 = 0; };
						if (a.限界利益14 == null) { a.限界利益14 = 0; };
						if (a.限界利益15 == null) { a.限界利益15 = 0; };
					}


					//var prelistz = (from p in prelist
					//               join s13s in context.S13_CARSB.Where(c => c.集計年月 == i年月) on new { a = p.経費ID, b = p.車輌KEY } equals new { a = s13s.経費項目ID, b = s13s.車輌KEY } into s13sgroup
					//               from s13sg in s13sgroup.DefaultIfEmpty()
					//               orderby p.車輌ID, p.印刷グループID, p.経費ID
					//               select new JMI13010_PRELIST_Member
					//               {
					//                   印刷グループID = p.印刷グループID,
					//                   経費ID = p.経費ID,
					//                   経費項目名 = p.経費項目名,
					//                   車輌ID = p.車輌ID,
					//                   車輌KEY = p.車輌KEY,
					//                   金額 = s13sg.金額 == null ? 0 : s13sg.金額,
					//               }).AsQueryable();


					var listprelist = prelist.ToList();


					var prt_GROPID1 = listprelist.Where(c => c.印刷グループID == 1).Select(c => c.経費ID);
					var prt_GROPID3 = listprelist.Where(c => c.印刷グループID == 3).Select(c => c.経費ID);
					var prt_GROPID4 = listprelist.Where(c => c.印刷グループID == 4).Select(c => c.経費ID);
					prt_GROPID1 = prt_GROPID1.Distinct();
					prt_GROPID3 = prt_GROPID3.Distinct();
					prt_GROPID4 = prt_GROPID4.Distinct();

					var prt_GROPID = listprelist.Select(c => c.経費ID);
					prt_GROPID = prt_GROPID.Distinct();

					//印刷用データ作成
					int[] carid = new int[6];
					int carid1 = 0;
					foreach (JMI13010g_Member a in carlist)
					{
						carid1 = a.乗務員KEY;


						List<JMI13010_PRELIST_Member> plist = (from p in prelist
															   where p.乗務員KEY == carid1
															   select p).ToList();

						JMI13010g_Member a1 = (from p in carlist where p.乗務員KEY == carid1 select p).FirstOrDefault();


						decimal syaryoutyokusetuhi1 = 0;
						decimal syaryoutyokusetuhi2 = 0;
						decimal syaryoutyokusetuhi3 = 0;
						decimal syaryoutyokusetuhi4 = 0;
						decimal syaryoutyokusetuhi5 = 0;
						decimal syaryoutyokusetuhi6 = 0;
						decimal syaryoutyokusetuhi7 = 0;
						decimal syaryoutyokusetuhi8 = 0;
						decimal syaryoutyokusetuhi9 = 0;
						decimal syaryoutyokusetuhi10 = 0;
						decimal syaryoutyokusetuhi11 = 0;
						decimal syaryoutyokusetuhi12 = 0;
						decimal syaryoutyokusetuhi13 = 0;
						decimal syaryoutyokusetuhi14 = 0;
						decimal syaryoutyokusetuhi15 = 0;


						if ((from s13s in context.S13_DRVSB
							 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月1 && s13s.乗務員KEY == carid1
							 select s13s.金額).Any())
						{
							syaryoutyokusetuhi1 = (from s13s in context.S13_DRVSB
												   where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月1 && s13s.乗務員KEY == carid1
												   select s13s.金額).Sum();
						}

						if ((from s13s in context.S13_DRVSB
							 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月2 && s13s.乗務員KEY == carid1
							 select s13s.金額).Any())
						{
							syaryoutyokusetuhi2 = (from s13s in context.S13_DRVSB
												   where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月2 && s13s.乗務員KEY == carid1
												   select s13s.金額).Sum();
						}

						if ((from s13s in context.S13_DRVSB
							 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月3 && s13s.乗務員KEY == carid1
							 select s13s.金額).Any())
						{
							syaryoutyokusetuhi3 = (from s13s in context.S13_DRVSB
												   where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月3 && s13s.乗務員KEY == carid1
												   select s13s.金額).Sum();
						}

						if ((from s13s in context.S13_DRVSB
							 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月4 && s13s.乗務員KEY == carid1
							 select s13s.金額).Any())
						{
							syaryoutyokusetuhi4 = (from s13s in context.S13_DRVSB
												   where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月4 && s13s.乗務員KEY == carid1
												   select s13s.金額).Sum();
						}

						if ((from s13s in context.S13_DRVSB
							 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月5 && s13s.乗務員KEY == carid1
							 select s13s.金額).Any())
						{
							syaryoutyokusetuhi5 = (from s13s in context.S13_DRVSB
												   where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月5 && s13s.乗務員KEY == carid1
												   select s13s.金額).Sum();
						}

						if ((from s13s in context.S13_DRVSB
							 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月6 && s13s.乗務員KEY == carid1
							 select s13s.金額).Any())
						{
							syaryoutyokusetuhi6 = (from s13s in context.S13_DRVSB
												   where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月6 && s13s.乗務員KEY == carid1
												   select s13s.金額).Sum();
						}

						if ((from s13s in context.S13_DRVSB
							 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月7 && s13s.乗務員KEY == carid1
							 select s13s.金額).Any())
						{
							syaryoutyokusetuhi7 = (from s13s in context.S13_DRVSB
												   where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月7 && s13s.乗務員KEY == carid1
												   select s13s.金額).Sum();
						}

						if ((from s13s in context.S13_DRVSB
							 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月8 && s13s.乗務員KEY == carid1
							 select s13s.金額).Any())
						{
							syaryoutyokusetuhi8 = (from s13s in context.S13_DRVSB
												   where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月8 && s13s.乗務員KEY == carid1
												   select s13s.金額).Sum();
						}

						if ((from s13s in context.S13_DRVSB
							 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月9 && s13s.乗務員KEY == carid1
							 select s13s.金額).Any())
						{
							syaryoutyokusetuhi9 = (from s13s in context.S13_DRVSB
												   where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月9 && s13s.乗務員KEY == carid1
												   select s13s.金額).Sum();
						}

						if ((from s13s in context.S13_DRVSB
							 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月10 && s13s.乗務員KEY == carid1
							 select s13s.金額).Any())
						{
							syaryoutyokusetuhi10 = (from s13s in context.S13_DRVSB
													where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月10 && s13s.乗務員KEY == carid1
													select s13s.金額).Sum();
						}

						if ((from s13s in context.S13_DRVSB
							 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月11 && s13s.乗務員KEY == carid1
							 select s13s.金額).Any())
						{
							syaryoutyokusetuhi11 = (from s13s in context.S13_DRVSB
													where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月11 && s13s.乗務員KEY == carid1
													select s13s.金額).Sum();
						}

						if ((from s13s in context.S13_DRVSB
							 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月12 && s13s.乗務員KEY == carid1
							 select s13s.金額).Any())
						{
							syaryoutyokusetuhi12 = (from s13s in context.S13_DRVSB
													where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 == i年月12 && s13s.乗務員KEY == carid1
													select s13s.金額).Sum();
						}

						if ((from s13s in context.S13_DRVSB
							 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 >= i年月1 && s13s.集計年月 <= i年月12 && s13s.乗務員KEY == carid1
							 select s13s.金額).Any())
						{
							syaryoutyokusetuhi13 = (from s13s in context.S13_DRVSB
													where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 >= i年月1 && s13s.集計年月 <= i年月12 && s13s.乗務員KEY == carid1
													select s13s.金額).Sum();
						}

						if ((from s13s in context.S13_DRVSB
							 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 >= i年月1 && s13s.集計年月 <= i年月12 && s13s.乗務員KEY == carid1
							 select s13s.金額).Any())
						{
							syaryoutyokusetuhi14 = (from s13s in context.S13_DRVSB
													where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 >= i年月1 && s13s.集計年月 <= i年月12 && s13s.乗務員KEY == carid1
													select s13s.金額).Sum() / 12;
						}
						if ((from s13s in context.S13_DRVSB
							 where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 >= i年月1 && s13s.集計年月 <= i年月12 && s13s.乗務員KEY == carid1
							 select s13s.金額).Any())
						{
							if (a1 != null && a1.運送収入13 != 0)
							{
								syaryoutyokusetuhi15 = (from s13s in context.S13_DRVSB
														where prt_GROPID.Contains(s13s.経費項目ID) && s13s.集計年月 >= i年月1 && s13s.集計年月 <= i年月12 && s13s.乗務員KEY == carid1
														select s13s.金額).Sum() / (decimal)a1.運送収入13 * 100;
							}
						}

						dDate = new DateTime(年, 月, 1);
						JMI13010_PRELIST_Member_CSV list = new JMI13010_PRELIST_Member_CSV()
						{
							乗務員ID = a1.乗務員ID,
							乗務員名 = a1.主乗務員,
							項目名 = "運送収入",
							金額1 = a1.運送収入1 == null ? 0 : (decimal)a1.運送収入1,
							金額2 = a1.運送収入2 == null ? 0 : (decimal)a1.運送収入2,
							金額3 = a1.運送収入3 == null ? 0 : (decimal)a1.運送収入3,
							金額4 = a1.運送収入4 == null ? 0 : (decimal)a1.運送収入4,
							金額5 = a1.運送収入5 == null ? 0 : (decimal)a1.運送収入5,
							金額6 = a1.運送収入6 == null ? 0 : (decimal)a1.運送収入6,
							金額7 = a1.運送収入7 == null ? 0 : (decimal)a1.運送収入7,
							金額8 = a1.運送収入8 == null ? 0 : (decimal)a1.運送収入8,
							金額9 = a1.運送収入9 == null ? 0 : (decimal)a1.運送収入9,
							金額10 = a1.運送収入10 == null ? 0 : (decimal)a1.運送収入10,
							金額11 = a1.運送収入11 == null ? 0 : (decimal)a1.運送収入11,
							金額12 = a1.運送収入12 == null ? 0 : (decimal)a1.運送収入12,
							年合計 = a1.運送収入13 == null ? 0 : (decimal)a1.運送収入13,
							平均 = a1.運送収入14 == null ? 0 : (decimal)a1.運送収入14,
							対売上 = (a1.運送収入13 == null || a1.運送収入13 == 0) ? 0 : Math.Round((decimal)(a1.運送収入13 / a1.運送収入13 * 100), 1, MidpointRounding.AwayFromZero),
							月1 = dDate.Month.ToString() + "月",
							月2 = dDate.AddMonths(1).Month.ToString() + "月",
							月3 = dDate.AddMonths(2).Month.ToString() + "月",
							月4 = dDate.AddMonths(3).Month.ToString() + "月",
							月5 = dDate.AddMonths(4).Month.ToString() + "月",
							月6 = dDate.AddMonths(5).Month.ToString() + "月",
							月7 = dDate.AddMonths(6).Month.ToString() + "月",
							月8 = dDate.AddMonths(7).Month.ToString() + "月",
							月9 = dDate.AddMonths(8).Month.ToString() + "月",
							月10 = dDate.AddMonths(9).Month.ToString() + "月",
							月11 = dDate.AddMonths(10).Month.ToString() + "月",
							月12 = dDate.AddMonths(11).Month.ToString() + "月",
						};
						retList.Add(list);

						foreach (var row in plist)
						{

							list = new JMI13010_PRELIST_Member_CSV()
							{
								乗務員ID = a1.乗務員ID,
								乗務員名 = a1.主乗務員,
								項目名 = row.経費項目名,
								金額1 = row.経費項目名 == "限界利益" ? a1.限界利益1 == null ? 0 : (decimal)a1.限界利益1 : (decimal)row.金額1,
								金額2 = row.経費項目名 == "限界利益" ? a1.限界利益2 == null ? 0 : (decimal)a1.限界利益2 : (decimal)row.金額2,
								金額3 = row.経費項目名 == "限界利益" ? a1.限界利益3 == null ? 0 : (decimal)a1.限界利益3 : (decimal)row.金額3,
								金額4 = row.経費項目名 == "限界利益" ? a1.限界利益4 == null ? 0 : (decimal)a1.限界利益4 : (decimal)row.金額4,
								金額5 = row.経費項目名 == "限界利益" ? a1.限界利益5 == null ? 0 : (decimal)a1.限界利益5 : (decimal)row.金額5,
								金額6 = row.経費項目名 == "限界利益" ? a1.限界利益6 == null ? 0 : (decimal)a1.限界利益6 : (decimal)row.金額6,
								金額7 = row.経費項目名 == "限界利益" ? a1.限界利益7 == null ? 0 : (decimal)a1.限界利益7 : (decimal)row.金額7,
								金額8 = row.経費項目名 == "限界利益" ? a1.限界利益8 == null ? 0 : (decimal)a1.限界利益8 : (decimal)row.金額8,
								金額9 = row.経費項目名 == "限界利益" ? a1.限界利益9 == null ? 0 : (decimal)a1.限界利益9 : (decimal)row.金額9,
								金額10 = row.経費項目名 == "限界利益" ? a1.限界利益10 == null ? 0 : (decimal)a1.限界利益10 : (decimal)row.金額10,
								金額11 = row.経費項目名 == "限界利益" ? a1.限界利益11 == null ? 0 : (decimal)a1.限界利益11 : (decimal)row.金額11,
								金額12 = row.経費項目名 == "限界利益" ? a1.限界利益12 == null ? 0 : (decimal)a1.限界利益12 : (decimal)row.金額12,
								年合計 = row.経費項目名 == "限界利益" ? a1.限界利益13 == null ? 0 : (decimal)a1.限界利益13 : (decimal)row.金額13,
								平均 = row.経費項目名 == "限界利益" ? a1.限界利益13 == null ? 0 : Math.Round((decimal)(a1.限界利益13 / 12), 0, MidpointRounding.AwayFromZero) : Math.Round((decimal)(row.金額13 / 12), 0, MidpointRounding.AwayFromZero),
								対売上 = row.経費項目名 == "限界利益" ? (a1.運送収入13 == null || a1.運送収入13 == 0) ? 0 : Math.Round((decimal)(a1.限界利益13 / a1.運送収入13 * 100), 1, MidpointRounding.AwayFromZero) : (a1.運送収入13 == null || a1.運送収入13 == 0) ? 0 : Math.Round((decimal)(row.金額13 / a1.運送収入13 * 100), 1, MidpointRounding.AwayFromZero),
							};
							retList.Add(list);

						};

						foreach (var row in plist)
						{

							list = new JMI13010_PRELIST_Member_CSV()
							{
								乗務員ID = a1.乗務員ID,
								乗務員名 = a1.主乗務員,
								項目名 = row.経費項目名,
								金額1 = row.経費項目名 == "限界利益" ? a1.限界利益1 == null ? 0 : (decimal)a1.限界利益1 : (decimal)row.金額1,
								金額2 = row.経費項目名 == "限界利益" ? a1.限界利益2 == null ? 0 : (decimal)a1.限界利益2 : (decimal)row.金額2,
								金額3 = row.経費項目名 == "限界利益" ? a1.限界利益3 == null ? 0 : (decimal)a1.限界利益3 : (decimal)row.金額3,
								金額4 = row.経費項目名 == "限界利益" ? a1.限界利益4 == null ? 0 : (decimal)a1.限界利益4 : (decimal)row.金額4,
								金額5 = row.経費項目名 == "限界利益" ? a1.限界利益5 == null ? 0 : (decimal)a1.限界利益5 : (decimal)row.金額5,
								金額6 = row.経費項目名 == "限界利益" ? a1.限界利益6 == null ? 0 : (decimal)a1.限界利益6 : (decimal)row.金額6,
								金額7 = row.経費項目名 == "限界利益" ? a1.限界利益7 == null ? 0 : (decimal)a1.限界利益7 : (decimal)row.金額7,
								金額8 = row.経費項目名 == "限界利益" ? a1.限界利益8 == null ? 0 : (decimal)a1.限界利益8 : (decimal)row.金額8,
								金額9 = row.経費項目名 == "限界利益" ? a1.限界利益9 == null ? 0 : (decimal)a1.限界利益9 : (decimal)row.金額9,
								金額10 = row.経費項目名 == "限界利益" ? a1.限界利益10 == null ? 0 : (decimal)a1.限界利益10 : (decimal)row.金額10,
								金額11 = row.経費項目名 == "限界利益" ? a1.限界利益11 == null ? 0 : (decimal)a1.限界利益11 : (decimal)row.金額11,
								金額12 = row.経費項目名 == "限界利益" ? a1.限界利益12 == null ? 0 : (decimal)a1.限界利益12 : (decimal)row.金額12,
								年合計 = row.経費項目名 == "限界利益" ? a1.限界利益13 == null ? 0 : (decimal)a1.限界利益13 : (decimal)row.金額13,
								平均 = row.経費項目名 == "限界利益" ? a1.限界利益13 == null ? 0 : Math.Round((decimal)(a1.限界利益13 / 12), 0, MidpointRounding.AwayFromZero) : Math.Round((decimal)(row.金額13 / 12), 0, MidpointRounding.AwayFromZero),
								対売上 = row.経費項目名 == "限界利益" ? (a1.運送収入13 == null || a1.運送収入13 == 0) ? 0 : Math.Round((decimal)(a1.限界利益13 / a1.運送収入13 * 100), 1, MidpointRounding.AwayFromZero) : (a1.運送収入13 == null || a1.運送収入13 == 0) ? 0 : Math.Round((decimal)(row.金額13 / a1.運送収入13 * 100), 1, MidpointRounding.AwayFromZero),

							};
							retList.Add(list);
						};

						list = new JMI13010_PRELIST_Member_CSV()
						{
							乗務員ID = a1.乗務員ID,
							乗務員名 = a1.主乗務員,
							項目名 = "車輌直接費",
							金額1 = syaryoutyokusetuhi1 == null ? 0 : (decimal)syaryoutyokusetuhi1,
							金額2 = syaryoutyokusetuhi2 == null ? 0 : (decimal)syaryoutyokusetuhi2,
							金額3 = syaryoutyokusetuhi3 == null ? 0 : (decimal)syaryoutyokusetuhi3,
							金額4 = syaryoutyokusetuhi4 == null ? 0 : (decimal)syaryoutyokusetuhi4,
							金額5 = syaryoutyokusetuhi5 == null ? 0 : (decimal)syaryoutyokusetuhi5,
							金額6 = syaryoutyokusetuhi6 == null ? 0 : (decimal)syaryoutyokusetuhi6,
							金額7 = syaryoutyokusetuhi7 == null ? 0 : (decimal)syaryoutyokusetuhi7,
							金額8 = syaryoutyokusetuhi8 == null ? 0 : (decimal)syaryoutyokusetuhi8,
							金額9 = syaryoutyokusetuhi9 == null ? 0 : (decimal)syaryoutyokusetuhi9,
							金額10 = syaryoutyokusetuhi10 == null ? 0 : (decimal)syaryoutyokusetuhi10,
							金額11 = syaryoutyokusetuhi11 == null ? 0 : (decimal)syaryoutyokusetuhi11,
							金額12 = syaryoutyokusetuhi12 == null ? 0 : (decimal)syaryoutyokusetuhi12,
							年合計 = syaryoutyokusetuhi13 == null ? 0 : (decimal)syaryoutyokusetuhi13,
							平均 = syaryoutyokusetuhi13 == null ? 0 : Math.Round((decimal)(syaryoutyokusetuhi13 / 12), 0, MidpointRounding.AwayFromZero),
							対売上 = (a1.運送収入13 == null || a1.運送収入13 == 0) ? 0 : Math.Round((decimal)(syaryoutyokusetuhi13 / a1.運送収入13 * 100), 1, MidpointRounding.AwayFromZero),
						};
						retList.Add(list);

						list = new JMI13010_PRELIST_Member_CSV()
						{
							乗務員ID = a1.乗務員ID,
							乗務員名 = a1.主乗務員,
							項目名 = "車輌直接益",
							金額1 = a1.運送収入1 - syaryoutyokusetuhi1 == null ? 0 : (decimal)a1.運送収入1 - syaryoutyokusetuhi1,
							金額2 = a1.運送収入2 - syaryoutyokusetuhi2 == null ? 0 : (decimal)a1.運送収入2 - syaryoutyokusetuhi2,
							金額3 = a1.運送収入3 - syaryoutyokusetuhi3 == null ? 0 : (decimal)a1.運送収入3 - syaryoutyokusetuhi3,
							金額4 = a1.運送収入4 - syaryoutyokusetuhi4 == null ? 0 : (decimal)a1.運送収入4 - syaryoutyokusetuhi4,
							金額5 = a1.運送収入5 - syaryoutyokusetuhi5 == null ? 0 : (decimal)a1.運送収入5 - syaryoutyokusetuhi5,
							金額6 = a1.運送収入6 - syaryoutyokusetuhi6 == null ? 0 : (decimal)a1.運送収入6 - syaryoutyokusetuhi6,
							金額7 = a1.運送収入7 - syaryoutyokusetuhi7 == null ? 0 : (decimal)a1.運送収入7 - syaryoutyokusetuhi7,
							金額8 = a1.運送収入8 - syaryoutyokusetuhi8 == null ? 0 : (decimal)a1.運送収入8 - syaryoutyokusetuhi8,
							金額9 = a1.運送収入9 - syaryoutyokusetuhi9 == null ? 0 : (decimal)a1.運送収入9 - syaryoutyokusetuhi9,
							金額10 = a1.運送収入10 - syaryoutyokusetuhi10 == null ? 0 : (decimal)a1.運送収入10 - syaryoutyokusetuhi10,
							金額11 = a1.運送収入11 - syaryoutyokusetuhi11 == null ? 0 : (decimal)a1.運送収入11 - syaryoutyokusetuhi11,
							金額12 = a1.運送収入12 - syaryoutyokusetuhi12 == null ? 0 : (decimal)a1.運送収入12 - syaryoutyokusetuhi12,
							年合計 = a1.運送収入13 - syaryoutyokusetuhi13 == null ? 0 : (decimal)a1.運送収入13 - syaryoutyokusetuhi13,
							平均 = a1.運送収入13 - syaryoutyokusetuhi13 == null ? 0 : Math.Round((decimal)((a1.運送収入13 - syaryoutyokusetuhi13) / 12), 0, MidpointRounding.AwayFromZero),
							対売上 = (a1.運送収入13 == null || a1.運送収入13 == 0) ? 0 : Math.Round((decimal)((a1.運送収入13 - syaryoutyokusetuhi13) / a1.運送収入13 * 100), 1, MidpointRounding.AwayFromZero),
						};
						retList.Add(list);

						list = new JMI13010_PRELIST_Member_CSV()
						{
							乗務員ID = a1.乗務員ID,
							乗務員名 = a1.主乗務員,
							項目名 = "一般管理費",
							金額1 = a1.一般管理費1 == null ? 0 : (decimal)a1.一般管理費1,
							金額2 = a1.一般管理費2 == null ? 0 : (decimal)a1.一般管理費2,
							金額3 = a1.一般管理費3 == null ? 0 : (decimal)a1.一般管理費3,
							金額4 = a1.一般管理費4 == null ? 0 : (decimal)a1.一般管理費4,
							金額5 = a1.一般管理費5 == null ? 0 : (decimal)a1.一般管理費5,
							金額6 = a1.一般管理費6 == null ? 0 : (decimal)a1.一般管理費6,
							金額7 = a1.一般管理費7 == null ? 0 : (decimal)a1.一般管理費7,
							金額8 = a1.一般管理費8 == null ? 0 : (decimal)a1.一般管理費8,
							金額9 = a1.一般管理費9 == null ? 0 : (decimal)a1.一般管理費9,
							金額10 = a1.一般管理費10 == null ? 0 : (decimal)a1.一般管理費10,
							金額11 = a1.一般管理費11 == null ? 0 : (decimal)a1.一般管理費11,
							金額12 = a1.一般管理費12 == null ? 0 : (decimal)a1.一般管理費12,
							年合計 = a1.一般管理費13 == null ? 0 : (decimal)a1.一般管理費13,
							平均 = a1.一般管理費13 == null ? 0 : Math.Round((decimal)(a1.一般管理費13 / 12), 0, MidpointRounding.AwayFromZero),
							対売上 = (a1.運送収入13 == null || a1.運送収入13 == 0) ? 0 : Math.Round((decimal)(a1.一般管理費13 / a1.運送収入13 * 100), 1, MidpointRounding.AwayFromZero),
						};
						retList.Add(list);

						list = new JMI13010_PRELIST_Member_CSV()
						{
							乗務員ID = a1.乗務員ID,
							乗務員名 = a1.主乗務員,
							項目名 = "当月利益",
							金額1 = a1.運送収入1 - syaryoutyokusetuhi1 - a1.一般管理費1 == null ? 0 : (decimal)(a1.運送収入1 - syaryoutyokusetuhi1 - a1.一般管理費1),
							金額2 = a1.運送収入2 - syaryoutyokusetuhi2 - a1.一般管理費2 == null ? 0 : (decimal)(a1.運送収入2 - syaryoutyokusetuhi2 - a1.一般管理費2),
							金額3 = a1.運送収入3 - syaryoutyokusetuhi3 - a1.一般管理費3 == null ? 0 : (decimal)(a1.運送収入3 - syaryoutyokusetuhi3 - a1.一般管理費3),
							金額4 = a1.運送収入4 - syaryoutyokusetuhi4 - a1.一般管理費4 == null ? 0 : (decimal)(a1.運送収入4 - syaryoutyokusetuhi4 - a1.一般管理費4),
							金額5 = a1.運送収入5 - syaryoutyokusetuhi5 - a1.一般管理費5 == null ? 0 : (decimal)(a1.運送収入5 - syaryoutyokusetuhi5 - a1.一般管理費5),
							金額6 = a1.運送収入6 - syaryoutyokusetuhi6 - a1.一般管理費6 == null ? 0 : (decimal)(a1.運送収入6 - syaryoutyokusetuhi6 - a1.一般管理費6),
							金額7 = a1.運送収入7 - syaryoutyokusetuhi7 - a1.一般管理費7 == null ? 0 : (decimal)(a1.運送収入7 - syaryoutyokusetuhi7 - a1.一般管理費7),
							金額8 = a1.運送収入8 - syaryoutyokusetuhi8 - a1.一般管理費8 == null ? 0 : (decimal)(a1.運送収入8 - syaryoutyokusetuhi8 - a1.一般管理費8),
							金額9 = a1.運送収入9 - syaryoutyokusetuhi9 - a1.一般管理費9 == null ? 0 : (decimal)(a1.運送収入9 - syaryoutyokusetuhi9 - a1.一般管理費9),
							金額10 = a1.運送収入10 - syaryoutyokusetuhi10 - a1.一般管理費10 == null ? 0 : (decimal)(a1.運送収入10 - syaryoutyokusetuhi10 - a1.一般管理費10),
							金額11 = a1.運送収入11 - syaryoutyokusetuhi11 - a1.一般管理費11 == null ? 0 : (decimal)(a1.運送収入11 - syaryoutyokusetuhi11 - a1.一般管理費11),
							金額12 = a1.運送収入12 - syaryoutyokusetuhi12 - a1.一般管理費12 == null ? 0 : (decimal)(a1.運送収入12 - syaryoutyokusetuhi12 - a1.一般管理費12),
							年合計 = a1.運送収入13 - syaryoutyokusetuhi13 - a1.一般管理費13 == null ? 0 : (decimal)(a1.運送収入13 - syaryoutyokusetuhi13 - a1.一般管理費13),
							平均 = a1.運送収入13 - syaryoutyokusetuhi13 - a1.一般管理費13 == null ? 0 : Math.Round((decimal)((a1.運送収入13 - syaryoutyokusetuhi13 - a1.一般管理費13) / 12), 0, MidpointRounding.AwayFromZero),
							対売上 = a1.運送収入15 - syaryoutyokusetuhi15 - a1.一般管理費15 == null ? 0 : Math.Round((decimal)((a1.運送収入15 - syaryoutyokusetuhi15 - a1.一般管理費15) * 100), 1, MidpointRounding.AwayFromZero),
						};
						retList.Add(list);

						carid1 = 0;

					};



					return retList;

				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}
		#endregion


    }
}