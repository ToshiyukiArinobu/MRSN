using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace KyoeiSystem.Application.Windows.Views
{
    using WinFormsScreen = System.Windows.Forms.Screen;

    /// <summary>
    /// SCHM70_JIS.xaml の相互作用ロジック
    /// </summary>
    public partial class SCHM70_JIS : WindowMasterSearchBase
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
        public class ConfigSCHM70_JIS : FormConfigBase
        {
            public int Combo { get; set; }

        }
        /// ※ 必ず public で定義する。
        public ConfigSCHM70_JIS frmcfg = null;

        #endregion

        #region << 定数定義 >>

        private const string TableNmList = "M70_JIS_getDataList";          //No.384 Add
        //private const string TableHanNm = "M70_JIS_GetHanList";
        private const string COLUM_ID = "自社コード";
        private const string COLUM_NAME = "自社名";
        private const string COLUM_REPRESENTATIVE_NAME = "代表者名";

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

        private int _表示順 = 0;
        private string _コード = string.Empty;
        private string _名称 = string.Empty;
        private string _代表者名 = string.Empty;

        public int 表示順
        {
            get { return this._表示順; }
            set { this._表示順 = value; NotifyPropertyChanged(); }
        }
        public string コード
        {
            get { return this._コード; }
            set { this._コード = value; NotifyPropertyChanged(); }
        }
        public string 名称
        {
            get { return this._名称; }
            set { this._名称 = value; NotifyPropertyChanged(); }
        }
        public string 代表者名
        {
            get { return this._代表者名; }
            set { this._代表者名 = value; NotifyPropertyChanged(); }
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

        #region << 画面初期表示処理 >>

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SCHM70_JIS()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Topmost = true;
        }

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

            this.txtCD.Focus();

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

            AppCommon.SetutpComboboxList(this.OrderColumn, false);

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigSCHM70_JIS)ucfg.GetConfigValue(typeof(ConfigSCHM70_JIS));
            if (frmcfg == null)
            {
                frmcfg = new ConfigSCHM70_JIS();
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

        }

        #endregion

        #region 表示データ取得
        /// <summary>
        /// データの取得
        /// </summary>
        private void GridOutPut()
        {
            string code = string.Empty;
            
            try
            {
                //No.384 Mod Start
                List<int> exclusionJisList = new List<int>();
                int? 自社区分 = null;

                if (this.TwinTextBox.LinkItem is string[])
                {
                    // LinkItemがリストの場合
                    string[] linkItemList = (this.TwinTextBox.LinkItem as string[]);


                    // [0]:自社区分を取得(販社の場合のみ条件設定)
                    自社区分 = string.IsNullOrEmpty(linkItemList[0]) ? (int?)null : int.Parse(linkItemList[0]);

                    // [1]:対象外リストを取得
                    string[] ary = linkItemList[1].ToString().Split(',');

                    foreach (var row in ary)
                    {
                        exclusionJisList.Add(AppCommon.IntParse(row));
                    }

                }
                else if (this.TwinTextBox.LinkItem is string)
                {
                    // LinkItemがstringの場合
                    string slinkItem = (this.TwinTextBox.LinkItem as string);

                    // 自社区分を設定
                    自社区分 = AppCommon.IntParse(slinkItem);
                }

                if (!string.IsNullOrEmpty(コード))
                {
                    code = コード;
                }

                // 自社マスタからデータを取得
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        TableNmList,
                        new object[] {
                            code,
                            exclusionJisList,
                            自社区分
                        }));
                //No.384 Mod End
            }
            catch (Exception)
            {
                return;
            }

        }
        #endregion

        #region << データ受信関連 >>

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
                case TableNmList:
                    if (tbl != null)
                    {
                        DataView dv = convertTableDisplayList(tbl).AsDataView();

                        // 絞り込みフィルタ生成
                        StringBuilder sb = new StringBuilder();
                        if (!string.IsNullOrEmpty(名称))
                        {
                            sb.AppendFormat("自社名 LIKE '%{0}%'", 名称);
                        }

                        if (!string.IsNullOrEmpty(代表者名))
                        {
                            sb.AppendFormat("{0} 代表者名 LIKE '%{1}%'", sb.Length == 0 ? string.Empty : " AND", 代表者名);
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
                                dv.Sort = COLUM_NAME;
                                break;
                            case 2:     // 代表者名
                                dv.Sort = COLUM_REPRESENTATIVE_NAME;
                                break;
                        }

                        SearchResult = dv.ToTable();
                        SearchGrid.SelectedIndex = 0;

                    }
                    break;

                default:
                    break;

            }

        }

        #endregion

        #region 表示不要列の削除
        /// <summary>
        /// 取得テーブルを表示項目のテーブルに変換して返す
        /// </summary>
        /// <param name="tbl"></param>
        /// <returns></returns>
        private DataTable convertTableDisplayList(DataTable tbl)
        {
            // 不要列を削除
            tbl.Columns.Remove("ロゴ画像");
            tbl.Columns.Remove("登録者");
            tbl.Columns.Remove("登録日時");
            tbl.Columns.Remove("最終更新者");
            tbl.Columns.Remove("最終更新日時");
            tbl.Columns.Remove("削除者");
            tbl.Columns.Remove("削除日時");

            // ロゴ画像の表記変更
            //foreach (DataRow row in tbl.Rows)
            //{
            //    // ロゴ未設定の場合はスキップ
            //    if (string.IsNullOrEmpty(row["ロゴ画像"].ToString()))
            //        continue;

            //    // 項目表記「有」とするよう変更
            //    row["ロゴ画像"] = "有";

            //}

            return tbl;

        }
        #endregion

        #region 検索ボタン押下
        /// <summary>
        /// 検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GridOutPut();
        }
        #endregion

        #region << 選択・キャンセルイベント群 >>

        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            CloseDataSelected();
        }

        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            Close();
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
            this.Close();
        }

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
        /// グリッド上でキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CloseDataSelected();
            }

        }

        #endregion

        #region データ選択時処理
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
        #endregion

        #region テキスト変更時イベント
        /// <summary>
        /// テキストが変更された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_cTextChanged(object sender, RoutedEventArgs e)
        {
            GridOutPut();

        }
        #endregion

        #region コンボ変更時イベント
        /// <summary>
        /// 並び順コンボが変更された時のイベント処理
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
                case 0:     // コード
                default:
                    view.Sort = COLUM_ID;
                    break;
                case 1:     // 名称
                    view.Sort = COLUM_NAME;
                    break;
                case 2:     // 代表者名
                    view.Sort = COLUM_REPRESENTATIVE_NAME;
                    break;
            }

            SearchResult = view.ToTable();
            SearchGrid.SelectedIndex = 1;

        }
        #endregion

        #region Mindoow_Closed
        //画面が閉じられた時、データを保持する
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

    }

}
