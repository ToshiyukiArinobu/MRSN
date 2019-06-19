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
using System.Web.UI.WebControls;
using System.Data.Objects.SqlClient;

namespace KyoeiSystem.Application.WCFService
{
    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コード、svc、および config ファイルで同時にクラス名 "DataDriveLogService" を変更できます。
    // 注意: このサービスをテストするために WCF テスト クライアントを起動するには、ソリューション エクスプローラーで DataDriveLogService.svc または DataDriveLogService.svc.cs を選択し、デバッグを開始してください。
    public class SRY21010
    {
        /// <summary>
        /// SRY21010  印刷　メンバー
        /// </summary>
        [DataContract]
        public class SRY21010_Member
		{
			public int 順序 { get; set; }
			public string 点検内容 { get; set; }
			public DateTime 次回車検日 { get; set; }
			public string str次回車検日 { get; set; }
			public int? 車輌コード { get; set; }
            public string 車輌番号 { get; set; }
            public string 車輌登録番号 { get; set; }
            public string 車種名 { get; set; }
            public string 主乗務員 { get; set; }
            public int? 初年度登録年 { get; set; }
            public int? 初年度登録月 { get; set; }
            public int? 自社部門ID { get; set; }
            public string 自社部門名 { get; set; }
            public string 車名 { get; set; }
            public string 型式 { get; set; }
            public string 車台番号 { get; set; }
            public string ディーラー { get; set; }
            public DateTime? 自賠責発効日 { get; set; }
            public string 備考 { get; set; }
            public string 集計表示年 { get; set; }
            public string 集計表示月 { get; set; }
            public string 車輌From { get; set; }
            public string 車輌To { get; set; }
            public string 車輌ピックアップ{ get; set; }
			public int 車輌ID { get; set; }
		}

        #region 得意先売上日計表プレビュー
        /// <summary>
        /// SRY21010 車検予定管理表プレビュー
        /// </summary>
        /// <param name="p商品ID">車輌コード</param>
        /// <returns>S02</returns>
        public List<SRY21010_Member> SEARCH_SRY21010_GetDataList(string p車輌From, string p車輌To, int?[] i車輌List, string p作成年, string p作成月, DateTime p集計期間To, int? p自社部門コード, string 部門名)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                List<SRY21010_Member> retList = new List<SRY21010_Member>();
                context.Connection.Open();
                //支払先指定　表示用
                string 車輌指定表示 = string.Empty;

				DateTime 点検3か月 = p集計期間To.AddMonths(9);
				DateTime 点検6か月 = p集計期間To.AddMonths(6);
				DateTime 点検9か月 = p集計期間To.AddMonths(3);

				var query = (from m05 in context.M05_CAR
							 join m06 in context.M06_SYA on m05.車種ID equals m06.車種ID into m06Group
							 from m06g in m06Group.DefaultIfEmpty()
							 join m04 in context.M04_DRV on m05.乗務員KEY equals m04.乗務員KEY into m04Group
							 from m04g in m04Group.DefaultIfEmpty()
							 join m05cdt2 in context.M05_CDT2 on m05.車輌KEY equals m05cdt2.車輌KEY into m05cdt2Group
							 from m05cdt2g in m05cdt2Group.DefaultIfEmpty()
							 join m71 in context.M71_BUM on m05.自社部門ID equals m71.自社部門ID into m71Group
							 from m71g in m71Group.DefaultIfEmpty()
							 where (m05.次回車検日 != null && m05.削除日付 == null) && (p自社部門コード == null || m05.自社部門ID == p自社部門コード)
							 //where m05.次回車検日 != null ? m05.次回車検日 ==  m05.次回車検日 <= p集計期間To && m05.削除日付 == null
							 orderby m05.次回車検日
							 select new SRY21010_Member
							{
								次回車検日 = (DateTime)m05.次回車検日,
								ディーラー = m05cdt2g.契約先,
								型式 = m05.型式,
								str次回車検日 = SqlFunctions.StringConvert((decimal)(((DateTime)m05.次回車検日).Year)).Trim() + SqlFunctions.StringConvert((decimal)(((DateTime)m05.次回車検日).Month)).Trim(),
								//次回車検日 = (DateTime)m05.次回車検日,
								自社部門ID = m05.自社部門ID,
								自社部門名 = m71g.自社部門名,
								自賠責発効日 = m05cdt2Group.Max(cdt2 => cdt2.加入年月日),
								車種名 = m06g.車種名,
								車台番号 = m05.車台番号,
								車名 = m05.車名,
								車輌From = p車輌From,
								車輌To = p車輌To,
								車輌コード = m05.車輌ID,
								車輌ピックアップ = 車輌指定表示,
								車輌登録番号 = m05.車輌登録番号,
								車輌番号 = m05.車輌番号,
								主乗務員 = m04g.乗務員名,
								集計表示年 = p作成年,
								集計表示月 = p作成月,
								順序 = 0,
								初年度登録年 = m05.初年度登録年,
								初年度登録月 = m05.初年度登録月,
								点検内容 = "",
								備考 = m05.備考,
								車輌ID = m05.車輌ID,
							}).ToList();
				//			 str次回車検日 = SqlFunctions.StringConvert((decimal)(q.次回車検日.Year)) + SqlFunctions.StringConvert((decimal)(q.次回車検日.Month)),
				//			 ディーラー = q.ディーラー,
				//			 型式 = q.型式,
				//			 次回車検日 = q.次回車検日,
				//			 自社部門ID = q.自社部門ID,
				//			 自社部門名 = q.自社部門名,
				//			 自賠責発効日 = q.自賠責発効日,
				//			 車種名 = q.車種名,
				//			 車台番号 = q.車台番号,
				//			 車名 = q.車名,
				//			 車輌From = q.車輌From,
				//			 車輌To = q.車輌To,
				//			 車輌コード = q.車輌コード,
				//			 車輌ピックアップ = q.車輌ピックアップ,
				//			 車輌登録番号 = q.車輌登録番号,
				//			 車輌番号 = q.車輌番号,
				//			 主乗務員 = q.主乗務員,
				//			 集計表示月 = q.集計表示月,
				//			 集計表示年 = q.集計表示年,
				//			 順序 = 0,
				//			 初年度登録月 = q.初年度登録月,
				//			 初年度登録年 = q.初年度登録年,
				//			 点検内容 = "",
				//			 備考 = q.備考,
				//			 車輌ID = q.車輌ID,

