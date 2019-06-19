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
    /// 支払残高問合せ
	/// </summary>
    public partial class SHR07010 : WindowMasterMainteBase
	{
        #region 定数定義
        private const string SEARCH_SHR07010 = "SEARCH_SHR07010";
        private const string SEARCH_SHR07010_CSV = "SEARCH_SHR07010_CSV";
        private const string rptFullPathName_PIC = @"Files\SHR\SHR07010.rpt";
        #endregion

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;

        //<summary>
        //画面固有設定項目のクラス定義
        //※ 必ず public で定義する。
        //</summary>
        public class ConfigSHR07010 : FormConfigBase
        {
            public string 作成年 { get; set; }
            public string 作成月 { get; set; }
            public string 締日 { get; set; }
            public DateTime? 集計期間From { get; set; }
            public DateTime? 集計期間To { get; set; }
            public int 区分1 { get; set; }
            public int 区分2 { get; set; }
            public int 区分3 { get; set; }
            public int 区分4 { get; set; }
            public int 区分5 { get; set; }
            public int 区分6 { get; set; }
            public int 区分7 { get; set; }
            public int 区分8 { get; set; }
            public bool? チェック { get; set; }
        }

        /// ※ 必ず public で定義する。
        public ConfigSHR07010 frmcfg = null;

        #endregion

        #region バインド用プロパティ

        private int? _得意先ID = null;
        public int? 得意先ID
        {
            get { return this._得意先ID; }
            set { this._得意先ID = value; NotifyPropertyChanged(); }
        }

        private string _作成年 = null;
        public string 作成年
        {
            get { return this._作成年; }
            set { this._作成年 = value; NotifyPropertyChanged(); }
        }

        private string _作成月 = null;
        public string 作成月
        {
            get { return this._作成月; }
            set { this._作成月 = value; NotifyPropertyChanged(); }
        }

        private int _表示区分 = 0;
        public int 表示区分
        {
            get { return this._表示区分; }
            set { this._表示区分 = value; NotifyPropertyChanged(); }
        }

        private int _取引区分 = 4;
        public int 取引区分
        {
            get { return this._取引区分; }
            set { this._取引区分 = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region SHR07010()

        /// <summary>
        /// 支払残高問い合わせ
        /// </summary>
        /// 
        public SHR07010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #endregion

        #region LOADイベント
        /// <summary>
        /// 画面読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { null, typeof(SCH01010) });
            AppCommon.SetutpComboboxList(this.表示区分_Cmb, false);

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigSHR07010)ucfg.GetConfigValue(typeof(ConfigSHR07010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigSHR07010();
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
                this.作成年 = frmcfg.作成年;
                this.作成月 = frmcfg.作成月;
                this.表示区分_Cmb.SelectedIndex = frmcfg.区分1;
            }
            #endregion

			ResetAllValidation();
			SetFocusToTopControl();

        }
        #endregion

        #region エラーメッセージ
        /// <summary>
        /// データアクセスエラー受信イベント
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            // 基底クラスのエラー受信イベントを呼び出します。
            base.OnReveivedError(message);
        }
        #endregion

        #region データ受信メソッド
        /// <summary>
        /// 取得データの正常受信時のイベント
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            try
            {
                var data = message.GetResultData();
                DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
                switch (message.GetMessageName())
                {
                    //プレビュー出力用
                    case SEARCH_SHR07010:
                        DispPreviw(tbl);
                        break;

                    //CSV出力用
                    case SEARCH_SHR07010_CSV:
                        OutPutCSV(tbl);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                view.MakeReport("支払残高問い合わせ", rptFullPathName_PIC, 0, 0, 0);
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
            if (tbl.Rows.Count < 1)
            {
                this.ErrorMessage = "対象データが存在しません。";
                return;
            }

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
            //支払先
            if (得意先ID == null)
            {
                this.ErrorMessage = "支払先は入力必須項目です。";
                MessageBox.Show("支払先は入力必須項目です。");
                return;
            }

            //集計年月
            if (string.IsNullOrEmpty(作成年) || string.IsNullOrEmpty(作成年))
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                MessageBox.Show("作成年月は入力必須項目です。");
                return;
            }

            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }

            //表示区分
            int i表示区分 = 表示区分_Cmb.SelectedIndex;



            int i集計年月;
            if (作成月.ToString().Length == 1)
            {
                i集計年月 = Convert.ToInt32(作成年.ToString() + "0" + 作成月.ToString());
            }
            else
            {
                i集計年月 = Convert.ToInt32(作成年.ToString() + 作成月.ToString());
            }

            //作成年月度
            string s作成年月度 = Convert.ToString(作成年 + "年" + 作成月 + "月度～");


			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SHR07010_CSV, new object[] { 得意先ID, 作成年, 作成月, i集計年月, i表示区分, s作成年月度 }));

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


            //支払先
            if (得意先ID == null)
            {
                this.ErrorMessage = "支払先は入力必須項目です。";
                MessageBox.Show("支払先は入力必須項目です。");
                return;
            }

            //集計年月
            if (string.IsNullOrEmpty(作成年) || string.IsNullOrEmpty(作成年))
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                MessageBox.Show("作成年月は入力必須項目です。");
                return;
            }


            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }

            //表示区分
            int i表示区分 = 表示区分_Cmb.SelectedIndex;


            int i集計年月;
            if (作成月.ToString().Length == 1)
            {
                i集計年月 = Convert.ToInt32(作成年.ToString() + "0" + 作成月.ToString());
            }
            else
            {
                i集計年月 = Convert.ToInt32(作成年.ToString() + 作成月.ToString());
            }


            //集計年月入力チェック
            if (string.IsNullOrEmpty(作成年))
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                return;
            }
            else if (string.IsNullOrEmpty(作成月))
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                return;
            }


            //作成年月度
            string s作成年月度 = Convert.ToString(作成年 + "年" + 作成月 + "月度～");


			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SHR07010, new object[] { 得意先ID, 作成年, 作成月, i集計年月, i表示区分, s作成年月度 }));

        }

        //リボン
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
            frmcfg.作成年 = this.作成年;
            frmcfg.作成月 = this.作成月;
            frmcfg.区分1 = this.表示区分_Cmb.SelectedIndex;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion

        #region 日付処理

        private void Lost_Year(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(作成年))
            {
                作成年 = DateTime.Today.ToString().Substring(0, 4);
            }
        }

        private void Lost_Month(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(作成月))
            {
                作成月 = DateTime.Today.ToString().Substring(5, 2);
            }
        }

        #endregion

        #region Enterキー(F8)

        private void F8(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
			{
				var yesno = MessageBox.Show("プレビューを表示しますか？", "確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
				if (yesno == MessageBoxResult.No)
				{
					return;
				}

                OnF8Key(sender, null);
            }
        }

        #endregion
	}
}
