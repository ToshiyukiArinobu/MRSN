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
    /// 燃費目標マスタ入力
    /// </summary>
    public partial class MST31010 : WindowMasterMainteBase
	{
		public class SERCHE_MST31010 : INotifyPropertyChanged
		{
			public int _車輌コード { get; set; }
			public string _車番 { get; set; }
			public string _車種 { get; set; }
			public decimal? _目標燃費 { get; set; }
			public string _S_目標燃費 { get; set; }
			public int? _Flg { get; set; }

			public int 車輌コード { get { return _車輌コード; } set { _車輌コード = value; NotifyPropertyChanged(); } }
			public string 車番 { get { return _車番; } set { _車番 = value; NotifyPropertyChanged(); } }
			public string 車種 { get { return _車種; } set { _車種 = value; NotifyPropertyChanged(); } }
			public decimal? 目標燃費 { get { return _目標燃費; } set { _目標燃費 = value; NotifyPropertyChanged(); } }
			public string S_目標燃費 { get { return _S_目標燃費; } set { _S_目標燃費 = value; NotifyPropertyChanged(); } }
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
        private const string LOAD_MST31010 = "LOAD_MST31010";
        //*** 検索 ***//
        private const string SEARCH_MST31010 = "SEARCH_MST31010";
        //*** セル登録 ***//
        private const string INSERT_MST31010 = "INSERT_MST31010";
        //***　一括登録 ***//
        private const string NINSERT_MST31010 = "NINSERT_MST31010";
        //*** 削除 ***//
        private const string DELETE_MST31010 = "DELETE_MST31010";
        //*** 先月 ***///
        private const string LAST_MANTH_MST31010 = "LAST_MANTH_MST31010";
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
        public class ConfigMST31010 : FormConfigBase
        {
            public byte[] spConfig = null;
        }
        /// ※ 必ず public で定義する。
        public ConfigMST31010 frmcfg = null;
        public byte[] spConfig = null;

        #endregion

        #region バインド用変数

        private string _作成年月 = string.Empty;
        public string 作成年月
        {
            get { return this._作成年月; }
            set { this._作成年月 = value; NotifyPropertyChanged(); }
        }


		private List<SERCHE_MST31010> _燃費目標データ = null;
		public List<SERCHE_MST31010> 燃費目標データ
		{
			get
			{
				return this._燃費目標データ;
			}
			set
			{
				this._燃費目標データ = value;
				this.sp燃費目標データ.ItemsSource = value;
				NotifyPropertyChanged();
			}
		}


		//private DataTable _燃費目標データ一覧 = new DataTable();
		//public DataTable 燃費目標データ一覧
		//{
		//	get { return this._燃費目標データ一覧; }
		//	set
		//	{
		//		this._燃費目標データ一覧 = value;
		//		NotifyPropertyChanged();
		//	}
		//}

        #endregion

        /// <summary>
        /// 燃費目標マスタ入力
        /// </summary>
        public MST31010()
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
            frmcfg = (ConfigMST31010)ucfg.GetConfigValue(typeof(ConfigMST31010));
            if (frmcfg == null)
            {
                frmcfg = new ConfigMST31010();
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

            base.SendRequest(new CommunicationObject(MessageType.RequestData, LOAD_MST31010, new object[] {  }));

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
                    case LOAD_MST31010:
                        if (tbl.Rows.Count > 0)
						{
							燃費目標データ = null;
							燃費目標データ = (List<SERCHE_MST31010>)AppCommon.ConvertFromDataTable(typeof(List<SERCHE_MST31010>), tbl);
                            //燃費目標データ一覧 = tbl;
                            //this.sp燃費目標データ.ItemsSource = this.燃費目標データ一覧.DefaultView;
                            Cnt = 0;
                        }
                        else
                        {
                            this.ErrorMessage = "データがありません。";
                            return;
                        }
                        break;

                    case SEARCH_MST31010:
                        if (tbl.Rows.Count > 0)
                            {
								燃費目標データ = (List<SERCHE_MST31010>)AppCommon.ConvertFromDataTable(typeof(List<SERCHE_MST31010>), tbl);
								
								//燃費目標データ一覧 = tbl;
								//this.sp燃費目標データ.ItemsSource = this.燃費目標データ一覧.DefaultView;
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

                            }
                            else
                            {
                                this.ErrorMessage = "データがありません。";
                                return;
                            }
                            break;

                    case LAST_MANTH_MST31010:
                            if (tbl.Rows.Count > 0)
                            {
								燃費目標データ = (List<SERCHE_MST31010>)AppCommon.ConvertFromDataTable(typeof(List<SERCHE_MST31010>), tbl);
								//燃費目標データ一覧 = tbl;
								//this.sp燃費目標データ.ItemsSource = this.燃費目標データ一覧.DefaultView;
                                Cnt = 1;
                            }
                            else
                            {
                                this.ErrorMessage = "データがありません。";
                                return;
                            }
                        break;

                    case NINSERT_MST31010:
                        MessageBox.Show("更新が完了しました。");
                        ChangeKeyItemChangeable(true);
                        SetFocusToTopControl();
                        this.MaintenanceMode = string.Empty;
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, LOAD_MST31010, new object[] { }));
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
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, SEARCH_MST31010, new object[] { 作成年月 }));
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

            base.SendRequest(new CommunicationObject(MessageType.RequestData, LAST_MANTH_MST31010, new object[] { 作成年月 }));
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

            if (string.IsNullOrEmpty(作成年月))
            {
                this.ErrorMessage = "作成年月は必須入力項目です。";
                MessageBox.Show("作成年月は必須入力項目です。");
                return;
            }


            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("車輌コード", typeof(Int32));
            dt.Columns.Add("車番", typeof(String));
            dt.Columns.Add("車種", typeof(String));
            dt.Columns.Add("燃費目標", typeof(Decimal));
            dt.Columns.Add("S_燃費目標", typeof(string));

            //Columns

            for (int i = 0; i < sp燃費目標データ.RowCount; i++)
            {

                DataRow dAdd = dt.NewRow();
                dAdd["車輌コード"] = Convert.ToInt32(sp燃費目標データ[i, 0].Value);
                dAdd["車番"] = sp燃費目標データ[i, 1].Value.ToString();
				dAdd["車種"] = sp燃費目標データ[i, 2].Value.ToString();
				dAdd["燃費目標"] = Convert.ToDecimal(sp燃費目標データ[i, 3].Value);
				dAdd["S_燃費目標"] = sp燃費目標データ[i, 3].Value.ToString();
                dt.Rows.Add(dAdd);

            }

            var yesno = MessageBox.Show("入力内容を登録しますか？", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (yesno == MessageBoxResult.Yes)
            {
                ds.Tables.Add(dt);
                dt.TableName = "燃費目標";
                base.SendRequest(new CommunicationObject(MessageType.UpdateData, NINSERT_MST31010, new object[] { ds, 作成年月 }));
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
            base.SendRequest(new CommunicationObject(MessageType.RequestData, LOAD_MST31010, new object[] { }));
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

        #region セル内容登録

        /// <summary>
        /// セル登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 燃費目標_CellEditEnded(object sender, GrapeCity.Windows.SpreadGrid.SpreadCellEditEndedEventArgs e)
        {
            if (Cnt == 0)
            {
                this.ErrorMessage = "作成年月を入力しデータを呼び起こしてください。";
                MessageBox.Show("作成年月を入力しデータを呼び起こしてください。");
                SetFocusToTopControl();
                base.SendRequest(new CommunicationObject(MessageType.RequestData, LOAD_MST31010, new object[] { }));
                return;
            }

        }

        #endregion

		private void WindowMasterMainteBase_Closed(object sender, EventArgs e)
		{
			燃費目標データ = null;

            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }

    }
}
