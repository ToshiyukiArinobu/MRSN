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
    /// 基礎情報マスタ入力
    /// </summary>
    public partial class MST30010 : WindowMasterMainteBase
    {
        #region 定数定義
        private const string TargetTableNm = "M87_CNTL";
        private const string UpdateTable = "M87_CNTL_UP";
        private const string DeleteTable = "M87_CNTL_DEL";
        private const string GetNextID = "M87_CNTL_NEXT";

        private const string SyukinTableNm = "M78_SYK_List";
        private const string SyukinUpdateTable = "M78_SYK_UP";
        private const string GetComboList = "GetComboboxList";
        #endregion

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
        public class ConfigMST30010 : FormConfigBase
        {
        }
        /// ※ 必ず public で定義する。
        public ConfigMST30010 frmcfg = null;

        #endregion

        #region バインド用変数
        private int? _期首年月 = null;
        public int? 期首年月
        {
            get { return this._期首年月; }
            set { this._期首年月 = value; NotifyPropertyChanged(); }
        }
        private int? _得意先処理年月 = null;
        public int? 得意先処理年月
        {
            get { return this._得意先処理年月; }
            set { this._得意先処理年月 = value; NotifyPropertyChanged(); }
        }
        private int? _支払先処理年月 = null;
        public int? 支払先処理年月
        {
            get { return this._支払先処理年月; }
            set { this._支払先処理年月 = value; NotifyPropertyChanged(); }
        }
        private int? _乗務員処理年月 = null;
        public int? 乗務員処理年月
        {
            get { return this._乗務員処理年月; }
            set { this._乗務員処理年月 = value; NotifyPropertyChanged(); }
        }
        private int? _車輌処理年月 = null;
        public int? 車輌処理年月
        {
            get { return this._車輌処理年月; }
            set { this._車輌処理年月 = value; NotifyPropertyChanged(); }
        }
        private int? _更新年月 = null;
        public int? 更新年月
        {
            get { return this._更新年月; }
            set { this._更新年月 = value; NotifyPropertyChanged(); }
        }
        private int? _決算月 = null;
        public int? 決算月
        {
            get { return this._決算月; }
            set { this._決算月 = value; NotifyPropertyChanged(); }
        }
        private int? _売上消費税端数区分 = null;
        public int? 売上消費税端数区分
        {
            get { return this._売上消費税端数区分; }
            set { this._売上消費税端数区分 = value; NotifyPropertyChanged(); }
        }
        private int? _支払消費税端数区分 = null;
        public int? 支払消費税端数区分
        {
            get { return this._支払消費税端数区分; }
            set { this._支払消費税端数区分 = value; NotifyPropertyChanged(); }
        }
        private int? _金額計算端数区分 = null;
        public int? 金額計算端数区分
        {
            get { return this._金額計算端数区分; }
            set { this._金額計算端数区分 = value; NotifyPropertyChanged(); }
        }
        private int? _出力プリンター設定 = null;
        public int? 出力プリンター設定
        {
            get { return this._出力プリンター設定; }
            set { this._出力プリンター設定 = value; NotifyPropertyChanged(); }
        }
        private int? _得意先自社締日 = null;
        public int? 得意先自社締日
        {
            get { return this._得意先自社締日; }
            set { this._得意先自社締日 = value; NotifyPropertyChanged(); }
        }
        private int? _支払先自社締日 = null;
        public int? 支払先自社締日
        {
            get { return this._支払先自社締日; }
            set { this._支払先自社締日 = value; NotifyPropertyChanged(); }
        }
        private int? _運転者自社締日 = null;
        public int? 運転者自社締日
        {
            get { return this._運転者自社締日; }
            set { this._運転者自社締日 = value; NotifyPropertyChanged(); }
        }
        private int? _車輌自社締日 = null;
        public int? 車輌自社締日
        {
            get { return this._車輌自社締日; }
            set { this._車輌自社締日 = value; NotifyPropertyChanged(); }
        }
        private int? _自社支払日 = null;
        public int? 自社支払日
        {
            get { return this._自社支払日; }
            set { this._自社支払日 = value; NotifyPropertyChanged(); }
        }
        private int? _自社サイト = null;
        public int? 自社サイト
        {
            get { return this._自社サイト; }
            set { this._自社サイト = value; NotifyPropertyChanged(); }
        }
        private int? _未定区分 = null;
        public int? 未定区分
        {
            get { return this._未定区分; }
            set { this._未定区分 = value; NotifyPropertyChanged(); }
        }
        private int? _部門管理区分 = null;
        public int? 部門管理区分
        {
            get { return this._部門管理区分; }
            set { this._部門管理区分 = value; NotifyPropertyChanged(); }
        }
        private int? _自動学習区分 = null;
        public int? 自動学習区分
        {
            get { return this._自動学習区分; }
            set { this._自動学習区分 = value; NotifyPropertyChanged(); }
        }
        private int? _月次集計区分 = null;
        public int? 月次集計区分
        {
            get { return this._月次集計区分; }
            set { this._月次集計区分 = value; NotifyPropertyChanged(); }
        }
        private int? _距離転送区分 = null;
        public int? 距離転送区分
        {
            get { return this._距離転送区分; }
            set { this._距離転送区分 = value; NotifyPropertyChanged(); }
        }
        private int? _番号通知区分 = null;
        public int? 番号通知区分
        {
            get { return this._番号通知区分; }
            set { this._番号通知区分 = value; NotifyPropertyChanged(); }
        }
        private int? _通行料転送区分 = null;
        public int? 通行料転送区分
        {
            get { return this._通行料転送区分; }
            set { this._通行料転送区分 = value; NotifyPropertyChanged(); }
        }
        private int? _路線計算区分 = null;
        public int? 路線計算区分
        {
            get { return this._路線計算区分; }
            set { this._路線計算区分 = value; NotifyPropertyChanged(); }
        }
        private string _割増料金名１ = string.Empty;
        public string 割増料金名１
        {
            get { return this._割増料金名１; }
            set { this._割増料金名１ = value; NotifyPropertyChanged(); }
        }
        private string _割増料金名２ = string.Empty;
        public string 割増料金名２
        {
            get { return this._割増料金名２; }
            set { this._割増料金名２ = value; NotifyPropertyChanged(); }
        }
        private string _確認名称;
        public string 確認名称
        {
            get { return this._確認名称; }
            set { this._確認名称 = value; NotifyPropertyChanged(); }
        }
        private int? _得意先ID区分 = null;
        public int? 得意先ID区分
        {
            get { return this._得意先ID区分; }
            set { this._得意先ID区分 = value; NotifyPropertyChanged(); }
        }
        private int? _支払先ID区分 = null;
        public int? 支払先ID区分
        {
            get { return this._支払先ID区分; }
            set { this._支払先ID区分 = value; NotifyPropertyChanged(); }
        }
        private int? _乗務員ID区分 = null;
        public int? 乗務員ID区分
        {
            get { return this._乗務員ID区分; }
            set { this._乗務員ID区分 = value; NotifyPropertyChanged(); }
        }
        private int? _車輌ID区分 = null;
        public int? 車輌ID区分
        {
            get { return this._車輌ID区分; }
            set { this._車輌ID区分 = value; NotifyPropertyChanged(); }
        }
        private int? _車種ID区分 = null;
        public int? 車種ID区分
        {
            get { return this._車種ID区分; }
            set { this._車種ID区分 = value; NotifyPropertyChanged(); }
        }
        private int? _発着地ID区分 = null;
        public int? 発着地ID区分
        {
            get { return this._発着地ID区分; }
            set { this._発着地ID区分 = value; NotifyPropertyChanged(); }
        }
        private int? _品名ID区分 = null;
        public int? 品名ID区分
        {
            get { return this._品名ID区分; }
            set { this._品名ID区分 = value; NotifyPropertyChanged(); }
        }
        private int? _摘要ID区分 = null;
        public int? 摘要ID区分
        {
            get { return this._摘要ID区分; }
            set { this._摘要ID区分 = value; NotifyPropertyChanged(); }
        }
        private int? _Ｇ期首月日 = null;
        public int? Ｇ期首月日
        {
            get { return this._Ｇ期首月日; }
            set { this._Ｇ期首月日 = value; NotifyPropertyChanged(); }
        }
        private int? _Ｇ期末月日 = null;
        public int? Ｇ期末月日
        {
            get { return this._Ｇ期末月日; }
            set { this._Ｇ期末月日 = value; NotifyPropertyChanged(); }
        }
        private int? _請求書区分 = null;
        public int? 請求書区分
        {
            get { return this._請求書区分; }
            set { this._請求書区分 = value; NotifyPropertyChanged(); }
        }

        private DataTable _AttendanceData;
        public DataTable 出勤区分データ
        {
            get { return this._AttendanceData; }
            set { this._AttendanceData = value; NotifyPropertyChanged(); }
        }

        private DataRow _rowM99;
        public DataRow RowM99
        {
            get { return this._rowM99; }
            set { this._rowM99 = value; NotifyPropertyChanged(); }
        }

		private DataRow _rowM87;
        public DataRow RowM87
        {
            get { return this._rowM87; }
            set { this._rowM87 = value; NotifyPropertyChanged(); }
        }

        private DataTable _DataTable = new DataTable();
        public DataTable DataTable
        {
            get { return this._DataTable; }
            set
            {
                this._DataTable = value;
                if (value == null)
                {
                    this.RowM87 = null;
                }
                else
                {
                    if (value.Rows.Count > 0)
                    {
                        this.RowM87 = value.Rows[0];
                    }
                    else
                    {
                        this.RowM87 = value.NewRow();
                        value.Rows.Add(this.RowM87);
                    }
                }
                NotifyPropertyChanged();
            }
        }



        private string _配達エリアコード = string.Empty;
        public string 配達エリアコード
        {
            get { return this._配達エリアコード; }
            set
            {
                this._配達エリアコード = value;

                if (value == "0")
                {
                    this.配達エリアコード = string.Empty;
                }
                NotifyPropertyChanged();
            }
        }

        //確認名称を変更した際Combo99_listを再度メモリに入れ直し
        bool resCOMBO = false;

        #endregion

        /// <summary>
        /// 基礎情報マスタ入力
        /// </summary>
        public MST30010()
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
            frmcfg = (ConfigMST30010)ucfg.GetConfigValue(typeof(ConfigMST30010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST30010();
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

            //基礎情報データ検索
            base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 1, 0 }));
            base.SendRequest(new CommunicationObject(MessageType.RequestData, SyukinTableNm, new object[] { 1, 0 }));

            //ScreenClear();
            //base.MasterMaintenanceWindowList.Add("M87_CNTL", new List<Type> { null, typeof(SCH30010) });


            AppCommon.SetutpComboboxList(this.COMBO1, false);
            AppCommon.SetutpComboboxList(this.COMBO2, false);
            AppCommon.SetutpComboboxList(this.COMBO3, false);
            //AppCommon.SetutpComboboxList(this.COMBO4, false);
            AppCommon.SetutpComboboxList(this.COMBO5, false);
            AppCommon.SetutpComboboxList(this.COMBO6, false);
            AppCommon.SetutpComboboxList(this.COMBO7, false);
            AppCommon.SetutpComboboxList(this.COMBO8, false);
            AppCommon.SetutpComboboxList(this.COMBO9, false);
            //AppCommon.SetutpComboboxList(this.COMBO10, false);
            AppCommon.SetutpComboboxList(this.COMBO11, false);
            AppCommon.SetutpComboboxList(this.COMBO12, false);

            売上消費税端数区分 = 1;
            支払消費税端数区分 = 1;
            未定区分 = 1;
            部門管理区分 = 1;
            得意先ID区分 = 1;
            支払先ID区分 = 1;
            乗務員ID区分 = 1;
            車輌ID区分 = 1;
            車種ID区分 = 1;
            発着地ID区分 = 1;
            品名ID区分 = 1;
            摘要ID区分 = 1;
            売上消費税端数区分 = 1;
            支払消費税端数区分 = 1;
            金額計算端数区分 = 1;
            出力プリンター設定 = 1;
            自動学習区分 = 1;
            月次集計区分 = 1;
            距離転送区分 = 1;
            番号通知区分 = 1;
            通行料転送区分 = 1;
            路線計算区分 = 1;

			SetFocusToTopControl();
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
            base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 1, 0 }));
            base.SendRequest(new CommunicationObject(MessageType.RequestData, SyukinTableNm, new object[] { 1, 0 }));

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
        }

        /// <summary>
        /// F9 データ登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            Update(1);
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
                //基礎情報データ検索
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { 1, 0 }));
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

        }

        #endregion

        #region  便利リンク
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
                DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
                if (data is DataTable)
                {
                    switch (message.GetMessageName())
                    {
                        //基礎情報データ取得
                        case TargetTableNm:
                            if (tbl.Rows.Count > 0)
                            {
                                SetTblData(tbl);
                            }
                            else
                            {
                                Update(0);
                            }
                            break;
                        case SyukinTableNm:
                            出勤区分データ = tbl;
                            break;
                        case UpdateTable:
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetComboList, null));
                            break;
                        case GetComboList:
                            (this.viewsCommData.AppData as AppCommonData).codedatacollection = tbl;
                            resCOMBO = true;
                            this.Close();
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
                                //int iNextCode = (int)data;
                                //基礎情報コード = iNextCode.ToString();
                                //ChangeKeyItemChangeable(false);
                                //this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                                //SetFocusToTopControl();
                            }
                            break;
                        case UpdateTable:
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetComboList, null));
                            break;
                        case GetComboList:
                            (this.viewsCommData.AppData as AppCommonData).codedatacollection = tbl;
                            resCOMBO = true;
                            this.Close();
							break;
                        case DeleteTable:
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
            bool result;
            int output;
            result = int.TryParse(tbl.Rows[0]["期首年月"].ToString(), out output);
            期首年月 = output;
            result = int.TryParse(tbl.Rows[0]["期首年月"].ToString(), out output);
            得意先処理年月 = output;
            result = int.TryParse(tbl.Rows[0]["支払先管理処理年月"].ToString(), out output);
            支払先処理年月 = output;
            result = int.TryParse(tbl.Rows[0]["運転者管理処理年月"].ToString(), out output);
            乗務員処理年月 = output;
            result = int.TryParse(tbl.Rows[0]["車輌管理処理年月"].ToString(), out output);
            車輌処理年月 = output;
            result = int.TryParse(tbl.Rows[0]["更新年月"].ToString(), out output);
            更新年月 = output;
            result = int.TryParse(tbl.Rows[0]["決算月"].ToString(), out output);
            決算月 = output;
            result = int.TryParse(tbl.Rows[0]["売上消費税端数区分"].ToString(), out output);
            売上消費税端数区分 = output;
            result = int.TryParse(tbl.Rows[0]["支払消費税端数区分"].ToString(), out output);
            支払消費税端数区分 = output;
            result = int.TryParse(tbl.Rows[0]["金額計算端数区分"].ToString(), out output);
            金額計算端数区分 = output;
            result = int.TryParse(tbl.Rows[0]["出力プリンター設定"].ToString(), out output);
            出力プリンター設定 = output;
            result = int.TryParse(tbl.Rows[0]["得意先自社締日"].ToString(), out output);
            得意先自社締日 = output;
            result = int.TryParse(tbl.Rows[0]["支払先自社締日"].ToString(), out output);
            支払先自社締日 = output;
            result = int.TryParse(tbl.Rows[0]["運転者自社締日"].ToString(), out output);
            運転者自社締日 = output;
            result = int.TryParse(tbl.Rows[0]["車輌自社締日"].ToString(), out output);
            車輌自社締日 = output;
            result = int.TryParse(tbl.Rows[0]["自社支払日"].ToString(), out output);
            自社支払日 = output;
            result = int.TryParse(tbl.Rows[0]["自社サイト"].ToString(), out output);
            自社サイト = output;
            result = int.TryParse(tbl.Rows[0]["未定区分"].ToString(), out output);
            未定区分 = output;
            result = int.TryParse(tbl.Rows[0]["部門管理区分"].ToString(), out output);
            部門管理区分 = output;
            割増料金名１ = tbl.Rows[0]["割増料金名１"].ToString();
            割増料金名２ = tbl.Rows[0]["割増料金名２"].ToString();
            確認名称 = tbl.Rows[0]["確認名称"].ToString();
            result = int.TryParse(tbl.Rows[0]["得意先ID区分"].ToString(), out output);
            得意先ID区分 = output;
            result = int.TryParse(tbl.Rows[0]["支払先ID区分"].ToString(), out output);
            支払先ID区分 = output;
            result = int.TryParse(tbl.Rows[0]["乗務員ID区分"].ToString(), out output);
            乗務員ID区分 = output;
            result = int.TryParse(tbl.Rows[0]["車輌ID区分"].ToString(), out output);
            車輌ID区分 = output;
            result = int.TryParse(tbl.Rows[0]["車種ID区分"].ToString(), out output);
            車種ID区分 = output;
            result = int.TryParse(tbl.Rows[0]["発着地ID区分"].ToString(), out output);
            発着地ID区分 = output;
            result = int.TryParse(tbl.Rows[0]["品名ID区分"].ToString(), out output);
            品名ID区分 = output;
            result = int.TryParse(tbl.Rows[0]["摘要ID区分"].ToString(), out output);
            摘要ID区分 = output;
            result = int.TryParse(tbl.Rows[0]["期首年月"].ToString(), out output);
            期首年月 = output;
            result = int.TryParse(tbl.Rows[0]["売上消費税端数区分"].ToString(), out output);
            売上消費税端数区分 = output;
            result = int.TryParse(tbl.Rows[0]["支払消費税端数区分"].ToString(), out output);
            支払消費税端数区分 = output;
            result = int.TryParse(tbl.Rows[0]["金額計算端数区分"].ToString(), out output);
            金額計算端数区分 = output;
            result = int.TryParse(tbl.Rows[0]["出力プリンター設定"].ToString(), out output);
            出力プリンター設定 = output;
            result = int.TryParse(tbl.Rows[0]["自動学習区分"].ToString(), out output);
            自動学習区分 = output;
            result = int.TryParse(tbl.Rows[0]["月次集計区分"].ToString(), out output);
            月次集計区分 = output;
            result = int.TryParse(tbl.Rows[0]["距離転送区分"].ToString(), out output);
            距離転送区分 = output;
            result = int.TryParse(tbl.Rows[0]["番号通知区分"].ToString(), out output);
            番号通知区分 = output;
            result = int.TryParse(tbl.Rows[0]["通行料転送区分"].ToString(), out output);
            通行料転送区分 = output;
            result = int.TryParse(tbl.Rows[0]["路線計算区分"].ToString(), out output);
            路線計算区分 = output;
            result = int.TryParse(tbl.Rows[0]["Ｇ期首月日"].ToString(), out output);
            Ｇ期首月日 = output;
            result = int.TryParse(tbl.Rows[0]["Ｇ期末月日"].ToString(), out output);
            Ｇ期末月日 = output;
            result = int.TryParse(tbl.Rows[0]["請求書区分"].ToString(), out output);
            請求書区分 = output;
        }

        private void TryParse(string p, int? 期首年月)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 関連テーブルの読み込み
        /// </summary>
        private void GetSubData()
        {
            try
            {
                CommunicationObject[] comlist = {
												new CommunicationObject(MessageType.RequestData, SyukinTableNm),
											};
                SendRequest(comlist);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region 登録

        /// <summary>
        /// Update
        /// </summary>
        private void Update(int UpKbn)
        {

            try
            {

                if (!base.CheckAllValidation())
                {
                    this.ErrorMessage = "入力内容に誤りがあります。";
                    MessageBox.Show("入力内容に誤りがあります。");
                    SetFocusToTopControl();
                    return;
                }

                if (UpKbn == 1)
                {
                    var yesno = MessageBox.Show("入力内容を登録しますか？", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                    if (yesno == MessageBoxResult.Yes)
                    {

                        //最後尾検索
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, UpdateTable, new object[] { 
                                                            1,
                                                            得意先処理年月,
                                                            支払先処理年月,
                                                            車輌処理年月,
                                                            乗務員処理年月,
                                                            更新年月,
                                                            決算月,
                                                            得意先自社締日,
                                                            支払先自社締日,
                                                            運転者自社締日,
                                                            車輌自社締日,
                                                            自社支払日,
                                                            自社サイト,
                                                            未定区分,
                                                            部門管理区分,
                                                            割増料金名１,
                                                            割増料金名２,
                                                            確認名称,
                                                            得意先ID区分,
                                                            支払先ID区分,
                                                            乗務員ID区分,
                                                            車輌ID区分,
                                                            車種ID区分,
                                                            発着地ID区分,
                                                            品名ID区分,
                                                            摘要ID区分,
                                                            期首年月,
                                                            売上消費税端数区分,
                                                            支払消費税端数区分,
                                                            金額計算端数区分,
                                                            出力プリンター設定,
                                                            自動学習区分,
                                                            月次集計区分,
                                                            距離転送区分,
                                                            番号通知区分,
                                                            通行料転送区分,
                                                            路線計算区分,
                                                            Ｇ期首月日,
                                                            Ｇ期末月日,
                                                            請求書区分
                        }));

                        //最後尾検索

                        CommunicationObject com = new CommunicationObject(MessageType.UpdateData, SyukinUpdateTable, this.出勤区分データ);
                        SendRequest(com);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, UpdateTable, new object[] { 
                                                            1,
                                                            得意先処理年月,
                                                            支払先処理年月,
                                                            車輌処理年月,
                                                            乗務員処理年月,
                                                            更新年月,
                                                            決算月,
                                                            得意先自社締日,
                                                            支払先自社締日,
                                                            運転者自社締日,
                                                            車輌自社締日,
                                                            自社支払日,
                                                            自社サイト,
                                                            未定区分,
                                                            部門管理区分,
                                                            割増料金名１,
                                                            割増料金名２,
                                                            得意先ID区分,
                                                            支払先ID区分,
                                                            乗務員ID区分,
                                                            車輌ID区分,
                                                            車種ID区分,
                                                            発着地ID区分,
                                                            品名ID区分,
                                                            摘要ID区分,
                                                            期首年月,
                                                            売上消費税端数区分,
                                                            支払消費税端数区分,
                                                            金額計算端数区分,
                                                            出力プリンター設定,
                                                            自動学習区分,
                                                            月次集計区分,
                                                            距離転送区分,
                                                            番号通知区分,
                                                            通行料転送区分,
                                                            路線計算区分,
                                                            Ｇ期首月日,
                                                            Ｇ期末月日,
                                                            請求書区分}));

                    CommunicationObject com = new CommunicationObject(MessageType.UpdateData, SyukinUpdateTable, this.出勤区分データ);
                    SendRequest(com);

                }
            }
            catch (Exception)
            {
                this.ErrorMessage = "更新処理に失敗しました";
                return;
            }
          
        }
        #endregion

        /// <summary>
        /// 最終エンター
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcLabelTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Update(1);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //画面が閉じられた時、データを保持する
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }

    }
}
