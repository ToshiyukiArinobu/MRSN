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
    /// SCH_M16HINGUN.xaml の相互作用ロジック
    /// </summary>
    public partial class SCHM16_HINGUN : WindowMasterSearchBase
    {
        /// <summary>対象コードデータの取得</summary>
        private const string TargetTableNm = "M16_HINGUN_GetData";
        /// <summary>列名 商品群コード</summary>
        private const string COLUM_ID = "商品群コード";
        /// <summary>列名 商品群名</summary>
        private const string COLUM_NAME = "商品群名";
        /// <summary>列名 かな読み</summary>
        private const string COLUM_KANA = "かな読み";

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;

        //<summary>
        //画面固有設定項目のクラス定義
        //※ 必ず public で定義する。
        //</summary>
        public class ConfigSCH_M16HINGUN : FormConfigBase
        {
            //public bool[] 表示順方向 { get; set; }
            /// コンボボックスの位置
            public int Combo { get; set; }
        }

        /// ※ 必ず public で定義する。
        public ConfigSCH_M16HINGUN frmcfg = null;

        #endregion

        #region データテーブル設定項目

        // データグリッドバインド用データテーブル
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

        private string _商品群名 = string.Empty;
        public string 商品群名
        {
            get { return this._商品群名; }
            set { this._商品群名 = value; NotifyPropertyChanged(); }
        }
        private string _かな読み = string.Empty;
        public string かな読み
        {
            get { return this._かな読み; }
            set { this._かな読み = value; NotifyPropertyChanged(); }
        }
        private string _商品群コード = string.Empty;
        public string 商品群コード
        {
            get { return this._商品群コード; }
            set { this._商品群コード = value; NotifyPropertyChanged(); }
        }

        #endregion

        /// <summary>
        /// 商品群検索 コンストラクタ
        /// </summary>
        public SCHM16_HINGUN()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Topmost = true;
        }

        /// <summary>
        /// 画面読み込み後のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.OkButton.FontSize = 9;
            this.OkButton.Content = "\n\n\n選択(F11)";
            this.CancelButton.FontSize = 9;
            this.CancelButton.Content = "\n\n\n終了(F1)";

            // 初期フォーカスの設定を行う
            this.txtName.Focus();
            GridOutPut();
            // 画面サイズをタスクバーをのぞいた状態で表示させる
            this.Height = WinFormsScreen.PrimaryScreen.WorkingArea.Size.Height;

            // メイン画面と子画面が被ることなく表示できるかチェック
            if (WinFormsScreen.PrimaryScreen.WorkingArea.Size.Width < 1024 + 342)
            {
                // 画面の左端に表示させる
                this.Left = WinFormsScreen.PrimaryScreen.WorkingArea.Size.Width - this.Width;
            }

            // コンボボックスのSelectionChangedを設定する。
            this.OrderColumn.SelectionChanged += this.OrderColumn_SelectionChanged;

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigSCH_M16HINGUN)ucfg.GetConfigValue(typeof(ConfigSCH_M16HINGUN));
            if (frmcfg == null)
            {
                frmcfg = new ConfigSCH_M16HINGUN();
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
                this.OrderColumn.SelectedIndex = frmcfg.Combo;

            }
            #endregion

            AppCommon.SetutpComboboxList(this.OrderColumn, false);

            #region 表示順序

            if (SearchResult == null)
            {
                return;
            }

            DataView view = new DataView(SearchResult);

            switch (OrderColumn.SelectedIndex)
            {
                case 0: // コード
                default:
                    view.Sort = COLUM_ID;
                    break;
                case 1: // 名称
                    view.Sort = COLUM_NAME;
                    break;
                case 2: // カナ
                    view.Sort = COLUM_KANA;
                    break;
            }

            SearchResult = view.ToTable();

            #endregion

        }

        /// <summary>
        /// データの取得
        /// </summary>
        private void GridOutPut()
        {
            try
            {
                // 商品群マスタ
                // REMARKS:オプション=nullにする事で全データを取得
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 商品群コード, null }));

            }
            catch (Exception)
            {
                return;
            }

        }

        /// <summary>
        /// データ取得エラー時処理
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            MessageBox.Show(ErrorMessage);
        }

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
                case TargetTableNm:

                    DataView dv = new DataView(tbl);
                
                    // 絞り込みフィルタ生成
                    StringBuilder sb = new StringBuilder();
                    if (!string.IsNullOrEmpty(商品群名))
                    {
                        sb.AppendFormat("商品群名 LIKE '%{0}%'", 商品群名);
                    }

                    if (!string.IsNullOrEmpty(かな読み))
                    {
                        sb.AppendFormat("{0} かな読み LIKE '%{1}%'", sb.Length == 0 ? string.Empty : " AND", かな読み);
                    }

                    if (!string.IsNullOrEmpty(商品群コード))
                    {
                        sb.AppendFormat("{0} 商品群コード LIKE '%{1}%'", sb.Length == 0 ? string.Empty : " AND", 商品群コード);
                    }

                    // フィルタリング設定
                    dv.RowFilter = sb.ToString();

                    switch (OrderColumn.SelectedIndex)
                    {
                        case 0: //コード
                        default:
                            dv.Sort = COLUM_ID;
                            break;
                        case 1: //名称
                            dv.Sort = COLUM_NAME;
                            break;
                        case 2:　//かな読み
                            dv.Sort = COLUM_KANA;
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
        /// F1 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// F11 選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            SetDataTwinText();
        }

        /// <summary>
        /// 検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GridOutPut();
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

        /// <summary>
        /// 選択ボタンが押下された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            SetDataTwinText();
        }

        /// <summary>
        /// 閉じるボタンが押下された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// グリッドダブルクリック時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SetDataTwinText();
        }

        /// <summary>
        /// データを呼び出し画面に戻して閉じる
        /// </summary>
        private void SetDataTwinText()
        {
            if (SearchGrid.SelectedIndex < 0)
            {
                return;
            }

            this.TwinTextBox.Text1 = SearchResult.Rows[SearchGrid.SelectedIndex][COLUM_ID].ToString();
            this.TwinTextBox.Text2 = SearchResult.Rows[SearchGrid.SelectedIndex][COLUM_NAME].ToString();
            this.DialogResult = true;
            this.Close();
        }

        /// <summary>
        /// 並び替えコンボ変更時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderColumn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchResult == null)
            {
                return;
            }

            DataView view = new DataView(SearchResult);

            switch (OrderColumn.SelectedIndex)
            {
                case 0: //コード
                default:
                    view.Sort = COLUM_ID;
                    break;
                case 1: //名称
                    view.Sort = COLUM_NAME;
                    break;
                case 2:　//かな読み
                    view.Sort = COLUM_KANA;
                    break;
            }

            SearchResult = view.ToTable();
            SearchGrid.SelectedIndex = 0;

        }

        /// <summary>
        /// 条件テキスト変更時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_cTextChanged(object sender, RoutedEventArgs e)
        {
            GridOutPut();
        }

        #region Mindoow_Closed
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
            frmcfg.Combo = this.OrderColumn.SelectedIndex;
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
            {
                SetDataTwinText();
            }

        }

        /// <summary>
        /// データを呼び出し画面に戻して閉じる
        /// </summary>
        private void CloseDataSelected()
        {
            if (this.SearchGrid.SelectedItems.Count == 0)
            {
                return;
            }

            this.SelectedCodeList = string.Empty;
            try
            {
                List<string> work = new List<string>();
                string delmtr = "";
                foreach (DataRowView row in this.SearchGrid.SelectedItems)
                {
                    this.SelectedCodeList += delmtr + (row as DataRowView).Row[COLUM_ID].ToString();
                    delmtr = ",";
                }

                this.TwinTextBox.Text1 = SearchResult.Rows[SearchGrid.SelectedIndex][COLUM_ID].ToString();
                this.TwinTextBox.Text2 = SearchResult.Rows[SearchGrid.SelectedIndex][COLUM_NAME].ToString();
            }
            catch (Exception)
            {
            }
            this.DialogResult = true;
            this.Close();

        }

    }

}
