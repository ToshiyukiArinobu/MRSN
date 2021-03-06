﻿using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;


namespace KyoeiSystem.Application.Windows.Views
{
    using FwRepPreview = KyoeiSystem.Framework.Reports.Preview;
    using WinForms = System.Windows.Forms;

    /// <summary>
    /// 得意先別売上統計表 フォームクラス
    /// </summary>
    public partial class BSK02010 : WindowReportBase
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
        public class ConfigBSK02010 : FormConfigBase
        {
            public byte[] spConfig = null;
        }

        /// ※ 必ず public で定義する。
        public ConfigBSK02010 frmcfg = null;
        CommonConfig ccfg = null;
        // SPREAD初期状態保存用
        public byte[] spConfig = null;

        #endregion

        #region 一覧データテーブル定義

        private DataTable _searchList;
        public DataTable SearchList
        {
            get { return _searchList; }
            set
            {
                _searchList = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region << 定数定義 >>

        private const string GET_PRINT_LIST = "BSK02010_GetPrintList";
        private const string GET_CSV_LIST = "BSK02010_GetCsvList";

        /// <summary>帳票定義体ファイルパス</summary>
        private const string ReportFileName = @"Files\BSK\BSK02010.rpt";

        /// <summary>初期決算月</summary>
        private const int DEFAULT_SETTLEMENT_MONTH = 3;

        // 画面パラメータ名
        private const string PARAMS_NAME_FISCAL_YEAR = "処理年度";
        private const string PARAMS_NAME_FISCAL_FROM = "処理開始";          // No.398 Add
        private const string PARAMS_NAME_FISCAL_TO = "処理終了";            // No.398 Add
        private const string PARAMS_NAME_TOKCD_FROM = "得意先コードFROM";   // No.398 Add
        private const string PARAMS_NAME_TOKED_FROM = "得意先枝番FROM";     // No.398 Add
        private const string PARAMS_NAME_TOKCD_TO = "得意先コードTO";       // No.398 Add
        private const string PARAMS_NAME_TOKED_TO = "得意枝番先TO";         // No.398 Add
        private const string PARAMS_NAME_COMPANY  = "自社コード";

        // 帳票パラメータ名
        private const string REPORT_PARAM_NAME_PRIOD_START = "期間開始";
        private const string REPORT_PARAM_NAME_PRIOD_END = "期間終了";
        private const string REPORT_PARAM_NAME_TOK_FROM = "得意先指定From";  // No.398 Add
        private const string REPORT_PARAM_NAME_TOK_TO = "得意先指定To";      // No.398 Add
        private const string REPORT_PARAM_NAME_YEAR_MONTH01 = "集計年月１";
        private const string REPORT_PARAM_NAME_YEAR_MONTH02 = "集計年月２";
        private const string REPORT_PARAM_NAME_YEAR_MONTH03 = "集計年月３";
        private const string REPORT_PARAM_NAME_YEAR_MONTH04 = "集計年月４";
        private const string REPORT_PARAM_NAME_YEAR_MONTH05 = "集計年月５";
        private const string REPORT_PARAM_NAME_YEAR_MONTH06 = "集計年月６";
        private const string REPORT_PARAM_NAME_YEAR_MONTH07 = "集計年月７";
        private const string REPORT_PARAM_NAME_YEAR_MONTH08 = "集計年月８";
        private const string REPORT_PARAM_NAME_YEAR_MONTH09 = "集計年月９";
        private const string REPORT_PARAM_NAME_YEAR_MONTH10 = "集計年月１０";
        private const string REPORT_PARAM_NAME_YEAR_MONTH11 = "集計年月１１";
        private const string REPORT_PARAM_NAME_YEAR_MONTH12 = "集計年月１２";

        #endregion

        #region << クラス変数定義 >>

        /// <summary>検索時パラメータ保持用</summary>
        Dictionary<string, string> paramDic = new Dictionary<string, string>();

        #endregion

        #region << 列挙型定義 >>
        /// <summary>
        /// 自社販社区分 内包データ
        /// </summary>
        private enum 自社販社区分 : int
        {
            自社 = 0,
            販社 = 1
        }
        #endregion

        #region << クラス定義 >>
        /// <summary>
        /// 年度指定期間
        /// </summary>
        public class FiscalPeriod
        {
            public DateTime PeriodStart;
            public DateTime PeriodEnd;
        }
        #endregion

        #region << 画面初期処理 >>

        /// <summary>
        /// 得意先別売上統計表 コンストラクタ
        /// </summary>
        public BSK02010()
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
            frmcfg = (ConfigBSK02010)ucfg.GetConfigValue(typeof(ConfigBSK02010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigBSK02010();
                ucfg.SetConfigValue(frmcfg);
                frmcfg.spConfig = this.spConfig;
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
            }

            // 権限設定を呼び出す(ucfgを取得した後のに入れる)
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            // 登録ボタン設定
            if (!権限Get.Authority_Update_Button(ccfg, this.GetType().Name))
            {
                DataUpdateVisible = System.Windows.Visibility.Hidden;
            }

            if (frmcfg.spConfig != null)
            {
                //AppCommon.LoadSpConfig(this.sp請求データ一覧, frmcfg.spConfig);
            }

            #endregion

            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });
            base.MasterMaintenanceWindowList.Add("M01_TOK_TOKU_SCH", new List<Type> { typeof(MST02010), typeof(SCHM01_TOK) });  // No.398 Add

            ScreenClear();

            SetFocusToTopControl();

        }

