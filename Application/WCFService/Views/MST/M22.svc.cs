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
using KyoeiSystem.Application.WCFService;


namespace KyoeiSystem.Application.WCFService
{
    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コード、svc、および config ファイルで同時にクラス名 "DataDriveLogService" を変更できます。
    // 注意: このサービスをテストするために WCF テスト クライアントを起動するには、ソリューション エクスプローラーで DataDriveLogService.svc または DataDriveLogService.svc.cs を選択し、デバッグを開始してください。
    public class M22
    {

        /// <summary>
        /// 倉庫マスタ 表示項目定義クラス
        /// </summary>
        public class M22_SOUK_Member
        {
            [DataMember]
            public int 倉庫コード { get; set; }
            [DataMember]
            public string 倉庫名 { get; set; }
            [DataMember]
            public string 略称名 { get; set; }
            [DataMember]
            public string かな読み { get; set; }
            [DataMember]
            public string 場所会社コード { get; set; }
            [DataMember]
            public string 寄託会社コード { get; set; }
            [DataMember]
            public int? 登録担当者 { get; set; }
            [DataMember]
            public int? 更新担当者 { get; set; }
            [DataMember]
            public int? 削除担当者 { get; set; }
            [DataMember]
            public DateTime? 登録日時 { get; set; }
            [DataMember]
            public DateTime? 更新日時 { get; set; }
            [DataMember]
            public DateTime? 削除日時 { get; set; }
            [DataMember]
            public int 場所会社自社区分 { get; set; }
        }

        /// <summary>
        /// 倉庫マスタリスト 表示項目定義クラス
        /// </summary>
        public class M22_SOUK_Search_Member
        {
            [DataMember]
            public int 倉庫コード { get; set; }
            [DataMember]
            public string 倉庫名 { get; set; }
            [DataMember]
            public string 略称名 { get; set; }
            [DataMember]
            public string かな読み { get; set; }
            [DataMember]
            public string 場所会社コード { get; set; }
            [DataMember]
            public string 寄託会社コード { get; set; }
        }

        /// <summary>
        /// M22_SOUKのデータ取得
        /// </summary>
        /// <param name="p倉庫コード">p倉庫コード</param>
        /// <returns>M22_SOUK_Member</returns>
        public List<M22_SOUK_Member> GetData(int? p倉庫コード, int pOptiion)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret =
                    context.M22_SOUK
                        .GroupJoin(context.M70_JIS.Where(w => w.削除日時 == null),
                            x => x.場所会社コード,
                            y => y.自社コード,
                            (x, y) => new { x, y })
                        .SelectMany(z => z.y.DefaultIfEmpty(),
                            (a, b) => new { m22 = a.x, m70 = b })
                        .OrderBy(o => o.m22.倉庫コード)
                        .ToList()
                        .Select(mem => new M22_SOUK_Member
                        {
                            倉庫コード = mem.m22.倉庫コード,
                            倉庫名 = mem.m22.倉庫名,
                            略称名 = mem.m22.倉庫略称名,
                            かな読み = mem.m22.かな読み,
                            場所会社コード = mem.m22.場所会社コード.ToString(),
                            寄託会社コード = mem.m22.寄託会社コード.ToString(),
                            登録日時 = mem.m22.登録日時,
                            更新日時 = mem.m22.最終更新日時,
                            削除日時 = mem.m22.削除日時,
                            場所会社自社区分 = mem.m70.自社区分
                        })
                        .AsQueryable();

                //データが1件もない状態で<< < > >>を押された時の処理
                if ((p倉庫コード == null || p倉庫コード == 0) && ret.Where(c => c.削除日時 == null).Count() == 0)
                {
                    return null;
                }

                if (p倉庫コード != null)
                {
                    if (pOptiion == 0)
                    {
                        if (-1 != p倉庫コード)
                        {
                            ret = ret.Where(c => c.倉庫コード == p倉庫コード);
                        }
                    }
                    else if (pOptiion > 0)
                    {
                        //p倉庫コードの1つ後のIDを取得
                        ret = ret.Where(c => c.削除日時 == null);
                        ret = ret.Where(c => c.倉庫コード > p倉庫コード);
                        ret = ret.OrderBy(c => c.倉庫コード);
                    }
                    else if (pOptiion < 0)
                    {
                        // p倉庫コードの1つ前のIDを取得
                        ret = ret.Where(c => c.削除日時 == null);
                        ret = ret.Where(c => c.倉庫コード < p倉庫コード);
                        ret = ret.OrderByDescending(c => c.倉庫コード);
                    }

                }
                else
                {
                    if (pOptiion == 0)
                    {
                        // 倉庫コードの先頭のIDを取得
                        ret = ret.Where(c => c.削除日時 == null);
                        ret = ret.OrderBy(c => c.倉庫コード);
                    }
                    else if (pOptiion == 1)
                    {
                        // 倉庫コードの最後のIDを取得
                        ret = ret.Where(c => c.削除日時 == null);
                        ret = ret.OrderByDescending(c => c.倉庫コード);
                    }
                }

