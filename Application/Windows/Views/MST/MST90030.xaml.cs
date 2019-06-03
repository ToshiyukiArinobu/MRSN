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

using System.IO;
using System.Windows.Resources;
using Microsoft.Win32;
using GrapeCity.Windows.SpreadGrid;


namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 請求書デザイン画面
    /// </summary>
    public partial class MST90030 : WindowReportBase
    {
        #region 定数定義
        //取引先マスタ
        private const string SEARCH_CSV01 = "SEARCH_CSV01";
        //請求内訳マスタ
        private const string SEARCH_CSV02 = "SEARCH_CSV02";
        //発着地マスタ
        private const string SEARCH_CSV03 = "SEARCH_CSV03";
        //車輌マスタ
        private const string SEARCH_CSV04 = "SEARCH_CSV04";
        //乗務員マスタ
        private const string SEARCH_CSV05 = "SEARCH_CSV05";
        //車種マスタ
        private const string SEARCH_CSV06 = "SEARCH_CSV06";
        //商品マスタ
        private const string SEARCH_CSV07 = "SEARCH_CSV07";
        //摘要マスタ
        private const string SEARCH_CSV08 = "SEARCH_CSV08";
        #endregion

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;

        //<summary>
        //画面固有設定項目のクラス定義
        //※ 必ず public で定義する。
        //</summary>
        public class ConfigMST90030 : FormConfigBase
        {
            public string FillPass { get; set; }
        }

        /// ※ 必ず public で定義する。
        public ConfigMST90030 frmcfg = null;

        #endregion

        #region Binding
        private string _FillName = string.Empty;
        public string FillName
        {
            get { return this._FillName; }
            set { this._FillName = value; NotifyPropertyChanged(); }
        }
        #endregion

        #region MST90030()
        /// <summary>
        /// 得意先売上合計表
        /// </summary>
        public MST90030()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion

        #region LOADイベント
        /// <summary>
        /// 画面読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            frmcfg = (ConfigMST90030)ucfg.GetConfigValue(typeof(ConfigMST90030));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST90030();
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
                this.FillName = frmcfg.FillPass;
            }
            #endregion

            AppCommon.SetutpComboboxList(this.TableName, false);
        }
        #endregion

        #region エラー受信
        /// <summary>
        /// データアクセスエラー受信イベント
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            // 基底クラスのエラー受信イベントを呼び出します。
            base.OnReveivedError(message);
        }
        #endregion

        #region データ受信メソッド
        /// <summary>
        /// 取得データの正常受信時のイベント
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            var data = message.GetResultData();
            DataTable tbl = (data is DataTable) ? (data as DataTable) : null;

            switch (message.GetMessageName())
            {
                case SEARCH_CSV01:
                    OutPutCSV(tbl);
                    break;
                
                case SEARCH_CSV02:
                    OutPutCSV(tbl);
                    break;
                
                case SEARCH_CSV03:
                    OutPutCSV(tbl);
                    break;
                
                case SEARCH_CSV04:
                    OutPutCSV(tbl);
                    break;
                
                case SEARCH_CSV05:
                    OutPutCSV(tbl);
                    break;
                
                case SEARCH_CSV06:
                    OutPutCSV(tbl);
                    break;
                
                case SEARCH_CSV07:
                    OutPutCSV(tbl);
                    break;
                
                case SEARCH_CSV08:
                    OutPutCSV(tbl);
                    break;
                
            }
        }
        #endregion

        #region リボン

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF5Key(object sender, KeyEventArgs e)
        {
            string FileName = this.TableName.Text.ToString();
             MessageBoxResult result = MessageBox.Show(FileName + "をCSV出力します。"
                                    , "確認"
                                    , MessageBoxButton.YesNo
                                    , MessageBoxImage.Question);
             if (result == MessageBoxResult.Yes)
             {
                 switch (this.TableName.SelectedIndex)
                 {
                     case 0://取引先マスタ
                         base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_CSV01, new object[] { }));
                         break;

                     case 1://請求内訳マスタ
                         base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_CSV02, new object[] { }));
                         break;

                     case 2://発着地マスタ
                         base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_CSV03, new object[] { }));
                         break;

                     case 3://車輌マスタ
                         base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_CSV04, new object[] { }));
                         break;

                     case 4://乗務員マスタ
                         base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_CSV05, new object[] { }));
                         break;

                     case 5://車種マスタ
                         base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_CSV06, new object[] { }));
                         break;

                     case 6://商品マスタ
                         base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_CSV07, new object[] { }));
                         break;

                     case 7://適用マスタ
                         base.SendRequest(new CommunicationObject(MessageType.UpdateData, SEARCH_CSV08, new object[] { }));
                         break;
                 }
             }
        } 

        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Mindoow_Closed
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            frmcfg.FillPass = this.FillName;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion

        #region CSV出力

        /// <summary>
        /// CSVファイル出力
        /// </summary>
        /// <param name="tbl"></param>
        private void OutPutCSV(DataTable tbl)
        {
            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            //はじめに表示されるフォルダを指定する
            if (string.IsNullOrEmpty(FillName))
            {
                sfd.InitialDirectory = @"C:\";
            }
            else
            {
                //前回開いたフォルダを指定する
                sfd.InitialDirectory = FillName;
            }
            //[ファイルの種類]に表示される選択肢を指定する
            sfd.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
            //「CSVファイル」が選択されているようにする
            sfd.FilterIndex = 1;
            //タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //ファイルの保存先のパス取得
                FillName = sfd.FileName;

                //CSVファイル出力
                CSVData.SaveCSV(tbl, sfd.FileName, true, true, false, ',');
                MessageBox.Show("CSVファイルの出力が完了しました。");
                
            }
        }

        #endregion 
    }
}
