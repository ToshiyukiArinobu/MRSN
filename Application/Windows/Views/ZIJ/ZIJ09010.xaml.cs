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
    /// 商品在庫問合せ フォームクラス
    /// </summary>
    public partial class ZIJ09010 : RibbonWindowViewBase
    {
        #region << 列挙型定義 >>

        /// <summary>
        /// データグリッドの列定義
        /// </summary>
        private enum GridColumnsMapping : int
        {
            印刷区分 = 0,
            得意先コード = 1,
            得意先枝番 = 2,
            得意先名 = 3,
            回数 = 4,
            集計期間 = 5,
            当月請求額 = 6,
            郵便番号 = 7,
            住所１ = 8,
            住所２ = 9,
            電話番号 = 10
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
        private const string GET_DATA = "ZIJ09010_GetData";

        #region レポート定義

        // 商品在庫一覧表
        const string ReportFileName = @"Files\ZIJ\ZIJ09010.rpt";

        #endregion

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
        public class ConfigZIJ09010 : FormConfigBase
        {
            public byte[] spConfigZIJ09010 = null;

        }

        /// ※ 必ず public で定義する。
        public ConfigZIJ09010 frmcfg = null;

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
        /// 売上明細問合せ
        /// </summary>
        public ZIJ09010()
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
            this.spGridList.Rows.Clear();
            this.sp_Config = AppCommon.SaveSpConfig(this.spGridList);

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            frmcfg = (ConfigZIJ09010)ucfg.GetConfigValue(typeof(ConfigZIJ09010));

            if (frmcfg == null)
            {
                frmcfg = new ConfigZIJ09010();
				ucfg.SetConfigValue(frmcfg);
				frmcfg.spConfigZIJ09010 = this.sp_Config;
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

            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });
            base.MasterMaintenanceWindowList.Add("M09_MYHIN", new List<Type> { typeof(MST02010), typeof(SCHM09_MYHIN) });
            base.MasterMaintenanceWindowList.Add("M14_BRAND", new List<Type> { typeof(MST04020), typeof(SCHM14_BRAND) });
            base.MasterMaintenanceWindowList.Add("M15_SERIES", new List<Type> { typeof(MST04021), typeof(SCHM15_SERIES) });

            AppCommon.SetutpComboboxList(this.cmbItemType, false);

            //↓よくわからないプログラム(運坊にあり)
            //AppCommon.LoadSpConfig(this.spGridList, frmcfg.spConfigZIJ09010 != null ? frmcfg.spConfigZIJ09010 : this.sp_Config);
            //this.表示固定列数 = this.spGridList.FrozenColumnCount.ToString();

            //入出庫明細検索用
            //ButtonCellType btn = this.spGridList.Columns[0].CellType as ButtonCellType;
            //btn.Command = new cmd売上詳細表示(spGridList);

            initSearchControl();

            // 初期フォーカスを設定
            this.myCompany.SetFocus();
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
                    spGridList.ActiveCellPosition = new CellPosition(0, 0);
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
                CSVData.SaveCSV(SearchResult, sfd.FileName, true, true, false, ',', true);
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
                int selItemType = int.Parse(cmbItemType.SelectedValue.ToString());
                Dictionary<int, string> itemTypeDic = new Dictionary<int, string>();
                itemTypeDic.Add(0, "指定なし");
                itemTypeDic.Add(1, "食品");
                itemTypeDic.Add(2, "繊維");
                itemTypeDic.Add(3, "その他");

                base.SetBusyForInput();
                var param = new List<Framework.Reports.Preview.ReportParameter>()
				{
                    new FwRepPreview.ReportParameter() { PNAME = "自社コード", VALUE = myCompany.Text2 },
                    new FwRepPreview.ReportParameter() { PNAME = "商品コード", VALUE = string.IsNullOrEmpty(Product.Text2) ? "" : Product.Text2  },
                    new FwRepPreview.ReportParameter() { PNAME = "商品分類", VALUE = itemTypeDic[selItemType] },
                    new FwRepPreview.ReportParameter() { PNAME = "商品名指定", VALUE = ProductName.Text },
                    new FwRepPreview.ReportParameter() { PNAME = "ブランド", VALUE = string.IsNullOrEmpty(Brand.Text2) ? "" : Brand.Text2 },
                    new FwRepPreview.ReportParameter() { PNAME = "シリーズ", VALUE = string.IsNullOrEmpty(Series.Text2) ? "" : Series.Text2 },
                };

                FwRepPreview.ReportPreview view = new FwRepPreview.ReportPreview();

                DataTable 印刷データ = SearchResult.Copy();
                印刷データ.TableName = "商品在庫問合せ";

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
                appLog.Error("商品在庫問合せ一覧の印刷時に例外が発生しました。", ex);

            }

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

            // 自社コードの入力値検証
            int companyCd;
            if (string.IsNullOrEmpty(myCompany.Text1))
            {
                base.ErrorMessage = "自社コードは必須入力項目です。";
                return;
            }
            else if (!int.TryParse(myCompany.Text1, out companyCd))
            {
                base.ErrorMessage = "自社コードの入力値に誤りがあります。";
                return;
            }

            // REMARKS:条件が増える場合は下記に追加する
            Dictionary<string, string> cond = new Dictionary<string, string>();
            cond.Add("自社品番", Product.Text1);
            cond.Add("検索品名", ProductName.Text);
            cond.Add("商品分類", cmbItemType.SelectedValue.ToString());
            cond.Add("ブランド", Brand.Text1);
            cond.Add("シリーズ", Series.Text1);

            base.SendRequest(
                new CommunicationObject(
                    MessageType.CallStoredProcedure,
                    GET_DATA,
                    new object[] {
                        companyCd,
                        ccfg.自社販社区分,
                        cond
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
                if (frmcfg == null) { frmcfg = new ConfigZIJ09010(); }
                frmcfg.Top = this.Top;
                frmcfg.Left = this.Left;
                frmcfg.Width = this.Width;
                frmcfg.Height = this.Height;
                frmcfg.spConfigZIJ09010 = AppCommon.SaveSpConfig(this.spGridList);
                ucfg.SetConfigValue(frmcfg);
                spGridList.InputBindings.Clear();

            }

        }
        #endregion

        /// <summary>
        /// 検索条件部の初期設定をおこなう
        /// </summary>
        private void initSearchControl()
        {
            this.myCompany.Text1 = ccfg.自社コード.ToString();
            this.myCompany.IsEnabled = ccfg.自社販社区分 == 自社販社区分.自社.GetHashCode();

        }

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

        /// <summary>
        /// ShouhinmeiSitei_PreviewKeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShouhinmeiSitei_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var ctl = sender as Framework.Windows.Controls.UcLabelTextBox;
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

    }

}
