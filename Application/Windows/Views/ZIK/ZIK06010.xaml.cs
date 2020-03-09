using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows;
using System.Windows.Input;


namespace KyoeiSystem.Application.Windows.Views
{
    using System.Text;
    using FwRepPreview = KyoeiSystem.Framework.Reports.Preview;
    using WinForms = System.Windows.Forms;

    /// <summary>
    /// 在庫評価額一覧表 フォームクラス
    /// </summary>
    public partial class ZIK06010 : WindowReportBase
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
        public class ConfigZIK06010 : FormConfigBase
        {
            public byte[] spConfig = null;
        }

        /// ※ 必ず public で定義する。
        public ConfigZIK06010 frmcfg = null;
        CommonConfig ccfg = null;
        // SPREAD初期状態保存用
        public byte[] spConfig = null;

        #endregion

        #region << 定数定義 >>
        private const string GET_DATA = "ZIK06010_GetOutputData";
        private const string CHECK_EXIST = "ZIK06010_CheckExist";
        private const string UPDATE = "ZIK06010_Update";
        private const string GET_PRINT_DATA = "ZIK06010_GetPrintData";
        private const string GET_CSV = "ZIK06010_GetCsvList";

        /// <summary>帳票定義体ファイルパス</summary>
        private const string ReportFileName = @"Files\ZIK\ZIK06010.rpt";

        // 帳票パラメータ名
        private const string REPORT_PARAM_NAME_COMPANY = "会社名";
        private const string REPORT_PARAM_NAME_COMPANY_CD = "会社コード";
        private const string REPORT_PARAM_NAME_SOKO = "倉庫名";
        private const string REPORT_PARAM_NAME_SOKO_CD = "倉庫コード";
        private const string REPORT_PARAM_NAME_YM = "処理年月";
        private const string REPORT_PARAM_NAME_TARGET_STOCK = "対象在庫";

        #endregion

