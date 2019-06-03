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
    public class M89 : IM89 {

        /// <summary>
        /// M89_KENALLのデータ取得
        /// </summary>
        /// <param name="p明細番号ID">明細番号ID</param>
        /// <returns>M89_KENALL_Member</returns>
		public M89_KENALL_Member GetData(string p明細番号ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

				////// ★ 明細番号ID という項目がなくなっている。対処が必要。
                var ret = (from m89 in context.M89_KENALL
                           where m89.郵便番号 == p明細番号ID
						   orderby m89.郵便番号
						   select new M89_KENALL_Member
                           {
							   
							   //明細番号ID = m89.現在明細番号,
							   //登録日時 = m89.登録日時,
							   //更新日時 = m89.更新日時,
							   //現在明細番号 = m89.現在明細番号,
							   //最大明細番号 = m89.最大明細番号,
                           });
                return ret.FirstOrDefault();
            }
        }


    }
}
