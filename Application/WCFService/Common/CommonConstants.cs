using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// システム共通定数管理クラス
    /// </summary>
    public class CommonConstants
    {
        #region << パラメータ定義 >>

        /// <summary>
        /// ページングボタン用
        /// パラメータ定義
        /// </summary>
        public enum PagingOption : int
        {
            /// <summary>先頭データ取得</summary>
            Paging_Top = -2,
            /// <summary>前データ取得</summary>
            Paging_Before = -1,
            /// <summary>指定コード取得</summary>
            Paging_Code = 0,
            /// <summary>次データ取得</summary>
            Paging_After = 1,
            /// <summary>最終データ取得</summary>
            Paging_End = 2
        }

        #endregion

        #region << コード名 文字定数定義 >>

        public static string 商品分類_食品 = "食品";
        public static string 商品分類_繊維 = "繊維";
        public static string 商品分類_その他 = "その他";

        public static string 商品形態分類_SET品 = "SET品";
        public static string 商品形態分類_資材単品 = "資材・単品";
        public static string 商品形態分類_雑コード = "雑コード";
        public static string 商品形態分類_副資材 = "副資材";

        public static string 伝票要否_未発行 = "未発行";
        public static string 伝票要否_発行済 = "発行済";
        public static string 伝票要否_発行しない = "発行しない";

        public static string 売上区分_通常売上 = "通常売上";
        public static string 売上区分_販社売上 = "販社売上";
        public static string 売上区分_メーカー直送 = "メーカー直送";
        public static string 売上区分_メーカー販社商流直送 = "メーカー販社商流直送";
        public static string 売上区分_委託売上 = "委託売上";
        public static string 売上区分_預け売上 = "預け売上";
        public static string 売上区分_通常売上返品 = "通常売上返品";
        public static string 売上区分_販社売上返品 = "販社売上返品";
        public static string 売上区分_メーカー直送返品 = "メーカー直送返品";
        public static string 売上区分_メーカー販社商流直送返品 = "メーカー販社商流直送返品";
        public static string 売上区分_委託売上返品 = "委託売上返品";
        public static string 売上区分_預け売上返品 = "預け売上返品";

        public static string 自社区分_自社 = "自社";
        public static string 自社区分_販社 = "販社";

        public static string 移動区分_通常移動 = "通常移動";
        public static string 移動区分_売上移動 = "売上移動";
        public static string 移動区分_調整移動 = "調整移動";
        public static string 移動区分_振替移動 = "振替移動";    // No.294 Add

        public static string 入力区分_仕入入力 = "仕入入力";
        public static string 入力区分_売上入力 = "売上入力";

        public static string 仕入区分_通常 = "通常";
        public static string 仕入区分_返品 = "返品";

        public static string 取引区分_得意先 = "得意先";
        public static string 取引区分_仕入先 = "仕入先";
        public static string 取引区分_加工先 = "加工先";
        public static string 取引区分_相殺 = "相殺";
        public static string 取引区分_販社 = "販社";

        public static string 明細番号ID_売上_仕入_移動 = "売上・仕入・移動";
        public static string 明細番号ID_揚り = "揚り";
        public static string 明細番号ID_入金 = "入金";
        public static string 明細番号ID_出金 = "出金";

        public static string 消費税区分_一括 = "一括";
        public static string 消費税区分_個別 = "個別";

        public static string 税区分_切捨て = "切捨て";
        public static string 税区分_四捨五入 = "四捨五入";
        public static string 税区分_切上げ = "切上げ";
        public static string 税区分_税なし = "税なし";

        public static string 商品消費税区分_通常 = "通常税率";
        public static string 商品消費税区分_軽減税率 = "軽減税率";
        public static string 商品消費税区分_非課税 = "非課税";

        public static string 金種コード_現金 = "現金 =";
        public static string 金種コード_振込 = "振込 =";
        public static string 金種コード_小切手 = "小切手";
        public static string 金種コード_手形 = "手形 =";
        public static string 金種コード_相殺 = "相殺 =";
        public static string 金種コード_調整 = "調整 =";
        public static string 金種コード_その他 = "その他";

        public static string 入出庫区分_入庫 = "入荷";
        public static string 入出庫区分_出庫 = "出荷";

        public static string 作成機能ID_仕入 = "仕入";
        public static string 作成機能ID_揚り = "揚り";
        public static string 作成機能ID_売上 = "売上";
        public static string 作成機能ID_移動 = "移動";
        public static string 作成機能ID_仕入返品 = "仕入返品";
        public static string 作成機能ID_売上返品 = "売上返品";

        // No-94 Add Start
        public static string 消費税区分略称_通常税率 = "";
        public static string 消費税区分略称_軽減税率 = "軽";
        public static string 消費税区分略称_非課税 = "非";
        // No-94 Add End

        #endregion

        #region << コード区分 列挙型定義 >>

        public enum 商品分類 : int
        {
            食品 = 1,
            繊維 = 2,
            その他 = 3
        }

        public enum 商品形態分類 : int
        {
            SET品 = 1,
            資材・単品 = 2,
            雑コード = 3,
            副資材 = 4
        }

        public enum 伝票要否 : int
        {
            未発行 = 0,
            発行済 = 1,
            発行しない = 2
        }

        public enum 売上区分 : int
        {
            // 売上系
            通常売上 = 1,
            販社売上 = 2,
            メーカー直送 = 3,
            メーカー販社商流直送 = 4,
            委託売上 = 5,
            預け売上 = 6,

            // 返品系
            通常売上返品 = 91,
            販社売上返品 = 92,
            メーカー直送返品 = 93,
            メーカー販社商流直送返品 = 94,
            委託売上返品 = 95,
            預け売上返品 = 96,
        }

        public enum 自社区分 : int
        {
            自社 = 0,
            販社 = 1
        }

        public enum 移動区分 : int
        {
            通常移動 = 1,
            売上移動 = 2,
            調整移動 = 3,
            振替移動 = 4
        }

        public enum 入力区分 : int
        {
            仕入入力 = 0,
            売上入力 = 1
        }

        public enum 仕入区分 : int
        {
            通常 = 0,
            返品 = 90
        }

        public enum 取引区分 : int
        {
            得意先 = 0,
            仕入先 = 1,
            加工先 = 2,
            相殺 = 3,
            販社 = 4
        }

        public enum 明細番号ID : int
        {
            ID01_売上_仕入_移動 = 1,
            ID02_揚り = 2,
            ID05_入金 = 5,
            ID06_出金 = 6
        }

        public enum 消費税区分 : int
        {
            ID01_一括 = 1,
            ID02_個別 = 2,
            ID03_請求無 = 3        // No.272 Add
        }

        public enum 税区分 : int
        {
            ID01_切捨て = 1,
            ID02_四捨五入 = 2,
            ID03_切上げ = 3,
            ID09_税なし = 9
        }

        // No-281 Add Start
        public enum 請求・支払区分 : int
        {
            ID01_以上 = 1,
            ID02_以下 = 2
        }
        // No-281 Add End

        public enum 商品消費税区分 : int
        {
            通常税率 = 0,
            軽減税率 = 1,
            非課税 = 2
        }

        public enum 金種コード : int
        {
            ID01_現金 = 1,
            ID02_振込 = 2,
            ID03_小切手 = 3,
            ID04_手形 = 4,
            ID05_相殺 = 5,
            ID06_調整 = 6,
            ID07_その他 = 7
        }

        public enum 入出庫区分 : int
        {
            ID01_入庫 = 1,
            ID02_出庫 = 2
        }

        public enum 作成機能ID : int
        {
            ID01_仕入 = 1,
            ID02_揚り = 2,
            ID03_売上 = 3,
            ID04_移動 = 4,
            ID91_仕入返品 = 91,
            ID93_売上返品 = 93
        }

        // No.272 Add Start
        public enum 単価種別 : int
        {
            原価 = 1,
            加工原価 = 2,
            卸値 = 3,
            売価 = 4
        }
        // No.272 Add End
        #endregion

        #region << コード区分 ディクショナリ定義 >>

        /// <summary>
        /// 入力区分のディクショナリを取得する
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> Get入力区分Dic()
        {
            return new Dictionary<int, string>()
                {
                    { 入力区分.仕入入力.GetHashCode(), 入力区分_仕入入力 },
                    { 入力区分.売上入力.GetHashCode(), 入力区分_売上入力 }
                };

        }

        /// <summary>
        /// 仕入区分のディクショナリを取得する
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> Get仕入区分Dic()
        {
            return new Dictionary<int, string>()
                {
                    { 仕入区分.通常.GetHashCode(), 仕入区分_通常 },
                    { 仕入区分.返品.GetHashCode(), 仕入区分_返品 }
                };

        }

        /// <summary>
        /// 商品分類のディクショナリを取得する
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> Get商品分類Dic()
        {
            return new Dictionary<int, string>()
                {
                    { 商品分類.食品.GetHashCode(), 商品分類_食品 },
                    { 商品分類.繊維.GetHashCode(), 商品分類_繊維 },
                    { 商品分類.その他.GetHashCode(), 商品分類_その他 }
                };

        }

        /// <summary>
        /// 商品形態分類のディクショナリを取得する
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> Get商品形態分類Dic()
        {
            return new Dictionary<int, string>()
                {
                    { 商品形態分類.SET品.GetHashCode(), 商品形態分類_SET品 },
                    { 商品形態分類.資材・単品.GetHashCode(), 商品形態分類_資材単品 },
                    { 商品形態分類.雑コード.GetHashCode(), 商品形態分類_雑コード },
                    { 商品形態分類.副資材.GetHashCode(), 商品形態分類_副資材 }
                };

        }

        /// <summary>
        /// 移動区分のディクショナリを取得する
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> Get移動区分Dic()
        {
            return new Dictionary<int, string>()
                {
                    { 移動区分.通常移動.GetHashCode(), 移動区分_通常移動 },
                    { 移動区分.売上移動.GetHashCode(), 移動区分_売上移動 },
                    { 移動区分.調整移動.GetHashCode(), 移動区分_調整移動 }
                };

        }

        /// <summary>
        /// 入出庫区分のディクショナリを取得する
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> Get入出庫区分Dic()
        {
            return new Dictionary<int, string>()
                {
                    { 入出庫区分.ID01_入庫.GetHashCode(), 入出庫区分_入庫 },
                    { 入出庫区分.ID02_出庫.GetHashCode(), 入出庫区分_出庫 }
                };

        }

        #endregion

        #region << 業務文字列定義 >>

        /// <summary>特殊ユーザID</summary>
        public static int SUPECIAL_USER_ID = 999999;
        /// <summary>【デフォルト】決算月</summary>
        public static int DEFAULT_SETTLEMENT_MONTH = 3;
        /// <summary>【デフォルト】締日(末日)</summary>
        public static int DEFAULT_CLOSING_DAY = 31;

        #endregion

    }

}