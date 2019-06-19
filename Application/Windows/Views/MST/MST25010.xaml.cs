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
    /// 得意先月次集計Ｆ修正入力
    /// </summary>
    public partial class MST25010 : WindowMasterMainteBase
    {
        #region 定数定義
        private const string TargetTableNm = "S01_TOKS";
        private const string UpdateTable = "S01_TOKS_UP";
        private const string DeleteTable = "S01_TOKS_DEL"
            ;
        private const string S11TableNm = "S11_TOKG";
        private const string S11UpdateTable = "S11_TOKG_UP";
        private const string S11DeleteTable = "S11_TOKG_DEL";

        private const string M01_TOKTableNm = "M_M01_TOK";
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
        public class ConfigMST25010 : FormConfigBase
        {
        }

        /// ※ 必ず public で定義する。
        public ConfigMST25010 frmcfg = null;

        #endregion


        #region バインド用変数


        private DataRow _rowS01;
        public DataRow rowS01
        {
            get { return this._rowS01; }
            set
            {
                try
                {
                    this._rowS01 = value;
                    NotifyPropertyChanged();
                }
                catch (Exception)
                {
                }
            }
        }

        private DataTable _rowS01Data = new DataTable();
        public DataTable rowS01Data
        {
            get { return this._rowS01Data; }
            set
            {
                this._rowS01Data = value;
                if (value == null)
                {
                    this.rowS01 = null;
                }
                else
                {
                    if (value.Rows.Count > 0)
                    {
                        this.rowS01 = value.Rows[0];
                    }
                    else
                    {
                        this.rowS01 = value.NewRow();
                        value.Rows.Add(this.rowS01);
                    }
                }
                NotifyPropertyChanged();
            }
        }


        private DataRow _rowS11;
        public DataRow rowS11
        {
            get { return this._rowS11; }
            set
            {
                try
                {
                    this._rowS11 = value;
                    NotifyPropertyChanged();
                }
                catch (Exception)
                {
                }
            }
        }

        private DataTable _rowS11Data = new DataTable();
        public DataTable rowS11Data
        {
            get { return this._rowS11Data; }
            set
            {
                this._rowS11Data = value;
                if (value == null)
                {
                    this.rowS11 = null;
                }
                else
                {
                    if (value.Rows.Count > 0)
                    {
                        this.rowS11 = value.Rows[0];
                    }
                    else
                    {
                        this.rowS11 = value.NewRow();
                        value.Rows.Add(this.rowS11);
                    }
                }
                NotifyPropertyChanged();
            }
        }

        private int? _得意先KEY = null;
        public int? 得意先KEY
        {
            get { return this._得意先KEY; }
            set { this._得意先KEY = value; NotifyPropertyChanged(); }
        }
        private int? _得意先ID = null;
        public int? 得意先ID
        {
            get { return this._得意先ID; }
            set { this._得意先ID = value; NotifyPropertyChanged(); }
        }

        private int? _締日 = null;
        public int? 締日
        {
            get { return this._締日; }
            set { this._締日 = value; NotifyPropertyChanged(); }
        }

        private DateTime? _処理年月 = null;
        public DateTime? 処理年月
        {
            get { return this._処理年月; }
            set { this._処理年月 = value; NotifyPropertyChanged(); }
        }

        private int? _回数 = null;
        public int? 回数
        {
            get { return this._回数; }
            set { this._回数 = value; NotifyPropertyChanged(); }
        }

        private int _取引区分 = 1;
        public int 取引区分
        {
            get { return this._取引区分; }
            set { this._取引区分 = value; NotifyPropertyChanged(); }
        }

        #endregion

        /// <summary>
        /// 得意先月次集計Ｆ修正入力
        /// </summary>
        public MST25010()
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
            frmcfg = (ConfigMST25010)ucfg.GetConfigValue(typeof(ConfigMST25010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST25010();
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
            SetFocusToTopControl();
			ResetAllValidation();
            ScreenClear();

            //得意先ID用
            base.MasterMaintenanceWindowList.Add("M01_TOK_TOKU_SCH", new List<Type> { typeof(MST01010), typeof(SCH01010) });
            //base.MasterMaintenanceWindowList.Add("S01_TOKS", new List<Type> { null, typeof(SCH14010) });

        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        private void ScreenClear()
        {

            rowS01 = null;
            rowS11 = null;

            this.MaintenanceMode = string.Empty;
            //キーのみtrue
            ChangeKeyItemChangeable(true);
            //ボタンはFalse
            btnEnableChange(true);

            SetFocusToTopControl();

            this.ErrorMessage = string.Empty;
            ResetAllValidation();
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
            //MST25020 view = new MST25020();
            //view.ShowDialog(this);
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

                if (得意先ID == null)
                {
                    this.ErrorMessage = "得意先IDの入力形式が不正です。";
                    MessageBox.Show("得意先IDの入力形式が不正です。");
                    return;
                }

                if (処理年月 == null)
                {
                    this.ErrorMessage = "処理年月の入力形式が不正です。";
                    MessageBox.Show("処理年月の入力形式が不正です。");
                    return;
                }

                if (回数 == null)
                {
                    this.ErrorMessage = "回数の入力形式が不正です。";
                    MessageBox.Show("回数の入力形式が不正です。");
                    return;
                }

                rowS01["得意先KEY"] = 得意先ID;
                rowS01["集計年月"] = ((DateTime)処理年月).Year * 100 + ((DateTime)処理年月).Month;
                rowS01["回数"] = 回数;
                rowS01["締日"] = 締日;
                rowS11["得意先KEY"] = 得意先ID;
                rowS11["集計年月"] = ((DateTime)処理年月).Year * 100 + ((DateTime)処理年月).Month;
                rowS11["回数"] = 回数;
                rowS11["締日"] = 締日;

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
                    //データ登録[]
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, UpdateTable, new object[] { 
                                                                                                            this.rowS01
                                                                                                            }));
                    //データ登録
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, S11UpdateTable, new object[] { 
                                                                                                            this.rowS11
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

            if (得意先KEY == null || 処理年月 == null)
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

            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            if (得意先KEY == null)
            {
                this.ErrorMessage = "得意先IDの入力形式が不正です。";
                return;
            }

            if (処理年月 == null)
            {
                this.ErrorMessage = "適用開始年月日の入力形式が不正です。";
                return;
            }

            int? 年月;

            年月 = ((DateTime)処理年月).Year * 100 + ((DateTime)処理年月).Month;

            //データ削除
            base.SendRequest(new CommunicationObject(MessageType.UpdateData, DeleteTable, new object[] { 得意先KEY, 年月, 回数 }));
            //データ削除
            base.SendRequest(new CommunicationObject(MessageType.UpdateData, S11DeleteTable, new object[] { 得意先KEY, 年月, 回数 }));
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

                        //データ取得
                        case TargetTableNm:
                            if (!(得意先ID > 0))
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
                                rowS01Data = tbl;
                                //新規ステータス表示
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                            }

                            //キーをfalse
                            ChangeKeyItemChangeable(false);
                            SetFocusToTopControl();

                            break;

                        //データ取得
                        case S11TableNm:


                            if (!(得意先ID > 0))
                            {
                                break;
                            }

                            if (tbl.Rows.Count > 0)
                            {
                                //編集ステータス表示
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;

                                rowS11 = tbl.Rows[0];
                                //SetTblData(tbl);
                            }
                            else
                            {

                                rowS11Data = tbl;
                                //新規ステータス表示
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                            }

                            //キーをfalse
                            ChangeKeyItemChangeable(false);
                            SetFocusToTopControl();

                            break;

                        case UpdateTable:
                        case DeleteTable:
                            ScreenClear();
                            break;
                        case M01_TOKTableNm:
                            string Henkan;
                            Henkan = tbl.Rows[0]["Ｔ締日"].ToString();
                            締日 = AppCommon.IntParse(Henkan);
                            break;

                        default:
                            break;
                    }
                }
                else
                {

                    DataTable tbl = data as DataTable;
                    switch (message.GetMessageName())
                    {
                        case TargetTableNm:

                            this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                            rowS01 = tbl.Rows[0];

                            //キーをfalse
                            ChangeKeyItemChangeable(false);
                            SetFocusToTopControl();

                            break;
                        case S11TableNm:

                            rowS11 = tbl.Rows[0];

                            this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                            rowS11 = tbl.Rows[0];

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
            }
            catch (Exception ex)
            {
                int ErrorCode = ex.HResult;
                if (ErrorCode != -2146233033)
                {
                    this.ErrorMessage = ex.Message;
                }
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

            rowS01 = tbl.Rows[0];

            string Henkan;
            DateTime Wk;
            Henkan = tbl.Rows[0]["得意先KEY"].ToString();
            得意先KEY = AppCommon.IntParse(Henkan);
            Henkan = tbl.Rows[0]["集計年月"].ToString();
            //処理年月 = DateTime.TryParse(Henkan , out Wk) ? Wk : DateTime.Today;
            Henkan = tbl.Rows[0]["回数"].ToString();
            回数 = AppCommon.IntParse(Henkan);

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
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, M01_TOKTableNm, new object[] { null, 0 }));
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
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, M01_TOKTableNm, new object[] { 得意先KEY, -1 }));

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
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, M01_TOKTableNm, new object[] { 得意先KEY, 1 }));
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
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, M01_TOKTableNm, new object[] { null, 1 }));
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        #endregion



        #region  マスタ前次ボタン

        /// <summary>
        /// 最初のIDを表示するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FistIdButton2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //先頭データ検索
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 得意先KEY, null, 0 }));
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
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 得意先KEY, 処理年月, -1 }));
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
                //次データ検索
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 得意先KEY, 処理年月, 1 }));
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
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 得意先KEY, null, 1 }));
            }
            catch (Exception)
            {
                return;
            }
        }

        #endregion


        /// <summary>
        /// コードキーダウンイベント時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcLabelTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    try
                    {
                        if (得意先ID == 0)
                        {
                            MessageBox.Show("得意先IDは必須入力項目です。");
                            return;
                        }
                        if (処理年月 == null)
                        {
                            MessageBox.Show("処理年月は必須入力項目です。");
                            return;
                        }

                        if (!base.CheckAllValidation())
                        {
                            this.ErrorMessage = "入力内容に誤りがあります。";
                            MessageBox.Show("入力内容に誤りがあります。");
                            SetFocusToTopControl();
                            return;
                        }
                        int? 年月;

                        年月 = ((DateTime)処理年月).Year * 100 + ((DateTime)処理年月).Month;
                        年月 = AppCommon.IntParse(年月.ToString());

                        //マスタ表示
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 得意先ID, 年月, 回数 }));
                        //マスタ表示
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, S11TableNm, new object[] { 得意先ID, 年月, 回数 }));

                    }
                    catch (Exception ex)
                    {
                        appLog.Debug("【Error:{0}***{1}】", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                        this.ErrorMessage = ex.Message;
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

            //FistIdButton2.IsEnabled = pBool;
            //BeforeIdButton2.IsEnabled = pBool;
            //NextIdButton2.IsEnabled = pBool;
            //LastIdButoon2.IsEnabled = pBool;
        }


        private void UcTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
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


        private void LabelTextShiharaiId_LostFocus(object sender, RoutedEventArgs e)
        {
            //得意先データ取得
            base.SendRequest(new CommunicationObject(MessageType.RequestData, M01_TOKTableNm, new object[] { 得意先ID, 0 }));

        }

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
