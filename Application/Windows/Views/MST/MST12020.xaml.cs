using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.Controls;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;


namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 倉庫マスタ入力
    /// </summary>
    public partial class MST12020 : WindowMasterMainteBase
    {
        public enum 自社区分 : int
        {
            自社 = 0,
            販社 = 1
        }

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
        public class ConfigSHR08010 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigSHR08010 frmcfg = null;

        #endregion

        #region 定数定義
        private const string TargetTableNm = "M22_SOUK";
        private const string UpdateTable = "M22_SOUK_UP";
        private const string DeleteTable = "M22_SOUK_DEL";
        private const string GetNextID = "M22_SOUK_NEXT";

        private const string M70_JIS_GetData = "M70_JIS_GetData";

        #endregion

        #region バインド用変数
        private string _倉庫コード = string.Empty;
        public string 倉庫コード
        {
            get { return this._倉庫コード; }
            set { this._倉庫コード = value; NotifyPropertyChanged(); }
        }
        private string _倉庫名 = string.Empty;
        public string 倉庫名
        {
            get { return this._倉庫名; }
            set { this._倉庫名 = value; NotifyPropertyChanged(); }
        }
        private string _略称名 = string.Empty;
        public string 略称名
        {
            get { return this._略称名; }
            set { this._略称名 = value; NotifyPropertyChanged(); }
        }
        private string _かな読み = string.Empty;
        public string かな読み
        {
            get { return this._かな読み; }
            set { this._かな読み = value; NotifyPropertyChanged(); }
        }
        private string _場所会社 = string.Empty;
        public string 場所会社
        {
            get { return _場所会社; }
            set { this._場所会社 = value; NotifyPropertyChanged(); }
        }
        private string _寄託会社 = string.Empty;
        public string 寄託会社
        {
            get { return _寄託会社; }
            set { this._寄託会社 = value; NotifyPropertyChanged(); }
        }
        private DateTime? _削除日時 = null;
        public DateTime? 削除日時
        {
            get { return this._削除日時; }
            set { this._削除日時 = value; NotifyPropertyChanged(); }
        }

        //マスタデータ
        private DataRow _MstData;
        public DataRow MstData
        {
            get { return this._MstData; }
            set { this._MstData = value; NotifyPropertyChanged(); }
        }
        #endregion

        /// <summary>
        /// イベントのバインド状態
        /// </summary>
        private bool isEventBinding = false;

        /// <summary>
        /// 倉庫マスタ入力
        /// </summary>
        public MST12020()
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
            frmcfg = (ConfigSHR08010)ucfg.GetConfigValue(typeof(ConfigSHR08010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigSHR08010();
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

            base.MasterMaintenanceWindowList.Add("M22_SOUK", new List<Type> { null, typeof(SCHM22_SOUK) });
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });

        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        private void ScreenClear()
        {
            MstData = null;
            倉庫コード = string.Empty;
            倉庫名 = string.Empty;
            略称名 = string.Empty;
            かな読み = string.Empty;
            場所会社 = string.Empty;
            寄託会社 = string.Empty;
            削除日時 = null;

            this.MaintenanceMode = string.Empty;
            // キーのみtrue
            ChangeKeyItemChangeable(true);
            // ボタンはFalse
            btnEnableChange(true);

            // 場所会社変更イベントを除外
            SetEventBinding(false);

            SetFocusToTopControl();
            ResetAllValidation();

        }

        #region リボン

        /// <summary>
        /// F1 マスタ検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF1Key(object sender, KeyEventArgs e)
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

        /// <summary>
        /// F2 マスタメンテ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF2Key(object sender, KeyEventArgs e)
        {
            try
            {


                //ViewBaseCommon.CallMasterMainte(this.MasterMaintenanceWindowList);
            }
            catch (Exception ex)
            {
                appLog.Error("マスターメンテ画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }
        }

        /// <summary>
        /// F8 リスト一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            MST12020_1 view = new MST12020_1();
            view.ShowDialog(this);
        }

        /// <summary>
        /// F9 データ登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            Update();
        }

        /// <summary>
        /// F10 入力取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            MessageBoxResult result =
                MessageBox.Show(AppConst.CONFIRM_CANCEL,
                        "確認",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
                this.ScreenClear();

        }

        /// <summary>
        /// F11 終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// F12　削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF12Key(object sender, KeyEventArgs e)
        {

            if (string.IsNullOrEmpty(倉庫コード))
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
                MessageBox.Show(AppConst.CONFIRM_DELETE
                             , "確認"
                             , MessageBoxButton.YesNo
                             , MessageBoxImage.Question
                             , MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
            {
                Delete();
            }

        }

        #endregion

        #region  便利リンク
        /// <summary>
        /// リボン便利リンク　検索ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Kensaku_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.yahoo.co.jp/");
        }

        /// <summary>
        /// リボン便利リンク　道路情報ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DouroJyouhou_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.jartic.or.jp/");

        }

        /// <summary>
        /// リボン便利リンク　道路ナビボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DouroNabi_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://highway.drivenavi.net/");
        }

        /// <summary>
        /// リボン便利リンク　渋滞情報ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JyuutaiJyouhou_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.mapfan.com/");
        }

        /// <summary>
        /// リボン便利リンク　天気ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tenki_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://weathernews.jp/");
        }

        /// <summary>
        /// リボン　WebHomeボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonButton_WebHome_Click_1(object sender, RoutedEventArgs e)
        {
            Process Pro = new Process();

            try
            {
                Pro.StartInfo.UseShellExecute = false;
                Pro.StartInfo.FileName = "C:\\Program Files (x86)/Internet Explorer/iexplore.exe";
                Pro.StartInfo.CreateNoWindow = true;
                Pro.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// リボン　メールボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonButton_Meil_Click_1(object sender, RoutedEventArgs e)
        {
            Process Pro = new Process();

            try
            {
                Pro.StartInfo.UseShellExecute = false;
                Pro.StartInfo.FileName = "C://Program Files (x86)//Windows Live//Mail//wlmail.exe";
                Pro.StartInfo.CreateNoWindow = true;
                Pro.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// リボン　電卓ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonButton_Dentaku_Click_1(object sender, RoutedEventArgs e)
        {
            Process Pro = new Process();

            try
            {
                Pro.StartInfo.UseShellExecute = false;
                Pro.StartInfo.FileName = "C://Windows//System32/calc.exe";
                Pro.StartInfo.CreateNoWindow = true;
                Pro.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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

                if (data is DataTable)
                {
                    DataTable tbl = data as DataTable;
                    bool isEnableControl = true;

                    switch (message.GetMessageName())
                    {
                        // 倉庫データ取得
                        case TargetTableNm:
                            if (tbl.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(tbl.Rows[0]["削除日時"].ToString()))
                                {
                                    MessageBoxResult result =
                                        MessageBox.Show("既に削除されているデータです。\nこのコードを復元しますか？",
                                        "質問",
                                        MessageBoxButton.YesNo,
                                        MessageBoxImage.Exclamation,
                                        MessageBoxResult.No);

                                    if (result == MessageBoxResult.No)
                                        return;

                                }

                                // 場所会社変更時イベントを削除(追加済の場合)
                                SetEventBinding(false);

                                SetTblData(tbl);
                                MstData = tbl.Rows[0];

                            }

                            if (string.IsNullOrEmpty(倉庫名))
                            {
                                // 新規ステータス表示
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                            }
                            else
                            {
                                // 編集ステータス表示
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;

                                // コードの状態により使用可否を設定
                                isEnableControl = ((int)MstData["場所会社自社区分"]) == (int)自社区分.自社;

                            }


                            // キーをfalse
                            ChangeKeyItemChangeable(false);
                            ttxt寄託会社.IsEnabled = isEnableControl;
                            SetFocusToTopControl();

                            // 場所会社変更時イベントを追加
                            SetEventBinding(true);

                            break;

                        case M70_JIS_GetData:
                            // 場所会社コード変更時
                            if (tbl == null || tbl.Rows.Count == 0)
                            {
                                ttxt寄託会社.IsEnabled = true;
                                ttxt寄託会社.Text1 = string.Empty;
                            }
                            else
                            {
                                var myKbn = int.Parse(tbl.Rows[0]["自社区分"].ToString());
                                ttxt寄託会社.IsEnabled = myKbn.Equals(0);
                                寄託会社 = 場所会社;
                            }
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    switch (message.GetMessageName())
                    {
                        case GetNextID:
                            if (data is int)
                            {
                                int iNextCode = (int)data;
                                倉庫コード = iNextCode.ToString();
                                ChangeKeyItemChangeable(false);
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                                SetFocusToTopControl();
                            }
                            break;
                        case UpdateTable:
                            switch (data.GetHashCode())
                            {
                                case -1:
                                    // 利用済コード判定時
                                    string msg = string.Format("倉庫コード: {0} は既に使用されています。\n自動採番して登録しますか？", 倉庫コード);
                                    MessageBoxResult result =
                                        MessageBox.Show(msg, "質問", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);
                                    if (result == MessageBoxResult.No)
                                        return;

                                    int i倉庫コード = AppCommon.IntParse(倉庫コード);

                                    base.SendRequest(
                                        new CommunicationObject(
                                            MessageType.UpdateData,
                                            UpdateTable,
                                            new object[] {
                                            i倉庫コード,
                                            倉庫名,
                                            略称名,
                                            かな読み,
                                            ccfg.ユーザID,
                                            this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false,
                                            true
                                        }));

                                    break;

                                case -9:
                                    // 場所会社コード、寄託会社コードの組み合わせが既に登録されている場合
                                    MessageBox.Show("指定された場所会社、寄託会社の組み合わせは既に登録済みです。", "登録エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;

                            }

                            MessageBox.Show(AppConst.COMPLETE_UPDATE);
                            // コントロール初期化
                            ScreenClear();
                            break;

                        case DeleteTable:
                            MessageBox.Show(AppConst.COMPLETE_DELETE);
                            // コントロール初期化
                            ScreenClear();
                            break;

                        default:
                            break;

                    }

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

        /// <summary>
        /// テーブルデータを各変数に代入
        /// </summary>
        /// <param name="tbl"></param>
        private void SetTblData(DataTable tbl)
        {
            DataRow dr = tbl.Rows[0];
            倉庫コード = dr["倉庫コード"].ToString();
            倉庫名 = dr["倉庫名"].ToString();
            略称名 = dr["略称名"].ToString();
            かな読み = dr["かな読み"].ToString();
            場所会社 = dr["場所会社コード"].ToString();
            寄託会社 = dr["寄託会社コード"].ToString();
            DateTime Wk;
            string str削除日時 = dr["削除日時"].ToString();
            削除日時 = string.IsNullOrEmpty(str削除日時) ? (DateTime?)null : DateTime.TryParse(str削除日時, out Wk) ? Wk : (DateTime?)null;

        }

        #region データ前後ボタン

        /// <summary>
        /// 最初のIDを表示するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstIdButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                //先頭データ検索
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 0 }));
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 前のIDを表示するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeforeIdButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(倉庫コード))
                {
                    //先頭データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 0 }));
                    return;
                }

                int iKeyCD = 0;
                iKeyCD = AppCommon.IntParse(this.倉庫コード);
                //前データ検索
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iKeyCD, -1 }));
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 次データを検索する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextIdButton_Click(object sender, RoutedEventArgs e)
        {
            int iKeyCD = 0;
            try
            {
                if (string.IsNullOrEmpty(倉庫コード))
                {
                    iKeyCD = 0;
                    //次データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iKeyCD, 1 }));

                }
                else
                {
                    iKeyCD = AppCommon.IntParse(this.倉庫コード);
                    //次データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iKeyCD, 1 }));
                }

            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 最後のデータを検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastIdButoon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //最後尾検索
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 1 }));
            }
            catch (Exception)
            {
                return;
            }
        }
        #endregion

        /// <summary>
        /// 場所会社イベントの追加・削除をおこなう
        /// </summary>
        /// <param name="isBinding">バインド指示(真：追加、偽：削除)</param>
        private void SetEventBinding(bool isBinding)
        {
            switch (isBinding)
            {
                case true:
                    // バインド指定
                    if (!isEventBinding)
                    {
                        ttxt場所会社.cText1Changed += ttxt場所会社_cText1Changed;
                        isEventBinding = isBinding;
                    }
                    break;

                case false:
                    // アンバインド指定
                    if (isEventBinding)
                    {
                        ttxt場所会社.cText1Changed -= ttxt場所会社_cText1Changed;
                        isEventBinding = isBinding;
                    }
                    break;

            }

        }

        /// <summary>
        /// 倉庫コードキーダウンイベント時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcLabelTwinTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {

                    try
                    {

                        if (string.IsNullOrEmpty(倉庫コード))
                        {
                            // 自動採番
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetNextID, new object[] { }));

                            return;
                        }

                        int i倉庫コード = 0;

                        if (!int.TryParse(倉庫コード, out i倉庫コード))
                        {
                            string msg = "倉庫コードの入力形式が不正です。";
                            this.ErrorMessage = msg;
                            MessageBox.Show(msg);
                            return;
                        }

                        // 倉庫データ検索
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { i倉庫コード, 0 }));


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


        /// <summary>
        /// 前後ボタンのEnableを変更する。
        /// </summary>
        /// <param name="pBool"></param>
        private void btnEnableChange(bool pBool)
        {
            FirstIdButton.IsEnabled = pBool;
            BeforeIdButton.IsEnabled = pBool;
            NextIdButton.IsEnabled = pBool;
            LastIdButoon.IsEnabled = pBool;

        }

        #region 登録

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            try
            {

                int i倉庫コード = 0, i寄託会社 = 0, i場所会社 = 0;
                string updateMessage;

                if (!int.TryParse(倉庫コード, out i倉庫コード))
                {
                    string msg1 = "倉庫コードの入力形式が不正です。";
                    this.ErrorMessage = msg1;
                    MessageBox.Show(msg1);
                    return;
                }

                if (string.IsNullOrEmpty(倉庫名))
                {
                    string msg2 = "倉庫名は入力必須項目です。";
                    this.ErrorMessage = msg2;
                    MessageBox.Show(msg2);
                    return;
                }

                if (string.IsNullOrEmpty(this.ttxt場所会社.Text1))
                {
                    string msg3 = "場所会社は必須入力項目です。";
                    this.ErrorMessage = msg3;
                    MessageBox.Show(msg3);
                    return;
                }
                else if (!int.TryParse(場所会社, out i場所会社))
                {
                    string msg4 = "場所会社の入力形式が不正です。";
                    this.ErrorMessage = msg4;
                    MessageBox.Show(msg4);
                    return;
                }

                if (string.IsNullOrEmpty(this.ttxt寄託会社.Text1))
                {
                    string msg3 = "寄託会社は必須入力項目です。";
                    this.ErrorMessage = msg3;
                    MessageBox.Show(msg3);
                    return;
                }
                else if (!int.TryParse(寄託会社, out i寄託会社))
                {
                    string msg4 = "寄託会社の入力形式が不正です。";
                    this.ErrorMessage = msg4;
                    MessageBox.Show(msg4);
                    return;
                }

                if (!base.CheckAllValidation())
                {
                    string msg5 = "入力内容に誤りがあります。";
                    this.ErrorMessage = msg5;
                    MessageBox.Show(msg5);
                    SetFocusToTopControl();
                    return;
                }

                // 削除されたデータを登録する場合
                if (削除日時 != null)
                {
                    updateMessage = AppConst.RESTORE_DATA;
                }
                else
                {
                    updateMessage = AppConst.CONFIRM_UPDATE;
                }


                var yesno = MessageBox.Show(updateMessage, "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (yesno == MessageBoxResult.No)
                    return;

                base.SendRequest(
                    new CommunicationObject(
                        MessageType.UpdateData,
                        UpdateTable,
                        new object[] {
                            i倉庫コード,
                            倉庫名,
                            略称名,
                            かな読み,
                            i場所会社,
                            i寄託会社,
                            ccfg.ユーザID,
                            this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false,
                            false
                        }));

            }
            catch (Exception)
            {
                this.ErrorMessage = AppConst.FAILED_UPDATE;
                return;
            }

        }
        #endregion

        #region 削除
        /// <summary>
        /// Update
        /// </summary>
        private void Delete()
        {
            try
            {
                int i倉庫コード = 0;

                if (!int.TryParse(倉庫コード, out i倉庫コード))
                {
                    string msg = "倉庫コードの入力形式が不正です。";
                    this.ErrorMessage = msg;
                    MessageBox.Show(msg);
                    return;
                }

                // 最後尾検索
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.UpdateData,
                        DeleteTable,
                        new object[] {
                            i倉庫コード,
                            ccfg.ユーザID
                        }));

            }
            catch (Exception)
            {
                return;
            }
        }
        #endregion

        /// <summary>
        /// 最終エンター
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcLabelTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Update();

        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            // 画面が閉じられた時、データを保持する
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);

        }

        /// <summary>
        /// 場所会社コードが変更された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ttxt場所会社_cText1Changed(object sender, RoutedEventArgs e)
        {
            UcLabelTwinTextBox tb = sender as UcLabelTwinTextBox;

            if (string.IsNullOrEmpty(tb.Text1))
                return;

            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    M70_JIS_GetData,
                    new object[] {
                        tb.Text1,
                        0
                    }));

        }

    }

}
