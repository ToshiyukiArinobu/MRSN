using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;


namespace KyoeiSystem.Application.Windows.Views
{
    using MethodBase = System.Reflection.MethodBase;

    /// <summary>
    /// 自社マスタ画面クラス
    /// </summary>
    public partial class MST16010 : WindowMasterMainteBase
    {
        /// <summary>対象テーブル検索用</summary>
        private const string TargetTableNm = "M70_JIS_GetData";
        /// <summary>対象テーブル更新用</summary>
        private const string TargetTableNmUpdate = "M70_JIS_Update";
        /// <summary>対象テーブル削除用</summary>
        private const string TargetTableNmDelete = "M70_JIS_Delete";
        /// <summary>住所取得</summary>
        private const string GET_UcZip = "UcZIP";

        #region 画面定数定義

        /// <summary>自社区分 販社</summary>
        private const string CompanyKbn_hansya = "1";

        #endregion

        /// <summary>取引先コードのフォーカス有無</summary>
        //private bool isFocusedSupCode = false;

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
        public class ConfigMST16010 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST16010 frmcfg = null;

        #endregion

        #region データバインド用プロパティ

        private string _自社ID;
        private string _自社名;
        private string _代表者名;
        private string _郵便番号;
        private string _住所１;
        private string _住所２;
        private string _電話番号;
        private string _ＦＡＸ番号;
        private string _振込先銀行１;
        private string _振込先銀行２;
        private string _振込先銀行３;
        private string _法人ナンバー;
        private string _自社区分;
        private string _取引先コード;
        private string _取引先コード枝番;
        private byte[] _ロゴ画像;
        private DateTime? _削除日時 = null;

        public string 自社ID { get { return _自社ID; } set { this._自社ID = value; NotifyPropertyChanged(); } }
        public string 自社名 { get { return _自社名; } set { this._自社名 = value; NotifyPropertyChanged(); } }
        public string 代表者名 { get { return _代表者名; } set { this._代表者名 = value; NotifyPropertyChanged(); } }
        public string 郵便番号 { get { return _郵便番号; } set { this._郵便番号 = value; NotifyPropertyChanged(); } }
        public string 住所１ { get { return _住所１; } set { this._住所１ = value; NotifyPropertyChanged(); } }
        public string 住所２ { get { return _住所２; } set { this._住所２ = value; NotifyPropertyChanged(); } }
        public string 電話番号 { get { return _電話番号; } set { this._電話番号 = value; NotifyPropertyChanged(); } }
        public string ＦＡＸ番号 { get { return _ＦＡＸ番号; } set { this._ＦＡＸ番号 = value; NotifyPropertyChanged(); } }
        public string 振込先銀行１ { get { return _振込先銀行１; } set { this._振込先銀行１ = value; NotifyPropertyChanged(); } }
        public string 振込先銀行２ { get { return _振込先銀行２; } set { this._振込先銀行２ = value; NotifyPropertyChanged(); } }
        public string 振込先銀行３ { get { return _振込先銀行３; } set { this._振込先銀行３ = value; NotifyPropertyChanged(); } }
        public string 法人ナンバー { get { return _法人ナンバー; } set { this._法人ナンバー = value; NotifyPropertyChanged(); } }
        public string 自社区分 { get { return _自社区分; } set { this._自社区分 = value; NotifyPropertyChanged(); } }
        public string 取引先コード { get { return _取引先コード; } set { this._取引先コード = value; NotifyPropertyChanged(); } }
        public string 取引先コード枝番 { get { return _取引先コード枝番; } set { this._取引先コード枝番 = value; NotifyPropertyChanged(); } }
        public byte[] ロゴ画像 { get { return _ロゴ画像; } set { this._ロゴ画像 = value; NotifyPropertyChanged(); } }
        public DateTime? 削除日時 { get { return this._削除日時; } set { this._削除日時 = value; NotifyPropertyChanged(); } }

