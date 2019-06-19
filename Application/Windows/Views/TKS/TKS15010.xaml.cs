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

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;
using KyoeiSystem.Framework.Windows.Controls;


namespace KyoeiSystem.Application.Windows.Views
{

    /// <summary>
    /// 得意先売上明細書画面
    /// </summary>
    public partial class TKS15010 : WindowReportBase
    {
        #region 定義
        //CSV出力用定数
        private const string SEARCH_TKS15010_CSV = "SEARCH_TKS15010_CSV";
        //プレビュー出力用定数
        private const string SEARCH_TKS15010 = "SEARCH_TKS15010";
        //レポート
        private const string rptFullPathName_PIC = @"Files\TKS\TKS15010_T.rpt";
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
        public class ConfigTKS15010 : FormConfigBase
        {
            public int? 作成年 { get; set; }
            public int? 作成月 { get; set; }
            public int? 累計年 { get; set; }
            public int? 累計月 { get; set; }
            public string 締日 { get; set; }
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
        public ConfigTKS15010 frmcfg = null;

        #endregion

        #region バインド用プロパティ

        private string _得意先ピックアップ = string.Empty;
        public string 得意先ピックアップ
        {
            set { _得意先ピックアップ = value; NotifyPropertyChanged(); }
            get { return _得意先ピックアップ; }
        }
        private string _得意先From = string.Empty;
        public string 得意先From
        {
            set { _得意先From = value; NotifyPropertyChanged(); }
            get { return _得意先From; }
        }
        private string _得意先To = string.Empty;
        public string 得意先To
        {
            set { _得意先To = value; NotifyPropertyChanged(); }
            get { return _得意先To; }
        }

        private int? _累計開始年 = null;
        public int? 累計開始年
        {
            set { _累計開始年 = value; NotifyPropertyChanged(); }
            get { return _累計開始年; }
        }

        private int? _累計開始月 = null;
        public int? 累計開始月
        {
            set { _累計開始月 = value; NotifyPropertyChanged(); }
            get { return _累計開始月; }
        }

        private int? _作成年 = null;
        public int? 作成年
        {
            set { _作成年 = value; NotifyPropertyChanged(); }
            get { return _作成年; }
        }

        private int? _作成月 = null;
        public int? 作成月
        {
            set { _作成月 = value; NotifyPropertyChanged(); }
            get { return _作成月; }
        }

        private bool _全締日集計 = true;
        public bool 全締日集計
        {
            set { _全締日集計 = value; NotifyPropertyChanged(); }
            get { return _全締日集計; }
        }

        private string _作成締日 = string.Empty;
        public string 作成締日
        {
            set { _作成締日 = value; NotifyPropertyChanged(); }
            get { return _作成締日; }
        }

        //private string _第一順序;
        //public string 第一順序
        //{
        //    set { _第一順序 = value; NotifyPropertyChanged(); }
        //    get { return _第一順序; }
        //}

        //private string _第二順序;
        //public string 第二順序
        //{
        //    set { _第二順序 = value; NotifyPropertyChanged(); }
        //    get { return _第二順序; }
        //}

        //private string _第三順序 = "3";
        //public string 第三順序
        //{
        //    set { _第三順序 = value; NotifyPropertyChanged(); }
        //    get { return _第三順序; }
        //}

        private int _作成区分;
        public int 作成区分
        {
            set { _作成区分 = value; NotifyPropertyChanged(); }
            get { return _作成区分; }
        }

        private DateTime? _集計期間From = null;
        public DateTime? 集計期間From
        {
            set { _集計期間From = value; NotifyPropertyChanged(); }
            get { return _集計期間From; }
        }
        private DateTime? _集計期間To = null;
        public DateTime? 集計期間To
        {
            set { _集計期間To = value; NotifyPropertyChanged(); }
            get { return _集計期間To; }
        }

        private int? _当月集計 = null;
        public int? 当月集計
        {
            set { _当月集計 = value; NotifyPropertyChanged(); }
            get { return _当月集計; }
        }

        private int? _累計集計From = null;
        public int? 累計集計From
        {
            set { _累計集計From = value; NotifyPropertyChanged(); }
            get { return _累計集計From; }
        }

