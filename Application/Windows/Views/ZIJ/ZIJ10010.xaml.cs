using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace KyoeiSystem.Application.Windows.Views
{
    using FwRepPreview = KyoeiSystem.Framework.Reports.Preview;
    using WinForms = System.Windows.Forms;

    /// <summary>
    /// 揚り明細問合せ フォームクラス
    /// </summary>
    public partial class ZIJ10010 : RibbonWindowViewBase
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

        public enum 加工区分 : int
        {
            外注加工 = 1,
            内職加工 = 2,
            社内加工 = 3,
        }

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
        public class ConfigZIJ10010 : FormConfigBase
        {
            public byte[] spConfig20180118 = null;

        }

        /// ※ 必ず public で定義する。
        public ConfigZIJ10010 frmcfg = null;

        public byte[] sp_Config = null;

        #endregion

        #region << 定数定義 >>

        private const string ZIJ10010_GetDataList = "ZIJ10010_GetData";
        private const string ReportFileName = @"Files\ZIJ\ZIJ10010.rpt";

        // 検索項目名定数定義
        private const string FORM_PARAMS_JIS_CODE = "自社コード";
        private const string FORM_PARAMS_JIS_NAME = "自社名";
        private const string FORM_PARAMS_SALES_DATE_FROM = "仕上日From";
        private const string FORM_PARAMS_SALES_DATE_TO = "仕上日To";
        private const string FORM_PARAMS_SALES_KBN_CODE = "加工区分";
        private const string FORM_PARAMS_SALES_KBN_NAME = "加工区分名";
        private const string FORM_PARAMS_TOK_CODE = "加工先コード";
        private const string FORM_PARAMS_TOK_EDA = "加工先枝番";
        private const string FORM_PARAMS_TOK_NAME = "外注先名";

        #endregion

        #region << 変数定義 >>

        private Dictionary<string, string> paramDic = new Dictionary<string, string>();

        #endregion

        #region バインディングプロパティ

        private DataTable _searchResult;
        public DataTable SearchResult
        {
            get { return _searchResult; }
            set
            {
                _searchResult = value;
                NotifyPropertyChanged();
            }

        }

        #endregion

        #region<< 揚り明細問合せ 初期処理群 >>

        /// <summary>
        /// 揚り明細問合せ コンストラクタ
        /// </summary>
        public ZIJ10010()
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
            // 初期状態を保存（SPREADリセット時にのみ使用する）
            this.sp_Config = AppCommon.SaveSpConfig(this.spGridList);

            base.MasterMaintenanceWindowList.Add("M01_TOK_TOKU_SCH", new List<Type> { typeof(MST02010), typeof(SCHM01_TOK) });
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            frmcfg = (ConfigZIJ10010)ucfg.GetConfigValue(typeof(ConfigZIJ10010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigZIJ10010();
                ucfg.SetConfigValue(frmcfg);
                frmcfg.spConfig20180118 = this.sp_Config;
            }
            else
            {
                // 表示できるかチェック
                var WidthCHK = WinForms.Screen.PrimaryScreen.Bounds.Width - frmcfg.Left;
                if (WidthCHK > 10)
                {
                    this.Left = frmcfg.Left;
                }
                // 表示できるかチェック
                var HeightCHK = WinForms.Screen.PrimaryScreen.Bounds.Height - frmcfg.Top;
                if (HeightCHK > 10)
                {
                    this.Top = frmcfg.Top;
                }
                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;
            }
            #endregion

            spGridList.InputBindings.Add(new KeyBinding(spGridList.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));

            // コントロールの初期設定をおこなう
            initSearchControl();

            spGridList.RowCount = 0;

            // 先頭項目にフォーカスを設定
            SetFocusToTopControl();
            base.ErrorMessage = string.Empty;

        }

        #endregion

        #region << 送信データ受信 >>

        /// <summary>
        /// 取得データの取り込み
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            var data = message.GetResultData();
            DataTable tbl = (data is DataTable) ? (data as DataTable) : null;

            switch (message.GetMessageName())
            {
                case ZIJ10010_GetDataList:
                    base.SetFreeForInput();

                    if (tbl == null)
                    {
                        this.SearchResult = null;
                    }
                    else
                    {
                        if (tbl.Rows.Count == 0)
                        {
                            this.ErrorMessage = "該当するデータはありません。";
                            SearchResult = null;
                            return;
                        }
                        else
                        {
                            SearchResult = tbl;
                        }

                    }
                    break;

            }

        }

        /// <summary>
        /// 受信エラー時の処理をおこなう
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            this.Message = base.ErrorMessage;
            base.SetFreeForInput();
        }

        #endregion

        #region << リボン >>

        #region F1 マスタ検索
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
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";

            }

        }
        #endregion

        #region F5 CSV出力
        /// <summary>
        /// F5　リボン　CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            if (this.SearchResult == null || this.SearchResult.Rows.Count == 0)
            {
                MessageBox.Show("検索データがありません。");
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
                CSVData.SaveCSV(SearchResult, sfd.FileName, true, true, false, ',');
                MessageBox.Show("CSVファイルの出力が完了しました。");
            }

        }
        #endregion

        /// <summary>
        /// F8　リボン　印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {

            //MessageBox.Show("帳票作成中");
            //return;

            PrintOut();

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

        #region 検索用ボタン

        /// <summary>
        /// 検索ボタンが押下された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!base.CheckAllValidation())
                {
                    this.ErrorMessage = "入力内容に誤りがあります。";
                    MessageBox.Show("入力内容に誤りがあります。");
                    return;
                }

                int iCompany;
                if (!int.TryParse(this.myCompany.Text1, out iCompany))
                {
                    base.ErrorMessage = "自社コードの入力に誤りがあります。";
                    this.myCompany.Focus();
                    this.myCompany.Focusable = true;
                    return;
                }

                setSearchParams();

                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        ZIJ10010_GetDataList,
                        new object[]
                        {
                            iCompany,
                            paramDic
                        }));

                base.SetBusyForInput();

            }
            catch
            {
                throw;
            }

        }

        #endregion

        #region << KeyDown Events >>

        /// <summary>
        /// コントロールでキーが押下された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastField_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var ctl = sender as Framework.Windows.Controls.UcLabelTwinTextBox;
                if (ctl == null)
                {
                    return;
                }
                e.Handled = true;
                bool chk = ctl.CheckValidation();
                if (chk == true)
                {
                    Keyboard.Focus(this.btnSearch);
                }
                else
                {
                    ctl.Focus();
                    this.ErrorMessage = ctl.GetValidationMessage();
                }
            }
        }

        /// <summary>
        /// スプレッドグリッドでキーが押下された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spGridList_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && spGridList.EditElement == null)
            {
                e.Handled = true;
            }
            else if (e.Key == Key.V && (((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) != KeyStates.Down) || ((Keyboard.GetKeyStates(Key.RightCtrl) & KeyStates.Down) != KeyStates.Down)))
            {
                e.Handled = true;
            }

        }

        #endregion

        #region Window_Closed

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            this.spGridList.InputBindings.Clear();
            this.SearchResult = null;

            if (ucfg != null)
            {
                if (frmcfg == null) { frmcfg = new ConfigZIJ10010(); }
                frmcfg.Top = this.Top;
                frmcfg.Left = this.Left;
                frmcfg.Width = this.Width;
                frmcfg.Height = this.Height;
                frmcfg.spConfig20180118 = AppCommon.SaveSpConfig(this.spGridList);

                ucfg.SetConfigValue(frmcfg);
            }

        }

        #endregion


        #region << 機能処理群 >>



        #region 検索条件部の初期設定

        /// <summary>
        /// 検索条件部の初期設定をおこなう
        /// </summary>
        private void initSearchControl()
        {
            // 自社コード
            this.myCompany.Text1 = ccfg.自社コード.ToString();
            this.myCompany.IsEnabled = ccfg.自社販社区分 == 自社販社区分.自社.GetHashCode();

            // 仕上り日(範囲指定)
            DateTime nowDate = DateTime.Today;
            this.SalesDatePriod.Text1 = String.Format("{0:yyyy/MM}/01", nowDate);
            this.SalesDatePriod.Text2 = String.Format("{0:yyyy/MM}/{1}", nowDate, DateTime.DaysInMonth(nowDate.Year, nowDate.Month));

            // 加工区分
            AppCommon.SetutpComboboxList(this.cmbSalesKbn, false);
            this.cmbSalesKbn.SelectedIndex = 0;

        }

        #endregion

        /// <summary>
        /// 検索パラメータを設定する
        /// </summary>
        private void setSearchParams()
        {
            #region 売上区分の名称辞書を作成
            Dictionary<int, string> salesKbnDic = new Dictionary<int, string>()
                {
                    { 0, "指定なし" },
                    // 売上系
                    { (int)加工区分.外注加工, "外注加工" },
                    { (int)加工区分.内職加工, "内職加工" },
                    { (int)加工区分.社内加工, "社内加工" },
                    
                };
            #endregion

            paramDic.Clear();

            paramDic.Add(FORM_PARAMS_JIS_CODE, myCompany.Text1);
            paramDic.Add(FORM_PARAMS_SALES_DATE_FROM, SalesDatePriod.Text1);
            paramDic.Add(FORM_PARAMS_SALES_DATE_TO, SalesDatePriod.Text2);
            paramDic.Add(FORM_PARAMS_SALES_KBN_CODE, cmbSalesKbn.SelectedValue.ToString());
            paramDic.Add(FORM_PARAMS_TOK_CODE, TOK.Text1);
            paramDic.Add(FORM_PARAMS_TOK_EDA, TOK.Text2);

            paramDic.Add(FORM_PARAMS_JIS_NAME, myCompany.Text2);
            paramDic.Add(FORM_PARAMS_SALES_KBN_NAME, salesKbnDic[int.Parse(cmbSalesKbn.SelectedValue.ToString())]);
            paramDic.Add(FORM_PARAMS_TOK_NAME, TOK.Label2Text);

        }

        /// <summary>
        /// 帳票印刷処理
        /// </summary>
        private void PrintOut()
        {
            PrinterDriver ret = AppCommon.GetPrinter(frmcfg.PrinterName);
            if (ret.Result == false)
            {
                this.ErrorMessage = "プリンタドライバーがインストールされていません！";
                return;
            }
            frmcfg.PrinterName = ret.PrinterName;

            if (this.SearchResult == null || this.SearchResult.Rows.Count == 0)
            {
                this.ErrorMessage = "印刷データがありません。";
                return;
            }

            try
            {
                base.SetBusyForInput();
                var parms = new List<FwRepPreview.ReportParameter>()
                {
                    // REMARKS:PNAMEは帳票定義のパラメータ名と同一を指定
                    new FwRepPreview.ReportParameter(){ PNAME = "自社名", VALUE = getReportParameterValue(FORM_PARAMS_JIS_NAME)},
                    new FwRepPreview.ReportParameter(){ PNAME = "仕上日From", VALUE = getReportParameterValue(FORM_PARAMS_SALES_DATE_FROM)},
                    new FwRepPreview.ReportParameter(){ PNAME = "仕上日To", VALUE = getReportParameterValue(FORM_PARAMS_SALES_DATE_TO)},
                    new FwRepPreview.ReportParameter(){ PNAME = "加工区分", VALUE = getReportParameterValue(FORM_PARAMS_SALES_KBN_NAME)},
                    new FwRepPreview.ReportParameter(){ PNAME = "外注先名", VALUE = getReportParameterValue(FORM_PARAMS_TOK_NAME)}
                };

                // REMARKS:テーブル名は帳票DataTableの名前と合わせる
                DataTable 印刷データ = SearchResult.Copy();
                印刷データ.TableName = "揚り明細問合せ";

                FwRepPreview.ReportPreview view = new FwRepPreview.ReportPreview();
                view.MakeReport(印刷データ.TableName, ReportFileName, 0, 0, 0);
                view.SetReportData(印刷データ);

                view.SetupParmeters(parms);

                base.SetFreeForInput();

                view.PrinterName = frmcfg.PrinterName;
                view.ShowPreview();
                view.Close();
                frmcfg.PrinterName = view.PrinterName;

            }
            catch (Exception ex)
            {
                base.SetFreeForInput();
                this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
                appLog.Error("仕上明細問合せ一覧表の印刷時に例外が発生しました。", ex);
            }
        }

        /// <summary>
        /// 指定された検索時条件値を取得する
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        private string getReportParameterValue(string keyName)
        {
            return !string.IsNullOrEmpty(paramDic[keyName]) ? paramDic[keyName] : string.Empty;

        }

        #endregion

    }

}
