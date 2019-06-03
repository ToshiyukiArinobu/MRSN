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
using System.Xml;
using System.Reflection;


namespace KyoeiSystem.Application.Windows.Views
{

    /// <summary>
    /// MCustomer.xaml の相互作用ロジック
    /// </summary>
    public partial class LicenseLogin : WindowGeneralBase
    {

        #region 定数定義
        private const string LicenseLoginCommonDB = "LicenseLoginCommonDB";
        #endregion

        #region バインド用プロパティ

        private string _ユーザーID;
        public string ユーザーID
        {
            get { return this._ユーザーID; }
            set { this._ユーザーID = value; NotifyPropertyChanged(); }
        }

        private string _パスワード = string.Empty;
        public string パスワード
        {
            get { return this._パスワード; }
			set { this._パスワード = value; NotifyPropertyChanged(); }
        }

        private bool? _ライセンス情報記憶;
        public bool? ライセンス情報記憶
        {
            get { return this._ライセンス情報記憶; }
			set {
					this._ライセンス情報記憶 = value; NotifyPropertyChanged();
					if (ライセンス情報記憶 == true)
					{
						ucAutoUser.IsEnabled = false;
						PASSWORD.IsEnabled = false;
						LOGIN_BTN.Focus();
					}
					else
					{
						ucAutoUser.IsEnabled = true;
						PASSWORD.IsEnabled = true;
					}
				}
        }

        #endregion

        public bool IsLoginCompleted = false;

        #region ログイン
        /// <summary>
        /// 
        /// </summary>
        public LicenseLogin()
        {
            InitializeComponent();
            this.DataContext = this;
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
                case LicenseLoginCommonDB:
                    int p顧客コード = Convert.ToInt32(tbl.Rows[0]["顧客コード"]);
                    string pユーザーID = tbl.Rows[0]["ユーザーID"].ToString();
                    string pパスワード = tbl.Rows[0]["パスワード"].ToString();

                    // ユーザーIDとパスワードが一致しかつエラーではない場合
                    // またはテスト用としてログイン中エラーを出さないユーザーの場合
                    if (
                        (Utility.Encrypt(ユーザーID) == pユーザーID && Utility.Encrypt(パスワード) == pパスワード && p顧客コード >= 0)
						|| (AppConst.CommonDB_DebugUser != string.Empty && Utility.Encrypt(ユーザーID) == AppConst.CommonDB_DebugUser)
                       )
                    {
                        try
                        {
							var apcmn = AppCommon.GetAppCommonData(this);
							// 取得した情報をセットする。
							apcmn.CommonDB_CustomerCd = tbl.Rows[0]["顧客コード"] == DBNull.Value ? (int?)null : Convert.ToInt32(tbl.Rows[0]["顧客コード"]);
							apcmn.CommonDB_UserId = tbl.Rows[0]["ユーザーID"].ToString();
							apcmn.CommonDB_LoginFlg = tbl.Rows[0]["ログインフラグ"] == DBNull.Value ? (int?)null : Convert.ToInt32(tbl.Rows[0]["ログインフラグ"]);
							apcmn.CommonDB_LastAccessDateTime = tbl.Rows[0]["アクセス時間"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(tbl.Rows[0]["アクセス時間"]);
							apcmn.UserDB_DBServer = tbl.Rows[0]["DB接続先"].ToString();
							apcmn.UserDB_DBName = tbl.Rows[0]["ユーザーDB"].ToString();
							apcmn.UserDB_DBId = tbl.Rows[0]["DBログインID"].ToString();
							apcmn.UserDB_DBPass = tbl.Rows[0]["DBパスワード"].ToString();
							apcmn.CommonDB_StartDate = tbl.Rows[0]["開始日"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(tbl.Rows[0]["開始日"]);
							apcmn.CommonDB_LimitDate = tbl.Rows[0]["有効期限"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(tbl.Rows[0]["有効期限"]);
							apcmn.CommonDB_RegDate = tbl.Rows[0]["登録日"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(tbl.Rows[0]["登録日"]);
                            IsLoginCompleted = true;
                            this.Close();
                            return;
                        }
                        catch (Exception ex)
                        {
                            //例外エラー表示
                            MessageBox.Show(ex.Message);
                        }

                    }
                    else if (Utility.Encrypt(ユーザーID) == pユーザーID && Utility.Encrypt(パスワード) == pパスワード && p顧客コード == -1)
                    {
                        //【※ログイン失敗※】
                        SetFocusToTopControl();
                        this.ErrorMessage = "このユーザーはログイン中です。";
                    }
                    else if (Utility.Encrypt(ユーザーID) == pユーザーID && Utility.Encrypt(パスワード) == pパスワード && p顧客コード == -2)
                    {
                        //【※ログイン失敗※】
                        SetFocusToTopControl();
                        this.ErrorMessage = "このユーザーはライセンスの有効期限が切れています。";
                    }
                    else if (p顧客コード == -90001)
                    {
                        // サーバー接続エラー
                        SetFocusToTopControl();
                        this.ErrorMessage = "認証サーバーに接続できませんでした。";
                    }
                    else
                    {
                        //【※ログイン失敗※】
                        SetFocusToTopControl();
                        this.ErrorMessage = "ユーザーIDまたはパスワードが違います。";
                    }
                    break;


            }
            this.Cursor = null;
        }
        #endregion

        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            this.Message = base.ErrorMessage;
        }



        #region ボタンクリック処理

        //ログインボタン
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            SaveProperties();
            Login();
        }
        //終了ボタン
        private void Debug_Login_Click(object sender, RoutedEventArgs e)
        {
            SaveProperties();
            Environment.Exit(0);
        }


        #endregion

        #region ログイン処理

        // LOGIN処理
        private void Key_login(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //20151106 Toyama いったんログインボタンにフォーカスを当てます
                e.Handled = true;
                LOGIN_BTN.Focus();

                //SaveProperties();
                //Login();
            }
        }

