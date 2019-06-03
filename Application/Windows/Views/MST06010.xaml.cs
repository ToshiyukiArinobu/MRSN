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

using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Windows.ViewBase;


namespace KyoeiSystem.Application.Windows.Views
{
	/// <summary>
    /// 車輌マスタ入力
    /// </summary>
	public partial class MST06010 : WindowMasterMainteBase
	{
        //対象テーブル検索用
        private const string TargetTableNm = "M05_CAR_UC";
        //対象テーブルボタン検索用
        private const string TargetTableNmBtn = "M05_CAR_BTN";
        //対象テーブル更新用
        private const string TargetTableNmUpdate = "M05_CAR_UPD";
        //対象テーブル削除用
        private const string TargetTableNmDelete = "M05_CAR_DEL";
        //運輸局情報取得
        private const string UnyukyokuTablNm = "M84_RIK_UC";


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
        private void RibbonWindow_Loaded_1(object sender, RoutedEventArgs e)
		{
            base.MasterMaintenanceWindowList.Add("取引先", new List<Type> { typeof(MST01010), typeof(SCH14010) });
            base.MasterMaintenanceWindowList.Add("支払先", new List<Type> { typeof(MST01010), typeof(SCH15010) });
            base.MasterMaintenanceWindowList.Add("仕入先", new List<Type> { typeof(MST01010), typeof(SCH16010) });
            base.MasterMaintenanceWindowList.Add("請求内訳", new List<Type> { typeof(MST02010), null });
            base.MasterMaintenanceWindowList.Add("乗務員", new List<Type> { typeof(MST04010), typeof(SCH04010) });
            base.MasterMaintenanceWindowList.Add("車種", new List<Type> { typeof(MST05010), typeof(SCH05010) });
            base.MasterMaintenanceWindowList.Add("車輌", new List<Type> { typeof(MST06010), typeof(SCH06010) });
            base.MasterMaintenanceWindowList.Add("商品", new List<Type> { typeof(MST07010), typeof(SCH07010) });
            base.MasterMaintenanceWindowList.Add("摘要", new List<Type> { typeof(MST08010), typeof(SCH08010) });
            base.MasterMaintenanceWindowList.Add("自社部門", new List<Type> { typeof(MST10010), typeof(SCH10010) });
            base.MasterMaintenanceWindowList.Add("コース配車", new List<Type> { typeof(MST11010), typeof(SCH11010) });
            base.MasterMaintenanceWindowList.Add("自社名", new List<Type> { typeof(MST12010), typeof(SCH12010) });
            base.MasterMaintenanceWindowList.Add("消費税率", new List<Type> { typeof(MST13010), null });
            base.MasterMaintenanceWindowList.Add("支払先別軽油", new List<Type> { typeof(MST14010), null });
            base.MasterMaintenanceWindowList.Add("得意先別車種別単価", new List<Type> { typeof(MST16010), null });
            base.MasterMaintenanceWindowList.Add("得意先別距離別運賃", new List<Type> { typeof(MST17010), null });
            base.MasterMaintenanceWindowList.Add("得意先別個建単価", new List<Type> { typeof(MST18010), null });
            base.MasterMaintenanceWindowList.Add("支払先別地区単価", new List<Type> { typeof(MST19010), null });
            base.MasterMaintenanceWindowList.Add("支払先別車種別単価", new List<Type> { typeof(MST20010), null });
            base.MasterMaintenanceWindowList.Add("支払先別距離別運賃", new List<Type> { typeof(MST21010), null });
            base.MasterMaintenanceWindowList.Add("支払先別個建単価", new List<Type> { typeof(MST22010), null });
            base.MasterMaintenanceWindowList.Add("担当者", new List<Type> { typeof(MST23010), typeof(SCH13010) });

            base.SendRequest(new CommunicationObject(MessageType.RequestData, UnyukyokuTablNm, new object[] { null }));


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
				switch (message.GetMessageName())
				{
                    //検索時処理
                    case TargetTableNm:
                    case TargetTableNmBtn:
                        if (tbl.Rows.Count == 0)
                        {
                            MstData = tbl.NewRow();
                        }
                        else
                        {
                            MstData = tbl.Rows[0];

                            車輌ID = MstData["車輌ID"].ToString();
                            所有者名 = MstData["所有者名"].ToString();
                            所有者住所 = MstData["所有者住所"].ToString();
                            使用者名 = MstData["使用者名"].ToString();
                            使用者住所 = MstData["使用者住所"].ToString();
                            //運輸局コンボボックス設定
                            foreach(DataRow dr in 所属運輸局View.Table.Rows)
                            {
                                if (MstData["運輸局ID"].ToString() == dr["運輸局ID"].ToString())
                                {
                                    ComboUnyukyoku.Combo_SelectedValue = dr["運輸局ID"];
                                    break;
                                }

                            }
                        }
                        //車輌ID変更不可
                        LabelTextSyaryou.Text1IsReadOnly = true;
					break;

                    //更新時処理
                    case TargetTableNmUpdate:
                        MessageBox.Show("データの更新が完了しました。");
                        //コントロール初期化
                        UcInitialize();

                    break;

                    //削除時処理
                    case TargetTableNmDelete:
                    MessageBox.Show("データの削除が完了しました。");
                    //コントロール初期化
                    UcInitialize();

                    break;

                    //運輸局データ取得
                    case UnyukyokuTablNm:
                    this.所属運輸局View = new DataView(tbl);

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
                int iSyaryouId = 0;
                if (int.TryParse(車輌ID, out iSyaryouId))
                {
                    MstData["車輌ID"] = iSyaryouId;

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


                    base.SendRequest(new CommunicationObject(MessageType.UpdateData, TargetTableNmUpdate, dt));
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
        /// F6 リボン　一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF6Key(object sender, KeyEventArgs e)
        {
            MST06020 mst06020 = new MST06020();

            mst06020.ShowDialog();
        }


        /// <summary>
        /// F8 リボン　印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF8Key(object sender, KeyEventArgs e)
        {
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
            UcInitialize();
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
            Delete();
        }

        private void Button_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == false)
            {
                (sender as Button).IsEnabled = true;
            }
        }

        private void CheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as UIElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
        #endregion

        private void UcLabelTwinTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                int iSyaryouId = 0;
                if (int.TryParse(車輌ID, out iSyaryouId))
                {
                    //車輌マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iSyaryouId}));
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        #region 処理メソッド
        private void UcInitialize()
        {
            車輌ID = string.Empty;
            所有者ID = string.Empty;
            所有者名 = string.Empty;
            所有者住所 = string.Empty;
            使用者ID = string.Empty;
            使用者名 = string.Empty;
            使用者住所 = string.Empty;
            MstData = null;

            LabelTextSyaryou.Text1IsReadOnly = false;

            //コンボボックス選択初期化
            ComboUnyukyoku.Combo_SelectedIndex = -1;


            //フォーカス設定
            SetFocusToTopControl();
        }

        #endregion

	}
}
