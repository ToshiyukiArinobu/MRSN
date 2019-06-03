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
    /// 支払先別軽油マスタ入力
	/// </summary>
	public partial class MST14010 : WindowMasterMainteBase
	{
        //対象テーブル検索用
        private const string TargetTableNm = "M03_YNEN_UC";
        //対象テーブル更新用
        private const string TargetTableNmUpdate = "M03_YNEN_UPD";
        //対象テーブル更新用
        private const string TargetTableNmDelete = "M03_YNEN_DEL";

        //支払先テーブル検索用
        private const string ShiharaisakiTableNm = "M01_TOK_UC";


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
        #region データバインド用プロパティ
        private string _支払先ID = string.Empty;
        public string 支払先ID
        {
            get { return this._支払先ID; }
            set { this._支払先ID = value; NotifyPropertyChanged(); }
        }
        private string _支払先名１ = string.Empty;
        public string 支払先名１
        {
            get { return this._支払先名１; }
            set { this._支払先名１ = value; NotifyPropertyChanged(); }
        }
        private string _支払先名２ = string.Empty;
        public string 支払先名２
        {
            get { return this._支払先名２; }
            set { this._支払先名２ = value; NotifyPropertyChanged(); }
        }
        private string _支払先ｶﾅ = string.Empty;
        public string 支払先ｶﾅ
        {
            get { return this._支払先ｶﾅ; }
            set { this._支払先ｶﾅ = value; NotifyPropertyChanged(); }
        }
        private DateTime? _適用開始日付 = null;
        public DateTime? 適用開始日付
        {
            get { return this._適用開始日付; }
            set { this._適用開始日付 = value; NotifyPropertyChanged(); }
        }
        private string _軽油単価 = string.Empty;
        public string 軽油単価
        {
            get { return this._軽油単価; }
            set { this._軽油単価 = value; NotifyPropertyChanged(); }
        }
        private string _軽油税単価 = string.Empty;
        public string 軽油税単価
        {
            get { return this._軽油税単価; }
            set { this._軽油税単価 = value; NotifyPropertyChanged(); }
        }
        #endregion
        /// <summary>
        /// 支払先別軽油マスタ入力
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

            UcInitialize();
		}

		
		private void SearchButton_Click_1(object sender, RoutedEventArgs e)
		{
			DataRequest();
		}

		void DataRequest()
		{
			try
			{
			}
			catch (Exception)
			{
				return;
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
                
				switch (message.GetMessageName())
				{
                        //支払先名検索時
                    case ShiharaisakiTableNm:
                        if (tbl.Rows.Count == 0)
                        {
                            return;
                        }
                        else
                        {
                            支払先名１ = tbl.Rows[0]["取引先名１"].ToString();
                            支払先名２ = tbl.Rows[0]["取引先名２"].ToString();
                        }


                        break;

                    //検索時
                    case TargetTableNm:
                        if (tbl.Rows.Count == 0)
                        {
                            MstData = tbl.NewRow();
                        }
                        else
                        {
                            MstData = tbl.Rows[0];

                            支払先ID = MstData["取引先ID"].ToString();
                            適用開始日付 = (DateTime?)MstData["適用開始日"];
                        }
                        //支払先ID変更不可
                        LabelTextShiharaiId.Text1IsReadOnly = true;
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


        #region リボン
        /// <summary>
        /// F6 リボン　一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnF6Key(object sender, KeyEventArgs e)
        {
            
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

        #region 処理メソッド
        /// <summary>
        /// コントロール初期化
        /// </summary>
        private void UcInitialize()
        {

            MstData = null;
            支払先ID = string.Empty;
            支払先名１ = string.Empty;
            支払先名２ = string.Empty;
            適用開始日付 = null;

            //商品ID変更不可
            LabelTextShiharaiId.Text1IsReadOnly = false;
            //フォーカス設定
            SetFocusToTopControl();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        private void Update()
        {

            try
            {
                int iSyouhinId = 0;
                if (int.TryParse(支払先ID, out iSyouhinId))
                {
                    MstData["取引先ID"] = iSyouhinId;
                    MstData["適用開始日"] = 適用開始日付;


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
            int iSyouhinId = 0;
            if (int.TryParse(支払先ID, out iSyouhinId))
            {
                MstData["取引先ID"] = iSyouhinId;

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

        /// <summary>
        /// 支払先IDテキストロストフォーカス時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelTextShiharaiId_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                int iShiharaiId = 0;
                if (int.TryParse(支払先ID, out iShiharaiId))
                {
                    //取引先マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, ShiharaisakiTableNm, new object[] { iShiharaiId }));


                    //日付未選択時処理
                    if (string.IsNullOrEmpty(支払先ID) || 適用開始日付 == null)
                    {
                        return;
                    }

                    //対象マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iShiharaiId, 適用開始日付 }));
                }
            }
            catch (Exception)
            {
                return;
            }

        }
        #endregion

    }
}
