using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;
using GrapeCity.Windows.SpreadGrid;

using System.Windows.Controls;
using KyoeiSystem.Framework.Windows.Controls;


namespace KyoeiSystem.Application.Windows.Views
{
    using WinForms = System.Windows.Forms;

    /// <summary>
    /// 取引先マスタ一括修正
    /// </summary>
    public partial class MST01011 : RibbonWindowViewBase
    {
        #region << 列挙型定義 >>

        /// <summary>
        /// データグリッドの列定義
        /// </summary>
        private enum GridColumnsMapping : int
        {
            取引先コード = 0,
            枝番 = 1,
            正式名称 = 2,
            請求担当者コード = 4,
            請求担当者 = 5,
            支払担当者コード = 7,
            支払担当者 = 8,
            
        }

        

        #endregion

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
        public class ConfigMST01011 : FormConfigBase
        {
            public byte[] spConfig20180118 = null;

        }

        /// ※ 必ず public で定義する。
        public ConfigMST01011 frmcfg = null;

        public byte[] sp_Config = null;

        #endregion

        #region << 定数定義 >>

        private const string MST01011_GetDataList = "MST01011_GetData";
        private const string MST01011_Update = "MST01011_Update";

        #endregion

        #region << 変数定義 >>

        private Dictionary<string, string> paramDic = new Dictionary<string, string>();

        #endregion

        #region バインディングプロパティ

        public DataTable _SearchResult;
        public DataTable SearchResult
        {
            get { return this._SearchResult; }
            set { this._SearchResult = value; NotifyPropertyChanged(); }

        }

        private string _正式名称 = string.Empty;
        public string 正式名称
        {
            get { return this._正式名称; }
            set { this._正式名称 = value; NotifyPropertyChanged(); }
        }

        private string _担当会社コード = string.Empty;
        public string 担当会社コード
        {
            get { return this._担当会社コード; }
            set { this._担当会社コード = value; NotifyPropertyChanged(); }
        }

        private string _取引区分 = string.Empty;
        public string 取引区分
        {
            get { return this._取引区分; }
            set { this._取引区分 = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region<< 仕入データ問合せ 初期処理群 >>

        /// <summary>
        /// 仕入データ問合せ コンストラクタ
        /// </summary>
        public MST01011()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// 画面読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 初期状態を保存（SPREADリセット時にのみ使用する）
            this.sp_Config = AppCommon.SaveSpConfig(this.spGridList);

            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            frmcfg = (ConfigMST01011)ucfg.GetConfigValue(typeof(ConfigMST01011));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST01011();
                ucfg.SetConfigValue(frmcfg);
                frmcfg.spConfig20180118 = this.sp_Config;
            }
            else
            {
                // 表示できるかチェック
                var WidthCHK = WinForms.Screen.PrimaryScreen.Bounds.Width - frmcfg.Left;
                if (WidthCHK > 10)
                {
                    this.Left = frmcfg.Left;
                }
                // 表示できるかチェック
                var HeightCHK = WinForms.Screen.PrimaryScreen.Bounds.Height - frmcfg.Top;
                if (HeightCHK > 10)
                {
                    this.Top = frmcfg.Top;
                }
                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;
            }
            #endregion

            spGridList.InputBindings.Add(new KeyBinding(spGridList.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));

            ScreenClear();
            // コントロールの初期設定をおこなう
            //initSearchControl();

            spGridList.RowCount = 0;

            SetFocusToTopControl();
            ErrorMessage = "";

        }

        #endregion

        #region << 送信データ受信 >>

        /// <summary>
        /// 取得データの取り込み
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            var data = message.GetResultData();
            DataTable tbl = (data is DataTable) ? (data as DataTable) : null;

            switch (message.GetMessageName())
            {
                case MST01011_GetDataList:
                    base.SetFreeForInput();
                    if (tbl.Rows.Count > 0)
                    {
                        SearchResult = tbl;
                        DataSet ds = new DataSet();
                        ds.Tables.Add(SearchResult);
                        SetData(tbl);
                    }

                    break;

                case MST01011_Update:
                    if ((int)data == 1)
                    {
                        MessageBox.Show("更新完了しました。");
                    }

                    ScreenClear();
                    break;

            }

        }