				//		 }).AsQueryable();

				var query2 = (from q in query
							  where q.str次回車検日 == ((p集計期間To.Year).ToString() + (p集計期間To.Month).ToString())
							  select new SRY21010_Member
							  {
								  str次回車検日 = q.str次回車検日,
								  ディーラー = q.ディーラー,
								  型式 = q.型式,
								  次回車検日 = q.次回車検日,
								  自社部門ID = q.自社部門ID,
								  自社部門名 = q.自社部門名,
								  自賠責発効日 = q.自賠責発効日,
								  車種名 = q.車種名,
								  車台番号 = q.車台番号,
								  車名 = q.車名,
								  車輌From = q.車輌From,
								  車輌To = q.車輌To,
								  車輌コード = q.車輌コード,
								  車輌ピックアップ = q.車輌ピックアップ,
								  車輌登録番号 = q.車輌登録番号,
								  車輌番号 = q.車輌番号,
								  主乗務員 = q.主乗務員,
								  集計表示月 = q.集計表示月,
								  集計表示年 = q.集計表示年,
								  順序 = 1,
								  初年度登録月 = q.初年度登録月,
								  初年度登録年 = q.初年度登録年,
								  点検内容 = "当月車検車輌",
								  備考 = q.備考,
								  車輌ID = q.車輌ID,

							  }).AsQueryable();

				query2 = query2.Union(from q in query
									  where q.str次回車検日 == ((点検3か月.Year).ToString() + (点検3か月.Month).ToString())
									  select new SRY21010_Member
									  {
										  str次回車検日 = q.str次回車検日,
										  ディーラー = q.ディーラー,
										  型式 = q.型式,
										  次回車検日 = q.次回車検日,
										  自社部門ID = q.自社部門ID,
										  自社部門名 = q.自社部門名,
										  自賠責発効日 = q.自賠責発効日,
										  車種名 = q.車種名,
										  車台番号 = q.車台番号,
										  車名 = q.車名,
										  車輌From = q.車輌From,
										  車輌To = q.車輌To,
										  車輌コード = q.車輌コード,
										  車輌ピックアップ = q.車輌ピックアップ,
										  車輌登録番号 = q.車輌登録番号,
										  車輌番号 = q.車輌番号,
										  主乗務員 = q.主乗務員,
										  集計表示月 = q.集計表示月,
										  集計表示年 = q.集計表示年,
										  順序 = 2,
										  初年度登録月 = q.初年度登録月,
										  初年度登録年 = q.初年度登録年,
										  点検内容 = "3か月点検",
										  備考 = q.備考,
										  車輌ID = q.車輌ID,
									  }).AsQueryable();

				query2 = query2.Union(from q in query
									  where q.str次回車検日 == ((点検6か月.Year).ToString() + (点検6か月.Month).ToString())
									  select new SRY21010_Member
									  {
										  str次回車検日 = q.str次回車検日,
										  ディーラー = q.ディーラー,
										  型式 = q.型式,
										  次回車検日 = q.次回車検日,
										  自社部門ID = q.自社部門ID,
										  自社部門名 = q.自社部門名,
										  自賠責発効日 = q.自賠責発効日,
										  車種名 = q.車種名,
										  車台番号 = q.車台番号,
										  車名 = q.車名,
										  車輌From = q.車輌From,
										  車輌To = q.車輌To,
										  車輌コード = q.車輌コード,
										  車輌ピックアップ = q.車輌ピックアップ,
										  車輌登録番号 = q.車輌登録番号,
										  車輌番号 = q.車輌番号,
										  主乗務員 = q.主乗務員,
										  集計表示月 = q.集計表示月,
										  集計表示年 = q.集計表示年,
										  順序 = 3,
										  初年度登録月 = q.初年度登録月,
										  初年度登録年 = q.初年度登録年,
										  点検内容 = "6か月点検",
										  備考 = q.備考,
										  車輌ID = q.車輌ID,
									  }).AsQueryable();

