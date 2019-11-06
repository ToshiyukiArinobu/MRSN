using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using GrapeCity.Windows.SpreadGrid;

namespace KyoeiSystem.Application.Windows.Views
{
    using WinForms = System.Windows.Forms;
    using KyoeiSystem.Application.Windows.Views.Common;

    /// <summary>
    /// 製品原価一括更新
    /// </summary>
    public partial class MST03011 : RibbonWindowViewBase
    {
        #region << 列挙型定義 >>
        /// <summary>
        /// データグリッドの新列定義
        /// </summary>
        private enum GridColumnsMapping : int
        {
            品番コード = 0,
            自社品番 = 1,
            自社色 = 2,
            自社色名称 = 3,
            自社品名 = 4,
            原価 = 5,
            加工原価 = 6,
            卸値 = 7,
            売価 = 8,
            原価＿合計 = 9,
            卸値＿合計 = 10,
            売価＿合計 = 11,
            原価＿入力 = 12,
            加工原価＿入力 = 13,
            卸値＿入力 = 14,
            売価＿入力 = 15
        }

        /// <summary>
        /// コンボボックス用
        /// </summary>
        public class ComboBoxClass
        {
            public int コード { get; set; }
            public string 名称 { get; set; }

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
        public class ConfigMST03011 : FormConfigBase
        {
            public byte[] spConfig20180118 = null;

        }

        /// ※ 必ず public で定義する。
        public ConfigMST03011 frmcfg = null;

        public byte[] sp_Config = null;

        #endregion

        #region << 定数定義 >>

        private const string MST03011_GetData = "MST03011_GetData";
        private const string MST03011_Update = "MST03011_Update";
        private const string MST03011_GetMasterDataSet = "MST03011_GetMasterDataSet";

