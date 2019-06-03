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
    /// <summary>
    /// 摘要マスタ入力
    /// </summary>
    public partial class MST08010 : WindowMasterMainteBase
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
        public class ConfigSHR08010 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigSHR08010 frmcfg = null;

        #endregion

        #region 定数定義
        private const string TargetTableNm = "M11_TEK";
        //private const string RTargetTableNm = "RM11_TEK";
        private const string UpdateTable = "M11_TEK_UP";
        private const string DeleteTable = "M11_TEK_DEL";
        private const string GetNextID = "M11_TEK_NEXT";
        #endregion

        #region バインド用変数
        private string _摘要コード = string.Empty;
        public string 摘要コード
        {
            get { return this._摘要コード; }
            set { this._摘要コード = value; NotifyPropertyChanged(); }
        }
        private string _摘要名 = string.Empty;
        public string 摘要名
        {
            get { return this._摘要名; }
            set { this._摘要名 = value; NotifyPropertyChanged(); }
        }
        private string _かな読み = string.Empty;
        public string かな読み
        {
            get { return this._かな読み; }
            set { this._かな読み = value; NotifyPropertyChanged(); }
        }
        private DateTime? _削除日時 = null;
        public DateTime? 削除日時
        {
            get { return this._削除日時; }
            set { this._削除日時 = value; NotifyPropertyChanged(); }
        }
        #endregion

        /// <summary>
        /// 摘要マスタ入力
        /// </summary>
        public MST08010()
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

            ///base.MasterMaintenanceWindowList.Add("M11_TEK_UC", new List<Type> { null, typeof(SCH08010) });
            base.MasterMaintenanceWindowList.Add("M11_TEK", new List<Type> { null, typeof(SCHM11_TEK) });
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        private void ScreenClear()
        {
            摘要コード = string.Empty;
            摘要名 = string.Empty;
            かな読み = string.Empty;
            削除日時 = null;

            this.MaintenanceMode = string.Empty;
            //キーのみtrue
            ChangeKeyItemChangeable(true);
            //ボタンはFalse
            btnEnableChange(true);

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
            MST08020 view = new MST08020();
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

            if (string.IsNullOrEmpty(摘要コード))
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

                    switch (message.GetMessageName())
                    {
                        //摘要データ取得
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

                            if (string.IsNullOrEmpty(摘要名))
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
                                摘要コード = iNextCode.ToString();
                                ChangeKeyItemChangeable(false);
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                                SetFocusToTopControl();
                            }
                            break;
                        // 更新処理受信
                        case UpdateTable:
                            if ((int)data == -1)
                            {
                                MessageBoxResult result = MessageBox.Show("摘要コード: " + 摘要コード + "は既に使われています。\n自動採番して登録しますか？",
                                                                                                                "質問",
                                                                                                               MessageBoxButton.YesNo,
                                                                                                               MessageBoxImage.Exclamation,
                                                                                                               MessageBoxResult.No);

                                if (result == MessageBoxResult.No)
                                {
                                    return;
                                }

                                int i摘要コード = AppCommon.IntParse(摘要コード);

                                base.SendRequest(new CommunicationObject(MessageType.UpdateData, UpdateTable, new object[] { i摘要コード
                                                                                                                            ,摘要名
                                                                                                                            ,かな読み
                                                                                                                            ,ccfg.ユーザID
                                                                                                                            ,this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false
                                                                                                                            ,true}));

                                break;
                            }
                            MessageBox.Show(AppConst.COMPLETE_UPDATE);
                            //コントロール初期化
                            ScreenClear();
                            break;
                        //削除処理受信
                        case DeleteTable:
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

        /// <summary>
        /// テーブルデータを各変数に代入
        /// </summary>
        /// <param name="tbl"></param>
        private void SetTblData(DataTable tbl)
        {
            摘要コード = tbl.Rows[0]["摘要ID"].ToString();
            摘要名 = tbl.Rows[0]["摘要名"].ToString();
            かな読み = tbl.Rows[0]["かな読み"].ToString();
            DateTime Wk;
            string str削除日時 = tbl.Rows[0]["削除日時"].ToString();
            削除日時 = str削除日時 == null ? (DateTime?)null : DateTime.TryParse(str削除日時.ToString(), out Wk) ? Wk : (DateTime?)null;

        }

        #region データ前後ボタン

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
                if (string.IsNullOrEmpty(摘要コード))
                {
                    //先頭データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 0 }));
                    return;
                }

                int iKeyCD = 0;
                iKeyCD = AppCommon.IntParse(this.摘要コード);
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
                if (string.IsNullOrEmpty(摘要コード))
                {
                    iKeyCD = 0;
                    //次データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iKeyCD, 1 }));

                }
                else
                {
                    iKeyCD = AppCommon.IntParse(this.摘要コード);
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
        /// 摘要コードキーダウンイベント時
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

                        if (string.IsNullOrEmpty(摘要コード))
                        {
                            //自動採番
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetNextID, new object[] { }));

                            return;
                        }

                        int i摘要コード = 0;

                        if (!int.TryParse(摘要コード, out i摘要コード))
                        {
                            this.ErrorMessage = "摘要IDの入力形式が不正です。";
                            MessageBox.Show("摘要IDの入力形式が不正です。");
                            return;
                        }


                        //摘要データ検索
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { i摘要コード, 0 }));


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
            FistIdButton.IsEnabled = pBool;
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

                int i摘要コード = 0;
                string updateMessage;

                if (!int.TryParse(摘要コード, out i摘要コード))
                {
                    this.ErrorMessage = "摘要IDの入力形式が不正です。";
                    MessageBox.Show("摘要IDの入力形式が不正です。");
                    return;
                }

                if (string.IsNullOrEmpty(摘要名))
                {
                    this.ErrorMessage = "摘要名は入力必須項目です。";
                    MessageBox.Show("摘要名は入力必須項目です。");
                    return;
                }
                if (!base.CheckAllValidation())
                {
                    this.ErrorMessage = "入力内容に誤りがあります。";
                    MessageBox.Show("入力内容に誤りがあります。");
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
                    updateMessage = "入力内容を登録しますか？";
                }


                var yesno = MessageBox.Show(updateMessage, "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (yesno == MessageBoxResult.Yes)
                {
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, UpdateTable, new object[] { i摘要コード
                                                                                                                ,摘要名
                                                                                                                ,かな読み
                                                                                                                ,ccfg.ユーザID
                                                                                                                ,this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false
                                                                                                                ,false}));
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {
                this.ErrorMessage = "更新処理に失敗しました";
                return;
            }

        }
        #endregion

        #region 削除
        /// <summary>
        /// Delete
        /// </summary>
        private void Delete()
        {
            try
            {
                int i摘要コード = 0;

                if (!int.TryParse(摘要コード, out i摘要コード))
                {
                    this.ErrorMessage = "摘要IDの入力形式が不正です。";
                    MessageBox.Show("摘要IDの入力形式が不正です。");
                    return;
                }

                //最後尾検索
                base.SendRequest(new CommunicationObject(MessageType.UpdateData, DeleteTable, new object[] { i摘要コード,ccfg.ユーザID }));
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
            {
                Update();
            }
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            //画面が閉じられた時、データを保持する
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }
    }
}
