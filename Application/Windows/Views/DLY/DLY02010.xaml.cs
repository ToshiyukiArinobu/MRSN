using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.ComponentModel;

using System.IO;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;
using System.Data;


using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;
using KyoeiSystem.Framework.Windows.Controls;
using GrapeCity.Windows.SpreadGrid;
using System.Reflection;

using System.Windows.Threading;


namespace KyoeiSystem.Application.Windows.Views
{
    using DebugLog = System.Diagnostics.Debug;
    using KyoeiSystem.Application.Windows.Views.Common;

    /// <summary>
    /// 揚り入力
    /// </summary>
    public partial class DLY02010 : RibbonWindowViewBase
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
        public class ConfigDLY02010 : FormConfigBase
        {
        }

        /// ※ 必ず public で定義する。
        public ConfigDLY02010 frmcfg = null;

        #endregion

        #region 定数定義

        #region サービスアクセス定義
        /// <summary>揚り情報取得</summary>
        private const string T04_GetData = "T04_GetData";
        /// <summary>揚り情報更新</summary>
        private const string T04_Update = "T04_Update";
        /// <summary>揚り情報削除</summary>
        private const string T04_Delete = "T04_Delete";
        /// <summary>揚り部材明細情報取得</summary>
        private const string T04_GetDTB = "T04_GetDtb";
        /// <summary>揚り部材明細情報作成</summary>
        private const string T04_CreateDTB = "T04_CreateDtb";
        /// <summary>更新用_在庫数チェック</summary>
        private const string UpdateData_StockCheck = "T04_UpdateData_CheckStock";       // No-222 Add

        /// <summary>外注先ベースのデータ取得</summary>
        private const string SearchTableToOutsource = "M04_BAIKA_GetData_Outsource";
        /// <summary>セット品番情報検索</summary>
        private const string SearchTableToShin = "T04_GetM10_Shin";
        /// <summary>セット品番情報検索</summary>
        private const string SearchTableToShinForDataTable = "T04_GetM10_ShinForDataTable";

        /// <summary>取引先名称取得</summary>
        private const string MasterCode_Supplier = "UcSupplier";
        /// <summary>自社品番情報取得</summary>
        private const string MasterCode_MyProduct = "UcCustomerProduct";           // No-65 Mod

        //20190528CB-S
        /// <summary>セット品番構成品情報取得</summary>
        private const string M10_GetCount = "M10_GetCount";
        //20190528CB-E
        #endregion

        #region 使用テーブル名定義
        private const string HEADER_TABLE_NAME = "T04_AGRHD";
        private const string DETAIL_TABLE_NAME = "T04_AGRDTL";
        private const string INNER_TABLE_NAME = "T04_AGRDTB";
        private const string INNER_TABLE_NAME_EXTENT = "T04_AGRDTB_Extension";
        private const string ZEI_TABLE_NAME = "M73_ZEI";
        #endregion

        /// <summary>金額フォーマット定義</summary>
        private const string PRICE_FORMAT_STRING = "{0:#,0}";

        /// <summary>加工区分が社内加工時の外注先固定コード</summary>
        private const string 社内加工_コード = "9000";
        /// <summary>加工区分が社内加工時の外注先固定枝番</summary>
        private const string 社内加工_枝番 = "99";

        #endregion

        #region 列挙型定義

        /// <summary>
        /// データグリッドの列定義
        /// </summary>
        private enum GridColumnsMapping : int
        {
            自社品番 = 0,
            自社品名 = 1,
            賞味期限 = 2,
            数量 = 3,
            単位 = 4,
            単価 = 5,
            金額 = 6,
            摘要 = 7,
            品番コード = 8,
            消費税区分 = 9,
            商品分類 = 10,

            // 20190606CB-S
            色コード = 11,
            色名称 = 12,
            // 20190606CB-E

            構成部品 = 13,
            商品形態分類 = 14
        }

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
            社内加工 = 3
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

        /// <summary>
        /// 消費税区分
        /// </summary>
        private enum 消費税区分 : int
        {
            通常税率 = 0,
            軽減税率 = 1,
            非課税 = 2
        }

        #endregion

        #region バインディングデータ

        /// <summary>揚りヘッダ情報</summary>
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

        /// <summary>揚り明細情報</summary>
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

        /// <summary>揚り部材明細情報(取得データ保持)</summary>
        private DataTable _innerDetailDtb;

