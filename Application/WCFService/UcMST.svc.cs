using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.Objects;
using System.Data.Objects.SqlClient;
using System.Data;
using System.Data.Common;
using System.Transactions;
using KyoeiSystem.Framework.Common;

namespace KyoeiSystem.Application.WCFService
{
	public class UcMST
	{
        private const int 論理削除 = 9;

        //// データメンバー定義
		[DataContract]
		public class CodeTextInt_Member
		{
			[DataMember]
			public int コード { get; set; }
			[DataMember]
			public string 名称 { get; set; }
		}

        [DataContract]
        public class TwinCodeText_Member
        {
            [DataMember]
            public string コード { get; set; }
            [DataMember]
            public string サブコード { get; set; }
            [DataMember]
            public string 名称 { get; set; }
            [DataMember]
            public int Ｓ支払消費税区分 { get; set; }
            [DataMember]
            public int Ｓ税区分ID { get; set; }
            [DataMember]
            public int Ｔ消費税区分 { get; set; }
            [DataMember]
            public int Ｔ税区分ID { get; set; }
        }
       
		[DataContract]
		public class CodeTextString_Member
		{
			[DataMember]
			public string コード { get; set; }
			[DataMember]
			public string 名称 { get; set; }
            [DataMember]
            public string 締日 { get; set; }
            [DataMember]
            public string 集金日 { get; set; }
            [DataMember]
            public string サイト { get; set; }
		}

		public class Combobox_List_Member
		{
			public int コード { get; set; }
			public int 表示順 { get; set; }
			public string 表示名 { get; set; }
		}

        [DataContract]
        public class CodeTextAll_Member
        {
            [DataMember]
            public int コード { get; set; }
            [DataMember]
            public string 名称 { get; set; }
            [DataMember]
            public int 締日 { get; set; }
            [DataMember]
            public int 集金日 { get; set; }
            [DataMember]
            public int サイト { get; set; }
        }

        public class M01_TOK_SearchMember
        {
            public string コード { get; set; }
            public string 名称 { get; set; }
            public int Ｓ支払消費税区分 { get; set; }
            public int Ｓ税区分ID { get; set; }
        }

        public class M10_TOKHIN_Extension : M09_HIN
        {
            // REMARKS:不足項目のみ定義
            public string 得意先品番コード { get; set; }
            public string 色名称 { get; set; } 

        }

		public List<Combobox_List_Member> GetComboboxList(string param)
		{
			List<Combobox_List_Member> result = new List<Combobox_List_Member>();

			using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				//int idx = 0;
				context.Connection.Open();
				switch (param)
				{
                    //case "@SYK":
                    //    result.AddRange(
                    //        (from mst in context.M78_SYK
                    //         orderby mst.出勤区分ID
                    //         select new Combobox_List_Member
                    //         {
                    //             コード = mst.出勤区分ID,
                    //             表示順 = 0,
                    //             表示名 = mst.出勤区分名,
                    //         }).ToList()
                    //        );
                    //    foreach (var rec in result)
                    //    {
                    //        rec.表示順 = idx++;
                    //    }
                    //    break;
                    //case "@BUMON":
                    //    // 先頭は全件対象とするための固定値
                    //    result.Add(new Combobox_List_Member
                    //    {
                    //        コード = 0,
                    //        表示順 = 0,
                    //        表示名 = "(全部門検索)",
                    //    }
                    //        );
                    //    result.AddRange(
                    //        (from mst in context.M71_BUM
                    //         orderby mst.自社部門ID
                    //         select new Combobox_List_Member
                    //         {
                    //             コード = mst.自社部門ID,
                    //             表示順 = 0,
                    //             表示名 = mst.自社部門名,
                    //         }).ToList()
                    //        );
                    //    foreach (var rec in result)
                    //    {
                    //        rec.表示順 = idx++;
                    //    }
                    //    break;
                    //case "@HENDO":
                    //    // 先頭は全件対象とするための固定値
                    //    result.Add(new Combobox_List_Member
                    //    {
                    //        コード = 0,
                    //        表示順 = 1,
                    //        表示名 = "(経費項目指定)",
                    //    }
                    //        );
                    //    result.AddRange(
                    //        (from mst in context.M07_KEI
                    //         where mst.固定変動区分 == 1
                    //         orderby mst.経費項目ID
                    //         select new Combobox_List_Member
                    //         {
                    //             コード = mst.経費項目ID,
                    //             表示順 = 0,
                    //             表示名 = mst.経費項目名,
                    //         }).ToList()
                    //        );
                    //    foreach (var rec in result)
                    //    {
                    //        rec.表示順 = idx++;
                    //    }
                    //    break;
                    //case "@SYASYU":
                    //    // 先頭は全件対象とするための固定値
                    //    result.Add(new Combobox_List_Member
                    //    {
                    //        コード = 0,
                    //        表示順 = 0,
                    //        表示名 = "(全車種検索)",
                    //    }
                    //        );
                    //    result.AddRange(
                    //        (from mst in context.M06_SYA
                    //         orderby mst.車種ID
                    //         select new Combobox_List_Member
                    //         {
                    //             コード = mst.車種ID,
                    //             表示順 = 0,
                    //             表示名 = mst.車種名,
                    //         }).ToList()
                    //        );
                    //    foreach (var rec in result)
                    //    {
                    //        rec.表示順 = idx++;
                    //    }
                    //    break;

                    default:
                        break;

                }

            }

