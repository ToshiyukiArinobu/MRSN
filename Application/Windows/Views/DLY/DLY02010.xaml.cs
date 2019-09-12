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
        /// <summary>在庫の存在チェック</summary>
        private const string T04_STOK_CHECK = "T04_STOK_CHECK";

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
        /// <summary>揚り部材明細情報(画面表示用)</summary>
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
        /// 編集中の行番号
        /// </summary>
        private int _編集行;

        #endregion

        #region << クラス変数定義 >>

        /// <summary>グリッドコントローラ</summary>
        GcSpreadGridController gridDtl;
        GcSpreadGridController gridDtb;

        /// <summary>消費税計算</summary>
        TaxCalculator taxCalc;

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
            gridDtl = new GcSpreadGridController(gcSpreadGrid);
            gridDtb = new GcSpreadGridController(sgInnerDetail);

            ScreenClear();
            ChangeKeyItemChangeable(true);

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
                    case T04_GetData:
                        // 伝票検索または新規伝票の場合
                        DataSet ds = data as DataSet;
                        if (ds != null)
                        {
                            SetTblData(ds);
                            ChangeKeyItemChangeable(false);
                            txt仕上り日.Focus();
                        }
                        else
                        {
                            MessageBox.Show("指定の伝票番号は登録されていません。", "伝票未登録", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            this.txt伝票番号.Focus();
                        }
                        break;

                    //20190724CB-S
                    case T04_STOK_CHECK:

                        if (MessageBox.Show(AppConst.CONFIRM_UPDATE,
                                "登録確認",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question,
                                MessageBoxResult.Yes) == MessageBoxResult.No)
                                return;

                        if (!(bool)data) 
                        {
                            if (MessageBox.Show("構成品の在庫が足りないですが、登録しますか？",
                                "登録確認",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question,
                                MessageBoxResult.Yes) == MessageBoxResult.No)
                                return;
                        }

                        Update();

                        break;
                    //20190724CB-E

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
                        _innerDetailDtb = S03_STOK_DisplayConvert(tbl);
                        break;

                    case T04_CreateDTB:
                        // 数量の計算を実施
                        InnerDetail = S03_STOK_DisplayConvert(tbl);
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

                        }
                        else if (tbl.Rows.Count > 1)
                        {
                            int cIdx = gcSpreadGrid.ActiveColumnIndex;
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
                            myhin.txtCode.Text = colVal;
                            myhin.txtCode.IsEnabled = false;
                            myhin.TwinTextBox.LinkItem = 1;

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
                                    gridDtl.SetCellValue((int)GridColumnsMapping.単価, myhin.TwinTextBox.Text3);
                                    gridDtl.SetCellValue((int)GridColumnsMapping.金額, string.IsNullOrEmpty(myhin.TwinTextBox.Text3) ? 0 :
                                                                                    decimal.ToInt32(AppCommon.DecimalParse(myhin.TwinTextBox.Text3)));
                                }
                                // No-96 Mod End

                                gridDtl.SetCellValue((int)GridColumnsMapping.消費税区分, myhin.SelectedRowData["消費税区分"]);
                                gridDtl.SetCellValue((int)GridColumnsMapping.商品分類, myhin.SelectedRowData["商品分類"]);

                                // 20190530CB-S
                                gridDtl.SetCellValue((int)GridColumnsMapping.色コード, myhin.SelectedRowData["自社色"]);
                                gridDtl.SetCellValue((int)GridColumnsMapping.色名称, myhin.SelectedRowData["自社色名"]);
                                // 20190530CB-E

                                // 集計計算をおこなう
                                summaryCalculation();

                                // 部材明細情報を取得する
                                gcSpreadGrid_SelectionChanged(gcSpreadGrid, null);

                            }

                        }
                        else
                        {
                            // 対象データありの場合
                            DataRow drow = ctbl.Rows[0];
                            gcSpreadGrid.BeginEdit();
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
                                gridDtl.SetCellValue((int)GridColumnsMapping.単価, drow["加工原価"]);
                                gridDtl.SetCellValue((int)GridColumnsMapping.金額, drow["加工原価"] == null ? 0 : Convert.ToInt32(drow["加工原価"]));
                            }
                            // No-96 Mod End

                            gridDtl.SetCellValue((int)GridColumnsMapping.消費税区分, drow["消費税区分"]);
                            gridDtl.SetCellValue((int)GridColumnsMapping.商品分類, drow["商品分類"]);

                            // 20190530CB-S
                            gridDtl.SetCellValue((int)GridColumnsMapping.色コード, drow["自社色"]);
                            gridDtl.SetCellValue((int)GridColumnsMapping.色名称, drow["色名称"]);                 // No-65 Mod
                            // 20190530CB-E

                            gcSpreadGrid.CommitCellEdit();
                            // 自社品番のセルをロック
                            // 数量以外はロック
                            gridDtl.SetCellLocked((int)GridColumnsMapping.品番コード, true);

                            // 20190704CB-S
                            gridDtl.SetCellLocked((int)GridColumnsMapping.自社品番, true);
                            gridDtl.SetCellLocked((int)GridColumnsMapping.自社品名, true);
                            gridDtl.SetCellLocked((int)GridColumnsMapping.単位, true);
                            gridDtl.SetCellLocked((int)GridColumnsMapping.単価, true);
                            gridDtl.SetCellLocked((int)GridColumnsMapping.金額, true);
                            gridDtl.SetCellLocked((int)GridColumnsMapping.消費税区分, true);
                            gridDtl.SetCellLocked((int)GridColumnsMapping.商品分類, true);
                            gridDtl.SetCellLocked((int)GridColumnsMapping.色コード, true);
                            gridDtl.SetCellLocked((int)GridColumnsMapping.色名称, true);
                            // 20190704CB-E

                            summaryCalculation();

                            // 部材明細情報を取得する
                            gcSpreadGrid_SelectionChanged(gcSpreadGrid, null);

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

        /// <summary>
        /// 在庫情報を表示用に加工する
        /// </summary>
        /// <param name="tbl"></param>
        private DataTable S03_STOK_DisplayConvert(DataTable tbl)
        {
            DateTime maxDate = AppCommon.GetMaxDate();
            foreach (DataRow row in tbl.Rows)
            {
                decimal impQty = gridDtl.GetCellValueToDecimal((int)GridColumnsMapping.数量) ?? 0;
                decimal needQty = decimal.Parse(row["必要数量"].ToString());
                row["数量"] = (needQty == 0 ? 1 : needQty) * impQty;

                // 賞味期限が最大値の場合は表示させない
                //20190530CB テストのため一時的にlengthのif文を追加
                if (row["賞味期限"].ToString().Length > 0)
                {
                    if (DateTime.Parse(row["賞味期限"].ToString()).Equals(maxDate))
                        row["賞味期限"] = DBNull.Value;
                }
            }

            return tbl;

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
                SearchDetail.Clear();
            if (_innerDetailDtb != null)
                _innerDetailDtb = null;
            if (InnerDetail != null)
                InnerDetail.Clear();

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
                                spgrid.Cells[rIdx, (int)GridColumnsMapping.単価].Value = myhin.TwinTextBox.Text3;
                                spgrid.Cells[rIdx, (int)GridColumnsMapping.金額].Value = string.IsNullOrEmpty(myhin.TwinTextBox.Text3) ?
                                                                                                        0 : decimal.ToInt32(AppCommon.DecimalParse(myhin.TwinTextBox.Text3));
                            }
                            // No-96 Mod End
                            
                            spgrid.Cells[rIdx, (int)GridColumnsMapping.消費税区分].Value = myhin.SelectedRowData["消費税区分"];
                            spgrid.Cells[rIdx, (int)GridColumnsMapping.商品分類].Value = myhin.SelectedRowData["商品分類"];

                            // 20190530CB-S
                            gridDtl.SetCellValue((int)GridColumnsMapping.色コード, myhin.SelectedRowData["自社色"]);
                            gridDtl.SetCellValue((int)GridColumnsMapping.色名称, myhin.SelectedRowData["自社色名"]);
                            // 20195030CB-E

                            // 設定自社品番の編集を不可とする
                            gridDtl.SetCellLocked((int)GridColumnsMapping.自社品番, true);

                            // 集計計算をおこなう
                            summaryCalculation();

                            // 部材明細情報を取得する
                            gcSpreadGrid_SelectionChanged(spgrid, null);

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
            }
            catch
            {
                SearchDetail.Rows[gridDtl.ActiveRowIndex].Delete();
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
            if (MaintenanceMode == null || SearchDetail == null)
                return;
            //20190724CB-S
            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    T04_STOK_CHECK,
                    new object[] {
                        SearchDetail.DataSet,
                        ccfg.ユーザID
                    }));
            //Update();
            //20190724CB-E
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
                foreach (var row in gcSpreadGrid.Rows)
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
            if (!isFormValidation())
                return;

            // 全項目エラーチェック
            if (!base.CheckAllValidation())
            {
                this.txt仕上り日.Focus();
                return;
            }

            //if (MessageBox.Show(AppConst.CONFIRM_UPDATE,
            //                    "登録確認",
            //                    MessageBoxButton.YesNo,
            //                    MessageBoxImage.Question,
            //                    MessageBoxResult.Yes) == MessageBoxResult.No)
            //    return;

            // -- 送信用データを作成 --
            // 消費税をヘッダに設定
            SearchHeader["消費税"] = AppCommon.IntParse(this.lbl消費税.Content.ToString(), System.Globalization.NumberStyles.Number);

            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    T04_Update,
                    new object[] {
                        SearchDetail.DataSet,
                        ccfg.ユーザID
                    }));

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
        /// <returns></returns>
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
        /// <returns></returns>
        private bool isFormValidation()
        {
            bool isResult = false;
            // 【ヘッダ】必須入力チェック
            // 仕上り日
            if (string.IsNullOrEmpty(this.txt仕上り日.Text))
            {
                base.ErrorMessage = string.Format("仕入日が入力されていません。");
                this.txt仕上り日.Focus();
                return isResult;

            }

            // 加工区分
            if (this.cmb加工区分.SelectedValue == null)
            {
                base.ErrorMessage = string.Format("加工区分が選択されていません。");
                this.cmb加工区分.Focus();
                return isResult;

            }

            // 仕入先
            if (string.IsNullOrEmpty(this.txt外注先.Text1) || string.IsNullOrEmpty(this.txt外注先.Text2))
            {
                base.ErrorMessage = string.Format("外注先が入力されていません。");
                this.txt外注先.Focus();
                return isResult;

            }

            // 入荷先
            if (string.IsNullOrEmpty(this.txt入荷先.Text1))
            {
                base.ErrorMessage = string.Format("入荷先が入力されていません。");
                this.txt入荷先.Focus();
                return isResult;

            }

            // 現在の明細行を取得
            var CurrentDetail = SearchDetail.Select("", "", DataViewRowState.CurrentRows).AsEnumerable();

            // 【明細】詳細データが１件もない場合はエラー
            if (SearchDetail == null || CurrentDetail.Where(a => !string.IsNullOrEmpty(a.Field<string>("自社品番"))).Count() == 0)
            {
                base.ErrorMessage = string.Format("明細情報が１件もありません。");
                gridDtl.SpreadGrid.Focus();
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

                DateTime? row賞味期限 = DBNull.Value.Equals(row["賞味期限"]) ? (DateTime?)null : Convert.ToDateTime(row["賞味期限"]);
                int? row品番コード = DBNull.Value.Equals(row["品番コード"]) ? (int?)null : Convert.ToInt32(row["品番コード"]);
                if (CurrentDetail.Where(x => x.Field<int?>("品番コード") == row品番コード && x.Field<DateTime?>("賞味期限") == row賞味期限).Count() > 1)
                {
                    gridDtl.SpreadGrid.Focus();
                    base.ErrorMessage = string.Format("同じ商品が存在するので、一つに纏めて下さい。");
                    gridDtl.AddValidationError(rIdx, (int)GridColumnsMapping.品番コード, "同じ商品が存在するので、一つに纏めて下さい。");
                    if (!isDetailErr)
                        gridDtl.SetCellFocus(rIdx, (int)GridColumnsMapping.品番コード);

                    isDetailErr = true;
                }

                if (string.IsNullOrEmpty(row["数量"].ToString()))
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

        #endregion

        /// <summary>
        /// キー項目としてマークされた項目の入力可否を切り替える
        /// </summary>
        /// <param name="flag">true:入力可、false:入力不可</param>
        private void ChangeKeyItemChangeable(bool flag)
        {
            base.ChangeKeyItemChangeable(flag);

            gridDtl.SetEnabled(!flag);
            gridDtb.SetEnabled(!flag);

        }

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

                    int taxKbnId = int.Parse(SearchHeader["Ｓ税区分ID"].ToString());
                    conTax += taxCalc.CalculateTax(date, row.Field<int>("金額"), row.Field<int>("消費税区分"), taxKbnId);

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
        private void gcSpredGrid_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
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
                case "数量":
                    // 金額の再計算
                    decimal cost = gridDtl.GetCellValueToDecimal((int)GridColumnsMapping.単価) ?? 0;
                    decimal qty = gridDtl.GetCellValueToDecimal((int)GridColumnsMapping.数量) ?? 0;

                    gridDtl.SetCellValue((int)GridColumnsMapping.金額, Convert.ToInt32(decimal.Multiply(cost, qty)));

                    // グリッド内容の再計算を実施
                    summaryCalculation();

                    SearchDetail.Rows[gridDtl.ActiveRowIndex].EndEdit();

                    break;

                default:
                    break;

            }

        }

        /// <summary>
        /// 選択セルが変更された後のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcSpreadGrid_SelectionChanged(object sender, EventArgs e)
        {
            GcSpreadGrid grid = sender as GcSpreadGrid;

            if (_innerDetailDtb != null && _innerDetailDtb.Rows.Count > 0)
            {
                // 部材明細登録あり(変更時)
                // No-66 Mod Start
                //// ⇒該当品番コードより該当するテーブルデータを表示
                //DataView dv = _innerDetailDtb.Copy().AsDataView();

                //var pCode = getSpreadGridValue(grid.ActiveRowIndex, GridColumnsMapping.品番コード);

                //// 編集時の行追加でnullになるパターンがあり得る為
                //if (pCode == null)
                //    return;

                //dv.RowFilter = string.Format("セット品番コード = {0}", pCode);

                //InnerDetail = dv.ToTable();
                var num = getSpreadGridValue(grid.ActiveRowIndex, GridColumnsMapping.品番コード);
                var code = this.txt入荷先.Text1;

                if (num == null || string.IsNullOrEmpty(num.ToString()))
                {
                    if (InnerDetail != null)
                        InnerDetail.Clear();

                    return;

                }

                // param<1:品番コード(string)、2:会社コード(string)>
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        T04_CreateDTB,
                        new object[] {
                                num.ToString(),
                                code.ToString()
                            }));
                // No-66 Mod End
            }
            else
            {
                // 部材明細登録なし(新規登録時)
                // ⇒設定されている品番コードよりデータを取得
                var num = getSpreadGridValue(grid.ActiveRowIndex, GridColumnsMapping.品番コード);
                var code = this.txt入荷先.Text1;

                if (num == null || string.IsNullOrEmpty(num.ToString()))
                {
                    if (InnerDetail != null)
                        InnerDetail.Clear();

                    return;

                }

                // 20190528CB-S
                // セット品番の構成品が登録されているかチェック
                base.SendRequest(new CommunicationObject(MessageType.RequestData, M10_GetCount, new object[] { num }));
                // 20190528CB-E

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
                                code.ToString()
                            }));

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

        #endregion

    }

}
