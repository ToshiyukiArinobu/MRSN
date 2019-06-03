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
using System.Data;

using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;


namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
	/// 軽油引取税率マスタ入力
	/// </summary>
	public partial class MST34010 : WindowMasterMainteBase
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
        public class ConfigMST34010 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST34010 frmcfg = null;

        #endregion

        #region 定数定義

        //対象テーブル検索用
        private const string TargetTableNm = "M92_KZEI_UC";
        //対象テーブル更新用
        private const string TargetTableNmUpdate = "M92_KZEI_UPD";
        //対象テーブル更新用
        private const string TargetTableNmDelete = "M92_KZEI_DEL";
        //自動採番
        private const string GetNextID = "M92_KZEI_NEXT";

        #endregion

        #region バインド用プロパティ
    
        private DateTime? _適用開始年月日;
        public DateTime? 適用開始年月日
        {
            get { return this._適用開始年月日; }
            set { this._適用開始年月日 = value; NotifyPropertyChanged(); }
        }
        private string _軽油引取税率 = string.Empty;
		public string 軽油引取税率
        {
			get { return this._軽油引取税率; }
			set { this._軽油引取税率 = value; NotifyPropertyChanged(); }
        }
        //マスタデータ
        private DataRow _MstData;
        public DataRow MstData
        {
            get { return this._MstData; }
            set { this._MstData = value; NotifyPropertyChanged(); }
        }
        #endregion

        #region MST34010

        /// <summary>
		/// 軽油引取税率マスタ入力
		/// </summary>
        public MST34010()
		{
			InitializeComponent();
			this.DataContext = this;
		}

        #endregion

        #region Loadイベント

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
            frmcfg = (ConfigMST34010)ucfg.GetConfigValue(typeof(ConfigMST34010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST34010();
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

            ResetAllValidation();

		}

        #endregion

        #region 画面初期化
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        private void ScreenClear()
        {
            this.MaintenanceMode = string.Empty;
            ChangeKeyItemChangeable(true);
            btnEnableChange(true);
            SetFocusToTopControl();
            MstData = null;
			軽油引取税率 = string.Empty;
            適用開始年月日 = null;
        }
        #endregion

        #region データ受信メソッド

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
                      //検索時処理
                      case TargetTableNm:
                          
                          if (tbl.Rows.Count > 0)
                          {
                              SetTblData(tbl);
                          }

						  if (軽油引取税率 == "")
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

                      //更新時処理
                      case TargetTableNmUpdate:
                          ScreenClear();
                          break;

                      //削除時処理
                      case TargetTableNmDelete:
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
                      case TargetTableNmUpdate:
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

        #endregion

        #region 画面反映
        /// <summary>
        /// データを画面反映
        /// </summary>
        /// <param name="tbl"></param>
        private void SetTblData(DataTable tbl)
        {
            //取得した値を表示
            MstData = tbl.Rows[0];
            適用開始年月日 = Convert.ToDateTime(tbl.Rows[0]["適用開始年月日"]);
			軽油引取税率 = tbl.Rows[0]["軽油引取税率"].ToString();
        }
        #endregion

        #region エラーメッセージ
        public override void OnReveivedError(CommunicationObject message)
		{
			base.OnReveivedError(message);
			MessageBox.Show(ErrorMessage);
		}
        #endregion

        #region 左右ボタン
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
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 適用開始年月日, -1 }));
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
                //最後尾出力
                if (string.IsNullOrEmpty(適用開始年月日.ToString()))
                {
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 0 }));
                    return;
                }

                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 適用開始年月日, 1 }));
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

        #region リボン

        /// <summary>
        /// F8 リボン　印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            MST34020 mst13420 = new MST34020();
            mst13420.ShowDialog(this);
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
            MessageBoxResult result = MessageBox.Show("保存せずに入力を取り消してよろしいですか？"
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
            try
            {
                //MstDataに値がなければメッセージ表示
                if (this.適用開始年月日 == null)
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

                //メッセージボックス
                MessageBoxResult result = MessageBox.Show("表示されている情報を削除しますか？"
                             , "確認"
                             , MessageBoxButton.YesNo
                             , MessageBoxImage.Question
                             , MessageBoxResult.No);
                //キャンセルなら終了
                if (result == MessageBoxResult.Yes)
                {
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmDelete, new object[] { 適用開始年月日 }));           
                }
                
            }
            catch
            {
                this.ErrorMessage = "削除処理が出来ませんでした。";
            }
        }

        #endregion

        #region 処理メソッド

        private void Update()
        {
            try
            {


				//軽油引取税率が未入力の場合
				if (軽油引取税率 == "")
                {
					this.ErrorMessage = "軽油引取税率は入力形式が不正です。";
					MessageBox.Show("軽油引取税率は入力形式が不正です。");
                    return;
                }

				decimal d軽油引取税率 = 0;
                //型変換
				d軽油引取税率 = Convert.ToDecimal(軽油引取税率);

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
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmUpdate, new object[] { 適用開始年月日
                                                                                                                    ,d軽油引取税率
                                                                                                                    }));
                }
                else
                {
                    return;
                }
            }
            catch(Exception)
            {
                this.ErrorMessage = "更新処理が失敗しました。";
            }
        }
       
        #endregion

        #region イベント

		// 軽油引取税率TxtBoxでEnterキーが押下された時UPDATE
        private void UcLabelTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Update();
            }
        }

        /// <summary>
        /// 適用開始日付テキストボックスロストフォーカス
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
                        if (Tekiyoymd.Text == string.Empty)
                        {
                            適用開始年月日 = DateTime.Today;
                        }
                        else
                        {
                            適用開始年月日 = DateTime.Parse(Tekiyoymd.Text);
                        }

                        //最後尾検索
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 適用開始年月日, 0 }));
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

        private void UcLabel_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Update();
            }
        }

       

    }
}
