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
    using WinForms = System.Windows.Forms;
    using FrameworkWinControls = Framework.Windows.Controls;
    using KyoeiReports = KyoeiSystem.Framework.Reports;

    /// <summary>
    /// 商品群マスタ問合せ
    /// </summary>
    public partial class MST04022_1 : WindowMasterMainteBase
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
        public class ConfigMst04022_1 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMst04022_1 frmcfg = null;

        #endregion

        /// <summary>CSVデータ抽出</summary>
        private const string TargetTableNmCsv = "M16_HINGUN_GetCsv";
        /// <summary>帳票データ抽出</summary>
        private const string TargetTableNmRpt = "M16_HINGUN_GetRpt";
        /// <summary>帳票定義格納パス</summary>
        private const string tempReportFilePath = @"Files\MST\MST04022_1.rpt";

        private string _コードFROM = string.Empty;
        public string コードFROM
        {
            get { return this._コードFROM; }
            set { this._コードFROM = value; NotifyPropertyChanged(); }
        }
        private string _コードTO = string.Empty;
        public string コードTO
        {
            get { return this._コードTO; }
            set { this._コードTO = value; NotifyPropertyChanged(); }
        }

        private string _商品群指定 = string.Empty;
        public string 商品群指定
        {
            get { return this._商品群指定; }
            set { this._商品群指定 = value; NotifyPropertyChanged(); }
        }

        private string _表示方法 = "0";
        public string 表示方法
        {
            get { return this._表示方法; }
            set { this._表示方法 = value; NotifyPropertyChanged(); }
        }

        private string _PrinterName;
        public string PrinterName
        {
            get { return this._PrinterName; }
            set { this._PrinterName = value; NotifyPropertyChanged(); }
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
        /// 商品群マスタ問合せ
        /// </summary>
        public MST04022_1()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigMst04022_1)ucfg.GetConfigValue(typeof(ConfigMst04022_1));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMst04022_1();
                ucfg.SetConfigValue(frmcfg);
            }
            else
            {
                // 表示できるかチェック
                var WidthCHK = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - frmcfg.Left;
                if (WidthCHK > 10)
                {
                    this.Left = frmcfg.Left;
                }
                // 表示できるかチェック
                var HeightCHK = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - frmcfg.Top;
                if (HeightCHK > 10)
                {
                    this.Top = frmcfg.Top;
                }
                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;
                PrinterName = frmcfg.PrinterName;
            }
            #endregion

            base.MasterMaintenanceWindowList.Add("M16_HINGUN", new List<Type> { null, typeof(SCHM16_HINGUN) });

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
                // 検索結果取得時
                case TargetTableNmRpt:
                    // 帳票出力
                    DispPreviw(tbl);
                    break;

                case TargetTableNmCsv:
                    // CSV出力
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
                // 印刷処理
                KyoeiReports.Preview.ReportPreview view = new KyoeiReports.Preview.ReportPreview();
                view.PrinterName = PrinterName;
                // 第1引数　帳票タイトル
                // 第2引数　帳票ファイルPass
                // 第3以上　帳票の開始点(0で良い)
                view.MakeReport("商品群マスタリスト", tempReportFilePath, 0, 0, 0);
                
                // 帳票ファイルに送るデータ。
                // 帳票データの列と同じ列名を保持したDataTableを引数とする
                view.SetReportData(tbl);
                view.PrinterName = frmcfg.PrinterName;
                view.ShowPreview();
                view.Close();
                frmcfg.PrinterName = view.PrinterName;

                PrinterName = view.PrinterName;

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
            WinForms.SaveFileDialog sfd = new WinForms.SaveFileDialog();
            // はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = @"C:\";
            // [ファイルの種類]に表示される選択肢を指定する
            sfd.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
            // 「CSVファイル」が選択されているようにする
            sfd.FilterIndex = 1;
            // タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください";
            // ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
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

                    SCHM16_HINGUN srch = new SCHM16_HINGUN();
                    switch (uctext.DataAccessName)
                    {
                        case "SCH_M16HINGUN":
                            srch.MultiSelect = true;
                            break;
                        default:
                            srch.MultiSelect = false;
                            break;
                    }

                    srch.TwinTextBox = new FrameworkWinControls.UcLabelTwinTextBox();
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
            string[] str商品群List = getHingunNameArray();

            base.SendRequest(new CommunicationObject(
                MessageType.UpdateData,
                TargetTableNmCsv,
                new object[] {
                    コードFROM,
                    コードTO,
                    str商品群List,
                    表示方法
                }));

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

            string[] str商品群List = getHingunNameArray();

            base.SendRequest(new CommunicationObject(
                MessageType.UpdateData,
                TargetTableNmRpt,
                new object[] {
                    コードFROM,
                    コードTO,
                    str商品群List,
                    表示方法
                }));

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

        /// <summary>
        /// 商品群名のカンマ区切りを配列で取得する
        /// </summary>
        /// <returns></returns>
        private string[] getHingunNameArray()
        {
            string[] nameArray = new string[0];
            if (!string.IsNullOrEmpty(商品群指定))
            {
                // 区切り内容を変数に設定
                // REMARKS:カンマでも句読点でも対応
                nameArray = 商品群指定.Split(new char[] { ',', '、' });

            }

            return nameArray;

        }

    }

}
