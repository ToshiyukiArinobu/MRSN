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
    using Const = CommonConstants;

    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コード、svc、および config ファイルで同時にクラス名 "DataDriveLogService" を変更できます。
    // 注意: このサービスをテストするために WCF テスト クライアントを起動するには、ソリューション エクスプローラーで DataDriveLogService.svc または DataDriveLogService.svc.cs を選択し、デバッグを開始してください。
    public class M88 {

        private List<Const.明細番号ID> 明細番号ID_List =
            new List<Const.明細番号ID> {
                Const.明細番号ID.ID01_売上_仕入_移動,
                Const.明細番号ID.ID02_揚り,
                Const.明細番号ID.ID05_入金,
                Const.明細番号ID.ID06_出金
            };

        /// <summary>
        /// M88_SEQのデータ取得
        /// </summary>
        /// <param name="p明細番号ID">明細番号ID</param>
        /// <returns></returns>
        public M88_SEQ GetData(int p明細番号ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = context.M88_SEQ
                                .Where(w => w.明細番号ID == p明細番号ID)
                                .OrderBy(o => o.現在明細番号);

                return ret.FirstOrDefault();

            }

        }

        /// <summary>
        /// M88_SEQの新規追加
        /// </summary>
        /// <param name="m88seq">M88_SEQ_Member</param>
        public void Insert(M88_SEQ m88seq)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                M88_SEQ m88 = setCopyEntityData(m88seq);

                try
                {
                    context.M88_SEQ.ApplyChanges(m88);
                    context.SaveChanges();
 
                }
                catch (UpdateException ex)
                {
                    // PKey違反等
                    Console.WriteLine(ex);
                }

            }

        }

        /// <summary>
        /// M88_SEQの更新
        /// </summary>
        /// <param name="m88seq">M88_SEQ_Member</param>
        public void Update(M88_SEQ m88seq)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var m88 = context.M88_SEQ
                                .Where(w => w.明細番号ID == m88seq.明細番号ID)
                                .FirstOrDefault();

                m88.最終更新日時 = DateTime.Now;

                m88.AcceptChanges();
                context.SaveChanges();

            }

        }

        /// <summary>
        /// M88_SEQの物理削除
        /// </summary>
        /// <param name="m88seq">M88_SEQ</param>
        public void Delete(M88_SEQ m88seq)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var m88 = context.M88_SEQ
                                .Where(w => w.明細番号ID == m88seq.明細番号ID)
                                .FirstOrDefault();

                context.DeleteObject(m88);
                context.SaveChanges();

            }

        }

        /// <summary>
        /// 新規伝票番号を取得する
        /// </summary>
        /// <param name="slipNumber">明細番号ID</param>
        /// <param name="userId">ログインユーザID</param>
        /// <returns></returns>
        public int getNextNumber(Const.明細番号ID slipNumber, int userId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = context.M88_SEQ
                                .Where(w => w.明細番号ID == (int)slipNumber);

                int num = 0;
                // 存在しない場合は作成した上で再取得する
                if (ret.Count() == 0)
                {
                    createData(userId);
                    num = getNextNumber(slipNumber, userId);

                }
                else
                {
                    // 新規番号取得
                    M88_SEQ m88 = ret.First();
                    num = m88.現在明細番号 + 1;

                    // 現在明細番号をカウントアップして更新
                    m88.現在明細番号++;
                    m88.最終更新者 = userId;
                    m88.最終更新日時 = DateTime.Now;

                    m88.AcceptChanges();
                    context.SaveChanges();

                }

                return num;

            }

        }

        /// <summary>
        /// 伝票番号を次の番号へ更新する
        /// </summary>
        /// <param name="slipNumber"></param>
        public void setNextNumber(Const.明細番号ID slipNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 指定情報を取得
                var ret = context.M88_SEQ
                                .Where(w => w.明細番号ID == (int)slipNumber)
                                .SingleOrDefault();

                ret.現在明細番号 = ret.現在明細番号 + 1;
                ret.最終更新日時 = DateTime.Now;
                
                ret.AcceptChanges();
                context.SaveChanges();

            }

        }

        /// <summary>
        /// 各明細番号IDの伝票番号情報を作成する
        /// </summary>
        /// <param name="userId">ログインユーザID</param>
        public void createData(int userId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                foreach (Const.明細番号ID id in 明細番号ID_List)
                {
                    // 対象の明細番号ID
                    int dtlId = id.GetHashCode();

                    // 登録済みか確認
                    var ret = context.M88_SEQ
                                    .Where(w => w.明細番号ID == dtlId)
                                    .SingleOrDefault();

                    // 登録済みの場合はスキップ
                    if (ret != null)
                        continue;

                    M88_SEQ seq = new M88_SEQ();
                    seq.明細番号ID = dtlId;
                    seq.現在明細番号 = 0;
                    seq.最大明細番号 = 9999999;
                    seq.登録者 = userId;
                    seq.登録日時 = DateTime.Now;
                    seq.最終更新者 = userId;
                    seq.最終更新日時 = DateTime.Now;

                    context.M88_SEQ.ApplyChanges(seq);

                }

                context.SaveChanges();

            }

        }

        /// <summary>
        /// データ内容を新規インスタンスにコピーする
        /// </summary>
        /// <param name="old"></param>
        /// <returns></returns>
        private M88_SEQ setCopyEntityData(M88_SEQ old)
        {
            M88_SEQ seq = new M88_SEQ();

            seq.明細番号ID = old.明細番号ID;
            seq.現在明細番号 = old.現在明細番号;
            seq.最大明細番号 = old.最大明細番号;
            seq.登録日時 = old.登録日時;
            seq.最終更新日時 = old.最終更新日時;
            seq.削除日時 = old.削除日時;

            return seq;

        }

    }

}
