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
    /// <summary>
    /// 配車依頼書作成
    /// </summary>
    public class DLY02020
    {

        /// <summary>
        /// 傭車先FAX取得メンバー
        /// </summary>
        public class DLY02020_FAX
        {
            [DataMember]
            public string FAX { get; set; }
        }

        /// <summary>
        /// 自社情報取得メンバー
        /// </summary>
        public class DLY02020_JISHA
        {
            [DataMember]
            public int? 自社ID { get; set; }
        }

        /// <summary>
        /// 積地情報取得メンバー
        /// </summary>
        public class DLY02020_TUMICHI
        {
            [DataMember]
            public string 発地名 { get; set; }
            [DataMember]
            public string 住所 { get; set; }
            [DataMember]
            public string 電話番号 { get; set; }
        }

        /// <summary>
        /// 卸地情報取得メンバー
        /// </summary>
        public class DLY02020_OROSHICHI
        {
            [DataMember]
            public string 着地名 { get; set; }
            [DataMember]
            public string 住所 { get; set; }
            [DataMember]
            public string 電話番号 { get; set; }
        }

        /// <summary>
        /// 印刷メンバー
        /// </summary>
        public class DLY02020_OUTPUT
        {
            [DataMember]
            public string 傭車先名 { get; set; }
            [DataMember]
            public string 自社名 { get; set; }
            [DataMember]
            public string 自社郵便番号 { get; set; }
            [DataMember]
            public string 自社住所1 { get; set; }
            [DataMember]
            public string 自社住所2 { get; set; }
            [DataMember]
            public string 自社TEL { get; set; }
            [DataMember]
            public string 自社FAX { get; set; }
            [DataMember]
            public string 配車担当者名 { get; set; }
            [DataMember]
            public string 車種名 { get; set; }
            [DataMember]
            public string 積荷 { get; set; }
            [DataMember]
            public int? 積地_月 { get; set; }
            [DataMember]
            public int? 積地_日 { get; set; }
            [DataMember]
            public string 積地_時間 { get; set; }
            [DataMember]
            public string 積地_住所 { get; set; }
            [DataMember]
            public string 積地_会社名 { get; set; }
            [DataMember]
            public string 積地_電話 { get; set; }
            [DataMember]
            public string 積地_メモ { get; set; }
            [DataMember]
            public int? 卸地_月 { get; set; }
            [DataMember]
            public int? 卸地_日 { get; set; }
            [DataMember]
            public string 卸地_時間 { get; set; }
            [DataMember]
            public string 卸地_住所 { get; set; }
            [DataMember]
            public string 卸地_会社名 { get; set; }
            [DataMember]
            public string 卸地_電話 { get; set; }
            [DataMember]
            public string 卸地_メモ { get; set; }
        }





        /// <summary>
        /// 傭車先FAX番号取得 
        /// </summary>
        /// <param name="i傭車先ID"></param>
        /// <returns></returns>
        public List<DLY02020_FAX> SEARCH_DLY02020_FAX(int? i傭車先ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                DLY02020_FAX result = new DLY02020_FAX();
                var query = (from m01 in context.M01_TOK.Where(c => c.削除日付 == null)
                             where m01.得意先ID == i傭車先ID
                             select new DLY02020_FAX
                             {
                                 FAX = m01.ＦＡＸ,
                             }).AsQueryable();

                return query.ToList();
            }
        }

        /// <summary>
        /// 自社情報取得 
        /// </summary>
        /// <returns></returns>
        public List<DLY02020_JISHA> SEARCH_DLY02020_JISHA()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                DLY02020_JISHA result = new DLY02020_JISHA();
                var query = (from m70 in context.M70_JIS.Where(c => c.削除日付 == null)
                             orderby m70.自社ID
                             select new DLY02020_JISHA
                             {
                                 自社ID = m70.自社ID,
                             }).AsQueryable();

                return query.ToList();
            }
        }

        /// <summary>
        /// 積地情報取得
        /// </summary>
        /// <param name="i積地ID"></param>
        /// <returns></returns>
        public List<DLY02020_TUMICHI> SEARCH_DLY02020_TUMICHI(int? i積地ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                DLY02020_TUMICHI result = new DLY02020_TUMICHI();
                var query = (from m08 in context.M08_TIK.Where(c => c.削除日付 == null)
                             where m08.発着地ID == i積地ID
                             select new DLY02020_TUMICHI
                             {
                                 発地名 = m08.発着地名,
                                 住所 = m08.住所１,
                                 電話番号 = m08.電話番号,
                             }).AsQueryable();

                return query.ToList();
            }
        }


        /// <summary>
        /// 卸地情報取得 
        /// </summary>
        /// <param name="i卸地ID"></param>
        /// <returns></returns>
        public List<DLY02020_OROSHICHI> SEARCH_DLY02020_OROSHICHI(int? i卸地ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                DLY02020_OROSHICHI result = new DLY02020_OROSHICHI();
                var query = (from m08 in context.M08_TIK.Where(c => c.削除日付 == null)
                             where m08.発着地ID == i卸地ID
                             select new DLY02020_OROSHICHI
                             {
                                 着地名 = m08.発着地名,
                                 住所 = m08.住所１,
                                 電話番号 = m08.電話番号,
                             }).AsQueryable();

                return query.ToList();
            }
        }

        /// <summary>
        /// 配車依頼書発行
        /// </summary>
        /// <param name="i傭車先ID"></param>
        /// <param name="i傭車先名"></param>
        /// <param name="i自社ID"></param>
        /// <param name="i自社名"></param>
        /// <param name="i配車担当者名"></param>
        /// <param name="i車種名"></param>
        /// <param name="i積荷"></param>
        /// <param name="iT日付"></param>
        /// <param name="iT時間"></param>
        /// <param name="iT会社名"></param>
        /// <param name="iT住所"></param>
        /// <param name="iTTEL"></param>
        /// <param name="iTメモ"></param>
        /// <param name="iO日付"></param>
        /// <param name="iO時間"></param>
        /// <param name="iO会社名"></param>
        /// <param name="iO住所"></param>
        /// <param name="iOTEL"></param>
        /// <param name="iOメモ"></param>
        /// <returns></returns>
        public List<DLY02020_OUTPUT> SEARCH_DLY02020(int? i傭車先ID, string i傭車先名, int? i自社ID, string i自社名, string i配車担当者名, string i車種名, string i積荷, DateTime? iT日付,
                                                     string iT時間, string iT会社名, string iT住所, string iTTEL, string iTメモ, DateTime? iO日付, string iO時間, string iO会社名, string iO住所,
                                                     string iOTEL, string iOメモ)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                DLY02020_OUTPUT result = new DLY02020_OUTPUT();
                var query = (from m70 in context.M70_JIS.Where(c => c.削除日付 == null)
                             from m01 in context.M01_TOK.Where(c => c.得意先ID == i傭車先ID && c.削除日付 == null)
                             where m70.自社ID == i自社ID
                             select new DLY02020_OUTPUT
                             {
                                 傭車先名 = m01 == null ? i傭車先名 : m01.得意先名１,
                                 自社名 = m70.自社名,
                                 自社郵便番号 = m70.郵便番号,
                                 自社住所1 = m70.住所１,
                                 自社住所2 = m70.住所２,
                                 自社TEL = m70.電話番号,
                                 自社FAX = m70.ＦＡＸ,
                                 配車担当者名 = i配車担当者名,
                                 車種名 = i車種名,
                                 積荷 = i積荷,
                                 積地_月 = iT日付 == null ? (int?)null : iT日付.Value.Month,
                                 積地_日 = iT日付 == null ? (int?)null : iT日付.Value.Day,
                                 積地_時間 = iT時間,
                                 積地_住所 = iT住所,
                                 積地_会社名 = iT会社名,
                                 積地_電話 = iTTEL,
                                 積地_メモ = iTメモ,
                                 卸地_月 = iO日付 == null ? (int?)null : iO日付.Value.Month,
                                 卸地_日 = iO日付 == null ? (int?)null : iO日付.Value.Day,
                                 卸地_時間 = iO時間,
                                 卸地_住所 = iO住所,
                                 卸地_会社名 = iO会社名,
                                 卸地_電話 = iOTEL,
                                 卸地_メモ = iOメモ,
                             }).AsQueryable();

                return query.ToList();
            }
        }


    }
}