				query2 = query2.Union(from q in query
									  where q.str次回車検日 == ((点検9か月.Year).ToString() + (点検9か月.Month).ToString())
									  select new SRY21010_Member
									  {
										  str次回車検日 = q.str次回車検日,
										  ディーラー = q.ディーラー,
										  型式 = q.型式,
										  次回車検日 = q.次回車検日,
										  自社部門ID = q.自社部門ID,
										  自社部門名 = q.自社部門名,
										  自賠責発効日 = q.自賠責発効日,
										  車種名 = q.車種名,
										  車台番号 = q.車台番号,
										  車名 = q.車名,
										  車輌From = q.車輌From,
										  車輌To = q.車輌To,
										  車輌コード = q.車輌コード,
										  車輌ピックアップ = q.車輌ピックアップ,
										  車輌登録番号 = q.車輌登録番号,
										  車輌番号 = q.車輌番号,
										  主乗務員 = q.主乗務員,
										  集計表示月 = q.集計表示月,
										  集計表示年 = q.集計表示年,
										  順序 = 4,
										  初年度登録月 = q.初年度登録月,
										  初年度登録年 = q.初年度登録年,
										  点検内容 = "9か月点検",
										  備考 = q.備考,
										  車輌ID = q.車輌ID,
									  }).AsQueryable();

				query2 = query2.Distinct();
	
				var query3 = query2;


                if (!(string.IsNullOrEmpty(p車輌From + p車輌To) && i車輌List.Length == 0))
                {

                    //車輌コードが検索対象に入っていない時全データ取得
                    if (string.IsNullOrEmpty(p車輌From + p車輌To))
                    {
                        query2 = query2.Where(c => c.車輌コード >= int.MaxValue);
                    }

                    //車輌From処理　Min値
                    if (!string.IsNullOrEmpty(p車輌From))
                    {
                        int? i車輌FROM = AppCommon.IntParse(p車輌From);
                        query2 = query2.Where(c => c.車輌コード >= i車輌FROM);
                    }

                    //車輌To処理　Max値
                    if (!string.IsNullOrEmpty(p車輌To))
                    {
                        int? i車輌TO = AppCommon.IntParse(p車輌To);
                        query2 = query2.Where(c => c.車輌コード <= i車輌TO);
                    }


                    if (i車輌List.Length > 0)
                    {
                        var intCause = i車輌List;
						query2 = query2.Union(from q in query3
                                            where intCause.Contains(q.車輌ID)
                                            select new SRY21010_Member
                                            {
												str次回車検日 = q.str次回車検日,
												ディーラー = q.ディーラー,
												型式 = q.型式,
												次回車検日 = q.次回車検日,
												自社部門ID = q.自社部門ID,
												自社部門名 = q.自社部門名,
												自賠責発効日 = q.自賠責発効日,
												車種名 = q.車種名,
												車台番号 = q.車台番号,
												車名 = q.車名,
												車輌From = q.車輌From,
												車輌To = q.車輌To,
												車輌コード = q.車輌コード,
												車輌ピックアップ = q.車輌ピックアップ,
												車輌登録番号 = q.車輌登録番号,
												車輌番号 = q.車輌番号,
												主乗務員 = q.主乗務員,
												集計表示月 = q.集計表示月,
												集計表示年 = q.集計表示年,
												順序 = q.順序,
												初年度登録月 = q.初年度登録月,
												初年度登録年 = q.初年度登録年,
												点検内容 = q.点検内容,
												備考 = q.備考,
												車輌ID = q.車輌ID,
											});

                    }
                }

				query2 = query2.Distinct();


				//else
				//{
				//	//車輌範囲の指定が空の場合の処理

				//	if (string.IsNullOrEmpty(p車輌From) && string.IsNullOrEmpty(p車輌To))
				//	{
				//		query = query.Where(c => c.車輌コード >= int.MinValue && c.車輌コード <= int.MaxValue);
				//	}

				//	if (string.IsNullOrEmpty(p車輌From))
				//	{
				//		query = query.Where(c => c.車輌コード >= int.MinValue);
				//	}

				//	if (string.IsNullOrEmpty(p車輌To))
				//	{   
				//		query = query.Where(c => c.車輌コード <= int.MaxValue);
				//	}

				//	if (p自社部門コード != null)
				//	{
				//		query = query.Where(c => c.自社部門ID == p自社部門コード);
				//	}
				//}

                //得意先指定の表示
				if (i車輌List.Length > 0)
				{
					for (int i = 0; i < query2.Count(); i++)
					{
						車輌指定表示 = 車輌指定表示 + i車輌List[i].ToString();

						if (i < i車輌List.Length)
						{
							if (i == i車輌List.Length - 1)
							{
								break;
							}
							車輌指定表示 = 車輌指定表示 + ",";
						}
						if (i車輌List.Length == 1)
						{
							break;
						}
					}
				}

				query2 = query2.OrderBy(q => q.順序).ThenBy(q => q.車輌ID);
                return query2.ToList();
            }
        }
        #endregion
    }
}
