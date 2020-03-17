using GrapeCity.Windows.SpreadGrid;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using KyoeiSystem.Application.Windows.Views.Common;
using KyoeiSystem.Framework.Windows.Controls;

namespace KyoeiSystem.Application.Windows.Views
{
    using FwRepPreview = KyoeiSystem.Framework.Reports.Preview;
    using WinForms = System.Windows.Forms;
    using WinFormsScreen = System.Windows.Forms.Screen;

    /// <summary>
    /// 適正在庫問合せ フォームクラス
    /// </summary>
    public partial class ZIK08010 : RibbonWindowViewBase
    {
        #region << 列挙型定義 >>

        /// <summary>
        /// データグリッドの列定義
        /// </summary>
        private enum GridColumnsMapping : int
        {
            倉庫 = 0,
            倉庫名称 = 1,
            品番コード = 2,
            自社品番コード = 3,
            自社色コード = 4,
            自社品番 = 5,
            自社色 = 6,
            適正数量 = 7,
            最低数量 = 8,
            単位1 = 9,
            実在庫数量 = 10,
            単位2 = 11,
        }

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
        /// <summary>検索サービス定数</summary>
        private const string GET_DATA = "ZIK08010_GetData";

        #endregion

        #region レポート定義
        // 適正在庫問合せ
        const string ReportFileName = @"Files\ZIK\ZIK08010.rpt";
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
        public class ConfigZIK07010 : FormConfigBase
        {
            public byte[] spConfigZIK07010 = null;

        }

        /// ※ 必ず public で定義する。
        public ConfigZIK07010 frmcfg = null;

        public byte[] sp_Config = null;

        #endregion

        #region << バインド用プロパティ >>

        DataTable _searchResult;
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

        #region << フォーム初期処理 >>

        /// <summary>
        /// 適正在庫問合せ
        /// </summary>
        public ZIK08010()
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
            this.spGridList.Rows.Clear();
            this.sp_Config = AppCommon.SaveSpConfig(this.spGridList);

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            frmcfg = (ConfigZIK07010)ucfg.GetConfigValue(typeof(ConfigZIK07010));

            if (frmcfg == null)
            {
                frmcfg = new ConfigZIK07010();
				ucfg.SetConfigValue(frmcfg);
				frmcfg.spConfigZIK07010 = this.sp_Config;
			}
            else
            {
                // 表示できるかチェック
                var WidthCHK = WinFormsScreen.PrimaryScreen.Bounds.Width - frmcfg.Left;
                if (WidthCHK > 10)
                    this.Left = frmcfg.Left;

                // 表示できるかチェック
                var HeightCHK = WinFormsScreen.PrimaryScreen.Bounds.Height - frmcfg.Top;
                if (HeightCHK > 10)
                    this.Top = frmcfg.Top;

                this.Width = frmcfg.Width;
                this.Height = frmcfg.Height;

            }
            #endregion

            base.MasterMaintenanceWindowList.Add("M70_JIS", new List<Type> { typeof(MST16010), typeof(SCHM70_JIS) });
            base.MasterMaintenanceWindowList.Add("M22_SOUK", new List<Type> { typeof(MST12020), typeof(SCHM22_SOUK) });
            base.MasterMaintenanceWindowList.Add("M09_MYHIN", new List<Type> { typeof(MST02010), typeof(SCHM09_MYHIN) });
            base.MasterMaintenanceWindowList.Add("M14_BRAND", new List<Type> { typeof(MST04020), typeof(SCHM14_BRAND) });
            base.MasterMaintenanceWindowList.Add("M15_SERIES", new List<Type> { typeof(MST04021), typeof(SCHM15_SERIES) });

            AppCommon.SetutpComboboxList(this.cmbItemType, false);

            initSearchControl();

            // 初期フォーカスを設定
            this.txt自社.SetFocus();
            ResetAllValidation();

        }

        #endregion