        #endregion

        #region << データ受信 >>

        /// <summary>
        /// 取得データの取り込み
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            try
            {
                this.ErrorMessage = string.Empty;

                base.SetFreeForInput();

                var data = message.GetResultData();
                DataSet dataset = (data is DataSet) ? (data as DataSet) : null;
                if (dataset == null)
                {
                    this.ErrorMessage = "対象データが有りません。";
                    return;
                }

                DataTable tbl = dataset.Tables["PRINT_DATA"];
                //DataTable jistbl = dataset.Tables["M70"];
                //int i決算月 = (int)jistbl.Rows[0]["決算月"];

                switch (message.GetMessageName())
                {
                    case GET_CSV_LIST:
                        outputCsv(tbl);     // No.398 Mod
                        break;

                    case GET_PRINT_LIST:
                        PageGoukeiSet(tbl);
                        outputReport(tbl);  // No.398 Mod
                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                base.SetFreeForInput();
                this.ErrorMessage = ex.Message;
            }

        }
        /// <summary>
        /// ページ計行セット
        /// </summary>
        /// <param name="tbl"></param>
        private void PageGoukeiSet(DataTable tbl)
        {
            const int gyoucnt = 23;
            int iPre自社コード = 1;
            long 合計金額01 = 0; 
            long 前年合計金額01 = 0;
            long 合計金額02 = 0; 
            long 前年合計金額02 = 0; 
            long 合計金額03 = 0; 
            long 前年合計金額03 = 0; 
            long 合計金額04 = 0; 
            long 前年合計金額04 = 0; 
            long 合計金額05 = 0; 
            long 前年合計金額05 = 0; 
            long 合計金額06 = 0; 
            long 前年合計金額06 = 0; 
            long 合計金額07 = 0; 
            long 前年合計金額07 = 0; 
            long 合計金額08 = 0; 
            long 前年合計金額08 = 0; 
            long 合計金額09 = 0; 
            long 前年合計金額09 = 0; 
            long 合計金額10 = 0; 
            long 前年合計金額10 = 0; 
            long 合計金額11 = 0; 
            long 前年合計金額11 = 0; 
            long 合計金額12 = 0; 
            long 前年合計金額12 = 0;
            int i行Cnt = 0;
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                try
                {
                    DataRow dr = tbl.Rows[i];
                    int i自社コード = (int)dr["自社コード"];

                    if (i == 0)
                    {
                        iPre自社コード = (int)dr["自社コード"];
                    }

                    合計金額01 += (long)dr["集計売上額０１"];
                    前年合計金額01 += (long)dr["前年集計売上額０１"];
                    合計金額02 += (long)dr["集計売上額０２"];
                    前年合計金額02 += (long)dr["前年集計売上額０２"];
                    合計金額03 += (long)dr["集計売上額０３"];
                    前年合計金額03 += (long)dr["前年集計売上額０３"];
                    合計金額04 += (long)dr["集計売上額０４"];
                    前年合計金額04 += (long)dr["前年集計売上額０４"];
                    合計金額05 += (long)dr["集計売上額０５"];
                    前年合計金額05 += (long)dr["前年集計売上額０５"];
                    合計金額06 += (long)dr["集計売上額０６"];
                    前年合計金額06 += (long)dr["前年集計売上額０６"];
                    合計金額07 += (long)dr["集計売上額０７"];
                    前年合計金額07 += (long)dr["前年集計売上額０７"];
                    合計金額08 += (long)dr["集計売上額０８"];
                    前年合計金額08 += (long)dr["前年集計売上額０８"];
                    合計金額09 += (long)dr["集計売上額０９"];
                    前年合計金額09 += (long)dr["前年集計売上額０９"];
                    合計金額10 += (long)dr["集計売上額１０"];
                    前年合計金額10 += (long)dr["前年集計売上額１０"];
                    合計金額11 += (long)dr["集計売上額１１"];
                    前年合計金額11 += (long)dr["前年集計売上額１１"];
                    合計金額12 += (long)dr["集計売上額１２"];
                    前年合計金額12 += (long)dr["前年集計売上額１２"];

                    if (iPre自社コード != i自社コード || ((i行Cnt + 1) % gyoucnt) == 22 || (tbl.Rows.Count == i + 1))
                    {
                        DataRow drGoukei = tbl.NewRow();
                        drGoukei["自社コード"] = iPre自社コード;
                        drGoukei["得意先名"] = "【 頁 合 計 】";
                        drGoukei["集計売上額０１"] = 合計金額01;
                        drGoukei["前年集計売上額０１"] = 前年合計金額01;
                        drGoukei["集計売上額０２"] = 合計金額02;
                        drGoukei["前年集計売上額０２"] = 前年合計金額02;
                        drGoukei["集計売上額０３"] = 合計金額03;
                        drGoukei["前年集計売上額０３"] = 前年合計金額03;
                        drGoukei["集計売上額０４"] = 合計金額04;
                        drGoukei["前年集計売上額０４"] = 前年合計金額04;
                        drGoukei["集計売上額０５"] = 合計金額05;
                        drGoukei["前年集計売上額０５"] = 前年合計金額05;
                        drGoukei["集計売上額０６"] = 合計金額06;
                        drGoukei["前年集計売上額０６"] = 前年合計金額06;
                        drGoukei["集計売上額０７"] = 合計金額07;
                        drGoukei["前年集計売上額０７"] = 前年合計金額07;
                        drGoukei["集計売上額０８"] = 合計金額08;
                        drGoukei["前年集計売上額０８"] = 前年合計金額08;
                        drGoukei["集計売上額０９"] = 合計金額09;
                        drGoukei["前年集計売上額０９"] = 前年合計金額09;
                        drGoukei["集計売上額１０"] = 合計金額10;
                        drGoukei["前年集計売上額１０"] = 前年合計金額10;
                        drGoukei["集計売上額１１"] = 合計金額11;
                        drGoukei["前年集計売上額１１"] = 前年合計金額11;
                        drGoukei["集計売上額１２"] = 合計金額12;
                        drGoukei["前年集計売上額１２"] = 前年合計金額12;

                        合計金額01 = 0;
                        前年合計金額01 = 0;
                        合計金額02 = 0;
                        前年合計金額02 = 0;
                        合計金額03 = 0;
                        前年合計金額03 = 0;
                        合計金額04 = 0;
                        前年合計金額04 = 0;
                        合計金額05 = 0;
                        前年合計金額05 = 0;
                        合計金額06 = 0;
                        前年合計金額06 = 0;
                        合計金額07 = 0;
                        前年合計金額07 = 0;
                        合計金額08 = 0;
                        前年合計金額08 = 0;
                        合計金額09 = 0;
                        前年合計金額09 = 0;
                        合計金額10 = 0;
                        前年合計金額10 = 0;
                        合計金額11 = 0;
                        前年合計金額11 = 0;
                        合計金額12 = 0;
                        前年合計金額12 = 0;
                        tbl.Rows.InsertAt(drGoukei, i + 1);

                        i++;
                        if (iPre自社コード != i自社コード)
                        {
                            i行Cnt = 1;
                            iPre自社コード = (int)dr["自社コード"];
                            continue;
                        }
                        else
                        {
                            i行Cnt++;
                        }
                    }

                    iPre自社コード = (int)dr["自社コード"];
                    i行Cnt++;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }

        }

