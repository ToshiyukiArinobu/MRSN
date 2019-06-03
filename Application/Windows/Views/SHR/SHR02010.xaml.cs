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
    /// 支払先明細書印刷画面
    /// </summary>
    public partial class SHR02010 : WindowReportBase
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
        public class ConfigSHR02010 : FormConfigBase
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
            public int 区分6 { get; set; }
            public int 区分7 { get; set; }
            public int 区分8 { get; set; }
            public bool? チェック { get; set; }
        }
        /// ※ 必ず public で定義する。
        public ConfigSHR02010 frmcfg = null;

        #endregion

        #region 定数

        //CSV出力用定数
        private const string SEARCH_SHR02010_CSV = "SEARCH_SHR02010_CSV";
        //社内用プレビュー出力用定数
        private const string SEARCH_SHR02010 = "SEARCH_SHR02010";
        //レポート
        private const string rptFullPathName_PIC = @"Files\SHR\SHR02010.rpt";
        private const string rptFullPathName_PIC2 = @"Files\SHR\SHR02010A.rpt";
        //Spread
        private const string SPREAD_SHR02010 = "SPREAD_SHR02010";

        #endregion

        #region バインド用プロパティ
        private string _支払先ピックアップ = string.Empty;
        public string 支払先ピックアップ
        {
            set { _支払先ピックアップ = value; NotifyPropertyChanged(); }
            get { return _支払先ピックアップ; }
        }
        private int? _支払先From = null;
        public int? 支払先From
        {
            set { _支払先From = value; NotifyPropertyChanged(); }
            get { return _支払先From; }
        }
        private int? _支払先To = null;
        public int? 支払先To
        {
            set { _支払先To = value; NotifyPropertyChanged(); }
            get { return _支払先To; }
        }

        private int? _作成締日 = null;
        public int? 作成締日
        {
            set { _作成締日 = value; NotifyPropertyChanged(); }
            get { return _作成締日; }
        }

        private int? _作成年 = null;
        public int? 作成年
        {
            set{ _作成年 = value; NotifyPropertyChanged(); }
            get { return _作成年; }
        }

        private int? _作成月 = null;
        public int? 作成月
        {
            set { _作成月 = value; NotifyPropertyChanged(); }
            get { return _作成月; }
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

        private int _社内区分;
        public int 社内区分
        {
            get { return this._社内区分; }
            set { this._社内区分 = value; NotifyPropertyChanged(); }
        }

        private int _取引区分 = 4;
        public int 取引区分
        {
            get { return this._取引区分; }
            set { this._取引区分 = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region SHR02010()
        /// <summary>
        /// 支払先明細書印刷画面
        /// </summary>
        public SHR02010()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion

        #region Loadイベント
        /// <summary>
        /// 画面読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigSHR02010)ucfg.GetConfigValue(typeof(ConfigSHR02010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigSHR02010();
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
                this.Combo1.SelectedIndex = frmcfg.区分1;
            }
            #endregion

            //得意先ID用
            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { null, typeof(SCH01010) });
            
            //ComboBoxの初期値設定
            AppCommon.SetutpComboboxList(this.Combo1, false);
            社内区分 = 0;
            //先頭にカーソルを合わせる
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
                    //検索結果取得時
                    case SEARCH_SHR02010:
                        if (Combo1.SelectedIndex == 0)
                        {
                            DispPreviw(tbl);
                        }
                        else if (Combo1.SelectedIndex == 1)
                        {
                            DispPreviw2(tbl);
                        }

                        break;

                    case SEARCH_SHR02010_CSV:
                        if (Combo1.SelectedIndex == 0)
                        {
                            OutPutCSV1(tbl);
                        }
                        else if (Combo1.SelectedIndex == 1)
                        {
                            OutPutCSV2(tbl);
                        }
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
                    srch.表示区分 = 4;
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

            int CValue;
            CValue = Combo1.SelectedIndex;

            //集計期間がNullの場合Max,Minを設定
            if (集計期間From == null)
            {
                集計期間From = DateTime.MinValue;

                if (集計期間To == null)
                {
                    集計期間To = DateTime.MaxValue;
                }
            }

            //締日がNullの場合
            if (作成締日 == null)
            {
                this.ErrorMessage = "作成締日は入力必須項目です。";
                MessageBox.Show("作成締日は入力必須項目です。");
                return;
            }
           
            //作成日付
            if (作成年 == null || 作成月 == null)
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                MessageBox.Show("作成年月は入力必須項目です。");
                return;
            }

            //集計期間
            if (集計期間From == null || 集計期間To == null)
            {
                this.ErrorMessage = "集計期間は入力必須項目です。";
                MessageBox.Show("集計期間は入力必須項目です");
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

            //CSV出力用
			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SHR02010_CSV, new object[] { 支払先From, 支払先To, 支払先ピックアップ, 作成締日, 集計期間From, 集計期間To, CValue }));
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

            //社内区分のIndexを取得
            int CValue;
            CValue = Combo1.SelectedIndex;



            //締日がNullの場合
            if (作成締日 == null)
            {
                this.ErrorMessage = "作成締日は入力必須項目です。";
                MessageBox.Show("作成締日は入力必須項目です。");
                return;
            }

            //作成日付
            if (作成年 == null || 作成月 == null)
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                MessageBox.Show("作成年月は入力必須項目です。");
                return;
            }

            //集計期間
            if (集計期間From == null || 集計期間To == null)
            {
                this.ErrorMessage = "集計期間は入力必須項目です。";
                MessageBox.Show("集計期間は入力必須項目です");
                return;
            }

            string 年度 = Convert.ToString(作成月) + "月度支払書";

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

                //社内用
			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SHR02010, new object[] { 支払先From, 支払先To, 支払先ピックアップ, 作成締日, 集計期間From, 集計期間To, CValue }));
            //int? p支払先範囲指定From, int? p支払先範囲指定To, string p支払先ピックアップ, int? p作成締日, DateTime? p請求対象期間From, DateTime? p請求対象期間To, int 表示区分 , int 社内区分
        }
        
        //リボン
        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            this.Close();
        }
        
        #endregion

        #region [社内用]プレビュー画面
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
                view.MakeReport("支払先明細書", rptFullPathName_PIC, 0, 0, 0);
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

        #region [社内用]プレビュー画面
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
                view.MakeReport("支払先明細書", rptFullPathName_PIC2, 0, 0, 0);
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

        #region CSVファイル出力1
        /// <summary>
        /// CSVファイル出力
        /// </summary>
        /// <param name="tbl"></param>
        private void OutPutCSV1(DataTable tbl)
        {
            if (tbl.Rows.Count < 1)
            {
                this.ErrorMessage = "対象データが存在しません。";
                return;
            }

            #region CSV列削除

            tbl.Columns.Remove("Ｓ税区分ID");
            tbl.Columns.Remove("請求書区分ID");
            tbl.Columns.Remove("請求内訳管理区分");
            tbl.Columns.Remove("請求内訳ID");
            tbl.Columns.Remove("親子区分ID");
            tbl.Columns.Remove("親ID");
            tbl.Columns.Remove("支払金額計1");
            tbl.Columns.Remove("支払金額計2");
            tbl.Columns.Remove("支払金額計3");
            tbl.Columns.Remove("請求税区分");
            tbl.Columns.Remove("支払税区分");
            tbl.Columns.Remove("当月請求合計");
            tbl.Columns.Remove("当月売上額");
            tbl.Columns.Remove("当月通行料");
            tbl.Columns.Remove("当月課税金額");
            tbl.Columns.Remove("当月非課税金額");
            tbl.Columns.Remove("当月消費税");

            #endregion

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

        #region CSVファイル出力2
        /// <summary>
        /// CSVファイル出力
        /// </summary>
        /// <param name="tbl"></param>
        private void OutPutCSV2(DataTable tbl)
        {
            if (tbl.Rows.Count < 1)
            {
                this.ErrorMessage = "対象データが存在しません。";
                return;
            }

            #region CSV列削除

            tbl.Columns.Remove("Ｓ税区分ID");
            tbl.Columns.Remove("請求書区分ID");
            tbl.Columns.Remove("請求内訳管理区分");
            tbl.Columns.Remove("請求内訳ID");
            tbl.Columns.Remove("親子区分ID");
            tbl.Columns.Remove("親ID");
            tbl.Columns.Remove("支払金額計1");
            tbl.Columns.Remove("支払金額計2");
            tbl.Columns.Remove("支払金額計3");
            tbl.Columns.Remove("請求税区分");
            tbl.Columns.Remove("支払税区分");
            tbl.Columns.Remove("当月請求合計");
            tbl.Columns.Remove("当月売上額");
            tbl.Columns.Remove("当月通行料");
            tbl.Columns.Remove("当月課税金額");
            tbl.Columns.Remove("当月非課税金額");
            tbl.Columns.Remove("当月消費税");
            tbl.Columns.Remove("請求通行料");
            tbl.Columns.Remove("差益");
            tbl.Columns.Remove("売上金額");

            #endregion

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

            //メソッドで期間計算
            DateFromTo ret = AppCommon.GetDateFromTo(Convert.ToInt32(作成年), Convert.ToInt32(作成月), Convert.ToInt32(作成締日));
            if (ret.Result == false || ret.Kikan > 31)
            {
                return;
            }
            集計期間From = ret.DATEFrom;
            集計期間To = ret.DATETo;
           
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
            frmcfg.締日 = this.作成締日;
            frmcfg.作成年 = this.作成年;
            frmcfg.作成月 = this.作成月;
            frmcfg.集計期間From = this.集計期間From;
            frmcfg.集計期間To = this.集計期間To;
            frmcfg.区分1 = this.Combo1.SelectedIndex;
            ucfg.SetConfigValue(frmcfg);
        }

        #endregion

        #region データ検索(SPREAD用)

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (作成締日 == null)
            {
                this.ErrorMessage = "作成締日は入力必須項目です。";
                return;
            }
            if (作成年 == null)
            {
                this.ErrorMessage = "作成年は入力必須項目です。";
                return;
            }
            if (作成月 == null)
            {
                this.ErrorMessage = "作成月は入力必須項目です。";
                return;
            }

            base.SendRequest(new CommunicationObject(MessageType.RequestData, SPREAD_SHR02010, new object[] { 支払先From, 支払先To , 作成締日, 集計期間From, 集計期間To }));
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
    }
}
