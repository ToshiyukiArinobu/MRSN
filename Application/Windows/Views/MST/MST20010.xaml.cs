﻿using System;
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
using System.Data;

using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;


namespace KyoeiSystem.Application.Windows.Views
{
    using ConsolLog = System.Diagnostics.Debug;

    /// <summary>
    /// 得意先品番登録
    /// </summary>
    public partial class MST20010 : WindowMasterMainteBase
    {
        /// <summary>
        /// 遷移元画面
        /// </summary>
        public enum SEND_FORM : int
        {
            メニュー = 0,
            取引先マスタ = 1,
            品番マスタ = 2
        }

        #region 定数定義

        /// <summary>得意先ベースのデータ取得</summary>
        private const string SearchTableToCustomer = "M10_TOKHIN_GetData";
        /// <summary>自社品番・色ベースでのデータ取得</summary>
        private const string SearchTableToProduct = "M10_TOKHIN_GetData_Product";
        /// <summary>得意先売価情報登録</summary>
        private const string M10_TOKUHIN_Update = "M10_TOKHIN_Update";
        /// <summary>取引先名称取得</summary>
        private const string MasterCode_Customer = "UcSupplier";
        /// <summary>品番情報取得</summary>
        private const string MasterCode_Product = "UcProduct";

        /// <summary>この画面が対応する取引区分</summary>
        private const int TARGET_TRADING_KBN = 0;   // 得意先

        #endregion

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;
        #region "権限関係"
        CommonConfig ccfg = null;
        #endregion

        //<summary>
        //画面固有設定項目のクラス定義
        //※ 必ず public で定義する。
        //</summary>
        public class ConfigMST20010 : FormConfigBase
        {
            //public bool[] 表示順方向 { get; set; }
            /// コンボボックスの位置
            public int 集計区分_Combo { get; set; }
        }

        /// ※ 必ず public で定義する。
        public ConfigMST20010 frmcfg = null;

        #endregion

        #region 遷移元からのパラメータ受取り変数定義

        /// <summary>
        /// 遷移元画面ID
        /// </summary>
        /// <value>
        /// 0:メニューより呼出し
        /// 1:取引先マスタより呼出し
        /// 2:品番マスタより呼出し
        /// </value>
        public int SendFormId { get; set; }

        /// <summary>取引先コード</summary>
        public string CustomerCode { get; set; }
        /// <summary>取引先コード枝番</summary>
        public string CustomerEda { get; set; }
        /// <summary>品番コード</summary>
        public int ProductNumber { get; set; }
        /// <summary>(自社)品番</summary>
        public string ItemNumber { get; set; }
        /// <summary>色</summary>
        public string ColorCode { get; set; }

        #endregion

        #region バインド用変数

