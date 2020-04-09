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
using System.Windows.Media;

namespace KyoeiSystem.Application.Windows.Views
{
    using DebugLog = System.Diagnostics.Debug;

    /// <summary>
    /// 売上入力
    /// </summary>
    public partial class DLY03010 : RibbonWindowViewBase
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
        public class ConfigDLY03010 : FormConfigBase
        {
        }

        /// ※ 必ず public で定義する。
        public ConfigDLY03010 frmcfg = null;

        #endregion

        #region 定数定義

        #region サービスアクセス定義
        /// <summary>売上情報取得</summary>
        private const string T02_GetData = "T02_GetData";
        /// <summary>売上情報更新</summary>
        private const string T02_Update = "T02_Update";
        /// <summary>納品書売上情報更新</summary>
        private const string T02_UpdateForPrint = "T02_UpdateForPrint";

        /// <summary>売上情報削除</summary>
        private const string T02_Delete = "T02_Delete";

        /// <summary>在庫数チェック</summary>
        private const string T02_StockCheck = "T02_CheckStock";
        /// <summary>更新用_在庫数チェック</summary>
        private const string UpdateData_StockCheck = "UpdateData_CheckStock";

        /// <summary>納品書用_在庫数チェック</summary>
        private const string UpdateData_StockCheck_Print = "UpdateData_CheckStock_Print";

        /// <summary>取引先名称取得</summary>
        private const string MasterCode_UcTwinMST = "UcTwinMST";

        /// <summary>仕入先名称取得</summary>
        private const string MasterCode_Supplier = "UcSupplier";

        /// <summary>得意先品番情報取得</summary>
        private const string MasterCode_CustomerProduct = "UcCustomerProduct";

        /// <summary>納品書 出力データ取得</summary>
        private const string DLY11010_PRINTOUT = "DLY11010_Print";
        /// <summary>納品書 帳票定義パス</summary>
        private const string rptFullPathName = @"Files\DLY\DLY11010.rpt";

        /// <summary>更新用_確定済チェック</summary>
        private const string FixCheck = "TKS90010_CheckFix";
        /// <summary>納品書用_確定済チェック</summary>
        private const string FixCheckPrint = "TKS90010_CheckFix_Print";
        #endregion

        #region 使用テーブル名定義
        /// <summary>売上ヘッダ テーブル名</summary>
        private const string T02_HEADER_TABLE_NAME = "T02_URHD";
        /// <summary>売上明細 テーブル名</summary>
        private const string T02_DETAIL_TABLE_NAME = "T02_URDTL";
        /// <summary>消費税 テーブル名</summary>
        private const string M73_ZEI_TABLE_NAME = "M73_ZEI";
        /// <summary>自社 テーブル名</summary>
        private const string M70_JIS_TABLE_NAME = "M70_JIS";
        /// <summary>確定データ テーブル名</summary>
        private const string S11_FIX_TABLE_NAME = "S11_KAKUTEI";
        #endregion

        /// <summary>金額フォーマット定義</summary>
        private const string PRICE_FORMAT_STRING = "{0:#,0}";

        /// <summary>メーカーの売上区分</summary>
        private List<string> メーカー区分 = new List<string>() { "3", "4" };

        /// <summary>得意先名称変更コード</summary>
        private const string 得意先名称変更コード = "9999";

        #endregion

        #region 列挙型定義

        /// <summary>
        /// 売上区分
        /// </summary>
        private enum 売上区分 : int
        {
            通常売上 = 1,
            販社売上 = 2,
            メーカー直送 = 3,
            メーカー販社商流直送 = 4,
            委託売上 = 5,
            預け売上 = 6,
        }

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

        /// <summary>
        /// 消費税区分
        /// </summary>
        private enum 消費税区分 : int
        {
            通常税率 = 0,
            軽減税率 = 1,
            非課税 = 2
        }

        /// <summary>
        /// 取引区分
        /// </summary>
        private enum 取引区分 : int
        {
            得意先 = 0,
            仕入先 = 1,
            相殺 = 3
        }

        /// <summary>
        /// 確定区分
        /// </summary>
        private enum 確定区分 : int
        {
            請求 = 0,
            支払 = 1
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

        // No-56 Strat
        // 削除済みレコード情報
        public DataTable SearchDeleteDetail;
        // No-56 End

        private string _出荷先名;
        public string 出荷先名
        {
            get { return _出荷先名; }
            set { _出荷先名 = value; NotifyPropertyChanged(); }
        }

        private string _出荷元名;
        public string 出荷元名
        {
            get { return _出荷元名; }
            set { _出荷元名 = value; NotifyPropertyChanged(); }
        }

        //▼課題No298 Add Start 2019/12/20
        private int _通常税率消費税;
        public int 通常税率消費税
        {
            get { return _通常税率消費税; }
            set { _通常税率消費税 = value; NotifyPropertyChanged(); }
        }

        private int _軽減税率消費税;
        public int 軽減税率消費税
        {
            get { return _軽減税率消費税; }
            set { _軽減税率消費税 = value; NotifyPropertyChanged(); }
        }
        //▲課題No298 Add End 2019/12/20

        /// <summary>
        /// 検索された自社区分(0:自社、1:販社)
        /// </summary>
        private int _自社区分;

        /// <summary>
        /// 編集中の行番号
        /// </summary>
        private int _編集行;

        private string[] _得意先LinkItem = new[] { "0,3", "0", "", "" };
        public string[] 得意先LinkItem
        {
            get { return this._得意先LinkItem; }
            set { this._得意先LinkItem = value; NotifyPropertyChanged(); }
        }

        private string[] _仕入先LinkItem = new[] { "1,3", "0", "", "" };
        public string[] 仕入先LinkItem
        {
            get { return this._仕入先LinkItem; }
            set { this._仕入先LinkItem = value; NotifyPropertyChanged(); }
        }
        #endregion

        #region << クラス変数定義 >>

        /// <summary>グリッドコントローラ</summary>
        GcSpreadGridController gridCtl;

        /// <summary>消費税計算</summary>
        TaxCalculator taxCalc;

        /// <summary>
        /// 確定情報
        /// </summary>
        DataTable FixData;

        #endregion



        #region << 初期処理群 >>

        /// <summary>
        /// 売上入力　コンストラクタ
        /// </summary>
        public DLY03010()
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

            frmcfg = (ConfigDLY03010)ucfg.GetConfigValue(typeof(ConfigDLY03010));

            #endregion

