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
using System.IO;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.Data;

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;
using GrapeCity.Windows.SpreadGrid;
using KyoeiSystem.Framework.Windows.Controls;
using System.ComponentModel;


namespace KyoeiSystem.Application.Windows.Views
{
    /// <summary>
    /// 車輌マスタ入力
    /// </summary>
    public partial class MST06010 : WindowMasterMainteBase
    {

        public class M05_CDT4_Member : INotifyPropertyChanged
        {
            public int _車輌ID { get; set; }
            public int _車輌KEY { get; set; }
            public int _年度 { get; set; }
            public int? _自動車税年月 { get; set; }
            public int? _自動車税 { get; set; }
            public decimal? _d自動車税 { get; set; }
            public int? _重量税年月 { get; set; }
            public int? _重量税 { get; set; }
            public decimal? _d重量税 { get; set; }
            public string _S_年度 { get; set; }
            public string _S_自動車税年月 { get; set; }
            public string _S_自動車税 { get; set; }
            public string _S_重量税年月 { get; set; }
            public string _S_重量税 { get; set; }


            public int 車輌ID { get { return _車輌ID; } set { _車輌ID = value; NotifyPropertyChanged(); } }
            public int 車輌KEY { get { return _車輌KEY; } set { _車輌KEY = value; NotifyPropertyChanged(); } }
            public int 年度 { get { return _年度; } set { _年度 = value; NotifyPropertyChanged(); } }
            public int? 自動車税年月 { get { return _自動車税年月; } set { _自動車税年月 = value; NotifyPropertyChanged(); } }
            public int? 自動車税 { get { return _自動車税; } set { _自動車税 = value; NotifyPropertyChanged(); } }
            public decimal? d自動車税 { get { return _d自動車税; } set { _d自動車税 = value; NotifyPropertyChanged(); } }
            public int? 重量税年月 { get { return _重量税年月; } set { _重量税年月 = value; NotifyPropertyChanged(); } }
            public int? 重量税 { get { return _重量税; } set { _重量税 = value; NotifyPropertyChanged(); } }
            public decimal? d重量税 { get { return _d重量税; } set { _d重量税 = value; NotifyPropertyChanged(); } }
            public string S_年度 { get { return _S_年度; } set { _S_年度 = value; NotifyPropertyChanged(); } }
            public string S_自動車税年月 { get { return _S_自動車税年月; } set { _S_自動車税年月 = value; NotifyPropertyChanged(); } }
            public string S_自動車税 { get { return _S_自動車税; } set { _S_自動車税 = value; NotifyPropertyChanged(); } }
            public string S_重量税年月 { get { return _S_重量税年月; } set { _S_重量税年月 = value; NotifyPropertyChanged(); } }
            public string S_重量税 { get { return _S_重量税; } set { _S_重量税 = value; NotifyPropertyChanged(); } }


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
        public class ConfigMST06010 : FormConfigBase
        {
            public byte[] spKyouseiConfig = null;
            public byte[] spNiniConfig = null;
            public byte[] spNouzeiConfig = null;
        }
        /// ※ 必ず public で定義する。
        public ConfigMST06010 frmcfg = null;

        #endregion

        private byte[] spKyouseiConfig = null;
        private byte[] spNiniConfig = null;
        private byte[] spNouzeiConfig = null;


        //
        private const string CAR_ID_CHG = "CAR_ID_CHG";
		//対象テーブル検索用
		private const string TargetTableNm = "M05_CAR_UC";
		//対象テーブル検索用
		private const string RTargetTableNm = "RM05_CAR_UC";
		//対象テーブルボタン検索用
        private const string TargetTableNmBtn = "M05_CAR_BTN";
        //対象テーブル更新用
        private const string TargetTableNmUpdate = "M05_CAR_UPD";
        //対象テーブル削除用
        private const string TargetTableNmDelete = "M05_CAR_DEL";
        //自動採番用
        private const string GetNextID = "M05_CAR_NEXT";
        //運輸局情報取得
        private const string UnyukyokuTablNm = "M84_RIK_UC";
        //車輌台帳PREV印刷
        private const string CarList = "CarList";
        //車輌台帳印刷メソッド用
        private const string rptFullPathName_PIC = @"Files\MST\MST06011.rpt";
        /// <summary>
        /// 適正診断実施
        /// </summary>
        private const string 強制保険データ取得 = "M05_CDT2";
        /// <summary>
        /// 事故・違反履歴
        /// </summary>
        private const string 任意保険データ取得 = "M05_CDT3";
        /// <summary>
        /// 特別教育実施状況
        /// </summary>
        private const string 納税データ取得 = "M05_CDT4";
        //登録件数
        private const string GetMaxMeisaiNo = "M05_GetMaxMeisaiNo";

        // ScrollView内のテキストボックスの初期位置保存用
        class ScrollViewPos
        {
            public UIElement fld;
            public double Y;
        }
        List<ScrollViewPos> scrollYlist = new List<ScrollViewPos>();

        //表示用マスタデータ
        private DataRow _MstData;
        public DataRow MstData
        {
            get { return this._MstData; }
            set
            {
                this._MstData = value;
                NotifyPropertyChanged();
            }
        }

        #region バインド用プロパティ

        private int? _登録件数 = null;
        public int? 登録件数
        {
            get { return this._登録件数; }
            set { this._登録件数 = value; NotifyPropertyChanged(); }
        }

        private string _車輌ID = string.Empty;
        public string 車輌ID
        {
            get { return this._車輌ID; }
            set { this._車輌ID = value; NotifyPropertyChanged(); }
        }

        private string _担当部門名 = string.Empty;
        public string 担当部門名
        {
            get { return this._担当部門名; }
            set { this._担当部門名 = value; NotifyPropertyChanged(); }
        }

        private string _車種名 = string.Empty;
        public string 車種名
        {
            get { return this._車種名; }
            set { this._車種名 = value; NotifyPropertyChanged(); }
        }