        /// <summary>
        /// 受信エラー時の処理をおこなう
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            this.Message = base.ErrorMessage;
            base.SetFreeForInput();
        }

        #endregion

        #region 画面項目の初期化
        /// <summary>
        /// 画面の初期化処理をおこなう
        /// </summary>
        private void ScreenClear()
        {
            this.MaintenanceMode = null;

            this.spGridList.ItemsSource = null;
            this.spGridList.RowCount = 0;

            if (SearchResult != null)
                SearchResult.Clear();


            正式名称 = string.Empty;
            担当会社コード = string.Empty;
            取引区分 = "0";


            ResetAllValidation();

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
                var ctl = FocusManager.GetFocusedElement(this);
                var spgrid = ViewBaseCommon.FindVisualParent<GcSpreadGrid>(ctl as Control);
                var m01Text = ViewBaseCommon.FindVisualParent<M01_TOK_TextBox>(ctl as UIElement);

                if (spgrid != null)
                {
                    int cIdx = spgrid.ActiveColumnIndex;
                    int rIdx = spgrid.ActiveRowIndex;

                    if (spgrid.ActiveColumnIndex == GridColumnsMapping.支払担当者コード.GetHashCode() || spgrid.ActiveColumnIndex == GridColumnsMapping.請求担当者コード.GetHashCode())
                    {
                        SCHM72_TNT TNT = new SCHM72_TNT();
                        TNT.TwinTextBox = new UcLabelTwinTextBox();
                        if (TNT.ShowDialog(this) == true)
                        {
                            spgrid.Cells[rIdx, cIdx].Value = TNT.TwinTextBox.Text1;

                            //更新用DataTableに反映
                            string targetColumn = spgrid.ActiveCellPosition.ColumnName;
                            SearchResult.Rows[rIdx][targetColumn] = spgrid.Cells[rIdx, cIdx].Value;
                           
                        }
                    }

 
                }
                else if (m01Text == null)
                {
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                }
                else
                {
                    m01Text.OpenSearchWindow(this);

                }

            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";

            }

        }
        #endregion

        #region F09 登録
        /// <summary>
        /// F9　リボン　登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            try
            {

                if (SearchResult == null)
                    return;

                base.SendRequest(
                    new CommunicationObject(MessageType.UpdateData, MST01011_Update, new object[]{
                        SearchResult.DataSet,
                        ccfg.ユーザID,
                    }));

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
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
            var yesno = MessageBox.Show("入力を取り消しますか？", "取消確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (yesno == MessageBoxResult.No)
                return;

            ScreenClear();

        }
        #endregion

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

       
        #region 一覧検索処理

        /// <summary>
        /// 検索ボタンが押下された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!base.CheckAllValidation())
                {
                    this.ErrorMessage = "入力内容に誤りがあります。";
                    MessageBox.Show("入力内容に誤りがあります。");
                    return;
                }

                base.SendRequest(
                    new CommunicationObject(MessageType.RequestData, MST01011_GetDataList, new object[]
                        {
                            正式名称
                            ,担当会社コード
                            ,取引区分
                        }));

                base.SetBusyForInput();

            }
            catch
            {
                throw;
            }

        }

        #endregion

        #region 取得データをセット
        private void SetData(DataTable tbl)
        {

            int iSpdRowIndex = 0;

            spGridList.InputBindings.Clear();
            spGridList.RowCount = iSpdRowIndex;

            for (int row = 0; row <= tbl.Rows.Count - 1; row++)
            {

                spGridList.Rows.AddNew();

                //取引先コード
                spGridList[iSpdRowIndex, GridColumnsMapping.取引先コード.GetHashCode()].Value = tbl.Rows[row]["取引先コード"].ToString();
                //枝番
                spGridList[iSpdRowIndex, GridColumnsMapping.枝番.GetHashCode()].Value = tbl.Rows[row]["枝番"].ToString();
                //正式名称
                spGridList[iSpdRowIndex, GridColumnsMapping.正式名称.GetHashCode()].Value = tbl.Rows[row]["正式名称"].ToString();
                //請求担当者コード
                spGridList[iSpdRowIndex, GridColumnsMapping.請求担当者コード.GetHashCode()].Value = tbl.Rows[row]["請求担当者コード"].ToString();
                //請求担当者名
                spGridList[iSpdRowIndex, GridColumnsMapping.請求担当者.GetHashCode()].Value = tbl.Rows[row]["請求担当者名"].ToString();
                //支払担当者コード
                spGridList[iSpdRowIndex, GridColumnsMapping.支払担当者コード.GetHashCode()].Value = tbl.Rows[row]["支払担当者コード"].ToString();
                //支払担当者名
                spGridList[iSpdRowIndex, GridColumnsMapping.支払担当者.GetHashCode()].Value = tbl.Rows[row]["支払担当者名"].ToString();

                //スプレッド行インデックスインクリメント
                iSpdRowIndex = iSpdRowIndex + 1;

            }

        }
        #endregion

        #region << KeyDown Events >>

        /// <summary>
        /// コントロールでキーが押下された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastField_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var ctl = sender as Framework.Windows.Controls.UcLabelTwinTextBox;
                if (ctl == null)
                {
                    return;
                }
                e.Handled = true;
                bool chk = ctl.CheckValidation();
                if (chk == true)
                {
                    Keyboard.Focus(this.btnSearch);
                }
                else
                {
                    ctl.Focus();
                    this.ErrorMessage = ctl.GetValidationMessage();
                }
            }
        }

        /// <summary>
        /// スプレッドグリッドでキーが押下された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spGridList_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && spGridList.EditElement == null)
            {
                e.Handled = true;
            }
            else if (e.Key == Key.V && (((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) != KeyStates.Down) || ((Keyboard.GetKeyStates(Key.RightCtrl) & KeyStates.Down) != KeyStates.Down)))
            {
                e.Handled = true;
            }

        }

        #endregion

        #region Window_Closed

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            this.spGridList.InputBindings.Clear();
            this.SearchResult = null;

            if (ucfg != null)
            {
                if (frmcfg == null) { frmcfg = new ConfigMST01011(); }
                frmcfg.Top = this.Top;
                frmcfg.Left = this.Left;
                frmcfg.Width = this.Width;
                frmcfg.Height = this.Height;
                frmcfg.spConfig20180118 = AppCommon.SaveSpConfig(this.spGridList);

                ucfg.SetConfigValue(frmcfg);
            }

        }

        #endregion

        /// <summary>
        /// spreadセル編集完了時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchGrid_CellEditEnded(object sender, GrapeCity.Windows.SpreadGrid.SpreadCellEditEndedEventArgs e)
        {
            GcSpreadGrid grid = sender as GcSpreadGrid;
            string targetColumn = grid.ActiveCellPosition.ColumnName;

            //明細行が存在しない場合は処理しない
            if (SearchResult == null) return;

            Row targetRow = grid.Rows[grid.ActiveRowIndex];

            //編集したセルの値を取得
            var CellValue = grid[grid.ActiveRowIndex, targetColumn].Value;

            if (CellValue != null)
            {
                SearchResult.Rows[targetRow.Index][targetColumn] = CellValue;
            }
            
        }

        


        

    }

}
