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
    /// 品群マスタ保守入力
	/// </summary>
	public partial class MST04022 : WindowMasterMainteBase
    {
        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;
        #region "権限関係"
        CommonConfig ccfg = null;
        #endregion

        #endregion

        #region 定数

        /// <summary>対象コードデータの取得</summary>
        private const string TargetTableNm = "M16_HINGUN_GetData";
        /// <summary>対象コードデータの更新</summary>
        private const string TargetTableNmUpdate = "M16_HINGUN_UPDATE";
        /// <summary>対象コードデータの削除</summary>
        private const string TargetTableNmDelete = "M16_HINGUN_DELETE";

        #endregion

        #region 表示用データ

        private string _商品群コード = string.Empty;
        public string 商品群コード
        {
            get { return this._商品群コード; }
            set { this._商品群コード = value; NotifyPropertyChanged(); }
        }

        private string _商品群名 = string.Empty;
        public string 商品群名
        {
            get { return this._商品群名; }
            set { this._商品群名 = value; NotifyPropertyChanged(); }
        }

        private string _かな読み = string.Empty;
        public string かな読み
        {
            get { return _かな読み; }
            set { this._かな読み = value; NotifyPropertyChanged(); }
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

        #region MST04022
        /// <summary>
        /// 商品群マスタ保守入力
		/// </summary>
        public MST04022()
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
            #endregion

            ScreenClear();
            base.MasterMaintenanceWindowList.Add("M16_HINGUN", new List<Type> { null, typeof(SCHM16_HINGUN) });

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
                        // 通常データ検索
                        case TargetTableNm:
                            SetData(tbl);
                            break;
                    }

                }
                else
                {

                    switch (message.GetMessageName())
                    {
                        // 更新処理受信
                        case TargetTableNmUpdate:

                            if ((int)data == -1)
                            {
                                // TODO:リトライ？？？
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

                        // 削除処理受信
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
        /// データ受信メソッド
        /// </summary>
        /// <param name="tbl"></param>
        private void SetData(DataTable tbl)
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
                MstData = tbl.Rows[0];

                // 表示データ
                商品群コード = MstData["商品群コード"].ToString();
                商品群名 = MstData["商品群名"].ToString();
                かな読み = MstData["かな読み"].ToString();
                DateTime Wk;
                削除日時 = DateTime.TryParse(tbl.Rows[0]["削除日時"].ToString(), out Wk) ? Wk : (DateTime?)null;


                ChangeKeyItemChangeable(false);
                SetFocusToTopControl();
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
            }
            else
            {
                MstData = tbl.NewRow();

                ChangeKeyItemChangeable(false);
                SetFocusToTopControl();
                // 矢印＜対応
                if (string.IsNullOrEmpty(商品群名))
                {
                    this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                }
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
        /// ページ変更時の実処理
        /// </summary>
        /// <param name="sender"></param>
        private void ChangePage_Click(object sender, RoutedEventArgs e)
        {
            int? option = 0;

            // 画面モードが編集モード時のみ動作
            if (this.MaintenanceMode.Equals(AppConst.MAINTENANCEMODE_EDIT))
            {
                if (this.TOP.Equals(sender))
                {   // TOP押下時
                    option = -2;
                }
                else if (this.PREV.Equals(sender))
                {   // PREV押下時
                    option = -1;
                }
                else if (this.NEXT.Equals(sender))
                {   // NEXT押下時
                    option = 1;
                }
                else if (this.END.Equals(sender))
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
                    if (!string.IsNullOrEmpty(商品群コード))
                    {
                        // 存在する商品群マスタ検索
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 商品群コード, option }));
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
        /// F1 マスタ照会 検索
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
        /// F8 リスト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            MST04022_1 mst04022_1 = new MST04022_1();
            mst04022_1.ShowDialog(this);
        }

        /// <summary>
        /// F9　登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            Update();
        }

        /// <summary>
        /// F10　リボン入力取消し　
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            //メッセージボックス
            MessageBoxResult result = MessageBox.Show("保存せずに入力を取り消しても宜しいですか？"
                            , "確認"
                            , MessageBoxButton.YesNo
                            , MessageBoxImage.Question);
            //OKならクリア
            if (result == MessageBoxResult.Yes)
            {
                this.ScreenClear();
            }
        }

        /// <summary>
        /// F11　リボン終了
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
            if (string.IsNullOrEmpty(this.商品群コード))
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

            MessageBoxResult result = MessageBox.Show("データを削除しても宜しいですか？"
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

        #region イベント

        /// <summary>
        /// 主キー検索時完了時に動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelTextHingunCode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {

                    if (!string.IsNullOrEmpty(商品群コード))
                    {
                        // 存在する商品群マスタ保守検索
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 商品群コード, 0 }));
                    }
                    else
                    {
                        this.ErrorMessage = "コードの入力形式が不正です。";
                        MessageBox.Show("コードの入力形式が不正です。");
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
        private void UcLabelTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                Update();
                
            }

        }

        #endregion

        #region 処理メソッド

        /// <summary>
        /// 入力内容の初期化をおこなう
        /// </summary>
        private void ScreenClear()
        {
            商品群コード = string.Empty;
            商品群名 = string.Empty;
            かな読み = string.Empty;
            削除日時 = null;
            
            this.MaintenanceMode = string.Empty;
            //キーのみtrue
            ChangeKeyItemChangeable(true);

            ResetAllValidation();

            SetFocusToTopControl();
        }

        
        /// <summary>
        /// データ更新処理
        /// </summary>
        private void Update()
        {
            
            try
            {
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
                    // row に入力値を設定する
                    MstData["商品群コード"] = 商品群コード;
                    MstData["商品群名"] = 商品群名;
                    MstData["かな読み"] = かな読み;

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
                if (string.IsNullOrEmpty(商品群コード))
                {
                    this.ErrorMessage = "商品群コードの入力形式が不正です。";
                }
                else
                {
                    base.SendRequest(new CommunicationObject(
                        MessageType.UpdateData,
                        TargetTableNmDelete,
                        new object[] {
                            商品群コード,
                            ccfg.ユーザID
                        }));
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

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            // 画面が閉じられた時、画面サイズを保持する
            
        }


        
    }
}
