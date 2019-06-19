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
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Configuration;

namespace KyoeiSystem.Application.Windows.Views
{

    /// <summary>
    /// MCustomer.xaml の相互作用ロジック
    /// </summary>
    public partial class DBSetup : WindowGeneralBase
    {
        #region 変数定義

		string _originalConnStr = string.Empty;
		public SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();

        #endregion

        #region バインド用プロパティ

		private string _NaviMessage = string.Empty;
		public string NaviMessage
		{
			get { return this._NaviMessage; }
			set { this._NaviMessage = value; NotifyPropertyChanged(); }
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

        private string _DBServer;
        public string DBServer
        {
            get { return this._DBServer; }
            set { this._DBServer = value; NotifyPropertyChanged(); }
        }

        private string _DBName;
        public string DBName
        {
            get { return this._DBName; }
            set { this._DBName = value; NotifyPropertyChanged(); }
        }


        #endregion

		public DBSetup()
		{
			InitializeComponent();
			this.DataContext = this;
		}

		#region Loadイベント

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
			NaviMessage = string.Format("データベースへの接続情報を登録して下さい。");
			SqlConnectionStringBuilder sqlBuilder = AppCommon.GetLocalPCConnStr();
			this.DBServer = sqlBuilder == null ? string.Empty : sqlBuilder.DataSource;
			this.DBName = sqlBuilder == null ? string.Empty : sqlBuilder.InitialCatalog;
			this.UserName = sqlBuilder == null ? string.Empty : sqlBuilder.UserID;
			//this.Password = sqlBuilder == null ? string.Empty : sqlBuilder.Password;
			this.txtPasswd.SetPassword(sqlBuilder == null ? string.Empty : sqlBuilder.Password);
		}

        #endregion

        #region ボタンクリック処理

		// 接続テストボタン
		private void ConnectTest_Click(object sender, RoutedEventArgs e)
		{
		}
		
		// 保存ボタン
        private void Save_Click(object sender, RoutedEventArgs e)
        {
			try
			{
				AppCommon.SaveSqlConnectString(this.DBServer, this.DBName, this.UserName, this.Password);
			}
			catch (Exception ex)
			{
				MessageBox.Show("設定を保存できませんでした。\r\n" + ex.Message);
				return;
			}
			this.DialogResult = true;
		}

		// キャンセルボタン
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
			var apcmn = AppCommon.GetAppCommonData(this);
			this.DialogResult = false;
		}

        #endregion

		private void txtPasswd_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				e.Handled = true;
				BtnSave.Focus();
			}
		}


        #region ログイン処理


        #endregion


    }
}