			return result;
		}


		public List<CodeTextString_Member> GetDataMasterTable(string pコード, string DataAccessName, object LinkItem)
		{
			List<CodeTextString_Member> Member = new List<CodeTextString_Member>();
			int Code = 0;
            int LinkCode = 0;

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();


                switch (DataAccessName)
                {
                    case "M01_TOK__":     // 取引先マスタ
                        // TODO:コード、枝番、取引区分のパラメータが必要になる為、ココでの取得は不可。
                        //      また、名称はTwinTextのLabel2に設定したい為これも処理ができない。
                        LinkItem = LinkItem == null ? "" : LinkItem;
                        List<int> targetKbn = new List<int>();
                        foreach (string str in LinkItem.ToString().Split(','))
                            targetKbn.Add(int.Parse(str));

                        if (int.TryParse(pコード, out Code))
                        {
                            Member =
                                context.M01_TOK.Where(x => x.削除日時 == null && x.取引先コード == Code && targetKbn.Contains(x.取引区分))
                                    .Select(s => new CodeTextString_Member
                                    {
                                        コード = SqlFunctions.StringConvert((double)s.取引先コード),
                                        名称 = s.得意先名１
                                    }).ToList();
                        }
                        break;

                    case "M06_IRO":     // 色マスタ
                        Member =
                            context.M06_IRO.Where(x => x.削除日時 == null && x.色コード == pコード)
                                .Select(s => new CodeTextString_Member
                                    {
                                        コード = s.色コード,
                                        名称 = s.色名称
                                    })
                                .ToList();
                        break;

                    case "M09_HIN":    // 商品マスタ
                        if (int.TryParse(pコード, out Code))
                        {
                            Member =
                                context.M09_HIN.Where(x => x.削除日時 == null && x.品番コード == Code)
                                    .Select(c => new CodeTextString_Member
                                    {
                                        コード = SqlFunctions.StringConvert((double)c.品番コード),
                                        名称 = c.自社品名
                                    }).ToList();
                        }
                        break;

                    case "M09_MYHIN":   // 商品マスタ(自社品番)
                        Member =
                            context.M09_HIN.Where(x => x.削除日時 == null && x.自社品番 == pコード)
                                .Select(c => new CodeTextString_Member
                                {
                                    コード = c.自社品番,
                                    名称 = c.自社品名
                                }).ToList();
                        break;

                    case "M09_HIN_SET":
                        Member =
                            context.M09_HIN
                                .Where(x => x.削除日時 == null && x.自社品番 == pコード && x.商品形態分類 == (int)CommonConstants.商品形態分類.SET品)
                                .Select(c => new CodeTextString_Member
                                {
                                    コード = c.自社品番,
                                    名称 = c.自社品名
                                })
                                .ToList();
                        break;

                    case "M12_DAI":     //大分類用検索画面SCH01010
                        if (int.TryParse(pコード, out Code))
                        {
                            Member =
                                (from x in context.M12_DAIBUNRUI
                                 where (x.大分類コード == Code && x.削除日時 == null)
                                 select new CodeTextInt_Member
                                 {
                                     コード = x.大分類コード,
                                     名称 = x.大分類名,
                                 })
                                 .ToList()
                                 .Select(c => new CodeTextString_Member
                                 {
                                     コード = c.コード.ToString(),
                                     名称 = c.名称,
                                 })
                                 .ToList();
                        }
                        break;

                    case "M13_CHU":     //中分類用検索画面SCH01010
                        string linkItem = LinkItem == null ? string.Empty : LinkItem.ToString();
                        if (int.TryParse(pコード, out Code) && int.TryParse(linkItem, out LinkCode))
                        {
                            Member =
                                context.M13_TYUBUNRUI
                                    .Where(w => w.削除日時 == null && w.大分類コード == LinkCode && w.中分類コード == Code)
                                    .Select(s => new CodeTextInt_Member
                                    {
                                        コード = s.中分類コード,
                                        名称 = s.中分類名
                                    })
                                    .ToList()
                                    .Select( s => new CodeTextString_Member {
                                        コード = s.コード.ToString(),
                                        名称 = s.名称
                                    })
                                    .ToList();

                        }
                        break;
                    case "M14_BRAND":
                        Member =
                                (from x in context.M14_BRAND
                                 where (x.ブランドコード == pコード && x.削除日時 == null)
                                 select new CodeTextString_Member
                                 {
                                     コード = x.ブランドコード,
                                     名称 = x.ブランド名,
                                 })
                                 .ToList();
                        break;

                    case "M15_SERIES":
                        Member =
                                (from x in context.M15_SERIES
                                 where (x.シリーズコード == pコード && x.削除日時 == null)
                                 select new CodeTextString_Member
                                 {
                                     コード = x.シリーズコード,
                                     名称 = x.シリーズ名,
                                 })
                                 .ToList();
                        break;

                    case "M16_HINGUN":
                        Member =
                            context.M16_HINGUN.Where(x => x.削除日時 == null && x.品群コード == pコード)
                                .Select(s => new CodeTextString_Member
                                {
                                    コード = s.品群コード,
                                    名称 = s.品群名,
                                }).ToList();
                        break;

                    case "M21_SYUK":    // 出荷先
                        Member =
                            context.M21_SYUK.Where(x => x.削除日時 == null && x.出荷先コード == pコード)
                                    .Select(s => new CodeTextString_Member
                                    {
                                        コード = s.出荷先コード,
                                        名称 = s.出荷先名１
                                    }).ToList();
                        break;

                    case "M22_SOUK":    // 倉庫
                        if (int.TryParse(pコード, out Code))
                        {
                            Member =
                                context.M22_SOUK.Where(x => x.削除日時 == null && x.倉庫コード == Code)
                                       .Select(s => new CodeTextString_Member
                                       {
                                           コード = SqlFunctions.StringConvert((double)s.倉庫コード),
                                           名称 = s.倉庫名
                                       }).ToList();
                        }
                        break;

                    case "M22_SOUK_JISC":    // 自社別倉庫
                        if (int.TryParse(pコード, out Code))
                        {
                            if (LinkItem != null) int.TryParse(LinkItem.ToString(), out LinkCode);

                            Member =
                                context.M22_SOUK.Where(x => x.削除日時 == null && x.倉庫コード == Code && x.寄託会社コード == (LinkItem == null ? x.寄託会社コード : LinkCode))
                                       .Select(s => new CodeTextString_Member
                                       {
                                           コード = SqlFunctions.StringConvert((double)s.倉庫コード),
                                           名称 = s.倉庫名
                                       }).ToList();
                        }
                        break;

                    case "M70_JIS":     //自社マスタ
                        if (int.TryParse(pコード, out Code))
                        {
                            Member =
                                context.M70_JIS.Where(x => x.削除日時 == null && x.自社コード == Code)
                                       .Select(s => new CodeTextString_Member
                                       {
                                           コード = SqlFunctions.StringConvert((double)s.自社コード),
                                           名称 = s.自社名
                                       }).ToList();
                        }
                        break;

                    case "M72_TNT":     //担当者マスタ
                        if (int.TryParse(pコード, out Code))
                        {
                            Member =
                                (from x in context.M72_TNT
                                 where (x.担当者ID == Code && x.削除日時 == null)
                                 select new CodeTextInt_Member
                                 {
                                     コード = x.担当者ID,
                                     名称 = x.担当者名,
                                 })
                                 .ToList()
                                 .Select(c => new CodeTextString_Member
                                 {
                                     コード = c.コード.ToString(),
                                     名称 = c.名称,
                                 })
                                 .ToList();
                        }
                        break;

                    case "M74_KGRP":     //権限マスタ
                        if (int.TryParse(pコード, out Code))
                        {
                            Member =
                                (from x in context.M74_KGRP
                                 where (x.グループ権限ID == Code && x.削除日付 == null)
                                 select new CodeTextInt_Member
                                 {
                                     コード = x.グループ権限ID,
                                     名称 = x.プログラムID,
                                 })
                                 .ToList()
                                 .Select(c => new CodeTextString_Member
                                 {
                                     コード = c.コード.ToString(),
                                     名称 = c.名称,
                                 })
                                 .ToList();
                        }
                        break;

                    case "M74_AUTHORITY_NAME":
                        if (int.TryParse(pコード, out Code))
                        {
                            Member =
                                (from x in context.M74_KGRP_NAME
                                 where (x.グループ権限ID == Code)
                                 select new CodeTextInt_Member
                                 {
                                     コード = x.グループ権限ID,
                                     名称 = x.グループ権限名,
                                 })
                                 .ToList()
                                 .Select(c => new CodeTextString_Member
                                 {
                                     コード = c.コード.ToString(),
                                     名称 = c.名称,
                                 })
                                 .ToList();
                        }
                        break;

                }

                switch (DataAccessName)
				{
                    case "M01_TOK_ALL":     //得意先用検索画面SCH01010
                        if (int.TryParse(pコード, out Code))
                        {
                            List<int> targetKbn = new List<int> {1, 2, 3, 4};
                            Member =
                                context.M01_TOK.Where(x => x.削除日時 == null && x.取引先コード == Code && targetKbn.Contains(x.取引区分))
                                    .Select(s => new CodeTextString_Member
                                        {
                                            コード = SqlFunctions.StringConvert((double)s.取引先コード),
                                            名称 = s.略称名
                                        }).ToList();
                        }
                        break;

                    case "M01_TOK_KBN_SELECT":      // 取引先マスタ(取引区分指定)

                        if (int.TryParse(pコード, out Code))
                        {
                            LinkItem = LinkItem == null ? "" : LinkItem;
                            List<int> targetKbn = new List<int>();
                            foreach (string str in LinkItem.ToString().Split(','))
                                targetKbn.Add(int.Parse(str));

                            Member =
                                context.M01_TOK.Where(x => x.削除日時 == null && x.取引先コード == Code && targetKbn.Contains(x.取引区分))
                                    .Select(s => new CodeTextString_Member
                                        {
                                            コード = SqlFunctions.StringConvert((double)s.取引先コード),
                                            名称 = s.略称名
                                        }).ToList();
                        }
                        break;


                    case "M01_TOK_UC":     //得意先用検索画面SCH01010
                        if (int.TryParse(pコード, out Code))
                        {
                            Member =
                                context.M01_TOK.Select(s => new CodeTextString_Member
                                    {
                                        コード = SqlFunctions.StringConvert((double)s.取引先コード),
                                        名称 = s.略称名,
                                    }).ToList();
                        }
                        break;

                    case "M01_TOK_ZENTOKU_SCH":     //全取引
                        if (int.TryParse(pコード, out Code))
                        {
                            Member =
                                context.M01_TOK.Select(s => new CodeTextString_Member
                                    {
                                        コード = SqlFunctions.StringConvert((double)s.取引先コード),
                                        名称 = s.略称名,
                                    }).ToList();
                        }
                        break;
                    case "M01_TOK_TOKU_SCH":     //取引先マスタ
                        if (int.TryParse(pコード, out Code))
                        {
                            Member =
                                context.M01_TOK.Select(s => new CodeTextString_Member
                                    {
                                        コード = SqlFunctions.StringConvert((double)s.取引先コード),
                                        名称 = s.略称名,
                                    }).ToList();
                        }
                        break;
                    case "M01_TOK_SHIHARAI_SCH":
					if (int.TryParse(pコード, out Code))
					{
                        Member =
                            context.M01_TOK.Select(s => new CodeTextString_Member
                                {
                                    コード = SqlFunctions.StringConvert((double)s.取引先コード),
                                    名称 = s.略称名,
                                }).ToList();
                    }
					break;

                    case "M01_KEI":     //経費先マスタ
                    if (int.TryParse(pコード, out Code))
                    {
                        Member =
                            context.M01_TOK.Select(s => new CodeTextString_Member
                                {
                                    コード = SqlFunctions.StringConvert((double)s.取引先コード),
                                    名称 = s.略称名,
                                }).ToList();
                    }
                    break;

                    case "M01_ZEN_SHIHARAI":     //全支払先マスタ
                    if (int.TryParse(pコード, out Code))
                    {
                        Member =
                            context.M01_TOK.Select(s => new CodeTextString_Member
                                {
                                    コード = SqlFunctions.StringConvert((double)s.取引先コード),
                                    名称 = s.略称名,
                                }).ToList();
                    }
                    break;

                    case "M01_TOK_JIS":     //取引先マスタ（住所取得）
					if (int.TryParse(pコード, out Code))
					{
                        Member =
                            context.M01_TOK.Where(s => s.取引先コード == Code)
                                .Select(s => new CodeTextString_Member
                                    {
                                        コード = SqlFunctions.StringConvert((double)s.取引先コード),
                                        名称 = s.略称名,
                                    }).ToList();
					}
					break;

                    case "M01_ALL_TOK":         //得意先用 コード,名称,締日,集金日,サイト
                    if (int.TryParse(pコード, out Code))
                    {
                        Member =
                            context.M01_TOK.Where(w => w.削除日時 == null && w.取引先コード == Code)
                                .Select(s => new CodeTextString_Member
                                    {
                                        コード = SqlFunctions.StringConvert((double)s.取引先コード),
                                        名称 = s.略称名,
                                        締日 =  SqlFunctions.StringConvert((double)s.Ｔ締日),
                                        集金日 = SqlFunctions.StringConvert((double)s.Ｔ入金日１),
                                        サイト = SqlFunctions.StringConvert((double)s.Ｔサイト１),
                                    }).ToList();
                    }
                    break;

                    case "M01_ALL_SHR":         //支払先用 コード,名称,締日,集金日,サイト
                    if (int.TryParse(pコード, out Code))
                    {
                        Member =
                            context.M01_TOK.Where(w => w.削除日時 == null && w.取引先コード == Code)
                                .Select(s => new CodeTextString_Member
                                {
                                    コード = SqlFunctions.StringConvert((double)s.取引先コード),
                                    名称 = s.略称名,
                                    締日 = SqlFunctions.StringConvert((double)s.Ｓ締日),
                                    集金日 = SqlFunctions.StringConvert((double)s.Ｓ入金日１),
                                    サイト = SqlFunctions.StringConvert((double)s.Ｓサイト１),
                                }).ToList();
                    }
                    break;

                case "M09_HIN":    //商品マスタ
                    if (int.TryParse(pコード, out Code))
                    {
                        Member =
                            context.M09_HIN.Where(x => x.品番コード == Code && x.削除日時 == null)
                                .Select(c => new CodeTextString_Member
                             {
                                 コード = SqlFunctions.StringConvert((double)c.品番コード),
                                 名称 = c.自社品名,
                             }).ToList();
                    }
                    break;

                case "M11_TEK":    //摘要マスタ
                    if (int.TryParse(pコード, out Code))
                    {
                        Member =
                            (from x in context.M11_TEK
                             where (x.摘要ID == Code && x.削除日時 == null)
                             select new CodeTextInt_Member
                             {
                                 コード = x.摘要ID,
                                 名称 = x.摘要名,
                             })
                             .ToList()
                             .Select(c => new CodeTextString_Member
                             {
                                 コード = c.コード.ToString(),
                                 名称 = c.名称,
                             })
                             .ToList();
                    }
                    break;

				}

            }

