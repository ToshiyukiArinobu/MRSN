using GrapeCity.Windows.SpreadGrid;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Application.Windows.Views.Common;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;


namespace KyoeiSystem.Application.Windows.Views
{
    using FwRepPreview = KyoeiSystem.Framework.Reports.Preview;
    using WinForms = System.Windows.Forms;
    using KyoeiSystem.Framework.Windows.Controls;
    using System.Windows.Media;


    /// <summary>
    /// 場所別棚卸入力 フォームクラス
    /// </summary>
    public partial class ZIK02010 : WindowReportBase
    {
        #region バインド用変数(SPREAD項目)

        /// <summary>
        /// スプレッドシート列定義クラス
        /// </summary>
        public class S10_STOCKTAKING_Member : INotifyPropertyChanged
        {
            private int? _倉庫コード;
            private string _倉庫名;
            private int? _品番コード;
            private string _自社品番;
            private string _自社品名;
            private string _自社色;
            private DateTime? _賞味期限;
            private decimal? _数量;
            private string _単位;
            private decimal? _実在庫数;

            public int? 倉庫コード { get { return _倉庫コード; } set { _倉庫コード = value; NotifyPropertyChanged(); } }
            public string 倉庫名 { get { return _倉庫名; } set { _倉庫名 = value; NotifyPropertyChanged(); } }
            public int? 品番コード { get { return _品番コード; } set { _品番コード = value; NotifyPropertyChanged(); } }
            public string 自社品番 { get { return _自社品番; } set { _自社品番 = value; NotifyPropertyChanged(); } }
            public string 自社品名 { get { return _自社品名; } set { _自社品名 = value; NotifyPropertyChanged(); } }
            public string 自社色 { get { return _自社色; } set { _自社色 = value; NotifyPropertyChanged(); } }
            public DateTime? 賞味期限 { get { return _賞味期限; } set { _賞味期限 = value; NotifyPropertyChanged(); } }
            public decimal? 数量 { get { return _数量; } set { _数量 = value; NotifyPropertyChanged(); } }
            public string 単位 { get { return _単位; } set { _単位 = value; NotifyPropertyChanged(); } }
            public decimal? 実在庫数 { get { return _実在庫数; } set { _実在庫数 = value; NotifyPropertyChanged(); } }


            #region INotifyPropertyChanged メンバー

            /// <summary>
            /// Binding機能対応（プロパティの変更通知イベント）
            /// </summary>
            public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
            /// <summary>
            /// Binding機能対応（プロパティの変更通知イベント送信）
            /// </summary>
            /// <param name="propertyName">Bindingプロパティ名</param>
            public void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
                }
            }

