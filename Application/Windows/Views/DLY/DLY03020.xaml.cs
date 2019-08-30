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
    /// 売上(返品)入力
    /// </summary>
    public partial class DLY03020 : RibbonWindowViewBase
    {
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
        public class ConfigDLY03020 : FormConfigBase
        {
		}

        /// ※ 必ず public で定義する。
        public ConfigDLY03020 frmcfg = null;

        #endregion

		#region 定数定義

        #region サービスアクセス定義
        /// <summary>売上返品情報取得</summary>
		private const string T02_GetData = "T02_GetReturnsData";
        /// <summary>売上返品情報更新</summary>
        private const string T02_Update = "T02_ReturnsUpdate";
        /// <summary>売上返品情報削除</summary>
        private const string T02_Delete = "T02_Delete";
        #endregion

        #region テーブル名定義
        /// <summary>売上ヘッダ テーブル名</summary>
        private const string T02_HEADER_TABLE_NAME = "T02_URHD";
        /// <summary>売上明細 テーブル名</summary>
        private const string T02_DETAIL_TABLE_NAME = "T02_URDTL";
        /// <summary>消費税 テーブル名</summary>
        private const string M73_ZEI_TABLE_NAME = "M73_ZEI";
        /// <summary>自社 テーブル名</summary>
        private const string M70_JIS_TABLE_NAME = "M70_JIS";
        #endregion

        /// <summary>金額フォーマット定義</summary>
        private const string PRICE_FORMAT_STRING = "{0:#,0}";

        #endregion

        #region 列挙型定義

        /// <summary>
        /// データグリッドの列定義
        /// </summary>
        private enum GridColumnsMapping : int
        {
            自社品番 = 0,
            得意先品番 = 1,
            自社品名 = 2,
            色コード = 3,
            色名称 = 4,
            賞味期限 = 5,
            数量 = 6,
            単位 = 7,
            単価 = 8,
            金額 = 9,
            税区分 = 10,                        // No-94 Add
            摘要 = 11,
            マルセン仕入 = 12,
            品番コード = 13,
            消費税区分 = 14,
            商品分類 = 15
        }

        /// <summary>
        /// 自社区分 内包データ
        /// </summary>
        private enum 自社販社区分 : int
        {
            自社 = 0,
            販社 = 1
        }

        /// <summary>
        /// 商品分類 内包データ
        /// </summary>
        private enum 商品分類 : int
        {
            食品 = 1,
            繊維 = 2,
            その他 = 3
        }

        private enum 消費税区分 : int
        {
            通常税率 = 0,
            軽減税率 = 1,
            非課税 = 2
        }

        #endregion

        #region バインディングデータ

        /// <summary>売上ヘッダ情報</summary>
        private DataRow _searchHeader;
        public DataRow SearchHeader
        {
            get { return _searchHeader; }
            set
            {
                _searchHeader = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>売上明細情報</summary>
        private DataTable _searchDetail;
        public DataTable SearchDetail
        {
            get { return _searchDetail; }
            set
            {
                _searchDetail = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 検索された自社区分(0:自社、1:販社)
        /// </summary>
        private int _自社区分;

        #endregion

        #region << クラス変数定義 >>

        /// <summary>グリッドコントローラ</summary>
        GcSpreadGridController gridCtl;

        /// <summary>消費税計算</summary>
        TaxCalculator taxCalc;

        #endregion


        #region << 初期処理群 >>

        /// <summary>
        /// 売上(返品)入力　コンストラクタ
        /// </summary>
        public DLY03020()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));

            #region "権限関係"

            // 登録ボタン設定
            if (!権限Get.Authority_Update_Button(ccfg, this.GetType().Name))
            {
                // RibbonWindowViewBaseのプロパティに設定
                DataUpdateVisible = System.Windows.Visibility.Hidden;
            }

            frmcfg = (ConfigDLY03020)ucfg.GetConfigValue(typeof(ConfigDLY03020));

            #endregion

            if (frmcfg == null)
            {
                frmcfg = new ConfigDLY03020();
                ucfg.SetConfigValue(frmcfg);
                //画面サイズをタスクバーをのぞいた状態で表示させる
                //this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;
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

            #endregion

            // 検索画面情報を設定
            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { typeof(MST01010), typeof(SCHM01_TOK) });
            base.MasterMaintenanceWindowList.Add("M09_HIN", new List<Type> { typeof(MST02010), typeof(SCHM09_HIN) });
            base.MasterMaintenanceWindowList.Add("M11_TEK", new List<Type> { typeof(MST08010), typeof(SCHM11_TEK) });
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });
            base.MasterMaintenanceWindowList.Add("M21_SYUK", new List<Type> { typeof(MST01020), typeof(SCHM21_SYUK) });
            base.MasterMaintenanceWindowList.Add("M22_SOUK", new List<Type> { typeof(MST12020), typeof(SCHM22_SOUK) });

            // コンボデータ取得
            AppCommon.SetutpComboboxList(this.cmb伝票要否, false);
            AppCommon.SetutpComboboxList(this.cmb売上区分, false);
            gridCtl = new GcSpreadGridController(gcSpreadGrid);

            ScreenClear();
            ChangeKeyItemChangeable(true);

            // ログインユーザの自社区分によりコントロール状態切換え
            this.txt自社名.Text1 = ccfg.自社コード.ToString();
            this.txt自社名.IsEnabled = ccfg.自社販社区分.Equals((int)自社販社区分.自社);

            this.txt伝票番号.Focus();

        }

        #endregion

        #region << データ受信 >>

        /// <summary>
        /// データ受信処理
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            try
            {
                this.ErrorMessage = string.Empty;
                var data = message.GetResultData();
                DataTable tbl = (data is DataTable) ? (data as DataTable) : null;

                switch (message.GetMessageName())
                {
                    case T02_GetData:
                        // 伝票検索または新規伝票の場合
                        DataSet ds = data as DataSet;
                        if (ds.Tables.Count == 0)
                        {
                            this.txt伝票番号.Focus();
                            base.ErrorMessage = "指定された伝票番号は存在しませんでした。";
                            return;
                        }
                        else
                        {
                            SetTblData(ds);
                            ChangeKeyItemChangeable(false);
                        }
                        break;

                    case T02_Update:
                        MessageBox.Show(AppConst.SUCCESS_UPDATE, "登録完了", MessageBoxButton.OK, MessageBoxImage.Information);
                        // コントロール初期化
                        ScreenClear();
                        break;

                    case T02_Delete:
                        MessageBox.Show(AppConst.SUCCESS_DELETE, "削除完了", MessageBoxButton.OK, MessageBoxImage.Information);
                        // コントロール初期化
                        ScreenClear();
                        break;

                    default:
                        break;

                }

            }
            catch
            {

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
                object elmnt = FocusManager.GetFocusedElement(this);
                var spgrid = ViewBaseCommon.FindVisualParent<GcSpreadGrid>(elmnt as Control);

                if (spgrid != null)
                {
                    #region グリッドファンクションイベント
                    if (gridCtl.ActiveColumnIndex == (int)GridColumnsMapping.摘要)
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
                else
                {
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                    // 得意先の場合は個別に処理
                    // REMARKS:消費税関連の情報を取得する為
                    var twinText = ViewBaseCommon.FindVisualParent<M01_TOK_TextBox>(elmnt as Control);
                    if (twinText == null)
                        return;

                    if (twinText.Name == this.txt得意先.Name)
                    {
                        txt得意先.OpenSearchWindow(this);
                    }
                    else if (twinText.Name == this.txt仕入先.Name)
                    {
                        txt仕入先.OpenSearchWindow(this);
                    }

                }

            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";

            }

        }
        #endregion

        #region F2 マスタ編集
        /// <summary>
        /// F02　リボン　マスタ編集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF2Key(object sender, KeyEventArgs e)
        {
            try
            {
                var elmnt = FocusManager.GetFocusedElement(this);
                var spgrid = ViewBaseCommon.FindVisualParent<GcSpreadGrid>(elmnt as Control);
                if (spgrid != null)
                {
                    #region スプレッド内のイベント処理

                    if (gridCtl.ActiveColumnIndex == (int)GridColumnsMapping.摘要)
                    {
                        // 摘要マスタ表示
                        MST08010 M11Form = new MST08010();
                        M11Form.Show(this);
                    }

                    #endregion

                }
                else
                {
                    var twinText = ViewBaseCommon.FindVisualParent<M01_TOK_TextBox>(elmnt as Control);

                    if (twinText == null)
                        ViewBaseCommon.CallMasterMainte(this.MasterMaintenanceWindowList);

                    else
                    {
                        // 取引先画面の表示
                        MST01010 mstForm = new MST01010();
                        mstForm.TORI_CODE.Text = twinText.Text1;
                        mstForm.TORI_EDA.Text = twinText.Text2;

                        mstForm.ShowDialog(this);

                    }

                }

            }
            catch (Exception ex)
            {
                appLog.Error("マスターメンテナンス画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }

        }
        #endregion

        #region F6 行削除
        /// <summary>
        /// F06　リボン　行削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF6Key(object sender, KeyEventArgs e)
        {
            if (this.MaintenanceMode == null)
                return;

            if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_EDIT)
            {
                MessageBox.Show("編集時は行の削除を行うことができません。", "削除不可", MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
        /// F09　リボン　登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		public override void OnF9Key(object sender, KeyEventArgs e)
        {
            if (MaintenanceMode == null)
                return;

            // 業務入力チェックをおこなう
            if (!isFormValidation())
                return;

            // 全項目エラーチェック
            if (!base.CheckAllValidation())
            {
                this.txt売上日.Focus();
                return;
            }

            if (MessageBox.Show(AppConst.CONFIRM_UPDATE,
                                "登録確認",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question,
                                MessageBoxResult.Yes) == MessageBoxResult.No)
                return;

            Update();

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

            var yesno = MessageBox.Show(AppConst.CONFIRM_CANCEL, "取消確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (yesno == MessageBoxResult.No)
                return;

            ScreenClear();

        }
        #endregion

        #region F11 リボン
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
					var yesno = MessageBox.Show("編集中の伝票を保存せずに終了してもよろしいですか？", "終了確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
					if (yesno == MessageBoxResult.No)
						return;

                }

                this.Close();

            }

        }
        #endregion

        #region F12 削除
        /// <summary>
        /// F12　リボン　削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF12Key(object sender, KeyEventArgs e)
        {
            if (this.MaintenanceMode == null)
                return;

            if (ccfg.自社販社区分.Equals((int)自社販社区分.販社))
            {
                MessageBox.Show("利用者が販社の為、削除する事はできません。", "操作不可", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            var yesno = MessageBox.Show("伝票を削除しますか？", "削除確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (yesno == MessageBoxResult.No)
                return;

            Delete();

        }
        #endregion

        #endregion

        #region << 検索データ設定・登録・削除処理 >>

        /// <summary>
        /// 取得内容を各コントロールに設定
        /// </summary>
        /// <param name="ds"></param>
        private void SetTblData(DataSet ds)
        {
            // 売上ヘッダ情報設定
            DataTable tblHd = ds.Tables[T02_HEADER_TABLE_NAME];
            SearchHeader = tblHd.Rows[0];
            SearchHeader.AcceptChanges();

            // 売上明細情報設定
            DataTable tblDtl = ds.Tables[T02_DETAIL_TABLE_NAME];
            SearchDetail = tblDtl;
            SearchDetail.AcceptChanges();

            // 消費税情報保持
            taxCalc = new TaxCalculator(ds.Tables[M73_ZEI_TABLE_NAME]);

            // 自社区分取得
            DataTable dtJis = ds.Tables[M70_JIS_TABLE_NAME];
            if (dtJis.Rows.Count > 0)
                _自社区分 = dtJis.Rows[0].Field<int>("自社区分");
            else
                _自社区分 = (int)自社販社区分.販社;  // データが取得できなかった場合は販社として扱う

            // データ状態から編集状態を設定
            if (bool.Parse(SearchHeader["データ状態"].ToString()))
            {
                // 取得データを元に編集する為、RowStatusをAddedとする
                foreach (DataRow row in SearchDetail.Rows)
                    row.SetAdded();

                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;

                this.cmb伝票要否.SelectedIndex = 0;

                this.txt返品日.Focus();

            }
            else
            {
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;

                // 取得明細の自社品番をロック(編集不可)に設定
                foreach (var row in gcSpreadGrid.Rows)
                {
                    row.Cells[(int)GridColumnsMapping.自社品番].Locked = true;
                    row.Cells[(int)GridColumnsMapping.得意先品番].Locked = true;
                }

                gridCtl.SetCellFocus(0, (int)GridColumnsMapping.自社品番);

            }

            // グリッド内容の再計算を実施
            summaryCalculation();

        }

        /// <summary>
        /// 売上情報の登録処理をおこなう
        /// </summary>
        private void Update()
        {
            // -- 送信用データを作成 --
            // 消費税をヘッダに設定
            SearchHeader["消費税"] = AppCommon.IntParse(this.lbl消費税.Content.ToString(), System.Globalization.NumberStyles.Number);
            // No-94 Add Start
            SearchHeader["通常税率対象金額"] = AppCommon.IntParse(this.lbl通常税率対象金額.Content.ToString(), System.Globalization.NumberStyles.Number);
            SearchHeader["軽減税率対象金額"] = AppCommon.IntParse(this.lbl軽減税率対象金額.Content.ToString(), System.Globalization.NumberStyles.Number);
            SearchHeader["通常税率消費税"] = AppCommon.IntParse(this.lbl通常税率消費税.Content.ToString(), System.Globalization.NumberStyles.Number);
            SearchHeader["軽減税率消費税"] = AppCommon.IntParse(this.lbl軽減税率消費税.Content.ToString(), System.Globalization.NumberStyles.Number);
            // No-94 Add End
            // No-95 Add Start
            SearchHeader["小計"] = AppCommon.IntParse(this.lbl小計.Content.ToString(), System.Globalization.NumberStyles.Number);
            SearchHeader["総合計"] = AppCommon.IntParse(this.lbl総合計.Content.ToString(), System.Globalization.NumberStyles.Number);
            // No-95 Add End

            // No-70 Start
            DataSet ds = new DataSet();
            ds.Tables.Add(SearchHeader.Table.Copy());
            ds.Tables.Add(SearchDetail.Copy());
            // No-70 End

            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    T02_Update,
                    new object[] {
                        //SearchDetail.DataSet,
                        ds,
                        ccfg.ユーザID
                    }));

        }

        /// <summary>
        /// 売上情報の削除処理をおこなう
        /// </summary>
        private void Delete()
        {
            // 削除処理実行
            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    T02_Delete,
                    new object[] {
                        this.txt伝票番号.Text,
                        ccfg.ユーザID
                    }));

        }

        #endregion

        #region << 入力検証処理 >>

        /// <summary>
        /// 検索項目の検証をおこなう
        /// </summary>
        /// <returns></returns>
        private bool isKeyItemValidation()
        {
            bool isResult = false;

            if (string.IsNullOrEmpty(this.txt自社名.Text1))
            {
                base.ErrorMessage = "自社名が入力されていません。";
                return isResult;
            }

            if (string.IsNullOrEmpty(this.txt伝票番号.Text) && string.IsNullOrEmpty(this.txt返品伝票番号.Text))
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

            #region 【ヘッダ】必須入力チェック

            // 伝票要否
            if (this.cmb伝票要否.SelectedValue == null)
            {
                this.cmb伝票要否.Focus();
                base.ErrorMessage = string.Format("伝票要否が選択されていません。");
                return isResult;

            }

            // 返品日
            if (string.IsNullOrEmpty(this.txt返品日.Text))
            {
                this.txt返品日.Focus();
                base.ErrorMessage = string.Format("返品日が入力されていません。");
                return isResult;

            }

            // 売上区分
            if (this.cmb売上区分.SelectedValue == null)
            {
                this.cmb売上区分.Focus();
                base.ErrorMessage = string.Format("売上区分が選択されていません。");
                return isResult;

            }

            string salesKbn = this.cmb売上区分.SelectedValue.ToString();
            if (salesKbn.Equals("3") || salesKbn.Equals("4"))
            {
                // 3：メーカー直送または4：メーカー販社商流直送の場合、仕入先は必須
                if (string.IsNullOrEmpty(this.txt仕入先.Text1) || string.IsNullOrEmpty(this.txt仕入先.Text2))
                {
                    this.txt仕入先.Focus();
                    base.ErrorMessage = string.Format("仕入先が入力されていません。");
                    return isResult;

                }

                if (!txt仕入先.CheckValidation())
                {
                    this.txt仕入先.Focus();
                    base.ErrorMessage = txt仕入先.GetValidationMessage();
                    return isResult;

                }

            }

            #endregion

            #region 【明細】入力チェック

            // 【明細】詳細データが１件もない場合はエラー
            if (SearchDetail == null || SearchDetail.Rows.Count == 0)
            {
                base.ErrorMessage = string.Format("明細情報が１件もありません。");
                this.gcSpreadGrid.Focus();
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
                    if (type.Equals((int)商品分類.食品))
                    {
                        gridCtl.AddValidationError(rIdx, (int)GridColumnsMapping.賞味期限, "商品分類が『食品』の為、賞味期限の設定が必要です。");
                        isDetailErr = true;
                    }

                }

                rIdx++;

            }

            if (isDetailErr)
                return isResult;

            #endregion

            return true;

        }

        #endregion

        #region 画面項目の初期化
        /// <summary>
        /// 画面の初期化処理をおこなう
        /// </summary>
        private void ScreenClear()
        {
            this.MaintenanceMode = null;
            if (SearchHeader != null)
                SearchHeader = null;
            if (SearchDetail != null)
            {
                SearchDetail.Clear();
                for (int i = 0; i < 10; i++)
                    SearchDetail.Rows.Add(SearchDetail.NewRow());

            }

            ChangeKeyItemChangeable(true);
            ResetAllValidation();

            this.cmb伝票要否.SelectedIndex = 0;
            this.cmb売上区分.SelectedIndex = 0;

            this.txt自社名.Text1 = ccfg.自社コード.ToString();
            this.cmb売上区分.IsEnabled = false;
            this.txt備考.Text1 = string.Empty;

            string initValue = string.Format("{0:#,0}", 0);
            this.lbl小計.Content = initValue;
            this.lbl消費税.Content = initValue;
            this.lbl総合計.Content = initValue;

            // ログインユーザの自社区分によりコントロール状態切換え
            this.txt自社名.Text1 = ccfg.自社コード.ToString();
            this.txt自社名.IsEnabled = ccfg.自社販社区分.Equals((int)自社販社区分.自社);

            this.txt伝票番号.Focus();

        }
        #endregion

        #region コントロールの入力可否変更
        /// <summary>
        /// キー項目としてマークされた項目の入力可否を切り替える
        /// </summary>
        /// <param name="flag">true:入力可、false:入力不可</param>
        private void ChangeKeyItemChangeable(bool flag)
        {
            base.ChangeKeyItemChangeable(flag);

            gridCtl.SetEnabled(!flag);

            txt返品日.Focus();

        }
        #endregion

        #region << コントロールイベント >>

        /// <summary>
        /// 伝票番号でキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt伝票番号_PreviewKeyDown(object sender, KeyEventArgs e)
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
                        T02_GetData,
                        new object[] {
                            this.txt自社名.Text1,
                            tb.Text,
                            ccfg.ユーザID
                        }));

            }

        }

        /// <summary>
        /// 取引先コード・枝番からフォーカスアウトした時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt得意先_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt得意先.Text1) && string.IsNullOrEmpty(txt得意先.Text2))
                return;

            // 消費税再計算
            summaryCalculation();

        }

        /// <summary>
        /// 得意先コードが変更された後のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt得意先_TextAfterChanged(object sender, RoutedEventArgs e)
        {
            // 明細内容・消費税の再計算を実施
            summaryCalculation();
        }

        #region Window_Closed

        /// <summary>
        /// 画面が閉じられた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            if (frmcfg == null) { frmcfg = new ConfigDLY03020(); }

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;

            ucfg.SetConfigValue(frmcfg);

        }

        #endregion

        #endregion

        #region << 消費税関連処理 >>

        /// <summary>
        /// 明細内容を集計して結果を設定する
        /// </summary>
        private void summaryCalculation()
        {
            if (SearchDetail == null)
                return;

            // 小計・消費税・総合計の再計算をおこなう
            long subTotal = SearchDetail.Select("", "", DataViewRowState.CurrentRows)
                                    .AsEnumerable()
                                    .Where(w => w.Field<int?>("金額") != null)
                                    .Select(x => x.Field<int>("金額"))
                                    .Sum();
            decimal conTax = 0;
            DateTime date = DateTime.Now;
            // No-94 Add Start
            int intTsujyo = 0;
            int intKeigen = 0;
            int intTaxTsujyo = 0;
            int intTaxKeigen = 0;
            // No-94 Add End

            if (DateTime.TryParse(txt売上日.Text, out date))
            {
                foreach (DataRow row in SearchDetail.Rows)
                {
                    if (row.RowState == DataRowState.Deleted)
                        continue;

                    // 自社品番が空値(行追加のみのデータ)は処理対象外とする
                    if (string.IsNullOrEmpty(row["自社品番"].ToString()))
                        continue;

                    int taxKbnId = txt得意先.SalesTaxId;
                    //conTax += taxCalc.CalculateTax(date, row.Field<int>("金額"), row.Field<int>("消費税区分"), taxKbnId);

                    // No-94 Mod Start
                    int intZeikbn = row.Field<int>("消費税区分");
                    int intKingakuWk = row.Field<int>("金額");
                    int intTaxWk = Decimal.ToInt32(taxCalc.CalculateTax(date, intKingakuWk, intZeikbn, taxKbnId));
                    switch (intZeikbn)
                    {
                        case (int)消費税区分.通常税率:
                            intTsujyo += intKingakuWk;
                            intTaxTsujyo += intTaxWk;
                            break;
                        case (int)消費税区分.軽減税率:
                            intKeigen += intKingakuWk;
                            intTaxKeigen += intTaxWk;
                            break;
                        case (int)消費税区分.非課税:
                        default:
                            break;
                    }
                    conTax += intTaxWk;
                    // No-94 Mod End
                }

                long total = (long)(subTotal + conTax);

                // No-94 Add Start
                lbl通常税率対象金額.Content = string.Format(PRICE_FORMAT_STRING, intTsujyo);
                lbl軽減税率対象金額.Content = string.Format(PRICE_FORMAT_STRING, intKeigen);
                lbl通常税率消費税.Content = string.Format(PRICE_FORMAT_STRING, intTaxTsujyo);
                lbl軽減税率消費税.Content = string.Format(PRICE_FORMAT_STRING, intTaxKeigen);
                // No-94 Add End

                lbl小計.Content = string.Format(PRICE_FORMAT_STRING, subTotal);
                lbl消費税.Content = string.Format(PRICE_FORMAT_STRING, conTax);
                lbl総合計.Content = string.Format(PRICE_FORMAT_STRING, total);

            }

        }

        #endregion

        #region << SpreadGridイベント処理群 >>

        /// <summary>
        /// SPREAD セル編集がコミットされた時の処理(手入力) CellEditEnadedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcSpredGrid_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
        {
            GcSpreadGrid grid = sender as GcSpreadGrid;

            if (e.EditAction == SpreadEditAction.Cancel)
                return;

            //明細行が存在しない場合は処理しない
            if (SearchDetail == null) return;
            if (SearchDetail.Select("", "", DataViewRowState.CurrentRows).Count() == 0) return;

            switch (e.CellPosition.ColumnName)
            {
                case "単価":
                case "数量":
                    // 金額の再計算
                    Row targetRow = grid.Rows[grid.ActiveRowIndex];
                    decimal cost = decimal.Parse(targetRow.Cells[GridColumnsMapping.単価.GetHashCode()].Value.ToString());
                    decimal qty = decimal.Parse(targetRow.Cells[GridColumnsMapping.数量.GetHashCode()].Value.ToString());

                    targetRow.Cells[GridColumnsMapping.金額.GetHashCode()].Value = Convert.ToInt32(decimal.Multiply(cost, qty));

                    // グリッド内容の再計算を実施
                    summaryCalculation();

                    SearchDetail.Rows[targetRow.Index].EndEdit();

                    break;

                default:
                    break;

            }

        }

        /// <summary>
        /// スプレッドグリッド上でキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcSpredGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            GcSpreadGrid grid = sender as GcSpreadGrid;

            // Delete押下時の処理
            // REMARKS:編集状態でない場合のDeleteキーは無視する
            if (e.Key == Key.Delete && !grid.Cells[gcSpreadGrid.ActiveRowIndex, grid.ActiveColumnIndex].IsEditing)
                e.Handled = true;

        }

        /// <summary>
        /// 指定セルの値を取得する
        /// </summary>
        /// <param name="rIdx">行番号</param>
        /// <param name="column">列定義</param>
        /// <returns></returns>
        private object getSpreadGridValue(int rIdx, GridColumnsMapping column)
        {
            if (gcSpreadGrid.RowCount - 1 < rIdx || rIdx < 0)
                return null;

            return gcSpreadGrid.Cells[rIdx, column.GetHashCode()].Value;

        }

        /// <summary>
        /// 指定セルの値を設定する
        /// </summary>
        /// <param name="rIdx">行番号</param>
        /// <param name="column">列定義</param>
        /// <param name="value">設定値</param>
        private void setSpreadGridValue(int rIdx, GridColumnsMapping column, object value)
        {
            if (gcSpreadGrid.RowCount - 1 < rIdx || rIdx < 0)
                return;

            gcSpreadGrid.Cells[rIdx, column.GetHashCode()].Value = value;

        }

        #endregion

    }

}
