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
    /// 得意先月次集計Ｆ修正
    /// </summary>
    public partial class MST25010 : WindowReportBase
    {
        // データアクセス定義名
        private const string DataAccessName = "定義名";

        #region データバインド用プロパティ
        private string _得意先コード = string.Empty;
        public string 得意先コード
        {
            get { return this._得意先コード; }
            set { this._得意先コード = value; NotifyPropertyChanged(); }
        }
        private string _得意先名 = string.Empty;
        public string 得意先名
        {
            get { return this._得意先名; }
            set { this._得意先名 = value; NotifyPropertyChanged(); }
        }
        private string _締日 = string.Empty;
        public string 締日
        {
            get { return this._締日; }
            set { this._締日 = value; NotifyPropertyChanged(); }
        }

        private DateTime _処理年月 = DateTime.MinValue;
        public DateTime 処理年月
        {
            get { return this._処理年月; }
            set { this._処理年月 = value; NotifyPropertyChanged(); }
        }

        private DateTime _締日集計日付開始 = DateTime.MinValue;
        public DateTime 締日集計日付開始
        {
            get { return this._締日集計日付開始; }
            set { this._締日集計日付開始 = value; NotifyPropertyChanged(); }
        }
        private DateTime _締日集計日付終了 = DateTime.MinValue;
        public DateTime 締日集計日付終了
        {
            get { return this._締日集計日付終了; }
            set { this._締日集計日付終了 = value; NotifyPropertyChanged(); }
        }

        private DateTime _月次集計日付開始 = DateTime.MinValue;
        public DateTime 月次集計日付開始
        {
            get { return this._月次集計日付開始; }
            set { this._月次集計日付開始 = value; NotifyPropertyChanged(); }
        }
        private DateTime _月次集計日付終了 = DateTime.MinValue;
        public DateTime 月次集計日付終了
        {
            get { return this._月次集計日付終了; }
            set { this._月次集計日付終了 = value; NotifyPropertyChanged(); }
        }

        private string _入金現金締日 = string.Empty;
        public string 入金現金締日
        {
            get { return this._入金現金締日; }
            set { this._入金現金締日 = value; NotifyPropertyChanged(); }
        }
        private string _入金現金月次 = string.Empty;
        public string 入金現金月次
        {
            get { return this._入金現金月次; }
            set { this._入金現金月次 = value; NotifyPropertyChanged(); }
        }
        
        private string _入金手形締日 = string.Empty;
        public string 入金手形締日
        {
            get { return this._入金手形締日; }
            set { this._入金手形締日 = value; NotifyPropertyChanged(); }
        }
        private string _入金手形月次 = string.Empty;
        public string 入金手形月次
        {
            get { return this._入金手形月次; }
            set { this._入金手形月次 = value; NotifyPropertyChanged(); }
        }
        
        private string _入金調整締日 = string.Empty;
        public string 入金調整締日
        {
            get { return this._入金調整締日; }
            set { this._入金調整締日 = value; NotifyPropertyChanged(); }
        }
        private string _入金調整月次 = string.Empty;
        public string 入金調整月次
        {
            get { return this._入金調整月次; }
            set { this._入金調整月次 = value; NotifyPropertyChanged(); }
        }

        private string _売上締日 = string.Empty;
        public string 売上締日
        {
            get { return this._売上締日; }
            set { this._売上締日 = value; NotifyPropertyChanged(); }
        }
        private string _売上月次 = string.Empty;
        public string 売上月次
        {
            get { return this._売上月次; }
            set { this._売上月次 = value; NotifyPropertyChanged(); }
        }

        private string _立替締日 = string.Empty;
        public string 立替締日
        {
            get { return this._立替締日; }
            set { this._立替締日 = value; NotifyPropertyChanged(); }
        }
        private string _立替月次 = string.Empty;
        public string 立替月次
        {
            get { return this._立替月次; }
            set { this._立替月次 = value; NotifyPropertyChanged(); }
        }

        private string _課税売上締日 = string.Empty;
        public string 課税売上締日
        {
            get { return this._課税売上締日; }
            set { this._課税売上締日 = value; NotifyPropertyChanged(); }
        }
        private string _課税売上月次 = string.Empty;
        public string 課税売上月次
        {
            get { return this._課税売上月次; }
            set { this._課税売上月次 = value; NotifyPropertyChanged(); }
        }

        private string _非課税売上締日 = string.Empty;
        public string 非課税売上締日
        {
            get { return this._非課税売上締日; }
            set { this._非課税売上締日 = value; NotifyPropertyChanged(); }
        }
        private string _非課税売上月次 = string.Empty;
        public string 非課税売上月次
        {
            get { return this._非課税売上月次; }
            set { this._非課税売上月次 = value; NotifyPropertyChanged(); }
        }

        private string _消費税締日 = string.Empty;
        public string 消費税締日
        {
            get { return this._消費税締日; }
            set { this._消費税締日 = value; NotifyPropertyChanged(); }
        }
        private string _消費税月次 = string.Empty;
        public string 消費税月次
        {
            get { return this._消費税月次; }
            set { this._消費税月次 = value; NotifyPropertyChanged(); }
        }
        
        private string _支払対象売上締日 = string.Empty;
        public string 支払対象売上締日
        {
            get { return this._支払対象売上締日; }
            set { this._支払対象売上締日 = value; NotifyPropertyChanged(); }
        }
        private string _支払対象売上月次 = string.Empty;
        public string 支払対象売上月次
        {
            get { return this._支払対象売上月次; }
            set { this._支払対象売上月次 = value; NotifyPropertyChanged(); }
        }

        private string _支払金額締日 = string.Empty;
        public string 支払金額締日
        {
            get { return this._支払金額締日; }
            set { this._支払金額締日 = value; NotifyPropertyChanged(); }
        }
        private string _支払金額月次 = string.Empty;
        public string 支払金額月次
        {
            get { return this._支払金額月次; }
            set { this._支払金額月次 = value; NotifyPropertyChanged(); }
        }

        private string _未定件数締日 = string.Empty;
        public string 未定件数締日
        {
            get { return this._未定件数締日; }
            set { this._未定件数締日 = value; NotifyPropertyChanged(); }
        }
        private string _未定件数月次 = string.Empty;
        public string 未定件数月次
        {
            get { return this._未定件数月次; }
            set { this._未定件数月次 = value; NotifyPropertyChanged(); }
        }

        private string _明細件数締日 = string.Empty;
        public string 明細件数締日
        {
            get { return this._明細件数締日; }
            set { this._明細件数締日 = value; NotifyPropertyChanged(); }
        }
        private string _明細件数月次 = string.Empty;
        public string 明細件数月次
        {
            get { return this._明細件数月次; }
            set { this._明細件数月次 = value; NotifyPropertyChanged(); }
        }
        #endregion
        /// <summary>
        /// 得意先月次集計Ｆ修正
        /// </summary>
        public MST25010()
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
                case Key.F6:
                    this.RibbonIChiran.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.RibbonIChiran));
                    break;
                case Key.F9:
                    this.Touroku.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Touroku));
                    break;
                case Key.F10:
                    this.Clear.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Clear));
                    break;
                case Key.F11:
                    this.Syuuryou.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Syuuryou));
                    break;
                case Key.F12:
                    this.Delete.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this.Delete));
                    break;
            }
        }

        #region リボン

        /// <summary>
        /// F1　リボン検索ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonKensaku_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("マスタ検索ボタンが押されました。");
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
        /// F6 マスタ一覧ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonIchiran_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("マスタ一覧ボタンが押されました。");
        }
        /// <summary>
        /// F9　マスタ登録ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Touroku_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("マスタ登録ボタンが押されました。");
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
        /// F10 リボン入力取消ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("入力取消ボタンが押されました。");
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
        /// F12　リボンマスタ削除ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("マスタ削除ボタンが押されました。");
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
        private void Purebyu_Click_1(object sender, RoutedEventArgs e)
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