        private DataRow _MstData;
        public DataRow MstData
        {
            get { return this._MstData; }
            set { this._MstData = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region MST16010
        /// <summary>
        /// 自社マスタ コンストラクタ
        /// </summary>
        public MST16010()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion

        #region LOAD処理
        /// <summary>
        /// 画面読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
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
            frmcfg = (ConfigMST16010)ucfg.GetConfigValue(typeof(ConfigMST16010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST16010();
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

            // 初期化
            ScreenClear();

            // 自社マスタ
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { null, typeof(SCHM70_JIS) });
            // 取引先マスタ
            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { null, typeof(SCHM01_TOK) });

        }
        #endregion

        #region データ受信
        /// <summary>
        /// 取得データの正常受信時のイベント
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            try
            {
                var data = message.GetResultData();
                DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
                switch (message.GetMessageName())
                {
                    case TargetTableNm:
                        // 登録内容検索用
                        strData(tbl);
                        break;

                    case TargetTableNmUpdate:
                        // 更新時処理
                        MessageBox.Show(AppConst.COMPLETE_UPDATE);
                        // コントロール初期化
                        ScreenClear();

                        break;

                    case TargetTableNmDelete:
                        // 削除時処理
                        MessageBox.Show(AppConst.COMPLETE_DELETE);
                        // コントロール初期化
                        ScreenClear();

                        break;

                    case GET_UcZip:
                        // 住所情報取得
                        ShowAddresList(tbl);
                        break;

                    default:
                        break;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        /// <summary>
        /// 取得データをバインディングデータに設定する
        /// </summary>
        /// <param name="tbl"></param>
        private void strData(DataTable tbl)
        {
            if (tbl.Rows.Count > 0)
            {
                //削除日時がnullのデータの時
                if (!string.IsNullOrEmpty(tbl.Rows[0]["削除日時"].ToString()))
                {
                    //this.ErrorMessage = "既に削除されているデータです。";
                    //MessageBox.Show("既に削除されているデータです。");
                    //ScreenClear();
                    //return;
                    MessageBoxResult result = MessageBox.Show("既に削除されているデータです。\nこのコードを復元しますか？",
                                                                                                                   "質問",
                                                                                                                  MessageBoxButton.YesNo,
                                                                                                                  MessageBoxImage.Exclamation,
                                                                                                                  MessageBoxResult.No);

                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                MstData = tbl.Rows[0];

                if (MstData["自社区分"].ToString() == "0")
                {
                    suppliers.LinkItem = "1";// 仕入先
                }
                else
                {
                    suppliers.LinkItem = "4";// 販社
                }

                自社ID = MstData["自社コード"].ToString();
                自社名 = MstData["自社名"].ToString();
                代表者名 = MstData["代表者名"].ToString();
                郵便番号 = MstData["郵便番号"].ToString();
                住所１ = MstData["住所１"].ToString();
                住所２ = MstData["住所２"].ToString();
                電話番号 = MstData["電話番号"].ToString();
                ＦＡＸ番号 = MstData["ＦＡＸ"].ToString();
                振込先銀行１ = MstData["振込銀行１"].ToString();
                振込先銀行２ = MstData["振込銀行２"].ToString();
                振込先銀行３ = MstData["振込銀行３"].ToString();
                法人ナンバー = MstData["法人ナンバー"].ToString();
                自社区分 = MstData["自社区分"].ToString();
                取引先コード = MstData["取引先コード"].ToString();
                取引先コード枝番 = MstData["枝番"].ToString();
                object obj = MstData["ロゴ画像"];
                ロゴ画像 = obj == DBNull.Value ? null : (byte[])obj;
                DateTime Wk;
                削除日時 = DateTime.TryParse(tbl.Rows[0]["削除日時"].ToString(), out Wk) ? Wk : (DateTime?)null;

                ChangeKeyItemChangeable(false);
                SetFocusToTopControl();

                // 編集モード表示
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;

            }
            else
            {
                MstData = tbl.NewRow();

                ChangeKeyItemChangeable(false);
                SetFocusToTopControl();

                // 新規作成モード表示
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;

            }

        }
        #endregion

        #region リボン
        /// <summary>
        /// F1 リボン　マスタ照会
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            try
            {
                var ctl = FocusManager.GetFocusedElement(this);
                var m01Text = ViewBaseCommon.FindVisualParent<M01_TOK_TextBox>(ctl as UIElement);

                if (m01Text == null)
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

        /// <summary>
        /// F9　リボン　登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            // 登録・更新実行
            Update();

        }

        /// <summary>
        /// F10　リボン　入力取消し　
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            // メッセージボックス
            MessageBoxResult result =
                MessageBox.Show(
                        "保存せずに入力を取り消してよろしいですか？",
                        "確認",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

            // OKならクリア
            if (result != MessageBoxResult.Yes)
                return;

            this.ScreenClear();

        }

        /// <summary>
        /// F11　リボン　終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            this.Close();

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


        /// <summary>
        /// F12　リボン　削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF12Key(object sender, KeyEventArgs e)
        {
            if (MstData == null)
            {
                string msg = "登録内容がありません。";
                this.ErrorMessage = msg;
                MessageBox.Show(msg);
                return;
            }

            if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
            {
                string msg = "新規登録データは削除できません。";
                this.ErrorMessage = msg;
                MessageBox.Show(msg);
                SetFocusToTopControl();
                return;
            }

            // 削除実行
            Delete();

        }

        #endregion

        #region イベント

        /// <summary>
        /// 主キー検索時完了時に動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxExecSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    // 検索実行
                    execSearchData(0);

                }

            }
            catch (Exception ex)
            {
                appLog.Debug("【Error:{0}***{1}】", MethodBase.GetCurrentMethod().Name, ex.Message);
                this.ErrorMessage = ex.Message;
                return;

            }

        }

        /// <summary>
        /// ページングボタンが押下された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageingButton_Click(object sender, RoutedEventArgs e)
        {
            // 編集モード以外の場合はスキップ
            if (MaintenanceMode != AppConst.MAINTENANCEMODE_EDIT)
                return;

            int? option = 0;
            if (this.PageingFirst.Equals(sender))
            {   // TOP押下時
                option = -2;
            }
            else if (this.PageingPrev.Equals(sender))
            {   // PREV押下時
                option = -1;
            }
            else if (this.PageingNext.Equals(sender))
            {   // NEXT押下時
                option = 1;
            }
            else if (this.PageingLast.Equals(sender))
            {   // END押下時
                option = 2;
            }
            else
            {
                // 上記以外の場合は処理しない
                return;
            }

            // 検索実行
            execSearchData(option);

        }

        /// <summary>
        /// 登録項目の入力完了時に動作
        /// </summary>p
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Update_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Update();

            }

        }

