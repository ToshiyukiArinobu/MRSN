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
    public class JMI12010g_Member
    {

        [DataMember]
        public int 乗務員KEY { get; set; }
        [DataMember]
        public int 乗務員ID { get; set; }
		[DataMember]
		public int? 車種ID { get; set; }
		[DataMember]
		public int 年月 { get; set; }
		[DataMember]
		public DateTime? 退職年月日 { get; set; }
		[DataMember]
        public string 車輌番号 { get; set; }
        [DataMember]
        public string 主乗務員 { get; set; }
        [DataMember]
        public string 車種 { get; set; }
        [DataMember]
        public decimal? 運送収入 { get; set; }
        [DataMember]
        public decimal? 限界利益 { get; set; }
        [DataMember]
        public decimal? 小計1 { get; set; }
        [DataMember]
        public decimal? 小計2 { get; set; }
        [DataMember]
        public decimal? 小計3 { get; set; }

        [DataMember]
        public decimal? 車輌直接費 { get; set; }
        [DataMember]
        public decimal? 車輌直接益 { get; set; }
        [DataMember]
        public decimal? 一般管理費 { get; set; }
        [DataMember]
        public decimal? 当月利益 { get; set; }
        [DataMember]
        public int? 稼働日数 { get; set; }
        [DataMember]
        public int? 実車KM { get; set; }
        [DataMember]
        public int? 空車KM { get; set; }
        [DataMember]
        public int? 走行KM { get; set; }
        [DataMember]
        public decimal 燃料L { get; set; }
        [DataMember]
        public decimal 燃費 { get; set; }
        [DataMember]
        public decimal 収入1KM { get; set; }
        [DataMember]
        public decimal 原価1KM { get; set; }
    }

    public class JMI12010_KEI_Member
    {
        [DataMember]
        public int 印刷グループID { get; set; }
        [DataMember]
        public int 経費ID { get; set; }
        [DataMember]
        public string 経費項目名 { get; set; }
        [DataMember]
        public decimal 金額 { get; set; }
    }

    public class JMI12010_PRELIST_Member
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
        public decimal 金額 { get; set; }
    }


    #region JMI12010_Member

    public class JMI12010_Member
    {
        [DataMember]
        public int? 乗務員ID1 { get; set; }
        [DataMember]
        public string 車輌番号1 { get; set; }
        [DataMember]
        public string 主乗務員1 { get; set; }
        [DataMember]
        public string 車種1 { get; set; }
        [DataMember]
        public decimal? 運送収入1 { get; set; }

        [DataMember]
        public int? 乗務員ID2 { get; set; }
        [DataMember]
        public string 車輌番号2 { get; set; }
        [DataMember]
        public string 主乗務員2 { get; set; }
        [DataMember]
        public string 車種2 { get; set; }
        [DataMember]
        public decimal? 運送収入2 { get; set; }

        [DataMember]
        public int? 乗務員ID3 { get; set; }
        [DataMember]
        public string 車輌番号3 { get; set; }
        [DataMember]
        public string 主乗務員3 { get; set; }
        [DataMember]
        public string 車種3 { get; set; }
        [DataMember]
        public decimal? 運送収入3 { get; set; }

        [DataMember]
        public int? 乗務員ID4 { get; set; }
        [DataMember]
        public string 車輌番号4 { get; set; }
        [DataMember]
        public string 主乗務員4 { get; set; }
        [DataMember]
        public string 車種4 { get; set; }
        [DataMember]
        public decimal? 運送収入4 { get; set; }

        [DataMember]
        public int? 乗務員ID5 { get; set; }
        [DataMember]
        public string 車輌番号5 { get; set; }
        [DataMember]
        public string 主乗務員5 { get; set; }
        [DataMember]
        public string 車種5 { get; set; }
        [DataMember]
        public decimal? 運送収入5 { get; set; }

        [DataMember]
        public int? 乗務員ID6 { get; set; }
        [DataMember]
        public string 車輌番号6 { get; set; }
        [DataMember]
        public string 主乗務員6 { get; set; }
        [DataMember]
        public string 車種6 { get; set; }
        [DataMember]
        public decimal? 運送収入6 { get; set; }

        [DataMember]
        public decimal? 車輌直接費1 { get; set; }
        [DataMember]
        public decimal? 車輌直接益1 { get; set; }
        [DataMember]
        public decimal? 一般管理費1 { get; set; }
        [DataMember]
        public decimal? 当月利益1 { get; set; }
        [DataMember]
        public int? 稼働日数1 { get; set; }
        [DataMember]
        public int? 実車KM1 { get; set; }
        [DataMember]
        public int? 空車KM1 { get; set; }
        [DataMember]
        public int? 走行KM1 { get; set; }
        [DataMember]
        public decimal? 燃料L1 { get; set; }
        [DataMember]
        public decimal? 燃費1 { get; set; }
        [DataMember]
        public decimal? 収入1KM1 { get; set; }
        [DataMember]
        public decimal? 原価1KM1 { get; set; }

        [DataMember]
        public decimal? 車輌直接費2 { get; set; }
        [DataMember]
        public decimal? 車輌直接益2 { get; set; }
        [DataMember]
        public decimal? 一般管理費2 { get; set; }
        [DataMember]
        public decimal? 当月利益2 { get; set; }
        [DataMember]
        public int? 稼働日数2 { get; set; }
        [DataMember]
        public int? 実車KM2 { get; set; }
        [DataMember]
        public int? 空車KM2 { get; set; }
        [DataMember]
        public int? 走行KM2 { get; set; }
        [DataMember]
        public decimal? 燃料L2 { get; set; }
        [DataMember]
        public decimal? 燃費2 { get; set; }
        [DataMember]
        public decimal? 収入1KM2 { get; set; }
        [DataMember]
        public decimal? 原価1KM2 { get; set; }

        [DataMember]
        public decimal? 車輌直接費3 { get; set; }
        [DataMember]
        public decimal? 車輌直接益3 { get; set; }
        [DataMember]
        public decimal? 一般管理費3 { get; set; }
        [DataMember]
        public decimal? 当月利益3 { get; set; }
        [DataMember]
        public int? 稼働日数3 { get; set; }
        [DataMember]
        public int? 実車KM3 { get; set; }
        [DataMember]
        public int? 空車KM3 { get; set; }
        [DataMember]
        public int? 走行KM3 { get; set; }
        [DataMember]
        public decimal? 燃料L3 { get; set; }
        [DataMember]
        public decimal? 燃費3 { get; set; }
        [DataMember]
        public decimal? 収入1KM3 { get; set; }
        [DataMember]
        public decimal? 原価1KM3 { get; set; }

        [DataMember]
        public decimal? 車輌直接費4 { get; set; }
        [DataMember]
        public decimal? 車輌直接益4 { get; set; }
        [DataMember]
        public decimal? 一般管理費4 { get; set; }
        [DataMember]
        public decimal? 当月利益4 { get; set; }
        [DataMember]
        public int? 稼働日数4 { get; set; }
        [DataMember]
        public int? 実車KM4 { get; set; }
        [DataMember]
        public int? 空車KM4 { get; set; }
        [DataMember]
        public int? 走行KM4 { get; set; }
        [DataMember]
        public decimal? 燃料L4 { get; set; }
        [DataMember]
        public decimal? 燃費4 { get; set; }
        [DataMember]
        public decimal? 収入1KM4 { get; set; }
        [DataMember]
        public decimal? 原価1KM4 { get; set; }

        [DataMember]
        public decimal? 車輌直接費5 { get; set; }
        [DataMember]
        public decimal? 車輌直接益5 { get; set; }
        [DataMember]
        public decimal? 一般管理費5 { get; set; }
        [DataMember]
        public decimal? 当月利益5 { get; set; }
        [DataMember]
        public int? 稼働日数5 { get; set; }
        [DataMember]
        public int? 実車KM5 { get; set; }
        [DataMember]
        public int? 空車KM5 { get; set; }
        [DataMember]
        public int? 走行KM5 { get; set; }
        [DataMember]
        public decimal? 燃料L5 { get; set; }
        [DataMember]
        public decimal? 燃費5 { get; set; }
        [DataMember]
        public decimal? 収入1KM5 { get; set; }
        [DataMember]
        public decimal? 原価1KM5 { get; set; }

        [DataMember]
        public decimal? 車輌直接費6 { get; set; }
        [DataMember]
        public decimal? 車輌直接益6 { get; set; }
        [DataMember]
        public decimal? 一般管理費6 { get; set; }
        [DataMember]
        public decimal? 当月利益6 { get; set; }
        [DataMember]
        public int? 稼働日数6 { get; set; }
        [DataMember]
        public int? 実車KM6 { get; set; }
        [DataMember]
        public int? 空車KM6 { get; set; }
        [DataMember]
        public int? 走行KM6 { get; set; }
        [DataMember]
        public decimal? 燃料L6 { get; set; }
        [DataMember]
        public decimal? 燃費6 { get; set; }
        [DataMember]
        public decimal? 収入1KM6 { get; set; }
        [DataMember]
        public decimal? 原価1KM6 { get; set; }


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

    #region JMI12010_Member_CSV

    public class JMI12010_Member_CSV
    {
        [DataMember]
        public int 乗務員ID { get; set; }
        [DataMember]
        public int 乗務員KEY { get; set; }
        [DataMember]
        public string 車輌番号 { get; set; }
        [DataMember]
        public string 主乗務員 { get; set; }
        [DataMember]
        public int? 車種ID { get; set; }
        [DataMember]
        public string 車種{ get; set; }
        [DataMember]
        public decimal? 運送収入 { get; set; }

        [DataMember]
        public decimal? 車輌直接費 { get; set; }
        [DataMember]
        public decimal? 車輌直接益 { get; set; }
        [DataMember]
        public decimal? 一般管理費 { get; set; }
        [DataMember]
        public decimal? 当月利益 { get; set; }
        [DataMember]
        public int? 稼働日数 { get; set; }
        [DataMember]
        public int? 実車KM { get; set; }
        [DataMember]
        public int? 空車KM { get; set; }
        [DataMember]
        public int? 走行KM { get; set; }
        [DataMember]
        public decimal? 燃料L { get; set; }
        [DataMember]
        public decimal? 燃費 { get; set; }
        [DataMember]
        public decimal? 収入1KM { get; set; }
        [DataMember]
        public decimal? 原価1KM { get; set; }

    

        [DataMember]
        public int 経費ID1 { get; set; }
        [DataMember]
        public string 経費項目名1 { get; set; }
        [DataMember]
        public decimal? 金額1 { get; set; }

        [DataMember]
        public int 経費ID2 { get; set; }
        [DataMember]
        public string 経費項目名2 { get; set; }
        [DataMember]
        public decimal? 金額2 { get; set; }

        [DataMember]
        public int 経費ID3 { get; set; }
        [DataMember]
        public string 経費項目名3 { get; set; }
        [DataMember]
        public decimal? 金額3 { get; set; }

        [DataMember]
        public int 経費ID4 { get; set; }
        [DataMember]
        public string 経費項目名4 { get; set; }
        [DataMember]
        public decimal? 金額4 { get; set; }

        [DataMember]
        public int 経費ID5 { get; set; }
        [DataMember]
        public string 経費項目名5 { get; set; }
        [DataMember]
        public decimal? 金額5 { get; set; }

        [DataMember]
        public int 経費ID6 { get; set; }
        [DataMember]
        public string 経費項目名6 { get; set; }
        [DataMember]
        public decimal? 金額6 { get; set; }

        [DataMember]
        public int 経費ID7 { get; set; }
        [DataMember]
        public string 経費項目名7 { get; set; }
        [DataMember]
        public decimal? 金額7 { get; set; }

        [DataMember]
        public int 経費ID8 { get; set; }
        [DataMember]
        public string 経費項目名8 { get; set; }
        [DataMember]
        public decimal? 金額8 { get; set; }

        [DataMember]
        public int 経費ID9 { get; set; }
        [DataMember]
        public string 経費項目名9 { get; set; }
        [DataMember]
        public decimal? 金額9 { get; set; }

        [DataMember]
        public int 経費ID10 { get; set; }
        [DataMember]
        public string 経費項目名10 { get; set; }
        [DataMember]
        public decimal? 金額10 { get; set; }

        [DataMember]
        public int 経費ID11 { get; set; }
        [DataMember]
        public string 経費項目名11 { get; set; }
        [DataMember]
        public decimal? 金額11 { get; set; }

        [DataMember]
        public int 経費ID12 { get; set; }
        [DataMember]
        public string 経費項目名12 { get; set; }
        [DataMember]
        public decimal? 金額12 { get; set; }

        [DataMember]
        public int 経費ID13 { get; set; }
        [DataMember]
        public string 経費項目名13 { get; set; }
        [DataMember]
        public decimal? 金額13 { get; set; }

        [DataMember]
        public int 経費ID14 { get; set; }
        [DataMember]
        public string 経費項目名14 { get; set; }
        [DataMember]
        public decimal? 金額14 { get; set; }

        [DataMember]
        public int 経費ID15 { get; set; }
        [DataMember]
        public string 経費項目名15 { get; set; }
        [DataMember]
        public decimal? 金額15 { get; set; }

        [DataMember]
        public int 経費ID16 { get; set; }
        [DataMember]
        public string 経費項目名16 { get; set; }
        [DataMember]
        public decimal? 金額16 { get; set; }

        [DataMember]
        public int 経費ID17 { get; set; }
        [DataMember]
        public string 経費項目名17 { get; set; }
        [DataMember]
        public decimal? 金額17 { get; set; }

        [DataMember]
        public int 経費ID18 { get; set; }
        [DataMember]
        public string 経費項目名18 { get; set; }
        [DataMember]
        public decimal? 金額18 { get; set; }

        [DataMember]
        public int 経費ID19 { get; set; }
        [DataMember]
        public string 経費項目名19 { get; set; }
        [DataMember]
        public decimal? 金額19 { get; set; }

        [DataMember]
        public int 経費ID20 { get; set; }
        [DataMember]
        public string 経費項目名20 { get; set; }
        [DataMember]
        public decimal? 金額20 { get; set; }

        [DataMember]
        public int 経費ID21 { get; set; }
        [DataMember]
        public string 経費項目名21 { get; set; }
        [DataMember]
        public decimal? 金額21 { get; set; }

        [DataMember]
        public int 経費ID22 { get; set; }
        [DataMember]
        public string 経費項目名22 { get; set; }
        [DataMember]
        public decimal? 金額22 { get; set; }

        [DataMember]
        public int 経費ID23 { get; set; }
        [DataMember]
        public string 経費項目名23 { get; set; }
        [DataMember]
        public decimal? 金額23 { get; set; }

        [DataMember]
        public int 経費ID24 { get; set; }
        [DataMember]
        public string 経費項目名24 { get; set; }
        [DataMember]
        public decimal? 金額24 { get; set; }

        [DataMember]
        public int 経費ID25 { get; set; }
        [DataMember]
        public string 経費項目名25 { get; set; }
        [DataMember]
        public decimal? 金額25 { get; set; }

        [DataMember]
        public int 経費ID26 { get; set; }
        [DataMember]
        public string 経費項目名26 { get; set; }
        [DataMember]
        public decimal? 金額26 { get; set; }

        [DataMember]
        public int 経費ID27 { get; set; }
        [DataMember]
        public string 経費項目名27 { get; set; }
        [DataMember]
        public decimal? 金額27 { get; set; }

        [DataMember]
        public int 経費ID28 { get; set; }
        [DataMember]
        public string 経費項目名28 { get; set; }
        [DataMember]
        public decimal? 金額28 { get; set; }

        [DataMember]
        public int 経費ID29 { get; set; }
        [DataMember]
        public string 経費項目名29 { get; set; }
        [DataMember]
        public decimal? 金額29 { get; set; }

        [DataMember]
        public int 経費ID30 { get; set; }
        [DataMember]
        public string 経費項目名30 { get; set; }
        [DataMember]
        public decimal? 金額30 { get; set; }

        [DataMember]
        public int 経費ID31 { get; set; }
        [DataMember]
        public string 経費項目名31 { get; set; }
        [DataMember]
        public decimal? 金額31 { get; set; }

        [DataMember]
        public int 経費ID32 { get; set; }
        [DataMember]
        public string 経費項目名32 { get; set; }
        [DataMember]
        public decimal? 金額32 { get; set; }

        [DataMember]
        public int 経費ID33 { get; set; }
        [DataMember]
        public string 経費項目名33 { get; set; }
        [DataMember]
        public decimal? 金額33 { get; set; }

        [DataMember]
        public int 経費ID34 { get; set; }
        [DataMember]
        public string 経費項目名34 { get; set; }
        [DataMember]
        public decimal? 金額34 { get; set; }

        [DataMember]
        public int 経費ID35 { get; set; }
        [DataMember]
        public string 経費項目名35 { get; set; }
        [DataMember]
        public decimal? 金額35 { get; set; }

        [DataMember]
        public int 経費ID36{ get; set; }
        [DataMember]
        public string 経費項目名36 { get; set; }
        [DataMember]
        public decimal? 金額36 { get; set; }

        [DataMember]
        public int 経費ID37 { get; set; }
        [DataMember]
        public string 経費項目名37 { get; set; }
        [DataMember]
        public decimal? 金額37 { get; set; }

        [DataMember]
        public int 経費ID38 { get; set; }
        [DataMember]
        public string 経費項目名38 { get; set; }
        [DataMember]
        public decimal? 金額38 { get; set; }

        [DataMember]
        public int 経費ID39 { get; set; }
        [DataMember]
        public string 経費項目名39 { get; set; }
        [DataMember]
        public decimal? 金額39 { get; set; }

        [DataMember]
        public int 経費ID40 { get; set; }
        [DataMember]
        public string 経費項目名40 { get; set; }
        [DataMember]
        public decimal? 金額40 { get; set; }



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

    public class JMI12010
    {
        #region JMI12010 印刷
        public List<JMI12010_Member> JMI12010_GetDataList(string s乗務員From, string s乗務員To, int?[] i乗務員List, string s乗務員List, int i年月, int 年, int 月)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {

                List<JMI12010_Member> retList = new List<JMI12010_Member>();

                context.Connection.Open();
                try
                {
                    string 車輌ﾋﾟｯｸｱｯﾌﾟ指定 = string.Empty;

                    var carlistz = (from m04 in context.M04_DRV.Where(c => c.削除日付 == null)
                                   join s13 in context.S13_DRV.Where(c => c.集計年月 == i年月) on m04.乗務員KEY equals s13.乗務員KEY
                                   join m05 in context.M05_CAR.Where(c => c.削除日付 == null).Take(1) on m04.乗務員KEY equals m05.乗務員KEY into m05Group
                                   from m05g in m05Group.DefaultIfEmpty()
                                   join m06 in context.M06_SYA.Where(c => c.削除日付 == null) on m05g.車種ID equals m06.車種ID into m06Group
                                   from m06g in m06Group.DefaultIfEmpty()
                                   orderby m04.乗務員ID
                                   select new JMI12010g_Member
                                   {
                                       乗務員ID = m04.乗務員ID,
                                       乗務員KEY = m04.乗務員KEY,
									   退職年月日 = m04.退職年月日,
									   年月 = s13.集計年月,
                                       車種ID = m05g.車種ID,
                                       車輌番号 = m05g.車輌番号,
                                       主乗務員 = m04.乗務員名,
                                       車種 = m06g.車種名,
                                       運送収入 = s13.運送収入,

                                       一般管理費 = s13.一般管理費,

                                       稼働日数 = s13.稼動日数,
                                       実車KM = s13.実車ＫＭ,
                                       空車KM = s13.走行ＫＭ - s13.実車ＫＭ,
                                       走行KM = s13.走行ＫＭ,
                                       燃料L = s13.燃料Ｌ == null ? 0 : s13.燃料Ｌ,
                                       燃費 = s13.燃料Ｌ == 0 ? 0 : s13.燃料Ｌ == null ? 0 : s13.走行ＫＭ / s13.燃料Ｌ,
                                       収入1KM = s13.走行ＫＭ == 0 ? 0 : s13.走行ＫＭ == null ? 0 : s13.運送収入 / s13.走行ＫＭ,
                                   }).AsQueryable();

                    if (!(string.IsNullOrEmpty(s乗務員From + s乗務員To) && i乗務員List.Length == 0))
                    {
                        if (string.IsNullOrEmpty(s乗務員From + s乗務員To))
                        {
                            carlistz = carlistz.Where(c => c.乗務員ID >= int.MaxValue);
                        }

                        //車輌From処理　Min値
                        if (!string.IsNullOrEmpty(s乗務員From))
                        {
                            int i乗務員From = AppCommon.IntParse(s乗務員From);
                            carlistz = carlistz.Where(c => c.乗務員ID >= i乗務員From);
                        }

                        //車輌To処理　Max値
                        if (!string.IsNullOrEmpty(s乗務員To))
                        {
                            int i車輌TO = AppCommon.IntParse(s乗務員To);
                            carlistz = carlistz.Where(c => c.乗務員ID <= i車輌TO);
                        }

                        if (i乗務員List.Length > 0)
                        {
                            var intCause = i乗務員List;
                            carlistz = carlistz.Union(from m04 in context.M04_DRV.Where(c => c.削除日付 == null)
                                                      join s13 in context.S13_DRV.Where(c => c.集計年月 == i年月) on m04.乗務員KEY equals s13.乗務員KEY
                                                      join m05 in context.M05_CAR.Where(c => c.削除日付 == null).Take(1) on m04.乗務員KEY equals m05.乗務員KEY into m05Group
                                                      from m05g in m05Group.DefaultIfEmpty()
                                                      join m06 in context.M06_SYA.Where(c => c.削除日付 == null) on m05g.車種ID equals m06.車種ID into m06Group
                                                      from m06g in m06Group.DefaultIfEmpty()
													  orderby m04.乗務員ID
                                                      where intCause.Contains(m04.乗務員ID)
                                                      select new JMI12010g_Member
                                                      {
                                                          乗務員ID = m04.乗務員ID,
                                                          乗務員KEY = m04.乗務員KEY,
														  退職年月日 = m04.退職年月日,
														  年月 = s13.集計年月,
														  車種ID = m05g.車種ID,
                                                          車輌番号 = m05g.車輌番号,
                                                          主乗務員 = m04.乗務員名,
                                                          車種 = m06g.車種名,
                                                          運送収入 = s13.運送収入,

                                                          一般管理費 = s13.一般管理費,

                                                          稼働日数 = s13.稼動日数,
                                                          実車KM = s13.実車ＫＭ,
                                                          空車KM = s13.走行ＫＭ - s13.実車ＫＭ,
                                                          走行KM = s13.走行ＫＭ,
                                                          燃料L = s13.燃料Ｌ == null ? 0 : s13.燃料Ｌ,
                                                          燃費 = s13.燃料Ｌ == 0 ? 0 : s13.燃料Ｌ == null ? 0 : s13.走行ＫＭ / s13.燃料Ｌ,
                                                          収入1KM = s13.走行ＫＭ == 0 ? 0 : s13.走行ＫＭ == null ? 0 : s13.運送収入 / s13.走行ＫＭ,
                                                      });
                        };
                    }
                    else
                    {

                        //車輌From処理　Min値
                        if (!string.IsNullOrEmpty(s乗務員From))
                        {
                            int i乗務員From = AppCommon.IntParse(s乗務員From);
                            carlistz = carlistz.Where(c => c.乗務員ID >= i乗務員From);
                        }

                        //車輌To処理　Max値
                        if (!string.IsNullOrEmpty(s乗務員To))
                        {
                            int i車輌TO = AppCommon.IntParse(s乗務員To);
                            carlistz = carlistz.Where(c => c.乗務員ID <= i車輌TO);
                        }

                    }
					carlistz = carlistz.Where(c => c.退職年月日 == null || ((((DateTime)c.退職年月日).Year * 100 + ((DateTime)c.退職年月日).Month) >= c.年月)).Distinct();
                    var carlistzz = carlistz.ToList();


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



                    keilist = keilist.Distinct();
                    keilist = keilist.OrderBy(c => c.印刷グループID).ThenBy(c => c.経費ID);


                    //経費取得
                    var prelistz = (from car in carlistz
                                   from kei in keilist
                                   orderby car.乗務員ID, kei.印刷グループID, kei.経費ID
                                   select new JMI12010_PRELIST_Member
                                   {
                                       印刷グループID = kei.印刷グループID,
                                       経費ID = kei.経費ID,
                                       経費項目名 = kei.経費項目名,
                                       乗務員ID = car.乗務員ID,
                                       乗務員KEY = car.乗務員KEY,
                                   }).ToList();


                    var prelist = (from pre in prelistz
                                   join s13 in context.S13_DRVSB.Where(c => c.集計年月 == i年月) on new { a = pre.乗務員KEY, b = pre.経費ID } equals new { a = s13.乗務員KEY, b = s13.経費項目ID } into s13Group
                                   //from s13g in s13Group.DefaultIfEmpty()
                                   select new JMI12010_PRELIST_Member
                                   {
                                       印刷グループID = pre.印刷グループID,
                                       経費ID = pre.経費ID,
                                       経費項目名 = pre.経費項目名,
                                       乗務員ID = pre.乗務員ID,
                                       乗務員KEY = pre.乗務員KEY,
                                       金額 = s13Group.Select(c => c.金額).Sum(),

                                   }).AsQueryable();

                    prelist = prelist.Union(from pre in prelist.Where(c => c.印刷グループID != 2)
                                            group pre by new { pre.乗務員KEY, pre.乗務員ID, pre.印刷グループID } into preGroup
                                            select new JMI12010_PRELIST_Member
                                            {
                                                印刷グループID = preGroup.Key.印刷グループID,
                                                経費ID = 9999999,
                                                経費項目名 = "【 小 計 】",
                                                乗務員ID = preGroup.Key.乗務員ID,
                                                乗務員KEY = preGroup.Key.乗務員KEY,
                                                金額 = preGroup.Select(c => c.金額).Sum(),

                                            }).AsQueryable();

                    //合計追加
                    if (prelist.Any())
                    {
                        prelist = prelist.Union(from pre in prelist
                                                group pre by new { pre.印刷グループID, pre.経費ID, pre.経費項目名 } into preGroup
                                                select new JMI12010_PRELIST_Member
                                                {
                                                    印刷グループID = preGroup.Key.印刷グループID,
                                                    経費ID = preGroup.Key.経費ID,
                                                    経費項目名 = preGroup.Key.経費項目名,
                                                    乗務員ID = 999999,
                                                    乗務員KEY = 999999,
                                                    金額 = preGroup.Select(c => c.金額).Sum(),

                                                }).AsQueryable();
                    }

                    prelist = prelist.Distinct();
                    prelist = prelist.OrderBy(c => c.乗務員ID).ThenBy(c => c.印刷グループID);


                    var carlist = (from car in carlistzz
                                   join pre in prelist.Where(c => c.印刷グループID != 2) on car.乗務員KEY equals pre.乗務員KEY into preGroup
                                   select new JMI12010g_Member
                                   {
                                       乗務員KEY = car.乗務員KEY,
                                       乗務員ID = car.乗務員ID,
                                       車種ID = car.車種ID,
                                       車輌番号 = car.車輌番号,
                                       主乗務員 = car.主乗務員,
                                       車種 = car.車種,
                                       運送収入 = car.運送収入,

                                       一般管理費 = car.一般管理費,

                                       稼働日数 = car.稼働日数,
                                       実車KM = car.実車KM,
                                       空車KM = car.走行KM - car.実車KM,
                                       走行KM = car.走行KM,
                                       燃料L = car.燃料L,
                                       燃費 = car.燃費,
                                       収入1KM = car.収入1KM,
                                       限界利益 = car.運送収入 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額).Sum(),
									   //小計1 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額).Sum() == null ? 0 : preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額).Sum(),
									   //小計2 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額).Sum() == null ? 0 : preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額).Sum(),
									   //小計3 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額).Sum() == null ? 0 : preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額).Sum(),
									   小計1 = preGroup.Where(c => c.印刷グループID == 1 && c.経費項目名 == "【 小 計 】").Select(p => p.金額).Sum() == null ? 0 : preGroup.Where(c => c.印刷グループID == 1 && c.経費項目名 == "【 小 計 】").Select(p => p.金額).Sum(),
									   小計2 = preGroup.Where(c => c.印刷グループID == 3 && c.経費項目名 == "【 小 計 】").Select(p => p.金額).Sum() == null ? 0 : preGroup.Where(c => c.印刷グループID == 3 && c.経費項目名 == "【 小 計 】").Select(p => p.金額).Sum(),
									   小計3 = preGroup.Where(c => c.印刷グループID == 4 && c.経費項目名 == "【 小 計 】").Select(p => p.金額).Sum() == null ? 0 : preGroup.Where(c => c.印刷グループID == 4 && c.経費項目名 == "【 小 計 】").Select(p => p.金額).Sum(),

                                   }).AsQueryable();


                    var carlistzzz = carlist.ToList();
                    carlistzzz.Add(new JMI12010g_Member
                    {
                        乗務員KEY = 999999,
                        乗務員ID = 999999,
                        車種ID = 0,
                        車輌番号 = "【 合 計 】",
                        主乗務員 = " ",
                        車種 = " ",
                        運送収入 = carlist.Sum(c => c.運送収入),

                        一般管理費 = carlist.Sum(c => c.一般管理費),

                        稼働日数 = carlist.Sum(c => c.稼働日数),
                        実車KM = carlist.Sum(c => c.実車KM),
                        空車KM = carlist.Sum(c => c.走行KM) - carlist.Sum(c => c.実車KM),
                        走行KM = carlist.Sum(c => c.走行KM),
                        燃料L = carlist.Sum(c => c.燃料L),
                        燃費 = carlist.Sum(c => c.燃料L) == 0 ? 0 : (decimal)carlist.Sum(c => c.走行KM) / carlist.Sum(c => c.燃料L),
                        収入1KM = carlist.Sum(c => c.収入1KM),
                        限界利益 = carlist.Sum(c => c.限界利益),
                        小計1 = carlist.Sum(c => c.小計1),
                        小計2 = carlist.Sum(c => c.小計2),
                        小計3 = carlist.Sum(c => c.小計3),
                    });

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
                    int cnt = 0;
                    int[] carid = new int[6];
                    int carid1 = 0, carid2 = 0, carid3 = 0, carid4 = 0, carid5 = 0, carid6 = 0;
                    foreach (JMI12010g_Member a in carlistzzz)
                    {
                        switch (cnt)
                        {
                            case 0:
                                carid1 = a.乗務員KEY;
                                break;
                            case 1:
                                carid2 = a.乗務員KEY;
                                break;
                            case 2:
                                carid3 = a.乗務員KEY;
                                break;
                            case 3:
                                carid4 = a.乗務員KEY;
                                break;
                            case 4:
                                carid5 = a.乗務員KEY;
                                break;
                            case 5:
                                carid6 = a.乗務員KEY;
                                break;
                        };


                        cnt += 1;

                        if (cnt >= 6)
                        {
                            List<JMI12010_PRELIST_Member> plist = (from p in prelist
                                                                   where p.乗務員KEY == carid1
                                                                   select p).ToList();

                            JMI12010g_Member a1 = (from p in carlistzzz where p.乗務員KEY == carid1 select p).FirstOrDefault();
                            JMI12010g_Member a2 = (from p in carlistzzz where p.乗務員KEY == carid2 select p).FirstOrDefault();
                            JMI12010g_Member a3 = (from p in carlistzzz where p.乗務員KEY == carid3 select p).FirstOrDefault();
                            JMI12010g_Member a4 = (from p in carlistzzz where p.乗務員KEY == carid4 select p).FirstOrDefault();
                            JMI12010g_Member a5 = (from p in carlistzzz where p.乗務員KEY == carid5 select p).FirstOrDefault();
                            JMI12010g_Member a6 = (from p in carlistzzz where p.乗務員KEY == carid6 select p).FirstOrDefault();


                            decimal syaryoutyokusetuhi1 = 0;
                            decimal syaryoutyokusetuhi2 = 0;
                            decimal syaryoutyokusetuhi3 = 0;
                            decimal syaryoutyokusetuhi4 = 0;
                            decimal syaryoutyokusetuhi5 = 0;
                            decimal syaryoutyokusetuhi6 = 0;

                            syaryoutyokusetuhi1 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.乗務員KEY == carid1).Sum(c => c.金額);
                            syaryoutyokusetuhi2 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.乗務員KEY == carid2).Sum(c => c.金額);
                            syaryoutyokusetuhi3 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.乗務員KEY == carid3).Sum(c => c.金額);
                            syaryoutyokusetuhi4 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.乗務員KEY == carid4).Sum(c => c.金額);
                            syaryoutyokusetuhi5 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.乗務員KEY == carid5).Sum(c => c.金額);
                            syaryoutyokusetuhi6 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.乗務員KEY == carid6).Sum(c => c.金額);


                            foreach (var row in plist)
                            {

                                JMI12010_Member list = new JMI12010_Member()
                                {
                                    一般管理費1 = a1 == null ? null : a1.一般管理費,
                                    一般管理費2 = a2 == null ? null : a2.一般管理費,
                                    一般管理費3 = a3 == null ? null : a3.一般管理費,
                                    一般管理費4 = a4 == null ? null : a4.一般管理費,
                                    一般管理費5 = a5 == null ? null : a5.一般管理費,
                                    一般管理費6 = a6 == null ? null : a6.一般管理費,

                                    印刷グループID = row.印刷グループID,

                                    運送収入1 = a1 == null ? null : a1.運送収入,
                                    運送収入2 = a2 == null ? null : a2.運送収入,
                                    運送収入3 = a3 == null ? null : a3.運送収入,
                                    運送収入4 = a4 == null ? null : a4.運送収入,
                                    運送収入5 = a5 == null ? null : a5.運送収入,
                                    運送収入6 = a6 == null ? null : a6.運送収入,

                                    稼働日数1 = a1 == null ? null : a1.稼働日数,
                                    稼働日数2 = a2 == null ? null : a2.稼働日数,
                                    稼働日数3 = a3 == null ? null : a3.稼働日数,
                                    稼働日数4 = a4 == null ? null : a4.稼働日数,
                                    稼働日数5 = a5 == null ? null : a5.稼働日数,
                                    稼働日数6 = a6 == null ? null : a6.稼働日数,

                                    金額1 = row.経費ID == 9999999 ?
                                            row.印刷グループID == 2 ?
                                            a1 == null ? null : a1.限界利益 :
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid1).Select(c => c.金額).Sum() :
                                            carid1 == 999999 ?
                                           prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid1).Select(c => c.金額).Sum() :
                                            (from s13s in context.S13_DRVSB
                                             where s13s.集計年月 == i年月 && s13s.経費項目ID == row.経費ID && s13s.乗務員KEY == carid1
                                             select s13s.金額).FirstOrDefault(),
                                    金額2 = row.経費ID == 9999999 ?
                                            row.印刷グループID == 2 ?
                                            a2 == null ? null : a2.限界利益 :
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid2).Select(c => c.金額).Sum() :
                                            carid2 == 999999 ?
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid2).Select(c => c.金額).Sum() :
                                           (from s13s in context.S13_DRVSB
                                            where s13s.集計年月 == i年月 && s13s.経費項目ID == row.経費ID && s13s.乗務員KEY == carid2
                                            select s13s.金額).FirstOrDefault(),
                                    金額3 = row.経費ID == 9999999 ?
                                            row.印刷グループID == 2 ?
                                            a3 == null ? null : a3.限界利益 :
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid3).Select(c => c.金額).Sum() :
                                            carid3 == 999999 ?
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid3).Select(c => c.金額).Sum() :
                                           (from s13s in context.S13_DRVSB
                                            where s13s.集計年月 == i年月 && s13s.経費項目ID == row.経費ID && s13s.乗務員KEY == carid3
                                            select s13s.金額).FirstOrDefault(),
                                    金額4 = row.経費ID == 9999999 ?
                                            row.印刷グループID == 2 ?
                                            a4 == null ? null : a4.限界利益 :
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid4).Select(c => c.金額).Sum() :
                                            carid4 == 999999 ?
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid4).Select(c => c.金額).Sum() :
                                           (from s13s in context.S13_DRVSB
                                            where s13s.集計年月 == i年月 && s13s.経費項目ID == row.経費ID && s13s.乗務員KEY == carid4
                                            select s13s.金額).FirstOrDefault(),
                                    金額5 = row.経費ID == 9999999 ?
                                            row.印刷グループID == 2 ?
                                            a5 == null ? null : a5.限界利益 :
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid5).Select(c => c.金額).Sum() :
                                            carid5 == 999999 ?
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid5).Select(c => c.金額).Sum() :
                                           (from s13s in context.S13_DRVSB
                                            where s13s.集計年月 == i年月 && s13s.経費項目ID == row.経費ID && s13s.乗務員KEY == carid5
                                            select s13s.金額).FirstOrDefault(),
                                    金額6 = row.経費ID == 9999999 ?
                                            row.印刷グループID == 2 ?
                                            a6 == null ? null : a6.限界利益 :
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid6).Select(c => c.金額).Sum() :
                                            carid6 == 999999 ?
                                            prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid6).Select(c => c.金額).Sum() :
                                           (from s13s in context.S13_DRVSB
                                            where s13s.集計年月 == i年月 && s13s.経費項目ID == row.経費ID && s13s.乗務員KEY == carid6
                                            select s13s.金額).FirstOrDefault(),

                                    空車KM1 = a1 == null ? null : a1.空車KM,
                                    空車KM2 = a2 == null ? null : a2.空車KM,
                                    空車KM3 = a3 == null ? null : a3.空車KM,
                                    空車KM4 = a4 == null ? null : a4.空車KM,
                                    空車KM5 = a5 == null ? null : a5.空車KM,
                                    空車KM6 = a6 == null ? null : a6.空車KM,

                                    経費ID = row.経費ID,
                                    経費項目名 = row.経費項目名,

                                    原価1KM1 = a1 == null ? null : a1.走行KM == null ? null : a1.走行KM == 0 ? null : (a1.一般管理費 + a1.小計1 + a1.小計2 + a1.小計3) / a1.走行KM,
                                    原価1KM2 = a2 == null ? null : a2.走行KM == null ? null : a2.走行KM == 0 ? null : (a2.一般管理費 + a2.小計1 + a2.小計2 + a2.小計3) / a2.走行KM,
                                    原価1KM3 = a3 == null ? null : a3.走行KM == null ? null : a3.走行KM == 0 ? null : (a3.一般管理費 + a3.小計1 + a3.小計2 + a3.小計3) / a3.走行KM,
                                    原価1KM4 = a4 == null ? null : a4.走行KM == null ? null : a4.走行KM == 0 ? null : (a4.一般管理費 + a4.小計1 + a4.小計2 + a4.小計3) / a4.走行KM,
                                    原価1KM5 = a5 == null ? null : a5.走行KM == null ? null : a5.走行KM == 0 ? null : (a5.一般管理費 + a5.小計1 + a5.小計2 + a5.小計3) / a5.走行KM,
                                    原価1KM6 = a6 == null ? null : a6.走行KM == null ? null : a6.走行KM == 0 ? null : (a6.一般管理費 + a6.小計1 + a6.小計2 + a6.小計3) / a6.走行KM,

                                    実車KM1 = a1 == null ? null : a1.実車KM,
                                    実車KM2 = a2 == null ? null : a2.実車KM,
                                    実車KM3 = a3 == null ? null : a3.実車KM,
                                    実車KM4 = a4 == null ? null : a4.実車KM,
                                    実車KM5 = a5 == null ? null : a5.実車KM,
                                    実車KM6 = a6 == null ? null : a6.実車KM,

                                    車種1 = a1 == null ? null : a1.車種,
                                    車種2 = a2 == null ? null : a2.車種,
                                    車種3 = a3 == null ? null : a3.車種,
                                    車種4 = a4 == null ? null : a4.車種,
                                    車種5 = a5 == null ? null : a5.車種,
                                    車種6 = a6 == null ? null : a6.車種,

                                    乗務員ID1 = a1 == null ? null : (int?)a1.乗務員ID,
                                    乗務員ID2 = a2 == null ? null : (int?)a2.乗務員ID,
                                    乗務員ID3 = a3 == null ? null : (int?)a3.乗務員ID,
                                    乗務員ID4 = a4 == null ? null : (int?)a4.乗務員ID,
                                    乗務員ID5 = a5 == null ? null : (int?)a5.乗務員ID,
                                    乗務員ID6 = a6 == null ? null : (int?)a6.乗務員ID,

                                    車輌直接益1 = a1 == null ? null : a1.運送収入 - syaryoutyokusetuhi1,
                                    車輌直接益2 = a2 == null ? null : a2.運送収入 - syaryoutyokusetuhi2,
                                    車輌直接益3 = a3 == null ? null : a3.運送収入 - syaryoutyokusetuhi3,
                                    車輌直接益4 = a4 == null ? null : a4.運送収入 - syaryoutyokusetuhi4,
                                    車輌直接益5 = a5 == null ? null : a5.運送収入 - syaryoutyokusetuhi5,
                                    車輌直接益6 = a6 == null ? null : a6.運送収入 - syaryoutyokusetuhi6,

                                    車輌直接費1 = syaryoutyokusetuhi1,
                                    車輌直接費2 = syaryoutyokusetuhi2,
                                    車輌直接費3 = syaryoutyokusetuhi3,
                                    車輌直接費4 = syaryoutyokusetuhi4,
                                    車輌直接費5 = syaryoutyokusetuhi5,
                                    車輌直接費6 = syaryoutyokusetuhi6,

                                    車輌番号1 = a1 == null ? null : a1.車輌番号,
                                    車輌番号2 = a2 == null ? null : a2.車輌番号,
                                    車輌番号3 = a3 == null ? null : a3.車輌番号,
                                    車輌番号4 = a4 == null ? null : a4.車輌番号,
                                    車輌番号5 = a5 == null ? null : a5.車輌番号,
                                    車輌番号6 = a6 == null ? null : a6.車輌番号,

                                    主乗務員1 = a1 == null ? null : a1.主乗務員,
                                    主乗務員2 = a2 == null ? null : a2.主乗務員,
                                    主乗務員3 = a3 == null ? null : a3.主乗務員,
                                    主乗務員4 = a4 == null ? null : a4.主乗務員,
                                    主乗務員5 = a5 == null ? null : a5.主乗務員,
                                    主乗務員6 = a6 == null ? null : a6.主乗務員,

                                    収入1KM1 = a1 == null ? null : (decimal?)a1.収入1KM,
                                    収入1KM2 = a2 == null ? null : (decimal?)a2.収入1KM,
                                    収入1KM3 = a3 == null ? null : (decimal?)a3.収入1KM,
                                    収入1KM4 = a4 == null ? null : (decimal?)a4.収入1KM,
                                    収入1KM5 = a5 == null ? null : (decimal?)a5.収入1KM,
                                    収入1KM6 = a6 == null ? null : (decimal?)a6.収入1KM,

                                    走行KM1 = a1 == null ? null : a1.走行KM,
                                    走行KM2 = a2 == null ? null : a2.走行KM,
                                    走行KM3 = a3 == null ? null : a3.走行KM,
                                    走行KM4 = a4 == null ? null : a4.走行KM,
                                    走行KM5 = a5 == null ? null : a5.走行KM,
                                    走行KM6 = a6 == null ? null : a6.走行KM,

                                    当月利益1 = a1 == null ? 0 - syaryoutyokusetuhi1 : a1.運送収入 == null ? 0 - syaryoutyokusetuhi1 - a1.一般管理費 : a1.一般管理費 == null ? a1.運送収入 - syaryoutyokusetuhi1 : a1.運送収入 - syaryoutyokusetuhi1 - a1.一般管理費,
                                    当月利益2 = a2 == null ? 0 - syaryoutyokusetuhi2 : a2.運送収入 == null ? 0 - syaryoutyokusetuhi2 - a2.一般管理費 : a2.一般管理費 == null ? a2.運送収入 - syaryoutyokusetuhi2 : a2.運送収入 - syaryoutyokusetuhi2 - a2.一般管理費,
                                    当月利益3 = a3 == null ? 0 - syaryoutyokusetuhi3 : a3.運送収入 == null ? 0 - syaryoutyokusetuhi3 - a3.一般管理費 : a3.一般管理費 == null ? a3.運送収入 - syaryoutyokusetuhi3 : a3.運送収入 - syaryoutyokusetuhi3 - a3.一般管理費,
                                    当月利益4 = a4 == null ? 0 - syaryoutyokusetuhi4 : a4.運送収入 == null ? 0 - syaryoutyokusetuhi4 - a4.一般管理費 : a4.一般管理費 == null ? a4.運送収入 - syaryoutyokusetuhi4 : a4.運送収入 - syaryoutyokusetuhi4 - a4.一般管理費,
                                    当月利益5 = a5 == null ? 0 - syaryoutyokusetuhi5 : a5.運送収入 == null ? 0 - syaryoutyokusetuhi5 - a5.一般管理費 : a5.一般管理費 == null ? a5.運送収入 - syaryoutyokusetuhi5 : a5.運送収入 - syaryoutyokusetuhi5 - a5.一般管理費,
                                    当月利益6 = a6 == null ? 0 - syaryoutyokusetuhi6 : a6.運送収入 == null ? 0 - syaryoutyokusetuhi6 - a6.一般管理費 : a6.一般管理費 == null ? a6.運送収入 - syaryoutyokusetuhi6 : a6.運送収入 - syaryoutyokusetuhi6 - a6.一般管理費,

                                    燃費1 = a1 == null ? null : (decimal?)a1.燃費,
                                    燃費2 = a2 == null ? null : (decimal?)a2.燃費,
                                    燃費3 = a3 == null ? null : (decimal?)a3.燃費,
                                    燃費4 = a4 == null ? null : (decimal?)a4.燃費,
                                    燃費5 = a5 == null ? null : (decimal?)a5.燃費,
                                    燃費6 = a6 == null ? null : (decimal?)a6.燃費,

                                    燃料L1 = a1 == null ? null : (decimal?)a1.燃料L,
                                    燃料L2 = a2 == null ? null : (decimal?)a2.燃料L,
                                    燃料L3 = a3 == null ? null : (decimal?)a3.燃料L,
                                    燃料L4 = a4 == null ? null : (decimal?)a4.燃料L,
                                    燃料L5 = a5 == null ? null : (decimal?)a5.燃料L,
                                    燃料L6 = a6 == null ? null : (decimal?)a6.燃料L,


                                    年 = 年.ToString(),
                                    コードFrom = s乗務員From,
                                    コードTo = s乗務員To,
                                    乗務員ﾋﾟｯｸｱｯﾌﾟ = s乗務員List,
                                    月 = 月.ToString(),

                                };
                                retList.Add(list);
                            };

                            cnt = 0;
                            carid1 = 0; carid2 = 0; carid3 = 0; carid4 = 0; carid5 = 0; carid6 = 0;
                        };

                    };

                    //余り分があれば
                    if (cnt > 0)
                    {
                        List<JMI12010_PRELIST_Member> plist = (from p in prelist
                                                               where p.乗務員KEY == carid1
                                                               select p).ToList();

                        JMI12010g_Member a1 = (from p in carlistzzz where p.乗務員KEY == carid1 select p).FirstOrDefault();
                        JMI12010g_Member a2 = (from p in carlistzzz where p.乗務員KEY == carid2 select p).FirstOrDefault();
                        JMI12010g_Member a3 = (from p in carlistzzz where p.乗務員KEY == carid3 select p).FirstOrDefault();
                        JMI12010g_Member a4 = (from p in carlistzzz where p.乗務員KEY == carid4 select p).FirstOrDefault();
                        JMI12010g_Member a5 = (from p in carlistzzz where p.乗務員KEY == carid5 select p).FirstOrDefault();
                        JMI12010g_Member a6 = (from p in carlistzzz where p.乗務員KEY == carid6 select p).FirstOrDefault();

                        decimal syaryoutyokusetuhi1 = 0;
                        decimal syaryoutyokusetuhi2 = 0;
                        decimal syaryoutyokusetuhi3 = 0;
                        decimal syaryoutyokusetuhi4 = 0;
                        decimal syaryoutyokusetuhi5 = 0;
                        decimal syaryoutyokusetuhi6 = 0;

                        syaryoutyokusetuhi1 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.乗務員KEY == carid1).Sum(c => c.金額);
                        syaryoutyokusetuhi2 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.乗務員KEY == carid2).Sum(c => c.金額);
                        syaryoutyokusetuhi3 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.乗務員KEY == carid3).Sum(c => c.金額);
                        syaryoutyokusetuhi4 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.乗務員KEY == carid4).Sum(c => c.金額);
                        syaryoutyokusetuhi5 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.乗務員KEY == carid5).Sum(c => c.金額);
                        syaryoutyokusetuhi6 = prelist.Where(c => prt_GROPID.Contains(c.経費ID) && c.経費ID != 9999999 && c.乗務員KEY == carid6).Sum(c => c.金額);

                        foreach (var row in plist)
                        {

                            JMI12010_Member list = new JMI12010_Member()
                            {
                                一般管理費1 = a1 == null ? null : a1.一般管理費,
                                一般管理費2 = a2 == null ? null : a2.一般管理費,
                                一般管理費3 = a3 == null ? null : a3.一般管理費,
                                一般管理費4 = a4 == null ? null : a4.一般管理費,
                                一般管理費5 = a5 == null ? null : a5.一般管理費,
                                一般管理費6 = a6 == null ? null : a6.一般管理費,

                                印刷グループID = row.印刷グループID,

                                運送収入1 = a1 == null ? null : a1.運送収入,
                                運送収入2 = a2 == null ? null : a2.運送収入,
                                運送収入3 = a3 == null ? null : a3.運送収入,
                                運送収入4 = a4 == null ? null : a4.運送収入,
                                運送収入5 = a5 == null ? null : a5.運送収入,
                                運送収入6 = a6 == null ? null : a6.運送収入,

                                稼働日数1 = a1 == null ? null : a1.稼働日数,
                                稼働日数2 = a2 == null ? null : a2.稼働日数,
                                稼働日数3 = a3 == null ? null : a3.稼働日数,
                                稼働日数4 = a4 == null ? null : a4.稼働日数,
                                稼働日数5 = a5 == null ? null : a5.稼働日数,
                                稼働日数6 = a6 == null ? null : a6.稼働日数,

                                金額1 = row.経費ID == 9999999 ?
                                        row.印刷グループID == 2 ?
                                        a1 == null ? null : a1.限界利益 :
                                        prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid1).Select(c => c.金額).Sum() :
                                        carid1 == 999999 ?
                                        prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid1).Select(c => c.金額).Sum() :
                                        (from s13s in context.S13_DRVSB
                                         where s13s.集計年月 == i年月 && s13s.経費項目ID == row.経費ID && s13s.乗務員KEY == carid1
                                         select s13s.金額).FirstOrDefault(),
                                金額2 = row.経費ID == 9999999 ?
                                        row.印刷グループID == 2 ?
                                        a2 == null ? null : a2.限界利益 :
                                        prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid2).Select(c => c.金額).Sum() :
                                        carid2 == 999999 ?
                                        prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid2).Select(c => c.金額).Sum() :
                                       (from s13s in context.S13_DRVSB
                                        where s13s.集計年月 == i年月 && s13s.経費項目ID == row.経費ID && s13s.乗務員KEY == carid2
                                        select s13s.金額).FirstOrDefault(),
                                金額3 = row.経費ID == 9999999 ?
                                        row.印刷グループID == 2 ?
                                        a3 == null ? null : a3.限界利益 :
                                        prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid3).Select(c => c.金額).Sum() :
                                        carid3 == 999999 ?
                                        prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid3).Select(c => c.金額).Sum() :
                                       (from s13s in context.S13_DRVSB
                                        where s13s.集計年月 == i年月 && s13s.経費項目ID == row.経費ID && s13s.乗務員KEY == carid3
                                        select s13s.金額).FirstOrDefault(),
                                金額4 = row.経費ID == 9999999 ?
                                        row.印刷グループID == 2 ?
                                        a4 == null ? null : a4.限界利益 :
                                        prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid4).Select(c => c.金額).Sum() :
                                        carid4 == 999999 ?
                                        prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid4).Select(c => c.金額).Sum() :
                                       (from s13s in context.S13_DRVSB
                                        where s13s.集計年月 == i年月 && s13s.経費項目ID == row.経費ID && s13s.乗務員KEY == carid4
                                        select s13s.金額).FirstOrDefault(),
                                金額5 = row.経費ID == 9999999 ?
                                        row.印刷グループID == 2 ?
                                        a5 == null ? null : a5.限界利益 :
                                        prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid5).Select(c => c.金額).Sum() :
                                        carid5 == 999999 ?
                                        prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid5).Select(c => c.金額).Sum() :
                                       (from s13s in context.S13_DRVSB
                                        where s13s.集計年月 == i年月 && s13s.経費項目ID == row.経費ID && s13s.乗務員KEY == carid5
                                        select s13s.金額).FirstOrDefault(),
                                金額6 = row.経費ID == 9999999 ?
                                        row.印刷グループID == 2 ?
                                        a6 == null ? null : a6.限界利益 :
                                        prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid6).Select(c => c.金額).Sum() :
                                        carid6 == 999999 ?
                                        prelist.Where(c => c.印刷グループID == row.印刷グループID && c.経費ID == row.経費ID && c.乗務員KEY == carid6).Select(c => c.金額).Sum() :
                                       (from s13s in context.S13_DRVSB
                                        where s13s.集計年月 == i年月 && s13s.経費項目ID == row.経費ID && s13s.乗務員KEY == carid6
                                        select s13s.金額).FirstOrDefault(),


                                空車KM1 = a1 == null ? null : a1.空車KM,
                                空車KM2 = a2 == null ? null : a2.空車KM,
                                空車KM3 = a3 == null ? null : a3.空車KM,
                                空車KM4 = a4 == null ? null : a4.空車KM,
                                空車KM5 = a5 == null ? null : a5.空車KM,
                                空車KM6 = a6 == null ? null : a6.空車KM,

                                経費ID = row.経費ID,
                                経費項目名 = row.経費項目名,

                                原価1KM1 = a1 == null ? null : a1.走行KM == null ? null : a1.走行KM == 0 ? null : (a1.一般管理費 + a1.小計1 + a1.小計2 + a1.小計3) / a1.走行KM,
                                原価1KM2 = a2 == null ? null : a2.走行KM == null ? null : a2.走行KM == 0 ? null : (a2.一般管理費 + a2.小計1 + a2.小計2 + a2.小計3) / a2.走行KM,
                                原価1KM3 = a3 == null ? null : a3.走行KM == null ? null : a3.走行KM == 0 ? null : (a3.一般管理費 + a3.小計1 + a3.小計2 + a3.小計3) / a3.走行KM,
                                原価1KM4 = a4 == null ? null : a4.走行KM == null ? null : a4.走行KM == 0 ? null : (a4.一般管理費 + a4.小計1 + a4.小計2 + a4.小計3) / a4.走行KM,
                                原価1KM5 = a5 == null ? null : a5.走行KM == null ? null : a5.走行KM == 0 ? null : (a5.一般管理費 + a5.小計1 + a5.小計2 + a5.小計3) / a5.走行KM,
                                原価1KM6 = a6 == null ? null : a6.走行KM == null ? null : a6.走行KM == 0 ? null : (a6.一般管理費 + a6.小計1 + a6.小計2 + a6.小計3) / a6.走行KM,

                                実車KM1 = a1 == null ? null : a1.実車KM,
                                実車KM2 = a2 == null ? null : a2.実車KM,
                                実車KM3 = a3 == null ? null : a3.実車KM,
                                実車KM4 = a4 == null ? null : a4.実車KM,
                                実車KM5 = a5 == null ? null : a5.実車KM,
                                実車KM6 = a6 == null ? null : a6.実車KM,

                                車種1 = a1 == null ? null : a1.車種,
                                車種2 = a2 == null ? null : a2.車種,
                                車種3 = a3 == null ? null : a3.車種,
                                車種4 = a4 == null ? null : a4.車種,
                                車種5 = a5 == null ? null : a5.車種,
                                車種6 = a6 == null ? null : a6.車種,

                                乗務員ID1 = a1 == null ? null : (int?)a1.乗務員ID,
                                乗務員ID2 = a2 == null ? null : (int?)a2.乗務員ID,
                                乗務員ID3 = a3 == null ? null : (int?)a3.乗務員ID,
                                乗務員ID4 = a4 == null ? null : (int?)a4.乗務員ID,
                                乗務員ID5 = a5 == null ? null : (int?)a5.乗務員ID,
                                乗務員ID6 = a6 == null ? null : (int?)a6.乗務員ID,

                                車輌直接益1 = a1 == null ? null : a1.運送収入 - syaryoutyokusetuhi1,
                                車輌直接益2 = a2 == null ? null : a2.運送収入 - syaryoutyokusetuhi2,
                                車輌直接益3 = a3 == null ? null : a3.運送収入 - syaryoutyokusetuhi3,
                                車輌直接益4 = a4 == null ? null : a4.運送収入 - syaryoutyokusetuhi4,
                                車輌直接益5 = a5 == null ? null : a5.運送収入 - syaryoutyokusetuhi5,
                                車輌直接益6 = a6 == null ? null : a6.運送収入 - syaryoutyokusetuhi6,

                                車輌直接費1 = syaryoutyokusetuhi1,
                                車輌直接費2 = syaryoutyokusetuhi2,
                                車輌直接費3 = syaryoutyokusetuhi3,
                                車輌直接費4 = syaryoutyokusetuhi4,
                                車輌直接費5 = syaryoutyokusetuhi5,
                                車輌直接費6 = syaryoutyokusetuhi6,

                                車輌番号1 = a1 == null ? null : a1.車輌番号,
                                車輌番号2 = a2 == null ? null : a2.車輌番号,
                                車輌番号3 = a3 == null ? null : a3.車輌番号,
                                車輌番号4 = a4 == null ? null : a4.車輌番号,
                                車輌番号5 = a5 == null ? null : a5.車輌番号,
                                車輌番号6 = a6 == null ? null : a6.車輌番号,

                                主乗務員1 = a1 == null ? null : a1.主乗務員,
                                主乗務員2 = a2 == null ? null : a2.主乗務員,
                                主乗務員3 = a3 == null ? null : a3.主乗務員,
                                主乗務員4 = a4 == null ? null : a4.主乗務員,
                                主乗務員5 = a5 == null ? null : a5.主乗務員,
                                主乗務員6 = a6 == null ? null : a6.主乗務員,

                                収入1KM1 = a1 == null ? null : (decimal?)a1.収入1KM,
                                収入1KM2 = a2 == null ? null : (decimal?)a2.収入1KM,
                                収入1KM3 = a3 == null ? null : (decimal?)a3.収入1KM,
                                収入1KM4 = a4 == null ? null : (decimal?)a4.収入1KM,
                                収入1KM5 = a5 == null ? null : (decimal?)a5.収入1KM,
                                収入1KM6 = a6 == null ? null : (decimal?)a6.収入1KM,

                                走行KM1 = a1 == null ? null : a1.走行KM,
                                走行KM2 = a2 == null ? null : a2.走行KM,
                                走行KM3 = a3 == null ? null : a3.走行KM,
                                走行KM4 = a4 == null ? null : a4.走行KM,
                                走行KM5 = a5 == null ? null : a5.走行KM,
                                走行KM6 = a6 == null ? null : a6.走行KM,

                                当月利益1 = a1 == null ? 0 - syaryoutyokusetuhi1 : a1.運送収入 == null ? 0 - syaryoutyokusetuhi1 - a1.一般管理費 : a1.一般管理費 == null ? a1.運送収入 - syaryoutyokusetuhi1 : a1.運送収入 - syaryoutyokusetuhi1 - a1.一般管理費,
                                当月利益2 = a2 == null ? 0 - syaryoutyokusetuhi2 : a2.運送収入 == null ? 0 - syaryoutyokusetuhi2 - a2.一般管理費 : a2.一般管理費 == null ? a2.運送収入 - syaryoutyokusetuhi2 : a2.運送収入 - syaryoutyokusetuhi2 - a2.一般管理費,
                                当月利益3 = a3 == null ? 0 - syaryoutyokusetuhi3 : a3.運送収入 == null ? 0 - syaryoutyokusetuhi3 - a3.一般管理費 : a3.一般管理費 == null ? a3.運送収入 - syaryoutyokusetuhi3 : a3.運送収入 - syaryoutyokusetuhi3 - a3.一般管理費,
                                当月利益4 = a4 == null ? 0 - syaryoutyokusetuhi4 : a4.運送収入 == null ? 0 - syaryoutyokusetuhi4 - a4.一般管理費 : a4.一般管理費 == null ? a4.運送収入 - syaryoutyokusetuhi4 : a4.運送収入 - syaryoutyokusetuhi4 - a4.一般管理費,
                                当月利益5 = a5 == null ? 0 - syaryoutyokusetuhi5 : a5.運送収入 == null ? 0 - syaryoutyokusetuhi5 - a5.一般管理費 : a5.一般管理費 == null ? a5.運送収入 - syaryoutyokusetuhi5 : a5.運送収入 - syaryoutyokusetuhi5 - a5.一般管理費,
                                当月利益6 = a6 == null ? 0 - syaryoutyokusetuhi6 : a6.運送収入 == null ? 0 - syaryoutyokusetuhi6 - a6.一般管理費 : a6.一般管理費 == null ? a6.運送収入 - syaryoutyokusetuhi6 : a6.運送収入 - syaryoutyokusetuhi6 - a6.一般管理費,

                                燃費1 = a1 == null ? null : (decimal?)a1.燃費,
                                燃費2 = a2 == null ? null : (decimal?)a2.燃費,
                                燃費3 = a3 == null ? null : (decimal?)a3.燃費,
                                燃費4 = a4 == null ? null : (decimal?)a4.燃費,
                                燃費5 = a5 == null ? null : (decimal?)a5.燃費,
                                燃費6 = a6 == null ? null : (decimal?)a6.燃費,

                                燃料L1 = a1 == null ? null : (decimal?)a1.燃料L,
                                燃料L2 = a2 == null ? null : (decimal?)a2.燃料L,
                                燃料L3 = a3 == null ? null : (decimal?)a3.燃料L,
                                燃料L4 = a4 == null ? null : (decimal?)a4.燃料L,
                                燃料L5 = a5 == null ? null : (decimal?)a5.燃料L,
                                燃料L6 = a6 == null ? null : (decimal?)a6.燃料L,


                                年 = 年.ToString(),
                                コードFrom = s乗務員From,
                                コードTo = s乗務員To,
                                乗務員ﾋﾟｯｸｱｯﾌﾟ = s乗務員List,
                                月 = 月.ToString(),

                            };

                            retList.Add(list);
                        };

                        cnt = 0;
                        carid1 = 0; carid2 = 0; carid3 = 0; carid4 = 0; carid5 = 0; carid6 = 0;
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

        #region JMI12010 csv

		public DataTable JMI12010_GetDataList_CSV(string s乗務員From, string s乗務員To, int?[] i乗務員List, string s乗務員List, int i年月, int 年, int 月)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{

				List<JMI12010_Member> retList = new List<JMI12010_Member>();

				context.Connection.Open();
				try
				{
					string 車輌ﾋﾟｯｸｱｯﾌﾟ指定 = string.Empty;

					var carlistz = (from m04 in context.M04_DRV.Where(c => c.削除日付 == null)
									join s13 in context.S13_DRV.Where(c => c.集計年月 == i年月) on m04.乗務員KEY equals s13.乗務員KEY
									join m05 in context.M05_CAR.Where(c => c.削除日付 == null).Take(1) on m04.乗務員KEY equals m05.乗務員KEY into m05Group
									from m05g in m05Group.DefaultIfEmpty()
									join m06 in context.M06_SYA.Where(c => c.削除日付 == null) on m05g.車種ID equals m06.車種ID into m06Group
									from m06g in m06Group.DefaultIfEmpty()
									orderby m04.乗務員ID
									select new JMI12010g_Member
									{
										乗務員ID = m04.乗務員ID,
										乗務員KEY = m04.乗務員KEY,
										車種ID = m05g.車種ID,
										車輌番号 = m05g.車輌番号,
										主乗務員 = m04.乗務員名,
										車種 = m06g.車種名,
										運送収入 = s13.運送収入,

										一般管理費 = s13.一般管理費,

										稼働日数 = s13.稼動日数,
										実車KM = s13.実車ＫＭ,
										空車KM = s13.走行ＫＭ - s13.実車ＫＭ,
										走行KM = s13.走行ＫＭ,
										燃料L = s13.燃料Ｌ == null ? 0 : s13.燃料Ｌ,
										燃費 = s13.燃料Ｌ == 0 ? 0 : s13.燃料Ｌ == null ? 0 : s13.走行ＫＭ / s13.燃料Ｌ,
										収入1KM = s13.走行ＫＭ == 0 ? 0 : s13.走行ＫＭ == null ? 0 : s13.運送収入 / s13.走行ＫＭ,
									}).AsQueryable();

					if (!(string.IsNullOrEmpty(s乗務員From + s乗務員To) && i乗務員List.Length == 0))
					{
						if (string.IsNullOrEmpty(s乗務員From + s乗務員To))
						{
							carlistz = carlistz.Where(c => c.乗務員ID >= int.MaxValue);
						}

						//車輌From処理　Min値
						if (!string.IsNullOrEmpty(s乗務員From))
						{
							int i乗務員From = AppCommon.IntParse(s乗務員From);
							carlistz = carlistz.Where(c => c.乗務員ID >= i乗務員From);
						}

						//車輌To処理　Max値
						if (!string.IsNullOrEmpty(s乗務員To))
						{
							int i車輌TO = AppCommon.IntParse(s乗務員To);
							carlistz = carlistz.Where(c => c.乗務員ID <= i車輌TO);
						}

						if (i乗務員List.Length > 0)
						{
							var intCause = i乗務員List;
							carlistz = carlistz.Union(from m04 in context.M04_DRV.Where(c => c.削除日付 == null)
													  join s13 in context.S13_DRV.Where(c => c.集計年月 == i年月) on m04.乗務員KEY equals s13.乗務員KEY
													  join m05 in context.M05_CAR.Where(c => c.削除日付 == null).Take(1) on m04.乗務員KEY equals m05.乗務員KEY into m05Group
													  from m05g in m05Group.DefaultIfEmpty()
													  join m06 in context.M06_SYA.Where(c => c.削除日付 == null) on m05g.車種ID equals m06.車種ID into m06Group
													  from m06g in m06Group.DefaultIfEmpty()
													  orderby m04.乗務員ID
													  where intCause.Contains(m04.乗務員ID)
													  select new JMI12010g_Member
													  {
														  乗務員ID = m04.乗務員ID,
														  乗務員KEY = m04.乗務員KEY,
														  車種ID = m05g.車種ID,
														  車輌番号 = m05g.車輌番号,
														  主乗務員 = m04.乗務員名,
														  車種 = m06g.車種名,
														  運送収入 = s13.運送収入,

														  一般管理費 = s13.一般管理費,

														  稼働日数 = s13.稼動日数,
														  実車KM = s13.実車ＫＭ,
														  空車KM = s13.走行ＫＭ - s13.実車ＫＭ,
														  走行KM = s13.走行ＫＭ,
														  燃料L = s13.燃料Ｌ == null ? 0 : s13.燃料Ｌ,
														  燃費 = s13.燃料Ｌ == 0 ? 0 : s13.燃料Ｌ == null ? 0 : s13.走行ＫＭ / s13.燃料Ｌ,
														  収入1KM = s13.走行ＫＭ == 0 ? 0 : s13.走行ＫＭ == null ? 0 : s13.運送収入 / s13.走行ＫＭ,
													  });
						};
					}
					else
					{

						//車輌From処理　Min値
						if (!string.IsNullOrEmpty(s乗務員From))
						{
							int i乗務員From = AppCommon.IntParse(s乗務員From);
							carlistz = carlistz.Where(c => c.乗務員ID >= i乗務員From);
						}

						//車輌To処理　Max値
						if (!string.IsNullOrEmpty(s乗務員To))
						{
							int i車輌TO = AppCommon.IntParse(s乗務員To);
							carlistz = carlistz.Where(c => c.乗務員ID <= i車輌TO);
						}

					}
					carlistz = carlistz.Where(c => c.退職年月日 == null || ((((DateTime)c.退職年月日).Year * 100 + ((DateTime)c.退職年月日).Month) >= c.年月)).Distinct();
					var carlistzz = carlistz.ToList();


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



					keilist = keilist.Distinct();
					keilist = keilist.OrderBy(c => c.印刷グループID).ThenBy(c => c.経費ID);


					//経費取得
					var prelistz = (from car in carlistz
									from kei in keilist
									orderby car.乗務員ID, kei.印刷グループID, kei.経費ID
									select new JMI12010_PRELIST_Member
									{
										印刷グループID = kei.印刷グループID,
										経費ID = kei.経費ID,
										経費項目名 = kei.経費項目名,
										乗務員ID = car.乗務員ID,
										乗務員KEY = car.乗務員KEY,
									}).ToList();


					var prelist = (from pre in prelistz
								   join s13 in context.S13_DRVSB.Where(c => c.集計年月 == i年月) on new { a = pre.乗務員KEY, b = pre.経費ID } equals new { a = s13.乗務員KEY, b = s13.経費項目ID } into s13Group
								   //from s13g in s13Group.DefaultIfEmpty()
								   select new JMI12010_PRELIST_Member
								   {
									   印刷グループID = pre.印刷グループID,
									   経費ID = pre.経費ID,
									   経費項目名 = pre.経費項目名,
									   乗務員ID = pre.乗務員ID,
									   乗務員KEY = pre.乗務員KEY,
									   金額 = s13Group.Select(c => c.金額).Sum(),

								   }).AsQueryable();

					prelist = prelist.Union(from pre in prelist.Where(c => c.印刷グループID != 2)
											group pre by new { pre.乗務員KEY, pre.乗務員ID, pre.印刷グループID } into preGroup
											select new JMI12010_PRELIST_Member
											{
												印刷グループID = preGroup.Key.印刷グループID,
												経費ID = 9999999,
												経費項目名 = "【 小 計 】",
												乗務員ID = preGroup.Key.乗務員ID,
												乗務員KEY = preGroup.Key.乗務員KEY,
												金額 = preGroup.Select(c => c.金額).Sum(),

											}).AsQueryable();

					//合計追加
					if (prelist.Any())
					{
						prelist = prelist.Union(from pre in prelist
												group pre by new { pre.印刷グループID, pre.経費ID, pre.経費項目名 } into preGroup
												select new JMI12010_PRELIST_Member
												{
													印刷グループID = preGroup.Key.印刷グループID,
													経費ID = preGroup.Key.経費ID,
													経費項目名 = preGroup.Key.経費項目名,
													乗務員ID = 999999,
													乗務員KEY = 999999,
													金額 = preGroup.Select(c => c.金額).Sum(),

												}).AsQueryable();
					}

					prelist = prelist.Distinct();
					prelist = prelist.OrderBy(c => c.乗務員ID).ThenBy(c => c.印刷グループID);


					var carlist = (from car in carlistzz
								   join pre in prelist.Where(c => c.印刷グループID != 2) on car.乗務員KEY equals pre.乗務員KEY into preGroup
								   select new JMI12010g_Member
								   {
									   乗務員KEY = car.乗務員KEY,
									   乗務員ID = car.乗務員ID,
									   車種ID = car.車種ID,
									   車輌番号 = car.車輌番号,
									   主乗務員 = car.主乗務員,
									   車種 = car.車種,
									   運送収入 = car.運送収入,

									   一般管理費 = car.一般管理費,

									   稼働日数 = car.稼働日数,
									   実車KM = car.実車KM,
									   空車KM = car.走行KM - car.実車KM,
									   走行KM = car.走行KM,
									   燃料L = car.燃料L,
									   燃費 = car.燃費,
									   収入1KM = car.収入1KM,
									   限界利益 = car.運送収入 - preGroup.Where(c => c.印刷グループID == 1 && c.経費ID != 9999999).Select(p => p.金額).Sum(),
									   //小計1 = preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額).Sum() == null ? 0 : preGroup.Where(c => c.印刷グループID == 1).Select(p => p.金額).Sum(),
									   //小計2 = preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額).Sum() == null ? 0 : preGroup.Where(c => c.印刷グループID == 3).Select(p => p.金額).Sum(),
									   //小計3 = preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額).Sum() == null ? 0 : preGroup.Where(c => c.印刷グループID == 4).Select(p => p.金額).Sum(),
									   小計1 = preGroup.Where(c => c.印刷グループID == 1 && c.経費項目名 == "【 小 計 】").Select(p => p.金額).Sum() == null ? 0 : preGroup.Where(c => c.印刷グループID == 1 && c.経費項目名 == "【 小 計 】").Select(p => p.金額).Sum(),
									   小計2 = preGroup.Where(c => c.印刷グループID == 3 && c.経費項目名 == "【 小 計 】").Select(p => p.金額).Sum() == null ? 0 : preGroup.Where(c => c.印刷グループID == 3 && c.経費項目名 == "【 小 計 】").Select(p => p.金額).Sum(),
									   小計3 = preGroup.Where(c => c.印刷グループID == 4 && c.経費項目名 == "【 小 計 】").Select(p => p.金額).Sum() == null ? 0 : preGroup.Where(c => c.印刷グループID == 4 && c.経費項目名 == "【 小 計 】").Select(p => p.金額).Sum(),



								   }).AsQueryable();


					var carlistzzz = carlist.ToList();
					carlistzzz.Add(new JMI12010g_Member
					{
						乗務員KEY = 999999,
						乗務員ID = 999999,
						車種ID = 0,
						車輌番号 = "【 合 計 】",
						主乗務員 = " ",
						車種 = " ",
						運送収入 = carlist.Sum(c => c.運送収入),

						一般管理費 = carlist.Sum(c => c.一般管理費),

						稼働日数 = carlist.Sum(c => c.稼働日数),
						実車KM = carlist.Sum(c => c.実車KM),
						空車KM = carlist.Sum(c => c.走行KM) - carlist.Sum(c => c.実車KM),
						走行KM = carlist.Sum(c => c.走行KM),
						燃料L = carlist.Sum(c => c.燃料L),
						燃費 = carlist.Sum(c => c.燃料L) == 0 ? 0 : (decimal)carlist.Sum(c => c.走行KM) / carlist.Sum(c => c.燃料L),
						収入1KM = carlist.Sum(c => c.収入1KM),
						限界利益 = carlist.Sum(c => c.限界利益),
						小計1 = carlist.Sum(c => c.小計1),
						小計2 = carlist.Sum(c => c.小計2),
						小計3 = carlist.Sum(c => c.小計3),
					});

					var listprelist = prelist.ToList();

					var prt_GROPID1 = listprelist.Where(c => c.印刷グループID == 1).Select(c => c.経費ID);
					var prt_GROPID3 = listprelist.Where(c => c.印刷グループID == 3).Select(c => c.経費ID);
					var prt_GROPID4 = listprelist.Where(c => c.印刷グループID == 4).Select(c => c.経費ID);
					prt_GROPID1 = prt_GROPID1.Distinct();
					prt_GROPID3 = prt_GROPID3.Distinct();
					prt_GROPID4 = prt_GROPID4.Distinct();

					var prt_GROPID = listprelist.Select(c => c.経費ID);
					prt_GROPID = prt_GROPID.Distinct();


					//CSVデータ作成
					int cnt = 0;

					DataTable printdata = new DataTable();
					printdata.Columns.Add("乗務員ID", typeof(Int32));
					printdata.Columns.Add("主乗務員".ToString(), typeof(String));
					printdata.Columns.Add("車輌番号".ToString(), typeof(String));
					printdata.Columns.Add("車種".ToString(), typeof(String));
					printdata.Columns.Add("運送収入".ToString(), typeof(decimal));

					if (carlistzzz == null)
					{
						return null;
					}

					//列作成
					int id = 1;
					foreach (JMI12010g_Member a in carlistzzz)
					{
						id = a.乗務員KEY;
						break;
					}
					List<JMI12010_PRELIST_Member> plist = (from p in prelist
														   where p.乗務員KEY == id
														   select p).ToList();
					int syoukei_cnt = 0;
					foreach (JMI12010_PRELIST_Member row in plist)
					{
						if (row.経費項目名.ToString() == "【 小 計 】")
						{
							printdata.Columns.Add(row.経費項目名.ToString() + syoukei_cnt.ToString(), typeof(decimal));
							syoukei_cnt += 1;
						}
						else
						{
							var coltyp = new DataColumn(row.経費項目名.ToString(), typeof(decimal));
							coltyp.AllowDBNull = true;
							//printdata.Columns[1].AllowDBNull = true;
							printdata.Columns.Add(coltyp);
						}
					}
					var coltyp2 = new DataColumn("車輌直接費", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("車輌直接益", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("一般管理費", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("営業利益", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("稼働日数", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("実車KM", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("空車KM", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("走行KM", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("燃料L", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("燃費", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("収入1KM", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);
					coltyp2 = new DataColumn("原価1KM", typeof(decimal));
					coltyp2.AllowDBNull = true;
					printdata.Columns.Add(coltyp2);


					//データ挿入
					int rowcnt = 0;
					foreach (JMI12010g_Member row in carlistzzz)
					{
						printdata.Rows.Add();
						var list = prelist.Where(c => c.乗務員KEY == row.乗務員KEY);
						printdata.Rows[rowcnt][0] = row.乗務員ID;
						printdata.Rows[rowcnt][1] = row.主乗務員;
						printdata.Rows[rowcnt][2] = row.車輌番号;
						printdata.Rows[rowcnt][3] = row.車種;
						printdata.Rows[rowcnt][4] = row.運送収入;
						//syaryoutyokusetuhi = (decimal)(row.小計1 + row.小計2 + row.小計3);

						decimal syaryoutyokusetuhi = 0;
						int colcnt = 5;
						foreach (JMI12010_PRELIST_Member row2 in list)
						{
							printdata.Rows[rowcnt][colcnt] = row2.金額;
							if (row2.経費項目名 == "限界利益")
							{
								printdata.Rows[rowcnt][colcnt] = row.限界利益;
							}
							if (row2.経費項目名 == "【 小 計 】")
							{
								syaryoutyokusetuhi += row2.金額;
							}

							colcnt += 1;
						}
						printdata.Rows[rowcnt][colcnt] = syaryoutyokusetuhi;
						colcnt += 1;
						printdata.Rows[rowcnt][colcnt] = row.運送収入 == null ? 0 - syaryoutyokusetuhi : row.運送収入 - syaryoutyokusetuhi;
						colcnt += 1;
						if (row.一般管理費 == null)
						{
							printdata.Rows[rowcnt][colcnt] = DBNull.Value;
						}
						else
						{
							printdata.Rows[rowcnt][colcnt] = row.一般管理費;
						}
						colcnt += 1;
						if ((row.運送収入 - syaryoutyokusetuhi - row.一般管理費) == null)
						{
							printdata.Rows[rowcnt][colcnt] = DBNull.Value;
						}
						else
						{
							printdata.Rows[rowcnt][colcnt] = row.運送収入 - syaryoutyokusetuhi - row.一般管理費;
						}
						colcnt += 1;
						if (row.稼働日数 == null)
						{
							printdata.Rows[rowcnt][colcnt] = DBNull.Value;
						}
						else
						{
							printdata.Rows[rowcnt][colcnt] = row.稼働日数;
						}
						colcnt += 1;
						if (row.実車KM == null)
						{
							printdata.Rows[rowcnt][colcnt] = DBNull.Value;
						}
						else
						{
							printdata.Rows[rowcnt][colcnt] = row.実車KM;
						}
						colcnt += 1;
						if (row.空車KM == null)
						{
							printdata.Rows[rowcnt][colcnt] = DBNull.Value;
						}
						else
						{
							printdata.Rows[rowcnt][colcnt] = row.空車KM;
						}
						colcnt += 1;
						if (row.走行KM == null)
						{
							printdata.Rows[rowcnt][colcnt] = DBNull.Value;
						}
						else
						{
							printdata.Rows[rowcnt][colcnt] = row.走行KM;
						}
						colcnt += 1;
						printdata.Rows[rowcnt][colcnt] = row.燃料L;
						colcnt += 1;
						printdata.Rows[rowcnt][colcnt] = Math.Round(row.燃費, 1, MidpointRounding.AwayFromZero);
						colcnt += 1;
						printdata.Rows[rowcnt][colcnt] = Math.Round(row.収入1KM, 2, MidpointRounding.AwayFromZero);
						colcnt += 1;
						printdata.Rows[rowcnt][colcnt] = (row.走行KM == null || row.走行KM == 0) ? 0 : Math.Round((decimal)((row.一般管理費 + syaryoutyokusetuhi) / row.走行KM), 2, MidpointRounding.AwayFromZero);
						colcnt += 1;

						rowcnt += 1;
					}


					return printdata;

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