        private int? _累計集計To = null;
        public int? 累計集計To
        {
            set { _累計集計To = value; NotifyPropertyChanged(); }
            get { return _累計集計To; }
        }

        private int? _前月集計 = null;
        public int? 前月集計
        {
            set { _前月集計 = value; NotifyPropertyChanged(); }
            get { return _前月集計; }
        }

        private int? _前年集計 = null;
        public int? 前年集計
        {
            set { _前年集計 = value; NotifyPropertyChanged(); }
            get { return _前年集計; }
        }

        private int? _前々年集計 = null;
        public int? 前々年集計
        {
            set { _前々年集計 = value; NotifyPropertyChanged(); }
            get { return _前々年集計; }
        }

        private int _取引区分 = 1;
        public int 取引区分
        {
            get { return this._取引区分; }
            set { this._取引区分 = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region TKS15010
        /// <summary>
        /// 得意先売上明細書
        /// </summary>
        public TKS15010()
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
            frmcfg = (ConfigTKS15010)ucfg.GetConfigValue(typeof(ConfigTKS15010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigTKS15010();
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
                this.作成締日 = frmcfg.締日;
                this.作成年 = frmcfg.作成年;
                this.作成月 = frmcfg.作成月;
                this.累計開始年 = frmcfg.累計年;
                this.累計開始月 = frmcfg.累計月;
                this.Combo2.SelectedIndex = frmcfg.区分2;
            }
            #endregion

            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { null, typeof(SCH01010) });
            //作成区分コンボボックス
            AppCommon.SetutpComboboxList(this.Combo2, false);
            作成区分 = 0;




        }
        #endregion

        #region 画面初期化
        private void ScreenClear()
        {

        }
        #endregion

        #region エラー受信
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            this.ErrorMessage = (string)message.GetResultData();
        }
        #endregion