                return ret.ToList();

            }

        }

        /// <summary>
        /// M22_SOUKの更新
        /// </summary>
        /// <param name="m22tik">M22_SOUK_Member</param>
        /// <returns>処理結果
        ///   1:正常
        ///  -9:ユニーク制約エラー
        /// </returns>
        public int Update(int p倉庫コード, string p倉庫名, string p略称名, string pかな読み, int p場所会社, int p寄託会社, int ユーザID, bool pMaintenanceFlg, bool pGetNextNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 登録処理前にユニーク制約チェックをおこなう
                var chk = context.M22_SOUK
                                .Where(w => w.倉庫コード != p倉庫コード &&
                                    w.場所会社コード == p場所会社 &&
                                    w.寄託会社コード == p寄託会社)
                                .Count();

                if (chk > 0)
                    return -9;

                if (pGetNextNumber)
                    p倉庫コード = GetNextNumber();

                // 更新行を特定
                var m22 = context.M22_SOUK
                                .Where(x => x.倉庫コード == p倉庫コード)
                                .FirstOrDefault();

                if (m22 == null)
                {
                    M22_SOUK m22in = new M22_SOUK();

                    m22in.倉庫コード = p倉庫コード;
                    m22in.倉庫名 = p倉庫名;
                    m22in.倉庫略称名 = p略称名;
                    m22in.かな読み = pかな読み;
                    m22in.場所会社コード = p場所会社;
                    m22in.寄託会社コード = p寄託会社;
                    m22in.登録日時 = DateTime.Now;
                    m22in.最終更新日時 = DateTime.Now;
                    m22in.登録者 = ユーザID;
                    m22in.最終更新者 = ユーザID;
                    m22in.削除日時 = null;
                    m22in.削除者 = null;

                    context.M22_SOUK.ApplyChanges(m22in);

                }
                else
                {
                    if (pMaintenanceFlg)
                        return -1;

                    m22.倉庫名 = p倉庫名;
                    m22.倉庫略称名 = p略称名;
                    m22.かな読み = pかな読み;
                    m22.場所会社コード = p場所会社;
                    m22.寄託会社コード = p寄託会社;
                    m22.最終更新日時 = DateTime.Now;
                    m22.最終更新者 = ユーザID;
                    m22.削除日時 = null;
                    m22.削除者 = null;
                    m22.AcceptChanges();
                }

                context.SaveChanges();
            }
            return 1;
        }

        /// <summary>
        /// M22_SOUKの削除
        /// </summary>
        /// <param name="m22tik">M22_SOUK_Member</param>
        public void Delete(int p倉庫コード, int ユーザID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 削除行を特定
                var m22 = context.M22_SOUK.Where(x => x.倉庫コード == p倉庫コード).FirstOrDefault();

                if (m22 != null)
                {
                    m22.削除日時 = DateTime.Now;
                    m22.削除者 = ユーザID;
                    m22.AcceptChanges();
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M22_SOUKのID自動採番
        /// </summary>
        /// <returns></returns>
        public int GetNextNumber()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //最大ID行を特定
                var query = context.M22_SOUK.Select(s => s.倉庫コード);

                int iMaxID;
                if (query.Count() == 0)
                {
                    iMaxID = 0;
                }
                else
                {
                    iMaxID = query.Max();
                }

                return iMaxID + 1;

            }

        }

        /// <summary>
        /// M22_SOUKの検索データ取得
        /// </summary>
        /// <param name="p摘要ID">摘要ID</param>
        /// <returns>M22_SOUK_Member</returns>
        public List<M22_SOUK_Search_Member> GetSearchData(int? p倉庫コード, int pOptiion)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = context.M22_SOUK
                                .Where(w => w.削除日時 == null)
                                .ToList()
                                .Select(m22 => new M22_SOUK_Search_Member
                                {
                                    倉庫コード = m22.倉庫コード,
                                    倉庫名 = m22.倉庫名,
                                    略称名 = m22.倉庫略称名,
                                    かな読み = m22.かな読み,
                                    場所会社コード = m22.場所会社コード.ToString(),
                                    寄託会社コード = m22.寄託会社コード.ToString()
                                })
                                .AsQueryable();

                if (p倉庫コード != null)
                {
                    if (pOptiion == 0)
                    {
                        if (-1 != p倉庫コード)
                        {
                            ret = ret.Where(c => c.倉庫コード == p倉庫コード);
                        }
                    }
                }


                return ret.ToList();
            }
        }

        /// <summary>
        /// 倉庫マスタ一覧検索データ取得
        /// </summary>
        /// <returns></returns>
        public List<M22_SOUK_Member> GetSearchDataForList(string 倉庫コードFROM, string 倉庫コードTO, string 倉庫名指定, string 表示方法)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = context.M22_SOUK
                                .Where(w => w.削除日時 == null)
                                .ToList()
                                .Select(m22 => new M22_SOUK_Member
                                {
                                    倉庫コード = m22.倉庫コード,
                                    倉庫名 = m22.倉庫名,
                                    略称名 = m22.倉庫略称名,
                                    かな読み = m22.かな読み,
                                    場所会社コード = m22.場所会社コード.ToString(),
                                    寄託会社コード = m22.寄託会社コード.ToString(),
                                    登録日時 = m22.登録日時,
                                    更新日時 = m22.最終更新日時
                                })
                                .AsQueryable();

                if (!(string.IsNullOrEmpty(倉庫コードFROM + 倉庫コードTO) && string.IsNullOrEmpty(倉庫名指定)))
                {
                    if (!string.IsNullOrEmpty(倉庫コードFROM))
                    {
                        int i倉庫コードFROM = AppCommon.IntParse(倉庫コードFROM);
                        ret = ret.Where(c => c.倉庫コード >= i倉庫コードFROM);
                    }
                    if (!string.IsNullOrEmpty(倉庫コードTO))
                    {
                        int i倉庫コードTO = AppCommon.IntParse(倉庫コードTO);
                        ret = ret.Where(c => c.倉庫コード <= i倉庫コードTO);
                    }


                    if (!string.IsNullOrEmpty(倉庫名指定))
                    {
                        ret = ret.Where(c => c.倉庫名.Contains(倉庫名指定));
                    }

                }

                ret = ret.Distinct();

                if (表示方法 == "0")
                {
                    ret = ret.OrderBy(c => c.倉庫コード);
                }
                else
                {
                    ret = ret.OrderBy(c => c.かな読み);
                }


                return ret.ToList();
            }

        }

    }

}
