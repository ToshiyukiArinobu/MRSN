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
    /// 自社名マスタ入力
	/// </summary>
	public partial class MST12010 : WindowMasterMainteBase
	{
        //対象テーブル検索用
        private const string TargetTableNm = "M71_JIS_UC";
        //対象テーブル更新用
        private const string TargetTableNmUpdate = "M71_JIS_UPD";
        //対象テーブル更新用
        private const string TargetTableNmDelete = "M71_JIS_DEL";


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
        private string _自社ID = string.Empty;
        public string 自社ID
        {
            get { return this._自社ID; }
            set { this._自社ID = value; NotifyPropertyChanged(); }
        }
        /*private string _事業者番号 = string.Empty;
        public string 事業者番号
        {
            get { return this._事業者番号; }
            set { this._事業者番号 = value; NotifyPropertyChanged(); }
        }
        private string _自社名 = string.Empty;
        public string 自社名
        {
            get { return this._自社名; }
            set { this._自社名 = value; NotifyPropertyChanged(); }
        }        
        private string _代表役職 = string.Empty;
        public string 代表役職
        {
            get { return this._代表役職; }
            set { this._代表役職 = value; NotifyPropertyChanged(); }
        }
        private string _代表者名 = string.Empty;
        public string 代表者名
        {
            get { return this._代表者名; }
            set { this._代表者名 = value; NotifyPropertyChanged(); }
        }
        private string _郵便番号 = string.Empty;
        public string 郵便番号
        {
            get { return this._郵便番号; }
            set { this._郵便番号 = value; NotifyPropertyChanged(); }
        }
        private string _住所１ = string.Empty;
        public string 住所１
        {
            get { return this._住所１; }
            set { this._住所１ = value; NotifyPropertyChanged(); }
        }
        private string _住所２ = string.Empty;
        public string 住所２
        {
            get { return this._住所２; }
            set { this._住所２ = value; NotifyPropertyChanged(); }
        }
        private string _電話番号 = string.Empty;
        public string 電話番号
        {
            get { return this._電話番号; }
            set { this._電話番号 = value; NotifyPropertyChanged(); }
        }
        private string _ＦＡＸ番号 = string.Empty;
        public string ＦＡＸ番号
        {
            get { return this._ＦＡＸ番号; }
            set { this._ＦＡＸ番号 = value; NotifyPropertyChanged(); }
        }
        private string _振込銀行１ = string.Empty;
        public string 振込銀行１
        {
            get { return this._振込銀行１; }
            set { this._振込銀行１ = value; NotifyPropertyChanged(); }
        }
        private string _振込銀行２ = string.Empty;
        public string 振込銀行２
        {
            get { return this._振込銀行２; }
            set { this._振込銀行２ = value; NotifyPropertyChanged(); }
        }
        private string _振込銀行３ = string.Empty;
        public string 振込銀行３
        {
            get { return this._振込銀行３; }
            set { this._振込銀行３ = value; NotifyPropertyChanged(); }
        }*/
        #endregion
        /// <summary>
        /// 自社名マスタ入力
		/// </summary>
        public MST12010()
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

		}

		private void SearchButton_Click_1(object sender, RoutedEventArgs e)
		{
			DataRequest();
		}

		void DataRequest()
		{
			int triCd = 0;
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
                    case TargetTableNm:
                        if (tbl.Rows.Count == 0)
                        {
                            MstData = tbl.NewRow();
                        }
                        else
                        {
                            MstData = tbl.Rows[0];

                            自社ID = MstData["自社ID"].ToString();
                        }
                        //自社ID変更不可
                        LabelTextJisya.Text1IsReadOnly = true;
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

		/// <summary>
		/// 最初のIDを表示するボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FistIdButton_Click(object sender, RoutedEventArgs e)
		{
            try
            {
                //先頭データ検索
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 0 }));
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
			try
			{
				//前データ検索
                int iJisyaId = 0;
                if (int.TryParse(自社ID, out iJisyaId))
                {
                    //自社マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iJisyaId, -1 }));
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
			try
			{
				//次データ検索
                int iJisyaId = 0;
                if (int.TryParse(自社ID, out iJisyaId))
                {
                    //自社マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iJisyaId, 1 }));
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
            try
            {
                //最後尾検索
                base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { null, 1 }));
            }
            catch (Exception)
            {
                return;
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
            自社ID = string.Empty;
            //商品ID変更不可
            LabelTextJisya.Text1IsReadOnly = false;
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
                int iJisyaId = 0;
                if (int.TryParse(自社ID, out iJisyaId))
                {
                    MstData["自社ID"] = iJisyaId;


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
            if (int.TryParse(自社ID, out iSyouhinId))
            {
                MstData["自社ID"] = iSyouhinId;

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
        /// 自社IDロストフォーカス時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelTextJisya_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                int iJisyaId = 0;
                if (int.TryParse(自社ID, out iJisyaId))
                {
                    //自社マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iJisyaId, 0 }));
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
