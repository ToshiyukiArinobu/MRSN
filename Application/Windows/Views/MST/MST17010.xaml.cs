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
using System.Data;

using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;


namespace KyoeiSystem.Application.Windows.Views
{
    using ConsolLog = System.Diagnostics.Debug;

    /// <summary>
    /// 仕入先商品売価設定
    /// </summary>
    public partial class MST17010 : WindowMasterMainteBase
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

        /// <summary>仕入先ベースのデータ取得</summary>
        private const string SearchTableToSupplier = "M03_BAIKA_GetData_Supplier";
        /// <summary>自社品番・色ベースでのデータ取得</summary>
        private const string SearchTableToProduct = "M03_BAIKA_GetData_Product";
        /// <summary>仕入先売価情報登録</summary>
        private const string M02_BAIKA_Update = "M03_BAIKA_Update";
        /// <summary>品番情報取得</summary>
        private const string MasterCode_Product = "UcProduct";

        /// <summary>この画面が対応する取引区分</summary>
        private const int TARGET_TRADING_KBN = 1;   // 仕入先

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
        public class ConfigMST17010 : FormConfigBase
        {
            //public bool[] 表示順方向 { get; set; }
            /// コンボボックスの位置
            public int 集計区分_Combo { get; set; }
        }

        /// ※ 必ず public で定義する。
        public ConfigMST17010 frmcfg = null;

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
        public string SupplierCode { get; set; }
        /// <summary>取引先コード枝番</summary>
        public string SupplierEda { get; set; }
        /// <summary>品番コード</summary>
        public int HinbanCode { get; set; }
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
        /// 仕入先商品売価設定 コンストラクタ
        /// </summary>
        public MST17010()
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
            frmcfg = (ConfigMST17010)ucfg.GetConfigValue(typeof(ConfigMST17010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST17010();
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
            if (!string.IsNullOrEmpty(SupplierCode))
                this.SHIIRESAKI.Text1 = SupplierCode;

            if (!string.IsNullOrEmpty(SupplierEda))
                this.SHIIRESAKI.Text2 = SupplierEda;

            if (!string.IsNullOrEmpty(ItemNumber))
                this.HINBAN.Text1 = ItemNumber;

            if (!string.IsNullOrEmpty(ColorCode))
                this.COLOR.Text1 = ColorCode;

            #endregion

            // 画面呼出し先定義
            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { null, typeof(SCHM01_TOK) });
            base.MasterMaintenanceWindowList.Add("M16_HINGUN", new List<Type> { null, typeof(SCHM16_HINGUN) });
            base.MasterMaintenanceWindowList.Add("M09_MYHIN", new List<Type> { null, typeof(SCHM09_MYHIN) });
            base.MasterMaintenanceWindowList.Add("M06_IRO", new List<Type> { null, typeof(SCHM06_IRO) });

            if ((!string.IsNullOrEmpty(SupplierCode) && !string.IsNullOrEmpty(SupplierEda))
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

        #region F01 マスタ参照
        /// <summary>
        /// F1　リボン　マスタ参照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            try
            {
                object elmnt = FocusManager.GetFocusedElement(this);
                var tokBox = ViewBaseCommon.FindVisualParent<M01_TOK_TextBox>(elmnt as Control);

                if (tokBox != null)
                {
                    // 取引先テキストの場合
                    tokBox.OpenSearchWindow(this);

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

        #region F05 行追加
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
        #endregion

        #region F06 行削除
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
        #endregion

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

            try
            {
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
                    DataSet ds = new DataSet();
                    SearchResult.TableName = "updTbl";
                    DeletedItem.TableName = "delTbl";
                    ds.Tables.Add(SearchResult.Copy());
                    ds.Tables.Add(DeletedItem.Copy());

                    base.SendRequest(
                        new CommunicationObject(
                            MessageType.UpdateData,
                            M02_BAIKA_Update,
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
        #endregion

        #region F10 入力取消
        /// <summary>
        /// F10　リボン　入力取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            MessageBoxResult result =
                MessageBox.Show("保存せずに入力を取り消してよろしいですか？",
                    "確認",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
                ScreenClear();

        }
        #endregion

        #region F11 終了
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

        #endregion

        #region 表示開始押下時イベント
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
                sendSearchForSupplier();
            }
            else if (SendFormId.Equals((int)SEND_FORM.品番マスタ))
            {
                sendSearchForProduct();
            }
            else
            {
                // 上記以外の場合は入力状態から呼出し先を判定
                if (string.IsNullOrEmpty(this.SHIIRESAKI.Text1) && string.IsNullOrEmpty(SHIIRESAKI.Text2))
                {
                    // 仕入先が未入力なので品番で検索実行
                    sendSearchForProduct();
                }
                else
                {
                    // 仕入先で検索実行
                    sendSearchForSupplier();
                }

            }

        }
        #endregion

        #region << フィルタ変更時イベント群 >>
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

        #endregion

        #region << spreadGrid 関連イベント >>

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
                    // 単価
                    TextBox tbPrice = val as TextBox;
                    double iPrice = 0;
                    if (string.IsNullOrEmpty(tbPrice.Text))
                    {
                        MessageBox.Show("単価を入力してください", errCaption, MessageBoxButton.OK, msgImg);
                        e.Cancel = true;
                    }
                    else if (!double.TryParse(tbPrice.Text, out iPrice))
                    {
                        MessageBox.Show("単価には数値を入力してください", errCaption, MessageBoxButton.OK, msgImg);
                        e.Cancel = true;
                    }
                    break;

            }

        }
        #endregion

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
                    case SearchTableToSupplier:
                        // 仕入先コードで検索された場合
                        DeletedItem = tbl.Clone();
                        SetTblData(tbl);
                        break;

                    case SearchTableToProduct:
                        // 品番コードで検索された場合
                        DeletedItem = tbl.Clone();
                        SetTblData(tbl);
                        break;

                    case MasterCode_Product:
                        productInfoList = tbl.Copy();
                        break;

                    case M02_BAIKA_Update:
                        MessageBox.Show("登録処理が完了しました", "情報", MessageBoxButton.OK, MessageBoxImage.Information);
                        //ScreenClear();
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
        /// 仕入先情報で検索を実施する
        /// </summary>
        private void sendSearchForSupplier()
        {
            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    SearchTableToSupplier,
                    new object[] {
                            this.SHIIRESAKI.Text1,
                            this.SHIIRESAKI.Text2
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
            this.SHIIRESAKI.Text1 = string.Empty;
            this.SHIIRESAKI.Text2 = string.Empty;
            this.SHIIRESAKI.Label2Text = string.Empty;
            this.HINBAN.Text1 = string.Empty;
            this.HINBAN.Text2 = string.Empty;
            this.COLOR.Text1 = string.Empty;
            this.COLOR.Text2 = string.Empty;

            this.HINGUN.Text1 = string.Empty;
            this.ITEM_KBN.Text = string.Empty;
            this.SHIIRE_NAME.Text = string.Empty;
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

        #region 絞り込みイベントの追加削除
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
                this.SHIIRE_NAME.cTextChanged += textFillter_cTextChanged;
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
                this.SHIIRE_NAME.cTextChanged -= textFillter_cTextChanged;
                this.ITEM_NAME.cTextChanged -= textFillter_cTextChanged;


            }

            isBindingEvents = isAdd;

        }
        #endregion

        #region 取得データ設定
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

            if (!string.IsNullOrEmpty(this.SHIIRE_NAME.Text))
            {
                sb.Append(" AND");
                sb.AppendFormat(" ( 仕入先名１ LIKE '%{0}%'", this.SHIIRE_NAME.Text);
                sb.AppendFormat(" OR 仕入先名２ LIKE '%{0}%' )", this.SHIIRE_NAME.Text);
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
        #endregion

        #region コントロール状態設定

        /// <summary>
        /// 検索条件項目のコントロール状態を設定する
        /// </summary>
        private void setSearchControlEnabled()
        {
            // REMARKS:メニューの場合は両方使用可なので反転で指定
            this.SHIIRESAKI.IsEnabled = (SendFormId != (int)SEND_FORM.品番マスタ);
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
                this.SHIIRESAKI.IsEnabled = false;
                this.HINBAN.IsEnabled = false;
                this.COLOR.IsEnabled = false;
                this.BtnStart.IsEnabled = false;

            }

            this.HINGUN.IsEnabled = (isEnabled && SendFormId != (int)SEND_FORM.品番マスタ);
            this.ITEM_KBN.IsEnabled = (isEnabled && SendFormId != (int)SEND_FORM.品番マスタ);
            this.SHIIRE_NAME.IsEnabled = (isEnabled && SendFormId != (int)SEND_FORM.取引先マスタ);
            this.ITEM_NAME.IsEnabled = (isEnabled && SendFormId != (int)SEND_FORM.品番マスタ);

            this.SearchGrid.IsEnabled = isEnabled;

        }

        #endregion

        #region << 入力チェック群 >>
        /// <summary>
        /// 表示開始処理時の入力チェック
        /// </summary>
        /// <returns></returns>
        private bool searchInputValidation()
        {
            // 検索条件に入力がない場合(仕入先コード＋枝番 or 品番＋色)
            if ((string.IsNullOrEmpty(this.SHIIRESAKI.Text1) || string.IsNullOrEmpty(this.SHIIRESAKI.Text2)) &&
                (string.IsNullOrEmpty(this.HINBAN.Text1) && string.IsNullOrEmpty(this.COLOR.Text1)))
            {
                MessageBox.Show("検索条件を入力して下さい", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // 仕入先と品番・色の両方に入力がある場合
            if ((!string.IsNullOrEmpty(this.SHIIRESAKI.Text1) || !string.IsNullOrEmpty(this.SHIIRESAKI.Text2)) &&
                (!string.IsNullOrEmpty(this.HINBAN.Text1) || !string.IsNullOrEmpty(this.COLOR.Text1)))
            {
                MessageBox.Show("仕入先と品番のどちらかを選択して入力して下さい", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    MessageBox.Show("品番が設定されていないデータが存在します。", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return false;
                }
                else if (row["単価"] == null || string.IsNullOrEmpty(row["単価"].ToString()))
                {
                    MessageBox.Show("単価が設定されていないデータが存在します。", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return false;
                }

            }

            return true;

        }
        #endregion

        #region グリッド行追加
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

            row["論理削除"] = false;
            if (!string.IsNullOrEmpty(this.SHIIRESAKI.Text1))
            {
                // 仕入先で検索されている場合
                row["仕入先コード"] = this.SHIIRESAKI.Text1;
                row["仕入先コード枝番"] = this.SHIIRESAKI.Text2;
                row["仕入先名１"] = this.SHIIRESAKI.Label2Text;

                // 品番検索を開く
                if (ShowProductDialogForm(row))
                {
                    SearchResult.Rows.Add(row);

                    // 行追加後は追加行を選択させる
                    int insIdx = SearchResult.Rows.Count - 1;
                    SetCurrentCell(SearchGrid, insIdx, 2);

                }
            }

            if (!string.IsNullOrEmpty(this.HINBAN.Text1))
            {
                // 品番で検索されている場合
                row["品番コード"] = this.HinbanCode;
                row["品番名称"] = this.HINBAN.Text2;

                // 仕入先が設定されていない場合は得意先を選択させる
                SCHM01_TOK tokForm = new SCHM01_TOK();
                tokForm.TwinTextBox = new Framework.Windows.Controls.UcLabelTwinTextBox();
                tokForm.TwinTextBox.LinkItem = "1,3";   // 仕入先・相殺

                if (tokForm.ShowDialog(this) ?? false)
                {
                    // 選択した品番が既に存在するかチェック
                    if (SearchResult.Select(string.Format("仕入先コード = {0} AND 仕入先コード枝番 = {1}", tokForm.TwinTextBox.Text1, tokForm.TwinTextBox.Text2)).Count() == 0)
                    {
                        row["仕入先コード"] = tokForm.TwinTextBox.Text1;
                        row["仕入先コード枝番"] = tokForm.TwinTextBox.Text2;
                        row["仕入先名１"] = tokForm.TwinTextBox.Text3;

                        SearchResult.Rows.Add(row);

                        // 行追加後は追加行を選択させる
                        int insIdx = SearchResult.Rows.Count - 1;
                        SetCurrentCell(SearchGrid, insIdx, 2);
                    }
                    else
                    {
                        MessageBox.Show("選択された仕入先は既に登録されています", "確認", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;

                    }
                }
            }

        }
        #endregion

        #region 品番検索表示
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
                // 選択した品番が既に存在するかチェック
                if (SearchResult.Select(string.Format("品番コード = {0}", diaForm.TwinTextBox.Text1)).Count() == 0)
                {
                    row["品番コード"] = diaForm.TwinTextBox.Text1;
                    row["品番名称"] = diaForm.TwinTextBox.Text2;
                    if (!string.IsNullOrEmpty(diaForm.TwinTextBox.Text3))
                        row["単価"] = diaForm.TwinTextBox.Text3;

                    if (string.IsNullOrEmpty(this.SHIIRESAKI.Text1) || string.IsNullOrEmpty(this.SHIIRESAKI.Text2))
                    {
                        // 得意先が設定されていない場合は得意先を選択させる
                        SCHM01_TOK tokForm = new SCHM01_TOK();
                        tokForm.TwinTextBox = new Framework.Windows.Controls.UcLabelTwinTextBox();
                        tokForm.TwinTextBox.LinkItem = "1,3";   // 仕入先・相殺

                        if (tokForm.ShowDialog(this) ?? false)
                        {
                            row["仕入先コード"] = tokForm.TwinTextBox.Text1;
                            row["仕入先コード枝番"] = tokForm.TwinTextBox.Text2;

                        }
                        else
                        {
                            return false;
                        }

                    }

                    return true;

                }
                else
                {
                    MessageBox.Show("選択された品番は既に登録されています", "確認", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }

            return false;

        }
        #endregion

        #region 行削除処理
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
        #endregion

        #region グリッドフォーカスセット
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
        #endregion

        #region 削除データ退避
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
        #endregion

        #region 品番取得
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

        #endregion

    }

}