        #region データ受信メソッド
        /// <summary>
        /// 取得データの取り込み
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            try
            {
                this.ErrorMessage = string.Empty;
                var data = message.GetResultData();
                DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
                switch (message.GetMessageName())
                {
                    //検索結果取得時
                    case SEARCH_TKS15010:
                        DataSet ds = (data is DataSet) ? (data as DataSet) : null;
                        if (ds == null)
                        {
                            return;
                        }
                        DispPreviw(ds);
                        break;

                    case SEARCH_TKS15010_CSV:
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
                    srch.multi = 1;
                    srch.表示区分 = 1;
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

            #region 日付処理


            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }

            if (!string.IsNullOrEmpty(作成締日) && 全締日集計 == true)
            {
                this.ErrorMessage = "作成締日、全締日は同時に入力できません。";
                MessageBox.Show("作成締日、全締日は同時に入力できません。");
                return;
            }
            else if (string.IsNullOrEmpty(作成締日) && 全締日集計 == false)
            {
                this.ErrorMessage = "締日を入力してください。";
                MessageBox.Show("締日を入力してください。");
                return;
            }

            if (累計開始年 == null || 累計開始月 == null)
            {
                this.ErrorMessage = "累計開始年月は必須入力項目です。";
                MessageBox.Show("累計開始年月は必須入力項目です。");
                return;
            }

            if (作成年 == null || 作成月 == null)
            {
                this.ErrorMessage = "作成年月は必須入力項目です。";
                MessageBox.Show("作成年月は必須入力項目です。");
                return;
            }



            string i作成年月日, i累計開始年月日;

            //開始日
            i累計開始年月日 = 累計開始年.ToString() + "/" + 累計開始月.ToString() + "/" + "01";
            集計期間From = Convert.ToDateTime(i累計開始年月日);

            //終了日
            i作成年月日 = 作成年.ToString() + "/" + 作成月.ToString() + "/" + "01";
            集計期間To = Convert.ToDateTime(i作成年月日).AddMonths(+1).AddDays(-1);

            string s作成年月, s作成年, s作成月, s当月年月, s前月年月, s前年年月, s前々年年月, s累計年月From;
            DateTime d当月年月, d前月年月, d前年年月, d前々年年月, d累計年月From;

            s作成年 = Convert.ToString(作成年);
            s作成月 = Convert.ToString(作成月);
            s作成年月 = s作成年 + s作成月;

            d当月年月 = Convert.ToDateTime(i作成年月日);
            d前月年月 = Convert.ToDateTime(i作成年月日).AddMonths(-1);
            d前年年月 = Convert.ToDateTime(i作成年月日).AddYears(-1);
            d前々年年月 = Convert.ToDateTime(i作成年月日).AddYears(-2);
            d累計年月From = Convert.ToDateTime(i累計開始年月日);

            s当月年月 = Convert.ToString(d当月年月);
            s前月年月 = Convert.ToString(d前月年月);
            s前年年月 = Convert.ToString(d前年年月);
            s前々年年月 = Convert.ToString(d前々年年月);
            s累計年月From = Convert.ToString(d累計年月From);

            s当月年月 = s当月年月.Substring(0, 4) + s当月年月.Substring(5, 2);
            s前月年月 = s前月年月.Substring(0, 4) + s前月年月.Substring(5, 2);
            s前年年月 = s前年年月.Substring(0, 4) + s前年年月.Substring(5, 2);
            s前々年年月 = s前々年年月.Substring(0, 4) + s前々年年月.Substring(5, 2);
            s累計年月From = s累計年月From.Substring(0, 4) + s累計年月From.Substring(5, 2);

            当月集計 = Convert.ToInt32(s当月年月);
            前月集計 = Convert.ToInt32(s前月年月);
            前年集計 = Convert.ToInt32(s前年年月);
            前々年集計 = Convert.ToInt32(s前々年年月);
            累計集計From = Convert.ToInt32(s累計年月From);
            累計集計To = Convert.ToInt32(s当月年月);

            #endregion

            //コンボボックスのIndexを取得
            int Cmd_Order1;
            Cmd_Order1 = Combo2.SelectedIndex;      //表示順序1

            //コンボボックスの中身を取得
            string Cmb_SelectItem;

            //支払先リスト作成
            int?[] i得意先List = new int?[0];
            if (!string.IsNullOrEmpty(得意先ピックアップ))
            {
                string[] 得意先List = 得意先ピックアップ.Split(',');
                i得意先List = new int?[得意先List.Length];

                for (int i = 0; i < 得意先List.Length; i++)
                {
                    string str = 得意先List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "得意先指定の形式が不正です。";
                        return;
                    }
                    i得意先List[i] = code;
                }
            }
            //帳票出力用
			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_TKS15010_CSV, new object[] { 得意先From,
                                                                                                                得意先To,
                                                                                                                i得意先List,
                                                                                                                作成締日,
                                                                                                                全締日集計,
                                                                                                                作成年,
                                                                                                                作成月,
                                                                                                                Cmd_Order1,
                                                                                                                当月集計,
                                                                                                                累計集計From,
                                                                                                                累計集計To,
                                                                                                                前月集計,
                                                                                                                前年集計,
                                                                                                                前々年集計,
                                                                                                                集計期間From,
                                                                                                                集計期間To,
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


            #region 日付処理


            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }


            if (作成年 == null || 作成月 == null)
            {
                this.ErrorMessage = "作成年月は必須入力項目です。";
                MessageBox.Show("作成年月は必須入力項目です。");
                return;
            }

            int 作成年月;
            if (作成月.ToString().Length == 1)
            {
                作成年月 = AppCommon.IntParse(作成年.ToString()+"0"+作成月.ToString());
            }else
            {
                作成年月 = AppCommon.IntParse(作成年.ToString()+作成月.ToString());
            }

            #endregion


            //帳票出力用
			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_TKS15010, new object[] {    作成年月
                                                                                                                }));
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
            frmcfg.締日 = this.作成締日;
            frmcfg.作成年 = this.作成年;
            frmcfg.作成月 = this.作成月;
            frmcfg.累計年 = this.累計開始年;
            frmcfg.累計月 = this.累計開始月;
            frmcfg.区分2 = this.Combo2.SelectedIndex;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion

