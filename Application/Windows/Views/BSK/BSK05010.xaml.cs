using GrapeCity.Windows.SpreadGrid;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;


namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 年次販社売上調整 フォームクラス
    /// </summary>
    public partial class BSK05010 : WindowReportBase
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
        public class ConfigBSK05010 : FormConfigBase
        {
            public byte[] spConfig = null;
        }

        /// ※ 必ず public で定義する。
        public ConfigBSK05010 frmcfg = null;
        CommonConfig ccfg = null;
        // SPREAD初期状態保存用
        public byte[] spConfig = null;

        #endregion

        #region 一覧データテーブル定義

        private DataTable _searchList;
        public DataTable SearchList
        {
            get { return _searchList; }
            set
            {
                _searchList = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region << 列挙型定義 >>

        /// <summary>
        /// 画面計算状態
        /// </summary>
        private enum SearchMode : int
        {
            初期表示 = 0,
            見込計算 = 1,
            調整計算 = 2,
            調整確定 = 3
        }

        #endregion

        #region << 定数定義 >>

        private const string GET_LIST_SEARCH = "BSK05010_GetDataList";
        private const string SET_CALCULATE = "BSK05010_SetCalculate";
        private const string SET_CONFIRM = "BSK05010_SetConfirm";

        /// <summary>初期決算月</summary>
        private const int DEFAULT_SETTLEMENT_MONTH = 3;

        
        #endregion

        #region << クラス変数定義 >>

        /// <summary>検索時パラメータ保持用</summary>
        Dictionary<string, string> paramDic = new Dictionary<string, string>();

        /// <summary>計算状態</summary>
        private SearchMode _searchMode = SearchMode.初期表示;

        #endregion


        #region << 画面初期処理 >>

        /// <summary>
        /// 請求書発行
        /// </summary>
        public BSK05010()
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
            frmcfg = (ConfigBSK05010)ucfg.GetConfigValue(typeof(ConfigBSK05010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigBSK05010();
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

            ScreenClear();

            SetFocusToTopControl();

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
                    case GET_LIST_SEARCH:
                    case SET_CALCULATE:
                        if (tbl == null)
                        {
                            this.sgSearchResult.ItemsSource = null;
                            this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
                            return;
                        }
                        else
                        {
                            if (tbl.Rows.Count > 0)
                            {
                                SearchList = tbl;
                                sgSearchResult.Visibility = System.Windows.Visibility.Visible;

                                if (message.GetMessageName() == GET_LIST_SEARCH)
                                {
                                    AjustRatio.Focus();
                                    _searchMode = SearchMode.見込計算;

                                }
                                else if (message.GetMessageName() == SET_CALCULATE)
                                {
                                    MessageBox.Show("調整比率計算処理が完了しました。\r\n計算結果を確定する場合は調整確定をおこなってください。",
                                        "調整計算終了",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);

                                    Confirm.Visibility = System.Windows.Visibility.Visible;
                                    _searchMode = SearchMode.調整計算;

                                }

                            }
                            else
                            {
                                SearchList = null;
                                this.ErrorMessage = "指定された条件の請求データはありません。";
                            }

                        }
                        break;

                    case SET_CONFIRM:
                        if ( MessageBox.Show(
                                "集計が終了しました。\r\n終了しても宜しいでしょうか?",
                                "確認",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question) == MessageBoxResult.Yes)
                            this.Close();
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
            this.ErrorMessage = (string)message.GetResultData();
        }

        #endregion

        #region << リボン >>

        #region F01 マスタ検索
        /// <summary>
        /// F1　リボン　マスタ検索
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

        #region F3 計算
        /// <summary>
        /// F3　リボン　計算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF3Key(object sender, KeyEventArgs e)
        {
            if (_searchMode < SearchMode.見込計算)
            {
                MessageBox.Show("見込計算後に実行してください。");
                return;
            }

            if (MessageBox.Show("調整比率計算処理を実行しますか？", "調整計算実行確認",
                MessageBoxButton.YesNo, MessageBoxImage.Question,
                MessageBoxResult.Yes) == MessageBoxResult.No)
                return;

            try
            {
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        SET_CALCULATE,
                        new object[] {
                            paramDic,
                            ccfg.ユーザID
                        }));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "計算エラー", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }
        #endregion

        #region F5 請求書
        /// <summary>
        /// F5　リボン　請求書
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            TKS01020 tksForm = new TKS01020();
            tksForm.ShowDialog(this);

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

            // 自社の入力値クリア
            this.MyCompany.Text1 = string.Empty;

            // 処理年度の初期値設定
            int fiscalYear = getFiscalYear(DateTime.Now.Year, DateTime.Now.Month, DEFAULT_SETTLEMENT_MONTH);
            this.FiscalYear.Text = fiscalYear.ToString();

            // 調整割合の入力値クリア
            this.AjustRatio.Text = string.Empty;

            // コントロール非表示
            Confirm.Visibility = System.Windows.Visibility.Hidden;
            sgSearchResult.Visibility = System.Windows.Visibility.Hidden;

            ResetAllValidation();
            SetFocusToTopControl();

        }
        #endregion

        #region 決算年度算出
        /// <summary>
        /// 決算年度を算出して返す
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="settlementMonth">決算月</param>
        /// <returns></returns>
        private int getFiscalYear(int year, int month, int settlementMonth)
        {
            int fiscalYear = year;
            // 決算月以前の場合は前年を年度として指定
            if (month <= settlementMonth)
                fiscalYear -= 1;

            return fiscalYear;

        }
        #endregion

        #region 業務入力チェック
        /// <summary>
        /// 業務入力チェックをおこなう
        /// </summary>
        /// <returns></returns>
        private bool formValidation()
        {
            if (string.IsNullOrEmpty(FiscalYear.Text))
            {
                FiscalYear.Focus();
                ErrorMessage = "処理年度が入力されていません。";
                return false;
            }

            if (string.IsNullOrEmpty(MyCompany.Text1))
            {
                MyCompany.Focus();
                ErrorMessage = "対象販社が設定されていません。";
                return false;
            }

            if (string.IsNullOrEmpty(AjustRatio.Text))
                // 未入力時はゼロを自動設定する
                AjustRatio.Text = "0";

            return true;

        }
        #endregion

        #region パラメータ設定
        /// <summary>
        /// パラメータを設定する
        /// </summary>
        private void setSearchParams()
        {
            paramDic.Clear();

            paramDic.Add("処理年度", FiscalYear.Text);
            paramDic.Add("対象販社", MyCompany.Text1);
            paramDic.Add("調整比率", AjustRatio.Text);

        }
        #endregion


        #endregion

        #region << コントロールイベント群 >>

        #region 見込計算ボタン押下
        /// <summary>
        /// 見込計算ボタン押下時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubCalc_Click(object sender, RoutedEventArgs e)
        {
            if (this.CheckAllValidation() != true)
            {
                MessageBox.Show("入力エラーがあります。");
                return;
            }

            if (!formValidation())
            {
                MessageBox.Show(ErrorMessage, "入力エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // パラメータを設定
            setSearchParams();

            try
            {
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        GET_LIST_SEARCH,
                        new object[] {
                            paramDic
                        }));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "見込計算エラー", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }
        #endregion

        #region 調整確定ボタン押下
        /// <summary>
        /// 調整確定ボタン押下時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (_searchMode < SearchMode.調整計算)
            {
                MessageBox.Show("調整計算(F3)後に実施してください。");
                return;
            }

            #region 入力検証

            if (sgSearchResult == null || sgSearchResult.Rows.Count == 0)
            {
                this.ErrorMessage = "対象データが存在しない為、確定処理ができません。";
                return;
            }

            #endregion

            if (MessageBox.Show("調整比率の確定計算処理を実行しますか？", "確定実行確認",
                    MessageBoxButton.YesNo, MessageBoxImage.Question,
                    MessageBoxResult.Yes) == MessageBoxResult.No)
                return;

            try
            {
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        SET_CONFIRM,
                        new object[] {
                            paramDic,
                            ccfg.ユーザID
                        }));

                base.SetBusyForInput();

            }
            catch (Exception)
            {
                base.SetFreeForInput();
                return;
            }

        }
        #endregion


        #region Window_Closed
        /// <summary>
        /// 画面が閉じられた時、データを保持する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            //this.請求書一覧データ = null;
            SearchList = null;
            sgSearchResult.InputBindings.Clear();

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            frmcfg.spConfig = AppCommon.SaveSpConfig(this.sgSearchResult);
            ucfg.SetConfigValue(frmcfg);

        }
        #endregion

        #endregion

    }

}