            #endregion
        }

        #endregion

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;

        /// <summary>
        /// 画面固有設定項目のクラス定義
        /// ※ 必ず public で定義する。
        /// </summary>
        public class ConfigZIK02010 : FormConfigBase
        {
            public byte[] spConfig = null;
        }

        /// ※ 必ず public で定義する。
        public ConfigZIK02010 frmcfg = null;
        CommonConfig ccfg = null;
        // SPREAD初期状態保存用
        public byte[] spConfig = null;

        #endregion

        #region << 列挙型定義 >>

        private enum GridColumnsMapping : int
        {
            倉庫コード = 0,
            倉庫名 = 1,
            品番コード = 2,
            自社品番 = 3,
            自社品名 = 4,
            自社色 = 5,
            賞味期限 = 6,
            数量 = 7,
            単位 = 8,
            実在数量 = 9,
        }


        /// <summary>
        /// 自社販社区分 内包データ
        /// </summary>
        private enum 自社販社区分 : int
        {
            自社 = 0,
            販社 = 1
        }

        private enum AddEditFlg : int
        {
            新規 = 0,
            編集 = 1,
        }
        #endregion

        #region << 定数定義 >>

        private const string INITIAL_DEL_PROCESS = "ZIK02010_IsInitialDelProcess";
        private const string CHECK_STOCKTAKING = "ZIK02010_IsCheckStocktaking";
        private const string GET_STOCKTAKING = "ZIK02010_GetStockTaking";

        private const string CHECK_ADDROWDATA = "ZIK02010_CheckAddRowData";
        private const string UPDATE_S10_STOCKTAKING = "ZIK02010_InventoryStocktaking";


        /// <summary>倉庫情報取得</summary>
        private const string GET_SOUK = "ZIK02010_GetSOUK";

        /// <summary>
        /// 商品マスタ情報取得
        /// </summary>
        private const string GET_HIN = "ZIK02010_GetHIN";

        /// <summary>自社品番情報取得</summary>
        private const string GET_UcCustomerProduct = "UcCustomerProduct";

        /// <summary>帳票定義体ファイルパス</summary>
        private const string ReportFileName = @"Files\ZIK\ZIK02010.rpt";


        #endregion

        #region バインディングデータ

        /// <summary>追加行情報</summary>
        private List<S10_STOCKTAKING_Member> _addRowDetailList;
        public List<S10_STOCKTAKING_Member> AddRowDetailList
        {
            get { return _addRowDetailList; }
            set
            {
                _addRowDetailList = value;
                this.sp新規明細データ.ItemsSource = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>棚卸在庫情報</summary>
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
        /// 新規/編集Flg
        /// 0:新規、1:編集
        /// </summary>
        private int iModeFlg;

        #endregion

        #region クラス変数定義

        // 追加行グリッドコントローラー
        GcSpreadGridController addGridCtl;

        // 棚卸在庫グリッドコントローラー
        GcSpreadGridController stokGridCtl;

        #endregion

        #region << 画面初期処理 >>

        /// <summary>
        /// シリーズ･商品別売上統計表 コンストラクタ
        /// </summary>
        public ZIK02010()
        {
            InitializeComponent();
            this.DataContext = this;

        }

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigZIK02010)ucfg.GetConfigValue(typeof(ConfigZIK02010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigZIK02010();
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

            // 検索画面情報を設定
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });
            base.MasterMaintenanceWindowList.Add("M09_MYHIN", new List<Type> { typeof(MST02010), typeof(SCHM09_MYHIN) });
            base.MasterMaintenanceWindowList.Add("M14_BRAND", new List<Type> { typeof(MST04020), typeof(SCHM14_BRAND) });
            base.MasterMaintenanceWindowList.Add("M15_SERIES", new List<Type> { typeof(MST04021), typeof(SCHM15_SERIES) });
            base.MasterMaintenanceWindowList.Add("M22_SOUK", new List<Type> { typeof(MST12020), typeof(SCHM22_SOUK) });

            // コンボデータ取得
            AppCommon.SetutpComboboxList(this.cmbItemType, false);
            addGridCtl = new GcSpreadGridController(sp新規明細データ);
            stokGridCtl = new GcSpreadGridController(sp棚卸明細データ);

            // 画面初期化
            ScreenClear();

            //Enterボタン処理イベント
            sp新規明細データ.InputBindings.Add(new KeyBinding(sp新規明細データ.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));
            sp棚卸明細データ.InputBindings.Add(new KeyBinding(sp棚卸明細データ.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));



            // 検索条件部の初期設定をおこなう
            initSearchControl();

            // 初期フォーカスを設定
            this.myCompany.SetFocus();
            ResetAllValidation();

            // 初回棚卸テーブル削除処理
            base.SendRequest(new CommunicationObject(MessageType.RequestData, INITIAL_DEL_PROCESS));
            // スプレッド初期化
            //InitSpread();
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
                var messageName = message.GetMessageName();
                var data = message.GetResultData();
                DataTable tbl = (data is DataTable) ? (data as DataTable) : null;

                switch (messageName)
                {

                    case CHECK_STOCKTAKING:

                        base.SetFreeForInput();

                        // パラメータ辞書の作成を行う
                        Dictionary<string, string> dicCond = setStocktakingParm();


                        // 新規/編集フラグ
                        iModeFlg = (int)AddEditFlg.新規;

                        //データが存在する場合
                        if (data != null)
                        {
                            DateTime dt = Convert.ToDateTime(data);

                            if (dt != DateTime.Parse(StocktakingDate.Text))
                            {
                                string date = ((DateTime)dt).ToLongDateString();
                                string str = "前回の入力（" + date + "締め）が存在します。\r\n続きを入力しますか？";
                                var ret = MessageBox.Show(str, "継続確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                                if (ret == MessageBoxResult.Yes)
                                {
                                    // 編集
                                    iModeFlg = (int)AddEditFlg.編集;
                                    StocktakingDate.Text = dt.ToShortDateString();
                                }
                            }
                            else
                            {
                                // 編集
                                iModeFlg = (int)AddEditFlg.編集;
                                StocktakingDate.Text = dt.ToShortDateString();
                            }
                        }

                        base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_STOCKTAKING, new object[] { int.Parse(myCompany.Text1), StocktakingDate.Text, dicCond, iModeFlg }));
                        base.SetBusyForInput();
                        break;

                    // ===========================
                    // 棚卸在庫テーブル情報取得
                    // ===========================
                    case GET_STOCKTAKING:

                        base.SetFreeForInput();

                        //データが存在する場合
                        if (tbl != null && tbl.Rows.Count > 0)
                        {
                            this.MaintenanceMode = iModeFlg == (int)AddEditFlg.新規 ? AppConst.MAINTENANCEMODE_ADD : AppConst.MAINTENANCEMODE_EDIT;
                            sp棚卸明細データ.ItemsSource = tbl.DefaultView;
                            SearchDetail = tbl;
                            SearchDetail.AcceptChanges();

                            // 品番追加フラグの行は背景色を変更
                            ChangeSpreadBackColor();

                            // 新規明細データ一覧初期化
                            InitNewDataRow();

                            StartSpreadEdit();
                        }
                        else
                        {
                            MessageBox.Show("在庫情報がありません。", "在庫未登録", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        break;

                    case CHECK_ADDROWDATA:

                        base.SetFreeForInput();

                        string errMsg = data.ToString();

                        if (string.IsNullOrEmpty(errMsg))
                        {

                            var nRow = SearchDetail.NewRow();
                            nRow["棚卸日"] = DateTime.Parse(StocktakingDate.Text);
                            nRow["倉庫コード"] = (int?)sp新規明細データ.Cells[0, "倉庫コード"].Value;
                            nRow["倉庫名"] = (string)sp新規明細データ.Cells[0, "倉庫名"].Value;
                            nRow["品番コード"] = (int?)sp新規明細データ.Cells[0, "品番コード"].Value;
                            nRow["自社品番"] = (string)sp新規明細データ.Cells[0, "自社品番"].Value;
                            nRow["自社品名"] = (string)sp新規明細データ.Cells[0, "自社品名"].Value;
                            nRow["賞味期限"] = AppCommon.DatetimeParse((string)sp新規明細データ.Cells[0, "賞味期限"].Value, DateTime.MaxValue);
                            nRow["数量"] = 0;
                            nRow["単位"] = (string)sp新規明細データ.Cells[0, "単位"].Value;
                            nRow["実在庫数"] = (decimal?)sp新規明細データ.Cells[0, "実在庫数"].Value;
                            nRow["品番追加FLG"] = 1;
                            SearchDetail.Rows.Add(nRow);

                            ChangeSpreadBackColor();

                            // 新規明細データ一覧初期化
                            InitNewDataRow();

                            // 行追加後は追加行を選択させる
                            // No-56 Start
                            int newRowIdx = SearchDetail.Select("", "", DataViewRowState.CurrentRows).AsEnumerable().Count() - 1;
                            // No-56 End
                            // TODO:追加行が表示されるようにしたかったが追加行の上行までしか移動できない...
                            stokGridCtl.ScrollShowCell(newRowIdx, (int)GridColumnsMapping.実在数量);
                            stokGridCtl.SetCellFocus(newRowIdx, (int)GridColumnsMapping.実在数量);
                        }
                        else
                        {
                            MessageBox.Show(errMsg,
                                                    "エラー",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.Error);

                        }
                        break;


                    case GET_SOUK:
                        #region 倉庫　手入力時
                        DataTable soukTbl = data as DataTable;
                        int rSoukIdx = sp棚卸明細データ.ActiveRowIndex;

                        if (soukTbl == null || soukTbl.Rows.Count == 0)
                        {
                            // 対象データなしの場合
                            addGridCtl.SetCellValue((int)GridColumnsMapping.倉庫コード, null);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.倉庫名, string.Empty);

                        }
                        else
                        {
                            // 対象データありの場合
                            DataRow drow = soukTbl.Rows[0];
                            addGridCtl.SetCellValue((int)GridColumnsMapping.倉庫コード, drow["倉庫コード"]);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.倉庫名, drow["倉庫名"]);
                        }
                        #endregion
                        break;

                    case GET_HIN:
                    case GET_UcCustomerProduct:
                        #region 自社品番・得意先品番 手入力時
                        DataTable ctbl = data as DataTable;
                        int rIdx = sp棚卸明細データ.ActiveRowIndex;
                        int columnIdx = sp棚卸明細データ.ActiveColumn.Index;


                        if (ctbl == null || ctbl.Rows.Count == 0)
                        {
                            // 対象データなしの場合

                            addGridCtl.SetCellValue((int)GridColumnsMapping.品番コード, null);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.自社品番, string.Empty);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.自社品名, string.Empty);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.数量, 0m);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.単位, string.Empty);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.自社色, string.Empty);

                        }
                        else if (ctbl.Rows.Count > 1)
                        {
                            // 対象データが複数ある場合
                            int cIdx = sp棚卸明細データ.ActiveColumnIndex;
                            string colVal = addGridCtl.GetCellValueToString((int)GridColumnsMapping.自社品番);

                            SCHM09_MYHIN myhin = new SCHM09_MYHIN();
                            myhin.txtCode.Text = colVal == null ? string.Empty : colVal.ToString();
                            myhin.txtCode.IsEnabled = false;
                            myhin.TwinTextBox = new UcLabelTwinTextBox();
                            myhin.TwinTextBox.LinkItem = 1;
                            if (myhin.ShowDialog(this) == true)
                            {
                                addGridCtl.SetCellValue((int)GridColumnsMapping.品番コード, myhin.SelectedRowData["品番コード"]);
                                addGridCtl.SetCellValue((int)GridColumnsMapping.自社品番, myhin.SelectedRowData["自社品番"]);
                                addGridCtl.SetCellValue((int)GridColumnsMapping.自社品名, myhin.SelectedRowData["自社品名"]);
                                addGridCtl.SetCellValue((int)GridColumnsMapping.数量, 0.00m);
                                addGridCtl.SetCellValue((int)GridColumnsMapping.単位, myhin.SelectedRowData["単位"]);
                                addGridCtl.SetCellValue((int)GridColumnsMapping.自社色, myhin.SelectedRowData["自社色名"]);
                            }
                        }
                        else
                        {
                            // 対象データありの場合
                            DataRow drow = ctbl.Rows[0];
                            addGridCtl.SetCellValue((int)GridColumnsMapping.品番コード, drow["品番コード"]);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.自社品番, drow["自社品番"]);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.自社品名, drow["自社品名"]);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.数量, 0.00m);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.単位, drow["単位"]);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.自社色, drow["色名称"]);

                        }

                        #endregion
                        break;

                    case UPDATE_S10_STOCKTAKING:
                        MessageBox.Show(AppConst.SUCCESS_UPDATE, "登録完了", MessageBoxButton.OK, MessageBoxImage.Information);
                        // コントロール初期化
                        ScreenClear();
                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                base.SetFreeForInput();
                this.ErrorMessage = ex.Message;
            }

        }

        /// <summary>
        /// データ受信エラー
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            base.SetFreeForInput();
            base.OnReveivedError(message);
            this.ErrorMessage = (string)message.GetResultData();
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
                    if (addGridCtl.ActiveColumnIndex == (int)GridColumnsMapping.倉庫コード)
                    {
                        //入力途中のセルを空欄状態に戻す
                        //gcAddRowGrid.CancelCellEdit();
                        sp新規明細データ.CommitCellEdit();

                        SCHM22_SOUK souk = new SCHM22_SOUK();
                        souk.TwinTextBox = new UcLabelTwinTextBox();

                        var soukVal = addGridCtl.GetCellValue(0, (int)GridColumnsMapping.倉庫コード);
                        souk.確定コード = soukVal == null ? string.Empty : soukVal.ToString();
                        if (souk.ShowDialog(this) == true)
                        {
                            sp新規明細データ.CommitCellEdit();
                            addGridCtl.SetCellValue((int)GridColumnsMapping.倉庫コード, souk.TwinTextBox.Text1);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.倉庫名, souk.TwinTextBox.Text2);
                        }

                    }
                    else if (addGridCtl.ActiveColumnIndex == (int)GridColumnsMapping.品番コード || addGridCtl.ActiveColumnIndex == (int)GridColumnsMapping.自社品番)
                    {
                        // 対象セルがロックされている場合は処理しない
                        if (addGridCtl.CellLocked == true)
                            return;

                        sp新規明細データ.CommitCellEdit();

                        // 自社品番または得意先品番の場合
                        SCHM09_MYHIN myhin = new SCHM09_MYHIN();
                        var jisHinVal = addGridCtl.GetCellValue(0, (int)GridColumnsMapping.自社品番);
                        myhin.txtCode.Text = jisHinVal == null ? string.Empty : jisHinVal.ToString();
                        myhin.TwinTextBox = new UcLabelTwinTextBox();
                        myhin.TwinTextBox.LinkItem = 0;
                        if (myhin.ShowDialog(this) == true)
                        {
                            //入力途中のセルを未編集状態に戻す
                            sp新規明細データ.CancelCellEdit();

                            addGridCtl.SetCellValue((int)GridColumnsMapping.自社品番, myhin.SelectedRowData["自社品番"].ToString());
                            addGridCtl.SetCellValue((int)GridColumnsMapping.自社品名, myhin.SelectedRowData["自社品名"].ToString());
                            addGridCtl.SetCellValue((int)GridColumnsMapping.品番コード, myhin.SelectedRowData["品番コード"].ToString());
                            addGridCtl.SetCellValue((int)GridColumnsMapping.自社色, myhin.SelectedRowData["自社色名"].ToString());
                            //addGridCtl.SetCellValue((int)GridColumnsMapping.数量, 1m);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.単位, myhin.SelectedRowData["単位"]);

                        }

                    }

                    SearchDetail.Rows[addGridCtl.ActiveRowIndex].EndEdit();

                    #endregion
                    //uctext.OpenSearchWindow(this);

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
        #endregion

        #region F4 CSV出力
        /// <summary>
        /// F4　リボン　CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF4Key(object sender, KeyEventArgs e)
        {
            // 入力チェック
            if (!isCheckInput())
                return;

            // パラメータ編集
            int companyCd;
            int.TryParse(myCompany.Text1, out companyCd);

            // パラメータ辞書情報設定
            Dictionary<string, string> cond = setSearchParams();

            sp棚卸明細データ.CommitCellEdit();

            foreach (DataRow row in SearchDetail.Rows)
            {

                if (row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added || row.RowState == DataRowState.Deleted)
                {
                    this.ErrorMessage = "変更データが存在します。登録してからCSV出力してください。";
                    MessageBox.Show("変更データが存在します。登録してからCSV出力してください。");
                    return;
                }
            }

            OutputCsv(SearchDetail);


        }
        #endregion

        #region F5 行追加
        /// <summary>
        /// F5　リボン　行追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            btnAdd_Click(null, null);
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
            if (this.MaintenanceMode == null) return;

            if (stokGridCtl.ActiveRowIndex < 0)
            {
                this.ErrorMessage = "行を選択してください";
                return;
            }

            int intDelRowIdx = stokGridCtl.ActiveRowIndex;


            int 品番追加FLG = this.sp棚卸明細データ.Columns.First(x => x.Name == "品番追加FLG").Index;

            if (!sp棚卸明細データ.Rows[intDelRowIdx].Cells[品番追加FLG].Value.Equals(1))
            {
                this.ErrorMessage = " 選択されている行を削除することはできません。";
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
                stokGridCtl.SpreadGrid.Rows.Remove(intDelRowIdx);
            }
            catch
            {
                // 削除処理をイベント不要のRemoveに変更する
                //SearchDetail.Rows[intDelRowIdx].Delete();
                SearchDetail.Rows.Remove(SearchDetail.Rows[intDelRowIdx]);
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
            // 入力チェック
            if (!isCheckInput())
                return;

            // パラメータ編集
            int companyCd;
            int.TryParse(myCompany.Text1, out companyCd);

            // パラメータ情報設定
            Dictionary<string, string> cond = setSearchParams();

            foreach (DataRow row in SearchDetail.Rows)
            {

                if (row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added || row.RowState == DataRowState.Deleted)
                {
                    this.ErrorMessage = "変更データが存在します。登録してから印刷してください。";
                    MessageBox.Show("変更データが存在します。登録してから印刷してください。");
                    return;
                }
            }

            OutputReport(SearchDetail);

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
            if (MaintenanceMode == null)
                return;

            sp棚卸明細データ.CommitCellEdit();

            // 入力チェック
            if (!isCheckInput())
                return;

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
        /// F10　リボン　入力取り消し
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
            if (!string.IsNullOrEmpty(MaintenanceMode))
            {

                MessageBoxResult result = MessageBox.Show("保存せずに終了してもよろしいですか？", "確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
            }

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
            // 入力チェック
            if (!isCheckInput())
                return;

            // パラメータ辞書の作成を行う
            Dictionary<string, string> dicCond = setStocktakingParm();


            base.SendRequest(new CommunicationObject(MessageType.RequestData, CHECK_STOCKTAKING, new object[] { int.Parse(myCompany.Text1), StocktakingDate.Text, dicCond }));
            base.SetBusyForInput();
        }

        #endregion

        #region 行追加ボタン
        /// <summary>
        /// 行追加処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            if (this.MaintenanceMode == null) return;

            sp新規明細データ.CommitCellEdit();

            #region 値の取得
            int? 新規_倉庫コード = (int?)sp新規明細データ.Cells[0, "倉庫コード"].Value;
            string 新規_倉庫名 = (string)sp新規明細データ.Cells[0, "倉庫名"].Value;
            int? 新規_品番コード = (int?)sp新規明細データ.Cells[0, "品番コード"].Value;
            string 新規_自社品番 = (string)sp新規明細データ.Cells[0, "自社品番"].Value;
            string 新規_自社品名 = (string)sp新規明細データ.Cells[0, "自社品名"].Value;
            string 新規_賞味期限 = (string)sp新規明細データ.Cells[0, "賞味期限"].Value;
            decimal? 新規_数量 = (decimal?)sp新規明細データ.Cells[0, "数量"].Value;
            string 新規_単位 = (string)sp新規明細データ.Cells[0, "単位"].Value;
            decimal? 新規_実在庫数量 = (decimal?)sp新規明細データ.Cells[0, "実在庫数"].Value;
            #endregion

            #region 入力内容チェック
            if (新規_倉庫コード == null || 新規_倉庫コード == 0)
            {
                ShowErrNewDataRow("倉庫コード", "倉庫が未入力または不正です");
                return;
            }

            if (string.IsNullOrEmpty((string)sp新規明細データ.Cells[0, "自社品番"].Value))
            {
                ShowErrNewDataRow("自社品番", "自社品番が未入力です");
                return;
            }

            if (新規_実在庫数量.Equals(Decimal.Zero) || 新規_実在庫数量 == null)
            {
                ShowErrNewDataRow("実在庫数量", "実在庫数量が未入力です");
                return;
            }

            #endregion

            DateTime? dt新規_賞味期限 = string.IsNullOrEmpty(新規_賞味期限) ? AppCommon.DateTimeToDate(null, DateTime.MaxValue) : DateTime.Parse(新規_賞味期限);



            // 同一キーの在庫がないか画面側でチェック
            int count = SearchDetail.Select("", "", DataViewRowState.CurrentRows).AsEnumerable()
                                    .Where(a => a.Field<int>("品番コード") == 新規_品番コード && a.Field<DateTime?>("賞味期限") == dt新規_賞味期限)
                                    .Count();

            // 存在しない場合は新規追加
            if (count > 0)
            {
                this.ErrorMessage = "入力済みです。追加できません。";
                return;
            }

            // 重複チェック
            base.SendRequest(new CommunicationObject(MessageType.RequestData, CHECK_ADDROWDATA, new object[]{ 
                                                                                                                    StocktakingDate.Text,
                                                                                                                    新規_倉庫コード, 
                                                                                                                    新規_品番コード, 
                                                                                                                    新規_賞味期限
                                                                                                            }));

        }

        #endregion

        #region 新規明細エラー表示
        /// <summary>
        /// 新規明細エラー表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowErrNewDataRow(string colName, string msg)
        {
            // メッセージ表示
            MessageBox.Show(msg);
            // 一覧にフォーカス移動
            sp新規明細データ.Focus();
            // セルのフォーカス移動
            sp新規明細データ.ActiveCellPosition = new CellPosition(0, colName);
        }
        #endregion

        #region << 機能処理関連 >>

        #region 画面初期化
        /// <summary>
        /// 画面初期化
        /// </summary>
        private void ScreenClear()
        {
            // 在庫明細データ クリア
            sp新規明細データ.ItemsSource = null;
            AddRowDetailList = null;

            // 新規明細データ クリア
            sp棚卸明細データ.ItemsSource = null;
            SearchDetail = null;

            this.MaintenanceMode = string.Empty;

            ChangeKeyItemChangeable(true);

            btnSearch.IsEnabled = true;

            // ヘッダー項目初期化
            this.myCompany.Text1 = ccfg.自社コード.ToString();
            this.Warehouse.Text1 = string.Empty;
            this.StocktakingDate.Text = string.Empty;
            this.cmbItemType.SelectedValue = -1;
            this.Product.Text1 = string.Empty;
            this.ProductName.Text = string.Empty;
            this.Brand.Text1 = string.Empty;
            this.Series.Text1 = string.Empty;

            ResetAllValidation();
            SetFocusToTopControl();

        }
        #endregion

        #region 画面表示設定
        /// <summary>
        /// 検索条件部の初期設定をおこなう
        /// </summary>
        private void initSearchControl()
        {
            this.myCompany.Text1 = ccfg.自社コード.ToString();
            this.myCompany.IsEnabled = ccfg.自社販社区分 == 自社販社区分.自社.GetHashCode();

        }
        #endregion

        #region 新規追加行一覧初期化
        /// <summary>
        /// 新規追加行一覧初期化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitNewDataRow()
        {
            // 新規行セット
            this.AddRowDetailList = new List<S10_STOCKTAKING_Member>();
            var tbl = new DataTable();
            AppCommon.ConvertToDataTable(this.AddRowDetailList, tbl);
            DataRow newRow = tbl.NewRow();
            tbl.Rows.Add(newRow);
            sp新規明細データ.ItemsSource = tbl.DefaultView;
            //gcAddRowGrid.Rows[0].Background = new SolidColorBrush(Colors.SkyBlue);

            AddRowDetailList = (List<S10_STOCKTAKING_Member>)AppCommon.ConvertFromDataTable(typeof(List<S10_STOCKTAKING_Member>), tbl);

            // 追加ボタン活性
            btnAdd.IsEnabled = true;

            sp新規明細データ.Focus();

        }
        #endregion

        #region 編集開始
        /// <summary>
        /// 編集開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartSpreadEdit()
        {
            // 条件をロック
            ChangeKeyItemChangeable(false);

            btnSearch.IsEnabled = false;

            // フォーカスをSPREADへ
            sp棚卸明細データ.Focus();
            // 実棚数量１にフォーカス
            sp棚卸明細データ.ActiveCellPosition = new CellPosition(0, "実在庫数");
        }
        #endregion

        #region 行背景色変更
        /// <summary>
        /// 行背景色変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeSpreadBackColor()
        {

            int 品番追加FLG = this.sp棚卸明細データ.Columns.First(x => x.Name == "品番追加FLG").Index;

            foreach (var row in sp棚卸明細データ.Rows.Where(x => x.Cells[品番追加FLG].Value.Equals(1)))
            {
                row.Background = new SolidColorBrush(Colors.SkyBlue);
            }
        }
        #endregion

        #endregion

        #region 業務入力チェック
        /// <summary>
        /// 業務入力チェックをおこなう
        /// </summary>
        /// <returns></returns>
        private bool isCheckInput()
        {
            bool boolResult = false;

            // 入力検証
            if (!base.CheckAllValidation())
            {
                this.ErrorMessage = "入力内容に誤りがあります。";
                MessageBox.Show("入力内容に誤りがあります。");
                return boolResult;
            }

            // 自社コードの入力値検証
            int companyCd;
            if (string.IsNullOrEmpty(myCompany.Text1))
            {
                base.ErrorMessage = "自社コードは必須入力項目です。";
                return boolResult;
            }
            else if (!int.TryParse(myCompany.Text1, out companyCd))
            {
                base.ErrorMessage = "自社コードの入力値に誤りがあります。";
                return boolResult;
            }

            // 棚卸日
            DateTime dtClosingDate;
            if (!DateTime.TryParse(StocktakingDate.Text, out dtClosingDate))
            {
                ErrorMessage = "棚卸日の内容が正しくありません。";
                MessageBox.Show("入力エラーがあります。");
                return boolResult;
            }

            boolResult = true;
            return boolResult;

        }
        #endregion

        #region パラメータ設定
        /// <summary>
        /// パラメータを設定する
        /// </summary>
        private Dictionary<string, string> setSearchParams()
        {
            // パラメータ生成
            Dictionary<string, string> cond = new Dictionary<string, string>();
            cond.Add("倉庫", Warehouse.Text1);
            cond.Add("自社品番", Product.Text1);
            cond.Add("自社品名", ProductName.Text);
            cond.Add("商品分類", cmbItemType.SelectedValue.ToString());
            cond.Add("ブランド", Brand.Text1);
            cond.Add("シリーズ", Series.Text1);

            return cond;

        }
        #endregion

        #region 帳票パラメータ取得
        /// <summary>
        /// 帳票パラメータを取得する
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, DateTime> getPrintParameter()
        {
            // TODO:ロジック整理後に記述
            // 期間を算出
            //int year = int.Parse(paramDic["処理年度"].Replace("/", "")),
            //    pMonth = DEFAULT_SETTLEMENT_MONTH,
            //    pYear = pMonth < 4 ? year + 1 : year,
            //    mCounter = 1;

            //DateTime lastMonth = new DateTime(pYear, pMonth, 1);
            //DateTime targetMonth = lastMonth.AddMonths(-11);

            Dictionary<string, DateTime> printDic = new Dictionary<string, DateTime>();
            //printDic.Add(REPORT_PARAM_NAME_PRIOD_START, targetMonth);
            //printDic.Add(REPORT_PARAM_NAME_PRIOD_END, lastMonth);
            //printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH01, targetMonth);
            //printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH02, targetMonth.AddMonths(mCounter++));
            //printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH03, targetMonth.AddMonths(mCounter++));
            //printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH04, targetMonth.AddMonths(mCounter++));
            //printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH05, targetMonth.AddMonths(mCounter++));
            //printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH06, targetMonth.AddMonths(mCounter++));
            //printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH07, targetMonth.AddMonths(mCounter++));
            //printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH08, targetMonth.AddMonths(mCounter++));
            //printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH09, targetMonth.AddMonths(mCounter++));
            //printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH10, targetMonth.AddMonths(mCounter++));
            //printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH11, targetMonth.AddMonths(mCounter++));
            //printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH12, lastMonth);

            return printDic;

        }
        #endregion

        #region ＣＳＶデータ出力
        /// <summary>
        /// ＣＳＶデータの出力をおこなう
        /// </summary>
        /// <param name="tbl"></param>
        private void OutputCsv(DataTable tbl)
        {
            if (tbl == null || tbl.Rows.Count == 0)
            {
                MessageBox.Show("出力対象のデータがありません。");
                return;
            }

            // CSV出力用に列名を編集する
            changeColumnsName(tbl);

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
                CSVData.SaveCSV(tbl, sfd.FileName, true, true, false, ',');
                MessageBox.Show("CSVファイルの出力が完了しました。");
            }

        }
        #endregion

        #region 帳票出力
        /// <summary>
        /// 帳票の印刷処理をおこなう
        /// </summary>
        /// <param name="tbl"></param>
        private void OutputReport(DataTable tbl)
        {
            PrinterDriver ret = AppCommon.GetPrinter(frmcfg.PrinterName);
            if (ret.Result == false)
            {
                this.ErrorMessage = "プリンタドライバーがインストールされていません！";
                return;
            }
            frmcfg.PrinterName = ret.PrinterName;

            if (tbl == null || tbl.Rows.Count == 0)
            {
                this.ErrorMessage = "印刷データがありません。";
                return;
            }

            try
            {
                base.SetBusyForInput();

                int selItemType = int.Parse(cmbItemType.SelectedValue.ToString());
                Dictionary<int, string> itemTypeDic = new Dictionary<int, string>();
                itemTypeDic.Add(0, "指定なし");
                itemTypeDic.Add(1, "食品");
                itemTypeDic.Add(2, "繊維");
                itemTypeDic.Add(3, "その他");

                Dictionary<string, DateTime> printParams = getPrintParameter();

                var parms = new List<FwRepPreview.ReportParameter>()
                {
                    #region 印字パラメータ設定
                    new FwRepPreview.ReportParameter() { PNAME = "自社コード", VALUE = myCompany.Text2 },
                    new FwRepPreview.ReportParameter() { PNAME = "商品コード", VALUE = string.IsNullOrEmpty(Product.Text2) ? "" : Product.Text2  },
                    new FwRepPreview.ReportParameter() { PNAME = "商品分類", VALUE = itemTypeDic[selItemType] },
                    new FwRepPreview.ReportParameter() { PNAME = "商品名指定", VALUE = ProductName.Text },
                    new FwRepPreview.ReportParameter() { PNAME = "ブランド", VALUE = string.IsNullOrEmpty(Brand.Text2) ? "" : Brand.Text2 },
                    new FwRepPreview.ReportParameter() { PNAME = "シリーズ", VALUE = string.IsNullOrEmpty(Series.Text2) ? "" : Series.Text2 },
                    #endregion
                };

                DataTable 印刷データ = tbl.Copy();
                印刷データ.TableName = "商品在庫残高一覧表";

                FwRepPreview.ReportPreview view = new FwRepPreview.ReportPreview();
                view.MakeReport(印刷データ.TableName, ReportFileName, 0, 0, 0);
                view.SetReportData(印刷データ);

                view.SetupParmeters(parms);

                base.SetFreeForInput();

                view.PrinterName = frmcfg.PrinterName;
                view.IsCustomMode = true;
                setPrinterInfoA3(view);
                view.ShowPreview();
                view.Close();
                frmcfg.PrinterName = view.PrinterName;

            }
            catch (Exception ex)
            {
                base.SetFreeForInput();
                this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
                appLog.Error("商品在庫残高一覧表の印刷時に例外が発生しました。", ex);
            }

        }
        #endregion

        #region A3用紙設定
        /// <summary>
        /// A3用紙設定をプリンタに設定する
        /// </summary>
        /// <param name="view"></param>
        private void setPrinterInfoA3(FwRepPreview.ReportPreview view)
        {
            view.PrinterInfo = new FwRepPreview.FwPrinterInfo();
            view.PrinterInfo.paperSizeName = "A3";
            view.PrinterInfo.landscape = true;
            view.PrinterInfo.margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);

        }
        #endregion

        #region 列名編集
        /// <summary>
        /// テーブル列名をCSV出力用に変更して返す
        /// </summary>
        /// <param name="tbl"></param>
        /// <returns></returns>
        private void changeColumnsName(DataTable tbl)
        {
            // TODO:ロジック整理後に記述する
            //Dictionary<string, DateTime> printParams = getPrintParameter();

            //foreach (DataColumn col in tbl.Columns)
            //{
            //    switch (col.ColumnName)
            //    {
            //        case "集計売上額０１":
            //            col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH01].ToString("yyyy年M月");
            //            break;

            //        case "集計売上額０２":
            //            col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH02].ToString("yyyy年M月");
            //            break;

            //        case "集計売上額０３":
            //            col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH03].ToString("yyyy年M月");
            //            break;

            //        case "集計売上額０４":
            //            col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH04].ToString("yyyy年M月");
            //            break;

            //        case "集計売上額０５":
            //            col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH05].ToString("yyyy年M月");
            //            break;

            //        case "集計売上額０６":
            //            col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH06].ToString("yyyy年M月");
            //            break;

            //        case "集計売上額０７":
            //            col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH07].ToString("yyyy年M月");
            //            break;

            //        case "集計売上額０８":
            //            col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH08].ToString("yyyy年M月");
            //            break;

            //        case "集計売上額０９":
            //            col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH09].ToString("yyyy年M月");
            //            break;

            //        case "集計売上額１０":
            //            col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH10].ToString("yyyy年M月");
            //            break;

            //        case "集計売上額１１":
            //            col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH11].ToString("yyyy年M月");
            //            break;

            //        case "集計売上額１２":
            //            col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH12].ToString("yyyy年M月");
            //            break;

            //    }

            //}

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
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);

        }
        #endregion

        #region Spreadイベント処理

        #region 新規行編集終了時処理

        private void gcAddRowGrid_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
        {
            GcSpreadGrid grid = sender as GcSpreadGrid;
            string columName = grid.ActiveCellPosition.ColumnName;

            if (e.EditAction == SpreadEditAction.Cancel)
                return;

            //明細行が存在しない場合は処理しない
            if (SearchDetail == null) return;
            //if (SearchDetail.Select("", "", DataViewRowState.CurrentRows).Count() == 0) return;

            var value = grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Value;

            switch (columName)
            {
                case "倉庫コード":


                    string soukCd = value == null ? "" : value.ToString();

                    base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_SOUK, AppCommon.IntParse(soukCd)));
                    SearchDetail.Rows[grid.ActiveRowIndex].EndEdit();
                    break;

                case "品番コード":


                    string hinCd = value == null ? "" : value.ToString();

                    base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_HIN, new object[] { hinCd }));
                    SearchDetail.Rows[grid.ActiveRowIndex].EndEdit();
                    break;

                case "自社品番":

                    if (value == null) return;
                    string jisHin = value == null ? "" : value.ToString();

                    base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_UcCustomerProduct, jisHin,
                                null,
                                null));
                    SearchDetail.Rows[grid.ActiveRowIndex].EndEdit();
                    break;
                case "実在庫数":
                    //decimal? num = value == null ? 0.00m : AppCommon.DecimalParse(value.ToString());
                    //grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Value = num;

                    //SearchDetail.Rows[grid.ActiveRowIndex].EndEdit();
                    break;
                default:
                    if (grid.ActiveRowIndex >= 0)
                    {
                        // EndEditが行われずに登録すると変更内容が反映されないため処理追加
                        SearchDetail.Rows[grid.ActiveRowIndex].EndEdit();
                    }
                    break;
            }

        }

        #endregion

        #region 新規入力欄ロストフォーカス
        private void gcAddRowGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sp新規明細データ.Focusable == true)
            {
                sp新規明細データ.SelectionBorder.Style = BorderLineStyle.Thick;
                sp新規明細データ.SelectionBorderUnfocused.Style = BorderLineStyle.None;
            }
            else
            {
                sp新規明細データ.SelectionBorder.Style = BorderLineStyle.None;
                sp新規明細データ.SelectionBorderUnfocused.Style = BorderLineStyle.None;
            }
        }
        #endregion

        #region 明細数量入力欄ロストフォーカス
        private void gcStokGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sp棚卸明細データ.Focusable == true)
            {
                sp棚卸明細データ.SelectionBorder.Style = BorderLineStyle.Thick;
                sp棚卸明細データ.SelectionBorderUnfocused.Style = BorderLineStyle.None;
            }
            else
            {
                sp棚卸明細データ.SelectionBorder.Style = BorderLineStyle.None;
                sp棚卸明細データ.SelectionBorderUnfocused.Style = BorderLineStyle.None;
            }
        }
        #endregion

        #region 新規入力欄キーダウンイベント
        private void gcAddRowGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //Delete押下時の処理
            //注意 : SPREAD上でDELETEキーを押下すると例外エラーが発生します(Delete禁止)
            if (e.Key == Key.Delete)
            {
                e.Handled = true;
            }

            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                sp新規明細データ.CommitCellEdit();

                // 更新在庫数量まで入力したら
                if (sp新規明細データ.ActiveCellPosition.ColumnName == "実在庫数")
                {
                    // 追加ボタンをフォーカス
                    this.btnAdd.Focus();

                    e.Handled = true;

                }
            }
        }
        #endregion



        #endregion

        #region 検索用パラメータ設定

        /// <summary>
        /// パラメータ辞書の作成を行う
        /// </summary>
        private Dictionary<string, string> setStocktakingParm()
        {
            // パラメータ生成
            Dictionary<string, string> dicCond = new Dictionary<string, string>();
            dicCond.Add("倉庫コード", Warehouse.Text1);
            dicCond.Add("自社品番", Product.Text1);
            dicCond.Add("自社品名", ProductName.Text);
            dicCond.Add("商品分類コード", cmbItemType.SelectedValue.ToString());
            dicCond.Add("ブランドコード", Brand.Text1);
            dicCond.Add("シリーズコード", Series.Text1);

            return dicCond;

        }

        #endregion

        #region セル内容取得・設定
        /// <summary>
        /// 指定セルの値を取得する
        /// </summary>
        /// <param name="rIdx">行番号</param>
        /// <param name="column">列定義</param>
        /// <returns></returns>
        private object getSpreadAddGridValue(int rIdx, GridColumnsMapping column)
        {

            if (sp新規明細データ.Cells[rIdx, (int)column].Value == null)
                return string.Empty;

            return sp新規明細データ.Cells[rIdx, (int)column].Value;

        }

        /// <summary>
        /// 指定セルの値を設定する
        /// </summary>
        /// <param name="rIdx">行番号</param>
        /// <param name="column">列定義</param>
        /// <param name="value">設定値</param>
        private void setSpreadGridValue(int rIdx, GridColumnsMapping column, object value)
        {
            if (sp棚卸明細データ.RowCount - 1 < rIdx || rIdx < 0)
                return;

            sp棚卸明細データ.Cells[rIdx, (int)column].Value = value;

        }
        #endregion

        #region 棚卸情報登録

        /// <summary>
        /// 棚卸情報の登録処理をおこなう
        /// </summary>
        private void Update()
        {

            // パラメータ辞書の作成を行う
            Dictionary<string, string> dicCond = setStocktakingParm();

            // 削除または追加したものをデータテーブルに反映
            var dt = SearchDetail.Copy();
            dt.AcceptChanges();

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    UPDATE_S10_STOCKTAKING,
                    new object[] {
                        ds,
                        int.Parse(myCompany.Text1),
                        StocktakingDate.Text,
                        dicCond ,
                        ccfg.ユーザID
                    }));

            base.SetBusyForInput();
        }

        #endregion
    }

}
