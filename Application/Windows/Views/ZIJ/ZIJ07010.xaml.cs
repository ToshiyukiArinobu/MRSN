using GrapeCity.Windows.SpreadGrid;
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
    using WinFormsScreen = System.Windows.Forms.Screen;

    /// <summary>
    /// 商品入出荷問合せ フォームクラス
    /// </summary>
    public partial class ZIJ07010 : RibbonWindowViewBase
    {
        #region << 列挙型定義 >>

        /// <summary>
        /// データグリッドの列定義
        /// </summary>
        private enum GridColumnsMapping : int
        {
            移動日 = 0,
            伝票番号 = 1,
            自社名 = 2,
            移動区分 = 3,
            移動元倉庫 = 4,
            移動先倉庫 = 5,
            品番コード = 6,
            自社品番 = 7,
            商品名 = 8,
            賞味期限 = 9,
            数量 = 10,
            単位 = 11
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

        /// <summary>検索サービス定数</summary>
        private const string GET_DATA = "ZIJ07010_GetData";

        #region レポート定義

        // 商品在庫一覧表
        private const string ReportFileName = @"Files\ZIJ\ZIJ07010.rpt";

        private const string PARAMS_NAME_入出庫開始日 = "入出庫開始日";
        private const string PARAMS_NAME_入出庫終了日 = "入出庫終了日";
        private const string PARAMS_NAME_入出庫区分 = "入出庫区分";
        private const string PARAMS_NAME_自社品番 = "自社品番";
        private const string PARAMS_NAME_倉庫コード = "倉庫コード";
        private const string PARAMS_NAME_自社品名 = "自社品名";
        private const string PARAMS_NAME_入出庫区分名 = "入出庫区分名";
        private const string PARAMS_NAME_倉庫名 = "倉庫名";

        #endregion

        #endregion

        #region << 変数定義 >>

        /// <summary>
        /// 検索時の条件を格納
        /// </summary>
        Dictionary<string, string> conditions = new Dictionary<string, string>();

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
        public class ConfigZIJ07010 : FormConfigBase
        {
            public byte[] spConfigZIJ07010 = null;

        }

        /// ※ 必ず public で定義する。
        public ConfigZIJ07010 frmcfg = null;

        public byte[] sp_Config = null;

        #endregion

        #region << バインド用プロパティ >>

        DataTable _searchResult;
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

        #region << フォーム初期処理 >>

        /// <summary>
        /// 商品入出荷問合せ
        /// コンストラクタ
        /// </summary>
        public ZIJ07010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// 画面が表示された後のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 初期状態を保存（SPREADリセット時にのみ使用する）
            this.spGridList.Rows.Clear();
            this.sp_Config = AppCommon.SaveSpConfig(this.spGridList);

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            frmcfg = (ConfigZIJ07010)ucfg.GetConfigValue(typeof(ConfigZIJ07010));

            if (frmcfg == null)
            {
                frmcfg = new ConfigZIJ07010();
                ucfg.SetConfigValue(frmcfg);
                frmcfg.spConfigZIJ07010 = this.sp_Config;
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

                this.Width = frmcfg.Width;
                this.Height = frmcfg.Height;

            }
            #endregion

            base.MasterMaintenanceWindowList.Add("M09_MYHIN", new List<Type> { typeof(MST02010), typeof(SCHM09_MYHIN) });
            base.MasterMaintenanceWindowList.Add("M22_SOUK", new List<Type> { typeof(MST12020), typeof(SCHM22_SOUK) });

            AppCommon.SetutpComboboxList(cmbInbound);

            initSearchControl();

            SetFocusToTopControl();
            ResetAllValidation();

        }

        #endregion

        #region 明細クリック時のアクション定義
        /// <summary>
        /// 明細クリック時のアクション定義
        /// </summary>
        public class cmd売上詳細表示 : ICommand
        {
            private GcSpreadGrid _gcSpreadGrid;
            public cmd売上詳細表示(GcSpreadGrid gcSpreadGrid)
            {
                this._gcSpreadGrid = gcSpreadGrid;
            }
            public bool CanExecute(object parameter)
            {
                return true;
            }
            public event EventHandler CanExecuteChanged;
            public void OnCanExecuteChanged()
            {
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, EventArgs.Empty);
            }
            public void Execute(object parameter)
            {
                CellCommandParameter cellCommandParameter = (CellCommandParameter)parameter;
                if (cellCommandParameter.Area == SpreadArea.Cells)
                {
                    int rowNo = cellCommandParameter.CellPosition.Row;
                    var row = this._gcSpreadGrid.Rows[rowNo];

/*
                    var wnd = GetWindow(this._gcSpreadGrid);
                    //表示区分
                    int i表示区分 = Convert.ToInt32(this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["表示区分"].Index].Value.ToString());
                    string d検索日付FROM = this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["検索日付FROM"].Index].Value.ToString();
                    string d検索日付TO = this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["検索日付TO"].Index].Value.ToString();

                    if (i表示区分 == 0)
                    {

                        string s得意先ID = this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["得意先ID"].Index].Value.ToString();
                        string s商品ID = this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["商品ID"].Index].Value.ToString();


                        //商品毎
                        DLY39010 frm = new DLY39010();
                        frm.初期得意先ID = s得意先ID;
                        frm.初期商品ID = s商品ID;
                        frm.初期表示区分 = i表示区分;
                        frm.初期検索日付From = Convert.ToDateTime(d検索日付FROM).ToShortDateString();
                        frm.初期検索日付To = Convert.ToDateTime(d検索日付TO).ToShortDateString();
                        frm.ShowDialog(wnd);
                    }
                    else if (i表示区分 == 1)
                    {

                        string s得意先ID = this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["得意先ID"].Index].Value.ToString();
                        string s商品ID = this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["商品ID"].Index].Value.ToString();
                        string sロット = this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["ロット番号"].Index].Value == null ? "" : this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["ロット番号"].Index].Value.ToString();

                        //商品+ロット毎
                        DLY39010 frm = new DLY39010();
                        frm.初期得意先ID = s得意先ID;
                        frm.初期商品ID = s商品ID;
                        frm.初期ロット = sロット;
                        frm.初期表示区分 = i表示区分;
                        frm.初期検索日付From = Convert.ToDateTime(d検索日付FROM).ToShortDateString();
                        frm.初期検索日付To = Convert.ToDateTime(d検索日付TO).ToShortDateString();
                        frm.ShowDialog(wnd);
                    }
                    else if (i表示区分 == 2)
                    {

                        string s得意先ID = this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["得意先ID"].Index].Value.ToString();
                        string s商品ID = this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["商品ID"].Index].Value.ToString();
                        string s倉庫ID = this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["倉庫ID"].Index].Value.ToString();

                        //商品+倉庫
                        DLY39010 frm = new DLY39010();
                        frm.初期得意先ID = s得意先ID;
                        frm.初期商品ID = s商品ID;
                        frm.初期倉庫ID = s倉庫ID;
                        frm.初期表示区分 = i表示区分;
                        frm.初期検索日付From = d検索日付FROM;
                        frm.初期検索日付To = d検索日付TO;
                        frm.ShowDialog(wnd);
                    }
                    else if (i表示区分 == 3)
                    {

                        string s得意先ID = this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["得意先ID"].Index].Value.ToString();
                        string s商品ID = this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["商品ID"].Index].Value.ToString();
                        string s倉庫ID = this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["倉庫ID"].Index].Value.ToString();
                        string sロット = this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["ロット番号"].Index].Value == null ? "" : this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["ロット番号"].Index].Value.ToString();
                        string sロケーション = this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["ロケーション"].Index].Value == null ? "" : this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["ロケーション"].Index].Value.ToString();

                        //商品+倉庫
                        DLY39010 frm = new DLY39010();
                        frm.初期得意先ID = s得意先ID;
                        frm.初期商品ID = s商品ID;
                        frm.初期倉庫ID = s倉庫ID;
                        frm.初期表示区分 = i表示区分;
                        frm.初期ロケーション = sロケーション;
                        frm.初期検索日付From = d検索日付FROM;
                        frm.初期検索日付To = d検索日付TO;
                        frm.ShowDialog(wnd);
                    }
                    else if (i表示区分 == 4)
                    {

                        string s得意先ID = this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["得意先ID"].Index].Value.ToString();
                        string s商品ID = this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["商品ID"].Index].Value.ToString();
                        string s倉庫ID = this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["倉庫ID"].Index].Value.ToString();
                        string sロット = this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["ロット番号"].Index].Value == null ? "" : this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["ロット番号"].Index].Value.ToString();
                        string sロケーション = this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["ロケーション"].Index].Value == null ? "" : this._gcSpreadGrid[rowNo, this._gcSpreadGrid.Columns["ロケーション"].Index].Value.ToString();

                        //商品+倉庫+ロット
                        DLY39010 frm = new DLY39010();
                        frm.初期得意先ID = s得意先ID;
                        frm.初期商品ID = s商品ID;
                        frm.初期倉庫ID = s倉庫ID;
                        frm.初期ロット = sロット;
                        frm.初期ロケーション = sロケーション;
                        frm.初期表示区分 = i表示区分;
                        frm.初期検索日付From = d検索日付FROM;
                        frm.初期検索日付To = d検索日付TO;
                        frm.ShowDialog(wnd);
                    }
*/
                }
            }
        }
        #endregion

        #region ﾋﾟｯｸｱｯﾌﾟ指定Grid読込

        /// <summary>
        /// ピックアップ指定のGridの読み込み
        /// </summary>
        private void GetPickupCodeList()
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            this.Message = base.ErrorMessage;
            base.SetFreeForInput();
        }

        #endregion

        #region << データ受信メソッド >>

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

                case GET_DATA:
                    base.SetFreeForInput();
                    if(tbl == null || tbl.Rows.Count == 0)
                    {
                        this.ErrorMessage = "該当するデータが見つかりません";
                        SearchResult = null;
                        return;
                    }

                    SearchResult = tbl;

                    // フォーカスをSPREADへ
                    spGridList.Focus();
                    spGridList.Focusable = true;
                    spGridList.ActiveCellPosition = new CellPosition(0, 1);
                    spGridList.ShowCell(0, 1);    // No.383 Add
                    break;

            }

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
        #endregion

        #region F5 CSV出力
        /// <summary>
        /// F5　リボン　CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            if (this.SearchResult == null)
                return;

            if (this.SearchResult.Rows.Count == 0)
            {
                this.ErrorMessage = "印刷データがありません。";
                return;
            }

            if (this.spGridList.ActiveCellPosition.Row < 0)
            {
                MessageBox.Show("検索データがありません。");
                return;
            }

            DataTable SearchResultCsvData = SearchResult.Copy();

            SearchResultCsvData.Columns.Remove("SEQ");

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
                CSVData.SaveCSV(SearchResultCsvData, sfd.FileName, true, true, false, ',', true);
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
            if (this.SearchResult == null)
                return;

            if (this.SearchResult.Rows.Count == 0)
            {
                this.ErrorMessage = "印刷データがありません。";
                return;
            }

            try
            {
                base.SetBusyForInput();

                var param = new List<FwRepPreview.ReportParameter>()
                {
                    new FwRepPreview.ReportParameter() { PNAME = PARAMS_NAME_入出庫開始日, VALUE = getReportParameterValue(PARAMS_NAME_入出庫開始日) },
                    new FwRepPreview.ReportParameter() { PNAME = PARAMS_NAME_入出庫終了日, VALUE = getReportParameterValue(PARAMS_NAME_入出庫終了日) },
                    new FwRepPreview.ReportParameter() { PNAME = PARAMS_NAME_入出庫区分名, VALUE = getReportParameterValue(PARAMS_NAME_入出庫区分名) },
                    new FwRepPreview.ReportParameter() { PNAME = PARAMS_NAME_倉庫名, VALUE = getReportParameterValue(PARAMS_NAME_倉庫名) },
                    new FwRepPreview.ReportParameter() { PNAME = PARAMS_NAME_自社品名, VALUE = getReportParameterValue(PARAMS_NAME_自社品名) },
                };

                FwRepPreview.ReportPreview view = new FwRepPreview.ReportPreview();

                DataTable 印刷データ = SearchResult.Copy();
                印刷データ.TableName = "商品入出荷問合せ";

                view.MakeReport(印刷データ.TableName, ReportFileName, 0, 0, 0);
                view.SetReportData(印刷データ);

                base.SetFreeForInput();

                view.PrinterName = frmcfg.PrinterName;
                view.SetupParmeters(param);
                view.ShowPreview();
                view.Close();

                frmcfg.PrinterName = view.PrinterName;

            }
            catch (Exception ex)
            {
                base.SetFreeForInput();
                this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
                appLog.Error("商品入出荷問合せ一覧の印刷時に例外が発生しました。", ex);

            }

        }

        /// <summary>
        /// 指定された検索時条件値を取得する
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        private string getReportParameterValue(string keyName)
        {
            return !string.IsNullOrEmpty(conditions[keyName]) ? conditions[keyName] : string.Empty;

        }

        #endregion

        #region F11 終了
        /// <summary>
        /// F11　リボン　終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF11Key(object sender, KeyEventArgs e )
        {
            this.Close();
        }
        #endregion

        #endregion

        #region 検索ボタン

        /// <summary>
        /// 検索ボタンが押下された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            if (!base.CheckAllValidation())
            {
                this.ErrorMessage = "入力内容に誤りがあります。";
                MessageBox.Show("入力内容に誤りがあります。");
                return;
            }

            // 検索条件を設定
            setSearchContidions();

            base.SendRequest(
                new CommunicationObject(
                    MessageType.CallStoredProcedure,
                    GET_DATA,
                    new object[] {
                        conditions
                    }));

            base.SetBusyForInput();

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
            if (ucfg != null)
            {
                if (frmcfg == null) { frmcfg = new ConfigZIJ07010(); }
                frmcfg.Top = this.Top;
                frmcfg.Left = this.Left;
                frmcfg.Width = this.Width;
                frmcfg.Height = this.Height;
                frmcfg.spConfigZIJ07010 = null;// AppCommon.SaveSpConfig(this.spGridList);
                ucfg.SetConfigValue(frmcfg);
                spGridList.InputBindings.Clear();

            }

        }
        #endregion

        #region 検索条件部の初期設定
        /// <summary>
        /// 検索条件部の初期設定をおこなう
        /// </summary>
        private void initSearchControl()
        {
            // 入出荷日(From～To)
            this.txtMoveDatePriod.Text1 = AppCommon.GetDateFirst().ToString("yyyy/MM/dd");
            this.txtMoveDatePriod.Text2 = AppCommon.GetDateLast().ToString("yyyy/MM/dd");

            // 入出荷区分
            this.cmbInbound.SelectedIndex = 0;

            // 出荷元倉庫
            this.txtConsignor.Text1 = string.Empty;

            // 自社品番
            this.txtProduct.Text1 = string.Empty;

        }
        #endregion

        #region 検索条件をディクショナリに設定
        /// <summary>
        /// 入力状態を検索条件として格納する
        /// </summary>
        private void setSearchContidions()
        {
            conditions.Clear();
            // REMARKS:条件が増える場合は下記に追加する
            conditions.Add(PARAMS_NAME_入出庫開始日, txtMoveDatePriod.Text1);
            conditions.Add(PARAMS_NAME_入出庫終了日, txtMoveDatePriod.Text2);
            conditions.Add(PARAMS_NAME_入出庫区分, cmbInbound.Combo_SelectedValue.ToString());
            conditions.Add(PARAMS_NAME_自社品番, txtProduct.Text1);
            conditions.Add(PARAMS_NAME_倉庫コード, txtConsignor.Text1);

            conditions.Add(PARAMS_NAME_自社品名, txtProduct.Text2);
            conditions.Add(PARAMS_NAME_入出庫区分名, cmbInbound.Combo_Text);
            conditions.Add(PARAMS_NAME_倉庫名, txtConsignor.Text2);

        }
        #endregion

        #region スプレッドシート イベント関連

        /// <summary>
        /// スプレッドグリッドからフォーカスが外れた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spGridList_LostFocus(object sender, RoutedEventArgs e)
        {
            if (spGridList.Focusable == true)
            {
                spGridList.SelectionBorder.Style = BorderLineStyle.Thick;
                spGridList.SelectionBorderUnfocused.Style = BorderLineStyle.None;
            }
            else
            {
                spGridList.SelectionBorder.Style = BorderLineStyle.None;
                spGridList.SelectionBorderUnfocused.Style = BorderLineStyle.None;
            }
        }

        #endregion

    }

}
