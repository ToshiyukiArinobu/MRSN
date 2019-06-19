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
using System.Data.SqlClient;
using System.Data;

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;
using KyoeiSystem.Framework.Windows.Controls;




namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 車種マスタ問合せ
    /// </summary>
    public partial class MST05020 : WindowMasterMainteBase
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
        public class ConfigMST05020 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST05020 frmcfg = null;

        #endregion

        #region 定数
        //車種情報検索用
        private const string TargetTableNm = "M06_SYA_Kensaku";
        //CSV出力用
        private const string SEARCH_MST05010_CSV = "SEARCH_MST05010_CSV";
        //プレビュー出力用
        private const string SEARCH_MST05010 = "SEARCH_MST05010";
        //プレビュー用
        private const string rptFullPathName_PIC = @"Files\MST\MST05010.rpt";
        #endregion

        #region バインド用プロパティ

        //入力用に車種IDFROMを生成
        private string _車種IDFROM = string.Empty;
        public string 車種IDFROM
        {
            get { return this._車種IDFROM; }
            set { this._車種IDFROM = value; NotifyPropertyChanged(); }
        }

        //入力用に車種IDTOを生成
        private string _車種IDTO = string.Empty;
        public string 車種IDTO
        {
            get { return this._車種IDTO; }
            set { this._車種IDTO = value; NotifyPropertyChanged(); }
        }

        private string _車種指定 = string.Empty;
        public string 車種指定
        {
            get { return this._車種指定; }
            set { this._車種指定 = value; NotifyPropertyChanged(); }
        }

        //画面表示用にテーブル格納領域を生成
        private DataTable _mSTData;
        public DataTable MstData
        {
            get { return this._mSTData; }
            set
            {
                this._mSTData = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region MST05020
        /// <summary>
        /// 車種マスタ問合せ
        /// </summary>
        public MST05020()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion

        #region LOADイベント
        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigMST05020)ucfg.GetConfigValue(typeof(ConfigMST05020));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST05020();
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

            ResetAllValidation();

            SetFocusToTopControl();
            //削除データ以外を全権表示
            //SearchColumn();
            //検索画面追加
            base.MasterMaintenanceWindowList.Add("M06_SYA", new List<Type> { null, typeof(SCH05010) });

        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchColumn();
        }
        #endregion

        #region 一覧表示処理
        /// <summary>
        /// 接続とDataSetの作成
        /// </summary>
        void SearchColumn()
        {
            try
            {
                int SharyouFrom = int.MinValue;
                int SharyouTo = int.MaxValue;


                if (!int.TryParse(車種IDFROM, out SharyouFrom))
                {
                    SharyouFrom = int.MinValue;
                }

                if (!int.TryParse(車種IDTO, out SharyouTo))
                {
                    SharyouTo = int.MaxValue;
                }


                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { SharyouFrom, SharyouTo}));
            }
            catch (Exception)
            {
                return;
            }
        }
        #endregion

        #region プレビュー処理
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
                view.MakeReport("車種マスタ一覧表", rptFullPathName_PIC, 0, 0, 0);
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

        #region CSVファイル処理
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

        #region 受信データ
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            var data = message.GetResultData();
            DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
            switch (message.GetMessageName())
            {
                //検索データ出力
                case TargetTableNm:
                    MstData = tbl;
                    break;

                //プレビュー出力
                case SEARCH_MST05010:
                    DispPreviw(tbl);
                    break;
                
                //CSV出力
                case SEARCH_MST05010_CSV:
                    OutPutCSV(tbl);
                    break;

                default:
                    break;
            }
            
        }
        #endregion

        #region エラー処理
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            MessageBox.Show(ErrorMessage);
        }
        #endregion

        #region リボン
        /// <summary>
        /// F1 リボン　検索
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
                    SCH05010 srch = new SCH05010();
                    switch (uctext.DataAccessName)
                    {
                        case "M06_SYA":
                            srch.MultiSelect = true;
                            break;
                        default:
                            srch.MultiSelect = false;
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
            int[] i車種List = new int[0];
            if (!string.IsNullOrEmpty(車種指定))
            {
                string[] 車種List = 車種指定.Split(',');
                i車種List = new int[車種List.Length];

                for (int i = 0; i < 車種List.Length; i++)
                {
                    string str = 車種List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "車種指定の形式が不正です。";
                        return;
                    }
                    i車種List[i] = code;
                }
            }
            //Todo: 条件渡し
            base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST05010_CSV, new object[] { 車種IDFROM, 車種IDTO, i車種List }));
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

            int[] i車種List = new int[0];
            if (!string.IsNullOrEmpty(車種指定))
            {
                string[] 車種List = 車種指定.Split(',');
                i車種List = new int[車種List.Length];

                for (int i = 0; i < 車種List.Length; i++)
                {
                    string str = 車種List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "車種指定の形式が不正です。";
                        return;
                    }
                    i車種List[i] = code;
                }
            }
            //Todo: 条件渡し
            base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST05010, new object[] { 車種IDFROM, 車種IDTO, i車種List }));
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
