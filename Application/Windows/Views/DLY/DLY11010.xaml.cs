using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;

namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 納品書発行
    /// </summary>
    public partial class DLY11010 : WindowReportBase
    {
        #region 定数定義

        /// <summary>納品書 出力データ取得</summary>
        private const string DLY11010_PRINTOUT = "DLY11010_Print";

        /// <summary>納品書 帳票定義パス</summary>
        private const string rptFullPathName = @"Files\DLY\DLY11010.rpt";

        #endregion

        //20190919 add-s CB 軽減税率対応
        #region 権限関係
        CommonConfig ccfg = null;
        #endregion
        //20190919 add-e CB 軽減税率対応

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;

        /// <summary>
        /// 画面固有設定項目のクラス定義
        /// ※ 必ず public で定義する。
        /// </summary>
        public class ConfigDLY11010 : FormConfigBase
        {
            public byte[] spConfig20180118 = null;

        }

        /// ※ 必ず public で定義する。
        public ConfigDLY11010 frmcfg = null;

        public byte[] sp_Config = null;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// 納品書出力 コンストラクタ
        /// </summary>
        public DLY11010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #endregion

        #region Load時

        /// <summary>
        /// 画面読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigDLY11010)ucfg.GetConfigValue(typeof(ConfigDLY11010));

            //20190919 add-s CB 軽減税率対応
            // 権限設定を呼び出す(ucfgを取得した後のに入れる)
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            //20190919 add-e CB 軽減税率対応

            if (frmcfg == null)
            {
                frmcfg = new ConfigDLY11010();
                ucfg.SetConfigValue(frmcfg);
                frmcfg.spConfig20180118 = this.sp_Config;

            }
            else
            {
                // 表示できるかチェック
                var WidthCHK = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - frmcfg.Left;
                if (WidthCHK > 10)
                {
                    this.Left = frmcfg.Left;
                }
                // 表示できるかチェック
                var HeightCHK = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - frmcfg.Top;
                if (HeightCHK > 10)
                {
                    this.Top = frmcfg.Top;
                }
                this.Width = frmcfg.Width;
                this.Height = frmcfg.Height;

            }
            #endregion

            // 検索画面情報を設定
            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { typeof(MST01010), typeof(SCHM01_TOK) });

            // 初期値設定
            txt売上日From.Text = DateTime.Now.ToString("yyyy/MM/dd");
            txt売上日To.Text = DateTime.Now.ToString("yyyy/MM/dd");

            SetFocusToTopControl();

        }

        #endregion

        #region データ受信メソッド

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
                case DLY11010_PRINTOUT:
                    printPreviewDisp(tbl);
                    break;

                default:
                    break;

            }

        }

        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            this.Message = base.ErrorMessage;
            base.SetFreeForInput();
        }

        #endregion

        #region リボン

        /// <summary>
        /// F1 リボン　マスタ検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            try
            {
                object elmnt = FocusManager.GetFocusedElement(this);
                var toktb = ViewBaseCommon.FindVisualParent<M01_TOK_TextBox>(elmnt as Control);

                if (toktb != null)
                {
                    toktb.OpenSearchWindow(this);
                }
                else
                {
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);
                }

            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }
        }

        /// <summary>
        /// F10　リボン　印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            if (!isInputFormValidation())
                return;

            if (!base.CheckAllValidation())
            {
                string msg = "入力内容に誤りがあります。";
                this.ErrorMessage = msg;
                SetFocusToTopControl();
                return;
            }

            PrinterDriver ret = AppCommon.GetPrinter(frmcfg.PrinterName);
            if (ret.Result == false)
            {
                this.ErrorMessage = "プリンタドライバーがインストールされていません！";
                return;
            }
            frmcfg.PrinterName = ret.PrinterName;

            if (MessageBox.Show(
                    AppConst.CONFIRM_PRINT,
                    "印刷確認",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.Yes) == MessageBoxResult.No)
                return;

            //20190919 add & mod -s CB 軽減税率対応
            //ccfg.自社区分 が　0の場合は現状のままccfg.自社区分が1の場合は、
            //T02_URHDの会社名コードとccfg.自社コードが一致する

            //int? 自社コード = null;
            //if (ccfg.自社販社区分 == 1)
            //{
            //    自社コード = ccfg.自社コード;
            //}
            //base.SendRequest(
            //    new CommunicationObject(
            //        MessageType.UpdateData,
            //        DLY11010_PRINTOUT,
            //        new object[] {
            //            this.txt売上日From.Text,
            //            this.txt売上日To.Text,
            //            this.txt得意先.Text1,
            //            this.txt得意先.Text2,
            //            this.txt伝票番号From.Text,
            //            this.txt伝票番号To.Text
            //        }));

            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    DLY11010_PRINTOUT,
                    new object[] {
                        this.txt売上日From.Text,
                        this.txt売上日To.Text,
                        this.txt得意先.Text1,
                        this.txt得意先.Text2,
                        this.txt伝票番号From.Text,
                        this.txt伝票番号To.Text,
                        ccfg.自社コード
                    }));
            //20190919 add & mod -e CB 軽減税率対応


            //if (this.売上明細データ == null)
            //{
            //    return;
            //}
            //if (this.売上明細データ.Count == 0)
            //{
            //    this.ErrorMessage = "印刷データがありません。";
            //    return;
            //}
            //try
            //{
            //    base.SetBusyForInput();
            //    var parms = new List<Framework.Reports.Preview.ReportParameter>()
            //    {
            //        new Framework.Reports.Preview.ReportParameter(){ PNAME="日付種類" , VALUE=(this.cmb検索日付種類.Text==null?"":this.cmb検索日付種類.Text)},
            //        new Framework.Reports.Preview.ReportParameter(){ PNAME="検索日付From", VALUE=(this.検索日付From==null?"":this.検索日付From)},
            //        new Framework.Reports.Preview.ReportParameter(){ PNAME="検索日付To", VALUE=(this.検索日付To==null?"":this.検索日付To)},
            //        new Framework.Reports.Preview.ReportParameter(){ PNAME="支払先指定", VALUE=(this.txtbox支払先.Text2==null?"":this.txtbox支払先.Text2)},
            //        new Framework.Reports.Preview.ReportParameter(){ PNAME="未定区分", VALUE=(this.cmb未定区分.Text==null?"":this.cmb未定区分.Text)},
            //        new Framework.Reports.Preview.ReportParameter(){ PNAME="部門指定", VALUE=(this.cmb部門指定.Text==null?"":this.cmb部門指定.Text)},
            //        new Framework.Reports.Preview.ReportParameter(){ PNAME="発地名" , VALUE=(this.txt発地名.Text==null?"":this.txt発地名.Text)},
            //        new Framework.Reports.Preview.ReportParameter(){ PNAME="着地名" , VALUE=(this.txt着地名.Text==null?"":this.txt着地名.Text)},
            //        new Framework.Reports.Preview.ReportParameter(){ PNAME="商品名" , VALUE=(this.txt商品名.Text==null?"":this.txt商品名.Text)},
            //        new Framework.Reports.Preview.ReportParameter(){ PNAME="請求摘要" , VALUE=(this.txt請求摘要.Text==null?"":this.txt請求摘要.Text)},
            //        new Framework.Reports.Preview.ReportParameter(){ PNAME="社内備考" , VALUE=(this.txt社内備考.Text==null?"":this.txt社内備考.Text)},
            //        new Framework.Reports.Preview.ReportParameter(){ PNAME="表示順序", VALUE=string.Format("{0} {1} {2} {3} {4}", 表示順名[0], 表示順名[1], 表示順名[2], 表示順名[3], 表示順名[4])},
            //    };
            //    KyoeiSystem.Framework.Reports.Preview.ReportPreview view = null;

            //    DataTable 印刷データ = new DataTable();
            //    //リストをデータテーブルへ
            //    //AppCommon.ConvertToDataTable(売上明細データ, 印刷データ);

            //    // SPREADのデータ順に List型のデータからデータテーブル作成する
            //    // バインドしている項目名と印刷で使用している項目名が違う場合、その変換用リストを指定する。
            //    Dictionary<string, string> changecols = new Dictionary<string, string>()
            //    {
            //        // バインド項目名、印刷項目名
            //        {"未区" , "未定区分名"},
            //        {"税区" , "請求税区分名"},
            //        {"走行KM" , "走行ＫＭ"},
            //        { "d売上金額", "売上金額" },
            //        { "d通行料", "通行料" },
            //        { "d支払金額", "支払金額" },
            //        { "d支払通行料", "支払通行料" },
            //    };
            //    AppCommon.ConvertSpreadDataToTable<DLY11010_Member>(this.sp売上明細データ, 印刷データ, changecols);

            //    印刷データ.TableName = "支払明細一覧";
            //    view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
            //    view.MakeReport(印刷データ.TableName, ReportFileName, 0, 0, 0);

                //view.SetReportData(印刷データ);

                //view.SetupParmeters(parms);

                //base.SetFreeForInput();

                //view.PrinterName = frmcfg.PrinterName;
                //view.ShowPreview();
                //view.Close();

                //frmcfg.PrinterName = view.PrinterName;

            //}
            //catch (Exception ex)
            //{
            //    base.SetFreeForInput();
            //    this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
            //    appLog.Error("支払先支払明細書の印刷時に例外が発生しました。", ex);
            //}

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

        #region << 機能処理部 >>

        /// <summary>
        /// 印刷実行時の入力検証
        /// </summary>
        /// <returns></returns>
        private bool isInputFormValidation()
        {
            if (string.IsNullOrEmpty(this.txt売上日From.Text) || string.IsNullOrEmpty(this.txt売上日To.Text))
            {
                base.ErrorMessage = "売上日が入力されていません。";
                return false;
            }

            return true;

        }

        /// <summary>
        /// 納品書のプレビュー画面を表示する
        /// </summary>
        /// <param name="tbl"></param>
        private void printPreviewDisp(DataTable tbl)
        {
            try
            {
                if (tbl.Rows.Count < 1)
                {
                    this.ErrorMessage = "対象データが存在しません。";
                    return;
                }

                // 印刷処理
                KyoeiSystem.Framework.Reports.Preview.ReportPreview view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
                view.PrinterName = frmcfg.PrinterName;
                // 第1引数　帳票タイトル
                // 第2引数　帳票ファイルPass
                // 第3以上　帳票の開始点(0で良い)
                view.MakeReport("納品書", rptFullPathName, "トレイ2");

                // 帳票ファイルに送るデータ。
                // 帳票データの列と同じ列名を保持したDataTableを引数とする
                view.SetReportData(tbl);
                view.PrinterName = frmcfg.PrinterName;
                //view.PageSettings.PaperSource.SourceName = "トレイ2";
                view.ShowPreview();
                view.Close();
                frmcfg.PrinterName = view.PrinterName;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region MainWindow_Closed
        //画面が閉じられた時、データを保持する
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            if (ucfg != null)
            {
                if (frmcfg == null) { frmcfg = new ConfigDLY11010(); }
                frmcfg.Top = this.Top;
                frmcfg.Left = this.Left;
                frmcfg.Width = this.Width;
                frmcfg.Height = this.Height;

                ucfg.SetConfigValue(frmcfg);
            }

        }
        #endregion

	}

}
