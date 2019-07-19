﻿using System;
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

    /// <summary>
    /// 品番マスタサービスクラス
    /// </summary>
    public class MST01011
    {


        /// <summary>
        /// MST01011データメンバー
        /// </summary>
        /// [DataContract]
        public class MST01011_spread
        {
            public int? 取引先コード { get; set; }
            public int? 担当会社コード { get; set; }
            public int? 枝番 { get; set; }
            public string 正式名称 { get; set; }
            public int? 請求担当者コード { get; set; }
            public string 請求担当者名 { get; set; }
            public int? 支払担当者コード { get; set; }
            public string 支払担当者名 { get; set; }
        }

        /// <summary>
        /// 取引先データを取得する
        /// </summary>
        /// <returns></returns>
        public List<MST01011_spread> GetData(string p正式名称, string p担当会社コード, string p取引区分)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                int code = 0;
                int i担当会社コード = 0;
                int i取引区分 = 1;

                if (int.TryParse(p担当会社コード, out code))
                {
                    i担当会社コード = code;
                }

                if (int.TryParse(p取引区分, out code))
                {
                    i取引区分 = code;
                }


                var result = (
                    from m01 in context.M01_TOK
                    join m72_t in context.M72_TNT on m01.Ｔ担当者コード equals m72_t.担当者ID into 担当者t
                    from TNT_t in 担当者t.DefaultIfEmpty()
                    join m72_s in context.M72_TNT on m01.Ｓ担当者コード equals m72_s.担当者ID into 担当者s
                    from TNT_s in 担当者s.DefaultIfEmpty()
                    where (m01.取引区分 == i取引区分)
                    select new MST01011_spread
                    {
                        取引先コード = m01.取引先コード,
                        担当会社コード = m01.担当会社コード,
                        枝番 = m01.枝番,
                        正式名称 = m01.得意先名１,
                        請求担当者コード = m01.Ｔ担当者コード,
                        請求担当者名 = TNT_t.担当者名,
                        支払担当者コード = m01.Ｓ担当者コード,
                        支払担当者名 = TNT_s.担当者名,

                    }).AsQueryable();

                if (!string.IsNullOrEmpty(p担当会社コード))
                {
                    result = result.Where(c => c.担当会社コード == i担当会社コード);
                }

                if (!string.IsNullOrEmpty(p正式名称))
                {
                    result = result.Where(c => c.正式名称.Contains(p正式名称));
                }

                return result.ToList();

            }

        }




        /// <summary>
        /// 商品群データの登録・更新をおこなう
        /// </summary>
        /// <param name="updData"></param>
        /// <param name="pLoginUserCode"></param>
        /// <returns></returns>
        public int Update(DataSet ds, int pLoginUserCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {

                try
                {

                    context.Connection.Open();

                    DataTable dt = ds.Tables["MST01011_GetData"];

                    foreach (DataRow rw in dt.Rows)
                    {
                        // 変更なしデータは処理対象外とする
                        if (rw.RowState == DataRowState.Unchanged)
                            continue;

                        MST01011_spread row = getRowData(rw);


                        // 対象データ取得
                        var data =
                            context.M01_TOK
                                .Where(w=> w.取引先コード == row.取引先コード && w.枝番 == row.枝番)
                                .FirstOrDefault();

                        if (data != null)
                        {
                            data.Ｔ担当者コード = row.請求担当者コード;
                            data.Ｓ担当者コード = row.支払担当者コード;
                            
                            data.最終更新者 = pLoginUserCode;
                            data.最終更新日時 = DateTime.Now;

                            data.AcceptChanges();
                        }

                    }

                    context.SaveChanges();

                }
                catch (Exception ex)
                {
                    return 0;
                }

            }

            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rw">DataRow</param>
        /// <returns></returns>
        private MST01011_spread getRowData(DataRow rw)
        {
            MST01011_spread data = new MST01011_spread();

            data.取引先コード = int.Parse(rw["取引先コード"].ToString());
            data.枝番 = int.Parse(rw["枝番"].ToString());

            data.請求担当者コード = int.Parse(rw["請求担当者コード"].ToString());
            data.支払担当者コード = int.Parse(rw["支払担当者コード"].ToString());
            return data;

        }

    }

}