using GrapeCity.Windows.SpreadGrid;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace KyoeiSystem.Application.Windows.Views
{
    using KyoeiSystem.Framework.Windows.Controls;
    using FwRepPreview = KyoeiSystem.Framework.Reports.Preview;
    using WinForms = System.Windows.Forms;
    using WinFormsScreen = System.Windows.Forms.Screen;

    /// <summary>
    /// 場所別差異表印刷 フォームクラス
    /// </summary>
    public partial class ZIK03010 : RibbonWindowViewBase
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
        public class ConfigZIK04010 : FormConfigBase
        {
            public byte[] spConfigZIK04010 = null;
        }

        /// ※ 必ず public で定義する。
        public ConfigZIK04010 frmcfg = null;
        CommonConfig ccfg = null;
        // SPREAD初期状態保存用
        public byte[] sp_Config = null;

        #endregion

        #region << 定数定義 >>

        /// <summary>通信キー　棚卸在庫の差異数データを取得する</summary>
        private const string GET_PRINTDATA = "ZIK03010_GetCsvData";
        /// <summary>通信キー　棚卸更新処理を実行する</summary>
        private const string GET_CSVDATA = "ZIK03010_GetPrintData";

        /// <summary>帳票定義体ファイルパス</summary>
        private const string ReportFileName = @"Files\ZIK\ZIK03010.rpt";


        #endregion

        /// <summary>
        /// 自社販社区分 内包データ
        /// </summary>
        private enum 自社販社区分 : int
        {
            自社 = 0,
            販社 = 1
        }

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

        #region << 画面初期処理 >>

        /// <summary>
        /// 場所別差異表印刷
        /// </summary>
        public ZIK03010()
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

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            frmcfg = (ConfigZIK04010)ucfg.GetConfigValue(typeof(ConfigZIK04010));

            if (frmcfg == null)
            {
                frmcfg = new ConfigZIK04010();
                ucfg.SetConfigValue(frmcfg);
                frmcfg.spConfigZIK04010 = this.sp_Config;
            }
            else
            {
                // 表示できるかチェック
                var varWidthCHK = WinFormsScreen.PrimaryScreen.Bounds.Width - frmcfg.Left;
                if (varWidthCHK > 10)
                    this.Left = frmcfg.Left;

                // 表示できるかチェック
                var varHeightCHK = WinFormsScreen.PrimaryScreen.Bounds.Height - frmcfg.Top;
                if (varHeightCHK > 10)
                    this.Top = frmcfg.Top;

                this.Width = frmcfg.Width;
                this.Height = frmcfg.Height;

            }
            #endregion

            // 検索画面情報を設定
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });
            base.MasterMaintenanceWindowList.Add("M09_MYHIN", new List<Type> { typeof(MST02010), typeof(SCHM09_MYHIN) });
            base.MasterMaintenanceWindowList.Add("M14_BRAND", new List<Type> { typeof(MST04020), typeof(SCHM14_BRAND) });
            base.MasterMaintenanceWindowList.Add("M15_SERIES", new List<Type> { typeof(MST04021), typeof(SCHM15_SERIES) });
            //base.MasterMaintenanceWindowList.Add("M22_SOUK", new List<Type> { typeof(MST12020), typeof(SCHM22_SOUK) });

            // コンボデータ取得
            AppCommon.SetutpComboboxList(this.cmbItemType, false);

            // 検索条件部の初期設定をおこなう
            initSearchControl();

            // 初期フォーカスを設定
            this.myCompany.SetFocus();
            ResetAllValidation();

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

                var varData = message.GetResultData();
                DataTable dtTbl = (varData is DataTable) ? (varData as DataTable) : null;

                switch (message.GetMessageName())
                {

                    // ===========================
                    // 棚卸在庫帳票出力
                    // ===========================
                    case GET_PRINTDATA:

                        DataTable PrintDt = SetDifData(dtTbl);
                        OutputReport(PrintDt);
                        break;

                    // ===========================
                    // 棚卸在庫CSV出力
                    // ===========================
                    case GET_CSVDATA:

                        DataTable CsvDt = SetDifData(dtTbl);
                        OutputCsv(CsvDt);
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
            base.OnReveivedError(message);
            base.SetFreeForInput();
            this.ErrorMessage = (string)message.GetResultData();
        }

        #endregion

        #region << リボン >>

        #region F1 マスタ照会
        /// <summary>
        /// F1　リボン　マスタ検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            try
            {
                var varCtl = FocusManager.GetFocusedElement(this);
                var ucText = ViewBaseCommon.FindVisualParent<UcLabelTwinTextBox>(varCtl as UIElement);

                // TwinTextboxのF1処理
                if (ucText != null && ucText.DataAccessName == "M22_SOUK_BASYOC")
                {
                    SCHM22_SOUK souk = new SCHM22_SOUK();
                    souk.TwinTextBox = Warehouse;

                    souk.確定コード = souk.TwinTextBox.Text1;

                    if (souk.ShowDialog(this) == true)
                    {
                        Warehouse.Text1 = souk.TwinTextBox.Text1;  // 得意先コード
                        Warehouse.Text2 = souk.TwinTextBox.Text3;  // 得意先略称名
                    }
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
            if (MaintenanceMode == null)
                return;


            // 入力チェック
            if (!isCheckInput())
                return;

            // パラメータ編集
            int companyCd;
            int.TryParse(myCompany.Text1, out companyCd);

            // パラメータ辞書の作成を行う
            Dictionary<string, string> dicCond = setStocktakingParm();

            base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_CSVDATA, new object[] { int.Parse(myCompany.Text1), StocktakingDate.Text, dicCond }));
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
            if (MaintenanceMode == null)
                return;


            // 入力チェック
            if (!isCheckInput())
                return;

            // パラメータ編集
            int companyCd;
            int.TryParse(myCompany.Text1, out companyCd);

            // パラメータ辞書の作成を行う
            Dictionary<string, string> dicCond = setStocktakingParm();

            base.SendRequest(new CommunicationObject(MessageType.RequestData, GET_PRINTDATA, new object[] { int.Parse(myCompany.Text1), StocktakingDate.Text, dicCond }));
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

            ResetAllValidation();
            SetFocusToTopControl();

        }
        #endregion

        #region 画面表示設定
        /// <summary>
        /// 検索条件部の初期設定をおこなう
        /// </summary>
        private void initSearchControl()
        {
            this.myCompany.Text1 = ccfg.自社コード.ToString();
            this.myCompany.IsEnabled = ccfg.自社販社区分 == 自社販社区分.自社.GetHashCode();

        }
        #endregion

        #region 入力検証
        /// <summary>
        /// 入力チェックをおこなう
        /// </summary>
        private bool isCheckInput()
        {
            bool bolResult = false;

            // 入力検証
            if (!base.CheckAllValidation())
            {
                this.ErrorMessage = "入力内容に誤りがあります。";
                MessageBox.Show("入力内容に誤りがあります。");
                return bolResult;
            }

            // 自社コードの入力値検証
            int intCompanyCd;
            if (string.IsNullOrEmpty(myCompany.Text1))
            {
                base.ErrorMessage = "自社コードは必須入力項目です。";
                return bolResult;
            }
            else if (!int.TryParse(myCompany.Text1, out intCompanyCd))
            {
                base.ErrorMessage = "自社コードの入力値に誤りがあります。";
                return bolResult;
            }

            // 棚卸日
            DateTime dteStocktakingDate;
            if (!DateTime.TryParse(StocktakingDate.Text, out dteStocktakingDate))
            {
                ErrorMessage = "棚卸日の内容が正しくありません。";
                MessageBox.Show("入力エラーがあります。");
                return bolResult;
            }

            bolResult = true;
            return bolResult;

        }
        #endregion

        #region パラメータ設定
        /// <summary>
        /// パラメータ辞書の作成を行う
        /// </summary>
        private Dictionary<string, string> setStocktakingParm()
        {
            // パラメータ生成
            Dictionary<string, string> dicCond = new Dictionary<string, string>();
            dicCond.Add("倉庫コード", Warehouse.Text1);
            dicCond.Add("自社品番", Product.Text1);
            dicCond.Add("自社品名", ProductName.Text);
            dicCond.Add("商品分類コード", cmbItemType.SelectedValue.ToString());
            dicCond.Add("ブランドコード", Brand.Text1);
            dicCond.Add("シリーズコード", Series.Text1);

            return dicCond;

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
                this.ErrorMessage = "出力対象のデータがありません。";
                return;
            }

            // CSV用データを取得
            DataTable CSVデータ = CreateStockTakingCsv(tbl);

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
                CSVData.SaveCSV(CSVデータ, sfd.FileName, true, true, false, ',');
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

                int selItemType = int.Parse(cmbItemType.SelectedValue.ToString());
                Dictionary<int, string> itemTypeDic = new Dictionary<int, string>();
                itemTypeDic.Add(0, "指定なし");
                itemTypeDic.Add(1, "食品");
                itemTypeDic.Add(2, "繊維");
                itemTypeDic.Add(3, "その他");


                var parms = new List<FwRepPreview.ReportParameter>()
                {
                    #region 印字パラメータ設定
                    new FwRepPreview.ReportParameter() { PNAME = "会社コード", VALUE = myCompany.Text1 },
                    new FwRepPreview.ReportParameter() { PNAME = "会社名", VALUE = myCompany.Text2 },
                    new FwRepPreview.ReportParameter() { PNAME = "倉庫コード", VALUE = string.IsNullOrEmpty(Warehouse.Text1) ? "" : Warehouse.Text1 },
                    new FwRepPreview.ReportParameter() { PNAME = "倉庫名", VALUE =string.IsNullOrEmpty(Warehouse.Text2) ? "" : Warehouse.Text2 },
                    new FwRepPreview.ReportParameter() { PNAME = "棚卸日", VALUE = StocktakingDate.Text},
                    new FwRepPreview.ReportParameter() { PNAME = "商品分類", VALUE = selItemType < 0 ? string.Empty : itemTypeDic[selItemType] },
                    new FwRepPreview.ReportParameter() { PNAME = "自社品番コード", VALUE = string.IsNullOrEmpty(Product.Text1) ? "" : Product.Text1  },
                    new FwRepPreview.ReportParameter() { PNAME = "自社品番_名称", VALUE = string.IsNullOrEmpty(Product.Text2) ? "" : Product.Text2  },
                    new FwRepPreview.ReportParameter() { PNAME = "自社品名", VALUE = ProductName.Text },
                    new FwRepPreview.ReportParameter() { PNAME = "ブランドコード", VALUE = string.IsNullOrEmpty(Brand.Text1) ? "" : Brand.Text1 },
                    new FwRepPreview.ReportParameter() { PNAME = "ブランド名称", VALUE = string.IsNullOrEmpty(Brand.Text2) ? "" : Brand.Text2 },
                    new FwRepPreview.ReportParameter() { PNAME = "シリーズコード", VALUE = string.IsNullOrEmpty(Series.Text1) ? "" : Series.Text1 },
                    new FwRepPreview.ReportParameter() { PNAME = "シリーズ名称", VALUE = string.IsNullOrEmpty(Series.Text2) ? "" : Series.Text2 },                    
                    #endregion
                };

                DataTable 印刷データ = tbl.Copy();
                印刷データ.TableName = "場所別棚卸一覧表";

                FwRepPreview.ReportPreview view = new FwRepPreview.ReportPreview();
                view.MakeReport(印刷データ.TableName, ReportFileName, 0, 0, 0);
                view.SetReportData(印刷データ);

                view.SetupParmeters(parms);

                base.SetFreeForInput();

                view.PrinterName = frmcfg.PrinterName;
                //view.IsCustomMode = true;

                view.ShowPreview();
                view.Close();
                frmcfg.PrinterName = view.PrinterName;

            }
            catch (Exception ex)
            {
                base.SetFreeForInput();
                this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
                appLog.Error("場所別棚卸一覧表の印刷時に例外が発生しました。", ex);
            }

        }
        #endregion

        #region CSV出力用データ作成
        /// <summary>
        /// CSV出力用データ作成
        /// </summary>
        /// <param name="tbl"></param>
        /// <returns></returns>
        private DataTable CreateStockTakingCsv(DataTable tbl)
        {
            DataTable CSVデータ = new DataTable();
            DateTime p棚卸日;

            // カラム名設定
            CSVデータ.Columns.Add("会社コード");
            CSVデータ.Columns.Add("会社名");
            CSVデータ.Columns.Add("倉庫コード");
            CSVデータ.Columns.Add("倉庫名");
            CSVデータ.Columns.Add("棚卸日");
            CSVデータ.Columns.Add("品番コード");
            CSVデータ.Columns.Add("自社品番");
            CSVデータ.Columns.Add("自社品名");
            CSVデータ.Columns.Add("自社色");
            CSVデータ.Columns.Add("賞味期限");
            CSVデータ.Columns.Add("数量");
            CSVデータ.Columns.Add("単位");
            CSVデータ.Columns.Add("実在庫数量");
            CSVデータ.Columns.Add("差異数量");

            // データセット
            foreach (DataRow data in tbl.Rows)
            {
                DataRow row = CSVデータ.NewRow();
                row["会社コード"] = myCompany.Text1;
                row["会社名"] = myCompany.Text2;
                row["倉庫コード"] = data["倉庫コード"].ToString();
                row["倉庫名"] = data["倉庫名"].ToString();
                row["棚卸日"] = DateTime.TryParse(data["棚卸日"].ToString(), out p棚卸日) ? p棚卸日.ToShortDateString() : string.Empty;
                row["品番コード"] = data["品番コード"].ToString();
                row["自社品番"] = data["自社品番"].ToString();
                row["自社品名"] = data["自社品名"].ToString();
                row["自社色"] = data["自社色"].ToString();
                row["賞味期限"] = data["表示用賞味期限"].ToString();
                row["数量"] = data["数量"].ToString();
                row["単位"] = data["単位"].ToString();
                row["実在庫数量"] = data["実在庫数"].ToString();
                row["差異数量"] = data["差異数量"].ToString();
                CSVデータ.Rows.Add(row);
            }

            return CSVデータ;
        }
        #endregion

        #region チェックリスト入力処理
        private void difListChk_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                cmbItemType.Focus();
            }
        }
        #endregion

        #region 差異データ設定
        /// <summary>
        /// 差異データ設定
        /// </summary>
        /// <param name="tbl"></param>
        /// <returns></returns>
        private DataTable SetDifData(DataTable tbl)
        {
            DataTable retDt = new DataTable();
            retDt = tbl.Clone();

            // 差異のみ印刷にチェックが入っている場合
            if ((bool)difListChk.IsChecked)
            {
                // 差異数量が0以外のものを取得
                foreach (DataRow row in tbl.Rows)
                {
                    if ((decimal)row["差異数量"] != 0)
                    {

                        retDt.ImportRow(row);
                    }
                }
            }
            else
            {
                retDt = tbl;
            }

            return retDt;
        }

        #endregion

        #endregion

        #region << コントロールイベント群 >>

        #region 会社コード変更イベント

        /// <summary>
        /// 会社コード変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myCompany_cText1Changed(object sender, RoutedEventArgs e)
        {
            // 会社コードが変更された場合、クリア
            Warehouse.Text1 = string.Empty;
        }

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

        }
        #endregion

        #endregion

    }

}
