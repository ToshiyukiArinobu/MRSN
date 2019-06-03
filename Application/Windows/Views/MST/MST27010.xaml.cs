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
    public partial class MST27010 : WindowMasterMainteBase
    {
        #region 定数定義
        private const string TargetTableNm = "S02_YOSS";
        private const string UpdateTable = "S02_YOSS_UP";
        private const string DeleteTable = "S02_YOSS_DEL";
        private const string S12TableNm = "S12_YOSG";
        private const string S12UpdateTable = "S12_YOSG_UP";
        private const string S12DeleteTable = "S12_YOSG_DEL";

        private const string M01_TOKTableNm = "M_M01_TOK";
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
        public class ConfigMST26010 : FormConfigBase
        {
            //public bool[] 表示順方向 { get; set; }
            /// コンボボックスの位置
            public int 集計区分_Combo { get; set; }
        }

        /// ※ 必ず public で定義する。
        public ConfigMST26010 frmcfg = null;

        #endregion


        #region バインド用変数


        private DataRow _rowS02;
        public DataRow rowS02
        {
            get { return this._rowS02; }
            set
            {
                try
                {
                    this._rowS02 = value;
                    NotifyPropertyChanged();
                }
                catch (Exception)
                {
                }
            }
        }

        private DataTable _rowS02Data = new DataTable();
        public DataTable rowS02Data
        {
            get { return this._rowS02Data; }
            set
            {
                this._rowS02Data = value;
                if (value == null)
                {
                    this.rowS02 = null;
                }
                else
                {
                    if (value.Rows.Count > 0)
                    {
                        this.rowS02 = value.Rows[0];
                    }
                    else
                    {
                        this.rowS02 = value.NewRow();
                        value.Rows.Add(this.rowS02);
                    }
                }
                NotifyPropertyChanged();
            }
        }


        private DataRow _rowS12;
        public DataRow rowS12
        {
            get { return this._rowS12; }
            set
            {
                try
                {
                    this._rowS12 = value;
                    NotifyPropertyChanged();
                }
                catch (Exception)
                {
                }
            }
        }

        private DataTable _rowS12Data = new DataTable();
        public DataTable rowS12Data
        {
            get { return this._rowS12Data; }
            set
            {
                this._rowS12Data = value;
                if (value == null)
                {
                    this.rowS12 = null;
                }
                else
                {
                    if (value.Rows.Count > 0)
                    {
                        this.rowS12 = value.Rows[0];
                    }
                    else
                    {
                        this.rowS12 = value.NewRow();
                        value.Rows.Add(this.rowS12);
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

        private int _取引区分 = 4;
        public int 取引区分
        {
            get { return this._取引区分; }
            set { this._取引区分 = value; NotifyPropertyChanged(); }
        }

        #endregion

        /// <summary>
        /// 燃料単価マスタ入力
        /// </summary>
        public MST27010()
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
            frmcfg = (ConfigMST26010)ucfg.GetConfigValue(typeof(ConfigMST26010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST26010();
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

            ScreenClear();

            //得意先ID用
            base.MasterMaintenanceWindowList.Add("M01_ZEN_SHIHARAI", new List<Type> { typeof(MST01010), typeof(SCH01010) });
            //base.MasterMaintenanceWindowList.Add("S02_YOSS", new List<Type> { null, typeof(SCH14010) });

        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        private void ScreenClear()
        {

            rowS02 = null;
            rowS12 = null;

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
            //MST26020 view = new MST26020();
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
                    MessageBox.Show("得意先コードの入力形式が不正です。");
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

                rowS02["支払先KEY"] = 得意先ID;
                rowS02["集計年月"] = ((DateTime)処理年月).Year * 100 + ((DateTime)処理年月).Month;
                rowS02["回数"] = 回数;
                rowS02["締日"] = 締日;
                rowS12["支払先KEY"] = 得意先ID;
                rowS12["集計年月"] = ((DateTime)処理年月).Year * 100 + ((DateTime)処理年月).Month;
                rowS12["回数"] = 回数;
                rowS12["締日"] = 締日;

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
                                                                                                            this.rowS02
                                                                                                            }));
                    //データ登録
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, S12UpdateTable, new object[] { 
                                                                                                            this.rowS12
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

            if (得意先ID == null)
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
                base.SendRequest(new CommunicationObject(MessageType.UpdateData, S12DeleteTable, new object[] { 得意先KEY, 年月, 回数 }));
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
                                rowS02Data = tbl;
                                //新規ステータス表示
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                            }

                            //キーをfalse
                            ChangeKeyItemChangeable(false);
                            SetFocusToTopControl();

                            break;

                        //データ取得
                        case S12TableNm:


                            if (!(得意先ID > 0))
                            {
                                break;
                            }

                            if (tbl.Rows.Count > 0)
                            {
                                //編集ステータス表示
                                this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;

                                rowS12 = tbl.Rows[0];
                                //SetTblData(tbl);
                            }
                            else
                            {

                                rowS12Data = tbl;
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
                            rowS02 = tbl.Rows[0];

                            //キーをfalse
                            ChangeKeyItemChangeable(false);
                            SetFocusToTopControl();

                            break;
                        case S12TableNm:

                            rowS12 = tbl.Rows[0];

                            this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                            rowS12 = tbl.Rows[0];

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

            rowS02 = tbl.Rows[0];

            string Henkan;

            Henkan = tbl.Rows[0]["支払先KEY"].ToString();
            得意先KEY = AppCommon.IntParse(Henkan);
            Henkan = tbl.Rows[0]["集計年月"].ToString();
			DateTime wk = DateTime.Now;
			処理年月 = DateTime.TryParse(Henkan, out wk) ? wk : (DateTime?)null;
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
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, S12TableNm, new object[] { 得意先ID, 年月, 回数 }));

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
