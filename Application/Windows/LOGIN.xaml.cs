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
using System.IO;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;
using KyoeiSystem.Framework.Windows.Controls;
using System.Collections.Specialized;
using System.Configuration;

namespace KyoeiSystem.Application.Windows.Views
{

    /// <summary>
    /// MCustomer.xaml の相互作用ロジック
    /// </summary>
    public partial class LOGIN : WindowGeneralBase
    {
		public bool IsLoggedIn = false;
		public bool IsReload = false;

        #region 定数定義
        private const string SEARCH_LOGIN = "SEARCH_LOGIN";
		//private const string SEARCH_LOGOUT = "Logout";
		private const string USER_LOGOUT = "updateLogout";
        #endregion

        public class LOGINConfig : FormConfigBase
        {
            public string[] gamenID = { "ログイン", "テスト" };
        }

        public UserConfig usercfg;
        CommonConfig ccfg;
        LOGINConfig lcfg;

        #region バインド用プロパティ

        private string _UserID;
        public string UserID
        {
            get { return this._UserID; }
            set { this._UserID = value; NotifyPropertyChanged(); }
        }

        private string _UserName;
        public string UserName
        {
            get { return this._UserName; }
            set { this._UserName = value; NotifyPropertyChanged(); }
        }


        private string _Password = string.Empty;
        public string Password
        {
            get { return this._Password; }
            set { this._Password = value; NotifyPropertyChanged(); }
        }

        private string _LicenseID;
        public string LicenseID
        {
            get { return this._LicenseID; }
            set { this._LicenseID = value; NotifyPropertyChanged(); }
        }


        #endregion

        #region ログイン
        /// <summary>
        /// ログインフォーム コンストラクタ
        /// </summary>
        public LOGIN()
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
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            var apcmn = AppCommon.GetAppCommonData(this);
            LicenseID = apcmn.CommonDB_UserId;
        }

        #endregion

