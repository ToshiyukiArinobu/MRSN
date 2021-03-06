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
    /// 支払明細表画面クラス
    /// </summary>
    public partial class SHR05020 : WindowReportBase
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
        private const string SEARCH_SHR05020_CSV = "SHR05020_GetCsvData";

        /// <summary>帳票印刷データ取得</summary>
        private const string SEARCH_SHR05020_PRT = "SHR05020_GetPrintData";

        /// <summary>帳票印刷データ取得</summary>
        private const string SEARCH_SHR05020_PRT_DETAIL = "SHR05020_GetPrintDetail";


        /// <summary>帳票定義ファイル 格納パス</summary>
        // 簡易
        private const string ReportSimpleFileName = @"Files\SHR\SHR05020.rpt";
        // 詳細
        private const string ReportDetailFileName = @"Files\SHR\SHR05021.rpt";
        private const string ReportDetailFileName_1 = @"Files\SHR\SHR05021_1.rpt";

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
        public class ConfigSHR05020 : FormConfigBase
        {

        }
        /// ※ 必ず public で定義する。
        public ConfigSHR05020 frmcfg = null;

        #endregion

        #region バインド用プロパティ

        // [0]:取引区分、[1]:マルセン追加フラグ
        private string[] _得意先リンクアイテム = new string[] { "1,2,3,4", "1" };
        public string[] 得意先リンクアイテム
        {
            get { return _得意先リンクアイテム; }
            set { _得意先リンクアイテム = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region << 初期表示処理 >>

        /// <summary>
        /// 支払明細表 コンストラクタ
        /// </summary>
        public SHR05020()
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
            frmcfg = (ConfigSHR05020)ucfg.GetConfigValue(typeof(ConfigSHR05020));
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));

            if (frmcfg == null)
            {
                frmcfg = new ConfigSHR05020();
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
            //AppCommon.SetutpComboboxList(this.CreateType);

            // 初期値設定
            myCompany.Text1 = ccfg.自社コード.ToString();
            myCompany.IsEnabled = ccfg.自社販社区分 == 自社販社区分.自社.GetHashCode();
            this.PrintDate.Text = DateTime.Now.ToString("yyyy/MM/dd");

            CreateYearMonth.Text = DateTime.Now.ToString("yyyy/MM");

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

                DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
                var ds = data as DataSet;

                switch (message.GetMessageName())
                {
                    case SEARCH_SHR05020_PRT:
                        // 検索結果取得時
                        DispPreviw(tbl);
                        break;

                    case SEARCH_SHR05020_PRT_DETAIL:

                        if (ds.Tables.Count < 1)
                        {
                            base.ErrorMessage = "対象データが存在しません。";
                            return;
                        }
                        else
                        {
                            // 詳細データがない場合、メッセージ表示
                            if ((ds.Tables["SHR05021_D支払明細書"].Rows.Count) <= 0)
                            {
                                base.ErrorMessage = "対象データが存在しません。";
                                return;
                            }
                        }

                        PrintPreview(ds);
                        break;

                    case SEARCH_SHR05020_CSV:
                        OutPutCSV(tbl);
                        break;

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
                    SEARCH_SHR05020_CSV,
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


            switch (this.rdo出力帳票.Text)
            {
                case "0":
                    //簡易帳票
                    base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SHR05020_PRT, new object[] { paramDic }));
                    break;
                case "1":
                    //詳細帳票
                    base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SHR05020_PRT_DETAIL, new object[] { paramDic }));
                    break;
                default:
                    return;
            }
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
        /// プレビュー画面表示(TBL)
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
                int yearMonth = int.Parse(CreateYearMonth.Text.Replace("/", ""));
                int year = yearMonth / 100;
                int month = yearMonth % 100;
                string closingText = isClosingAllDays.IsChecked == true ?
                    isClosingAllDays.Content.ToString() : string.Format("{0}日締", ClosingDate.Text);

                var parms = new List<FwPreview.ReportParameter>()
                {
                    new FwPreview.ReportParameter(){ PNAME="自社名", VALUE=(this.myCompany.Text2)},
                    new FwPreview.ReportParameter(){ PNAME="支払年", VALUE=(year)},
                    new FwPreview.ReportParameter(){ PNAME="支払月", VALUE=(month)},
                    new FwPreview.ReportParameter(){ PNAME="作成締日", VALUE=(closingText)},
                    new FwPreview.ReportParameter(){ PNAME="支払先名", VALUE=(this.Customer.Label2Text)},
                };

                // 第1引数　帳票タイトル
                // 第2引数　帳票ファイルPass
                // 第3以上　帳票の開始点(0で良い)
                view.MakeReport("支払明細表", ReportSimpleFileName, 0, 0, 0);          // No-148 Mod
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


        /// <summary>
        /// 帳票出力処理(DS)
        /// </summary>
        /// <param name="ds"></param>
        private void PrintPreview(DataSet ds)
        {
            // 印刷処理
            FwPreview.ReportPreview view = new FwPreview.ReportPreview();
            view.PrinterName = frmcfg.PrinterName;
            


            switch (this.rdo詳細.Text)
            {
                case "0":
                    //前回支払額無し
                    // 第1引数　帳票タイトル
                    // 第2引数　帳票ファイルPass
                    // 第3以上　帳票の開始点(0で良い)
                    view.MakeReport("支払明細書", ReportDetailFileName, 0, 0, 0);
                    break;
                case "1":
                    // 第1引数　帳票タイトル
                    // 第2引数　帳票ファイルPass
                    // 第3以上　帳票の開始点(0で良い)
                    view.MakeReport("支払明細書", ReportDetailFileName_1, 0, 0, 0);
                    break;
                default:
                    //前回支払額無し
                    // 第1引数　帳票タイトル
                    // 第2引数　帳票ファイルPass
                    // 第3以上　帳票の開始点(0で良い)
                    view.MakeReport("支払明細書", ReportDetailFileName, 0, 0, 0);
                    break;
            }

            var parms = new List<FwPreview.ReportParameter>()
                {
                    //new FwPreview.ReportParameter(){ PNAME="選択項目", VALUE=str選択項目},
                    //new FwPreview.ReportParameter(){ PNAME="行数１", VALUE=(MAX_PRINT_ROW_COUNT)},// ページあたり行数
                    //new FwPreview.ReportParameter(){ PNAME="最大行数", VALUE=(MAX_PRINT_ROW_COUNT)},// ページあたり行数
                    //new FwPreview.ReportParameter(){ PNAME="行数２", VALUE=(ds.Tables[0].Rows.Count)},
                };

            // 帳票ファイルに送るデータ。
            // 帳票データの列と同じ列名を保持したDataSetを引数とする
            view.SetReportData(ds);
            view.PrinterName = frmcfg.PrinterName;
            view.SetupParmeters(parms);
            view.ShowPreview();
            view.Close();
            frmcfg.PrinterName = view.PrinterName;

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
            isClosingAllDays.IsChecked = string.IsNullOrWhiteSpace(tb.Text);

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

            // 作成年月の必須入力チェック
            if (string.IsNullOrEmpty(CreateYearMonth.Text))
            {
                CreateYearMonth.Focus();
                ErrorMessage = "作成年月が入力されていません。";
                return false;
            }

            if (isClosingAllDays.IsChecked == true)
            {
                // 全締日の状態チェック(作成締日入力かつチェックオンはエラー)
                if (!string.IsNullOrEmpty(ClosingDate.Text))
                {
                    isClosingAllDays.Focus();
                    ErrorMessage = "作成締日が設定されている場合はチェックをオフにしてください。";
                    return false;
                }

            }
            else
            {
                // 作成締日の入力チェック(全締日チェック無しの場合)
                if (string.IsNullOrEmpty(ClosingDate.Text))
                {
                    ClosingDate.Focus();
                    ErrorMessage = "作成締日が入力されていません。";
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
            paramDic.Add("作成年月日", this.PrintDate.Text);
            paramDic.Add("作成年月", CreateYearMonth.Text);
            paramDic.Add("作成締日", ClosingDate.Text);
            paramDic.Add("全締日", isClosingAllDays.IsChecked.ToString());
            paramDic.Add("得意先コード", Customer.Text1 == null ? string.Empty : Customer.Text1);
            paramDic.Add("得意先枝番", Customer.Text2 == null ? string.Empty : Customer.Text2);

            return paramDic;

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
