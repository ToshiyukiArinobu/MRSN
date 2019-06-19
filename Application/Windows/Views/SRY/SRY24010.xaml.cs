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
    /// 支払先明細書印刷画面
    /// </summary>
    public partial class SRY24010 : WindowReportBase
    {
        //車輌売上管理表プレビュー定数
        private const string SEARCH_SRY24010 = "SEARCH_SRY24010";
        //車輌売上管理表CSV定数
            private const string SEARCH_SRY24010_CSV = "SEARCH_SRY24010_CSV";

            //車輌売上合計表レポート定数
            private const string rptFullPathName_PIC = @"Files\SRY\SRY24010.rpt";

            #region 画面設定項目
            /// <summary>
            /// ユーザ設定項目
            /// </summary>
            UserConfig ucfg = null;

            /// <summary>
            /// 画面固有設定項目のクラス定義
            /// ※ 必ず public で定義する。
            /// </summary>
            public class ConfigSRY24010 : FormConfigBase
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
                public bool? チェック { get; set; }
            }
            /// ※ 必ず public で定義する。
            public ConfigSRY24010 frmcfg = null;

            #endregion

            #region データバインド用変数
            //車輌ピックアップ
            string _車輌ピックアップ = string.Empty;
            public string 車輌ピックアップ
            {
                get { return this._車輌ピックアップ; }
                set
                {
                    this._車輌ピックアップ = value;
                    NotifyPropertyChanged();
                }
            }

            //車輌範囲指定From
            string _車輌From = string.Empty;
            public string 車輌From
            {
                get { return this._車輌From; }
                set
                {
                    this._車輌From = value;
                    NotifyPropertyChanged();
                }
            }

            //車輌範囲指定To
            string _車輌To = string.Empty;
            public string 車輌To
            {
                get { return this._車輌To; }
                set
                {
                    this._車輌To = value;
                    NotifyPropertyChanged();
                }
            }

            //作成締日
            string _作成締日 = null;
            public string 作成締日
            {
                get { return this._作成締日; }
                set
                {
                    this._作成締日 = value;
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

            //集計期間From
            DateTime? _集計期間From = null;
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
            DateTime? _集計期間To = null;
            public DateTime? 集計期間To
            {
                get { return this._集計期間To; }
                set
                {
                    this._集計期間To = value;
                    NotifyPropertyChanged();
                }
            }


            private DataTable _車輌売上明細書データ = null;
            public DataTable 車輌売上明細書データ
            {
                get { return this._車輌売上明細書データ; }
                set
                {
                    this._車輌売上明細書データ = value;
                    NotifyPropertyChanged();
                }
            }

            private int _取引区分 = 1;
            public int 取引区分
            {
                get { return this._取引区分; }
                set { this._取引区分 = value; NotifyPropertyChanged(); }
            }

            private int _前年対比 = 0;
            public int 前年対比
            {
                get { return this._前年対比; }
                set { this._前年対比 = value; NotifyPropertyChanged(); }
            }
            #endregion

            #region SRY24010
            /// <summary>
            /// 車輌売上売上管理表
            /// </summary>
            public SRY24010()
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
                base.MasterMaintenanceWindowList.Add("M05_CAR", new List<Type> { null, typeof(SCH06010) });
                
                #region 設定項目取得
                ucfg = AppCommon.GetConfig(this);
                frmcfg = (ConfigSRY24010)ucfg.GetConfigValue(typeof(ConfigSRY24010));
                if (frmcfg == null)
                {
                    frmcfg = new ConfigSRY24010();
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
                    this.集計期間From = frmcfg.集計期間From;
                    this.集計期間To = frmcfg.集計期間To;	
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
                        //締日プレビュー出力用
                        case SEARCH_SRY24010:
                            DispPreviw(tbl);
                            break;

                        //締日CSV出力用
                        case SEARCH_SRY24010_CSV:
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
                    view.MakeReport("稼働日数管理票", rptFullPathName_PIC, 0, 0, 0);
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
                        SCH06010 srch = new SCH06010();
                        switch (uctext.DataAccessName)
                        {
                            case "M05_CAR":
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
                if (!base.CheckAllValidation())
                {
                    MessageBox.Show("入力内容に誤りがあります。");
                    SetFocusToTopControl();
                    return;
                }

                if (string.IsNullOrEmpty(作成締日))
                {
                    this.ErrorMessage = "締日を入力してください。";
                    MessageBox.Show("締日を入力してください。");
                    SetFocusToTopControl();
                    return;
                }
                if (string.IsNullOrEmpty(作成年))
                {
                    this.ErrorMessage = "作成年を入力してください。";
                    MessageBox.Show("作成年を入力してください。");
                    SetFocusToTopControl();
                    return;
                }
                if (string.IsNullOrEmpty(作成月))
                {
                    this.ErrorMessage = "作成月を入力してください。";
                    MessageBox.Show("作成月を入力してください。");
                    SetFocusToTopControl();
                    return;
                }

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
                            this.ErrorMessage = "車輌指定の形式が不正です。";
                            return;
                        }
                        i車輌List[i] = code;
                    }
                }
                DateTime Wk;
                DateTime d集計期間From = DateTime.TryParse(集計期間From.ToString(), out Wk) ? Wk : DateTime.Today;
                DateTime d集計期間To = DateTime.TryParse(集計期間To.ToString(),out Wk) ? Wk : DateTime.Today;
				base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SRY24010_CSV, new object[] { 車輌From, 車輌To, i車輌List, 車輌ピックアップ, 作成締日, 作成年, 作成月, d集計期間From, d集計期間To }));
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

                if (string.IsNullOrEmpty(作成締日))
                {
                    this.ErrorMessage = "締日を入力してください。";
                    MessageBox.Show("締日を入力してください。");
                    SetFocusToTopControl();
                    return;
                }

                if (string.IsNullOrEmpty(作成年))
                {
                    this.ErrorMessage = "作成年を入力してください。";
                    MessageBox.Show("作成年を入力してください。");
                    SetFocusToTopControl();
                    return;
                }
                if (string.IsNullOrEmpty(作成月))
                {
                    this.ErrorMessage = "作成月を入力してください。";
                    MessageBox.Show("作成月を入力してください。");
                    SetFocusToTopControl();
                    return;
                }


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
                            this.ErrorMessage = "車輌指定の形式が不正です。";
                            return;
                        }
                        i車輌List[i] = code;
                    }
                }
                DateTime Wk;
                DateTime d集計期間From = DateTime.TryParse(集計期間From.ToString(), out Wk) ? Wk : DateTime.Today;
                DateTime d集計期間To = DateTime.TryParse(集計期間To.ToString() , out Wk) ? Wk : DateTime.Today;



                #region 各月ごとの開始終了日付を取得

                DateTime[] 年月 = new DateTime[12];
                int[] 当年年月 = new int[12];
                int[] 前年年月 = new int[12];
                int[] 前月年月 = new int[12];


                for (int i = 0; i <= 11; i++)
                {
                    年月[i] = d集計期間To.AddMonths(i);

                    if (年月[i].Month.ToString().Length == 1)
                    {
                        当年年月[i] = AppCommon.IntParse(年月[i].Year.ToString() + "0" + 年月[i].Month.ToString());
                    }
                    else
                    {
                        当年年月[i] = AppCommon.IntParse(年月[i].Year.ToString() + 年月[i].Month.ToString());
                    }

                    if (年月[i].AddYears(-1).Month.ToString().Length == 1)
                    {
                        前年年月[i] = AppCommon.IntParse(年月[i].AddYears(-1).Year.ToString() + "0" + 年月[i].AddYears(-1).Month.ToString());
                    }
                    else
                    {
                        前年年月[i] = AppCommon.IntParse(年月[i].AddYears(-1).Year.ToString() + 年月[i].AddYears(-1).Month.ToString());
                    }

                    if (年月[i].AddMonths(-1).Month.ToString().Length == 1)
                    {
                        前月年月[i] = AppCommon.IntParse(年月[i].AddMonths(-1).Year.ToString() + "0" + 年月[i].AddMonths(-1).Month.ToString());
                    }
                    else
                    {
                        前月年月[i] = AppCommon.IntParse(年月[i].AddMonths(-1).Year.ToString() + 年月[i].AddMonths(-1).Month.ToString());
                    }

                }

                #endregion

				base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SRY24010, new object[] { 車輌From, 車輌To, i車輌List, 車輌ピックアップ, 作成締日, 年月, 当年年月, 前年年月, 前月年月 }));

            }

            //リボン
            public override void OnF11Key(object sender, KeyEventArgs e)
            {
                this.Close();
            }

            #endregion

            #region 日付処理
            private void Lost_Shimebi(object sender, RoutedEventArgs e)
            {
                if (作成締日 == null)
                {
                    作成締日 = "31";
                }
                else
                {
                    int? 変換締日 = -1;
                    if (!string.IsNullOrEmpty(作成締日))
                    {
                        変換締日 = AppCommon.IntParse(作成締日);
                    }
                    //再入力時のエラー処理
                    //初期値0ではエラー処理が通らないので-1をセット
                    if (変換締日 < 1 || 変換締日 > 31)
                    {
                        this.ErrorMessage = "入力値エラーです。もう一度入力してください。";
                        MessageBox.Show("入力値エラーです。もう一度入力してください。");
                        作成締日 = null;
                    }
                }  
            }

            //作成年がNullの場合は今年の年を挿入
            private void Lost_Year(object sender, RoutedEventArgs e)
            {
                if (string.IsNullOrEmpty(作成年))
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

            //作成月がNullの場合は今月の月を挿入
            private void Lost_Month(object sender, RoutedEventArgs e)
            {
                if (string.IsNullOrEmpty(作成年))
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

                if (string.IsNullOrEmpty(作成月))
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

                if (!string.IsNullOrEmpty(作成締日))
                {
                    int? 変換締日 = Convert.ToInt32(作成締日);
                    if (変換締日 < 31)
                    {
                        string Date = 作成年.ToString() + "/" + 作成月.ToString() + "/" + 01;
                        DateTime CheckDate;
                        if (!DateTime.TryParse(Date, out CheckDate))
                        {
                            return;
                        }
                        DateTime 作成年月 = Convert.ToDateTime(Date);
                        DateTime YYY, DDD, Test;
                        int itest, i作成締日;
                        YYY = 作成年月.AddMonths(-1);
                        string test = YYY.ToString();
                        string test2 = test;
                        Test = Convert.ToDateTime(test).AddMonths(+1).AddDays(-1);
                        string test3 = Test.ToString().Substring(8, 2);
                        itest = Convert.ToInt32(test3);
                        i作成締日 = Convert.ToInt32(作成締日);
                        if (itest < i作成締日)
                        {
                            test = test.Substring(0, 7);
                            test = test + "/" + itest;
                            YYY = Convert.ToDateTime(test).AddDays(+1);
                            DDD = YYY.AddMonths(+1).AddDays(-1);
                            集計期間From = YYY;
                            集計期間To = DDD;
                        }
                        else
                        {
                            test = test.Substring(0, 7);
                            test = test + "/" + i作成締日.ToString();
                            YYY = Convert.ToDateTime(test).AddDays(+1);
                            DDD = Convert.ToDateTime(test).AddMonths(+1);
                            集計期間From = YYY;
                            集計期間To = DDD;

                        }
                    }
                    else if (変換締日 == 31)
                    {
                        string Date = 作成年.ToString() + "/" + 作成月.ToString() + "/" + 01;
                        DateTime CheckDate;
                        if (!DateTime.TryParse(Date, out CheckDate))
                        {
                            return;
                        }
                        DateTime YYY, DDD;
                        YYY = Convert.ToDateTime(Date);
                        DDD = Convert.ToDateTime(Date);
                        DDD = DDD.AddMonths(+1).AddDays(-1);
                        集計期間From = YYY;
                        集計期間To = DDD;

                    }
                }
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
                frmcfg.集計期間From = this.集計期間From;
                frmcfg.集計期間To = this.集計期間To;
                ucfg.SetConfigValue(frmcfg);
            }
            #endregion

            #region F8
            private void UcLabelTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Enter)
                {
                    try
                    {
                        var uctxt = ViewBaseCommon.FindLogicalChildList<UcTextBox>(sender as DependencyObject).FirstOrDefault();
                        uctxt.ApplyFormat();
                        DateTime dt = Convert.ToDateTime(uctxt.Text);
                        集計期間To = dt;
                    }
                    catch (Exception)
                    {
                    }
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