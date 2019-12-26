using GrapeCity.Windows.SpreadGrid;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace KyoeiSystem.Application.Windows.Views
{
    using FwRepPreview = KyoeiSystem.Framework.Reports.Preview;
    using WinForms = System.Windows.Forms;
    using WinFormsScreen = System.Windows.Forms.Screen;

    /// <summary>
    /// 棚卸更新 フォームクラス
    /// </summary>
    public partial class ZIK04010 : RibbonWindowViewBase
    {
        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;

        /// <summary>
        /// 画面固有設定項目のクラス定義
        /// ※ 必ず public で定義する。
        /// </summary>
        public class ConfigZIK04010 : FormConfigBase
        {
            public byte[] spConfigZIK04010 = null;
        }

        /// ※ 必ず public で定義する。
        public ConfigZIK04010 frmcfg = null;
        CommonConfig ccfg = null;
        // SPREAD初期状態保存用
        public byte[] sp_Config = null;

        #endregion

        #region << 定数定義 >>

        /// <summary>通信キー　対象年月日の棚卸更新の実行済み確認、棚卸入力確認を行う</summary>
        private const string CHECK_STOCKTAKING = "ZIK04010_IsCheckStocktaking";
        /// <summary>通信キー　棚卸更新処理を実行する</summary>
        private const string STOCKTAKING = "ZIK04010_InventoryStocktaking";

        #endregion

        /// <summary>
        /// 自社販社区分 内包データ
        /// </summary>
        private enum 自社販社区分 : int
        {
            自社 = 0,
            販社 = 1
        }

        #region << 画面初期処理 >>

        /// <summary>
        /// 棚卸更新
        /// </summary>
        public ZIK04010()
        {
            InitializeComponent();
            this.DataContext = this;

        }

        /// <summary>
        /// 画面読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            frmcfg = (ConfigZIK04010)ucfg.GetConfigValue(typeof(ConfigZIK04010));

            if (frmcfg == null)
            {
                frmcfg = new ConfigZIK04010();
                ucfg.SetConfigValue(frmcfg);
                frmcfg.spConfigZIK04010 = this.sp_Config;
            }
            else
            {
                // 表示できるかチェック
                var varWidthCHK = WinFormsScreen.PrimaryScreen.Bounds.Width - frmcfg.Left;
                if (varWidthCHK > 10)
                    this.Left = frmcfg.Left;

                // 表示できるかチェック
                var varHeightCHK = WinFormsScreen.PrimaryScreen.Bounds.Height - frmcfg.Top;
                if (varHeightCHK > 10)
                    this.Top = frmcfg.Top;

                this.Width = frmcfg.Width;
                this.Height = frmcfg.Height;

            }
            #endregion

            // 検索画面情報を設定
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });
            base.MasterMaintenanceWindowList.Add("M09_MYHIN", new List<Type> { typeof(MST02010), typeof(SCHM09_MYHIN) });
            base.MasterMaintenanceWindowList.Add("M14_BRAND", new List<Type> { typeof(MST04020), typeof(SCHM14_BRAND) });
            base.MasterMaintenanceWindowList.Add("M15_SERIES", new List<Type> { typeof(MST04021), typeof(SCHM15_SERIES) });
            base.MasterMaintenanceWindowList.Add("M22_SOUK", new List<Type> { typeof(MST12020), typeof(SCHM22_SOUK) });

            // コンボデータ取得
            AppCommon.SetutpComboboxList(this.cmbItemType, false);

            // 検索条件部の初期設定をおこなう
            initSearchControl();

            // 初期フォーカスを設定
            this.myCompany.SetFocus();
            ResetAllValidation();

        }

        #endregion

        #region << データ受信 >>

        /// <summary>
        /// 取得データの取り込み
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            try
            {
                this.ErrorMessage = string.Empty;

                base.SetFreeForInput();

                var varData = message.GetResultData();
                DataTable dtTbl = (varData is DataTable) ? (varData as DataTable) : null;

                switch (message.GetMessageName())
                {
                    // ===========================
                    // 対象年月日の棚卸更新の実行済み確認、棚卸入力確認 結果取得
                    // ( return 0:対象あり、-1:棚卸未入力、1:全件棚卸更新済み)
                    // ===========================
                    case CHECK_STOCKTAKING:

                        switch (varData.ToString())
                        {
                            case "-1":
                                // 棚卸未入力の場合
                                MessageBoxResult retNotEntered =
                                    MessageBox.Show(
                                        "棚卸入力が行われていません。処理を中止します。",
                                        "エラー",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);
                                break;

                            case "1":
                                // 全件棚卸更新済みの場合
                                MessageBoxResult retNoTarget =
                                    MessageBox.Show(
                                        "棚卸確定済みです。\n\r処理を中止します。",
                                        "エラー",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);
                                break;

                            default:
                                if (MessageBox.Show("棚卸更新を実行しますか？。\n\r※更新前の状態に戻せなくなりますが、よろしいですか？",
                                        "実行確認", MessageBoxButton.YesNo, MessageBoxImage.Question,
                                        MessageBoxResult.Yes) == MessageBoxResult.Yes)
                                {

                                    // ---------------------------
                                    // 棚卸更新処理を実行する
                                    // ---------------------------
                                    SendRequestUpdateStocktaking();

                                    base.SetBusyForInput();
                                }
                                break;
                        }    
                        break;

                    // ===========================
                    // 棚卸更新処理を実行 結果取得
                    // ===========================
                    case STOCKTAKING:
                        MessageBoxResult result =
                            MessageBox.Show(
                                "棚卸更新が終了しました。",
                                "確認",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                        break;

                }

            }
            catch (Exception ex)
            {
                base.SetFreeForInput();
                this.ErrorMessage = ex.Message;
            }

        }

        /// <summary>
        /// データ受信エラー
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            base.SetFreeForInput();
            this.ErrorMessage = (string)message.GetResultData();
        }

        #endregion

        #region << リボン >>

        #region F1 マスタ照会
        /// <summary>
        /// F1　リボン　マスタ検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            try
            {
                var varCtl = FocusManager.GetFocusedElement(this);
                var varM01Text = ViewBaseCommon.FindVisualParent<M01_TOK_TextBox>(varCtl as UIElement);

                if (varM01Text == null)
                {
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                }
                else
                {
                    varM01Text.OpenSearchWindow(this);

                }

            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }

        }
        #endregion

        #region F9 棚卸更新
        /// <summary>
        /// F9　リボン　棚卸更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            // 入力チェックを行う
            if (isCheckInput() == false)
            {
                return;
            }

            // ---------------------------
            // 対象年月日の棚卸更新の実行済み確認、棚卸入力確認を行う
            // ---------------------------
            SendRequestCheckStocktaking();

        }

        #endregion

        #region F11 終了
        /// <summary>
        /// F11　リボン　終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            this.Close();
        }
        #endregion

        #endregion

        #region << 機能処理関連 >>

        #region 画面初期化
        /// <summary>
        /// 画面初期化
        /// </summary>
        private void ScreenClear()
        {
            this.MaintenanceMode = null;

            ResetAllValidation();
            SetFocusToTopControl();

        }
        #endregion

        #region 画面表示設定
        /// <summary>
        /// 検索条件部の初期設定をおこなう
        /// </summary>
        private void initSearchControl()
        {
            this.myCompany.Text1 = ccfg.自社コード.ToString();
            this.myCompany.IsEnabled = ccfg.自社販社区分 == 自社販社区分.自社.GetHashCode();

        }
        #endregion

        #region 入力検証
        /// <summary>
        /// 入力チェックをおこなう
        /// </summary>
        private bool isCheckInput()
        {
            bool bolResult = false;

            // 入力検証
            if (!base.CheckAllValidation())
            {
                this.ErrorMessage = "入力内容に誤りがあります。";
                MessageBox.Show("入力内容に誤りがあります。");
                return bolResult;
            }

            // 自社コードの入力値検証
            int intCompanyCd;
            if (string.IsNullOrEmpty(myCompany.Text1))
            {
                base.ErrorMessage = "自社コードは必須入力項目です。";
                return bolResult;
            }
            else if (!int.TryParse(myCompany.Text1, out intCompanyCd))
            {
                base.ErrorMessage = "自社コードの入力値に誤りがあります。";
                return bolResult;
            }

            // 棚卸日
            DateTime dteStocktakingDate;
            if (!DateTime.TryParse(StocktakingDate.Text, out dteStocktakingDate))
            {
                ErrorMessage = "棚卸日の内容が正しくありません。";
                MessageBox.Show("入力エラーがあります。");
                return bolResult;
            }

            bolResult = true;
            return bolResult;

        }
        #endregion

        #region 棚卸更新実行部
        /// <summary>
        /// パラメータ辞書の作成を行う
        /// </summary>
        private Dictionary<string, string> setStocktakingParm()
        {
            // パラメータ生成
            Dictionary<string, string> dicCond = new Dictionary<string, string>();
            dicCond.Add("倉庫コード", Warehouse.Text1);
            dicCond.Add("自社品番", Product.Text1);
            dicCond.Add("自社品名", ProductName.Text);
            dicCond.Add("商品分類コード", cmbItemType.SelectedValue.ToString());
            dicCond.Add("ブランドコード", Brand.Text1);
            dicCond.Add("シリーズコード", Series.Text1);

            return dicCond;

        }

        /// <summary>
        /// 対象年月日の棚卸更新の実行済み確認、棚卸入力確認を行う
        /// </summary>
        private void SendRequestCheckStocktaking()
        {
            // パラメータ辞書の作成を行う
            Dictionary<string, string> dicCond = setStocktakingParm();

            // 対象年月日の棚卸更新の実行済み確認、棚卸入力確認を行う
            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    CHECK_STOCKTAKING,
                    new object[] {
                        int.Parse(myCompany.Text1),
                        StocktakingDate.Text,
                        dicCond
                    }));

        }

        /// <summary>
        /// 棚卸更新処理を実行する
        /// </summary>
        private void SendRequestUpdateStocktaking()
        {
            // パラメータ辞書の作成を行う
            Dictionary<string, string> dicCond = setStocktakingParm();

            // 棚卸更新処理を実行する
            base.SendRequest(
                new CommunicationObject(
                    MessageType.CallStoredProcedure,
                    STOCKTAKING,
                    new object[] {
                        int.Parse(myCompany.Text1),
                        StocktakingDate.Text,
                        dicCond,
                        ccfg.ユーザID
                    }));

        }

        #endregion

        #endregion

        #region << コントロールイベント群 >>

        #region Window_Closed
        /// <summary>
        /// 画面が閉じられた時、データを保持する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);

        }
        #endregion

        #endregion

    }

}
