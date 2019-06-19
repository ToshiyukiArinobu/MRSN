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
using KyoeiSystem.Framework.Windows.Controls;


namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 発着地マスタ問合せ
    /// </summary>
    public partial class MST14020 : WindowMasterMainteBase
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
        public class ConfigMST14020 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST14020 frmcfg = null;

        #endregion

        private const string SEARCH_MST14020 = "SEARCH_MST14020";
        private const string SEARCH_MST14020_CSV = "SEARCH_MST14020_CSV";
        private const string rptFullPathName_PIC = @"Files\MST\MST14020.rpt";

        private string _得意先ID_FROM = string.Empty;
        public string 得意先ID_FROM
        {
            get { return this._得意先ID_FROM; }
            set { this._得意先ID_FROM = value; NotifyPropertyChanged(); }
        }

        private string _得意先ID_TO = string.Empty;
        public string 得意先ID_TO
        {
            get { return this._得意先ID_TO; }
            set { this._得意先ID_TO = value; NotifyPropertyChanged(); }
        }

        private string _適用開始年月日TO = string.Empty;
        public string 適用開始年月日TO
        {
            get { return this._適用開始年月日TO; }
            set { this._適用開始年月日TO = value; NotifyPropertyChanged(); }
        }

        private string _表示方法 = "0";
        public string 表示方法
        {
            get { return this._表示方法; }
            set { this._表示方法 = value; NotifyPropertyChanged(); }
        }

        private int _取引区分 = 4;
        public int 取引区分
        {
            get { return this._取引区分; }
            set { this._取引区分 = value; NotifyPropertyChanged(); }
        }

        private string _支払先指定 = string.Empty;
        public string 支払先指定
        {
            get { return this._支払先指定; }
            set { this._支払先指定 = value; NotifyPropertyChanged(); }
        }


        private DataTable _SeachResultData;
        public DataTable SeachResultData
        {
            get { return this._SeachResultData; }
            set
            {
                this._SeachResultData = value;
                NotifyPropertyChanged();
            }
        }


        /// <summary>
        /// 燃料単価マスタ問合せ
        /// </summary>
        public MST14020()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigMST14020)ucfg.GetConfigValue(typeof(ConfigMST14020));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST14020();
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

            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { null, typeof(SCH01010) });
        }


        /// <summary>
        /// データ受信メソッド
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            var data = message.GetResultData();
            DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
            switch (message.GetMessageName())
            {
                //検索結果取得時
                case SEARCH_MST14020:
                    DispPreviw(tbl);
                    break;
                case SEARCH_MST14020_CSV:
                    OutPutCSV(tbl);
                    break;
                default:
                    break;
            }
            this.SeachResultData = null;
        }
        /// <summary>
        /// データ受信エラー時メソッド
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            MessageBox.Show(ErrorMessage);
        }

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
                view.MakeReport("燃料単価マスタ一覧表", rptFullPathName_PIC, 0, 0, 0);
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
                var ctl = FocusManager.GetFocusedElement(this);
                if (ctl is TextBox)
                {
                    var uctext = ViewBaseCommon.FindVisualParent<UcTextBox>(ctl as UIElement);
                    if (uctext == null)
                    {
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(uctext.DataAccessName))
                    {
                        ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                        return;
                    }
                    SCH01010 srch = new SCH01010();
                    switch (uctext.DataAccessName)
                    {
                        case "M01_TOK":
                            srch.MultiSelect = false;
                            break;
                        default:
                            srch.MultiSelect = true;
                            break;
                    }
                    Framework.Windows.Controls.UcLabelTwinTextBox dmy = new Framework.Windows.Controls.UcLabelTwinTextBox();
                    srch.TwinTextBox = dmy;
                    var ret = srch.ShowDialog(this);
                    if (ret == true)
                    {
                        uctext.Text = srch.SelectedCodeList;
                        FocusControl.SetFocusWithOrder(new TraversalRequest(FocusNavigationDirection.Next));
                    }
                }
            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }
        }

        /// <summary>
        /// CSVファイル出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            int[] i支払先List = new int[0];
            if (!string.IsNullOrEmpty(支払先指定))
            {
                string[] 支払先List = 支払先指定.Split(',');
                i支払先List = new int[支払先List.Length];

                for (int i = 0; i < 支払先List.Length; i++)
                {
                    string str = 支払先List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "支払先指定の形式が不正です。";
                        return;
                    }
                    i支払先List[i] = code;
                }
            }

            base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST14020_CSV, new object[] { 得意先ID_FROM, 得意先ID_TO, i支払先List, 表示方法 }));
        }

        /// <summary>
        /// 印刷
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

            int[] i支払先List = new int[0];
            if (!string.IsNullOrEmpty(支払先指定))
            {
                string[] 支払先List = 支払先指定.Split(',');
                i支払先List = new int[支払先List.Length];

                for (int i = 0; i < 支払先List.Length; i++)
                {
                    string str = 支払先List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "支払先指定の形式が不正です。";
                        return;
                    }
                    i支払先List[i] = code;
                }
            }

            base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST14020, new object[] { 得意先ID_FROM, 得意先ID_TO, i支払先List, 表示方法 }));
        }

        /// <summary>
        /// F11 終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            this.Close();
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
