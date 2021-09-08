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
    using WinFormsScreen = System.Windows.Forms.Screen;

    /// <summary>
    /// 入金伝票問合せ
    /// </summary>
    public partial class ZIJ03010 : RibbonWindowViewBase
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

        #region << 定数定義 >>

        /// <summary>
        /// 入金情報取得
        /// </summary>
        private const string TargetTableNm = "ZIJ03010_GetData";

        /// <summary>
        /// 帳票定義体ファイルパス
        /// </summary>
        private const string ReportFileName = @"Files\ZIJ\ZIJ03010.rpt";

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

        #region << 画面設定項目 >>
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;
        CommonConfig ccfg = null;

        /// <summary>
        /// 画面固有設定項目のクラス定義
        /// ※ 必ず public で定義する。
        /// </summary>
        public class ConfigZIJ03010 : FormConfigBase
        {
            public byte[] spConfig20180118 = null;

        }

        /// ※ 必ず public で定義する。
        public ConfigZIJ03010 frmcfg = null;

        public byte[] sp_Config = null;

        #endregion

        #region << バインディングプロパティ >>

        private DataTable _searchResult = null;
        public DataTable SearchResult
        {
            get { return this._searchResult; }
            set
            {
                this._searchResult = value;
                NotifyPropertyChanged();
            }

        }

        #endregion


        #region<< 入金問合せ 初期処理群 >>

        /// <summary>
        /// 売上明細問合せ
        /// </summary>
        public ZIJ03010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// 画面読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            // スプレッドリセット時
            this.sp_Config = AppCommon.SaveSpConfig(this.spGridList);

            base.MasterMaintenanceWindowList.Add("M01_TOK_TOKU_SCH", new List<Type> { null, typeof(SCHM01_TOK) });
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });
            base.MasterMaintenanceWindowList.Add("M72_TNT", new List<Type> { null, typeof(SCHM72_TNT) });

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            frmcfg = (ConfigZIJ03010)ucfg.GetConfigValue(typeof(ConfigZIJ03010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigZIJ03010();
                ucfg.SetConfigValue(frmcfg);
                frmcfg.spConfig20180118 = this.sp_Config;
            }
            else
            {
                // 表示できるかチェック
                var WidthCHK = WinFormsScreen.PrimaryScreen.Bounds.Width - frmcfg.Left;
                if (WidthCHK > 10)
                    this.Left = frmcfg.Left;

                // 表示できるかチェック
                var HeightCHK = WinFormsScreen.PrimaryScreen.Bounds.Height - frmcfg.Top;
                if (HeightCHK > 10)
                    this.Top = frmcfg.Top;

                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;

            }
            #endregion

            // AppCommon.LoadSpConfig(this.spGridList, frmcfg.spConfig20180118 != null ? frmcfg.spConfig20180118 : this.sp_Config);
            spGridList.InputBindings.Add(new KeyBinding(spGridList.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));

            // No.145 Add Start
            // 金種の名称辞書を作成
            Window view = System.Windows.Window.GetWindow(this);
            List<CodeData> codeList = AppCommon.GetComboboxCodeList(view, "随時", "入金問合せ", "金種", false);
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
            base.ErrorMessage = string.Empty;

        }
        #endregion

        #region データ受信メソッド

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
                case TargetTableNm:
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

                            // フッター合計欄の計算
                            summaryCalc();
                        }
                    }
                    break;

            }

        }

        /// <summary>
        /// データ受信エラー時の処理
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

        #region F8 印刷
        /// <summary>
        /// F8　リボン　印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {

            PrintOut();
        }

        #endregion

        #region F11　終了
        /// <summary>
        /// F11　リボン終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            this.Close();
        }

        #endregion

        #endregion

        #region Window_Closed
        /// <summary>
        /// 画面が閉じられた時、データを保持する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            this.spGridList.InputBindings.Clear();
            this.SearchResult = null;

            if (ucfg != null)
            {
                if (frmcfg == null) { frmcfg = new ConfigZIJ03010(); }
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

            // 入金日(範囲指定)
            DateTime nowDate = DateTime.Today;
            this.depositDateFrom.Text = String.Format("{0:yyyy/MM}/01", nowDate);
            this.depositDateTo.Text = String.Format("{0:yyyy/MM}/{1}", nowDate, DateTime.DaysInMonth(nowDate.Year, nowDate.Month));

            // 金種
            AppCommon.SetutpComboboxList(this.cmbDepositType, false);
            this.cmbDepositType.SelectedIndex = 0;

        }

        #endregion

        #region 検索ボタン

        /// <summary>
        /// 検索ボタンが押下された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
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
                        TargetTableNm,
                        new object[]
                        {
                            iCompany,
                            paramDic
                        }));

                base.SetBusyForInput();

            }
            catch (Exception)
            {

            }

        }

        #endregion

        #region 検索ボタンフォーカス
        /// <summary>
        /// 検索ボタンフォーカス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KensakuBtnFocus(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var ctl = sender as Framework.Windows.Controls.UcLabelTwinTextBox;
                if (ctl == null)
                    return;

                e.Handled = true;
                bool chk = ctl.CheckValidation();

                if (chk == true)
                {
                    //Keyboard.Focus(this.btnKensaku);
                }
                else
                {
                    ctl.Focus();
                    this.ErrorMessage = ctl.GetValidationMessage();
                }
            }
        }
        #endregion

        /// <summary>
        /// 検索パラメータを設定する
        /// </summary>
        private void setSearchParams()
        {
            paramDic.Clear();
            paramDic.Add("入金日From", depositDateFrom.Text);
            paramDic.Add("入金日To", depositDateTo.Text);
            paramDic.Add("伝票番号From", slipNoFrom.Text);
            paramDic.Add("伝票番号To", slipNoTo.Text);
            paramDic.Add("金種コード", cmbDepositType.SelectedValue.ToString());
            paramDic.Add("入金元販社コード", depositCompany.Text1);
            paramDic.Add("得意先コード", TOK.Text1);
            paramDic.Add("得意先枝番", TOK.Text2);

            paramDic.Add("自社名", myCompany.Text2);
            paramDic.Add("金種名", goldNameDic[int.Parse(cmbDepositType.SelectedValue.ToString())]);
            paramDic.Add("入金元販社名", depositCompany.Text2);
            paramDic.Add("得意先名", TOK.Label2Text);

        }

        /// <summary>
        /// 各金種の合計を算出して設定する
        /// </summary>
        private void summaryCalc()
        {
            // No.145 Mod Start
            sumCash.Text = string.Format("{0:#,0}",parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("現金")).Key));
            sumTransfer.Text = string.Format("{0:#,0}",parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("振込")).Key));
            sumCheck.Text = string.Format("{0:#,0}",parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("小切手")).Key));
            sumPromissory.Text = string.Format("{0:#,0}",parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("手形")).Key));
            sumOffset.Text = string.Format("{0:#,0}",parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("相殺")).Key));
            sumAjustment.Text = string.Format("{0:#,0}",parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("調整")).Key));
            sumOther.Text = string.Format("{0:#,0}",
                                parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("その他")).Key) + 
                                parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("振込手数料")).Key) + 
                                parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("歩引き")).Key) + 
                                parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("送料")).Key) + 
                                parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("協賛金")).Key) + 
                                parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("支払明細代")).Key) +
                                parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("ｶﾀﾛｸﾞ掲載料")).Key) +
                                parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("売掛金振替")).Key) + 
                                parseLongSummary(goldNameDic.FirstOrDefault(x => x.Value.Equals("買掛金振替")).Key));
            // No.145 Mod End

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

            return  long.Parse(sVal);

        }

        #region 印刷処理
        /// <summary>
        /// 印刷処理
        /// </summary>
        void PrintOut()
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
                    new FwRepPreview.ReportParameter(){ PNAME = "入金日From", VALUE = string.IsNullOrEmpty(paramDic["入金日From"]) ? "" : paramDic["入金日From"]},
                    new FwRepPreview.ReportParameter(){ PNAME = "入金日To", VALUE = string.IsNullOrEmpty(paramDic["入金日To"]) ? "" : paramDic["入金日To"]},
                    new FwRepPreview.ReportParameter(){ PNAME = "伝票番号From", VALUE = string.IsNullOrEmpty(paramDic["伝票番号From"]) ? "" : paramDic["伝票番号From"]},
                    new FwRepPreview.ReportParameter(){ PNAME = "伝票番号To", VALUE = string.IsNullOrEmpty(paramDic["伝票番号To"]) ? "" : paramDic["伝票番号To"]},
                    new FwRepPreview.ReportParameter(){ PNAME = "金種コード", VALUE = paramDic["金種名"]},
                    new FwRepPreview.ReportParameter(){ PNAME = "入金元販社", VALUE = string.IsNullOrEmpty(paramDic["入金元販社名"]) ? "" : paramDic["入金元販社名"]},
                    new FwRepPreview.ReportParameter(){ PNAME = "得意先", VALUE = string.IsNullOrEmpty(paramDic["得意先名"]) ? "" : paramDic["得意先名"]}
                };

                DataTable 印刷データ = SearchResult.Copy();
                印刷データ.TableName = "入金明細問合せ";

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
                appLog.Error("入金問合せ一覧表の印刷時に例外が発生しました。", ex);
            }

        }
        #endregion

        #endregion

    }

}