﻿using KyoeiSystem.Framework.Common;
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
            得意先名 = 2,
            請求担当者コード = 4,
            請求担当者 = 5,
            支払担当者コード = 7,
            支払担当者 = 8,
            消費税区分 = 9,
            税区分ID = 10,
            締日 = 11,
            サイト = 12,
            入金日 = 13,
            手形条件 = 14,
            手形区分 = 15,
            手形サイト = 16,
            手形入金日 = 17
            
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
        private const string MST01011_GetM72 = "MST01011_GetM72";


        #endregion

        #region << 変数定義 >>

        private Dictionary<string, string> paramDic = new Dictionary<string, string>();

        /// <summary>
        /// 対象行番号
        /// </summary>
        private int targetRowIdx;

        /// <summary>
        /// 対象列番号
        /// </summary>
        private int targetColIdx;

        #endregion

        #region バインディングプロパティ

        public DataTable _SearchResult;
        public DataTable SearchResult
        {
            get { return this._SearchResult; }
            set { this._SearchResult = value; NotifyPropertyChanged(); }

        }

        private string _得意先名 = string.Empty;
        public string 得意先名
        {
            get { return this._得意先名; }
            set { this._得意先名 = value; NotifyPropertyChanged(); }
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
            frmcfg =  (ConfigMST01011)ucfg.GetConfigValue(typeof(ConfigMST01011));
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
                    else
                    {
                        this.ErrorMessage = "対象データが存在しません。";
                        return;
                    }

                    break;

                case MST01011_Update:
                    if ((int)data == 1)
                    {
                        MessageBox.Show("更新完了しました。");
                    }

                    ScreenClear();
                    break;

                case MST01011_GetM72:
                    var ctl = FocusManager.GetFocusedElement(this);
                    var spgrid = ViewBaseCommon.FindVisualParent<GcSpreadGrid>(ctl as Control);
                    if (spgrid == null) return;

                    //担当者名を表示
                    if (data != null)
                    {
                        spgrid.Cells[targetRowIdx, targetColIdx + 1].Value = data.ToString();
                    }
                    else
                    {
                        spgrid.Cells[targetRowIdx, targetColIdx].Value = null;
                        spgrid.Cells[targetRowIdx, targetColIdx + 1].Value = string.Empty;
                    }

                    break;

            }

        }

        #region OnReveivedError

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


            得意先名 = string.Empty;
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
                            //担当者ID
                            spgrid.Cells[rIdx, cIdx].Value = TNT.TwinTextBox.Text1;
                            //担当者名
                            spgrid.Cells[rIdx, cIdx + 1].Value = TNT.TwinTextBox.Text2;

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
                spGridList.CommitCellEdit();
                if (SearchResult == null)
                    return;

                DataSet ds = new DataSet();
                ds.Tables.Add(SearchResult);

                base.SendRequest(
                    new CommunicationObject(MessageType.UpdateData, MST01011_Update, new object[]{
                        ds,
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
                            得意先名
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
                spGridList[iSpdRowIndex, GridColumnsMapping.得意先名.GetHashCode()].Value = tbl.Rows[row]["得意先略称名"].ToString();
                //請求担当者コード
                spGridList[iSpdRowIndex, GridColumnsMapping.請求担当者コード.GetHashCode()].Value = tbl.Rows[row]["請求担当者コード"].ToString();
                //請求担当者名
                spGridList[iSpdRowIndex, GridColumnsMapping.請求担当者.GetHashCode()].Value = tbl.Rows[row]["請求担当者名"].ToString();
                //支払担当者コード
                spGridList[iSpdRowIndex, GridColumnsMapping.支払担当者コード.GetHashCode()].Value = tbl.Rows[row]["支払担当者コード"].ToString();
                //支払担当者名
                spGridList[iSpdRowIndex, GridColumnsMapping.支払担当者.GetHashCode()].Value = tbl.Rows[row]["支払担当者名"].ToString();
                //請求消費税区分
                spGridList[iSpdRowIndex, GridColumnsMapping.消費税区分.GetHashCode()].Value = tbl.Rows[row]["請求消費税区分"].ToString();
                //請求税区分ID
                spGridList[iSpdRowIndex, GridColumnsMapping.税区分ID.GetHashCode()].Value = tbl.Rows[row]["請求税区分ID"].ToString();
                //請求締日
                spGridList[iSpdRowIndex, GridColumnsMapping.締日.GetHashCode()].Value = tbl.Rows[row]["請求締日"].ToString();
                //請求サイト
                spGridList[iSpdRowIndex, GridColumnsMapping.サイト.GetHashCode()].Value = tbl.Rows[row]["請求サイト"].ToString();
                //請求入金日
                spGridList[iSpdRowIndex, GridColumnsMapping.入金日.GetHashCode()].Value = tbl.Rows[row]["請求入金日"].ToString();
                //請求手形条件
                spGridList[iSpdRowIndex, GridColumnsMapping.手形条件.GetHashCode()].Value = tbl.Rows[row]["請求手形条件"].ToString();
                //請求手形区分
                spGridList[iSpdRowIndex, GridColumnsMapping.手形区分.GetHashCode()].Value = tbl.Rows[row]["請求手形区分"].ToString();
                //請求手形サイト
                spGridList[iSpdRowIndex, GridColumnsMapping.手形サイト.GetHashCode()].Value = tbl.Rows[row]["請求手形サイト"].ToString();
                //請求手形入金日
                spGridList[iSpdRowIndex, GridColumnsMapping.手形入金日.GetHashCode()].Value = tbl.Rows[row]["請求手形入金日"].ToString();
                
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

        #region spreadセル編集完了時処理
        /// <summary>
        /// spreadセル編集完了時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchGrid_CellEditEnded(object sender, GrapeCity.Windows.SpreadGrid.SpreadCellEditEndedEventArgs e)
        {
            GcSpreadGrid grid = sender as GcSpreadGrid;
            string targetColumn = grid.ActiveCellPosition.ColumnName;
            int? i担当者コード = null;

            //明細行が存在しない場合は処理しない
            if (SearchResult == null) return;

            Row targetRow = grid.Rows[grid.ActiveRowIndex];

            targetRowIdx = targetRow.Index;
            targetColIdx = grid.ActiveColumnIndex;

            //編集したセルの値を取得
            var CellValue = grid[grid.ActiveRowIndex, targetColumn].Value;



            //担当者コードが入力された際担当者名をDBから取得
            if (CellValue != null && CellValue.ToString().Length > 0)
            {
                i担当者コード = int.Parse(CellValue.ToString());
            }
            else
            {
                CellValue = DBNull.Value;
            }

            SearchResult.Rows[targetRow.Index][targetColumn] = CellValue;

            if (targetColumn == "請求担当者コード" || targetColumn == "支払担当者コード")
            {

                base.SendRequest(
                new CommunicationObject(MessageType.RequestData, MST01011_GetM72, new object[]
                        {

                           i担当者コード 

                        }));
            }


        }
        #endregion

        /// <summary>
        /// CSV出力ボタン押下時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CSVOutPut_Click(object sender, RoutedEventArgs e)
        {
            if (SearchResult == null)
            {
                ErrorMessage = "検索を行ってから出力ボタンを押下してください。";
                return;
            }
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
                CSVData.SaveCSV(SearchResult, sfd.FileName, true, true, false, ',');
                MessageBox.Show("CSVファイルの出力が完了しました。");
            }
        }

        /// <summary>
        /// CSVInputボタン押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CSVInPut_Click(object sender, RoutedEventArgs e)
        {
            string selectFile = string.Empty;

            try
            {
                //ファイル選択ダイアログ表示
                selectFile = SelectCsvFile();
                if (selectFile == String.Empty)
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }

            DataTable tbl = CSVData.ReadCsv(selectFile, ",", true, true, true);
            SearchResult = tbl;
            SearchResult.TableName = MST01011_GetDataList;
            SetData(tbl);
            MessageBox.Show("CSVファイルの内容を表に表示しました。");
        }

        /// <summary>
        /// ファイル選択ダイアログ表示(CSVファイル用)
        /// </summary>
        /// <param name="title">ダイアログタイトル</param>
        /// <param name="initFileName">初期表示ファイル名(フルパス)</param>
        /// <returns></returns>
        private string SelectCsvFile()
        {
            //OpenFileDialogクラスのインスタンスを作成
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();

            string folder = string.Empty, fileName = string.Empty;

            //はじめのファイル名を指定する
            ofd.FileName = fileName;

            //はじめに表示されるフォルダを指定する
            ofd.InitialDirectory = @folder;

            //[ファイルの種類]に表示される選択肢を指定する
            ofd.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";

            //[ファイルの種類]にはじめに表示するものを指定する
            ofd.FilterIndex = 1;

            //タイトルを設定する
            ofd.Title = "取込ファイルを選択してください。";

            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;

            //存在しないファイルの名前が指定されたとき警告を表示する
            ofd.CheckFileExists = true;

            //存在しないパスが指定されたとき警告を表示する
            ofd.CheckPathExists = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return ofd.FileName;
            else
                return string.Empty;
        }

    }

}
