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
    /// 揚り依頼入力
    /// </summary>
    public partial class DLY07010 : RibbonWindowViewBase
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
        public class ConfigDLY07010 : FormConfigBase
        {
		}

        /// ※ 必ず public で定義する。
        public ConfigDLY07010 frmcfg = null;

        #endregion

		#region 定数定義

        /// <summary>揚り依頼情報取得</summary>
        private const string DLY07010_GetData = "DLY07010_GetData";
        /// <summary>揚り依頼情報更新</summary>
        private const string DLY07010_Update = "DLY07010_Update";

        /// <summary>取引先名称取得</summary>
        private const string MasterCode_Supplier = "UcSupplier";
        /// <summary>自社品番情報取得</summary>
        private const string MasterCode_MyProductSet = "UcMyProductSet";

        #endregion

        #region 列挙型定義

        /// <summary>
        /// データグリッドの列定義
        /// </summary>
        private enum GridColumnsMapping : int
        {
            依頼日 = 0,
            取引先コード = 1,
            スプリッタ = 2,
            枝番 = 3,
            得意先名 = 4,
            自社品番 = 5,
            色コード = 6,
            自社品名 = 7,
            依頼数 = 8,
            仕上数 = 9,
            品番コード = 10
        }

        #endregion

        #region クラス変数定義

        /// <summary>初期処理フラグ</summary>
        /// <remarks>初期処理中はtrue</remarks>
        private bool isChange = true;

        #endregion

        #region バインディングデータ

        /// <summary>揚り依頼情報</summary>
        private DataTable _searchResult;
        public DataTable SearchResult
        {
            get { return _searchResult; }
            set
            {
                _searchResult = value;
                NotifyPropertyChanged();
            }
        }

        #endregion


        #region << 初期処理群 >>

        /// <summary>
        /// 揚り依頼入力　コンストラクタ
        /// </summary>
        public DLY07010()
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

            frmcfg = (ConfigDLY07010)ucfg.GetConfigValue(typeof(ConfigDLY07010));

            #endregion

            if (frmcfg == null)
            {
                frmcfg = new ConfigDLY07010();
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

            // 検索画面情報を設定
            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { typeof(MST01010), typeof(SCHM01_TOK) });
            base.MasterMaintenanceWindowList.Add("M09_MYHIN", new List<Type> { typeof(MST02010), typeof(SCHM09_MYHIN) });
            base.MasterMaintenanceWindowList.Add("DLY07010", new List<Type> { typeof(DLY02010), null });

            // 画面の初期化
            ScreenClear();

            // 検索実行
            ProcDataSearch();

            isChange = false;

            SetFocusToTopControl();

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
                    case DLY07010_GetData:
                        // 検索結果処理
                        DataTable dt = data as DataTable;
                        if (dt != null)
                        {
                            SetTblData(dt);
                            ChangeKeyItemChangeable(false);
                        }
                        else
                        {
                            MessageBox.Show("指定の伝票番号は登録されていません。", "伝票未登録", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        }
                        break;

                    case DLY07010_Update:
                        MessageBox.Show(AppConst.SUCCESS_UPDATE, "登録完了", MessageBoxButton.OK, MessageBoxImage.Information);
                        // データ再取得
                        ProcDataSearch();
                        break;

                    case MasterCode_Supplier:
                        // 依頼先手入力時
                        setSpreadGridValue(
                            gcSpreadGrid.ActiveRowIndex,
                            GridColumnsMapping.得意先名,
                            (tbl != null && tbl.Rows.Count > 0) ? tbl.Rows[0]["名称"].ToString() : string.Empty);

                        gcSpreadGrid.ActiveCellPosition = new CellPosition(gcSpreadGrid.ActiveRowIndex, (int)GridColumnsMapping.自社品番);
                        break;

                    case MasterCode_MyProductSet:
                        #region 自社品番手入力時
                        DataTable ctbl = data as DataTable;
                        int rIdx = gcSpreadGrid.ActiveRowIndex;
                        if (ctbl == null || ctbl.Rows.Count == 0)
                        {
                            // 対象データなしの場合
                            setSpreadGridValue(rIdx, GridColumnsMapping.依頼日, string.Empty);
                            setSpreadGridValue(rIdx, GridColumnsMapping.取引先コード, string.Empty);
                            setSpreadGridValue(rIdx, GridColumnsMapping.スプリッタ, "－");
                            setSpreadGridValue(rIdx, GridColumnsMapping.枝番, string.Empty);
                            setSpreadGridValue(rIdx, GridColumnsMapping.得意先名, string.Empty);
                            setSpreadGridValue(rIdx, GridColumnsMapping.自社品番, string.Empty);
                            setSpreadGridValue(rIdx, GridColumnsMapping.自社品名, string.Empty);
                            setSpreadGridValue(rIdx, GridColumnsMapping.依頼数, 0);
                            setSpreadGridValue(rIdx, GridColumnsMapping.仕上数, 0);
                            setSpreadGridValue(rIdx, GridColumnsMapping.品番コード, 0);
                            gcSpreadGrid.ActiveCellPosition = new CellPosition(rIdx, (int)GridColumnsMapping.自社品番);

                        }
                        else if (tbl.Rows.Count > 1)
                        {
                            // 自社品番が複数存在の場合
                            SCHM09_MYHIN myhin = new SCHM09_MYHIN();

                            myhin.chkItemClass_2.cIsEnabled = false;
                            myhin.chkItemClass_2.IsChecked = false;
                            myhin.chkItemClass_3.cIsEnabled = false;
                            myhin.chkItemClass_3.IsChecked = false;
                            myhin.chkItemClass_4.cIsEnabled = false;
                            myhin.chkItemClass_4.IsChecked = false;

                            myhin.TwinTextBox = new UcLabelTwinTextBox();
                            myhin.txtCode.Text = getSpreadGridValue(rIdx, GridColumnsMapping.自社品番).ToString();
                            myhin.txtCode.IsEnabled = false;
                            myhin.TwinTextBox.LinkItem = 2;

                            if (myhin.ShowDialog(this) == true)
                            {
                                setSpreadGridValue(rIdx, GridColumnsMapping.自社品番, myhin.SelectedRowData["自社品番"]);
                                setSpreadGridValue(rIdx, GridColumnsMapping.色コード, myhin.SelectedRowData["自社色"]);
                                setSpreadGridValue(rIdx, GridColumnsMapping.自社品名, myhin.SelectedRowData["自社品名"]);
                                setSpreadGridValue(rIdx, GridColumnsMapping.品番コード, myhin.SelectedRowData["品番コード"]);
                                gcSpreadGrid.ActiveCellPosition = new CellPosition(rIdx, (int)GridColumnsMapping.依頼数);
                            }

                        }
                        else
                        {
                            // 対象データありの場合
                            DataRow drow = ctbl.Rows[0];
                            gcSpreadGrid.BeginEdit();

                            // セット商品の場合に設定
                            if (drow["商品形態分類"].ToString().Equals("1"))
                            {
                                setSpreadGridValue(rIdx, GridColumnsMapping.色コード, drow["自社色"]);
                                setSpreadGridValue(rIdx, GridColumnsMapping.自社品名, drow["自社品名"]);
                                setSpreadGridValue(rIdx, GridColumnsMapping.品番コード, drow["品番コード"]);
                                gcSpreadGrid.ActiveCellPosition = new CellPosition(rIdx, (int)GridColumnsMapping.依頼数);
                            }
                            else
                            {
                                //setSpreadGridValue(rIdx, GridColumnsMapping.自社品番, string.Empty);
                                setSpreadGridValue(rIdx, GridColumnsMapping.色コード, string.Empty);
                                setSpreadGridValue(rIdx, GridColumnsMapping.自社品名, string.Empty);
                                setSpreadGridValue(rIdx, GridColumnsMapping.品番コード, string.Empty);
                                gcSpreadGrid.ActiveCellPosition = new CellPosition(rIdx, (int)GridColumnsMapping.自社品番);
                            }

                            gcSpreadGrid.CommitCellEdit();

                        }

                        SearchResult.Rows[rIdx].EndEdit();

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
                var m01Text = ViewBaseCommon.FindVisualParent<M01_TOK_TextBox>(elmnt as Control);

                if (spgrid != null)
                {
                    int cIdx = spgrid.ActiveColumnIndex;
                    int rIdx = spgrid.ActiveRowIndex;

                    // 対象セルがロックされている場合は処理しない
                    if (spgrid.Cells[rIdx, cIdx].Locked == true)
                        return;

                    #region グリッドファンクションイベント
                    switch (spgrid.ActiveColumnIndex)
                    {
                        case (int)GridColumnsMapping.取引先コード:
                        case (int)GridColumnsMapping.枝番:
                            // 入力値を取得
                            string code = getSpreadGridValue(rIdx, GridColumnsMapping.取引先コード).ToString(),
                                eda = getSpreadGridValue(rIdx, GridColumnsMapping.枝番).ToString();

                            SCHM01_TOK tok = new SCHM01_TOK();
                            tok.TwinTextBox = new UcLabelTwinTextBox();
                            tok.TwinTextBox.Text1 = code;
                            tok.TwinTextBox.Text2 = eda;
                            tok.TwinTextBox.LinkItem = "2,3";

                            if (tok.ShowDialog(this) == true)
                            {
                                setSpreadGridValue(rIdx, GridColumnsMapping.取引先コード, tok.TwinTextBox.Text1);
                                setSpreadGridValue(rIdx, GridColumnsMapping.スプリッタ, '－');
                                setSpreadGridValue(rIdx, GridColumnsMapping.枝番, tok.TwinTextBox.Text2);
                                setSpreadGridValue(rIdx, GridColumnsMapping.得意先名, tok.TwinTextBox.Text3);
                                gcSpreadGrid.ActiveCellPosition = new CellPosition(rIdx, (int)GridColumnsMapping.自社品番);

                            }
                            break;

                        case (int)GridColumnsMapping.自社品番:
                            SCHM09_MYHIN myhin = new SCHM09_MYHIN();
                            myhin.chkItemClass_2.IsChecked = false;myhin.chkItemClass_2.cIsEnabled = false;
                            myhin.chkItemClass_3.IsChecked = false;myhin.chkItemClass_3.cIsEnabled = false;
                            myhin.chkItemClass_4.IsChecked = false;myhin.chkItemClass_4.cIsEnabled = false;
                            myhin.TwinTextBox = new UcLabelTwinTextBox();
                            myhin.TwinTextBox.LinkItem = "1";
                            if (myhin.ShowDialog(this) == true)
                            {
                                setSpreadGridValue(rIdx, GridColumnsMapping.品番コード, myhin.SelectedRowData["品番コード"]);
                                setSpreadGridValue(rIdx, GridColumnsMapping.自社品番, myhin.SelectedRowData["自社品番"]);
                                setSpreadGridValue(rIdx, GridColumnsMapping.自社品名, myhin.SelectedRowData["自社品名"]);
                                gcSpreadGrid.ActiveCellPosition = new CellPosition(rIdx, (int)GridColumnsMapping.依頼数);

                            }

                            break;

                        default:
                            break;

                    }

                    SearchResult.Rows[rIdx].EndEdit();

                    #endregion

                }
                else if (m01Text != null)
                {
                    m01Text.OpenSearchWindow(this);

                }
                else
                {
                    //ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);
                    SCHM09_MYHIN myhin = new SCHM09_MYHIN(true);
                    myhin.TwinTextBox = new UcLabelTwinTextBox();
                    myhin.TwinTextBox.Text1 = ProductCode.Text1;
                    myhin.TwinTextBox.LinkItem = ProductCode.LinkItem;

                    if (myhin.ShowDialog(this) == true)
                    {
                        ProductCode.Text1 = myhin.TwinTextBox.Text1;
                        ProductCode.Text2 = myhin.TwinTextBox.Text2;
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
                var m01Text = ViewBaseCommon.FindVisualParent<M01_TOK_TextBox>(elmnt as Control);

                if (spgrid != null)
                {
                    #region スプレッド内のイベント処理

                    switch (spgrid.ActiveColumnIndex)
                    {
                        case (int)GridColumnsMapping.取引先コード:
                        case (int)GridColumnsMapping.枝番:
                            MST01010 M01Form = new MST01010();
                            M01Form.Show(this);
                            break;

                        case (int)GridColumnsMapping.自社品番:
                            MST02010 M09Form = new MST02010();
                            M09Form.Show(this);

                            break;

                        default:
                            break;

                    }

                    #endregion

                }
                else if (m01Text != null)
                {
                    MST01010 m01Form = new MST01010();
                    m01Form.Show(this);

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

        #region F3 揚り入力
        /// <summary>
        /// F3　リボン　揚り入力
        /// </summary>
        /// <param name="e"></param>
        public override void OnF3Key(object sender, KeyEventArgs e)
        {
            // 揚り入力画面を表示する
            DLY02010 agrForm = new DLY02010();
            agrForm.ShowDialog(this);

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

            int delRowCount = (SearchResult.GetChanges(DataRowState.Deleted) == null) ? 0 : SearchResult.GetChanges(DataRowState.Deleted).Rows.Count;

            DataRow dtlRow = SearchResult.NewRow();
            SearchResult.Rows.Add(dtlRow);

            // 行追加後は追加行を選択させる
            int newRowIdx = SearchResult.Rows.Count - delRowCount - 1;

            setSpreadGridValue(newRowIdx, GridColumnsMapping.スプリッタ, "－");

            gcSpreadGrid.ActiveCellPosition = new CellPosition(newRowIdx, (int)GridColumnsMapping.依頼日);
            gcSpreadGrid.ShowCell(newRowIdx, (int)GridColumnsMapping.依頼日, VerticalPosition.Bottom);

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

            if (MessageBox.Show(
                    AppConst.CONFIRM_DELETE_ROW,
                    "行削除確認",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.No) == MessageBoxResult.No)
                return;

            int targetRowIdx = this.gcSpreadGrid.ActiveRowIndex;
            try
            {
                gcSpreadGrid.Rows.Remove(targetRowIdx);
            }
            catch
            {
                SearchResult.Rows.RemoveAt(targetRowIdx);
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

            // 入力情報のクリア
            ScreenClear();

            // データ再取得
            ProcDataSearch();

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
                    if (isChange)
                    {
                        var yesno = MessageBox.Show("保存せずに終了してもよろしいですか？", "終了確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (yesno == MessageBoxResult.No)
                            return;
                    }
                }

                this.Close();

            }

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
            if (frmcfg == null) { frmcfg = new ConfigDLY07010(); }

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;

            ucfg.SetConfigValue(frmcfg);

        }

        #endregion

        #region << 検索データ設定・登録・削除処理 >>

        #region 検索実行
        /// <summary>
        /// 揚り依頼情報の検索をおこなう
        /// </summary>
        private void ProcDataSearch()
        {
            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    DLY07010_GetData,
                    new object[] {
                        this.ProductCode.Text1,
                        this.Subcontractor.Text1,
                        this.Subcontractor.Text2
                    }));

        }
        #endregion

        #region 検索結果処理
        /// <summary>
        /// 取得内容を各コントロールに設定
        /// </summary>
        /// <param name="ds"></param>
        private void SetTblData(DataTable dt)
        {
            // 揚り依頼情報設定
            SearchResult = dt;
            SearchResult.AcceptChanges();

            // データ状態から編集状態を設定
            if (SearchResult.Select("品番コード > 0").Count() == 0)
            {
                // 新規行を追加
                for (int i = 0; i < 10; i++)
                {
                    DataRow row = SearchResult.NewRow();
                    SearchResult.Rows.Add(row);

                }

                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;

            }
            else
            {
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;

                // 取得明細の自社品番をロック(編集不可)に設定
                foreach (var row in gcSpreadGrid.Rows)
                {
                    row.Cells[GridColumnsMapping.依頼日.GetHashCode()].Locked = true;
                    row.Cells[GridColumnsMapping.取引先コード.GetHashCode()].Locked = true;
                    row.Cells[GridColumnsMapping.枝番.GetHashCode()].Locked = true;
                    row.Cells[GridColumnsMapping.自社品番.GetHashCode()].Locked = true;
                }

            }

            // 行に対してハイフンを設定
            foreach(var row in gcSpreadGrid.Rows)
                setSpreadGridValue(row.Index, GridColumnsMapping.スプリッタ, "－");

            // フォーカスセット
            this.gcSpreadGrid.Focus();
            this.gcSpreadGrid.ActiveCellPosition = new CellPosition(0, (int)GridColumnsMapping.依頼日);

        }
        #endregion

        #region リスト登録処理
        /// <summary>
        /// 揚り依頼情報の登録処理をおこなう
        /// </summary>
        private void Update()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(_searchResult.Copy());

            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    DLY07010_Update,
                    new object[] {
                        ds,
                        ccfg.ユーザID
                    }));

        }
        #endregion

        #endregion

        #region << コントロールイベント >>

        #region テキストロストフォーカス
        /// <summary>
        /// テキストからフォーカスが移動した場合のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TwinTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //UcLabelTwinTextBox tbox = sender as UcLabelTwinTextBox;
            M01_TOK_TextBox m01tbox = sender as M01_TOK_TextBox;

            //if (tbox != null && string.IsNullOrEmpty(tbox.Text1))
            //    return;

            if (m01tbox != null && !string.IsNullOrEmpty(m01tbox.Text1) && string.IsNullOrEmpty(m01tbox.Text2))
                // テキスト１入力済・テキスト２未入力の場合は検索をおこなわない
                return;

            if (m01tbox != null && string.IsNullOrEmpty(m01tbox.Text1) && !string.IsNullOrEmpty(m01tbox.Text2))
                // テキスト１未入力・テキスト２入力済の場合は検索をおこなわない
                return;

            // 再検索実行
            ProcDataSearch();

        }
        #endregion


        #endregion

        #region << 機能処理群 >>

        #region 画面項目の初期化
        /// <summary>
        /// 画面の初期化処理をおこなう
        /// </summary>
        private void ScreenClear()
        {
            this.MaintenanceMode = null;

            if (SearchResult != null)
                SearchResult.Clear();

            this.ProductCode.Text1 = string.Empty;
            this.Subcontractor.Text1 = string.Empty;

            ChangeKeyItemChangeable(true);
            ResetAllValidation();

        }
        #endregion

        #region 入力検証処理
        /// <summary>
        /// 入力内容の検証をおこなう
        /// </summary>
        /// <returns></returns>
        private bool isFormValidation()
        {
            bool isDetailErr = false;

            // 【明細】詳細データが１件もない場合はエラー
            if (SearchResult == null || SearchResult.Rows.Count == 0)
            {
                base.ErrorMessage = string.Format("揚り依頼情報が１件もありません。");
                this.gcSpreadGrid.Focus();
                return isDetailErr;
            }

            foreach (var row in gcSpreadGrid.Rows)
            {
                object productCode = row.Cells[(int)GridColumnsMapping.品番コード].Value;
                // 追加行未入力レコードはスキップ
                if (productCode == null || string.IsNullOrEmpty(productCode.ToString()) || productCode.ToString().Equals("0"))
                    continue;

                // エラー情報をクリア
                row.ValidationErrors.Clear();

                // 依頼日
                var reqDate = row.Cells[(int)GridColumnsMapping.依頼日].Value;
                if (reqDate == null || string.IsNullOrEmpty(reqDate.ToString()))
                {
                    int targetColumnIdx = (int)GridColumnsMapping.依頼日;
                    row.ValidationErrors.Add(
                        new SpreadValidationError("依頼日が入力されていません。", null, row.Index, targetColumnIdx));

                    if (!isDetailErr)
                        gcSpreadGrid.ActiveCellPosition = new CellPosition(row.Index, targetColumnIdx);

                    isDetailErr = true;

                }

                // 依頼先
                var reqName = row.Cells[(int)GridColumnsMapping.得意先名].Value;
                if (reqName == null || string.IsNullOrEmpty(reqName.ToString()))
                {
                    int targetColumnIdx = (int)GridColumnsMapping.得意先名;
                    row.ValidationErrors.Add(
                        new SpreadValidationError("得意先の入力に誤りがあります。", null, row.Index, targetColumnIdx));

                    if (!isDetailErr)
                        gcSpreadGrid.ActiveCellPosition = new CellPosition(row.Index, targetColumnIdx);

                    isDetailErr = true;

                }

                // 品番
                var itemName = row.Cells[(int)GridColumnsMapping.自社品名].Value;
                if (itemName == null || string.IsNullOrEmpty(itemName.ToString()))
                {
                    int targetColumnIdx = (int)GridColumnsMapping.自社品名;
                    row.ValidationErrors.Add(
                        new SpreadValidationError("商品の入力に誤りがあります。", null, row.Index, targetColumnIdx));

                    if (!isDetailErr)
                        gcSpreadGrid.ActiveCellPosition = new CellPosition(row.Index, targetColumnIdx);

                    isDetailErr = true;

                }

                // 依頼数
                var reqQty = row.Cells[(int)GridColumnsMapping.依頼数].Value;
                if (reqQty == null || string.IsNullOrEmpty(reqQty.ToString()))
                {
                    int targetColumnIdx = (int)GridColumnsMapping.依頼数;
                    row.ValidationErrors.Add(
                        new SpreadValidationError("依頼数が入力されていません。", null, row.Index, targetColumnIdx));

                    if (!isDetailErr)
                        gcSpreadGrid.ActiveCellPosition = new CellPosition(row.Index, targetColumnIdx);

                    isDetailErr = true;

                }

                // 仕上数
                var upQty = row.Cells[(int)GridColumnsMapping.仕上数].Value;
                if (upQty == null || string.IsNullOrEmpty(upQty.ToString()))
                {
                    int targetColumnIdx = (int)GridColumnsMapping.仕上数;
                    row.ValidationErrors.Add(
                        new SpreadValidationError("仕上数が入力されていません。", null, row.Index, targetColumnIdx));

                    if (!isDetailErr)
                        gcSpreadGrid.ActiveCellPosition = new CellPosition(row.Index, targetColumnIdx);

                    isDetailErr = true;

                }

                if (!isDetailErr)
                {
                    // 依頼数－仕上数 数量チェック
                    if (AppCommon.IntParse(reqQty.ToString()) < AppCommon.IntParse(upQty.ToString()))
                    {
                        int targetColumnIdx = (int)GridColumnsMapping.仕上数;
                        row.ValidationErrors.Add(
                            new SpreadValidationError("仕上数が依頼数を超えています。", null, row.Index, targetColumnIdx));

                        if (!isDetailErr)
                            gcSpreadGrid.ActiveCellPosition = new CellPosition(row.Index, targetColumnIdx);

                        isDetailErr = true;

                    }

                }

            }

            return !isDetailErr;

        }
        #endregion


        #endregion

        #region << SpreadGridイベント処理群 >>

        #region セル編集終了時イベント
        /// <summary>
        /// SPREAD セル編集がコミットされた時の処理(手入力) CellEditEnadedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcSpredGrid_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
        {
            GcSpreadGrid grid = sender as GcSpreadGrid;
            int targetColumnIdx = grid.ActiveColumnIndex;
            int targetRowIndex = grid.ActiveRowIndex;

            if (e.EditAction == SpreadEditAction.Cancel)
                return;

            switch (targetColumnIdx)
            {
                case (int)GridColumnsMapping.取引先コード:
                case (int)GridColumnsMapping.枝番:
                    string code = getSpreadGridValue(targetRowIndex, GridColumnsMapping.取引先コード).ToString();
                    string eda = getSpreadGridValue(targetRowIndex, GridColumnsMapping.枝番).ToString();

                    if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(eda))
                    {
                        setSpreadGridValue(targetRowIndex, GridColumnsMapping.得意先名, string.Empty);
                        return;
                    }

                    // 入力内容から取引先マスタを参照
                    base.SendRequest(
                        new CommunicationObject(
                            MessageType.RequestData,
                            MasterCode_Supplier,
                            new object[] {
                        this.Subcontractor.DataAccessName,
                        code,
                        eda,
                        this.Subcontractor.LinkItem
                    }));
                    break;

                case (int)GridColumnsMapping.自社品番:
                case (int)GridColumnsMapping.色コード:
                    string productCode = getSpreadGridValue(targetRowIndex, GridColumnsMapping.自社品番).ToString();

                    if (string.IsNullOrEmpty(productCode))
                        return;

                    // 自社品番から品番マスタを参照
                    base.SendRequest(
                        new CommunicationObject(
                            MessageType.RequestData,
                            MasterCode_MyProductSet,
                            new object[] {
                                productCode
                            }));
                    break;

                case (int)GridColumnsMapping.依頼日:
                case (int)GridColumnsMapping.依頼数:
                case (int)GridColumnsMapping.仕上数:
                    DataRow row = SearchResult.Rows[targetRowIndex];
                    if (row.RowState == DataRowState.Unchanged)
                        if (row.HasVersion(DataRowVersion.Original))
                        {
                            // Remarks:テーブル列とグリッド列の並びが違うので加算して合わせる
                            var org = row[targetColumnIdx + 1, DataRowVersion.Original];
                            var val = row[targetColumnIdx + 1];

                            if (org.ToString() != val.ToString())
                            {
                                row.SetModified();
                                row[targetColumnIdx + 1] = val;
                            }

                        }

                    break;

                default:
                    break;

            }

        }
        #endregion

        #region キーが押された時のイベント処理
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
        #endregion

        #region セル内容取得・設定
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

            if (gcSpreadGrid.Cells[rIdx, (int)column].Value == null)
                return string.Empty;

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

        #region 行コレクション変更時のイベント処理
        /// <summary>
        /// 行コレクションに変更があった時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcSpreadGrid_RowCollectionChanged(object sender, SpreadCollectionChangedEventArgs e)
        {
            //var spGrid = sender as GcSpreadGrid;
            //Brush enableBrush = new SolidColorBrush(Color.FromRgb(255,248,220));

            //if (isFirst || spGrid.Rows.Count == 0)
            //    return;

            //var row = spGrid.Rows[e.NewStartingIndex];

            //row.Cells[(int)GridColumnsMapping.依頼日].Background = enableBrush;
            //row.Cells[(int)GridColumnsMapping.取引先コード].Background = enableBrush;
            //row.Cells[(int)GridColumnsMapping.枝番].Background = enableBrush;
            //row.Cells[(int)GridColumnsMapping.自社品番].Background = enableBrush;

            //row.Cells[(int)GridColumnsMapping.依頼数].Background = enableBrush;
            //row.Cells[(int)GridColumnsMapping.仕上数].Background = enableBrush;

        }
        #endregion

        #endregion

    }

}
