using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;

namespace KyoeiSystem.Application.Windows.Views
{

    using FwRepPreview = KyoeiSystem.Framework.Reports.Preview;
    using WinForms = System.Windows.Forms;
    using GrapeCity.Windows.SpreadGrid;
    using KyoeiSystem.Framework.Windows.Controls;
    using System.Windows.Controls;

    /// <summary>
    /// 製品原価計算表 フォームクラス
    /// </summary>
    public partial class BSK06010 : WindowReportBase
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
        public class ConfigBSK06010 : FormConfigBase
        {
            public byte[] spConfig = null;
        }

        /// ※ 必ず public で定義する。
        public ConfigBSK06010 frmcfg = null;
        CommonConfig ccfg = null;
        // SPREAD初期状態保存用
        public byte[] spConfig = null;

        #endregion

        #region バインディングデータ
        /// <summary>
        /// 商品原価計算表クラス
        /// </summary>  
        public class CostingSheetMember : INotifyPropertyChanged
        {
            private int _品番コード;
            public int 品番コード { get { return _品番コード; } set { _品番コード = value; NotifyPropertyChanged(); } }
            private string _自社品番;
            public string 自社品番 { get { return _自社品番; } set { _自社品番 = value; NotifyPropertyChanged(); } }
            private string _自社品名;
            public string 自社品名 { get { return _自社品名; } set { _自社品名 = value; NotifyPropertyChanged(); } }
            private string _色コード;
            public string 色コード { get { return _色コード; } set { _色コード = value; NotifyPropertyChanged(); } }
            private string _色名称;
            public string 色名称 { get { return _色名称; } set { _色名称 = value; NotifyPropertyChanged(); } }
            private decimal? _原価;
            public decimal? 原価 { get { return _原価; } set { _原価 = value; NotifyPropertyChanged(); } }
            private decimal? _数量;
            public decimal? 数量 { get { return _数量; } set { _数量 = value; NotifyPropertyChanged(); } }
            private decimal? _金額;
            public decimal? 金額 { get { return _金額; } set { _金額 = value; NotifyPropertyChanged(); } }
            private string _仕入先;
            public string 仕入先 { get { return _仕入先; } set { _仕入先 = value; NotifyPropertyChanged(); } }
            private string _資材;
            public string 資材 { get { return _資材; } set { _資材 = value; NotifyPropertyChanged(); } }
            private string _内容;
            public string 内容 { get { return _内容; } set { _内容 = value; NotifyPropertyChanged(); } }

            /// <summary>1:構成品、2:資材、3:その他</summary>
            private int _明細区分;
            public int 明細区分 { get { return _明細区分; } set { _明細区分 = value; NotifyPropertyChanged(); } }

            private int? _行番号;
            public int? 行番号 { get { return _行番号; } set { _行番号 = value; NotifyPropertyChanged(); } }

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

