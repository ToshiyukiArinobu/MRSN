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
    using System.Text;

    /// <summary>
    /// 場所別棚卸入力 フォームクラス
    /// </summary>
    public partial class ZIK02010 : RibbonWindowViewBase
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
            private int? _商品分類;

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
            public int? 商品分類 { get { return _商品分類; } set { _商品分類 = value; NotifyPropertyChanged(); } }



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
            public byte[] spConfigZIK02010 = null;
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
            商品分類 = 10,
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
        /// 商品分類 内包データ
        /// </summary>
        private enum 商品分類 : int
        {
            食品 = 1,
            繊維 = 2,
            その他 = 3
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
        private const string UPDATE_S10_STOCKTAKING = "ZIK02010_UpdateStocktaking";


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

        /// <summary>会社コード</summary>
        private string _会社コード;
        public string 会社コード
        {
            get { return _会社コード; }
            set
            {
                _会社コード = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>棚卸在庫情報</summary>
        private DataView _DetailDataView;
        public DataView DetailDataView
        {
            get { return _DetailDataView; }
            set
            {
                _DetailDataView = value;
                //NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 新規/編集Flg
        /// 0:新規、1:編集
        /// </summary>
        private int modeFlg;

        #endregion

        #region クラス変数定義

        // 追加行グリッドコントローラー
        GcSpreadGridController addGridCtl;

        // 棚卸在庫グリッドコントローラー
        GcSpreadGridController stokGridCtl;

        StringBuilder JokenFilter;

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
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigZIK02010)ucfg.GetConfigValue(typeof(ConfigZIK02010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigZIK02010();
                ucfg.SetConfigValue(frmcfg);
                frmcfg.spConfigZIK02010 = this.spConfig;
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

            if (frmcfg.spConfigZIK02010 != null)
            {
                //AppCommon.LoadSpConfig(this.sp請求データ一覧, frmcfg.spConfig);
            }

            #endregion

            // 検索画面情報を設定
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });
            base.MasterMaintenanceWindowList.Add("M09_MYHIN", new List<Type> { typeof(MST02010), typeof(SCHM09_MYHIN) });
            base.MasterMaintenanceWindowList.Add("M14_BRAND", new List<Type> { typeof(MST04020), typeof(SCHM14_BRAND) });
            base.MasterMaintenanceWindowList.Add("M15_SERIES", new List<Type> { typeof(MST04021), typeof(SCHM15_SERIES) });

            // コンボデータ取得
            AppCommon.SetutpComboboxList(this.cmbItemType, false);
            addGridCtl = new GcSpreadGridController(sp新規明細データ);
            stokGridCtl = new GcSpreadGridController(sp棚卸明細データ);

            //Enterボタン処理イベント
            sp新規明細データ.InputBindings.Add(new KeyBinding(sp新規明細データ.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));
            sp棚卸明細データ.InputBindings.Add(new KeyBinding(sp棚卸明細データ.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));

            // 初期フォーカスを設定
            this.myCompany.SetFocus();
            ResetAllValidation();

            // 初回棚卸テーブル削除処理
            base.SendRequest(new CommunicationObject(MessageType.RequestData, INITIAL_DEL_PROCESS));

            // 画面初期化
            ScreenClear(true);

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
                        modeFlg = (int)AddEditFlg.新規;

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
                                    modeFlg = (int)AddEditFlg.編集;
                                    StocktakingDate.Text = dt.ToShortDateString();
                                }
                            }
                            else
                            {
                                // 編集
                                modeFlg = (int)AddEditFlg.編集;
                                StocktakingDate.Text = dt.ToShortDateString();
                            }
                        }

                        base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_STOCKTAKING, new object[] { int.Parse(myCompany.Text1), StocktakingDate.Text, dicCond, modeFlg }));
                        base.SetBusyForInput();
                        break;

                    // ===========================
                    // 棚卸在庫テーブル情報取得
                    // ===========================
                    case GET_STOCKTAKING:

                        base.SetFreeForInput();

                        //データが存在しない場合
                        if (tbl == null && tbl.Rows.Count == 0)
                        {
                            MessageBox.Show("在庫情報がありません。");
                            break;
                        }

                        SetTblData(tbl);

                        if (DetailDataView.Count > 0)
                        {
                            // 新規/編集ラベルをセット
                            this.MaintenanceMode = modeFlg == (int)AddEditFlg.新規 ? AppConst.MAINTENANCEMODE_ADD : AppConst.MAINTENANCEMODE_EDIT;

                            // 会社コード制御
                            myCompany.IsEnabled = false;

                            // 品番追加フラグの行は背景色を変更
                            ChangeSpreadBackColor();

                            // 新規明細データ一覧初期化
                            InitNewDataRow();

                            StartSpreadEdit();
                        }
                        else
                        {
                            MessageBox.Show("在庫情報がありません。");

                            // 棚卸明細データ クリア
                            sp棚卸明細データ.ItemsSource = null;
                            SearchDetail = null;
                        }

                        break;

                    case CHECK_ADDROWDATA:

                        base.SetFreeForInput();

                        string errMsg = data.ToString();

                        if (string.IsNullOrEmpty(errMsg))
                        {
                            // 新規行追加
                            var nRow = DetailDataView.AddNew();
                            nRow["棚卸日"] = DateTime.Parse(StocktakingDate.Text);
                            nRow["倉庫コード"] = (int?)sp新規明細データ.Cells[0, "倉庫コード"].Value;
                            nRow["倉庫名"] = (string)sp新規明細データ.Cells[0, "倉庫名"].Value;
                            nRow["品番コード"] = (int?)sp新規明細データ.Cells[0, "品番コード"].Value;
                            nRow["自社品番"] = (string)sp新規明細データ.Cells[0, "自社品番"].Value;
                            nRow["自社品名"] = (string)sp新規明細データ.Cells[0, "自社品名"].Value;

                            DateTime dt賞味期限;
                            string s賞味期限 = (string)sp新規明細データ.Cells[0, "賞味期限"].Value;
                            nRow["賞味期限"] = !DateTime.TryParse(s賞味期限, out dt賞味期限) ? AppCommon.DateTimeToDate(null, DateTime.MaxValue) : dt賞味期限;
                            nRow["表示用賞味期限"] = s賞味期限;

                            nRow["数量"] = 0;
                            nRow["単位"] = (string)sp新規明細データ.Cells[0, "単位"].Value;
                            nRow["実在庫数"] = (decimal?)sp新規明細データ.Cells[0, "実在庫数"].Value;
                            nRow["品番追加FLG"] = 1;
                            nRow["行追加FLG"] = true;

                            nRow.EndEdit();

                            ChangeSpreadBackColor();

                            // 新規明細データ一覧初期化
                            InitNewDataRow();

                            // 行追加後は追加行を選択させる                            
                            int newRowIdx = DetailDataView.Count - 1;

                            if (newRowIdx >= 0)
                            {
                                // TODO:追加行が表示されるようにしたかったが追加行の上行までしか移動できない...
                                stokGridCtl.ScrollShowCell(newRowIdx, (int)GridColumnsMapping.実在数量);
                                stokGridCtl.SetCellFocus(newRowIdx, (int)GridColumnsMapping.実在数量);
                            }
                        }
                        else
                        {
                            MessageBox.Show(errMsg,
                                                    "追加不可",
                                                     MessageBoxButton.OK, MessageBoxImage.Information);

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
                            addGridCtl.SetCellValue((int)GridColumnsMapping.倉庫名, drow["略称名"]);
                        }
                        #endregion
                        break;

                    case GET_HIN:
                    case GET_UcCustomerProduct:
                        #region 自社品番・得意先品番 手入力時
                        DataTable ctbl = data as DataTable;

                        if (ctbl == null || ctbl.Rows.Count == 0)
                        {
                            // 対象データなしの場合

                            addGridCtl.SetCellValue((int)GridColumnsMapping.品番コード, null);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.自社品番, string.Empty);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.自社品名, string.Empty);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.数量, 0m);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.単位, string.Empty);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.自社色, string.Empty);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.商品分類, (int)商品分類.その他);

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
                                addGridCtl.SetCellValue((int)GridColumnsMapping.商品分類, myhin.SelectedRowData["商品分類"]);
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
                            addGridCtl.SetCellValue((int)GridColumnsMapping.商品分類, drow["商品分類"]);

                        }

                        #endregion
                        break;

                    case UPDATE_S10_STOCKTAKING:
                        MessageBox.Show(AppConst.SUCCESS_UPDATE, "登録完了", MessageBoxButton.OK, MessageBoxImage.Information);

                        ScreenClear();

                        // 再表示
                        btnSearch_Click(null, null);
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
                var ucText = ViewBaseCommon.FindVisualParent<UcLabelTwinTextBox>(elmnt as Control);

                if (spgrid != null)
                {
                    // スプレッドのF1処理

                    #region グリッドファンクションイベント
                    if (addGridCtl.ActiveColumnIndex == (int)GridColumnsMapping.倉庫コード)
                    {
                        //入力途中のセルを空欄状態に戻す
                        sp新規明細データ.CommitCellEdit();

                        SCHM22_SOUK souk = new SCHM22_SOUK();
                        souk.TwinTextBox = new UcLabelTwinTextBox();

                        var soukVal = addGridCtl.GetCellValue(0, (int)GridColumnsMapping.倉庫コード);
                        souk.確定コード = soukVal == null ? string.Empty : soukVal.ToString();
                        souk.TwinTextBox.LinkItem = 会社コード;
                        if (souk.ShowDialog(this) == true)
                        {
                            sp新規明細データ.CommitCellEdit();
                            addGridCtl.SetCellValue((int)GridColumnsMapping.倉庫コード, souk.TwinTextBox.Text1);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.倉庫名, souk.TwinTextBox.Text3);
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
                            addGridCtl.SetCellValue((int)GridColumnsMapping.商品分類, myhin.SelectedRowData["商品分類"].ToString());
                            //addGridCtl.SetCellValue((int)GridColumnsMapping.数量, 1m);
                            addGridCtl.SetCellValue((int)GridColumnsMapping.単位, myhin.SelectedRowData["単位"]);

                        }

                    }

                    #endregion

                }
                else
                {
                    // TwinTextboxのF1処理

                    if (ucText != null)
                    {
                        switch (ucText.DataAccessName)
                        {
                            case "M22_SOUK_BASYOC":

                                SCHM22_SOUK souk = new SCHM22_SOUK();
                                souk.TwinTextBox = Warehouse;

                                souk.確定コード = souk.TwinTextBox.Text1;

                                if (souk.ShowDialog(this) == true)
                                {
                                    Warehouse.Text1 = souk.TwinTextBox.Text1;  // 倉庫コード
                                    Warehouse.Text2 = souk.TwinTextBox.Text3;  // 倉庫略称名
                                }
                                break;
                            default:
                                ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);
                                break;
                        }
                    }
                    else
                    {
                        ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);
                    }

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
            if (MaintenanceMode == null)
                return;

            // 入力チェック
            if (!isCheckInput())
                return;

            // パラメータ編集
            int companyCd;
            int.TryParse(myCompany.Text1, out companyCd);

            sp棚卸明細データ.CommitCellEdit();

            // 変更チェック
            string erMessage = "変更データが存在します。登録してからCSV出力してください。";

            foreach (DataRow row in SearchDetail.Rows)
            {

                if (row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added || row.RowState == DataRowState.Deleted)
                {
                    this.ErrorMessage = erMessage;
                    MessageBox.Show(erMessage, "出力不可", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
            }

            OutputCsv(DetailDataView.ToTable());


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
                sp棚卸明細データ.Rows.Remove(intDelRowIdx);
            }
            catch (Exception ex)
            {
                appLog.Error("行削除エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";
                return;
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
            if (MaintenanceMode == null)
                return;


            // 入力チェック
            if (!isCheckInput())
                return;

            // パラメータ編集
            int companyCd;
            int.TryParse(myCompany.Text1, out companyCd);

            // 変更チェック
            string erMessage = "変更データが存在します。登録してから印刷してください。";

            foreach (DataRow row in SearchDetail.Rows)
            {

                if (row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added || row.RowState == DataRowState.Deleted)
                {
                    this.ErrorMessage = erMessage;
                    MessageBox.Show(erMessage, "出力不可", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
            }

            OutputReport(DetailDataView.ToTable());

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

            string sMessage = AppConst.CONFIRM_UPDATE;

            // 新規行に値が設定されている場合、処理続行確認メッセージを表示
            if (AddRowDetailList[0].倉庫コード != null || AddRowDetailList[0].品番コード != null || !string.IsNullOrEmpty(AddRowDetailList[0].自社品番) ||
                AddRowDetailList[0].賞味期限 != null || (AddRowDetailList[0].実在庫数 != null && AddRowDetailList[0].実在庫数 != 0))
            {
                sMessage = "行追加がされていませんがよろしいですか？";
            }

            if (MessageBox.Show(sMessage,
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
            string sMessage = AppConst.CONFIRM_CANCEL;

            if (SearchDetail != null)
            {
                foreach (DataRow row in SearchDetail.Rows)
                {

                    if (row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added || row.RowState == DataRowState.Deleted)
                    {
                        sMessage = "保存せずに取り消してもよろしいですか？";
                        break;
                    }
                }
            }

            var yesno = MessageBox.Show(sMessage, "取消確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (yesno == MessageBoxResult.No)
                return;

            ScreenClear(true);

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
            string sMessage = AppConst.CONFIRM_CLOSE;

            if (SearchDetail != null)
            {
                foreach (DataRow row in SearchDetail.Rows)
                {

                    if (row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added || row.RowState == DataRowState.Deleted)
                    {
                        sMessage = "保存せずに終了してもよろしいですか？";
                        break;
                    }
                }
            }

            var yesno = MessageBox.Show(sMessage, "取消確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (yesno == MessageBoxResult.No)
                return;

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
            int? 新規_商品分類 = (int?)sp新規明細データ.Cells[0, "商品分類"].Value;
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
                ShowErrNewDataRow("実在庫数", "実在庫数量が未入力です");
                return;
            }

            DateTime dt新規_賞味期限;

            if (!DateTime.TryParse(新規_賞味期限, out dt新規_賞味期限))
            {
                // 変換に失敗かつ商品分類が「食品」の場合はエラー
                if (新規_商品分類 == null || 新規_商品分類.Equals(商品分類.食品.GetHashCode()))
                {
                    ShowErrNewDataRow("賞味期限", "商品分類が『食品』の為、賞味期限の設定が必要です。");
                    return;
                }

                dt新規_賞味期限 = AppCommon.DateTimeToDate(null, DateTime.MaxValue);
            }


            // 同一キーの在庫がないか画面側でチェック
            int count = SearchDetail.Select("", "", DataViewRowState.CurrentRows).AsEnumerable()
                                    .Where(a => a.Field<int>("倉庫コード") == 新規_倉庫コード && a.Field<int>("品番コード") == 新規_品番コード && a.Field<DateTime?>("賞味期限") == dt新規_賞味期限)
                                    .Count();

            // 明細に存在する場合はエラー
            if (count > 0)
            {
                MessageBox.Show("入力済みです。追加できません。", "追加不可", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            #endregion

            int? 指定倉庫 = string.IsNullOrEmpty(Warehouse.Text1) ? (int?)null : AppCommon.IntParse(Warehouse.Text1);

            // 重複チェック
            base.SendRequest(new CommunicationObject(MessageType.RequestData, CHECK_ADDROWDATA, new object[]{ 
                                                                                                                    StocktakingDate.Text,
                                                                                                                    新規_倉庫コード, 
                                                                                                                    新規_品番コード, 
                                                                                                                    新規_賞味期限,
                                                                                                                    AppCommon.IntParse(会社コード),
                                                                                                                    指定倉庫
                                                                                                            }));

        }

        #endregion

        #region チェックリスト入力処理
        private void writeListChk_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                cmbItemType.Focus();
            }
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
        private void ScreenClear(bool headerInit = false)
        {
            // 新規明細データ クリア
            sp新規明細データ.ItemsSource = null;
            AddRowDetailList = null;

            // 棚卸明細データ クリア
            sp棚卸明細データ.ItemsSource = null;
            DetailDataView = null;
            SearchDetail = null;

            this.MaintenanceMode = null;

            ChangeKeyItemChangeable(true);

            btnSearch.IsEnabled = true;

            if (headerInit)
            {
                // ヘッダー項目初期化
                this.myCompany.Text1 = ccfg.自社コード.ToString();
                this.myCompany.IsEnabled = ccfg.自社販社区分 == 自社販社区分.自社.GetHashCode();
                this.myCompany.Text2IsReadOnly = true;

                this.Warehouse.Text1 = string.Empty;
                this.StocktakingDate.Text = string.Empty;
                this.cmbItemType.SelectedValue = 0;
                this.Product.Text1 = string.Empty;
                this.ProductName.Text = string.Empty;
                this.Brand.Text1 = string.Empty;
                this.Series.Text1 = string.Empty;

            }

            ResetAllValidation();
            SetFocusToTopControl();

        }
        #endregion

        #region 取得データをセット
        /// <summary>
        /// 取得内容を各コントロールに設定
        /// </summary>
        /// <param name="ds"></param>
        private void SetTblData(DataTable tbl)
        {

            SearchDetail = tbl;
            SearchDetail.AcceptChanges();

            foreach (DataRow row in SearchDetail.Rows)
            {
                if ((bool)row["新規データFLG"] == true)
                {
                    row.SetAdded();
                }
            }

            #region フィルタ実施
            // ヘッダーの「会社コード・棚卸日・倉庫」以外でフィルタ項目絞込みを実施
            JokenFilter = new StringBuilder();

            // 追加行は常に表示
            JokenFilter.AppendFormat(" 行追加FLG = TRUE OR (  ", this.cmbItemType.SelectedIndex.ToString());

            #region 条件部分のフィルター設定

            // 棚卸日
            JokenFilter.AppendFormat("棚卸日 ='{0}'", this.StocktakingDate.Text);

            // 倉庫コード
            if (!string.IsNullOrEmpty(this.Warehouse.Text1))
            {
                JokenFilter.AppendFormat(" AND 倉庫コード ='{0}'", this.Warehouse.Text1);
            }

            // 商品分類
            if (this.cmbItemType.SelectedIndex != 0)
            {
                JokenFilter.AppendFormat(" AND 商品分類 ='{0}'", this.cmbItemType.SelectedIndex.ToString());
            }

            // 自社品番
            if (!string.IsNullOrEmpty(this.Product.Text1))
            {
                JokenFilter.AppendFormat(" AND 自社品番 = '{0}'", this.Product.Text1);
            }

            // 自社品名
            if (!string.IsNullOrEmpty(this.ProductName.Text))
            {
                JokenFilter.AppendFormat(" AND 自社品名 LIKE '%{0}%'", this.ProductName.Text);
            }

            // ブランド
            if (!string.IsNullOrEmpty(this.Brand.Text1))
            {
                JokenFilter.AppendFormat(" AND ブランド = '{0}'", this.Brand.Text1);
            }

            // シリーズ
            if (!string.IsNullOrEmpty(this.Series.Text1))
            {
                JokenFilter.AppendFormat(" AND シリーズ = '{0}'", this.Series.Text1);
            }

            JokenFilter.Append(")");

            #endregion

            // フィルター適用
            DetailDataView = tbl.AsDataView();
            DetailDataView.RowFilter = JokenFilter.ToString();
            sp棚卸明細データ.ItemsSource = DetailDataView;

            #endregion

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

            // KEY項目の入力検証
            if (!base.CheckKeyItemValidation())
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

            if (dtClosingDate < DateTime.Now.AddMonths(-1))
            {
                ErrorMessage = "1ヶ月前の日付で、棚卸在庫を登録することは出来ません。";
                MessageBox.Show("1ヶ月前の日付で、棚卸在庫を登録することは出来ません。");
                return boolResult;
            }

            boolResult = true;
            return boolResult;

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

            // CSV用データを取得
            DataTable CSVデータ = CreateStockTakingCsv(tbl);

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
                CSVData.SaveCSV(CSVデータ, sfd.FileName, true, true, false, ',');
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


                var parms = new List<FwRepPreview.ReportParameter>()
                {
                     #region 印字パラメータ設定
                    new FwRepPreview.ReportParameter() { PNAME = "会社コード", VALUE = myCompany.Text1 },
                    new FwRepPreview.ReportParameter() { PNAME = "会社名", VALUE = myCompany.Text2 },
                    new FwRepPreview.ReportParameter() { PNAME = "倉庫コード", VALUE = string.IsNullOrEmpty(Warehouse.Text1) ? "" : Warehouse.Text1 },
                    new FwRepPreview.ReportParameter() { PNAME = "倉庫名", VALUE =string.IsNullOrEmpty(Warehouse.Text2) ? "" : Warehouse.Text2 },
                    new FwRepPreview.ReportParameter() { PNAME = "棚卸日", VALUE = StocktakingDate.Text},
                    new FwRepPreview.ReportParameter() { PNAME = "商品分類", VALUE = selItemType<0?string.Empty:itemTypeDic[selItemType] },
                    new FwRepPreview.ReportParameter() { PNAME = "自社品番コード", VALUE = string.IsNullOrEmpty(Product.Text1) ? "" : Product.Text1  },
                    new FwRepPreview.ReportParameter() { PNAME = "自社品番_名称", VALUE = string.IsNullOrEmpty(Product.Text2) ? "" : Product.Text2  },
                    new FwRepPreview.ReportParameter() { PNAME = "自社品名", VALUE = ProductName.Text },
                    new FwRepPreview.ReportParameter() { PNAME = "ブランドコード", VALUE = string.IsNullOrEmpty(Brand.Text1) ? "" : Brand.Text1 },
                    new FwRepPreview.ReportParameter() { PNAME = "ブランド名称", VALUE = string.IsNullOrEmpty(Brand.Text2) ? "" : Brand.Text2 },
                    new FwRepPreview.ReportParameter() { PNAME = "シリーズコード", VALUE = string.IsNullOrEmpty(Series.Text1) ? "" : Series.Text1 },
                    new FwRepPreview.ReportParameter() { PNAME = "シリーズ名称", VALUE = string.IsNullOrEmpty(Series.Text2) ? "" : Series.Text2 },
                    new FwRepPreview.ReportParameter() { PNAME = "記入リスト印刷フラグ", VALUE = writeListChk.IsChecked }
                    #endregion
                };

                DataTable 印刷データ = tbl.Copy();
                印刷データ.TableName = "場所別棚卸一覧表";

                FwRepPreview.ReportPreview view = new FwRepPreview.ReportPreview();
                view.MakeReport(印刷データ.TableName, ReportFileName, 0, 0, 0);
                view.SetReportData(印刷データ);

                view.SetupParmeters(parms);

                base.SetFreeForInput();

                view.PrinterName = frmcfg.PrinterName;
                //view.IsCustomMode = true;

                view.ShowPreview();
                view.Close();
                frmcfg.PrinterName = view.PrinterName;

            }
            catch (Exception ex)
            {
                base.SetFreeForInput();
                this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
                appLog.Error("場所別棚卸一覧表の印刷時に例外が発生しました。", ex);
            }

        }
        #endregion

        #region CSV出力用データ作成
        /// <summary>
        /// CSV出力用データ作成
        /// </summary>
        /// <param name="tbl"></param>
        /// <returns></returns>
        private DataTable CreateStockTakingCsv(DataTable tbl)
        {
            DataTable CSVデータ = new DataTable();
            DateTime p棚卸日;

            // カラム名設定
            CSVデータ.Columns.Add("会社コード");
            CSVデータ.Columns.Add("会社名");
            CSVデータ.Columns.Add("倉庫コード");
            CSVデータ.Columns.Add("倉庫名");
            CSVデータ.Columns.Add("棚卸日");
            CSVデータ.Columns.Add("品番コード");
            CSVデータ.Columns.Add("自社品番");
            CSVデータ.Columns.Add("自社品名");
            CSVデータ.Columns.Add("自社色");
            CSVデータ.Columns.Add("賞味期限");
            CSVデータ.Columns.Add("数量");
            CSVデータ.Columns.Add("単位");
            CSVデータ.Columns.Add("更新在庫数量");

            // データセット
            foreach (DataRow data in tbl.Rows)
            {
                DataRow row = CSVデータ.NewRow();
                row["会社コード"] = myCompany.Text1;
                row["会社名"] = myCompany.Text2;
                row["倉庫コード"] = data["倉庫コード"].ToString();
                row["倉庫名"] = data["倉庫名"].ToString();
                row["棚卸日"] = DateTime.TryParse(data["棚卸日"].ToString(), out p棚卸日) ? p棚卸日.ToShortDateString() : string.Empty;
                row["品番コード"] = data["品番コード"].ToString();
                row["自社品番"] = data["自社品番"].ToString();
                row["自社品名"] = data["自社品名"].ToString();
                row["自社色"] = data["自社色"].ToString();
                row["賞味期限"] = data["表示用賞味期限"].ToString();
                row["数量"] = data["数量"].ToString();
                row["単位"] = data["単位"].ToString();
                row["更新在庫数量"] = (bool)writeListChk.IsChecked ? string.Empty : data["実在庫数"].ToString();
                CSVデータ.Rows.Add(row);
            }

            return CSVデータ;
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
            if (string.IsNullOrEmpty(this.MaintenanceMode)) return;

            var value = grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Value;

            switch (columName)
            {
                case "倉庫コード":

                    string soukCd = value == null ? "" : value.ToString();
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_SOUK, new object[] { AppCommon.IntParse(soukCd), AppCommon.IntParse(会社コード) }));

                    break;

                case "品番コード":

                    string hinCd = value == null ? "" : value.ToString();
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_HIN, new object[] { hinCd }));

                    break;

                case "自社品番":

                    if (value == null) return;
                    string jisHin = value == null ? "" : value.ToString();

                    base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_UcCustomerProduct, jisHin,
                                null,
                                null));

                    break;
                default:
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

        #region 明細行編集終了時処理

        private void gcRowGrid_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
        {

            //明細行が存在しない場合は処理しない
            if (SearchDetail == null) return;
            if (SearchDetail.Select("", "", DataViewRowState.CurrentRows).Count() == 0) return;

            // フィルターされたセルの編集を確定
            DetailDataView[stokGridCtl.ActiveRowIndex].EndEdit();

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
            dicCond.Add("商品分類", cmbItemType.SelectedValue.ToString());
            dicCond.Add("ブランドコード", Brand.Text1);
            dicCond.Add("シリーズコード", Series.Text1);

            return dicCond;

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

            DataSet ds = new DataSet();
            ds.Tables.Add(SearchDetail.Copy());

            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    UPDATE_S10_STOCKTAKING,
                    new object[] {
                        ds,
                        int.Parse(myCompany.Text1),
                        StocktakingDate.Text,
                        dicCond ,
                        ccfg.ユーザID,
                        this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false,

                    }));

            base.SetBusyForInput();
        }

        #endregion

        #region 会社コード変更イベント

        /// <summary>
        /// 会社コード変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myCompany_cText1Changed(object sender, RoutedEventArgs e)
        {
            // 会社コードが変更された場合、クリア
            Warehouse.Text1 = string.Empty;
        }

        #endregion


        #region フィルター再適用

        #endregion
    }

}
