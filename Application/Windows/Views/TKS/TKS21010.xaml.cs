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
    /// 支払先明細書印刷画面
    /// </summary>
    public partial class TKS21010 : WindowReportBase
    {
        //部門売上管理表プレビュー定数
        private const string SEARCH_TKS21010 = "SEARCH_TKS21010";
        //部門売上管理表プレビュー定数
        private const string SEARCH_TKS21011 = "SEARCH_TKS21011";
        //部門売上管理表CSV定数
        private const string SEARCH_TKS21010_CSV = "SEARCH_TKS21010_CSV";
        //部門売上管理表CSV定数
        private const string SEARCH_TKS21011_CSV = "SEARCH_TKS21011_CSV";

            //部門売上合計表レポート定数
            private const string rptFullPathName_PIC = @"Files\TKS\TKS21010.rpt";
            //前年対比レポート定数
            private const string rptFullPathName_PIC1 = @"Files\TKS\TKS21011.rpt";

            #region 画面設定項目
            /// <summary>
            /// ユーザ設定項目
            /// </summary>
            UserConfig ucfg = null;

            /// <summary>
            /// 画面固有設定項目のクラス定義
            /// ※ 必ず public で定義する。
            /// </summary>
            public class ConfigTKS21010 : FormConfigBase
            {
                public string 作成年 { get; set; }
                public string 作成月 { get; set; }
                public string 締日 { get; set; }
                public DateTime? 集計期間From { get; set; }
                public DateTime? 集計期間To { get; set; }
                public bool? チェック { get; set; }
            }
            /// ※ 必ず public で定義する。
            public ConfigTKS21010 frmcfg = null;

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


            private DataTable _部門売上明細書データ = null;
            public DataTable 部門売上明細書データ
            {
                get { return this._部門売上明細書データ; }
                set
                {
                    this._部門売上明細書データ = value;
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

            #region TKS21010
            /// <summary>
            /// 部門売上売上管理表
            /// </summary>
            public TKS21010()
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
                
                #region 設定項目取得
                ucfg = AppCommon.GetConfig(this);
                frmcfg = (ConfigTKS21010)ucfg.GetConfigValue(typeof(ConfigTKS21010));
                if (frmcfg == null)
                {
                    frmcfg = new ConfigTKS21010();
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
                        case SEARCH_TKS21010:
                            DispPreviw(tbl);
                            break;

                        //締日プレビュー出力用
                        case SEARCH_TKS21011:
                            DispPreviw1(tbl);
                            break;

                        //締日CSV出力用
                        case SEARCH_TKS21010_CSV:
                            tbl.Columns["月1"].ColumnName = tbl.Rows[0]["月1項目"].ToString();
                            tbl.Columns["月2"].ColumnName = tbl.Rows[0]["月2項目"].ToString();
                            tbl.Columns["月3"].ColumnName = tbl.Rows[0]["月3項目"].ToString();
                            tbl.Columns["月4"].ColumnName = tbl.Rows[0]["月4項目"].ToString();
                            tbl.Columns["月5"].ColumnName = tbl.Rows[0]["月5項目"].ToString();
                            tbl.Columns["月6"].ColumnName = tbl.Rows[0]["月6項目"].ToString();
                            tbl.Columns["月7"].ColumnName = tbl.Rows[0]["月7項目"].ToString();
                            tbl.Columns["月8"].ColumnName = tbl.Rows[0]["月8項目"].ToString();
                            tbl.Columns["月9"].ColumnName = tbl.Rows[0]["月9項目"].ToString();
                            tbl.Columns["月10"].ColumnName = tbl.Rows[0]["月10項目"].ToString();
                            tbl.Columns["月11"].ColumnName = tbl.Rows[0]["月11項目"].ToString();
                            tbl.Columns["月12"].ColumnName = tbl.Rows[0]["月12項目"].ToString();
                            tbl.Columns.Remove("月1項目");
                            tbl.Columns.Remove("月2項目");
                            tbl.Columns.Remove("月3項目");
                            tbl.Columns.Remove("月4項目");
                            tbl.Columns.Remove("月5項目");
                            tbl.Columns.Remove("月6項目");
                            tbl.Columns.Remove("月7項目");
                            tbl.Columns.Remove("月8項目");
                            tbl.Columns.Remove("月9項目");
                            tbl.Columns.Remove("月10項目");
                            tbl.Columns.Remove("月11項目");
                            tbl.Columns.Remove("月12項目");
                            tbl.Columns.Remove("項目2");
                            OutPutCSV(tbl);
                            break;

                        //締日CSV出力用
                        case SEARCH_TKS21011_CSV:
                            tbl.Columns["月1"].ColumnName = tbl.Rows[0]["月1項目"].ToString();
                            tbl.Columns["月2"].ColumnName = tbl.Rows[0]["月2項目"].ToString();
                            tbl.Columns["月3"].ColumnName = tbl.Rows[0]["月3項目"].ToString();
                            tbl.Columns["月4"].ColumnName = tbl.Rows[0]["月4項目"].ToString();
                            tbl.Columns["月5"].ColumnName = tbl.Rows[0]["月5項目"].ToString();
                            tbl.Columns["月6"].ColumnName = tbl.Rows[0]["月6項目"].ToString();
                            tbl.Columns["月7"].ColumnName = tbl.Rows[0]["月7項目"].ToString();
                            tbl.Columns["月8"].ColumnName = tbl.Rows[0]["月8項目"].ToString();
                            tbl.Columns["月9"].ColumnName = tbl.Rows[0]["月9項目"].ToString();
                            tbl.Columns["月10"].ColumnName = tbl.Rows[0]["月10項目"].ToString();
                            tbl.Columns["月11"].ColumnName = tbl.Rows[0]["月11項目"].ToString();
                            tbl.Columns["月12"].ColumnName = tbl.Rows[0]["月12項目"].ToString();
                            tbl.Columns.Remove("月1項目");
                            tbl.Columns.Remove("月2項目");
                            tbl.Columns.Remove("月3項目");
                            tbl.Columns.Remove("月4項目");
                            tbl.Columns.Remove("月5項目");
                            tbl.Columns.Remove("月6項目");
                            tbl.Columns.Remove("月7項目");
                            tbl.Columns.Remove("月8項目");
                            tbl.Columns.Remove("月9項目");
                            tbl.Columns.Remove("月10項目");
                            tbl.Columns.Remove("月11項目");
                            tbl.Columns.Remove("月12項目");
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
                    view.MakeReport("部門別売上管理表", rptFullPathName_PIC, 0, 0, 0);
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

            #region プレビュー画面 前年対比
            /// <summary>
            /// プレビュー画面表示
            /// </summary>
            /// <param name="tbl"></param>
            private void DispPreviw1(DataTable tbl)
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
                    view.MakeReport("部門別売上推移表", rptFullPathName_PIC1, 0, 0, 0);
                    //帳票ファイルに送るデータ。
                    //帳票データの列と同じ列名を保持したDataTableを引数とする
                    view.SetReportData(tbl);
					view.ShowPreview();
					view.Close();

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
                //DateTime d集計期間From = DateTime.Parse(集計期間From.ToString()), d集計期間To = DateTime.Parse(集計期間To.ToString());
                DateTime d集計期間From;
                if (!DateTime.TryParse(集計期間From.ToString(), out d集計期間From))
                {
                    this.ErrorMessage = "集計期間の形式が不正です。";
                    return;
                }
                DateTime d集計期間To;
                if (!DateTime.TryParse(集計期間To.ToString(), out d集計期間To))
                {
                    this.ErrorMessage = "集計期間の形式が不正です。";
                    return;
                }


                #region 各月ごとの開始終了日付を取得

                DateTime[] 開始日付 = new DateTime[12];
                DateTime[] 終了日付 = new DateTime[12];


                for (int i = 0; i <= 11; i++)
                {
                    //締日入力時
                    //メソッドで期間計算
                    DateFromTo ret = AppCommon.GetDateFromTo(Convert.ToInt32(d集計期間To.AddMonths(i).Year), Convert.ToInt32(d集計期間To.AddMonths(i).Month), Convert.ToInt32(作成締日));
                    if (ret.Result == false || ret.Kikan > 31)
                    {
                        this.ErrorMessage = "入力値エラーです。もう一度入力してください。";
                        MessageBox.Show("入力値エラーです。もう一度入力してください。");
                        SetFocusToTopControl();
                        return;
                    }
                    開始日付[i] = ret.DATEFrom;
                    終了日付[i] = ret.DATETo;

                }

                #endregion

                if (前年対比 == 0)
                {
                    base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_TKS21010_CSV, new object[] { 部門From, 部門To, i部門List, 部門ピックアップ, 作成締日, 開始日付, 終了日付 }));
                }
                else
                {
					base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_TKS21011_CSV, new object[] { 部門From, 部門To, i部門List, 部門ピックアップ, 作成締日, 開始日付, 終了日付, 前年対比 }));
                }
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

                if (集計期間From == null || 集計期間To == null)
                {
                    this.ErrorMessage = "入力値エラーです。もう一度入力してください。";
                    MessageBox.Show("入力値エラーです。もう一度入力してください。");
                    SetFocusToTopControl();
                    return;
                }

                DateTime d集計期間From = DateTime.Parse(集計期間From.ToString()), d集計期間To = DateTime.Parse(集計期間To.ToString());



                #region 各月ごとの開始終了日付を取得

                DateTime[] 開始日付 = new DateTime[12];
                DateTime[] 終了日付 = new DateTime[12];


                for (int i = 0; i <= 11; i++)
                {
                    //締日入力時
                    //メソッドで期間計算
                    DateFromTo ret = AppCommon.GetDateFromTo(Convert.ToInt32(d集計期間To.AddMonths(i).Year), Convert.ToInt32(d集計期間To.AddMonths(i).Month), Convert.ToInt32(作成締日));
                    if (ret.Result == false || ret.Kikan > 31)
                    {
                        this.ErrorMessage = "入力値エラーです。もう一度入力してください。";
                        MessageBox.Show("入力値エラーです。もう一度入力してください。");
                        SetFocusToTopControl();
                        return;
                    }
                    開始日付[i] = ret.DATEFrom;
                    終了日付[i] = ret.DATETo;

                }

                #endregion

                if (前年対比 == 0)
                {
					base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_TKS21010, new object[] { 部門From, 部門To, i部門List, 部門ピックアップ, 作成締日, 開始日付, 終了日付 }));
                }
                else
                {
					base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_TKS21011, new object[] { 部門From, 部門To, i部門List, 部門ピックアップ, 作成締日, 開始日付, 終了日付, 前年対比 }));
                }

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

				var yesno = MessageBox.Show("プレビューを表示しますか？", "確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
				if (yesno == MessageBoxResult.No)
				{
					return;
				}
                OnF8Key(sender, null);

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
                    OnF8Key(sender, null);
                }
            }
            #endregion

    }
}