using GrapeCity.Windows.SpreadGrid;
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
    /// <summary>
    /// 都度請求締集計 画面クラス
    /// </summary>
    public partial class TKS09010 : WindowReportBase
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
        public class ConfigTKS09010 : FormConfigBase
        {
            public byte[] spConfig = null;
        }

        /// ※ 必ず public で定義する。
        public ConfigTKS09010 frmcfg = null;
        CommonConfig ccfg = null;
        // SPREAD初期状態保存用
        public byte[] spConfig = null;

        #endregion

        #region << 列挙型定義 >>

        private enum GridColumnsMapping : int
        {
            ID = 0,
            得意先名 = 1,
            締日 = 2,
            クリア = 3,
            期間開始1 = 4,
            期間終了1 = 5,
            期間開始2 = 6,
            期間終了2 = 7,
            期間開始3 = 8,
            期間終了3 = 9,
        }

        #endregion

        #region << 定数定義 >>

        private const string BILLING_AGGREGATE = "TKS09010_BillingAggregation";

        private const string PARAMS_NAME_COMPANY = "自社コード";
        private const string PARAMS_NAME_CREATE_DATE_START = "集計開始日";
        private const string PARAMS_NAME_CREATE_DATE_END = "集計終了日";

        #endregion

        Dictionary<string, string> paramDic = new Dictionary<string, string>();


        #region << 画面初期処理 >>

        /// <summary>
        /// 都度請求締集計 コンストラクタ
        /// </summary>
        public TKS09010()
        {
            InitializeComponent();
            this.DataContext = this;

        }

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigTKS09010)ucfg.GetConfigValue(typeof(ConfigTKS09010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigTKS09010();
                ucfg.SetConfigValue(frmcfg);
                frmcfg.spConfig = this.spConfig;
            }
            else
            {
                //表示できるかチェック
                var WidthCHK = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - frmcfg.Left;
                if (WidthCHK > 10)
                {
                    this.Left = frmcfg.Left;
                }
                //表示できるかチェック
                var HeightCHK = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - frmcfg.Top;
                if (HeightCHK > 10)
                {
                    this.Top = frmcfg.Top;
                }
                this.Width = frmcfg.Width;
                this.Height = frmcfg.Height;
            }

            // 権限設定を呼び出す(ucfgを取得した後のに入れる)
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            // 登録ボタン設定
            if (!権限Get.Authority_Update_Button(ccfg, this.GetType().Name))
            {
                DataUpdateVisible = System.Windows.Visibility.Hidden;
            }

            if (frmcfg.spConfig != null)
            {
                //AppCommon.LoadSpConfig(this.sp請求データ一覧, frmcfg.spConfig);
            }

            #endregion

            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });

            this.MyCompany.Text1 = ccfg.自社コード.ToString();
            this.MyCompany.Text1IsReadOnly = (ccfg.自社販社区分 != 0);
            this.CreateYMDPriod.Text1 = string.Format("{0:yyyy/MM/dd}", DateTime.Now);
            this.CreateYMDPriod.Text2 = string.Format("{0:yyyy/MM/dd}", DateTime.Now);

            ScreenClear();

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

                var data = message.GetResultData();
                DataTable tbl = (data is DataTable) ? (data as DataTable) : null;

                switch (message.GetMessageName())
                {
                    case BILLING_AGGREGATE:
                        MessageBoxResult result =
                            MessageBox.Show(
                                "集計が終了しました。\n\r終了しても宜しいでしょうか?",
                                "確認",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                            this.Close();
                        break;

                }

            }
            catch (Exception ex)
            {
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
            this.ErrorMessage = (string)message.GetResultData();
        }

        #endregion

        #region << リボン >>

        #region F1 マスタ検索
        /// <summary>
        /// F1 マスタ検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            try
            {
                var ctl = FocusManager.GetFocusedElement(this);
                var uctext = ViewBaseCommon.FindVisualParent<M01_TOK_TextBox>(ctl as UIElement);

                if (uctext != null)
                {
                    uctext.OpenSearchWindow(this);

                }
                else
                {
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                }

            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }

        }
        #endregion

        #region F9 集計開始
        /// <summary>
        /// F9　リボン　集計開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {

            if (this.CheckAllValidation() != true)
            {
                MessageBox.Show("入力エラーがあります。");
                return;
            }

            // 業務バリデーションチェック
            if (!isFormValid())
                return;

            if (MessageBox.Show("都度請求締集計処理を実行しますか？",
                    "集計実行確認", MessageBoxButton.YesNo, MessageBoxImage.Question,
                    MessageBoxResult.Yes) == MessageBoxResult.No)
                return;

            try
            {
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        BILLING_AGGREGATE,
                        getParams(),
                        ccfg.ユーザID));

                base.SetBusyForInput();

            }
            catch (Exception)
            {
                base.SetFreeForInput();
                return;
            }

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

        private bool isFormValid()
        {
            int p会社コード;
            if (!int.TryParse(MyCompany.Text1, out p会社コード))
            {
                ErrorMessage = "自社コードが設定されていません。";
                MessageBox.Show("入力エラーがあります。");
                return false;

            }

            DateTime p作成日開始, p作成日終了;

            if (!DateTime.TryParse(CreateYMDPriod.Text1, out p作成日開始))
            {
                ErrorMessage = "集計対象日(開始)の内容が正しくありません。";
                MessageBox.Show("入力エラーがあります。");
                return false;

            }
            else if (!DateTime.TryParse(CreateYMDPriod.Text2, out p作成日終了))
            {
                ErrorMessage = "集計対象日(終了)の内容が正しくありません。";
                MessageBox.Show("入力エラーがあります。");
                return false;

            }

            return true;

        }

        private Dictionary<string, string> getParams()
        {
            paramDic.Clear();

            // パラメータセット
            paramDic.Add(PARAMS_NAME_COMPANY, MyCompany.Text1);
            paramDic.Add(PARAMS_NAME_CREATE_DATE_START, CreateYMDPriod.Text1);
            paramDic.Add(PARAMS_NAME_CREATE_DATE_END, CreateYMDPriod.Text2);

            return paramDic;

        }

    }

}
