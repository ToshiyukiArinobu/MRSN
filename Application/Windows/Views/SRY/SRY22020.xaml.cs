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

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;
using KyoeiSystem.Application.Windows.Views;
using GrapeCity.Windows.SpreadGrid;
using KyoeiSystem.Framework.Windows.Controls;


namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 基礎情報マスタ入力
    /// </summary>
    public partial class SRY22020 : WindowMasterMainteBase
    {
        #region 定数定義
        private const string LOAD_SRY22010 = "LOAD_SRY22010";
        //*** 表示 ***//
        private const string LOAD_SRY22020 = "LOAD_SRY22020";
        //***　一括登録 ***//
        private const string INSERT_SRY22020 = "INSERT_SRY22020";
        //***  データ削除 ***//
        private const string DELETE_SRY22020 = "DELETE_SRY22020";
        //*** グローバル変数 ***//
        static public int Cnt = 0;

        #endregion

        #region バインド用変数

        public int? 初期車輌コード = null;
        public int? 初期作成年月 = null;
        public int? 初期点検日 = null;

        private string _作成年月 = string.Empty;
        public string 作成年月
        {
            get { return this._作成年月; }
            set { this._作成年月 = value; NotifyPropertyChanged(); }
        }

        private int _車輌ID;
        public int 車輌ID
        {
            get { return this._車輌ID; }
            set { this._車輌ID = value; NotifyPropertyChanged(); }
        }

        private int? _乗務員ID;
        public int? 乗務員ID
        {
            get { return this._乗務員ID; }
            set { this._乗務員ID = value; NotifyPropertyChanged(); }
        }

        private string _乗務員名;
        public string 乗務員名
        {
            get { return this._乗務員名; }
            set { this._乗務員名 = value; NotifyPropertyChanged(); }
        }

        private int _チェック日;
        public int チェック日
        {
            get { return this._チェック日; }
            set { this._チェック日 = value; NotifyPropertyChanged(); }
        }

        private int? _エアコン区分;
        public int? エアコン区分
        {
            get { return this._エアコン区分; }
            set { this._エアコン区分 = value; NotifyPropertyChanged(); }
        }

        private int? _異音区分;
        public int? 異音区分
        {
            get { return this._異音区分; }
            set { this._異音区分 = value; NotifyPropertyChanged(); }
        }

        private int? _排気区分;
        public int? 排気区分
        {
            get { return this._排気区分; }
            set { this._排気区分 = value; NotifyPropertyChanged(); }
        }

        private int? _燃費区分;
        public int? 燃費区分
        {
            get { return this._燃費区分; }
            set { this._燃費区分 = value; NotifyPropertyChanged(); }
        }

        private int? _その他区分;
        public int? その他区分
        {
            get { return this._その他区分; }
            set { this._その他区分 = value; NotifyPropertyChanged(); }
        }

        private string _エアコン備考;
        public string エアコン備考
        {
            get { return this._エアコン備考; }
            set { this._エアコン備考 = value; NotifyPropertyChanged(); }
        }

        private string _異音備考;
        public string 異音備考
        {
            get { return this._異音備考; }
            set { this._異音備考 = value; NotifyPropertyChanged(); }
        }

        private string _排気備考;
        public string 排気備考
        {
            get { return this._排気備考; }
            set { this._排気備考 = value; NotifyPropertyChanged(); }
        }

        private string _燃費備考;
        public string 燃費備考
        {
            get { return this._燃費備考; }
            set { this._燃費備考 = value; NotifyPropertyChanged(); }
        }

        private string _その他備考;
        public string その他備考
        {
            get { return this._その他備考; }
            set { this._その他備考 = value; NotifyPropertyChanged(); }
        }

        private DateTime? _指示日付;
        public DateTime? 指示日付
        {
            get { return this._指示日付; }
            set { this._指示日付 = value; NotifyPropertyChanged(); }
        }

        private string _整備指示;
        public string 整備指示
        {
            get { return this._整備指示; }
            set { this._整備指示 = value; NotifyPropertyChanged(); }
        }

        private DateTime? _部品日付;
        public DateTime? 部品日付
        {
            get { return this._部品日付; }
            set { this._部品日付 = value; NotifyPropertyChanged(); }
        }

        private string _整備部品;
        public string 整備部品
        {
            get { return this._整備部品; }
            set { this._整備部品 = value; NotifyPropertyChanged(); }
        }

        private DateTime? _結果日付;
        public DateTime? 結果日付
        {
            get { return this._結果日付; }
            set { this._結果日付 = value; NotifyPropertyChanged(); }
        }

        private string _整備結果;
        public string 整備結果
        {
            get { return this._整備結果; }
            set { this._整備結果 = value; NotifyPropertyChanged(); }
        }
        #endregion

        /// <summary>
        /// 基礎情報マスタ入力
        /// </summary>
        public SRY22020()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            base.MasterMaintenanceWindowList.Add("M04_DRV", new List<Type> { typeof(MST04010), typeof(SCH04010) });
            base.MasterMaintenanceWindowList.Add("M05_CAR", new List<Type> { typeof(MST06010), typeof(SCH06010) });

            if (初期車輌コード != null)
            {
                車輌ID = AppCommon.IntParse(初期車輌コード.ToString());
                作成年月 = 初期作成年月.ToString();
                チェック日 = AppCommon.IntParse(初期点検日.ToString());
                int i作成年月 = Convert.ToInt32(初期作成年月);
                base.SendRequest(new CommunicationObject(MessageType.RequestData, LOAD_SRY22020, new object[] { 車輌ID, i作成年月 }));
            }
			SetFocusToTopControl();

        }

        #region 受信系処理
        /// <summary>
        /// データ受信メソッド
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceivedResponseData(CommunicationObject message)
        {
            try
            {
                this.ErrorMessage = string.Empty;

                base.SetFreeForInput();
                var data = message.GetResultData();
                DataTable tbl = (data is DataTable) ? (data as DataTable) : null;
                switch (message.GetMessageName())
                {
                    case LOAD_SRY22020:
                        if (tbl.Rows.Count > 0)
                        {
                            //新規ステータス表示
                            this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
                            SetDate(tbl);
                        }
                        else
                        {
                            //新規ステータス表示
                            this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                        }
                        break;

                    case INSERT_SRY22020:
                        MessageBox.Show("登録完了しました。");
                        this.MaintenanceMode = string.Empty;
                        ScreenClear();

                        if (初期車輌コード != null)
                        {
                            車輌ID = AppCommon.IntParse(初期車輌コード.ToString());
                            作成年月 = 初期作成年月.ToString();
                            チェック日 = AppCommon.IntParse(初期点検日.ToString());
                            int i作成年月 = Convert.ToInt32(初期作成年月);
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, LOAD_SRY22020, new object[] { 車輌ID, i作成年月 }));
                        }

                        break;

                    case DELETE_SRY22020:
                        MessageBox.Show("削除完了しました。");
                            this.MaintenanceMode = string.Empty;
                        ScreenClear();
                        if (初期車輌コード != null)
                        {
                            車輌ID = AppCommon.IntParse(初期車輌コード.ToString());
                            作成年月 = 初期作成年月.ToString();
                            チェック日 = AppCommon.IntParse(初期点検日.ToString());
                            int i作成年月 = Convert.ToInt32(初期作成年月);
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, LOAD_SRY22020, new object[] { 車輌ID, i作成年月 }));
                        }
                        break;
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

        public void SetDate(DataTable tbl)
        {

            DateTime CheckDate1 , CheckDate2 , CheckDate3;
            int init;
            if (int.TryParse(tbl.Rows[0]["乗務員ID"].ToString(), out init))
            {
                乗務員ID = AppCommon.IntParse(tbl.Rows[0]["乗務員ID"].ToString());
            }
            乗務員名 = tbl.Rows[0]["乗務員名"].ToString();
            エアコン備考 = tbl.Rows[0]["エアコン備考"].ToString();
            異音備考 = tbl.Rows[0]["異音備考"].ToString();
            排気備考 = tbl.Rows[0]["排気備考"].ToString();
            燃費備考 = tbl.Rows[0]["燃費備考"].ToString();
            その他備考 = tbl.Rows[0]["その他備考"].ToString();

            if (tbl.Rows[0]["エアコン区分"].ToString() == "1")
            {
                this.Chk1.IsChecked = true;
            }

            if (tbl.Rows[0]["異音区分"].ToString() == "1")
            {
                this.Chk2.IsChecked = true;
            }

            if (tbl.Rows[0]["排気区分"].ToString() == "1")
            {
                this.Chk3.IsChecked = true;
            }

            if (tbl.Rows[0]["燃費区分"].ToString() == "1")
            {
                this.Chk4.IsChecked = true;
            }

            if (tbl.Rows[0]["その他区分"].ToString() == "1")
            {
                this.Chk5.IsChecked = true;
            }

            if (DateTime.TryParse(tbl.Rows[0]["指示日付"].ToString(), out CheckDate1))
            {
                指示日付 = CheckDate1;
            }

            整備指示 = tbl.Rows[0]["整備指示"].ToString();

            if (DateTime.TryParse(tbl.Rows[0]["部品日付"].ToString(), out CheckDate2))
            {
                指示日付 = CheckDate2;
            }

            整備部品 = tbl.Rows[0]["整備部品"].ToString();

            if (DateTime.TryParse(tbl.Rows[0]["結果日付"].ToString(), out CheckDate3))
            {
                指示日付 = CheckDate3;
            }

            整備結果 = tbl.Rows[0]["整備結果"].ToString();
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
        /// F9 データ登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            int p作成年月 = AppCommon.IntParse(作成年月);
            エアコン区分 = 0;
            if (this.Chk1.IsChecked == true)
            {
                エアコン区分 = 1;
            }
            異音区分 = 0;
            if (this.Chk2.IsChecked == true)
            {
                異音区分 = 1;
            }
            排気区分 = 0;
            if (this.Chk3.IsChecked == true)
            {
                排気区分 = 1;
            }
            燃費区分 = 0;
            if (this.Chk4.IsChecked == true)
            {
                燃費区分 = 1;
            }
            その他区分 = 0;
            if (this.Chk5.IsChecked == true)
            {
                その他区分 = 1;
            }

            if (チェック日 == null)
            {
                this.ErrorMessage = "点検日は入力必須項目です。";
                MessageBox.Show("点検日は入力必須項目です。");
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

                //INSERT_SRY22020
				base.SendRequest(new CommunicationObject(MessageType.RequestDataWithBusy, INSERT_SRY22020, new object[] { 車輌ID, p作成年月 , チェック日 , エアコン区分 , エアコン備考 , 異音区分 , 異音備考 , 排気区分 , 排気備考 , 燃費区分 , 燃費備考,
                                                                                                              その他区分 , その他備考 , 乗務員ID , 乗務員名 , 整備指示 , 指示日付 , 整備部品 , 部品日付 , 整備結果 , 結果日付 }));
            }
        }

        /// <summary>
        /// F10 入力取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            ScreenClear();
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
        /// F12 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF12Key(object sender, KeyEventArgs e)
        {
            if (初期車輌コード != null)
            {
                車輌ID = AppCommon.IntParse(初期車輌コード.ToString());
                作成年月 = 初期作成年月.ToString();
                チェック日 = AppCommon.IntParse(初期点検日.ToString());
                int i作成年月 = Convert.ToInt32(初期作成年月);
                base.SendRequest(new CommunicationObject(MessageType.RequestData, DELETE_SRY22020, new object[] { 車輌ID, i作成年月 }));
            }
        }


        #endregion

        #region クリア

        public void ScreenClear()
        {
            乗務員ID = null;
            乗務員名 = null;
            エアコン備考 = null;
            異音備考 = null;
            排気備考 = null;
            燃費備考 = null;
            その他備考 = null;
            Chk1.IsChecked = false;
            Chk2.IsChecked = false;
            Chk3.IsChecked = false;
            Chk4.IsChecked = false;
            Chk5.IsChecked = false;
            指示日付 = null;
            整備指示 = null;
            部品日付 = null;
            整備部品 = null;
            結果日付 = null;
            整備結果 = null;
        }

        #endregion

    }
}
