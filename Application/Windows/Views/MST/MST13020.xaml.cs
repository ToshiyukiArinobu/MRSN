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
    /// 消費税率マスタ問合せ
	/// </summary>
	public partial class MST13020 : WindowMasterMainteBase
    {
        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;

        /// <summary>
        /// 画面固有設定項目のクラス定義
        /// ※ 必ず public で定義する。
        /// </summary>
        public class ConfigMST13020 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST13020 frmcfg = null;

        #endregion

        #region 定数定義
        
        //CSV出力用定数
        private const string SEARCH_MST13020_CSV = "SEARCH_MST13020_CSV";
        //プレビュー出力用定数
        private const string SEARCH_MST13020 = "SEARCH_MST13020";
        //レポート
        private const string rptFullPathName_PIC = @"Files\MST\MST13010.rpt";

        #endregion

        #region バインディング用プロパティ
        private DateTime? _適用開始日From = null;
        public DateTime? 適用開始日From
        {
            get { return this._適用開始日From; }
            set { this._適用開始日From = value; NotifyPropertyChanged(); }

        }

        private DateTime? _適用開始日To = null ;
        public DateTime? 適用開始日To
        {
            get { return this._適用開始日To; }
            set { this._適用開始日To = value; NotifyPropertyChanged(); }

        }
		

        #endregion

        #region MST13020
        /// <summary>
		/// 消費税率マスタ問合せ
		/// </summary>
        public MST13020()
		{
			InitializeComponent();
			this.DataContext = this;
		}
        #endregion

        #region Load時イベント
        /// <summary>
		/// Loadイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigMST13020)ucfg.GetConfigValue(typeof(ConfigMST13020));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST13020();
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
		}
        #endregion

        #region 画面初期化
        private void ScreenClear()
        {
            SetFocusToTopControl();
            ResetAllValidation();

            適用開始日From = null;
            適用開始日To = null;
        }
        #endregion

        #region データ受信メソッド
        public override void OnReceivedResponseData(CommunicationObject message)
		{
			var data = message.GetResultData();
			DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
			switch (message.GetMessageName())
			{
                //検索結果取得時
                case SEARCH_MST13020:
                    DispPreviw(tbl);
                    break;

                case SEARCH_MST13020_CSV:
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
                view.MakeReport("消費税率マスタリスト", rptFullPathName_PIC, 0, 0, 0);
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
            try
            {
                DateTime? DayFrom = null;
                DateTime? DayTo = null;

                if (適用開始日From == null)
                {
                    DayFrom = DateTime.MinValue;
                }
                else
                {
                    DayFrom = 適用開始日From;
                }
                if (適用開始日To == null)
                {
                    DayTo = DateTime.MaxValue;
                }
                else
                {
                    DayTo = 適用開始日To;
                }
                //CSV出力用
                base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST13020_CSV, new object[] { DayFrom, DayTo }));
            }
            catch
            {
                this.ErrorMessage = "システムエラーです担当者にご確認ください";
            }
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


            try
            {
                DateTime? DayFrom = null;
                DateTime? DayTo = null;

                if (適用開始日From == null)
                {
                    DayFrom = DateTime.MinValue;
                }
                else
                {
                    DayFrom = 適用開始日From;
                }
                if (適用開始日To == null)
                {
                    DayTo = DateTime.MaxValue;
                }
                else
                {
                    DayTo = 適用開始日To;
                }
                //帳票出力用
                base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST13020, new object[] { DayFrom, DayTo }));
            }
            catch
            {
                this.ErrorMessage = "システムエラーです担当者にご確認ください";
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

		#endregion

        #region イベント

        //クリアボタンクリック
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ScreenClear();
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

        private void Day1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (適用開始日From == null)
                {
                    適用開始日From = DateTime.Today;
                }
            }
        }

        private void Day2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (適用開始日To == null)
                {
                    適用開始日To = DateTime.Today;
                }
                OnF8Key(null, null);
            }
        }

      

    }
}
