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
    public partial class TKS06010 : WindowReportBase
    {
            #region データアクセスID
            //得意先締日集計プレビュー定数
            private const string SEARCH_TKS06010_Simebi = "SEARCH_TKS06010_Simebi";
            //得意先月次集計プレビュー定数
            private const string SEARCH_TKS06010_Getsuji = "SEARCH_TKS06010_Getsuji";
            //得意先締日集計CSV定数
            private const string SEARCH_TKS06010_Simebi_CSV = "SEARCH_TKS06010_Simebi_CSV";
            //得意先月次集計CSV定数
            private const string SEARCH_TKS06010_Getsuji_CSV = "SEARCH_TKS06010_Getsuji_CSV";
            //得意先売上合計表レポート定数
            private const string rptFullPathName_PIC = @"Files\TKS\TKS06010.rpt";
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
            public class ConfigTKS06010 : FormConfigBase
            {
                public string 作成年 { get; set; }
                public string 作成月 { get; set; }
                public int? 集計年月 { get; set; }
                public DateTime? 集計期間From { get; set; }
                public DateTime? 集計期間To { get; set; }
            }

            /// ※ 必ず public で定義する。
            public ConfigTKS06010 frmcfg = null;

            #endregion

            //データバインド用変数

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

            //集計年月
            private int? _集計年月;
            public int? 集計年月
            {
                get
                {
                    return this._集計年月;
                }
                set
                {
                    this._集計年月 = value;
                    NotifyPropertyChanged();
                }
            }

            //集計期間From
            DateTime? _集計期間From = DateTime.Today;
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
            DateTime? _集計期間To = DateTime.Today;
            public DateTime? 集計期間To
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

            //集計区分コンボ
            private int _集計区分_Combo;
            public int 集計区分_Combo
            {
                get
                {
                    return this._集計区分_Combo;
                }
                set
                {
                    this._集計区分_Combo = value;
                    NotifyPropertyChanged();
                }
            }

            //作成区分コンボ
            private int _作成区分_Combo = 0;
            public int 作成区分_Combo
            {
                get
                {
                    return this._作成区分_Combo;
                }
                set
                {
                    this._作成区分_Combo = value;
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

            //内訳別合計_checkbox
            private bool _内訳別合計 = false;
            public bool 内訳別合計
            {
                set
                {
                    _内訳別合計 = value;
                    NotifyPropertyChanged();
                }
                get { return _内訳別合計; }
            }

            //得意先売上明細書データ
            private DataTable _GoukeiData = null;
            public DataTable GoukeiData
            {
                get { return this._GoukeiData; }
                set
                {
                    this._GoukeiData = value;
                    NotifyPropertyChanged();
                }
            }

            private int _取引区分 = 1;
            public int 取引区分
            {
                get { return this._取引区分; }
                set { this._取引区分 = value; NotifyPropertyChanged(); }
            }

            #region
            /// <summary>
            /// 得意先売上合計表
            /// </summary>
            public TKS06010()
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
                AppCommon.SetutpComboboxList(this.集計区分_Combo1, false);
                AppCommon.SetutpComboboxList(this.作成区分_Combo1, false);
                集計区分_Combo = 0;
                作成区分_Combo = 0;
                表示順序_Combo1.SelectedIndex = 0;

                #region 設定項目取得
                ucfg = AppCommon.GetConfig(this);
                frmcfg = (ConfigTKS06010)ucfg.GetConfigValue(typeof(ConfigTKS06010));
                if (frmcfg == null)
                {
                    frmcfg = new ConfigTKS06010();
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
                    this.集計年月 = frmcfg.集計年月;
                    this.集計期間From = frmcfg.集計期間From;
                    this.集計期間To = frmcfg.集計期間To;
                }
                #endregion

                //画面サイズをタスクバーをのぞいた状態で表示させる
                //this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;

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

            #region 画面反映プログラム

            private void SetTblData(DataTable tbl)
            {

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
                        case SEARCH_TKS06010_Simebi:
                            DispPreviw(tbl);
                            break;

                        //締日CSV出力用
                        case SEARCH_TKS06010_Simebi_CSV:
                            OutPutCSV(tbl);
                            break;

                        //月次プレビュー出力用
                        case SEARCH_TKS06010_Getsuji:
                            DispPreviw(tbl);
                            break;

                        //月次CSV出力用
                        case SEARCH_TKS06010_Getsuji_CSV:
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
                    view.MakeReport("得意先売上合計表", rptFullPathName_PIC, 0, 0, 0);
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
                //支払,集計,作成区分のインデックス値取得                
                int 集計区分_CValue;
                int 作成区分_CValue;
                集計区分_CValue = 集計区分_Combo1.SelectedIndex;
                作成区分_CValue = 作成区分_Combo1.SelectedIndex;
                表示順序 = 表示順序_Combo1.SelectedIndex;
                if (!base.CheckAllValidation())
                {
                    MessageBox.Show("入力内容に誤りがあります。");
                    SetFocusToTopControl();
                    return;
                }

                if (!string.IsNullOrEmpty(作成締日) && 全締日集計 == true)
                {
                    this.ErrorMessage = "作成締日、全締日は同時に入力できません。";
                    MessageBox.Show("作成締日、全締日は同時に入力できません。");
                    return;
                }
                else if (string.IsNullOrEmpty(作成締日) && 全締日集計 == false)
                {
                    this.ErrorMessage = "締日を入力してください。";
                    MessageBox.Show("締日を入力してください。");
                    return;
                }

                if (string.IsNullOrEmpty(作成年) || string.IsNullOrEmpty(作成月))
                {
                    this.ErrorMessage = "作成年月は入力必須項目です。";
                    MessageBox.Show("作成年月は入力必須項目です。");
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

                if (集計区分_CValue == 0)
                {
                    //締日帳票出力用
					base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_TKS06010_Simebi_CSV, new object[] { 得意先From, 得意先To, i得意先List, 集計区分_CValue, 作成締日, 全締日集計, 作成年, 作成月, 表示順序, 作成区分_CValue, 内訳別合計, 集計期間From, 集計期間To, 集計年月 }));
                }
                else
                {
                    //月次帳票出力用
					base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_TKS06010_Getsuji_CSV, new object[] { 得意先From, 得意先To, i得意先List, 集計区分_CValue, 作成締日, 全締日集計, 作成年, 作成月, 表示順序, 作成区分_CValue, 内訳別合計, 集計期間From, 集計期間To, 集計年月 }));
                }
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


                //支払,集計,作成区分のインデックス値取得                
                int 集計区分_CValue;
                int 作成区分_CValue;
                集計区分_CValue = 集計区分_Combo1.SelectedIndex;
                作成区分_CValue = 作成区分_Combo1.SelectedIndex;
                表示順序 = 表示順序_Combo1.SelectedIndex;

                if (!base.CheckAllValidation())
                {
                    MessageBox.Show("入力内容に誤りがあります。");
                    SetFocusToTopControl();
                    return;
                }

                if (!string.IsNullOrEmpty(作成締日) && 全締日集計 == true)
                {
                    this.ErrorMessage = "作成締日、全締日は同時に入力できません。";
                    MessageBox.Show("作成締日、全締日は同時に入力できません。");
                    return;
                }
                else if (string.IsNullOrEmpty(作成締日) && 全締日集計 == false)
                {
                    this.ErrorMessage = "締日を入力してください。";
                    MessageBox.Show("締日を入力してください。");
                    return;
                }

                if (string.IsNullOrEmpty(作成年) || string.IsNullOrEmpty(作成月))
                {
                    this.ErrorMessage = "作成年月は入力必須項目です。";
                    MessageBox.Show("作成年月は入力必須項目です。");
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

                if (集計区分_CValue == 0)
                {
                    //締日帳票出力用
					base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_TKS06010_Simebi, new object[] { 得意先From, 得意先To, i得意先List, 集計区分_CValue, 作成締日, 全締日集計, 作成年, 作成月, 表示順序, 作成区分_CValue, 内訳別合計, 集計期間From, 集計期間To, 集計年月 }));
                }
                else
                {
                    //月次帳票出力用
					base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_TKS06010_Getsuji, new object[] { 得意先From, 得意先To, i得意先List, 集計区分_CValue, 作成締日, 全締日集計, 作成年, 作成月, 表示順序, 作成区分_CValue, 内訳別合計, 集計期間From, 集計期間To, 集計年月 }));
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
                frmcfg.作成年 = this.作成年;
                frmcfg.作成月 = this.作成月;
                frmcfg.集計年月 = this.集計年月;
                frmcfg.集計期間From = this.集計期間From;
                frmcfg.集計期間To = this.集計期間To;
                
                ucfg.SetConfigValue(frmcfg);
            }
            #endregion

            #region 締日入力
            private void check_KeyDown(object sender, RoutedEventArgs e)
            {
                if (全締日集計 == false)
                {
                    作成締日 = "31";
                }
                else
                {
                    作成締日 = null;
                }
            }
            #endregion

            #region 全締日の判定
            private void Lost_Shimebi(object sender, RoutedEventArgs e)
            {
                if (string.IsNullOrEmpty(作成締日))
                {
                    Zensimebi.IsChecked = true;
                }
                else
                {
                    int? 変換締日 = -1;
                    if (!string.IsNullOrEmpty(作成締日))
                    {
                        変換締日 = AppCommon.IntParse(作成締日);
                        Zensimebi.IsChecked = false;
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
            #endregion

            #region 日付処理
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
                        作成月 = null;
                        SetFocusToTopControl();
                        return;
                    }
                    集計期間From = ret.DATEFrom;
                    集計期間To = ret.DATETo;
                }
                else
                {
                    //全締日選択時 例外処理
                    int? 全締日時作成月 = 0;

                    if (作成月 != null)
                    {
                        全締日時作成月 = AppCommon.IntParse(作成月);
                        if (全締日時作成月 < 1 || 全締日時作成月 > 12)
                        {
                            this.ErrorMessage = "入力値エラーです。もう一度入力してください。";
                            MessageBox.Show("入力値エラーです。もう一度入力してください。");
                            作成月 = null;
                            SetFocusToTopControl();
                            return;
                        }
                    }
                    
                    //全締日選択時
                    string Date = 作成年.ToString() + "/" + 全締日時作成月 + "/" + "01";
                    DateTime YYY, DDD;
                    YYY = Convert.ToDateTime(Date);
                    DDD = Convert.ToDateTime(Date);
                    DDD = DDD.AddMonths(+1).AddDays(-1);
                    集計期間From = YYY;
                    集計期間To = DDD;
                }

                //集計年月を作成
                Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
                int num = sjisEnc.GetByteCount(作成月);

                if (!string.IsNullOrEmpty(作成締日))
                {
                    int? p変換作成締日 = AppCommon.IntParse(作成締日);

                    //文字のバイト数が1の場合、月に0を足して表示
                    if (num == 1)
                    {
                        集計年月 = Convert.ToInt32(作成年 + "0" + 作成月);
                    }
                    else
                    {
                        集計年月 = Convert.ToInt32(作成年 + 作成月);
                    }
                }
                else
                {
                    //文字のバイト数が1の場合、月に0を足して表示
                    if (num == 1)
                    {
                        集計年月 = Convert.ToInt32(作成年 + "0" + 作成月);
                    }
                    else
                    {
                        集計年月 = Convert.ToInt32(作成年 + 作成月);
                    }
                }
            }
            #endregion

            #region 表示順序処理
            private void OnOffCheck_uriage(object sender, RoutedEventArgs e)
            {
                //if (内訳別合計 == true)
                //{
                //    HyoujiRadio.RadioViewCount = UcLabelTextRadioButton.RadioButtonCount.Two;
                //}
                //else
                //{
                //    HyoujiRadio.RadioViewCount = UcLabelTextRadioButton.RadioButtonCount.Three;
                //}
            }
            
            private void OnOffHyouji(object sender, RoutedEventArgs e)
            {
                //表示順序:0 ***true***
                if (表示順序 == 0)
                {
                    if (内訳別合計 == false)
                    {
                        Uchiwake.IsEnabled = true;
                    }
                }
                //表示順序:1 ***true***
                if (表示順序 == 1)
                {
                    if (内訳別合計 == false)
                    {
                        Uchiwake.IsEnabled = true;
                    }
                }
                //表示順序:2 ***false***
                if (表示順序 == 2)
                {
                    if (内訳別合計 == false)
                    {
                        Uchiwake.IsEnabled = false;
                    }
                }
            }
            #endregion

            #region F8
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
            #endregion
    }
}
