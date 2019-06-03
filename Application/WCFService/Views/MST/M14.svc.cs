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
    public class M14 : IM14 {

        /// <summary>
        /// M14_GSYAのデータ取得
        /// </summary>
        /// <param name="pG車種IDID">G車種ID</param>
        /// <returns>M14_GSYA_Member</returns>
        public List<M14_GSYA_Member> GetData(int? pG車種ID, int pオプションコード)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = (from m14 in context.M14_GSYA
                             where m14.削除日付 == null
                             select new M14_GSYA_Member
                             {
                                 G車種ID = m14.G車種ID,
                                 登録日時 = m14.登録日時,
                                 更新日時 = m14.更新日時,
                                 G車種名 = m14.G車種名,
                                 略称名 = m14.略称名,
                                 CO2排出係数１ = m14.CO2排出係数１,
                                 CO2排出係数２ = m14.CO2排出係数２,
                                 事業用区分 = m14.事業用区分,
                                 ディーゼル区分 = m14.ディーゼル区分,
                                 小型普通区分 = m14.小型普通区分,
                                 低公害区分 = m14.低公害区分,
                                 削除日付 = m14.削除日付,
                             }).AsQueryable();

                //データが1件もない状態で<< < > >>を押された時の処理
                if ((pG車種ID == null || pG車種ID == 0) && query.Count() == 0)
                {
                    return null;
                }
                
                if (pG車種ID != null)
                {

                    if(pG車種ID == -1)
                    {
                        //全件取得
                        return query.ToList();
                    }
                    
                    if (pオプションコード == 0)
                    {
                        query = query.Where(c => c.G車種ID == pG車種ID);
                    }
                    
                    else if (pオプションコード == -1)
                    {
                        //p車種IDの1つ前のIDを取得
                        query = query.Where(c => (c.削除日付 == null));
                        
                        query = query.Where(c => c.G車種ID < pG車種ID);
                        if (query.Count() >= 2)
                        {
                            query = query.Where(c => c.G車種ID < pG車種ID);
                        }
                        query = query.OrderByDescending(c => c.G車種ID);
                    }
                    else
                    {
                        //p車種IDの1つ後のIDを取得
                        query = query.Where(c => (c.削除日付 == null));
                        query = query.Where(c => c.G車種ID > pG車種ID);

                        if (query.Count() >= 2)
                        {
                            query = query.Where(c => c.G車種ID > pG車種ID);
                        }
                        query = query.OrderBy(c => c.G車種ID);
                    }
                }
                else
                {

                    if (pオプションコード == 0)
                    {
                        //車種IDの先頭のIDを取得
                        query = query.Where(c => (c.削除日付 == null));
                        query = query.OrderBy(c => c.G車種ID);
                    }
                    else if (pオプションコード == 1)
                    {
                        //車種IDの最後のIDを取得
                        query = query.Where(c => (c.削除日付 == null));
                        query = query.OrderByDescending(c => c.G車種ID);
                    }
                    else
                    {
                        //pオプションコード == 2
                        //query = query.Where(c => (c.削除日付 == null));
                        //query = query.OrderBy(c => c.G車種ID < pG車種ID + 1);
                    }
                }

                var ret = query.FirstOrDefault();
                List<M14_GSYA_Member> result = new List<M14_GSYA_Member>();
                if (ret != null)
                {
                    result.Add(ret);
                }
                return result;
			

            }
        }

        /// <summary>
        /// M14_GSYAの新規追加
        /// </summary>
        /// <param name="m14gsya">M14_GSYA_Member</param>
        public int Update(int? pG車種ID, string pG車種名, string 略称名, decimal? CO2排出係数１, decimal? CO2排出係数２, int? 事業用区分, int? ディーゼル区分, int? 小型普通貨物区分, int? 低公害者区分, bool pMaintenanceFlg, bool pGetNextNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                if (pGetNextNumber)
                {
                    pG車種ID = GetNextNumber();
                }

                //更新行を特定
                var ret = from x in context.M14_GSYA
                          where (x.G車種ID == pG車種ID)
                          orderby x.G車種ID
                          select x;
                var data = ret.FirstOrDefault();

                //更新
                if (data == null)
                {
                    M14_GSYA m14 = new M14_GSYA();
                    m14.G車種ID = (int)pG車種ID;
                    m14.登録日時 = m14.登録日時;
                    m14.更新日時 = m14.更新日時;
                    m14.G車種名 = pG車種名;
                    m14.略称名 = 略称名;
                    m14.CO2排出係数１ = CO2排出係数１;
                    m14.CO2排出係数２ = CO2排出係数２;
                    m14.事業用区分 = 事業用区分;
                    m14.ディーゼル区分 = ディーゼル区分;
                    m14.小型普通区分 = 小型普通貨物区分;
                    m14.低公害区分 = 低公害者区分;
                    m14.削除日付 = null;

                    //登録時、記述
                    context.M14_GSYA.ApplyChanges(m14);
                }
                //登録
                else
                {
                    if (pMaintenanceFlg)
                    {
                        return -1;
                    }

                    data.G車種ID = (int)pG車種ID;
                    data.登録日時 = DateTime.Now;
                    data.G車種名 = pG車種名;
                    data.略称名 = 略称名;
                    data.CO2排出係数１ = CO2排出係数１;
                    data.CO2排出係数２ = CO2排出係数２;
                    data.事業用区分 = 事業用区分;
                    data.ディーゼル区分 = ディーゼル区分;
                    data.小型普通区分 = 小型普通貨物区分;
                    data.低公害区分 = 低公害者区分;
                    data.削除日付 = null;

                    //更新時、記述
                    data.AcceptChanges();
                }
                //データベースへの最終登録
                context.SaveChanges();
                
            }
            return 1;
        }

        

        /// <summary>
        /// M14_GSYAの物理削除
        /// </summary>
        /// <param name="m14gsya">M14_GSYA_Member</param>
        public void Delete(int? pG車種ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M14_GSYA
                          where (x.G車種ID == pG車種ID)
                          orderby x.G車種ID
                          select x;
                var m14 = ret.FirstOrDefault();

                if (m14 != null)
                {
                    m14.削除日付 = DateTime.Now;
                }

                m14.AcceptChanges();
                context.SaveChanges();
            }
        }


        /// <summary>
        /// M09_HINの検索データ取得
        /// </summary>
        /// <param name="p発着地ID">商品ID</param>
        /// <returns>M09_HIN_Member</returns>
        public List<M14_GSYA_SCH_Member> M14_GSYA_SCH(int? pG車種ID, int pOptiion)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m14 in context.M14_GSYA
                           where m14.削除日付 == null
                           select new M14_GSYA_SCH_Member
                           {
                               G車種ID = m14.G車種ID,
                               G車種名 = m14.G車種名,
                               事業用区分 = m14.事業用区分 == 0 ? "事業用" : m14.事業用区分 == 1 ? "自家用" : "",
                           }).AsQueryable();

                if (pG車種ID != null)
                {
                    if (pOptiion == 0)
                    {
                        if (-1 != pG車種ID)
                        {
                            ret = ret.Where(c => c.G車種ID == pG車種ID);
                        }
                    }
                }

                return ret.ToList();
            }
        }


        /// <summary>
        /// M08_TIKのID自動採番
        /// </summary>
        /// <returns></returns>
        public int GetNextNumber()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //最大ID行を特定
                var query = from x in context.M14_GSYA
                            select x.G車種ID;

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

    }
}