            return Member;

        }

        public List<TwinCodeText_Member> GetTwinDataMasterTable(string DataAccessName, string pコード, string pサブコード, object LinkItem)
        {
            List<TwinCodeText_Member> Member = new List<TwinCodeText_Member>();
            int Code = 0, Eda = 0;

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                switch (DataAccessName)
                {
                    case "M01_TOK":
                        #region 取引先マスタ
                        LinkItem = LinkItem == null ? "" : LinkItem;
                        List<int> targetKbn = new List<int>();
                        foreach (string str in LinkItem.ToString().Split(','))
                            if(!string.IsNullOrEmpty(str))
                                targetKbn.Add(int.Parse(str));

                        // LinkItemの設定がない場合は全ての取引区分を対象とする
                        if (targetKbn.Count == 0)
                            targetKbn = new List<int> {
                                CommonConstants.取引区分.得意先.GetHashCode(),
                                CommonConstants.取引区分.仕入先.GetHashCode(),
                                CommonConstants.取引区分.加工先.GetHashCode(),
                                CommonConstants.取引区分.相殺.GetHashCode(),
                                CommonConstants.取引区分.販社.GetHashCode()
                            };

                        if (int.TryParse(pコード, out Code) && int.TryParse(pサブコード, out Eda))
                        {
                            Member =
                                context.M01_TOK
                                    .Where(x =>
                                        x.削除日時 == null &&
                                        x.取引先コード == Code &&
                                        x.枝番 == Eda &&
                                        targetKbn.Contains(x.取引区分))
                                    .Select(s => new TwinCodeText_Member
                                    {
                                        コード = SqlFunctions.StringConvert((double)s.取引先コード),
                                        サブコード = SqlFunctions.StringConvert((double)s.枝番),
                                        名称 = s.得意先名１,
                                        Ｓ税区分ID = s.Ｓ税区分ID,
                                        Ｓ支払消費税区分 = s.Ｓ支払消費税区分,
                                        Ｔ税区分ID = s.Ｔ税区分ID,
                                        Ｔ消費税区分 = s.Ｔ消費税区分
                                    }).ToList();
                        }
                        #endregion

                        break;
                    
                }

            }

            return Member;

        }

        /// <summary>
        /// 取引先マスタ参照用
        /// </summary>
        /// <param name="DataAccessName">データアクセス名</param>
        /// <param name="取引先コード">取引先コード</param>
        /// <param name="枝番">枝番</param>
        /// <param name="LinkItem">対象取引区分(カンマ区切りにて複数指定可)</param>
        /// <returns></returns>
        public List<M01_TOK_SearchMember> GetDataMasterTable_Product(string DataAccessName, string 取引先コード, string 枝番, object LinkItem)
        {
            List<M01_TOK_SearchMember> Member = new List<M01_TOK_SearchMember>();
			int Code = -1;
            int Eda = -1;
            List<int> tradingKbnList = new List<int>();

            // 取引先コードの型変換：失敗時は処理終了
            if (!int.TryParse(取引先コード, out Code))
                return Member;

            // 枝番の型変換：失敗時は処理終了
            if (!int.TryParse(枝番, out Eda))
                return Member;

            // LinkItemを取引区分(複数指定可)として処理
            if (LinkItem != null)
            {
                foreach (string str in LinkItem.ToString().Split(','))
                {
                    int val = -1;
                    if (int.TryParse(str, out val))
                        tradingKbnList.Add(val);
                }

            }

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                switch (DataAccessName)
                {
                    case "M01_TOK":
                        if (tradingKbnList.Count == 0)
                        {
                            Member =
                                context.M01_TOK.Where(x => x.削除日時 == null && x.取引先コード == Code && x.枝番 == Eda)
                                    .Select(s => new M01_TOK_SearchMember
                                        {
                                            コード = SqlFunctions.StringConvert((double)s.取引先コード),
                                            名称 = s.得意先名１,
                                            Ｓ支払消費税区分 = s.Ｓ支払消費税区分,
                                            Ｓ税区分ID = s.Ｓ税区分ID
                                        }).ToList();
                        }
                        else
                        {
                            Member =
                                context.M01_TOK.Where(x => x.削除日時 == null
                                                            && x.取引先コード == Code
                                                            && x.枝番 == Eda
                                                            && tradingKbnList.Contains(x.取引区分))
                                    .Select(s => new M01_TOK_SearchMember
                                        {
                                            コード = SqlFunctions.StringConvert((double)s.取引先コード),
                                            名称 = s.得意先名１,
                                            Ｓ支払消費税区分 = s.Ｓ支払消費税区分,
                                            Ｓ税区分ID = s.Ｓ税区分ID
                                        }).ToList();
                        }
                        break;

                    case "M01_TOK_ALL":
                        Member =
                            context.M01_TOK.Where(x => x.削除日時 == null)
                                .Select(s => new M01_TOK_SearchMember
                                    {
                                        コード = SqlFunctions.StringConvert((double)s.取引先コード),
                                        名称 = s.得意先名１,
                                        Ｓ支払消費税区分 = s.Ｓ支払消費税区分,
                                        Ｓ税区分ID = s.Ｓ税区分ID
                                    }).ToList();
                        break;

                    default:
                        break;

                }

            }

            return Member;

        }

        /// <summary>
        /// 郵便番号から住所を検索する
        /// </summary>
        /// <param name="zip">郵便番号</param>
        /// <returns></returns>
		public List<M99_ZIP> GetAddressByZip(string zip)
		{
            // 郵便番号ハイフンを除去
            zip = zip.Replace("-", "");

            // 空値または数値以外の場合は処理しない
            if (string.IsNullOrEmpty(zip) || !Regex.IsMatch(zip, @"^[0-9]+$"))
                return new List<M99_ZIP>();

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
			{
				try { context.Connection.Open(); }
				catch (Exception ex) { throw new DBOpenException(ex); }

                var zipList = context.M99_ZIP
                                    .Where(w => w.郵便番号 == zip)
                                    .OrderBy(o => o.地区コード)
                                    .ThenBy(t => t.郵便番号);

				return zipList.ToList();

            }

        }

        /// <summary>
        /// 担当者IDに合致する担当者情報を取得する
        /// </summary>
        /// <param name="staffCode"></param>
        /// <param name="staffCode2"></param>
        /// <returns></returns>
        public List<M72_TNT> GetStaffInfo(string staffCode, string staffCode2)
        {
            // 空値または数値以外の場合は処理しない
            List<int> paramList = new List<int>();

            // スタッフ１
            if (!string.IsNullOrEmpty(staffCode) && Regex.IsMatch(staffCode, @"^[0-9]+$"))
                paramList.Add(int.Parse(staffCode));

            // スタッフ２
            if (!string.IsNullOrEmpty(staffCode2) && Regex.IsMatch(staffCode2, @"^[0-9]+$"))
                paramList.Add(int.Parse(staffCode2));

            if (paramList.Count == 0)
                return new List<M72_TNT>();

            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try { context.Connection.Open(); }
                catch (Exception ex) { throw new DBOpenException(ex); }

                var staffList = context.M72_TNT
                                    .Where(w => paramList.Contains(w.担当者ID))
                                    .OrderBy(o => o.担当者ID);

                return staffList.ToList();

            }

        }

        /// <summary>
        /// 品番より品番情報を取得する
        /// </summary>
        /// <param name="productCode">品番</param>
        /// <returns></returns>
        public List<M09_HIN> GetDataProductData(string productCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try { context.Connection.Open(); }
                catch (Exception ex) { throw new DBOpenException(ex); }

                int code = 0;
                List<M09_HIN> result;
                if (int.TryParse(productCode, out code))
                {
                    result = context.M09_HIN
                                    .Where(x => x.削除日時 == null && x.品番コード == code)
                                    .ToList();

                }
                else
                {
                    result = context.M09_HIN
                                    .Where(x => x.削除日時 == null)
                                    .ToList();

                }

                return result;

            }

        }

        /// <summary>
        /// 自社品番より品番情報を取得する
        /// </summary>
        /// <param name="myPCode">自社品番</param>
        /// <returns></returns>
        public List<M09.M09_HIN_NAMED> GetDataMyProductData(string myPCode, int? code, int? eda)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try { context.Connection.Open(); }
                catch (Exception ex) { throw new DBOpenException(ex); }

                // TODO:もしかすると自社色の指定が必要かもしれない
                //var result = context.M09_HIN
                //                .Where(x => x.削除日時 == null && x.自社品番 == myPCode);

                var result =
                    context.M09_HIN.Where(w => (w.削除日時 == null || w.論理削除 == 論理削除) && w.自社品番 == myPCode)
                    .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null),
                        x => x.自社色, y => y.色コード, (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (a, b) => new { HIN = a.x, IRO = b })
                    .GroupJoin(context.M12_DAIBUNRUI.Where(w => w.削除日時 == null),
                        x => x.HIN.大分類, y => y.大分類コード, (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (c, d) => new { c.x.HIN, c.x.IRO, DAI = d })
                    .GroupJoin(context.M13_TYUBUNRUI.Where(w => w.削除日時 == null),
                        x => x.HIN.中分類, y => y.中分類コード, (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (e, f) => new { e.x.HIN, e.x.IRO, e.x.DAI, TYU = f })
                    .GroupJoin(context.M14_BRAND.Where(w => w.削除日時 == null),
                        x => x.HIN.ブランド, y => y.ブランドコード, (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (g, h) => new { g.x.HIN, g.x.IRO, g.x.DAI, g.x.TYU, BRA = h })
                    .GroupJoin(context.M15_SERIES.Where(w => w.削除日時 == null),
                        x => x.HIN.シリーズ, y => y.シリーズコード, (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (i, j) => new { i.x.HIN, i.x.IRO, i.x.DAI, i.x.TYU, i.x.BRA, SER = j })
                    .GroupJoin(context.M16_HINGUN.Where(w => w.削除日時 == null),
                        x => x.HIN.品群, y => y.品群コード, (x, y) => new { x, y })
                    .SelectMany(m => m.y.DefaultIfEmpty(), (k, l) => new { k.x.HIN, k.x.IRO, k.x.DAI, k.x.TYU, k.x.BRA, k.x.SER, GUN = l })
                    .GroupJoin(context.M02_BAIKA.Where(w => w.削除日時 == null && w.得意先コード == code && w.枝番 == eda), x => x.HIN.品番コード, y => y.品番コード, (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(), (m, n) => new { m.x.HIN, m.x.IRO, m.x.DAI, m.x.TYU, m.x.BRA, m.x.SER, m.x.GUN, TBAI = n })
                    .GroupJoin(context.M03_BAIKA.Where(w => w.削除日時 == null && w.仕入先コード == code && w.枝番 == eda), x => x.HIN.品番コード, y => y.品番コード, (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(), (o, p) => new { o.x.HIN, o.x.IRO, o.x.DAI, o.x.TYU, o.x.BRA, o.x.SER, o.x.GUN, o.x.TBAI, SBAI = p })
                    .GroupJoin(context.M04_BAIKA.Where(w => w.削除日時 == null && w.外注先コード == code && w.枝番 == eda), x => x.HIN.品番コード, y => y.品番コード, (x, y) => new { x, y })
                    .SelectMany(x => x.y.DefaultIfEmpty(), (q, r) => new { q.x.HIN, q.x.IRO, q.x.DAI, q.x.TYU, q.x.BRA, q.x.SER, q.x.GUN, q.x.TBAI, q.x.SBAI, GBAI = r })
                    .Where(c => c.HIN.大分類 == c.TYU.大分類コード).DefaultIfEmpty() //暫定
                    .Select(t => new M09.M09_HIN_NAMED
                    {
                        品番コード = t.HIN.品番コード,
                        自社品番 = t.HIN.自社品番,
                        自社色 = t.HIN.自社色,
                        自社色名 = t.IRO.色名称,
                        商品形態分類 = t.HIN.商品形態分類,
                        商品形態分類名 = "",
                        商品分類 = t.HIN.商品分類,
                        商品分類名 = "",
                        大分類 = t.HIN.大分類,
                        大分類名 = t.DAI.大分類名,
                        中分類 = t.HIN.中分類,
                        中分類名 = t.TYU.中分類名,
                        ブランド = t.HIN.ブランド,
                        ブランド名 = t.BRA.ブランド名,
                        シリーズ = t.HIN.シリーズ,
                        シリーズ名 = t.SER.シリーズ名,
                        品群 = t.HIN.品群,
                        品群名 = t.GUN.品群名,
                        自社品名 = t.HIN.自社品名,
                        単位 = t.HIN.単位,
                        マスタ原価 = t.HIN.原価,
                        原価 = t.SBAI.単価 != null ? t.SBAI.単価 : t.HIN.原価,
                        加工原価 = t.GBAI != null ? t.GBAI.単価 : t.HIN.加工原価,
                        卸値 = t.HIN.卸値,
                        売価 = t.TBAI != null ? t.TBAI.単価 : t.HIN.売価,
                        掛率 = t.HIN.掛率,
                        消費税区分 = t.HIN.消費税区分,
                        論理削除 = t.HIN.論理削除,
                        削除日時 = t.HIN.削除日時,
                        削除者 = t.HIN.削除者,
                        登録日時 = t.HIN.登録日時,
                        登録者 = t.HIN.登録者,
                        最終更新日時 = t.HIN.最終更新日時,
                        最終更新者 = t.HIN.最終更新者,
                        備考１ = t.HIN.備考１,
                        備考２ = t.HIN.備考２,
                        返却可能期限 = t.HIN.返却可能期限,
                        ＪＡＮコード = t.HIN.ＪＡＮコード
                    });

                return result.ToList();

            }

        }

        /// <summary>
        /// 自社品番よりセット品番情報を取得する
        /// </summary>
        /// <param name="myPCode">自社品番</param>
        /// <returns></returns>
        public List<M09_HIN> GetDataMyProductSetData(string myPCode)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try { context.Connection.Open(); }
                catch (Exception ex) { throw new DBOpenException(ex); }

                // TODO:もしかすると自社色の指定が必要かもしれない
                var result = context.M09_HIN
                                .Where(x => x.削除日時 == null && x.自社品番 == myPCode && x.商品形態分類 == (int)CommonConstants.商品形態分類.SET品);

                return result.ToList();

            }

        }

        /// <summary>
        /// 指定自社品番(得意先品番含む)より品番情報を取得する
        /// </summary>
        /// <param name="pCode"></param>
        /// <param name="customerId"></param>
        /// <param name="customerEda"></param>
        /// <returns></returns>
        public List<M10_TOKHIN_Extension> GetDataCustomerProductData(string pCode, string customerId, string customerEda)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                try { context.Connection.Open(); }
                catch (Exception ex) { throw new DBOpenException(ex); }

                int iCode, iEda;
                if (int.TryParse(customerId, out iCode) && int.TryParse(customerEda, out iEda))
                {
                    var tokhin =
                        context.M10_TOKHIN
                            .Where(w => w.削除日時 == null && w.得意先品番コード == pCode && w.取引先コード == iCode && w.枝番 == iEda)
                            .Join(context.M09_HIN.Where(w => w.削除日時 == null),
                                x => x.品番コード,
                                y => y.品番コード,
                                (x, y) => new { TOKHIN = x, HIN = y })
                            .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null),
                                x => x.HIN.自社色,
                                y => y.色コード,
                                (c, d) => new { c, d })
                            .SelectMany(z => z.d.DefaultIfEmpty(), (x, y) => new { x.c.HIN, x.c.TOKHIN, IRO = y })
                            .Select(x => new M10_TOKHIN_Extension
                            {
                                品番コード = x.HIN.品番コード,
                                得意先品番コード = x.TOKHIN.得意先品番コード,
                                自社品番 = x.HIN.自社品番,
                                自社色 = x.HIN.自社色,
                                色名称 = x.IRO.色名称,
                                商品形態分類 = x.HIN.商品形態分類,
                                商品分類 = x.HIN.商品分類,
                                大分類 = x.HIN.大分類,
                                中分類 = x.HIN.中分類,
                                ブランド = x.HIN.ブランド,
                                シリーズ = x.HIN.シリーズ,
                                品群 = x.HIN.品群,
                                自社品名 = x.HIN.自社品名,
                                単位 = x.HIN.単位,
                                原価 = x.HIN.原価,
                                加工原価 = x.HIN.加工原価,
                                卸値 = x.HIN.卸値,
                                売価 = x.HIN.売価,
                                掛率 = x.HIN.掛率,
                                消費税区分 = x.HIN.消費税区分,
                                備考１ = x.HIN.備考１,
                                備考２ = x.HIN.備考２,
                                返却可能期限 = x.HIN.返却可能期限,
                                ＪＡＮコード = x.HIN.ＪＡＮコード
                            });

                    if (tokhin.Count() > 0)
                        return tokhin.ToList();

                    // 得意先品番が見つからない場合は品番マスタを検索
                    // TODO:もしかすると自社色の指定が必要かもしれない
                    var result = context.M09_HIN
                                    .Where(x => x.削除日時 == null && x.自社品番 == pCode)
                                    .GroupJoin(context.M10_TOKHIN.Where(w => w.削除日時 == null && w.取引先コード == iCode && w.枝番 == iEda),
                                        x => x.品番コード,
                                        y => y.品番コード,
                                        (x, y) => new { x, y })
                                    .SelectMany(x => x.y.DefaultIfEmpty(),
                                        (a, b) => new { HIN = a.x, TOKHIN = b })
                                    .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null),
                                        x => x.HIN.自社色,
                                        y => y.色コード,
                                        (c, d) => new { c, d })
                                    .SelectMany(z => z.d.DefaultIfEmpty(), (x, y) => new { x.c.HIN, x.c.TOKHIN, IRO = y })
                                    .Select(x => new M10_TOKHIN_Extension
                                    {
                                        品番コード = x.HIN.品番コード,
                                        得意先品番コード = x.TOKHIN.得意先品番コード,
                                        自社品番 = x.HIN.自社品番,
                                        自社色 = x.HIN.自社色,
                                        色名称 = x.IRO.色名称,
                                        商品形態分類 = x.HIN.商品形態分類,
                                        商品分類 = x.HIN.商品分類,
                                        大分類 = x.HIN.大分類,
                                        中分類 = x.HIN.中分類,
                                        ブランド = x.HIN.ブランド,
                                        シリーズ = x.HIN.シリーズ,
                                        品群 = x.HIN.品群,
                                        自社品名 = x.HIN.自社品名,
                                        単位 = x.HIN.単位,
                                        原価 = x.HIN.原価,
                                        加工原価 = x.HIN.加工原価,
                                        卸値 = x.HIN.卸値,
                                        売価 = x.HIN.売価,
                                        掛率 = x.HIN.掛率,
                                        消費税区分 = x.HIN.消費税区分,
                                        備考１ = x.HIN.備考１,
                                        備考２ = x.HIN.備考２,
                                        返却可能期限 = x.HIN.返却可能期限,
                                        ＪＡＮコード = x.HIN.ＪＡＮコード
                                    });

                    return result.ToList();

                }
                else
                {
                    // 得意先、枝番が不正値だった場合は品番情報のみを取得して返す
                    var result = context.M09_HIN.Where(x => x.削除日時 == null && x.自社品番 == pCode)
                                    .GroupJoin(context.M06_IRO.Where(w => w.削除日時 == null),
                                       x => x.自社色,
                                       y => y.色コード,
                                       (c, d) => new { c, d })
                                    .SelectMany(z => z.d.DefaultIfEmpty(), (x, y) => new { HIN = x.c, y })
                                    .Select(x => new M10_TOKHIN_Extension
                                    {
                                        品番コード = x.HIN.品番コード,
                                        自社品番 = x.HIN.自社品番,
                                        自社色 = x.HIN.自社色,
                                        色名称 = x.y.色名称,
                                        商品形態分類 = x.HIN.商品形態分類,
                                        商品分類 = x.HIN.商品分類,
                                        大分類 = x.HIN.大分類,
                                        中分類 = x.HIN.中分類,
                                        ブランド = x.HIN.ブランド,
                                        シリーズ = x.HIN.シリーズ,
                                        品群 = x.HIN.品群,
                                        自社品名 = x.HIN.自社品名,
                                        単位 = x.HIN.単位,
                                        原価 = x.HIN.原価,
                                        加工原価 = x.HIN.加工原価,
                                        卸値 = x.HIN.卸値,
                                        売価 = x.HIN.売価,
                                        掛率 = x.HIN.掛率,
                                        消費税区分 = x.HIN.消費税区分,
                                        備考１ = x.HIN.備考１,
                                        備考２ = x.HIN.備考２,
                                        返却可能期限 = x.HIN.返却可能期限,
                                        ＪＡＮコード = x.HIN.ＪＡＮコード
                                    });

                    return result.ToList();

                }

            }

        }

    }

}