        /// <summary>揚り部材明細情報(新規：画面表示用)</summary>
        private DataTable _innerDetail;
        public DataTable InnerDetail
        {
            get { return _innerDetail; }
            set
            {
                _innerDetail = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 外注先売価データテーブル
        /// </summary>
        public DataTable _Outsource;
        public DataTable Outsource
        {
            get { return _Outsource; }
            set
            {
                this._Outsource = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// セット品データテーブル
        /// </summary>
        public DataTable _SetHin;
        public DataTable SetHin
        {
            get { return _SetHin; }
            set
            {
                this._SetHin = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// セット品データテーブル（揚り明細分）
        /// </summary>
        public DataTable _SetHinAll;
        public DataTable SetHinAll
        {
            get { return _SetHinAll; }
            set
            {
                this._SetHinAll = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 揚り部材明細情報拡張クラス
        /// </summary>
        public class T04_AGRDTB_Extension
        {
            public int セット品番コード { get; set; }
            public int 品番コード { get; set; }
            public string 自社品番 { get; set; }
            public string 自社品名 { get; set; }
            public string 自社色 { get; set; }
            public string 自社色名 { get; set; }
            public DateTime? 賞味期限 { get; set; }
            public decimal 数量 { get; set; }
            public string 単位 { get; set; }
            public decimal 必要数量 { get; set; }
            public decimal 在庫数量 { get; set; }
            public int 行番号 { get; set; }
            public int 部材行番号 { get; set; }
            /// <summary>1:食品、2:繊維、3:その他</summary>
            public int 商品分類 { get; set; }
        }
        /// 揚り部材明細　配列
        private List<T04_AGRDTB_Extension>[] T04_AGRDTB_List = new List<T04_AGRDTB_Extension>[10];

        /// <summary>
        /// 編集中の行番号
        /// </summary>
        private int _編集行;

        #endregion

        #region << クラス変数定義 >>

        /// <summary>グリッドコントローラ</summary>
        GcSpreadGridController gridDtl;

        /// <summary>消費税計算</summary>
        TaxCalculator taxCalc;

        /// <summary>警告フラグ(true:警告あり、false:警告なし)</summary>
        private bool blnWarningFlg = false;

        #endregion

        #region << 初期処理群 >>

        /// <summary>
        /// 揚り入力　コンストラクタ
        /// </summary>
        public DLY02010()
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

            frmcfg = (ConfigDLY02010)ucfg.GetConfigValue(typeof(ConfigDLY02010));

            #endregion

            if (frmcfg == null)
            {
                frmcfg = new ConfigDLY02010();
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
            base.MasterMaintenanceWindowList.Add("M09_HIN_SET", new List<Type> { typeof(MST02010), typeof(SCHM09_HIN) });
            base.MasterMaintenanceWindowList.Add("M11_TEK", new List<Type> { typeof(MST08010), typeof(SCHM11_TEK) });
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });

            // コンボデータ取得
            AppCommon.SetutpComboboxList(this.cmb加工区分, false);
            gridDtl = new GcSpreadGridController(sp製品一覧);

            // Spread設定
            ScreenClear();
            ChangeKeyItemChangeable(true);

            this.sp製品一覧.PreviewKeyDown += sp製品一覧_PreviewKeyDown;

            ButtonCellType btn = this.sp製品一覧.Columns[13].CellType as ButtonCellType;
            btn.Command = new cmd構成部品(sp製品一覧);

            // ログインユーザの自社区分によりコントロール状態切換え
            this.txt会社名.Text1 = ccfg.自社コード.ToString();
            this.txt会社名.IsEnabled = ccfg.自社販社区分.Equals((int)自社販社区分.自社);

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
                    case SearchTableToOutsource:
                        // 外注先コードで検索された場合
                        Outsource = tbl.Copy();
                        break;

                    case SearchTableToShin:
                        // セット品で検索された場合
                        SetHin = tbl.Copy();
                        break;

                    case T04_GetData:
                        // 伝票検索または新規伝票の場合
                        DataSet ds = data as DataSet;
                        if (ds != null)
                        {
                            SetTblData(ds);
                            ChangeKeyItemChangeable(false);
                            txt仕上り日.Focus();
                            // No.162-3 Add Start
                            bool blnEnabled = true;
                            if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_EDIT)
                            {
                                blnEnabled = false;
                            }
                            // 入力制御
                            setDispHeaderEnabled(blnEnabled);
                            SetDispRibbonEnabled(blnEnabled);
                            // No.162-3 Add End
                        }
                        else
                        {
                            MessageBox.Show("指定の伝票番号は登録されていません。", "伝票未登録", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            this.txt伝票番号.Focus();
                        }
                        break;

                    case UpdateData_StockCheck:
                        // No-222 Add Start
                        // 在庫数チェック結果受信
                        Dictionary<string, string> updateList = data as Dictionary<string, string>;
                        string zaiUpdateMessage = AppConst.CONFIRM_UPDATE;
                        var zaiMBImage = MessageBoxImage.Question;

                        foreach (DataRow row in SearchDetail.Select("", "", DataViewRowState.CurrentRows))
                        {
                            if (updateList.Count > 0)
                            {
                                zaiMBImage = MessageBoxImage.Warning;
                                zaiUpdateMessage = "在庫がマイナスになる品番が存在しますが、\r\n登録してもよろしいでしょうか？";
                                break;
                            }
                        }

                        if (MessageBox.Show(zaiUpdateMessage,
                                "登録確認",
                                MessageBoxButton.YesNo,
                                zaiMBImage,
                                MessageBoxResult.Yes) == MessageBoxResult.No)
                            return;
                        // No-222 Add End

                        // セット品データ（明細行全件）を取得する
                        sendSearchForShinAll(SearchDetail);

                        break;

                    case SearchTableToShinForDataTable:
                        // セット品データ（明細行全件）で検索された場合
                        SetHinAll = tbl.Copy();

                        Update();

                        break;

                    case T04_Update:
                        MessageBox.Show(AppConst.SUCCESS_UPDATE, "登録完了", MessageBoxButton.OK, MessageBoxImage.Information);
                        // コントロール初期化
                        ScreenClear();
                        break;

                    case T04_Delete:
                        MessageBox.Show(AppConst.SUCCESS_DELETE, "削除完了", MessageBoxButton.OK, MessageBoxImage.Information);
                        // コントロール初期化
                        ScreenClear();
                        break;

                    case T04_GetDTB:
                        // TODO:仕入先コード変更時に入れ替えの必要があるかも
                        _innerDetailDtb.Clear();
                        // 揚り部材明細情報(登録済み)を表示用に加工する
                        _innerDetailDtb = T04_AGRDTB_DisplayConvert(SearchDetail, tbl);

                        break;

                    case T04_CreateDTB:

                        // No-279 Mod Start
                        int intIdx = gridDtl.ActiveRowIndex;
                        int intSuryo = gridDtl.GetCellValueToInt((int)GridColumnsMapping.数量) ?? 0;
                        
                        // 揚り部材明細情報(新規用)を表示用に加工する
                        InnerDetail = T04_AGRDTB_NewDataDisplayConvert(tbl);

                        // セット品マスタを主とした取得でない場合
                        if (InnerDetail.Rows.Count == 0)
                         {
                            // 品番マスタから取得したレコードを表示編集する
                            InnerDetail = T04_AGRDTB_NewDataDisplayConvertForHin(intSuryo, tbl);
                        }
                        // No-279 Mod End

                        // 揚り部材明細 配列に追加する
                        AddAgrDtbList(gridDtl.ActiveRowIndex, InnerDetail);
                        break;

                    case MasterCode_Supplier:
                        // 仕入先名称取得
                        if (tbl != null && tbl.Rows.Count > 0)
                        {
                            this.txt外注先.Label2Text = tbl.Rows[0]["名称"].ToString();
                            SearchHeader["Ｓ支払消費税区分"] = tbl.Rows[0]["Ｓ支払消費税区分"];
                            SearchHeader["Ｓ税区分ID"] = tbl.Rows[0]["Ｓ税区分ID"];
                        }
                        else
                        {
                            this.txt外注先.Label2Text = string.Empty;
                            SearchHeader["Ｓ支払消費税区分"] = 1;   // 1:一括
                            SearchHeader["Ｓ税区分ID"] = 9; // 9:税なし
                        }
                        summaryCalculation();
                        
                        // 外注先売価　情報取得
                        sendSearchForOutsource();

                        break;

                    case MasterCode_MyProduct:
                        #region 自社品番手入力時
                        DataTable ctbl = data as DataTable;

                        int columnIdx = gridDtl.ActiveColumnIndex;
                        int rIdx = gridDtl.ActiveRowIndex;

                        // フォーカス移動後の項目が異なる場合または編集行が異なる場合は処理しない。
                        if ((columnIdx != (int)GridColumnsMapping.自社品名) || _編集行 != rIdx) return;

                        if (ctbl == null || ctbl.Rows.Count == 0)
                        {
                            // 対象データなしの場合
                            gridDtl.SetCellValue((int)GridColumnsMapping.品番コード, 0);
                            gridDtl.SetCellValue((int)GridColumnsMapping.自社品番, string.Empty);
                            gridDtl.SetCellValue((int)GridColumnsMapping.自社品名, string.Empty);
                            gridDtl.SetCellValue((int)GridColumnsMapping.数量, 0m);
                            gridDtl.SetCellValue((int)GridColumnsMapping.単位, string.Empty);
                            gridDtl.SetCellValue((int)GridColumnsMapping.単価, 0);
                            gridDtl.SetCellValue((int)GridColumnsMapping.金額, 0);
                            gridDtl.SetCellValue((int)GridColumnsMapping.消費税区分, 0);   // [軽減税率対象]0:対象外
                            gridDtl.SetCellValue((int)GridColumnsMapping.商品分類, (int)商品分類.その他);

                            // 20190530CB-S
                            gridDtl.SetCellValue((int)GridColumnsMapping.色コード, string.Empty);
                            gridDtl.SetCellValue((int)GridColumnsMapping.色名称, string.Empty);
                            // 20190530CB-E

                            gridDtl.SetCellValue((int)GridColumnsMapping.商品形態分類, 0);       // No-279 Add

                        }
                        else if (tbl.Rows.Count > 1)
                        {
                            int cIdx = sp製品一覧.ActiveColumnIndex;
                            var colVal = gridDtl.GetCellValueToString((int)GridColumnsMapping.自社品番);

                            if (string.IsNullOrEmpty(this.txt外注先.Text1) || string.IsNullOrEmpty(this.txt外注先.Text2))
                            {
                                MessageBox.Show("外注先が設定されていません。", "外注先未設定", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                txt外注先.SetFocus();
                                return;
                            }

                            int code = int.Parse(this.txt外注先.Text1);
                            int eda = int.Parse(this.txt外注先.Text2);

                            // 自社品番の場合
                            SCHM09_MYHIN myhin = new SCHM09_MYHIN(code, eda);
                            myhin.TwinTextBox = new UcLabelTwinTextBox();
                            myhin.IsItemStatusType = true;
                            myhin.txtCode.Text = colVal;
                            myhin.txtCode.IsEnabled = false;
                            myhin.TwinTextBox.LinkItem = 2;

                            if (myhin.ShowDialog(this) == true)
                            {
                                gridDtl.SetCellValue((int)GridColumnsMapping.品番コード, myhin.SelectedRowData["品番コード"]);
                                gridDtl.SetCellValue((int)GridColumnsMapping.自社品番, myhin.SelectedRowData["自社品番"]);
                                gridDtl.SetCellValue((int)GridColumnsMapping.自社品名, myhin.SelectedRowData["自社品名"]);
                                gridDtl.SetCellValue((int)GridColumnsMapping.数量, 1m);
                                gridDtl.SetCellValue((int)GridColumnsMapping.単位, myhin.SelectedRowData["単位"]);

                                // No-96 Mod Start
                                if (cmb加工区分.SelectedValue.ToString() == ((int)加工区分.社内加工).ToString())
                                {
                                    gridDtl.SetCellValue((int)GridColumnsMapping.単価, 0m);
                                    gridDtl.SetCellValue((int)GridColumnsMapping.金額, 0);
                                }
                                else
                                {
                                    Decimal dcmUnitPrice = 0;
                                    if (getOutsourceUnitPrice(int.Parse(myhin.SelectedRowData["品番コード"].ToString()), out dcmUnitPrice) == true)
                                    {
                                        gridDtl.SetCellValue((int)GridColumnsMapping.単価, dcmUnitPrice);
                                        gridDtl.SetCellValue((int)GridColumnsMapping.金額, Convert.ToInt32(dcmUnitPrice));
                                    }
                                    else
                                    {
                                        // No.119 Mod Start
                                        gridDtl.SetCellValue((int)GridColumnsMapping.単価, myhin.SelectedRowData["加工原価"]);
                                        gridDtl.SetCellValue((int)GridColumnsMapping.金額, string.IsNullOrEmpty(myhin.SelectedRowData["加工原価"].ToString()) ? 0 :
                                                                                         Convert.ToInt32(myhin.SelectedRowData["加工原価"]));
                                        // No.119 Mod End
                                    }

                                }
                                // No-96 Mod End

                                gridDtl.SetCellValue((int)GridColumnsMapping.消費税区分, myhin.SelectedRowData["消費税区分"]);
                                gridDtl.SetCellValue((int)GridColumnsMapping.商品分類, myhin.SelectedRowData["商品分類"]);

                                // 20190530CB-S
                                gridDtl.SetCellValue((int)GridColumnsMapping.色コード, myhin.SelectedRowData["自社色"]);
                                gridDtl.SetCellValue((int)GridColumnsMapping.色名称, myhin.SelectedRowData["自社色名"]);
                                // 20190530CB-E

                                gridDtl.SetCellValue((int)GridColumnsMapping.商品形態分類, myhin.SelectedRowData["商品形態分類"]);       // No-279 Add

                                // 集計計算をおこなう
                                summaryCalculation();

                                // セット品情報を取得する
                                sendSearchForShin(myhin.SelectedRowData["品番コード"].ToString());

                                // 揚り部材明細情報を取得する
                                sendSearchForAgrDtb();

                            }

                        }
                        else
                        {
                            // 対象データありの場合
                            DataRow drow = ctbl.Rows[0];
                            sp製品一覧.BeginEdit();
                            gridDtl.SetCellValue((int)GridColumnsMapping.品番コード, drow["品番コード"]);
                            gridDtl.SetCellValue((int)GridColumnsMapping.自社品番, drow["自社品番"]);
                            gridDtl.SetCellValue((int)GridColumnsMapping.自社品名, drow["自社品名"]);
                            gridDtl.SetCellValue((int)GridColumnsMapping.数量, 1m);
                            gridDtl.SetCellValue((int)GridColumnsMapping.単位, drow["単位"]);


                            // No-96 Mod Start
                            if (cmb加工区分.SelectedValue.ToString() == ((int)加工区分.社内加工).ToString())
                            {
                                gridDtl.SetCellValue((int)GridColumnsMapping.単価, 0m);
                                gridDtl.SetCellValue((int)GridColumnsMapping.金額, 0);
                            }
                            else
                            {
                                Decimal dcmUnitPrice = 0;
                                if (getOutsourceUnitPrice(int.Parse(drow["品番コード"].ToString()), out dcmUnitPrice) == true)
                                {
                                    gridDtl.SetCellValue((int)GridColumnsMapping.単価, dcmUnitPrice);
                                    gridDtl.SetCellValue((int)GridColumnsMapping.金額,  Convert.ToInt32(dcmUnitPrice));
                                }
                                else
                                {
                                    gridDtl.SetCellValue((int)GridColumnsMapping.単価, drow["加工原価"]);
                                    gridDtl.SetCellValue((int)GridColumnsMapping.金額, drow["加工原価"] == null ? 0 : Convert.ToInt32(drow["加工原価"]));
                                }
                            }
                            // No-96 Mod End

                            gridDtl.SetCellValue((int)GridColumnsMapping.消費税区分, drow["消費税区分"]);
                            gridDtl.SetCellValue((int)GridColumnsMapping.商品分類, drow["商品分類"]);

                            // 20190530CB-S
                            gridDtl.SetCellValue((int)GridColumnsMapping.色コード, drow["自社色"]);
                            gridDtl.SetCellValue((int)GridColumnsMapping.色名称, drow["色名称"]);                 // No-65 Mod
                            // 20190530CB-E

                            gridDtl.SetCellValue((int)GridColumnsMapping.商品形態分類, drow["商品形態分類"]);       // No-279 Add

                            sp製品一覧.CommitCellEdit();
                            // 自社品番のセルをロック
                            // 数量以外はロック
                            gridDtl.SetCellLocked((int)GridColumnsMapping.品番コード, true);

                            // 20190704CB-S
                            gridDtl.SetCellLocked((int)GridColumnsMapping.自社品番, true);
                            gridDtl.SetCellLocked((int)GridColumnsMapping.自社品名, true);
                            gridDtl.SetCellLocked((int)GridColumnsMapping.単位, true);
                            //gridDtl.SetCellLocked((int)GridColumnsMapping.単価, true);                          // No.110 Mod
                            gridDtl.SetCellLocked((int)GridColumnsMapping.金額, true);
                            gridDtl.SetCellLocked((int)GridColumnsMapping.消費税区分, true);
                            gridDtl.SetCellLocked((int)GridColumnsMapping.商品分類, true);
                            gridDtl.SetCellLocked((int)GridColumnsMapping.色コード, true);
                            gridDtl.SetCellLocked((int)GridColumnsMapping.色名称, true);
                            // 20190704CB-E

                            summaryCalculation();

                            // セット品情報を取得する
                            sendSearchForShin(drow["品番コード"].ToString());

                            // 揚り部材明細情報を取得する
                            sendSearchForAgrDtb();

                        }

                        SearchDetail.Rows[rIdx].EndEdit();

                        #endregion

                        break;
                    // 20190528CB-S
                    case M10_GetCount:

                        if ((int)data == 0)
                        {
                            MessageBox.Show("セット品番の構成品が登録されておりません。構成品の登録を行ってください。");
                        }
                        break;
                    // 20190528CB-E

                    default:
                        break;

                }

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
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

        #region 外注先情報 データ取得
        /// <summary>
        /// 外注先売価情報を取得する
        /// </summary>
        private void sendSearchForOutsource()
        {
            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    SearchTableToOutsource,
                    new object[] {
                            this.txt外注先.Text1,
                            this.txt外注先.Text2
                        }));
        }
        #endregion

        #region セット品 データ取得
        /// <summary>
        /// セット品情報を取得する
        /// <param name="strProduct">品番コード（セット品）</param>
        /// </summary>
        private void sendSearchForShin(string strProduct)
        {
            if (!string.IsNullOrEmpty(strProduct))
            {
                int intProduct = int.Parse(strProduct);

                // セット品の構成品を取得
                this.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        SearchTableToShin,
                        new object[] {
                            intProduct
                        }));

            }
        }
        #endregion

        #region セット品 データ取得（明細行全件）
        /// <summary>
        /// セット品データ（明細行全件）を取得する
        /// <param name="strProduct">品番コード（セット品）</param>
        /// </summary>
        private void sendSearchForShinAll(DataTable dtAgrDtl)
        {
            DataTable dtAgrDtlTmp = dtAgrDtl.Clone();

            // 不要レコード削除
            foreach (DataRow rowAgrDtl in dtAgrDtl.Rows)
            {
                int? intHinNo = rowAgrDtl.Field<int?>("品番コード");

                if (string.IsNullOrEmpty(intHinNo.ToString()) == false)
                {
                    dtAgrDtlTmp.ImportRow(rowAgrDtl);
                }
            }

            DataSet dsAgrDtl = new DataSet();
            dsAgrDtl.Tables.Add(dtAgrDtlTmp.Copy());

            // セット品の構成品を取得
            this.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    SearchTableToShinForDataTable,
                    new object[] {
                        dsAgrDtl
                    }));

        }
        #endregion

        #region 揚り部材明細 データ取得
        /// <summary>
        /// 揚り部材明細情報を取得する
        /// </summary>
        private void sendSearchForAgrDtb()
        {
            DateTime date = DateTime.Now;
            DateTime.TryParse(txt仕上り日.Text, out date);

            if (_innerDetailDtb != null && _innerDetailDtb.Rows.Count > 0)
            {
                // 部材明細登録あり(変更時)
                var num = getSpreadGridValue(gridDtl.ActiveRowIndex, GridColumnsMapping.品番コード);
                var code = this.txt入荷先.Text1;

                if (num == null || string.IsNullOrEmpty(num.ToString()))
                {
                    if (InnerDetail != null)
                        InnerDetail.Clear();

                    return;

                }

                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        T04_GetDTB,
                        new object[] {
                            this.txt伝票番号.Text,
                            int.Parse(this.txt入荷先.Text1)
                            }));
            }
            else
            {
                // 部材明細登録なし(新規登録時)
                // ⇒設定されている品番コードよりデータを取得
                var num = getSpreadGridValue(gridDtl.ActiveRowIndex, GridColumnsMapping.品番コード);
                var code = this.txt入荷先.Text1;

                if (num == null || string.IsNullOrEmpty(num.ToString()))
                {
                    if (InnerDetail != null)
                        InnerDetail.Clear();

                    return;

                }

                // 商品形態分類を取得
                var syohinkeitaibunrui = getSpreadGridValue(gridDtl.ActiveRowIndex, GridColumnsMapping.商品形態分類);
                // セット品の場合
                if (int.Parse(syohinkeitaibunrui.ToString()) == 1)
                {
                    // 20190528CB-S
                    // セット品番の構成品が登録されているかチェック
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, M10_GetCount, new object[] { num }));
                    // 20190528CB-E
                }

                if (string.IsNullOrEmpty(this.txt入荷先.Text1))
                {
                    MessageBox.Show("入荷先が未設定の為、部材明細を取得できませんでした。", "確認", MessageBoxButton.OK);
                    return;
                }

                // param<1:品番コード(string)、2:会社コード(string)>
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        T04_CreateDTB,
                        new object[] {
                                num.ToString(),
                                code.ToString(),
                                date
                            }));

            }

        }
        #endregion

        #region 表示用データ加工
        /// <summary>
        /// 揚り部材明細情報(新規用)を表示用に加工する
        /// </summary>
        /// <param name="dtBuzaiDetail">揚り部材明細データテーブル</param>
        /// <returns>DataTable</returns>
        private DataTable T04_AGRDTB_NewDataDisplayConvert(DataTable dtBuzaiDetail)
        {
            int intHinCode = 0;                             // 品番コード
            int intHinBunrui = (int)商品分類.その他;       // 商品分類
            int intUnit = 0;                                // 構成数
            decimal dcmQuantity = 0;                        // 数量
            decimal dcmStokTotalQuantity = 0;               // 在庫数量

            DataTable dtResult = dtBuzaiDetail.Clone();

            // 揚り明細より数量を取得
            dcmQuantity = gridDtl.GetCellValueToDecimal((int)GridColumnsMapping.数量) ?? 0;

            // セット品マスタ取得
            foreach (DataRow row in SetHin.Rows)
            {
                // 使用数量取得
                intUnit = int.Parse(row["使用数量"].ToString());
                intHinCode = int.Parse(row["構成品番コード"].ToString());

                // 取得情報より該当品番の在庫数量を取得する
                var drRow = dtBuzaiDetail.AsEnumerable()
                    .Where(w => w.Field<int>("品番コード") == intHinCode)
                    .GroupBy(g => new { 品番コード = g.Field<int>("品番コード") })
                    .Select(s => new
                    {
                        品番コード = s.Key.品番コード,
                        在庫数量合計 = s.Sum(m => m.Field<decimal>("在庫数量")),
                        商品分類 = s.Max(m => m.Field<int>("商品分類"))
                    })
                    .FirstOrDefault();

                if (drRow == null)
                {
                    // 取得件数が0件の場合
                    intHinBunrui = (int)商品分類.その他;
                    dcmStokTotalQuantity = 0;
                }
                else
                {
                    // 取得件数が0件以外の場合
                    intHinBunrui = drRow.商品分類;
                    dcmStokTotalQuantity = drRow.在庫数量合計;
                }

                // 必要数量と在庫数量を比較
                if (dcmQuantity * intUnit <= dcmStokTotalQuantity)
                {
                    // 在庫数量が等しいもしくは大きい場合
                    // 在庫引き当てを行う
                    DataTable dtStockAllocat = setStockAllocation(dtBuzaiDetail, intHinCode, intUnit, dcmQuantity);
                    foreach (DataRow drStockAllocat in dtStockAllocat.Rows)
                    {
                        dtResult.ImportRow(drStockAllocat);
                    }
                }
                else
                {
                    // 在庫数量が小さい場合
                    if (intHinBunrui == (int)商品分類.食品)
                    {
                        // 食品の場合
                        // 引き当てを行い新規行を作成し残数を設定する
                        DataTable dtStockAllocat = setStockAllocation(dtBuzaiDetail, intHinCode, intUnit, dcmQuantity);
                        foreach (DataRow drStockAllocat in dtStockAllocat.Rows)
                        {
                            dtResult.ImportRow(drStockAllocat);
                        }
                    }
                    else
                    {
                        // 食品以外の場合
                        // 先頭行に必要数量を割り当てる
                        DataTable dtStockAllocat = setStockAllocationForFirstRecord(dtBuzaiDetail, intHinCode, intUnit, dcmQuantity);
                        foreach (DataRow drStockAllocat in dtStockAllocat.Rows)
                        {
                            dtResult.ImportRow(drStockAllocat);
                        }
                    }
                }
            }

            return dtResult;
        }

        /// <summary>
        /// 揚り部材明細情報(新規用)を表示用に加工する（在庫引き当て）
        /// </summary>
        /// <param name="dtBuzaiDetail">揚り部材明細データテーブル</param>
        /// <param name="intHinCode">品番コード</param>
        /// <param name="intUnit">構成数量</param>
        /// <param name="dcmQuantity">数量</param>
        /// <returns>DataTable</returns>
        private DataTable setStockAllocation(DataTable dtBuzaiDetail, int intHinCode, int intUnit, decimal dcmQuantity)
        {
            DataTable dtResult = new DataTable();               // 戻り
            DataRow rowBuff = dtResult.NewRow();                // 新規追加行（行退避）
            dtResult = dtBuzaiDetail.Clone();                       

            DateTime datMaxDate = AppCommon.GetMaxDate();       // 最大日（項目非表示制御）
            DateTime datDate = DateTime.Now;                    // 仕上り日（変換用）
            DateTime.TryParse(txt仕上り日.Text, out datDate);      

            decimal dcmCalcQuantity = 0;                        // 算出用数量
            bool blnRowDelete = false;                          // 行非表示
            int intRowcnt = 0;                                  // 行数

            // 取得情報より該当品番を取得する
            var varRow = dtBuzaiDetail.AsEnumerable()
                .Where(w => w.Field<int>("品番コード") == intHinCode)
                .ToArray();

            foreach (DataRow rowBuzai in varRow)
            {
                if (intRowcnt == 0)
                {
                    // 項目初期化
                    dcmCalcQuantity = dcmQuantity * intUnit;
                    rowBuff = rowBuzai;
                    blnRowDelete = false;
                }

                // 表示対象外判定
                if (Decimal.Parse(rowBuzai["在庫数量"].ToString()) == 0 || blnRowDelete == true)
                {
                    // 在庫数が0または在庫数が必要数量を満たす場合は表示対象外とする
                    continue;
                }

                if (rowBuzai["賞味期限"].ToString().Length > 0)
                {
                    if (DateTime.Parse(rowBuzai["賞味期限"].ToString()) < datDate )
                    {
                        // 賞味期限が過去日の場合は表示対象外とする
                        continue;
                    }

                    // 賞味期限が最大値の場合は賞味期限を表示させない
                    if (DateTime.Parse(rowBuzai["賞味期限"].ToString()).Equals(datMaxDate))
                    {
                        rowBuzai["賞味期限"] = DBNull.Value;
                    }
                }

                // 在庫数がプラスの場合、数量算出を行う
                if (Decimal.Parse(rowBuzai["在庫数量"].ToString()) > 0)
                {
                    dcmCalcQuantity = dcmCalcQuantity - Decimal.Parse(rowBuzai["在庫数量"].ToString());

                    if (dcmCalcQuantity > 0)
                    {
                        rowBuzai["数量"] = rowBuzai["在庫数量"];
                    }
                    else
                    {
                        rowBuzai["数量"] = Decimal.Parse(rowBuzai["在庫数量"].ToString()) + dcmCalcQuantity;
                        // 品番コードが一致する以降の行は表示対象外とする
                        blnRowDelete = true;
                    }
                }

                if (intRowcnt == 0)
                {
                    rowBuzai["必要数量"] = dcmQuantity * intUnit;
                }
                else
                {
                    rowBuzai["必要数量"] = 0;
                }

                // レコード出力
                dtResult.ImportRow(rowBuzai);
                intRowcnt ++;
            }

            // 引き当てのない在庫が存在する　または　数量マイナス入力かつ算出用数量がマイナスかつ引当が未充足の場合
            if (dcmCalcQuantity > 0 || (dcmQuantity < 0 && dcmCalcQuantity < 0 && blnRowDelete == false))
            {
                // 新規行を追加する
                rowBuff["賞味期限"] = DBNull.Value;
                rowBuff["数量"] = dcmCalcQuantity;
                if (dtResult.Rows.Count == 0)
                {
                    rowBuff["必要数量"] = dcmQuantity * intUnit;
                }
                else
                {
                    rowBuff["必要数量"] = 0;
                }
                rowBuff["在庫数量"] = 0.00m;
                dtResult.ImportRow(rowBuff);
            }

            return dtResult;
        }

        /// <summary>
        /// 揚り部材明細情報(新規用)を表示用に加工する（先頭行に在庫引き当て）
        /// </summary>
        /// <param name="dtBuzaiDetail">揚り部材明細データテーブル</param>
        /// <param name="intHinCode">品番コード</param>
        /// <param name="intUnit">構成数量</param>
        /// <param name="dcmQuantity">数量</param>
        /// <returns>DataTable</returns>
        private DataTable setStockAllocationForFirstRecord(DataTable dtBuzaiDetail, int intHinCode, int intUnit, decimal dcmQuantity)
        {
            DataTable dtResult = new DataTable();               // 戻り
            dtResult = dtBuzaiDetail.Clone();

            DateTime datMaxDate = AppCommon.GetMaxDate();       // 最大日（項目非表示制御）
            DateTime datDate = DateTime.Now;                    // 仕上り日（変換用）
            DateTime.TryParse(txt仕上り日.Text, out datDate);

            decimal dcmCalcQuantity = 0;                        // 算出用数量
            int intRowcnt = 0;                                  // 行数

            // 取得情報より該当品番を取得する
            var varRow = dtBuzaiDetail.AsEnumerable()
                .Where(w => w.Field<int>("品番コード") == intHinCode)
                .ToArray();

            foreach (DataRow rowBuzai in varRow)
            {
                if (intRowcnt == 0)
                {
                    // 項目初期化
                    dcmCalcQuantity = dcmQuantity * intUnit;
                }

                if (rowBuzai["賞味期限"].ToString().Length > 0)
                {
                    if (DateTime.Parse(rowBuzai["賞味期限"].ToString()) < datDate)
                    {
                        // 賞味期限が過去日の場合は表示対象外とする
                        continue;
                    }

                    // 賞味期限が最大値の場合は賞味期限を表示させない
                    if (DateTime.Parse(rowBuzai["賞味期限"].ToString()).Equals(datMaxDate))
                    {
                        rowBuzai["賞味期限"] = DBNull.Value;
                    }
                }

                if (intRowcnt == 0)
                {
                    rowBuzai["数量"] = dcmCalcQuantity;
                    rowBuzai["必要数量"] = dcmQuantity * intUnit;
                }
                else
                {
                    rowBuzai["必要数量"] = 0;
                }

                // DataRow編集
                dtResult.ImportRow(rowBuzai);
                intRowcnt++;
            }

            return dtResult;
        }

        /// <summary>
        /// 揚り部材明細情報(登録済み)を表示用に加工する
        /// </summary>
        /// <param name="dtDetail">揚り明細</param>
        /// <param name="dtBuzaiDetail">揚り部材明細</param>
        /// <returns>DataTable</returns>
        private DataTable T04_AGRDTB_DisplayConvert(DataTable dtDetail, DataTable dtBuzaiDetail)
        {
            DataTable dtResult = new DataTable();               // 戻り
            dtResult = dtBuzaiDetail.Copy();

            DateTime DatMaxDate = AppCommon.GetMaxDate();       // 最大日（項目非表示制御）

            foreach (DataRow rowBuzai in dtResult.Rows)
            {
                if (rowBuzai["賞味期限"].ToString().Length > 0)
                {
                    // 賞味期限が最大値の場合は賞味期限を表示させない
                    if (DateTime.Parse(rowBuzai["賞味期限"].ToString()).Equals(DatMaxDate))
                    {
                        rowBuzai["賞味期限"] = DBNull.Value;

                    }
                }
            }

            // 揚り部材明細レコードを揚り内部情報に格納する
            for (int i = 1; i <= 10; i++)
            {
                if (dtDetail.Rows.Count < i || dtDetail.Rows[i - 1]["品番コード"] == DBNull.Value)
                {
                    break;
                }
                // 揚り明細　数量取得
                Decimal dcmQuantity = decimal.Parse(dtDetail.Rows[i - 1]["数量"].ToString());

                DataTable dtAgrDtb = new DataTable();
                dtAgrDtb = dtResult.Clone();

                // 行番号が等しいレコードを取得する
                var varRow = dtBuzaiDetail.AsEnumerable()
                    .Where(row => row.Field<int>("行番号") == i).ToArray();

                if (varRow.Count() > 0)
                {
                    int intHinCode = 0;

                    foreach (DataRow rowBuzai in varRow)
                    {
                        if (intHinCode != int.Parse(rowBuzai["品番コード"].ToString()))
                        {
                            rowBuzai["必要数量"] = dcmQuantity * Decimal.Parse(rowBuzai["必要数量"].ToString());
                        }
                        else
                        {
                            rowBuzai["必要数量"] = 0;
                        }

                        // 揚り部材明細 配列用に編集する
                        dtAgrDtb.ImportRow(rowBuzai);

                        intHinCode = int.Parse(rowBuzai["品番コード"].ToString());
                    }

                    // 揚り部材明細 配列に追加する
                    AddAgrDtbList(i - 1, dtAgrDtb);
                }
            }

            return dtResult;
        }

		// No-279 Add Start
        /// <summary>
        /// 揚り部材明細情報(新規用)を表示用に加工する(品番マスタから取得したレコード編集)
        /// </summary>
        /// <param name="intSuryo">揚り明細で入力された数量</param>
        /// <param name="dtBuzaiDetail">揚り部材明細データテーブル</param>
        /// <returns>DataTable</returns>
        private DataTable T04_AGRDTB_NewDataDisplayConvertForHin(int intSuryo, DataTable dtBuzaiDetail)
        {
            DataTable dtResult = dtBuzaiDetail.Clone();
            DateTime DatMaxDate = AppCommon.GetMaxDate();       // 最大日（項目非表示制御）

            foreach (DataRow rowBuzai in dtBuzaiDetail.Rows)
            {
                if (rowBuzai["賞味期限"].ToString().Length > 0)
                {
                    // 賞味期限が最大値の場合は賞味期限を表示させない
                    if (DateTime.Parse(rowBuzai["賞味期限"].ToString()).Equals(DatMaxDate))
                    {
                        rowBuzai["賞味期限"] = DBNull.Value;
                    }
                }
                // 揚り明細で入力された数量を設定する
                rowBuzai["数量"] = intSuryo;
                rowBuzai["必要数量"] = 1m;

                dtResult.ImportRow(rowBuzai);
            }

            return dtResult;
        }
		// No-279 Add End

        #endregion

        #region 外注先売価取得
        /// <summary>
        /// 外注先売価単価を取得する
        /// </summary>
        /// <param name="intCode">品番コード</param>
        /// <param name="dcmUnitPrice">外注先売価単価</param>
        /// <returns>bool</returns>
        private bool getOutsourceUnitPrice(int intCode, out decimal dcmUnitPrice)
        {
            dcmUnitPrice = 0;
            bool blnResult = false;

            var varRow = Outsource.AsEnumerable()
                .Where(w => w.Field<int>("品番コード") == intCode)
                .ToArray();

            foreach (DataRow rowOutsource in varRow)
            {
                dcmUnitPrice = rowOutsource.Field<decimal>("単価");
                blnResult = true;
            }

            return blnResult;

        }
        #endregion

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
                SearchDetail.Clear();
            if (_innerDetailDtb != null)
                _innerDetailDtb = null;
            if (InnerDetail != null)
                InnerDetail.Clear();
            if (Outsource != null)
                Outsource.Clear();
            if (SetHin != null)
                SetHin.Clear();

            clearAgrDtbList();

            this.txt備考.Text1 = string.Empty;

            string initValue = string.Format("{0:#,0}", 0);
            this.lbl小計.Content = initValue;
            this.lbl消費税.Content = initValue;
            this.lbl総合計.Content = initValue;

            ChangeKeyItemChangeable(true);
            ResetAllValidation();

            // ログインユーザの自社区分によりコントロール状態切換え
            this.txt会社名.Text1 = ccfg.自社コード.ToString();
            this.txt会社名.IsEnabled = ccfg.自社販社区分.Equals((int)自社販社区分.自社);

            this.lbl情報.Content = string.Empty;                      // No-87 Add

            this.txt伝票番号.Focus();

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
                var twintxt = ViewBaseCommon.FindVisualParent<UcLabelTwinTextBox>(elmnt as Control);

                if (spgrid != null)
                {
                    int cIdx = spgrid.ActiveColumnIndex;
                    int rIdx = spgrid.ActiveRowIndex;

                    #region グリッドファンクションイベント
                    if (spgrid.ActiveColumnIndex == GridColumnsMapping.自社品番.GetHashCode())
                    {
                        // 対象セルがロックされている場合は処理しない
                        if (spgrid.Cells[rIdx, cIdx].Locked == true)
                            return;

                        if (string.IsNullOrEmpty(this.txt外注先.Text1) || string.IsNullOrEmpty(this.txt外注先.Text2))
                        {
                            MessageBox.Show("外注先が設定されていません。", "外注先未設定", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            txt外注先.SetFocus();
                            return;
                        }

                        int code = int.Parse(this.txt外注先.Text1);
                        int eda = int.Parse(this.txt外注先.Text2);

                        // 自社品番の場合
                        SCHM09_MYHIN myhin = new SCHM09_MYHIN(code, eda);
                        myhin.TwinTextBox = new UcLabelTwinTextBox();
                        myhin.IsItemStatusType = true;
                        myhin.TwinTextBox.LinkItem = 2;
                        if (myhin.ShowDialog(this) == true)
                        {
                            //入力途中のセルを未編集状態に戻す
                            spgrid.CancelCellEdit();

                            spgrid.Cells[rIdx, (int)GridColumnsMapping.品番コード].Value = myhin.SelectedRowData["品番コード"];
                            spgrid.Cells[rIdx, (int)GridColumnsMapping.自社品番].Value = myhin.SelectedRowData["自社品番"];
                            spgrid.Cells[rIdx, (int)GridColumnsMapping.自社品名].Value = myhin.SelectedRowData["自社品名"];
                            spgrid.Cells[rIdx, (int)GridColumnsMapping.数量].Value = 1m;
                            spgrid.Cells[rIdx, (int)GridColumnsMapping.単位].Value = myhin.SelectedRowData["単位"];

                            // No-96 Mod Start
                            if (cmb加工区分.SelectedValue.ToString() == ((int)加工区分.社内加工).ToString())
                            {
                                spgrid.Cells[rIdx, (int)GridColumnsMapping.単価].Value = 0m;
                                spgrid.Cells[rIdx, (int)GridColumnsMapping.金額].Value = 0;
                            }
                            else
                            {
                                Decimal dcmUnitPrice = 0;
                                if (getOutsourceUnitPrice(int.Parse(myhin.SelectedRowData["品番コード"].ToString()), out dcmUnitPrice) == true)
                                {
                                    spgrid.Cells[rIdx, (int)GridColumnsMapping.単価].Value = dcmUnitPrice;
                                    spgrid.Cells[rIdx, (int)GridColumnsMapping.金額].Value = decimal.ToInt32(dcmUnitPrice);
                                }
                                else
                                {
                                    spgrid.Cells[rIdx, (int)GridColumnsMapping.単価].Value = myhin.TwinTextBox.Text3;
                                    spgrid.Cells[rIdx, (int)GridColumnsMapping.金額].Value = string.IsNullOrEmpty(myhin.TwinTextBox.Text3) ?
                                                                                                        0 : decimal.ToInt32(AppCommon.DecimalParse(myhin.TwinTextBox.Text3));
                                }
                            }
                            // No-96 Mod End
                            
                            spgrid.Cells[rIdx, (int)GridColumnsMapping.消費税区分].Value = myhin.SelectedRowData["消費税区分"];
                            spgrid.Cells[rIdx, (int)GridColumnsMapping.商品分類].Value = myhin.SelectedRowData["商品分類"];

                            // 20190530CB-S
                            gridDtl.SetCellValue((int)GridColumnsMapping.色コード, myhin.SelectedRowData["自社色"]);
                            gridDtl.SetCellValue((int)GridColumnsMapping.色名称, myhin.SelectedRowData["自社色名"]);
                            // 20195030CB-E

                            gridDtl.SetCellValue((int)GridColumnsMapping.商品形態分類, myhin.SelectedRowData["商品形態分類"]);       // No-279 Add

                            // 設定自社品番の編集を不可とする
                            gridDtl.SetCellLocked((int)GridColumnsMapping.自社品番, true);

                            // 集計計算をおこなう
                            summaryCalculation();

                            // セット品情報を取得する
                            sendSearchForShin(myhin.SelectedRowData["品番コード"].ToString());

                            // 揚り部材明細情報を取得する
                            sendSearchForAgrDtb();

                        }

                    }
                    else if (spgrid.ActiveColumnIndex == GridColumnsMapping.摘要.GetHashCode())
                    {
                        // TODO:全角６文字を超える可能性アリ
                        SCHM11_TEK tek = new SCHM11_TEK();
                        tek.TwinTextBox = new UcLabelTwinTextBox();
                        if (tek.ShowDialog(this) == true)
                        {
                            spgrid.Cells[rIdx, cIdx].Value = tek.TwinTextBox.Text2;
                        }

                    }

                    SearchDetail.Rows[rIdx].EndEdit();

                    #endregion

                }
                else
                {
                    if (!(twintxt is UcLabelTwinTextBox))
                    {
                        return;
                    }
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                    // 仕入先の場合は個別に処理
                    // REMARKS:消費税関連の情報を取得する為
                    var twinText = ViewBaseCommon.FindVisualParent<UcLabelTwinTextBox>(elmnt as Control);
                    if (twinText.Name == this.txt外注先.Name)
                        txt外注先_cTextChanged(this.txt外注先, null);

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

                    if (spgrid.ActiveColumnIndex == GridColumnsMapping.自社品番.GetHashCode())
                    {
                        // 品番マスタ表示
                        MST02010 M09Form = new MST02010();
                        M09Form.Show(this);

                    }
                    else if (spgrid.ActiveColumnIndex == GridColumnsMapping.摘要.GetHashCode())
                    {
                        // 摘要マスタ表示
                        MST08010 M11Form = new MST08010();
                        M11Form.Show(this);
                    }

                    #endregion

                }
                else
                {
                    ViewBaseCommon.CallMasterMainte(this.MasterMaintenanceWindowList);

                }

            }
            catch (Exception ex)
            {
                appLog.Error("マスターメンテナンス画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }

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
            // No.131 Add Start
            dtlRow["伝票番号"] = this.txt伝票番号.Text;
            if (SearchDetail.Rows.Count - delRowCount > 0)
            {
                dtlRow["行番号"] = SearchDetail.Select("", "", DataViewRowState.CurrentRows).AsEnumerable().Select(a => a.Field<int>("行番号")).Max() + 1;
            }
            else
            {
                dtlRow["行番号"] = 1;
            }
            // No.131 Add End
            SearchDetail.Rows.Add(dtlRow);

            // 行追加後は追加行を選択させる
            // TODO:追加行が表示されるようにしたかったが追加行の上行までしか移動できない...
            int newRowIdx = SearchDetail.Rows.Count - delRowCount - 1;
            gridDtl.SetCellFocus(newRowIdx, (int)GridColumnsMapping.自社品名);

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

            int rowIdx = gridDtl.ActiveRowIndex;

            // No-109 Add Start
            if (gridDtl.ActiveRowIndex < 0)
            {
                this.ErrorMessage = "行を選択してください";
                return;
            }
            // No-109 Add End

            if (MessageBox.Show(
                    AppConst.CONFIRM_DELETE_ROW,
                    "行削除確認",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.No) == MessageBoxResult.No)
                return;

            try
            {
                gridDtl.SpreadGrid.Rows.Remove(gridDtl.ActiveRowIndex);
                // 揚り部材明細 配列から削除する
                deleteAgrDtbList(rowIdx);
            }
            catch
            {
                SearchDetail.Rows[gridDtl.ActiveRowIndex].Delete();
                // 揚り部材明細 配列から削除する
                deleteAgrDtbList(rowIdx);
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

            sp製品一覧.CommitCellEdit();          // No-173 Add

            if (MaintenanceMode == null || SearchDetail == null)
                return;

            // 揚り情報の設定
            DataSet ds = setAgrDataToDataSet();

            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    UpdateData_StockCheck,
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

            var yesno = MessageBox.Show("伝票を削除しますか？", "削除確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (yesno == MessageBoxResult.No)
                return;

            Delete();

        }
        #endregion

        #endregion

        #region Window_Closed

        /// <summary>
        /// 画面が閉じられた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            if (frmcfg == null) { frmcfg = new ConfigDLY02010(); }

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;

            ucfg.SetConfigValue(frmcfg);

        }

        #endregion
            
        #region << 検索データ設定・登録・削除処理 >>

        /// <summary>
        /// 取得内容を各コントロールに設定
        /// </summary>
        /// <param name="ds"></param>
        private void SetTblData(DataSet ds)
        {
            // 揚りヘッダ情報設定
            DataTable tblHd = ds.Tables[HEADER_TABLE_NAME];
            SearchHeader = tblHd.Rows[0];
            SearchHeader.AcceptChanges();

            // 揚り部材明細情報設定
            // REMARKS:明細設定時にイベントが走るので先に格納
            _innerDetailDtb = ds.Tables[INNER_TABLE_NAME];
            _innerDetailDtb.AcceptChanges();

            // 揚り明細情報設定
            SearchDetail = ds.Tables[DETAIL_TABLE_NAME];
            SearchDetail.AcceptChanges();

            // 消費税情報保持
            taxCalc = new TaxCalculator(ds.Tables[ZEI_TABLE_NAME]);

            // データ状態から編集状態を設定
            if (SearchDetail.Select("品番コード > 0").Count() == 0)
            {
                // 新規行を追加
                for (int i = 0; i < 10; i++)
                {
                    DataRow row = SearchDetail.NewRow();
                    row["伝票番号"] = AppCommon.IntParse(tblHd.Rows[0]["伝票番号"].ToString());
                    row["行番号"] = (i + 1);

                    SearchDetail.Rows.Add(row);

                }

                this.cmb加工区分.SelectedIndex = 0;
                this.txt外注先.Text1 = string.Empty;
                this.txt外注先.Text2 = string.Empty;
                this.txt入荷先.Text1 = string.Empty;

                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                this.txt仕上り日.Focus();

            }
            else
            {
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;

                // 取得明細の自社品番をロック(編集不可)に設定
                foreach (var row in sp製品一覧.Rows)
                    row.Cells[GridColumnsMapping.自社品番.GetHashCode()].Locked = true;

                gridDtl.SetCellFocus(0, (int)GridColumnsMapping.自社品番);

                this.lbl情報.Content = "セット品番を減らしても構成品は在庫を戻しません。";        // No-87 Add
            }

            // グリッド内容の再計算を実施
            summaryCalculation();

        }

        /// <summary>
        /// 揚り情報の登録処理をおこなう
        /// </summary>
        private void Update()
        {
            // 業務入力チェックをおこなう
            bool blnResult = isFormValidation();

            // 業務入力チェックをおこなう(揚り部材明細)
            bool blnResultAgrDtb = isFormValidationForAgrDtb(blnResult);

            // 業務入力チェックでエラーの場合、以降の処理を中止する
            if (blnResult == false || blnResultAgrDtb == false)
            {
                return;
            }

            // 全項目エラーチェック
            if (!base.CheckAllValidation())
            {
                this.txt仕上り日.Focus();
                return;
            }

            // 業務チェックの結果がワーニングの場合
            if (blnWarningFlg == true)
            {
                // 処理続行確認メッセージを表示する
                var yesno = MessageBox.Show("警告がありますが処理を続行しますか？", "確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (yesno == MessageBoxResult.No)
                {
                    return;
                }
            }

            // -- 送信用データを作成 --
            // 消費税をヘッダに設定
            SearchHeader["消費税"] = AppCommon.IntParse(this.lbl消費税.Content.ToString(), System.Globalization.NumberStyles.Number);

            // No.112 Add Start
            // 小計をヘッダに設定
            SearchHeader["小計"] = AppCommon.IntParse(this.lbl小計.Content.ToString(), System.Globalization.NumberStyles.Number);

            // 総合計をヘッダに設定
            SearchHeader["総合計"] = AppCommon.IntParse(this.lbl総合計.Content.ToString(), System.Globalization.NumberStyles.Number);
            // No.112 Add End

            // 揚り情報の設定
            DataSet ds = setAgrDataToDataSet();

            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    T04_Update,
                    new object[] {
                        ds,
                        ccfg.ユーザID
                    }));

        }

        /// <summary>
        /// 揚り情報の設定
        /// </summary>
        /// <returns>DataSet</returns>
        private DataSet setAgrDataToDataSet()
        {
            DataSet dsResult = new DataSet();

            // 揚りヘッダ追加
            dsResult.Tables.Add(SearchHeader.Table.Copy());

            // 揚り明細  行番号採番
            int intIdx = 1;
            foreach (DataRow row in SearchDetail.Rows)
            {
                // 追加行未入力レコードはスキップ
                if (row["品番コード"] == null || string.IsNullOrEmpty(row["品番コード"].ToString()) || row["品番コード"].ToString().Equals("0"))
                {
                    continue;
                }

                row["行番号"] = intIdx;
                intIdx++;
            }

            // 揚り部材明細　行番号、部材行番号採番
            dsResult.Tables.Add(SearchDetail.Copy());

            List<T04_AGRDTB_Extension> listAgrDtb = new List<T04_AGRDTB_Extension>();
            DataTable dtInnerDetail = new DataTable("T04_AGRDTB_Extension");

            // Listの中身を編集　DataTableに変換する
            for (int inti = 0; inti < 10; inti++)
            {
                int intj = 1;
                if (T04_AGRDTB_List[inti] != null)
                {
                    foreach (T04_AGRDTB_Extension row in T04_AGRDTB_List[inti])
                    {
                        // 数量が0以外のレコードを登録対象とする
                        if (row.数量 != 0)
                        {
                            row.行番号 = inti + 1;
                            row.部材行番号 = intj++;
                            listAgrDtb.Add(row);
                        }    
                    }
                }
            }
            // 揚り部材明細追加
            AppCommon.ConvertToDataTable(listAgrDtb, dtInnerDetail);
            dsResult.Tables.Add(dtInnerDetail.Copy());

            return dsResult;

        }

        /// <summary>
        /// 揚り情報の削除処理をおこなう
        /// </summary>
        private void Delete()
        {
            // 削除処理実行
            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    T04_Delete,
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
        /// <returns>bool</returns>
        private bool isKeyItemValidation()
        {
            bool isResult = false;

            if (string.IsNullOrEmpty(this.txt会社名.Text1))
            {
                base.ErrorMessage = "会社名が入力されていません。";
                return isResult;
            }

            return isResult = true;

        }

        /// <summary>
        /// 入力内容の検証をおこなう
        /// </summary>
        /// <returns>bool</returns>
        private bool isFormValidation()
        {
            bool isResult = false;
            // 【ヘッダ】必須入力チェック
            // 仕上り日
            if (string.IsNullOrEmpty(this.txt仕上り日.Text))
            {
                this.txt仕上り日.Focus();
                base.ErrorMessage = string.Format("仕入日が入力されていません。");
                return isResult;

            }

            // 加工区分
            if (this.cmb加工区分.SelectedValue == null)
            {
                this.cmb加工区分.Focus();
                base.ErrorMessage = string.Format("加工区分が選択されていません。");
                return isResult;

            }

            // 仕入先
            if (string.IsNullOrEmpty(this.txt外注先.Text1) || string.IsNullOrEmpty(this.txt外注先.Text2))
            {
                this.txt外注先.Focus();
                base.ErrorMessage = string.Format("外注先が入力されていません。");
                return isResult;

            }

            if (string.IsNullOrEmpty(this.txt外注先.Label2Text))
            {
                this.txt外注先.Focus();
                base.ErrorMessage = string.Format("外注先がマスタに存在していないデータが入力されています。");
                return isResult;
            }

            // 入荷先
            if (string.IsNullOrEmpty(this.txt入荷先.Text1))
            {
                this.txt入荷先.Focus();
                base.ErrorMessage = string.Format("入荷先が入力されていません。");
                return isResult;

            }

            // 現在の明細行を取得
            var CurrentDetail = SearchDetail.Select("", "", DataViewRowState.CurrentRows).AsEnumerable();

            // 【明細】詳細データが１件もない場合はエラー
            if (SearchDetail == null || CurrentDetail.Where(a => !string.IsNullOrEmpty(a.Field<string>("自社品番"))).Count() == 0)
            {
                gridDtl.SpreadGrid.Focus();
                base.ErrorMessage = string.Format("明細情報が１件もありません。");
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
                {
                    rIdx++;
                    continue;
                }

                // エラー情報をクリア
                gridDtl.ClearValidationErrors(rIdx);

                if (string.IsNullOrEmpty(row["数量"].ToString()) || decimal.Parse(row["数量"].ToString()) == 0)
                {
                    gridDtl.AddValidationError(rIdx, (int)GridColumnsMapping.数量, "数量が入力されていません。");
                    if (!isDetailErr)
                        gridDtl.SetCellFocus(rIdx, (int)GridColumnsMapping.数量);

                    isDetailErr = true;
                }

                if (string.IsNullOrEmpty(row["単価"].ToString()))
                {
                    gridDtl.AddValidationError(rIdx, (int)GridColumnsMapping.単価, "単価が入力されていません。");
                    if (!isDetailErr)
                        gridDtl.SetCellFocus(rIdx, (int)GridColumnsMapping.単価);

                    isDetailErr = true;
                }

                int type = Convert.ToInt32(row["商品分類"]);
                DateTime date;
                if (!DateTime.TryParse(row["賞味期限"].ToString(), out date))
                {
                    // 変換に失敗かつ商品分類が「食品」の場合はエラー
                    if (type.Equals((int)商品分類.食品))
                    {
                        gridDtl.AddValidationError(rIdx, (int)GridColumnsMapping.賞味期限, "商品分類が『食品』の為、賞味期限の設定が必要です。");
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

        #region << 揚り部材明細　入力検証 部品>>
        /// <summary>
        /// 入力内容の検証をおこなう
        /// </summary>
        /// <returns>bool</returns>
        private bool isFormValidationForAgrDtb(bool blnDetailResult)
        {
            bool isResult = false;
            bool isDetailErr = !blnDetailResult;

            blnWarningFlg = false;

            // 揚り明細分処理を繰り返す
            int rIdx = 0;
            int intListIdx = 0;
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

                int intDataCnt = 0;

                // セット品情報を取得する
                var varSetHin = SetHinAll.AsEnumerable()
                    .Where(w => w.Field<int>("品番コード") == int.Parse(row["品番コード"].ToString()))
                    .ToList();

                DataTable dtSetHin = SetHinAll.Clone();
                foreach (DataRow rowSetHin in varSetHin)
                {
                    dtSetHin.ImportRow(rowSetHin);
                }

                // 商品形態分類がセット品の場合、エラーチェックを行う
                if (int.Parse(row["商品形態分類"].ToString()) == 1)           // No-279 Add
                {
                    if (dtSetHin.Rows.Count <= 0)
                    {
                        // セット品マスタに登録がない場合
                        gridDtl.SpreadGrid.Focus();
                        gridDtl.AddValidationError(rIdx, (int)GridColumnsMapping.品番コード, "セット品マスタに登録がありません。");
                        if (!isDetailErr)
                            gridDtl.SetCellFocus(rIdx, (int)GridColumnsMapping.品番コード);

                        isDetailErr = true;
                    }
                }

                // 揚り部材明細のチェックを行う
                if (T04_AGRDTB_List[intListIdx] != null)
                {
                    foreach (T04_AGRDTB_Extension rowAgrDtb in T04_AGRDTB_List[intListIdx])
                    {

                        DateTime? row賞味期限 = DBNull.Value.Equals(rowAgrDtb.賞味期限) ? (DateTime?)null : Convert.ToDateTime(rowAgrDtb.賞味期限);
                        int? row品番コード = DBNull.Value.Equals(rowAgrDtb.品番コード) ? (int?)null : Convert.ToInt32(rowAgrDtb.品番コード);

                        if (T04_AGRDTB_List[intListIdx].AsEnumerable().Where(x => x.品番コード == row品番コード && x.賞味期限 == row賞味期限).Count() > 1)
                        {
                            gridDtl.SpreadGrid.Focus();
                            gridDtl.AddValidationError(rIdx, (int)GridColumnsMapping.品番コード, "構成品に同じ商品が存在するので、一つに纏めて下さい。");
                            if (!isDetailErr)
                                gridDtl.SetCellFocus(rIdx, (int)GridColumnsMapping.品番コード);

                            isDetailErr = true;
                        }

                        if (string.IsNullOrEmpty(rowAgrDtb.数量.ToString()))
                        {
                            gridDtl.SpreadGrid.Focus();
                            gridDtl.AddValidationError(rIdx, (int)GridColumnsMapping.数量, "構成品の数量が入力されていません。");
                            if (!isDetailErr)
                                gridDtl.SetCellFocus(rIdx, (int)GridColumnsMapping.品番コード);

                            isDetailErr = true;
                        }

                        int type = Convert.ToInt32(rowAgrDtb.商品分類);
                        DateTime date;
                        if (!DateTime.TryParse(rowAgrDtb.賞味期限.ToString(), out date))
                        {
                            // 変換に失敗かつ商品分類が「食品」かつ数量が0以外の場合はエラー
                            if (type.Equals((int)商品分類.食品) && rowAgrDtb.数量 != 0)
                            {
                                gridDtl.SpreadGrid.Focus();
                                gridDtl.AddValidationError(rIdx, (int)GridColumnsMapping.賞味期限, "構成品の商品分類が『食品』の為、賞味期限の設定が必要です。");
                                if (!isDetailErr)
                                    gridDtl.SetCellFocus(rIdx, (int)GridColumnsMapping.品番コード);

                                isDetailErr = true;
                            }

                        }

                        // 商品形態分類がセット品の場合、エラーチェックを行う
                        if (int.Parse(row["商品形態分類"].ToString()) == 1)           // No-279 Add
                        {
                            // セット品番マスタチェック（構成しない部材の入力判定）
                            if (DLY02010.isCheckShinNotExist(dtSetHin, rowAgrDtb.品番コード.ToString()) == false)
                            {
                                gridDtl.SpreadGrid.Focus();
                                gridDtl.AddValidationError(rIdx, (int)GridColumnsMapping.品番コード, "構成品に製品を構成しない部材が入力されています。");
                                if (!isDetailErr)
                                    gridDtl.SetCellFocus(rIdx, (int)GridColumnsMapping.品番コード);

                                blnWarningFlg = true;
                            }
                        }

                        intDataCnt++;
                    }

                    // 揚り部材明細データが１件もない場合はエラー
                    if (intDataCnt <= 0)
                    {
                        gridDtl.SpreadGrid.Focus();
                        gridDtl.AddValidationError(rIdx, (int)GridColumnsMapping.品番コード, "構成品の明細情報が１件もありません。");
                        if (!isDetailErr)
                            gridDtl.SetCellFocus(rIdx, (int)GridColumnsMapping.品番コード);

                        isDetailErr = true;
                        return isResult;
                    }

                    // チェック用変換
                    DataTable dtSearchDetailTmp = SearchDetail.Clone();
                    dtSearchDetailTmp.ImportRow(row);

                    DataTable dtInnerDetailTmp = new DataTable();
                    AppCommon.ConvertToDataTable(T04_AGRDTB_List[intListIdx], dtInnerDetailTmp);

                    // 商品形態分類がセット品の場合、エラーチェックを行う
                    if (int.Parse(row["商品形態分類"].ToString()) == 1)           // No-279 Add
                    {
                        // セット品番マスタチェック（構成部材の入力判定）
                        if (DLY02010.isCheckShinUnnecessary(dtSetHin, dtInnerDetailTmp) == false)
                        {
                            gridDtl.SpreadGrid.Focus();
                            gridDtl.AddValidationError(rIdx, (int)GridColumnsMapping.品番コード, "構成品に製品を構成する部材が入力されていません。");
                            if (!isDetailErr)
                                gridDtl.SetCellFocus(rIdx, (int)GridColumnsMapping.品番コード);

                            blnWarningFlg = true;
                        }

                        // セット品番マスタチェック（数量）
                        string strErrMsg = string.Empty;
                        if (DLY02010.isCheckShinQuantity(dtSetHin, dtSearchDetailTmp, dtInnerDetailTmp, out strErrMsg) == false)
                        {
                            gridDtl.SpreadGrid.Focus();
                            gridDtl.AddValidationError(rIdx, (int)GridColumnsMapping.品番コード, strErrMsg);
                            if (!isDetailErr)
                                gridDtl.SetCellFocus(rIdx, (int)GridColumnsMapping.品番コード);

                            blnWarningFlg = true;
                        }
                    }
                    rIdx++;
                    intListIdx++;
                }
            }
            
            if (isDetailErr)
                return isResult;

            isResult = true;

            return isResult;

        }

        #endregion

        #region << 揚り部材明細　入力検証 部品　(DLY02011共通)>>
        /// <summary>
        /// セット品番マスタチェック（構成しない部材の入力判定）
        /// </summary>
        /// <param name="dtSetHin">セット品マスタ</param>
        /// <param name="strHinCode">品番コード</param>
        /// <returns>bool</returns>
        public static bool isCheckShinNotExist(DataTable dtSetHin, string strHinCode)
        {
            bool blnResult = false;

            if (string.IsNullOrEmpty(strHinCode) == true)
            {
                return blnResult;
            }

            int intHinCode = int.Parse(strHinCode);

            var varCount = dtSetHin.AsEnumerable()
                .Where(w => w.Field<int>("構成品番コード") == intHinCode)
                .Count();

            if (varCount > 0)
            {
                blnResult = true;
            }

            return blnResult;
        }

        /// <summary>
        /// セット品番マスタチェック（構成部材の入力判定）
        /// </summary>
        /// <param name="dtSetHin">セット品マスタ</param>
        /// <param name="dtBuzaiDetail">揚り部材</param>
        /// <returns>bool</returns>
        public static bool isCheckShinUnnecessary(DataTable dtSetHin, DataTable dtBuzaiDetail)
        {
            bool blnResult = false;

            DataTable dtBuzaiDetailTmp = dtBuzaiDetail.Copy();
            dtBuzaiDetailTmp.AcceptChanges();

            foreach (DataRow row in dtSetHin.Rows)
            {
                var varCount = dtBuzaiDetailTmp.AsEnumerable()
                    .Where(w => w.Field<int?>("品番コード") == int.Parse(row["構成品番コード"].ToString()))
                    .Count();

                if (varCount <= 0)
                {
                    return blnResult;
                }
            }

            blnResult = true;

            return blnResult;
        }

        /// <summary>
        /// セット品番マスタチェック（数量）
        /// </summary>
        /// <param name="dtSetHin">セット品マスタ</param>
        /// <param name="dtDetail">揚り明細</param>
        /// <param name="dtBuzaiDetail">揚り部材</param>
        /// <param name="strErrMsg">エラーメッセージ</param>
        /// <returns>bool</returns>
        public static bool isCheckShinQuantity(DataTable dtSetHin, DataTable dtDetail, DataTable dtBuzaiDetail, out string strErrMsg)
        {
            bool blnResult = true;
            strErrMsg = string.Empty;

            decimal dcmQuantity = 0;
            decimal dcmBuzaiQuantity = 0;
            int intUnit = 0;

            DataTable dtDetailTmp = dtDetail.Copy();
            DataTable dtBuzaiDetailTmp = dtBuzaiDetail.Copy();
            dtDetailTmp.AcceptChanges();
            dtBuzaiDetailTmp.AcceptChanges();

            // 明細より数量を取得
            foreach (DataRow row in dtDetailTmp.Rows)
            {
                dcmQuantity = Decimal.Parse(row["数量"].ToString());
            }

            // 構成品マスタをLOOP
            foreach (DataRow row in dtSetHin.Rows)
            {
                intUnit = int.Parse(row["使用数量"].ToString());

                // 品番単位に数量の合算を取得
                var varTotalQuantity = dtBuzaiDetailTmp.AsEnumerable()
                    .Where(w => w.Field<int>("品番コード") == int.Parse(row["構成品番コード"].ToString()))
                    .GroupBy(g => new { 品番コード = g.Field<int>("品番コード") })
                    .Select(s => new
                    {
                        品番コード = s.Key.品番コード,
                        数量合計 = s.Sum(m => m.Field<decimal>("数量"))
                    })
                    .FirstOrDefault();

                if (varTotalQuantity == null)
                {
                    dcmBuzaiQuantity = 0;
                }
                else
                {
                    dcmBuzaiQuantity = varTotalQuantity.数量合計;
                }

                if (dcmBuzaiQuantity < (dcmQuantity * intUnit))
                {
                    // 数量が数量×必要数量を満たさない場合
                    strErrMsg = "構成品の数量が不足しています。";
                    blnResult = false;
                }
                else if (dcmBuzaiQuantity > (dcmQuantity * intUnit))
                {
                    // 数量が数量×必要数量を超える場合
                    strErrMsg = "構成品の数量が製品数量を超えています。";
                    blnResult = false;
                }
            }

            return blnResult;
        }
        #endregion

        #endregion

        /// <summary>
        /// キー項目としてマークされた項目の入力可否を切り替える
        /// </summary>
        /// <param name="flag">true:入力可、false:入力不可</param>
        private void ChangeKeyItemChangeable(bool flag)
        {
            base.ChangeKeyItemChangeable(flag);

            gridDtl.SetEnabled(!flag);

        }

        // No-162-3 Add Start
        #region 画面ヘッダ部の入力制御
        /// <summary>
        /// 画面ヘッダ部の入力設定を行う
        /// </summary>
        /// <param name="blnEnabled">true:入力可、false:入力不可</param>
        private void setDispHeaderEnabled(bool blnEnabled)
        {
            txt外注先.IsEnabled = blnEnabled;
            txt入荷先.IsEnabled = blnEnabled;
            cmb加工区分.IsEnabled = blnEnabled;
        }
        #endregion

        #region 画面リボンの入力制御
        /// <summary>
        /// 画面リボンの入力制御
        /// </summary>
        /// <param name="blnEnabled">true:入力可、false:入力不可</param>
        private void SetDispRibbonEnabled(bool blnEnabled)
        {
            // 使用設定（可・不可）
            this.RibbonF5.IsEnabled = blnEnabled;
            this.RibbonF6.IsEnabled = blnEnabled;
        }

        #endregion
        // No-162-3 Add End

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
                        T04_GetData,
                        new object[] {
                            this.txt会社名.Text1,
                            this.txt伝票番号.Text,
                            ccfg.ユーザID
                        }));

            }

        }

        /// <summary>
        /// 外注先コード・枝番が変更された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt外注先_cTextChanged(object sender, RoutedEventArgs e)
        {
            // どちらかが入力されていない場合は処理しない
            if (string.IsNullOrEmpty(this.txt外注先.Text1) || string.IsNullOrEmpty(this.txt外注先.Text2))
            {
                this.txt外注先.Label2Text = string.Empty;
                return;
            }

            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    MasterCode_Supplier,
                    new object[] {
                        this.txt外注先.DataAccessName,
                        this.txt外注先.Text1,
                        this.txt外注先.Text2,
                        this.txt外注先.LinkItem
                    }));

        }

        /// <summary>
        /// 加工区分変更後のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb加工区分_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UcLabelComboBox cmb = sender as UcLabelComboBox;

            if (cmb.SelectedValue == null)
                return;

            bool is社内加工 = cmb.SelectedValue.Equals(3);

            this.txt外注先.Text1 = is社内加工 ? 社内加工_コード : string.Empty;
            this.txt外注先.Text2 = is社内加工 ? 社内加工_枝番 : string.Empty;
            this.txt外注先.IsEnabled = !is社内加工;

        }

        /// <summary>
        /// 入荷先が変更された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt入荷先_cText1Changed(object sender, RoutedEventArgs e)
        {
            // 会社名が変更される事になるので部材明細を再取得(在庫数が変動する)
            if (!this.txt入荷先.CheckValidation())
                return;

            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    T04_GetDTB,
                    new object[] {
                        this.txt伝票番号.Text,
                        int.Parse(this.txt入荷先.Text1)
                    }));

        }

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
            //base.SendRequest(
            //    new CommunicationObject(
            //        MessageType.RequestData,
            //        T02_GetData,
            //        new object[] {
            //                this.txt自社名.Text1,
            //                this.txt伝票番号.Text,
            //                sendParam,
            //                ccfg.ユーザID
            //            }));

        }

        /// <summary>
        /// 仕上り日が変更された後のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt仕上り日_cTextChanged(object sender, RoutedEventArgs e)
        {
            // 明細内容・消費税の再計算を実施
            summaryCalculation();
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

            if (DateTime.TryParse(txt仕上り日.Text, out date))
            {
                foreach (DataRow row in SearchDetail.Rows)
                {
                    if (row.RowState == DataRowState.Deleted)
                        continue;

                    // 自社品番が空値(行追加のみのデータ)は処理対象外とする
                    if (string.IsNullOrEmpty(row["自社品番"].ToString()))
                        continue;

                    // No.272 Mod Start
                    int ival = 0;
                    int taxKbnId = int.TryParse(SearchHeader["Ｓ税区分ID"].ToString(), out ival)? ival : 1;
                    int salesTaxKbn = int.TryParse(SearchHeader["Ｓ支払消費税区分"].ToString(), out ival)? ival : 1;
                    // ▼揚り入力は全て通常税率 Mod Start
                    //conTax += taxCalc.CalculateTax(date, row.Field<int>("金額"), row.Field<int>("消費税区分"), taxKbnId, salesTaxKbn);
                    conTax += taxCalc.CalculateTax(date, row.Field<int>("金額"), (int)消費税区分.通常税率, taxKbnId, salesTaxKbn);
                    // ▲揚り入力は全て通常税率 Mod End
                    // No.272 Mod End
                }

                long total = (long)(subTotal + conTax);

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
        private void sp製品一覧_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
        {
            GcSpreadGrid grid = sender as GcSpreadGrid;
            string targetColumn = grid.ActiveCellPosition.ColumnName;

            if (e.EditAction == SpreadEditAction.Cancel)
                return;

            //明細行が存在しない場合は処理しない
            if (SearchDetail == null) return;
            if (SearchDetail.Select("", "", DataViewRowState.CurrentRows).Count() == 0) return;

            _編集行 = e.CellPosition.Row;

            switch (targetColumn)
            {
                case "自社品番":
                    var target = gridDtl.GetCellValueToString();
                    if (string.IsNullOrEmpty(target))
                        return;

                    // 自社品番からデータを参照し、取得内容をグリッドに設定
                    base.SendRequest(
                        new CommunicationObject(
                            MessageType.RequestData,
                            MasterCode_MyProduct,
                            new object[] {
                                target.ToString()
                                // No-65 Add Strat
                               ,string.Empty,
                                string.Empty
                                // No-65 Add End
                            }));
                    break;

                case "単価":
                    // 金額の再計算
                    decimal costt = gridDtl.GetCellValueToDecimal((int)GridColumnsMapping.単価) ?? 0;
                    decimal qtyt = gridDtl.GetCellValueToDecimal((int)GridColumnsMapping.数量) ?? 0;

                    gridDtl.SetCellValue((int)GridColumnsMapping.金額, Convert.ToInt32(decimal.Multiply(costt, qtyt)));

                    // グリッド内容の再計算を実施
                    summaryCalculation();

                    SearchDetail.Rows[gridDtl.ActiveRowIndex].EndEdit();

                    break;

                case "数量":
                    // 金額の再計算
                    decimal costs = gridDtl.GetCellValueToDecimal((int)GridColumnsMapping.単価) ?? 0;
                    decimal qtys = gridDtl.GetCellValueToDecimal((int)GridColumnsMapping.数量) ?? 0;

                    gridDtl.SetCellValue((int)GridColumnsMapping.金額, Convert.ToInt32(decimal.Multiply(costs, qtys)));

                    // グリッド内容の再計算を実施
                    summaryCalculation();

                    // セット品情報を取得する
                    sendSearchForShin(gridDtl.GetCellValueToString((int)GridColumnsMapping.品番コード) ?? "0");

                    // 揚り部材明細情報を取得する
                    sendSearchForAgrDtb();

                    SearchDetail.Rows[gridDtl.ActiveRowIndex].EndEdit();

                    break;

                case "賞味期限":
                    SearchDetail.Rows[gridDtl.ActiveRowIndex].EndEdit();

                    break;

                default:
                    if (gridDtl.ActiveRowIndex >= 0)
                    {
                        // EndEditが行われずに登録すると変更内容が反映されないため処理追加
                        SearchDetail.Rows[gridDtl.ActiveRowIndex].EndEdit();
                    }
                    break;

            }

        }

        /// <summary>
        /// 選択セルが変更された後のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sp製品一覧_SelectionChanged(object sender, EventArgs e)
        {
            GcSpreadGrid grid = sender as GcSpreadGrid;
        }

        /// <summary>
        /// グリッド上でキーが押される時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sp製品一覧_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && !sp製品一覧.ActiveCell.IsEditing)
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
            if (sp製品一覧.RowCount - 1 < rIdx || rIdx < 0)
                return null;

            return sp製品一覧.Cells[rIdx, column.GetHashCode()].Value;

        }

        /// <summary>
        /// 指定セルの値を設定する
        /// </summary>
        /// <param name="rIdx">行番号</param>
        /// <param name="column">列定義</param>
        /// <param name="value">設定値</param>
        private void setSpreadGridValue(int rIdx, GridColumnsMapping column, object value)
        {
            if (sp製品一覧.RowCount - 1 < rIdx || rIdx < 0)
                return;

            sp製品一覧.Cells[rIdx, column.GetHashCode()].Value = value;

        }

        #region 明細クリック時のアクション定義
        /// <summary>
        /// 明細クリック時のアクション定義
        /// </summary>
        public class cmd構成部品 : ICommand
        {
            private GcSpreadGrid _gcSpreadGrid;
            public cmd構成部品(GcSpreadGrid gcSpreadGrid)
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
                    int spRowIdx = cellCommandParameter.CellPosition.Row;
                    var spRow = this._gcSpreadGrid.Rows[spRowIdx];
                    var wnd = GetWindow(this._gcSpreadGrid);

                    var parent = ViewBaseCommon.FindVisualParent<DLY02010>(this._gcSpreadGrid);
                    try
                    {
                        MouseButtonEventArgs e = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left);
                        e.RoutedEvent = UIElement.MouseLeftButtonDownEvent;
                        e.Source = this;

                        // 遷移判定
                        var objHinNM = spRow.Cells[(int)GridColumnsMapping.自社品名].Value;
                        if (objHinNM == null || string.IsNullOrEmpty(objHinNM.ToString()))
                        {
                            return;
                        }

                        // DLY02011 引き渡しデータ作成
                        // 製品一覧(AGRDTL)
                        DataTable dtDetail = new DataTable("T04_AGRDTL");
                        dtDetail = parent.SearchDetail.Clone();
                        DataRow rowDetail = dtDetail.NewRow();

                        rowDetail = parent.SearchDetail.Rows[spRowIdx];
                        dtDetail.ImportRow(rowDetail);

                        // 部材一覧(AGRDTB)
                        DataTable dtInnerDetail = new DataTable("T04_AGRDTB_Extension");
                        // 揚り部材明細 配列から取得する
                        List<T04_AGRDTB_Extension> lstInnerDetail = parent.getAgrDtbList(spRowIdx);
                        AppCommon.ConvertToDataTable(lstInnerDetail, dtInnerDetail);

                        // 画面表示
                        DLY02011 frmDLY02011 = new DLY02011();
                        frmDLY02011.MaintenanceMode = parent.MaintenanceMode;
                        frmDLY02011.SearchHeader = parent.SearchHeader;
                        frmDLY02011.SearchDetail = dtDetail.Copy();
                        frmDLY02011.InnerDetail = dtInnerDetail.Copy();

                        if (frmDLY02011.ShowDialog(wnd) == true)
                        {
                            // 保存終了
                            // 揚り部材明細 配列に追加する
                            parent.AddAgrDtbList(spRowIdx, frmDLY02011.InnerDetail);
                        }

                    }
                    catch (Exception ex)
                    {
                        parent.ErrorMessage = ex.Message;
                    }
                }
            }
        }

        #endregion

        #endregion

        #region <<揚り部材明細 内部データ制御>>
        /// <summary>
        /// 揚り部材明細 配列を初期化する
        /// </summary>
        private void clearAgrDtbList()
        {

            for (int i = 0; i <= 9; i++)
            {
                if (T04_AGRDTB_List[i] != null && T04_AGRDTB_List[i].Count > 0)
                {
                    T04_AGRDTB_List[i].Clear();
                }
            }

            return;
        }

        /// <summary>
        /// 揚り部材明細 配列に追加する
        /// </summary>
        /// <param name="intIdx">追加Index</param>
        /// <param name="tbl">揚り部材明細データテーブル</param>
        private void AddAgrDtbList(int intIdx, DataTable tbl)
        {

            if (T04_AGRDTB_List[intIdx] != null && T04_AGRDTB_List[intIdx].Count > 0)
            {
                T04_AGRDTB_List[intIdx].Clear();
            }

            List<T04_AGRDTB_Extension> lstAgrDtb = (List<T04_AGRDTB_Extension>)AppCommon.ConvertFromDataTable(typeof(List<T04_AGRDTB_Extension>), tbl);

            T04_AGRDTB_List[intIdx] = lstAgrDtb;

            return;
        }

        /// <summary>
        /// 揚り部材明細 配列から削除する(初期化)
        /// </summary>
        /// <param name="intIdx">削除Index</param>
        private void deleteAgrDtbList(int intIdx)
        {
            T04_AGRDTB_List[intIdx].Clear();

            for(int i = intIdx + 1 ; i <= 9; i++)
            {
                T04_AGRDTB_List[i - 1] = T04_AGRDTB_List[i];
            }

            return;
        }

        /// <summary>
        /// 揚り部材明細 配列から取得する
        /// </summary>
        /// <param name="intIdx">取得Index</param>
        /// <returns>List<T04_AGRDTB_Extension></returns>
        private List<T04_AGRDTB_Extension> getAgrDtbList(int intIdx)
        {
            List<T04_AGRDTB_Extension> lstAgrDtb = T04_AGRDTB_List[intIdx];

            return lstAgrDtb;
        }
        #endregion
    }

}