        #region プレビュー画面
        /// <summary>
        /// プレビュー画面表示
        /// </summary>
        /// <param name="tbl"></param>
        private void DispPreviw(DataSet ds)
        {
            try
            {
                //if (ds.Tables["売上構成グラフ"].Rows.Count == 0)
                //{
                //    this.ErrorMessage = "対象データが存在しません。";
                //    return;
                //}
                //if (ds.Tables["得意先上位グラフ"].Rows.Count == 0)
                //{
                //    this.ErrorMessage = "対象データが存在しません。";
                //    return;
                //}

                //印刷処理
                KyoeiSystem.Framework.Reports.Preview.ReportPreview view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
                //第1引数　帳票タイトル
                //第2引数　帳票ファイルPass
                //第3以上　帳票の開始点(0で良い)
                view.MakeReport("売上分析グラフ", rptFullPathName_PIC, 0, 0, 0);
                //帳票ファイルに送るデータ。
                //帳票データの列と同じ列名を保持したDataTableを引数とする
				view.SetReportData(ds);
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

        #region 日付処理

        //累計開始年
        private void rLost_Year(object sender, RoutedEventArgs e)
        {
            if (累計開始年 == null)
            {
                string Date;
                int iDate;
                DateTime YYYY;
                YYYY = DateTime.Today;
                Date = Convert.ToString(YYYY);
                Date = Date.Substring(0, 4);
                iDate = Convert.ToInt32(Date);
                累計開始年 = iDate;
            }
        }

        //累計開始月
        private void rLost_Month(object sender, RoutedEventArgs e)
        {
            if (累計開始月 == null)
            {
                string Date;
                int iDate;
                DateTime MM;
                MM = DateTime.Today;
                Date = Convert.ToString(MM);
                Date = Date.Substring(5, 2);
                iDate = Convert.ToInt32(Date);
                累計開始月 = iDate;
            }
            else if (累計開始月 == 0 || 累計開始月 > 12)
            {
                累計開始月 = null;
                this.ErrorMessage = "入力値エラーです。もう一度入力してください。";
                MessageBox.Show("入力値エラーです。もう一度入力してください。");

                return;
            }
        }

        //作成年
        private void sLost_Year(object sender, RoutedEventArgs e)
        {
            if (作成年 == null)
            {
                string Date;
                int iDate;
                DateTime YYYY;
                YYYY = DateTime.Today;
                Date = Convert.ToString(YYYY);
                Date = Date.Substring(0, 4);
                iDate = Convert.ToInt32(Date);
                作成年 = iDate;
            }

        }

        //作成月
        private void sLost_Month(object sender, RoutedEventArgs e)
        {
            if (作成月 == null)
            {
                string Date;
                int iDate;
                DateTime MM;
                MM = DateTime.Today;
                Date = Convert.ToString(MM);
                Date = Date.Substring(5, 2);
                iDate = Convert.ToInt32(Date);
                作成月 = iDate;
            }
            else if (作成月 == 0 || 作成月 > 12)
            {
                this.ErrorMessage = "入力値エラーです。もう一度入力してください。";
                MessageBox.Show("入力値エラーです。もう一度入力してください。");
                作成月 = null;
                return;
            }
        }

        //締日
        private void Lost_Shime(object sender, RoutedEventArgs e)
        {
            string i作成年月日, i累計開始年月日;

            if (!string.IsNullOrEmpty(作成締日))
            {
                int i作成締日 = Convert.ToInt32(作成締日);
                if (i作成締日 >= 1 && i作成締日 <= 31)
                {

                    //開始日
                    i累計開始年月日 = 累計開始年.ToString() + "/" + 累計開始月.ToString() + "/" + "01";
                    集計期間From = Convert.ToDateTime(i累計開始年月日);

                    //終了日
                    i作成年月日 = 作成年.ToString() + "/" + 作成月.ToString() + "/" + "01";
                    集計期間To = Convert.ToDateTime(i作成年月日).AddMonths(+1).AddDays(-1);

                    string s作成年月, s作成年, s作成月, s当月年月, s前月年月, s前年年月, s前々年年月, s累計年月From;
                    DateTime d当月年月, d前月年月, d前年年月, d前々年年月, d累計年月From;

                    s作成年 = Convert.ToString(作成年);
                    s作成月 = Convert.ToString(作成月);
                    s作成年月 = s作成年 + s作成月;

                    d当月年月 = Convert.ToDateTime(i作成年月日);
                    d前月年月 = Convert.ToDateTime(i作成年月日).AddMonths(-1);
                    d前年年月 = Convert.ToDateTime(i作成年月日).AddYears(-1);
                    d前々年年月 = Convert.ToDateTime(i作成年月日).AddYears(-2);
                    d累計年月From = Convert.ToDateTime(i累計開始年月日);

                    s当月年月 = Convert.ToString(d当月年月);
                    s前月年月 = Convert.ToString(d前月年月);
                    s前年年月 = Convert.ToString(d前年年月);
                    s前々年年月 = Convert.ToString(d前々年年月);
                    s累計年月From = Convert.ToString(d累計年月From);

                    s当月年月 = s当月年月.Substring(0, 4) + s当月年月.Substring(5, 2);
                    s前月年月 = s前月年月.Substring(0, 4) + s前月年月.Substring(5, 2);
                    s前年年月 = s前年年月.Substring(0, 4) + s前年年月.Substring(5, 2);
                    s前々年年月 = s前々年年月.Substring(0, 4) + s前々年年月.Substring(5, 2);
                    s累計年月From = s累計年月From.Substring(0, 4) + s累計年月From.Substring(5, 2);

                    当月集計 = Convert.ToInt32(s当月年月);
                    前月集計 = Convert.ToInt32(s前月年月);
                    前年集計 = Convert.ToInt32(s前年年月);
                    前々年集計 = Convert.ToInt32(s前々年年月);
                    累計集計From = Convert.ToInt32(s累計年月From);
                    累計集計To = Convert.ToInt32(s当月年月);
                }



            }
            else
            {

                //開始日
                i累計開始年月日 = 累計開始年.ToString() + "/" + 累計開始月.ToString() + "/" + "01";
                集計期間From = Convert.ToDateTime(i累計開始年月日);

                //終了日
                i作成年月日 = 作成年.ToString() + "/" + 作成月.ToString() + "/" + "01";
                集計期間To = Convert.ToDateTime(i作成年月日).AddMonths(+1).AddDays(-1);

                string s作成年月, s作成年, s作成月, s当月年月, s前月年月, s前年年月, s前々年年月, s累計年月From;
                DateTime d当月年月, d前月年月, d前年年月, d前々年年月, d累計年月From;

                s作成年 = Convert.ToString(作成年);
                s作成月 = Convert.ToString(作成月);
                s作成年月 = s作成年 + s作成月;

                d当月年月 = Convert.ToDateTime(i作成年月日);
                d前月年月 = Convert.ToDateTime(i作成年月日).AddMonths(-1);
                d前年年月 = Convert.ToDateTime(i作成年月日).AddYears(-1);
                d前々年年月 = Convert.ToDateTime(i作成年月日).AddYears(-2);
                d累計年月From = Convert.ToDateTime(i累計開始年月日);

                s当月年月 = Convert.ToString(d当月年月);
                s前月年月 = Convert.ToString(d前月年月);
                s前年年月 = Convert.ToString(d前年年月);
                s前々年年月 = Convert.ToString(d前々年年月);
                s累計年月From = Convert.ToString(d累計年月From);

                s当月年月 = s当月年月.Substring(0, 4) + s当月年月.Substring(5, 2);
                s前月年月 = s前月年月.Substring(0, 4) + s前月年月.Substring(5, 2);
                s前年年月 = s前年年月.Substring(0, 4) + s前年年月.Substring(5, 2);
                s前々年年月 = s前々年年月.Substring(0, 4) + s前々年年月.Substring(5, 2);
                s累計年月From = s累計年月From.Substring(0, 4) + s累計年月From.Substring(5, 2);

                当月集計 = Convert.ToInt32(s当月年月);
                前月集計 = Convert.ToInt32(s前月年月);
                前年集計 = Convert.ToInt32(s前年年月);
                前々年集計 = Convert.ToInt32(s前々年年月);
                累計集計From = Convert.ToInt32(s累計年月From);
                累計集計To = Convert.ToInt32(s当月年月);
            }
        }


        #endregion

        #region F8
        private void Combo2_PreviewKeyDown(object sender, KeyEventArgs e)
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