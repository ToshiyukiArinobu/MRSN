using KyoeiSystem.Framework.Common;
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
    /// 商品在庫残高一覧表 フォームクラス
    /// </summary>
    public partial class ZIK05010 : WindowReportBase
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
        public class ConfigZIK05010 : FormConfigBase
        {
            public byte[] spConfig = null;
        }

        /// ※ 必ず public で定義する。
        public ConfigZIK05010 frmcfg = null;
        CommonConfig ccfg = null;
        // SPREAD初期状態保存用
        public byte[] spConfig = null;

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

        #region << 定数定義 >>

        private const string GET_PRINT_LIST = "ZIK05010_GetPrintList";
        private const string GET_CSV_LIST = "ZIK05010_GetCsvList";

        /// <summary>帳票定義体ファイルパス</summary>
        private const string ReportFileName = @"Files\ZIK\ZIK05010.rpt";

        /// <summary>初期決算月</summary>
        private const int DEFAULT_SETTLEMENT_MONTH = 3;

        #endregion

        #region << 画面初期処理 >>

        /// <summary>
        /// シリーズ･商品別売上統計表 コンストラクタ
        /// </summary>
        public ZIK05010()
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
            frmcfg = (ConfigZIK05010)ucfg.GetConfigValue(typeof(ConfigZIK05010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigZIK05010();
                ucfg.SetConfigValue(frmcfg);
                frmcfg.spConfig = this.spConfig;
            }
            else
            {
                //表示できるかチェック
                var varWidthCHK = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - frmcfg.Left;
                if (varWidthCHK > 10)
                {
                    this.Left = frmcfg.Left;
                }
                //表示できるかチェック
                var varHeightCHK = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - frmcfg.Top;
                if (varHeightCHK > 10)
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

            // 検索画面情報を設定
            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });
            base.MasterMaintenanceWindowList.Add("M09_MYHIN", new List<Type> { typeof(MST02010), typeof(SCHM09_MYHIN) });
            base.MasterMaintenanceWindowList.Add("M14_BRAND", new List<Type> { typeof(MST04020), typeof(SCHM14_BRAND) });
            base.MasterMaintenanceWindowList.Add("M15_SERIES", new List<Type> { typeof(MST04021), typeof(SCHM15_SERIES) });
            base.MasterMaintenanceWindowList.Add("M22_SOUK", new List<Type> { typeof(MST12020), typeof(SCHM22_SOUK) });

            // コンボデータ取得
            AppCommon.SetutpComboboxList(this.cmbItemType, false);

            // 画面初期化
            ScreenClear();

            // 検索条件部の初期設定をおこなう
            initSearchControl();

            // 初期フォーカスを設定
            this.myCompany.SetFocus();
            ResetAllValidation();

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

                var varData = message.GetResultData();
                DataTable dtTbl = (varData is DataTable) ? (varData as DataTable) : null;

                switch (message.GetMessageName())
                {
                    // ===========================
                    // CSVデータ取得
                    // ===========================
                    case GET_CSV_LIST:
                        OutputCsv(dtTbl);
                        break;

                    // ===========================
                    // 帳票データ取得
                    // ===========================
                    case GET_PRINT_LIST:
                        OutputReport(dtTbl);
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
                var varCtl = FocusManager.GetFocusedElement(this);
                var varUctext = ViewBaseCommon.FindVisualParent<M01_TOK_TextBox>(varCtl as UIElement);

                if (varUctext != null)
                {
                    varUctext.OpenSearchWindow(this);

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
            if (!isCheckInput())
                return;

            // パラメータ編集
            int intCompanyCd;
            int.TryParse(myCompany.Text1, out intCompanyCd);

            decimal dcmCoefficient;
            decimal.TryParse(Coefficient.Text, out dcmCoefficient);

            // パラメータ辞書情報設定
            Dictionary<string, string> dicCond = setSearchParams();

            // データ取得
            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    GET_CSV_LIST,
                    new object[] {
                        intCompanyCd,
                        ClosingDate.Text,
                        dicCond,
                        dcmCoefficient
                    }));

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
            if (!isCheckInput())
                return;

            // パラメータ編集
            int intCompanyCd;
            int.TryParse(myCompany.Text1, out intCompanyCd);

            decimal dcmCoefficient;
            decimal.TryParse(Coefficient.Text, out dcmCoefficient);

            // パラメータ情報設定
            Dictionary<string, string> dicCond = setSearchParams();

            // データ取得
            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    GET_PRINT_LIST,
                    new object[] {
                        intCompanyCd,
                        ClosingDate.Text,
                        dicCond,
                        dcmCoefficient
                    }));

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

        #region 検索ボタン
        /// <summary>
        /// 検索ボタンが押下された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            // TODO:処理が必要になったら記述する
        }

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

        #region 画面表示設定
        /// <summary>
        /// 検索条件部の初期設定をおこなう
        /// </summary>
        private void initSearchControl()
        {
            this.myCompany.Text1 = ccfg.自社コード.ToString();
            this.myCompany.IsEnabled = ccfg.自社販社区分 == 自社販社区分.自社.GetHashCode();
            this.Coefficient.Text = "1.0";

        }
        #endregion

        #endregion

        #region 業務入力チェック
        /// <summary>
        /// 業務入力チェックをおこなう
        /// </summary>
        /// <returns></returns>
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

            // 会社コードの入力値検証
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
            DateTime dteClosingDate;
            if (!DateTime.TryParse(ClosingDate.Text, out dteClosingDate))
            {
                ErrorMessage = "棚卸日の内容が正しくありません。";
                MessageBox.Show("入力エラーがあります。");
                return bolResult;
            }

            bolResult = true;
            return bolResult;

        }
        #endregion

        #region 検索パラメータ設定
        /// <summary>
        /// 検索パラメータを設定する
        /// </summary>
        private Dictionary<string, string> setSearchParams()
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
        /// <param name="dtTbl"></param>
        private void OutputCsv(DataTable dtTbl)
        {
            if (dtTbl == null || dtTbl.Rows.Count == 0)
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
                CSVData.SaveCSV(dtTbl, sfd.FileName, true, true, false, ',');
                MessageBox.Show("CSVファイルの出力が完了しました。");
            }

        }
        #endregion

        #region 帳票出力
        /// <summary>
        /// 帳票の印刷処理をおこなう
        /// </summary>
        /// <param name="dtTbl"></param>
        private void OutputReport(DataTable dtTbl)
        {
            PrinterDriver ret = AppCommon.GetPrinter(frmcfg.PrinterName);
            if (ret.Result == false)
            {
                this.ErrorMessage = "プリンタドライバーがインストールされていません！";
                return;
            }
            frmcfg.PrinterName = ret.PrinterName;

            if (dtTbl == null || dtTbl.Rows.Count == 0)
            {
                this.ErrorMessage = "印刷データがありません。";
                return;
            }

            try
            {
                base.SetBusyForInput();

                int intSelItemType = int.Parse(cmbItemType.SelectedValue.ToString());
                Dictionary<int, string> dicItemType = new Dictionary<int, string>();
                dicItemType.Add(0, "指定なし");
                dicItemType.Add(1, "食品");
                dicItemType.Add(2, "繊維");
                dicItemType.Add(3, "その他");

                var varParms = new List<FwRepPreview.ReportParameter>()
                {
                    #region 印字パラメータ設定
                    new FwRepPreview.ReportParameter() { PNAME = "会社コード", VALUE = myCompany.Text1 },
                    new FwRepPreview.ReportParameter() { PNAME = "会社名", VALUE = myCompany.Text2 },
                    new FwRepPreview.ReportParameter() { PNAME = "倉庫コード", VALUE = string.IsNullOrEmpty(Warehouse.Text1) ? "" : Warehouse.Text1 },
                    new FwRepPreview.ReportParameter() { PNAME = "倉庫名", VALUE =string.IsNullOrEmpty(Warehouse.Text2) ? "" : Warehouse.Text2 },
                    new FwRepPreview.ReportParameter() { PNAME = "締年月", VALUE = ClosingDate.Text},
                    new FwRepPreview.ReportParameter() { PNAME = "商品分類", VALUE = dicItemType[intSelItemType] },
                    new FwRepPreview.ReportParameter() { PNAME = "自社品番コード", VALUE = string.IsNullOrEmpty(Product.Text1) ? "" : Product.Text1  },
                    new FwRepPreview.ReportParameter() { PNAME = "自社品番_名称", VALUE = string.IsNullOrEmpty(Product.Text2) ? "" : Product.Text2  },
                    new FwRepPreview.ReportParameter() { PNAME = "自社品名", VALUE = ProductName.Text },
                    new FwRepPreview.ReportParameter() { PNAME = "ブランドコード", VALUE = string.IsNullOrEmpty(Brand.Text1) ? "" : Brand.Text1 },
                    new FwRepPreview.ReportParameter() { PNAME = "ブランド名称", VALUE = string.IsNullOrEmpty(Brand.Text2) ? "" : Brand.Text2 },
                    new FwRepPreview.ReportParameter() { PNAME = "シリーズコード", VALUE = string.IsNullOrEmpty(Series.Text1) ? "" : Series.Text1 },
                    new FwRepPreview.ReportParameter() { PNAME = "シリーズ名称", VALUE = string.IsNullOrEmpty(Series.Text2) ? "" : Series.Text2 },
                    new FwRepPreview.ReportParameter() { PNAME = "係数", VALUE = string.IsNullOrEmpty(Coefficient.Text) ? "" : Coefficient.Text },
                    #endregion
                };

                DataTable 印刷データ = dtTbl.Copy();
                印刷データ.TableName = "商品在庫残高一覧表";

                FwRepPreview.ReportPreview view = new FwRepPreview.ReportPreview();
                view.MakeReport(印刷データ.TableName, ReportFileName, 0, 0, 0);
                view.SetReportData(印刷データ);

                view.SetupParmeters(varParms);

                base.SetFreeForInput();

                view.PrinterName = frmcfg.PrinterName;
                view.IsCustomMode = true;
                view.ShowPreview();
                view.Close();
                frmcfg.PrinterName = view.PrinterName;

            }
            catch (Exception ex)
            {
                base.SetFreeForInput();
                this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
                appLog.Error("商品在庫残高一覧表の印刷時に例外が発生しました。", ex);
            }

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

    }

}
