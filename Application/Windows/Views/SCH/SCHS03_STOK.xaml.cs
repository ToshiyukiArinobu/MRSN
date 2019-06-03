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
    /// SCHS03_STOK.xaml の相互作用ロジック
    /// </summary>
    public partial class SCHS03_STOK : WindowMasterSearchBase
    {
        /// <summary>対象コードデータの取得</summary>
        private const string TargetTableNm = "S03_STOK_GetData";

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;

        //<summary>
        //画面固有設定項目のクラス定義
        //※ 必ず public で定義する。
        //</summary>
        public class ConfigSCHS03_STOK : FormConfigBase
        {
            //public bool[] 表示順方向 { get; set; }
            /// コンボボックスの位置
            public int Combo { get; set; }
        }

        /// ※ 必ず public で定義する。
        public ConfigSCHS03_STOK frmcfg = null;

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

        #endregion

        #region << 変数定義 >>

        /// <summary>
        /// 自社コード(呼び出し元から受け取る)
        /// </summary>
        private int _stockpileCode;

        #endregion

        /// <summary>
        /// 品番コード
        /// </summary>
        /// <remarks>
        /// 初期表示サポート用
        /// </remarks>
        public int productCode { get; set; }

        #region << 初期処理群 >>

        /// <summary>
        /// 在庫参照 コンストラクタ
        /// </summary>
        public SCHS03_STOK(int stockpile)
        {
            InitializeComponent();

            this.DataContext = this;
            this.Topmost = true;

            _stockpileCode = stockpile;

        }

        /// <summary>
        /// 画面読み込み後のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //this.OkButton.FontSize = 9;
            //this.OkButton.Content = "\n\n\n選択(F11)";
            this.CancelButton.FontSize = 9;
            this.CancelButton.Content = "\n\n\n終了(F1)";

            // 画面サイズをタスクバーをのぞいた状態で表示させる
            this.Height = WinFormsScreen.PrimaryScreen.WorkingArea.Size.Height;

            // メイン画面と子画面が被ることなく表示できるかチェック
            if (WinFormsScreen.PrimaryScreen.WorkingArea.Size.Width < 1024 + 342)
            {
                // 画面の左端に表示させる
                this.Left = WinFormsScreen.PrimaryScreen.WorkingArea.Size.Width - this.Width;
            }

            // コンボボックスのSelectionChangedを設定する。
            //this.OrderColumn.SelectionChanged += this.OrderColumn_SelectionChanged;

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigSCHS03_STOK)ucfg.GetConfigValue(typeof(ConfigSCHS03_STOK));
            if (frmcfg == null)
            {
                frmcfg = new ConfigSCHS03_STOK();
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

            }
            #endregion

            //AppCommon.SetutpComboboxList(this.OrderColumn, false);

            // 初期表示データ取得
            GridOutPut();

            // 初期フォーカスの設定を行う
            this.txtProductCode.Focus();

        }

        #endregion

        #region << リボン >>

        /// <summary>
        /// F1　リボン　閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            Close();
        }

        #endregion

        #region << データ受信 >>

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
                    SearchResult = tbl;

                    // 品番コード指定がある場合は該当品番を選択状態にする
                    if (productCode < 0)
                        SearchGrid.SelectedIndex = 0;

                    else
                    {
                        // ①該当品番コードが検索結果にあるか
                        if(tbl.Select(string.Format("品番コード = {0}", productCode)).Count() > 0)
                        {
                            // ②存在する場合行インデックスを取得
                            var index =
                                tbl.Rows.IndexOf(
                                    tbl.AsEnumerable()
                                        .Where(a => a.Field<int>("品番コード") == productCode)
                                        .FirstOrDefault());

                            SearchGrid.SelectedIndex = index;
                            SearchGrid.Focus();
                        }

                    }
                    break;


                default:
                    break;
            }

        }

        #endregion

        #region << フォームイベント関連 >>

        #region *** 検索ボタン関連イベント ***
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

        #region *** 選択ボタン関連イベント ***
        private void OkButton_MouseEnter(object sender, MouseEventArgs e)
        {
            //this.OkButton.FontSize = 12;
            //this.OkButton.Content = "選択";
        }

        private void OkButton_MouseLeave(object sender, MouseEventArgs e)
        {
            //this.OkButton.FontSize = 9;
            //this.OkButton.Content = "\n\n\n選択(F11)";
        }
        #endregion

        #region *** 閉じるボタン関連イベント ***
        /// <summary>
        /// 閉じるボタンの上にマウスカーソルがあたった時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_MouseEnter(object sender, MouseEventArgs e)
        {
            this.CancelButton.FontSize = 12;
            this.CancelButton.Content = "閉じる";
        }

        /// <summary>
        /// 閉じるボタンの上からマウスカーソルが外れた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_MouseLeave(object sender, MouseEventArgs e)
        {
            this.CancelButton.FontSize = 9;
            this.CancelButton.Content = "\n\n\n終了(F1)";
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
        #endregion

        #region *** テキスト関連イベント ***
        /// <summary>
        /// 条件テキスト変更時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_cTextChanged(object sender, RoutedEventArgs e)
        {
            //GridOutPut();

        }

        /// <summary>
        /// 条件テキストからフォーカスアウトした時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            GridOutPut();

        }

        #endregion

        #region *** 画面終了関連イベント ***
        /// <summary>
        /// 画面が閉じられた時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;

            ucfg.SetConfigValue(frmcfg);

        }
        #endregion

        #endregion

        #region << 業務ロジック群 >>

        /// <summary>
        /// データの取得
        /// </summary>
        private void GridOutPut()
        {
            Dictionary<string, string> paramDic = new Dictionary<string, string>();
            try
            {
                // 在庫データ取得
                base.SendRequest(
                    new CommunicationObject(
                        MessageType.RequestData,
                        TargetTableNm,
                        new object[] {
                            _stockpileCode,
                            getFormParams()
                        }));

            }
            catch (Exception)
            {
                return;
            }

        }

        /// <summary>
        /// 画面設定を辞書に設定
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> getFormParams()
        {
            Dictionary<string, string> paramDic = new Dictionary<string, string>();

            paramDic.Add("品番", this.txtProductCode.Text);
            paramDic.Add("品名", this.txtProductName.Text);
            paramDic.Add("色名", this.txtColorName.Text);
            paramDic.Add("ブランド", this.txtBrandName.Text);
            paramDic.Add("シリーズ", this.txtSeriesName.Text);
            paramDic.Add("品群", this.txtHingun.Text);

            return paramDic;

        }

        #endregion

    }

}
