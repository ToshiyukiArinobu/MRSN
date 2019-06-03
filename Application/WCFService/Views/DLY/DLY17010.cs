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
    /// 運転日報関連機能
    /// </summary>
    public class DLY17010
    {
        /// <summary>
        /// メンバー定義
        /// </summary>
        public class DLY17010_Member
        {
            [DataMember] public string 車番 { get; set; }
            [DataMember] public string 部門 { get; set; }
            [DataMember] public string 車種 { get; set; }
            [DataMember] public string 運輸局 { get; set; }
            [DataMember] public string 主運転手 { get; set; }
            [DataMember] public string 会社名 { get; set; }
            [DataMember] public string 郵便番号 { get; set; }
            [DataMember] public string 住所 { get; set; }
            [DataMember] public string 担当者 { get; set; }
            [DataMember] public DateTime? 購入年月日 { get; set; }
            [DataMember] public decimal 購入価格 { get; set; }
            [DataMember] public int? 取得税 { get; set; }
            [DataMember] public decimal 登録手数料 { get; set; }
            [DataMember] public string 登録番号変更 { get; set; }
            [DataMember] public DateTime 番号変更年月日 { get; set; }
            [DataMember] public int 年度 { get; set; }
            [DataMember] public int? 自動車税年月日 { get; set; }
            [DataMember] public int? 自動車税納税額 { get; set; }
            [DataMember] public int? 重量税年月日 { get; set; }
            [DataMember] public int? 重量税納税額 { get; set; }
            [DataMember] public string 強制保険会社 { get; set; }
            [DataMember] public string 強制契約証券番号 { get; set; }
            [DataMember] public DateTime 強制保険期間From { get; set; }
            [DataMember] public DateTime 強制保険期間To { get; set; }
            [DataMember] public string 任意保険会社 { get; set; }
            [DataMember] public string 任意契約証券番号 { get; set; }
            [DataMember] public DateTime 任意保険期間From { get; set; }
            [DataMember] public DateTime 任意保険期間To { get; set; }
            [DataMember] public string 備考 { get; set; }

        }

  
        /// <summary>
        /// 車輌管理台帳
        /// </summary>
        /// <returns></returns>
        public List<DLY17010_Member> SHR12010_GetDataHinList()
        {
            string 取引先指定ﾋﾟｯｸｱｯﾌﾟ = string.Empty;


            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<DLY17010_Member> retList = new List<DLY17010_Member>();
                context.Connection.Open();


                //売上データ取得
                var query = (from m05C in context.M05_CAR
                             from m05C1 in context.M05_CDT1.Where(c => c.車輌KEY == m05C.車輌KEY)
                             from m05C2 in context.M05_CDT2.Where(c => c.車輌KEY == m05C.車輌KEY)
                             from m05C3 in context.M05_CDT3.Where(c => c.車輌KEY == m05C.車輌KEY)
                             from m05C4 in context.M05_CDT4.Where(c => c.車輌KEY == m05C.車輌KEY)
                             from m04 in context.M04_DRV.Where(c => c.乗務員KEY == m05C.乗務員KEY)
                             from m71 in context.M71_BUM.Where(c => c.自社部門ID == m05C.自社部門ID)
                             from m06 in context.M06_SYA.Where(c => c.車種ID == m05C.車種ID)
                              select new DLY17010_Member
                              {
                                  車番 = m05C.車輌番号,
                                  部門 = m71.自社部門名,　
                                  車種 = m06.車種名,
                                  //運輸局 = m05C.運輸局ID == 1 ? "北海道" : m05C.運輸局ID == 2 ? "東北" : m05C.運輸局ID == 3　? "北陸信越" : m05C.運輸局ID == 4 ? "関東" : 
                                  //主運転手 = m04.乗務員名,
                                  //会社名
                                  //郵便番号
                                  //住所
                                  //担当者
                                  //購入年月日 = m05C.登録日,
                                  //購入価格
                                  取得税 = m05C.固定取得税,
                                  //登録手数料
                                  //登録番号変更
                                  //番号変更年月日
                                  年度 = m05C4.年度,
                                  自動車税年月日 = m05C4.自動車税年月,
                                  自動車税納税額 = m05C4.自動車税,
                                  重量税年月日 = m05C4.重量税年月,
                                  重量税納税額 = m05C4.重量税,
                                  強制保険会社 = m05C2.契約先,
                                  強制契約証券番号 = m05C2.保険証番号,
                                  強制保険期間From = m05C2.加入年月日,
                                  強制保険期間To = m05C2.期限,
                                  任意保険会社 = m05C3.契約先,
                                  任意契約証券番号 = m05C3.保険証番号,
                                  任意保険期間From = m05C3.加入年月日,
                                  任意保険期間To = m05C3.期限,
                                  備考 = m05C.備考

                              }).AsQueryable();


                return query.ToList();
            }
        }


      
    }
}
