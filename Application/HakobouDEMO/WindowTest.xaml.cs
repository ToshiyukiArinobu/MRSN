using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using KyoeiSystem.Framework.Core;
using KyoeiSystem.Framework.Common;
using KyoeiSystem.Framework.Windows.ViewBase;
using KyoeiSystem.Framework.Reports;
using KyoeiSystem.Application.Windows.Views;
using KyoeiSystem.Framework.Reports.Preview;


namespace Hakobo
{

	/// <summary>
	/// テスト用初期画面
	/// </summary>
	public partial class WindowTest : WindowMenuBase
	{
		private const string ReqLOGIN = "SEARCH_LOGIN";
		private const string ReqLOGOUT = "Logout";
		#region プロパティ等

		private int _testcombovalue = -2;
		public int testcombovalue
		{
			get
			{
				return this._testcombovalue;
			}
			set
			{
				this._testcombovalue = value;
				NotifyPropertyChanged();
			}
		}

		private bool isIDSelected = true;
		public bool IsIDSelected
		{
			get
			{
				return this.isIDSelected;
			}
			set
			{
				this.isIDSelected = value;
				NotifyPropertyChanged();
			}
		}
		private bool isNameSelected = false;
		public bool IsNameSelected
		{
			get
			{
				return this.isNameSelected;
			}
			set
			{
				this.isNameSelected = value;
				NotifyPropertyChanged();
			}
		}
		private string[] _paramYM = { string.Empty, string.Empty };
		public string paramYYYY
		{
			get { return _paramYM[0]; }
			set { _paramYM[0] = value; NotifyPropertyChanged(); }
		}
		private object _drv_id;
		public object TBLDriverID
		{
			get { return _drv_id; }
			set { _drv_id = value; NotifyPropertyChanged(); }
		}
		public string paramMM
		{
			get { return _paramYM[1]; }
			set { _paramYM[1] = value; NotifyPropertyChanged(); }
		}

		private string currenttime = string.Empty;
		public string CurrentTime
		{
			get
			{
				return this.currenttime;
			}
			set
			{
				this.currenttime = value;
				NotifyPropertyChanged();
			}
		}

		private DataTable griddata = new DataTable();
		public DataTable TESTDATA
		{
			get
			{
				return this.griddata;
			}
			set
			{
				this.griddata = value;
				NotifyPropertyChanged();
			}
		}
		private string tokuisakiID;
		public string TokuisakiID
		{
			get
			{
				return this.tokuisakiID;
			}
			set
			{
				this.tokuisakiID = value;
				NotifyPropertyChanged();
			}
		}
		private string _tantoID;
		public string 担当者ID
		{
			get
			{
				return this._tantoID;
			}
			set
			{
				this._tantoID = value;
				NotifyPropertyChanged();
			}
		}

		private string changedColumns = string.Empty;
		public string ChangedColumns
		{
			get
			{
				return this.changedColumns;
			}
			set
			{
				this.changedColumns = value;
				NotifyPropertyChanged();
			}
		}
		private byte[] _pic;
		public byte[] Picture
		{
			get
			{
				return this._pic;
			}
			set
			{
				this._pic = value;
				NotifyPropertyChanged();
			}
		}

		private DateTime? _dateTest = null;
		public DateTime? DateTest
		{
			get { return _dateTest; }
			set { _dateTest = value; NotifyPropertyChanged(); }
		}

		#endregion

		public WindowTest()
			: base()
		{
			InitializeComponent();
			this.DataContext = this;
		}

		private void Window_Loaded_1(object sender, RoutedEventArgs e)
		{
			this.appLog.Debug("テストメニュ開始");
			//TimerLoopStart(500);
			//AppCommon.SetutpComboboxList(this.cmbTest, true);

		}

		private void Button_Click_Printer(object sender, RoutedEventArgs e)
		{
			PrinterSettingWindow pset = new PrinterSettingWindow();
			bool? ret = pset.ShowDialog(this);
			if (ret == true)
			{
			}
		}

		private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.threadmgr.Stop();
			//base.WindowClosing(sender, e);
		}

