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
    public partial class SCH19010 : WindowMasterSearchBase
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
        public class ConfigSCH19010 : FormConfigBase
        {
            public int Combo { get; set; }

        }
        /// ※ 必ず public で定義する。
        public ConfigSCH19010 frmcfg = null;

        #endregion

        #region 定数定義

        private const string TabelNm = "M03_YTAN1_SCH";
        private const string COLUM_ID = "取引先ID";
        private const string COLUM_NAME = "取引先名";

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
        private string _かな読み = string.Empty;
        public string かな読み
        {
            get { return this._かな読み; }
            set { this._かな読み = value; NotifyPropertyChanged(); }
        }
        private string _確定コード = string.Empty;
        public string 確定コード
        {
            get { return this._確定コード; }
            set { this._確定コード = value; NotifyPropertyChanged(); }
        }

        private string _得意先ID = string.Empty;
        public string 得意先ID
        {
            get { return this._得意先ID; }
            set { this._得意先ID = value; NotifyPropertyChanged(); }
        }

        private string _発地ID = string.Empty;
        public string 発地ID
        {
            get { return this._発地ID; }
            set { this._発地ID = value; NotifyPropertyChanged(); }
        }

        private string _着地ID = string.Empty;
        public string 着地ID
        {
            get { return this._着地ID; }
            set { this._着地ID = value; NotifyPropertyChanged(); }
        }

        private string _商品ID = string.Empty;
        public string 商品ID
        {
            get { return this._商品ID; }
            set { this._商品ID = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region SCH19010

        public SCH19010()
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

            // 初期フォーカスの設定を行う
            this.OkButton.Focus();
            GridOutPut();
            //画面サイズをタスクバーをのぞいた状態で表示させる
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;

            //メイン画面と子画面が被ることなく表示できるかチェック
            if (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width < 1024 + 342)
            {
                //画面の左端に表示させる
                this.Left = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width;
            }

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigSCH19010)ucfg.GetConfigValue(typeof(ConfigSCH19010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigSCH19010();
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
                //this.OrderColumn_Copy.SelectedIndex = frmcfg.Combo;
            }
            #endregion

            #region 表示順序

            if (SearchResult == null)
            {
                return;
            }

            DataView view = new DataView(SearchResult);

            //switch (OrderColumn_Copy.SelectedIndex)
            //{
            //    case 0: //コード
            //        view.Sort = COLUM_ID;
            //        break;
            //    case 1: //商品名
            //        view.Sort = COLUM_NAME;
            //        break;
            //}

            SearchResult = view.ToTable();

            #endregion
        }

        #endregion 

        #region GridOutPut

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

            int iTokuisakiId = 0;
            int iHatutiId = 0;
            int iTyakutiId = 0;
            int iSyouhinId = 0;
            
            int.TryParse(得意先ID, out iTokuisakiId);
            int.TryParse(発地ID, out iHatutiId);
            int.TryParse(着地ID, out iTyakutiId);
            int.TryParse(商品ID, out iSyouhinId);

            //マスタ
            base.SendRequest(new CommunicationObject(MessageType.RequestData, TabelNm, new object[] { iTokuisakiId, iHatutiId, iTyakutiId, iSyouhinId }));


            //if (int.TryParse(得意先ID, out iTokuisakiId)
            //    && int.TryParse(発地ID, out iHatutiId)
            //    && int.TryParse(着地ID, out iTyakutiId)
            //    && int.TryParse(商品ID, out iSyouhinId)
            //    )
            //{
            //    //マスタ
            //    base.SendRequest(new CommunicationObject(MessageType.RequestData, TabelNm, new object[] { iTokuisakiId, iHatutiId, iTyakutiId, iSyouhinId }));
            //}
            //else
            //{
            //    base.SendRequest(new CommunicationObject(MessageType.RequestData, TabelNm, new object[] { iTokuisakiId, iHatutiId, iTyakutiId, iSyouhinId }));

            //}

        }

        #endregion

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
                    if (!string.IsNullOrEmpty(かな読み))
                    {
                        DataTable dt = SearchResult.Clone();

                        foreach (DataRow dtRow in SearchResult.Select("かな読み LIKE '%" + かな読み + "%'"))
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
                    //switch (OrderColumn_Copy.SelectedIndex)
                    //{
                    //    case 0: //コード
                    //    default:
                    //        view.Sort = COLUM_ID;
                    //        break;
                    //    case 1: //商品名
                    //        view.Sort = COLUM_NAME;
                    //        break;
                    //}

                    SearchResult = view.ToTable();
                    SearchGrid.SelectedIndex = 0;

                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Grid内でEnter押下でタブ移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Control s = e.Source as Control;
                if (s is Button)
                {
                    //// クリックのときはイベント発生
                    //s.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, s));
                    this.OkButton.Focus();
                }
                else
                {
                    s.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
                e.Handled = true;
            }
            else if (e.Key == Key.F11)
            {
                // 選択
                this.OkButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.OkButton));
            }
            else if (e.Key == Key.F11)
            {
                // 閉じる
                this.CancelButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.CancelButton));
            }
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
            if (ucfg != null)
            {
                frmcfg.Top = this.Top;
                frmcfg.Left = this.Left;
                frmcfg.Height = this.Height;
                frmcfg.Width = this.Width;
                //frmcfg.Combo = this.OrderColumn_Copy.SelectedIndex;
                ucfg.SetConfigValue(frmcfg);
            }
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

            this.得意先ID = SearchResult.Rows[SearchGrid.SelectedIndex]["取引先ID"].ToString();
            this.発地ID = SearchResult.Rows[SearchGrid.SelectedIndex]["発地ID"].ToString(); ;
            this.着地ID = SearchResult.Rows[SearchGrid.SelectedIndex]["着地ID"].ToString(); ;
            this.商品ID = SearchResult.Rows[SearchGrid.SelectedIndex]["商品ID"].ToString(); ;
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

            //switch (OrderColumn_Copy.SelectedIndex)
            //{
            //    case 0: //コード
            //    default:
            //        view.Sort = COLUM_ID;
            //        break;
            //    case 1: //商品名
            //        view.Sort = COLUM_NAME;
            //        break;
            //    //case 2:　//商品よみ
            //    //    view.Sort = COLUM_KANA;
            //    //    break;
            //}

            SearchResult = view.ToTable();
            SearchGrid.SelectedIndex = 0;
        }

        private void txtKana_cTextChanged(object sender, RoutedEventArgs e)
        {
            GridOutPut();
        }


    }
}

