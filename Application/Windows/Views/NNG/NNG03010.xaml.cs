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
    /// 乗務員月別合計表画面
    /// </summary>
    public partial class NNG03010 : WindowReportBase
    {

        #region 定数定義

        private const string SEARCH_NNG03010 = "SEARCH_NNG03010";
        private const string SEARCH_NNG03010_CSV = "SEARCH_NNG03010_CSV";
        private const string rptFullPathName_PIC1 = @"Files\NNG\NNG03010.rpt";
        private const string rptFullPathName_PIC2 = @"Files\NNG\NNG03020.rpt";
        
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
        public class ConfigNNG03010 : FormConfigBase
        {
            public string 作成年月 { get; set; }
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
        public ConfigNNG03010 frmcfg = null;

        #endregion

        #region バインド用プロパティ

        // データバインド用変数
        private string _乗務員ピックアップ = string.Empty;
        public string 乗務員ピックアップ
        {
            set { _乗務員ピックアップ = value; NotifyPropertyChanged(); }
            get { return _乗務員ピックアップ; }
        }
        private string _乗務員From = string.Empty;
        public string 乗務員From
        {
            set { _乗務員From = value; NotifyPropertyChanged(); }
            get { return _乗務員From; }
        }
        private string _乗務員To = string.Empty;
        public string 乗務員To
        {
            set { _乗務員To = value; NotifyPropertyChanged(); }
            get { return _乗務員To; }
        }

        private int _表示区分 = 0;
        public int 表示区分
        {
            set { _表示区分 = value; NotifyPropertyChanged(); }
            get { return _表示区分; }
        }

        
        private int _表示順序 = 0;
        public int 表示順序
        {
            set { _表示順序 = value; NotifyPropertyChanged(); }
            get { return _表示順序; }
        }

        private bool _前年前々年 = false;
        public bool 前年前々年
        {
            set { _前年前々年 = value; NotifyPropertyChanged(); }
            get { return _前年前々年; }
        }

        string _作成年月 = string.Empty;
        public string 作成年月
        {
            get { return this._作成年月; }
            set { this._作成年月 = value; NotifyPropertyChanged(); }
        }


        string _作成年 = string.Empty;
        public string 作成年
        {
            get { return this._作成年; }
            set { this._作成年 = value; NotifyPropertyChanged(); }
        }


        string _作成月 = string.Empty;
        public string 作成月
        {
            get { return this._作成月; }
            set { this._作成月 = value; NotifyPropertyChanged(); }
        }


        DateTime _集計期間From = DateTime.Today;
        public DateTime 集計期間From
        {
            get { return this._集計期間From; }
            set { this._集計期間From = value; NotifyPropertyChanged(); }
        }


        DateTime _集計期間To = DateTime.Today;
        public DateTime 集計期間To
        {
            get { return this._集計期間To; }
            set { this._集計期間To = value; NotifyPropertyChanged(); }
        }


        #endregion

        #region NNG03010()

        /// <summary>
        /// 乗務員月別合計表画面
        /// </summary>
        public NNG03010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #endregion

        #region Load時

        /// <summary>
        /// 画面読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigNNG03010)ucfg.GetConfigValue(typeof(ConfigNNG03010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigNNG03010();
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
                this.作成年月 = frmcfg.作成年月;
                this.表示区分_Cmb.SelectedIndex = frmcfg.区分1;
                this.表示順序_Cmb.SelectedIndex = frmcfg.区分2;
            }
            #endregion

            base.MasterMaintenanceWindowList.Add("M04_DRV", new List<Type> { null, typeof(SCH04010) });
            表示区分 = 0;
            表示順序 = 0;
            AppCommon.SetutpComboboxList(this.表示区分_Cmb, false);
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
                    case SEARCH_NNG03010:
                        DispPreviw(tbl);
                        break;

					case SEARCH_NNG03010_CSV:
						if (tbl == null || tbl.Rows.Count == 0)
						{
							return;
						}
						tbl.Columns["月1"].ColumnName = (string)tbl.Rows[0]["月名1"];
						tbl.Columns["月2"].ColumnName = (string)tbl.Rows[0]["月名2"];
						tbl.Columns["月3"].ColumnName = (string)tbl.Rows[0]["月名3"];
						tbl.Columns["月4"].ColumnName = (string)tbl.Rows[0]["月名4"];
						tbl.Columns["月5"].ColumnName = (string)tbl.Rows[0]["月名5"];
						tbl.Columns["月6"].ColumnName = (string)tbl.Rows[0]["月名6"];
						tbl.Columns["月7"].ColumnName = (string)tbl.Rows[0]["月名7"];
						tbl.Columns["月8"].ColumnName = (string)tbl.Rows[0]["月名8"];
						tbl.Columns["月9"].ColumnName = (string)tbl.Rows[0]["月名9"];
						tbl.Columns["月10"].ColumnName = (string)tbl.Rows[0]["月名10"];
						tbl.Columns["月11"].ColumnName = (string)tbl.Rows[0]["月名11"];
						tbl.Columns["月12"].ColumnName = (string)tbl.Rows[0]["月名12"];

						tbl.Columns.Remove("月名1");
						tbl.Columns.Remove("月名2");
						tbl.Columns.Remove("月名3");
						tbl.Columns.Remove("月名4");
						tbl.Columns.Remove("月名5");
						tbl.Columns.Remove("月名6");
						tbl.Columns.Remove("月名7");
						tbl.Columns.Remove("月名8");
						tbl.Columns.Remove("月名9");
						tbl.Columns.Remove("月名10");
						tbl.Columns.Remove("月名11");
						tbl.Columns.Remove("月名12");

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

                if (前年前々年 == false)
                {
                    //印刷処理
                    KyoeiSystem.Framework.Reports.Preview.ReportPreview view1 = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
                    //第1引数　帳票タイトル
                    //第2引数　帳票ファイルPass
                    //第3以上　帳票の開始点(0で良い)
                    view1.MakeReport("乗務員月別売上合計表", rptFullPathName_PIC1, 0, 0, 0);
                    //帳票ファイルに送るデータ。
                    //帳票データの列と同じ列名を保持したDataTableを引数とする
					view1.SetReportData(tbl);
					view1.PrinterName = frmcfg.PrinterName;
					view1.ShowPreview();
					view1.Close();
					frmcfg.PrinterName = view1.PrinterName;

                    // 印刷した場合
                    if (view1.IsPrinted)
                    {
                        //印刷した場合はtrueを返す
                    }

                }
                else if (前年前々年 == true)
                {
                    //印刷処理
                    KyoeiSystem.Framework.Reports.Preview.ReportPreview view2 = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
                    //第1引数　帳票タイトル
                    //第2引数　帳票ファイルPass
                    //第3以上　帳票の開始点(0で良い)
                    view2.MakeReport("乗務員月別売上合計表", rptFullPathName_PIC2, 0, 0, 0);
                    //帳票ファイルに送るデータ。
                    //帳票データの列と同じ列名を保持したDataTableを引数とする
					view2.SetReportData(tbl);
					view2.PrinterName = frmcfg.PrinterName;
					frmcfg.PrinterName = view2.PrinterName;

                    view2.ShowPreview();
					view2.Close();

                    // 印刷した場合
                    if (view2.IsPrinted)
                    {
                        //印刷した場合はtrueを返す
                    }
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
                    SCH04010 srch = new SCH04010();
                    switch (uctext.DataAccessName)
                    {
                        case "M04_DRV":
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
            SakuseiNengetsu(sender, null);

            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }

            if (string.IsNullOrEmpty(作成年月))
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                MessageBox.Show("作成年月は入力必須項目です。");
                SetFocusToTopControl();
                return;
            }

            #region 集計期間計算
            if (string.IsNullOrEmpty(作成年月))
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                return;
            }
            int[] i開始年月日 = new int[12];
            int[] i前年開始年月日 = new int[12];
            int[] i前々年開始年月日 = new int[12];

            //開始年月日作成
            for (int i = 0; i < 12; i++)
            {
                if (i == 0)
                {
                    i開始年月日[i] = Convert.ToInt32(作成年月.ToString().Substring(0, 4) + 作成年月.ToString().Substring(5, 2));
                }
                else
                {
                    DateTime 開始年月日;
                    開始年月日 = Convert.ToDateTime(作成年月.ToString().Substring(0, 4) + "/" + 作成年月.ToString().Substring(5, 2) + "/01").AddMonths(+i);
                    i開始年月日[i] = Convert.ToInt32(開始年月日.ToString().Substring(0, 4) + 開始年月日.ToString().Substring(5, 2));
                }
            }
            //前年開始年月日
            for (int i = 0; i < 12; i++)
            {
                if (i == 0)
                {
                    DateTime 前年開始年月日;
                    前年開始年月日 = Convert.ToDateTime(作成年月.ToString().Substring(0, 4) + "/" + 作成年月.ToString().Substring(5, 2) + "/01").AddYears(-1);
                    i前年開始年月日[i] = Convert.ToInt32(前年開始年月日.ToString().Substring(0,4) + 前年開始年月日.ToString().Substring(5,2));
                }
                else
                {
                    DateTime 前年開始年月日;
                    前年開始年月日 = Convert.ToDateTime(作成年月.ToString().Substring(0, 4) + "/" + 作成年月.ToString().Substring(5, 2) + "/01").AddYears(-1).AddMonths(+i);
                    i前年開始年月日[i] = Convert.ToInt32(前年開始年月日.ToString().Substring(0, 4) + 前年開始年月日.ToString().Substring(5, 2));
                }
            }
            //前年開始年月日
            for (int i = 0; i < 12; i++)
            {
                if (i == 0)
                {
                    DateTime 前々年開始年月日;
                    前々年開始年月日 = Convert.ToDateTime(作成年月.ToString().Substring(0, 4) + "/" + 作成年月.ToString().Substring(5, 2) + "/01").AddYears(-2);
                    i前々年開始年月日[i] = Convert.ToInt32(前々年開始年月日.ToString().Substring(0,4) + 前々年開始年月日.ToString().Substring(5,2));
                }
                else
                {
                    DateTime 前々年開始年月日;
                    前々年開始年月日 = Convert.ToDateTime(作成年月.ToString().Substring(0, 4) + "/" + 作成年月.ToString().Substring(5, 2) + "/01").AddYears(-2).AddMonths(+i);
                    i前々年開始年月日[i] = Convert.ToInt32(前々年開始年月日.ToString().Substring(0, 4) + 前々年開始年月日.ToString().Substring(5, 2));   
                }
            }

            #endregion

            string 作成年月度1, 作成年月度2;
            DateTime 来年度;
            来年度 = Convert.ToDateTime(作成年月.ToString().Substring(0, 4) + "/" + 作成年月.ToString().Substring(5, 2) + "/01").AddYears(+1);
            作成年月度1 = 作成年月.ToString().Substring(0, 4) + "年" + 作成年月.ToString().Substring(5, 2) + "月度～";
            作成年月度2 =  来年度.ToString().Substring(0, 4) + "年" + 来年度.ToString().Substring(5, 2) + "月度";
            //コンボボックスの値取得               
            int i表示区分 = 表示区分_Cmb.SelectedIndex;
            int i表示順序 = 表示順序_Cmb.SelectedIndex;


            int?[] i乗務員List = new int?[0];
            if (!string.IsNullOrEmpty(乗務員ピックアップ))
            {
                string[] 得意先List = 乗務員ピックアップ.Split(',');
                i乗務員List = new int?[得意先List.Length];

                for (int i = 0; i < 得意先List.Length; i++)
                {
                    string str = 得意先List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "乗務員指定の形式が不正です。";
                        return;
                    }
                    i乗務員List[i] = code;
                }
            }


			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_NNG03010_CSV, new object[] { 乗務員From, 乗務員To, i乗務員List, i表示区分, i表示順序, 前年前々年, 作成年月度1, 作成年月度2, i開始年月日, i前年開始年月日, i前々年開始年月日 }));
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

            SakuseiNengetsu(sender, null);

            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }

            if (string.IsNullOrEmpty(作成年月))
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                MessageBox.Show("作成年月は入力必須項目です。");
                SetFocusToTopControl();
                return;
            }

            #region 集計期間計算
            if (string.IsNullOrEmpty(作成年月))
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                return;
            }
            int[] i開始年月日 = new int[12];
            int[] i前年開始年月日 = new int[12];
            int[] i前々年開始年月日 = new int[12];

            //開始年月日作成
            for (int i = 0; i < 12; i++)
            {
                if (i == 0)
                {
                    i開始年月日[i] = Convert.ToInt32(作成年月.ToString().Substring(0, 4) + 作成年月.ToString().Substring(5, 2));
                }
                else
                {
                    DateTime 開始年月日;
                    開始年月日 = Convert.ToDateTime(作成年月.ToString().Substring(0, 4) + "/" + 作成年月.ToString().Substring(5, 2) + "/01").AddMonths(+i);
                    i開始年月日[i] = Convert.ToInt32(開始年月日.ToString().Substring(0, 4) + 開始年月日.ToString().Substring(5, 2));
                }
            }
            //前年開始年月日
            for (int i = 0; i < 12; i++)
            {
                if (i == 0)
                {
                    DateTime 前年開始年月日;
                    前年開始年月日 = Convert.ToDateTime(作成年月.ToString().Substring(0, 4) + "/" + 作成年月.ToString().Substring(5, 2) + "/01").AddYears(-1);
                    i前年開始年月日[i] = Convert.ToInt32(前年開始年月日.ToString().Substring(0,4) + 前年開始年月日.ToString().Substring(5,2));
                }
                else
                {
                    DateTime 前年開始年月日;
                    前年開始年月日 = Convert.ToDateTime(作成年月.ToString().Substring(0, 4) + "/" + 作成年月.ToString().Substring(5, 2) + "/01").AddYears(-1).AddMonths(+i);
                    i前年開始年月日[i] = Convert.ToInt32(前年開始年月日.ToString().Substring(0, 4) + 前年開始年月日.ToString().Substring(5, 2));
                }
            }
            //前年開始年月日
            for (int i = 0; i < 12; i++)
            {
                if (i == 0)
                {
                    DateTime 前々年開始年月日;
                    前々年開始年月日 = Convert.ToDateTime(作成年月.ToString().Substring(0, 4) + "/" + 作成年月.ToString().Substring(5, 2) + "/01").AddYears(-2);
                    i前々年開始年月日[i] = Convert.ToInt32(前々年開始年月日.ToString().Substring(0,4) + 前々年開始年月日.ToString().Substring(5,2));
                }
                else
                {
                    DateTime 前々年開始年月日;
                    前々年開始年月日 = Convert.ToDateTime(作成年月.ToString().Substring(0, 4) + "/" + 作成年月.ToString().Substring(5, 2) + "/01").AddYears(-2).AddMonths(+i);
                    i前々年開始年月日[i] = Convert.ToInt32(前々年開始年月日.ToString().Substring(0, 4) + 前々年開始年月日.ToString().Substring(5, 2));   
                }
            }

            #endregion

            string 作成年月度1, 作成年月度2;
            DateTime 来年度;
            来年度 = Convert.ToDateTime(作成年月.ToString().Substring(0, 4) + "/" + 作成年月.ToString().Substring(5, 2) + "/01").AddYears(+1);
            作成年月度1 = 作成年月.ToString().Substring(0, 4) + "年" + 作成年月.ToString().Substring(5, 2) + "月度～";
            作成年月度2 =  来年度.ToString().Substring(0, 4) + "年" + 来年度.ToString().Substring(5, 2) + "月度";
            //コンボボックスの値取得               
            int i表示区分 = 表示区分_Cmb.SelectedIndex;
            int i表示順序 = 表示順序_Cmb.SelectedIndex;


            int?[] i乗務員List = new int?[0];
            if (!string.IsNullOrEmpty(乗務員ピックアップ))
            {
                string[] 得意先List = 乗務員ピックアップ.Split(',');
                i乗務員List = new int?[得意先List.Length];

                for (int i = 0; i < 得意先List.Length; i++)
                {
                    string str = 得意先List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "乗務員指定の形式が不正です。";
                        return;
                    }
                    i乗務員List[i] = code;
                }
            }


			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_NNG03010, new object[] { 乗務員From, 乗務員To, i乗務員List, i表示区分, i表示順序, 前年前々年, 作成年月度1, 作成年月度2, i開始年月日, i前年開始年月日, i前々年開始年月日 }));
        }

        /// <summary>
        /// F11 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 変換作成年月
        private void SakuseiNengetsu(object sender, RoutedEventArgs e)
        {
            NNGDays ret = AppCommon.GetNNGDays(作成年月);
            if (ret.Result == true)
            {
                作成年月 = ret.DateFrom;
            }
            else if (ret.Result == false)
            {
                this.ErrorMessage = "入力された作成年月は利用できません。　入力例：201406";
                this.作成年月 = string.Empty;
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

        #region 画面保持

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            //画面が閉じられた時、データを保持する
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            frmcfg.作成年月 = this.作成年月;
            frmcfg.区分1 = this.表示区分_Cmb.SelectedIndex;
            frmcfg.区分2 = this.表示順序_Cmb.SelectedIndex;
            ucfg.SetConfigValue(frmcfg);
        }

        #endregion
    }
}