        /// <summary>
        /// 構成品明細情報
        /// </summary>
        private List<CostingSheetMember> _KouseiHinList;
        public List<CostingSheetMember> 構成品明細リスト
        {
            get
            {
                return this._KouseiHinList;
            }
            set
            {
                this._KouseiHinList = value;
                if (value == null)
                {
                    this.sp構成品明細.ItemsSource = null;
                }
                else
                {
                    this.sp構成品明細.ItemsSource = value;
                }
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 資材明細情報
        /// </summary>
        private List<CostingSheetMember> _ShizaiList;
        public List<CostingSheetMember> 資材明細リスト
        {
            get
            {
                return this._ShizaiList;
            }
            set
            {
                this._ShizaiList = value;
                if (value == null)
                {
                    this.sp資材明細.ItemsSource = null;
                }
                else
                {
                    this.sp資材明細.ItemsSource = value;
                }
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// その他明細情報
        /// </summary>
        private List<CostingSheetMember> _SonotaList;
        public List<CostingSheetMember> その他明細リスト
        {
            get
            {
                return this._SonotaList;
            }
            set
            {
                this._SonotaList = value;
                if (value == null)
                {
                    this.spその他明細.ItemsSource = null;
                }
                else
                {
                    this.spその他明細.ItemsSource = value;
                }
                NotifyPropertyChanged();
            }
        }


        /// <summary>色情報</summary>
        private string _自社色情報 = string.Empty;
        public string 自社色情報
        {
            get { return _自社色情報; }
            set
            {
                _自社色情報 = value;
                NotifyPropertyChanged();
            }
        }

        private int _iSetID;
        public int iSetID { get { return _iSetID; } set { _iSetID = value; NotifyPropertyChanged(); } }

        #region フッターバインディングデータ

        private decimal? _販社販売価格;
        public decimal? 販社販売価格
        {
            get { return _販社販売価格; }
            set { _販社販売価格 = value; NotifyPropertyChanged(); }
        }

        private decimal? _得意先販売価格;
        public decimal? 得意先販売価格
        {
            get { return _得意先販売価格; }
            set { _得意先販売価格 = value; NotifyPropertyChanged(); }
        }

        private int _食品割増率 = 0;
        public int 食品割増率
        {
            get { return _食品割増率; }
            set { _食品割増率 = value; NotifyPropertyChanged(); }
        }

        #endregion

        #endregion

        #region << 定数定義 >>

        /// <summary>小計フォーマット定義</summary>
        private const string SUBTOTAL_FORMAT_STRING = "{0:#,0.0}";

        /// <summary>合計フォーマット定義</summary>
        private const string TOTAL_FORMAT_STRING = "{0:#,0}";

        /// <summary> セット品の自社品番情報取得</summary>
        private const string SetHin_MyProduct = "BSK06010_GetSetHinProduct";

        /// <summary>セット品番情報検索</summary>
        private const string GetShinDataList = "BSK06010_GetShinDataList";

        /// <summary>構成品の自社品番情報取得</summary>
        private const string Kouseihin_MyProduct = "BSK06010_KouseihinProduct";

        /// <summary>資材の自社品番情報取得</summary>
        private const string Shizai_MyProduct = "BSK06010_ShizaiProduct";

        /// <summary>仕入売価情報取得</summary>
        private const string GetShireBaika = "BSK06010_GetBaikaData";

        /// <summary>更新登録処理 </summary>
        private const string UpdateData = "BSK06010_UpdateData";
        /// <summary>印刷用更新登録処理</summary>
        private const string UpdatePrint = "BSK06010_UpdatePrint";
        /// <summary>削除処理 </summary>
        private const string DeleteData = "BSK06010_DeleteData";
        /// <summary>新製品情報取得 </summary>
        private const string GetNewShinDataList = "BSK06010_GetNewSHinProduct";

        /// <summary>帳票定義体ファイルパス</summary>
        private const string ReportFileName = @"Files\BSK\BSK06010.rpt";

        // 明細リスト初期項目
        readonly string[] SHIZAI_INIT_LIST = { "内カートン", "パッド", "外カートン", "包装紙" };
        readonly string[] SONOTA_INIT_LIST = { "加工費", "包装代", "運賃 概算" };


        #endregion

        #region << クラス変数定義 >>

        /// <summary>
        /// 編集中の行番号
        /// </summary>
        private int sp編集行;

        /// <summary>
        /// 明細区分
        /// </summary>
        private enum 明細区分 : int
        {
            構成品 = 1,
            資材 = 2,
            その他 = 3,
        }


        /// <summary>
        /// 編集中のセル名
        /// </summary>
        private string eCellName;          // No.409 Add

        /// <summary>
        /// 編集中のセル
        /// </summary>
        private string eCellText;          // No.409 Add

        #endregion

        #region << 画面初期処理 >>

        /// <summary>
        /// 製品原価計算表 コンストラクタ
        /// </summary>
        public BSK06010()
        {
            InitializeComponent();
            this.DataContext = this;

        }

        #region Window_Load

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigBSK06010)ucfg.GetConfigValue(typeof(ConfigBSK06010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigBSK06010();
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
            base.MasterMaintenanceWindowList.Add("M09_MYHIN", new List<Type> { null, typeof(SCHM09_MYHIN) });

            //Enterボタン処理イベント
            sp構成品明細.InputBindings.Add(new KeyBinding(sp構成品明細.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));
            sp資材明細.InputBindings.Add(new KeyBinding(sp資材明細.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));
            spその他明細.InputBindings.Add(new KeyBinding(spその他明細.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));

            ScreenClear();

            SetFocusToTopControl();
        }

        #endregion

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

                    case SetHin_MyProduct:
                        #region 自社品番マスタに存在するかチェック

                        if (tbl == null || tbl.Rows.Count == 0)
                        {

                            // No369 Add Start
                            // 自社品番マスタに存在しない場合、新セット品として表示する

                            SetHinban.Text2 = "新セット品";

                            List<CostingSheetMember> rowlist = new List<CostingSheetMember>();
                            CostingSheetMember row = new CostingSheetMember();
                            row.明細区分 = (int)明細区分.構成品;
                            rowlist.Add(row);

                            // 画面に反映
                            構成品明細リスト = rowlist;

                            //ロックしたデータを解除し、伝票番号をロック
                            ChangeKeyItemChangeable(false);

                            sp構成品明細.Focus();

                            // No369 Add End
                        }
                        else if (tbl.Rows.Count > 1)
                        {
                            // 自社品番の場合
                            SCHM09_MYHIN myhin = new SCHM09_MYHIN();
                            myhin.TwinTextBox = new UcLabelTwinTextBox();
                            myhin.IsDisabledItemTypes = new[] { 2, 3, 4 };                         // No.362 Mod
                            myhin.txtCode.Text = SetHinban.Text1;
                            myhin.txtCode.IsEnabled = false;
                            myhin.TwinTextBox.LinkItem = 2;

                            if (myhin.ShowDialog(this) == true)
                            {
                                SetHinban.Text2 = myhin.SelectedRowData["自社品名"].ToString();
                                this.自社色情報 = myhin.SelectedRowData["自社色名"].ToString();

                                sendSearchForShin(myhin.SelectedRowData["品番コード"].ToString());
                            }

                        }
                        else
                        {
                            // 対象データありの場合
                            DataRow drow = tbl.Rows[0];

                            // No369 Add Start
                            // セット品ではない品番の場合、エラーを表示
                            if (int.Parse(drow["商品形態分類"].ToString()) != 1)
                            {
                                this.ErrorMessage = "セット品の自社品番を入力してください。";
                                return;
                            }
                            // No369 Add End

                            SetHinban.Text2 = drow["自社品名"].ToString();
                            this.自社色情報 = drow["色名称"].ToString();

                            sendSearchForShin(drow["品番コード"].ToString());
                        }

                        MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;

                        #endregion
                        break;

                    case GetShinDataList:
                        #region セット品取得時

                        // データが存在する場合、構成品にセットする
                        if (tbl != null && tbl.Rows.Count != 0)
                        {
                            構成品明細リスト = (List<CostingSheetMember>)AppCommon.ConvertFromDataTable(typeof(List<CostingSheetMember>), tbl);
                        }
                        else
                        {
                            List<CostingSheetMember> rowlist = new List<CostingSheetMember>();
                            CostingSheetMember row = new CostingSheetMember();
                            row.明細区分 = (int)明細区分.構成品;
                            rowlist.Add(row);

                            // 画面に反映
                            構成品明細リスト = rowlist;
                        }

                        // 金額再計算
                        summaryCalculation();

                        //ロックしたデータを解除し、伝票番号をロック
                        ChangeKeyItemChangeable(false);

                        sp構成品明細.Focus();

                        #endregion
                        break;

                    case Kouseihin_MyProduct:
                        #region 構成品手入力時

                        int kRow = sp編集行;

                        if (tbl == null || tbl.Rows.Count == 0)
                        {

                            sp構成品明細.Cells[kRow, "自社品名"].Value = string.Empty;
                            sp構成品明細.Cells[kRow, "色コード"].Value = string.Empty;
                            sp構成品明細.Cells[kRow, "色名称"].Value = string.Empty;
                            sp構成品明細.Cells[kRow, "品番コード"].Value = string.Empty;
                            sp構成品明細.Cells[kRow, "原価"].Value = null;
                            sp構成品明細.Cells[kRow, "数量"].Value = null;
                            sp構成品明細.Cells[kRow, "金額"].Value = null;

                            // No.370 Add Start
                            this.sp構成品明細.ActiveCellPosition = new CellPosition(kRow, "自社品番");
                            this.ErrorMessage = "対象データが存在しません。";
                            // No.370 Add End

                        }
                        else if (tbl.Rows.Count > 1)
                        {
                            // 自社品番の場合
                            SCHM09_MYHIN myhin = new SCHM09_MYHIN();
                            myhin.TwinTextBox = new UcLabelTwinTextBox();
                            myhin.IsDisabledItemTypes = null;                             // No.427 Mod
                            myhin.txtCode.Text = tbl.Rows[0]["自社品番"].ToString();
                            myhin.txtCode.IsEnabled = false;
                            if (myhin.ShowDialog(this) == true)
                            {
                                // 対象データありの場合
                                DataRow drow = myhin.SelectedRowData;

                                decimal d原価 = AppCommon.DecimalParse(drow["マスタ原価"].ToString());                             // No.371 Mod
                                decimal d数量 = sp構成品明細.Cells[kRow, "数量"].Value == null ? 0 : AppCommon.DecimalParse(sp構成品明細.Cells[kRow, "数量"].Value.ToString());

                                sp構成品明細.Cells[kRow, "自社品番"].Value = drow["自社品番"].ToString();
                                sp構成品明細.Cells[kRow, "自社品名"].Value = drow["自社品名"].ToString();
                                sp構成品明細.Cells[kRow, "色コード"].Value = drow["自社色"].ToString();
                                sp構成品明細.Cells[kRow, "色名称"].Value = drow["自社色名"].ToString();
                                sp構成品明細.Cells[kRow, "品番コード"].Value = drow["品番コード"].ToString();
                                sp構成品明細.Cells[kRow, "原価"].Value = d原価;
                                sp構成品明細.Cells[kRow, "数量"].Value = d数量;
                                sp構成品明細.Cells[kRow, "金額"].Value = Math.Ceiling((d原価 * d数量 * 10)) / 10;

                                // 仕入先売価マスタの売価と仕入先名を取得
                                base.SendRequest(
                                    new CommunicationObject(
                                        MessageType.RequestData,
                                        GetShireBaika,
                                        new object[] {
                                            　　AppCommon.IntParse(drow["品番コード"].ToString())
                                            　　, (int)明細区分.構成品
                                            }));
                            }
                        }
                        else
                        {
                            // 対象データありの場合
                            DataRow drow = tbl.Rows[0];

                            // No.427 Del Start
                            // 構成品ではない品番の場合、エラーを表示
                            //if (int.Parse(drow["商品形態分類"].ToString()) != 2)
                            //{
                            //    sp構成品明細.Cells[kRow, "自社品名"].Value = string.Empty;
                            //    sp構成品明細.Cells[kRow, "色コード"].Value = string.Empty;
                            //    sp構成品明細.Cells[kRow, "色名称"].Value = string.Empty;
                            //    sp構成品明細.Cells[kRow, "品番コード"].Value = string.Empty;
                            //    sp構成品明細.Cells[kRow, "原価"].Value = null;
                            //    sp構成品明細.Cells[kRow, "数量"].Value = null;
                            //    sp構成品明細.Cells[kRow, "金額"].Value = null;

                            //    this.sp構成品明細.ActiveCellPosition = new CellPosition(kRow, "自社品番");
                            //    this.ErrorMessage = "構成品の自社品番を入力してください。";

                            //    // 金額再計算
                            //    summaryCalculation();
                            //    return;
                            //}
                            // No.427 Del End

                            decimal d原価 = AppCommon.DecimalParse(drow["原価"].ToString());
                            decimal d数量 = sp構成品明細.Cells[kRow, "数量"].Value == null ? 0 : AppCommon.DecimalParse(sp構成品明細.Cells[kRow, "数量"].Value.ToString());

                            sp構成品明細.Cells[kRow, "自社品番"].Value = drow["自社品番"].ToString();
                            sp構成品明細.Cells[kRow, "自社品名"].Value = drow["自社品名"].ToString();
                            sp構成品明細.Cells[kRow, "色コード"].Value = drow["自社色"].ToString();
                            sp構成品明細.Cells[kRow, "色名称"].Value = drow["色名称"].ToString();
                            sp構成品明細.Cells[kRow, "品番コード"].Value = drow["品番コード"].ToString();
                            sp構成品明細.Cells[kRow, "原価"].Value = d原価;
                            sp構成品明細.Cells[kRow, "数量"].Value = d数量;
                            sp構成品明細.Cells[kRow, "金額"].Value = Math.Ceiling((d原価 * d数量 * 10)) / 10;
                            sp構成品明細.Cells[kRow, "仕入先"].Value = drow["仕入先名称"].ToString();

                            this.sp構成品明細.ActiveCellPosition = new CellPosition(kRow, "数量");
                        }

                        // 金額再計算
                        summaryCalculation();

                        #endregion
                        break;

                    case Shizai_MyProduct:
                        #region 資材情報手入力時

                        int sRow = sp編集行;

                        if (tbl == null || tbl.Rows.Count == 0)
                        {
                            sp資材明細.Cells[sRow, "自社品名"].Value = string.Empty;
                            sp資材明細.Cells[sRow, "品番コード"].Value = string.Empty;
                            sp資材明細.Cells[sRow, "原価"].Value = null;
                            sp資材明細.Cells[sRow, "数量"].Value = null;
                            sp資材明細.Cells[sRow, "金額"].Value = null;

                            // No.370 Add Start
                            this.sp資材明細.ActiveCellPosition = new CellPosition(sRow, "自社品番");
                            this.ErrorMessage = "対象データが存在しません。";
                            // No.370 Add End

                        }
                        else if (tbl.Rows.Count > 1)
                        {
                            // 自社品番の場合
                            SCHM09_MYHIN myhin = new SCHM09_MYHIN();
                            myhin.TwinTextBox = new UcLabelTwinTextBox();
                            myhin.IsDisabledItemTypes = new[] { 1, 2, 3 };                               // No.362 Mod
                            myhin.txtCode.Text = tbl.Rows[0]["自社品番"].ToString();
                            myhin.txtCode.IsEnabled = false;
                            //myhin.TwinTextBox.LinkItem = 2;

                            if (myhin.ShowDialog(this) == true)
                            {
                                // 対象データありの場合
                                DataRow drow = myhin.SelectedRowData;

                                // 売価情報設定用に行番号を保持
                                sp編集行 = sRow;

                                decimal d原価 = AppCommon.DecimalParse(drow["マスタ原価"].ToString());                             // No.371 Mod
                                decimal d数量 = sp資材明細.Cells[sRow, "数量"].Value == null ? 0 : AppCommon.DecimalParse(sp資材明細.Cells[sRow, "数量"].Value.ToString());

                                sp資材明細.Cells[sRow, "自社品番"].Value = drow["自社品番"].ToString();
                                sp資材明細.Cells[sRow, "自社品名"].Value = drow["自社品名"].ToString();
                                sp資材明細.Cells[sRow, "品番コード"].Value = drow["品番コード"].ToString();
                                sp資材明細.Cells[sRow, "原価"].Value = d原価;
                                sp資材明細.Cells[sRow, "数量"].Value = d数量;
                                sp資材明細.Cells[sRow, "金額"].Value = (d原価 == 0 || d数量 == 0) ? 0 : Math.Ceiling((d原価 / d数量 * 10)) / 10;

                                // 仕入先売価マスタの売価と仕入先名を取得
                                base.SendRequest(
                                    new CommunicationObject(
                                        MessageType.RequestData,
                                        GetShireBaika,
                                        new object[] {
                                            　　AppCommon.IntParse(drow["品番コード"].ToString())
                                            　　, (int)明細区分.資材
                                            }));
                            }

                        }
                        else
                        {
                            // 対象データありの場合
                            DataRow drow = tbl.Rows[0];

                            // No.363 Add Start
                            // 資材ではない品番の場合、エラーを表示
                            if (int.Parse(drow["商品形態分類"].ToString()) != 4)
                            {
                                sp資材明細.Cells[sRow, "自社品名"].Value = string.Empty;
                                sp資材明細.Cells[sRow, "品番コード"].Value = string.Empty;
                                sp資材明細.Cells[sRow, "原価"].Value = null;
                                sp資材明細.Cells[sRow, "数量"].Value = null;
                                sp資材明細.Cells[sRow, "金額"].Value = null;

                                this.sp資材明細.ActiveCellPosition = new CellPosition(sRow, "自社品番");
                                this.ErrorMessage = "資材の自社品番を入力してください。";

                                // 金額再計算
                                summaryCalculation();
                                return;
                            }
                            // No.363 Add End

                            decimal d原価 = AppCommon.DecimalParse(drow["原価"].ToString());
                            decimal d数量 = sp資材明細.Cells[sRow, "数量"].Value == null ? 0 : AppCommon.DecimalParse(sp資材明細.Cells[sRow, "数量"].Value.ToString());

                            sp資材明細.Cells[sRow, "自社品番"].Value = drow["自社品番"].ToString();
                            sp資材明細.Cells[sRow, "自社品名"].Value = drow["自社品名"].ToString();
                            sp資材明細.Cells[sRow, "品番コード"].Value = drow["品番コード"].ToString();
                            sp資材明細.Cells[sRow, "原価"].Value = d原価;
                            sp資材明細.Cells[sRow, "数量"].Value = d数量;
                            sp資材明細.Cells[sRow, "金額"].Value = (d原価 == 0 || d数量 == 0) ? 0 : Math.Ceiling((d原価 / d数量 * 10)) / 10;
                            sp資材明細.Cells[sRow, "仕入先"].Value = drow["仕入先名称"].ToString();

                            this.sp資材明細.ActiveCellPosition = new CellPosition(sRow, "数量");
                        }

                        // 金額再計算
                        summaryCalculation();

                        #endregion
                        break;

                    case GetShireBaika:
                        #region 売価情報取得時
                        int baikaSpRow = sp編集行;

                        if (tbl != null && tbl.Rows.Count != 0)
                        {
                            DataRow BaikaDrow = tbl.Rows[0];

                            switch (int.Parse(BaikaDrow["明細区分"].ToString()))
                            {
                                case (int)明細区分.構成品:
                                    sp構成品明細.Cells[baikaSpRow, "仕入先"].Value = BaikaDrow["仕入先名称"].ToString();
                                    sp構成品明細.Cells[baikaSpRow, "原価"].Value = AppCommon.DecimalParse(BaikaDrow["単価"].ToString());

                                    this.sp構成品明細.ActiveCellPosition = new CellPosition(baikaSpRow, "数量");

                                    break;
                                case (int)明細区分.資材:
                                    sp資材明細.Cells[baikaSpRow, "仕入先"].Value = BaikaDrow["仕入先名称"].ToString();
                                    sp資材明細.Cells[baikaSpRow, "原価"].Value = AppCommon.DecimalParse(BaikaDrow["単価"].ToString());

                                    this.sp資材明細.ActiveCellPosition = new CellPosition(baikaSpRow, "数量");
                                    break;
                            }
                        }

                        // 金額再計算
                        summaryCalculation();

                        #endregion
                        break;
                    case UpdateData:
                        #region 登録・更新完了
                        if ((bool)data)
                        {
                            MessageBox.Show("登録が完了しました。");
                            ScreenClear();
                        }
                        #endregion
                        break;
                    case UpdatePrint:
                        #region 登録・更新完了
                        ScreenClear();
                        #endregion
                        break;
                    case DeleteData:
                        #region 削除
                        if ((bool)data)
                        {
                            MessageBox.Show("製品を削除しました。");
                            ScreenClear();
                        }
                        #endregion
                        break;
                    case GetNewShinDataList:
                        if (data is DataSet)
                        {
                            SetData((DataSet)data);
                        }
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
                var uctext = ViewBaseCommon.FindVisualParent<UcLabelTwinTextBox>(elmnt as UIElement);

                if (spgrid != null)
                {
                    #region グリッドファンクションイベント

                    int iRow = spgrid.ActiveRow.Index;

                    //spgrid.CommitCellEdit();           // No.368 Del
                    spgrid.CancelCellEdit();             // No.368 Add

                    if (spgrid.Name == "sp構成品明細")
                    {
                        #region 構成品明細処理

                        switch (spgrid.ActiveColumn.Name)
                        {
                            case "自社品番":

                                // 自社品番の場合
                                SCHM09_MYHIN myhin = new SCHM09_MYHIN();
                                myhin.TwinTextBox = new UcLabelTwinTextBox();
                                myhin.IsDisabledItemTypes = null;                                          // No.427 Mod
                                if (myhin.ShowDialog(this) == true)
                                {
                                    // 対象データありの場合
                                    DataRow drow = myhin.SelectedRowData;

                                    // 売価情報設定用に行番号を保持
                                    sp編集行 = iRow;

                                    decimal d原価 = AppCommon.DecimalParse(drow["マスタ原価"].ToString());
                                    decimal d数量 = sp構成品明細.Cells[iRow, "数量"].Value == null ? 0 : AppCommon.DecimalParse(sp構成品明細.Cells[iRow, "数量"].Value.ToString());

                                    sp構成品明細.Cells[iRow, "自社品番"].Value = drow["自社品番"].ToString();
                                    sp構成品明細.Cells[iRow, "自社品名"].Value = drow["自社品名"].ToString();
                                    sp構成品明細.Cells[iRow, "色コード"].Value = drow["自社色"].ToString();
                                    sp構成品明細.Cells[iRow, "色名称"].Value = drow["自社色名"].ToString();
                                    sp構成品明細.Cells[iRow, "品番コード"].Value = drow["品番コード"].ToString();
                                    sp構成品明細.Cells[iRow, "原価"].Value = d原価;
                                    sp構成品明細.Cells[iRow, "数量"].Value = d数量;
                                    sp構成品明細.Cells[iRow, "金額"].Value = Math.Ceiling((d原価 * d数量 * 10)) / 10;

                                    // フォーカス設定
                                    this.sp構成品明細.ActiveCellPosition = new CellPosition(iRow, "自社品名");

                                    // 仕入先売価マスタの売価と仕入先名を取得
                                    base.SendRequest(
                                        new CommunicationObject(
                                            MessageType.RequestData,
                                            GetShireBaika,
                                            new object[] {
                                            　　AppCommon.IntParse(drow["品番コード"].ToString())
                                            　　, (int)明細区分.構成品
                                            }));

                                }

                                break;


                            case "仕入先":
                                SCHM01_TOK tok = new SCHM01_TOK();
                                tok.TwinTextBox = new UcLabelTwinTextBox();
                                string[] 仕入先LinkItem = new[] { "1,3", "0", "", "" };
                                tok.TwinTextBox.LinkItem = 仕入先LinkItem;

                                if (tok.ShowDialog(this) == true)
                                {
                                    sp構成品明細.Cells[iRow, "仕入先"].Value = tok.TwinTextBox.Text3;
                                }
                                break;
                        }

                        #endregion
                    }
                    else if (spgrid.Name == "sp資材明細")
                    {
                        #region 資材明細処理

                        switch (spgrid.ActiveColumn.Name)
                        {
                            case "自社品番":

                                // 自社品番の場合
                                SCHM09_MYHIN myhin = new SCHM09_MYHIN();
                                myhin.TwinTextBox = new UcLabelTwinTextBox();
                                myhin.IsDisabledItemTypes = new[] { 1, 2, 3 };                                       // No.362 Mod
                                if (myhin.ShowDialog(this) == true)
                                {
                                    // 対象データありの場合
                                    DataRow drow = myhin.SelectedRowData;

                                    // 売価情報設定用に行番号を保持
                                    sp編集行 = iRow;

                                    decimal d原価 = AppCommon.DecimalParse(drow["マスタ原価"].ToString());
                                    decimal d数量 = sp資材明細.Cells[iRow, "数量"].Value == null ? 0 : AppCommon.DecimalParse(sp資材明細.Cells[iRow, "数量"].Value.ToString());

                                    sp資材明細.Cells[iRow, "自社品番"].Value = drow["自社品番"].ToString();
                                    sp資材明細.Cells[iRow, "自社品名"].Value = drow["自社品名"].ToString();
                                    sp資材明細.Cells[iRow, "品番コード"].Value = drow["品番コード"].ToString();
                                    sp資材明細.Cells[iRow, "原価"].Value = d原価;
                                    sp資材明細.Cells[iRow, "数量"].Value = d数量;
                                    sp資材明細.Cells[iRow, "金額"].Value = (d原価 == 0 || d数量 == 0) ? 0 : Math.Ceiling((d原価 / d数量 * 10)) / 10;

                                    // フォーカス設定
                                    this.sp資材明細.ActiveCellPosition = new CellPosition(iRow, "自社品名");

                                    // 仕入先売価マスタの売価と仕入先名を取得
                                    base.SendRequest(
                                        new CommunicationObject(
                                            MessageType.RequestData,
                                            GetShireBaika,
                                            new object[] {
                                            　　AppCommon.IntParse(drow["品番コード"].ToString())
                                            　　, (int)明細区分.資材
                                            }));

                                }

                                break;


                            case "仕入先":
                                SCHM01_TOK tok = new SCHM01_TOK();
                                tok.TwinTextBox = new UcLabelTwinTextBox();
                                string[] 仕入先LinkItem = new[] { "1,3", "0", "", "" };
                                tok.TwinTextBox.LinkItem = 仕入先LinkItem;

                                if (tok.ShowDialog(this) == true)
                                {
                                    sp資材明細.Cells[iRow, "仕入先"].Value = tok.TwinTextBox.Text3;
                                }
                                break;
                        }

                        #endregion
                    }

                    #endregion
                }
                else if (uctext != null && uctext.DataAccessName == "M09_MYHIN")
                {
                    #region テキストボックスファンクションイベント

                    SCHM09_MYHIN myhin = new SCHM09_MYHIN();
                    myhin.txtCode.Text = uctext.Text1;
                    myhin.TwinTextBox = new UcLabelTwinTextBox();
                    myhin.IsDisabledItemTypes = new[] { 2, 3, 4 };                                    // No.362 Mod
                    if (myhin.ShowDialog(this) == true)
                    {
                        SetHinban.Text1 = myhin.SelectedRowData["自社品番"].ToString();
                        SetHinban.Text2 = myhin.SelectedRowData["自社品名"].ToString();
                        自社色情報 = myhin.SelectedRowData["自社色名"].ToString();
                        // セット品データを取得
                        sendSearchForShin(myhin.SelectedRowData["品番コード"].ToString());
                        MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                    }

                    #endregion
                }
                else
                {
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                }
            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";

            }

        }
        #endregion

        #region F2 既存検索
        /// <summary>
        /// F2　リボン　既存検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF2Key(object sender, KeyEventArgs e)
        {
            try
            {

                object elmnt = FocusManager.GetFocusedElement(this);
                var uctext = ViewBaseCommon.FindVisualParent<UcLabelTwinTextBox>(elmnt as UIElement);

                if (uctext != null && uctext.DataAccessName == "M09_MYHIN")
                {
                    #region テキストボックスファンクションイベント

                    SCHM10_NEWSHIN myhin = new SCHM10_NEWSHIN();
                    myhin.txtCode.Text = uctext.Text1;
                    myhin.TwinTextBox = new UcLabelTwinTextBox();
                    if (myhin.ShowDialog(this) == true)
                    {
                        SetHinban.Text1 = myhin.SelectedRowData["セット品番"].ToString();
                        SetHinban.Text2 = myhin.SelectedRowData["セット品名"].ToString();
                        // 新製品データを取得
                        iSetID = (int)myhin.SelectedRowData["SETID"];
                        MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
                        ChangeKeyItemChangeable(false);

                        // セット品の構成品を取得
                        this.SendRequest(
                            new CommunicationObject(
                                MessageType.RequestData,
                                GetNewShinDataList,
                                new object[] {
                            iSetID
                        }));
                    }

                    #endregion
                }
                else
                {
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                }
            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";

            }

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

            // 金額再計算
            summaryCalculation();

            // 入力チェック
            if (string.IsNullOrEmpty(SetHinban.Text1))
            {
                this.ErrorMessage = "セット品番を入力してください。";
                return;
            }

            Update(false);

        }
        /// <summary>
        /// 更新処理を行う
        /// </summary>
        private void Update(bool pbPrintFlg)
        {

            try
            {
                if (!base.CheckAllValidation())
                {
                    string msg = "入力内容に誤りがあります。";
                    this.ErrorMessage = msg;
                    MessageBox.Show(msg);

                    SetFocusToTopControl();
                    return;

                }

                if (pbPrintFlg == false)
                {
                    // 確認メッセージ表示
                    var yesno = MessageBox.Show(
                                    "入力内容を登録しますか？",
                                    "登録確認",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Question,
                                    MessageBoxResult.Yes);
                    if (yesno == MessageBoxResult.No)
                    {
                        return;
                    }
                }
                DataSet dsUpdate = CreateDataSet();

                string message = pbPrintFlg ? UpdatePrint : UpdateData;


                base.SendRequest(
                    new CommunicationObject(
                        MessageType.UpdateData,
                        message,
                        new object[] {
                            MaintenanceMode == AppConst.MAINTENANCEMODE_ADD,
                            iSetID,
                            SetHinban.Text1,
                            SetHinban.Text2,
                            食品割増率,
                            販社販売価格 == null ? 0 : 販社販売価格,
                            得意先販売価格 == null ? 0 : 得意先販売価格,
                            dsUpdate,
                            ccfg.ユーザID
                        }));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// DataSet作成
        /// </summary>
        /// <returns></returns>
        private DataSet CreateDataSet()
        {
            DataSet ds = new DataSet();

            DataTable tblDtl = new DataTable();
            AppCommon.ConvertToDataTable(構成品明細リスト, tblDtl);

            DataTable tblShizai = new DataTable();
            AppCommon.ConvertToDataTable(資材明細リスト, tblShizai);

            DataTable tblETC = new DataTable();
            AppCommon.ConvertToDataTable(その他明細リスト, tblETC);

            ds.Tables.Add(tblDtl);
            ds.Tables.Add(tblShizai);
            ds.Tables.Add(tblETC);

            return ds;
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

            // 金額再計算
            summaryCalculation();

            // 入力チェック
            if (string.IsNullOrEmpty(SetHinban.Text1))
            {
                this.ErrorMessage = "セット品番を入力してください。";
                return;
            }


            // 製品原価計算表リスト作成
            List<CostingSheetMember> 製品原価リスト = new List<CostingSheetMember>();

            // 構成品
            製品原価リスト.AddRange(SetCostingKbn(構成品明細リスト, (int)明細区分.構成品));
            // 資材
            製品原価リスト.AddRange(SetCostingKbn(資材明細リスト, (int)明細区分.資材));
            // その他
            製品原価リスト.AddRange(SetCostingKbn(その他明細リスト, (int)明細区分.その他));

            var 製品原価データ = new DataTable();
            AppCommon.ConvertToDataTable(製品原価リスト, 製品原価データ);

            Update(true);


            outputReport(製品原価データ);

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
            this.Close();
        }

        #endregion

        #region F12 削除
        /// <summary>
        /// F11　リボン　削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF12Key(object sender, KeyEventArgs e)
        {
            Delete();
        }

        /// <summary>
        /// 削除SQL発行
        /// </summary>
        private void Delete()
        {
            if (MaintenanceMode != AppConst.MAINTENANCEMODE_EDIT)
            {
                return;
            }

            if (MessageBox.Show("表示している製品を削除してよろしいでしょうか？", "確認", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }

            base.SendRequest(
                    new CommunicationObject(
                        MessageType.UpdateData,
                        DeleteData,
                        new object[] {
                            iSetID,
                            ccfg.ユーザID
                        }));
        }
        #endregion
        #endregion

        #region << 機能処理関連 >>

        #region 画面初期化
        /// <summary>
        /// 画面初期化
        /// </summary>
        private void ScreenClear()
        {
            this.MaintenanceMode = null;

            SetHinban.Text1 = string.Empty;
            SetHinban.Text2 = string.Empty;

            // スプレッドの初期値をセット
            SpeadInit();           // No.366 Add

            lbl構成品小計.Content = string.Empty;
            lbl資材小計.Content = string.Empty;
            lblその他小計.Content = string.Empty;
            lbl合計.Content = string.Empty;
            lbl食品割増.Content = string.Empty;

            txb販社販売価格.Text = string.Empty;
            txb得意先販売価格.Text = string.Empty;

            lbl販社粗利.Content = string.Empty;
            lbl得意先粗利.Content = string.Empty;

            this.食品割増率 = 0;
            MaintenanceMode = null;

            ResetAllValidation();
            ChangeKeyItemChangeable(true);
            SetFocusToTopControl();
        }
        #endregion

        #region スプレッド初期値設定
        /// <summary>
        /// スプレッド初期値設定
        /// </summary>
        private void SpeadInit()
        {
            this.構成品明細リスト = new List<CostingSheetMember>();
            this.資材明細リスト = new List<CostingSheetMember>();
            this.その他明細リスト = new List<CostingSheetMember>();

            // No366 Add Start
            // 構成品初期値設定

            for (int i = 0; i < 15; i++)
            {
                CostingSheetMember row = new CostingSheetMember();
                row.明細区分 = (int)明細区分.構成品;
                構成品明細リスト.Add(row);
            }

            // 画面に反映
            構成品明細リスト = RemakeCostingList(構成品明細リスト);

            // No366 Add End

            // 資材初期値設定
            foreach (var rShizai in SHIZAI_INIT_LIST)
            {
                CostingSheetMember row = new CostingSheetMember();
                row.資材 = rShizai;
                row.明細区分 = (int)明細区分.資材;
                資材明細リスト.Add(row);
            }

            // 画面に反映
            資材明細リスト = RemakeCostingList(資材明細リスト);

            // その他初期値設定
            foreach (var rSonota in SONOTA_INIT_LIST)
            {
                CostingSheetMember row = new CostingSheetMember();
                row.内容 = rSonota;
                row.明細区分 = (int)明細区分.その他;
                その他明細リスト.Add(row);
            }

            // 画面に反映
            その他明細リスト = RemakeCostingList(その他明細リスト);

            // スプレッドの初期カーソル位置を設定
            sp資材明細_LostFocus(null, null);
            spその他明細_LostFocus(null, null);

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

            this.SetHinban.IsEnabled = true;
            this.SetHinban.Text1IsReadOnly = !flag;

            this.sp構成品明細.IsEnabled = !flag;
            this.sp資材明細.IsEnabled = !flag;
            this.spその他明細.IsEnabled = !flag;

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
                int iProduct = int.Parse(strProduct);

                // セット品の構成品を取得
                this.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        GetShinDataList,
                        new object[] {
                            iProduct
                        }));

            }
        }

        #endregion

        #region 帳票出力
        /// <summary>
        /// 帳票の印刷処理をおこなう
        /// </summary>
        /// <param name="tbl"></param>
        private void outputReport(DataTable tbl)
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

                var parms = new List<FwRepPreview.ReportParameter>()
                {
                    #region 印字パラメータ設定

                    new FwRepPreview.ReportParameter(){ PNAME = "構成品小計", VALUE = lbl構成品小計.Content.ToString()},
                    new FwRepPreview.ReportParameter(){ PNAME = "資材小計", VALUE = lbl資材小計.Content.ToString()},
                    new FwRepPreview.ReportParameter(){ PNAME = "その他小計", VALUE = lblその他小計.Content.ToString()},
                    new FwRepPreview.ReportParameter(){ PNAME = "原価合計", VALUE = lbl合計.Content.ToString()},
                    new FwRepPreview.ReportParameter(){ PNAME = "食品割増", VALUE = lbl食品割増.Content.ToString()},
                    new FwRepPreview.ReportParameter(){ PNAME = "販社販売価格", VALUE = txb販社販売価格.Text},
                    new FwRepPreview.ReportParameter(){ PNAME = "得意先販売価格", VALUE = txb得意先販売価格.Text},
                    new FwRepPreview.ReportParameter(){ PNAME = "ヘッダ自社品番", VALUE = SetHinban.Text1},
                    new FwRepPreview.ReportParameter(){ PNAME = "ヘッダ自社品名", VALUE = SetHinban.Text2},
                    new FwRepPreview.ReportParameter(){ PNAME = "ヘッダ自社色名", VALUE = 自社色情報},
                    new FwRepPreview.ReportParameter(){ PNAME = "食品割増率", VALUE = 食品割増率},
                    new FwRepPreview.ReportParameter(){ PNAME = "販社粗利", VALUE = lbl販社粗利.Content.ToString()},         // No.394 Mod
                    new FwRepPreview.ReportParameter(){ PNAME = "得意先粗利", VALUE = lbl得意先粗利.Content.ToString()},     // No.394 Add

                    #endregion
                };

                DataTable 印刷データ = tbl.Copy();
                印刷データ.TableName = "製品原価計算表";

                FwRepPreview.ReportPreview view = new FwRepPreview.ReportPreview();
                view.MakeReport(印刷データ.TableName, ReportFileName, 0, 0, 0);
                view.SetReportData(印刷データ);

                view.SetupParmeters(parms);

                base.SetFreeForInput();

                view.PrinterName = frmcfg.PrinterName;
                view.ShowPreview();
                view.Close();
                frmcfg.PrinterName = view.PrinterName;

            }
            catch (Exception ex)
            {
                base.SetFreeForInput();
                this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
                appLog.Error("製品原価計算表の印刷時に例外が発生しました。", ex);
            }

        }
        #endregion