            if (frmcfg == null)
            {
                frmcfg = new ConfigDLY03010();
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
            base.MasterMaintenanceWindowList.Add("M22_SOUK_JISC", new List<Type> { typeof(MST12020), typeof(SCHM22_SOUK) });

            // コンボデータ取得
            if (ccfg.自社販社区分 == (int)自社販社区分.販社)
                // 販社の場合
                this.cmb売上区分.ComboListingParams = "日次,売上入力,売上区分販社";

            AppCommon.SetutpComboboxList(this.cmb伝票要否, false);
            AppCommon.SetutpComboboxList(this.cmb売上区分, false);
            gridCtl = new GcSpreadGridController(gcSpreadGrid);

            this.cmb売上区分.SelectedIndex = 0;

            ScreenClear();
            ChangeKeyItemChangeable(true);

            // ログインユーザの自社区分によりコントロール状態切換え
            this.txt自社名.Text1 = ccfg.自社コード.ToString();
            this.txt自社名.IsEnabled = ccfg.自社販社区分.Equals((int)自社販社区分.自社);

            this.txt伝票番号.Focus();

            // ログインユーザの自社コードにより参照条件の切り替え
            this.txt在庫倉庫.LinkItem = ccfg.自社販社区分.Equals((int)自社販社区分.自社) ? (int?)null : ccfg.自社コード;
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
                        if (ds != null)
                        {
                            SetTblData(ds);
                            ChangeKeyItemChangeable(false);
                            txt売上日.Focus();

                            // No.162-2 Mod Start
                            bool blnEnabled = true;
                            if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_EDIT)
                            {
                                blnEnabled = false;
                            }
                            // 入力制御
                            setDispHeaderEnabled(blnEnabled);
                            // No.162-2 Mod End

                            // 確定モード画面制御
                            setFixDisplay(this.MaintenanceMode);
                        }
                        else
                        {
                            MessageBox.Show("指定の伝票番号は登録されていません。", "伝票未登録", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            this.txt伝票番号.Focus();
                        }
                        break;

                    case T02_Update:
                        MessageBox.Show(AppConst.SUCCESS_UPDATE, "登録完了", MessageBoxButton.OK, MessageBoxImage.Information);
                        // コントロール初期化
                        ScreenClear();
                        break;

                    case T02_UpdateForPrint:
                        //登録後納品書出力
                        base.SendRequest(
                                new CommunicationObject(
                                    MessageType.UpdateData,
                                    DLY11010_PRINTOUT,
                                    new object[] {
                                        null,
                                        null,
                                        this.txt得意先.Text1,
                                        this.txt得意先.Text2,
                                        this.SearchHeader["伝票番号"].ToString(),
                                        this.SearchHeader["伝票番号"].ToString(),
                                        int.Parse(txt自社名.Text1)
                                    }));
                        break;

                    case DLY11010_PRINTOUT:
                        printPreviewDisp(tbl);
                        //納品書プレビュー画面閉じた後は表示を残しておく
                        break;

                    case T02_Delete:
                        MessageBox.Show(AppConst.SUCCESS_DELETE, "削除完了", MessageBoxButton.OK, MessageBoxImage.Information);
                        // コントロール初期化
                        ScreenClear();
                        break;

                    case MasterCode_CustomerProduct:
                        #region 自社品番・得意先品番 手入力時
                        DataTable ctbl = data as DataTable;
                        int rIdx = gcSpreadGrid.ActiveRowIndex;
                        int columnIdx = gcSpreadGrid.ActiveColumn.Index;

                        // フォーカス移動後の項目が異なる場合または編集行が異なる場合は処理しない。
                        if ((!(columnIdx == (int)GridColumnsMapping.得意先品番 || columnIdx == (int)GridColumnsMapping.自社品名)) || _編集行 != rIdx) return;

                        if (ctbl == null || ctbl.Rows.Count == 0)
                        {
                            // 対象データなしの場合
                            gridCtl.SetCellValue((int)GridColumnsMapping.品番コード, 0);
                            gridCtl.SetCellValue((int)GridColumnsMapping.自社品番, string.Empty);
                            gridCtl.SetCellValue((int)GridColumnsMapping.得意先品番, string.Empty);
                            gridCtl.SetCellValue((int)GridColumnsMapping.自社品名, string.Empty);
                            gridCtl.SetCellValue((int)GridColumnsMapping.数量, 0m);
                            gridCtl.SetCellValue((int)GridColumnsMapping.単位, string.Empty);
                            gridCtl.SetCellValue((int)GridColumnsMapping.単価, 0);
                            gridCtl.SetCellValue((int)GridColumnsMapping.金額, 0);
                            gridCtl.SetCellValue((int)GridColumnsMapping.税区分, string.Empty);      // No-94 Add
                            gridCtl.SetCellValue((int)GridColumnsMapping.消費税区分, (int)消費税区分.通常税率);
                            gridCtl.SetCellValue((int)GridColumnsMapping.商品分類, (int)商品分類.その他);
                            gridCtl.SetCellValue((int)GridColumnsMapping.色コード, string.Empty);
                            gridCtl.SetCellValue((int)GridColumnsMapping.色名称, string.Empty);

                        }
                        else if (ctbl.Rows.Count > 1)
                        {
                            // 対象データが複数ある場合
                            int cIdx = gcSpreadGrid.ActiveColumnIndex;
                            string colVal = gridCtl.GetCellValueToString((int)GridColumnsMapping.自社品番);
                            if (string.IsNullOrEmpty(colVal))
                                colVal = gridCtl.GetCellValueToString((int)GridColumnsMapping.得意先品番);

                            SCHM10_TOKHIN tokhin = new SCHM10_TOKHIN(this.txt得意先.Text1, this.txt得意先.Text2);
                            tokhin.txtCode.Text = colVal == null ? string.Empty : colVal.ToString();
                            tokhin.txtCode.IsEnabled = false;
                            tokhin.TwinTextBox = new UcLabelTwinTextBox();
                            tokhin.TwinTextBox.LinkItem = 0;    // No197 Mod
                            if (tokhin.ShowDialog(this) == true)
                            {
                                gridCtl.SetCellValue((int)GridColumnsMapping.品番コード, tokhin.SelectedDataRow["品番コード"]);
                                gridCtl.SetCellValue((int)GridColumnsMapping.自社品番, tokhin.SelectedDataRow["自社品番"]);
                                gridCtl.SetCellValue((int)GridColumnsMapping.得意先品番, tokhin.SelectedDataRow["得意先品番コード"]);
                                gridCtl.SetCellValue((int)GridColumnsMapping.自社品名, tokhin.SelectedDataRow["自社品名"]);
                                gridCtl.SetCellValue((int)GridColumnsMapping.数量, 1m);
                                gridCtl.SetCellValue((int)GridColumnsMapping.単位, tokhin.SelectedDataRow["単位"]);
                                gridCtl.SetCellValue((int)GridColumnsMapping.単価, tokhin.TwinTextBox.Text3);
                                // No-88 Mod Start
                                gridCtl.SetCellValue((int)GridColumnsMapping.金額, AppCommon.DecimalParse(tokhin.TwinTextBox.Text3));
                                // No-88 Mod End
                                gridCtl.SetCellValue((int)GridColumnsMapping.税区分, taxCalc.getTaxRareKbnString(tokhin.SelectedDataRow["消費税区分"]));      // No-94 Add
                                gridCtl.SetCellValue((int)GridColumnsMapping.消費税区分, tokhin.SelectedDataRow["消費税区分"]);
                                gridCtl.SetCellValue((int)GridColumnsMapping.商品分類, tokhin.SelectedDataRow["商品分類"]);
                                gridCtl.SetCellValue((int)GridColumnsMapping.色コード, tokhin.SelectedDataRow["自社色"]);
                                gridCtl.SetCellValue((int)GridColumnsMapping.色名称, tokhin.SelectedDataRow["色名称"]);

                                // 自社品番のセルをロック
                                gridCtl.SetCellLocked((int)GridColumnsMapping.自社品番, true);
                                gridCtl.SetCellLocked((int)GridColumnsMapping.得意先品番, true);
                                gridCtl.SetCellLocked((int)GridColumnsMapping.税区分, true);               // No-94 Add

                                summaryCalculation();

                            }

                        }
                        else
                        {
                            // 対象データありの場合
                            DataRow drow = ctbl.Rows[0];
                            gridCtl.SetCellValue((int)GridColumnsMapping.品番コード, drow["品番コード"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.自社品番, drow["自社品番"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.得意先品番, drow["得意先品番コード"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.自社品名, drow["自社品名"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.数量, 1m);
                            gridCtl.SetCellValue((int)GridColumnsMapping.単位, drow["単位"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.単価, drow["売価"]);
                            // No-55 Start
                            gridCtl.SetCellValue((int)GridColumnsMapping.金額, AppCommon.DecimalParse(drow["売価"].ToString()));
                            // No-55 End
                            gridCtl.SetCellValue((int)GridColumnsMapping.税区分, taxCalc.getTaxRareKbnString(drow["消費税区分"]));        // No-94 Add
                            gridCtl.SetCellValue((int)GridColumnsMapping.消費税区分, drow["消費税区分"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.商品分類, drow["商品分類"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.色コード, drow["自社色"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.色名称, drow["色名称"]);

                            // 自社品番のセルをロック
                            gridCtl.SetCellLocked((int)GridColumnsMapping.自社品番, true);
                            gridCtl.SetCellLocked((int)GridColumnsMapping.得意先品番, true);
                            gridCtl.SetCellLocked((int)GridColumnsMapping.税区分, true);               // No-94 Add

                            summaryCalculation();

                        }

                        #endregion

                        break;

                    case T02_StockCheck:
                        // 在庫数チェック結果受信
                        Dictionary<int, string> resultList = data as Dictionary<int, string>;

                        // 行の状態（削除）を反映させたデータを取得
                        var tempSearchDetail = SearchDetail.Copy();
                        tempSearchDetail.AcceptChanges();

                        foreach (DataRow row in tempSearchDetail.Rows)
                        {
                            int rowNum = row.Field<int>("行番号");

                            // 行インデックス取得
                            int ridx =
                                tempSearchDetail.Rows.IndexOf(tempSearchDetail.AsEnumerable()
                                    .Where(a => a.Field<int>("行番号") == rowNum)
                                    .FirstOrDefault());

                            gcSpreadGrid.Rows[ridx].ValidationErrors.Clear();

                            //メーカー以外
                            if (!メーカー区分.Contains(this.cmb売上区分.SelectedValue.ToString()) && resultList.ContainsKey(rowNum))
                            {
                                // エラー該当行にエラーメッセージ追加
                                gcSpreadGrid.Rows[ridx]
                                    .ValidationErrors.Add(new SpreadValidationError(resultList[rowNum], null, ridx, (int)GridColumnsMapping.数量));

                            }

                        }
                        break;
                    case FixCheck:
                        // 確定済チェック結果受信

                        if (IsFixCheck(tbl))
                        {
                            // 確定済エラー
                            MessageBox.Show("すでに確定済の取引先です。登録・編集できません。", "確定済取引先", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            return;
                        }

                        // 在庫ﾁｪｯｸ
                        base.SendRequest(
                           new CommunicationObject(
                               MessageType.RequestData,
                               UpdateData_StockCheck,
                               new object[] {
                            this.txt在庫倉庫.Text1,
                            SearchDetail.DataSet
                        }));
                        break;

                    case FixCheckPrint:
                        // 確定済チェック結果受信(納品書)
                        if (IsFixCheck(tbl))
                        {
                            // 確定済エラー
                            MessageBox.Show("すでに確定済の取引先です。登録・編集できません。", "確定済取引先", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                            return;
                        }

                        // 在庫ﾁｪｯｸ
                        base.SendRequest(
                           new CommunicationObject(
                               MessageType.RequestData,
                               UpdateData_StockCheck_Print,
                               new object[] {
                            this.txt在庫倉庫.Text1,
                            SearchDetail.DataSet
                        }));

                        break;

                    case UpdateData_StockCheck:
                        // 在庫数チェック結果受信
                        Dictionary<int, string> updateList = data as Dictionary<int, string>;
                        string zaiUpdateMessage = AppConst.CONFIRM_UPDATE;
                        var zaiMBImage = MessageBoxImage.Question;

                        foreach (DataRow row in SearchDetail.Select("", "", DataViewRowState.CurrentRows))
                        {
                            int rowNum = row.Field<int>("行番号");

                            //メーカー以外
                            if (!メーカー区分.Contains(this.cmb売上区分.SelectedValue.ToString()) && updateList.ContainsKey(rowNum))
                            {
                                zaiMBImage = MessageBoxImage.Warning;
                                zaiUpdateMessage = "在庫がマイナスになる品番が存在しますが、\r\n登録してもよろしいでしょうか？";
                            }
                        }

                        if (MessageBox.Show(zaiUpdateMessage,
                                "登録確認",
                                MessageBoxButton.YesNo,
                                zaiMBImage,
                                MessageBoxResult.Yes) == MessageBoxResult.No)
                            return;

                        Update();
                        break;
                    case UpdateData_StockCheck_Print:
                        // 在庫数チェック結果受信
                        updateList = data as Dictionary<int, string>;
                        zaiUpdateMessage = AppConst.CONFIRM_UPDATE;
                        zaiMBImage = MessageBoxImage.Question;

                        foreach (DataRow row in SearchDetail.Select("", "", DataViewRowState.CurrentRows))
                        {
                            int rowNum = row.Field<int>("行番号");

                            //メーカー以外
                            if (!メーカー区分.Contains(this.cmb売上区分.SelectedValue.ToString()) && updateList.ContainsKey(rowNum))
                            {
                                zaiMBImage = MessageBoxImage.Warning;
                                zaiUpdateMessage = "在庫がマイナスになる品番が存在しますが、\r\n納品書を印刷してよろしいでしょうか？";
                            }
                        }

                        if (MessageBox.Show(zaiUpdateMessage,
                                "登録確認",
                                MessageBoxButton.YesNo,
                                zaiMBImage,
                                MessageBoxResult.Yes) == MessageBoxResult.No)
                            return;

                        Update(true);
                        break;

                    case MasterCode_UcTwinMST:
                        // 得意先名称取得
                        if (tbl.Rows.Count > 0)
                        {
                            this.txt得意先.Label2Text = tbl.Rows[0].Field<string>("名称");
                            this.txt得意先.SalesTaxKbn = tbl.Rows[0].Field<int>("Ｓ支払消費税区分");
                            this.txt得意先.SalesTaxId = tbl.Rows[0].Field<int>("Ｓ税区分ID");
                            this.txt得意先.ClaimTaxKbn = tbl.Rows[0].Field<int>("Ｔ消費税区分");
                            this.txt得意先.ClaimTaxId = tbl.Rows[0].Field<int>("Ｔ税区分ID");
                        }
                        else
                        {
                            // 新規作成の場合、確定している得意先は名称を表示しない
                            if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
                            {
                                this.txt得意先.Label2Text = string.Empty;
                                this.txt得意先.SalesTaxKbn = 0;
                                this.txt得意先.SalesTaxId = 0;
                                this.txt得意先.ClaimTaxKbn = 0;
                                this.txt得意先.ClaimTaxId = 0;
                            }
                            else if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_EDIT)
                            {
                                // 編集中の場合、確定している得意先は名称を表示しない
                                int code = -1;
                                int eda = -1;
                                if (int.TryParse(this.txt得意先.Text1, out code) &&
                                    int.TryParse(this.txt得意先.Text2, out eda))
                                {
                                    // 得意先の確定データ
                                    var tokData = FixData.AsEnumerable().Where(w => w.Field<int?>("取引先コード") == code &&
                                                                            w.Field<int?>("枝番") == eda)
                                                                            .OrderByDescending(o => o.Field<DateTime?>("確定日")).ToList();

                                    if (tokData.Any())
                                    {
                                        DateTime dt;
                                        int val = -1;
                                        DateTime? fixDay = DateTime.TryParse(tokData[0].Field<DateTime?>("確定日").ToString(), out dt) ? dt : (DateTime?)null;
                                        DateTime? uriDay = DateTime.TryParse(this.txt売上日.Text, out dt) ? dt : (DateTime?)null;
                                        int n取引区分 = int.TryParse(tokData[0].Field<int>("取引区分").ToString(), out val) ? val : -1;
                                        if (n取引区分 == (int)取引区分.相殺)
                                        {
                                            // 相殺の確定日
                                            fixDay = getSousaiFixDay(tokData);
                                        }

                                        if (fixDay != null && uriDay != null && fixDay >= uriDay)
                                        {
                                            this.txt得意先.Label2Text = string.Empty;
                                            this.txt得意先.SalesTaxKbn = 0;
                                            this.txt得意先.SalesTaxId = 0;
                                            this.txt得意先.ClaimTaxKbn = 0;
                                            this.txt得意先.ClaimTaxId = 0;
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case MasterCode_Supplier:
                        // 仕入先名称取得
                        if (tbl.Rows.Count > 0)
                        {
                            this.txt仕入先.Label2Text = tbl.Rows[0].Field<string>("名称");
                            this.txt仕入先.SalesTaxKbn = tbl.Rows[0].Field<int>("Ｓ支払消費税区分");
                            this.txt仕入先.SalesTaxId = tbl.Rows[0].Field<int>("Ｓ税区分ID");
                        }
                        else
                        {
                            // 新規作成の場合、確定している得意先は名称を表示しない
                            if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
                            {
                                this.txt仕入先.Label2Text = string.Empty;
                                this.txt仕入先.SalesTaxKbn = 0;
                                this.txt仕入先.SalesTaxId = 0;
                            }
                            // 編集中に売上日変更された場合の仕入先名称
                            else if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_EDIT &&
                                        ((int)this.cmb売上区分.SelectedValue == (int)売上区分.メーカー直送 ||
                                        (int)this.cmb売上区分.SelectedValue == (int)売上区分.メーカー販社商流直送))        // 課題No.412 Mod
                            {
                                int code = -1;
                                int eda = -1;
                                if (int.TryParse(this.txt仕入先.Text1, out code) &&
                                    int.TryParse(this.txt仕入先.Text2, out eda))
                                {
                                    // 仕入先の確定データ
                                    var sirData = FixData.AsEnumerable().Where(w => w.Field<int?>("取引先コード") == code &&
                                                                            w.Field<int?>("枝番") == eda)
                                                                        .OrderByDescending(o => o.Field<DateTime?>("確定日")).ToList();

                                    if (sirData.Any())
                                    {
                                        DateTime dt;
                                        int val = -1;
                                        DateTime? fixDay = DateTime.TryParse(sirData[0].Field<DateTime?>("確定日").ToString(), out dt) ? dt : (DateTime?)null;
                                        DateTime? uriDay = DateTime.TryParse(this.txt売上日.Text, out dt) ? dt : (DateTime?)null;
                                        int n取引区分 = int.TryParse(sirData[0].Field<int?>("取引区分").ToString(), out val) ? val : -1;
                                        if (n取引区分 == (int)取引区分.相殺)
                                        {
                                            // 相殺の確定日
                                            fixDay = getSousaiFixDay(sirData);
                                        }

                                        if (fixDay != null && uriDay != null && fixDay >= uriDay)
                                        {
                                            this.txt仕入先.Label2Text = string.Empty;
                                            this.txt仕入先.SalesTaxKbn = 0;
                                            this.txt仕入先.SalesTaxId = 0;
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    default:
                        break;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                    if (gridCtl.ActiveColumnIndex == (int)GridColumnsMapping.自社品番 ||
                        gridCtl.ActiveColumnIndex == (int)GridColumnsMapping.得意先品番)
                    {
                        // 対象セルがロックされている場合は処理しない
                        if (gridCtl.CellLocked == true)
                            return;

                        if (string.IsNullOrEmpty(this.txt得意先.Text1) || string.IsNullOrEmpty(this.txt得意先.Text2))
                        {
                            txt得意先.SetFocus();
                            MessageBox.Show("得意先が設定されていない為、選択できません。", "得意先未設定", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            return;
                        }

                        // 自社品番または得意先品番の場合
                        SCHM10_TOKHIN myhin = new SCHM10_TOKHIN(this.txt得意先.Text1, this.txt得意先.Text2);
                        myhin.txtCode.Text = gridCtl.GetCellValueToString();
                        myhin.TwinTextBox = new UcLabelTwinTextBox();
                        myhin.TwinTextBox.LinkItem = 0;
                        if (myhin.ShowDialog(this) == true)
                        {
                            //入力途中のセルを未編集状態に戻す
                            spgrid.CancelCellEdit();

                            gridCtl.SetCellValue((int)GridColumnsMapping.品番コード, myhin.SelectedDataRow["品番コード"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.自社品番, myhin.SelectedDataRow["自社品番"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.得意先品番, myhin.SelectedDataRow["得意先品番コード"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.自社品名, myhin.SelectedDataRow["自社品名"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.色コード, myhin.SelectedDataRow["自社色"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.色名称, myhin.SelectedDataRow["色名称"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.数量, 1m);
                            gridCtl.SetCellValue((int)GridColumnsMapping.単位, myhin.SelectedDataRow["単位"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.単価, myhin.TwinTextBox.Text3);
                            gridCtl.SetCellValue((int)GridColumnsMapping.金額, string.IsNullOrEmpty(myhin.TwinTextBox.Text3) ? 0 : AppCommon.DecimalParse(myhin.TwinTextBox.Text3));
                            gridCtl.SetCellValue((int)GridColumnsMapping.税区分, taxCalc.getTaxRareKbnString(myhin.SelectedDataRow["消費税区分"]));       // No-94 Add
                            gridCtl.SetCellValue((int)GridColumnsMapping.消費税区分, myhin.SelectedDataRow["消費税区分"]);
                            gridCtl.SetCellValue((int)GridColumnsMapping.商品分類, myhin.SelectedDataRow["商品分類"]);

                            // 設定自社品番の編集を不可とする
                            gridCtl.SetCellLocked((int)GridColumnsMapping.自社品番, true);

                            // 設定得意先品番の編集を不可とする
                            gridCtl.SetCellLocked((int)GridColumnsMapping.得意先品番, true);

                            gridCtl.SetCellLocked((int)GridColumnsMapping.税区分, true);               // No-94 Add

                            // 集計計算をおこなう
                            summaryCalculation();

                        }

                    }
                    else if (gridCtl.ActiveColumnIndex == (int)GridColumnsMapping.摘要)
                    {
                        //入力途中のセルを空欄状態に戻す
                        spgrid.CancelCellEdit();

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
                        string[] item = { "0,3", "0", txt売上日.Text, this.txt自社名.Text1 };
                        得意先LinkItem = item;
                        txt得意先.OpenSearchWindow(this);
                    }
                    else if (twinText.Name == this.txt出荷元.Name)
                    {
                        txt出荷元.OpenSearchWindow(this);
                    }
                    else if (twinText.Name == this.txt出荷先.Name)
                    {
                        txt出荷先.OpenSearchWindow(this);
                    }
                    else if (twinText.Name == this.txt仕入先.Name)
                    {
                        // メーカー直送,販社直送は、仕入確定後、編集不可
                        if ((int)this.cmb売上区分.SelectedValue == (int)売上区分.メーカー直送 ||
                            (int)this.cmb売上区分.SelectedValue == (int)売上区分.メーカー販社商流直送)  // 課題No.412 Mod
                        {
                            string[] item = { "1,3", "0", txt売上日.Text, this.txt自社名.Text1 };
                            仕入先LinkItem = item;
                        }
                        else
                        {
                            string[] item = { "1,3", "0" };
                            仕入先LinkItem = item;
                        }
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

                    if (spgrid.ActiveColumnIndex == (int)GridColumnsMapping.自社品番 ||
                        spgrid.ActiveColumnIndex == (int)GridColumnsMapping.得意先品番)
                    {
                        // 品番マスタ表示
                        MST02010 M09Form = new MST02010();
                        M09Form.Show(this);

                    }
                    else if (spgrid.ActiveColumnIndex == (int)GridColumnsMapping.摘要)
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

        #region F3 在庫参照
        /// <summary>
        /// F03　リボン　在庫参照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF3Key(object sender, KeyEventArgs e)
        {
            if (this.MaintenanceMode == null)
                return;

            // 在庫倉庫未入力時は起動しない
            if (string.IsNullOrEmpty(txt在庫倉庫.Text1))
            {
                txt在庫倉庫.Focus();
                ErrorMessage = "在庫を参照する在庫倉庫を入力してください。";
                return;
            }

            try
            {
                int ival = -1;
                int stockpile = int.TryParse(this.txt在庫倉庫.Text1, out ival) ? ival : -1;

                // 自社未設定は無視する
                if (ival < 0)
                    return;

                // 選択中の品番コード取得
                int product = gridCtl.ActiveRowIndex < 0 ? -1 : gridCtl.GetCellValueToInt((int)GridColumnsMapping.品番コード) ?? -1;

                SCHS03_STOK stokForm = new SCHS03_STOK(stockpile);
                stokForm.productCode = product;
                stokForm.ShowDialog(this);

            }
            catch (Exception ex)
            {
                appLog.Error("在庫参照画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";

            }

        }
        #endregion

        #region F4 在庫チェック
        public override void OnF4Key(object sender, KeyEventArgs e)
        {

            // 【明細】詳細データが１件もない場合はエラー
            if (SearchDetail == null || SearchDetail.Select("", "", DataViewRowState.CurrentRows).AsEnumerable().Where(a => !string.IsNullOrEmpty(a.Field<string>("自社品番"))).Count() == 0)
            {
                this.gcSpreadGrid.Focus();
                base.ErrorMessage = string.Format("明細情報が１件もありません。");
                return;
            }

            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    T02_StockCheck,
                    new object[] {
                            this.txt在庫倉庫.Text1,
                            SearchDetail.DataSet
                        }));

        }
        #endregion

        #region F5 行追加
        /// <summary>
        /// F05　リボン　行追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            if (this.MaintenanceMode == null)
                return;

            int delRowCount = (SearchDetail.GetChanges(DataRowState.Deleted) == null) ? 0 : SearchDetail.GetChanges(DataRowState.Deleted).Rows.Count;
            if (SearchDetail.Rows.Count - delRowCount >= 10)
            {
                MessageBox.Show("明細行数が上限に達している為、これ以上追加できません。", "明細上限", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            DataRow dtlRow = SearchDetail.NewRow();
            dtlRow["伝票番号"] = this.txt伝票番号.Text;
            if (SearchDetail.Rows.Count - delRowCount > 0)
            {
                dtlRow["行番号"] = SearchDetail.Select("", "", DataViewRowState.CurrentRows).AsEnumerable().Select(a => a.Field<int>("行番号")).Max() + 1;
                dtlRow["マルセン仕入"] = _自社区分.Equals((int)自社販社区分.販社);
            }
            else
            {
                dtlRow["行番号"] = 1;
                dtlRow["マルセン仕入"] = _自社区分.Equals((int)自社販社区分.販社);
            }

            SearchDetail.Rows.Add(dtlRow);

            // 行追加後は追加行を選択させる
            // No-56 Start
            int newRowIdx = SearchDetail.Rows.Count - 1;
            // No-56 End
            // TODO:追加行が表示されるようにしたかったが追加行の上行までしか移動できない...
            gridCtl.ScrollShowCell(newRowIdx, (int)GridColumnsMapping.自社品名);

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

            if (gridCtl.ActiveRowIndex < 0)
            {
                this.ErrorMessage = "行を選択してください";
                return;
            }
            if (MessageBox.Show(
                    AppConst.CONFIRM_DELETE_ROW,
                    "行削除確認",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.No) == MessageBoxResult.No)
                return;

            // No-56 Start
            int intDelRowIdx = gridCtl.ActiveRowIndex;                              // 削除行Index

            // 選択行の削除
            // Spreadより該当行を削除する
            try
            {
                gridCtl.SpreadGrid.Rows.Remove(intDelRowIdx);
            }
            catch
            {
                // 削除処理をイベント不要のRemoveに変更する
                //SearchDetail.Rows[intDelRowIdx].Delete();
                SearchDetail.Rows.Remove(SearchDetail.Rows[intDelRowIdx]);
            }

            // 追加行の判定（登録済みレコードの場合）
            if (SearchDetail.Rows.Count > intDelRowIdx && SearchDetail.Rows[intDelRowIdx].RowState != DataRowState.Added)
            {
                // 削除行を売上明細情報（削除）(SearchDeleteDetail)に格納する
                SearchDeleteDetail.ImportRow(SearchDetail.Rows[intDelRowIdx]);
            }

            // SearchDetailより該当行を削除する
            try
            {
                if (gridCtl.SpreadGrid.Rows.Count != SearchDetail.Rows.Count)
                {
                    SearchDetail.Rows.Remove(SearchDetail.Rows[intDelRowIdx]);
                }
            }
            catch
            {
                // エラー処理なし
            }
            // No-56 End

            // グリッド内容の再計算を実施
            summaryCalculation();

        }
        #endregion

        #region F8 納品書印刷
        /// <summary>
        /// F08　リボン　納品書印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {

            gcSpreadGrid.CommitCellEdit();          // No-173 Add

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

            // 確定データチェック
            base.SendRequest(
               new CommunicationObject(
                   MessageType.RequestData,
                   FixCheckPrint,
                   new object[] {
                            this.txt自社名.Text1,
                            this.txt得意先.Text1,
                            this.txt得意先.Text2,
                            this.txt仕入先.Text1,
                            this.txt仕入先.Text2,
                        }));
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

            gcSpreadGrid.CommitCellEdit();          // No-173 Add

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

            // 確定データチェック
            base.SendRequest(
               new CommunicationObject(
                   MessageType.RequestData,
                   FixCheck,
                   new object[] {
                            this.txt自社名.Text1,
                            this.txt得意先.Text1,
                            this.txt得意先.Text2,
                            this.txt仕入先.Text1,
                            this.txt仕入先.Text2,
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

            var yesno = MessageBox.Show(AppConst.CONFIRM_CANCEL, "取消確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
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

            if (ccfg.自社販社区分.Equals(自社販社区分.販社.GetHashCode()))
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

        #region 取得内容を各コントロールに設定
        /// <summary>
        /// 取得内容を各コントロールに設定
        /// </summary>
        /// <param name="ds"></param>
        private void SetTblData(DataSet ds)
        {
            // 画面表示モードを設定
            SetDispMode(ds);

            // 変更イベントを発生させるため初期化
            SearchHeader = null;
            // 売上ヘッダ情報設定
            DataTable tblHd = ds.Tables[T02_HEADER_TABLE_NAME];
            SearchHeader = tblHd.Rows[0];
            SearchHeader.AcceptChanges();

            if (string.IsNullOrEmpty(txt自社名.Text1))
            {
                txt自社名.Text1 = tblHd.Rows[0]["会社名コード"].ToString();
            }
            // 売上明細情報設定
            DataTable tblDtl = ds.Tables[T02_DETAIL_TABLE_NAME];
            SearchDetail = tblDtl;
            SearchDetail.AcceptChanges();

            // No-56 Strat
            // 売上明細情報（削除）設定
            SearchDeleteDetail = SearchDetail.Clone();
            // No-56 End

            // 消費税情報保持
            taxCalc = new TaxCalculator(ds.Tables[M73_ZEI_TABLE_NAME]);

            // 自社区分取得
            DataTable dtJis = ds.Tables[M70_JIS_TABLE_NAME];
            if (dtJis.Rows.Count > 0)
                _自社区分 = dtJis.Rows[0].Field<int>("自社区分");
            else
                _自社区分 = (int)自社販社区分.販社;  // データが取得できなかった場合は販社として扱う

            // データ状態から編集状態を設定
            if (SearchDetail.Select("品番コード > 0").Count() == 0)
            {
                // 新規行を追加
                for (int i = 0; i < 10; i++)
                {
                    DataRow row = SearchDetail.NewRow();
                    row["伝票番号"] = AppCommon.IntParse(tblHd.Rows[0]["伝票番号"].ToString());
                    row["行番号"] = (i + 1);
                    row["マルセン仕入"] = _自社区分.Equals((int)自社販社区分.販社);

                    SearchDetail.Rows.Add(row);
                    if (SearchDetail.Rows[i].RowState == DataRowState.Unchanged)
                        SearchDetail.Rows[i].SetAdded();

                }

                this.cmb伝票要否.SelectedIndex = 0;
                this.cmb売上区分.SelectedIndex = 0;
                this.txt得意先.Text1 = string.Empty;
                this.txt得意先.Text2 = string.Empty;
                this.txt出荷先.Text1 = string.Empty;
                this.txt出荷元.Text1 = string.Empty;

                //this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                this.txt売上日.Focus();

            }
            else
            {
                // 取得明細の自社品番をロック(編集不可)に設定
                foreach (var row in gcSpreadGrid.Rows)
                {
                    row.Cells[(int)GridColumnsMapping.自社品番].Locked = true;
                    row.Cells[(int)GridColumnsMapping.得意先品番].Locked = true;
                    row.Cells[(int)GridColumnsMapping.税区分].Locked = true;
                }

                gridCtl.SetCellFocus(0, (int)GridColumnsMapping.自社品番);

            }

            //▼課題No298 Mod Start 2019/12/20
            // グリッド内容の再計算を実施
            //summaryCalculation();
            // ヘッダの情報を設定
            this.lbl消費税.Content = string.Format(PRICE_FORMAT_STRING, int.Parse(SearchHeader["消費税"].ToString()));
            this.lbl通常税率対象金額.Content = string.Format(PRICE_FORMAT_STRING, int.Parse(SearchHeader["通常税率対象金額"].ToString()));
            this.lbl軽減税率対象金額.Content = string.Format(PRICE_FORMAT_STRING, int.Parse(SearchHeader["軽減税率対象金額"].ToString()));
            this.txb通常税率消費税.Text = string.Format(PRICE_FORMAT_STRING, int.Parse(SearchHeader["通常税率消費税"].ToString()));
            this.txb軽減税率消費税.Text = string.Format(PRICE_FORMAT_STRING, int.Parse(SearchHeader["軽減税率消費税"].ToString()));
            this.lbl小計.Content = string.Format(PRICE_FORMAT_STRING, int.Parse(SearchHeader["小計"].ToString()));
            this.lbl総合計.Content = string.Format(PRICE_FORMAT_STRING, int.Parse(SearchHeader["総合計"].ToString()));
            //▲課題No298 Mod End 2019/12/20

        }
        #endregion

        #region 売上情報の登録処理をおこなう
        /// <summary>
        /// 売上情報の登録処理をおこなう
        /// </summary>
        private void Update(bool pPrintFlg = false)
        {
            // -- 送信用データを作成 --
            // 消費税をヘッダに設定
            SearchHeader["消費税"] = AppCommon.IntParse(this.lbl消費税.Content.ToString(), System.Globalization.NumberStyles.Number);
            // No-94 Add Start
            SearchHeader["通常税率対象金額"] = AppCommon.IntParse(this.lbl通常税率対象金額.Content.ToString(), System.Globalization.NumberStyles.Number);
            SearchHeader["軽減税率対象金額"] = AppCommon.IntParse(this.lbl軽減税率対象金額.Content.ToString(), System.Globalization.NumberStyles.Number);
            //▼課題No298 Mod Start 2019/12/20
            SearchHeader["通常税率消費税"] = AppCommon.IntParse(this.txb通常税率消費税.Text, System.Globalization.NumberStyles.Number);
            SearchHeader["軽減税率消費税"] = AppCommon.IntParse(this.txb軽減税率消費税.Text, System.Globalization.NumberStyles.Number);
            //▲課題No298 Mod End 2019/12/20
            // No-94 Add End
            // No-95 Add Start
            SearchHeader["小計"] = AppCommon.IntParse(this.lbl小計.Content.ToString(), System.Globalization.NumberStyles.Number);
            SearchHeader["総合計"] = AppCommon.IntParse(this.lbl総合計.Content.ToString(), System.Globalization.NumberStyles.Number);
            // No-95 Add End

            DataSet ds = new DataSet();
            ds.Tables.Add(SearchHeader.Table.Copy());

            // No-56 Start
            // 売上明細情報（削除）を売上明細情報に追加する
            // (※Rows.AddだとRowStateがAddedに変更されるため1行ずつImportする)
            if (SearchDeleteDetail.Rows.Count != 0)
            {
                for (int intIdx = 0; intIdx < SearchDeleteDetail.Rows.Count; intIdx++)
                {
                    SearchDetail.ImportRow(SearchDeleteDetail.Rows[intIdx]);
                }
            }
            // No-56 End

            ds.Tables.Add(SearchDetail.Copy());

            if (pPrintFlg)
            {
                //登録後納品書出力
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.UpdateData,
                        T02_UpdateForPrint,
                        new object[] {
                        //SearchDetail.DataSet,
                        ds,
                        ccfg.ユーザID
                        }));
            }
            else
            {
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

        }
        #endregion

        #region 売上情報の削除処理をおこなう
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

        #region 納品書のプレビュー画面を表示する
        /// <summary>
        /// 納品書のプレビュー画面を表示する
        /// </summary>
        /// <param name="tbl"></param>
        private void printPreviewDisp(DataTable tbl)
        {
            try
            {
                if (tbl.Rows.Count < 1)
                {
                    this.ErrorMessage = "対象データが存在しません。";
                    return;
                }

                // 印刷処理
                KyoeiSystem.Framework.Reports.Preview.ReportPreview view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
                view.PrinterName = frmcfg.PrinterName;
                // 第1引数　帳票タイトル
                // 第2引数　帳票ファイルPass
                // 第3以上　帳票の開始点(0で良い)
                view.MakeReport("納品書", rptFullPathName, "トレイ1");

                // 帳票ファイルに送るデータ。
                // 帳票データの列と同じ列名を保持したDataTableを引数とする
                view.SetReportData(tbl);
                view.PrinterName = frmcfg.PrinterName;
                //view.PageSettings.PaperSource.SourceName = "トレイ2";
                view.ShowPreview();
                view.Close();
                frmcfg.PrinterName = view.PrinterName;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

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

            // 売上日
            if (string.IsNullOrEmpty(this.txt売上日.Text))
            {
                this.txt売上日.Focus();
                base.ErrorMessage = string.Format("売上日が入力されていません。");
                return isResult;

            }
            else if (!this.txt売上日.CheckValidation())
            {
                this.txt売上日.Focus();
                base.ErrorMessage = string.Format("売上日の設定内容に誤りがあります。");
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

            // 得意先
            if (string.IsNullOrEmpty(this.txt得意先.Text1) || string.IsNullOrEmpty(this.txt得意先.Text2))
            {
                this.txt得意先.Focus();
                base.ErrorMessage = string.Format("得意先が入力されていません。");
                return isResult;

            }
            else if (string.IsNullOrEmpty(this.txt得意先.Label2Text))
            {
                this.txt得意先.Focus();
                base.ErrorMessage = string.Format("得意先にマスタに存在していないデータが入力されています。");
                return isResult;
            }
            else if (!this.txt得意先.CheckValidation())
            {
                this.txt得意先.Focus();
                base.ErrorMessage = string.Format("得意先の設定内容に誤りがあります。");
                return isResult;

            }

            // 在庫倉庫チェック（メーカー以外）
            if (!メーカー区分.Contains(salesKbn) && string.IsNullOrEmpty(this.txt在庫倉庫.Text1))
            {
                this.txt在庫倉庫.Focus();
                base.ErrorMessage = string.Format("在庫倉庫が入力されていません。");
                return isResult;

            }
            else if (!this.txt在庫倉庫.CheckValidation())
            {
                this.txt在庫倉庫.Focus();
                base.ErrorMessage = string.Format("在庫倉庫の設定内容に誤りがあります。");
                return isResult;

            }

            // 出荷日
            if (string.IsNullOrEmpty(this.txt出荷日.Text))
            {
                this.txt出荷日.Focus();
                base.ErrorMessage = string.Format("出荷日が入力されていません。");
                return isResult;

            }
            else if (!this.txt出荷日.CheckValidation())
            {
                this.txt出荷日.Focus();
                base.ErrorMessage = string.Format("出荷日の設定内容に誤りがあります。");
                return isResult;

            }

            // 出荷先
            if (string.IsNullOrEmpty(this.txt出荷先.Text1) || string.IsNullOrEmpty(this.txt出荷先.Text2))
            {
                this.txt出荷先.Focus();
                base.ErrorMessage = string.Format("出荷先が入力されていません。");
                return isResult;

            }
            // No.421 Add Start
            else if (this.txt出荷先.Text1 != 得意先名称変更コード && string.IsNullOrEmpty(txt出荷先名.Text))
            {
                this.txt出荷先.Focus();
                base.ErrorMessage = string.Format("出荷先にマスタに存在していないデータが入力されています。");
                return isResult;
            }
            // No.421 Add End
            else if (!this.txt出荷先.CheckValidation())
            {
                this.txt出荷先.Focus();
                base.ErrorMessage = string.Format("出荷先の設定内容に誤りがあります。");
                return isResult;

            }

            // 出荷元
            if (string.IsNullOrEmpty(this.txt出荷元.Text1) || string.IsNullOrEmpty(this.txt出荷元.Text2))
            {
                this.txt出荷元.Focus();
                base.ErrorMessage = string.Format("出荷元が入力されていません。");
                return isResult;

            }
            // No.421 Add Start
            else if (this.txt出荷元.Text1 != 得意先名称変更コード && string.IsNullOrEmpty(txt出荷元名.Text))
            {
                this.txt出荷元.Focus();
                base.ErrorMessage = string.Format("出荷元にマスタに存在していないデータが入力されています。");
                return isResult;
            }
            // No.421 Add End
            else if (!this.txt出荷元.CheckValidation())
            {
                this.txt出荷元.Focus();
                base.ErrorMessage = string.Format("出荷元の設定内容に誤りがあります。");
                return isResult;

            }


            // 4：メーカー販社商流直送で、ログインユーザーが自社以外の場合NG
            if (salesKbn.Equals("4") && ccfg.自社販社区分 != (int)自社販社区分.自社)
            {
                //　※ログインユーザーで売上区分を管理しているため、通常この処理は行われない
                this.txt自社名.Focus();
                base.ErrorMessage = string.Format("ログインユーザーがマルセンユーザーではないため、登録できません。");
                return isResult;
            }

            // 自社名コードが自社で、2：販社売上または4：メーカー販社商流直送の場合NG
            if (_自社区分 == (int)自社販社区分.自社 && (salesKbn.Equals("2") || salesKbn.Equals("4")))
            {
                this.txt自社名.Focus();
                base.ErrorMessage = string.Format("売上区分に誤りがあります。");
                return isResult;
            }

            // 自社名コードが販社で、売上区分が 2：販社売上または4：メーカー販社商流直送以外はNG
            if (_自社区分 != (int)自社販社区分.自社 && !(salesKbn.Equals("2") || salesKbn.Equals("4")))
            {
                this.txt自社名.Focus();
                base.ErrorMessage = string.Format("売上区分に誤りがあります。");
                return isResult;
            }

            if (salesKbn.Equals("3") || salesKbn.Equals("4"))
            {
                // 3：メーカー直送または4：メーカー販社商流直送の場合、仕入先は必須
                if (string.IsNullOrEmpty(this.txt仕入先.Text1) || string.IsNullOrEmpty(this.txt仕入先.Text2))
                {
                    this.txt仕入先.Focus();
                    base.ErrorMessage = string.Format("仕入先が入力されていません。");
                    return isResult;

                }
                // No.413 Add Start
                else if (string.IsNullOrEmpty(txt仕入先.Label2Text))
                {
                    this.txt仕入先.Focus();
                    base.ErrorMessage = string.Format("仕入先にマスタに存在していないデータが入力されています。");
                    return isResult;
                }
                // No.413 Add End
                else if (!txt仕入先.CheckValidation())
                {
                    this.txt仕入先.Focus();
                    base.ErrorMessage = txt仕入先.GetValidationMessage();
                    return isResult;

                }

            }


            #endregion

            #region 【明細】入力チェック

            // 現在の明細行を取得
            var CurrentDetail = SearchDetail.Select("", "", DataViewRowState.CurrentRows).AsEnumerable();

            // 【明細】詳細データが１件もない場合はエラー
            if (SearchDetail == null || CurrentDetail.Where(a => !string.IsNullOrEmpty(a.Field<string>("自社品番"))).Count() == 0)
            {
                this.gcSpreadGrid.Focus();
                base.ErrorMessage = string.Format("明細情報が１件もありません。");
                return isResult;
            }

            // 【明細】品番の商品分類が食品(1)の場合は賞味期限が必須
            int rIdx = 0;
            bool? 返品数量Flg = null;
            bool isDetailErr = false;
            foreach (DataRow row in SearchDetail.Rows)
            {
                // 削除行は検証対象外
                if (row.RowState == DataRowState.Deleted)
                    continue;

                // 追加行未入力レコードはスキップ
                if (row["品番コード"] == null || string.IsNullOrEmpty(row["品番コード"].ToString()) || row["品番コード"].ToString().Equals("0"))
                {
                    rIdx++;
                    continue;
                }

                // エラー情報をクリア
                gcSpreadGrid.Rows[rIdx].ValidationErrors.Clear();

                if (string.IsNullOrEmpty(row["数量"].ToString()))
                {
                    gcSpreadGrid.Rows[rIdx]
                        .ValidationErrors.Add(new SpreadValidationError("数量が入力されていません。", null, rIdx, GridColumnsMapping.数量.GetHashCode()));
                    if (!isDetailErr)
                        gcSpreadGrid.ActiveCellPosition = new CellPosition(rIdx, GridColumnsMapping.数量.GetHashCode());

                    isDetailErr = true;
                }

                decimal d数量;
                decimal.TryParse(row["数量"].ToString(), out d数量);

                if (d数量 != 0)
                {
                    bool wk返品数量Flg = d数量 > 0 ? false : true;

                    if (返品数量Flg == null)
                    {
                        返品数量Flg = wk返品数量Flg;
                    }
                    else if ((bool)返品数量Flg != wk返品数量Flg)
                    {
                        this.gcSpreadGrid.Focus();
                        base.ErrorMessage = string.Format("通常数量と返品数量が混在しています。");
                        return isResult;
                    }
                }

                if (string.IsNullOrEmpty(row["単価"].ToString()))
                {
                    gcSpreadGrid.Rows[rIdx]
                        .ValidationErrors.Add(new SpreadValidationError("単価が入力されていません。", null, rIdx, GridColumnsMapping.単価.GetHashCode()));
                    if (!isDetailErr)
                        gcSpreadGrid.ActiveCellPosition = new CellPosition(rIdx, GridColumnsMapping.単価.GetHashCode());

                    isDetailErr = true;
                }

                int type = Convert.ToInt32(row["商品分類"]);
                DateTime date;
                if (!DateTime.TryParse(row["賞味期限"].ToString(), out date))
                {
                    // 変換に失敗かつ商品分類が「食品」の場合はエラー
                    if (type.Equals(商品分類.食品.GetHashCode()))
                    {
                        gcSpreadGrid.Rows[rIdx]
                            .ValidationErrors.Add(new SpreadValidationError("商品分類が『食品』の為、賞味期限の設定が必要です。", null, rIdx, GridColumnsMapping.賞味期限.GetHashCode()));
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
            if (FixData != null)
                FixData = null;

            this.cmb伝票要否.SelectedIndex = 0;
            this.cmb売上区分.SelectedIndex = 0;

            this.txt納品伝票番号.Text = string.Empty;
            this.txt受注番号.Text = string.Empty;
            this.txt備考.Text1 = string.Empty;

            string initValue = string.Format("{0:#,0}", 0);
            // No-94 Add Start
            lbl通常税率対象金額.Content = initValue;
            lbl軽減税率対象金額.Content = initValue;
            //▼課題No298 Mod Start 2019/12/20
            txb通常税率消費税.Text = initValue;
            txb軽減税率消費税.Text = initValue;
            //▲課題No298 Mod End 2019/12/20
            // No-94 Add End
            this.lbl小計.Content = initValue;
            this.lbl消費税.Content = initValue;
            this.lbl総合計.Content = initValue;

            ChangeKeyItemChangeable(true);
            ResetAllValidation();

            this.txt自社名.Text1 = string.IsNullOrEmpty(txt自社名.Text1) ? ccfg.自社コード.ToString() : txt自社名.Text1;

            // ログインユーザの自社区分によりコントロール状態切換え
            this.txt自社名.IsEnabled = ccfg.自社販社区分.Equals((int)自社販社区分.自社);

            this.txt伝票番号.Focus();

        }
        #endregion

        #region 画面表示モードの設定
        /// <summary>
        /// 画面表示モードの設定
        /// </summary>
        /// <param name="ds"></param>
        private void SetDispMode(DataSet ds)
        {
            DataTable tblHd = ds.Tables[T02_HEADER_TABLE_NAME];
            DataTable tblDtl = ds.Tables[T02_DETAIL_TABLE_NAME];
            FixData = ds.Tables[S11_FIX_TABLE_NAME];

            if (tblDtl.Rows.Count == 0)
            {
                // 新規モード
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                return;
            }

            int val = -1;
            int 得意先コード = int.TryParse(tblHd.Rows[0]["得意先コード"].ToString(), out val) ? val : -1;
            int 得意先枝番 = int.TryParse(tblHd.Rows[0]["得意先枝番"].ToString(), out val) ? val : -1;
            int 仕入先コード = int.TryParse(tblHd.Rows[0]["仕入先コード"].ToString(), out val) ? val : -1;
            int 仕入先枝番 = int.TryParse(tblHd.Rows[0]["仕入先枝番"].ToString(), out val) ? val : -1;

            // 得意先確定データ
            var tokData = FixData.AsEnumerable()
                            .Where(w => w.Field<int?>("取引先コード") == 得意先コード &&
                                       w.Field<int?>("枝番") == 得意先枝番);
            // 仕入先確定データ
            var shiData = FixData.AsEnumerable()
                            .Where(w => w.Field<int?>("取引先コード") == 仕入先コード &&
                                        w.Field<int?>("枝番") == 仕入先枝番);

            // 相殺確定データ
            var soData = FixData.AsEnumerable()
                            .Where(w => w.Field<int?>("取引区分") == (int)取引区分.相殺);

            DateTime? 売上日 = tblHd.Rows[0].Field<DateTime?>("売上日");
            int n売上区分 = tblHd.Rows[0].Field<int>("売上区分");

            DateTime wkDt;
            DateTime? tok確定日 = null;
            DateTime? shr確定日 = null;
            DateTime? wk確定日 = null;

            tok確定日 = DateTime.TryParse(tokData.Where(w => w.Field<int?>("確定区分") == (int)確定区分.請求).Select(s => s.Field<DateTime?>("確定日")).FirstOrDefault().ToString(), out wkDt) ?
                                wkDt : (DateTime?)null;
            if (soData.Any())
            {
                tok確定日 = getSousaiFixDay(tokData.ToList());
                shr確定日 = getSousaiFixDay(shiData.ToList());
            }

            if (shiData.Any())
            {
                wk確定日 = DateTime.TryParse(shiData.Where(w => w.Field<int?>("確定区分") == (int)確定区分.支払).Select(s => s.Field<DateTime?>("確定日")).FirstOrDefault().ToString(), out wkDt) ?
                                wkDt : (DateTime?)null;

                shr確定日 = shr確定日 != null ?
                                shr確定日 < wk確定日 ? wk確定日 :
                                shr確定日 :
                            wk確定日;
            }

            // 得意先確定日が売上日以降の場合、編集不可
            if (tok確定日 != null && tok確定日 >= 売上日)
            {
                // 確定モード
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_FIX;
            }
            else
            {
                // 編集モード
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
            }

            // メーカー直送,販社直送は、仕入確定後、編集不可
            if (n売上区分 == (int)売上区分.メーカー直送 ||
                n売上区分 == (int)売上区分.メーカー販社商流直送)
            {
                if (shr確定日 != null && shr確定日 >= 売上日)
                {
                    // 確定モード
                    this.MaintenanceMode = AppConst.MAINTENANCEMODE_FIX;
                }
            }
        }
        #endregion

        #region 得意先(相殺)の確定日を取得する
        /// <summary>
        /// 得意先(相殺)の確定日を取得する
        /// </summary>
        /// <param name="fixDt"></param>
        /// <returns></returns>
        public DateTime? getSousaiFixDay(List<DataRow> fixData)
        {
            if (fixData.Count == 0)
            {
                return null;
            }

            // 相殺確定データ
            var soData = fixData
                            .Where(w => w.Field<int?>("取引区分") == (int)取引区分.相殺);

            if (!soData.Any())
            {
                return null;
            }

            DateTime wkDt;
            DateTime? tok確定日 = null;
            DateTime? shr確定日 = null;
            DateTime? ret確定日 = null;

            // 得意先が相殺の場合
            tok確定日 = DateTime.TryParse(soData.Where(w => w.Field<int?>("確定区分") == 0).Select(s => s.Field<DateTime?>("確定日")).FirstOrDefault().ToString(), out wkDt) ?
                            wkDt : (DateTime?)null;

            shr確定日 = DateTime.TryParse(soData.Where(w => w.Field<int?>("確定区分") == 1).Select(s => s.Field<DateTime?>("確定日")).FirstOrDefault().ToString(), out wkDt) ?
                            wkDt : (DateTime?)null;

            // 大きい日付を確定日とする
            ret確定日 = tok確定日 != null ?
                            shr確定日 != null ?
                                tok確定日 < shr確定日 ? shr確定日 : tok確定日 :
                            tok確定日 :
                         shr確定日;

            return ret確定日;
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

            this.PrevSlip.IsEnabled = true;
            this.NextSlip.IsEnabled = true;
            this.gcSpreadGrid.IsEnabled = !flag;

        }
        #endregion

        #region 画面ヘッダ部の入力制御
        // No.162-2 Add Start
        /// <summary>
        /// 画面ヘッダ部の入力設定を行う
        /// </summary>
        /// <param name="blnEnabled">true:入力可、false:入力不可</param>
        private void setDispHeaderEnabled(bool blnEnabled)
        {
            txt在庫倉庫.IsEnabled = blnEnabled;
            cmb売上区分.IsEnabled = blnEnabled;
        }
        // No.162-2 Add End
        #endregion

        #region 確定モード時の画面制御
        /// <summary>
        /// 確定モード時の画面制御
        /// </summary>
        /// <param name="Mode">画面モード</param>
        private void setFixDisplay(string Mode)
        {
            bool blnEnabled = Mode == AppConst.MAINTENANCEMODE_FIX ? true : false;

            // リボンボタン表示制御
            this.F1.Visibility = blnEnabled == true ? Visibility.Hidden : Visibility.Visible;
            this.F2.Visibility = blnEnabled == true ? Visibility.Hidden : Visibility.Visible;
            this.F3.Visibility = blnEnabled == true ? Visibility.Hidden : Visibility.Visible;
            this.F4.Visibility = blnEnabled == true ? Visibility.Hidden : Visibility.Visible;
            this.F5.Visibility = blnEnabled == true ? Visibility.Hidden : Visibility.Visible;
            this.F6.Visibility = blnEnabled == true ? Visibility.Hidden : Visibility.Visible;
            this.F8.Visibility = blnEnabled == true ? Visibility.Hidden : Visibility.Visible;
            this.F9.Visibility = blnEnabled == true ? Visibility.Hidden : Visibility.Visible;
            this.F12.Visibility = blnEnabled == true ? Visibility.Hidden : Visibility.Visible;

            // ヘッダー項目
            this.txt自社名.IsEnabled = !blnEnabled;
            this.cmb伝票要否.IsEnabled = !blnEnabled;
            this.txt売上日.IsEnabled = !blnEnabled;
            this.txt得意先.IsEnabled = !blnEnabled;
            this.txt出荷先.IsEnabled = !blnEnabled;
            this.txt出荷先名.IsEnabled = !blnEnabled;
            this.txt納品伝票番号.IsEnabled = !blnEnabled;
            this.txt出荷日.IsEnabled = !blnEnabled;
            this.txt受注番号.IsEnabled = !blnEnabled;
            this.txt仕入先.IsEnabled = !blnEnabled;
            this.txt出荷元.IsEnabled = !blnEnabled;
            this.txt出荷元名.IsEnabled = !blnEnabled;
            this.txt在庫倉庫.IsEnabled = Mode == AppConst.MAINTENANCEMODE_ADD ? true : false;
            this.cmb売上区分.IsEnabled = Mode == AppConst.MAINTENANCEMODE_ADD ? true : false;

            // 明細
            this.gcSpreadGrid.IsEnabled = !blnEnabled;
        }
        #endregion

        #region 確定済チェック
        /// <summary>
        /// 確定済チェック
        /// </summary>
        /// <param name="fixDt"></param>
        /// <returns>true:確定済/false:未確定</returns>
        private bool IsFixCheck(DataTable fixDt)
        {
            DateTime dt;
            int val = -1;
            DateTime? fixDay;       // 得意先確定日
            DateTime? uriDay;       // 売上日
            DateTime? shrFixDay;    // 仕入先確定日

            if (fixDt == null)
            {
                return false;
            }

            // 得意先の確定データ
            var tokData = fixDt.AsEnumerable().Where(w => w.Field<string>("取引先コード") == this.txt得意先.Text1 &&
                                                        w.Field<string>("枝番") == this.txt得意先.Text2)
                                                        .OrderByDescending(o => o.Field<DateTime?>("確定日")).ToList();

            if (tokData.Any())
            {
                fixDay = DateTime.TryParse(tokData[0].Field<DateTime?>("確定日").ToString(), out dt) ? dt : (DateTime?)null;
                uriDay = DateTime.TryParse(this.txt売上日.Text, out dt) ? dt : (DateTime?)null;
                int n取引区分 = int.TryParse(tokData[0].Field<int?>("取引区分").ToString(), out val) ? val : -1;
                if (n取引区分 == (int)取引区分.相殺)
                {
                    // 相殺の確定日
                    fixDay = getSousaiFixDay(tokData);
                }

                // メーカー直送,販社直送は、仕入確定後、編集不可
                if ((int)this.cmb売上区分.SelectedValue == (int)売上区分.メーカー直送 ||
                    (int)this.cmb売上区分.SelectedValue == (int)売上区分.メーカー販社商流直送)        // 課題No.412 Mod
                {
                    // 仕入先の確定データ
                    var shrData = fixDt.AsEnumerable().Where(w => w.Field<string>("取引先コード") == this.txt仕入先.Text1 &&
                                                                w.Field<string>("枝番") == this.txt仕入先.Text2)
                                                                .OrderByDescending(o => o.Field<DateTime?>("確定日")).ToList();

                    shrFixDay = DateTime.TryParse(shrData[0].Field<DateTime?>("確定日").ToString(), out dt) ? dt : (DateTime?)null;

                    if (fixDay != null && shrFixDay != null && fixDay < shrFixDay)
                    {
                        fixDay = shrFixDay;
                    }
                }

                if (fixDay != null && uriDay != null && fixDay >= uriDay)
                {
                    // 確定済エラー
                    return true;
                }
            }



            return false;
        }
        #endregion

        #region << コントロールイベント >>

        #region 伝票番号でキーが押された時のイベント処理
        /// <summary>
        /// 伝票番号でキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt伝票番号_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                // 検索項目検証
                //if (!isKeyItemValidation())
                //{
                //    this.txt伝票番号.Focus();
                //    return;
                //}

                // 全項目エラーチェック
                if (!base.CheckKeyItemValidation())
                {
                    this.txt伝票番号.Focus();
                    return;
                }

                // 入力伝票番号で検索
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        T02_GetData,
                        new object[] {
                            this.txt自社名.Text1,
                            this.txt伝票番号.Text,
                            0,
                            ccfg.ユーザID
                        }));

            }

        }
        #endregion

        #region 取引先コード・枝番からフォーカスアウトした時のイベント処理
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
        #endregion

        #region 得意先コードが変更された後のイベント処理
        /// <summary>
        /// 得意先コードが変更された後のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt得意先_TextAfterChanged(object sender, RoutedEventArgs e)
        {
            //▼課題No298 Del Start 2019/12/20
            // 明細内容・消費税の再計算を実施
            //summaryCalculation();
            //▲課題No298 Del End 2019/12/20
        }
        #endregion

        #region 出荷元コードが変更された後のイベント処理
        /// <summary>
        /// 出荷元コードが変更された後のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt出荷元_cText1Changed(object sender, RoutedEventArgs e)
        {
            // text1が"9999"の場合、出荷元名変更可能
            if (txt出荷元.Text1 == 得意先名称変更コード)
            {
                txt出荷元.Text2 = "0";
                txt出荷元名.cIsReadOnly = false;
            }
            else
            {
                txt出荷元名.Text = txt出荷元.Label2Text;
                txt出荷元名.cIsReadOnly = true;
            }
        }
        #endregion

        #region 出荷元が変更された後のイベント処理
        /// <summary>
        /// 出荷元が変更された後のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt出荷元_TextAfterChanged(object sender, RoutedEventArgs e)
        {
            txt出荷元.Label2Visibility = System.Windows.Visibility.Collapsed;

            if (txt出荷元.Text1 != 得意先名称変更コード)
            {
                txt出荷元名.Text = txt出荷元.Label2Text;
            }

            //▼課題No298 Del Start 2019/12/20
            // 明細内容・消費税の再計算を実施
            //summaryCalculation();
            //▲課題No298 Del End 2019/12/20

        }
        #endregion

        #region 出荷先コードが変更された後のイベント処理
        /// <summary>
        /// 出荷先コードが変更された後のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt出荷先_cText1Changed(object sender, RoutedEventArgs e)
        {
            // text1が"9999"の場合、出荷先名変更可能
            if (txt出荷先.Text1 == 得意先名称変更コード)
            {
                txt出荷先.Text2 = "0";
                txt出荷先名.cIsReadOnly = false;
            }
            else
            {
                txt出荷先名.Text = txt出荷先.Label2Text;
                txt出荷先名.cIsReadOnly = true;
            }
        }
        #endregion

        #region 出荷先が変更された後のイベント処理
        /// <summary>
        /// 出荷先が変更された後のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt出荷先_TextAfterChanged(object sender, RoutedEventArgs e)
        {
            txt出荷先.Label2Visibility = System.Windows.Visibility.Collapsed;

            if (txt出荷先.Text1 != 得意先名称変更コード)
            {
                txt出荷先名.Text = txt出荷先.Label2Text;
            }

            //▼課題No298 Del Start 2019/12/20
            // 明細内容・消費税の再計算を実施
            //summaryCalculation();
            //▲課題No298 Del End 2019/12/20

        }
        #endregion

        #region 売上日が変更された後のイベント処理
        // No.175-1 Add
        /// <summary>
        /// 売上日が変更された後のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt売上日_cTextChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txt売上日.Text))
            {
                if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_FIX)
                {
                    string[] item = { "0,3", "0" };
                    string[] shrItem = { "1,3", "0" };
                    得意先LinkItem = item;
                    仕入先LinkItem = shrItem;
                }
                else
                {
                    string[] tokItem = { "0,3", "0", this.txt売上日.Text, this.txt自社名.Text1 };
                    string[] shrItem = { "1,3", "0", this.txt売上日.Text, this.txt自社名.Text1 };
                    得意先LinkItem = tokItem;
                    仕入先LinkItem = shrItem;
                }

                // 得意先、仕入先を再設定
                txt得意先_cText1Changed(sender, e);
                txt仕入先_cText1Changed(sender, e);
            }

            // 明細内容・消費税の再計算を実施
            summaryCalculation();
        }
        // No.175-1 End
        #endregion

        #region 得意先が変更された時のイベント処理
        /// <summary>
        /// 得意先が変更された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt得意先_cText1Changed(object sender, RoutedEventArgs e)
        {
            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    MasterCode_UcTwinMST,
                    new object[] {
                        "M01_TOK",
                        this.txt得意先.Text1,
                        this.txt得意先.Text2,
                        this.txt得意先.LinkItem
                    }));
        }
        #endregion

        #region 仕入先が変更された時のイベント処理
        /// <summary>
        /// 仕入先が変更された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt仕入先_cText1Changed(object sender, RoutedEventArgs e)
        {
            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    MasterCode_Supplier,
                    new object[] {
                        "M01_TOK",
                        this.txt仕入先.Text1,
                        this.txt仕入先.Text2,
                        this.txt仕入先.LinkItem
                    }));
        }
        #endregion

        #region 明細番号ページングボタン押下時のイベント処理
        /// <summary>
        /// 明細番号ページングボタン押下時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PagingButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            int sendParam = btn.Name == PrevSlip.Name ? -1 : 1;

            // 検索項目検証
            if (!isKeyItemValidation())
            {
                this.txt伝票番号.Focus();
                return;
            }

            // 全項目エラーチェック
            if (!base.CheckKeyItemValidation())
            {
                this.txt伝票番号.Focus();
                return;
            }

            // 入力伝票番号で検索
            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    T02_GetData,
                    new object[] {
                            this.txt自社名.Text1,
                            this.txt伝票番号.Text,
                            sendParam,
                            ccfg.ユーザID
                        }));

        }
        #endregion

        #region 自社名が変更された時のイベント処理
        /// <summary>
        /// 自社名が変更された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt自社名_cText1Changed(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txt売上日.Text))
            {
                if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_FIX)
                {
                    string[] item = { "0,3", "0" };
                    string[] shrItem = { "1,3", "0" };
                    得意先LinkItem = item;
                    仕入先LinkItem = shrItem;
                }
                else
                {
                    string[] tokItem = { "0,3", "0", this.txt売上日.Text, this.txt自社名.Text1 };
                    string[] shrItem = { "1,3", "0", this.txt売上日.Text, this.txt自社名.Text1 };
                    得意先LinkItem = tokItem;
                    仕入先LinkItem = shrItem;
                }

                // 得意先、仕入先を再設定
                txt得意先_cText1Changed(sender, e);
                txt仕入先_cText1Changed(sender, e);
            }
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
            if (frmcfg == null) { frmcfg = new ConfigDLY03010(); }

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
            //decimal subTotal =
            //    SearchDetail.Select("", "", DataViewRowState.CurrentRows)
            //        .AsEnumerable()
            //        .Where(w => w.Field<decimal?>("金額") != null)
            //        .Select(x => x.Field<decimal>("金額"))
            //        .Sum();
            decimal conTax = 0;
            DateTime date = DateTime.Now;

            // No-94 Add Start
            int intTsujyo = 0;
            int intKeigen = 0;
            int intTaxTsujyo = 0;
            int intTaxKeigen = 0;
            int subTotal = 0;
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

                    int taxKbnId = txt得意先.ClaimTaxId;

                    // No-94 Mod Start
                    int intZeikbn = row.Field<int>("消費税区分");
                    int intKingakuWk = Decimal.ToInt32(Math.Round(row.Field<decimal>("金額"), 0, MidpointRounding.AwayFromZero));
                    int intTaxWk = Decimal.ToInt32(taxCalc.CalculateTax(date, intKingakuWk, intZeikbn, taxKbnId, txt得意先.ClaimTaxKbn));      // No.272 Mod

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
                    subTotal += intKingakuWk;
                    conTax += intTaxWk;
                    // No-94 Mod End
                }

                long total = Convert.ToInt64(subTotal + conTax);

                // No-94 Add Start
                lbl通常税率対象金額.Content = string.Format(PRICE_FORMAT_STRING, intTsujyo);
                lbl軽減税率対象金額.Content = string.Format(PRICE_FORMAT_STRING, intKeigen);
                //▼課題No298 Mod Start 2019/12/20
                txb通常税率消費税.Text = string.Format(PRICE_FORMAT_STRING, intTaxTsujyo);
                txb軽減税率消費税.Text = string.Format(PRICE_FORMAT_STRING, intTaxKeigen);
                //▲課題No298 Mod End 2019/12/20
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

            //明細行が存在しない場合は処理しない
            if (SearchDetail == null) return;
            if (SearchDetail.Select("", "", DataViewRowState.CurrentRows).Count() == 0) return;

            _編集行 = e.CellPosition.Row;

            switch (e.CellPosition.ColumnName)
            {
                case "自社品番":
                case "得意先品番コード":
                    var target = grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Value;
                    if (target == null)
                        return;

                    // 自社品番(または得意先品番)からデータを参照し、取得内容をグリッドに設定
                    base.SendRequest(
                        new CommunicationObject(
                            MessageType.RequestData,
                            MasterCode_CustomerProduct,
                            new object[] {
                                target.ToString(),
                                this.txt得意先.Text1,
                                this.txt得意先.Text2
                            }));

                    // No-57 Start
                    SearchDetail.Rows[gridCtl.ActiveRowIndex].EndEdit();
                    // No-57 End

                    break;

                case "単価":
                case "数量":
                    // 金額の再計算
                    decimal cost = gridCtl.GetCellValueToDecimal((int)GridColumnsMapping.単価) ?? 0;
                    decimal qty = gridCtl.GetCellValueToDecimal((int)GridColumnsMapping.数量) ?? 0;

                    gridCtl.SetCellValue((int)GridColumnsMapping.金額, Math.Round(decimal.Multiply(cost, qty), 0, MidpointRounding.AwayFromZero));

                    // グリッド内容の再計算を実施
                    summaryCalculation();

                    SearchDetail.Rows[gridCtl.ActiveRowIndex].EndEdit();

                    break;

                case "金額":
                    // グリッド内容の再計算を実施
                    summaryCalculation();

                    SearchDetail.Rows[gridCtl.ActiveRowIndex].EndEdit();

                    break;
                default:
                    if (gridCtl.ActiveRowIndex >= 0)
                    {
                        // EndEditが行われずに登録すると変更内容が反映されないため処理追加
                        SearchDetail.Rows[gridCtl.ActiveRowIndex].EndEdit();
                    }
                    break;

            }

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

        /// <summary>
        /// SPREAD セルが編集状態になった時の処理 EditElementShowingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcSpredGrid_EditElementShowing(object sender, EditElementShowingEventArgs e)
        {
            GcSpreadGrid grid = sender as GcSpreadGrid;
            if (grid.ActiveCell.InheritedCellType is GrapeCity.Windows.SpreadGrid.CheckBoxCellType)
            {
                // チェックボックス型セルのイベントを関連付けます。
                GrapeCity.Windows.SpreadGrid.Editors.CheckBoxEditElement gcchk = grid.EditElement as GrapeCity.Windows.SpreadGrid.Editors.CheckBoxEditElement;
                if (gcchk != null)
                {
                    gcchk.Checked += checkEdit_Checked;
                    gcchk.Unchecked += checkEdit_Unchecked;
                }
            }
        }

        /// <summary>
        /// checkEdit_Checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEdit_Checked(object sender, RoutedEventArgs e)
        {
            if (SearchDetail != null && SearchDetail.Select("", "", DataViewRowState.CurrentRows).Count() != 0 && gridCtl.ActiveRowIndex >= 0)
            {
                // EndEditが行われずに登録すると変更内容が反映されないため処理追加
                SearchDetail.Rows[gridCtl.ActiveRowIndex].EndEdit();
            }
        }

        /// <summary>
        /// checkEdit_Unchecked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEdit_Unchecked(object sender, RoutedEventArgs e)
        {

            if (SearchDetail != null && SearchDetail.Select("", "", DataViewRowState.CurrentRows).Count() != 0 && gridCtl.ActiveRowIndex >= 0)
            {
                // EndEditが行われずに登録すると変更内容が反映されないため処理追加
                SearchDetail.Rows[gridCtl.ActiveRowIndex].EndEdit();
            }
        }

        //▼課題No298 Add Start 2019/12/20
        /// <summary>
        /// フッタ消費税編集時再計算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txb消費税_LostFocus(object sender, RoutedEventArgs e)
        {
            int intTsujyo;
            int intKeigen;
            if (int.TryParse(txb通常税率消費税.Text, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign, null, out intTsujyo) == false)
            {
                ErrorMessage = "消費税は整数で入力してください。";
                return;
            }
            if (int.TryParse(txb軽減税率消費税.Text, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign, null, out intKeigen) == false)
            {
                ErrorMessage = "消費税は整数で入力してください。";
                return;
            }
            int subTotal;
            if (int.TryParse(lbl小計.Content.ToString(), System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign, null, out subTotal) == false)
            {
                return;
            }
            lbl消費税.Content = string.Format(PRICE_FORMAT_STRING, intTsujyo + intKeigen);
            lbl総合計.Content = string.Format(PRICE_FORMAT_STRING, subTotal + intTsujyo + intKeigen);
        }
        //▲課題No298 Add End 2019/12/20
        #endregion

    }

}