        private string _主乗務員名 = string.Empty;
        public string 主乗務員名
        {
            get { return this._主乗務員名; }
            set { this._主乗務員名 = value; NotifyPropertyChanged(); }
        }

        private string _車輌KEY = string.Empty;
        public string 車輌KEY
        {
            get { return this._車輌KEY; }
            set { this._車輌KEY = value; NotifyPropertyChanged(); }
        }

        private DataView _所属運輸局View = null;
        public DataView 所属運輸局View
        {
            get { return this._所属運輸局View; }
            set { this._所属運輸局View = value; NotifyPropertyChanged(); }
        }

        private string _所有者ID = string.Empty;
        public string 所有者ID
        {
            get { return this._所有者ID; }
            set { this._所有者ID = value; NotifyPropertyChanged(); }
        }
        private string _所有者名 = string.Empty;
        public string 所有者名
        {
            get { return this._所有者名; }
            set { this._所有者名 = value; NotifyPropertyChanged(); }
        }
        private string _所有者住所 = string.Empty;
        public string 所有者住所
        {
            get { return this._所有者住所; }
            set { this._所有者住所 = value; NotifyPropertyChanged(); }
        }

        private string _使用者ID = string.Empty;
        public string 使用者ID
        {
            get { return this._使用者ID; }
            set { this._使用者ID = value; NotifyPropertyChanged(); }
        }
        private string _使用者名 = string.Empty;
        public string 使用者名
        {
            get { return this._使用者名; }
            set { this._使用者名 = value; NotifyPropertyChanged(); }
        }
        private string _使用者住所 = string.Empty;
        public string 使用者住所
        {
            get { return this._使用者住所; }
            set { this._使用者住所 = value; NotifyPropertyChanged(); }
        }

        private string _Ｇ経営用車種名 = string.Empty;
        public string Ｇ経営用車種名
        {
            get { return this._Ｇ経営用車種名; }
            set { this._Ｇ経営用車種名 = value; NotifyPropertyChanged(); }
        }

        private string _規制区分名 = string.Empty;
        public string 規制区分名
        {
            get { return this._規制区分名; }
            set { this._規制区分名 = value; NotifyPropertyChanged(); }
        }

        //バインド用プロパティ後追加分

        private string _車輌番号 = string.Empty;
        public string 車輌番号
        {
            get { return this._車輌番号; }
            set { this._車輌番号 = value; NotifyPropertyChanged(); }
        }

        private string _車輌登録番号 = string.Empty;
        public string 車輌登録番号
        {
            get { return this._車輌登録番号; }
            set { this._車輌登録番号 = value; NotifyPropertyChanged(); }
        }
        private DataTable _強制保険データ;
        public DataTable 強制保険データ
        {
            get { return this._強制保険データ; }
            set { this._強制保険データ = value; NotifyPropertyChanged(); }
        }
        private DataTable _任意保険データ;
        public DataTable 任意保険データ
        {
            get { return this._任意保険データ; }
            set { this._任意保険データ = value; NotifyPropertyChanged(); }
        }
        private List<M05_CDT4_Member> _納税データ;
        public List<M05_CDT4_Member> 納税データ
        {
            get { return this._納税データ; }
            set
            {
                this._納税データ = value;
                this.sp納税.ItemsSource = value;
                NotifyPropertyChanged();
            }
        }

        private DataTable _納税dt = null;
        public DataTable 納税dt
        {
            get { return this._納税dt; }
            set { this._納税dt = value; NotifyPropertyChanged(); }
        }


        private int? _new_ID;
        public int? new_ID
        {
            get { return this._new_ID; }
            set { this._new_ID = value; NotifyPropertyChanged(); }
        }

		private int? _類似ID = null;
		public int? 類似ID
		{
			get { return this._類似ID; }
			set { this._類似ID = value; NotifyPropertyChanged(); }
		}
        #endregion

        /// <summary>
        /// 車輌マスタ入力
        /// </summary>
        public MST06010()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var child in ViewBaseCommon.FindLogicalChildList<UcTextBox>(this.General_ScrollViewer))
                {
                    scrollYlist.Add(new ScrollViewPos() { fld = child, Y = child.TranslatePoint(new Point(0, 0), this.General_ScrollViewer).Y });
                }

                //this.spKyouseiConfig = AppCommon.SaveSpConfig(this.sp強制保険);
                //this.spNiniConfig = AppCommon.SaveSpConfig(this.sp任意保険);
                //this.spNouzeiConfig = AppCommon.SaveSpConfig(this.sp納税);

                //AppCommon.LoadSpConfig(this.sp強制保険, this.spKyouseiConfig);
                //AppCommon.LoadSpConfig(this.sp任意保険, this.spNiniConfig);
                //AppCommon.LoadSpConfig(this.sp納税, this.spNouzeiConfig);