        #region データ受信メソッド
        /// <summary>
        /// 取得データの正常受信時のイベント
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {

            var data = message.GetResultData();
            DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
            switch (message.GetMessageName())
            {
                //ログイン
                case SEARCH_LOGIN:
					byte[] bin = tbl.Rows[0]["設定項目"] as byte[];
					if (bin == null)
					{
						usercfg = new UserConfig();
					}
					else
					{
						System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(UserConfig));
						var strm = new System.IO.MemoryStream(bin);
						var val = serializer.Deserialize(strm);
						strm.Close();
						usercfg = val as UserConfig;
						if (usercfg == null) { usercfg = new UserConfig(); }
					}
                    AppCommon.SetupConfig(this, usercfg);
                    ccfg = (CommonConfig)usercfg.GetConfigValue(typeof(CommonConfig));
                    if (ccfg == null)
                    {
                        ccfg = new CommonConfig();
                    }
                    ccfg.ログイン時刻 = DateTime.Now;
                    ccfg.ユーザID = AppCommon.IntParse(UserID);
                    ccfg.ユーザ名 = tbl.Rows[0]["担当者名"].ToString();
                    ccfg.自社コード = AppCommon.IntParse(tbl.Rows[0]["自社コード"].ToString());
                    ccfg.自社販社区分 = AppCommon.IntParse(tbl.Rows[0]["自社販社区分"].ToString());
                    ccfg.ライセンスID = LicenseID;
                    // 変更
                    ccfg.権限 = (int)tbl.Rows[0]["グループ権限ID"];
                    ccfg.タブグループ番号 = (int?[])tbl.Rows[0]["タブグループ番号"];
                    ccfg.プログラムID = (string[])tbl.Rows[0]["プログラムID"];
                    ccfg.使用可能FLG = (Boolean[])tbl.Rows[0]["使用可能FLG"];
                    ccfg.データ更新FLG = (Boolean[])tbl.Rows[0]["データ更新FLG"];

                    usercfg.SetConfigValue(ccfg);

                    lcfg = (LOGINConfig)usercfg.GetConfigValue(typeof(LOGINConfig));
                    if (lcfg == null)
                    {
                        lcfg = new LOGINConfig();
                        lcfg.Top = this.Top;
                        lcfg.Left = this.Left;
                        usercfg.SetConfigValue(lcfg);
                    }
                    this.Top = lcfg.Top;
                    this.Left = lcfg.Left;

                    string pUserID = tbl.Rows[0]["担当者ID"].ToString();
                    string pPassword = tbl.Rows[0]["パスワード"].ToString();


                    //【ログイン】UserID && Passwordが一致する場合 かつ【999999】エラーデータではない場合
                    if (UserID == pUserID && Password == pPassword && pUserID != "999999")
                    {
                        try
                        {
                            IsLoggedIn = true;
                            this.Close();

                        }
                        catch (Exception ex)
                        {
                            //例外エラー表示
                            MessageBox.Show(ex.Message);
                        }

                    }
                    else
                    {
                        //【※ログイン失敗※】
                        SetFocusToTopControl();
                        this.ErrorMessage = "該当するデータが見つかりません";
                    }
                    break;

                //ログアウト
				case USER_LOGOUT:
                    //this.Close();
                    Environment.Exit(0);
                    break;
            }
        }
        #endregion

        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            this.Message = base.ErrorMessage;
            switch (message.GetMessageName())
            {
                case SEARCH_LOGIN:

                    MessageBox.Show("ログイン失敗しました（" + message.ErrorType + " : " + message.GetParameters()[0] + "）");
                    break;
            }
        }



        #region ボタンクリック処理

        //ログインボタン
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }
        //終了ボタン
        private void Debug_Login_Click(object sender, RoutedEventArgs e)
        {
			if (base.viewsCommData.WithLicenseDB)
			{
				var apcmn = AppCommon.GetAppCommonData(this);
				// 20150806 wada add ログイン中フラグをログアウト状態に更新する。
				SendRequest(new CommunicationObject(MessageType.RequestLicense, USER_LOGOUT, apcmn.CommonDB_UserId));
			}
			else
			{
                //this.Close();
                Environment.Exit(0);
            }

        }

        #endregion

        #region ログイン処理

        // LOGIN処理
        private void Key_login(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                //Login();
                BtnLogin.Focus();
                //this.User.SelectAll();
            }
        }

        public void Login()
        {
            int id, pass;
			if (int.TryParse(this.UserID, out id) != true)
			{
				ErrorMessage = "利用者IDを入力してください。";
				return;
			}
			//if (int.TryParse(Password, out pass) != true && Password != string.Empty)
			//{
			//	ErrorMessage = "パスワードを入力して下さい。";
			//	return;
			//}

            SendRequest(new CommunicationObject(MessageType.RequestData, SEARCH_LOGIN, id/*, typeof(UserConfig)*/));



        }

        #endregion


        private void ucAutoUser_ValueSelected(object sender, RoutedEventArgs e)
        {
            try
            {

                UcAutoCompleteTextBox actext = sender as UcAutoCompleteTextBox;
                if (actext == null)
                {
                    return;
                }
                DataRow selectdata = actext.SelectedItem;
                if (selectdata == null)
                {
                    return;
                }
                UserID = selectdata["担当者ID"].ToString();

            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }

        }

        private void WindowGeneralBase_ContentRendered(object sender, EventArgs e)
        {
			if (base.viewsCommData.WithLicenseDB)
			{
				var apcmn = AppCommon.GetAppCommonData(this);
				// 20150909 wada add ログイン中フラグをログイン状態に更新する。
				SendRequest(new CommunicationObject(MessageType.RequestLicense, "updateLogin", apcmn.CommonDB_UserId));
			}
        }

		private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.S)
			{
				// Ctrl + Alt + Shift + S でDB接続情報画面を表示する
				if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Alt | ModifierKeys.Shift))
				{
					bool IsNeedLicenseCheck = true;
					var plist = (NameValueCollection)ConfigurationManager.GetSection("serviceSettings");
					if (plist != null)
					{
						if (plist["mode"] == CommonConst.WithoutLicenceDBMode)
						{
							IsNeedLicenseCheck = false;
						}
					}
					if (IsNeedLicenseCheck)
					{
						return;
					}

					e.Handled = true;
					DBSetup setup = new DBSetup();
					if (setup.ShowDialog(this) == true)
					{
						IsReload = true;
						this.Close();
					}
				}
			}
		}

    }
}
