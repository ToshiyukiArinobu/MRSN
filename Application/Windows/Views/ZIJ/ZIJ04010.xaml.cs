using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace KyoeiSystem.Application.Windows.Views
{
    using GrapeCity.Windows.SpreadGrid;
    using FwRepPreview = KyoeiSystem.Framework.Reports.Preview;
    using WinForms = System.Windows.Forms;

    /// <summary>
    /// 出金問合せ フォームクラス
    /// </summary>
    public partial class ZIJ04010 : RibbonWindowViewBase
    {
        #region << 列挙型定義 >>

        /// <summary>
        /// データグリッドの列定義
        /// </summary>
        private enum GridColumnsMapping : int
        {
            入金日 = 0,
            伝票番号 = 1,
            得意先 = 2,
            入金元販社 = 3,
            金種 = 4,
            期日 = 5,
            金額 = 6,
            摘要 = 7
        }

        /// <summary>
        /// 自社販社区分 内包データ
        /// </summary>
        private enum 自社販社区分 : int
        {
            自社 = 0,
            販社 = 1
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
        public class ConfigZIJ04010 : FormConfigBase
        {
            public byte[] spConfig20180118 = null;

        }

        /// ※ 必ず public で定義する。
        public ConfigZIJ04010 frmcfg = null;

        public byte[] sp_Config = null;

        #endregion

        #region << 定数定義 >>

        private const string ZIJ04010_GetDataList = "ZIJ04010_GetData";
        private const string ReportFileName = @"Files\ZIJ\ZIJ04010.rpt";

        #endregion

        #region << 変数定義 >>

        private Dictionary<string, string> paramDic = new Dictionary<string, string>();
        // No.145 Add Start
        private Dictionary<int, string> goldNameDic = new Dictionary<int, string>()
        {
            { 0, "指定なし" }, 
        };
        // No.145 Add End
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

        #region<< 出金問合せ 初期処理群 >>

        /// <summary>
        /// 出金問合せ コンストラクタ
        /// </summary>
        public ZIJ04010()
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
            base.MasterMaintenanceWindowList.Add("M72_TNT", new List<Type> { typeof(MST23010), typeof(SCHM72_TNT) });

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            frmcfg = (ConfigZIJ04010)ucfg.GetConfigValue(typeof(ConfigZIJ04010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigZIJ04010();
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

            // No.145 Add Start
            // 金種の名称辞書を作成
            Window view = System.Windows.Window.GetWindow(this);
            List<CodeData> codeList = AppCommon.GetComboboxCodeList(view, "随時", "出金問合せ", "金種", false);
            codeList = codeList.Where(x => x.コード != 0).ToList();
            foreach (CodeData code in codeList)
            {
                goldNameDic.Add(code.コード, code.表示名);
            }
            // No.145 Add End

            // コントロールの初期設定をおこなう
            initSearchControl();

            spGridList.RowCount = 0;
            SetFocusToTopControl();
            ErrorMessage = string.Empty;

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
                case ZIJ04010_GetDataList:
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
                            return;
                        }
                        else
                        {
                            SearchResult = tbl;
                            // No.383 Add Start
                            // フォーカスをSPREADへ
                            spGridList.Focus();
                            spGridList.Focusable = true;
                            spGridList.ActiveCellPosition = new CellPosition(0, (int)GridColumnsMapping.入金日);
                            spGridList.ShowCell(0, (int)GridColumnsMapping.入金日);
                            // No.383 Add End

                            summaryCalc();
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
                        ZIJ04010_GetDataList,
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
                if (frmcfg == null) { frmcfg = new ConfigZIJ04010(); }
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

            // 出金日(範囲指定)
            DateTime nowDate = DateTime.Today;
            this.withdrawalDateFrom.Text = String.Format("{0:yyyy/MM}/01", nowDate);
            this.withdrawalDateTo.Text = String.Format("{0:yyyy/MM}/{1}", nowDate, DateTime.DaysInMonth(nowDate.Year, nowDate.Month));

            // 金種
            AppCommon.SetutpComboboxList(this.cmbDepositType, false);
            this.cmbDepositType.SelectedIndex = 0;

        }

        #endregion

        /// <summary>
        /// 検索パラメータを設定する
        /// </summary>
        private void setSearchParams()
        {
            paramDic.Clear();
            paramDic.Add("出金日From", withdrawalDateFrom.Text);
            paramDic.Add("出金日To", withdrawalDateTo.Text);
            paramDic.Add("金種コード", cmbDepositType.SelectedValue.ToString());
            paramDic.Add("出金先販社コード", depositCompany.Text1);
            paramDic.Add("得意先コード", TOK.Text1);
            paramDic.Add("得意先枝番", TOK.Text2);

            paramDic.Add("自社名", myCompany.Text2);
            paramDic.Add("金種名", goldNameDic[int.Parse(cmbDepositType.SelectedValue.ToString())]);
            paramDic.Add("出金先販社名", depositCompany.Text2);
            paramDic.Add("得意先名", TOK.Label2Text);
            paramDic.Add("伝票番号From", slipNoFrom.Text);
            paramDic.Add("伝票番号To", slipNoTo.Text);

        }

        /// <summary>
        /// 各金種の合計を算出して設定する
        /// </summary>
        private void summaryCalc()
        {
            // Mo.145 Mod Start
            sumCash.Text = string.Format("{0:#,0}", parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("現金")).Key));
            sumTransfer.Text = string.Format("{0:#,0}", parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("振込")).Key));
            sumCheck.Text = string.Format("{0:#,0}", parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("小切手")).Key));
            sumPromissory.Text = string.Format("{0:#,0}", parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("手形")).Key));
            sumOffset.Text = string.Format("{0:#,0}", parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("相殺")).Key));
            sumAjustment.Text = string.Format("{0:#,0}", parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("調整")).Key));
            sumOther.Text = string.Format("{0:#,0}",
                                parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("その他")).Key) +
                                parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("振込手数料")).Key) +
                                parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("歩引き")).Key) +
                                parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("送料")).Key) +
                                parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("協賛金")).Key) +
                                parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("支払明細代")).Key) +
                                parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("ｶﾀﾛｸﾞ掲載料")).Key) +
                                parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("売掛金振替")).Key) +
                                parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("買掛金振替")).Key) +
                                parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("口座振替")).Key));
            // Mo.145 Mod End

            sumTotal.Text = string.Format("{0:#,0}",parseLongSummary(null));

        }

        /// <summary>
        /// 指定金種の合計値を取得して返す
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private long parseLongSummary(int? code)
        {
            string sVal;
            if (code == null)
                sVal = SearchResult.Compute("Sum(金額)", "").ToString();

            else
                sVal = SearchResult.Compute("Sum(金額)", string.Format("金種コード = '{0}'", code.GetHashCode())).ToString();

            if (string.IsNullOrEmpty(sVal))
                return 0;

            return long.Parse(sVal);

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
                    new FwRepPreview.ReportParameter(){ PNAME = "自社コード", VALUE = paramDic["自社名"]},
                    new FwRepPreview.ReportParameter(){ PNAME = "出金日From", VALUE = string.IsNullOrEmpty(paramDic["出金日From"]) ? "" : paramDic["出金日From"]},
                    new FwRepPreview.ReportParameter(){ PNAME = "出金日To", VALUE = string.IsNullOrEmpty(paramDic["出金日To"]) ? "" : paramDic["出金日To"]},
                    new FwRepPreview.ReportParameter(){ PNAME = "金種コード", VALUE = paramDic["金種名"]},
                    new FwRepPreview.ReportParameter(){ PNAME = "出金先販社", VALUE = string.IsNullOrEmpty(paramDic["出金先販社名"]) ? "" : paramDic["出金先販社名"]},
                    new FwRepPreview.ReportParameter(){ PNAME = "得意先", VALUE = string.IsNullOrEmpty(paramDic["得意先名"]) ? "" : paramDic["得意先名"]},
                    new FwRepPreview.ReportParameter(){ PNAME = "伝票番号From", VALUE = string.IsNullOrEmpty(paramDic["伝票番号From"]) ? "" : paramDic["伝票番号From"]},
                    new FwRepPreview.ReportParameter(){ PNAME = "伝票番号To", VALUE = string.IsNullOrEmpty(paramDic["伝票番号To"]) ? "" : paramDic["伝票番号To"]},
                };

                DataTable 印刷データ = SearchResult.Copy();
                印刷データ.TableName = "出金明細問合せ";

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
                appLog.Error("出金問合せ一覧表の印刷時に例外が発生しました。", ex);
            }
        }

        #endregion

    }

}
