using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KyoeiSystem.Framework.Common;

namespace KyoeiSystem.Application.WCFService
{

    /// <summary>
    /// 
    /// </summary>
    public static class DataAccessCfg
    {
        /// <summary>
        /// Dataアクセス用設定取得
        /// </summary>
        /// <param name="name">機能名</param>
        /// <returns>設定内容</returns>
        public static WCFDataAccessConfig GetDacCfg(string name)
        {
            var cfg = daccfglist.Where(c => c.Name == name).FirstOrDefault();
            if (cfg != null)
            {
                if (string.IsNullOrWhiteSpace(cfg.Namespace))
                {
                    cfg.Namespace = SVCnamespace;
                }
                if (string.IsNullOrWhiteSpace(cfg.Dll))
                {
                    cfg.Dll = cfg.Namespace + ".dll";
                }
            }

            return cfg;
        }

        private static string SVCnamespace = "KyoeiSystem.Application.WCFService";
        private static List<WCFDataAccessConfig> daccfglist = new List<WCFDataAccessConfig>()
		{

            #region システム共通

            // メニュー 
			new WCFDataAccessConfig() { Name = "Oshirase", ServiceClass = "MENU01", MethodName = "Oshirase", Descprition = "メッセージマスタ取得", },

			// スタートアップ処理 
			new WCFDataAccessConfig() { Name = "GetMessageList", ServiceClass = "Common", MethodName = "GetMessageList", Descprition = "メッセージマスタ取得", },
			new WCFDataAccessConfig() { Name = "GetComboboxList", ServiceClass = "Common", MethodName = "GetCodeList", Descprition = "コンボボックス用リストデータ取得", },
			new WCFDataAccessConfig() { Name = "GetZipList", ServiceClass = "Common", MethodName = "GetZipList", Descprition = "郵便番号データ取得", },

			// ライセンス認証関連 
			new WCFDataAccessConfig() { Name = "LicenseLoginCommonDB", ServiceClass = "CommonDBReadWrite", MethodName = "LicenseLogin", Descprition = "ログイン処理", },
			new WCFDataAccessConfig() { Name = "updateAccessDateTime", ServiceClass = "CommonDBReadWrite", MethodName = "updateAccessDateTime", Descprition = "アクセス時刻更新処理", },
			new WCFDataAccessConfig() { Name = "updateLogout", ServiceClass = "CommonDBReadWrite", MethodName = "updateLogout", Descprition = "ログアウト処理", },
			new WCFDataAccessConfig() { Name = "updateLogin", ServiceClass = "CommonDBReadWrite", MethodName = "updateLogin", Descprition = "ログイン処理", },
            new WCFDataAccessConfig() { Name = "MessgeShow", ServiceClass = "CommonDBReadWrite", MethodName = "MessgeShow", Descprition = "お知らせ情報取得", },

			// アカウント処理 
			new WCFDataAccessConfig() { Name = "SEARCH_LOGIN", ServiceClass = "Account", MethodName = "Login", Descprition = "ログイン処理", },
			new WCFDataAccessConfig() { Name = "Logout", ServiceClass = "Account", MethodName = "Logout", Descprition = "ログアウト処理", },
			new WCFDataAccessConfig() { Name = "Get_UserList", ServiceClass = "Account", MethodName = "Get_UserList", Descprition = "オートコンプリート取得処理", },

			// ユーザコントロールのマスタ参照 
			new WCFDataAccessConfig() { Name = "UcMST", ServiceClass = "UcMST", MethodName = "GetDataMasterTable", Descprition = "ユーザコントロールのマスタ参照", },
			new WCFDataAccessConfig() { Name = "UcTwinMST", ServiceClass = "UcMST", MethodName = "GetTwinDataMasterTable", Descprition = "ユーザコントロールのマスタ参照(Text1、2参照)", },
			new WCFDataAccessConfig() { Name = "UcAllMST", ServiceClass = "UcMST", MethodName = "GetMasterData", Descprition = "ユーザコントロールのマスタ参照", },
			new WCFDataAccessConfig() { Name = "UcCOMBO", ServiceClass = "UcMST", MethodName = "GetComboboxList", Descprition = "コンボボックスのリスト取得", },
			new WCFDataAccessConfig() { Name = "UcZIP", ServiceClass = "UcMST", MethodName = "GetAddressByZip", Descprition = "郵便番号から住所リスト取得", },
			new WCFDataAccessConfig() { Name = "UcUser", ServiceClass = "UcMST", MethodName = "GetStaffInfo", Descprition = "担当者IDから担当者リスト取得", },
			new WCFDataAccessConfig() { Name = "UcSupplier", ServiceClass = "UcMST", MethodName = "GetDataMasterTable_Product", Descprition = "取引先名称取得", },
            new WCFDataAccessConfig() { Name = "UcProduct", ServiceClass = "UcMST", MethodName = "GetDataProductData", Descprition = "対象品名の取得" },
            new WCFDataAccessConfig() { Name = "UcMyProduct", ServiceClass = "UcMST", MethodName = "GetDataMyProductData", Descprition = "自社品番から品番情報を取得" },
            new WCFDataAccessConfig() { Name = "UcMyProductSet", ServiceClass = "UcMST", MethodName = "GetDataMyProductSetData", Descprition = "自社品番からセット品番情報を取得" },
            new WCFDataAccessConfig() { Name = "UcCustomerProduct", ServiceClass = "UcMST", MethodName = "GetDataCustomerProductData", Descprition = "自社品番(得意先品番含む)から品番情報を取得" },

            #endregion

            #region マスタ(M01 - M10)

            // 取引先マスターメンテ用
            new WCFDataAccessConfig() { Name = "M01_TOK_GetData", ServiceClass="M01", MethodName="GetData", Descprition="取引先マスタメンテデータ取得" },
            new WCFDataAccessConfig() { Name = "M01_TOK_Update", ServiceClass="M01", MethodName="Update", Descprition="取引先マスタメンテナンス登録・更新" },
            new WCFDataAccessConfig() { Name = "M01_TOK_Delete", ServiceClass="M01", MethodName="Delete", Descprition="取引先マスタメンテナンス削除" },
            new WCFDataAccessConfig() { Name = "M01_TOK_getDataList", ServiceClass = "M01", MethodName = "GetDataList", Descprition="取引先マスタ検索 リスト取得" },

            // 得意先商品売価設定
            new WCFDataAccessConfig() { Name = "M02_BAIKA_GetData_Customer", ServiceClass="M02", MethodName="GetData_Customer", Descprition="得意先商品売価設定 得意先検索" },
            new WCFDataAccessConfig() { Name = "M02_BAIKA_GetData_Product", ServiceClass="M02", MethodName="GetData_Product", Descprition="得意先商品売価設定 品番検索" },
            new WCFDataAccessConfig() { Name = "M02_BAIKA_Update", ServiceClass="M02", MethodName="Update", Descprition="得意先商品売価設定 データ登録" },

            // 仕入先商品売価設定
            new WCFDataAccessConfig() { Name = "M03_BAIKA_GetData_Supplier", ServiceClass="M03", MethodName="GetData_Supplier", Descprition="仕入先商品売価設定 仕入先検索" },
            new WCFDataAccessConfig() { Name = "M03_BAIKA_GetData_Product", ServiceClass="M03", MethodName="GetData_Product", Descprition="仕入先商品売価設定 品番検索" },
            new WCFDataAccessConfig() { Name = "M03_BAIKA_Update", ServiceClass="M03", MethodName="Update", Descprition="仕入先商品売価設定 データ登録" },

            // 外注先商品売価設定
            new WCFDataAccessConfig() { Name = "M04_BAIKA_GetData_Outsource", ServiceClass="M04", MethodName="GetData_Outsource", Descprition="外注先商品売価設定 外注先検索" },
            new WCFDataAccessConfig() { Name = "M04_BAIKA_GetData_Product", ServiceClass="M04", MethodName="GetData_Product", Descprition="外注先商品売価設定 品番検索" },
            new WCFDataAccessConfig() { Name = "M04_BAIKA_Update", ServiceClass="M04", MethodName="Update", Descprition="外注先商品売価設定 データ登録" },

            // 色マスタ
            new WCFDataAccessConfig() { Name = "M06_IRO_GetData", ServiceClass = "M06", MethodName = "GetData", Descprition = "色マスタ検索用" },
            new WCFDataAccessConfig() { Name = "M06_IRO_Update", ServiceClass = "M06", MethodName = "Update", Descprition = "色マスタ登録・更新用" },
            new WCFDataAccessConfig() { Name = "M06_IRO_Delete", ServiceClass = "M06", MethodName = "Delete", Descprition = "色マスタ削除用" },

            new WCFDataAccessConfig() { Name = "M06_IRO_GetCsv", ServiceClass = "M06", MethodName = "GetCsv", Descprition = "色マスタCSV出力用" },
            new WCFDataAccessConfig() { Name = "M06_IRO_GetRpt", ServiceClass = "M06", MethodName = "GetRpt", Descprition = "色マスタ帳票出力用" },

            // 品番マスタ
            new WCFDataAccessConfig() { Name = "M09_HIN_getDataList", ServiceClass = "M09", MethodName = "GetDataList", Descprition = "品番検索 データリスト取得" },
            new WCFDataAccessConfig() { Name = "M09_HIN_getNamedDataList", ServiceClass = "M09", MethodName = "GetNamedDataList", Descprition = "品番検索 名前付きデータリスト取得" },
            new WCFDataAccessConfig() { Name = "M09_HIN_getData", ServiceClass = "M09", MethodName = "GetData", Descprition = "品番マスタ データ取得" },
			new WCFDataAccessConfig() { Name = "M09_HIN_getNext", ServiceClass = "M09", MethodName = "GetNextNumber", Descprition = "品番マスタ 新規採番取得", },
            new WCFDataAccessConfig() { Name = "M09_HIN_Update", ServiceClass = "M09", MethodName = "Update", Descprition = "品番マスタ 登録・更新処理" },
            new WCFDataAccessConfig() { Name = "M09_HIN_getNamedData", ServiceClass="M09", MethodName = "GetNamedData", Descprition="品番マスタ 各種コード名称取得版" },
            // No-92 Add Start
            new WCFDataAccessConfig() { Name = "M09_HIN_getDataMyProduct", ServiceClass = "M09", MethodName = "GetDataMyProduct", Descprition = "品番マスタ データ取得(自社品番、色コード)" },
            // No-92 Add End

            // セット品番マスタ
			new WCFDataAccessConfig() { Name = "M10_SHIN", ServiceClass = "M10", MethodName = "GetData", Descprition = "セット品番マスタ データ取得" },
            new WCFDataAccessConfig() { Name = "M10_SHIN_Update", ServiceClass = "M10", MethodName = "Update", Descprition = "セット品番マスタ データ更新" },

            // 得意先品番マスタ
			new WCFDataAccessConfig() { Name = "M10_TOKHIN_GetData", ServiceClass = "M10", MethodName = "GetData_TOKHIN", Descprition = "得意先品番マスタ データ取得(得意先・枝番)" },
            new WCFDataAccessConfig() { Name = "M10_TOKHIN_GetData_Product", ServiceClass = "M10", MethodName = "GetData_TOKHIN_Product", Descprition = "得意先品番マスタ データ取得(自社品番・色)" },
            new WCFDataAccessConfig() { Name = "M10_TOKHIN_GetNamedDataList", ServiceClass = "M10", MethodName = "GetTOKHIN_NamedDataList", Descprition = "得意先品番検索 名前付きデータリスト取得" },
            new WCFDataAccessConfig() { Name = "M10_TOKHIN_Update", ServiceClass = "M10", MethodName = "Update_TOKHIN", Descprition = "得意先品番マスタ データ更新" },

            // 品番マスタ一括更新
            new WCFDataAccessConfig() { Name = "MST02011_GetData", ServiceClass = "MST02011", MethodName = "GetData", Descprition = "商品データ　取得" },
            new WCFDataAccessConfig() { Name = "MST02011_Update", ServiceClass = "MST02011", MethodName = "Update", Descprition = "商品データ　更新" },
            new WCFDataAccessConfig() { Name = "MST02011_GetMasterDataSet", ServiceClass = "MST02011", MethodName = "GetMasterDataSet", Descprition = "マスタデータ 取得" },
            
            // 取引先マスタ一括更新
            new WCFDataAccessConfig() { Name = "MST01011_GetData", ServiceClass = "MST01011", MethodName = "GetData", Descprition = "取引先データ　取得" },
            new WCFDataAccessConfig() { Name = "MST01011_Update", ServiceClass = "MST01011", MethodName = "Update", Descprition = "取引先データ　更新" },
            new WCFDataAccessConfig() { Name = "MST01011_GetM72", ServiceClass = "MST01011", MethodName = "GetM72", Descprition = "担当データ　取得" },


            #endregion

            #region マスタ(M11 - M20)

            // 摘要マスタ
			new WCFDataAccessConfig() { Name = "M11_TEK", ServiceClass = "M11", MethodName = "GetData", Descprition = "M11_TEK", },
			new WCFDataAccessConfig() { Name = "M11_TEK_UP", ServiceClass = "M11", MethodName = "Update", Descprition = "M11_TEK", },
			new WCFDataAccessConfig() { Name = "M11_TEK_DEL", ServiceClass = "M11", MethodName = "Delete", Descprition = "M11_TEK", },
			new WCFDataAccessConfig() { Name = "M11_TEK_NEXT", ServiceClass = "M11", MethodName = "GetNextNumber", Descprition = "M11_BUM_NEXT", },
			new WCFDataAccessConfig() { Name = "M11_TEK_SCH", ServiceClass = "M11", MethodName = "GetSearchData", Descprition = "M11_TEK", },
			new WCFDataAccessConfig() { Name = "SEARCH_MST08020", ServiceClass = "M11", MethodName = "GetSearchDataForList", Descprition = "SEARCH_MST08020", },
			new WCFDataAccessConfig() { Name = "SEARCH_MST08020_CSV", ServiceClass = "M11", MethodName = "GetSearchDataForList", Descprition = "SEARCH_MST08020_CSV", },

            // 大分類マスタ
            new WCFDataAccessConfig() { Name = "M12_GetListData", ServiceClass = "M12", MethodName = "GetDataList", Descprition = "大分類マスター(検索)", },

            // 中分類マスター 
            new WCFDataAccessConfig() { Name = "SCHM13_SCH", ServiceClass = "M13", MethodName = "SEARCH_GetData", Descprition = "中分類マスター(検索)", },

            // 商品中分類マスタ保守
            new WCFDataAccessConfig() { Name = "M13_CHU_SEARCH", ServiceClass = "M13", MethodName = "MST04010_SearchData", Descprition = "商品中分類マスタ保守データ検索", },
            new WCFDataAccessConfig() { Name = "M13_CHU_UPDATE", ServiceClass = "M13", MethodName = "MST04010_UpdateData", Descprition = "商品中分類マスタ保守データ登録", },
            new WCFDataAccessConfig() { Name = "M13_CHU_DELETE", ServiceClass = "M13", MethodName = "MST04010_DeleteData", Descprition = "商品中分類マスタ保守データ削除", },

            // ブランドマスタ
			new WCFDataAccessConfig() { Name = "M14_BRAND", ServiceClass = "M14BRAND", MethodName = "GetData", Descprition = "ブランドマスタ（検索用）", },
			new WCFDataAccessConfig() { Name = "M14_BRAND_UPDATE", ServiceClass = "M14BRAND", MethodName = "Update", Descprition = "ブランドマスタ（更新用）", },
			new WCFDataAccessConfig() { Name = "M14_BRAND_DELETE", ServiceClass = "M14BRAND", MethodName = "Delete", Descprition = "グブランドマスタ（削除用）", },
			new WCFDataAccessConfig() { Name = "M14_BRAND_SCH", ServiceClass = "M14BRAND", MethodName = "GetDataSCH", Descprition = "M14_BRAND", },
            new WCFDataAccessConfig() { Name = "SEARCH_mst04020_1",  ServiceClass = "M14BRAND",  MethodName = "GetSearchDataForList",  Descprition = "SEARCH_mst04020_1",     },
			new WCFDataAccessConfig() { Name = "SEARCH_mst04020_1_CSV",  ServiceClass = "M14BRAND",  MethodName = "GetSearchDataForList",  Descprition = "SEARCH_mst04020_1_CSV",     }, 
            new WCFDataAccessConfig() { Name = "M14_BRAND_GetPagingCode", ServiceClass = "M14BRAND", MethodName = "GetPagingCode", Descprition = "ブランドマスタ(ページング用)", },

            // シリーズマスタ
			new WCFDataAccessConfig() { Name = "M15_SERIES", ServiceClass = "M15SERIES", MethodName = "GetData", Descprition = "シリーズマスタ（検索用）", },
			new WCFDataAccessConfig() { Name = "M15_SERIES_UPDATE", ServiceClass = "M15SERIES", MethodName = "Update", Descprition = "シリーズマスタ（更新用）", },
			new WCFDataAccessConfig() { Name = "M15_SERIES_DELETE", ServiceClass = "M15SERIES", MethodName = "Delete", Descprition = "グシリーズマスタ（削除用）", },
			new WCFDataAccessConfig() { Name = "M15_SERIES_SCH", ServiceClass = "M15SERIES", MethodName = "GetDataSCH", Descprition = "M15_SERIES", },
            new WCFDataAccessConfig() { Name = "SEARCH_mst04021_1",  ServiceClass = "M15SERIES",  MethodName = "GetSearchDataForList",  Descprition = "SEARCH_MST04021_1",     },
			new WCFDataAccessConfig() { Name = "SEARCH_mst04021_1_CSV",  ServiceClass = "M15SERIES",  MethodName = "GetSearchDataForList",  Descprition = "SEARCH_MST04021_1_CSV",     }, 
            new WCFDataAccessConfig() { Name = "M15_SERIES_GetPagingCode", ServiceClass = "M15SERIES", MethodName = "GetPagingCode", Descprition = "シリーズマスタ(ページング用)", },

            // 商品群マスタ
            new WCFDataAccessConfig() { Name = "M16_HINGUN_GetData", ServiceClass = "M16", MethodName = "GetData", Descprition = "商品群マスタ保守データ取得", },
            new WCFDataAccessConfig() { Name = "M16_HINGUN_UPDATE", ServiceClass = "M16", MethodName = "Update", Descprition = "商品群マスタ保守データ更新", },
            new WCFDataAccessConfig() { Name = "M16_HINGUN_DELETE", ServiceClass = "M16", MethodName = "Delete", Descprition = "商品群マスタ保守データ削除", },

            new WCFDataAccessConfig() { Name = "M16_HINGUN_GetCsv", ServiceClass = "M16", MethodName = "GetCsv", Descprition = "商品群マスタCSVデータ出力", },
            new WCFDataAccessConfig() { Name = "M16_HINGUN_GetRpt", ServiceClass = "M16", MethodName = "GetRpt", Descprition = "商品群マスタ帳票出力", },


            #endregion

            #region マスタ(M21 - M30)

            // 出荷先マスタ
            new WCFDataAccessConfig() { Name = "M21_SYUK", ServiceClass = "M21", MethodName = "GetData", Descprition = "出荷先マスタ（検索用）", },
            new WCFDataAccessConfig() { Name = "M21_SYUK_UPDATE", ServiceClass = "M21", MethodName = "Update", Descprition = "出荷先マスタ（更新用）", },
            new WCFDataAccessConfig() { Name = "M21_SYUK_DELETE", ServiceClass = "M21", MethodName = "Delete", Descprition = "出荷先マスタ（削除用）", },
            new WCFDataAccessConfig() { Name = "M21_SYUK_SCH", ServiceClass = "M21", MethodName = "GetSearchData", Descprition = "M21_SYUK", },
            new WCFDataAccessConfig() { Name = "SEARCH_MST01020_1",  ServiceClass = "M21",  MethodName = "GetSearchDataForList",  Descprition = "SEARCH_MST01020_1",     },
            new WCFDataAccessConfig() { Name = "SEARCH_MST01020_1_CSV",  ServiceClass = "M21",  MethodName = "GetSearchDataForList",  Descprition = "SEARCH_MST01020_1_CSV",     }, 
            new WCFDataAccessConfig() { Name = "M21_SYUK_GetPagingCode", ServiceClass = "M21", MethodName = "GetPagingCode", Descprition = "出荷先マスタ(ページング用)", },

            // 倉庫マスタ
            new WCFDataAccessConfig() { Name = "M22_SOUK", ServiceClass = "M22", MethodName = "GetData", Descprition = "M22_SOUK", },
            new WCFDataAccessConfig() { Name = "M22_SOUK_UP", ServiceClass = "M22", MethodName = "Update", Descprition = "M22_SOUK", },
            new WCFDataAccessConfig() { Name = "M22_SOUK_DEL", ServiceClass = "M22", MethodName = "Delete", Descprition = "M22_SOUK", },
            new WCFDataAccessConfig() { Name = "M22_SOUK_NEXT", ServiceClass = "M22", MethodName = "GetNextNumber", Descprition = "M11_BUM_NEXT", },
            new WCFDataAccessConfig() { Name = "M22_SOUK_SCH", ServiceClass = "M22", MethodName = "GetSearchData", Descprition = "M22_SOUK", },
            new WCFDataAccessConfig() { Name = "SEARCH_MST12020_1", ServiceClass = "M22", MethodName = "GetSearchDataForList", Descprition = "SEARCH_MST12020_1", },
            new WCFDataAccessConfig() { Name = "SEARCH_MST12020_1_CSV", ServiceClass = "M22", MethodName = "GetSearchDataForList", Descprition = "SEARCH_MST12020_1_CSV", },


            #endregion

            #region マスタ(M31 - M40)
            #endregion

            #region マスタ(M41 - M50)
            #endregion

            #region マスタ(M51 - M60)
            #endregion

            #region マスタ(M61 - M70)

            // 自社マスタ
            // TODO:[MaineImage]MAIN MENU から呼出しがあるがサービスから削除された
            //      ⇒一応過去ソースから拾ってきて追加している(返却クラスが変わっているのは要確認が必要)
            new WCFDataAccessConfig() { Name = "MaineImage", ServiceClass = "M70", MethodName = "GetImageData", Descprition = "MineImage", },
            new WCFDataAccessConfig() { Name = "M70_JIS_GetData", ServiceClass = "M70", MethodName = "GetData", Descprition = "自社マスタ入力検索用" },
            new WCFDataAccessConfig() { Name = "M70_JIS_Update", ServiceClass = "M70", MethodName = "Update", Descprition = "自社マスタ入力更新用" },
            new WCFDataAccessConfig() { Name = "M70_JIS_Delete", ServiceClass = "M70", MethodName = "Delete", Descprition = "自社マスタ入力削除用" },
            new WCFDataAccessConfig() { Name = "M70_JIS_GetHanList", ServiceClass = "M70", MethodName = "GetHanList", Descprition = "販社データリスト取得用" },

            #endregion

            #region マスタ(M71 - M80)

             // 担当者マスタ
            new WCFDataAccessConfig() { Name = "M72_TNT", ServiceClass = "M72", MethodName = "GetData", Descprition = "担当者マスター検索用", },
            new WCFDataAccessConfig() { Name = "M72_TNT_UP", ServiceClass = "M72", MethodName = "Update", Descprition = "担当者マスター更新用", },
            new WCFDataAccessConfig() { Name = "M72_TNT_DEL", ServiceClass = "M72", MethodName = "Delete", Descprition = "担当者マスター削除用", },
            new WCFDataAccessConfig() { Name = "M72_TNT_SCH", ServiceClass = "M72", MethodName = "GetDataSCH", Descprition = "担当者マスター検索用", },
            new WCFDataAccessConfig() { Name = "M72_TNT_NEXT", ServiceClass = "M72", MethodName = "GetNextNumber", Descprition = "担当者マスター自動さいばん", },
            new WCFDataAccessConfig() { Name = "SEARCH_MST23020", ServiceClass = "M72", MethodName = "GetSearchDataForList", Descprition = "SEARCH_MST23020", },
            new WCFDataAccessConfig() { Name = "SEARCH_MST23020_CSV", ServiceClass = "M72", MethodName = "GetSearchDataForList", Descprition = "SEARCH_MST23020_CSV", },

            // 消費税マスタ
            new WCFDataAccessConfig() { Name = "M73_ZEI", ServiceClass = "M73", MethodName = "GetAllData", Descprition = "消費税マスター全件取得", },
            new WCFDataAccessConfig() { Name = "M73_ZEI_BTN", ServiceClass = "M73", MethodName = "GetDataBtn", Descprition = "M73_ZEI", },
            new WCFDataAccessConfig() { Name = "M73_ZEI_UPD", ServiceClass = "M73", MethodName = "Update", Descprition = "消費税マスター更新用", },
            new WCFDataAccessConfig() { Name = "M73_ZEI_DEL", ServiceClass = "M73", MethodName = "Delete", Descprition = "消費税マスター削除用", },
            new WCFDataAccessConfig() { Name = "M73_ZEI_NEXT", ServiceClass = "M73", MethodName = "GetNextNumber", Descprition = "消費税率マスター自動採番", },
            new WCFDataAccessConfig() { Name = "SEARCH_MST13020", ServiceClass = "M73", MethodName = "GetDataHinList", Descprition = "M73_ZEI", },
            new WCFDataAccessConfig() { Name = "SEARCH_MST13020_CSV", ServiceClass = "M73", MethodName = "GetDataHinList", Descprition = "M73_ZEI", },

            // 権限マスタ
            new WCFDataAccessConfig() { Name = "M74_GET_NEW_KGRPID", ServiceClass = "M74", MethodName = "GetNewID", Descprition = "権限ID新規取得", },
            new WCFDataAccessConfig() { Name = "M74_AUTHORITY_SEL", ServiceClass = "M74", MethodName = "GetData", Descprition = "権限マスタデータ取得", },
            new WCFDataAccessConfig() { Name = "M74_AUTHORITY_UPD", ServiceClass = "M74", MethodName = "UPD_Data", Descprition = "権限マスタデータ更新", },
            new WCFDataAccessConfig() { Name = "M74_KGRPNAME_SCH", ServiceClass = "M74", MethodName = "GetDataSCH", Descprition = "権限グループ名検索用", },


            #endregion

            #region マスタ(M81 - M90)
            #endregion

            #region マスタ(M91 - M100)
            #endregion

            #region トランザクション

            #region トラン参照

            // 在庫テーブル
            new WCFDataAccessConfig() { Name = "S03_STOK_GetData", ServiceClass = "SCHS03_STOK", MethodName = "GetData", Descprition = "在庫検索用" },

            #endregion

            #region 日次処理

            // 売上入力
            new WCFDataAccessConfig() { Name = "T02_GetData", ServiceClass = "DLY03010", MethodName = "GetData", Descprition = "売上入力情報取得" },
            new WCFDataAccessConfig() { Name = "T02_Update", ServiceClass = "DLY03010", MethodName = "Update", Descprition = "売上入力情報登録・更新" },
            new WCFDataAccessConfig() { Name = "T02_UpdateForPrint", ServiceClass = "DLY03010", MethodName = "Update", Descprition = "売上入力情報登録・更新" },
            new WCFDataAccessConfig() { Name = "T02_Delete", ServiceClass = "DLY03010", MethodName = "Delete", Descprition = "売上入力情報削除" },
            new WCFDataAccessConfig() { Name = "T02_CheckStock", ServiceClass = "DLY03010", MethodName = "CheckStockQty", Descprition = "在庫数量チェック" },
            new WCFDataAccessConfig() { Name = "UpdateData_CheckStock_Print", ServiceClass = "DLY03010", MethodName = "CheckStockQty", Descprition = "在庫数量チェック" },
            new WCFDataAccessConfig() { Name = "UpdateData_CheckStock", ServiceClass = "DLY03010", MethodName = "CheckStockQty", Descprition = "在庫数量チェック" },

            // 売上(返品)入力
            new WCFDataAccessConfig() { Name = "T02_GetReturnsData", ServiceClass = "DLY03020", MethodName = "GetRtData", Descprition = "売上返品入力情報取得" },
            new WCFDataAccessConfig() { Name = "T02_ReturnsUpdate", ServiceClass = "DLY03020", MethodName = "Update", Descprition = "売上入力情報登録・更新" },

            // 仕入入力
            new WCFDataAccessConfig() { Name = "T03_GetData", ServiceClass = "DLY01010", MethodName = "GetData", Descprition = "仕入入力情報取得" },
            new WCFDataAccessConfig() { Name = "T03_Update", ServiceClass = "DLY01010", MethodName = "Update", Descprition = "仕入入力情報登録・更新" },
            new WCFDataAccessConfig() { Name = "T03_Delete", ServiceClass = "DLY01010", MethodName = "Delete", Descprition = "仕入入力情報削除" },

            // 仕入(返品)入力
            new WCFDataAccessConfig() { Name = "T03_GetRtData", ServiceClass = "DLY01020", MethodName = "ReturnsSearch", Descprition = "仕入返品入力情報取得" },
            new WCFDataAccessConfig() { Name = "T03_ReturnsUpdate", ServiceClass = "DLY01020", MethodName = "ReturnsUpdate", Descprition = "仕入返品入力情報登録・更新" },

            // 揚り入力
            new WCFDataAccessConfig() { Name = "T04_GetData", ServiceClass = "T04", MethodName = "GetData", Descprition = "揚り入力情報取得" },
            new WCFDataAccessConfig() { Name = "T04_Update", ServiceClass = "T04", MethodName = "Update", Descprition = "揚り入力情報登録・更新" },
            new WCFDataAccessConfig() { Name = "T04_Delete", ServiceClass = "T04", MethodName = "Delete", Descprition = "揚り入力情報削除" },
            new WCFDataAccessConfig() { Name = "T04_CreateDtb", ServiceClass = "T04", MethodName = "getT04_AGRDTB_Create", Descprition = "揚り部材明細作成(品番指定・単体)" },
            new WCFDataAccessConfig() { Name = "T04_GetDtb", ServiceClass = "T04", MethodName = "getT04_AGRDTB_Extension", Descprition = "揚り部材明細取得(伝票番号指定)" },
            // 20190528CB-S
            new WCFDataAccessConfig() { Name = "M10_GetCount", ServiceClass = "T04", MethodName = "M10_GetCount", Descprition = "セット品番構成品の登録件数取得" },
            // 20190528CB-E
            new WCFDataAccessConfig() { Name = "T04_STOK_CHECK", ServiceClass = "T04", MethodName = "STOK_CHECK", Descprition = "セット品の在庫存在確認" },

            // 移動入力
            new WCFDataAccessConfig() { Name = "T05_GetData", ServiceClass = "DLY04010", MethodName = "GetData", Descprition = "移動入力情報取得" },
            new WCFDataAccessConfig() { Name = "T05_Update", ServiceClass = "DLY04010", MethodName = "Update", Descprition = "移動入力情報登録・更新" },

            // 揚り依頼入力
            new WCFDataAccessConfig() { Name = "DLY07010_GetData", ServiceClass = "DLY07010", MethodName = "GetData", Descprition = "揚り依頼入力情報取得" },
            new WCFDataAccessConfig() { Name = "DLY07010_Update", ServiceClass = "DLY07010", MethodName = "Update", Descprition = "揚り依頼入力情報登録・更新" },

            // 入金入力
            new WCFDataAccessConfig() { Name = "T11_GetData", ServiceClass = "T11", MethodName = "GetData", Descprition = "入金入力情報取得" },
            new WCFDataAccessConfig() { Name = "T11_Update", ServiceClass = "T11", MethodName = "Update", Descprition = "入金入力情報登録・更新" },

            // 支払入力
            new WCFDataAccessConfig() { Name = "T12_GetData", ServiceClass = "T12", MethodName = "GetData", Descprition = "支払入力情報取得" },
            new WCFDataAccessConfig() { Name = "T12_Update", ServiceClass = "T12", MethodName = "Update", Descprition = "支払入力情報登録・更新" },

            // 販社売上修正
            new WCFDataAccessConfig() { Name = "DLY12010_GetData", ServiceClass = "DLY12010", MethodName = "GetData", Descprition = "販社売上入力情報取得" },
            new WCFDataAccessConfig() { Name = "DLY12010_Update", ServiceClass = "DLY12010", MethodName = "Update", Descprition = "販社売上入力情報登録・更新" },
            new WCFDataAccessConfig() { Name = "DLY12010_Delete", ServiceClass = "DLY12010", MethodName = "Delete", Descprition = "販社売上入力情報削除" },
            new WCFDataAccessConfig() { Name = "DLY12010_CheckStock", ServiceClass = "DLY12010", MethodName = "CheckStockQty", Descprition = "在庫数量チェック" },
            new WCFDataAccessConfig() { Name = "DLY12010_UpdateData_CheckStock", ServiceClass = "DLY12010", MethodName = "CheckStockQty", Descprition = "在庫数量チェック" },


            #endregion

            #region 随時処理

            // 仕入データ問合せ
            new WCFDataAccessConfig() { Name = "ZIJ01010_GetData", ServiceClass = "ZIJ01010", MethodName = "GetDataList", Descprition = "仕入データ問合せ情報取得" },

            // 支払明細問合せ
            new WCFDataAccessConfig() { Name = "ZIJ02010_GetData", ServiceClass = "ZIJ02010", MethodName = "GetDataList", Descprition = "支払明細問合せ情報取得" },

            // 入金問合せ
            new WCFDataAccessConfig() { Name = "ZIJ03010_GetData", ServiceClass = "ZIJ03010", MethodName = "GetDataList", Descprition = "入金問合せ情報取得" },

            // 出金問合せ
            new WCFDataAccessConfig() { Name = "ZIJ04010_GetData", ServiceClass = "ZIJ04010", MethodName = "GetDataList", Descprition = "出金問合せ情報取得" },

            // 売上明細問合せ
            new WCFDataAccessConfig() { Name = "ZIJ05010_GetData", ServiceClass = "ZIJ05010", MethodName = "GetDataList", Descprition = "売上明細問合せ情報取得" },

            // 商品移動/振替入力問合せ
            new WCFDataAccessConfig() { Name = "ZIJ06010_GetData", ServiceClass = "ZIJ06010", MethodName = "GetDataList", Descprition = "商品移動問い合わせ情報取得" },

            // 商品入出荷問合せ
            new WCFDataAccessConfig() { Name = "ZIJ07010_GetData", ServiceClass = "ZIJ07010", MethodName = "GetDataList", Descprition = "商品入出荷問い合わせ情報取得" },

            // 商品在庫問い合わせ
            new WCFDataAccessConfig() { Name = "ZIJ09010_GetData", ServiceClass = "ZIJ09010", MethodName = "GetDataList", Descprition = "商品在庫問い合わせ情報取得" },

            // 揚り明細問合せ
            new WCFDataAccessConfig() { Name = "ZIJ10010_GetData", ServiceClass = "ZIJ10010", MethodName = "GetDataList", Descprition = "揚り明細問合せ情報取得" },
            #endregion

            #region 得意先管理

            // 売上データ一覧
            new WCFDataAccessConfig() { Name = "SalesAggregation", ServiceClass = "TKS02010", MethodName = "SalesAggregation", Descprition = "売上集計処理" },
            new WCFDataAccessConfig() { Name = "TKS02010_GetCsvData", ServiceClass = "TKS02010", MethodName = "GetCsvData", Descprition = "売上集計、一覧表ＣＳＶデータ取得" },
            new WCFDataAccessConfig() { Name = "TKS02010_GetPrintData", ServiceClass = "TKS02010", MethodName = "GetPrintData", Descprition = "売上集計処理、一覧表印刷データ取得" },

            // 納品書出力
            new WCFDataAccessConfig() { Name = "DLY11010_Print", ServiceClass = "DLY11010", MethodName = "GetPrintData", Descprition = "納品書出力情報取得" },

            // 請求締集計
            new WCFDataAccessConfig() { Name = "GetAggrListData", ServiceClass = "TKS01010", MethodName = "GetListData", Descprition = "請求集計対象取得" },
            new WCFDataAccessConfig() { Name = "BillingAggregation", ServiceClass = "TKS01010", MethodName = "BillingAggregation", Descprition = "請求締集計処理" },

            // 請求書発行
            new WCFDataAccessConfig() { Name = "TKS01020_GetDataList", ServiceClass = "TKS01020", MethodName = "GetDataList", Descprition = "請求書発行検索情報取得" },
            new WCFDataAccessConfig() { Name = "TKS01020_GetPrintData", ServiceClass = "TKS01020", MethodName = "GetPrintData", Descprition = "請求書印刷データ取得" },

            // 請求一覧表
            new WCFDataAccessConfig() { Name = "TKS03010_GetCsvData", ServiceClass = "TKS03010", MethodName = "GetCsvData", Descprition = "請求一覧表ＣＳＶデータ取得" },
            new WCFDataAccessConfig() { Name = "TKS03010_GetPrintData", ServiceClass = "TKS03010", MethodName = "GetPrintData", Descprition = "請求一覧表印刷データ取得" },

            // 入金予定表
            new WCFDataAccessConfig() { Name = "TKS07010_GetCsvData", ServiceClass = "TKS07010", MethodName = "GetCsvData", Descprition = "入金予定表ＣＳＶデータ取得" },
            new WCFDataAccessConfig() { Name = "TKS07010_GetPrintData", ServiceClass = "TKS07010", MethodName = "GetPrintData", Descprition = "入金予定表印刷データ取得" },

            // 回収予定実績表
            new WCFDataAccessConfig() { Name = "TKS08010_GetCsvData", ServiceClass = "TKS08010", MethodName = "GetCsvData", Descprition = "回収予定実績表ＣＳＶデータ取得" },
            new WCFDataAccessConfig() { Name = "TKS08010_GetPrintData", ServiceClass = "TKS08010", MethodName = "GetPrintData", Descprition = "回収予定実績表印刷データ取得" },

            // 都度請求締集計
            new WCFDataAccessConfig() { Name = "TKS09010_BillingAggregation", ServiceClass = "TKS09010", MethodName = "BillingAggregation", Descprition = "都度請求締集計処理" },

            #endregion

            #region 支払先管理

            // 支払締集計
            new WCFDataAccessConfig() { Name = "SHR03010_GetDataList", ServiceClass = "SHR03010", MethodName = "GetListData", Descprition = "支払集計対象取得" },
            new WCFDataAccessConfig() { Name = "SHR03010_PaymentAggregation", ServiceClass = "SHR03010", MethodName = "PaymentAggregation", Descprition = "支払締集計処理" },

            // 支払一覧表
            new WCFDataAccessConfig() { Name = "SHR05020_GetCsvData", ServiceClass = "SHR05020", MethodName = "GetCsvData", Descprition = "支払明細表ＣＳＶデータ取得" },
            new WCFDataAccessConfig() { Name = "SHR05020_GetPrintData", ServiceClass = "SHR05020", MethodName = "GetPrintData", Descprition = "支払明細表印刷データ取得" },

            // 支払一覧表
            new WCFDataAccessConfig() { Name = "SHR05010_GetCsvData", ServiceClass = "SHR05010", MethodName = "GetCsvData", Descprition = "支払一覧表ＣＳＶデータ取得" },
            new WCFDataAccessConfig() { Name = "SHR05010_GetPrintData", ServiceClass = "SHR05010", MethodName = "GetPrintData", Descprition = "支払一覧表印刷データ取得" },

            // 出金予定表
            new WCFDataAccessConfig() { Name = "SHR06010_GetCsvData", ServiceClass = "SHR06010", MethodName = "GetCsvData", Descprition = "出金予定表ＣＳＶデータ取得" },
            new WCFDataAccessConfig() { Name = "SHR06010_GetPrintData", ServiceClass = "SHR06010", MethodName = "GetPrintData", Descprition = "出金予定表印刷データ取得" },

            #endregion

            #region 在庫管理

            // 在庫締集計
            new WCFDataAccessConfig() { Name = "ZIK01010_IsCheckSummary", ServiceClass = "ZIK01010", MethodName = "IsCheckSummary", Descprition = "締集計作成済みかを取得する" },
            new WCFDataAccessConfig() { Name = "ZIK01010_InventorySummary", ServiceClass = "ZIK01010", MethodName = "InventorySummary", Descprition = "在庫締集計処理" },
            new WCFDataAccessConfig() { Name = "ZIK01010_GetPrintData", ServiceClass = "ZIK01010", MethodName = "GetPrintData", Descprition = "在庫締帳票データ取得" },

            #endregion

            #region 分析資料

            // 得意先・商品別売上統計表
            new WCFDataAccessConfig() { Name = "BSK01010_GetPrintList", ServiceClass = "BSK01010", MethodName = "GetPrintList", Descprition = "帳票印字データを取得" },
            new WCFDataAccessConfig() { Name = "BSK01010_GetCsvList", ServiceClass = "BSK01010", MethodName = "GetPrintList", Descprition = "CSV出力データを取得" },

            // 得意先別売上統計表
            new WCFDataAccessConfig() { Name = "BSK02010_GetPrintList", ServiceClass = "BSK02010", MethodName = "GetPrintList", Descprition = "帳票印字データを取得" },
            new WCFDataAccessConfig() { Name = "BSK02010_GetCsvList", ServiceClass = "BSK02010", MethodName = "GetPrintList", Descprition = "CSV出力データを取得" },

            // シリーズ･商品別売上統計表
            new WCFDataAccessConfig() { Name = "BSK03010_GetPrintList", ServiceClass = "BSK03010", MethodName = "GetPrintList", Descprition = "帳票印字データを取得" },
            new WCFDataAccessConfig() { Name = "BSK03010_GetCsvList", ServiceClass = "BSK03010", MethodName = "GetPrintList", Descprition = "CSV出力データを取得" },

            // 年次販社売上調整
            new WCFDataAccessConfig() { Name = "BSK05010_GetDataList", ServiceClass = "BSK05010", MethodName = "GetDataList", Descprition = "売上調整計算をおこなう" },
            new WCFDataAccessConfig() { Name = "BSK05010_SetCalculate", ServiceClass = "BSK05010", MethodName = "SetCalculate", Descprition = "売上調整計算をおこなう" },
            new WCFDataAccessConfig() { Name = "BSK05010_SetConfirm", ServiceClass = "BSK05010", MethodName = "SetConfirm", Descprition = "売上調整計算の確定処理をおこなう" },

            // 相殺請求管理表
            new WCFDataAccessConfig() { Name = "BSK09010_GetPrintList", ServiceClass = "BSK09010", MethodName = "GetPrintList", Descprition = "相殺請求印字データを取得" },
            new WCFDataAccessConfig() { Name = "BSK09010_GetCsvList", ServiceClass = "BSK09010", MethodName = "GetPrintList", Descprition = "相殺請求出力データを取得" },

            #endregion

            #endregion

            #region 旧定義
            // 各種KEY変換(T01_TRN用) 
            //new WCFDataAccessConfig() { Name = "T01_TRN", ServiceClass = "KeyChage", MethodName = "T01_TRN_KeyChage", Descprition = "T01_TRN_KEY変換", },

			// 各種KEY変換(T02_UTRN用) 
            //new WCFDataAccessConfig() { Name = "T02_UTRN", ServiceClass = "KeyChage", MethodName = "T02_UTRN_KeyChage", Descprition = "T02_UTRN_KEY変換", },

			// 各種KEY変換(T03_KTRN用) 
            //new WCFDataAccessConfig() { Name = "T03_KTRN", ServiceClass = "KeyChage", MethodName = "T03_KTRN_KeyChage", Descprition = "T03_KTRN_KEY変換", },

			// 各種KEY変換(T04_NYUK用) 
            //new WCFDataAccessConfig() { Name = "T04_NYUK", ServiceClass = "KeyChage", MethodName = "T04_NYUK_KeyChage", Descprition = "T04_NYUK_KEY変換", },

			// 各種KEY変換(T05_TTRN用) 
            //new WCFDataAccessConfig() { Name = "T05_TTRN", ServiceClass = "KeyChage", MethodName = "T05_TTRN_KeyChage", Descprition = "T05_TTRN_KEY変換", },

			// 乗務員マスター検索用 
            //new WCFDataAccessConfig() { Name = "SCH04010", ServiceClass = "M04", MethodName = "GetListSCH04010", Descprition = "乗務員マスター検索", },

			// 乗務員マスターメンテ用 
            //new WCFDataAccessConfig() { Name = "M_M04_DRV", ServiceClass = "M04", MethodName = "GetData", Descprition = "乗務員マスターメンテ用", },
            //new WCFDataAccessConfig() { Name = "RM_M04_DRV", ServiceClass = "M04", MethodName = "RGetData", Descprition = "乗務員マスターメンテ用類似用", },
            //new WCFDataAccessConfig() { Name = "M_M04_DRVPIC", ServiceClass = "M04", MethodName = "GetDataDrvpic", Descprition = "乗務員マスターメンテ用", },
            //new WCFDataAccessConfig() { Name = "M04_DDT1", ServiceClass = "M04", MethodName = "GetListDDT1", Descprition = "乗務員マスターメンテ用", },
            //new WCFDataAccessConfig() { Name = "M04_DDT2", ServiceClass = "M04", MethodName = "GetListDDT2", Descprition = "乗務員マスターメンテ用", },
            //new WCFDataAccessConfig() { Name = "M04_DDT3", ServiceClass = "M04", MethodName = "GetListDDT3", Descprition = "乗務員マスターメンテ用", },
            //new WCFDataAccessConfig() { Name = "M04_DDT1_ALL", ServiceClass = "M04", MethodName = "GetAllDDT1", Descprition = "乗務員マスターメンテ・一括・適正診断", },
            //new WCFDataAccessConfig() { Name = "M04_DDT2_ALL", ServiceClass = "M04", MethodName = "GetAllDDT2", Descprition = "乗務員マスターメンテ・一括・事故違反", },
            //new WCFDataAccessConfig() { Name = "M04_DDT3_ALL", ServiceClass = "M04", MethodName = "GetAllDDT3", Descprition = "乗務員マスターメンテ・一括・特別教育", },
            //new WCFDataAccessConfig() { Name = "M04_DDT_ADD", ServiceClass = "M04", MethodName = "PutALLDDT", Descprition = "乗務員マスターメンテ・一括更新", },
            //new WCFDataAccessConfig() { Name = "M_M04_DRV_UPD", ServiceClass = "M04", MethodName = "UpdateAll", Descprition = "乗務員マスターメンテ 更新処理", },
            //new WCFDataAccessConfig() { Name = "M_M04_DRV_DEL", ServiceClass = "M04", MethodName = "DeleteDriver", Descprition = "乗務員マスターメンテ 削除処理", },
            //new WCFDataAccessConfig() { Name = "M04_DRV_LIST", ServiceClass = "M04", MethodName = "GetListMST04010Report", Descprition = "乗務員マスター  一覧印刷用", },
            //new WCFDataAccessConfig() { Name = "M04_DRV_LIST_CSV", ServiceClass = "M04", MethodName = "GetListMST04010Report", Descprition = "乗務員マスター  一覧印刷用", },
            //new WCFDataAccessConfig() { Name = "M_M04_DRV_NEXT", ServiceClass = "M04", MethodName = "GetListMST04010Next", Descprition = "乗務員マスター  採番", },
            //new WCFDataAccessConfig() { Name = "GetListJMI", ServiceClass = "M04", MethodName = "GetListJMIAllData", Descprition = "乗務員台帳", },
            //new WCFDataAccessConfig() { Name = "DRV_ID_CHG", ServiceClass = "M04", MethodName = "DRV_ID_CHG", Descprition = "乗務員マスターメンテ用（ID変換）", },
            //new WCFDataAccessConfig() { Name = "M04_GetMaxMeisaiNo", ServiceClass = "M04", MethodName = "GetMaxMeisaiCount", Descprition = "現在の登録件数", },



			// 取引先マスターメンテ用 
            //new WCFDataAccessConfig() { Name = "M_M01_TOK", ServiceClass = "M01", MethodName = "GetDataTok", Descprition = "取引先マスターメンテ用（取得）", },
            //new WCFDataAccessConfig() { Name = "R_M_M01_TOK", ServiceClass = "M01", MethodName = "GetDataTok", Descprition = "取引先マスターメンテ用（類似）", },
            //new WCFDataAccessConfig() { Name = "M_M01_YOS", ServiceClass = "M01", MethodName = "GetDataTok", Descprition = "取引先(傭車)マスターメンテ用（取得）", },
            //new WCFDataAccessConfig() { Name = "M_M01_TOK_ADD", ServiceClass = "M01", MethodName = "Insert", Descprition = "取引先マスターメンテ用（新規）", },
            //new WCFDataAccessConfig() { Name = "M_M01_TOK_PUT", ServiceClass = "M01", MethodName = "Update", Descprition = "取引先マスターメンテ用（更新）", },
            //new WCFDataAccessConfig() { Name = "M_M01_TOK_DEL", ServiceClass = "M01", MethodName = "Delete", Descprition = "取引先マスターメンテ用（削除）", },
            //new WCFDataAccessConfig() { Name = "M_M01_TOK_NEXT", ServiceClass = "M01", MethodName = "GetNextNumber", Descprition = "取引先マスターメンテ用（自動採番）", },
            //new WCFDataAccessConfig() { Name = "TokID_CHG", ServiceClass = "M01", MethodName = "TokID_CHG", Descprition = "取引先マスターメンテ用（ID変換）", },
            //new WCFDataAccessConfig() { Name = "M01_GetMaxMeisaiNo", ServiceClass = "M01", MethodName = "GetMaxMeisaiCount", Descprition = "現在の登録件数", },

            //new WCFDataAccessConfig() { Name = "M01_TOK_LIST", ServiceClass = "M01", MethodName = "GetMasterList", Descprition = "取引先マスタ一覧取得", },
            //new WCFDataAccessConfig() { Name = "M01_TOK_LIST_CSV", ServiceClass = "M01", MethodName = "GetMasterList_CSV", Descprition = "取引先マスタ一覧取得", },

            // 取引先期首残一括
            //new WCFDataAccessConfig() { Name = "M01_KIS_ALL", ServiceClass = "M01", MethodName = "GetDataKisyu", Descprition = "取引先期首残一括データ取得", },
            //new WCFDataAccessConfig() { Name = "M01_KIS_UPD", ServiceClass = "M01", MethodName = "UpdateKisyu", Descprition = "取引先期首残一括データ取得", },

            // 削除得意先復活用
            //new WCFDataAccessConfig() { Name = "M01_DEL_ALL", ServiceClass = "M01", MethodName = "GetDataResurrection", Descprition = "取引先マスターメンテ用（復活）", },
            //new WCFDataAccessConfig() { Name = "M01_DEL_UPD", ServiceClass = "M01", MethodName = "Resurrection", Descprition = "取引先マスターメンテ用（復活削除）", },

			//得意先検索画面用
            //new WCFDataAccessConfig() { Name = "M01_TOK_TOKU_SCH", ServiceClass = "M01", MethodName = "GetTokuDataSCH", Descprition = "得意先検索", },
            //new WCFDataAccessConfig() { Name = "M01_ALLData", ServiceClass = "M01", MethodName = "GetAllDataSCH", Descprition = "得意先検索", },
            //new WCFDataAccessConfig() { Name = "M01_TOK_TOKU_SCH02", ServiceClass = "M01", MethodName = "GetTokuDataSCH02", Descprition = "得意先検索請求内訳用", },

			//支払先検索画面用
            //new WCFDataAccessConfig() { Name = "M01_TOK_SHIHARAI_SCH", ServiceClass = "M01", MethodName = "GetShiharaiDataSCH", Descprition = "支払先検索", },

			//仕入先検索画面用
            //new WCFDataAccessConfig() { Name = "M01_TOK_SHIIRE_SCH", ServiceClass = "M01", MethodName = "GetShiireDataSCH", Descprition = "仕入先検索", },

			//全仕入先検索画面用
            //new WCFDataAccessConfig() { Name = "M01_TOK_ZENSHIHARAI_SCH", ServiceClass = "M01", MethodName = "GetZENShiireDataSCH", Descprition = "全仕入先検索", },

			// 運転日報　DLY01010 
            //new WCFDataAccessConfig() { Name = "DLY01010_1", ServiceClass = "DLY01010", MethodName = "GetListDrive", Descprition = "運転日報", },
            //new WCFDataAccessConfig() { Name = "DLY01010_MAXNO", ServiceClass = "DLY01010", MethodName = "GetMaxMeisaiNo", Descprition = "運転日報（明細番号最大値）", },
            //new WCFDataAccessConfig() { Name = "DLY01010_GETNO", ServiceClass = "DLY01010", MethodName = "GetMeisaiNo", Descprition = "運転日報（明細番号移動検索）", },
            //new WCFDataAccessConfig() { Name = "DLY01010_3", ServiceClass = "DLY01010", MethodName = "GetListKeihi", Descprition = "運転日報（経費データ）", },
            //new WCFDataAccessConfig() { Name = "DLY01010_DEFAULT_K", ServiceClass = "DLY01010", MethodName = "GetDefaultKeihi", Descprition = "運転日報（経費データ）", },
            //new WCFDataAccessConfig() { Name = "DLY01010_KEIHI_NAME", ServiceClass = "DLY01010", MethodName = "GetKeihiName", Descprition = "運転日報（経費項目名取得）", },
            //new WCFDataAccessConfig() { Name = "DLY01010_SHR_NAME", ServiceClass = "DLY01010", MethodName = "GetSiharaisakiName", Descprition = "運転日報（経費支払先名取得）", },
            //new WCFDataAccessConfig() { Name = "DLY01010_TEKIYO_NAME", ServiceClass = "DLY01010", MethodName = "GetTekiyoName", Descprition = "運転日報（摘要名取得）", },
            //new WCFDataAccessConfig() { Name = "DLY01010_KEIYU_ZEI", ServiceClass = "DLY01010", MethodName = "GetKeiyuZeiritu", Descprition = "運転日報（軽油税率取得）", },
            //new WCFDataAccessConfig() { Name = "DLY01010_PUTALL", ServiceClass = "DLY01010", MethodName = "PutAllData", Descprition = "運転日報登録", },
            //new WCFDataAccessConfig() { Name = "DLY01010_DELALL", ServiceClass = "DLY01010", MethodName = "DeleteAllData", Descprition = "運転日報削除", },
            //new WCFDataAccessConfig() { Name = "DLY01010_TANKA", ServiceClass = "DLY01010", MethodName = "GetUnitCost", Descprition = "運転日報（運行データ）", },
            //new WCFDataAccessConfig() { Name = "DLY01010_GetMaxMeisaiNo", ServiceClass = "DLY01010", MethodName = "GetMaxMeisaiCount", Descprition = "現在の登録件数", },
            //new WCFDataAccessConfig() { Name = "DLY01010_TRN_RIREKI", ServiceClass = "DLY01010", MethodName = "TRN_RIREKI", Descprition = "売上履歴", },


			// 売上伝票入力　DLY02010 
            //new WCFDataAccessConfig() { Name = "DLY02010_1", ServiceClass = "DLY02010", MethodName = "GetListDrive", Descprition = "売上伝票入力", },
            //new WCFDataAccessConfig() { Name = "DLY02010_MAXNO", ServiceClass = "DLY02010", MethodName = "GetMaxMeisaiNo", Descprition = "売上伝票入力（明細番号最大値）", },
            //new WCFDataAccessConfig() { Name = "DLY02010_GETNO", ServiceClass = "DLY02010", MethodName = "GetMeisaiNo", Descprition = "売上伝票入力（明細番号移動検索）", },
            //new WCFDataAccessConfig() { Name = "DLY02010_TEKIYO_NAME", ServiceClass = "DLY02010", MethodName = "GetTekiyoName", Descprition = "売上伝票入力（摘要名取得）", },
            //new WCFDataAccessConfig() { Name = "DLY02010_PUTALL", ServiceClass = "DLY02010", MethodName = "PutAllData", Descprition = "売上伝票入力登録", },
            //new WCFDataAccessConfig() { Name = "DLY02010_DELALL", ServiceClass = "DLY02010", MethodName = "DeleteAllData", Descprition = "売上伝票入力削除", },
            //new WCFDataAccessConfig() { Name = "DLY02010_TANKA", ServiceClass = "DLY01010", MethodName = "GetUnitCost", Descprition = "売上伝票入力（運行データ）", },
            //new WCFDataAccessConfig() { Name = "DLY02010_GetMaxMeisaiNo", ServiceClass = "DLY02010", MethodName = "GetMaxMeisaiCount", Descprition = "現在の登録件数", },
            //new WCFDataAccessConfig() { Name = "DLY02010_TRN_RIREKI", ServiceClass = "DLY02010", MethodName = "TRN_RIREKI", Descprition = "売上履歴", },

			// 売上伝票入力　DLY02015 
            //new WCFDataAccessConfig() { Name = "DLY02015_1", ServiceClass = "DLY02015", MethodName = "GetListDrive", Descprition = "売上伝票入力", },
            //new WCFDataAccessConfig() { Name = "DLY02015_MAXNO", ServiceClass = "DLY02015", MethodName = "GetMaxMeisaiNo", Descprition = "売上伝票入力（明細番号最大値）", },
            //new WCFDataAccessConfig() { Name = "DLY02015_GETNO", ServiceClass = "DLY02015", MethodName = "GetMeisaiNo", Descprition = "売上伝票入力（明細番号移動検索）", },
            //new WCFDataAccessConfig() { Name = "DLY02015_TEKIYO_NAME", ServiceClass = "DLY02015", MethodName = "GetTekiyoName", Descprition = "売上伝票入力（摘要名取得）", },
            //new WCFDataAccessConfig() { Name = "DLY02015_PUTALL", ServiceClass = "DLY02015", MethodName = "PutAllData", Descprition = "売上伝票入力登録", },
            //new WCFDataAccessConfig() { Name = "DLY02015_DELALL", ServiceClass = "DLY02015", MethodName = "DeleteAllData", Descprition = "売上伝票入力削除", },
            //new WCFDataAccessConfig() { Name = "DLY02015_TANKA", ServiceClass = "DLY01010", MethodName = "GetUnitCost", Descprition = "売上伝票入力（運行データ）", },
            //new WCFDataAccessConfig() { Name = "DLY02015_GetMaxMeisaiNo", ServiceClass = "DLY02015", MethodName = "GetMaxMeisaiCount", Descprition = "現在の登録件数", },
            //new WCFDataAccessConfig() { Name = "DLY02015_TRN_RIREKI", ServiceClass = "DLY02015", MethodName = "TRN_RIREKI", Descprition = "売上履歴", },

            // 配車依頼書 DLY02020
            //new WCFDataAccessConfig() { Name = "SEARCH_DLY02020_FAX" , ServiceClass = "DLY02020",MethodName = "SEARCH_DLY02020_FAX", Descprition = "傭車FAX情報取得"},
            //new WCFDataAccessConfig() { Name = "SEARCH_DLY02020_JISHA" , ServiceClass = "DLY02020",MethodName = "SEARCH_DLY02020_JISHA", Descprition = "自社情報取得"},
            //new WCFDataAccessConfig() { Name = "SEARCH_DLY02020_TUMICHI", ServiceClass = "DLY02020", MethodName = "SEARCH_DLY02020_TUMICHI", Descprition = "積地情報取得", },
            //new WCFDataAccessConfig() { Name = "SEARCH_DLY02020_OROSHICHI" , ServiceClass = "DLY02020",MethodName = "SEARCH_DLY02020_OROSHICHI", Descprition = "卸地情報取得"},
            //new WCFDataAccessConfig() { Name = "SEARCH_DLY02020" , ServiceClass = "DLY02020",MethodName = "SEARCH_DLY02020", Descprition = "印刷情報取得"},

			// 日報入力　DLY03010 
            //new WCFDataAccessConfig() { Name = "DLY03010_1", ServiceClass = "DLY03010", MethodName = "GetListDrive", Descprition = "日報入力", },
            //new WCFDataAccessConfig() { Name = "DLY03010_MAXNO", ServiceClass = "DLY03010", MethodName = "GetMaxMeisaiNo", Descprition = "日報入力（明細番号最大値）", },
            //new WCFDataAccessConfig() { Name = "DLY03010_GETNO", ServiceClass = "DLY03010", MethodName = "GetMeisaiNo", Descprition = "日報入力（明細番号移動検索）", },
            //new WCFDataAccessConfig() { Name = "DLY03010_TEKIYO_NAME", ServiceClass = "DLY03010", MethodName = "GetTekiyoName", Descprition = "日報入力（摘要名取得）", },
            //new WCFDataAccessConfig() { Name = "DLY03010_PUTALL", ServiceClass = "DLY03010", MethodName = "PutAllData", Descprition = "日報入力登録", },
            //new WCFDataAccessConfig() { Name = "DLY03010_DELALL", ServiceClass = "DLY03010", MethodName = "DeleteAllData", Descprition = "日報入力削除", },
            //new WCFDataAccessConfig() { Name = "DLY03010_TANKA", ServiceClass = "DLY03010", MethodName = "GetUnitCost", Descprition = "日報入力（運行データ）", },
            //new WCFDataAccessConfig() { Name = "DLY03010_GetMaxMeisaiNo", ServiceClass = "DLY03010", MethodName = "GetMaxMeisaiCount", Descprition = "現在の登録件数", },


			// 経費伝票入力　DLY07010 
            //new WCFDataAccessConfig() { Name = "DLY07010_1", ServiceClass = "DLY07010", MethodName = "GetListDrive", Descprition = "経費伝票入力", },
            //new WCFDataAccessConfig() { Name = "DLY07010_MAXNO", ServiceClass = "DLY07010", MethodName = "GetMaxMeisaiNo", Descprition = "経費伝票入力（明細番号最大値）", },
            //new WCFDataAccessConfig() { Name = "DLY07010_Mainte", ServiceClass = "DLY07010", MethodName = "GetMainte", Descprition = "経費伝票入力（明細番号最大値）", },
            //new WCFDataAccessConfig() { Name = "DLY07010_MAXNO_Update", ServiceClass = "DLY07010", MethodName = "MaxMeisaiNoUpdate", Descprition = "経費伝票入力（明細番号最大値更新）", },
            //new WCFDataAccessConfig() { Name = "DLY07010_GETNO", ServiceClass = "DLY07010", MethodName = "GetMeisaiNo", Descprition = "経費伝票入力（明細番号移動検索）", },
            //new WCFDataAccessConfig() { Name = "DLY07010_3", ServiceClass = "DLY07010", MethodName = "GetListKeihi", Descprition = "経費伝票入力（経費データ）", },
            //new WCFDataAccessConfig() { Name = "DLY07010_DEFAULT_K", ServiceClass = "DLY07010", MethodName = "GetDefaultKeihi", Descprition = "経費伝票入力（経費データ）", },
            //new WCFDataAccessConfig() { Name = "DLY07010_KEIHI_NAME", ServiceClass = "DLY07010", MethodName = "GetKeihiName", Descprition = "経費伝票入力（経費項目名取得）", },
            //new WCFDataAccessConfig() { Name = "DLY07010_SHR_NAME", ServiceClass = "DLY07010", MethodName = "GetSiharaisakiName", Descprition = "経費伝票入力（経費支払先名取得）", },
            //new WCFDataAccessConfig() { Name = "DLY07010_TEKIYO_NAME", ServiceClass = "DLY07010", MethodName = "GetTekiyoName", Descprition = "経費伝票入力（摘要名取得）", },
            //new WCFDataAccessConfig() { Name = "DLY07010_JISHA_NAME", ServiceClass = "DLY07010", MethodName = "GetJishaName", Descprition = "経費伝票入力（自社部門名取得）", },
            //new WCFDataAccessConfig() { Name = "DLY07010_JYOMUIN_NAME", ServiceClass = "DLY07010", MethodName = "GetJyomuinName", Descprition = "経費伝票入力（乗務員名取得）", },
            //new WCFDataAccessConfig() { Name = "DLY07010_SHARYO_NAME", ServiceClass = "DLY07010", MethodName = "GetSyaryoName", Descprition = "経費伝票入力（乗務員名取得）", },
            //new WCFDataAccessConfig() { Name = "DLY07010_KEIYU_ZEI", ServiceClass = "DLY07010", MethodName = "GetKeiyuZeiritu", Descprition = "経費伝票入力（軽油税率取得）", },
            //new WCFDataAccessConfig() { Name = "DLY07010_NENRYO_TANKA", ServiceClass = "DLY07010", MethodName = "GetNenryoSum", Descprition = "経費伝票入力（燃料単価取得）", },
            //new WCFDataAccessConfig() { Name = "DLY07010_PUTALL", ServiceClass = "DLY07010", MethodName = "PutAllData", Descprition = "経費伝票入力登録", },
            //new WCFDataAccessConfig() { Name = "DLY07010_DELALL", ServiceClass = "DLY07010", MethodName = "DeleteAllData", Descprition = "経費伝票入力一括削除", },
            //new WCFDataAccessConfig() { Name = "DLY07010_TANKA", ServiceClass = "DLY07010", MethodName = "GetUnitCost", Descprition = "運転日報（運行データ）", },
            //new WCFDataAccessConfig() { Name = "DLY07010_GetMaxMeisaiNo", ServiceClass = "DLY07010", MethodName = "GetMaxMeisaiCount", Descprition = "現在の登録件数", },


			// 入金伝票入力　DLY08010 
            //new WCFDataAccessConfig() { Name = "GetDetailsNumber", ServiceClass = "DLY08010", MethodName = "DLY08010_GetData", Descprition = "入金伝票データ取得", },
            //new WCFDataAccessConfig() { Name = "DLY08010_NData", ServiceClass = "DLY08010", MethodName = "DLY08010_GETNData", Descprition = "入金予定額取得", },
            //new WCFDataAccessConfig() { Name = "DLY08010_OData", ServiceClass = "DLY08010", MethodName = "DLY08010_GETOData", Descprition = "入金予定額取得", },
            //new WCFDataAccessConfig() { Name = "New_Details", ServiceClass = "DLY08010", MethodName = "DLY08010_GetNEXTData", Descprition = "明細番号採番", },
            //new WCFDataAccessConfig() { Name = "DLY08010_TEKIYO_NAME", ServiceClass = "DLY08010", MethodName = "GetTekiyoName", Descprition = "SpreadでのF1機能", },
            //new WCFDataAccessConfig() { Name = "DLY08010_UPDATE", ServiceClass = "DLY08010", MethodName = "DLY08010_UPDATE", Descprition = "登録・編集", },
            //new WCFDataAccessConfig() { Name = "DLY08010_DELETE", ServiceClass = "DLY08010", MethodName = "DLY08010_DELETE", Descprition = "削除", },
            //new WCFDataAccessConfig() { Name = "DLY08010_GETNO", ServiceClass = "DLY08010", MethodName = "GetMeisaiNo", Descprition = "運転日報（明細番号移動検索）", },
            //new WCFDataAccessConfig() { Name = "DLY08010_GetMaxMeisaiNo", ServiceClass = "DLY08010", MethodName = "GetMaxMeisaiCount", Descprition = "現在の登録件数", },


			// 出金伝票入力　DLY09010 
            //new WCFDataAccessConfig() { Name = "GetDetailsNumber1", ServiceClass = "DLY09010", MethodName = "DLY09010_GetData", Descprition = "出金伝票データ取得", },
            //new WCFDataAccessConfig() { Name = "DLY09010_NData", ServiceClass = "DLY09010", MethodName = "DLY09010_GETNData", Descprition = "出金予定額取得", },
            //new WCFDataAccessConfig() { Name = "DLY09010_OData", ServiceClass = "DLY09010", MethodName = "DLY09010_GETOData", Descprition = "出金予定額取得", },
            //new WCFDataAccessConfig() { Name = "New_Details", ServiceClass = "DLY09010", MethodName = "DLY09010_GetNEXTData", Descprition = "明細番号採番", },
            //new WCFDataAccessConfig() { Name = "DLY09010_TEKIYO_NAME", ServiceClass = "DLY09010", MethodName = "GetTekiyoName", Descprition = "SpreadでのF1機能", },
            //new WCFDataAccessConfig() { Name = "DLY09010_UPDATE", ServiceClass = "DLY09010", MethodName = "DLY09010_UPDATE", Descprition = "登録・編集", },
            //new WCFDataAccessConfig() { Name = "DLY09010_DELETE", ServiceClass = "DLY09010", MethodName = "DLY09010_DELETE", Descprition = "削除", },
            //new WCFDataAccessConfig() { Name = "DLY09010_GETNO", ServiceClass = "DLY09010", MethodName = "GetMeisaiNo", Descprition = "運転日報（明細番号移動検索）", },
            //new WCFDataAccessConfig() { Name = "DLY09010_GetMaxMeisaiNo", ServiceClass = "DLY09010", MethodName = "GetMaxMeisaiCount", Descprition = "現在の登録件数", },


			// 売上明細（画面）　DLY10010 
            //new WCFDataAccessConfig() { Name = "DLY10010", ServiceClass = "DLY10010", MethodName = "GetListDLY10010", Descprition = "売上明細", },
            //new WCFDataAccessConfig() { Name = "DLY10010_UPDATE", ServiceClass = "DLY10010", MethodName = "UpdateColumn", Descprition = "売上明細", },
            //new WCFDataAccessConfig() { Name = "DLY10010_UPDATE2", ServiceClass = "DLY10010", MethodName = "UpdateColumn2", Descprition = "売上明細", },
            //new WCFDataAccessConfig() { Name = "DLY10010_MST1", ServiceClass = "DLY10010", MethodName = "GetMasterList", Descprition = "売上明細検索条件用マスタ一覧(得意先)", },
            //new WCFDataAccessConfig() { Name = "DLY10010_MST2", ServiceClass = "DLY10010", MethodName = "GetMasterList", Descprition = "売上明細検索条件用マスタ一覧(請求先)", },
            //new WCFDataAccessConfig() { Name = "DLY10010_MST3", ServiceClass = "DLY10010", MethodName = "GetMasterList", Descprition = "売上明細検索条件用マスタ一覧(支払先)", },
            //new WCFDataAccessConfig() { Name = "DLY10010_MST4", ServiceClass = "DLY10010", MethodName = "GetMasterList", Descprition = "売上明細検索条件用マスタ一覧(乗務員)", },
            //new WCFDataAccessConfig() { Name = "DLY10010_MST5", ServiceClass = "DLY10010", MethodName = "GetMasterList", Descprition = "売上明細検索条件用マスタ一覧(車輌)", },
            //new WCFDataAccessConfig() { Name = "DLY10010_MST6", ServiceClass = "DLY10010", MethodName = "GetMasterList", Descprition = "売上明細検索条件用マスタ一覧(車種)", },
            //new WCFDataAccessConfig() { Name = "DLY10010_MST7", ServiceClass = "DLY10010", MethodName = "GetMasterList", Descprition = "売上明細検索条件用マスタ一覧(発地)", },
            //new WCFDataAccessConfig() { Name = "DLY10010_MST8", ServiceClass = "DLY10010", MethodName = "GetMasterList", Descprition = "売上明細検索条件用マスタ一覧(着地)", },
            //new WCFDataAccessConfig() { Name = "DLY10010_MST9", ServiceClass = "DLY10010", MethodName = "GetMasterList", Descprition = "売上明細検索条件用マスタ一覧(商品)", },
            //new WCFDataAccessConfig() { Name = "DLY10010_TANKA_TOK", ServiceClass = "DLY10010", MethodName = "GetUnitCostTOK", Descprition = "売上金額・売上単価計算", },
            //new WCFDataAccessConfig() { Name = "DLY10010_TANKA_SHR", ServiceClass = "DLY10010", MethodName = "GetUnitCostSHR", Descprition = "支払金額・支払単価計算", },

			// 支払明細（画面）　DLY11010 
            //new WCFDataAccessConfig() { Name = "DLY11010", ServiceClass = "DLY11010", MethodName = "GetListDLY11010", Descprition = "売上明細", },
            //new WCFDataAccessConfig() { Name = "DLY11010_UPDATE", ServiceClass = "DLY11010", MethodName = "UpdateColumn", Descprition = "売上明細", },
            //new WCFDataAccessConfig() { Name = "DLY11010_UPDATE2", ServiceClass = "DLY11010", MethodName = "UpdateColumn2", Descprition = "売上明細", },
            //new WCFDataAccessConfig() { Name = "DLY11010_MST1", ServiceClass = "DLY11010", MethodName = "GetMasterList", Descprition = "売上明細検索条件用マスタ一覧(得意先)", },
            //new WCFDataAccessConfig() { Name = "DLY11010_MST2", ServiceClass = "DLY11010", MethodName = "GetMasterList", Descprition = "売上明細検索条件用マスタ一覧(請求先)", },
            //new WCFDataAccessConfig() { Name = "DLY11010_MST3", ServiceClass = "DLY11010", MethodName = "GetMasterList", Descprition = "売上明細検索条件用マスタ一覧(支払先)", },
            //new WCFDataAccessConfig() { Name = "DLY11010_MST4", ServiceClass = "DLY11010", MethodName = "GetMasterList", Descprition = "売上明細検索条件用マスタ一覧(乗務員)", },
            //new WCFDataAccessConfig() { Name = "DLY11010_MST5", ServiceClass = "DLY11010", MethodName = "GetMasterList", Descprition = "売上明細検索条件用マスタ一覧(車輌)", },
            //new WCFDataAccessConfig() { Name = "DLY11010_MST6", ServiceClass = "DLY11010", MethodName = "GetMasterList", Descprition = "売上明細検索条件用マスタ一覧(車種)", },
            //new WCFDataAccessConfig() { Name = "DLY11010_MST7", ServiceClass = "DLY11010", MethodName = "GetMasterList", Descprition = "売上明細検索条件用マスタ一覧(発地)", },
            //new WCFDataAccessConfig() { Name = "DLY11010_MST8", ServiceClass = "DLY11010", MethodName = "GetMasterList", Descprition = "売上明細検索条件用マスタ一覧(着地)", },
            //new WCFDataAccessConfig() { Name = "DLY11010_MST9", ServiceClass = "DLY11010", MethodName = "GetMasterList", Descprition = "売上明細検索条件用マスタ一覧(商品)", },
            //new WCFDataAccessConfig() { Name = "SHRCHE_DLY11010_Pri", ServiceClass = "DLY11010", MethodName = "GetListDLY11010_Pri", Descprition = "支払明細問合せ", },
            //new WCFDataAccessConfig() { Name = "DLY11010_TANKA_TOK", ServiceClass = "DLY11010", MethodName = "GetUnitCostTOK", Descprition = "売上金額・売上単価計算", },
            //new WCFDataAccessConfig() { Name = "DLY11010_TANKA_SHR", ServiceClass = "DLY11010", MethodName = "GetUnitCostSHR", Descprition = "支払金額・支払単価計算", },

			// 乗務員明細（画面）　DLY12010 
            //new WCFDataAccessConfig() { Name = "DLY12010", ServiceClass = "DLY12010", MethodName = "GetListDLY12010", Descprition = "乗務員明細", },
            //new WCFDataAccessConfig() { Name = "DLY12010_PRT", ServiceClass = "DLY12010", MethodName = "GetListDLY12010_PRT", Descprition = "乗務員明細", },
            //new WCFDataAccessConfig() { Name = "DLY12010_UPDATE", ServiceClass = "DLY12010", MethodName = "UpdateColumn", Descprition = "売上明細", },
            //new WCFDataAccessConfig() { Name = "DLY12010_UPDATE2", ServiceClass = "DLY12010", MethodName = "UpdateColumn2", Descprition = "売上明細", },
            //new WCFDataAccessConfig() { Name = "DLY12010_MST1", ServiceClass = "DLY12010", MethodName = "GetMasterList", Descprition = "乗務員明細検索条件用マスタ一覧(得意先)", },
            //new WCFDataAccessConfig() { Name = "DLY12010_MST2", ServiceClass = "DLY12010", MethodName = "GetMasterList", Descprition = "乗務員明細検索条件用マスタ一覧(請求先)", },
            //new WCFDataAccessConfig() { Name = "DLY12010_MST3", ServiceClass = "DLY12010", MethodName = "GetMasterList", Descprition = "乗務員明細検索条件用マスタ一覧(支払先)", },
            //new WCFDataAccessConfig() { Name = "DLY12010_MST4", ServiceClass = "DLY12010", MethodName = "GetMasterList", Descprition = "乗務員明細検索条件用マスタ一覧(乗務員)", },
            //new WCFDataAccessConfig() { Name = "DLY12010_MST5", ServiceClass = "DLY12010", MethodName = "GetMasterList", Descprition = "乗務員明細検索条件用マスタ一覧(車輌)", },
            //new WCFDataAccessConfig() { Name = "DLY12010_MST6", ServiceClass = "DLY12010", MethodName = "GetMasterList", Descprition = "乗務員明細検索条件用マスタ一覧(車種)", },
            //new WCFDataAccessConfig() { Name = "DLY12010_MST7", ServiceClass = "DLY12010", MethodName = "GetMasterList", Descprition = "乗務員明細検索条件用マスタ一覧(発地)", },
            //new WCFDataAccessConfig() { Name = "DLY12010_MST8", ServiceClass = "DLY12010", MethodName = "GetMasterList", Descprition = "乗務員明細検索条件用マスタ一覧(着地)", },
            //new WCFDataAccessConfig() { Name = "DLY12010_MST9", ServiceClass = "DLY12010", MethodName = "GetMasterList", Descprition = "乗務員明細検索条件用マスタ一覧(商品)", },
            //new WCFDataAccessConfig() { Name = "DLY12010_TANKA_TOK", ServiceClass = "DLY12010", MethodName = "GetUnitCostTOK", Descprition = "売上金額・売上単価計算", },
            //new WCFDataAccessConfig() { Name = "DLY12010_TANKA_SHR", ServiceClass = "DLY12010", MethodName = "GetUnitCostSHR", Descprition = "支払金額・支払単価計算", },

			// 運転日報明細（画面）　DLY13010 
            //new WCFDataAccessConfig() { Name = "DLY13010", ServiceClass = "DLY13010", MethodName = "GetListDLY13010", Descprition = "運転日報明細", },
            //new WCFDataAccessConfig() { Name = "DLY13010_Preview", ServiceClass = "DLY13010", MethodName = "GetListDLY13010", Descprition = "運転日報明細印刷", },
            //new WCFDataAccessConfig() { Name = "DLY13010_UPDATE", ServiceClass = "DLY13010", MethodName = "UpdateColumn", Descprition = "運転日報明細", },
            //new WCFDataAccessConfig() { Name = "DLY13010_MST1", ServiceClass = "DLY13010", MethodName = "GetMasterList", Descprition = "運転日報明細検索条件用マスタ一覧(得意先)", },
            //new WCFDataAccessConfig() { Name = "DLY13010_MST2", ServiceClass = "DLY13010", MethodName = "GetMasterList", Descprition = "運転日報明細検索条件用マスタ一覧(請求先)", },
            //new WCFDataAccessConfig() { Name = "DLY13010_MST3", ServiceClass = "DLY13010", MethodName = "GetMasterList", Descprition = "運転日報明細検索条件用マスタ一覧(支払先)", },
            //new WCFDataAccessConfig() { Name = "DLY13010_MST4", ServiceClass = "DLY13010", MethodName = "GetMasterList", Descprition = "運転日報明細検索条件用マスタ一覧(乗務員)", },
            //new WCFDataAccessConfig() { Name = "DLY13010_MST5", ServiceClass = "DLY13010", MethodName = "GetMasterList", Descprition = "運転日報明細検索条件用マスタ一覧(車輌)", },
            //new WCFDataAccessConfig() { Name = "DLY13010_MST6", ServiceClass = "DLY13010", MethodName = "GetMasterList", Descprition = "運転日報明細検索条件用マスタ一覧(車種)", },
            //new WCFDataAccessConfig() { Name = "DLY13010_MST7", ServiceClass = "DLY13010", MethodName = "GetMasterList", Descprition = "運転日報明細検索条件用マスタ一覧(発地)", },
            //new WCFDataAccessConfig() { Name = "DLY13010_MST8", ServiceClass = "DLY13010", MethodName = "GetMasterList", Descprition = "運転日報明細検索条件用マスタ一覧(着地)", },
            //new WCFDataAccessConfig() { Name = "DLY13010_MST9", ServiceClass = "DLY13010", MethodName = "GetMasterList", Descprition = "運転日報明細検索条件用マスタ一覧(商品)", },

			// 経費明細（画面）　DLY14010 
            //new WCFDataAccessConfig() { Name = "DLY14010", ServiceClass = "DLY14010", MethodName = "GetListDLY14010", Descprition = "経費明細", },
            //new WCFDataAccessConfig() { Name = "DLY14010_UPDATE", ServiceClass = "DLY14010", MethodName = "UpdateColumn", Descprition = "経費明細", },
            //new WCFDataAccessConfig() { Name = "DLY14010_PRT", ServiceClass = "DLY14010", MethodName = "GetListDLY14010_PRT", Descprition = "経費明細", },
            //new WCFDataAccessConfig() { Name = "DLY14010_CSV", ServiceClass = "DLY14010", MethodName = "GetListDLY14010_CSV", Descprition = "経費明細", },

			// 入金伝票入力　　DLY15010 
            //new WCFDataAccessConfig() { Name = "DLY15010_GetData", ServiceClass = "DLY15010", MethodName = "DLY15010_GetData", Descprition = "入金伝票データ取得", },
            //new WCFDataAccessConfig() { Name = "DLY15010_UPDATE", ServiceClass = "DLY15010", MethodName = "UpdateColumn", Descprition = "入金伝票spシート登録・修正", },
            //new WCFDataAccessConfig() { Name = "DLY15010_CSV", ServiceClass = "DLY15010", MethodName = "DLY15010_CSV", Descprition = "入金伝票データCSV出力", },
            //new WCFDataAccessConfig() { Name = "DLY15010_PREVIEW", ServiceClass = "DLY15010", MethodName = "DLY15010_GetData", Descprition = "入金伝票データ印刷プレビュー", },

			// 出金伝票問合せ（画面）　DLY16010 
            //new WCFDataAccessConfig() { Name = "DLY16010", ServiceClass = "DLY16010", MethodName = "GetListDLY16010", Descprition = "出金伝票問合せ", },
            //new WCFDataAccessConfig() { Name = "DLY16010_UPDATE", ServiceClass = "DLY16010", MethodName = "Update", Descprition = "出金伝票問合せ", },
            //new WCFDataAccessConfig() { Name = "DLY16010_Pri", ServiceClass = "DLY16010", MethodName = "GetListDLY16010_Pri", Descprition = "出金伝票問合せ", },

			// 車輌台帳問合せ（印刷のみ) 画面名 <MST06020> LINQ <DLY17010> 帳票<MST06020> 
            //new WCFDataAccessConfig() { Name = "SEARCH_DLY17010", ServiceClass = "DLY17010", MethodName = "GetListDLY17010", Descprition = "車輌台帳問合せ", },

			// DLY19010 運行指示書 
            //new WCFDataAccessConfig() { Name = "SEARCH_DLY19010", ServiceClass = "DLY19010", MethodName = "GetDataList", Descprition = "運行指示書DLY19010)", },

			// DLY23010 日別売上管理表表 
            //new WCFDataAccessConfig() { Name = "SEARCH_DLY23010", ServiceClass = "DLY23010", MethodName = "GetDataList", Descprition = "日別売上管理表DLY23010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_DLY23010_CSV", ServiceClass = "DLY23010", MethodName = "GetDataList_CSV", Descprition = "日別売上管理表(DLY23010)", },

			// DLY24010 チェックリスト 
            //new WCFDataAccessConfig() { Name = "SEARCH_DLY24010", ServiceClass = "DLY24010", MethodName = "GetDataList", Descprition = "チェックリスト DLY24010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_DLY24010_CSV", ServiceClass = "DLY24010", MethodName = "GetDataList_CSV", Descprition = "チェックリスト (DLY24010)", },

			//SHR03010（支払経費明細書）
            //new WCFDataAccessConfig() { Name = "T01_TRN_SEARCH_SHR03010", ServiceClass = "SHR03010", MethodName = "SearchPreviewListData", Descprition = "支払明細書_プレビュー", },
            //new WCFDataAccessConfig() { Name = "T01_TRN_SEARCH_SHR03010_CSV", ServiceClass = "SHR03010", MethodName = "SearchCsvListData", Descprition = "支払明細書_CSV", },

			// SHR04010 支払先締日集計 
            //new WCFDataAccessConfig() { Name = "SHR04010_KIKAN_SET", ServiceClass = "SHR04010", MethodName = "SHR04010_KIKAN_SET", Descprition = "支払先締日集計(TKS04010)", },
            //new WCFDataAccessConfig() { Name = "SHR04010_SYUKEI", ServiceClass = "SHR04010", MethodName = "SHR04010_SYUKEI", Descprition = "支払先締日集計(SHR04010)", },

			//（帳票用）
            //new WCFDataAccessConfig() { Name = "TKS01010", ServiceClass = "TKS01010", MethodName = "TKS01010MakeWork", Descprition = "請求書用データ作成", },
            //new WCFDataAccessConfig() { Name = "TKS01010_2", ServiceClass = "TKS01010", MethodName = "GetListTKS01010_02", Descprition = "請求書用データ作成（TKS01010MakeWork）で作成されたワークを取得", },
            //new WCFDataAccessConfig() { Name = "TKS01010_KIKAN", ServiceClass = "TKS01010", MethodName = "TKS01010_KIKAN", Descprition = "請求書用期間取得", },
            //new WCFDataAccessConfig() { Name = "TKS01010_GAZOU", ServiceClass = "TKS01010", MethodName = "TKS01010_GAZOU", Descprition = "請求書用画像取得", },

			// ユーザーコントロール用 
			// 支払先別軽油マスタ 
            //new WCFDataAccessConfig() { Name = "M03_YNEN_UC", ServiceClass = "M03", MethodName = "GetDataYnen", Descprition = "支払先別軽油マスタ検索用", },
            //new WCFDataAccessConfig() { Name = "M03_YNEN_UPD", ServiceClass = "M03", MethodName = "UpdateYnen", Descprition = "支払先別軽油マスタ更新用", },
            //new WCFDataAccessConfig() { Name = "M03_YNEN_DEL", ServiceClass = "M03", MethodName = "DeleteYnen", Descprition = "支払先別軽油マスタ削除用", },

			// 規制マスタ保守 
            //new WCFDataAccessConfig() { Name = "M12_KIS_GetData", ServiceClass = "M12", MethodName = "GetData", Descprition = "グリーン経営車種マスタ保守（検索用）", },
            //new WCFDataAccessConfig() { Name = "M12_KIS_UPDATE", ServiceClass = "M12", MethodName = "Update", Descprition = "グリーン経営車種マスタ保守（更新用）", },
            //new WCFDataAccessConfig() { Name = "M12_KIS_DELETE", ServiceClass = "M12", MethodName = "Delete", Descprition = "グリーン経営車種マスタ保守（削除用）", },
            //new WCFDataAccessConfig() { Name = "M12_KIS_SCH", ServiceClass = "M12", MethodName = "M12_KIS_SCH", Descprition = "M12_KIS", },
            //new WCFDataAccessConfig() { Name = "M12_TIK_NEXT", ServiceClass = "M12", MethodName = "GetNextNumber", Descprition = "M12_TIK_NEXT", },

			// グリーン経営車種マスタ保守 
            //new WCFDataAccessConfig() { Name = "M14_GSYA_GetData", ServiceClass = "M14", MethodName = "GetData", Descprition = "グリーン経営車種マスタ保守（検索用）", },
            //new WCFDataAccessConfig() { Name = "M14_GSYA_UPDATE", ServiceClass = "M14", MethodName = "Update", Descprition = "グリーン経営車種マスタ保守（更新用）", },
            //new WCFDataAccessConfig() { Name = "M14_GSYA_DELETE", ServiceClass = "M14", MethodName = "Delete", Descprition = "グリーン経営車種マスタ保守（削除用）", },
            //new WCFDataAccessConfig() { Name = "M14_GSYA_SCH", ServiceClass = "M14", MethodName = "M14_GSYA_SCH", Descprition = "M14_GSYA", },
            //new WCFDataAccessConfig() { Name = "M14_TIK_NEXT", ServiceClass = "M14", MethodName = "GetNextNumber", Descprition = "M14_TIK_NEXT", },


			// 得意先別地区別単価マスタ 
            //new WCFDataAccessConfig() { Name = "M03_YTAN1_UC", ServiceClass = "M03", MethodName = "GetDataYtan1", Descprition = "得意先別地区別単価マスタ検索用", },
            //new WCFDataAccessConfig() { Name = "M03_YTAN1_UPD", ServiceClass = "M03", MethodName = "UpdateYtan1", Descprition = "得意先別地区別単価マスタ更新用", },
            //new WCFDataAccessConfig() { Name = "M03_YTAN1_DEL", ServiceClass = "M03", MethodName = "DeleteYtan1", Descprition = "得意先別地区別単価マスタ削除用", },
            //new WCFDataAccessConfig() { Name = "M03_YTAN1_ichiran", ServiceClass = "M03", MethodName = "GetDataYtan_Ichiran_Pre", Descprition = "得意先別地区別単価マスタ_データ取得用", },
            //new WCFDataAccessConfig() { Name = "M03_SEARCH_MST21010", ServiceClass = "M03", MethodName = "GetDataYtan_Ichiran_Pre", Descprition = "得意先別地区別単価マスタ_プレビュー用", },
            //new WCFDataAccessConfig() { Name = "M03_SEARCH_MST21010_CSV", ServiceClass = "M03", MethodName = "GetDataYtan_Ichiran_Pre", Descprition = "得意先別地区別単価マスタ_CSV用", },
            //new WCFDataAccessConfig() { Name = "M03_YTAN1_SCH", ServiceClass = "M03", MethodName = "GetDataYtanSCH", Descprition = "得意先別地区別単価マスタ検索", },

			//支払先別車種別単価マスタ
            //new WCFDataAccessConfig() { Name = "M03_YTAN2_UC", ServiceClass = "M03", MethodName = "GetDataYtan2", Descprition = "得意先別車種別単価マスター検索用", },
            //new WCFDataAccessConfig() { Name = "M03_YTAN2_UPD", ServiceClass = "M03", MethodName = "UpdateYtan2", Descprition = "得意先別車種別単価マスター更新用", },
            //new WCFDataAccessConfig() { Name = "M03_YTAN2_DEL", ServiceClass = "M03", MethodName = "DeleteYtan2", Descprition = "得意先別車種別単価マスター削除用", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST20020_Pre", ServiceClass = "M03", MethodName = "GetSearchListData", Descprition = "得意先別車種別単価マスタープレビュー用", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST20020_CSV", ServiceClass = "M03", MethodName = "GetSearchListData", Descprition = "得意先別車種別単価マスターCSV出力用", },

			//支払先別車種別個建単価マスタ
            //new WCFDataAccessConfig() { Name = "M03_YTAN4_UC", ServiceClass = "M03", MethodName = "GetDataTTAN4", Descprition = "支払先別個建単価マスタ検索用", },
            //new WCFDataAccessConfig() { Name = "M03_YTAN4_UC_Newold", ServiceClass = "M03", MethodName = "GetDataTTAN4", Descprition = "支払先別個建単価マスタ検索確定用", },
            //new WCFDataAccessConfig() { Name = "M03_YTAN4_UPD", ServiceClass = "M03", MethodName = "UpdateTTAN4", Descprition = "支払先別個建単価マスタ更新用", },

			//支払先別車種別個建単価マスタ
            //new WCFDataAccessConfig() { Name = "M03_YTAN4_UC", ServiceClass = "M03", MethodName = "GetDataTTAN4", Descprition = "支払先別個建単価マスタ検索用", },
            //new WCFDataAccessConfig() { Name = "M03_YTAN4_UC_Newold", ServiceClass = "M03", MethodName = "GetDataTTAN4", Descprition = "支払先別個建単価マスタ検索確定用", },
            //new WCFDataAccessConfig() { Name = "M03_YTAN4_UC_Newold2", ServiceClass = "M03", MethodName = "GetDataTTAN4_2", Descprition = "支払先別個建単価マスタ検索確定用", },
            //new WCFDataAccessConfig() { Name = "M03_YTAN4_UPD", ServiceClass = "M03", MethodName = "UpdateTTAN4", Descprition = "支払先別個建単価マスタ更新用", },
            //new WCFDataAccessConfig() { Name = "spM03_YTAN4_UC", ServiceClass = "M03", MethodName = "spGetDataTTAN4", Descprition = "支払先別個建単価マスタ検索用(SPREAD)", },
            //new WCFDataAccessConfig() { Name = "M03_YTAN4_DEL", ServiceClass = "M03", MethodName = "DeleteTTAN4", Descprition = "支払先別個建単価マスタ削除用", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST22020_Pre", ServiceClass = "M03", MethodName = "GetSearchListData_M03_YTAN4", Descprition = "支払先別個建単価マスタ一覧表プレビュー出力用", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST22020_CSV", ServiceClass = "M03", MethodName = "GetSearchListData_M03_YTAN4", Descprition = "支払先別個建単価マスタ一覧表CSV出力", },

			// 支払先別距離別単価マスタ 
            //new WCFDataAccessConfig() { Name = "M09_HIN_getDataListSEARCH_MST21010", ServiceClass = "M03", MethodName = "GetDataList", Descprition = "支払先別距離別単価マスタ検索用", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST21010_CSV", ServiceClass = "M03", MethodName = "GetDataList", Descprition = "支払先別距離別単価マスタ更新用", },

			// 得意先別品名単価マスタ 
            //new WCFDataAccessConfig() { Name = "M02_TTAN1_UC", ServiceClass = "M02", MethodName = "GetDataTtan1", Descprition = "得意先別地区別単価マスタ検索用", },
            //new WCFDataAccessConfig() { Name = "M02_TTAN1_UPD", ServiceClass = "M02", MethodName = "UpdateTtan1", Descprition = "得意先別地区別単価マスタ更新用", },
            //new WCFDataAccessConfig() { Name = "M02_TTAN1_DEL", ServiceClass = "M02", MethodName = "DeleteTtan1", Descprition = "得意先別地区別単価マスタ削除用", },
            //new WCFDataAccessConfig() { Name = "M02_TTAN1_ichiran", ServiceClass = "M02", MethodName = "GetDataTtan_Ichiran_Pre", Descprition = "得意先別地区別単価マスタ_データ取得用", },
            //new WCFDataAccessConfig() { Name = "M02_SEARCH_MST19010", ServiceClass = "M02", MethodName = "GetDataTtan_Ichiran_Pre", Descprition = "得意先別地区別単価マスタ_プレビュー用", },
            //new WCFDataAccessConfig() { Name = "M02_SEARCH_MST19010_CSV", ServiceClass = "M02", MethodName = "GetDataTtan_Ichiran_Pre", Descprition = "得意先別地区別単価マスタ_CSV用", },

			//支払先別車種別個建単価マスタ
            //new WCFDataAccessConfig() { Name = "M02_TTAN4_UC", ServiceClass = "M02", MethodName = "GetDataTTAN4", Descprition = "支払先別個建単価マスタ検索用", },
            //new WCFDataAccessConfig() { Name = "M02_TTAN4_UC_Newold", ServiceClass = "M02", MethodName = "GetDataTTAN4", Descprition = "支払先別個建単価マスタSpread確定用", },
            //new WCFDataAccessConfig() { Name = "M02_TTAN4_UC_Newold2", ServiceClass = "M02", MethodName = "GetDataTTAN4_2", Descprition = "支払先別個建単価マスタ検索確定用", },

            //new WCFDataAccessConfig() { Name = "M02_TTAN4_UPD", ServiceClass = "M02", MethodName = "UpdateTTAN4", Descprition = "支払先別個建単価マスタ更新用", },
            //new WCFDataAccessConfig() { Name = "spM02_TTAN4_UC", ServiceClass = "M02", MethodName = "spGetDataTTAN4", Descprition = "支払先別個建単価マスタ検索用(SPREAD)", },
            //new WCFDataAccessConfig() { Name = "spM02_TTAN4_UPD", ServiceClass = "M02", MethodName = "spUpdateTTAN4", Descprition = "支払先別個建単価マスタ更新用(SPREAD)", },
            //new WCFDataAccessConfig() { Name = "M02_TTAN4_DEL", ServiceClass = "M02", MethodName = "DeleteTTAN4", Descprition = "支払先別個建単価マスタ削除用", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST18020_Pre", ServiceClass = "M02", MethodName = "GetSearchListData_M02_TTAN4", Descprition = "支払先別個建単価マスタ一覧表プレビュー出力用", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST18020_CSV", ServiceClass = "M02", MethodName = "GetSearchListData_M02_TTAN4", Descprition = "支払先別個建単価マスタ一覧表CSV出力", },

			//経費項目マスタ
            //new WCFDataAccessConfig() { Name = "M07_KEI", ServiceClass = "M07", MethodName = "GetData", Descprition = "M07_KEI", },
            //new WCFDataAccessConfig() { Name = "M07_KEI_UP", ServiceClass = "M07", MethodName = "Update", Descprition = "M07_KEI_UP", },
            //new WCFDataAccessConfig() { Name = "M07_KEI_DEL", ServiceClass = "M07", MethodName = "Delete", Descprition = "M07_KEI_DEL", },
            //new WCFDataAccessConfig() { Name = "M07_KEI_NEXT", ServiceClass = "M07", MethodName = "GetNextNumber", Descprition = "M07_KEI_NEXT", },
            //new WCFDataAccessConfig() { Name = "M07_KEI_SCH", ServiceClass = "M07", MethodName = "GetSearchData", Descprition = "M07_KEI_SCH", },
            //new WCFDataAccessConfig() { Name = "M07_KEI_SCH2", ServiceClass = "M07", MethodName = "GetSearchData2", Descprition = "M07_KEI_SCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST09020", ServiceClass = "M07", MethodName = "GetSearchDataForList", Descprition = "SEARCH_MST09020", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST09020_CSV", ServiceClass = "M07", MethodName = "GetSearchDataForList", Descprition = "SEARCH_MST09020_CSV", },

			//発着地マスタ
            //new WCFDataAccessConfig() { Name = "RM08_TIK", ServiceClass = "M08", MethodName = "RGetData", Descprition = "類似検索", },
            //new WCFDataAccessConfig() { Name = "M08_TIK", ServiceClass = "M08", MethodName = "GetData", Descprition = "M08_TIK", },
            //new WCFDataAccessConfig() { Name = "M08_TIK_UP", ServiceClass = "M08", MethodName = "Update", Descprition = "M08_TIK_UP", },
            //new WCFDataAccessConfig() { Name = "M08_TIK_DEL", ServiceClass = "M08", MethodName = "Delete", Descprition = "M08_TIK_DEL", },
            //new WCFDataAccessConfig() { Name = "M08_TIK_NEXT", ServiceClass = "M08", MethodName = "GetNextNumber", Descprition = "M08_TIK_NEXT", },
            //new WCFDataAccessConfig() { Name = "M08_TIK_SCH", ServiceClass = "M08", MethodName = "GetSearchData", Descprition = "M08_TIK_SCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST03020", ServiceClass = "M08", MethodName = "GetSearchDataForList", Descprition = "SEARCH_MST03020", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST03020_CSV", ServiceClass = "M08", MethodName = "GetSearchDataForList", Descprition = "SEARCH_MST03020_CSV", },

			//請求内訳マスタ
            //new WCFDataAccessConfig() { Name = "M10_UHK", ServiceClass = "M10", MethodName = "GetData", Descprition = "請求内訳マスター", },
            //new WCFDataAccessConfig() { Name = "M10_UHK_UP", ServiceClass = "M10", MethodName = "Update", Descprition = "請求内訳マスター", },
            //new WCFDataAccessConfig() { Name = "M10_UHK_DEL", ServiceClass = "M10", MethodName = "Delete", Descprition = "請求内訳マスター", },
            //new WCFDataAccessConfig() { Name = "M10_UHK_NEXT", ServiceClass = "M10", MethodName = "GetNextID", Descprition = "請求内訳マスター自動採番", },
            //new WCFDataAccessConfig() { Name = "M10_UHK_SCH", ServiceClass = "M10", MethodName = "GetDataSCH", Descprition = "検索画面用データ取得", },
            //new WCFDataAccessConfig() { Name = "UHK_ID_CHG", ServiceClass = "M10", MethodName = "UHK_ID_CHG", Descprition = "請求内訳マスターメンテ用（ID変換）", },



			//得意先別車種別単価マスタ
            //new WCFDataAccessConfig() { Name = "M02_TTAN2_UC", ServiceClass = "M02", MethodName = "GetDataTTAN2", Descprition = "得意先別車種別単価マスター検索用", },
            //new WCFDataAccessConfig() { Name = "M02_TTAN2_UPD", ServiceClass = "M02", MethodName = "UpdateTTAN2", Descprition = "得意先別車種別単価マスター更新用", },
            //new WCFDataAccessConfig() { Name = "M02_TTAN2_DEL", ServiceClass = "M02", MethodName = "DeleteTTAN2", Descprition = "得意先別車種別単価マスター削除用", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST16020_Pre", ServiceClass = "M02", MethodName = "GetSearchListData", Descprition = "得意先別車種別単価マスタープレビュー用", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST16020_CSV", ServiceClass = "M02", MethodName = "GetSearchListData", Descprition = "得意先別車種別単価マスターCSV出力用", },

			//得意先別距離別運賃マスタ
            //new WCFDataAccessConfig() { Name = "M02_TTAN3_UC", ServiceClass = "M02", MethodName = "GetDataTTAN3", Descprition = "得意先別距離別運賃マスタ検索用", },
            //new WCFDataAccessConfig() { Name = "M02_TTAN3_UPD", ServiceClass = "M02", MethodName = "UpdateTTAN3", Descprition = "得意先別距離別運賃マスタ更新用", },
            //new WCFDataAccessConfig() { Name = "M02_TTAN3_DEL", ServiceClass = "M02", MethodName = "DeleteTTAN3", Descprition = "得意先別距離別運賃マスタ削除用", },

			//得意先別個建単価マスタ
            //new WCFDataAccessConfig() { Name = "M02_TTAN4_UC", ServiceClass = "M02", MethodName = "GetDataTTAN4", Descprition = "得意先別個建単価マスタ検索用", },
            //new WCFDataAccessConfig() { Name = "M02_TTAN4_UC_Newold", ServiceClass = "M02", MethodName = "GetDataTTAN4", Descprition = "得意先別個建単価マスタ検索確定用", },
            //new WCFDataAccessConfig() { Name = "M02_TTAN4_UPD", ServiceClass = "M02", MethodName = "UpdateTTAN4", Descprition = "得意先別個建単価マスタ更新用", },
            //new WCFDataAccessConfig() { Name = "spM02_TTAN4_UC", ServiceClass = "M02", MethodName = "spGetDataTTAN4", Descprition = "得意先別個建単価マスタ検索用(SPREAD)", },
            //new WCFDataAccessConfig() { Name = "spM02_TTAN4_UPD", ServiceClass = "M02", MethodName = "spUpdateTTAN4", Descprition = "得意先別個建単価マスタ更新用(SPREAD)", },
            //new WCFDataAccessConfig() { Name = "M02_TTAN4_DEL", ServiceClass = "M02", MethodName = "DeleteTTAN4", Descprition = "得意先別個建単価マスタ削除用", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST18020_Pre", ServiceClass = "M02", MethodName = "GetSearchListData_M02_TTAN4", Descprition = "得意先別個建単価マスタ一覧表プレビュー出力用", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST18020_CSV", ServiceClass = "M02", MethodName = "GetSearchListData_M02_TTAN4", Descprition = "得意先別個建単価マスタ一覧表CSV出力", },


			//自社部門マスター
            //new WCFDataAccessConfig() { Name = "M71_BUM", ServiceClass = "M71", MethodName = "GetData", Descprition = "M71_BUM", },
            //new WCFDataAccessConfig() { Name = "M71_BUM_UP", ServiceClass = "M71", MethodName = "Update", Descprition = "M71_BUM_UP", },
            //new WCFDataAccessConfig() { Name = "M71_BUM_DEL", ServiceClass = "M71", MethodName = "Delete", Descprition = "M71_BUM_DEL", },
            //new WCFDataAccessConfig() { Name = "M71_BUM_NEXT", ServiceClass = "M71", MethodName = "GetNextNumber", Descprition = "M71_BUM_NEXT", },
            //new WCFDataAccessConfig() { Name = "M71_BUM_SCH", ServiceClass = "M71", MethodName = "GetSearchData", Descprition = "M71_BUM_SCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST10020", ServiceClass = "M71", MethodName = "GetSearchDataForList", Descprition = "SEARCH_MST10020", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST10020_CSV", ServiceClass = "M71", MethodName = "GetSearchDataForList", Descprition = "SEARCH_MST10020_CSV", },

            //new WCFDataAccessConfig() { Name = "M71_Bumon", ServiceClass = "M71", MethodName = "GetBumon", Descprition = "問合せからの呼び出し", },


			//経由引取税マスタ
            //new WCFDataAccessConfig() { Name = "M92_KZEI_UC", ServiceClass = "M92", MethodName = "GetData", Descprition = "経由引取税マスター検索用", },
            //new WCFDataAccessConfig() { Name = "M92_KZEI_BTN", ServiceClass = "M92", MethodName = "GetDataBtn", Descprition = "M92_KZEI", },
            //new WCFDataAccessConfig() { Name = "M92_KZEI_UPD", ServiceClass = "M92", MethodName = "Update", Descprition = "経由引取税マスター更新用", },
            //new WCFDataAccessConfig() { Name = "M92_KZEI_DEL", ServiceClass = "M92", MethodName = "Delete", Descprition = "経由引取税マスター削除用", },
            //new WCFDataAccessConfig() { Name = "M92_KZEI_SCH", ServiceClass = "M92", MethodName = "GetDataJisSCH", Descprition = "経由引取税マスター検索用", },
            //new WCFDataAccessConfig() { Name = "M92_KZEI_NEXT", ServiceClass = "M92", MethodName = "GetNextNumber", Descprition = "経由引取税マスター自動採番", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST34020", ServiceClass = "M92", MethodName = "GetDataHinList", Descprition = "M92_KZEI", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST34020_CSV", ServiceClass = "M92", MethodName = "GetDataHinList", Descprition = "M92_KZEI", },

			//出勤区分マスタ
            //new WCFDataAccessConfig() { Name = "M78_SYK", ServiceClass = "M78", MethodName = "GetAllData", Descprition = "M78_SYK", },
            //new WCFDataAccessConfig() { Name = "M78_SYK_List", ServiceClass = "M78", MethodName = "GetAllData", Descprition = "M78_SYK_List", },
            //new WCFDataAccessConfig() { Name = "M78_SYK_UP", ServiceClass = "M78", MethodName = "GetListSYK", Descprition = "M78_SYK_UP", },

			//基礎情報マスタ
            //new WCFDataAccessConfig() { Name = "M87_CNTL", ServiceClass = "M87", MethodName = "GetData", Descprition = "M87_CNTL", },
            //new WCFDataAccessConfig() { Name = "M87_CNTL_UP", ServiceClass = "M87", MethodName = "Update", Descprition = "M87_CNTL_UP", },

			//CSV取り込み  MST90020
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020", ServiceClass = "M90020", MethodName = "SEARCH_MST90020", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST900201", ServiceClass = "M90020", MethodName = "SEARCH_MST900201", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_00", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_00", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_01", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_01", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_02", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_02", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_03", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_03", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_04", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_04", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_05", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_05", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_06", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_06", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_07", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_07", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_08", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_08", Descprition = "SEARCH", },

