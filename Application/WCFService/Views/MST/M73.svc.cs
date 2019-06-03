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


// メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コード、svc、および config ファイルで同時にクラス名 "DataDriveLogService" を変更できます。
// 注意: このサービスをテストするために WCF テスト クライアントを起動するには、ソリューション エクスプローラーで DataDriveLogService.svc または DataDriveLogService.svc.cs を選択し、デバッグを開始してください。
namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 消費税マスタサービスクラス
    /// </summary>
    public class M73
    {
        #region << メンバクラス定義 >>

        public class M73_ZEI_Member
        {
            public DateTime 適用開始日付 { get; set; }
            public int? 消費税率 { get; set; }
            public int? 軽減税率 { get; set; }
            public int? 登録担当者 { get; set; }
            public int? 更新担当者 { get; set; }
            public DateTime? 登録日時 { get; set; }
            public DateTime? 更新日時 { get; set; }
            public DateTime? 削除日時 { get; set; }
        }

        public class M73_ZEI_GridMember
        {
            public DateTime 適用開始日付 { get; set; }
            public int? 消費税率 { get; set; }
            public int? 軽減税率 { get; set; }
        }

        #endregion


        #region 全データリスト取得
        /// <summary>
        /// M73_ZEIのデータ取得
        /// </summary>
        public List<M73_ZEI_GridMember> GetAllData()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var result =
                     context.M73_ZEI
                        .Where(w => w.削除日時 == null)
                        .OrderBy(o => o.適用開始日付)
                        .Select(s => new M73_ZEI_GridMember
                        {
                            適用開始日付 = s.適用開始日付,
                            消費税率 = s.消費税率,
                            軽減税率 = s.軽減税率,
                        })
                        .ToList();

                return result;

            }

        }

        /// <summary>
        /// 消費税情報のリストを取得する
        /// </summary>
        /// <returns></returns>
        public List<M73_ZEI> GetDataList()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var result = context.M73_ZEI
                                .Where(x => x.削除日時 == null)
                                .OrderBy(o => o.適用開始日付);

                return result.ToList();

            }

        }
        #endregion

        #region データ取得
        /// <summary>
        /// M73_ZEIのデータ取得
        /// </summary>
        public List<M73_ZEI_Member> GetData(DateTime? p適用開始日付, int? pオプションコード)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m73 in context.M73_ZEI
                             select new M73_ZEI_Member
                             {
                                 適用開始日付 = m73.適用開始日付,
                                 消費税率 = m73.消費税率,
                                 軽減税率 = m73.軽減税率,
                                 削除日時 = m73.削除日時,
                             }).AsQueryable();

                if (p適用開始日付 != null)
                {
                    if (pオプションコード == 0)
                    {
                        query = query.Where(c => c.適用開始日付 == p適用開始日付);
                    }
                    else if (pオプションコード == -1)
                    {
                        //自社IDの1つ前のIDを取得
                        query = query.Where(c => c.適用開始日付 <= p適用開始日付);
                        if (query.Count() >= 2)
                        {
                            query = query.Where(c => c.適用開始日付 < p適用開始日付);
                        }
                        query = query.OrderByDescending(c => c.適用開始日付);
                    }
                    else
                    {
                        //自社IDの1つ後のIDを取得
                        query = query.Where(c => c.適用開始日付 >= p適用開始日付);
                        if (query.Count() >= 2)
                        {
                            query = query.Where(c => c.適用開始日付 > p適用開始日付);
                        }
                        query = query.OrderBy(c => c.適用開始日付);
                    }
                }
                else
                {
                    if (pオプションコード == 0)
                    {
                        //自社IDの先頭のIDを取得
                        query = query.OrderBy(c => c.適用開始日付);
                    }
                    else if (pオプションコード == 1)
                    {

                        query = query.Where(c => c.削除日時 == null);
                        query = query.OrderByDescending(c => c.適用開始日付);
                        if (pオプションコード == 0)
                        {
                            query = query.OrderBy(c => c.適用開始日付);
                        }
                    }
                }

                var ret = query.FirstOrDefault();
                List<M73_ZEI_Member> result = new List<M73_ZEI_Member>();
                if (ret != null)
                    result.Add(ret);

                return query.ToList();

            }

        }
        #endregion

        #region 更新
        /// <summary>
        /// M73_ZEIの更新
        /// </summary>
        /// <param name="m73zei">M73_ZEI_Member</param>
        public void Update(DateTime p適用開始日付, int p消費税率, int p軽減税率, int ユーザーID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                try
                {
                    // 更新行を取得
                    var m73 =
                        context.M73_ZEI
                            .Where(w => w.適用開始日付 == p適用開始日付)
                            .FirstOrDefault();

                    if (m73 == null)
                    {
                        M73_ZEI m73Zei = new M73_ZEI();
                        m73Zei.適用開始日付 = p適用開始日付;
                        m73Zei.消費税率 = p消費税率;
                        m73Zei.軽減税率 = p軽減税率;
                        m73Zei.登録者 = ユーザーID;
                        m73Zei.登録日時 = DateTime.Now;
                        m73Zei.最終更新者 = ユーザーID;
                        m73Zei.最終更新日時 = DateTime.Now;

                        context.M73_ZEI.ApplyChanges(m73Zei);

                    }
                    else
                    {
                        m73.消費税率 = p消費税率;
                        m73.軽減税率 = p軽減税率;
                        m73.最終更新日時 = DateTime.Now;
                        m73.最終更新者 = ユーザーID;

                        m73.AcceptChanges();

                    }

                    context.SaveChanges();

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        #endregion

        #region 物理削除
        /// <summary>
        /// M73_ZEIの物理削除
        /// </summary>
        /// <param name="m73zei">M73_ZEI_Member</param>
        public void Delete(DateTime p適用開始日付)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 削除行を取得
                var m73 =
                    context.M73_ZEI
                        .Where(w => w.適用開始日付 == p適用開始日付)
                        .FirstOrDefault();

                if (m73 != null)
                    context.DeleteObject(m73);

                context.SaveChanges();

            }

        }
        #endregion

        #region 一覧表示データ取得
        /// <summary>
        /// M73_ZEIの一覧表示
        /// </summary>
        /// <summary>
        public List<M73_ZEI_Member> GetDataHinList(DateTime dDayFrom, DateTime dDayTo)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 全件表示
                var resultList =
                    context.M73_ZEI
                        .Where(w => w.適用開始日付 >= dDayFrom && w.適用開始日付 <= dDayTo)
                        .Select(s => new M73_ZEI_Member
                            {
                                適用開始日付 = s.適用開始日付,
                                消費税率 = s.消費税率,
                                軽減税率 = s.軽減税率,
                                登録日時 = s.登録日時,
                                更新日時 = s.最終更新日時,
                                削除日時 = s.削除日時
                            })
                        .ToList();

                return resultList;

            }

        }
        #endregion


        #region 消費税計算
        /// <summary>
        /// 消費税計算をおこなう
        /// </summary>
        /// <returns></returns>
        public decimal getCalculatTax(int taxKbn, DateTime salesDate, int productCode, int cost, decimal qty)
        {
            decimal taxRate = 0;
            decimal calcValue = 0;

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                var hin =
                    context.M09_HIN
                        .Where(w => w.削除日時 == null && w.品番コード == productCode)
                        .FirstOrDefault();

                if (hin == null)
                    return 0;

                taxRate = getTargetTaxRate(context, salesDate, hin.消費税区分 ?? 0);
                calcValue = decimal.Multiply(cost * qty, decimal.Divide(taxRate, 100m));

            }

            decimal conTax = 0;
            switch (taxKbn)
            {
                case 1:
                    // 切捨て
                    conTax += Math.Floor(calcValue);
                    break;

                case 2:
                    // 四捨五入
                    conTax += Math.Round(calcValue, 0);
                    break;

                case 3:
                    // 切上げ
                    conTax += Math.Ceiling(calcValue);
                    break;

                default:
                    // 上記以外は税計算なしとする
                    break;

            }

            return conTax;

        }
        #endregion

        #region 指定日時点の消費税率取得
        /// <summary>
        /// 指定日時点の消費税を取得する
        /// </summary>
        /// <param name="targetDate">対象となる日付</param>
        /// <param name="option">軽減税率対象(0:対象外、1:対象)</param>
        /// <returns></returns>
        private int getTargetTaxRate(TRAC3Entities context, DateTime? targetDate, int option = 0)
        {
            int rate = 0;

            // 対象となる開始日を取得
            var maxDate =
                context.M73_ZEI
                    .Where(w => w.削除日時 == null && w.適用開始日付 <= targetDate)
                    .Max(m => m.適用開始日付);

            // 対象日付が取得できない場合は以下を処理しない
            if (maxDate == null)
                return rate;

            // 対象開始日と一致する消費税情報を取得
            var targetRow =
                context.M73_ZEI
                    .Where(x => x.適用開始日付 == maxDate)
                    .FirstOrDefault();

            switch (option)
            {
                case 0:
                    // 通常税率
                    rate = targetRow.消費税率 ?? 0;
                    break;

                case 1:
                    // 軽減税率
                    rate = targetRow.軽減税率 ?? 0;
                    break;

                case 2:
                    // 非課税
                    rate = 0;
                    break;

                default:
                    break;

            }

            return rate;

        }
        #endregion

    }

}
