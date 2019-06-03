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
    /// 規制マスタ保守入力
	/// </summary>
	public partial class MST29010 : WindowMasterMainteBase
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
        private const string TargetTableNm = "M12_KIS_GetData";
        //対象テーブル更新用
        private const string TargetTableNmUpdate = "M12_KIS_UPDATE";
        //対象テーブル削除用
        private const string TargetTableNmDelete = "M12_KIS_DELETE";
        //自動採番用
        private const string GetNextID = "M12_TIK_NEXT";
        #endregion

        #region 表示用データ

        private string _規制区分ID = string.Empty;
        public string 規制区分ID
        {
            get { return this._規制区分ID; }
            set { this._規制区分ID = value; NotifyPropertyChanged(); }
        }

        private string _種別 = string.Empty;
        public string 種別
        {
            get { return this._種別; }
            set { this._種別 = value; NotifyPropertyChanged(); }
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

        #region MST29010
        /// <summary>
        /// 規制マスタ保守入力
		/// </summary>
		public MST29010()
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

            ScreenClear();
            base.MasterMaintenanceWindowList.Add("M12_KIS", new List<Type> { null, typeof(SCH29010) });
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
                //通常データ検索
                    case TargetTableNm:
                        StrData(tbl);
                      
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
                                規制区分ID = iNextCode.ToString();
                                ChangeKeyItemChangeable(false);
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                                SetFocusToTopControl();
                            }
                            break;

                        case TargetTableNmUpdate:

                            if ((int)data == -1)
                            {
                                MessageBoxResult result = MessageBox.Show("車種コード: " + 規制区分ID + "は既に使われています。\n自動採番して登録しますか？",
                                                                                                                "質問",
                                                                                                               MessageBoxButton.YesNo,
                                                                                                               MessageBoxImage.Exclamation,
                                                                                                               MessageBoxResult.No);

                                if (result == MessageBoxResult.No)
                                {
                                    return;
                                }

                                int i規制区分ID = AppCommon.IntParse(規制区分ID);

                                base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmUpdate, new object[] {i規制区分ID
                                                                                                                            ,種別
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
            //データがある場合
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

                //表示データ
                MstData = tbl.Rows[0];
                規制区分ID = tbl.Rows[0]["規制区分ID"].ToString();
                種別 = tbl.Rows[0]["規制名"].ToString();
                
                ChangeKeyItemChangeable(false);
                SetFocusToTopControl();
                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
            }
            else
            {
                ChangeKeyItemChangeable(false);
                SetFocusToTopControl();
                //矢印＜対応
                if (string.IsNullOrEmpty(種別))
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
		/// 最初のIDを表示するボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FistIdButton_Click(object sender, RoutedEventArgs e)
		{
			
			//先頭データ検索
            try
            {
                //規制マスタ保守
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
                if (string.IsNullOrEmpty(規制区分ID))
                {
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 0 }));
                    return;
                }
                
                //前データ検索
                int iSyasyuId = 0;
                if (int.TryParse(規制区分ID, out iSyasyuId))
                {
                    //規制マスタ保守
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
                if(string.IsNullOrEmpty(規制区分ID))
                {
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 0 }));
                    return;
                }
                
				//次データ検索
                int iSyasyuId = 0;
                if (int.TryParse(規制区分ID, out iSyasyuId))
                {
                    //規制マスタ保守
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
                //規制マスタ保守
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
            if (string.IsNullOrEmpty(this.規制区分ID))
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

                    if (string.IsNullOrEmpty(規制区分ID))
                    {
                        //自動採番
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, GetNextID, new object[] { }));
                        return;
                    }

                    int iSyasyuId = 0;
                    if (int.TryParse(規制区分ID, out iSyasyuId))
                    {
                        //存在する規制マスタ保守検索
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iSyasyuId, 0 }));
                    }
                    else
                    {
                        this.ErrorMessage = "規制区分IDの入力形式が不正です。";
                        MessageBox.Show("規制区分IDの入力形式が不正です。");
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
            規制区分ID = string.Empty;
            種別 = string.Empty;
            
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
                int i規制区分ID = 0;

                if (!int.TryParse(規制区分ID, out i規制区分ID))
                {
                    this.ErrorMessage = "規制区分IDの入力形式が不正です。";
                    MessageBox.Show("規制区分IDの入力形式が不正です。");
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
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmUpdate, new object[] {i規制区分ID
                                                                                                                        ,種別
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
            finally
            {
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

                int i規制区分ID = 0;

                if (int.TryParse(規制区分ID, out i規制区分ID))
                {
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmDelete, new object[] { i規制区分ID }));
                }
                else
                {
                    this.ErrorMessage = "車種コードの入力形式が不正です。";
                    
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
