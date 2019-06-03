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
    public partial class TKS04010 : WindowReportBase
    {
        //得意先日計表プレビュー定数
        private const string SEARCH_TKS04010 = "SEARCH_TKS04010";
        //得意先日計表プレビュー定数
        private const string SEARCH = "SEARCH_TKS04";
            //得意先日計表CSV定数
            private const string SEARCH_TKS04010_CSV = "SEARCH_TKS04010_CSV";

            //得意先売上合計表レポート定数
            private const string rptFullPathName_PIC = @"Files\TKS\TKS04010.rpt";

            #region 画面設定項目
            /// <summary>
            /// ユーザ設定項目
            /// </summary>
            UserConfig ucfg = null;

            /// <summary>
            /// 画面固有設定項目のクラス定義
            /// ※ 必ず public で定義する。
            /// </summary>
            public class ConfigTKS04010 : FormConfigBase
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
            public ConfigTKS04010 frmcfg = null;

            #endregion

            #region データバインド用変数
            //得意先ピックアップ
            string _得意先ピックアップ = string.Empty;
            public string 得意先ピックアップ
            {
                get { return this._得意先ピックアップ; }
                set
                {
                    this._得意先ピックアップ = value;
                    NotifyPropertyChanged();
                }
            }

            //得意先範囲指定From
            string _得意先From = string.Empty;
            public string 得意先From
            {
                get { return this._得意先From; }
                set
                {
                    this._得意先From = value;
                    NotifyPropertyChanged();
                }
            }

            //得意先範囲指定To
            string _得意先To = string.Empty;
            public string 得意先To
            {
                get { return this._得意先To; }
                set
                {
                    this._得意先To = value;
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


            private DataTable _得意先売上明細書データ = null;
            public DataTable 得意先売上明細書データ
            {
                get { return this._得意先売上明細書データ; }
                set
                {
                    this._得意先売上明細書データ = value;
                    NotifyPropertyChanged();
                }
            }

            private int _取引区分 = 1;
            public int 取引区分
            {
                get { return this._取引区分; }
                set { this._取引区分 = value; NotifyPropertyChanged(); }
            }
            #endregion

            #region TKS04010
            /// <summary>
            /// 得意先売上日計表
            /// </summary>
            public TKS04010()
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
                
                #region 設定項目取得
                ucfg = AppCommon.GetConfig(this);
                frmcfg = (ConfigTKS04010)ucfg.GetConfigValue(typeof(ConfigTKS04010));
                if (frmcfg == null)
                {
                    frmcfg = new ConfigTKS04010();
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
                        case SEARCH_TKS04010:
                            DispPreviw(tbl);
                            break;

                        //締日CSV出力用
                        case SEARCH_TKS04010_CSV:
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
                    view.MakeReport("得意先売上日計表", rptFullPathName_PIC, 0, 0, 0);
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
                base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_TKS04010_CSV, new object[] { 得意先From, 得意先To, i得意先List, 作成締日, 作成年, 作成月, 集計期間From, 集計期間To }));
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
                base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_TKS04010, new object[] { 得意先From, 得意先To, i得意先List, 作成締日, 作成年, 作成月, 集計期間From, 集計期間To }));
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

            //作成月がNullの場合は今月の月を挿入
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

                if (!string.IsNullOrEmpty(作成締日))
                {
                    //締日入力時
                    //メソッドで期間計算
                    DateFromTo ret = AppCommon.GetDateFromTo(Convert.ToInt32(作成年), Convert.ToInt32(作成月), Convert.ToInt32(作成締日));
                    if (ret.Result == false || ret.Kikan > 31)
                    {
                        this.ErrorMessage = "入力値エラーです。もう一度入力してください。";
                        MessageBox.Show("入力値エラーです。もう一度入力してください。");
                        SetFocusToTopControl();
                        return;
                    }
                    集計期間From = ret.DATEFrom;
                    集計期間To = ret.DATETo;
                }
                else
                {
                    //全締日選択時
                    string Date = 作成年.ToString() + "/" + 作成月.ToString() + "/" + 01;
                    DateTime YYY, DDD;
                    YYY = Convert.ToDateTime(Date);
                    DDD = Convert.ToDateTime(Date);
                    DDD = DDD.AddMonths(+1).AddDays(-1);
                    集計期間From = YYY;
                    集計期間To = DDD;
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