        #region 住所情報自動設定
        /// <summary>
        /// 郵便番号変更時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YUBIN_NO_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            // 未入力時は処理しない
            if (string.IsNullOrEmpty(郵便番号))
            {
                return;
            }

            try
            {
                // 住所情報取得
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        GET_UcZip,
                        new object[] {
                            郵便番号
                        }));

            }
            catch { }

        }
        #endregion

        #region 取引先コードのフォーカスイベント群

        /// <summary>
        /// 取引先コードフォーカス取得時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void supCode_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    isFocusedSupCode = true;
        //}

        /// <summary>
        /// 取引先コードフォーカスアウト時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void supCode_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    isFocusedSupCode = false;
        //}

        /// <summary>
        /// 取引先コードマウスフォーカス取得時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void supCode_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    isFocusedSupCode = true;
        //}

        #endregion

        #endregion

        #region 処理メソッド

        /// <summary>
        /// データ検索を実行する
        /// </summary>
        /// <param name="option">
        ///   データ検索オプション
        ///     -2:先頭データ取得
        ///     -1:前データ取得
        ///      0:指定コード取得
        ///      1:次データ取得
        ///      2:最終データ取得
        /// </param>
        private void execSearchData(int? option)
        {
            // キー未設定時は処理しない
            if (string.IsNullOrEmpty(自社ID))
                return;

            try
            {
                // 検索実行
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        TargetTableNm,
                        new object[] {
                                自社ID,
                                option
                        }));

            }
            catch (Exception ex)
            {
                appLog.Debug("【Error:{0}***{1}】", MethodBase.GetCurrentMethod().Name, ex.Message);
                this.ErrorMessage = ex.Message;

            }

        }

        /// <summary>
        /// 画面編集内容の初期化をおこなう
        /// </summary>
        private void ScreenClear()
        {
            自社ID = string.Empty;
            自社名 = string.Empty;
            代表者名 = string.Empty;
            郵便番号 = string.Empty;
            住所１ = string.Empty;
            住所２ = string.Empty;
            電話番号 = string.Empty;
            ＦＡＸ番号 = string.Empty;
            振込先銀行１ = string.Empty;
            振込先銀行２ = string.Empty;
            振込先銀行３ = string.Empty;
            法人ナンバー = string.Empty;
            自社区分 = string.Empty;
            取引先コード = string.Empty;
            取引先コード枝番 = string.Empty;
            ロゴ画像 = null;
            削除日時 = null;

            this.MaintenanceMode = string.Empty;

            // キーのみtrue
            ChangeKeyItemChangeable(true);
            ResetAllValidation();

            // フォーカス設定
            SetFocusToTopControl();

        }

        /// <summary>
        /// 更新処理
        /// </summary>
        private void Update()
        {
            try
            {
                int iCompanyId = 0;
                if (string.IsNullOrEmpty(this.自社ID))
                {
                    string msg = "自社IDは入力必須項目です。";
                    this.ErrorMessage = msg;
                    MessageBox.Show(msg);
                    return;
                }
                else if (!int.TryParse(自社ID, out iCompanyId))
                {
                    string msg = "自社IDの入力内容に誤りがあります。";
                    this.ErrorMessage = msg;
                    MessageBox.Show(msg);
                    return;
                }

                // 自社区分 = 販社の場合は取引先コード必須
                if (自社区分.Equals(CompanyKbn_hansya) && (string.IsNullOrEmpty(取引先コード) || string.IsNullOrEmpty(取引先コード枝番)))
                {
                    string msg = "自社区分が『販社』の場合は取引先コードの設定が必須です。";
                    this.ErrorMessage = msg;
                    MessageBox.Show(msg);
                    return;
                }

                if (!base.CheckAllValidation())
                {
                    this.ErrorMessage = "入力内容に誤りがあります。";
                    MessageBox.Show("入力内容に誤りがあります。");
                    SetFocusToTopControl();
                    return;
                }

                string updateMessage;

                // 削除されたデータを登録する場合
                if (削除日時 != null)
                {
                    updateMessage = AppConst.RESTORE_DATA;
                }
                else
                {
                    updateMessage = "入力内容を登録しますか？";
                }

                var yesno = MessageBox.Show(updateMessage, "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (yesno != MessageBoxResult.Yes)
                    return;

                // 入力内容をDataRowに移送
                MstData["自社コード"] = iCompanyId;
                MstData["自社名"] = 自社名;
                MstData["代表者名"] = 代表者名;
                MstData["郵便番号"] = 郵便番号;
                MstData["住所１"] = 住所１;
                MstData["住所２"] = 住所２;
                MstData["電話番号"] = 電話番号;
                MstData["ＦＡＸ"] = ＦＡＸ番号;
                MstData["振込銀行１"] = 振込先銀行１;
                MstData["振込銀行２"] = 振込先銀行２;
                MstData["振込銀行３"] = 振込先銀行３;
                MstData["法人ナンバー"] = 法人ナンバー;
                MstData["自社区分"] = TryParseInteger(自社区分);
                MstData["取引先コード"] = TryParseInteger(取引先コード);
                MstData["枝番"] = TryParseInteger(取引先コード枝番);
                MstData["ロゴ画像"] = ロゴ画像;

                // 更新実行
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.UpdateData,
                        TargetTableNmUpdate,
                        new object[] {
                            MstData,
                            ccfg.ユーザID
                        }));

            }
            catch
            {
                // 更新後エラー
                this.ErrorMessage = "更新時にエラーが発生しました。";
                return;

            }

        }

        /// <summary>
        /// 削除処理
        /// </summary>
        private void Delete()
        {
            try
            {
                MessageBoxResult result =
                    MessageBox.Show("データを削除しても宜しいですか？"
                            , "確認"
                            , MessageBoxButton.YesNo
                            , MessageBoxImage.Question
                            , MessageBoxResult.No);

                if (result != MessageBoxResult.Yes)
                    return;

                base.SendRequest(
                    new CommunicationObject(
                        MessageType.UpdateData,
                        TargetTableNmDelete,
                        new object[] {
                            自社ID,
                            ccfg.ユーザID
                        }));

            }
            catch (Exception)
            {
                // 削除時エラー
                this.ErrorMessage = "削除処理時にエラーが発生しました。";
                return;

            }

        }

        /// <summary>
        /// null許容数値型への変換をおこなう
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private object TryParseInteger(string str)
        {
            int val;

            if (int.TryParse(str, out val))
                return val;

            return DBNull.Value;

        }

        /// <summary>
        /// 住所情報取得
        /// </summary>
        /// <param name="tbl"></param>
        private void ShowAddresList(DataTable tbl)
        {
            foreach (DataRow dr in tbl.Rows)
            {
                // 取得データの１件目を対象として設定
                住所１ = dr["住所漢字"].ToString();
                break;
            }

            // REMARKS:見つからなかった場合は何もしない

        }

        #region 画像処理関連

        /// <summary>
        /// クリアボタン押下時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ロゴ画像 = null;
        }

        /// <summary>
        /// 画像欄ダブルクリック時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.FileName = "";
            ofd.DefaultExt = "*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            if (ofd.ShowDialog() == true)
            {
                ChangeImageData(ofd.FileName);
            }

        }

        /// <summary>
        /// 画像変更時の処理
        /// </summary>
        /// <param name="filename"></param>
        private void ChangeImageData(string filename)
        {
            using (FileStream rdr = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    // ■ サイズの上限は、DBから取得したConfigによる
                    long max = 2 * 1024 * 1024;
                    long size = rdr.Length;
                    if (size > (max))
                    {
                        // 2MBまでとする
                        MessageBox.Show(string.Format("画像ファイルサイズが {0}MB を超えています。\r\n[{1}]", (max / 1024 / 1024), size));
                        return;
                    }
                    byte[] img = new byte[size];
                    rdr.Read(img, 0, (int)size);
                    this.ロゴ画像 = img;
                    NotifyPropertyChanged("ロゴ画像");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("画像の読み込みに失敗しました。\r\n[{0}]", ex.Message));
                }

            }

        }

        /// <summary>
        /// ファイルがドロップされた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DragDrop(object sender, DragEventArgs e)
        {
            string fileName = this.getFileNameToDragEvent(e);
            ChangeImageData(fileName);

        }

        /// <summary>
        /// 設定するファイルのパスを取得する
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private string getFileNameToDragEvent(DragEventArgs e)
        {
            string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (File.Exists(fileName[0]) == true)
            {
                return fileName[0];
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// ファイルがドロップされた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Drag_Enter(object sender, DragEventArgs e)
        {
            this.toggleEffectsToDragEvent(e);

        }

        //ドラッグで取得したものがファイルなら受け取り　ファイルではない場合破棄
        /// <summary>
        /// ファイルかどうかを判定する。
        /// ファイル以外のものがドロップされた場合は対象を破棄する。
        /// </summary>
        /// <param name="e"></param>
        private void toggleEffectsToDragEvent(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.All;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }

        }

        #endregion

        #endregion

    }

}
