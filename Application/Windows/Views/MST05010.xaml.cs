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
	/// 車種マスタ入力
	/// </summary>
	public partial class MST05010 : WindowMasterMainteBase
	{
        //対象テーブル検索用
        private const string TargetTableNm = "M06_SYA_UC";
        //対象テーブル更新用
        private const string TargetTableNmUpdate = "M06_SYA_UPD";
        //対象テーブル削除用
        private const string TargetTableNmDelete = "M06_SYA_DEL";

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
    

        private string _車種ID = string.Empty;
        public string 車種ID
        {
            get { return this._車種ID; }
            set { this._車種ID = value; NotifyPropertyChanged(); }
        }

        
        /// <summary>
		/// 車種マスタ入力
		/// </summary>
		public MST05010()
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

                            車種ID = MstData["車種ID"].ToString();
                        }
                        //車種ID変更不可
                        LabelTextSyaSyuCode.Text1IsReadOnly = true;
					break;
                    case TargetTableNmUpdate:
                    
                        MessageBox.Show("登録が完了しました。");
                        //コントロール初期化
                        UcInitialize();
                        
                    break;
                    case TargetTableNmDelete:
                        MessageBox.Show("データを削除しました。");
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
			
			//先頭データ検索
            try
            {
                //車種マスタ
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
                int iSyasyuId = 0;
                if (int.TryParse(車種ID, out iSyasyuId))
                {
                    //車種マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iSyasyuId, -1 }));
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
                int iSyasyuId = 0;
                if (int.TryParse(車種ID, out iSyasyuId))
                {
                    //車種マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iSyasyuId, 1 }));
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
                //車種マスタ
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
            MST05020 mst05020 = new MST05020();

            mst05020.ShowDialog();
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


        #region イベント
        /// <summary>
        /// 車種コードテキストボックスロストフォーカス時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelTextSyaSyuCode_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                int iSyasyuId = 0;
                if(int.TryParse(車種ID, out iSyasyuId))
                {
                    //車種マスタ
                    base.SendRequest(new CommunicationObject(MessageType.RequestData, TargetTableNm, new object[] { iSyasyuId , 0}));
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        #endregion

        #region 処理メソッド
        private void UcInitialize()
        {
            車種ID = string.Empty;
            MstData = null;

            LabelTextSyaSyuCode.Text1IsReadOnly = false;


            //フォーカス設定
            SetFocusToTopControl();
        }

        private void Update()
        {
            
            try
            {
                int iSyasyuId = 0;
                if (int.TryParse(車種ID, out iSyasyuId))
                {
                    MstData["車種ID"] = iSyasyuId;

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
        /// 削除メソッド
        /// </summary>
        private void Delete()
        {
            int iSyasyuId = 0;
            if (int.TryParse(車種ID, out iSyasyuId))
            {
                MstData["車種ID"] = iSyasyuId;

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

        #endregion
    }
}
