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
using System.Diagnostics;
using System.Data;

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Reports.Preview;
using KyoeiSystem.Framework.Windows.ViewBase;


namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 配送依頼書印刷画面
    /// </summary>
    public partial class SHR01010 : WindowReportBase
    {
        // データアクセス定義名
        private const string DataAccessName = "定義名";

        // データバインド用変数
        private string _部門指定 = string.Empty;
        public string 部門指定
        {
            set
            {
                _部門指定 = value;
                NotifyPropertyChanged();
            }
            get { return _部門指定; }
        }

        private string _日付指定 = string.Empty;
        public string 日付指定
        {
            set
            {
                _日付指定 = value;
                NotifyPropertyChanged();
            }
            get { return _日付指定; }
        }

        private string _日付指定From = string.Empty;
        public string 日付指定From
        {
            set
            {
                _日付指定From = value;
                NotifyPropertyChanged();
            }
            get { return _日付指定From; }
        }
        private string _日付指定To = string.Empty;
        public string 日付指定To
        {
            set
            {
                _日付指定To = value;
                NotifyPropertyChanged();
            }
            get { return _日付指定To; }
        }

        private string _得意先ピックアップ = string.Empty;
        public string 得意先ピックアップ
        {
            set
            {
                _得意先ピックアップ = value;
                NotifyPropertyChanged();
            }
            get { return _得意先ピックアップ; }
        }
        private string _得意先From = string.Empty;
        public string 得意先From
        {
            set
            {
                _得意先From = value;
                NotifyPropertyChanged();
            }
            get { return _得意先From; }
        }
        private string _得意先To = string.Empty;
        public string 得意先To
        {
            set
            {
                _得意先To = value;
                NotifyPropertyChanged();
            }
            get { return _得意先To; }
        }

        private string _支払先ピックアップ = string.Empty;
        public string 支払先ピックアップ
        {
            set
            {
                _支払先ピックアップ = value;
                NotifyPropertyChanged();
            }
            get { return _支払先ピックアップ; }
        }
        private string _支払先From = string.Empty;
        public string 支払先From
        {
            set
            {
                _支払先From = value;
                NotifyPropertyChanged();
            }
            get { return _支払先From; }
        }
        private string _支払先To = string.Empty;
        public string 支払先To
        {
            set
            {
                _支払先To = value;
                NotifyPropertyChanged();
            }
            get { return _支払先To; }
        }

        private bool _内訳指定 = false;
        public bool 内訳指定
        {
            set
            {
                _内訳指定 = value;
                NotifyPropertyChanged();
            }
            get { return _内訳指定; }
        }

        private string _得意先コード = string.Empty;
        public string 得意先コード
        {
            set
            {
                _得意先コード = value;
                NotifyPropertyChanged();
            }
            get { return _得意先コード; }
        }

        private string _得意先名 = string.Empty;
        public string 得意先名
        {
            set
            {
                _得意先名 = value;
                NotifyPropertyChanged();
            }
            get { return _得意先名; }
        }

        private string _内訳ピックアップ = string.Empty;
        public string 内訳ピックアップ
        {
            set
            {
                _内訳ピックアップ = value;
                NotifyPropertyChanged();
            }
            get { return _内訳ピックアップ; }
        }
        private string _内訳From = string.Empty;
        public string 内訳From
        {
            set
            {
                _内訳From = value;
                NotifyPropertyChanged();
            }
            get { return _内訳From; }
        }
        private string _内訳To = string.Empty;
        public string 内訳To
        {
            set
            {
                _内訳To = value;
                NotifyPropertyChanged();
            }
            get { return _内訳To; }
        }

        private string _発地ピックアップ = string.Empty;
        public string 発地ピックアップ
        {
            set
            {
                _発地ピックアップ = value;
                NotifyPropertyChanged();
            }
            get { return _発地ピックアップ; }
        }
        private string _発地From = string.Empty;
        public string 発地From
        {
            set
            {
                _発地From = value;
                NotifyPropertyChanged();
            }
            get { return _発地From; }
        }
        private string _発地To = string.Empty;
        public string 発地To
        {
            set
            {
                _発地To = value;
                NotifyPropertyChanged();
            }
            get { return _発地To; }
        }
        private string _発地名 = string.Empty;
        public string 発地名
        {
            set
            {
                _発地名 = value;
                NotifyPropertyChanged();
            }
            get { return _発地名; }
        }

        private string _着地ピックアップ = string.Empty;
        public string 着地ピックアップ
        {
            set
            {
                _着地ピックアップ = value;
                NotifyPropertyChanged();
            }
            get { return _着地ピックアップ; }
        }
        private string _着地From = string.Empty;
        public string 着地From
        {
            set
            {
                _着地From = value;
                NotifyPropertyChanged();
            }
            get { return _着地From; }
        }
        private string _着地To = string.Empty;
        public string 着地To
        {
            set
            {
                _着地To = value;
                NotifyPropertyChanged();
            }
            get { return _着地To; }
        }
        private string _着地名 = string.Empty;
        public string 着地名
        {
            set
            {
                _着地名 = value;
                NotifyPropertyChanged();
            }
            get { return _着地名; }
        }


        private string _車種ピックアップ = string.Empty;
        public string 車種ピックアップ
        {
            set
            {
                _車種ピックアップ = value;
                NotifyPropertyChanged();
            }
            get { return _車種ピックアップ; }
        }
        private string _車種From = string.Empty;
        public string 車種From
        {
            set
            {
                _車種From = value;
                NotifyPropertyChanged();
            }
            get { return _車種From; }
        }
        private string _車種To = string.Empty;
        public string 車種To
        {
            set
            {
                _車種To = value;
                NotifyPropertyChanged();
            }
            get { return _車種To; }
        }

        private string _第一順序 = string.Empty;
        public string 第一順序
        {
            set
            {
                _第一順序 = value;
                NotifyPropertyChanged();
            }
            get { return _第一順序; }
        }
        private string _第二順序 = string.Empty;
        public string 第二順序
        {
            set
            {
                _第二順序 = value;
                NotifyPropertyChanged();
            }
            get { return _第二順序; }
        }
        private string _第三順序 = string.Empty;
        public string 第三順序
        {
            set
            {
                _第三順序 = value;
                NotifyPropertyChanged();
            }
            get { return _第三順序; }
        }
        private string _第四順序 = string.Empty;
        public string 第四順序
        {
            set
            {
                _第四順序 = value;
                NotifyPropertyChanged();
            }
            get { return _第四順序; }
        }
        
        
        /// <summary>
        /// 配送依頼書印刷画面
        /// </summary>
        public SHR01010()
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
            // 画面が表示される最後の段階で処理すべき内容があれば、ここに記述します。
        }

        /// <summary>
        /// データアクセスエラー受信イベント
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            // 基底クラスのエラー受信イベントを呼び出します。
            base.OnReveivedError(message);

            // 個別にエラー処理が必要な場合、ここに記述してください。

        }

        /// <summary>
        /// 取得データの正常受信時のイベント
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            try
            {
				var data = message.GetResultData();
				DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
				switch (message.GetMessageName())
				{
				case "":
					break;
				default:
					break;
				}
			}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //<summary>
        //キーボードで押下されたkeyはここで拾えます
        //PreviewKeyDown="Window_PreviewKeyDown"をxamlのwindowプロパティに追加
        //</summary>
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Control s = e.Source as Control;
            switch (e.Key)
            {
                case Key.F1:
                    //照会
                    this.RibbonKensaku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonKensaku));
                    break;
                case Key.F11:
                    //終了
                    this.Syuuryou.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Syuuryou));
                    break;
                case Key.F5:
                    //CSV出力
                    this.CsvSyuturyoku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.CsvSyuturyoku));
                    break;
                case Key.F7:
                    //プレビュー
                    this.PreView.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.PreView));
                    break;
                case Key.F8:
                    //印刷
                    this.Insatu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Insatu));
                    break;
            }
        }

        /// <summary>
        /// 表示順序ボタンクリック時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HyoujiJyun_Click_1(object sender, RoutedEventArgs e)
        {
            this.HyoujiList.Visibility = Visibility.Visible;
            this.HyoujiButtonGrid.Visibility = Visibility.Visible;
            this.HyoujiTojiru.Visibility = Visibility.Visible;
            this.HyoujiCria.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// 表示閉じる押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HyoujiTojiru_Click_1(object sender, RoutedEventArgs e)
        {

            this.HyoujiList.Visibility = Visibility.Collapsed;
            this.HyoujiButtonGrid.Visibility = Visibility.Collapsed;
            this.HyoujiTojiru.Visibility = Visibility.Collapsed;
            this.HyoujiCria.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Listをダブルクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HyoujiList_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            /*string selectItem = string.Empty;

            //選択項目を取得
            foreach (ListBoxItem aitem in HyoujiList.SelectedItems)
            {
                selectItem += aitem.Content;
            }

            //順に格納
            switch (OutputCount)
            {
                case 0:
                    this.HyoujiText1.Text = selectItem;
                    OutputCount++;
                    break;
                case 1:
                    this.HyoujiText2.Text = selectItem;
                    OutputCount++;
                    break;
                case 2:
                    this.HyoujiText3.Text = selectItem;
                    OutputCount++;
                    break;
                case 3:
                    this.HyoujiText4.Text = selectItem;
                    OutputCount++;
                    break;
                case 4:
                    this.HyoujiText5.Text = selectItem;
                    OutputCount = 0;
                    break;
            }*/
        }


        /// <summary>
        /// クリアボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HyoujiCria_Click_1(object sender, RoutedEventArgs e)
        {
            /*this.HyoujiText1.Text = "";
            this.HyoujiText2.Text = "";
            this.HyoujiText3.Text = "";
            this.HyoujiText4.Text = "";
            this.HyoujiText5.Text = "";
            this.OutputImageFlag1 = false;
            this.OutputImageFlag2 = false;
            this.OutputImageFlag3 = false;
            this.OutputImageFlag4 = false;
            this.OutputImageFlag5 = false;
            TextChange();
            this.OutputCount = 0;*/
        }
        #region Ribbon

        /// <summary>
        /// F1　リボン検索ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonKensaku_Click_1(object sender, RoutedEventArgs e)
        {
            SCH04010 page = new SCH04010();
            page.Show();
        }

        /// <summary>
        /// F11　リボン終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Syuuryou_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// F5 リボン　CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CsvSyuturyoku_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("CSV出力ボタンが押されました。");
        }


        /// <summary>
        /// F8　リボン印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Insatu_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("印刷ボタンが押されました。");
        }

        /// <summary>
        /// F7　リボンプレビュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreView_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("プレビューボタンが押されました。");
        }

        /// <summary>
        /// リボン便利リンク　検索ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Kensaku_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.yahoo.co.jp/");
        }

        /// <summary>
        /// リボン便利リンク　道路情報ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DouroJyouhou_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.jartic.or.jp/");

        }

        /// <summary>
        /// リボン便利リンク　道路ナビボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DouroNabi_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://highway.drivenavi.net/");
        }

        /// <summary>
        /// リボン便利リンク　渋滞情報ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JyuutaiJyouhou_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.mapfan.com/");
        }

        /// <summary>
        /// リボン便利リンク　天気ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tenki_RibbonHomeBenri_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("http://weathernews.jp/");
        }

        /// <summary>
        /// リボン　WebHomeボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonButton_WebHome_Click_1(object sender, RoutedEventArgs e)
        {
            Process Pro = new Process();

            try
            {
                Pro.StartInfo.UseShellExecute = false;
                Pro.StartInfo.FileName = "C:\\Program Files (x86)/Internet Explorer/iexplore.exe";
                Pro.StartInfo.CreateNoWindow = true;
                Pro.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// リボン　メールボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonButton_Meil_Click_1(object sender, RoutedEventArgs e)
        {
            Process Pro = new Process();

            try
            {
                Pro.StartInfo.UseShellExecute = false;
                Pro.StartInfo.FileName = "C://Program Files (x86)//Windows Live//Mail//wlmail.exe";
                Pro.StartInfo.CreateNoWindow = true;
                Pro.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// リボン　電卓ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonButton_Dentaku_Click_1(object sender, RoutedEventArgs e)
        {
            Process Pro = new Process();

            try
            {
                Pro.StartInfo.UseShellExecute = false;
                Pro.StartInfo.FileName = "C://Windows//System32/calc.exe";
                Pro.StartInfo.CreateNoWindow = true;
                Pro.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
