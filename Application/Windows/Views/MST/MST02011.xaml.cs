using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using KyoeiSystem.Framework.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using GrapeCity.Windows.SpreadGrid;


namespace KyoeiSystem.Application.Windows.Views
{
    using FwRepPreview = KyoeiSystem.Framework.Reports.Preview;
    using WinForms = System.Windows.Forms;

    /// <summary>
    /// 品番マスタ一括修正
    /// </summary>
    public partial class MST02011 : RibbonWindowViewBase
    {
        #region << 列挙型定義 >>

        /// <summary>
        /// データグリッドの列定義
        /// </summary>
        private enum GridColumnsMapping : int
        {
            自社品番 = 0,
            色 = 1,
            自社品名 = 2,
            原価 = 3,
            加工原価 = 4,
            卸値 = 5,
            売価 = 6,
            掛率 = 7
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
        public class ConfigMST02011 : FormConfigBase
        {
            public byte[] spConfig20180118 = null;

        }

        /// ※ 必ず public で定義する。
        public ConfigMST02011 frmcfg = null;

        public byte[] sp_Config = null;

        #endregion

        #region << 定数定義 >>

        private const string MST02011_GetDataList = "MST02011_GetData";
        private const string MST02011_Update = "MST02011_Update";

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

        private string _自社品名 = string.Empty;
        public string 自社品名
        {
            get { return this._自社品名; }
            set { this._自社品名 = value; NotifyPropertyChanged(); }
        }

        private string _商品分類 = string.Empty;
        public string 商品分類
        {
            get { return this._商品分類; }
            set { this._商品分類 = value; NotifyPropertyChanged(); }
        }

        private string _商品形態 = string.Empty;
        public string 商品形態
        {
            get { return this._商品形態; }
            set { this._商品形態 = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region<< 仕入データ問合せ 初期処理群 >>

        /// <summary>
        /// 仕入データ問合せ コンストラクタ
        /// </summary>
        public MST02011()
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

            

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            frmcfg = (ConfigMST02011)ucfg.GetConfigValue(typeof(ConfigMST02011));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST02011();
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

            ScreenClear();

            spGridList.InputBindings.Add(new KeyBinding(spGridList.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));

            // コントロールの初期設定をおこなう
            initSearchControl();

            spGridList.RowCount = 0;

            SetFocusToTopControl();
            ErrorMessage = "";

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


            自社品名 = string.Empty;
            商品分類 = "1";
            商品形態 = "1";
            

            ResetAllValidation();

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
                case MST02011_GetDataList :
                    base.SetFreeForInput();
                    if (tbl.Rows.Count > 0)
                    {
                        SearchResult = tbl;
                        DataSet ds = new DataSet();
                        ds.Tables.Add(SearchResult);
                        SetData(tbl);
                    }

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

        #region << リボン >>

        #region F09 登録
        /// <summary>
        /// F9　リボン　登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            if (SearchResult == null)
                return;

            //DataSet ds = new DataSet();
            //DataTable dt = new DataTable();
            //dt = SearchResult;
            //ds.Tables.Add(dt);

            SearchResult.Rows[0]["原価"] = 200;

            base.SendRequest(
                new CommunicationObject(MessageType.UpdateData, MST02011_Update, new object[]{
                        SearchResult.DataSet,
                        ccfg.ユーザID,
                        自社品名,
                        商品分類,
                        商品形態
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

                //setSearchParams();

                base.SendRequest(
                    new CommunicationObject(MessageType.RequestData,MST02011_GetDataList,new object[]
                        {
                            自社品名
                            ,商品分類
                            ,商品形態
                        }));

                base.SetBusyForInput();

            }
            catch
            {
                throw;
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
                if (frmcfg == null) { frmcfg = new ConfigMST02011(); }
                frmcfg.Top = this.Top;
                frmcfg.Left = this.Left;
                frmcfg.Width = this.Width;
                frmcfg.Height = this.Height;
                frmcfg.spConfig20180118 = AppCommon.SaveSpConfig(this.spGridList);

                ucfg.SetConfigValue(frmcfg);
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

                //自社品番
                spGridList[iSpdRowIndex, "自社品番"].Value = tbl.Rows[row]["自社品番"].ToString();
                //色
                spGridList[iSpdRowIndex, "色"].Value = tbl.Rows[row]["色"].ToString();
                //自社品名
                spGridList[iSpdRowIndex, "自社品名"].Value = tbl.Rows[row]["自社品名"].ToString();
                //原価
                spGridList[iSpdRowIndex, "原価"].Value = tbl.Rows[row]["原価"].ToString();
                //加工原価
                spGridList[iSpdRowIndex, "加工原価"].Value = tbl.Rows[row]["加工原価"].ToString();
                //卸値
                spGridList[iSpdRowIndex, "卸値"].Value = tbl.Rows[row]["卸値"].ToString();
                //売価
                spGridList[iSpdRowIndex, "売価"].Value = tbl.Rows[row]["売価"].ToString();
                //掛率
                spGridList[iSpdRowIndex, "掛率"].Value = tbl.Rows[row]["掛率"].ToString();

                //スプレッド行インデックスインクリメント
                iSpdRowIndex = iSpdRowIndex + 1;

            }

        }
        #endregion


        #region << 機能処理群 >>



        #region 検索条件部の初期設定

        /// <summary>
        /// 検索条件部の初期設定をおこなう
        /// </summary>
        private void initSearchControl()
        {
            

        }

        #endregion

        #region 検索パラメータの設定
        /// <summary>
        /// 検索パラメータを設定する
        /// </summary>
        private void setSearchParams()
        {
            
        }
        #endregion

        /// <summary>
        /// セル編集コミット時処理
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
            var CellValue = grid[grid.ActiveRowIndex,targetColumn].Value;

            SearchResult.Rows[targetRow.Index][targetColumn] = CellValue;

            //SearchResult.Rows[targetRow.Index].EndEdit();
            //SearchResult.AcceptChanges();
        }

        

       

        #endregion

    }

}
