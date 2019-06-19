using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace KyoeiSystem.Application.WCFService
{
    // アクセスインターフェース定義
    [ServiceContract]
    public interface IM90020
    {

    }

    public class M90020_TOK
    {
        [DataMember]
        public int 得意先ID { get; set; }
    }

    public class M90020_TTAN1
    {
        [DataMember]
        public int 得意先ID { get; set; }
        [DataMember]
        public int 得意先KEY { get; set; }
        [DataMember]
        public int 発地ID { get; set; }
        [DataMember]
        public int 着地ID { get; set; }
        [DataMember]
        public int 商品ID { get; set; }
    }

    public class M90020_TTAN2
    {
        [DataMember]
        public int 得意先ID { get; set; }
        [DataMember]
        public int 得意先KEY { get; set; }
        [DataMember]
        public int 車種ID { get; set; }
        [DataMember]
        public int 発地ID { get; set; }
        [DataMember]
        public int 着地ID { get; set; }
    }

    public class M90020_TTAN3
    {
        [DataMember]
        public int 得意先ID { get; set; }
        [DataMember]
        public int 得意先KEY { get; set; }
        [DataMember]
        public int 距離 { get; set; }
        [DataMember]
        public decimal 重量 { get; set; }
    }

    public class M90020_TTAN4
    {
        [DataMember]
        public int 得意先ID { get; set; }
        [DataMember]
        public int 得意先KEY { get; set; }
        [DataMember]
        public decimal 重量 { get; set; }
        [DataMember]
        public decimal 個数 { get; set; }
        [DataMember]
        public int 着地ID { get; set; }
        [DataMember]
        public decimal 個建単価 { get; set; }
        [DataMember]
        public int 個建金額 { get; set; }
    }

    public class M90020_YTAN1
    {
        [DataMember]
        public int 支払先ID { get; set; }
        [DataMember]
        public int 支払先KEY { get; set; }
        [DataMember]
        public int 発地ID { get; set; }
        [DataMember]
        public int 着地ID { get; set; }
        [DataMember]
        public int 商品ID { get; set; }
    }

    public class M90020_YTAN2
    {
        [DataMember]
        public int 支払先ID { get; set; }
        [DataMember]
        public int 支払先KEY { get; set; }
        [DataMember]
        public int 車種ID { get; set; }
        [DataMember]
        public int 発地ID { get; set; }
        [DataMember]
        public int 着地ID { get; set; }
    }

    public class M90020_YTAN3
    {
        [DataMember]
        public int 支払先ID { get; set; }
        [DataMember]
        public int 支払先KEY { get; set; }
        [DataMember]
        public int 距離 { get; set; }
        [DataMember]
        public decimal 重量 { get; set; }
    }

    public class M90020_YTAN4
    {
        [DataMember]
        public int 支払先ID { get; set; }
        [DataMember]
        public int 支払先KEY { get; set; }
        [DataMember]
        public decimal 重量 { get; set; }
        [DataMember]
        public decimal 個数 { get; set; }
        [DataMember]
        public int 着地ID { get; set; }
    }

    public class M90020_CDT1
    {
        [DataMember]
        public int 車輌ID { get; set; }
        [DataMember]
        public int 車輌KEY { get; set; }
        [DataMember]
        public DateTime 配置年月日 { get; set; }
    }

    public class M90020_CDT2
    {
        [DataMember]
        public int 車輌ID { get; set; }
        [DataMember]
        public int 車輌KEY { get; set; }
        [DataMember]
        public DateTime 加入年月日 { get; set; }
    }

    public class M90020_CDT3
    {
        [DataMember]
        public int 車輌ID { get; set; }
        [DataMember]
        public int 車輌KEY { get; set; }
        [DataMember]
        public DateTime 加入年月日 { get; set; }
    }

    public class M90020_CDT4
    {
        [DataMember]
        public int 車輌ID { get; set; }
        [DataMember]
        public int 車輌KEY { get; set; }
        [DataMember]
        public int 年度 { get; set; }
    }

    public class M90020_TEN
    {
        [DataMember]
        public int 車輌ID { get; set; }
        [DataMember]
        public int 車輌KEY { get; set; }
        [DataMember]
        public int 年月 { get; set; }
        [DataMember]
        public int 点検日 { get; set; }
    }

    public class M90020_ZEI
    {
        [DataMember]
        public DateTime 適用開始日付 { get; set; }
    }

    public class M90020_HIN
    {
        [DataMember]
        public int 商品ID { get; set; }
    }

    public class M90020_TEK
    {
        [DataMember]
        public int 摘要ID { get; set; }
    }

    public class M90020_KEI
    {
        [DataMember]
        public int 経費項目ID { get; set; }
    }

    public class M90020_JIS
    {
        [DataMember]
        public int 自社ID { get; set; }
    }

    public class M90020_BUM
    {
        [DataMember]
        public int 自社部門ID { get; set; }
    }

    public class M90020_TNT
    {
        [DataMember]
        public int 担当者ID { get; set; }
    }

    public class M90020_ZKB
    {
        [DataMember]
        public int 税区分ID { get; set; }
    }

    public class M90020_OYK
    {
        [DataMember]
        public int 親子区分ID { get; set; }
    }

    public class M90020_SEI
    {
        [DataMember]
        public int 請求書区分ID { get; set; }
    }

    public class M90020_DBU
    {
        [DataMember]
        public int 歩合計算区分ID { get; set; }
    }

    public class M90020_KOU
    {
        [DataMember]
        public int 経費項目ID { get; set; }
        [DataMember]
        public int 車輌KEY { get; set; }
        [DataMember]
        public int 車輌ID { get; set; }
    }

    public class M90020_SEQ
    {
        [DataMember]
        public int 明細番号ID { get; set; }
    }

    public class M90020_CNTL
    {
        [DataMember]
        public int 管理ID { get; set; }
    }

    public class M90020_KZEI
    {
        [DataMember]
        public DateTime 適用開始年月日 { get; set; }
    }

    public class M90020_DRV
    {
        [DataMember]
        public int 乗務員ID { get; set; }
    }

    public class M90020_CAR
    {
        [DataMember]
        public int 車輌ID { get; set; }
        [DataMember]
        public int 車輌KEY { get; set; }
        [DataMember]
        public int 乗務員ID { get; set; }
        [DataMember]
        public int? 乗務員KEY { get; set; }
    }

    public class M90020_SYA
    {
        [DataMember]
        public int 車種ID { get; set; }
    }

    public class M90020_TIK
    {
        [DataMember]
        public int 発着地ID { get; set; }
    }

    public class M90020_UHK
    {
        [DataMember]
        public int 得意先ID { get; set; }
        [DataMember]
        public int 得意先KEY { get; set; }
        public int 請求内訳ID { get; set; }
    }

    public class M90020_DDT1
    {
        [DataMember]
        public int 乗務員ID { get; set; }
        [DataMember]
        public int 乗務員KEY { get; set; }
        public int 明細行 { get; set; }
    }

    public class M90020_DDT2
    {
        [DataMember]
        public int 乗務員ID { get; set; }
        [DataMember]
        public int 乗務員KEY { get; set; }
        public int 明細行 { get; set; }
    }

    public class M90020_DDT3
    {
        [DataMember]
        public int 乗務員ID { get; set; }
        [DataMember]
        public int 乗務員KEY { get; set; }
        public int 明細行 { get; set; }
    }

    public class M90020_MOK
    {
        [DataMember]
        public int 車輌ID { get; set; }
        [DataMember]
        public int 車輌KEY { get; set; }
        public int 年月 { get; set; }
    }

    public class M90020_KIS
    {
        [DataMember]
        public int 規制区分ID { get; set; }
    }

    public class M90020_GSYA
    {
        [DataMember]
        public int G車種ID { get; set; }
    }

    public class M90020_DRD
    {
        [DataMember] public int 乗務員KEY { get; set; }
        [DataMember] public int 集計年月 { get; set; }
        [DataMember] public int 乗務員ID { get; set; }
        [DataMember] public int 車輌KEY { get; set; }
        [DataMember] public int 車輌ID { get; set; }
    }

    public class M90020_DRSB
    {
        [DataMember]
        public int 乗務員ID { get; set; }
        [DataMember]
        public int 乗務員KEY { get; set; }
        public int 集計年月 { get; set; }
        public int 経費項目ID { get; set; }
    }

    public class M90020_DRVPIC
    {
        [DataMember]
        public int 乗務員KEY { get; set; }
    }

    public class M90020_UKE
    {
        [DataMember]
        public int 運賃計算区分ID { get; set; }
    }

    public class M90020_RIK
    {
        [DataMember]
        public int 運輸局ID { get; set; }
    }

    public class M90020_SYK
    {
        [DataMember]
        public int 出勤区分ID { get; set; }
    }

    public class M90020_TRH
    {
        [DataMember]
        public int 取引区分ID { get; set; }
    }

    public class M90020_KENALL
    {
        [DataMember]
        public int 郵便番号ID { get; set; }
    }

    public class M90020_GRID
    {
        [DataMember]
        public int 担当者ID { get; set; }
        public string 画面ID { get; set; }
        public string GRIDID { get; set; }
        public string 列名 { get; set; }
    }

    public class M90020_OTAN
    {
        [DataMember]
        public int 支払先ID { get; set; }
        [DataMember]
        public DateTime 適用開始年月日 { get; set; }
        public int 支払先KEY { get; set; }
    }

    public class T90020_T01_TRN
    {
        [DataMember]
        public int 明細番号 { get; set; }
        public int 明細行 { get; set; }
    }

    public class T90020_T02_UTRN
    {
        [DataMember]
        public int 明細番号 { get; set; }
        public int 明細行 { get; set; }
    }

    public class T90020_T03_KTRAN
    {
        [DataMember]
        public int 明細番号 { get; set; }
        public int 明細行 { get; set; }
    }

    public class T90020_T04_NYUK
    {
        [DataMember]
        public int 明細番号 { get; set; }
        public int 明細行 { get; set; }
    }


}
