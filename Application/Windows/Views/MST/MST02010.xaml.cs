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
    /// 品番マスタ入力
	/// </summary>
	public partial class MST02010 : WindowMasterMainteBase
    {
        /// <summary>
        /// 商品形態分類
        /// </summary>
        private enum eItemStyle : int
        {
            SET品 = 1,
            資材単品 = 2,
            雑コード = 3,
            副資材 = 4
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
        public class ConfigMST03010 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST03010 frmcfg = null;

        System.Windows.Forms.ContextMenuStrip cMenu = new System.Windows.Forms.ContextMenuStrip();

        #endregion

        #region 定数定義

        private const string TargetTableNm = "M09_HIN_getData";
        private const string GetNextID = "M09_HIN_getNext";
        private const string UpdateTable = "M09_HIN_Update";

        #endregion

        #region バインド用変数

        private string _品番コード = string.Empty;
        public string 品番コード
        {
            get { return this._品番コード; }
            set { this._品番コード = value; NotifyPropertyChanged(); }
        }

        private DataRow _M09_HIN_SearchRow;
        public DataRow M09_HIN_SearchRow
        {
            get { return _M09_HIN_SearchRow; }
            set { _M09_HIN_SearchRow = value; NotifyPropertyChanged(); }
        }

        #endregion

        /// <summary>
        /// 品番マスタ入力
		/// </summary>
        public MST02010()
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
            frmcfg = (ConfigMST03010)ucfg.GetConfigValue(typeof(ConfigMST03010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST03010();
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

            base.MasterMaintenanceWindowList.Add("M09_HIN", new List<Type> { null, typeof(SCHM09_HIN) });
            base.MasterMaintenanceWindowList.Add("M06_IRO", new List<Type> { null, typeof(SCHM06_IRO) });
            base.MasterMaintenanceWindowList.Add("M14_BRAND", new List<Type> { null, typeof(SCHM14_BRAND) });
            base.MasterMaintenanceWindowList.Add("M15_SERIES", new List<Type> { null, typeof(SCHM15_SERIES) });
            base.MasterMaintenanceWindowList.Add("M16_HINGUN", new List<Type> { null, typeof(SCHM16_HINGUN) });
            base.MasterMaintenanceWindowList.Add("M12_DAI", new List<Type> { null, typeof(SCHM12_DAIBUNRUI) });
            base.MasterMaintenanceWindowList.Add("M13_CHU", new List<Type> { null, typeof(SCHM13_TYUBUNRUI) });

        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        private void ScreenClear()
        {
            品番コード = string.Empty;
            M09_HIN_SearchRow = null;

            this.MaintenanceMode = string.Empty;
			
            //キーのみtrue
            ChangeKeyItemChangeable(true);
            //ボタンはFalse
            //btnEnableChange(true);

            SetFocusToTopControl();
            this.ResetAllValidation();

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
                ViewBaseCommon.CallMasterMainte(this.MasterMaintenanceWindowList);
            }
            catch (Exception ex)
            {
                appLog.Error("マスターメンテ画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }
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
            if (string.IsNullOrEmpty(this.MaintenanceMode))
            {
                this.ScreenClear();
                return;
            }

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
            if (string.IsNullOrEmpty(this.MaintenanceMode))
            {
                this.Close();
                return;
            }

            MessageBoxResult result = MessageBox.Show("表示されている情報を登録せずに終了しますか？"
                            , "確認"
                            , MessageBoxButton.YesNo
                            , MessageBoxImage.Question
                            , MessageBoxResult.No);
            //キャンセルなら終了
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }

        }

        ///// <summary>
        ///// F12　削除
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public override void OnF12Key(object sender, KeyEventArgs e)
        //{

        //    if (string.IsNullOrEmpty(this.発着地コード))
        //    {
        //        this.ErrorMessage = "登録内容がありません。";
        //        MessageBox.Show("登録内容がありません。");
        //        return;
        //    }

        //    if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
        //    {
        //        this.ErrorMessage = "新規入力データは削除できません。";
        //        MessageBox.Show("新規入力データは削除できません。");
        //        return;
        //    }

        //    MessageBoxResult result = MessageBox.Show("表示されている情報を削除しますか？"
        //                    , "確認"
        //                    , MessageBoxButton.YesNo
        //                    , MessageBoxImage.Question
        //                    , MessageBoxResult.No);
        //    //キャンセルなら終了
        //    if (result == MessageBoxResult.Yes)
        //    {
        //        Delete();
        //    }
            
        //}

        #endregion

        #region 更新処理
        /// <summary>
        /// 更新処理
        /// </summary>
        public void Update()
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

                var yesno = MessageBox.Show("入力内容を登録しますか？", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (yesno != MessageBoxResult.Yes)
                    return;

                base.SendRequest(
                    new CommunicationObject(
                        MessageType.UpdateData,
                        UpdateTable,
                        new object[] {
                            M09_HIN_SearchRow,
                            ccfg.ユーザID
                        }));

            }
            catch (Exception)
            {
                return;
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

                DataTable tbl = data as DataTable;

                switch (message.GetMessageName())
                {
                    case TargetTableNm:
                        SetTblData(tbl);

                        // 編集ステータス表示
                        this.MaintenanceMode = (tbl.Rows.Count > 0) ? AppConst.MAINTENANCEMODE_EDIT : AppConst.MAINTENANCEMODE_ADD;

                        SetFocusToTopControl();

                        // REMARKS:行バインドではLinkItemの大分類が設定されないので
                        //         再適用させる
                        string midium = M09_HIN_SearchRow["中分類"].ToString();
                        txtMediumClassCode.Text1 = string.Empty;
                        txtMediumClassCode.Text1 = midium;
                        break;

                    case UpdateTable:
                        if (LogicalDel.Text.Equals("9"))
                            MessageBox.Show("削除処理が完了しました。", "確認", MessageBoxButton.OK, MessageBoxImage.Information);
                        else
                            MessageBox.Show("登録処理が完了しました。", "確認", MessageBoxButton.OK, MessageBoxImage.Information);
                        ScreenClear();
                        break;

                    case GetNextID:
                        SetTblData(tbl);
                        品番コード = M09_HIN_SearchRow["品番コード"].ToString();

                        ChangeKeyItemChangeable(false);
                        this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                        SetFocusToTopControl();
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

        /// <summary>
        /// テーブルデータを各変数に代入
        /// </summary>
        /// <param name="tbl"></param>
        private void SetTblData(DataTable tbl)
        {
            // キーをfalse
            ChangeKeyItemChangeable(false);

            if (tbl.Rows.Count > 0)
            {
                M09_HIN_SearchRow = tbl.Rows[0];
                M09_HIN_SearchRow["消費税区分"] = 0;

            }
            else
            {
                M09_HIN_SearchRow = tbl.NewRow();
                M09_HIN_SearchRow["品番コード"] = this.ProductCode.Text1;
                M09_HIN_SearchRow["消費税区分"] = 0;
            }
            NotifyPropertyChanged("M09_HIN_SearchRow");

            // 商品形態が"1:SET商品"の場合のみ使用可能とする
            SetButton.IsEnabled = this.ItemStyle.Text.Equals(((int)eItemStyle.SET品).ToString());

        }

        /// <summary>
        /// 画面が閉じられた後のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// 品番コードでキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HinbanCode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                try
                {
                    if (string.IsNullOrEmpty(品番コード))
                    {
                        // 項目が空値だった場合は最大番号の次を取得して新規登録とする
                        base.SendRequest(
                            new CommunicationObject(
                                MessageType.RequestData,
                                GetNextID,
                                new object[] { }));

                    }
                    else
                    {
                        int i品番コード = 0;
                        if (!int.TryParse(品番コード, out i品番コード))
                        {
                            this.ErrorMessage = "品番コードの入力形式が不正です。";
                            MessageBox.Show("品番コードの入力形式が不正です。");
                            return;
                        }

                        // 品番コードデータ検索
                        base.SendRequest(
                            new CommunicationObject(
                                MessageType.RequestData,
                                TargetTableNm,
                                new object[] {
                                    品番コード
                                }));

                    }

                }
                catch (Exception)
                {
                    return;
                }

            }

        }

        /// <summary>
        /// セット品構成品登録ボタン押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MST03010 frm = new MST03010();
                frm.txtMyProduct.Text1 = this.MyProductCode.Text;
                frm.txtMyColor.Text1 = this.ColorCode.Text1;

                frm.ShowDialog(this);

            }
            catch
            {
            }

        }

        /// <summary>
        /// 得意先商品売価設定ボタン押下時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TokuiButton_Click(object sender, RoutedEventArgs e)
        {
            MST19010 mstForm = new MST19010();
            mstForm.SendFormId = (int)MST19010.SEND_FORM.品番マスタ;
            mstForm.HinbanCode = int.Parse(品番コード);
            mstForm.ItemNumber = this.MyProductCode.Text;
            mstForm.ColorCode = this.ColorCode.Text1;

            mstForm.ShowDialog(this);

        }

        /// <summary>
        /// 仕入先商品売価設定ボタン押下時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShiireButton_Click(object sender, RoutedEventArgs e)
        {
            MST17010 mstForm = new MST17010();
            mstForm.SendFormId = (int)MST17010.SEND_FORM.品番マスタ;
            mstForm.HinbanCode = int.Parse(品番コード);
            mstForm.ItemNumber = this.MyProductCode.Text;
            mstForm.ColorCode = this.ColorCode.Text1;

            mstForm.ShowDialog(this);

        }

        /// <summary>
        /// 外注先商品売価設定ボタン押下時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GaichuButton_Click(object sender, RoutedEventArgs e)
        {
            MST18010 mstForm = new MST18010();
            mstForm.SendFormId = (int)MST18010.SEND_FORM.品番マスタ;
            mstForm.HinbanCode = int.Parse(品番コード);
            mstForm.ItemNumber = this.MyProductCode.Text;
            mstForm.ColorCode = this.ColorCode.Text1;

            mstForm.ShowDialog(this);

        }

        /// <summary>
        /// 客先品番登録ボタン押下時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KyakusakiButton_Click(object sender, RoutedEventArgs e)
        {
            MST20010 mstForm = new MST20010();
            mstForm.SendFormId = (int)MST20010.SEND_FORM.品番マスタ;
            mstForm.ProductNumber = int.Parse(this.ProductCode.Text1);
            mstForm.ItemNumber = this.MyProductCode.Text;
            mstForm.ColorCode = this.ColorCode.Text1;

            mstForm.ShowDialog(this);

        }

        /// <summary>
        /// 商品形態分類変更時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemStyle_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            SetButton.IsEnabled = this.ItemStyle.Text.Equals(((int)eItemStyle.SET品).ToString());

        }

        /// <summary>
        /// 大分類が変更された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMajorClassCode_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            // 中分類のLinkItemを変更、設定値を初期化する
            this.txtMediumClassCode.LinkItem = this.txtMajorClassCode.Text1;
            this.txtMediumClassCode.Text1 = string.Empty;

        }


	}

}