        #region バインディングデータ
        /// <summary>会社コード</summary>
        private string _会社コード;
        public string 会社コード
        {
            get { return _会社コード; }
            set
            {
                _会社コード = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region << クラス変数定義 >>
        /// <summary> Excel取込データリスト</summary>
        private List<ExcelInputMenber> InputList;
        /// <summary> 検索条件Dictionary</summary>
        private Dictionary<string, string> paramDic = new Dictionary<string, string>();
        
        #endregion

        #region 項目クラス定義
        /// <summary>
        /// Excel取込メンバー
        /// </summary>
        public class ExcelInputMenber
        {
            public int? 行番号 { get; set; }
            public int? 締年月 { get; set; }
            public int? 品番コード { get; set; }
            public string 自社品番 { get; set; }
            public string 自社色 { get; set; }
            public string 自社品名 { get; set; }
            public string 色名称 { get; set; }
            public DateTime 賞味期限 { get; set; }
            public int? 倉庫コード { get; set; }
            public string 倉庫名 { get; set; }
            public string 在庫数量 { get; set; }
            public string 調整数量 { get; set; }
        }
        #endregion

        #region << 画面初期処理 >>

        /// <summary>
        /// 在庫評価額一覧表 コンストラクタ
        /// </summary>
        public ZIK06010()
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
            frmcfg = (ConfigZIK06010)ucfg.GetConfigValue(typeof(ConfigZIK06010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigZIK06010();
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

            #endregion

            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });
            base.MasterMaintenanceWindowList.Add("M22_SOUK", new List<Type> { typeof(MST12020), typeof(SCHM22_SOUK) });

            // 画面初期化
            ScreenClear();

            // コントロール初期化
            InitControl();

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
                DataTable tbl = (data is DataTable) ? (data as DataTable) : null;

                switch (message.GetMessageName())
                {
                    case GET_DATA:
                        // 月次在庫データ取得
                        if (tbl == null || tbl.Rows.Count == 0)
                        {
                            MessageBox.Show("対象データが存在しません。\n月次在庫集計処理を行ってください。");
                            return;
                        }
                        // EXCEL出力
                        OutputExcel(tbl);
                        break;

                    case CHECK_EXIST:
                        // 取込データ存在チェック
                        if (tbl != null && tbl.Rows.Count != 0)
                        {
                            MessageBox.Show(GetCodeErr(tbl), "取込データ整合性エラー");
                            return;
                        }

                        // 調整在庫TBL更新
                        Update();
                        break;

                    case UPDATE:
                        // 調整在庫TBL更新
                        int? ret = (data is int) ? (data as int?) : -1;
                        if (ret != 1)
                        {
                            MessageBox.Show("調整在庫の取込に失敗しました。");
                        }
                        else
                        {
                            MessageBox.Show("調整在庫の取込が完了しました。");
                        }

                        break;
                    case GET_CSV:
                        // CSV出力
                        OutputCsv(tbl);
                        break;

                    case GET_PRINT_DATA:
                        // 帳票出力
                        OutputReport(tbl);
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

        #region F2 Excel出力
        /// <summary>
        /// F2　リボン　Excel出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF2Key(object sender, KeyEventArgs e)
        {
            // 入力チェック
            if (!CheckFormValidation())
                return;

            if (MessageBox.Show("月次集計をExcel出力します。\nよろしいですか？", "月次集計Excel出力",
                MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.Cancel) != MessageBoxResult.OK)
            {
                return;
            }

            // 月次在庫から出力データを取得する
            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    GET_DATA,
                    txt処理年月.Text,
                    txt会社.Text1,
                    txt倉庫.Text1
                ));

            base.SetBusyForInput();
        }
        #endregion

        #region F3 Excel取込
        /// <summary>
        /// F3　リボン　Excel取込
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF3Key(object sender, KeyEventArgs e)
        {
            // 入力チェック
            if (!CheckFormValidation())
                return;
            if (MessageBox.Show("調整在庫を取込ます。\nよろしいですか？", "調整在庫Excel取込",
                    MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.Cancel) != MessageBoxResult.OK)
            {
                return;
            }
            
            // Excel取込
            InputList = null;
            InputList = InputExcel();
            if (InputList == null || InputList.Count == 0)
            {
                MessageBox.Show("取込データが存在しません。");
                return;
            }

            // 取込データのチェック
            if (!CheckInputData(InputList))
            {
                return;
            }

            // データ整合性チェック
            DataTable dt = new DataTable();
            AppCommon.ConvertToDataTable(InputList, dt);
            SendRequestCheckExist(dt);
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
            if (!CheckFormValidation())
                return;

            paramDic.Clear();
            paramDic.Add("処理年月", this.txt処理年月.Text);
            paramDic.Add("会社コード", this.txt会社.Text1);
            paramDic.Add("倉庫コード", this.txt倉庫.Text1);
            paramDic.Add("対象在庫", this.rdo対象在庫.Text);

            base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        GET_CSV,
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
            if (!CheckFormValidation())
                return;

            paramDic.Clear();
            paramDic.Add("処理年月", this.txt処理年月.Text);
            paramDic.Add("会社コード", this.txt会社.Text1);
            paramDic.Add("倉庫コード", this.txt倉庫.Text1);
            paramDic.Add("対象在庫", this.rdo対象在庫.Text);

            base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        GET_PRINT_DATA,
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
            InputList = null;
            paramDic.Clear();
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
            ResetAllValidation();
            SetFocusToTopControl();

        }
        #endregion

        #region コントロール初期化
        /// <summary>
        /// コントロール初期化
        /// </summary>
        private void InitControl()
        {
            this.txt処理年月.Text = DateTime.Now.ToString("yyyy/MM");
            this.txt会社.Text1 = ccfg.自社コード.ToString();
            this.txt倉庫.Text1 = string.Empty;
            this.rdo対象在庫.Text = "0";
        }
        #endregion

        #region 業務入力チェック
        /// <summary>
        /// 業務入力チェックをおこなう
        /// </summary>
        /// <returns></returns>
        private bool CheckFormValidation()
        {
            if (string.IsNullOrEmpty(txt処理年月.Text))
            {
                txt処理年月.Focus();
                ErrorMessage = "処理年度が入力されていません。";
                return false;
            }       
            return true;
        }
        #endregion

        #region 取込データチェック
        /// <summary>
        /// 取込データチェック
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns>true:OK / false:NG</returns>
        private bool CheckInputData(List<ExcelInputMenber> dataList)
        {
            // フォーマットチェック
            if (!IsFileFormat(dataList))
            {
                return false;
            }

            // 処理年月存在チェック
            int nVal = -1;
            int nTargetYm = Int32.TryParse(this.txt処理年月.Text.Replace("/", ""), out nVal)? nVal: -1;
            var targetYm = dataList.Where(w => w.締年月 == nTargetYm);
            if (targetYm == null || !targetYm.Any())
            {
                MessageBox.Show("処理年月のデータが存在しません。");
                return false;
            }

            // 倉庫コード存在チェック
            if (!string.IsNullOrEmpty(this.txt倉庫.Text1))
            {
                int nTargetSokoCd = Int32.TryParse(this.txt倉庫.Text1, out nVal) ? nVal : -1;
                var targetSok = dataList.Where(w => w.倉庫コード == nTargetSokoCd);
                if (targetSok == null || !targetSok.Any())
                {
                    MessageBox.Show("倉庫コードが一致するデータが存在しません");
                    return false;
                }
            }

            // 重複Keyチェック
            if (!IsPrimaryKey(dataList))
            {
                return false;
            }

            return true;
        }
        #endregion

        #region フォーマットチェック
        /// <summary>
        /// フォーマットチェック
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns>true:OK / false:NG</returns>
        private bool IsFileFormat(List<ExcelInputMenber> dataList)
        {
            int nVal = -1;
            StringBuilder sb = new StringBuilder();
            DateTime wkDt;

            for (int i = 0; i < dataList.Count; i++)
            {
                // 未入力チェック
                if (dataList[i].締年月 == null ||
                    dataList[i].品番コード == null ||
                    dataList[i].自社品番 == null ||
                    dataList[i].倉庫コード == null ||
                    dataList[i].倉庫名 == null ||
                    (dataList[i].在庫数量 == null && dataList[i].調整数量 == null))
                {
                    MessageBox.Show(string.Format("{0}行目 未入力項目があります。", dataList[i].行番号));
                    return false;
                }

                // 数値チェック
                if (!Int32.TryParse(dataList[i].締年月.ToString(), out nVal))
                {
                    MessageBox.Show(string.Format("{0}行目 締年月が不正です。\n数値を設定してください。", dataList[i].行番号), "取込データフォーマットエラー");
                    return false;
                }
                
                if (dataList[i].締年月.ToString().Length < 6)
                {
                    MessageBox.Show(string.Format("{0}行目 締年月が不正です。\nyyyyMM(数値)を設定してください。", dataList[i].行番号), "取込データフォーマットエラー");
                    return false;
                }

                sb.Clear();
                sb.Append(dataList[i].締年月.ToString().Substring(0, 4)).Append("/")
                        .Append(dataList[i].締年月.ToString().Substring(4, 2)).Append("/").Append("01");
                if (!DateTime.TryParse(sb.ToString(), out wkDt))
                {
                    MessageBox.Show(string.Format("{0}行目 締年月が不正です。\nyyyyMM(数値)を設定してください。", dataList[i].行番号), "取込データフォーマットエラー");
                    return false;
                }

                if (!Int32.TryParse(dataList[i].品番コード.ToString(), out nVal))
                {
                    MessageBox.Show(string.Format("{0}行目 品番コードが不正です。\n数値を設定してください。", dataList[i].行番号), "取込データフォーマットエラー");
                    return false;
                }
                
                if (!Int32.TryParse(dataList[i].倉庫コード.ToString(), out nVal))
                {
                    MessageBox.Show(string.Format("{0}行目 倉庫コードが不正です。\n数値を設定してください。", dataList[i].行番号), "取込データフォーマットエラー");
                    return false;
                }

                if (dataList[i].在庫数量 != null &&
                    !Int32.TryParse(dataList[i].在庫数量.ToString(), out nVal))
                {
                    MessageBox.Show(string.Format("{0}行目 在庫数量が不正です。\n数値を設定してください。", dataList[i].行番号), "取込データフォーマットエラー");
                    return false;
                }

                if (dataList[i].調整数量 != null &&
                    !Int32.TryParse(dataList[i].調整数量.ToString(), out nVal))
                {
                    MessageBox.Show(string.Format("{0}行目 調整数量が不正です。\n数値を設定してください。", dataList[i].行番号), "取込データフォーマットエラー");
                    return false;
                }
                
                if (dataList[i].賞味期限 != null &&
                    !DateTime.TryParse(dataList[i].賞味期限.ToString(), out wkDt))
                {
                    MessageBox.Show(string.Format("{0}行目 賞味期限が不正です。\nyyyy/MM/dd形式を設定してください。", dataList[i].行番号), "取込データフォーマットエラー");
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 重複Keyチェック
        /// <summary>
        /// 重複Keyチェック
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns>true:OK / false:NG</returns>
        private bool IsPrimaryKey(List<ExcelInputMenber> dataList)
        {
            var record = dataList
                            .GroupBy(g => new
                            {
                                g.締年月,
                                g.倉庫コード,
                                g.品番コード,
                                g.賞味期限
                            })
                            .Select(s => new
                            {
                                締年月 = s.Key.締年月,
                                倉庫コード = s.Key.倉庫コード,
                                品番コード = s.Key.賞味期限,
                                行数 = s.Min(m => m.行番号),
                                count = s.Count()
                            });

            var errRec = record.Where(w => w.count > 1);

            if (errRec.Any())
            {
                MessageBox.Show(string.Format("{0}行目 データが重複しています。", errRec.FirstOrDefault().行数));
                return false;
            }

            return true;
        }
        #endregion

        #region データ存在チェック
        /// <summary>
        /// データ存在チェック
        /// </summary>
        /// <param name="dt"></param>
        private void SendRequestCheckExist(DataTable dt)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            // データ存在チェック
            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    CHECK_EXIST,
                    ds
                ));
        }
        #endregion

        #region コードエラー取得
        /// <summary>
        /// コードエラー取得
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>エラーメッセージ</returns>
        private string GetCodeErr(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            // 自社品番存在エラー
            var hinErr = dt.AsEnumerable()
                        .Where(w => w.Field<string>("MST自社品番") == null ||
                                    w.Field<string>("MST自社品番") != w.Field<string>("自社品番") ||
                                    w.Field<string>("MST自社色") != w.Field<string>("自社色"));

            foreach (var row in hinErr)
            {
                sb.Append(row.Field<int>("行番号")).Append("行目 品番コードと自社品番、自社色が一致しません。\n");
            }

            // 自社色存在エラー
            var iroErr = dt.AsEnumerable()
                        .Where(w => 
                                    (w.Field<string>("MST自社色") != null && w.Field<string>("色名称") == null) ||
                                    (w.Field<string>("MST自社色") != null && w.Field<string>("色名称") != w.Field<string>("MST色名称")));

            foreach (var row in iroErr)
            {
                sb.Append(row.Field<int>("行番号")).Append("行目 自社色と色名称が一致しません。\n");
            }

            // 倉庫存在エラー
            var sokoErr = dt.AsEnumerable()
                        .Where(w => w.Field<string>("MST倉庫名") == null ||
                                    w.Field<int?>("MST倉庫コード") == null ||
                                    w.Field<string>("倉庫名") != w.Field<string>("MST倉庫名"));

            foreach (var row in sokoErr)
            {
                sb.Append(row.Field<int>("行番号")).Append("行目 倉庫コードと倉庫名が一致しません。\n");
            }

            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }
        #endregion

        #region 調整在庫更新
        /// <summary>
        /// 調整在庫更新
        /// </summary>
        private void Update()
        {
            DataTable dt = new DataTable();
            AppCommon.ConvertToDataTable(InputList, dt);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            paramDic.Clear();
            paramDic.Add("処理年月", this.txt処理年月.Text);
            paramDic.Add("会社コード", this.txt会社.Text1);
            paramDic.Add("倉庫コード", this.txt倉庫.Text1);

            // 調整在庫更新
            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    UPDATE,
                    new object[]
                    {
                        paramDic,
                        ds,
                        this.ccfg.ユーザID
                    }
                ));
        }
        #endregion

        #region Excel出力
        /// <summary>
        /// Excel出力
        /// </summary>
        /// <param name="dt"></param>
        private void OutputExcel(DataTable dt)
        {
            // Excel起動
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook excelWb = ExcelApp.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet excelWs = excelWb.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlCellsTo = null;          // セル終点（中継用）
            Microsoft.Office.Interop.Excel.Range xlRangeTo = null;          // セル終点  
            Microsoft.Office.Interop.Excel.Range xlTargetRange = null;      // 出力対象レンジ

            // Excelウインドウ非表示
            ExcelApp.Visible = false;

            // EXCEL出力処理
            try
            {
                base.SetBusyForInput();

                excelWs.Select(Type.Missing);

                // 読み込む範囲を指定する
                object[,] values = new object[dt.Rows.Count + 1, dt.Columns.Count];

                // ヘッダカラム設定
                DataRow columnRow = dt.Rows[0];
                for (int i = 0; i < columnRow.Table.Columns.Count; i++)
                {
                    values[0, i] = columnRow.Table.Columns[i].Caption;
                }

                // 明細部設定
                int cnt = 1;
                foreach (DataRow row in dt.Rows)
                {

                    values[cnt, 0] = row.Field<int>("締年月").ToString();
                    values[cnt, 1] = row.Field<int>("品番コード").ToString();
                    values[cnt, 2] = row.Field<string>("自社品番");
                    values[cnt, 3] = row.Field<string>("自社色");
                    values[cnt, 4] = row.Field<string>("自社品名");
                    values[cnt, 5] = row.Field<string>("色名称");
                    values[cnt, 6] = row.Field<DateTime?>("賞味期限") == null ? null : row.Field<DateTime?>("賞味期限").Value.ToShortDateString();
                    values[cnt, 7] = row.Field<int>("倉庫コード").ToString();
                    values[cnt, 8] = row.Field<string>("倉庫名");
                    values[cnt, 9] = row.Field<int>("在庫数量").ToString();
                    values[cnt, 10] = row.Field<decimal?>("調整数量");
                    cnt++;
                }

                // 終点セル取得
                xlCellsTo = excelWs.Cells;
                xlRangeTo = xlCellsTo[dt.Rows.Count + 1, columnRow.Table.Columns.Count] as Microsoft.Office.Interop.Excel.Range;

                // 対象レンジ設定
                xlTargetRange = excelWs.Range["A1", xlRangeTo];
                // 書式を文字列に設定
                xlTargetRange.NumberFormatLocal = "@";
                xlTargetRange.Value = values;

                #region 名前を付けて保存Dialogを出力
                WinForms.SaveFileDialog sfd = new WinForms.SaveFileDialog();
                // はじめに表示されるフォルダを指定する
                sfd.InitialDirectory = @"C:\";
                // [ファイルの種類]に表示される選択肢を指定する
                sfd.Filter = "Excelファイル(*.xlsx)|*.xlsx";
                // 「CSVファイル」が選択されているようにする
                sfd.FilterIndex = 1;
                // タイトルを設定する
                sfd.Title = "保存先のファイルを選択してください";
                // ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
                sfd.RestoreDirectory = true;
                #endregion
                base.SetFreeForInput();

                if (sfd.ShowDialog() == WinForms.DialogResult.OK)
                {
                    base.SetBusyForInput();
                    ExcelApp.DisplayAlerts = false;  //　(Excelアプリ)上書きアラート false
                    
                    // EXCEL出力
                    excelWb.SaveAs(sfd.FileName);

                    base.SetFreeForInput();
                    MessageBox.Show("EXCELファイルの出力が完了しました。");
                }

                excelWb.Close(false);
                ExcelApp.Quit();
            }
            catch (Exception ex)
            {
                base.SetFreeForInput();
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //オブジェクト解放
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCellsTo);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlRangeTo);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlTargetRange);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWs);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWb);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelApp);
            }

        }
        #endregion

        #region Excel取込
        /// <summary>
        /// Excel取込
        /// </summary>
        private List<ExcelInputMenber> InputExcel()
        {
            // Excel起動
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook excelWb = ExcelApp.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet excelWs = excelWb.Sheets[1];
            string inpFileNm;
            int val = -1;
            DateTime wkDt;
            DateTime defaultDt = new DateTime(9999, 12, 31);

            try
            {
                #region ファイルを開くDialog
                // ファイルを開くDialog
                System.Windows.Forms.OpenFileDialog ofDlg = new WinForms.OpenFileDialog();
                // デフォルトのフォルダを指定する
                ofDlg.InitialDirectory = @"C:";
                // [ファイルの種類]に表示される選択肢を指定する
                ofDlg.Filter = "Excelファイル(*.xlsx)|*.xlsx";
                //ダイアログのタイトルを指定する
                ofDlg.Title = "取込ファイルを選択してください";
                //ダイアログを表示する
                if (ofDlg.ShowDialog() ==　WinForms. DialogResult.OK)
                {
                    inpFileNm = ofDlg.FileName;
                }
                else
                {
                    return null;
                }
                #endregion

                base.SetBusyForInput();

                // ブック（ファイル）を開き、１つ目のシートを選択する
                excelWb = ExcelApp.Workbooks.Open(inpFileNm);
                excelWs = excelWb.Sheets[1];
                excelWs.Select();

                // 取り込む範囲を指定する
                Microsoft.Office.Interop.Excel.Range xlInputRange = excelWs.UsedRange;

                // 指定された範囲のセルの値をオブジェクト型の配列に読み込む
                object[,] InputObject = (System.Object[,])xlInputRange.Value2;

                int rowCnt = xlInputRange.Rows.Count;

                // クローズ
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlInputRange);
                excelWb.Close();
                ExcelApp.Quit();

                // Listに変換
                List<ExcelInputMenber> inpList = new List<ExcelInputMenber>();
                for (int i = 2; i <= rowCnt; i++)
                {
                    ExcelInputMenber men = new ExcelInputMenber();
                    men.行番号 = i;
                    men.締年月 = InputObject[i, 1] != null ?
                                    Int32.TryParse(InputObject[i, 1].ToString(), out val) ? val : (int?)null :
                                 (int?)null;
                    men.品番コード = InputObject[i, 2] != null ?
                                        Int32.TryParse(InputObject[i, 2].ToString(), out val) ? val : (int?)null :
                                     (int?)null;
                    men.自社品番 = InputObject[i, 3] != null ? InputObject[i, 3].ToString() : null;
                    men.自社色 = InputObject[i, 4] != null ? InputObject[i, 4].ToString() : null;
                    men.自社品名 = InputObject[i, 5] != null ? InputObject[i, 5].ToString() : null;
                    men.色名称 = InputObject[i, 6] != null ? InputObject[i, 6].ToString() : null;
                    men.賞味期限 = InputObject[i, 7] != null ?
                                        DateTime.TryParse(InputObject[i, 7].ToString(), out wkDt) ? wkDt : defaultDt :
                                   defaultDt;
                    men.倉庫コード = InputObject[i, 8] != null ?
                                        Int32.TryParse(InputObject[i, 8].ToString(), out val) ? val : (int?)null :
                                     (int?)null;
                    men.倉庫名 = InputObject[i, 9] != null ? InputObject[i, 9].ToString() : null;
                    men.在庫数量 = InputObject[i, 10] != null ? InputObject[i, 10].ToString() : null;
                    men.調整数量 = InputObject[i, 11] != null ? InputObject[i, 11].ToString() : null;

                    inpList.Add(men);
                }

                return inpList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excelファイルの取込に失敗しました。");
                throw ex;
            }
            finally
            {
                base.SetFreeForInput();
                //オブジェクト解放
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWs);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWb);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelApp);
            }
        }
        #endregion

        #region ＣＳＶデータ出力
        /// <summary>
        /// ＣＳＶデータの出力をおこなう
        /// </summary>
        /// <param name="tbl"></param>
        private void OutputCsv(DataTable tbl)
        {
            if (tbl == null || tbl.Rows.Count == 0)
            {
                MessageBox.Show("出力対象のデータがありません。");
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
        private void OutputReport(DataTable tbl)
        {
            string reportFileName;

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

                var parms = new List<FwRepPreview.ReportParameter>()
                {
                    // 印字パラメータ設定
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_YM, VALUE = this.txt処理年月.Text},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_COMPANY_CD, VALUE = this.txt会社.Text1},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_COMPANY, VALUE = this.txt会社.Text2},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_SOKO_CD, VALUE = string.IsNullOrEmpty(this.txt倉庫.Text1)? "": this.txt倉庫.Text1},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_SOKO, VALUE = string.IsNullOrEmpty(this.txt倉庫.Text2)? "": this.txt倉庫.Text2},
                    new FwRepPreview.ReportParameter(){ PNAME = REPORT_PARAM_NAME_TARGET_STOCK, VALUE = this.rdo対象在庫.Text == "0"? "月次在庫":"調整在庫"},
                };

                reportFileName = ReportFileName;
                DataTable 印刷データ = tbl.Copy();
                印刷データ.TableName = "在庫評価額一覧";

                FwRepPreview.ReportPreview view = new FwRepPreview.ReportPreview();
                view.MakeReport(印刷データ.TableName, reportFileName, 0, 0, 0);
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
                appLog.Error("在庫評価額一覧表の印刷時に例外が発生しました。", ex);
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
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);

            InputList = null;
            paramDic.Clear();

        }
        #endregion

        #region << コントロールイベント処理 >>
        /// <summary>
        /// 会社コード変更イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt会社_cText1Changed(object sender, RoutedEventArgs e)
        {
            // 会社コードが変更された場合、クリア
            this.txt倉庫.Text1 = string.Empty;
        }
        #endregion
    }

}