		private void OnColumnChanged(object sender, DataColumnChangeEventArgs e)
		{
			this.ChangedColumns = string.Format("{0} -> {1}", e.Column.ColumnName, e.ProposedValue);
		}

		private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
		{
			if (e.EditAction == DataGridEditAction.Cancel)
			{
				return;
			}

		}

		/// <summary>
		/// 締日集計処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			try
			{
				TKS31010 frm = new TKS31010();
				frm.ShowDialog(this);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 運転日報画面ボタン押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_3(object sender, RoutedEventArgs e)
		{
			try
			{
				DLY01010 frm = new DLY01010();
				frm.ShowDialog(this);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 乗務員マスタ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_4(object sender, RoutedEventArgs e)
		{
			try
			{
				MST04010 frm = new MST04010();
				frm.Show(this);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 乗務員マスタ一括入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_乗務員一括入力(object sender, RoutedEventArgs e)
		{
            try
            {
                MST04020 frm = new MST04020();
                frm.Show(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
		}

		/// <summary>
		/// その他画面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_5(object sender, RoutedEventArgs e)
		{
			try
			{
				DLY10010 frm = new DLY10010();

				frm.ShowDialog(this);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 請求書発行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_6(object sender, RoutedEventArgs e)
		{
			try
			{
                TKS01010 frm = new TKS01010();

				frm.ShowDialog(this);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 取引先マスタ問合せ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_8(object sender, RoutedEventArgs e)
		{
			try
			{
				MST01020 frm = new MST01020();
				frm.ShowDialog(this);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		/// <summary>
		/// 取引先マスタ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_9(object sender, RoutedEventArgs e)
		{
			try
			{
				MST01010 frm = new MST01010();
				frm.ShowDialog(this);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 請求内訳マスタ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_10(object sender, RoutedEventArgs e)
		{
			try
			{
				MST02010 frm = new MST02010();
				frm.ShowDialog(this);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}

		/// <summary>
		/// 日報入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_11(object sender, RoutedEventArgs e)
		{
			try
			{
				DLY05010 frm = new DLY05010();
				frm.ShowDialog(this);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 入金伝票入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_12(object sender, RoutedEventArgs e)
		{
			try
			{
				DLY08010 frm = new DLY08010();
				frm.ShowDialog(this);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}
		/// <summary>
		/// メインメニュー
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_13(object sender, RoutedEventArgs e)
		{
			try
			{
				MessageBox.Show("未作成");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		/// <summary>
		/// 車輌月次経費入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_14(object sender, RoutedEventArgs e)
		{
			try
			{
				SRY06010 frm = new SRY06010();
				frm.ShowDialog(this);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 乗務員運行表印刷
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_15(object sender, RoutedEventArgs e)
		{
			try
			{
				DLY16010 frm = new DLY16010();
				frm.ShowDialog(this);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 乗務員収支実績表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_16(object sender, RoutedEventArgs e)
		{
			try
			{
				JMI11010 frm = new JMI11010();
				frm.ShowDialog(this);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			try
			{
				TKS13010 frm = new TKS13010();
				frm.ShowDialog(this);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        /// <summary>
		/// タイマーイベント受信時の処理
		/// </summary>
		/// <param name="message"></param>
		public override void OnReceivedTimer(CommunicationObject message)
		{
			this.CurrentTime = string.Format("{0:yyyy/MM/dd HH:mm.ss}", DateTime.Now);
		}


        #region マスタ[MST]
        /// <summary>
        /// 車種マスタ呼び出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_05(object sender, RoutedEventArgs e)
        {
            try
            {
                MST05010 frm = new MST05010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 発着地マスタ呼び出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST03(object sender, RoutedEventArgs e)
        {
            try
            {
                MST03010 frm = new MST03010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 商品マスタ呼び出し      
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_07(object sender, RoutedEventArgs e)
        {
            try
            {
                MST07010 frm = new MST07010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 摘要マスタ呼び出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_08(object sender, RoutedEventArgs e)
        {
            
            try
            {
                MST08010 frm = new MST08010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 経費項目マスタ呼び出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST09(object sender, RoutedEventArgs e)
        {
            try
            {
                MST09010 frm = new MST09010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        /// <summary>
        /// 自社部門マスタ呼び出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST10(object sender, RoutedEventArgs e)
        {
            try
            {
                MST10010 frm = new MST10010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 消費税率マスタ呼び出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST13(object sender, RoutedEventArgs e)
        {
            try
            {
                MST13010 frm = new MST13010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 支払先別軽油マスタ呼び出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST14(object sender, RoutedEventArgs e)
        {
            try
            {
                MST14010 frm = new MST14010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 得意先別車種別単価マスタ呼び出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST16(object sender, RoutedEventArgs e)
        {
            try
            {
                MST16010 frm = new MST16010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 得意先別距離別運賃マスタ呼び出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST17(object sender, RoutedEventArgs e)
        {
            try
            {
                MST17010 frm = new MST17010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        /// <summary>
        /// 得意先別個建単価マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST18(object sender, RoutedEventArgs e)
        {
            try
            {
                MST18010 frm = new MST18010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 支払先別車種単価マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST20(object sender, RoutedEventArgs e)
        {
            try
            {
                MST20010 frm = new MST20010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 支払先別距離別運賃マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST21(object sender, RoutedEventArgs e)
        {
            try
            {
                MST21010 frm = new MST21010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 支払先別個建単価マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST22(object sender, RoutedEventArgs e)
        {
            try
            {
                MST22010 frm = new MST22010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 担当者マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST23(object sender, RoutedEventArgs e)
        {
            try
            {
                MST23010 frm = new MST23010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// カレンダーマスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST24(object sender, RoutedEventArgs e)
        {
            try
            {
                MST24010 frm = new MST24010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 得意先別品名単価マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST19(object sender, RoutedEventArgs e)
        {
            try
            {
                MST19010 frm = new MST19010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 自社名マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST12(object sender, RoutedEventArgs e)
        {
            try
            {
                MST12010 frm = new MST12010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// コース配車マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST11(object sender, RoutedEventArgs e)
        {
            try
            {
                MST11010 frm = new MST11010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 得意先月次集計Ｆ修正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST25(object sender, RoutedEventArgs e)
        {
            try
            {
                MST25010 frm = new MST25010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 支払先月次集計Ｆ修正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST26(object sender, RoutedEventArgs e)
        {
            try
            {
                MST26010 frm = new MST26010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 基礎情報設定呼び出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST30(object sender, RoutedEventArgs e)
        {
            try
            {
                MST30010 frm = new MST30010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        /// <summary>
        /// 車両マスタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST06(object sender, RoutedEventArgs e)
        {
            try
            {
                MST06010 frm = new MST06010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 問合せ画面[MSTXX020]
        /// <summary>
        /// 車種マスタ問い合わせ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST05_2(object sender, RoutedEventArgs e)
        {
            try
            {
                MST05020 frm = new MST05020();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 車輌マスタ問い合わせ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST06_2(object sender, RoutedEventArgs e)
        {
            try
            {
                MST06020 frm = new MST06020();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 商品マスタ問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST07_2(object sender, RoutedEventArgs e)
        {
            try
            {
                MST07020 frm = new MST07020();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 摘要マスタ問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST08_2(object sender, RoutedEventArgs e)
        {
            try
            {
                MST08020 frm = new MST08020();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 自社部門マスタ問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST10_2(object sender, RoutedEventArgs e)
        {
            try
            {
                MST10020 frm = new MST10020();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 自社名マスタ問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST12_2(object sender, RoutedEventArgs e)
        {
            try
            {
                MST12020 frm = new MST12020();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 支払先別軽油マスタ問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST14_2(object sender, RoutedEventArgs e)
        {
            try
            {
                MST14020 frm = new MST14020();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// コース配車マスタ問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST11_2(object sender, RoutedEventArgs e)
        {
            try
            {
                MST11020 frm = new MST11020();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 得意先別車種別単価マスタ入力問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST16_2(object sender, RoutedEventArgs e)
        {
            try
            {
                MST16020 frm = new MST16020();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 支払先別車種別単価マスタ入力問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST20_2(object sender, RoutedEventArgs e)
        {
            try
            {
                MST20020 frm = new MST20020();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 得意先別距離別単価マスタ入力問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST17_2(object sender, RoutedEventArgs e)
        {
            try
            {
                MST17020 frm = new MST17020();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 支払先別距離別単価マスタ入力問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST21_2(object sender, RoutedEventArgs e)
        {
            try
            {
                MST21020 frm = new MST21020();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 消費税マスタ問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST13_2(object sender, RoutedEventArgs e)
        {
            try
            {
                MST13020 frm = new MST13020();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 得意先別個建単価マスタ問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST18_2(object sender, RoutedEventArgs e)
        {
            try
            {
                MST18020 frm = new MST18020();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 支払先別地区単価マスタ問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST19_2(object sender, RoutedEventArgs e)
        {
            try
            {
                MST19020 frm = new MST19020();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 支払先別個建単価マスタ問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST22_2(object sender, RoutedEventArgs e)
        {
            try
            {
                MST22020 frm = new MST22020();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 担当者マスタ問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_MST23_2(object sender, RoutedEventArgs e)
        {
            try
            {
                MST23020 frm = new MST23020();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 検索画面[SCH]
        /// <summary>
        /// 車種検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SCH05(object sender, RoutedEventArgs e)
        {
            try
            {
                SCH05010 frm = new SCH05010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 車輌検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SCH06(object sender, RoutedEventArgs e)
        {
            try
            {
                SCH06010 frm = new SCH06010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// コース配車検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SCH11(object sender, RoutedEventArgs e)
        {
            try
            {
                SCH11010 frm = new SCH11010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 自社名検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SCH12(object sender, RoutedEventArgs e)
        {
            try
            {
                SCH12010 frm = new SCH12010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 商品検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SCH07(object sender, RoutedEventArgs e)
        {
            try
            {
                SCH07010 frm = new SCH07010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 摘要検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SCH08(object sender, RoutedEventArgs e)
        {
            try
            {
                SCH08010 frm = new SCH08010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 担当者検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SCH13(object sender, RoutedEventArgs e)
        {
            try
            {
                SCH13010 frm = new SCH13010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 得意先検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SCH14(object sender, RoutedEventArgs e)
        {
            try
            {
                SCH01010 sch01010 = new SCH01010();
                sch01010.表示区分 = 1;
                sch01010.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 支払先検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SCH15(object sender, RoutedEventArgs e)
        {
            try
            {
                SCH01010 sch01010 = new SCH01010();
                sch01010.表示区分 = 2;
                sch01010.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 仕入先検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SCH16(object sender, RoutedEventArgs e)
        {
            try
            {
                SCH01010 sch01010 = new SCH01010();
                sch01010.表示区分 = 3;
                sch01010.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 自社部門検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SCH10(object sender, RoutedEventArgs e)
        {
            try
            {
                SCH10010 frm = new SCH10010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 経費先コード検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SCH17(object sender, RoutedEventArgs e)
        {
            try
            {
                SCH01030 frm = new SCH01030();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region 日次[DLY]
        /// <summary>
        /// コース配車入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_DLY02(object sender, RoutedEventArgs e)
        {
            try
            {
                DLY02010 frm = new DLY02010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 配車表入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_DLY03(object sender, RoutedEventArgs e)
        {
            try
            {
                DLY03010 frm = new DLY03010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 配車入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_DLY04(object sender, RoutedEventArgs e)
        {
            try
            {
                DLY04010 frm = new DLY04010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 売上明細問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_DLY10(object sender, RoutedEventArgs e)
        {
            try
            {
                DLY10010 frm = new DLY10010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 支払明細問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_DLY11(object sender, RoutedEventArgs e)
        {
            try
            {
                DLY11010 frm = new DLY11010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 乗務員・車輌明細問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_DLY12(object sender, RoutedEventArgs e)
        {
            try
            {
                DLY12010 frm = new DLY12010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 運転日報明細問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_DLY13(object sender, RoutedEventArgs e)
        {
            try
            {
                DLY13010 frm = new DLY13010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 支払先別日計収支管理表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_DLY22(object sender, RoutedEventArgs e)
        {
            try
            {
                DLY22010 frm = new DLY22010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 車輌別日計収支管理表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_DLY23(object sender, RoutedEventArgs e)
        {
            try
            {
                DLY23010 frm = new DLY23010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 乗務員別日計収支管理表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_DLY24(object sender, RoutedEventArgs e)
        {
            try
            {
                DLY24010 frm = new DLY24010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 日別売上管理表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_DLY25(object sender, RoutedEventArgs e)
        {
            try
            {
                DLY25010 frm = new DLY25010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 車輌運行表印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_DLY17(object sender, RoutedEventArgs e)
        {
            try
            {
                DLY17010 frm = new DLY17010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 運転作業日報印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_DLY19(object sender, RoutedEventArgs e)
        {
            try
            {
                DLY19010 frm = new DLY19010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 乗務員管理[JMI]
        /// <summary>
        /// 乗務員管理表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_JMI01(object sender, RoutedEventArgs e)
        {
            try
            {
                JMI01010 frm = new JMI01010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 乗務員管理表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_JMI02(object sender, RoutedEventArgs e)
        {
            try
            {
                JMI02010 frm = new JMI02010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 乗務員労務管理表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_JMI03(object sender, RoutedEventArgs e)
        {
            try
            {
                JMI03010 frm = new JMI03010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 乗務員管理合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_JMI04(object sender, RoutedEventArgs e)
        {
            try
            {
                JMI04010 frm = new JMI04010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 乗務員売上明細書
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_JMI05(object sender, RoutedEventArgs e)
        {
            try
            {
                JMI05010 frm = new JMI05010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 乗務員売上日計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_JMI06(object sender, RoutedEventArgs e)
        {
            try
            {
                JMI06010 frm = new JMI06010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 乗務員売上合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_JMI07(object sender, RoutedEventArgs e)
        {
            try
            {
                JMI07010 frm = new JMI07010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 乗務員状況履歴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_JMI08(object sender, RoutedEventArgs e)
        {
            try
            {
                JMI08010 frm = new JMI08010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 乗務員月次集計
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_JMI09(object sender, RoutedEventArgs e)
        {
            try
            {
                JMI09010 frm = new JMI09010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 乗務員月別収支合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_JMI12(object sender, RoutedEventArgs e)
        {
            try
            {
                JMI12010 frm = new JMI12010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 乗務員別収支合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_JMI13(object sender, RoutedEventArgs e)
        {
            try
            {
                JMI13010 frm = new JMI13010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 乗務員運転免許管理表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_JMI14(object sender, RoutedEventArgs e)
        {
            try
            {
                JMI14010 frm = new JMI14010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 乗務員月次経費入力画面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_JMI10(object sender, RoutedEventArgs e)
        {
            try
            {
                JMI10010 frm = new JMI10010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 車輌管理[SRY]
        /// <summary>
        /// 車輌管理表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SRY01(object sender, RoutedEventArgs e)
        {
            try
            {
                SRY01010 frm = new SRY01010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 車輌売上明細表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SRY02(object sender, RoutedEventArgs e)
        {
            try
            {
                SRY02010 frm = new SRY02010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 車輌別日計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SRY03(object sender, RoutedEventArgs e)
        {
            try
            {
                SRY03010 frm = new SRY03010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 車種別日計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SRY04(object sender, RoutedEventArgs e)
        {
            try
            {
                SRY04010 frm = new SRY04010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 車輌合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SRY09(object sender, RoutedEventArgs e)
        {
            try
            {
                SRY09010 frm = new SRY09010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 車輌統計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SRY10(object sender, RoutedEventArgs e)
        {
            try
            {
                SRY10010 frm = new SRY10010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 車種収支実績表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SRY11(object sender, RoutedEventArgs e)
        {
            try
            {
                SRY11010 frm = new SRY11010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 車種合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SRY12(object sender, RoutedEventArgs e)
        {
            try
            {
                SRY12010 frm = new SRY12010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 車種統計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SRY13(object sender, RoutedEventArgs e)
        {
            try
            {
                SRY13010 frm = new SRY13010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 輸送実績報告書
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SRY14(object sender, RoutedEventArgs e)
        {
            try
            {
                SRY14010 frm = new SRY14010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 車輌別収支合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SRY15(object sender, RoutedEventArgs e)
        {
            try
            {
                SRY15010 frm = new SRY15010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 車輌別経費明細一覧表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SRY16(object sender, RoutedEventArgs e)
        {
            try
            {
                SRY16010 frm = new SRY16010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 車輌別燃料消費量累計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SRY18(object sender, RoutedEventArgs e)
        {
            try
            {
                SRY18010 frm = new SRY18010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 車輌別燃料消費量実績表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SRY19(object sender, RoutedEventArgs e)
        {
            try
            {
                SRY19010 frm = new SRY19010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 支払先管理[SHR]
        /// <summary>
        /// 配送依頼書印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SHR01(object sender, RoutedEventArgs e)
        {
            try
            {
                SHR01010 frm = new SHR01010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 支払先明細書印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SHR02(object sender, RoutedEventArgs e)
        {
            try
            {
                SHR02010 frm = new SHR02010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 支払経費明細書印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SHR03(object sender, RoutedEventArgs e)
        {
            try
            {
                SHR03010 frm = new SHR03010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 支払月次集計
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SHR04(object sender, RoutedEventArgs e)
        {
            try
            {
                SHR04010 frm = new SHR04010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 支払先合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SHR05(object sender, RoutedEventArgs e)
        {
            try
            {
                SHR05010 frm = new SHR05010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 支払先累積表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SHR06(object sender, RoutedEventArgs e)
        {
            try
            {
                SHR06010 frm = new SHR06010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 支払先残高問合せ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SHR07(object sender, RoutedEventArgs e)
        {
            try
            {
                SHR07010 frm = new SHR07010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 支払先一覧表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SHR08(object sender, RoutedEventArgs e)
        {
            try
            {
                SHR08010 frm = new SHR08010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 支払先予定表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SHR09(object sender, RoutedEventArgs e)
        {
            try
            {
                SHR09010 frm = new SHR09010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// 支払先別支払先日計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SHR10(object sender, RoutedEventArgs e)
        {
            try
            {
                SHR10010 frm = new SHR10010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_LOGIN(object sender, RoutedEventArgs e)
        {
            try
            {
                LOGIN frm = new LOGIN();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region 年次[NNG]
        /// <summary>
        /// 支払先月別合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_NNG02(object sender, RoutedEventArgs e)
        {
            try
            {
                NNG02010 frm = new NNG02010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 乗務員月別合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_NNG03(object sender, RoutedEventArgs e)
        {
            try
            {
                NNG03010 frm = new NNG03010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 車輌月別合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_NNG04(object sender, RoutedEventArgs e)
        {
            try
            {
                NNG04010 frm = new NNG04010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 車種月別合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_NNG05(object sender, RoutedEventArgs e)
        {
            try
            {
                NNG05010 frm = new NNG05010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 部門別売上合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_NNG06(object sender, RoutedEventArgs e)
        {
            try
            {
                NNG06010 frm = new NNG06010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 部門別売上日計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_NNG07(object sender, RoutedEventArgs e)
        {
            try
            {
                NNG07010 frm = new NNG07010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 部門月別合計表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_NNG08(object sender, RoutedEventArgs e)
        {
            try
            {
                NNG08010 frm = new NNG08010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 得意先管理[TKS]

           private void Button_Click_TKS02(object sender, RoutedEventArgs e)
        {
            try
            {
                TKS02010 frm = new TKS02010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_TKS04(object sender, RoutedEventArgs e)
        {
            try
            {
                TKS04010 frm = new TKS04010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_TKS06(object sender, RoutedEventArgs e)
        {
            try
            {
                TKS06010 frm = new TKS06010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_TKS07(object sender, RoutedEventArgs e)
        {
            try
            {
                TKS07010 frm = new TKS07010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_TKS08(object sender, RoutedEventArgs e)
        {
            try
            {
                TKS08010 frm = new TKS08010();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void ExitButtonClicked(object sender, RoutedEventArgs e)
		{
			//this.Close();
			Application.Current.Shutdown();
		}

		private void UcTextBox_cTextChanged_1(object sender, RoutedEventArgs e)
		{
			appLog.Debug("UcTextBox_cTextChanged_1");
		}

		private void UcLabelTextBox_cTextChanged_1(object sender, RoutedEventArgs e)
		{
			appLog.Debug("UcLabelTextBox_cTextChanged_1");
		}

		private void LoginClicked(object sender, RoutedEventArgs e)
		{
			int id;
			if (int.TryParse(this.担当者ID, out id) != true)
			{
				MessageBox.Show("担当者IDは数値を入力してください");
				return;
			}

			SendRequest(new CommunicationObject(MessageType.RequestData, ReqLOGIN, id, typeof(UserConfig)));
		}
     
		UserConfig usercfg = null;
		CommonConfig ccfg = null;
		TestMenuConfig mcfg = null;

		public override void OnReceivedResponseData(CommunicationObject message)
		{
			var data = message.GetResultData();
			DataTable tbl = (data is DataTable) ? data as DataTable : null;
			switch (message.GetMessageName())
			{
			case ReqLOGIN:
				usercfg = tbl.Rows[0]["設定項目"] as UserConfig;
				if (usercfg == null) { usercfg = new UserConfig(); }
				AppCommon.SetupConfig(this, usercfg);
				ccfg = (CommonConfig)usercfg.GetConfigValue(typeof(CommonConfig));
				if (ccfg == null)
				{
					ccfg = new CommonConfig();
				}
				ccfg.ログイン時刻 = DateTime.Now;
				usercfg.SetConfigValue(ccfg);

				mcfg = (TestMenuConfig)usercfg.GetConfigValue(typeof(TestMenuConfig));
				if (mcfg == null)
				{
					mcfg = new TestMenuConfig();
					mcfg.Top = this.Top;
					mcfg.Left = this.Left;
					usercfg.SetConfigValue(mcfg);
				}
				this.Top = mcfg.Top;
				this.Left = mcfg.Left;
				break;
			case ReqLOGOUT:
				break;
			}
		}
        
		public override void OnReveivedError(CommunicationObject message)
		{
			base.OnReveivedError(message);
			this.Message = base.ErrorMessage;
			switch (message.GetMessageName())
			{
			case ReqLOGIN:
				// 本来、ログイン失敗した場合は何も操作できないのでConfigの初期化も必要ない
				// ここはテストのために初期化する
				usercfg = new UserConfig();
				AppCommon.SetupConfig(this, usercfg);
				ccfg = (CommonConfig)usercfg.GetConfigValue(typeof(CommonConfig));
				if (ccfg == null)
				{
					ccfg = new CommonConfig();
				}
				ccfg.ログイン時刻 = DateTime.Now;
				usercfg.SetConfigValue(ccfg);

				mcfg = (TestMenuConfig)usercfg.GetConfigValue(typeof(TestMenuConfig));
				if (mcfg == null)
				{
					mcfg = new TestMenuConfig();
					mcfg.Top = this.Top;
					mcfg.Left = this.Left;
					usercfg.SetConfigValue(mcfg);
				}
				this.Top = mcfg.Top;
				this.Left = mcfg.Left;

				MessageBox.Show("ログイン失敗しました（" + message.ErrorType + " : " + message.GetParameters()[0] + "）");
				break;
			}
		}

		private void CfgUpdateClicked(object sender, RoutedEventArgs e)
		{
			int id;
			if (int.TryParse(this.担当者ID, out id) != true)
			{
				MessageBox.Show("担当者IDは数値を入力してください");
				return;
			}
			ccfg = (CommonConfig)usercfg.GetConfigValue(typeof(CommonConfig));
			ccfg.ログアウト時刻 = DateTime.Now;
            ccfg.ユーザID = id;
			ccfg.ユーザ名 = "";
			usercfg.SetConfigValue(ccfg);
            mcfg.gamenID = new string[] { "newG", "newG", "newG", "newG", "newG", "newG", "newG", };
			mcfg.Top = this.Top;
			mcfg.Left = this.Left;
			usercfg.SetConfigValue(mcfg);
			SendRequest(new CommunicationObject(MessageType.UpdateData, ReqLOGOUT, id, AppCommon.GetConfig(this)));
		}


		public class TestMenuConfig : FormConfigBase
		{
			public string[] gamenID = { "メニュー", "テスト" };
		}

        
        
        

        
        
    }
}