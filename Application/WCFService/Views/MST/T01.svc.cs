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
    public class T01 : IT01
    {



        /// <summary>
        /// 乗務員運行表ストアド実行＆リスト取得
        /// </summary>
        /// <param name="p運行日付">運行日付</param>
        /// <param name="p開始乗務員ID">開始乗務員ID</param>
        /// <param name="p終了乗務員ID">終了乗務員ID</param>
        /// <param name="p開始車種ID">開始車種ID</param>
        /// <param name="p終了車種ID">終了車種ID</param>
        /// <param name="p開始車輌ID">開始車輌ID</param>
        /// <param name="p終了車輌ID">終了車輌ID</param>
        /// <param name="p乗務員ピックアップ">乗務員ピックアップ</param>
        /// <param name="p車種ピックアップ">車種ピックアップ</param>
        /// <param name="p車輌ピックアップ">車輌ピックアップ</param>
        /// <returns>W_DLY16010_Memberのリスト</returns>
        public List<W_DLY16010_Member> RunStoredDLY16010(string p運行日付, int? p開始乗務員ID, int? p終了乗務員ID, int? p開始車種ID, int? p終了車種ID,
            int? p開始車輌ID, int? p終了車輌ID, string p乗務員ピックアップ, string p車種ピックアップ, string p車輌ピックアップ)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                using (DbTransaction transaction = context.Connection.BeginTransaction())
                {
                    int result = context.DLY16010(p運行日付, p開始乗務員ID, p終了乗務員ID, p開始車種ID, p終了車種ID,
                                        p開始車輌ID, p終了車輌ID, p乗務員ピックアップ, p車種ピックアップ, p車輌ピックアップ);

                    if (result != 0)
                    {
                        transaction.Commit();
                        return GetListDLY16010();
                    }
                    else
                    {
                        transaction.Rollback();
                        return null;
                    }
                }
            }

        }

        /// <summary>
        /// RunStoredDLY16010で作成されたワークを取得
        /// </summary>
        /// <returns>W_DLY16010_Memberのリスト</returns>
        private List<W_DLY16010_Member> GetListDLY16010()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

				var ret = (from x in context.W_DLY16010
						   select new W_DLY16010_Member
						   {
							   H_運行日付 = x.H_運行日付,
							   D_乗務員ID = x.D_乗務員ID,
							   D_乗務員名 = x.D_乗務員名,
							   D_車種ID = x.D_車種ID,
							   D_車種名 = x.D_車種名,
							   D_車輌ID = x.D_車輌ID,
							   D_車輌番号 = x.D_車輌番号,
							   D_得意先ID1 = x.D_得意先ID1,
							   D_得意先名1 = x.D_得意先名1,
							   D_商品名1 = x.D_商品名1,
							   D_発地名1 = x.D_発地名1,
							   D_着地名1 = x.D_着地名1,
							   D_数量1 = x.D_数量1,
							   D_重量1 = x.D_重量1,
							   D_配送時間1 = x.D_配送時間1,
							   D_得意先ID2 = x.D_得意先ID2,
							   D_得意先名2 = x.D_得意先名2,
							   D_商品名2 = x.D_商品名2,
							   D_発地名2 = x.D_発地名2,
							   D_着地名2 = x.D_着地名2,
							   D_数量2 = x.D_数量2,
							   D_重量2 = x.D_重量2,
							   D_配送時間2 = x.D_配送時間2,
							   D_得意先ID3 = x.D_得意先ID3,
							   D_得意先名3 = x.D_得意先名3,
							   D_商品名3 = x.D_商品名3,
							   D_発地名3 = x.D_発地名3,
							   D_着地名3 = x.D_着地名3,
							   D_数量3 = x.D_数量3,
							   D_重量3 = x.D_重量3,
							   D_配送時間3 = x.D_配送時間3,
							   D_得意先ID4 = x.D_得意先ID4,
							   D_得意先名4 = x.D_得意先名4,
							   D_商品名4 = x.D_商品名4,
							   D_発地名4 = x.D_発地名4,
							   D_着地名4 = x.D_着地名4,
							   D_数量4 = x.D_数量4,
							   D_重量4 = x.D_重量4,
							   D_配送時間4 = x.D_配送時間4,
							   D_得意先ID5 = x.D_得意先ID5,
							   D_得意先名5 = x.D_得意先名5,
							   D_商品名5 = x.D_商品名5,
							   D_発地名5 = x.D_発地名5,
							   D_着地名5 = x.D_着地名5,
							   D_数量5 = x.D_数量5,
							   D_重量5 = x.D_重量5,
							   D_配送時間5 = x.D_配送時間5,
						   }).ToList();

                return ret;
            }
        }

    }
}