        #region 明細クリック時のアクション定義
        /// <summary>
        /// 明細クリック時のアクション定義
        /// </summary>
        public class cmd売上詳細表示 : ICommand
        {
            private GcSpreadGrid _gcSpreadGrid;
            public cmd売上詳細表示(GcSpreadGrid gcSpreadGrid)
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
            }
        }
        #endregion

        #region データ受信エラー
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            this.Message = base.ErrorMessage;
            base.SetFreeForInput();
        }

        #endregion

        #region << データ受信メソッド >>

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

                case GET_DATA:
                    // 検索処理結果受信
                    base.SetFreeForInput();
                    if(tbl == null || tbl.Rows.Count == 0)
                    {
                        this.ErrorMessage = "該当するデータが見つかりません";
                        SearchResult = null;
                        return;
                    }

                    SearchResult = tbl;

                    // グリッド文字色の変更
                    SetGridColor();
                    
                    // フォーカスをSPREADへ
                    spGridList.Focus();
                    spGridList.Focusable = true;
                    spGridList.ActiveCellPosition = new CellPosition(0, (int)GridColumnsMapping.倉庫);
                    // スクロールバーをもとの位置に戻す
                    spGridList.ShowCell(0, (int)GridColumnsMapping.倉庫); 
                    break;

                default:
                    break;

            }

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
				var ctl = FocusManager.GetFocusedElement(this);
                var uTwin = ViewBaseCommon.FindVisualParent<UcLabelTwinTextBox>(ctl as UIElement);

                if (uTwin == null)
                {
                    ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);

                }
                else
                {
                    // 商品コード指定
                    if (uTwin.DataAccessName == "M09_MYHIN")
                    {
                        // 雑コード非表示
                        int[] disableItem = new []{ 3 };
                        SCHM09_MYHIN myhin = new SCHM09_MYHIN(disableItem);
                        myhin.TwinTextBox = uTwin;
                        if (myhin.ShowDialog(this) == true)
                        {
                            this.Product.Text1 = myhin.SelectedRowData["自社品番"].ToString();
                            this.Product.Text2 = myhin.SelectedRowData["自社品名"].ToString();
                        }
                    }
                    else
                    {
                        ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);
                    }

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
            if (this.SearchResult == null)
                return;

            if (this.SearchResult.Rows.Count == 0)
            {
                this.ErrorMessage = "印刷データがありません。";
                return;
            }

            if (this.spGridList.ActiveCellPosition.Row < 0)
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
                // 不要な行を削除する
                DataTable csvDt = SearchResult.Copy();
                csvDt.Columns.Remove("str品番コード");

                // CSVファイル出力
                CSVData.SaveCSV(csvDt, sfd.FileName, true, true, false, ',', true);
                MessageBox.Show("CSVファイルの出力が完了しました。");

            }

        }
        #endregion 

        #region F8 印刷
        /// <summary>
        /// F8 印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            if (this.SearchResult == null)
                return;

            if (this.SearchResult.Rows.Count == 0)
            {
                this.ErrorMessage = "印刷データがありません。";
                return;
            }

            try
            {
                int selItemType = int.Parse(cmbItemType.SelectedValue.ToString());
                Dictionary<int, string> itemTypeDic = new Dictionary<int, string>();
                itemTypeDic.Add(0, "指定なし");
                itemTypeDic.Add(1, "食品");
                itemTypeDic.Add(2, "繊維");
                itemTypeDic.Add(3, "その他");

                int condItem = int.Parse(rdo表示条件.Text);
                Dictionary<int, string> 表示条件Dic = new Dictionary<int, string>();
                表示条件Dic.Add(0, "なし");
                表示条件Dic.Add(1, "適正数量に満たない商品または過剰数量");
                表示条件Dic.Add(2, "最低数量に満たない商品");

                base.SetBusyForInput();
                var param = new List<Framework.Reports.Preview.ReportParameter>()
				{
                    new FwRepPreview.ReportParameter() { PNAME = "自社コード", VALUE = txt自社.Text2 },
                    new FwRepPreview.ReportParameter() { PNAME = "倉庫名", VALUE = string.IsNullOrEmpty(txt倉庫.Text2) ? "" : txt倉庫.Text2 },
                    new FwRepPreview.ReportParameter() { PNAME = "商品コード", VALUE = string.IsNullOrEmpty(Product.Text2) ? "" : Product.Text2  },
                    new FwRepPreview.ReportParameter() { PNAME = "商品分類", VALUE = itemTypeDic[selItemType] },
                    new FwRepPreview.ReportParameter() { PNAME = "商品名指定", VALUE = ProductName.Text },
                    new FwRepPreview.ReportParameter() { PNAME = "ブランド", VALUE = string.IsNullOrEmpty(Brand.Text2) ? "" : Brand.Text2 },
                    new FwRepPreview.ReportParameter() { PNAME = "シリーズ", VALUE = string.IsNullOrEmpty(Series.Text2) ? "" : Series.Text2 },
                    new FwRepPreview.ReportParameter() { PNAME = "表示条件", VALUE = 表示条件Dic[condItem] },
                };

                FwRepPreview.ReportPreview view = new FwRepPreview.ReportPreview();

                DataTable 印刷データ = SearchResult.Copy();
                印刷データ.TableName = "適正在庫問合せ";

                view.MakeReport(印刷データ.TableName, ReportFileName, 0, 0, 0);
                view.SetReportData(印刷データ);

                base.SetFreeForInput();

                view.PrinterName = frmcfg.PrinterName;
                view.SetupParmeters(param);
                view.ShowPreview();
                view.Close();

                frmcfg.PrinterName = view.PrinterName;

            }
            catch (Exception ex)
            {
                base.SetFreeForInput();
                this.ErrorMessage = "システムエラーが発生しました。サポートにお問い合わせください。";
                appLog.Error("商品在庫問合せ一覧の印刷時に例外が発生しました。", ex);

            }
        }
        #endregion

        #region F11 終了
        /// <summary>
        /// F11　リボン　終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF11Key(object sender, KeyEventArgs e )
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

            if (!base.CheckAllValidation())
            {
                this.ErrorMessage = "入力内容に誤りがあります。";
                MessageBox.Show("入力内容に誤りがあります。");
                return;
            }

            // REMARKS:条件が増える場合は下記に追加する
            Dictionary<string, string> cond = new Dictionary<string, string>();
            cond.Add("自社品番", Product.Text1);
            cond.Add("検索品名", ProductName.Text);
            cond.Add("商品分類", cmbItemType.SelectedValue.ToString());
            cond.Add("ブランド", Brand.Text1);
            cond.Add("シリーズ", Series.Text1);
            cond.Add("表示条件", rdo表示条件.Text);

            base.SendRequest(
                new CommunicationObject(
                    MessageType.RequestData,
                    GET_DATA,
                    new object[] {
                        txt自社.Text1,
                        txt倉庫.Text1,
                        cond
                    }));

            base.SetBusyForInput();

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
            if (ucfg != null)
            {
                if (frmcfg == null) { frmcfg = new ConfigZIK07010(); }
                frmcfg.Top = this.Top;
                frmcfg.Left = this.Left;
                frmcfg.Width = this.Width;
                frmcfg.Height = this.Height;
                frmcfg.spConfigZIK07010 = AppCommon.SaveSpConfig(this.spGridList);
                ucfg.SetConfigValue(frmcfg);
                spGridList.InputBindings.Clear();

            }

        }
        #endregion

        #region 画面項目の初期化
        /// <summary>
        /// 画面項目の初期化
        /// </summary>
        private void initSearchControl()
        {
            this.txt自社.Text1 = ccfg.自社コード.ToString();
            this.txt自社.IsEnabled = ccfg.自社販社区分 == 自社販社区分.自社.GetHashCode();
            this.txt倉庫.Text1 = string.Empty;
            this.Product.Text1 = string.Empty;
            this.cmbItemType.SelectedIndex = 0;
            this.ProductName.Text = string.Empty;
            this.Brand.Text1 = string.Empty;
            this.Series.Text1 = string.Empty;
            this.rdo表示条件.Text = "0";

            if (SearchResult != null)
            {
                SearchResult.Clear();
            }

        }
        #endregion

        #region グリット文字色変更
        /// <summary>
        /// グリット文字色変更
        /// </summary>
        private void SetGridColor()
        {
            
            foreach (var row in spGridList.Rows)
            {
                decimal d適正数量 = (decimal)row.Cells[(int)GridColumnsMapping.適正数量].Value;
                decimal d最低数量 = (decimal)row.Cells[(int)GridColumnsMapping.最低数量].Value;
                decimal d実在庫数量 = (decimal)row.Cells[(int)GridColumnsMapping.実在庫数量].Value;

                // 適正数量に満たない商品または
                // 適正数量×2を超える商品
                // 最低数量に満たない商品
                if ( d実在庫数量 < d適正数量 ||
                     d実在庫数量 > d適正数量 * 2 ||
                     d実在庫数量 < d最低数量)
                {
                    row.Cells[(int)GridColumnsMapping.実在庫数量].Foreground = System.Windows.Media.Brushes.Red;
                }
            }
        }
        #endregion

        #region コントロール　イベント
        #region スプレッドグリッドからフォーカスが外れた時のイベント処理
        /// <summary>
        /// スプレッドグリッドからフォーカスが外れた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spGridList_LostFocus(object sender, RoutedEventArgs e)
        {
            if (spGridList.Focusable == true)
            {
                spGridList.SelectionBorder.Style = BorderLineStyle.Thick;
                spGridList.SelectionBorderUnfocused.Style = BorderLineStyle.None;
            }
            else
            {
                spGridList.SelectionBorder.Style = BorderLineStyle.None;
                spGridList.SelectionBorderUnfocused.Style = BorderLineStyle.None;
            }
        }
        #endregion

        #region 商品名指定_PreviewKeyDown
        /// <summary>
        /// ShouhinmeiSitei_PreviewKeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShouhinmeiSitei_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var ctl = sender as Framework.Windows.Controls.UcLabelTextBox;
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
        #endregion

        #endregion

    }

}
