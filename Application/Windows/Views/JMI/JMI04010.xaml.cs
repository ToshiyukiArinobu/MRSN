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
    /// 乗務員明細書印刷画面
    /// </summary>
    public partial class JMI04010 : WindowReportBase
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
        public class ConfigJMI04010 : FormConfigBase
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
        public ConfigJMI04010 frmcfg = null;

        #endregion

        #region 定数
        //CSV出力用定数
        private const string SEARCH_JMI04010_CSV = "SEARCH_JMI04010_CSV";
        //プレビュー出力用定数
        private const string SEARCH_JMI04010 = "SEARCH_JMI04010";
        //レポート
        private const string rptFullPathName_PIC = @"Files\JMI\JMI04010.rpt";
        #endregion

        #region バインド用プロパティ
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

        private int? _作成締日 = null;
        public int? 作成締日
        {
            set { _作成締日 = value; NotifyPropertyChanged(); }
            get { return _作成締日; }
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

        private int _取引区分 = 2;
        public int 取引区分
        {
            get { return this._取引区分; }
            set { this._取引区分 = value; NotifyPropertyChanged(); }
        }

        private DataTable _日付TBL = null;
        public DataTable 日付TBL
        {
            get { return this._日付TBL; }
            set { this._日付TBL = value; NotifyPropertyChanged(); }
        }
        private int _ReturnValue = 0;
        public int ReturnValue
        {
            get { return this._ReturnValue; }
            set { this._ReturnValue = value; NotifyPropertyChanged(); }
        }
        private int _期間日数 = 0;
        public int 期間日数
        {
            get { return this._期間日数; }
            set { this._期間日数 = value; NotifyPropertyChanged(); }
        }


        #endregion

        #region JMI04010()
        /// <summary>
        /// 乗務員明細書印刷画面
        /// </summary>
        public JMI04010()
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
            frmcfg = (ConfigJMI04010)ucfg.GetConfigValue(typeof(ConfigJMI04010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigJMI04010();
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
                this.作成締日 = frmcfg.締日;
                this.作成年 = frmcfg.作成年;
                this.作成月 = frmcfg.作成月;
                this.集計期間From = frmcfg.集計期間From;
                this.集計期間To = frmcfg.集計期間To;
            }
            #endregion

            //得意先ID用
            base.MasterMaintenanceWindowList.Add("M04_DRV", new List<Type> { null, typeof(SCH04010) });

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
                    case SEARCH_JMI04010:
                        DispPreviw(tbl);
                        break;

                    case SEARCH_JMI04010_CSV:
						if (tbl == null || tbl.Rows.Count == 0)
						{
							return;
						}
						tbl.Columns["経費1"].ColumnName = (string)tbl.Rows[0]["経費名1"];
						tbl.Columns["経費2"].ColumnName = (string)tbl.Rows[0]["経費名2"];
						tbl.Columns["経費3"].ColumnName = (string)tbl.Rows[0]["経費名3"];
						tbl.Columns["経費4"].ColumnName = (string)tbl.Rows[0]["経費名4"];
						tbl.Columns["経費5"].ColumnName = (string)tbl.Rows[0]["経費名5"];
						tbl.Columns["経費6"].ColumnName = (string)tbl.Rows[0]["経費名6"];
						tbl.Columns["経費7"].ColumnName = (string)tbl.Rows[0]["経費名7"];
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
                    srch.表示順 = 0;
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

            ////集計期間がNullの場合Max,Minを設定
            //if (集計期間From == null)
            //{
            //    集計期間From = DateTime.MinValue;
            //}
            //if (集計期間To == null)
            //{
            //    集計期間To = DateTime.MaxValue;
            //}

            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }

            ////締日がNullの場合
            if (作成締日 == null)
            {
                this.ErrorMessage = "締日は入力必須項目です。";
                MessageBox.Show("締日は入力必須項目です。");
                return;
            }

            ////作成日付
            if (作成年 == null || 作成月 == null)
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                MessageBox.Show("作成年月は入力必須項目です。");
                return;
            }

            string 年度 = Convert.ToString(作成月) + "月度支払書";

            int?[] i乗務員List = new int?[0];
            if (!string.IsNullOrEmpty(乗務員ピックアップ))
            {
                string[] 乗務員List = 乗務員ピックアップ.Split(',');
                i乗務員List = new int?[乗務員List.Length];

                for (int i = 0; i < 乗務員List.Length; i++)
                {
                    string str = 乗務員List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "乗務員指定の形式が不正です。";
                        return;
                    }
                    i乗務員List[i] = code;
                }
            }

            //CSV出力用
			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_JMI04010_CSV, new object[] { 乗務員From, 乗務員To, i乗務員List, 作成締日, 集計期間From, 集計期間To, 年度, 乗務員ピックアップ }));
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

            ////集計期間がNullの場合Max,Minを設定
            //if (集計期間From == null)
            //{
            //    集計期間From = DateTime.MinValue;
            //}
            //if (集計期間To == null)
            //{
            //    集計期間To = DateTime.MaxValue;
            //}

            if (!base.CheckAllValidation())
            {
                MessageBox.Show("入力内容に誤りがあります。");
                SetFocusToTopControl();
                return;
            }

            ////締日がNullの場合
            if (作成締日 == null)
            {
                this.ErrorMessage = "締日は入力必須項目です。";
                MessageBox.Show("締日は入力必須項目です。");
                return;
            }

            ////作成日付
            if (作成年 == null || 作成月 == null)
            {
                this.ErrorMessage = "作成年月は入力必須項目です。";
                MessageBox.Show("作成年月は入力必須項目です。");
                return;
            }

            string 年度 = Convert.ToString(作成月) + "月度支払書";

            int?[] i乗務員List = new int?[0];
            if (!string.IsNullOrEmpty(乗務員ピックアップ))
            {
                string[] 乗務員List = 乗務員ピックアップ.Split(',');
                i乗務員List = new int?[乗務員List.Length];

                for (int i = 0; i < 乗務員List.Length; i++)
                {
                    string str = 乗務員List[i];
                    int code;
                    if (!int.TryParse(str, out code))
                    {
                        this.ErrorMessage = "乗務員指定の形式が不正です。";
                        return;
                    }
                    i乗務員List[i] = code;
                }
            }

            //帳票出力用
			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_JMI04010, new object[] { 乗務員From, 乗務員To, i乗務員List, 作成締日, 集計期間From, 集計期間To, 年度, 乗務員ピックアップ }));

        }

        //リボン
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
                view.MakeReport("乗務員管理合計表", rptFullPathName_PIC, 0, 0, 0);
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
                作成年 = iDate;
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
                作成月 = iDate;
            }
            else if (作成月 <= 0 || 作成月 > 12)
            {
                this.ErrorMessage = "入力値エラーです。もう一度入力してください。";
                MessageBox.Show("入力値エラーです。もう一度入力してください。");
                作成月 = null;
                return;
            }

            //Kikan_Keisan();

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


    }
}
