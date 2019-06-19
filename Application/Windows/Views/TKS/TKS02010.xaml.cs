﻿using System;
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
    public partial class TKS02010 : WindowReportBase
    {
        #region 定数定義

        //CSV出力用定数
        private const string SEARCH_TKS02010_CSV = "SEARCH_TKS02010_CSV";
        //プレビュー出力用定数
        private const string SEARCH_TKS02010 = "SEARCH_TKS02010";
        //レポート
        private const string rptFullPathName_PIC = @"Files\TKS\TKS02010.rpt";
        //Spread
        private const string SPREAD_TKS02010 = "SPREAD_TKS02010";

        #endregion

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;

        /// <summary>
        /// 画面固有設定項目のクラス定義
        /// ※ 必ず public で定義する。
        /// </summary>
        public class ConfigTKS02010 : FormConfigBase
        {
            public int? 作成年 { get; set; }
            public int? 作成月 { get; set; }
            public string 締日 { get; set; }
            public string 自社ID { get; set; }
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
        public ConfigTKS02010 frmcfg = null;

        #endregion

        #region バインド用プロパティ

        private string _自社ID = string.Empty;
        public string 自社ID
        {
            get { return this._自社ID; }
            set { this._自社ID = value; NotifyPropertyChanged(); }
        }

        private string _自社名 = string.Empty;
        public string 自社名
        {
            get { return this._自社名; }
            set { this._自社名 = value; NotifyPropertyChanged(); }
        }
         private string _得意先コード = string.Empty;
         public string 得意先コード
        {
            get { return this._得意先コード; }
            set { this._得意先コード = value; NotifyPropertyChanged(); }
        }


        string _得意先ピックアップ = string.Empty;
        public string 得意先ピックアップ
        {
            get { return this._得意先ピックアップ; }
            set { this._得意先ピックアップ = value; NotifyPropertyChanged(); }
        }

        string _作成締日;
        public string 作成締日
        {
            get { return this._作成締日; }
            set { this._作成締日 = value; NotifyPropertyChanged(); }
        }

        DateTime? _p作成年月;
        public DateTime? 作成年月
        {
            get { return this._p作成年月; }
            set { this._p作成年月 = value; NotifyPropertyChanged(); }
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

        DateTime? _出力日付 = DateTime.Today;
        public DateTime? 出力日付
        {
            get { return this._出力日付; }
            set { this._出力日付 = value; NotifyPropertyChanged(); }
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

        private int _取引区分 = 1;
        public int 取引区分
        {
            get { return this._取引区分; }
            set { this._取引区分 = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region LOAD処理

        /// <summary>
        /// 得意先売上明細書
        /// </summary>
        public TKS02010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// 画面読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigTKS02010)ucfg.GetConfigValue(typeof(ConfigTKS02010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigTKS02010();
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
                this.自社ID = frmcfg.自社ID;
            }
            #endregion

            //得意先ID用
            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { null, typeof(SCH01010) });
            //自社名ID用
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { null, typeof(SCH12010) });

        }

        #endregion

        #region 画面クリア

        private void ScreenClear()
        {

        }

        #endregion

        #region データ受信メソッド
        /// <summary>
        /// データ受信メソッド
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            try
            {
                var data = message.GetResultData();

                if (data is DataTable)
                {
                    DataTable tbl = data as DataTable;

                    switch (message.GetMessageName())
                    {
                        //検索結果取得時
                        case SEARCH_TKS02010:
                            DispPreviw(tbl);
                            break;

                        case SEARCH_TKS02010_CSV:
                            OutPutCSV(tbl);
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }
        }
        #endregion

        #region エラーメッセージ表示

        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            this.ErrorMessage = (string)message.GetResultData();
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
        /// F5　CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {

            try
            {
                if (!base.CheckAllValidation())
                {
                    MessageBox.Show("入力内容に誤りがあります。");
                    SetFocusToTopControl();
                    return;
                }

                int i得意先コード = 0;
                int i自社ID = 0;
                int i作成締日 = 0;

                if (作成締日 == null)
                {
                    this.ErrorMessage = "作成締日は入力必須項目です。";
                    MessageBox.Show("作成締日は入力必須項目です。");
                    return;
                }

                if (作成年 == null || 作成月 == null)
                {
                    this.ErrorMessage = "作成年月は入力必須項目です。";
                    MessageBox.Show("作成年月は入力必須項目です。");
                    return;
                }

                if (集計期間From == null || 集計期間To == null)
                {
                    this.ErrorMessage = "集計期間は入力必須項目です。";
                    MessageBox.Show("集計期間は入力必須項目です。");
                    return;
                }

                if (!int.TryParse(得意先コード, out i得意先コード))
                {
                    //this.ErrorMessage = "得意先IDの入力形式が不正です。";
                    //MessageBox.Show("得意先IDの入力形式が不正です。");
                }

                if (!int.TryParse(自社ID, out i自社ID))
                {
                    //this.ErrorMessage = "自社IDの入力形式が不正です。";
                    //MessageBox.Show("自社IDの入力形式が不正です。");
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

                if (int.TryParse(_作成締日, out i作成締日))
                {
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, SEARCH_TKS02010_CSV, new object[] { i得意先コード, i得意先List, i自社ID, i作成締日, 集計期間From, 集計期間To }));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }


        /// <summary>
        /// F8　リボン印刷
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


            try
            {
                if (!base.CheckAllValidation())
                {
                    MessageBox.Show("入力内容に誤りがあります。");
                    SetFocusToTopControl();
                    return;
                }

                int i得意先コード = 0;
                int i自社ID = 0;
                int i作成締日 = 0;

                if (作成締日 == null)
                {
                    this.ErrorMessage = "作成締日は入力必須項目です。";
                    MessageBox.Show("作成締日は入力必須項目です。");
                    return;
                }

                if (作成年 == null || 作成月 == null)
                {
                    this.ErrorMessage = "作成年月は入力必須項目です。";
                    MessageBox.Show("作成年月は入力必須項目です。");
                    return;
                }

                if (集計期間From == null || 集計期間To == null)
                {
                    this.ErrorMessage = "集計期間は入力必須項目です。";
                    MessageBox.Show("集計期間は入力必須項目です。");
                    return;
                }

                if (!int.TryParse(得意先コード, out i得意先コード))
                {
                    //this.ErrorMessage = "得意先IDの入力形式が不正です。";
                    //MessageBox.Show("得意先IDの入力形式が不正です。");
                }

                if (!int.TryParse(自社ID, out i自社ID))
                {
                    //this.ErrorMessage = "自社IDの入力形式が不正です。";
                    //MessageBox.Show("自社IDの入力形式が不正です。");
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

                if (int.TryParse(_作成締日, out i作成締日))
                {
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, SEARCH_TKS02010, new object[] { i得意先コード, i得意先List, i自社ID, i作成締日, 集計期間From, 集計期間To }));
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
            
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
                this.ErrorMessage = "入力値エラーです。もう一度入力してください。";
                MessageBox.Show("入力値エラーです。もう一度入力してください。");
                SetFocusToTopControl();
                return;
            }
            集計期間From = ret.DATEFrom;
            集計期間To = ret.DATETo;
           
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
                view.MakeReport("内訳請求書発行", rptFullPathName_PIC, 0, 0, 0);
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
            frmcfg.自社ID = this.自社ID;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion

        #region SPREAD用(データ検索)

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Spread内に印刷できるデータを表示するためのSendRequest
            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }

            if (得意先コード == null)
            {
                this.ErrorMessage = "得意先コードは入力必須項目です。";
                return;
            }
            if (自社ID == null)
            {
                this.ErrorMessage = "自社IDは入力必須項目です。";
                return;
            }
            if (作成締日 == null)
            {
                this.ErrorMessage = "作成締日は入力必須項目です。";
                return;
            }
            if (作成年 == null || 作成月 == null)
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                return;
            }
            if (集計期間From == null || 集計期間To == null)
            {
                this.ErrorMessage = "集計期間は入力必須項目です。";
                return;
            }

            base.SendRequest(new CommunicationObject(MessageType.RequestData, SPREAD_TKS02010, new object[] { 得意先コード, 自社ID, 作成締日, 集計期間From, 集計期間To }));
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