        #region 明細データ再作成
        /// <summary>
        /// ItemSource通知用リスト作成
        /// </summary>
        /// <returns></returns>
        private List<CostingSheetMember> RemakeCostingList(List<CostingSheetMember> CostingSheetList)
        {
            // 明細データを画面通知用に再作成する
            List<CostingSheetMember> retList = new List<CostingSheetMember>();

            var Spreadデータ = new DataTable();
            AppCommon.ConvertToDataTable(CostingSheetList, Spreadデータ);
            retList = (List<CostingSheetMember>)AppCommon.ConvertFromDataTable(typeof(List<CostingSheetMember>), Spreadデータ);

            return retList;
        }
        #endregion

        #region 明細判別用区分をセット

        /// <summary>
        /// 構成品、資材、その他が判別できるよう区分を設定
        /// </summary>
        /// <param name="CostingSheetList"></param>
        /// <param name="kbn"></param>
        private List<CostingSheetMember> SetCostingKbn(List<CostingSheetMember> CostingSList, int kbn)
        {
            List<CostingSheetMember> retList = new List<CostingSheetMember>();

            // 構成品、資材、その他が判別できるよう区分を設定
            foreach (var row in CostingSList)
            {
                row.明細区分 = kbn;
            }

            retList = CostingSList;

            return retList;
        }

