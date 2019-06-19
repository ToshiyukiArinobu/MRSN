using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 消費税率マスタ入力
    /// </summary>
    public partial class MST13010 : WindowMasterMainteBase
    {
        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;
        #region "権限関係"
        CommonConfig ccfg = null;
        #endregion

        /// <summary>
        /// 画面固有設定項目のクラス定義
        /// ※ 必ず public で定義する。
        /// </summary>
        public class ConfigMST13010 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST13010 frmcfg = null;

        #endregion

        #region << 列挙型定義 >>

        /// <summary>
        /// グリッドの列定義
        /// </summary>
        private enum GridColumnsMapping : int
        {
            適用開始日 = 0,
            消費税率 = 1,
            軽減税率 = 2
        }

        #endregion

        #region << 定数定義 >>

        // 対象テーブル検索用
        private const string TargetTableNmAll = "M73_ZEI";
        // 対象テーブル更新用
        private const string TargetTableNmUpdate = "M73_ZEI_UPD";
        // 対象テーブル更新用
        private const string TargetTableNmDelete = "M73_ZEI_DEL";

        #endregion

        #region << バインディングプロパティ >>

        private DateTime? _適用開始年月日;
        public DateTime? 適用開始年月日
        {
            get { return this._適用開始年月日; }
            set { this._適用開始年月日 = value; NotifyPropertyChanged(); }
        }
        private string _消費税率 = string.Empty;
        public string 消費税率
        {
            get { return this._消費税率; }
            set { this._消費税率 = value; NotifyPropertyChanged(); }
        }
        private string _軽減税率 = string.Empty;
        public string 軽減税率
        {
            get { return this._軽減税率; }
            set { this._軽減税率 = value; NotifyPropertyChanged(); }
        }

        // データグリッドバインド用データテーブル
        private DataTable _SearchResult = null;
        public DataTable SearchResult
        {
            get { return this._SearchResult; }
            set
            {
                this._SearchResult = value;
                NotifyPropertyChanged();
            }
        }
        #endregion


        #region << 画面初期表示処理 >>

        /// <summary>
        /// 消費税率マスタ入力 コンストラクタ
        /// </summary>
        public MST13010()
        {
            InitializeComponent();
            this.DataContext = this;

        }

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
            frmcfg = (ConfigMST13010)ucfg.GetConfigValue(typeof(ConfigMST13010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST13010();
                ucfg.SetConfigValue(frmcfg);
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
                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;
            }
            #endregion

            ScreenClear();

            ResetAllValidation();

            // 初期表示データ取得
            SendSearchRequest();

        }

        #endregion

        #region << データ受信メソッド >>

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
                    case TargetTableNmAll:
                        // 検索結果設定
                        SearchResult = tbl;
                        break;

                    case TargetTableNmUpdate:
                        // 更新時処理
                        MessageBox.Show(AppConst.COMPLETE_UPDATE, "登録完了", MessageBoxButton.OK, MessageBoxImage.Information);
                        ScreenClear();
                        break;

                    case TargetTableNmDelete:
                        // 削除時処理
                        MessageBox.Show(AppConst.COMPLETE_DELETE, "削除完了", MessageBoxButton.OK, MessageBoxImage.Information);
                        ScreenClear();
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

        #region エラーメッセージ
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            MessageBox.Show(ErrorMessage);

        }
        #endregion

        #endregion

        #region << リボン >>

        #region F8 印刷
        /// <summary>
        /// F8 リボン　印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            MST13020 mst13020 = new MST13020();
            mst13020.ShowDialog(this);

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
            Update();

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
                MessageBox.Show(
                    "保存せずに入力を取り消してよろしいですか？",
                    "確認",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
                this.ScreenClear();

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
        /// F12　リボン　削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF12Key(object sender, KeyEventArgs e)
        {
            if (SearchResult == null)
                return;

            try
            {
                if (this.適用開始年月日 == null)
                {
                    this.ErrorMessage = "登録内容がありません。";
                    MessageBox.Show("登録内容がありません。");
                    return;
                }

                if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
                {
                    this.ErrorMessage = "新規登録データは削除できません。";
                    MessageBox.Show("新規登録データは削除できません。");
                    SetFocusToTopControl();
                    return;
                }

                MessageBoxResult result =
                    MessageBox.Show(
                        "表示されている情報を削除しますか？",
                        "確認",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question,
                        MessageBoxResult.No);

                if (result == MessageBoxResult.Yes)
                {
                    base.SendRequest(
                        new CommunicationObject(
                            MessageType.UpdateData,
                            TargetTableNmDelete,
                            new object[] {
                                適用開始年月日
                            }));

                }

            }
            catch
            {
                this.ErrorMessage = "削除処理が出来ませんでした。";
            }

        }
        #endregion

        #endregion

        #region << 処理メソッド群 >>

        #region 画面初期化
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        private void ScreenClear()
        {
            this.MaintenanceMode = string.Empty;

            SearchResult = null;

            消費税率 = string.Empty;
            軽減税率 = string.Empty;
            適用開始年月日 = null;

            // 全項目初期化後にデータを再取得
            SendSearchRequest();

            ChangeKeyItemChangeable(true);
            btnEnableChange(true);
            SetFocusToTopControl();

        }
        #endregion

        #region ページングボタンの使用可否変更
        /// <summary>
        /// ページングボタンのEnableを変更する。
        /// </summary>
        /// <param name="pBool"></param>
        private void btnEnableChange(bool pBool)
        {
            btnTop.IsEnabled = pBool;
            btnBefore.IsEnabled = pBool;
            btnAfter.IsEnabled = pBool;
            btnEnd.IsEnabled = pBool;
        }
        #endregion

        #region 消費税情報取得
        /// <summary>
        /// 消費税情報を取得する
        /// </summary>
        /// <param name="pageOption"></param>
        private void SendSearchRequest()
        {
            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    TargetTableNmAll,
                    new object[] {}));

        }
        #endregion

        #region 消費税情報更新
        /// <summary>
        /// 消費税情報の更新をおこなう
        /// </summary>
        private void Update()
        {
            try
            {
                // 消費税率が未入力の場合
                if (消費税率 == "")
                {
                    this.ErrorMessage = "消費税率は入力形式が不正です。";
                    MessageBox.Show("消費税率は入力形式が不正です。");
                    return;
                }

                int i消費税率 = 0;
                int i軽減税率 = 0;
                //型変換
                i消費税率 = Convert.ToInt32(消費税率);
                i軽減税率 = 軽減税率 == string.Empty ? 0 : Convert.ToInt32(軽減税率);

                if (!base.CheckAllValidation())
                {
                    this.ErrorMessage = "入力内容に誤りがあります。";
                    MessageBox.Show("入力内容に誤りがあります。");
                    SetFocusToTopControl();
                    return;
                }

                var yesno = MessageBox.Show("入力内容を登録しますか？", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (yesno == MessageBoxResult.Yes)
                {
                    base.SendRequest(
                        new CommunicationObject(
                            MessageType.UpdateData,
                            TargetTableNmUpdate,
                            new object[] {
                                適用開始年月日,
                                i消費税率,
                                i軽減税率,
                                ccfg.ユーザID
                            }));

                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {
                this.ErrorMessage = "更新処理が失敗しました。";
            }
        }
        #endregion

        #region 検索対象の適用開始日取得
        /// <summary>
        /// ページング結果の適用開始日を取得する
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        private DateTime getSelectTargetDate(int option)
        {
            DateTime wdt;
            DateTime? targetDate = null;
            DateTime? inputDate = DateTime.TryParse(this.txtTargetDate.Text, out wdt) ? wdt : (DateTime?)null;

            switch (option)
            {
                case -2:
                    // 先頭データ取得
                    targetDate =
                        SearchResult
                            .AsEnumerable()
                            .Min(m => m.Field<DateTime>((int)GridColumnsMapping.適用開始日));
                    break;

                case -1:
                    // 前のデータ取得
                    var before =
                            SearchResult
                                .AsEnumerable()
                                .Where(w => w.Field<DateTime>((int)GridColumnsMapping.適用開始日) < inputDate);

                    if (inputDate == null || before.Count() == 0)
                        targetDate = getSelectTargetDate(-2);

                    else
                        targetDate =
                            before.Max(m => m.Field<DateTime>((int)GridColumnsMapping.適用開始日));

                    break;

                case 0:
                    targetDate = inputDate;
                    break;

                case 1:
                    // 次のデータ取得
                    var after =
                        SearchResult
                            .AsEnumerable()
                            .Where(w => w.Field<DateTime>((int)GridColumnsMapping.適用開始日) > inputDate);

                    if (inputDate == null || after.Count() == 0)
                        targetDate = getSelectTargetDate(2);

                    else
                        targetDate =
                                after.Min(m => m.Field<DateTime>((int)GridColumnsMapping.適用開始日));

                    break;

                case 2:
                    // 最後尾データ取得
                    targetDate =
                        SearchResult
                            .AsEnumerable()
                            .Max(m => m.Field<DateTime>((int)GridColumnsMapping.適用開始日));
                    break;

            }

            return (DateTime)targetDate;

        }
        #endregion

        #region DataRow to Control
        /// <summary>
        /// 対象行の内容をコントロールに設定する
        /// </summary>
        /// <param name="row"></param>
        private void setGridToControl(DataRow row)
        {
            適用開始年月日 = Convert.ToDateTime(row[(int)GridColumnsMapping.適用開始日]);
            消費税率 = row[(int)GridColumnsMapping.消費税率].ToString();
            軽減税率 = row[(int)GridColumnsMapping.軽減税率].ToString();

            // 対象データ行を選択
            SearchGrid.SelectedIndex = getTargetRowIndex(row.Field<DateTime>((int)GridColumnsMapping.適用開始日));

        }
        #endregion

        #region 適用開始日から行インデックス取得
        /// <summary>
        /// 対象の適用開始日の行インデックスを取得する
        /// </summary>
        /// <param name="targetDate"></param>
        /// <returns></returns>
        private int getTargetRowIndex(DateTime targetDate)
        {
            return SearchResult.Rows.IndexOf(
                SearchResult.AsEnumerable()
                    .Where(a => a.Field<DateTime>((int)GridColumnsMapping.適用開始日) == targetDate)
                    .FirstOrDefault());

        }
        #endregion

        #region メンテモード変更
        /// <summary>
        /// メンテナンスモード変更をおこなう
        /// </summary>
        /// <param name="maintenanceMode"></param>
        private void changeMaintenanceMode(string maintenanceMode)
        {
            if (string.IsNullOrEmpty(maintenanceMode))
            {
                this.MaintenanceMode = null;
                txtSRate.IsEnabled = false;
                txtKRate.IsEnabled = false;

            }
            else
            {
                this.MaintenanceMode = maintenanceMode;
                txtSRate.IsEnabled = true;
                txtKRate.IsEnabled = true;

            }

        }
        #endregion

        #region 指定セルのテキスト取得
        /// <summary>
        /// 選択行の指定列からテキストを取得する
        /// </summary>
        /// <param name="grid">対象のデータグリッド</param>
        /// <param name="column">指定列</param>
        /// <returns></returns>
        private string getTargetCellValue(DataGrid grid, GridColumnsMapping column)
        {
            return ((TextBlock)grid
                .Columns[(int)column]
                .GetCellContent(grid.SelectedItem)).Text;

        }
        #endregion

        #endregion

        #region << イベント処理群 >>

        #region テキストキー押下での更新イベント
        /// <summary>
        /// テキストボックスでキー押下時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                Update();
            }

        }
        #endregion

        #region 適用開始日ロストフォーカスイベント
        /// <summary>
        /// 適用開始日付テキストボックスロストフォーカス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTargetDate_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter || e.Key == Key.Tab)
                {
                    try
                    {
                        if (txtTargetDate.Text == string.Empty)
                        {
                            適用開始年月日 = DateTime.Today;
                            // 新規ステータス表示
                            changeMaintenanceMode(AppConst.MAINTENANCEMODE_ADD);

                        }
                        else
                        {
                            // グリッド内から対象データを検索
                            PagingButton_Click(sender, null);
                            
                        }

                    }
                    catch (Exception)
                    {
                        return;
                    }

                }

            }
            catch (Exception ex)
            {
                appLog.Debug("【Error:{0}***{1}】", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                this.ErrorMessage = ex.Message;
            }

        }
        #endregion

        #region ページングボタンイベント
        /// <summary>
        /// ページングボタンが押下された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PagingButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchResult == null)
                return;

            int optionNum = 0;// コード指定

            Button btn = sender as Button;

            if (btn != null)
                if (btn.Name.Equals(btnTop.Name))
                {
                    optionNum = -2;
                }
                else if (btn.Name.Equals(btnBefore.Name))
                {
                    optionNum = -1;
                }
                else if (btn.Name.Equals(btnAfter.Name))
                {
                    optionNum = 1;
                }
                else if (btn.Name.Equals(btnEnd.Name))
                {
                    optionNum = 2;
                }

            DateTime target = getSelectTargetDate(optionNum);

            DataRow selRow =
                SearchResult
                    .AsEnumerable()
                    .Where(w => w.Field<DateTime>((int)GridColumnsMapping.適用開始日) == target)
                    .FirstOrDefault();

            if (selRow != null)
            {
                setGridToControl(selRow);
                // 編集ステータス表示
                changeMaintenanceMode(AppConst.MAINTENANCEMODE_EDIT);

            }
            else
            {
                // 対象が見つからない場合は新規登録扱いとする
                // 新規ステータス表示
                changeMaintenanceMode(AppConst.MAINTENANCEMODE_ADD);
                SetFocusToTopControl();

            }

        }
        #endregion

        #region グリッド選択セル変更時イベント
        /// <summary>
        /// データグリッドの選択セルが変更された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;

            if (grid.SelectedItem != null)
            {
                int ival;

                // 選択行の内容を各項目に設定
                適用開始年月日 = DateTime.Parse(getTargetCellValue(grid, GridColumnsMapping.適用開始日));
                消費税率 = int.TryParse(getTargetCellValue(grid, GridColumnsMapping.消費税率).Replace("%", ""), out ival) ? ival.ToString() : string.Empty;
                軽減税率 = int.TryParse(getTargetCellValue(grid, GridColumnsMapping.軽減税率).Replace("%", ""), out ival) ? ival.ToString() : string.Empty;

                // 編集ステータス表示
                changeMaintenanceMode(AppConst.MAINTENANCEMODE_EDIT);

            }

        }
        #endregion

        #region Form Closed
        /// <summary>
        /// 画面が閉じられた後のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

    }

}
