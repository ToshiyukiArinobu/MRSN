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
    /// MCustomer.xaml の相互作用ロジック
    /// </summary>
    public partial class SCH29010 : WindowMasterSearchBase
    {
        private const string TabelNm = "M12_KIS_SCH";
        private const string COLUM_ID = "規制区分ID";
        private const string COLUM_NAME = "規制名";

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;

        //<summary>
        //画面固有設定項目のクラス定義
        //※ 必ず public で定義する。
        //</summary>
        public class ConfigSCH29010 : FormConfigBase
        {
            //public bool[] 表示順方向 { get; set; }
            /// コンボボックスの位置
            public int Combo { get; set; }
        }

        /// ※ 必ず public で定義する。
        public ConfigSCH29010 frmcfg = null;

        #endregion

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
        private string _規制名 = string.Empty;
        public string 規制名
        {
            get { return this._規制名; }
            set { this._規制名 = value; NotifyPropertyChanged(); }
        }
        private string _確定コード = string.Empty;
        public string 確定コード
        {
            get { return this._確定コード; }
            set { this._確定コード = value; NotifyPropertyChanged(); }
        }

        public SCH29010()
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

            // 初期フォーカスの設定を行う
            this.txtShashu.Focus();
            GridOutPut();
            //画面サイズをタスクバーをのぞいた状態で表示させる
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;

            //メイン画面と子画面が被ることなく表示できるかチェック
            if (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width < 1024 + 342)
            {
                //画面の左端に表示させる
                this.Left = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width;
            }

            //コンボボックスのSelectionChangedを設定する。
            this.OrderColumn.SelectionChanged += this.OrderColumn_SelectionChanged;

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigSCH29010)ucfg.GetConfigValue(typeof(ConfigSCH29010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigSCH29010();
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
                case 0: //コード
                default:
                    view.Sort = COLUM_ID;
                    break;
                case 1: //G規制名
                    view.Sort = COLUM_NAME;
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
            int searchId = -1;
            if (!int.TryParse(this._確定コード, out searchId))
            {
                searchId = -1;
            }

            try
            {
                //商品マスタ
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TabelNm, new object[] { searchId, 0 }));
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
                case TabelNm:
                    //Gridのバインド変数に代入
                    SearchResult = tbl;

                    DataRow r = null;
                    //かな読みの条件で抽出
                    if (!string.IsNullOrEmpty(規制名))
                    {
                        DataTable dt = SearchResult.Clone();

                        foreach (DataRow dtRow in SearchResult.Select("規制名 LIKE '&" + 規制名 + "%'"))
                        {
                            r = dt.NewRow();
                            for (int n = 0; n < dtRow.ItemArray.Length; n++)
                            {
                                r[n] = dtRow[n];
                            }
                            dt.Rows.Add(r);
                        }
                        SearchResult = dt;

                    }
                    DataView view = new DataView(SearchResult);

                    switch (OrderColumn.SelectedIndex)
                    {
                        case 0: //コード
                        default:
                            view.Sort = COLUM_ID;
                            break;
                        case 1: //G規制名
                            view.Sort = COLUM_NAME;
                            break;
                    }

                    SearchResult = view.ToTable();
                    SearchGrid.SelectedIndex = 0;

                    break;
                default:
                    break;
            }
        }

        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            SetDataTwinText();
        }

        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            Close();
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

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            SetDataTwinText();
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
                case 1: //規制名
                    view.Sort = COLUM_NAME;
                    break;
            }

            SearchResult = view.ToTable();
            SearchGrid.SelectedIndex = 0;
        }

        private void txtKisei_cTextChanged(object sender, RoutedEventArgs e)
        {
            GridOutPut();
        }

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

        private void SearchGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SetDataTwinText();
            }
        }

    }
}
