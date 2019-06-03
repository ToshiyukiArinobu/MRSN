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
    /// 支払先売上明細書画面
    /// </summary>
    public partial class SHR09010 : WindowReportBase
    {
        #region データアクセスID
        //支払先締日集計プレビュー定数
        private const string SEARCH_SHR09010 = "SEARCH_SHR09010";
        //支払先月次集計プレビュー定数
        private const string SEARCH_SHR09010_CSV = "SEARCH_SHR09010_CSV";
        //支払先売上合計表レポート定数
        private const string rptFullPathName_PIC = @"Files\SHR\SHR09010.rpt";
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
        public class ConfigSRY11010 : FormConfigBase
        {
            public string 作成年 { get; set; }
            public string 作成月 { get; set; }
            public DateTime? 集計期間From { get; set; }
            public DateTime? 集計期間To { get; set; }
        }

        /// ※ 必ず public で定義する。
        public ConfigSRY11010 frmcfg = null;

        #endregion

        #region バインド

        //データバインド用変数

        //支払先ピックアップ
        string _支払先ピックアップ = string.Empty;
        public string 支払先ピックアップ
        {
            get { return this._支払先ピックアップ; }
            set
            {
                this._支払先ピックアップ = value;
                NotifyPropertyChanged();
            }
        }

        //支払先範囲指定From
        string _支払先From = string.Empty;
        public string 支払先From
        {
            get { return this._支払先From; }
            set
            {
                this._支払先From = value;
                NotifyPropertyChanged();
            }
        }

        //支払先範囲指定To
        string _支払先To = string.Empty;
        public string 支払先To
        {
            get { return this._支払先To; }
            set
            {
                this._支払先To = value;
                NotifyPropertyChanged();
            }
        }

        //作成締日
        string _作成集金日 = null;
        public string 作成集金日
        {
            get { return this._作成集金日; }
            set
            {
                this._作成集金日 = value;
                NotifyPropertyChanged();
            }
        }

        //作成年
        string _作成年 = string.Empty;
        public string 作成年
        {
            get { return this._作成年; }
            set
            {
                this._作成年 = value;
                NotifyPropertyChanged();
            }
        }

        //作成月
        string _作成月 = string.Empty;
        public string 作成月
        {
            get { return this._作成月; }
            set
            {
                this._作成月 = value;
                NotifyPropertyChanged();
            }
        }

        //集計年月
        int? _集計年月 = null;
        public int? 集計年月
        {
            get { return this._集計年月; }
            set
            {
                this._集計年月 = value;
                NotifyPropertyChanged();
            }
        }

        //集計期間From
        DateTime? _集計期間From;
        public DateTime? 集計期間From
        {
            get { return this._集計期間From; }
            set
            {
                this._集計期間From = value;
                NotifyPropertyChanged();
            }
        }

        //集計期間To
        DateTime? _集計期間To;
        public DateTime? 集計期間To
        {
            get { return this._集計期間To; }
            set
            {
                this._集計期間To = value;
                NotifyPropertyChanged();
            }
        }

        //全締日集計
        private bool _全集金日 = true;
        public bool 全集金日
        {
            set
            {
                _全集金日 = value;
                NotifyPropertyChanged();
            }
            get { return _全集金日; }
        }

        //作成区分コンボ
        private int _取引区分 = 4;
        public int 取引区分
        {
            get
            {
                return this._取引区分;
            }
            set
            {
                this._取引区分 = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region
        /// <summary>
        /// 支払先売上合計表
        /// </summary>
        public SHR09010()
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
            AppCommon.SetutpComboboxList(this.作成区分_Combo1, false);
            //作成区分_Combo = 0;

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigSRY11010)ucfg.GetConfigValue(typeof(ConfigSRY11010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigSRY11010();
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
            }
            #endregion
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
                    case SEARCH_SHR09010:
                        DispPreviw(tbl);
                        break;

                    //CSV出力用
                    case SEARCH_SHR09010_CSV:
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
                view.MakeReport("支払予定表", rptFullPathName_PIC, 0, 0, 0);
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
                    srch.表示区分 = 2;
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
            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }

            //作成区分のインデックス値取得                
            int 作成区分_CValue;
            作成区分_CValue = 作成区分_Combo1.SelectedIndex;

            if (string.IsNullOrEmpty(作成集金日) && 全集金日 == false)
            {
                this.ErrorMessage = "作成締日の入力形式が不正です。";
                MessageBox.Show("作成締日の入力形式が不正です。");
                SetFocusToTopControl();
                return;
            }

            if (string.IsNullOrEmpty(作成年) && string.IsNullOrEmpty(作成月) || string.IsNullOrEmpty(作成年) || string.IsNullOrEmpty(作成月))
            {
                this.ErrorMessage = "作成年月を入力してください。";
                MessageBox.Show("作成年月を入力してください。");
                SetFocusToTopControl();
                return;
            }

            int?[] i支払先List = new int?[0];
            if (!string.IsNullOrEmpty(支払先ピックアップ))
            {
                string[] 支払先List = 支払先ピックアップ.Split(',');
                i支払先List = new int?[支払先List.Length];

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
			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SHR09010_CSV, new object[] { 支払先From, 支払先To, i支払先List, 作成集金日, 全集金日, 作成年, 作成月, 作成区分_CValue, 集計期間From, 集計期間To, 集計年月 }));
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

            //作成区分のインデックス値取得                
            int 作成区分_CValue;
            作成区分_CValue = 作成区分_Combo1.SelectedIndex;

            if (string.IsNullOrEmpty(作成集金日) && 全集金日 == false || !string.IsNullOrEmpty(作成集金日) && 全集金日 == true)
            {
                this.ErrorMessage = "作成締日の入力形式が不正です。";
                MessageBox.Show("作成締日の入力形式が不正です。");
                SetFocusToTopControl();
                return;
            }

            if (string.IsNullOrEmpty(作成年) && string.IsNullOrEmpty(作成月) || string.IsNullOrEmpty(作成年) || string.IsNullOrEmpty(作成月))
            {
                this.ErrorMessage = "作成年月を入力してください。";
                MessageBox.Show("作成年月を入力してください。");
                SetFocusToTopControl();
                return;
            }

            int?[] i支払先List = new int?[0];
            if (!string.IsNullOrEmpty(支払先ピックアップ))
            {
                string[] 支払先List = 支払先ピックアップ.Split(',');
                i支払先List = new int?[支払先List.Length];

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
			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SHR09010, new object[] { 支払先From, 支払先To, i支払先List, 作成集金日, 全集金日, 作成年, 作成月, 作成区分_CValue, 集計期間From, 集計期間To, 集計年月 }));
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
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion

        #region 締日入力
        private void check_KeyDown(object sender, RoutedEventArgs e)
        {
            if (全集金日 == false)
            {
                作成集金日 = "31";
            }
            else
            {
                作成集金日 = null;
            }
        }
        #endregion

        #region 全締日の判定
        private void Lost_Shimebi(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(作成集金日))
            {
                ZenShukinbi.IsChecked = true;
            }
            else
            {
                int? 変換締日 = -1;
                if (!string.IsNullOrEmpty(作成集金日))
                {
                    変換締日 = AppCommon.IntParse(作成集金日);
                    ZenShukinbi.IsChecked = false;
                }
            }
        }
        #endregion

        private void Lost_Year(object sender, RoutedEventArgs e)
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
                作成年 = iDate.ToString();
            }
            else
            {
                int? 変換年 = 0;
                if (!string.IsNullOrEmpty(作成年))
                {
                    変換年 = AppCommon.IntParse(作成年);
                }
                //再入力処理
                if (変換年 == 0)
                {
                    string Date;
                    int iDate;
                    DateTime YYYY;
                    YYYY = DateTime.Today;
                    Date = Convert.ToString(YYYY);
                    Date = Date.Substring(0, 4);
                    iDate = Convert.ToInt32(Date);
                    作成年 = iDate.ToString();
                }
            }
        }

        private void Lost_Month(object sender, RoutedEventArgs e)
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
                作成月 = iDate.ToString();
            }
            else
            {
                int? 変換月 = -1;
                if (!string.IsNullOrEmpty(作成月))
                {
                    変換月 = AppCommon.IntParse(作成月);
                }
                //再入力処理
                if (変換月 == 0)
                {
                    string Date;
                    int iDate;
                    DateTime MM;
                    MM = DateTime.Today;
                    Date = Convert.ToString(MM);
                    Date = Date.Substring(5, 2);
                    iDate = Convert.ToInt32(Date);
                    作成月 = iDate.ToString();
                }
                //再入力時のエラー処理
                //初期値0ではエラー処理が通らないので-1をセット
                if (変換月 < 1 || 変換月 > 12)
                {
                    if (変換月 == -1)
                    {
                        string Date;
                        int iDate;
                        DateTime MM;
                        MM = DateTime.Today;
                        Date = Convert.ToString(MM);
                        Date = Date.Substring(5, 2);
                        iDate = Convert.ToInt32(Date);
                        作成月 = iDate.ToString();
                    }
                }
            }

            //期間計算処理
            if (!string.IsNullOrEmpty(作成集金日))
            {
                //締日入力時
                //メソッドで期間計算
                DateFromTo ret = AppCommon.GetDateFromTo(Convert.ToInt32(作成年), Convert.ToInt32(作成月), Convert.ToInt32(作成集金日));
                if (ret.Result == false || ret.Kikan > 31)
                {
                    return;
                }
                集計期間From = ret.DATEFrom;
                集計期間To = ret.DATETo;
            }
            else
            {
                //全締日選択時 例外処理
                int? 全締日時作成月 = 0;

                if (作成月 != null)
                {
                    全締日時作成月 = AppCommon.IntParse(作成月);
                    if (全締日時作成月 < 1 || 全締日時作成月 > 12)
                    {
                        return;
                    }
                }

                //全締日選択時
                string Date = 作成年.ToString() + "/" + 全締日時作成月 + "/" + "01";
                DateTime YYY, DDD;
                YYY = Convert.ToDateTime(Date);
                DDD = Convert.ToDateTime(Date);
                DDD = DDD.AddMonths(+1).AddDays(-1);
                集計期間From = YYY;
                集計期間To = DDD;
            }

            //集計年月を作成
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            int num = sjisEnc.GetByteCount(作成月);

            if (!string.IsNullOrEmpty(作成集金日))
            {
                int? p変換作成締日 = AppCommon.IntParse(作成集金日);

                //文字のバイト数が1の場合、月に0を足して表示
                if (num == 1)
                {
                    集計年月 = Convert.ToInt32(作成年 + "0" + 作成月);
                }
                else
                {
                    集計年月 = Convert.ToInt32(作成年 + 作成月);
                }
            }
            else
            {
                //文字のバイト数が1の場合、月に0を足して表示
                if (num == 1)
                {
                    集計年月 = Convert.ToInt32(作成年 + "0" + 作成月);
                }
                else
                {
                    集計年月 = Convert.ToInt32(作成年 + 作成月);
                }
            }
        }

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