                #region 設定項目取得
                ucfg = AppCommon.GetConfig(this);
                // 権限設定を呼び出す(ucfgを取得した後のに入れる)
                ccfg = (CommonConfig)ucfg.GetConfigValue(typeof(CommonConfig));
                // 登録ボタン設定
                if (!権限Get.Authority_Update_Button(ccfg, this.GetType().Name))
                {
                    DataUpdateVisible = System.Windows.Visibility.Hidden;
                }
                frmcfg = (ConfigMST06010)ucfg.GetConfigValue(typeof(ConfigMST06010));
                if (frmcfg == null)
                {
                    frmcfg = new ConfigMST06010();
                    ucfg.SetConfigValue(frmcfg);
                    frmcfg.spKyouseiConfig = this.spKyouseiConfig;
                    frmcfg.spNiniConfig = this.spNiniConfig;
                    frmcfg.spNouzeiConfig = this.spNouzeiConfig;
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

				sp強制保険.InputBindings.Add(new KeyBinding(sp強制保険.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));
				sp任意保険.InputBindings.Add(new KeyBinding(sp任意保険.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));
				sp納税.InputBindings.Add(new KeyBinding(sp納税.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));

                #endregion
                base.SendRequest(new CommunicationObject(MessageType.RequestData, UnyukyokuTablNm, new object[] { null }));
                base.MasterMaintenanceWindowList.Add("M01_TOK", new List<Type> { typeof(MST01010), typeof(SCH01010) });
                base.MasterMaintenanceWindowList.Add("M71_BUM", new List<Type> { typeof(MST10010), typeof(SCH10010) });
                base.MasterMaintenanceWindowList.Add("M06_SYA", new List<Type> { typeof(MST05010), typeof(SCH05010) });
                base.MasterMaintenanceWindowList.Add("M04_DRV", new List<Type> { typeof(MST04010), typeof(SCH04010) });
                base.MasterMaintenanceWindowList.Add("M05_CAR", new List<Type> { typeof(MST06010), typeof(SCH06010) });
                base.MasterMaintenanceWindowList.Add("M14_GSYA", new List<Type> { typeof(MST28010), typeof(SCH28010) });
                base.MasterMaintenanceWindowList.Add("M12_KIS", new List<Type> { typeof(MST29010), typeof(SCH29010) });

				SetFocusToTopControl();
            }
            catch (Exception ex)
            {
                return;
            }

        }

		/// <summary>
		/// 取得データを変数に代入
		/// </summary>
		/// <param name="tbl"></param>
		private void strData(DataTable tbl)
		{
			if (tbl.Rows.Count == 0)
			{
				MstData = tbl.NewRow();
				this.MaintenanceMode = AppConst.MAINTENANCEMODE_ADD;
				txt類似ID.Visibility = Visibility.Visible;
				ID変換.Visibility = Visibility.Collapsed;
			}
			else
			{
				if (!string.IsNullOrEmpty(tbl.Rows[0]["削除日付"].ToString()))
				{
					ScreenClear();
					this.ErrorMessage = "既に削除されたデータです。";
					return;
				}

				MstData = tbl.Rows[0];


				車輌KEY = MstData["車輌KEY"].ToString();
				車輌ID = MstData["車輌ID"].ToString();
				所有者名 = MstData["所有者名"].ToString();
				所有者住所 = MstData["所有者住所"].ToString();
				使用者名 = MstData["使用者名"].ToString();
				使用者住所 = MstData["使用者住所"].ToString();

				//バインド用のプロパティ後追加分
				車輌番号 = MstData["車輌番号"].ToString();
				車輌登録番号 = MstData["車輌登録番号"].ToString();

				//運輸局コンボボックス設定
				foreach (DataRow dr in 所属運輸局View.Table.Rows)
				{
					if (MstData["運輸局ID"].ToString() == dr["運輸局ID"].ToString())
					{
						ComboUnyukyoku.Combo_SelectedValue = dr["運輸局ID"];
						break;
					}

				}
				this.MaintenanceMode = AppConst.MAINTENANCEMODE_EDIT;
				txt類似ID.Visibility = Visibility.Collapsed;
				ID変換.Visibility = Visibility.Visible;


			}
			//主キー項目
			ChangeKeyItemChangeable(false);
			btnEnableChange(true);
			Txt登録件数.Focusable = false;

		}

		/// <summary>
		/// 類似用取得データを変数に代入
		/// </summary>
		/// <param name="tbl"></param>
		private void RstrData(DataTable tbl)
		{
				if (!string.IsNullOrEmpty(tbl.Rows[0]["削除日付"].ToString()))
				{
					ScreenClear();
					this.ErrorMessage = "既に削除されたデータです。";
					return;
				}

				MstData = tbl.Rows[0];


				//車輌KEY = MstData["車輌KEY"].ToString();
				//車輌ID = MstData["車輌ID"].ToString();
				所有者名 = MstData["所有者名"].ToString();
				所有者住所 = MstData["所有者住所"].ToString();
				使用者名 = MstData["使用者名"].ToString();
				使用者住所 = MstData["使用者住所"].ToString();

				//バインド用のプロパティ後追加分
				車輌番号 = MstData["車輌番号"].ToString();
				車輌登録番号 = MstData["車輌登録番号"].ToString();

				//運輸局コンボボックス設定
				foreach (DataRow dr in 所属運輸局View.Table.Rows)
				{
					if (MstData["運輸局ID"].ToString() == dr["運輸局ID"].ToString())
					{
						ComboUnyukyoku.Combo_SelectedValue = dr["運輸局ID"];
						break;
					}

				}

		}

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
                int iSyaryouId = 0;
                switch (message.GetMessageName())
				{
					//検索時処理
					case TargetTableNm:
						strData(tbl);

						iSyaryouId = AppCommon.IntParse(車輌ID);
						GetSubData(iSyaryouId);
						SetFocusToTopControl();
						break;
					//検索時処理
					case RTargetTableNm:
						類似ID = null;
                        if (tbl == null || tbl.Rows.Count < 1)
                        {
                            return;
                        }
						RstrData(tbl);

						iSyaryouId = AppCommon.IntParse(車輌ID);
						//GetSubData(iSyaryouId);
						//SetFocusToTopControl();
						break;
                    case TargetTableNmBtn:
                        if (tbl == null || tbl.Rows.Count < 1)
                        {
                            return;
                        }
                        strData(tbl);

                        iSyaryouId = AppCommon.IntParse(車輌ID);
                        GetSubData(iSyaryouId);
                        SetFocusToTopControl();
                        break;

                    //更新時処理
                    case TargetTableNmUpdate:

                        if ((int)data == -1)
                        {
                            MessageBoxResult result = MessageBox.Show("車輌ID: " + 車輌ID + "は既に使われています。\n自動採番して登録しますか？",
                                                                                                            "質問",
                                                                                                           MessageBoxButton.YesNo,
                                                                                                           MessageBoxImage.Exclamation,
                                                                                                           MessageBoxResult.No);

                            if (result == MessageBoxResult.No)
                            {
                                return;
                            }

                            iSyaryouId = AppCommon.IntParse(車輌ID);
                            MstData["車輌ID"] = iSyaryouId;

                            MstData["車輌番号"] = 車輌番号;
                            MstData["車輌登録番号"] = 車輌登録番号;
                            MstData["所有者名"] = 所有者名;
                            MstData["所有者住所"] = 所有者住所;
                            MstData["使用者名"] = 使用者名;
                            MstData["使用者住所"] = 使用者住所;
                            MstData["運輸局ID"] = ComboUnyukyoku.Combo_SelectedValue == null ? DBNull.Value : ComboUnyukyoku.Combo_SelectedValue;

                            DataTable dt = new DataTable(TargetTableNmUpdate);
                            foreach (DataColumn col in this.MstData.Table.Columns)
                            {
                                DataColumn newcol = new DataColumn(col.ColumnName, col.DataType);
                                newcol.AllowDBNull = col.AllowDBNull;
                                dt.Columns.Add(newcol);
                            }
                            DataRow row = dt.NewRow();
                            foreach (DataColumn col in dt.Columns)
                            {
                                row[col.ColumnName] = this.MstData[col.ColumnName];
                            }
                            dt.Rows.Add(row);

                            base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmUpdate, new object[] { dt
                                                                                                                                ,this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false
                                                                                                                                ,true}));

