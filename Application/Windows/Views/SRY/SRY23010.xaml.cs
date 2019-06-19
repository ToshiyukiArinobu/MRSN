using System;
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
using KyoeiSystem.Framework.Windows.Controls;

using Excel = Microsoft.Office.Interop.Excel;


namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 輸送実績報告書
    /// </summary>
    public partial class SRY23010 : WindowReportBase
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
        public class ConfigSRY23010 : FormConfigBase
        {
            public string 開始作成年 { get; set; }
            public string 開始作成月 { get; set; }
            public string 終了作成年 { get; set; }
            public string 終了作成月 { get; set; }
            public int 自社ID { get; set; }
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
        public ConfigSRY23010 frmcfg = null;

        #endregion

        #region 定数定義

        // 付表1取得
        private const string SEARCH_SRY23010 = "SEARCH_SRY23010";
        // 付表2取得
        private const string SEARCH_SRY23020 = "SEARCH_SRY23020";
        // Excel出力1
        private const string SEARCH_SRY23010_EXCEL = "SEARCH_SRY23010_EXCEL";
        // Excel出力2
        private const string SEARCH_SRY23020_EXCEL = "SEARCH_SRY23020_EXCEL";
        // PreviewKeyDown
        private const string SEARCH_SRY23010_GDATE = "SEARCH_SRY23010_GDATE";

        #endregion

        #region バインド用プロパティ

        // データバインド用変数
        private string _作成年 = string.Empty;
        public string 作成年
        {
            set { _作成年 = value; NotifyPropertyChanged(); }
            get { return _作成年; }
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

        private int? _部門コード = null;
        public int? 部門コード
        {
            set { _部門コード = value; NotifyPropertyChanged(); }
            get { return _部門コード; }
        }

        public int? 印刷Flg = 0;

        #endregion

        #region SRY23010

        /// <summary>
        /// 輸送実績報告書
        /// </summary>
        public SRY23010()
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
            //F1 自社部門ID
            base.MasterMaintenanceWindowList.Add("M71_BUM", new List<Type> { null, typeof(SCH10010) });

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigSRY23010)ucfg.GetConfigValue(typeof(ConfigSRY23010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigSRY23010();
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

            }
            #endregion

			spFuhyo1.InputBindings.Add(new KeyBinding(spFuhyo1.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));
			spFuhyo2.InputBindings.Add(new KeyBinding(spFuhyo2.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));

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

            // 個別にエラー処理が必要な場合、ここに記述してください。

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
                    // 付表1取得
                    case SEARCH_SRY23010:
                        this.spFuhyo1.ItemsSource = tbl.DefaultView;
                        break;

                    // 付表2取得
                    case SEARCH_SRY23020:
                        this.spFuhyo2.ItemsSource = tbl.DefaultView;
                        印刷Flg = 1;
                        break;

                    // Excel出力
                    case SEARCH_SRY23010_EXCEL:
                        if (Type.GetTypeFromProgID("Excel.Application") != null)
                        {
                            Excel(tbl);

                        }
                        else
                        {
                            MessageBox.Show(this, "Excelがインストールされていません。\n\r出力処理を中断します。");
                        }
                        break;

                    case SEARCH_SRY23020_EXCEL:
                        if (Type.GetTypeFromProgID("Excel.Application") != null)
                        {
                            Excel2(tbl);
                            MessageBox.Show("データを出力しました");
                        }
                        else
                        {
                            MessageBox.Show(this, "Excelがインストールされていません。\n\r出力処理を中断します。");
                        }
                        break;

                    case SEARCH_SRY23010_GDATE:
                        if (tbl != null)
                        {
                            //M87_CTRLからG期首・期末月日を取得
                            string sG期首月日 = tbl.Rows[0]["G期首月日"].ToString() == string.Empty ? "0" : tbl.Rows[0]["G期首月日"].ToString();
                            string sG期末月日 = tbl.Rows[0]["G期末月日"].ToString() == string.Empty ? "0" : tbl.Rows[0]["G期末月日"].ToString();
                            DateTime date;

                            //M87_CNTLのG期首月日のデータをセット
                            switch (sG期首月日.Length)
                            {
                                case 0:
                                    sG期首月日 = "01/01";
                                    break;
                                case 1:
                                    sG期首月日 = "01/01";
                                    break;
                                case 2:
                                    sG期首月日 = "01/" + sG期首月日;
                                    break;
                                case 3:
                                    sG期首月日 = "0" + sG期首月日.Substring(0, 1) + "/" + sG期首月日.Substring(1, 2);
                                    break;
                                case 4:
                                    sG期首月日 = sG期首月日.Substring(0, 2) + "/" + sG期首月日.Substring(2, 2);
                                    break;
                            }

                            //DATETIMEに変換できるかチェック
                            if (DateTime.TryParse(作成年 + "/" + sG期首月日, out date))
                            {
                                集計期間From = date;
                            }
                            else
                            {
                                集計期間From = Convert.ToDateTime(作成年 + "/01/01");
                            }

                            //M87_CNTLのG期末月日のデータをセット
                            switch (sG期末月日.Length)
                            {
                                case 0:
                                    sG期末月日 = "12/31";
                                    break;
                                case 1:
                                    sG期末月日 = "12/31";
                                    break;
                                case 2:
                                    sG期末月日 = "12/" + sG期末月日;
                                    break;
                                case 3:
                                    sG期末月日 = "0" + sG期末月日.Substring(0, 1) + "/" + sG期末月日.Substring(1, 2);
                                    break;
                                case 4:
                                    sG期末月日 = sG期末月日.Substring(0, 2) + "/" + sG期末月日.Substring(2, 2);
                                    break;
                            }

                            //DATETIMEに変換できるかチェック
                            if (DateTime.TryParse(作成年 + "/" + sG期末月日, out date))
                            {
                                集計期間To = date;
                            }
                            else
                            {
                                集計期間To = Convert.ToDateTime(作成年 + "/12/31");
                            }

                            //【集計期間From】の方が大きかった場合【集計期間To】のYearを+1する
                            if (集計期間From > 集計期間To)
                            {
                                集計期間To = 集計期間To.Value.AddYears(+1);
                            }
                        }
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
        /// F1 リボン　検索
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
                    SCH06010 srch = new SCH06010();
                    switch (uctext.DataAccessName)
                    {
                        case "M05_CAR":
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
        /// F5 Excelファイル出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            if (印刷Flg == 0)
            {
                this.ErrorMessage = "Excelへ出力するデータが見つかりませんでした。";
                MessageBox.Show("Excelへ出力するデータが見つかりませんでした。");
                return;
            }
			base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, SEARCH_SRY23010_EXCEL, new object[] { 集計期間From, 集計期間To, 部門コード }));
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

        #region Excel出力

        public void Excel(DataTable tbl)
        {
            //Excelのパス
            string fileName = @"C:/GREEN/グリーン経営付表.xls";
            Excel.Application xlApp = new Excel.Application();
            //Excelが開かないようにする
            xlApp.Visible = false;
            //指定したパスのExcelを起動
            Excel.Workbook wb = xlApp.Workbooks.Open(Filename: fileName);
            try
            {
                //Sheetの番号か名称で設定できる

                //((Excel.Worksheet)wb.Sheets["5"]).Select();
                ((Excel.Worksheet)wb.Sheets["表１"]).Select();
            }
            catch (Exception ex)
            {
                //Sheetがなかった場合のエラー処理
                //Appを閉じる
                wb.Close(false);
                xlApp.Quit();
                //Errorメッセージ
                MessageBox.Show("指定したSheetは存在しません。\n\r出力処理を中断します。");
                //アプリケーションを終了
                return;
            }
            //変数宣言
            int row = 9;
            int cal1 = 10;
            int cal2 = 12;
            int cal3 = 17;
            int cnt = 0;
            string p種別 = string.Empty;
            int p事業用区分 = 0;
            int pディーゼル区分 = 0;
            int TableCnt = 0;
            TableCnt = tbl.Rows.Count;
            Excel.Range CellRange;
            string[] 種別Array1 = new string[]{ "最大積載量１ｔ未満", "最大積載量１ｔ以上２ｔ未満" , "最大積載量2ｔ以上4ｔ未満" , "最大積載量4ｔ以上6ｔ未満", "最大積載量6ｔ以上8ｔ未満" ,
                                                "最大積載量8ｔ以上10ｔ未満","最大積載量10ｔ以上12ｔ未満","最大積載量１２ｔ以上17ｔ未満","最大積載量17ｔ以上","特殊用途自動車"};

            string[] 種別Array2 = new string[] { "天然ガス自動車(ＣＮＧ自動車)", "電気自動車", "ハイブリッド自動車", "メタノール自動車", "ガソリン自動車", "ＬＰＧ自動車" };

            string[] 種別Array3 = new string[] { "ディーゼル自動車", "天然ガス自動車", "電気自動車", "ハイブリッド自動車", "メタノール自動車", "ガソリン自動車", "ＬＰＧ自動車" };

            for (int ArrayCnt = 0; ArrayCnt <= 9; ArrayCnt++)
            {
                foreach (var Rows in tbl.Rows)
                {
                    p事業用区分 = Convert.ToInt32(tbl.Rows[cnt]["事業用区分"].ToString());
                    pディーゼル区分 = Convert.ToInt32(tbl.Rows[cnt]["ディーゼル区分"].ToString());
                    p種別 = tbl.Rows[cnt]["種別"].ToString();
                    if (p事業用区分 == 0 && pディーゼル区分 == 0)
                    {
                        if (種別Array1[ArrayCnt] == p種別)
                        {
                            CellRange = xlApp.Cells[row, cal1] as Excel.Range;
                            CellRange.Value2 = tbl.Rows[cnt]["保有台数"].ToString() == "" ? "0" : tbl.Rows[cnt]["保有台数"].ToString();

                            CellRange = xlApp.Cells[row, cal2] as Excel.Range;
                            CellRange.Value2 = tbl.Rows[cnt]["走行距離"].ToString() == "" ? "0" : tbl.Rows[cnt]["走行距離"].ToString();

                            CellRange = xlApp.Cells[row, cal3] as Excel.Range;
                            CellRange.Value2 = tbl.Rows[cnt]["燃料使用量"].ToString() == "" ? "0" : tbl.Rows[cnt]["燃料使用量"].ToString();
                            row++;
                            cnt++;
                            break;
                        }
                        else
                        {
                            if (cnt == TableCnt)
                            {
                                row++;
                            }
                        }
                    }
                    cnt++;
                }
            }
            cnt = 0;
            row++;
            for (int ArrayCnt = 0; ArrayCnt <= 5; ArrayCnt++)
            {
                foreach (var Rows in tbl.Rows)
                {
                    p事業用区分 = Convert.ToInt32(tbl.Rows[cnt]["事業用区分"].ToString());
                    pディーゼル区分 = Convert.ToInt32(tbl.Rows[cnt]["ディーゼル区分"].ToString());
                    p種別 = tbl.Rows[cnt]["種別"].ToString();
                    if (p事業用区分 == 0 && pディーゼル区分 == 1)
                    {
                        if (種別Array2[ArrayCnt] == p種別)
                        {
                            CellRange = xlApp.Cells[row, cal1] as Excel.Range;
                            CellRange.Value2 = tbl.Rows[cnt]["保有台数"].ToString() == "" ? "0" : tbl.Rows[cnt]["保有台数"].ToString();

                            CellRange = xlApp.Cells[row, cal2] as Excel.Range;
                            CellRange.Value2 = tbl.Rows[cnt]["走行距離"].ToString() == "" ? "0" : tbl.Rows[cnt]["走行距離"].ToString();

                            CellRange = xlApp.Cells[row, cal3] as Excel.Range;
                            CellRange.Value2 = tbl.Rows[cnt]["燃料使用量"].ToString() == "" ? "0" : tbl.Rows[cnt]["燃料使用量"].ToString();
                            row++;
                            cnt++;
                            break;
                        }
                        else
                        {
                            if (cnt == TableCnt)
                            {
                                row++;
                            }
                        }
                    }
                    cnt++;
                }
            }
            cnt = 0;
            row = row + 2;
            for (int ArrayCnt = 0; ArrayCnt <= 6; ArrayCnt++)
            {
                foreach (var Rows in tbl.Rows)
                {
                    p事業用区分 = Convert.ToInt32(tbl.Rows[cnt]["事業用区分"].ToString());
                    pディーゼル区分 = Convert.ToInt32(tbl.Rows[cnt]["ディーゼル区分"].ToString());
                    p種別 = tbl.Rows[cnt]["種別"].ToString();
                    if (p事業用区分 == 1)
                    {
                        if (種別Array3[ArrayCnt] == p種別)
                        {
                            CellRange = xlApp.Cells[row, cal1] as Excel.Range;
                            CellRange.Value2 = tbl.Rows[cnt]["保有台数"].ToString() == "" ? "0" : tbl.Rows[cnt]["保有台数"].ToString();

                            CellRange = xlApp.Cells[row, cal2] as Excel.Range;
                            CellRange.Value2 = tbl.Rows[cnt]["走行距離"].ToString() == "" ? "0" : tbl.Rows[cnt]["走行距離"].ToString();

                            CellRange = xlApp.Cells[row, cal3] as Excel.Range;
                            CellRange.Value2 = tbl.Rows[cnt]["燃料使用量"].ToString() == "" ? "0" : tbl.Rows[cnt]["燃料使用量"].ToString();
                            row++;
                            cnt++;
                            break;
                        }
                        else
                        {
                            if (cnt == TableCnt)
                            {
                                row++;
                            }
                        }
                    }
                    cnt++;
                }
            }
            //Appを閉じる            
            wb.Close(true);
            xlApp.Quit();

            //付表2の方を書き込み開始
            base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_SRY23020_EXCEL, new object[] { 集計期間From, 集計期間To, 部門コード }));
        }

        #endregion

        #region Excel出力2

        public void Excel2(DataTable tbl)
        {
            //Excelのパス
            string fileName = @"C:/GREEN/グリーン経営付表.xls";
            Excel.Application xlApp = new Excel.Application();
            //Excelが開かないようにする
            xlApp.Visible = false;
            //指定したパスのExcelを起動
            Excel.Workbook wb = xlApp.Workbooks.Open(Filename: fileName);
            try
            {
                //Sheetの番号か名称で設定できる

                //((Excel.Worksheet)wb.Sheets["5"]).Select();
                ((Excel.Worksheet)wb.Sheets["表７"]).Select();
            }
            catch (Exception ex)
            {
                //Sheetがなかった場合のエラー処理
                //Appを閉じる
                wb.Close(false);
                xlApp.Quit();
                //Errorメッセージ
                MessageBox.Show("指定したSheetは存在しません。\n\r出力処理を中断します。");
                //アプリケーションを終了
                return;
            }
            //変数宣言
            int row = 26;
            int cal1 = 5;
            int cnt = 0;
            string p種別 = string.Empty;
            Excel.Range CellRange;

            foreach (var Rows in tbl.Rows)
            {
                CellRange = xlApp.Cells[row, cal1] as Excel.Range;
                CellRange.Value2 = tbl.Rows[cnt]["保有台数"].ToString() == "" ? "0" : tbl.Rows[cnt]["保有台数"].ToString();
                cnt++;
                row++;
            }
            //Appを閉じる            
            wb.Close(true);
            xlApp.Quit();
        }

        #endregion

        #region PreviewKeyDown

        /// <summary>
        /// 部門指定でEnter押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bumon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (string.IsNullOrEmpty(作成年))
                {
                    this.ErrorMessage = "作成年は入力必須項目です。";
                    MessageBox.Show("作成年は入力必須項目です。");
                    return;
                }

                if (集計期間From == null || 集計期間To == null)
                {
                    this.ErrorMessage = "集計期間は入力必須項目です。";
                    MessageBox.Show("集計期間は入力必須項目です。");
                    return;
                }

                if (部門コード == null)
                {
                    部門コード = 0;
                }

                base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_SRY23010, new object[] { 集計期間From, 集計期間To, 部門コード }));
                base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_SRY23020, new object[] { 集計期間From, 集計期間To, 部門コード }));
            }
        }

        #endregion

        private void PreviewKeyDown_Sakusei_Nen(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (作成年 == string.Empty)
                {
                    作成年 = DateTime.Now.Year.ToString();
                }

                //会社情報設定からG期首・期末年月を取得
                base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_SRY23010_GDATE, new object[] { }));
            }

            //集計期間From = Convert.ToDateTime(作成年 + "/01/01");
            //集計期間To = Convert.ToDateTime(作成年 + "/12/31");
        }

		private void MainWindow_Closed(object sender, EventArgs e)
		{
			spFuhyo1.ItemsSource = null;
			spFuhyo2.ItemsSource = null;
			spFuhyo1.InputBindings.Clear();
			spFuhyo2.InputBindings.Clear();
		}

    }
}
