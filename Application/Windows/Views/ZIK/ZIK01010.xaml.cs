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
    using FwReportPreview = KyoeiSystem.Framework.Reports.Preview.ReportPreview;
    using FwPreview = KyoeiSystem.Framework.Reports.Preview;

    /// <summary>
    /// 月次在庫集計 フォームクラス
    /// </summary>
    public partial class ZIK01010 : WindowReportBase
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
        public class ConfigZIK01010 : FormConfigBase
        {
            public byte[] spConfig = null;
        }

        /// ※ 必ず public で定義する。
        public ConfigZIK01010 frmcfg = null;
        CommonConfig ccfg = null;
        // SPREAD初期状態保存用
        public byte[] spConfig = null;

        #endregion

        #region << 定数定義 >>

        /// <summary>通信キー　対象月の締集計が実施済かどうか</summary>
        private const string CHECK_SUMMARY = "ZIK01010_IsCheckSummary";
        /// <summary>通信キー　締集計処理の実行</summary>
        private const string SUMMARY = "ZIK01010_InventorySummary";
        /// <summary>通信キー　帳票出力データの取得</summary>
        private const string GET_PRINT_DATA = "ZIK01010_GetPrintData";

        /// <summary>帳票定義ファイル 格納パス</summary>
        private const string ReportTemplateFileName = @"Files\ZIK\ZIK01010.rpt";

        #endregion


        #region << 画面初期処理 >>

        /// <summary>
        /// 請求書発行
        /// </summary>
        public ZIK01010()
        {
            InitializeComponent();
            this.DataContext = this;

        }

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigZIK01010)ucfg.GetConfigValue(typeof(ConfigZIK01010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigZIK01010();
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

            // システム日付の前月を初期値とする
            this.CreateYearMonth.Text = string.Format("{0:yyyy/MM}", DateTime.Now.AddMonths(-1));

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
                    case CHECK_SUMMARY:
                        if (data is bool)
                        {
                            if (MessageBox.Show("集計済のデータが存在します。\n\r再集計をおこないますか？",
                                    "集計実行確認",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Asterisk,
                                    MessageBoxResult.Yes) == MessageBoxResult.No)
                                return;
                        }
                        else
                        {
                            if (MessageBox.Show("月次在庫締集計処理を実行しますか？",
                                    "集計実行確認", MessageBoxButton.YesNo, MessageBoxImage.Question,
                                    MessageBoxResult.Yes) == MessageBoxResult.No)
                                return;

                        }
                        // 集計処理を実行
                        runSummaryProc();
                        
                        break;

                    case SUMMARY:
                        MessageBoxResult result =
                            MessageBox.Show(
                                "在庫締集計処理が終了しました。",
                                "確認",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                        break;

                    case GET_PRINT_DATA:
                        base.SetFreeForInput();

                        // 帳票作成
                        DispPreviw(tbl);
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

        #region F8 印刷
        /// <summary>
        /// F8　リボン　印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            // 印刷対象データ取得
            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    GET_PRINT_DATA,
                    CreateYearMonth.Text));

            base.SetBusyForInput();

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
            // 入力検証
            if (this.CheckAllValidation() != true)
            {
                MessageBox.Show("入力エラーがあります。");
                return;
            }

            DateTime d集計年月;
            if (!DateTime.TryParse(CreateYearMonth.Text, out d集計年月))
            {
                ErrorMessage = "集計年月の内容が正しくありません。";
                MessageBox.Show("入力エラーがあります。");
                return;
            }

            // 集計年月のデータが作成済みかどうか
            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    CHECK_SUMMARY,
                    CreateYearMonth.Text));

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

        #region 集計処理実行部
        /// <summary>
        /// 集計処理の実行部
        /// </summary>
        private void runSummaryProc()
        {
            // パラメータ生成
            Dictionary<string, string> paramDic = new Dictionary<string, string>();
            paramDic.Add("集計年月", CreateYearMonth.Text);

            try
            {
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.UpdateData,
                        SUMMARY,
                        paramDic,
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

        #region プレビュー画面
        /// <summary>
        /// プレビュー画面表示
        /// </summary>
        /// <param name="tbl"></param>
        private void DispPreviw(DataTable tbl)
        {
            try
            {
                if (tbl.Rows.Count < 1)
                {
                    this.ErrorMessage = "集計年月の在庫締集計をおこなってください。";
                    return;
                }

                // 印刷処理
                FwReportPreview view = new FwReportPreview();

                // 印字用にパラメータを編集
                int yearMonth = int.Parse(CreateYearMonth.Text.Replace("/", ""));
                int year = yearMonth / 100;
                int month = yearMonth % 100;
                DateTime date = new DateTime(year, month, DateTime.DaysInMonth(year, month));

                var parms = new List<FwPreview.ReportParameter>()
                {
                    new FwPreview.ReportParameter(){ PNAME="集計年月時点", VALUE=(date.ToString("yyyy/M/d "))}
                };

                // 第1引数　帳票タイトル
                // 第2引数　帳票ファイルPass
                // 第3以上　帳票の開始点(0で良い)
                view.MakeReport("在庫締集計表", ReportTemplateFileName, 0, 0, 0);
                // 帳票ファイルに送るデータ。
                // 帳票データの列と同じ列名を保持したDataTableを引数とする
                view.SetReportData(tbl);
                view.PrinterName = frmcfg.PrinterName;
                view.SetupParmeters(parms);
                view.ShowPreview();
                view.Close();
                frmcfg.PrinterName = view.PrinterName;

                // 印刷した場合
                if (view.IsPrinted)
                {
                    // 印刷した場合はtrueを返す
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

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
