﻿using System;
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
    /// 部門月別合計表画面
    /// </summary>
    public partial class NNG08010 : WindowReportBase
    {
        // データアクセス定義名
        private const string DataAccessName = "定義名";

        // データバインド用変数
        private string _部門ピックアップ = string.Empty;
        public string 部門ピックアップ
        {
            set
            {
                _部門ピックアップ = value;
                NotifyPropertyChanged();
            }
            get { return _部門ピックアップ; }
        }
        private string _部門From = string.Empty;
        public string 部門From
        {
            set
            {
                _部門From = value;
                NotifyPropertyChanged();
            }
            get { return _部門From; }
        }
        private string _部門To = string.Empty;
        public string 部門To
        {
            set
            {
                _部門To = value;
                NotifyPropertyChanged();
            }
            get { return _部門To; }
        }

        private string _作成年月 = string.Empty;
        public string 作成年月
        {
            set
            {
                _作成年月 = value;
                NotifyPropertyChanged();
            }
            get { return _作成年月; }
        }

        private string _表示区分 = string.Empty;
        public string 表示区分
        {
            set
            {
                _表示区分 = value;
                NotifyPropertyChanged();
            }
            get { return _表示区分; }
        }

        private bool _過去印刷 = false;
        public bool 過去印刷
        {
            set
            {
                _過去印刷 = value;
                NotifyPropertyChanged();
            }
            get { return _過去印刷; }
        }
        
        private string _表示順序 = "0";
        public string 表示順序
        {
            set
            {
                _表示順序 = value;
                NotifyPropertyChanged();
            }
            get { return _表示順序; }
        }

        
        /// <summary>
        /// 部門月別合計表画面
        /// </summary>
        public NNG08010()
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
