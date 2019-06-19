using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;

using KyoeiSystem.Application.Windows.Views;
using System.Data;
using System.Collections.Specialized;
using System.Configuration;

namespace Hakobo
{
	/// <summary>
	/// StartupWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class StartupWindow : WindowGeneralBase
	{
		int count = 0;
		double step = 0;
		private const string GetMSGLIST = "GetMessageList";
		private const string GetComboList = "GetComboboxList";

		public bool IsConnected = false;
		public bool IsReload = false;

		private double _progress = 0;
		public double Progress
		{
			get { return this._progress; }
			set { this._progress = value; NotifyPropertyChanged(); }
		}

		private Visibility _ProgressVisibility = Visibility.Visible;
		public Visibility ProgressVisibility
		{
			get { return this._ProgressVisibility; }
			set { this._ProgressVisibility = value; NotifyPropertyChanged(); }
		}
		private Visibility _dBSetupButtonVisibility = Visibility.Collapsed;
		public Visibility DBSetupButtonVisibility
		{
			get { return this._dBSetupButtonVisibility; }
			set { this._dBSetupButtonVisibility = value; NotifyPropertyChanged(); }
		}

		private DataTable _msgmas = null;
		public DataTable MSGMAS
		{
			get { return this._msgmas; }
			set { this._msgmas = value; NotifyPropertyChanged(); }
		}

		int reqCount = 2;
		bool isErrorDelected = false;
		bool resMSG = false;
		bool resCOMBO = false;

		public StartupWindow()
			: base()
		{
			InitializeComponent();
			this.DataContext = this;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			appLog.CleanUp();
			this.Start();
		}

		private void Start()
		{
			isErrorDelected = false;
			DBSetupButtonVisibility = System.Windows.Visibility.Collapsed;
			this.Progress = 0;
			this.count = 2000;	/* x 100ミリ秒 */
			step = 200.0 / (double)count;
			//各初期設定を実行
			Message = "データベースへの接続を確認します。";

			base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMSGLIST, null));
			base.SendRequest(new CommunicationObject(MessageType.RequestData, GetComboList, null));

			base.TimerLoopStart(200);
		}

		public override void OnReveivedError(CommunicationObject message)
		{
			base.OnReveivedError(message);
			ConnectErrorMessage(false, message);
		}

		public override void OnReceivedTimer(CommunicationObject message)
		{
			base.OnReceivedTimer(message);
			if (count > 0)
			{
				this.Progress += step * 10;
				count--;
				return;
			}
			ConnectErrorMessage(true, message);
		}

		private void ConnectErrorMessage(bool isTimeout, CommunicationObject message)
		{
			appLog.Error("DBアクセス失敗");
			if (isErrorDelected)
			{
				return;
			}
			isErrorDelected = true;

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
				// ライセンスDBに登録されたユーザDB情報が不正または未稼働のとき
				Message += "\r\nデータベースに接続できませんでした。";
				Message += "\r\nお手数ですが、ライセンスの登録内容についてお問い合わせ下さい。";
				return;
			}

			if (IsConnected)
			{
				// ストアドの実行タイムアウト
				if (isTimeout)
				{
					Message += "\r\n時間内に処理が完了しませんでした。";
				}
				else
				{
					Message += "\r\nデータベースにアクセスできませんでした。";
					Message += "\r\n処理を中止します。";
				}
			}
			else
			{
				if (message.ErrorType == MessageErrorType.DBConnectError)
				{
					Message += "\r\nデータベースに接続できませんでした。";
				}
				else
				{
					Message += "\r\nデータベースへのアクセスが失敗しました。";
				}
			}
			base.TimerLoopStop();
			Message += "\r\nスタートアップ処理を中止します。";
			Message += "\r\nデータベースの動作状況またはネットワークの状態をご確認ください。";
			DBSetupButtonVisibility = System.Windows.Visibility.Visible;
			ProgressVisibility = System.Windows.Visibility.Collapsed;
		}

		public override void OnReceivedResponseData(CommunicationObject message)
		{
			IsConnected = true;
			var data = message.GetResultData();
			DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
			MSGMAS = tbl;
			switch (message.GetMessageName())
			{
			case GetMSGLIST:
				this.viewsCommData.SetupMessageList(tbl);
				resMSG = true;
				break;
			case GetComboList:
				(this.viewsCommData.AppData as AppCommonData).codedatacollection = tbl;
				resCOMBO = true;
				break;
			}
			if (resMSG)
			{
				Message += "\r\nデータベースに接続できました。";
				Message += "\r\nアプリケーション実行の準備をしています。";
			}
			if (resMSG && resCOMBO)
			{
				this.Progress = 100;
				base.TimerLoopStop();
				Message += "\r\n準備完了しました。";
				this.Close();
			}
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			IsConnected = false;
			Message += "\r\n処理を終了します。";
			if (this.threadmgr != null)
			{
				this.threadmgr.Dispose();
				this.threadmgr = null;
			}
			this.Close();
		}

		private void DBSetup_Click(object sender, RoutedEventArgs e)
		{
			DBSetup frm = new DBSetup();
			bool? result = frm.ShowDialog(this);
			if (result == true)
			{
				IsReload = true;
				this.Close();
			}
			else
			{
				IsConnected = false;
			}
		}
	}
}