                            break;
                        }
                        MessageBox.Show("データの更新が完了しました。");
                        //コントロール初期化
                        ScreenClear();

                        break;

                    //削除時処理
                    case TargetTableNmDelete:
                        MessageBox.Show("データの削除が完了しました。");
                        //コントロール初期化
                        ScreenClear();

                        break;

                    //自動採番
                    case GetNextID:
                        if (data is int)
                        {
                            int iNextCode = (int)data;
                            if (iNextCode < 1)
                            {
                                iNextCode = 1;
                            }
                            車輌ID = iNextCode.ToString();

                            //車輌マスタ
                            base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iNextCode }));
                        }
                        break;

                    //運輸局データ取得
                    case UnyukyokuTablNm:
                        this.所属運輸局View = new DataView(tbl);
                        ScreenClear();

                        break;
                    case 強制保険データ取得:
                        if (data == null)
                        {
                            break;
                        }
                        this.強制保険データ = (data as DataTable);
                        break;
                    case 任意保険データ取得:
                        if (data == null)
                        {
                            break;
                        }
                        this.任意保険データ = (data as DataTable);
                        break;
                    case 納税データ取得:
                        if (data == null)
                        {
                            break;
                        }
                        //datatableをlistへ変換
                        納税データ = (List<M05_CDT4_Member>)AppCommon.ConvertFromDataTable(typeof(List<M05_CDT4_Member>), data);
                        break;
                    case CarList:
                        //車輌管理台帳出力
                        DispPreviw(tbl);
                        break;
                    case CAR_ID_CHG:
                        if (data is int)
                        {
                            switch ((int)data)
                            {
                                case -1:
                                    MessageBox.Show("このIDはすでに使用済みです。");
                                    break;
                                case 0:
                                    MessageBox.Show("変換に失敗しました。");
                                    break;
                                default:
                                    車輌ID = ID変換.Text;
                                    ID変換.Text = null;
                                    MessageBox.Show("変換完了しました。");
                                    break;
                            }
                        }
                        break;

                    case GetMaxMeisaiNo:
                        登録件数 = (int)data;
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }
        }

        public override void OnReveivedError(CommunicationObject message)
        {
            base.OnReveivedError(message);
            MessageBox.Show(ErrorMessage);
        }

        /// <summary>
        /// 最初のIDを表示するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FistIdButton_Click(object sender, RoutedEventArgs e)
        {
            //先頭データ検索
            try
            {
                //車輌マスタ
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNmBtn, new object[] { null, 0 }));

            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// １つ前のIDを表示するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeforeIdButton_Click(object sender, RoutedEventArgs e)
        {

            //前データ検索
            try
            {
                int iSyaryouId = 0;
                if (int.TryParse(車輌ID, out iSyaryouId))
                {
                    //車輌マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNmBtn, new object[] { iSyaryouId, -1 }));
                }
                else
                {
                    //車輌マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNmBtn, new object[] { null, 0 }));
                }
            }
            catch (Exception)
            {
                return;
            }

        }

        /// <summary>
        /// １つ次のIDを表示するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextIdButton_Click(object sender, RoutedEventArgs e)
        {
            //次データ検索
            try
            {
                int iSyaryouId = 0;
                if (int.TryParse(車輌ID, out iSyaryouId))
                {
                    //車輌マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNmBtn, new object[] { iSyaryouId, 1 }));
                }
                else
                {
                    //車輌マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNmBtn, new object[] { null, 0 }));
                }

            }
            catch (Exception)
            {
                return;
            }

        }

        /// <summary>
        /// 最後尾のIDを表示するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastIdButoon_Click(object sender, RoutedEventArgs e)
        {

            //最後尾検索
            try
            {
                //車輌マスタ
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNmBtn, new object[] { null, 1 }));

            }
            catch (Exception)
            {
                return;
            }

        }

        /// <summary>
        /// 更新処理
        /// </summary>
        private void Update()
        {

            try
            {
                #region SpreadのKEY重複チェック

                //強制保険の重複チェック
                int RowsCnt01A = sp強制保険.Rows.Count;
                int RowsCnt01B = (from x in sp強制保険.Rows select x.Cells[sp強制保険.Columns["Str加入年月日"].Index].Text).Distinct().Count();
                if (RowsCnt01A != RowsCnt01B)
                {
                    this.ErrorMessage = "強制保険情報の入力形式が不正です。";
                    MessageBox.Show("強制保険情報の入力形式が不正です。");
                    return;
                }

                //未入力チェック
                foreach (var row in sp強制保険.Rows)
                {
                    string 加入年月日 = row.Cells[sp強制保険.Columns["Str加入年月日"].Index].Text;
                    string 期限年月日 = row.Cells[sp強制保険.Columns["Str期限年月日"].Index].Text;

                    DateTime CheckDate;
                    if (DateTime.TryParse(加入年月日, out CheckDate) == false || CheckDate == Convert.ToDateTime("0001/01/01"))
                    {
                        this.ErrorMessage = "加入年月日の入力形式が不正です。";
                        MessageBox.Show("加入年月日の入力形式が不正です。");
                        return;
                    }
                    else if (DateTime.TryParse(期限年月日, out CheckDate) == false || CheckDate == Convert.ToDateTime("0001/01/01"))
                    {
                        this.ErrorMessage = "期限の入力形式が不正です。";
                        MessageBox.Show("期限の入力形式が不正です。");
                        return;
                    }
                }

                //任意保険の重複チェック
                int RowsCnt02A = sp任意保険.Rows.Count;
                int RowsCnt02B = (from x in sp任意保険.Rows select x.Cells[sp任意保険.Columns["Str加入年月日"].Index].Text).Distinct().Count();
                if (RowsCnt02A != RowsCnt02B)
                {
                    this.ErrorMessage = "任意保険情報の入力形式が不正です。";
                    MessageBox.Show("任意保険情報の入力形式が不正です。");
                    return;
                }

                foreach (var row in sp任意保険.Rows)
                {
                    string 加入年月日 = row.Cells[sp任意保険.Columns["Str加入年月日"].Index].Text;
                    string 期限年月日 = row.Cells[sp任意保険.Columns["Str期限年月日"].Index].Text;

                    DateTime CheckDate;
                    if (DateTime.TryParse(加入年月日, out CheckDate) == false || CheckDate == Convert.ToDateTime("0001/01/01"))
                    {
                        this.ErrorMessage = "加入年月日の入力形式が不正です。";
                        MessageBox.Show("加入年月日の入力形式が不正です。");
                        return;
                    }
                    else if (DateTime.TryParse(期限年月日, out CheckDate) == false || CheckDate == Convert.ToDateTime("0001/01/01"))
                    {
                        this.ErrorMessage = "期限の入力形式が不正です。";
                        MessageBox.Show("期限の入力形式が不正です。");
                        return;
                    }
                }

                //納税情報の重複チェック
                int RowsCnt03A = sp納税.Rows.Count;
                int RowsCnt03B = (from x in sp納税.Rows select x.Cells[sp納税.Columns["S_年度"].Index].Text).Distinct().Count();
                if (RowsCnt03A != RowsCnt03B)
                {
                    this.ErrorMessage = "年度の入力形式が不正です。";
                    MessageBox.Show("年度の入力形式が不正です。");
                    return;
                }

                foreach (var row in sp納税.Rows)
                {
                    string 年度 = row.Cells[sp納税.Columns["S_年度"].Index].Text;
                    int init;
                    if (int.TryParse(年度, out init) == false)
                    {
                        this.ErrorMessage = "納税情報の入力形式が不正です。";
                        MessageBox.Show("納税情報の入力形式が不正です。");
                        return;
                    }
                }

                #endregion

                int i車輌ID = 0;
                if (!int.TryParse(車輌ID, out i車輌ID))
                {
                    this.ErrorMessage = "車輌IDの入力形式が不正です。";
                    MessageBox.Show("車輌IDの入力形式が不正です。");
                    return;
                }

                //if (JmiName.Text1 == null || JmiName.Text1 == "0")
                //{
                //    this.ErrorMessage = "乗務員IDの入力形式が不正です。";
                //    MessageBox.Show("乗務員IDの入力形式が不正です。");
                //}

                if (!base.CheckAllValidation())
                {
                    this.ErrorMessage = "入力内容に誤りがあります。";
                    MessageBox.Show("入力内容に誤りがあります。");
                    SetFocusToTopControl();
                    return;
                }


                int iSyaryouId = 0;
                if (int.TryParse(車輌ID, out iSyaryouId))
                {
                    MstData["車輌ID"] = iSyaryouId;
                    MstData["車輌番号"] = 車輌番号;
                    MstData["車輌登録番号"] = 車輌登録番号;
                    MstData["所有者名"] = 所有者名;
                    MstData["所有者住所"] = 所有者住所;
                    MstData["使用者名"] = 使用者名;
                    MstData["使用者住所"] = 使用者住所;
                    MstData["運輸局ID"] = ComboUnyukyoku.Combo_SelectedValue == null ? DBNull.Value : ComboUnyukyoku.Combo_SelectedValue;
                    MstData["廃車日"] = ltb廃車日.Text == string.Empty ? DBNull.Value : MstData["廃車日"];
                    MstData["次回車検日"] = ltb次回車検日.Text == string.Empty ? DBNull.Value : MstData["次回車検日"];
                    MstData["登録日"] = ltb登録日.Text == string.Empty ? DBNull.Value : MstData["登録日"];

                    MstData["長さ"] = Car1.Text == string.Empty ? 0 : MstData["長さ"];
                    MstData["幅"] = Car2.Text == string.Empty ? 0 : MstData["幅"];
                    MstData["高さ"] = Car3.Text == string.Empty ? 0 : MstData["高さ"];
                    MstData["総排気量"] = Car4.Text == string.Empty ? 0 : MstData["総排気量"];

                    MstData["車輌重量"] = Kg1.Text == string.Empty ? 0 : MstData["車輌重量"];
                    MstData["車輌最大積載量"] = Kg2.Text == string.Empty ? 0 : MstData["車輌最大積載量"];
                    MstData["車輌総重量"] = Kg3.Text == string.Empty ? 0 : MstData["車輌総重量"];
                    MstData["車輌実積載量"] = Kg4.Text == string.Empty ? 0 : MstData["車輌実積載量"];

                    MstData["前前軸重"] = Cars2.Text == string.Empty ? 0 : MstData["前前軸重"];
                    MstData["前後軸重"] = Cars3.Text == string.Empty ? 0 : MstData["前後軸重"];
                    MstData["後前軸重"] = Cars4.Text == string.Empty ? 0 : MstData["後前軸重"];
                    MstData["後後軸重"] = Cars5.Text == string.Empty ? 0 : MstData["後後軸重"];

                    MstData["固定自動車税"] = Zei1.Text == string.Empty ? 0 : MstData["固定自動車税"];
                    MstData["固定重量税"] = Zei2.Text == string.Empty ? 0 : MstData["固定重量税"];
                    MstData["固定取得税"] = Zei3.Text == string.Empty ? 0 : MstData["固定取得税"];

                    MstData["固定自賠責保険"] = Hoken1.Text == string.Empty ? 0 : MstData["固定自賠責保険"];
                    MstData["固定車輌保険"] = Hoken2.Text == string.Empty ? 0 : MstData["固定車輌保険"];
                    MstData["固定対人保険"] = Hoken3.Text == string.Empty ? 0 : MstData["固定対人保険"];
                    MstData["固定対物保険"] = Hoken4.Text == string.Empty ? 0 : MstData["固定対物保険"];
                    MstData["固定貨物保険"] = Hoken5.Text == string.Empty ? 0 : MstData["固定貨物保険"]; 





                    DataTable dt = new DataTable(TargetTableNmUpdate);
                    foreach (DataColumn col in this.MstData.Table.Columns)
                    {
                        DataColumn newcol = new DataColumn(col.ColumnName, col.DataType);
                        newcol.AllowDBNull = col.AllowDBNull;
                        dt.Columns.Add(newcol);
                    }
                    DataRow row = dt.NewRow();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row[col.ColumnName] = this.MstData[col.ColumnName];
                    }
                    dt.Rows.Add(row);

                    for (int i = 0; i < 強制保険データ.Rows.Count; i++)
                    {
                        強制保険データ.Rows[i]["車輌KEY"] = 車輌KEY;
                    }

                    for (int i = 0; i < 任意保険データ.Rows.Count; i++)
                    {
                        任意保険データ.Rows[i]["車輌KEY"] = 車輌KEY;
                    }

                    foreach (M05_CDT4_Member 納税row in 納税データ)
                    {
                        納税row.車輌KEY = AppCommon.IntParse(車輌KEY);
                        納税row.自動車税 = 納税row.d自動車税 == null ? null : (int?)AppCommon.IntParse(納税row.d自動車税.ToString());
                        納税row.重量税 = 納税row.d重量税 == null ? null : (int?)AppCommon.IntParse(納税row.d重量税.ToString());
                    }
                    納税dt = new DataTable();
                    //リストをデータテーブルへ
                    AppCommon.ConvertToDataTable(納税データ, 納税dt);

                    var yesno = MessageBox.Show("入力内容を登録しますか？", "登録確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                    if (yesno == MessageBoxResult.Yes)
                    {
                        base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmUpdate, new object[] { dt
                                                                                                                        ,this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD ? true : false
                                                                                                                        ,false
                                                                                                                        ,this.強制保険データ
                                                                                                                        ,this.任意保険データ
                                                                                                                        ,納税dt
                                                                                                                        }));
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }

        /// <summary>
        /// 削除処理
        /// </summary>
        private void Delete()
        {

            int iSyaryouId = 0;
            if (int.TryParse(車輌ID, out iSyaryouId))
            {
                MstData["車輌ID"] = iSyaryouId;

                DataTable dt = new DataTable(TargetTableNmDelete);
                foreach (DataColumn col in this.MstData.Table.Columns)
                {
                    DataColumn newcol = new DataColumn(col.ColumnName, col.DataType);
                    newcol.AllowDBNull = col.AllowDBNull;
                    dt.Columns.Add(newcol);
                }
                DataRow row = dt.NewRow();
                foreach (DataColumn col in dt.Columns)
                {
                    row[col.ColumnName] = this.MstData[col.ColumnName];
                }
                dt.Rows.Add(row);


                base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmDelete, dt));
            }
        }

        #region リボン
        /// <summary>
        /// F1 リボン　検索
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
        /// F2 リボン　マスタ登録
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
                appLog.Error("検索画面起動エラー", ex);
                this.ErrorMessage = "システムエラーです。サポートへご連絡ください。";
            }
        }


        /// <summary>
        /// F7 車輌台帳PRE印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF7Key(object sender, KeyEventArgs e)
        {
            try
            {
                base.SendRequest(new CommunicationObject(MessageType.RequestData, CarList, new object[] { }));
            }
            catch (Exception)
            {
                MessageBox.Show("印刷できませんでした。");
            }
        }

        /// <summary>
        /// F8 リスト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
            MST06020 mst06020 = new MST06020();
            mst06020.ShowDialog(this);
        }

        /// <summary>
        /// F9　登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF9Key(object sender, KeyEventArgs e)
        {
            Update();
        }

        /// <summary>
        /// F10　リボン入力取消し　
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF10Key(object sender, KeyEventArgs e)
        {
            //メッセージボックス
            MessageBoxResult result = MessageBox.Show("保存せずに入力を取り消しても宜しいですか？"
                            , "確認"
                            , MessageBoxButton.YesNo
                            , MessageBoxImage.Question);
            //OKならクリア
            if (result == MessageBoxResult.Yes)
            {
                this.ScreenClear();
            }
        }

        /// <summary>
        /// F11　リボン終了
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

            if (MstData == null)
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

            MessageBoxResult result = MessageBox.Show("データを削除しても宜しいですか？"
                             , "確認"
                             , MessageBoxButton.YesNo
                             , MessageBoxImage.Question
                             , MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
            {
                Delete();
            }
        }

        #endregion

        #region プレビュー画面表示
        /// <summary>
        /// プレビュー画面表示
        /// </summary>
        /// <param name="tbl"></param>
        private void DispPreviw(DataTable tbl)
        {
            try
            {
                if (tbl.Rows.Count < 1)
                {
                    this.ErrorMessage = "対象データが存在しません。";
                    return;
                }
                //印刷処理
                KyoeiSystem.Framework.Reports.Preview.ReportPreview view = new KyoeiSystem.Framework.Reports.Preview.ReportPreview();
                //第1引数　帳票タイトル
                //第2引数　帳票ファイルPass
                //第3以上　帳票の開始点(0で良い)
                view.MakeReport("車輌台帳問合せ", rptFullPathName_PIC, 0, 0, 0);
                //帳票ファイルに送るデータ。
                //帳票データの列と同じ列名を保持したDataTableを引数とする
                view.SetReportData(tbl);
				view.ShowPreview();
				view.Close();

                // 印刷した場合
                if (view.IsPrinted)
                {
                    //印刷した場合はtrueを返す
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 処理メソッド
        private void ScreenClear()
        {
            new_ID = null;
            MstData = null;
            車輌ID = string.Empty;
            車輌番号 = string.Empty;
            車輌登録番号 = string.Empty;
            所有者ID = string.Empty;
            所有者名 = string.Empty;
            所有者住所 = string.Empty;
            使用者ID = string.Empty;
            使用者名 = string.Empty;
            使用者住所 = string.Empty;
            this.強制保険データ = null;
            this.任意保険データ = null;
            this.納税データ = null;

            this.MaintenanceMode = string.Empty;
			txt類似ID.Visibility = Visibility.Collapsed;
			ID変換.Visibility = Visibility.Collapsed;
            this.ErrorMessage = string.Empty;
            //ユーザーコントロールValidation初期化
            this.ResetAllValidation();

            //コンボボックス選択初期化
            ComboUnyukyoku.Combo_SelectedIndex = -1;

            //sp強制保険.BeginInit();
            //sp強制保険.EndInit();
            //sp任意保険.BeginInit();
            //sp任意保険.EndInit();

            //sp強制保険.Reset();
            //sp任意保険.Reset();

            ChangeKeyItemChangeable(true);
            btnEnableChange(true);
            ResetAllValidation();

            //フォーカス設定
            SetFocusToTopControl();

            //現在の登録件数を表示
            base.SendRequest(new CommunicationObject(MessageType.RequestData, GetMaxMeisaiNo));
        }

        #endregion

        /// <summary>
        /// 車輌IDキーダウン時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelTextSyaryou_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    int iSyaryouId = 0;

                    if (string.IsNullOrEmpty(車輌ID))
                    {
                        //自動採番
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, GetNextID, new object[] { iSyaryouId }));
                        return;
                    }

                    if (int.TryParse(車輌ID, out iSyaryouId))
                    {
                        //車輌マスタ
                        base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iSyaryouId }));
                    }
                    else
                    {
                        this.ErrorMessage = "車輌IDの形式が不正です。";
                        MessageBox.Show("車輌IDの形式が不正です。");
                        return;
                    }

                }
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 前後ボタンのEnableを変更する。
        /// </summary>
        /// <param name="pBool"></param>
        private void btnEnableChange(bool pBool)
        {
            FistIdButton.IsEnabled = pBool;
            BeforeIdButton.IsEnabled = pBool;
            NextIdButton.IsEnabled = pBool;
            LastIdButton.IsEnabled = pBool;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {

			sp強制保険.InputBindings.Clear();
			sp任意保険.InputBindings.Clear();
			sp納税.InputBindings.Clear();
			強制保険データ = null;
			任意保険データ = null;
			納税データ = null;

            //画面が閉じられた時、データを保持する
            frmcfg.Top = this.Top;
            frmcfg.Left = this.Left;
            frmcfg.Height = this.Height;
            frmcfg.Width = this.Width;
            ucfg.SetConfigValue(frmcfg);
        }

        /// <summary>
        /// 関連テーブルの読み込み
        /// </summary>
        private void GetSubData(int iCarCD)
        {
            try
            {
                CommunicationObject[] comlist = {
												new CommunicationObject(MessageType.RequestData, 強制保険データ取得, iCarCD),
                                                new CommunicationObject(MessageType.RequestData, 任意保険データ取得, iCarCD),
                                                new CommunicationObject(MessageType.RequestData, 納税データ取得, iCarCD),
                                                
											};
                SendRequest(comlist);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 強制保険タブ行削除ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sp強制保険_Delete_Click(object sender, RoutedEventArgs e)
        {
            spGrid_Delete(sp強制保険, this.強制保険データ);
        }

        /// <summary>
        /// 強制保険タブ行追加ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sp強制保険_Insert_Click(object sender, RoutedEventArgs e)
        {
            this.強制保険データ.Rows.InsertAt(this.強制保険データ.NewRow(), 0);
            this.sp強制保険.Select(new CellRange(0, 0), SelectionType.New);
        }

        /// <summary>
        /// 任意保険タブ行削除ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sp任意保険_Delete_Click(object sender, RoutedEventArgs e)
        {
            spGrid_Delete(sp任意保険, this.任意保険データ);
        }

        /// <summary>
        /// 任意保険タブ行追加ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sp任意保険_Insert_Click(object sender, RoutedEventArgs e)
        {
            this.任意保険データ.Rows.InsertAt(this.任意保険データ.NewRow(), 0);
            this.sp任意保険.Select(new CellRange(0, 0), SelectionType.New);
        }
        /// <summary>
        /// 納税タブ行削除ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sp納税_Delete_Click(object sender, RoutedEventArgs e)
        {
            sp納税.Rows.Remove(sp納税.ActiveCellPosition.Row, 1);
            //spGrid_Delete(sp納税, 納税データ);
        }
        /// <summary>
        /// 納税タブ行追加ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sp納税_Insert_Click(object sender, RoutedEventArgs e)
        {

            List<M05_CDT4_Member> 納税データ2 = 納税データ;
            納税データ2.Insert(0, new M05_CDT4_Member());
            納税データ = null;
            納税データ = 納税データ2;

            //納税dt  = new DataTable();
            //    //リストをデータテーブルへ
            //    AppCommon.ConvertToDataTable(納税データ, 納税dt);
            //    納税dt.Rows.InsertAt(納税dt.NewRow(), 0);

            //    //datatableをlistへ変換
            //    納税データ = (List<M05_CDT4_Member>)AppCommon.ConvertFromDataTable(typeof(List<M05_CDT4_Member>), 納税dt);


            //納税データ.Insert(0, new M05_CDT4_Member());
            //this.sp納税.Select(new CellRange(0, 0), SelectionType.New);
        }

        /// <summary>
        /// Spread行削除ボタン
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="tbl"></param>
        void spGrid_Delete(GcSpreadGrid sp, DataTable tbl)
        {
            if (sp.SelectedItem is DataRowView)
            {
                int selrow = sp.SelectedRanges.Count > 0 ? sp.SelectedRanges[0].Row : 0;
                tbl.Rows.Remove((sp.SelectedItem as DataRowView).Row);
                if (sp.Rows.Count > 0)
                {
                    if (selrow > sp.Rows.Count - 1)
                    {
                        selrow--;
                    }
                    sp.Select(new CellRange(selrow, 0), SelectionType.New);
                }
            }
        }

        private void LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void spread_CellEditEnded(object sender, SpreadCellEditEndedEventArgs e)
        {
            string cname = e.CellPosition.ColumnName;
            if (cname.Contains("年月日") == true)
            {
                AppCommon.SpreadYMDCellCheck(sender, e, this._originalText);
            }
        }
        string _originalText = null;

        private void spread_CellEnter(object sender, SpreadCellEnterEventArgs e)
        {
            var grid = sender as GcSpreadGrid;
            if (grid == null) return;
            if (grid.RowCount == 0) return;
            this._originalText = grid.Cells[e.Row, e.Column].Text;
        }

        private void KeyDownUpDate(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Update();
            }
        }


        private void UcTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var ctl = sender as UcTextBox;
            if (ctl == null)
            {
                ctl = ViewBaseCommon.FindVisualChild<UcTextBox>(sender as DependencyObject);
            }
            if (ctl == null)
            {
                return;
            }
            foreach (var item in scrollYlist)
            {
                if (item.fld.Equals(ctl))
                {
                    var a = General_ScrollViewer.ContentVerticalOffset;

                    if ((item.Y + ctl.ActualHeight + 20) > General_ScrollViewer.ActualHeight + a)
                    {
                        // 入力フィールドがちゃんと見える範囲に自動スクロールする。
                        General_ScrollViewer.ScrollToVerticalOffset(item.Y - General_ScrollViewer.ActualHeight + ctl.ActualHeight + 20);

                        // カレントフィールドを大体中央付近に見える位置にスクロールする場合は下記を使う
                        //General_ScrollViewer.ScrollToVerticalOffset(item.Y - (General_ScrollViewer.ActualHeight / 2));
                    }
                    break;
                }
            }

        }

        #region フォーカス制御1

        private void Down1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Kg1.Focus();
            }

        }

        private void Down2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Kg2.Focus();
            }
        }

        private void Down3(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Kg3.Focus();
            }
        }

        private void Down4(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Car1.Focus();
            }
        }


        private void Down5(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Car2.Focus();
            }

        }

        private void Down6(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Car3.Focus();
            }
        }

        private void Down7(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Car4.Focus();
            }
        }

        #endregion

        #region フォーカス制御2

        private void Down11(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Tanka1.Focus();
            }
        }

        private void Down12(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Tanka2.Focus();
            }
        }

        private void Down13(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Tanka3.Focus();
            }
        }

        private void Down14(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Tanka4.Focus();
            }
        }

        private void Down15(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Cars1.Focus();
            }
        }

        private void Down16(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Tanka1.Text == string.Empty)
                {
                    MstData["燃料費単価"] = 0;
                }
                Cars2.Focus();
            }
        }

        private void Down17(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Tanka2.Text == string.Empty)
                {
                    MstData["油脂費単価"] = 0;
                }
                Cars3.Focus();
            }
        }

        private void Down18(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Tanka3.Text == string.Empty)
                {
                    MstData["タイヤ費単価"] = 0;
                }

                Cars4.Focus();
            }
        }


        private void Down19(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Tanka4.Text == string.Empty)
                {
                    MstData["修繕費単価"] = 0;
                }
                Cars5.Focus();
            }
        }

        private void Down20(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Tanka5.Text == string.Empty)
                {
                    MstData["車検費単価"] = 0;
                }
            }
        }

        #endregion

        #region フォーカス制御3

        private void Down21(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Hoken2.Focus();
            }
        }

        private void Down22(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Hoken3.Focus();
            }
        }

        private void Down23(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Hoken4.Focus();
            }
        }

        private void Down24(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Zei1.Focus();
            }
        }

        private void Down25(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Zei2.Focus();
            }
        }

        private void Down26(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Zei3.Focus();
            }
        }

        private void Down27(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Hoken1.Focus();
            }
        }

        #endregion

        private void ID変換_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int old_ID = 0;
                if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_EDIT)
                {
                    if (new_ID == 0 || new_ID == null)
                    {
                        return;
                    }
                    if (!int.TryParse(車輌ID, out old_ID))
                    {
                        MessageBox.Show("入力内容に誤りがあります。");
                        return;
                    }
                    var yesno = MessageBox.Show("IDを変更しますか？", "確認", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (yesno == MessageBoxResult.No)
                    {
                        return;
                    }
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, CAR_ID_CHG, old_ID, new_ID));
                }
                else
                {
                    MessageBox.Show("変換するデータを呼び出して下さい。");
                    return;
                }
            }


        }

		private void txt類似ID_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				if (this.MaintenanceMode == AppConst.MAINTENANCEMODE_ADD)
				{

					//発着地データ検索
					base.SendRequest(new CommunicationObject(MessageType.RequestData, RTargetTableNm, new object[] { 類似ID }));
				}
			}
		}

    }
}
