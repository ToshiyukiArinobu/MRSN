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
    /// 部門売上明細書画面
    /// </summary>
    public partial class NNG08010 : WindowReportBase
    {
        #region データアクセスID
        //部門集計プレビュー定数
        private const string SEARCH_NNG08010 = "SEARCH_NNG08010";
        //部門集計プレビュー定数
        private const string SEARCH_NNG08011 = "SEARCH_NNG08011";
        //部門集計CSV定数
        private const string SEARCH_NNG08010_CSV = "SEARCH_NNG08010_CSV";
        //部門売上合計表レポート定数
        private const string rptFullPathName_PIC = @"Files\NNG\NNG08010.rpt";
        //部門売上合計表レポート定数
        private const string rptFullPathName_PIC2 = @"Files\NNG\NNG08011.rpt";
        //部門売上合計表レポート定数
        private const string SIMEBI_NNG08010 = "M87_CNTL";

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
        public class ConfigNNG08010 : FormConfigBase
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
        public ConfigNNG08010 frmcfg = null;

        #endregion

        #region データバインド用変数

        //部門ピックアップ
        string _部門ピックアップ = string.Empty;
        public string 部門ピックアップ
        {
            get { return this._部門ピックアップ; }
            set
            {
                this._部門ピックアップ = value;
                NotifyPropertyChanged();
            }
        }

        //部門範囲指定From
        string _部門From = string.Empty;
        public string 部門From
        {
            get { return this._部門From; }
            set
            {
                this._部門From = value;
                NotifyPropertyChanged();
            }
        }

        //部門範囲指定To
        string _部門To = string.Empty;
        public string 部門To
        {
            get { return this._部門To; }
            set
            {
                this._部門To = value;
                NotifyPropertyChanged();
            }
        }

        //作成締日
        string _作成締日 = string.Empty;
        public string 作成締日
        {
            get { return this._作成締日; }
            set
            {
                this._作成締日 = value;
                NotifyPropertyChanged();
            }
        }

        //作成年月
        string _作成年月 = string.Empty;
        public string 作成年月
        {
            get { return this._作成年月; }
            set
            {
                this._作成年月 = value;
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
        DateTime _集計期間From = DateTime.Today;
        public DateTime 集計期間From
        {
            get { return this._集計期間From; }
            set
            {
                this._集計期間From = value;
                NotifyPropertyChanged();
            }
        }

        //集計期間To
        DateTime _集計期間To = DateTime.Today;
        public DateTime 集計期間To
        {
            get { return this._集計期間To; }
            set
            {
                this._集計期間To = value;
                NotifyPropertyChanged();
            }
        }

        //全締日集計
        private bool _全締日集計 = true;
        public bool 全締日集計
        {
            set
            {
                _全締日集計 = value;
                NotifyPropertyChanged();
            }
            get { return _全締日集計; }
        }

        //前年・前々年分の印刷チェック
        private bool _前年前々年 = false;
        public bool 前年前々年
        {
            set
            {
                _前年前々年 = value;
                NotifyPropertyChanged();
            }
            get { return _前年前々年; }
        }


        //表示区分コンボ
        private int _表示区分_Combo = 0;
        public int 表示区分_Combo
        {
            get
            {
                return this._表示区分_Combo;
            }
            set
            {
                this._表示区分_Combo = value;
                NotifyPropertyChanged();
            }
        }

        //表示順序
        private int _表示順序 = 0;
        public int 表示順序
        {
            set
            {
                _表示順序 = value;
                NotifyPropertyChanged();
            }
            get { return _表示順序; }
        }

#endregion

        #region
        /// <summary>
        /// 部門売上合計表
        /// </summary>
        public NNG08010()
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

            base.MasterMaintenanceWindowList.Add("M71_BUM", new List<Type> { null, typeof(SCH10010) });
            AppCommon.SetutpComboboxList(this.表示区分_Combo1, false);
            AppCommon.SetutpComboboxList(this.表示順序_Combo, false);
            表示区分_Combo = 0;

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigNNG08010)ucfg.GetConfigValue(typeof(ConfigNNG08010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigNNG08010();
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
                this.表示区分_Combo1.SelectedIndex = frmcfg.区分1;
                this.表示順序_Combo.SelectedIndex = frmcfg.区分2;
            }
            #endregion

            System.Windows.Forms.Application.DoEvents();
            //締日取得
            base.SendRequest(new CommunicationObject(MessageType.RequestData, SIMEBI_NNG08010, new object[] { 1, 0 }));

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
                    case SEARCH_NNG08010:
                        DispPreviw(tbl);
                        break;

                    //締日プレビュー出力用
                    case SEARCH_NNG08011:
                        DispPreviw2(tbl);
                        break;

                    //締日CSV出力用
					case SEARCH_NNG08010_CSV:
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

                    case SIMEBI_NNG08010:
                        bool result;
                        int output;
                        result = int.TryParse(tbl.Rows[0]["得意先自社締日"].ToString(), out output);
                        作成締日 = output.ToString();
                        
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
                view.MakeReport("部門別月別売上合計表", rptFullPathName_PIC, 0, 0, 0);
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

        #region プレビュー画面
        /// <summary>
        /// プレビュー画面表示
        /// </summary>
        /// <param name="tbl"></param>
        private void DispPreviw2(DataTable tbl)
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
                view.MakeReport("部門別月別売上合計表", rptFullPathName_PIC2, 0, 0, 0);
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
                    SCH10010 srch = new SCH10010();
                    switch (uctext.DataAccessName)
                    {
                        case "M71_BUM":
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

            //表示区分のインデックス値取得                
            int 表示区分_CValue;
            int 表示順序;
            表示区分_CValue = 表示区分_Combo1.SelectedIndex;
            表示順序 = 表示順序_Combo.SelectedIndex;


            ////締日が0 or 31を超える場合
            //if (!string.IsNullOrEmpty(作成締日))
            //{
            //    if (AppCommon.IntParse(作成締日) <= 0 || AppCommon.IntParse(作成締日) > 31)
            //    {
            //        this.ErrorMessage = "基礎情報設定で自社締日(得意先)に締日を設定して下さい。";
            //        MessageBox.Show("基礎情報設定で自社締日(得意先)に締日を設定して下さい。");
            //        return;
            //    }
            //}
            //else
            //{
            //    this.ErrorMessage = "基礎情報設定で自社締日(得意先)に締日を設定して下さい。";
            //    MessageBox.Show("基礎情報設定で自社締日(得意先)に締日を設定して下さい。");
            //}

            ////作成年月
            //if (string.IsNullOrEmpty(作成年月))
            //{
            //    this.ErrorMessage = "作成年月は入力必須項目です。";
            //    MessageBox.Show("作成年月は入力必須項目です。");
            //    return;
            //}

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

            int?[] i部門List = new int?[0];
            if (!string.IsNullOrEmpty(部門ピックアップ))
            {
                string[] 部門List = 部門ピックアップ.Split(',');
                i部門List = new int?[部門List.Length];

                for (int i = 0; i < 部門List.Length; i++)
                {
                    string str = 部門List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "部門指定の形式が不正です。";
                        return;
                    }
                    i部門List[i] = code;
                }
            }


            DateTime[] d開始年月日 = new DateTime[12];
            DateTime[] d終了年月日 = new DateTime[12];
            DateTime[] d前年開始年月日 = new DateTime[12];
            DateTime[] d前年終了年月日 = new DateTime[12];
            DateTime[] d前々年開始年月日 = new DateTime[12];
            DateTime[] d前々年終了年月日 = new DateTime[12];
            //当月
            DateTime Wk;
            DateTime d当月 = DateTime.TryParse(作成年月.ToString().Substring(0, 4) + "/" + 作成年月.ToString().Substring(5, 2) + "/" + 1 , out Wk) ? Wk : DateTime.Today;
            //当月
            DateTime d当月2;

            for (int i = 0; i < 12; i++)
            {
                d当月2 = d当月.AddMonths(i);
                //メソッドで期間計算
                DateFromTo ret = AppCommon.GetDateFromTo(Convert.ToInt32(d当月2.Year), Convert.ToInt32(d当月2.Month), Convert.ToInt32(作成締日));
                d開始年月日[i] = ret.DATEFrom;
                d終了年月日[i] = ret.DATETo;
            }

            for (int i = 0; i < 12; i++)
            {
                d当月2 = d当月.AddMonths((-12 + i));
                //メソッドで期間計算
                DateFromTo ret = AppCommon.GetDateFromTo(Convert.ToInt32(d当月2.Year), Convert.ToInt32(d当月2.Month), Convert.ToInt32(作成締日));
                d前年開始年月日[i] = ret.DATEFrom;
                d前年終了年月日[i] = ret.DATETo;
            }

            for (int i = 0; i < 12; i++)
            {
                d当月2 = d当月.AddMonths((-24 + i));
                //メソッドで期間計算
                DateFromTo ret = AppCommon.GetDateFromTo(Convert.ToInt32(d当月2.Year), Convert.ToInt32(d当月2.Month), Convert.ToInt32(作成締日));
                d前々年開始年月日[i] = ret.DATEFrom;
                d前々年終了年月日[i] = ret.DATETo;
            }

            base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_NNG08010_CSV, new object[] { 部門From, 部門To, i部門List, 前年前々年, 表示区分_CValue, 表示順序, d開始年月日, d終了年月日, d前年開始年月日, d前年終了年月日, d前々年開始年月日, d前々年終了年月日, 表示区分_Combo1.Text, 表示順序_Combo.Text, 部門ピックアップ }));
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


            SakuseiNengetsu(sender, null);

            //表示区分のインデックス値取得                
            int 表示区分_CValue;
            int 表示順序;
            表示区分_CValue = 表示区分_Combo1.SelectedIndex;
            表示順序 = 表示順序_Combo.SelectedIndex;


            ////締日が0 or 31を超える場合
            //if (!string.IsNullOrEmpty(作成締日))
            //{
            //    if (AppCommon.IntParse(作成締日) <= 0 || AppCommon.IntParse(作成締日) > 31)
            //    {
            //        this.ErrorMessage = "基礎情報設定で自社締日(得意先)に締日を設定して下さい。";
            //        MessageBox.Show("基礎情報設定で自社締日(得意先)に締日を設定して下さい。");
            //        return;
            //    }
            //}
            //else
            //{
            //    this.ErrorMessage = "基礎情報設定で自社締日(得意先)に締日を設定して下さい。";
            //    MessageBox.Show("基礎情報設定で自社締日(得意先)に締日を設定して下さい。");
            //}

            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }

            //作成年月
            if (string.IsNullOrEmpty(作成年月))
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                MessageBox.Show("作成年月は入力必須項目です。");
                return;
            }

            int?[] i部門List = new int?[0];
            if (!string.IsNullOrEmpty(部門ピックアップ))
            {
                string[] 部門List = 部門ピックアップ.Split(',');
                i部門List = new int?[部門List.Length];

                for (int i = 0; i < 部門List.Length; i++)
                {
                    string str = 部門List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "部門指定の形式が不正です。";
                        return;
                    }
                    i部門List[i] = code;
                }
            }


            DateTime[] d開始年月日 = new DateTime[12];
            DateTime[] d終了年月日 = new DateTime[12];
            DateTime[] d前年開始年月日 = new DateTime[12];
            DateTime[] d前年終了年月日 = new DateTime[12];
            DateTime[] d前々年開始年月日 = new DateTime[12];
            DateTime[] d前々年終了年月日 = new DateTime[12];
            //当月
            DateTime Wk;
            DateTime d当月 = DateTime.TryParse(作成年月.ToString().Substring(0, 4) + "/" + 作成年月.ToString().Substring(5, 2) + "/" + 1 , out Wk) ? Wk : DateTime.Today;
            //当月
            DateTime d当月2;

            for (int i = 0; i < 12; i++)
            {
                d当月2 = d当月.AddMonths(i);
                //メソッドで期間計算
                DateFromTo ret = AppCommon.GetDateFromTo(Convert.ToInt32(d当月2.Year), Convert.ToInt32(d当月2.Month), Convert.ToInt32(作成締日));
                d開始年月日[i] = ret.DATEFrom;
                d終了年月日[i] = ret.DATETo;
            }

            for (int i = 0; i < 12; i++)
            {
                d当月2 = d当月.AddMonths((-12 + i));
                //メソッドで期間計算
                DateFromTo ret = AppCommon.GetDateFromTo(Convert.ToInt32(d当月2.Year), Convert.ToInt32(d当月2.Month), Convert.ToInt32(作成締日));
                d前年開始年月日[i] = ret.DATEFrom;
                d前年終了年月日[i] = ret.DATETo;
            }

            for (int i = 0; i < 12; i++)
            {
                d当月2 = d当月.AddMonths((-24 + i));
                //メソッドで期間計算
                DateFromTo ret = AppCommon.GetDateFromTo(Convert.ToInt32(d当月2.Year), Convert.ToInt32(d当月2.Month), Convert.ToInt32(作成締日));
                d前々年開始年月日[i] = ret.DATEFrom;
                d前々年終了年月日[i] = ret.DATETo;
            }
            if (前年前々年)
            {
				base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_NNG08011, new object[] { 部門From, 部門To, i部門List, 前年前々年, 表示区分_CValue, 表示順序, d開始年月日, d終了年月日, d前年開始年月日, d前年終了年月日, d前々年開始年月日, d前々年終了年月日, 表示区分_Combo1.Text, 表示順序_Combo.Text, 部門ピックアップ }));
            }
            else
            {
				base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_NNG08010, new object[] { 部門From, 部門To, i部門List, 前年前々年, 表示区分_CValue, 表示順序, d開始年月日, d終了年月日, d前年開始年月日, d前年終了年月日, d前々年開始年月日, d前々年終了年月日, 表示区分_Combo1.Text, 表示順序_Combo.Text, 部門ピックアップ }));
            }
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
            frmcfg.作成年月 = this.作成年月;
            frmcfg.区分1 = this.表示区分_Combo1.SelectedIndex;
            frmcfg.区分2 = this.表示順序_Combo.SelectedIndex;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion

        #region 締日入力
        private void check_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (string.IsNullOrEmpty(作成締日))
                {
                    if (全締日集計 == false)
                    {
                        作成締日 = "31";
                    }
                }
            }
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

        private void UcLabelTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
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


    }
}
