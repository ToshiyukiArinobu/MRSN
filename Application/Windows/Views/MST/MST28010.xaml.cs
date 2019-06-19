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
    /// グリーン経営車種マスタ保守入力
	/// </summary>
	public partial class MST28010 : WindowMasterMainteBase
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
        public class ConfigMST28010 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST28010 frmcfg = null;

        #endregion

        #region 定数
        //対象テーブル検索用
        private const string TargetTableNm = "M14_GSYA_GetData";
        //対象テーブル更新用
        private const string TargetTableNmUpdate = "M14_GSYA_UPDATE";
        //対象テーブル削除用
        private const string TargetTableNmDelete = "M14_GSYA_DELETE";
        //自動採番用
        private const string GetNextID = "M14_TIK_NEXT";
        #endregion

        #region 表示用データ

        private string _G車種ID = string.Empty;
        public string G車種ID
        {
            get { return this._G車種ID; }
            set { this._G車種ID = value; NotifyPropertyChanged(); }
        }

        private string _G車種名 = string.Empty;
        public string G車種名
        {
            get { return this._G車種名; }
            set { this._G車種名 = value; NotifyPropertyChanged(); }
        }

        private string _略称名 = string.Empty;
        public string 略称名
        {
            get { return this._略称名; }
            set { this._略称名 = value; NotifyPropertyChanged(); }
        }

        private decimal? _CO2排出係数１ = null;
        public decimal? CO2排出係数１
        {
            get { return this._CO2排出係数１; }
            set { this._CO2排出係数１ = value; NotifyPropertyChanged(); }
        }

        private decimal? _CO2排出係数２ = null;
        public decimal? CO2排出係数２
        {
            get { return this._CO2排出係数２; }
            set { this._CO2排出係数２ = value; NotifyPropertyChanged(); }
        }

        private DataRow _MstData;
        public DataRow MstData
        {
            get { return this._MstData; }
            set { this._MstData = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region MST28010
        /// <summary>
        /// グリーン経営車種マスタ保守入力
		/// </summary>
		public MST28010()
		{
			InitializeComponent();
			this.DataContext = this;
		}
        #endregion

        #region LOAD
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
            frmcfg = (ConfigMST28010)ucfg.GetConfigValue(typeof(ConfigMST28010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST28010();
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
            base.MasterMaintenanceWindowList.Add("M14_GSYA", new List<Type> { null, typeof(SCH28010) });
            AppCommon.SetutpComboboxList(this.Combo事業用区分, false);
            AppCommon.SetutpComboboxList(this.Comboディーゼル区分, false);
            AppCommon.SetutpComboboxList(this.Combo小型普通貨物区分, false);
            AppCommon.SetutpComboboxList(this.Combo低公害者区分, false);
		}
        #endregion

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
                    //通常データ検索
                    case TargetTableNm:

                            if (tbl.Rows.Count > 0)
                            {
                                //削除日付がnullのデータの時
                                if (!string.IsNullOrEmpty(tbl.Rows[0]["削除日付"].ToString()))
                                {
                                    this.ErrorMessage = "既に削除されているデータです。";
                                    MessageBox.Show("既に削除されているデータです。");
                                    ScreenClear();
                                    return;
                                }
                                StrData(tbl);
                            }

                        
                            //リボンの状態表示
                            if (string.IsNullOrEmpty(G車種名))
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
                    
                    //更新処理受信
                    case TargetTableNmUpdate:


                    //削除処理受信
                    case TargetTableNmDelete:
                        MessageBox.Show("削除が完了しました。");
                        //コントロール初期化
                        ScreenClear();
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
                                G車種ID = iNextCode.ToString();
                                ChangeKeyItemChangeable(false);
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                                SetFocusToTopControl();
                            }
                            break;

                        case TargetTableNmUpdate:

                            if ((int)data == -1)
                            {
                                MessageBoxResult result = MessageBox.Show("車種コード: " + G車種ID + "は既に使われています。\n自動採番して登録しますか？",
                                                                                                                "質問",
                                                                                                               MessageBoxButton.YesNo,
                                                                                                               MessageBoxImage.Exclamation,
                                                                                                               MessageBoxResult.No);

                                if (result == MessageBoxResult.No)
                                {
                                    return;
                                }

                                int i車種ID = AppCommon.IntParse(G車種ID);

                                base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmUpdate, new object[] {i車種ID
                                                                                                                                    ,G車種名
                                                                                                                                    ,略称名
                                                                                                                                    ,CO2排出係数１
                                                                                                                                    ,CO2排出係数２
                                                                                                                                    ,Combo事業用区分.SelectedIndex
                                                                                                                                    ,Comboディーゼル区分.SelectedIndex
                                                                                                                                    ,Combo小型普通貨物区分.SelectedIndex
                                                                                                                                    ,Combo低公害者区分.SelectedIndex
                                                                                                                                    ,this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false
                                                                                                                                    ,true }));
                                break;
                            }

                            ScreenClear();
                            break;
                        case TargetTableNmDelete:
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



        //データ受信メソッド
        private void StrData(DataTable tbl)
        {

            //表示データ
            MstData = tbl.Rows[0];
            G車種ID = tbl.Rows[0]["G車種ID"].ToString();
            G車種名 = tbl.Rows[0]["G車種名"].ToString();
            略称名 = tbl.Rows[0]["略称名"].ToString();
            string henkan1 = tbl.Rows[0]["CO2排出係数１"].ToString();
            CO2排出係数１ = AppCommon.DecimalParse(henkan1);
            string henkan2 = tbl.Rows[0]["CO2排出係数２"].ToString();
            CO2排出係数２ = AppCommon.DecimalParse(henkan2);
            Combo事業用区分.SelectedIndex = Convert.ToInt32(tbl.Rows[0]["事業用区分"].ToString());
            Comboディーゼル区分.SelectedIndex = Convert.ToInt32(tbl.Rows[0]["ディーゼル区分"].ToString());
            Combo小型普通貨物区分.SelectedIndex = Convert.ToInt32(tbl.Rows[0]["小型普通区分"].ToString());
            Combo低公害者区分.SelectedIndex = Convert.ToInt32(tbl.Rows[0]["低公害区分"].ToString());
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
		/// 最初のIDを表示するボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FistIdButton_Click(object sender, RoutedEventArgs e)
		{
			
			//先頭データ検索
            try
            {
                //車種マスタ
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 0 }));
                
            }
            catch (Exception)
            {
                return;
            }
			
		}

		/// <summary>
		/// １つ前のIDを表示するボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BeforeIdButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
                //先頭行出力
                if (string.IsNullOrEmpty(G車種ID))
                {
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 0 }));
                    return;
                }
                
                //前データ検索
                int iSyasyuId = 0;
                if (int.TryParse(G車種ID, out iSyasyuId))
                {
                    //車種マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iSyasyuId, -1 }));
                    
                }
                
			}
			catch (Exception)
			{
				return;
			}
		}

		/// <summary>
		/// １つ次のIDを表示するボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NextIdButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
                //最後尾出力
                if(string.IsNullOrEmpty(G車種ID))
                {
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 2 }));
                    return;
                }
                
				//次データ検索
                int iSyasyuId = 0;
                if (int.TryParse(G車種ID, out iSyasyuId))
                {
                    //車種マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iSyasyuId, 1 }));
                    
                }
                
                
			}
			catch (Exception)
			{
				return;
			}
		}

		/// <summary>
		/// 最後尾のIDを表示するボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LastIdButoon_Click(object sender, RoutedEventArgs e)
		{
			try
			{
                //車種マスタ
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 1 }));
                
			}
			catch (Exception)
			{
				return;
			}
		}
        #endregion

        #region リボン
        /// <summary>
        /// F1 リボン　検索
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
        /// F8 リスト一覧表　
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            //MST05020 mst05020 = new MST05020();
            //mst05020.ShowDialog(this);
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
            if (string.IsNullOrEmpty(this.G車種ID))
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
        private void LabelTextSyaSyuCode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {

                    if (string.IsNullOrEmpty(G車種ID))
                    {
                        //自動採番
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, GetNextID, new object[] { }));
                        return;
                    }


                    int iSyasyuId = 0;
                    if (int.TryParse(G車種ID, out iSyasyuId))
                    {
                        //存在する車種マスタ検索
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iSyasyuId, 0 }));
                    }
                    else
                    {
                        this.ErrorMessage = "車種IDの入力に誤りがあります。";
                        MessageBox.Show("車種IDの入力に誤りがあります。");
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
        private void ScreenClear()
        {
            G車種ID = string.Empty;
            G車種名 = string.Empty;
            略称名 = string.Empty;
            CO2排出係数１ = null;
            CO2排出係数２ = null;
            Combo事業用区分.SelectedIndex = 0;
            Comboディーゼル区分.SelectedIndex = 0;
            Combo小型普通貨物区分.SelectedIndex = 0;
            Combo低公害者区分.SelectedIndex = 0;
            
            this.MaintenanceMode = string.Empty;
            //キーのみtrue
            ChangeKeyItemChangeable(true);
            //ボタンはFalse
            btnEnableChange(true);

            ResetAllValidation();

            SetFocusToTopControl();
        }

        
        private void Update()
        {
            try
            {
                int i車種ID = 0;

                if (!int.TryParse(G車種ID, out i車種ID))
                {
                    this.ErrorMessage = "車種IDの入力形式が不正です。";
                    MessageBox.Show("車種IDの入力形式が不正です。");
                    return;
                }

                if (string.IsNullOrEmpty(G車種名))
                {
                    this.ErrorMessage = "車種名は必須入力項目です。";
                    MessageBox.Show("車種名は必須入力項目です。");
                    return;
                }

                if(CO2排出係数１ == null)
                {
                    this.ErrorMessage = "二酸化炭素排出係数①の入力に誤りがあります。";
                    MessageBox.Show("二酸化炭素排出係数①の入力に誤りがあります。");
                    return;
                }

                if (CO2排出係数２ == null)
                {
                    this.ErrorMessage = "二酸化炭素排出係数②の入力に誤りがあります。";
                    MessageBox.Show("二酸化炭素排出係数②の入力に誤りがあります。");
                    return;
                }

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
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmUpdate, new object[] {i車種ID
                                                                                                                        ,G車種名
                                                                                                                        ,略称名
                                                                                                                        ,CO2排出係数１
                                                                                                                        ,CO2排出係数２
                                                                                                                        ,Combo事業用区分.SelectedIndex
                                                                                                                        ,Comboディーゼル区分.SelectedIndex
                                                                                                                        ,Combo小型普通貨物区分.SelectedIndex
                                                                                                                        ,Combo低公害者区分.SelectedIndex
                                                                                                                        ,this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false
                                                                                                                        ,false }));
                }
                else
                {
                    return;
                }

            }
            catch
            {
                //更新後エラー
                this.ErrorMessage = "更新時にエラーが発生しました。";
            }
        }

        //IME
        private void sekisai_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            // 数字(0-9)は入力可
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
                return;
            }
            // 上記以外は入力不可
            e.Handled = true;
        }


        /// <summary>
        /// 削除メソッド
        /// </summary>
        private void Delete()
        {
            try
            {

                int i車種ID = 0;

                if (int.TryParse(G車種ID, out i車種ID))
                {
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmDelete, new object[] { i車種ID }));
                }
                else
                {
                    this.ErrorMessage = "車種IDの入力に誤りがあります。";
                    MessageBox.Show("車種IDの入力に誤りがあります。");
                }
            }
            catch
            {
                //削除登録エラー
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

        #region CO2排出係数１未入力時
        private void UcLabelTextBox_CO2排出係数１(object sender, RoutedEventArgs e)
        {
            if (CO2排出係数１ == null)
            {
                CO2排出係数１ = 0;
            }
        }
        #endregion

        #region CO2排出係数２未入力時
        private void UcLabelTextBox_CO2排出係数２(object sender, RoutedEventArgs e)
        {
            if(CO2排出係数２ == null)
            {
                CO2排出係数２ = 0;
            }
        }
        #endregion

    }
}