        /// <summary>
        /// グリッドバインディングデータテーブル
        /// </summary>
        public DataTable _SearchResult;
        public DataTable SearchResult
        {
            get { return _SearchResult; }
            set
            {
                this._SearchResult = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 行削除対象データテーブル
        /// </summary>
        public DataTable _DeletedItem;
        public DataTable DeletedItem
        {
            get { return _DeletedItem; }
            set
            {
                this._DeletedItem = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        private DataTable productInfoList;
        private bool isBindingEvents = false;

        #region コンストラクタ

        /// <summary>
        /// 得意先品番登録 コンストラクタ
        /// </summary>
        public MST20010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #endregion

        #region イベント処理群

        #region Load時

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            // 権限設定を呼び出す(ucfgを取得した後のに入れる)
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            // 登録ボタン設定
            if (!権限Get.Authority_Update_Button(ccfg, this.GetType().Name))
            {
                DataUpdateVisible = System.Windows.Visibility.Hidden;
            }
            frmcfg = (ConfigMST20010)ucfg.GetConfigValue(typeof(ConfigMST20010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST20010();
                ucfg.SetConfigValue(frmcfg);
            }
            else
            {
                // 表示できるかチェック
                var WidthCHK = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - frmcfg.Left;
                if (WidthCHK > 10)
                {
                    this.Left = frmcfg.Left;
                }
                // 表示できるかチェック
                var HeightCHK = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - frmcfg.Top;
                if (HeightCHK > 10)
                {
                    this.Top = frmcfg.Top;
                }
                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;
            }
            #endregion

            ScreenClear();

            #region 遷移元からのパラメータを設定
            if (!string.IsNullOrEmpty(CustomerCode))
                this.TOKUISAKI.Text1 = CustomerCode;

            if (!string.IsNullOrEmpty(CustomerEda))
                this.TOKUISAKI.Text2 = CustomerEda;

            if (!string.IsNullOrEmpty(ItemNumber))
                this.HINBAN.Text1 = ItemNumber;

            if (!string.IsNullOrEmpty(ColorCode))
                this.COLOR.Text1 = ColorCode;

            #endregion

            switch(SendFormId)
            {
                case (int)SEND_FORM.取引先マスタ:
                    this.HINBAN.Visibility = System.Windows.Visibility.Hidden;
                    this.COLOR.Visibility = System.Windows.Visibility.Hidden;
                    break;

                case (int)SEND_FORM.品番マスタ:
                    this.TOKUISAKI.Visibility = System.Windows.Visibility.Hidden;
                    this.Grid_ProductNumber.Visibility = System.Windows.Visibility.Hidden;
                    this.Grid_ProductName.Visibility = System.Windows.Visibility.Hidden;
                    this.Grid_SupplierCode.Visibility = System.Windows.Visibility.Visible;
                    this.Grid_SupplierEda.Visibility = System.Windows.Visibility.Visible;
                    this.Grid_SupplierName.Visibility = System.Windows.Visibility.Visible;
                    break;

                default:
                    break;

            }

            // 画面呼出し先定義
            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { null, typeof(SCHM01_TOK) });
            base.MasterMaintenanceWindowList.Add("M16_HINGUN", new List<Type> { null, typeof(SCHM16_HINGUN) });
            base.MasterMaintenanceWindowList.Add("M09_MYHIN", new List<Type> { null, typeof(SCHM09_MYHIN) });
            base.MasterMaintenanceWindowList.Add("M06_IRO", new List<Type> { null, typeof(SCHM06_IRO) });

            if ((!string.IsNullOrEmpty(CustomerCode) && !string.IsNullOrEmpty(CustomerEda))
                || !string.IsNullOrEmpty(ItemNumber))
            {
                // 値が設定されている場合は自動でボタン押下処理を実行
                BtnStart_Click(sender, null);
            }

            setFiltterEvent(true);

            GetProductInfo();

            // 品群にフォーカスを設定
            this.HINGUN.Focus();

        }

        #endregion

        #region リボン

        /// <summary>
        /// F1　リボン　マスタ参照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            // フォーカスコントロールを取得
			IInputElement ctl = Keyboard.FocusedElement;
            
            if (ctl is DataGridCell)
            {
                /*
                 * TODO:品番検索を開こうかと思ったけど行追加の場合だけでよい気がしたので保留
                */
            }
            else
            {
                try
                {
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                }
                catch (Exception ex)
                {
                    appLog.Error("検索画面起動エラー", ex);
                    this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";

                }

            }

        }

        /// <summary>
        /// F5　リボン　行追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            if (SearchResult == null)
                return;

            addDataGridRow();
        }

        /// <summary>
        /// F6　リボン　行削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF6Key(object sender, KeyEventArgs e)
        {
            if (SearchResult == null)
                return;

            deleteSelectedDataGridRow();

        }

        /// <summary>
        /// F9　リボン　登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            if (SearchResult == null)
                return;
            try
            {
                SearchGrid.CommitEdit(DataGridEditingUnit.Row, true);
            
                if (!isDataGridValidation())
                    return;

                if (!base.CheckAllValidation())
                {
                    this.ErrorMessage = "入力内容に誤りがあります。";
                    MessageBox.Show("入力内容に誤りがあります。");
                    SetFocusToTopControl();
                    return;
                }

                // データなしの場合は処理しない
                if (SearchResult.Rows.Count == 0 && DeletedItem.Rows.Count == 0)
                {
                    this.ErrorMessage = "登録対象のデータが存在しません。";
                    MessageBox.Show("登録対象のデータが存在しません。");
                    SetFocusToTopControl();
                    return;
                }

                var yesno = MessageBox.Show("入力内容を登録しますか？", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (yesno == MessageBoxResult.Yes)
                {
                    // REMARKS:DataTableを引数にしてもサービス側で
                    // 受け取れなかったのでDataSetとして引き渡す
                    DataSet ds = new DataSet();
                    SearchResult.TableName = "updTbl";
                    DeletedItem.TableName = "delTbl";
                    ds.Tables.Add(SearchResult);
                    ds.Tables.Add(DeletedItem);

                    base.SendRequest(
                        new CommunicationObject(
                            MessageType.UpdateData,
                            M10_TOKUHIN_Update,
                            new object[] {
                                ds,
                                ccfg.ユーザID
                            }));

                }

            }
            catch (Exception)
            {
                return;
            }

        }

        /// <summary>
        /// F10　リボン　入力取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("保存せずに入力を取り消してよろしいですか？"
                             , "確認"
                             , MessageBoxButton.YesNo
                             , MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                ScreenClear();
                this.Close();
            }

        }

