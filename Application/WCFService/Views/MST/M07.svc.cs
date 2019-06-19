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
    public class M07 : IM07
    {

        /// <summary>
        /// M07_KEIのデータ取得
        /// </summary>
        /// <param name="p経費項目ID">経費項目ID</param>
        /// <returns>M07_KEI_Member</returns>
        public List<M07_KEI_Member> GetData(int? p経費項目ID, int pOptiion)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m07 in context.M07_KEI
						   //where m07.削除日付 == null
                           select new M07_KEI_Member
                           {
                               経費項目ID = m07.経費項目ID,
                               登録日時 = m07.登録日時,
                               更新日時 = m07.更新日時,
                               経費項目名 = m07.経費項目名,
                               経費区分 = m07.経費区分,
                               固定変動区分 = m07.固定変動区分,
                               編集区分 = m07.編集区分,
                               グリーン区分 = m07.グリーン区分,
                               削除日付 = m07.削除日付,
                           }).AsQueryable();

                //データが1件もない状態で<< < > >>を押された時の処理
				if ((p経費項目ID == null || p経費項目ID == 0) && ret.Where(c => c.削除日付 == null).Count() == 0)
                {
                    return null;
                }

                if (p経費項目ID != null)
                {
                    if (pOptiion == 0)
                    {
                        if (-1 != p経費項目ID)
                        {
                            ret = ret.Where(c => c.経費項目ID == p経費項目ID);
                        }
                    }
                    else if (pOptiion > 0)
                    {
                        //p経費項目IDの1つ後のIDを取得
                        ret = ret.Where(c => c.削除日付 == null);
                        ret = ret.Where(c => c.経費項目ID > p経費項目ID);
                        ret = ret.OrderBy(c => c.経費項目ID);
                    }
                    else if (pOptiion < 0)
                    {
                        //p経費項目IDの1つ前のIDを取得
                        ret = ret.Where(c => c.削除日付 == null);
                        ret = ret.Where(c => c.経費項目ID < p経費項目ID);
                        ret = ret.OrderByDescending(c => c.経費項目ID);
                    }
                }
                else
                {
                    if (pOptiion == 0)
                    {
                        //経費項目IDの先頭のIDを取得
                        ret = ret.Where(c => c.削除日付 == null);
                        ret = ret.OrderBy(c => c.経費項目ID);
                    }
                    else if (pOptiion == 1)
                    {
                        //経費項目IDの最後のIDを取得
                        ret = ret.Where(c => c.削除日付 == null);
                        ret = ret.OrderByDescending(c => c.経費項目ID);
                    }
                }


                    return ret.ToList();
            }
        }

        /// <summary>
        /// M07_KEIの更新
        /// </summary>
        /// <param name="m07tik">M07_KEI_Member</param>
        public int Update(int p経費項目ID, string p経費項目名, int p経費区分, int p固定変動区分, int p編集区分, int pグリーン区分, bool pMaintenanceFlg, bool pGetNextNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                if (pGetNextNumber)
                {
                    p経費項目ID = GetNextNumber();
                }

                //更新行を特定
                var ret = from x in context.M07_KEI
                          where (x.経費項目ID == p経費項目ID)
                          orderby x.経費項目ID
                          select x;
                var m07 = ret.FirstOrDefault();

                if (m07 == null)
                {
                    M07_KEI m07in = new M07_KEI();
                    m07in.経費項目ID = p経費項目ID;
                    m07in.登録日時 = DateTime.Now;
                    m07in.更新日時 = DateTime.Now;
                    m07in.経費項目名 = p経費項目名;
                    m07in.経費区分 = p経費区分;
                    m07in.固定変動区分 = p固定変動区分;
                    m07in.編集区分 = p編集区分;
                    m07in.グリーン区分 = pグリーン区分;
                    m07in.削除日付 = null;
                    context.M07_KEI.ApplyChanges(m07in);
                }
                else
                {
                    if (pMaintenanceFlg)
                    {
                        return -1;
                    }

                    m07.経費項目ID = p経費項目ID;
                    //m07.登録日時 = DateTime.Now;
                    m07.更新日時 = DateTime.Now;
                    m07.経費項目名 = p経費項目名;
                    m07.経費区分 = p経費区分;
                    m07.固定変動区分 = p固定変動区分;
                    m07.編集区分 = p編集区分;
                    m07.グリーン区分 = pグリーン区分;
                    m07.削除日付 = null;
                    m07.AcceptChanges();
                }

                context.SaveChanges();
            }
            return 1;
        }

        /// <summary>
        /// M07_KEIの削除
        /// </summary>
        /// <param name="m07tik">M07_KEI_Member</param>
        public void Delete(int p経費項目ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M07_KEI
                          where (x.経費項目ID == p経費項目ID)
                          orderby x.経費項目ID
                          select x;
                var m07 = ret.FirstOrDefault();
                if (m07 != null)
                {
                    m07.削除日付 = DateTime.Now;
                    m07.AcceptChanges();
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M07_KEIのID自動採番
        /// </summary>
        /// <returns></returns>
        public int GetNextNumber()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //最大ID行を特定
                var query = from x in context.M07_KEI
                            select x.経費項目ID;

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
        /// M07_KEIの検索データ取得
        /// </summary>
        /// <param name="p経費項目ID">経費項目ID</param>
        /// <returns>M07_KEI_Member</returns>
        public List<M07_KEI_Search_Member> GetSearchData(int? p経費項目ID, int pOptiion)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m07 in context.M07_KEI
                           where m07.削除日付 == null
                           select new M07_KEI_Search_Member
                           {
                               経費項目ID = m07.経費項目ID,
                               経費項目名 = m07.経費項目名,
                               経費区分 = m07.経費区分,
                               固定変動区分 = m07.固定変動区分,
                           }).AsQueryable();

                if (p経費項目ID != null)
                {
                    if (pOptiion == 0)
                    {
                        if (-1 != p経費項目ID)
                        {
                            ret = ret.Where(c => c.経費項目ID == p経費項目ID);
                        }
                    }

                    if (pOptiion == 1)
                    {
                        ret = ret.Where(c => c.固定変動区分 == 0);
                        if (-1 != p経費項目ID)
                        {
                            ret = ret.Where(c => c.経費項目ID == p経費項目ID);
                        }
                    }

                    if (pOptiion == 2)
                    {
                        ret = ret.Where(c => c.固定変動区分 == 1);
                        if (-1 != p経費項目ID)
                        {
                            ret = ret.Where(c => c.経費項目ID == p経費項目ID);
                        }
                    }
                }

                return ret.ToList();
            }
        }


        /// <summary>
        /// 経費項目マスタ一覧検索データ取得
        /// </summary>
        /// <returns></returns>
        public List<M07_KEI_PRINT> GetSearchDataForList(string 経費項目コードFROM, string 経費項目コードTO, int[] i経費項目IDList, string 表示方法)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();


                //string[] Keihi_items = { "1:車輌費", "2:保険費", "3:人件費", "4:燃料費", "5:修理費", "6:諸経費" };

                var ret = (from m07 in context.M07_KEI
                           where m07.削除日付 == null
                           select new M07_KEI_PRINT
                           {
                               
                               経費項目ID = m07.経費項目ID,
                               経費項目名 = m07.経費項目名,
                               経費区分 = m07.経費区分,
                               //経費区分名 = m07.経費区分 == null ? "" : Keihi_items[Convert.ToInt32(m07.経費区分)],
                               経費区分名 = m07.経費区分 == 1 ? "1:車輌費" : m07.経費区分 == 2 ? "2:保険費" : m07.経費区分 == 3 ? "3:人件費" : 
                                                            m07.経費区分 == 4 ? "4:燃料費" : m07.経費区分 == 5 ? "5:修理費" : m07.経費区分 == 6 ? "6:諸経費" : "",
                               固定変動区分名 = m07.固定変動区分 == 0 ? "固定" : "変動",
                           }).AsQueryable();

                if (!(string.IsNullOrEmpty(経費項目コードFROM + 経費項目コードTO ) && i経費項目IDList.Length == 0))
                {

                    if (string.IsNullOrEmpty(経費項目コードFROM + 経費項目コードTO))
                    {

                        ret = ret.Where(c => c.経費項目ID >= int.MaxValue);
                    }
                    if (!string.IsNullOrEmpty(経費項目コードFROM))
                    {
                        int i経費項目コードFROM = AppCommon.IntParse(経費項目コードFROM);
                        ret = ret.Where(c => c.経費項目ID >= i経費項目コードFROM);
                    }
                    if (!string.IsNullOrEmpty(経費項目コードTO))
                    {
                        int i経費項目コードTO = AppCommon.IntParse(経費項目コードTO);
                        ret = ret.Where(c => c.経費項目ID <= i経費項目コードTO);
                    }


                    if (i経費項目IDList.Length > 0)
                    {
                        var intCause = i経費項目IDList;

                        ret = ret.Union(from m07 in context.M07_KEI
                                        where m07.削除日付 == null && intCause.Contains(m07.経費項目ID)
                                        select new M07_KEI_PRINT
                                        {
                                            経費項目ID = m07.経費項目ID,
                                            経費項目名 = m07.経費項目名,
                                            経費区分 = m07.経費区分,
                                            経費区分名 = m07.経費区分 == 1 ? "1:車輌費" : m07.経費区分 == 2 ? "2:保険費" : m07.経費区分 == 3 ? "3:人件費" :
                                                                         m07.経費区分 == 4 ? "4:燃料費" : m07.経費区分 == 4 ? "5:修理費" : m07.経費区分 == 4 ? "6:諸経費" : "",
                                            固定変動区分名 = m07.固定変動区分 == 0 ? "固定" : "変動",
                                        });
                    }

                }

                ret = ret.Distinct();

                if (表示方法 == "0")
                {
                    ret = ret.OrderBy(c => c.経費項目ID);
                }
                else
                {
                    ret = ret.OrderBy(c => c.経費区分).ThenBy(c => c.経費項目ID);
                }
                

                return ret.ToList();
            }
        }

    }
}
