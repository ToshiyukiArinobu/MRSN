using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 入出庫履歴サービスクラス
    /// </summary>
    public class S04 : BaseService
    {
        #region << 列挙型定義 >>

        public enum 機能ID : int
        {
            仕入入力 = 1,
            揚り入力 = 2,
            売上入力 = 3,
            商品移動入力 = 4,
            振替入力 = 5,
            棚卸更新 = 6,
            仕入返品 = 91,
            売上返品 = 93
        }

        #endregion

        #region << 定数定義 >>

        // === 条件設定をDictionary定義とする為、共通定義を作成 === /

        /// <summary>列名定義 SEQ</summary>
        public static string COLUMNS_NAME_SEQ = "SEQ";
        /// <summary>列名定義 入出庫日</summary>
        public static string COLUMNS_NAME_入出庫日 = "入出庫日";
        /// <summary>列名定義 入出庫時刻K</summary>
        public static string COLUMNS_NAME_入出庫時刻 = "入出庫時刻";
        /// <summary>列名定義 倉庫コード</summary>
        public static string COLUMNS_NAME_倉庫コード = "倉庫コード";
        /// <summary>列名定義 入出庫区分</summary>
        public static string COLUMNS_NAME_入出庫区分 = "入出庫区分";
        /// <summary>列名定義 品番コード</summary>
        public static string COLUMNS_NAME_品番コード = "品番コード";
        /// <summary>列名定義 賞味期限</summary>
        public static string COLUMNS_NAME_賞味期限 = "賞味期限";
        /// <summary>列名定義 数量</summary>
        public static string COLUMNS_NAME_数量 = "数量";
        /// <summary>列名定義 伝票番号</summary>
        public static string COLUMNS_NAME_伝票番号 = "伝票番号";

        #endregion

        #region << クラス変数定義 >>

        /// <summary>
        /// エンティティコネクション
        /// </summary>
        /// <remarks>
        /// 他のサービスから呼び出される想定なので
        /// トランザクションの同一化を想定して定義
        /// </remarks>
        private TRAC3Entities _context;

        /// <summary>
        /// ログインユーザＩＤ
        /// </summary>
        private int _loginUserId;

        /// <summary>
        /// 機能ＩＤ
        /// （1:仕入入力、2:揚り入力、3:売上入力、4:商品移動・振替入力、91:仕入返品、93:売上返品）
        /// </summary>
        private int _functionId;

        Common com = new Common();

        #endregion

        #region コンストラクタ
        /// <summary>
        /// 入出庫履歴コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        public S04(TRAC3Entities context, int userId, 機能ID funcId)
        {
            _context = context;
            _loginUserId = userId;
            _functionId = (int)funcId;

        }
        #endregion

        #region 入出庫履歴作成
        /// <summary>
        /// 入出庫履歴の作成をおこなう
        /// </summary>
        public void CreateProductHistory(S04_HISTORY history)
        {
            history.作成機能ID = _functionId;
            history.登録者 = _loginUserId;
            history.登録日時 = com.GetDbDateTime();
            history.最終更新者 = _loginUserId;
            history.最終更新日時 = com.GetDbDateTime();

            _context.S04_HISTORY.ApplyChanges(history);

        }
        #endregion

        #region 入出庫履歴更新
        /// <summary>
        /// 入出庫履歴の更新をおこなう
        /// </summary>
        /// <param name="history"></param>
        /// <param name="conditionDic"></param>
        public void UpdateProductHistory(S04_HISTORY history, Dictionary<string, string> conditionDic)
        {
            // パラメータを展開
            long? seq = null;       // シーケンス
            DateTime? date = null,  // 入出庫日
                expiration = null;  // 賞味期限
            TimeSpan? time = null;  // 入出庫時刻
            int? souk = null,       // 倉庫コード
                kbn = null,         // 入出庫区分
                code = null,        // 品番コード
                qty = null,         // 数量
                denNo = null;       // 伝票番号

            getRequestParams(conditionDic, ref seq, ref date, ref time, ref souk, ref kbn, ref code, ref expiration, ref qty, ref denNo);

            // 更新対象データを取得
            var tmp =
                _context.S04_HISTORY.Where(w => w.削除日時 == null && w.作成機能ID == _functionId);

            #region データ絞込み
            if (seq != null)
                tmp = tmp.Where(w => w.SEQ == seq);

            if (date != null)
                tmp = tmp.Where(w => w.入出庫日 == date);

            if (time != null)
                tmp = tmp.Where(w => w.入出庫時刻 == time);

            if (souk != null)
                tmp = tmp.Where(w => w.倉庫コード == souk);

            if (kbn != null)
                tmp = tmp.Where(w => w.入出庫区分 == kbn);

            if (code != null)
                tmp = tmp.Where(w => w.品番コード == code);

            if (expiration != null)
                tmp = tmp.Where(w => w.賞味期限 == expiration);

            if (qty != null)
                tmp = tmp.Where(w => w.数量 == qty);

            if (denNo != null)
                tmp = tmp.Where(w => w.伝票番号 == denNo);
            #endregion

            // 対象データの更新を実施
            var hst = tmp.FirstOrDefault();

            if (hst != null)
            {
                hst.入出庫日 = history.入出庫日;        // No.156-1 Add
                hst.入出庫時刻 = history.入出庫時刻;    // No.156-1 Add
                hst.倉庫コード = history.倉庫コード;
                hst.入出庫区分 = history.入出庫区分;
                hst.賞味期限 = history.賞味期限;
                hst.数量 = history.数量;
                hst.最終更新者 = _loginUserId;
                hst.最終更新日時 = com.GetDbDateTime();

                hst.AcceptChanges();

            }

        }
        #endregion

        #region 入出庫履歴(論理)削除
        /// <summary>
        /// 入出庫履歴の(論理)削除をおこなう
        /// </summary>
        /// <param name="conditionDic"></param>
        public void DeleteProductHistory(Dictionary<string, string> conditionDic)
        {
            // パラメータを展開
            long? seq = null;       // シーケンス
            DateTime? date = null,  // 入出庫日
                expiration = null;  // 賞味期限
            TimeSpan? time = null;  // 入出庫時刻
            int? souk = null,       // 倉庫コード
                kbn = null,         // 入出庫区分
                code = null,        // 品番コード
                qty = null,         // 数量
                denNo = null;       // 伝票番号

            getRequestParams(conditionDic, ref seq, ref date, ref time, ref souk, ref kbn, ref code, ref expiration, ref qty, ref denNo);

            // 更新対象データを取得
            var tmp =
                _context.S04_HISTORY.Where(w => w.削除日時 == null);

            #region データ絞込み
            if (seq != null)
                tmp = tmp.Where(w => w.SEQ == seq);

            if (date != null)
                tmp = tmp.Where(w => w.入出庫日 == date);

            if (time != null)
                tmp = tmp.Where(w => w.入出庫時刻 == time);

            if (souk != null)
                tmp = tmp.Where(w => w.倉庫コード == souk);

            if (kbn != null)
                tmp = tmp.Where(w => w.入出庫区分 == kbn);

            if (code != null)
                tmp = tmp.Where(w => w.品番コード == code);

            if (expiration != null)
                tmp = tmp.Where(w => w.賞味期限 == expiration);

            if (qty != null)
                tmp = tmp.Where(w => w.数量 == qty);

            if (denNo != null)
                tmp = tmp.Where(w => w.伝票番号 == denNo);
            #endregion

            foreach (var data in tmp)
            {
                data.削除者 = _loginUserId;
                data.削除日時 = com.GetDbDateTime();

                data.AcceptChanges();

            }

        }
        #endregion

        // No-258 Add Start
        /// <summary>
        /// 入出庫履歴(物理)削除
        /// </summary>
        /// <param name="context"></param>
        /// <param name="slipNumber"></param>
        /// <param name="CreateId"></param>
        /// <returns></returns>
        public void PhysicalDeletionProductHistory(TRAC3Entities context, int slipNumber, int CreateId)
        {
            // 登録済みデータを物理削除
            var delData = context.S04_HISTORY.Where(w => w.伝票番号 == slipNumber
                                                    && w.作成機能ID == CreateId)
                                                    .ToList();
            if (delData != null)
            {
                foreach (S04_HISTORY dtl in delData)
                    context.S04_HISTORY.DeleteObject(dtl);

                context.SaveChanges();

            }
        }
        // No-258 Add End

        #region 検索条件パラメータ展開
        /// <summary>
        /// 検索条件パラメータを展開する
        /// </summary>
        /// <param name="paramDic">パラメータDIC</param>
        /// <param name="seq">シーケンス</param>
        /// <param name="date">入出庫日</param>
        /// <param name="time">入出庫時刻</param>
        /// <param name="souk">倉庫コード</param>
        /// <param name="kbn">入出庫区分</param>
        /// <param name="code">品番コード</param>
        /// <param name="expiration">賞味期限</param>
        /// <param name="qty">数量</param>
        /// <param name="denNo">伝票番号</param>
        private void getRequestParams(Dictionary<string,string> paramDic,
            ref long? seq, ref DateTime? date, ref TimeSpan? time, ref int? souk, ref int? kbn, ref int? code, ref DateTime? expiration, ref int? qty, ref int? denNo)
        {
            // 型変換用変数
            long lval;
            int ival;
            DateTime dval;
            TimeSpan tval;

            // SEQ(bigint identity)
            if (paramDic.ContainsKey(COLUMNS_NAME_SEQ))
                seq = long.TryParse(paramDic[COLUMNS_NAME_SEQ], out lval) ? (long?)lval : null;

            // 入出庫日(date)
            if (paramDic.ContainsKey(COLUMNS_NAME_入出庫日))
                date = DateTime.TryParse(paramDic[COLUMNS_NAME_入出庫日], out dval) ? dval : (DateTime?)null;

            // 入出庫時刻(time)
            if (paramDic.ContainsKey(COLUMNS_NAME_入出庫時刻))
                time = TimeSpan.TryParse(paramDic[COLUMNS_NAME_入出庫時刻], out tval) ? tval : (TimeSpan?)null;

            // 倉庫コード(int)
            if (paramDic.ContainsKey(COLUMNS_NAME_倉庫コード))
                souk = int.TryParse(paramDic[COLUMNS_NAME_倉庫コード], out ival) ? ival : (int?)null;

            // 入出庫区分(int)
            if (paramDic.ContainsKey(COLUMNS_NAME_入出庫区分))
                kbn = int.TryParse(paramDic[COLUMNS_NAME_入出庫区分], out ival) ? ival : (int?)null;

            // 品番コード(int)
            if (paramDic.ContainsKey(COLUMNS_NAME_品番コード))
                code = int.TryParse(paramDic[COLUMNS_NAME_品番コード], out ival) ? ival : (int?)null;

            // 賞味期限(date)
            if (paramDic.ContainsKey(COLUMNS_NAME_賞味期限))
                expiration = DateTime.TryParse(paramDic[COLUMNS_NAME_賞味期限], out dval) ? dval : (DateTime?)null;

            // 数量(int)
            if (paramDic.ContainsKey(COLUMNS_NAME_数量))
                qty = int.TryParse(paramDic[COLUMNS_NAME_数量], out ival) ? ival : (int?)null;

            // 伝票番号(int)
            if (paramDic.ContainsKey(COLUMNS_NAME_伝票番号))
                denNo = int.TryParse(paramDic[COLUMNS_NAME_伝票番号], out ival) ? ival : (int?)null;

        }
        #endregion

        #region 入出庫区分取得
        /// <summary>
        /// 数量変動状態から入出庫区分を判定して返す
        /// </summary>
        /// <param name="funcId"></param>
        /// <param name="row"></param>
        /// <param name="colName"></param>
        /// <param name="afterQty"></param>
        /// <returns></returns>
        public CommonConstants.入出庫区分 getInboundType(DataRow row, string colName, decimal afterQty)
        {
            decimal origin = 0;
            if (row.HasVersion(DataRowVersion.Original))
            {
                // オリジナルが存在する場合はその数量を優先する
                origin = ParseNumeric<decimal>(row[colName, DataRowVersion.Original]);
            }
            else
                origin = afterQty;

            decimal qty = origin - afterQty;

            CommonConstants.作成機能ID func = (CommonConstants.作成機能ID)_functionId;
            switch (func)
            {
                case CommonConstants.作成機能ID.ID01_仕入:
                case CommonConstants.作成機能ID.ID93_売上返品:
                    return qty >= 0 ? CommonConstants.入出庫区分.ID01_入庫 : CommonConstants.入出庫区分.ID02_出庫;

                case CommonConstants.作成機能ID.ID03_売上:
                case CommonConstants.作成機能ID.ID91_仕入返品:
                    return qty >= 0 ? CommonConstants.入出庫区分.ID02_出庫 : CommonConstants.入出庫区分.ID01_入庫;

                default:
                //case CommonConstants.作成機能ID.ID02_揚り:
                //case CommonConstants.作成機能ID.ID04_移動:
                    return qty >= 0 ? CommonConstants.入出庫区分.ID01_入庫 : CommonConstants.入出庫区分.ID02_出庫;

            }

        }
        #endregion

    }

}