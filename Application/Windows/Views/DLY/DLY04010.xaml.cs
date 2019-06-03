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

    /// <summary>
    /// 商品移動/振替入力
    /// </summary>
    public partial class DLY04010 : RibbonWindowViewBase
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
        public class ConfigDLY04010 : FormConfigBase
        {
		}

        /// ※ 必ず public で定義する。
        public ConfigDLY04010 frmcfg = null;

        #endregion

		#region 定数定義

        /// <summary>移動情報取得</summary>
		private const string T05_GetData = "T05_GetData";
        /// <summary>移動情報更新</summary>
        private const string T05_Update = "T05_Update";

        /// <summary>自社品番情報取得</summary>
        private const string MasterCode_MyProduct = "UcMyProduct";

        /// <summary>移動ヘッダ テーブル名</summary>
        private const string T05_HEADER_TABLE_NAME = "T05_IDOHD";
        /// <summary>移動明細 テーブル名</summary>
        private const string T05_DETAIL_TABLE_NAME = "T05_IDODTL";
        
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
            摘要 = 4,
            品番コード = 5,
            消費税区分 = 6,
            商品分類 = 7
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
        /// 移動区分 内包データ
        /// </summary>
        private enum 移動区分 : int
        {
            通常移動 = 1,
            売上移動 = 2,
            調整移動 = 3
        }

        #endregion

        #region バインディングデータ

        /// <summary>移動ヘッダ情報</summary>
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

        /// <summary>移動明細情報</summary>
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

        #endregion

        #region << グリッドボタンコマンド >>

        /// <summary>
        /// SPREADの【構成部品】ボタンを押下時の処理
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
                    int rowNo = cellCommandParameter.CellPosition.Row;
                    var row = this._gcSpreadGrid.Rows[rowNo];
                    var 品番コード = row.Cells[(int)GridColumnsMapping.品番コード].Value;

                    // 未設定行の場合は処理しない
                    if (品番コード == null || string.IsNullOrEmpty(品番コード.ToString()))
                        return;

                    //throw new NotImplementedException("画面未実装");

                    var wnd = GetWindow(this._gcSpreadGrid);
                    //DLY02020 form = new DLY02020();
                    //form.品番コード = int.Parse(品番コード.ToString());
                    //form.行番号 = rowNo + 1;
                    //form.自社品番 = row.Cells[(int)GridColumnsMapping.自社品番].Value.ToString();
                    //form.自社色 = "";
                    //form.自社品名 = row.Cells[(int)GridColumnsMapping.自社品名].Value.ToString();
                    //form.自社色名 = "";

                    MST03010 form = new MST03010();
                    form.txtMyProduct.Text1 = row.Cells[(int)GridColumnsMapping.自社品番].Value.ToString();
                    form.txtMyColor.Text1 = "";

                    form.ShowDialog(wnd);

                    //var p商品ID = row.Cells[this._gcSpreadGrid.Columns["商品ID"].Index].Value;
                    //var p入庫日 = row.Cells[this._gcSpreadGrid.Columns["str入庫日"].Index].Value;
                    //var p保管料計算開始日 = row.Cells[this._gcSpreadGrid.Columns["str保管料計算開始日"].Index].Value;
                    //var p賞味期限 = row.Cells[this._gcSpreadGrid.Columns["str賞味期限"].Index].Value;
                    //var pロケーション番号 = row.Cells[this._gcSpreadGrid.Columns["ロケーション"].Index].Value;
                    //var pロット番号 = row.Cells[this._gcSpreadGrid.Columns["ロット番号"].Index].Value;
                    //var p得意先コード = row.Cells[this._gcSpreadGrid.Columns["得意先コード"].Index].Value;
                    //var wnd = GetWindow(this._gcSpreadGrid);
                    //var query = this._gcSpreadGrid.ItemsSource;
                    //DateTime Wk;
                    //DateTime? d入庫日, d賞味期限;
                    //d入庫日 = p入庫日 == null ? (DateTime?)null : DateTime.TryParse(p入庫日.ToString(), out Wk) ? Wk : (DateTime?)null;
                    //d賞味期限 = p賞味期限 == null ? (DateTime?)null : DateTime.TryParse(p賞味期限.ToString(), out Wk) ? Wk : (DateTime?)null;

                    //var innercmd在庫詳細 = typeof(DLY31010.cmd在庫詳細);
                    //var outercmd在庫詳細 = innercmd在庫詳細.DeclaringType;


                    //DLY31020 frm = new DLY31020();
                    //frm.在庫参照元 = 1;
                    //frm.在庫商品ID = p商品ID == null ? string.Empty : (string)p商品ID;
                    //frm.在庫入庫日 = d入庫日;
                    //frm.在庫保管料計算開始日 = (DateTime?)null;
                    //frm.在庫賞味期限 = d賞味期限;
                    //frm.在庫ロケーション番号 = pロケーション番号 == null ? string.Empty : (string)pロケーション番号;
                    //frm.在庫ロット番号 = pロット番号 == null ? string.Empty : (string)pロット番号;
                    //frm.得意先コード = (int?)p得意先コード;
                    //frm.在庫行番号 = (int?)rowNo;
                    //frm.出庫データ = query;

                    //frm.ShowDialog(wnd);

                    //this._gcSpreadGrid.Columns["在庫照会"].Focusable = false;

                }

            }

        }

        #endregion


        #region << 初期処理群 >>

        /// <summary>
        /// 商品移動/振替入力　コンストラクタ
        /// </summary>
        public DLY04010()
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

            frmcfg = (ConfigDLY04010)ucfg.GetConfigValue(typeof(ConfigDLY04010));

            #endregion

            if (frmcfg == null)
            {
                frmcfg = new ConfigDLY04010();
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

            gcSpreadGrid.InputBindings.Add(new KeyBinding(gcSpreadGrid.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));

            // グリッドボタンにコマンド配置
            //ButtonCellType btn = new ButtonCellType();
            //btn.Content = "構成部品";
            //btn.Command = new cmd構成部品(gcSpreadGrid);
            //this.gcSpreadGrid.Columns[(int)GridColumnsMapping.構成部品].CellType = btn;

            // 検索画面情報を設定
            base.MasterMaintenanceWindowList.Add("M09_HIN", new List<Type> { typeof(MST02010), typeof(SCHM09_HIN) });
            base.MasterMaintenanceWindowList.Add("M11_TEK", new List<Type> { typeof(MST08010), typeof(SCHM11_TEK) });
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });
            base.MasterMaintenanceWindowList.Add("M22_SOUK", new List<Type> { typeof(MST12020), typeof(SCHM22_SOUK) });

            // コンボデータ取得
            AppCommon.SetutpComboboxList(this.cmb移動区分, false);

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
                    case T05_GetData:
                        // 伝票検索または新規伝票の場合
                        DataSet ds = data as DataSet;
                        if (ds != null)
                        {
                            SetTblData(ds);
                            ChangeKeyItemChangeable(false);
                        }
                        else
                        {
                            MessageBox.Show("指定の伝票番号は登録されていません。", "伝票未登録", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            this.txt伝票番号.Focus();
                        }
                        break;

                    case T05_Update:
                        MessageBox.Show(AppConst.SUCCESS_UPDATE, "登録完了", MessageBoxButton.OK, MessageBoxImage.Information);
                        // コントロール初期化
                        ScreenClear();
                        break;

                    case MasterCode_MyProduct:
                        #region 自社品番 手入力時
                        DataTable ctbl = data as DataTable;
                        int rIdx = gcSpreadGrid.ActiveRowIndex;

                        if (ctbl == null || ctbl.Rows.Count == 0)
                        {
                            // 対象データなしの場合
                            gcSpreadGrid.Cells[rIdx, (int)GridColumnsMapping.品番コード].Value = 0;
                            gcSpreadGrid.Cells[rIdx, (int)GridColumnsMapping.自社品番].Value = string.Empty;
                            gcSpreadGrid.Cells[rIdx, (int)GridColumnsMapping.自社品名].Value = string.Empty;
                            gcSpreadGrid.Cells[rIdx, (int)GridColumnsMapping.数量].Value = 0m;
                            gcSpreadGrid.Cells[rIdx, (int)GridColumnsMapping.消費税区分].Value = 0;   // [軽減税率対象]0:対象外
                            gcSpreadGrid.Cells[rIdx, (int)GridColumnsMapping.商品分類].Value = 3;    // [商品分類]3:その他

                        }
                        else if (ctbl.Rows.Count > 1)
                        {
                            // 対象データが複数ある場合
                            int cIdx = gcSpreadGrid.ActiveColumnIndex;
                            var colVal = gcSpreadGrid.Cells[rIdx, (int)GridColumnsMapping.自社品番].Value;

                            SCHM09_MYHIN myhin = new SCHM09_MYHIN();
                            myhin.txtCode.Text = colVal == null ? string.Empty : colVal.ToString();
                            myhin.txtCode.IsEnabled = false;
                            myhin.TwinTextBox = new UcLabelTwinTextBox();
                            myhin.TwinTextBox.LinkItem = 1;
                            if (myhin.ShowDialog(this) == true)
                            {
                                gcSpreadGrid.Cells[rIdx, (int)GridColumnsMapping.品番コード].Value = myhin.SelectedRowData["品番コード"].ToString();
                                gcSpreadGrid.Cells[rIdx, (int)GridColumnsMapping.自社品番].Value = myhin.SelectedRowData["自社品番"].ToString();
                                gcSpreadGrid.Cells[rIdx, (int)GridColumnsMapping.自社品名].Value = myhin.SelectedRowData["自社品名"].ToString();
                                gcSpreadGrid.Cells[rIdx, (int)GridColumnsMapping.数量].Value = 1m;
                                gcSpreadGrid.Cells[rIdx, (int)GridColumnsMapping.消費税区分].Value = myhin.SelectedRowData["消費税区分"];
                                gcSpreadGrid.Cells[rIdx, (int)GridColumnsMapping.商品分類].Value = myhin.SelectedRowData["商品分類"];

                                // 自社品番のセルをロック
                                gcSpreadGrid.Cells[rIdx, (int)GridColumnsMapping.自社品番].Locked = true;

                            }

                        }
                        else
                        {
                            // 対象データありの場合
                            DataRow drow = ctbl.Rows[0];
                            gcSpreadGrid.Cells[rIdx, (int)GridColumnsMapping.品番コード].Value = drow["品番コード"].ToString();
                            gcSpreadGrid.Cells[rIdx, (int)GridColumnsMapping.自社品番].Value = drow["自社品番"].ToString();
                            gcSpreadGrid.Cells[rIdx, (int)GridColumnsMapping.自社品名].Value = drow["自社品名"].ToString();
                            gcSpreadGrid.Cells[rIdx, (int)GridColumnsMapping.数量].Value = 1m;
                            gcSpreadGrid.Cells[rIdx, (int)GridColumnsMapping.消費税区分].Value = drow["消費税区分"];
                            gcSpreadGrid.Cells[rIdx, (int)GridColumnsMapping.商品分類].Value = drow["商品分類"];

                            // 自社品番のセルをロック
                            gcSpreadGrid.Cells[rIdx, (int)GridColumnsMapping.自社品番].Locked = true;

                        }

                        #endregion

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
                    int cIdx = spgrid.ActiveColumnIndex;
                    int rIdx = spgrid.ActiveRowIndex;

                    #region グリッドファンクションイベント
                    if (spgrid.ActiveColumnIndex == (int)GridColumnsMapping.自社品番)
                    {
                        // 対象セルがロックされている場合は処理しない
                        if (spgrid.Cells[rIdx, cIdx].Locked == true)
                            return;

                        // 自社品番または得意先品番の場合
                        SCHM09_MYHIN myhin = new SCHM09_MYHIN();
                        myhin.txtCode.Text = spgrid.Cells[rIdx, cIdx].Value == null ? string.Empty : spgrid.Cells[rIdx, cIdx].Value.ToString();
                        myhin.TwinTextBox = new UcLabelTwinTextBox();
                        myhin.TwinTextBox.LinkItem = 1;

                        if (myhin.ShowDialog(this) == true)
                        {
                            spgrid.Cells[rIdx, (int)GridColumnsMapping.品番コード].Value = myhin.SelectedRowData["品番コード"].ToString();
                            spgrid.Cells[rIdx, (int)GridColumnsMapping.自社品番].Value = myhin.SelectedRowData["自社品番"].ToString();
                            spgrid.Cells[rIdx, (int)GridColumnsMapping.自社品名].Value = myhin.SelectedRowData["自社品名"].ToString();
                            spgrid.Cells[rIdx, (int)GridColumnsMapping.数量].Value = 1m;
                            spgrid.Cells[rIdx, (int)GridColumnsMapping.消費税区分].Value = myhin.SelectedRowData["消費税区分"];
                            spgrid.Cells[rIdx, (int)GridColumnsMapping.商品分類].Value = myhin.SelectedRowData["商品分類"];

                            // 設定自社品番の編集を不可とする
                            spgrid.Cells[rIdx, cIdx].Locked = true;

                        }

                    }
                    else if (spgrid.ActiveColumnIndex == (int)GridColumnsMapping.摘要)
                    {
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
                this.txt移動日.Focus();
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

        #endregion

        #region << 検索データ設定・登録・削除処理 >>

        /// <summary>
        /// 取得内容を各コントロールに設定
        /// </summary>
        /// <param name="ds"></param>
        private void SetTblData(DataSet ds)
        {
            // 移動ヘッダ情報設定
            DataTable tblHd = ds.Tables[T05_HEADER_TABLE_NAME];
            SearchHeader = tblHd.Rows[0];
            SearchHeader.AcceptChanges();

            // 移動明細情報設定
            DataTable tblDtl = ds.Tables[T05_DETAIL_TABLE_NAME];
            SearchDetail = tblDtl;
            SearchDetail.AcceptChanges();

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
                    SearchDetail.Rows[i].SetAdded();

                }

                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;

                this.txt移動日.Focus();

            }
            else
            {
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;

                // 取得明細の自社品番をロック(編集不可)に設定
                foreach (var row in gcSpreadGrid.Rows)
                {
                    if (row.Cells[(int)GridColumnsMapping.自社品番].Value != null &&
                            string.IsNullOrEmpty(row.Cells[(int)GridColumnsMapping.自社品番].Value.ToString()))
                        row.Cells[(int)GridColumnsMapping.自社品番].Locked = true;
                }

                this.gcSpreadGrid.Focus();
                this.gcSpreadGrid.ActiveCellPosition = new CellPosition(0, (int)GridColumnsMapping.自社品番);

            }

        }

        /// <summary>
        /// 移動情報の登録処理をおこなう
        /// </summary>
        private void Update()
        {
            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    T05_Update,
                    new object[] {
                        SearchDetail.DataSet,
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

            #region 【ヘッダ】必須入力チェック

            // 移動日
            if (string.IsNullOrEmpty(this.txt移動日.Text))
            {
                this.txt移動日.Focus();
                base.ErrorMessage = string.Format("移動日が入力されていません。");
                return isResult;

            }

            // 移動区分
            if (this.cmb移動区分.SelectedValue == null)
            {
                this.cmb移動区分.Focus();
                base.ErrorMessage = string.Format("移動区分が選択されていません。");
                return isResult;

            }

            int kbn = int.Parse(this.cmb移動区分.SelectedValue.ToString());
            switch (kbn)
            {
                case (int)移動区分.通常移動:
                    if (string.IsNullOrEmpty(this.txt移動元倉庫.Text1))
                    {
                        this.txt移動元倉庫.Focus();
                        base.ErrorMessage = string.Format("移動元倉庫が入力されていません。");
                        return isResult;
                    }

                    if (string.IsNullOrEmpty(this.txt移動先倉庫.Text1))
                    {
                        this.txt移動先倉庫.Focus();
                        base.ErrorMessage = string.Format("移動先倉庫が入力されていません。");
                        return isResult;
                    }
                    break;

                case (int)移動区分.売上移動:
                    if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
                    {
                        base.ErrorMessage = string.Format("売上移動データを作成する事はできません。");
                        return isResult;
                    }
                    else if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_EDIT)
                    {
                        base.ErrorMessage = string.Format("売上移動データを変更する事はできません。");
                        return isResult;
                    }
                    break;

                case (int)移動区分.調整移動:
                    if (!string.IsNullOrEmpty(this.txt移動元倉庫.Text1) &&
                        !string.IsNullOrEmpty(this.txt移動先倉庫.Text1))
                    {
                        this.txt移動元倉庫.Focus();
                        base.ErrorMessage = string.Format("調整移動の場合、移動元倉庫か移動先倉庫のどちらかのみしか設定できません。");
                        return isResult;
                    }
                    break;

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
                gcSpreadGrid.Rows[rIdx].ValidationErrors.Clear();

                if (string.IsNullOrEmpty(row["数量"].ToString()))
                {
                    gcSpreadGrid.Rows[rIdx]
                        .ValidationErrors.Add(new SpreadValidationError("数量が入力されていません。", null, rIdx, (int)GridColumnsMapping.数量));
                    if (!isDetailErr)
                        gcSpreadGrid.ActiveCellPosition = new CellPosition(rIdx, (int)GridColumnsMapping.数量);

                    isDetailErr = true;
                }

                int type = Convert.ToInt32(row["商品分類"]);
                DateTime date;
                if (!DateTime.TryParse(row["賞味期限"].ToString(), out date))
                {
                    // 変換に失敗かつ商品分類が「食品」の場合はエラー
                    if (type.Equals((int)商品分類.食品))
                    {
                        gcSpreadGrid.Rows[rIdx]
                            .ValidationErrors.Add(new SpreadValidationError("商品分類が『食品』の為、賞味期限の設定が必要です。", null, rIdx, (int)GridColumnsMapping.賞味期限));
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
            this.cmb移動区分.SelectedIndex = 0;
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

            this.gcSpreadGrid.IsEnabled = !flag;

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
                        T05_GetData,
                        new object[] {
                            this.txt会社名.Text1,
                            this.txt伝票番号.Text,
                            ccfg.ユーザID
                        }));

            }

        }

        #region Window_Closed

        /// <summary>
        /// 画面が閉じられた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            if (frmcfg == null) { frmcfg = new ConfigDLY04010(); }

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;

            ucfg.SetConfigValue(frmcfg);

        }

        #endregion

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

            switch (e.CellPosition.ColumnName)
            {
                case "自社品番":
                    var target = grid.Cells[e.CellPosition.Row, e.CellPosition.Column].Value;
                    if (target == null)
                        return;

                    // 自社品番(または得意先品番)からデータを参照し、取得内容をグリッドに設定
                    base.SendRequest(
                        new CommunicationObject(
                            MessageType.RequestData,
                            MasterCode_MyProduct,
                            new object[] {
                                target.ToString()
                            }));
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

            return gcSpreadGrid.Cells[rIdx, (int)column].Value;

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

            gcSpreadGrid.Cells[rIdx, (int)column].Value = value;

        }

        #endregion

    }

}
