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
    /// 部門別売上予算マスタ入力
    /// </summary>
    public partial class MST33010 : WindowMasterMainteBase
    {

		public class SERCHE_MST33010 : INotifyPropertyChanged
		{
			public int _部門ID { get; set; }
			public string _部門名 { get; set; }
			public decimal _月1 { get; set; }
			public decimal _月2 { get; set; }
			public decimal _月3 { get; set; }
			public decimal _月4 { get; set; }
			public decimal _月5 { get; set; }
			public decimal _月6 { get; set; }
			public decimal _月7 { get; set; }
			public decimal _月8 { get; set; }
			public decimal _月9 { get; set; }
			public decimal _月10 { get; set; }
			public decimal _月11 { get; set; }
			public decimal _月12 { get; set; }
			public int _年月1 { get; set; }
			public int _年月2 { get; set; }
			public int _年月3 { get; set; }
			public int _年月4 { get; set; }
			public int _年月5 { get; set; }
			public int _年月6 { get; set; }
			public int _年月7 { get; set; }
			public int _年月8 { get; set; }
			public int _年月9 { get; set; }
			public int _年月10 { get; set; }
			public int _年月11 { get; set; }
			public int _年月12 { get; set; }
			public int? _Flg { get; set; }


			public int 部門ID { get { return _部門ID; } set { _部門ID = value; NotifyPropertyChanged(); } }
			public string 部門名 { get { return _部門名; } set { _部門名 = value; NotifyPropertyChanged(); } }
			public decimal 月1 { get { return _月1; } set { _月1 = value; NotifyPropertyChanged(); } }
			public decimal 月2 { get { return _月2; } set { _月2 = value; NotifyPropertyChanged(); } }
			public decimal 月3 { get { return _月3; } set { _月3 = value; NotifyPropertyChanged(); } }
			public decimal 月4 { get { return _月4; } set { _月4 = value; NotifyPropertyChanged(); } }
			public decimal 月5 { get { return _月5; } set { _月5 = value; NotifyPropertyChanged(); } }
			public decimal 月6 { get { return _月6; } set { _月6 = value; NotifyPropertyChanged(); } }
			public decimal 月7 { get { return _月7; } set { _月7 = value; NotifyPropertyChanged(); } }
			public decimal 月8 { get { return _月8; } set { _月8 = value; NotifyPropertyChanged(); } }
			public decimal 月9 { get { return _月9; } set { _月9 = value; NotifyPropertyChanged(); } }
			public decimal 月10 { get { return _月10; } set { _月10 = value; NotifyPropertyChanged(); } }
			public decimal 月11 { get { return _月11; } set { _月11 = value; NotifyPropertyChanged(); } }
			public decimal 月12 { get { return _月12; } set { _月12 = value; NotifyPropertyChanged(); } }
			public int 年月1 { get { return _年月1; } set { _年月1 = value; NotifyPropertyChanged(); } }
			public int 年月2 { get { return _年月2; } set { _年月2 = value; NotifyPropertyChanged(); } }
			public int 年月3 { get { return _年月3; } set { _年月3 = value; NotifyPropertyChanged(); } }
			public int 年月4 { get { return _年月4; } set { _年月4 = value; NotifyPropertyChanged(); } }
			public int 年月5 { get { return _年月5; } set { _年月5 = value; NotifyPropertyChanged(); } }
			public int 年月6 { get { return _年月6; } set { _年月6 = value; NotifyPropertyChanged(); } }
			public int 年月7 { get { return _年月7; } set { _年月7 = value; NotifyPropertyChanged(); } }
			public int 年月8 { get { return _年月8; } set { _年月8 = value; NotifyPropertyChanged(); } }
			public int 年月9 { get { return _年月9; } set { _年月9 = value; NotifyPropertyChanged(); } }
			public int 年月10 { get { return _年月10; } set { _年月10 = value; NotifyPropertyChanged(); } }
			public int 年月11 { get { return _年月11; } set { _年月11 = value; NotifyPropertyChanged(); } }
			public int 年月12 { get { return _年月12; } set { _年月12 = value; NotifyPropertyChanged(); } }
			public int? Flg { get { return _Flg; } set { _Flg = value; NotifyPropertyChanged(); } }

			#region INotifyPropertyChanged メンバー
			/// <summary>
			/// Binding機能対応（プロパティの変更通知イベント）
			/// </summary>
			public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
			/// <summary>
			/// Binding機能対応（プロパティの変更通知イベント送信）
			/// </summary>
			/// <param name="propertyName">Bindingプロパティ名</param>
			public void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
			{
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
				}
			}
			#endregion

		}

        #region 定数定義

        //*** 表示 ***//
        private const string LOAD_MST33010 = "LOAD_MST33010";
        //*** 検索 ***//
        private const string SEARCH_MST33010 = "SEARCH_MST33010";
        //*** セル登録 ***//
        private const string INSERT_MST33010 = "INSERT_MST33010";
        //***　一括登録 ***//
        private const string NINSERT_MST33010 = "NINSERT_MST33010";
        //*** 削除 ***//
        private const string DELETE_MST33010 = "DELETE_MST33010";
        //*** 先月 ***///
        private const string LAST_MANTH_MST33010 = "LAST_MANTH_MST33010";
        //*** グローバル変数 ***//
        static public int Cnt = 0;
       
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
        public class ConfigMST33010 : FormConfigBase
        {
            public byte[] spConfig = null;
        }
        /// ※ 必ず public で定義する。
        public ConfigMST33010 frmcfg = null;
        public byte[] spConfig = null;

        #endregion

        #region バインド用変数



		private List<SERCHE_MST33010> _部門別予算データ = null;
		public List<SERCHE_MST33010> 部門別予算データ
		{
			get
			{
				return this._部門別予算データ;
			}
			set
			{
				this._部門別予算データ = value;
				this.sp燃費目標データ.ItemsSource = value;
				NotifyPropertyChanged();
			}
		}


        private string _作成年月 = string.Empty;
        public string 作成年月
        {
            get { return this._作成年月; }
            set { this._作成年月 = value; NotifyPropertyChanged(); }
        }


		private DataTable _部門別予算TBL = new DataTable();
		public DataTable 部門別予算TBL
		{
			get { return this._部門別予算TBL; }
			set { this._部門別予算TBL = value; NotifyPropertyChanged(); }
		}

        #endregion

        /// <summary>
        /// 部門別売上予算マスタ入力
        /// </summary>
        public MST33010()
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

            //base.SendRequest(new CommunicationObject(MessageType.RequestData, LOAD_MST33010, new object[] {  }));

            #region 設定項目取得
            ucfg = AppCommon.GetConfig(this);
            // 権限設定を呼び出す(ucfgを取得した後のに入れる)
            ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
            // 登録ボタン設定
            if (!権限Get.Authority_Update_Button(ccfg, this.GetType().Name))
            {
                DataUpdateVisible = System.Windows.Visibility.Hidden;
            }
            frmcfg = (ConfigMST33010)ucfg.GetConfigValue(typeof(ConfigMST33010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST33010();
                ucfg.SetConfigValue(frmcfg);
                frmcfg.spConfig = this.spConfig;
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

			SetFocusToTopControl();
			ResetAllValidation();

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
                    case LOAD_MST33010:
                        if (tbl.Rows.Count > 0)
						{
							部門別予算データ = (List<SERCHE_MST33010>)AppCommon.ConvertFromDataTable(typeof(List<SERCHE_MST33010>), tbl);

							//部門別予算データ = tbl;
							//this.sp燃費目標データ.ItemsSource = this.部門別予算データ.DefaultView;
                            Cnt = 0;
                        }
                        else
                        {
                            this.ErrorMessage = "データがありません。";
                            return;
                        }
                        break;

                    case SEARCH_MST33010:
                        if (tbl.Rows.Count > 0)
						{
							部門別予算データ = null;
								部門別予算データ = (List<SERCHE_MST33010>)AppCommon.ConvertFromDataTable(typeof(List<SERCHE_MST33010>), tbl);
								//部門別予算データ = tbl;
                                //this.sp燃費目標データ.ItemsSource = this.部門別予算データ.DefaultView;
                                Cnt = 1;
                                ChangeKeyItemChangeable(false);
                                //リボンの状態表示
                                if(tbl.Rows[0]["Flg"].ToString() == "0")
                                {
                                    //新規ステータス表示
                                    this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
                                }
                                else
                                {
                                    //編集ステータス表示
                                    this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
                                }

                                DateTime d年月 = Convert.ToDateTime(作成年月.Substring(0, 4).ToString() + "/" + 作成年月.Substring(5, 2).ToString() + "/" + "01");
                                sp燃費目標データ.ColumnHeader[0, 2].Value = (d年月.Month).ToString() + "月";
                                sp燃費目標データ.ColumnHeader[0, 3].Value = (d年月.AddMonths(1).Month).ToString() + "月";
                                sp燃費目標データ.ColumnHeader[0, 4].Value = d年月.AddMonths(2).Month.ToString() + "月";
                                sp燃費目標データ.ColumnHeader[0, 5].Value = d年月.AddMonths(3).Month.ToString() + "月";
                                sp燃費目標データ.ColumnHeader[0, 6].Value = d年月.AddMonths(4).Month.ToString() + "月";
                                sp燃費目標データ.ColumnHeader[0, 7].Value = d年月.AddMonths(5).Month.ToString() + "月";
                                sp燃費目標データ.ColumnHeader[0, 8].Value = d年月.AddMonths(6).Month.ToString() + "月";
                                sp燃費目標データ.ColumnHeader[0, 9].Value = d年月.AddMonths(7).Month.ToString() + "月";
                                sp燃費目標データ.ColumnHeader[0, 10].Value = d年月.AddMonths(8).Month.ToString() + "月";
                                sp燃費目標データ.ColumnHeader[0, 11].Value = d年月.AddMonths(9).Month.ToString() + "月";
                                sp燃費目標データ.ColumnHeader[0, 12].Value = d年月.AddMonths(10).Month.ToString() + "月";
                                sp燃費目標データ.ColumnHeader[0, 13].Value = d年月.AddMonths(11).Month.ToString() + "月";
                            }
                            else
                            {
                                this.ErrorMessage = "データがありません。";
                                return;
                            }
                            break;

                    case LAST_MANTH_MST33010:
                            if (tbl.Rows.Count > 0)
							{
								部門別予算データ = (List<SERCHE_MST33010>)AppCommon.ConvertFromDataTable(typeof(List<SERCHE_MST33010>), tbl);
								//部門別予算データ = tbl;
                                //this.sp燃費目標データ.ItemsSource = this.部門別予算データ.DefaultView;
                                Cnt = 1;
                            }
                            else
                            {
                                this.ErrorMessage = "データがありません。";
                                return;
                            }
                        break;

                    case NINSERT_MST33010:
                        MessageBox.Show("更新が完了しました。");
                        ChangeKeyItemChangeable(true);
                        SetFocusToTopControl();
                        this.MaintenanceMode = string.Empty;
                        部門別予算データ = null;
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

        #region 日付取得

        /// <summary>
        /// 作成年月でEnterキー押下
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
                        NNGDays ret = AppCommon.GetNNGDays(作成年月);
                        if (ret.Result == true)
                        {
                            作成年月 = ret.DateFrom;
                            //最後尾検索
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, SEARCH_MST33010, new object[] { 作成年月 }));
                        }
                        else if (ret.Result == false)
                        {
                            this.ErrorMessage = "入力された作成年月は利用できません。　入力例：201406";
                            this.作成年月 = string.Empty;
                            return;
                        }


                       
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

        #region 先月データ取得

        /// <summary>
        /// 先月データ取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastMonth(object sender, RoutedEventArgs e)
        {
            if (Cnt == 0)
            {
                this.ErrorMessage = "作成年月を入力しデータを呼び起こしてください。";
                MessageBox.Show("作成年月を入力しデータを呼び起こしてください。");
                return;
            }

            base.SendRequest(new CommunicationObject(MessageType.RequestData, LAST_MANTH_MST33010, new object[] { 作成年月 }));
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
        /// F9 データ登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            //if (this.MaintenanceMode != AppConst.MAINTENANCEMODE_ADD && this.MaintenanceMode != AppConst.MAINTENANCEMODE_EDIT)
            //{
            //    this.ErrorMessage = "登録データがありません";
            //    return;
            //}

            if (部門別予算データ == null)
            {
                this.ErrorMessage = "作成年月は必須入力項目です。";
                MessageBox.Show("作成年月は必須入力項目です。");
                return;

            }

            if (string.IsNullOrEmpty(作成年月))
            {
                this.ErrorMessage = "作成年月は必須入力項目です。";
                MessageBox.Show("作成年月は必須入力項目です。");
                return;
            }

			部門別予算TBL = new DataTable();
			//リストをデータテーブルへ
			AppCommon.ConvertToDataTable(部門別予算データ, 部門別予算TBL);
            
            var yesno = MessageBox.Show("入力内容を登録しますか？", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (yesno == MessageBoxResult.Yes)
            {
                base.SendRequest(new CommunicationObject(MessageType.UpdateData, NINSERT_MST33010, new object[] { 部門別予算TBL, 作成年月 }));
            }
            else
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
            Cnt = 0;
            作成年月 = string.Empty;
            ChangeKeyItemChangeable(true);
            SetFocusToTopControl();
            this.MaintenanceMode = string.Empty;
            部門別予算データ = null;
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

        #endregion

		private void WindowMasterMainteBase_Closed(object sender, EventArgs e)
		{
			部門別予算データ = null;

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }
    }
}
