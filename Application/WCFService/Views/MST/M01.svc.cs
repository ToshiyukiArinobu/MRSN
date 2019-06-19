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
    public class M01
    {
        /// <summary>
        /// 取引先情報取得
        /// </summary>
        /// <param name="code">取引先コード</param>
        /// <param name="eda">枝番</param>
        /// <param name="myCompany">ログインユーザの自社コード</param>
        /// <param name="option">
        ///   オプション
        ///     null:全データリスト
        ///       -2:先頭データ
        ///       -1:前のデータ
        ///        0:コード指定
        ///        1:次のデータ
        ///        2:最終データ
        /// </param>
        /// <returns></returns>
        public List<M01_TOK> GetData(int code, int eda, int myCompany, int? option)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var jisKbn =
                    context.M70_JIS
                        .Where(w => w.削除日時 == null && w.自社コード == myCompany)
                        .Select(s => s.自社区分)
                        .FirstOrDefault();

                // REMARKS:論理削除による削除データは抽出対象とする
                var result =
                    context.M01_TOK
                            .Where(w => w.削除日時 == null || w.論理削除 == 9)
                            .OrderBy(o => o.取引先コード)
                            .ThenBy(t => t.枝番)
                            .AsQueryable();

                // ログインユーザが販社の場合は担当会社の一致するデータのみを返す
                if (jisKbn == CommonConstants.自社区分.販社.GetHashCode())
                    result = result.Where(w => w.担当会社コード == myCompany);

                if (option == null)
                {   // オプションがnullの場合は全リストを返す
                    return result.ToList();
                }

                switch(option)
                {
                    case (int)Const.PagingOption.Paging_Code:
                        // コード指定
                        result = result.Where(w => w.取引先コード == code && w.枝番 == eda);
                        break;

                    case (int)Const.PagingOption.Paging_Top:
                        var fsub =
                            context.M01_TOK
                                .Where(x => x.削除日時 == null)
                                .AsQueryable();

                            result =
                                result.Where(w =>
                                    w.取引先コード == fsub.Min(m => m.取引先コード) &&
                                                        w.枝番 == fsub.Where(s => s.取引先コード == fsub.Min(m => m.取引先コード))
                                                            .Min(m => m.枝番));
                        break;

                    case (int)Const.PagingOption.Paging_Before:
                        var psub =
                            context.M01_TOK
                                .Where(x => x.削除日時 == null && x.取引先コード <= code && x.枝番 < eda)
                                .AsQueryable();

                        result =
                            result.Where(w => w.取引先コード == psub.Max(m => m.取引先コード) &&
                                 w.枝番 == psub.Where(s => s.取引先コード == psub.Max(m => m.取引先コード))
                                    .Max(m => m.枝番));
                        break;

                    case (int)Const.PagingOption.Paging_After:
                        var nsub =
                            context.M01_TOK
                                .Where(x => x.削除日時 == null && x.取引先コード >= code && x.枝番 > eda)
                                .AsQueryable();

                        result =
                            result.Where(w => w.取引先コード == nsub.Min(m => m.取引先コード) &&
                                 w.枝番 == nsub.Where(s => s.取引先コード == nsub.Min(m => m.取引先コード))
                                    .Min(m => m.枝番));
                        break;

                    case (int)Const.PagingOption.Paging_End:
                        var lsub =
                            context.M01_TOK
                                .Where(x => x.削除日時 == null)
                                .AsQueryable();

                        result =
                            result.Where(w => w.取引先コード == lsub.Max(m => m.取引先コード) &&
                                 w.枝番 == lsub.Where(s => s.取引先コード == lsub.Max(m => m.取引先コード))
                                    .Max(m => m.枝番));
                        break;

                }

                var resultData = result.FirstOrDefault();

                // 対象データが存在しない場合
                if (resultData == null)
                    return new List<M01_TOK>();

                return new List<M01_TOK>() { result.FirstOrDefault() };

            }

        }

        /// <summary>
        /// 取引先情報登録・更新
        /// </summary>
        /// <param name="data"></param>
        /// <param name="loginUserId"></param>
        public void Update(M01_TOK data, int loginUserId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 対象データ取得
                var tok = context.M01_TOK.Where(w => w.取引先コード == data.取引先コード && w.枝番 == data.枝番).FirstOrDefault();

                if (tok == null)
                {
                    // 登録データなし＝新規登録
                    M01_TOK regist = new M01_TOK();
                    regist.取引先コード = data.取引先コード;
                    regist.枝番 = data.枝番;
                    setInputDataToEntity(regist, data);
                    regist.登録者 = loginUserId;
                    regist.登録日時 = DateTime.Now;
                    regist.最終更新者 = loginUserId;
                    regist.最終更新日時 = DateTime.Now;

                    context.M01_TOK.ApplyChanges(regist);

                }
                else if (data != null && data.論理削除.Equals(9))
                {
                    // 論理削除 = 9 の場合
                    setInputDataToEntity(tok, data);
                    tok.削除者 = loginUserId;
                    tok.削除日時 = DateTime.Now;

                    tok.AcceptChanges();

                }
                else
                {
                    // データ更新
                    setInputDataToEntity(tok, data);
                    tok.最終更新者 = loginUserId;
                    tok.最終更新日時 = DateTime.Now;
                    tok.削除者 = null;
                    tok.削除日時 = null;

                    tok.AcceptChanges();

                }

                context.SaveChanges();

            }

        }

        /// <summary>
        /// 取引先情報削除
        /// </summary>
        /// <param name="data"></param>
        /// <param name="loginUserId"></param>
        public void Delete(M01_TOK data, int loginUserId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 対象データ取得
                var tok =
                    context.M01_TOK
                        .Where(w => w.取引先コード == data.取引先コード && w.枝番 == data.枝番)
                        .FirstOrDefault();

                // 対象データなしの場合は処理をスキップ
                if (tok == null)
                    return;

                tok.削除者 = loginUserId;
                tok.削除日時 = DateTime.Now;

                tok.AcceptChanges();

                context.SaveChanges();

            }

        }

        /// <summary>
        /// データ内容をEntityに詰め替える
        /// </summary>
        /// <param name="entity">データ設定先変数</param>
        /// <param name="data">画面入力値変数</param>
        private M01_TOK setInputDataToEntity(M01_TOK entity, M01_TOK data)
        {
            entity.取引区分 = data.取引区分;
            entity.得意先名１ = data.得意先名１;
            entity.得意先名２ = data.得意先名２;
            entity.部課名称 = data.部課名称;
            entity.略称名 = data.略称名;
            entity.郵便番号 = data.郵便番号;
            entity.住所１ = data.住所１;
            entity.住所２ = data.住所２;
            entity.電話番号 = data.電話番号;
            entity.ＦＡＸ = data.ＦＡＸ;
            entity.かな読み = data.かな読み;
            entity.担当会社コード = data.担当会社コード;
            entity.Ｔ消費税区分 = data.Ｔ消費税区分;
            entity.Ｔ税区分ID = data.Ｔ税区分ID;
            entity.Ｔ締日 = data.Ｔ締日;
            entity.Ｔ請求条件 = data.Ｔ請求条件;
            entity.Ｔ請求区分 = data.Ｔ請求区分;
            entity.Ｔサイト１ = data.Ｔサイト１;
            entity.Ｔサイト２ = data.Ｔサイト２;
            entity.Ｔ入金日１ = data.Ｔ入金日１;
            entity.Ｔ入金日２ = data.Ｔ入金日２;
            entity.Ｓ支払消費税区分 = data.Ｓ支払消費税区分;
            entity.Ｓ税区分ID = data.Ｓ税区分ID;
            entity.Ｓ締日 = data.Ｓ締日;
            entity.Ｓ支払条件 = data.Ｓ支払条件;
            entity.Ｓ支払区分 = data.Ｓ支払区分;
            entity.Ｓサイト１ = data.Ｓサイト１;
            entity.Ｓサイト２ = data.Ｓサイト２;
            entity.Ｓ入金日１ = data.Ｓ入金日１;
            entity.Ｓ入金日２ = data.Ｓ入金日２;
            entity.与信枠 = data.与信枠;
            entity.Ｔ担当者コード = data.Ｔ担当者コード;
            entity.Ｓ担当者コード = data.Ｓ担当者コード;
            entity.自社備考１ = data.自社備考１;
            entity.自社備考２ = data.自社備考２;
            entity.論理削除 = data.論理削除;
            entity.集約取引先コード = data.集約取引先コード;
            entity.集約取引先枝番 = data.集約取引先枝番;
            entity.参照取引先コード１ = data.参照取引先コード１;
            entity.参照取引先枝番１ = data.参照取引先枝番１;
            entity.参照取引先コード２ = data.参照取引先コード２;
            entity.参照取引先枝番２ = data.参照取引先枝番２;
            entity.参照取引先コード３ = data.参照取引先コード３;
            entity.参照取引先枝番３ = data.参照取引先枝番３;
            entity.集計コード１ = data.集計コード１;
            entity.集計コード２ = data.集計コード２;
            entity.集計コード３ = data.集計コード３;

            return entity;

        }

        /// <summary>
        /// 取引先マスタをリストで取得する
        /// </summary>
        /// <returns></returns>
        public List<M01_TOK> GetDataList(int? 表示区分, int 自社コード)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var result =
                    context.M01_TOK
                        .Where(w => w.削除日時 == null)
                        .OrderBy(o => o.取引先コード)
                        .AsQueryable();

                if (表示区分 != null && 表示区分 != 9)
                {
                    // 表示区分が設定されている場合は更に絞り込みをおこなう
                    result = result.Where(w => w.取引区分 == 表示区分);
                }

                var myKbn =
                    context.M70_JIS
                        .Where(w => w.削除日時 == null && w.自社コード == 自社コード)
                        .Select(s => s.自社区分)
                        .FirstOrDefault();

                // 自社区分が自社でない場合は担当会社と一致するデータのみを表示
                if (myKbn != CommonConstants.自社区分.自社.GetHashCode())
                    result =
                        result.Where(w => w.担当会社コード == 自社コード);

                return result.ToList();

            }

        }

        /// <summary>
        /// 指定された取引先情報を取得する
        /// </summary>
        /// <param name="取引先コード"></param>
        /// <param name="枝番"></param>
        /// <returns></returns>
        public static M01_TOK M01_TOK_Single_GetData(int 取引先コード, int 枝番)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var tok =
                    context.M01_TOK
                        .Where(w => w.削除日時 == null &&
                            w.取引先コード == 取引先コード
                            && w.枝番 == 枝番)
                        .FirstOrDefault();

                return tok;

            }

        }

    }

}
