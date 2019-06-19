using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
	/// アプリケーション固有の画面間連携データ
	/// </summary>
	[Serializable]
	public class AppCommonData
	{
		#region マスタメンテ画面呼び出し用
		public WindowControler _CtlWindow = null;
		#endregion

		public System.Version Version = new Version();

		// string型のデータは暗号化した値を格納
		public int? CommonDB_CustomerCd;                 // 顧客コード
		public string CommonDB_UserId;                   // ライセンス認証用ユーザーID
		public int? CommonDB_LoginFlg;                   // ログイン中フラグ
		public DateTime? CommonDB_LastAccessDateTime;    // 前回アクセス日時
		public DateTime? CommonDB_StartDate;             // 開始日
		public DateTime? CommonDB_LimitDate;             // 有効期限
		public DateTime? CommonDB_RegDate;               // 登録日
		public string UserDB_DBServer;                 // DB接続先
		public string UserDB_DBName;                   // DB名
		public string UserDB_DBId;                     // DB接続ID
		public string UserDB_DBPass;                   // DB接続パスワード
	
		private UserConfig usercfg = null;
		public UserConfig UserConfig
		{
			get { return this.usercfg; }
			set
			{
				this.usercfg = value;
			}
		}

		public System.Data.DataTable codedatacollection = null;

		public AppCommonData()
		{
			if (ApplicationDeployment.IsNetworkDeployed)
			{
				this.Version = ApplicationDeployment.CurrentDeployment.CurrentVersion;
			}
			else
			{
				System.Reflection.Assembly asm = System.Reflection.Assembly.GetEntryAssembly();
				this.Version = asm.GetName().Version;
			}
		}
	}
}
