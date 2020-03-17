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

namespace KyoeiSystem.Application.Windows.Views
{
    using WinFormsScreen = System.Windows.Forms.Screen;
    using KyoeiSystem.Application.Windows.Views.Common;
    using KyoeiSystem.Framework.Windows.Controls;

    /// <summary>
    /// 適正在庫入力 フォームクラス
    /// </summary>
    public partial class ZIK07010 : RibbonWindowViewBase
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
            単位1 = 8,
            最低数量 = 9,
            単位2 = 10,
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
        private const string GET_DATA = "ZIK07010_GetData";
        private const string UPDATE = "ZIK07010_Update";

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
        /// 適正在庫入力
        /// </summary>
        public ZIK07010()
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
                        this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                        return;
                    }

                    SearchResult = tbl;
                    this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;

                    // フォーカスをSPREADへ
                    spGridList.Focus();
                    spGridList.Focusable = true;
                    spGridList.ActiveCellPosition = new CellPosition(0, (int)GridColumnsMapping.適正数量);
                    // スクロールバーをもとの位置に戻す
                    spGridList.ShowCell(0, (int)GridColumnsMapping.適正数量); 
                    break;

                case UPDATE:
                    // 更新処理結果受信
                    MessageBox.Show(AppConst.SUCCESS_UPDATE, "登録完了", MessageBoxButton.OK, MessageBoxImage.Information);
                    // コントロール初期化
                    initSearchControl();
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
                        int[] disableItem = new[] { 3 };
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

        #region F9 登録
        /// <summary>
        /// F9 登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            bool isErr = false;
            if (SearchResult == null || SearchResult.Rows.Count == 0)
            {
                return;
            }

            // 編集有無のチェック
            if (!IsModify())
            {
                MessageBox.Show("適正数量または最低数量が変更されていません。");
                return;
            }

            // 登録数量チェック
            int idx = 0;
            foreach (var gridRow in spGridList.Rows)
            {
                // 明細部エラークリア
                gridRow.ValidationErrors.Clear();

                // 適正数量 < 最低数量
                if ((decimal)gridRow.Cells[(int)GridColumnsMapping.適正数量].Value < (decimal)gridRow.Cells[(int)GridColumnsMapping.最低数量].Value)
                {
                    gridRow.ValidationErrors.Add(new SpreadValidationError("最低数量が適正数量を超えています。", null, idx, GridColumnsMapping.最低数量.GetHashCode()));
                    isErr = true;
                }
                idx++;
            }

            if (isErr)
            {
                return;
            }
            
            // 登録処理呼び出し
            DataSet ds = new DataSet();
            ds.Tables.Add(SearchResult.Copy());

            base.SendRequest(
                new CommunicationObject(
                    MessageType.UpdateData,
                    UPDATE,
                    new object[] {
                        ds,
                        ccfg.ユーザID
                    }));
        }
        #endregion

        #region F10 入力取消
        /// <summary>
        /// F11　リボン　入力取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            if (SearchResult == null ||
                (SearchResult.Rows.Count == 0 &&
                string.IsNullOrEmpty(txt倉庫.Text1) == true &&
                string.IsNullOrEmpty(Product.Text1) == true &&
                cmbItemType.SelectedIndex == 0 &&
                string.IsNullOrEmpty(Brand.Text1) == true &&
                string.IsNullOrEmpty(Series.Text1) == true)
                )
            {
                return;
            }

            var yesno = MessageBox.Show("入力を取り消しますか？", "取消確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (yesno == MessageBoxResult.No)
                return;

            initSearchControl();
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
            if (SearchResult == null || SearchResult.Rows.Count == 0)
            {
                return;
            }

            // グリッドの編集有無のチェック
            if (IsModify())
            {
                var yesno = MessageBox.Show("編集中の数量を保存せずに終了してもよろしいですか？", 
                                            "終了確認",
                                            MessageBoxButton.YesNo,
                                            MessageBoxImage.Question, MessageBoxResult.No);

                if (yesno == MessageBoxResult.No)
                    return;
            }

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
            this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
            this.txt自社.Text1 = ccfg.自社コード.ToString();
            this.txt自社.IsEnabled = ccfg.自社販社区分 == 自社販社区分.自社.GetHashCode();
            this.txt倉庫.Text1 = string.Empty;
            this.Product.Text1 = string.Empty;
            this.cmbItemType.SelectedIndex = 0;
            this.ProductName.Text = string.Empty;
            this.Brand.Text1 = string.Empty;
            this.Series.Text1 = string.Empty;

            if (SearchResult != null)
            {
                SearchResult.Clear();
            }

        }
        #endregion

        #region 編集有無のチェック
        /// <summary>
        /// 編集有無のチェック
        /// </summary>
        /// <returns>true:編集あり/false:編集なし</returns>
        private bool IsModify()
        {
            // 明細部の編集中を確定
            spGridList.CommitCellEdit();
            GcSpreadGridController gridCtl = new GcSpreadGridController(spGridList);
            SearchResult.Rows[gridCtl.ActiveRowIndex].EndEdit();

            // 編集有無のチェック
            var mod = SearchResult.AsEnumerable()
                        .Where(w => w.RowState == DataRowState.Modified);

            if (mod.Any())
            {
                // 編集あり
                return true;
            }

            return false;
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

        #region スプレッドグリッドspGridList_PreviewKeyDownイベント処理
        /// <summary>
        /// spGridList_PreviewKeyDownイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spGridList_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Enterキー押下でフォーカスを右に移動する
            if (e.Key == Key.Enter)
            {
                GcSpreadGridController gridCtl = new GcSpreadGridController(spGridList);
                int rowIdx = gridCtl.ActiveRowIndex;
                if (gridCtl.ActiveColumnIndex == (int)GridColumnsMapping.適正数量)
                {
                    spGridList.ActiveCellPosition = new CellPosition(rowIdx, (int)GridColumnsMapping.適正数量 + 1);

                }

            }
        }
        #endregion

        #endregion


    }

}
