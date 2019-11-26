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
using System.Data;
using System.Data.SqlClient;

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;

namespace KyoeiSystem.Application.Windows.Views
{
    using WinFormsScreen = System.Windows.Forms.Screen;

    /// <summary>
    /// 自社品番検索
    /// </summary>
    public partial class SCHM09_MYHIN : WindowMasterSearchBase
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
        public class ConfigSCHM09_MYHIN : FormConfigBase
        {
            //public int Combo { get; set; }
            public int Combo_Copy { get; set; }
        }
        /// ※ 必ず public で定義する。
        public ConfigSCHM09_MYHIN frmcfg = null;

        #endregion

        #region 定数定義

        /// <summary>品番マスタデータリスト取得</summary>
        private const string TabelNmList = "M09_HIN_getNamedDataList";

        // === 品番テーブルカラム名 ===
        private const string COLUMN_CODE = "品番コード";
        private const string COLUMN_MY_CODE = "自社品番";
        private const string COLUMN_MY_NAME = "自社品名";
        private const string COLUMN_BRAND = "ブランド";
        private const string COLUMN_SERIES = "シリーズ";
        private const string COLUMN_ITEM_CLASS = "商品形態分類";

        #endregion

        #region バインド用プロパティ
        //データグリッドバインド用データテーブル
        private DataTable _SearchResult = null;
        public DataTable SearchResult
        {
            get { return this._SearchResult; }
            set
            {
                this._SearchResult = value;
                NotifyPropertyChanged();
            }
        }

        private int _multi = 0;
        public int multi
        {
            get { return this._multi; }
            set { this._multi = value; NotifyPropertyChanged(); }
        }

        public string SelectedCodeList = string.Empty;
        private bool _multiSelect = false;
        public bool MultiSelect
        {
            get { return this._multiSelect; }
            set
            {
                this._multiSelect = value;
                if (value == true)
                {
                    this.SearchGrid.SelectionMode = DataGridSelectionMode.Extended;
                }
                else
                {
                    this.SearchGrid.SelectionMode = DataGridSelectionMode.Single;
                }
            }
        }

        #endregion

        /// <summary>
        /// 取引区分
        ///   0:得意先
        ///   1:仕入先
        ///   2:加工先
        ///   3:相殺
        ///   4:販社
        /// </summary>
        /// <remarks>
        /// 返却する単価の識別に使用
        /// </remarks>
        public int 取引区分 { get; set; }

        /// <summary>
        /// 商品形態分類の指定
        /// セット商品のみを選択可とするかどうか
        /// </summary>
        public bool IsItemStatusType { get; set; }

        /// <summary>選択行データ(M09_HIN)</summary>
        public DataRow SelectedRowData { get; set; }

        #region クラス変数定義

        private int? _取引先コード = null;
        private int? _枝番 = null;

        #endregion


        #region SCHM09_MYHIN
        /// <summary>
        /// 品番マスタ コンストラクタ
        /// </summary>
        public SCHM09_MYHIN()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Topmost = true;

