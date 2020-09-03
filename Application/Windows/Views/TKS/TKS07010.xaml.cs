﻿using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;


namespace KyoeiSystem.Application.Windows.Views
{
    using KyoeiSystem.Framework.Windows.Controls;
    using FwReportPreview = KyoeiSystem.Framework.Reports.Preview.ReportPreview;
    using FwPreview = KyoeiSystem.Framework.Reports.Preview;
    using WinForms = System.Windows.Forms;

    /// <summary>
    /// 入金予定表画面クラス
    /// </summary>
    public partial class TKS07010 : WindowReportBase
    {
        #region << 列挙型定義 >>

        /// <summary>
        /// 自社販社区分 内包データ
        /// </summary>
        private enum 自社販社区分 : int
        {
            自社 = 0,
            販社 = 1
        }


        #endregion

        #region 定数定義

        /// <summary>CSVファイル出力データ取得</summary>
        private const string SEARCH_TKS07010_CSV = "TKS07010_GetCsvData";

        /// <summary>帳票印刷データ取得</summary>
        private const string SEARCH_TKS07010_PRT = "TKS07010_GetPrintData";

        /// <summary>帳票定義ファイル 格納パス</summary>
        private const string ReportTemplateFileName = @"Files\TKS\TKS07010.rpt";
        private const string ReportFileName = @"Files\TKS\TKS07010_3.rpt";

        #endregion

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;
        CommonConfig ccfg = null;

        /// <summary>
        /// 画面固有設定項目のクラス定義
        /// ※ 必ず public で定義する。
        /// </summary>
        public class ConfigTKS07010 : FormConfigBase
        {

        }
        /// ※ 必ず public で定義する。
        public ConfigTKS07010 frmcfg = null;

        #endregion

        #region バインド用プロパティ

        #endregion

        #region << 初期表示処理 >>

        /// <summary>
        /// 入金予定表 コンストラクタ
        /// </summary>
        public TKS07010()
        {
            InitializeComponent();
            this.DataContext = this;

        }