        public void Login()
        {
            this.Cursor = Cursors.Wait;

            // 暗号化したIDで照合する。
            SendRequest(new CommunicationObject(MessageType.RequestLicense, LicenseLoginCommonDB,
                new object[] { Utility.Encrypt(ユーザーID) }));
        }

        #endregion

        /// <summary>
        /// 描画後に文字列を埋める処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowGeneralBase_ContentRendered(object sender, EventArgs e)
        {
			bool lcflag = false;
			string lcuser = string.Empty;
			string lcpass = string.Empty;

			try
			{

				FileInfo fi = new System.IO.FileInfo(GetLocalConfigFilePath());
				if (fi.Exists)
				{
					ConfigXmlDocument cdoc = new ConfigXmlDocument();
					cdoc.Load(fi.FullName);
					var cfgs = cdoc.SelectNodes("/configuration/settings/add");
					foreach (var item in cfgs)
					{
						var key = (item as XmlElement).Attributes.GetNamedItem("key").Value;
						var val = (item as XmlElement).Attributes.GetNamedItem("value").Value;
						switch (key)
						{
						case "LoginCheck":
							lcflag = string.IsNullOrWhiteSpace(val) ? false : Convert.ToBoolean(val);
							break;
						case "TextUr":
							lcuser = Utility.Decrypt(val);
							break;
						case "TextLr":
							lcpass = Utility.Decrypt(val);
							break;
						}
					}
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				this.ライセンス情報記憶 = lcflag;
				if (lcflag)
				{
					this.ユーザーID = lcuser;
					if (lcpass.Trim() != string.Empty)
					{
						this.PASSWORD.SetPassword(lcpass);
					}
				}
			}

        }

        /// <summary>
        /// プロパティを保存する処理
        /// </summary>
        private void SaveProperties()
        {
			try
			{
				FileInfo fi = new System.IO.FileInfo(GetLocalConfigFilePath());
				if (fi.Exists != true)
				{
					if (fi.Directory.Exists != true)
					{
						fi.Directory.Create();
					}
				}
				ConfigXmlDocument cdoc = new ConfigXmlDocument();
				var cfg = cdoc.CreateElement("configuration");
				var apps = cdoc.CreateElement("settings");
				Func<string, string, bool> addItem = (key, val) =>
				{
					var node = cdoc.CreateElement("add");
					var attrK = cdoc.CreateAttribute("key");
					attrK.InnerText = key;
					var attrV = cdoc.CreateAttribute("value");
					attrV.InnerText = string.IsNullOrWhiteSpace(val) ? string.Empty : val;
					node.Attributes.Append(attrK);
					node.Attributes.Append(attrV);
					apps.AppendChild(node);
					return true;
				};
				addItem("LoginCheck", this.ライセンス情報記憶.ToString());
				addItem("TextUr", Utility.Encrypt(this.ユーザーID));
				addItem("TextLr", Utility.Encrypt(this.パスワード));
				cdoc.AppendChild(cfg);
				cfg.AppendChild(apps);
				cdoc.Save(fi.FullName);
			}
			catch (Exception)
			{
			}
			finally
			{
			}

        }

		private string GetLocalConfigFilePath()
		{
			// ファイル名
			// {Systemドライブ}:\ユーザー\{ユーザ名}\AppData\Local\KyoeiSystem\{APL名}\local.config
			// 
			var dir = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			var exeasm = System.Reflection.Assembly.GetEntryAssembly();
			var assemblyTitle = exeasm.GetCustomAttributes(typeof(AssemblyTitleAttribute), true).Single() as AssemblyTitleAttribute;
			FileInfo fi = new System.IO.FileInfo(dir + @"\KyoeiSystem\" + assemblyTitle.Title + @"\local.config");

			return fi.FullName;
		}

    }
}
