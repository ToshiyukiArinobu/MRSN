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
    public class DLY17010_1
    {
        [DataContract]
        public class M05_CAR_List {

        [DataMember] public int 車輌KEY { get; set; }
        [DataMember] public int 車輌ID { get; set; }
        [DataMember] public string 乗務員名 { get; set; }
		[DataMember] public string 車輌番号 { get; set; }
        [DataMember] public string 車番 { get; set; }
        [DataMember] public string 車種名 { get; set; }
        [DataMember] public DateTime? 登録日 { get; set; }
        [DataMember] public int 初年度 { get; set; }
        [DataMember] public string 自動車種別 { get; set; }
        [DataMember] public string 用途 { get; set; }
        [DataMember] public string 自家用事業用 { get; set; }
        [DataMember] public string 車体形状 { get; set; }
        [DataMember] public string 車名 { get; set; }
        [DataMember] public int 乗車定員 { get; set; }
        [DataMember] public int 長さ { get; set; }
        [DataMember] public int 幅 { get; set; }
        [DataMember] public int 高さ { get; set; }
        [DataMember] public int 最大積載量 { get; set; }
        [DataMember] public int 車輌重量 { get; set; }
        [DataMember] public int 車輌総重量 { get; set; }
        [DataMember] public string 車台番号 { get; set; }
        [DataMember] public string 型式 { get; set; }
        [DataMember] public string 原動機型式 { get; set; }
        [DataMember] public int 総排気量 { get; set; }
        [DataMember] public string 燃料の種類 { get; set; }
        [DataMember] public int 前前軸重 { get; set; }
        [DataMember] public int 前後軸重 { get; set; }
        [DataMember] public int 後前軸重 { get; set; }
        [DataMember] public int 後後軸重 { get; set; }
        [DataMember] public string 備考 { get; set; }
        
        //CDT2 １行目
        [DataMember] public string 契約先_CDT2_0 { get; set; }
        [DataMember] public string 保険証番号_CDT2_0 { get; set; }
        [DataMember] public DateTime? 加入年月日_CDT2_0 { get; set; }
        [DataMember] public DateTime? 期限_CDT2_0 { get; set; }
        [DataMember] public string s加入年月日_CDT2_0 { get; set; }
        [DataMember] public string s期限_CDT2_0 { get; set; }

        //CDT2 ２行目
        [DataMember] public string 契約先_CDT2_1 { get; set; }
        [DataMember] public string 保険証番号_CDT2_1 { get; set; }
        [DataMember] public DateTime? 加入年月日_CDT2_1 { get; set; }
        [DataMember] public DateTime? 期限_CDT2_1 { get; set; }
        [DataMember] public string s加入年月日_CDT2_1 { get; set; }
        [DataMember] public string s期限_CDT2_1 { get; set; }
        //CDT2 ３行目
        [DataMember] public string 契約先_CDT2_2 { get; set; }
        [DataMember] public string 保険証番号_CDT2_2 { get; set; }
        [DataMember] public DateTime? 加入年月日_CDT2_2 { get; set; }
        [DataMember] public DateTime? 期限_CDT2_2 { get; set; }
        [DataMember] public string s加入年月日_CDT2_2 { get; set; }
        [DataMember] public string s期限_CDT2_2 { get; set; }
        //CDT2 ４行目
        [DataMember] public string 契約先_CDT2_3 { get; set; }
        [DataMember] public string 保険証番号_CDT2_3 { get; set; }
        [DataMember] public DateTime? 加入年月日_CDT2_3 { get; set; }
        [DataMember] public DateTime? 期限_CDT2_3 { get; set; }
        [DataMember] public string s加入年月日_CDT2_3 { get; set; }
        [DataMember] public string s期限_CDT2_3 { get; set; }
        //CDT2 ５行目
        [DataMember] public string 契約先_CDT2_4 { get; set; }
        [DataMember] public string 保険証番号_CDT2_4 { get; set; }
        [DataMember] public DateTime? 加入年月日_CDT2_4 { get; set; }
        [DataMember] public DateTime? 期限_CDT2_4 { get; set; }
        [DataMember] public string s加入年月日_CDT2_4 { get; set; }
        [DataMember] public string s期限_CDT2_4 { get; set; }
        //CDT2 ６行目
        [DataMember] public string 契約先_CDT2_5 { get; set; }
        [DataMember] public string 保険証番号_CDT2_5 { get; set; }
        [DataMember] public DateTime? 加入年月日_CDT2_5 { get; set; }
        [DataMember] public DateTime? 期限_CDT2_5 { get; set; }
        [DataMember] public string s加入年月日_CDT2_5 { get; set; }
        [DataMember] public string s期限_CDT2_5 { get; set; }
        //CDT2 ７行目
        [DataMember] public string 契約先_CDT2_6 { get; set; }
        [DataMember] public string 保険証番号_CDT2_6 { get; set; }
        [DataMember] public DateTime? 加入年月日_CDT2_6 { get; set; }
        [DataMember] public DateTime? 期限_CDT2_6 { get; set; }
        [DataMember] public string s加入年月日_CDT2_6 { get; set; }
        [DataMember] public string s期限_CDT2_6 { get; set; }
        //CDT2 ８行目
        [DataMember] public string 契約先_CDT2_7 { get; set; }
        [DataMember] public string 保険証番号_CDT2_7 { get; set; }
        [DataMember] public DateTime? 加入年月日_CDT2_7 { get; set; }
        [DataMember] public DateTime? 期限_CDT2_7 { get; set; }
        [DataMember] public string s加入年月日_CDT2_7 { get; set; }
        [DataMember] public string s期限_CDT2_7 { get; set; }
        //CDT2 ９行目
        [DataMember] public string 契約先_CDT2_8 { get; set; }
        [DataMember] public string 保険証番号_CDT2_8 { get; set; }
        [DataMember] public DateTime? 加入年月日_CDT2_8 { get; set; }
        [DataMember] public DateTime? 期限_CDT2_8 { get; set; }
        [DataMember] public string s加入年月日_CDT2_8 { get; set; }
        [DataMember] public string s期限_CDT2_8 { get; set; }
        //CDT2 １０行目
        [DataMember] public string 契約先_CDT2_9 { get; set; }
        [DataMember] public string 保険証番号_CDT2_9 { get; set; }
        [DataMember] public DateTime? 加入年月日_CDT2_9 { get; set; }
        [DataMember] public DateTime? 期限_CDT2_9 { get; set; }
        [DataMember] public string s加入年月日_CDT2_9 { get; set; }
        [DataMember] public string s期限_CDT2_9 { get; set; }

        //CDT3 １行目
        [DataMember] public string 契約先_CDT3_0 { get; set; }
        [DataMember] public string 保険証番号_CDT3_0 { get; set; }
        [DataMember] public DateTime? 加入年月日_CDT3_0 { get; set; }
        [DataMember] public DateTime? 期限_CDT3_0 { get; set; }
        [DataMember] public string s加入年月日_CDT3_0 { get; set; }
        [DataMember] public string s期限_CDT3_0 { get; set; }
        //CDT3 ２行目
        [DataMember] public string 契約先_CDT3_1 { get; set; }
        [DataMember] public string 保険証番号_CDT3_1 { get; set; }
        [DataMember] public DateTime? 加入年月日_CDT3_1 { get; set; }
        [DataMember] public DateTime? 期限_CDT3_1 { get; set; }
        [DataMember] public string s加入年月日_CDT3_1 { get; set; }
        [DataMember] public string s期限_CDT3_1 { get; set; }
        //CDT3 ３行目
        [DataMember] public string 契約先_CDT3_2 { get; set; }
        [DataMember] public string 保険証番号_CDT3_2 { get; set; }
        [DataMember] public DateTime? 加入年月日_CDT3_2 { get; set; }
        [DataMember] public DateTime? 期限_CDT3_2 { get; set; }
        [DataMember] public string s加入年月日_CDT3_2 { get; set; }
        [DataMember] public string s期限_CDT3_2 { get; set; }
        //CDT3 ４行目
        [DataMember] public string 契約先_CDT3_3 { get; set; }
        [DataMember] public string 保険証番号_CDT3_3 { get; set; }
        [DataMember] public DateTime? 加入年月日_CDT3_3 { get; set; }
        [DataMember] public DateTime? 期限_CDT3_3 { get; set; }
        [DataMember] public string s加入年月日_CDT3_3 { get; set; }
        [DataMember] public string s期限_CDT3_3 { get; set; }
        //CDT3 ５行目
        [DataMember] public string 契約先_CDT3_4 { get; set; }
        [DataMember] public string 保険証番号_CDT3_4 { get; set; }
        [DataMember] public DateTime? 加入年月日_CDT3_4 { get; set; }
        [DataMember] public DateTime? 期限_CDT3_4 { get; set; }
        [DataMember] public string s加入年月日_CDT3_4 { get; set; }
        [DataMember] public string s期限_CDT3_4 { get; set; }
        //CDT3 ６行目
        [DataMember] public string 契約先_CDT3_5 { get; set; }
        [DataMember] public string 保険証番号_CDT3_5 { get; set; }
        [DataMember] public DateTime? 加入年月日_CDT3_5 { get; set; }
        [DataMember] public DateTime? 期限_CDT3_5 { get; set; }
        [DataMember] public string s加入年月日_CDT3_5 { get; set; }
        [DataMember] public string s期限_CDT3_5 { get; set; }
        //CDT3 ７行目
        [DataMember] public string 契約先_CDT3_6 { get; set; }
        [DataMember] public string 保険証番号_CDT3_6 { get; set; }
        [DataMember] public DateTime? 加入年月日_CDT3_6 { get; set; }
        [DataMember] public DateTime? 期限_CDT3_6 { get; set; }
        [DataMember] public string s加入年月日_CDT3_6 { get; set; }
        [DataMember] public string s期限_CDT3_6 { get; set; }
        //CDT3 ８行目
        [DataMember] public string 契約先_CDT3_7 { get; set; }
        [DataMember] public string 保険証番号_CDT3_7 { get; set; }
        [DataMember] public DateTime? 加入年月日_CDT3_7 { get; set; }
        [DataMember] public DateTime? 期限_CDT3_7 { get; set; }
        [DataMember] public string s加入年月日_CDT3_7 { get; set; }
        [DataMember] public string s期限_CDT3_7 { get; set; }
        //CDT3 ９行目
        [DataMember] public string 契約先_CDT3_8 { get; set; }
        [DataMember] public string 保険証番号_CDT3_8 { get; set; }
        [DataMember] public DateTime? 加入年月日_CDT3_8 { get; set; }
        [DataMember] public DateTime? 期限_CDT3_8 { get; set; }
        [DataMember] public string s加入年月日_CDT3_8 { get; set; }
        [DataMember] public string s期限_CDT3_8 { get; set; }
        //CDT3 １０行目
        [DataMember] public string 契約先_CDT3_9 { get; set; }
        [DataMember] public string 保険証番号_CDT3_9 { get; set; }
        [DataMember] public DateTime? 加入年月日_CDT3_9 { get; set; }
        [DataMember] public DateTime? 期限_CDT3_9 { get; set; }
        [DataMember] public string s加入年月日_CDT3_9 { get; set; }
        [DataMember] public string s期限_CDT3_9 { get; set; }

        //CDT4 １行目
        [DataMember] public int 年度_0 { get; set; }
        [DataMember] public int? 自動車税年月_0 { get; set; }
        [DataMember] public int? 自動車税_0 { get; set; }
        [DataMember] public int? 重量税年月_0 { get; set; }
        [DataMember] public int? 重量税_0 { get; set; }
        [DataMember] public string s年度_0 { get; set; }
        [DataMember] public string s自動車税年月_0 { get; set; }
        [DataMember] public string s重量税年月_0 { get; set; }
        //CDT4 ２行目
        [DataMember] public int 年度_1 { get; set; }
        [DataMember] public int? 自動車税年月_1 { get; set; }
        [DataMember] public int? 自動車税_1 { get; set; }
        [DataMember] public int? 重量税年月_1 { get; set; }
        [DataMember] public int? 重量税_1 { get; set; }
        [DataMember] public string s年度_1 { get; set; }
        [DataMember] public string s自動車税年月_1 { get; set; }
        [DataMember] public string s重量税年月_1 { get; set; }
        //CDT4 ３行目
        [DataMember] public int 年度_2 { get; set; }
        [DataMember] public int? 自動車税年月_2 { get; set; }
        [DataMember] public int? 自動車税_2 { get; set; }
        [DataMember] public int? 重量税年月_2 { get; set; }
        [DataMember] public int? 重量税_2 { get; set; }
        [DataMember] public string s年度_2 { get; set; }
        [DataMember] public string s自動車税年月_2 { get; set; }
        [DataMember] public string s重量税年月_2 { get; set; }
        //CDT4 ４行目
        [DataMember] public int 年度_3 { get; set; }
        [DataMember] public int? 自動車税年月_3 { get; set; }
        [DataMember] public int? 自動車税_3 { get; set; }
        [DataMember] public int? 重量税年月_3 { get; set; }
        [DataMember] public int? 重量税_3 { get; set; }
        [DataMember] public string s年度_3 { get; set; }
        [DataMember] public string s自動車税年月_3 { get; set; }
        [DataMember] public string s重量税年月_3 { get; set; }
        //CDT4 ５行目
        [DataMember] public int 年度_4 { get; set; }
        [DataMember] public int? 自動車税年月_4 { get; set; }
        [DataMember] public int? 自動車税_4 { get; set; }
        [DataMember] public int? 重量税年月_4 { get; set; }
        [DataMember] public int? 重量税_4 { get; set; }
        [DataMember] public string s年度_4 { get; set; }
        [DataMember] public string s自動車税年月_4 { get; set; }
        [DataMember] public string s重量税年月_4 { get; set; }
        //CDT4 ６行目
        [DataMember] public int 年度_5 { get; set; }
        [DataMember] public int? 自動車税年月_5 { get; set; }
        [DataMember] public int? 自動車税_5 { get; set; }
        [DataMember] public int? 重量税年月_5 { get; set; }
        [DataMember] public int? 重量税_5 { get; set; }
        [DataMember] public string s年度_5 { get; set; }
        [DataMember] public string s自動車税年月_5 { get; set; }
        [DataMember] public string s重量税年月_5 { get; set; }
        //CDT4 ７行目
        [DataMember] public int 年度_6 { get; set; }
        [DataMember] public int? 自動車税年月_6 { get; set; }
        [DataMember] public int? 自動車税_6 { get; set; }
        [DataMember] public int? 重量税年月_6 { get; set; }
        [DataMember] public int? 重量税_6 { get; set; }
        [DataMember] public string s年度_6 { get; set; }
        [DataMember] public string s自動車税年月_6 { get; set; }
        [DataMember] public string s重量税年月_6 { get; set; }
        //CDT4 ８行目
        [DataMember] public int 年度_7 { get; set; }
        [DataMember] public int? 自動車税年月_7 { get; set; }
        [DataMember] public int? 自動車税_7 { get; set; }
        [DataMember] public int? 重量税年月_7 { get; set; }
        [DataMember] public int? 重量税_7 { get; set; }
        [DataMember] public string s年度_7 { get; set; }
        [DataMember] public string s自動車税年月_7 { get; set; }
        [DataMember] public string s重量税年月_7 { get; set; }
        //CDT4 ９行目
        [DataMember] public int 年度_8 { get; set; }
        [DataMember] public int? 自動車税年月_8 { get; set; }
        [DataMember] public int? 自動車税_8 { get; set; }
        [DataMember] public int? 重量税年月_8 { get; set; }
        [DataMember] public int? 重量税_8 { get; set; }
        [DataMember] public string s年度_8 { get; set; }
        [DataMember] public string s自動車税年月_8 { get; set; }
        [DataMember] public string s重量税年月_8 { get; set; }
        //CDT4 １０行目
        [DataMember] public int 年度_9 { get; set; }
        [DataMember] public int? 自動車税年月_9 { get; set; }
        [DataMember] public int? 自動車税_9 { get; set; }
        [DataMember] public int? 重量税年月_9 { get; set; }
        [DataMember] public int? 重量税_9 { get; set; }
        [DataMember] public string s年度_9 { get; set; }
        [DataMember] public string s自動車税年月_9 { get; set; }
        [DataMember] public string s重量税年月_9 { get; set; }

	}

  
        /// <summary>
        /// 車輌管理台帳
        /// </summary>
        /// <returns>M05_CAR_List</returns>
        public List<M05_CAR_List> GetSearchCarList()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<M05_CAR_List> retList = new List<M05_CAR_List>();
                context.Connection.Open();

                //自動車検査証データ（M05_CAR）
                var ret = (from m05 in context.M05_CAR
                           from m06 in context.M06_SYA.Where(m06 => m06.車種ID == m05.車種ID).DefaultIfEmpty()
                           from m04 in context.M04_DRV.Where(m04 => m04.乗務員KEY == m05.乗務員KEY).DefaultIfEmpty()
                           where m05.削除日付 == null
                           select new M05_CAR_List
                           {
                               車輌KEY = m05.車輌KEY,
                               車輌ID = m05.車輌ID,
                               車番 = m05.車輌登録番号,
                               車輌番号 = m05.車輌番号,
                               車種名 = m06.車種名,
                               乗務員名 = m04.乗務員名,
                               登録日 = m05.登録日,
                               初年度 = m05.初年度登録月,
                               自動車種別 = m05.自動車種別,
                               用途 = m05.用途,
                               自家用事業用 = m05.自家用事業用,
                               車体形状 = m05.車体形状,
                               車名 = m05.車名,
                               乗車定員 = m05.乗車定員,
                               長さ = m05.長さ,
                               幅 = m05.幅,
                               高さ = m05.高さ,
                               最大積載量 = m05.車輌最大積載量,
                               車輌重量 = m05.車輌重量,
                               車輌総重量 = m05.車輌総重量,
                               車台番号 = m05.車台番号,
                               型式 = m05.型式,
                               原動機型式 = m05.原動機型式,
                               総排気量 = m05.総排気量,
                               燃料の種類 = m05.燃料種類,
                               前前軸重 = m05.前前軸重,
                               前後軸重 = m05.前後軸重,
                               後前軸重 = m05.後前軸重,
                               後後軸重 = m05.後後軸重,
                               備考 = m05.備考,
                           }).ToList();

                //CDT2データ取得
                var ret2 = (from cdt2 in context.M05_CDT2
                            orderby cdt2.加入年月日
                            select new M05_CAR_List
                            {
                                車輌KEY = cdt2.車輌KEY,
                                加入年月日_CDT2_0 = cdt2.加入年月日,
                                期限_CDT2_0 = cdt2.期限,
                                契約先_CDT2_0 = cdt2.契約先,
                                保険証番号_CDT2_0 = cdt2.保険証番号,
                            }).ToList();

                //CDT3データ取得
                var ret3 = (from cdt3 in context.M05_CDT3
                            orderby cdt3.加入年月日
                            select new M05_CAR_List
                            {
                                車輌KEY = cdt3.車輌KEY,
                                加入年月日_CDT3_0 = cdt3.加入年月日,
                                期限_CDT3_0 = cdt3.期限,
                                契約先_CDT3_0 = cdt3.契約先,
                                保険証番号_CDT3_0 = cdt3.保険証番号,
                            }).ToList();

                //CDT4データ取得
                var ret4 = (from cdt4 in context.M05_CDT4
                            orderby cdt4.年度
                            select new M05_CAR_List
                            {
                                車輌KEY = cdt4.車輌KEY,
                                年度_0 = cdt4.年度,
                                自動車税年月_0 = cdt4.自動車税年月,
                                自動車税_0 = cdt4.自動車税,
                                重量税年月_0 = cdt4.重量税年月,
                                重量税_0 = cdt4.重量税,
                            }).ToList();

                //M05_CARにある車輌台数分を回す
                foreach (M05_CAR_List a in ret)
                {
                    //強陪保険データをリスト型で取得
                    var cdt2 = ret2.Where(c => c.車輌KEY == a.車輌KEY).OrderBy(c => c.加入年月日_CDT2_0).ToList();
                    //任意保険データをリスト型で取得
                    var cdt3 = ret3.Where(c => c.車輌KEY == a.車輌KEY).OrderBy(c => c.加入年月日_CDT3_0).ToList();
                    //車輛納税データをリスト型で取得
                    var cdt4 = ret4.Where(c => c.車輌KEY == a.車輌KEY).OrderBy(c => c.年度_0).ToList();


                    if (cdt2.Count > 0 || cdt3.Count > 0 || cdt4.Count > 0)
                    {
                        //強陪保険データがあれば、取得レコード分をループさせる
                        for (int i = 0; i < cdt2.Count; i++)
                        {
                            // iの値に列に追加するデータの振り分けする
                            if(i == 0)
                            {
                                //１行目出力
                                a.契約先_CDT2_0 = cdt2[0].契約先_CDT2_0 == null ? string.Empty : cdt2[0].契約先_CDT2_0;
                                a.保険証番号_CDT2_0 = cdt2[0].保険証番号_CDT2_0 == null ? string.Empty : cdt2[0].保険証番号_CDT2_0;
                                a.s加入年月日_CDT2_0 = cdt2[0].加入年月日_CDT2_0 == null ? string.Empty : cdt2[0].加入年月日_CDT2_0.ToString().Substring(0, 4) + "年" + cdt2[0].加入年月日_CDT2_0.ToString().Substring(5, 2) + "月" + cdt2[0].加入年月日_CDT2_0.ToString().Substring(8, 2) + "日　～";
                                a.s期限_CDT2_0 = cdt2[0].期限_CDT2_0 == null ? string.Empty : cdt2[0].期限_CDT2_0.ToString().Substring(0, 4) + "年" + cdt2[0].期限_CDT2_0.ToString().Substring(5, 2) + "月" + cdt2[0].期限_CDT2_0.ToString().Substring(8, 2) + "日";
                            }
                            if (i == 1)
                            {
                                //２行目出力
                                a.契約先_CDT2_1 = cdt2[1].契約先_CDT2_0 == null ? string.Empty : cdt2[1].契約先_CDT2_0;
                                a.保険証番号_CDT2_1 = cdt2[1].保険証番号_CDT2_0 == null ? string.Empty : cdt2[1].保険証番号_CDT2_0;
                                a.s加入年月日_CDT2_1 = cdt2[1].加入年月日_CDT2_0 == null ? string.Empty : cdt2[1].加入年月日_CDT2_0.ToString().Substring(0, 4) + "年" + cdt2[1].加入年月日_CDT2_0.ToString().Substring(5, 2) + "月" + cdt2[1].加入年月日_CDT2_0.ToString().Substring(8, 2) + "日　～";
                                a.s期限_CDT2_1 = cdt2[1].期限_CDT2_0 == null ? string.Empty : cdt2[1].期限_CDT2_0.ToString().Substring(0, 4) + "年" + cdt2[1].期限_CDT2_0.ToString().Substring(5, 2) + "月" + cdt2[1].期限_CDT2_0.ToString().Substring(8, 2) + "日";
                            }
                            if (i == 2)
                            {
                                //３行目出力
                                a.契約先_CDT2_2 = cdt2[2].契約先_CDT2_0 == null ? string.Empty : cdt2[2].契約先_CDT2_0;
                                a.保険証番号_CDT2_2 = cdt2[2].保険証番号_CDT2_0 == null ? string.Empty : cdt2[2].保険証番号_CDT2_0;
                                a.s加入年月日_CDT2_2 = cdt2[2].加入年月日_CDT2_0 == null ? string.Empty : cdt2[2].加入年月日_CDT2_0.ToString().Substring(0, 4) + "年" + cdt2[2].加入年月日_CDT2_0.ToString().Substring(5, 2) + "月" + cdt2[2].加入年月日_CDT2_0.ToString().Substring(8, 2) + "日　～";
                                a.s期限_CDT2_2 = cdt2[2].期限_CDT2_0 == null ? string.Empty : cdt2[2].期限_CDT2_0.ToString().Substring(0, 4) + "年" + cdt2[2].期限_CDT2_0.ToString().Substring(5, 2) + "月" + cdt2[2].期限_CDT2_0.ToString().Substring(8, 2) + "日";
                            }
                            if (i == 3)
                            {
                                //４行目出力
                                a.契約先_CDT2_3 = cdt2[3].契約先_CDT2_0 == null ? string.Empty : cdt2[3].契約先_CDT2_0;
                                a.保険証番号_CDT2_3 = cdt2[3].保険証番号_CDT2_0 == null ? string.Empty : cdt2[3].保険証番号_CDT2_0;
                                a.s加入年月日_CDT2_3 = cdt2[3].加入年月日_CDT2_0 == null ? string.Empty : cdt2[3].加入年月日_CDT2_0.ToString().Substring(0, 4) + "年" + cdt2[3].加入年月日_CDT2_0.ToString().Substring(5, 2) + "月" + cdt2[3].加入年月日_CDT2_0.ToString().Substring(8, 2) + "日　～";
                                a.s期限_CDT2_3 = cdt2[3].期限_CDT2_0 == null ? string.Empty : cdt2[3].期限_CDT2_0.ToString().Substring(0, 4) + "年" + cdt2[3].期限_CDT2_0.ToString().Substring(5, 2) + "月" + cdt2[3].期限_CDT2_0.ToString().Substring(8, 2) + "日";
                            }
                            if (i == 4)
                            {
                                //５行目出力
                                a.契約先_CDT2_4 = cdt2[4].契約先_CDT2_0 == null ? string.Empty : cdt2[4].契約先_CDT2_0;
                                a.保険証番号_CDT2_4 = cdt2[4].保険証番号_CDT2_0 == null ? string.Empty : cdt2[4].保険証番号_CDT2_0;
                                a.s加入年月日_CDT2_4 = cdt2[4].加入年月日_CDT2_0 == null ? string.Empty : cdt2[4].加入年月日_CDT2_0.ToString().Substring(0, 4) + "年" + cdt2[4].加入年月日_CDT2_0.ToString().Substring(5, 2) + "月" + cdt2[4].加入年月日_CDT2_0.ToString().Substring(8, 2) + "日　～";
                                a.s期限_CDT2_4 = cdt2[4].期限_CDT2_0 == null ? string.Empty : cdt2[4].期限_CDT2_0.ToString().Substring(0, 4) + "年" + cdt2[4].期限_CDT2_0.ToString().Substring(5, 2) + "月" + cdt2[4].期限_CDT2_0.ToString().Substring(8, 2) + "日";
                            }
                            if (i == 5)
                            {
                                //６行目出力
                                a.契約先_CDT2_5 = cdt2[5].契約先_CDT2_0 == null ? string.Empty : cdt2[5].契約先_CDT2_0;
                                a.保険証番号_CDT2_5 = cdt2[5].保険証番号_CDT2_0 == null ? string.Empty : cdt2[5].保険証番号_CDT2_0;
                                a.s加入年月日_CDT2_5 = cdt2[5].加入年月日_CDT2_0 == null ? string.Empty : cdt2[5].加入年月日_CDT2_0.ToString().Substring(0, 4) + "年" + cdt2[5].加入年月日_CDT2_0.ToString().Substring(5, 2) + "月" + cdt2[5].加入年月日_CDT2_0.ToString().Substring(8, 2) + "日　～";
                                a.s期限_CDT2_5 = cdt2[5].期限_CDT2_0 == null ? string.Empty : cdt2[5].期限_CDT2_0.ToString().Substring(0, 4) + "年" + cdt2[5].期限_CDT2_0.ToString().Substring(5, 2) + "月" + cdt2[5].期限_CDT2_0.ToString().Substring(8, 2) + "日";
                            }
                            if (i == 6)
                            {
                                //７行目出力
                                a.契約先_CDT2_6 = cdt2[6].契約先_CDT2_0 == null ? string.Empty : cdt2[6].契約先_CDT2_0;
                                a.保険証番号_CDT2_6 = cdt2[6].保険証番号_CDT2_0 == null ? string.Empty : cdt2[6].保険証番号_CDT2_0;
                                a.s加入年月日_CDT2_6 = cdt2[6].加入年月日_CDT2_0 == null ? string.Empty : cdt2[6].加入年月日_CDT2_0.ToString().Substring(0, 4) + "年" + cdt2[6].加入年月日_CDT2_0.ToString().Substring(5, 2) + "月" + cdt2[6].加入年月日_CDT2_0.ToString().Substring(8, 2) + "日　～";
                                a.s期限_CDT2_6 = cdt2[6].期限_CDT2_0 == null ? string.Empty : cdt2[6].期限_CDT2_0.ToString().Substring(0, 4) + "年" + cdt2[6].期限_CDT2_0.ToString().Substring(5, 2) + "月" + cdt2[6].期限_CDT2_0.ToString().Substring(8, 2) + "日";
                            }
                            if (i == 7)
                            {
                                //８行目出力
                                a.契約先_CDT2_7 = cdt2[7].契約先_CDT2_0 == null ? string.Empty : cdt2[7].契約先_CDT2_0;
                                a.保険証番号_CDT2_7 = cdt2[7].保険証番号_CDT2_0 == null ? string.Empty : cdt2[7].保険証番号_CDT2_0;
                                a.s加入年月日_CDT2_7 = cdt2[7].加入年月日_CDT2_0 == null ? string.Empty : cdt2[7].加入年月日_CDT2_0.ToString().Substring(0, 4) + "年" + cdt2[7].加入年月日_CDT2_0.ToString().Substring(5, 2) + "月" + cdt2[7].加入年月日_CDT2_0.ToString().Substring(8, 2) + "日　～";
                                a.s期限_CDT2_7 = cdt2[7].期限_CDT2_0 == null ? string.Empty : cdt2[7].期限_CDT2_0.ToString().Substring(0, 4) + "年" + cdt2[7].期限_CDT2_0.ToString().Substring(5, 2) + "月" + cdt2[7].期限_CDT2_0.ToString().Substring(8, 2) + "日";
                            }
                            if (i == 8)
                            {
                                //９行目出力
                                a.契約先_CDT2_8 = cdt2[8].契約先_CDT2_0 == null ? string.Empty : cdt2[8].契約先_CDT2_0;
                                a.保険証番号_CDT2_8 = cdt2[8].保険証番号_CDT2_0 == null ? string.Empty : cdt2[8].保険証番号_CDT2_0;
                                a.s加入年月日_CDT2_8 = cdt2[8].加入年月日_CDT2_0 == null ? string.Empty : cdt2[8].加入年月日_CDT2_0.ToString().Substring(0, 4) + "年" + cdt2[8].加入年月日_CDT2_0.ToString().Substring(5, 2) + "月" + cdt2[8].加入年月日_CDT2_0.ToString().Substring(8, 2) + "日　～";
                                a.s期限_CDT2_8 = cdt2[8].期限_CDT2_0 == null ? string.Empty : cdt2[8].期限_CDT2_0.ToString().Substring(0, 4) + "年" + cdt2[8].期限_CDT2_0.ToString().Substring(5, 2) + "月" + cdt2[8].期限_CDT2_0.ToString().Substring(8, 2) + "日";
                            }
                            if (i == 9)
                            {
                                //１０行目出力
                                a.契約先_CDT2_9 = cdt2[9].契約先_CDT2_0 == null ? string.Empty : cdt2[9].契約先_CDT2_0;
                                a.保険証番号_CDT2_9 = cdt2[9].保険証番号_CDT2_0 == null ? string.Empty : cdt2[9].保険証番号_CDT2_0;
                                a.s加入年月日_CDT2_9 = cdt2[9].加入年月日_CDT2_0 == null ? string.Empty : cdt2[9].加入年月日_CDT2_0.ToString().Substring(0, 4) + "年" + cdt2[9].加入年月日_CDT2_0.ToString().Substring(5, 2) + "月" + cdt2[9].加入年月日_CDT2_0.ToString().Substring(8, 2) + "日　～";
                                a.s期限_CDT2_9 = cdt2[9].期限_CDT2_0 == null ? string.Empty : cdt2[9].期限_CDT2_0.ToString().Substring(0, 4) + "年" + cdt2[9].期限_CDT2_0.ToString().Substring(5, 2) + "月" + cdt2[9].期限_CDT2_0.ToString().Substring(8, 2) + "日";
                            }
                        }

                        //任意保険データ
                        for (int i = 0; i < cdt3.Count; i++)
                        {
                            if (i == 0)
                            {
                                //１行目出力
                                a.契約先_CDT3_0 = cdt3[0].契約先_CDT3_0 == null ? string.Empty : cdt3[0].契約先_CDT3_0;
                                a.保険証番号_CDT3_0 = cdt3[0].保険証番号_CDT3_0 == null ? string.Empty : cdt3[0].保険証番号_CDT3_0;
                                a.s加入年月日_CDT3_0 = cdt3[0].加入年月日_CDT3_0 == null ? string.Empty : cdt3[0].加入年月日_CDT3_0.ToString().Substring(0, 4) + "年" + cdt3[0].加入年月日_CDT3_0.ToString().Substring(5, 2) + "月" + cdt3[0].加入年月日_CDT3_0.ToString().Substring(8, 2) + "日　～";
                                a.s期限_CDT3_0 = cdt3[0].期限_CDT3_0 == null ? string.Empty : cdt3[0].期限_CDT3_0.ToString().Substring(0, 4) + "年" + cdt3[0].期限_CDT3_0.ToString().Substring(5, 2) + "月" + cdt3[0].期限_CDT3_0.ToString().Substring(8, 2) + "日";
                            }
                            if (i == 1)
                            {
                                //２行目出力
                                a.契約先_CDT3_1 = cdt3[1].契約先_CDT3_0 == null ? string.Empty : cdt3[1].契約先_CDT3_0;
                                a.保険証番号_CDT3_1 = cdt3[1].保険証番号_CDT3_0 == null ? string.Empty : cdt3[1].保険証番号_CDT3_0;
                                a.s加入年月日_CDT3_1 = cdt3[1].加入年月日_CDT3_0 == null ? string.Empty : cdt3[1].加入年月日_CDT3_0.ToString().Substring(0, 4) + "年" + cdt3[1].加入年月日_CDT3_0.ToString().Substring(5, 2) + "月" + cdt3[1].加入年月日_CDT3_0.ToString().Substring(8, 2) + "日　～";
                                a.s期限_CDT3_1 = cdt3[1].期限_CDT3_0 == null ? string.Empty : cdt3[1].期限_CDT3_0.ToString().Substring(0, 4) + "年" + cdt3[1].期限_CDT3_0.ToString().Substring(5, 2) + "月" + cdt3[1].期限_CDT3_0.ToString().Substring(8, 2) + "日";
                            }
                            if (i == 2)
                            {
                                //３行目出力
                                a.契約先_CDT3_2 = cdt3[2].契約先_CDT3_0 == null ? string.Empty : cdt3[2].契約先_CDT3_0;
                                a.保険証番号_CDT3_2 = cdt3[2].保険証番号_CDT3_0 == null ? string.Empty : cdt3[2].保険証番号_CDT3_0;
                                a.s加入年月日_CDT3_2 = cdt3[2].加入年月日_CDT3_0 == null ? string.Empty : cdt3[2].加入年月日_CDT3_0.ToString().Substring(0, 4) + "年" + cdt3[2].加入年月日_CDT3_0.ToString().Substring(5, 2) + "月" + cdt3[2].加入年月日_CDT3_0.ToString().Substring(8, 2) + "日　～";
                                a.s期限_CDT3_2 = cdt3[2].期限_CDT3_0 == null ? string.Empty : cdt3[2].期限_CDT3_0.ToString().Substring(0, 4) + "年" + cdt3[2].期限_CDT3_0.ToString().Substring(5, 2) + "月" + cdt3[2].期限_CDT3_0.ToString().Substring(8, 2) + "日";
                            }
                            if (i == 3)
                            {
                                //４行目出力
                                a.契約先_CDT3_3 = cdt3[3].契約先_CDT3_0 == null ? string.Empty : cdt3[3].契約先_CDT3_0;
                                a.保険証番号_CDT3_3 = cdt3[3].保険証番号_CDT3_0 == null ? string.Empty : cdt3[3].保険証番号_CDT3_0;
                                a.s加入年月日_CDT3_3 = cdt3[3].加入年月日_CDT3_0 == null ? string.Empty : cdt3[3].加入年月日_CDT3_0.ToString().Substring(0, 4) + "年" + cdt3[3].加入年月日_CDT3_0.ToString().Substring(5, 2) + "月" + cdt3[3].加入年月日_CDT3_0.ToString().Substring(8, 2) + "日　～";
                                a.s期限_CDT3_3 = cdt3[3].期限_CDT3_0 == null ? string.Empty : cdt3[3].期限_CDT3_0.ToString().Substring(0, 4) + "年" + cdt3[3].期限_CDT3_0.ToString().Substring(5, 2) + "月" + cdt3[3].期限_CDT3_0.ToString().Substring(8, 2) + "日";
                            }
                            if (i == 4)
                            {
                                //５行目出力
                                a.契約先_CDT3_4 = cdt3[4].契約先_CDT3_0 == null ? string.Empty : cdt3[4].契約先_CDT3_0;
                                a.保険証番号_CDT3_4 = cdt3[4].保険証番号_CDT3_0 == null ? string.Empty : cdt3[4].保険証番号_CDT3_0;
                                a.s加入年月日_CDT3_4 = cdt3[4].加入年月日_CDT3_0 == null ? string.Empty : cdt3[4].加入年月日_CDT3_0.ToString().Substring(0, 4) + "年" + cdt3[4].加入年月日_CDT3_0.ToString().Substring(5, 2) + "月" + cdt3[4].加入年月日_CDT3_0.ToString().Substring(8, 2) + "日　～";
                                a.s期限_CDT3_4 = cdt3[4].期限_CDT3_0 == null ? string.Empty : cdt3[4].期限_CDT3_0.ToString().Substring(0, 4) + "年" + cdt3[4].期限_CDT3_0.ToString().Substring(5, 2) + "月" + cdt3[4].期限_CDT3_0.ToString().Substring(8, 2) + "日";
                            }
                            if (i == 5)
                            {
                                //６行目出力
                                a.契約先_CDT3_5 = cdt3[5].契約先_CDT3_0 == null ? string.Empty : cdt3[5].契約先_CDT3_0;
                                a.保険証番号_CDT3_5 = cdt3[5].保険証番号_CDT3_0 == null ? string.Empty : cdt3[5].保険証番号_CDT3_0;
                                a.s加入年月日_CDT3_5 = cdt3[5].加入年月日_CDT3_0 == null ? string.Empty : cdt3[5].加入年月日_CDT3_0.ToString().Substring(0, 4) + "年" + cdt3[5].加入年月日_CDT3_0.ToString().Substring(5, 2) + "月" + cdt3[5].加入年月日_CDT3_0.ToString().Substring(8, 2) + "日　～";
                                a.s期限_CDT3_5 = cdt3[5].期限_CDT3_0 == null ? string.Empty : cdt3[5].期限_CDT3_0.ToString().Substring(0, 4) + "年" + cdt3[5].期限_CDT3_0.ToString().Substring(5, 2) + "月" + cdt3[5].期限_CDT3_0.ToString().Substring(8, 2) + "日";
                            }
                            if (i == 6)
                            {
                                //７行目出力
                                a.契約先_CDT3_6 = cdt3[6].契約先_CDT3_0 == null ? string.Empty : cdt3[6].契約先_CDT3_0;
                                a.保険証番号_CDT3_6 = cdt3[6].保険証番号_CDT3_0 == null ? string.Empty : cdt3[6].保険証番号_CDT3_0;
                                a.s加入年月日_CDT3_6 = cdt3[6].加入年月日_CDT3_0 == null ? string.Empty : cdt3[6].加入年月日_CDT3_0.ToString().Substring(0, 4) + "年" + cdt3[6].加入年月日_CDT3_0.ToString().Substring(5, 2) + "月" + cdt3[6].加入年月日_CDT3_0.ToString().Substring(8, 2) + "日　～";
                                a.s期限_CDT3_6 = cdt3[6].期限_CDT3_0 == null ? string.Empty : cdt3[6].期限_CDT3_0.ToString().Substring(0, 4) + "年" + cdt3[6].期限_CDT3_0.ToString().Substring(5, 2) + "月" + cdt3[6].期限_CDT3_0.ToString().Substring(8, 2) + "日";
                            }
                            if (i == 7)
                            {
                                //８行目出力
                                a.契約先_CDT3_7 = cdt3[7].契約先_CDT3_0 == null ? string.Empty : cdt3[7].契約先_CDT3_0;
                                a.保険証番号_CDT3_7 = cdt3[7].保険証番号_CDT3_0 == null ? string.Empty : cdt3[7].保険証番号_CDT3_0;
                                a.s加入年月日_CDT3_7 = cdt3[7].加入年月日_CDT3_0 == null ? string.Empty : cdt3[7].加入年月日_CDT3_0.ToString().Substring(0, 4) + "年" + cdt3[7].加入年月日_CDT3_0.ToString().Substring(5, 2) + "月" + cdt3[7].加入年月日_CDT3_0.ToString().Substring(8, 2) + "日　～";
                                a.s期限_CDT3_7 = cdt3[7].期限_CDT3_0 == null ? string.Empty : cdt3[7].期限_CDT3_0.ToString().Substring(0, 4) + "年" + cdt3[7].期限_CDT3_0.ToString().Substring(5, 2) + "月" + cdt3[7].期限_CDT3_0.ToString().Substring(8, 2) + "日";
                            }
                            if (i == 8)
                            {
                                //９行目出力
                                a.契約先_CDT3_8 = cdt3[8].契約先_CDT3_0 == null ? string.Empty : cdt3[8].契約先_CDT3_0;
                                a.保険証番号_CDT3_8 = cdt3[8].保険証番号_CDT3_0 == null ? string.Empty : cdt3[8].保険証番号_CDT3_0;
                                a.s加入年月日_CDT3_8 = cdt3[8].加入年月日_CDT3_0 == null ? string.Empty : cdt3[8].加入年月日_CDT3_0.ToString().Substring(0, 4) + "年" + cdt3[8].加入年月日_CDT3_0.ToString().Substring(5, 2) + "月" + cdt3[8].加入年月日_CDT3_0.ToString().Substring(8, 2) + "日　～";
                                a.s期限_CDT3_8 = cdt3[8].期限_CDT3_0 == null ? string.Empty : cdt3[8].期限_CDT3_0.ToString().Substring(0, 4) + "年" + cdt3[8].期限_CDT3_0.ToString().Substring(5, 2) + "月" + cdt3[8].期限_CDT3_0.ToString().Substring(8, 2) + "日";
                            }
                            if (i == 9)
                            {
                                //１０行目出力
                                a.契約先_CDT3_9 = cdt3[9].契約先_CDT3_0 == null ? string.Empty : cdt3[9].契約先_CDT3_0;
                                a.保険証番号_CDT3_9 = cdt3[9].保険証番号_CDT3_0 == null ? string.Empty : cdt3[9].保険証番号_CDT3_0;
                                a.s加入年月日_CDT3_9 = cdt3[9].加入年月日_CDT3_0 == null ? string.Empty : cdt3[9].加入年月日_CDT3_0.ToString().Substring(0, 4) + "年" + cdt3[9].加入年月日_CDT3_0.ToString().Substring(5, 2) + "月" + cdt3[9].加入年月日_CDT3_0.ToString().Substring(8, 2) + "日　～";
                                a.s期限_CDT3_9 = cdt3[9].期限_CDT3_0 == null ? string.Empty : cdt3[9].期限_CDT3_0.ToString().Substring(0, 4) + "年" + cdt3[9].期限_CDT3_0.ToString().Substring(5, 2) + "月" + cdt3[9].期限_CDT3_0.ToString().Substring(8, 2) + "日";
                            }
                        }

						
                        //車輌納税データ
                        for (int i = 0; i < cdt4.Count; i++)
                        {
                            if (i == 0)
                            {
                                //１行目出力
                                a.自動車税_0 = cdt4[0].自動車税_0 == null ? 0 : cdt4[0].自動車税_0;
                                a.重量税_0 = cdt4[0].重量税_0 == null ? 0 : cdt4[0].重量税_0;
                                a.s年度_0 = cdt4[0].年度_0 == 0 ? string.Empty : cdt4[0].年度_0.ToString() + "年";
								a.s自動車税年月_0 = cdt4[0].自動車税年月_0 == null ? string.Empty : cdt4[0].自動車税年月_0 == 0 ? string.Empty : (cdt4[0].自動車税年月_0.ToString().Length != 4 ? cdt4[0].自動車税年月_0.ToString().Substring(0, 1) + "月" + cdt4[0].自動車税年月_0.ToString().Substring(1, 2) + "日" : cdt4[0].自動車税年月_0.ToString().Substring(0, 2) + "月" + cdt4[0].自動車税年月_0.ToString().Substring(2, 2) + "日");
								a.s重量税年月_0 = cdt4[0].重量税年月_0 == null ? string.Empty : cdt4[0].重量税年月_0 == 0 ? string.Empty : (cdt4[0].重量税年月_0.ToString().Length != 4 ? cdt4[0].重量税年月_0.ToString().Substring(0, 1) + "月" + cdt4[0].重量税年月_0.ToString().Substring(1, 2) + "日" : cdt4[0].重量税年月_0.ToString().Substring(0, 2) + "月" + cdt4[0].重量税年月_0.ToString().Substring(2, 2) + "日");
                            }
                            if (i == 1)
                            {
                                //２行目出力
                                a.自動車税_1 = cdt4[1].自動車税_0 == null ? 0 : cdt4[1].自動車税_0;
                                a.重量税_1 = cdt4[1].重量税_0 == null ? 0 : cdt4[1].重量税_0;
                                a.s年度_1 = cdt4[1].年度_0 == 0 ? string.Empty : cdt4[1].年度_0.ToString() + "年";
								a.s自動車税年月_1 = cdt4[1].自動車税年月_0 == null ? string.Empty : cdt4[1].自動車税年月_0 == 0 ? string.Empty : (cdt4[1].自動車税年月_0.ToString().Length != 4 ? cdt4[1].自動車税年月_0.ToString().Substring(0, 1) + "月" + cdt4[1].自動車税年月_0.ToString().Substring(1, 2) + "日" : cdt4[1].自動車税年月_0.ToString().Substring(0, 2) + "月" + cdt4[1].自動車税年月_0.ToString().Substring(2, 2) + "日");
								a.s重量税年月_1 = cdt4[1].重量税年月_0 == null ? string.Empty : cdt4[1].重量税年月_0 == 0 ? string.Empty : (cdt4[1].重量税年月_0.ToString().Length != 4 ? cdt4[1].重量税年月_0.ToString().Substring(0, 1) + "月" + cdt4[1].重量税年月_0.ToString().Substring(1, 2) + "日" : cdt4[1].重量税年月_0.ToString().Substring(0, 2) + "月" + cdt4[1].重量税年月_0.ToString().Substring(2, 2) + "日");
                            }
                            if (i == 2)
                            {
                                //３行目出力
                                a.自動車税_2 = cdt4[2].自動車税_0 == null ? 0 : cdt4[2].自動車税_0;
                                a.重量税_2 = cdt4[2].重量税_0 == null ? 0 : cdt4[2].重量税_0;
                                a.s年度_2 = cdt4[2].年度_0 == 0 ? string.Empty : cdt4[2].年度_0.ToString() + "年";
								a.s自動車税年月_2 = cdt4[2].自動車税年月_0 == null ? string.Empty : cdt4[2].自動車税年月_0 == 0 ? string.Empty : (cdt4[2].自動車税年月_0.ToString().Length != 4 ? cdt4[2].自動車税年月_0.ToString().Substring(0, 1) + "月" + cdt4[2].自動車税年月_0.ToString().Substring(1, 2) + "日" : cdt4[2].自動車税年月_0.ToString().Substring(0, 2) + "月" + cdt4[2].自動車税年月_0.ToString().Substring(2, 2) + "日");
								a.s重量税年月_2 = cdt4[2].重量税年月_0 == null ? string.Empty : cdt4[2].重量税年月_0 == 0 ? string.Empty : (cdt4[2].重量税年月_0.ToString().Length != 4 ? cdt4[2].重量税年月_0.ToString().Substring(0, 1) + "月" + cdt4[2].重量税年月_0.ToString().Substring(1, 2) + "日" : cdt4[2].重量税年月_0.ToString().Substring(0, 2) + "月" + cdt4[2].重量税年月_0.ToString().Substring(2, 2) + "日");
                            }
                            if (i == 3)
                            {
                                //４行目出力
                                a.自動車税_3 = cdt4[3].自動車税_0 == null ? 0 : cdt4[3].自動車税_0;
                                a.重量税_3 = cdt4[3].重量税_0 == null ? 0 : cdt4[3].重量税_0;
                                a.s年度_3 = cdt4[3].年度_0 == 0 ? string.Empty : cdt4[3].年度_0.ToString() + "年";
								a.s自動車税年月_3 = cdt4[3].自動車税年月_0 == null ? string.Empty : cdt4[3].自動車税年月_0 == 0 ? string.Empty : (cdt4[3].自動車税年月_0.ToString().Length != 4 ? cdt4[3].自動車税年月_0.ToString().Substring(0, 1) + "月" + cdt4[3].自動車税年月_0.ToString().Substring(1, 2) + "日" : cdt4[3].自動車税年月_0.ToString().Substring(0, 2) + "月" + cdt4[3].自動車税年月_0.ToString().Substring(2, 2) + "日");
								a.s重量税年月_3 = cdt4[3].重量税年月_0 == null ? string.Empty : cdt4[3].重量税年月_0 == 0 ? string.Empty : (cdt4[3].重量税年月_0.ToString().Length != 4 ? cdt4[3].重量税年月_0.ToString().Substring(0, 1) + "月" + cdt4[3].重量税年月_0.ToString().Substring(1, 2) + "日" : cdt4[3].重量税年月_0.ToString().Substring(0, 2) + "月" + cdt4[3].重量税年月_0.ToString().Substring(2, 2) + "日");
                            }
                            if (i == 4)
                            {
                                //５行目出力
                                a.自動車税_4 = cdt4[4].自動車税_0 == null ? 0 : cdt4[4].自動車税_0;
                                a.重量税_4 = cdt4[4].重量税_0 == null ? 0 : cdt4[4].重量税_0;
                                a.s年度_4 = cdt4[4].年度_0 == 0 ? string.Empty : cdt4[4].年度_0.ToString() + "年";
								a.s自動車税年月_4 = cdt4[4].自動車税年月_0 == null ? string.Empty : cdt4[4].自動車税年月_0 == 0 ? string.Empty : (cdt4[4].自動車税年月_0.ToString().Length != 4 ? cdt4[4].自動車税年月_0.ToString().Substring(0, 1) + "月" + cdt4[4].自動車税年月_0.ToString().Substring(1, 2) + "日" : cdt4[4].自動車税年月_0.ToString().Substring(0, 2) + "月" + cdt4[4].自動車税年月_0.ToString().Substring(2, 2) + "日");
								a.s重量税年月_4 = cdt4[4].重量税年月_0 == null ? string.Empty : cdt4[4].重量税年月_0 == 0 ? string.Empty : (cdt4[4].重量税年月_0.ToString().Length != 4 ? cdt4[4].重量税年月_0.ToString().Substring(0, 1) + "月" + cdt4[4].重量税年月_0.ToString().Substring(1, 2) + "日" : cdt4[4].重量税年月_0.ToString().Substring(0, 2) + "月" + cdt4[4].重量税年月_0.ToString().Substring(2, 2) + "日");
                            }
                            if (i == 5)
                            {
                                //６行目出力
                                a.自動車税_5 = cdt4[5].自動車税_0 == null ? 0 : cdt4[5].自動車税_0;
                                a.重量税_5 = cdt4[5].重量税_0 == null ? 0 : cdt4[5].重量税_0;
                                a.s年度_5 = cdt4[5].年度_0 == 0 ? string.Empty : cdt4[5].年度_0.ToString() + "年";
								a.s自動車税年月_5 = cdt4[5].自動車税年月_0 == null ? string.Empty : cdt4[5].自動車税年月_0 == 0 ? string.Empty : (cdt4[5].自動車税年月_0.ToString().Length != 4 ? cdt4[5].自動車税年月_0.ToString().Substring(0, 1) + "月" + cdt4[5].自動車税年月_0.ToString().Substring(1, 2) + "日" : cdt4[5].自動車税年月_0.ToString().Substring(0, 2) + "月" + cdt4[5].自動車税年月_0.ToString().Substring(2, 2) + "日");
								a.s重量税年月_5 = cdt4[5].重量税年月_0 == null ? string.Empty : cdt4[5].重量税年月_0 == 0 ? string.Empty : (cdt4[5].重量税年月_0.ToString().Length != 4 ? cdt4[5].重量税年月_0.ToString().Substring(0, 1) + "月" + cdt4[5].重量税年月_0.ToString().Substring(1, 2) + "日" : cdt4[5].重量税年月_0.ToString().Substring(0, 2) + "月" + cdt4[5].重量税年月_0.ToString().Substring(2, 2) + "日");
                            }
                            if (i == 6)
                            {
                                //７行目出力
                                a.自動車税_6 = cdt4[6].自動車税_0 == null ? 0 : cdt4[6].自動車税_0;
                                a.重量税_6 = cdt4[6].重量税_0 == null ? 0 : cdt4[6].重量税_0;
                                a.s年度_6 = cdt4[6].年度_0 == 0 ? string.Empty : cdt4[6].年度_0.ToString() + "年";
								a.s自動車税年月_6 = cdt4[6].自動車税年月_0 == null ? string.Empty : cdt4[6].自動車税年月_0 == 0 ? string.Empty : (cdt4[6].自動車税年月_0.ToString().Length != 4 ? cdt4[6].自動車税年月_0.ToString().Substring(0, 1) + "月" + cdt4[6].自動車税年月_0.ToString().Substring(1, 2) + "日" : cdt4[6].自動車税年月_0.ToString().Substring(0, 2) + "月" + cdt4[6].自動車税年月_0.ToString().Substring(2, 2) + "日");
								a.s重量税年月_6 = cdt4[6].重量税年月_0 == null ? string.Empty : cdt4[6].重量税年月_0 == 0 ? string.Empty : (cdt4[6].重量税年月_0.ToString().Length != 4 ? cdt4[6].重量税年月_0.ToString().Substring(0, 1) + "月" + cdt4[6].重量税年月_0.ToString().Substring(1, 2) + "日" : cdt4[6].重量税年月_0.ToString().Substring(0, 2) + "月" + cdt4[6].重量税年月_0.ToString().Substring(2, 2) + "日");
                            }
                            if (i == 7)
                            {
                                //８行目出力
                                a.自動車税_7 = cdt4[7].自動車税_0 == null ? 0 : cdt4[7].自動車税_0;
                                a.重量税_7 = cdt4[7].重量税_0 == null ? 0 : cdt4[7].重量税_0;
                                a.s年度_7 = cdt4[7].年度_0 == 0 ? string.Empty : cdt4[7].年度_0.ToString() + "年";
								a.s自動車税年月_7 = cdt4[7].自動車税年月_0 == null ? string.Empty : cdt4[7].自動車税年月_0 == 0 ? string.Empty : (cdt4[7].自動車税年月_0.ToString().Length != 4 ? cdt4[7].自動車税年月_0.ToString().Substring(0, 1) + "月" + cdt4[7].自動車税年月_0.ToString().Substring(1, 2) + "日" : cdt4[7].自動車税年月_0.ToString().Substring(0, 2) + "月" + cdt4[7].自動車税年月_0.ToString().Substring(2, 2) + "日");
								a.s重量税年月_7 = cdt4[7].重量税年月_0 == null ? string.Empty : cdt4[7].重量税年月_0 == 0 ? string.Empty : (cdt4[7].重量税年月_0.ToString().Length != 4 ? cdt4[7].重量税年月_0.ToString().Substring(0, 1) + "月" + cdt4[7].重量税年月_0.ToString().Substring(1, 2) + "日" : cdt4[7].重量税年月_0.ToString().Substring(0, 2) + "月" + cdt4[7].重量税年月_0.ToString().Substring(2, 2) + "日");
                            }
                            if (i == 8)
                            {
                                //９行目出力
                                a.自動車税_8 = cdt4[8].自動車税_0 == null ? 0 : cdt4[8].自動車税_0;
                                a.重量税_8 = cdt4[8].重量税_0 == null ? 0 : cdt4[8].重量税_0;
                                a.s年度_8 = cdt4[8].年度_0 == 0 ? string.Empty : cdt4[8].年度_0.ToString() + "年";
								a.s自動車税年月_8 = cdt4[8].自動車税年月_0 == null ? string.Empty : cdt4[8].自動車税年月_0 == 0 ? string.Empty : (cdt4[8].自動車税年月_0.ToString().Length != 4 ? cdt4[8].自動車税年月_0.ToString().Substring(0, 1) + "月" + cdt4[8].自動車税年月_0.ToString().Substring(1, 2) + "日" : cdt4[8].自動車税年月_0.ToString().Substring(0, 2) + "月" + cdt4[8].自動車税年月_0.ToString().Substring(2, 2) + "日");
								a.s重量税年月_8 = cdt4[8].重量税年月_0 == null ? string.Empty : cdt4[8].重量税年月_0 == 0 ? string.Empty : (cdt4[8].重量税年月_0.ToString().Length != 4 ? cdt4[8].重量税年月_0.ToString().Substring(0, 1) + "月" + cdt4[8].重量税年月_0.ToString().Substring(1, 2) + "日" : cdt4[8].重量税年月_0.ToString().Substring(0, 2) + "月" + cdt4[8].重量税年月_0.ToString().Substring(2, 2) + "日");
                            }
                            if (i == 9)
                            {
                                //１０行目出力
                                a.自動車税_9 = cdt4[9].自動車税_0 == null ? 0 : cdt4[9].自動車税_0;
                                a.重量税_9 = cdt4[9].重量税_0 == null ? 0 : cdt4[9].重量税_0;
                                a.s年度_9 = cdt4[9].年度_0 == 0 ? string.Empty : cdt4[9].年度_0.ToString() + "年";
								a.s自動車税年月_9 = cdt4[9].自動車税年月_0 == null ? string.Empty : cdt4[9].自動車税年月_0 == 0 ? string.Empty : (cdt4[9].自動車税年月_0.ToString().Length != 4 ? cdt4[9].自動車税年月_0.ToString().Substring(0, 1) + "月" + cdt4[9].自動車税年月_0.ToString().Substring(1, 2) + "日" : cdt4[9].自動車税年月_0.ToString().Substring(0, 2) + "月" + cdt4[9].自動車税年月_0.ToString().Substring(2, 2) + "日");
								a.s重量税年月_9 = cdt4[9].重量税年月_0 == null ? string.Empty : cdt4[9].重量税年月_0 == 0 ? string.Empty : (cdt4[9].重量税年月_0.ToString().Length != 4 ? cdt4[9].重量税年月_0.ToString().Substring(0, 1) + "月" + cdt4[9].重量税年月_0.ToString().Substring(1, 2) + "日" : cdt4[9].重量税年月_0.ToString().Substring(0, 2) + "月" + cdt4[9].重量税年月_0.ToString().Substring(2, 2) + "日");
                            }
                        }
                    }
                }
                retList = ret;
                return retList;
            }
        }


      
    }
}
