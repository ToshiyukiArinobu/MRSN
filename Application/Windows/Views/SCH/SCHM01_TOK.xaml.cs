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
    /// 取引先検索
    /// </summary>
    public partial class SCHM01_TOK : WindowMasterSearchBase
    {
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
        public class ConfigSCHM01_TOK : FormConfigBase
        {
            //public int Combo { get; set; }
            public int Combo_Copy { get; set; }
        }
        /// ※ 必ず public で定義する。
        public ConfigSCHM01_TOK frmcfg = null;

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

        #region 定数定義

        /// <summary>取引先マスタデータリスト取得</summary>
        private const string TabelNmList = "M01_TOK_getDataList";

        // TODO:テーブルの定義が古そうなので変わる可能性アリ
        private const string COLUM_ID = "取引先コード";
        private const string COLUM_NAME1 = "得意先名１";
        private const string COLUM_NAME2 = "得意先名２";
        private const string COLUM_KANA = "かな読み";
        private const string COLUM_SUP_KBN = "取引区分";

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
        private string _名称 = string.Empty;
        public string 名称
        {
            get { return this._名称; }
            set { this._名称 = value; NotifyPropertyChanged(); }
        }
        private string _コード = string.Empty;
        public string コード
        {
            get { return this._コード; }
            set { this._コード = value; NotifyPropertyChanged(); }
        }

        private int _支払消費税区分;
        public int 支払消費税区分 { get { return _支払消費税区分; } }

        private int _税区分;
        public int 税区分 { get { return _税区分; } }

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

        private int _マルセン追加フラグ = 0;
        public int マルセン追加フラグ
        {
            get { return this._マルセン追加フラグ; }
            set { this._マルセン追加フラグ = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region SCHM01_TOK
        /// <summary>
        /// 取引先マスタ コンストラクタ
        /// </summary>
        public SCHM01_TOK()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Topmost = true;
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
            ucfg = AppCommon.GetConfig(this);

            // 権限設定を呼び出す(ucfgを取得した後のに入れる)
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));

            this.OkButton.FontSize = 9;
            this.OkButton.Content = "\n\n\n選択(F11)";
            this.CancelButton.FontSize = 9;
            this.CancelButton.Content = "\n\n\n終了(F1)";

            // 呼び出し元から表示区分を取ってくる。
            if (this.TwinTextBox.LinkItem != null)
            {
                // No-268 Mod Start
                // linkitemがリストかどうか判定し取引区分を取得
                string s取引区分;
                if (this.TwinTextBox.LinkItem is string[])
                {
                    int linkAddVal = 0;
                    string[] linkItemList = (this.TwinTextBox.LinkItem as string[]);
                    s取引区分 = linkItemList[0].ToString();
                    マルセン追加フラグ = int.TryParse(linkItemList[1].ToString(), out linkAddVal) ? linkAddVal : 0;
                }
                else
                {
                    s取引区分 = this.TwinTextBox.LinkItem.ToString();
                }

                string[] ary = s取引区分.Split(',');
                // No-268 Mod End

                if (ary.Length <= 1)
                {
                    // TwinTextLinkItem設定
                    int linkVal = 9;
                    if (int.TryParse(ary[0], out linkVal))
                        this.cmbDealings.Combo_IsEnabled = false;

                    AppCommon.SetutpComboboxList(this.cmbDealings, false);
                    this.cmbDealings.SelectedValue = linkVal;

                }
                else
                {
                    // 複数指定してある場合はパラメータを再設定して再取得する
                    this.cmbDealings.ComboListingParams = getComboListingParams_tradingKbn(ary);
                    AppCommon.SetutpComboboxList(this.cmbDealings, false);

                }

            }
            else if (multi == 1)
            {
                // TODO:現状マルチのパターンが不明の為一旦コメントアウト
                //this.cmbDealings.SelectedValue = (int)取引区分;
            }
            else
            {
                AppCommon.SetutpComboboxList(this.cmbDealings, false);
                // デフォルト値(全取引先)を設定
                this.cmbDealings.SelectedValue = 9;
            }

            // No-291 Mod Start
            AppCommon.SetutpComboboxList(this.OrderColumn, false);            

            // 呼び出し時にコードが設定されている場合は初期値として設定する
            if (!string.IsNullOrEmpty(this.TwinTextBox.Text1))
                コード = this.TwinTextBox.Text1;
            // No-291 Mod End

            GridOutPut();
            this.OrderColumn.SelectionChanged += this.OrderColumn_SelectionChanged;

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
            frmcfg = (ConfigSCHM01_TOK)ucfg.GetConfigValue(typeof(ConfigSCHM01_TOK));
            if (frmcfg == null)
            {
                frmcfg = new ConfigSCHM01_TOK();
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
            try
            {
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        TabelNmList,
                        new object[] {
                            this.cmbDealings.SelectedValue,
                            ccfg.自社コード,
                            マルセン追加フラグ
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

                    DataView dv = convertTableDisplayList(tbl).AsDataView();
                    int selectKbn = (int)this.cmbDealings.SelectedValue;

                    // 絞り込みフィルタ生成
                    StringBuilder sb = new StringBuilder();
                    if (!string.IsNullOrEmpty(名称))
                    {
                        sb.Append("(");
                        sb.AppendFormat("得意先名１ LIKE '%{0}%'", 名称);
                        sb.AppendFormat(" OR 得意先名２ LIKE '%{0}%'", 名称);
                        sb.AppendFormat(" OR 略称名 LIKE '%{0}%'", 名称);
                        sb.AppendFormat(" OR かな読み LIKE '%{0}%'", 名称);
                        sb.Append(")");

                    }

                    if (!string.IsNullOrEmpty(コード))
                    {
                        sb.AppendFormat("{0} 取引先コード = {1}", sb.Length == 0 ? string.Empty : " AND", コード);
                    }

                    if (selectKbn < 9)
                    {
                        // No-268 Mod Start
                        // 取引区分=9(全取引)より下でフィルタ処理
                        sb.AppendFormat("{0} ( 取引区分 = {1}", sb.Length == 0 ? string.Empty : " AND", selectKbn);

                        if (selectKbn == 1 && マルセン追加フラグ == 1)
                        {
                            sb.AppendFormat(" OR  自社区分 = {0}", (int)自社販社区分.自社);
                        }
                        sb.Append(")");
                        // No-268 Mod End
                    }

                    // フィルタリング設定
                    dv.RowFilter = sb.ToString();

                    switch (OrderColumn.SelectedIndex)
                    {
                        case 0:     // コード
                        default:
                            dv.Sort = COLUM_ID;
                            break;
                        case 1:     // 名称
                            dv.Sort = COLUM_NAME1;
                            break;
                        case 2:     // かな読み
                            dv.Sort = COLUM_KANA;
                            break;
                    }

                    SearchResult = dv.ToTable();

                    // No-268 Mod Start
                    int jisKbnIndex = SearchResult.Columns.IndexOf("自社区分");
                    SearchGrid.Columns[jisKbnIndex].Visibility = System.Windows.Visibility.Hidden;
                    // No-268 Mod End

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
                    this.SelectedCodeList += delmtr + (row as DataRowView).Row[COLUM_ID].ToString();
                    delmtr = ",";
                    // シングルの場合なのでこちらに移動
                    if (this.SearchGrid.SelectionMode == DataGridSelectionMode.Single)
                    {
                        this.TwinTextBox.Text1 = row[COLUM_ID].ToString();
                        this.TwinTextBox.Text2 = row["枝番"].ToString();// TODO:フォーカスがあたっていないと値が設定されない仕様っぽい？？
                        this.TwinTextBox.Text3 = row["略称名"] == null ? row["得意先名１"].ToString() : row["略称名"].ToString();      // No.199 Mod

                        // 他情報を設定
                        int ival;
                        this._支払消費税区分 = int.TryParse(row["Ｓ支払消費税区分"].ToString(), out ival) ? ival : 1;  // 1:一括、2:個別
                        this._税区分 = int.TryParse(string.Format("{0}", row["Ｓ税区分ID"]), out ival) ? ival : 9;       // 1：切捨て、2：四捨五入、3：切上げ、9：税なし

                    }

                }

            }
            catch (Exception)
            {
            }
            this.DialogResult = true;
            this.Close();

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
                case 0:     //コード
                default:
                    view.Sort = COLUM_ID;
                    break;
                case 1:     //名称
                    view.Sort = COLUM_NAME1;
                    break;
                case 2:     //かな読み
                    view.Sort = COLUM_KANA;
                    break;
            }

            SearchResult = view.ToTable();

            // No-268 Mod Start
            int jisKbnIndex = SearchResult.Columns.IndexOf("自社区分");
            SearchGrid.Columns[jisKbnIndex].Visibility = System.Windows.Visibility.Hidden;
            // No-268 Mod End

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
        /// グリッド上でキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                CloseDataSelected();

        }

        /// <summary>
        /// 取引区分が変更された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDealings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridOutPut();
        }

        #endregion

        /// <summary>
        /// 取引区分複数指定時のコンボリストパラメータを生成して返す
        /// </summary>
        /// <param name="ary"></param>
        /// <returns></returns>
        private string getComboListingParams_tradingKbn(string[] ary)
        {
            string param1 = "取引先マスタ";
            string param2 = "照会画面";
            string param3 = "取引区分";

            Array.Sort(ary);
            StringBuilder sb = new StringBuilder();
            foreach (string str in ary)
                sb.Append(str);

            switch (sb.ToString())
            {
                case "03":  // 得意先商品売価設定(得意先、相殺)
                    param3 = "得意先売価";
                    break;

                case "13":  // 仕入先商品売価設定(仕入先、相殺)
                    param3 = "仕入先売価";
                    break;

                case "23":  // 外注先商品売価設定(外注先、相殺)
                    param3 = "外注先売価";
                    break;

                case "034":  // 得意先商品売価設定(得意先、相殺、販社)
                    param3 = "得意先販社売価";
                    break;

                case "123":  // 仕入先外注先売価設定(仕入先、外注先、相殺)             // No.127 Add
                    param3 = "仕入先外注先売価";
                    break;

                case "1234":  // 仕入先外注先売価設定(仕入先、外注先、相殺、販社)      // No.207 Add
                    param3 = "仕入先外注先販社売価";
                    break;
                default:
                    break;
            }

            return string.Format("{0},{1},{2}", param1, param2, param3);

        }

    }

}