        #endregion

        /// <summary>
        /// 新製品情報セット
        /// </summary>
        /// <param name="pDataSet"></param>
        public void SetData(DataSet pDataSet)
        {
            DataTable dthd = pDataSet.Tables["M10_HD"];
            DataTable dtdtl = pDataSet.Tables["M10_DTL"];
            DataTable dtshizai = pDataSet.Tables["M10_SHIZAI"];
            DataTable dtetc = pDataSet.Tables["M10_ETC"];

            if (dthd.Rows.Count > 0)
            {
                食品割増率 = (int)dthd.Rows[0]["食品割増率"];
                販社販売価格 = (decimal)dthd.Rows[0]["販社販売価格"];
                得意先販売価格 = (decimal)dthd.Rows[0]["得意先販売価格"];
            }

            if (dtdtl != null)
            {
                構成品明細リスト = new List<CostingSheetMember>();
                //構成品リスト
                foreach (DataRow dr in dtdtl.Rows)
                {
                    CostingSheetMember mem = new CostingSheetMember();

                    mem.自社品番 = dr["自社品番"].ToString();
                    mem.自社品名 = dr["自社品名"].ToString();
                    mem.色コード = dr["色コード"].ToString();

                    if (string.IsNullOrEmpty(mem.色コード) == false)
                    {
                        //色コードが入っていたら色名称を取得
                    }

                    mem.原価 = (decimal)dr["原価"];
                    mem.数量 = (decimal)dr["必要数量"];
                    mem.仕入先 = dr["仕入先名"].ToString();
                    mem.金額 = (mem.原価 == 0 || mem.数量 == 0) ? 0 : (decimal)(Math.Ceiling((double)(mem.原価 * mem.数量 * 10)) / 10);
                    mem.明細区分 = (int)明細区分.構成品;
                    mem.行番号 = dr["構成行"] == null ? 0 : AppCommon.IntParse(dr["構成行"].ToString());

                    構成品明細リスト.Add(mem);
                }
                構成品明細リスト = RemakeCostingList(構成品明細リスト);
            }
            if (dtshizai != null)
            {
                資材明細リスト = new List<CostingSheetMember>();
                //資材リスト
                foreach (DataRow dr in dtshizai.Rows)
                {
                    CostingSheetMember mem = new CostingSheetMember();
                    mem.資材 = dr["資材名"].ToString();
                    mem.自社品番 = dr["自社品番"].ToString();
                    mem.自社品名 = dr["自社品名"].ToString();
                    mem.原価 = (decimal)dr["原価"];
                    mem.数量 = (decimal)dr["入数"];
                    mem.仕入先 = dr["仕入先名"].ToString();
                    mem.金額 = (mem.原価 == 0 || mem.数量 == 0) ? 0 : (decimal)(Math.Ceiling((double)(mem.原価 / mem.数量 * 10)) / 10);
                    mem.明細区分 = (int)明細区分.資材;
                    mem.行番号 = dr["行番号"] == null ? 0 : AppCommon.IntParse(dr["行番号"].ToString());
                    資材明細リスト.Add(mem);
                }
                資材明細リスト = RemakeCostingList(資材明細リスト);
            }
            if (dtetc != null)
            {
                その他明細リスト = new List<CostingSheetMember>();
                //その他リスト
                foreach (DataRow dr in dtetc.Rows)
                {
                    CostingSheetMember mem = new CostingSheetMember();
                    mem.内容 = dr["内容"].ToString();
                    mem.原価 = (decimal)dr["原価"];
                    mem.数量 = (decimal)dr["入数"];
                    mem.金額 = (mem.原価 == 0 || mem.数量 == 0) ? 0 : (decimal)(Math.Ceiling((double)(mem.原価 / mem.数量 * 10)) / 10);
                    mem.明細区分 = (int)明細区分.その他;
                    mem.行番号 = dr["行番号"] == null ? 0 : AppCommon.IntParse(dr["行番号"].ToString());
                    その他明細リスト.Add(mem);
                }
                その他明細リスト = RemakeCostingList(その他明細リスト);
            }

            //各種金額再計算
            summaryCalculation();
        }

