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
    /// 入金滞留管理表(TKS10010)
    /// </summary>
    public partial class TKS10010 : WindowReportBase
    {
            #region データアクセスID
            //得意先締日集計プレビュー定数
            private const string SEARCH_TKS10010 = "SEARCH_TKS10010";
            //得意先月次集計プレビュー定数
            private const string SEARCH_TKS10010_CSV = "SEARCH_TKS10010_CSV";
            //得意先売上合計表レポート定数
            private const string rptFullPathName_PIC = @"Files\TKS\TKS10010.rpt";
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
            public class ConfigTKS10010 : FormConfigBase
            {
                public string 作成年 { get; set; }
                public string 作成月 { get; set; }
                public DateTime? 集計期間From { get; set; }
                public DateTime? 集計期間To { get; set; }
            }

            /// ※ 必ず public で定義する。
            public ConfigTKS10010 frmcfg = null;

            #endregion

            #region バインド用プロパティ

            private string _得意先ピックアップ = string.Empty;
            public string 得意先ピックアップ
            {
                get { return this._得意先ピックアップ; }
                set
                { this._得意先ピックアップ = value; NotifyPropertyChanged(); }
            }

            private string _得意先From = string.Empty;
            public string 得意先From
            {
                get { return this._得意先From; }
                set { this._得意先From = value; NotifyPropertyChanged(); }
            }

            private string _得意先To = string.Empty;
            public string 得意先To
            {
                get { return this._得意先To; }
                set { this._得意先To = value; NotifyPropertyChanged(); }
            }

            private string _作成集金日 = string.Empty;
            public string 作成集金日
            {
                get { return this._作成集金日; }
                set { this._作成集金日 = value; NotifyPropertyChanged(); }
            }

            private string _作成年 = string.Empty;
            public string 作成年
            {
                get { return this._作成年; }
                set { this._作成年 = value; NotifyPropertyChanged(); }
            }

            private string _作成月 = string.Empty;
            public string 作成月
            {
                get { return this._作成月; }
                set { this._作成月 = value; NotifyPropertyChanged(); }
            }

            private bool _全集金日集計 = true;
            public bool 全集金日集計
            {
                set { _全集金日集計 = value; NotifyPropertyChanged(); }
                get { return _全集金日集計; }
            }

            private int _作成区分 = 0;
            public int 作成区分
            {
                get { return this._作成区分; }
                set { this._作成区分 = value; NotifyPropertyChanged(); }
            }

            private int _取引区分 = 1;
            public int 取引区分
            {
                get { return this._取引区分; }
                set { this._取引区分 = value; NotifyPropertyChanged(); }
            }

            #endregion

            #region TKS10010()

            /// <summary>
            /// 入金滞留管理表
            /// </summary>
            /// 
            public TKS10010()
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
                AppCommon.SetutpComboboxList(this.作成区分_Cmb, false);

                #region 設定項目取得
                ucfg = AppCommon.GetConfig(this);
                frmcfg = (ConfigTKS10010)ucfg.GetConfigValue(typeof(ConfigTKS10010));
                if (frmcfg == null)
                {
                    frmcfg = new ConfigTKS10010();
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
                        //プレビュー出力用
                        case SEARCH_TKS10010:
                            DispPreviw(tbl);
                            break;

                        //CSV出力用
                        case SEARCH_TKS10010_CSV:
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
                    view.MakeReport("入金滞留管理表", rptFullPathName_PIC, 0, 0, 0);
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
                if (!base.CheckAllValidation())
                {
                    MessageBox.Show("入力内容に誤りがあります。");
                    SetFocusToTopControl();
                    return;
                }

                //作成区分のインデックス値取得                
                int 作成区分;
                作成区分 = 作成区分_Cmb.SelectedIndex;

                //作成年月度
                string s作成年月度 = Convert.ToString(作成年 + "年" + 作成月 + "月度");

                //締日が0 or 31を超える場合
                //if (!string.IsNullOrEmpty(作成集金日))
                //{
                //    if (AppCommon.IntParse(作成集金日) == 0 || AppCommon.IntParse(作成集金日) > 31)
                //    {
                //        this.ErrorMessage = "締日は1～31の間で入力してください。末締は月に関らず31日と入力してください。";
                //        MessageBox.Show("締日は1～31の間で入力してください。\r\n末締は月に関らず31日と入力してください。");
                //        return;
                //    }
                //}
                //else
                //{
                //    Zensimebi.IsChecked = true;
                //}

                

                if (!string.IsNullOrEmpty(作成集金日) && 全集金日集計 == true)
                {
                    this.ErrorMessage = "作成集金日、全集金日は同時に入力できません。";
                    MessageBox.Show("作成集金日、全集金日は同時に入力できません。");
                    return;
                }
                else if (string.IsNullOrEmpty(作成集金日) && 全集金日集計 == false)
                {
                    this.ErrorMessage = "作成集金日、全集金日を入力してください。";
                    MessageBox.Show("作成集金日、全集金日を入力してください。");
                    return;
                }

                //作成日付
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

				base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_TKS10010_CSV, new object[] { 得意先From, 得意先To, i得意先List, 作成集金日, 全集金日集計, 作成年, 作成月, 作成区分, s作成年月度 }));

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

                //作成区分のインデックス値取得                
                int 作成区分;
                作成区分 = 作成区分_Cmb.SelectedIndex;

                //作成年月度
                string s作成年月度 = Convert.ToString(作成年 + "年" + 作成月 + "月度");

                //締日が0 or 31を超える場合
                //if (!string.IsNullOrEmpty(作成集金日))
                //{
                //    if (AppCommon.IntParse(作成集金日) == 0 || AppCommon.IntParse(作成集金日) > 31)
                //    {
                //        this.ErrorMessage = "締日は1～31の間で入力してください。末締は月に関らず31日と入力してください。";
                //        MessageBox.Show("締日は1～31の間で入力してください。\r\n末締は月に関らず31日と入力してください。");
                //        return;
                //    }
                //}
                //else
                //{
                //    Zensimebi.IsChecked = true;
                //}

                

                if (!string.IsNullOrEmpty(作成集金日) && 全集金日集計 == true)
                {
                    this.ErrorMessage = "作成集金日、全集金日は同時に入力できません。";
                    MessageBox.Show("作成集金日、全集金日は同時に入力できません。");
                    return;
                }
                else if (string.IsNullOrEmpty(作成集金日) && 全集金日集計 == false)
                {
                    this.ErrorMessage = "作成集金日、全集金日を入力してください。";
                    MessageBox.Show("作成集金日、全集金日を入力してください。");
                    return;
                }

                //作成日付
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

				base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_TKS10010, new object[] { 得意先From, 得意先To, i得意先List, 作成集金日, 全集金日集計, 作成年, 作成月, 作成区分, s作成年月度 }));

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
                ucfg.SetConfigValue(frmcfg);
            }
            #endregion

            #region 集金日入力
            private void check_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Enter)
                {
                    if(string.IsNullOrEmpty(作成集金日))
                    {
                        if (全集金日集計 == false)
                        {
                            作成集金日 = "31";
                        }
                    }
                }
            }
            #endregion

            #region 全集金日集計の判定
            private void Lost_Shimebi(object sender, RoutedEventArgs e)
            {
            
                if (!string.IsNullOrEmpty(作成集金日))
                {
                    Zensimebi.IsChecked = false;
                }
                else
                {
                    作成集金日 = "";
                    Zensimebi.IsChecked = true;
                }

            }
            #endregion

            #region 日付処理

            private void Lost_Year(object sender, RoutedEventArgs e)
            {
                if (string.IsNullOrEmpty(作成年))
                {
                    作成年 = DateTime.Today.ToString().Substring(0, 4);
                }
            }

            private void Lost_Month(object sender, RoutedEventArgs e)
            {
                if (string.IsNullOrEmpty(作成月))
                {
                    作成月 = DateTime.Today.ToString().Substring(5, 2);
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
    }
}
