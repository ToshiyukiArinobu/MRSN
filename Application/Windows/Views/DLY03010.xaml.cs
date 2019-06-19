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
    /// 配車表入力画面
    /// </summary>
    public partial class DLY03010 : WindowReportBase
    {
        // データアクセス定義名
        private const string DataAccessName = "定義名";

        // データバインド用変数
        private DateTime _検索日付From;
        public DateTime 検索日付From
        {
            get { return this._検索日付From; }
            set
            {
                this._検索日付From = value;
                NotifyPropertyChanged();
            }
        }

        private DateTime _検索日付To;
        public DateTime 検索日付To
        {
            get { return this._検索日付To; }
            set
            {
                this._検索日付To = value;
                NotifyPropertyChanged();
            }
        }


        private string _発地名指定;
        public string 発地名指定
        {
            get { return this._発地名指定; }
            set
            {
                this._発地名指定 = value;
                NotifyPropertyChanged();
            }
        }

        private string _着地名指定;
        public string 着地名指定
        {
            get { return this._着地名指定; }
            set
            {
                this._着地名指定 = value;
                NotifyPropertyChanged();
            }
        }

        private string _自社部門指定;
        public string 自社部門指定
        {
            get { return this._自社部門指定; }
            set
            {
                this._自社部門指定 = value;
                NotifyPropertyChanged();
            }
        }

        private string _入力担当;
        public string 入力担当
        {
            get { return this._入力担当; }
            set
            {
                this._入力担当 = value;
                NotifyPropertyChanged();
            }
        }

        private string _指定区分;
        public string 指定区分
        {
            get { return this._指定区分; }
            set
            {
                this._指定区分 = value;
                NotifyPropertyChanged();
            }
        }

        private string _指定コード;
        public string 指定コード
        {
            get { return this._指定コード; }
            set
            {
                this._指定コード = value;
                NotifyPropertyChanged();
            }
        }

        private string _指定名;
        public string 指定名
        {
            get { return this._指定名; }
            set
            {
                this._指定名 = value;
                NotifyPropertyChanged();
            }
        }

        private string _商品名指定;
        public string 商品名指定
        {
            get { return this._商品名指定; }
            set
            {
                this._商品名指定 = value;
                NotifyPropertyChanged();
            }
        }

        private string _摘要備考;
        public string 摘要備考
        {
            get { return this._摘要備考; }
            set
            {
                this._摘要備考 = value;
                NotifyPropertyChanged();
            }
        }
        
        private string _第一順序;
        public string 第一順序
        {
            get { return this._第一順序; }
            set
            {
                this._第一順序 = value;
                NotifyPropertyChanged();
            }
        }

        private string _第二順序;
        public string 第二順序
        {
            get { return this._第二順序; }
            set
            {
                this._第二順序 = value;
                NotifyPropertyChanged();
            }
        }

        private string _第三順序;
        public string 第三順序
        {
            get { return this._第三順序; }
            set
            {
                this._第三順序 = value;
                NotifyPropertyChanged();
            }
        }

        private string _第四順序;
        public string 第四順序
        {
            get { return this._第四順序; }
            set
            {
                this._第四順序 = value;
                NotifyPropertyChanged();
            }
        }

        

        

        /// <summary>
        /// 配車表入力画面
        /// </summary>
        public DLY03010()
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
                //	ボタンのクリック時のイベントを呼び出します

                case Key.F1:
                    //照会
                    this.RibbonKensaku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonKensaku));
                    break;
                case Key.F2:
                    //入力
                    this.RibbonNyuuryoku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonNyuuryoku));
                    break;
                case Key.F3:
                    //行追加
                    this.RibbonGoyouTsuika.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonNyuuryoku));
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
                case Key.F11:
                    //終了
                    this.Syuuryou.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Syuuryou));
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
        /// F2　リボンマスタ入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonNyuuryoku_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("マスタ入力ボタンが押されました。");
        }

        /// <summary>
        /// F3　行追加ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tuika_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("行追加ボタンが押されました。");

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
        /// F11　リボン終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Syuuryou_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
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
