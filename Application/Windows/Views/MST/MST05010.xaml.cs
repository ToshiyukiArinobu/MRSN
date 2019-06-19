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
    using WinFormsScreen = System.Windows.Forms.Screen;

    /// <summary>
    /// 色マスタ入力画面クラス
    /// </summary>
    public partial class MST05010 : WindowMasterMainteBase
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
        public class ConfigMST05010 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST05010 frmcfg = null;

        #endregion

		#region 定数
        /// <summary>データ検索</summary>
		private const string TargetTableNm = "M06_IRO_GetData";
        /// <summary>データ登録・更新</summary>
        private const string TargetTableNmUpdate = "M06_IRO_Update";
        /// <summary>データ削除</summary>
        private const string TargetTableNmDelete = "M06_IRO_Delete";

        #endregion

        #region 表示用データ

        private string _色コード = string.Empty;
        public string 色コード
        {
            get { return this._色コード; }
            set { this._色コード = value; NotifyPropertyChanged(); }
        }

        private string _色名称 = string.Empty;
        public string 色名称
        {
            get { return this._色名称; }
            set { this._色名称 = value; NotifyPropertyChanged(); }
        }

        private DateTime? _削除日時 = null;
        public DateTime? 削除日時
        {
            get { return this._削除日時; }
            set { this._削除日時 = value; NotifyPropertyChanged(); }
        }

        private DataRow _MstData;
        public DataRow MstData
        {
            get { return this._MstData; }
            set { this._MstData = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region MST05010
        /// <summary>
        /// 色マスタ コンストラクタ
        /// </summary>
        public MST05010()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion

        /// <summary>
        /// Loadイベント
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
            frmcfg = (ConfigMST05010)ucfg.GetConfigValue(typeof(ConfigMST05010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST05010();
                ucfg.SetConfigValue(frmcfg);
            }
            else
            {
                //表示できるかチェック
                var WidthCHK = WinFormsScreen.PrimaryScreen.Bounds.Width - frmcfg.Left;
                if (WidthCHK > 10)
                {
                    this.Left = frmcfg.Left;
                }
                //表示できるかチェック
                var HeightCHK = WinFormsScreen.PrimaryScreen.Bounds.Height - frmcfg.Top;
                if (HeightCHK > 10)
                {
                    this.Top = frmcfg.Top;
                }
                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;
            }
            #endregion

            ScreenClear();
            base.MasterMaintenanceWindowList.Add("M06_IRO", new List<Type> { null, typeof(SCHM06_IRO) });
        }

        #region データ受信
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
                        case TargetTableNm:
                            // 通常データ検索
                            StrData(tbl);
                            break;

                        default:
                            break;

                    }

                }
                else
                {

                    switch (message.GetMessageName())
                    {
                        //更新処理受信
                        case TargetTableNmUpdate:

                            if ((int)data == -1)
                            {
                                // 入力値を設定
                                MstData["色コード"] = 色コード;
                                MstData["色名称"] = 色名称;

                                base.SendRequest(new CommunicationObject(
                                    MessageType.UpdateData,
                                    TargetTableNmUpdate,
                                    new object[] {
                                        MstData,
                                        ccfg.ユーザID
                                    }));
                                break;
                            }
                            MessageBox.Show(AppConst.COMPLETE_UPDATE);
                            //コントロール初期化
                            ScreenClear();
                            break;

                        //削除処理受信
                        case TargetTableNmDelete:
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
        /// 受信データ設定
        /// </summary>
        /// <param name="tbl"></param>
        private void StrData(DataTable tbl)
        {
            // データがある場合
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

                // 表示データ
                MstData = tbl.Rows[0];

                色コード = MstData["色コード"].ToString();
                色名称 = MstData["色名称"].ToString();
                DateTime Wk;
                削除日時 = DateTime.TryParse(tbl.Rows[0]["削除日時"].ToString(), out Wk) ? Wk : (DateTime?)null;

                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
                ChangeKeyItemChangeable(false);
                SetFocusToTopControl();

            }
            else
            {
                // 表示データインスタンス生成
                MstData = tbl.NewRow();

                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
				ChangeKeyItemChangeable(false);
                SetFocusToTopControl();

            }

        }

		#endregion

        #region エラー表示用
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            MessageBox.Show(ErrorMessage);
        }
        #endregion

        #region << <> >>ボタン
        /// <summary>
        /// ページングボタンが押下された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageingButton_Click(object sender, RoutedEventArgs e)
        {
            int? option = 0;

            // 画面モードが編集モード時のみ動作
            if (this.MaintenanceMode.Equals(AppConst.MAINTENANCEMODE_EDIT))
            {
                if (FistIdButton.Equals(sender))
                {   // TOP押下時
                    option = -2;
                }
                else if (BeforeIdButton.Equals(sender))
                {   // PREV押下時
                    option = -1;
                }
                else if (NextIdButton.Equals(sender))
                {   // NEXT押下時
                    option = 1;
                }
                else if (LastIdButoon.Equals(sender))
                {   // END押下時
                    option = 2;
                }
                else
                {
                    // 上記以外の場合は処理しない
                    return;
                }

                try
                {
                    if (!string.IsNullOrEmpty(色コード))
                    {
                        // 存在する商品群マスタ検索
                        base.SendRequest(new CommunicationObject(
                            MessageType.RequestData,
                            TargetTableNm,
                            new object[] { 色コード, option }));

                    }

                }
                catch (Exception ex)
                {
                    appLog.Debug("【Error:{0}***{1}】", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                    this.ErrorMessage = ex.Message;

                }

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
                ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);
            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }

        }

        /// <summary>
        /// F8 リボン　リスト一覧表　
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            MST05010_1 mst05010_1 = new MST05010_1();
            mst05010_1.ShowDialog(this);

        }

        /// <summary>
        /// F9　リボン　登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
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
            MessageBoxResult result = MessageBox.Show("保存せずに入力を取り消しても宜しいですか？"
                            , "確認"
                            , MessageBoxButton.YesNo
                            , MessageBoxImage.Question);
            // OKならクリア
            if (result == MessageBoxResult.Yes)
            {
                this.ScreenClear();
            }

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

        /// <summary>
        /// F12　リボン　削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF12Key(object sender, KeyEventArgs e)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(this.色コード))
            {
                msg = "登録内容がありません。";
                this.ErrorMessage = msg;
                MessageBox.Show(msg);
                return;
            }

            if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
            {
                msg = "新規登録データは削除できません。";
                this.ErrorMessage = msg;
                MessageBox.Show(msg);
                SetFocusToTopControl();
                return;
            }

            MessageBoxResult result =
                MessageBox.Show("データを削除しても宜しいですか？",
                            "確認",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question,
                            MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
            {
                Delete();
            }

        }
        #endregion

        #region イベント

        /// <summary>
        /// 主キー検索時完了時に動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchCode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(色コード))
                        {
                            // 存在する色マスタ検索
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 色コード, 0 }));

                        }
                        else
                        {
                            string msg = "色コードが入力されていません。";
                            this.ErrorMessage = msg;
                            MessageBox.Show(msg);
                            return;
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

        /// <summary>
        /// 登録項目の入力完了時に動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ltbRunnable_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Update();
            }

        }

        #endregion

        #region 処理メソッド

        /// <summary>
        /// 表示内容の初期化をおこなう
        /// </summary>
        private void ScreenClear()
        {
            色コード = string.Empty;
            色名称 = string.Empty;
            削除日時 = null;

            this.MaintenanceMode = string.Empty;
            // キーのみtrue
            ChangeKeyItemChangeable(true);
            // ボタンはFalse
            btnEnableChange(true);

            ResetAllValidation();

            SetFocusToTopControl();

        }

        /// <summary>
        /// データ更新処理をおこなう
        /// </summary>
        private void Update()
        {
            try
            {
                SetFocusToTopControl();

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
                if (yesno == MessageBoxResult.Yes)
                {
                    // 入力値を設定
                    MstData["色コード"] = 色コード;
                    MstData["色名称"] = 色名称;

                    base.SendRequest(new CommunicationObject(
                        MessageType.UpdateData,
                        TargetTableNmUpdate,
                        new object[] {
                            MstData,
                            ccfg.ユーザID
                        }));
                }
                else
                {
                    return;
                }

            }
            catch
            {
                // 更新後エラー
                this.ErrorMessage = "更新時にエラーが発生しました。";

            }
            finally
            {
            }
        }

        /// <summary>
        /// 削除メソッド
        /// </summary>
        private void Delete()
        {
            try
            {
                if (!string.IsNullOrEmpty(色コード))
                {
                    base.SendRequest(new CommunicationObject(
                        MessageType.UpdateData,
                        TargetTableNmDelete,
                        new object[] {
                            色コード,
                            ccfg.ユーザID
                        }));
                }
                else
                {
                    string msg = "色コードが入力されていません。";
                    this.ErrorMessage = msg;
                    MessageBox.Show(msg);
                    return;
                }

            }
            catch
            {
                // 削除登録エラー
                this.ErrorMessage = "削除時にエラーが発生しました。";
                return;

            }

        }

        #endregion

        #region ボタンEnabled
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

        #region MainWindow_Closed
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            //画面が閉じられた時、データを保持する
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion

    }

}