        /// <summary>
        /// データ受信エラー
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            base.SetFreeForInput();
            base.OnReveivedError(message);
            this.ErrorMessage = (string)message.GetResultData();
        }

        #endregion

        #region << リボン >>

        #region F01 マスタ検索
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
                var uctext = ViewBaseCommon.FindVisualParent<M01_TOK_TextBox>(ctl as UIElement);

                if (uctext != null)
                {
                    uctext.OpenSearchWindow(this);

                }
                else
                {
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                }

            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                ErrorMessage = "システムエラーです。サポートへご連絡ください。";
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
            // 入力チェック
            if (!formValidation())
                return;

            // パラメータ情報設定
            setSearchParams();

            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    GET_CSV_LIST,
                    paramDic
                ));

            base.SetBusyForInput();

        }
        #endregion

        #region F8 印刷
        /// <summary>
        /// F8　リボン　印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            // 入力チェック
            if (!formValidation())
                return;

            // パラメータ情報設定
            setSearchParams();

            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    GET_PRINT_LIST,
                    paramDic
                ));

            base.SetBusyForInput();

        }

        #endregion

        #region F11 終了
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

        #endregion

        #region << 機能処理関連 >>

        #region 画面初期化
        /// <summary>
        /// 画面初期化
        /// </summary>
        private void ScreenClear()
        {
            this.MaintenanceMode = null;

            // No.353 Mod Start
            // 自社コード
            this.MyCompany.Text1 = ccfg.自社コード.ToString();
            this.MyCompany.IsEnabled = ccfg.自社販社区分 == 自社販社区分.自社.GetHashCode();
            // No.353 Mod End

            // 処理年度の初期値設定
            // No.398 Mod Start
            this.FiscalYear.Text = string.Format("{0}/{1}", DateTime.Now.Year, DateTime.Now.Month);
            FiscalPeriod period = getFiscalFromTo(this.FiscalYear.Text);
            this.PeriodYM.Text = string.Format("月度 : {0}～{1}月度", period.PeriodStart.ToString("yyyy/MM"), period.PeriodEnd.ToString("yyyy/MM"));
            // No.398 Mod End

            ResetAllValidation();
            SetFocusToTopControl();

        }
        #endregion

        #region 年度指定期間の設定
        // No.398 Add Start
        /// <summary>
        /// 年度指定期間の設定
        /// </summary>
        /// <param name="fiscalYm">年度指定yyyy/MM</param>
        /// <returns></returns>
        public FiscalPeriod getFiscalFromTo(string fiscalYm)
        {
            if (string.IsNullOrEmpty(fiscalYm))
            {
                return null;
            }

            FiscalPeriod ret = new FiscalPeriod();
            int ival = -1;
            string[] yearMonth = fiscalYm.Split('/');
            DateTime wkDt = new DateTime(Int32.TryParse(yearMonth[0], out ival)? ival : -1,
                                         Int32.TryParse(yearMonth[1], out ival) ? ival : -1,
                                         1);
            if (wkDt == null)
            {
                return null;
            }
            // 年度指定の値から過去12か月が指定期間
            ret.PeriodStart = wkDt.AddMonths(-11);
            ret.PeriodEnd = wkDt.AddMonths(1).AddDays(-1);

            return ret;
        }
        // No.398 Add End
        #endregion

        #region 業務入力チェック
        /// <summary>
        /// 業務入力チェックをおこなう
        /// </summary>
        /// <returns></returns>
        private bool formValidation()
        {
            // key項目のエラーチェック                // No.407 Add
            if (!base.CheckKeyItemValidation())
                return false;

            // Validationチェック                   　// No.407 Mod
            if (!CheckAllValidation(true))
            {
                return false;
            }

            if (string.IsNullOrEmpty(FiscalYear.Text))
            {
                FiscalYear.Focus();
                ErrorMessage = "年度指定が入力されていません。";
                return false;
            }

            return true;

        }
        #endregion

        #region パラメータ設定
        /// <summary>
        /// パラメータを設定する
        /// </summary>
        private void setSearchParams()
        {
            paramDic.Clear();
            
            paramDic.Add(PARAMS_NAME_FISCAL_YEAR, FiscalYear.Text);
            paramDic.Add(PARAMS_NAME_COMPANY, MyCompany.Text1);

            // No.398 Add Start
            FiscalPeriod period = new FiscalPeriod();
            period = getFiscalFromTo(FiscalYear.Text);
            paramDic.Add(PARAMS_NAME_FISCAL_FROM, period.PeriodStart.ToShortDateString());
            paramDic.Add(PARAMS_NAME_FISCAL_TO, period.PeriodEnd.ToShortDateString());
            paramDic.Add(PARAMS_NAME_TOKCD_FROM, 得意先From.Text1);
            paramDic.Add(PARAMS_NAME_TOKED_FROM, 得意先From.Text2);
            paramDic.Add(PARAMS_NAME_TOKCD_TO, 得意先To.Text1);
            paramDic.Add(PARAMS_NAME_TOKED_TO, 得意先To.Text2);
            // No.398 Add End

        }
        #endregion

        #region 帳票パラメータ取得
        /// <summary>
        /// 帳票パラメータを取得する
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, DateTime> getPrintParameter()
        {
            // 期間を算出
            int mCounter = 1;
            
            DateTime targetMonth = Convert.ToDateTime(paramDic[PARAMS_NAME_FISCAL_FROM]);   // No.398 Mod
            DateTime lastMonth = Convert.ToDateTime(paramDic[PARAMS_NAME_FISCAL_TO]);       // No.398 Mod
            
            Dictionary<string, DateTime> printDic = new Dictionary<string, DateTime>();
            printDic.Add(REPORT_PARAM_NAME_PRIOD_START, targetMonth);
            printDic.Add(REPORT_PARAM_NAME_PRIOD_END, lastMonth);
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH01, targetMonth);
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH02, targetMonth.AddMonths(mCounter++));
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH03, targetMonth.AddMonths(mCounter++));
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH04, targetMonth.AddMonths(mCounter++));
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH05, targetMonth.AddMonths(mCounter++));
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH06, targetMonth.AddMonths(mCounter++));
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH07, targetMonth.AddMonths(mCounter++));
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH08, targetMonth.AddMonths(mCounter++));
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH09, targetMonth.AddMonths(mCounter++));
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH10, targetMonth.AddMonths(mCounter++));
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH11, targetMonth.AddMonths(mCounter++));
            printDic.Add(REPORT_PARAM_NAME_YEAR_MONTH12, lastMonth);

            return printDic;

        }
        #endregion

        #region ＣＳＶデータ出力
        /// <summary>
        /// ＣＳＶデータの出力をおこなう
        /// </summary>
        /// <param name="tbl"></param>
        private void outputCsv(DataTable tbl)
        {
            if (tbl == null || tbl.Rows.Count == 0)
            {
                MessageBox.Show("出力対象のデータがありません。");
                return;
            }

            // CSV出力用に列名を編集する
            changeColumnsName(tbl);     // Np.398 Mod

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
                CSVData.SaveCSV(tbl, sfd.FileName, true, true, false, ',');
                MessageBox.Show("CSVファイルの出力が完了しました。");
            }

        }
        #endregion

        #region 帳票出力
        /// <summary>
        /// 帳票の印刷処理をおこなう
        /// </summary>
        /// <param name="tbl"></param>
        private void outputReport(DataTable tbl)
        {
            PrinterDriver ret = AppCommon.GetPrinter(frmcfg.PrinterName);
            if (ret.Result == false)
            {
                this.ErrorMessage = "プリンタドライバーがインストールされていません！";
                return;
            }
            frmcfg.PrinterName = ret.PrinterName;

            if (tbl == null || tbl.Rows.Count == 0)
            {
                this.ErrorMessage = "印刷データがありません。";
                return;
            }

            try
            {
                base.SetBusyForInput();

                Dictionary<string, DateTime> printParams = getPrintParameter(); // No.398 Mod

                var parms = new List<FwRepPreview.ReportParameter>()
                {
                    #region 印字パラメータ設定
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_PRIOD_START, VALUE = printParams[REPORT_PARAM_NAME_PRIOD_START]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_PRIOD_END, VALUE = printParams[REPORT_PARAM_NAME_PRIOD_END]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_TOK_FROM, VALUE = this.得意先From.Label2Text},                       // No.398 Add
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_TOK_TO, VALUE = this.得意先To.Label2Text},                           // No.398 Add
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH01, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH01]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH02, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH02]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH03, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH03]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH04, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH04]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH05, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH05]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH06, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH06]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH07, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH07]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH08, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH08]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH09, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH09]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH10, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH10]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH11, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH11]},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YEAR_MONTH12, VALUE = printParams[REPORT_PARAM_NAME_YEAR_MONTH12]},
                    #endregion
                };

                DataTable 印刷データ = tbl.Copy();
                印刷データ.TableName = "得意先別売上統計表";

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
                appLog.Error("得意先別売上統計表の印刷時に例外が発生しました。", ex);
            }

        }
        #endregion

        #region 列名編集
        /// <summary>
        /// テーブル列名をCSV出力用に変更して返す
        /// </summary>
        /// <param name="tbl"></param>
        /// <returns></returns>
        private void changeColumnsName(DataTable tbl)
        {
            Dictionary<string, DateTime> printParams = getPrintParameter(); // No.398 Mod

            foreach (DataColumn col in tbl.Columns)
            {
                switch (col.ColumnName)
                {
                    case "集計売上額０１":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH01].ToString("yyyy年M月");
                        break;

                    case "集計売上額０２":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH02].ToString("yyyy年M月");
                        break;

                    case "集計売上額０３":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH03].ToString("yyyy年M月");
                        break;

                    case "集計売上額０４":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH04].ToString("yyyy年M月");
                        break;

                    case "集計売上額０５":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH05].ToString("yyyy年M月");
                        break;

                    case "集計売上額０６":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH06].ToString("yyyy年M月");
                        break;

                    case "集計売上額０７":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH07].ToString("yyyy年M月");
                        break;

                    case "集計売上額０８":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH08].ToString("yyyy年M月");
                        break;

                    case "集計売上額０９":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH09].ToString("yyyy年M月");
                        break;

                    case "集計売上額１０":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH10].ToString("yyyy年M月");
                        break;

                    case "集計売上額１１":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH11].ToString("yyyy年M月");
                        break;

                    case "集計売上額１２":
                        col.ColumnName = printParams[REPORT_PARAM_NAME_YEAR_MONTH12].ToString("yyyy年M月");
                        break;

                }

            }

        }
        #endregion

        #endregion

        #region Window_Closed
        /// <summary>
        /// 画面が閉じられた時、データを保持する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            SearchList = null;

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);

        }
        #endregion

        #region 年度指定が変更された時のイベント処理
        /// <summary>
        /// 年度指定が変更された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FiscalYear_cTextChanged(object sender, RoutedEventArgs e)
        {
            FiscalPeriod period = new FiscalPeriod();
            
            // 年度指定期間を再計算
            period = getFiscalFromTo(this.FiscalYear.Text);
            if (period == null)
            {
                this.PeriodYM.Text = string.Empty;
            }
            else
            {
                this.PeriodYM.Text = string.Format("月度 : {0}～{1}月度", period.PeriodStart.ToString("yyyy/MM"), period.PeriodEnd.ToString("yyyy/MM"));
            }
        }
        #endregion

    }

}
