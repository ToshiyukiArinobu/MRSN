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
    /// 取引先別相殺管理表
    /// </summary>
    public partial class TKS14010 : WindowReportBase
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
        public class ConfigTKS14010 : FormConfigBase
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
        public ConfigTKS14010 frmcfg = null;

        #endregion

        #region 定数
        //CSV出力用定数
        private const string SEARCH_TKS14010_CSV = "SEARCH_TKS14010_CSV";
        //プレビュー出力用定数
        private const string SEARCH_TKS14010 = "SEARCH_TKS14010";
        //レポート  
        private const string rptFullPathName_PIC = @"Files\SHR\SHR13010.rpt";
        #endregion

        #region バインド用プロパティ

        private string _取引先ピックアップ = string.Empty;
        public string 取引先ピックアップ
        {
            set { _取引先ピックアップ = value; NotifyPropertyChanged(); }
            get { return _取引先ピックアップ; }
        }
        private string _取引先From = string.Empty;
        public string 取引先From
        {
            set { _取引先From = value; NotifyPropertyChanged(); }
            get { return _取引先From; }
        }
        private string _取引先To = string.Empty;
        public string 取引先To
        {
            set { _取引先To = value; NotifyPropertyChanged(); }
            get { return _取引先To; }
        }
        private string _作成年 = string.Empty;
        public string 作成年
        {
            set { _作成年 = value; NotifyPropertyChanged(); }
            get { return _作成年; }
        }

        private string _作成月 = string.Empty;
        public string 作成月
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

        private int _取引区分 = 1;
        public int 取引区分
        {
            get { return this._取引区分; }
            set { this._取引区分 = value; NotifyPropertyChanged(); }
        }

        private int _作成区分 = 0;
        public int 作成区分
        {
            get { return this._作成区分; }
            set { this._作成区分 = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region TKS14010

        /// <summary>
        /// 得意先売上明細書
        /// </summary>
        public TKS14010()
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
            frmcfg = (ConfigTKS14010)ucfg.GetConfigValue(typeof(ConfigTKS14010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigTKS14010();
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
                this.作成区分_Cmb.SelectedIndex = frmcfg.区分1;
                this.取引区分_Cmb.SelectedIndex = frmcfg.区分2;
            }
            #endregion
            AppCommon.SetutpComboboxList(this.取引区分_Cmb, false);
            AppCommon.SetutpComboboxList(this.作成区分_Cmb, false);
            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { null, typeof(SCH01010) });

			SetFocusToTopControl();
        }

        #endregion

        #region エラーメッセージ

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
                var data = message.GetResultData();
                DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
                switch (message.GetMessageName())
                {
                    //検索結果取得時
                    case SEARCH_TKS14010:
                        DispPreviw(tbl);
                        break;

                    case SEARCH_TKS14010_CSV:
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
        /// マスタメンテ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF2Key(object sender, KeyEventArgs e)
        {

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

            if (string.IsNullOrEmpty(作成年) || string.IsNullOrEmpty(作成月))
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                MessageBox.Show("作成年月は入力必須項目です。");
                return;
            }

            //ComboBox
            int i作成区分, i取引区分;
            i作成区分 = 作成区分_Cmb.SelectedIndex;
            i取引区分 = 取引区分_Cmb.SelectedIndex;

            //日付
            int i作成年 = Convert.ToInt32(作成年);
            int i作成月 = Convert.ToInt32(作成月);
            DateTime d集計月 = Convert.ToDateTime(作成年 + "/" + 作成月 + "/01");
            string s作成年月度 = i作成年.ToString() + "年" + i作成月.ToString() + "月度";

            int?[] i取引先List = new int?[0];
            if (!string.IsNullOrEmpty(取引先ピックアップ))
            {
                string[] 取引先List = 取引先ピックアップ.Split(',');
                i取引先List = new int?[取引先List.Length];

                for (int i = 0; i < 取引先List.Length; i++)
                {
                    string str = 取引先List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "得意先指定の形式が不正です。";
                        return;
                    }
                    i取引先List[i] = code;
                }
            }

            //帳票出力用
			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_TKS14010_CSV, new object[] { 取引先From, 取引先To, i取引先List, i作成年, i作成月, d集計月, s作成年月度, i作成区分, i取引区分 }));
        
        }

        /// <summary>
        /// F8　リボン印刷
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

            if (string.IsNullOrEmpty(作成年) || string.IsNullOrEmpty(作成月))
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                MessageBox.Show("作成年月は入力必須項目です。");
                return;
            }

            //ComboBox
            int i作成区分, i取引区分;
            i作成区分 = 作成区分_Cmb.SelectedIndex;
            i取引区分 = 取引区分_Cmb.SelectedIndex;

            //日付
            int i作成年 = Convert.ToInt32(作成年);
            int i作成月 = Convert.ToInt32(作成月);
            DateTime d集計月 = Convert.ToDateTime(作成年 + "/" + 作成月 + "/01");
            string s作成年月度 = i作成年.ToString() + "年" + i作成月.ToString() + "月度";

            int?[] i取引先List = new int?[0];
            if (!string.IsNullOrEmpty(取引先ピックアップ))
            {
                string[] 取引先List = 取引先ピックアップ.Split(',');
                i取引先List = new int?[取引先List.Length];

                for (int i = 0; i < 取引先List.Length; i++)
                {
                    string str = 取引先List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "得意先指定の形式が不正です。";
                        return;
                    }
                    i取引先List[i] = code;
                }
            }

            //帳票出力用
			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_TKS14010, new object[] { 取引先From, 取引先To, i取引先List, i作成年, i作成月, d集計月, s作成年月度, i作成区分, i取引区分 }));
        
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
                view.MakeReport("取引先別相殺管理表", rptFullPathName_PIC, 0, 0, 0);
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

        #region 日付処理

        //作成年がNullの場合は今年の年を挿入
        private void Lost_Year(object sender, RoutedEventArgs e)
        {
            if (作成年 == "")
            {
                作成年 = DateTime.Today.ToString().Substring(0, 4);
            }
        }

        //作成月がNullの場合は今月の月を挿入
        private void Lost_Month(object sender, RoutedEventArgs e)
        {
            if (作成月 == "")
            {
                作成月 = DateTime.Today.ToString().Substring(5, 2);
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
            frmcfg.作成年 = this.作成年;
            frmcfg.作成月 = this.作成月;
            frmcfg.区分1 = this.作成区分_Cmb.SelectedIndex;
            frmcfg.区分2 = this.取引区分_Cmb.SelectedIndex;
            ucfg.SetConfigValue(frmcfg);
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