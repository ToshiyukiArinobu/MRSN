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
using KyoeiSystem.Framework.Windows.Controls;


namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 車輌合計表画面
    /// </summary>
    public partial class SRY08010 : WindowReportBase
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
        public class ConfigSRY08010 : FormConfigBase
        {
            public int? 作成年 { get; set; }
            public int? 作成月 { get; set; }
            public int? 締日 { get; set; }
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
        public ConfigSRY08010 frmcfg = null;

        #endregion

        #region 定数定義

        // データアクセス定義名
        private const string SEARCH_SRY08010 = "SEARCH_SRY08010";
        // データアクセス定義名
        private const string SEARCH_SRY08010_CSV = "SEARCH_SRY08010_CSV";
        // 車輌管理台帳　レポート定数
        private const string rptFullPathName_PIC = @"Files\SRY\SRY08010.rpt";

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
            set
            { _車輌To = value; NotifyPropertyChanged(); }
            get { return _車輌To; }
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

        private string _表示順序 = null;
        public string 表示順序
        {
            set { _表示順序 = value; NotifyPropertyChanged(); }
            get { return _表示順序; }
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

        private int _作成区分;
        public int 作成区分
        {
            set { _作成区分 = value; NotifyPropertyChanged(); }
            get { return _作成区分; }
        }

        //変数
        string 項目1, 項目2, 項目3, 項目4, 項目5, 項目6, 項目7, 項目8, 項目9, 項目10, 項目11, 項目12, 項目13, 項目14, 項目15;
        int 開始年, 開始月, 終了年, 終了月;

        #endregion

        #region SRY08010

        /// <summary>
        /// 車輌合計表画面
        /// </summary>
        public SRY08010()
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
            frmcfg = (ConfigSRY08010)ucfg.GetConfigValue(typeof(ConfigSRY08010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigSRY08010();
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
                this.作成年 = frmcfg.作成年;
                this.作成月 = frmcfg.作成月;
                this.集計期間From = frmcfg.集計期間From;
                this.集計期間To = frmcfg.集計期間To;
                this.表示順序_Cmb.SelectedIndex = frmcfg.区分1;

            }
            #endregion

            //F1 車輌 
            base.MasterMaintenanceWindowList.Add("M05_CAR", new List<Type> { null, typeof(SCH06010) });
            AppCommon.SetutpComboboxList(this.表示順序_Cmb, false);
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
                    case SEARCH_SRY08010:
                        DispPreviw(tbl);
                        break;

                    //締日CSV出力用
                    case SEARCH_SRY08010_CSV:
						if (tbl == null)
						{
							break;
						}
						tbl.Columns["金額1"].ColumnName = tbl.Rows[0]["月1"].ToString();
						tbl.Columns["金額2"].ColumnName = tbl.Rows[0]["月2"].ToString();
						tbl.Columns["金額3"].ColumnName = tbl.Rows[0]["月3"].ToString();
						tbl.Columns["金額4"].ColumnName = tbl.Rows[0]["月4"].ToString();
						tbl.Columns["金額5"].ColumnName = tbl.Rows[0]["月5"].ToString();
						tbl.Columns["金額6"].ColumnName = tbl.Rows[0]["月6"].ToString();
						tbl.Columns["金額7"].ColumnName = tbl.Rows[0]["月7"].ToString();
						tbl.Columns["金額8"].ColumnName = tbl.Rows[0]["月8"].ToString();
						tbl.Columns["金額9"].ColumnName = tbl.Rows[0]["月9"].ToString();
						tbl.Columns["金額10"].ColumnName = tbl.Rows[0]["月10"].ToString();
						tbl.Columns["金額11"].ColumnName = tbl.Rows[0]["月11"].ToString();
						tbl.Columns["金額12"].ColumnName = tbl.Rows[0]["月12"].ToString();
						tbl.Columns.Remove("月1");
						tbl.Columns.Remove("月2");
						tbl.Columns.Remove("月3");
						tbl.Columns.Remove("月4");
						tbl.Columns.Remove("月5");
						tbl.Columns.Remove("月6");
						tbl.Columns.Remove("月7");
						tbl.Columns.Remove("月8");
						tbl.Columns.Remove("月9");
						tbl.Columns.Remove("月10");
						tbl.Columns.Remove("月11");
						tbl.Columns.Remove("月12");
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


			int i表示順序 = 表示順序_Cmb.SelectedIndex;


			if (作成年 == null || 作成月 == null)
			{
				this.ErrorMessage = "作成年月は入力必須項目です。";
				return;
			}
			int i年月;
			i年月 = Convert.ToInt32((作成年).ToString()) * 100 + Convert.ToInt32(作成月.ToString());

			//車輌リスト作成
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

			DateTime dDate = new DateTime(AppCommon.IntParse(作成年.ToString()), AppCommon.IntParse(作成月.ToString()), 1);
			開始年 = AppCommon.IntParse(作成年.ToString());
			開始月 = AppCommon.IntParse(作成月.ToString());
			項目1 = dDate.Month.ToString() + "月";
			int i開始年月 = dDate.Year * 100 + dDate.Month;
			dDate = dDate.AddMonths(1);
			項目2 = dDate.Month.ToString() + "月";
			dDate = dDate.AddMonths(1);
			項目3 = dDate.Month.ToString() + "月";
			dDate = dDate.AddMonths(1);
			項目4 = dDate.Month.ToString() + "月";
			dDate = dDate.AddMonths(1);
			項目5 = dDate.Month.ToString() + "月";
			dDate = dDate.AddMonths(1);
			項目6 = dDate.Month.ToString() + "月";
			dDate = dDate.AddMonths(1);
			項目7 = dDate.Month.ToString() + "月";
			dDate = dDate.AddMonths(1);
			項目8 = dDate.Month.ToString() + "月";
			dDate = dDate.AddMonths(1);
			項目9 = dDate.Month.ToString() + "月";
			dDate = dDate.AddMonths(1);
			項目10 = dDate.Month.ToString() + "月";
			dDate = dDate.AddMonths(1);
			項目11 = dDate.Month.ToString() + "月";
			dDate = dDate.AddMonths(1);
			項目12 = dDate.Month.ToString() + "月";
			int i終了年月 = dDate.Year * 100 + dDate.Month;
			終了年 = dDate.Year;
			終了月 = dDate.Month;
			項目13 = "年合計";
			項目14 = "平　均";
			項目15 = "対売上";


			//帳票出力用
			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SRY08010_CSV, new object[] { 車輌From , 車輌To , i車輌List , 車輌ピックアップ,
                                                                                                             作成年, 作成月, 
                                                                                                            }));
        }


        /// <summary>
        /// F8 リボン　印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
		{
			PrinterDriver ret2 = AppCommon.GetPrinter(frmcfg.PrinterName);
			if (ret2.Result == false)
			{
				this.ErrorMessage = "プリンタドライバーがインストールされていません！";
				return;
			}
			frmcfg.PrinterName = ret2.PrinterName;


            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }


            int i表示順序 = 表示順序_Cmb.SelectedIndex;


            if (作成年 == null || 作成月 == null)
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                return;
            }
            int i年月;
            i年月 = Convert.ToInt32((作成年).ToString()) * 100 + Convert.ToInt32(作成月.ToString());

            //車輌リスト作成
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

            DateTime dDate = new DateTime(AppCommon.IntParse(作成年.ToString()), AppCommon.IntParse(作成月.ToString()), 1);
            開始年 = AppCommon.IntParse(作成年.ToString());
            開始月 = AppCommon.IntParse(作成月.ToString());
            項目1 = dDate.Month.ToString() + "月";
            int i開始年月 = dDate.Year * 100 + dDate.Month;
            dDate = dDate.AddMonths(1);
            項目2 = dDate.Month.ToString() + "月";
            dDate = dDate.AddMonths(1);
            項目3 = dDate.Month.ToString() + "月";
            dDate = dDate.AddMonths(1);
            項目4 = dDate.Month.ToString() + "月";
            dDate = dDate.AddMonths(1);
            項目5 = dDate.Month.ToString() + "月";
            dDate = dDate.AddMonths(1);
            項目6 = dDate.Month.ToString() + "月";
            dDate = dDate.AddMonths(1);
            項目7 = dDate.Month.ToString() + "月";
            dDate = dDate.AddMonths(1);
            項目8 = dDate.Month.ToString() + "月";
            dDate = dDate.AddMonths(1);
            項目9 = dDate.Month.ToString() + "月";
            dDate = dDate.AddMonths(1);
            項目10 = dDate.Month.ToString() + "月";
            dDate = dDate.AddMonths(1);
            項目11 = dDate.Month.ToString() + "月";
            dDate = dDate.AddMonths(1);
            項目12 = dDate.Month.ToString() + "月";
            int i終了年月 = dDate.Year * 100 + dDate.Month;
            終了年 = dDate.Year;
            終了月 = dDate.Month;
            項目13 = "年合計";
            項目14 = "平　均";
            項目15 = "対売上";


            //帳票出力用
			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SRY08010, new object[] { 車輌From , 車輌To , i車輌List , 車輌ピックアップ,
                                                                                                             作成年, 作成月, 
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

                base.SetBusyForInput();
                var parms = new List<Framework.Reports.Preview.ReportParameter>()
				{
					new Framework.Reports.Preview.ReportParameter(){ PNAME="項目1", VALUE=(項目1==null?"":項目1)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="項目2", VALUE=(項目2==null?"":項目2)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="項目3", VALUE=(項目3==null?"":項目3)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="項目4", VALUE=(項目4==null?"":項目4)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="項目5", VALUE=(項目5==null?"":項目5)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="項目6", VALUE=(項目6==null?"":項目6)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="項目7", VALUE=(項目7==null?"":項目7)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="項目8", VALUE=(項目8==null?"":項目8)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="項目9", VALUE=(項目9==null?"":項目9)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="項目10", VALUE=(項目10==null?"":項目10)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="項目11", VALUE=(項目11==null?"":項目11)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="項目12", VALUE=(項目12==null?"":項目12)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="項目13", VALUE=(項目13==null?"":項目13)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="項目14", VALUE=(項目14==null?"":項目14)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="項目15", VALUE=(項目15==null?"":項目15)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="開始年", VALUE=(開始年)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="開始月", VALUE=(開始月)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="終了年", VALUE=(終了年)},
					new Framework.Reports.Preview.ReportParameter(){ PNAME="終了月", VALUE=(終了月)},
				};

                //印刷処理
                KyoeiSystem.Framework.Reports.Preview.ReportPreview view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
                //第1引数　帳票タイトル
                //第2引数　帳票ファイルPass
                //第3以上　帳票の開始点(0で良い)
                view.MakeReport("車輌収支合計表", rptFullPathName_PIC, 0, 0, 0);
                //帳票ファイルに送るデータ。
                //帳票データの列と同じ列名を保持したDataTableを引数とする
                view.SetReportData(tbl);
                view.SetupParmeters(parms);
				base.SetFreeForInput();
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



        //作成年がNullの場合は今年の年を挿入
        private void Lost_Year(object sender, RoutedEventArgs e)
        {
            if (作成年 == null)
            {
                作成年 = Convert.ToInt32(DateTime.Today.ToString().Substring(0, 4));
            }

        }

        //作成月がNullの場合は今月の月を挿入
        private void Lost_Month(object sender, RoutedEventArgs e)
        {
            if (作成月 == null)
            {
                作成月 = Convert.ToInt32(DateTime.Today.ToString().Substring(5, 2));
            }
            else if (作成月 == 0 || 作成月 > 12)
            {
                this.ErrorMessage = "入力値エラーです。もう一度入力してください。";
                MessageBox.Show("入力値エラーです。もう一度入力してください。");
                作成月 = null;
                return;
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

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            frmcfg.作成年 = this.作成年;
            frmcfg.作成月 = this.作成月;
            frmcfg.集計期間From = this.集計期間From;
            frmcfg.集計期間To = this.集計期間To;
            frmcfg.区分1 = this.表示順序_Cmb.SelectedIndex;
            ucfg.SetConfigValue(frmcfg);
        }

        private void 表示順序_Cmb_DataListInitialized(object sender, RoutedEventArgs e)
        {
            表示順序 = "0";
        }

    }
}
