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
using KyoeiSystem.Framework.Windows.Controls;

namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
    /// 商品中分類マスタ保守入力
	/// </summary>
    public partial class MST04010 : WindowMasterMainteBase
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
        public class ConfigMST29010 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST29010 frmcfg = null;

        #endregion

        #region 定数
        //対象テーブル検索用
        private const string TargetTableNm = "M13_CHU_SEARCH";
        //対象テーブル更新用
        private const string TargetTableNmUpdate = "M13_CHU_UPDATE";
        //対象テーブル削除用
        private const string TargetTableNmDelete = "M13_CHU_DELETE";
        #endregion

        #region 表示用データ
        private string _大分類コード = string.Empty;
        public string 大分類コード
        {
            get { return this._大分類コード; }
            set { this._大分類コード = value; NotifyPropertyChanged(); }
        }

        private string _大分類名 = string.Empty;
        public string 大分類名
        {
            get { return this._大分類名; }
            set { this._大分類名 = value; NotifyPropertyChanged(); }
        }

        private string _中分類コード = string.Empty;
        public string 中分類コード
        {
            get { return this._中分類コード; }
            set { this._中分類コード = value; NotifyPropertyChanged(); }
        }

        private string _中分類名 = string.Empty;
        public string 中分類名
        {
            get { return this._中分類名; }
            set { this._中分類名 = value; NotifyPropertyChanged(); }
        }

        private string _削除日付 = string.Empty;
        public string 削除日付
        {
            get { return this._削除日付; }
            set { this._削除日付 = value; NotifyPropertyChanged(); }
        }

        private DataRow _MstData;
        public DataRow MstData
        {
            get { return this._MstData; }
            set { this._MstData = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region << 初期表示処理 >>

        /// <summary>
        /// 商品中分類マスタ保守入力
		/// </summary>
        public MST04010()
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
            frmcfg = (ConfigMST29010)ucfg.GetConfigValue(typeof(ConfigMST29010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST29010();
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

            base.MasterMaintenanceWindowList.Add("M12_DAI", new List<Type> { null, typeof(SCHM12_DAIBUNRUI) });
            base.MasterMaintenanceWindowList.Add("M13_CHU", new List<Type> { null, typeof(SCHM13_TYUBUNRUI) });

            ScreenClear();

            SetFocusToTopControl();

        }

        #endregion

        #region << データ受信関連 >>
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
                    case TargetTableNm:
                        // 通常データ検索
                        bool isResult = StrData(tbl);
                        if (isResult)
                        {
                            ChangeKeyItemChangeable(false);

                            // 新規登録時は中分類を編集可とする
                            if (MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
                                txtMediumClassCode.IsEnabled = true;

                        }
                        break;

                    case TargetTableNmUpdate:
                        // 更新処理受信
                        MessageBox.Show(AppConst.COMPLETE_UPDATE, "処理完了", MessageBoxButton.OK, MessageBoxImage.Information);
                        ScreenClear();
                        break;

                    case TargetTableNmDelete:
                        // 削除処理受信
                        MessageBox.Show(AppConst.COMPLETE_DELETE, "処理完了", MessageBoxButton.OK, MessageBoxImage.Information);

                        // コントロール初期化
                        ScreenClear();
                        break;

                }

                SetFocusToTopControl();

			}
			catch (Exception ex)
			{
				this.ErrorMessage = ex.Message;

            }

        }

        /// <summary>
        /// サービス実行エラー時の処理
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
		{
			base.OnReveivedError(message);
			MessageBox.Show(ErrorMessage);
		}

        #endregion

        #region リボン

        #region F1 マスタ検索
        /// <summary>
        /// F1　リボン　マスタ検索
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
        #endregion

        #region F2 マスタメンテ
        /// <summary>
        /// F2　リボン　マスタメンテ
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
        #endregion

        #region F8 リスト一覧
        /// <summary>
        /// F8　リボン　リスト一覧表　
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            //MST05020 mst05020 = new MST05020();
            //mst05020.ShowDialog(this);
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
            if (MaintenanceMode == null)
                return;

            int i大分類コード = 0;
            int i中分類コード = 0;

            if (!int.TryParse(大分類コード, out i大分類コード))
            {
                this.txtMajerTypeCode.Focus();
                this.ErrorMessage = "大分類コードコードの入力形式が不正です。";
                return;
            }

            if (!int.TryParse(中分類コード, out i中分類コード))
            {
                this.txtMediumClassCode.Focus();
                this.ErrorMessage = "中分類コードの入力形式が不正です。";
                return;
            }

            if (string.IsNullOrEmpty(txtMediumClassName.Text))
            {
                this.txtMediumClassName.Focus();
                this.ErrorMessage = "中分類名が入力されていません。";
                return;
            }

            if (!base.CheckAllValidation())
            {
                SetFocusToTopControl();
                MessageBox.Show("入力内容に誤りがあります。");
                this.ErrorMessage = "入力内容に誤りがあります。";
                return;
            }

            var yesno =
                MessageBox.Show(
                    AppConst.CONFIRM_UPDATE,
                    "登録確認",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.Yes);

            if (yesno == MessageBoxResult.Yes)
                Update(i大分類コード, i中分類コード);

        }
        #endregion

        #region F10 入力取消
        /// <summary>
        /// F10　リボン　入力取消し　
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            // メッセージボックス
            MessageBoxResult result =
                MessageBox.Show("保存せずに入力を取り消しても宜しいですか？",
                    "確認",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

            // OKならクリア
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
            if (MaintenanceMode == null)
                return;

            if (string.IsNullOrEmpty(中分類コード))
            {
                this.ErrorMessage = "登録内容がありません。";
                return;
            }

            if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
            {
                SetFocusToTopControl();
                this.ErrorMessage = "新規登録データは削除できません。";
                return;
            }

            MessageBoxResult result =
                MessageBox.Show(
                    AppConst.CONFIRM_DELETE,
                    "確認",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
                Delete();

        }
        #endregion

        #endregion

        #region イベント

        #region 中分類キー押下イベント
        /// <summary>
        /// 中分類テキストキー押下時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelTextSyaSyuCode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (MaintenanceMode != null)
                    return;

                if (e.Key == Key.Enter || e.Key == Key.Tab)
                {

                    if (txtMajerTypeCode.CheckValidation() == false)
                        return;

                    // データ検索
                    base.SendRequest(
                        new CommunicationObject(
                            MessageType.RequestData,
                            TargetTableNm,
                            new object[] {
                                大分類コード,
                                中分類コード,
                                (int)AppConst.PagingOption.Paging_Code
                            }));

                }

            }
            catch (Exception ex)
            {
                appLog.Debug("【Error:{0}***{1}】", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                this.ErrorMessage = ex.Message;
            }

        }
        #endregion

        #region 最終項目のキー押下時イベント
        /// <summary>
        /// 登録項目の入力完了時に動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcLabelTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            UcLabelTextBox tb = sender as UcLabelTextBox;

            if (e.Key == Key.Enter && !string.IsNullOrEmpty(tb.cText))
            {
                OnF9Key(sender, null);
                
            }

        }
        #endregion

        #region ページングボタンイベント
        /// <summary>
        /// ページングボタン押下時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PagingButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            if (string.IsNullOrEmpty(大分類コード))
            {
                MessageBox.Show("大分類が入力されていません。", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int searchPage = (int)AppConst.PagingOption.Paging_Code;

            if (btn.Name.Equals(this.FistIdButton.Name))
                searchPage = (int)AppConst.PagingOption.Paging_Top;

            else if (btn.Name.Equals(this.PrevIdButton.Name))
                searchPage = (int)AppConst.PagingOption.Paging_Before;

            else if (btn.Name.Equals(this.NextIdButton.Name))
                searchPage = (int)AppConst.PagingOption.Paging_After;

            else if (btn.Name.Equals(this.LastIdButoon.Name))
                searchPage = (int)AppConst.PagingOption.Paging_End;

            // ページングデータ検索
            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    TargetTableNm,
                    new object[] {
                        大分類コード,
                        中分類コード,
                        searchPage
                    }));

        }
        #endregion

        #region Window_closed
        /// <summary>
        /// 画面が閉じられた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        #endregion

        #region 処理メソッド

        #region 画面項目初期化
        /// <summary>
        /// 画面の初期化をおこなう
        /// </summary>
        private void ScreenClear()
        {
            大分類コード = string.Empty;
            大分類名 = string.Empty;
            中分類コード = string.Empty;
            中分類名 = string.Empty;
            
            this.MaintenanceMode = null;
            // キーのみtrue
            ChangeKeyItemChangeable(true);

            ResetAllValidation();

            // ページングボタンは常に有効とする
            FistIdButton.IsEnabled = true;
            PrevIdButton.IsEnabled = true;
            NextIdButton.IsEnabled = true;
            LastIdButoon.IsEnabled = true;

            SetFocusToTopControl();

        }
        #endregion

        #region 検索結果設定
        /// <summary>
        /// 検索結果の設定をおこなう
        /// </summary>
        /// <param name="tbl"></param>
        private bool StrData(DataTable tbl)
        {
            // データがある場合
            if (tbl.Rows.Count > 0)
            {
                // 削除日付がnullのデータの時
                if (!string.IsNullOrEmpty(tbl.Rows[0]["削除日時"].ToString()))
                {
                    this.ErrorMessage = "既に削除されているデータです。";
                    MessageBox.Show("既に削除されているデータです。");
                    ScreenClear();
                    return false;
                }

                // 表示データ
                MstData = tbl.Rows[0];
                中分類コード = tbl.Rows[0]["中分類コード"].ToString();
                中分類名 = tbl.Rows[0]["中分類名"].ToString();

                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;

            }
            else
            {
                // データなしの場合は新規登録
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;

            }

            return true;

        }
        #endregion

        #region 更新処理
        /// <summary>
        /// 更新処理
        /// </summary>
        private void Update(int majCode, int tyuCode)
        {
            try
            {
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.UpdateData,
                        TargetTableNmUpdate,
                        new object[] {
                            majCode,
                            tyuCode,
                            txtMediumClassName.Text,
                            ccfg.ユーザID
                        }));

            }
            catch
            {
                // 更新後エラー
                this.ErrorMessage = "更新時にエラーが発生しました。";

            }

        }
        #endregion

        #region 削除処理
        /// <summary>
        /// 削除メソッド
        /// </summary>
        private void Delete()
        {
            try
            {
                int i大分類コード = 0;
                int i中分類コード = 0;

                if (int.TryParse(中分類コード, out i中分類コード) && int.TryParse(大分類コード , out i大分類コード))
                {
                    base.SendRequest(
                        new CommunicationObject(
                            MessageType.UpdateData,
                            TargetTableNmDelete,
                            new object[] {
                                i大分類コード,
                                i中分類コード,
                                ccfg.ユーザID
                            }));
                }
                else
                {
                    this.ErrorMessage = "中分類コードの入力形式が不正です。";
                    
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

        #endregion

    }

}
