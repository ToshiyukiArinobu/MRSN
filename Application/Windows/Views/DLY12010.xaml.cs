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
    /// 乗務員・車輌問合せ画面
    /// </summary>
    public partial class DLY12010 : WindowReportBase
    {
        // データアクセス定義名
        private const string DataAccessName = "定義名";

        // データバインド用変数
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

        private string _車輌番号;
        public string 車輌番号
        {
            get { return this._車輌番号; }
            set
            {
                this._車輌番号 = value;
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

        private string _商品名;
        public string 商品名
        {
            get { return this._商品名; }
            set
            {
                this._商品名 = value;
                NotifyPropertyChanged();
            }
        }

        private string _発地名;
        public string 発地名
        {
            get { return this._発地名; }
            set
            {
                this._発地名 = value;
                NotifyPropertyChanged();
            }
        }

        private string _着地名;
        public string 着地名
        {
            get { return this._着地名; }
            set
            {
                this._着地名 = value;
                NotifyPropertyChanged();
            }
        }

        private string _社内備考;
        public string 社内備考
        {
            get { return this._社内備考; }
            set
            {
                this._社内備考 = value;
                NotifyPropertyChanged();
            }
        }

        private string _請求摘要;
        public string 請求摘要
        {
            get { return this._請求摘要; }
            set
            {
                this._請求摘要 = value;
                NotifyPropertyChanged();
            }
        }

        private string _売上金額計;
        public string 売上金額計
        {
            get { return this._売上金額計; }
            set
            {
                this._売上金額計 = value;
                NotifyPropertyChanged();
            }
        }

        private string _通行料計;
        public string 通行料計
        {
            get { return this._通行料計; }
            set
            {
                this._通行料計 = value;
                NotifyPropertyChanged();
            }
        }

        private string _距離割増計;
        public string 距離割増計
        {
            get { return this._距離割増計; }
            set
            {
                this._距離割増計 = value;
                NotifyPropertyChanged();
            }
        }

        private string _時間割増計;
        public string 時間割増計
        {
            get { return this._時間割増計; }
            set
            {
                this._時間割増計 = value;
                NotifyPropertyChanged();
            }
        }

        private string _請求金額合計;
        public string 請求金額合計
        {
            get { return this._請求金額合計; }
            set
            {
                this._請求金額合計 = value;
                NotifyPropertyChanged();
            }
        }

        private string _支払金額計;
        public string 支払金額計
        {
            get { return this._支払金額計; }
            set
            {
                this._支払金額計 = value;
                NotifyPropertyChanged();
            }
        }

        private string _支払立替計;
        public string 支払立替計
        {
            get { return this._支払立替計; }
            set
            {
                this._支払立替計 = value;
                NotifyPropertyChanged();
            }
        }

        private string _支払金額合計;
        public string 支払金額合計
        {
            get { return this._支払金額合計; }
            set
            {
                this._支払金額合計 = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        ///　メンバー変数
        /// </summary>
        #region Member
        //表示している表示順序指定数
        public int OutputCount = 0;

        //表示順序指定の各テキストFlag
        public bool OutputImageFlag1 = false;
        public bool OutputImageFlag2 = false;
        public bool OutputImageFlag3 = false;
        public bool OutputImageFlag4 = false;
        public bool OutputImageFlag5 = false;
        #endregion
        
        
        /// <summary>
        /// 乗務員・車輌問合せ画面
        /// </summary>
        public DLY12010()
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
                    this.RibbonKensaku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonKensaku));
                    break;
                case Key.F4:
                    //詳細表示
                    this.RibbonSyousai.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonSyousai));
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
                    this.Syuuryou.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Syuuryou));
                    break;
            }
        }

        /// <summary>
        /// 検索ボタン クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            

        }

        /// <summary>
        /// クリアボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HyoujiCria_Click_1(object sender, RoutedEventArgs e)
        {
            this.HyoujiText1.Text = "";
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
            this.OutputCount = 0;
        }

        /// <summary>
        /// 順序表示テキスト変化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HyoujiText1_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            TextChange();
        }

        /// <summary>
        /// 表示順の文字変更時処理
        /// </summary>
        private void TextChange()
        {
            //テキスト1
            if (this.HyoujiText1.Text == "" && this.OutputImageFlag1 == false)
            {
                HyoujiImage1.Visibility = Visibility.Collapsed;
            }
            else
            {
                HyoujiImage1.Visibility = Visibility.Visible;
            }

            //テキスト2
            if (this.HyoujiText2.Text == "" && this.OutputImageFlag2 == false)
            {
                HyoujiImage2.Visibility = Visibility.Collapsed;
            }
            else
            {
                HyoujiImage2.Visibility = Visibility.Visible;
            }


            //テキスト3
            if (this.HyoujiText3.Text == "" && this.OutputImageFlag3 == false)
            {
                HyoujiImage3.Visibility = Visibility.Collapsed;
            }
            else
            {
                HyoujiImage3.Visibility = Visibility.Visible;
            }

            //テキスト3
            if (this.HyoujiText4.Text == "" && this.OutputImageFlag4 == false)
            {
                HyoujiImage4.Visibility = Visibility.Collapsed;
            }
            else
            {
                HyoujiImage4.Visibility = Visibility.Visible;
            }

            //テキスト3
            if (this.HyoujiText5.Text == "" && this.OutputImageFlag5 == false)
            {
                HyoujiImage5.Visibility = Visibility.Collapsed;
            }
            else
            {
                HyoujiImage5.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// 表示順序指定ボタン押下
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
            string selectItem = string.Empty;

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
            }
        }

        /// <summary>
        /// 表示イメージ1押下時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HyoujiImage1_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            OrderImageChange(1);
        }

        /// <summary>
        /// 表示イメージ2押下時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HyoujiImage2_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            OrderImageChange(2);
        }

        /// <summary>
        /// 表示イメージ3押下時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HyoujiImage3_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            OrderImageChange(3);
        }

        /// <summary>
        /// 表示イメージ4押下時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HyoujiImage4_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            OrderImageChange(4);
        }

        /// <summary>
        /// 表示イメージ5押下時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HyoujiImage5_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            OrderImageChange(5);
        }

        ///<summary>
        ///昇順･降順制御
        ///引数1:押下されたボタン番号
        ///</summary>
        private void OrderImageChange(int index)
        {
            switch (index)
            {
                case 1:
                    //ボタン1
                    if (this.OutputImageFlag1 == false)
                    {
                        BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/KouJyun.png", UriKind.Relative));
                        this.HyoujiImage1.Source = bi2;
                        this.OutputImageFlag1 = true;
                    }
                    else
                    {
                        BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/SyouJyun.png", UriKind.Relative));
                        this.HyoujiImage1.Source = bi2;
                        this.OutputImageFlag1 = false;
                    }

                    break;
                case 2:
                    //ボタン2
                    if (this.OutputImageFlag2 == false)
                    {
                        BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/KouJyun.png", UriKind.Relative));
                        this.HyoujiImage2.Source = bi2;
                        this.OutputImageFlag2 = true;
                    }
                    else
                    {
                        BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/SyouJyun.png", UriKind.Relative));
                        this.HyoujiImage2.Source = bi2;
                        this.OutputImageFlag2 = false;
                    }

                    break;
                case 3:
                    //ボタン3
                    if (this.OutputImageFlag3 == false)
                    {
                        BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/KouJyun.png", UriKind.Relative));
                        this.HyoujiImage3.Source = bi2;
                        this.OutputImageFlag3 = true;
                    }
                    else
                    {
                        BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/SyouJyun.png", UriKind.Relative));
                        this.HyoujiImage3.Source = bi2;
                        this.OutputImageFlag3 = false;
                    }
                    break;
                case 4:
                    //ボタン4
                    if (this.OutputImageFlag4 == false)
                    {
                        BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/KouJyun.png", UriKind.Relative));
                        this.HyoujiImage4.Source = bi2;
                        this.OutputImageFlag4 = true;
                    }
                    else
                    {
                        BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/SyouJyun.png", UriKind.Relative));
                        this.HyoujiImage4.Source = bi2;
                        this.OutputImageFlag4 = false;
                    }
                    break;
                case 5:
                    //ボタン4
                    if (this.OutputImageFlag5 == false)
                    {
                        BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/KouJyun.png", UriKind.Relative));
                        this.HyoujiImage5.Source = bi2;
                        this.OutputImageFlag5 = true;
                    }
                    else
                    {
                        BitmapImage bi2 = new BitmapImage(new Uri(@"../Images/SyouJyun.png", UriKind.Relative));
                        this.HyoujiImage5.Source = bi2;
                        this.OutputImageFlag5 = false;
                    }
                    break;
            }
        }

        /// <summary>
        /// バージョン3　ComboBoxでの表示の切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PickUplan3ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            
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
        /// F4 リボン詳細表示ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonSyousaiHyouji_Click_1(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("詳細表示ボタンが押されました。");
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