        #endregion

        #region << ヘッダーイベント処理 >>

        /// <summary>
        /// セット品番でキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetHinban_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (string.IsNullOrEmpty(SetHinban.Text1) || !string.IsNullOrEmpty(SetHinban.Text2)) return;

                    // 自社品番からデータを参照し、取得内容をグリッドに設定
                    base.SendRequest(
                        new CommunicationObject(
                            MessageType.RequestData,
                            SetHin_MyProduct,
                            new object[] {
                                SetHinban.Text1
                            }));
                }
                catch (Exception ex)
                {
                    appLog.Debug("【Error:{0}***{1}】", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                    this.ErrorMessage = ex.Message;
                }
            }
        }

        /// <summary>
        /// セット品番でキーが変更された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetHinban_cText1Changed(object sender, RoutedEventArgs e)
        {
            // セット品番が変更された場合、品番情報を初期化
            SetHinban.Text2 = string.Empty;
            this.自社色情報 = string.Empty;
        }

        #region 食品割増率イベント
        /// <summary>
        /// 食品割増率でキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HinPremium_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (Key.Enter == e.Key)
            {
                if (string.IsNullOrEmpty(HinPremium.Text)) 食品割増率 = 0;

                // 構成品明細をフォーカス
                this.sp構成品明細.Focus();
                if (this.sp構成品明細.Rows.Count - 1 >= 0)
                {
                    this.sp構成品明細.ActiveCellPosition = new CellPosition(0, "自社品番");
                }
                e.Handled = true;
            }
        }

        /// <summary>
        /// 食品割増率でキーのフォーカスが外れた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HinPremium_LostFocus(object sender, RoutedEventArgs e)
        {
            // 金額再計算
            summaryCalculation();
        }

        #endregion

        #endregion

        #region << スプレッドイベント処理 >>

        #region 構成品イベント処理

        #region 構成品明細CellEditEnding
        private void sp構成品明細_CellEditEnding(object sender, SpreadCellEditEndingEventArgs e)
        {
            // No.409 Add Start
            if (e.EditAction == SpreadEditAction.Cancel)
            {
                return;
            }

            // 編集前のデータを保持
            eCellName = e.CellPosition.ColumnName;
            eCellText = sp構成品明細.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
            // No.409 Add End
        }
        #endregion

        #region 構成品明細CellEditEnded
        /// <summary>
        /// SPREAD セル編集がコミットされた時の処理(手入力) CellEditEnadedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sp構成品明細_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
        {
            try
            {
                var grid = sender as GcSpreadGrid;
                int iRow = e.CellPosition.Row;

                if (e.EditAction == SpreadEditAction.Cancel)                    // No.368 Add
                    return;

                // 行番号を保持
                sp編集行 = iRow;

                switch (e.CellPosition.ColumnName)
                {

                    case "自社品番":

                        string target = grid.Cells[iRow, "自社品番"].Text;

                        // 変更がなければ処理しない
                        if (eCellText == target) return;          // No.409 Add

                        // No.370 Add Start
                        if (string.IsNullOrEmpty(target))
                        {
                            // 空欄の場合クリア
                            grid.Cells[iRow, "自社品名"].Value = string.Empty;
                            grid.Cells[iRow, "色コード"].Value = string.Empty;
                            grid.Cells[iRow, "色名称"].Value = string.Empty;
                            grid.Cells[iRow, "品番コード"].Value = string.Empty;
                            grid.Cells[iRow, "原価"].Value = null;
                            grid.Cells[iRow, "数量"].Value = null;
                            grid.Cells[iRow, "金額"].Value = null;

                            // 金額再計算
                            summaryCalculation();
                            return;
                        }
                        // No.370 Add End

                        // 自社品番からデータを参照し、取得内容をグリッドに設定
                        base.SendRequest(
                            new CommunicationObject(
                                MessageType.RequestData,
                                Kouseihin_MyProduct,
                                new object[] {
                                target
                            }));

                        break;


                    case "原価":
                    case "数量":

                        var text = grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
                        if (string.IsNullOrWhiteSpace(text) == true)
                        {
                            grid.Cells[iRow, "金額"].Value = null;

                            // 金額再計算
                            summaryCalculation();
                            return;
                        }

                        decimal d原価 = grid.Cells[iRow, "原価"].Value == null ? 0 : AppCommon.DecimalParse(grid.Cells[iRow, "原価"].Value.ToString());
                        decimal d数量 = grid.Cells[iRow, "数量"].Value == null ? 0 : AppCommon.DecimalParse(grid.Cells[iRow, "数量"].Value.ToString());

                        grid.Cells[iRow, "原価"].Value = d原価;
                        grid.Cells[iRow, "数量"].Value = d数量;
                        grid.Cells[iRow, "金額"].Value = (d原価 == 0 || d数量 == 0) ? 0 : Math.Ceiling((d原価 * d数量 * 10)) / 10;

                        // 金額再計算
                        summaryCalculation();

                        break;
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }
        }

        #endregion

        #region 構成品行追加
        /// <summary>
        /// 構成品の行追加ボタンが押下された時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KouseiHinBtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sp構成品明細 == null || sp構成品明細.Rows.Count == 0)
                {
                    構成品明細リスト = new List<CostingSheetMember>();
                }

                // 最大15行まで
                if (sp構成品明細.Rows.Count >= 15)
                {
                    MessageBox.Show("明細行数が上限に達している為、これ以上追加できません。", "明細上限", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                CostingSheetMember row = new CostingSheetMember();
                row.明細区分 = (int)明細区分.構成品;
                構成品明細リスト.Add(row);

                // 画面に反映
                構成品明細リスト = RemakeCostingList(構成品明細リスト);

                // Spreadの初期フォーカス位置を追加行に設定
                sp構成品明細.Focus();
                int maxrow = sp構成品明細.Rows.Count - 1;
                this.sp構成品明細.ActiveCellPosition = new CellPosition(maxrow, 0);
            }
            catch (Exception ex)
            {
                appLog.Error("行追加失敗", ex);
                this.ErrorMessage = "行追加に失敗しました。一度画面を閉じもう一度お試しください。";
            }
        }

        #endregion

        #region 構成品行削除
        /// <summary>
        /// 構成品の行削除ボタンが押下された時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KouseiHinBtnDel_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (構成品明細リスト == null || 構成品明細リスト.Count == 0)
                {
                    return;
                }

                int activerow = sp構成品明細.ActiveRowIndex;

                var yesno = MessageBox.Show("選択した行を削除しますか？", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (yesno == MessageBoxResult.Yes)
                {

                    構成品明細リスト.RemoveAt(activerow);

                    // 画面に反映
                    構成品明細リスト = RemakeCostingList(構成品明細リスト);

                    // 金額再計算
                    summaryCalculation();

                    // フォーカスを設定
                    sp構成品明細.Focus();
                }
            }
            catch (Exception ex)
            {
                appLog.Error("行削除失敗", ex);
                this.ErrorMessage = "行削除に失敗しました。一度画面を閉じもう一度お試しください。";
            }
        }

        #endregion

        #region 構成品明細キーダウンイベント
        /// <summary>
        /// 構成品明細でキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sp構成品明細_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //Delete押下時の処理
            //注意 : SPREAD上でDELETEキーを押下すると例外エラーが発生します(Delete禁止)
            if (e.Key == Key.Delete)
            {
                e.Handled = true;
            }

            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                sp構成品明細.CommitCellEdit();

                // 最終行の仕入先まで入力したら
                if (sp構成品明細.Rows.Count - 1 == sp構成品明細.ActiveCellPosition.Row && sp構成品明細.ActiveCellPosition.ColumnName == "仕入先")
                {
                    // 資材明細をフォーカス
                    this.sp資材明細.Focus();
                    if (this.sp資材明細.Rows.Count - 1 >= 0)
                    {
                        this.sp資材明細.ActiveCellPosition = new CellPosition(0, "自社品番");
                    }
                    e.Handled = true;

                }
            }
        }
        #endregion

        #region 構成品明細ロストフォーカス
        /// <summary>
        /// 構成品明細でフォーカスが外れた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sp構成品明細_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sp構成品明細.Focusable == true)
            {
                sp構成品明細.SelectionBorder.Style = BorderLineStyle.Thick;
                sp構成品明細.SelectionBorderUnfocused.Style = BorderLineStyle.None;
            }
            else
            {
                sp構成品明細.SelectionBorder.Style = BorderLineStyle.None;
                sp構成品明細.SelectionBorderUnfocused.Style = BorderLineStyle.None;
            }
        }
        #endregion

        #endregion

        #region 資材明細イベント処理

        #region 資材明細CellEditEnding
        private void sp資材明細_CellEditEnding(object sender, SpreadCellEditEndingEventArgs e)
        {
            // No.409 Add Start
            if (e.EditAction == SpreadEditAction.Cancel)
            {
                return;
            }

            // 編集前のデータを保持
            eCellName = e.CellPosition.ColumnName;
            eCellText = sp資材明細.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
            // No.409 Add End
        }
        #endregion

        #region 資材明細CellEditEnded
        /// <summary>
        /// SPREAD セル編集がコミットされた時の処理(手入力) CellEditEnadedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sp資材明細_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
        {
            try
            {
                var grid = sender as GcSpreadGrid;
                int iRow = e.CellPosition.Row;

                if (e.EditAction == SpreadEditAction.Cancel)                    // No.368 Add
                    return;

                // 行番号を保持
                sp編集行 = iRow;

                switch (e.CellPosition.ColumnName)
                {
                    case "自社品番":

                        // No.370 Add Start
                        string target = grid.Cells[iRow, "自社品番"].Text;

                        // 変更がなければ処理しない
                        if (eCellText == target) return;      // No.409 Add

                        if (string.IsNullOrEmpty(target))
                        {
                            // 空欄の場合クリア
                            grid.Cells[iRow, "自社品名"].Value = string.Empty;
                            grid.Cells[iRow, "品番コード"].Value = string.Empty;
                            grid.Cells[iRow, "原価"].Value = null;
                            grid.Cells[iRow, "数量"].Value = null;
                            grid.Cells[iRow, "金額"].Value = null;

                            // 金額再計算
                            summaryCalculation();
                            return;
                        }
                        // No.370 Add End

                        // 自社品番からデータを参照し、取得内容をグリッドに設定
                        base.SendRequest(
                            new CommunicationObject(
                                MessageType.RequestData,
                                Shizai_MyProduct,
                                new object[] {
                                target
                            }));

                        break;

                    case "原価":
                    case "数量":

                        var text = grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
                        if (string.IsNullOrWhiteSpace(text) == true)
                        {
                            //if (grid.Cells[iRow, "自社品番"].Value == null)
                            //    grid.Cells[iRow, "原価"].Value = null;

                            grid.Cells[iRow, "金額"].Value = null;

                            // 金額再計算
                            summaryCalculation();
                            return;
                        }

                        decimal d原価 = grid.Cells[iRow, "原価"].Value == null ? 0 : AppCommon.DecimalParse(grid.Cells[iRow, "原価"].Value.ToString());
                        decimal d数量 = grid.Cells[iRow, "数量"].Value == null ? 0 : AppCommon.DecimalParse(grid.Cells[iRow, "数量"].Value.ToString());

                        grid.Cells[iRow, "原価"].Value = d原価;
                        grid.Cells[iRow, "数量"].Value = d数量;
                        grid.Cells[iRow, "金額"].Value = (d原価 == 0 || d数量 == 0) ? 0 : Math.Ceiling((d原価 / d数量 * 10)) / 10;

                        // 金額再計算
                        summaryCalculation();

                        break;
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }
        }

        #endregion

        #region 資材明細キーダウンイベント
        /// <summary>
        /// 資材明細でキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sp資材明細_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //Delete押下時の処理
            //注意 : SPREAD上でDELETEキーを押下すると例外エラーが発生します(Delete禁止)
            if (e.Key == Key.Delete)
            {
                e.Handled = true;
            }

            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                sp資材明細.CommitCellEdit();

                // 最終行の仕入先まで入力したら
                if (sp資材明細.Rows.Count - 1 == sp資材明細.ActiveCellPosition.Row && sp資材明細.ActiveCellPosition.ColumnName == "仕入先")
                {
                    // その他明細をフォーカス
                    this.spその他明細.Focus();
                    if (this.spその他明細.Rows.Count - 1 >= 0)
                    {
                        this.spその他明細.ActiveCellPosition = new CellPosition(0, "内容");
                    }
                    e.Handled = true;

                }
            }
        }
        #endregion

        #region 資材明細ロストフォーカス
        /// <summary>
        /// 資材明細でフォーカスが外れた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sp資材明細_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sp資材明細.Focusable == true)
            {
                sp資材明細.SelectionBorder.Style = BorderLineStyle.Thick;
                sp資材明細.SelectionBorderUnfocused.Style = BorderLineStyle.None;
            }
            else
            {
                sp資材明細.SelectionBorder.Style = BorderLineStyle.None;
                sp資材明細.SelectionBorderUnfocused.Style = BorderLineStyle.None;
            }
        }
        #endregion

        #endregion

        #region その他イベント処理

        #region その他明細CellEditEnded
        /// <summary>
        /// SPREAD セル編集がコミットされた時の処理(手入力) CellEditEnadedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spその他明細_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
        {
            try
            {
                var grid = sender as GcSpreadGrid;

                int sRow = e.CellPosition.Row;

                switch (e.CellPosition.ColumnName)
                {
                    case "原価":
                    case "数量":

                        var text = grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Text;
                        if (string.IsNullOrWhiteSpace(text) == true)
                        {
                            grid.Cells[sRow, "金額"].Value = null;

                            // 金額再計算
                            summaryCalculation();
                            return;
                        }

                        decimal d原価 = grid.Cells[sRow, "原価"].Value == null ? 0 : AppCommon.DecimalParse(grid.Cells[sRow, "原価"].Value.ToString());
                        decimal d数量 = grid.Cells[sRow, "数量"].Value == null ? 0 : AppCommon.DecimalParse(grid.Cells[sRow, "数量"].Value.ToString());

                        grid.Cells[sRow, "原価"].Value = d原価;
                        grid.Cells[sRow, "数量"].Value = d数量;
                        grid.Cells[sRow, "金額"].Value = (d原価 == 0 || d数量 == 0) ? 0 : Math.Ceiling((d原価 / d数量 * 10)) / 10;

                        // 金額再計算
                        summaryCalculation();

                        break;
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }
        }

        #endregion

        #region その他行追加
        /// <summary>
        /// その他行追加ボタンが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SonotaBtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (spその他明細 == null || spその他明細.Rows.Count == 0)
                {
                    その他明細リスト = new List<CostingSheetMember>();
                }

                // 最大10行まで
                if (spその他明細.Rows.Count >= 10)
                {
                    MessageBox.Show("明細行数が上限に達している為、これ以上追加できません。", "明細上限", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                //Spreadの行を1行追加します
                CostingSheetMember row = new CostingSheetMember();
                row.明細区分 = (int)明細区分.その他;
                その他明細リスト.Add(row);

                // 画面に反映
                その他明細リスト = RemakeCostingList(その他明細リスト);

                // Spreadの初期フォーカス位置を追加行に設定
                spその他明細.Focus();
                int maxrow = spその他明細.Rows.Count - 1;
                this.spその他明細.ActiveCellPosition = new CellPosition(maxrow, 0);
            }
            catch (Exception ex)
            {
                appLog.Error("行追加失敗", ex);
                this.ErrorMessage = "行追加に失敗しました。一度画面を閉じもう一度お試しください。";
            }
        }

        #endregion

        #region その他行削除
        /// <summary>
        /// その他行削除ボタンが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SonotaBtnDel_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (その他明細リスト == null || その他明細リスト.Count == 0)
                {
                    return;
                }

                int activerow = spその他明細.ActiveRowIndex;

                var yesno = MessageBox.Show("選択した行を削除しますか？", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (yesno == MessageBoxResult.Yes)
                {

                    その他明細リスト.RemoveAt(activerow);

                    // 画面に反映
                    その他明細リスト = RemakeCostingList(その他明細リスト);

                    // 金額再計算
                    summaryCalculation();

                    // フォーカスを設定
                    spその他明細.Focus();

                }
            }
            catch (Exception ex)
            {
                appLog.Error("行削除失敗", ex);
                this.ErrorMessage = "行削除に失敗しました。一度画面を閉じもう一度お試しください。";
            }
        }

        #endregion

        #region その他明細キーダウンイベント
        /// <summary>
        /// その他明細でキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spその他明細_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //Delete押下時の処理
            //注意 : SPREAD上でDELETEキーを押下すると例外エラーが発生します(Delete禁止)
            if (e.Key == Key.Delete)
            {
                e.Handled = true;
            }

            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                spその他明細.CommitCellEdit();

                // 最終行の入数まで入力したら
                if (spその他明細.Rows.Count - 1 == spその他明細.ActiveCellPosition.Row && spその他明細.ActiveCellPosition.ColumnName == "数量")
                {
                    // 販社販売価格をフォーカス
                    this.txb販社販売価格.Focus();

                    e.Handled = true;

                }
            }
        }


        #endregion

        #region その他明細ロストフォーカス
        /// <summary>
        /// その他明細でフォーカスが外れた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spその他明細_LostFocus(object sender, RoutedEventArgs e)
        {
            if (spその他明細.Focusable == true)
            {
                spその他明細.SelectionBorder.Style = BorderLineStyle.Thick;
                spその他明細.SelectionBorderUnfocused.Style = BorderLineStyle.None;
            }
            else
            {
                spその他明細.SelectionBorder.Style = BorderLineStyle.None;
                spその他明細.SelectionBorderUnfocused.Style = BorderLineStyle.None;
            }
        }
        #endregion

        #endregion

        #endregion

        #region << フッターイベント処理 >>

        /// <summary>
        /// 得意先販売価格でキーが押された時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txb得意先販売価格_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
            {
                // フォーカス移動
                HinPremium.Focus();
                e.Handled = true;
            }
        }

        /// <summary>
        /// 販社販売価格のフォーカスが外れた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txb販社販売価格_LostFocus(object sender, RoutedEventArgs e)
        {
            // 金額再計算
            summaryCalculation();      // No.394 Add
        }

        /// <summary>
        /// 得意先販売価格でキーのフォーカスが外れた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txb得意先販売価格_LostFocus(object sender, RoutedEventArgs e)
        {
            // 金額再計算
            summaryCalculation();      // No.394 Add
        }

        #endregion

        #region << 小計・合計関連処理 >>
        /// <summary>
        /// 明細内容を集計して結果を設定する
        /// </summary>
        private void summaryCalculation()
        {
            if (sp構成品明細 == null || sp資材明細 == null || spその他明細 == null)
                return;

            decimal d構成品小計 = 0;
            decimal d資材小計 = 0;
            decimal dその他小計 = 0;

            decimal d原価合計 = 0;
            decimal d食品割増 = 0;

            foreach (CostingSheetMember row in 構成品明細リスト)
            {
                d構成品小計 += row.金額 ?? 0;
            }

            foreach (CostingSheetMember row in 資材明細リスト)
            {
                d資材小計 += row.金額 ?? 0;
            }

            foreach (CostingSheetMember row in その他明細リスト)
            {
                dその他小計 += row.金額 ?? 0;
            }

            // 原価合計算出
            d原価合計 = Math.Ceiling(d構成品小計 + d資材小計 + dその他小計);

            // 食品割増算出
            d食品割増 = Math.Ceiling(d原価合計 * 食品割増率 / 100);

            lbl構成品小計.Content = string.Format(SUBTOTAL_FORMAT_STRING, d構成品小計);
            lbl資材小計.Content = string.Format(SUBTOTAL_FORMAT_STRING, d資材小計);
            lblその他小計.Content = string.Format(SUBTOTAL_FORMAT_STRING, dその他小計);
            lbl合計.Content = string.Format(TOTAL_FORMAT_STRING, d原価合計);
            lbl食品割増.Content = string.Format(TOTAL_FORMAT_STRING, d原価合計 + d食品割増);

            // No.394 Add Start
            // 粗利計算
            // 0で除算しないかチェック
            if (販社販売価格 != null && 販社販売価格 != 0)
            {
                // 粗利は小数点第2位を四捨五入
                lbl販社粗利.Content = Math.Round((decimal)((販社販売価格 - (d原価合計 + d食品割増)) / 販社販売価格 * 100), 1, MidpointRounding.AwayFromZero) + "％";
            }
            else
            {
                lbl販社粗利.Content = string.Empty;
            }

            if (得意先販売価格 != null && 得意先販売価格 != 0)
            {
                lbl得意先粗利.Content = Math.Round((decimal)((得意先販売価格 - (販社販売価格 ?? 0)) / 得意先販売価格 * 100), 1, MidpointRounding.AwayFromZero) + "％";
            }
            else
            {
                lbl得意先粗利.Content = string.Empty;
            }
            // No.394 Add End
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
            if (frmcfg == null) { frmcfg = new ConfigBSK06010(); }

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);

        }

        #endregion

    }
}