        //商品分類コンボ用
        private ComboBoxClass[] _ShouhinBunrui
            = { 
				  new ComboBoxClass() { コード = 0, 名称 = "", },
				  new ComboBoxClass() { コード = 1, 名称 = "食品", },
				  new ComboBoxClass() { コード = 2, 名称 = "繊維", },
				  new ComboBoxClass() { コード = 3, 名称 = "その他", },
			  };
        public ComboBoxClass[] ShouhinBunrui
        {
            get { return _ShouhinBunrui; }
            set { _ShouhinBunrui = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region << 変数定義 >>

        private Dictionary<string, string> paramDic = new Dictionary<string, string>();

        /// <summary>グリッドコントローラ</summary>
        GcSpreadGridController gridDtl;

        #endregion

        #region バインディングプロパティ

        public DataTable _SearchResult;
        public DataTable SearchResult
        {
            get { return this._SearchResult; }
            set { this._SearchResult = value; NotifyPropertyChanged(); }

        }

        public DataSet _MasterDataSet;
        public DataSet MasterDataSet
        {
            get { return this._MasterDataSet; }
            set { this._MasterDataSet = value; NotifyPropertyChanged(); }

        }

        private string _自社品名 = string.Empty;
        public string 自社品名
        {
            get { return this._自社品名; }
            set { this._自社品名 = value; NotifyPropertyChanged(); }
        }

        private int _商品分類 = 0;
        public int 商品分類
        {
            get { return this._商品分類; }
            set { this._商品分類 = value; NotifyPropertyChanged(); }
        }

        private string _ブランド = string.Empty;
        public string ブランド
        {
            get { return this._ブランド; }
            set { this._ブランド = value; NotifyPropertyChanged(); }
        }

        private string _シリーズ = string.Empty;
        public string シリーズ
        {
            get { return this._シリーズ; }
            set { this._シリーズ = value; NotifyPropertyChanged(); }
        }

        private string _品群 = string.Empty;
        public string 品群
        {
            get { return this._品群; }
            set { this._品群 = value; NotifyPropertyChanged(); }
        }

        #endregion

 

        #region<< 製品原価一括更新 初期処理群 >>

        /// <summary>
        ///製品原価一括更新 コンストラクタ
        /// </summary>
        public MST03011()
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

            base.SendRequest(new CommunicationObject(MessageType.RequestData, MST03011_GetMasterDataSet, null));

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            frmcfg = (ConfigMST03011)ucfg.GetConfigValue(typeof(ConfigMST03011));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST03011();
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

            base.MasterMaintenanceWindowList.Add("M14_BRAND", new List<Type> { typeof(MST04020), typeof(SCHM14_BRAND) });
            base.MasterMaintenanceWindowList.Add("M15_SERIES", new List<Type> { typeof(MST04021), typeof(SCHM15_SERIES) });
            base.MasterMaintenanceWindowList.Add("M16_HINGUN", new List<Type> { typeof(MST04022), typeof(SCHM16_HINGUN) });

            ScreenClear();

            spGridList.InputBindings.Add(new KeyBinding(spGridList.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));
            spGridList.RowCount = 0;

            // グリッドコントローラ設定
            gridDtl = new GcSpreadGridController(spGridList);

            SetFocusToTopControl();
            ErrorMessage = "";

            // フォーカス設定
            cmb_商品分類.Focus();

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
            {
                SearchResult.Clear();
            }
            SearchResult = null;

            自社品名 = string.Empty;
            cmb_商品分類.SelectedIndex = 0;
            ブランド = string.Empty;
            シリーズ = string.Empty;
            品群 = string.Empty;

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
                case MST03011_GetMasterDataSet:
                    if (data is DataSet)
                    {
                        MasterDataSet = data as DataSet;
                    }
                    break;
                case MST03011_GetData :
                    base.SetFreeForInput();
                    
                    SearchResult = tbl;
                    SetData(tbl);

                    if (tbl.Rows.Count > 0)
                    {
                        spGridList.ShowRow(0);
                    }

                    break;
                    
                case MST03011_Update:
                    base.SetFreeForInput();
                    if ((int)data == 1)
                    {
                        MessageBox.Show("更新完了しました。");
                    }
                    else
                    {
                        MessageBox.Show("更新に失敗しました。");
                        return;
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

        #region << リボン >>

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
                {
                    return;
                }

                if (UpdateValidataCheck() == false)
                {
                    return;
                }

                DataSet ds = new DataSet();
                ds.Tables.Add(SearchResult);

                base.SendRequest(
                    new CommunicationObject(MessageType.UpdateData, MST03011_Update, new object[]{
                            ds,
                            ccfg.ユーザID,
                        }));
                base.SetBusyForInput();
            }
            catch (Exception ex)
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

        /// <summary>
        /// 更新前データチェック
        /// </summary>
        /// <returns></returns>
        private bool UpdateValidataCheck()
        {
            //バイト数検索用
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            int bytenum;
            int iData;
            decimal dData;
            Dictionary<string, string> dicTableKey = new Dictionary<string, string>();

            for (int i = 0; i < SearchResult.Rows.Count; i++)
            {
                DataRow drData = SearchResult.Rows[i];
                //品番コード	int
                if (int.TryParse(drData["セット品番コード"].ToString(), out iData) == false)
                {
                    this.ErrorMessage = (i + 1).ToString() + "行目のセット品番コードに数値以外のデータがセットされています。";
                    return false;
                }
                //自社品番	varchar(12)	
                bytenum = sjisEnc.GetByteCount(drData["セット品品番"].ToString());
                if (bytenum > 12)
                {
                    this.ErrorMessage = (i + 1).ToString() + "行目のセット品品番が12byteを超えています。";
                    return false;
                }
                //自社色	varchar(3)
                if (string.IsNullOrEmpty(drData["自社色"].ToString()) == false)
                {
                    DataTable dtM06 = MasterDataSet.Tables["M06List"];

                    var drlistM06 = dtM06.AsEnumerable()
                                    .Where(w => w.Field<string>("色コード") == drData["自社色"].ToString())
                                    .ToArray();

                    if (drlistM06.Length == 0)
                    {
                        this.ErrorMessage = (i + 1).ToString() + "行目の自社色はマスタに存在しないデータがセットされています。";
                        return false;
                    }
                }

                // 品番コードと自社品番、名称の存在チェック
                DataTable dtM09 = MasterDataSet.Tables["M09List"];

                var drlistM09 = dtM09.AsEnumerable()
                                .Where(w => w.Field<string>("自社品番") == drData["セット品品番"].ToString()
                                        && ((drData["自社色"].ToString() == String.Empty
                                            && w.Field<string>("自社色") == null)
                                            || w.Field<string>("自社色") == drData["自社色"].ToString()))
                                .ToArray();

                if (drlistM09.Length == 0)
                {
                    this.ErrorMessage = (i + 1).ToString() + "行目は品番マスタに存在しない自社品番・自社色がセットされています。";
                    return false;
                }

                //原価	decimal(9, 2)	
                if (string.IsNullOrEmpty(drData["更新用原価"].ToString()) == false)
                {
                    if (decimal.TryParse(drData["更新用原価"].ToString(), out dData) == false)
                    {
                        this.ErrorMessage = (i + 1).ToString() + "行目の更新用原価に数値以外のデータがセットされています。";
                        return false;
                    }
                }
                //加工原価	decimal(9, 2)
                if (string.IsNullOrEmpty(drData["更新用加工原価"].ToString()) == false)
                {
                    if (decimal.TryParse(drData["更新用加工原価"].ToString(), out dData) == false)
                    {
                        this.ErrorMessage = (i + 1).ToString() + "行目の更新用加工原価に数値以外のデータがセットされています。";
                        return false;
                    }
                }
                //卸値	decimal(9, 2)	
                if (string.IsNullOrEmpty(drData["更新用卸値"].ToString()) == false)
                {
                    if (decimal.TryParse(drData["更新用卸値"].ToString(), out dData) == false)
                    {
                        this.ErrorMessage = (i + 1).ToString() + "行目の更新用卸値に数値以外のデータがセットされています。";
                        return false;
                    }
                }
                //売価	decimal(9, 2)
                if (string.IsNullOrEmpty(drData["更新用売価"].ToString()) == false)
                {
                    if (decimal.TryParse(drData["更新用売価"].ToString(), out dData) == false)
                    {
                        this.ErrorMessage = (i + 1).ToString() + "行目の更新用売価に数値以外のデータがセットされています。";
                        return false;
                    }
                }

                // キー項目重複チェック
                if (dicTableKey.ContainsKey(drData["セット品番コード"].ToString()) == true)
                {
                    this.ErrorMessage = (i + 1).ToString() + "行目のセット品番コードが重複しています。";
                    return false;
                }
                dicTableKey.Add(drData["セット品番コード"].ToString(), drData["セット品品番"].ToString());

            }

            return true;
        }

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
                    new CommunicationObject(MessageType.RequestData,MST03011_GetData,new object[]
                        {
                            自社品名
                            ,商品分類
                            ,ブランド
                            ,シリーズ
                            ,品群
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
                if (frmcfg == null) { frmcfg = new ConfigMST03011(); }
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
                //ここでグリッドに表示する

                spGridList.Rows.AddNew();

                spGridList[iSpdRowIndex, GridColumnsMapping.品番コード.GetHashCode()].Value = tbl.Rows[row]["セット品番コード"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.自社品番.GetHashCode()].Value = tbl.Rows[row]["セット品品番"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.自社色.GetHashCode()].Value = tbl.Rows[row]["自社色"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.自社色名称.GetHashCode()].Value = tbl.Rows[row]["自社色名称"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.自社品名.GetHashCode()].Value = tbl.Rows[row]["自社品名"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.原価.GetHashCode()].Value = tbl.Rows[row]["登録原価"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.加工原価.GetHashCode()].Value = tbl.Rows[row]["登録加工原価"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.卸値.GetHashCode()].Value = tbl.Rows[row]["登録卸値"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.売価.GetHashCode()].Value = tbl.Rows[row]["登録売価"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.原価＿合計.GetHashCode()].Value = tbl.Rows[row]["構成品原価合計"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.卸値＿合計.GetHashCode()].Value = tbl.Rows[row]["構成品卸値合計"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.売価＿合計.GetHashCode()].Value = tbl.Rows[row]["構成品売価合計"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.原価＿入力.GetHashCode()].Value = tbl.Rows[row]["更新用原価"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.加工原価＿入力.GetHashCode()].Value = tbl.Rows[row]["更新用加工原価"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.卸値＿入力.GetHashCode()].Value = tbl.Rows[row]["更新用卸値"].ToString();
                spGridList[iSpdRowIndex, GridColumnsMapping.売価＿入力.GetHashCode()].Value = tbl.Rows[row]["更新用売価"].ToString();

                //スプレッド行インデックスインクリメント
                iSpdRowIndex = iSpdRowIndex + 1;

            }

        }
        #endregion

        #region << 機能処理群 >>

        /// <summary>
        /// セル編集コミット時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchGrid_CellEditEnded(object sender, GrapeCity.Windows.SpreadGrid.SpreadCellEditEndedEventArgs e)
        {
            GcSpreadGrid grid = sender as GcSpreadGrid;

            int intColumnIdx = grid.ActiveCellPosition.Column;
            string targetColumn = grid.ActiveCellPosition.ColumnName;

            //明細行が存在しない場合は処理しない
            if (SearchResult == null) return;

            Row targetRow = grid.Rows[grid.ActiveRowIndex];

            //編集したセルの値を取得
            var CellValue = grid[grid.ActiveRowIndex,targetColumn].Value;

            if (CellValue != null)
            {
                SearchResult.Rows[targetRow.Index][targetColumn] = CellValue;

            }
        }

        #endregion

        /// <summary>
        /// CSVOutputボタン押下イベント
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
            SearchResult.TableName = MST03011_GetData;
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
