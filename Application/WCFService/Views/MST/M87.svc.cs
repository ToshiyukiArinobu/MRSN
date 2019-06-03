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
    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コード、svc、および config ファイルで同時にクラス名 "DataDriveLogService" を変更できます。
    // 注意: このサービスをテストするために WCF テスト クライアントを起動するには、ソリューション エクスプローラーで DataDriveLogService.svc または DataDriveLogService.svc.cs を選択し、デバッグを開始してください。
    public class M87 : IM87
    {

        public class M99_COMBOLIST
        {
            public string 名称確認 { get; set; }
        }

        /// <summary>
        /// M87_CNTLのデータ取得
        /// </summary>
        /// <param name="p管理ID">管理ID</param>
        /// <returns>M87_CNTL_Member</returns>
        public List<M87_CNTL_Member> GetData(int? p管理ID, int pOptiion)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m87 in context.M87_CNTL
                           select new M87_CNTL_Member
                           {
                               管理ID = m87.管理ID,
                               登録日時 = m87.登録日時,
                               更新日時 = m87.更新日時,
                               得意先管理処理年月 = m87.得意先管理処理年月,
                               支払先管理処理年月 = m87.支払先管理処理年月,
                               車輌管理処理年月 = m87.車輌管理処理年月,
                               運転者管理処理年月 = m87.運転者管理処理年月,
                               更新年月 = m87.更新年月,
                               決算月 = m87.決算月,
                               得意先自社締日 = m87.得意先自社締日,
                               支払先自社締日 = m87.支払先自社締日,
                               運転者自社締日 = m87.運転者自社締日,
                               車輌自社締日 = m87.車輌自社締日,
                               自社支払日 = m87.自社支払日,
                               自社サイト = m87.自社サイト,
                               未定区分 = m87.未定区分,
                               部門管理区分 = m87.部門管理区分,
                               割増料金名１ = m87.割増料金名１,
                               割増料金名２ = m87.割増料金名２,
                               確認名称 = m87.確認名称,
                               得意先ID区分 = m87.得意先ID区分,
                               支払先ID区分 = m87.支払先ID区分,
                               乗務員ID区分 = m87.乗務員ID区分,
                               車輌ID区分 = m87.車輌ID区分,
                               車種ID区分 = m87.車種ID区分,
                               発着地ID区分 = m87.発着地ID区分,
                               品名ID区分 = m87.品名ID区分,
                               摘要ID区分 = m87.摘要ID区分,
                               期首年月 = m87.期首年月,
                               売上消費税端数区分 = m87.売上消費税端数区分,
                               支払消費税端数区分 = m87.支払消費税端数区分,
                               金額計算端数区分 = m87.金額計算端数区分,
                               出力プリンター設定 = m87.出力プリンター設定,
                               自動学習区分 = m87.自動学習区分,
                               月次集計区分 = m87.月次集計区分,
                               距離転送区分 = m87.距離転送区分,
                               番号通知区分 = m87.番号通知区分,
                               通行料転送区分 = m87.通行料転送区分,
                               路線計算区分 = m87.路線計算区分,
                               Ｇ期首月日 = m87.Ｇ期首月日,
                               Ｇ期末月日 =m87.Ｇ期末月日,
                               請求書区分 = m87.請求書区分,
                               削除日付 = m87.削除日付

                           }).AsQueryable();

                    if (pOptiion == 0)
                    {
                        if (-1 != p管理ID)
                        {
                            ret = ret.Where(c => c.管理ID == p管理ID);
                        }
                    }


                return ret.ToList();
            }
        }

        /// <summary>
        /// M87_CNTLの更新
        /// </summary>
        /// <param name="m87tik">M87_CNTL_Member</param>
        public void Update(int p管理ID,
            int? p得意先管理処理年月,
            int? p支払先管理処理年月,
            int? p車輌管理処理年月,
            int? p運転者管理処理年月,
            int? p更新年月,
            int? p決算月,
            int? p得意先自社締日,
            int? p支払先自社締日,
            int? p運転者自社締日,
            int? p車輌自社締日,
            int? p自社支払日,
            int? p自社サイト,
            int? p未定区分,
            int? p部門管理区分,
            string p割増料金名１,
            string p割増料金名２,
            string p確認名称,
            int? p得意先ID区分,
            int? p支払先ID区分,
            int? p乗務員ID区分,
            int? p車輌ID区分,
            int? p車種ID区分,
            int? p発着地ID区分,
            int? p品名ID区分,
            int? p摘要ID区分,
            int? p期首年月,
            int? p売上消費税端数区分,
            int? p支払消費税端数区分,
            int? p金額計算端数区分,
            int? p出力プリンター設定,
            int? p自動学習区分,
            int? p月次集計区分,
            int? p距離転送区分,
            int? p番号通知区分,
            int? p通行料転送区分,
            int? p路線計算区分,
            int? pＧ期首月日,
            int? pＧ期末月日,
            int? p請求書区分
            )
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //更新行を特定
                var ret = from x in context.M87_CNTL
                          where (x.管理ID == p管理ID)
                          orderby x.管理ID
                          select x;


                var m87 = ret.FirstOrDefault();

                if (m87 == null)
                {
                    M87_CNTL m87in = new M87_CNTL();
                    m87in.管理ID = p管理ID;
                    m87in.登録日時 = DateTime.Now;
                    m87in.更新日時 = DateTime.Now;

                    m87in.得意先管理処理年月 = p得意先管理処理年月;
                    m87in.支払先管理処理年月 = p支払先管理処理年月;
                    m87in.車輌管理処理年月 = p車輌管理処理年月;
                    m87in.運転者管理処理年月 = p運転者管理処理年月;
                    m87in.更新年月 = p更新年月;
                    m87in.決算月 = p決算月;
                    m87in.得意先自社締日 = p得意先自社締日;
                    m87in.支払先自社締日 = p支払先自社締日;
                    m87in.運転者自社締日 = p運転者自社締日;
                    m87in.車輌自社締日 = p車輌自社締日;
                    m87in.自社支払日 = p自社支払日;
                    m87in.自社サイト = p自社サイト;
                    m87in.未定区分 = p未定区分;
                    m87in.部門管理区分 = p部門管理区分;
                    m87in.割増料金名１ = p割増料金名１;
                    m87in.割増料金名２ = p割増料金名２;
                    m87in.確認名称 = p確認名称 == "" ? "確認" : p確認名称;
                    m87in.得意先ID区分 = p得意先ID区分;
                    m87in.支払先ID区分 = p支払先ID区分;
                    m87in.乗務員ID区分 = p乗務員ID区分;
                    m87in.車輌ID区分 = p車輌ID区分;
                    m87in.車種ID区分 = p車種ID区分;
                    m87in.発着地ID区分 = p発着地ID区分;
                    m87in.品名ID区分 = p品名ID区分;
                    m87in.摘要ID区分 = p摘要ID区分;
                    m87in.期首年月 = p期首年月;
                    m87in.売上消費税端数区分 = p売上消費税端数区分;
                    m87in.支払消費税端数区分 = p支払消費税端数区分;
                    m87in.金額計算端数区分 = p金額計算端数区分;
                    m87in.出力プリンター設定 = p出力プリンター設定;
                    m87in.自動学習区分 = p自動学習区分;
                    m87in.月次集計区分 = p月次集計区分;
                    m87in.距離転送区分 = p距離転送区分;
                    m87in.番号通知区分 = p番号通知区分;
                    m87in.通行料転送区分 = p通行料転送区分;
                    m87in.路線計算区分 = p路線計算区分;
                    m87in.Ｇ期首月日 = pＧ期首月日;
                    m87in.Ｇ期末月日 = pＧ期末月日;
                    m87in.請求書区分 = p請求書区分;
                    m87in.削除日付 = null;
                    context.M87_CNTL.ApplyChanges(m87in);
                }
                else
                {

                    m87.更新日時 = DateTime.Now;
                    m87.得意先管理処理年月 = p得意先管理処理年月;
                    m87.支払先管理処理年月 = p支払先管理処理年月;
                    m87.車輌管理処理年月 = p車輌管理処理年月;
                    m87.運転者管理処理年月 = p運転者管理処理年月;
                    m87.更新年月 = p更新年月;
                    m87.決算月 = p決算月;
                    m87.得意先自社締日 = p得意先自社締日;
                    m87.支払先自社締日 = p支払先自社締日;
                    m87.運転者自社締日 = p運転者自社締日;
                    m87.車輌自社締日 = p車輌自社締日;
                    m87.自社支払日 = p自社支払日;
                    m87.自社サイト = p自社サイト;
                    m87.未定区分 = p未定区分;
                    m87.部門管理区分 = p部門管理区分;
                    m87.割増料金名１ = p割増料金名１;
                    m87.割増料金名２ = p割増料金名２;
                    m87.確認名称 = p確認名称 == "" ? "確認" : p確認名称;
                    m87.得意先ID区分 = p得意先ID区分;
                    m87.支払先ID区分 = p支払先ID区分;
                    m87.乗務員ID区分 = p乗務員ID区分;
                    m87.車輌ID区分 = p車輌ID区分;
                    m87.車種ID区分 = p車種ID区分;
                    m87.発着地ID区分 = p発着地ID区分;
                    m87.品名ID区分 = p品名ID区分;
                    m87.摘要ID区分 = p摘要ID区分;
                    m87.期首年月 = p期首年月;
                    m87.売上消費税端数区分 = p売上消費税端数区分;
                    m87.支払消費税端数区分 = p支払消費税端数区分;
                    m87.金額計算端数区分 = p金額計算端数区分;
                    m87.出力プリンター設定 = p出力プリンター設定;
                    m87.部門管理区分 = p部門管理区分;
                    m87.自動学習区分 = p自動学習区分;
                    m87.月次集計区分 = p月次集計区分;
                    m87.距離転送区分 = p距離転送区分;
                    m87.番号通知区分 = p番号通知区分;
                    m87.通行料転送区分 = p通行料転送区分;
                    m87.路線計算区分 = p路線計算区分;
                    m87.Ｇ期首月日 = pＧ期首月日;
                    m87.Ｇ期末月日 = pＧ期末月日;
                    m87.請求書区分 = p請求書区分;
                    m87.削除日付 = null;
                    m87.AcceptChanges();
                }

                context.SaveChanges();

                var ret2 = from m99 in context.M99_COMBOLIST
                           where m99.分類 == "マスタ" && m99.機能 == "基礎情報設定" && m99.カテゴリ == "確認名称" && m99.コード == 0 && m99.表示順 == 1
                           select m99;
                var m99_1 = ret2.FirstOrDefault();

                m99_1.表示名 = p確認名称 == "" ? "未確認" : "未" + p確認名称;
                context.SaveChanges();

                var ret3 =  from m99 in context.M99_COMBOLIST
                            where m99.分類 == "マスタ" && m99.機能 == "基礎情報設定" && m99.カテゴリ == "確認名称" && m99.コード == 1 && m99.表示順 == 2
                            select m99;
                var m99_2 = ret3.FirstOrDefault();

                m99_2.表示名 = p確認名称 == "" ? "確認" : p確認名称;
                context.SaveChanges();

                var ret4 = from m99 in context.M99_COMBOLIST
                           where m99.分類 == "日次" && m99.機能 == "売上明細問合せ" && m99.カテゴリ == "確認名称区分" && m99.コード == 1 && m99.表示順 == 2
                           select m99;
                var m99_3 = ret4.FirstOrDefault();

                m99_3.表示名 = p確認名称 == "" ? "確認" : p確認名称 + "のみ";
                context.SaveChanges();

                var ret5 = from m99 in context.M99_COMBOLIST
                           where m99.分類 == "日次" && m99.機能 == "売上明細問合せ" && m99.カテゴリ == "確認名称区分" && m99.コード == 2 && m99.表示順 == 3
                           select m99;
                var m99_4 = ret5.FirstOrDefault();

                m99_4.表示名 = p確認名称 == "" ? "未確認" : "未" + p確認名称 + "のみ";
                context.SaveChanges();
            }
        }

    }
}
