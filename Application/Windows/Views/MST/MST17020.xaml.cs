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
    /// 得意先別距離別単価マスタ問合せ
	/// </summary>
	public partial class MST17020 : WindowMasterMainteBase
    {
        #region 定数

        //CSV出力用定数
        private const string SEARCH_MST17010_CSV = "SEARCH_MST17010_CSV";
        //プレビュー出力用定数
        private const string SEARCH_MST17010 = "SEARCH_MST17010";
        //レポート
        private const string rptFullPathName_PIC = @"Files\MST\MST17010.rpt";

        #endregion

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;

        /// <summary>
        /// 画面固有設定項目のクラス定義
        /// ※ 必ず public で定義する。
        /// </summary>
        public class Config17020 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public Config17020 frmcfg = null;

        #endregion

        #region バインド用プロパティ

        private string _タリフID = string.Empty;
        public string タリフID
        {
            get { return this._タリフID; }
            set { this._タリフID = value; NotifyPropertyChanged(); }
        }

        private string _タリフID指定 = string.Empty;
        public string タリフID指定
        {
            get { return this._タリフID指定; }
            set { this._タリフID指定 = value; NotifyPropertyChanged(); }
        }
  

		private DataTable _mSTData;
		public DataTable MSTData
		{
			get { return this._mSTData; }
			set
			{
				this._mSTData = value;
				NotifyPropertyChanged();
			}
		}

        #endregion

        #region MST17020
        /// <summary>
        /// 得意先別距離別単価マスタ問合せ
		/// </summary>
        public MST17020()
		{
			InitializeComponent();
			this.DataContext = this;
		}
        #endregion

        #region Load
        /// <summary>
		/// Loadイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (Config17020)ucfg.GetConfigValue(typeof(Config17020));
            if (frmcfg == null)
            {
                frmcfg = new Config17020();
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

            //先頭にカーソルを合わせる
            SetFocusToTopControl();
		}
        #endregion

        #region データ受信用メソッド
        /// <summary>
        /// データ受信用メソッド
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            var data = message.GetResultData();
            DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
            switch (message.GetMessageName())
            {


                //検索結果取得時
                case SEARCH_MST17010:
                    DispPreviw(tbl);
                    break;

                case SEARCH_MST17010_CSV:
                    OutPutCSV(tbl);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region エラーメッセージ
        public override void OnReveivedError(CommunicationObject message)
		{
			base.OnReveivedError(message);
			MessageBox.Show(ErrorMessage);
		}
        #endregion

        #region プレビュー画面
        /// <summary>
        /// プレビュー画面表示
        /// </summary>
        /// <param name="tbl"></param>
        private void DispPreviw(DataTable tbl)
        {
            try
            {
                if (tbl.Rows.Count < 1)
                {
                    this.ErrorMessage = "対象データが存在しません。";
                    return;
                }
                //印刷処理
                KyoeiSystem.Framework.Reports.Preview.ReportPreview view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
                //第1引数　帳票タイトル
                //第2引数　帳票ファイルPass
                //第3以上　帳票の開始点(0で良い)
                view.MakeReport("独自タリフマスタ問合せ", rptFullPathName_PIC, 0, 0, 0);
                //帳票ファイルに送るデータ。
                //帳票データの列と同じ列名を保持したDataTableを引数とする
				view.SetReportData(tbl);
				view.PrinterName = frmcfg.PrinterName;
				view.ShowPreview();
				view.Close();
				frmcfg.PrinterName = view.PrinterName;

                // 印刷した場合
                if (view.IsPrinted)
                {
                    //印刷した場合はtrueを返す
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CSVファイル出力
        /// <summary>
        /// CSVファイル出力
        /// </summary>
        /// <param name="tbl"></param>
        private void OutPutCSV(DataTable tbl)
        {
            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            //はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = @"C:\";
            //[ファイルの種類]に表示される選択肢を指定する
            sfd.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
            //「CSVファイル」が選択されているようにする
            sfd.FilterIndex = 1;
            //タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //CSVファイル出力
                CSVData.SaveCSV(tbl, sfd.FileName, true, true, false, ',');
                MessageBox.Show("CSVファイルの出力が完了しました。");
            }
        }
        #endregion

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
        /// F5 CSVファイル出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            //int型変数
            int iタリフID;

            //得意先コードの代入
            if (タリフID == "")
            {
                iタリフID = 0;
            }
            else
            {
                iタリフID = AppCommon.IntParse(タリフID);
            }

            int[] iタリフList = new int[0];
            if (!string.IsNullOrEmpty(タリフID指定))
            {
                string[] タリフList = タリフID指定.Split(',');
                iタリフList = new int[タリフList.Length];

                for (int i = 0; i < タリフList.Length; i++)
                {
                    string str = タリフList[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "タリフ指定の形式が不正です。";
                        return;
                    }
                    iタリフList[i] = code;
                }
            }


            //SQL発行
            base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST17010_CSV, new object[] { iタリフID , iタリフList }));
        }


        /// <summary>
        /// F8 リボン　印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {

			PrinterDriver ret = AppCommon.GetPrinter(frmcfg.PrinterName);
			if (ret.Result == false)
			{
				this.ErrorMessage = "プリンタドライバーがインストールされていません！";
				return;
			}
			frmcfg.PrinterName = ret.PrinterName;


            //int型変数
            int iタリフID;

            //得意先コードの代入
            if (タリフID == "")
            {
                iタリフID = 0;
            }
            else
            {
                iタリフID = AppCommon.IntParse(タリフID);
            }

            int[] iタリフList = new int[0];
            if (!string.IsNullOrEmpty(タリフID指定))
            {
                string[] タリフList = タリフID指定.Split(',');
                iタリフList = new int[タリフList.Length];

                for (int i = 0; i < タリフList.Length; i++)
                {
                    string str = タリフList[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "タリフ指定の形式が不正です。";
                        return;
                    }
                    iタリフList[i] = code;
                }
            }

            //SQL発行
            base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST17010, new object[] { iタリフID , iタリフList }));
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
	
		#endregion


        #region Mindoow_Closed
        //画面が閉じられた時、データを保持する
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion

	}
}
