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
    /// 乗務員月次経費入力画面
    /// </summary>
    public partial class JMI10010 : WindowReportBase
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

        private string _集計年月;
        public string 集計年月
        {
            get { return this._集計年月; }
            set
            {
                this._集計年月 = value;
                NotifyPropertyChanged();
            }
        }

        private string _運送収入;
        public string 運送収入
        {
            get { return this._運送収入; }
            set
            {
                this._運送収入 = value;
                NotifyPropertyChanged();
            }
        }

        private DataTable _mSTDataHendou;
        public DataTable MSTDataHendou
        {
            get { return this._mSTDataHendou; }
            set
            {
                this._mSTDataHendou = value;
                NotifyPropertyChanged();
            }
        }

        private DataTable _mSTDataJinken;
        public DataTable MSTDataJinken
        {
            get { return this._mSTDataJinken; }
            set
            {
                this._mSTDataJinken = value;
                NotifyPropertyChanged();
            }
        }

        private DataTable _mSTDataKotei;
        public DataTable MSTDataKotei
        {
            get { return this._mSTDataKotei; }
            set
            {
                this._mSTDataKotei = value;
                NotifyPropertyChanged();
            }
        }

        private string _小計B;
        public string 小計B
        {
            get { return this._小計B; }
            set
            {
                this._小計B = value;
                NotifyPropertyChanged();
            }
        }

        private string _小計C;
        public string 小計C
        {
            get { return this._小計C; }
            set
            {
                this._小計C = value;
                NotifyPropertyChanged();
            }
        }

        private string _小計D;
        public string 小計D
        {
            get { return this._小計D; }
            set
            {
                this._小計D = value;
                NotifyPropertyChanged();
            }
        }

        private string _限界利益;
        public string 限界利益
        {
            get { return this._限界利益; }
            set
            {
                this._限界利益 = value;
                NotifyPropertyChanged();
            }
        }

        private string _運転手直接費合計;
        public string 運転手直接費合計
        {
            get { return this._運転手直接費合計; }
            set
            {
                this._運転手直接費合計 = value;
                NotifyPropertyChanged();
            }
        }

        private string _直接利益;
        public string 直接利益
        {
            get { return this._直接利益; }
            set
            {
                this._直接利益 = value;
                NotifyPropertyChanged();
            }
        }

        private string _一般管理費;
        public string 一般管理費
        {
            get { return this._一般管理費; }
            set
            {
                this._一般管理費 = value;
                NotifyPropertyChanged();
            }
        }

        private string _当月利益;
        public string 当月利益
        {
            get { return this._当月利益; }
            set
            {
                this._当月利益 = value;
                NotifyPropertyChanged();
            }
        }
        private string _当月利益率;
        public string 当月利益率
        {
            get { return this._当月利益率; }
            set
            {
                this._当月利益率 = value;
                NotifyPropertyChanged();
            }
        }

        private string _稼動日数;
        public string 稼動日数
        {
            get { return this._稼動日数; }
            set
            {
                this._稼動日数 = value;
                NotifyPropertyChanged();
            }
        }

        private string _実車;
        public string 実車
        {
            get { return this._実車; }
            set
            {
                this._実車 = value;
                NotifyPropertyChanged();
            }
        }

        private string _空車;
        public string 空車
        {
            get { return this._空車; }
            set
            {
                this._空車 = value;
                NotifyPropertyChanged();
            }
        }

        private string _走行;
        public string 走行
        {
            get { return this._走行; }
            set
            {
                this._走行 = value;
                NotifyPropertyChanged();
            }
        }

        private string _消費量;
        public string 消費量
        {
            get { return this._消費量; }
            set
            {
                this._消費量 = value;
                NotifyPropertyChanged();
            }
        }

        private string _燃費;
        public string 燃費
        {
            get { return this._燃費; }
            set
            {
                this._燃費 = value;
                NotifyPropertyChanged();
            }
        }

        private string _収入単価;
        public string 収入単価
        {
            get { return this._収入単価; }
            set
            {
                this._収入単価 = value;
                NotifyPropertyChanged();
            }
        }

        private string _輸送単価;
        public string 輸送単価
        {
            get { return this._輸送単価; }
            set
            {
                this._輸送単価 = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 乗務員月次経費入力画面
        /// </summary>
        public JMI10010()
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
                    //照会
                    this.RibbonKensaku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonKensaku));
                    break;
                case Key.F11:
                    //終了
                    this.Syuuryou.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Syuuryou));
                    break;
                case Key.F5:
                    //削除
                    this.RibbonSakujyo.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonSakujyo));
                    break;
                case Key.F9:
                    //伝票登録
                    this.DenpyouToroku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.DenpyouToroku));
                    break;
                case Key.System:
                    if (e.SystemKey == Key.F10)
                    {
                        //入力取消
                        this.Torikesi.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Torikesi));
                    }
                    break;
            }
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
        /// F11　リボン終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Syuuryou_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// F5　削除ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonSakujyo_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("削除ボタンが押されました。");
        }

        /// <summary>
        /// F9　伝票登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DenpyouToroku_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("伝票登録ボタンが押されました。");
        }

        /// <summary>
        /// F10 入力取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Torikesi_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("入力取消ボタンが押されました。");
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
