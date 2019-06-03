using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using System.Data;

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;


namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 担当者マスタ入力
    /// </summary>
    public partial class MST23010 : WindowMasterMainteBase
    {
        #region 定数定義
        private const string TargetTableNm = "M72_TNT";
        private const string UpdateTable = "M72_TNT_UP";
        private const string DeleteTable = "M72_TNT_DEL";
        private const string GetNextID = "M72_TNT_NEXT";
        private const string M70_JIS = "M70_JIS_GetData";
        private const string M74_KGRP = "M74_AUTHORITY_SEL";
        #endregion

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
        public class ConfigMST23010 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST23010 frmcfg = null;

        #endregion


        #region バインド用変数

        private string _担当者ID = string.Empty;
        public string 担当者ID
        {
            get { return this._担当者ID; }
            set { this._担当者ID = value; NotifyPropertyChanged(); }
        }
        private string _担当者名 = string.Empty;
        public string 担当者名
        {
            get { return this._担当者名; }
            set { this._担当者名 = value; NotifyPropertyChanged(); }
        }
        private string _かな読み = string.Empty;
        public string かな読み
        {
            get { return this._かな読み; }
            set { this._かな読み = value; NotifyPropertyChanged(); }
        }

        private string _パスワード = string.Empty;
        public string パスワード
        {
            get { return this._パスワード; }
            set { this._パスワード = value; NotifyPropertyChanged(); }
        }
        private int? _グループ権限ID = null;
        public int? グループ権限ID
        {
            get { return this._グループ権限ID; }
            set { this._グループ権限ID = value; NotifyPropertyChanged(); }
        }
        private int? _自社コード = null;
        public int? 自社コード
        {
            get { return this._自社コード; }
            set { this._自社コード = value; NotifyPropertyChanged(); }
        }

        private string _個人ナンバー = string.Empty;
        public string 個人ナンバー
        {
            get { return this._個人ナンバー; }
            set { this._個人ナンバー = value; NotifyPropertyChanged(); }
        }

        private string _自社名 = string.Empty;
        public string 自社名
        {
            get { return this._自社名; }
            set { this._自社名 = value; NotifyPropertyChanged(); }
        }
        private DateTime? _削除日時 = null;
        public DateTime? 削除日時
        {
            get { return this._削除日時; }
            set { this._削除日時 = value; NotifyPropertyChanged(); }
        }

        private string _プログラムID = string.Empty;
        public string プログラムID
        {
            get { return this._プログラムID; }
            set { this._プログラムID = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region MST23010

        /// <summary>
        /// 担当者マスタ入力
        /// </summary>
        public MST23010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #endregion

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

            frmcfg = (ConfigMST23010)ucfg.GetConfigValue(typeof(ConfigMST23010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST23010();
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

            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { null, typeof(SCHM70_JIS) });
            base.MasterMaintenanceWindowList.Add("M72_TNT", new List<Type> { null, typeof(SCHM72_TNT) });
            base.MasterMaintenanceWindowList.Add("M74_AUTHORITY_NAME", new List<Type> { null, typeof(SCHM74_KGRPNAME) }); 

        }

        #endregion


        #region 画面初期化
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        private void ScreenClear()
        {
            担当者ID = null;
            担当者名 = string.Empty;
            かな読み = string.Empty;
            パスワード = string.Empty;
            password.PasswordText = string.Empty;
            グループ権限ID = null;
            個人ナンバー = string.Empty;
            自社コード = null;
            自社名 = string.Empty;
            削除日時 = null;


            this.MaintenanceMode = string.Empty;
            //キーのみtrue
            ChangeKeyItemChangeable(true);
            //ボタンはFalse
            btnEnableChange(true);

            SetFocusToTopControl();

            ResetAllValidation();
        }
        #endregion

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
                ViewBaseCommon.CallMasterMainte(this.MasterMaintenanceWindowList);
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
            MST23020 view = new MST23020();
            view.ShowDialog(this);
        }

        /// <summary>
        /// F9 データ登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            if (担当者ID == "999999")
            {
                this.ErrorMessage = "この担当者は変更できません。";
                return;
            }
            Update();
        }

        /// <summary>
        /// F10 入力取消
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
                this.ScreenClear();
            }
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
            if (担当者ID == "99999")
            {
                this.ErrorMessage = "この担当者は削除できません。";
                return;
            }
            if (string.IsNullOrEmpty(担当者ID))
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

            MessageBoxResult result = MessageBox.Show("表示されている情報を削除しますか？"
                             , "確認"
                             , MessageBoxButton.YesNo
                             , MessageBoxImage.Question
                             , MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
            {
                int i担当者ID = 0;
                if (!int.TryParse(担当者ID, out i担当者ID))
                {
                    this.ErrorMessage = "担当者IDの入力形式が不正です。";
                    MessageBox.Show("担当者IDの入力形式が不正です。");
                    return;
                }
                //最後尾検索
                base.SendRequest(new CommunicationObject(MessageType.UpdateData, DeleteTable, new object[] { i担当者ID,ccfg.ユーザID }));
            }

        }
        #endregion

        #region Window_Closed
        //画面が閉じられた時、データを保持する
        private void Window_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion

        #region 便利リンク

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

                    switch (message.GetMessageName())
                    {
                        //担当者データ取得
                        case TargetTableNm:
                            if (tbl.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(tbl.Rows[0]["削除日時"].ToString()))
                                {

                                    //this.ErrorMessage = "既に削除されているデータです。";
                                    //MessageBox.Show("既に削除されているデータです。");
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

                                SetTblData(tbl);

                            }

                            if (string.IsNullOrEmpty(担当者名))
                            {
                                //新規ステータス表示
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                            }
                            else
                            {
                                //編集ステータス表示
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
                            }

                            //キーをfalse
                            ChangeKeyItemChangeable(false);
                            SetFocusToTopControl();
                            break;

                        case UpdateTable:
                        case DeleteTable:
                            ScreenClear();
                            break;
                        case M70_JIS: // 自社マスタ参照
                            M70_JIS_SET(tbl);
                            break;
                        case M74_KGRP: // 権限マスタ参照
                            M74_KGRP_SET(tbl);
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
                                担当者ID = iNextCode.ToString();
                                ChangeKeyItemChangeable(false);
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                                SetFocusToTopControl();
                            }
                            break;

                        //更新処理受信
                        case UpdateTable:

                            if ((int)data == -1)
                            {
                                MessageBoxResult result = MessageBox.Show("担当者ID: " + 担当者ID + "は既に使われています。\n自動採番して登録しますか？",
                                                                                                                "質問",
                                                                                                               MessageBoxButton.YesNo,
                                                                                                               MessageBoxImage.Exclamation,
                                                                                                               MessageBoxResult.No);

                                if (result == MessageBoxResult.No)
                                {
                                    return;
                                }

                                base.SendRequest(new CommunicationObject(MessageType.UpdateData, UpdateTable, new object[] { 担当者ID
                                                                                                                            ,担当者名
                                                                                                                            ,かな読み
                                                                                                                            ,パスワード
                                                                                                                            ,グループ権限ID
                                                                                                                            ,自社コード
                                                                                                                            ,個人ナンバー
                                                                                                                            ,ccfg.ユーザID
                                                                                                                            ,this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false
                                                                                                                            ,true
                                                                                                                            }));
                                break;
                            }

                            if (data is int && (int)data == -9)
                            {
                                MessageBox.Show("グループ権限IDが'0'の人が一人は必要です。");
                                break;
                            }

                            MessageBox.Show(AppConst.COMPLETE_UPDATE);
                            //コントロール初期化
                            ScreenClear();
                            break;

                        //削除処理受信
                        case DeleteTable:
                            if (data is int && (int)data == -9)
                            {
                                MessageBox.Show("グループ権限IDが'0'の人が一人は必要です。");
                                break;
                            }
                            MessageBox.Show(AppConst.COMPLETE_DELETE);
                            //コントロール初期化
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

        #region SetTblData

        /// <summary>
        /// テーブルデータを各変数に代入
        /// </summary>
        /// <param name="tbl"></param>
        private void SetTblData(DataTable tbl)
        {
            担当者ID = tbl.Rows[0]["担当者ID"].ToString();
            担当者名 = tbl.Rows[0]["担当者名"].ToString();
            かな読み = tbl.Rows[0]["かな読み"].ToString();
            パスワード = tbl.Rows[0]["パスワード"].ToString();
            グループ権限ID = tbl.Rows[0]["グループ権限ID"].ToString() == string.Empty ? (int?)null : AppCommon.IntParse(tbl.Rows[0]["グループ権限ID"].ToString());
            自社コード = tbl.Rows[0]["自社コード"].ToString() == string.Empty ? (int?)null : AppCommon.IntParse(tbl.Rows[0]["自社コード"].ToString());
            自社名 = tbl.Rows[0]["自社名"].ToString();
            個人ナンバー = tbl.Rows[0]["個人ナンバー"].ToString();
            DateTime Wk;
            string str削除日時 = tbl.Rows[0]["削除日時"].ToString();
            削除日時 = str削除日時 == null ? (DateTime?)null : DateTime.TryParse(str削除日時.ToString(), out Wk) ? Wk : (DateTime?)null;
        }

        #endregion

        #region コード送りボタン
        /// <summary>
        /// 最初のIDを表示するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FistIdButton_Click(object sender, RoutedEventArgs e)
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
                //前データ検索
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 担当者ID, -1 }));
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
            try
            {
                if (担当者ID == null)
                {
                    //次データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 担当者ID, 0 }));
                }
                else
                {
                    //次データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 担当者ID, 1 }));
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

        #region 自動採番

        /// <summary>
        /// 担当者コードキーダウンイベント時
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

                        if (string.IsNullOrEmpty(担当者ID))
                        {
                            //自動採番
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetNextID, new object[] { }));

                            return;
                        }

                        int i担当者ID = 0;
                        if (!int.TryParse(担当者ID, out i担当者ID))
                        {
                            this.ErrorMessage = "担当者IDの入力形式が不正です。";
                            MessageBox.Show("担当者IDの入力形式が不正です。");
                            return;
                        }

                        //担当者データ検索
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 担当者ID, 0 }));

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

        #region ボタン

        /// <summary>
        /// 前後ボタンのEnableを変更する。
        /// </summary>
        /// <param name="pBool"></param>
        private void btnEnableChange(bool pBool)
        {

            FistIdButton.IsEnabled = pBool;
            BeforeIdButton.IsEnabled = pBool;
            NextIdButton.IsEnabled = pBool;
            LastIdButoon.IsEnabled = pBool;
        }

        #endregion

        #region 登録

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            try
            {

                int i担当者ID = 0;
                string updateMessage;

                if (string.IsNullOrEmpty(担当者ID))
                {
                    this.ErrorMessage = "担当者IDは必須入力項目です。";
                    MessageBox.Show("担当者IDは必須入力項目です。");
                    return;
                }

                if (string.IsNullOrEmpty(担当者名))
                {
                    this.ErrorMessage = "担当者名は入力必須項目です。";
                    MessageBox.Show("担当者名は入力必須項目です。");
                    return;
                }

                if (!base.CheckAllValidation())
                {
                    this.ErrorMessage = "入力内容に誤りがあります。";
                    MessageBox.Show("入力内容に誤りがあります。");
                    SetFocusToTopControl();
                    return;
                }


                if (int.TryParse(担当者ID, out i担当者ID))
                {
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
                    if (yesno == MessageBoxResult.Yes)
                    {
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, UpdateTable, new object[] { i担当者ID
                                                                                                                    ,担当者名
                                                                                                                    ,かな読み
                                                                                                                    ,パスワード
                                                                                                                    ,グループ権限ID
                                                                                                                    ,自社コード
                                                                                                                    ,個人ナンバー
                                                                                                                    ,ccfg.ユーザID
                                                                                                                    ,this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false
                                                                                                                    ,false
                                                                                                                    }));
                    }
                    else
                    {
                        return;
                    }
                }

            }
            catch (Exception)
            {
                this.ErrorMessage = "更新処理に失敗しました";
                return;
            }

        }
        #endregion

        #region KeyDownイベント(UPDATE)

        /// 最終項目エンター
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcLabelTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Update();
            }
        }

        #endregion

        //#region 自社コードEnter時の自社マスタ呼び出し
        //private void TwinTxt自社コード_PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    if ((e.Key == Key.Enter) ||
        //        (e.Key == Key.Tab))
        //    {
        //        // 自社マスタ呼び出し
        //        if (自社コード != null)
        //        {
        //            string JisyaID = 自社コード.Value.ToString();
        //            base.SendRequest(new CommunicationObject(MessageType.RequestData, M70_JIS, JisyaID, 0));
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //}
        //#endregion

        #region 自社マスタ取得

        /// <summary>
        /// 自社情報バインディング
        /// </summary>
        /// <param name="tbl"></param>
        void M70_JIS_SET(DataTable tbl)
        {
            if (tbl == null || tbl.Rows.Count == 0)
            {
                // 自社マスタにデータが存在しない場合は項目を初期化
                自社名 = string.Empty;
            }
            else
            {
                // 自社マスタにデータが存在すれば画面に反映
                自社名 = tbl.Rows[0]["自社名"].ToString();
            }
        }

        #endregion

        #region 自社マスタ取得

        /// <summary>
        /// 自社情報バインディング
        /// </summary>
        /// <param name="tbl"></param>
        void M74_KGRP_SET(DataTable tbl)
        {
            if (tbl == null || tbl.Rows.Count == 0)
            {
                // 自社マスタにデータが存在しない場合は項目を初期化
                プログラムID = string.Empty;
            }
            else
            {
                // 自社マスタにデータが存在すれば画面に反映
                プログラムID = tbl.Rows[0]["プログラムID"].ToString();
            }
        }

        #endregion

        //#region グループ権限ID Enter時の権限マスタ呼び出し
        //private void TwinTxtグループ権限ID_PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    if ((e.Key == Key.Enter) ||
        //        (e.Key == Key.Tab))
        //    {
        //        // 権限マスタ呼び出し
        //        if (グループ権限ID != null)
        //        {
        //            int? kgrpID = グループ権限ID.Value;
        //            base.SendRequest(new CommunicationObject(MessageType.RequestData, M74_KGRP, kgrpID));
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //}
        //#endregion

    }
}
