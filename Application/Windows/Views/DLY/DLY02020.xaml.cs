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
    /// 構成品入力
    /// </summary>
    public partial class DLY02020 : RibbonWindowViewBase
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
        public class ConfigDLY02020 : FormConfigBase
        {
		}

        /// ※ 必ず public で定義する。
        public ConfigDLY02020 frmcfg = null;

        #endregion

		#region 定数定義

        /// <summary>揚り情報取得</summary>
		private const string T04_GetData = "T04_GetData";
        /// <summary>揚り情報更新</summary>
        private const string T04_Update = "T04_Update";
        /// <summary>揚り情報削除</summary>
        private const string T04_Delete = "T04_Delete";

        /// <summary>取引先名称取得</summary>
        private const string MasterCode_Supplier = "UcSupplier";
        /// <summary>自社品番情報取得</summary>
        private const string MasterCode_MyProduct = "UcMyProduct";

        #endregion

        #region 列挙型定義

        /// <summary>
        /// データグリッドの列定義
        /// </summary>
        private enum GridColumnsMapping : int
        {
            自社品番 = 0,
            自社色 = 1,
            自社品名 = 2,
            自社色名 = 3,
            品番コード = 4,
            賞味期限 = 5,
            数量 = 6,
            単位 = 7,
            必要数量 = 8,
            在庫 = 9
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

        public int 品番コード { get; set; }
        public int 行番号 { get; set; }
        public string 自社品番 { get; set; }
        public string 自社色 { get; set; }
        public string 自社品名 { get; set; }
        public string 自社色名 { get; set; }

        /// <summary>構成品明細情報</summary>
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
        /// SPREADの【在庫】ボタンを押下時の処理
        /// </summary>
        public class cmd在庫 : ICommand
        {
            private GcSpreadGrid _gcSpreadGrid;
            public cmd在庫(GcSpreadGrid gcSpreadGrid)
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
                    var 品番コード = row.Cells[GridColumnsMapping.品番コード.GetHashCode()].Value;

                    // 未設定行の場合は処理しない
                    if (品番コード == null || string.IsNullOrEmpty(品番コード.ToString()))
                        return;

                    throw new NotImplementedException("画面未実装");
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
        /// 構成品入力　コンストラクタ
        /// </summary>
        public DLY02020()
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

            frmcfg = (ConfigDLY02020)ucfg.GetConfigValue(typeof(ConfigDLY02020));

            #endregion

            if (frmcfg == null)
            {
                frmcfg = new ConfigDLY02020();
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
            ButtonCellType btn = new ButtonCellType();
            btn.Content = "在庫";
            btn.Command = new cmd在庫(gcSpreadGrid);
            this.gcSpreadGrid.Columns[GridColumnsMapping.在庫.GetHashCode()].CellType = btn;

            // 検索画面情報を設定
            base.MasterMaintenanceWindowList.Add("M09_HIN", new List<Type> { typeof(MST02010), typeof(SCHM09_HIN) });

            ScreenClear();
            ChangeKeyItemChangeable(true);

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
                        SetTblData(ds);
                        ChangeKeyItemChangeable(false);
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

                    case MasterCode_Supplier:
                        break;

                    case MasterCode_MyProduct:
                        #region 自社品番手入力時
                        DataTable ctbl = data as DataTable;
                        int rIdx = gcSpreadGrid.ActiveRowIndex;

                        if (ctbl == null || ctbl.Rows.Count == 0)
                        {
                            // 対象データなしの場合
                            setSpreadGridValue(rIdx, GridColumnsMapping.品番コード, 0);
                            setSpreadGridValue(rIdx, GridColumnsMapping.自社品番, string.Empty);
                            setSpreadGridValue(rIdx, GridColumnsMapping.自社品名, string.Empty);
                            setSpreadGridValue(rIdx, GridColumnsMapping.数量, 0m);
                            setSpreadGridValue(rIdx, GridColumnsMapping.単位, string.Empty);

                        }
                        else
                        {
                            // 対象データありの場合
                            DataRow drow = ctbl.Rows[0];
                            gcSpreadGrid.BeginEdit();
                            setSpreadGridValue(rIdx, GridColumnsMapping.品番コード, drow["品番コード"]);
                            setSpreadGridValue(rIdx, GridColumnsMapping.自社品番, drow["自社品番"]);
                            setSpreadGridValue(rIdx, GridColumnsMapping.自社品名, drow["自社品名"]);
                            setSpreadGridValue(rIdx, GridColumnsMapping.数量, 1m);
                            setSpreadGridValue(rIdx, GridColumnsMapping.単位, drow["単位"]);
                            gcSpreadGrid.CommitCellEdit();
                            // 自社品番のセルをロック
                            gcSpreadGrid.Cells[rIdx, GridColumnsMapping.自社品番.GetHashCode()].Locked = true;

                        }

                        SearchDetail.Rows[rIdx].EndEdit();

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

        #region 画面項目の初期化
        /// <summary>
        /// 画面の初期化処理をおこなう
        /// </summary>
        private void ScreenClear()
        {
            this.MaintenanceMode = null;

            if (SearchDetail != null)
                SearchDetail.Clear();

            ChangeKeyItemChangeable(true);
            ResetAllValidation();

        }

        /// <summary>
        /// 指定行の明細データを初期化する
        /// </summary>
        /// <param name="rIdx"></param>
        private void ClearRowItems(int rIdx)
        {
            gcSpreadGrid.Cells[rIdx, GridColumnsMapping.品番コード.GetHashCode()].Value = string.Empty;
            gcSpreadGrid.Cells[rIdx, GridColumnsMapping.自社品名.GetHashCode()].Value = string.Empty;
            gcSpreadGrid.Cells[rIdx, GridColumnsMapping.自社色名.GetHashCode()].Value = string.Empty;
            gcSpreadGrid.Cells[rIdx, GridColumnsMapping.賞味期限.GetHashCode()].Value = string.Empty;
            gcSpreadGrid.Cells[rIdx, GridColumnsMapping.数量.GetHashCode()].Value = string.Empty;
            gcSpreadGrid.Cells[rIdx, GridColumnsMapping.単位.GetHashCode()].Value = string.Empty;
            gcSpreadGrid.Cells[rIdx, GridColumnsMapping.必要数量.GetHashCode()].Value = string.Empty;

        }

        #endregion

        #region << リボン >>

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

                        // 自社品番の場合
                        SCHM09_MYHIN myhin = new SCHM09_MYHIN();
                        myhin.TwinTextBox = new UcLabelTwinTextBox();
                        myhin.TwinTextBox.LinkItem = 1;
                        if (myhin.ShowDialog(this) == true)
                        {
                            spgrid.Cells[rIdx, GridColumnsMapping.品番コード.GetHashCode()].Value = myhin.SelectedRowData["品番コード"];
                            spgrid.Cells[rIdx, GridColumnsMapping.自社品番.GetHashCode()].Value = myhin.SelectedRowData["自社品番"];
                            spgrid.Cells[rIdx, GridColumnsMapping.自社品名.GetHashCode()].Value = myhin.SelectedRowData["自社品名"];
                            spgrid.Cells[rIdx, GridColumnsMapping.数量.GetHashCode()].Value = 1m;
                            spgrid.Cells[rIdx, GridColumnsMapping.単位.GetHashCode()].Value = myhin.SelectedRowData["単位"];

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
                    if (spgrid.ActiveColumnIndex == GridColumnsMapping.自社品番.GetHashCode())
                    {
                        // 品番マスタ表示
                        MST02010 M09Form = new MST02010();
                        M09Form.Show(this);

                    }

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
            gcSpreadGrid.ActiveCellPosition = new CellPosition(newRowIdx, GridColumnsMapping.自社品番.GetHashCode());
            gcSpreadGrid.ShowCell(newRowIdx, GridColumnsMapping.自社品名.GetHashCode(), VerticalPosition.Bottom);

        }

        /// <summary>
        /// F06　リボン　行削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF6Key(object sender, KeyEventArgs e)
        {
            if (this.MaintenanceMode == null)
                return;

            if (MessageBox.Show(
                    AppConst.CONFIRM_DELETE_ROW,
                    "行削除確認",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.No) == MessageBoxResult.No)
                return;

            gcSpreadGrid.Rows.Remove(this.gcSpreadGrid.ActiveRowIndex);

        }

        /// <summary>
        /// F09　リボン　登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		public override void OnF9Key(object sender, KeyEventArgs e)
        {
            if (MaintenanceMode == null)
                return;

            if (MessageBox.Show(AppConst.CONFIRM_UPDATE,
                                "登録確認",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question,
                                MessageBoxResult.Yes) == MessageBoxResult.No)
                return;

            Update();

        }

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

        #region Window_Closed

        /// <summary>
        /// 画面が閉じられた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
		{
            if (frmcfg == null) { frmcfg = new ConfigDLY02020(); }

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
            // 揚り部材明細情報設定
            DataTable tblDtl = ds.Tables["T04_AGRDTL"];
            SearchDetail = tblDtl;
            SearchDetail.AcceptChanges();

            // データ状態から編集状態を設定
            if (tblDtl.Select("品番コード > 0").Count() == 0)
            {
                // 新規行を追加
                for (int i = 0; i < 10; i++)
                {
                    DataRow row = SearchDetail.NewRow();
                    row["行番号"] = (i + 1);

                    SearchDetail.Rows.Add(row);

                }

                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;

            }
            else
            {
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;

                // 取得明細の自社品番をロック(編集不可)に設定
                foreach (var row in gcSpreadGrid.Rows)
                    row.Cells[GridColumnsMapping.自社品番.GetHashCode()].Locked = true;

                this.gcSpreadGrid.Focus();
                this.gcSpreadGrid.ActiveCellPosition = new CellPosition(0, GridColumnsMapping.自社品番.GetHashCode());

            }

        }

        /// <summary>
        /// 揚り情報の登録処理をおこなう
        /// </summary>
        private void Update()
        {
            // 全項目エラーチェック
            if (!base.CheckAllValidation())
            {
                return;
            }

            // -- 送信用データを作成 --
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
                        "",
                        ccfg.ユーザID
                    }));

        }

        #endregion

        #region << SpreadGridイベント処理群 >>

        /// <summary>
        /// SPREAD セル編集がコミットされた時の処理(手入力) CellEditEnadedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcSpreadGrid_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
        {
            GcSpreadGrid grid = sender as GcSpreadGrid;
            string targetColumn = grid.ActiveCellPosition.ColumnName;

            if (e.EditAction == SpreadEditAction.Cancel)
                return;

            int rIdx = gcSpreadGrid.ActiveRowIndex;

            switch (targetColumn)
            {
                case "自社品番":
                    var 自社品番 = getSpreadGridValue(rIdx, GridColumnsMapping.自社品番);
                    var 自社色 = getSpreadGridValue(rIdx, GridColumnsMapping.自社色);

                    if (自社品番 == null || 自社色 == null)
                        return;

                    // 品番未入力時には処理しない
                    if (string.IsNullOrEmpty(自社品番.ToString()))
                    {
                        // 行データをクリア
                        ClearRowItems(rIdx);
                        return;
                    }

                    // 自社品番からデータを参照し、取得内容をグリッドに設定
                    base.SendRequest(
                        new CommunicationObject(
                            MessageType.RequestData,
                            MasterCode_MyProduct,
                            new object[] {
                                自社品番.ToString(),
                                自社色.ToString()
                            }));
                    break;

                case "数量":
                    // 在庫数不足チェック

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
        private void gcSpreadGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Delete押下時の処理
            // REMARKS:編集状態でない場合のDeleteキーは無視する
            if (e.Key == Key.Delete)
            {
                int rIdx = gcSpreadGrid.ActiveRowIndex;
                int cIdx = gcSpreadGrid.ActiveColumnIndex;

                if (!gcSpreadGrid.Cells[rIdx, cIdx].IsEditing)
                    e.Handled = true;

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
            gcSpreadGrid.Cells[rIdx, column.GetHashCode()].Value = value;

        }

        #endregion

    }

}
