using GrapeCity.Windows.SpreadGrid;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using KyoeiSystem.Application.Windows.Views.Common;
using System.Windows.Media;


namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 確定処理 画面クラス
    /// </summary>
    public partial class TKS90010 : WindowReportBase
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
        public class ConfigTKS90010 : FormConfigBase
        {
            public byte[] spConfig = null;
        }

        /// ※ 必ず public で定義する。
        public ConfigTKS90010 frmcfg = null;
        CommonConfig ccfg = null;
        // SPREAD初期状態保存用
        public byte[] spConfig = null;

        #endregion

        #region << 列挙型定義 >>
        /// <summary>
        /// sp確定データ一覧ColumnsMapping
        /// </summary>
        private enum GridColumnsMapping : int
        {
            自社名 = 0,
            区分 = 1,
            取引区分 = 2,
            ID = 3,
            得意先名 = 4,
            締日 = 5,
            確定日 = 6,
            確定ボタン = 7,
            取引区分ID = 8,
            確定区分 = 9,
            自社コード = 10
        }

        #endregion

        #region << 定数定義 >>

        private const string GET_LIST_SEARCH = "TKS90010_GetDataList";
        private const string UPDATE = "TKS90010_Update";
        private const string DEFAULT_YYMMDD = "0001/01/01";

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

        #region << クラス変数定義 >>
        /// <summary>グリッドコントローラ</summary>
        GcSpreadGridController gridCtl;
        
        #endregion

        #region 明細クリック時のアクション定義
        /// <summary>
        /// 明細クリック時のアクション定義
        /// </summary>
        public class cmd確定日登録 : ICommand
        {
            private GcSpreadGrid _gcSpreadGrid;
            public cmd確定日登録(GcSpreadGrid gcSpreadGrid)
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

            /// <summary>
            /// 確定ボタン押下
            /// </summary>
            /// <param name="parameter"></param>
            public void Execute(object parameter)
            {
                CellCommandParameter cellCommandParameter = (CellCommandParameter)parameter;
                var parent = ViewBaseCommon.FindVisualParent<TKS90010>(this._gcSpreadGrid);
                string msg = string.Empty;

                if (cellCommandParameter.Area == SpreadArea.Cells)
                {
                    int rowNo = cellCommandParameter.CellPosition.Row;
                    var row = this._gcSpreadGrid.Rows[rowNo];
                    var fixKbn = row.Cells[(int)GridColumnsMapping.確定区分].Value;
                    var fixDay = row.Cells[(int)GridColumnsMapping.確定日].Value;
                    var closeDay = row.Cells[(int)GridColumnsMapping.締日].Value;
                    var toriKbn = row.Cells[(int)GridColumnsMapping.取引区分ID].Value;
                    var toriKbnNm = row.Cells[(int)GridColumnsMapping.取引区分].Value;
                    var jisCode = row.Cells[(int)GridColumnsMapping.自社コード].Value;
                    
                    // エラー情報をクリア
                    row.ValidationErrors.Clear();

                    if (fixDay == null || fixDay.ToString() == DEFAULT_YYMMDD)
                    {
                        msg = "確定日を設定してください。";
                        CellPosition cp = new CellPosition(rowNo, GridColumnsMapping.確定日.GetHashCode());
                        this._gcSpreadGrid[cp].ValidationErrors.Add(new SpreadValidationError(msg, null));
                        MessageBox.Show(msg);
                        return;
                    }

                    if (MessageBox.Show(
                                string.Format("{0}日締の{1}を {2} で確定登録します。\n確定日以前の伝票は編集できなくなります。\nよろしいですか？"
                                    , closeDay.ToString()
                                    , toriKbnNm.ToString()
                                    , fixDay.ToString()),
                                "登録確認",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question,
                                MessageBoxResult.Yes) == MessageBoxResult.No)
                    {   
                        return;
                    }

                    // 登録処理を呼び出し
                    parent.UpdateTableData(jisCode.ToString(), fixKbn.ToString(), closeDay.ToString(), fixDay.ToString(), toriKbn.ToString());

                }
            }
        }
        #endregion

        #region << 画面初期処理 >>

        /// <summary>
        /// 確定処理 コンストラクタ
        /// </summary>
        public TKS90010()
        {
            InitializeComponent();
            this.DataContext = this;

        }

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.sp確定データ一覧.RowCount = 0;
            //this.spConfig = AppCommon.SaveSpConfig(this.sp確定データ一覧);

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigTKS90010)ucfg.GetConfigValue(typeof(ConfigTKS90010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigTKS90010();
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
                //AppCommon.LoadSpConfig(this.sp確定データ一覧, frmcfg.spConfig);
            }

            #endregion

            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });

            ButtonCellType btn = this.sp確定データ一覧.Columns[(int)GridColumnsMapping.確定ボタン].CellType as ButtonCellType;
            btn.Command = new cmd確定日登録(sp確定データ一覧);
            gridCtl = new GcSpreadGridController(sp確定データ一覧);

            this.MyCompany.Text1 = ccfg.自社コード.ToString();
            this.MyCompany.Text1IsReadOnly = (ccfg.自社販社区分 != 0);

            ScreenClear();

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
                    case GET_LIST_SEARCH:
                        if (tbl == null)
                        {
                            this.sp確定データ一覧.ItemsSource = null;
                            this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
                            return;
                        }
                        else
                        {
                            if (tbl.Rows.Count > 0)
                            {
                                SearchList = tbl;

                                // 確定ボタンを連結
                                var spanList = tbl.AsEnumerable()
                                                .GroupBy(g => new{ 
                                                                    jis = g.Field<string>("自社名"),
                                                                    kbn = g.Field<int>("確定区分"),
                                                                    closeDay = g.Field<int>("締日")})
                                                .Select(s => new
                                                {
                                                    day = s.Key,
                                                    count = s.Count()
                                                }).ToList();
                                
                                int rowNo = 0;
                                int cnt = 1;
                                var brsh = new BrushConverter();

                                foreach (var span in spanList)
                                {
                                    // 連結
                                    sp確定データ一覧[rowNo, (int)GridColumnsMapping.締日].RowSpan = span.count;
                                    sp確定データ一覧[rowNo, (int)GridColumnsMapping.確定日].RowSpan = span.count;
                                    sp確定データ一覧[rowNo, (int)GridColumnsMapping.確定ボタン].RowSpan = span.count;
                                    
                                    // 背景色設定
                                    if (cnt % 2 == 0)
                                    {
                                        sp確定データ一覧[rowNo, (int)GridColumnsMapping.締日].Background = new SolidColorBrush(Colors.White);
                                        sp確定データ一覧[rowNo, (int)GridColumnsMapping.確定日].Background = new SolidColorBrush(Colors.White);
                                        sp確定データ一覧[rowNo, (int)GridColumnsMapping.確定ボタン].Background = new SolidColorBrush(Colors.White);
                                    }
                                    else
                                    {
                                        sp確定データ一覧[rowNo, (int)GridColumnsMapping.締日].Background = (SolidColorBrush)brsh.ConvertFrom("#FFEAF1DD");
                                        sp確定データ一覧[rowNo, (int)GridColumnsMapping.確定日].Background = (SolidColorBrush)brsh.ConvertFrom("#FFEAF1DD"); ;
                                        sp確定データ一覧[rowNo, (int)GridColumnsMapping.確定ボタン].Background = (SolidColorBrush)brsh.ConvertFrom("#FFEAF1DD");
                                    }
                                    rowNo += span.count;
                                    cnt++;
                                }
                                
                                gridCtl.SetCellFocus(0, 0);
                                gridCtl.ScrollShowCell(0, 0);
                            }
                            else
                            {
                                SearchList = null;
                                this.ErrorMessage = "指定された条件の請求データはありません。";
                            }
                        }
                        break;

                    case UPDATE:
                        MessageBoxResult result =
                            MessageBox.Show(
                                "登録が完了しました。",
                                "メッセージ",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                        if (result == MessageBoxResult.OK)
                        {
                            gridCtl.SetCellFocus(0, 0);
                        }
                        
                        break;

                }

            }
            catch (Exception ex)
            {
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
            this.ErrorMessage = (string)message.GetResultData();
        }

        #endregion

        #region << リボン >>

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

        /// <summary>
        /// F11　リボン終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            this.Close();
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
            this.sp確定データ一覧.RowCount = 0;
            this.rdo取引区分.Text = "0";

            ResetAllValidation();
            SetFocusToTopControl();

        }
        #endregion

        #region 確定テーブル更新
        /// <summary>
        /// 確定テーブル更新
        /// </summary>
        /// <param name="自社コード"></param>
        /// <param name="確定区分">確定区分</param>
        /// <param name="締日">締日</param>
        /// <param name="確定日">確定日</param>
        /// <param name="取引区分">取引区分</param>
        public void UpdateTableData(string 自社コード, string 確定区分, string 締日, string 確定日, string 取引区分)
        {
            base.SendRequest(
                   new CommunicationObject(
                       MessageType.RequestData,
                       UPDATE,
                       new object[] {
                           自社コード,
                           取引区分,
                           確定区分,
                           締日,
                           確定日,
                           ccfg.ユーザID
                       }));
        }
        #endregion

        #endregion

        #region << コントロールイベント群 >>

        #region 検索ボタンクリック
        /// <summary>
        /// 検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.CheckAllValidation() != true)
                {
                    MessageBox.Show("入力エラーがあります。");
                    return;
                }

                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        GET_LIST_SEARCH,
                        MyCompany.Text1,
                        ClosingDate.Text,
                        rdo取引区分.Text
                        ));

            }
            catch (Exception)
            {
                return;
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
            //this.請求書一覧データ = null;
            SearchList = null;
            sp確定データ一覧.InputBindings.Clear();

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            frmcfg.spConfig = AppCommon.SaveSpConfig(this.sp確定データ一覧);
            ucfg.SetConfigValue(frmcfg);

        }
        #endregion

        #endregion

        

    }

}
