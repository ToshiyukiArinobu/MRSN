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
using System.Data.Objects.SqlClient;


namespace KyoeiSystem.Application.WCFService
{
	public class Account
	{
        //// 権限関係追加
        //public class 権限
        //{
        //    public Boolean 使用可能FLG;
        //    public Boolean データ更新FLG;
        //}

		public class AccountData
		{
			public int 担当者ID { get; set; }
			public DateTime? 登録日時 { get; set; }
			public DateTime? 更新日時 { get; set; }
			public string 担当者名 { get; set; }
            public int 自社コード { get; set; }
            public int 自社販社区分 { get; set; }
			public string かな読み { get; set; }
			public string パスワード { get; set; }
			public int グループ権限ID { get; set; }
			public string 個人ナンバー { get; set; }
			public object 設定項目 { get; set; }
			public DateTime? 削除日付 { get; set; }
            //権限関係追加
            //public Dictionary<string, 権限> 権限List { get; set; }
			//public Dictionary<string, string> 権限List { get; set; }
            public int?[] タブグループ番号 { get; set; }
            public string[] プログラムID { get; set; }
            public Boolean[] 使用可能FLG { get; set; }
            public Boolean[] データ更新FLG { get; set; }
        }

        /// <summary>
        /// 担当者選択用
        /// </summary>
        public class M72_TNT_SELECT
        {
            public int 担当者ID { get; set; }
            public string 担当者名 { get; set; }
            public string 担当者表示区分 { get; set; }
        }

        // データメンバー定義
        [DataContract]
        public class M72_TNT_Member
        {
            [DataMember]
            public int 担当者ID { get; set; }
            [DataMember]
            public DateTime? 登録日時 { get; set; }
            [DataMember]
            public DateTime? 更新日時 { get; set; }
            [DataMember]
            public string 担当者名 { get; set; }
            [DataMember]
            public int 自社コード { get; set; }
            [DataMember]
            public int 自社販社区分 { get; set; }
            [DataMember]
            public string かな読み { get; set; }
            [DataMember]
            public string パスワード { get; set; }
            [DataMember]
            public int グループ権限ID { get; set; }
            [DataMember]
            public string 個人ナンバー { get; set; }
            [DataMember]
            public byte[] 設定項目 { get; set; }
            [DataMember]
            public DateTime? 削除日付 { get; set; }
        }

        //権限関係追加
        [DataContract]
        public class M74_AUTHORITY_Member
        {
            [DataMember]
            public int? タブグループ番号 { get; set; }
            [DataMember]
            public string プログラムID { get; set; }
            [DataMember]
            public Boolean 使用可能FLG { get; set; }
            [DataMember]
            public Boolean データ更新FLG { get; set; }
        }

		/// <summary>
		/// 担当者マスタ取得（ログイン用）
		/// </summary>
		/// <param name="p担当者ID"></param>
		/// <returns></returns>
		public List<AccountData> Login(int p担当者ID/*, Type type*/)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

				var ret = (from m72 in context.M72_TNT
                           from m70 in context.M70_JIS.Where( c => c.自社コード == m72.自社コード)
						   where m72.担当者ID == p担当者ID && m72.削除日時 == null
						   select new M72_TNT_Member
						   {
							   担当者ID = m72.担当者ID,
							   登録日時 = m72.登録日時,
							   更新日時 = m72.最終更新日時,
                               担当者名 = m72.担当者名,
                               自社コード = m72.自社コード,
                               自社販社区分 = m70.自社区分,
							   かな読み = m72.かな読み,
							   パスワード = m72.パスワード,
							   グループ権限ID = m72.グループ権限ID,
							   個人ナンバー = m72.個人ナンバー,
							   設定項目 = m72.設定項目,
							   削除日付 = m72.削除日時,
						   }).FirstOrDefault();
                
				if (ret == null)
                {
                    //データが該当しなければ【999999】をLogin.xamlに返す
                    AccountData MDate = new AccountData()
                    {
                        担当者ID = 999999,
                        // 権限関係追加(管理者以外)
                        グループ権限ID = 1,
                    };

                    // 権限関係追加(管理者グループ権限ID)
                    List<M74_AUTHORITY_Member> ret3;
                    ret3 = AuthorityData(0);
                    if (ret3 == null)
                    {
                    }
                    else
                    {
                        MDate.タブグループ番号 = ret3.Select(aut => aut.タブグループ番号).ToArray<int?>();
                        MDate.プログラムID = ret3.Select(aut => aut.プログラムID).ToArray<string>();
                        MDate.使用可能FLG = ret3.Select(aut => aut.使用可能FLG).ToArray<Boolean>();
                        MDate.データ更新FLG = ret3.Select(aut => aut.データ更新FLG).ToArray<Boolean>();
                    }

                    return new List<AccountData>() { MDate };


                    throw new Framework.Common.DBDataNotFoundException();
                }

