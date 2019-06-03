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


using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;


namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 支払先別地区単価マスタ問合せ
    /// </summary>
    public partial class MST21020 : WindowMasterMainteBase
    {
        //データ検索用
        private const string TargetTableNm = "M03_YTAN1_ichiran";
        //CSV出力用
        private const string M03_SEARCH_MST21010_CSV = "M03_SEARCH_MST21010_CSV";
        //プレビュー出力用
        private const string M03_SEARCH_MST21010 = "M03_SEARCH_MST21010";
        //プレビュー用
        private const string rptFullPathName_PIC = @"Files\MST\MST21010.rpt";

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;

        /// <summary>
        /// 画面固有設定項目のクラス定義
        /// ※ 必ず public で定義する。
        /// </summary>
        public class ConfigMST21020 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST21020 frmcfg = null;

        #endregion

        private string _得意先コード = string.Empty;
        public string 得意先コード
        {
            get { return this._得意先コード; }
            set { this._得意先コード = value; NotifyPropertyChanged(); }
        }
        private string _得意先名 = string.Empty;
        public string 得意先名
        {
            get { return this._得意先名; }
            set { this._得意先名 = value; NotifyPropertyChanged(); }
        }



        private string _発地コード = string.Empty;
        public string 発地コード
        {
            get { return this._発地コード; }
            set { this._発地コード = value; NotifyPropertyChanged(); }
        }
        private string _発地名 = string.Empty;
        public string 発地名
        {
            get { return this._発地名; }
            set { this._発地名 = value; NotifyPropertyChanged(); }
        }

        private string _着地コード = string.Empty;
        public string 着地コード
        {
            get { return this._着地コード; }
            set { this._着地コード = value; NotifyPropertyChanged(); }
        }
        private string _着地名 = string.Empty;
        public string 着地名
        {
            get { return this._着地名; }
            set { this._着地名 = value; NotifyPropertyChanged(); }
        }
        private string _商品コード = string.Empty;
        public string 商品コード
        {
            get { return this._商品コード; }
            set { this._商品コード = value; NotifyPropertyChanged(); }
        }
        private string _商品名 = string.Empty;
        public string 商品名
        {
            get { return this._商品名; }
            set { this._商品名 = value; NotifyPropertyChanged(); }
        }
        private string _表示区分 = "0";
        public string 表示区分
        {
            get { return this._表示区分; }
            set { this._表示区分 = value; NotifyPropertyChanged(); }
        }

        private int _取引区分 = 4;
        public int 取引区分
        {
            get { return this._取引区分; }
            set { this._取引区分 = value; NotifyPropertyChanged(); }
        }

        private DataTable _mSTData;
        public DataTable MSTData
        {
            get { return this._mSTData; }
            set
            {
                this._mSTData = value;
                NotifyPropertyChanged();
            }
        }


        /// <summary>
        /// 支払先別地区単価マスタ問合せ
        /// </summary>
        public MST21020()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigMST21020)ucfg.GetConfigValue(typeof(ConfigMST21020));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST21020();
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
            }
            #endregion

            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { null, typeof(SCH01010) });
            base.MasterMaintenanceWindowList.Add("M08_TIK_UC", new List<Type> { typeof(MST03010), typeof(SCH03010) });
            base.MasterMaintenanceWindowList.Add("M09_HIN", new List<Type> { typeof(MST07010), typeof(SCH07010) });
        }


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
                view.MakeReport("支払先別品名単価マスタ", rptFullPathName_PIC, 0, 0, 0);
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

        public override void OnReceivedResponseData(CommunicationObject message)
        {
            var data = message.GetResultData();
            DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
            switch (message.GetMessageName())
            {
                //データ検索
                case TargetTableNm:
                    MSTData = tbl;

                    break;

                //プレビュー出力
                case M03_SEARCH_MST21010:
                    DispPreviw(tbl);

                    break;

                //CSV出力
                case M03_SEARCH_MST21010_CSV:
                    OutPutCSV(tbl);
                    break;
            }
            this.MSTData = null;
        }

        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            MessageBox.Show(ErrorMessage);
        }



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
                ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);
            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }
        }

        /// <summary>
        /// F2 マスタメンテ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF2Key(object sender, KeyEventArgs e)
        {
            try
            {
                ViewBaseCommon.CallMasterMainte(this.MasterMaintenanceWindowList);
            }
            catch (Exception ex)
            {
                appLog.Error("マスターメンテ画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }
        }

        /// <summary>
        /// CSVファイル出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            //Todo: 条件渡し
            base.SendRequest(new CommunicationObject(MessageType.UpdateData, M03_SEARCH_MST21010_CSV, new object[] { 得意先コード, 発地コード, 着地コード, 商品コード }));
        }


        /// <summary>
        /// 印刷
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

            //Todo: 条件渡し
            base.SendRequest(new CommunicationObject(MessageType.UpdateData, M03_SEARCH_MST21010, new object[] { 得意先コード, 発地コード, 着地コード, 商品コード }));
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

        #region Mindoow_Closed
        //画面が閉じられた時、データを保持する
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion


    }
}
