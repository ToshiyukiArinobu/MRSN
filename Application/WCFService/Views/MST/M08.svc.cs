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
    public class M08 : IM08
    {

        /// <summary>
        /// M08_TIKのデータ取得
        /// </summary>
        /// <param name="p発着地ID">発着地ID</param>
        /// <returns>M08_TIK_Member</returns>
        public List<M08_TIK_Member> GetData(int? p発着地ID, int pOptiion)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m08 in context.M08_TIK
                           select new M08_TIK_Member
                           {
                               発着地ID = m08.発着地ID,
                               登録日時 = m08.登録日時,
                               更新日時 = m08.更新日時,
                               発着地名 = m08.発着地名,
                               かな読み = m08.かな読み,
                               タリフ距離 = m08.タリフ距離,
                               郵便番号 = m08.郵便番号,
                               住所１ = m08.住所１,
                               住所２ = m08.住所２,
                               電話番号 = m08.電話番号,
                               ＦＡＸ番号 = m08.ＦＡＸ番号,
                               配送エリアID = m08.配送エリアID,
                               削除日付 = m08.削除日付,
                           }).AsQueryable();

                //データが1件もない状態で<< < > >>を押された時の処理
				if ((p発着地ID == null || p発着地ID == 0) && ret.Where(c => c.削除日付 == null).Count() == 0)
                {
                    return null;
                }

                if (p発着地ID != null)
                {
                    if (pOptiion == 0)
                    {
                        if (-1 != p発着地ID)
                        {
                            ret = ret.Where(c => c.発着地ID == p発着地ID);
                        }
                    }
                    else if (pOptiion > 0)
                    {
                        if (p発着地ID == 0)
                        {
                            //　右矢印の採番
                            ret = ret.Where(c => c.削除日付 == null);
                            ret = ret.Where(c => c.発着地ID >= p発着地ID + 1);
                            ret = ret.OrderBy(c => c.発着地ID <= p発着地ID);
                        }
                        else
                        {
                            ret = ret.Where(c => c.削除日付 == null);
                            ret = ret.Where(c => c.発着地ID >= p発着地ID);
                            if (ret.Count() > 1)
                            {
                                ret = ret.Where(c => c.発着地ID > p発着地ID);
                                ret = ret.OrderBy(c => c.発着地ID);
                            }
                        }
                        //p発着地IDの1つ後のIDを取得
                    }
                    else if (pOptiion < 0)
                    {
                        if (p発着地ID == 0)
                        {
                            // 左矢印の採番 
                            ret = ret.Where(c => c.削除日付 == null);
                            ret = ret.Where(c => c.発着地ID >= p発着地ID);
                        }
                        else
                        {
                            //p発着地IDの1つ前のIDを取得
                            ret = ret.Where(c => c.削除日付 == null);
                            ret = ret.Where(c => c.発着地ID <= p発着地ID);
                            if (ret.Count() > 1)
                            {
                                ret = ret.Where(c => c.発着地ID < p発着地ID);
                                ret = ret.OrderByDescending(c => c.発着地ID);
                            }
                        }
                    }
                }
                else
                {
                    if (pOptiion == 0)
                    {
                        //発着地IDの先頭のIDを取得
                        ret = ret.Where(c => c.削除日付 == null);
                        ret = ret.OrderBy(c => c.発着地ID);
                    }
                    else if (pOptiion == 1)
                    {
                        //発着地IDの最後のIDを取得
                        ret = ret.Where(c => c.削除日付 == null);
                        ret = ret.OrderByDescending(c => c.発着地ID);
                    }
                }

                var query = ret.FirstOrDefault();
                List<M08_TIK_Member> result = new List<M08_TIK_Member>();
                if (query != null)
                {
                    result.Add(query);
                }

                return result;
            }
        }

		/// <summary>
		/// M08_TIKの類似データ取得
		/// </summary>
		/// <param name="p発着地ID">発着地ID</param>
		/// <returns>M08_TIK_Member</returns>
		public List<M08_TIK_Member> RGetData(int? p発着地ID)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

				p発着地ID = p発着地ID == 0 ? null : p発着地ID;

				var ret = (from m08 in context.M08_TIK where m08.発着地ID == p発着地ID
						   select new M08_TIK_Member
						   {
							   発着地ID = m08.発着地ID,
							   登録日時 = m08.登録日時,
							   更新日時 = m08.更新日時,
							   発着地名 = m08.発着地名,
							   かな読み = m08.かな読み,
							   タリフ距離 = m08.タリフ距離,
							   郵便番号 = m08.郵便番号,
							   住所１ = m08.住所１,
							   住所２ = m08.住所２,
							   電話番号 = m08.電話番号,
							   ＦＡＸ番号 = m08.ＦＡＸ番号,
							   配送エリアID = m08.配送エリアID,
							   削除日付 = m08.削除日付,
						   }).ToList();

				return ret;
			}
		}

        /// <summary>
        /// M08_TIKの更新
        /// </summary>
        /// <param name="m08tik">M08_TIK_Member</param>
        public int Update(int p発着地ID, string p発着地名, string pかな読み, int? pタリフ距離, string p郵便番号, string p住所１, string p住所２, string p電話番号, string pＦＡＸ番号, int? p配送エリアID, bool pMaintenanceFlg, bool pGetNextNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                if (pGetNextNumber)
                {
                    p発着地ID = GetNextNumber();
                }

                //更新行を特定
                var ret = from x in context.M08_TIK
                          where (x.発着地ID == p発着地ID)
                          orderby x.発着地ID
                          select x;
                var m08 = ret.FirstOrDefault();

                if (m08 == null)
                {
                    M08_TIK m08in = new M08_TIK();
                    m08in.発着地ID = p発着地ID;
                    m08in.登録日時 = DateTime.Now;
                    m08in.更新日時 = DateTime.Now;
                    m08in.発着地名 = p発着地名;
                    m08in.かな読み = pかな読み;
                    m08in.タリフ距離 = pタリフ距離;
                    m08in.郵便番号 = p郵便番号;
                    m08in.住所１ = p住所１;
                    m08in.住所２ = p住所２;
                    m08in.電話番号 = p電話番号;
                    m08in.ＦＡＸ番号 = pＦＡＸ番号;
                    m08in.配送エリアID = p配送エリアID;
                    m08in.削除日付 = null;
                    context.M08_TIK.ApplyChanges(m08in);
                }
                else
                {
                    if (pMaintenanceFlg)
                    {
                        return -1;
                    }
                    
                    m08.更新日時 = DateTime.Now;
                    m08.発着地名 = p発着地名;
                    m08.かな読み = pかな読み;
                    m08.タリフ距離 = pタリフ距離;
                    m08.郵便番号 = p郵便番号;
                    m08.住所１ = p住所１;
                    m08.住所２ = p住所２;
                    m08.電話番号 = p電話番号;
                    m08.ＦＡＸ番号 = pＦＡＸ番号;
                    m08.配送エリアID = p配送エリアID;
                    m08.削除日付 = null;
                    m08.AcceptChanges();
                }

                context.SaveChanges();
            }
            return 1;

        }

        /// <summary>
        /// M08_TIKの削除
        /// </summary>
        /// <param name="m08tik">M08_TIK_Member</param>
        public void Delete(int p発着地ID)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M08_TIK
                          where (x.発着地ID == p発着地ID)
                          orderby x.発着地ID
                          select x;
                var m08 = ret.FirstOrDefault();
                if (m08 != null)
                {
                    m08.削除日付 = DateTime.Now;
                    m08.AcceptChanges();
                }
                context.SaveChanges();
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
                var query = from x in context.M08_TIK
                            select x.発着地ID;

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
        /// M08_TIKの検索データ取得
        /// </summary>
        /// <param name="p発着地ID">発着地ID</param>
        /// <returns>M08_TIK_Member</returns>
        public List<M08_TIK_Search_Member> GetSearchData(int? p発着地ID, int pOptiion)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m08 in context.M08_TIK
                           where m08.削除日付 == null
                           select new M08_TIK_Search_Member
                           {
                               発着地ID = m08.発着地ID,
                               発着地名 = m08.発着地名,
                               かな読み = m08.かな読み,
                           }).AsQueryable();

                if (p発着地ID != null)
                {
                    if (pOptiion == 0)
                    {
                        if (-1 != p発着地ID)
                        {
                            ret = ret.Where(c => c.発着地ID == p発着地ID);
                        }
                    }
                }


                return ret.ToList();
            }
        }

        /// <summary>
        /// 発着地マスタ一覧検索データ取得
        /// </summary>
        /// <returns></returns>
        public List<M08_TIK_Member> GetSearchDataForList(string 発着地コードFROM, string 発着地コードTO, int[] i発着地IDList, string 指定エリアコード, string 表示方法)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();



                var ret = (from m08 in context.M08_TIK
                           where m08.削除日付 == null
                           select new M08_TIK_Member
                           {
                               発着地ID = m08.発着地ID,
                               登録日時 = m08.登録日時,
                               更新日時 = m08.更新日時,
                               発着地名 = m08.発着地名,
                               かな読み = m08.かな読み,
                               タリフ距離 = m08.タリフ距離,
                               郵便番号 = m08.郵便番号,
                               住所１ = m08.住所１,
                               住所２ = m08.住所２,
                               電話番号 = m08.電話番号,
                               ＦＡＸ番号 = m08.ＦＡＸ番号,
                               配送エリアID = m08.配送エリアID,
                           }).AsQueryable();

                if (!(string.IsNullOrEmpty(発着地コードFROM + 発着地コードTO + 指定エリアコード) && i発着地IDList.Length == 0))
                {

                    if (string.IsNullOrEmpty(発着地コードFROM + 発着地コードTO))
                    {

                        ret = ret.Where(c => c.発着地ID >= int.MaxValue);
                    }
                    if (!string.IsNullOrEmpty(発着地コードFROM))
                    {
                        int i発着地コードFROM = AppCommon.IntParse(発着地コードFROM);
                        ret = ret.Where(c => c.発着地ID >= i発着地コードFROM);
                    }
                    if (!string.IsNullOrEmpty(発着地コードTO))
                    {
                        int i発着地コードTO = AppCommon.IntParse(発着地コードTO);
                        ret = ret.Where(c => c.発着地ID <= i発着地コードTO);
                    }

                    if (!string.IsNullOrEmpty(指定エリアコード))
                    {
                        int i指定エリアコード = AppCommon.IntParse(指定エリアコード);
                        ret = ret.Union(from m08 in context.M08_TIK
                                        where m08.削除日付 == null && m08.配送エリアID == i指定エリアコード
                                        select new M08_TIK_Member
                                        {
                                            発着地ID = m08.発着地ID,
                                            登録日時 = m08.登録日時,
                                            更新日時 = m08.更新日時,
                                            発着地名 = m08.発着地名,
                                            かな読み = m08.かな読み,
                                            タリフ距離 = m08.タリフ距離,
                                            郵便番号 = m08.郵便番号,
                                            住所１ = m08.住所１,
                                            住所２ = m08.住所２,
                                            電話番号 = m08.電話番号,
                                            ＦＡＸ番号 = m08.ＦＡＸ番号,
                                            配送エリアID = m08.配送エリアID,
                                        });
                    }

                    if (i発着地IDList.Length > 0)
                    {
                        var intCause = i発着地IDList;

                        ret = ret.Union(from m08 in context.M08_TIK
                                        where m08.削除日付 == null && intCause.Contains(m08.発着地ID)
                                        select new M08_TIK_Member
                                        {
                                            発着地ID = m08.発着地ID,
                                            登録日時 = m08.登録日時,
                                            更新日時 = m08.更新日時,
                                            発着地名 = m08.発着地名,
                                            かな読み = m08.かな読み,
                                            タリフ距離 = m08.タリフ距離,
                                            郵便番号 = m08.郵便番号,
                                            住所１ = m08.住所１,
                                            住所２ = m08.住所２,
                                            電話番号 = m08.電話番号,
                                            ＦＡＸ番号 = m08.ＦＡＸ番号,
                                            配送エリアID = m08.配送エリアID,
                                        });
                    }

                }

                ret = ret.Distinct();

                if (表示方法 == "0")
                {
                    ret = ret.OrderBy(c => c.発着地ID);
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
