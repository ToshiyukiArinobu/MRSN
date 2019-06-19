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
    public class M11
    {

        /// <summary>
        /// 摘要マスタ 表示項目定義クラス
        /// </summary>
        public class M11_TEK_Member
        {
            [DataMember]
            public int 摘要ID { get; set; }
            [DataMember]
            public string 摘要名 { get; set; }
            [DataMember]
            public string かな読み { get; set; }
            [DataMember]
            public DateTime? 登録日時 { get; set; }
            [DataMember]
            public DateTime? 更新日時 { get; set; }
            [DataMember]
            public DateTime? 削除日時 { get; set; }
        }

        /// <summary>
        /// 摘要マスタリスト 表示項目定義クラス
        /// </summary>
        public class M11_TEK_Search_Member
        {
            public int 摘要ID { get; set; }
            [DataMember]
            public string 摘要名 { get; set; }
            [DataMember]
            public string かな読み { get; set; }
        }

		/// <summary>
		/// M11_TEKのデータ取得
		/// </summary>
		/// <param name="p摘要ID">摘要ID</param>
		/// <returns>M11_TEK_Member</returns>
		public List<M11_TEK_Member> GetData(int? p摘要ID, int pOptiion)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

                var ret = (from m11 in context.M11_TEK
                           //where m11.削除日時 == null
						   select new M11_TEK_Member
						   {
                               摘要ID = m11.摘要ID,
                               登録日時 = m11.登録日時,
                               更新日時 = m11.最終更新日時,
                               摘要名 = m11.摘要名,
                               かな読み = m11.かな読み,
                               削除日時 = m11.削除日時,
						   }).AsQueryable();

				//データが1件もない状態で<< < > >>を押された時の処理
                if ((p摘要ID == null || p摘要ID == 0) && ret.Where(c => c.削除日時 == null).Count() == 0)
				{
					return null;
				}

				if (p摘要ID != null)
				{
					if (pOptiion == 0)
					{
						if (-1 != p摘要ID)
						{
							ret = ret.Where(c => c.摘要ID == p摘要ID);
						}
					}
					else if (pOptiion > 0)
					{
						//p摘要IDの1つ後のIDを取得
                        ret = ret.Where(c => c.削除日時 == null);
						ret = ret.Where(c => c.摘要ID > p摘要ID);
						ret = ret.OrderBy(c => c.摘要ID);
					}
					else if (pOptiion < 0)
					{
						//p摘要IDの1つ前のIDを取得
                        ret = ret.Where(c => c.削除日時 == null);
						ret = ret.Where(c => c.摘要ID < p摘要ID);
						ret = ret.OrderByDescending(c => c.摘要ID);
					}
				}
				else
				{
					if (pOptiion == 0)
					{
						//摘要IDの先頭のIDを取得
						ret = ret.Where(c => c.削除日時 == null);
						ret = ret.OrderBy(c => c.摘要ID);
					}
					else if (pOptiion == 1)
					{
						//摘要IDの最後のIDを取得
						ret = ret.Where(c => c.削除日時 == null);
						ret = ret.OrderByDescending(c => c.摘要ID);
					}
				}


				return ret.ToList();
			}
		}

		/// <summary>
		/// M11_TEKのデータ取得
		/// </summary>
		/// <param name="p摘要ID">摘要ID</param>
		/// <returns>M11_TEK_Member</returns>
		public List<M11_TEK_Member> RGetData(int? p摘要ID)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

                var ret = (from m11 in context.M11_TEK
                           where m11.摘要ID == p摘要ID
                           where m11.削除日時 == null
						   select new M11_TEK_Member
						   {
                               摘要ID = m11.摘要ID,
                               登録日時 = m11.登録日時,
                               更新日時 = m11.最終更新日時,
                               摘要名 = m11.摘要名,
                               かな読み = m11.かな読み,
                               削除日時 = m11.削除日時,
						   }).AsQueryable();

				return ret.ToList();
			}
		}

        /// <summary>
        /// M11_TEKの更新
        /// </summary>
        /// <param name="m11tek">M11_TEK_Member</param>
        public int Update(int p摘要ID, string p摘要名, string pかな読み,int loginUserId, bool pMaintenanceFlg, bool pGetNextNumber)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                if (pGetNextNumber)
                {
                    p摘要ID = GetNextNumber();
                }

                //更新行を特定
                var ret = from x in context.M11_TEK
                          where (x.摘要ID == p摘要ID)
                          orderby x.摘要ID
                          select x;
                var m11 = ret.FirstOrDefault();

                if (m11 == null)
                {
                    M11_TEK m11tek = new M11_TEK();
                    m11tek.摘要ID = p摘要ID;
                    m11tek.登録日時 = DateTime.Now;
                    m11tek.最終更新日時 = DateTime.Now;
                    m11tek.かな読み = pかな読み;
                    m11tek.摘要名 = p摘要名;
                    m11tek.登録者 = loginUserId;
                    m11tek.最終更新者 = loginUserId;
                    m11tek.削除者 = null;
                    m11tek.削除日時 = null;
                    context.M11_TEK.ApplyChanges(m11tek);
                }
                else
                {
                    if (pMaintenanceFlg)
                    {
                        return -1;
                    }

                    m11.最終更新者 = loginUserId;
                    m11.最終更新日時 = DateTime.Now;
                    m11.かな読み = pかな読み;
                    m11.摘要名 = p摘要名;
                    m11.削除日時 = null;
                    m11.削除者 = null;
                    m11.AcceptChanges();
                }

                context.SaveChanges();
            }
            return 1;
        }

        /// <summary>
        /// M11_TEKの削除
        /// </summary>
        /// <param name="m11tik">M11_TEK_Member</param>
        public void Delete(int p摘要ID,int loginUserId)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //削除行を特定
                var ret = from x in context.M11_TEK
                          where (x.摘要ID == p摘要ID)
                          orderby x.摘要ID
                          select x;
                var m11 = ret.FirstOrDefault();
                if (m11 != null)
                {
                    m11.削除者 = loginUserId;
                    m11.削除日時 = DateTime.Now;
                    m11.AcceptChanges();
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// M11_TEKのID自動採番
        /// </summary>
        /// <returns></returns>
        public int GetNextNumber()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //最大ID行を特定
                var query = from x in context.M11_TEK
                            select x.摘要ID;

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
        /// M11_TEKの検索データ取得
        /// </summary>
        /// <param name="p摘要ID">摘要ID</param>
        /// <returns>M11_TEK_Member</returns>
        public List<M11_TEK_Search_Member> GetSearchData(int? p摘要ID, int pOptiion)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from m11 in context.M11_TEK
                           where m11.削除日時 == null
                           select new M11_TEK_Search_Member
                           {
                               摘要ID = m11.摘要ID,
                               摘要名 = m11.摘要名,
                               かな読み = m11.かな読み,
                           }).AsQueryable();

                if (p摘要ID != null)
                {
                    if (pOptiion == 0)
                    {
                        if (-1 != p摘要ID)
                        {
                            ret = ret.Where(c => c.摘要ID == p摘要ID);
                        }
                    }
                }


                return ret.ToList();
            }
        }

        /// <summary>
        /// 摘要マスタ一覧検索データ取得
        /// </summary>
        /// <returns></returns>
        public List<M11_TEK_Member> GetSearchDataForList(string 摘要コードFROM, string 摘要コードTO, string 摘要指定, string 表示方法)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();



                var ret = (from m11 in context.M11_TEK
                           where m11.削除日時 == null
                           select new M11_TEK_Member
                           {
                               摘要ID = m11.摘要ID,
                               登録日時 = m11.登録日時,
                               更新日時 = m11.最終更新日時,
                               摘要名 = m11.摘要名,
                               かな読み = m11.かな読み,
                           }).AsQueryable();

                if (!(string.IsNullOrEmpty(摘要コードFROM + 摘要コードTO) && string.IsNullOrEmpty(摘要指定)))
                {

                    //if (string.IsNullOrEmpty(摘要コードFROM + 摘要コードTO))
                    //{

                    //    ret = ret.Where(c => c.摘要ID >= int.MaxValue);
                    //}
                    if (!string.IsNullOrEmpty(摘要コードFROM))
                    {
                        int i摘要コードFROM = AppCommon.IntParse(摘要コードFROM);
                        ret = ret.Where(c => c.摘要ID >= i摘要コードFROM);
                    }
                    if (!string.IsNullOrEmpty(摘要コードTO))
                    {
                        int i摘要コードTO = AppCommon.IntParse(摘要コードTO);
                        ret = ret.Where(c => c.摘要ID <= i摘要コードTO);
                    }


                    if (!string.IsNullOrEmpty(摘要指定))
                    {
                        ret = ret.Where(c => c.摘要名.Contains(摘要指定));
                        //var intCause = i摘要IDList;

                        //ret = ret.Union(from m11 in context.M11_TEK
                        //                where m11.削除日時 == null && intCause.Contains(m11.摘要ID)
                        //                select new M11_TEK_Member
                        //                {
                        //                    摘要ID = m11.摘要ID,
                        //                    登録日時 = m11.登録日時,
                        //                    更新日時 = m11.更新日時,
                        //                    摘要名 = m11.摘要名,
                        //                    かな読み = m11.かな読み,
                        //                });
                    }

                }

                ret = ret.Distinct();

                if (表示方法 == "0")
                {
                   ret = ret.OrderBy(c => c.摘要ID);
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
