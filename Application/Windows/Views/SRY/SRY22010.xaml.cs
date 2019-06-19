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

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;
using KyoeiSystem.Application.Windows.Views;
using GrapeCity.Windows.SpreadGrid;
using KyoeiSystem.Framework.Windows.Controls;


namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 基礎情報マスタ入力
    /// </summary>
    public partial class SRY22010 : WindowMasterMainteBase
    {
        #region 定数定義

        //*** 表示 ***//
        private const string LOAD_SRY22010 = "LOAD_SRY22010";
        //*** 検索 ***//
        private const string SEARCH_SRY22010 = "SEARCH_SRY22010";
        //*** セル登録 ***//
        private const string INSERT_SRY22010 = "INSERT_SRY22010";
        //***　一括登録 ***//
        private const string NINSERT_SRY22010 = "NINSERT_SRY22010";
        //*** 削除 ***//
        private const string DELETE_SRY22010 = "DELETE_SRY22010";
        //*** グローバル変数 ***//
        static public int Cnt = 0;
        //*** 印刷 ***//
        private const string OUTPUT_SRY22010_M87_CNTL = "OUTPUT_SRY22010_M87_CNTL";
        private const string OUTPUT_SRY22010 = "OUTPUT_SRY22010";
        private const string rptFullPathName_PIC = @"Files\SRY\SRY22010.rpt";
        #endregion

        #region 明細クリック時のアクション定義
        /// <summary>
        /// 明細クリック時のアクション定義
        /// </summary>
        public class cmd燃費詳細表示 : ICommand
        {
            private GcSpreadGrid _gcSpreadGrid;
            public cmd燃費詳細表示(GcSpreadGrid gcSpreadGrid)
            {
                this._gcSpreadGrid = gcSpreadGrid;
            }
            public bool CanExecute(object parameter)
            {
                return true;
            }
            public event EventHandler CanExecuteChanged;
            public void OnCanExecuteChanged()
            {
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, EventArgs.Empty);
            }
            public void Execute(object parameter)
            {
                try
                {
                    CellCommandParameter cellCommandParameter = (CellCommandParameter)parameter;
                    if (cellCommandParameter.Area == SpreadArea.Cells
                        && cellCommandParameter.CellPosition.Row >= 0
                        && cellCommandParameter.CellPosition.Row < this._gcSpreadGrid.Rows.Count
                        )
                    {
                        int rowNo = cellCommandParameter.CellPosition.Row;
                        var row = this._gcSpreadGrid.Rows[rowNo];
                        var sNo = row.Cells[this._gcSpreadGrid.Columns["車輌コード"].Index].Value;
                        var sNe = row.Cells[this._gcSpreadGrid.Columns["年月"].Index].Value;
                        var sTe = row.Cells[this._gcSpreadGrid.Columns["点検日"].Index].Value;
                        var wnd = GetWindow(this._gcSpreadGrid);
                        if ((int?)sNo != null && (int?)sNe != null)
                        {
                            SRY22020 frm = new SRY22020();
                            frm.初期車輌コード = (int?)sNo;
                            frm.初期作成年月 = (int?)sNe;
                            frm.初期点検日 = (int?)sTe;
                            frm.ShowDialog(wnd);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    this._gcSpreadGrid.IsEnabled = true;
                }
            }
        }
        #endregion

        #region バインド用変数

        public byte[] spConfig = null;

        private string _作成年月 = string.Empty;
        public string 作成年月
        {
            get { return this._作成年月; }
            set { this._作成年月 = value; NotifyPropertyChanged(); }
        }

        private int? _自社部門ID;
        public int? 自社部門ID
        {
            get { return this._自社部門ID; }
            set { this._自社部門ID = value; NotifyPropertyChanged(); }
        }

        private int? _点検日一括 = null;
        public int? 点検日一括
        {
            get { return this._点検日一括; }
            set { this._点検日一括 = value; NotifyPropertyChanged(); }
        }


        private DataTable _燃費目標データ一覧 = new DataTable();
        public DataTable 燃費目標データ一覧
        {
            get { return this._燃費目標データ一覧; }
            set
            {
                this._燃費目標データ一覧 = value;
                NotifyPropertyChanged();
            }
        }

        //***  印刷情報  ***//
        private string _印刷作成年;
        public string 印刷作成年
        {
            get { return this._印刷作成年; }
            set { _印刷作成年 = value; NotifyPropertyChanged(); }
        }

        private string _開始月;
        public string 開始月
        {
            get { return this._開始月; }
            set { _開始月 = value; NotifyPropertyChanged(); }
        }

        private string _終了月;
        public string 終了月
        {
            get { return this._終了月; }
            set { _終了月 = value; NotifyPropertyChanged(); }
        }

        #endregion

        /// <summary>
        /// 基礎情報マスタ入力
        /// </summary>
        public SRY22010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public void ScreenClear()
        {
            作成年月 = null;
            自社部門ID = null;
            点検日一括 = null;
            印刷作成年 = null;
            開始月 = null;
            終了月 = null;
            this.sp燃費目標データ.ItemsSource = null;
            base.SendRequest(new CommunicationObject(MessageType.RequestData, LOAD_SRY22010, new object[] { null }));
            SetFocusToTopControl();
        }

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            this.spConfig = AppCommon.SaveSpConfig(this.sp燃費目標データ);

            //詳細ボタンの設定
            ButtonCellType btn = this.sp燃費目標データ.Columns[0].CellType as ButtonCellType;
            btn.Command = new cmd燃費詳細表示(sp燃費目標データ);
            base.MasterMaintenanceWindowList.Add("M71_BUM", new List<Type> { typeof(MST10010), typeof(SCH10010) });
            base.SendRequest(new CommunicationObject(MessageType.RequestData, LOAD_SRY22010, new object[] { 自社部門ID }));

			SetFocusToTopControl();

        }

        #region 受信系処理
        /// <summary>
        /// データ受信メソッド
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
                    case LOAD_SRY22010:
                        if (tbl.Rows.Count > 0)
                        {
                            燃費目標データ一覧 = tbl;
                            this.sp燃費目標データ.ItemsSource = this.燃費目標データ一覧.DefaultView;
                            Nengetu();
                        }
                        else
                        {
                            this.ErrorMessage = "データがありません。";
                            燃費目標データ一覧 = null;
                            this.sp燃費目標データ.ItemsSource = null;
                            return;
                        }
                        break;

                    case SEARCH_SRY22010:
                        if (tbl.Rows.Count > 0)
                        {
                            燃費目標データ一覧 = tbl;
                            this.sp燃費目標データ.ItemsSource = this.燃費目標データ一覧.DefaultView;
                            Nengetu();
                        }
                        else
                        {
                            this.ErrorMessage = "データがありません。";
                            燃費目標データ一覧 = null;
                            this.sp燃費目標データ.ItemsSource = null;
                            return;
                        }
                        break;

                    case OUTPUT_SRY22010_M87_CNTL:
                        if(tbl.Rows.Count > 0)
                        {
                            開始月 = tbl.Rows[0]["G期首月日"].ToString().Length == 3 ? tbl.Rows[0]["G期首月日"].ToString().Substring(0,1) 
                                : tbl.Rows[0]["G期首月日"].ToString().Length == 4 ? tbl.Rows[0]["G期首月日"].ToString().Substring(0,2) : "4";
                            終了月 = tbl.Rows[0]["G期末月日"].ToString().Length == 3 ? tbl.Rows[0]["G期末月日"].ToString().Substring(0, 1)
                                : tbl.Rows[0]["G期末月日"].ToString().Length == 4 ? tbl.Rows[0]["G期末月日"].ToString().Substring(0, 2) : "3";
                        }
                        break;

                    case NINSERT_SRY22010:
                        MessageBox.Show("更新が完了しました。");
                        ChangeKeyItemChangeable(true);
                        SetFocusToTopControl();
                        this.MaintenanceMode = string.Empty;
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, LOAD_SRY22010, new object[] { }));
                        break;

                    case OUTPUT_SRY22010:
                        DispPreviw(tbl);
                        break;


                }


            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }
        }

        public void Nengetu()
        {
            if (作成年月 != string.Empty && 作成年月 != null)
            {
                //年月を作成
                int NngVal = AppCommon.IntParse(作成年月.Substring(0, 4) + 作成年月.Substring(5, 2));
                var ColVal = sp燃費目標データ.Columns["年月"].Index;
                int Count = 0;
                foreach (var Rows in sp燃費目標データ.Rows)
                {
                    sp燃費目標データ[Count, ColVal].Value = NngVal;
                    Count++;
                }
            }
        }

        /// <summary>
        /// データエラー受信メソッド
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            MessageBox.Show(ErrorMessage);
        }
        #endregion

        #region 日付取得

        /// <summary>
        /// 作成年月でEnterキー押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcLabelTwinTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {

                    try
                    {
                        NNGDays ret = AppCommon.GetNNGDays(作成年月);
                        if (ret.Result == true)
                        {
                            作成年月 = ret.DateFrom;
                            //最後尾検索
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, SEARCH_SRY22010, new object[] { 自社部門ID, 作成年月 }));
                        }
                        else if (ret.Result == false)
                        {
                            this.ErrorMessage = "入力された作成年月は利用できません。　入力例：201406";
                            this.作成年月 = string.Empty;
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                appLog.Debug("【Error:{0}***{1}】", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                this.ErrorMessage = ex.Message;
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
                ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);
            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }
        }

        /// <summary>
        /// F9 データ登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {


            if (OUTPIT_SakuseiNen.Text != null && OUTPIT_Stuki.Text != null && OUTPIT_Etuki.Text != null && OUTPIT_SakuseiNen.Text != "" && OUTPIT_Stuki.Text != "" && OUTPIT_Etuki.Text != "")
                {
					base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, OUTPUT_SRY22010, new object[] { 自社部門ID, 印刷作成年, 開始月, 終了月 }));
                }
                else
                {
                    this.ErrorMessage = "印刷を行う場合は(印刷用)作成年が必須項目になります。";
                    MessageBox.Show("印刷を行う場合は(印刷用)作成年が必須項目になります。");
                    return;
                }

        }

        /// <summary>
        /// F10 入力取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            ScreenClear();
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
                view.MakeReport("車輌点検", rptFullPathName_PIC, 0, 0, 0);
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

        private void 自社部門_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                base.SendRequest(new CommunicationObject(MessageType.RequestData, LOAD_SRY22010, new object[] { 自社部門ID }));
            }
        }

        private void 点検日_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (点検日一括 != null)
                {
                    if (点検日一括.Value >= 1 && 点検日一括.Value <= 31)
                    {
                        int TnkVal = 点検日一括.Value;
                        var ColVal = sp燃費目標データ.Columns["点検日"].Index;
                        int Count = 0;
                        foreach (var Rows in sp燃費目標データ.Rows)
                        {
                            sp燃費目標データ[Count, ColVal].Value = TnkVal;
                            Count++;
                        }
                    }
                }
            }
        }

        private void 印刷用作成年_PriviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (印刷作成年 != null)
                {
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, OUTPUT_SRY22010_M87_CNTL, new object[] {  }));
                }
            }
        }

		private void WindowMasterMainteBase_Closed(object sender, EventArgs e)
		{
			sp燃費目標データ.InputBindings.Clear();
			this.sp燃費目標データ.ItemsSource = null;


		}

    }
}