                try
                {
                    AccountData data = new AccountData()
                    {
                        担当者ID = ret.担当者ID,
                        登録日時 = ret.登録日時,
                        更新日時 = ret.更新日時,
                        担当者名 = ret.担当者名,
                        自社コード = ret.自社コード,
                        自社販社区分 = ret.自社販社区分,
                        かな読み = ret.かな読み,
                        パスワード = ret.パスワード,
                        グループ権限ID = ret.グループ権限ID,
                        個人ナンバー = ret.個人ナンバー,
                        削除日付 = ret.削除日付,
                    };
					data.設定項目 = ret.設定項目;

                    // 権限関係追加
                    List<M74_AUTHORITY_Member> ret3;
                    ret3 = AuthorityData(data.グループ権限ID);
                    if (ret3 == null)
                    {
                    }
                    else
                    {
                        data.タブグループ番号 = ret3.Select(aut => aut.タブグループ番号).ToArray<int?>();
                        data.プログラムID = ret3.Select(aut => aut.プログラムID).ToArray<string>();
                        data.使用可能FLG = ret3.Select(aut => aut.使用可能FLG).ToArray<Boolean>();
                        data.データ更新FLG = ret3.Select(aut => aut.データ更新FLG).ToArray<Boolean>();
                    }

                    return new List<AccountData>() { data };
                }
                //catch (Exception ex)
                catch
                {
                    throw new Framework.Common.DBAccessException();
                }

			}
		}

		/// <summary>
		/// 権限マスタ取得（ログイン用）
		/// </summary>
		/// <param name="p担当者ID"></param>
		/// <returns></returns>
        public List<M74_AUTHORITY_Member> AuthorityData(int p権限ID/*, Type type*/)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                var ret = (from m74 in context.M74_KGRP
                            where m74.グループ権限ID == p権限ID
                            select new M74_AUTHORITY_Member
                            {
                                プログラムID = m74.プログラムID,
                                タブグループ番号 = m74.タブグループ番号,
                                使用可能FLG = m74.使用可能FLG == 1 ? true : false,
                                データ更新FLG = m74.データ更新FLG == 1 ? true : false,
                            }).ToList();
                return ret;
            }
        }

		/// <summary>
		/// 担当者マスタ更新（ユーザ設定項目のみ）
		/// </summary>
		/// <param name="p担当者ID"></param>
		/// <returns></returns>
		public void Logout(int p担当者ID ,  object p設定項目)
		{
			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				context.Connection.Open();

				using (var tran = new TransactionScope())
				{
					var tnt = (from m72 in context.M72_TNT
							   where m72.担当者ID == p担当者ID
								  && m72.削除日時 == null
							   select m72
							   ).FirstOrDefault();

					if (tnt == null)
					{
						throw new Framework.Common.DBDataNotFoundException();
					}

					try
					{
						if (p設定項目 != null)
						{
							if (p設定項目 is byte[])
							{
								tnt.設定項目 = p設定項目 as byte[];
							}
							else
							{
								string keyname = p設定項目.GetType().Name;
								System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(p設定項目.GetType());
								var strm = new System.IO.MemoryStream();
								serializer.Serialize(strm, p設定項目);
								tnt.設定項目 = strm.ToArray();
								strm.Close();
							}
						}
						else
						{
							tnt.設定項目 = null;
						}

						tnt.AcceptChanges();
						context.SaveChanges();
					}
					catch (Exception ex)
					{
						throw new Framework.Common.DBPutException("データ更新例外", ex);
					}

					tran.Complete();
				}
			}

			return;
		}

        /// <summary>
        /// 作業区分Combo用リスト取得
        /// </summary>
        /// <param name="eigyoushoCode"></param>
        /// <returns></returns>
        public List<M72_TNT_SELECT> Get_UserList()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {

                var combolist = (from x in context.M72_TNT
								 where x.削除日時 == null
                                 select new M72_TNT_SELECT
                                 {
                                     担当者ID = x.担当者ID,
                                     担当者表示区分 = x.担当者名 == null ? SqlFunctions.StringConvert((decimal)x.担当者ID, 8) : SqlFunctions.StringConvert((decimal)x.担当者ID, 8) + " " + x.担当者名,
                                     担当者名 = x.担当者名,
                                 }
                               ).ToList();
				if (combolist.Where(q => q.担当者ID != 99999).Any())
				{
                    //初期ユーザが存在すれば削除
                    if (combolist.Where(q => q.担当者ID == 99999).Any())
                    {
                        combolist.RemoveAll(q => q.担当者ID == 99999);
                    }
				}
                return combolist.ToList();
            }
        }


	}
}