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
using System.ComponentModel;
using System.Data;

using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;


namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 担当者マスタ入力
    /// </summary>
    public partial class MST09010 : WindowMasterMainteBase
    {
        #region 定数定義
        private const string TargetTableNm = "M07_KEI";
        private const string UpdateTable = "M07_KEI_UP";
        private const string DeleteTable = "M07_KEI_DEL";
        private const string GetNextID = "M07_KEI_NEXT";
        #endregion

        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;
        #region "権限関係"
        CommonConfig ccfg = null;
        #endregion

        //<summary>
        //画面固有設定項目のクラス定義
        //※ 必ず public で定義する。
        //</summary>
        public class ConfigMST09010 : FormConfigBase
        {
            //public bool[] 表示順方向 { get; set; }
            /// コンボボックスの位置
            public int 集計区分_Combo { get; set; }
        }

        /// ※ 必ず public で定義する。
        public ConfigMST09010 frmcfg = null;

        #endregion

        #region バインド用変数

        private string _経費項目ID = string.Empty;
        public string 経費項目ID
        {
            get { return this._経費項目ID; }
            set { this._経費項目ID = value; NotifyPropertyChanged(); }
        }
        private string _経費項目名 = string.Empty;
        public string 経費項目名
        {
            get { return this._経費項目名; }
            set { this._経費項目名 = value; NotifyPropertyChanged(); }
        }
        private int _固定変動区分 = 0;
        public int 固定変動区分
        {
            get { return this._固定変動区分; }
            set { this._固定変動区分 = value; NotifyPropertyChanged(); }
        }

        private int _編集区分 = 0;
        public int 編集区分
        {
            get { return this._編集区分; }
            set { this._編集区分 = value; NotifyPropertyChanged(); }
        }
        private int _グリーン区分 = 0;
        public int グリーン区分
        {
            get { return this._グリーン区分; }
            set { this._グリーン区分 = value; NotifyPropertyChanged(); }
        }
        private int _経費区分 = 0;
        public int 経費区分
        {
            get { return this._経費区分; }
            set { this._経費区分 = value; NotifyPropertyChanged(); }
        }

        // 経費区分プロパティ
        private DataView _productGroupView;
        public DataView ProductGroupView
        {
            get { return this._productGroupView; }
            set { this._productGroupView = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region MST09010

        /// <summary>
        /// 担当者マスタ入力
        /// </summary>
        public MST09010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #endregion

        #region Load時

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            // 権限設定を呼び出す(ucfgを取得した後のに入れる)
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            // 登録ボタン設定
            if (!権限Get.Authority_Update_Button(ccfg, this.GetType().Name))
            {
                DataUpdateVisible = System.Windows.Visibility.Hidden;
            }
            frmcfg = (ConfigMST09010)ucfg.GetConfigValue(typeof(ConfigMST09010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST09010();
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
            }
            #endregion
            //string[] Keihi_items = { "1:車輌費", "2:保険費", "3:人件費", "4:燃料費", "5:修理費", "6:諸経費" };
            //foreach (string Keihi_item in Keihi_items)
            //{
            //    // 項目を追加する
            //    ComboKeihi.Items.Add(Keihi_item);
            //}

            AppCommon.SetutpComboboxList(this.cmbKeihi, false);

            ScreenClear();

            base.MasterMaintenanceWindowList.Add("M07_KEI", new List<Type> { null, typeof(SCH09010) });

        }

        #endregion

        #region 画面初期化

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        private void ScreenClear()
        {

            経費項目ID = null;
            経費項目名 = string.Empty;
            経費区分 = 0;
            固定変動区分 = 0;
            編集区分 = 0;
            グリーン区分 = 0;



            this.MaintenanceMode = string.Empty;
            //キーのみtrue
            ChangeKeyItemChangeable(true);
            //ボタンはFalse
            btnEnableChange(true);

            ResetAllValidation();

            SetFocusToTopControl();

        }

        #endregion

        #region リボン

        /// <summary>
        /// F1 マスタ検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF1Key(object sender, KeyEventArgs e)
        {
            try
            {
                ViewBaseCommon.CallMasterSearch(this, this.MasterMaintenanceWindowList);
            }
            catch (Exception ex)
            {
                appLog.Error("検索画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }
        }

        /// <summary>
        /// F2 マスタメンテ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF2Key(object sender, KeyEventArgs e)
        {
            try
            {
                ViewBaseCommon.CallMasterMainte(this.MasterMaintenanceWindowList);
            }
            catch (Exception ex)
            {
                appLog.Error("マスターメンテ画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }
        }

        /// <summary>
        /// F8 リスト一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            MST09020 view = new MST09020();
            view.ShowDialog(this);
        }

        /// <summary>
        /// F9 データ登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            try
            {
                int i経費項目ID = 0;

                if (string.IsNullOrEmpty(経費項目ID))
                {
                    this.ErrorMessage = "経費項目IDは入力必須項目です。";
                    MessageBox.Show("経費項目IDは入力必須項目です。");
                    return;
                }

                if (string.IsNullOrEmpty(経費項目名))
                {
                    this.ErrorMessage = "経費項目名は入力必須項目です。";
                    MessageBox.Show("経費項目名は入力必須項目です。");
                    return;
                }

                if (!int.TryParse(経費項目ID, out i経費項目ID))
                {
                    this.ErrorMessage = "経費項目IDの入力形式が不正です。";
                    MessageBox.Show("経費項目IDの入力形式が不正です。");
                    return;
                }

                if (!base.CheckAllValidation())
                {
                    this.ErrorMessage = "入力内容に誤りがあります。";
                    MessageBox.Show("入力内容に誤りがあります。");
                    SetFocusToTopControl();
                    return;
                }
                var yesno = MessageBox.Show("入力内容を登録しますか？", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (yesno == MessageBoxResult.Yes)
                {
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, UpdateTable, new object[] { i経費項目ID
                                                                                                            ,経費項目名
                                                                                                            ,経費区分
                                                                                                            ,固定変動区分
                                                                                                            ,編集区分
                                                                                                            ,グリーン区分
                                                                                                            ,this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false
                                                                                                            ,false
                                                                                                            }));
                }

            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// F10 入力取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("保存せずに入力を取り消してよろしいですか？"
                             , "確認"
                             , MessageBoxButton.YesNo
                             , MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                this.ScreenClear();
            }
        }

        /// <summary>
        /// F11 終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF11Key(object sender, KeyEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// F12　削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF12Key(object sender, KeyEventArgs e)
        {

            if (string.IsNullOrEmpty(経費項目ID))
            {
                this.ErrorMessage = "登録内容がありません。";
                MessageBox.Show("登録内容がありません。");
                return;
            }

            if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
            {
                this.ErrorMessage = "新規登録データは削除できません。";
                MessageBox.Show("新規登録データは削除できません。");
                SetFocusToTopControl();
                return;
            }

            MessageBoxResult result = MessageBox.Show("表示されている情報を削除しますか？"
                             , "確認"
                             , MessageBoxButton.YesNo
                             , MessageBoxImage.Question
                             , MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
            {

                int i経費項目ID = 0;
                if (!int.TryParse(経費項目ID, out i経費項目ID))
                {
                    this.ErrorMessage = "経費項目IDの入力形式が不正です。";
                    MessageBox.Show("経費項目IDの入力形式が不正です。");
                    return;
                }

                //最後尾検索
                base.SendRequest(new CommunicationObject(MessageType.UpdateData, DeleteTable, new object[] { i経費項目ID }));
            }

        }
        #endregion

        #region 便利リンク

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

        #region 受信系処理
        /// <summary>
        /// データ受信メソッド
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            try
            {
                var data = message.GetResultData();

                if (data is DataTable)
                {
                    DataTable tbl = data as DataTable;

                    switch (message.GetMessageName())
                    {
                        //経費項目データ取得
                        case TargetTableNm:
                            if (tbl.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(tbl.Rows[0]["削除日付"].ToString()))
                                {
                                    //this.ErrorMessage = "既に削除されているデータです。";
                                    //MessageBox.Show("既に削除されているデータです。");

                                    MessageBoxResult result = MessageBox.Show("既に削除されているデータです。\nこのコードを復元しますか？",
                                                                                                                    "質問",
                                                                                                                   MessageBoxButton.YesNo,
                                                                                                                   MessageBoxImage.Exclamation,
                                                                                                                   MessageBoxResult.No);

                                    if (result == MessageBoxResult.No)
                                    {
                                        return;
                                    }

                                    //return;
                                }
                                SetTblData(tbl);

                            }

                            if (string.IsNullOrEmpty(経費項目名))
                            {
                                //新規ステータス表示
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                            }
                            else
                            {
                                //編集ステータス表示
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
                            }


                            //キーをfalse
                            ChangeKeyItemChangeable(false);
                            SetFocusToTopControl();


                            break;

                        case UpdateTable:
                            ScreenClear();
                            break;
                        case DeleteTable:
                            ScreenClear();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (message.GetMessageName())
                    {
                        case GetNextID:
                            if (data is int)
                            {
                                int iNextCode = (int)data;
                                経費項目ID = iNextCode.ToString();
                                ChangeKeyItemChangeable(false);
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                                SetFocusToTopControl();
                            }
                            break;

                        case UpdateTable:

                            if ((int)data == -1)
                            {
                                MessageBoxResult result = MessageBox.Show("経費項目ID: " + 経費項目ID + "は既に使われています。\n自動採番して登録しますか？",
                                                                                                                "質問",
                                                                                                               MessageBoxButton.YesNo,
                                                                                                               MessageBoxImage.Exclamation,
                                                                                                               MessageBoxResult.No);

                                if (result == MessageBoxResult.No)
                                {
                                    return;
                                }

                                base.SendRequest(new CommunicationObject(MessageType.UpdateData, UpdateTable, new object[] { 経費項目ID
                                                                                                            ,経費項目名
                                                                                                            ,経費区分
                                                                                                            ,固定変動区分
                                                                                                            ,編集区分
                                                                                                            ,グリーン区分
                                                                                                            ,this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false
                                                                                                            ,true
                                                                                                            }));


                            }
                            ScreenClear();
                            break;

                        case DeleteTable:
                            ScreenClear();
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }
        }

        /// <summary>
        /// データエラー受信メソッド
        /// </summary>
        /// <param name="message"></param>
        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            MessageBox.Show(ErrorMessage);
        }
        #endregion

        #region SetTblData

        /// <summary>
        /// テーブルデータを各変数に代入
        /// </summary>
        /// <param name="tbl"></param>
        private void SetTblData(DataTable tbl)
        {

            経費項目ID = tbl.Rows[0]["経費項目ID"].ToString();
            経費項目名 = tbl.Rows[0]["経費項目名"].ToString();
            if (!int.TryParse(tbl.Rows[0]["経費区分"].ToString(), out _経費区分))
            {
                経費区分 = 0;
            }
            else
            {
                経費区分 = AppCommon.IntParse(tbl.Rows[0]["経費区分"].ToString());
            }
            固定変動区分 = AppCommon.IntParse(tbl.Rows[0]["固定変動区分"].ToString());
            編集区分 = AppCommon.IntParse(tbl.Rows[0]["編集区分"].ToString());
            グリーン区分 = AppCommon.IntParse(tbl.Rows[0]["グリーン区分"].ToString());


        }

        #endregion

        #region コード送りボタン
        /// <summary>
        /// 最初のIDを表示するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FistIdButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                //先頭データ検索
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 0 }));
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 前のIDを表示するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeforeIdButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int i経費項目ID = 0;

                if (string.IsNullOrEmpty(経費項目ID))
                {
                    //先頭データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 0 }));
                }
                else
                {

                    if (int.TryParse(経費項目ID, out i経費項目ID))
                    {
                        //次データ検索
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { i経費項目ID, -1 }));
                    }

                }
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 次データを検索する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextIdButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int i経費項目ID = 0;

                if (string.IsNullOrEmpty(経費項目ID))
                {
                    //先頭データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 0 }));
                }
                else
                {

                    if (int.TryParse(経費項目ID, out i経費項目ID))
                    {
                        //次データ検索
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { i経費項目ID, 1 }));
                    }

                }
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 最後のデータを検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastIdButoon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //最後尾検索
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 1 }));
            }
            catch (Exception)
            {
                return;
            }
        }

        #endregion

        #region 自動裁判

        /// <summary>
        /// 担当者コードキーダウンイベント時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcLabelTwinTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {

                    try
                    {
                        int i経費項目ID = 0;

                        if (string.IsNullOrEmpty(経費項目ID))
                        {
                            //自動採番
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetNextID, new object[] { }));

                            return;
                        }

                        if (!int.TryParse(経費項目ID, out i経費項目ID))
                        {
                            this.ErrorMessage = "経費項目IDの入力形式が不正です。";
                            MessageBox.Show("経費項目IDの入力形式が不正です。");
                            return;
                        }

                        //経費項目データ検索
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { i経費項目ID, 0 }));
                    }
                    catch (Exception)
                    {
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                appLog.Debug("【Error:{0}***{1}】", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                this.ErrorMessage = ex.Message;
            }
        }

        #endregion

        #region ボタン
        /// <summary>
        /// 前後ボタンのEnableを変更する。
        /// </summary>
        /// <param name="pBool"></param>
        private void btnEnableChange(bool pBool)
        {
            FistIdButton.IsEnabled = pBool;
            BeforeIdButton.IsEnabled = pBool;
            NextIdButton.IsEnabled = pBool;
            LastIdButoon.IsEnabled = pBool;
        }



        private void Last_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    OnF9Key(sender, null);
                }
            }
            catch (Exception)
            {
                return;
            }
        }


        #endregion

        #region Mindoow_Closed
        //画面が閉じられた時、データを保持する
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }
        #endregion
    }

}