            IsItemStatusType = false;

        }

        /// <summary>
        /// 品番マスタ コンストラクタ
        /// </summary>
        public SCHM09_MYHIN(bool isItemStatusType)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Topmost = true;

            IsItemStatusType = isItemStatusType;

        }

        /// <summary>
        /// 品番マスタ　コンストラクタ
        /// </summary>
        /// <param name="得意先コード"></param>
        /// <param name="枝番"></param>
        public SCHM09_MYHIN(int 得意先コード, int 枝番)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Topmost = true;

            _取引先コード = 得意先コード;
            _枝番 = 枝番;
            IsItemStatusType = false;

        }
        #endregion

        #region LOAD

        /// <summary>
        /// 画面表示後のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.OkButton.FontSize = 9;
            this.OkButton.Content = "\n\n\n選択(F11)";
            this.CancelButton.FontSize = 9;
            this.CancelButton.Content = "\n\n\n終了(F1)";

            AppCommon.SetutpComboboxList(this.OrderColumn, false);

            GridOutPut();
            this.OrderColumn.SelectionChanged += this.OrderColumn_SelectionChanged;

            if (TwinTextBox.LinkItem != null)
            {
                // 取引区分として値を設定
                int val = -1;
                if (int.TryParse(TwinTextBox.LinkItem.ToString(), out val))
                    取引区分 = val;
            }

            if (IsItemStatusType)
            {
                // SET商品のみを選択可とする
                chkItemClass_2.IsEnabled = false;
                chkItemClass_2.IsChecked = false;
                // No-279 Del Start
                //chkItemClass_3.IsEnabled = false;
                //chkItemClass_3.IsChecked = false;
                // No-279 Del End
                chkItemClass_4.IsEnabled = false;
                chkItemClass_4.IsChecked = false;

            }

            // 画面サイズをタスクバーをのぞいた状態で表示させる
            this.Height = WinFormsScreen.PrimaryScreen.WorkingArea.Size.Height;

            // メイン画面と子画面が被ることなく表示できるかチェック
            if (WinFormsScreen.PrimaryScreen.WorkingArea.Size.Width < 1024 + 342)
            {
                // 画面の左端に表示させる
                this.Left = WinFormsScreen.PrimaryScreen.WorkingArea.Size.Width - this.Width;
            }

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigSCHM09_MYHIN)ucfg.GetConfigValue(typeof(ConfigSCHM09_MYHIN));
            if (frmcfg == null)
            {
                frmcfg = new ConfigSCHM09_MYHIN();
                ucfg.SetConfigValue(frmcfg);
            }
            else
            {
                // 表示できるかチェック
                var WidthCHK = WinFormsScreen.PrimaryScreen.Bounds.Width - frmcfg.Left;
                if (WidthCHK > 10)
                {
                    this.Left = frmcfg.Left;
                }
                // 表示できるかチェック
                var HeightCHK = WinFormsScreen.PrimaryScreen.Bounds.Height - frmcfg.Top;
                if (HeightCHK > 10)
                {
                    this.Top = frmcfg.Top;
                }
                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;

                this.OrderColumn.SelectedIndex = frmcfg.Combo_Copy;

            }
            #endregion

        }
        #endregion

        #region GridOutPut
        /// <summary>
        /// データの取得
        /// </summary>
        private void GridOutPut()
        {
            // パラメータ辞書生成
            Dictionary<string, int?> paramDic = new Dictionary<string, int?>();
            paramDic.Add("コード", _取引先コード);
            paramDic.Add("枝番", _枝番);

            try
            {
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        TabelNmList,
                        new object[] {
                            paramDic
                        }));

            }
            catch { }

        }
        #endregion

        #region エラー表示
        /// <summary>
        /// データ取得エラー時処理
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            MessageBox.Show(ErrorMessage);
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
                case TabelNmList:
                    // データが取得できなかった場合は処理しない
                    if (tbl == null)
                        return;

                    DataRow[] drList = tbl.Select("原価 IS NOT NULL");
                    DataView dv;
                    if (drList.Count() > 0 && string.IsNullOrEmpty(this.txtCode.Text))
                    {
                        DataTable tblShiire = tbl.Clone();

                        foreach (DataRow dr in drList)
                        {
                            tblShiire.ImportRow(dr);
                        }
                        dv = convertTableDisplayList(tblShiire).AsDataView();
                    }
                    else
                    {
                        dv = convertTableDisplayList(tbl).AsDataView();
                    }
                    // 絞り込みフィルタ生成
                    StringBuilder sb = new StringBuilder();

                    // 品番
                    if (!string.IsNullOrEmpty(this.txtCode.Text))
                        //sb.AppendFormat("( 品番コード = {0} OR 自社品番 LIKE '%{1}%' )", this.txtCode.Text, this.txtCode.Text);
                        sb.AppendFormat("( 自社品番 LIKE '%{0}%' )", this.txtCode.Text);

                    // 品名
                    if (!string.IsNullOrEmpty(this.txtName.Text))
                        sb.AppendFormat("{0} {1} LIKE '%{2}%'", sb.Length == 0 ? string.Empty : " AND", COLUMN_MY_NAME, this.txtName.Text);

                    // ブランド
                    if (!string.IsNullOrEmpty(this.txtBrand.Text1))
                        sb.AppendFormat("{0} {1} = '{2}'", sb.Length == 0 ? string.Empty : " AND", COLUMN_BRAND, this.txtBrand.Text1);

                    // シリーズ
                    if (!string.IsNullOrEmpty(this.txtSeries.Text1))
                        sb.AppendFormat("{0} {1} = '{2}'", sb.Length == 0 ? string.Empty : " AND", COLUMN_SERIES, this.txtSeries.Text1);

                    // 商品形態分類
                    sb.Append(getItemClassFilterStrings(sb.Length > 0));

                    // フィルタリング設定
                    dv.RowFilter = sb.ToString();

                    switch (OrderColumn.SelectedIndex)
                    {
                        case 0:     // 品番
                        default:
                            dv.Sort = COLUMN_CODE;
                            break;
                        case 1:     // 自社品番
                            dv.Sort = COLUMN_MY_CODE;
                            break;
                        case 2:     // 自社品名
                            dv.Sort = COLUMN_MY_NAME;
                            break;
                    }

                    SearchResult = dv.ToTable();
                    SearchGrid.SelectedIndex = 0;
                    break;

                default:
                    break;

            }

        }

        /// <summary>
        /// 取得テーブルを表示項目のテーブルに変換して返す
        /// </summary>
        /// <param name="tbl"></param>
        /// <returns></returns>
        private DataTable convertTableDisplayList(DataTable tbl)
        {
            // 不要項目の削除
            // REMARKS:DBのカラム名を指定
            tbl.Columns.Remove("論理削除");
            tbl.Columns.Remove("削除日時");
            tbl.Columns.Remove("削除者");
            tbl.Columns.Remove("登録日時");
            tbl.Columns.Remove("登録者");
            tbl.Columns.Remove("最終更新日時");
            tbl.Columns.Remove("最終更新者");

            return tbl;

        }

        /// <summary>
        /// 商品形態分類のフィルタ条件を生成して返す
        /// </summary>
        /// <param name="isOtherFilter">他に条件が入力されているか</param>
        /// <returns></returns>
        private string getItemClassFilterStrings(bool isOtherFilter)
        {
            List<int> targetClassList = new List<int>();
            if (this.chkItemClass_1.IsChecked ?? false)
                targetClassList.Add(1);     // SET品

            if (this.chkItemClass_2.IsChecked ?? false)
                targetClassList.Add(2);     // 単品

            if (this.chkItemClass_3.IsChecked ?? false)
                targetClassList.Add(3);     // 材料

            if (this.chkItemClass_4.IsChecked ?? false)
                targetClassList.Add(4);     // 副資材

            if (targetClassList.Count > 0)
            {
                StringBuilder wk = new StringBuilder();
                wk.AppendFormat("{0} {1} IN (", isOtherFilter ? " AND" : string.Empty, COLUMN_ITEM_CLASS);
                foreach (int code in targetClassList)
                    wk.AppendFormat("{0},", code);

                return wk.ToString().TrimEnd(',') + ")";

            }

            return string.Empty;

        }
        #endregion

        #region ユーザ操作

        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            CloseDataSelected();
        }

        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            CancelButton_Click(sender, e);
        }

        private void OkButton_MouseEnter(object sender, MouseEventArgs e)
        {
            this.OkButton.FontSize = 12;
            this.OkButton.Content = "選択";
        }

        private void OkButton_MouseLeave(object sender, MouseEventArgs e)
        {
            this.OkButton.FontSize = 9;
            this.OkButton.Content = "\n\n\n選択(F11)";
        }

        private void CancelButton_MouseEnter(object sender, MouseEventArgs e)
        {
            this.CancelButton.FontSize = 12;
            this.CancelButton.Content = "閉じる";
        }

        private void CancelButton_MouseLeave(object sender, MouseEventArgs e)
        {
            this.CancelButton.FontSize = 9;
            this.CancelButton.Content = "\n\n\n終了(F1)";
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            CloseDataSelected();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (ucfg != null)
            {
                frmcfg.Top = this.Top;
                frmcfg.Left = this.Left;
                frmcfg.Height = this.Height;
                frmcfg.Width = this.Width;
                //frmcfg.Combo = this.OrderColumn.SelectedIndex;
                ucfg.SetConfigValue(frmcfg);
            }
            this.Close();
        }

        #endregion

        /// <summary>
        /// データを呼び出し画面に戻して閉じる
        /// </summary>
        private void CloseDataSelected()
        {
            if (this.SearchGrid.SelectedItems.Count == 0)
                return;

            this.SelectedCodeList = string.Empty;
            try
            {
                List<string> work = new List<string>();
                string delmtr = "";
                foreach (DataRowView row in this.SearchGrid.SelectedItems)
                {
                    this.SelectedCodeList += delmtr + (row as DataRowView).Row[COLUMN_MY_CODE].ToString();
                    delmtr = ",";
                    // シングルの場合なのでこちらに移動
                    if (this.SearchGrid.SelectionMode == DataGridSelectionMode.Single)
                    {
                        SelectedRowData = (row as DataRowView).Row;
                        this.TwinTextBox.Text1 = row[COLUMN_MY_CODE].ToString();
                        this.TwinTextBox.Text2 = row[COLUMN_MY_NAME].ToString();
                        // 取引区分に応じた単価を設定
                        string cost = "0";
                        switch(取引区分)
                        {
                            case 0:     // 得意先
                                cost = row["売価"].ToString();
                                break;

                            case 1:     // 仕入先
                                cost = row["原価"] == DBNull.Value ? row["マスタ原価"].ToString() : row["原価"].ToString();
                                break;

                            case 2:     // 加工先
                                cost = row["加工原価"].ToString();
                                break;

                            case 4:     // 販社
                                cost = row["卸値"].ToString();
                                break;

                            case 3:     // 相殺
                            default:
                                cost = string.Empty;
                                break;

                        }

                        this.TwinTextBox.Text3 = cost;
 
                    }

                }

            }
            catch (Exception)
            {
            }
            this.DialogResult = true;
            this.Close();

        }


        #region イベント処理

        /// <summary>
        /// グリッドダブルクリック時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CloseDataSelected();

        }

        /// <summary>
        /// 表示順コンボボックス選択変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderColumn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchResult == null)
                return;

            DataView view = new DataView(SearchResult);
            switch (OrderColumn.SelectedIndex)
            {
                case 0:     // 品番
                default:
                    view.Sort = COLUMN_CODE;
                    break;
                case 1:     // 自社品番
                    view.Sort = COLUMN_MY_CODE;
                    break;
                case 2:     // 自社品名
                    view.Sort = COLUMN_MY_NAME;
                    break;
            }

            SearchResult = view.ToTable();
            SearchGrid.SelectedIndex = 0;

        }

        /// <summary>
        /// 検索条件テキストが変更された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchContaints_cTextChanged(object sender, RoutedEventArgs e)
        {
            GridOutPut();
        }

        /// <summary>
        /// グリッド上でキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                CloseDataSelected();

        }

        #region MainWindow_Closed
        /// <summary>
        /// 画面が閉じられた時、データを保持する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            frmcfg.Combo_Copy = this.OrderColumn.SelectedIndex;
            ucfg.SetConfigValue(frmcfg);

        }
        #endregion

        /// <summary>
        /// 商品形態分類のチェックが変更された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkItemClass_CheckChanged(object sender, RoutedEventArgs e)
        {
            GridOutPut();
        }

        #endregion

    }

}

