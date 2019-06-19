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
    /// 商品マスタ問合せ
    /// </summary>
    public partial class MST07020 : WindowMasterMainteBase
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
        public class ConfigMST07020 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST07020 frmcfg = null;

        #endregion

        #region 定数

        //CSV出力用定数
        private const string SEARCH_MST07020_CSV = "SEARCH_MST07020_CSV";
        //プレビュー出力用定数
        private const string SEARCH_MST07020 = "SEARCH_MST07020";
        //レポート
        private const string rptFullPathName_PIC = @"Files\MST\MST07010.rpt";
        
        #endregion

        #region バインド用プロパティ
        private string _商品コードFROM = string.Empty;
        public string 商品コードFROM
        {
            get { return this._商品コードFROM; }
            set { this._商品コードFROM = value; NotifyPropertyChanged(); }
        }
        private string _商品コードTO = string.Empty;
        public string 商品コードTO
        {
            get { return this._商品コードTO; }
            set { this._商品コードTO = value; NotifyPropertyChanged(); }
        }

        private string _商品指定 = string.Empty;
        public string 商品指定
        {
            get { return this._商品指定; }
            set { this._商品指定 = value; NotifyPropertyChanged(); }
        }



        private string _表示方法 = "0";
        public string 表示方法
        {
            get { return this._表示方法; }
            set { this._表示方法 = value; NotifyPropertyChanged(); }
        }

        private string _表示区分 = "0";
        public string 表示区分
        {
            get { return this._表示区分; }
            set { this._表示区分 = value; NotifyPropertyChanged(); }
        }

        private DataTable _mSTData;
        public DataTable MstData
        {
            get { return this._mSTData; }
            set { this._mSTData = value; NotifyPropertyChanged(); }
        }
        #endregion

        #region MST07020
        /// <summary>
        /// 商品マスタ問合せ
        /// </summary>
        public MST07020()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion

        #region Loadイベント
        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigMST07020)ucfg.GetConfigValue(typeof(ConfigMST07020));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST07020();
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

            //F1 検索取得
            base.MasterMaintenanceWindowList.Add("M09_HIN", new List<Type> { null, typeof(SCH07010) }); 

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
                case SEARCH_MST07020:
                        DispPreviw(tbl);
                        break;

                case SEARCH_MST07020_CSV:
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
                view.MakeReport("商品マスタ一覧", rptFullPathName_PIC, 0, 0, 0);
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
                    SCH07010 srch = new SCH07010();
                    switch (uctext.DataAccessName)
                    {
                        case "M09_HIN":
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
        /// F5 CSVファイル出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            int[] i商品List = new int[0];
            if (!string.IsNullOrEmpty(商品指定))
            {
                string[] 商品List = 商品指定.Split(',');
                i商品List = new int[商品List.Length];

                for (int i = 0; i < 商品List.Length; i++)
                {
                    string str = 商品List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "商品指定の形式が不正です。";
                        return;
                    }
                    i商品List[i] = code;
                }
            }

            //CSV出力用
            base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST07020_CSV, new object[] { 商品コードFROM, 商品コードTO, i商品List , 表示方法}));
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

            int[] i商品List = new int[0];
            if (!string.IsNullOrEmpty(商品指定))
            {
                string[] 商品List = 商品指定.Split(',');
                i商品List = new int[商品List.Length];

                for (int i = 0; i < 商品List.Length; i++)
                {
                    string str = 商品List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "商品指定の形式が不正です。";
                        return;
                    }
                    i商品List[i] = code;
                }
            }


            //帳票出力用
            base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_MST07020, new object[] { 商品コードFROM, 商品コードTO, i商品List, 表示方法}));
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
