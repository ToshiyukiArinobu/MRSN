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
    /// 燃料単価マスタ入力
    /// </summary>
    public partial class MST14010 : WindowMasterMainteBase
    {
        #region 画面設定項目
        /// <summary>
        /// ユーザ設定項目
        /// </summary>
        UserConfig ucfg = null;
        #region "権限関係"
        CommonConfig ccfg = null;
        #endregion

        /// <summary>
        /// 画面固有設定項目のクラス定義
        /// ※ 必ず public で定義する。
        /// </summary>
        public class ConfigMST14010 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST14010 frmcfg = null;

        #endregion

        #region 定数定義
        private const string TargetTableNm = "M91_OTAN";
        private const string UpdateTable = "M91_OTAN_UP";
        private const string DeleteTable = "M91_OTAN_DEL";
        private const string M01_KEITableNm = "M01_KEI";
        #endregion

        #region バインド用変数

        private int _取引区分 = 3;
        public int 取引区分
        {
            get { return this._取引区分; }
            set { this._取引区分 = value; NotifyPropertyChanged(); }
        }

        private int? _支払先KEY;
        public int? 支払先KEY
        {
            get { return this._支払先KEY; }
            set { this._支払先KEY = value; NotifyPropertyChanged(); }
        }
        private int? _得意先ID = null;
        public int? 得意先ID
        {
            get { return this._得意先ID; }
            set { this._得意先ID = value; NotifyPropertyChanged(); }
        }
        private DateTime? _適用開始年月日 = null;
        public DateTime? 適用開始年月日
        {
            get { return this._適用開始年月日; }
            set { this._適用開始年月日 = value; NotifyPropertyChanged(); }
        }

        private decimal? _燃料単価 = null;
        public decimal? 燃料単価
        {
            get { return this._燃料単価; }
            set { this._燃料単価 = value; NotifyPropertyChanged(); }
        }

        #endregion

        /// <summary>
        /// 燃料単価マスタ入力
        /// </summary>
        public MST14010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

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
            frmcfg = (ConfigMST14010)ucfg.GetConfigValue(typeof(ConfigMST14010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST14010();
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

            //画面サイズをタスクバーをのぞいた状態で表示させる
            //this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;


            ChangeKeyItemChangeable(true);
            //SetFocusToTopControl();

            ScreenClear();

            base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { typeof(MST01010), typeof(SCH01010) });
            base.MasterMaintenanceWindowList.Add("M91_OTAN", new List<Type> { null, typeof(SCH14010) });

        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        private void ScreenClear()
        {

            支払先KEY = null;
            得意先ID = null;
            適用開始年月日 = null;
            燃料単価 = null;

            this.MaintenanceMode = string.Empty;
            //キーのみtrue
            ChangeKeyItemChangeable(true);
            //ボタンはFalse
            btnEnableChange(false);

            ResetAllValidation();

            SetFocusToTopControl();
        }

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
            MST14020 view = new MST14020();
            view.ShowDialog(this);
        }

        /// <summary>
        /// F9 データ登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            Update();
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

            if (支払先KEY == null || 適用開始年月日 == null)
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
                             , MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {

                //最後尾検索
                base.SendRequest(new CommunicationObject(MessageType.UpdateData, DeleteTable, new object[] { 支払先KEY, 適用開始年月日 }));

            }
            
        }

        #endregion

        #region

        public void Update()
        {
            try
            {
                if (支払先KEY == null)
                {
                    this.ErrorMessage = "支払先IDは必須入力項目です。";
                    MessageBox.Show("支払先IDは必須入力項目です。");
                    return;
                }

                if (適用開始年月日 == null)
                {
                    this.ErrorMessage = "適用開始年月日の入力形式が不正です。";
                    MessageBox.Show("適用開始年月日の入力形式が不正です。");
                    return;
                }
                decimal d燃料単価;
                if (!decimal.TryParse(燃料単価.ToString(), out d燃料単価))
                {
                    this.ErrorMessage = "燃料単価の入力形式が不正です。";
                    MessageBox.Show("燃料単価の入力形式が不正です。");
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
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, UpdateTable, new object[] { 支払先KEY
                                                                                                            ,適用開始年月日
                                                                                                            ,燃料単価}));

                }
            }
            catch (Exception)
            {
                return;
            }
        }

        #endregion

        #region リボンボタン系

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

                        //燃料単価データ取得
                        case TargetTableNm:

                            if (!(支払先KEY > 0))
                            {
                                break;
                            }

                            if (tbl.Rows.Count > 0)
                            {
                                //編集ステータス表示
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
                                SetTblData(tbl);
                            }
                            else
                            {
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;

                                if (string.IsNullOrEmpty(適用開始年月日.ToString()))
                                {
                                    MessageBox.Show("適用開始年月日は必須入力項目です。");
                                    return;
                                }
                            }
                
                            //キーをfalse
                            ChangeKeyItemChangeable(false);
                            SetFocusToTopControl();
                            break;

                        case UpdateTable:
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
                        case UpdateTable:
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

        /// <summary>
        /// テーブルデータを各変数に代入
        /// </summary>
        /// <param name="tbl"></param>
        private void SetTblData(DataTable tbl)
        {
            string Henkan;
            DateTime Wk;
            Henkan = tbl.Rows[0]["得意先ID"].ToString();
            支払先KEY = AppCommon.IntParse(Henkan);
            Henkan = tbl.Rows[0]["適用開始年月日"].ToString();
            適用開始年月日 = DateTime.TryParse(Henkan , out Wk) ? Wk : DateTime.Today;
            Henkan = tbl.Rows[0]["適用開始年月日"].ToString();
            適用開始年月日 = DateTime.TryParse(Henkan , out Wk) ? Wk : DateTime.Today;
            Henkan = tbl.Rows[0]["燃料単価"].ToString();
            燃料単価 = AppCommon.DecimalParse(Henkan);
            
        }

        #region  経費先前次ボタン 現在使用してない

        /// <summary>
        /// 最初のIDを表示するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FistIdButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (LabelTextShiharaiId.IsEnabled)
                {
                    //先頭データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, M01_KEITableNm, new object[] { null, 0 }));
                }
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
                if (LabelTextShiharaiId.IsEnabled)
                {
                    //前データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, M01_KEITableNm, new object[] { 支払先KEY, -1 }));

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
                if (LabelTextShiharaiId.IsEnabled)
                {
                    //次データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, M01_KEITableNm, new object[] { 支払先KEY, 1 }));
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
                if (LabelTextShiharaiId.IsEnabled)
                {
                    //最後尾検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, M01_KEITableNm, new object[] { null, 1 }));
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        #endregion



        #region  燃料単価マスタ前次ボタン

        /// <summary>
        /// 最初のIDを表示するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FistIdButton2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(適用開始年月日 == null)
                {
                    //先頭データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 支払先KEY, null, 0 }));
                }
                else
                {
                    //先頭データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 支払先KEY, null, 0 }));
                }

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
        private void BeforeIdButton2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //前データ検索
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 支払先KEY, 適用開始年月日, -1 }));
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
        private void NextIdButton2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (適用開始年月日 == null)
                {
                    //次データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 支払先KEY, null, 0 }));
                }
                else
                {
                    //次データ検索
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 支払先KEY, 適用開始年月日, 1 }));
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
        private void LastIdButoon2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //最後尾検索
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 支払先KEY, null, 1 }));
            }
            catch (Exception)
            {
                return;
            }
        }

        #endregion


        /// <summary>
        /// 燃料単価コードキーダウンイベント時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void UcLabelTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    return;
        //    try
        //    {
        //        if (e.Key == Key.Enter)
        //        {
        //            try
        //            {
        //                if (支払先KEY == 0)
        //                {
        //                    MessageBox.Show("支払先コードは必須入力項目です。");
        //                    return;
        //                }
        //                //if (適用開始年月日 == null)
        //                //{
        //                //    MessageBox.Show("適用開始年月日は必須入力項目です。");
        //                //    return;
        //                //}

        //                if (!base.CheckAllValidation())
        //                {
        //                    MessageBox.Show("入力内容に誤りがあります。");
        //                    SetFocusToTopControl();
        //                    return;
        //                }

        //                //燃料単価マスタ
        //                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 支払先KEY, 適用開始年月日, 0 }));

        //            }
        //            catch (Exception ex)
        //            {
        //                appLog.Debug("【Error:{0}***{1}】", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
        //                this.ErrorMessage = ex.Message;
        //                return;
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        appLog.Debug("【Error:{0}***{1}】", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
        //        this.ErrorMessage = ex.Message;
        //    }
        //}


        /// <summary>
        /// 前後ボタンのEnableを変更する。
        /// </summary>
        /// <param name="pBool"></param>
        private void btnEnableChange(bool pBool)
        {
            /* 経費先のボタン
            FistIdButton.IsEnabled = pBool;
            BeforeIdButton.IsEnabled = pBool;
            NextIdButton.IsEnabled = pBool;
            LastIdButoon.IsEnabled = pBool;
            */

            FistIdButton2.IsEnabled = pBool;
            BeforeIdButton2.IsEnabled = pBool;
            NextIdButton2.IsEnabled = pBool;
            LastIdButoon2.IsEnabled = pBool;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            //画面が閉じられた時、データを保持する
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }

        private void txtboxdate_lostfocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (支払先KEY == 0)
                {
                    MessageBox.Show("支払先コードは必須入力項目です。");
                    return;
                }

                if (!base.CheckAllValidation())
                {
                    MessageBox.Show("入力内容に誤りがあります。");
                    SetFocusToTopControl();
                    return;
                }

                //燃料単価マスタ
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 支払先KEY, 適用開始年月日, 0 }));

            }
            catch (Exception ex)
            {
                appLog.Debug("【Error:{0}***{1}】", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                this.ErrorMessage = ex.Message;
                return;
            }
        }


        private void UcLabelTextBox_PreviewKeyDown1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (NenryoTanka.Text == string.Empty)
                {
                    燃料単価 = 0;
                }
                else
                {
                    decimal Decit;
                    if (decimal.TryParse(NenryoTanka.Text, out Decit) == true)
                    {
                        燃料単価 = Decit;
                    }
                }
                Update();
            }
        }

        private void LabelTextShiharaiId_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(支払先KEY == null)
                {
                    this.ErrorMessage = "支払先IDは必須入力項目です。";
                    MessageBox.Show("支払先IDは必須入力項目です。");
                    return;
                }
                btnEnableChange(true);
            }
        }

        private void txtboxdate_PreviewKeyDown(object sender, RoutedEventArgs e)
        {
                try
                {
                    string days = txtboxdate.Text;
                    DateTime TestDays;
                    if (DateTime.TryParse(days, out TestDays) == true)
                    {
                        適用開始年月日 = Convert.ToDateTime(days);
                    }
                    else
                    {
                        this.ErrorMessage = "入力内容に誤りがあります。";
                        return;
                    }
                    

                    if (!base.CheckAllValidation())
                    {
                        MessageBox.Show("入力内容に誤りがあります。");
                        SetFocusToTopControl();
                        return;
                    }

                    if (支払先KEY == 0)
                    {
                        MessageBox.Show("支払先コードは必須入力項目です。");
                        return;
                    }



                        if (適用開始年月日 != null)
                        {
                            //適用開始年月日がNULLの時、呼び出し・修正へ
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 支払先KEY, 適用開始年月日, 0 }));
                        }
                        else
                        {
                            //適用開始年月日がNULLの時、新規登録へ
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 支払先KEY, 適用開始年月日, 3 }));
                        }

                    

                }
                catch (Exception ex)
                {
                    appLog.Debug("【Error:{0}***{1}】", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                    this.ErrorMessage = ex.Message;
                    return;
                }

            
        }
    }
}
