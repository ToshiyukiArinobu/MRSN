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

    /// <summary>
    /// 大分類検索
    /// </summary>
    public partial class SCHM12_DAIBUNRUI : WindowMasterSearchBase
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
        public class ConfigSCH01010 : FormConfigBase
        {
            //public int Combo { get; set; }
            public int Combo_Copy { get; set; }
        }
        /// ※ 必ず public で定義する。
        public ConfigSCH01010 frmcfg = null;

        #endregion

        #region 定数定義

        /// <summary>データ検索キー</summary>
        private const string TabelNm = "M12_GetListData";

        /// <summary>列名 大分類コード</summary>
        private const string COLNAME_CODE = "大分類コード";
        /// <summary>列名 大分類名称</summary>
        private const string COLNAME_NAME = "大分類名";

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

        #region コンストラクタ

        /// <summary>
        /// 大分類検索 コンストラクタ
        /// </summary>
        public SCHM12_DAIBUNRUI()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Topmost = true;
        }

        #endregion

        #region LOAD

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.OkButton.FontSize = 9;
            this.OkButton.Content = "\n\n\n選択(F11)";
            this.CancelButton.FontSize = 9;
            this.CancelButton.Content = "\n\n\n終了(F1)";

            GridOutPut();

            // 画面サイズをタスクバーをのぞいた状態で表示させる
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;

            // メイン画面と子画面が被ることなく表示できるかチェック
            if (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width < 1024 + 342)
            {
                // 画面の左端に表示させる
                this.Left = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width;
            }

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigSCH01010)ucfg.GetConfigValue(typeof(ConfigSCH01010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigSCH01010();
                ucfg.SetConfigValue(frmcfg);
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
                this.Height = frmcfg.Height;
                this.Width = frmcfg.Width;

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
                // 大分類コード
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        TabelNm,
                        new object[] { }));
               
            }
            catch (Exception)
            {
                return;
            }

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
                case TabelNm:
                    tbl = getTrimColumns(tbl);
                    // Gridのバインド変数に代入
                    DataView view = new DataView(tbl);

                    StringBuilder sb = new StringBuilder();
                    if (!string.IsNullOrEmpty(this.txtKana.Text))
                        sb.AppendFormat("{0} LIKE '%{1}%'", COLNAME_NAME, this.txtKana.Text);

                    view.RowFilter = sb.ToString();
                    SearchResult = view.ToTable();
                    SearchGrid.SelectedIndex = -1;

                    break;
                
                default:
                    break;
            }
        }

        /// <summary>
        /// リストに表示不要な項目を削除する
        /// </summary>
        /// <param name="tbl"></param>
        /// <returns></returns>
        private DataTable getTrimColumns(DataTable tbl)
        {
            // 不要列を削除
            tbl.Columns.Remove("登録日時");
            tbl.Columns.Remove("登録者");
            tbl.Columns.Remove("最終更新日時");
            tbl.Columns.Remove("最終更新者");
            tbl.Columns.Remove("削除日時");
            tbl.Columns.Remove("削除者");

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
                    this.SelectedCodeList += delmtr + (row as DataRowView).Row[COLNAME_CODE].ToString();
                    delmtr = ",";
                    // シンググルの場合なのでこちらに移動
                    if (this.SearchGrid.SelectionMode == DataGridSelectionMode.Single)
                    {
                        this.TwinTextBox.Text1 = row[COLNAME_CODE].ToString();
                        this.TwinTextBox.Text2 = row[COLNAME_NAME].ToString();
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
        /// 絞込テキストが変更された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_cTextChanged(object sender, RoutedEventArgs e)
        {
            GridOutPut();
        }

        #region MainWindow_Closed
        //画面が閉じられた時、データを保持する
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion

        private void SearchGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CloseDataSelected();
            }
        }
    }
}

