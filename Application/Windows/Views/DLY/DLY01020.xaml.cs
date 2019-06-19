using GrapeCity.Windows.SpreadGrid;
using KyoeiSystem.Application.Windows.Views.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.Controls;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KyoeiSystem.Application.Windows.Views
{
    using DebugLog = System.Diagnostics.Debug;

    /// <summary>
	/// 仕入入力フォームクラス
	/// </summary>
    public partial class DLY01020 : WindowReportBase
    {
        #region 列挙型定義

        /// <summary>
        /// データグリッドの列定義
        /// </summary>
        private enum GridColumnsMapping : int
        {
            伝票番号 = 0,
            品番コード = 1,
            自社品番 = 2,
            自社品名 = 3,
            賞味期限 = 4,
            数量 = 5,
            単位 = 6,
            単価 = 7,
            金額 = 8,
            摘要 = 9,
            消費税区分 = 10,
            商品分類 = 11
        }

        /// <summary>
        /// 自社販社区分 内包データ
        /// </summary>
        private enum 自社販社区分 : int
        {
            自社 = 0,
            販社 = 1
        }

        /// <summary>
        /// 商品分類 商品分類
        /// </summary>
        private enum 商品分類 : int
        {
            食品 = 1,
            繊維 = 2,
            その他 = 3
        }

        #endregion

        #region 定数定義

        #region サービスアクセス定義
        /// <summary>仕入情報検索</summary>
        private const string T03_GetData = "T03_GetRtData";
        /// <summary>仕入情報更新</summary>
        private const string T03_Update = "T03_ReturnsUpdate";
        /// <summary>仕入情報削除</summary>
        private const string T03_Delete = "T03_ReturnsDelete";
        #endregion

        #region 使用テーブル名定義
        private const string HEADER_TABLE_NAME = "T03_SRHD";
        private const string DETAIL_TABLE_NAME = "T03_SRDTL";
        private const string ZEI_TABLE_NAME = "M73_ZEI";
        #endregion

        /// <summary>金額フォーマット定義</summary>
        private const string PRICE_FORMAT_STRING = "{0:#,0}";
        /// <summary>グリッドの最大行数</summary>
        private const int GRID_MAX_ROW_COUNT = 10;

        #endregion

        #region 権限関係
        public UserConfig ucfg = null;
        CommonConfig ccfg = null;
        public class ConfigDLY01020 : FormConfigBase
        {
        }

        public ConfigDLY01020 frmcfg = null;
        #endregion

        #region バインディングデータ

        /// <summary>
        /// ヘッダバインディングデータ行
        /// </summary>
        public DataRow _SearchHeader;
        public DataRow SearchHeader
        {
            get { return _SearchHeader; }
            set
            {
                _SearchHeader = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 明細バインディングデータテーブル
        /// </summary>
        public DataTable _searchDetail;
        public DataTable SearchDetail
        {
            get { return _searchDetail; }
            set
            {
                this._searchDetail = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region << クラス変数定義 >>

        /// <summary>グリッドコントローラ</summary>
        GcSpreadGridController gridCtl;

        /// <summary>消費税計算</summary>
        TaxCalculator taxCalc;

        #endregion


        #region << 画面起動初期処理 >>

        /// <summary>
		/// 仕入入力 コンストラクタ
		/// </summary>
		public DLY01020()
		{
			InitializeComponent();
			this.DataContext = this;

		}

        /// <summary>
		/// 画面が表示された後のイベント処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Window_Loaded(object sender, RoutedEventArgs e)
		{
            #region 画面初期設定(権限設定等)
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigDLY01020)ucfg.GetConfigValue(typeof(ConfigDLY01020));

            // 権限設定を呼び出す(ucfgを取得した後のに入れる)
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            // 登録ボタン設定
            if (!権限Get.Authority_Update_Button(ccfg, this.GetType().Name))
            {
                DataUpdateVisible = System.Windows.Visibility.Hidden;
            }

            if (frmcfg == null)
            {
                frmcfg = new ConfigDLY01020();
                // 画面サイズをタスクバーをのぞいた状態で表示させる
                //this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height - this.Top;
            }
            else
            {
                this.Top = frmcfg.Top;
                this.Left = frmcfg.Left;
                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;
            }

            #endregion

            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { typeof(MST01010), typeof(SCHM01_TOK) });
            base.MasterMaintenanceWindowList.Add("M09_HIN", new List<Type> { typeof(MST02010), typeof(SCHM09_HIN) });
            base.MasterMaintenanceWindowList.Add("M11_TEK", new List<Type> { typeof(MST08010), typeof(SCHM11_TEK) });
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });

            AppCommon.SetutpComboboxList(this.c仕入区分, false);
            gridCtl = new GcSpreadGridController(gcSpreadGrid);

            ScreenClear();
            ChangeKeyItemChangeable(true);

            // ログインユーザの自社区分によりコントロール状態切換え
            this.c会社名.Text1 = ccfg.自社コード.ToString();
            this.c会社名.IsEnabled = ccfg.自社販社区分.Equals((int)自社販社区分.自社);

            gridCtl.SetCellFocus(0, (int)GridColumnsMapping.自社品番);
            this.c伝票番号.Focus();

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
                var data = message.GetResultData();

                switch (message.GetMessageName())
                {
                    case T03_GetData:
                        // 伝票検索または新規伝票の場合
                        DataSet ds = data as DataSet;
                        if (ds.Tables.Count == 0)
                        {
                            this.c伝票番号.Focus();
                            base.ErrorMessage = "指定された伝票番号は存在しませんでした。";
                            return;
                        }
                        else
                        {
                            SetTblData(ds);
                            ChangeKeyItemChangeable(false);
                        }
                        break;

                    case T03_Update:
                        MessageBox.Show(AppConst.SUCCESS_UPDATE, "登録完了", MessageBoxButton.OK, MessageBoxImage.Information);
                        // コントロール初期化
                        ScreenClear();
                        break;

                    case T03_Delete:
                        MessageBox.Show(AppConst.SUCCESS_DELETE, "削除完了", MessageBoxButton.OK, MessageBoxImage.Information);
                        // コントロール初期化
                        ScreenClear();
                        break;

                    default:
                        break;

                }

            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }

        }

        /// <summary>
        /// データエラー受信メソッド
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            MessageBox.Show(ErrorMessage);
            DebugLog.WriteLine("=================================");
            DebugLog.WriteLine(message.GetParameters().GetValue(0));
            DebugLog.WriteLine("=================================");
        }

        #endregion

        #region 画面項目の初期化
        /// <summary>
        /// 画面項目の初期化をおこなう
        /// </summary>
		private void ScreenClear()
		{
            this.MaintenanceMode = null;
            if (SearchHeader != null)
                SearchHeader = null;
            if (SearchDetail != null)
            {
                SearchDetail.Clear();
                for (int i = 0; i < GRID_MAX_ROW_COUNT; i++)
                    SearchDetail.Rows.Add(SearchDetail.NewRow());

            }
            this.c備考.Text1 = string.Empty;

            string initValue = string.Format("{0:#,0}", 0);
            this.c小計.Content = initValue;
            this.c消費税.Content = initValue;
            this.c総合計.Content = initValue;

            ChangeKeyItemChangeable(true);
            ResetAllValidation();

            this.c会社名.Text1 = ccfg.自社コード.ToString();

            // ログインユーザの自社区分によりコントロール状態切換え
            this.c会社名.Text1 = ccfg.自社コード.ToString();
            this.c会社名.IsEnabled = ccfg.自社販社区分.Equals((int)自社販社区分.自社);

            this.c伝票番号.Focus();

        }
        #endregion

        #region << リボン >>

        #region F1 マスタ参照
        /// <summary>
		/// F1 リボン　マスタ参照
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnF1Key(object sender, KeyEventArgs e)
		{
            try
            {
                object elmnt = FocusManager.GetFocusedElement(this);
                var spgrid = ViewBaseCommon.FindVisualParent<GcSpreadGrid>(elmnt as Control);

                if (spgrid == null)
                {
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                }
                else
                {
                    #region スプレッド内のイベント処理

                    if (gridCtl.ActiveColumnIndex == GridColumnsMapping.摘要.GetHashCode())
                    {
                        // TODO:全角６文字を超える可能性アリ
                        SCHM11_TEK tek = new SCHM11_TEK();
                        tek.TwinTextBox = new UcLabelTwinTextBox();
                        if (tek.ShowDialog(this) == true)
                            gridCtl.SetCellValue(tek.TwinTextBox.Text2);

                    }

                    SearchDetail.Rows[gridCtl.ActiveRowIndex].EndEdit();

                    #endregion

                }

            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }

        }
        #endregion

        #region F2 マスタ入力
        /// <summary>
        /// F2　リボン　マスタ入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		public override void OnF2Key(object sender, KeyEventArgs e)
		{
            try
            {
                object elmnt = FocusManager.GetFocusedElement(this);
                var spgrid = ViewBaseCommon.FindVisualParent<GcSpreadGrid>(elmnt as Control);

                if (spgrid == null)
                    ViewBaseCommon.CallMasterMainte(this.MasterMaintenanceWindowList);

                else
                {
                    #region スプレッド内のイベント処理

                    if (gridCtl.ActiveColumnIndex == GridColumnsMapping.自社品番.GetHashCode())
                    {
                        // 品番マスタ表示
                        MST02010 M09Form = new MST02010();
                        M09Form.Show(this);

                    }
                    else if (gridCtl.ActiveColumnIndex == GridColumnsMapping.摘要.GetHashCode())
                    {
                        // 摘要マスタ表示
                        MST08010 M11Form = new MST08010();
                        M11Form.Show(this);
                    }

                    #endregion

                }

            }
            catch (Exception ex)
            {
                appLog.Error("メンテ画面起動エラー", ex);
                ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }

        }
        #endregion

        #region F6 行削除
        /// <summary>
        /// F6　リボン　行削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF6Key(object sender, KeyEventArgs e)
        {
            if (SearchDetail == null)
                return;

            if (this.MaintenanceMode == null)
                return;

            else if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_EDIT)
            {
                ErrorMessage = "編集時に行削除はおこなえません。";
                return;
            }

            if (MessageBox.Show(
                    AppConst.CONFIRM_DELETE_ROW,
                    "行削除確認",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.No) == MessageBoxResult.No)
                return;

            try
            {
                gridCtl.SpreadGrid.Rows.Remove(gridCtl.ActiveRowIndex);
            }
            catch
            {
                SearchDetail.Rows[gridCtl.ActiveRowIndex].Delete();
            }

            // グリッド内容の再計算を実施
            summaryCalculation();

        }
        #endregion

        #region F9 登録
        /// <summary>
        /// F9　リボン　登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            if (SearchDetail == null)
                return;

            // 業務入力チェックをおこなう
            if (!isFormValidation())
                return;

            // 全項目エラーチェック
            if (!base.CheckAllValidation())
            {
                this.c伝票番号.Focus();
                return;
            }

            if (MessageBox.Show(AppConst.CONFIRM_UPDATE,
                                "登録確認",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question,
                                MessageBoxResult.Yes) == MessageBoxResult.No)
                return;

            // -- 送信用データを作成 --
            // 消費税をヘッダに設定
            SearchHeader["消費税"] = AppCommon.IntParse(this.c消費税.Content.ToString(), System.Globalization.NumberStyles.Number);

            DataSet ds = new DataSet();
            ds.Tables.Add(SearchHeader.Table.Copy());
            ds.Tables.Add(SearchDetail.Copy());

            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    T03_Update,
                    new object[] {
                        ds,
                        ccfg.ユーザID
                    }));

        }
        #endregion

        #region F10 入力取消
        /// <summary>
        /// F10　リボン　入力取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		public override void OnF10Key(object sender, KeyEventArgs e)
		{
			if (this.MaintenanceMode == null)
				return;

            var yesno = MessageBox.Show("入力を取り消しますか？", "取消確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (yesno == MessageBoxResult.No)
                return;

            ScreenClear();

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
            if (this.MaintenanceMode == null)
                this.Close();

            else
            {
                if (DataUpdateVisible != Visibility.Hidden)
                {
                    var yesno = MessageBox.Show(
                            "編集中の伝票を保存せずに終了してもよろしいですか？",
                            "終了確認",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question,
                            MessageBoxResult.No);
                    if (yesno == MessageBoxResult.No)
                        return;

                }

                this.Close();

            }

        }
        #endregion

        #endregion

        #region 入力検証処理

        /// <summary>
        /// 検索項目の検証をおこなう
        /// </summary>
        /// <returns></returns>
        private bool isKeyItemValidation()
        {
            bool isResult = false;

            if (string.IsNullOrEmpty(this.c会社名.Text1))
            {
                base.ErrorMessage = "会社名が入力されていません。";
                return isResult;
            }

            if (string.IsNullOrEmpty(this.c伝票番号.Text) && string.IsNullOrEmpty(this.c返品伝票番号.Text))
            {
                base.ErrorMessage = "返品伝票番号または伝票番号を入力してください。";
                return isResult;
            }

            return isResult = true;

        }

        /// <summary>
        /// 入力内容の検証をおこなう
        /// </summary>
        /// <returns></returns>
        private bool isFormValidation()
        {
            bool isResult = false;
            // 【ヘッダ】必須入力チェック
            // 仕入日
            if (string.IsNullOrEmpty(this.c仕入日.Text))
            {
                base.ErrorMessage = string.Format("仕入日が入力されていません。");
                this.c仕入日.Focus();
                return isResult;

            }

            // 仕入区分
            if (this.c仕入区分.SelectedValue == null)
            {
                base.ErrorMessage = string.Format("仕入区分が選択されていません。");
                this.c仕入区分.Focus();
                return isResult;

            }

            // 仕入先
            if (string.IsNullOrEmpty(this.c仕入先.Text1) || string.IsNullOrEmpty(this.c仕入先.Text2))
            {
                base.ErrorMessage = string.Format("仕入先が入力されていません。");
                this.c仕入先.Focus();
                return isResult;

            }

            // 入荷先
            if (string.IsNullOrEmpty(this.c入荷先.Text1))
            {
                base.ErrorMessage = string.Format("入荷先が入力されていません。");
                this.c入荷先.Focus();
                return isResult;

            }

            // 【明細】詳細データが１件もない場合はエラー
            if (SearchDetail == null || SearchDetail.Rows.Count == 0)
            {
                base.ErrorMessage = string.Format("明細情報が１件もありません。");
                gridCtl.SpreadGrid.Focus();
                return isResult;
            }

            // 【明細】品番の商品分類が食品(1)の場合は賞味期限が必須
            int rIdx = 0;
            bool isDetailErr = false;
            foreach (DataRow row in SearchDetail.Rows)
            {
                // 削除行は検証対象外
                if (row.RowState == DataRowState.Deleted)
                    continue;

                // 追加行未入力レコードはスキップ
                if (row["品番コード"] == null || string.IsNullOrEmpty(row["品番コード"].ToString()) || row["品番コード"].ToString().Equals("0"))
                    continue;

                // エラー情報をクリア
                gridCtl.ClearValidationErrors(rIdx);

                if (string.IsNullOrEmpty(row["数量"].ToString()))
                {
                    gridCtl.AddValidationError(rIdx, (int)GridColumnsMapping.数量, "数量が入力されていません。");
                    if (!isDetailErr)
                        gridCtl.SetCellFocus(rIdx, (int)GridColumnsMapping.数量);

                    isDetailErr = true;
                }

                if (string.IsNullOrEmpty(row["単価"].ToString()))
                {
                    gridCtl.AddValidationError(rIdx, (int)GridColumnsMapping.単価, "単価が入力されていません。");
                    if (!isDetailErr)
                        gridCtl.SetCellFocus(rIdx, (int)GridColumnsMapping.単価);

                    isDetailErr = true;
                }

                int type = Convert.ToInt32(row["商品分類"]);
                DateTime date;
                if (!DateTime.TryParse(row["賞味期限"].ToString(), out date))
                {
                    // 変換に失敗かつ商品分類が「食品」の場合はエラー
                    if (type.Equals(商品分類.食品.GetHashCode()))
                    {
                        gridCtl.AddValidationError(rIdx, (int)GridColumnsMapping.賞味期限, "商品分類が『食品』の為、賞味期限の設定が必要です。");
                        isDetailErr = true;
                    }

                }

                rIdx++;

            }

            if (isDetailErr)
                return isResult;

            isResult = true;

            return isResult;

        }

        #endregion

        #region Window_Closed

        /// <summary>
        /// 画面が閉じられた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void Window_Closed(object sender, EventArgs e)
		{
            // 画面が閉じられた時、データを保持する

        }

        #endregion

        #region 検索結果データ設定
        /// <summary>
        /// 取得内容を各コントロールに設定
        /// </summary>
        /// <param name="ds"></param>
        private void SetTblData(DataSet ds)
        {
            // 仕入ヘッダ情報設定
            DataTable tblHd = ds.Tables[HEADER_TABLE_NAME];
            SearchHeader = tblHd.Rows[0];
            SearchHeader.AcceptChanges();

            // 仕入詳細情報設定
            DataTable tblDtl = ds.Tables[DETAIL_TABLE_NAME];
            SearchDetail = tblDtl;
            SearchDetail.AcceptChanges();

            // 消費税情報保持
            taxCalc = new TaxCalculator(ds.Tables[ZEI_TABLE_NAME]);

            // データ状態から編集状態を設定
            if (bool.Parse(SearchHeader["データ状態"].ToString()))
            {
                // 新規の場合は新規行として扱う
                foreach (DataRow row in SearchDetail.Rows)
                    row.SetAdded();

                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                this.c返品日.Focus();

            }
            else
            {
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;

                gridCtl.SetCellFocus(0, (int)GridColumnsMapping.自社品番);

            }

            // グリッド内容の再計算を実施
            summaryCalculation();

        }
        #endregion

        #region コントロール使用可否設定
        /// <summary>
        /// キー項目としてマークされた項目の入力可否を切り替える
        /// </summary>
        /// <param name="flag">true:入力可、false:入力不可</param>
        private void ChangeKeyItemChangeable(bool flag)
        {
            base.ChangeKeyItemChangeable(flag);

            gridCtl.SetEnabled(!flag);
            this.c仕入区分.IsEnabled = false;

        }
        #endregion

        #region << コントロールイベント >>

        /// <summary>
        /// 伝票番号でキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void c伝票番号_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                UcTextBox tb = sender as UcTextBox;

                if (string.IsNullOrEmpty(tb.Text))
                    return;

                // 検索項目検証
                if (!isKeyItemValidation())
                {
                    tb.Focus();
                    return;
                }

                // 全項目エラーチェック
                if (!base.CheckKeyItemValidation())
                {
                    tb.Focus();
                    return;
                }

                // 入力伝票番号で検索
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        T03_GetData,
                        new object[] {
                            this.c会社名.Text1,
                            tb.Text,
                            ccfg.ユーザID
                    }));

            }

        }

        /// <summary>
        /// 仕入先コードが変更された後のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void c仕入先_TextAfterChanged(object sender, RoutedEventArgs e)
        {
            // 明細内容・消費税の再計算を実施
            summaryCalculation();

        }

        #endregion

        #region << SpreadGridイベント処理群 >>

        /// <summary>
        /// SPREAD セル編集がコミットされた時の処理(手入力) CellEditEnadedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchGrid_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
        {
            GcSpreadGrid grid = sender as GcSpreadGrid;
            string targetColumn = grid.ActiveCellPosition.ColumnName;

            if (e.EditAction == SpreadEditAction.Cancel)
                return;

            switch (targetColumn)
            {
                case "単価":
                case "数量":
                    // 金額の再計算
                    decimal cost = gridCtl.GetCellValueToDecimal((int)GridColumnsMapping.単価) ?? 0;
                    decimal qty = gridCtl.GetCellValueToDecimal((int)GridColumnsMapping.数量) ?? 0;

                    gridCtl.SetCellValue((int)GridColumnsMapping.金額, Convert.ToInt32(decimal.Multiply(cost, qty)));

                    // グリッド内容の再計算を実施
                    summaryCalculation();

                    SearchDetail.Rows[gridCtl.ActiveRowIndex].EndEdit();
                    
                    break;

            }


        }

        #endregion

        #region << 消費税関連処理 >>

        /// <summary>
        /// 明細内容を集計して結果を設定する
        /// </summary>
        private void summaryCalculation()
        {
            // 小計・消費税・総合計の再計算をおこなう
            long subTotal = SearchDetail.Select("", "", DataViewRowState.CurrentRows)
                                    .AsEnumerable()
                                    .Where(w => w.Field<int?>("金額") != null)
                                    .Select(x => x.Field<int>("金額"))
                                    .Sum();
            decimal conTax = 0;
            DateTime date = DateTime.Now;

            if (DateTime.TryParse(c仕入日.Text, out date))
            {
                foreach (DataRow row in SearchDetail.Rows)
                {
                    if (row.RowState == DataRowState.Deleted)
                        continue;

                    // 自社品番が空値(行追加のみのデータ)は処理対象外とする
                    if (string.IsNullOrEmpty(row["自社品番"].ToString()))
                        continue;

                    int taxKbnId = this.c仕入先.SalesTaxId;
                    conTax += taxCalc.CalculateTax(date, row.Field<int>("金額"), row.Field<int>("消費税区分"), taxKbnId);

                }

                long total = (long)(subTotal + conTax);

                c小計.Content = string.Format(PRICE_FORMAT_STRING, subTotal);
                c消費税.Content = string.Format(PRICE_FORMAT_STRING, conTax);
                c総合計.Content = string.Format(PRICE_FORMAT_STRING, total);

            }

        }

        #endregion

    }

}