			//車輌マスタ
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_09", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_09", Descprition = "SEARCH", },

			//適正診断データ
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_10", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_10", Descprition = "SEARCH", },

			//事故違反履歴データ
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_11", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_11", Descprition = "SEARCH", },

			//特別教育
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_12", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_12", Descprition = "SEARCH", },

			//乗務員データ
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_13", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_13", Descprition = "SEARCH", },

			//乗務員経費データ
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_14", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_14", Descprition = "SEARCH", },

			//乗務員画像データ
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_15", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_15", Descprition = "SEARCH", },

			//車輌マスタ
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_16", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_16", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_17", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_17", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_18", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_18", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_19", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_19", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_20", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_20", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_21", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_21", Descprition = "SEARCH", },

			//車種マスタ
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_22", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_22", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_23", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_23", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_24", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_24", Descprition = "SEARCH", },

			//地区マスタ
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_25", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_25", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_26", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_26", Descprition = "SEARCH", },

			//請求内訳マスタ
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_27", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_27", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_28", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_28", Descprition = "SEARCH", },

			//規制マスタ
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_29", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_29", Descprition = "SEARCH", },

			//燃費目標マスタ
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_30", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_30", Descprition = "SEARCH", },

			//グリーン車種マスタ
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_31", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_31", Descprition = "SEARCH", },

			// 自社名マスタ 
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_33", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_33", Descprition = "SEARCH", },

			// 自社部門マスタ 
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_34", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_34", Descprition = "SEARCH", },

			// 担当者マスタ 
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_35", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_35", Descprition = "SEARCH", },

			// 歩合率マスタ 
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_37", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_37", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_38", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_38", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_39", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_39", Descprition = "SEARCH", },

			//取引区分マスタ
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_40", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_40", Descprition = "SEARCH", },

			//出勤区分マスタ
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_41", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_41", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_42", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_42", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_43", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_43", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_44", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_44", Descprition = "SEARCH", },

			//運賃計算区分マスタ
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_45", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_45", Descprition = "SEARCH", },

			//運輸局マスタ
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_46", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_46", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_47", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_47", Descprition = "SEARCH", },

			//明細番号マスタ
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_48", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_48", Descprition = "SEARCH", },

			//郵便番号マスタ
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_49", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_49", Descprition = "SEARCH", },

			//グリッド表示マスタ
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_50", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_50", Descprition = "SEARCH", },

			//燃料単価マスタ
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_51", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_51", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90020_52", ServiceClass = "M90020", MethodName = "SEARCH_MST90020_52", Descprition = "SEARCH", },

			//売上トラン
            //new WCFDataAccessConfig() { Name = "SEARCH_TRN90020_00", ServiceClass = "M90020", MethodName = "SEARCH_TRN90020_00", Descprition = "SEARCH", },

			//運行トラン
            //new WCFDataAccessConfig() { Name = "SEARCH_TRN90020_01", ServiceClass = "M90020", MethodName = "SEARCH_TRN90020_01", Descprition = "SEARCH", },

			//経費トラン
            //new WCFDataAccessConfig() { Name = "SEARCH_TRN90020_02", ServiceClass = "M90020", MethodName = "SEARCH_TRN90020_02", Descprition = "SEARCH", },
            //new WCFDataAccessConfig() { Name = "MST90020_TOK", ServiceClass = "M90020", MethodName = "MST90020_TOK", Descprition = "得意先データ取得", },
            //new WCFDataAccessConfig() { Name = "MST90020_DRV", ServiceClass = "M90020", MethodName = "MST90020_DRV", Descprition = "乗務員データ取得", },
            //new WCFDataAccessConfig() { Name = "MST90020_CAR", ServiceClass = "M90020", MethodName = "MST90020_CAR", Descprition = "車輌データ取得", },
            //new WCFDataAccessConfig() { Name = "MST90020_TIK", ServiceClass = "M90020", MethodName = "MST90020_TIK", Descprition = "発着地データ取得", },
            //new WCFDataAccessConfig() { Name = "MST90020_HIN", ServiceClass = "M90020", MethodName = "MST90020_HIN", Descprition = "商品データ取得", },
            //new WCFDataAccessConfig() { Name = "MST90020_SYA", ServiceClass = "M90020", MethodName = "MST90020_SYA", Descprition = "車種データ取得", },
            //new WCFDataAccessConfig() { Name = "MST90020_TEK", ServiceClass = "M90020", MethodName = "MST90020_TEK", Descprition = "摘要データ取得", },
  
			//入出金トラン
            //new WCFDataAccessConfig() { Name = "SEARCH_TRN90020_03", ServiceClass = "M90020", MethodName = "SEARCH_TRN90020_03", Descprition = "SEARCH", },

			//CSV出力
            //new WCFDataAccessConfig() { Name = "SEARCH_CSV01", ServiceClass = "M90030", MethodName = "GETCSVDATA01", Descprition = "取引先マスタ取得", },
            //new WCFDataAccessConfig() { Name = "SEARCH_CSV02", ServiceClass = "M90030", MethodName = "GETCSVDATA02", Descprition = "請求内訳マスタ取得", },
            //new WCFDataAccessConfig() { Name = "SEARCH_CSV03", ServiceClass = "M90030", MethodName = "GETCSVDATA03", Descprition = "発着地マスタ取得", },
            //new WCFDataAccessConfig() { Name = "SEARCH_CSV04", ServiceClass = "M90030", MethodName = "GETCSVDATA04", Descprition = "車輌マスタ取得", },
            //new WCFDataAccessConfig() { Name = "SEARCH_CSV05", ServiceClass = "M90030", MethodName = "GETCSVDATA05", Descprition = "乗務員マスタ取得", },
            //new WCFDataAccessConfig() { Name = "SEARCH_CSV06", ServiceClass = "M90030", MethodName = "GETCSVDATA06", Descprition = "車種マスタ取得", },
            //new WCFDataAccessConfig() { Name = "SEARCH_CSV07", ServiceClass = "M90030", MethodName = "GETCSVDATA07", Descprition = "商品マスタ取得", },
            //new WCFDataAccessConfig() { Name = "SEARCH_CSV08", ServiceClass = "M90030", MethodName = "GETCSVDATA08", Descprition = "摘要マスタ取得", },
			
			//伝票データ取り込み
            //new WCFDataAccessConfig() { Name = "MST90050_TOK", ServiceClass = "M90050", MethodName = "MST90050_TOK", Descprition = "得意先データ取得", },
            //new WCFDataAccessConfig() { Name = "MST90050_DRV", ServiceClass = "M90050", MethodName = "MST90050_DRV", Descprition = "乗務員データ取得", },
            //new WCFDataAccessConfig() { Name = "MST90050_CAR", ServiceClass = "M90050", MethodName = "MST90050_CAR", Descprition = "車輌データ取得", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90050_00", ServiceClass = "M90050", MethodName = "SEARCH_MST90050_00", Descprition = "TRNデータ取得", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90050_01", ServiceClass = "M90050", MethodName = "SEARCH_MST90050_01", Descprition = "TRNデータ取得", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90050_02", ServiceClass = "M90050", MethodName = "SEARCH_MST90050_02", Descprition = "TRNデータ取得", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90050_03", ServiceClass = "M90050", MethodName = "SEARCH_MST90050_03", Descprition = "TRNデータ取得", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST900501", ServiceClass = "M90050", MethodName = "SEARCH_MST900501", Descprition = "データ登録", },

			//伝票データ取り込みテスト
            //new WCFDataAccessConfig() { Name = "MST90060_TOK", ServiceClass = "M90060", MethodName = "MST90060_TOK", Descprition = "得意先データ取得", },
            //new WCFDataAccessConfig() { Name = "MST90060_DRV", ServiceClass = "M90060", MethodName = "MST90060_DRV", Descprition = "乗務員データ取得", },
            //new WCFDataAccessConfig() { Name = "MST90060_CAR", ServiceClass = "M90060", MethodName = "MST90060_CAR", Descprition = "車輌データ取得", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90060_00", ServiceClass = "M90060", MethodName = "SEARCH_MST90060_00", Descprition = "TRNデータ取得", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90022_00", ServiceClass = "M90020", MethodName = "SEARCH_MST90060_00", Descprition = "TRNデータ取得", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90060_01", ServiceClass = "M90060", MethodName = "SEARCH_MST90060_01", Descprition = "TRNデータ取得", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90060_02", ServiceClass = "M90060", MethodName = "SEARCH_MST90060_02", Descprition = "TRNデータ取得", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST90060_03", ServiceClass = "M90060", MethodName = "SEARCH_MST90060_03", Descprition = "TRNデータ取得", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST900601", ServiceClass = "M90060", MethodName = "SEARCH_MST900601", Descprition = "データ登録", },

			//燃料単価マスタ
            //new WCFDataAccessConfig() { Name = "M91_OTAN", ServiceClass = "M91", MethodName = "GetData", Descprition = "M91_OTAN", },
            //new WCFDataAccessConfig() { Name = "M91_OTAN_UP", ServiceClass = "M91", MethodName = "Update", Descprition = "M91_OTAN_UP", },
            //new WCFDataAccessConfig() { Name = "M91_OTAN_DEL", ServiceClass = "M91", MethodName = "Delete", Descprition = "M91_OTAN_DEL", },
            //new WCFDataAccessConfig() { Name = "M91_OTAN_SCH", ServiceClass = "M91", MethodName = "GetSearchData", Descprition = "M91_OTAN_SCH", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST14020", ServiceClass = "M91", MethodName = "GetSearchDataForList", Descprition = "SEARCH_MST14020", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST14020_CSV", ServiceClass = "M91", MethodName = "GetSearchDataForList", Descprition = "SEARCH_MST14020_CSV", },

			//運輸局
            //new WCFDataAccessConfig() { Name = "M84_RIK_UC", ServiceClass = "M84", MethodName = "GetData", Descprition = "運輸局データ取得用", },

			//車種マスタ
            //new WCFDataAccessConfig() { Name = "M06_SYA_UC", ServiceClass = "M06", MethodName = "GetData", Descprition = "車種マスタ検索用", },
            //new WCFDataAccessConfig() { Name = "RM06_SYA_UC", ServiceClass = "M06", MethodName = "RGetData", Descprition = "車種マスタ検索用類似用", },
            //new WCFDataAccessConfig() { Name = "M06_SYA_UC_Delete", ServiceClass = "M06", MethodName = "GetDataDeleteSerch", Descprition = "車種マスタ検索用", },
            //new WCFDataAccessConfig() { Name = "M06_SYA_UC_F1", ServiceClass = "M06", MethodName = "F1_Kensaku", Descprition = "車種マスタ検索用", },
            //new WCFDataAccessConfig() { Name = "M06_SYA_Kensaku", ServiceClass = "M06", MethodName = "GetKensaku", Descprition = "車種マスタ一覧出力用", },
            //new WCFDataAccessConfig() { Name = "M06_SYA_UPD", ServiceClass = "M06", MethodName = "Update", Descprition = "車種マスタ更新用", },
            //new WCFDataAccessConfig() { Name = "M06_SYA_DEL", ServiceClass = "M06", MethodName = "Delete", Descprition = "車種マスタ削除用", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST05010", ServiceClass = "M06", MethodName = "GetSearchListData", Descprition = "印刷プレビュー", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST05010_CSV", ServiceClass = "M06", MethodName = "GetSearchListData", Descprition = "CSV出力", },
            //new WCFDataAccessConfig() { Name = "M06_TIK_NEXT", ServiceClass = "M06", MethodName = "GetNextNumber", Descprition = "車種マスター自動採番", },

			//車輌マスタ
            //new WCFDataAccessConfig() { Name = "M05_CAR_UC", ServiceClass = "M05", MethodName = "GetDataCar", Descprition = "車輌マスタ検索用", },
            //new WCFDataAccessConfig() { Name = "RM05_CAR_UC", ServiceClass = "M05", MethodName = "RGetDataCar", Descprition = "車輌マスタ類似用", },

			//売上入力用車輌マスタ
            //new WCFDataAccessConfig() { Name = "M05_CAR_UC2", ServiceClass = "M05", MethodName = "GetDataCar", Descprition = "車輌マスタ検索用", },

			//車輌マスタ
            //new WCFDataAccessConfig() { Name = "M05_CAR_UC2", ServiceClass = "M05", MethodName = "GetDataCar", Descprition = "車輌マスタ検索用", },
            //new WCFDataAccessConfig() { Name = "M05_CAR_BTN", ServiceClass = "M05", MethodName = "GetDataCarBtn", Descprition = "車輌マスタ検索用", },
            //new WCFDataAccessConfig() { Name = "M05_CAR_UPD", ServiceClass = "M05", MethodName = "UpdateCar", Descprition = "車輌マスタ更新用", },
            //new WCFDataAccessConfig() { Name = "M05_CAR_DEL", ServiceClass = "M05", MethodName = "DeleteCar", Descprition = "車輌マスタ削除用", },
            //new WCFDataAccessConfig() { Name = "M05_CAR_NEXT", ServiceClass = "M05", MethodName = "GetNextID", Descprition = "車輌マスタ自動採番用", },
            //new WCFDataAccessConfig() { Name = "M05_CDT2", ServiceClass = "M05", MethodName = "GetDataCdt2", Descprition = "強制保険データ取得", },
            //new WCFDataAccessConfig() { Name = "M05_CDT3", ServiceClass = "M05", MethodName = "GetDataCdt3", Descprition = "任意保険データ取得", },
            //new WCFDataAccessConfig() { Name = "M05_CDT4", ServiceClass = "M05", MethodName = "GetDataCdt4", Descprition = "納税データ取得", },
            //new WCFDataAccessConfig() { Name = "M05_CAR_KOTEI_ALL", ServiceClass = "M05", MethodName = "GetDataKoteihi", Descprition = "一括車輌固定データ取得", },
            //new WCFDataAccessConfig() { Name = "M05_CAR_KOTEI_UPD", ServiceClass = "M05", MethodName = "UpdateKoteihi", Descprition = "一括車輌固定データ更新", },
            //new WCFDataAccessConfig() { Name = "M05_CAR_KOTEI_UPD_EXIT", ServiceClass = "M05", MethodName = "UpdateKoteihi", Descprition = "一括車輌固定データ更新終了時", },
            //new WCFDataAccessConfig() { Name = "M05_CAR_SCH", ServiceClass = "M05", MethodName = "GetDataCarSCH", Descprition = "車輌マスタ検索画面用", },
            //new WCFDataAccessConfig() { Name = "M05_CAR_ICHIRAN", ServiceClass = "M05", MethodName = "GetDataCarIchiran", Descprition = "車輌マスタ一覧画面用", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST06010", ServiceClass = "M05", MethodName = "GetSearchListData", Descprition = "印刷プレビュー", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST06010_CSV", ServiceClass = "M05", MethodName = "GetSearchListData", Descprition = "CSV出力", },
            //new WCFDataAccessConfig() { Name = "CAR_ID_CHG", ServiceClass = "M05", MethodName = "CAR_ID_CHG", Descprition = "ID変換", },
            //new WCFDataAccessConfig() { Name = "M05_GetMaxMeisaiNo", ServiceClass = "M05", MethodName = "GetMaxMeisaiCount", Descprition = "現在の登録件数", },

			//車輌管理台帳
            //new WCFDataAccessConfig() { Name = "CarList", ServiceClass = "DLY17010_1", MethodName = "GetSearchCarList", Descprition = "車輌台帳印刷", },

			//得意先別距離単価マスタ
            //new WCFDataAccessConfig() { Name = "M50_RTBL_UC", ServiceClass = "M50", MethodName = "GetData", Descprition = "データ受信", },
            //new WCFDataAccessConfig() { Name = "M50_RTBL_UCR", ServiceClass = "M50", MethodName = "GetRData", Descprition = "データ受信", },
            //new WCFDataAccessConfig() { Name = "M50_RTBL_UC1", ServiceClass = "M50", MethodName = "GetData1", Descprition = "データ受信", },
            //new WCFDataAccessConfig() { Name = "M50_RTBL_ColCount", ServiceClass = "M50", MethodName = "GetDataColCount", Descprition = "データ受信", },
            //new WCFDataAccessConfig() { Name = "M50_RTBL_RowCount", ServiceClass = "M50", MethodName = "GetDataRowCount", Descprition = "データ受信", },
            //new WCFDataAccessConfig() { Name = "M50_RTBL_UPD", ServiceClass = "M50", MethodName = "Update", Descprition = "Update", },
            //new WCFDataAccessConfig() { Name = "M50_RTBL_INS", ServiceClass = "M50", MethodName = "Insert", Descprition = "INSERT", },
            //new WCFDataAccessConfig() { Name = "M50_RTBL_DEL", ServiceClass = "M50", MethodName = "Delete", Descprition = "Delete", },
            //new WCFDataAccessConfig() { Name = "M50_RTBL_Kyori_DEL", ServiceClass = "M50", MethodName = "Kyori_Delete", Descprition = "Delete1", },
            //new WCFDataAccessConfig() { Name = "M50_RTBL_Jyuryou_DEL", ServiceClass = "M50", MethodName = "Jyuryou_Delete", Descprition = "Delete1", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST17010", ServiceClass = "M50", MethodName = "GetDataHinList", Descprition = "CSV出力", },
            //new WCFDataAccessConfig() { Name = "SEARCH_MST17010_CSV", ServiceClass = "M50", MethodName = "GetDataHinList", Descprition = "CSV出力", },
            //new WCFDataAccessConfig() { Name = "M50_RTBL_Kyori_DEL", ServiceClass = "M50", MethodName = "Kyori_Delete", Descprition = "Delete", },
            //new WCFDataAccessConfig() { Name = "M50_RTBL_Jyuryou_DEL", ServiceClass = "M50", MethodName = "Jyuryou_Delete", Descprition = "Delete", },
            //new WCFDataAccessConfig() { Name = "M50_NEXT", ServiceClass = "M50", MethodName = "M50_NEXT", Descprition = "nextコード取得", },
            //new WCFDataAccessConfig() { Name = "M50_BEFORE", ServiceClass = "M50", MethodName = "M50_BEFORE", Descprition = "BEFOREコード取得", },

			// 車輌別目標金額マスター保守 
            //new WCFDataAccessConfig() { Name = "SEARCH_MST32010", ServiceClass = "M17", MethodName = "SEARCH_GetData", Descprition = "車輌別目標金額マスター保守", },
            //new WCFDataAccessConfig() { Name = "NINSERT_MST32010", ServiceClass = "M17", MethodName = "NINSERT_GetData", Descprition = "車輌別目標金額マスター保守(F9登録)", },
            //new WCFDataAccessConfig() { Name = "LAST_MANTH_MST32010", ServiceClass = "M17", MethodName = "LAST_MANTH_MST32010", Descprition = "車輌別目標金額マスター保守前年データ取得", },

			// 部門別目標金額マスター保守 
            //new WCFDataAccessConfig() { Name = "SEARCH_MST33010", ServiceClass = "M16", MethodName = "SEARCH_GetData", Descprition = "部門別目標金額マスター保守", },
            //new WCFDataAccessConfig() { Name = "NINSERT_MST33010", ServiceClass = "M16", MethodName = "NINSERT_GetData", Descprition = "部門別目標金額マスター保守(F9登録)", },
            //new WCFDataAccessConfig() { Name = "LAST_MANTH_MST33010", ServiceClass = "M16", MethodName = "LAST_MANTH_MST33010", Descprition = "部門別目標金額マスター保守前年データ取得", },

			//得意先締日集計マスタ
            //new WCFDataAccessConfig() { Name = "S01_TOKS", ServiceClass = "S01", MethodName = "GetData", Descprition = "得意先締日集計", },
            //new WCFDataAccessConfig() { Name = "S01_TOKS_UP", ServiceClass = "S01", MethodName = "Update", Descprition = "得意先締日集計更新用", },
            //new WCFDataAccessConfig() { Name = "S01_TOKS_DEL", ServiceClass = "S01", MethodName = "Delete", Descprition = "得意先締日集計更新用削除用", },
            //new WCFDataAccessConfig() { Name = "S01_TOKS_SCH", ServiceClass = "S01", MethodName = "GetDataSCH", Descprition = "得意先締日集計マスター検索用", },

			//得意先月次集計マスタ
            //new WCFDataAccessConfig() { Name = "S11_TOKG", ServiceClass = "S11", MethodName = "GetData", Descprition = "得意先月次集計", },
            //new WCFDataAccessConfig() { Name = "S11_TOKG_UP", ServiceClass = "S11", MethodName = "Update", Descprition = "得意先月次集計更新用", },
            //new WCFDataAccessConfig() { Name = "S11_TOKG_DEL", ServiceClass = "S11", MethodName = "Delete", Descprition = "得意先月次更新用削除用", },
            //new WCFDataAccessConfig() { Name = "S11_TOKG_SCH", ServiceClass = "S11", MethodName = "GetDataSCH", Descprition = "得意先月次集計マスター検索用", },

			//支払先締日集計マスタ
            //new WCFDataAccessConfig() { Name = "S02_YOSS", ServiceClass = "S02", MethodName = "GetData", Descprition = "仕入先締日集計", },
            //new WCFDataAccessConfig() { Name = "S02_YOSS_UP", ServiceClass = "S02", MethodName = "Update", Descprition = "仕入先締日集計更新用", },
            //new WCFDataAccessConfig() { Name = "S02_YOSS_DEL", ServiceClass = "S02", MethodName = "Delete", Descprition = "仕入先締日集計更新用削除用", },
            //new WCFDataAccessConfig() { Name = "S02_YOSS_SCH", ServiceClass = "S02", MethodName = "GetDataSCH", Descprition = "仕入先締日集計マスター検索用", },

			//仕入先月次集計マスタ
            //new WCFDataAccessConfig() { Name = "S12_YOSG", ServiceClass = "S12", MethodName = "GetData", Descprition = "仕入先月次集計", },
            //new WCFDataAccessConfig() { Name = "S12_YOSG_UP", ServiceClass = "S12", MethodName = "Update", Descprition = "仕入先月次集計更新用", },
            //new WCFDataAccessConfig() { Name = "S12_YOSG_DEL", ServiceClass = "S12", MethodName = "Delete", Descprition = "仕入先月次更新用削除用", },
            //new WCFDataAccessConfig() { Name = "S12_YOSG_SCH", ServiceClass = "S12", MethodName = "GetDataSCH", Descprition = "仕入先月次集計マスター検索用", },

			//乗務員月次経費入力マスタ
            //new WCFDataAccessConfig() { Name = "S13_DRV", ServiceClass = "S13", MethodName = "GetData", Descprition = "乗務員月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S13_DRV_UP", ServiceClass = "S13", MethodName = "Update", Descprition = "乗務員月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S13_DRV_DEL", ServiceClass = "S13", MethodName = "Delete", Descprition = "乗務員月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S13_DRV_SCH", ServiceClass = "S13", MethodName = "GetDataSCH", Descprition = "乗務員月次経費入力検索用", },

			//変動費
            //new WCFDataAccessConfig() { Name = "S13_DRVSB_Hendo", ServiceClass = "S13SB", MethodName = "GetData_Hendo", Descprition = "乗務員月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S13_DRVSB_UP_Hendo", ServiceClass = "S13SB", MethodName = "Update_Hendo", Descprition = "乗務員月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S13_DRVSB_DEL_Hendo", ServiceClass = "S13SB", MethodName = "Delete_Hendo", Descprition = "乗務員月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S13_DRVSB_SCH_Hendo", ServiceClass = "S13SB", MethodName = "GetDataSCH_Hendo", Descprition = "乗務員月次経費入力検索用", },

			//人件費
            //new WCFDataAccessConfig() { Name = "S13_DRVSB_Jinken", ServiceClass = "S13SB", MethodName = "GetData_Jinken", Descprition = "乗務員月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S13_DRVSB_UP_Jinken", ServiceClass = "S13SB", MethodName = "Update_Jinken", Descprition = "乗務員月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S13_DRVSB_DEL_Jinken", ServiceClass = "S13SB", MethodName = "Delete_Jinken", Descprition = "乗務員月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S13_DRVSB_SCH_Jinken", ServiceClass = "S13SB", MethodName = "GetDataSCH_Jinken", Descprition = "乗務員月次経費入力検索用", },

			//固定費
            //new WCFDataAccessConfig() { Name = "S13_DRVSB_Kotei", ServiceClass = "S13SB", MethodName = "GetData_Kotei", Descprition = "乗務員月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S13_DRVSB_UP_Kotei", ServiceClass = "S13SB", MethodName = "Update_Kotei", Descprition = "乗務員月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S13_DRVSB_DEL_Kotei", ServiceClass = "S13SB", MethodName = "Delete_Kotei", Descprition = "乗務員月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S13_DRVSB_SCH_Kotei", ServiceClass = "S13SB", MethodName = "GetDataSCH_Kotei", Descprition = "乗務員月次経費入力検索用", },

			//車輌月次経費入力マスタ
            //new WCFDataAccessConfig() { Name = "S14_CAR", ServiceClass = "S14", MethodName = "GetData", Descprition = "車輌月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S14_CAR_UP", ServiceClass = "S14", MethodName = "Update", Descprition = "車輌月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S14_CAR_DEL", ServiceClass = "S14", MethodName = "Delete", Descprition = "車輌月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S14_CAR_SCH", ServiceClass = "S14", MethodName = "GetDataSCH", Descprition = "車輌月次経費入力検索用", },

			//変動費
            //new WCFDataAccessConfig() { Name = "S14_CARSB_Hendo", ServiceClass = "S14SB", MethodName = "GetData_Hendo", Descprition = "車輌月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S14_CARSB_UP_Hendo", ServiceClass = "S14SB", MethodName = "Update_Hendo", Descprition = "車輌月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S14_CARSB_DEL_Hendo", ServiceClass = "S14SB", MethodName = "Delete_Hendo", Descprition = "車輌月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S14_CARSB_SCH_Hendo", ServiceClass = "S14SB", MethodName = "GetDataSCH_Hendo", Descprition = "車輌月次経費入力検索用", },

			//人件費
            //new WCFDataAccessConfig() { Name = "S14_CARSB_Jinken", ServiceClass = "S14SB", MethodName = "GetData_Jinken", Descprition = "車輌月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S14_CARSB_UP_Jinken", ServiceClass = "S14SB", MethodName = "Update_Jinken", Descprition = "車輌月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S14_CARSB_DEL_Jinken", ServiceClass = "S14SB", MethodName = "Delete_Jinken", Descprition = "車輌月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S14_CARSB_SCH_Jinken", ServiceClass = "S14SB", MethodName = "GetDataSCH_Jinken", Descprition = "車輌月次経費入力検索用", },

			//固定費
            //new WCFDataAccessConfig() { Name = "S14_CARSB_Kotei", ServiceClass = "S14SB", MethodName = "GetData_Kotei", Descprition = "車輌月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S14_CARSB_UP_Kotei", ServiceClass = "S14SB", MethodName = "Update_Kotei", Descprition = "車輌月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S14_CARSB_DEL_Kotei", ServiceClass = "S14SB", MethodName = "Delete_Kotei", Descprition = "車輌月次経費入力", },
            //new WCFDataAccessConfig() { Name = "S14_CARSB_SCH_Kotei", ServiceClass = "S14SB", MethodName = "GetDataSCH_Kotei", Descprition = "車輌月次経費入力検索用", },

			// SHR02010 支払先明細書 [社内用] 
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR02010", ServiceClass = "SHR02010", MethodName = "GetListSHR02010", Descprition = "支払先明細書印刷(SHR02010)", },

			// SHR02010 支払先明細書 [社外用] 
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR02010S", ServiceClass = "SHR02010", MethodName = "GetDataHinList2", Descprition = "支払先明細書印刷(SHR02010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR02010_CSV", ServiceClass = "SHR02010", MethodName = "GetListSHR02010", Descprition = "支払先明細書印刷(SHR02010)", },
            //new WCFDataAccessConfig() { Name = "SPREAD_SHR02010", ServiceClass = "SHR02010", MethodName = "GetListSHR02010_SPREAD", Descprition = "請求書発行内訳別SPREAD(SHR02010)", },

			// NNG01010 得意先別月別売上合計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_NNG01010", ServiceClass = "NNG01010", MethodName = "SEARCH_NNG01010_GetDataList", Descprition = "得意先別月別売上合計表(NNG01010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_NNG01010_CSV", ServiceClass = "NNG01010", MethodName = "SEARCH_NNG01010_CSV_GetDataList", Descprition = "得意先別月別売上合計表(NNG01010)", },

			// NNG02010 支払先別月別支払合計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_NNG02010", ServiceClass = "NNG02010", MethodName = "SEARCH_NNG02010_GetDataList", Descprition = "支払先別月別支払合計表(NNG01010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_NNG02010_CSV", ServiceClass = "NNG02010", MethodName = "SEARCH_NNG02010_CSV_GetDataList", Descprition = "支払先別月別支払合計表(NNG01010)", },

			// NNG03010 乗務員月別売上合計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_NNG03010", ServiceClass = "NNG03010", MethodName = "NNG03010_GetDataHinList", Descprition = "乗務員月別売上合計表(NNG03010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_NNG03010_CSV", ServiceClass = "NNG03010", MethodName = "NNG03010_GetDataHinList", Descprition = "乗務員月別売上合計表(NNG03010)", },

			// NNG04010 車輌月別売上合計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_NNG04010", ServiceClass = "NNG04010", MethodName = "NNG04010_GetDataHinList", Descprition = "車輌月別売上合計表(NNG04010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_NNG04010_CSV", ServiceClass = "NNG04010", MethodName = "NNG04010_GetDataHinList", Descprition = "車輌月別売上合計表(NNG04010)", },

			// NNG05010 車種月別売上合計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_NNG05010", ServiceClass = "NNG05010", MethodName = "NNG05010_GetDataHinList", Descprition = "車種月別売上合計表(NNG05010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_NNG05010_CSV", ServiceClass = "NNG05010", MethodName = "NNG05010_GetDataHinList", Descprition = "車種月別売上合計表(NNG05010)", },

			// NNG06010 部門別合計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_NNG06010", ServiceClass = "NNG06010", MethodName = "GetDataList", Descprition = "部門別合計表(NNG06010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_NNG06010_CSV", ServiceClass = "NNG06010", MethodName = "GetDataList_CSV", Descprition = "部門別合計表(NNG06010)", },

			// NNG07010 部門別日計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_NNG07010", ServiceClass = "NNG07010", MethodName = "GetDataList", Descprition = "部門別日計表(NNG07010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_NNG07010_CSV", ServiceClass = "NNG07010", MethodName = "GetDataList_CSV", Descprition = "部門別日計表(NNG07010)", },

			// NNG08010 部門別売上合計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_NNG08010", ServiceClass = "NNG08010", MethodName = "SEARCH_NNG08010", Descprition = "部門別売上合計表(NNG08010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_NNG08011", ServiceClass = "NNG08010", MethodName = "SEARCH_NNG08010", Descprition = "部門別売上合計表(NNG08010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_NNG08010_CSV", ServiceClass = "NNG08010", MethodName = "SEARCH_NNG08010_CSV", Descprition = "部門別売上合計表(NNG08010)", },

			// SHR06010 支払先累計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR06010", ServiceClass = "SHR06010", MethodName = "SHR06010_GetDataHinList", Descprition = "支払先累計表(SHR06010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR06010_CSV", ServiceClass = "SHR06010", MethodName = "SHR06010_GetDataHinList_CSV", Descprition = "支払先累計表(SHR06010)", },

			// SHR07010 支払残高問い合わせ 
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR07010", ServiceClass = "SHR07010", MethodName = "SHR07010_GetDataHinList", Descprition = "支払残高問い合わせ(SHR07010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR07010_CSV", ServiceClass = "SHR07010", MethodName = "SHR07010_GetDataHinList", Descprition = "支払残高問い合わせ(SHR07010)", },


			// SHR08010 支払先一覧表　締日データ 
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR08010", ServiceClass = "SHR08010", MethodName = "SHR08010_GetDataHinList", Descprition = "支払先一覧表 締日データ(SHR08010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR08010_CSV", ServiceClass = "SHR08010", MethodName = "SHR08010_GetDataHinList", Descprition = "支払先一覧表 締日データ(SHR08010)", },

			// SHR08010 支払先一覧表　月次データ 
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR08010g", ServiceClass = "SHR08010", MethodName = "SHR08010_GetDataHinList2", Descprition = "支払先一覧表 月次データ(SHR08010g)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR08010g_CSV", ServiceClass = "SHR08010", MethodName = "SHR08010_GetDataHinList2", Descprition = "支払先一覧表 月次データ(SHR08010g)", },

			// SHR10010 支払先別支払日計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR10010", ServiceClass = "SHR10010", MethodName = "SHR10010_GetDataHinList", Descprition = "支払先別支払日計表(SHR10010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR10010_CSV", ServiceClass = "SHR10010", MethodName = "SHR10010_GetDataHinList", Descprition = "支払先別支払日計表(SHR10010)", },

			// SHR09010 支払先予定表 
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR09010", ServiceClass = "SHR09010", MethodName = "SHR09010_GetDataList", Descprition = "支払予定表(SHR09010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR09010_CSV", ServiceClass = "SHR09010", MethodName = "SHR09010_CSV_GetDataList", Descprition = "支払予定表(SHR09010)", },

			// SHR12010 取引先別管理表 
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR12010", ServiceClass = "SHR12010", MethodName = "SHR12010_GetDataHinList", Descprition = "取引先管理表(SHR12010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR12010_CSV", ServiceClass = "SHR12010", MethodName = "SHR12010_GetDataHinList", Descprition = "取引先管理表(SHR12010)", },

			// SHR13010 取引先別相殺管理表 
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR13010", ServiceClass = "SHR13010", MethodName = "SHR13010_GetDataHinList", Descprition = "取引先別相殺管理表(SHR13010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR13010_CSV", ServiceClass = "SHR13010", MethodName = "SHR13010_GetDataHinList", Descprition = "取引先別相殺管理表(SHR13010)", },

			// SHR05010 支払合計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR05010_Simebi", ServiceClass = "SHR05010", MethodName = "SEARCH_SHR05010_SimebiPreview", Descprition = "支払合計表プレビュー(SHR05010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR05010_Getsuji", ServiceClass = "SHR05010", MethodName = "SEARCH_SHR05010_GetsujiPreview", Descprition = "支払合計表プレビュー(SHR05010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR05010_Simebi_CSV", ServiceClass = "SHR05010", MethodName = "SEARCH_SHR05010_SimebiCSV", Descprition = "支払合計表プレビュー(SHR05010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SHR05010_Getsuji_CSV", ServiceClass = "SHR05010", MethodName = "SEARCH_SHR05010_GetsujiCSV", Descprition = "支払合計表プレビュー(SHR05010)", },

			// TKS02010 請求書発行内訳別 
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS02010", ServiceClass = "TKS02010", MethodName = "GetListTKS02010", Descprition = "請求書発行内訳別(TKS02010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS02010_CSV", ServiceClass = "TKS02010", MethodName = "GetListTKS02010", Descprition = "請求書発行内訳別CSV(TKS02010)", },
            //new WCFDataAccessConfig() { Name = "SPREAD_TKS02010", ServiceClass = "TKS02010", MethodName = "GetListTKS02010_SPREAD", Descprition = "請求書発行内訳別SPREAD(TKS02010)", },

			// TKS03010 得意先売上明細書 
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS03010", ServiceClass = "TKS03010", MethodName = "GetListTKS03010", Descprition = "得意先売上明細書(TKS03010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS03010_CSV", ServiceClass = "TKS03010", MethodName = "GetListTKS03010", Descprition = "得意先売上明細書CSV(TKS03010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS99999_CSV", ServiceClass = "TKS99999", MethodName = "GetListTKS99999", Descprition = "得意先売上明細書(TKS03010)", },
            //new WCFDataAccessConfig() { Name = "SPREAD_TKS03010", ServiceClass = "TKS03010", MethodName = "GetListTKS03010_SPREAD", Descprition = "得意先売上明細書(TKS03010)", },

			// TKS05010 得意先締日集計 
            //new WCFDataAccessConfig() { Name = "TKS05010_KIKAN_SET", ServiceClass = "TKS05010", MethodName = "TKS05010_KIKAN_SET", Descprition = "得意先締日集計(TKS05010)", },
            //new WCFDataAccessConfig() { Name = "TKS05010_SYUKEI", ServiceClass = "TKS05010", MethodName = "TKS05010_SYUKEI", Descprition = "得意先締日集計(TKS05010)", },

			// TKS99999 得意先売上明細書 
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS99999", ServiceClass = "TKS99999", MethodName = "GetListTKS99999", Descprition = "得意先売上明細書(TKS03010)", },
            //new WCFDataAccessConfig() { Name = "SPREAD_TKS99999", ServiceClass = "TKS99999", MethodName = "GetListTKS99999_SPREAD", Descprition = "得意先売上明細書(TKS03010)", },

			// TKS06010 売上合計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS06010_Simebi", ServiceClass = "TKS06010", MethodName = "SEARCH_TKS06010_SimebiPreview", Descprition = "売上合計表プレビュー(TKS06010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS06010_Getsuji", ServiceClass = "TKS06010", MethodName = "SEARCH_TKS06010_GetsujiPreview", Descprition = "売上合計表プレビュー(TKS06010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS06010_Simebi_CSV", ServiceClass = "TKS06010", MethodName = "SEARCH_TKS06010_SimebiCSV", Descprition = "売上合計表CSV(TKS06010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS06010_Getsuji_CSV", ServiceClass = "TKS06010", MethodName = "SEARCH_TKS06010_GetsujiCSV", Descprition = "売上合計表CSV(TKS06010)", },

			// TKS04010 売上日計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS04010", ServiceClass = "TKS04010", MethodName = "SEARCH_TKS04010_Preview", Descprition = "売上日計表プレビュー(TKS04010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS04010_CSV", ServiceClass = "TKS04010", MethodName = "SEARCH_TKS04010_CSV", Descprition = "売上日計表CSV(TKS04010)", },

			// TKS07010 売上累計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS07010", ServiceClass = "TKS07010", MethodName = "TKS07010_GetDataHinList", Descprition = "売上累計表(TKS07010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS07010_CSV", ServiceClass = "TKS07010", MethodName = "TKS07010_GetDataHinList_CSV", Descprition = "売上累計表CSV(TKS07010)", },

			// TKS08010 請求一覧表 
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS08010_Simebi", ServiceClass = "TKS08010", MethodName = "SEARCH_TKS08010_SimebiPreview", Descprition = "請求一覧表プレビュー(TKS06010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS08010_Getsuji", ServiceClass = "TKS08010", MethodName = "SEARCH_TKS08010_GetsujiPreview", Descprition = "請求一覧表プレビュー(TKS06010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS08010_Simebi_CSV", ServiceClass = "TKS08010", MethodName = "SEARCH_TKS08010_SimebiCSV", Descprition = "請求一覧表CSV(TKS06010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS08010_Getsuji_CSV", ServiceClass = "TKS08010", MethodName = "SEARCH_TKS08010_GetsujiCSV", Descprition = "請求一覧表CSV(TKS06010)", },

			// TKS09010 得意先残高問い合わせ 
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS09010", ServiceClass = "TKS09010", MethodName = "TKS09010_GetDataHinList", Descprition = "得意先残高問い合わせ(TKS09010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS09010_CSV", ServiceClass = "TKS09010", MethodName = "TKS09010_GetDataHinList_CSV", Descprition = "得意先残高問い合わせ(TKS09010)", },

			// TKS10010 入金滞留管理表 
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS10010", ServiceClass = "TKS10010", MethodName = "TKS10010_GetDataHinList", Descprition = "入金滞留管理表(TKS10010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS10010_CSV", ServiceClass = "TKS10010", MethodName = "TKS10010_GetDataHinList", Descprition = "入金滞留管理表(TKS10010)", },

			// TKS11010 回収予定表 
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS11010", ServiceClass = "TKS11010", MethodName = "SEARCH_TKS11010_GetDataList", Descprition = "回収予定表プレビュー(TKS11010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS11010_CSV", ServiceClass = "TKS11010", MethodName = "SEARCH_TKS11010_GetDataList_CSV", Descprition = "回収予定表プレビュー(TKS11010)", },

			// TKS12010 取引先管理表 ※SHR12010の使いまわし※ 
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS12010", ServiceClass = "SHR12010", MethodName = "SHR12010_GetDataHinList", Descprition = "取引先管理表(TKS12010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS12010_CSV", ServiceClass = "SHR12010", MethodName = "SHR12010_GetDataHinList", Descprition = "取引先管理表(TKS12010)", },

			// 得意先売上明細書 TKS13010 
            //new WCFDataAccessConfig() { Name = "TKS13010", ServiceClass = "TKS13010", MethodName = "GetListTKS13010", Descprition = "売上明細", },

			// TKS14010 取引先別相殺管理表 ※SHR13010の使いまわし※ 
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS14010", ServiceClass = "SHR13010", MethodName = "SHR13010_GetDataHinList", Descprition = "取引先別相殺管理表(TKS14010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS14010_CSV", ServiceClass = "SHR13010", MethodName = "SHR13010_GetDataHinList", Descprition = "取引先別相殺管理表(TKS14010)", },

			// TKS15010 売上分析グラフ 
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS15010", ServiceClass = "TKS15010", MethodName = "TKS15010_GetData", Descprition = "売上分析グラフ(TKS15010)", },

			// TKS16010 売上分析グラフ 
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS16010", ServiceClass = "TKS16010", MethodName = "TKS16010_GetData", Descprition = "売上分析グラフ(TKS16010)", },

			// TKS17010 売上推移表 
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS17010", ServiceClass = "TKS17010", MethodName = "TKS17010_GetData", Descprition = "売上推移表(TKS17010)", },

			// TKS18010 部門別日計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS18010", ServiceClass = "TKS18010", MethodName = "TKS18010_GetData", Descprition = "部門別日計表(TKS18010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS18010_CSV", ServiceClass = "TKS18010", MethodName = "TKS18010_GetData_CSV", Descprition = "部門別日計表(TKS18010)", },

			// TKS19010 車輌別日計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS19010", ServiceClass = "TKS19010", MethodName = "TKS19010_GetData", Descprition = "車輌別日計表(TKS19010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS19010_CSV", ServiceClass = "TKS19010", MethodName = "TKS19010_GetData_CSV", Descprition = "車輌別日計表(TKS19010)", },

			// TKS20010 得意先部門別売上管理表 
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS20010", ServiceClass = "TKS20010", MethodName = "TKS20010_GetData", Descprition = "得意先別部門別売上管理表(TKS20010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS20010_CSV", ServiceClass = "TKS20010", MethodName = "TKS20010_GetData_CSV", Descprition = "得意先別部門別売上管理表(TKS20010)", },

			// TKS21010 部門別売上推移表 
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS21010", ServiceClass = "TKS21010", MethodName = "TKS21010_GetData", Descprition = "部門別売上推移表(TKS21010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS21010_CSV", ServiceClass = "TKS21010", MethodName = "TKS21010_GetData_CSV", Descprition = "部門別売上推移表(TKS21010)", },

			// TKS21011 部門別売上推移表前年対比 
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS21011", ServiceClass = "TKS21010", MethodName = "TKS21011_GetData", Descprition = "部門別売上推移表前年対比(TKS21011)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_TKS21011_CSV", ServiceClass = "TKS21010", MethodName = "TKS21011_GetData_CSV", Descprition = "部門別売上推移表前年対比(TKS21011)", },

			// JMI01010 乗務員出勤表 
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI01010", ServiceClass = "JMI01010", MethodName = "GetDataList", Descprition = "乗務員出勤表(JMI01010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI01010_CSV", ServiceClass = "JMI01010", MethodName = "GetDataList_CSV", Descprition = "乗務員出勤表(JMI01010)", },

			// JMI02010 乗務員管理表 
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI02010", ServiceClass = "JMI02010", MethodName = "GetDataList", Descprition = "乗務員管理表(JMI02010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI02010_CSV", ServiceClass = "JMI02010", MethodName = "GetDataList_CSV", Descprition = "乗務員管理表(JMI02010)", },

			// JMI03010 乗務員労務管理表 
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI03010", ServiceClass = "JMI03010", MethodName = "GetDataList", Descprition = "乗務員労務管理表(JMI03010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI03010_CSV", ServiceClass = "JMI03010", MethodName = "GetDataList_CSV", Descprition = "乗務員労務管理表(JMI03010)", },

			// JMI04010 乗務員管理合計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI04010", ServiceClass = "JMI04010", MethodName = "GetDataList", Descprition = "乗務員管理合計表(JMI04010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI04010_CSV", ServiceClass = "JMI04010", MethodName = "GetDataList_CSV", Descprition = "乗務員管理合計表(JMI04010)", },

			// JM5010 乗務員売上明細書 
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI05010", ServiceClass = "JMI05010", MethodName = "GetDataList", Descprition = "乗務員売上明細書JMI05010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI05010_Syagai", ServiceClass = "JMI05010", MethodName = "GetDataList_Syagai", Descprition = "乗務員売上明細書社外JMI05010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI05010_CSV", ServiceClass = "JMI05010", MethodName = "GetDataList_CSV", Descprition = "乗務員売上明細書(JMI05010)", },

			// JM6010 乗務員売上日計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI06010", ServiceClass = "JMI06010", MethodName = "GetDataList", Descprition = "乗務員売上日計表JMI06010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI06010_CSV", ServiceClass = "JMI06010", MethodName = "GetDataList_CSV", Descprition = "乗務員売上日計表(JMI06010)", },

			// JM7010 乗務員売上合計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI07010", ServiceClass = "JMI07010", MethodName = "GetDataList", Descprition = "乗務員売上合計表JMI07010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI07010_CSV", ServiceClass = "JMI07010", MethodName = "GetDataList_CSV", Descprition = "乗務員売上合計表(JMI07010)", },

			// JM08010 乗務員売上合計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI08010A", ServiceClass = "JMI08010", MethodName = "GetDataListA", Descprition = "乗務員状況履歴JMI08010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI08010B", ServiceClass = "JMI08010", MethodName = "GetDataListB", Descprition = "乗務員状況履歴JMI08010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI08010C", ServiceClass = "JMI08010", MethodName = "GetDataListC", Descprition = "乗務員状況履歴JMI08010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI08010D", ServiceClass = "JMI08010", MethodName = "GetDataListD", Descprition = "乗務員状況履歴JMI08010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI08010A_CSV", ServiceClass = "JMI08010", MethodName = "GetDataListA_CSV", Descprition = "乗務員状況履歴(JMI08010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI08010B_CSV", ServiceClass = "JMI08010", MethodName = "GetDataListB_CSV", Descprition = "乗務員状況履歴(JMI08010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI08010C_CSV", ServiceClass = "JMI08010", MethodName = "GetDataListC_CSV", Descprition = "乗務員状況履歴(JMI08010)", },

			// JM08010 乗務員売上合計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI09010", ServiceClass = "JMI09010", MethodName = "GetDataList", Descprition = "運転免許管理表JMI09010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI09010_CSV", ServiceClass = "JMI09010", MethodName = "GetDataList_CSV", Descprition = "運転免許管理表(JMI09010)", },

			// JM10010 乗務員月次集計 
            //new WCFDataAccessConfig() { Name = "SYUKEI_JMI10010", ServiceClass = "JMI10010", MethodName = "SYUKEI", Descprition = "乗務員月次集計JMI10010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI09010_CSV", ServiceClass = "JMI09010", MethodName = "GetDataList_CSV", Descprition = "運転免許管理表(JMI09010)", },

			// JM13010 乗務員収支合計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI13010", ServiceClass = "JMI13010", MethodName = "JMI13010_GetDataList", Descprition = "乗務員収支合計表JMI13010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI13010_CSV", ServiceClass = "JMI13010", MethodName = "JMI13010_GetDataList_CSV", Descprition = "乗務員収支合計表JMI13010)", },

			// JM14010 給与システムデータ作成 
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI14010", ServiceClass = "JMI14010", MethodName = "GetDataList", Descprition = "給与システムデータ作成JMI14010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI14010_CSV", ServiceClass = "JMI14010", MethodName = "GetDataList_CSV", Descprition = "給与システムデータ作成JMI14010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI14010_SYUKKIN_CSV", ServiceClass = "JMI14010", MethodName = "GetDataList_SYUKKIN_CSV", Descprition = "給与システムデータ作成JMI14010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI14010_KEIHI_CSV", ServiceClass = "JMI14010", MethodName = "GetDataList_KEIHI_CSV", Descprition = "給与システムデータ作成JMI14010)", },
			
			
			// SRY01010 車輌管理表 
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY01010", ServiceClass = "SRY01010", MethodName = "GetDataList", Descprition = "車輌管理表SRY01010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY01010_CSV", ServiceClass = "SRY01010", MethodName = "GetDataList_CSV", Descprition = "車輌管理表(SRY01010)", },

			// SRY02010 車輌売上明細書 
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY02010", ServiceClass = "SRY02010", MethodName = "GetDataList", Descprition = "車輌売上明細書SRY02010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY02010_CSV", ServiceClass = "SRY02010", MethodName = "GetDataList_CSV", Descprition = "車輌売上明細書(SRY02010)", },

			// SRY03010 車輌日計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY03010", ServiceClass = "SRY03010", MethodName = "GetDataList", Descprition = "車輌日計表SRY03010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY03010_CSV", ServiceClass = "SRY03010", MethodName = "GetDataList_CSV", Descprition = "車輌日計表(SRY03010)", },

			// SRY04010 車種日計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY04010", ServiceClass = "SRY04010", MethodName = "GetDataList", Descprition = "車種日計表SRY04010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY04010_CSV", ServiceClass = "SRY04010", MethodName = "GetDataList_CSV", Descprition = "車種日計表(SRY03010)", },

			// SRY05010 車輌月次集計 
            //new WCFDataAccessConfig() { Name = "SYUKEI_SRY05010", ServiceClass = "SRY05010", MethodName = "SYUKEI", Descprition = "車輌月次集計SRY05010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY05010_CSV", ServiceClass = "SRY05010", MethodName = "GetDataList_CSV", Descprition = "車輌月次集計(SRY05010)", },

			// SRY07010 車輌収支実績表 
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY07010", ServiceClass = "SRY07010", MethodName = "SRY07010_GetDataList", Descprition = "車輌収支実績表(SRY07010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY07010_CSV", ServiceClass = "SRY07010", MethodName = "SRY07010_GetData_CSV", Descprition = "車輌収支実績表(SRY07010)", },

			// SRY08010 車輌収支実績表 
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY08010", ServiceClass = "SRY08010", MethodName = "SRY08010_GetDataList", Descprition = "車輌収支実績表(SRY08010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY08010_CSV", ServiceClass = "SRY08010", MethodName = "SRY08010_GetData_CSV", Descprition = "車輌収支実績表(SRY08010)", },

			// SRY09010 車輌合計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY09010", ServiceClass = "SRY09010", MethodName = "SRY09010_GetDataHinList", Descprition = "車輌合計表(SRY09010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY09010_CSV", ServiceClass = "SRY09010", MethodName = "SRY09010_GetDataHinList", Descprition = "車輌合計表(SRY09010)", },

			// SRY10010 車輌統計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY10010", ServiceClass = "SRY10010", MethodName = "SRY10010_GetDataList", Descprition = "車輌統計表表(SRY10010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY10010_CSV", ServiceClass = "SRY10010", MethodName = "SRY10010_GetData_CSV", Descprition = "車輌統計表(SRY10010)", },

			// SRY11010 車種収支実績表 
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY11010", ServiceClass = "SRY11010", MethodName = "SRY11010_GetDataList", Descprition = "車種収支実績表(SRY11010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY11010_CSV", ServiceClass = "SRY11010", MethodName = "SRY11010_GetData_CSV", Descprition = "車種収支実績表(SRY11010)", },

			// SRY12010 車種合計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY12010", ServiceClass = "SRY12010", MethodName = "SRY12010_GetDataHinList", Descprition = "車種合計表(SRY12010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY12010_CSV", ServiceClass = "SRY12010", MethodName = "SRY12010_GetData_CSV", Descprition = "車種合計表(SRY12010)", },

			// SRY13010 車種統計表 
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY13010", ServiceClass = "SRY13010", MethodName = "SRY13010_GetDataList", Descprition = "車種統計表表(SRY13010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY13010_CSV", ServiceClass = "SRY13010", MethodName = "SRY13010_GetData_CSV", Descprition = "車輌統計表(SRY13010)", },

			// SRY14010 輸送実績報告書 
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY14010", ServiceClass = "SRY14010", MethodName = "SRY14010_GetDataHinList", Descprition = "輸送実績報告書(SRY14010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY14010_CSV", ServiceClass = "SRY14010", MethodName = "SRY14010_GetDataHinList_CSV", Descprition = "輸送実績報告書(SRY14010)", },

			// SRY17010 車輌管理台帳 
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY17010", ServiceClass = "SRY17010", MethodName = "SRY17010_GetDataHinList", Descprition = "車輌管理台帳(SRY17010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY17010_CSV", ServiceClass = "SRY17010", MethodName = "SRY17010_GetDataHinList", Descprition = "車輌管理台帳(SRY17010)", },

			// SRY19010 車輌別燃料消費量実績表 
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY19010", ServiceClass = "SRY19010", MethodName = "SRY19010_GetDataHinList", Descprition = "車輌別燃料消費量実績表(SRY09010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY19010_CSV", ServiceClass = "SRY19010", MethodName = "SRY19010_GetDataHinList", Descprition = "車輌別燃料消費量実績表(SRY09010)", },

			// SRY20010 燃費管理表 
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY20010", ServiceClass = "SRY20010", MethodName = "GetDataList", Descprition = "燃費管理表(SRY20010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY20010_CSV", ServiceClass = "SRY20010", MethodName = "GetDataList_CSV", Descprition = "燃費管理表(SRY20010)", },

			// SRY21010 車検予定管理表 
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY21010", ServiceClass = "SRY21010", MethodName = "SEARCH_SRY21010_GetDataList", Descprition = "車検予定管理表(SRY21010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY21010_CSV", ServiceClass = "SRY21010", MethodName = "SEARCH_SRY21010_GetDataList", Descprition = "車検予定管理表(SRY21010)", },

			// 車輌点検 
            //new WCFDataAccessConfig() { Name = "LOAD_SRY22010", ServiceClass = "SRY22010", MethodName = "LOAD_GetData", Descprition = "車輌点検(検索)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY22010", ServiceClass = "SRY22010", MethodName = "SEARCH_GetData", Descprition = "車輌点検(検索)", },
            //new WCFDataAccessConfig() { Name = "INSERT_SRY22010", ServiceClass = "SRY22010", MethodName = "INSERT_GetData", Descprition = "車輌点検(登録)", },
            //new WCFDataAccessConfig() { Name = "NINSERT_SRY22010", ServiceClass = "SRY22010", MethodName = "NINSERT_GetData", Descprition = "車輌点検(F9登録)", },
            //new WCFDataAccessConfig() { Name = "DELETE_SRY22010", ServiceClass = "SRY22010", MethodName = "DELETE_GetData", Descprition = "車輌点検(削除)", },
            //new WCFDataAccessConfig() { Name = "LOAD_SRY22020", ServiceClass = "SRY22010", MethodName = "LOAD_GetData2", Descprition = "車輌点検(検索)", },
            //new WCFDataAccessConfig() { Name = "INSERT_SRY22020", ServiceClass = "SRY22010", MethodName = "INSERT_GetData", Descprition = "車輌点検(検索)", },
            //new WCFDataAccessConfig() { Name = "OUTPUT_SRY22010_M87_CNTL", ServiceClass = "SRY22010", MethodName = "OUTPUT_SRY22010_M87_CNTL", Descprition = "車輌点検(検索)", },
            //new WCFDataAccessConfig() { Name = "OUTPUT_SRY22010", ServiceClass = "SRY22010", MethodName = "SERCHE_OUTPUT", Descprition = "車輌点検(検索)", },
            //new WCFDataAccessConfig() { Name = "DELETE_SRY22020", ServiceClass = "SRY22010", MethodName = "DELETE_GetData", Descprition = "データ削除", },

			// SRY23010 付表作成 
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY23010", ServiceClass = "SRY23010", MethodName = "SRY23010_GetData", Descprition = "付表作成(付表1)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY23020", ServiceClass = "SRY23010", MethodName = "SRY23020_GetData", Descprition = "付表作成(付表2)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY23010_EXCEL", ServiceClass = "SRY23010", MethodName = "SRY23010_GetData", Descprition = "付表出力(付表1)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY23020_EXCEL", ServiceClass = "SRY23010", MethodName = "SRY23020_GetData", Descprition = "付表出力(付表1)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY23010_GDATE", ServiceClass = "SRY23010", MethodName = "SRY23010_GDATE_GetData", Descprition = "グリーン日付取得", },

			// SRY24010 稼働日数管理表 
            //new WCFDataAccessConfig() { Name = "SEARCH_SRY24010", ServiceClass = "SRY24010", MethodName = "SRY24010_GetData", Descprition = "稼働日数管理表", },

			// レポート定義データ関連 
            //new WCFDataAccessConfig() { Name = "M99_RPT_GET", ServiceClass = "M99", MethodName = "GetReportDefine", Descprition = "クリスタルレポート定義ファイルデータ取得", },
            //new WCFDataAccessConfig() { Name = "M99_RPT_UPD", ServiceClass = "M99", MethodName = "PutReportDefine", Descprition = "クリスタルレポート定義ファイルデー更新", },

			// プロシージャ定義 
            //new WCFDataAccessConfig() { Name = "JMI11010", ServiceClass = "M04", MethodName = "RunStoredJMI11010", Descprition = "乗務員収支実績表", },
            //new WCFDataAccessConfig() { Name = "DLY16010", ServiceClass = "T01", MethodName = "RunStoredDLY16010", Descprition = "乗務員運行表", },

			// SRY12010 乗務員収支実績表 
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI12010", ServiceClass = "JMI12010", MethodName = "JMI12010_GetDataList", Descprition = "乗務員収支実績表(JMI12010)", },
            //new WCFDataAccessConfig() { Name = "SEARCH_JMI12010_CSV", ServiceClass = "JMI12010", MethodName = "JMI12010_GetDataList_CSV", Descprition = "乗務員収支実績表(JMI12010)", },

            #endregion

        };

    }

}