        /// <summary>
        /// F11 リボン　終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            // 表示開始ボタンが使用不可の場合、確認メッセージを表示する
            if (!this.BtnStart.IsEnabled)
            {
                if (MessageBox.Show("編集を取消して終了しますか？",
                                    "確認",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning,
                                    MessageBoxResult.No) == MessageBoxResult.No)
                    return;

            }

            this.Close();

        }

        #endregion

        #region 画面イベント

        /// <summary>
        /// 表示開始ボタン押下時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (!searchInputValidation())
                return;

            if (SendFormId.Equals((int)SEND_FORM.取引先マスタ))
            {
                sendSearchForCustomer();
            }
            else if (SendFormId.Equals((int)SEND_FORM.品番マスタ))
            {
                sendSearchForProduct();
            }
            else
            {
                // 上記以外の場合は入力状態から呼出し先を判定
                if (string.IsNullOrEmpty(this.TOKUISAKI.Text1) && string.IsNullOrEmpty(TOKUISAKI.Text2))
                {
                    // 得意先が未入力なので品番で検索実行
                    sendSearchForProduct();
                }
                else
                {
                    // 得意先で検索実行
                    sendSearchForCustomer();
                }

            }

        }

        /// <summary>
        /// 絞り込み条件のイベント追加・削除をおこなう
        /// </summary>
        /// <param name="isAdd">イベントを追加するか</param>
        private void setFiltterEvent(bool isAdd)
        {
            if (isAdd)
            {
                if (isBindingEvents)
                    return;

                // コントロールイベントを追加
                // REMARKS:xamlに追加すると表示時に動作するのでロード後に追加する
                this.HINGUN.cText1Changed += textFillter_cTextChanged;
                this.ITEM_KBN.TargetUpdated += radioFillter_TargetUpdated;  // ラジオボタン操作時
                this.ITEM_KBN.SourceUpdated += textFillter_cTextChanged;    // ラジオ番号入力時
                this.TOKUI_NAME.cTextChanged += textFillter_cTextChanged;
                this.ITEM_NAME.cTextChanged += textFillter_cTextChanged;

            }
            else
            {
                if (!isBindingEvents)
                    return;

                // コントロールイベントを削除
                // REMARKS:xamlに追加すると表示時に動作するのでロード後に追加する
                this.HINGUN.cText1Changed -= textFillter_cTextChanged;
                this.ITEM_KBN.TargetUpdated -= radioFillter_TargetUpdated;  // ラジオボタン操作時
                this.ITEM_KBN.SourceUpdated -= textFillter_cTextChanged;    // ラジオ番号入力時
                this.TOKUI_NAME.cTextChanged -= textFillter_cTextChanged;
                this.ITEM_NAME.cTextChanged -= textFillter_cTextChanged;


            }

            isBindingEvents = isAdd;

        }

        /// <summary>
        /// フィルタ項目(テキスト)変更時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textFillter_cTextChanged(object sender, RoutedEventArgs e)
        {
            BtnStart_Click(sender, null);
        }

        /// <summary>
        /// フィルタ項目(ラジオボタン)変更時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioFillter_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            BtnStart_Click(sender, null);
        }

        /// <summary>
        /// 得意先コード変更時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchCustomer_cTextChanged(object sender, RoutedEventArgs e)
        {
            // どちらかが入力されていない場合は処理しない
            if (string.IsNullOrEmpty(this.TOKUISAKI.Text1) || string.IsNullOrEmpty(this.TOKUISAKI.Text2))
                return;

            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    MasterCode_Customer,
                    new object[] {
                            this.TOKUISAKI.DataAccessName,
                            this.TOKUISAKI.Text1,
                            this.TOKUISAKI.Text2,
                            this.TOKUISAKI.LinkItem
                        }));

        }

        /// <summary>
        /// データグリッドセル編集開始時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (e.Column.DisplayIndex != 2)
                return;

            // 品番情報再取得
            GetProductInfo();

        }

        /// <summary>
        /// データグリッドセルの編集終了時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Cancel)
            {
                // キャンセル時は編集前の状態を維持して処理終了
                //e.Cancel = true;
                return;
            }

            //DataRow dr = ((DataRowView)e.Row.Item).Row;
            MessageBoxImage msgImg = MessageBoxImage.Asterisk;
            string errCaption = "入力エラー";

            // 編集内容のチェック
            var val = e.Column.GetCellContent(e.Row);
            
            switch (e.Column.DisplayIndex)
            {
                case 0:
                case 1:
                    // 取引先コード
                    // 枝番
                    break;

                case 2:
                    // 品番
                    TextBox tbProduct = val as TextBox;
                    int iProduct = 0;
                    if (string.IsNullOrEmpty(tbProduct.Text))
                    {
                        MessageBox.Show("品番を入力してください", errCaption, MessageBoxButton.OK, msgImg);
                        e.Cancel = true;
                    }
                    else if (!int.TryParse(tbProduct.Text, out iProduct))
                    {
                        MessageBox.Show("品番には数値を入力してください", errCaption, MessageBoxButton.OK, msgImg);
                        e.Cancel = true;
                    }
                    else
                    {
                        // 品名を取得してグリッドに設定
                        string name = GetProductName(tbProduct.Text);
                        if (string.IsNullOrEmpty(name))
                        {
                            MessageBox.Show("指定された品番はマスタに存在しませんでした", errCaption, MessageBoxButton.OK, msgImg);
                            e.Cancel = true;
                        }
                        else
                        {
                            ((DataRowView)e.Row.Item).Row["品番名称"] = name;
                        }

                    }
                    break;

                case 3:
                    // 品名
                    break;

                case 4:
                    // 得意先品番
                    TextBox tbCustomerProduct = val as TextBox;
                    if (string.IsNullOrEmpty(tbCustomerProduct.Text))
                    {
                        MessageBox.Show("得意先品番を入力してください", errCaption, MessageBoxButton.OK, msgImg);
                        e.Cancel = true;
                    }
                    else
                    {
                        ((DataRowView)e.Row.Item).Row["得意先品番"] = tbCustomerProduct.Text;
                    }
                    break;

            }

        #endregion

        }

        #region Mindoow_Closed
        //画面が閉じられた時、データを保持する
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion

        #endregion

        #region 受信系処理
        /// <summary>
        /// データ受信メソッド
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            try
            {
                var data = message.GetResultData();
                DataTable tbl = data as DataTable;

                switch (message.GetMessageName())
                {
                    case SearchTableToCustomer:
                        // 得意先コードで検索された場合
                        DeletedItem = tbl.Clone();
                        SetTblData(tbl);
                        break;

                    case SearchTableToProduct:
                        // 品番コードで検索された場合
                        DeletedItem = tbl.Clone();
                        SetTblData(tbl);
                        break;

                    case MasterCode_Customer:
                        // 得意先名称取得
                        if (tbl != null && tbl.Rows.Count > 0)
                            this.TOKUISAKI.Label2Text = tbl.Rows[0]["名称"].ToString();

                        else
                            this.TOKUISAKI.Label2Text = string.Empty;

                        break;

                    case MasterCode_Product:
                        productInfoList = tbl.Copy();
                        break;

                    case M10_TOKUHIN_Update:
                        MessageBox.Show("登録処理が完了しました", "情報", MessageBoxButton.OK, MessageBoxImage.Information);
                        ScreenClear();
                        this.Close();

                        break;

                    default:
                        break;

                }

            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
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
        }

        #endregion

        #region 検索実処理部

        /// <summary>
        /// 得意先情報で検索を実施する
        /// </summary>
        private void sendSearchForCustomer()
        {
            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    SearchTableToCustomer,
                    new object[] {
                            this.TOKUISAKI.Text1,
                            this.TOKUISAKI.Text2
                        }));
        }

        /// <summary>
        /// 品番情報で検索を実施する
        /// </summary>
        private void sendSearchForProduct()
        {
            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    SearchTableToProduct,
                    new object[] {
                            this.HINBAN.Text1,
                            this.COLOR.Text1
                        }));

        }

        #endregion

        #region 処理メソッド群

        #region 画面初期化

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        private void ScreenClear()
        {
            // REMARKS:値クリア時にイベントが動作するので一時的に削除
            setFiltterEvent(false);

            // 各項目のクリア
            this.TOKUISAKI.Text1 = string.Empty;
            this.TOKUISAKI.Text2 = string.Empty;
            this.TOKUISAKI.Label2Text = string.Empty;
            this.HINBAN.Text1 = string.Empty;
            this.HINBAN.Text2 = string.Empty;
            this.COLOR.Text1 = string.Empty;
            this.COLOR.Text2 = string.Empty;

            this.HINGUN.Text1 = string.Empty;
            this.ITEM_KBN.Text = string.Empty;
            this.TOKUI_NAME.Text = string.Empty;
            this.ITEM_NAME.Text = string.Empty;

            SearchResult = null;
            DeletedItem = null;

            this.MaintenanceMode = string.Empty;

            // コントロール状態制御
            setSearchControlEnabled();
            setFilterControlEnabled(false);
            this.BtnStart.IsEnabled = true;
            setFiltterEvent(true);

            ResetAllValidation();

            SetFocusToTopControl();

        }

        #endregion

        /// <summary>
        /// テーブルデータを各変数に代入
        /// </summary>
        /// <param name="tbl"></param>
        private void SetTblData(DataTable tbl)
        {
            // コントロール状態を変更
            setFilterControlEnabled(true);

            // フィルタ項目絞込みを実施

            StringBuilder sb = new StringBuilder();

            sb.Append("論理削除 = false");
            if (!string.IsNullOrEmpty(this.HINGUN.Text1))
            {
                sb.AppendFormat(" AND 品群 = '{0}'", this.HINGUN.Text1);
            }

            if (!string.IsNullOrEmpty(this.ITEM_KBN.Text))
            {
                sb.AppendFormat(" AND 商品区分 = {0}", this.ITEM_KBN.Text);
            }

            if (!string.IsNullOrEmpty(this.TOKUI_NAME.Text))
            {
                sb.Append(" AND");
                sb.AppendFormat(" ( 得意先名１ LIKE '%{0}%'", this.TOKUI_NAME.Text);
                sb.AppendFormat(" OR 得意先名２ LIKE '%{0}%' )", this.TOKUI_NAME.Text);
            }

            if (!string.IsNullOrEmpty(this.ITEM_NAME.Text))
            {
                sb.AppendFormat(" AND 品番名称 LIKE '%{0}%'", this.ITEM_NAME.Text);
            }

            DataView dv = tbl.AsDataView();
            dv.RowFilter = sb.ToString();

            // 検索データ設定
            SearchResult = dv.ToTable();
            SearchResult.AcceptChanges();

        }

        #region コントロール状態設定

        /// <summary>
        /// 検索条件項目のコントロール状態を設定する
        /// </summary>
        private void setSearchControlEnabled()
        {
            // REMARKS:メニューの場合は両方使用可なので反転で指定
            this.TOKUISAKI.IsEnabled = (SendFormId != (int)SEND_FORM.品番マスタ);
            this.HINBAN.IsEnabled = (SendFormId != (int)SEND_FORM.取引先マスタ);
            this.COLOR.IsEnabled = (SendFormId != (int)SEND_FORM.取引先マスタ);

        }

        /// <summary>
        /// フィルタ条件項目のコントロール状態を設定する
        /// </summary>
        /// <param name="isEnabled"></param>
        private void setFilterControlEnabled(bool isEnabled)
        {
            if (isEnabled)
            {
                // 有効になる場合、検索条件項目を利用不可とする
                this.TOKUISAKI.IsEnabled = false;
                this.HINBAN.IsEnabled = false;
                this.COLOR.IsEnabled = false;
                this.BtnStart.IsEnabled = false;

            }

            this.HINGUN.IsEnabled = (isEnabled && SendFormId != (int)SEND_FORM.品番マスタ);
            this.ITEM_KBN.IsEnabled = (isEnabled && SendFormId != (int)SEND_FORM.品番マスタ);
            this.TOKUI_NAME.IsEnabled = (isEnabled && SendFormId != (int)SEND_FORM.取引先マスタ);
            this.ITEM_NAME.IsEnabled = (isEnabled && SendFormId != (int)SEND_FORM.品番マスタ);

            this.SearchGrid.IsEnabled = isEnabled;

        }

        #endregion

        /// <summary>
        /// 表示開始処理時の入力チェック
        /// </summary>
        /// <returns></returns>
        private bool searchInputValidation()
        {
            // 検索条件に入力がない場合(得意先コード＋枝番 or 品番＋色)
            if ((string.IsNullOrEmpty(this.TOKUISAKI.Text1) || string.IsNullOrEmpty(this.TOKUISAKI.Text2)) &&
                string.IsNullOrEmpty(this.HINBAN.Text1))
            {
                MessageBox.Show("検索条件を入力して下さい", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // 得意先と品番・色の両方に入力がある場合
            if ((!string.IsNullOrEmpty(this.TOKUISAKI.Text1) || !string.IsNullOrEmpty(this.TOKUISAKI.Text2)) &&
                (!string.IsNullOrEmpty(this.HINBAN.Text1) || !string.IsNullOrEmpty(this.COLOR.Text1)))
            {
                MessageBox.Show("得意先と品番のどちらかを選択して入力して下さい", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;

        }

        /// <summary>
        /// データグリッド内容の検証をおこなう
        /// </summary>
        /// <returns></returns>
        private bool isDataGridValidation()
        {
            foreach (var dgr in SearchGrid.Items)
            {
                DataRow row = (dgr as DataRowView).Row;

                if (row["品番コード"] == null || string.IsNullOrEmpty(row["品番コード"].ToString()))
                {
                    string msg = "品番が設定されていないデータが存在します。";
                    MessageBox.Show(msg, "入力エラー", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    ErrorMessage = msg;
                    return false;
                }
                else if (row["得意先品番"] == null || string.IsNullOrEmpty(row["得意先品番"].ToString()))
                {
                    string msg = "得意先品番が設定されていないデータが存在します。";
                    MessageBox.Show(msg, "入力エラー", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    ErrorMessage = msg;
                    return false;
                }

            }

            return true;

        }

        /// <summary>
        /// 行の追加処理をおこなう
        /// </summary>
        private void addDataGridRow()
        {
            if (SearchResult == null)
                return;

            // グリッドにフォーカスを設定
            SearchGrid.Focus();

            DataRow row = SearchResult.NewRow();

            if (!string.IsNullOrEmpty(this.TOKUISAKI.Text1))
            {
                // 得意先で検索されている場合
                row["得意先コード"] = this.TOKUISAKI.Text1;
                row["枝番"] = this.TOKUISAKI.Text2;
                row["得意先名１"] = this.TOKUISAKI.Label2Text;

            }

            row["論理削除"] = false;

            if (SendFormId == (int)SEND_FORM.取引先マスタ || SendFormId == (int)SEND_FORM.メニュー)
            {
                // 品番検索を開く
                if (ShowProductDialogForm(row))
                {
                    SearchResult.Rows.Add(row);

                    // 行追加後は追加行を選択させる
                    int insIdx = SearchResult.Rows.Count - 1;
                    SetCurrentCell(SearchGrid, insIdx, 2);

                }

            }
            else if (SendFormId == (int)SEND_FORM.品番マスタ)
            {
                SCHM01_TOK tokForm = new SCHM01_TOK();
                tokForm.TwinTextBox = new Framework.Windows.Controls.UcLabelTwinTextBox();
                tokForm.TwinTextBox.LinkItem = "0,3";   // 得意先・相殺

                if (tokForm.ShowDialog(this) ?? false)
                {
                    row["品番コード"] = ProductNumber;
                    row["品番名称"] = this.HINBAN.Text2;
                    row["得意先コード"] = tokForm.TwinTextBox.Text1;
                    row["枝番"] = tokForm.TwinTextBox.Text2;
                    row["得意先名１"] = tokForm.TwinTextBox.Text3;

                    SearchResult.Rows.Add(row);

                    // 行追加後は追加行を選択させる
                    int insIdx = SearchResult.Rows.Count - 1;
                    SetCurrentCell(SearchGrid, insIdx, 2);

                }


            }

        }

        /// <summary>
        /// 品番検索画面を展開し選択値を設定する
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool ShowProductDialogForm(DataRow row)
        {
            SCHM09_HIN diaForm = new SCHM09_HIN();
            diaForm.TwinTextBox = new Framework.Windows.Controls.UcLabelTwinTextBox();
            diaForm.取引区分 = TARGET_TRADING_KBN;
            diaForm.IsSetItemEnabled = false;

            if (diaForm.ShowDialog(this) ?? false)
            {
                row["品番コード"] = diaForm.TwinTextBox.Text1;
                row["品番名称"] = diaForm.TwinTextBox.Text2;

                if (string.IsNullOrEmpty(this.TOKUISAKI.Text1) || string.IsNullOrEmpty(this.TOKUISAKI.Text2))
                {
                    // 得意先が設定されていない場合は得意先を選択させる
                    SCHM01_TOK tokForm = new SCHM01_TOK();
                    tokForm.TwinTextBox = new Framework.Windows.Controls.UcLabelTwinTextBox();
                    tokForm.TwinTextBox.LinkItem = "0,3";   // 得意先・相殺

                    if (tokForm.ShowDialog(this) ?? false)
                    {
                        row["得意先コード"] = tokForm.TwinTextBox.Text1;
                        row["枝番"] = tokForm.TwinTextBox.Text2;

                    }
                    else
                    {
                        return false;
                    }

                }

                return true;

            }

            return false;

        }

        /// <summary>
        /// 選択されている行の削除処理をおこなう
        /// </summary>
        private void deleteSelectedDataGridRow()
        {
            if (SearchResult == null)
                return;

            // グリッドにフォーカスを設定
            this.SearchGrid.Focus();
            int selRowIdx = SearchGrid.Items.IndexOf(SearchGrid.CurrentItem);//SearchGrid.SelectedIndex;

            if (selRowIdx < 0)
                return;

            DataRow row = SearchResult.Rows[selRowIdx];
            // 検索後に追加された行についてはストック対象外とする
            if (row.RowState != DataRowState.Added)
                DeleteRowSet(row);
            row["論理削除"] = true;

            // フィルターを再適用
            SetTblData(SearchResult);

            // 削除した結果データが存在しない場合はここで終了
            if (SearchGrid.Items.Count == 0)
                return;

            // 削除行の１つ上を選択する
            int setIdx = selRowIdx - 1;
            if (selRowIdx == 0 && SearchGrid.Items.Count > 0)
            {
                // 削除対象が先頭データで削除後にデータが存在する場合は１つ下(にあったデータ)を選択
                setIdx = selRowIdx;
            }

            SetCurrentCell(SearchGrid, setIdx, 2);

        }

        /// <summary>
        /// データグリッドの指定位置にフォーカスを設定する
        /// </summary>
        /// <param name="grid">対象のデータグリッド</param>
        /// <param name="rIdx">指定行インデックス</param>
        /// <param name="cIdx">指定列インデックス</param>
        private void SetCurrentCell(DataGrid grid, int rIdx, int cIdx)
        {
            grid.Focus();

            // REMARKS:SelectionUnit=Cellの場合はエラーになる(.NETバージョンによる？)
            if (grid.SelectionUnit != DataGridSelectionUnit.Cell)
            {
                DataGridCellInfo info = new DataGridCellInfo(SearchGrid.Items[rIdx], SearchGrid.Columns[cIdx]);

                SearchGrid.SelectedIndex = rIdx;
                grid.CurrentCell = info;

                // 選択位置にスクロールする
                grid.ScrollIntoView(info);

            }

        }

        /// <summary>
        /// 削除行データを退避する
        /// </summary>
        /// <param name="row"></param>
        private void DeleteRowSet(DataRow row)
        {
            DataRow dRow = DeletedItem.NewRow();
            dRow.ItemArray = row.ItemArray;
            DeletedItem.Rows.Add(dRow);

        }

        /// <summary>
        /// 対象の品番情報を取得する
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="rowIdx"></param>
        /// <returns></returns>
        private void GetProductInfo()
        {
            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    MasterCode_Product,
                    new object[] {
                            string.Empty
                        }));

        }

        /// <summary>
        /// 品番から自社品番名を取得して返す
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        private string GetProductName(string productCode)
        {
            string name = string.Empty;

            foreach (DataRow row in productInfoList.Rows)
            {
                if (row["品番コード"].ToString().Equals(productCode))
                {
                    name = row["自社品名"].ToString();
                    break;
                }
            }

            return name;

        }

        #endregion

    }

}
