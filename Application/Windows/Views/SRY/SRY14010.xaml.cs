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

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Reports.Preview;
using KyoeiSystem.Framework.Windows.ViewBase;


namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 輸送実績報告書
    /// </summary>
    public partial class SRY14010 : WindowReportBase
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
        public class ConfigSRY14010 : FormConfigBase
        {
            public string 開始作成年 { get; set; }
            public string 開始作成月 { get; set; }
            public string 終了作成年 { get; set; }
            public string 終了作成月 { get; set; }
            public int? 自社ID { get; set; }
            public DateTime? 集計期間From { get; set; }
            public DateTime? 集計期間To { get; set; }
            public int 区分1 { get; set; }
            public int 区分2 { get; set; }
            public int 区分3 { get; set; }
            public int 区分4 { get; set; }
            public int 区分5 { get; set; }
            public bool? チェック { get; set; }
        }
        /// ※ 必ず public で定義する。
        public ConfigSRY14010 frmcfg = null;

        #endregion

        #region 定数定義

        // データアクセス定義名
        private const string SEARCH_SRY14010 = "SEARCH_SRY14010";
        // データアクセス定義名
        private const string SEARCH_SRY14010_CSV = "SEARCH_SRY14010_CSV";
        // 車輌管理台帳　レポート定数
        private const string rptFullPathName_PIC = @"Files\SRY\SRY14010.rpt";

        #endregion

        #region バインド用プロパティ

        // データバインド用変数
        private string _車輌ピックアップ = string.Empty;
        public string 車輌ピックアップ
        {
            set { _車輌ピックアップ = value; NotifyPropertyChanged(); }
            get { return _車輌ピックアップ; }
        }
        private string _車輌From = string.Empty;
        public string 車輌From
        {
            set { _車輌From = value; NotifyPropertyChanged(); }
            get { return _車輌From; }
        }
        private string _車輌To = string.Empty;
        public string 車輌To
        {
            set { _車輌To = value; NotifyPropertyChanged(); }
            get { return _車輌To; }
        }

        private int? _作成年月From = null;
        public int? 作成年月From
        {
            set { _作成年月From = value; NotifyPropertyChanged(); }
            get { return _作成年月From; }
        }
        private int? _作成年月To = null;
        public int? 作成年月To
        {
            set { _作成年月To = value; NotifyPropertyChanged(); }
            get { return _作成年月To; }
        }

        private string _提出陸運局 = string.Empty;
        public string 提出陸運局
        {
            set { _提出陸運局 = value; NotifyPropertyChanged(); }
            get { return _提出陸運局; }
        }

        private int? _自社コード = null;
        public int? 自社コード
        {
            set { _自社コード = value; NotifyPropertyChanged(); }
            get { return _自社コード; }
        }
        private string _自社名 = string.Empty;
        public string 自社名
        {
            set { _自社名 = value; NotifyPropertyChanged(); }
            get { return _自社名; }
        }

        private string _自動車数 = string.Empty;
        public string 自動車数
        {
            set { _自動車数 = value; NotifyPropertyChanged(); }
            get { return _自動車数; }
        }

        private string _従業員数 = string.Empty;
        public string 従業員数
        {
            set { _従業員数 = value; NotifyPropertyChanged(); }
            get { return _従業員数; }
        }

        private string _運転者数 = string.Empty;
        public string 運転者数
        {
            set { _運転者数 = value; NotifyPropertyChanged(); }
            get { return _運転者数; }
        }

        private string _s作成年 = string.Empty;
        public string s作成年
        {
            set { _s作成年 = value; NotifyPropertyChanged(); }
            get { return _s作成年; }
        }

        private string _s作成月 = string.Empty;
        public string s作成月
        {
            set { _s作成月 = value; NotifyPropertyChanged(); }
            get { return _s作成月; }
        }

        private string _e作成年 = string.Empty;
        public string e作成年
        {
            set { _e作成年 = value; NotifyPropertyChanged(); }
            get { return _e作成年; }
        }

        private string _e作成月 = string.Empty;
        public string e作成月
        {
            set { _e作成月 = value; NotifyPropertyChanged(); }
            get { return _e作成月; }
        }

        private DateTime? _d請求From = null;
        public DateTime? d請求From
        {
            set { _d請求From = value; NotifyPropertyChanged(); }
            get { return _d請求From; }
        }

        private DateTime? _d請求To = null;
        public DateTime? d請求To
        {
            set { _d請求To = value; NotifyPropertyChanged(); }
            get { return _d請求To; }
        }

        #endregion

        #region SRY14010

        /// <summary>
        /// 輸送実績報告書
        /// </summary>
        public SRY14010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #endregion

        #region Load

        /// <summary>
        /// 画面読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigSRY14010)ucfg.GetConfigValue(typeof(ConfigSRY14010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigSRY14010();
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
                this.Width = frmcfg.Width;
                this.Height = frmcfg.Height;
                this.s作成年 = frmcfg.開始作成年;
                this.s作成月 = frmcfg.開始作成月;
                this.e作成年 = frmcfg.終了作成年;
                this.e作成月 = frmcfg.終了作成月;
                this.自社コード = frmcfg.自社ID;
                this.Combo1.SelectedIndex = frmcfg.区分1;

            }
            #endregion

            //F1 自社名
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { null, typeof(SCH12010) });
            //F1 車輌 
            base.MasterMaintenanceWindowList.Add("M05_CAR", new List<Type> { null, typeof(SCH06010) });
            //Cmb 運輸局
            AppCommon.SetutpComboboxList(this.Combo1, false);

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

            // 個別にエラー処理が必要な場合、ここに記述してください。

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
                    //締日プレビュー出力用
                    case SEARCH_SRY14010:
                        DispPreviw(tbl);
                        break;

                    //締日CSV出力用
                    case SEARCH_SRY14010_CSV:
                        OutPutCSV(tbl);
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
                view.MakeReport("輸送実績報告書", rptFullPathName_PIC, 0, 0, 0);
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
        /// F1 リボン　検索
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
            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }

            if (string.IsNullOrEmpty(s作成年) || string.IsNullOrEmpty(s作成月) || string.IsNullOrEmpty(e作成年) || string.IsNullOrEmpty(e作成月))
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                MessageBox.Show("作成年月は入力必須項目です。");
                return;
            }

            if (自社コード == null)
            {
                this.ErrorMessage = "自社コードは入力必須項目です。";
                MessageBox.Show("自社コードは入力必須項目です。");
                return;
            }

            int Cmb_Value;
            Cmb_Value = Combo1.SelectedIndex;
            WeekDay();            
            //支払先リスト作成
            int?[] i車輌List = new int?[0];
            if (!string.IsNullOrEmpty(車輌ピックアップ))
            {
                string[] 車輌List = 車輌ピックアップ.Split(',');
                i車輌List = new int?[車輌List.Length];

                for (int i = 0; i < 車輌List.Length; i++)
                {
                    string str = 車輌List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "支払先指定の形式が不正です。";
                        return;
                    }
                    i車輌List[i] = code;
                }
            }

            //帳票出力用　//String , String , Int , string , string , Int , Int , String , String , String
			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SRY14010_CSV, new object[] { 車輌From , 車輌To , i車輌List , 
                                                                                                                作成年月From , 作成年月To ,
                                                                                                                Cmb_Value , 自社コード , 自動車数,
                                                                                                                従業員数 , 運転者数,
                                                                                                                d請求From , d請求To
                                                                                                                }));
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


            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }

            if (string.IsNullOrEmpty(s作成年) || string.IsNullOrEmpty(s作成月) || string.IsNullOrEmpty(e作成年) || string.IsNullOrEmpty(e作成月))
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                MessageBox.Show("作成年月は入力必須項目です。");
                return;
            }

            if (自社コード == null)
            {
                this.ErrorMessage = "自社コードは入力必須項目です。";
                MessageBox.Show("自社コードは入力必須項目です。");
                return;
            }

            int Cmb_Value;
            Cmb_Value = Combo1.SelectedIndex;
            WeekDay();
            //支払先リスト作成
            int?[] i車輌List = new int?[0];
            if (!string.IsNullOrEmpty(車輌ピックアップ))
            {
                string[] 車輌List = 車輌ピックアップ.Split(',');
                i車輌List = new int?[車輌List.Length];

                for (int i = 0; i < 車輌List.Length; i++)
                {
                    string str = 車輌List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "支払先指定の形式が不正です。";
                        return;
                    }
                    i車輌List[i] = code;
                }
            }

            //帳票出力用　//String , String , Int , stirng , string , Int , Int , String , String , String
			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SRY14010, new object[] { 車輌From , 車輌To , i車輌List , 
                                                                                                             作成年月From , 作成年月To ,
                                                                                                             Cmb_Value　, 自社コード , 自動車数,
                                                                                                             従業員数 , 運転者数,
                                                                                                             d請求From , d請求To  }));
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

        #region 日付処理
       
        public void WeekDay()
        {
            //作成年月From ～ To
            作成年月From = Convert.ToInt32(s作成年 + s作成月);
            作成年月To = Convert.ToInt32(e作成年 + e作成月);
            d請求From = Convert.ToDateTime(s作成年 + "/" + s作成月 + "/" + "01");
            d請求To = Convert.ToDateTime(e作成年 + "/" + e作成月 + "/" + "01").AddMonths(+1).AddDays(-1);
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



        #region 画面保持
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            frmcfg.開始作成年 = this.s作成年;
            frmcfg.開始作成月 = this.s作成月;
            frmcfg.終了作成年 = this.e作成年;
            frmcfg.終了作成月 = this.e作成月;
            frmcfg.自社ID = this.自社コード;
            frmcfg.区分1 = this.Combo1.SelectedIndex;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion
    }
}
