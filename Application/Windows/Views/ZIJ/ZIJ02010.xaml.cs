﻿using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace KyoeiSystem.Application.Windows.Views
{
    using FwRepPreview = KyoeiSystem.Framework.Reports.Preview;
    using WinForms = System.Windows.Forms;
    using GrapeCity.Windows.SpreadGrid;

    /// <summary>
    /// 仕入明細問合せ フォームクラス
    /// </summary>
    public partial class ZIJ02010 : RibbonWindowViewBase
    {
        #region << 列挙型定義 >>

        /// <summary>
        /// データグリッドの列定義
        /// </summary>
        private enum GridColumnsMapping : int
        {
            仕入日 = 0,
            仕入区分 = 1,
            入力区分 = 2,
            伝票番号 = 3,
            元伝票番号 = 4,
            仕入先名 = 5,
            品番コード = 6,
            自社品番 = 7,
            自社品名 = 8,
            自社色 = 9,
            賞味期限 = 10,
            数量 = 11,
            単位 = 12,
            単価 = 13,
            金額 = 14,
            入荷先名 = 15,      // No.396 Add
            摘要 = 16,
            発注番号 = 17
        }

        /// <summary>
        /// 自社販社区分 内包データ
        /// </summary>
        private enum 自社販社区分 : int
        {
            自社 = 0,
            販社 = 1
        }

        public enum 入力区分 : int
        {
            仕入入力 = 0,
            売上入力 = 1
        }

        public enum 仕入区分 : int
        {
            通常 = 0,
            返品 = 90
        }

        #endregion

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;
        CommonConfig ccfg = null;

        /// <summary>
        /// 画面固有設定項目のクラス定義
        /// ※ 必ず public で定義する。
        /// </summary>
        public class ConfigZIJ02010 : FormConfigBase
        {
            public byte[] spConfig20180118 = null;

        }

        /// ※ 必ず public で定義する。
        public ConfigZIJ02010 frmcfg = null;

        public byte[] sp_Config = null;

        #endregion

        #region << 定数定義 >>

        private const string ZIJ02010_GetDataList = "ZIJ02010_GetData";
        private const string ReportFileName = @"Files\ZIJ\ZIJ02010.rpt";

        /// <summary>金額フォーマット定義</summary>
        private const string PRICE_FORMAT_STRING = "{0:#,0}";        // No.396 Add
        #endregion

        #region << 変数定義 >>

        private Dictionary<string, string> paramDic = new Dictionary<string, string>();

        #endregion

        #region バインディングプロパティ

        private DataTable _searchResult;
        public DataTable SearchResult
        {
            get { return _searchResult; }
            set
            {
                _searchResult = value;
                NotifyPropertyChanged();
            }

        }

        #endregion

        #region<< 支払明細問合せ 初期処理群 >>

        /// <summary>
        /// 支払明細問合せ コンストラクタ
        /// </summary>
        public ZIJ02010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// 画面読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 初期状態を保存（SPREADリセット時にのみ使用する）
            this.sp_Config = AppCommon.SaveSpConfig(this.spGridList);

            base.MasterMaintenanceWindowList.Add("M01_TOK_TOKU_SCH", new List<Type> { typeof(MST02010), typeof(SCHM01_TOK) });
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });
            base.MasterMaintenanceWindowList.Add("M72_TNT", new List<Type> { typeof(MST23010), typeof(SCHM72_TNT) });
            base.MasterMaintenanceWindowList.Add("M09_MYHIN", new List<Type> { typeof(MST02010), typeof(SCHM09_MYHIN) });    // No.396 Add

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            frmcfg = (ConfigZIJ02010)ucfg.GetConfigValue(typeof(ConfigZIJ02010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigZIJ02010();
                ucfg.SetConfigValue(frmcfg);
                frmcfg.spConfig20180118 = this.sp_Config;
            }
            else
            {
                // 表示できるかチェック
                var WidthCHK = WinForms.Screen.PrimaryScreen.Bounds.Width - frmcfg.Left;
                if (WidthCHK > 10)
                {
                    this.Left = frmcfg.Left;
                }
                // 表示できるかチェック
                var HeightCHK = WinForms.Screen.PrimaryScreen.Bounds.Height - frmcfg.Top;
                if (HeightCHK > 10)
                {
                    this.Top = frmcfg.Top;
                }
                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;
            }
            #endregion

            spGridList.InputBindings.Add(new KeyBinding(spGridList.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));

            // コントロールの初期設定をおこなう
            initSearchControl();

            spGridList.RowCount = 0;

            SetFocusToTopControl();
            ErrorMessage = "";

        }

        #endregion

        #region << 送信データ受信 >>

        /// <summary>
        /// 取得データの取り込み
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            var data = message.GetResultData();
            DataTable tbl = (data is DataTable) ? (data as DataTable) : null;

            switch (message.GetMessageName())
            {
                case ZIJ02010_GetDataList:
                    base.SetFreeForInput();

                    if (tbl == null)
                    {
                        DetailClear();
                    }
                    else
                    {
                        if (tbl.Rows.Count == 0)
                        {
                            this.ErrorMessage = "該当するデータはありません。";
                            DetailClear();
                            return;
                        }
                        else
                        {
                            SearchResult = tbl;
                            // No.383 Add Start
                            // フォーカスをSPREADへ
                            spGridList.Focus();
                            spGridList.Focusable = true;
                            spGridList.ActiveCellPosition = new CellPosition(0, (int)GridColumnsMapping.仕入日);
                            spGridList.ShowCell(0, (int)GridColumnsMapping.仕入日);
                            // No.383 Add End

                            summaryCalc();       // No.396 Mod
                        }

                    }
                    break;

            }

        }

        /// <summary>
        /// 受信エラー時の処理をおこなう
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            this.Message = base.ErrorMessage;
            base.SetFreeForInput();
        }

        #endregion

        #region << リボン >>

        #region F1 マスタ検索
        /// <summary>
        /// F1　リボン　マスタ検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            try
            {
                var ctl = FocusManager.GetFocusedElement(this);
                var m01Text = ViewBaseCommon.FindVisualParent<M01_TOK_TextBox>(ctl as UIElement);

                if (m01Text == null)
                {
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                }
                else
                {
                    m01Text.OpenSearchWindow(this);

                }

            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";

            }

        }
        #endregion

        #region F5 CSV出力
        /// <summary>
        /// F5　リボン　CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            if (this.SearchResult == null || this.SearchResult.Rows.Count == 0)
            {
                MessageBox.Show("検索データがありません。");
                return;
            }

            WinForms.SaveFileDialog sfd = new WinForms.SaveFileDialog();
            // はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = @"C:\";
            // [ファイルの種類]に表示される選択肢を指定する
            sfd.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
            // 「CSVファイル」が選択されているようにする
            sfd.FilterIndex = 1;
            // タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください";
            // ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == WinForms.DialogResult.OK)
            {
                // CSVファイル出力
                CSVData.SaveCSV(SearchResult, sfd.FileName, true, true, false, ',');
                MessageBox.Show("CSVファイルの出力が完了しました。");
            }

        }
        #endregion

        /// <summary>
        /// F8　リボン　印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            PrintOut();

        }

        /// <summary>
        /// F11　リボン　終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 一覧検索処理

        /// <summary>
        /// 検索ボタンが押下された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!base.CheckAllValidation())
                {
                    MessageBox.Show("入力内容に誤りがあります。");
                    this.ErrorMessage = "入力内容に誤りがあります。";
                    return;
                }

                int iCompany;
                if (!int.TryParse(this.myCompany.Text1, out iCompany))
                {
                    this.myCompany.Focus();
                    this.myCompany.Focusable = true;
                    base.ErrorMessage = "自社コードの入力に誤りがあります。";
                    return;
                }

                setSearchParams();

                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        ZIJ02010_GetDataList,
                        new object[]
                        {
                            iCompany,
                            paramDic
                        }));

                base.SetBusyForInput();

            }
            catch
            {
                throw;
            }

        }

        #endregion

        #region << KeyDown Events >>

        /// <summary>
        /// コントロールでキーが押下された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastField_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var ctl = sender as Framework.Windows.Controls.UcLabelTwinTextBox;
                if (ctl == null)
                {
                    return;
                }
                e.Handled = true;
                bool chk = ctl.CheckValidation();
                if (chk == true)
                {
                    Keyboard.Focus(this.btnSearch);
                }
                else
                {
                    ctl.Focus();
                    this.ErrorMessage = ctl.GetValidationMessage();
                }
            }
        }

        /// <summary>
        /// スプレッドグリッドでキーが押下された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spGridList_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && spGridList.EditElement == null)
            {
                e.Handled = true;
            }
            else if (e.Key == Key.V && (((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) != KeyStates.Down) || ((Keyboard.GetKeyStates(Key.RightCtrl) & KeyStates.Down) != KeyStates.Down)))
            {
                e.Handled = true;
            }

        }

        #endregion

        #region Window_Closed

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            this.spGridList.InputBindings.Clear();
            this.SearchResult = null;

            if (ucfg != null)
            {
                if (frmcfg == null) { frmcfg = new ConfigZIJ02010(); }
                frmcfg.Top = this.Top;
                frmcfg.Left = this.Left;
                frmcfg.Width = this.Width;
                frmcfg.Height = this.Height;
                frmcfg.spConfig20180118 = AppCommon.SaveSpConfig(this.spGridList);

                ucfg.SetConfigValue(frmcfg);
            }

        }

        #endregion


        #region << 機能処理群 >>

        #region 明細初期化
        /// <summary>
        /// 明細初期化
        /// </summary>
        private void DetailClear()
        {
            // 明細初期化
            this.SearchResult = null;

            // 仕入合計(通常)
            sumSrTotalNomal.Text = string.Empty;
            // 仕入消費税(通常)
            sumSrTaxNomal.Text = string.Empty;
            // 仕入合計(軽減)
            sumReducedTotal.Text = string.Empty;
            // 仕入消費税(軽減)
            sumReducedTax.Text = string.Empty;
            //仕入合計(非課税)
            sumHikazei.Text = string.Empty;
            // 総合計
            sumTotal.Text = string.Empty;

        }
        #endregion

        #region 検索条件部の初期設定

        /// <summary>
        /// 検索条件部の初期設定をおこなう
        /// </summary>
        private void initSearchControl()
        {
            // 自社コード
            this.myCompany.Text1 = ccfg.自社コード.ToString();
            this.myCompany.IsEnabled = ccfg.自社販社区分 == 自社販社区分.自社.GetHashCode();

            // 仕入日(範囲指定)
            DateTime nowDate = DateTime.Today;
            this.purchaseDatePriod.Text1 = String.Format("{0:yyyy/MM}/01", nowDate);
            this.purchaseDatePriod.Text2 = String.Format("{0:yyyy/MM}/{1}", nowDate, DateTime.DaysInMonth(nowDate.Year, nowDate.Month));

            // 入力区分
            AppCommon.SetutpComboboxList(this.cmbInputType, false);
            this.cmbInputType.SelectedIndex = 0;

            // 仕入先
            if (ccfg.自社販社区分 == 自社販社区分.販社.GetHashCode())
            {
                // 仕入先の指定を不可とする
                // REMARKS:販社の場合、基本的にマルセンからの仕入となる為指定する意味がない
                this.suppliers.IsEnabled = false;
                this.suppliers.Text1 = string.Empty;
                this.suppliers.Text2 = string.Empty;

            }

        }

        #endregion

        #region 検索パラメータの設定
        /// <summary>
        /// 検索パラメータを設定する
        /// </summary>
        private void setSearchParams()
        {
            #region 金種の名称辞書を作成
            Dictionary<int, string> inputTypeDic = new Dictionary<int, string>()
                {
                    { -1, "指定なし" },
                    { 入力区分.仕入入力.GetHashCode(), "仕入入力" },
                    { 入力区分.売上入力.GetHashCode(), "売上入力" }
                };
            #endregion

            paramDic.Clear();
            paramDic.Add("仕入日From", purchaseDatePriod.Text1);
            paramDic.Add("仕入日To", purchaseDatePriod.Text2);
            paramDic.Add("入金日From", depositDatePriod.Text1);
            paramDic.Add("入金日To", depositDatePriod.Text2);
            paramDic.Add("入力区分", cmbInputType.SelectedValue.ToString());
            paramDic.Add("仕入先コード", suppliers.Text1);
            paramDic.Add("仕入先枝番", suppliers.Text2);
            paramDic.Add("入荷先コード", arrivalCompany.Text1);
            paramDic.Add("自社品番", Hinban.Text1);    // No.396 Add

            paramDic.Add("自社名", myCompany.Text2);
            paramDic.Add("入力区分名", inputTypeDic[int.Parse(cmbInputType.SelectedValue.ToString())]);
            paramDic.Add("仕入先名", suppliers.Label2Text);
            paramDic.Add("入荷先名", arrivalCompany.Text2);

        }
        #endregion

        #region 合計額の設定
        /// <summary>
        /// 各金額の合計を算出して設定する
        /// </summary>
        private void summaryCalc()
        {
            // No.396 Mod Start

            long 仕入合計通常 = 0;
            long 仕入合計軽減 = 0;
            long 仕入合計非課税 = 0;
            int 通常消費税 = 0;
            int 軽減消費税 = 0;

            仕入合計通常 = SearchResult.AsEnumerable().Where(x => x.Field<int?>("仕入区分コード") == (int)仕入区分.通常).Select(c => c.Field<int>("通常税率対象金額")).Sum();

            仕入合計軽減 = SearchResult.AsEnumerable().Where(x => x.Field<int?>("仕入区分コード") == (int)仕入区分.通常).Select(c => c.Field<int>("軽減税率対象金額")).Sum();
            仕入合計非課税 = SearchResult.AsEnumerable().Where(x => x.Field<int?>("仕入区分コード") == (int)仕入区分.通常).Select(c => c.Field<int>("非課税対象金額")).Sum(); 

            if (string.IsNullOrEmpty(Hinban.Text1))
            {
                // 明細にヘッダーの消費税を持たせているので、1伝票1件のみ取得
                通常消費税 = SearchResult.AsEnumerable().Where(x => x.Field<int?>("仕入区分コード") == (int)仕入区分.通常)
                                         .GroupBy(a => a.Field<int>("伝票番号")).Select(c => c.FirstOrDefault().Field<int>("通常税率消費税")).Sum();

                軽減消費税 = SearchResult.AsEnumerable().Where(x => x.Field<int?>("仕入区分コード") == (int)仕入区分.通常)
                                         .GroupBy(a => a.Field<int>("伝票番号")).Select(c => c.FirstOrDefault().Field<int>("軽減税率消費税")).Sum();
            }

            // 仕入合計(通常)
            sumSrTotalNomal.Text = string.Format(PRICE_FORMAT_STRING, 仕入合計通常);
            // 仕入消費税(通常)
            sumSrTaxNomal.Text = string.Format(PRICE_FORMAT_STRING, 通常消費税);
            // 仕入合計(軽減)
            sumReducedTotal.Text = string.Format(PRICE_FORMAT_STRING, 仕入合計軽減);
            // 仕入消費税(軽減)
            sumReducedTax.Text = string.Format(PRICE_FORMAT_STRING, 軽減消費税);
            // 仕入合計(非課税)
            sumHikazei.Text = string.Format(PRICE_FORMAT_STRING, 仕入合計非課税);
            // 総合計
            sumTotal.Text = string.Format(PRICE_FORMAT_STRING, (仕入合計通常 + 通常消費税 + 仕入合計軽減 + 軽減消費税 + 仕入合計非課税));


            // No.396 Mod End

        }
        #endregion

        /* 
        // No.396 Del Start
        #region 各金額の合計値計算
        /// <summary>
        /// 指定金額の合計値を取得して返す
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private string parseLongSummary(int code)
        {
            string sVal = string.Empty;
            long lVal;
            int iVal;

            switch (code)
            {
                case 0:     // 総合計
                    long stotal = long.TryParse(SearchResult.Compute("Sum(合計金額)", "").ToString(), out lVal) ? lVal : 0;
                    int stax = int.TryParse(SearchResult.Compute("Sum(消費税)", "").ToString(), out iVal) ? iVal : 0;
                    long rtotal = long.TryParse(SearchResult.Compute("Sum(返品合計金額)", "").ToString(), out lVal) ? lVal : 0;
                    int rtax = int.TryParse(SearchResult.Compute("Sum(返品消費税)", "").ToString(), out iVal) ? iVal : 0;

                    sVal = ((stotal + stax) + (rtotal + rtax)).ToString();
                    break;

                case 1:     // 仕入合計
                    sVal = (long.TryParse(SearchResult.Compute("Sum(合計金額)", "").ToString(), out lVal) ? lVal : 0).ToString();
                    break;

                case 2:     // 仕入消費税
                    sVal = (int.TryParse(SearchResult.Compute("Sum(消費税)", "").ToString(), out iVal) ? iVal : 0).ToString();
                    break;

                case -1:    // 返品合計
                    sVal = (long.TryParse(SearchResult.Compute("Sum(返品合計金額)", "").ToString(), out lVal) ? lVal : 0).ToString();
                    break;

                case -2:    // 返品消費税
                    sVal = (int.TryParse(SearchResult.Compute("Sum(返品消費税)", "").ToString(), out iVal) ? iVal : 0).ToString();
                    break;

                default:
                    break;

            }

            if (string.IsNullOrEmpty(sVal))
                return "0";

            return string.Format("{0:#,0}", long.Parse(sVal));

        }
        #endregion
        // No.396 Del End
        */

        #region 帳票印刷処理
        /// <summary>
        /// 帳票印刷処理
        /// </summary>
        private void PrintOut()
        {
            PrinterDriver ret = AppCommon.GetPrinter(frmcfg.PrinterName);
            if (ret.Result == false)
            {
                this.ErrorMessage = "プリンタドライバーがインストールされていません！";
                return;
            }
            frmcfg.PrinterName = ret.PrinterName;

            if (this.SearchResult == null || this.SearchResult.Rows.Count == 0)
            {
                this.ErrorMessage = "印刷データがありません。";
                return;
            }

            try
            {
                base.SetBusyForInput();
                var parms = new List<FwRepPreview.ReportParameter>()
                {
                    new FwRepPreview.ReportParameter(){ PNAME = "自社名", VALUE = paramDic["自社名"] },
                    new FwRepPreview.ReportParameter(){ PNAME = "仕入日From", VALUE = string.IsNullOrEmpty(paramDic["仕入日From"]) ? "" : paramDic["仕入日From"] },
                    new FwRepPreview.ReportParameter(){ PNAME = "仕入日To", VALUE = string.IsNullOrEmpty(paramDic["仕入日To"]) ? "" : paramDic["仕入日To"] },
                    new FwRepPreview.ReportParameter(){ PNAME = "入金日From", VALUE = string.IsNullOrEmpty(paramDic["入金日From"]) ? "" : paramDic["入金日From"] },
                    new FwRepPreview.ReportParameter(){ PNAME = "入金日To", VALUE = string.IsNullOrEmpty(paramDic["入金日To"]) ? "" : paramDic["入金日To"] },
                    new FwRepPreview.ReportParameter(){ PNAME = "入力区分", VALUE = paramDic["入力区分名"] },
                    new FwRepPreview.ReportParameter(){ PNAME = "仕入先名", VALUE = string.IsNullOrEmpty(paramDic["仕入先名"]) ? "" : paramDic["仕入先名"] },
                    new FwRepPreview.ReportParameter(){ PNAME = "入荷先名", VALUE = string.IsNullOrEmpty(paramDic["入荷先名"]) ? "" : paramDic["入荷先名"] },
                     new FwRepPreview.ReportParameter(){ PNAME = "自社品番", VALUE = string.IsNullOrEmpty(paramDic["自社品番"]) ? "" : paramDic["自社品番"] },     // No.396 Add
                };

                DataTable 印刷データ = SearchResult.Copy();
                印刷データ.TableName = "仕入明細問合せ";

                FwRepPreview.ReportPreview view = new FwRepPreview.ReportPreview();
                view.MakeReport(印刷データ.TableName, ReportFileName, 0, 0, 0);
                view.SetReportData(印刷データ);

                view.SetupParmeters(parms);

                base.SetFreeForInput();

                view.PrinterName = frmcfg.PrinterName;
                view.ShowPreview();
                view.Close();
                frmcfg.PrinterName = view.PrinterName;

            }
            catch (Exception ex)
            {
                base.SetFreeForInput();
                this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
                appLog.Error("支払明細問合せ一覧表の印刷時に例外が発生しました。", ex);

            }

        }
        #endregion

        #endregion

    }

}
