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
    /// 請求締集計 画面クラス
    /// </summary>
    public partial class TKS01010 : WindowReportBase
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
        public class ConfigTKS01010 : FormConfigBase
        {
            public byte[] spConfig = null;
        }

        /// ※ 必ず public で定義する。
        public ConfigTKS01010 frmcfg = null;
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

        private const string GET_LIST_SEARCH = "GetAggrListData";
        private const string BILLING_AGGREGATE = "BillingAggregation";

        // SPREADのCELLに移動したとき入力前に表示されていた文字列保存用
        string _originalText = null;

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

        #region 明細クリック時のアクション定義
        /// <summary>
        /// 明細クリック時のアクション定義
        /// </summary>
        public class cmd期間クリア : ICommand
        {
            private GcSpreadGrid _gcSpreadGrid;
            public cmd期間クリア(GcSpreadGrid gcSpreadGrid)
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
                    var wnd = GetWindow(this._gcSpreadGrid);

                    var a = row.Cells[this._gcSpreadGrid.Columns["開始日付1"].Index].CellType;
                    var b = row.Cells[this._gcSpreadGrid.Columns["開始日付2"].Index].CellType;
                    row.Cells[this._gcSpreadGrid.Columns["開始日付1"].Index].Value = row.Cells[this._gcSpreadGrid.Columns["クリア開始日付"].Index].Value;
                    row.Cells[this._gcSpreadGrid.Columns["終了日付1"].Index].Value = row.Cells[this._gcSpreadGrid.Columns["クリア終了日付"].Index].Value;
                    row.Cells[this._gcSpreadGrid.Columns["開始日付2"].Index].Value = DBNull.Value;
                    row.Cells[this._gcSpreadGrid.Columns["終了日付2"].Index].Value = DBNull.Value;
                    row.Cells[this._gcSpreadGrid.Columns["開始日付3"].Index].Value = DBNull.Value;
                    row.Cells[this._gcSpreadGrid.Columns["終了日付3"].Index].Value = DBNull.Value;

                }
            }
        }
        #endregion

        #region << 画面初期処理 >>

        /// <summary>
        /// 請求書発行
        /// </summary>
        public TKS01010()
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
            this.sp請求データ一覧.RowCount = 0;
            //this.spConfig = AppCommon.SaveSpConfig(this.sp請求データ一覧);

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigTKS01010)ucfg.GetConfigValue(typeof(ConfigTKS01010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigTKS01010();
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

            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { typeof(MST01010), typeof(SCHM01_TOK) });
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });

            this.sp請求データ一覧.PreviewKeyDown += sp請求データ一覧_PreviewKeyDown;

            ButtonCellType btn = this.sp請求データ一覧.Columns[3].CellType as ButtonCellType;
            btn.Command = new cmd期間クリア(sp請求データ一覧);

            this.MyCompany.Text1 = ccfg.自社コード.ToString();
            this.MyCompany.Text1IsReadOnly = (ccfg.自社販社区分 != 0);
            this.CreateYearMonth.Text = string.Format("{0:yyyy/MM}", DateTime.Now);

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
                    case GET_LIST_SEARCH:
                        if (tbl == null)
                        {
                            this.sp請求データ一覧.ItemsSource = null;
                            this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
                            return;
                        }
                        else
                        {
                            if (tbl.Rows.Count > 0)
                                SearchList = tbl;

                            else
                            {
                                SearchList = null;
                                this.ErrorMessage = "指定された条件の請求データはありません。";
                            }
                        }
                        break;

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

            if (sp請求データ一覧.Rows.Count == 0)
            {
                this.ErrorMessage = "指定された条件の請求データはありません。";
                MessageBox.Show("指定された条件の請求データはありません。");
                return;
            }

            int p会社コード;
            if (!int.TryParse(MyCompany.Text1, out p会社コード))
            {
                ErrorMessage = "自社コードが設定されていません。";
                MessageBox.Show("入力エラーがあります。");
                return;

            }

            int? p作成年月;
            DateTime p作成年月日;

            if (!DateTime.TryParse(CreateYearMonth.Text, out p作成年月日))
            {
                ErrorMessage = "作成年月の内容が正しくありません。";
                MessageBox.Show("入力エラーがあります。");
                return;
            }
            p作成年月 = p作成年月日.Year * 100 + p作成年月日.Month;

            // リスト内容の入力検証
            if (!isListValidation())
            {
                MessageBox.Show("入力エラーがあります。");
                return;
            }

            if (MessageBox.Show("請求締集計処理を実行しますか？",
                    "集計実行確認", MessageBoxButton.YesNo, MessageBoxImage.Question,
                    MessageBoxResult.Yes) == MessageBoxResult.No)
                return;

            DataSet ds = new DataSet();
            ds.Tables.Add(SearchList.Copy());

            try
            {
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        BILLING_AGGREGATE,
                        ds,
                        p会社コード,
                        p作成年月,
                        ccfg.ユーザID));

                base.SetBusyForInput();

            }
            catch (Exception)
            {
                base.SetFreeForInput();
                return;
            }

        }

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

        #region << 機能処理関連 >>

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void ScreenClear()
        {
            this.MaintenanceMode = null;
            this.sp請求データ一覧.RowCount = 0;

            ResetAllValidation();
            SetFocusToTopControl();

        }

        /// <summary>
        /// 検索条件の入力検証をおこなう
        /// </summary>
        /// <returns></returns>
        private bool isSearchValid()
        {
            DateTime p作成年月日;

            if (string.IsNullOrWhiteSpace(this.MyCompany.Text1))
            {
                ErrorMessage = "自社コードが設定されていません。";
                MessageBox.Show("入力エラーがあります。");
                return false;
            }

            if (string.IsNullOrWhiteSpace(this.CreateYearMonth.Text))
            {
                ErrorMessage = "作成年月が設定されていません。";
                MessageBox.Show("入力エラーがあります。");
                return false;
            }
            else
            {
                string workDate = string.Format("{0}/01", this.CreateYearMonth.Text);
                if (!DateTime.TryParse(workDate, out p作成年月日))
                {
                    ErrorMessage = "作成年月の入力内容に誤りがあります。";
                    MessageBox.Show("入力エラーがあります。");
                    return false;
                }

            }

            return true;

        }

        /// <summary>
        /// 集計開始時の入力検証をおこなう
        /// </summary>
        /// <returns></returns>
        private bool isListValidation()
        {
            bool isResult = false;
            bool isDetailErr = false;
            int rIdx = 0;

            foreach (DataRow row in SearchList.Rows)
            {
                // 削除行は検証対象外
                if (row.RowState == DataRowState.Deleted)
                    continue;

                // 締日作成
                int ival;
                DateTime? closingDate = null;
                if (int.TryParse(this.CreateYearMonth.Text.Replace("/", ""), out ival))
                {
                    int year = ival / 100;
                    int month = ival % 100;
                    int lastDay = int.Parse(row["締日"].ToString());

                    if (lastDay == 31)
                        lastDay = DateTime.DaysInMonth(year, month);

                    try
                    {
                        closingDate = new DateTime(year, month, lastDay);

                    }
                    catch
                    {
                        // 月末指定ではない(30)でその日が無い(2月など)場合の為、
                        // エラーをキャッチして月末を設定する
                        closingDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

                    }

                }

                // エラー情報をクリア
                sp請求データ一覧.Rows[rIdx].ValidationErrors.Clear();

                string msg = string.Empty;
                #region 【１回目】

                if (string.IsNullOrWhiteSpace(row["開始日付1"].ToString()))
                {
                    isDetailErr = true;
                    msg = "１回目の開始日付が設定されていません。";
                    sp請求データ一覧.Rows[rIdx]
                        .ValidationErrors.Add(new SpreadValidationError(msg, null, rIdx, GridColumnsMapping.期間開始1.GetHashCode()));

                }
                else if (closingDate != null && DateTime.Parse(row["開始日付1"].ToString()) >= closingDate)
                {
                    isDetailErr = true;
                    msg = "１回目の開始日付が締日を超えています。";
                    sp請求データ一覧.Rows[rIdx]
                        .ValidationErrors.Add(new SpreadValidationError(msg, null, rIdx, GridColumnsMapping.期間開始1.GetHashCode()));

                }

                if(string.IsNullOrWhiteSpace(row["終了日付1"].ToString()))
                {
                    isDetailErr = true;
                    msg = "１回目の終了日付が設定されていません。";
                    sp請求データ一覧.Rows[rIdx]
                        .ValidationErrors.Add(new SpreadValidationError(msg, null, rIdx, GridColumnsMapping.期間終了1.GetHashCode()));

                }
                else if (closingDate != null && DateTime.Parse(row["終了日付1"].ToString()) >= closingDate)
                {
                    isDetailErr = true;
                    msg = "１回目の終了日付が締日を超えています。";
                    sp請求データ一覧.Rows[rIdx]
                        .ValidationErrors.Add(new SpreadValidationError(msg, null, rIdx, GridColumnsMapping.期間終了1.GetHashCode()));

                }

                #endregion

                #region 【２回目】

                if (string.IsNullOrWhiteSpace(row["開始日付2"].ToString()) && !string.IsNullOrWhiteSpace(row["終了日付2"].ToString()))
                {
                    isDetailErr = true;
                    msg = "２回目の開始日付が設定されていません。";
                    sp請求データ一覧.Rows[rIdx]
                        .ValidationErrors.Add(new SpreadValidationError(msg, null, rIdx, GridColumnsMapping.期間開始2.GetHashCode()));

                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(row["開始日付2"].ToString()))
                    {
                        // １回目の終了直後の日付かどうかチェック
                        DateTime date1, date2;
                        if (DateTime.TryParse(row["終了日付1"].ToString(), out date1) && DateTime.TryParse(row["開始日付2"].ToString(), out date2))
                        {
                            if (date1.AddDays(1) != date2)
                            {
                                isDetailErr = true;
                                msg = "２回目の開始日付が正しくありません。";
                                sp請求データ一覧.Rows[rIdx]
                                    .ValidationErrors.Add(new SpreadValidationError(msg, null, rIdx, GridColumnsMapping.期間開始2.GetHashCode()));

                            }
                        }

                        if (closingDate != null && DateTime.Parse(row["開始日付2"].ToString()) >= closingDate)
                        {
                            isDetailErr = true;
                            msg = "２回目の開始日付が締日を超えています。";
                            sp請求データ一覧.Rows[rIdx]
                                .ValidationErrors.Add(new SpreadValidationError(msg, null, rIdx, GridColumnsMapping.期間開始2.GetHashCode()));

                        }

                    }

                }

                if (string.IsNullOrWhiteSpace(row["終了日付2"].ToString()) && !string.IsNullOrWhiteSpace(row["開始日付2"].ToString()))
                {
                    isDetailErr = true;
                    msg = "２回目の終了日付が設定されていません。";
                    sp請求データ一覧.Rows[rIdx]
                        .ValidationErrors.Add(new SpreadValidationError(msg, null, rIdx, GridColumnsMapping.期間終了2.GetHashCode()));

                }
                else if (!string.IsNullOrWhiteSpace(row["終了日付2"].ToString()))
                {
                    if (closingDate != null && DateTime.Parse(row["終了日付2"].ToString()) >= closingDate)
                    {
                        isDetailErr = true;
                        msg = "２回目の終了日付が締日を超えています。";
                        sp請求データ一覧.Rows[rIdx]
                            .ValidationErrors.Add(new SpreadValidationError(msg, null, rIdx, GridColumnsMapping.期間終了2.GetHashCode()));

                    }

                }
                #endregion

                #region 【３回目】

                if (string.IsNullOrWhiteSpace(row["開始日付3"].ToString()) && !string.IsNullOrWhiteSpace(row["終了日付3"].ToString()))
                {
                    isDetailErr = true;
                    msg = "３回目の開始日付が設定されていません。";
                    sp請求データ一覧.Rows[rIdx]
                        .ValidationErrors.Add(new SpreadValidationError(msg, null, rIdx, GridColumnsMapping.期間開始3.GetHashCode()));

                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(row["開始日付3"].ToString()))
                    {
                        // ２回目の終了直後の日付かどうかチェック
                        DateTime date1, date2;
                        if (DateTime.TryParse(row["終了日付2"].ToString(), out date1) && DateTime.TryParse(row["開始日付3"].ToString(), out date2))
                        {
                            if (date1.AddDays(1) != date2)
                            {
                                isDetailErr = true;
                                msg = "３回目の開始日付が正しくありません。";
                                sp請求データ一覧.Rows[rIdx]
                                    .ValidationErrors.Add(new SpreadValidationError(msg, null, rIdx, GridColumnsMapping.期間開始3.GetHashCode()));

                            }
                        }
                        
                        if (closingDate != null && DateTime.Parse(row["開始日付3"].ToString()) >= closingDate)
                        {
                            isDetailErr = true;
                            msg = "３回目の開始日付が締日を超えています。";
                            sp請求データ一覧.Rows[rIdx]
                                .ValidationErrors.Add(new SpreadValidationError(msg, null, rIdx, GridColumnsMapping.期間開始3.GetHashCode()));

                        }

                    }

                }

                if (string.IsNullOrWhiteSpace(row["終了日付3"].ToString()) && !string.IsNullOrWhiteSpace(row["開始日付3"].ToString()))
                {
                    isDetailErr = true;
                    msg = "３回目の終了日付が設定されていません。";
                    sp請求データ一覧.Rows[rIdx]
                        .ValidationErrors.Add(new SpreadValidationError(msg, null, rIdx, GridColumnsMapping.期間終了3.GetHashCode()));

                }
                else if (!string.IsNullOrWhiteSpace(row["終了日付3"].ToString()))
                {
                    if (closingDate != null && DateTime.Parse(row["終了日付3"].ToString()) >= closingDate)
                    {
                        isDetailErr = true;
                        msg = "３回目の終了日付が締日を超えています。";
                        sp請求データ一覧.Rows[rIdx]
                            .ValidationErrors.Add(new SpreadValidationError(msg, null, rIdx, GridColumnsMapping.期間終了3.GetHashCode()));

                    }

                }

                #endregion

                rIdx++;

            }

            if (isDetailErr)
                return isResult;

            isResult = true;

            return isResult;

        }

        #endregion

        #region << コントロールイベント群 >>

        /// <summary>
        /// 検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.CheckAllValidation() != true)
                {
                    MessageBox.Show("入力エラーがあります。");
                    return;
                }

                if (!isSearchValid())
                    return;

                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        GET_LIST_SEARCH,
                        MyCompany.Text1,
                        CreateYearMonth.Text,
                        ClosingDate.Text,
                        Customer.Text1,
                        Customer.Text2));

            }
            catch (Exception)
            {
                return;
            }

        }


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
            sp請求データ一覧.InputBindings.Clear();

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            frmcfg.spConfig = AppCommon.SaveSpConfig(this.sp請求データ一覧);
            ucfg.SetConfigValue(frmcfg);

        }
        #endregion

        #endregion

        #region ?????
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var ctl = sender as Button;
            if (ctl != null)
                ctl.IsEnabled = true;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColumnReset_Click(object sender, RoutedEventArgs e)
        {
            AppCommon.LoadSpConfig(this.sp請求データ一覧, this.spConfig);
            ScreenClear();

        }
        #endregion

        #region << グリッドイベント群 >>

        string CellName = string.Empty;
        string CellText = string.Empty;
        private void sp請求データ一覧_CellEditEnding(object sender, SpreadCellEditEndingEventArgs e)
        {

            if (e.EditAction == SpreadEditAction.Cancel)
            {
                return;
            }
            CellName = e.CellPosition.ColumnName;
            CellText = sp請求データ一覧.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
        }

        private void sp請求データ一覧_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
        {

            if (e.EditAction == SpreadEditAction.Cancel)
                return;

            var gcsp = (sender as GcSpreadGrid);
            if (gcsp == null)
                return;

            try
            {
                string cname = e.CellPosition.ColumnName;
                string ctext = sp請求データ一覧.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
                ctext = ctext == null ? string.Empty : ctext;
                if (cname == CellName && ctext == CellText)
                {
                    // セルの値が変化していなければ何もしない
                    return;
                }

                var row = gcsp.Rows[e.CellPosition.Row];
                object val = row.Cells[e.CellPosition.Column].Value;
                val = val == null ? "" : val;
                if (cname.Contains("開始日付") == true)
                {
                    AppCommon.SpreadYMDCellCheck(sender, e, this._originalText);

                    //cname = cname.Replace("年月日", "日付");

                    DateTime dt;
                    if (DateTime.TryParse(row.Cells[e.CellPosition.Column].Text, out dt) == true)
                    {
                        val = dt;
                    }
                    else
                    {
                        this.ErrorMessage = "正しい日付を入力してください。";
                        return;
                    }
                }
                if (cname.Contains("終了日付") == true)
                {
                    AppCommon.SpreadYMDCellCheck(sender, e, this._originalText);

                    //cname = cname.Replace("年月日", "日付");

                    DateTime dt;
                    if (DateTime.TryParse(row.Cells[e.CellPosition.Column].Text, out dt) == true)
                    {
                        val = dt;
                    }
                    else
                    {
                        this.ErrorMessage = "正しい日付を入力してください。";
                        return;
                    }
                }


            }
            //catch (Exception ex)
            catch
            {
                this.ErrorMessage = "入力内容が不正です。";
            }
        }

        /// <summary>
        /// グリッド上でキーが押される時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sp請求データ一覧_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && !sp請求データ一覧.ActiveCell.IsEditing)
                e.Handled = true;

        }

        #endregion

    }

}