        /// <summary>
        /// 画面読み込み後のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigTKS07010)ucfg.GetConfigValue(typeof(ConfigTKS07010));
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));

            if (frmcfg == null)
            {
                frmcfg = new ConfigTKS07010();
                ucfg.SetConfigValue(frmcfg);

            }
            else
            {
                // 表示できるかチェック
                var WidthCHK = WinForms.Screen.PrimaryScreen.Bounds.Width - frmcfg.Left;
                if (WidthCHK > 10)
                    this.Left = frmcfg.Left;

                // 表示できるかチェック
                var HeightCHK = WinForms.Screen.PrimaryScreen.Bounds.Height - frmcfg.Top;
                if (HeightCHK > 10)
                    this.Top = frmcfg.Top;

                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;

            }
            #endregion

            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { null, typeof(SCHM01_TOK) });
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { null, typeof(SCHM70_JIS) });

            // コンボデータ取得
            AppCommon.SetutpComboboxList(this.CreateType);

            // 初期値設定
            myCompany.Text1 = ccfg.自社コード.ToString();
            myCompany.IsEnabled = ccfg.自社販社区分 == 自社販社区分.自社.GetHashCode();

            PaymentYearMonth.Text = DateTime.Now.ToString("yyyy/MM");

			SetFocusToTopControl();
            ErrorMessage = string.Empty;

        }

        #endregion

        #region データ受信メソッド
        /// <summary>
        /// データ受信メソッド
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            try
            {
                var data = message.GetResultData();

                if (data is DataTable)
                {
                    DataTable tbl = data as DataTable;

                    switch (message.GetMessageName())
                    {
                        case SEARCH_TKS07010_PRT:
                            // 検索結果取得時
                            DispPreviw(tbl);
                            break;

                        case SEARCH_TKS07010_CSV:
                            OutPutCSV(tbl);
                            break;

                    }

                }

            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }

        }

        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            this.ErrorMessage = (string)message.GetResultData();
        }

        #endregion

        #region リボン

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
                var m01Text = ViewBaseCommon.FindVisualParent<M01_TOK_TextBox>(ctl as UIElement);

                if (m01Text == null)
                {
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                }
                else
                {
                    m01Text.OpenSearchWindow(this);

                }

            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }

        }


        /// <summary>
        /// F5　リボン　CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }

            if (!CheckFormValid())
            {
                return;
            }

            Dictionary<string, string> paramDic = createParamDic();

            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestDataWithBusy,
                    SEARCH_TKS07010_CSV,
                    new object[] {
                        paramDic
                    }));

        }
       
        /// <summary>
        /// F8　リボン　印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
		{
			PrinterDriver ret = AppCommon.GetPrinter(frmcfg.PrinterName);
			if (ret.Result == false)
			{
				this.ErrorMessage = "プリンタドライバーがインストールされていません！";
				return;
			}
			frmcfg.PrinterName = ret.PrinterName;

            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }

            if (!CheckFormValid())
            {
                return;
            }

            Dictionary<string, string> paramDic = createParamDic();

            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestDataWithBusy,
                    SEARCH_TKS07010_PRT,
                    new object[] {
                        paramDic
                    }));

        }

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
                    this.ErrorMessage = "対象データが存在しません。";
                    return;
                }

                // 印刷処理
                FwReportPreview view = new FwReportPreview();

                // 印字用にパラメータを編集
                int yearMonth = int.Parse(PaymentYearMonth.Text.Replace("/", ""));
                int year = yearMonth / 100;
                int month = yearMonth % 100;
                string closingText = isPaymentAllDays.IsChecked == true ?
                    isPaymentAllDays.Content.ToString() : string.Format("{0}日入金分", PaymentDay.Text);

                var parms = new List<FwPreview.ReportParameter>()
                {
                    new FwPreview.ReportParameter(){ PNAME="自社名", VALUE=(this.myCompany.Text2)},
                    new FwPreview.ReportParameter(){ PNAME="入金年", VALUE=(year)},
                    new FwPreview.ReportParameter(){ PNAME="入金月", VALUE=(month)},
                    new FwPreview.ReportParameter(){ PNAME="入金日", VALUE=(closingText)},
                    new FwPreview.ReportParameter(){ PNAME="作成区分", VALUE=(this.CreateType.Text)},
                    new FwPreview.ReportParameter(){ PNAME="得意先名", VALUE=(this.Customer.Label2Text)},
                };

                // 第1引数　帳票タイトル
                // 第2引数　帳票ファイルPass
                // 第3以上　帳票の開始点(0で良い)
                if (CreateType.SelectedValue.ToString() != "3")
                {
                    view.MakeReport("入金予定表", ReportTemplateFileName, 0, 0, 0);
                }
                else
                {
                    view.MakeReport("入金予定表", ReportFileName, 0, 0, 0);
                }
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

        #region CSVファイル出力
        /// <summary>
        /// CSVファイル出力
        /// </summary>
        /// <param name="tbl"></param>
        private void OutPutCSV(DataTable tbl)
        {
            if (tbl.Rows.Count < 1)
            {
                this.ErrorMessage = "対象データが存在しません。";
                return;
            }

            WinForms.SaveFileDialog sfd = new WinForms.SaveFileDialog();
            // はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = @"C:\";
            // [ファイルの種類]に表示される選択肢を指定する
            sfd.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
            // 「CSVファイル」が選択されているようにする
            sfd.FilterIndex = 1;
            // タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください";
            // ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == WinForms.DialogResult.OK)
            {
                // CSVファイル出力
                CSVData.SaveCSV(tbl, sfd.FileName, true, true, false, ',');
                MessageBox.Show("CSVファイルの出力が完了しました。");
            }

        }
        #endregion

        #region Mindoow_Closed
        /// <summary>
        /// 画面が閉じられた時、データを保持する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;

            ucfg.SetConfigValue(frmcfg);

        }
        #endregion

        /// <summary>
        /// 作成締日のテキストが変更された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosingDate_TextChanged(object sender, RoutedEventArgs e)
        {
            UcLabelTextBox tb = sender as UcLabelTextBox;

            // 作成締日が空値の場合に自動で全締日をチェック
            // 値が設定された場合は自動でチェックオフにする
            isPaymentAllDays.IsChecked = string.IsNullOrWhiteSpace(tb.Text);

        }

        /// <summary>
        /// 業務バリデーションチェックをおこなう
        /// </summary>
        /// <returns></returns>
        private bool CheckFormValid()
        {
            // 自社コードの必須入力チェック
            if (string.IsNullOrEmpty(myCompany.Text1))
            {
                myCompany.SetFocus();
                ErrorMessage = "自社コードが入力されていません。";
                return false;
            }

            // 入金年月の必須入力チェック
            if (string.IsNullOrEmpty(PaymentYearMonth.Text))
            {
                PaymentYearMonth.Focus();
                ErrorMessage = "入金年月が入力されていません。";
                return false;
            }

            if (isPaymentAllDays.IsChecked == true)
            {
                // 全入金日の状態チェック(作成入金日入力かつチェックオンはエラー)
                if (!string.IsNullOrEmpty(PaymentDay.Text))
                {
                    isPaymentAllDays.Focus();
                    ErrorMessage = "全入金締日が設定されている場合はチェックをオフにしてください。";
                    return false;
                }

            }
            else
            {
                // 作成入金日の入力チェック(全入金日チェック無しの場合)
                if (string.IsNullOrEmpty(PaymentDay.Text))
                {
                    PaymentDay.Focus();
                    ErrorMessage = "入金締日が入力されていません。";
                    return false;
                }

            }

            return true;

        }

        /// <summary>
        /// 検索条件を作成して返す
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> createParamDic()
        {
            Dictionary<string, string> paramDic = new Dictionary<string, string>();

            paramDic.Add("自社コード", myCompany.Text1);
            paramDic.Add("入金年月", PaymentYearMonth.Text);
            paramDic.Add("入金日", PaymentDay.Text);
            paramDic.Add("全入金日", isPaymentAllDays.IsChecked.ToString());
            paramDic.Add("得意先コード", Customer.Text1 == null ? string.Empty : Customer.Text1);
            paramDic.Add("得意先枝番", Customer.Text2 == null ? string.Empty : Customer.Text2);
            paramDic.Add("作成区分", CreateType.SelectedValue.ToString());

            return paramDic;

        }

        /// <summary>
        /// 全入金締日がチェックされた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void isPaymentAllDays_Checked(object sender, RoutedEventArgs e)
        {
            // 全入金締日がチェックされた時、締日入力値を初期化する
            this.PaymentDay.Text = string.Empty;
        }

        #region Enterキー(F8)

        //private void F8(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //    {
        //        var yesno = MessageBox.Show("プレビューを表示しますか？", "確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
        //        if (yesno == MessageBoxResult.Yes)
        //            OnF8Key(sender, null);

        //    }

        //}

        #endregion

    }

}
