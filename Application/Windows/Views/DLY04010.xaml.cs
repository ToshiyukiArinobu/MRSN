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
    /// 配車入力画面
    /// </summary>
    public partial class DLY04010 : WindowReportBase
    {
        // データアクセス定義名
        private const string DataAccessName = "定義名";

        // データバインド用変数
        private DateTime _配達日付;
        public DateTime 配達日付
        {
            get { return this._配達日付; }
            set
            {
                this._配達日付 = value;
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

        private string _管理部門指定;
        public string 管理部門指定
        {
            get { return this._管理部門指定; }
            set
            {
                this._管理部門指定 = value;
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

        private bool _車番未定;
        public bool 車番未定
        {
            get { return this._車番未定; }
            set
            {
                this._車番未定 = value;
                NotifyPropertyChanged();
            }
        }

        private bool _乗務未定;
        public bool 乗務未定
        {
            get { return this._乗務未定; }
            set
            {
                this._乗務未定 = value;
                NotifyPropertyChanged();
            }
        }

        private string _選択総重量;
        public string 選択総重量
        {
            get { return this._選択総重量; }
            set
            {
                this._選択総重量 = value;
                NotifyPropertyChanged();
            }
        }


        private string _車輌番号コード;
        public string 車輌番号コード
        {
            get { return this._車輌番号コード; }
            set
            {
                this._車輌番号コード = value;
                NotifyPropertyChanged();
            }
        }
        private string _車輌名;
        public string 車輌名
        {
            get { return this._車輌名; }
            set
            {
                this._車輌名 = value;
                NotifyPropertyChanged();
            }
        }

        private string _車種ID;
        public string 車種ID
        {
            get { return this._車種ID; }
            set
            {
                this._車種ID = value;
                NotifyPropertyChanged();
            }
        }
        private string _車種名;
        public string 車種名
        {
            get { return this._車種名; }
            set
            {
                this._車種名 = value;
                NotifyPropertyChanged();
            }
        }

        private string _数量;
        public string 数量
        {
            get { return this._数量; }
            set
            {
                this._数量 = value;
                NotifyPropertyChanged();
            }
        }

        private string _明細NO;
        public string 明細NO
        {
            get { return this._明細NO; }
            set
            {
                this._明細NO = value;
                NotifyPropertyChanged();
            }
        }

        private string _乗務員コード;
        public string 乗務員コード
        {
            get { return this._乗務員コード; }
            set
            {
                this._乗務員コード = value;
                NotifyPropertyChanged();
            }
        }
        private string _乗務員名;
        public string 乗務員名
        {
            get { return this._乗務員名; }
            set
            {
                this._乗務員名 = value;
                NotifyPropertyChanged();
            }
        }

        private string _社内区分;
        public string 社内区分
        {
            get { return this._社内区分; }
            set
            {
                this._社内区分 = value;
                NotifyPropertyChanged();
            }
        }

        private string _未定;
        public string 未定
        {
            get { return this._未定; }
            set
            {
                this._未定 = value;
                NotifyPropertyChanged();
            }
        }

        private string _重量;
        public string 重量
        {
            get { return this._重量; }
            set
            {
                this._重量 = value;
                NotifyPropertyChanged();
            }
        }

        private string _支払先コード;
        public string 支払先コード
        {
            get { return this._支払先コード; }
            set
            {
                this._支払先コード = value;
                NotifyPropertyChanged();
            }
        }
        private string _支払先名;
        public string 支払先名
        {
            get { return this._支払先名; }
            set
            {
                this._支払先名 = value;
                NotifyPropertyChanged();
            }
        }

        private string _支払先コード_２次;
        public string 支払先コード_２次
        {
            get { return this._支払先コード_２次; }
            set
            {
                this._支払先コード_２次 = value;
                NotifyPropertyChanged();
            }
        }
        private string _支払先名_２次;
        public string 支払先名_２次
        {
            get { return this._支払先名_２次; }
            set
            {
                this._支払先名_２次 = value;
                NotifyPropertyChanged();
            }
        }

        private string _走行ＫＭ;
        public string 走行ＫＭ
        {
            get { return this._走行ＫＭ; }
            set
            {
                this._走行ＫＭ = value;
                NotifyPropertyChanged();
            }
        }

        private string _計算区分;
        public string 計算区分
        {
            get { return this._計算区分; }
            set
            {
                this._計算区分 = value;
                NotifyPropertyChanged();
            }
        }

        private string _支払単価;
        public string 支払単価
        {
            get { return this._支払単価; }
            set
            {
                this._支払単価 = value;
                NotifyPropertyChanged();
            }
        }

        private string _支払金額;
        public string 支払金額
        {
            get { return this._支払金額; }
            set
            {
                this._支払金額 = value;
                NotifyPropertyChanged();
            }
        }

        private string _支払通行料;
        public string 支払通行料
        {
            get { return this._支払通行料; }
            set
            {
                this._支払通行料 = value;
                NotifyPropertyChanged();
            }
        }

        private string _税区分;
        public string 税区分
        {
            get { return this._税区分; }
            set
            {
                this._税区分 = value;
                NotifyPropertyChanged();
            }
        }

        private string _支払先乗務員;
        public string 支払先乗務員
        {
            get { return this._支払先乗務員; }
            set
            {
                this._支払先乗務員 = value;
                NotifyPropertyChanged();
            }
        }

        private string _連絡先;
        public string 連絡先
        {
            get { return this._連絡先; }
            set
            {
                this._連絡先 = value;
                NotifyPropertyChanged();
            }
        }

        private string _管理部門指定_明細;
        public string 管理部門指定_明細
        {
            get { return this._管理部門指定_明細; }
            set
            {
                this._管理部門指定_明細 = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// 配車入力
        /// </summary>
        public DLY04010()
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
                case Key.F1:
                    //	ボタンのクリック時のイベントを呼び出します
                    this.RibbonKensaku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonKensaku));
                    break;
                case Key.F2:
                    this.RibbonNyuuryoku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonNyuuryoku));
                    break;
                case Key.F11:
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
        /// F3 リボン休車入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KyuusyaNyuuryoku_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("休車入力ボタンが押されました。");

        }

        /// <summary>
        /// F8　リボン配車入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HaisyaHyouNyuryoku_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("配車表入力ボタンが押されました。");

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
